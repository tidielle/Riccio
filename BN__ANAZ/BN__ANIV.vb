Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__ANIV
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__ANIV))
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
    Me.lbAi_codivapr = New NTSInformatica.NTSLabel
    Me.edAi_codivapr = New NTSInformatica.NTSTextBoxNum
    Me.fmIvagruppo = New NTSInformatica.NTSGroupBox
    Me.lbXx_codditcg = New NTSInformatica.NTSLabel
    Me.edAi_dtfiivgr = New NTSInformatica.NTSTextBoxData
    Me.lbAi_dtinivgr = New NTSInformatica.NTSLabel
    Me.lbAi_codditcg = New NTSInformatica.NTSLabel
    Me.edAi_dtinivgr = New NTSInformatica.NTSTextBoxData
    Me.ckAi_ivagrup = New NTSInformatica.NTSCheckBox
    Me.edAi_codditcg = New NTSInformatica.NTSTextBoxStr
    Me.lbAi_multatt = New NTSInformatica.NTSLabel
    Me.cbAi_multatt = New NTSInformatica.NTSComboBox
    Me.lbAi_sezliqriep = New NTSInformatica.NTSLabel
    Me.cbAi_sezliqriep = New NTSInformatica.NTSComboBox
    Me.lbXx_codivapr = New NTSInformatica.NTSLabel
    Me.lbAi_verdociva = New NTSInformatica.NTSLabel
    Me.cbAi_verdociva = New NTSInformatica.NTSComboBox
    Me.lbAi_calcacc12 = New NTSInformatica.NTSLabel
    Me.cbAi_calcacc12 = New NTSInformatica.NTSComboBox
    Me.lbAi_gesplaf = New NTSInformatica.NTSLabel
    Me.cbAi_gesplaf = New NTSInformatica.NTSComboBox
    Me.lbAi_comelini = New NTSInformatica.NTSLabel
    Me.edAi_comelini = New NTSInformatica.NTSTextBoxData
    Me.cbAi_comtipop = New NTSInformatica.NTSComboBox
    Me.lbAi_pgulrir = New NTSInformatica.NTSLabel
    Me.edAi_pgulrir = New NTSInformatica.NTSTextBoxNum
    Me.ckAi_comdatiiva = New NTSInformatica.NTSCheckBox
    Me.ckAi_dichanniva = New NTSInformatica.NTSCheckBox
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.pnBottom = New NTSInformatica.NTSPanel
    Me.fmWeb = New NTSInformatica.NTSGroupBox
    Me.lbAi_comelind = New NTSInformatica.NTSLabel
    Me.edAi_comelisp = New NTSInformatica.NTSTextBoxStr
    Me.lbAi_comelisp = New NTSInformatica.NTSLabel
    Me.edAi_comelind = New NTSInformatica.NTSTextBoxStr
    Me.lbAi_comeltis = New NTSInformatica.NTSLabel
    Me.cbAi_comeltis = New NTSInformatica.NTSComboBox
    Me.ckAi_comelfl = New NTSInformatica.NTSCheckBox
    Me.pnLeft = New NTSInformatica.NTSPanel
    Me.pnRight = New NTSInformatica.NTSPanel
    Me.fmIntra = New NTSInformatica.NTSGroupBox
    Me.lbAi_intraperacq = New NTSInformatica.NTSLabel
    Me.cbAi_intraperacq = New NTSInformatica.NTSComboBox
    Me.ckAi_valstacq = New NTSInformatica.NTSCheckBox
    Me.cbAi_intraper = New NTSInformatica.NTSComboBox
    Me.lbAi_intraper = New NTSInformatica.NTSLabel
    Me.ckAi_valstven = New NTSInformatica.NTSCheckBox
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAi_codivapr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmIvagruppo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmIvagruppo.SuspendLayout()
    CType(Me.edAi_dtfiivgr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAi_dtinivgr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAi_ivagrup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAi_codditcg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAi_multatt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAi_sezliqriep.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAi_verdociva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAi_calcacc12.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAi_gesplaf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAi_comelini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAi_comtipop.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAi_pgulrir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAi_comdatiiva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAi_dichanniva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBottom.SuspendLayout()
    CType(Me.fmWeb, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmWeb.SuspendLayout()
    CType(Me.edAi_comelisp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAi_comelind.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAi_comeltis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAi_comelfl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnLeft, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnLeft.SuspendLayout()
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnRight.SuspendLayout()
    CType(Me.fmIntra, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmIntra.SuspendLayout()
    CType(Me.cbAi_intraperacq.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAi_valstacq.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAi_intraper.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAi_valstven.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'lbAi_codivapr
    '
    Me.lbAi_codivapr.AutoSize = True
    Me.lbAi_codivapr.BackColor = System.Drawing.Color.Transparent
    Me.lbAi_codivapr.Location = New System.Drawing.Point(9, 13)
    Me.lbAi_codivapr.Name = "lbAi_codivapr"
    Me.lbAi_codivapr.NTSDbField = ""
    Me.lbAi_codivapr.Size = New System.Drawing.Size(99, 13)
    Me.lbAi_codivapr.TabIndex = 505
    Me.lbAi_codivapr.Text = "Cod. IVA prioritario"
    '
    'edAi_codivapr
    '
    Me.edAi_codivapr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAi_codivapr.EditValue = "0"
    Me.edAi_codivapr.Location = New System.Drawing.Point(181, 10)
    Me.edAi_codivapr.Name = "edAi_codivapr"
    Me.edAi_codivapr.NTSDbField = ""
    Me.edAi_codivapr.NTSFormat = "0"
    Me.edAi_codivapr.NTSForzaVisZoom = False
    Me.edAi_codivapr.NTSOldValue = ""
    Me.edAi_codivapr.Properties.Appearance.Options.UseTextOptions = True
    Me.edAi_codivapr.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAi_codivapr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAi_codivapr.Properties.MaxLength = 65536
    Me.edAi_codivapr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAi_codivapr.Size = New System.Drawing.Size(88, 20)
    Me.edAi_codivapr.TabIndex = 506
    '
    'fmIvagruppo
    '
    Me.fmIvagruppo.AllowDrop = True
    Me.fmIvagruppo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmIvagruppo.Appearance.Options.UseBackColor = True
    Me.fmIvagruppo.Controls.Add(Me.lbXx_codditcg)
    Me.fmIvagruppo.Controls.Add(Me.edAi_dtfiivgr)
    Me.fmIvagruppo.Controls.Add(Me.lbAi_dtinivgr)
    Me.fmIvagruppo.Controls.Add(Me.lbAi_codditcg)
    Me.fmIvagruppo.Controls.Add(Me.edAi_dtinivgr)
    Me.fmIvagruppo.Controls.Add(Me.ckAi_ivagrup)
    Me.fmIvagruppo.Controls.Add(Me.edAi_codditcg)
    Me.fmIvagruppo.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmIvagruppo.Location = New System.Drawing.Point(6, 36)
    Me.fmIvagruppo.Name = "fmIvagruppo"
    Me.fmIvagruppo.Size = New System.Drawing.Size(596, 78)
    Me.fmIvagruppo.TabIndex = 507
    '
    'lbXx_codditcg
    '
    Me.lbXx_codditcg.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codditcg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codditcg.Location = New System.Drawing.Point(269, 23)
    Me.lbXx_codditcg.Name = "lbXx_codditcg"
    Me.lbXx_codditcg.NTSDbField = ""
    Me.lbXx_codditcg.Size = New System.Drawing.Size(318, 20)
    Me.lbXx_codditcg.TabIndex = 516
    Me.lbXx_codditcg.Text = "lbXx_codditcg"
    '
    'edAi_dtfiivgr
    '
    Me.edAi_dtfiivgr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAi_dtfiivgr.EditValue = "01/01/1900"
    Me.edAi_dtfiivgr.Location = New System.Drawing.Point(269, 49)
    Me.edAi_dtfiivgr.Name = "edAi_dtfiivgr"
    Me.edAi_dtfiivgr.NTSDbField = ""
    Me.edAi_dtfiivgr.NTSForzaVisZoom = False
    Me.edAi_dtfiivgr.NTSOldValue = ""
    Me.edAi_dtfiivgr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAi_dtfiivgr.Properties.MaxLength = 65536
    Me.edAi_dtfiivgr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAi_dtfiivgr.Size = New System.Drawing.Size(88, 20)
    Me.edAi_dtfiivgr.TabIndex = 514
    '
    'lbAi_dtinivgr
    '
    Me.lbAi_dtinivgr.AutoSize = True
    Me.lbAi_dtinivgr.BackColor = System.Drawing.Color.Transparent
    Me.lbAi_dtinivgr.Location = New System.Drawing.Point(9, 52)
    Me.lbAi_dtinivgr.Name = "lbAi_dtinivgr"
    Me.lbAi_dtinivgr.NTSDbField = ""
    Me.lbAi_dtinivgr.Size = New System.Drawing.Size(146, 13)
    Me.lbAi_dtinivgr.TabIndex = 510
    Me.lbAi_dtinivgr.Text = "Data inizio/fine IVA di gruppo"
    '
    'lbAi_codditcg
    '
    Me.lbAi_codditcg.AutoSize = True
    Me.lbAi_codditcg.BackColor = System.Drawing.Color.Transparent
    Me.lbAi_codditcg.Location = New System.Drawing.Point(9, 30)
    Me.lbAi_codditcg.Name = "lbAi_codditcg"
    Me.lbAi_codditcg.NTSDbField = ""
    Me.lbAi_codditcg.Size = New System.Drawing.Size(90, 13)
    Me.lbAi_codditcg.TabIndex = 509
    Me.lbAi_codditcg.Text = "Ditta capogruppo"
    '
    'edAi_dtinivgr
    '
    Me.edAi_dtinivgr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAi_dtinivgr.EditValue = "01/01/1900"
    Me.edAi_dtinivgr.Location = New System.Drawing.Point(175, 49)
    Me.edAi_dtinivgr.Name = "edAi_dtinivgr"
    Me.edAi_dtinivgr.NTSDbField = ""
    Me.edAi_dtinivgr.NTSForzaVisZoom = False
    Me.edAi_dtinivgr.NTSOldValue = ""
    Me.edAi_dtinivgr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAi_dtinivgr.Properties.MaxLength = 65536
    Me.edAi_dtinivgr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAi_dtinivgr.Size = New System.Drawing.Size(88, 20)
    Me.edAi_dtinivgr.TabIndex = 513
    '
    'ckAi_ivagrup
    '
    Me.ckAi_ivagrup.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAi_ivagrup.Location = New System.Drawing.Point(6, 0)
    Me.ckAi_ivagrup.Name = "ckAi_ivagrup"
    Me.ckAi_ivagrup.NTSCheckValue = "S"
    Me.ckAi_ivagrup.NTSUnCheckValue = "N"
    Me.ckAi_ivagrup.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAi_ivagrup.Properties.Appearance.Options.UseBackColor = True
    Me.ckAi_ivagrup.Properties.Caption = "Iva di gruppo"
    Me.ckAi_ivagrup.Size = New System.Drawing.Size(93, 19)
    Me.ckAi_ivagrup.TabIndex = 515
    '
    'edAi_codditcg
    '
    Me.edAi_codditcg.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAi_codditcg.Location = New System.Drawing.Point(175, 23)
    Me.edAi_codditcg.Name = "edAi_codditcg"
    Me.edAi_codditcg.NTSDbField = ""
    Me.edAi_codditcg.NTSForzaVisZoom = False
    Me.edAi_codditcg.NTSOldValue = ""
    Me.edAi_codditcg.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAi_codditcg.Properties.MaxLength = 65536
    Me.edAi_codditcg.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAi_codditcg.Size = New System.Drawing.Size(88, 20)
    Me.edAi_codditcg.TabIndex = 512
    '
    'lbAi_multatt
    '
    Me.lbAi_multatt.AutoSize = True
    Me.lbAi_multatt.BackColor = System.Drawing.Color.Transparent
    Me.lbAi_multatt.Location = New System.Drawing.Point(9, 3)
    Me.lbAi_multatt.Name = "lbAi_multatt"
    Me.lbAi_multatt.NTSDbField = ""
    Me.lbAi_multatt.Size = New System.Drawing.Size(106, 13)
    Me.lbAi_multatt.TabIndex = 510
    Me.lbAi_multatt.Text = "Gestione attività IVA"
    '
    'cbAi_multatt
    '
    Me.cbAi_multatt.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAi_multatt.DataSource = Nothing
    Me.cbAi_multatt.DisplayMember = ""
    Me.cbAi_multatt.Location = New System.Drawing.Point(181, 0)
    Me.cbAi_multatt.Name = "cbAi_multatt"
    Me.cbAi_multatt.NTSDbField = ""
    Me.cbAi_multatt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAi_multatt.Properties.DropDownRows = 30
    Me.cbAi_multatt.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAi_multatt.SelectedValue = ""
    Me.cbAi_multatt.Size = New System.Drawing.Size(182, 20)
    Me.cbAi_multatt.TabIndex = 511
    Me.cbAi_multatt.ValueMember = ""
    '
    'lbAi_sezliqriep
    '
    Me.lbAi_sezliqriep.AutoSize = True
    Me.lbAi_sezliqriep.BackColor = System.Drawing.Color.Transparent
    Me.lbAi_sezliqriep.Location = New System.Drawing.Point(9, 29)
    Me.lbAi_sezliqriep.Name = "lbAi_sezliqriep"
    Me.lbAi_sezliqriep.NTSDbField = ""
    Me.lbAi_sezliqriep.Size = New System.Drawing.Size(163, 13)
    Me.lbAi_sezliqriep.TabIndex = 512
    Me.lbAi_sezliqriep.Text = "Sezione per stampa liquidaz. IVA"
    '
    'cbAi_sezliqriep
    '
    Me.cbAi_sezliqriep.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAi_sezliqriep.DataSource = Nothing
    Me.cbAi_sezliqriep.DisplayMember = ""
    Me.cbAi_sezliqriep.Location = New System.Drawing.Point(181, 26)
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
    'lbXx_codivapr
    '
    Me.lbXx_codivapr.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codivapr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codivapr.Location = New System.Drawing.Point(275, 10)
    Me.lbXx_codivapr.Name = "lbXx_codivapr"
    Me.lbXx_codivapr.NTSDbField = ""
    Me.lbXx_codivapr.Size = New System.Drawing.Size(318, 20)
    Me.lbXx_codivapr.TabIndex = 517
    Me.lbXx_codivapr.Text = "lbXx_codivapr"
    '
    'lbAi_verdociva
    '
    Me.lbAi_verdociva.AutoSize = True
    Me.lbAi_verdociva.BackColor = System.Drawing.Color.Transparent
    Me.lbAi_verdociva.Location = New System.Drawing.Point(9, 55)
    Me.lbAi_verdociva.Name = "lbAi_verdociva"
    Me.lbAi_verdociva.NTSDbField = ""
    Me.lbAi_verdociva.Size = New System.Drawing.Size(150, 13)
    Me.lbAi_verdociva.TabIndex = 518
    Me.lbAi_verdociva.Text = "Verifica preesistenza doc. IVA"
    '
    'cbAi_verdociva
    '
    Me.cbAi_verdociva.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAi_verdociva.DataSource = Nothing
    Me.cbAi_verdociva.DisplayMember = ""
    Me.cbAi_verdociva.Location = New System.Drawing.Point(181, 52)
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
    Me.lbAi_calcacc12.Location = New System.Drawing.Point(9, 81)
    Me.lbAi_calcacc12.Name = "lbAi_calcacc12"
    Me.lbAi_calcacc12.NTSDbField = ""
    Me.lbAi_calcacc12.Size = New System.Drawing.Size(128, 13)
    Me.lbAi_calcacc12.TabIndex = 520
    Me.lbAi_calcacc12.Text = "Calcolo acconto dicembre"
    '
    'cbAi_calcacc12
    '
    Me.cbAi_calcacc12.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAi_calcacc12.DataSource = Nothing
    Me.cbAi_calcacc12.DisplayMember = ""
    Me.cbAi_calcacc12.Location = New System.Drawing.Point(181, 78)
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
    'lbAi_gesplaf
    '
    Me.lbAi_gesplaf.AutoSize = True
    Me.lbAi_gesplaf.BackColor = System.Drawing.Color.Transparent
    Me.lbAi_gesplaf.Location = New System.Drawing.Point(9, 107)
    Me.lbAi_gesplaf.Name = "lbAi_gesplaf"
    Me.lbAi_gesplaf.NTSDbField = ""
    Me.lbAi_gesplaf.Size = New System.Drawing.Size(88, 13)
    Me.lbAi_gesplaf.TabIndex = 521
    Me.lbAi_gesplaf.Text = "Gestione plafond"
    '
    'cbAi_gesplaf
    '
    Me.cbAi_gesplaf.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAi_gesplaf.DataSource = Nothing
    Me.cbAi_gesplaf.DisplayMember = ""
    Me.cbAi_gesplaf.Location = New System.Drawing.Point(181, 104)
    Me.cbAi_gesplaf.Name = "cbAi_gesplaf"
    Me.cbAi_gesplaf.NTSDbField = ""
    Me.cbAi_gesplaf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAi_gesplaf.Properties.DropDownRows = 30
    Me.cbAi_gesplaf.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAi_gesplaf.SelectedValue = ""
    Me.cbAi_gesplaf.Size = New System.Drawing.Size(182, 20)
    Me.cbAi_gesplaf.TabIndex = 523
    Me.cbAi_gesplaf.ValueMember = ""
    '
    'lbAi_comelini
    '
    Me.lbAi_comelini.AutoSize = True
    Me.lbAi_comelini.BackColor = System.Drawing.Color.Transparent
    Me.lbAi_comelini.Location = New System.Drawing.Point(9, 159)
    Me.lbAi_comelini.Name = "lbAi_comelini"
    Me.lbAi_comelini.NTSDbField = ""
    Me.lbAi_comelini.Size = New System.Drawing.Size(158, 13)
    Me.lbAi_comelini.TabIndex = 524
    Me.lbAi_comelini.Text = "Data / tipo operaz. mod. AA7/9"
    '
    'edAi_comelini
    '
    Me.edAi_comelini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAi_comelini.EditValue = "01/01/1900"
    Me.edAi_comelini.Location = New System.Drawing.Point(181, 156)
    Me.edAi_comelini.Name = "edAi_comelini"
    Me.edAi_comelini.NTSDbField = ""
    Me.edAi_comelini.NTSForzaVisZoom = False
    Me.edAi_comelini.NTSOldValue = ""
    Me.edAi_comelini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAi_comelini.Properties.MaxLength = 65536
    Me.edAi_comelini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAi_comelini.Size = New System.Drawing.Size(88, 20)
    Me.edAi_comelini.TabIndex = 527
    '
    'cbAi_comtipop
    '
    Me.cbAi_comtipop.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAi_comtipop.DataSource = Nothing
    Me.cbAi_comtipop.DisplayMember = ""
    Me.cbAi_comtipop.Location = New System.Drawing.Point(275, 156)
    Me.cbAi_comtipop.Name = "cbAi_comtipop"
    Me.cbAi_comtipop.NTSDbField = ""
    Me.cbAi_comtipop.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAi_comtipop.Properties.DropDownRows = 30
    Me.cbAi_comtipop.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAi_comtipop.SelectedValue = ""
    Me.cbAi_comtipop.Size = New System.Drawing.Size(88, 20)
    Me.cbAi_comtipop.TabIndex = 528
    Me.cbAi_comtipop.ValueMember = ""
    '
    'lbAi_pgulrir
    '
    Me.lbAi_pgulrir.AutoSize = True
    Me.lbAi_pgulrir.BackColor = System.Drawing.Color.Transparent
    Me.lbAi_pgulrir.Location = New System.Drawing.Point(9, 133)
    Me.lbAi_pgulrir.Name = "lbAi_pgulrir"
    Me.lbAi_pgulrir.NTSDbField = ""
    Me.lbAi_pgulrir.Size = New System.Drawing.Size(169, 13)
    Me.lbAi_pgulrir.TabIndex = 526
    Me.lbAi_pgulrir.Text = "N° ultima pagina reg. riepilogativo"
    '
    'edAi_pgulrir
    '
    Me.edAi_pgulrir.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAi_pgulrir.EditValue = "0"
    Me.edAi_pgulrir.Location = New System.Drawing.Point(181, 130)
    Me.edAi_pgulrir.Name = "edAi_pgulrir"
    Me.edAi_pgulrir.NTSDbField = ""
    Me.edAi_pgulrir.NTSFormat = "0"
    Me.edAi_pgulrir.NTSForzaVisZoom = False
    Me.edAi_pgulrir.NTSOldValue = ""
    Me.edAi_pgulrir.Properties.Appearance.Options.UseTextOptions = True
    Me.edAi_pgulrir.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAi_pgulrir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAi_pgulrir.Properties.MaxLength = 65536
    Me.edAi_pgulrir.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAi_pgulrir.Size = New System.Drawing.Size(88, 20)
    Me.edAi_pgulrir.TabIndex = 529
    '
    'ckAi_comdatiiva
    '
    Me.ckAi_comdatiiva.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAi_comdatiiva.Location = New System.Drawing.Point(6, 26)
    Me.ckAi_comdatiiva.Name = "ckAi_comdatiiva"
    Me.ckAi_comdatiiva.NTSCheckValue = "S"
    Me.ckAi_comdatiiva.NTSUnCheckValue = "N"
    Me.ckAi_comdatiiva.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAi_comdatiiva.Properties.Appearance.Options.UseBackColor = True
    Me.ckAi_comdatiiva.Properties.Caption = "Soggetto a comunicaz. annuale IVA"
    Me.ckAi_comdatiiva.Size = New System.Drawing.Size(199, 19)
    Me.ckAi_comdatiiva.TabIndex = 530
    '
    'ckAi_dichanniva
    '
    Me.ckAi_dichanniva.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAi_dichanniva.Location = New System.Drawing.Point(6, 1)
    Me.ckAi_dichanniva.Name = "ckAi_dichanniva"
    Me.ckAi_dichanniva.NTSCheckValue = "S"
    Me.ckAi_dichanniva.NTSUnCheckValue = "N"
    Me.ckAi_dichanniva.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAi_dichanniva.Properties.Appearance.Options.UseBackColor = True
    Me.ckAi_dichanniva.Properties.Caption = "Soggetto a dichiaraz. annuale IVA"
    Me.ckAi_dichanniva.Size = New System.Drawing.Size(193, 19)
    Me.ckAi_dichanniva.TabIndex = 531
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.fmIvagruppo)
    Me.pnTop.Controls.Add(Me.edAi_codivapr)
    Me.pnTop.Controls.Add(Me.lbAi_codivapr)
    Me.pnTop.Controls.Add(Me.lbXx_codivapr)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.Size = New System.Drawing.Size(610, 119)
    Me.pnTop.TabIndex = 532
    Me.pnTop.Text = "NtsPanel1"
    '
    'pnBottom
    '
    Me.pnBottom.AllowDrop = True
    Me.pnBottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBottom.Appearance.Options.UseBackColor = True
    Me.pnBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBottom.Controls.Add(Me.fmWeb)
    Me.pnBottom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnBottom.Location = New System.Drawing.Point(0, 339)
    Me.pnBottom.Name = "pnBottom"
    Me.pnBottom.Size = New System.Drawing.Size(610, 94)
    Me.pnBottom.TabIndex = 533
    Me.pnBottom.Text = "NtsPanel1"
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
    Me.fmWeb.Location = New System.Drawing.Point(6, 6)
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
    '
    'edAi_comelisp
    '
    Me.edAi_comelisp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAi_comelisp.Location = New System.Drawing.Point(269, 49)
    Me.edAi_comelisp.Name = "edAi_comelisp"
    Me.edAi_comelisp.NTSDbField = ""
    Me.edAi_comelisp.NTSForzaVisZoom = False
    Me.edAi_comelisp.NTSOldValue = ""
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
    '
    'edAi_comelind
    '
    Me.edAi_comelind.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAi_comelind.Location = New System.Drawing.Point(61, 23)
    Me.edAi_comelind.Name = "edAi_comelind"
    Me.edAi_comelind.NTSDbField = ""
    Me.edAi_comelind.NTSForzaVisZoom = False
    Me.edAi_comelind.NTSOldValue = ""
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
    'pnLeft
    '
    Me.pnLeft.AllowDrop = True
    Me.pnLeft.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnLeft.Appearance.Options.UseBackColor = True
    Me.pnLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnLeft.Controls.Add(Me.lbAi_multatt)
    Me.pnLeft.Controls.Add(Me.cbAi_multatt)
    Me.pnLeft.Controls.Add(Me.cbAi_sezliqriep)
    Me.pnLeft.Controls.Add(Me.lbAi_sezliqriep)
    Me.pnLeft.Controls.Add(Me.cbAi_verdociva)
    Me.pnLeft.Controls.Add(Me.lbAi_comelini)
    Me.pnLeft.Controls.Add(Me.lbAi_verdociva)
    Me.pnLeft.Controls.Add(Me.edAi_comelini)
    Me.pnLeft.Controls.Add(Me.cbAi_gesplaf)
    Me.pnLeft.Controls.Add(Me.cbAi_comtipop)
    Me.pnLeft.Controls.Add(Me.lbAi_gesplaf)
    Me.pnLeft.Controls.Add(Me.lbAi_pgulrir)
    Me.pnLeft.Controls.Add(Me.cbAi_calcacc12)
    Me.pnLeft.Controls.Add(Me.edAi_pgulrir)
    Me.pnLeft.Controls.Add(Me.lbAi_calcacc12)
    Me.pnLeft.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnLeft.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnLeft.Location = New System.Drawing.Point(0, 149)
    Me.pnLeft.Name = "pnLeft"
    Me.pnLeft.Size = New System.Drawing.Size(370, 190)
    Me.pnLeft.TabIndex = 534
    Me.pnLeft.Text = "NtsPanel1"
    '
    'pnRight
    '
    Me.pnRight.AllowDrop = True
    Me.pnRight.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnRight.Appearance.Options.UseBackColor = True
    Me.pnRight.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnRight.Controls.Add(Me.fmIntra)
    Me.pnRight.Controls.Add(Me.ckAi_dichanniva)
    Me.pnRight.Controls.Add(Me.ckAi_comdatiiva)
    Me.pnRight.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnRight.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnRight.Location = New System.Drawing.Point(370, 149)
    Me.pnRight.Name = "pnRight"
    Me.pnRight.Size = New System.Drawing.Size(240, 190)
    Me.pnRight.TabIndex = 535
    Me.pnRight.Text = "NtsPanel1"
    '
    'fmIntra
    '
    Me.fmIntra.AllowDrop = True
    Me.fmIntra.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmIntra.Appearance.Options.UseBackColor = True
    Me.fmIntra.Controls.Add(Me.lbAi_intraperacq)
    Me.fmIntra.Controls.Add(Me.cbAi_intraperacq)
    Me.fmIntra.Controls.Add(Me.ckAi_valstacq)
    Me.fmIntra.Controls.Add(Me.cbAi_intraper)
    Me.fmIntra.Controls.Add(Me.lbAi_intraper)
    Me.fmIntra.Controls.Add(Me.ckAi_valstven)
    Me.fmIntra.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmIntra.Location = New System.Drawing.Point(3, 55)
    Me.fmIntra.Name = "fmIntra"
    Me.fmIntra.Size = New System.Drawing.Size(229, 131)
    Me.fmIntra.TabIndex = 532
    Me.fmIntra.Text = "Intrastat"
    '
    'lbAi_intraperacq
    '
    Me.lbAi_intraperacq.AutoSize = True
    Me.lbAi_intraperacq.BackColor = System.Drawing.Color.Transparent
    Me.lbAi_intraperacq.Location = New System.Drawing.Point(5, 104)
    Me.lbAi_intraperacq.Name = "lbAi_intraperacq"
    Me.lbAi_intraperacq.NTSDbField = ""
    Me.lbAi_intraperacq.Size = New System.Drawing.Size(109, 13)
    Me.lbAi_intraperacq.TabIndex = 507
    Me.lbAi_intraperacq.Text = "Periodo Intra acquisti"
    '
    'cbAi_intraperacq
    '
    Me.cbAi_intraperacq.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAi_intraperacq.DataSource = Nothing
    Me.cbAi_intraperacq.DisplayMember = ""
    Me.cbAi_intraperacq.Location = New System.Drawing.Point(120, 101)
    Me.cbAi_intraperacq.Name = "cbAi_intraperacq"
    Me.cbAi_intraperacq.NTSDbField = ""
    Me.cbAi_intraperacq.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAi_intraperacq.Properties.DropDownRows = 30
    Me.cbAi_intraperacq.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAi_intraperacq.SelectedValue = ""
    Me.cbAi_intraperacq.Size = New System.Drawing.Size(100, 20)
    Me.cbAi_intraperacq.TabIndex = 508
    Me.cbAi_intraperacq.ValueMember = ""
    '
    'ckAi_valstacq
    '
    Me.ckAi_valstacq.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAi_valstacq.Location = New System.Drawing.Point(5, 75)
    Me.ckAi_valstacq.Name = "ckAi_valstacq"
    Me.ckAi_valstacq.NTSCheckValue = "S"
    Me.ckAi_valstacq.NTSUnCheckValue = "N"
    Me.ckAi_valstacq.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAi_valstacq.Properties.Appearance.Options.UseBackColor = True
    Me.ckAi_valstacq.Properties.Caption = "Valore statistico su acquisti"
    Me.ckAi_valstacq.Size = New System.Drawing.Size(160, 19)
    Me.ckAi_valstacq.TabIndex = 506
    '
    'cbAi_intraper
    '
    Me.cbAi_intraper.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAi_intraper.DataSource = Nothing
    Me.cbAi_intraper.DisplayMember = ""
    Me.cbAi_intraper.Location = New System.Drawing.Point(120, 49)
    Me.cbAi_intraper.Name = "cbAi_intraper"
    Me.cbAi_intraper.NTSDbField = ""
    Me.cbAi_intraper.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAi_intraper.Properties.DropDownRows = 30
    Me.cbAi_intraper.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAi_intraper.SelectedValue = ""
    Me.cbAi_intraper.Size = New System.Drawing.Size(100, 20)
    Me.cbAi_intraper.TabIndex = 505
    Me.cbAi_intraper.ValueMember = ""
    '
    'lbAi_intraper
    '
    Me.lbAi_intraper.AutoSize = True
    Me.lbAi_intraper.BackColor = System.Drawing.Color.Transparent
    Me.lbAi_intraper.Location = New System.Drawing.Point(5, 52)
    Me.lbAi_intraper.Name = "lbAi_intraper"
    Me.lbAi_intraper.NTSDbField = ""
    Me.lbAi_intraper.Size = New System.Drawing.Size(109, 13)
    Me.lbAi_intraper.TabIndex = 504
    Me.lbAi_intraper.Text = "Periodo Intra vendite"
    '
    'ckAi_valstven
    '
    Me.ckAi_valstven.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAi_valstven.Location = New System.Drawing.Point(5, 23)
    Me.ckAi_valstven.Name = "ckAi_valstven"
    Me.ckAi_valstven.NTSCheckValue = "S"
    Me.ckAi_valstven.NTSUnCheckValue = "N"
    Me.ckAi_valstven.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAi_valstven.Properties.Appearance.Options.UseBackColor = True
    Me.ckAi_valstven.Properties.Caption = "Valore statistico su vendite"
    Me.ckAi_valstven.Size = New System.Drawing.Size(154, 19)
    Me.ckAi_valstven.TabIndex = 503
    '
    'FRM__ANIV
    '
    Me.ClientSize = New System.Drawing.Size(610, 433)
    Me.Controls.Add(Me.pnRight)
    Me.Controls.Add(Me.pnLeft)
    Me.Controls.Add(Me.pnBottom)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__ANIV"
    Me.Text = "DATI IVA"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAi_codivapr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmIvagruppo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmIvagruppo.ResumeLayout(False)
    Me.fmIvagruppo.PerformLayout()
    CType(Me.edAi_dtfiivgr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAi_dtinivgr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAi_ivagrup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAi_codditcg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAi_multatt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAi_sezliqriep.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAi_verdociva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAi_calcacc12.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAi_gesplaf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAi_comelini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAi_comtipop.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAi_pgulrir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAi_comdatiiva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAi_dichanniva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBottom.ResumeLayout(False)
    CType(Me.fmWeb, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmWeb.ResumeLayout(False)
    Me.fmWeb.PerformLayout()
    CType(Me.edAi_comelisp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAi_comelind.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAi_comeltis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAi_comelfl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnLeft, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnLeft.ResumeLayout(False)
    Me.pnLeft.PerformLayout()
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnRight.ResumeLayout(False)
    Me.pnRight.PerformLayout()
    CType(Me.fmIntra, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmIntra.ResumeLayout(False)
    Me.fmIntra.PerformLayout()
    CType(Me.cbAi_intraperacq.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAi_valstacq.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAi_intraper.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAi_valstven.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
      cbAi_intraper.NTSSetParam(oApp.Tr(Me, 128648358519843750, "Periodo Intra vendite"))
      cbAi_intraperacq.NTSSetParam(oApp.Tr(Me, 128648358528593750, "Periodo Intra acquisti"))
      ckAi_valstven.NTSSetParam(oMenu, oApp.Tr(Me, 128648358541406250, "Valore statistico su vendite"), "S", "N")
      ckAi_valstacq.NTSSetParam(oMenu, oApp.Tr(Me, 128648358554375000, "Valore statistico su acquisti"), "S", "N")
      edAi_codivapr.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128648358567187500, "Cod. IVA prioritario"), tabciva)
      ckAi_ivagrup.NTSSetParam(oMenu, oApp.Tr(Me, 128648358581406250, "Iva di gruppo"), "S", "N")
      edAi_codditcg.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128648358595156250, "Ditta capogruppo"), tabanaz, True)
      edAi_dtinivgr.NTSSetParam(oMenu, oApp.Tr(Me, 128648358607031250, "Data inizio/fine IVA di gruppo"), True, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      edAi_dtfiivgr.NTSSetParam(oMenu, oApp.Tr(Me, 128648358619531250, "data fine iva di gruppo"), True, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      cbAi_multatt.NTSSetParam(oApp.Tr(Me, 128648358633593750, "Gestione attività IVA"))
      cbAi_sezliqriep.NTSSetParam(oApp.Tr(Me, 128648358646875000, "Sezione per stampa liquidaz. IVA"))
      ckAi_comdatiiva.NTSSetParam(oMenu, oApp.Tr(Me, 128648358660000000, "Soggetto a comunicaz. annuale IVA"), "S", "N")
      ckAi_dichanniva.NTSSetParam(oMenu, oApp.Tr(Me, 128648358672656250, "Soggetto a dichiaraz. annuale IVA"), "S", "N")
      cbAi_verdociva.NTSSetParam(oApp.Tr(Me, 128648358700156250, "Verifica preesistenza doc. IVA"))
      cbAi_calcacc12.NTSSetParam(oApp.Tr(Me, 128648358712187500, "Calcolo acconto dicembre"))
      cbAi_gesplaf.NTSSetParam(oApp.Tr(Me, 128648358725468750, "Gestione plafond"))
      ckAi_comelfl.NTSSetParam(oMenu, oApp.Tr(Me, 128648358738281250, "Commercio elettronico"), "S", "N")
      edAi_comelind.NTSSetParam(oMenu, oApp.Tr(Me, 128648358750937500, "Indirizzo Web"), 100, True)
      cbAi_comeltis.NTSSetParam(oApp.Tr(Me, 128648358763437500, "Tipo sito"))
      edAi_comelisp.NTSSetParam(oMenu, oApp.Tr(Me, 128648358777031250, "Service provider"), 100, True)
      edAi_comelini.NTSSetParam(oMenu, oApp.Tr(Me, 128648358795937500, "Data operazione (per modello AA7/9)"), True, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      cbAi_comtipop.NTSSetParam(oApp.Tr(Me, 128648358810468750, "Tipo operazione"))
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
      Dim dttIntraper As New DataTable()
      dttIntraper.Columns.Add("cod", GetType(String))
      dttIntraper.Columns.Add("val", GetType(String))
      dttIntraper.Rows.Add(New Object() {"M", "Mese"})
      dttIntraper.Rows.Add(New Object() {"T", "Trimestre"})
      dttIntraper.Rows.Add(New Object() {"A", "Anno"})
      dttIntraper.Rows.Add(New Object() {"N", "Non soggetto"})
      dttIntraper.AcceptChanges()
      cbAi_intraper.DataSource = dttIntraper
      cbAi_intraper.ValueMember = "cod"
      cbAi_intraper.DisplayMember = "val"

      '---------------------------
      Dim dttIntraperacq As New DataTable()
      dttIntraperacq.Columns.Add("cod", GetType(String))
      dttIntraperacq.Columns.Add("val", GetType(String))
      dttIntraperacq.Rows.Add(New Object() {"M", "Mese"})
      dttIntraperacq.Rows.Add(New Object() {"T", "Trimestre"})
      dttIntraperacq.Rows.Add(New Object() {"A", "Anno"})
      dttIntraperacq.Rows.Add(New Object() {"N", "Non soggetto"})
      dttIntraperacq.AcceptChanges()
      cbAi_intraperacq.DataSource = dttIntraperacq
      cbAi_intraperacq.ValueMember = "cod"
      cbAi_intraperacq.DisplayMember = "val"

      '---------------------------
      Dim dttMultatt As New DataTable()
      dttMultatt.Columns.Add("cod", GetType(String))
      dttMultatt.Columns.Add("val", GetType(String))
      dttMultatt.Rows.Add(New Object() {"N", "Unica"})
      dttMultatt.Rows.Add(New Object() {"S", "Separata per obbligo"})
      dttMultatt.Rows.Add(New Object() {"O", "Separata per opzione"})
      dttMultatt.Rows.Add(New Object() {"I", "Separata non ai fini Iva"})
      dttMultatt.AcceptChanges()
      cbAi_multatt.DataSource = dttMultatt
      cbAi_multatt.ValueMember = "cod"
      cbAi_multatt.DisplayMember = "val"

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
      Dim dttGesplaf As New DataTable()
      dttGesplaf.Columns.Add("cod", GetType(String))
      dttGesplaf.Columns.Add("val", GetType(String))
      dttGesplaf.Rows.Add(New Object() {"N", "No"})
      dttGesplaf.Rows.Add(New Object() {"S", "Fisso"})
      dttGesplaf.Rows.Add(New Object() {"M", "Mobile"})
      dttGesplaf.AcceptChanges()
      cbAi_gesplaf.DataSource = dttGesplaf
      cbAi_gesplaf.ValueMember = "cod"
      cbAi_gesplaf.DisplayMember = "val"

      '---------------------------
      Dim dttComtipop As New DataTable()
      dttComtipop.Columns.Add("cod", GetType(String))
      dttComtipop.Columns.Add("val", GetType(String))
      dttComtipop.Rows.Add(New Object() {"A", "Apertura"})
      dttComtipop.Rows.Add(New Object() {"V", "Variazione"})
      dttComtipop.Rows.Add(New Object() {"C", "Chiusura"})
      dttComtipop.AcceptChanges()
      cbAi_comtipop.DataSource = dttComtipop
      cbAi_comtipop.ValueMember = "cod"
      cbAi_comtipop.DisplayMember = "val"

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
      cbAi_intraper.NTSDbField = "ANAZIVA.ai_intraper"
      cbAi_intraperacq.NTSDbField = "ANAZIVA.ai_intraperacq"
      ckAi_valstven.NTSText.NTSDbField = "ANAZIVA.ai_valstven"
      ckAi_valstacq.NTSText.NTSDbField = "ANAZIVA.ai_valstacq"
      edAi_codivapr.NTSDbField = "ANAZIVA.ai_codivapr"
      ckAi_ivagrup.NTSText.NTSDbField = "ANAZIVA.ai_ivagrup"
      edAi_codditcg.NTSDbField = "ANAZIVA.ai_codditcg"
      edAi_dtinivgr.NTSDbField = "ANAZIVA.ai_dtinivgr"
      edAi_dtfiivgr.NTSDbField = "ANAZIVA.ai_dtfiivgr"
      cbAi_multatt.NTSDbField = "ANAZIVA.ai_multatt"
      cbAi_sezliqriep.NTSDbField = "ANAZIVA.ai_sezliqriep"
      ckAi_comdatiiva.NTSText.NTSDbField = "ANAZIVA.ai_comdatiiva"
      ckAi_dichanniva.NTSText.NTSDbField = "ANAZIVA.ai_dichanniva"
      cbAi_verdociva.NTSDbField = "ANAZIVA.ai_verdociva"
      cbAi_calcacc12.NTSDbField = "ANAZIVA.ai_calcacc12"
      cbAi_gesplaf.NTSDbField = "ANAZIVA.ai_gesplaf"
      ckAi_comelfl.NTSText.NTSDbField = "ANAZIVA.ai_comelfl"
      edAi_comelind.NTSDbField = "ANAZIVA.ai_comelind"
      cbAi_comeltis.NTSDbField = "ANAZIVA.ai_comeltis"
      edAi_comelisp.NTSDbField = "ANAZIVA.ai_comelisp"
      edAi_comelini.NTSDbField = "ANAZIVA.ai_comelini"
      cbAi_comtipop.NTSDbField = "ANAZIVA.ai_comtipop"
      edAi_pgulrir.NTSDbField = "ANAZIVA.ai_pgulrir"
      lbXx_codditcg.NTSDbField = "ANAZIVA.xx_codditcg"
      lbXx_codivapr.NTSDbField = "ANAZIVA.xx_codivapr"

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

      ckAi_ivagrup_CheckedChanged(ckAi_ivagrup, Nothing)
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
        pnTop.Visible = False
        pnBottom.Visible = False
        pnLeft.Visible = False
        pnRight.Visible = False
        Me.Text = oApp.Tr(Me, 128765040734014000, "DATI IVA")
      Else
        GctlSetVisEnab(tlbSalva, False)
        GctlSetVisEnab(tlbCancella, False)
        GctlSetVisEnab(tlbRipristina, False)
        GctlSetVisEnab(tlbAttivita, False)
        GctlSetVisEnab(tlbRegistri, False)
        pnTop.Visible = True
        pnBottom.Visible = True
        pnLeft.Visible = True
        pnRight.Visible = True
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

  Public Overridable Sub ckAi_ivagrup_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckAi_ivagrup.CheckedChanged
    Try

      If ckAi_ivagrup.Checked Then
        GctlSetVisEnab(edAi_codditcg, False)
        GctlSetVisEnab(edAi_dtinivgr, False)
        GctlSetVisEnab(edAi_dtfiivgr, False)
      Else
        edAi_codditcg.Text = ""
        edAi_dtinivgr.Text = ""
        edAi_dtfiivgr.Text = ""
        edAi_codditcg.Enabled = False
        edAi_dtinivgr.Enabled = False
        edAi_dtfiivgr.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

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

