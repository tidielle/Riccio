Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__DITA_F
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
  Public WithEvents lbAc_xlscee As NTSInformatica.NTSLabel
  Public WithEvents edAc_xlscee As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAc_xlsric As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAc_flintbol As NTSInformatica.NTSLabel
  Public WithEvents lbAc_tpdesagg As NTSInformatica.NTSLabel
  Public WithEvents cbAc_tpdesagg As NTSInformatica.NTSComboBox
  Public WithEvents cbAc_flintbol As NTSInformatica.NTSComboBox
  Public WithEvents ckAc_gestefcc As NTSInformatica.NTSCheckBox
  Public WithEvents lbAc_flgiobol As NTSInformatica.NTSLabel
  Public WithEvents cbAc_flgiobol As NTSInformatica.NTSComboBox

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__DITA_F))
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
    Me.lbAc_xlscee = New NTSInformatica.NTSLabel
    Me.edAc_xlsric = New NTSInformatica.NTSTextBoxStr
    Me.edAc_xlscee = New NTSInformatica.NTSTextBoxStr
    Me.lbAc_flintbol = New NTSInformatica.NTSLabel
    Me.cbAc_flintbol = New NTSInformatica.NTSComboBox
    Me.cbAc_tpdesagg = New NTSInformatica.NTSComboBox
    Me.lbAc_tpdesagg = New NTSInformatica.NTSLabel
    Me.cbAc_flgiobol = New NTSInformatica.NTSComboBox
    Me.lbAc_flgiobol = New NTSInformatica.NTSLabel
    Me.ckAc_gestefcc = New NTSInformatica.NTSCheckBox
    Me.ckAc_flrifboi = New NTSInformatica.NTSCheckBox
    Me.cbAc_contabft = New NTSInformatica.NTSComboBox
    Me.lbAc_contabft = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAc_xlsric.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAc_xlscee.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAc_flintbol.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAc_tpdesagg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAc_flgiobol.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAc_gestefcc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAc_flrifboi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAc_contabft.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.lbAc_xlsric.Location = New System.Drawing.Point(12, 103)
    Me.lbAc_xlsric.Name = "lbAc_xlsric"
    Me.lbAc_xlsric.NTSDbField = ""
    Me.lbAc_xlsric.Size = New System.Drawing.Size(159, 13)
    Me.lbAc_xlsric.TabIndex = 18
    Me.lbAc_xlsric.Text = "Nome file excel per riclassificato"
    Me.lbAc_xlsric.Tooltip = ""
    Me.lbAc_xlsric.UseMnemonic = False
    '
    'lbAc_xlscee
    '
    Me.lbAc_xlscee.AutoSize = True
    Me.lbAc_xlscee.BackColor = System.Drawing.Color.Transparent
    Me.lbAc_xlscee.Location = New System.Drawing.Point(12, 81)
    Me.lbAc_xlscee.Name = "lbAc_xlscee"
    Me.lbAc_xlscee.NTSDbField = ""
    Me.lbAc_xlscee.Size = New System.Drawing.Size(196, 13)
    Me.lbAc_xlscee.TabIndex = 24
    Me.lbAc_xlscee.Text = "Nome file excel per riclassificazione CEE"
    Me.lbAc_xlscee.Tooltip = ""
    Me.lbAc_xlscee.UseMnemonic = False
    '
    'edAc_xlsric
    '
    Me.edAc_xlsric.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAc_xlsric.Location = New System.Drawing.Point(277, 100)
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
    Me.edAc_xlscee.Location = New System.Drawing.Point(277, 78)
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
    'lbAc_flintbol
    '
    Me.lbAc_flintbol.AutoSize = True
    Me.lbAc_flintbol.BackColor = System.Drawing.Color.Transparent
    Me.lbAc_flintbol.Location = New System.Drawing.Point(12, 37)
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
    Me.cbAc_flintbol.Location = New System.Drawing.Point(277, 34)
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
    Me.cbAc_tpdesagg.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.cbAc_tpdesagg.DataSource = Nothing
    Me.cbAc_tpdesagg.DisplayMember = ""
    Me.cbAc_tpdesagg.Location = New System.Drawing.Point(277, 56)
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
    Me.lbAc_tpdesagg.Location = New System.Drawing.Point(12, 59)
    Me.lbAc_tpdesagg.Name = "lbAc_tpdesagg"
    Me.lbAc_tpdesagg.NTSDbField = ""
    Me.lbAc_tpdesagg.Size = New System.Drawing.Size(235, 13)
    Me.lbAc_tpdesagg.TabIndex = 603
    Me.lbAc_tpdesagg.Text = "Combinazioni per stampa descrizione aggiuntiva"
    Me.lbAc_tpdesagg.Tooltip = ""
    Me.lbAc_tpdesagg.UseMnemonic = False
    '
    'cbAc_flgiobol
    '
    Me.cbAc_flgiobol.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAc_flgiobol.DataSource = Nothing
    Me.cbAc_flgiobol.DisplayMember = ""
    Me.cbAc_flgiobol.Location = New System.Drawing.Point(277, 122)
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
    Me.lbAc_flgiobol.Location = New System.Drawing.Point(12, 125)
    Me.lbAc_flgiobol.Name = "lbAc_flgiobol"
    Me.lbAc_flgiobol.NTSDbField = ""
    Me.lbAc_flgiobol.Size = New System.Drawing.Size(70, 13)
    Me.lbAc_flgiobol.TabIndex = 605
    Me.lbAc_flgiobol.Text = "Tipo chiusure"
    Me.lbAc_flgiobol.Tooltip = ""
    Me.lbAc_flgiobol.UseMnemonic = False
    '
    'ckAc_gestefcc
    '
    Me.ckAc_gestefcc.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAc_gestefcc.Location = New System.Drawing.Point(12, 164)
    Me.ckAc_gestefcc.Name = "ckAc_gestefcc"
    Me.ckAc_gestefcc.NTSCheckValue = "S"
    Me.ckAc_gestefcc.NTSUnCheckValue = "N"
    Me.ckAc_gestefcc.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAc_gestefcc.Properties.Appearance.Options.UseBackColor = True
    Me.ckAc_gestefcc.Properties.AutoHeight = False
    Me.ckAc_gestefcc.Properties.Caption = "Emissione effetti con chiusura cliente"
    Me.ckAc_gestefcc.Size = New System.Drawing.Size(201, 19)
    Me.ckAc_gestefcc.TabIndex = 608
    '
    'ckAc_flrifboi
    '
    Me.ckAc_flrifboi.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAc_flrifboi.Location = New System.Drawing.Point(12, 144)
    Me.ckAc_flrifboi.Name = "ckAc_flrifboi"
    Me.ckAc_flrifboi.NTSCheckValue = "S"
    Me.ckAc_flrifboi.NTSUnCheckValue = "N"
    Me.ckAc_flrifboi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAc_flrifboi.Properties.Appearance.Options.UseBackColor = True
    Me.ckAc_flrifboi.Properties.AutoHeight = False
    Me.ckAc_flrifboi.Properties.Caption = "Gestione ratei e risconti"
    Me.ckAc_flrifboi.Size = New System.Drawing.Size(139, 19)
    Me.ckAc_flrifboi.TabIndex = 513
    '
    'cbAc_contabft
    '
    Me.cbAc_contabft.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAc_contabft.DataSource = Nothing
    Me.cbAc_contabft.DisplayMember = ""
    Me.cbAc_contabft.Location = New System.Drawing.Point(499, 147)
    Me.cbAc_contabft.Name = "cbAc_contabft"
    Me.cbAc_contabft.NTSDbField = ""
    Me.cbAc_contabft.Properties.AutoHeight = False
    Me.cbAc_contabft.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAc_contabft.Properties.DropDownRows = 30
    Me.cbAc_contabft.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAc_contabft.SelectedValue = ""
    Me.cbAc_contabft.Size = New System.Drawing.Size(117, 20)
    Me.cbAc_contabft.TabIndex = 614
    Me.cbAc_contabft.ValueMember = ""
    '
    'lbAc_contabft
    '
    Me.lbAc_contabft.AutoSize = True
    Me.lbAc_contabft.BackColor = System.Drawing.Color.Transparent
    Me.lbAc_contabft.Location = New System.Drawing.Point(274, 150)
    Me.lbAc_contabft.Name = "lbAc_contabft"
    Me.lbAc_contabft.NTSDbField = ""
    Me.lbAc_contabft.Size = New System.Drawing.Size(219, 13)
    Me.lbAc_contabft.TabIndex = 615
    Me.lbAc_contabft.Text = "Contabilizzaz. automatica fatture da magaz."
    Me.lbAc_contabft.Tooltip = ""
    Me.lbAc_contabft.UseMnemonic = False
    '
    'FRM__DITA_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(633, 194)
    Me.Controls.Add(Me.lbAc_contabft)
    Me.Controls.Add(Me.cbAc_contabft)
    Me.Controls.Add(Me.ckAc_gestefcc)
    Me.Controls.Add(Me.lbAc_flgiobol)
    Me.Controls.Add(Me.cbAc_flgiobol)
    Me.Controls.Add(Me.lbAc_tpdesagg)
    Me.Controls.Add(Me.cbAc_tpdesagg)
    Me.Controls.Add(Me.cbAc_flintbol)
    Me.Controls.Add(Me.lbAc_flintbol)
    Me.Controls.Add(Me.edAc_xlscee)
    Me.Controls.Add(Me.edAc_xlsric)
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
    Me.Name = "FRM__DITA_F"
    Me.Text = "DATI AGGIUNTIVI CONTABILITA'"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAc_xlsric.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAc_xlscee.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAc_flintbol.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAc_tpdesagg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAc_flgiobol.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAc_gestefcc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAc_flrifboi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAc_contabft.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
      dttFlintbol.Rows.Add(New Object() {"N", "Libro gi‡ intestato e numerato"})
      dttFlintbol.Rows.Add(New Object() {"P", "Solo intestazione"})
      dttFlintbol.AcceptChanges()
      cbAc_flintbol.DataSource = dttFlintbol
      cbAc_flintbol.ValueMember = "cod"
      cbAc_flintbol.DisplayMember = "val"

      dttTpdesagg.Columns.Add("cod", GetType(Short))
      dttTpdesagg.Columns.Add("val", GetType(String))
      dttTpdesagg.Rows.Add(New Object() {0, "No"})
      dttTpdesagg.Rows.Add(New Object() {1, "No Libro Giornale/No registri Iva/SÏ Partitari"})
      dttTpdesagg.Rows.Add(New Object() {2, "SÏ Libro Giornale/No registri Iva/SÏ Partitari"})
      dttTpdesagg.Rows.Add(New Object() {3, "No Libro Giornale/SÏ registri Iva/SÏ Partitari"})
      dttTpdesagg.Rows.Add(New Object() {4, "No Libro Giornale/No registri Iva/No Partitari"})
      dttTpdesagg.Rows.Add(New Object() {5, "No Libro Giornale/SÏ registri Iva/No Partitari"})
      dttTpdesagg.Rows.Add(New Object() {6, "SÏ Libro Giornale/No registri Iva/No Partitari"})
      dttTpdesagg.Rows.Add(New Object() {7, "SÏ Libro Giornale/SÏ registri Iva/No Partitari"})
      dttTpdesagg.Rows.Add(New Object() {8, "SÏ Libro Giornale/SÏ registri Iva/SÏ Partitari"})
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
        'non gestisco l'errore: se non c'√® una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      '-------------------------------------------------
      'completo le informazioni dei controlli
      ckAc_gestefcc.NTSSetParam(oMenu, oApp.Tr(Me, 128647487859062500, "Emissione effetti con chiusura cliente"), "S", "N")
      cbAc_flgiobol.NTSSetParam(oApp.Tr(Me, 128647487859687500, "Tipo chiusure"))
      cbAc_tpdesagg.NTSSetParam(oApp.Tr(Me, 128647487860000000, "Combinazioni per stampa descr.aggiunt."))
      cbAc_flintbol.NTSSetParam(oApp.Tr(Me, 128647487860156250, "Intestazione su bollati"))
      edAc_xlscee.NTSSetParam(oMenu, oApp.Tr(Me, 128647487860937500, "Nome file excel per riclassificazione CEE"), 50, True)
      edAc_xlsric.NTSSetParam(oMenu, oApp.Tr(Me, 128647487861093750, "Nome file excel per riclassificato"), 50, True)
      ckAc_flrifboi.NTSSetParam(oMenu, oApp.Tr(Me, 128647487862343750, "Gestione ratei e risconti"), "S", "N")
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
      'se i controlli erano gi√† stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      ckAc_gestefcc.NTSText.NTSDbField = "ANADITAC.ac_gestefcc"
      cbAc_flgiobol.NTSDbField = "ANADITAC.ac_flgiobol"
      cbAc_tpdesagg.NTSDbField = "ANADITAC.ac_tpdesagg"
      cbAc_flintbol.NTSDbField = "ANADITAC.ac_flintbol"
      edAc_xlscee.NTSDbField = "ANADITAC.ac_xlscee"
      edAc_xlsric.NTSDbField = "ANADITAC.ac_xlsric"
      ckAc_flrifboi.NTSText.NTSDbField = "ANADITAC.ac_flrifboi"
      cbAc_contabft.NTSDbField = "ANADITAC.ac_contabft"

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