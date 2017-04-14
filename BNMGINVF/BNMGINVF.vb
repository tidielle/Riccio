Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGINVF
  Private Moduli_P As Integer = bsModMG
  Private ModuliExt_P As Integer = 0
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

  Public oCleInvf As CLEMGINVF
  Public oCallParams As CLE__CLDP
  Public strWhereArtico As String = "."
  Public lNumdocRPT As Integer = 0
  Public bModTCO As Boolean = False

  Private components As System.ComponentModel.IContainer

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGINVF))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbElabora = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
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
    Me.lbDatainv = New NTSInformatica.NTSLabel
    Me.edDatainv = New NTSInformatica.NTSTextBoxData
    Me.lbCodmagaLabel = New NTSInformatica.NTSLabel
    Me.edCodmaga = New NTSInformatica.NTSTextBoxNum
    Me.lbCodmaga = New NTSInformatica.NTSLabel
    Me.fmOrigine = New NTSInformatica.NTSGroupBox
    Me.lbCausInvLabel = New NTSInformatica.NTSLabel
    Me.lbCausInv = New NTSInformatica.NTSLabel
    Me.edCausInv = New NTSInformatica.NTSTextBoxNum
    Me.edAlistaOrig = New NTSInformatica.NTSTextBoxNum
    Me.edDalistaOrig = New NTSInformatica.NTSTextBoxNum
    Me.opDocmagOrig = New NTSInformatica.NTSRadioButton
    Me.opLselOrig = New NTSInformatica.NTSRadioButton
    Me.fmSelezione = New NTSInformatica.NTSGroupBox
    Me.cmdArtSel = New NTSInformatica.NTSButton
    Me.opDaDocOrig = New NTSInformatica.NTSRadioButton
    Me.opArtSel = New NTSInformatica.NTSRadioButton
    Me.edAlistaDest = New NTSInformatica.NTSTextBoxNum
    Me.edDalistaDest = New NTSInformatica.NTSTextBoxNum
    Me.opArtBloc = New NTSInformatica.NTSRadioButton
    Me.opLselDest = New NTSInformatica.NTSRadioButton
    Me.fmDoc = New NTSInformatica.NTSGroupBox
    Me.edAnno = New NTSInformatica.NTSTextBoxNum
    Me.ckElabora = New NTSInformatica.NTSCheckBox
    Me.edListino = New NTSInformatica.NTSTextBoxNum
    Me.cbTipVal = New NTSInformatica.NTSComboBox
    Me.lbTipVal = New NTSInformatica.NTSLabel
    Me.lbCodcausmeno = New NTSInformatica.NTSLabel
    Me.lbCodcauspiu = New NTSInformatica.NTSLabel
    Me.lbCodconto = New NTSInformatica.NTSLabel
    Me.lbCodtpbf = New NTSInformatica.NTSLabel
    Me.lbCodcausmenoLabel = New NTSInformatica.NTSLabel
    Me.lbCodcauspiuLabel = New NTSInformatica.NTSLabel
    Me.lbCodcontoLabel = New NTSInformatica.NTSLabel
    Me.lbCodtpbfLabel = New NTSInformatica.NTSLabel
    Me.edCodcausmeno = New NTSInformatica.NTSTextBoxNum
    Me.edCodcauspiu = New NTSInformatica.NTSTextBoxNum
    Me.edCodtpbf = New NTSInformatica.NTSTextBoxNum
    Me.edCodconto = New NTSInformatica.NTSTextBoxNum
    Me.edNumdoc = New NTSInformatica.NTSTextBoxNum
    Me.edSerie = New NTSInformatica.NTSTextBoxStr
    Me.lbEstremiDoc = New NTSInformatica.NTSLabel
    Me.lbStatus = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatainv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodmaga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmOrigine, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmOrigine.SuspendLayout()
    CType(Me.edCausInv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAlistaOrig.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDalistaOrig.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opDocmagOrig.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opLselOrig.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmSelezione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmSelezione.SuspendLayout()
    CType(Me.opDaDocOrig.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opArtSel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAlistaDest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDalistaDest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opArtBloc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opLselDest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmDoc, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmDoc.SuspendLayout()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckElabora.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edListino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipVal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodcausmeno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodcauspiu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodtpbf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumdoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbElabora, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti, Me.tlbImpostaStampante})
    Me.NtsBarManager1.MaxItemId = 17
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbElabora), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbElabora
    '
    Me.tlbElabora.Caption = "Elabora"
    Me.tlbElabora.Glyph = CType(resources.GetObject("tlbElabora.Glyph"), System.Drawing.Image)
    Me.tlbElabora.Id = 0
    Me.tlbElabora.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbElabora.Name = "tlbElabora"
    Me.tlbElabora.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 13
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 15
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.Id = 16
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 4
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 5
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 11
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 12
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'lbDatainv
    '
    Me.lbDatainv.AutoSize = True
    Me.lbDatainv.BackColor = System.Drawing.Color.Transparent
    Me.lbDatainv.Location = New System.Drawing.Point(12, 37)
    Me.lbDatainv.Name = "lbDatainv"
    Me.lbDatainv.NTSDbField = ""
    Me.lbDatainv.Size = New System.Drawing.Size(202, 13)
    Me.lbDatainv.TabIndex = 4
    Me.lbDatainv.Text = "Data inventario/documento di magazzino"
    Me.lbDatainv.Tooltip = ""
    Me.lbDatainv.UseMnemonic = False
    '
    'edDatainv
    '
    Me.edDatainv.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatainv.EditValue = "01/01/1900"
    Me.edDatainv.Location = New System.Drawing.Point(221, 34)
    Me.edDatainv.Name = "edDatainv"
    Me.edDatainv.NTSDbField = ""
    Me.edDatainv.NTSForzaVisZoom = False
    Me.edDatainv.NTSOldValue = ""
    Me.edDatainv.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatainv.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatainv.Properties.AutoHeight = False
    Me.edDatainv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatainv.Properties.MaxLength = 65536
    Me.edDatainv.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatainv.Size = New System.Drawing.Size(100, 20)
    Me.edDatainv.TabIndex = 5
    Me.edDatainv.TextData = New Date(1900, 1, 1, 0, 0, 0, 0)
    '
    'lbCodmagaLabel
    '
    Me.lbCodmagaLabel.AutoSize = True
    Me.lbCodmagaLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbCodmagaLabel.Location = New System.Drawing.Point(327, 37)
    Me.lbCodmagaLabel.Name = "lbCodmagaLabel"
    Me.lbCodmagaLabel.NTSDbField = ""
    Me.lbCodmagaLabel.Size = New System.Drawing.Size(57, 13)
    Me.lbCodmagaLabel.TabIndex = 6
    Me.lbCodmagaLabel.Text = "Magazzino"
    Me.lbCodmagaLabel.Tooltip = ""
    Me.lbCodmagaLabel.UseMnemonic = False
    '
    'edCodmaga
    '
    Me.edCodmaga.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodmaga.EditValue = "0"
    Me.edCodmaga.Location = New System.Drawing.Point(390, 34)
    Me.edCodmaga.Name = "edCodmaga"
    Me.edCodmaga.NTSDbField = ""
    Me.edCodmaga.NTSFormat = "0"
    Me.edCodmaga.NTSForzaVisZoom = False
    Me.edCodmaga.NTSOldValue = ""
    Me.edCodmaga.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodmaga.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodmaga.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodmaga.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodmaga.Properties.AutoHeight = False
    Me.edCodmaga.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodmaga.Properties.MaxLength = 65536
    Me.edCodmaga.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodmaga.Size = New System.Drawing.Size(51, 20)
    Me.edCodmaga.TabIndex = 7
    Me.edCodmaga.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCodmaga.TextInt = 0
    '
    'lbCodmaga
    '
    Me.lbCodmaga.BackColor = System.Drawing.Color.Transparent
    Me.lbCodmaga.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbCodmaga.Location = New System.Drawing.Point(447, 34)
    Me.lbCodmaga.Name = "lbCodmaga"
    Me.lbCodmaga.NTSDbField = ""
    Me.lbCodmaga.Size = New System.Drawing.Size(205, 20)
    Me.lbCodmaga.TabIndex = 8
    Me.lbCodmaga.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbCodmaga.Tooltip = ""
    Me.lbCodmaga.UseMnemonic = False
    '
    'fmOrigine
    '
    Me.fmOrigine.AllowDrop = True
    Me.fmOrigine.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmOrigine.Appearance.Options.UseBackColor = True
    Me.fmOrigine.Controls.Add(Me.lbCausInvLabel)
    Me.fmOrigine.Controls.Add(Me.lbCausInv)
    Me.fmOrigine.Controls.Add(Me.edCausInv)
    Me.fmOrigine.Controls.Add(Me.edAlistaOrig)
    Me.fmOrigine.Controls.Add(Me.edDalistaOrig)
    Me.fmOrigine.Controls.Add(Me.opDocmagOrig)
    Me.fmOrigine.Controls.Add(Me.opLselOrig)
    Me.fmOrigine.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmOrigine.Location = New System.Drawing.Point(6, 63)
    Me.fmOrigine.Name = "fmOrigine"
    Me.fmOrigine.Size = New System.Drawing.Size(654, 81)
    Me.fmOrigine.TabIndex = 9
    Me.fmOrigine.Text = "Origine inventario                                 (Articoli di cui si è riscontr" & _
        "ata la quantità effettiva)"
    '
    'lbCausInvLabel
    '
    Me.lbCausInvLabel.AutoSize = True
    Me.lbCausInvLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbCausInvLabel.Location = New System.Drawing.Point(261, 49)
    Me.lbCausInvLabel.Name = "lbCausInvLabel"
    Me.lbCausInvLabel.NTSDbField = ""
    Me.lbCausInvLabel.Size = New System.Drawing.Size(123, 13)
    Me.lbCausInvLabel.TabIndex = 12
    Me.lbCausInvLabel.Text = "Causale inventario fisico"
    Me.lbCausInvLabel.Tooltip = ""
    Me.lbCausInvLabel.UseMnemonic = False
    '
    'lbCausInv
    '
    Me.lbCausInv.BackColor = System.Drawing.Color.Transparent
    Me.lbCausInv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbCausInv.Location = New System.Drawing.Point(441, 45)
    Me.lbCausInv.Name = "lbCausInv"
    Me.lbCausInv.NTSDbField = ""
    Me.lbCausInv.Size = New System.Drawing.Size(205, 20)
    Me.lbCausInv.TabIndex = 11
    Me.lbCausInv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbCausInv.Tooltip = ""
    Me.lbCausInv.UseMnemonic = False
    '
    'edCausInv
    '
    Me.edCausInv.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCausInv.EditValue = "0"
    Me.edCausInv.Location = New System.Drawing.Point(384, 46)
    Me.edCausInv.Name = "edCausInv"
    Me.edCausInv.NTSDbField = ""
    Me.edCausInv.NTSFormat = "0"
    Me.edCausInv.NTSForzaVisZoom = False
    Me.edCausInv.NTSOldValue = ""
    Me.edCausInv.Properties.Appearance.Options.UseTextOptions = True
    Me.edCausInv.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCausInv.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCausInv.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCausInv.Properties.AutoHeight = False
    Me.edCausInv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCausInv.Properties.MaxLength = 65536
    Me.edCausInv.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCausInv.Size = New System.Drawing.Size(51, 20)
    Me.edCausInv.TabIndex = 10
    Me.edCausInv.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCausInv.TextInt = 0
    '
    'edAlistaOrig
    '
    Me.edAlistaOrig.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAlistaOrig.EditValue = "0"
    Me.edAlistaOrig.Location = New System.Drawing.Point(384, 23)
    Me.edAlistaOrig.Name = "edAlistaOrig"
    Me.edAlistaOrig.NTSDbField = ""
    Me.edAlistaOrig.NTSFormat = "0"
    Me.edAlistaOrig.NTSForzaVisZoom = False
    Me.edAlistaOrig.NTSOldValue = "0"
    Me.edAlistaOrig.Properties.Appearance.Options.UseTextOptions = True
    Me.edAlistaOrig.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAlistaOrig.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAlistaOrig.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAlistaOrig.Properties.AutoHeight = False
    Me.edAlistaOrig.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAlistaOrig.Properties.MaxLength = 65536
    Me.edAlistaOrig.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAlistaOrig.Size = New System.Drawing.Size(51, 20)
    Me.edAlistaOrig.TabIndex = 9
    Me.edAlistaOrig.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edAlistaOrig.TextInt = 0
    '
    'edDalistaOrig
    '
    Me.edDalistaOrig.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDalistaOrig.EditValue = "0"
    Me.edDalistaOrig.Location = New System.Drawing.Point(264, 23)
    Me.edDalistaOrig.Name = "edDalistaOrig"
    Me.edDalistaOrig.NTSDbField = ""
    Me.edDalistaOrig.NTSFormat = "0"
    Me.edDalistaOrig.NTSForzaVisZoom = False
    Me.edDalistaOrig.NTSOldValue = ""
    Me.edDalistaOrig.Properties.Appearance.Options.UseTextOptions = True
    Me.edDalistaOrig.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDalistaOrig.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDalistaOrig.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDalistaOrig.Properties.AutoHeight = False
    Me.edDalistaOrig.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDalistaOrig.Properties.MaxLength = 65536
    Me.edDalistaOrig.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDalistaOrig.Size = New System.Drawing.Size(51, 20)
    Me.edDalistaOrig.TabIndex = 8
    Me.edDalistaOrig.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edDalistaOrig.TextInt = 0
    '
    'opDocmagOrig
    '
    Me.opDocmagOrig.Cursor = System.Windows.Forms.Cursors.Default
    Me.opDocmagOrig.Location = New System.Drawing.Point(19, 47)
    Me.opDocmagOrig.Name = "opDocmagOrig"
    Me.opDocmagOrig.NTSCheckValue = "S"
    Me.opDocmagOrig.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opDocmagOrig.Properties.Appearance.Options.UseBackColor = True
    Me.opDocmagOrig.Properties.AutoHeight = False
    Me.opDocmagOrig.Properties.Caption = "Documento di magazzino"
    Me.opDocmagOrig.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opDocmagOrig.Size = New System.Drawing.Size(146, 19)
    Me.opDocmagOrig.TabIndex = 1
    '
    'opLselOrig
    '
    Me.opLselOrig.Cursor = System.Windows.Forms.Cursors.Default
    Me.opLselOrig.EditValue = True
    Me.opLselOrig.Location = New System.Drawing.Point(19, 24)
    Me.opLselOrig.Name = "opLselOrig"
    Me.opLselOrig.NTSCheckValue = "S"
    Me.opLselOrig.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opLselOrig.Properties.Appearance.Options.UseBackColor = True
    Me.opLselOrig.Properties.AutoHeight = False
    Me.opLselOrig.Properties.Caption = "Lista selezionata Da / A"
    Me.opLselOrig.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opLselOrig.Size = New System.Drawing.Size(139, 19)
    Me.opLselOrig.TabIndex = 0
    '
    'fmSelezione
    '
    Me.fmSelezione.AllowDrop = True
    Me.fmSelezione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmSelezione.Appearance.Options.UseBackColor = True
    Me.fmSelezione.Controls.Add(Me.cmdArtSel)
    Me.fmSelezione.Controls.Add(Me.opDaDocOrig)
    Me.fmSelezione.Controls.Add(Me.opArtSel)
    Me.fmSelezione.Controls.Add(Me.edAlistaDest)
    Me.fmSelezione.Controls.Add(Me.edDalistaDest)
    Me.fmSelezione.Controls.Add(Me.opArtBloc)
    Me.fmSelezione.Controls.Add(Me.opLselDest)
    Me.fmSelezione.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmSelezione.Location = New System.Drawing.Point(6, 150)
    Me.fmSelezione.Name = "fmSelezione"
    Me.fmSelezione.Size = New System.Drawing.Size(654, 125)
    Me.fmSelezione.TabIndex = 10
    Me.fmSelezione.Text = "Selezione degli articoli inventariati        (Articoli la cui esistenza dovrà ess" & _
        "ere corretta con quella effettivamente riscontrata)"
    '
    'cmdArtSel
    '
    Me.cmdArtSel.ImageText = ""
    Me.cmdArtSel.Location = New System.Drawing.Point(264, 69)
    Me.cmdArtSel.Name = "cmdArtSel"
    Me.cmdArtSel.NTSContextMenu = Nothing
    Me.cmdArtSel.Size = New System.Drawing.Size(171, 23)
    Me.cmdArtSel.TabIndex = 11
    Me.cmdArtSel.Text = "Seleziona articoli"
    '
    'opDaDocOrig
    '
    Me.opDaDocOrig.Cursor = System.Windows.Forms.Cursors.Default
    Me.opDaDocOrig.Location = New System.Drawing.Point(19, 98)
    Me.opDaDocOrig.Name = "opDaDocOrig"
    Me.opDaDocOrig.NTSCheckValue = "S"
    Me.opDaDocOrig.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opDaDocOrig.Properties.Appearance.Options.UseBackColor = True
    Me.opDaDocOrig.Properties.AutoHeight = False
    Me.opDaDocOrig.Properties.Caption = "Dal documento di magazzino di origine"
    Me.opDaDocOrig.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opDaDocOrig.Size = New System.Drawing.Size(210, 19)
    Me.opDaDocOrig.TabIndex = 10
    '
    'opArtSel
    '
    Me.opArtSel.Cursor = System.Windows.Forms.Cursors.Default
    Me.opArtSel.Location = New System.Drawing.Point(19, 73)
    Me.opArtSel.Name = "opArtSel"
    Me.opArtSel.NTSCheckValue = "S"
    Me.opArtSel.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opArtSel.Properties.Appearance.Options.UseBackColor = True
    Me.opArtSel.Properties.AutoHeight = False
    Me.opArtSel.Properties.Caption = "Selezione articoli"
    Me.opArtSel.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opArtSel.Size = New System.Drawing.Size(109, 19)
    Me.opArtSel.TabIndex = 10
    '
    'edAlistaDest
    '
    Me.edAlistaDest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAlistaDest.EditValue = "0"
    Me.edAlistaDest.Location = New System.Drawing.Point(384, 23)
    Me.edAlistaDest.Name = "edAlistaDest"
    Me.edAlistaDest.NTSDbField = ""
    Me.edAlistaDest.NTSFormat = "0"
    Me.edAlistaDest.NTSForzaVisZoom = False
    Me.edAlistaDest.NTSOldValue = "0"
    Me.edAlistaDest.Properties.Appearance.Options.UseTextOptions = True
    Me.edAlistaDest.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAlistaDest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAlistaDest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAlistaDest.Properties.AutoHeight = False
    Me.edAlistaDest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAlistaDest.Properties.MaxLength = 65536
    Me.edAlistaDest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAlistaDest.Size = New System.Drawing.Size(51, 20)
    Me.edAlistaDest.TabIndex = 9
    Me.edAlistaDest.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edAlistaDest.TextInt = 0
    '
    'edDalistaDest
    '
    Me.edDalistaDest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDalistaDest.EditValue = "0"
    Me.edDalistaDest.Location = New System.Drawing.Point(264, 23)
    Me.edDalistaDest.Name = "edDalistaDest"
    Me.edDalistaDest.NTSDbField = ""
    Me.edDalistaDest.NTSFormat = "0"
    Me.edDalistaDest.NTSForzaVisZoom = False
    Me.edDalistaDest.NTSOldValue = ""
    Me.edDalistaDest.Properties.Appearance.Options.UseTextOptions = True
    Me.edDalistaDest.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDalistaDest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDalistaDest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDalistaDest.Properties.AutoHeight = False
    Me.edDalistaDest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDalistaDest.Properties.MaxLength = 65536
    Me.edDalistaDest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDalistaDest.Size = New System.Drawing.Size(51, 20)
    Me.edDalistaDest.TabIndex = 8
    Me.edDalistaDest.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edDalistaDest.TextInt = 0
    '
    'opArtBloc
    '
    Me.opArtBloc.Cursor = System.Windows.Forms.Cursors.Default
    Me.opArtBloc.Location = New System.Drawing.Point(19, 48)
    Me.opArtBloc.Name = "opArtBloc"
    Me.opArtBloc.NTSCheckValue = "S"
    Me.opArtBloc.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opArtBloc.Properties.Appearance.Options.UseBackColor = True
    Me.opArtBloc.Properties.AutoHeight = False
    Me.opArtBloc.Properties.Caption = "Articoli bloccati"
    Me.opArtBloc.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opArtBloc.Size = New System.Drawing.Size(97, 19)
    Me.opArtBloc.TabIndex = 1
    '
    'opLselDest
    '
    Me.opLselDest.Cursor = System.Windows.Forms.Cursors.Default
    Me.opLselDest.EditValue = True
    Me.opLselDest.Location = New System.Drawing.Point(19, 24)
    Me.opLselDest.Name = "opLselDest"
    Me.opLselDest.NTSCheckValue = "S"
    Me.opLselDest.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opLselDest.Properties.Appearance.Options.UseBackColor = True
    Me.opLselDest.Properties.AutoHeight = False
    Me.opLselDest.Properties.Caption = "Lista selezionata Da / A"
    Me.opLselDest.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opLselDest.Size = New System.Drawing.Size(139, 19)
    Me.opLselDest.TabIndex = 0
    '
    'fmDoc
    '
    Me.fmDoc.AllowDrop = True
    Me.fmDoc.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmDoc.Appearance.Options.UseBackColor = True
    Me.fmDoc.Controls.Add(Me.edAnno)
    Me.fmDoc.Controls.Add(Me.ckElabora)
    Me.fmDoc.Controls.Add(Me.edListino)
    Me.fmDoc.Controls.Add(Me.cbTipVal)
    Me.fmDoc.Controls.Add(Me.lbTipVal)
    Me.fmDoc.Controls.Add(Me.lbCodcausmeno)
    Me.fmDoc.Controls.Add(Me.lbCodcauspiu)
    Me.fmDoc.Controls.Add(Me.lbCodconto)
    Me.fmDoc.Controls.Add(Me.lbCodtpbf)
    Me.fmDoc.Controls.Add(Me.lbCodcausmenoLabel)
    Me.fmDoc.Controls.Add(Me.lbCodcauspiuLabel)
    Me.fmDoc.Controls.Add(Me.lbCodcontoLabel)
    Me.fmDoc.Controls.Add(Me.lbCodtpbfLabel)
    Me.fmDoc.Controls.Add(Me.edCodcausmeno)
    Me.fmDoc.Controls.Add(Me.edCodcauspiu)
    Me.fmDoc.Controls.Add(Me.edCodtpbf)
    Me.fmDoc.Controls.Add(Me.edCodconto)
    Me.fmDoc.Controls.Add(Me.edNumdoc)
    Me.fmDoc.Controls.Add(Me.edSerie)
    Me.fmDoc.Controls.Add(Me.lbEstremiDoc)
    Me.fmDoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmDoc.Location = New System.Drawing.Point(6, 281)
    Me.fmDoc.Name = "fmDoc"
    Me.fmDoc.Size = New System.Drawing.Size(654, 185)
    Me.fmDoc.TabIndex = 11
    Me.fmDoc.Text = "Documento di rettifica da generare"
    '
    'edAnno
    '
    Me.edAnno.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnno.EditValue = "2009"
    Me.edAnno.Location = New System.Drawing.Point(441, 28)
    Me.edAnno.Name = "edAnno"
    Me.edAnno.NTSDbField = ""
    Me.edAnno.NTSFormat = "0"
    Me.edAnno.NTSForzaVisZoom = False
    Me.edAnno.NTSOldValue = "2009"
    Me.edAnno.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnno.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnno.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnno.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnno.Properties.AutoHeight = False
    Me.edAnno.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnno.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnno.Size = New System.Drawing.Size(43, 20)
    Me.edAnno.TabIndex = 27
    Me.edAnno.TextDec = New Decimal(New Integer() {2009, 0, 0, 0})
    Me.edAnno.TextInt = 2009
    '
    'ckElabora
    '
    Me.ckElabora.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckElabora.Location = New System.Drawing.Point(490, 159)
    Me.ckElabora.Name = "ckElabora"
    Me.ckElabora.NTSCheckValue = "S"
    Me.ckElabora.NTSUnCheckValue = "N"
    Me.ckElabora.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckElabora.Properties.Appearance.Options.UseBackColor = True
    Me.ckElabora.Properties.AutoHeight = False
    Me.ckElabora.Properties.Caption = "Elabora senza interruzione"
    Me.ckElabora.Size = New System.Drawing.Size(156, 19)
    Me.ckElabora.TabIndex = 26
    '
    'edListino
    '
    Me.edListino.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edListino.EditValue = "0"
    Me.edListino.Location = New System.Drawing.Point(384, 158)
    Me.edListino.Name = "edListino"
    Me.edListino.NTSDbField = ""
    Me.edListino.NTSFormat = "0"
    Me.edListino.NTSForzaVisZoom = False
    Me.edListino.NTSOldValue = ""
    Me.edListino.Properties.Appearance.Options.UseTextOptions = True
    Me.edListino.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edListino.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edListino.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edListino.Properties.AutoHeight = False
    Me.edListino.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edListino.Properties.MaxLength = 65536
    Me.edListino.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edListino.Size = New System.Drawing.Size(51, 20)
    Me.edListino.TabIndex = 25
    Me.edListino.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edListino.TextInt = 0
    '
    'cbTipVal
    '
    Me.cbTipVal.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipVal.DataSource = Nothing
    Me.cbTipVal.DisplayMember = ""
    Me.cbTipVal.Location = New System.Drawing.Point(179, 158)
    Me.cbTipVal.Name = "cbTipVal"
    Me.cbTipVal.NTSDbField = ""
    Me.cbTipVal.Properties.AutoHeight = False
    Me.cbTipVal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipVal.Properties.DropDownRows = 30
    Me.cbTipVal.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipVal.SelectedValue = ""
    Me.cbTipVal.Size = New System.Drawing.Size(199, 20)
    Me.cbTipVal.TabIndex = 24
    Me.cbTipVal.ValueMember = ""
    '
    'lbTipVal
    '
    Me.lbTipVal.AutoSize = True
    Me.lbTipVal.BackColor = System.Drawing.Color.Transparent
    Me.lbTipVal.Location = New System.Drawing.Point(16, 161)
    Me.lbTipVal.Name = "lbTipVal"
    Me.lbTipVal.NTSDbField = ""
    Me.lbTipVal.Size = New System.Drawing.Size(155, 13)
    Me.lbTipVal.TabIndex = 23
    Me.lbTipVal.Text = "Tipo valorizzazione  per rett. +"
    Me.lbTipVal.Tooltip = ""
    Me.lbTipVal.UseMnemonic = False
    '
    'lbCodcausmeno
    '
    Me.lbCodcausmeno.BackColor = System.Drawing.Color.Transparent
    Me.lbCodcausmeno.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbCodcausmeno.Location = New System.Drawing.Point(272, 132)
    Me.lbCodcausmeno.Name = "lbCodcausmeno"
    Me.lbCodcausmeno.NTSDbField = ""
    Me.lbCodcausmeno.Size = New System.Drawing.Size(374, 20)
    Me.lbCodcausmeno.TabIndex = 22
    Me.lbCodcausmeno.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbCodcausmeno.Tooltip = ""
    Me.lbCodcausmeno.UseMnemonic = False
    '
    'lbCodcauspiu
    '
    Me.lbCodcauspiu.BackColor = System.Drawing.Color.Transparent
    Me.lbCodcauspiu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbCodcauspiu.Location = New System.Drawing.Point(272, 106)
    Me.lbCodcauspiu.Name = "lbCodcauspiu"
    Me.lbCodcauspiu.NTSDbField = ""
    Me.lbCodcauspiu.Size = New System.Drawing.Size(374, 20)
    Me.lbCodcauspiu.TabIndex = 21
    Me.lbCodcauspiu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbCodcauspiu.Tooltip = ""
    Me.lbCodcauspiu.UseMnemonic = False
    '
    'lbCodconto
    '
    Me.lbCodconto.BackColor = System.Drawing.Color.Transparent
    Me.lbCodconto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbCodconto.Location = New System.Drawing.Point(272, 54)
    Me.lbCodconto.Name = "lbCodconto"
    Me.lbCodconto.NTSDbField = ""
    Me.lbCodconto.Size = New System.Drawing.Size(374, 20)
    Me.lbCodconto.TabIndex = 20
    Me.lbCodconto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbCodconto.Tooltip = ""
    Me.lbCodconto.UseMnemonic = False
    '
    'lbCodtpbf
    '
    Me.lbCodtpbf.BackColor = System.Drawing.Color.Transparent
    Me.lbCodtpbf.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbCodtpbf.Location = New System.Drawing.Point(272, 79)
    Me.lbCodtpbf.Name = "lbCodtpbf"
    Me.lbCodtpbf.NTSDbField = ""
    Me.lbCodtpbf.Size = New System.Drawing.Size(374, 20)
    Me.lbCodtpbf.TabIndex = 19
    Me.lbCodtpbf.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbCodtpbf.Tooltip = ""
    Me.lbCodtpbf.UseMnemonic = False
    '
    'lbCodcausmenoLabel
    '
    Me.lbCodcausmenoLabel.AutoSize = True
    Me.lbCodcausmenoLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbCodcausmenoLabel.Location = New System.Drawing.Point(16, 135)
    Me.lbCodcausmenoLabel.Name = "lbCodcausmenoLabel"
    Me.lbCodcausmenoLabel.NTSDbField = ""
    Me.lbCodcausmenoLabel.Size = New System.Drawing.Size(103, 13)
    Me.lbCodcausmenoLabel.TabIndex = 18
    Me.lbCodcausmenoLabel.Text = "Causale in rettifica -"
    Me.lbCodcausmenoLabel.Tooltip = ""
    Me.lbCodcausmenoLabel.UseMnemonic = False
    '
    'lbCodcauspiuLabel
    '
    Me.lbCodcauspiuLabel.AutoSize = True
    Me.lbCodcauspiuLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbCodcauspiuLabel.Location = New System.Drawing.Point(16, 109)
    Me.lbCodcauspiuLabel.Name = "lbCodcauspiuLabel"
    Me.lbCodcauspiuLabel.NTSDbField = ""
    Me.lbCodcauspiuLabel.Size = New System.Drawing.Size(107, 13)
    Me.lbCodcauspiuLabel.TabIndex = 17
    Me.lbCodcauspiuLabel.Text = "Causale in rettifica +"
    Me.lbCodcauspiuLabel.Tooltip = ""
    Me.lbCodcauspiuLabel.UseMnemonic = False
    '
    'lbCodcontoLabel
    '
    Me.lbCodcontoLabel.AutoSize = True
    Me.lbCodcontoLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbCodcontoLabel.Location = New System.Drawing.Point(16, 57)
    Me.lbCodcontoLabel.Name = "lbCodcontoLabel"
    Me.lbCodcontoLabel.NTSDbField = ""
    Me.lbCodcontoLabel.Size = New System.Drawing.Size(36, 13)
    Me.lbCodcontoLabel.TabIndex = 16
    Me.lbCodcontoLabel.Text = "Conto"
    Me.lbCodcontoLabel.Tooltip = ""
    Me.lbCodcontoLabel.UseMnemonic = False
    '
    'lbCodtpbfLabel
    '
    Me.lbCodtpbfLabel.AutoSize = True
    Me.lbCodtpbfLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbCodtpbfLabel.Location = New System.Drawing.Point(16, 83)
    Me.lbCodtpbfLabel.Name = "lbCodtpbfLabel"
    Me.lbCodtpbfLabel.NTSDbField = ""
    Me.lbCodtpbfLabel.Size = New System.Drawing.Size(90, 13)
    Me.lbCodtpbfLabel.TabIndex = 15
    Me.lbCodtpbfLabel.Text = "Tipo bolla/fattura"
    Me.lbCodtpbfLabel.Tooltip = ""
    Me.lbCodtpbfLabel.UseMnemonic = False
    '
    'edCodcausmeno
    '
    Me.edCodcausmeno.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodcausmeno.EditValue = "0"
    Me.edCodcausmeno.Location = New System.Drawing.Point(179, 132)
    Me.edCodcausmeno.Name = "edCodcausmeno"
    Me.edCodcausmeno.NTSDbField = ""
    Me.edCodcausmeno.NTSFormat = "0"
    Me.edCodcausmeno.NTSForzaVisZoom = False
    Me.edCodcausmeno.NTSOldValue = ""
    Me.edCodcausmeno.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodcausmeno.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodcausmeno.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodcausmeno.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodcausmeno.Properties.AutoHeight = False
    Me.edCodcausmeno.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodcausmeno.Properties.MaxLength = 65536
    Me.edCodcausmeno.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodcausmeno.Size = New System.Drawing.Size(87, 20)
    Me.edCodcausmeno.TabIndex = 14
    Me.edCodcausmeno.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCodcausmeno.TextInt = 0
    '
    'edCodcauspiu
    '
    Me.edCodcauspiu.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodcauspiu.EditValue = "0"
    Me.edCodcauspiu.Location = New System.Drawing.Point(179, 106)
    Me.edCodcauspiu.Name = "edCodcauspiu"
    Me.edCodcauspiu.NTSDbField = ""
    Me.edCodcauspiu.NTSFormat = "0"
    Me.edCodcauspiu.NTSForzaVisZoom = False
    Me.edCodcauspiu.NTSOldValue = ""
    Me.edCodcauspiu.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodcauspiu.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodcauspiu.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodcauspiu.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodcauspiu.Properties.AutoHeight = False
    Me.edCodcauspiu.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodcauspiu.Properties.MaxLength = 65536
    Me.edCodcauspiu.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodcauspiu.Size = New System.Drawing.Size(87, 20)
    Me.edCodcauspiu.TabIndex = 13
    Me.edCodcauspiu.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCodcauspiu.TextInt = 0
    '
    'edCodtpbf
    '
    Me.edCodtpbf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodtpbf.EditValue = "0"
    Me.edCodtpbf.Location = New System.Drawing.Point(179, 80)
    Me.edCodtpbf.Name = "edCodtpbf"
    Me.edCodtpbf.NTSDbField = ""
    Me.edCodtpbf.NTSFormat = "0"
    Me.edCodtpbf.NTSForzaVisZoom = False
    Me.edCodtpbf.NTSOldValue = ""
    Me.edCodtpbf.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodtpbf.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodtpbf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodtpbf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodtpbf.Properties.AutoHeight = False
    Me.edCodtpbf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodtpbf.Properties.MaxLength = 65536
    Me.edCodtpbf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodtpbf.Size = New System.Drawing.Size(87, 20)
    Me.edCodtpbf.TabIndex = 12
    Me.edCodtpbf.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCodtpbf.TextInt = 0
    '
    'edCodconto
    '
    Me.edCodconto.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edCodconto.EditValue = "0"
    Me.edCodconto.Location = New System.Drawing.Point(179, 54)
    Me.edCodconto.Name = "edCodconto"
    Me.edCodconto.NTSDbField = ""
    Me.edCodconto.NTSFormat = "0"
    Me.edCodconto.NTSForzaVisZoom = False
    Me.edCodconto.NTSOldValue = ""
    Me.edCodconto.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodconto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodconto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodconto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodconto.Properties.AutoHeight = False
    Me.edCodconto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodconto.Properties.MaxLength = 65536
    Me.edCodconto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodconto.Size = New System.Drawing.Size(87, 20)
    Me.edCodconto.TabIndex = 11
    Me.edCodconto.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCodconto.TextInt = 0
    '
    'edNumdoc
    '
    Me.edNumdoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumdoc.EditValue = "0"
    Me.edNumdoc.Location = New System.Drawing.Point(541, 28)
    Me.edNumdoc.Name = "edNumdoc"
    Me.edNumdoc.NTSDbField = ""
    Me.edNumdoc.NTSFormat = "0"
    Me.edNumdoc.NTSForzaVisZoom = False
    Me.edNumdoc.NTSOldValue = "0"
    Me.edNumdoc.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumdoc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumdoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNumdoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNumdoc.Properties.AutoHeight = False
    Me.edNumdoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumdoc.Properties.MaxLength = 65536
    Me.edNumdoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumdoc.Size = New System.Drawing.Size(105, 20)
    Me.edNumdoc.TabIndex = 10
    Me.edNumdoc.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edNumdoc.TextInt = 0
    '
    'edSerie
    '
    Me.edSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSerie.EditValue = " "
    Me.edSerie.Location = New System.Drawing.Point(490, 28)
    Me.edSerie.Name = "edSerie"
    Me.edSerie.NTSDbField = ""
    Me.edSerie.NTSForzaVisZoom = False
    Me.edSerie.NTSOldValue = " "
    Me.edSerie.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSerie.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSerie.Properties.AutoHeight = False
    Me.edSerie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSerie.Properties.MaxLength = 65536
    Me.edSerie.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSerie.Size = New System.Drawing.Size(45, 20)
    Me.edSerie.TabIndex = 7
    Me.edSerie.TextStr = " "
    '
    'lbEstremiDoc
    '
    Me.lbEstremiDoc.AutoSize = True
    Me.lbEstremiDoc.BackColor = System.Drawing.Color.Transparent
    Me.lbEstremiDoc.Location = New System.Drawing.Point(16, 31)
    Me.lbEstremiDoc.Name = "lbEstremiDoc"
    Me.lbEstremiDoc.NTSDbField = ""
    Me.lbEstremiDoc.Size = New System.Drawing.Size(264, 13)
    Me.lbEstremiDoc.TabIndex = 5
    Me.lbEstremiDoc.Text = "Bolla di movimentazione interna Anno / serie / numero"
    Me.lbEstremiDoc.Tooltip = ""
    Me.lbEstremiDoc.UseMnemonic = False
    '
    'lbStatus
    '
    Me.lbStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbStatus.BackColor = System.Drawing.Color.Transparent
    Me.lbStatus.Location = New System.Drawing.Point(12, 470)
    Me.lbStatus.Name = "lbStatus"
    Me.lbStatus.NTSDbField = ""
    Me.lbStatus.Size = New System.Drawing.Size(648, 13)
    Me.lbStatus.TabIndex = 4
    Me.lbStatus.Tooltip = ""
    Me.lbStatus.UseMnemonic = False
    '
    'FRMMGINVF
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(668, 488)
    Me.Controls.Add(Me.lbStatus)
    Me.Controls.Add(Me.fmDoc)
    Me.Controls.Add(Me.fmSelezione)
    Me.Controls.Add(Me.fmOrigine)
    Me.Controls.Add(Me.lbCodmaga)
    Me.Controls.Add(Me.edCodmaga)
    Me.Controls.Add(Me.lbCodmagaLabel)
    Me.Controls.Add(Me.edDatainv)
    Me.Controls.Add(Me.lbDatainv)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRMMGINVF"
    Me.Text = "GESTIONE INVENTARIO FISICO"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatainv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodmaga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmOrigine, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmOrigine.ResumeLayout(False)
    Me.fmOrigine.PerformLayout()
    CType(Me.edCausInv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAlistaOrig.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDalistaOrig.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opDocmagOrig.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opLselOrig.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmSelezione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmSelezione.ResumeLayout(False)
    Me.fmSelezione.PerformLayout()
    CType(Me.opDaDocOrig.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opArtSel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAlistaDest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDalistaDest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opArtBloc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opLselDest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmDoc, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmDoc.ResumeLayout(False)
    Me.fmDoc.PerformLayout()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckElabora.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edListino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipVal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodcausmeno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodcauspiu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodtpbf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNumdoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    'creo e attivo l'entity e inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGINVF", "BEMGINVF", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128792466424095000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleInvf = CType(oTmp, CLEMGINVF)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGINVF", strRemoteServer, strRemotePort)
    AddHandler oCleInvf.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleInvf.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)

    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbElabora.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      'esempio per colonna combo
      Dim dttTipo As New DataTable()
      dttTipo.Columns.Add("cod", GetType(String))
      dttTipo.Columns.Add("val", GetType(String))
      dttTipo.Rows.Add(New Object() {"M", "Costo medio dell' anno"})
      dttTipo.Rows.Add(New Object() {"U", "Ultimo costo"})
      dttTipo.Rows.Add(New Object() {"A", "Ultimo costo con oneri acc."})
      dttTipo.Rows.Add(New Object() {"L", "Listino"})
      dttTipo.AcceptChanges()
      cbTipVal.DataSource = dttTipo
      cbTipVal.ValueMember = "cod"
      cbTipVal.DisplayMember = "val"

      edAnno.NTSSetParam(oMenu, oApp.Tr(Me, 128792510441141000, "Anno nuovo documento"), "0", 4, 1900, 2099)
      ckElabora.NTSSetParam(oMenu, oApp.Tr(Me, 128792510441297000, "Elabora senza interruzione"), "S", "N")
      edListino.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128792510441453000, "Listino"), tablist)
      cbTipVal.NTSSetParam(oApp.Tr(Me, 128792510441609000, "Tipo valorizzazione"))
      edCodcauspiu.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128792510443325000, "Causale in rettifica +"), tabcaum)
      edCodcausmeno.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128792522657341000, "Causale in rettifica -"), tabcaum)
      edCodtpbf.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128792510443481000, "Tipo bolla/fattura"), tabtpbf)
      edCodconto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128792510443637000, "Conto cliente/fornitore"), tabanagraf)
      edNumdoc.NTSSetParam(oMenu, oApp.Tr(Me, 128792510443793000, "Numero documento"), "0", 9, 0, 999999999)
      edSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128792510443949000, "Serie documento"), CLN__STD.SerieMaxLen, False)
      opArtSel.NTSSetParam(oMenu, oApp.Tr(Me, 128792510444261000, "Selezione articoli"), "A")
      opDaDocOrig.NTSSetParam(oMenu, oApp.Tr(Me, 130386828038570243, "Da documento di origine"), "D")
      edAlistaDest.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128792510444417000, "A lista selezionata articoli inventariati"), tablsar)
      edDalistaDest.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128792510444573000, "DA lista selezionata articoli inventariati"), tablsar)
      opArtBloc.NTSSetParam(oMenu, oApp.Tr(Me, 128792510444729000, "Articoli bloccati"), "B")
      opLselDest.NTSSetParam(oMenu, oApp.Tr(Me, 128792510444885000, "Lista selezionata Da / A"), "L")
      edCausInv.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128792510445197000, "Causale inventario fisico"), tabcaum)
      edAlistaOrig.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128792510445353000, "A lista origine inventario"), tablsar)
      edDalistaOrig.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128792510445509000, "DA lista origine inventario"), tablsar)
      opDocmagOrig.NTSSetParam(oMenu, oApp.Tr(Me, 128792510445665000, "Documento di magazzino"), "D")
      opLselOrig.NTSSetParam(oMenu, oApp.Tr(Me, 128792510445821000, "Lista selezionata Da / A"), "L")
      edCodmaga.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128792510445977000, "Magazzino"), tabmaga)
      edDatainv.NTSSetParam(oMenu, oApp.Tr(Me, 128792510446289000, "Data inventario fisico"), False)

      edDatainv.NTSSetRichiesto()
      edCodmaga.NTSSetRichiesto()

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

  Overloads Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    Try
      If Not IsMyThrowRemoteEvent() Then Return 'il messaggio non è per questa form ...
      MyBase.GestisciEventiEntity(sender, e)

      If e.TipoEvento.Trim.Length < 10 Then Return
      Select Case e.TipoEvento.Substring(0, 10)
        Case "AGGIOLABEL"
          lbStatus.Text = e.Message
          lbStatus.Refresh()
          Application.DoEvents()      'non togliere, altrimenti non aggiorna la label ...
      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi di Form"
  Public Overridable Sub FRMMGINVF_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      InitControls()

      cbTipVal.SelectedValue = "M"

      '--------------------------
      edCodtpbf.Text = NTSCInt(oMenu.GetSettingBus("BSMGINVF", "RECENT", ".", "TipoBF", "0", " ", "0")).ToString
      edCodconto.Text = NTSCInt(oMenu.GetSettingBus("BSMGINVF", "RECENT", ".", "ContoCf", "0", " ", "0")).ToString
      edCodcauspiu.Text = NTSCInt(oMenu.GetSettingBus("BSMGINVF", "RECENT", ".", "CausMagaCar", "0", " ", "0")).ToString
      edCodcausmeno.Text = NTSCInt(oMenu.GetSettingBus("BSMGINVF", "RECENT", ".", "CausMagaScar", "0", " ", "0")).ToString
      edCausInv.Text = NTSCInt(oMenu.GetSettingBus("BSMGINVF", "RECENT", ".", "CausInv", "0", " ", "0")).ToString
      oCleInvf.lIITTArtinvf = oMenu.GetTblInstId("TTARTINVF", False)
      oCleInvf.lIITTArtinvtc = oMenu.GetTblInstId("TTARTINVTC", False)
      '--------------------------------------------------------------------------------------------------------------
      oCleInvf.nCodivaFisso = NTSCInt(oMenu.GetSettingBus("BSMGINVF", "OPZIONI", ".", "CodivaFisso", "0", " ", "0"))
      If oCleInvf.nCodivaFisso <> 0 Then
        If oMenu.ValCodiceDb(oCleInvf.nCodivaFisso.ToString, DittaCorrente, "TABCIVA", "N") = False Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 130395378538213906, "Attenzione!" & vbCrLf & _
            "Il Codice Iva indicato nell'opzione di registro:" & vbCrLf & _
            " --> 'BSMGINVF\OPZIONI\CodivaFisso'" & vbCrLf & _
            "NON Ã¨ valido." & vbCrLf & _
            "Non sarÃ  considerato nell'elaborazione."))
          oCleInvf.nCodivaFisso = 0
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      edDatainv.Text = DateTime.Now.ToShortDateString
      edAnno.Text = DateTime.Now.Year.ToString
      edNumdoc.Text = oCleInvf.TrovaNumdoc(NTSCInt(edAnno.Text), edSerie.Text).ToString

      '--------------------------
      'per far abilitare/disabilitare i controlli in form
      opLselOrig_CheckedChanged(opLselOrig, Nothing)
      opDocmagOrig_CheckedChanged(opDocmagOrig, Nothing)
      opLselDest_CheckedChanged(opLselDest, Nothing)
      opArtSel_CheckedChanged(opArtSel, Nothing)
      cbTipVal_SelectedIndexChanged(cbTipVal, Nothing)

      GctlSetRoules()

      GctlApplicaDefaultValue()

      '--------------------------
      'se ho il modulo TCO posso solo usare il doc di magazzino
      If CBool(oCleInvf.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCO) Then
        opLselOrig.Enabled = False
        opDocmagOrig.Checked = True
        edDalistaOrig.Text = "0"
        edAlistaOrig.Text = "0"
      End If
      NTSGetDataAutocompletamentoTipork = "Z" 'per filtrare solo i tipibf utilizzabili per questo tipork
      '--------------------------------------------------------------------------------------------------------------
      '--- Controllo esistenza modulo Taglie & Colori
      '--------------------------------------------------------------------------------------------------------------
      bModTCO = CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCO)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGINVF_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      oMenu.ResetTblInstId("TTARTINVF", False, oCleInvf.lIITTArtinvf)
      oMenu.ResetTblInstId("TTARTINVTC", False, oCleInvf.lIITTArtinvtc)
      oMenu.SaveSettingBus("BSMGINVF", "Recent", ".", "TipoBF", edCodtpbf.Text, " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("BSMGINVF", "Recent", ".", "ContoCf", edCodconto.Text, " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("BSMGINVF", "Recent", ".", "CausMagaCar", edCodcauspiu.Text, " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("BSMGINVF", "Recent", ".", "CausMagaScar", edCodcausmeno.Text, " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("BSMGINVF", "Recent", ".", "CausInv", edCausInv.Text, " ", "NS.", "...", "...")

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbElabora_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbElabora.ItemClick
    Dim strParam As String = ""
    Dim bDocCreato As Boolean = False
    Dim bOk As Boolean = False
    Dim lNumdoc As Integer = 0

    Try
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      '--- Se è attivo il modulo Taglie & Colori, controlla l'esistenza di articoli T&C 
      '--- non presenti in tabella "Scala Taglie", chiedendo di proseguire o meno
      '--------------------------------------------------------------------------------------------------------------
      If CheckCoerenzaArticoliTCO() = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      'seleziono gli articoli se non è stato fatto
      If strWhereArtico.Trim = "." And opArtSel.Checked Then
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128792565050247000, "Non sono stati selezionati gli articoli." & vbCrLf & "Selezionarli ora?")) = Windows.Forms.DialogResult.Yes Then
          cmdArtSel_Click(cmdArtSel, Nothing)
        End If
        If strWhereArtico.Trim = "." Then Return
      End If

      Dim strOrigine As String = ""
      Select Case True
        Case opLselDest.Checked : strOrigine = "L"
        Case opArtBloc.Checked : strOrigine = "B"
        Case opArtSel.Checked : strOrigine = "A"
        Case opDaDocOrig.Checked : strOrigine = "D"
      End Select

      '-------------------------
      'eseguo l'elaborazione
      Me.Cursor = Cursors.WaitCursor
      lNumdoc = NTSCInt(edNumdoc.Text)
      bOk = oCleInvf.Elabora(edDatainv.Text, NTSCInt(edCodmaga.Text), IIf(opDocmagOrig.Checked, "M", "L").ToString, _
                          NTSCInt(edDalistaOrig.Text), NTSCInt(edAlistaOrig.Text), NTSCInt(edCausInv.Text), _
                          strOrigine, NTSCInt(edDalistaDest.Text), NTSCInt(edAlistaDest.Text), strWhereArtico, NTSCInt(edAnno.Text), _
                          edSerie.Text, lNumdoc, NTSCInt(edCodconto.Text), NTSCInt(edCodtpbf.Text), _
                          NTSCInt(edCodcauspiu.Text), NTSCInt(edCodcausmeno.Text), cbTipVal.SelectedValue.ToString, _
                          NTSCInt(edListino.Text), ckElabora.Checked, bDocCreato)
      Me.Cursor = Cursors.Default

      If oCleInvf.LogError = True Then
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127940796626250000, "Esistono dei messaggi nel file di log del programma. Visualizzare il file?")) = Windows.Forms.DialogResult.Yes Then
          NTSProcessStart("notepad", oCleInvf.LogFileName)
        End If
      End If

      If bOk Then
        If bDocCreato = True Then
          lNumdocRPT = lNumdoc
          If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128792551053203000, "Aprire il documento creato?")) = Windows.Forms.DialogResult.Yes Then
            strParam = "APRI;Z;" & _
                       edAnno.Text.PadLeft(4, "0"c) & ";" & _
                       edSerie.Text & ";" & _
                       lNumdoc.ToString.PadLeft(9, "0"c) & ";"
            oMenu.RunChild("BSVEBOLL", "CLSVEBOLL", oApp.Tr(Me, 128792551810271000, "Gestione documenti di magazzino"), DittaCorrente, "", "", Nothing, strParam, True, True)
          End If
        End If
        edNumdoc.Text = oCleInvf.TrovaNumdoc(NTSCInt(edAnno.Text), edSerie.Text).ToString
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
      lbStatus.Text = oApp.Tr(Me, 129083654145487537, "Pronto.")
      lbStatus.Refresh()
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      'zoom standard
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      '------------------------------------
      'zoom standard di textbox e griglia
      NTSCallStandardZoom()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      Stampa(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub
#End Region

#Region "Abilita/disabilita controlli"
  Public Overridable Sub opLselOrig_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opLselOrig.CheckedChanged
    Try
      If opLselOrig.Checked Then
        GctlSetVisEnab(edDalistaOrig, False)
        GctlSetVisEnab(edAlistaOrig, False)
        opDaDocOrig.Enabled = False
        If opDaDocOrig.Checked Then opLselDest.Checked = True
      Else
        edDalistaOrig.Enabled = False
        edAlistaOrig.Enabled = False
        GctlSetVisEnab(opDaDocOrig, False)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opDocmagOrig_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opDocmagOrig.CheckedChanged
    Try
      If opDocmagOrig.Checked Then
        GctlSetVisEnab(edCausInv, False)
      Else
        edCausInv.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opLselDest_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opLselDest.CheckedChanged
    Try
      If opLselDest.Checked Then
        GctlSetVisEnab(edDalistaDest, False)
        GctlSetVisEnab(edAlistaDest, False)
      Else
        edDalistaDest.Enabled = False
        edAlistaDest.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opArtSel_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opArtSel.CheckedChanged, opDaDocOrig.CheckedChanged, opDaDocOrig.CheckedChanged, opDaDocOrig.CheckedChanged
    Try
      If opArtSel.Checked Then
        GctlSetVisEnab(cmdArtSel, False)
      Else
        cmdArtSel.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cbTipVal_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTipVal.SelectedIndexChanged
    Try
      If cbTipVal.SelectedValue = "L" Then
        GctlSetVisEnab(edListino, False)
      Else
        edListino.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Proposta numero documento"
  Public Overridable Sub edAnno_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAnno.ValidatedAndChanged
    Try
      If oCleInvf Is Nothing Then Return
      edNumdoc.Text = oCleInvf.TrovaNumdoc(NTSCInt(edAnno.Text), edSerie.Text).ToString
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edSerie_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSerie.ValidatedAndChanged
    Dim strTmp As String = ""
    Try
      If oCleInvf Is Nothing Then Return

      strTmp = TestSerieMaxLen(edSerie.Text, False)
      If strTmp <> edSerie.Text Then edSerie.Text = strTmp

      edNumdoc.Text = oCleInvf.TrovaNumdoc(NTSCInt(edAnno.Text), edSerie.Text).ToString
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

  Public Overridable Sub cmdArtSel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdArtSel.Click
    Dim oPar As CLE__PATB = Nothing
    Try
      oPar = New CLE__PATB
      oPar.bVisGriglia = False
      oPar.strTipoArticolo = "N"
      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oPar)
      If oPar.CANCELZOOM = False Then strWhereArtico = oPar.strOut.Trim

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

