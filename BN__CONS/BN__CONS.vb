Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__CONS
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

  Public oCleCons As CLE__CONS
  Public oCallParams As CLE__CLDP
  Public dsCons As DataSet
  Public dcCons As BindingSource = New BindingSource()

  Private components As System.ComponentModel.IContainer

  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__CONS))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbAnalisi = New NTSInformatica.NTSBarButtonItem
    Me.tlbClienti = New NTSInformatica.NTSBarButtonItem
    Me.tlbFornitori = New NTSInformatica.NTSBarButtonItem
    Me.tlbArticoli = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.cmdApriAnagraf = New NTSInformatica.NTSButton
    Me.cmdNuovaAnagraf = New NTSInformatica.NTSButton
    Me.cmdApriDoc = New NTSInformatica.NTSButton
    Me.cmdNuovoDoc = New NTSInformatica.NTSButton
    Me.cmdVisOrd = New NTSInformatica.NTSButton
    Me.cmdVisMovmag = New NTSInformatica.NTSButton
    Me.cmdVisPart = New NTSInformatica.NTSButton
    Me.cmdVisScad = New NTSInformatica.NTSButton
    Me.cmdArtpro = New NTSInformatica.NTSButton
    Me.cmdArtprox = New NTSInformatica.NTSButton
    Me.cmdDatiCont = New NTSInformatica.NTSButton
    Me.cmdStatistiche = New NTSInformatica.NTSButton
    Me.ckFiltri = New NTSInformatica.NTSCheckBox
    Me.edDatfin = New NTSInformatica.NTSTextBoxData
    Me.edDatini = New NTSInformatica.NTSTextBoxData
    Me.lbDataAl = New NTSInformatica.NTSLabel
    Me.lbDataDal = New NTSInformatica.NTSLabel
    Me.lbParentesi = New NTSInformatica.NTSLabel
    Me.lbConto = New NTSInformatica.NTSLabel
    Me.edConto = New NTSInformatica.NTSTextBoxNum
    Me.lbContoLabel = New NTSInformatica.NTSLabel
    Me.lbArticolo = New NTSInformatica.NTSLabel
    Me.lbArticoloLabel = New NTSInformatica.NTSLabel
    Me.edArticolo = New NTSInformatica.NTSTextBoxStr
    Me.fmFiltri = New NTSInformatica.NTSGroupBox
    Me.cbEscomp = New NTSInformatica.NTSComboBox
    Me.edEsaltro = New NTSInformatica.NTSTextBoxNum
    Me.lbEscomp = New NTSInformatica.NTSLabel
    Me.lbNota = New NTSInformatica.NTSLabel
    Me.lbAnnoDoc = New NTSInformatica.NTSLabel
    Me.lbtipoDoc = New NTSInformatica.NTSLabel
    Me.edAnnoDoc = New NTSInformatica.NTSTextBoxNum
    Me.cbTipoDoc = New NTSInformatica.NTSComboBox
    Me.pnMain = New NTSInformatica.NTSPanel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckFiltri.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatfin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edArticolo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmFiltri, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmFiltri.SuspendLayout()
    CType(Me.cbEscomp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEsaltro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnnoDoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipoDoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnMain, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnMain.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbClienti, Me.tlbFornitori, Me.tlbAnalisi, Me.tlbArticoli, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom})
    Me.NtsBarManager1.MaxItemId = 17
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAnalisi), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbClienti), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbFornitori), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbArticoli), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbAnalisi
    '
    Me.tlbAnalisi.Caption = "Analisi su: "
    Me.tlbAnalisi.Id = 3
    Me.tlbAnalisi.Name = "tlbAnalisi"
    Me.tlbAnalisi.Visible = True
    '
    'tlbClienti
    '
    Me.tlbClienti.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbClienti.Caption = "Clienti"
    Me.tlbClienti.Down = True
    Me.tlbClienti.Id = 0
    Me.tlbClienti.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L))
    Me.tlbClienti.Name = "tlbClienti"
    Me.tlbClienti.Visible = True
    '
    'tlbFornitori
    '
    Me.tlbFornitori.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbFornitori.Caption = "Fornitori"
    Me.tlbFornitori.Id = 1
    Me.tlbFornitori.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F))
    Me.tlbFornitori.Name = "tlbFornitori"
    Me.tlbFornitori.Visible = True
    '
    'tlbArticoli
    '
    Me.tlbArticoli.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbArticoli.Caption = "Articoli"
    Me.tlbArticoli.Id = 2
    Me.tlbArticoli.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
    Me.tlbArticoli.Name = "tlbArticoli"
    Me.tlbArticoli.Visible = True
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
    'cmdApriAnagraf
    '
    Me.cmdApriAnagraf.Location = New System.Drawing.Point(525, 32)
    Me.cmdApriAnagraf.Name = "cmdApriAnagraf"
    Me.cmdApriAnagraf.Size = New System.Drawing.Size(205, 23)
    Me.cmdApriAnagraf.TabIndex = 4
    Me.cmdApriAnagraf.Text = "Apri anagrafica (F3)"
    '
    'cmdNuovaAnagraf
    '
    Me.cmdNuovaAnagraf.Location = New System.Drawing.Point(525, 54)
    Me.cmdNuovaAnagraf.Name = "cmdNuovaAnagraf"
    Me.cmdNuovaAnagraf.Size = New System.Drawing.Size(205, 23)
    Me.cmdNuovaAnagraf.TabIndex = 5
    Me.cmdNuovaAnagraf.Text = "Nuova anagrafica (F2)"
    '
    'cmdApriDoc
    '
    Me.cmdApriDoc.Location = New System.Drawing.Point(525, 92)
    Me.cmdApriDoc.Name = "cmdApriDoc"
    Me.cmdApriDoc.Size = New System.Drawing.Size(205, 23)
    Me.cmdApriDoc.TabIndex = 6
    Me.cmdApriDoc.Text = "Apri documento (Shift+F3)"
    '
    'cmdNuovoDoc
    '
    Me.cmdNuovoDoc.Location = New System.Drawing.Point(525, 114)
    Me.cmdNuovoDoc.Name = "cmdNuovoDoc"
    Me.cmdNuovoDoc.Size = New System.Drawing.Size(205, 23)
    Me.cmdNuovoDoc.TabIndex = 7
    Me.cmdNuovoDoc.Text = "Nuovo documento (Shift+F2)"
    '
    'cmdVisOrd
    '
    Me.cmdVisOrd.Location = New System.Drawing.Point(525, 148)
    Me.cmdVisOrd.Name = "cmdVisOrd"
    Me.cmdVisOrd.Size = New System.Drawing.Size(205, 23)
    Me.cmdVisOrd.TabIndex = 8
    Me.cmdVisOrd.Text = "Visualizza ordini/impegni (Ctrl+O)"
    '
    'cmdVisMovmag
    '
    Me.cmdVisMovmag.Location = New System.Drawing.Point(525, 170)
    Me.cmdVisMovmag.Name = "cmdVisMovmag"
    Me.cmdVisMovmag.Size = New System.Drawing.Size(205, 23)
    Me.cmdVisMovmag.TabIndex = 9
    Me.cmdVisMovmag.Text = "Visualizza movim. di magazz. (Ctrl+M)"
    '
    'cmdVisPart
    '
    Me.cmdVisPart.Location = New System.Drawing.Point(525, 206)
    Me.cmdVisPart.Name = "cmdVisPart"
    Me.cmdVisPart.Size = New System.Drawing.Size(205, 23)
    Me.cmdVisPart.TabIndex = 10
    Me.cmdVisPart.Text = "Visualizza partitario (Ctrl+P)"
    '
    'cmdVisScad
    '
    Me.cmdVisScad.Location = New System.Drawing.Point(525, 228)
    Me.cmdVisScad.Name = "cmdVisScad"
    Me.cmdVisScad.Size = New System.Drawing.Size(205, 23)
    Me.cmdVisScad.TabIndex = 11
    Me.cmdVisScad.Text = "Visualizza scadenziario (Ctrl+Z)"
    '
    'cmdArtpro
    '
    Me.cmdArtpro.Location = New System.Drawing.Point(6, 286)
    Me.cmdArtpro.Name = "cmdArtpro"
    Me.cmdArtpro.Size = New System.Drawing.Size(205, 23)
    Me.cmdArtpro.TabIndex = 12
    Me.cmdArtpro.Text = "Progressivi/esist/disp x mag. (Ctrl+E)"
    '
    'cmdArtprox
    '
    Me.cmdArtprox.Location = New System.Drawing.Point(219, 286)
    Me.cmdArtprox.Name = "cmdArtprox"
    Me.cmdArtprox.Size = New System.Drawing.Size(205, 23)
    Me.cmdArtprox.TabIndex = 13
    Me.cmdArtprox.Text = "Progressivi/esist/dispon totale (Ctrl+T)"
    '
    'cmdDatiCont
    '
    Me.cmdDatiCont.Location = New System.Drawing.Point(525, 264)
    Me.cmdDatiCont.Name = "cmdDatiCont"
    Me.cmdDatiCont.Size = New System.Drawing.Size(205, 23)
    Me.cmdDatiCont.TabIndex = 14
    Me.cmdDatiCont.Text = "Dati contabili sintetici/statistici (Ctrl+D)"
    '
    'cmdStatistiche
    '
    Me.cmdStatistiche.Location = New System.Drawing.Point(525, 286)
    Me.cmdStatistiche.Name = "cmdStatistiche"
    Me.cmdStatistiche.Size = New System.Drawing.Size(205, 23)
    Me.cmdStatistiche.TabIndex = 15
    Me.cmdStatistiche.Text = "Statistiche (Ctrl+S)"
    '
    'ckFiltri
    '
    Me.ckFiltri.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckFiltri.Location = New System.Drawing.Point(6, 3)
    Me.ckFiltri.Name = "ckFiltri"
    Me.ckFiltri.NTSCheckValue = "S"
    Me.ckFiltri.NTSUnCheckValue = "N"
    Me.ckFiltri.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckFiltri.Properties.Appearance.Options.UseBackColor = True
    Me.ckFiltri.Properties.Caption = "Richiedi ulteriori filtri nella finestra richiamata"
    Me.ckFiltri.Size = New System.Drawing.Size(240, 19)
    Me.ckFiltri.TabIndex = 16
    '
    'edDatfin
    '
    Me.edDatfin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatfin.EditValue = "24/06/2008"
    Me.edDatfin.Location = New System.Drawing.Point(98, 29)
    Me.edDatfin.Name = "edDatfin"
    Me.edDatfin.NTSDbField = ""
    Me.edDatfin.NTSForzaVisZoom = False
    Me.edDatfin.NTSOldValue = ""
    Me.edDatfin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatfin.Properties.MaxLength = 65536
    Me.edDatfin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatfin.Size = New System.Drawing.Size(108, 20)
    Me.edDatfin.TabIndex = 17
    '
    'edDatini
    '
    Me.edDatini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatini.Location = New System.Drawing.Point(246, 29)
    Me.edDatini.Name = "edDatini"
    Me.edDatini.NTSDbField = ""
    Me.edDatini.NTSForzaVisZoom = False
    Me.edDatini.NTSOldValue = ""
    Me.edDatini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatini.Properties.MaxLength = 65536
    Me.edDatini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatini.Size = New System.Drawing.Size(100, 20)
    Me.edDatini.TabIndex = 18
    '
    'lbDataAl
    '
    Me.lbDataAl.AutoSize = True
    Me.lbDataAl.BackColor = System.Drawing.Color.Transparent
    Me.lbDataAl.Location = New System.Drawing.Point(5, 32)
    Me.lbDataAl.Name = "lbDataAl"
    Me.lbDataAl.NTSDbField = ""
    Me.lbDataAl.Size = New System.Drawing.Size(49, 13)
    Me.lbDataAl.TabIndex = 19
    Me.lbDataAl.Text = "Data (al)"
    '
    'lbDataDal
    '
    Me.lbDataDal.AutoSize = True
    Me.lbDataDal.BackColor = System.Drawing.Color.Transparent
    Me.lbDataDal.Location = New System.Drawing.Point(212, 32)
    Me.lbDataDal.Name = "lbDataDal"
    Me.lbDataDal.NTSDbField = ""
    Me.lbDataDal.Size = New System.Drawing.Size(28, 13)
    Me.lbDataDal.TabIndex = 20
    Me.lbDataDal.Text = "( dal"
    '
    'lbParentesi
    '
    Me.lbParentesi.AutoSize = True
    Me.lbParentesi.BackColor = System.Drawing.Color.Transparent
    Me.lbParentesi.Location = New System.Drawing.Point(352, 32)
    Me.lbParentesi.Name = "lbParentesi"
    Me.lbParentesi.NTSDbField = ""
    Me.lbParentesi.Size = New System.Drawing.Size(11, 13)
    Me.lbParentesi.TabIndex = 21
    Me.lbParentesi.Text = ")"
    '
    'lbConto
    '
    Me.lbConto.BackColor = System.Drawing.Color.Transparent
    Me.lbConto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbConto.Location = New System.Drawing.Point(212, 60)
    Me.lbConto.Name = "lbConto"
    Me.lbConto.NTSDbField = ""
    Me.lbConto.Size = New System.Drawing.Size(307, 20)
    Me.lbConto.TabIndex = 81
    '
    'edConto
    '
    Me.edConto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edConto.EditValue = "0"
    Me.edConto.Location = New System.Drawing.Point(98, 60)
    Me.edConto.Name = "edConto"
    Me.edConto.NTSDbField = ""
    Me.edConto.NTSFormat = "0"
    Me.edConto.NTSForzaVisZoom = False
    Me.edConto.NTSOldValue = ""
    Me.edConto.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edConto.Properties.Appearance.Options.UseBackColor = True
    Me.edConto.Properties.Appearance.Options.UseTextOptions = True
    Me.edConto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edConto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edConto.Properties.MaxLength = 65536
    Me.edConto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edConto.Size = New System.Drawing.Size(108, 20)
    Me.edConto.TabIndex = 80
    '
    'lbContoLabel
    '
    Me.lbContoLabel.AutoSize = True
    Me.lbContoLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbContoLabel.Location = New System.Drawing.Point(5, 63)
    Me.lbContoLabel.Name = "lbContoLabel"
    Me.lbContoLabel.NTSDbField = ""
    Me.lbContoLabel.Size = New System.Drawing.Size(86, 13)
    Me.lbContoLabel.TabIndex = 82
    Me.lbContoLabel.Text = "Cliente/fornitore"
    '
    'lbArticolo
    '
    Me.lbArticolo.BackColor = System.Drawing.Color.Transparent
    Me.lbArticolo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbArticolo.Location = New System.Drawing.Point(212, 85)
    Me.lbArticolo.Name = "lbArticolo"
    Me.lbArticolo.NTSDbField = ""
    Me.lbArticolo.Size = New System.Drawing.Size(307, 20)
    Me.lbArticolo.TabIndex = 83
    '
    'lbArticoloLabel
    '
    Me.lbArticoloLabel.AutoSize = True
    Me.lbArticoloLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbArticoloLabel.Location = New System.Drawing.Point(5, 88)
    Me.lbArticoloLabel.Name = "lbArticoloLabel"
    Me.lbArticoloLabel.NTSDbField = ""
    Me.lbArticoloLabel.Size = New System.Drawing.Size(43, 13)
    Me.lbArticoloLabel.TabIndex = 84
    Me.lbArticoloLabel.Text = "Articolo"
    '
    'edArticolo
    '
    Me.edArticolo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edArticolo.Location = New System.Drawing.Point(98, 85)
    Me.edArticolo.Name = "edArticolo"
    Me.edArticolo.NTSDbField = ""
    Me.edArticolo.NTSForzaVisZoom = False
    Me.edArticolo.NTSOldValue = ""
    Me.edArticolo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edArticolo.Properties.MaxLength = 65536
    Me.edArticolo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edArticolo.Size = New System.Drawing.Size(108, 20)
    Me.edArticolo.TabIndex = 85
    '
    'fmFiltri
    '
    Me.fmFiltri.AllowDrop = True
    Me.fmFiltri.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmFiltri.Appearance.Options.UseBackColor = True
    Me.fmFiltri.Controls.Add(Me.cbEscomp)
    Me.fmFiltri.Controls.Add(Me.edEsaltro)
    Me.fmFiltri.Controls.Add(Me.lbEscomp)
    Me.fmFiltri.Controls.Add(Me.lbNota)
    Me.fmFiltri.Controls.Add(Me.lbAnnoDoc)
    Me.fmFiltri.Controls.Add(Me.lbtipoDoc)
    Me.fmFiltri.Controls.Add(Me.edAnnoDoc)
    Me.fmFiltri.Controls.Add(Me.cbTipoDoc)
    Me.fmFiltri.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmFiltri.Location = New System.Drawing.Point(6, 116)
    Me.fmFiltri.Name = "fmFiltri"
    Me.fmFiltri.Size = New System.Drawing.Size(513, 103)
    Me.fmFiltri.TabIndex = 86
    Me.fmFiltri.Text = "Filtri impostazioni"
    '
    'cbEscomp
    '
    Me.cbEscomp.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbEscomp.DataSource = Nothing
    Me.cbEscomp.DisplayMember = ""
    Me.cbEscomp.Location = New System.Drawing.Point(92, 77)
    Me.cbEscomp.Name = "cbEscomp"
    Me.cbEscomp.NTSDbField = ""
    Me.cbEscomp.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbEscomp.Properties.Appearance.Options.UseBackColor = True
    Me.cbEscomp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbEscomp.Properties.DropDownRows = 30
    Me.cbEscomp.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbEscomp.SelectedValue = ""
    Me.cbEscomp.Size = New System.Drawing.Size(115, 20)
    Me.cbEscomp.TabIndex = 89
    Me.cbEscomp.ValueMember = ""
    '
    'edEsaltro
    '
    Me.edEsaltro.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEsaltro.EditValue = "0"
    Me.edEsaltro.Location = New System.Drawing.Point(247, 77)
    Me.edEsaltro.Name = "edEsaltro"
    Me.edEsaltro.NTSDbField = ""
    Me.edEsaltro.NTSFormat = "0"
    Me.edEsaltro.NTSForzaVisZoom = False
    Me.edEsaltro.NTSOldValue = ""
    Me.edEsaltro.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edEsaltro.Properties.Appearance.Options.UseBackColor = True
    Me.edEsaltro.Properties.Appearance.Options.UseTextOptions = True
    Me.edEsaltro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEsaltro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEsaltro.Properties.MaxLength = 65536
    Me.edEsaltro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEsaltro.Size = New System.Drawing.Size(100, 20)
    Me.edEsaltro.TabIndex = 90
    '
    'lbEscomp
    '
    Me.lbEscomp.AutoSize = True
    Me.lbEscomp.BackColor = System.Drawing.Color.Transparent
    Me.lbEscomp.Location = New System.Drawing.Point(3, 80)
    Me.lbEscomp.Name = "lbEscomp"
    Me.lbEscomp.NTSDbField = ""
    Me.lbEscomp.Size = New System.Drawing.Size(91, 13)
    Me.lbEscomp.TabIndex = 88
    Me.lbEscomp.Text = "Esercizio di comp."
    '
    'lbNota
    '
    Me.lbNota.AutoSize = True
    Me.lbNota.BackColor = System.Drawing.Color.Transparent
    Me.lbNota.Location = New System.Drawing.Point(164, 54)
    Me.lbNota.Name = "lbNota"
    Me.lbNota.NTSDbField = ""
    Me.lbNota.Size = New System.Drawing.Size(333, 13)
    Me.lbNota.TabIndex = 87
    Me.lbNota.Text = "<-- applicabile solo per apertura/creazione documenti/ordini/impegni"
    '
    'lbAnnoDoc
    '
    Me.lbAnnoDoc.AutoSize = True
    Me.lbAnnoDoc.BackColor = System.Drawing.Color.Transparent
    Me.lbAnnoDoc.Location = New System.Drawing.Point(3, 54)
    Me.lbAnnoDoc.Name = "lbAnnoDoc"
    Me.lbAnnoDoc.NTSDbField = ""
    Me.lbAnnoDoc.Size = New System.Drawing.Size(88, 13)
    Me.lbAnnoDoc.TabIndex = 86
    Me.lbAnnoDoc.Text = "Anno documento"
    '
    'lbtipoDoc
    '
    Me.lbtipoDoc.AutoSize = True
    Me.lbtipoDoc.BackColor = System.Drawing.Color.Transparent
    Me.lbtipoDoc.Location = New System.Drawing.Point(3, 28)
    Me.lbtipoDoc.Name = "lbtipoDoc"
    Me.lbtipoDoc.NTSDbField = ""
    Me.lbtipoDoc.Size = New System.Drawing.Size(83, 13)
    Me.lbtipoDoc.TabIndex = 85
    Me.lbtipoDoc.Text = "Tipo documento"
    '
    'edAnnoDoc
    '
    Me.edAnnoDoc.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAnnoDoc.EditValue = "2008"
    Me.edAnnoDoc.Location = New System.Drawing.Point(92, 51)
    Me.edAnnoDoc.Name = "edAnnoDoc"
    Me.edAnnoDoc.NTSDbField = ""
    Me.edAnnoDoc.NTSFormat = "0"
    Me.edAnnoDoc.NTSForzaVisZoom = False
    Me.edAnnoDoc.NTSOldValue = "2008"
    Me.edAnnoDoc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAnnoDoc.Properties.Appearance.Options.UseBackColor = True
    Me.edAnnoDoc.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnnoDoc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnnoDoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnnoDoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnnoDoc.Size = New System.Drawing.Size(54, 20)
    Me.edAnnoDoc.TabIndex = 44
    '
    'cbTipoDoc
    '
    Me.cbTipoDoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipoDoc.DataSource = Nothing
    Me.cbTipoDoc.DisplayMember = ""
    Me.cbTipoDoc.Location = New System.Drawing.Point(92, 25)
    Me.cbTipoDoc.Name = "cbTipoDoc"
    Me.cbTipoDoc.NTSDbField = ""
    Me.cbTipoDoc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTipoDoc.Properties.Appearance.Options.UseBackColor = True
    Me.cbTipoDoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipoDoc.Properties.DropDownRows = 30
    Me.cbTipoDoc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipoDoc.SelectedValue = ""
    Me.cbTipoDoc.Size = New System.Drawing.Size(255, 20)
    Me.cbTipoDoc.TabIndex = 43
    Me.cbTipoDoc.ValueMember = ""
    '
    'pnMain
    '
    Me.pnMain.AllowDrop = True
    Me.pnMain.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnMain.Appearance.Options.UseBackColor = True
    Me.pnMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnMain.Controls.Add(Me.ckFiltri)
    Me.pnMain.Controls.Add(Me.fmFiltri)
    Me.pnMain.Controls.Add(Me.edDatfin)
    Me.pnMain.Controls.Add(Me.edArticolo)
    Me.pnMain.Controls.Add(Me.edDatini)
    Me.pnMain.Controls.Add(Me.lbArticoloLabel)
    Me.pnMain.Controls.Add(Me.lbDataAl)
    Me.pnMain.Controls.Add(Me.lbArticolo)
    Me.pnMain.Controls.Add(Me.lbDataDal)
    Me.pnMain.Controls.Add(Me.lbContoLabel)
    Me.pnMain.Controls.Add(Me.lbParentesi)
    Me.pnMain.Controls.Add(Me.lbConto)
    Me.pnMain.Controls.Add(Me.edConto)
    Me.pnMain.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnMain.Location = New System.Drawing.Point(0, 32)
    Me.pnMain.Name = "pnMain"
    Me.pnMain.Size = New System.Drawing.Size(526, 232)
    Me.pnMain.TabIndex = 87
    Me.pnMain.Text = "NtsPanel1"
    '
    'FRM__CONS
    '
    Me.ClientSize = New System.Drawing.Size(738, 316)
    Me.Controls.Add(Me.pnMain)
    Me.Controls.Add(Me.cmdStatistiche)
    Me.Controls.Add(Me.cmdDatiCont)
    Me.Controls.Add(Me.cmdArtprox)
    Me.Controls.Add(Me.cmdArtpro)
    Me.Controls.Add(Me.cmdVisScad)
    Me.Controls.Add(Me.cmdVisPart)
    Me.Controls.Add(Me.cmdVisMovmag)
    Me.Controls.Add(Me.cmdVisOrd)
    Me.Controls.Add(Me.cmdNuovoDoc)
    Me.Controls.Add(Me.cmdApriDoc)
    Me.Controls.Add(Me.cmdNuovaAnagraf)
    Me.Controls.Add(Me.cmdApriAnagraf)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.HelpContext = ""
    Me.MaximizeBox = False
    Me.Name = "FRM__CONS"
    Me.Text = "CONSOLE CLIENTI/FORNITORI/ARTICOLI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckFiltri.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatfin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edArticolo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmFiltri, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmFiltri.ResumeLayout(False)
    Me.fmFiltri.PerformLayout()
    CType(Me.cbEscomp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEsaltro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnnoDoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipoDoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnMain, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnMain.ResumeLayout(False)
    Me.pnMain.PerformLayout()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__CONS", "BE__CONS", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128587983450625000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleCons = CType(oTmp, CLE__CONS)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__CONS", strRemoteServer, strRemotePort)
    AddHandler oCleCons.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleCons.Init(oApp, oScript, oMenu.oCleComm, "TABZONE", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)

    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      Dim dttEscomp As New DataTable()
      dttEscomp.Columns.Add("cod", GetType(String))
      dttEscomp.Columns.Add("val", GetType(String))
      dttEscomp.Rows.Add(New Object() {"C", "Corrente"})
      dttEscomp.Rows.Add(New Object() {"P", "Precedente"})
      dttEscomp.Rows.Add(New Object() {"A", "Altro"})
      dttEscomp.AcceptChanges()
      cbEscomp.DataSource = dttEscomp
      cbEscomp.DisplayMember = "val"
      cbEscomp.ValueMember = "cod"
      cbEscomp.SelectedValue = "C"

      Dim dttTipoRk As New DataTable()

      dttTipoRk.Columns.Add("cod", GetType(String))
      dttTipoRk.Columns.Add("val", GetType(String))
      dttTipoRk.Rows.Add(New Object() {"§", "Tutti"})

      dttTipoRk.Rows.Add(New Object() {"R", "Impegno cliente"})
      dttTipoRk.Rows.Add(New Object() {"O", "Ordine fornitore"})
      dttTipoRk.Rows.Add(New Object() {"H", "Ordine di produzione"})
      dttTipoRk.Rows.Add(New Object() {"X", "Impegno Trasferimento"})
      dttTipoRk.Rows.Add(New Object() {"Q", "Preventivo"})
      dttTipoRk.Rows.Add(New Object() {"#", "Impegno di commessa"})
      dttTipoRk.Rows.Add(New Object() {"V", "Impegno cliente aperto"})
      dttTipoRk.Rows.Add(New Object() {"$", "Ordine fornitore aperto"})
      dttTipoRk.Rows.Add(New Object() {"Y", "Impegno di produzione"})

      dttTipoRk.Rows.Add(New Object() {"A", "Fatture immediate emesse"})
      dttTipoRk.Rows.Add(New Object() {"B", "D.D.T. emessi"})
      dttTipoRk.Rows.Add(New Object() {"C", "Corrispettivi emessi"})
      dttTipoRk.Rows.Add(New Object() {"D", "Fatture differite emesse"})
      dttTipoRk.Rows.Add(New Object() {"E", "Note di addebito emesse"})
      dttTipoRk.Rows.Add(New Object() {"F", "Ricevute fiscali emesse"})
      dttTipoRk.Rows.Add(New Object() {"i", "Riemissione ricevute fiscali"})
      dttTipoRk.Rows.Add(New Object() {"J", "Note di accredito ricevute"})
      dttTipoRk.Rows.Add(New Object() {"(", "Note di accredito differite ricevute"})
      dttTipoRk.Rows.Add(New Object() {"L", "Fatture immediate ricevute"})
      dttTipoRk.Rows.Add(New Object() {"M", "D.D.T. ricevuti"})
      dttTipoRk.Rows.Add(New Object() {"K", "Fatture differite ricevute"})
      dttTipoRk.Rows.Add(New Object() {"P", "Fatture/ricevute fiscali differite"})
      dttTipoRk.Rows.Add(New Object() {"N", "Note di accredito emesse"})
      dttTipoRk.Rows.Add(New Object() {"£", "Note di accredito differite emesse"})
      dttTipoRk.Rows.Add(New Object() {"S", "Fatture/ricevute fiscali emesse"})
      dttTipoRk.Rows.Add(New Object() {"T", "Carichi da produzione"})
      dttTipoRk.Rows.Add(New Object() {"U", "Scarichi a produzione"})
      dttTipoRk.Rows.Add(New Object() {"W", "Note di prelievo"})
      dttTipoRk.Rows.Add(New Object() {"Z", "Bolle di movimentazione interna"})

      cbTipoDoc.DataSource = dttTipoRk
      cbTipoDoc.ValueMember = "cod"
      cbTipoDoc.DisplayMember = "val"

      edAnnoDoc.NTSSetParam(oMenu, oApp.Tr(Me, 128588815825156250, "Anno documento"), "0", 4, 1900, 2099)
      cbTipoDoc.NTSSetParam(oApp.Tr(Me, 128782966222006000, "Tipo documento"))
      edArticolo.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128782966233394000, "Articolo"), tabartico, True)
      edConto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128782966242910000, "Conto cliente/fornitore"), tabanagra)
      edDatini.NTSSetParam(oMenu, oApp.Tr(Me, 128782966251334000, "Data inizio"), True)
      edDatfin.NTSSetParam(oMenu, oApp.Tr(Me, 128782966260382000, "Data fine"), False)
      ckFiltri.NTSSetParam(oMenu, oApp.Tr(Me, 128782966269274000, "Richiedi ulteriori filtri nella finestra richiamata"), "S", "N")
      cbEscomp.NTSSetParam(oApp.Tr(Me, 128782966277386000, "Esercizio di competenza"))
      edEsaltro.NTSSetParam(oMenu, oApp.Tr(Me, 128782966284856000, "Altro esercizio"), "0")
      edEsaltro.NTSSetParamZoom("ZOOMTABESCO")

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

