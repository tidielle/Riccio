Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__ABIC
  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = bsModExtAll
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
  Public oCleAbic As CLE__ABIC
  Public dsAbic As DataSet
  Public oCallParams As CLE__CLDP
  Public dcAbic As BindingSource = New BindingSource

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
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbAbcabi As NTSInformatica.NTSLabel
  Public WithEvents lbAbcabicab As NTSInformatica.NTSLabel
  Public WithEvents lbAbcbanc As NTSInformatica.NTSLabel
  Public WithEvents edAbcbanc As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAbcfiliale As NTSInformatica.NTSLabel
  Public WithEvents edAbcfiliale As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAbcindir As NTSInformatica.NTSLabel
  Public WithEvents edAbcindir As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAbccaplocprov As NTSInformatica.NTSLabel
  Public WithEvents edAbccap As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAbclocalita As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAbccomune As NTSInformatica.NTSLabel
  Public WithEvents edAbccomune As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAbcprov As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAbcabichk As NTSInformatica.NTSLabel
  Public WithEvents edAbcabichk As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAbccabchk As NTSInformatica.NTSLabel
  Public WithEvents edAbccabchk As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAbcstato As NTSInformatica.NTSLabel
  Public WithEvents edAbcstato As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAbcswift As NTSInformatica.NTSLabel
  Public WithEvents edAbcswift As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAbccabhq As NTSInformatica.NTSLabel
  Public WithEvents edAbccabhq As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAbctelef As NTSInformatica.NTSLabel
  Public WithEvents edAbctelef As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAbcfax As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAbcindpost As NTSInformatica.NTSLabel
  Public WithEvents edAbcindpost As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAbcnote As NTSInformatica.NTSLabel
  Public WithEvents edAbcnote As NTSInformatica.NTSMemoBox
  Public WithEvents lbAbccab As NTSInformatica.NTSLabel
  Public WithEvents pnAbic As NTSInformatica.NTSPanel
