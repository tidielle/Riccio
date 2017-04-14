Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__ESCI_F
#Region "Variabili"
  Public oCleAnaz As CLE__ANAZ
  Public dsEsci As DataSet
  Public oCallParams As CLE__CLDP
  Public dcEsci As BindingSource = New BindingSource

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
  Public WithEvents lbTb_codesco As NTSInformatica.NTSLabel
  Public WithEvents edTb_codesco As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_desesco As NTSInformatica.NTSLabel
  Public WithEvents edTb_desesco As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_dtineser As NTSInformatica.NTSLabel
  Public WithEvents edTb_dtineser As NTSInformatica.NTSTextBoxData
  Public WithEvents edTb_dtfieser As NTSInformatica.NTSTextBoxData
  Public WithEvents ckTb_bilstraor As NTSInformatica.NTSCheckBox
  Public WithEvents lbTb_aztipcont As NTSInformatica.NTSLabel
  Public WithEvents cbTb_aztipcont As NTSInformatica.NTSComboBox
  Public WithEvents lbTb_azcoprof As NTSInformatica.NTSLabel
  Public WithEvents cbTb_azcoprof As NTSInformatica.NTSComboBox
  Public WithEvents lbTb_forfctre As NTSInformatica.NTSLabel
  Public WithEvents edTb_forfctre As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckTb_forfctiv As NTSInformatica.NTSCheckBox
#End Region

