Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__ANEX
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

  Public oCleAnex As CLE__ANEX
  Public dsAnex As New DataSet
  Public oCallParams As CLE__CLDP
  Public dcAnex As BindingSource = New BindingSource

  Private components As System.ComponentModel.IContainer


  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__ANEX))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.tsAnex = New NTSInformatica.NTSTabControl
    Me.NtsTabPage1 = New NTSInformatica.NTSTabPage
    Me.pnPage1 = New NTSInformatica.NTSPanel
    Me.lbHelptipo3 = New NTSInformatica.NTSLabel
    Me.lbHelptipo2 = New NTSInformatica.NTSLabel
    Me.lbHelptipo1 = New NTSInformatica.NTSLabel
    Me.lbAx_desext1 = New NTSInformatica.NTSLabel
    Me.lbAx_tipo1 = New NTSInformatica.NTSLabel
    Me.edAx_tipo1 = New NTSInformatica.NTSTextBoxStr
    Me.edAx_desext3 = New NTSInformatica.NTSMemoBox
    Me.lbAx_desext3 = New NTSInformatica.NTSLabel
    Me.lbAx_tipo2 = New NTSInformatica.NTSLabel
    Me.edAx_tipo2 = New NTSInformatica.NTSTextBoxStr
    Me.edAx_desext2 = New NTSInformatica.NTSMemoBox
    Me.lbAx_desext2 = New NTSInformatica.NTSLabel
    Me.lbAx_tipo3 = New NTSInformatica.NTSLabel
    Me.edAx_tipo3 = New NTSInformatica.NTSTextBoxStr
    Me.edAx_desext1 = New NTSInformatica.NTSMemoBox
    Me.NtsTabPage2 = New NTSInformatica.NTSTabPage
    Me.pnPage2 = New NTSInformatica.NTSPanel
    Me.lbAx_descr1 = New NTSInformatica.NTSLabel
    Me.edAx_descr1 = New NTSInformatica.NTSTextBoxStr
    Me.lbAx_descr2 = New NTSInformatica.NTSLabel
    Me.edAx_descr2 = New NTSInformatica.NTSTextBoxStr
    Me.lbAx_descr3 = New NTSInformatica.NTSLabel
    Me.edAx_descr3 = New NTSInformatica.NTSTextBoxStr
    Me.lbAx_descr4 = New NTSInformatica.NTSLabel
    Me.edAx_descr4 = New NTSInformatica.NTSTextBoxStr
    Me.lbAx_descr5 = New NTSInformatica.NTSLabel
    Me.edAx_descr5 = New NTSInformatica.NTSTextBoxStr
    Me.lbAx_descr6 = New NTSInformatica.NTSLabel
    Me.edAx_descr6 = New NTSInformatica.NTSTextBoxStr
    Me.lbAx_descr7 = New NTSInformatica.NTSLabel
    Me.edAx_descr7 = New NTSInformatica.NTSTextBoxStr
    Me.lbAx_descr8 = New NTSInformatica.NTSLabel
    Me.edAx_descr8 = New NTSInformatica.NTSTextBoxStr
    Me.lbAx_descr9 = New NTSInformatica.NTSLabel
    Me.edAx_descr9 = New NTSInformatica.NTSTextBoxStr
    Me.lbAx_descr10 = New NTSInformatica.NTSLabel
    Me.edAx_descr10 = New NTSInformatica.NTSTextBoxStr
    Me.lbAx_data1 = New NTSInformatica.NTSLabel
    Me.edAx_data1 = New NTSInformatica.NTSTextBoxData
    Me.lbAx_data2 = New NTSInformatica.NTSLabel
    Me.edAx_data2 = New NTSInformatica.NTSTextBoxData
    Me.lbAx_data3 = New NTSInformatica.NTSLabel
    Me.edAx_data3 = New NTSInformatica.NTSTextBoxData
    Me.lbAx_data4 = New NTSInformatica.NTSLabel
    Me.edAx_data4 = New NTSInformatica.NTSTextBoxData
    Me.lbAx_data5 = New NTSInformatica.NTSLabel
    Me.edAx_data5 = New NTSInformatica.NTSTextBoxData
    Me.NtsTabPage3 = New NTSInformatica.NTSTabPage
    Me.pnPage3 = New NTSInformatica.NTSPanel
    Me.lbAx_memo1 = New NTSInformatica.NTSLabel
    Me.edAx_memo1 = New NTSInformatica.NTSMemoBox
    Me.NtsTabPage4 = New NTSInformatica.NTSTabPage
    Me.pnPage4 = New NTSInformatica.NTSPanel
    Me.lbAx_memo2 = New NTSInformatica.NTSLabel
    Me.edAx_memo2 = New NTSInformatica.NTSMemoBox
    Me.NtsTabPage5 = New NTSInformatica.NTSTabPage
    Me.pnPage5 = New NTSInformatica.NTSPanel
    Me.lbAx_num1 = New NTSInformatica.NTSLabel
    Me.edAx_num1 = New NTSInformatica.NTSTextBoxNum
    Me.lbAx_num2 = New NTSInformatica.NTSLabel
    Me.edAx_num2 = New NTSInformatica.NTSTextBoxNum
    Me.lbAx_num3 = New NTSInformatica.NTSLabel
    Me.edAx_num3 = New NTSInformatica.NTSTextBoxNum
    Me.lbAx_num4 = New NTSInformatica.NTSLabel
    Me.edAx_num4 = New NTSInformatica.NTSTextBoxNum
    Me.lbAx_num5 = New NTSInformatica.NTSLabel
    Me.edAx_num5 = New NTSInformatica.NTSTextBoxNum
    Me.lbAx_num6 = New NTSInformatica.NTSLabel
    Me.edAx_num6 = New NTSInformatica.NTSTextBoxNum
    Me.lbAx_num7 = New NTSInformatica.NTSLabel
    Me.edAx_num7 = New NTSInformatica.NTSTextBoxNum
    Me.lbAx_num8 = New NTSInformatica.NTSLabel
    Me.edAx_num8 = New NTSInformatica.NTSTextBoxNum
    Me.lbAx_num9 = New NTSInformatica.NTSLabel
    Me.edAx_num9 = New NTSInformatica.NTSTextBoxNum
    Me.lbAx_num10 = New NTSInformatica.NTSLabel
    Me.edAx_num10 = New NTSInformatica.NTSTextBoxNum
    Me.NtsTabPage6 = New NTSInformatica.NTSTabPage
    Me.pnPage6 = New NTSInformatica.NTSPanel
    Me.ckAx_check1 = New NTSInformatica.NTSCheckBox
    Me.ckAx_check3 = New NTSInformatica.NTSCheckBox
    Me.ckAx_check5 = New NTSInformatica.NTSCheckBox
    Me.ckAx_check7 = New NTSInformatica.NTSCheckBox
    Me.ckAx_check9 = New NTSInformatica.NTSCheckBox
    Me.ckAx_check2 = New NTSInformatica.NTSCheckBox
    Me.ckAx_check4 = New NTSInformatica.NTSCheckBox
    Me.ckAx_check6 = New NTSInformatica.NTSCheckBox
    Me.ckAx_check8 = New NTSInformatica.NTSCheckBox
    Me.ckAx_check10 = New NTSInformatica.NTSCheckBox
    Me.lbAx_combo1 = New NTSInformatica.NTSLabel
    Me.cbAx_combo1 = New NTSInformatica.NTSComboBox
    Me.lbAx_combo2 = New NTSInformatica.NTSLabel
    Me.cbAx_combo2 = New NTSInformatica.NTSComboBox
    Me.lbAx_combo3 = New NTSInformatica.NTSLabel
    Me.cbAx_combo3 = New NTSInformatica.NTSComboBox
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tsAnex, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsAnex.SuspendLayout()
    Me.NtsTabPage1.SuspendLayout()
    CType(Me.pnPage1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPage1.SuspendLayout()
    CType(Me.edAx_tipo1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_desext3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_tipo2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_desext2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_tipo3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_desext1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage2.SuspendLayout()
    CType(Me.pnPage2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPage2.SuspendLayout()
    CType(Me.edAx_descr1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_descr2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_descr3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_descr4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_descr5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_descr6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_descr7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_descr8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_descr9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_descr10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_data1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_data2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_data3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_data4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_data5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage3.SuspendLayout()
    CType(Me.pnPage3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPage3.SuspendLayout()
    CType(Me.edAx_memo1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage4.SuspendLayout()
    CType(Me.pnPage4, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPage4.SuspendLayout()
    CType(Me.edAx_memo2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage5.SuspendLayout()
    CType(Me.pnPage5, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPage5.SuspendLayout()
    CType(Me.edAx_num1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_num2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_num3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_num4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_num5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_num6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_num7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_num8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_num9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAx_num10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage6.SuspendLayout()
    CType(Me.pnPage6, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPage6.SuspendLayout()
    CType(Me.ckAx_check1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAx_check3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAx_check5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAx_check7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAx_check9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAx_check2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAx_check4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAx_check6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAx_check8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAx_check10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAx_combo1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAx_combo2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAx_combo3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 26
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
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
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 18
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 19
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'tsAnex
    '
    Me.tsAnex.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tsAnex.Location = New System.Drawing.Point(0, 26)
    Me.tsAnex.Name = "tsAnex"
    Me.tsAnex.SelectedTabPage = Me.NtsTabPage1
    Me.tsAnex.Size = New System.Drawing.Size(670, 380)
    Me.tsAnex.TabIndex = 4
    Me.tsAnex.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.NtsTabPage1, Me.NtsTabPage2, Me.NtsTabPage3, Me.NtsTabPage4, Me.NtsTabPage5, Me.NtsTabPage6})
    Me.tsAnex.Text = "NtsTabControl1"
    '
    'NtsTabPage1
    '
    Me.NtsTabPage1.AllowDrop = True
    Me.NtsTabPage1.Controls.Add(Me.pnPage1)
    Me.NtsTabPage1.Enable = True
    Me.NtsTabPage1.Name = "NtsTabPage1"
    Me.NtsTabPage1.Size = New System.Drawing.Size(661, 350)
    Me.NtsTabPage1.Text = "&1 - Dati 1"
    '
    'pnPage1
    '
    Me.pnPage1.AllowDrop = True
    Me.pnPage1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPage1.Appearance.Options.UseBackColor = True
    Me.pnPage1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPage1.Controls.Add(Me.lbHelptipo3)
    Me.pnPage1.Controls.Add(Me.lbHelptipo2)
    Me.pnPage1.Controls.Add(Me.lbHelptipo1)
    Me.pnPage1.Controls.Add(Me.lbAx_desext1)
    Me.pnPage1.Controls.Add(Me.lbAx_tipo1)
    Me.pnPage1.Controls.Add(Me.edAx_tipo1)
    Me.pnPage1.Controls.Add(Me.edAx_desext3)
    Me.pnPage1.Controls.Add(Me.lbAx_desext3)
    Me.pnPage1.Controls.Add(Me.lbAx_tipo2)
    Me.pnPage1.Controls.Add(Me.edAx_tipo2)
    Me.pnPage1.Controls.Add(Me.edAx_desext2)
    Me.pnPage1.Controls.Add(Me.lbAx_desext2)
    Me.pnPage1.Controls.Add(Me.lbAx_tipo3)
    Me.pnPage1.Controls.Add(Me.edAx_tipo3)
    Me.pnPage1.Controls.Add(Me.edAx_desext1)
    Me.pnPage1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPage1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPage1.Location = New System.Drawing.Point(0, 0)
    Me.pnPage1.Name = "pnPage1"
    Me.pnPage1.Size = New System.Drawing.Size(661, 350)
    Me.pnPage1.TabIndex = 526
    Me.pnPage1.Text = "NtsPanel6"
    '
    'lbHelptipo3
    '
    Me.lbHelptipo3.AutoSize = True
    Me.lbHelptipo3.BackColor = System.Drawing.Color.Transparent
    Me.lbHelptipo3.Location = New System.Drawing.Point(272, 68)
    Me.lbHelptipo3.Name = "lbHelptipo3"
    Me.lbHelptipo3.NTSDbField = ""
    Me.lbHelptipo3.Size = New System.Drawing.Size(60, 13)
    Me.lbHelptipo3.TabIndex = 518
    Me.lbHelptipo3.Text = "lbHelptipo3"
    '
    'lbHelptipo2
    '
    Me.lbHelptipo2.AutoSize = True
    Me.lbHelptipo2.BackColor = System.Drawing.Color.Transparent
    Me.lbHelptipo2.Location = New System.Drawing.Point(272, 42)
    Me.lbHelptipo2.Name = "lbHelptipo2"
    Me.lbHelptipo2.NTSDbField = ""
    Me.lbHelptipo2.Size = New System.Drawing.Size(60, 13)
    Me.lbHelptipo2.TabIndex = 517
    Me.lbHelptipo2.Text = "lbHelptipo2"
    '
    'lbHelptipo1
    '
    Me.lbHelptipo1.AutoSize = True
    Me.lbHelptipo1.BackColor = System.Drawing.Color.Transparent
    Me.lbHelptipo1.Location = New System.Drawing.Point(272, 16)
    Me.lbHelptipo1.Name = "lbHelptipo1"
    Me.lbHelptipo1.NTSDbField = ""
    Me.lbHelptipo1.Size = New System.Drawing.Size(60, 13)
    Me.lbHelptipo1.TabIndex = 516
    Me.lbHelptipo1.Text = "lbHelptipo1"
    '
    'lbAx_desext1
    '
    Me.lbAx_desext1.AutoSize = True
    Me.lbAx_desext1.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_desext1.Location = New System.Drawing.Point(10, 93)
    Me.lbAx_desext1.Name = "lbAx_desext1"
    Me.lbAx_desext1.NTSDbField = ""
    Me.lbAx_desext1.Size = New System.Drawing.Size(58, 13)
    Me.lbAx_desext1.TabIndex = 23
    Me.lbAx_desext1.Text = "Descr2551"
    '
    'lbAx_tipo1
    '
    Me.lbAx_tipo1.AutoSize = True
    Me.lbAx_tipo1.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_tipo1.Location = New System.Drawing.Point(10, 16)
    Me.lbAx_tipo1.Name = "lbAx_tipo1"
    Me.lbAx_tipo1.NTSDbField = ""
    Me.lbAx_tipo1.Size = New System.Drawing.Size(33, 13)
    Me.lbAx_tipo1.TabIndex = 10
    Me.lbAx_tipo1.Text = "Tipo1"
    '
    'edAx_tipo1
    '
    Me.edAx_tipo1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_tipo1.Location = New System.Drawing.Point(239, 13)
    Me.edAx_tipo1.Name = "edAx_tipo1"
    Me.edAx_tipo1.NTSDbField = ""
    Me.edAx_tipo1.NTSForzaVisZoom = False
    Me.edAx_tipo1.NTSOldValue = ""
    Me.edAx_tipo1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_tipo1.Properties.MaxLength = 65536
    Me.edAx_tipo1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_tipo1.Size = New System.Drawing.Size(27, 20)
    Me.edAx_tipo1.TabIndex = 500
    '
    'edAx_desext3
    '
    Me.edAx_desext3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_desext3.Location = New System.Drawing.Point(239, 238)
    Me.edAx_desext3.Name = "edAx_desext3"
    Me.edAx_desext3.NTSDbField = ""
    Me.edAx_desext3.Properties.MaxLength = 65536
    Me.edAx_desext3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_desext3.Size = New System.Drawing.Size(416, 67)
    Me.edAx_desext3.TabIndex = 515
    '
    'lbAx_desext3
    '
    Me.lbAx_desext3.AutoSize = True
    Me.lbAx_desext3.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_desext3.Location = New System.Drawing.Point(10, 240)
    Me.lbAx_desext3.Name = "lbAx_desext3"
    Me.lbAx_desext3.NTSDbField = ""
    Me.lbAx_desext3.Size = New System.Drawing.Size(58, 13)
    Me.lbAx_desext3.TabIndex = 25
    Me.lbAx_desext3.Text = "Descr2553"
    '
    'lbAx_tipo2
    '
    Me.lbAx_tipo2.AutoSize = True
    Me.lbAx_tipo2.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_tipo2.Location = New System.Drawing.Point(10, 42)
    Me.lbAx_tipo2.Name = "lbAx_tipo2"
    Me.lbAx_tipo2.NTSDbField = ""
    Me.lbAx_tipo2.Size = New System.Drawing.Size(33, 13)
    Me.lbAx_tipo2.TabIndex = 11
    Me.lbAx_tipo2.Text = "Tipo2"
    '
    'edAx_tipo2
    '
    Me.edAx_tipo2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_tipo2.Location = New System.Drawing.Point(239, 39)
    Me.edAx_tipo2.Name = "edAx_tipo2"
    Me.edAx_tipo2.NTSDbField = ""
    Me.edAx_tipo2.NTSForzaVisZoom = False
    Me.edAx_tipo2.NTSOldValue = ""
    Me.edAx_tipo2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_tipo2.Properties.MaxLength = 65536
    Me.edAx_tipo2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_tipo2.Size = New System.Drawing.Size(27, 20)
    Me.edAx_tipo2.TabIndex = 501
    '
    'edAx_desext2
    '
    Me.edAx_desext2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_desext2.Location = New System.Drawing.Point(239, 165)
    Me.edAx_desext2.Name = "edAx_desext2"
    Me.edAx_desext2.NTSDbField = ""
    Me.edAx_desext2.Properties.MaxLength = 65536
    Me.edAx_desext2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_desext2.Size = New System.Drawing.Size(416, 67)
    Me.edAx_desext2.TabIndex = 514
    '
    'lbAx_desext2
    '
    Me.lbAx_desext2.AutoSize = True
    Me.lbAx_desext2.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_desext2.Location = New System.Drawing.Point(10, 167)
    Me.lbAx_desext2.Name = "lbAx_desext2"
    Me.lbAx_desext2.NTSDbField = ""
    Me.lbAx_desext2.Size = New System.Drawing.Size(58, 13)
    Me.lbAx_desext2.TabIndex = 24
    Me.lbAx_desext2.Text = "Descr2552"
    '
    'lbAx_tipo3
    '
    Me.lbAx_tipo3.AutoSize = True
    Me.lbAx_tipo3.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_tipo3.Location = New System.Drawing.Point(10, 68)
    Me.lbAx_tipo3.Name = "lbAx_tipo3"
    Me.lbAx_tipo3.NTSDbField = ""
    Me.lbAx_tipo3.Size = New System.Drawing.Size(33, 13)
    Me.lbAx_tipo3.TabIndex = 12
    Me.lbAx_tipo3.Text = "Tipo3"
    '
    'edAx_tipo3
    '
    Me.edAx_tipo3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_tipo3.Location = New System.Drawing.Point(239, 65)
    Me.edAx_tipo3.Name = "edAx_tipo3"
    Me.edAx_tipo3.NTSDbField = ""
    Me.edAx_tipo3.NTSForzaVisZoom = False
    Me.edAx_tipo3.NTSOldValue = ""
    Me.edAx_tipo3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_tipo3.Properties.MaxLength = 65536
    Me.edAx_tipo3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_tipo3.Size = New System.Drawing.Size(27, 20)
    Me.edAx_tipo3.TabIndex = 502
    '
    'edAx_desext1
    '
    Me.edAx_desext1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_desext1.Location = New System.Drawing.Point(239, 91)
    Me.edAx_desext1.Name = "edAx_desext1"
    Me.edAx_desext1.NTSDbField = ""
    Me.edAx_desext1.Properties.MaxLength = 65536
    Me.edAx_desext1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_desext1.Size = New System.Drawing.Size(416, 67)
    Me.edAx_desext1.TabIndex = 513
    '
    'NtsTabPage2
    '
    Me.NtsTabPage2.AllowDrop = True
    Me.NtsTabPage2.Controls.Add(Me.pnPage2)
    Me.NtsTabPage2.Enable = True
    Me.NtsTabPage2.Name = "NtsTabPage2"
    Me.NtsTabPage2.Size = New System.Drawing.Size(661, 350)
    Me.NtsTabPage2.Text = "&2 - Dati 2"
    '
    'pnPage2
    '
    Me.pnPage2.AllowDrop = True
    Me.pnPage2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPage2.Appearance.Options.UseBackColor = True
    Me.pnPage2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPage2.Controls.Add(Me.lbAx_descr1)
    Me.pnPage2.Controls.Add(Me.edAx_descr1)
    Me.pnPage2.Controls.Add(Me.lbAx_descr2)
    Me.pnPage2.Controls.Add(Me.edAx_descr2)
    Me.pnPage2.Controls.Add(Me.lbAx_descr3)
    Me.pnPage2.Controls.Add(Me.edAx_descr3)
    Me.pnPage2.Controls.Add(Me.lbAx_descr4)
    Me.pnPage2.Controls.Add(Me.edAx_descr4)
    Me.pnPage2.Controls.Add(Me.lbAx_descr5)
    Me.pnPage2.Controls.Add(Me.edAx_descr5)
    Me.pnPage2.Controls.Add(Me.lbAx_descr6)
    Me.pnPage2.Controls.Add(Me.edAx_descr6)
    Me.pnPage2.Controls.Add(Me.lbAx_descr7)
    Me.pnPage2.Controls.Add(Me.edAx_descr7)
    Me.pnPage2.Controls.Add(Me.lbAx_descr8)
    Me.pnPage2.Controls.Add(Me.edAx_descr8)
    Me.pnPage2.Controls.Add(Me.lbAx_descr9)
    Me.pnPage2.Controls.Add(Me.edAx_descr9)
    Me.pnPage2.Controls.Add(Me.lbAx_descr10)
    Me.pnPage2.Controls.Add(Me.edAx_descr10)
    Me.pnPage2.Controls.Add(Me.lbAx_data1)
    Me.pnPage2.Controls.Add(Me.edAx_data1)
    Me.pnPage2.Controls.Add(Me.lbAx_data2)
    Me.pnPage2.Controls.Add(Me.edAx_data2)
    Me.pnPage2.Controls.Add(Me.lbAx_data3)
    Me.pnPage2.Controls.Add(Me.edAx_data3)
    Me.pnPage2.Controls.Add(Me.lbAx_data4)
    Me.pnPage2.Controls.Add(Me.edAx_data4)
    Me.pnPage2.Controls.Add(Me.lbAx_data5)
    Me.pnPage2.Controls.Add(Me.edAx_data5)
    Me.pnPage2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPage2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPage2.Location = New System.Drawing.Point(0, 0)
    Me.pnPage2.Name = "pnPage2"
    Me.pnPage2.Size = New System.Drawing.Size(661, 350)
    Me.pnPage2.TabIndex = 546
    Me.pnPage2.Text = "NtsPanel5"
    '
    'lbAx_descr1
    '
    Me.lbAx_descr1.AutoSize = True
    Me.lbAx_descr1.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_descr1.Location = New System.Drawing.Point(10, 16)
    Me.lbAx_descr1.Name = "lbAx_descr1"
    Me.lbAx_descr1.NTSDbField = ""
    Me.lbAx_descr1.Size = New System.Drawing.Size(52, 13)
    Me.lbAx_descr1.TabIndex = 533
    Me.lbAx_descr1.Text = "Descr301"
    '
    'edAx_descr1
    '
    Me.edAx_descr1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_descr1.Location = New System.Drawing.Point(239, 13)
    Me.edAx_descr1.Name = "edAx_descr1"
    Me.edAx_descr1.NTSDbField = ""
    Me.edAx_descr1.NTSForzaVisZoom = False
    Me.edAx_descr1.NTSOldValue = ""
    Me.edAx_descr1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_descr1.Properties.MaxLength = 65536
    Me.edAx_descr1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_descr1.Size = New System.Drawing.Size(416, 20)
    Me.edAx_descr1.TabIndex = 543
    '
    'lbAx_descr2
    '
    Me.lbAx_descr2.AutoSize = True
    Me.lbAx_descr2.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_descr2.Location = New System.Drawing.Point(10, 42)
    Me.lbAx_descr2.Name = "lbAx_descr2"
    Me.lbAx_descr2.NTSDbField = ""
    Me.lbAx_descr2.Size = New System.Drawing.Size(52, 13)
    Me.lbAx_descr2.TabIndex = 534
    Me.lbAx_descr2.Text = "Descr302"
    '
    'edAx_descr2
    '
    Me.edAx_descr2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_descr2.Location = New System.Drawing.Point(239, 39)
    Me.edAx_descr2.Name = "edAx_descr2"
    Me.edAx_descr2.NTSDbField = ""
    Me.edAx_descr2.NTSForzaVisZoom = False
    Me.edAx_descr2.NTSOldValue = ""
    Me.edAx_descr2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_descr2.Properties.MaxLength = 65536
    Me.edAx_descr2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_descr2.Size = New System.Drawing.Size(416, 20)
    Me.edAx_descr2.TabIndex = 544
    '
    'lbAx_descr3
    '
    Me.lbAx_descr3.AutoSize = True
    Me.lbAx_descr3.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_descr3.Location = New System.Drawing.Point(10, 68)
    Me.lbAx_descr3.Name = "lbAx_descr3"
    Me.lbAx_descr3.NTSDbField = ""
    Me.lbAx_descr3.Size = New System.Drawing.Size(52, 13)
    Me.lbAx_descr3.TabIndex = 535
    Me.lbAx_descr3.Text = "Descr303"
    '
    'edAx_descr3
    '
    Me.edAx_descr3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_descr3.Location = New System.Drawing.Point(239, 65)
    Me.edAx_descr3.Name = "edAx_descr3"
    Me.edAx_descr3.NTSDbField = ""
    Me.edAx_descr3.NTSForzaVisZoom = False
    Me.edAx_descr3.NTSOldValue = ""
    Me.edAx_descr3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_descr3.Properties.MaxLength = 65536
    Me.edAx_descr3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_descr3.Size = New System.Drawing.Size(416, 20)
    Me.edAx_descr3.TabIndex = 545
    '
    'lbAx_descr4
    '
    Me.lbAx_descr4.AutoSize = True
    Me.lbAx_descr4.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_descr4.Location = New System.Drawing.Point(10, 94)
    Me.lbAx_descr4.Name = "lbAx_descr4"
    Me.lbAx_descr4.NTSDbField = ""
    Me.lbAx_descr4.Size = New System.Drawing.Size(52, 13)
    Me.lbAx_descr4.TabIndex = 536
    Me.lbAx_descr4.Text = "Descr304"
    '
    'edAx_descr4
    '
    Me.edAx_descr4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_descr4.Location = New System.Drawing.Point(239, 91)
    Me.edAx_descr4.Name = "edAx_descr4"
    Me.edAx_descr4.NTSDbField = ""
    Me.edAx_descr4.NTSForzaVisZoom = False
    Me.edAx_descr4.NTSOldValue = ""
    Me.edAx_descr4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_descr4.Properties.MaxLength = 65536
    Me.edAx_descr4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_descr4.Size = New System.Drawing.Size(416, 20)
    Me.edAx_descr4.TabIndex = 546
    '
    'lbAx_descr5
    '
    Me.lbAx_descr5.AutoSize = True
    Me.lbAx_descr5.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_descr5.Location = New System.Drawing.Point(10, 120)
    Me.lbAx_descr5.Name = "lbAx_descr5"
    Me.lbAx_descr5.NTSDbField = ""
    Me.lbAx_descr5.Size = New System.Drawing.Size(52, 13)
    Me.lbAx_descr5.TabIndex = 537
    Me.lbAx_descr5.Text = "Descr305"
    '
    'edAx_descr5
    '
    Me.edAx_descr5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_descr5.Location = New System.Drawing.Point(239, 117)
    Me.edAx_descr5.Name = "edAx_descr5"
    Me.edAx_descr5.NTSDbField = ""
    Me.edAx_descr5.NTSForzaVisZoom = False
    Me.edAx_descr5.NTSOldValue = ""
    Me.edAx_descr5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_descr5.Properties.MaxLength = 65536
    Me.edAx_descr5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_descr5.Size = New System.Drawing.Size(416, 20)
    Me.edAx_descr5.TabIndex = 547
    '
    'lbAx_descr6
    '
    Me.lbAx_descr6.AutoSize = True
    Me.lbAx_descr6.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_descr6.Location = New System.Drawing.Point(10, 146)
    Me.lbAx_descr6.Name = "lbAx_descr6"
    Me.lbAx_descr6.NTSDbField = ""
    Me.lbAx_descr6.Size = New System.Drawing.Size(52, 13)
    Me.lbAx_descr6.TabIndex = 538
    Me.lbAx_descr6.Text = "Descr306"
    '
    'edAx_descr6
    '
    Me.edAx_descr6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_descr6.Location = New System.Drawing.Point(239, 143)
    Me.edAx_descr6.Name = "edAx_descr6"
    Me.edAx_descr6.NTSDbField = ""
    Me.edAx_descr6.NTSForzaVisZoom = False
    Me.edAx_descr6.NTSOldValue = ""
    Me.edAx_descr6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_descr6.Properties.MaxLength = 65536
    Me.edAx_descr6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_descr6.Size = New System.Drawing.Size(416, 20)
    Me.edAx_descr6.TabIndex = 548
    '
    'lbAx_descr7
    '
    Me.lbAx_descr7.AutoSize = True
    Me.lbAx_descr7.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_descr7.Location = New System.Drawing.Point(10, 172)
    Me.lbAx_descr7.Name = "lbAx_descr7"
    Me.lbAx_descr7.NTSDbField = ""
    Me.lbAx_descr7.Size = New System.Drawing.Size(52, 13)
    Me.lbAx_descr7.TabIndex = 539
    Me.lbAx_descr7.Text = "Descr307"
    '
    'edAx_descr7
    '
    Me.edAx_descr7.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_descr7.Location = New System.Drawing.Point(239, 169)
    Me.edAx_descr7.Name = "edAx_descr7"
    Me.edAx_descr7.NTSDbField = ""
    Me.edAx_descr7.NTSForzaVisZoom = False
    Me.edAx_descr7.NTSOldValue = ""
    Me.edAx_descr7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_descr7.Properties.MaxLength = 65536
    Me.edAx_descr7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_descr7.Size = New System.Drawing.Size(416, 20)
    Me.edAx_descr7.TabIndex = 549
    '
    'lbAx_descr8
    '
    Me.lbAx_descr8.AutoSize = True
    Me.lbAx_descr8.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_descr8.Location = New System.Drawing.Point(10, 198)
    Me.lbAx_descr8.Name = "lbAx_descr8"
    Me.lbAx_descr8.NTSDbField = ""
    Me.lbAx_descr8.Size = New System.Drawing.Size(52, 13)
    Me.lbAx_descr8.TabIndex = 540
    Me.lbAx_descr8.Text = "Descr308"
    '
    'edAx_descr8
    '
    Me.edAx_descr8.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_descr8.Location = New System.Drawing.Point(239, 195)
    Me.edAx_descr8.Name = "edAx_descr8"
    Me.edAx_descr8.NTSDbField = ""
    Me.edAx_descr8.NTSForzaVisZoom = False
    Me.edAx_descr8.NTSOldValue = ""
    Me.edAx_descr8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_descr8.Properties.MaxLength = 65536
    Me.edAx_descr8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_descr8.Size = New System.Drawing.Size(416, 20)
    Me.edAx_descr8.TabIndex = 550
    '
    'lbAx_descr9
    '
    Me.lbAx_descr9.AutoSize = True
    Me.lbAx_descr9.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_descr9.Location = New System.Drawing.Point(10, 224)
    Me.lbAx_descr9.Name = "lbAx_descr9"
    Me.lbAx_descr9.NTSDbField = ""
    Me.lbAx_descr9.Size = New System.Drawing.Size(52, 13)
    Me.lbAx_descr9.TabIndex = 541
    Me.lbAx_descr9.Text = "Descr309"
    '
    'edAx_descr9
    '
    Me.edAx_descr9.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_descr9.Location = New System.Drawing.Point(239, 221)
    Me.edAx_descr9.Name = "edAx_descr9"
    Me.edAx_descr9.NTSDbField = ""
    Me.edAx_descr9.NTSForzaVisZoom = False
    Me.edAx_descr9.NTSOldValue = ""
    Me.edAx_descr9.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_descr9.Properties.MaxLength = 65536
    Me.edAx_descr9.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_descr9.Size = New System.Drawing.Size(416, 20)
    Me.edAx_descr9.TabIndex = 551
    '
    'lbAx_descr10
    '
    Me.lbAx_descr10.AutoSize = True
    Me.lbAx_descr10.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_descr10.Location = New System.Drawing.Point(10, 250)
    Me.lbAx_descr10.Name = "lbAx_descr10"
    Me.lbAx_descr10.NTSDbField = ""
    Me.lbAx_descr10.Size = New System.Drawing.Size(58, 13)
    Me.lbAx_descr10.TabIndex = 542
    Me.lbAx_descr10.Text = "Descr3010"
    '
    'edAx_descr10
    '
    Me.edAx_descr10.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_descr10.Location = New System.Drawing.Point(239, 247)
    Me.edAx_descr10.Name = "edAx_descr10"
    Me.edAx_descr10.NTSDbField = ""
    Me.edAx_descr10.NTSForzaVisZoom = False
    Me.edAx_descr10.NTSOldValue = ""
    Me.edAx_descr10.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_descr10.Properties.MaxLength = 65536
    Me.edAx_descr10.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_descr10.Size = New System.Drawing.Size(416, 20)
    Me.edAx_descr10.TabIndex = 552
    '
    'lbAx_data1
    '
    Me.lbAx_data1.AutoSize = True
    Me.lbAx_data1.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_data1.Location = New System.Drawing.Point(10, 276)
    Me.lbAx_data1.Name = "lbAx_data1"
    Me.lbAx_data1.NTSDbField = ""
    Me.lbAx_data1.Size = New System.Drawing.Size(36, 13)
    Me.lbAx_data1.TabIndex = 523
    Me.lbAx_data1.Text = "Data1"
    '
    'edAx_data1
    '
    Me.edAx_data1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_data1.EditValue = "01/01/1900"
    Me.edAx_data1.Location = New System.Drawing.Point(239, 273)
    Me.edAx_data1.Name = "edAx_data1"
    Me.edAx_data1.NTSDbField = ""
    Me.edAx_data1.NTSForzaVisZoom = False
    Me.edAx_data1.NTSOldValue = ""
    Me.edAx_data1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_data1.Properties.MaxLength = 65536
    Me.edAx_data1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_data1.Size = New System.Drawing.Size(100, 20)
    Me.edAx_data1.TabIndex = 528
    '
    'lbAx_data2
    '
    Me.lbAx_data2.AutoSize = True
    Me.lbAx_data2.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_data2.Location = New System.Drawing.Point(10, 302)
    Me.lbAx_data2.Name = "lbAx_data2"
    Me.lbAx_data2.NTSDbField = ""
    Me.lbAx_data2.Size = New System.Drawing.Size(36, 13)
    Me.lbAx_data2.TabIndex = 524
    Me.lbAx_data2.Text = "Data2"
    '
    'edAx_data2
    '
    Me.edAx_data2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_data2.EditValue = "01/01/1900"
    Me.edAx_data2.Location = New System.Drawing.Point(239, 299)
    Me.edAx_data2.Name = "edAx_data2"
    Me.edAx_data2.NTSDbField = ""
    Me.edAx_data2.NTSForzaVisZoom = False
    Me.edAx_data2.NTSOldValue = ""
    Me.edAx_data2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_data2.Properties.MaxLength = 65536
    Me.edAx_data2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_data2.Size = New System.Drawing.Size(100, 20)
    Me.edAx_data2.TabIndex = 529
    '
    'lbAx_data3
    '
    Me.lbAx_data3.AutoSize = True
    Me.lbAx_data3.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_data3.Location = New System.Drawing.Point(10, 328)
    Me.lbAx_data3.Name = "lbAx_data3"
    Me.lbAx_data3.NTSDbField = ""
    Me.lbAx_data3.Size = New System.Drawing.Size(36, 13)
    Me.lbAx_data3.TabIndex = 525
    Me.lbAx_data3.Text = "Data3"
    '
    'edAx_data3
    '
    Me.edAx_data3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_data3.EditValue = "01/01/1900"
    Me.edAx_data3.Location = New System.Drawing.Point(239, 325)
    Me.edAx_data3.Name = "edAx_data3"
    Me.edAx_data3.NTSDbField = ""
    Me.edAx_data3.NTSForzaVisZoom = False
    Me.edAx_data3.NTSOldValue = ""
    Me.edAx_data3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_data3.Properties.MaxLength = 65536
    Me.edAx_data3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_data3.Size = New System.Drawing.Size(100, 20)
    Me.edAx_data3.TabIndex = 530
    '
    'lbAx_data4
    '
    Me.lbAx_data4.AutoSize = True
    Me.lbAx_data4.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_data4.Location = New System.Drawing.Point(355, 276)
    Me.lbAx_data4.Name = "lbAx_data4"
    Me.lbAx_data4.NTSDbField = ""
    Me.lbAx_data4.Size = New System.Drawing.Size(36, 13)
    Me.lbAx_data4.TabIndex = 526
    Me.lbAx_data4.Text = "Data4"
    '
    'edAx_data4
    '
    Me.edAx_data4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_data4.EditValue = "01/01/1900"
    Me.edAx_data4.Location = New System.Drawing.Point(555, 273)
    Me.edAx_data4.Name = "edAx_data4"
    Me.edAx_data4.NTSDbField = ""
    Me.edAx_data4.NTSForzaVisZoom = False
    Me.edAx_data4.NTSOldValue = ""
    Me.edAx_data4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_data4.Properties.MaxLength = 65536
    Me.edAx_data4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_data4.Size = New System.Drawing.Size(100, 20)
    Me.edAx_data4.TabIndex = 531
    '
    'lbAx_data5
    '
    Me.lbAx_data5.AutoSize = True
    Me.lbAx_data5.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_data5.Location = New System.Drawing.Point(355, 302)
    Me.lbAx_data5.Name = "lbAx_data5"
    Me.lbAx_data5.NTSDbField = ""
    Me.lbAx_data5.Size = New System.Drawing.Size(36, 13)
    Me.lbAx_data5.TabIndex = 527
    Me.lbAx_data5.Text = "Data5"
    '
    'edAx_data5
    '
    Me.edAx_data5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_data5.EditValue = "01/01/1900"
    Me.edAx_data5.Location = New System.Drawing.Point(555, 299)
    Me.edAx_data5.Name = "edAx_data5"
    Me.edAx_data5.NTSDbField = ""
    Me.edAx_data5.NTSForzaVisZoom = False
    Me.edAx_data5.NTSOldValue = ""
    Me.edAx_data5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_data5.Properties.MaxLength = 65536
    Me.edAx_data5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_data5.Size = New System.Drawing.Size(100, 20)
    Me.edAx_data5.TabIndex = 532
    '
    'NtsTabPage3
    '
    Me.NtsTabPage3.AllowDrop = True
    Me.NtsTabPage3.Controls.Add(Me.pnPage3)
    Me.NtsTabPage3.Enable = True
    Me.NtsTabPage3.Name = "NtsTabPage3"
    Me.NtsTabPage3.Size = New System.Drawing.Size(661, 350)
    Me.NtsTabPage3.Text = "&3 - Memo 1"
    '
    'pnPage3
    '
    Me.pnPage3.AllowDrop = True
    Me.pnPage3.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPage3.Appearance.Options.UseBackColor = True
    Me.pnPage3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPage3.Controls.Add(Me.lbAx_memo1)
    Me.pnPage3.Controls.Add(Me.edAx_memo1)
    Me.pnPage3.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPage3.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPage3.Location = New System.Drawing.Point(0, 0)
    Me.pnPage3.Name = "pnPage3"
    Me.pnPage3.Size = New System.Drawing.Size(661, 350)
    Me.pnPage3.TabIndex = 1
    Me.pnPage3.Text = "NtsPanel4"
    '
    'lbAx_memo1
    '
    Me.lbAx_memo1.AutoSize = True
    Me.lbAx_memo1.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_memo1.Location = New System.Drawing.Point(10, 16)
    Me.lbAx_memo1.Name = "lbAx_memo1"
    Me.lbAx_memo1.NTSDbField = ""
    Me.lbAx_memo1.Size = New System.Drawing.Size(41, 13)
    Me.lbAx_memo1.TabIndex = 517
    Me.lbAx_memo1.Text = "Memo1"
    '
    'edAx_memo1
    '
    Me.edAx_memo1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edAx_memo1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_memo1.Location = New System.Drawing.Point(3, 32)
    Me.edAx_memo1.Name = "edAx_memo1"
    Me.edAx_memo1.NTSDbField = ""
    Me.edAx_memo1.Properties.MaxLength = 65536
    Me.edAx_memo1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_memo1.Size = New System.Drawing.Size(655, 318)
    Me.edAx_memo1.TabIndex = 518
    '
    'NtsTabPage4
    '
    Me.NtsTabPage4.AllowDrop = True
    Me.NtsTabPage4.Controls.Add(Me.pnPage4)
    Me.NtsTabPage4.Enable = True
    Me.NtsTabPage4.Name = "NtsTabPage4"
    Me.NtsTabPage4.Size = New System.Drawing.Size(661, 350)
    Me.NtsTabPage4.Text = "&4 - Memo 2"
    '
    'pnPage4
    '
    Me.pnPage4.AllowDrop = True
    Me.pnPage4.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPage4.Appearance.Options.UseBackColor = True
    Me.pnPage4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPage4.Controls.Add(Me.lbAx_memo2)
    Me.pnPage4.Controls.Add(Me.edAx_memo2)
    Me.pnPage4.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPage4.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPage4.Location = New System.Drawing.Point(0, 0)
    Me.pnPage4.Name = "pnPage4"
    Me.pnPage4.Size = New System.Drawing.Size(661, 350)
    Me.pnPage4.TabIndex = 1
    Me.pnPage4.Text = "NtsPanel3"
    '
    'lbAx_memo2
    '
    Me.lbAx_memo2.AutoSize = True
    Me.lbAx_memo2.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_memo2.Location = New System.Drawing.Point(10, 16)
    Me.lbAx_memo2.Name = "lbAx_memo2"
    Me.lbAx_memo2.NTSDbField = ""
    Me.lbAx_memo2.Size = New System.Drawing.Size(41, 13)
    Me.lbAx_memo2.TabIndex = 518
    Me.lbAx_memo2.Text = "Memo2"
    '
    'edAx_memo2
    '
    Me.edAx_memo2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edAx_memo2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_memo2.Location = New System.Drawing.Point(3, 32)
    Me.edAx_memo2.Name = "edAx_memo2"
    Me.edAx_memo2.NTSDbField = ""
    Me.edAx_memo2.Properties.MaxLength = 65536
    Me.edAx_memo2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_memo2.Size = New System.Drawing.Size(655, 318)
    Me.edAx_memo2.TabIndex = 519
    '
    'NtsTabPage5
    '
    Me.NtsTabPage5.AllowDrop = True
    Me.NtsTabPage5.Controls.Add(Me.pnPage5)
    Me.NtsTabPage5.Enable = True
    Me.NtsTabPage5.Name = "NtsTabPage5"
    Me.NtsTabPage5.Size = New System.Drawing.Size(661, 350)
    Me.NtsTabPage5.Text = "&5 - Valori"
    '
    'pnPage5
    '
    Me.pnPage5.AllowDrop = True
    Me.pnPage5.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPage5.Appearance.Options.UseBackColor = True
    Me.pnPage5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPage5.Controls.Add(Me.lbAx_num1)
    Me.pnPage5.Controls.Add(Me.edAx_num1)
    Me.pnPage5.Controls.Add(Me.lbAx_num2)
    Me.pnPage5.Controls.Add(Me.edAx_num2)
    Me.pnPage5.Controls.Add(Me.lbAx_num3)
    Me.pnPage5.Controls.Add(Me.edAx_num3)
    Me.pnPage5.Controls.Add(Me.lbAx_num4)
    Me.pnPage5.Controls.Add(Me.edAx_num4)
    Me.pnPage5.Controls.Add(Me.lbAx_num5)
    Me.pnPage5.Controls.Add(Me.edAx_num5)
    Me.pnPage5.Controls.Add(Me.lbAx_num6)
    Me.pnPage5.Controls.Add(Me.edAx_num6)
    Me.pnPage5.Controls.Add(Me.lbAx_num7)
    Me.pnPage5.Controls.Add(Me.edAx_num7)
    Me.pnPage5.Controls.Add(Me.lbAx_num8)
    Me.pnPage5.Controls.Add(Me.edAx_num8)
    Me.pnPage5.Controls.Add(Me.lbAx_num9)
    Me.pnPage5.Controls.Add(Me.edAx_num9)
    Me.pnPage5.Controls.Add(Me.lbAx_num10)
    Me.pnPage5.Controls.Add(Me.edAx_num10)
    Me.pnPage5.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPage5.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPage5.Location = New System.Drawing.Point(0, 0)
    Me.pnPage5.Name = "pnPage5"
    Me.pnPage5.Size = New System.Drawing.Size(661, 350)
    Me.pnPage5.TabIndex = 1
    Me.pnPage5.Text = "NtsPanel2"
    '
    'lbAx_num1
    '
    Me.lbAx_num1.AutoSize = True
    Me.lbAx_num1.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_num1.Location = New System.Drawing.Point(10, 16)
    Me.lbAx_num1.Name = "lbAx_num1"
    Me.lbAx_num1.NTSDbField = ""
    Me.lbAx_num1.Size = New System.Drawing.Size(34, 13)
    Me.lbAx_num1.TabIndex = 547
    Me.lbAx_num1.Text = "Num1"
    '
    'edAx_num1
    '
    Me.edAx_num1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_num1.EditValue = "0"
    Me.edAx_num1.Location = New System.Drawing.Point(239, 13)
    Me.edAx_num1.Name = "edAx_num1"
    Me.edAx_num1.NTSDbField = ""
    Me.edAx_num1.NTSFormat = "0"
    Me.edAx_num1.NTSForzaVisZoom = False
    Me.edAx_num1.NTSOldValue = ""
    Me.edAx_num1.Properties.Appearance.Options.UseTextOptions = True
    Me.edAx_num1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAx_num1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_num1.Properties.MaxLength = 65536
    Me.edAx_num1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_num1.Size = New System.Drawing.Size(100, 20)
    Me.edAx_num1.TabIndex = 550
    '
    'lbAx_num2
    '
    Me.lbAx_num2.AutoSize = True
    Me.lbAx_num2.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_num2.Location = New System.Drawing.Point(10, 42)
    Me.lbAx_num2.Name = "lbAx_num2"
    Me.lbAx_num2.NTSDbField = ""
    Me.lbAx_num2.Size = New System.Drawing.Size(34, 13)
    Me.lbAx_num2.TabIndex = 548
    Me.lbAx_num2.Text = "Num2"
    '
    'edAx_num2
    '
    Me.edAx_num2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_num2.EditValue = "0"
    Me.edAx_num2.Location = New System.Drawing.Point(239, 39)
    Me.edAx_num2.Name = "edAx_num2"
    Me.edAx_num2.NTSDbField = ""
    Me.edAx_num2.NTSFormat = "0"
    Me.edAx_num2.NTSForzaVisZoom = False
    Me.edAx_num2.NTSOldValue = ""
    Me.edAx_num2.Properties.Appearance.Options.UseTextOptions = True
    Me.edAx_num2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAx_num2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_num2.Properties.MaxLength = 65536
    Me.edAx_num2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_num2.Size = New System.Drawing.Size(100, 20)
    Me.edAx_num2.TabIndex = 551
    '
    'lbAx_num3
    '
    Me.lbAx_num3.AutoSize = True
    Me.lbAx_num3.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_num3.Location = New System.Drawing.Point(10, 68)
    Me.lbAx_num3.Name = "lbAx_num3"
    Me.lbAx_num3.NTSDbField = ""
    Me.lbAx_num3.Size = New System.Drawing.Size(34, 13)
    Me.lbAx_num3.TabIndex = 549
    Me.lbAx_num3.Text = "Num3"
    '
    'edAx_num3
    '
    Me.edAx_num3.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAx_num3.EditValue = "0"
    Me.edAx_num3.Location = New System.Drawing.Point(239, 65)
    Me.edAx_num3.Name = "edAx_num3"
    Me.edAx_num3.NTSDbField = ""
    Me.edAx_num3.NTSFormat = "0"
    Me.edAx_num3.NTSForzaVisZoom = False
    Me.edAx_num3.NTSOldValue = ""
    Me.edAx_num3.Properties.Appearance.Options.UseTextOptions = True
    Me.edAx_num3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAx_num3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_num3.Properties.MaxLength = 65536
    Me.edAx_num3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_num3.Size = New System.Drawing.Size(100, 20)
    Me.edAx_num3.TabIndex = 552
    '
    'lbAx_num4
    '
    Me.lbAx_num4.AutoSize = True
    Me.lbAx_num4.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_num4.Location = New System.Drawing.Point(10, 94)
    Me.lbAx_num4.Name = "lbAx_num4"
    Me.lbAx_num4.NTSDbField = ""
    Me.lbAx_num4.Size = New System.Drawing.Size(34, 13)
    Me.lbAx_num4.TabIndex = 533
    Me.lbAx_num4.Text = "Num4"
    '
    'edAx_num4
    '
    Me.edAx_num4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_num4.EditValue = "0"
    Me.edAx_num4.Location = New System.Drawing.Point(239, 91)
    Me.edAx_num4.Name = "edAx_num4"
    Me.edAx_num4.NTSDbField = ""
    Me.edAx_num4.NTSFormat = "0"
    Me.edAx_num4.NTSForzaVisZoom = False
    Me.edAx_num4.NTSOldValue = ""
    Me.edAx_num4.Properties.Appearance.Options.UseTextOptions = True
    Me.edAx_num4.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAx_num4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_num4.Properties.MaxLength = 65536
    Me.edAx_num4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_num4.Size = New System.Drawing.Size(100, 20)
    Me.edAx_num4.TabIndex = 540
    '
    'lbAx_num5
    '
    Me.lbAx_num5.AutoSize = True
    Me.lbAx_num5.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_num5.Location = New System.Drawing.Point(10, 119)
    Me.lbAx_num5.Name = "lbAx_num5"
    Me.lbAx_num5.NTSDbField = ""
    Me.lbAx_num5.Size = New System.Drawing.Size(34, 13)
    Me.lbAx_num5.TabIndex = 534
    Me.lbAx_num5.Text = "Num5"
    '
    'edAx_num5
    '
    Me.edAx_num5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_num5.EditValue = "0"
    Me.edAx_num5.Location = New System.Drawing.Point(239, 116)
    Me.edAx_num5.Name = "edAx_num5"
    Me.edAx_num5.NTSDbField = ""
    Me.edAx_num5.NTSFormat = "0"
    Me.edAx_num5.NTSForzaVisZoom = False
    Me.edAx_num5.NTSOldValue = ""
    Me.edAx_num5.Properties.Appearance.Options.UseTextOptions = True
    Me.edAx_num5.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAx_num5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_num5.Properties.MaxLength = 65536
    Me.edAx_num5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_num5.Size = New System.Drawing.Size(100, 20)
    Me.edAx_num5.TabIndex = 541
    '
    'lbAx_num6
    '
    Me.lbAx_num6.AutoSize = True
    Me.lbAx_num6.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_num6.Location = New System.Drawing.Point(10, 145)
    Me.lbAx_num6.Name = "lbAx_num6"
    Me.lbAx_num6.NTSDbField = ""
    Me.lbAx_num6.Size = New System.Drawing.Size(34, 13)
    Me.lbAx_num6.TabIndex = 535
    Me.lbAx_num6.Text = "Num6"
    '
    'edAx_num6
    '
    Me.edAx_num6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_num6.EditValue = "0"
    Me.edAx_num6.Location = New System.Drawing.Point(239, 142)
    Me.edAx_num6.Name = "edAx_num6"
    Me.edAx_num6.NTSDbField = ""
    Me.edAx_num6.NTSFormat = "0"
    Me.edAx_num6.NTSForzaVisZoom = False
    Me.edAx_num6.NTSOldValue = ""
    Me.edAx_num6.Properties.Appearance.Options.UseTextOptions = True
    Me.edAx_num6.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAx_num6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_num6.Properties.MaxLength = 65536
    Me.edAx_num6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_num6.Size = New System.Drawing.Size(100, 20)
    Me.edAx_num6.TabIndex = 542
    '
    'lbAx_num7
    '
    Me.lbAx_num7.AutoSize = True
    Me.lbAx_num7.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_num7.Location = New System.Drawing.Point(10, 171)
    Me.lbAx_num7.Name = "lbAx_num7"
    Me.lbAx_num7.NTSDbField = ""
    Me.lbAx_num7.Size = New System.Drawing.Size(34, 13)
    Me.lbAx_num7.TabIndex = 536
    Me.lbAx_num7.Text = "Num7"
    '
    'edAx_num7
    '
    Me.edAx_num7.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_num7.EditValue = "0"
    Me.edAx_num7.Location = New System.Drawing.Point(239, 168)
    Me.edAx_num7.Name = "edAx_num7"
    Me.edAx_num7.NTSDbField = ""
    Me.edAx_num7.NTSFormat = "0"
    Me.edAx_num7.NTSForzaVisZoom = False
    Me.edAx_num7.NTSOldValue = ""
    Me.edAx_num7.Properties.Appearance.Options.UseTextOptions = True
    Me.edAx_num7.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAx_num7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_num7.Properties.MaxLength = 65536
    Me.edAx_num7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_num7.Size = New System.Drawing.Size(100, 20)
    Me.edAx_num7.TabIndex = 543
    '
    'lbAx_num8
    '
    Me.lbAx_num8.AutoSize = True
    Me.lbAx_num8.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_num8.Location = New System.Drawing.Point(10, 197)
    Me.lbAx_num8.Name = "lbAx_num8"
    Me.lbAx_num8.NTSDbField = ""
    Me.lbAx_num8.Size = New System.Drawing.Size(34, 13)
    Me.lbAx_num8.TabIndex = 537
    Me.lbAx_num8.Text = "Num8"
    '
    'edAx_num8
    '
    Me.edAx_num8.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_num8.EditValue = "0"
    Me.edAx_num8.Location = New System.Drawing.Point(239, 194)
    Me.edAx_num8.Name = "edAx_num8"
    Me.edAx_num8.NTSDbField = ""
    Me.edAx_num8.NTSFormat = "0"
    Me.edAx_num8.NTSForzaVisZoom = False
    Me.edAx_num8.NTSOldValue = ""
    Me.edAx_num8.Properties.Appearance.Options.UseTextOptions = True
    Me.edAx_num8.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAx_num8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_num8.Properties.MaxLength = 65536
    Me.edAx_num8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_num8.Size = New System.Drawing.Size(100, 20)
    Me.edAx_num8.TabIndex = 544
    '
    'lbAx_num9
    '
    Me.lbAx_num9.AutoSize = True
    Me.lbAx_num9.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_num9.Location = New System.Drawing.Point(10, 223)
    Me.lbAx_num9.Name = "lbAx_num9"
    Me.lbAx_num9.NTSDbField = ""
    Me.lbAx_num9.Size = New System.Drawing.Size(34, 13)
    Me.lbAx_num9.TabIndex = 538
    Me.lbAx_num9.Text = "Num9"
    '
    'edAx_num9
    '
    Me.edAx_num9.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAx_num9.EditValue = "0"
    Me.edAx_num9.Location = New System.Drawing.Point(239, 220)
    Me.edAx_num9.Name = "edAx_num9"
    Me.edAx_num9.NTSDbField = ""
    Me.edAx_num9.NTSFormat = "0"
    Me.edAx_num9.NTSForzaVisZoom = False
    Me.edAx_num9.NTSOldValue = ""
    Me.edAx_num9.Properties.Appearance.Options.UseTextOptions = True
    Me.edAx_num9.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAx_num9.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_num9.Properties.MaxLength = 65536
    Me.edAx_num9.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_num9.Size = New System.Drawing.Size(100, 20)
    Me.edAx_num9.TabIndex = 545
    '
    'lbAx_num10
    '
    Me.lbAx_num10.AutoSize = True
    Me.lbAx_num10.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_num10.Location = New System.Drawing.Point(10, 249)
    Me.lbAx_num10.Name = "lbAx_num10"
    Me.lbAx_num10.NTSDbField = ""
    Me.lbAx_num10.Size = New System.Drawing.Size(40, 13)
    Me.lbAx_num10.TabIndex = 539
    Me.lbAx_num10.Text = "Num10"
    '
    'edAx_num10
    '
    Me.edAx_num10.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAx_num10.EditValue = "0"
    Me.edAx_num10.Location = New System.Drawing.Point(239, 246)
    Me.edAx_num10.Name = "edAx_num10"
    Me.edAx_num10.NTSDbField = ""
    Me.edAx_num10.NTSFormat = "0"
    Me.edAx_num10.NTSForzaVisZoom = False
    Me.edAx_num10.NTSOldValue = ""
    Me.edAx_num10.Properties.Appearance.Options.UseTextOptions = True
    Me.edAx_num10.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAx_num10.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAx_num10.Properties.MaxLength = 65536
    Me.edAx_num10.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAx_num10.Size = New System.Drawing.Size(100, 20)
    Me.edAx_num10.TabIndex = 546
    '
    'NtsTabPage6
    '
    Me.NtsTabPage6.AllowDrop = True
    Me.NtsTabPage6.Controls.Add(Me.pnPage6)
    Me.NtsTabPage6.Enable = True
    Me.NtsTabPage6.Name = "NtsTabPage6"
    Me.NtsTabPage6.Size = New System.Drawing.Size(661, 350)
    Me.NtsTabPage6.Text = "&6 - Combo / Check"
    '
    'pnPage6
    '
    Me.pnPage6.AllowDrop = True
    Me.pnPage6.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPage6.Appearance.Options.UseBackColor = True
    Me.pnPage6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPage6.Controls.Add(Me.ckAx_check1)
    Me.pnPage6.Controls.Add(Me.ckAx_check3)
    Me.pnPage6.Controls.Add(Me.ckAx_check5)
    Me.pnPage6.Controls.Add(Me.ckAx_check7)
    Me.pnPage6.Controls.Add(Me.ckAx_check9)
    Me.pnPage6.Controls.Add(Me.ckAx_check2)
    Me.pnPage6.Controls.Add(Me.ckAx_check4)
    Me.pnPage6.Controls.Add(Me.ckAx_check6)
    Me.pnPage6.Controls.Add(Me.ckAx_check8)
    Me.pnPage6.Controls.Add(Me.ckAx_check10)
    Me.pnPage6.Controls.Add(Me.lbAx_combo1)
    Me.pnPage6.Controls.Add(Me.cbAx_combo1)
    Me.pnPage6.Controls.Add(Me.lbAx_combo2)
    Me.pnPage6.Controls.Add(Me.cbAx_combo2)
    Me.pnPage6.Controls.Add(Me.lbAx_combo3)
    Me.pnPage6.Controls.Add(Me.cbAx_combo3)
    Me.pnPage6.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPage6.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPage6.Location = New System.Drawing.Point(0, 0)
    Me.pnPage6.Name = "pnPage6"
    Me.pnPage6.Size = New System.Drawing.Size(661, 350)
    Me.pnPage6.TabIndex = 0
    Me.pnPage6.Text = "NtsPanel1"
    '
    'ckAx_check1
    '
    Me.ckAx_check1.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAx_check1.Location = New System.Drawing.Point(13, 91)
    Me.ckAx_check1.Name = "ckAx_check1"
    Me.ckAx_check1.NTSCheckValue = "S"
    Me.ckAx_check1.NTSUnCheckValue = "N"
    Me.ckAx_check1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAx_check1.Properties.Appearance.Options.UseBackColor = True
    Me.ckAx_check1.Properties.Caption = "Check1"
    Me.ckAx_check1.Size = New System.Drawing.Size(313, 19)
    Me.ckAx_check1.TabIndex = 557
    '
    'ckAx_check3
    '
    Me.ckAx_check3.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAx_check3.Location = New System.Drawing.Point(13, 138)
    Me.ckAx_check3.Name = "ckAx_check3"
    Me.ckAx_check3.NTSCheckValue = "S"
    Me.ckAx_check3.NTSUnCheckValue = "N"
    Me.ckAx_check3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAx_check3.Properties.Appearance.Options.UseBackColor = True
    Me.ckAx_check3.Properties.Caption = "Check3"
    Me.ckAx_check3.Size = New System.Drawing.Size(313, 19)
    Me.ckAx_check3.TabIndex = 558
    '
    'ckAx_check5
    '
    Me.ckAx_check5.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAx_check5.Location = New System.Drawing.Point(13, 188)
    Me.ckAx_check5.Name = "ckAx_check5"
    Me.ckAx_check5.NTSCheckValue = "S"
    Me.ckAx_check5.NTSUnCheckValue = "N"
    Me.ckAx_check5.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAx_check5.Properties.Appearance.Options.UseBackColor = True
    Me.ckAx_check5.Properties.Caption = "Check5"
    Me.ckAx_check5.Size = New System.Drawing.Size(313, 19)
    Me.ckAx_check5.TabIndex = 559
    '
    'ckAx_check7
    '
    Me.ckAx_check7.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAx_check7.Location = New System.Drawing.Point(13, 238)
    Me.ckAx_check7.Name = "ckAx_check7"
    Me.ckAx_check7.NTSCheckValue = "S"
    Me.ckAx_check7.NTSUnCheckValue = "N"
    Me.ckAx_check7.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAx_check7.Properties.Appearance.Options.UseBackColor = True
    Me.ckAx_check7.Properties.Caption = "Check7"
    Me.ckAx_check7.Size = New System.Drawing.Size(313, 19)
    Me.ckAx_check7.TabIndex = 560
    '
    'ckAx_check9
    '
    Me.ckAx_check9.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAx_check9.Location = New System.Drawing.Point(13, 288)
    Me.ckAx_check9.Name = "ckAx_check9"
    Me.ckAx_check9.NTSCheckValue = "S"
    Me.ckAx_check9.NTSUnCheckValue = "N"
    Me.ckAx_check9.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAx_check9.Properties.Appearance.Options.UseBackColor = True
    Me.ckAx_check9.Properties.Caption = "Check9"
    Me.ckAx_check9.Size = New System.Drawing.Size(313, 19)
    Me.ckAx_check9.TabIndex = 561
    '
    'ckAx_check2
    '
    Me.ckAx_check2.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAx_check2.Location = New System.Drawing.Point(13, 116)
    Me.ckAx_check2.Name = "ckAx_check2"
    Me.ckAx_check2.NTSCheckValue = "S"
    Me.ckAx_check2.NTSUnCheckValue = "N"
    Me.ckAx_check2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAx_check2.Properties.Appearance.Options.UseBackColor = True
    Me.ckAx_check2.Properties.Caption = "Check2"
    Me.ckAx_check2.Size = New System.Drawing.Size(313, 19)
    Me.ckAx_check2.TabIndex = 552
    '
    'ckAx_check4
    '
    Me.ckAx_check4.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAx_check4.Location = New System.Drawing.Point(13, 163)
    Me.ckAx_check4.Name = "ckAx_check4"
    Me.ckAx_check4.NTSCheckValue = "S"
    Me.ckAx_check4.NTSUnCheckValue = "N"
    Me.ckAx_check4.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAx_check4.Properties.Appearance.Options.UseBackColor = True
    Me.ckAx_check4.Properties.Caption = "Check4"
    Me.ckAx_check4.Size = New System.Drawing.Size(313, 19)
    Me.ckAx_check4.TabIndex = 553
    '
    'ckAx_check6
    '
    Me.ckAx_check6.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAx_check6.Location = New System.Drawing.Point(13, 213)
    Me.ckAx_check6.Name = "ckAx_check6"
    Me.ckAx_check6.NTSCheckValue = "S"
    Me.ckAx_check6.NTSUnCheckValue = "N"
    Me.ckAx_check6.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAx_check6.Properties.Appearance.Options.UseBackColor = True
    Me.ckAx_check6.Properties.Caption = "Check6"
    Me.ckAx_check6.Size = New System.Drawing.Size(313, 19)
    Me.ckAx_check6.TabIndex = 554
    '
    'ckAx_check8
    '
    Me.ckAx_check8.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAx_check8.Location = New System.Drawing.Point(13, 263)
    Me.ckAx_check8.Name = "ckAx_check8"
    Me.ckAx_check8.NTSCheckValue = "S"
    Me.ckAx_check8.NTSUnCheckValue = "N"
    Me.ckAx_check8.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAx_check8.Properties.Appearance.Options.UseBackColor = True
    Me.ckAx_check8.Properties.Caption = "Check8"
    Me.ckAx_check8.Size = New System.Drawing.Size(313, 19)
    Me.ckAx_check8.TabIndex = 555
    '
    'ckAx_check10
    '
    Me.ckAx_check10.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAx_check10.Location = New System.Drawing.Point(13, 313)
    Me.ckAx_check10.Name = "ckAx_check10"
    Me.ckAx_check10.NTSCheckValue = "S"
    Me.ckAx_check10.NTSUnCheckValue = "N"
    Me.ckAx_check10.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAx_check10.Properties.Appearance.Options.UseBackColor = True
    Me.ckAx_check10.Properties.Caption = "Check10"
    Me.ckAx_check10.Size = New System.Drawing.Size(313, 19)
    Me.ckAx_check10.TabIndex = 556
    '
    'lbAx_combo1
    '
    Me.lbAx_combo1.AutoSize = True
    Me.lbAx_combo1.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_combo1.Location = New System.Drawing.Point(10, 16)
    Me.lbAx_combo1.Name = "lbAx_combo1"
    Me.lbAx_combo1.NTSDbField = ""
    Me.lbAx_combo1.Size = New System.Drawing.Size(46, 13)
    Me.lbAx_combo1.TabIndex = 546
    Me.lbAx_combo1.Text = "Combo1"
    '
    'cbAx_combo1
    '
    Me.cbAx_combo1.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAx_combo1.DataSource = Nothing
    Me.cbAx_combo1.DisplayMember = ""
    Me.cbAx_combo1.Location = New System.Drawing.Point(239, 13)
    Me.cbAx_combo1.Name = "cbAx_combo1"
    Me.cbAx_combo1.NTSDbField = ""
    Me.cbAx_combo1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAx_combo1.Properties.DropDownRows = 30
    Me.cbAx_combo1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAx_combo1.SelectedValue = ""
    Me.cbAx_combo1.Size = New System.Drawing.Size(252, 20)
    Me.cbAx_combo1.TabIndex = 549
    Me.cbAx_combo1.ValueMember = ""
    '
    'lbAx_combo2
    '
    Me.lbAx_combo2.AutoSize = True
    Me.lbAx_combo2.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_combo2.Location = New System.Drawing.Point(10, 42)
    Me.lbAx_combo2.Name = "lbAx_combo2"
    Me.lbAx_combo2.NTSDbField = ""
    Me.lbAx_combo2.Size = New System.Drawing.Size(46, 13)
    Me.lbAx_combo2.TabIndex = 547
    Me.lbAx_combo2.Text = "Combo2"
    '
    'cbAx_combo2
    '
    Me.cbAx_combo2.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAx_combo2.DataSource = Nothing
    Me.cbAx_combo2.DisplayMember = ""
    Me.cbAx_combo2.Location = New System.Drawing.Point(239, 39)
    Me.cbAx_combo2.Name = "cbAx_combo2"
    Me.cbAx_combo2.NTSDbField = ""
    Me.cbAx_combo2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAx_combo2.Properties.DropDownRows = 30
    Me.cbAx_combo2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAx_combo2.SelectedValue = ""
    Me.cbAx_combo2.Size = New System.Drawing.Size(252, 20)
    Me.cbAx_combo2.TabIndex = 550
    Me.cbAx_combo2.ValueMember = ""
    '
    'lbAx_combo3
    '
    Me.lbAx_combo3.AutoSize = True
    Me.lbAx_combo3.BackColor = System.Drawing.Color.Transparent
    Me.lbAx_combo3.Location = New System.Drawing.Point(10, 68)
    Me.lbAx_combo3.Name = "lbAx_combo3"
    Me.lbAx_combo3.NTSDbField = ""
    Me.lbAx_combo3.Size = New System.Drawing.Size(46, 13)
    Me.lbAx_combo3.TabIndex = 548
    Me.lbAx_combo3.Text = "Combo3"
    '
    'cbAx_combo3
    '
    Me.cbAx_combo3.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAx_combo3.DataSource = Nothing
    Me.cbAx_combo3.DisplayMember = ""
    Me.cbAx_combo3.Location = New System.Drawing.Point(239, 65)
    Me.cbAx_combo3.Name = "cbAx_combo3"
    Me.cbAx_combo3.NTSDbField = ""
    Me.cbAx_combo3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAx_combo3.Properties.DropDownRows = 30
    Me.cbAx_combo3.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAx_combo3.SelectedValue = ""
    Me.cbAx_combo3.Size = New System.Drawing.Size(252, 20)
    Me.cbAx_combo3.TabIndex = 551
    Me.cbAx_combo3.ValueMember = ""
    '
    'FRM__ANEX
    '
    Me.ClientSize = New System.Drawing.Size(670, 406)
    Me.Controls.Add(Me.tsAnex)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.HelpContext = "*"
    Me.MinimizeBox = False
    Me.Name = "FRM__ANEX"
    Me.Text = "ESTENSIONI ANAGRAFICHE"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tsAnex, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsAnex.ResumeLayout(False)
    Me.NtsTabPage1.ResumeLayout(False)
    CType(Me.pnPage1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPage1.ResumeLayout(False)
    Me.pnPage1.PerformLayout()
    CType(Me.edAx_tipo1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_desext3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_tipo2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_desext2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_tipo3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_desext1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage2.ResumeLayout(False)
    CType(Me.pnPage2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPage2.ResumeLayout(False)
    Me.pnPage2.PerformLayout()
    CType(Me.edAx_descr1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_descr2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_descr3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_descr4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_descr5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_descr6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_descr7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_descr8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_descr9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_descr10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_data1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_data2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_data3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_data4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_data5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage3.ResumeLayout(False)
    CType(Me.pnPage3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPage3.ResumeLayout(False)
    Me.pnPage3.PerformLayout()
    CType(Me.edAx_memo1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage4.ResumeLayout(False)
    CType(Me.pnPage4, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPage4.ResumeLayout(False)
    Me.pnPage4.PerformLayout()
    CType(Me.edAx_memo2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage5.ResumeLayout(False)
    CType(Me.pnPage5, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPage5.ResumeLayout(False)
    Me.pnPage5.PerformLayout()
    CType(Me.edAx_num1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_num2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_num3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_num4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_num5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_num6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_num7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_num8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_num9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAx_num10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage6.ResumeLayout(False)
    CType(Me.pnPage6, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPage6.ResumeLayout(False)
    Me.pnPage6.PerformLayout()
    CType(Me.ckAx_check1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAx_check3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAx_check5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAx_check7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAx_check9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAx_check2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAx_check4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAx_check6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAx_check8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAx_check10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAx_combo1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAx_combo2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAx_combo3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__ANEX", "BE__ANEX", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128382165714640775, "ERRORE in fase di creazione Entity:" & vbCrLf & strErr))
      Return False
    End If
    oCleAnex = CType(oTmp, CLE__ANEX)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__ANEX", strRemoteServer, strRemotePort)
    AddHandler oCleAnex.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleAnex.Init(oApp, NTSScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim i As Integer = 0
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'Ã¨ una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      '-------------------------------------------------
      'completo le informazioni dei controlli
      With oCleAnex.dttTabaExt.Rows(0)
        edAx_tipo1.NTSSetParam(oMenu, NTSCStr(!tb_tipo1), 1, False)
        edAx_tipo2.NTSSetParam(oMenu, NTSCStr(!tb_tipo2), 1, False)
        edAx_tipo3.NTSSetParam(oMenu, NTSCStr(!tb_tipo3), 1, False)
        edAx_descr1.NTSSetParam(oMenu, NTSCStr(!tb_descr1), 30, False)
        edAx_descr2.NTSSetParam(oMenu, NTSCStr(!tb_descr2), 30, False)
        edAx_descr3.NTSSetParam(oMenu, NTSCStr(!tb_descr3), 30, False)
        edAx_descr4.NTSSetParam(oMenu, NTSCStr(!tb_descr4), 30, False)
        edAx_descr5.NTSSetParam(oMenu, NTSCStr(!tb_descr5), 30, False)
        edAx_descr6.NTSSetParam(oMenu, NTSCStr(!tb_descr6), 30, False)
        edAx_descr7.NTSSetParam(oMenu, NTSCStr(!tb_descr7), 30, False)
        edAx_descr8.NTSSetParam(oMenu, NTSCStr(!tb_descr8), 30, False)
        edAx_descr9.NTSSetParam(oMenu, NTSCStr(!tb_descr9), 30, False)
        edAx_descr10.NTSSetParam(oMenu, NTSCStr(!tb_descr10), 30, False)
        edAx_desext1.NTSSetParam(oMenu, NTSCStr(!tb_desext1), 255, False)
        edAx_desext2.NTSSetParam(oMenu, NTSCStr(!tb_desext2), 255, False)
        edAx_desext3.NTSSetParam(oMenu, NTSCStr(!tb_desext3), 255, False)
        edAx_memo1.NTSSetParam(oMenu, NTSCStr(!tb_memo1), 0, True)
        edAx_memo2.NTSSetParam(oMenu, NTSCStr(!tb_memo2), 0, True)
        edAx_data1.NTSSetParam(oMenu, NTSCStr(!tb_data1), True, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
        edAx_data2.NTSSetParam(oMenu, NTSCStr(!tb_data2), True, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
        edAx_data3.NTSSetParam(oMenu, NTSCStr(!tb_data3), True, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
        edAx_data4.NTSSetParam(oMenu, NTSCStr(!tb_data4), True, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
        edAx_data5.NTSSetParam(oMenu, NTSCStr(!tb_data5), True, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
        edAx_num1.NTSSetParam(oMenu, NTSCStr(!tb_num1), oApp.FormatSconti, 9, -999999999, 999999999)
        edAx_num2.NTSSetParam(oMenu, NTSCStr(!tb_num2), oApp.FormatSconti, 9, -999999999, 999999999)
        edAx_num3.NTSSetParam(oMenu, NTSCStr(!tb_num3), oApp.FormatSconti, 9, -999999999, 999999999)
        edAx_num4.NTSSetParam(oMenu, NTSCStr(!tb_num4), oApp.FormatSconti, 9, -999999999, 999999999)
        edAx_num5.NTSSetParam(oMenu, NTSCStr(!tb_num5), oApp.FormatSconti, 9, -999999999, 999999999)
        edAx_num6.NTSSetParam(oMenu, NTSCStr(!tb_num6), oApp.FormatSconti, 9, -999999999, 999999999)
        edAx_num7.NTSSetParam(oMenu, NTSCStr(!tb_num7), oApp.FormatSconti, 9, -999999999, 999999999)
        edAx_num8.NTSSetParam(oMenu, NTSCStr(!tb_num8), oApp.FormatSconti, 9, -999999999, 999999999)
        edAx_num9.NTSSetParam(oMenu, NTSCStr(!tb_num9), oApp.FormatSconti, 9, -999999999, 999999999)
        edAx_num10.NTSSetParam(oMenu, NTSCStr(!tb_num10), oApp.FormatSconti, 9, -999999999, 999999999)
        ckAx_check1.NTSSetParam(oMenu, NTSCStr(!tb_check1), "S", "N")
        ckAx_check2.NTSSetParam(oMenu, NTSCStr(!tb_check2), "S", "N")
        ckAx_check3.NTSSetParam(oMenu, NTSCStr(!tb_check3), "S", "N")
        ckAx_check4.NTSSetParam(oMenu, NTSCStr(!tb_check4), "S", "N")
        ckAx_check5.NTSSetParam(oMenu, NTSCStr(!tb_check5), "S", "N")
        ckAx_check6.NTSSetParam(oMenu, NTSCStr(!tb_check6), "S", "N")
        ckAx_check7.NTSSetParam(oMenu, NTSCStr(!tb_check7), "S", "N")
        ckAx_check8.NTSSetParam(oMenu, NTSCStr(!tb_check8), "S", "N")
        ckAx_check9.NTSSetParam(oMenu, NTSCStr(!tb_check9), "S", "N")
        ckAx_check10.NTSSetParam(oMenu, NTSCStr(!tb_check10), "S", "N")
        cbAx_combo1.NTSSetParam(NTSCStr(!tb_combo1))
        cbAx_combo2.NTSSetParam(NTSCStr(!tb_combo2))
        cbAx_combo3.NTSSetParam(NTSCStr(!tb_combo3))
      End With

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

  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano giÃ  stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      edAx_tipo1.NTSDbField = "ANAEXT.ax_tipo1"
      edAx_tipo2.NTSDbField = "ANAEXT.ax_tipo2"
      edAx_tipo3.NTSDbField = "ANAEXT.ax_tipo3"
      edAx_descr1.NTSDbField = "ANAEXT.ax_descr1"
      edAx_descr2.NTSDbField = "ANAEXT.ax_descr2"
      edAx_descr3.NTSDbField = "ANAEXT.ax_descr3"
      edAx_descr4.NTSDbField = "ANAEXT.ax_descr4"
      edAx_descr5.NTSDbField = "ANAEXT.ax_descr5"
      edAx_descr6.NTSDbField = "ANAEXT.ax_descr6"
      edAx_descr7.NTSDbField = "ANAEXT.ax_descr7"
      edAx_descr8.NTSDbField = "ANAEXT.ax_descr8"
      edAx_descr9.NTSDbField = "ANAEXT.ax_descr9"
      edAx_descr10.NTSDbField = "ANAEXT.ax_descr10"
      edAx_desext1.NTSDbField = "ANAEXT.ax_desext1"
      edAx_desext2.NTSDbField = "ANAEXT.ax_desext2"
      edAx_desext3.NTSDbField = "ANAEXT.ax_desext3"
      edAx_memo1.NTSDbField = "ANAEXT.ax_memo1"
      edAx_memo2.NTSDbField = "ANAEXT.ax_memo2"
      edAx_data1.NTSDbField = "ANAEXT.ax_data1"
      edAx_data2.NTSDbField = "ANAEXT.ax_data2"
      edAx_data3.NTSDbField = "ANAEXT.ax_data3"
      edAx_data4.NTSDbField = "ANAEXT.ax_data4"
      edAx_data5.NTSDbField = "ANAEXT.ax_data5"
      edAx_num1.NTSDbField = "ANAEXT.ax_num1"
      edAx_num2.NTSDbField = "ANAEXT.ax_num2"
      edAx_num3.NTSDbField = "ANAEXT.ax_num3"
      edAx_num4.NTSDbField = "ANAEXT.ax_num4"
      edAx_num5.NTSDbField = "ANAEXT.ax_num5"
      edAx_num6.NTSDbField = "ANAEXT.ax_num6"
      edAx_num7.NTSDbField = "ANAEXT.ax_num7"
      edAx_num8.NTSDbField = "ANAEXT.ax_num8"
      edAx_num9.NTSDbField = "ANAEXT.ax_num9"
      edAx_num10.NTSDbField = "ANAEXT.ax_num10"
      ckAx_check1.NTSText.NTSDbField = "ANAEXT.ax_check1"
      ckAx_check2.NTSText.NTSDbField = "ANAEXT.ax_check2"
      ckAx_check3.NTSText.NTSDbField = "ANAEXT.ax_check3"
      ckAx_check4.NTSText.NTSDbField = "ANAEXT.ax_check4"
      ckAx_check5.NTSText.NTSDbField = "ANAEXT.ax_check5"
      ckAx_check6.NTSText.NTSDbField = "ANAEXT.ax_check6"
      ckAx_check7.NTSText.NTSDbField = "ANAEXT.ax_check7"
      ckAx_check8.NTSText.NTSDbField = "ANAEXT.ax_check8"
      ckAx_check9.NTSText.NTSDbField = "ANAEXT.ax_check9"
      ckAx_check10.NTSText.NTSDbField = "ANAEXT.ax_check10"
      cbAx_combo1.NTSDbField = "ANAEXT.ax_combo1"
      cbAx_combo2.NTSDbField = "ANAEXT.ax_combo2"
      cbAx_combo3.NTSDbField = "ANAEXT.ax_combo3"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcAnex, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub


  Public Overridable Function CaricaCombo() As Boolean
    Try
      Dim dttCmb1 As New DataTable()
      dttCmb1.Columns.Add("cod", GetType(String))
      dttCmb1.Columns.Add("val", GetType(String))
      dttCmb1.Rows.Add(New Object() {" ", oApp.Tr(Me, 129054277639023437, "Non impostato")})
      dttCmb1.Rows.Add(New Object() {"A", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom1_A)})
      dttCmb1.Rows.Add(New Object() {"B", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom1_B)})
      dttCmb1.Rows.Add(New Object() {"C", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom1_C)})
      dttCmb1.Rows.Add(New Object() {"D", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom1_D)})
      dttCmb1.Rows.Add(New Object() {"E", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom1_E)})
      dttCmb1.Rows.Add(New Object() {"F", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom1_F)})
      dttCmb1.Rows.Add(New Object() {"G", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom1_G)})
      dttCmb1.Rows.Add(New Object() {"H", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom1_H)})
      dttCmb1.Rows.Add(New Object() {"I", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom1_I)})
      dttCmb1.Rows.Add(New Object() {"L", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom1_L)})
      dttCmb1.AcceptChanges()
      cbAx_combo1.DataSource = dttCmb1
      cbAx_combo1.ValueMember = "cod"
      cbAx_combo1.DisplayMember = "val"

      Dim dttCmb2 As New DataTable()
      dttCmb2.Columns.Add("cod", GetType(String))
      dttCmb2.Columns.Add("val", GetType(String))
      dttCmb2.Rows.Add(New Object() {" ", oApp.Tr(Me, 129054277662460937, "Non impostato")})
      dttCmb2.Rows.Add(New Object() {"A", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom2_A)})
      dttCmb2.Rows.Add(New Object() {"B", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom2_B)})
      dttCmb2.Rows.Add(New Object() {"C", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom2_C)})
      dttCmb2.Rows.Add(New Object() {"D", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom2_D)})
      dttCmb2.Rows.Add(New Object() {"E", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom2_E)})
      dttCmb2.AcceptChanges()
      cbAx_combo2.DataSource = dttCmb2
      cbAx_combo2.ValueMember = "cod"
      cbAx_combo2.DisplayMember = "val"

      Dim dttCmb3 As New DataTable()
      dttCmb3.Columns.Add("cod", GetType(String))
      dttCmb3.Columns.Add("val", GetType(String))
      dttCmb3.Rows.Add(New Object() {" ", oApp.Tr(Me, 128383034272252000, "Non impostato")})
      dttCmb3.Rows.Add(New Object() {"A", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom3_A)})
      dttCmb3.Rows.Add(New Object() {"B", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom3_B)})
      dttCmb3.Rows.Add(New Object() {"C", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom3_C)})
      dttCmb3.Rows.Add(New Object() {"D", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom3_D)})
      dttCmb3.Rows.Add(New Object() {"E", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom3_E)})
      dttCmb3.Rows.Add(New Object() {"F", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom3_F)})
      dttCmb3.Rows.Add(New Object() {"G", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom3_G)})
      dttCmb3.Rows.Add(New Object() {"H", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom3_H)})
      dttCmb3.Rows.Add(New Object() {"I", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom3_I)})
      dttCmb3.Rows.Add(New Object() {"L", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom3_L)})
      dttCmb3.Rows.Add(New Object() {"M", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom3_M)})
      dttCmb3.Rows.Add(New Object() {"N", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom3_N)})
      dttCmb3.Rows.Add(New Object() {"O", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom3_O)})
      dttCmb3.Rows.Add(New Object() {"P", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom3_P)})
      dttCmb3.Rows.Add(New Object() {"Q", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom3_Q)})
      dttCmb3.Rows.Add(New Object() {"R", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom3_R)})
      dttCmb3.Rows.Add(New Object() {"S", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom3_S)})
      dttCmb3.Rows.Add(New Object() {"T", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom3_T)})
      dttCmb3.Rows.Add(New Object() {"U", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom3_U)})
      dttCmb3.Rows.Add(New Object() {"V", NTSCStr(oCleAnex.dttTabaExt.Rows(0)!tb_helpcom3_V)})
      dttCmb3.AcceptChanges()
      cbAx_combo3.DataSource = dttCmb3
      cbAx_combo3.ValueMember = "cod"
      cbAx_combo3.DisplayMember = "val"

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Function CaricaLabel() As Boolean
    Try
      With oCleAnex.dttTabaExt.Rows(0)
        lbAx_tipo1.Text = NTSCStr(!tb_tipo1)
        lbAx_tipo2.Text = NTSCStr(!tb_tipo2)
        lbAx_tipo3.Text = NTSCStr(!tb_tipo3)

        lbAx_descr1.Text = NTSCStr(!tb_descr1)
        lbAx_descr2.Text = NTSCStr(!tb_descr2)
        lbAx_descr3.Text = NTSCStr(!tb_descr3)
        lbAx_descr4.Text = NTSCStr(!tb_descr4)
        lbAx_descr5.Text = NTSCStr(!tb_descr5)
        lbAx_descr6.Text = NTSCStr(!tb_descr6)
        lbAx_descr7.Text = NTSCStr(!tb_descr7)
        lbAx_descr8.Text = NTSCStr(!tb_descr8)
        lbAx_descr9.Text = NTSCStr(!tb_descr9)
        lbAx_descr10.Text = NTSCStr(!tb_descr10)

        lbAx_desext1.Text = NTSCStr(!tb_desext1)
        lbAx_desext2.Text = NTSCStr(!tb_desext2)
        lbAx_desext3.Text = NTSCStr(!tb_desext3)

        lbAx_memo1.Text = NTSCStr(!tb_memo1)
        lbAx_memo2.Text = NTSCStr(!tb_memo2)

        lbAx_data1.Text = NTSCStr(!tb_data1)
        lbAx_data2.Text = NTSCStr(!tb_data2)
        lbAx_data3.Text = NTSCStr(!tb_data3)
        lbAx_data4.Text = NTSCStr(!tb_data4)
        lbAx_data5.Text = NTSCStr(!tb_data5)

        lbAx_num1.Text = NTSCStr(!tb_num1)
        lbAx_num2.Text = NTSCStr(!tb_num2)
        lbAx_num3.Text = NTSCStr(!tb_num3)
        lbAx_num4.Text = NTSCStr(!tb_num4)
        lbAx_num5.Text = NTSCStr(!tb_num5)
        lbAx_num6.Text = NTSCStr(!tb_num6)
        lbAx_num7.Text = NTSCStr(!tb_num7)
        lbAx_num8.Text = NTSCStr(!tb_num8)
        lbAx_num9.Text = NTSCStr(!tb_num9)
        lbAx_num10.Text = NTSCStr(!tb_num10)

        ckAx_check1.Text = NTSCStr(!tb_check1)
        ckAx_check2.Text = NTSCStr(!tb_check2)
        ckAx_check3.Text = NTSCStr(!tb_check3)
        ckAx_check4.Text = NTSCStr(!tb_check4)
        ckAx_check5.Text = NTSCStr(!tb_check5)
        ckAx_check6.Text = NTSCStr(!tb_check6)
        ckAx_check7.Text = NTSCStr(!tb_check7)
        ckAx_check8.Text = NTSCStr(!tb_check8)
        ckAx_check9.Text = NTSCStr(!tb_check9)
        ckAx_check10.Text = NTSCStr(!tb_check10)

        lbAx_combo1.Text = NTSCStr(!tb_combo1)
        lbAx_combo2.Text = NTSCStr(!tb_combo2)
        lbAx_combo3.Text = NTSCStr(!tb_combo3)

        If NTSCStr(!tb_destab1) <> "" Then tsAnex.TabPages(0).Text = "&1 - " & NTSCStr(!tb_destab1)
        If NTSCStr(!tb_destab2) <> "" Then tsAnex.TabPages(1).Text = "&2 - " & NTSCStr(!tb_destab2)
        If NTSCStr(!tb_destab3) <> "" Then tsAnex.TabPages(2).Text = "&3 - " & NTSCStr(!tb_destab3)
        If NTSCStr(!tb_destab4) <> "" Then tsAnex.TabPages(3).Text = "&4 - " & NTSCStr(!tb_destab4)
        If NTSCStr(!tb_destab5) <> "" Then tsAnex.TabPages(4).Text = "&5 - " & NTSCStr(!tb_destab5)
        If NTSCStr(!tb_destab6) <> "" Then tsAnex.TabPages(5).Text = "&6 - " & NTSCStr(!tb_destab6)

        lbHelptipo1.Text = NTSCStr(!tb_helptipo1).Replace(vbCrLf, " - ")
        lbHelptipo2.Text = NTSCStr(!tb_helptipo2).Replace(vbCrLf, " - ")
        lbHelptipo3.Text = NTSCStr(!tb_helptipo3).Replace(vbCrLf, " - ")
      End With

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Function NascondiControlli() As Boolean
    Try
      If lbAx_tipo1.Text.Trim = "" Then
        edAx_tipo1.Visible = False
        lbHelptipo1.Visible = False
      End If

      If lbAx_tipo2.Text.Trim = "" Then
        edAx_tipo2.Visible = False
        lbHelptipo2.Visible = False
      End If

      If lbAx_tipo3.Text.Trim = "" Then
        edAx_tipo3.Visible = False
        lbHelptipo3.Visible = False
      End If

      If lbAx_descr1.Text.Trim = "" Then edAx_descr1.Visible = False
      If lbAx_descr2.Text.Trim = "" Then edAx_descr2.Visible = False
      If lbAx_descr3.Text.Trim = "" Then edAx_descr3.Visible = False
      If lbAx_descr4.Text.Trim = "" Then edAx_descr4.Visible = False
      If lbAx_descr5.Text.Trim = "" Then edAx_descr5.Visible = False
      If lbAx_descr6.Text.Trim = "" Then edAx_descr6.Visible = False
      If lbAx_descr7.Text.Trim = "" Then edAx_descr7.Visible = False
      If lbAx_descr8.Text.Trim = "" Then edAx_descr8.Visible = False
      If lbAx_descr9.Text.Trim = "" Then edAx_descr9.Visible = False
      If lbAx_descr10.Text.Trim = "" Then edAx_descr10.Visible = False

      If lbAx_desext1.Text.Trim = "" Then edAx_desext1.Visible = False
      If lbAx_desext2.Text.Trim = "" Then edAx_desext2.Visible = False
      If lbAx_desext3.Text.Trim = "" Then edAx_desext3.Visible = False

      If lbAx_memo1.Text.Trim = "" Then edAx_memo1.Visible = False
      If lbAx_memo2.Text.Trim = "" Then edAx_memo2.Visible = False

      If lbAx_data1.Text.Trim = "" Then edAx_data1.Visible = False
      If lbAx_data2.Text.Trim = "" Then edAx_data2.Visible = False
      If lbAx_data3.Text.Trim = "" Then edAx_data3.Visible = False
      If lbAx_data4.Text.Trim = "" Then edAx_data4.Visible = False
      If lbAx_data5.Text.Trim = "" Then edAx_data5.Visible = False

      If lbAx_num1.Text.Trim = "" Then edAx_num1.Visible = False
      If lbAx_num2.Text.Trim = "" Then edAx_num2.Visible = False
      If lbAx_num3.Text.Trim = "" Then edAx_num3.Visible = False
      If lbAx_num4.Text.Trim = "" Then edAx_num4.Visible = False
      If lbAx_num5.Text.Trim = "" Then edAx_num5.Visible = False
      If lbAx_num6.Text.Trim = "" Then edAx_num6.Visible = False
      If lbAx_num7.Text.Trim = "" Then edAx_num7.Visible = False
      If lbAx_num8.Text.Trim = "" Then edAx_num8.Visible = False
      If lbAx_num9.Text.Trim = "" Then edAx_num9.Visible = False
      If lbAx_num10.Text.Trim = "" Then edAx_num10.Visible = False

      If ckAx_check1.Text.Trim = "" Then ckAx_check1.Visible = False
      If ckAx_check2.Text.Trim = "" Then ckAx_check2.Visible = False
      If ckAx_check3.Text.Trim = "" Then ckAx_check3.Visible = False
      If ckAx_check4.Text.Trim = "" Then ckAx_check4.Visible = False
      If ckAx_check5.Text.Trim = "" Then ckAx_check5.Visible = False
      If ckAx_check6.Text.Trim = "" Then ckAx_check6.Visible = False
      If ckAx_check7.Text.Trim = "" Then ckAx_check7.Visible = False
      If ckAx_check8.Text.Trim = "" Then ckAx_check8.Visible = False
      If ckAx_check9.Text.Trim = "" Then ckAx_check9.Visible = False
      If ckAx_check10.Text.Trim = "" Then ckAx_check10.Visible = False

      If lbAx_combo1.Text.Trim = "" Then cbAx_combo1.Visible = False
      If lbAx_combo2.Text.Trim = "" Then cbAx_combo2.Visible = False
      If lbAx_combo3.Text.Trim = "" Then cbAx_combo3.Visible = False

      '------------------------------------------
      'nascondo i tab senza dati: non posso più nasconderli, visto che potrei postare i campi da un tab ad uno nascosto ...
      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function


