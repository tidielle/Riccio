Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__DESD
  Public oCleAnaz As CLE__ANAZ
  Public dsDesg As DataSet
  Public oCallParams As CLE__CLDP
  Public dcDesg As BindingSource = New BindingSource

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbUl_coddest As NTSInformatica.NTSLabel
  Public WithEvents edUl_coddest As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbUl_nomdest As NTSInformatica.NTSLabel
  Public WithEvents edUl_nomdest As NTSInformatica.NTSTextBoxStr
  Public WithEvents edUl_nomdest2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbUl_inddest As NTSInformatica.NTSLabel
  Public WithEvents edUl_inddest As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbUl_capdest As NTSInformatica.NTSLabel
  Public WithEvents edUl_capdest As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbUl_locdest As NTSInformatica.NTSLabel
  Public WithEvents edUl_locdest As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbUl_prodest As NTSInformatica.NTSLabel
  Public WithEvents edUl_prodest As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbUl_turno As NTSInformatica.NTSLabel
  Public WithEvents edUl_turno As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbUl_telef As NTSInformatica.NTSLabel
  Public WithEvents edUl_telef As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbUl_faxtlx As NTSInformatica.NTSLabel
  Public WithEvents edUl_faxtlx As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbUl_email As NTSInformatica.NTSLabel
  Public WithEvents edUl_email As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbUl_usaem As NTSInformatica.NTSLabel
  Public WithEvents cbUl_usaem As NTSInformatica.NTSComboBox
  Public WithEvents lbUl_stato As NTSInformatica.NTSLabel
  Public WithEvents edUl_stato As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbUl_codcomu As NTSInformatica.NTSLabel
  Public WithEvents edUl_codcomu As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbUl_statofed As NTSInformatica.NTSLabel
  Public WithEvents edUl_statofed As NTSInformatica.NTSTextBoxStr

  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__DESD))
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
    Me.lbUl_coddest = New NTSInformatica.NTSLabel
    Me.edUl_coddest = New NTSInformatica.NTSTextBoxNum
    Me.lbUl_nomdest = New NTSInformatica.NTSLabel
    Me.edUl_nomdest = New NTSInformatica.NTSTextBoxStr
    Me.edUl_nomdest2 = New NTSInformatica.NTSTextBoxStr
    Me.lbUl_inddest = New NTSInformatica.NTSLabel
    Me.edUl_inddest = New NTSInformatica.NTSTextBoxStr
    Me.lbUl_capdest = New NTSInformatica.NTSLabel
    Me.edUl_capdest = New NTSInformatica.NTSTextBoxStr
    Me.lbUl_locdest = New NTSInformatica.NTSLabel
    Me.edUl_locdest = New NTSInformatica.NTSTextBoxStr
    Me.lbUl_prodest = New NTSInformatica.NTSLabel
    Me.edUl_prodest = New NTSInformatica.NTSTextBoxStr
    Me.lbUl_turno = New NTSInformatica.NTSLabel
    Me.edUl_turno = New NTSInformatica.NTSTextBoxStr
    Me.lbUl_telef = New NTSInformatica.NTSLabel
    Me.edUl_telef = New NTSInformatica.NTSTextBoxStr
    Me.lbUl_faxtlx = New NTSInformatica.NTSLabel
    Me.edUl_faxtlx = New NTSInformatica.NTSTextBoxStr
    Me.lbUl_email = New NTSInformatica.NTSLabel
    Me.edUl_email = New NTSInformatica.NTSTextBoxStr
    Me.lbUl_usaem = New NTSInformatica.NTSLabel
    Me.cbUl_usaem = New NTSInformatica.NTSComboBox
    Me.lbUl_stato = New NTSInformatica.NTSLabel
    Me.edUl_stato = New NTSInformatica.NTSTextBoxStr
    Me.lbUl_codcomu = New NTSInformatica.NTSLabel
    Me.edUl_codcomu = New NTSInformatica.NTSTextBoxStr
    Me.lbUl_statofed = New NTSInformatica.NTSLabel
    Me.edUl_statofed = New NTSInformatica.NTSTextBoxStr
    Me.pnSx = New NTSInformatica.NTSPanel
    Me.lbXx_codcomu = New NTSInformatica.NTSLabel
    Me.lbXx_stato = New NTSInformatica.NTSLabel
    Me.lbTitle = New NTSInformatica.NTSLabel
    Me.pnDx = New NTSInformatica.NTSPanel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edUl_coddest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edUl_nomdest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edUl_nomdest2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edUl_inddest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edUl_capdest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edUl_locdest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edUl_prodest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edUl_turno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edUl_telef.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edUl_faxtlx.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edUl_email.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbUl_usaem.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edUl_stato.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edUl_codcomu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edUl_statofed.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnSx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSx.SuspendLayout()
    CType(Me.pnDx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDx.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbRipristina, Me.tlbZoom, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbGuida, Me.tlbEsci, Me.tlbUltimo, Me.tlbCancella})
    Me.NtsBarManager1.MaxItemId = 27
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbCancella.Id = 26
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
    Me.tlbUltimo.Id = 20
    Me.tlbUltimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
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
    'lbUl_coddest
    '
    Me.lbUl_coddest.AutoSize = True
    Me.lbUl_coddest.BackColor = System.Drawing.Color.Transparent
    Me.lbUl_coddest.Location = New System.Drawing.Point(4, 31)
    Me.lbUl_coddest.Name = "lbUl_coddest"
    Me.lbUl_coddest.NTSDbField = ""
    Me.lbUl_coddest.Size = New System.Drawing.Size(39, 13)
    Me.lbUl_coddest.TabIndex = 10
    Me.lbUl_coddest.Text = "Codice"
    Me.lbUl_coddest.UseMnemonic = False
    '
    'edUl_coddest
    '
    Me.edUl_coddest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edUl_coddest.EditValue = "0"
    Me.edUl_coddest.Location = New System.Drawing.Point(103, 28)
    Me.edUl_coddest.Name = "edUl_coddest"
    Me.edUl_coddest.NTSDbField = ""
    Me.edUl_coddest.NTSFormat = "0"
    Me.edUl_coddest.NTSForzaVisZoom = False
    Me.edUl_coddest.NTSOldValue = ""
    Me.edUl_coddest.Properties.Appearance.Options.UseTextOptions = True
    Me.edUl_coddest.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edUl_coddest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUl_coddest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUl_coddest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUl_coddest.Properties.MaxLength = 65536
    Me.edUl_coddest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUl_coddest.Size = New System.Drawing.Size(62, 20)
    Me.edUl_coddest.TabIndex = 500
    '
    'lbUl_nomdest
    '
    Me.lbUl_nomdest.AutoSize = True
    Me.lbUl_nomdest.BackColor = System.Drawing.Color.Transparent
    Me.lbUl_nomdest.Location = New System.Drawing.Point(4, 60)
    Me.lbUl_nomdest.Name = "lbUl_nomdest"
    Me.lbUl_nomdest.NTSDbField = ""
    Me.lbUl_nomdest.Size = New System.Drawing.Size(61, 13)
    Me.lbUl_nomdest.TabIndex = 13
    Me.lbUl_nomdest.Text = "Descrizione"
    Me.lbUl_nomdest.UseMnemonic = False
    '
    'edUl_nomdest
    '
    Me.edUl_nomdest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edUl_nomdest.Location = New System.Drawing.Point(103, 57)
    Me.edUl_nomdest.Name = "edUl_nomdest"
    Me.edUl_nomdest.NTSDbField = ""
    Me.edUl_nomdest.NTSForzaVisZoom = False
    Me.edUl_nomdest.NTSOldValue = ""
    Me.edUl_nomdest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUl_nomdest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUl_nomdest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUl_nomdest.Properties.MaxLength = 65536
    Me.edUl_nomdest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUl_nomdest.Size = New System.Drawing.Size(278, 20)
    Me.edUl_nomdest.TabIndex = 503
    '
    'edUl_nomdest2
    '
    Me.edUl_nomdest2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edUl_nomdest2.Location = New System.Drawing.Point(103, 83)
    Me.edUl_nomdest2.Name = "edUl_nomdest2"
    Me.edUl_nomdest2.NTSDbField = ""
    Me.edUl_nomdest2.NTSForzaVisZoom = False
    Me.edUl_nomdest2.NTSOldValue = ""
    Me.edUl_nomdest2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUl_nomdest2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUl_nomdest2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUl_nomdest2.Properties.MaxLength = 65536
    Me.edUl_nomdest2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUl_nomdest2.Size = New System.Drawing.Size(278, 20)
    Me.edUl_nomdest2.TabIndex = 504
    '
    'lbUl_inddest
    '
    Me.lbUl_inddest.AutoSize = True
    Me.lbUl_inddest.BackColor = System.Drawing.Color.Transparent
    Me.lbUl_inddest.Location = New System.Drawing.Point(4, 112)
    Me.lbUl_inddest.Name = "lbUl_inddest"
    Me.lbUl_inddest.NTSDbField = ""
    Me.lbUl_inddest.Size = New System.Drawing.Size(47, 13)
    Me.lbUl_inddest.TabIndex = 15
    Me.lbUl_inddest.Text = "Indirizzo"
    Me.lbUl_inddest.UseMnemonic = False
    '
    'edUl_inddest
    '
    Me.edUl_inddest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edUl_inddest.Location = New System.Drawing.Point(103, 109)
    Me.edUl_inddest.Name = "edUl_inddest"
    Me.edUl_inddest.NTSDbField = ""
    Me.edUl_inddest.NTSForzaVisZoom = False
    Me.edUl_inddest.NTSOldValue = ""
    Me.edUl_inddest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUl_inddest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUl_inddest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUl_inddest.Properties.MaxLength = 65536
    Me.edUl_inddest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUl_inddest.Size = New System.Drawing.Size(278, 20)
    Me.edUl_inddest.TabIndex = 505
    '
    'lbUl_capdest
    '
    Me.lbUl_capdest.AutoSize = True
    Me.lbUl_capdest.BackColor = System.Drawing.Color.Transparent
    Me.lbUl_capdest.Location = New System.Drawing.Point(4, 164)
    Me.lbUl_capdest.Name = "lbUl_capdest"
    Me.lbUl_capdest.NTSDbField = ""
    Me.lbUl_capdest.Size = New System.Drawing.Size(26, 13)
    Me.lbUl_capdest.TabIndex = 16
    Me.lbUl_capdest.Text = "Cap"
    Me.lbUl_capdest.UseMnemonic = False
    '
    'edUl_capdest
    '
    Me.edUl_capdest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edUl_capdest.Location = New System.Drawing.Point(103, 187)
    Me.edUl_capdest.Name = "edUl_capdest"
    Me.edUl_capdest.NTSDbField = ""
    Me.edUl_capdest.NTSForzaVisZoom = False
    Me.edUl_capdest.NTSOldValue = ""
    Me.edUl_capdest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUl_capdest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUl_capdest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUl_capdest.Properties.MaxLength = 65536
    Me.edUl_capdest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUl_capdest.Size = New System.Drawing.Size(62, 20)
    Me.edUl_capdest.TabIndex = 506
    '
    'lbUl_locdest
    '
    Me.lbUl_locdest.AutoSize = True
    Me.lbUl_locdest.BackColor = System.Drawing.Color.Transparent
    Me.lbUl_locdest.Location = New System.Drawing.Point(4, 164)
    Me.lbUl_locdest.Name = "lbUl_locdest"
    Me.lbUl_locdest.NTSDbField = ""
    Me.lbUl_locdest.Size = New System.Drawing.Size(67, 13)
    Me.lbUl_locdest.TabIndex = 17
    Me.lbUl_locdest.Text = "Città/località"
    Me.lbUl_locdest.UseMnemonic = False
    '
    'edUl_locdest
    '
    Me.edUl_locdest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edUl_locdest.Location = New System.Drawing.Point(103, 161)
    Me.edUl_locdest.Name = "edUl_locdest"
    Me.edUl_locdest.NTSDbField = ""
    Me.edUl_locdest.NTSForzaVisZoom = False
    Me.edUl_locdest.NTSOldValue = ""
    Me.edUl_locdest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUl_locdest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUl_locdest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUl_locdest.Properties.MaxLength = 65536
    Me.edUl_locdest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUl_locdest.Size = New System.Drawing.Size(278, 20)
    Me.edUl_locdest.TabIndex = 507
    '
    'lbUl_prodest
    '
    Me.lbUl_prodest.AutoSize = True
    Me.lbUl_prodest.BackColor = System.Drawing.Color.Transparent
    Me.lbUl_prodest.Location = New System.Drawing.Point(171, 190)
    Me.lbUl_prodest.Name = "lbUl_prodest"
    Me.lbUl_prodest.NTSDbField = ""
    Me.lbUl_prodest.Size = New System.Drawing.Size(50, 13)
    Me.lbUl_prodest.TabIndex = 18
    Me.lbUl_prodest.Text = "Provincia"
    Me.lbUl_prodest.UseMnemonic = False
    '
    'edUl_prodest
    '
    Me.edUl_prodest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edUl_prodest.Location = New System.Drawing.Point(330, 187)
    Me.edUl_prodest.Name = "edUl_prodest"
    Me.edUl_prodest.NTSDbField = ""
    Me.edUl_prodest.NTSForzaVisZoom = False
    Me.edUl_prodest.NTSOldValue = ""
    Me.edUl_prodest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUl_prodest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUl_prodest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUl_prodest.Properties.MaxLength = 65536
    Me.edUl_prodest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUl_prodest.Size = New System.Drawing.Size(51, 20)
    Me.edUl_prodest.TabIndex = 508
    '
    'lbUl_turno
    '
    Me.lbUl_turno.AutoSize = True
    Me.lbUl_turno.BackColor = System.Drawing.Color.Transparent
    Me.lbUl_turno.Location = New System.Drawing.Point(3, 164)
    Me.lbUl_turno.Name = "lbUl_turno"
    Me.lbUl_turno.NTSDbField = ""
    Me.lbUl_turno.Size = New System.Drawing.Size(89, 13)
    Me.lbUl_turno.TabIndex = 19
    Me.lbUl_turno.Text = "Turno di chiusura"
    Me.lbUl_turno.UseMnemonic = False
    '
    'edUl_turno
    '
    Me.edUl_turno.Cursor = System.Windows.Forms.Cursors.Default
    Me.edUl_turno.Location = New System.Drawing.Point(143, 161)
    Me.edUl_turno.Name = "edUl_turno"
    Me.edUl_turno.NTSDbField = ""
    Me.edUl_turno.NTSForzaVisZoom = False
    Me.edUl_turno.NTSOldValue = ""
    Me.edUl_turno.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUl_turno.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUl_turno.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUl_turno.Properties.MaxLength = 65536
    Me.edUl_turno.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUl_turno.Size = New System.Drawing.Size(125, 20)
    Me.edUl_turno.TabIndex = 509
    '
    'lbUl_telef
    '
    Me.lbUl_telef.AutoSize = True
    Me.lbUl_telef.BackColor = System.Drawing.Color.Transparent
    Me.lbUl_telef.Location = New System.Drawing.Point(3, 60)
    Me.lbUl_telef.Name = "lbUl_telef"
    Me.lbUl_telef.NTSDbField = ""
    Me.lbUl_telef.Size = New System.Drawing.Size(49, 13)
    Me.lbUl_telef.TabIndex = 20
    Me.lbUl_telef.Text = "Telefono"
    Me.lbUl_telef.UseMnemonic = False
    '
    'edUl_telef
    '
    Me.edUl_telef.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edUl_telef.Location = New System.Drawing.Point(143, 57)
    Me.edUl_telef.Name = "edUl_telef"
    Me.edUl_telef.NTSDbField = ""
    Me.edUl_telef.NTSForzaVisZoom = False
    Me.edUl_telef.NTSOldValue = ""
    Me.edUl_telef.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUl_telef.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUl_telef.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUl_telef.Properties.MaxLength = 65536
    Me.edUl_telef.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUl_telef.Size = New System.Drawing.Size(125, 20)
    Me.edUl_telef.TabIndex = 510
    '
    'lbUl_faxtlx
    '
    Me.lbUl_faxtlx.AutoSize = True
    Me.lbUl_faxtlx.BackColor = System.Drawing.Color.Transparent
    Me.lbUl_faxtlx.Location = New System.Drawing.Point(3, 86)
    Me.lbUl_faxtlx.Name = "lbUl_faxtlx"
    Me.lbUl_faxtlx.NTSDbField = ""
    Me.lbUl_faxtlx.Size = New System.Drawing.Size(25, 13)
    Me.lbUl_faxtlx.TabIndex = 23
    Me.lbUl_faxtlx.Text = "Fax"
    Me.lbUl_faxtlx.UseMnemonic = False
    '
    'edUl_faxtlx
    '
    Me.edUl_faxtlx.Cursor = System.Windows.Forms.Cursors.Default
    Me.edUl_faxtlx.Location = New System.Drawing.Point(143, 83)
    Me.edUl_faxtlx.Name = "edUl_faxtlx"
    Me.edUl_faxtlx.NTSDbField = ""
    Me.edUl_faxtlx.NTSForzaVisZoom = False
    Me.edUl_faxtlx.NTSOldValue = ""
    Me.edUl_faxtlx.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUl_faxtlx.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUl_faxtlx.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUl_faxtlx.Properties.MaxLength = 65536
    Me.edUl_faxtlx.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUl_faxtlx.Size = New System.Drawing.Size(125, 20)
    Me.edUl_faxtlx.TabIndex = 513
    '
    'lbUl_email
    '
    Me.lbUl_email.AutoSize = True
    Me.lbUl_email.BackColor = System.Drawing.Color.Transparent
    Me.lbUl_email.Location = New System.Drawing.Point(3, 112)
    Me.lbUl_email.Name = "lbUl_email"
    Me.lbUl_email.NTSDbField = ""
    Me.lbUl_email.Size = New System.Drawing.Size(35, 13)
    Me.lbUl_email.TabIndex = 24
    Me.lbUl_email.Text = "E-mail"
    Me.lbUl_email.UseMnemonic = False
    '
    'edUl_email
    '
    Me.edUl_email.Cursor = System.Windows.Forms.Cursors.Default
    Me.edUl_email.Location = New System.Drawing.Point(143, 109)
    Me.edUl_email.Name = "edUl_email"
    Me.edUl_email.NTSDbField = ""
    Me.edUl_email.NTSForzaVisZoom = False
    Me.edUl_email.NTSOldValue = ""
    Me.edUl_email.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUl_email.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUl_email.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUl_email.Properties.MaxLength = 65536
    Me.edUl_email.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUl_email.Size = New System.Drawing.Size(125, 20)
    Me.edUl_email.TabIndex = 514
    '
    'lbUl_usaem
    '
    Me.lbUl_usaem.AutoSize = True
    Me.lbUl_usaem.BackColor = System.Drawing.Color.Transparent
    Me.lbUl_usaem.Location = New System.Drawing.Point(3, 138)
    Me.lbUl_usaem.Name = "lbUl_usaem"
    Me.lbUl_usaem.NTSDbField = ""
    Me.lbUl_usaem.Size = New System.Drawing.Size(134, 13)
    Me.lbUl_usaem.TabIndex = 25
    Me.lbUl_usaem.Text = "Modalità di corrispondenza"
    Me.lbUl_usaem.UseMnemonic = False
    '
    'cbUl_usaem
    '
    Me.cbUl_usaem.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbUl_usaem.DataSource = Nothing
    Me.cbUl_usaem.DisplayMember = ""
    Me.cbUl_usaem.Location = New System.Drawing.Point(143, 135)
    Me.cbUl_usaem.Name = "cbUl_usaem"
    Me.cbUl_usaem.NTSDbField = ""
    Me.cbUl_usaem.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbUl_usaem.Properties.DropDownRows = 30
    Me.cbUl_usaem.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbUl_usaem.SelectedValue = ""
    Me.cbUl_usaem.Size = New System.Drawing.Size(125, 20)
    Me.cbUl_usaem.TabIndex = 515
    Me.cbUl_usaem.ValueMember = ""
    '
    'lbUl_stato
    '
    Me.lbUl_stato.AutoSize = True
    Me.lbUl_stato.BackColor = System.Drawing.Color.Transparent
    Me.lbUl_stato.Location = New System.Drawing.Point(4, 190)
    Me.lbUl_stato.Name = "lbUl_stato"
    Me.lbUl_stato.NTSDbField = ""
    Me.lbUl_stato.Size = New System.Drawing.Size(33, 13)
    Me.lbUl_stato.TabIndex = 26
    Me.lbUl_stato.Text = "Stato"
    Me.lbUl_stato.UseMnemonic = False
    '
    'edUl_stato
    '
    Me.edUl_stato.Cursor = System.Windows.Forms.Cursors.Default
    Me.edUl_stato.Location = New System.Drawing.Point(103, 213)
    Me.edUl_stato.Name = "edUl_stato"
    Me.edUl_stato.NTSDbField = ""
    Me.edUl_stato.NTSForzaVisZoom = False
    Me.edUl_stato.NTSOldValue = ""
    Me.edUl_stato.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUl_stato.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUl_stato.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUl_stato.Properties.MaxLength = 65536
    Me.edUl_stato.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUl_stato.Size = New System.Drawing.Size(62, 20)
    Me.edUl_stato.TabIndex = 516
    '
    'lbUl_codcomu
    '
    Me.lbUl_codcomu.AutoSize = True
    Me.lbUl_codcomu.BackColor = System.Drawing.Color.Transparent
    Me.lbUl_codcomu.Location = New System.Drawing.Point(4, 138)
    Me.lbUl_codcomu.Name = "lbUl_codcomu"
    Me.lbUl_codcomu.NTSDbField = ""
    Me.lbUl_codcomu.Size = New System.Drawing.Size(70, 13)
    Me.lbUl_codcomu.TabIndex = 27
    Me.lbUl_codcomu.Text = "Cod. comune"
    Me.lbUl_codcomu.UseMnemonic = False
    '
    'edUl_codcomu
    '
    Me.edUl_codcomu.Cursor = System.Windows.Forms.Cursors.Default
    Me.edUl_codcomu.Location = New System.Drawing.Point(103, 135)
    Me.edUl_codcomu.Name = "edUl_codcomu"
    Me.edUl_codcomu.NTSDbField = ""
    Me.edUl_codcomu.NTSForzaVisZoom = False
    Me.edUl_codcomu.NTSOldValue = ""
    Me.edUl_codcomu.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUl_codcomu.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUl_codcomu.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUl_codcomu.Properties.MaxLength = 65536
    Me.edUl_codcomu.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUl_codcomu.Size = New System.Drawing.Size(62, 20)
    Me.edUl_codcomu.TabIndex = 517
    '
    'lbUl_statofed
    '
    Me.lbUl_statofed.AutoSize = True
    Me.lbUl_statofed.BackColor = System.Drawing.Color.Transparent
    Me.lbUl_statofed.Location = New System.Drawing.Point(4, 244)
    Me.lbUl_statofed.Name = "lbUl_statofed"
    Me.lbUl_statofed.NTSDbField = ""
    Me.lbUl_statofed.Size = New System.Drawing.Size(93, 13)
    Me.lbUl_statofed.TabIndex = 29
    Me.lbUl_statofed.Text = "Stato fed./contea"
    Me.lbUl_statofed.UseMnemonic = False
    '
    'edUl_statofed
    '
    Me.edUl_statofed.Cursor = System.Windows.Forms.Cursors.Default
    Me.edUl_statofed.Location = New System.Drawing.Point(103, 241)
    Me.edUl_statofed.Name = "edUl_statofed"
    Me.edUl_statofed.NTSDbField = ""
    Me.edUl_statofed.NTSForzaVisZoom = False
    Me.edUl_statofed.NTSOldValue = ""
    Me.edUl_statofed.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUl_statofed.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUl_statofed.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUl_statofed.Properties.MaxLength = 65536
    Me.edUl_statofed.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUl_statofed.Size = New System.Drawing.Size(278, 20)
    Me.edUl_statofed.TabIndex = 519
    '
    'pnSx
    '
    Me.pnSx.AllowDrop = True
    Me.pnSx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSx.Appearance.Options.UseBackColor = True
    Me.pnSx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSx.Controls.Add(Me.edUl_coddest)
    Me.pnSx.Controls.Add(Me.lbXx_codcomu)
    Me.pnSx.Controls.Add(Me.lbXx_stato)
    Me.pnSx.Controls.Add(Me.lbTitle)
    Me.pnSx.Controls.Add(Me.lbUl_coddest)
    Me.pnSx.Controls.Add(Me.edUl_nomdest)
    Me.pnSx.Controls.Add(Me.lbUl_nomdest)
    Me.pnSx.Controls.Add(Me.edUl_prodest)
    Me.pnSx.Controls.Add(Me.lbUl_prodest)
    Me.pnSx.Controls.Add(Me.edUl_statofed)
    Me.pnSx.Controls.Add(Me.edUl_locdest)
    Me.pnSx.Controls.Add(Me.lbUl_statofed)
    Me.pnSx.Controls.Add(Me.lbUl_locdest)
    Me.pnSx.Controls.Add(Me.lbUl_capdest)
    Me.pnSx.Controls.Add(Me.edUl_codcomu)
    Me.pnSx.Controls.Add(Me.lbUl_codcomu)
    Me.pnSx.Controls.Add(Me.edUl_capdest)
    Me.pnSx.Controls.Add(Me.lbUl_stato)
    Me.pnSx.Controls.Add(Me.edUl_stato)
    Me.pnSx.Controls.Add(Me.edUl_nomdest2)
    Me.pnSx.Controls.Add(Me.edUl_inddest)
    Me.pnSx.Controls.Add(Me.lbUl_inddest)
    Me.pnSx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnSx.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnSx.Location = New System.Drawing.Point(0, 30)
    Me.pnSx.Name = "pnSx"
    Me.pnSx.Size = New System.Drawing.Size(400, 267)
    Me.pnSx.TabIndex = 522
    Me.pnSx.Text = "NtsPanel1"
    '
    'lbXx_codcomu
    '
    Me.lbXx_codcomu.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codcomu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codcomu.Location = New System.Drawing.Point(171, 135)
    Me.lbXx_codcomu.Name = "lbXx_codcomu"
    Me.lbXx_codcomu.NTSDbField = ""
    Me.lbXx_codcomu.Size = New System.Drawing.Size(210, 20)
    Me.lbXx_codcomu.TabIndex = 582
    Me.lbXx_codcomu.Text = "xx_codcomu"
    Me.lbXx_codcomu.UseMnemonic = False
    '
    'lbXx_stato
    '
    Me.lbXx_stato.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_stato.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_stato.Location = New System.Drawing.Point(172, 213)
    Me.lbXx_stato.Name = "lbXx_stato"
    Me.lbXx_stato.NTSDbField = ""
    Me.lbXx_stato.Size = New System.Drawing.Size(209, 20)
    Me.lbXx_stato.TabIndex = 581
    Me.lbXx_stato.Text = "xx_stato"
    Me.lbXx_stato.UseMnemonic = False
    '
    'lbTitle
    '
    Me.lbTitle.AutoSize = True
    Me.lbTitle.BackColor = System.Drawing.Color.Transparent
    Me.lbTitle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbTitle.Location = New System.Drawing.Point(3, 3)
    Me.lbTitle.Name = "lbTitle"
    Me.lbTitle.NTSDbField = ""
    Me.lbTitle.Size = New System.Drawing.Size(77, 13)
    Me.lbTitle.TabIndex = 10
    Me.lbTitle.Text = "Altri indirizzi"
    Me.lbTitle.UseMnemonic = False
    '
    'pnDx
    '
    Me.pnDx.AllowDrop = True
    Me.pnDx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnDx.Appearance.Options.UseBackColor = True
    Me.pnDx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnDx.Controls.Add(Me.edUl_turno)
    Me.pnDx.Controls.Add(Me.lbUl_turno)
    Me.pnDx.Controls.Add(Me.cbUl_usaem)
    Me.pnDx.Controls.Add(Me.lbUl_usaem)
    Me.pnDx.Controls.Add(Me.lbUl_email)
    Me.pnDx.Controls.Add(Me.lbUl_faxtlx)
    Me.pnDx.Controls.Add(Me.edUl_email)
    Me.pnDx.Controls.Add(Me.lbUl_telef)
    Me.pnDx.Controls.Add(Me.edUl_faxtlx)
    Me.pnDx.Controls.Add(Me.edUl_telef)
    Me.pnDx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnDx.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnDx.Location = New System.Drawing.Point(409, 30)
    Me.pnDx.Name = "pnDx"
    Me.pnDx.Size = New System.Drawing.Size(280, 267)
    Me.pnDx.TabIndex = 523
    Me.pnDx.Text = "NtsPanel1"
    '
    'FRM__DESD
    '
    Me.ClientSize = New System.Drawing.Size(689, 297)
    Me.Controls.Add(Me.pnDx)
    Me.Controls.Add(Me.pnSx)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__DESD"
    Me.Text = "DESTINAZIONI DIVERSE"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edUl_coddest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edUl_nomdest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edUl_nomdest2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edUl_inddest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edUl_capdest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edUl_locdest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edUl_prodest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edUl_turno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edUl_telef.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edUl_faxtlx.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edUl_email.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbUl_usaem.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edUl_stato.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edUl_codcomu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edUl_statofed.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnSx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSx.ResumeLayout(False)
    Me.pnSx.PerformLayout()
    CType(Me.pnDx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDx.ResumeLayout(False)
    Me.pnDx.PerformLayout()
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

  Public Overridable Sub InitEntity(ByRef cleAnaz As CLE__ANAZ, ByRef ds As DataSet, ByVal lCodDest As Integer)
    Dim i As Integer = 0
    Try
      oCleAnaz = cleAnaz
      oCleAnaz.lCodDestNew = lCodDest
      AddHandler oCleAnaz.RemoteEvent, AddressOf GestisciEventiEntity

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      dsDesg = ds
      oCleAnaz.DesgSetDataTable(DittaCorrente, dsDesg.Tables("ANAZUL"))
      dcDesg.DataSource = dsDesg.Tables("ANAZUL")
      dsDesg.AcceptChanges()

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
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'Ã¨ una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      Dim dttTipoSend As New DataTable()
      dttTipoSend.Columns.Add("cod", GetType(String))
      dttTipoSend.Columns.Add("val", GetType(String))
      dttTipoSend.Rows.Add(New Object() {"S", "E-mail Internet"})
      dttTipoSend.Rows.Add(New Object() {"X", "Fax service Win XP/2003"})
      'dttTipoSend.Rows.Add(New Object() {"Y", "Fax service Win 2000 (locale)"})
      dttTipoSend.Rows.Add(New Object() {"N", "Microsoft Fax (mapi)"})
      dttTipoSend.Rows.Add(New Object() {"Z", "Zetafax MAPI"})
      dttTipoSend.Rows.Add(New Object() {"H", "HylaFAX"})
      cbUl_usaem.DataSource = dttTipoSend
      cbUl_usaem.ValueMember = "cod"
      cbUl_usaem.DisplayMember = "val"

      '-------------------------------------------------
      'completo le informazioni dei controlli
      edUl_coddest.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128765040867394000, "Codice"), tabanazul)
      edUl_nomdest.NTSSetParam(oMenu, oApp.Tr(Me, 128765040882526000, "Descrizione"), 50, False)
      edUl_nomdest2.NTSSetParam(oMenu, oApp.Tr(Me, 128765040894070000, "Descrizione 2"), 128, True)
      edUl_inddest.NTSSetParam(oMenu, oApp.Tr(Me, 128765040903742000, "Indirizzo"), 70, True)
      edUl_capdest.NTSSetParam(oMenu, oApp.Tr(Me, 128765040913414000, "Cap"), 9, True)
      edUl_locdest.NTSSetParam(oMenu, oApp.Tr(Me, 128765040922774000, "Città"), 50, True)
      edUl_prodest.NTSSetParam(oMenu, oApp.Tr(Me, 128765040932290000, "Provincia"), 2, True)
      edUl_turno.NTSSetParam(oMenu, oApp.Tr(Me, 128765040941962000, "Turno di chiusura"), 15, True)
      edUl_telef.NTSSetParam(oMenu, oApp.Tr(Me, 128765040954754000, "Telefono"), 18, True)
      edUl_faxtlx.NTSSetParam(oMenu, oApp.Tr(Me, 128765040995470000, "Fax"), 18, True)
      edUl_email.NTSSetParam(oMenu, oApp.Tr(Me, 128765041004830000, "E-mail"), 100, True)
      cbUl_usaem.NTSSetParam(oApp.Tr(Me, 128765041015750000, "Modalità di corrispondenza"))
      edUl_stato.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128765041025110000, "Stato"), tabstat, True)
      edUl_codcomu.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128765041042894000, "Cod. comune"), tabcomuni, True)
      edUl_statofed.NTSSetParam(oMenu, oApp.Tr(Me, 128765041053190000, "Stato federato/contea"), 30, True)

      edUl_coddest.NTSSetParamZoom("")
      edUl_nomdest.NTSSetParamZoom("ZOOMANAZUL")
      'edUl_nomdest.NTSForzaVisZoom = True
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
      'se i controlli erano già  stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      edUl_coddest.NTSDbField = "ANAZUL.ul_coddest"
      edUl_nomdest.NTSDbField = "ANAZUL.ul_nomdest"
      edUl_nomdest2.NTSDbField = "ANAZUL.ul_nomdest2"
      edUl_inddest.NTSDbField = "ANAZUL.ul_inddest"
      edUl_capdest.NTSDbField = "ANAZUL.ul_capdest"
      edUl_locdest.NTSDbField = "ANAZUL.ul_locdest"
      edUl_prodest.NTSDbField = "ANAZUL.ul_prodest"
      edUl_turno.NTSDbField = "ANAZUL.ul_turno"
      edUl_telef.NTSDbField = "ANAZUL.ul_telef"
      edUl_faxtlx.NTSDbField = "ANAZUL.ul_faxtlx"
      edUl_email.NTSDbField = "ANAZUL.ul_email"
      cbUl_usaem.NTSDbField = "ANAZUL.ul_usaem"
      edUl_stato.NTSDbField = "ANAZUL.ul_stato"
      edUl_codcomu.NTSDbField = "ANAZUL.ul_codcomu"
      edUl_statofed.NTSDbField = "ANAZUL.ul_statofed"
      lbXx_codcomu.NTSDbField = "ANAZUL.xx_codcomu"
      lbXx_stato.NTSDbField = "ANAZUL.xx_stato"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcDesg, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRM__DESG_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim i As Integer = 0
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      If dsDesg.Tables("ANAZUL").Rows.Count = 0 Then
        edUl_coddest.Enabled = True
        tlbNuovo_ItemClick(tlbNuovo, Nothing)
      Else
        If oCleAnaz.lCodDestNew <> 0 Then tlbNuovo.Enabled = False
        edUl_coddest.Enabled = False
      End If

      dcDesg.ResetBindings(False)
      dcDesg.MoveFirst()

      '--------------------------------------------
      'sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
      If Not oCallParams Is Nothing Then
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
          edUl_coddest.Enabled = True
          tlbNuovo_ItemClick(Me, Nothing)
        ElseIf Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) <> "" Then
          For i = 0 To dcDesg.List.Count - 1
            If NTSCInt(CType(dcDesg.Item(i), DataRowView)!ul_coddest) = NTSCInt(Microsoft.VisualBasic.Mid(oCallParams.strParam, 6)) Then
              dcDesg.Position = i
              Exit For
            End If
          Next
        End If
      End If    'If Not oCallParams Is Nothing Then

      lbTitle.Text = oCallParams.strPar1
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__DESG_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva(True, True) Then e.Cancel = True
  End Sub

  Public Overridable Sub FRM__DESG_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcDesg.Dispose()
      dsDesg.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      '-------------------------------------------------
      'creo una nuova forma di pagamento
      If Not Salva(False, True) Then Return
      oCleAnaz.DesgNuovo()
      dcDesg.MoveLast()
      edUl_coddest.Enabled = True
      edUl_coddest.Focus()

      '-------------------------------------------------
      'imposto i valori di default come impostato nella GCTL
      Me.GctlApplicaDefaultValue()

      If oCleAnaz.lCodDestNew <> 0 Then
        edUl_coddest.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva(False, True)

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
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127791952085312500, "Cancellare la destinazione?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes

          If dsDesg.Tables("ANAZUL").Rows.Count = 1 Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          dcDesg.RemoveAt(dcDesg.Position)
          oCleAnaz.DesgSalva(True)

          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcDesg, Me)
            bRemovBinding = False
            edUl_coddest.Enabled = True
          Else
            edUl_coddest.Enabled = False
          End If

          Return
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcDesg, Me)
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
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128275017320420000, "Ripristinare le modifiche apportate?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          If dsDesg.Tables("ANAZUL").Rows.Count = 1 And dsDesg.Tables("ANAZUL").Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          oCleAnaz.DesgRipristina(dcDesg.Position, dcDesg.Filter)

          If bRemovBinding Then
            tlbNuovo.Enabled = True
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcDesg, Me)
            bRemovBinding = False
            If oCleAnaz.lCodDestNew = 0 Then
              edUl_coddest.Enabled = True
            End If
          Else
            edUl_coddest.Enabled = False
          End If
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcDesg, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim i As Integer = 0
    Dim strTmp As String = ""
    Dim oParam As New CLE__PATB
    Dim ds As New DataSet
    Try
      If edUl_nomdest.ContainsFocus Then
        'non posso fare lo zoom standard, visto che potrei selez. una destinaz. diversa appena inserita e non ancora salvata ...
        If Not Salva(False, True) Then Return
        'creo un dataset contenente tutte le destinazioni diverse che ho in memoria
        ds.Tables.Add(dsDesg.Tables("ANAZUL").Clone)
        ds.Tables(0).TableName = "DESTDIV"
        For i = 0 To dsDesg.Tables("ANAZUL").Rows.Count - 1
          ds.Tables(0).ImportRow(dsDesg.Tables("ANAZUL").Rows(i))
        Next
        'rinomino le colonne per farle uguali a quelle dello zoom
        For i = 0 To ds.Tables("DESTDIV").Columns.Count - 1
          If ds.Tables("DESTDIV").Columns(i).ColumnName.ToLower.Substring(0, 2) = "ul" Then
            ds.Tables("DESTDIV").Columns(i).ColumnName = "dd_" & ds.Tables("DESTDIV").Columns(i).ColumnName.Substring(3)
          End If
        Next
        ds.Tables(0).AcceptChanges()
        NTSZOOM.strIn = edUl_nomdest.Text
        oParam.oParam = ds
        NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edUl_coddest.Text Then
          For i = 0 To dcDesg.List.Count - 1
            If CType(dcDesg.Item(i), DataRowView)!ul_coddest.ToString = NTSZOOM.strIn Then
              dcDesg.Position = i
              Exit For
            End If
          Next
        End If
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

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub

  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    '-------------------------------------------------
    'vado sul primo record
    If Not Salva(False, True) Then Return
    dcDesg.MoveFirst()
  End Sub

  Public Overridable Sub tlbPrecedente_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrecedente.ItemClick
    '-------------------------------------------------
    'vado sul record precedente
    If Not Salva(False, True) Then Return
    dcDesg.MovePrevious()
  End Sub

  Public Overridable Sub tlbSuccessivo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSuccessivo.ItemClick
    '-------------------------------------------------
    'vado sul record successivo
    If Not Salva(False, True) Then Return
    dcDesg.MoveNext()
  End Sub

  Public Overridable Sub tlbUltimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbUltimo.ItemClick
    '-------------------------------------------------
    'vado sull'ultimo record
    If Not Salva(False, True) Then Return
    dcDesg.MoveLast()
  End Sub
#End Region

  Public Overridable Function Salva(ByVal bEsci As Boolean, ByVal bAsk As Boolean) As Boolean
    Dim dRes As DialogResult
    Try
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      '-------------------------------------------------
      'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
      If dsDesg.Tables("ANAZUL").Select("", "", DataViewRowState.Added Or DataViewRowState.ModifiedCurrent).Length > 0 Then
        If GctlControllaOutNotEqual() = False Then Return False

        If bAsk Then
          dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128275017320576000, "Confermi il salvataggio?"))
        Else
          dRes = System.Windows.Forms.DialogResult.Yes
        End If

        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oCleAnaz.DesgSalva(False) Then Return False
          If dsDesg.Tables("ANAZUL").Rows.Count > 0 Then
            edUl_coddest.Enabled = False
            If oCleAnaz.lCodDestNew <> 0 Then tlbNuovo.Enabled = False
          End If
        End If
        If dRes = System.Windows.Forms.DialogResult.No Then

          If bEsci Then
            oCleAnaz.DesgRipristina(dcDesg.Position, dcDesg.Filter)
          Else
            tlbRipristina_ItemClick(Nothing, Nothing)
          End If
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

