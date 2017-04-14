Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__ESCI
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
  Public WithEvents lbTb_dtbilsta As NTSInformatica.NTSLabel
  Public WithEvents edTb_dtbilsta As NTSInformatica.NTSTextBoxData
  Public WithEvents lbTb_dtbileff As NTSInformatica.NTSLabel
  Public WithEvents edTb_dtbileff As NTSInformatica.NTSTextBoxData
  Public WithEvents ckTb_bilstraor As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_flopestr As NTSInformatica.NTSCheckBox
  Public WithEvents lbTb_numestr As NTSInformatica.NTSLabel
  Public WithEvents edTb_numestr As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_cdtsitso As NTSInformatica.NTSLabel
  Public WithEvents edTb_cdtsitso As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_aztipcont As NTSInformatica.NTSLabel
  Public WithEvents cbTb_aztipcont As NTSInformatica.NTSComboBox
  Public WithEvents lbTb_azcoprof As NTSInformatica.NTSLabel
  Public WithEvents cbTb_azcoprof As NTSInformatica.NTSComboBox
  Public WithEvents lbTb_azcosoga As NTSInformatica.NTSLabel
  Public WithEvents cbTb_azcosoga As NTSInformatica.NTSComboBox
  Public WithEvents ckTb_forfflge As NTSInformatica.NTSCheckBox
  Public WithEvents lbTb_forfctre As NTSInformatica.NTSLabel
  Public WithEvents edTb_forfctre As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckTb_forfctiv As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_azconcfo As NTSInformatica.NTSCheckBox
  Public WithEvents lbTb_perricr1 As NTSInformatica.NTSLabel
  Public WithEvents edTb_perricr1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_perricr2 As NTSInformatica.NTSLabel
  Public WithEvents edTb_perricr2 As NTSInformatica.NTSTextBoxNum
#End Region

