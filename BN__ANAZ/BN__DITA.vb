Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__DITA
  Public oCleAnaz As CLE__ANAZ
  Public dsDita As DataSet
  Public oCallParams As CLE__CLDP
  Public dcDita As BindingSource = New BindingSource

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
  Public WithEvents lbAc_xlsric As NTSInformatica.NTSLabel
  Public WithEvents ckAc_flrifboi As NTSInformatica.NTSCheckBox
  Public WithEvents lbAc_xlscee As NTSInformatica.NTSLabel
  Public WithEvents ckAc_geststanz As NTSInformatica.NTSCheckBox
  Public WithEvents ckAc_cespint As NTSInformatica.NTSCheckBox
  Public WithEvents lbXx_cdtivapri As NTSInformatica.NTSLabel
  Public WithEvents lbAc_cdtivapri As NTSInformatica.NTSLabel
  Public WithEvents edAc_cdtivapri As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAc_xlscee As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAc_xlsric As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAc_flintbol As NTSInformatica.NTSLabel
  Public WithEvents lbAc_dtinipre As NTSInformatica.NTSLabel
  Public WithEvents edAc_dtfinpre As NTSInformatica.NTSTextBoxData
  Public WithEvents edAc_dtinipre As NTSInformatica.NTSTextBoxData
  Public WithEvents lbAc_tpdesagg As NTSInformatica.NTSLabel
  Public WithEvents cbAc_tpdesagg As NTSInformatica.NTSComboBox
  Public WithEvents cbAc_flintbol As NTSInformatica.NTSComboBox
  Public WithEvents ckAc_gestefcc As NTSInformatica.NTSCheckBox
  Public WithEvents ckAc_gesived As NTSInformatica.NTSCheckBox
  Public WithEvents ckAc_percint As NTSInformatica.NTSCheckBox
  Public WithEvents lbAc_flgiobol As NTSInformatica.NTSLabel
  Public WithEvents cbAc_flgiobol As NTSInformatica.NTSComboBox

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__DITA))
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
    Me.lbAc_xlsric = New NTSInformatica.NTSLabel
    Me.ckAc_flrifboi = New NTSInformatica.NTSCheckBox
    Me.lbAc_xlscee = New NTSInformatica.NTSLabel
    Me.lbAc_cdtivapri = New NTSInformatica.NTSLabel
    Me.edAc_cdtivapri = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_cdtivapri = New NTSInformatica.NTSLabel
    Me.ckAc_cespint = New NTSInformatica.NTSCheckBox
    Me.ckAc_geststanz = New NTSInformatica.NTSCheckBox
    Me.edAc_xlsric = New NTSInformatica.NTSTextBoxStr
    Me.edAc_xlscee = New NTSInformatica.NTSTextBoxStr
    Me.edAc_dtinipre = New NTSInformatica.NTSTextBoxData
    Me.edAc_dtfinpre = New NTSInformatica.NTSTextBoxData
    Me.lbAc_dtinipre = New NTSInformatica.NTSLabel
    Me.lbAc_flintbol = New NTSInformatica.NTSLabel
    Me.cbAc_flintbol = New NTSInformatica.NTSComboBox
    Me.cbAc_tpdesagg = New NTSInformatica.NTSComboBox
    Me.lbAc_tpdesagg = New NTSInformatica.NTSLabel
    Me.cbAc_flgiobol = New NTSInformatica.NTSComboBox
    Me.lbAc_flgiobol = New NTSInformatica.NTSLabel
    Me.ckAc_percint = New NTSInformatica.NTSCheckBox
    Me.ckAc_gesived = New NTSInformatica.NTSCheckBox
    Me.ckAc_gestefcc = New NTSInformatica.NTSCheckBox
    Me.ckAc_gprincomp = New NTSInformatica.NTSCheckBox
    Me.ckAc_provvig2 = New NTSInformatica.NTSCheckBox
    Me.ckAc_lotti2 = New NTSInformatica.NTSCheckBox
    Me.lbAc_contabft = New NTSInformatica.NTSLabel
    Me.cbAc_contabft = New NTSInformatica.NTSComboBox
    Me.ckAc_mgdi = New NTSInformatica.NTSCheckBox
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAc_flrifboi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAc_cdtivapri.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAc_cespint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAc_geststanz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAc_xlsric.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAc_xlscee.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAc_dtinipre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAc_dtfinpre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAc_flintbol.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAc_tpdesagg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAc_flgiobol.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAc_percint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAc_gesived.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAc_gestefcc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAc_gprincomp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAc_provvig2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAc_lotti2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAc_contabft.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAc_mgdi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'lbAc_xlsric
    '
    Me.lbAc_xlsric.AutoSize = True
    Me.lbAc_xlsric.BackColor = System.Drawing.Color.Transparent
    Me.lbAc_xlsric.Location = New System.Drawing.Point(12, 170)
    Me.lbAc_xlsric.Name = "lbAc_xlsric"
    Me.lbAc_xlsric.NTSDbField = ""
    Me.lbAc_xlsric.Size = New System.Drawing.Size(159, 13)
    Me.lbAc_xlsric.TabIndex = 18
    Me.lbAc_xlsric.Text = "Nome file excel per riclassificato"
    Me.lbAc_xlsric.Tooltip = ""
    Me.lbAc_xlsric.UseMnemonic = False
    '
    'ckAc_flrifboi
    '
    Me.ckAc_flrifboi.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAc_flrifboi.Location = New System.Drawing.Point(12, 219)
    Me.ckAc_flrifboi.Name = "ckAc_flrifboi"
    Me.ckAc_flrifboi.NTSCheckValue = "S"
    Me.ckAc_flrifboi.NTSUnCheckValue = "N"
    Me.ckAc_flrifboi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAc_flrifboi.Properties.Appearance.Options.UseBackColor = True
    Me.ckAc_flrifboi.Properties.AutoHeight = False
    Me.ckAc_flrifboi.Properties.Caption = "Gestione ratei e risconti"
    Me.ckAc_flrifboi.Size = New System.Drawing.Size(216, 19)
    Me.ckAc_flrifboi.TabIndex = 513
    '
    'lbAc_xlscee
    '
    Me.lbAc_xlscee.AutoSize = True
    Me.lbAc_xlscee.BackColor = System.Drawing.Color.Transparent
    Me.lbAc_xlscee.Location = New System.Drawing.Point(12, 144)
    Me.lbAc_xlscee.Name = "lbAc_xlscee"
    Me.lbAc_xlscee.NTSDbField = ""
    Me.lbAc_xlscee.Size = New System.Drawing.Size(196, 13)
    Me.lbAc_xlscee.TabIndex = 24
    Me.lbAc_xlscee.Text = "Nome file excel per riclassificazione CEE"
    Me.lbAc_xlscee.Tooltip = ""
    Me.lbAc_xlscee.UseMnemonic = False
    '
    'lbAc_cdtivapri
    '
    Me.lbAc_cdtivapri.AutoSize = True
    Me.lbAc_cdtivapri.BackColor = System.Drawing.Color.Transparent
    Me.lbAc_cdtivapri.Location = New System.Drawing.Point(12, 118)
    Me.lbAc_cdtivapri.Name = "lbAc_cdtivapri"
    Me.lbAc_cdtivapri.NTSDbField = ""
    Me.lbAc_cdtivapri.Size = New System.Drawing.Size(148, 13)
    Me.lbAc_cdtivapri.TabIndex = 587
    Me.lbAc_cdtivapri.Text = "Codice aliquota Iva prioritaria"
    Me.lbAc_cdtivapri.Tooltip = ""
    Me.lbAc_cdtivapri.UseMnemonic = False
    '
    'edAc_cdtivapri
    '
    Me.edAc_cdtivapri.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAc_cdtivapri.EditValue = "0"
    Me.edAc_cdtivapri.Location = New System.Drawing.Point(215, 115)
    Me.edAc_cdtivapri.Name = "edAc_cdtivapri"
    Me.edAc_cdtivapri.NTSDbField = ""
    Me.edAc_cdtivapri.NTSFormat = "0"
    Me.edAc_cdtivapri.NTSForzaVisZoom = False
    Me.edAc_cdtivapri.NTSOldValue = ""
    Me.edAc_cdtivapri.Properties.Appearance.Options.UseTextOptions = True
    Me.edAc_cdtivapri.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAc_cdtivapri.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAc_cdtivapri.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAc_cdtivapri.Properties.AutoHeight = False
    Me.edAc_cdtivapri.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAc_cdtivapri.Properties.MaxLength = 65536
    Me.edAc_cdtivapri.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAc_cdtivapri.Size = New System.Drawing.Size(100, 20)
    Me.edAc_cdtivapri.TabIndex = 589
    Me.edAc_cdtivapri.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edAc_cdtivapri.TextInt = 0
    '
    'lbXx_cdtivapri
    '
    Me.lbXx_cdtivapri.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_cdtivapri.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_cdtivapri.Location = New System.Drawing.Point(321, 115)
    Me.lbXx_cdtivapri.Name = "lbXx_cdtivapri"
    Me.lbXx_cdtivapri.NTSDbField = ""
    Me.lbXx_cdtivapri.Size = New System.Drawing.Size(233, 20)
    Me.lbXx_cdtivapri.TabIndex = 590
    Me.lbXx_cdtivapri.Text = "lbXx_cdtivapri"
    Me.lbXx_cdtivapri.Tooltip = ""
    Me.lbXx_cdtivapri.UseMnemonic = False
    '
    'ckAc_cespint
    '
    Me.ckAc_cespint.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAc_cespint.Location = New System.Drawing.Point(311, 267)
    Me.ckAc_cespint.Name = "ckAc_cespint"
    Me.ckAc_cespint.NTSCheckValue = "S"
    Me.ckAc_cespint.NTSUnCheckValue = "N"
    Me.ckAc_cespint.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAc_cespint.Properties.Appearance.Options.UseBackColor = True
    Me.ckAc_cespint.Properties.AutoHeight = False
    Me.ckAc_cespint.Properties.Caption = "Gest. punto/att. senza studi di settore AS"
    Me.ckAc_cespint.Size = New System.Drawing.Size(243, 19)
    Me.ckAc_cespint.TabIndex = 592
    '
    'ckAc_geststanz
    '
    Me.ckAc_geststanz.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAc_geststanz.Location = New System.Drawing.Point(12, 267)
    Me.ckAc_geststanz.Name = "ckAc_geststanz"
    Me.ckAc_geststanz.NTSCheckValue = "S"
    Me.ckAc_geststanz.NTSUnCheckValue = "N"
    Me.ckAc_geststanz.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAc_geststanz.Properties.Appearance.Options.UseBackColor = True
    Me.ckAc_geststanz.Properties.AutoHeight = False
    Me.ckAc_geststanz.Properties.Caption = "Gestione stanziamenti"
    Me.ckAc_geststanz.Size = New System.Drawing.Size(216, 19)
    Me.ckAc_geststanz.TabIndex = 593
    '
    'edAc_xlsric
    '
    Me.edAc_xlsric.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAc_xlsric.EditValue = ""
    Me.edAc_xlsric.Location = New System.Drawing.Point(215, 167)
    Me.edAc_xlsric.Name = "edAc_xlsric"
    Me.edAc_xlsric.NTSDbField = ""
    Me.edAc_xlsric.NTSForzaVisZoom = False
    Me.edAc_xlsric.NTSOldValue = ""
    Me.edAc_xlsric.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAc_xlsric.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAc_xlsric.Properties.AutoHeight = False
    Me.edAc_xlsric.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAc_xlsric.Properties.MaxLength = 65536
    Me.edAc_xlsric.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAc_xlsric.Size = New System.Drawing.Size(339, 20)
    Me.edAc_xlsric.TabIndex = 595
    Me.edAc_xlsric.TextStr = ""
    '
    'edAc_xlscee
    '
    Me.edAc_xlscee.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAc_xlscee.EditValue = ""
    Me.edAc_xlscee.Location = New System.Drawing.Point(215, 141)
    Me.edAc_xlscee.Name = "edAc_xlscee"
    Me.edAc_xlscee.NTSDbField = ""
    Me.edAc_xlscee.NTSForzaVisZoom = False
    Me.edAc_xlscee.NTSOldValue = ""
    Me.edAc_xlscee.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAc_xlscee.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAc_xlscee.Properties.AutoHeight = False
    Me.edAc_xlscee.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAc_xlscee.Properties.MaxLength = 65536
    Me.edAc_xlscee.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAc_xlscee.Size = New System.Drawing.Size(339, 20)
    Me.edAc_xlscee.TabIndex = 596
    Me.edAc_xlscee.TextStr = ""
    '
    'edAc_dtinipre
    '
    Me.edAc_dtinipre.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAc_dtinipre.EditValue = "01/01/2000"
    Me.edAc_dtinipre.Location = New System.Drawing.Point(215, 36)
    Me.edAc_dtinipre.Name = "edAc_dtinipre"
    Me.edAc_dtinipre.NTSDbField = ""
    Me.edAc_dtinipre.NTSForzaVisZoom = False
    Me.edAc_dtinipre.NTSOldValue = ""
    Me.edAc_dtinipre.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAc_dtinipre.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAc_dtinipre.Properties.AutoHeight = False
    Me.edAc_dtinipre.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAc_dtinipre.Properties.MaxLength = 65536
    Me.edAc_dtinipre.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAc_dtinipre.Size = New System.Drawing.Size(100, 20)
    Me.edAc_dtinipre.TabIndex = 597
    Me.edAc_dtinipre.TextData = New Date(2000, 1, 1, 0, 0, 0, 0)
    '
    'edAc_dtfinpre
    '
    Me.edAc_dtfinpre.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAc_dtfinpre.EditValue = "01/01/2000"
    Me.edAc_dtfinpre.Location = New System.Drawing.Point(321, 36)
    Me.edAc_dtfinpre.Name = "edAc_dtfinpre"
    Me.edAc_dtfinpre.NTSDbField = ""
    Me.edAc_dtfinpre.NTSForzaVisZoom = False
    Me.edAc_dtfinpre.NTSOldValue = ""
    Me.edAc_dtfinpre.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAc_dtfinpre.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAc_dtfinpre.Properties.AutoHeight = False
    Me.edAc_dtfinpre.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAc_dtfinpre.Properties.MaxLength = 65536
    Me.edAc_dtfinpre.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAc_dtfinpre.Size = New System.Drawing.Size(100, 20)
    Me.edAc_dtfinpre.TabIndex = 598
    Me.edAc_dtfinpre.TextData = New Date(2000, 1, 1, 0, 0, 0, 0)
    '
    'lbAc_dtinipre
    '
    Me.lbAc_dtinipre.AutoSize = True
    Me.lbAc_dtinipre.BackColor = System.Drawing.Color.Transparent
    Me.lbAc_dtinipre.Location = New System.Drawing.Point(12, 39)
    Me.lbAc_dtinipre.Name = "lbAc_dtinipre"
    Me.lbAc_dtinipre.NTSDbField = ""
    Me.lbAc_dtinipre.Size = New System.Drawing.Size(139, 13)
    Me.lbAc_dtinipre.TabIndex = 599
    Me.lbAc_dtinipre.Text = "Data inizio / fine prestazioni"
    Me.lbAc_dtinipre.Tooltip = ""
    Me.lbAc_dtinipre.UseMnemonic = False
    '
    'lbAc_flintbol
    '
    Me.lbAc_flintbol.AutoSize = True
    Me.lbAc_flintbol.BackColor = System.Drawing.Color.Transparent
    Me.lbAc_flintbol.Location = New System.Drawing.Point(12, 66)
    Me.lbAc_flintbol.Name = "lbAc_flintbol"
    Me.lbAc_flintbol.NTSDbField = ""
    Me.lbAc_flintbol.Size = New System.Drawing.Size(112, 13)
    Me.lbAc_flintbol.TabIndex = 600
    Me.lbAc_flintbol.Text = "Intestazione su bollati"
    Me.lbAc_flintbol.Tooltip = ""
    Me.lbAc_flintbol.UseMnemonic = False
    '
    'cbAc_flintbol
    '
    Me.cbAc_flintbol.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAc_flintbol.DataSource = Nothing
    Me.cbAc_flintbol.DisplayMember = ""
    Me.cbAc_flintbol.Location = New System.Drawing.Point(215, 63)
    Me.cbAc_flintbol.Name = "cbAc_flintbol"
    Me.cbAc_flintbol.NTSDbField = ""
    Me.cbAc_flintbol.Properties.AutoHeight = False
    Me.cbAc_flintbol.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAc_flintbol.Properties.DropDownRows = 30
    Me.cbAc_flintbol.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAc_flintbol.SelectedValue = ""
    Me.cbAc_flintbol.Size = New System.Drawing.Size(339, 20)
    Me.cbAc_flintbol.TabIndex = 601
    Me.cbAc_flintbol.ValueMember = ""
    '
    'cbAc_tpdesagg
    '
    Me.cbAc_tpdesagg.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAc_tpdesagg.DataSource = Nothing
    Me.cbAc_tpdesagg.DisplayMember = ""
    Me.cbAc_tpdesagg.Location = New System.Drawing.Point(215, 89)
    Me.cbAc_tpdesagg.Name = "cbAc_tpdesagg"
    Me.cbAc_tpdesagg.NTSDbField = ""
    Me.cbAc_tpdesagg.Properties.AutoHeight = False
    Me.cbAc_tpdesagg.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAc_tpdesagg.Properties.DropDownRows = 30
    Me.cbAc_tpdesagg.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAc_tpdesagg.SelectedValue = ""
    Me.cbAc_tpdesagg.Size = New System.Drawing.Size(339, 20)
    Me.cbAc_tpdesagg.TabIndex = 602
    Me.cbAc_tpdesagg.ValueMember = ""
    '
    'lbAc_tpdesagg
    '
    Me.lbAc_tpdesagg.AutoSize = True
    Me.lbAc_tpdesagg.BackColor = System.Drawing.Color.Transparent
    Me.lbAc_tpdesagg.Location = New System.Drawing.Point(12, 92)
    Me.lbAc_tpdesagg.Name = "lbAc_tpdesagg"
    Me.lbAc_tpdesagg.NTSDbField = ""
    Me.lbAc_tpdesagg.Size = New System.Drawing.Size(199, 13)
    Me.lbAc_tpdesagg.TabIndex = 603
    Me.lbAc_tpdesagg.Text = "Combinazioni per stampa descr.aggiunt."
    Me.lbAc_tpdesagg.Tooltip = ""
    Me.lbAc_tpdesagg.UseMnemonic = False
    '
    'cbAc_flgiobol
    '
    Me.cbAc_flgiobol.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAc_flgiobol.DataSource = Nothing
    Me.cbAc_flgiobol.DisplayMember = ""
    Me.cbAc_flgiobol.Location = New System.Drawing.Point(215, 193)
    Me.cbAc_flgiobol.Name = "cbAc_flgiobol"
    Me.cbAc_flgiobol.NTSDbField = ""
    Me.cbAc_flgiobol.Properties.AutoHeight = False
    Me.cbAc_flgiobol.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAc_flgiobol.Properties.DropDownRows = 30
    Me.cbAc_flgiobol.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAc_flgiobol.SelectedValue = ""
    Me.cbAc_flgiobol.Size = New System.Drawing.Size(339, 20)
    Me.cbAc_flgiobol.TabIndex = 604
    Me.cbAc_flgiobol.ValueMember = ""
    '
    'lbAc_flgiobol
    '
    Me.lbAc_flgiobol.AutoSize = True
    Me.lbAc_flgiobol.BackColor = System.Drawing.Color.Transparent
    Me.lbAc_flgiobol.Location = New System.Drawing.Point(12, 196)
    Me.lbAc_flgiobol.Name = "lbAc_flgiobol"
    Me.lbAc_flgiobol.NTSDbField = ""
    Me.lbAc_flgiobol.Size = New System.Drawing.Size(70, 13)
    Me.lbAc_flgiobol.TabIndex = 605
    Me.lbAc_flgiobol.Text = "Tipo chiusure"
    Me.lbAc_flgiobol.Tooltip = ""
    Me.lbAc_flgiobol.UseMnemonic = False
    '
    'ckAc_percint
    '
    Me.ckAc_percint.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAc_percint.Location = New System.Drawing.Point(311, 219)
    Me.ckAc_percint.Name = "ckAc_percint"
    Me.ckAc_percint.NTSCheckValue = "S"
    Me.ckAc_percint.NTSUnCheckValue = "N"
    Me.ckAc_percint.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAc_percint.Properties.Appearance.Options.UseBackColor = True
    Me.ckAc_percint.Properties.AutoHeight = False
    Me.ckAc_percint.Properties.Caption = "Gestione percip./ritenute integrata con CG"
    Me.ckAc_percint.Size = New System.Drawing.Size(243, 19)
    Me.ckAc_percint.TabIndex = 606
    '
    'ckAc_gesived
    '
    Me.ckAc_gesived.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAc_gesived.Location = New System.Drawing.Point(311, 243)
    Me.ckAc_gesived.Name = "ckAc_gesived"
    Me.ckAc_gesived.NTSCheckValue = "S"
    Me.ckAc_gesived.NTSUnCheckValue = "N"
    Me.ckAc_gesived.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAc_gesived.Properties.Appearance.Options.UseBackColor = True
    Me.ckAc_gesived.Properties.AutoHeight = False
    Me.ckAc_gesived.Properties.Caption = "Gestione Iva ad esigib. differita come rel 8"
    Me.ckAc_gesived.Size = New System.Drawing.Size(243, 19)
    Me.ckAc_gesived.TabIndex = 607
    '
    'ckAc_gestefcc
    '
    Me.ckAc_gestefcc.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAc_gestefcc.Location = New System.Drawing.Point(12, 243)
    Me.ckAc_gestefcc.Name = "ckAc_gestefcc"
    Me.ckAc_gestefcc.NTSCheckValue = "S"
    Me.ckAc_gestefcc.NTSUnCheckValue = "N"
    Me.ckAc_gestefcc.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAc_gestefcc.Properties.Appearance.Options.UseBackColor = True
    Me.ckAc_gestefcc.Properties.AutoHeight = False
    Me.ckAc_gestefcc.Properties.Caption = "Emissione effetti con chiusura cliente"
    Me.ckAc_gestefcc.Size = New System.Drawing.Size(216, 19)
    Me.ckAc_gestefcc.TabIndex = 608
    '
    'ckAc_gprincomp
    '
    Me.ckAc_gprincomp.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAc_gprincomp.Location = New System.Drawing.Point(311, 292)
    Me.ckAc_gprincomp.Name = "ckAc_gprincomp"
    Me.ckAc_gprincomp.NTSCheckValue = "S"
    Me.ckAc_gprincomp.NTSUnCheckValue = "N"
    Me.ckAc_gprincomp.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAc_gprincomp.Properties.Appearance.Options.UseBackColor = True
    Me.ckAc_gprincomp.Properties.AutoHeight = False
    Me.ckAc_gprincomp.Properties.Caption = "Ripartisci competenza costi/ricavi su mese"
    Me.ckAc_gprincomp.Size = New System.Drawing.Size(243, 19)
    Me.ckAc_gprincomp.TabIndex = 609
    '
    'ckAc_provvig2
    '
    Me.ckAc_provvig2.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAc_provvig2.Location = New System.Drawing.Point(12, 292)
    Me.ckAc_provvig2.Name = "ckAc_provvig2"
    Me.ckAc_provvig2.NTSCheckValue = "S"
    Me.ckAc_provvig2.NTSUnCheckValue = "N"
    Me.ckAc_provvig2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAc_provvig2.Properties.Appearance.Options.UseBackColor = True
    Me.ckAc_provvig2.Properties.AutoHeight = False
    Me.ckAc_provvig2.Properties.Caption = "Nuova gestione Provvigioni su incassato"
    Me.ckAc_provvig2.Size = New System.Drawing.Size(216, 19)
    Me.ckAc_provvig2.TabIndex = 610
    '
    'ckAc_lotti2
    '
    Me.ckAc_lotti2.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAc_lotti2.Location = New System.Drawing.Point(12, 316)
    Me.ckAc_lotti2.Name = "ckAc_lotti2"
    Me.ckAc_lotti2.NTSCheckValue = "S"
    Me.ckAc_lotti2.NTSUnCheckValue = "N"
    Me.ckAc_lotti2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAc_lotti2.Properties.Appearance.Options.UseBackColor = True
    Me.ckAc_lotti2.Properties.AutoHeight = False
    Me.ckAc_lotti2.Properties.Caption = "Gestione lotti alfanumerici"
    Me.ckAc_lotti2.Size = New System.Drawing.Size(216, 19)
    Me.ckAc_lotti2.TabIndex = 611
    '
    'lbAc_contabft
    '
    Me.lbAc_contabft.AutoSize = True
    Me.lbAc_contabft.BackColor = System.Drawing.Color.Transparent
    Me.lbAc_contabft.Location = New System.Drawing.Point(308, 318)
    Me.lbAc_contabft.Name = "lbAc_contabft"
    Me.lbAc_contabft.NTSDbField = ""
    Me.lbAc_contabft.Size = New System.Drawing.Size(173, 13)
    Me.lbAc_contabft.TabIndex = 617
    Me.lbAc_contabft.Text = "Contab. autom. fatture da magaz."
    Me.lbAc_contabft.Tooltip = ""
    Me.lbAc_contabft.UseMnemonic = False
    '
    'cbAc_contabft
    '
    Me.cbAc_contabft.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAc_contabft.DataSource = Nothing
    Me.cbAc_contabft.DisplayMember = ""
    Me.cbAc_contabft.Location = New System.Drawing.Point(487, 315)
    Me.cbAc_contabft.Name = "cbAc_contabft"
    Me.cbAc_contabft.NTSDbField = ""
    Me.cbAc_contabft.Properties.AutoHeight = False
    Me.cbAc_contabft.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAc_contabft.Properties.DropDownRows = 30
    Me.cbAc_contabft.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAc_contabft.SelectedValue = ""
    Me.cbAc_contabft.Size = New System.Drawing.Size(67, 20)
    Me.cbAc_contabft.TabIndex = 616
    Me.cbAc_contabft.ValueMember = ""
    '
    'ckAc_mgdi
    '
    Me.ckAc_mgdi.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAc_mgdi.Location = New System.Drawing.Point(12, 341)
    Me.ckAc_mgdi.Name = "ckAc_mgdi"
    Me.ckAc_mgdi.NTSCheckValue = "S"
    Me.ckAc_mgdi.NTSUnCheckValue = "N"
    Me.ckAc_mgdi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAc_mgdi.Properties.Appearance.Options.UseBackColor = True
    Me.ckAc_mgdi.Properties.AutoHeight = False
    Me.ckAc_mgdi.Properties.Caption = "Dich. di intento imputabili da magazzino"
    Me.ckAc_mgdi.Size = New System.Drawing.Size(216, 19)
    Me.ckAc_mgdi.TabIndex = 618
    '
    'FRM__DITA
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(566, 367)
    Me.Controls.Add(Me.ckAc_mgdi)
    Me.Controls.Add(Me.lbAc_contabft)
    Me.Controls.Add(Me.cbAc_contabft)
    Me.Controls.Add(Me.ckAc_lotti2)
    Me.Controls.Add(Me.ckAc_provvig2)
    Me.Controls.Add(Me.ckAc_gprincomp)
    Me.Controls.Add(Me.ckAc_gestefcc)
    Me.Controls.Add(Me.ckAc_gesived)
    Me.Controls.Add(Me.ckAc_percint)
    Me.Controls.Add(Me.lbAc_flgiobol)
    Me.Controls.Add(Me.cbAc_flgiobol)
    Me.Controls.Add(Me.lbAc_tpdesagg)
    Me.Controls.Add(Me.cbAc_tpdesagg)
    Me.Controls.Add(Me.cbAc_flintbol)
    Me.Controls.Add(Me.lbAc_flintbol)
    Me.Controls.Add(Me.lbAc_dtinipre)
    Me.Controls.Add(Me.edAc_dtfinpre)
    Me.Controls.Add(Me.edAc_dtinipre)
    Me.Controls.Add(Me.edAc_xlscee)
    Me.Controls.Add(Me.edAc_xlsric)
    Me.Controls.Add(Me.ckAc_geststanz)
    Me.Controls.Add(Me.ckAc_cespint)
    Me.Controls.Add(Me.lbXx_cdtivapri)
    Me.Controls.Add(Me.lbAc_cdtivapri)
    Me.Controls.Add(Me.edAc_cdtivapri)
    Me.Controls.Add(Me.lbAc_xlsric)
    Me.Controls.Add(Me.lbAc_xlscee)
    Me.Controls.Add(Me.ckAc_flrifboi)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__DITA"
    Me.Text = "DATI AGGIUNTIVI CONTABILITA'"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAc_flrifboi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAc_cdtivapri.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAc_cespint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAc_geststanz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAc_xlsric.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAc_xlscee.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAc_dtinipre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAc_dtfinpre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAc_flintbol.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAc_tpdesagg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAc_flgiobol.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAc_percint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAc_gesived.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAc_gestefcc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAc_gprincomp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAc_provvig2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAc_lotti2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAc_contabft.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAc_mgdi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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

  Public Overridable Sub CaricaCombo()

    Try
      Dim dttFlgiobol As New DataTable()
      Dim dttFlintbol As New DataTable()
      Dim dttTpdesagg As New DataTable()
      Dim dttContab As New DataTable()

      dttFlgiobol.Columns.Add("cod", GetType(String))
      dttFlgiobol.Columns.Add("val", GetType(String))
      dttFlgiobol.Rows.Add(New Object() {"N", "Chiusure in data fine esercizio"})
      dttFlgiobol.Rows.Add(New Object() {"S", "Chiusure in post-fine esercizio e Libro Giornale continuo"})
      dttFlgiobol.Rows.Add(New Object() {"D", "Chiusure post-fine esercizio e progressivi Libro Giornale distinti"})
      dttFlgiobol.AcceptChanges()
      cbAc_flgiobol.DataSource = dttFlgiobol
      cbAc_flgiobol.ValueMember = "cod"
      cbAc_flgiobol.DisplayMember = "val"

      dttFlintbol.Columns.Add("cod", GetType(String))
      dttFlintbol.Columns.Add("val", GetType(String))
      dttFlintbol.Rows.Add(New Object() {"S", "Intesta e numera automaticamente"})
      dttFlintbol.Rows.Add(New Object() {"N", "Libro già intestato e numerato"})
      dttFlintbol.Rows.Add(New Object() {"P", "Solo intestazione"})
      dttFlintbol.AcceptChanges()
      cbAc_flintbol.DataSource = dttFlintbol
      cbAc_flintbol.ValueMember = "cod"
      cbAc_flintbol.DisplayMember = "val"

      dttTpdesagg.Columns.Add("cod", GetType(Short))
      dttTpdesagg.Columns.Add("val", GetType(String))
      dttTpdesagg.Rows.Add(New Object() {0, "No"})
      dttTpdesagg.Rows.Add(New Object() {1, "No Libro Giornale/No registri Iva/Sì Partitari"})
      dttTpdesagg.Rows.Add(New Object() {2, "Sì Libro Giornale/No registri Iva/Sì Partitari"})
      dttTpdesagg.Rows.Add(New Object() {3, "No Libro Giornale/Sì registri Iva/Sì Partitari"})
      dttTpdesagg.Rows.Add(New Object() {4, "No Libro Giornale/No registri Iva/No Partitari"})
      dttTpdesagg.Rows.Add(New Object() {5, "No Libro Giornale/Sì registri Iva/No Partitari"})
      dttTpdesagg.Rows.Add(New Object() {6, "Sì Libro Giornale/No registri Iva/No Partitari"})
      dttTpdesagg.Rows.Add(New Object() {7, "Sì Libro Giornale/Sì registri Iva/No Partitari"})
      dttTpdesagg.Rows.Add(New Object() {8, "Sì Libro Giornale/Sì registri Iva/Sì Partitari"})
      dttTpdesagg.AcceptChanges()
      cbAc_tpdesagg.DataSource = dttTpdesagg
      cbAc_tpdesagg.ValueMember = "cod"
      cbAc_tpdesagg.DisplayMember = "val"

      dttContab.Columns.Add("cod", GetType(String))
      dttContab.Columns.Add("val", GetType(String))
      dttContab.Rows.Add(New Object() {"S", "Si"})
      dttContab.Rows.Add(New Object() {"N", "No"})
      dttContab.Rows.Add(New Object() {"A", "Solo ciclo attivo"})
      dttContab.Rows.Add(New Object() {"P", "Solo ciclo passivo"})
      dttContab.AcceptChanges()
      cbAc_contabft.DataSource = dttContab
      cbAc_contabft.ValueMember = "cod"
      cbAc_contabft.DisplayMember = "val"

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
      ckAc_gestefcc.NTSSetParam(oMenu, oApp.Tr(Me, 128647487859062500, "Emissione effetti con chiusura cliente"), "S", "N")
      ckAc_gesived.NTSSetParam(oMenu, oApp.Tr(Me, 128647487859218750, "Gestione Iva ad esigib. differita come rel 8"), "S", "N")
      ckAc_percint.NTSSetParam(oMenu, oApp.Tr(Me, 128647487859375000, "Gestione percip./ritenute integrata con CG"), "S", "N")
      cbAc_flgiobol.NTSSetParam(oApp.Tr(Me, 128647487859687500, "Tipo chiusure"))
      cbAc_tpdesagg.NTSSetParam(oApp.Tr(Me, 128647487860000000, "Combinazioni per stampa descr.aggiunt."))
      cbAc_flintbol.NTSSetParam(oApp.Tr(Me, 128647487860156250, "Intestazione su bollati"))
      edAc_dtfinpre.NTSSetParam(oMenu, oApp.Tr(Me, 128647487860625000, "Data fine prestazioni"), False, "D", NTSCDate(IntSetDate("01/01/1900")), NTSCDate(IntSetDate("31/12/2099")))
      edAc_dtinipre.NTSSetParam(oMenu, oApp.Tr(Me, 128647487860781250, "Data inizio prestazioni"), False, "D", NTSCDate(IntSetDate("01/01/1900")), NTSCDate(IntSetDate("31/12/2099")))
      edAc_xlscee.NTSSetParam(oMenu, oApp.Tr(Me, 128647487860937500, "Nome file excel per riclassificazione CEE"), 50, True)
      edAc_xlsric.NTSSetParam(oMenu, oApp.Tr(Me, 128647487861093750, "Nome file excel per riclassificato"), 50, True)
      ckAc_geststanz.NTSSetParam(oMenu, oApp.Tr(Me, 128647487861250000, "Gestione stanziamenti"), "S", "N")
      ckAc_cespint.NTSSetParam(oMenu, oApp.Tr(Me, 128647487861406250, "Gest. punto/att. senza studi di settore AS"), "S", "N")
      edAc_cdtivapri.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128647487861875000, "Codice IVA prioritario"), tabciva)
      ckAc_flrifboi.NTSSetParam(oMenu, oApp.Tr(Me, 128647487862343750, "Gestione ratei e risconti"), "S", "N")
      ckAc_gprincomp.NTSSetParam(oMenu, oApp.Tr(Me, 129240188101611172, "Ripartisci competenza costi/ricavi su mese"), "S", "N")
      ckAc_provvig2.NTSSetParam(oMenu, oApp.Tr(Me, 129476686148320313, "Provvigioni su incassato vers. 2"), "S", "N")
      cbAc_contabft.NTSSetParam(oApp.Tr(Me, 130416182726044089, "Contabilizzazione automatica fatture da magazzino"))

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
      ckAc_gestefcc.NTSText.NTSDbField = "ANADITAC.ac_gestefcc"
      ckAc_gesived.NTSText.NTSDbField = "ANADITAC.ac_gesived"
      ckAc_percint.NTSText.NTSDbField = "ANADITAC.ac_percint"
      cbAc_flgiobol.NTSDbField = "ANADITAC.ac_flgiobol"
      cbAc_tpdesagg.NTSDbField = "ANADITAC.ac_tpdesagg"
      cbAc_flintbol.NTSDbField = "ANADITAC.ac_flintbol"
      edAc_dtfinpre.NTSDbField = "ANADITAC.ac_dtfinpre"
      edAc_dtinipre.NTSDbField = "ANADITAC.ac_dtinipre"
      edAc_xlscee.NTSDbField = "ANADITAC.ac_xlscee"
      edAc_xlsric.NTSDbField = "ANADITAC.ac_xlsric"
      ckAc_geststanz.NTSText.NTSDbField = "ANADITAC.ac_geststanz"
      ckAc_cespint.NTSText.NTSDbField = "ANADITAC.ac_cespint"
      lbXx_cdtivapri.NTSDbField = "ANADITAC.xx_cdtivapri"
      edAc_cdtivapri.NTSDbField = "ANADITAC.ac_cdtivapri"
      ckAc_flrifboi.NTSText.NTSDbField = "ANADITAC.ac_flrifboi"
      ckAc_gprincomp.NTSText.NTSDbField = "ANADITAC.ac_gprincomp"
      ckAc_provvig2.NTSText.NTSDbField = "ANADITAC.ac_provvig2"
      ckAc_lotti2.NTSText.NTSDbField = "ANADITAC.ac_lotti2"
      cbAc_contabft.NTSDbField = "ANADITAC.ac_contabft"
      ckAc_mgdi.NTSText.NTSDbField = "ANADITAC.ac_mgdi"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcDita, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRM__DITA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'carico i combobox
      CaricaCombo()

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      dsDita = oCleAnaz.dsShared
      dcDita.DataSource = dsDita.Tables("ANADITAC")
      dsDita.Tables("ANADITAC").AcceptChanges()

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      If oCleAnaz.RitenuteMovimentate Then ckAc_percint.Enabled = False
      If oCleAnaz.IvaMovimentata Then ckAc_gesived.Enabled = False
      'con l'inserimento dell'iva di cassa per gestire l'emissione effetti senza chiusura cliente non è necessario avere moduli particolari
      'ckAc_gestefcc.Enabled = False

      'If NTSCStr(oCleAnaz.dsShared.Tables("TABANAZ").Rows(0)!tb_azprofes) = "N" Then
      '  If oCleAnaz.dsShared.Tables("ANAZMOD").Select("am_modulo = 112 AND am_abilit = 'S' AND xx_abinsg = 'S' AND xx_abchiave = 'S'").Length > 0 Then
      '    GctlSetVisEnab(ckAc_gestefcc, False)
      '  Else
      '    ckAc_gestefcc.Checked = True
      '  End If
      'End If
      If oCleAnaz.ProvvigioniNonPagate Then ckAc_provvig2.Enabled = False

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__DITA_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129006944247073603, "Ripristinare le modifiche apportate?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          If dsDita.Tables("ANADITAC").Rows.Count = 1 And dsDita.Tables("ANADITAC").Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          oCleAnaz.DitaRipristina(dcDita.Position, dcDita.Filter)
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcDita, Me)
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

      If oCleAnaz.DitaRecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129006944377233185, "Confermi il salvataggio?"))
        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oCleAnaz.DitaSalva(False) Then Return False
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

