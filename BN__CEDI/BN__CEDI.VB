Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__CEDI
  Private Moduli_P As Integer = CLN__STD.bsModAll
  Private ModuliExt_P As Integer = CLN__STD.bsModExtAll
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

#Region "Variabili"
  Public dsCedi As DataSet
  Public oCleCedi As CLE__CEDI
  Public oCallParams As CLE__CLDP
  Public dcCedi As BindingSource = New BindingSource

  Public strMsg As String


  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents pnCedi As NTSInformatica.NTSPanel
  Public WithEvents cmdElimina As NTSInformatica.NTSButton
  Public WithEvents cmdImporta As NTSInformatica.NTSButton
  Public WithEvents lbBar As NTSInformatica.NTSLabel
  Public WithEvents edTb_azpiva As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_azcodf As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_azprov As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_azcap As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_azcitta As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_azindir As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_azrags2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_azrags1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edCodditt As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_azprov As NTSInformatica.NTSLabel
  Public WithEvents lbTb_azpiva As NTSInformatica.NTSLabel
  Public WithEvents lbTb_azcodf As NTSInformatica.NTSLabel
  Public WithEvents lbTb_azcap As NTSInformatica.NTSLabel
  Public WithEvents lbTb_azcitta As NTSInformatica.NTSLabel
  Public WithEvents lbTb_azindir As NTSInformatica.NTSLabel
  Public WithEvents lbTb_azrags2 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_azrags1 As NTSInformatica.NTSLabel
  Public WithEvents lbCodditt As NTSInformatica.NTSLabel

  Private components As System.ComponentModel.IContainer