#Region "Inizializzazione"
  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__ESCI))
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
    Me.lbTb_dtbilsta = New NTSInformatica.NTSLabel
    Me.edTb_dtbilsta = New NTSInformatica.NTSTextBoxData
    Me.lbTb_dtbileff = New NTSInformatica.NTSLabel
    Me.edTb_dtbileff = New NTSInformatica.NTSTextBoxData
    Me.ckTb_bilstraor = New NTSInformatica.NTSCheckBox
    Me.ckTb_flopestr = New NTSInformatica.NTSCheckBox
    Me.lbTb_numestr = New NTSInformatica.NTSLabel
    Me.edTb_numestr = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_cdtsitso = New NTSInformatica.NTSLabel
    Me.edTb_cdtsitso = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_aztipcont = New NTSInformatica.NTSLabel
    Me.cbTb_aztipcont = New NTSInformatica.NTSComboBox
    Me.lbTb_azcoprof = New NTSInformatica.NTSLabel
    Me.cbTb_azcoprof = New NTSInformatica.NTSComboBox
    Me.lbTb_azcosoga = New NTSInformatica.NTSLabel
    Me.cbTb_azcosoga = New NTSInformatica.NTSComboBox
    Me.ckTb_forfflge = New NTSInformatica.NTSCheckBox
    Me.lbTb_forfctre = New NTSInformatica.NTSLabel
    Me.edTb_forfctre = New NTSInformatica.NTSTextBoxNum
    Me.ckTb_forfctiv = New NTSInformatica.NTSCheckBox
    Me.ckTb_azconcfo = New NTSInformatica.NTSCheckBox
    Me.lbTb_perricr1 = New NTSInformatica.NTSLabel
    Me.edTb_perricr1 = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_perricr2 = New NTSInformatica.NTSLabel
    Me.edTb_perricr2 = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_cdtsitso = New NTSInformatica.NTSLabel
    Me.lbXx_forfctre = New NTSInformatica.NTSLabel
    Me.lbXx_numestr = New NTSInformatica.NTSLabel
    Me.edTb_codescg = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_codescg = New NTSInformatica.NTSLabel
    Me.lbXX_codescg = New NTSInformatica.NTSLabel
    Me.ckTb_gestcadp = New NTSInformatica.NTSCheckBox
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codesco.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_desesco.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_dtineser.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_dtfieser.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_dtbilsta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_dtbileff.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_bilstraor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_flopestr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_numestr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_cdtsitso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_aztipcont.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_azcoprof.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_azcosoga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_forfflge.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_forfctre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_forfctiv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_azconcfo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_perricr1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_perricr2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codescg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_gestcadp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.tlbNuovo.GlyphPath = ""
    Me.tlbNuovo.Id = 0
    Me.tlbNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2)
    Me.tlbNuovo.Name = "tlbNuovo"
    Me.tlbNuovo.Visible = True
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.GlyphPath = ""
    Me.tlbSalva.Id = 1
    Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.GlyphPath = ""
    Me.tlbRipristina.Id = 2
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.GlyphPath = ""
    Me.tlbCancella.Id = 3
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.GlyphPath = ""
    Me.tlbZoom.Id = 4
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbPrimo
    '
    Me.tlbPrimo.Caption = "Primo"
    Me.tlbPrimo.Glyph = CType(resources.GetObject("tlbPrimo.Glyph"), System.Drawing.Image)
    Me.tlbPrimo.GlyphPath = ""
    Me.tlbPrimo.Id = 5
    Me.tlbPrimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P))
    Me.tlbPrimo.Name = "tlbPrimo"
    Me.tlbPrimo.Visible = True
    '
    'tlbPrecedente
    '
    Me.tlbPrecedente.Caption = "Precedente"
    Me.tlbPrecedente.Glyph = CType(resources.GetObject("tlbPrecedente.Glyph"), System.Drawing.Image)
    Me.tlbPrecedente.GlyphPath = ""
    Me.tlbPrecedente.Id = 6
    Me.tlbPrecedente.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
    Me.tlbPrecedente.Name = "tlbPrecedente"
    Me.tlbPrecedente.Visible = True
    '
    'tlbSuccessivo
    '
    Me.tlbSuccessivo.Caption = "Successivo"
    Me.tlbSuccessivo.Glyph = CType(resources.GetObject("tlbSuccessivo.Glyph"), System.Drawing.Image)
    Me.tlbSuccessivo.GlyphPath = ""
    Me.tlbSuccessivo.Id = 7
    Me.tlbSuccessivo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
    Me.tlbSuccessivo.Name = "tlbSuccessivo"
    Me.tlbSuccessivo.Visible = True
    '
    'tlbUltimo
    '
    Me.tlbUltimo.Caption = "Ultimo"
    Me.tlbUltimo.Glyph = CType(resources.GetObject("tlbUltimo.Glyph"), System.Drawing.Image)
    Me.tlbUltimo.GlyphPath = ""
    Me.tlbUltimo.Id = 20
    Me.tlbUltimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 18
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 19
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'lbTb_codesco
    '
    Me.lbTb_codesco.AutoSize = True
    Me.lbTb_codesco.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_codesco.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbTb_codesco.Location = New System.Drawing.Point(9, 47)
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
    Me.edTb_codesco.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTb_codesco.EditValue = "0"
    Me.edTb_codesco.Location = New System.Drawing.Point(164, 44)
    Me.edTb_codesco.Name = "edTb_codesco"
    Me.edTb_codesco.NTSDbField = ""
    Me.edTb_codesco.NTSFormat = "0"
    Me.edTb_codesco.NTSForzaVisZoom = False
    Me.edTb_codesco.NTSOldValue = ""
    Me.edTb_codesco.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codesco.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codesco.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codesco.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codesco.Properties.AutoHeight = False
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
    Me.lbTb_desesco.Location = New System.Drawing.Point(249, 47)
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
    Me.edTb_desesco.Location = New System.Drawing.Point(324, 44)
    Me.edTb_desesco.Name = "edTb_desesco"
    Me.edTb_desesco.NTSDbField = ""
    Me.edTb_desesco.NTSForzaVisZoom = False
    Me.edTb_desesco.NTSOldValue = ""
    Me.edTb_desesco.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_desesco.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_desesco.Properties.AutoHeight = False
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
    Me.lbTb_dtineser.Location = New System.Drawing.Point(9, 73)
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
    Me.edTb_dtineser.Location = New System.Drawing.Point(164, 70)
    Me.edTb_dtineser.Name = "edTb_dtineser"
    Me.edTb_dtineser.NTSDbField = ""
    Me.edTb_dtineser.NTSForzaVisZoom = False
    Me.edTb_dtineser.NTSOldValue = ""
    Me.edTb_dtineser.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_dtineser.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_dtineser.Properties.AutoHeight = False
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
    Me.edTb_dtfieser.Location = New System.Drawing.Point(324, 70)
    Me.edTb_dtfieser.Name = "edTb_dtfieser"
    Me.edTb_dtfieser.NTSDbField = ""
    Me.edTb_dtfieser.NTSForzaVisZoom = False
    Me.edTb_dtfieser.NTSOldValue = ""
    Me.edTb_dtfieser.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_dtfieser.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_dtfieser.Properties.AutoHeight = False
    Me.edTb_dtfieser.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_dtfieser.Properties.MaxLength = 65536
    Me.edTb_dtfieser.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_dtfieser.Size = New System.Drawing.Size(84, 20)
    Me.edTb_dtfieser.TabIndex = 503
    '
    'lbTb_dtbilsta
    '
    Me.lbTb_dtbilsta.AutoSize = True
    Me.lbTb_dtbilsta.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_dtbilsta.Location = New System.Drawing.Point(34, 519)
    Me.lbTb_dtbilsta.Name = "lbTb_dtbilsta"
    Me.lbTb_dtbilsta.NTSDbField = ""
    Me.lbTb_dtbilsta.Size = New System.Drawing.Size(231, 13)
    Me.lbTb_dtbilsta.TabIndex = 14
    Me.lbTb_dtbilsta.Text = "Data approvazione bilancio: Termine statutario"
    Me.lbTb_dtbilsta.Tooltip = ""
    Me.lbTb_dtbilsta.UseMnemonic = False
    Me.lbTb_dtbilsta.Visible = False
    '
    'edTb_dtbilsta
    '
    Me.edTb_dtbilsta.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_dtbilsta.EditValue = "01/01/1900"
    Me.edTb_dtbilsta.Location = New System.Drawing.Point(334, 516)
    Me.edTb_dtbilsta.Name = "edTb_dtbilsta"
    Me.edTb_dtbilsta.NTSDbField = ""
    Me.edTb_dtbilsta.NTSForzaVisZoom = False
    Me.edTb_dtbilsta.NTSOldValue = ""
    Me.edTb_dtbilsta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_dtbilsta.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_dtbilsta.Properties.AutoHeight = False
    Me.edTb_dtbilsta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_dtbilsta.Properties.MaxLength = 65536
    Me.edTb_dtbilsta.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_dtbilsta.Size = New System.Drawing.Size(84, 20)
    Me.edTb_dtbilsta.TabIndex = 504
    Me.edTb_dtbilsta.Visible = False
    '
    'lbTb_dtbileff
    '
    Me.lbTb_dtbileff.AutoSize = True
    Me.lbTb_dtbileff.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_dtbileff.Location = New System.Drawing.Point(426, 519)
    Me.lbTb_dtbileff.Name = "lbTb_dtbileff"
    Me.lbTb_dtbileff.NTSDbField = ""
    Me.lbTb_dtbileff.Size = New System.Drawing.Size(75, 13)
    Me.lbTb_dtbileff.TabIndex = 15
    Me.lbTb_dtbileff.Text = "Data effettiva"
    Me.lbTb_dtbileff.Tooltip = ""
    Me.lbTb_dtbileff.UseMnemonic = False
    Me.lbTb_dtbileff.Visible = False
    '
    'edTb_dtbileff
    '
    Me.edTb_dtbileff.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_dtbileff.EditValue = "01/01/1900"
    Me.edTb_dtbileff.Location = New System.Drawing.Point(546, 516)
    Me.edTb_dtbileff.Name = "edTb_dtbileff"
    Me.edTb_dtbileff.NTSDbField = ""
    Me.edTb_dtbileff.NTSForzaVisZoom = False
    Me.edTb_dtbileff.NTSOldValue = ""
    Me.edTb_dtbileff.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_dtbileff.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_dtbileff.Properties.AutoHeight = False
    Me.edTb_dtbileff.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_dtbileff.Properties.MaxLength = 65536
    Me.edTb_dtbileff.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_dtbileff.Size = New System.Drawing.Size(84, 20)
    Me.edTb_dtbileff.TabIndex = 505
    Me.edTb_dtbileff.Visible = False
    '
    'ckTb_bilstraor
    '
    Me.ckTb_bilstraor.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_bilstraor.Location = New System.Drawing.Point(771, 461)
    Me.ckTb_bilstraor.Name = "ckTb_bilstraor"
    Me.ckTb_bilstraor.NTSCheckValue = "S"
    Me.ckTb_bilstraor.NTSUnCheckValue = "N"
    Me.ckTb_bilstraor.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_bilstraor.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_bilstraor.Properties.AutoHeight = False
    Me.ckTb_bilstraor.Properties.Caption = "Gestione di bilancio diverso da 12 mesi"
    Me.ckTb_bilstraor.Size = New System.Drawing.Size(210, 19)
    Me.ckTb_bilstraor.TabIndex = 506
    Me.ckTb_bilstraor.Visible = False
    '
    'ckTb_flopestr
    '
    Me.ckTb_flopestr.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_flopestr.Location = New System.Drawing.Point(716, 581)
    Me.ckTb_flopestr.Name = "ckTb_flopestr"
    Me.ckTb_flopestr.NTSCheckValue = "S"
    Me.ckTb_flopestr.NTSUnCheckValue = "N"
    Me.ckTb_flopestr.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_flopestr.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_flopestr.Properties.AutoHeight = False
    Me.ckTb_flopestr.Properties.Caption = "Operaz. straord. nel periodo d'imposta"
    Me.ckTb_flopestr.Size = New System.Drawing.Size(219, 19)
    Me.ckTb_flopestr.TabIndex = 507
    Me.ckTb_flopestr.Visible = False
    '
    'lbTb_numestr
    '
    Me.lbTb_numestr.AutoSize = True
    Me.lbTb_numestr.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_numestr.Location = New System.Drawing.Point(704, 550)
    Me.lbTb_numestr.Name = "lbTb_numestr"
    Me.lbTb_numestr.NTSDbField = ""
    Me.lbTb_numestr.Size = New System.Drawing.Size(74, 13)
    Me.lbTb_numestr.TabIndex = 18
    Me.lbTb_numestr.Text = "Esercizio corr."
    Me.lbTb_numestr.Tooltip = ""
    Me.lbTb_numestr.UseMnemonic = False
    Me.lbTb_numestr.Visible = False
    '
    'edTb_numestr
    '
    Me.edTb_numestr.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTb_numestr.EditValue = "0"
    Me.edTb_numestr.Location = New System.Drawing.Point(789, 549)
    Me.edTb_numestr.Name = "edTb_numestr"
    Me.edTb_numestr.NTSDbField = ""
    Me.edTb_numestr.NTSFormat = "0"
    Me.edTb_numestr.NTSForzaVisZoom = False
    Me.edTb_numestr.NTSOldValue = ""
    Me.edTb_numestr.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_numestr.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_numestr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_numestr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_numestr.Properties.AutoHeight = False
    Me.edTb_numestr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_numestr.Properties.MaxLength = 65536
    Me.edTb_numestr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_numestr.Size = New System.Drawing.Size(59, 20)
    Me.edTb_numestr.TabIndex = 508
    Me.edTb_numestr.Visible = False
    '
    'lbTb_cdtsitso
    '
    Me.lbTb_cdtsitso.AutoSize = True
    Me.lbTb_cdtsitso.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_cdtsitso.Location = New System.Drawing.Point(34, 570)
    Me.lbTb_cdtsitso.Name = "lbTb_cdtsitso"
    Me.lbTb_cdtsitso.NTSDbField = ""
    Me.lbTb_cdtsitso.Size = New System.Drawing.Size(127, 13)
    Me.lbTb_cdtsitso.TabIndex = 19
    Me.lbTb_cdtsitso.Text = "Codice situazione società"
    Me.lbTb_cdtsitso.Tooltip = ""
    Me.lbTb_cdtsitso.UseMnemonic = False
    Me.lbTb_cdtsitso.Visible = False
    '
    'edTb_cdtsitso
    '
    Me.edTb_cdtsitso.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_cdtsitso.EditValue = "0"
    Me.edTb_cdtsitso.Location = New System.Drawing.Point(189, 563)
    Me.edTb_cdtsitso.Name = "edTb_cdtsitso"
    Me.edTb_cdtsitso.NTSDbField = ""
    Me.edTb_cdtsitso.NTSFormat = "0"
    Me.edTb_cdtsitso.NTSForzaVisZoom = False
    Me.edTb_cdtsitso.NTSOldValue = ""
    Me.edTb_cdtsitso.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_cdtsitso.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_cdtsitso.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_cdtsitso.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_cdtsitso.Properties.AutoHeight = False
    Me.edTb_cdtsitso.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_cdtsitso.Properties.MaxLength = 65536
    Me.edTb_cdtsitso.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_cdtsitso.Size = New System.Drawing.Size(84, 20)
    Me.edTb_cdtsitso.TabIndex = 509
    Me.edTb_cdtsitso.Visible = False
    '
    'lbTb_aztipcont
    '
    Me.lbTb_aztipcont.AutoSize = True
    Me.lbTb_aztipcont.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_aztipcont.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbTb_aztipcont.Location = New System.Drawing.Point(9, 99)
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
    Me.cbTb_aztipcont.Location = New System.Drawing.Point(164, 96)
    Me.cbTb_aztipcont.Name = "cbTb_aztipcont"
    Me.cbTb_aztipcont.NTSDbField = ""
    Me.cbTb_aztipcont.Properties.AutoHeight = False
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
    Me.lbTb_azcoprof.Location = New System.Drawing.Point(9, 124)
    Me.lbTb_azcoprof.Name = "lbTb_azcoprof"
    Me.lbTb_azcoprof.NTSDbField = ""
    Me.lbTb_azcoprof.Size = New System.Drawing.Size(129, 13)
    Me.lbTb_azcoprof.TabIndex = 21
    Me.lbTb_azcoprof.Text = "Tipo gest. incassi/pagam."
    Me.lbTb_azcoprof.Tooltip = ""
    Me.lbTb_azcoprof.UseMnemonic = False
    '
    'cbTb_azcoprof
    '
    Me.cbTb_azcoprof.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_azcoprof.DataSource = Nothing
    Me.cbTb_azcoprof.DisplayMember = ""
    Me.cbTb_azcoprof.Location = New System.Drawing.Point(164, 121)
    Me.cbTb_azcoprof.Name = "cbTb_azcoprof"
    Me.cbTb_azcoprof.NTSDbField = ""
    Me.cbTb_azcoprof.Properties.AutoHeight = False
    Me.cbTb_azcoprof.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_azcoprof.Properties.DropDownRows = 30
    Me.cbTb_azcoprof.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_azcoprof.SelectedValue = ""
    Me.cbTb_azcoprof.Size = New System.Drawing.Size(244, 20)
    Me.cbTb_azcoprof.TabIndex = 511
    Me.cbTb_azcoprof.ValueMember = ""
    '
    'lbTb_azcosoga
    '
    Me.lbTb_azcosoga.AutoSize = True
    Me.lbTb_azcosoga.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azcosoga.Location = New System.Drawing.Point(210, 475)
    Me.lbTb_azcosoga.Name = "lbTb_azcosoga"
    Me.lbTb_azcosoga.NTSDbField = ""
    Me.lbTb_azcosoga.Size = New System.Drawing.Size(102, 13)
    Me.lbTb_azcosoga.TabIndex = 22
    Me.lbTb_azcosoga.Text = "Soggetto agevolato"
    Me.lbTb_azcosoga.Tooltip = ""
    Me.lbTb_azcosoga.UseMnemonic = False
    Me.lbTb_azcosoga.Visible = False
    '
    'cbTb_azcosoga
    '
    Me.cbTb_azcosoga.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_azcosoga.DataSource = Nothing
    Me.cbTb_azcosoga.DisplayMember = ""
    Me.cbTb_azcosoga.Location = New System.Drawing.Point(365, 472)
    Me.cbTb_azcosoga.Name = "cbTb_azcosoga"
    Me.cbTb_azcosoga.NTSDbField = ""
    Me.cbTb_azcosoga.Properties.AutoHeight = False
    Me.cbTb_azcosoga.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_azcosoga.Properties.DropDownRows = 30
    Me.cbTb_azcosoga.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_azcosoga.SelectedValue = ""
    Me.cbTb_azcosoga.Size = New System.Drawing.Size(244, 20)
    Me.cbTb_azcosoga.TabIndex = 512
    Me.cbTb_azcosoga.ValueMember = ""
    Me.cbTb_azcosoga.Visible = False
    '
    'ckTb_forfflge
    '
    Me.ckTb_forfflge.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_forfflge.Location = New System.Drawing.Point(12, 173)
    Me.ckTb_forfflge.Name = "ckTb_forfflge"
    Me.ckTb_forfflge.NTSCheckValue = "S"
    Me.ckTb_forfflge.NTSUnCheckValue = "N"
    Me.ckTb_forfflge.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_forfflge.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_forfflge.Properties.AutoHeight = False
    Me.ckTb_forfflge.Properties.Caption = "Soggetto forfettario"
    Me.ckTb_forfflge.Size = New System.Drawing.Size(127, 19)
    Me.ckTb_forfflge.TabIndex = 513
    '
    'lbTb_forfctre
    '
    Me.lbTb_forfctre.AutoSize = True
    Me.lbTb_forfctre.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_forfctre.Location = New System.Drawing.Point(9, 276)
    Me.lbTb_forfctre.Name = "lbTb_forfctre"
    Me.lbTb_forfctre.NTSDbField = ""
    Me.lbTb_forfctre.Size = New System.Drawing.Size(63, 13)
    Me.lbTb_forfctre.TabIndex = 24
    Me.lbTb_forfctre.Text = "Cod. forfait"
    Me.lbTb_forfctre.Tooltip = ""
    Me.lbTb_forfctre.UseMnemonic = False
    Me.lbTb_forfctre.Visible = False
    '
    'edTb_forfctre
    '
    Me.edTb_forfctre.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_forfctre.EditValue = "0"
    Me.edTb_forfctre.Location = New System.Drawing.Point(164, 173)
    Me.edTb_forfctre.Name = "edTb_forfctre"
    Me.edTb_forfctre.NTSDbField = ""
    Me.edTb_forfctre.NTSFormat = "0"
    Me.edTb_forfctre.NTSForzaVisZoom = False
    Me.edTb_forfctre.NTSOldValue = ""
    Me.edTb_forfctre.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_forfctre.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_forfctre.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_forfctre.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_forfctre.Properties.AutoHeight = False
    Me.edTb_forfctre.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_forfctre.Properties.MaxLength = 65536
    Me.edTb_forfctre.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_forfctre.Size = New System.Drawing.Size(84, 20)
    Me.edTb_forfctre.TabIndex = 514
    '
    'ckTb_forfctiv
    '
    Me.ckTb_forfctiv.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_forfctiv.Location = New System.Drawing.Point(414, 70)
    Me.ckTb_forfctiv.Name = "ckTb_forfctiv"
    Me.ckTb_forfctiv.NTSCheckValue = "S"
    Me.ckTb_forfctiv.NTSUnCheckValue = "N"
    Me.ckTb_forfctiv.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_forfctiv.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_forfctiv.Properties.AutoHeight = False
    Me.ckTb_forfctiv.Properties.Caption = "Primo esercizio"
    Me.ckTb_forfctiv.Size = New System.Drawing.Size(94, 19)
    Me.ckTb_forfctiv.TabIndex = 515
    '
    'ckTb_azconcfo
    '
    Me.ckTb_azconcfo.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.ckTb_azconcfo.Location = New System.Drawing.Point(681, 390)
    Me.ckTb_azconcfo.Name = "ckTb_azconcfo"
    Me.ckTb_azconcfo.NTSCheckValue = "S"
    Me.ckTb_azconcfo.NTSUnCheckValue = "N"
    Me.ckTb_azconcfo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_azconcfo.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_azconcfo.Properties.AutoHeight = False
    Me.ckTb_azconcfo.Properties.Caption = "Ente non commerciale con forfait redditi (disciplina ex art.109 bis)"
    Me.ckTb_azconcfo.Size = New System.Drawing.Size(340, 19)
    Me.ckTb_azconcfo.TabIndex = 516
    Me.ckTb_azconcfo.Visible = False
    '
    'lbTb_perricr1
    '
    Me.lbTb_perricr1.AutoSize = True
    Me.lbTb_perricr1.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_perricr1.Location = New System.Drawing.Point(34, 545)
    Me.lbTb_perricr1.Name = "lbTb_perricr1"
    Me.lbTb_perricr1.NTSDbField = ""
    Me.lbTb_perricr1.Size = New System.Drawing.Size(262, 13)
    Me.lbTb_perricr1.TabIndex = 27
    Me.lbTb_perricr1.Text = "Stampa situazione E/P: 1^ % ricarico rimanenze finali"
    Me.lbTb_perricr1.Tooltip = ""
    Me.lbTb_perricr1.UseMnemonic = False
    Me.lbTb_perricr1.Visible = False
    '
    'edTb_perricr1
    '
    Me.edTb_perricr1.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTb_perricr1.EditValue = "0"
    Me.edTb_perricr1.Location = New System.Drawing.Point(334, 542)
    Me.edTb_perricr1.Name = "edTb_perricr1"
    Me.edTb_perricr1.NTSDbField = ""
    Me.edTb_perricr1.NTSFormat = "0"
    Me.edTb_perricr1.NTSForzaVisZoom = False
    Me.edTb_perricr1.NTSOldValue = ""
    Me.edTb_perricr1.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_perricr1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_perricr1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_perricr1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_perricr1.Properties.AutoHeight = False
    Me.edTb_perricr1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_perricr1.Properties.MaxLength = 65536
    Me.edTb_perricr1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_perricr1.Size = New System.Drawing.Size(84, 20)
    Me.edTb_perricr1.TabIndex = 517
    Me.edTb_perricr1.Visible = False
    '
    'lbTb_perricr2
    '
    Me.lbTb_perricr2.AutoSize = True
    Me.lbTb_perricr2.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_perricr2.Location = New System.Drawing.Point(426, 545)
    Me.lbTb_perricr2.Name = "lbTb_perricr2"
    Me.lbTb_perricr2.NTSDbField = ""
    Me.lbTb_perricr2.Size = New System.Drawing.Size(112, 13)
    Me.lbTb_perricr2.TabIndex = 28
    Me.lbTb_perricr2.Text = "2^ % ricarico rim. fin."
    Me.lbTb_perricr2.Tooltip = ""
    Me.lbTb_perricr2.UseMnemonic = False
    Me.lbTb_perricr2.Visible = False
    '
    'edTb_perricr2
    '
    Me.edTb_perricr2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_perricr2.EditValue = "0"
    Me.edTb_perricr2.Location = New System.Drawing.Point(546, 542)
    Me.edTb_perricr2.Name = "edTb_perricr2"
    Me.edTb_perricr2.NTSDbField = ""
    Me.edTb_perricr2.NTSFormat = "0"
    Me.edTb_perricr2.NTSForzaVisZoom = False
    Me.edTb_perricr2.NTSOldValue = ""
    Me.edTb_perricr2.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_perricr2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_perricr2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_perricr2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_perricr2.Properties.AutoHeight = False
    Me.edTb_perricr2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_perricr2.Properties.MaxLength = 65536
    Me.edTb_perricr2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_perricr2.Size = New System.Drawing.Size(84, 20)
    Me.edTb_perricr2.TabIndex = 518
    Me.edTb_perricr2.Visible = False
    '
    'lbXx_cdtsitso
    '
    Me.lbXx_cdtsitso.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_cdtsitso.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_cdtsitso.Location = New System.Drawing.Point(281, 565)
    Me.lbXx_cdtsitso.Name = "lbXx_cdtsitso"
    Me.lbXx_cdtsitso.NTSDbField = ""
    Me.lbXx_cdtsitso.Size = New System.Drawing.Size(364, 20)
    Me.lbXx_cdtsitso.TabIndex = 580
    Me.lbXx_cdtsitso.Text = "lbXx_cdtsitso"
    Me.lbXx_cdtsitso.Tooltip = ""
    Me.lbXx_cdtsitso.UseMnemonic = False
    Me.lbXx_cdtsitso.Visible = False
    '
    'lbXx_forfctre
    '
    Me.lbXx_forfctre.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_forfctre.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_forfctre.Location = New System.Drawing.Point(256, 172)
    Me.lbXx_forfctre.Name = "lbXx_forfctre"
    Me.lbXx_forfctre.NTSDbField = ""
    Me.lbXx_forfctre.Size = New System.Drawing.Size(364, 20)
    Me.lbXx_forfctre.TabIndex = 581
    Me.lbXx_forfctre.Text = "lbXx_forfctre"
    Me.lbXx_forfctre.Tooltip = ""
    Me.lbXx_forfctre.UseMnemonic = False
    '
    'lbXx_numestr
    '
    Me.lbXx_numestr.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_numestr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_numestr.Location = New System.Drawing.Point(854, 549)
    Me.lbXx_numestr.Name = "lbXx_numestr"
    Me.lbXx_numestr.NTSDbField = ""
    Me.lbXx_numestr.Size = New System.Drawing.Size(206, 20)
    Me.lbXx_numestr.TabIndex = 582
    Me.lbXx_numestr.Text = "lbXx_numestr"
    Me.lbXx_numestr.Tooltip = ""
    Me.lbXx_numestr.UseMnemonic = False
    Me.lbXx_numestr.Visible = False
    '
    'edTb_codescg
    '
    Me.edTb_codescg.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codescg.EditValue = "0"
    Me.edTb_codescg.Location = New System.Drawing.Point(164, 147)
    Me.edTb_codescg.Name = "edTb_codescg"
    Me.edTb_codescg.NTSDbField = ""
    Me.edTb_codescg.NTSFormat = "0"
    Me.edTb_codescg.NTSForzaVisZoom = False
    Me.edTb_codescg.NTSOldValue = ""
    Me.edTb_codescg.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codescg.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codescg.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codescg.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codescg.Properties.AutoHeight = False
    Me.edTb_codescg.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codescg.Properties.MaxLength = 65536
    Me.edTb_codescg.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codescg.Size = New System.Drawing.Size(84, 20)
    Me.edTb_codescg.TabIndex = 584
    '
    'lbTb_codescg
    '
    Me.lbTb_codescg.AutoSize = True
    Me.lbTb_codescg.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_codescg.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbTb_codescg.Location = New System.Drawing.Point(9, 150)
    Me.lbTb_codescg.Name = "lbTb_codescg"
    Me.lbTb_codescg.NTSDbField = ""
    Me.lbTb_codescg.Size = New System.Drawing.Size(150, 13)
    Me.lbTb_codescg.TabIndex = 583
    Me.lbTb_codescg.Text = "Codice esercizio (gruppo)"
    Me.lbTb_codescg.Tooltip = ""
    Me.lbTb_codescg.UseMnemonic = False
    '
    'lbXX_codescg
    '
    Me.lbXX_codescg.BackColor = System.Drawing.Color.Transparent
    Me.lbXX_codescg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXX_codescg.Location = New System.Drawing.Point(256, 147)
    Me.lbXX_codescg.Name = "lbXX_codescg"
    Me.lbXX_codescg.NTSDbField = ""
    Me.lbXX_codescg.Size = New System.Drawing.Size(364, 20)
    Me.lbXX_codescg.TabIndex = 679
    Me.lbXX_codescg.Tooltip = ""
    Me.lbXX_codescg.UseMnemonic = False
    '
    'ckTb_gestcadp
    '
    Me.ckTb_gestcadp.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_gestcadp.Location = New System.Drawing.Point(414, 122)
    Me.ckTb_gestcadp.Name = "ckTb_gestcadp"
    Me.ckTb_gestcadp.NTSCheckValue = "S"
    Me.ckTb_gestcadp.NTSUnCheckValue = "N"
    Me.ckTb_gestcadp.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_gestcadp.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_gestcadp.Properties.AutoHeight = False
    Me.ckTb_gestcadp.Properties.Caption = "Gestione cont. analit. duplice contabile"
    Me.ckTb_gestcadp.Size = New System.Drawing.Size(210, 19)
    Me.ckTb_gestcadp.TabIndex = 680
    Me.ckTb_gestcadp.Visible = False
    '
    'FRM__ESCI
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(628, 202)
    Me.Controls.Add(Me.ckTb_gestcadp)
    Me.Controls.Add(Me.lbXX_codescg)
    Me.Controls.Add(Me.edTb_codescg)
    Me.Controls.Add(Me.lbTb_codescg)
    Me.Controls.Add(Me.lbXx_numestr)
    Me.Controls.Add(Me.lbXx_forfctre)
    Me.Controls.Add(Me.lbXx_cdtsitso)
    Me.Controls.Add(Me.lbTb_dtbilsta)
    Me.Controls.Add(Me.edTb_dtbilsta)
    Me.Controls.Add(Me.lbTb_dtineser)
    Me.Controls.Add(Me.lbTb_dtbileff)
    Me.Controls.Add(Me.cbTb_azcosoga)
    Me.Controls.Add(Me.edTb_dtbileff)
    Me.Controls.Add(Me.edTb_desesco)
    Me.Controls.Add(Me.edTb_dtineser)
    Me.Controls.Add(Me.ckTb_bilstraor)
    Me.Controls.Add(Me.edTb_dtfieser)
    Me.Controls.Add(Me.lbTb_cdtsitso)
    Me.Controls.Add(Me.lbTb_desesco)
    Me.Controls.Add(Me.edTb_cdtsitso)
    Me.Controls.Add(Me.edTb_numestr)
    Me.Controls.Add(Me.lbTb_numestr)
    Me.Controls.Add(Me.lbTb_perricr1)
    Me.Controls.Add(Me.edTb_perricr1)
    Me.Controls.Add(Me.ckTb_azconcfo)
    Me.Controls.Add(Me.lbTb_perricr2)
    Me.Controls.Add(Me.lbTb_forfctre)
    Me.Controls.Add(Me.edTb_perricr2)
    Me.Controls.Add(Me.edTb_codesco)
    Me.Controls.Add(Me.ckTb_flopestr)
    Me.Controls.Add(Me.lbTb_codesco)
    Me.Controls.Add(Me.ckTb_forfctiv)
    Me.Controls.Add(Me.cbTb_aztipcont)
    Me.Controls.Add(Me.lbTb_aztipcont)
    Me.Controls.Add(Me.lbTb_azcoprof)
    Me.Controls.Add(Me.ckTb_forfflge)
    Me.Controls.Add(Me.edTb_forfctre)
    Me.Controls.Add(Me.lbTb_azcosoga)
    Me.Controls.Add(Me.cbTb_azcoprof)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__ESCI"
    Me.Text = "ESERCIZI CONTABILI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codesco.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_desesco.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_dtineser.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_dtfieser.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_dtbilsta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_dtbileff.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_bilstraor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_flopestr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_numestr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_cdtsitso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_aztipcont.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_azcoprof.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_azcosoga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_forfflge.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_forfctre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_forfctiv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_azconcfo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_perricr1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_perricr2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codescg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_gestcadp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
      edTb_dtbilsta.NTSSetParam(oMenu, oApp.Tr(Me, 128647315792031250, "Data approvazione bilancio - Termine statutario"), False, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      edTb_dtbileff.NTSSetParam(oMenu, oApp.Tr(Me, 128647315804375000, "Data approvazione bilancio - Data effettiva"), False, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      ckTb_bilstraor.NTSSetParam(oMenu, oApp.Tr(Me, 128647315817656250, "Gestione di un bilancio diverso da 12 mesi"), "S", "N")
      ckTb_flopestr.NTSSetParam(oMenu, oApp.Tr(Me, 128647315830156250, "Operazione straordinaria nel periodo d'imposta"), "S", "N")
      edTb_numestr.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128647315843750000, "Codice esercizio correlato"), tabesco)
      edTb_cdtsitso.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128647315855937500, "Codice situazione società"), tabsiso)
      cbTb_aztipcont.NTSSetParam(oApp.Tr(Me, 128647315868906250, "Tipo contabilità"))
      cbTb_azcoprof.NTSSetParam(oApp.Tr(Me, 128647315881406250, "Tipo gest. incassi/pagamenti"))
      cbTb_azcosoga.NTSSetParam(oApp.Tr(Me, 128647315894843750, "Soggetto agevolato"))
      ckTb_forfflge.NTSSetParam(oMenu, oApp.Tr(Me, 128647315908593750, "Soggetto forfettario"), "S", "N")
      edTb_forfctre.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128647315921250000, "Cod. forfait"), tabdfor)
      ckTb_forfctiv.NTSSetParam(oMenu, oApp.Tr(Me, 128647315933750000, "Primo esercizio"), "1", "0")
      ckTb_azconcfo.NTSSetParam(oMenu, oApp.Tr(Me, 128647315952500000, "Ente non commerciale con forfait redditi (disciplina ex art.109 bis)"), "S", "N")
      edTb_perricr1.NTSSetParam(oMenu, oApp.Tr(Me, 128647315965312500, "Prima % ricarica rimanenze finali"), oApp.FormatSconti, 6, 0, 100)
      edTb_perricr2.NTSSetParam(oMenu, oApp.Tr(Me, 128647315979218750, "Seconda % ricarico rimanenze finali"), oApp.FormatSconti, 6, 0, 100)
      edTb_codescg.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129159020095502453, "Codice esercizio a livello di gruppo aziende"), tabescg)
      ckTb_gestcadp.NTSSetParam(oMenu, oApp.Tr(Me, 129235085826250000, "Gestione cont. analit. duplice contabile"), "S", "N")

      edTb_codesco.NTSForzaVisZoom = False
      edTb_codesco.NTSSetRichiesto()
      edTb_desesco.NTSSetRichiesto()
      edTb_dtineser.NTSSetRichiesto()
      edTb_dtfieser.NTSSetRichiesto()
      cbTb_aztipcont.NTSSetRichiesto()
      edTb_numestr.NTSForzaVisZoom = False

      edTb_codescg.Visible = oCleAnaz.IsNuovaAnalitica
      lbTb_codescg.Visible = oCleAnaz.IsNuovaAnalitica
      lbXX_codescg.Visible = oCleAnaz.IsNuovaAnalitica

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
      Dim dttCosoga As New DataTable()

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

      dttCosoga.Columns.Add("cod", GetType(String))
      dttCosoga.Columns.Add("val", GetType(String))
      dttCosoga.Rows.Add(New Object() {"N", "No"})
      dttCosoga.Rows.Add(New Object() {"I", "Nuova iniziativa produttiva"})
      dttCosoga.Rows.Add(New Object() {"M", "Soggetto marginale"})
      dttCosoga.AcceptChanges()
      cbTb_azcosoga.DataSource = dttCosoga
      cbTb_azcosoga.ValueMember = "cod"
      cbTb_azcosoga.DisplayMember = "val"

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
      edTb_dtbilsta.NTSDbField = "TABESCO.tb_dtbilsta"
      edTb_dtbileff.NTSDbField = "TABESCO.tb_dtbileff"
      ckTb_bilstraor.NTSText.NTSDbField = "TABESCO.tb_bilstraor"
      ckTb_flopestr.NTSText.NTSDbField = "TABESCO.tb_flopestr"
      edTb_numestr.NTSDbField = "TABESCO.tb_numestr"
      edTb_cdtsitso.NTSDbField = "TABESCO.tb_cdtsitso"
      cbTb_aztipcont.NTSDbField = "TABESCO.tb_aztipcont"
      cbTb_azcoprof.NTSDbField = "TABESCO.tb_azcoprof"
      cbTb_azcosoga.NTSDbField = "TABESCO.tb_azcosoga"
      ckTb_forfflge.NTSText.NTSDbField = "TABESCO.tb_forfflge"
      edTb_forfctre.NTSDbField = "TABESCO.tb_forfctre"
      ckTb_forfctiv.NTSText.NTSDbField = "TABESCO.tb_forfctiv"
      ckTb_azconcfo.NTSText.NTSDbField = "TABESCO.tb_azconcfo"
      edTb_perricr1.NTSDbField = "TABESCO.tb_perricr1"
      edTb_perricr2.NTSDbField = "TABESCO.tb_perricr2"
      lbXx_cdtsitso.NTSDbField = "TABESCO.xx_cdtsitso"
      lbXx_forfctre.NTSDbField = "TABESCO.xx_forfctre"
      lbXx_numestr.NTSDbField = "TABESCO.xx_numestr"
      edTb_codescg.NTSDbField = "TABESCO.tb_codescg"
      lbXX_codescg.NTSDbField = "TABESCO.xx_codescg"
      ckTb_gestcadp.NTSText.NTSDbField = "TABESCO.tb_gestcadp"

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

      ckTb_forfflge_CheckedChanged(ckTb_forfflge, Nothing)
      ckTb_flopestr_CheckedChanged(ckTb_flopestr, Nothing)
      cbTb_aztipcont_SelectedIndexChanged(cbTb_aztipcont, Nothing)

      If CBool(bsModSupCAE And oMenu.ModuliSupDittaDitt(DittaCorrente)) Then GctlSetVisEnab(ckTb_gestcadp, True)

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

      If edTb_codescg.ContainsFocus Then
        oParam.strCodice = oCleAnaz.dsShared.Tables("TABANAZ").Rows(0)!tb_azcodgrua.ToString()
        NTSZOOM.strIn = NTSCStr(edTb_codescg.Text)
        NTSZOOM.ZoomStrIn("ZOOMTABESCG", "", oParam)
        If NTSZOOM.strIn <> edTb_codescg.Text Then edTb_codescg.Text = NTSZOOM.strIn
      Else
        'zoom standard di textbox e griglia
        NTSCallStandardZoom()
      End If
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

  Public Overridable Sub ckTb_forfflge_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckTb_forfflge.CheckedChanged
    Try
      If ckTb_forfflge.Checked Then
        GctlSetVisEnab(edTb_forfctre, False)
      Else
        edTb_forfctre.Enabled = False
        edTb_forfctre.NTSTextDB = "0"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckTb_flopestr_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckTb_flopestr.CheckedChanged
    Try
      If ckTb_flopestr.Checked Then
        GctlSetVisEnab(edTb_numestr, False)
      Else
        edTb_numestr.Enabled = False
        edTb_numestr.NTSTextDB = "0"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cbTb_aztipcont_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTb_aztipcont.SelectedIndexChanged
    Try
      If oCleAnaz.dsShared.Tables("TABANAZ").Rows(0)!tb_azprofes.ToString = "S" And cbTb_aztipcont.SelectedValue = "S" Then
        GctlSetVisEnab(cbTb_azcoprof, False)
      Else
        cbTb_azcoprof.Enabled = False
        cbTb_azcoprof.SelectedValue = "N"
      End If

      Select Case cbTb_aztipcont.SelectedValue
        Case "O"
          ckTb_forfflge.Enabled = False
          If ckTb_forfflge.Checked Then ckTb_forfflge.Checked = False
        Case "S"
          GctlSetVisEnab(ckTb_forfflge, False)
        Case "C"
          ckTb_forfflge.Enabled = False
          If ckTb_forfflge.Checked Then ckTb_forfflge.Checked = False
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

