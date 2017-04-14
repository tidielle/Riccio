Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGELSA

#Region "Variabili"
  Public oCleLsar As CLEMGLSAR
  Public oCallParams As CLE__CLDP
  Public dsElsa As DataSet
  Public dcElsa As BindingSource = New BindingSource()
  Public bColonnaArticolo As Boolean

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents grElsa As NTSInformatica.NTSGrid
  Public WithEvents grvElsa As NTSInformatica.NTSGridView
  Public WithEvents codditt As NTSInformatica.NTSGridColumn
  Public WithEvents lsa_codart As NTSInformatica.NTSGridColumn
  Public WithEvents lsa_note As NTSInformatica.NTSGridColumn
  Public WithEvents lsa_codlsar As NTSInformatica.NTSGridColumn
  Public WithEvents lsa_riga As NTSInformatica.NTSGridColumn
  Public WithEvents lsa_flag As NTSInformatica.NTSGridColumn
  Public WithEvents lsa_fase As NTSInformatica.NTSGridColumn
  Public WithEvents lsa_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents xx_lottox As NTSInformatica.NTSGridColumn
  Public WithEvents lsa_ubicaz As NTSInformatica.NTSGridColumn
  Public WithEvents lsa_matric As NTSInformatica.NTSGridColumn
  Public WithEvents lsa_esist As NTSInformatica.NTSGridColumn
  Public WithEvents lsa_trattato As NTSInformatica.NTSGridColumn
  Public WithEvents lsa_int1 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descr As NTSInformatica.NTSGridColumn
  Public WithEvents xx_desint As NTSInformatica.NTSGridColumn
  Public WithEvents xx_unmis As NTSInformatica.NTSGridColumn
  Public WithEvents xx_fase As NTSInformatica.NTSGridColumn
  Public WithEvents xx_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents tlbStampaWord As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnBottom As NTSInformatica.NTSPanel
  Public WithEvents cmdCancella As NTSInformatica.NTSButton
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents tlbRecordBlocca As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRecordSblocca As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents pnRight As NTSInformatica.NTSPanel
#End Region

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGELSA))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbFileOrdina = New NTSInformatica.NTSBarMenuItem
    Me.tlbFileOrdinaDescr = New NTSInformatica.NTSBarMenuItem
    Me.tlbFileOrdinaCodalt = New NTSInformatica.NTSBarMenuItem
    Me.tlbImpostaStato = New NTSInformatica.NTSBarMenuItem
    Me.tlbImpostaTerm = New NTSInformatica.NTSBarMenuItem
    Me.tlbImportaTerm = New NTSInformatica.NTSBarMenuItem
    Me.tlbSeleziona = New NTSInformatica.NTSBarButtonItem
    Me.tlbDeseleziona = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancellaRigheSel = New NTSInformatica.NTSBarButtonItem
    Me.tlbSelezLottoUbicaz = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecordBlocca = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecordSblocca = New NTSInformatica.NTSBarButtonItem
    Me.tlbTrattatoS = New NTSInformatica.NTSBarButtonItem
    Me.tlbTrattatoN = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaWord = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grElsa = New NTSInformatica.NTSGrid
    Me.grvElsa = New NTSInformatica.NTSGridView
    Me.xx_sel = New NTSInformatica.NTSGridColumn
    Me.lsa_codart = New NTSInformatica.NTSGridColumn
    Me.xx_descr = New NTSInformatica.NTSGridColumn
    Me.xx_desint = New NTSInformatica.NTSGridColumn
    Me.xx_unmis = New NTSInformatica.NTSGridColumn
    Me.lsa_note = New NTSInformatica.NTSGridColumn
    Me.lsa_codlsar = New NTSInformatica.NTSGridColumn
    Me.lsa_riga = New NTSInformatica.NTSGridColumn
    Me.lsa_flag = New NTSInformatica.NTSGridColumn
    Me.lsa_fase = New NTSInformatica.NTSGridColumn
    Me.xx_fase = New NTSInformatica.NTSGridColumn
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.lsa_commeca = New NTSInformatica.NTSGridColumn
    Me.xx_commeca = New NTSInformatica.NTSGridColumn
    Me.xx_lottox = New NTSInformatica.NTSGridColumn
    Me.lsa_ubicaz = New NTSInformatica.NTSGridColumn
    Me.lsa_matric = New NTSInformatica.NTSGridColumn
    Me.lsa_esist = New NTSInformatica.NTSGridColumn
    Me.lsa_trattato = New NTSInformatica.NTSGridColumn
    Me.lsa_int1 = New NTSInformatica.NTSGridColumn
    Me.lsa_tctaglia = New NTSInformatica.NTSGridColumn
    Me.pnBottom = New NTSInformatica.NTSPanel
    Me.pnRight = New NTSInformatica.NTSPanel
    Me.cmdCancella = New NTSInformatica.NTSButton
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.pnTop = New NTSInformatica.NTSPanel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grElsa, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvElsa, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBottom.SuspendLayout()
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnRight.SuspendLayout()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbStampaWord, Me.tlbRecordBlocca, Me.tlbRecordSblocca, Me.tlbImpostaStato, Me.tlbFileOrdina, Me.tlbFileOrdinaDescr, Me.tlbFileOrdinaCodalt, Me.tlbTrattatoS, Me.tlbTrattatoN, Me.tlbImpostaTerm, Me.tlbImportaTerm, Me.tlbSeleziona, Me.tlbDeseleziona, Me.tlbCancellaRigheSel, Me.tlbSelezLottoUbicaz})
    Me.NtsBarManager1.MaxItemId = 34
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordBlocca, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordSblocca), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbTrattatoS, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbTrattatoN), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaWord), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbZoom.Id = 13
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 15
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbFileOrdina, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbFileOrdinaDescr), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbFileOrdinaCodalt), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStato, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaTerm), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImportaTerm), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSeleziona, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDeseleziona), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancellaRigheSel), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSelezLottoUbicaz, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.GlyphPath = ""
    Me.tlbImpostaStampante.Id = 16
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbFileOrdina
    '
    Me.tlbFileOrdina.Caption = "&Ordina per Cod. Articolo"
    Me.tlbFileOrdina.GlyphPath = ""
    Me.tlbFileOrdina.Id = 21
    Me.tlbFileOrdina.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O))
    Me.tlbFileOrdina.Name = "tlbFileOrdina"
    Me.tlbFileOrdina.NTSIsCheckBox = True
    Me.tlbFileOrdina.Visible = True
    '
    'tlbFileOrdinaDescr
    '
    Me.tlbFileOrdinaDescr.Caption = "Ordina per Descr. articolo"
    Me.tlbFileOrdinaDescr.GlyphPath = ""
    Me.tlbFileOrdinaDescr.Id = 22
    Me.tlbFileOrdinaDescr.Name = "tlbFileOrdinaDescr"
    Me.tlbFileOrdinaDescr.NTSIsCheckBox = True
    Me.tlbFileOrdinaDescr.Visible = True
    '
    'tlbFileOrdinaCodalt
    '
    Me.tlbFileOrdinaCodalt.Caption = "Ordina per Cod. Articolo alternativo"
    Me.tlbFileOrdinaCodalt.GlyphPath = ""
    Me.tlbFileOrdinaCodalt.Id = 23
    Me.tlbFileOrdinaCodalt.Name = "tlbFileOrdinaCodalt"
    Me.tlbFileOrdinaCodalt.NTSIsCheckBox = True
    Me.tlbFileOrdinaCodalt.Visible = True
    '
    'tlbImpostaStato
    '
    Me.tlbImpostaStato.Caption = "Imposta lo stato di '&Trattato' sulle nuove righe"
    Me.tlbImpostaStato.GlyphPath = ""
    Me.tlbImpostaStato.Id = 20
    Me.tlbImpostaStato.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.T))
    Me.tlbImpostaStato.Name = "tlbImpostaStato"
    Me.tlbImpostaStato.NTSIsCheckBox = True
    Me.tlbImpostaStato.Visible = True
    '
    'tlbImpostaTerm
    '
    Me.tlbImpostaTerm.Caption = "Imposta file terminale"
    Me.tlbImpostaTerm.GlyphPath = ""
    Me.tlbImpostaTerm.Id = 28
    Me.tlbImpostaTerm.Name = "tlbImpostaTerm"
    Me.tlbImpostaTerm.NTSIsCheckBox = False
    Me.tlbImpostaTerm.Visible = True
    '
    'tlbImportaTerm
    '
    Me.tlbImportaTerm.Caption = "Importa da file terminale"
    Me.tlbImportaTerm.GlyphPath = ""
    Me.tlbImportaTerm.Id = 29
    Me.tlbImportaTerm.Name = "tlbImportaTerm"
    Me.tlbImportaTerm.NTSIsCheckBox = False
    Me.tlbImportaTerm.Visible = True
    '
    'tlbSeleziona
    '
    Me.tlbSeleziona.Caption = "Seleziona tutte le righe"
    Me.tlbSeleziona.GlyphPath = ""
    Me.tlbSeleziona.Id = 30
    Me.tlbSeleziona.Name = "tlbSeleziona"
    Me.tlbSeleziona.Visible = True
    '
    'tlbDeseleziona
    '
    Me.tlbDeseleziona.Caption = "Deseleziona tutte le righe"
    Me.tlbDeseleziona.GlyphPath = ""
    Me.tlbDeseleziona.Id = 31
    Me.tlbDeseleziona.Name = "tlbDeseleziona"
    Me.tlbDeseleziona.Visible = True
    '
    'tlbCancellaRigheSel
    '
    Me.tlbCancellaRigheSel.Caption = "Cancella righe selezionate"
    Me.tlbCancellaRigheSel.GlyphPath = ""
    Me.tlbCancellaRigheSel.Id = 32
    Me.tlbCancellaRigheSel.Name = "tlbCancellaRigheSel"
    Me.tlbCancellaRigheSel.Visible = True
    '
    'tlbSelezLottoUbicaz
    '
    Me.tlbSelezLottoUbicaz.Caption = "Lotti/ ubicazioni aperti"
    Me.tlbSelezLottoUbicaz.GlyphPath = ""
    Me.tlbSelezLottoUbicaz.Id = 33
    Me.tlbSelezLottoUbicaz.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F7))
    Me.tlbSelezLottoUbicaz.Name = "tlbSelezLottoUbicaz"
    Me.tlbSelezLottoUbicaz.Visible = True
    '
    'tlbRecordBlocca
    '
    Me.tlbRecordBlocca.Caption = "Record Blocca"
    Me.tlbRecordBlocca.Glyph = CType(resources.GetObject("tlbRecordBlocca.Glyph"), System.Drawing.Image)
    Me.tlbRecordBlocca.GlyphPath = ""
    Me.tlbRecordBlocca.Id = 18
    Me.tlbRecordBlocca.Name = "tlbRecordBlocca"
    Me.tlbRecordBlocca.Visible = True
    '
    'tlbRecordSblocca
    '
    Me.tlbRecordSblocca.Caption = "Record Sblocca"
    Me.tlbRecordSblocca.Glyph = CType(resources.GetObject("tlbRecordSblocca.Glyph"), System.Drawing.Image)
    Me.tlbRecordSblocca.GlyphPath = ""
    Me.tlbRecordSblocca.Id = 19
    Me.tlbRecordSblocca.Name = "tlbRecordSblocca"
    Me.tlbRecordSblocca.Visible = True
    '
    'tlbTrattatoS
    '
    Me.tlbTrattatoS.Caption = "Imposta lo stato 'Trattato' su tutte le righe"
    Me.tlbTrattatoS.Glyph = CType(resources.GetObject("tlbTrattatoS.Glyph"), System.Drawing.Image)
    Me.tlbTrattatoS.GlyphPath = ""
    Me.tlbTrattatoS.Id = 24
    Me.tlbTrattatoS.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
    Me.tlbTrattatoS.Name = "tlbTrattatoS"
    Me.tlbTrattatoS.Visible = True
    '
    'tlbTrattatoN
    '
    Me.tlbTrattatoN.Caption = "Rimuovi lo stato 'Trattato' su tutte le righe"
    Me.tlbTrattatoN.Glyph = CType(resources.GetObject("tlbTrattatoN.Glyph"), System.Drawing.Image)
    Me.tlbTrattatoN.GlyphPath = ""
    Me.tlbTrattatoN.Id = 25
    Me.tlbTrattatoN.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N))
    Me.tlbTrattatoN.Name = "tlbTrattatoN"
    Me.tlbTrattatoN.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.GlyphPath = ""
    Me.tlbStampa.Id = 4
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.GlyphPath = ""
    Me.tlbStampaVideo.Id = 5
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbStampaWord
    '
    Me.tlbStampaWord.Caption = "StampaWord"
    Me.tlbStampaWord.Glyph = CType(resources.GetObject("tlbStampaWord.Glyph"), System.Drawing.Image)
    Me.tlbStampaWord.GlyphPath = ""
    Me.tlbStampaWord.Id = 17
    Me.tlbStampaWord.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F7))
    Me.tlbStampaWord.Name = "tlbStampaWord"
    Me.tlbStampaWord.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 11
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 12
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'grElsa
    '
    Me.grElsa.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grElsa.EmbeddedNavigator.Name = ""
    Me.grElsa.Location = New System.Drawing.Point(0, 0)
    Me.grElsa.MainView = Me.grvElsa
    Me.grElsa.Name = "grElsa"
    Me.grElsa.Size = New System.Drawing.Size(750, 358)
    Me.grElsa.TabIndex = 5
    Me.grElsa.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvElsa})
    '
    'grvElsa
    '
    Me.grvElsa.ActiveFilterEnabled = False
    Me.grvElsa.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_sel, Me.lsa_codart, Me.xx_descr, Me.xx_desint, Me.xx_unmis, Me.lsa_note, Me.lsa_codlsar, Me.lsa_riga, Me.lsa_flag, Me.lsa_fase, Me.xx_fase, Me.codditt, Me.lsa_commeca, Me.xx_commeca, Me.xx_lottox, Me.lsa_ubicaz, Me.lsa_matric, Me.lsa_esist, Me.lsa_trattato, Me.lsa_int1, Me.lsa_tctaglia})
    Me.grvElsa.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvElsa.Enabled = True
    Me.grvElsa.GridControl = Me.grElsa
    Me.grvElsa.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvElsa.MinRowHeight = 14
    Me.grvElsa.Name = "grvElsa"
    Me.grvElsa.NTSAllowDelete = True
    Me.grvElsa.NTSAllowInsert = True
    Me.grvElsa.NTSAllowUpdate = True
    Me.grvElsa.NTSMenuContext = Nothing
    Me.grvElsa.OptionsCustomization.AllowRowSizing = True
    Me.grvElsa.OptionsFilter.AllowFilterEditor = False
    Me.grvElsa.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvElsa.OptionsNavigation.UseTabKey = False
    Me.grvElsa.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvElsa.OptionsView.ColumnAutoWidth = False
    Me.grvElsa.OptionsView.EnableAppearanceEvenRow = True
    Me.grvElsa.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvElsa.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvElsa.OptionsView.ShowGroupPanel = False
    Me.grvElsa.RowHeight = 16
    '
    'xx_sel
    '
    Me.xx_sel.AppearanceCell.Options.UseBackColor = True
    Me.xx_sel.AppearanceCell.Options.UseTextOptions = True
    Me.xx_sel.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_sel.Caption = "Sel."
    Me.xx_sel.Enabled = True
    Me.xx_sel.FieldName = "xx_sel"
    Me.xx_sel.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_sel.Name = "xx_sel"
    Me.xx_sel.NTSRepositoryComboBox = Nothing
    Me.xx_sel.NTSRepositoryItemCheck = Nothing
    Me.xx_sel.NTSRepositoryItemMemo = Nothing
    Me.xx_sel.NTSRepositoryItemText = Nothing
    Me.xx_sel.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_sel.OptionsFilter.AllowFilter = False
    Me.xx_sel.Visible = True
    Me.xx_sel.VisibleIndex = 0
    '
    'lsa_codart
    '
    Me.lsa_codart.AppearanceCell.Options.UseBackColor = True
    Me.lsa_codart.AppearanceCell.Options.UseTextOptions = True
    Me.lsa_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lsa_codart.Caption = "Articolo"
    Me.lsa_codart.Enabled = True
    Me.lsa_codart.FieldName = "lsa_codart"
    Me.lsa_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lsa_codart.Name = "lsa_codart"
    Me.lsa_codart.NTSRepositoryComboBox = Nothing
    Me.lsa_codart.NTSRepositoryItemCheck = Nothing
    Me.lsa_codart.NTSRepositoryItemMemo = Nothing
    Me.lsa_codart.NTSRepositoryItemText = Nothing
    Me.lsa_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lsa_codart.OptionsFilter.AllowFilter = False
    Me.lsa_codart.Visible = True
    Me.lsa_codart.VisibleIndex = 1
    Me.lsa_codart.Width = 70
    '
    'xx_descr
    '
    Me.xx_descr.AppearanceCell.Options.UseBackColor = True
    Me.xx_descr.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descr.Caption = "Descrizione"
    Me.xx_descr.Enabled = False
    Me.xx_descr.FieldName = "xx_descr"
    Me.xx_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descr.Name = "xx_descr"
    Me.xx_descr.NTSRepositoryComboBox = Nothing
    Me.xx_descr.NTSRepositoryItemCheck = Nothing
    Me.xx_descr.NTSRepositoryItemMemo = Nothing
    Me.xx_descr.NTSRepositoryItemText = Nothing
    Me.xx_descr.OptionsColumn.AllowEdit = False
    Me.xx_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descr.OptionsColumn.ReadOnly = True
    Me.xx_descr.OptionsFilter.AllowFilter = False
    Me.xx_descr.Visible = True
    Me.xx_descr.VisibleIndex = 2
    Me.xx_descr.Width = 70
    '
    'xx_desint
    '
    Me.xx_desint.AppearanceCell.Options.UseBackColor = True
    Me.xx_desint.AppearanceCell.Options.UseTextOptions = True
    Me.xx_desint.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_desint.Caption = "Descr.interna"
    Me.xx_desint.Enabled = False
    Me.xx_desint.FieldName = "xx_desint"
    Me.xx_desint.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_desint.Name = "xx_desint"
    Me.xx_desint.NTSRepositoryComboBox = Nothing
    Me.xx_desint.NTSRepositoryItemCheck = Nothing
    Me.xx_desint.NTSRepositoryItemMemo = Nothing
    Me.xx_desint.NTSRepositoryItemText = Nothing
    Me.xx_desint.OptionsColumn.AllowEdit = False
    Me.xx_desint.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_desint.OptionsColumn.ReadOnly = True
    Me.xx_desint.OptionsFilter.AllowFilter = False
    Me.xx_desint.Visible = True
    Me.xx_desint.VisibleIndex = 3
    Me.xx_desint.Width = 70
    '
    'xx_unmis
    '
    Me.xx_unmis.AppearanceCell.Options.UseBackColor = True
    Me.xx_unmis.AppearanceCell.Options.UseTextOptions = True
    Me.xx_unmis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_unmis.Caption = "U.M."
    Me.xx_unmis.Enabled = False
    Me.xx_unmis.FieldName = "xx_unmis"
    Me.xx_unmis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_unmis.Name = "xx_unmis"
    Me.xx_unmis.NTSRepositoryComboBox = Nothing
    Me.xx_unmis.NTSRepositoryItemCheck = Nothing
    Me.xx_unmis.NTSRepositoryItemMemo = Nothing
    Me.xx_unmis.NTSRepositoryItemText = Nothing
    Me.xx_unmis.OptionsColumn.AllowEdit = False
    Me.xx_unmis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_unmis.OptionsColumn.ReadOnly = True
    Me.xx_unmis.OptionsFilter.AllowFilter = False
    Me.xx_unmis.Visible = True
    Me.xx_unmis.VisibleIndex = 4
    Me.xx_unmis.Width = 70
    '
    'lsa_note
    '
    Me.lsa_note.AppearanceCell.Options.UseBackColor = True
    Me.lsa_note.AppearanceCell.Options.UseTextOptions = True
    Me.lsa_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lsa_note.Caption = "Note"
    Me.lsa_note.Enabled = True
    Me.lsa_note.FieldName = "lsa_note"
    Me.lsa_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lsa_note.Name = "lsa_note"
    Me.lsa_note.NTSRepositoryComboBox = Nothing
    Me.lsa_note.NTSRepositoryItemCheck = Nothing
    Me.lsa_note.NTSRepositoryItemMemo = Nothing
    Me.lsa_note.NTSRepositoryItemText = Nothing
    Me.lsa_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lsa_note.OptionsFilter.AllowFilter = False
    Me.lsa_note.Visible = True
    Me.lsa_note.VisibleIndex = 5
    Me.lsa_note.Width = 70
    '
    'lsa_codlsar
    '
    Me.lsa_codlsar.AppearanceCell.Options.UseBackColor = True
    Me.lsa_codlsar.AppearanceCell.Options.UseTextOptions = True
    Me.lsa_codlsar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lsa_codlsar.Caption = "Cod list sel"
    Me.lsa_codlsar.Enabled = False
    Me.lsa_codlsar.FieldName = "lsa_codlsar"
    Me.lsa_codlsar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lsa_codlsar.Name = "lsa_codlsar"
    Me.lsa_codlsar.NTSRepositoryComboBox = Nothing
    Me.lsa_codlsar.NTSRepositoryItemCheck = Nothing
    Me.lsa_codlsar.NTSRepositoryItemMemo = Nothing
    Me.lsa_codlsar.NTSRepositoryItemText = Nothing
    Me.lsa_codlsar.OptionsColumn.AllowEdit = False
    Me.lsa_codlsar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lsa_codlsar.OptionsColumn.ReadOnly = True
    Me.lsa_codlsar.OptionsFilter.AllowFilter = False
    Me.lsa_codlsar.Width = 70
    '
    'lsa_riga
    '
    Me.lsa_riga.AppearanceCell.Options.UseBackColor = True
    Me.lsa_riga.AppearanceCell.Options.UseTextOptions = True
    Me.lsa_riga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lsa_riga.Caption = "Riga"
    Me.lsa_riga.Enabled = False
    Me.lsa_riga.FieldName = "lsa_riga"
    Me.lsa_riga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lsa_riga.Name = "lsa_riga"
    Me.lsa_riga.NTSRepositoryComboBox = Nothing
    Me.lsa_riga.NTSRepositoryItemCheck = Nothing
    Me.lsa_riga.NTSRepositoryItemMemo = Nothing
    Me.lsa_riga.NTSRepositoryItemText = Nothing
    Me.lsa_riga.OptionsColumn.AllowEdit = False
    Me.lsa_riga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lsa_riga.OptionsColumn.ReadOnly = True
    Me.lsa_riga.OptionsFilter.AllowFilter = False
    Me.lsa_riga.Width = 70
    '
    'lsa_flag
    '
    Me.lsa_flag.AppearanceCell.Options.UseBackColor = True
    Me.lsa_flag.AppearanceCell.Options.UseTextOptions = True
    Me.lsa_flag.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lsa_flag.Caption = "Salto pagina"
    Me.lsa_flag.Enabled = True
    Me.lsa_flag.FieldName = "lsa_flag"
    Me.lsa_flag.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lsa_flag.Name = "lsa_flag"
    Me.lsa_flag.NTSRepositoryComboBox = Nothing
    Me.lsa_flag.NTSRepositoryItemCheck = Nothing
    Me.lsa_flag.NTSRepositoryItemMemo = Nothing
    Me.lsa_flag.NTSRepositoryItemText = Nothing
    Me.lsa_flag.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lsa_flag.OptionsFilter.AllowFilter = False
    Me.lsa_flag.Visible = True
    Me.lsa_flag.VisibleIndex = 6
    Me.lsa_flag.Width = 70
    '
    'lsa_fase
    '
    Me.lsa_fase.AppearanceCell.Options.UseBackColor = True
    Me.lsa_fase.AppearanceCell.Options.UseTextOptions = True
    Me.lsa_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lsa_fase.Caption = "Fase"
    Me.lsa_fase.Enabled = True
    Me.lsa_fase.FieldName = "lsa_fase"
    Me.lsa_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lsa_fase.Name = "lsa_fase"
    Me.lsa_fase.NTSRepositoryComboBox = Nothing
    Me.lsa_fase.NTSRepositoryItemCheck = Nothing
    Me.lsa_fase.NTSRepositoryItemMemo = Nothing
    Me.lsa_fase.NTSRepositoryItemText = Nothing
    Me.lsa_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lsa_fase.OptionsFilter.AllowFilter = False
    Me.lsa_fase.Visible = True
    Me.lsa_fase.VisibleIndex = 7
    Me.lsa_fase.Width = 70
    '
    'xx_fase
    '
    Me.xx_fase.AppearanceCell.Options.UseBackColor = True
    Me.xx_fase.AppearanceCell.Options.UseTextOptions = True
    Me.xx_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_fase.Caption = "Descrizione Fase"
    Me.xx_fase.Enabled = False
    Me.xx_fase.FieldName = "xx_fase"
    Me.xx_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_fase.Name = "xx_fase"
    Me.xx_fase.NTSRepositoryComboBox = Nothing
    Me.xx_fase.NTSRepositoryItemCheck = Nothing
    Me.xx_fase.NTSRepositoryItemMemo = Nothing
    Me.xx_fase.NTSRepositoryItemText = Nothing
    Me.xx_fase.OptionsColumn.AllowEdit = False
    Me.xx_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_fase.OptionsColumn.ReadOnly = True
    Me.xx_fase.OptionsFilter.AllowFilter = False
    Me.xx_fase.Visible = True
    Me.xx_fase.VisibleIndex = 8
    Me.xx_fase.Width = 70
    '
    'codditt
    '
    Me.codditt.AppearanceCell.Options.UseBackColor = True
    Me.codditt.AppearanceCell.Options.UseTextOptions = True
    Me.codditt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.codditt.Caption = "Cod Ditta"
    Me.codditt.Enabled = False
    Me.codditt.FieldName = "codditt"
    Me.codditt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.codditt.Name = "codditt"
    Me.codditt.NTSRepositoryComboBox = Nothing
    Me.codditt.NTSRepositoryItemCheck = Nothing
    Me.codditt.NTSRepositoryItemMemo = Nothing
    Me.codditt.NTSRepositoryItemText = Nothing
    Me.codditt.OptionsColumn.AllowEdit = False
    Me.codditt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.codditt.OptionsColumn.ReadOnly = True
    Me.codditt.OptionsFilter.AllowFilter = False
    Me.codditt.Width = 70
    '
    'lsa_commeca
    '
    Me.lsa_commeca.AppearanceCell.Options.UseBackColor = True
    Me.lsa_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.lsa_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lsa_commeca.Caption = "Commessa"
    Me.lsa_commeca.Enabled = True
    Me.lsa_commeca.FieldName = "lsa_commeca"
    Me.lsa_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lsa_commeca.Name = "lsa_commeca"
    Me.lsa_commeca.NTSRepositoryComboBox = Nothing
    Me.lsa_commeca.NTSRepositoryItemCheck = Nothing
    Me.lsa_commeca.NTSRepositoryItemMemo = Nothing
    Me.lsa_commeca.NTSRepositoryItemText = Nothing
    Me.lsa_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lsa_commeca.OptionsFilter.AllowFilter = False
    Me.lsa_commeca.Visible = True
    Me.lsa_commeca.VisibleIndex = 9
    Me.lsa_commeca.Width = 70
    '
    'xx_commeca
    '
    Me.xx_commeca.AppearanceCell.Options.UseBackColor = True
    Me.xx_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.xx_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_commeca.Caption = "Descr. commessa"
    Me.xx_commeca.Enabled = False
    Me.xx_commeca.FieldName = "xx_commeca"
    Me.xx_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_commeca.Name = "xx_commeca"
    Me.xx_commeca.NTSRepositoryComboBox = Nothing
    Me.xx_commeca.NTSRepositoryItemCheck = Nothing
    Me.xx_commeca.NTSRepositoryItemMemo = Nothing
    Me.xx_commeca.NTSRepositoryItemText = Nothing
    Me.xx_commeca.OptionsColumn.AllowEdit = False
    Me.xx_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_commeca.OptionsColumn.ReadOnly = True
    Me.xx_commeca.OptionsFilter.AllowFilter = False
    Me.xx_commeca.Visible = True
    Me.xx_commeca.VisibleIndex = 10
    Me.xx_commeca.Width = 70
    '
    'xx_lottox
    '
    Me.xx_lottox.AppearanceCell.Options.UseBackColor = True
    Me.xx_lottox.AppearanceCell.Options.UseTextOptions = True
    Me.xx_lottox.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_lottox.Caption = "Lotto"
    Me.xx_lottox.Enabled = True
    Me.xx_lottox.FieldName = "xx_lottox"
    Me.xx_lottox.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_lottox.Name = "xx_lottox"
    Me.xx_lottox.NTSRepositoryComboBox = Nothing
    Me.xx_lottox.NTSRepositoryItemCheck = Nothing
    Me.xx_lottox.NTSRepositoryItemMemo = Nothing
    Me.xx_lottox.NTSRepositoryItemText = Nothing
    Me.xx_lottox.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_lottox.OptionsFilter.AllowFilter = False
    Me.xx_lottox.Visible = True
    Me.xx_lottox.VisibleIndex = 11
    Me.xx_lottox.Width = 70
    '
    'lsa_ubicaz
    '
    Me.lsa_ubicaz.AppearanceCell.Options.UseBackColor = True
    Me.lsa_ubicaz.AppearanceCell.Options.UseTextOptions = True
    Me.lsa_ubicaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lsa_ubicaz.Caption = "Ubicazione"
    Me.lsa_ubicaz.Enabled = True
    Me.lsa_ubicaz.FieldName = "lsa_ubicaz"
    Me.lsa_ubicaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lsa_ubicaz.Name = "lsa_ubicaz"
    Me.lsa_ubicaz.NTSRepositoryComboBox = Nothing
    Me.lsa_ubicaz.NTSRepositoryItemCheck = Nothing
    Me.lsa_ubicaz.NTSRepositoryItemMemo = Nothing
    Me.lsa_ubicaz.NTSRepositoryItemText = Nothing
    Me.lsa_ubicaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lsa_ubicaz.OptionsFilter.AllowFilter = False
    Me.lsa_ubicaz.Visible = True
    Me.lsa_ubicaz.VisibleIndex = 12
    Me.lsa_ubicaz.Width = 70
    '
    'lsa_matric
    '
    Me.lsa_matric.AppearanceCell.Options.UseBackColor = True
    Me.lsa_matric.AppearanceCell.Options.UseTextOptions = True
    Me.lsa_matric.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lsa_matric.Caption = "Matricola"
    Me.lsa_matric.Enabled = True
    Me.lsa_matric.FieldName = "lsa_matric"
    Me.lsa_matric.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lsa_matric.Name = "lsa_matric"
    Me.lsa_matric.NTSRepositoryComboBox = Nothing
    Me.lsa_matric.NTSRepositoryItemCheck = Nothing
    Me.lsa_matric.NTSRepositoryItemMemo = Nothing
    Me.lsa_matric.NTSRepositoryItemText = Nothing
    Me.lsa_matric.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lsa_matric.OptionsFilter.AllowFilter = False
    Me.lsa_matric.Visible = True
    Me.lsa_matric.VisibleIndex = 13
    Me.lsa_matric.Width = 70
    '
    'lsa_esist
    '
    Me.lsa_esist.AppearanceCell.Options.UseBackColor = True
    Me.lsa_esist.AppearanceCell.Options.UseTextOptions = True
    Me.lsa_esist.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lsa_esist.Caption = "Esistenza"
    Me.lsa_esist.Enabled = True
    Me.lsa_esist.FieldName = "lsa_esist"
    Me.lsa_esist.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lsa_esist.Name = "lsa_esist"
    Me.lsa_esist.NTSRepositoryComboBox = Nothing
    Me.lsa_esist.NTSRepositoryItemCheck = Nothing
    Me.lsa_esist.NTSRepositoryItemMemo = Nothing
    Me.lsa_esist.NTSRepositoryItemText = Nothing
    Me.lsa_esist.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lsa_esist.OptionsFilter.AllowFilter = False
    Me.lsa_esist.Visible = True
    Me.lsa_esist.VisibleIndex = 14
    Me.lsa_esist.Width = 70
    '
    'lsa_trattato
    '
    Me.lsa_trattato.AppearanceCell.Options.UseBackColor = True
    Me.lsa_trattato.AppearanceCell.Options.UseTextOptions = True
    Me.lsa_trattato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lsa_trattato.Caption = "Trattato"
    Me.lsa_trattato.Enabled = True
    Me.lsa_trattato.FieldName = "lsa_trattato"
    Me.lsa_trattato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lsa_trattato.Name = "lsa_trattato"
    Me.lsa_trattato.NTSRepositoryComboBox = Nothing
    Me.lsa_trattato.NTSRepositoryItemCheck = Nothing
    Me.lsa_trattato.NTSRepositoryItemMemo = Nothing
    Me.lsa_trattato.NTSRepositoryItemText = Nothing
    Me.lsa_trattato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lsa_trattato.OptionsFilter.AllowFilter = False
    Me.lsa_trattato.Visible = True
    Me.lsa_trattato.VisibleIndex = 15
    Me.lsa_trattato.Width = 70
    '
    'lsa_int1
    '
    Me.lsa_int1.AppearanceCell.Options.UseBackColor = True
    Me.lsa_int1.AppearanceCell.Options.UseTextOptions = True
    Me.lsa_int1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lsa_int1.Caption = "Int1"
    Me.lsa_int1.Enabled = False
    Me.lsa_int1.FieldName = "lsa_int1"
    Me.lsa_int1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lsa_int1.Name = "lsa_int1"
    Me.lsa_int1.NTSRepositoryComboBox = Nothing
    Me.lsa_int1.NTSRepositoryItemCheck = Nothing
    Me.lsa_int1.NTSRepositoryItemMemo = Nothing
    Me.lsa_int1.NTSRepositoryItemText = Nothing
    Me.lsa_int1.OptionsColumn.AllowEdit = False
    Me.lsa_int1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lsa_int1.OptionsColumn.ReadOnly = True
    Me.lsa_int1.OptionsFilter.AllowFilter = False
    Me.lsa_int1.Width = 70
    '
    'lsa_tctaglia
    '
    Me.lsa_tctaglia.AppearanceCell.Options.UseBackColor = True
    Me.lsa_tctaglia.AppearanceCell.Options.UseTextOptions = True
    Me.lsa_tctaglia.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lsa_tctaglia.Caption = "Taglia"
    Me.lsa_tctaglia.Enabled = False
    Me.lsa_tctaglia.FieldName = "lsa_tctaglia"
    Me.lsa_tctaglia.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lsa_tctaglia.Name = "lsa_tctaglia"
    Me.lsa_tctaglia.NTSRepositoryComboBox = Nothing
    Me.lsa_tctaglia.NTSRepositoryItemCheck = Nothing
    Me.lsa_tctaglia.NTSRepositoryItemMemo = Nothing
    Me.lsa_tctaglia.NTSRepositoryItemText = Nothing
    Me.lsa_tctaglia.OptionsColumn.AllowEdit = False
    Me.lsa_tctaglia.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lsa_tctaglia.OptionsColumn.ReadOnly = True
    Me.lsa_tctaglia.OptionsFilter.AllowFilter = False
    Me.lsa_tctaglia.Visible = True
    Me.lsa_tctaglia.VisibleIndex = 16
    '
    'pnBottom
    '
    Me.pnBottom.AllowDrop = True
    Me.pnBottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBottom.Appearance.Options.UseBackColor = True
    Me.pnBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBottom.Controls.Add(Me.pnRight)
    Me.pnBottom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnBottom.Location = New System.Drawing.Point(0, 388)
    Me.pnBottom.Name = "pnBottom"
    Me.pnBottom.NTSActiveTrasparency = True
    Me.pnBottom.Size = New System.Drawing.Size(750, 54)
    Me.pnBottom.TabIndex = 7
    Me.pnBottom.Text = "NtsPanel1"
    '
    'pnRight
    '
    Me.pnRight.AllowDrop = True
    Me.pnRight.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnRight.Appearance.Options.UseBackColor = True
    Me.pnRight.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnRight.Controls.Add(Me.cmdCancella)
    Me.pnRight.Controls.Add(Me.cmdSeleziona)
    Me.pnRight.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnRight.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnRight.Location = New System.Drawing.Point(520, 0)
    Me.pnRight.Name = "pnRight"
    Me.pnRight.NTSActiveTrasparency = True
    Me.pnRight.Size = New System.Drawing.Size(230, 54)
    Me.pnRight.TabIndex = 2
    Me.pnRight.Text = "NtsPanel1"
    '
    'cmdCancella
    '
    Me.cmdCancella.ImagePath = ""
    Me.cmdCancella.ImageText = ""
    Me.cmdCancella.Location = New System.Drawing.Point(116, 12)
    Me.cmdCancella.Name = "cmdCancella"
    Me.cmdCancella.NTSContextMenu = Nothing
    Me.cmdCancella.Size = New System.Drawing.Size(102, 30)
    Me.cmdCancella.TabIndex = 1
    Me.cmdCancella.Text = "&Cancella tutto"
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.ImagePath = ""
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(8, 12)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.NTSContextMenu = Nothing
    Me.cmdSeleziona.Size = New System.Drawing.Size(102, 30)
    Me.cmdSeleziona.TabIndex = 0
    Me.cmdSeleziona.Text = "&Seleziona Articoli"
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.grElsa)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTop.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(750, 358)
    Me.pnTop.TabIndex = 8
    Me.pnTop.Text = "NtsPanel1"
    '
    'FRMMGELSA
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(750, 442)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.pnBottom)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMMGELSA"
    Me.NTSLastControlFocussed = Me.grElsa
    Me.Text = "LISTA SELEZIONATA ARTICOLI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grElsa, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvElsa, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBottom.ResumeLayout(False)
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnRight.ResumeLayout(False)
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
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

    '------------------------------
    'collego il datatable agli altri oggetti di form

    Return True
  End Function

  Public Overridable Sub InitEntity(ByRef cleLsar As CLEMGLSAR)
    oCleLsar = cleLsar
    AddHandler oCleLsar.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)

    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbRecordBlocca.GlyphPath = (oApp.ChildImageDir & "\lock.gif")
        tlbRecordSblocca.GlyphPath = (oApp.ChildImageDir & "\unlock.gif")
        tlbTrattatoS.GlyphPath = (oApp.ChildImageDir & "\add_filter.gif")
        tlbTrattatoN.GlyphPath = (oApp.ChildImageDir & "\del_filter.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbStampaWord.GlyphPath = (oApp.ChildImageDir & "\word.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvElsa.NTSSetParam(oMenu, "LISTA SELEZIONATA ARTICOLI")


      codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128522046075760468, "Cod Ditta"), 12, True)
      lsa_codart.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128522046075918174, "Articolo"), tabartico, False)
      lsa_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128522046076075880, "Note"), 50, True)
      lsa_codlsar.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128522046076233586, "Cod list sel"), tablsar)
      lsa_riga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128522046076391292, "Riga"), "0", 9, -999999999, 999999999)
      lsa_flag.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128522046076548998, "Salto pagina"), "S", "N")
      lsa_fase.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128522046076706704, "Fase"), tabartfasi)
      lsa_fase.ArtiPerFase = lsa_codart
      lsa_commeca.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128522046076864410, "Commessa"), tabcommess)
      If oCleLsar.bLottoNew = False Then
        xx_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129519352144996521, "Lotto"), 9, False)
        xx_lottox.NTSSetParamZoom("ZOOMANALOTTI")
      Else
        xx_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128522046077022116, "Lotto"), 50, False)
        xx_lottox.NTSSetParamZoom("ZOOMANALOTTI")
      End If
      lsa_ubicaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128522046077179822, "Ubicazione"), 18, False)
      lsa_ubicaz.NTSSetParamZoom("ZOOMUBICAZ")
      lsa_matric.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128522046077337528, "Matricola"), 30, True)
      lsa_esist.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128522046077495234, "Esistenza"), oApp.FormatQta, 14, -9999999999, 99999999999999)
      lsa_trattato.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128522046077652940, "Trattato"), "S", "N")
      lsa_int1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128522046077810646, "Int1"), "0", 4, 0, 9999)
      xx_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128522046077968352, "Descrizione"), 0, True)
      xx_desint.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128522046078126058, "Descr.interna"), 0, True)
      xx_unmis.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128522046078283764, "U.M."), 3, True)
      xx_fase.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128522046078441470, "Descrizione Fase"), "0", 4)
      xx_commeca.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128522046078599176, "Descr. commessa"), "0", 9)
      lsa_tctaglia.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129667129617352252, "Taglia"), 4, True)
      xx_sel.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130386724981427998, "Sel."), "S", "N")

      lsa_codlsar.NTSSetRichiesto()

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
  Public Overridable Sub FRMMGELSA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim dttTmp As New DataTable
    Dim nOrdin As Integer = 0
    Try
      oMenu.ValCodiceDb(DittaCorrente, DittaCorrente, "ANADITAC", "S", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then
        oCleLsar.bLottoNew = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ac_lotti2) = "S", True, False))
      End If
      dttTmp.Clear()
      oCleLsar.bLottoUnivoco = CBool(oMenu.GetSettingBus("OPZIONI", ".", ".", "LottoUnivoco", "0", " ", "0"))


      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '----------------------------------------------------------------------------------------------------
      nOrdin = NTSCInt(oMenu.GetSettingBus("BSMGLSAR", "RECENT", ".", "Ordina_per_codice", "0", " ", "0"))
      Select Case nOrdin
        Case 1
          tlbFileOrdina.Checked = True
          tlbFileOrdina_ItemClick(tlbFileOrdina, Nothing)
        Case 2
          tlbFileOrdinaDescr.Checked = True
          tlbFileOrdinaDescr_ItemClick(tlbFileOrdinaDescr, Nothing)
        Case 3
          tlbFileOrdinaCodalt.Checked = True
          tlbFileOrdinaCodalt_ItemClick(tlbFileOrdinaCodalt, Nothing)
      End Select
      '-----------------------------------------------------------------------------------------
      oCleLsar.bFlagTrattato = CBool(oMenu.GetSettingBus("BSMGLSAR", "RECENT", ".", "FlagTrattato", "0", " ", "0"))
      tlbImpostaStato.Checked = oCleLsar.bFlagTrattato
      oCleLsar.bImpostastato = oCleLsar.bFlagTrattato
      oCleLsar.bMsgArticoloDuplicato = CBool(oMenu.GetSettingBus("BSMGLSAR", "OPZIONI", ".", "MsgArticoloDuplicato", "-1", " ", "-1"))
      '-----------------------------------------------------------------------------------------

      Apri()

      '-----------------------------------------------------------------------------------------
      '--- Blocca la griglia e le varie funzioni, se lo 'Status' della lista selezionata è:
      '------ 'Prelevato'
      '------ 'Sospeso'
      '-----------------------------------------------------------------------------------------
      If oCleLsar.bElsaGrigliaBloccata = True Then
        grvElsa.NTSAllowDelete = False
        grvElsa.NTSAllowInsert = False
        grvElsa.NTSAllowUpdate = False
        cmdSeleziona.Enabled = False
        cmdCancella.Enabled = False
        tlbNuovo.Enabled = False
        tlbCancella.Enabled = False
        tlbRipristina.Enabled = False
        tlbSalva.Enabled = False
        tlbRecordBlocca.Enabled = False
        tlbRecordSblocca.Enabled = False
        tlbZoom.Enabled = False
      End If

      If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCO) Then
        GctlSetVisEnab(lsa_tctaglia, True)
      Else
        lsa_tctaglia.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione Friendly nasconde, sempre, alcuni controlli
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = True Then
        lsa_fase.Visible = False
        xx_fase.Visible = False
        lsa_commeca.Visible = False
        xx_commeca.Visible = False
        xx_lottox.Visible = False
        lsa_ubicaz.Visible = False
        lsa_matric.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Sub

  Public Overridable Sub FRMMGELSA_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub FRMMGELSA_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Dim nOrdin As Integer = 0
    Try
      If tlbFileOrdina.Checked Then nOrdin = 1
      If tlbFileOrdinaDescr.Checked Then nOrdin = 2
      If tlbFileOrdinaCodalt.Checked Then nOrdin = 3

      oMenu.SaveSettingBus("BSMGLSAR", "RECENT", ".", "Ordina_per_codice", nOrdin.ToString, " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("BSMGLSAR", "RECENT", ".", "FlagTrattato", NTSCStr(IIf(tlbImpostaStato.Checked = True, "-1", "0")), " ", "NS.", "...", "...")
      dcElsa.Dispose()
      dsElsa.Dispose()
    Catch
    End Try
  End Sub

  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Dim oPar As CLE__PATB = Nothing
    Dim frmMpro As FRMMGMPRO = Nothing
    Dim oParam As New CLE__CLDP
    Try
      oPar = New CLE__PATB
      oPar.bVisGriglia = False
      oPar.strTipoArticolo = "N"
      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oPar)
      oCleLsar.strWhereFiar = oPar.strOut.Trim
      If oCleLsar.strWhereFiar = "" Then Exit Sub

      'Form selezione movimentazioni prodotti
      frmMpro = CType(NTSNewFormModal("FRMMGMPRO"), FRMMGMPRO)
      frmMpro.Init(oMenu, oParam, DittaCorrente)
      frmMpro.InitEntity(oCleLsar)
      frmMpro.ShowDialog()

      oCleLsar.Seleziona(tlbImpostaStato.Checked)

      Apri()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmMpro Is Nothing Then frmMpro.Dispose()
      frmMpro = Nothing
    End Try
  End Sub

  Public Overridable Sub cmdCancella_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancella.Click
    Dim dRes As DialogResult
    Try
      Salva()

      If dsElsa.Tables("LISTSAR").Rows.Count = 0 Then Return

      dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128522901675202696, "Cancellare tutti i records ?"))
      If Not dRes = System.Windows.Forms.DialogResult.Yes Then Exit Sub

      If Not oCleLsar.DeleteListsar(oCleLsar.strCodLSar) Then Exit Sub

      Apri()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvElsa.NTSNuovo()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Try
      If dsElsa.Tables("LISTSAR").Rows.Count = 0 Then Return

      If Not oCleLsar.CheckInPromozioni(NTSCInt(dsElsa.Tables("LISTSAR").Rows(0)!lsa_codlsar)) Then Return

      If Not grvElsa.NTSDeleteRigaCorrente(dcElsa, True) Then Return
      oCleLsar.ElsaSalva(True)
      AbilDisabControlli()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvElsa.NTSRipristinaRigaCorrenteBefore(dcElsa, True) Then Return
      oCleLsar.ElsaRipristina(dcElsa.Position, dcElsa.Filter)
      grvElsa.NTSRipristinaRigaCorrenteAfter()
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

      If ctrlTmp.GetType.ToString.IndexOf("NTSGrid") > -1 Then
        SetFastZoom(NTSCStr(CType(CType(ctrlTmp, NTSGrid).DefaultView, NTSGridView).EditingValue), oParam)

        If grElsa.Focused And grvElsa.FocusedColumn.Name.ToLower = "lsa_fase" Then
          'zoom su artfasi
          If grvElsa.NTSGetCurrentDataRow() Is Nothing Then Return
          If Trim(NTSCStr(grvElsa.NTSGetCurrentDataRow()!lsa_codart)) = "" Then
            oApp.MsgBoxInfo(oApp.Tr(Me, 129022506425048828, "Indicare l'articolo prima di inserire la fase."))
            Return
          End If

          SetFastZoom(NTSCStr(CType(CType(ctrlTmp, NTSGrid).DefaultView, NTSGridView).EditingValue), oParam)
          oParam.strTipo = NTSCStr(grvElsa.NTSGetCurrentDataRow()!lsa_codart)
          NTSZOOM.strIn = NTSCStr(grvElsa.EditingValue)
          NTSZOOM.ZoomStrIn("ZOOMARTFASI", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvElsa.EditingValue) Then grvElsa.SetFocusedRowCellValue(lsa_fase, NTSZOOM.strIn)
          Return
        ElseIf grElsa.Focused And grvElsa.FocusedColumn.Name.ToLower = "lsa_commeca" Then
          'zoom su commess
          If grvElsa.NTSGetCurrentDataRow() Is Nothing Then Return
          If Trim(NTSCStr(grvElsa.NTSGetCurrentDataRow()!lsa_codart)) = "" Then
            oApp.MsgBoxInfo(oApp.Tr(Me, 128523185016215343, "Indicare l'articolo prima di inserire la commessa."))
            Return
          End If
          NTSCallStandardZoom()
          
        ElseIf grElsa.Focused And grvElsa.FocusedColumn.Name.ToLower = "xx_lottox" Then
          'zoom su lotto

          If NTSCStr(grvElsa.GetRowCellValue(grvElsa.FocusedRowHandle, lsa_codart)).Trim = "" Then
            oApp.MsgBoxInfo(oApp.Tr(Me, 128776114971210001, "Indicare prima il codice articolo"))
            Return
          End If

          Dim dttArti As New DataTable
          oMenu.ValCodiceDb(NTSCStr(grvElsa.NTSGetCurrentDataRow()!lsa_codart), DittaCorrente, "ARTICO", "S", , dttArti)
          If NTSCStr(dttArti.Rows(0)!ar_geslotti) = "N" Then
            oApp.MsgBoxInfo(oApp.Tr(Me, 130652685584033091, "Zoom disponibile solo se l'articolo è gestito a lotti"))
            Return
          End If

          If oCleLsar.nCodMagP = 0 Then
            oApp.MsgBoxInfo(oApp.Tr(Me, 131037137065543226, "Indicare un codice magazzino diverso da 0."))
            Return
          End If

          SetFastZoom(NTSCStr(CType(CType(ctrlTmp, NTSGrid).DefaultView, NTSGridView).EditingValue), oParam)
          oParam.strTipo = NTSCStr(grvElsa.NTSGetCurrentDataRow()!lsa_codart)
          NTSZOOM.strIn = NTSCStr(grvElsa.EditingValue)
          NTSZOOM.ZoomStrIn("ZOOMANALOTTI", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvElsa.EditingValue) Then grvElsa.SetFocusedRowCellValue(xx_lottox, NTSZOOM.strIn)
          Return


        ElseIf grvElsa.FocusedColumn.Equals(lsa_ubicaz) Then
          '------------------------------------
          'zoom ubicazioni
          If NTSCStr(grvElsa.GetRowCellValue(grvElsa.FocusedRowHandle, lsa_codart)).Trim = "" Then
            oApp.MsgBoxInfo(oApp.Tr(Me, 128776114971210000, "Indicare prima il codice articolo"))
            Return
          End If

          Dim dttArti As New DataTable
          oMenu.ValCodiceDb(NTSCStr(grvElsa.NTSGetCurrentDataRow()!lsa_codart), DittaCorrente, "ARTICO", "S", , dttArti)
          If NTSCStr(dttArti.Rows(0)!ar_gesubic) = "N" Then
            oApp.MsgBoxInfo(oApp.Tr(Me, 130652685584033092, "Zoom disponibile solo se l'articolo è gestito a ubicazioni"))
            Return
          End If

          If oCleLsar.nCodMagP = 0 Then
            oApp.MsgBoxInfo(oApp.Tr(Me, 131037137220587330, "Indicare un codice magazzino diverso da 0."))
            Return
          End If

          NTSZOOM.strIn = NTSCStr(grvElsa.EditingValue)
          oParam.strTipo = NTSCStr(grvElsa.GetRowCellValue(grvElsa.FocusedRowHandle, lsa_codart))

          oParam.nMagaz = oCleLsar.nCodMagP   'serve per visual solo i lotti aperti

          oParam.nAnno = NTSCInt(grvElsa.GetRowCellValue(grvElsa.FocusedRowHandle, lsa_fase))     'serve per visual solo i lotti aperti
          'oParam.strDatreg = NTSCDate(edet_datdoc.Text).ToShortDateString                          'serve per visual solo i lotti aperti
          NTSZOOM.ZoomStrIn("ZOOMUBICAZ", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvElsa.EditingValue) Then grvElsa.SetFocusedValue(NTSZOOM.strIn)
          Return

        Else
          '------------------------------------
          'zoom standard di textbox e griglia
          NTSCallStandardZoom()
        End If
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

  Public Overridable Sub tlbRecordBlocca_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordBlocca.ItemClick
    Try
      oCleLsar.RecordBlocca()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRecordSblocca_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordSblocca.ItemClick
    Try
      oCleLsar.RecordSblocca()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
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

  Public Overridable Sub tlbStampaWord_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaWord.ItemClick
    Dim oPar As CLE__CLDP = Nothing
    Dim strQueryWord As String = ""
    Try
      '-----------------------
      'faccio comporre la query al dl
      strQueryWord = oCleLsar.GetQueryStampaWord(oCleLsar.strCodLSar)
      If strQueryWord = "" Then Return

      '-----------------------
      'chiamo la stampa su word passandogli la query
      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strPar1 = "BNMGLSAR"
      oPar.strPar2 = strQueryWord
      oMenu.RunChild("NTSInformatica", "FRM__STW1", "", DittaCorrente, "", "BN__STWO", oPar, "", True, True)

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

  Public Overridable Sub tlbFileOrdina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbFileOrdina.ItemClick
    Try
      If tlbFileOrdina.Checked = False Then
        tlbFileOrdina.Checked = True
        Return
      Else
        tlbFileOrdinaDescr.Checked = False
        tlbFileOrdinaCodalt.Checked = False
      End If

      If Not Salva() Then
        tlbFileOrdina.Checked = False
        Exit Sub
      End If

      If tlbFileOrdina.Checked Then
        oCleLsar.bOrdinaCodart = True
      Else
        oCleLsar.bOrdinaCodart = False
      End If

      oCleLsar.bOrdinaCodalt = False
      oCleLsar.bOrdinaDescr = False

      Apri()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbFileOrdinaDescr_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbFileOrdinaDescr.ItemClick
    Try
      If tlbFileOrdinaDescr.Checked = False Then
        tlbFileOrdinaDescr.Checked = True
        Return
      Else
        tlbFileOrdina.Checked = False
        tlbFileOrdinaCodalt.Checked = False
      End If

      If Not Salva() Then
        tlbFileOrdinaDescr.Checked = False
        Exit Sub
      End If

      If tlbFileOrdinaDescr.Checked Then
        oCleLsar.bOrdinaDescr = True
      Else
        oCleLsar.bOrdinaDescr = False
      End If

      oCleLsar.bOrdinaCodart = False
      oCleLsar.bOrdinaCodalt = False

      Apri()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbFileOrdinaCodalt_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbFileOrdinaCodalt.ItemClick
    Try
      If tlbFileOrdinaCodalt.Checked = False Then
        tlbFileOrdinaCodalt.Checked = True
        Return
      Else
        tlbFileOrdina.Checked = False
        tlbFileOrdinaDescr.Checked = False
      End If

      If Not Salva() Then
        tlbFileOrdinaCodalt.Checked = False
        Exit Sub
      End If

      If tlbFileOrdinaCodalt.Checked Then
        oCleLsar.bOrdinaCodalt = True
      Else
        oCleLsar.bOrdinaCodalt = False
      End If

      oCleLsar.bOrdinaCodart = False
      oCleLsar.bOrdinaDescr = False

      Apri()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbImpostaStato_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStato.ItemClick
    Try
      oCleLsar.bImpostastato = tlbImpostaStato.Checked
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbTrattatoS_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbTrattatoS.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      FlagTratto("S")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub tlbTrattatoN_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbTrattatoN.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      FlagTratto("N")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbImpostaTerm_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaTerm.ItemClick
    Dim oParam As New CLE__CLDP
    Dim frmTerm As FRMMGTERM = Nothing
    Try
      oParam.strNomProg = "BSMGLSAR"
      frmTerm = CType(NTSNewFormModal("FRMMGTERM"), FRMMGTERM)
      frmTerm.Init(oMenu, oParam, DittaCorrente)
      frmTerm.InitEntity(oCleLsar)
      frmTerm.ShowDialog()

    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      If Not frmTerm Is Nothing Then frmTerm.Dispose()
      frmTerm = Nothing
    End Try
  End Sub
  Public Overridable Sub tlbImportaTerm_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImportaTerm.ItemClick
    Dim strPath As String = ""
    Dim strFile As String = ""
    Dim dttTmp As New DataTable
    Dim oParam As New CLE__CLDP
    Dim frmLayo As FRMMGLAYO = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      oCleLsar.strTermKey = "Terminale"
      If oCleLsar.GetImpostazioniFile(dttTmp) = True Then
        If dttTmp.Rows.Count > 0 Then
          frmLayo = CType(NTSNewFormModal("FRMMGLAYO"), FRMMGLAYO)
          frmLayo.Init(oMenu, oParam, DittaCorrente)
          frmLayo.InitEntity(oCleLsar)
          frmLayo.ShowDialog()
          If oCleLsar.bLayoAnnullato = True Then Return
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleLsar.TerminaleLeggiCheck("BSMGLSAR") Then Return

      '---------------------------
      'Se il nome del file in strTermFileName è anteposto da ? lo chiede all'utente
      oCleLsar.SeparatePathAndFileName(oCleLsar.strTermFileName, strPath, strFile)
      If Microsoft.VisualBasic.Left(strFile, 1) = "?" Then
        'Lo chiede all'utente
        'strFile = oApp.InputBoxNew(oApp.Tr(Me, 128571298019531250, "Inserire il nome del file da acquisire (che deve risiedere nella cartella '|" & strPath & "|'):"), Mid(strFile, 2))
        'oCleBoll.strTermFileName = (strPath & "\").Replace("\\", "\") & strFile
        Dim oDlg As New NTSOpenFileDialog
        oDlg.Multiselect = False
        oDlg.ShowHelp = False
        oDlg.Title = oApp.Tr(Me, 128571256152187500, "File da acquisire")
        oDlg.DefaultExt = "TXT"
        oDlg.Filter = "File di testo (*.txt)|*.txt|Tutti i file (*.*)|*.*"
        oDlg.InitialDirectory = strPath
        oDlg.FileName = Mid(strFile, 2)
        oDlg.oMenu = oMenu
        oDlg.ShowDialog()
        If oDlg.FileName <> "" Then
          oCleLsar.strTermFileName = oDlg.FileName
        End If
        oDlg.Dispose()
        oDlg = Nothing
      End If

      'Lancia la shell
      If oCleLsar.bTermExecute Then
        'Prima di tutto cancella il file se esiste
        Try
          System.IO.File.Delete(oCleLsar.strTermFileName)
        Catch ex As Exception
          oApp.MsgBoxErr(oApp.Tr(Me, 130356318812712087, "Impossibile cancellare il file '|" & oCleLsar.strTermFileName & "|' prima della acquisizione. Errore: ") & vbCrLf & ex.Message)
          Return
        End Try

        If oCleLsar.strTermCommand.Trim <> "" Then
          Try
            Dim proc As New System.Diagnostics.Process
            If oCleLsar.bTermSilent Then proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            proc.StartInfo.FileName = oCleLsar.strTermCommand
            proc.StartInfo.UseShellExecute = True
            proc.Start()
            proc.WaitForExit()
          Catch ex As Exception
            oApp.MsgBoxErr(oApp.Tr(Me, 130356318865212087, "Errore durante l'esecuzione della Shell '|" & oCleLsar.strTermCommand & "|'. Errore: ") & vbCrLf & ex.Message)
            Return
          End Try
        End If
      End If    'If oCleBoll.bTermExecute Then

      '---------------------------
      'inizio l'import delle righe del file
      Me.Cursor = Cursors.WaitCursor
      If Not oCleLsar.TerminaleLeggiImportaFile() Then Return

      '---------------------------
      'Visualizza il log
      If oCleLsar.bTermShowLogErr Or oCleLsar.nCountLog <> 0 Then
        NTSProcessStart("notepad", oCleLsar.LogFileName)
      End If
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
      dttTmp.Clear()
      dttTmp.Dispose()
      If Not frmLayo Is Nothing Then frmLayo.Dispose()
      frmLayo = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbSeleziona_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSeleziona.ItemClick
    Dim i As Integer = 0
    Try
      Salva()
      For Each dtrTmp As DataRow In dsElsa.Tables("LISTSAR").Rows
        dtrTmp!xx_sel = "S"
      Next
      dsElsa.Tables("LISTSAR").AcceptChanges()
      oCleLsar.bElsaHasChanges = False
    Catch ex As Exception
      CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbDeseleziona_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDeseleziona.ItemClick
    Dim i As Integer = 0
    Try
      Salva()
      For Each dtrTmp As DataRow In dsElsa.Tables("LISTSAR").Rows
        dtrTmp!xx_sel = "N"
      Next
      dsElsa.Tables("LISTSAR").AcceptChanges()
      oCleLsar.bElsaHasChanges = False
    Catch ex As Exception
      CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbCancellaRigheSel_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancellaRigheSel.ItemClick
    Dim dtrT() As DataRow = Nothing
    Dim currentRow As DataRow = Nothing
    Try
      dtrT = dsElsa.Tables("LISTSAR").Select("xx_sel = 'S'")
      If dtrT.Length = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129599631362153776, "Non è stata selezionata nessuna riga."))
        Return
      End If

      If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129599631287151376, "Cancellare le righe selezionate?")) = Windows.Forms.DialogResult.No Then Return

      Me.Cursor = Cursors.WaitCursor
      '1) Si ripristina la riga appena inserita non salvata
      If grvElsa.FocusedRowHandle >= 0 Then
        currentRow = dsElsa.Tables("LISTSAR").Rows(grvElsa.FocusedRowHandle)
        If NTSCStr(currentRow!xx_sel) = "S" AndAlso currentRow.RowState = DataRowState.Added Then
          If Not grvElsa.NTSRipristinaRigaCorrenteBefore(dcElsa, False) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129599658533653863, "Impossibile cancellare la riga corrente." & vbCrLf & _
                                                           "Non è stata cancellata nessuna riga."))
          Else
            oCleLsar.Ripristina(dcElsa.Position, dcElsa.Filter)
            grvElsa.NTSRipristinaRigaCorrenteAfter()
          End If
        End If
      End If

      '2) Si cancellano le righe salvate
      For Each row As DataRow In dtrT
        dcElsa.RemoveAt(dsElsa.Tables("LISTSAR").Rows.IndexOf(row))
        oCleLsar.ElsaSalva(True)
      Next

      Me.Cursor = Cursors.Default
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbSelezLottoUbicaz_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSelezLottoUbicaz.ItemClick
    Dim oParam As New CLE__PATB
    Dim dDaAss As Decimal = 0
    Try
      If NTSCStr(grvElsa.GetRowCellValue(grvElsa.FocusedRowHandle, lsa_codart)).Trim = "" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128776114985406000, "Indicare prima il codice articolo"))
        Return
      End If

      Dim dttArti As New DataTable
      oMenu.ValCodiceDb(NTSCStr(grvElsa.NTSGetCurrentDataRow()!lsa_codart), DittaCorrente, "ARTICO", "S", , dttArti)
      If NTSCStr(dttArti.Rows(0)!ar_geslotti) = "N" And NTSCStr(dttArti.Rows(0)!ar_gesubic) = "N" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 131037123627791498, "Articolo non gestito a lotti/ubicazioni. Zoom non disponibile."))
        Return
      End If

      If oCleLsar.nCodMagP = 0 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 131037136380335505, "Indicare un codice magazzino diverso da 0."))
        Return
      End If

      NTSZOOM.strIn = NTSCStr(grvElsa.EditingValue)
      oParam.bLiv2 = True 'Abilitata multiselezione

      oParam.strTipo = NTSCStr(grvElsa.GetRowCellValue(grvElsa.FocusedRowHandle, lsa_codart))
      oParam.nMagaz = oCleLsar.nCodMagP   'serve per visual solo i lotti aperti
      oParam.nAnno = NTSCInt(grvElsa.GetRowCellValue(grvElsa.FocusedRowHandle, lsa_fase))      'serve per visual solo i lotti aperti

      Dim dttLista As New DataTable
      oMenu.ValCodiceDb(oCleLsar.strCodLSar, DittaCorrente, "TABLSAR", "N", , dttLista)
      oParam.strDatreg = dttLista.Rows(0)!tb_dtcomp.ToString                        'serve per visual solo i lotti aperti

      NTSZOOM.ZoomStrIn("ZOOMANALOTTI", DittaCorrente, oParam)

      If oParam.bFlag1 = False Then Return

      If oParam.oParam Is Nothing Then
        'vecchio sistema, è stato fatto doppio click su una riga invece della selezione multipla
        If grvElsa.NTSGetCurrentDataRow!xxo_geslotti.ToString = "S" Then
          grvElsa.NTSGetCurrentDataRow!xxo_lottox = oParam.strOut
        End If
      End If

      If dttArti.Rows(0)!ar_gesubic.ToString = "S" Then
        grvElsa.NTSGetCurrentDataRow!lsa_ubicaz = oParam.strDescr
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Eventi Griglia"
  Public Overridable Sub grvElsa_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvElsa.NTSBeforeRowUpdate
    Try

      If grvElsa.NTSGetCurrentDataRow Is Nothing Then Return

      If grvElsa.NTSGetCurrentDataRow.RowState = DataRowState.Added Then
        bColonnaArticolo = True
      Else
        bColonnaArticolo = False
      End If

      If Not Salva() Then
        'rimango sulla riga su cui sono
        e.Allow = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub grvElsa_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvElsa.NTSFocusedRowChanged
    'blocco le colonne non modificabili
    Dim dtrT As DataRow = Nothing
    Try
      If oCleLsar Is Nothing Then Return

      dtrT = grvElsa.NTSGetCurrentDataRow
      '------------------------------------
      'sono su una nuova riga
      If dtrT Is Nothing Then
        lsa_codart.Enabled = True
        If bColonnaArticolo Then grvElsa.FocusedColumn = lsa_codart
        Return
      End If

      If NTSCInt(dtrT!lsa_riga) <> 0 Then
        lsa_codart.Enabled = False
      Else
        lsa_codart.Enabled = True
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Function Apri() As Boolean
    Try
      dcElsa.DataSource = Nothing
      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleLsar.ElsaApri(DittaCorrente, dsElsa) Then
        Me.Close()
        Return False
      End If
      dcElsa.DataSource = dsElsa.Tables("LISTSAR")
      dsElsa.AcceptChanges()

      grElsa.DataSource = dcElsa

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      AbilDisabControlli()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function Salva() As Boolean
    Try
      If dcElsa.Position < 0 Then Return True
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvElsa.NTSSalvaRigaCorrente(dcElsa, oCleLsar.ElsaRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleLsar.ElsaSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleLsar.ElsaRipristina(dcElsa.Position, dcElsa.Filter)
        Case System.Windows.Forms.DialogResult.Cancel
          'non posso fare nulla
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          'la riga non ha subito modifiche
      End Select

      AbilDisabControlli()

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer
    Dim strReports As String = ""
    Dim bReportPerLista As Boolean
    Try
      bReportPerLista = CBool(oMenu.GetSettingBus("BSMGLSAR", "OPZIONI", ".", "ReportPerLista", "0", " ", "0"))
      '---------------------------------
      If bReportPerLista Then
        strReports = "Reports" & oCleLsar.strCodLSar
      Else
        strReports = "Reports1"
      End If

      '--------------------------------------------------
      'preparo il motore di stampa
      strCrpe = "{listsar.codditt} = '" & DittaCorrente & "' AND {listsar.lsa_codlsar} = " & oCleLsar.strCodLSar
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGELSA", strReports, " ", 0, nDestin, "BSMGELSA.RPT", False, "LISTA SELEZIONATA ARTICOLI", False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce già il multireport)
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

  Public Overridable Sub AbilDisabControlli()
    Try
      If dsElsa.Tables("LISTSAR").Rows.Count = 0 Then
        cmdCancella.Enabled = False
        tlbStampaVideo.Visible = False
        tlbStampa.Visible = False
        tlbStampaWord.Visible = False
        tlbImpostaStampante.Visible = False
      Else
        cmdCancella.Enabled = True
        tlbStampaVideo.Visible = True
        tlbStampa.Visible = True
        tlbStampaWord.Visible = True
        tlbImpostaStampante.Visible = True
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FlagTratto(ByVal strTrattato As String)
    Dim dlgRes As DialogResult

    Try
      '--------------------------------------------------------------------------------------------------------------
      If strTrattato = "S" Then
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 130064275580195763, "Impostare lo stato di 'Trattato' su tutte le righe?"))
      Else
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 130064275599100803, "Rimuovere lo stato di 'Trattato' su tutte le righe?"))
      End If
      If dlgRes = Windows.Forms.DialogResult.No Then Return
      '--------------------------------------------------------------------------------------------------------------
      If oCleLsar.ImpostaTrattatoElsa(strTrattato) = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      Apri()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class
