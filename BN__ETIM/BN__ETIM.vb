Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__ETIM
#Region "Moduli"
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
#End Region

  Public oCleEtim As CLE__ETIM
  Public oCallParams As CLE__CLDP
  Public dttAlerts As DataTable
  Public dsEtim As DataSet
  Public dcEtim As BindingSource = New BindingSource()

#Region "Variabili"
  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbElabora As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public bStatoIcona As Boolean = False         'server per il lampeggio dell'icona nella try area
  Public nIndice As Integer = 0
  Public nIntervallo As Integer = 30          'intervallo di esecuzione programma (usato quando vengo chiamato in modalità busbatch), ovvero ogni quanti minuti il programma deve verificare se esistono alert da verificare/eseguire
  Public tcpServer As New System.Net.Sockets.TcpListener(System.Net.IPAddress.Any, 1002)   'socket server in attesa che si connettano dei client con Busserv.exe
  Public ProcessRequest As Threading.Thread = Nothing
#End Region

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__ETIM))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbElabora = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.lbDataelab = New NTSInformatica.NTSLabel
    Me.edDataelab = New NTSInformatica.NTSTextBoxData
    Me.fmDitta = New NTSInformatica.NTSGroupBox
    Me.lbDitta = New NTSInformatica.NTSLabel
    Me.edDitta = New NTSInformatica.NTSTextBoxStr
    Me.opDitteuna = New NTSInformatica.NTSRadioButton
    Me.opDittetutte = New NTSInformatica.NTSRadioButton
    Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
    Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
    Me.ckNoAggData = New NTSInformatica.NTSCheckBox
    Me.ckBatch1Giro = New NTSInformatica.NTSCheckBox
    Me.fmAlert = New NTSInformatica.NTSGroupBox
    Me.fmSoloalert = New NTSInformatica.NTSGroupBox
    Me.cmdDesel = New NTSInformatica.NTSButton
    Me.cmdSel = New NTSInformatica.NTSButton
    Me.liAlert = New NTSInformatica.NTSListBox
    Me.opAlertalcuni = New NTSInformatica.NTSRadioButton
    Me.opAlerttutti = New NTSInformatica.NTSRadioButton
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbGeneraBub = New NTSInformatica.NTSBarMenuItem
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataelab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmDitta, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmDitta.SuspendLayout()
    CType(Me.edDitta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opDitteuna.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opDittetutte.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNoAggData.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckBatch1Giro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmAlert, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmAlert.SuspendLayout()
    CType(Me.fmSoloalert, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.liAlert, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opAlertalcuni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opAlerttutti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbElabora, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti, Me.tlbGeneraBub})
    Me.NtsBarManager1.MaxItemId = 20
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbElabora), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbElabora
    '
    Me.tlbElabora.Caption = "Elabora"
    Me.tlbElabora.Glyph = CType(resources.GetObject("tlbElabora.Glyph"), System.Drawing.Image)
    Me.tlbElabora.GlyphPath = ""
    Me.tlbElabora.Id = 0
    Me.tlbElabora.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbElabora.Name = "tlbElabora"
    Me.tlbElabora.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.GlyphPath = ""
    Me.tlbZoom.Id = 13
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 11
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 12
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'lbDataelab
    '
    Me.lbDataelab.AutoSize = True
    Me.lbDataelab.BackColor = System.Drawing.Color.Transparent
    Me.lbDataelab.Location = New System.Drawing.Point(11, 39)
    Me.lbDataelab.Name = "lbDataelab"
    Me.lbDataelab.NTSDbField = ""
    Me.lbDataelab.Size = New System.Drawing.Size(94, 13)
    Me.lbDataelab.TabIndex = 4
    Me.lbDataelab.Text = "Data elaborazione"
    Me.lbDataelab.Tooltip = ""
    Me.lbDataelab.UseMnemonic = False
    '
    'edDataelab
    '
    Me.edDataelab.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edDataelab.EditValue = "01/01/1900"
    Me.edDataelab.Enabled = False
    Me.edDataelab.Location = New System.Drawing.Point(112, 36)
    Me.edDataelab.Name = "edDataelab"
    Me.edDataelab.NTSDbField = ""
    Me.edDataelab.NTSForzaVisZoom = False
    Me.edDataelab.NTSOldValue = ""
    Me.edDataelab.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataelab.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataelab.Properties.AutoHeight = False
    Me.edDataelab.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataelab.Properties.MaxLength = 65536
    Me.edDataelab.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataelab.Size = New System.Drawing.Size(100, 20)
    Me.edDataelab.TabIndex = 5
    '
    'fmDitta
    '
    Me.fmDitta.AllowDrop = True
    Me.fmDitta.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmDitta.Appearance.Options.UseBackColor = True
    Me.fmDitta.Controls.Add(Me.lbDitta)
    Me.fmDitta.Controls.Add(Me.edDitta)
    Me.fmDitta.Controls.Add(Me.opDitteuna)
    Me.fmDitta.Controls.Add(Me.opDittetutte)
    Me.fmDitta.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmDitta.Location = New System.Drawing.Point(11, 62)
    Me.fmDitta.Name = "fmDitta"
    Me.fmDitta.Size = New System.Drawing.Size(411, 75)
    Me.fmDitta.TabIndex = 6
    Me.fmDitta.Text = "Ditta da elaborare"
    '
    'lbDitta
    '
    Me.lbDitta.BackColor = System.Drawing.Color.Transparent
    Me.lbDitta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDitta.Location = New System.Drawing.Point(203, 49)
    Me.lbDitta.Name = "lbDitta"
    Me.lbDitta.NTSDbField = ""
    Me.lbDitta.Size = New System.Drawing.Size(203, 20)
    Me.lbDitta.TabIndex = 3
    Me.lbDitta.Tooltip = ""
    Me.lbDitta.UseMnemonic = False
    '
    'edDitta
    '
    Me.edDitta.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDitta.EditValue = "PROVA"
    Me.edDitta.Location = New System.Drawing.Point(81, 49)
    Me.edDitta.Name = "edDitta"
    Me.edDitta.NTSDbField = ""
    Me.edDitta.NTSForzaVisZoom = False
    Me.edDitta.NTSOldValue = "PROVA"
    Me.edDitta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDitta.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDitta.Properties.AutoHeight = False
    Me.edDitta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDitta.Properties.MaxLength = 65536
    Me.edDitta.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDitta.Size = New System.Drawing.Size(116, 20)
    Me.edDitta.TabIndex = 2
    '
    'opDitteuna
    '
    Me.opDitteuna.Cursor = System.Windows.Forms.Cursors.Default
    Me.opDitteuna.Location = New System.Drawing.Point(3, 49)
    Me.opDitteuna.Name = "opDitteuna"
    Me.opDitteuna.NTSCheckValue = "S"
    Me.opDitteuna.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opDitteuna.Properties.Appearance.Options.UseBackColor = True
    Me.opDitteuna.Properties.AutoHeight = False
    Me.opDitteuna.Properties.Caption = "&Una ditta"
    Me.opDitteuna.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opDitteuna.Size = New System.Drawing.Size(72, 19)
    Me.opDitteuna.TabIndex = 1
    '
    'opDittetutte
    '
    Me.opDittetutte.Cursor = System.Windows.Forms.Cursors.Default
    Me.opDittetutte.EditValue = True
    Me.opDittetutte.Location = New System.Drawing.Point(3, 24)
    Me.opDittetutte.Name = "opDittetutte"
    Me.opDittetutte.NTSCheckValue = "S"
    Me.opDittetutte.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opDittetutte.Properties.Appearance.Options.UseBackColor = True
    Me.opDittetutte.Properties.AutoHeight = False
    Me.opDittetutte.Properties.Caption = "&Tutte le ditte"
    Me.opDittetutte.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opDittetutte.Size = New System.Drawing.Size(89, 19)
    Me.opDittetutte.TabIndex = 0
    '
    'NotifyIcon1
    '
    Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
    Me.NotifyIcon1.Text = "NotifyIcon1"
    '
    'Timer1
    '
    Me.Timer1.Interval = 60000
    '
    'ckNoAggData
    '
    Me.ckNoAggData.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckNoAggData.Location = New System.Drawing.Point(11, 401)
    Me.ckNoAggData.Name = "ckNoAggData"
    Me.ckNoAggData.NTSCheckValue = "S"
    Me.ckNoAggData.NTSUnCheckValue = "N"
    Me.ckNoAggData.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNoAggData.Properties.Appearance.Options.UseBackColor = True
    Me.ckNoAggData.Properties.AutoHeight = False
    Me.ckNoAggData.Properties.Caption = "Non aggiornare la data ultima esecuzione su alert (da usare solo per il test)"
    Me.ckNoAggData.Size = New System.Drawing.Size(380, 17)
    Me.ckNoAggData.TabIndex = 7
    '
    'ckBatch1Giro
    '
    Me.ckBatch1Giro.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.ckBatch1Giro.Location = New System.Drawing.Point(11, 424)
    Me.ckBatch1Giro.Name = "ckBatch1Giro"
    Me.ckBatch1Giro.NTSCheckValue = "S"
    Me.ckBatch1Giro.NTSUnCheckValue = "N"
    Me.ckBatch1Giro.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckBatch1Giro.Properties.Appearance.Options.UseBackColor = True
    Me.ckBatch1Giro.Properties.AutoHeight = False
    Me.ckBatch1Giro.Properties.Caption = "In modalità batch chiudi il programma dopo l'esecuzione"
    Me.ckBatch1Giro.Size = New System.Drawing.Size(297, 19)
    Me.ckBatch1Giro.TabIndex = 7
    '
    'fmAlert
    '
    Me.fmAlert.AllowDrop = True
    Me.fmAlert.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmAlert.Appearance.Options.UseBackColor = True
    Me.fmAlert.Controls.Add(Me.fmSoloalert)
    Me.fmAlert.Controls.Add(Me.cmdDesel)
    Me.fmAlert.Controls.Add(Me.cmdSel)
    Me.fmAlert.Controls.Add(Me.liAlert)
    Me.fmAlert.Controls.Add(Me.opAlertalcuni)
    Me.fmAlert.Controls.Add(Me.opAlerttutti)
    Me.fmAlert.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmAlert.Location = New System.Drawing.Point(11, 143)
    Me.fmAlert.Name = "fmAlert"
    Me.fmAlert.Size = New System.Drawing.Size(411, 252)
    Me.fmAlert.TabIndex = 9
    Me.fmAlert.Text = "Alert da eseguire"
    '
    'fmSoloalert
    '
    Me.fmSoloalert.AllowDrop = True
    Me.fmSoloalert.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmSoloalert.Appearance.Options.UseBackColor = True
    Me.fmSoloalert.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmSoloalert.Location = New System.Drawing.Point(390, 24)
    Me.fmSoloalert.Name = "fmSoloalert"
    Me.fmSoloalert.Size = New System.Drawing.Size(16, 23)
    Me.fmSoloalert.TabIndex = 8
    Me.fmSoloalert.Visible = False
    '
    'cmdDesel
    '
    Me.cmdDesel.ImagePath = ""
    Me.cmdDesel.ImageText = ""
    Me.cmdDesel.Location = New System.Drawing.Point(306, 223)
    Me.cmdDesel.Name = "cmdDesel"
    Me.cmdDesel.NTSContextMenu = Nothing
    Me.cmdDesel.Size = New System.Drawing.Size(100, 22)
    Me.cmdDesel.TabIndex = 3
    Me.cmdDesel.Text = "Deseleziona tutti"
    '
    'cmdSel
    '
    Me.cmdSel.ImagePath = ""
    Me.cmdSel.ImageText = ""
    Me.cmdSel.Location = New System.Drawing.Point(22, 223)
    Me.cmdSel.Name = "cmdSel"
    Me.cmdSel.NTSContextMenu = Nothing
    Me.cmdSel.Size = New System.Drawing.Size(100, 22)
    Me.cmdSel.TabIndex = 4
    Me.cmdSel.Text = "Seleziona tutti"
    '
    'liAlert
    '
    Me.liAlert.Cursor = System.Windows.Forms.Cursors.Default
    Me.liAlert.ItemHeight = 14
    Me.liAlert.Location = New System.Drawing.Point(22, 74)
    Me.liAlert.Name = "liAlert"
    Me.liAlert.NTSDbField = ""
    Me.liAlert.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
    Me.liAlert.Size = New System.Drawing.Size(384, 143)
    Me.liAlert.TabIndex = 2
    '
    'opAlertalcuni
    '
    Me.opAlertalcuni.Cursor = System.Windows.Forms.Cursors.Default
    Me.opAlertalcuni.Location = New System.Drawing.Point(3, 49)
    Me.opAlertalcuni.Name = "opAlertalcuni"
    Me.opAlertalcuni.NTSCheckValue = "S"
    Me.opAlertalcuni.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opAlertalcuni.Properties.Appearance.Options.UseBackColor = True
    Me.opAlertalcuni.Properties.AutoHeight = False
    Me.opAlertalcuni.Properties.Caption = "&Solo gli alert selezionati:"
    Me.opAlertalcuni.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opAlertalcuni.Size = New System.Drawing.Size(143, 19)
    Me.opAlertalcuni.TabIndex = 1
    '
    'opAlerttutti
    '
    Me.opAlerttutti.Cursor = System.Windows.Forms.Cursors.Default
    Me.opAlerttutti.EditValue = True
    Me.opAlerttutti.Location = New System.Drawing.Point(3, 24)
    Me.opAlerttutti.Name = "opAlerttutti"
    Me.opAlerttutti.NTSCheckValue = "S"
    Me.opAlerttutti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opAlerttutti.Properties.Appearance.Options.UseBackColor = True
    Me.opAlerttutti.Properties.AutoHeight = False
    Me.opAlerttutti.Properties.Caption = "&Tutti gli alert"
    Me.opAlerttutti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opAlerttutti.Size = New System.Drawing.Size(89, 19)
    Me.opAlerttutti.TabIndex = 0
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 17
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGeneraBub)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbGeneraBub
    '
    Me.tlbGeneraBub.Caption = "Crea file per la schedulazione"
    Me.tlbGeneraBub.GlyphPath = ""
    Me.tlbGeneraBub.Id = 19
    Me.tlbGeneraBub.Name = "tlbGeneraBub"
    Me.tlbGeneraBub.NTSIsCheckBox = False
    Me.tlbGeneraBub.Visible = True
    '
    'FRM__ETIM
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(433, 445)
    Me.Controls.Add(Me.fmAlert)
    Me.Controls.Add(Me.ckBatch1Giro)
    Me.Controls.Add(Me.ckNoAggData)
    Me.Controls.Add(Me.fmDitta)
    Me.Controls.Add(Me.edDataelab)
    Me.Controls.Add(Me.lbDataelab)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRM__ETIM"
    Me.Text = "ESECUZIONE AUTOMATICA PROCEDURE DI ALERTING"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataelab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmDitta, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmDitta.ResumeLayout(False)
    Me.fmDitta.PerformLayout()
    CType(Me.edDitta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opDitteuna.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opDittetutte.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNoAggData.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckBatch1Giro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmAlert, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmAlert.ResumeLayout(False)
    Me.fmAlert.PerformLayout()
    CType(Me.fmSoloalert, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.liAlert, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opAlertalcuni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opAlerttutti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

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
    'creo e attivo l'entity e inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__ETIM", "BE__ETIM", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128744059373510000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleEtim = CType(oTmp, CLE__ETIM)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__ETIM", strRemoteServer, strRemotePort)
    AddHandler oCleEtim.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleEtim.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)

    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbElabora.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      edDitta.NTSSetParam(oMenu, oApp.Tr(Me, 128744076337010000, "Ditta da elaborare"), 12, False)
      opDitteuna.NTSSetParam(oMenu, oApp.Tr(Me, 128744076337166000, "Una ditta"), "U")
      opDittetutte.NTSSetParam(oMenu, oApp.Tr(Me, 128744079788466000, "Tutte le ditte"), "T")
      opAlerttutti.NTSSetParam(oMenu, oApp.Tr(Me, 128744076337166001, "Tutti gli alert"), "T")
      opAlertalcuni.NTSSetParam(oMenu, oApp.Tr(Me, 128744079788466002, "Solo gli alert selezionati"), "A")
      edDataelab.NTSSetParam(oMenu, oApp.Tr(Me, 128744076337322000, "Data elaborazione"), True)
      edDitta.NTSSetParamZoom("ZOOMDITTE")
      ckNoAggData.NTSSetParam(oMenu, oApp.Tr(Me, 128745802528217000, "Non aggiornare la data ultima esecuzione su alert"), "S", "N")

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
        Try
          Select Case strTmp(i).Substring(0, 10)
            Case "NOTIFYI_1:"
              NotifyIcon1.Icon = New Icon(oApp.ChildImageDir & "\ricevitore_b.ico")
              NotifyIcon1.BalloonTipTitle = "Busines NET Batch"
              NotifyIcon1.Text = oApp.Tr(Me, 128745176962977000, "Esecuzione automatica procedure di alerting - running")
              'sospendo la socket server in listen
              Try
                If oApp.Batch Then tcpServer.Stop()
              Catch ex As Exception
              End Try

            Case "NOTIFYI_2:"
              NotifyIcon1.Icon = New Icon(oApp.ChildImageDir & "\ricevitore_a.ico")
              NotifyIcon1.Text = oApp.Tr(Me, 128745176977017000, "Esecuzione automatica procedure di alerting - sleep")
              'riavvio la socket server in listen
              'Ascolto
              Try
                If oApp.Batch Then
                  'tcpServer.Start()
                  StartSocketServer()
                End If

              Catch ex As Exception
              End Try

          End Select
        Catch ex As Exception
        End Try
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi di Form"
  Public Overridable Sub FRM__ETIM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      
      edDitta.Text = DittaCorrente
      edDitta_Validated(edDitta, Nothing)

      edDataelab.Text = DateTime.Now.ToShortDateString
      opDittetutte_CheckedChanged(opDittetutte, Nothing)

      opAlerttutti.Checked = True

      oCleEtim.GetAlerts(dttAlerts)

      liAlert.DisplayMember = "val"
      liAlert.ValueMember = "cod"
      liAlert.DataSource = dttAlerts

      For z As Integer = 0 To liAlert.ItemCount - 1
        liAlert.SetSelected(z, False)
      Next

      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      GctlApplicaDefaultValue()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__ETIM_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try

      '-------------------------------------------------
      'se sono stato chiamato in modalità batch:
      'esempio di riga di comando:
      'Busnet.exe mirto . prova14 business BN__ETIM /B c:\bus\asc\BN__ETIM.BUB ord01 
      If oApp.Batch And oApp.AvvioProgrammaParametri.Trim <> "" Then
        'Me.Visible = False
        Me.Top = -10000
        Me.Left = -10000
        nIntervallo = NTSCInt(oMenu.GetSettingBus("BS--ETIM", "OPZIONI", ".", "Intervallo_Esecuzione_Programma", "30", " ", "30")) 'ogni quanti minuti il programma deve verificare se esistono alert da verificare/eseguire

        'creo l'icona sulla try area
        NotifyIcon1.Text = oApp.Tr(Me, 128745177100101000, "Esecuzione automatica procedure di alerting - sleep")
        NotifyIcon1.Icon = New Icon(oApp.ChildImageDir & "\ricevitore_a.ico")
        NotifyIcon1.Visible = True

        'metto il server socket in attesa
        'server per fare in modo che se quando qualche cliet si connette mi manda in avviso che è presente 
        'ed io devo eventualmente inviargli gli alert di tipo popup che non erano stati ricevuti perchè il client era chiuso
        StartSocketServer()


        'lancio l'elaborazione. i parametri di avvio verranno presi dal file BN__ETIM.BUB
        StartBatch()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__ETIM_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Try

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__ETIM_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If oCleEtim.bInElaborazione Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128744093853082000, "Attendere il termine dell'elaborazione prima di chiudere il programma"))
        e.Cancel = True
        Return
      Else
        If oApp.Batch And oApp.AvvioProgrammaParametri.Trim <> "" Then
          NotifyIcon1.Visible = False
          StopBatch()
        End If

        Try
          If oApp.Batch Then tcpServer.Stop()
        Catch ex As Exception
        End Try
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbElabora_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbElabora.ItemClick
    Dim strTmp As String = ""
    Dim dttAle As DataTable = Nothing
    Dim strAlert As String = ""
    Try
      If oCleEtim.bInElaborazione Then Return

      Me.ValidaLastControl()

      oCleEtim.lIdAlertUnico = 0

      If Not oApp.Batch Then
        'Richiesta di conferma
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128744094523570000, "Si conferma di voler procedere con l'esecuzione automatica di procedure di alerting?")) = Windows.Forms.DialogResult.No Then Return

        If opAlertalcuni.Checked And GetSelectedAlert() = "" Then
          'Messaggio
          oApp.MsgBoxErr(oApp.Tr(Me, 128744371685129001, "Almeno un alert deve essere eseguito." & vbCrLf & _
                                                         "Impossibile procedere."))
          Return
        End If

        If ckNoAggData.Checked Then
          'solo per il debug
          strTmp = oApp.InputBoxNew(oApp.Tr(Me, 128745808643257000, _
                          "Attenzione: è stato impostato che la procedura non dovrà aggiornare la data ultima esecuzione sugli alert da processare." & _
                          "questa modalità dovrebbe essere utilizzata solo per il test di un nuovo alert." & vbCrLf & _
                          "Inserire il numero di Alert da processare"), "0")
          If NTSCInt(strTmp) = 0 Then
            oApp.MsgBoxInfo(oApp.Tr(Me, 128745815922015000, "Elaborazione annullata"))
            Return
          Else
            oCleEtim.lIdAlertUnico = NTSCInt(strTmp)
          End If
        End If
      End If

      strAlert = GetSelectedAlert()
      If strAlert.Trim <> "" Then
        oCleEtim.SelezionaAlertDaEseguire(edDitta.Text, opDitteuna.Checked, strAlert, dttAle)
      End If
      Me.Cursor = Cursors.WaitCursor
      oCleEtim.strDittaDaElab = IIf(opDittetutte.Checked, "", edDitta.Text).ToString
      oCleEtim.strDataElab = edDataelab.Text
      oCleEtim.bNoAggdataUlEsec = CBool(IIf(ckNoAggData.Checked, True, False))
      oCleEtim.Elabora(False, dttAle)

      'se serve visualizzo il file di log
      Me.Cursor = Cursors.Default
      oApp.MsgBoxInfo(oApp.Tr(Me, 127939835583750000, "Esecuzione automatica procedure di alerting terminata."))

      If oCleEtim.LogError = True Then
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127940796626250000, "Esistono dei messaggi nel file di log del programma. Visualizzare il file?")) = Windows.Forms.DialogResult.Yes Then
          NTSProcessStart("notepad", oCleEtim.LogFileName)
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim ctrlTmp As Control = Nothing
    Dim oParam As New CLE__CLDP
    Dim strDittaNew As String = ""
    Try
      'zoom standard
      ctrlTmp = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return

      '-------------------------------
      'ho più ditte e le anagrafiche generali: visualizzo lo zoom ditte
      If ctrlTmp.Name = "edDitta" Then
        oParam.strNomProg = "BN__ETIM"
        oParam.dPar1 = oCleEtim.Moduli
        oParam.dPar2 = oCleEtim.ModuliExt
        oParam.dPar3 = oCleEtim.ModuliSup
        oParam.dPar4 = oCleEtim.ModuliSupExt
        oParam.dPar5 = oCleEtim.ModuliPtn
        oParam.strPar1 = oCleEtim.ModuliPtnExt.ToString
        oMenu.RunZoomNet("NTSInformatica", "FRM__HLDI", "", "BN__HLDI", "Zoom ditte", edDitta.Text, CObj(oParam))

        strDittaNew = oParam.Ditta
        oParam = Nothing
        If strDittaNew.Trim <> "" Then edDitta.Text = strDittaNew.Trim

      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        NTSCallStandardZoom()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGeneraBub_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGeneraBub.ItemClick
    Dim ctrlTmp As Control = Nothing
    Dim strSelectedAlert As String = ""
    Try
      If Not CLN__STD.UserIsAdmin(oApp.User.Gruppo) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130917308022872889, "Funzionalità abilitata solo per utenti amministratori"))
        Return
      End If

      If opAlertalcuni.Checked Then
        strSelectedAlert = GetSelectedAlert()
        If strSelectedAlert = "" Then
          'Messaggio
          oApp.MsgBoxErr(oApp.Tr(Me, 128744371685129002, "Almeno un alert deve essere eseguito." & vbCrLf & _
                                                         "Impossibile creare il file .BUB."))
          Return
        End If
      End If
      If System.IO.File.Exists(oApp.AscDir & "\BN__ETIM.BUB") Then
        If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128744957738192000, "Esiste già un file con nome |" & oApp.AscDir & "\BN__ETIM.BUB" & "|: sovrascriverlo?")) = Windows.Forms.DialogResult.No Then Return
      End If
      Dim w1 As New System.IO.StreamWriter(oApp.AscDir & "\BN__ETIM.BUB", False)
      w1.WriteLine("edDataelab;" & edDataelab.Text)
      w1.WriteLine("edDitta;" & edDitta.Text)
      w1.WriteLine("opDittetutte;" & IIf(opDittetutte.Checked, "true", "false").ToString)
      w1.WriteLine("opDitteuna;" & IIf(opDitteuna.Checked, "true", "false").ToString)
      w1.WriteLine("liAlert;" & strSelectedAlert)
      w1.WriteLine("ckBatch1Giro;" & IIf(ckBatch1Giro.Checked, "true", "false").ToString)
      w1.Flush()
      w1.Close()
      oApp.MsgBoxInfo(oApp.Tr(Me, 128744371685129000, "Creato file |" & oApp.AscDir & "\BN__ETIM.BUB" & "| correttamente"))

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub
#End Region

  Public Overridable Sub edDitta_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDitta.Validated
    Dim strTmp As String = ""
    Try
      If oCleEtim Is Nothing Then Return
      If Not oCleEtim.edDitta_Validated(edDitta.Text, strTmp) Then
        edDitta.Text = NTSCStr(edDitta.OldEditValue)
      Else
        lbDitta.Text = strTmp
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub opDittetutte_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opDittetutte.CheckedChanged
    Try
      If opDittetutte.Checked Then
        edDitta.Enabled = False
      Else
        GctlSetVisEnab(edDitta, False)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  '------------------------
  Public Overridable Sub cmdSel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSel.Click
    Try
      For z As Integer = 0 To liAlert.ItemCount - 1
        liAlert.SetSelected(z, True)
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdDesel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDesel.Click
    Try
      For z As Integer = 0 To liAlert.ItemCount - 1
        liAlert.SetSelected(z, False)
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


  Public Overridable Function StartBatch() As Boolean
    Try
      oCleEtim.Elabora(True)
      If oCleEtim.bSolo1Giro Then
        Me.Close()
        Return True
      End If

      'primo timer
      nIndice = 0
      Timer1.Interval = 60000

      Timer1.Start()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Function StopBatch() As Boolean
    Try
      Try
        Timer1.Stop()
      Catch ex As Exception
      End Try

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
    Dim i As Integer = 0
    Try
      nIndice = nIndice + 1
      If nIndice = nIntervallo Then
        nIndice = 0
        'Ritenta per 15 minuti
        While oCleEtim.bInElaborazione And i < 900
          System.Threading.Thread.Sleep(1000)
          i += 1
        End While
        If oCleEtim.bInElaborazione Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129207438234789039, "Elaborazione alert saltata, è ancora in corso una elaborazione precedente."))
        Else
          oCleEtim.Elabora(True)
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub NotifyIcon1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseClick
    Dim bBatch As Boolean = False
    Try
      If oCleEtim.bInElaborazione Then Return
      bBatch = oApp.Batch
      oApp.Batch = False    'altrimenti il messaggio viene reindirizzato al file di log e viene preso il valore di default ...
      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128744388072142000, "Sei sicuro di voler terminare l'applicazione?")) = Windows.Forms.DialogResult.Yes Then
        oCleEtim.bInElaborazione = False
        Me.Close()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      oApp.Batch = bBatch
    End Try
  End Sub

  Public Overridable Function GetSelectedAlert() As String
    Try
      Dim strAlert As String = ""
      For z As Integer = 0 To liAlert.SelectedIndices.Count - 1
        strAlert &= NTSCInt(dttAlerts.Rows(liAlert.SelectedIndices(z))!cod) & ","
      Next

      If strAlert.Length = 0 Then Return ""

      Return strAlert.Remove(strAlert.Length - 1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
      Return ""
    End Try
  End Function

  Public Overridable Function StartSocketServer() As Boolean
    Try
      ProcessRequest = Nothing
      ProcessRequest = New Threading.Thread(AddressOf ProcessConnectionRequest)
      ProcessRequest.Start()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub ProcessConnectionRequest()
    'un client (busserv.exe) è appena partito e mi sta avvisando che se voglio posso mandargli gli alert popup che non ha ricevuto quando era chiuso
    'Dim ClientConnection As System.Net.Sockets.TcpClient = Nothing
    'Dim LenBuffer(3) As Byte
    'Dim MessageLength As Integer
    'Dim Buffer() As Byte
    'Dim MessageChars() As Char
    'Dim strPcname As String = ""
    Dim dttTmp As New DataTable
    Dim client As System.Net.Sockets.TcpClient = Nothing
    Dim stream As System.Net.Sockets.NetworkStream = Nothing
    Dim bytes(1024) As Byte
    Dim data As String = Nothing
    Dim i As Integer = 0
    Dim strPcname As String = ""

    Try
      tcpServer.Start()

      While True
        'aspetta che un client si connetta
        client = tcpServer.AcceptTcpClient()
        data = Nothing
        strPcname = ""
        stream = client.GetStream()
        i = stream.Read(bytes, 0, bytes.Length) 'non gestisco messaggi più lunghi di 1024 bytes ... tanto mi viene passato solo il nome del pc
        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i)
        strPcname = data
        client.Close()
        If strPcname.Trim <> "" Then oCleEtim.ProcessConnectionRequest(strPcname.Trim)
      End While

    Catch ex As Threading.ThreadAbortException
      'Ho forzato la chiusura del thread da codice. (probabilmente non usato ma meglio gestirlo)
    Catch ex As Net.Sockets.SocketException
      'Capita costantemente quando uso la tcpServer.Stop()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
End Class
