Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__ANIV_F
  Public oCleAnaz As CLE__ANAZ
  Public dsAniv As DataSet
  Public oCallParams As CLE__CLDP
  Public dcAniv As BindingSource = New BindingSource
  Public nAnivEscomp As Integer = 0
  Public nModale As Integer = 0       'utilizzato da wizard ditta
  Public bSceglianno As Boolean = False
  Public nAnnoAperto As Integer = 0

  Private components As System.ComponentModel.IContainer


  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__ANIV_F))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbAnno = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbAttivita = New NTSInformatica.NTSBarButtonItem
    Me.tlbRegistri = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.lbAi_sezliqriep = New NTSInformatica.NTSLabel
    Me.cbAi_sezliqriep = New NTSInformatica.NTSComboBox
    Me.lbAi_verdociva = New NTSInformatica.NTSLabel
    Me.cbAi_verdociva = New NTSInformatica.NTSComboBox
    Me.lbAi_calcacc12 = New NTSInformatica.NTSLabel
    Me.cbAi_calcacc12 = New NTSInformatica.NTSComboBox
    Me.lbAi_pgulrir = New NTSInformatica.NTSLabel
    Me.edAi_pgulrir = New NTSInformatica.NTSTextBoxNum
    Me.fmWeb = New NTSInformatica.NTSGroupBox
    Me.lbAi_comelind = New NTSInformatica.NTSLabel
    Me.edAi_comelisp = New NTSInformatica.NTSTextBoxStr
    Me.lbAi_comelisp = New NTSInformatica.NTSLabel
    Me.edAi_comelind = New NTSInformatica.NTSTextBoxStr
    Me.lbAi_comeltis = New NTSInformatica.NTSLabel
    Me.cbAi_comeltis = New NTSInformatica.NTSComboBox
    Me.ckAi_comelfl = New NTSInformatica.NTSCheckBox
    Me.pnControlli = New NTSInformatica.NTSPanel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAi_sezliqriep.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAi_verdociva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAi_calcacc12.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAi_pgulrir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmWeb, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmWeb.SuspendLayout()
    CType(Me.edAi_comelisp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAi_comelind.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAi_comeltis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAi_comelfl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnControlli, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnControlli.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbAnno, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbAttivita, Me.tlbRegistri, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 26
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAnno), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAttivita, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRegistri), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbAnno
    '
    Me.tlbAnno.Caption = "Cambia anno"
    Me.tlbAnno.Glyph = CType(resources.GetObject("tlbAnno.Glyph"), System.Drawing.Image)
    Me.tlbAnno.Id = 0
    Me.tlbAnno.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
    Me.tlbAnno.Name = "tlbAnno"
    Me.tlbAnno.Visible = True
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
    'tlbAttivita
    '
    Me.tlbAttivita.Caption = "Attività IVA"
    Me.tlbAttivita.Glyph = CType(resources.GetObject("tlbAttivita.Glyph"), System.Drawing.Image)
    Me.tlbAttivita.Id = 5
    Me.tlbAttivita.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F7))
    Me.tlbAttivita.Name = "tlbAttivita"
    Me.tlbAttivita.Visible = True
    '
    'tlbRegistri
    '
    Me.tlbRegistri.Caption = "Registri IVA"
    Me.tlbRegistri.Glyph = CType(resources.GetObject("tlbRegistri.Glyph"), System.Drawing.Image)
    Me.tlbRegistri.Id = 6
    Me.tlbRegistri.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F8))
    Me.tlbRegistri.Name = "tlbRegistri"
    Me.tlbRegistri.Visible = True
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
    'lbAi_sezliqriep
    '
    Me.lbAi_sezliqriep.AutoSize = True
    Me.lbAi_sezliqriep.BackColor = System.Drawing.Color.Transparent
    Me.lbAi_sezliqriep.Location = New System.Drawing.Point(11, 9)
    Me.lbAi_sezliqriep.Name = "lbAi_sezliqriep"
    Me.lbAi_sezliqriep.NTSDbField = ""
    Me.lbAi_sezliqriep.Size = New System.Drawing.Size(178, 13)
    Me.lbAi_sezliqriep.TabIndex = 512
    Me.lbAi_sezliqriep.Text = "Sezione per stampa liquidazione Iva"
    Me.lbAi_sezliqriep.Tooltip = ""
    Me.lbAi_sezliqriep.UseMnemonic = False
    '
    'cbAi_sezliqriep
    '
    Me.cbAi_sezliqriep.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAi_sezliqriep.DataSource = Nothing
    Me.cbAi_sezliqriep.DisplayMember = ""
    Me.cbAi_sezliqriep.Location = New System.Drawing.Point(283, 6)
    Me.cbAi_sezliqriep.Name = "cbAi_sezliqriep"
    Me.cbAi_sezliqriep.NTSDbField = ""
    Me.cbAi_sezliqriep.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAi_sezliqriep.Properties.DropDownRows = 30
    Me.cbAi_sezliqriep.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAi_sezliqriep.SelectedValue = ""
    Me.cbAi_sezliqriep.Size = New System.Drawing.Size(182, 20)
    Me.cbAi_sezliqriep.TabIndex = 513
    Me.cbAi_sezliqriep.ValueMember = ""
    '
    'lbAi_verdociva
    '
    Me.lbAi_verdociva.AutoSize = True
    Me.lbAi_verdociva.BackColor = System.Drawing.Color.Transparent
    Me.lbAi_verdociva.Location = New System.Drawing.Point(11, 31)
    Me.lbAi_verdociva.Name = "lbAi_verdociva"
    Me.lbAi_verdociva.NTSDbField = ""
    Me.lbAi_verdociva.Size = New System.Drawing.Size(177, 13)
    Me.lbAi_verdociva.TabIndex = 518
    Me.lbAi_verdociva.Text = "Verifica preesistenza documenti Iva"
    Me.lbAi_verdociva.Tooltip = ""
    Me.lbAi_verdociva.UseMnemonic = False
    '
    'cbAi_verdociva
    '
    Me.cbAi_verdociva.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAi_verdociva.DataSource = Nothing
    Me.cbAi_verdociva.DisplayMember = ""
    Me.cbAi_verdociva.Location = New System.Drawing.Point(283, 28)
    Me.cbAi_verdociva.Name = "cbAi_verdociva"
    Me.cbAi_verdociva.NTSDbField = ""
    Me.cbAi_verdociva.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAi_verdociva.Properties.DropDownRows = 30
    Me.cbAi_verdociva.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAi_verdociva.SelectedValue = ""
    Me.cbAi_verdociva.Size = New System.Drawing.Size(182, 20)
    Me.cbAi_verdociva.TabIndex = 519
    Me.cbAi_verdociva.ValueMember = ""
    '
    'lbAi_calcacc12
    '
    Me.lbAi_calcacc12.AutoSize = True
    Me.lbAi_calcacc12.BackColor = System.Drawing.Color.Transparent
    Me.lbAi_calcacc12.Location = New System.Drawing.Point(11, 53)
    Me.lbAi_calcacc12.Name = "lbAi_calcacc12"
    Me.lbAi_calcacc12.NTSDbField = ""
    Me.lbAi_calcacc12.Size = New System.Drawing.Size(128, 13)
    Me.lbAi_calcacc12.TabIndex = 520
    Me.lbAi_calcacc12.Text = "Calcolo acconto dicembre"
    Me.lbAi_calcacc12.Tooltip = ""
    Me.lbAi_calcacc12.UseMnemonic = False
    '
    'cbAi_calcacc12
    '
    Me.cbAi_calcacc12.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAi_calcacc12.DataSource = Nothing
    Me.cbAi_calcacc12.DisplayMember = ""
    Me.cbAi_calcacc12.Location = New System.Drawing.Point(283, 50)
    Me.cbAi_calcacc12.Name = "cbAi_calcacc12"
    Me.cbAi_calcacc12.NTSDbField = ""
    Me.cbAi_calcacc12.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAi_calcacc12.Properties.DropDownRows = 30
    Me.cbAi_calcacc12.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAi_calcacc12.SelectedValue = ""
    Me.cbAi_calcacc12.Size = New System.Drawing.Size(182, 20)
    Me.cbAi_calcacc12.TabIndex = 522
    Me.cbAi_calcacc12.ValueMember = ""
    '
    'lbAi_pgulrir
    '
    Me.lbAi_pgulrir.AutoSize = True
    Me.lbAi_pgulrir.BackColor = System.Drawing.Color.Transparent
    Me.lbAi_pgulrir.Location = New System.Drawing.Point(11, 75)
    Me.lbAi_pgulrir.Name = "lbAi_pgulrir"
    Me.lbAi_pgulrir.NTSDbField = ""
    Me.lbAi_pgulrir.Size = New System.Drawing.Size(211, 13)
    Me.lbAi_pgulrir.TabIndex = 526
    Me.lbAi_pgulrir.Text = "N° ultima pagina registrazione riepilogativo"
    Me.lbAi_pgulrir.Tooltip = ""
    Me.lbAi_pgulrir.UseMnemonic = False
    '
    'edAi_pgulrir
    '
    Me.edAi_pgulrir.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAi_pgulrir.EditValue = "0"
    Me.edAi_pgulrir.Location = New System.Drawing.Point(283, 72)
    Me.edAi_pgulrir.Name = "edAi_pgulrir"
    Me.edAi_pgulrir.NTSDbField = ""
    Me.edAi_pgulrir.NTSFormat = "0"
    Me.edAi_pgulrir.NTSForzaVisZoom = False
    Me.edAi_pgulrir.NTSOldValue = ""
    Me.edAi_pgulrir.Properties.Appearance.Options.UseTextOptions = True
    Me.edAi_pgulrir.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAi_pgulrir.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAi_pgulrir.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAi_pgulrir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAi_pgulrir.Properties.MaxLength = 65536
    Me.edAi_pgulrir.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAi_pgulrir.Size = New System.Drawing.Size(88, 20)
    Me.edAi_pgulrir.TabIndex = 529
    '
    'fmWeb
    '
    Me.fmWeb.AllowDrop = True
    Me.fmWeb.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmWeb.Appearance.Options.UseBackColor = True
    Me.fmWeb.Controls.Add(Me.lbAi_comelind)
    Me.fmWeb.Controls.Add(Me.edAi_comelisp)
    Me.fmWeb.Controls.Add(Me.lbAi_comelisp)
    Me.fmWeb.Controls.Add(Me.edAi_comelind)
    Me.fmWeb.Controls.Add(Me.lbAi_comeltis)
    Me.fmWeb.Controls.Add(Me.cbAi_comeltis)
    Me.fmWeb.Controls.Add(Me.ckAi_comelfl)
    Me.fmWeb.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmWeb.Location = New System.Drawing.Point(14, 98)
    Me.fmWeb.Name = "fmWeb"
    Me.fmWeb.Size = New System.Drawing.Size(596, 82)
    Me.fmWeb.TabIndex = 533
    '
    'lbAi_comelind
    '
    Me.lbAi_comelind.AutoSize = True
    Me.lbAi_comelind.BackColor = System.Drawing.Color.Transparent
    Me.lbAi_comelind.Location = New System.Drawing.Point(3, 26)
    Me.lbAi_comelind.Name = "lbAi_comelind"
    Me.lbAi_comelind.NTSDbField = ""
    Me.lbAi_comelind.Size = New System.Drawing.Size(50, 13)
    Me.lbAi_comelind.TabIndex = 521
    Me.lbAi_comelind.Text = "Sito Web"
    Me.lbAi_comelind.Tooltip = ""
    Me.lbAi_comelind.UseMnemonic = False
    '
    'edAi_comelisp
    '
    Me.edAi_comelisp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAi_comelisp.Location = New System.Drawing.Point(269, 49)
    Me.edAi_comelisp.Name = "edAi_comelisp"
    Me.edAi_comelisp.NTSDbField = ""
    Me.edAi_comelisp.NTSForzaVisZoom = False
    Me.edAi_comelisp.NTSOldValue = ""
    Me.edAi_comelisp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAi_comelisp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAi_comelisp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAi_comelisp.Properties.MaxLength = 65536
    Me.edAi_comelisp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAi_comelisp.Size = New System.Drawing.Size(318, 20)
    Me.edAi_comelisp.TabIndex = 526
    '
    'lbAi_comelisp
    '
    Me.lbAi_comelisp.AutoSize = True
    Me.lbAi_comelisp.BackColor = System.Drawing.Color.Transparent
    Me.lbAi_comelisp.Location = New System.Drawing.Point(172, 52)
    Me.lbAi_comelisp.Name = "lbAi_comelisp"
    Me.lbAi_comelisp.NTSDbField = ""
    Me.lbAi_comelisp.Size = New System.Drawing.Size(85, 13)
    Me.lbAi_comelisp.TabIndex = 523
    Me.lbAi_comelisp.Text = "Service provider"
    Me.lbAi_comelisp.Tooltip = ""
    Me.lbAi_comelisp.UseMnemonic = False
    '
    'edAi_comelind
    '
    Me.edAi_comelind.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAi_comelind.Location = New System.Drawing.Point(61, 23)
    Me.edAi_comelind.Name = "edAi_comelind"
    Me.edAi_comelind.NTSDbField = ""
    Me.edAi_comelind.NTSForzaVisZoom = False
    Me.edAi_comelind.NTSOldValue = ""
    Me.edAi_comelind.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAi_comelind.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAi_comelind.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAi_comelind.Properties.MaxLength = 65536
    Me.edAi_comelind.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAi_comelind.Size = New System.Drawing.Size(526, 20)
    Me.edAi_comelind.TabIndex = 524
    '
    'lbAi_comeltis
    '
    Me.lbAi_comeltis.AutoSize = True
    Me.lbAi_comeltis.BackColor = System.Drawing.Color.Transparent
    Me.lbAi_comeltis.Location = New System.Drawing.Point(3, 52)
    Me.lbAi_comeltis.Name = "lbAi_comeltis"
    Me.lbAi_comeltis.NTSDbField = ""
    Me.lbAi_comeltis.Size = New System.Drawing.Size(47, 13)
    Me.lbAi_comeltis.TabIndex = 522
    Me.lbAi_comeltis.Text = "Tipo sito"
    Me.lbAi_comeltis.Tooltip = ""
    Me.lbAi_comeltis.UseMnemonic = False
    '
    'cbAi_comeltis
    '
    Me.cbAi_comeltis.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAi_comeltis.DataSource = Nothing
    Me.cbAi_comeltis.DisplayMember = ""
    Me.cbAi_comeltis.Location = New System.Drawing.Point(61, 49)
    Me.cbAi_comeltis.Name = "cbAi_comeltis"
    Me.cbAi_comeltis.NTSDbField = ""
    Me.cbAi_comeltis.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAi_comeltis.Properties.DropDownRows = 30
    Me.cbAi_comeltis.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAi_comeltis.SelectedValue = ""
    Me.cbAi_comeltis.Size = New System.Drawing.Size(100, 20)
    Me.cbAi_comeltis.TabIndex = 525
    Me.cbAi_comeltis.ValueMember = ""
    '
    'ckAi_comelfl
    '
    Me.ckAi_comelfl.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAi_comelfl.Location = New System.Drawing.Point(6, 0)
    Me.ckAi_comelfl.Name = "ckAi_comelfl"
    Me.ckAi_comelfl.NTSCheckValue = "S"
    Me.ckAi_comelfl.NTSUnCheckValue = "N"
    Me.ckAi_comelfl.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAi_comelfl.Properties.Appearance.Options.UseBackColor = True
    Me.ckAi_comelfl.Properties.Caption = "Commercio elettronico"
    Me.ckAi_comelfl.Size = New System.Drawing.Size(136, 19)
    Me.ckAi_comelfl.TabIndex = 518
    '
    'pnControlli
    '
    Me.pnControlli.AllowDrop = True
    Me.pnControlli.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnControlli.Appearance.Options.UseBackColor = True
    Me.pnControlli.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnControlli.Controls.Add(Me.fmWeb)
    Me.pnControlli.Controls.Add(Me.cbAi_sezliqriep)
    Me.pnControlli.Controls.Add(Me.lbAi_sezliqriep)
    Me.pnControlli.Controls.Add(Me.cbAi_verdociva)
    Me.pnControlli.Controls.Add(Me.lbAi_verdociva)
    Me.pnControlli.Controls.Add(Me.lbAi_pgulrir)
    Me.pnControlli.Controls.Add(Me.cbAi_calcacc12)
    Me.pnControlli.Controls.Add(Me.edAi_pgulrir)
    Me.pnControlli.Controls.Add(Me.lbAi_calcacc12)
    Me.pnControlli.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnControlli.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnControlli.Location = New System.Drawing.Point(0, 30)
    Me.pnControlli.Name = "pnControlli"
    Me.pnControlli.NTSActiveTrasparency = True
    Me.pnControlli.Size = New System.Drawing.Size(621, 189)
    Me.pnControlli.TabIndex = 534
    Me.pnControlli.Text = "NtsPanel1"
    '
    'FRM__ANIV_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(621, 219)
    Me.Controls.Add(Me.pnControlli)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__ANIV_F"
    Me.Text = "DATI IVA"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAi_sezliqriep.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAi_verdociva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAi_calcacc12.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAi_pgulrir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmWeb, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmWeb.ResumeLayout(False)
    Me.fmWeb.PerformLayout()
    CType(Me.edAi_comelisp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAi_comelind.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAi_comeltis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAi_comelfl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnControlli, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnControlli.ResumeLayout(False)
    Me.pnControlli.PerformLayout()
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
        tlbAnno.GlyphPath = (oApp.ChildImageDir & "\open.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbAttivita.GlyphPath = (oApp.ChildImageDir & "\ordini.gif")
        tlbRegistri.GlyphPath = (oApp.ChildImageDir & "\ordini_2.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'Ã¨ una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      '-------------------------------------------------
      'completo le informazioni dei controlli
      cbAi_sezliqriep.NTSSetParam(oApp.Tr(Me, 128648358646875000, "Sezione per stampa liquidaz. IVA"))
      cbAi_verdociva.NTSSetParam(oApp.Tr(Me, 128648358700156250, "Verifica preesistenza doc. IVA"))
      cbAi_calcacc12.NTSSetParam(oApp.Tr(Me, 128648358712187500, "Calcolo acconto dicembre"))
      ckAi_comelfl.NTSSetParam(oMenu, oApp.Tr(Me, 128648358738281250, "Commercio elettronico"), "S", "N")
      edAi_comelind.NTSSetParam(oMenu, oApp.Tr(Me, 128648358750937500, "Indirizzo Web"), 100, True)
      cbAi_comeltis.NTSSetParam(oApp.Tr(Me, 128648358763437500, "Tipo sito"))
      edAi_comelisp.NTSSetParam(oMenu, oApp.Tr(Me, 128648358777031250, "Service provider"), 100, True)
      edAi_pgulrir.NTSSetParam(oMenu, oApp.Tr(Me, 128648358823437500, "Numero ultima pagina reg. riepilogativo"), "0", 9, 0, 999999999)

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
    Try
      '---------------------------
      Dim dttSezliqriep As New DataTable()
      dttSezliqriep.Columns.Add("cod", GetType(String))
      dttSezliqriep.Columns.Add("val", GetType(String))
      dttSezliqriep.Rows.Add(New Object() {"V", "Vendite prima attività"})
      dttSezliqriep.Rows.Add(New Object() {"A", "Acquisti prima attività"})
      dttSezliqriep.Rows.Add(New Object() {"C", "Corrispettivi prima attività"})
      dttSezliqriep.Rows.Add(New Object() {"R", "Riepilogativa"})
      dttSezliqriep.AcceptChanges()
      cbAi_sezliqriep.DataSource = dttSezliqriep
      cbAi_sezliqriep.ValueMember = "cod"
      cbAi_sezliqriep.DisplayMember = "val"

      '---------------------------
      Dim dttVerdociva As New DataTable()
      dttVerdociva.Columns.Add("cod", GetType(String))
      dttVerdociva.Columns.Add("val", GetType(String))
      dttVerdociva.Rows.Add(New Object() {"N", "(Nessuna)"})
      dttVerdociva.Rows.Add(New Object() {"2", "Su N.docum. e protocollo"})
      dttVerdociva.Rows.Add(New Object() {"3", "Su N.documento"})
      dttVerdociva.Rows.Add(New Object() {"4", "Su protocollo"})
      dttVerdociva.AcceptChanges()
      cbAi_verdociva.DataSource = dttVerdociva
      cbAi_verdociva.ValueMember = "cod"
      cbAi_verdociva.DisplayMember = "val"

      '---------------------------
      Dim dttCalcacc12 As New DataTable()
      dttCalcacc12.Columns.Add("cod", GetType(String))
      dttCalcacc12.Columns.Add("val", GetType(String))
      dttCalcacc12.Rows.Add(New Object() {"M", "Da movimenti"})
      dttCalcacc12.Rows.Add(New Object() {"D", "Da dichiarazioni"})
      dttCalcacc12.Rows.Add(New Object() {"N", "Non soggetto"})
      dttCalcacc12.AcceptChanges()
      cbAi_calcacc12.DataSource = dttCalcacc12
      cbAi_calcacc12.ValueMember = "cod"
      cbAi_calcacc12.DisplayMember = "val"

      '---------------------------
      Dim dttComeltis As New DataTable()
      dttComeltis.Columns.Add("cod", GetType(String))
      dttComeltis.Columns.Add("val", GetType(String))
      dttComeltis.Rows.Add(New Object() {"P", "Proprio"})
      dttComeltis.Rows.Add(New Object() {"O", "Ospitante"})
      dttComeltis.AcceptChanges()
      cbAi_comeltis.DataSource = dttComeltis
      cbAi_comeltis.ValueMember = "cod"
      cbAi_comeltis.DisplayMember = "val"

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano giÃ  stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      cbAi_sezliqriep.NTSDbField = "ANAZIVA.ai_sezliqriep"
      cbAi_verdociva.NTSDbField = "ANAZIVA.ai_verdociva"
      cbAi_calcacc12.NTSDbField = "ANAZIVA.ai_calcacc12"
      ckAi_comelfl.NTSText.NTSDbField = "ANAZIVA.ai_comelfl"
      edAi_comelind.NTSDbField = "ANAZIVA.ai_comelind"
      cbAi_comeltis.NTSDbField = "ANAZIVA.ai_comeltis"
      edAi_comelisp.NTSDbField = "ANAZIVA.ai_comelisp"
      edAi_pgulrir.NTSDbField = "ANAZIVA.ai_pgulrir"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcAniv, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRM__ANIV_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim i As Integer = 0
    Dim bOk As Boolean = False
    Dim nAnno As Integer = 0
    Try
      '-------------------------------------------------
      'carico i combobox
      CaricaCombo()

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      dsAniv = oCleAnaz.dsShared
      dcAniv.DataSource = dsAniv.Tables("ANAZIVA")
      dsAniv.Tables("ANAZIVA").AcceptChanges()

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      SetStato(0)

      If dsAniv.Tables("ANAZIVA").Rows.Count > 0 Then dcAniv.ResetBindings(False)

      If bSceglianno Then
        nAnno = ScegliAnno()
        If nAnno = 0 Then
          Me.Close()
          Return
        End If
      Else
        nAnno = nAnivEscomp
        nAnnoAperto = nAnivEscomp
      End If

      For i = 0 To dcAniv.List.Count - 1
        If NTSCInt(CType(dcAniv.Item(i), DataRowView)!ai_aanno) = nAnno Then
          bOk = True
          dcAniv.Position = i
          Exit For
        End If
      Next

      If bOk = False Then
        Nuovo()
        dcAniv.ResetBindings(False)
        dcAniv.MoveLast()
      End If

      ckAi_comelfl_CheckedChanged(ckAi_comelfl, Nothing)

      SetStato(1)

      '----------------
      If nModale = 1 Then
        tlbAttivita_ItemClick(tlbAttivita, Nothing)
        Me.Close()
        Return
      End If

      If nModale = 2 Then
        tlbRegistri_ItemClick(tlbRegistri, Nothing)
        Me.Close()
        Return
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__ANIV_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub FRM__ANIV_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcAniv.Dispose()
      dsAniv.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbAnno_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAnno.ItemClick
    Dim nAnno As Integer = 0
    Dim nAnnoTmp As Integer = 0
    Dim i As Integer = 0
    Dim bOk As Boolean = False

    Try
      '-------------------------------------------------
      'creo una nuova forma di pagamento
      If Not Salva() Then Return
      SetStato(0)
      nAnnoTmp = nAnnoAperto
      nAnno = ScegliAnno()

      If nAnno = 0 Then
        Me.Close()
        Return
      End If

      If nAnno <> nAnnoTmp Then
        For i = 0 To dcAniv.List.Count - 1
          If NTSCInt(CType(dcAniv.Item(i), DataRowView)!ai_aanno) = nAnno Then
            bOk = True
            dcAniv.Position = i
            Exit For
          End If
        Next

        If bOk = False Then
          Nuovo()
          dcAniv.MoveLast()
        End If
      End If

      SetStato(1)

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
      'cancello la forma di pagamento
      Dim dlgRes As DialogResult
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129006949920343835, "Confermi la cancellazione?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes

          If dsAniv.Tables("ANAZIVA").Rows.Count = 1 Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          dcAniv.RemoveAt(dcAniv.Position)
          oCleAnaz.AnivSalva(True, nAnnoAperto)

          Me.Close()
          Return
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcAniv, Me)
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
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129006950022221443, "Ripristinare le modifiche apportate?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          If CType(dcAniv.Current, DataRowView).Row.RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          oCleAnaz.AnivRipristina(dcAniv.Position, dcAniv.Filter)

          If bRemovBinding Then
            Me.Close()
            Return
          End If
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcAniv, Me)
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

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub

  Public Overridable Sub tlbAttivita_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAttivita.ItemClick
    Dim ds As New DataSet
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim frmAiva As FRM__AIVA = Nothing
    Try
      frmAiva = CType(NTSNewFormModal("FRM__AIVA"), FRM__AIVA)

      If Not Salva() Then Return

      Me.Cursor = Cursors.WaitCursor
      '-------------------------------
      'clono la tabella perchè devo far vedere solo le attività con anno uguale a quello in analisi
      ds.Tables.Clear()
      ds.Tables.Add(oCleAnaz.dsShared.Tables("TABATTI").Clone())
      ds.Tables(0).TableName = "TABATTI"
      dtrT = oCleAnaz.dsShared.Tables("TABATTI").Select("tb_anno = " & nAnnoAperto, "tb_codatti")
      For i = 0 To dtrT.Length - 1
        ds.Tables("TABATTI").ImportRow(dtrT(i))
        dtrT(i).Delete()
      Next
      oCleAnaz.dsShared.Tables("TABATTI").AcceptChanges()

      frmAiva.Init(oMenu, Nothing, DittaCorrente)
      frmAiva.InitEntity(oCleAnaz, ds, nAnnoAperto)
      frmAiva.ShowDialog()

      '-------------------------------
      'riacquisisco tabatti
      For i = 0 To ds.Tables("TABATTI").Rows.Count - 1
        If ds.Tables("TABATTI").Rows(i).RowState <> DataRowState.Deleted Then
          oCleAnaz.dsShared.Tables("TABATTI").ImportRow(ds.Tables("TABATTI").Rows(i))
        End If
      Next
      ds.Tables.Clear()
      oCleAnaz.dsShared.Tables("TABATTI").AcceptChanges()
      oCleAnaz.bHasChanges = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmAiva Is Nothing Then frmAiva.Dispose()
      frmAiva = Nothing
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbRegistri_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRegistri.ItemClick
    Dim ds As New DataSet
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim frmDuri As FRM__DURI = Nothing
    Try
      frmDuri = CType(NTSNewFormModal("FRM__DURI"), FRM__DURI)

      If Not Salva() Then Return

      Me.Cursor = Cursors.WaitCursor
      '-------------------------------
      'clono la tabella perchè devo far vedere solo le attività con anno uguale a quello in analisi
      ds.Tables.Clear()
      ds.Tables.Add(oCleAnaz.dsShared.Tables("TABDURI").Clone())
      ds.Tables(0).TableName = "TABDURI"
      dtrT = oCleAnaz.dsShared.Tables("TABDURI").Select("tb_anno = " & nAnnoAperto, "tb_ucodatti, tb_utipreg, tb_unumreg")
      For i = 0 To dtrT.Length - 1
        ds.Tables("TABDURI").ImportRow(dtrT(i))
        dtrT(i).Delete()
      Next
      oCleAnaz.dsShared.Tables("TABDURI").AcceptChanges()

      frmDuri.Init(oMenu, Nothing, DittaCorrente)
      frmDuri.InitEntity(oCleAnaz, ds, nAnnoAperto)
      frmDuri.ShowDialog()

      '-------------------------------
      'riacquisisco TABDURI
      For i = 0 To ds.Tables("TABDURI").Rows.Count - 1
        If ds.Tables("TABDURI").Rows(i).RowState <> DataRowState.Deleted Then
          oCleAnaz.dsShared.Tables("TABDURI").ImportRow(ds.Tables("TABDURI").Rows(i))
        End If
      Next
      ds.Tables.Clear()
      oCleAnaz.dsShared.Tables("TABDURI").AcceptChanges()
      oCleAnaz.bHasChanges = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmDuri Is Nothing Then frmDuri.Dispose()
      frmDuri = Nothing
      Me.Cursor = Cursors.Default
    End Try
  End Sub

#End Region

  Public Overridable Function SetStato(ByVal nStato As Integer) As Boolean
    Try
      If nStato = 0 Then
        tlbSalva.Enabled = False
        tlbCancella.Enabled = False
        tlbRipristina.Enabled = False
        tlbAttivita.Enabled = False
        tlbRegistri.Enabled = False
        pnControlli.Visible = False
        Me.Text = oApp.Tr(Me, 128765040734014000, "DATI IVA")
      Else
        GctlSetVisEnab(tlbSalva, False)
        GctlSetVisEnab(tlbCancella, False)
        GctlSetVisEnab(tlbRipristina, False)
        GctlSetVisEnab(tlbAttivita, False)
        GctlSetVisEnab(tlbRegistri, False)
        pnControlli.Visible = True
        Me.Text = oApp.Tr(Me, 128648401288593750, "DATI IVA PER ANNO |" & nAnnoAperto.ToString & "|")
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub Nuovo()
    Try
      '-------------------------------------------------
      'creo una nuova forma di pagamento
      If Not Salva() Then Return
      oCleAnaz.AnivNuovo()
      dcAniv.MoveLast()
      dsAniv.Tables("ANAZIVA").Rows(dsAniv.Tables("ANAZIVA").Rows.Count - 1)!ai_aanno = nAnnoAperto

      '-------------------------------------------------
      'imposto i valori di default come impostato nella GCTL
      Me.GctlApplicaDefaultValue()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function ScegliAnno() As Integer
    Dim strTmp As String = DateTime.Now.Year.ToString
    Try
Restart:
      strTmp = DateTime.Now.Year.ToString
      strTmp = oApp.InputBoxNew(oApp.Tr(Me, 128648391209531250, "Anno IVA da aprire:"), strTmp)
      If strTmp.Trim = "" Then strTmp = "0"

      If Not IsNumeric(strTmp) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128648391430156250, "Inserire solo numeri"))
        GoTo Restart
      End If

      If NTSCInt(strTmp) = 0 Then Return 0

      If NTSCInt(strTmp) < 1900 Or NTSCInt(strTmp) > 2099 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128648392112343750, "Inserire solo numeri compresi tra 1900 e 2099"))
        GoTo Restart
      End If

      nAnnoAperto = NTSCInt(strTmp)
      Return NTSCInt(strTmp)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Function Salva() As Boolean
    Dim dRes As DialogResult
    Try
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      If oCleAnaz.AnivRecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129006950144880833, "Confermi il salvataggio?"))
        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oCleAnaz.AnivSalva(False, nAnnoAperto) Then Return False
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

  Public Overridable Sub ckAi_comelfl_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckAi_comelfl.CheckedChanged
    Try

      If ckAi_comelfl.Checked Then
        GctlSetVisEnab(edAi_comelind, False)
        GctlSetVisEnab(cbAi_comeltis, False)
        GctlSetVisEnab(edAi_comelisp, False)
      Else
        edAi_comelind.Text = ""
        cbAi_comeltis.SelectedValue = "P"
        edAi_comelisp.Text = ""

        edAi_comelind.Enabled = False
        cbAi_comeltis.Enabled = False
        edAi_comelisp.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

End Class