#Region "Eventi di Form"
  Public Overridable Sub FRM__CONS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim strDriver As String = ""
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      ckFiltri.Checked = CBool(oMenu.GetSettingBus("BS--CONS", "OPZIONI", ".", "RichiediUlterioriFiltri", "0", " ", "0"))
      strDriver = oMenu.GetSettingBus("BS--CONS", "RECENT", ".", "TipoDriver", "C", " ", "C")
      edEsaltro.Visible = False
      edDatfin.Text = DateTime.Now.ToShortDateString

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlTipoDoc = "C"         'C'lienti, 'F'ornitori, 'A'rticoli
      GctlSetRoules()
      GctlApplicaDefaultValue()

      If strDriver <> "C" Then
        If strDriver = "F" Then
          tlbFornitori.Down = True
          tlbFornitori_ItemClick(tlbFornitori, Nothing)
        End If

        If strDriver = "A" Then
          tlbArticoli.Down = True
          tlbArticoli_ItemClick(tlbArticoli, Nothing)
        End If
      End If
      StatoPulsanti()


      If Not oCallParams Is Nothing Then
        edConto.Text = NTSCInt(Microsoft.VisualBasic.Mid(oCallParams.strParam, 1, 9)).ToString
        strDriver = Microsoft.VisualBasic.Mid(oCallParams.strParam, 11, 1)
        If strDriver = "C" And tlbClienti.Down = False Then
          tlbClienti.Down = True
          tlbClienti_ItemClick(tlbClienti, Nothing)
        End If
        If strDriver = "F" And tlbFornitori.Down = False Then
          tlbFornitori.Down = True
          tlbFornitori_ItemClick(tlbFornitori, Nothing)
        End If
        If strDriver = "A" And tlbArticoli.Down = False Then
          tlbArticoli.Down = True
          tlbArticoli_ItemClick(tlbArticoli, Nothing)
        End If
      End If

      edAnnoDoc.Text = DateTime.Now.Year.ToString

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__CONS_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Try
      If e.Alt = False And e.Control = False And e.Shift = False And e.KeyCode = Keys.F3 And cmdApriAnagraf.Enabled Then
        cmdApriAnagraf_Click(cmdApriAnagraf, Nothing)
        e.Handled = True
      End If

      If e.Alt = False And e.Control = False And e.Shift = False And e.KeyCode = Keys.F2 And cmdNuovaAnagraf.Enabled Then
        cmdNuovaAnagraf_Click(cmdNuovaAnagraf, Nothing)
        e.Handled = True
      End If

      If e.Alt = False And e.Control = False And e.Shift = True And e.KeyCode = Keys.F3 And cmdApriDoc.Enabled Then
        cmdApriDoc_Click(cmdApriDoc, Nothing)
        e.Handled = True
      End If

      If e.Alt = False And e.Control = False And e.Shift = True And e.KeyCode = Keys.F2 And cmdNuovoDoc.Enabled Then
        cmdNuovoDoc_Click(cmdNuovoDoc, Nothing)
        e.Handled = True
      End If

      If e.Alt = False And e.Control = True And e.Shift = False And e.KeyCode = Keys.O And cmdVisOrd.Enabled Then
        cmdVisOrd_Click(cmdVisOrd, Nothing)
        e.Handled = True
      End If

      If e.Alt = False And e.Control = True And e.Shift = False And e.KeyCode = Keys.M And cmdVisMovmag.Enabled Then
        cmdVisMovmag_Click(cmdVisMovmag, Nothing)
        e.Handled = True
      End If

      If e.Alt = False And e.Control = True And e.Shift = False And e.KeyCode = Keys.P And cmdVisPart.Enabled Then
        cmdVisPart_Click(cmdVisPart, Nothing)
        e.Handled = True
      End If

      If e.Alt = False And e.Control = True And e.Shift = False And e.KeyCode = Keys.Z And cmdVisScad.Enabled Then
        cmdVisScad_Click(cmdVisScad, Nothing)
        e.Handled = True
      End If

      If e.Alt = False And e.Control = True And e.Shift = False And e.KeyCode = Keys.D And cmdDatiCont.Enabled Then
        cmdDatiCont_Click(cmdDatiCont, Nothing)
        e.Handled = True
      End If

      If e.Alt = False And e.Control = True And e.Shift = False And e.KeyCode = Keys.S And cmdStatistiche.Enabled Then
        cmdStatistiche_Click(cmdStatistiche, Nothing)
        e.Handled = True
      End If

      If e.Alt = False And e.Control = True And e.Shift = False And e.KeyCode = Keys.E And cmdArtpro.Enabled Then
        cmdArtpro_Click(cmdArtpro, Nothing)
        e.Handled = True
      End If

      If e.Alt = False And e.Control = True And e.Shift = False And e.KeyCode = Keys.T And cmdArtprox.Enabled Then
        cmdArtprox_Click(cmdArtprox, Nothing)
        e.Handled = True
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub FRM__CONS_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Dim strDriver As String = ""
    Try
      If tlbClienti.Down Then strDriver = "C"
      If tlbFornitori.Down Then strDriver = "F"
      If tlbArticoli.Down Then strDriver = "A"
      oMenu.SaveSettingBus("BS--CONS", "RECENT", ".", "TipoDriver", strDriver, " ", "NS.", "...", "...")

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbClienti_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbClienti.ItemClick
    Try
      If tlbClienti.Down Then
        tlbFornitori.Down = False
        tlbArticoli.Down = False
        GctlTipoDoc = "C"         'C'lienti, 'F'ornitori, 'A'rticoli
        GctlSetRoules()
        GctlApplicaDefaultValue()
      Else
        If tlbClienti.Down = False Then tlbClienti.Down = True
      End If
      StatoPulsanti()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbFornitori_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbFornitori.ItemClick
    Try
      If tlbFornitori.Down Then
        tlbClienti.Down = False
        tlbArticoli.Down = False
        GctlTipoDoc = "F"         'C'lienti, 'F'ornitori, 'A'rticoli
        GctlSetRoules()
        GctlApplicaDefaultValue()
      Else
        If tlbFornitori.Down = False Then tlbFornitori.Down = True
      End If
      StatoPulsanti()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbArticoli_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbArticoli.ItemClick
    Try
      If tlbArticoli.Down Then
        tlbClienti.Down = False
        tlbFornitori.Down = False
        GctlTipoDoc = "A"         'C'lienti, 'F'ornitori, 'A'rticoli
        GctlSetRoules()
        GctlApplicaDefaultValue()
      Else
        If tlbArticoli.Down = False Then tlbArticoli.Down = True
      End If
      StatoPulsanti()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      'zoom standard
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      If edConto.Focused Then
        SetFastZoom(edConto.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edConto.Text
        oParam.bVisGriglia = True
        oParam.bTipoProposto = True
        oParam.nMastro = 0
        If tlbFornitori.Down Then
          oParam.strTipo = "F"
        Else
          oParam.strTipo = "C"
        End If
        NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edConto.Text Then edConto.NTSTextDB = NTSZOOM.strIn
      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        NTSCallStandardZoom()
      End If

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
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region


  Public Overridable Sub StatoPulsanti()
    Try
      If tlbClienti.Down Or tlbFornitori.Down Then
        cmdArtpro.Enabled = False
        cmdArtprox.Enabled = False
      Else
        GctlSetVisEnab(cmdArtpro, False)
        GctlSetVisEnab(cmdArtprox, False)
      End If

      If tlbArticoli.Down Then
        cmdApriDoc.Enabled = False
        cmdNuovoDoc.Enabled = False
        cmdVisPart.Enabled = False
        cmdVisScad.Enabled = False
        cmdDatiCont.Enabled = False
        cmdStatistiche.Enabled = False
      Else
        GctlSetVisEnab(cmdApriDoc, False)
        GctlSetVisEnab(cmdNuovoDoc, False)
        GctlSetVisEnab(cmdVisPart, False)
        GctlSetVisEnab(cmdVisScad, False)
        GctlSetVisEnab(cmdDatiCont, False)
        GctlSetVisEnab(cmdStatistiche, False)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


  Public Overridable Sub cbEscomp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbEscomp.SelectedIndexChanged
    Dim strTmp As String = ""
    Dim dtTmp As DateTime = Nothing
    Try
      If cbEscomp.SelectedValue.ToString = "A" Then
        edEsaltro.Visible = True
      Else
        edEsaltro.Visible = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edConto_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edConto.Validated
    Dim strDescr As String = ""
    Try
      If Not oCleCons.edConto_Validated(NTSCInt(edConto.Text), strDescr) Then
        edConto.Text = "0"
        lbConto.Text = ""
      Else
        lbConto.Text = strDescr
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edArticolo_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edArticolo.Validated
    Dim strDescr As String = ""
    Try
      If Not oCleCons.edArticolo_Validated(edArticolo.Text, strDescr) Then
        edArticolo.Text = ""
        lbArticolo.Text = ""
      Else
        lbArticolo.Text = strDescr
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edEsaltro_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edEsaltro.Validated
    Try
      If Not oCleCons.edEsaltro_Validated(NTSCInt(edEsaltro.Text)) Then
        edEsaltro.Text = NTSCStr(edEsaltro.OldEditValue)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  '---------------------------

  Public Overridable Sub cmdApriAnagraf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApriAnagraf.Click
    Dim strParam As String = ""
    Try
      Me.ValidaLastControl()
      If Not CheckSelection() Then Return

      If tlbClienti.Down Then
        If NTSCInt(edConto.Text) = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128782966657256000, "Indicare un cliente/fornitore valido."))
          Return
        End If
        strParam = "APRI;C;" & edConto.Text.PadLeft(9, "0"c)
        oMenu.RunChild("BS__CLIE", "CLS__CLIE", oApp.Tr(Me, 128782966722152000, "Anagrafica Clienti"), DittaCorrente, "", "", Nothing, strParam, True, True)
      End If

      If tlbFornitori.Down Then
        If NTSCInt(edConto.Text) = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128589361750000000, "Indicare un cliente/fornitore valido."))
          Return
        End If
        strParam = "APRI;F;" & edConto.Text.PadLeft(9, "0"c)
        oMenu.RunChild("BS__CLIE", "CLS__CLIE", oApp.Tr(Me, 128782966736348000, "Anagrafica Fornitori"), DittaCorrente, "", "", Nothing, strParam, True, True)
      End If

      If tlbArticoli.Down Then
        If edArticolo.Text.Trim = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128589362209062500, "Indicare un codice articolo valido."))
          Return
        End If
        strParam = "APRI;" & edArticolo.Text
        oMenu.RunChild("BSMGARTI", "CLSMGARTI", oApp.Tr(Me, 128782966750076000, "Anagrafica articoli"), DittaCorrente, "", "", Nothing, strParam, True, True)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdNuovaAnagraf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNuovaAnagraf.Click
    Dim strParam As String = ""
    Try
      Me.ValidaLastControl()
      If Not CheckSelection() Then Return

      If tlbClienti.Down Then
        strParam = "NUOV;C;000000000"
        oMenu.RunChild("BS__CLIE", "CLS__CLIE", oApp.Tr(Me, 128588844510000000, "Anagrafica Clienti"), DittaCorrente, "", "", Nothing, strParam, True, True)
      End If

      If tlbFornitori.Down Then
        strParam = "NUOV;F;000000000"
        oMenu.RunChild("BS__CLIE", "CLS__CLIE", oApp.Tr(Me, 128588844938281250, "Anagrafica Fornitori"), DittaCorrente, "", "", Nothing, strParam, True, True)
      End If

      If tlbArticoli.Down Then
        strParam = "NUOV;"
        oMenu.RunChild("BSMGARTI", "CLSMGARTI", oApp.Tr(Me, 128588842722031250, "Anagrafica articoli"), DittaCorrente, "", "", Nothing, strParam, True, True)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdApriDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApriDoc.Click
    Dim strParam As String = ""
    Dim strSerie As String = ""
    Dim lNumdoc As Integer = 0
    Dim strWhere As String = ""
    Dim oPar As New CLE__PATB
    Try
      Me.ValidaLastControl()
      If Not CheckSelection() Then Return

      If tlbArticoli.Down Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128589364500312500, "Il tipo di consultazione deve essere per 'Cliente' o per 'Fornitore'" & vbCrLf & "per l'apertura di un documento."))
        Return
      End If

      If NTSCInt(edConto.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128589362853593750, "Indicare un cliente/fornitore prima di passare all'apertura di un documento."))
        Return
      End If

      If cbTipoDoc.SelectedValue = "§" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128589363656406250, "Indicare un tipo ordine/documento valido da aprire."))
        cbTipoDoc.Focus()
        Return
      End If

      Select Case cbTipoDoc.SelectedValue
        Case "$", "H", "O", "Q", "R", "V", "X", "Y"
          strWhere = oCleCons.GetWhereHltd(cbTipoDoc.SelectedValue, NTSCInt(edAnnoDoc.Text), NTSCInt(edConto.Text), edDatini.Text, edDatfin.Text)

          oPar.strDescr = strWhere
          oPar.nTipologia = 0       '0 = posso selez una sola riga, 1 posso fare multiselezione
          NTSZOOM.ZoomStrIn("ZOOMTESTORD", DittaCorrente, oPar)        'in vb6 la dohltd
          If oPar.oParam Is Nothing Then Return
          'se non premuto 'annulla' in oPar.oParam viene restituito l'elenco delle righe della griglia da trattare!!!
          If CType(oPar.oParam, DataTable).Rows.Count = 0 Then Return
          lNumdoc = NTSCInt(CType(oPar.oParam, DataTable).Rows(0)!td_numord)
          strSerie = NTSCStr(CType(oPar.oParam, DataTable).Rows(0)!td_serie)
          If lNumdoc = 0 Then Return

          strParam = "APRI;" & cbTipoDoc.SelectedValue & ";" & _
                     edAnnoDoc.Text.PadLeft(4, "0"c) & ";" & _
                     strSerie & ";" & _
                     lNumdoc.ToString.PadLeft(9, "0"c) & ";"
          oMenu.RunChild("BSORGSOR", "CLSORGSOR", oApp.Tr(Me, 128782966865846000, "Gestione ordini/impegno"), DittaCorrente, "", "", Nothing, strParam, True, True)

        Case "D", "K", "P", "£", "("
          strWhere = oCleCons.GetWhereHltm(cbTipoDoc.SelectedValue, NTSCInt(edAnnoDoc.Text), NTSCInt(edConto.Text), edDatini.Text, edDatfin.Text)

          oPar.strDescr = strWhere
          oPar.nTipologia = 0       '0 = posso selez una sola riga, 1 posso fare multiselezione
          oPar.strTipo = cbTipoDoc.SelectedValue
          NTSZOOM.ZoomStrIn("ZOOMTESTMAG", DittaCorrente, oPar)        'in vb6 la dohltm
          If oPar.oParam Is Nothing Then Return
          'se non premuto 'annulla' in oPar.oParam viene restituito l'elenco delle righe della griglia da trattare!!!
          If CType(oPar.oParam, DataTable).Rows.Count = 0 Then Return
          lNumdoc = NTSCInt(CType(oPar.oParam, DataTable).Rows(0)!tm_numdoc)
          strSerie = NTSCStr(CType(oPar.oParam, DataTable).Rows(0)!tm_serie)
          If lNumdoc = 0 Then Return

          strParam = "APRI;" & cbTipoDoc.SelectedValue & ";" & _
                     edAnnoDoc.Text.PadLeft(4, "0"c) & ";" & _
                     strSerie & ";" & _
                     lNumdoc.ToString.PadLeft(9, "0"c) & ";"
          oMenu.RunChild("BSVEFDIN", "CLSVEFDIN", oApp.Tr(Me, 128589418181718750, "Fatturazione differita interattiva"), DittaCorrente, "", "", Nothing, strParam, True, True)

        Case Else
          strWhere = oCleCons.GetWhereHltm(cbTipoDoc.SelectedValue, NTSCInt(edAnnoDoc.Text), NTSCInt(edConto.Text), edDatini.Text, edDatfin.Text)

          oPar.strDescr = strWhere
          oPar.nTipologia = 0       '0 = posso selez una sola riga, 1 posso fare multiselezione
          oPar.strTipo = cbTipoDoc.SelectedValue
          NTSZOOM.ZoomStrIn("ZOOMTESTMAG", DittaCorrente, oPar)        'in vb6 la dohltm
          If oPar.oParam Is Nothing Then Return
          'se non premuto 'annulla' in oPar.oParam viene restituito l'elenco delle righe della griglia da trattare!!!
          If CType(oPar.oParam, DataTable).Rows.Count = 0 Then Return
          lNumdoc = NTSCInt(CType(oPar.oParam, DataTable).Rows(0)!tm_numdoc)
          strSerie = NTSCStr(CType(oPar.oParam, DataTable).Rows(0)!tm_serie)
          If lNumdoc = 0 Then Return

          strParam = "APRI;" & cbTipoDoc.SelectedValue & ";" & _
                     edAnnoDoc.Text.PadLeft(4, "0"c) & ";" & _
                     strSerie & ";" & _
                     lNumdoc.ToString.PadLeft(9, "0"c) & ";"
          oMenu.RunChild("BSVEBOLL", "CLSVEBOLL", oApp.Tr(Me, 128782966918886000, "Gestione documenti di magazzino"), DittaCorrente, "", "", Nothing, strParam, True, True)
      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdNuovoDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNuovoDoc.Click
    Dim strMsg As String = ""
    Dim strParam As String = ""
    Try
      Me.ValidaLastControl()
      If Not CheckSelection() Then Return

      If tlbArticoli.Down Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128589368807812500, "Il tipo di consultazione deve essere per 'Cliente' o per 'Fornitore'" & vbCrLf & "per la creazione di un documento."))
        Return
      End If

      If NTSCInt(edConto.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128589368618750000, "Indicare un cliente/fornitore prima di passare alla creazione di un documento."))
        Return
      End If

      If cbTipoDoc.SelectedValue = "§" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128589368487968750, "Indicare un tipo ordine/documento valido da creare."))
        cbTipoDoc.Focus()
        Return
      End If

      Select Case cbTipoDoc.SelectedValue
        Case "D" : strMsg = oApp.Tr(Me, 128589365181718750, "Impossibile creare una nuova Fattura differita emessa." & vbCrLf)
        Case "K" : strMsg = oApp.Tr(Me, 128589365410000000, "Impossibile creare una nuova Fattura differita ricevuta." & vbCrLf)
        Case "P" : strMsg = oApp.Tr(Me, 128589365395781250, "Impossibile creare una nuova Fattura/ricevuta fiscale differita." & vbCrLf)
        Case "£" : strMsg = oApp.Tr(Me, 129243505588750000, "Impossibile creare una nuova Nota accredito differita emessa." & vbCrLf)
        Case "(" : strMsg = oApp.Tr(Me, 129243505606269531, "Impossibile creare una nuova Nota accredito differita ricevuta." & vbCrLf)
        Case "U" : strMsg = oApp.Tr(Me, 128589365379687500, "Impossibile creare un nuovo Scarico a produzione." & vbCrLf)
        Case "Y" : strMsg = oApp.Tr(Me, 128589365363125000, "Impossibile creare un nuovo Impegno di produzione." & vbCrLf)
      End Select
      Select Case cbTipoDoc.SelectedValue
        Case "D", "K", "P", "U", "Y", "£", "("
          oApp.MsgBoxErr(strMsg & oApp.Tr(Me, 128589365351875000, "Ripetere la selezione."))
          cbTipoDoc.SelectedValue = "§"
          cbTipoDoc.Focus()
          Return
      End Select

      Select Case cbTipoDoc.SelectedValue
        Case "$", "H", "O", "Q", "R", "V", "X"
          strParam = "NUOD;" & cbTipoDoc.SelectedValue & ";" & edAnnoDoc.Text.PadLeft(4, "0"c) & ";" & edConto.Text.PadLeft(9, "0"c) & ";"
          oMenu.RunChild("BSORGSOR", "CLSORGSOR", oApp.Tr(Me, 128589367205937500, "Gestione ordini/impegno"), DittaCorrente, "", "", Nothing, strParam, True, True)

        Case "A", "B", "C", "E", "F", "I", "J", "L", "M", "N", "S", "T", "U", "W", "Z"
          strParam = "NUOD;" & cbTipoDoc.SelectedValue & ";" & edAnnoDoc.Text.PadLeft(4, "0"c) & ";" & edConto.Text.PadLeft(9, "0"c) & ";"
          oMenu.RunChild("BSVEBOLL", "CLSVEBOLL", oApp.Tr(Me, 128589367449843750, "Gestione documenti di magazzino"), DittaCorrente, "", "", Nothing, strParam, True, True)

      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdVisOrd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdVisOrd.Click
    Dim strParam As String = ""
    Try
      Me.ValidaLastControl()
      If Not CheckSelection() Then Return

      If NTSCInt(edConto.Text) = 0 And tlbArticoli.Down = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128782966978946000, "Se il tipo di consultazione è per Cliente o Fornitore'" & vbCrLf & _
                                                        "il cliente/fornitore deve essere obbligatoriamente indicato."))
        edConto.Focus()
        Return
      End If

      If edArticolo.Text.Trim = "" And tlbArticoli.Down = True Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128782966553056000, "Se il tipo di consultazione è per 'Articolo'" & vbCrLf & _
                                                        "il codice articolo deve essere obbligatoriamente indicato."))
        edArticolo.Focus()
        Return
      End If

      Select Case cbTipoDoc.SelectedValue
        Case "§", "$", "H", "O", "Q", "R", "V", "X", "Y"
        Case Else
          oApp.MsgBoxErr(oApp.Tr(Me, 128589371984062500, "Tipo ordine/documento selezionato non valido per la visualizzazione degli ordini/impegni."))
          cbTipoDoc.Focus()
          Return
      End Select

      '-----------------------------------------------------------------------------------------
      strParam = edConto.Text.PadLeft(9, "0"c) & ";" & _
                 IIf(NTSCInt(edConto.Text) = 0, "999999999", edConto.Text.PadLeft(9, "0"c)).ToString & ";"
      If edArticolo.Text = "" Then
        strParam += "".PadLeft(CLN__STD.CodartMaxLen) & ";" & "".PadLeft(CLN__STD.CodartMaxLen, "z"c) & ";"
      Else
        strParam += edArticolo.Text.ToUpper.PadRight(CLN__STD.CodartMaxLen) & ";" & edArticolo.Text.ToUpper.PadRight(CLN__STD.CodartMaxLen) & ";"
      End If
      strParam += cbTipoDoc.SelectedValue & ";" & _
                  IIf(ckFiltri.Checked, "1", "0").ToString & ";" & _
                  IIf(edDatini.Text = "", edDatini.Text.PadRight(10), NTSCDate(edDatini.Text).ToString("dd/MM/yy").PadRight(10)).ToString & ";" & _
                  IIf(edDatfin.Text = "", edDatfin.Text.PadRight(10), NTSCDate(edDatfin.Text).ToString("dd/MM/yy").PadRight(10)).ToString & ";"
      oMenu.RunChild("BSORSCHO", "CLSORSCHO", oApp.Tr(Me, 128589374808750000, "Stampa / visualizza schede ordini"), DittaCorrente, "", "", Nothing, strParam, True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdVisMovmag_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdVisMovmag.Click
    Dim strParam As String = ""
    Try
      Me.ValidaLastControl()
      If Not CheckSelection() Then Return

      If NTSCInt(edConto.Text) = 0 And tlbArticoli.Down = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128589371276718750, "Se il tipo di consultazione è per Cliente o Fornitore'" & vbCrLf & _
                                                        "il cliente/fornitore deve essere obbligatoriamente indicato."))
        edConto.Focus()
        Return
      End If

      If edArticolo.Text.Trim = "" And tlbArticoli.Down = True Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128589371694375000, "Se il tipo di consultazione è per 'Articolo'" & vbCrLf & _
                                                        "il codice articolo deve essere obbligatoriamente indicato."))
        edArticolo.Focus()
        Return
      End If

      Select Case cbTipoDoc.SelectedValue
        Case "§", "A", "B", "C", "D", "E", "F", "I", "J", "K", "L", "M", "N", "P", "S", "T", "U", "W", "Z", "£", "("
        Case Else
          oApp.MsgBoxErr(oApp.Tr(Me, 128589380989687500, "Tipo documento selezionato non valido."))
          cbTipoDoc.Focus()
          Return
      End Select

      '-----------------------------------------------------------------------------------------
      strParam = "APRD:" & edConto.Text.PadLeft(9, "0"c) & ";" & _
                 edArticolo.Text.ToUpper.PadRight(CLN__STD.CodartMaxLen) & ";" & _
                 cbTipoDoc.SelectedValue & ";" & _
                 IIf(edDatini.Text = "", edDatini.Text.PadRight(10), NTSCDate(edDatini.Text).ToString("dd/MM/yy").PadRight(10)).ToString & ";" & _
                 IIf(edDatfin.Text = "", edDatfin.Text.PadRight(10), NTSCDate(edDatfin.Text).ToString("dd/MM/yy").PadRight(10)).ToString & ";" & _
                 IIf(tlbArticoli.Down, "A", "C").ToString & ";" & _
                 IIf(ckFiltri.Checked, "1", "0").ToString & ";"
      oMenu.RunChild("BSMGSCHE", "CLSMGSCHE", oApp.Tr(Me, 128589381105937500, "Stampa / visualizza schede articoli"), DittaCorrente, "", "", Nothing, strParam, True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdVisPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdVisPart.Click
    Dim strParam As String = ""
    Try
      Me.ValidaLastControl()
      If Not CheckSelection() Then Return

      If tlbArticoli.Down = True Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128589385209218750, "Il tipo di consultazione deve essere per 'Cliente' o per 'Fornitore'" & vbCrLf & _
                                                        "per la visualizzazione del partitario."))
        Return
      End If

      If NTSCInt(edConto.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128782966401424000, "Indicare un cliente/fornitore valido prima di passare alla" & _
                                                        " visualizzazione del partitario."))
        edConto.Focus()
        Return
      End If

      If cbEscomp.SelectedValue = "A" And NTSCInt(edEsaltro.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128782966432312000, "Indicare un esercizio contabile diverso da 0."))
        edEsaltro.Focus()
        Return
      End If

      '-----------------------------------------------------------------------------------------
      strParam = cbEscomp.SelectedValue & ";"
      If cbEscomp.SelectedValue = "A" Then
        strParam += NTSCInt(edEsaltro.Text).ToString.PadLeft(4, "0"c) & ";"
      Else
        strParam += "0000;"
      End If
      strParam += "".PadLeft(30) & ";" & _
      IIf(edDatini.Text = "", edDatini.Text.PadRight(10), NTSCDate(edDatini.Text).ToString("dd/MM/yy").PadRight(10)).ToString & ";" & _
      IIf(edDatfin.Text = "", edDatfin.Text.PadRight(10), NTSCDate(edDatfin.Text).ToString("dd/MM/yy").PadRight(10)).ToString & ";" & _
      edConto.Text.PadLeft(9, "0"c) & ";" & _
      IIf(NTSCInt(edConto.Text) = 0, "999999999", edConto.Text.PadLeft(9, "0"c)).ToString & ";" & _
      IIf(ckFiltri.Checked, "1", "0").ToString & ";"
      oMenu.RunChild("BSCGPART", "CLSCGPART", oApp.Tr(Me, 128589389317656250, "Stampa / visualizza partitari"), DittaCorrente, "", "", Nothing, strParam, True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdVisScad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdVisScad.Click
    Dim strParam As String = ""
    Try
      Me.ValidaLastControl()
      If Not CheckSelection() Then Return

      If tlbArticoli.Down = True Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128589398157343750, "Il tipo di consultazione deve essere per 'Cliente' o per 'Fornitore'" & vbCrLf & _
                                                        "per la visualizzazione delle scadenze."))
        Return
      End If

      If NTSCInt(edConto.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128589398175000000, "Indicare un cliente/fornitore valido prima di passare alla" & _
                                                        " visualizzazione delle scadenze."))
        edConto.Focus()
        Return
      End If

      '-----------------------------------------------------------------------------------------
      strParam = "BS--CONS;1;" & _
                 edConto.Text.PadLeft(9, "0"c) & ";" & _
                 edConto.Text.PadLeft(9, "0"c) & ";" & _
                 IIf(ckFiltri.Checked, "1", "0").ToString & ";"
      oMenu.RunChild("BSCGSTSC", "CLSCGSTSC", oApp.Tr(Me, 128589391174375000, "Stampa/Visualizzazione scadenziario"), DittaCorrente, "", "", Nothing, strParam, True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdDatiCont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDatiCont.Click
    Dim oPar As New CLE__CLDP
    Dim oDcst As New CLE__DCST
    Dim nEscomp As Integer = 0
    Dim dttTmp As New DataTable
    Try
      Me.ValidaLastControl()
      If Not CheckSelection() Then Return

      If tlbArticoli.Down = True Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128589398255625000, "Il tipo di consultazione deve essere per 'Cliente' o per 'Fornitore'" & vbCrLf & _
                                                        "per la visualizzazione dei dati sintetici/statistici."))
        Return
      End If

      If NTSCInt(edConto.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128589398265312500, "Indicare un cliente/fornitore valido prima di passare alla" & _
                                                        " visualizzazione dei dati sintetici/statistici."))
        edConto.Focus()
        Return
      End If

      If cbEscomp.SelectedValue = "A" And NTSCInt(edEsaltro.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128589385442343750, "Indicare un esercizio contabile diverso da 0."))
        edEsaltro.Focus()
        Return
      End If
      nEscomp = NTSCInt(edEsaltro.Text)

      If cbEscomp.SelectedValue <> "A" Then
        oMenu.ValCodiceDb(DittaCorrente, DittaCorrente, "TABANAZ", "S", "", dttTmp)
        If cbEscomp.SelectedValue = "C" Then
          nEscomp = NTSCInt(dttTmp.Rows(0)!tb_escomp)
        Else
          nEscomp = NTSCInt(dttTmp.Rows(0)!tb_escompp)
        End If
        dttTmp.Clear()
      End If

      'struttura per passare i parametri a BNCGDCST e ricevere gli stessi compilati con i valori calcolati in BECGDCST
      With oDcst
        .lConto = NTSCInt(edConto.Text)
        .nEscomp = nEscomp
        .strData = NTSCDate(edDatfin.Text).ToShortDateString
        .bControllaFido = True
        .bControllaInsoluti = True
        .bStatPagamenti = True
        .dttMotrans = Nothing
        .dImpOdierno = 0
        .bOpendoc = False
        .strTipork = " "
        .nAnno = 0
        .strSerie = " "
        .lNumdoc = 0
        .bBlocca = False
        .bVisForm = True
        .bVismess = True
      End With

      oPar.ctlPar1 = oDcst
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BN__CONS"

      oMenu.RunChild("NTSInformatica", "FRMCGDCST", "", DittaCorrente, "", "BNCGDCST", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdStatistiche_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStatistiche.Click
    Dim strParam As String = ""
    Try
      Me.ValidaLastControl()
      If Not CheckSelection() Then Return

      If tlbArticoli.Down = True Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128589404995000000, "Selezionare un tipo di consultazione per cliente o fornitore per la visualizzazione dele statistiche."))
        Return
      End If

      Select Case cbTipoDoc.SelectedValue
        Case "§", "A", "B", "C", "D", "E", "F", "I", "J", "K", "L", "M", "N", "P", "S", "T", "U", "Z", "£", "("
        Case Else
          oApp.MsgBoxErr(oApp.Tr(Me, 128589405251250000, "Tipo documento selezionato non valido per la visualizzazione delle statistiche."))
          cbTipoDoc.Focus()
          Return
      End Select

      strParam = IIf(tlbClienti.Down, "C", "F").ToString & ";" & _
                 edConto.Text.PadLeft(9, "0"c) & ";" & _
                 IIf(NTSCInt(edConto.Text) = 0, "999999999", edConto.Text.PadLeft(9, "0"c)).ToString & ";" & _
                 cbTipoDoc.SelectedValue & ";" & _
                 IIf(edDatini.Text = "", edDatini.Text.PadRight(10), NTSCDate(edDatini.Text).ToString("dd/MM/yy").PadRight(10)).ToString & ";" & _
                 IIf(edDatfin.Text = "", edDatfin.Text.PadRight(10), NTSCDate(edDatfin.Text).ToString("dd/MM/yy").PadRight(10)).ToString & ";" & _
                 IIf(ckFiltri.Checked, "1", "0").ToString & ";"

      oMenu.RunChild("BSMGSCHC", "CLSMGSCHC", oApp.Tr(Me, 128589405613281250, "Stampa / visualizza schede clienti/fornitori"), DittaCorrente, "", "", Nothing, strParam, True, True)


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdArtpro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdArtpro.Click
    Dim oPar As New CLE__CLDP
    Try
      Me.ValidaLastControl()
      If Not CheckSelection() Then Return

      If tlbArticoli.Down = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128589413509531250, "Il tipo di consultazione deve essere per 'Articolo'" & vbCrLf & _
                                                        "per la visualizzazione progressivi/esistenza/disponibilità."))
        edConto.Focus()
        Return
      End If

      If edArticolo.Text.Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128782966479268000, "Indicare un codice articolo valido."))
        edArticolo.Focus()
        Return
      End If

      oPar.strPar1 = "BN__CONS"
      oPar.strPar2 = edArticolo.Text
      oPar.dPar1 = 0

      oMenu.RunChild("NTSInformatica", "FRMMGHLAP", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdArtprox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdArtprox.Click
    Dim oPar As New CLE__CLDP
    Dim dttTmp As New DataTable
    Try
      Me.ValidaLastControl()
      If Not CheckSelection() Then Return

      If tlbArticoli.Down = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128589411221406250, "Il tipo di consultazione deve essere per 'Articolo'" & vbCrLf & _
                                                        "per la visualizzazione progressivi/esistenza/disponibilità totali."))
        edConto.Focus()
        Return
      End If

      If edArticolo.Text.Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128589411276406250, "Indicare un codice articolo valido."))
        edArticolo.Focus()
        Return
      End If

      oMenu.ValCodiceDb(edArticolo.Text, DittaCorrente, "ARTICO", "S", "", dttTmp)
      oPar.dPar1 = NTSCDec(dttTmp.Rows(0)!ar_perqta)
      dttTmp.Clear()
      oPar.strPar1 = "BN__CONS"
      oPar.strPar2 = edArticolo.Text
      oPar.dPar2 = 0

      oMenu.RunChild("NTSInformatica", "FRMMGHLAT", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function CheckSelection() As Boolean
    Try
      If edDatini.Text <> "" Then
        If CDate(edDatini.Text) > CDate(edDatfin.Text) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128668900405217443, "La data iniziale (dal), se indicata, non può essere superiore a quella finale (al)."))
          Return False
        End If
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

End Class
