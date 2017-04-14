Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGPDON
  Private Moduli_P As Integer = bsModMG + bsModVE
  Private ModuliExt_P As Integer = bsModExtMGE
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

  Public oClePdon As CLEMGPDON
  Public dsPdon As DataSet
  Public oCallParams As CLE__CLDP
  Public dcPdon As BindingSource = New BindingSource

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
  Public WithEvents lbTb_codpdon As NTSInformatica.NTSLabel
  Public WithEvents edTb_codpdon As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_despdon As NTSInformatica.NTSLabel
  Public WithEvents edTb_despdon As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_rica_1 As NTSInformatica.NTSLabel
  Public WithEvents edTb_rica_1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_rica_2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_rica_2 As NTSInformatica.NTSLabel
  Public WithEvents edTb_rica_3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_rica_4 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_rica_3 As NTSInformatica.NTSLabel
  Public WithEvents edTb_rica_5 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_rica_6 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_rica_4 As NTSInformatica.NTSLabel
  Public WithEvents edTb_rica_7 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_rica_8 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_rica_5 As NTSInformatica.NTSLabel
  Public WithEvents edTb_arro_1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_arro_2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_rica_6 As NTSInformatica.NTSLabel
  Public WithEvents edTb_arro_3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_arro_4 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_rica_7 As NTSInformatica.NTSLabel
  Public WithEvents edTb_arro_5 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_arro_6 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_rica_8 As NTSInformatica.NTSLabel
  Public WithEvents edTb_arro_7 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_arro_8 As NTSInformatica.NTSTextBoxNum

  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGPDON))
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
    Me.lbTb_codpdon = New NTSInformatica.NTSLabel
    Me.edTb_codpdon = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_despdon = New NTSInformatica.NTSLabel
    Me.edTb_despdon = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_rica_1 = New NTSInformatica.NTSLabel
    Me.edTb_rica_1 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_rica_2 = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_rica_2 = New NTSInformatica.NTSLabel
    Me.edTb_rica_3 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_rica_4 = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_rica_3 = New NTSInformatica.NTSLabel
    Me.edTb_rica_5 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_rica_6 = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_rica_4 = New NTSInformatica.NTSLabel
    Me.edTb_rica_7 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_rica_8 = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_rica_5 = New NTSInformatica.NTSLabel
    Me.edTb_arro_1 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_arro_2 = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_rica_6 = New NTSInformatica.NTSLabel
    Me.edTb_arro_3 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_arro_4 = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_rica_7 = New NTSInformatica.NTSLabel
    Me.edTb_arro_5 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_arro_6 = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_rica_8 = New NTSInformatica.NTSLabel
    Me.edTb_arro_7 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_arro_8 = New NTSInformatica.NTSTextBoxNum
    Me.lbIncremento = New NTSInformatica.NTSLabel
    Me.lbArrotondamento = New NTSInformatica.NTSLabel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codpdon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_despdon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_rica_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_rica_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_rica_3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_rica_4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_rica_5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_rica_6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_rica_7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_rica_8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_arro_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_arro_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_arro_3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_arro_4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_arro_5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_arro_6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_arro_7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_arro_8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbUltimo, Me.tlbStrumenti, Me.tlbImpostaStampante})
    Me.NtsBarManager1.MaxItemId = 26
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'lbTb_codpdon
    '
    Me.lbTb_codpdon.AutoSize = True
    Me.lbTb_codpdon.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_codpdon.Location = New System.Drawing.Point(8, 46)
    Me.lbTb_codpdon.Name = "lbTb_codpdon"
    Me.lbTb_codpdon.NTSDbField = ""
    Me.lbTb_codpdon.Size = New System.Drawing.Size(92, 13)
    Me.lbTb_codpdon.TabIndex = 10
    Me.lbTb_codpdon.Text = "Cod. Relaz. Listini"
    '
    'edTb_codpdon
    '
    Me.edTb_codpdon.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codpdon.EditValue = "0"
    Me.edTb_codpdon.Location = New System.Drawing.Point(117, 46)
    Me.edTb_codpdon.Name = "edTb_codpdon"
    Me.edTb_codpdon.NTSDbField = ""
    Me.edTb_codpdon.NTSFormat = "0"
    Me.edTb_codpdon.NTSForzaVisZoom = False
    Me.edTb_codpdon.NTSOldValue = ""
    Me.edTb_codpdon.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codpdon.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codpdon.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codpdon.Properties.MaxLength = 65536
    Me.edTb_codpdon.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codpdon.Size = New System.Drawing.Size(54, 20)
    Me.edTb_codpdon.TabIndex = 500
    '
    'lbTb_despdon
    '
    Me.lbTb_despdon.AutoSize = True
    Me.lbTb_despdon.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_despdon.Location = New System.Drawing.Point(8, 72)
    Me.lbTb_despdon.Name = "lbTb_despdon"
    Me.lbTb_despdon.NTSDbField = ""
    Me.lbTb_despdon.Size = New System.Drawing.Size(89, 13)
    Me.lbTb_despdon.TabIndex = 11
    Me.lbTb_despdon.Text = "Descr. Rel. Listini"
    '
    'edTb_despdon
    '
    Me.edTb_despdon.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTb_despdon.Location = New System.Drawing.Point(117, 72)
    Me.edTb_despdon.Name = "edTb_despdon"
    Me.edTb_despdon.NTSDbField = ""
    Me.edTb_despdon.NTSForzaVisZoom = False
    Me.edTb_despdon.NTSOldValue = ""
    Me.edTb_despdon.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_despdon.Properties.MaxLength = 65536
    Me.edTb_despdon.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_despdon.Size = New System.Drawing.Size(278, 20)
    Me.edTb_despdon.TabIndex = 501
    '
    'lbTb_rica_1
    '
    Me.lbTb_rica_1.AutoSize = True
    Me.lbTb_rica_1.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_rica_1.Location = New System.Drawing.Point(19, 131)
    Me.lbTb_rica_1.Name = "lbTb_rica_1"
    Me.lbTb_rica_1.NTSDbField = ""
    Me.lbTb_rica_1.Size = New System.Drawing.Size(46, 13)
    Me.lbTb_rica_1.TabIndex = 12
    Me.lbTb_rica_1.Text = "Listino 1"
    '
    'edTb_rica_1
    '
    Me.edTb_rica_1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_rica_1.EditValue = "0"
    Me.edTb_rica_1.Location = New System.Drawing.Point(117, 128)
    Me.edTb_rica_1.Name = "edTb_rica_1"
    Me.edTb_rica_1.NTSDbField = ""
    Me.edTb_rica_1.NTSFormat = "0"
    Me.edTb_rica_1.NTSForzaVisZoom = False
    Me.edTb_rica_1.NTSOldValue = ""
    Me.edTb_rica_1.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_rica_1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_rica_1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_rica_1.Properties.MaxLength = 65536
    Me.edTb_rica_1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_rica_1.Size = New System.Drawing.Size(81, 20)
    Me.edTb_rica_1.TabIndex = 502
    '
    'edTb_rica_2
    '
    Me.edTb_rica_2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_rica_2.EditValue = "0"
    Me.edTb_rica_2.Location = New System.Drawing.Point(117, 154)
    Me.edTb_rica_2.Name = "edTb_rica_2"
    Me.edTb_rica_2.NTSDbField = ""
    Me.edTb_rica_2.NTSFormat = "0"
    Me.edTb_rica_2.NTSForzaVisZoom = False
    Me.edTb_rica_2.NTSOldValue = ""
    Me.edTb_rica_2.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_rica_2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_rica_2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_rica_2.Properties.MaxLength = 65536
    Me.edTb_rica_2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_rica_2.Size = New System.Drawing.Size(81, 20)
    Me.edTb_rica_2.TabIndex = 503
    '
    'lbTb_rica_2
    '
    Me.lbTb_rica_2.AutoSize = True
    Me.lbTb_rica_2.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_rica_2.Location = New System.Drawing.Point(19, 157)
    Me.lbTb_rica_2.Name = "lbTb_rica_2"
    Me.lbTb_rica_2.NTSDbField = ""
    Me.lbTb_rica_2.Size = New System.Drawing.Size(46, 13)
    Me.lbTb_rica_2.TabIndex = 14
    Me.lbTb_rica_2.Text = "Listino 2"
    '
    'edTb_rica_3
    '
    Me.edTb_rica_3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_rica_3.EditValue = "0"
    Me.edTb_rica_3.Location = New System.Drawing.Point(117, 180)
    Me.edTb_rica_3.Name = "edTb_rica_3"
    Me.edTb_rica_3.NTSDbField = ""
    Me.edTb_rica_3.NTSFormat = "0"
    Me.edTb_rica_3.NTSForzaVisZoom = False
    Me.edTb_rica_3.NTSOldValue = ""
    Me.edTb_rica_3.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_rica_3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_rica_3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_rica_3.Properties.MaxLength = 65536
    Me.edTb_rica_3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_rica_3.Size = New System.Drawing.Size(81, 20)
    Me.edTb_rica_3.TabIndex = 504
    '
    'edTb_rica_4
    '
    Me.edTb_rica_4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_rica_4.EditValue = "0"
    Me.edTb_rica_4.Location = New System.Drawing.Point(117, 206)
    Me.edTb_rica_4.Name = "edTb_rica_4"
    Me.edTb_rica_4.NTSDbField = ""
    Me.edTb_rica_4.NTSFormat = "0"
    Me.edTb_rica_4.NTSForzaVisZoom = False
    Me.edTb_rica_4.NTSOldValue = ""
    Me.edTb_rica_4.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_rica_4.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_rica_4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_rica_4.Properties.MaxLength = 65536
    Me.edTb_rica_4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_rica_4.Size = New System.Drawing.Size(81, 20)
    Me.edTb_rica_4.TabIndex = 505
    '
    'lbTb_rica_3
    '
    Me.lbTb_rica_3.AutoSize = True
    Me.lbTb_rica_3.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_rica_3.Location = New System.Drawing.Point(19, 183)
    Me.lbTb_rica_3.Name = "lbTb_rica_3"
    Me.lbTb_rica_3.NTSDbField = ""
    Me.lbTb_rica_3.Size = New System.Drawing.Size(46, 13)
    Me.lbTb_rica_3.TabIndex = 16
    Me.lbTb_rica_3.Text = "Listino 3"
    '
    'edTb_rica_5
    '
    Me.edTb_rica_5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_rica_5.EditValue = "0"
    Me.edTb_rica_5.Location = New System.Drawing.Point(117, 232)
    Me.edTb_rica_5.Name = "edTb_rica_5"
    Me.edTb_rica_5.NTSDbField = ""
    Me.edTb_rica_5.NTSFormat = "0"
    Me.edTb_rica_5.NTSForzaVisZoom = False
    Me.edTb_rica_5.NTSOldValue = ""
    Me.edTb_rica_5.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_rica_5.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_rica_5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_rica_5.Properties.MaxLength = 65536
    Me.edTb_rica_5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_rica_5.Size = New System.Drawing.Size(81, 20)
    Me.edTb_rica_5.TabIndex = 506
    '
    'edTb_rica_6
    '
    Me.edTb_rica_6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_rica_6.EditValue = "0"
    Me.edTb_rica_6.Location = New System.Drawing.Point(117, 258)
    Me.edTb_rica_6.Name = "edTb_rica_6"
    Me.edTb_rica_6.NTSDbField = ""
    Me.edTb_rica_6.NTSFormat = "0"
    Me.edTb_rica_6.NTSForzaVisZoom = False
    Me.edTb_rica_6.NTSOldValue = ""
    Me.edTb_rica_6.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_rica_6.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_rica_6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_rica_6.Properties.MaxLength = 65536
    Me.edTb_rica_6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_rica_6.Size = New System.Drawing.Size(81, 20)
    Me.edTb_rica_6.TabIndex = 507
    '
    'lbTb_rica_4
    '
    Me.lbTb_rica_4.AutoSize = True
    Me.lbTb_rica_4.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_rica_4.Location = New System.Drawing.Point(19, 209)
    Me.lbTb_rica_4.Name = "lbTb_rica_4"
    Me.lbTb_rica_4.NTSDbField = ""
    Me.lbTb_rica_4.Size = New System.Drawing.Size(46, 13)
    Me.lbTb_rica_4.TabIndex = 18
    Me.lbTb_rica_4.Text = "Listino 4"
    '
    'edTb_rica_7
    '
    Me.edTb_rica_7.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_rica_7.EditValue = "0"
    Me.edTb_rica_7.Location = New System.Drawing.Point(117, 284)
    Me.edTb_rica_7.Name = "edTb_rica_7"
    Me.edTb_rica_7.NTSDbField = ""
    Me.edTb_rica_7.NTSFormat = "0"
    Me.edTb_rica_7.NTSForzaVisZoom = False
    Me.edTb_rica_7.NTSOldValue = ""
    Me.edTb_rica_7.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_rica_7.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_rica_7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_rica_7.Properties.MaxLength = 65536
    Me.edTb_rica_7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_rica_7.Size = New System.Drawing.Size(81, 20)
    Me.edTb_rica_7.TabIndex = 508
    '
    'edTb_rica_8
    '
    Me.edTb_rica_8.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_rica_8.EditValue = "0"
    Me.edTb_rica_8.Location = New System.Drawing.Point(117, 310)
    Me.edTb_rica_8.Name = "edTb_rica_8"
    Me.edTb_rica_8.NTSDbField = ""
    Me.edTb_rica_8.NTSFormat = "0"
    Me.edTb_rica_8.NTSForzaVisZoom = False
    Me.edTb_rica_8.NTSOldValue = ""
    Me.edTb_rica_8.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_rica_8.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_rica_8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_rica_8.Properties.MaxLength = 65536
    Me.edTb_rica_8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_rica_8.Size = New System.Drawing.Size(81, 20)
    Me.edTb_rica_8.TabIndex = 509
    '
    'lbTb_rica_5
    '
    Me.lbTb_rica_5.AutoSize = True
    Me.lbTb_rica_5.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_rica_5.Location = New System.Drawing.Point(19, 235)
    Me.lbTb_rica_5.Name = "lbTb_rica_5"
    Me.lbTb_rica_5.NTSDbField = ""
    Me.lbTb_rica_5.Size = New System.Drawing.Size(46, 13)
    Me.lbTb_rica_5.TabIndex = 20
    Me.lbTb_rica_5.Text = "Listino 5"
    '
    'edTb_arro_1
    '
    Me.edTb_arro_1.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTb_arro_1.EditValue = "0"
    Me.edTb_arro_1.Location = New System.Drawing.Point(204, 128)
    Me.edTb_arro_1.Name = "edTb_arro_1"
    Me.edTb_arro_1.NTSDbField = ""
    Me.edTb_arro_1.NTSFormat = "0"
    Me.edTb_arro_1.NTSForzaVisZoom = False
    Me.edTb_arro_1.NTSOldValue = ""
    Me.edTb_arro_1.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_arro_1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_arro_1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_arro_1.Properties.MaxLength = 65536
    Me.edTb_arro_1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_arro_1.Size = New System.Drawing.Size(103, 20)
    Me.edTb_arro_1.TabIndex = 510
    '
    'edTb_arro_2
    '
    Me.edTb_arro_2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_arro_2.EditValue = "0"
    Me.edTb_arro_2.Location = New System.Drawing.Point(204, 154)
    Me.edTb_arro_2.Name = "edTb_arro_2"
    Me.edTb_arro_2.NTSDbField = ""
    Me.edTb_arro_2.NTSFormat = "0"
    Me.edTb_arro_2.NTSForzaVisZoom = False
    Me.edTb_arro_2.NTSOldValue = ""
    Me.edTb_arro_2.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_arro_2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_arro_2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_arro_2.Properties.MaxLength = 65536
    Me.edTb_arro_2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_arro_2.Size = New System.Drawing.Size(103, 20)
    Me.edTb_arro_2.TabIndex = 511
    '
    'lbTb_rica_6
    '
    Me.lbTb_rica_6.AutoSize = True
    Me.lbTb_rica_6.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_rica_6.Location = New System.Drawing.Point(19, 261)
    Me.lbTb_rica_6.Name = "lbTb_rica_6"
    Me.lbTb_rica_6.NTSDbField = ""
    Me.lbTb_rica_6.Size = New System.Drawing.Size(46, 13)
    Me.lbTb_rica_6.TabIndex = 22
    Me.lbTb_rica_6.Text = "Listino 6"
    '
    'edTb_arro_3
    '
    Me.edTb_arro_3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_arro_3.EditValue = "0"
    Me.edTb_arro_3.Location = New System.Drawing.Point(204, 180)
    Me.edTb_arro_3.Name = "edTb_arro_3"
    Me.edTb_arro_3.NTSDbField = ""
    Me.edTb_arro_3.NTSFormat = "0"
    Me.edTb_arro_3.NTSForzaVisZoom = False
    Me.edTb_arro_3.NTSOldValue = ""
    Me.edTb_arro_3.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_arro_3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_arro_3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_arro_3.Properties.MaxLength = 65536
    Me.edTb_arro_3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_arro_3.Size = New System.Drawing.Size(103, 20)
    Me.edTb_arro_3.TabIndex = 512
    '
    'edTb_arro_4
    '
    Me.edTb_arro_4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_arro_4.EditValue = "0"
    Me.edTb_arro_4.Location = New System.Drawing.Point(204, 206)
    Me.edTb_arro_4.Name = "edTb_arro_4"
    Me.edTb_arro_4.NTSDbField = ""
    Me.edTb_arro_4.NTSFormat = "0"
    Me.edTb_arro_4.NTSForzaVisZoom = False
    Me.edTb_arro_4.NTSOldValue = ""
    Me.edTb_arro_4.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_arro_4.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_arro_4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_arro_4.Properties.MaxLength = 65536
    Me.edTb_arro_4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_arro_4.Size = New System.Drawing.Size(103, 20)
    Me.edTb_arro_4.TabIndex = 513
    '
    'lbTb_rica_7
    '
    Me.lbTb_rica_7.AutoSize = True
    Me.lbTb_rica_7.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_rica_7.Location = New System.Drawing.Point(19, 287)
    Me.lbTb_rica_7.Name = "lbTb_rica_7"
    Me.lbTb_rica_7.NTSDbField = ""
    Me.lbTb_rica_7.Size = New System.Drawing.Size(46, 13)
    Me.lbTb_rica_7.TabIndex = 24
    Me.lbTb_rica_7.Text = "Listino 7"
    '
    'edTb_arro_5
    '
    Me.edTb_arro_5.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edTb_arro_5.EditValue = "0"
    Me.edTb_arro_5.Location = New System.Drawing.Point(204, 232)
    Me.edTb_arro_5.Name = "edTb_arro_5"
    Me.edTb_arro_5.NTSDbField = ""
    Me.edTb_arro_5.NTSFormat = "0"
    Me.edTb_arro_5.NTSForzaVisZoom = False
    Me.edTb_arro_5.NTSOldValue = ""
    Me.edTb_arro_5.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_arro_5.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_arro_5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_arro_5.Properties.MaxLength = 65536
    Me.edTb_arro_5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_arro_5.Size = New System.Drawing.Size(103, 20)
    Me.edTb_arro_5.TabIndex = 514
    '
    'edTb_arro_6
    '
    Me.edTb_arro_6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_arro_6.EditValue = "0"
    Me.edTb_arro_6.Location = New System.Drawing.Point(204, 258)
    Me.edTb_arro_6.Name = "edTb_arro_6"
    Me.edTb_arro_6.NTSDbField = ""
    Me.edTb_arro_6.NTSFormat = "0"
    Me.edTb_arro_6.NTSForzaVisZoom = False
    Me.edTb_arro_6.NTSOldValue = ""
    Me.edTb_arro_6.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_arro_6.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_arro_6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_arro_6.Properties.MaxLength = 65536
    Me.edTb_arro_6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_arro_6.Size = New System.Drawing.Size(103, 20)
    Me.edTb_arro_6.TabIndex = 515
    '
    'lbTb_rica_8
    '
    Me.lbTb_rica_8.AutoSize = True
    Me.lbTb_rica_8.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_rica_8.Location = New System.Drawing.Point(19, 313)
    Me.lbTb_rica_8.Name = "lbTb_rica_8"
    Me.lbTb_rica_8.NTSDbField = ""
    Me.lbTb_rica_8.Size = New System.Drawing.Size(46, 13)
    Me.lbTb_rica_8.TabIndex = 26
    Me.lbTb_rica_8.Text = "Listino 8"
    '
    'edTb_arro_7
    '
    Me.edTb_arro_7.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edTb_arro_7.EditValue = "0"
    Me.edTb_arro_7.Location = New System.Drawing.Point(204, 284)
    Me.edTb_arro_7.Name = "edTb_arro_7"
    Me.edTb_arro_7.NTSDbField = ""
    Me.edTb_arro_7.NTSFormat = "0"
    Me.edTb_arro_7.NTSForzaVisZoom = False
    Me.edTb_arro_7.NTSOldValue = ""
    Me.edTb_arro_7.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_arro_7.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_arro_7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_arro_7.Properties.MaxLength = 65536
    Me.edTb_arro_7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_arro_7.Size = New System.Drawing.Size(103, 20)
    Me.edTb_arro_7.TabIndex = 516
    '
    'edTb_arro_8
    '
    Me.edTb_arro_8.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_arro_8.EditValue = "0"
    Me.edTb_arro_8.Location = New System.Drawing.Point(204, 310)
    Me.edTb_arro_8.Name = "edTb_arro_8"
    Me.edTb_arro_8.NTSDbField = ""
    Me.edTb_arro_8.NTSFormat = "0"
    Me.edTb_arro_8.NTSForzaVisZoom = False
    Me.edTb_arro_8.NTSOldValue = ""
    Me.edTb_arro_8.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_arro_8.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_arro_8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_arro_8.Properties.MaxLength = 65536
    Me.edTb_arro_8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_arro_8.Size = New System.Drawing.Size(103, 20)
    Me.edTb_arro_8.TabIndex = 517
    '
    'lbIncremento
    '
    Me.lbIncremento.AutoSize = True
    Me.lbIncremento.BackColor = System.Drawing.Color.Transparent
    Me.lbIncremento.Location = New System.Drawing.Point(114, 103)
    Me.lbIncremento.Name = "lbIncremento"
    Me.lbIncremento.NTSDbField = ""
    Me.lbIncremento.Size = New System.Drawing.Size(76, 13)
    Me.lbIncremento.TabIndex = 518
    Me.lbIncremento.Text = "% Incremento"
    '
    'lbArrotondamento
    '
    Me.lbArrotondamento.AutoSize = True
    Me.lbArrotondamento.BackColor = System.Drawing.Color.Transparent
    Me.lbArrotondamento.Location = New System.Drawing.Point(201, 103)
    Me.lbArrotondamento.Name = "lbArrotondamento"
    Me.lbArrotondamento.NTSDbField = ""
    Me.lbArrotondamento.Size = New System.Drawing.Size(86, 13)
    Me.lbArrotondamento.TabIndex = 519
    Me.lbArrotondamento.Text = "Arrotondamento"
    '
    'FRMMGPDON
    '
    Me.ClientSize = New System.Drawing.Size(437, 350)
    Me.Controls.Add(Me.lbArrotondamento)
    Me.Controls.Add(Me.lbIncremento)
    Me.Controls.Add(Me.lbTb_rica_1)
    Me.Controls.Add(Me.edTb_rica_2)
    Me.Controls.Add(Me.lbTb_despdon)
    Me.Controls.Add(Me.edTb_rica_4)
    Me.Controls.Add(Me.edTb_rica_1)
    Me.Controls.Add(Me.lbTb_codpdon)
    Me.Controls.Add(Me.edTb_rica_6)
    Me.Controls.Add(Me.edTb_despdon)
    Me.Controls.Add(Me.lbTb_rica_2)
    Me.Controls.Add(Me.edTb_rica_8)
    Me.Controls.Add(Me.edTb_rica_3)
    Me.Controls.Add(Me.edTb_arro_2)
    Me.Controls.Add(Me.edTb_codpdon)
    Me.Controls.Add(Me.edTb_arro_7)
    Me.Controls.Add(Me.edTb_arro_4)
    Me.Controls.Add(Me.lbTb_rica_8)
    Me.Controls.Add(Me.lbTb_rica_3)
    Me.Controls.Add(Me.edTb_arro_6)
    Me.Controls.Add(Me.edTb_arro_5)
    Me.Controls.Add(Me.edTb_rica_5)
    Me.Controls.Add(Me.edTb_arro_8)
    Me.Controls.Add(Me.lbTb_rica_7)
    Me.Controls.Add(Me.edTb_arro_3)
    Me.Controls.Add(Me.lbTb_rica_6)
    Me.Controls.Add(Me.lbTb_rica_4)
    Me.Controls.Add(Me.edTb_arro_1)
    Me.Controls.Add(Me.edTb_rica_7)
    Me.Controls.Add(Me.lbTb_rica_5)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMMGPDON"
    Me.Text = "RELAZIONI LISTINI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codpdon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_despdon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_rica_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_rica_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_rica_3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_rica_4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_rica_5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_rica_6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_rica_7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_rica_8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_arro_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_arro_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_arro_3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_arro_4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_arro_5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_arro_6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_arro_7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_arro_8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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

    '------------------------------------------------
    'creo e attivo l'entity e inizializzo la funzione che dovrÃ  rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGPDON", "BEMGPDON", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128491901516750590, "ERRORE in fase di creazione Entity:" & vbCrLf) & strErr)
      Return False
    End If
    oClePdon = CType(oTmp, CLEMGPDON)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGPDON", strRemoteServer, strRemotePort)
    AddHandler oClePdon.RemoteEvent, AddressOf GestisciEventiEntity
    If oClePdon.Init(oApp, oScript, oMenu.oCleComm, "TABPDON", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

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
      edTb_codpdon.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128777124043702838, "Cod. Relaz. Listini"), tabpdon)
      edTb_despdon.NTSSetParam(oMenu, oApp.Tr(Me, 128777124060738993, "Descr. Rel. Listini"), 30, True)
      edTb_rica_1.NTSSetParam(oMenu, oApp.Tr(Me, 128777124075587018, "Listino 1"), oApp.FormatImporti, 6, -100, 9999)
      edTb_rica_2.NTSSetParam(oMenu, oApp.Tr(Me, 128777124088090618, "Listino 2"), oApp.FormatImporti, 6, -100, 9999)
      edTb_rica_3.NTSSetParam(oMenu, oApp.Tr(Me, 128777124105126773, "Listino 3"), oApp.FormatImporti, 6, -100, 9999)
      edTb_rica_4.NTSSetParam(oMenu, oApp.Tr(Me, 128777124121068863, "Listino 4"), oApp.FormatImporti, 6, -100, 9999)
      edTb_rica_5.NTSSetParam(oMenu, oApp.Tr(Me, 128777124137792428, "Listino 5"), oApp.FormatImporti, 6, -100, 9999)
      edTb_rica_6.NTSSetParam(oMenu, oApp.Tr(Me, 128777124170458083, "Listino 6"), oApp.FormatImporti, 6, -100, 9999)
      edTb_rica_7.NTSSetParam(oMenu, oApp.Tr(Me, 128777124199372658, "Listino 7"), oApp.FormatImporti, 6, -100, 9999)
      edTb_rica_8.NTSSetParam(oMenu, oApp.Tr(Me, 128777124213908093, "Listino 8"), oApp.FormatImporti, 6, -100, 9999)
      edTb_arro_1.NTSSetParam(oMenu, oApp.Tr(Me, 128777124233132378, "Listino 1"), oClePdon.strFormatCampo, 20, 0, 999999)
      edTb_arro_2.NTSSetParam(oMenu, oApp.Tr(Me, 128777124248761878, "Listino 2"), oClePdon.strFormatCampo, 20, 0, 999999)
      edTb_arro_3.NTSSetParam(oMenu, oApp.Tr(Me, 128777124263453608, "Listino 3"), oClePdon.strFormatCampo, 20, 0, 999999)
      edTb_arro_4.NTSSetParam(oMenu, oApp.Tr(Me, 128777124278614223, "Listino 4"), oClePdon.strFormatCampo, 20, 0, 999999)
      edTb_arro_5.NTSSetParam(oMenu, oApp.Tr(Me, 128777124293618543, "Listino 5"), oClePdon.strFormatCampo, 20, 0, 999999)
      edTb_arro_6.NTSSetParam(oMenu, oApp.Tr(Me, 128777124310810993, "Listino 6"), oClePdon.strFormatCampo, 20, 0, 999999)
      edTb_arro_7.NTSSetParam(oMenu, oApp.Tr(Me, 128777124326753083, "Listino 7"), oClePdon.strFormatCampo, 20, 0, 999999)
      edTb_arro_8.NTSSetParam(oMenu, oApp.Tr(Me, 128777124341132223, "Listino 8"), oClePdon.strFormatCampo, 20, 0, 999999)

      edTb_codpdon.NTSSetRichiesto()
      edTb_despdon.NTSSetRichiesto()

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
      edTb_codpdon.NTSDbField = "TABPDON.tb_codpdon"
      edTb_despdon.NTSDbField = "TABPDON.tb_despdon"
      edTb_rica_1.NTSDbField = "TABPDON.tb_rica_1"
      edTb_rica_2.NTSDbField = "TABPDON.tb_rica_2"
      edTb_rica_3.NTSDbField = "TABPDON.tb_rica_3"
      edTb_rica_4.NTSDbField = "TABPDON.tb_rica_4"
      edTb_rica_5.NTSDbField = "TABPDON.tb_rica_5"
      edTb_rica_6.NTSDbField = "TABPDON.tb_rica_6"
      edTb_rica_7.NTSDbField = "TABPDON.tb_rica_7"
      edTb_rica_8.NTSDbField = "TABPDON.tb_rica_8"
      edTb_arro_1.NTSDbField = "TABPDON.tb_arro_1"
      edTb_arro_2.NTSDbField = "TABPDON.tb_arro_2"
      edTb_arro_3.NTSDbField = "TABPDON.tb_arro_3"
      edTb_arro_4.NTSDbField = "TABPDON.tb_arro_4"
      edTb_arro_5.NTSDbField = "TABPDON.tb_arro_5"
      edTb_arro_6.NTSDbField = "TABPDON.tb_arro_6"
      edTb_arro_7.NTSDbField = "TABPDON.tb_arro_7"
      edTb_arro_8.NTSDbField = "TABPDON.tb_arro_8"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcPdon, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRMMGPDON_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim i As Integer = 0
    Try
      oClePdon.TrovaDecimali()

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      If Not oClePdon.Apri(DittaCorrente, dsPdon) Then Me.Close()
      dcPdon.DataSource = dsPdon.Tables("TABPDON")
      dsPdon.AcceptChanges()

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      If dsPdon.Tables("TABPDON").Rows.Count = 0 Then
        tlbNuovo_ItemClick(tlbNuovo, Nothing)
      Else
        edTb_codpdon.Enabled = False
      End If

      dcPdon.ResetBindings(False)
      dcPdon.MoveFirst()
      '--------------------------------------------
      'sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
      If Not oCallParams Is Nothing Then
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
          If dsPdon.Tables("TABPDON").Rows.Count = 1 And dsPdon.Tables("TABPDON").Rows(0).RowState = DataRowState.Added Then
            'sono già in record nuovo perchè la tabella è vuota: non devo fare nulla
          Else
            tlbNuovo_ItemClick(Me, Nothing)
          End If
        ElseIf Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) <> "" Then
          For i = 0 To dcPdon.List.Count - 1
            If CType(dcPdon.Item(i), DataRowView)!tb_codpdon.ToString = Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) Then
              dcPdon.Position = i
              Exit For
            End If
          Next
        End If
      End If    'If Not oCallParams Is Nothing Then

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGPDON_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub FRMMGPDON_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcPdon.Dispose()
      dsPdon.Dispose()
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
      oClePdon.Nuovo()
      dcPdon.MoveLast()

      Select Case oMenu.TrovaNdecSuPrzUn(0)
        Case 0
          edTb_arro_1.NTSTextDB = "1"
          edTb_arro_2.NTSTextDB = "1"
          edTb_arro_3.NTSTextDB = "1"
          edTb_arro_4.NTSTextDB = "1"
          edTb_arro_5.NTSTextDB = "1"
          edTb_arro_6.NTSTextDB = "1"
          edTb_arro_7.NTSTextDB = "1"
          edTb_arro_8.NTSTextDB = "1"
        Case 1
          edTb_arro_1.NTSTextDB = "0,1"
          edTb_arro_2.NTSTextDB = "0,1"
          edTb_arro_3.NTSTextDB = "0,1"
          edTb_arro_4.NTSTextDB = "0,1"
          edTb_arro_5.NTSTextDB = "0,1"
          edTb_arro_6.NTSTextDB = "0,1"
          edTb_arro_7.NTSTextDB = "0,1"
          edTb_arro_8.NTSTextDB = "0,1"
        Case 2
          edTb_arro_1.NTSTextDB = "0,01"
          edTb_arro_2.NTSTextDB = "0,01"
          edTb_arro_3.NTSTextDB = "0,01"
          edTb_arro_4.NTSTextDB = "0,01"
          edTb_arro_5.NTSTextDB = "0,01"
          edTb_arro_6.NTSTextDB = "0,01"
          edTb_arro_7.NTSTextDB = "0,01"
          edTb_arro_8.NTSTextDB = "0,01"
        Case 3
          edTb_arro_1.NTSTextDB = "0,001"
          edTb_arro_2.NTSTextDB = "0,001"
          edTb_arro_3.NTSTextDB = "0,001"
          edTb_arro_4.NTSTextDB = "0,001"
          edTb_arro_5.NTSTextDB = "0,001"
          edTb_arro_6.NTSTextDB = "0,001"
          edTb_arro_7.NTSTextDB = "0,001"
          edTb_arro_8.NTSTextDB = "0,001"
        Case 4
          edTb_arro_1.NTSTextDB = "0,0001"
          edTb_arro_2.NTSTextDB = "0,0001"
          edTb_arro_3.NTSTextDB = "0,0001"
          edTb_arro_4.NTSTextDB = "0,0001"
          edTb_arro_5.NTSTextDB = "0,0001"
          edTb_arro_6.NTSTextDB = "0,0001"
          edTb_arro_7.NTSTextDB = "0,0001"
          edTb_arro_8.NTSTextDB = "0,0001"
      End Select

      edTb_codpdon.Enabled = True
      edTb_codpdon.Focus()



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
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128491901516906811, "Confermi la cancellazione?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes

          If dsPdon.Tables("TABPDON").Rows.Count = 1 Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          dcPdon.RemoveAt(dcPdon.Position)
          oClePdon.Salva(True)

          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcPdon, Me)
            bRemovBinding = False
            edTb_codpdon.Enabled = True
          Else
            edTb_codpdon.Enabled = False
          End If
          Return
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcPdon, Me)
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
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128491901517063032, "Ripristinare le modifiche apportate?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          oClePdon.Ripristina(dcPdon.Position, dcPdon.Filter)
          If NTSCInt(edTb_codpdon.Text) = 0 Then
            edTb_codpdon.Enabled = True
          Else
            edTb_codpdon.Enabled = False
          End If
      End Select

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      'per eventuali altri controlli caricati al volo
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      If edTb_despdon.Focused Then
        Salva()
        '  '--------------------------------------------
        '  'zoom tabpdon
        SetFastZoom(edTb_codpdon.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edTb_codpdon.Text
        NTSZOOM.ZoomStrIn("ZOOMTABPDON", DittaCorrente, oParam)
        posizionaAfterZoom(NTSZOOM.strIn)
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
    dcPdon.MoveFirst()
  End Sub

  Public Overridable Sub tlbPrecedente_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrecedente.ItemClick
    '-------------------------------------------------
    'vado sul record precedente
    If Not Salva() Then Return
    dcPdon.MovePrevious()
  End Sub

  Public Overridable Sub tlbSuccessivo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSuccessivo.ItemClick
    '-------------------------------------------------
    'vado sul record successivo
    If Not Salva() Then Return
    dcPdon.MoveNext()
  End Sub

  Public Overridable Sub tlbUltimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbUltimo.ItemClick
    '-------------------------------------------------
    'vado sull'ultimo record
    If Not Salva() Then Return
    dcPdon.MoveLast()
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

      If oClePdon.RecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128491901517219253, "Confermi il salvataggio?"))
        If dRes = System.Windows.Forms.DialogResult.No Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oClePdon.Salva(False) Then Return False
          If dsPdon.Tables("TABPDON").Rows.Count > 0 Then
            edTb_codpdon.Enabled = False
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

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer

    Try
      '--------------------------------------------------
      'preparo il motore di stampa
      strCrpe = "{tabpdon.codditt} = " & CStrSQL(DittaCorrente)
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGPDON", "Reports1", " ", 0, nDestin, "BSMGPDON.RPT", False, "RELAZIONI LISTINI", False)
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

  Public Overridable Sub posizionaAfterZoom(ByVal strZoom As String)
    Dim i As Integer
    Dim lPos As Integer
    Try
      lPos = dcPdon.Position
      dcPdon.MoveFirst()
      For i = 0 To dcPdon.Count - 1
        If dsPdon.Tables("TABPDON").Rows(dcPdon.Position)("tb_codpdon").ToString = strZoom Then
          Exit Sub
        End If
        dcPdon.MoveNext()
      Next
      dcPdon.Position = lPos
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class