#Region "Validazione controlli collegati a tabelle"
  Public Overridable Sub edCodmaga_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodmaga.ValidatedAndChanged
    Dim strTmp As String = ""
    Try
      If oCleInvf Is Nothing Then Return
      If Not oCleInvf.edCodmaga_ValidatedAndChanged(NTSCInt(edCodmaga.Text), strTmp) Then
        edCodmaga.Text = "0"
        lbCodmaga.Text = ""
      Else
        lbCodmaga.Text = strTmp
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edCausInv_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCausInv.ValidatedAndChanged
    Dim strTmp As String = ""
    Try
      If oCleInvf Is Nothing Then Return
      If Not oCleInvf.edCausale_ValidatedAndChanged(NTSCInt(edCausInv.Text), strTmp) Then
        edCausInv.Text = "0"
        lbCausInv.Text = ""
      Else
        lbCausInv.Text = strTmp
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edCodconto_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodconto.ValidatedAndChanged
    Dim strTmp As String = ""
    Try
      If oCleInvf Is Nothing Then Return
      If Not oCleInvf.edCodconto_ValidatedAndChanged(NTSCInt(edCodconto.Text), strTmp) Then
        edCodconto.Text = "0"
        lbCodconto.Text = ""
      Else
        lbCodconto.Text = strTmp
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edCodtpbf_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodtpbf.ValidatedAndChanged
    Dim strTmp As String = ""
    Try
      If oCleInvf Is Nothing Then Return
      If Not oCleInvf.edCodtpbf_ValidatedAndChanged(NTSCInt(edCodtpbf.Text), strTmp) Then
        edCodtpbf.Text = "0"
        lbCodtpbf.Text = ""
      Else
        lbCodtpbf.Text = strTmp
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edCodcauspiu_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodcauspiu.ValidatedAndChanged
    Dim strTmp As String = ""
    Try
      If oCleInvf Is Nothing Then Return
      If Not oCleInvf.edCausale_ValidatedAndChanged(NTSCInt(edCodcauspiu.Text), strTmp) Then
        edCodcauspiu.Text = "0"
        lbCodcauspiu.Text = ""
      Else
        lbCodcauspiu.Text = strTmp
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edCodcausmeno_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodcausmeno.ValidatedAndChanged
    Dim strTmp As String = ""
    Try
      If oCleInvf Is Nothing Then Return
      If Not oCleInvf.edCausale_ValidatedAndChanged(NTSCInt(edCodcausmeno.Text), strTmp) Then
        edCodcausmeno.Text = "0"
        lbCodcausmeno.Text = ""
      Else
        lbCodcausmeno.Text = strTmp
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer

    Try
      Me.ValidaLastControl()

      '--------------------------------------------------
      'preparo il motore di stampa
      strCrpe = "{TTARTINVF.codditt} = '" & DittaCorrente & "' And {TTARTINVF.instid} = " & oCleInvf.lIITTArtinvf.ToString
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGINVF", "Reports1", " ", 0, nDestin, "BSMGINVF.RPT", False, "Gestione Inventario Fisico", False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce già il multireport)
      For i = 1 To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "MAGAZZINO", NTSCInt(edCodmaga.Text).ToString)
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DATACONT", ConvStrRpt(edDatainv.Text))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

      '--------------------------------------------------
      'Se abilitato il modulo TCO lancio anche un report su TTARTINVTC
      If CBool(oCleInvf.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCO) Then
        strCrpe = "{TTARTINVTC.codditt} = " & ConvStrRpt(DittaCorrente) & _
                  " And {TTARTINVTC.instid} = " & oCleInvf.lIITTArtinvtc.ToString & _
                  " And {movmag.mm_tipork} = " & ConvStrRpt("Z") & _
                  " And {movmag.mm_anno} = " & edAnno.Text & _
                  " And {movmag.mm_serie} = " & ConvStrRpt(edSerie.Text) & _
                  " And {movmag.mm_numdoc} = " & lNumdocRPT.ToString
        nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGINVF", "Reports2", " ", 0, nDestin, "BSMGINVFTC.RPT", False, "Gestione Inventario Fisico Taglie e colori", False)
        If nPjob Is Nothing Then Return
        For i = 1 To UBound(CType(nPjob, Array), 2)
          nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "MAGAZZINO", NTSCInt(edCodmaga.Text).ToString)
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DATACONT", ConvStrRpt(edDatainv.Text))
          nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
        Next
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function CheckCoerenzaArticoliTCO() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Se non è attivo il modulo Taglie & Colori, ritorna "True" (non ha senso procedere con il controllo)
      '--------------------------------------------------------------------------------------------------------------
      If bModTCO = False Then Return True
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla l'esistenza di articoli T&C non presenti in tabella "Scala Taglie"
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edCodmaga.Text) = 0 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130790237322191733, "Attenzione!" & vbCrLf & _
          "Il codice magazzino da elaborare deve essere diverso da 0."))
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCleInvf.CheckArticoliTCO(NTSCInt(edCodmaga.Text)) = False Then
        If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 130790224169125936, "ATTENZIONE!" & vbCrLf & vbCrLf & _
          "Esistono articoli T&C che hanno quantità su taglie" & vbCrLf & _
          "NON presenti in tabella 'Scala Taglie'." & vbCrLf & vbCrLf & _
          "Per avere un elenco completo degli articoli incongruenti" & vbCrLf & _
          "si consiglia di lanciare il programma di:" & vbCrLf & _
          " . Controllo Coerenza Dati (Menu-->Utility)" & vbCrLf & _
          "selezionando le procedure: '00059', '00062', '00064' e '00065'." & vbCrLf & vbCrLf & _
          "Proseguire ugualmente con l'elaborazione?")) = Windows.Forms.DialogResult.No Then
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

End Class