#End Region

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__ABIC))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbApri = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrecedente = New NTSInformatica.NTSBarButtonItem
    Me.tlbSuccessivo = New NTSInformatica.NTSBarButtonItem
    Me.tlbUltimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.lbAbcabi = New NTSInformatica.NTSLabel
    Me.lbAbcabicab = New NTSInformatica.NTSLabel
    Me.lbAbcbanc = New NTSInformatica.NTSLabel
    Me.edAbcbanc = New NTSInformatica.NTSTextBoxStr
    Me.lbAbcfiliale = New NTSInformatica.NTSLabel
    Me.edAbcfiliale = New NTSInformatica.NTSTextBoxStr
    Me.lbAbcindir = New NTSInformatica.NTSLabel
    Me.edAbcindir = New NTSInformatica.NTSTextBoxStr
    Me.lbAbccaplocprov = New NTSInformatica.NTSLabel
    Me.edAbccap = New NTSInformatica.NTSTextBoxStr
    Me.edAbclocalita = New NTSInformatica.NTSTextBoxStr
    Me.lbAbccomune = New NTSInformatica.NTSLabel
    Me.edAbccomune = New NTSInformatica.NTSTextBoxStr
    Me.edAbcprov = New NTSInformatica.NTSTextBoxStr
    Me.lbAbcabichk = New NTSInformatica.NTSLabel
    Me.edAbcabichk = New NTSInformatica.NTSTextBoxNum
    Me.lbAbccabchk = New NTSInformatica.NTSLabel
    Me.edAbccabchk = New NTSInformatica.NTSTextBoxNum
    Me.lbAbcstato = New NTSInformatica.NTSLabel
    Me.edAbcstato = New NTSInformatica.NTSTextBoxStr
    Me.lbAbcswift = New NTSInformatica.NTSLabel
    Me.edAbcswift = New NTSInformatica.NTSTextBoxStr
    Me.lbAbccabhq = New NTSInformatica.NTSLabel
    Me.edAbccabhq = New NTSInformatica.NTSTextBoxNum
    Me.lbAbctelef = New NTSInformatica.NTSLabel
    Me.edAbctelef = New NTSInformatica.NTSTextBoxStr
    Me.edAbcfax = New NTSInformatica.NTSTextBoxStr
    Me.lbAbcindpost = New NTSInformatica.NTSLabel
    Me.edAbcindpost = New NTSInformatica.NTSTextBoxStr
    Me.lbAbcnote = New NTSInformatica.NTSLabel
    Me.edAbcnote = New NTSInformatica.NTSMemoBox
    Me.lbAbccab = New NTSInformatica.NTSLabel
    Me.pnAbic = New NTSInformatica.NTSPanel
    Me.ckXx_abcabichk = New NTSInformatica.NTSCheckBox
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAbcbanc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAbcfiliale.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAbcindir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAbccap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAbclocalita.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAbccomune.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAbcprov.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAbcabichk.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAbccabchk.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAbcstato.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAbcswift.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAbccabhq.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAbctelef.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAbcfax.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAbcindpost.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAbcnote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnAbic, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAbic.SuspendLayout()
    CType(Me.ckXx_abcabichk.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbUltimo, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbApri})
    Me.NtsBarManager1.MaxItemId = 27
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApri), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbApri
    '
    Me.tlbApri.Caption = "Apri"
    Me.tlbApri.Glyph = CType(resources.GetObject("tlbApri.Glyph"), System.Drawing.Image)
    Me.tlbApri.Id = 26
    Me.tlbApri.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3)
    Me.tlbApri.Name = "tlbApri"
    Me.tlbApri.Visible = True
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
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 22
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta stampante"
    Me.tlbImpostaStampante.Id = 25
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 16
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 17
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
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
    'lbAbcabi
    '
    Me.lbAbcabi.BackColor = System.Drawing.Color.Transparent
    Me.lbAbcabi.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbAbcabi.Location = New System.Drawing.Point(131, 20)
    Me.lbAbcabi.Name = "lbAbcabi"
    Me.lbAbcabi.NTSDbField = ""
    Me.lbAbcabi.Size = New System.Drawing.Size(100, 20)
    Me.lbAbcabi.TabIndex = 500
    Me.lbAbcabi.Tooltip = ""
    Me.lbAbcabi.UseMnemonic = False
    '
    'lbAbcabicab
    '
    Me.lbAbcabicab.AutoSize = True
    Me.lbAbcabicab.BackColor = System.Drawing.Color.Transparent
    Me.lbAbcabicab.Location = New System.Drawing.Point(7, 21)
    Me.lbAbcabicab.Name = "lbAbcabicab"
    Me.lbAbcabicab.NTSDbField = ""
    Me.lbAbcabicab.Size = New System.Drawing.Size(100, 13)
    Me.lbAbcabicab.TabIndex = 11
    Me.lbAbcabicab.Text = "Cod.ABI / Cod.CAB"
    Me.lbAbcabicab.Tooltip = ""
    Me.lbAbcabicab.UseMnemonic = False
    '
    'lbAbcbanc
    '
    Me.lbAbcbanc.AutoSize = True
    Me.lbAbcbanc.BackColor = System.Drawing.Color.Transparent
    Me.lbAbcbanc.Location = New System.Drawing.Point(7, 49)
    Me.lbAbcbanc.Name = "lbAbcbanc"
    Me.lbAbcbanc.NTSDbField = ""
    Me.lbAbcbanc.Size = New System.Drawing.Size(66, 13)
    Me.lbAbcbanc.TabIndex = 12
    Me.lbAbcbanc.Text = "Nome Banca"
    Me.lbAbcbanc.Tooltip = ""
    Me.lbAbcbanc.UseMnemonic = False
    '
    'edAbcbanc
    '
    Me.edAbcbanc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAbcbanc.Location = New System.Drawing.Point(131, 46)
    Me.edAbcbanc.Name = "edAbcbanc"
    Me.edAbcbanc.NTSDbField = ""
    Me.edAbcbanc.NTSForzaVisZoom = False
    Me.edAbcbanc.NTSOldValue = ""
    Me.edAbcbanc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAbcbanc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAbcbanc.Properties.AutoHeight = False
    Me.edAbcbanc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAbcbanc.Properties.MaxLength = 65536
    Me.edAbcbanc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAbcbanc.Size = New System.Drawing.Size(365, 20)
    Me.edAbcbanc.TabIndex = 502
    '
    'lbAbcfiliale
    '
    Me.lbAbcfiliale.AutoSize = True
    Me.lbAbcfiliale.BackColor = System.Drawing.Color.Transparent
    Me.lbAbcfiliale.Location = New System.Drawing.Point(7, 153)
    Me.lbAbcfiliale.Name = "lbAbcfiliale"
    Me.lbAbcfiliale.NTSDbField = ""
    Me.lbAbcfiliale.Size = New System.Drawing.Size(63, 13)
    Me.lbAbcfiliale.TabIndex = 13
    Me.lbAbcfiliale.Text = "Nome Filiale"
    Me.lbAbcfiliale.Tooltip = ""
    Me.lbAbcfiliale.UseMnemonic = False
    '
    'edAbcfiliale
    '
    Me.edAbcfiliale.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAbcfiliale.Location = New System.Drawing.Point(131, 150)
    Me.edAbcfiliale.Name = "edAbcfiliale"
    Me.edAbcfiliale.NTSDbField = ""
    Me.edAbcfiliale.NTSForzaVisZoom = False
    Me.edAbcfiliale.NTSOldValue = ""
    Me.edAbcfiliale.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAbcfiliale.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAbcfiliale.Properties.AutoHeight = False
    Me.edAbcfiliale.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAbcfiliale.Properties.MaxLength = 65536
    Me.edAbcfiliale.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAbcfiliale.Size = New System.Drawing.Size(365, 20)
    Me.edAbcfiliale.TabIndex = 503
    '
    'lbAbcindir
    '
    Me.lbAbcindir.AutoSize = True
    Me.lbAbcindir.BackColor = System.Drawing.Color.Transparent
    Me.lbAbcindir.Location = New System.Drawing.Point(7, 102)
    Me.lbAbcindir.Name = "lbAbcindir"
    Me.lbAbcindir.NTSDbField = ""
    Me.lbAbcindir.Size = New System.Drawing.Size(47, 13)
    Me.lbAbcindir.TabIndex = 14
    Me.lbAbcindir.Text = "Indirizzo"
    Me.lbAbcindir.Tooltip = ""
    Me.lbAbcindir.UseMnemonic = False
    '
    'edAbcindir
    '
    Me.edAbcindir.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAbcindir.Location = New System.Drawing.Point(131, 99)
    Me.edAbcindir.Name = "edAbcindir"
    Me.edAbcindir.NTSDbField = ""
    Me.edAbcindir.NTSForzaVisZoom = False
    Me.edAbcindir.NTSOldValue = ""
    Me.edAbcindir.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAbcindir.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAbcindir.Properties.AutoHeight = False
    Me.edAbcindir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAbcindir.Properties.MaxLength = 65536
    Me.edAbcindir.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAbcindir.Size = New System.Drawing.Size(365, 20)
    Me.edAbcindir.TabIndex = 504
    '
    'lbAbccaplocprov
    '
    Me.lbAbccaplocprov.AutoSize = True
    Me.lbAbccaplocprov.BackColor = System.Drawing.Color.Transparent
    Me.lbAbccaplocprov.Location = New System.Drawing.Point(7, 128)
    Me.lbAbccaplocprov.Name = "lbAbccaplocprov"
    Me.lbAbccaplocprov.NTSDbField = ""
    Me.lbAbccaplocprov.Size = New System.Drawing.Size(100, 13)
    Me.lbAbccaplocprov.TabIndex = 15
    Me.lbAbccaplocprov.Text = "Cap -- Loc. -- Prov."
    Me.lbAbccaplocprov.Tooltip = ""
    Me.lbAbccaplocprov.UseMnemonic = False
    '
    'edAbccap
    '
    Me.edAbccap.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAbccap.Location = New System.Drawing.Point(131, 125)
    Me.edAbccap.Name = "edAbccap"
    Me.edAbccap.NTSDbField = ""
    Me.edAbccap.NTSForzaVisZoom = False
    Me.edAbccap.NTSOldValue = ""
    Me.edAbccap.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAbccap.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAbccap.Properties.AutoHeight = False
    Me.edAbccap.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAbccap.Properties.MaxLength = 65536
    Me.edAbccap.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAbccap.Size = New System.Drawing.Size(88, 20)
    Me.edAbccap.TabIndex = 505
    '
    'edAbclocalita
    '
    Me.edAbclocalita.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAbclocalita.Location = New System.Drawing.Point(225, 125)
    Me.edAbclocalita.Name = "edAbclocalita"
    Me.edAbclocalita.NTSDbField = ""
    Me.edAbclocalita.NTSForzaVisZoom = False
    Me.edAbclocalita.NTSOldValue = ""
    Me.edAbclocalita.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAbclocalita.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAbclocalita.Properties.AutoHeight = False
    Me.edAbclocalita.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAbclocalita.Properties.MaxLength = 65536
    Me.edAbclocalita.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAbclocalita.Size = New System.Drawing.Size(119, 20)
    Me.edAbclocalita.TabIndex = 506
    '
    'lbAbccomune
    '
    Me.lbAbccomune.AutoSize = True
    Me.lbAbccomune.BackColor = System.Drawing.Color.Transparent
    Me.lbAbccomune.Location = New System.Drawing.Point(7, 73)
    Me.lbAbccomune.Name = "lbAbccomune"
    Me.lbAbccomune.NTSDbField = ""
    Me.lbAbccomune.Size = New System.Drawing.Size(46, 13)
    Me.lbAbccomune.TabIndex = 17
    Me.lbAbccomune.Text = "Comune"
    Me.lbAbccomune.Tooltip = ""
    Me.lbAbccomune.UseMnemonic = False
    '
    'edAbccomune
    '
    Me.edAbccomune.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAbccomune.Location = New System.Drawing.Point(131, 73)
    Me.edAbccomune.Name = "edAbccomune"
    Me.edAbccomune.NTSDbField = ""
    Me.edAbccomune.NTSForzaVisZoom = False
    Me.edAbccomune.NTSOldValue = ""
    Me.edAbccomune.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAbccomune.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAbccomune.Properties.AutoHeight = False
    Me.edAbccomune.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAbccomune.Properties.MaxLength = 65536
    Me.edAbccomune.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAbccomune.Size = New System.Drawing.Size(365, 20)
    Me.edAbccomune.TabIndex = 507
    '
    'edAbcprov
    '
    Me.edAbcprov.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAbcprov.Location = New System.Drawing.Point(350, 125)
    Me.edAbcprov.Name = "edAbcprov"
    Me.edAbcprov.NTSDbField = ""
    Me.edAbcprov.NTSForzaVisZoom = False
    Me.edAbcprov.NTSOldValue = ""
    Me.edAbcprov.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAbcprov.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAbcprov.Properties.AutoHeight = False
    Me.edAbcprov.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAbcprov.Properties.MaxLength = 65536
    Me.edAbcprov.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAbcprov.Size = New System.Drawing.Size(47, 20)
    Me.edAbcprov.TabIndex = 508
    '
    'lbAbcabichk
    '
    Me.lbAbcabichk.AutoSize = True
    Me.lbAbcabichk.BackColor = System.Drawing.Color.Transparent
    Me.lbAbcabichk.Enabled = False
    Me.lbAbcabichk.Location = New System.Drawing.Point(7, 254)
    Me.lbAbcabichk.Name = "lbAbcabichk"
    Me.lbAbcabichk.NTSDbField = ""
    Me.lbAbcabichk.Size = New System.Drawing.Size(79, 13)
    Me.lbAbcabichk.TabIndex = 19
    Me.lbAbcabichk.Text = "Check digit ABI"
    Me.lbAbcabichk.Tooltip = ""
    Me.lbAbcabichk.UseMnemonic = False
    Me.lbAbcabichk.Visible = False
    '
    'edAbcabichk
    '
    Me.edAbcabichk.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAbcabichk.EditValue = "0"
    Me.edAbcabichk.Enabled = False
    Me.edAbcabichk.Location = New System.Drawing.Point(131, 251)
    Me.edAbcabichk.Name = "edAbcabichk"
    Me.edAbcabichk.NTSDbField = ""
    Me.edAbcabichk.NTSFormat = "0"
    Me.edAbcabichk.NTSForzaVisZoom = False
    Me.edAbcabichk.NTSOldValue = ""
    Me.edAbcabichk.Properties.Appearance.Options.UseTextOptions = True
    Me.edAbcabichk.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAbcabichk.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAbcabichk.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAbcabichk.Properties.AutoHeight = False
    Me.edAbcabichk.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAbcabichk.Properties.MaxLength = 65536
    Me.edAbcabichk.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAbcabichk.Size = New System.Drawing.Size(40, 20)
    Me.edAbcabichk.TabIndex = 509
    Me.edAbcabichk.Visible = False
    '
    'lbAbccabchk
    '
    Me.lbAbccabchk.AutoSize = True
    Me.lbAbccabchk.BackColor = System.Drawing.Color.Transparent
    Me.lbAbccabchk.Enabled = False
    Me.lbAbccabchk.Location = New System.Drawing.Point(288, 257)
    Me.lbAbccabchk.Name = "lbAbccabchk"
    Me.lbAbccabchk.NTSDbField = ""
    Me.lbAbccabchk.Size = New System.Drawing.Size(82, 13)
    Me.lbAbccabchk.TabIndex = 20
    Me.lbAbccabchk.Text = "Check digit CAB"
    Me.lbAbccabchk.Tooltip = ""
    Me.lbAbccabchk.UseMnemonic = False
    Me.lbAbccabchk.Visible = False
    '
    'edAbccabchk
    '
    Me.edAbccabchk.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAbccabchk.EditValue = "0"
    Me.edAbccabchk.Enabled = False
    Me.edAbccabchk.Location = New System.Drawing.Point(454, 251)
    Me.edAbccabchk.Name = "edAbccabchk"
    Me.edAbccabchk.NTSDbField = ""
    Me.edAbccabchk.NTSFormat = "0"
    Me.edAbccabchk.NTSForzaVisZoom = False
    Me.edAbccabchk.NTSOldValue = ""
    Me.edAbccabchk.Properties.Appearance.Options.UseTextOptions = True
    Me.edAbccabchk.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAbccabchk.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAbccabchk.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAbccabchk.Properties.AutoHeight = False
    Me.edAbccabchk.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAbccabchk.Properties.MaxLength = 65536
    Me.edAbccabchk.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAbccabchk.Size = New System.Drawing.Size(42, 20)
    Me.edAbccabchk.TabIndex = 510
    Me.edAbccabchk.Visible = False
    '
    'lbAbcstato
    '
    Me.lbAbcstato.AutoSize = True
    Me.lbAbcstato.BackColor = System.Drawing.Color.Transparent
    Me.lbAbcstato.Location = New System.Drawing.Point(397, 127)
    Me.lbAbcstato.Name = "lbAbcstato"
    Me.lbAbcstato.NTSDbField = ""
    Me.lbAbcstato.Size = New System.Drawing.Size(33, 13)
    Me.lbAbcstato.TabIndex = 21
    Me.lbAbcstato.Text = "Stato"
    Me.lbAbcstato.Tooltip = ""
    Me.lbAbcstato.UseMnemonic = False
    '
    'edAbcstato
    '
    Me.edAbcstato.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAbcstato.Location = New System.Drawing.Point(436, 124)
    Me.edAbcstato.Name = "edAbcstato"
    Me.edAbcstato.NTSDbField = ""
    Me.edAbcstato.NTSForzaVisZoom = False
    Me.edAbcstato.NTSOldValue = ""
    Me.edAbcstato.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAbcstato.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAbcstato.Properties.AutoHeight = False
    Me.edAbcstato.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAbcstato.Properties.MaxLength = 65536
    Me.edAbcstato.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAbcstato.Size = New System.Drawing.Size(60, 20)
    Me.edAbcstato.TabIndex = 511
    '
    'lbAbcswift
    '
    Me.lbAbcswift.AutoSize = True
    Me.lbAbcswift.BackColor = System.Drawing.Color.Transparent
    Me.lbAbcswift.Location = New System.Drawing.Point(7, 179)
    Me.lbAbcswift.Name = "lbAbcswift"
    Me.lbAbcswift.NTSDbField = ""
    Me.lbAbcswift.Size = New System.Drawing.Size(86, 13)
    Me.lbAbcswift.TabIndex = 22
    Me.lbAbcswift.Text = "Cod. BIC/SWIFT"
    Me.lbAbcswift.Tooltip = ""
    Me.lbAbcswift.UseMnemonic = False
    '
    'edAbcswift
    '
    Me.edAbcswift.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAbcswift.Location = New System.Drawing.Point(131, 176)
    Me.edAbcswift.Name = "edAbcswift"
    Me.edAbcswift.NTSDbField = ""
    Me.edAbcswift.NTSForzaVisZoom = False
    Me.edAbcswift.NTSOldValue = ""
    Me.edAbcswift.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAbcswift.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAbcswift.Properties.AutoHeight = False
    Me.edAbcswift.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAbcswift.Properties.MaxLength = 65536
    Me.edAbcswift.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAbcswift.Size = New System.Drawing.Size(151, 20)
    Me.edAbcswift.TabIndex = 512
    '
    'lbAbccabhq
    '
    Me.lbAbccabhq.AutoSize = True
    Me.lbAbccabhq.BackColor = System.Drawing.Color.Transparent
    Me.lbAbccabhq.Location = New System.Drawing.Point(8, 205)
    Me.lbAbccabhq.Name = "lbAbccabhq"
    Me.lbAbccabhq.NTSDbField = ""
    Me.lbAbccabhq.Size = New System.Drawing.Size(120, 13)
    Me.lbAbccabhq.TabIndex = 23
    Me.lbAbccabhq.Text = "Cod. Cab sede centrale"
    Me.lbAbccabhq.Tooltip = ""
    Me.lbAbccabhq.UseMnemonic = False
    '
    'edAbccabhq
    '
    Me.edAbccabhq.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAbccabhq.EditValue = "0"
    Me.edAbccabhq.Location = New System.Drawing.Point(131, 202)
    Me.edAbccabhq.Name = "edAbccabhq"
    Me.edAbccabhq.NTSDbField = ""
    Me.edAbccabhq.NTSFormat = "0"
    Me.edAbccabhq.NTSForzaVisZoom = False
    Me.edAbccabhq.NTSOldValue = ""
    Me.edAbccabhq.Properties.Appearance.Options.UseTextOptions = True
    Me.edAbccabhq.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAbccabhq.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAbccabhq.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAbccabhq.Properties.AutoHeight = False
    Me.edAbccabhq.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAbccabhq.Properties.MaxLength = 65536
    Me.edAbccabhq.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAbccabhq.Size = New System.Drawing.Size(65, 20)
    Me.edAbccabhq.TabIndex = 513
    '
    'lbAbctelef
    '
    Me.lbAbctelef.AutoSize = True
    Me.lbAbctelef.BackColor = System.Drawing.Color.Transparent
    Me.lbAbctelef.Location = New System.Drawing.Point(8, 231)
    Me.lbAbctelef.Name = "lbAbctelef"
    Me.lbAbctelef.NTSDbField = ""
    Me.lbAbctelef.Size = New System.Drawing.Size(77, 13)
    Me.lbAbctelef.TabIndex = 24
    Me.lbAbctelef.Text = "Telefono / Fax"
    Me.lbAbctelef.Tooltip = ""
    Me.lbAbctelef.UseMnemonic = False
    '
    'edAbctelef
    '
    Me.edAbctelef.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAbctelef.Location = New System.Drawing.Point(131, 228)
    Me.edAbctelef.Name = "edAbctelef"
    Me.edAbctelef.NTSDbField = ""
    Me.edAbctelef.NTSForzaVisZoom = False
    Me.edAbctelef.NTSOldValue = ""
    Me.edAbctelef.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAbctelef.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAbctelef.Properties.AutoHeight = False
    Me.edAbctelef.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAbctelef.Properties.MaxLength = 65536
    Me.edAbctelef.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAbctelef.Size = New System.Drawing.Size(151, 20)
    Me.edAbctelef.TabIndex = 514
    '
    'edAbcfax
    '
    Me.edAbcfax.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAbcfax.Location = New System.Drawing.Point(291, 228)
    Me.edAbcfax.Name = "edAbcfax"
    Me.edAbcfax.NTSDbField = ""
    Me.edAbcfax.NTSForzaVisZoom = False
    Me.edAbcfax.NTSOldValue = ""
    Me.edAbcfax.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAbcfax.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAbcfax.Properties.AutoHeight = False
    Me.edAbcfax.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAbcfax.Properties.MaxLength = 65536
    Me.edAbcfax.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAbcfax.Size = New System.Drawing.Size(205, 20)
    Me.edAbcfax.TabIndex = 515
    '
    'lbAbcindpost
    '
    Me.lbAbcindpost.AutoSize = True
    Me.lbAbcindpost.BackColor = System.Drawing.Color.Transparent
    Me.lbAbcindpost.Location = New System.Drawing.Point(204, 205)
    Me.lbAbcindpost.Name = "lbAbcindpost"
    Me.lbAbcindpost.NTSDbField = ""
    Me.lbAbcindpost.Size = New System.Drawing.Size(85, 13)
    Me.lbAbcindpost.TabIndex = 26
    Me.lbAbcindpost.Text = "Indirizzo postale"
    Me.lbAbcindpost.Tooltip = ""
    Me.lbAbcindpost.UseMnemonic = False
    '
    'edAbcindpost
    '
    Me.edAbcindpost.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAbcindpost.Location = New System.Drawing.Point(291, 202)
    Me.edAbcindpost.Name = "edAbcindpost"
    Me.edAbcindpost.NTSDbField = ""
    Me.edAbcindpost.NTSForzaVisZoom = False
    Me.edAbcindpost.NTSOldValue = ""
    Me.edAbcindpost.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAbcindpost.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAbcindpost.Properties.AutoHeight = False
    Me.edAbcindpost.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAbcindpost.Properties.MaxLength = 65536
    Me.edAbcindpost.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAbcindpost.Size = New System.Drawing.Size(205, 20)
    Me.edAbcindpost.TabIndex = 516
    '
    'lbAbcnote
    '
    Me.lbAbcnote.AutoSize = True
    Me.lbAbcnote.BackColor = System.Drawing.Color.Transparent
    Me.lbAbcnote.Location = New System.Drawing.Point(8, 253)
    Me.lbAbcnote.Name = "lbAbcnote"
    Me.lbAbcnote.NTSDbField = ""
    Me.lbAbcnote.Size = New System.Drawing.Size(30, 13)
    Me.lbAbcnote.TabIndex = 27
    Me.lbAbcnote.Text = "Note"
    Me.lbAbcnote.Tooltip = ""
    Me.lbAbcnote.UseMnemonic = False
    '
    'edAbcnote
    '
    Me.edAbcnote.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAbcnote.Location = New System.Drawing.Point(131, 251)
    Me.edAbcnote.Name = "edAbcnote"
    Me.edAbcnote.NTSDbField = ""
    Me.edAbcnote.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAbcnote.Size = New System.Drawing.Size(365, 54)
    Me.edAbcnote.TabIndex = 517
    '
    'lbAbccab
    '
    Me.lbAbccab.BackColor = System.Drawing.Color.Transparent
    Me.lbAbccab.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbAbccab.Location = New System.Drawing.Point(237, 20)
    Me.lbAbccab.Name = "lbAbccab"
    Me.lbAbccab.NTSDbField = ""
    Me.lbAbccab.Size = New System.Drawing.Size(100, 20)
    Me.lbAbccab.TabIndex = 518
    Me.lbAbccab.Tooltip = ""
    Me.lbAbccab.UseMnemonic = False
    '
    'pnAbic
    '
    Me.pnAbic.AllowDrop = True
    Me.pnAbic.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAbic.Appearance.Options.UseBackColor = True
    Me.pnAbic.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAbic.Controls.Add(Me.ckXx_abcabichk)
    Me.pnAbic.Controls.Add(Me.lbAbccab)
    Me.pnAbic.Controls.Add(Me.lbAbcfiliale)
    Me.pnAbic.Controls.Add(Me.lbAbcabi)
    Me.pnAbic.Controls.Add(Me.edAbcfiliale)
    Me.pnAbic.Controls.Add(Me.lbAbcbanc)
    Me.pnAbic.Controls.Add(Me.lbAbccaplocprov)
    Me.pnAbic.Controls.Add(Me.lbAbcabicab)
    Me.pnAbic.Controls.Add(Me.edAbccap)
    Me.pnAbic.Controls.Add(Me.edAbcbanc)
    Me.pnAbic.Controls.Add(Me.lbAbccomune)
    Me.pnAbic.Controls.Add(Me.edAbcindpost)
    Me.pnAbic.Controls.Add(Me.edAbccomune)
    Me.pnAbic.Controls.Add(Me.lbAbcindpost)
    Me.pnAbic.Controls.Add(Me.lbAbcindir)
    Me.pnAbic.Controls.Add(Me.edAbcabichk)
    Me.pnAbic.Controls.Add(Me.edAbctelef)
    Me.pnAbic.Controls.Add(Me.lbAbcstato)
    Me.pnAbic.Controls.Add(Me.edAbcindir)
    Me.pnAbic.Controls.Add(Me.edAbcstato)
    Me.pnAbic.Controls.Add(Me.lbAbctelef)
    Me.pnAbic.Controls.Add(Me.lbAbccabhq)
    Me.pnAbic.Controls.Add(Me.edAbcswift)
    Me.pnAbic.Controls.Add(Me.edAbccabhq)
    Me.pnAbic.Controls.Add(Me.lbAbcswift)
    Me.pnAbic.Controls.Add(Me.edAbcfax)
    Me.pnAbic.Controls.Add(Me.edAbccabchk)
    Me.pnAbic.Controls.Add(Me.lbAbcnote)
    Me.pnAbic.Controls.Add(Me.edAbclocalita)
    Me.pnAbic.Controls.Add(Me.edAbcnote)
    Me.pnAbic.Controls.Add(Me.lbAbccabchk)
    Me.pnAbic.Controls.Add(Me.edAbcprov)
    Me.pnAbic.Controls.Add(Me.lbAbcabichk)
    Me.pnAbic.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnAbic.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnAbic.Location = New System.Drawing.Point(0, 30)
    Me.pnAbic.Name = "pnAbic"
    Me.pnAbic.NTSActiveTrasparency = True
    Me.pnAbic.Size = New System.Drawing.Size(509, 313)
    Me.pnAbic.TabIndex = 5
    Me.pnAbic.Text = "NtsPanel1"
    '
    'ckXx_abcabichk
    '
    Me.ckXx_abcabichk.Location = New System.Drawing.Point(350, 20)
    Me.ckXx_abcabichk.Name = "ckXx_abcabichk"
    Me.ckXx_abcabichk.NTSCheckValue = "S"
    Me.ckXx_abcabichk.NTSUnCheckValue = "N"
    Me.ckXx_abcabichk.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckXx_abcabichk.Properties.Appearance.Options.UseBackColor = True
    Me.ckXx_abcabichk.Properties.AutoHeight = False
    Me.ckXx_abcabichk.Properties.Caption = "Soppresso"
    Me.ckXx_abcabichk.Size = New System.Drawing.Size(80, 20)
    Me.ckXx_abcabichk.TabIndex = 519
    '
    'FRM__ABIC
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(509, 343)
    Me.Controls.Add(Me.pnAbic)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRM__ABIC"
    Me.Text = "GESTIONE ABI/CAB"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAbcbanc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAbcfiliale.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAbcindir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAbccap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAbclocalita.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAbccomune.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAbcprov.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAbcabichk.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAbccabchk.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAbcstato.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAbcswift.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAbccabhq.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAbctelef.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAbcfax.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAbcindpost.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAbcnote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnAbic, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAbic.ResumeLayout(False)
    Me.pnAbic.PerformLayout()
    CType(Me.ckXx_abcabichk.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    'creo e attivo l'entity e inizializzo la funzione che dovrÃ  rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__ABIC", "BE__ABIC", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128492708370430440, "ERRORE in fase di creazione Entity:" & vbCrLf) & strErr)
      Return False
    End If
    oCleAbic = CType(oTmp, CLE__ABIC)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__ABIC", strRemoteServer, strRemotePort)
    AddHandler oCleAbic.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleAbic.Init(oApp, oScript, oMenu.oCleComm, "ABICAB", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim i As Integer = 0
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbApri.GlyphPath = (oApp.ChildImageDir & "\open.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'Ã¨ una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      '-------------------------------------------------
      'completo le informazioni dei controlli
      edAbcbanc.NTSSetParam(oMenu, oApp.Tr(Me, 128867052852857681, "Nome Banca"), 50, True)
      edAbcfiliale.NTSSetParam(oMenu, oApp.Tr(Me, 128867052872875931, "Nome Filiale"), 50, True)
      edAbcindir.NTSSetParam(oMenu, oApp.Tr(Me, 128867052887484371, "Indirizzo"), 70, True)
      edAbccap.NTSSetParam(oMenu, oApp.Tr(Me, 128867052900559706, "Cap"), 10, True)
      edAbclocalita.NTSSetParam(oMenu, oApp.Tr(Me, 128867052914572481, "Loc."), 50, True)
      edAbccomune.NTSSetParam(oMenu, oApp.Tr(Me, 128867052927852881, "Comune"), 50, True)
      edAbcprov.NTSSetParam(oMenu, oApp.Tr(Me, 128867052941533646, "Prov."), 2, True)
      edAbcabichk.NTSSetParam(oMenu, oApp.Tr(Me, 128867052955312061, "Check digit ABI"), "0", 1, 0, 9)
      edAbccabchk.NTSSetParam(oMenu, oApp.Tr(Me, 128867052977527436, "Check digit CAB"), "0", 1, 0, 9)
      edAbcstato.NTSSetParam(oMenu, oApp.Tr(Me, 128867052990514886, "Stato"), 3, True)
      edAbcswift.NTSSetParam(oMenu, oApp.Tr(Me, 128867053003307036, "SWIFT code"), 14, True)
      edAbccabhq.NTSSetParam(oMenu, oApp.Tr(Me, 128867053020434846, "Cod. Cab sede centrale"), "0", 5, 0, 99999)
      edAbctelef.NTSSetParam(oMenu, oApp.Tr(Me, 128867053036097906, "Telefono"), 18, True)
      edAbcfax.NTSSetParam(oMenu, oApp.Tr(Me, 128867053049866556, "Fax"), 18, True)
      edAbcindpost.NTSSetParam(oMenu, oApp.Tr(Me, 128867053063098131, "Indirizzo postale"), 35, True)
      edAbcnote.NTSSetParam(oMenu, oApp.Tr(Me, 128492919576182836, "Note"), 255)
      ckXx_abcabichk.NTSSetParam(oMenu, oApp.Tr(Me, 130482502712171855, "Soppresso"), "S", "N")

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
      lbAbcabi.NTSDbField = "ABICAB.abcabi"
      lbAbccab.NTSDbField = "ABICAB.abccab"
      edAbcbanc.NTSDbField = "ABICAB.abcbanc"
      edAbcfiliale.NTSDbField = "ABICAB.abcfiliale"
      edAbcindir.NTSDbField = "ABICAB.abcindir"
      edAbccap.NTSDbField = "ABICAB.abccap"
      edAbclocalita.NTSDbField = "ABICAB.abclocalita"
      edAbccomune.NTSDbField = "ABICAB.abccomune"
      edAbcprov.NTSDbField = "ABICAB.abcprov"
      edAbcabichk.NTSDbField = "ABICAB.abcabichk"
      edAbccabchk.NTSDbField = "ABICAB.abccabchk"
      edAbcstato.NTSDbField = "ABICAB.abcstato"
      edAbcswift.NTSDbField = "ABICAB.abcswift"
      edAbccabhq.NTSDbField = "ABICAB.abccabhq"
      edAbctelef.NTSDbField = "ABICAB.abctelef"
      edAbcfax.NTSDbField = "ABICAB.abcfax"
      edAbcindpost.NTSDbField = "ABICAB.abcindpost"
      edAbcnote.NTSDbField = "ABICAB.abcnote"
      ckXx_abcabichk.NTSText.NTSDbField = "ABICAB.xx_abcabichk"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcAbic, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRM__ABIC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      SetStato(0)

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__ABIC_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub FRM__ABIC_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcAbic.Dispose()
      dsAbic.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Dim oParam As New CLE__CLDP
    Dim frmSabi As FRM__SABI = Nothing
    Try
      frmSabi = CType(NTSNewFormModal("FRM__SABI"), FRM__SABI)

      '-------------------------------------------------
      'creo una nuova forma di pagamento
      If Not Salva() Then Return
      SetStato(0)

      oCleAbic.bAbicApri = False
      oCleAbic.bAbicNuovo = True

      frmSabi.Init(oMenu, oParam, DittaCorrente)
      frmSabi.InitEntity(oCleAbic)
      frmSabi.ShowDialog()

      If frmSabi.bSabiAnnullato = True Then Exit Sub

      oCleAbic.lAbicAbi = frmSabi.lAbicAbi
      oCleAbic.lAbicCab = frmSabi.lAbicCab
      oCleAbic.strAbicComune = frmSabi.strAbicComune

      oCleAbic.NuovoAbiCab(dsAbic)

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      dcAbic.DataSource = dsAbic.Tables("ABICAB")
      dcAbic.MoveLast()

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()
      dcAbic.ResetBindings(False)

      SetStato(1)

      edAbcbanc.Focus()

      '-------------------------------------------------
      'imposto i valori di default come impostato nella GCTL
      Me.GctlApplicaDefaultValue()
      ColoraAbiCabSoppressi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmSabi Is Nothing Then frmSabi.Dispose()
      frmSabi = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbApri_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApri.ItemClick
    Dim oParam As New CLE__CLDP
    Dim frmSabi As FRM__SABI = Nothing
    Try
      frmSabi = CType(NTSNewFormModal("FRM__SABI"), FRM__SABI)

      '-------------------------------------------------
      'apro un nuovo abi cab
      If Not Salva() Then Return
      SetStato(0)

      oCleAbic.bAbicApri = True
      oCleAbic.bAbicNuovo = False

      frmSabi.Init(oMenu, oParam, DittaCorrente)
      frmSabi.InitEntity(oCleAbic)
      frmSabi.ShowDialog()

      If frmSabi.bSabiAnnullato = True Then Exit Sub

      oCleAbic.lAbicAbi = frmSabi.lAbicAbi
      oCleAbic.lAbicCab = frmSabi.lAbicCab
      oCleAbic.strAbicComune = frmSabi.strAbicComune

      oCleAbic.ApriAbiCab(dsAbic)

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      dcAbic.DataSource = dsAbic.Tables("ABICAB")
      dsAbic.AcceptChanges()

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()
      dcAbic.ResetBindings(False)

      SetStato(1)

      edAbcbanc.Focus()
      ColoraAbiCabSoppressi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmSabi Is Nothing Then frmSabi.Dispose()
      frmSabi = Nothing
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
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128492708370588320, "Confermi la cancellazione?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes

          If dsAbic.Tables("ABICAB").Rows.Count = 1 Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          dcAbic.RemoveAt(dcAbic.Position)
          oCleAbic.Salva(True)

          If bRemovBinding Then
            SetStato(0)
          End If
          Return
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcAbic, Me)
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
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128492708370746200, "Ripristinare le modifiche apportate?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          If dsAbic.Tables("ABICAB").Rows.Count = 1 And dsAbic.Tables("ABICAB").Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          oCleAbic.Ripristina(dcAbic.Position, dcAbic.Filter)

          If bRemovBinding Then
            SetStato(0)
          End If
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcAbic, Me)
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

  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    '-------------------------------------------------
    'vado sul primo record
    If Not Salva() Then Return
    dcAbic.MoveFirst()
  End Sub

  Public Overridable Sub tlbPrecedente_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrecedente.ItemClick
    '-------------------------------------------------
    'vado sul record precedente
    If Not Salva() Then Return
    dcAbic.MovePrevious()
  End Sub

  Public Overridable Sub tlbSuccessivo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSuccessivo.ItemClick
    '-------------------------------------------------
    'vado sul record successivo
    If Not Salva() Then Return
    dcAbic.MoveNext()
  End Sub

  Public Overridable Sub tlbUltimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbUltimo.ItemClick
    '-------------------------------------------------
    'vado sull'ultimo record
    If Not Salva() Then Return
    dcAbic.MoveLast()
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      If Not Salva() Then Return
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      If Not Salva() Then Return
      Stampa(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub
#End Region

  Public Overridable Function Salva() As Boolean
    Dim dRes As DialogResult
    Try
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      If oCleAbic.RecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128492708370904080, "Confermi il salvataggio?"))
        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          'Prima del salvataggio devo rimuovere la colonna XX e poi riaggiungerla.
          dsAbic.Tables("ABICAB").Columns.Remove("xx_abcabichk")
          If Not oCleAbic.Salva(False) Then Return False
          dsAbic.Tables("ABICAB").Columns.Add("xx_abcabichk", GetType(String))
          For Each dtrRow As DataRow In dsAbic.Tables("ABICAB").Rows
            If NTSCInt(dtrRow!AbcAbichk) = 0 Then
              dtrRow!xx_abcabichk = "N"
            Else
              dtrRow!xx_abcabichk = "S"
            End If
          Next
          dsAbic.AcceptChanges()
          oCleAbic.bHasChanges = False
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

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer

    Try
      '--------------------------------------------------
      'preparo il motore di stampa
      strCrpe = ""
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BS--ABIC", "Reports1", " ", 0, nDestin, "BS--ABIC.RPT", False, "Tabella ABI/CAB", False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce già  il multireport)
      For i = LBound(CType(nPjob, Array), 2) To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub SetStato(ByVal nSetStato As Integer)
    Try
      '----------------------------------
      If nSetStato = 0 Then

        pnAbic.Visible = False

        tlbSalva.Enabled = False
        tlbCancella.Enabled = False
        tlbRipristina.Enabled = False
        tlbZoom.Enabled = False
        tlbPrimo.Enabled = False
        tlbPrecedente.Enabled = False
        tlbSuccessivo.Enabled = False
        tlbUltimo.Enabled = False
        tlbImpostaStampante.Enabled = False
        tlbStampa.Enabled = False
        tlbStampaVideo.Enabled = False

        oCleAbic.bAbicApri = False
        oCleAbic.bAbicNuovo = False

        If Not dsAbic Is Nothing Then
          dsAbic.AcceptChanges()
          oCleAbic.bHasChanges = False
        End If

      Else

        GctlSetVisEnab(pnAbic, True)

        GctlSetVisEnab(pnAbic, False)
        GctlSetVisEnab(tlbSalva, False)
        GctlSetVisEnab(tlbCancella, False)
        GctlSetVisEnab(tlbRipristina, False)
        GctlSetVisEnab(tlbZoom, False)
        GctlSetVisEnab(tlbPrimo, False)
        GctlSetVisEnab(tlbPrecedente, False)
        GctlSetVisEnab(tlbSuccessivo, False)
        GctlSetVisEnab(tlbUltimo, False)
        GctlSetVisEnab(tlbImpostaStampante, False)
        GctlSetVisEnab(tlbStampa, False)
        GctlSetVisEnab(tlbStampaVideo, False)

      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ColoraAbiCabSoppressi()
    Try
      If NTSCStr(dsAbic.Tables("ABICAB").Rows(0)!xx_abcabichk) = "S" Then
        lbAbccab.BackColor = Color.Salmon
      Else
        lbAbccab.BackColor = Color.Transparent
      End If

      Dim dttAbi As New DataTable
      oMenu.ValCodiceDb(NTSCStr(dsAbic.Tables("ABICAB").Rows(0)!AbcAbi), DittaCorrente, "ABI", "N", , dttAbi)
      If dttAbi.Rows.Count = 0 OrElse NTSCInt(dttAbi.Rows(0)!AbiAbichk) = 1 Then
        lbAbcabi.BackColor = Color.Salmon
      Else
        lbAbcabi.BackColor = Color.Transparent
      End If


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckXx_abcabichk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckXx_abcabichk.CheckedChanged
    Try
      ColoraAbiCabSoppressi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
End Class