#Region "Inizializzazione"
  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__ESCI_F))
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
    Me.lbTb_codesco = New NTSInformatica.NTSLabel
    Me.edTb_codesco = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_desesco = New NTSInformatica.NTSLabel
    Me.edTb_desesco = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_dtineser = New NTSInformatica.NTSLabel
    Me.edTb_dtineser = New NTSInformatica.NTSTextBoxData
    Me.edTb_dtfieser = New NTSInformatica.NTSTextBoxData
    Me.ckTb_bilstraor = New NTSInformatica.NTSCheckBox
    Me.lbTb_aztipcont = New NTSInformatica.NTSLabel
    Me.cbTb_aztipcont = New NTSInformatica.NTSComboBox
    Me.lbTb_azcoprof = New NTSInformatica.NTSLabel
    Me.cbTb_azcoprof = New NTSInformatica.NTSComboBox
    Me.lbTb_forfctre = New NTSInformatica.NTSLabel
    Me.edTb_forfctre = New NTSInformatica.NTSTextBoxNum
    Me.ckTb_forfctiv = New NTSInformatica.NTSCheckBox
    Me.lbXx_forfctre = New NTSInformatica.NTSLabel
    Me.lbTb_dtfieser = New NTSInformatica.NTSLabel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codesco.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_desesco.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_dtineser.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_dtfieser.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_bilstraor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_aztipcont.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_azcoprof.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_forfctre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_forfctiv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbGuida, Me.tlbEsci, Me.tlbUltimo})
    Me.NtsBarManager1.MaxItemId = 26
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
    'lbTb_codesco
    '
    Me.lbTb_codesco.AutoSize = True
    Me.lbTb_codesco.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_codesco.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbTb_codesco.Location = New System.Drawing.Point(9, 39)
    Me.lbTb_codesco.Name = "lbTb_codesco"
    Me.lbTb_codesco.NTSDbField = ""
    Me.lbTb_codesco.Size = New System.Drawing.Size(137, 13)
    Me.lbTb_codesco.TabIndex = 10
    Me.lbTb_codesco.Text = "Codice esercizio (ditta)"
    Me.lbTb_codesco.Tooltip = ""
    Me.lbTb_codesco.UseMnemonic = False
    '
    'edTb_codesco
    '
    Me.edTb_codesco.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codesco.EditValue = "0"
    Me.edTb_codesco.Location = New System.Drawing.Point(185, 36)
    Me.edTb_codesco.Name = "edTb_codesco"
    Me.edTb_codesco.NTSDbField = ""
    Me.edTb_codesco.NTSFormat = "0"
    Me.edTb_codesco.NTSForzaVisZoom = False
    Me.edTb_codesco.NTSOldValue = ""
    Me.edTb_codesco.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codesco.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codesco.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codesco.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codesco.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codesco.Properties.MaxLength = 65536
    Me.edTb_codesco.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codesco.Size = New System.Drawing.Size(84, 20)
    Me.edTb_codesco.TabIndex = 500
    '
    'lbTb_desesco
    '
    Me.lbTb_desesco.AutoSize = True
    Me.lbTb_desesco.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_desesco.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbTb_desesco.Location = New System.Drawing.Point(9, 61)
    Me.lbTb_desesco.Name = "lbTb_desesco"
    Me.lbTb_desesco.NTSDbField = ""
    Me.lbTb_desesco.Size = New System.Drawing.Size(72, 13)
    Me.lbTb_desesco.TabIndex = 11
    Me.lbTb_desesco.Text = "Descrizione"
    Me.lbTb_desesco.Tooltip = ""
    Me.lbTb_desesco.UseMnemonic = False
    '
    'edTb_desesco
    '
    Me.edTb_desesco.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_desesco.Location = New System.Drawing.Point(185, 58)
    Me.edTb_desesco.Name = "edTb_desesco"
    Me.edTb_desesco.NTSDbField = ""
    Me.edTb_desesco.NTSForzaVisZoom = False
    Me.edTb_desesco.NTSOldValue = ""
    Me.edTb_desesco.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_desesco.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_desesco.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_desesco.Properties.MaxLength = 65536
    Me.edTb_desesco.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_desesco.Size = New System.Drawing.Size(296, 20)
    Me.edTb_desesco.TabIndex = 501
    '
    'lbTb_dtineser
    '
    Me.lbTb_dtineser.AutoSize = True
    Me.lbTb_dtineser.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_dtineser.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbTb_dtineser.Location = New System.Drawing.Point(9, 83)
    Me.lbTb_dtineser.Name = "lbTb_dtineser"
    Me.lbTb_dtineser.NTSDbField = ""
    Me.lbTb_dtineser.Size = New System.Drawing.Size(146, 13)
    Me.lbTb_dtineser.TabIndex = 12
    Me.lbTb_dtineser.Text = "Data inizio/fine esercizio"
    Me.lbTb_dtineser.Tooltip = ""
    Me.lbTb_dtineser.UseMnemonic = False
    '
    'edTb_dtineser
    '
    Me.edTb_dtineser.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_dtineser.EditValue = "01/01/1900"
    Me.edTb_dtineser.Location = New System.Drawing.Point(185, 80)
    Me.edTb_dtineser.Name = "edTb_dtineser"
    Me.edTb_dtineser.NTSDbField = ""
    Me.edTb_dtineser.NTSForzaVisZoom = False
    Me.edTb_dtineser.NTSOldValue = ""
    Me.edTb_dtineser.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_dtineser.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_dtineser.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_dtineser.Properties.MaxLength = 65536
    Me.edTb_dtineser.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_dtineser.Size = New System.Drawing.Size(84, 20)
    Me.edTb_dtineser.TabIndex = 502
    '
    'edTb_dtfieser
    '
    Me.edTb_dtfieser.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_dtfieser.EditValue = "01/01/1900"
    Me.edTb_dtfieser.Location = New System.Drawing.Point(300, 80)
    Me.edTb_dtfieser.Name = "edTb_dtfieser"
    Me.edTb_dtfieser.NTSDbField = ""
    Me.edTb_dtfieser.NTSForzaVisZoom = False
    Me.edTb_dtfieser.NTSOldValue = ""
    Me.edTb_dtfieser.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_dtfieser.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_dtfieser.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_dtfieser.Properties.MaxLength = 65536
    Me.edTb_dtfieser.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_dtfieser.Size = New System.Drawing.Size(84, 20)
    Me.edTb_dtfieser.TabIndex = 503
    '
    'ckTb_bilstraor
    '
    Me.ckTb_bilstraor.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_bilstraor.Location = New System.Drawing.Point(12, 188)
    Me.ckTb_bilstraor.Name = "ckTb_bilstraor"
    Me.ckTb_bilstraor.NTSCheckValue = "S"
    Me.ckTb_bilstraor.NTSUnCheckValue = "N"
    Me.ckTb_bilstraor.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_bilstraor.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_bilstraor.Properties.Caption = "Gestione di bilancio diverso da 12 mesi"
    Me.ckTb_bilstraor.Size = New System.Drawing.Size(210, 19)
    Me.ckTb_bilstraor.TabIndex = 506
    '
    'lbTb_aztipcont
    '
    Me.lbTb_aztipcont.AutoSize = True
    Me.lbTb_aztipcont.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_aztipcont.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbTb_aztipcont.Location = New System.Drawing.Point(9, 105)
    Me.lbTb_aztipcont.Name = "lbTb_aztipcont"
    Me.lbTb_aztipcont.NTSDbField = ""
    Me.lbTb_aztipcont.Size = New System.Drawing.Size(94, 13)
    Me.lbTb_aztipcont.TabIndex = 20
    Me.lbTb_aztipcont.Text = "Tipo contabilità"
    Me.lbTb_aztipcont.Tooltip = ""
    Me.lbTb_aztipcont.UseMnemonic = False
    '
    'cbTb_aztipcont
    '
    Me.cbTb_aztipcont.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_aztipcont.DataSource = Nothing
    Me.cbTb_aztipcont.DisplayMember = ""
    Me.cbTb_aztipcont.Location = New System.Drawing.Point(185, 102)
    Me.cbTb_aztipcont.Name = "cbTb_aztipcont"
    Me.cbTb_aztipcont.NTSDbField = ""
    Me.cbTb_aztipcont.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_aztipcont.Properties.DropDownRows = 30
    Me.cbTb_aztipcont.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_aztipcont.SelectedValue = ""
    Me.cbTb_aztipcont.Size = New System.Drawing.Size(244, 20)
    Me.cbTb_aztipcont.TabIndex = 510
    Me.cbTb_aztipcont.ValueMember = ""
    '
    'lbTb_azcoprof
    '
    Me.lbTb_azcoprof.AutoSize = True
    Me.lbTb_azcoprof.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azcoprof.Location = New System.Drawing.Point(9, 127)
    Me.lbTb_azcoprof.Name = "lbTb_azcoprof"
    Me.lbTb_azcoprof.NTSDbField = ""
    Me.lbTb_azcoprof.Size = New System.Drawing.Size(173, 13)
    Me.lbTb_azcoprof.TabIndex = 21
    Me.lbTb_azcoprof.Text = "Tipo gestione incassi/pagamamenti"
    Me.lbTb_azcoprof.Tooltip = ""
    Me.lbTb_azcoprof.UseMnemonic = False
    '
    'cbTb_azcoprof
    '
    Me.cbTb_azcoprof.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_azcoprof.DataSource = Nothing
    Me.cbTb_azcoprof.DisplayMember = ""
    Me.cbTb_azcoprof.Location = New System.Drawing.Point(185, 124)
    Me.cbTb_azcoprof.Name = "cbTb_azcoprof"
    Me.cbTb_azcoprof.NTSDbField = ""
    Me.cbTb_azcoprof.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_azcoprof.Properties.DropDownRows = 30
    Me.cbTb_azcoprof.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_azcoprof.SelectedValue = ""
    Me.cbTb_azcoprof.Size = New System.Drawing.Size(244, 20)
    Me.cbTb_azcoprof.TabIndex = 511
    Me.cbTb_azcoprof.ValueMember = ""
    '
    'lbTb_forfctre
    '
    Me.lbTb_forfctre.AutoSize = True
    Me.lbTb_forfctre.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_forfctre.Location = New System.Drawing.Point(9, 149)
    Me.lbTb_forfctre.Name = "lbTb_forfctre"
    Me.lbTb_forfctre.NTSDbField = ""
    Me.lbTb_forfctre.Size = New System.Drawing.Size(72, 13)
    Me.lbTb_forfctre.TabIndex = 24
    Me.lbTb_forfctre.Text = "Codice forfait"
    Me.lbTb_forfctre.Tooltip = ""
    Me.lbTb_forfctre.UseMnemonic = False
    '
    'edTb_forfctre
    '
    Me.edTb_forfctre.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_forfctre.EditValue = "0"
    Me.edTb_forfctre.Location = New System.Drawing.Point(185, 146)
    Me.edTb_forfctre.Name = "edTb_forfctre"
    Me.edTb_forfctre.NTSDbField = ""
    Me.edTb_forfctre.NTSFormat = "0"
    Me.edTb_forfctre.NTSForzaVisZoom = False
    Me.edTb_forfctre.NTSOldValue = ""
    Me.edTb_forfctre.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_forfctre.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_forfctre.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_forfctre.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_forfctre.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_forfctre.Properties.MaxLength = 65536
    Me.edTb_forfctre.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_forfctre.Size = New System.Drawing.Size(59, 20)
    Me.edTb_forfctre.TabIndex = 514
    '
    'ckTb_forfctiv
    '
    Me.ckTb_forfctiv.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_forfctiv.Location = New System.Drawing.Point(12, 168)
    Me.ckTb_forfctiv.Name = "ckTb_forfctiv"
    Me.ckTb_forfctiv.NTSCheckValue = "S"
    Me.ckTb_forfctiv.NTSUnCheckValue = "N"
    Me.ckTb_forfctiv.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_forfctiv.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_forfctiv.Properties.Caption = "Primo esercizio"
    Me.ckTb_forfctiv.Size = New System.Drawing.Size(94, 19)
    Me.ckTb_forfctiv.TabIndex = 515
    '
    'lbXx_forfctre
    '
    Me.lbXx_forfctre.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_forfctre.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_forfctre.Location = New System.Drawing.Point(250, 146)
    Me.lbXx_forfctre.Name = "lbXx_forfctre"
    Me.lbXx_forfctre.NTSDbField = ""
    Me.lbXx_forfctre.Size = New System.Drawing.Size(326, 20)
    Me.lbXx_forfctre.TabIndex = 581
    Me.lbXx_forfctre.Text = "lbXx_forfctre"
    Me.lbXx_forfctre.Tooltip = ""
    Me.lbXx_forfctre.UseMnemonic = False
    '
    'lbTb_dtfieser
    '
    Me.lbTb_dtfieser.AutoSize = True
    Me.lbTb_dtfieser.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_dtfieser.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbTb_dtfieser.Location = New System.Drawing.Point(275, 83)
    Me.lbTb_dtfieser.Name = "lbTb_dtfieser"
    Me.lbTb_dtfieser.NTSDbField = ""
    Me.lbTb_dtfieser.Size = New System.Drawing.Size(19, 13)
    Me.lbTb_dtfieser.TabIndex = 582
    Me.lbTb_dtfieser.Text = " / "
    Me.lbTb_dtfieser.Tooltip = ""
    Me.lbTb_dtfieser.UseMnemonic = False
    '
    'FRM__ESCI_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(587, 217)
    Me.Controls.Add(Me.lbTb_dtfieser)
    Me.Controls.Add(Me.lbXx_forfctre)
    Me.Controls.Add(Me.lbTb_dtineser)
    Me.Controls.Add(Me.edTb_desesco)
    Me.Controls.Add(Me.edTb_dtineser)
    Me.Controls.Add(Me.ckTb_bilstraor)
    Me.Controls.Add(Me.edTb_dtfieser)
    Me.Controls.Add(Me.lbTb_desesco)
    Me.Controls.Add(Me.lbTb_forfctre)
    Me.Controls.Add(Me.edTb_codesco)
    Me.Controls.Add(Me.lbTb_codesco)
    Me.Controls.Add(Me.ckTb_forfctiv)
    Me.Controls.Add(Me.cbTb_aztipcont)
    Me.Controls.Add(Me.lbTb_aztipcont)
    Me.Controls.Add(Me.lbTb_azcoprof)
    Me.Controls.Add(Me.edTb_forfctre)
    Me.Controls.Add(Me.cbTb_azcoprof)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__ESCI_F"
    Me.Text = "ESERCIZI CONTABILI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codesco.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_desesco.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_dtineser.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_dtfieser.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_bilstraor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_aztipcont.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_azcoprof.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_forfctre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_forfctiv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
        'non gestisco l'errore: se non c'Ã¨ una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      '-------------------------------------------------
      'completo le informazioni dei controlli
      edTb_codesco.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128647315735312500, "Codice"), tabesco)
      edTb_desesco.NTSSetParam(oMenu, oApp.Tr(Me, 128647315752968750, "Descrizione"), 30, False)
      edTb_dtineser.NTSSetParam(oMenu, oApp.Tr(Me, 128647315766875000, "Data inizio esercizio"), False, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      edTb_dtfieser.NTSSetParam(oMenu, oApp.Tr(Me, 128647315779687500, "Data fine esercizio"), False, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      ckTb_bilstraor.NTSSetParam(oMenu, oApp.Tr(Me, 128647315817656250, "Gestione di un bilancio diverso da 12 mesi"), "S", "N")
      cbTb_aztipcont.NTSSetParam(oApp.Tr(Me, 128647315868906250, "Tipo contabilità"))
      cbTb_azcoprof.NTSSetParam(oApp.Tr(Me, 128647315881406250, "Tipo gest. incassi/pagamenti"))
      edTb_forfctre.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128647315921250000, "Cod. forfait"), tabdfor)
      ckTb_forfctiv.NTSSetParam(oMenu, oApp.Tr(Me, 128647315933750000, "Primo esercizio"), "1", "0")

      edTb_codesco.NTSForzaVisZoom = False
      edTb_codesco.NTSSetRichiesto()
      edTb_desesco.NTSSetRichiesto()
      edTb_dtineser.NTSSetRichiesto()
      edTb_dtfieser.NTSSetRichiesto()
      cbTb_aztipcont.NTSSetRichiesto()

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
      Dim dttTipcont As New DataTable()
      Dim dttCodprof As New DataTable()

      dttTipcont.Columns.Add("cod", GetType(String))
      dttTipcont.Columns.Add("val", GetType(String))
      dttTipcont.Rows.Add(New Object() {"O", "Ordinaria"})
      dttTipcont.Rows.Add(New Object() {"S", "Semplificata"})
      dttTipcont.Rows.Add(New Object() {"C", "Professionista ord. con cronologico"})
      dttTipcont.AcceptChanges()
      cbTb_aztipcont.DataSource = dttTipcont
      cbTb_aztipcont.ValueMember = "cod"
      cbTb_aztipcont.DisplayMember = "val"

      dttCodprof.Columns.Add("cod", GetType(String))
      dttCodprof.Columns.Add("val", GetType(String))
      dttCodprof.Rows.Add(New Object() {"N", "No"})
      dttCodprof.Rows.Add(New Object() {"U", "Registro unico"})
      dttCodprof.Rows.Add(New Object() {"S", "Registro separato"})
      dttCodprof.Rows.Add(New Object() {"I", "Prospetto su registri Iva"})
      dttCodprof.AcceptChanges()
      cbTb_azcoprof.DataSource = dttCodprof
      cbTb_azcoprof.ValueMember = "cod"
      cbTb_azcoprof.DisplayMember = "val"

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
      edTb_codesco.NTSDbField = "TABESCO.tb_codesco"
      edTb_desesco.NTSDbField = "TABESCO.tb_desesco"
      edTb_dtineser.NTSDbField = "TABESCO.tb_dtineser"
      edTb_dtfieser.NTSDbField = "TABESCO.tb_dtfieser"
      ckTb_bilstraor.NTSText.NTSDbField = "TABESCO.tb_bilstraor"
      cbTb_aztipcont.NTSDbField = "TABESCO.tb_aztipcont"
      cbTb_azcoprof.NTSDbField = "TABESCO.tb_azcoprof"
      edTb_forfctre.NTSDbField = "TABESCO.tb_forfctre"
      ckTb_forfctiv.NTSText.NTSDbField = "TABESCO.tb_forfctiv"
      lbXx_forfctre.NTSDbField = "TABESCO.xx_forfctre"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcEsci, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

