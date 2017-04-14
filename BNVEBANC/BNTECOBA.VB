Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMTECOBA

#Region "Variabili"
  Public oCallParams As CLE__CLDP
  Public oCleBanc As CLEVEBANC
  Public dsCoba As New DataSet
  Public dcCoba As New BindingSource()

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnCoba As NTSInformatica.NTSPanel
  Public WithEvents fmSpese As NTSInformatica.NTSGroupBox
  Public WithEvents fmValuta As NTSInformatica.NTSGroupBox
  Public WithEvents lbCb_causale As NTSInformatica.NTSLabel
  Public WithEvents lbCb_codbanc As NTSInformatica.NTSLabel
  Public WithEvents lbCb_causaleLabel As NTSInformatica.NTSLabel
  Public WithEvents lbCb_codbancLabel As NTSInformatica.NTSLabel
  Public WithEvents edCb_causale As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCb_codbanc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCb_valfiss As NTSInformatica.NTSLabel
  Public WithEvents lbCb_avaind As NTSInformatica.NTSLabel
  Public WithEvents lbCb_ggvalu As NTSInformatica.NTSLabel
  Public WithEvents edCb_impboll As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCb_spesaop As NTSInformatica.NTSLabel
  Public WithEvents ckCb_addspop As NTSInformatica.NTSCheckBox
  Public WithEvents edCb_spesaop As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCb_ggvalu As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCb_impboll As NTSInformatica.NTSLabel
  Public WithEvents cbCb_valfiss As NTSInformatica.NTSComboBox
  Public WithEvents cbCb_avaind As NTSInformatica.NTSComboBox