#Region "Eventi Form"
  Public Overridable Sub FRM__ANEX_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim i As Integer = 0
    Dim dttTmp As New DataTable
    Dim dtrT() As DataRow = Nothing
    Try
      '-------------------------------------------------
      'parametri passati dal chiamante
      oCleAnex.bNew = oCallParams.bAddNew
      oCleAnex.strTipork = oCallParams.strPar1
      oCleAnex.strParent = oCallParams.strNomProg
      oCleAnex.nCoddest = NTSCInt(oCallParams.dPar1)
      'datatable contenente il record passatomi: viene sempre passato un record, con bNew so se è nuovo oppure no
      dsAnex.Tables.Clear()
      dttTmp = CType(oCallParams.ctlPar1, DataTable).Clone
      dttTmp.TableName = "ANAEXT"
      dtrT = CType(oCallParams.ctlPar1, DataTable).Select("ax_coddest = " & oCleAnex.nCoddest.ToString)
      If dtrT.Length = 0 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 129737941910839844, "BN__ANEX: Nel DataTable passato non è presente nessun record con destinazione diversa = |" & oCleAnex.nCoddest.ToString & "|"))
        Me.Close()
        Return
      End If
      dttTmp.ImportRow(dtrT(0))
      dsAnex.Tables.Add(dttTmp)
      dsAnex.AcceptChanges()

      '-------------------------------------------------
      'carico i nomi dei controlli
      If Not oCleAnex.Apri(DittaCorrente, oCleAnex.strTipork, dsAnex) Then
        Me.Close()
        Return
      End If

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      dcAnex.DataSource = dsAnex.Tables("ANAEXT")
      dsAnex.AcceptChanges()

      '-------------------------------------------------
      'carico il testo delle label
      CaricaLabel()
      CaricaCombo()
      NascondiControlli()

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlTipoDoc = oCleAnex.strTipork
      GctlSetRoules()

      dcAnex.ResetBindings(False)
      dcAnex.MoveFirst()

      tsAnex.SelectedTabPageIndex = 0

      '-------------------------------------------------
      'se è un nuovo record applico i valori di default
      If oCleAnex.bNew Then GctlApplicaDefaultValue()

      Me.Text += " " & oApp.Tr(Me, 128382916430266000, "Conto/Destin/Lead/Art/Matr/") & ": " & _
                                                  dttTmp.Rows(0)!ax_conto.ToString & "/" & _
                                                  dttTmp.Rows(0)!ax_coddest.ToString & "/" & _
                                                  dttTmp.Rows(0)!ax_codlead.ToString & "/" & _
                                                  dttTmp.Rows(0)!ax_codart.ToString & "/" & _
                                                  dttTmp.Rows(0)!ax_matric.ToString
      'se serve blocco il comando 'salva'
      If Not oCallParams Is Nothing Then
        If oCallParams.bPar2 = True Then
          tlbSalva.Enabled = False
        End If
      End If

      If oCallParams.bPar1 Then DisabilitaControlli(Me)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__ANEX_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Dim dtrT() As DataRow = Nothing
    Try
      If dsAnex.Tables("ANAEXT") Is Nothing Then Return
      If dsAnex.Tables("ANAEXT").Rows.Count > 0 Then
        If tlbSalva.Enabled Then
          If Not Salva() Then
            e.Cancel = True
            Return
          End If
        Else
          NTSFormClearDataBinding(Me)
          oCleAnex.Ripristina(dcAnex.Position, dcAnex.Filter)
        End If
        dtrT = CType(oCallParams.ctlPar1, DataTable).Select("ax_coddest = " & oCleAnex.nCoddest.ToString)
        dtrT(0).Delete()
        CType(oCallParams.ctlPar1, DataTable).ImportRow(dsAnex.Tables("ANAEXT").Rows(0))
        CType(oCallParams.ctlPar1, DataTable).AcceptChanges()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub FRM__ANEX_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcAnex.Dispose()
      dsAnex.Dispose()
    Catch
    End Try
  End Sub
