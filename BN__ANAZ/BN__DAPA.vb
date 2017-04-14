Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__DAPA
  Public oCleAnaz As CLE__ANAZ
  Public dsDapa As DataSet
  Public oCallParams As CLE__CLDP
  Public dcDapa As BindingSource = New BindingSource

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbesci As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbAcs_codvparsg As NTSInformatica.NTSLabel
  Public WithEvents ckAcs_appcomm As NTSInformatica.NTSCheckBox
  Public WithEvents lbAcs_codvpar As NTSInformatica.NTSLabel
  Public WithEvents edAcs_cascom As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAcs_spegen As NTSInformatica.NTSTextBoxNum

  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__DAPA))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbesci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.lbAcs_codvparsg = New NTSInformatica.NTSLabel
    Me.ckAcs_appcomm = New NTSInformatica.NTSCheckBox
    Me.lbAcs_codvpar = New NTSInformatica.NTSLabel
    Me.edAcs_cascom = New NTSInformatica.NTSTextBoxNum
    Me.edAcs_spegen = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_codvpar = New NTSInformatica.NTSLabel
    Me.lbXx_codvparsg = New NTSInformatica.NTSLabel
    Me.lbXx_codvparsa = New NTSInformatica.NTSLabel
    Me.lbAcs_codvparsa = New NTSInformatica.NTSLabel
    Me.edAcs_codrtacp = New NTSInformatica.NTSTextBoxNum
    Me.lbAcs_codrtacp = New NTSInformatica.NTSLabel
    Me.lbAcs_codtpbf = New NTSInformatica.NTSLabel
    Me.edAcs_codtpbf = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_codrtacp = New NTSInformatica.NTSLabel
    Me.lbXx_codtpbf = New NTSInformatica.NTSLabel
    Me.ckAcs_appspegen = New NTSInformatica.NTSCheckBox
    Me.ckAcs_appenas = New NTSInformatica.NTSCheckBox
    Me.edAcs_codvparsa = New NTSInformatica.NTSTextBoxStr
    Me.edAcs_codvparsg = New NTSInformatica.NTSTextBoxStr
    Me.edAcs_codvpar = New NTSInformatica.NTSTextBoxStr
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAcs_appcomm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAcs_cascom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAcs_spegen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAcs_codrtacp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAcs_codtpbf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAcs_appspegen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAcs_appenas.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAcs_codvparsa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAcs_codvparsg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAcs_codvpar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'DevXDefaultLookAndFeel
    '
    
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbRipristina, Me.tlbZoom, Me.tlbGuida, Me.tlbesci})
    Me.NtsBarManager1.MaxItemId = 26
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbesci)})
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
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 4
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 18
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbesci
    '
    Me.tlbesci.Caption = "Esci"
    Me.tlbesci.Glyph = CType(resources.GetObject("tlbesci.Glyph"), System.Drawing.Image)
    Me.tlbesci.Id = 19
    Me.tlbesci.Name = "tlbesci"
    Me.tlbesci.Visible = True
    '
    'lbAcs_codvparsg
    '
    Me.lbAcs_codvparsg.AutoSize = True
    Me.lbAcs_codvparsg.BackColor = System.Drawing.Color.Transparent
    Me.lbAcs_codvparsg.Location = New System.Drawing.Point(9, 62)
    Me.lbAcs_codvparsg.Name = "lbAcs_codvparsg"
    Me.lbAcs_codvparsg.NTSDbField = ""
    Me.lbAcs_codvparsg.Size = New System.Drawing.Size(171, 13)
    Me.lbAcs_codvparsg.TabIndex = 18
    Me.lbAcs_codvparsg.Text = "Voce parcellazione 'spese generali'"
    '
    'ckAcs_appcomm
    '
    Me.ckAcs_appcomm.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAcs_appcomm.Location = New System.Drawing.Point(12, 178)
    Me.ckAcs_appcomm.Name = "ckAcs_appcomm"
    Me.ckAcs_appcomm.NTSCheckValue = "S"
    Me.ckAcs_appcomm.NTSUnCheckValue = "N"
    Me.ckAcs_appcomm.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAcs_appcomm.Properties.Appearance.Options.UseBackColor = True
    Me.ckAcs_appcomm.Properties.Caption = "Applica cassa commercialisti (percentuale):"
    Me.ckAcs_appcomm.Size = New System.Drawing.Size(230, 18)
    Me.ckAcs_appcomm.TabIndex = 513
    '
    'lbAcs_codvpar
    '
    Me.lbAcs_codvpar.AutoSize = True
    Me.lbAcs_codvpar.BackColor = System.Drawing.Color.Transparent
    Me.lbAcs_codvpar.Location = New System.Drawing.Point(9, 39)
    Me.lbAcs_codvpar.Name = "lbAcs_codvpar"
    Me.lbAcs_codvpar.NTSDbField = ""
    Me.lbAcs_codvpar.Size = New System.Drawing.Size(197, 13)
    Me.lbAcs_codvpar.TabIndex = 24
    Me.lbAcs_codvpar.Text = "Voce parcellazione 'cassa commercialisti'"
    '
    'edAcs_cascom
    '
    Me.edAcs_cascom.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAcs_cascom.EditValue = "0"
    Me.edAcs_cascom.Location = New System.Drawing.Point(245, 176)
    Me.edAcs_cascom.Name = "edAcs_cascom"
    Me.edAcs_cascom.NTSDbField = ""
    Me.edAcs_cascom.NTSFormat = "0"
    Me.edAcs_cascom.NTSForzaVisZoom = False
    Me.edAcs_cascom.NTSOldValue = ""
    Me.edAcs_cascom.Properties.Appearance.Options.UseTextOptions = True
    Me.edAcs_cascom.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAcs_cascom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAcs_cascom.Properties.MaxLength = 65536
    Me.edAcs_cascom.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAcs_cascom.Size = New System.Drawing.Size(76, 20)
    Me.edAcs_cascom.TabIndex = 517
    '
    'edAcs_spegen
    '
    Me.edAcs_spegen.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAcs_spegen.EditValue = "0"
    Me.edAcs_spegen.Location = New System.Drawing.Point(245, 202)
    Me.edAcs_spegen.Name = "edAcs_spegen"
    Me.edAcs_spegen.NTSDbField = ""
    Me.edAcs_spegen.NTSFormat = "0"
    Me.edAcs_spegen.NTSForzaVisZoom = False
    Me.edAcs_spegen.NTSOldValue = ""
    Me.edAcs_spegen.Properties.Appearance.Options.UseTextOptions = True
    Me.edAcs_spegen.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAcs_spegen.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAcs_spegen.Properties.MaxLength = 65536
    Me.edAcs_spegen.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAcs_spegen.Size = New System.Drawing.Size(76, 20)
    Me.edAcs_spegen.TabIndex = 518
    '
    'lbXx_codvpar
    '
    Me.lbXx_codvpar.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codvpar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codvpar.Location = New System.Drawing.Point(326, 36)
    Me.lbXx_codvpar.Name = "lbXx_codvpar"
    Me.lbXx_codvpar.NTSDbField = ""
    Me.lbXx_codvpar.Size = New System.Drawing.Size(215, 20)
    Me.lbXx_codvpar.TabIndex = 581
    Me.lbXx_codvpar.Text = "lbXx_codvpar"
    '
    'lbXx_codvparsg
    '
    Me.lbXx_codvparsg.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codvparsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codvparsg.Location = New System.Drawing.Point(326, 62)
    Me.lbXx_codvparsg.Name = "lbXx_codvparsg"
    Me.lbXx_codvparsg.NTSDbField = ""
    Me.lbXx_codvparsg.Size = New System.Drawing.Size(215, 20)
    Me.lbXx_codvparsg.TabIndex = 582
    Me.lbXx_codvparsg.Text = "lbXx_codvparsg"
    '
    'lbXx_codvparsa
    '
    Me.lbXx_codvparsa.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codvparsa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codvparsa.Location = New System.Drawing.Point(326, 88)
    Me.lbXx_codvparsa.Name = "lbXx_codvparsa"
    Me.lbXx_codvparsa.NTSDbField = ""
    Me.lbXx_codvparsa.Size = New System.Drawing.Size(215, 20)
    Me.lbXx_codvparsa.TabIndex = 585
    Me.lbXx_codvparsa.Text = "lbXx_codvparsa"
    '
    'lbAcs_codvparsa
    '
    Me.lbAcs_codvparsa.AutoSize = True
    Me.lbAcs_codvparsa.BackColor = System.Drawing.Color.Transparent
    Me.lbAcs_codvparsa.Location = New System.Drawing.Point(9, 88)
    Me.lbAcs_codvparsa.Name = "lbAcs_codvparsa"
    Me.lbAcs_codvparsa.NTSDbField = ""
    Me.lbAcs_codvparsa.Size = New System.Drawing.Size(170, 13)
    Me.lbAcs_codvparsa.TabIndex = 583
    Me.lbAcs_codvparsa.Text = "Voce parcellazione 'storno acconti'"
    '
    'edAcs_codrtacp
    '
    Me.edAcs_codrtacp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAcs_codrtacp.EditValue = "0"
    Me.edAcs_codrtacp.Location = New System.Drawing.Point(245, 140)
    Me.edAcs_codrtacp.Name = "edAcs_codrtacp"
    Me.edAcs_codrtacp.NTSDbField = ""
    Me.edAcs_codrtacp.NTSFormat = "0"
    Me.edAcs_codrtacp.NTSForzaVisZoom = False
    Me.edAcs_codrtacp.NTSOldValue = ""
    Me.edAcs_codrtacp.Properties.Appearance.Options.UseTextOptions = True
    Me.edAcs_codrtacp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAcs_codrtacp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAcs_codrtacp.Properties.MaxLength = 65536
    Me.edAcs_codrtacp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAcs_codrtacp.Size = New System.Drawing.Size(76, 20)
    Me.edAcs_codrtacp.TabIndex = 588
    '
    'lbAcs_codrtacp
    '
    Me.lbAcs_codrtacp.AutoSize = True
    Me.lbAcs_codrtacp.BackColor = System.Drawing.Color.Transparent
    Me.lbAcs_codrtacp.Location = New System.Drawing.Point(9, 143)
    Me.lbAcs_codrtacp.Name = "lbAcs_codrtacp"
    Me.lbAcs_codrtacp.NTSDbField = ""
    Me.lbAcs_codrtacp.Size = New System.Drawing.Size(205, 13)
    Me.lbAcs_codrtacp.TabIndex = 586
    Me.lbAcs_codrtacp.Text = "C. assog. riten. acconto per clienti privati"
    '
    'lbAcs_codtpbf
    '
    Me.lbAcs_codtpbf.AutoSize = True
    Me.lbAcs_codtpbf.BackColor = System.Drawing.Color.Transparent
    Me.lbAcs_codtpbf.Location = New System.Drawing.Point(9, 117)
    Me.lbAcs_codtpbf.Name = "lbAcs_codtpbf"
    Me.lbAcs_codtpbf.NTSDbField = ""
    Me.lbAcs_codtpbf.Size = New System.Drawing.Size(145, 13)
    Me.lbAcs_codtpbf.TabIndex = 587
    Me.lbAcs_codtpbf.Text = "Tipo bolla/fattura predefinito"
    '
    'edAcs_codtpbf
    '
    Me.edAcs_codtpbf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAcs_codtpbf.EditValue = "0"
    Me.edAcs_codtpbf.Location = New System.Drawing.Point(245, 114)
    Me.edAcs_codtpbf.Name = "edAcs_codtpbf"
    Me.edAcs_codtpbf.NTSDbField = ""
    Me.edAcs_codtpbf.NTSFormat = "0"
    Me.edAcs_codtpbf.NTSForzaVisZoom = False
    Me.edAcs_codtpbf.NTSOldValue = ""
    Me.edAcs_codtpbf.Properties.Appearance.Options.UseTextOptions = True
    Me.edAcs_codtpbf.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAcs_codtpbf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAcs_codtpbf.Properties.MaxLength = 65536
    Me.edAcs_codtpbf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAcs_codtpbf.Size = New System.Drawing.Size(76, 20)
    Me.edAcs_codtpbf.TabIndex = 589
    '
    'lbXx_codrtacp
    '
    Me.lbXx_codrtacp.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codrtacp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codrtacp.Location = New System.Drawing.Point(326, 140)
    Me.lbXx_codrtacp.Name = "lbXx_codrtacp"
    Me.lbXx_codrtacp.NTSDbField = ""
    Me.lbXx_codrtacp.Size = New System.Drawing.Size(215, 20)
    Me.lbXx_codrtacp.TabIndex = 590
    Me.lbXx_codrtacp.Text = "lbXx_codrtacp"
    '
    'lbXx_codtpbf
    '
    Me.lbXx_codtpbf.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codtpbf.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codtpbf.Location = New System.Drawing.Point(326, 114)
    Me.lbXx_codtpbf.Name = "lbXx_codtpbf"
    Me.lbXx_codtpbf.NTSDbField = ""
    Me.lbXx_codtpbf.Size = New System.Drawing.Size(215, 20)
    Me.lbXx_codtpbf.TabIndex = 591
    Me.lbXx_codtpbf.Text = "lbXx_codtpbf"
    '
    'ckAcs_appspegen
    '
    Me.ckAcs_appspegen.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAcs_appspegen.Location = New System.Drawing.Point(12, 204)
    Me.ckAcs_appspegen.Name = "ckAcs_appspegen"
    Me.ckAcs_appspegen.NTSCheckValue = "S"
    Me.ckAcs_appspegen.NTSUnCheckValue = "N"
    Me.ckAcs_appspegen.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAcs_appspegen.Properties.Appearance.Options.UseBackColor = True
    Me.ckAcs_appspegen.Properties.Caption = "Applica spese generali (percentuale):"
    Me.ckAcs_appspegen.Size = New System.Drawing.Size(202, 18)
    Me.ckAcs_appspegen.TabIndex = 592
    '
    'ckAcs_appenas
    '
    Me.ckAcs_appenas.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAcs_appenas.Location = New System.Drawing.Point(12, 228)
    Me.ckAcs_appenas.Name = "ckAcs_appenas"
    Me.ckAcs_appenas.NTSCheckValue = "S"
    Me.ckAcs_appenas.NTSUnCheckValue = "N"
    Me.ckAcs_appenas.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAcs_appenas.Properties.Appearance.Options.UseBackColor = True
    Me.ckAcs_appenas.Properties.Caption = "Applica enasarco"
    Me.ckAcs_appenas.Size = New System.Drawing.Size(111, 18)
    Me.ckAcs_appenas.TabIndex = 593
    '
    'edAcs_codvparsa
    '
    Me.edAcs_codvparsa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAcs_codvparsa.Location = New System.Drawing.Point(212, 88)
    Me.edAcs_codvparsa.Name = "edAcs_codvparsa"
    Me.edAcs_codvparsa.NTSDbField = ""
    Me.edAcs_codvparsa.NTSForzaVisZoom = False
    Me.edAcs_codvparsa.NTSOldValue = ""
    Me.edAcs_codvparsa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAcs_codvparsa.Properties.MaxLength = 65536
    Me.edAcs_codvparsa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAcs_codvparsa.Size = New System.Drawing.Size(109, 20)
    Me.edAcs_codvparsa.TabIndex = 594
    '
    'edAcs_codvparsg
    '
    Me.edAcs_codvparsg.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAcs_codvparsg.Location = New System.Drawing.Point(212, 62)
    Me.edAcs_codvparsg.Name = "edAcs_codvparsg"
    Me.edAcs_codvparsg.NTSDbField = ""
    Me.edAcs_codvparsg.NTSForzaVisZoom = False
    Me.edAcs_codvparsg.NTSOldValue = ""
    Me.edAcs_codvparsg.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAcs_codvparsg.Properties.MaxLength = 65536
    Me.edAcs_codvparsg.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAcs_codvparsg.Size = New System.Drawing.Size(109, 20)
    Me.edAcs_codvparsg.TabIndex = 595
    '
    'edAcs_codvpar
    '
    Me.edAcs_codvpar.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAcs_codvpar.Location = New System.Drawing.Point(212, 36)
    Me.edAcs_codvpar.Name = "edAcs_codvpar"
    Me.edAcs_codvpar.NTSDbField = ""
    Me.edAcs_codvpar.NTSForzaVisZoom = False
    Me.edAcs_codvpar.NTSOldValue = ""
    Me.edAcs_codvpar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAcs_codvpar.Properties.MaxLength = 65536
    Me.edAcs_codvpar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAcs_codvpar.Size = New System.Drawing.Size(109, 20)
    Me.edAcs_codvpar.TabIndex = 596
    '
    'FRM__DAPA
    '
    Me.ClientSize = New System.Drawing.Size(548, 255)
    Me.Controls.Add(Me.edAcs_codvpar)
    Me.Controls.Add(Me.edAcs_codvparsg)
    Me.Controls.Add(Me.edAcs_codvparsa)
    Me.Controls.Add(Me.ckAcs_appenas)
    Me.Controls.Add(Me.ckAcs_appspegen)
    Me.Controls.Add(Me.lbXx_codtpbf)
    Me.Controls.Add(Me.lbXx_codrtacp)
    Me.Controls.Add(Me.edAcs_codrtacp)
    Me.Controls.Add(Me.lbAcs_codrtacp)
    Me.Controls.Add(Me.lbAcs_codtpbf)
    Me.Controls.Add(Me.edAcs_codtpbf)
    Me.Controls.Add(Me.lbXx_codvparsa)
    Me.Controls.Add(Me.lbAcs_codvparsa)
    Me.Controls.Add(Me.lbXx_codvparsg)
    Me.Controls.Add(Me.lbXx_codvpar)
    Me.Controls.Add(Me.lbAcs_codvparsg)
    Me.Controls.Add(Me.edAcs_cascom)
    Me.Controls.Add(Me.lbAcs_codvpar)
    Me.Controls.Add(Me.edAcs_spegen)
    Me.Controls.Add(Me.ckAcs_appcomm)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__DAPA"
    Me.Text = "DATI AGGIUNTIVI PARCELLAZIONE"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAcs_appcomm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAcs_cascom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAcs_spegen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAcs_codrtacp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAcs_codtpbf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAcs_appspegen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAcs_appenas.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAcs_codvparsa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAcs_codvparsg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAcs_codvpar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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

    Return True
  End Function


  Public Overridable Sub InitEntity(ByRef cleAnaz As CLE__ANAZ)
    oCleAnaz = cleAnaz
    AddHandler oCleAnaz.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim i As Integer = 0
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbesci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'Ã¨ una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      '-------------------------------------------------
      'completo le informazioni dei controlli
      edAcs_codvpar.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128647372142656250, "Voce di parcellazione 'Cassa commercialisti'"), tabvpar, False)
      edAcs_codvparsg.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128647372142812500, "Voce di parcellazione 'Spese generali'"), tabvpar, False)
      edAcs_codvparsa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128647372142968750, "Voce di parcellazione 'Storno acconti'"), tabvpar, False)
      ckAcs_appenas.NTSSetParam(oMenu, oApp.Tr(Me, 128647372143125000, "Applica enasarco"), "S", "N")
      ckAcs_appspegen.NTSSetParam(oMenu, oApp.Tr(Me, 128647372143281250, "Applica spese generali"), "S", "N")
      edAcs_codrtacp.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128647372143750000, "Cod. assog. rit. acc. a privati"), tabrtac)
      edAcs_codtpbf.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128647372144218750, "Tipo bolla/fattura"), tabtpbf)
      edAcs_cascom.NTSSetParam(oMenu, oApp.Tr(Me, 128647372145156250, "Percentuale cassa commercialisti"), oApp.FormatSconti, 6, 0, 100)
      edAcs_spegen.NTSSetParam(oMenu, oApp.Tr(Me, 128647372145468750, "Percentuale spese generali"), oApp.FormatSconti, 6, 0, 100)
      ckAcs_appcomm.NTSSetParam(oMenu, oApp.Tr(Me, 128647372145625000, "Applica cassa commercialisti"), "S", "N")

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
      edAcs_codvpar.NTSDbField = "ANADITPA.acs_codvpar"
      edAcs_codvparsg.NTSDbField = "ANADITPA.acs_codvparsg"
      edAcs_codvparsa.NTSDbField = "ANADITPA.acs_codvparsa"
      ckAcs_appenas.NTSText.NTSDbField = "ANADITPA.acs_appenas"
      ckAcs_appspegen.NTSText.NTSDbField = "ANADITPA.acs_appspegen"
      lbXx_codtpbf.NTSDbField = "ANADITPA.xx_codtpbf"
      lbXx_codrtacp.NTSDbField = "ANADITPA.xx_codrtacp"
      edAcs_codrtacp.NTSDbField = "ANADITPA.acs_codrtacp"
      edAcs_codtpbf.NTSDbField = "ANADITPA.acs_codtpbf"
      lbXx_codvparsa.NTSDbField = "ANADITPA.xx_codvparsa"
      lbXx_codvparsg.NTSDbField = "ANADITPA.xx_codvparsg"
      lbXx_codvpar.NTSDbField = "ANADITPA.xx_codvpar"
      edAcs_cascom.NTSDbField = "ANADITPA.acs_cascom"
      edAcs_spegen.NTSDbField = "ANADITPA.acs_spegen"
      ckAcs_appcomm.NTSText.NTSDbField = "ANADITPA.acs_appcomm"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcDapa, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRM__DAPA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      dsDapa = oCleAnaz.dsShared
      dcDapa.DataSource = dsDapa.Tables("ANADITPA")
      dsDapa.Tables("ANADITPA").AcceptChanges()

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      ckAcs_appcomm_CheckedChanged(ckAcs_appcomm, Nothing)
      ckAcs_appspegen_CheckedChanged(ckAcs_appspegen, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__DAPA_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

#End Region

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
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129006943815968817, "Ripristinare le modifiche apportate?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          If dsDapa.Tables("ANADITPA").Rows.Count = 1 And dsDapa.Tables("ANADITPA").Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          oCleAnaz.DapaRipristina(dcDapa.Position, dcDapa.Filter)
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcDapa, Me)
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

      '------------------------------------
      'zoom standard di textbox e griglia
      NTSCallStandardZoom()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
  End Sub

  Public Overridable Sub tlbesci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbesci.ItemClick
    Me.Close()
  End Sub
#End Region

  Public Overridable Function Salva() As Boolean
    Dim dRes As DialogResult
    Try
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      If oCleAnaz.DapaRecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129006943969722753, "Confermi il salvataggio?"))
        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oCleAnaz.DapaSalva(False) Then Return False
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

  Public Overridable Sub ckAcs_appcomm_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckAcs_appcomm.CheckedChanged
    Try
      If ckAcs_appcomm.Checked Then
        GctlSetVisEnab(edAcs_cascom, False)
      Else
        edAcs_cascom.Text = "0"
        edAcs_cascom.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckAcs_appspegen_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckAcs_appspegen.CheckedChanged
    Try
      If ckAcs_appspegen.Checked Then
        GctlSetVisEnab(edAcs_spegen, False)
      Else
        edAcs_spegen.Text = "0"
        edAcs_spegen.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
End Class