#Region "Eventi Form"
  Public Overridable Sub FRM__ESCI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim i As Integer = 0
    Try
      '-------------------------------------------------
      'carico i combobox
      CaricaCombo()

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      dsEsci = oCleAnaz.dsShared
      dcEsci.DataSource = dsEsci.Tables("TABESCO")
      dsEsci.Tables("TABESCO").AcceptChanges()
      If dsEsci.Tables("TABESCO").Rows.Count > 0 Then
        edTb_codesco.Enabled = False
      End If

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      If dsEsci.Tables("TABESCO").Rows.Count = 0 Then tlbNuovo_ItemClick(tlbNuovo, Nothing)
      dcEsci.ResetBindings(False)
      dcEsci.MoveLast()

      cbTb_aztipcont_SelectedIndexChanged(cbTb_aztipcont, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__ESCI_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub FRM__ESCI_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcEsci.Dispose()
      dsEsci.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      '-------------------------------------------------
      'creo una nuova forma di pagamento
      If Not Salva() Then Return
      oCleAnaz.EscoNuovo()
      dcEsci.MoveLast()
      edTb_codesco.Enabled = True
      edTb_codesco.Focus()

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
      'cancello la forma di pagamento
      Dim dlgRes As DialogResult
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129006947467781051, "Confermi la cancellazione?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes

          If dsEsci.Tables("TABESCO").Rows.Count = 1 Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          dcEsci.RemoveAt(dcEsci.Position)
          oCleAnaz.EscoSalva(True)

          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcEsci, Me)
            bRemovBinding = False
            edTb_codesco.Enabled = True
          Else
            edTb_codesco.Enabled = False
          End If
          Return
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcEsci, Me)
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
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129006947579815169, "Ripristinare le modifiche apportate?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          If dsEsci.Tables("TABESCO").Rows.Count = 1 And dsEsci.Tables("TABESCO").Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          oCleAnaz.EscoRipristina(dcEsci.Position, dcEsci.Filter)

          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcEsci, Me)
            bRemovBinding = False
            edTb_codesco.Enabled = True
          Else
            edTb_codesco.Enabled = False
          End If
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcEsci, Me)
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

  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    '-------------------------------------------------
    'vado sul primo record
    If Not Salva() Then Return
    dcEsci.MoveFirst()
  End Sub

  Public Overridable Sub tlbPrecedente_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrecedente.ItemClick
    '-------------------------------------------------
    'vado sul record precedente
    If Not Salva() Then Return
    dcEsci.MovePrevious()
  End Sub

  Public Overridable Sub tlbSuccessivo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSuccessivo.ItemClick
    '-------------------------------------------------
    'vado sul record successivo
    If Not Salva() Then Return
    dcEsci.MoveNext()
  End Sub

  Public Overridable Sub tlbUltimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbUltimo.ItemClick
    '-------------------------------------------------
    'vado sull'ultimo record
    If Not Salva() Then Return
    dcEsci.MoveLast()
  End Sub