#End Region

  Public Overridable Function DisabilitaControlli(ByVal oControls As Control) As Boolean
    Try
      For Each oControl As Control In oControls.Controls
        Dim strControl As String = oControl.GetType().ToString.ToLower
        If strControl.IndexOf("text") >= 0 OrElse strControl.IndexOf("check") >= 0 OrElse strControl.IndexOf("combo") >= 0 OrElse _
           strControl.IndexOf("memo") >= 0 Then
          oControl.Enabled = False
        Else
          DisabilitaControlli(oControl)
        End If
      Next

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function


#Region "Eventi Toolbar"
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

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Dim bRemovBinding As Boolean = False
    Try
      Me.ValidaLastControl()          'se non valido il controllo su cui sono, quando modifico il controllo e, senza uscire, faccio 'ripristina' il controllo rimane sporco

      '-------------------------------------------------
      'ripristino la forma di pagamento
      Dim dlgRes As DialogResult
      If Not sender Is Nothing Then
        'chiamato facendo pressione sulla funzione 'ripristina'
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128382165714952775, "Ripristinare le modifiche apportate?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          If dsAnex.Tables("ANAEXT").Rows.Count = 1 And dsAnex.Tables("ANAEXT").Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          oCleAnex.Ripristina(dcAnex.Position, dcAnex.Filter)

          If bRemovBinding Then
            'tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcAnex, Me)
            bRemovBinding = False
          End If
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcAnex, Me)
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
    Dim dRes As DialogResult
    Try
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      If oCleAnex.RecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128382165715108775, "Confermi il salvataggio?"))
        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oCleAnex.Salva(False) Then Return False
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
End Class