#End Region

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__CEDI))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
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
    Me.pnCedi = New NTSInformatica.NTSPanel
    Me.lbBar = New NTSInformatica.NTSLabel
    Me.edTb_azpiva = New NTSInformatica.NTSTextBoxStr
    Me.edTb_azcodf = New NTSInformatica.NTSTextBoxStr
    Me.edTb_azprov = New NTSInformatica.NTSTextBoxStr
    Me.edTb_azcap = New NTSInformatica.NTSTextBoxStr
    Me.edTb_azcitta = New NTSInformatica.NTSTextBoxStr
    Me.edTb_azindir = New NTSInformatica.NTSTextBoxStr
    Me.edTb_azrags2 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_azrags1 = New NTSInformatica.NTSTextBoxStr
    Me.edCodditt = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_azprov = New NTSInformatica.NTSLabel
    Me.lbTb_azpiva = New NTSInformatica.NTSLabel
    Me.lbTb_azcodf = New NTSInformatica.NTSLabel
    Me.lbTb_azcap = New NTSInformatica.NTSLabel
    Me.lbTb_azcitta = New NTSInformatica.NTSLabel
    Me.lbTb_azindir = New NTSInformatica.NTSLabel
    Me.lbTb_azrags2 = New NTSInformatica.NTSLabel
    Me.lbTb_azrags1 = New NTSInformatica.NTSLabel
    Me.lbCodditt = New NTSInformatica.NTSLabel
    Me.cmdElimina = New NTSInformatica.NTSButton
    Me.cmdImporta = New NTSInformatica.NTSButton
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnCedi, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCedi.SuspendLayout()
    CType(Me.edTb_azpiva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azcodf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azprov.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azcap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azcitta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azindir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azrags2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_azrags1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodditt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbZoom, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbUltimo, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 11
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 0
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbPrimo
    '
    Me.tlbPrimo.Caption = "Primo"
    Me.tlbPrimo.Glyph = CType(resources.GetObject("tlbPrimo.Glyph"), System.Drawing.Image)
    Me.tlbPrimo.Id = 1
    Me.tlbPrimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P))
    Me.tlbPrimo.Name = "tlbPrimo"
    Me.tlbPrimo.Visible = True
    '
    'tlbPrecedente
    '
    Me.tlbPrecedente.Caption = "Precedente"
    Me.tlbPrecedente.Glyph = CType(resources.GetObject("tlbPrecedente.Glyph"), System.Drawing.Image)
    Me.tlbPrecedente.Id = 2
    Me.tlbPrecedente.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
    Me.tlbPrecedente.Name = "tlbPrecedente"
    Me.tlbPrecedente.Visible = True
    '
    'tlbSuccessivo
    '
    Me.tlbSuccessivo.Caption = "Successivo"
    Me.tlbSuccessivo.Glyph = CType(resources.GetObject("tlbSuccessivo.Glyph"), System.Drawing.Image)
    Me.tlbSuccessivo.Id = 3
    Me.tlbSuccessivo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
    Me.tlbSuccessivo.Name = "tlbSuccessivo"
    Me.tlbSuccessivo.Visible = True
    '
    'tlbUltimo
    '
    Me.tlbUltimo.Caption = "Ultimo"
    Me.tlbUltimo.Glyph = CType(resources.GetObject("tlbUltimo.Glyph"), System.Drawing.Image)
    Me.tlbUltimo.Id = 4
    Me.tlbUltimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 6
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 7
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnCedi
    '
    Me.pnCedi.AllowDrop = True
    Me.pnCedi.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCedi.Appearance.Options.UseBackColor = True
    Me.pnCedi.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCedi.Controls.Add(Me.lbBar)
    Me.pnCedi.Controls.Add(Me.edTb_azpiva)
    Me.pnCedi.Controls.Add(Me.edTb_azcodf)
    Me.pnCedi.Controls.Add(Me.edTb_azprov)
    Me.pnCedi.Controls.Add(Me.edTb_azcap)
    Me.pnCedi.Controls.Add(Me.edTb_azcitta)
    Me.pnCedi.Controls.Add(Me.edTb_azindir)
    Me.pnCedi.Controls.Add(Me.edTb_azrags2)
    Me.pnCedi.Controls.Add(Me.edTb_azrags1)
    Me.pnCedi.Controls.Add(Me.edCodditt)
    Me.pnCedi.Controls.Add(Me.lbTb_azprov)
    Me.pnCedi.Controls.Add(Me.lbTb_azpiva)
    Me.pnCedi.Controls.Add(Me.lbTb_azcodf)
    Me.pnCedi.Controls.Add(Me.lbTb_azcap)
    Me.pnCedi.Controls.Add(Me.lbTb_azcitta)
    Me.pnCedi.Controls.Add(Me.lbTb_azindir)
    Me.pnCedi.Controls.Add(Me.lbTb_azrags2)
    Me.pnCedi.Controls.Add(Me.lbTb_azrags1)
    Me.pnCedi.Controls.Add(Me.lbCodditt)
    Me.pnCedi.Controls.Add(Me.cmdElimina)
    Me.pnCedi.Controls.Add(Me.cmdImporta)
    Me.pnCedi.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCedi.Location = New System.Drawing.Point(0, 30)
    Me.pnCedi.Name = "pnCedi"
    Me.pnCedi.Size = New System.Drawing.Size(629, 250)
    Me.pnCedi.TabIndex = 6
    Me.pnCedi.Text = "NtsPanel1"
    '
    'lbBar
    '
    Me.lbBar.BackColor = System.Drawing.Color.Transparent
    Me.lbBar.Location = New System.Drawing.Point(12, 226)
    Me.lbBar.Name = "lbBar"
    Me.lbBar.NTSDbField = ""
    Me.lbBar.Size = New System.Drawing.Size(437, 13)
    Me.lbBar.TabIndex = 26
    Me.lbBar.UseMnemonic = False
    '
    'edTb_azpiva
    '
    Me.edTb_azpiva.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azpiva.Location = New System.Drawing.Point(117, 197)
    Me.edTb_azpiva.Name = "edTb_azpiva"
    Me.edTb_azpiva.NTSDbField = ""
    Me.edTb_azpiva.NTSForzaVisZoom = False
    Me.edTb_azpiva.NTSOldValue = ""
    Me.edTb_azpiva.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azpiva.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azpiva.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azpiva.Properties.MaxLength = 65536
    Me.edTb_azpiva.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azpiva.Size = New System.Drawing.Size(332, 20)
    Me.edTb_azpiva.TabIndex = 25
    '
    'edTb_azcodf
    '
    Me.edTb_azcodf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azcodf.Location = New System.Drawing.Point(117, 171)
    Me.edTb_azcodf.Name = "edTb_azcodf"
    Me.edTb_azcodf.NTSDbField = ""
    Me.edTb_azcodf.NTSForzaVisZoom = False
    Me.edTb_azcodf.NTSOldValue = ""
    Me.edTb_azcodf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azcodf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azcodf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azcodf.Properties.MaxLength = 65536
    Me.edTb_azcodf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azcodf.Size = New System.Drawing.Size(332, 20)
    Me.edTb_azcodf.TabIndex = 24
    '
    'edTb_azprov
    '
    Me.edTb_azprov.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azprov.Location = New System.Drawing.Point(371, 145)
    Me.edTb_azprov.Name = "edTb_azprov"
    Me.edTb_azprov.NTSDbField = ""
    Me.edTb_azprov.NTSForzaVisZoom = False
    Me.edTb_azprov.NTSOldValue = ""
    Me.edTb_azprov.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azprov.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azprov.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azprov.Properties.MaxLength = 65536
    Me.edTb_azprov.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azprov.Size = New System.Drawing.Size(78, 20)
    Me.edTb_azprov.TabIndex = 23
    '
    'edTb_azcap
    '
    Me.edTb_azcap.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azcap.Location = New System.Drawing.Point(117, 145)
    Me.edTb_azcap.Name = "edTb_azcap"
    Me.edTb_azcap.NTSDbField = ""
    Me.edTb_azcap.NTSForzaVisZoom = False
    Me.edTb_azcap.NTSOldValue = ""
    Me.edTb_azcap.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azcap.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azcap.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azcap.Properties.MaxLength = 65536
    Me.edTb_azcap.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azcap.Size = New System.Drawing.Size(121, 20)
    Me.edTb_azcap.TabIndex = 22
    '
    'edTb_azcitta
    '
    Me.edTb_azcitta.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azcitta.Location = New System.Drawing.Point(117, 118)
    Me.edTb_azcitta.Name = "edTb_azcitta"
    Me.edTb_azcitta.NTSDbField = ""
    Me.edTb_azcitta.NTSForzaVisZoom = False
    Me.edTb_azcitta.NTSOldValue = ""
    Me.edTb_azcitta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azcitta.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azcitta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azcitta.Properties.MaxLength = 65536
    Me.edTb_azcitta.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azcitta.Size = New System.Drawing.Size(332, 20)
    Me.edTb_azcitta.TabIndex = 21
    '
    'edTb_azindir
    '
    Me.edTb_azindir.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azindir.Location = New System.Drawing.Point(117, 92)
    Me.edTb_azindir.Name = "edTb_azindir"
    Me.edTb_azindir.NTSDbField = ""
    Me.edTb_azindir.NTSForzaVisZoom = False
    Me.edTb_azindir.NTSOldValue = ""
    Me.edTb_azindir.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azindir.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azindir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azindir.Properties.MaxLength = 65536
    Me.edTb_azindir.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azindir.Size = New System.Drawing.Size(332, 20)
    Me.edTb_azindir.TabIndex = 20
    '
    'edTb_azrags2
    '
    Me.edTb_azrags2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azrags2.Location = New System.Drawing.Point(117, 66)
    Me.edTb_azrags2.Name = "edTb_azrags2"
    Me.edTb_azrags2.NTSDbField = ""
    Me.edTb_azrags2.NTSForzaVisZoom = False
    Me.edTb_azrags2.NTSOldValue = ""
    Me.edTb_azrags2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azrags2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azrags2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azrags2.Properties.MaxLength = 65536
    Me.edTb_azrags2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azrags2.Size = New System.Drawing.Size(332, 20)
    Me.edTb_azrags2.TabIndex = 19
    '
    'edTb_azrags1
    '
    Me.edTb_azrags1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_azrags1.Location = New System.Drawing.Point(117, 40)
    Me.edTb_azrags1.Name = "edTb_azrags1"
    Me.edTb_azrags1.NTSDbField = ""
    Me.edTb_azrags1.NTSForzaVisZoom = False
    Me.edTb_azrags1.NTSOldValue = ""
    Me.edTb_azrags1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_azrags1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_azrags1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_azrags1.Properties.MaxLength = 65536
    Me.edTb_azrags1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_azrags1.Size = New System.Drawing.Size(332, 20)
    Me.edTb_azrags1.TabIndex = 18
    '
    'edCodditt
    '
    Me.edCodditt.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodditt.Location = New System.Drawing.Point(117, 10)
    Me.edCodditt.Name = "edCodditt"
    Me.edCodditt.NTSDbField = ""
    Me.edCodditt.NTSForzaVisZoom = False
    Me.edCodditt.NTSOldValue = ""
    Me.edCodditt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodditt.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodditt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodditt.Properties.MaxLength = 65536
    Me.edCodditt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodditt.Size = New System.Drawing.Size(121, 20)
    Me.edCodditt.TabIndex = 17
    '
    'lbTb_azprov
    '
    Me.lbTb_azprov.AutoSize = True
    Me.lbTb_azprov.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azprov.Location = New System.Drawing.Point(315, 148)
    Me.lbTb_azprov.Name = "lbTb_azprov"
    Me.lbTb_azprov.NTSDbField = ""
    Me.lbTb_azprov.Size = New System.Drawing.Size(50, 13)
    Me.lbTb_azprov.TabIndex = 16
    Me.lbTb_azprov.Text = "Provincia"
    Me.lbTb_azprov.UseMnemonic = False
    '
    'lbTb_azpiva
    '
    Me.lbTb_azpiva.AutoSize = True
    Me.lbTb_azpiva.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azpiva.Location = New System.Drawing.Point(12, 200)
    Me.lbTb_azpiva.Name = "lbTb_azpiva"
    Me.lbTb_azpiva.NTSDbField = ""
    Me.lbTb_azpiva.Size = New System.Drawing.Size(58, 13)
    Me.lbTb_azpiva.TabIndex = 15
    Me.lbTb_azpiva.Text = "Partita Iva"
    Me.lbTb_azpiva.UseMnemonic = False
    '
    'lbTb_azcodf
    '
    Me.lbTb_azcodf.AutoSize = True
    Me.lbTb_azcodf.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azcodf.Location = New System.Drawing.Point(12, 174)
    Me.lbTb_azcodf.Name = "lbTb_azcodf"
    Me.lbTb_azcodf.NTSDbField = ""
    Me.lbTb_azcodf.Size = New System.Drawing.Size(74, 13)
    Me.lbTb_azcodf.TabIndex = 14
    Me.lbTb_azcodf.Text = "Codice Fiscale"
    Me.lbTb_azcodf.UseMnemonic = False
    '
    'lbTb_azcap
    '
    Me.lbTb_azcap.AutoSize = True
    Me.lbTb_azcap.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azcap.Location = New System.Drawing.Point(12, 148)
    Me.lbTb_azcap.Name = "lbTb_azcap"
    Me.lbTb_azcap.NTSDbField = ""
    Me.lbTb_azcap.Size = New System.Drawing.Size(39, 13)
    Me.lbTb_azcap.TabIndex = 13
    Me.lbTb_azcap.Text = "C.A.P."
    Me.lbTb_azcap.UseMnemonic = False
    '
    'lbTb_azcitta
    '
    Me.lbTb_azcitta.AutoSize = True
    Me.lbTb_azcitta.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azcitta.Location = New System.Drawing.Point(12, 121)
    Me.lbTb_azcitta.Name = "lbTb_azcitta"
    Me.lbTb_azcitta.NTSDbField = ""
    Me.lbTb_azcitta.Size = New System.Drawing.Size(67, 13)
    Me.lbTb_azcitta.TabIndex = 12
    Me.lbTb_azcitta.Text = "Città/località"
    Me.lbTb_azcitta.UseMnemonic = False
    '
    'lbTb_azindir
    '
    Me.lbTb_azindir.AutoSize = True
    Me.lbTb_azindir.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azindir.Location = New System.Drawing.Point(12, 95)
    Me.lbTb_azindir.Name = "lbTb_azindir"
    Me.lbTb_azindir.NTSDbField = ""
    Me.lbTb_azindir.Size = New System.Drawing.Size(47, 13)
    Me.lbTb_azindir.TabIndex = 11
    Me.lbTb_azindir.Text = "Indirizzo"
    Me.lbTb_azindir.UseMnemonic = False
    '
    'lbTb_azrags2
    '
    Me.lbTb_azrags2.AutoSize = True
    Me.lbTb_azrags2.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azrags2.Location = New System.Drawing.Point(12, 69)
    Me.lbTb_azrags2.Name = "lbTb_azrags2"
    Me.lbTb_azrags2.NTSDbField = ""
    Me.lbTb_azrags2.Size = New System.Drawing.Size(90, 13)
    Me.lbTb_azrags2.TabIndex = 10
    Me.lbTb_azrags2.Text = "Ragione sociale 2"
    Me.lbTb_azrags2.UseMnemonic = False
    '
    'lbTb_azrags1
    '
    Me.lbTb_azrags1.AutoSize = True
    Me.lbTb_azrags1.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_azrags1.Location = New System.Drawing.Point(12, 43)
    Me.lbTb_azrags1.Name = "lbTb_azrags1"
    Me.lbTb_azrags1.NTSDbField = ""
    Me.lbTb_azrags1.Size = New System.Drawing.Size(90, 13)
    Me.lbTb_azrags1.TabIndex = 9
    Me.lbTb_azrags1.Text = "Ragione sociale 1"
    Me.lbTb_azrags1.UseMnemonic = False
    '
    'lbCodditt
    '
    Me.lbCodditt.AutoSize = True
    Me.lbCodditt.BackColor = System.Drawing.Color.Transparent
    Me.lbCodditt.Location = New System.Drawing.Point(12, 13)
    Me.lbCodditt.Name = "lbCodditt"
    Me.lbCodditt.NTSDbField = ""
    Me.lbCodditt.Size = New System.Drawing.Size(64, 13)
    Me.lbCodditt.TabIndex = 8
    Me.lbCodditt.Text = "Codice ditta"
    Me.lbCodditt.UseMnemonic = False
    '
    'cmdElimina
    '
    Me.cmdElimina.Location = New System.Drawing.Point(469, 36)
    Me.cmdElimina.Name = "cmdElimina"
    Me.cmdElimina.Size = New System.Drawing.Size(149, 24)
    Me.cmdElimina.TabIndex = 7
    Me.cmdElimina.Text = "&Elimina dati"
    '
    'cmdImporta
    '
    Me.cmdImporta.Location = New System.Drawing.Point(469, 6)
    Me.cmdImporta.Name = "cmdImporta"
    Me.cmdImporta.Size = New System.Drawing.Size(149, 24)
    Me.cmdImporta.TabIndex = 6
    Me.cmdImporta.Text = "&Importa dati da altra ditta"
    '
    'FRM__CEDI
    '
    Me.ClientSize = New System.Drawing.Size(628, 278)
    Me.Controls.Add(Me.pnCedi)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.Name = "FRM__CEDI"
    Me.Text = "COPIA/ELIMINAZIONE DATI DITTA"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnCedi, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCedi.ResumeLayout(False)
    Me.pnCedi.PerformLayout()
    CType(Me.edTb_azpiva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azcodf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azprov.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azcap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azcitta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azindir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azrags2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_azrags1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodditt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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

    '------------------------------------------------
    'creo e attivo l'entity e inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__CEDI", "BE__CEDI", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128269970690085455, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleCedi = CType(oTmp, CLE__CEDI)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__CEDI", strRemoteServer, strRemotePort)
    AddHandler oCleCedi.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleCedi.Init(oApp, NTSScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    '------------------------------------------------

    Return True
  End Function
  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano già stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 

      edTb_azpiva.NTSDbField = "tabanaz.tb_azpiva"
      edTb_azcodf.NTSDbField = "tabanaz.tb_azcodf"
      edTb_azprov.NTSDbField = "tabanaz.tb_azprov"
      edTb_azcap.NTSDbField = "tabanaz.tb_azcap"
      edTb_azcitta.NTSDbField = "tabanaz.tb_azcitta"
      edTb_azindir.NTSDbField = "tabanaz.tb_azindir"
      edTb_azrags2.NTSDbField = "tabanaz.tb_azrags2"
      edTb_azrags1.NTSDbField = "tabanaz.tb_azrags1"
      edCodditt.NTSDbField = "tabanaz.codditt"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcCedi, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()
      '-------------------------------------------------

      edTb_azpiva.NTSSetParam(oMenu, oApp.Tr(Me, 128344887970345377, "Partita Iva"), 0)
      edTb_azcodf.NTSSetParam(oMenu, oApp.Tr(Me, 128344887970501613, "Codice Fiscale"), 0)
      edTb_azprov.NTSSetParam(oMenu, oApp.Tr(Me, 128344887970657849, "Provincia"), 0)
      edTb_azcap.NTSSetParam(oMenu, oApp.Tr(Me, 128344887970814085, "C.A.P."), 0)
      edTb_azcitta.NTSSetParam(oMenu, oApp.Tr(Me, 128344887970970321, "Città/località"), 0)
      edTb_azindir.NTSSetParam(oMenu, oApp.Tr(Me, 128344887971126557, "Indirizzo"), 0)
      edTb_azrags2.NTSSetParam(oMenu, oApp.Tr(Me, 128344887971282793, "Ragione sociale 2"), 0)
      edTb_azrags1.NTSSetParam(oMenu, oApp.Tr(Me, 128344887971439029, "Ragione sociale 1"), 0)
      edCodditt.NTSSetParam(oMenu, oApp.Tr(Me, 128344887971595265, "Codice ditta"), 0)


      ''non faccio lanciare lo zoom dal controllo visto che non devo cambiare il contenuto del campo ma riposizionare il dataset!!!

      edTb_azpiva.Enabled = False
      edTb_azcodf.Enabled = False
      edTb_azprov.Enabled = False
      edTb_azcap.Enabled = False
      edTb_azcitta.Enabled = False
      edTb_azindir.Enabled = False
      edTb_azrags2.Enabled = False
      edTb_azrags1.Enabled = False
      edCodditt.Enabled = False

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
        Select Case strTmp(i).Substring(0, 10)
          Case "CANCELDIT:"
            Me.Cursor = Cursors.WaitCursor
            lbBar.Text = strTmp(i).Substring(10)
            Me.Refresh()
        End Select
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "EventiForm"
  Public Overridable Sub FRM__CEDI_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-----------------------------------------------------------------------------------------
      '--- Se l'azienda corrente NON è MultiDitta
      '--- disabilita il tasto 'Importa dati da altra ditta'
      '--- i tasti e le voci di menu relativi alla navigazione e allo zoom
      '-----------------------------------------------------------------------------------------

      If oCleCedi.SetIsMultiditta = False Then
        oCleCedi.bMultiditta = False
        'cmdImporta.Enabled = False
        tlbPrimo.Enabled = False
        tlbPrecedente.Enabled = False
        tlbSuccessivo.Enabled = False
        tlbUltimo.Enabled = False
        tlbZoom.Enabled = False
      Else
        oCleCedi.bMultiditta = True
      End If
      '-----------------------------------------------------------------------------------------
      Apri()
      '-----------------------------------------------------------------------------------------
      lbBar.Text = "Pronto."
      '-----------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY Then
        cmdImporta.Visible = False
      End If
      '-----------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdImporta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImporta.Click
    Try
      ImportaDati()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdElimina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdElimina.Click
    Try
      EliminaDati()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

#End Region

#Region "EventiToolbar"

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    'per eventuali altri controlli caricati al volo
    Dim ctrlTmp As Control = NTSFindControlForZoom()
    If ctrlTmp Is Nothing Then Return
    Dim oParam As New CLE__PATB

    Try
      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMFXDI", DittaCorrente, oParam)
      'ottengo l'elenco delle ditte da elaborare separate da ,
      posizionaAfterApri(NTSZOOM.strIn.Trim)

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
      dcCedi.MoveFirst()
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
      dcCedi.MovePrevious()
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
      dcCedi.MoveNext()
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
      dcCedi.MoveLast()
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
      If Not oCleCedi.Apri(dsCedi) Then Me.Close()
      dcCedi.DataSource = dsCedi.Tables("TABANAZ")
      dsCedi.AcceptChanges()

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      'e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipodocumento, ecc ...
      'GctlTipoDoc = ""
      GctlSetRoules()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function ImportaDati() As Boolean
    Dim strTmp As String
    Dim oParam As New CLE__CLDP
    Dim frmImpd As FRM__IMPD = Nothing
    Try
      frmImpd = CType(NTSNewFormModal("FRM__IMPD"), FRM__IMPD)
      '-----------------------------------------------------------------------------------------
      '--- Avvisa che nessun utente lavori sulla ditta in esame
      '-----------------------------------------------------------------------------------------
      oApp.MsgBoxInfo(oApp.Tr(Me, 128344926232523647, "Prima di passare all'importazione dati" & _
        " assicurarsi che nessun utente lavori sulla ditta di destinazione |'" & edCodditt.Text & "'|!"))
      '-----------------------------------------------------------------------------------------
      lbBar.Text = oApp.Tr(Me, 130777955380845893, "Importazione dati da altra ditta in corso...")
      '-----------------------------------------------------------------------------------------
      oCleCedi.strImpdCoddittDest = edCodditt.Text
      oCleCedi.strImpdDescrDittDest = edTb_azrags1.Text
      oCleCedi.strImpdAzcodpcon = NTSCStr(dsCedi.Tables("TABANAZ").Rows(dcCedi.Position)!tb_azcodpcon)
      '-----------------------------------------------------------------------------------------
      '--- Chiama la modale
      '-----------------------------------------------------------------------------------------
      frmImpd.Init(oMenu, oParam, DittaCorrente)
      frmImpd.InitEntity(oCleCedi)
      frmImpd.ShowDialog()
      '-----------------------------------------------------------------------------------------
      '--- Se nella modale è stato scelto 'Tutti i dati'
      '--- chiude e riapre il recordset sui dati aggiornati e si riposiziona su record corrente
      '-----------------------------------------------------------------------------------------
      If oCleCedi.bImpdRefresh = True Then
        strTmp = edCodditt.Text
        Apri()
        posizionaAfterApri(strTmp)
      End If
      '-----------------------------------------------------------------------------------------
      oCleCedi.ScriviActlog(0, edCodditt.Text, edTb_azrags1.Text, edTb_azrags2.Text, edTb_azindir.Text, _
      edTb_azcitta.Text, edTb_azcap.Text, edTb_azprov.Text, edTb_azcodf.Text, edTb_azpiva.Text)
      '-----------------------------------------------------------------------------------------
      lbBar.Text = oApp.Tr(Me, 130777956617712491, "Pronto.")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If Not frmImpd Is Nothing Then frmImpd.Dispose()
      frmImpd = Nothing
    End Try
  End Function
  Public Overridable Function EliminaDati() As Boolean
    Dim oParam As New CLE__CLDP
    Dim frmElda As FRM__ELDA = Nothing
    Dim frmElda_f As FRM__ELDA_F = Nothing
    Try

      '-----------------------------------------------------------------------------------------
      '--- Avvisa che nessun utente lavori sulla ditta in esame
      '-----------------------------------------------------------------------------------------
      strMsg = "Prima di passare all'eliminazione dei dati relativi alla ditta '" & edCodditt.Text & "'" & _
        " assicurarsi che nessun utente lavori su tale ditta!"
      oApp.MsgBoxErr(oApp.Tr(Me, 128344947353148647, strMsg))
      '-----------------------------------------------------------------------------------------
      '--- Chiama la modale
      '-----------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY Then
        frmElda_f = CType(NTSNewFormModal("FRM__ELDA_F"), FRM__ELDA_F)
        frmElda_f.Init(oMenu, oParam, DittaCorrente)
        frmElda_f.InitEntity(oCleCedi)
        frmElda_f.ShowDialog()
      Else
        frmElda = CType(NTSNewFormModal("FRM__ELDA"), FRM__ELDA)
        frmElda.Init(oMenu, oParam, DittaCorrente)
        frmElda.InitEntity(oCleCedi)
        frmElda.ShowDialog()
      End If


      '-----------------------------------------------------------------------------------------
      '--- Se premuto annulla esce
      '-----------------------------------------------------------------------------------------
      If oCleCedi.bEldaAnnullato = True Then Exit Function
      '-----------------------------------------------------------------------------------------
      '--- Se è stata selezionata l'eliminazione delle Anagrafiche dei cespiti ma non i movimenti,
      '--- controlla che non esistano movimenti. Se non esistono prosegue.
      '-----------------------------------------------------------------------------------------
      If (oCleCedi.bEldaAnac = True) And (oCleCedi.bEldaMovc = False) Then
        If oCleCedi.ControllaEsistenzaMovcesp(edCodditt.Text) = True Then
          Dim dlgRes As DialogResult
          strMsg = "Attenzione!" & vbCrLf & _
            "E' stata selezionata la cancellazione delle 'Anagrafiche Cespiti'" & vbCrLf & _
            "ma NON i movimenti relativi che risultano essere esistenti." & vbCrLf & _
            "Procedere con la cancellazione anche dei 'Movimenti Cespiti'?"
          dlgRes = oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128873963282522158, strMsg))
          Select Case dlgRes
            Case Windows.Forms.DialogResult.No
              strMsg = "Operazione di:" & vbCrLf & _
                " . 'Eliminazione Dati' annullata."
              oApp.MsgBoxInfo(oApp.Tr(Me, 128873964587522158, strMsg))
              Exit Function
            Case Windows.Forms.DialogResult.Yes : oCleCedi.bEldaMovc = True
          End Select
        End If
      End If
      '-----------------------------------------------------------------------------------------
      If oCleCedi.EliminaDati(edCodditt.Text) = True Then
        '-----------------------------------------------------------------------------------------
        oCleCedi.ScriviActlog(1, edCodditt.Text, edTb_azrags1.Text, edTb_azrags2.Text, edTb_azindir.Text, _
          edTb_azcitta.Text, edTb_azcap.Text, edTb_azprov.Text, edTb_azcodf.Text, edTb_azpiva.Text)
        '-----------------------------------------------------------------------------------------
        oApp.MsgBoxInfo(oApp.Tr(Me, 128344949735804897, "Eliminazione dati relativi alla ditta '" & edCodditt.Text & "' terminata."))
        '-----------------------------------------------------------------------------------------
        lbBar.Text = "Pronto."
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If Not frmElda Is Nothing Then frmElda.Dispose()
      frmElda = Nothing
      If Not frmElda_f Is Nothing Then frmElda_f.Dispose()
      frmElda_f = Nothing
    End Try
  End Function

  Public Overridable Sub posizionaAfterApri(ByVal strTmp As String)
    Dim i As Integer
    Dim lPos As Integer
    Try
      If oCleCedi.bMultiditta = True Then
        lPos = dcCedi.Position
        dcCedi.MoveFirst()
        For i = 0 To dcCedi.Count - 1
          If dsCedi.Tables("TABANAZ").Rows(dcCedi.Position)("codditt").ToString = strTmp Then
            Exit Sub
          End If
          dcCedi.MoveNext()
        Next
        oApp.MsgBoxErr(oApp.Tr(Me, 128344931686586147, "Ditta inesistente."))
        dcCedi.Position = lPos
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class