#End Region

  Public Overridable Function Salva() As Boolean
    Dim dRes As DialogResult
    Try
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      If oCleAnaz.EscoRecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129006947693724335, "Confermi il salvataggio?"))
        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oCleAnaz.EscoSalva(False) Then Return False
          If dsEsci.Tables("TABESCO").Rows.Count > 0 Then
            edTb_codesco.Enabled = False
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

  Public Overridable Sub cbTb_aztipcont_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTb_aztipcont.SelectedIndexChanged
    Try
      If oCleAnaz.dsShared.Tables("TABANAZ").Rows(0)!tb_azprofes.ToString = "S" And cbTb_aztipcont.SelectedValue = "S" Then
        GctlSetVisEnab(cbTb_azcoprof, False)
      Else
        cbTb_azcoprof.Enabled = False
        cbTb_azcoprof.SelectedValue = "N"
      End If

      Select Case cbTb_aztipcont.SelectedValue
        Case "C"
          If oCleAnaz.dsShared.Tables("TABANAZ").Rows(0)!tb_azprofes.ToString = "N" Then
            cbTb_aztipcont.SelectedValue = "O"
            oApp.MsgBoxErr(oApp.Tr(Me, 128644894378281250, "La ditta non è di tipo professionista. Scelta effettuata non corretta"))
          End If
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
End Class