#End Region

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMTECOBA))
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
    Me.pnCoba = New NTSInformatica.NTSPanel
    Me.lbCb_impboll = New NTSInformatica.NTSLabel
    Me.edCb_impboll = New NTSInformatica.NTSTextBoxNum
    Me.edCb_causale = New NTSInformatica.NTSTextBoxNum
    Me.fmSpese = New NTSInformatica.NTSGroupBox
    Me.lbCb_spesaop = New NTSInformatica.NTSLabel
    Me.ckCb_addspop = New NTSInformatica.NTSCheckBox
    Me.edCb_spesaop = New NTSInformatica.NTSTextBoxNum
    Me.edCb_codbanc = New NTSInformatica.NTSTextBoxNum
    Me.fmValuta = New NTSInformatica.NTSGroupBox
    Me.cbCb_valfiss = New NTSInformatica.NTSComboBox
    Me.cbCb_avaind = New NTSInformatica.NTSComboBox
    Me.edCb_ggvalu = New NTSInformatica.NTSTextBoxNum
    Me.lbCb_valfiss = New NTSInformatica.NTSLabel
    Me.lbCb_avaind = New NTSInformatica.NTSLabel
    Me.lbCb_ggvalu = New NTSInformatica.NTSLabel
    Me.lbCb_causale = New NTSInformatica.NTSLabel
    Me.lbCb_codbanc = New NTSInformatica.NTSLabel
    Me.lbCb_causaleLabel = New NTSInformatica.NTSLabel
    Me.lbCb_codbancLabel = New NTSInformatica.NTSLabel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnCoba, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCoba.SuspendLayout()
    CType(Me.edCb_impboll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCb_causale.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmSpese, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmSpese.SuspendLayout()
    CType(Me.ckCb_addspop.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCb_spesaop.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCb_codbanc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmValuta, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmValuta.SuspendLayout()
    CType(Me.cbCb_valfiss.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbCb_avaind.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCb_ggvalu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbRipristina, Me.tlbCancella, Me.tlbZoom, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbUltimo, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 11
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbUltimo.Id = 8
    Me.tlbUltimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 9
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 10
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnCoba
    '
    Me.pnCoba.AllowDrop = True
    Me.pnCoba.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCoba.Appearance.Options.UseBackColor = True
    Me.pnCoba.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCoba.Controls.Add(Me.lbCb_impboll)
    Me.pnCoba.Controls.Add(Me.edCb_impboll)
    Me.pnCoba.Controls.Add(Me.edCb_causale)
    Me.pnCoba.Controls.Add(Me.fmSpese)
    Me.pnCoba.Controls.Add(Me.edCb_codbanc)
    Me.pnCoba.Controls.Add(Me.fmValuta)
    Me.pnCoba.Controls.Add(Me.lbCb_causale)
    Me.pnCoba.Controls.Add(Me.lbCb_codbanc)
    Me.pnCoba.Controls.Add(Me.lbCb_causaleLabel)
    Me.pnCoba.Controls.Add(Me.lbCb_codbancLabel)
    Me.pnCoba.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCoba.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnCoba.Location = New System.Drawing.Point(0, 30)
    Me.pnCoba.Name = "pnCoba"
    Me.pnCoba.NTSActiveTrasparency = True
    Me.pnCoba.Size = New System.Drawing.Size(403, 236)
    Me.pnCoba.TabIndex = 4
    Me.pnCoba.Text = "NtsPanel1"
    '
    'lbCb_impboll
    '
    Me.lbCb_impboll.AutoSize = True
    Me.lbCb_impboll.BackColor = System.Drawing.Color.Transparent
    Me.lbCb_impboll.Location = New System.Drawing.Point(196, 189)
    Me.lbCb_impboll.Name = "lbCb_impboll"
    Me.lbCb_impboll.NTSDbField = ""
    Me.lbCb_impboll.Size = New System.Drawing.Size(74, 13)
    Me.lbCb_impboll.TabIndex = 6
    Me.lbCb_impboll.Text = "Importo bollo:"
    Me.lbCb_impboll.Tooltip = ""
    Me.lbCb_impboll.UseMnemonic = False
    '
    'edCb_impboll
    '
    Me.edCb_impboll.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCb_impboll.Location = New System.Drawing.Point(275, 186)
    Me.edCb_impboll.Name = "edCb_impboll"
    Me.edCb_impboll.NTSDbField = ""
    Me.edCb_impboll.NTSFormat = "0"
    Me.edCb_impboll.NTSForzaVisZoom = False
    Me.edCb_impboll.NTSOldValue = ""
    Me.edCb_impboll.Properties.Appearance.Options.UseTextOptions = True
    Me.edCb_impboll.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCb_impboll.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCb_impboll.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCb_impboll.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCb_impboll.Properties.MaxLength = 65536
    Me.edCb_impboll.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCb_impboll.Size = New System.Drawing.Size(111, 20)
    Me.edCb_impboll.TabIndex = 1
    '
    'edCb_causale
    '
    Me.edCb_causale.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edCb_causale.Location = New System.Drawing.Point(84, 39)
    Me.edCb_causale.Name = "edCb_causale"
    Me.edCb_causale.NTSDbField = ""
    Me.edCb_causale.NTSFormat = "0"
    Me.edCb_causale.NTSForzaVisZoom = False
    Me.edCb_causale.NTSOldValue = ""
    Me.edCb_causale.Properties.Appearance.Options.UseTextOptions = True
    Me.edCb_causale.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCb_causale.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCb_causale.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCb_causale.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCb_causale.Properties.MaxLength = 65536
    Me.edCb_causale.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCb_causale.Size = New System.Drawing.Size(66, 20)
    Me.edCb_causale.TabIndex = 4
    '
    'fmSpese
    '
    Me.fmSpese.AllowDrop = True
    Me.fmSpese.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmSpese.Appearance.Options.UseBackColor = True
    Me.fmSpese.Controls.Add(Me.lbCb_spesaop)
    Me.fmSpese.Controls.Add(Me.ckCb_addspop)
    Me.fmSpese.Controls.Add(Me.edCb_spesaop)
    Me.fmSpese.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmSpese.Location = New System.Drawing.Point(194, 79)
    Me.fmSpese.Name = "fmSpese"
    Me.fmSpese.Size = New System.Drawing.Size(197, 89)
    Me.fmSpese.TabIndex = 5
    Me.fmSpese.Text = "Spese operazione"
    '
    'lbCb_spesaop
    '
    Me.lbCb_spesaop.AutoSize = True
    Me.lbCb_spesaop.BackColor = System.Drawing.Color.Transparent
    Me.lbCb_spesaop.Location = New System.Drawing.Point(10, 61)
    Me.lbCb_spesaop.Name = "lbCb_spesaop"
    Me.lbCb_spesaop.NTSDbField = ""
    Me.lbCb_spesaop.Size = New System.Drawing.Size(65, 13)
    Me.lbCb_spesaop.TabIndex = 3
    Me.lbCb_spesaop.Text = "Spesa agg.:"
    Me.lbCb_spesaop.Tooltip = ""
    Me.lbCb_spesaop.UseMnemonic = False
    '
    'ckCb_addspop
    '
    Me.ckCb_addspop.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckCb_addspop.Location = New System.Drawing.Point(5, 29)
    Me.ckCb_addspop.Name = "ckCb_addspop"
    Me.ckCb_addspop.NTSCheckValue = "S"
    Me.ckCb_addspop.NTSUnCheckValue = "N"
    Me.ckCb_addspop.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckCb_addspop.Properties.Appearance.Options.UseBackColor = True
    Me.ckCb_addspop.Properties.Caption = "Addebito spesa operazione"
    Me.ckCb_addspop.Size = New System.Drawing.Size(171, 19)
    Me.ckCb_addspop.TabIndex = 1
    '
    'edCb_spesaop
    '
    Me.edCb_spesaop.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCb_spesaop.Location = New System.Drawing.Point(81, 58)
    Me.edCb_spesaop.Name = "edCb_spesaop"
    Me.edCb_spesaop.NTSDbField = ""
    Me.edCb_spesaop.NTSFormat = "0"
    Me.edCb_spesaop.NTSForzaVisZoom = False
    Me.edCb_spesaop.NTSOldValue = ""
    Me.edCb_spesaop.Properties.Appearance.Options.UseTextOptions = True
    Me.edCb_spesaop.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCb_spesaop.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCb_spesaop.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCb_spesaop.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCb_spesaop.Properties.MaxLength = 65536
    Me.edCb_spesaop.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCb_spesaop.Size = New System.Drawing.Size(111, 20)
    Me.edCb_spesaop.TabIndex = 0
    '
    'edCb_codbanc
    '
    Me.edCb_codbanc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCb_codbanc.Location = New System.Drawing.Point(84, 13)
    Me.edCb_codbanc.Name = "edCb_codbanc"
    Me.edCb_codbanc.NTSDbField = ""
    Me.edCb_codbanc.NTSFormat = "0"
    Me.edCb_codbanc.NTSForzaVisZoom = False
    Me.edCb_codbanc.NTSOldValue = ""
    Me.edCb_codbanc.Properties.Appearance.Options.UseTextOptions = True
    Me.edCb_codbanc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCb_codbanc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCb_codbanc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCb_codbanc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCb_codbanc.Properties.MaxLength = 65536
    Me.edCb_codbanc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCb_codbanc.Size = New System.Drawing.Size(66, 20)
    Me.edCb_codbanc.TabIndex = 3
    '
    'fmValuta
    '
    Me.fmValuta.AllowDrop = True
    Me.fmValuta.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmValuta.Appearance.Options.UseBackColor = True
    Me.fmValuta.Controls.Add(Me.cbCb_valfiss)
    Me.fmValuta.Controls.Add(Me.cbCb_avaind)
    Me.fmValuta.Controls.Add(Me.edCb_ggvalu)
    Me.fmValuta.Controls.Add(Me.lbCb_valfiss)
    Me.fmValuta.Controls.Add(Me.lbCb_avaind)
    Me.fmValuta.Controls.Add(Me.lbCb_ggvalu)
    Me.fmValuta.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmValuta.Location = New System.Drawing.Point(12, 79)
    Me.fmValuta.Name = "fmValuta"
    Me.fmValuta.Size = New System.Drawing.Size(176, 150)
    Me.fmValuta.TabIndex = 4
    Me.fmValuta.Text = "Valuta"
    '
    'cbCb_valfiss
    '
    Me.cbCb_valfiss.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbCb_valfiss.DataSource = Nothing
    Me.cbCb_valfiss.DisplayMember = ""
    Me.cbCb_valfiss.Location = New System.Drawing.Point(8, 107)
    Me.cbCb_valfiss.Name = "cbCb_valfiss"
    Me.cbCb_valfiss.NTSDbField = ""
    Me.cbCb_valfiss.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbCb_valfiss.Properties.DropDownRows = 30
    Me.cbCb_valfiss.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbCb_valfiss.SelectedValue = ""
    Me.cbCb_valfiss.Size = New System.Drawing.Size(163, 20)
    Me.cbCb_valfiss.TabIndex = 5
    Me.cbCb_valfiss.ValueMember = ""
    '
    'cbCb_avaind
    '
    Me.cbCb_avaind.Cursor = System.Windows.Forms.Cursors.Hand
    Me.cbCb_avaind.DataSource = Nothing
    Me.cbCb_avaind.DisplayMember = ""
    Me.cbCb_avaind.Location = New System.Drawing.Point(82, 58)
    Me.cbCb_avaind.Name = "cbCb_avaind"
    Me.cbCb_avaind.NTSDbField = ""
    Me.cbCb_avaind.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbCb_avaind.Properties.DropDownRows = 30
    Me.cbCb_avaind.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbCb_avaind.SelectedValue = ""
    Me.cbCb_avaind.Size = New System.Drawing.Size(89, 20)
    Me.cbCb_avaind.TabIndex = 4
    Me.cbCb_avaind.ValueMember = ""
    '
    'edCb_ggvalu
    '
    Me.edCb_ggvalu.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCb_ggvalu.Location = New System.Drawing.Point(82, 28)
    Me.edCb_ggvalu.Name = "edCb_ggvalu"
    Me.edCb_ggvalu.NTSDbField = ""
    Me.edCb_ggvalu.NTSFormat = "0"
    Me.edCb_ggvalu.NTSForzaVisZoom = False
    Me.edCb_ggvalu.NTSOldValue = ""
    Me.edCb_ggvalu.Properties.Appearance.Options.UseTextOptions = True
    Me.edCb_ggvalu.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCb_ggvalu.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCb_ggvalu.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCb_ggvalu.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCb_ggvalu.Properties.MaxLength = 65536
    Me.edCb_ggvalu.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCb_ggvalu.Size = New System.Drawing.Size(89, 20)
    Me.edCb_ggvalu.TabIndex = 3
    '
    'lbCb_valfiss
    '
    Me.lbCb_valfiss.AutoSize = True
    Me.lbCb_valfiss.BackColor = System.Drawing.Color.Transparent
    Me.lbCb_valfiss.Location = New System.Drawing.Point(5, 86)
    Me.lbCb_valfiss.Name = "lbCb_valfiss"
    Me.lbCb_valfiss.NTSDbField = ""
    Me.lbCb_valfiss.Size = New System.Drawing.Size(130, 13)
    Me.lbCb_valfiss.TabIndex = 2
    Me.lbCb_valfiss.Text = "Giorni esclusi dalla valuta:"
    Me.lbCb_valfiss.Tooltip = ""
    Me.lbCb_valfiss.UseMnemonic = False
    '
    'lbCb_avaind
    '
    Me.lbCb_avaind.AutoSize = True
    Me.lbCb_avaind.BackColor = System.Drawing.Color.Transparent
    Me.lbCb_avaind.Location = New System.Drawing.Point(5, 61)
    Me.lbCb_avaind.Name = "lbCb_avaind"
    Me.lbCb_avaind.NTSDbField = ""
    Me.lbCb_avaind.Size = New System.Drawing.Size(71, 13)
    Me.lbCb_avaind.TabIndex = 1
    Me.lbCb_avaind.Text = "Cont. valuta:"
    Me.lbCb_avaind.Tooltip = ""
    Me.lbCb_avaind.UseMnemonic = False
    '
    'lbCb_ggvalu
    '
    Me.lbCb_ggvalu.AutoSize = True
    Me.lbCb_ggvalu.BackColor = System.Drawing.Color.Transparent
    Me.lbCb_ggvalu.Location = New System.Drawing.Point(5, 31)
    Me.lbCb_ggvalu.Name = "lbCb_ggvalu"
    Me.lbCb_ggvalu.NTSDbField = ""
    Me.lbCb_ggvalu.Size = New System.Drawing.Size(71, 13)
    Me.lbCb_ggvalu.TabIndex = 0
    Me.lbCb_ggvalu.Text = "Giorni valuta:"
    Me.lbCb_ggvalu.Tooltip = ""
    Me.lbCb_ggvalu.UseMnemonic = False
    '
    'lbCb_causale
    '
    Me.lbCb_causale.BackColor = System.Drawing.Color.Transparent
    Me.lbCb_causale.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbCb_causale.Location = New System.Drawing.Point(167, 39)
    Me.lbCb_causale.Name = "lbCb_causale"
    Me.lbCb_causale.NTSDbField = ""
    Me.lbCb_causale.Size = New System.Drawing.Size(225, 20)
    Me.lbCb_causale.TabIndex = 3
    Me.lbCb_causale.Tooltip = ""
    Me.lbCb_causale.UseMnemonic = False
    '
    'lbCb_codbanc
    '
    Me.lbCb_codbanc.BackColor = System.Drawing.Color.Transparent
    Me.lbCb_codbanc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbCb_codbanc.Location = New System.Drawing.Point(167, 15)
    Me.lbCb_codbanc.Name = "lbCb_codbanc"
    Me.lbCb_codbanc.NTSDbField = ""
    Me.lbCb_codbanc.Size = New System.Drawing.Size(225, 20)
    Me.lbCb_codbanc.TabIndex = 2
    Me.lbCb_codbanc.Tooltip = ""
    Me.lbCb_codbanc.UseMnemonic = False
    '
    'lbCb_causaleLabel
    '
    Me.lbCb_causaleLabel.AutoSize = True
    Me.lbCb_causaleLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbCb_causaleLabel.Location = New System.Drawing.Point(12, 41)
    Me.lbCb_causaleLabel.Name = "lbCb_causaleLabel"
    Me.lbCb_causaleLabel.NTSDbField = ""
    Me.lbCb_causaleLabel.Size = New System.Drawing.Size(49, 13)
    Me.lbCb_causaleLabel.TabIndex = 1
    Me.lbCb_causaleLabel.Text = "Causale:"
    Me.lbCb_causaleLabel.Tooltip = ""
    Me.lbCb_causaleLabel.UseMnemonic = False
    '
    'lbCb_codbancLabel
    '
    Me.lbCb_codbancLabel.AutoSize = True
    Me.lbCb_codbancLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbCb_codbancLabel.Location = New System.Drawing.Point(12, 16)
    Me.lbCb_codbancLabel.Name = "lbCb_codbancLabel"
    Me.lbCb_codbancLabel.NTSDbField = ""
    Me.lbCb_codbancLabel.Size = New System.Drawing.Size(66, 13)
    Me.lbCb_codbancLabel.TabIndex = 0
    Me.lbCb_codbancLabel.Text = "Cod. banca:"
    Me.lbCb_codbancLabel.Tooltip = ""
    Me.lbCb_codbancLabel.UseMnemonic = False
    '
    'FRMTECOBA
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(403, 266)
    Me.Controls.Add(Me.pnCoba)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.HelpContext = "bstecoba.htm"
    Me.Name = "FRMTECOBA"
    Me.Text = "CONDIZIONI BANCARIE PER TIPO DI OPERAZIONE"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnCoba, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCoba.ResumeLayout(False)
    Me.pnCoba.PerformLayout()
    CType(Me.edCb_impboll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCb_causale.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmSpese, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmSpese.ResumeLayout(False)
    Me.fmSpese.PerformLayout()
    CType(Me.ckCb_addspop.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCb_spesaop.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCb_codbanc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmValuta, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmValuta.ResumeLayout(False)
    Me.fmValuta.PerformLayout()
    CType(Me.cbCb_valfiss.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbCb_avaind.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCb_ggvalu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
  Public Overridable Sub InitEntity(ByRef cleBanc As CLEVEBANC)
    oCleBanc = cleBanc
    AddHandler oCleBanc.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttAvaind As New DataTable()
    Dim dttValfiss As New DataTable()
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
        '  'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      dttAvaind.Columns.Add("cod", GetType(String))
      dttAvaind.Columns.Add("val", GetType(String))
      dttAvaind.Rows.Add(New Object() {"-", "Indietro"})
      dttAvaind.Rows.Add(New Object() {"+", "Avanti"})
      dttAvaind.AcceptChanges()

      cbCb_avaind.DataSource = dttAvaind
      cbCb_avaind.ValueMember = "cod"
      cbCb_avaind.DisplayMember = "val"

      cbCb_avaind.SelectedIndex = 1

      dttValfiss.Columns.Add("cod", GetType(String))
      dttValfiss.Columns.Add("val", GetType(String))
      dttValfiss.Rows.Add(New Object() {"D", "Festivi"})
      dttValfiss.Rows.Add(New Object() {"N", "Non lavorativi"})
      dttValfiss.Rows.Add(New Object() {"S", "Nessuno"})
      dttValfiss.AcceptChanges()

      cbCb_valfiss.DataSource = dttValfiss
      cbCb_valfiss.ValueMember = "cod"
      cbCb_valfiss.DisplayMember = "val"

      cbCb_valfiss.SelectedIndex = 2

      edCb_impboll.NTSSetParam(oMenu, oApp.Tr(Me, 128400411738668092, "Importo bollo"), "0", 9, 0, 999999999)
      edCb_causale.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128400411738824310, "Causale"), tabcauc)
      ckCb_addspop.NTSSetParam(oMenu, oApp.Tr(Me, 128400411739136746, "Addebito spesa op."), "S", "N")
      edCb_spesaop.NTSSetParam(oMenu, oApp.Tr(Me, 128400411739292964, "Spesa agg."), oApp.FormatImporti, 8, 0, 99999999)
      edCb_codbanc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128777131697625283, "Cod. banca"), tabbanc)
      cbCb_valfiss.NTSSetParam(oApp.Tr(Me, 128400411739449182, "Giorni esclusi dalla valuta"))
      cbCb_avaind.NTSSetParam(oApp.Tr(Me, 128400411739605400, "Cont. valuta"))
      edCb_ggvalu.NTSSetParam(oMenu, oApp.Tr(Me, 128400411739761618, "Giorni valuta"), "0", 4, 0, 9999)

      edCb_codbanc.Enabled = False

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
  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano già  stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      edCb_impboll.NTSDbField = "condban.cb_impboll"
      edCb_causale.NTSDbField = "condban.cb_causale"
      ckCb_addspop.NTSText.NTSDbField = "condban.cb_addspop"
      edCb_spesaop.NTSDbField = "condban.cb_spesaop"
      edCb_codbanc.NTSDbField = "condban.cb_codbanc"
      cbCb_valfiss.NTSDbField = "condban.cb_valfiss"
      cbCb_avaind.NTSDbField = "condban.cb_avaind"
      edCb_ggvalu.NTSDbField = "condban.cb_ggvalu"
      lbCb_causale.NTSDbField = "condban.xx_descauc"
      lbCb_codbanc.NTSDbField = "condban.xx_desbanc"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcCoba, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMTECOBA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      Apri()

      '--------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
      GctlApplicaDefaultValue()

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

#Region "EventiToolbar"

  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      '-------------------------------------------------
      'creo una nuova condizione bancaria
      If Not Salva() Then Return
      oCleBanc.CobaNuovo()
      dcCoba.MoveLast()
      edCb_causale.Enabled = True
      edCb_causale.Focus()

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
      'cancello la condizione bancaria
      Dim dlgRes As DialogResult
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128256093191691883, "Cancellare la condizione bancaria?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes

          If dsCoba.Tables("CONDBAN").Rows.Count = 1 Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          dcCoba.RemoveAt(dcCoba.Position)
          oCleBanc.CobaSalva(True)

          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcCoba, Me)
            bRemovBinding = False
            edCb_causale.Enabled = True
          Else
            edCb_causale.Enabled = False
          End If

          Return
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcCoba, Me)
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
      'Ripristino il condizione bancaria
      Dim dlgRes As DialogResult
      If Not sender Is Nothing Then
        'chiamato facendo pressione sulla funzione 'ripristina'
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128257699390236031, "Ripristinare le modifiche apportate alla condizione bancaria?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          If dsCoba.Tables("CONDBAN").Rows.Count = 1 And dsCoba.Tables("CONDBAN").Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          oCleBanc.CobaRipristina(dcCoba.Position, dcCoba.Filter)
          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcCoba, Me)
            bRemovBinding = False
            edCb_causale.Enabled = True
          Else
            edCb_causale.Enabled = False
          End If
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcCoba, Me)
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

      If edCb_causale.Focused Then
        '--------------------------------------------
        'Zoom TABCAUC
        SetFastZoom(edCb_causale.Text, oParam)    'abilito la gestione dello zoom veloce
        oParam.strCodPdc = "*"                  'non filtro per codice PDC
        oParam.nEscomp = -1                     'non filtro il tipo di contabilità
        NTSZOOM.strIn = edCb_causale.Text
        NTSZOOM.ZoomStrIn("ZOOMTABCAUC", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edCb_causale.Text Then
          edCb_causale.NTSTextDB = NTSZOOM.strIn
        End If
      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        NTSCallStandardZoom()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    Try
      '-------------------------------------------------
      'vado sul primo record
      If Not Salva() Then Return
      dcCoba.MoveFirst()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbPrecedente_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrecedente.ItemClick
    Try
      '-------------------------------------------------
      'vado sul record precedente
      If Not Salva() Then Return
      dcCoba.MovePrevious()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbSuccessivo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSuccessivo.ItemClick
    Try
      '-------------------------------------------------
      'vado sul record successivo
      If Not Salva() Then Return
      dcCoba.MoveNext()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbUltimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbUltimo.ItemClick
    Try
      '-------------------------------------------------
      'vado sull'ultimo record
      If Not Salva() Then Return
      dcCoba.MoveLast()
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
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

  Public Overridable Function Apri() As Boolean
    Try

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      NTSFormClearDataBinding(Me)
      If Not oCleBanc.CobaApri(dsCoba) Then Me.Close()
      dcCoba.DataSource = dsCoba.Tables("CONDBAN")
      dsCoba.AcceptChanges()
      If dsCoba.Tables("CONDBAN").Rows.Count > 0 Then
        edCb_causale.Enabled = False
      End If

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      'e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipodocumento, ecc ...
      'GctlTipoDoc = ""
      GctlSetRoules()

      If dsCoba.Tables("CONDBAN").Rows.Count = 0 Then tlbNuovo_ItemClick(tlbNuovo, Nothing)
      dcCoba.ResetBindings(False)
      dcCoba.MoveFirst()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function Salva() As Boolean
    Dim dRes As DialogResult

    Try
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo

      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      If oCleBanc.CobaRecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128306257061636669, "Salvo la condizione bancaria?"))
        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oCleBanc.CobaSalva(False) Then Return False
          If dsCoba.Tables("CONDBAN").Rows.Count > 0 Then
            edCb_causale.Enabled = False
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

End Class