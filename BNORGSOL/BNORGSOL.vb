Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORGSOL
  Private Moduli_P As Integer = bsModOR
  Private ModuliExt_P As Integer = bsModExtORE
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

  'testord serve per fare in modo che ordlist venga gestito tipo testord/movord
  'quando cambio un campo di ordlist che dovrebbe essere i ntestord lo memorizzo anche in TESTA, così le routine di BEMGDOCU funzionano regolarmente
  'ad ogni cambio di riga di ordlist memorizzo in testa i dati di ordlist su cui entro

  Public oCleGsol As CLEORGSOL
  Public oCallParams As CLE__CLDP
  Public dsGsol As DataSet
  Public dcGsol As BindingSource = New BindingSource()
  Public dttUm As DataTable = Nothing                    'elenco delle unità di misura utilizzate in artico
  Public dsGrvTCO As New DataSet                         'conterrà il dattable per la visualizzaz delle qta per taglia 
  Public dcGsorTCO As BindingSource = New BindingSource
  Public strOldTipork As String = ""
  Public oParApri As CLE__CLDP = Nothing                 'parametri di ritorno da BNORSEOR

  Public lIIOrdl As Integer = 0
  Public lIIOrlist As Integer = 0
  Public lIIAssris As Integer = 0
  Public lIIAttivit As Integer = 0
  Public lIIOrlisttc As Integer = 0
  Public bCambioDelCambio As Boolean = False
  Public bInAssociazioneDatasource As Boolean = False

  Private components As System.ComponentModel.IContainer


  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMORGSOL))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbApri = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecordNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecordSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecordRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecordCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbImpegni = New NTSInformatica.NTSBarButtonItem
    Me.tlbLavorazioni = New NTSInformatica.NTSBarButtonItem
    Me.tlbProgressivi = New NTSInformatica.NTSBarButtonItem
    Me.tlbMrp = New NTSInformatica.NTSBarButtonItem
    Me.tlbMovimenti = New NTSInformatica.NTSBarButtonItem
    Me.tlbDettTco = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbConfermatutto = New NTSInformatica.NTSBarMenuItem
    Me.tlbConfermaselez = New NTSInformatica.NTSBarMenuItem
    Me.tlbGeneraOrdini = New NTSInformatica.NTSBarMenuItem
    Me.tlbCancRigheSel = New NTSInformatica.NTSBarMenuItem
    Me.tlbVisDetTco = New NTSInformatica.NTSBarMenuItem
    Me.tlbZoomFornPrz = New NTSInformatica.NTSBarMenuItem
    Me.tlbRicalcolaCollidaQta = New NTSInformatica.NTSBarMenuItem
    Me.tblPrnAttVideo = New NTSInformatica.NTSBarMenuItem
    Me.tblPrnAttCarta = New NTSInformatica.NTSBarMenuItem
    Me.tblPrnImpVideo = New NTSInformatica.NTSBarMenuItem
    Me.tblPrnImpCarta = New NTSInformatica.NTSBarMenuItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbSelezionaTutto = New NTSInformatica.NTSBarMenuItem
    Me.tlbDeselezionaTutto = New NTSInformatica.NTSBarMenuItem
    Me.tlbInvertiSelezione = New NTSInformatica.NTSBarMenuItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnGriglia = New NTSInformatica.NTSPanel
    Me.grRighe = New NTSInformatica.NTSGrid
    Me.grvRighe = New NTSInformatica.NTSGridView
    Me.xxo_prznet = New NTSInformatica.NTSGridColumn
    Me.xxo_seleziona = New NTSInformatica.NTSGridColumn
    Me.ec_riga = New NTSInformatica.NTSGridColumn
    Me.ec_codart = New NTSInformatica.NTSGridColumn
    Me.ec_descr = New NTSInformatica.NTSGridColumn
    Me.ec_desint = New NTSInformatica.NTSGridColumn
    Me.ec_unmis = New NTSInformatica.NTSGridColumn
    Me.ec_colli = New NTSInformatica.NTSGridColumn
    Me.ec_ump = New NTSInformatica.NTSGridColumn
    Me.ec_quant = New NTSInformatica.NTSGridColumn
    Me.ec_conto = New NTSInformatica.NTSGridColumn
    Me.xxo_conto = New NTSInformatica.NTSGridColumn
    Me.ec_datcons = New NTSInformatica.NTSGridColumn
    Me.ec_codvalu = New NTSInformatica.NTSGridColumn
    Me.xxo_codvalu = New NTSInformatica.NTSGridColumn
    Me.ec_cambio = New NTSInformatica.NTSGridColumn
    Me.ec_prezvalc = New NTSInformatica.NTSGridColumn
    Me.ec_prezzo = New NTSInformatica.NTSGridColumn
    Me.ec_stato = New NTSInformatica.NTSGridColumn
    Me.ec_scont1 = New NTSInformatica.NTSGridColumn
    Me.ec_scont2 = New NTSInformatica.NTSGridColumn
    Me.ec_scont3 = New NTSInformatica.NTSGridColumn
    Me.ec_scont4 = New NTSInformatica.NTSGridColumn
    Me.ec_scont5 = New NTSInformatica.NTSGridColumn
    Me.ec_scont6 = New NTSInformatica.NTSGridColumn
    Me.ec_magaz = New NTSInformatica.NTSGridColumn
    Me.xxo_magaz = New NTSInformatica.NTSGridColumn
    Me.ec_magaz2 = New NTSInformatica.NTSGridColumn
    Me.xxo_magaz2 = New NTSInformatica.NTSGridColumn
    Me.ec_magimp = New NTSInformatica.NTSGridColumn
    Me.xxo_magimp = New NTSInformatica.NTSGridColumn
    Me.ec_datord = New NTSInformatica.NTSGridColumn
    Me.ec_controp = New NTSInformatica.NTSGridColumn
    Me.xxo_controp = New NTSInformatica.NTSGridColumn
    Me.ec_codiva = New NTSInformatica.NTSGridColumn
    Me.xxo_codiva = New NTSInformatica.NTSGridColumn
    Me.ec_codcfam = New NTSInformatica.NTSGridColumn
    Me.xxo_codcfam = New NTSInformatica.NTSGridColumn
    Me.ec_commeca = New NTSInformatica.NTSGridColumn
    Me.xxo_commeca = New NTSInformatica.NTSGridColumn
    Me.ec_subcommeca = New NTSInformatica.NTSGridColumn
    Me.ec_codcena = New NTSInformatica.NTSGridColumn
    Me.xxo_codcena = New NTSInformatica.NTSGridColumn
    Me.xxo_lottox = New NTSInformatica.NTSGridColumn
    Me.ec_note = New NTSInformatica.NTSGridColumn
    Me.ec_valore = New NTSInformatica.NTSGridColumn
    Me.ec_contocontr = New NTSInformatica.NTSGridColumn
    Me.xxo_contocon = New NTSInformatica.NTSGridColumn
    Me.ec_codclie = New NTSInformatica.NTSGridColumn
    Me.xxo_codclie = New NTSInformatica.NTSGridColumn
    Me.ec_misura1 = New NTSInformatica.NTSGridColumn
    Me.ec_misura2 = New NTSInformatica.NTSGridColumn
    Me.ec_misura3 = New NTSInformatica.NTSGridColumn
    Me.ec_perqta = New NTSInformatica.NTSGridColumn
    Me.ec_flcom = New NTSInformatica.NTSGridColumn
    Me.ec_flprznet = New NTSInformatica.NTSGridColumn
    Me.ec_flforf = New NTSInformatica.NTSGridColumn
    Me.ec_matric = New NTSInformatica.NTSGridColumn
    Me.ec_umprz = New NTSInformatica.NTSGridColumn
    Me.ec_fase = New NTSInformatica.NTSGridColumn
    Me.xxo_fase = New NTSInformatica.NTSGridColumn
    Me.ec_codlavo = New NTSInformatica.NTSGridColumn
    Me.xxo_codlavo = New NTSInformatica.NTSGridColumn
    Me.ec_datini = New NTSInformatica.NTSGridColumn
    Me.ec_datfin = New NTSInformatica.NTSGridColumn
    Me.ec_valorev = New NTSInformatica.NTSGridColumn
    Me.ec_pmtaskid = New NTSInformatica.NTSGridColumn
    Me.xxo_pmtaskid = New NTSInformatica.NTSGridColumn
    Me.ec_pmsalcon = New NTSInformatica.NTSGridColumn
    Me.ec_pmqtadis = New NTSInformatica.NTSGridColumn
    Me.ec_pmvaldis = New NTSInformatica.NTSGridColumn
    Me.ec_rdatipork = New NTSInformatica.NTSGridColumn
    Me.ec_rdaanno = New NTSInformatica.NTSGridColumn
    Me.ec_rdaserie = New NTSInformatica.NTSGridColumn
    Me.ec_rdanum = New NTSInformatica.NTSGridColumn
    Me.ec_rdariga = New NTSInformatica.NTSGridColumn
    Me.ec_offreq = New NTSInformatica.NTSGridColumn
    Me.ec_ortipork = New NTSInformatica.NTSGridColumn
    Me.ec_oranno = New NTSInformatica.NTSGridColumn
    Me.ec_orserie = New NTSInformatica.NTSGridColumn
    Me.ec_ornum = New NTSInformatica.NTSGridColumn
    Me.ec_orriga = New NTSInformatica.NTSGridColumn
    Me.xxo_codtagl = New NTSInformatica.NTSGridColumn
    Me.xxo_livmindb = New NTSInformatica.NTSGridColumn
    Me.grTco = New NTSInformatica.NTSGrid
    Me.grvTco = New NTSInformatica.NTSGridView
    Me.ec_quant01 = New NTSInformatica.NTSGridColumn
    Me.ec_quant02 = New NTSInformatica.NTSGridColumn
    Me.ec_quant03 = New NTSInformatica.NTSGridColumn
    Me.ec_quant04 = New NTSInformatica.NTSGridColumn
    Me.ec_quant05 = New NTSInformatica.NTSGridColumn
    Me.ec_quant06 = New NTSInformatica.NTSGridColumn
    Me.ec_quant07 = New NTSInformatica.NTSGridColumn
    Me.ec_quant08 = New NTSInformatica.NTSGridColumn
    Me.ec_quant09 = New NTSInformatica.NTSGridColumn
    Me.ec_quant10 = New NTSInformatica.NTSGridColumn
    Me.ec_quant11 = New NTSInformatica.NTSGridColumn
    Me.ec_quant12 = New NTSInformatica.NTSGridColumn
    Me.ec_quant13 = New NTSInformatica.NTSGridColumn
    Me.ec_quant14 = New NTSInformatica.NTSGridColumn
    Me.ec_quant15 = New NTSInformatica.NTSGridColumn
    Me.ec_quant16 = New NTSInformatica.NTSGridColumn
    Me.ec_quant17 = New NTSInformatica.NTSGridColumn
    Me.ec_quant18 = New NTSInformatica.NTSGridColumn
    Me.ec_quant19 = New NTSInformatica.NTSGridColumn
    Me.ec_quant20 = New NTSInformatica.NTSGridColumn
    Me.ec_quant21 = New NTSInformatica.NTSGridColumn
    Me.ec_quant22 = New NTSInformatica.NTSGridColumn
    Me.ec_quant23 = New NTSInformatica.NTSGridColumn
    Me.ec_quant24 = New NTSInformatica.NTSGridColumn
    Me.pnTCO = New NTSInformatica.NTSPanel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnGriglia, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGriglia.SuspendLayout()
    CType(Me.grRighe, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvRighe, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grTco, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvTco, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTCO, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTCO.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbApri, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbConfermatutto, Me.tlbConfermaselez, Me.tlbVisDetTco, Me.tlbZoomFornPrz, Me.tlbRicalcolaCollidaQta, Me.tlbGeneraOrdini, Me.tblPrnAttVideo, Me.tblPrnAttCarta, Me.tblPrnImpVideo, Me.tblPrnImpCarta, Me.tlbRecordNuovo, Me.tlbRecordSalva, Me.tlbRecordRipristina, Me.tlbRecordCancella, Me.tlbMrp, Me.tlbImpegni, Me.tlbLavorazioni, Me.tlbMovimenti, Me.tlbDettTco, Me.tlbProgressivi, Me.tlbSelezionaTutto, Me.tlbDeselezionaTutto, Me.tlbInvertiSelezione, Me.tlbCancRigheSel})
    Me.NtsBarManager1.MaxItemId = 42
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApri), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordNuovo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpegni, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbLavorazioni), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbProgressivi), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbMrp, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbMovimenti), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDettTco), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbApri
    '
    Me.tlbApri.Caption = "Apri"
    Me.tlbApri.Glyph = CType(resources.GetObject("tlbApri.Glyph"), System.Drawing.Image)
    Me.tlbApri.GlyphPath = ""
    Me.tlbApri.Id = 2
    Me.tlbApri.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3)
    Me.tlbApri.Name = "tlbApri"
    Me.tlbApri.Visible = True
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Ritorna"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.GlyphPath = ""
    Me.tlbSalva.Id = 1
    Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F10)
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
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
    'tlbRecordNuovo
    '
    Me.tlbRecordNuovo.Caption = "Record Nuovo"
    Me.tlbRecordNuovo.Glyph = CType(resources.GetObject("tlbRecordNuovo.Glyph"), System.Drawing.Image)
    Me.tlbRecordNuovo.GlyphPath = ""
    Me.tlbRecordNuovo.Id = 28
    Me.tlbRecordNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F2))
    Me.tlbRecordNuovo.Name = "tlbRecordNuovo"
    Me.tlbRecordNuovo.Visible = True
    '
    'tlbRecordSalva
    '
    Me.tlbRecordSalva.Caption = "Record Salva"
    Me.tlbRecordSalva.Glyph = CType(resources.GetObject("tlbRecordSalva.Glyph"), System.Drawing.Image)
    Me.tlbRecordSalva.GlyphPath = ""
    Me.tlbRecordSalva.Id = 29
    Me.tlbRecordSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F9))
    Me.tlbRecordSalva.Name = "tlbRecordSalva"
    Me.tlbRecordSalva.Visible = True
    '
    'tlbRecordRipristina
    '
    Me.tlbRecordRipristina.Caption = "Record Ripristina"
    Me.tlbRecordRipristina.Glyph = CType(resources.GetObject("tlbRecordRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRecordRipristina.GlyphPath = ""
    Me.tlbRecordRipristina.Id = 30
    Me.tlbRecordRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F8))
    Me.tlbRecordRipristina.Name = "tlbRecordRipristina"
    Me.tlbRecordRipristina.Visible = True
    '
    'tlbRecordCancella
    '
    Me.tlbRecordCancella.Caption = "Record Cancella"
    Me.tlbRecordCancella.Glyph = CType(resources.GetObject("tlbRecordCancella.Glyph"), System.Drawing.Image)
    Me.tlbRecordCancella.GlyphPath = ""
    Me.tlbRecordCancella.Id = 31
    Me.tlbRecordCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F4))
    Me.tlbRecordCancella.Name = "tlbRecordCancella"
    Me.tlbRecordCancella.Visible = True
    '
    'tlbImpegni
    '
    Me.tlbImpegni.Caption = "Impegni collegati"
    Me.tlbImpegni.Glyph = CType(resources.GetObject("tlbImpegni.Glyph"), System.Drawing.Image)
    Me.tlbImpegni.GlyphPath = ""
    Me.tlbImpegni.Id = 33
    Me.tlbImpegni.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F7))
    Me.tlbImpegni.Name = "tlbImpegni"
    Me.tlbImpegni.Visible = True
    '
    'tlbLavorazioni
    '
    Me.tlbLavorazioni.Caption = "Lavorazioni collegate"
    Me.tlbLavorazioni.Glyph = CType(resources.GetObject("tlbLavorazioni.Glyph"), System.Drawing.Image)
    Me.tlbLavorazioni.GlyphPath = ""
    Me.tlbLavorazioni.Id = 34
    Me.tlbLavorazioni.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F8))
    Me.tlbLavorazioni.Name = "tlbLavorazioni"
    Me.tlbLavorazioni.Visible = True
    '
    'tlbProgressivi
    '
    Me.tlbProgressivi.Caption = "Progressivi"
    Me.tlbProgressivi.Glyph = CType(resources.GetObject("tlbProgressivi.Glyph"), System.Drawing.Image)
    Me.tlbProgressivi.GlyphPath = ""
    Me.tlbProgressivi.Id = 37
    Me.tlbProgressivi.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O))
    Me.tlbProgressivi.Name = "tlbProgressivi"
    Me.tlbProgressivi.Visible = True
    '
    'tlbMrp
    '
    Me.tlbMrp.Caption = "Navigazione MRP"
    Me.tlbMrp.Glyph = CType(resources.GetObject("tlbMrp.Glyph"), System.Drawing.Image)
    Me.tlbMrp.GlyphPath = ""
    Me.tlbMrp.Id = 32
    Me.tlbMrp.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N))
    Me.tlbMrp.Name = "tlbMrp"
    Me.tlbMrp.Visible = True
    '
    'tlbMovimenti
    '
    Me.tlbMovimenti.Caption = "Movimenti collegati"
    Me.tlbMovimenti.Glyph = CType(resources.GetObject("tlbMovimenti.Glyph"), System.Drawing.Image)
    Me.tlbMovimenti.GlyphPath = ""
    Me.tlbMovimenti.Id = 35
    Me.tlbMovimenti.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M))
    Me.tlbMovimenti.Name = "tlbMovimenti"
    Me.tlbMovimenti.Visible = True
    '
    'tlbDettTco
    '
    Me.tlbDettTco.Caption = "Dettaglio TCO"
    Me.tlbDettTco.Glyph = CType(resources.GetObject("tlbDettTco.Glyph"), System.Drawing.Image)
    Me.tlbDettTco.GlyphPath = ""
    Me.tlbDettTco.Id = 36
    Me.tlbDettTco.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q))
    Me.tlbDettTco.Name = "tlbDettTco"
    Me.tlbDettTco.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 15
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbConfermatutto), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbConfermaselez), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGeneraOrdini), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancRigheSel), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbVisDetTco, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoomFornPrz, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRicalcolaCollidaQta), New DevExpress.XtraBars.LinkPersistInfo(Me.tblPrnAttVideo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tblPrnAttCarta), New DevExpress.XtraBars.LinkPersistInfo(Me.tblPrnImpVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tblPrnImpCarta), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSelezionaTutto, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDeselezionaTutto), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbInvertiSelezione)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbConfermatutto
    '
    Me.tlbConfermatutto.Caption = "Conferma tutte le righe"
    Me.tlbConfermatutto.GlyphPath = ""
    Me.tlbConfermatutto.Id = 17
    Me.tlbConfermatutto.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F9))
    Me.tlbConfermatutto.Name = "tlbConfermatutto"
    Me.tlbConfermatutto.NTSIsCheckBox = False
    Me.tlbConfermatutto.Visible = True
    '
    'tlbConfermaselez
    '
    Me.tlbConfermaselez.Caption = "Conferma righe selezionate"
    Me.tlbConfermaselez.GlyphPath = ""
    Me.tlbConfermaselez.Id = 18
    Me.tlbConfermaselez.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F6))
    Me.tlbConfermaselez.Name = "tlbConfermaselez"
    Me.tlbConfermaselez.NTSIsCheckBox = False
    Me.tlbConfermaselez.Visible = True
    '
    'tlbGeneraOrdini
    '
    Me.tlbGeneraOrdini.Caption = "Generazione ordini da righe selezionate"
    Me.tlbGeneraOrdini.GlyphPath = ""
    Me.tlbGeneraOrdini.Id = 23
    Me.tlbGeneraOrdini.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G))
    Me.tlbGeneraOrdini.Name = "tlbGeneraOrdini"
    Me.tlbGeneraOrdini.NTSIsCheckBox = False
    Me.tlbGeneraOrdini.Visible = True
    '
    'tlbCancRigheSel
    '
    Me.tlbCancRigheSel.Caption = "Cancella righe selezionate"
    Me.tlbCancRigheSel.GlyphPath = ""
    Me.tlbCancRigheSel.Id = 41
    Me.tlbCancRigheSel.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F4))
    Me.tlbCancRigheSel.Name = "tlbCancRigheSel"
    Me.tlbCancRigheSel.NTSIsCheckBox = False
    Me.tlbCancRigheSel.Visible = True
    '
    'tlbVisDetTco
    '
    Me.tlbVisDetTco.Caption = "Visualizza dettagli per taglia"
    Me.tlbVisDetTco.GlyphPath = ""
    Me.tlbVisDetTco.Id = 19
    Me.tlbVisDetTco.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.T))
    Me.tlbVisDetTco.Name = "tlbVisDetTco"
    Me.tlbVisDetTco.NTSIsCheckBox = False
    Me.tlbVisDetTco.Visible = True
    '
    'tlbZoomFornPrz
    '
    Me.tlbZoomFornPrz.AllowAllUp = True
    Me.tlbZoomFornPrz.Caption = "Zoom fornitori prezzi"
    Me.tlbZoomFornPrz.GlyphPath = ""
    Me.tlbZoomFornPrz.Id = 21
    Me.tlbZoomFornPrz.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F5))
    Me.tlbZoomFornPrz.Name = "tlbZoomFornPrz"
    Me.tlbZoomFornPrz.NTSIsCheckBox = False
    Me.tlbZoomFornPrz.Visible = True
    '
    'tlbRicalcolaCollidaQta
    '
    Me.tlbRicalcolaCollidaQta.Caption = "Ricalcola colli da quantità"
    Me.tlbRicalcolaCollidaQta.GlyphPath = ""
    Me.tlbRicalcolaCollidaQta.Id = 22
    Me.tlbRicalcolaCollidaQta.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbRicalcolaCollidaQta.Name = "tlbRicalcolaCollidaQta"
    Me.tlbRicalcolaCollidaQta.NTSIsCheckBox = False
    Me.tlbRicalcolaCollidaQta.Visible = True
    '
    'tblPrnAttVideo
    '
    Me.tblPrnAttVideo.Caption = "Stampa Attività a video"
    Me.tblPrnAttVideo.GlyphPath = ""
    Me.tblPrnAttVideo.Id = 24
    Me.tblPrnAttVideo.Name = "tblPrnAttVideo"
    Me.tblPrnAttVideo.NTSIsCheckBox = False
    Me.tblPrnAttVideo.Visible = True
    '
    'tblPrnAttCarta
    '
    Me.tblPrnAttCarta.Caption = "Stampa Attività su carta"
    Me.tblPrnAttCarta.GlyphPath = ""
    Me.tblPrnAttCarta.Id = 25
    Me.tblPrnAttCarta.Name = "tblPrnAttCarta"
    Me.tblPrnAttCarta.NTSIsCheckBox = False
    Me.tblPrnAttCarta.Visible = True
    '
    'tblPrnImpVideo
    '
    Me.tblPrnImpVideo.Caption = "Stampa Impegni a video"
    Me.tblPrnImpVideo.GlyphPath = ""
    Me.tblPrnImpVideo.Id = 26
    Me.tblPrnImpVideo.Name = "tblPrnImpVideo"
    Me.tblPrnImpVideo.NTSIsCheckBox = False
    Me.tblPrnImpVideo.Visible = True
    '
    'tblPrnImpCarta
    '
    Me.tblPrnImpCarta.Caption = "Stampa Impegni su carta"
    Me.tblPrnImpCarta.GlyphPath = ""
    Me.tblPrnImpCarta.Id = 27
    Me.tblPrnImpCarta.Name = "tblPrnImpCarta"
    Me.tblPrnImpCarta.NTSIsCheckBox = False
    Me.tblPrnImpCarta.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.GlyphPath = ""
    Me.tlbImpostaStampante.Id = 16
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbSelezionaTutto
    '
    Me.tlbSelezionaTutto.Caption = "Seleziona tutte le righe"
    Me.tlbSelezionaTutto.GlyphPath = ""
    Me.tlbSelezionaTutto.Id = 38
    Me.tlbSelezionaTutto.Name = "tlbSelezionaTutto"
    Me.tlbSelezionaTutto.NTSIsCheckBox = False
    Me.tlbSelezionaTutto.Visible = True
    '
    'tlbDeselezionaTutto
    '
    Me.tlbDeselezionaTutto.Caption = "Deseleziona tutte le righe"
    Me.tlbDeselezionaTutto.GlyphPath = ""
    Me.tlbDeselezionaTutto.Id = 39
    Me.tlbDeselezionaTutto.Name = "tlbDeselezionaTutto"
    Me.tlbDeselezionaTutto.NTSIsCheckBox = False
    Me.tlbDeselezionaTutto.Visible = True
    '
    'tlbInvertiSelezione
    '
    Me.tlbInvertiSelezione.Caption = "Inverti selezione righe"
    Me.tlbInvertiSelezione.GlyphPath = ""
    Me.tlbInvertiSelezione.Id = 40
    Me.tlbInvertiSelezione.Name = "tlbInvertiSelezione"
    Me.tlbInvertiSelezione.NTSIsCheckBox = False
    Me.tlbInvertiSelezione.Visible = True
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
    'pnGriglia
    '
    Me.pnGriglia.AllowDrop = True
    Me.pnGriglia.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGriglia.Appearance.Options.UseBackColor = True
    Me.pnGriglia.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGriglia.Controls.Add(Me.grRighe)
    Me.pnGriglia.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGriglia.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGriglia.Location = New System.Drawing.Point(0, 30)
    Me.pnGriglia.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGriglia.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGriglia.Name = "pnGriglia"
    Me.pnGriglia.NTSActiveTrasparency = True
    Me.pnGriglia.Size = New System.Drawing.Size(648, 349)
    Me.pnGriglia.TabIndex = 4
    Me.pnGriglia.Text = "NtsPanel1"
    '
    'grRighe
    '
    Me.grRighe.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grRighe.EmbeddedNavigator.Name = ""
    Me.grRighe.Location = New System.Drawing.Point(0, 0)
    Me.grRighe.MainView = Me.grvRighe
    Me.grRighe.Name = "grRighe"
    Me.grRighe.Size = New System.Drawing.Size(648, 349)
    Me.grRighe.TabIndex = 3
    Me.grRighe.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvRighe})
    '
    'grvRighe
    '
    Me.grvRighe.ActiveFilterEnabled = False
    '
    'xxo_prznet
    '
    Me.xxo_prznet.AppearanceCell.Options.UseBackColor = True
    Me.xxo_prznet.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_prznet.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_prznet.Caption = "Prezzo netto"
    Me.xxo_prznet.Enabled = False
    Me.xxo_prznet.FieldName = "xxo_prznet"
    Me.xxo_prznet.Name = "xxo_prznet"
    Me.xxo_prznet.NTSRepositoryComboBox = Nothing
    Me.xxo_prznet.NTSRepositoryItemCheck = Nothing
    Me.xxo_prznet.NTSRepositoryItemMemo = Nothing
    Me.xxo_prznet.NTSRepositoryItemText = Nothing
    Me.xxo_prznet.OptionsColumn.AllowEdit = False
    Me.xxo_prznet.OptionsColumn.ReadOnly = True
    Me.xxo_prznet.Visible = True
    Me.xxo_prznet.VisibleIndex = 42
    Me.grvRighe.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xxo_seleziona, Me.ec_riga, Me.ec_codart, Me.ec_descr, Me.ec_desint, Me.ec_unmis, Me.ec_colli, Me.ec_ump, Me.ec_quant, Me.ec_conto, Me.xxo_conto, Me.ec_datcons, Me.ec_codvalu, Me.xxo_codvalu, Me.ec_cambio, Me.ec_prezvalc, Me.ec_prezzo, Me.ec_stato, Me.ec_scont1, Me.ec_scont2, Me.ec_scont3, Me.ec_scont4, Me.ec_scont5, Me.ec_scont6, Me.ec_magaz, Me.xxo_magaz, Me.ec_magaz2, Me.xxo_magaz2, Me.ec_magimp, Me.xxo_magimp, Me.ec_datord, Me.ec_controp, Me.xxo_controp, Me.ec_codiva, Me.xxo_codiva, Me.ec_codcfam, Me.xxo_codcfam, Me.ec_commeca, Me.xxo_commeca, Me.ec_subcommeca, Me.ec_codcena, Me.xxo_codcena, Me.xxo_lottox, Me.ec_note, Me.ec_valore, Me.ec_contocontr, Me.xxo_contocon, Me.ec_codclie, Me.xxo_codclie, Me.ec_misura1, Me.ec_misura2, Me.ec_misura3, Me.ec_perqta, Me.ec_flcom, Me.ec_flprznet, Me.ec_flforf, Me.ec_matric, Me.ec_umprz, Me.ec_fase, Me.xxo_fase, Me.ec_codlavo, Me.xxo_codlavo, Me.ec_datini, Me.ec_datfin, Me.ec_valorev, Me.ec_pmtaskid, Me.xxo_pmtaskid, Me.ec_pmsalcon, Me.ec_pmqtadis, Me.ec_pmvaldis, Me.ec_rdatipork, Me.ec_rdaanno, Me.ec_rdaserie, Me.ec_rdanum, Me.ec_rdariga, Me.ec_offreq, Me.ec_ortipork, Me.ec_oranno, Me.ec_orserie, Me.ec_ornum, Me.ec_orriga, Me.xxo_codtagl, Me.xxo_livmindb, Me.xxo_prznet})
    Me.grvRighe.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvRighe.Enabled = True
    Me.grvRighe.GridControl = Me.grRighe
    Me.grvRighe.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvRighe.MinRowHeight = 14
    Me.grvRighe.Name = "grvRighe"
    Me.grvRighe.NTSAllowDelete = True
    Me.grvRighe.NTSAllowInsert = True
    Me.grvRighe.NTSAllowUpdate = True
    Me.grvRighe.NTSMenuContext = Nothing
    Me.grvRighe.OptionsCustomization.AllowRowSizing = True
    Me.grvRighe.OptionsFilter.AllowFilterEditor = False
    Me.grvRighe.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvRighe.OptionsNavigation.UseTabKey = False
    Me.grvRighe.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvRighe.OptionsView.ColumnAutoWidth = False
    Me.grvRighe.OptionsView.EnableAppearanceEvenRow = True
    Me.grvRighe.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvRighe.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvRighe.OptionsView.ShowGroupPanel = False
    Me.grvRighe.RowHeight = 16
    '
    'xxo_seleziona
    '
    Me.xxo_seleziona.AppearanceCell.Options.UseBackColor = True
    Me.xxo_seleziona.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_seleziona.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_seleziona.Caption = "Seleziona"
    Me.xxo_seleziona.Enabled = True
    Me.xxo_seleziona.FieldName = "xxo_seleziona"
    Me.xxo_seleziona.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_seleziona.Name = "xxo_seleziona"
    Me.xxo_seleziona.NTSRepositoryComboBox = Nothing
    Me.xxo_seleziona.NTSRepositoryItemCheck = Nothing
    Me.xxo_seleziona.NTSRepositoryItemMemo = Nothing
    Me.xxo_seleziona.NTSRepositoryItemText = Nothing
    Me.xxo_seleziona.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_seleziona.OptionsFilter.AllowFilter = False
    Me.xxo_seleziona.Visible = True
    Me.xxo_seleziona.VisibleIndex = 0
    Me.xxo_seleziona.Width = 32
    '
    'ec_riga
    '
    Me.ec_riga.AppearanceCell.Options.UseBackColor = True
    Me.ec_riga.AppearanceCell.Options.UseTextOptions = True
    Me.ec_riga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_riga.Caption = "Progr."
    Me.ec_riga.Enabled = False
    Me.ec_riga.FieldName = "ec_riga"
    Me.ec_riga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_riga.Name = "ec_riga"
    Me.ec_riga.NTSRepositoryComboBox = Nothing
    Me.ec_riga.NTSRepositoryItemCheck = Nothing
    Me.ec_riga.NTSRepositoryItemMemo = Nothing
    Me.ec_riga.NTSRepositoryItemText = Nothing
    Me.ec_riga.OptionsColumn.AllowEdit = False
    Me.ec_riga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_riga.OptionsColumn.ReadOnly = True
    Me.ec_riga.OptionsFilter.AllowFilter = False
    Me.ec_riga.Visible = True
    Me.ec_riga.VisibleIndex = 1
    Me.ec_riga.Width = 73
    '
    'ec_codart
    '
    Me.ec_codart.AppearanceCell.Options.UseBackColor = True
    Me.ec_codart.AppearanceCell.Options.UseTextOptions = True
    Me.ec_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_codart.Caption = "Cod. Art."
    Me.ec_codart.Enabled = True
    Me.ec_codart.FieldName = "ec_codart"
    Me.ec_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_codart.Name = "ec_codart"
    Me.ec_codart.NTSRepositoryComboBox = Nothing
    Me.ec_codart.NTSRepositoryItemCheck = Nothing
    Me.ec_codart.NTSRepositoryItemMemo = Nothing
    Me.ec_codart.NTSRepositoryItemText = Nothing
    Me.ec_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_codart.OptionsFilter.AllowFilter = False
    Me.ec_codart.Visible = True
    Me.ec_codart.VisibleIndex = 2
    Me.ec_codart.Width = 64
    '
    'ec_descr
    '
    Me.ec_descr.AppearanceCell.Options.UseBackColor = True
    Me.ec_descr.AppearanceCell.Options.UseTextOptions = True
    Me.ec_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_descr.Caption = "Descrizione"
    Me.ec_descr.Enabled = True
    Me.ec_descr.FieldName = "ec_descr"
    Me.ec_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_descr.Name = "ec_descr"
    Me.ec_descr.NTSRepositoryComboBox = Nothing
    Me.ec_descr.NTSRepositoryItemCheck = Nothing
    Me.ec_descr.NTSRepositoryItemMemo = Nothing
    Me.ec_descr.NTSRepositoryItemText = Nothing
    Me.ec_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_descr.OptionsFilter.AllowFilter = False
    Me.ec_descr.Visible = True
    Me.ec_descr.VisibleIndex = 3
    Me.ec_descr.Width = 139
    '
    'ec_desint
    '
    Me.ec_desint.AppearanceCell.Options.UseBackColor = True
    Me.ec_desint.AppearanceCell.Options.UseTextOptions = True
    Me.ec_desint.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_desint.Caption = "Descr.interna"
    Me.ec_desint.Enabled = True
    Me.ec_desint.FieldName = "ec_desint"
    Me.ec_desint.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_desint.Name = "ec_desint"
    Me.ec_desint.NTSRepositoryComboBox = Nothing
    Me.ec_desint.NTSRepositoryItemCheck = Nothing
    Me.ec_desint.NTSRepositoryItemMemo = Nothing
    Me.ec_desint.NTSRepositoryItemText = Nothing
    Me.ec_desint.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_desint.OptionsFilter.AllowFilter = False
    Me.ec_desint.Width = 126
    '
    'ec_unmis
    '
    Me.ec_unmis.AppearanceCell.Options.UseBackColor = True
    Me.ec_unmis.AppearanceCell.Options.UseTextOptions = True
    Me.ec_unmis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_unmis.Caption = "U.M."
    Me.ec_unmis.Enabled = True
    Me.ec_unmis.FieldName = "ec_unmis"
    Me.ec_unmis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_unmis.Name = "ec_unmis"
    Me.ec_unmis.NTSRepositoryComboBox = Nothing
    Me.ec_unmis.NTSRepositoryItemCheck = Nothing
    Me.ec_unmis.NTSRepositoryItemMemo = Nothing
    Me.ec_unmis.NTSRepositoryItemText = Nothing
    Me.ec_unmis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_unmis.OptionsFilter.AllowFilter = False
    Me.ec_unmis.Visible = True
    Me.ec_unmis.VisibleIndex = 4
    Me.ec_unmis.Width = 39
    '
    'ec_colli
    '
    Me.ec_colli.AppearanceCell.Options.UseBackColor = True
    Me.ec_colli.AppearanceCell.Options.UseTextOptions = True
    Me.ec_colli.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_colli.Caption = "Colli ord."
    Me.ec_colli.Enabled = True
    Me.ec_colli.FieldName = "ec_colli"
    Me.ec_colli.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_colli.Name = "ec_colli"
    Me.ec_colli.NTSRepositoryComboBox = Nothing
    Me.ec_colli.NTSRepositoryItemCheck = Nothing
    Me.ec_colli.NTSRepositoryItemMemo = Nothing
    Me.ec_colli.NTSRepositoryItemText = Nothing
    Me.ec_colli.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_colli.OptionsFilter.AllowFilter = False
    Me.ec_colli.Visible = True
    Me.ec_colli.VisibleIndex = 5
    Me.ec_colli.Width = 54
    '
    'ec_ump
    '
    Me.ec_ump.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.ec_ump.AppearanceCell.Options.UseBackColor = True
    Me.ec_ump.AppearanceCell.Options.UseTextOptions = True
    Me.ec_ump.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_ump.Caption = "UMP"
    Me.ec_ump.Enabled = False
    Me.ec_ump.FieldName = "ec_ump"
    Me.ec_ump.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_ump.Name = "ec_ump"
    Me.ec_ump.NTSRepositoryComboBox = Nothing
    Me.ec_ump.NTSRepositoryItemCheck = Nothing
    Me.ec_ump.NTSRepositoryItemMemo = Nothing
    Me.ec_ump.NTSRepositoryItemText = Nothing
    Me.ec_ump.OptionsColumn.AllowEdit = False
    Me.ec_ump.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_ump.OptionsColumn.ReadOnly = True
    Me.ec_ump.OptionsFilter.AllowFilter = False
    Me.ec_ump.Visible = True
    Me.ec_ump.VisibleIndex = 6
    '
    'ec_quant
    '
    Me.ec_quant.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant.Caption = "Q.tà ordin."
    Me.ec_quant.Enabled = True
    Me.ec_quant.FieldName = "ec_quant"
    Me.ec_quant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant.Name = "ec_quant"
    Me.ec_quant.NTSRepositoryComboBox = Nothing
    Me.ec_quant.NTSRepositoryItemCheck = Nothing
    Me.ec_quant.NTSRepositoryItemMemo = Nothing
    Me.ec_quant.NTSRepositoryItemText = Nothing
    Me.ec_quant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant.OptionsFilter.AllowFilter = False
    Me.ec_quant.Visible = True
    Me.ec_quant.VisibleIndex = 7
    '
    'ec_conto
    '
    Me.ec_conto.AppearanceCell.Options.UseBackColor = True
    Me.ec_conto.AppearanceCell.Options.UseTextOptions = True
    Me.ec_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_conto.Caption = "Fornitore"
    Me.ec_conto.Enabled = True
    Me.ec_conto.FieldName = "ec_conto"
    Me.ec_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_conto.Name = "ec_conto"
    Me.ec_conto.NTSRepositoryComboBox = Nothing
    Me.ec_conto.NTSRepositoryItemCheck = Nothing
    Me.ec_conto.NTSRepositoryItemMemo = Nothing
    Me.ec_conto.NTSRepositoryItemText = Nothing
    Me.ec_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_conto.OptionsFilter.AllowFilter = False
    Me.ec_conto.Visible = True
    Me.ec_conto.VisibleIndex = 8
    Me.ec_conto.Width = 54
    '
    'xxo_conto
    '
    Me.xxo_conto.AppearanceCell.Options.UseBackColor = True
    Me.xxo_conto.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_conto.Caption = "Descr. forn."
    Me.xxo_conto.Enabled = False
    Me.xxo_conto.FieldName = "xxo_conto"
    Me.xxo_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_conto.Name = "xxo_conto"
    Me.xxo_conto.NTSRepositoryComboBox = Nothing
    Me.xxo_conto.NTSRepositoryItemCheck = Nothing
    Me.xxo_conto.NTSRepositoryItemMemo = Nothing
    Me.xxo_conto.NTSRepositoryItemText = Nothing
    Me.xxo_conto.OptionsColumn.AllowEdit = False
    Me.xxo_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_conto.OptionsColumn.ReadOnly = True
    Me.xxo_conto.OptionsFilter.AllowFilter = False
    Me.xxo_conto.Visible = True
    Me.xxo_conto.VisibleIndex = 9
    Me.xxo_conto.Width = 51
    '
    'ec_datcons
    '
    Me.ec_datcons.AppearanceCell.Options.UseBackColor = True
    Me.ec_datcons.AppearanceCell.Options.UseTextOptions = True
    Me.ec_datcons.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_datcons.Caption = "Data cons."
    Me.ec_datcons.Enabled = True
    Me.ec_datcons.FieldName = "ec_datcons"
    Me.ec_datcons.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_datcons.Name = "ec_datcons"
    Me.ec_datcons.NTSRepositoryComboBox = Nothing
    Me.ec_datcons.NTSRepositoryItemCheck = Nothing
    Me.ec_datcons.NTSRepositoryItemMemo = Nothing
    Me.ec_datcons.NTSRepositoryItemText = Nothing
    Me.ec_datcons.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_datcons.OptionsFilter.AllowFilter = False
    Me.ec_datcons.Visible = True
    Me.ec_datcons.VisibleIndex = 10
    '
    'ec_codvalu
    '
    Me.ec_codvalu.AppearanceCell.Options.UseBackColor = True
    Me.ec_codvalu.AppearanceCell.Options.UseTextOptions = True
    Me.ec_codvalu.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_codvalu.Caption = "Valuta"
    Me.ec_codvalu.Enabled = True
    Me.ec_codvalu.FieldName = "ec_codvalu"
    Me.ec_codvalu.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_codvalu.Name = "ec_codvalu"
    Me.ec_codvalu.NTSRepositoryComboBox = Nothing
    Me.ec_codvalu.NTSRepositoryItemCheck = Nothing
    Me.ec_codvalu.NTSRepositoryItemMemo = Nothing
    Me.ec_codvalu.NTSRepositoryItemText = Nothing
    Me.ec_codvalu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_codvalu.OptionsFilter.AllowFilter = False
    Me.ec_codvalu.Visible = True
    Me.ec_codvalu.VisibleIndex = 11
    '
    'xxo_codvalu
    '
    Me.xxo_codvalu.AppearanceCell.Options.UseBackColor = True
    Me.xxo_codvalu.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_codvalu.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_codvalu.Caption = "Descr. valuta"
    Me.xxo_codvalu.Enabled = False
    Me.xxo_codvalu.FieldName = "xxo_codvalu"
    Me.xxo_codvalu.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_codvalu.Name = "xxo_codvalu"
    Me.xxo_codvalu.NTSRepositoryComboBox = Nothing
    Me.xxo_codvalu.NTSRepositoryItemCheck = Nothing
    Me.xxo_codvalu.NTSRepositoryItemMemo = Nothing
    Me.xxo_codvalu.NTSRepositoryItemText = Nothing
    Me.xxo_codvalu.OptionsColumn.AllowEdit = False
    Me.xxo_codvalu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_codvalu.OptionsColumn.ReadOnly = True
    Me.xxo_codvalu.OptionsFilter.AllowFilter = False
    Me.xxo_codvalu.Visible = True
    Me.xxo_codvalu.VisibleIndex = 12
    '
    'ec_cambio
    '
    Me.ec_cambio.AppearanceCell.Options.UseBackColor = True
    Me.ec_cambio.AppearanceCell.Options.UseTextOptions = True
    Me.ec_cambio.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_cambio.Caption = "Cambio"
    Me.ec_cambio.Enabled = True
    Me.ec_cambio.FieldName = "ec_cambio"
    Me.ec_cambio.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_cambio.Name = "ec_cambio"
    Me.ec_cambio.NTSRepositoryComboBox = Nothing
    Me.ec_cambio.NTSRepositoryItemCheck = Nothing
    Me.ec_cambio.NTSRepositoryItemMemo = Nothing
    Me.ec_cambio.NTSRepositoryItemText = Nothing
    Me.ec_cambio.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_cambio.OptionsFilter.AllowFilter = False
    Me.ec_cambio.Visible = True
    Me.ec_cambio.VisibleIndex = 13
    '
    'ec_prezvalc
    '
    Me.ec_prezvalc.AppearanceCell.Options.UseBackColor = True
    Me.ec_prezvalc.AppearanceCell.Options.UseTextOptions = True
    Me.ec_prezvalc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_prezvalc.Caption = "Prezzo valuta"
    Me.ec_prezvalc.Enabled = True
    Me.ec_prezvalc.FieldName = "ec_prezvalc"
    Me.ec_prezvalc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_prezvalc.Name = "ec_prezvalc"
    Me.ec_prezvalc.NTSRepositoryComboBox = Nothing
    Me.ec_prezvalc.NTSRepositoryItemCheck = Nothing
    Me.ec_prezvalc.NTSRepositoryItemMemo = Nothing
    Me.ec_prezvalc.NTSRepositoryItemText = Nothing
    Me.ec_prezvalc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_prezvalc.OptionsFilter.AllowFilter = False
    Me.ec_prezvalc.Visible = True
    Me.ec_prezvalc.VisibleIndex = 14
    '
    'ec_prezzo
    '
    Me.ec_prezzo.AppearanceCell.Options.UseBackColor = True
    Me.ec_prezzo.AppearanceCell.Options.UseTextOptions = True
    Me.ec_prezzo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_prezzo.Caption = "Prezzo"
    Me.ec_prezzo.Enabled = True
    Me.ec_prezzo.FieldName = "ec_prezzo"
    Me.ec_prezzo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_prezzo.Name = "ec_prezzo"
    Me.ec_prezzo.NTSRepositoryComboBox = Nothing
    Me.ec_prezzo.NTSRepositoryItemCheck = Nothing
    Me.ec_prezzo.NTSRepositoryItemMemo = Nothing
    Me.ec_prezzo.NTSRepositoryItemText = Nothing
    Me.ec_prezzo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_prezzo.OptionsFilter.AllowFilter = False
    Me.ec_prezzo.Visible = True
    Me.ec_prezzo.VisibleIndex = 15
    '
    'ec_stato
    '
    Me.ec_stato.AppearanceCell.Options.UseBackColor = True
    Me.ec_stato.AppearanceCell.Options.UseTextOptions = True
    Me.ec_stato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_stato.Caption = "Stato"
    Me.ec_stato.Enabled = True
    Me.ec_stato.FieldName = "ec_stato"
    Me.ec_stato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_stato.Name = "ec_stato"
    Me.ec_stato.NTSRepositoryComboBox = Nothing
    Me.ec_stato.NTSRepositoryItemCheck = Nothing
    Me.ec_stato.NTSRepositoryItemMemo = Nothing
    Me.ec_stato.NTSRepositoryItemText = Nothing
    Me.ec_stato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_stato.OptionsFilter.AllowFilter = False
    Me.ec_stato.Visible = True
    Me.ec_stato.VisibleIndex = 16
    '
    'ec_scont1
    '
    Me.ec_scont1.AppearanceCell.Options.UseBackColor = True
    Me.ec_scont1.AppearanceCell.Options.UseTextOptions = True
    Me.ec_scont1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_scont1.Caption = "Sconto 1"
    Me.ec_scont1.Enabled = True
    Me.ec_scont1.FieldName = "ec_scont1"
    Me.ec_scont1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_scont1.Name = "ec_scont1"
    Me.ec_scont1.NTSRepositoryComboBox = Nothing
    Me.ec_scont1.NTSRepositoryItemCheck = Nothing
    Me.ec_scont1.NTSRepositoryItemMemo = Nothing
    Me.ec_scont1.NTSRepositoryItemText = Nothing
    Me.ec_scont1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_scont1.OptionsFilter.AllowFilter = False
    Me.ec_scont1.Visible = True
    Me.ec_scont1.VisibleIndex = 17
    '
    'ec_scont2
    '
    Me.ec_scont2.AppearanceCell.Options.UseBackColor = True
    Me.ec_scont2.AppearanceCell.Options.UseTextOptions = True
    Me.ec_scont2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_scont2.Caption = "Sconto 2"
    Me.ec_scont2.Enabled = True
    Me.ec_scont2.FieldName = "ec_scont2"
    Me.ec_scont2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_scont2.Name = "ec_scont2"
    Me.ec_scont2.NTSRepositoryComboBox = Nothing
    Me.ec_scont2.NTSRepositoryItemCheck = Nothing
    Me.ec_scont2.NTSRepositoryItemMemo = Nothing
    Me.ec_scont2.NTSRepositoryItemText = Nothing
    Me.ec_scont2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_scont2.OptionsFilter.AllowFilter = False
    Me.ec_scont2.Visible = True
    Me.ec_scont2.VisibleIndex = 18
    '
    'ec_scont3
    '
    Me.ec_scont3.AppearanceCell.Options.UseBackColor = True
    Me.ec_scont3.AppearanceCell.Options.UseTextOptions = True
    Me.ec_scont3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_scont3.Caption = "Sconto 3"
    Me.ec_scont3.Enabled = True
    Me.ec_scont3.FieldName = "ec_scont3"
    Me.ec_scont3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_scont3.Name = "ec_scont3"
    Me.ec_scont3.NTSRepositoryComboBox = Nothing
    Me.ec_scont3.NTSRepositoryItemCheck = Nothing
    Me.ec_scont3.NTSRepositoryItemMemo = Nothing
    Me.ec_scont3.NTSRepositoryItemText = Nothing
    Me.ec_scont3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_scont3.OptionsFilter.AllowFilter = False
    Me.ec_scont3.Visible = True
    Me.ec_scont3.VisibleIndex = 19
    '
    'ec_scont4
    '
    Me.ec_scont4.AppearanceCell.Options.UseBackColor = True
    Me.ec_scont4.AppearanceCell.Options.UseTextOptions = True
    Me.ec_scont4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_scont4.Caption = "Sconto 4"
    Me.ec_scont4.Enabled = True
    Me.ec_scont4.FieldName = "ec_scont4"
    Me.ec_scont4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_scont4.Name = "ec_scont4"
    Me.ec_scont4.NTSRepositoryComboBox = Nothing
    Me.ec_scont4.NTSRepositoryItemCheck = Nothing
    Me.ec_scont4.NTSRepositoryItemMemo = Nothing
    Me.ec_scont4.NTSRepositoryItemText = Nothing
    Me.ec_scont4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_scont4.OptionsFilter.AllowFilter = False
    '
    'ec_scont5
    '
    Me.ec_scont5.AppearanceCell.Options.UseBackColor = True
    Me.ec_scont5.AppearanceCell.Options.UseTextOptions = True
    Me.ec_scont5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_scont5.Caption = "Sconto 5"
    Me.ec_scont5.Enabled = True
    Me.ec_scont5.FieldName = "ec_scont5"
    Me.ec_scont5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_scont5.Name = "ec_scont5"
    Me.ec_scont5.NTSRepositoryComboBox = Nothing
    Me.ec_scont5.NTSRepositoryItemCheck = Nothing
    Me.ec_scont5.NTSRepositoryItemMemo = Nothing
    Me.ec_scont5.NTSRepositoryItemText = Nothing
    Me.ec_scont5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_scont5.OptionsFilter.AllowFilter = False
    '
    'ec_scont6
    '
    Me.ec_scont6.AppearanceCell.Options.UseBackColor = True
    Me.ec_scont6.AppearanceCell.Options.UseTextOptions = True
    Me.ec_scont6.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_scont6.Caption = "Sconto 6"
    Me.ec_scont6.Enabled = True
    Me.ec_scont6.FieldName = "ec_scont6"
    Me.ec_scont6.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_scont6.Name = "ec_scont6"
    Me.ec_scont6.NTSRepositoryComboBox = Nothing
    Me.ec_scont6.NTSRepositoryItemCheck = Nothing
    Me.ec_scont6.NTSRepositoryItemMemo = Nothing
    Me.ec_scont6.NTSRepositoryItemText = Nothing
    Me.ec_scont6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_scont6.OptionsFilter.AllowFilter = False
    '
    'ec_magaz
    '
    Me.ec_magaz.AppearanceCell.Options.UseBackColor = True
    Me.ec_magaz.AppearanceCell.Options.UseTextOptions = True
    Me.ec_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_magaz.Caption = "Magazzino"
    Me.ec_magaz.Enabled = True
    Me.ec_magaz.FieldName = "ec_magaz"
    Me.ec_magaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_magaz.Name = "ec_magaz"
    Me.ec_magaz.NTSRepositoryComboBox = Nothing
    Me.ec_magaz.NTSRepositoryItemCheck = Nothing
    Me.ec_magaz.NTSRepositoryItemMemo = Nothing
    Me.ec_magaz.NTSRepositoryItemText = Nothing
    Me.ec_magaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_magaz.OptionsFilter.AllowFilter = False
    Me.ec_magaz.Visible = True
    Me.ec_magaz.VisibleIndex = 20
    '
    'xxo_magaz
    '
    Me.xxo_magaz.AppearanceCell.Options.UseBackColor = True
    Me.xxo_magaz.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_magaz.Caption = "Descr. magazzino"
    Me.xxo_magaz.Enabled = False
    Me.xxo_magaz.FieldName = "xxo_magaz"
    Me.xxo_magaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_magaz.Name = "xxo_magaz"
    Me.xxo_magaz.NTSRepositoryComboBox = Nothing
    Me.xxo_magaz.NTSRepositoryItemCheck = Nothing
    Me.xxo_magaz.NTSRepositoryItemMemo = Nothing
    Me.xxo_magaz.NTSRepositoryItemText = Nothing
    Me.xxo_magaz.OptionsColumn.AllowEdit = False
    Me.xxo_magaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_magaz.OptionsColumn.ReadOnly = True
    Me.xxo_magaz.OptionsFilter.AllowFilter = False
    Me.xxo_magaz.Visible = True
    Me.xxo_magaz.VisibleIndex = 21
    '
    'ec_magaz2
    '
    Me.ec_magaz2.AppearanceCell.Options.UseBackColor = True
    Me.ec_magaz2.AppearanceCell.Options.UseTextOptions = True
    Me.ec_magaz2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_magaz2.Caption = "Magaz 2"
    Me.ec_magaz2.Enabled = True
    Me.ec_magaz2.FieldName = "ec_magaz2"
    Me.ec_magaz2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_magaz2.Name = "ec_magaz2"
    Me.ec_magaz2.NTSRepositoryComboBox = Nothing
    Me.ec_magaz2.NTSRepositoryItemCheck = Nothing
    Me.ec_magaz2.NTSRepositoryItemMemo = Nothing
    Me.ec_magaz2.NTSRepositoryItemText = Nothing
    Me.ec_magaz2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_magaz2.OptionsFilter.AllowFilter = False
    Me.ec_magaz2.Visible = True
    Me.ec_magaz2.VisibleIndex = 22
    '
    'xxo_magaz2
    '
    Me.xxo_magaz2.AppearanceCell.Options.UseBackColor = True
    Me.xxo_magaz2.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_magaz2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_magaz2.Caption = "Descr. magaz. 2"
    Me.xxo_magaz2.Enabled = False
    Me.xxo_magaz2.FieldName = "xxo_magaz2"
    Me.xxo_magaz2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_magaz2.Name = "xxo_magaz2"
    Me.xxo_magaz2.NTSRepositoryComboBox = Nothing
    Me.xxo_magaz2.NTSRepositoryItemCheck = Nothing
    Me.xxo_magaz2.NTSRepositoryItemMemo = Nothing
    Me.xxo_magaz2.NTSRepositoryItemText = Nothing
    Me.xxo_magaz2.OptionsColumn.AllowEdit = False
    Me.xxo_magaz2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_magaz2.OptionsColumn.ReadOnly = True
    Me.xxo_magaz2.OptionsFilter.AllowFilter = False
    Me.xxo_magaz2.Visible = True
    Me.xxo_magaz2.VisibleIndex = 23
    '
    'ec_magimp
    '
    Me.ec_magimp.AppearanceCell.Options.UseBackColor = True
    Me.ec_magimp.AppearanceCell.Options.UseTextOptions = True
    Me.ec_magimp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_magimp.Caption = "Magaz imp"
    Me.ec_magimp.Enabled = True
    Me.ec_magimp.FieldName = "ec_magimp"
    Me.ec_magimp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_magimp.Name = "ec_magimp"
    Me.ec_magimp.NTSRepositoryComboBox = Nothing
    Me.ec_magimp.NTSRepositoryItemCheck = Nothing
    Me.ec_magimp.NTSRepositoryItemMemo = Nothing
    Me.ec_magimp.NTSRepositoryItemText = Nothing
    Me.ec_magimp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_magimp.OptionsFilter.AllowFilter = False
    Me.ec_magimp.Visible = True
    Me.ec_magimp.VisibleIndex = 24
    '
    'xxo_magimp
    '
    Me.xxo_magimp.AppearanceCell.Options.UseBackColor = True
    Me.xxo_magimp.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_magimp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_magimp.Caption = "Descr. magaz. imp."
    Me.xxo_magimp.Enabled = False
    Me.xxo_magimp.FieldName = "xxo_magimp"
    Me.xxo_magimp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_magimp.Name = "xxo_magimp"
    Me.xxo_magimp.NTSRepositoryComboBox = Nothing
    Me.xxo_magimp.NTSRepositoryItemCheck = Nothing
    Me.xxo_magimp.NTSRepositoryItemMemo = Nothing
    Me.xxo_magimp.NTSRepositoryItemText = Nothing
    Me.xxo_magimp.OptionsColumn.AllowEdit = False
    Me.xxo_magimp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_magimp.OptionsColumn.ReadOnly = True
    Me.xxo_magimp.OptionsFilter.AllowFilter = False
    Me.xxo_magimp.Visible = True
    Me.xxo_magimp.VisibleIndex = 25
    '
    'ec_datord
    '
    Me.ec_datord.AppearanceCell.Options.UseBackColor = True
    Me.ec_datord.AppearanceCell.Options.UseTextOptions = True
    Me.ec_datord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_datord.Caption = "Data mass. ord"
    Me.ec_datord.Enabled = True
    Me.ec_datord.FieldName = "ec_datord"
    Me.ec_datord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_datord.Name = "ec_datord"
    Me.ec_datord.NTSRepositoryComboBox = Nothing
    Me.ec_datord.NTSRepositoryItemCheck = Nothing
    Me.ec_datord.NTSRepositoryItemMemo = Nothing
    Me.ec_datord.NTSRepositoryItemText = Nothing
    Me.ec_datord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_datord.OptionsFilter.AllowFilter = False
    Me.ec_datord.Visible = True
    Me.ec_datord.VisibleIndex = 26
    '
    'ec_controp
    '
    Me.ec_controp.AppearanceCell.Options.UseBackColor = True
    Me.ec_controp.AppearanceCell.Options.UseTextOptions = True
    Me.ec_controp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_controp.Caption = "Controp."
    Me.ec_controp.Enabled = True
    Me.ec_controp.FieldName = "ec_controp"
    Me.ec_controp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_controp.Name = "ec_controp"
    Me.ec_controp.NTSRepositoryComboBox = Nothing
    Me.ec_controp.NTSRepositoryItemCheck = Nothing
    Me.ec_controp.NTSRepositoryItemMemo = Nothing
    Me.ec_controp.NTSRepositoryItemText = Nothing
    Me.ec_controp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_controp.OptionsFilter.AllowFilter = False
    Me.ec_controp.Visible = True
    Me.ec_controp.VisibleIndex = 27
    '
    'xxo_controp
    '
    Me.xxo_controp.AppearanceCell.Options.UseBackColor = True
    Me.xxo_controp.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_controp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_controp.Caption = "Descr. controp."
    Me.xxo_controp.Enabled = False
    Me.xxo_controp.FieldName = "xxo_controp"
    Me.xxo_controp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_controp.Name = "xxo_controp"
    Me.xxo_controp.NTSRepositoryComboBox = Nothing
    Me.xxo_controp.NTSRepositoryItemCheck = Nothing
    Me.xxo_controp.NTSRepositoryItemMemo = Nothing
    Me.xxo_controp.NTSRepositoryItemText = Nothing
    Me.xxo_controp.OptionsColumn.AllowEdit = False
    Me.xxo_controp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_controp.OptionsColumn.ReadOnly = True
    Me.xxo_controp.OptionsFilter.AllowFilter = False
    Me.xxo_controp.Visible = True
    Me.xxo_controp.VisibleIndex = 28
    '
    'ec_codiva
    '
    Me.ec_codiva.AppearanceCell.Options.UseBackColor = True
    Me.ec_codiva.AppearanceCell.Options.UseTextOptions = True
    Me.ec_codiva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_codiva.Caption = "Cod. IVA"
    Me.ec_codiva.Enabled = True
    Me.ec_codiva.FieldName = "ec_codiva"
    Me.ec_codiva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_codiva.Name = "ec_codiva"
    Me.ec_codiva.NTSRepositoryComboBox = Nothing
    Me.ec_codiva.NTSRepositoryItemCheck = Nothing
    Me.ec_codiva.NTSRepositoryItemMemo = Nothing
    Me.ec_codiva.NTSRepositoryItemText = Nothing
    Me.ec_codiva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_codiva.OptionsFilter.AllowFilter = False
    Me.ec_codiva.Visible = True
    Me.ec_codiva.VisibleIndex = 29
    '
    'xxo_codiva
    '
    Me.xxo_codiva.AppearanceCell.Options.UseBackColor = True
    Me.xxo_codiva.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_codiva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_codiva.Caption = "Descr. IVA"
    Me.xxo_codiva.Enabled = False
    Me.xxo_codiva.FieldName = "xxo_codiva"
    Me.xxo_codiva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_codiva.Name = "xxo_codiva"
    Me.xxo_codiva.NTSRepositoryComboBox = Nothing
    Me.xxo_codiva.NTSRepositoryItemCheck = Nothing
    Me.xxo_codiva.NTSRepositoryItemMemo = Nothing
    Me.xxo_codiva.NTSRepositoryItemText = Nothing
    Me.xxo_codiva.OptionsColumn.AllowEdit = False
    Me.xxo_codiva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_codiva.OptionsColumn.ReadOnly = True
    Me.xxo_codiva.OptionsFilter.AllowFilter = False
    Me.xxo_codiva.Visible = True
    Me.xxo_codiva.VisibleIndex = 30
    '
    'ec_codcfam
    '
    Me.ec_codcfam.AppearanceCell.Options.UseBackColor = True
    Me.ec_codcfam.AppearanceCell.Options.UseTextOptions = True
    Me.ec_codcfam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_codcfam.Caption = "Linea/Fam."
    Me.ec_codcfam.Enabled = True
    Me.ec_codcfam.FieldName = "ec_codcfam"
    Me.ec_codcfam.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_codcfam.Name = "ec_codcfam"
    Me.ec_codcfam.NTSRepositoryComboBox = Nothing
    Me.ec_codcfam.NTSRepositoryItemCheck = Nothing
    Me.ec_codcfam.NTSRepositoryItemMemo = Nothing
    Me.ec_codcfam.NTSRepositoryItemText = Nothing
    Me.ec_codcfam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_codcfam.OptionsFilter.AllowFilter = False
    Me.ec_codcfam.Visible = True
    Me.ec_codcfam.VisibleIndex = 31
    '
    'xxo_codcfam
    '
    Me.xxo_codcfam.AppearanceCell.Options.UseBackColor = True
    Me.xxo_codcfam.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_codcfam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_codcfam.Caption = "Descr. linea/fam"
    Me.xxo_codcfam.Enabled = False
    Me.xxo_codcfam.FieldName = "xxo_codcfam"
    Me.xxo_codcfam.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_codcfam.Name = "xxo_codcfam"
    Me.xxo_codcfam.NTSRepositoryComboBox = Nothing
    Me.xxo_codcfam.NTSRepositoryItemCheck = Nothing
    Me.xxo_codcfam.NTSRepositoryItemMemo = Nothing
    Me.xxo_codcfam.NTSRepositoryItemText = Nothing
    Me.xxo_codcfam.OptionsColumn.AllowEdit = False
    Me.xxo_codcfam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_codcfam.OptionsColumn.ReadOnly = True
    Me.xxo_codcfam.OptionsFilter.AllowFilter = False
    Me.xxo_codcfam.Visible = True
    Me.xxo_codcfam.VisibleIndex = 32
    '
    'ec_commeca
    '
    Me.ec_commeca.AppearanceCell.Options.UseBackColor = True
    Me.ec_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.ec_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_commeca.Caption = "Comm. C.A."
    Me.ec_commeca.Enabled = True
    Me.ec_commeca.FieldName = "ec_commeca"
    Me.ec_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_commeca.Name = "ec_commeca"
    Me.ec_commeca.NTSRepositoryComboBox = Nothing
    Me.ec_commeca.NTSRepositoryItemCheck = Nothing
    Me.ec_commeca.NTSRepositoryItemMemo = Nothing
    Me.ec_commeca.NTSRepositoryItemText = Nothing
    Me.ec_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_commeca.OptionsFilter.AllowFilter = False
    Me.ec_commeca.Visible = True
    Me.ec_commeca.VisibleIndex = 33
    '
    'xxo_commeca
    '
    Me.xxo_commeca.AppearanceCell.Options.UseBackColor = True
    Me.xxo_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_commeca.Caption = "Descr. commessa"
    Me.xxo_commeca.Enabled = False
    Me.xxo_commeca.FieldName = "xxo_commeca"
    Me.xxo_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_commeca.Name = "xxo_commeca"
    Me.xxo_commeca.NTSRepositoryComboBox = Nothing
    Me.xxo_commeca.NTSRepositoryItemCheck = Nothing
    Me.xxo_commeca.NTSRepositoryItemMemo = Nothing
    Me.xxo_commeca.NTSRepositoryItemText = Nothing
    Me.xxo_commeca.OptionsColumn.AllowEdit = False
    Me.xxo_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_commeca.OptionsColumn.ReadOnly = True
    Me.xxo_commeca.OptionsFilter.AllowFilter = False
    Me.xxo_commeca.Visible = True
    Me.xxo_commeca.VisibleIndex = 34
    '
    'ec_subcommeca
    '
    Me.ec_subcommeca.AppearanceCell.Options.UseBackColor = True
    Me.ec_subcommeca.AppearanceCell.Options.UseTextOptions = True
    Me.ec_subcommeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_subcommeca.Caption = "Sub C."
    Me.ec_subcommeca.Enabled = True
    Me.ec_subcommeca.FieldName = "ec_subcommeca"
    Me.ec_subcommeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_subcommeca.Name = "ec_subcommeca"
    Me.ec_subcommeca.NTSRepositoryComboBox = Nothing
    Me.ec_subcommeca.NTSRepositoryItemCheck = Nothing
    Me.ec_subcommeca.NTSRepositoryItemMemo = Nothing
    Me.ec_subcommeca.NTSRepositoryItemText = Nothing
    Me.ec_subcommeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_subcommeca.OptionsFilter.AllowFilter = False
    Me.ec_subcommeca.Visible = True
    Me.ec_subcommeca.VisibleIndex = 35
    '
    'ec_codcena
    '
    Me.ec_codcena.AppearanceCell.Options.UseBackColor = True
    Me.ec_codcena.AppearanceCell.Options.UseTextOptions = True
    Me.ec_codcena.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_codcena.Caption = "Centro C.A."
    Me.ec_codcena.Enabled = True
    Me.ec_codcena.FieldName = "ec_codcena"
    Me.ec_codcena.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_codcena.Name = "ec_codcena"
    Me.ec_codcena.NTSRepositoryComboBox = Nothing
    Me.ec_codcena.NTSRepositoryItemCheck = Nothing
    Me.ec_codcena.NTSRepositoryItemMemo = Nothing
    Me.ec_codcena.NTSRepositoryItemText = Nothing
    Me.ec_codcena.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_codcena.OptionsFilter.AllowFilter = False
    Me.ec_codcena.Visible = True
    Me.ec_codcena.VisibleIndex = 36
    '
    'xxo_codcena
    '
    Me.xxo_codcena.AppearanceCell.Options.UseBackColor = True
    Me.xxo_codcena.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_codcena.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_codcena.Caption = "Descr. centro"
    Me.xxo_codcena.Enabled = False
    Me.xxo_codcena.FieldName = "xxo_codcena"
    Me.xxo_codcena.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_codcena.Name = "xxo_codcena"
    Me.xxo_codcena.NTSRepositoryComboBox = Nothing
    Me.xxo_codcena.NTSRepositoryItemCheck = Nothing
    Me.xxo_codcena.NTSRepositoryItemMemo = Nothing
    Me.xxo_codcena.NTSRepositoryItemText = Nothing
    Me.xxo_codcena.OptionsColumn.AllowEdit = False
    Me.xxo_codcena.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_codcena.OptionsColumn.ReadOnly = True
    Me.xxo_codcena.OptionsFilter.AllowFilter = False
    Me.xxo_codcena.Visible = True
    Me.xxo_codcena.VisibleIndex = 37
    '
    'xxo_lottox
    '
    Me.xxo_lottox.AppearanceCell.Options.UseBackColor = True
    Me.xxo_lottox.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_lottox.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_lottox.Caption = "Lotto"
    Me.xxo_lottox.Enabled = True
    Me.xxo_lottox.FieldName = "xxo_lottox"
    Me.xxo_lottox.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_lottox.Name = "xxo_lottox"
    Me.xxo_lottox.NTSRepositoryComboBox = Nothing
    Me.xxo_lottox.NTSRepositoryItemCheck = Nothing
    Me.xxo_lottox.NTSRepositoryItemMemo = Nothing
    Me.xxo_lottox.NTSRepositoryItemText = Nothing
    Me.xxo_lottox.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_lottox.OptionsFilter.AllowFilter = False
    Me.xxo_lottox.Visible = True
    Me.xxo_lottox.VisibleIndex = 38
    '
    'ec_note
    '
    Me.ec_note.AppearanceCell.Options.UseBackColor = True
    Me.ec_note.AppearanceCell.Options.UseTextOptions = True
    Me.ec_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_note.Caption = "Note"
    Me.ec_note.Enabled = True
    Me.ec_note.FieldName = "ec_note"
    Me.ec_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_note.Name = "ec_note"
    Me.ec_note.NTSRepositoryComboBox = Nothing
    Me.ec_note.NTSRepositoryItemCheck = Nothing
    Me.ec_note.NTSRepositoryItemMemo = Nothing
    Me.ec_note.NTSRepositoryItemText = Nothing
    Me.ec_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_note.OptionsFilter.AllowFilter = False
    Me.ec_note.Visible = True
    Me.ec_note.VisibleIndex = 39
    '
    'ec_valore
    '
    Me.ec_valore.AppearanceCell.Options.UseBackColor = True
    Me.ec_valore.AppearanceCell.Options.UseTextOptions = True
    Me.ec_valore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_valore.Caption = "Valore"
    Me.ec_valore.Enabled = False
    Me.ec_valore.FieldName = "ec_valore"
    Me.ec_valore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_valore.Name = "ec_valore"
    Me.ec_valore.NTSRepositoryComboBox = Nothing
    Me.ec_valore.NTSRepositoryItemCheck = Nothing
    Me.ec_valore.NTSRepositoryItemMemo = Nothing
    Me.ec_valore.NTSRepositoryItemText = Nothing
    Me.ec_valore.OptionsColumn.AllowEdit = False
    Me.ec_valore.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_valore.OptionsColumn.ReadOnly = True
    Me.ec_valore.OptionsFilter.AllowFilter = False
    Me.ec_valore.Visible = True
    Me.ec_valore.VisibleIndex = 40
    '
    'ec_contocontr
    '
    Me.ec_contocontr.AppearanceCell.Options.UseBackColor = True
    Me.ec_contocontr.AppearanceCell.Options.UseTextOptions = True
    Me.ec_contocontr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_contocontr.Caption = "Conto controp."
    Me.ec_contocontr.Enabled = False
    Me.ec_contocontr.FieldName = "ec_contocontr"
    Me.ec_contocontr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_contocontr.Name = "ec_contocontr"
    Me.ec_contocontr.NTSRepositoryComboBox = Nothing
    Me.ec_contocontr.NTSRepositoryItemCheck = Nothing
    Me.ec_contocontr.NTSRepositoryItemMemo = Nothing
    Me.ec_contocontr.NTSRepositoryItemText = Nothing
    Me.ec_contocontr.OptionsColumn.AllowEdit = False
    Me.ec_contocontr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_contocontr.OptionsColumn.ReadOnly = True
    Me.ec_contocontr.OptionsFilter.AllowFilter = False
    '
    'xxo_contocon
    '
    Me.xxo_contocon.AppearanceCell.Options.UseBackColor = True
    Me.xxo_contocon.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_contocon.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_contocon.Caption = "Descr. conto controp."
    Me.xxo_contocon.Enabled = False
    Me.xxo_contocon.FieldName = "xxo_contocon"
    Me.xxo_contocon.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_contocon.Name = "xxo_contocon"
    Me.xxo_contocon.NTSRepositoryComboBox = Nothing
    Me.xxo_contocon.NTSRepositoryItemCheck = Nothing
    Me.xxo_contocon.NTSRepositoryItemMemo = Nothing
    Me.xxo_contocon.NTSRepositoryItemText = Nothing
    Me.xxo_contocon.OptionsColumn.AllowEdit = False
    Me.xxo_contocon.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_contocon.OptionsColumn.ReadOnly = True
    Me.xxo_contocon.OptionsFilter.AllowFilter = False
    '
    'ec_codclie
    '
    Me.ec_codclie.AppearanceCell.Options.UseBackColor = True
    Me.ec_codclie.AppearanceCell.Options.UseTextOptions = True
    Me.ec_codclie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_codclie.Caption = "Cod. cliente"
    Me.ec_codclie.Enabled = True
    Me.ec_codclie.FieldName = "ec_codclie"
    Me.ec_codclie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_codclie.Name = "ec_codclie"
    Me.ec_codclie.NTSRepositoryComboBox = Nothing
    Me.ec_codclie.NTSRepositoryItemCheck = Nothing
    Me.ec_codclie.NTSRepositoryItemMemo = Nothing
    Me.ec_codclie.NTSRepositoryItemText = Nothing
    Me.ec_codclie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_codclie.OptionsFilter.AllowFilter = False
    '
    'xxo_codclie
    '
    Me.xxo_codclie.AppearanceCell.Options.UseBackColor = True
    Me.xxo_codclie.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_codclie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_codclie.Caption = "Descr. cliente"
    Me.xxo_codclie.Enabled = False
    Me.xxo_codclie.FieldName = "xxo_codclie"
    Me.xxo_codclie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_codclie.Name = "xxo_codclie"
    Me.xxo_codclie.NTSRepositoryComboBox = Nothing
    Me.xxo_codclie.NTSRepositoryItemCheck = Nothing
    Me.xxo_codclie.NTSRepositoryItemMemo = Nothing
    Me.xxo_codclie.NTSRepositoryItemText = Nothing
    Me.xxo_codclie.OptionsColumn.AllowEdit = False
    Me.xxo_codclie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_codclie.OptionsColumn.ReadOnly = True
    Me.xxo_codclie.OptionsFilter.AllowFilter = False
    '
    'ec_misura1
    '
    Me.ec_misura1.AppearanceCell.Options.UseBackColor = True
    Me.ec_misura1.AppearanceCell.Options.UseTextOptions = True
    Me.ec_misura1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_misura1.Caption = "Misura 1"
    Me.ec_misura1.Enabled = True
    Me.ec_misura1.FieldName = "ec_misura1"
    Me.ec_misura1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_misura1.Name = "ec_misura1"
    Me.ec_misura1.NTSRepositoryComboBox = Nothing
    Me.ec_misura1.NTSRepositoryItemCheck = Nothing
    Me.ec_misura1.NTSRepositoryItemMemo = Nothing
    Me.ec_misura1.NTSRepositoryItemText = Nothing
    Me.ec_misura1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_misura1.OptionsFilter.AllowFilter = False
    '
    'ec_misura2
    '
    Me.ec_misura2.AppearanceCell.Options.UseBackColor = True
    Me.ec_misura2.AppearanceCell.Options.UseTextOptions = True
    Me.ec_misura2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_misura2.Caption = "Misura 2"
    Me.ec_misura2.Enabled = True
    Me.ec_misura2.FieldName = "ec_misura2"
    Me.ec_misura2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_misura2.Name = "ec_misura2"
    Me.ec_misura2.NTSRepositoryComboBox = Nothing
    Me.ec_misura2.NTSRepositoryItemCheck = Nothing
    Me.ec_misura2.NTSRepositoryItemMemo = Nothing
    Me.ec_misura2.NTSRepositoryItemText = Nothing
    Me.ec_misura2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_misura2.OptionsFilter.AllowFilter = False
    '
    'ec_misura3
    '
    Me.ec_misura3.AppearanceCell.Options.UseBackColor = True
    Me.ec_misura3.AppearanceCell.Options.UseTextOptions = True
    Me.ec_misura3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_misura3.Caption = "Misura 3"
    Me.ec_misura3.Enabled = True
    Me.ec_misura3.FieldName = "ec_misura3"
    Me.ec_misura3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_misura3.Name = "ec_misura3"
    Me.ec_misura3.NTSRepositoryComboBox = Nothing
    Me.ec_misura3.NTSRepositoryItemCheck = Nothing
    Me.ec_misura3.NTSRepositoryItemMemo = Nothing
    Me.ec_misura3.NTSRepositoryItemText = Nothing
    Me.ec_misura3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_misura3.OptionsFilter.AllowFilter = False
    '
    'ec_perqta
    '
    Me.ec_perqta.AppearanceCell.Options.UseBackColor = True
    Me.ec_perqta.AppearanceCell.Options.UseTextOptions = True
    Me.ec_perqta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_perqta.Caption = "Prz/Qta"
    Me.ec_perqta.Enabled = False
    Me.ec_perqta.FieldName = "ec_perqta"
    Me.ec_perqta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_perqta.Name = "ec_perqta"
    Me.ec_perqta.NTSRepositoryComboBox = Nothing
    Me.ec_perqta.NTSRepositoryItemCheck = Nothing
    Me.ec_perqta.NTSRepositoryItemMemo = Nothing
    Me.ec_perqta.NTSRepositoryItemText = Nothing
    Me.ec_perqta.OptionsColumn.AllowEdit = False
    Me.ec_perqta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_perqta.OptionsColumn.ReadOnly = True
    Me.ec_perqta.OptionsFilter.AllowFilter = False
    '
    'ec_flcom
    '
    Me.ec_flcom.AppearanceCell.Options.UseBackColor = True
    Me.ec_flcom.AppearanceCell.Options.UseTextOptions = True
    Me.ec_flcom.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_flcom.Caption = "Da controllare"
    Me.ec_flcom.Enabled = True
    Me.ec_flcom.FieldName = "ec_flcom"
    Me.ec_flcom.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_flcom.Name = "ec_flcom"
    Me.ec_flcom.NTSRepositoryComboBox = Nothing
    Me.ec_flcom.NTSRepositoryItemCheck = Nothing
    Me.ec_flcom.NTSRepositoryItemMemo = Nothing
    Me.ec_flcom.NTSRepositoryItemText = Nothing
    Me.ec_flcom.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_flcom.OptionsFilter.AllowFilter = False
    '
    'ec_flprznet
    '
    Me.ec_flprznet.AppearanceCell.Options.UseBackColor = True
    Me.ec_flprznet.AppearanceCell.Options.UseTextOptions = True
    Me.ec_flprznet.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_flprznet.Caption = "Prezzo netto"
    Me.ec_flprznet.Enabled = True
    Me.ec_flprznet.FieldName = "ec_flprznet"
    Me.ec_flprznet.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_flprznet.Name = "ec_flprznet"
    Me.ec_flprznet.NTSRepositoryComboBox = Nothing
    Me.ec_flprznet.NTSRepositoryItemCheck = Nothing
    Me.ec_flprznet.NTSRepositoryItemMemo = Nothing
    Me.ec_flprznet.NTSRepositoryItemText = Nothing
    Me.ec_flprznet.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_flprznet.OptionsFilter.AllowFilter = False
    '
    'ec_flforf
    '
    Me.ec_flforf.AppearanceCell.Options.UseBackColor = True
    Me.ec_flforf.AppearanceCell.Options.UseTextOptions = True
    Me.ec_flforf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_flforf.Caption = "A forfait"
    Me.ec_flforf.Enabled = True
    Me.ec_flforf.FieldName = "ec_flforf"
    Me.ec_flforf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_flforf.Name = "ec_flforf"
    Me.ec_flforf.NTSRepositoryComboBox = Nothing
    Me.ec_flforf.NTSRepositoryItemCheck = Nothing
    Me.ec_flforf.NTSRepositoryItemMemo = Nothing
    Me.ec_flforf.NTSRepositoryItemText = Nothing
    Me.ec_flforf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_flforf.OptionsFilter.AllowFilter = False
    '
    'ec_matric
    '
    Me.ec_matric.AppearanceCell.Options.UseBackColor = True
    Me.ec_matric.AppearanceCell.Options.UseTextOptions = True
    Me.ec_matric.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_matric.Caption = "Matricola"
    Me.ec_matric.Enabled = True
    Me.ec_matric.FieldName = "ec_matric"
    Me.ec_matric.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_matric.Name = "ec_matric"
    Me.ec_matric.NTSRepositoryComboBox = Nothing
    Me.ec_matric.NTSRepositoryItemCheck = Nothing
    Me.ec_matric.NTSRepositoryItemMemo = Nothing
    Me.ec_matric.NTSRepositoryItemText = Nothing
    Me.ec_matric.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_matric.OptionsFilter.AllowFilter = False
    '
    'ec_umprz
    '
    Me.ec_umprz.AppearanceCell.Options.UseBackColor = True
    Me.ec_umprz.AppearanceCell.Options.UseTextOptions = True
    Me.ec_umprz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_umprz.Caption = "Prezzi x u.d.m."
    Me.ec_umprz.Enabled = False
    Me.ec_umprz.FieldName = "ec_umprz"
    Me.ec_umprz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_umprz.Name = "ec_umprz"
    Me.ec_umprz.NTSRepositoryComboBox = Nothing
    Me.ec_umprz.NTSRepositoryItemCheck = Nothing
    Me.ec_umprz.NTSRepositoryItemMemo = Nothing
    Me.ec_umprz.NTSRepositoryItemText = Nothing
    Me.ec_umprz.OptionsColumn.AllowEdit = False
    Me.ec_umprz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_umprz.OptionsColumn.ReadOnly = True
    Me.ec_umprz.OptionsFilter.AllowFilter = False
    '
    'ec_fase
    '
    Me.ec_fase.AppearanceCell.Options.UseBackColor = True
    Me.ec_fase.AppearanceCell.Options.UseTextOptions = True
    Me.ec_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_fase.Caption = "Fase"
    Me.ec_fase.Enabled = True
    Me.ec_fase.FieldName = "ec_fase"
    Me.ec_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_fase.Name = "ec_fase"
    Me.ec_fase.NTSRepositoryComboBox = Nothing
    Me.ec_fase.NTSRepositoryItemCheck = Nothing
    Me.ec_fase.NTSRepositoryItemMemo = Nothing
    Me.ec_fase.NTSRepositoryItemText = Nothing
    Me.ec_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_fase.OptionsFilter.AllowFilter = False
    '
    'xxo_fase
    '
    Me.xxo_fase.AppearanceCell.Options.UseBackColor = True
    Me.xxo_fase.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_fase.Caption = "Descr. fase"
    Me.xxo_fase.Enabled = False
    Me.xxo_fase.FieldName = "xxo_fase"
    Me.xxo_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_fase.Name = "xxo_fase"
    Me.xxo_fase.NTSRepositoryComboBox = Nothing
    Me.xxo_fase.NTSRepositoryItemCheck = Nothing
    Me.xxo_fase.NTSRepositoryItemMemo = Nothing
    Me.xxo_fase.NTSRepositoryItemText = Nothing
    Me.xxo_fase.OptionsColumn.AllowEdit = False
    Me.xxo_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_fase.OptionsColumn.ReadOnly = True
    Me.xxo_fase.OptionsFilter.AllowFilter = False
    '
    'ec_codlavo
    '
    Me.ec_codlavo.AppearanceCell.Options.UseBackColor = True
    Me.ec_codlavo.AppearanceCell.Options.UseTextOptions = True
    Me.ec_codlavo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_codlavo.Caption = "Codice lavoraz."
    Me.ec_codlavo.Enabled = False
    Me.ec_codlavo.FieldName = "ec_codlavo"
    Me.ec_codlavo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_codlavo.Name = "ec_codlavo"
    Me.ec_codlavo.NTSRepositoryComboBox = Nothing
    Me.ec_codlavo.NTSRepositoryItemCheck = Nothing
    Me.ec_codlavo.NTSRepositoryItemMemo = Nothing
    Me.ec_codlavo.NTSRepositoryItemText = Nothing
    Me.ec_codlavo.OptionsColumn.AllowEdit = False
    Me.ec_codlavo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_codlavo.OptionsColumn.ReadOnly = True
    Me.ec_codlavo.OptionsFilter.AllowFilter = False
    '
    'xxo_codlavo
    '
    Me.xxo_codlavo.AppearanceCell.Options.UseBackColor = True
    Me.xxo_codlavo.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_codlavo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_codlavo.Caption = "Descr. lavoraz."
    Me.xxo_codlavo.Enabled = False
    Me.xxo_codlavo.FieldName = "xxo_codlavo"
    Me.xxo_codlavo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_codlavo.Name = "xxo_codlavo"
    Me.xxo_codlavo.NTSRepositoryComboBox = Nothing
    Me.xxo_codlavo.NTSRepositoryItemCheck = Nothing
    Me.xxo_codlavo.NTSRepositoryItemMemo = Nothing
    Me.xxo_codlavo.NTSRepositoryItemText = Nothing
    Me.xxo_codlavo.OptionsColumn.AllowEdit = False
    Me.xxo_codlavo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_codlavo.OptionsColumn.ReadOnly = True
    Me.xxo_codlavo.OptionsFilter.AllowFilter = False
    '
    'ec_datini
    '
    Me.ec_datini.AppearanceCell.Options.UseBackColor = True
    Me.ec_datini.AppearanceCell.Options.UseTextOptions = True
    Me.ec_datini.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_datini.Caption = "Dt iniz. comp. econ."
    Me.ec_datini.Enabled = True
    Me.ec_datini.FieldName = "ec_datini"
    Me.ec_datini.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_datini.Name = "ec_datini"
    Me.ec_datini.NTSRepositoryComboBox = Nothing
    Me.ec_datini.NTSRepositoryItemCheck = Nothing
    Me.ec_datini.NTSRepositoryItemMemo = Nothing
    Me.ec_datini.NTSRepositoryItemText = Nothing
    Me.ec_datini.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_datini.OptionsFilter.AllowFilter = False
    '
    'ec_datfin
    '
    Me.ec_datfin.AppearanceCell.Options.UseBackColor = True
    Me.ec_datfin.AppearanceCell.Options.UseTextOptions = True
    Me.ec_datfin.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_datfin.Caption = "Dt fin. comp. econ."
    Me.ec_datfin.Enabled = True
    Me.ec_datfin.FieldName = "ec_datfin"
    Me.ec_datfin.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_datfin.Name = "ec_datfin"
    Me.ec_datfin.NTSRepositoryComboBox = Nothing
    Me.ec_datfin.NTSRepositoryItemCheck = Nothing
    Me.ec_datfin.NTSRepositoryItemMemo = Nothing
    Me.ec_datfin.NTSRepositoryItemText = Nothing
    Me.ec_datfin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_datfin.OptionsFilter.AllowFilter = False
    '
    'ec_valorev
    '
    Me.ec_valorev.AppearanceCell.Options.UseBackColor = True
    Me.ec_valorev.AppearanceCell.Options.UseTextOptions = True
    Me.ec_valorev.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_valorev.Caption = "Valore in valuta"
    Me.ec_valorev.Enabled = False
    Me.ec_valorev.FieldName = "ec_valorev"
    Me.ec_valorev.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_valorev.Name = "ec_valorev"
    Me.ec_valorev.NTSRepositoryComboBox = Nothing
    Me.ec_valorev.NTSRepositoryItemCheck = Nothing
    Me.ec_valorev.NTSRepositoryItemMemo = Nothing
    Me.ec_valorev.NTSRepositoryItemText = Nothing
    Me.ec_valorev.OptionsColumn.AllowEdit = False
    Me.ec_valorev.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_valorev.OptionsColumn.ReadOnly = True
    Me.ec_valorev.OptionsFilter.AllowFilter = False
    '
    'ec_pmtaskid
    '
    Me.ec_pmtaskid.AppearanceCell.Options.UseBackColor = True
    Me.ec_pmtaskid.AppearanceCell.Options.UseTextOptions = True
    Me.ec_pmtaskid.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_pmtaskid.Caption = "ID Task"
    Me.ec_pmtaskid.Enabled = True
    Me.ec_pmtaskid.FieldName = "ec_pmtaskid"
    Me.ec_pmtaskid.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_pmtaskid.Name = "ec_pmtaskid"
    Me.ec_pmtaskid.NTSRepositoryComboBox = Nothing
    Me.ec_pmtaskid.NTSRepositoryItemCheck = Nothing
    Me.ec_pmtaskid.NTSRepositoryItemMemo = Nothing
    Me.ec_pmtaskid.NTSRepositoryItemText = Nothing
    Me.ec_pmtaskid.OptionsColumn.AllowEdit = False
    Me.ec_pmtaskid.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_pmtaskid.OptionsFilter.AllowFilter = False
    '
    'xxo_pmtaskid
    '
    Me.xxo_pmtaskid.AppearanceCell.Options.UseBackColor = True
    Me.xxo_pmtaskid.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_pmtaskid.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_pmtaskid.Caption = "Descr. Task"
    Me.xxo_pmtaskid.Enabled = False
    Me.xxo_pmtaskid.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_pmtaskid.Name = "xxo_pmtaskid"
    Me.xxo_pmtaskid.NTSRepositoryComboBox = Nothing
    Me.xxo_pmtaskid.NTSRepositoryItemCheck = Nothing
    Me.xxo_pmtaskid.NTSRepositoryItemMemo = Nothing
    Me.xxo_pmtaskid.NTSRepositoryItemText = Nothing
    Me.xxo_pmtaskid.OptionsColumn.AllowEdit = False
    Me.xxo_pmtaskid.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_pmtaskid.OptionsColumn.ReadOnly = True
    Me.xxo_pmtaskid.OptionsFilter.AllowFilter = False
    '
    'ec_pmsalcon
    '
    Me.ec_pmsalcon.AppearanceCell.Options.UseBackColor = True
    Me.ec_pmsalcon.AppearanceCell.Options.UseTextOptions = True
    Me.ec_pmsalcon.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_pmsalcon.Caption = "Sal. Task"
    Me.ec_pmsalcon.Enabled = False
    Me.ec_pmsalcon.FieldName = "ec_pmsalcon"
    Me.ec_pmsalcon.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_pmsalcon.Name = "ec_pmsalcon"
    Me.ec_pmsalcon.NTSRepositoryComboBox = Nothing
    Me.ec_pmsalcon.NTSRepositoryItemCheck = Nothing
    Me.ec_pmsalcon.NTSRepositoryItemMemo = Nothing
    Me.ec_pmsalcon.NTSRepositoryItemText = Nothing
    Me.ec_pmsalcon.OptionsColumn.AllowEdit = False
    Me.ec_pmsalcon.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_pmsalcon.OptionsColumn.ReadOnly = True
    Me.ec_pmsalcon.OptionsFilter.AllowFilter = False
    '
    'ec_pmqtadis
    '
    Me.ec_pmqtadis.AppearanceCell.Options.UseBackColor = True
    Me.ec_pmqtadis.AppearanceCell.Options.UseTextOptions = True
    Me.ec_pmqtadis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_pmqtadis.Caption = "PMQTADIS"
    Me.ec_pmqtadis.Enabled = False
    Me.ec_pmqtadis.FieldName = "ec_pmqtadis"
    Me.ec_pmqtadis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_pmqtadis.Name = "ec_pmqtadis"
    Me.ec_pmqtadis.NTSRepositoryComboBox = Nothing
    Me.ec_pmqtadis.NTSRepositoryItemCheck = Nothing
    Me.ec_pmqtadis.NTSRepositoryItemMemo = Nothing
    Me.ec_pmqtadis.NTSRepositoryItemText = Nothing
    Me.ec_pmqtadis.OptionsColumn.AllowEdit = False
    Me.ec_pmqtadis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_pmqtadis.OptionsColumn.ReadOnly = True
    Me.ec_pmqtadis.OptionsFilter.AllowFilter = False
    '
    'ec_pmvaldis
    '
    Me.ec_pmvaldis.AppearanceCell.Options.UseBackColor = True
    Me.ec_pmvaldis.AppearanceCell.Options.UseTextOptions = True
    Me.ec_pmvaldis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_pmvaldis.Caption = "PMVALDIS"
    Me.ec_pmvaldis.Enabled = False
    Me.ec_pmvaldis.FieldName = "ec_pmvaldis"
    Me.ec_pmvaldis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_pmvaldis.Name = "ec_pmvaldis"
    Me.ec_pmvaldis.NTSRepositoryComboBox = Nothing
    Me.ec_pmvaldis.NTSRepositoryItemCheck = Nothing
    Me.ec_pmvaldis.NTSRepositoryItemMemo = Nothing
    Me.ec_pmvaldis.NTSRepositoryItemText = Nothing
    Me.ec_pmvaldis.OptionsColumn.AllowEdit = False
    Me.ec_pmvaldis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_pmvaldis.OptionsColumn.ReadOnly = True
    Me.ec_pmvaldis.OptionsFilter.AllowFilter = False
    '
    'ec_rdatipork
    '
    Me.ec_rdatipork.AppearanceCell.Options.UseBackColor = True
    Me.ec_rdatipork.AppearanceCell.Options.UseTextOptions = True
    Me.ec_rdatipork.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_rdatipork.Caption = "Tipo RDA"
    Me.ec_rdatipork.Enabled = False
    Me.ec_rdatipork.FieldName = "ec_rdatipork"
    Me.ec_rdatipork.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_rdatipork.Name = "ec_rdatipork"
    Me.ec_rdatipork.NTSRepositoryComboBox = Nothing
    Me.ec_rdatipork.NTSRepositoryItemCheck = Nothing
    Me.ec_rdatipork.NTSRepositoryItemMemo = Nothing
    Me.ec_rdatipork.NTSRepositoryItemText = Nothing
    Me.ec_rdatipork.OptionsColumn.AllowEdit = False
    Me.ec_rdatipork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_rdatipork.OptionsColumn.ReadOnly = True
    Me.ec_rdatipork.OptionsFilter.AllowFilter = False
    '
    'ec_rdaanno
    '
    Me.ec_rdaanno.AppearanceCell.Options.UseBackColor = True
    Me.ec_rdaanno.AppearanceCell.Options.UseTextOptions = True
    Me.ec_rdaanno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_rdaanno.Caption = "Anno RDA"
    Me.ec_rdaanno.Enabled = False
    Me.ec_rdaanno.FieldName = "ec_rdaanno"
    Me.ec_rdaanno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_rdaanno.Name = "ec_rdaanno"
    Me.ec_rdaanno.NTSRepositoryComboBox = Nothing
    Me.ec_rdaanno.NTSRepositoryItemCheck = Nothing
    Me.ec_rdaanno.NTSRepositoryItemMemo = Nothing
    Me.ec_rdaanno.NTSRepositoryItemText = Nothing
    Me.ec_rdaanno.OptionsColumn.AllowEdit = False
    Me.ec_rdaanno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_rdaanno.OptionsColumn.ReadOnly = True
    Me.ec_rdaanno.OptionsFilter.AllowFilter = False
    '
    'ec_rdaserie
    '
    Me.ec_rdaserie.AppearanceCell.Options.UseBackColor = True
    Me.ec_rdaserie.AppearanceCell.Options.UseTextOptions = True
    Me.ec_rdaserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_rdaserie.Caption = "Serie RDA"
    Me.ec_rdaserie.Enabled = False
    Me.ec_rdaserie.FieldName = "ec_rdaserie"
    Me.ec_rdaserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_rdaserie.Name = "ec_rdaserie"
    Me.ec_rdaserie.NTSRepositoryComboBox = Nothing
    Me.ec_rdaserie.NTSRepositoryItemCheck = Nothing
    Me.ec_rdaserie.NTSRepositoryItemMemo = Nothing
    Me.ec_rdaserie.NTSRepositoryItemText = Nothing
    Me.ec_rdaserie.OptionsColumn.AllowEdit = False
    Me.ec_rdaserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_rdaserie.OptionsColumn.ReadOnly = True
    Me.ec_rdaserie.OptionsFilter.AllowFilter = False
    '
    'ec_rdanum
    '
    Me.ec_rdanum.AppearanceCell.Options.UseBackColor = True
    Me.ec_rdanum.AppearanceCell.Options.UseTextOptions = True
    Me.ec_rdanum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_rdanum.Caption = "Num. RDA"
    Me.ec_rdanum.Enabled = False
    Me.ec_rdanum.FieldName = "ec_rdanum"
    Me.ec_rdanum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_rdanum.Name = "ec_rdanum"
    Me.ec_rdanum.NTSRepositoryComboBox = Nothing
    Me.ec_rdanum.NTSRepositoryItemCheck = Nothing
    Me.ec_rdanum.NTSRepositoryItemMemo = Nothing
    Me.ec_rdanum.NTSRepositoryItemText = Nothing
    Me.ec_rdanum.OptionsColumn.AllowEdit = False
    Me.ec_rdanum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_rdanum.OptionsColumn.ReadOnly = True
    Me.ec_rdanum.OptionsFilter.AllowFilter = False
    '
    'ec_rdariga
    '
    Me.ec_rdariga.AppearanceCell.Options.UseBackColor = True
    Me.ec_rdariga.AppearanceCell.Options.UseTextOptions = True
    Me.ec_rdariga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_rdariga.Caption = "Riga RDA"
    Me.ec_rdariga.Enabled = False
    Me.ec_rdariga.FieldName = "ec_rdariga"
    Me.ec_rdariga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_rdariga.Name = "ec_rdariga"
    Me.ec_rdariga.NTSRepositoryComboBox = Nothing
    Me.ec_rdariga.NTSRepositoryItemCheck = Nothing
    Me.ec_rdariga.NTSRepositoryItemMemo = Nothing
    Me.ec_rdariga.NTSRepositoryItemText = Nothing
    Me.ec_rdariga.OptionsColumn.AllowEdit = False
    Me.ec_rdariga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_rdariga.OptionsColumn.ReadOnly = True
    Me.ec_rdariga.OptionsFilter.AllowFilter = False
    '
    'ec_offreq
    '
    Me.ec_offreq.AppearanceCell.Options.UseBackColor = True
    Me.ec_offreq.AppearanceCell.Options.UseTextOptions = True
    Me.ec_offreq.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_offreq.Caption = "OFFREQ"
    Me.ec_offreq.Enabled = False
    Me.ec_offreq.FieldName = "ec_offreq"
    Me.ec_offreq.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_offreq.Name = "ec_offreq"
    Me.ec_offreq.NTSRepositoryComboBox = Nothing
    Me.ec_offreq.NTSRepositoryItemCheck = Nothing
    Me.ec_offreq.NTSRepositoryItemMemo = Nothing
    Me.ec_offreq.NTSRepositoryItemText = Nothing
    Me.ec_offreq.OptionsColumn.AllowEdit = False
    Me.ec_offreq.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_offreq.OptionsColumn.ReadOnly = True
    Me.ec_offreq.OptionsFilter.AllowFilter = False
    '
    'ec_ortipork
    '
    Me.ec_ortipork.AppearanceCell.Options.UseBackColor = True
    Me.ec_ortipork.AppearanceCell.Options.UseTextOptions = True
    Me.ec_ortipork.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_ortipork.Caption = "Tipo ord.gen."
    Me.ec_ortipork.Enabled = False
    Me.ec_ortipork.FieldName = "ec_ortipork"
    Me.ec_ortipork.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_ortipork.Name = "ec_ortipork"
    Me.ec_ortipork.NTSRepositoryComboBox = Nothing
    Me.ec_ortipork.NTSRepositoryItemCheck = Nothing
    Me.ec_ortipork.NTSRepositoryItemMemo = Nothing
    Me.ec_ortipork.NTSRepositoryItemText = Nothing
    Me.ec_ortipork.OptionsColumn.AllowEdit = False
    Me.ec_ortipork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_ortipork.OptionsColumn.ReadOnly = True
    Me.ec_ortipork.OptionsFilter.AllowFilter = False
    '
    'ec_oranno
    '
    Me.ec_oranno.AppearanceCell.Options.UseBackColor = True
    Me.ec_oranno.AppearanceCell.Options.UseTextOptions = True
    Me.ec_oranno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_oranno.Caption = "Anno ord.gen."
    Me.ec_oranno.Enabled = False
    Me.ec_oranno.FieldName = "ec_oranno"
    Me.ec_oranno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_oranno.Name = "ec_oranno"
    Me.ec_oranno.NTSRepositoryComboBox = Nothing
    Me.ec_oranno.NTSRepositoryItemCheck = Nothing
    Me.ec_oranno.NTSRepositoryItemMemo = Nothing
    Me.ec_oranno.NTSRepositoryItemText = Nothing
    Me.ec_oranno.OptionsColumn.AllowEdit = False
    Me.ec_oranno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_oranno.OptionsColumn.ReadOnly = True
    Me.ec_oranno.OptionsFilter.AllowFilter = False
    '
    'ec_orserie
    '
    Me.ec_orserie.AppearanceCell.Options.UseBackColor = True
    Me.ec_orserie.AppearanceCell.Options.UseTextOptions = True
    Me.ec_orserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_orserie.Caption = "Serie ord.gen."
    Me.ec_orserie.Enabled = False
    Me.ec_orserie.FieldName = "ec_orserie"
    Me.ec_orserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_orserie.Name = "ec_orserie"
    Me.ec_orserie.NTSRepositoryComboBox = Nothing
    Me.ec_orserie.NTSRepositoryItemCheck = Nothing
    Me.ec_orserie.NTSRepositoryItemMemo = Nothing
    Me.ec_orserie.NTSRepositoryItemText = Nothing
    Me.ec_orserie.OptionsColumn.AllowEdit = False
    Me.ec_orserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_orserie.OptionsColumn.ReadOnly = True
    Me.ec_orserie.OptionsFilter.AllowFilter = False
    '
    'ec_ornum
    '
    Me.ec_ornum.AppearanceCell.Options.UseBackColor = True
    Me.ec_ornum.AppearanceCell.Options.UseTextOptions = True
    Me.ec_ornum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_ornum.Caption = "N° ord.gen."
    Me.ec_ornum.Enabled = False
    Me.ec_ornum.FieldName = "ec_ornum"
    Me.ec_ornum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_ornum.Name = "ec_ornum"
    Me.ec_ornum.NTSRepositoryComboBox = Nothing
    Me.ec_ornum.NTSRepositoryItemCheck = Nothing
    Me.ec_ornum.NTSRepositoryItemMemo = Nothing
    Me.ec_ornum.NTSRepositoryItemText = Nothing
    Me.ec_ornum.OptionsColumn.AllowEdit = False
    Me.ec_ornum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_ornum.OptionsColumn.ReadOnly = True
    Me.ec_ornum.OptionsFilter.AllowFilter = False
    '
    'ec_orriga
    '
    Me.ec_orriga.AppearanceCell.Options.UseBackColor = True
    Me.ec_orriga.AppearanceCell.Options.UseTextOptions = True
    Me.ec_orriga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_orriga.Caption = "Riga ord.gen."
    Me.ec_orriga.Enabled = False
    Me.ec_orriga.FieldName = "ec_orriga"
    Me.ec_orriga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_orriga.Name = "ec_orriga"
    Me.ec_orriga.NTSRepositoryComboBox = Nothing
    Me.ec_orriga.NTSRepositoryItemCheck = Nothing
    Me.ec_orriga.NTSRepositoryItemMemo = Nothing
    Me.ec_orriga.NTSRepositoryItemText = Nothing
    Me.ec_orriga.OptionsColumn.AllowEdit = False
    Me.ec_orriga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_orriga.OptionsColumn.ReadOnly = True
    Me.ec_orriga.OptionsFilter.AllowFilter = False
    '
    'xxo_codtagl
    '
    Me.xxo_codtagl.AppearanceCell.Options.UseBackColor = True
    Me.xxo_codtagl.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_codtagl.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_codtagl.Enabled = False
    Me.xxo_codtagl.FieldName = "xxo_codtagl"
    Me.xxo_codtagl.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_codtagl.Name = "xxo_codtagl"
    Me.xxo_codtagl.NTSRepositoryComboBox = Nothing
    Me.xxo_codtagl.NTSRepositoryItemCheck = Nothing
    Me.xxo_codtagl.NTSRepositoryItemMemo = Nothing
    Me.xxo_codtagl.NTSRepositoryItemText = Nothing
    Me.xxo_codtagl.OptionsColumn.AllowEdit = False
    Me.xxo_codtagl.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_codtagl.OptionsColumn.ReadOnly = True
    Me.xxo_codtagl.OptionsFilter.AllowFilter = False
    '
    'xxo_livmindb
    '
    Me.xxo_livmindb.AppearanceCell.Options.UseBackColor = True
    Me.xxo_livmindb.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_livmindb.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_livmindb.Caption = "LLC Articolo"
    Me.xxo_livmindb.Enabled = False
    Me.xxo_livmindb.FieldName = "xxo_livmindb"
    Me.xxo_livmindb.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_livmindb.Name = "xxo_livmindb"
    Me.xxo_livmindb.NTSRepositoryComboBox = Nothing
    Me.xxo_livmindb.NTSRepositoryItemCheck = Nothing
    Me.xxo_livmindb.NTSRepositoryItemMemo = Nothing
    Me.xxo_livmindb.NTSRepositoryItemText = Nothing
    Me.xxo_livmindb.OptionsColumn.AllowEdit = False
    Me.xxo_livmindb.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_livmindb.OptionsColumn.ReadOnly = True
    Me.xxo_livmindb.OptionsFilter.AllowFilter = False
    Me.xxo_livmindb.Visible = True
    Me.xxo_livmindb.VisibleIndex = 41
    '
    'grTco
    '
    Me.grTco.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grTco.EmbeddedNavigator.Name = ""
    Me.grTco.Location = New System.Drawing.Point(0, 0)
    Me.grTco.MainView = Me.grvTco
    Me.grTco.Name = "grTco"
    Me.grTco.Size = New System.Drawing.Size(648, 63)
    Me.grTco.TabIndex = 2
    Me.grTco.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvTco})
    '
    'grvTco
    '
    Me.grvTco.ActiveFilterEnabled = False
    Me.grvTco.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ec_quant01, Me.ec_quant02, Me.ec_quant03, Me.ec_quant04, Me.ec_quant05, Me.ec_quant06, Me.ec_quant07, Me.ec_quant08, Me.ec_quant09, Me.ec_quant10, Me.ec_quant11, Me.ec_quant12, Me.ec_quant13, Me.ec_quant14, Me.ec_quant15, Me.ec_quant16, Me.ec_quant17, Me.ec_quant18, Me.ec_quant19, Me.ec_quant20, Me.ec_quant21, Me.ec_quant22, Me.ec_quant23, Me.ec_quant24})
    Me.grvTco.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvTco.Enabled = True
    Me.grvTco.GridControl = Me.grTco
    Me.grvTco.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvTco.MinRowHeight = 14
    Me.grvTco.Name = "grvTco"
    Me.grvTco.NTSAllowDelete = True
    Me.grvTco.NTSAllowInsert = True
    Me.grvTco.NTSAllowUpdate = True
    Me.grvTco.NTSMenuContext = Nothing
    Me.grvTco.OptionsCustomization.AllowRowSizing = True
    Me.grvTco.OptionsFilter.AllowFilterEditor = False
    Me.grvTco.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvTco.OptionsNavigation.UseTabKey = False
    Me.grvTco.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvTco.OptionsView.ColumnAutoWidth = False
    Me.grvTco.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvTco.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvTco.OptionsView.ShowGroupPanel = False
    Me.grvTco.RowHeight = 14
    '
    'ec_quant01
    '
    Me.ec_quant01.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant01.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant01.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant01.Caption = "QTA1"
    Me.ec_quant01.Enabled = True
    Me.ec_quant01.FieldName = "ec_quant01"
    Me.ec_quant01.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant01.Name = "ec_quant01"
    Me.ec_quant01.NTSRepositoryComboBox = Nothing
    Me.ec_quant01.NTSRepositoryItemCheck = Nothing
    Me.ec_quant01.NTSRepositoryItemMemo = Nothing
    Me.ec_quant01.NTSRepositoryItemText = Nothing
    Me.ec_quant01.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant01.OptionsFilter.AllowFilter = False
    Me.ec_quant01.Visible = True
    Me.ec_quant01.VisibleIndex = 0
    '
    'ec_quant02
    '
    Me.ec_quant02.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant02.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant02.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant02.Caption = "QTA2"
    Me.ec_quant02.Enabled = True
    Me.ec_quant02.FieldName = "ec_quant02"
    Me.ec_quant02.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant02.Name = "ec_quant02"
    Me.ec_quant02.NTSRepositoryComboBox = Nothing
    Me.ec_quant02.NTSRepositoryItemCheck = Nothing
    Me.ec_quant02.NTSRepositoryItemMemo = Nothing
    Me.ec_quant02.NTSRepositoryItemText = Nothing
    Me.ec_quant02.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant02.OptionsFilter.AllowFilter = False
    Me.ec_quant02.Visible = True
    Me.ec_quant02.VisibleIndex = 1
    '
    'ec_quant03
    '
    Me.ec_quant03.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant03.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant03.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant03.Caption = "QTA3"
    Me.ec_quant03.Enabled = True
    Me.ec_quant03.FieldName = "ec_quant03"
    Me.ec_quant03.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant03.Name = "ec_quant03"
    Me.ec_quant03.NTSRepositoryComboBox = Nothing
    Me.ec_quant03.NTSRepositoryItemCheck = Nothing
    Me.ec_quant03.NTSRepositoryItemMemo = Nothing
    Me.ec_quant03.NTSRepositoryItemText = Nothing
    Me.ec_quant03.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant03.OptionsFilter.AllowFilter = False
    Me.ec_quant03.Visible = True
    Me.ec_quant03.VisibleIndex = 2
    '
    'ec_quant04
    '
    Me.ec_quant04.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant04.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant04.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant04.Caption = "QTA4"
    Me.ec_quant04.Enabled = True
    Me.ec_quant04.FieldName = "ec_quant04"
    Me.ec_quant04.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant04.Name = "ec_quant04"
    Me.ec_quant04.NTSRepositoryComboBox = Nothing
    Me.ec_quant04.NTSRepositoryItemCheck = Nothing
    Me.ec_quant04.NTSRepositoryItemMemo = Nothing
    Me.ec_quant04.NTSRepositoryItemText = Nothing
    Me.ec_quant04.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant04.OptionsFilter.AllowFilter = False
    Me.ec_quant04.Visible = True
    Me.ec_quant04.VisibleIndex = 3
    '
    'ec_quant05
    '
    Me.ec_quant05.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant05.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant05.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant05.Caption = "QTA5"
    Me.ec_quant05.Enabled = True
    Me.ec_quant05.FieldName = "ec_quant05"
    Me.ec_quant05.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant05.Name = "ec_quant05"
    Me.ec_quant05.NTSRepositoryComboBox = Nothing
    Me.ec_quant05.NTSRepositoryItemCheck = Nothing
    Me.ec_quant05.NTSRepositoryItemMemo = Nothing
    Me.ec_quant05.NTSRepositoryItemText = Nothing
    Me.ec_quant05.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant05.OptionsFilter.AllowFilter = False
    Me.ec_quant05.Visible = True
    Me.ec_quant05.VisibleIndex = 4
    '
    'ec_quant06
    '
    Me.ec_quant06.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant06.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant06.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant06.Caption = "QTA6"
    Me.ec_quant06.Enabled = True
    Me.ec_quant06.FieldName = "ec_quant06"
    Me.ec_quant06.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant06.Name = "ec_quant06"
    Me.ec_quant06.NTSRepositoryComboBox = Nothing
    Me.ec_quant06.NTSRepositoryItemCheck = Nothing
    Me.ec_quant06.NTSRepositoryItemMemo = Nothing
    Me.ec_quant06.NTSRepositoryItemText = Nothing
    Me.ec_quant06.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant06.OptionsFilter.AllowFilter = False
    Me.ec_quant06.Visible = True
    Me.ec_quant06.VisibleIndex = 5
    '
    'ec_quant07
    '
    Me.ec_quant07.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant07.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant07.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant07.Caption = "QTA7"
    Me.ec_quant07.Enabled = True
    Me.ec_quant07.FieldName = "ec_quant07"
    Me.ec_quant07.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant07.Name = "ec_quant07"
    Me.ec_quant07.NTSRepositoryComboBox = Nothing
    Me.ec_quant07.NTSRepositoryItemCheck = Nothing
    Me.ec_quant07.NTSRepositoryItemMemo = Nothing
    Me.ec_quant07.NTSRepositoryItemText = Nothing
    Me.ec_quant07.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant07.OptionsFilter.AllowFilter = False
    Me.ec_quant07.Visible = True
    Me.ec_quant07.VisibleIndex = 6
    '
    'ec_quant08
    '
    Me.ec_quant08.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant08.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant08.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant08.Caption = "QTA8"
    Me.ec_quant08.Enabled = True
    Me.ec_quant08.FieldName = "ec_quant08"
    Me.ec_quant08.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant08.Name = "ec_quant08"
    Me.ec_quant08.NTSRepositoryComboBox = Nothing
    Me.ec_quant08.NTSRepositoryItemCheck = Nothing
    Me.ec_quant08.NTSRepositoryItemMemo = Nothing
    Me.ec_quant08.NTSRepositoryItemText = Nothing
    Me.ec_quant08.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant08.OptionsFilter.AllowFilter = False
    Me.ec_quant08.Visible = True
    Me.ec_quant08.VisibleIndex = 7
    '
    'ec_quant09
    '
    Me.ec_quant09.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant09.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant09.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant09.Caption = "QTA9"
    Me.ec_quant09.Enabled = True
    Me.ec_quant09.FieldName = "ec_quant09"
    Me.ec_quant09.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant09.Name = "ec_quant09"
    Me.ec_quant09.NTSRepositoryComboBox = Nothing
    Me.ec_quant09.NTSRepositoryItemCheck = Nothing
    Me.ec_quant09.NTSRepositoryItemMemo = Nothing
    Me.ec_quant09.NTSRepositoryItemText = Nothing
    Me.ec_quant09.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant09.OptionsFilter.AllowFilter = False
    Me.ec_quant09.Visible = True
    Me.ec_quant09.VisibleIndex = 8
    '
    'ec_quant10
    '
    Me.ec_quant10.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant10.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant10.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant10.Caption = "QTA10"
    Me.ec_quant10.Enabled = True
    Me.ec_quant10.FieldName = "ec_quant10"
    Me.ec_quant10.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant10.Name = "ec_quant10"
    Me.ec_quant10.NTSRepositoryComboBox = Nothing
    Me.ec_quant10.NTSRepositoryItemCheck = Nothing
    Me.ec_quant10.NTSRepositoryItemMemo = Nothing
    Me.ec_quant10.NTSRepositoryItemText = Nothing
    Me.ec_quant10.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant10.OptionsFilter.AllowFilter = False
    Me.ec_quant10.Visible = True
    Me.ec_quant10.VisibleIndex = 9
    '
    'ec_quant11
    '
    Me.ec_quant11.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant11.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant11.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant11.Caption = "QTA11"
    Me.ec_quant11.Enabled = True
    Me.ec_quant11.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant11.Name = "ec_quant11"
    Me.ec_quant11.NTSRepositoryComboBox = Nothing
    Me.ec_quant11.NTSRepositoryItemCheck = Nothing
    Me.ec_quant11.NTSRepositoryItemMemo = Nothing
    Me.ec_quant11.NTSRepositoryItemText = Nothing
    Me.ec_quant11.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant11.OptionsFilter.AllowFilter = False
    Me.ec_quant11.Visible = True
    Me.ec_quant11.VisibleIndex = 10
    '
    'ec_quant12
    '
    Me.ec_quant12.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant12.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant12.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant12.Caption = "QTA12"
    Me.ec_quant12.Enabled = True
    Me.ec_quant12.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant12.Name = "ec_quant12"
    Me.ec_quant12.NTSRepositoryComboBox = Nothing
    Me.ec_quant12.NTSRepositoryItemCheck = Nothing
    Me.ec_quant12.NTSRepositoryItemMemo = Nothing
    Me.ec_quant12.NTSRepositoryItemText = Nothing
    Me.ec_quant12.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant12.OptionsFilter.AllowFilter = False
    Me.ec_quant12.Visible = True
    Me.ec_quant12.VisibleIndex = 11
    '
    'ec_quant13
    '
    Me.ec_quant13.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant13.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant13.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant13.Caption = "QTA13"
    Me.ec_quant13.Enabled = True
    Me.ec_quant13.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant13.Name = "ec_quant13"
    Me.ec_quant13.NTSRepositoryComboBox = Nothing
    Me.ec_quant13.NTSRepositoryItemCheck = Nothing
    Me.ec_quant13.NTSRepositoryItemMemo = Nothing
    Me.ec_quant13.NTSRepositoryItemText = Nothing
    Me.ec_quant13.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant13.OptionsFilter.AllowFilter = False
    Me.ec_quant13.Visible = True
    Me.ec_quant13.VisibleIndex = 12
    '
    'ec_quant14
    '
    Me.ec_quant14.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant14.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant14.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant14.Caption = "QTA14"
    Me.ec_quant14.Enabled = True
    Me.ec_quant14.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant14.Name = "ec_quant14"
    Me.ec_quant14.NTSRepositoryComboBox = Nothing
    Me.ec_quant14.NTSRepositoryItemCheck = Nothing
    Me.ec_quant14.NTSRepositoryItemMemo = Nothing
    Me.ec_quant14.NTSRepositoryItemText = Nothing
    Me.ec_quant14.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant14.OptionsFilter.AllowFilter = False
    Me.ec_quant14.Visible = True
    Me.ec_quant14.VisibleIndex = 13
    '
    'ec_quant15
    '
    Me.ec_quant15.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant15.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant15.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant15.Caption = "QTA15"
    Me.ec_quant15.Enabled = True
    Me.ec_quant15.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant15.Name = "ec_quant15"
    Me.ec_quant15.NTSRepositoryComboBox = Nothing
    Me.ec_quant15.NTSRepositoryItemCheck = Nothing
    Me.ec_quant15.NTSRepositoryItemMemo = Nothing
    Me.ec_quant15.NTSRepositoryItemText = Nothing
    Me.ec_quant15.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant15.OptionsFilter.AllowFilter = False
    Me.ec_quant15.Visible = True
    Me.ec_quant15.VisibleIndex = 14
    '
    'ec_quant16
    '
    Me.ec_quant16.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant16.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant16.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant16.Caption = "QTA16"
    Me.ec_quant16.Enabled = True
    Me.ec_quant16.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant16.Name = "ec_quant16"
    Me.ec_quant16.NTSRepositoryComboBox = Nothing
    Me.ec_quant16.NTSRepositoryItemCheck = Nothing
    Me.ec_quant16.NTSRepositoryItemMemo = Nothing
    Me.ec_quant16.NTSRepositoryItemText = Nothing
    Me.ec_quant16.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant16.OptionsFilter.AllowFilter = False
    Me.ec_quant16.Visible = True
    Me.ec_quant16.VisibleIndex = 15
    '
    'ec_quant17
    '
    Me.ec_quant17.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant17.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant17.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant17.Caption = "QTA17"
    Me.ec_quant17.Enabled = True
    Me.ec_quant17.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant17.Name = "ec_quant17"
    Me.ec_quant17.NTSRepositoryComboBox = Nothing
    Me.ec_quant17.NTSRepositoryItemCheck = Nothing
    Me.ec_quant17.NTSRepositoryItemMemo = Nothing
    Me.ec_quant17.NTSRepositoryItemText = Nothing
    Me.ec_quant17.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant17.OptionsFilter.AllowFilter = False
    Me.ec_quant17.Visible = True
    Me.ec_quant17.VisibleIndex = 16
    '
    'ec_quant18
    '
    Me.ec_quant18.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant18.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant18.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant18.Caption = "QTA18"
    Me.ec_quant18.Enabled = True
    Me.ec_quant18.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant18.Name = "ec_quant18"
    Me.ec_quant18.NTSRepositoryComboBox = Nothing
    Me.ec_quant18.NTSRepositoryItemCheck = Nothing
    Me.ec_quant18.NTSRepositoryItemMemo = Nothing
    Me.ec_quant18.NTSRepositoryItemText = Nothing
    Me.ec_quant18.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant18.OptionsFilter.AllowFilter = False
    Me.ec_quant18.Visible = True
    Me.ec_quant18.VisibleIndex = 17
    '
    'ec_quant19
    '
    Me.ec_quant19.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant19.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant19.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant19.Caption = "QTA19"
    Me.ec_quant19.Enabled = True
    Me.ec_quant19.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant19.Name = "ec_quant19"
    Me.ec_quant19.NTSRepositoryComboBox = Nothing
    Me.ec_quant19.NTSRepositoryItemCheck = Nothing
    Me.ec_quant19.NTSRepositoryItemMemo = Nothing
    Me.ec_quant19.NTSRepositoryItemText = Nothing
    Me.ec_quant19.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant19.OptionsFilter.AllowFilter = False
    Me.ec_quant19.Visible = True
    Me.ec_quant19.VisibleIndex = 18
    '
    'ec_quant20
    '
    Me.ec_quant20.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant20.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant20.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant20.Caption = "QTA20"
    Me.ec_quant20.Enabled = True
    Me.ec_quant20.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant20.Name = "ec_quant20"
    Me.ec_quant20.NTSRepositoryComboBox = Nothing
    Me.ec_quant20.NTSRepositoryItemCheck = Nothing
    Me.ec_quant20.NTSRepositoryItemMemo = Nothing
    Me.ec_quant20.NTSRepositoryItemText = Nothing
    Me.ec_quant20.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant20.OptionsFilter.AllowFilter = False
    Me.ec_quant20.Visible = True
    Me.ec_quant20.VisibleIndex = 19
    '
    'ec_quant21
    '
    Me.ec_quant21.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant21.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant21.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant21.Caption = "QTA21"
    Me.ec_quant21.Enabled = True
    Me.ec_quant21.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant21.Name = "ec_quant21"
    Me.ec_quant21.NTSRepositoryComboBox = Nothing
    Me.ec_quant21.NTSRepositoryItemCheck = Nothing
    Me.ec_quant21.NTSRepositoryItemMemo = Nothing
    Me.ec_quant21.NTSRepositoryItemText = Nothing
    Me.ec_quant21.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant21.OptionsFilter.AllowFilter = False
    Me.ec_quant21.Visible = True
    Me.ec_quant21.VisibleIndex = 20
    '
    'ec_quant22
    '
    Me.ec_quant22.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant22.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant22.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant22.Caption = "QTA22"
    Me.ec_quant22.Enabled = True
    Me.ec_quant22.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant22.Name = "ec_quant22"
    Me.ec_quant22.NTSRepositoryComboBox = Nothing
    Me.ec_quant22.NTSRepositoryItemCheck = Nothing
    Me.ec_quant22.NTSRepositoryItemMemo = Nothing
    Me.ec_quant22.NTSRepositoryItemText = Nothing
    Me.ec_quant22.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant22.OptionsFilter.AllowFilter = False
    Me.ec_quant22.Visible = True
    Me.ec_quant22.VisibleIndex = 21
    '
    'ec_quant23
    '
    Me.ec_quant23.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant23.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant23.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant23.Caption = "QTA23"
    Me.ec_quant23.Enabled = True
    Me.ec_quant23.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant23.Name = "ec_quant23"
    Me.ec_quant23.NTSRepositoryComboBox = Nothing
    Me.ec_quant23.NTSRepositoryItemCheck = Nothing
    Me.ec_quant23.NTSRepositoryItemMemo = Nothing
    Me.ec_quant23.NTSRepositoryItemText = Nothing
    Me.ec_quant23.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant23.OptionsFilter.AllowFilter = False
    Me.ec_quant23.Visible = True
    Me.ec_quant23.VisibleIndex = 22
    '
    'ec_quant24
    '
    Me.ec_quant24.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant24.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant24.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant24.Caption = "QTA24"
    Me.ec_quant24.Enabled = True
    Me.ec_quant24.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant24.Name = "ec_quant24"
    Me.ec_quant24.NTSRepositoryComboBox = Nothing
    Me.ec_quant24.NTSRepositoryItemCheck = Nothing
    Me.ec_quant24.NTSRepositoryItemMemo = Nothing
    Me.ec_quant24.NTSRepositoryItemText = Nothing
    Me.ec_quant24.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant24.OptionsFilter.AllowFilter = False
    Me.ec_quant24.Visible = True
    Me.ec_quant24.VisibleIndex = 23
    '
    'pnTCO
    '
    Me.pnTCO.AllowDrop = True
    Me.pnTCO.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTCO.Appearance.Options.UseBackColor = True
    Me.pnTCO.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTCO.Controls.Add(Me.grTco)
    Me.pnTCO.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTCO.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnTCO.Location = New System.Drawing.Point(0, 379)
    Me.pnTCO.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTCO.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTCO.Name = "pnTCO"
    Me.pnTCO.NTSActiveTrasparency = True
    Me.pnTCO.Size = New System.Drawing.Size(648, 63)
    Me.pnTCO.TabIndex = 5
    Me.pnTCO.Text = "pnTCO"
    '
    'FRMORGSOL
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.pnGriglia)
    Me.Controls.Add(Me.pnTCO)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMORGSOL"
    Me.Text = "GESTIONE PROPOSTE D'ORDINE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnGriglia, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGriglia.ResumeLayout(False)
    CType(Me.grRighe, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvRighe, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grTco, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvTco, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTCO, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTCO.ResumeLayout(False)
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNORGSOL", "BEORGSOL", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128617274933281250, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleGsol = CType(oTmp, CLEORGSOL)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNORGSOL", strRemoteServer, strRemotePort)
    AddHandler oCleGsol.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleGsol.Init(oApp, oScript, oMenu.oCleComm, "TABZONE", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overloads Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    '---------------------------------
    'questa funzione riceve gli eventi dall'ENTITY: rimappata rispetto a quella standard di FRM__CHILD
    'prima eseguo quella standard
    Dim strTmp() As String
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim strT As String = ""

    If Not IsMyThrowRemoteEvent() Then Return 'il messaggio non è per questa form ...
    MyBase.GestisciEventiEntity(sender, e)

    Try
      '---------------------------------
      'adesso gestisco le specifiche
      'devo inserire delle funzioni qui sotto per fare in modo che al variare di dati nell'entity delle informazioni 
      'legate all'interfaccia grafica (ui) vengano allineate a quanto richiesto dall'entity

      If e.TipoEvento.Trim.Length < 10 Then Return
      strTmp = e.TipoEvento.Split(CType("|", Char))
      For i = 0 To strTmp.Length - 1
        Select Case strTmp(i).Substring(0, 10)

          Case "ArticoloTC"
            'blocco la colonna QTA se articolo TC
            If strTmp(i).Substring(11, 1) = "S" Then
              ec_quant.Enabled = False
            Else
              GctlSetVisEnab(ec_quant, False)
            End If

          Case "DataValDB."
            e.RetValue = DateTime.Now.ToShortDateString

        End Select
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttTask As New DataTable()
    Dim dttStato As New DataTable()
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbApri.GlyphPath = (oApp.ChildImageDir & "\open.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbRecordNuovo.GlyphPath = (oApp.ChildImageDir & "\recnew.gif")
        tlbRecordSalva.GlyphPath = (oApp.ChildImageDir & "\recagg.gif")
        tlbRecordRipristina.GlyphPath = (oApp.ChildImageDir & "\recrestore.gif")
        tlbRecordCancella.GlyphPath = (oApp.ChildImageDir & "\recdelete.gif")
        tlbMrp.GlyphPath = (oApp.ChildImageDir & "\movmrp.gif")
        tlbImpegni.GlyphPath = (oApp.ChildImageDir & "\doc.gif")
        tlbLavorazioni.GlyphPath = (oApp.ChildImageDir & "\ordini.gif")
        tlbProgressivi.GlyphPath = (oApp.ChildImageDir & "\ordini_3.gif")
        tlbMovimenti.GlyphPath = (oApp.ChildImageDir & "\ordini_2.gif")
        tlbDettTco.GlyphPath = (oApp.ChildImageDir & "\tc.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      '-------------------------------------------------
      'la griglia

      dttTask.Columns.Add("cod", GetType(String))
      dttTask.Columns.Add("val", GetType(String))
      dttTask.Rows.Add(New Object() {"S", "Saldato"})
      dttTask.Rows.Add(New Object() {"C", "Aperto"})
      dttTask.Rows.Add(New Object() {"Q", "Q"})
      dttTask.Rows.Add(New Object() {"V", "B"})
      dttTask.AcceptChanges()

      dttStato.Columns.Add("cod", GetType(String))
      dttStato.Columns.Add("val", GetType(String))
      dttStato.Rows.Add(New Object() {" ", "Generato"})
      dttStato.Rows.Add(New Object() {"P", "Emissione RDA"})
      dttStato.Rows.Add(New Object() {"Q", "Approvazione RDA"})
      dttStato.Rows.Add(New Object() {"R", "Emissione RDO"})
      dttStato.Rows.Add(New Object() {"S", "Confermato"})
      dttStato.Rows.Add(New Object() {"T", "Emesso ordine"})
      dttStato.Rows.Add(New Object() {"F", "Congelato"})
      dttStato.Rows.Add(New Object() {"X", "Temporaneo"})
      dttStato.AcceptChanges()

      '-------------------------------------------------
      'carico le unità di misura nella colonna colli: caso particolare
      'carico tutte le unità di misura gestite dagli articoli, 
      'poi al cambio di riga filtrero nel combo solo quelle gestite dall'articolo in analisi
      dttUm = oCleGsol.CaricaUnMis()
      dttUm.AcceptChanges()

      grvRighe.NTSSetParam(oMenu, "GESTIONE PROPOSTE D'ORDINE")
      grvRighe.NTSSetParam(oMenu, oApp.Tr(Me, 128230023247061922, "Griglia righe ordine"))
      ec_matric.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023247374268, "Barcode"), 30, False)
      ec_codart.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128230023247530441, "Cod. Art."), tabartico, False)
      ec_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023247686614, "Descrizione"), 40, True)
      ec_desint.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023247842787, "Descr.interna"), 40, True)
      ec_unmis.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128230023247998960, "U.M."), dttUm, "tb_codumis", "tb_codumis")
      ec_colli.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023248155133, "Colli ord."), oApp.FormatQta, 13)
      ec_ump.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023248623652, "UMP"), 3, False)
      ec_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023248779825, "Q.tà  ordin."), oApp.FormatQta, 13)
      ec_prezvalc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023250029209, "Prezzo valuta"), oApp.FormatPrzUnVal, 13)
      ec_prezzo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023250185382, "Prezzo"), oApp.FormatPrzUn, 13)
      ec_scont1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023250341555, "Sconto 1"), oApp.FormatSconti, 6, -100, 100)
      ec_scont2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023250497728, "Sconto 2"), oApp.FormatSconti, 6, -100, 100)
      ec_scont3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023250653901, "Sconto 3"), oApp.FormatSconti, 6, -100, 100)
      ec_scont4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023250810074, "Sconto 4"), oApp.FormatSconti, 6, -100, 100)
      ec_scont5.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023250966247, "Sconto 5"), oApp.FormatSconti, 6, -100, 100)
      ec_scont6.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023251122420, "Sconto 6"), oApp.FormatSconti, 6, -100, 100)
      ec_magaz.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023251278593, "Magazzino"), tabmaga)
      xxo_magaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023251434766, "Descr. magazzino"), 0, True)
      ec_datcons.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128230023251590939, "Data cons."), False)
      ec_controp.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023252996496, "Controp."), tabcove)
      xxo_controp.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023253152669, "Descr. controp."), 0, True)
      ec_codiva.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023253308842, "Cod. IVA"), tabciva)
      xxo_codiva.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023253465015, "Descr. IVA"), 0, True)
      ec_codcfam.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128230023253777361, "Linea/Fam."), tabcfam, True)
      xxo_codcfam.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023253933534, "Descr. linea/fam"), 0, True)
      ec_commeca.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023254089707, "Comm. C.A."), tabcommess)
      xxo_commeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023254245880, "Descr. commessa"), 0, True)
      ec_subcommeca.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128230023254402053, "Sub C."), tabsubcomm, True)
      If oCleGsol.bLottoNew = False Then
        xxo_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023254558226, "Lotto"), 9, True)
      Else
        xxo_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129513869925818712, "Lotto"), 50, True)
      End If
      ec_codcena.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023254714399, "Centro C.A."), tabcena)
      xxo_codcena.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023254870572, "Descr. centro"), 0, True)
      ec_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023255026745, "Note"), 0, True)
      ec_magaz2.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023255182918, "Magaz 2"), tabmaga)
      xxo_magaz2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023255339091, "Descr. magaz. 2"), 0, True)
      ec_valore.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023255495264, "Valore"), oApp.FormatImporti, 13)
      ec_valorev.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617963257968750, "Valore in valuta"), oApp.FormatImpVal, 13)
      ec_contocontr.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023255651437, "Conto controp."), tabanagra)
      xxo_contocon.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023255807610, "Descr. conto controp."), 0, True)
      ec_codclie.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023256119956, "Cod. cliente"), tabanagrac)
      xxo_codclie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023256276129, "Descr. cliente"), 0, True)
      ec_misura1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023256432302, "Misura 1"), "#,##0.00", 13)
      ec_misura2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023256588475, "Misura 2"), "#,##0.00", 13)
      ec_misura3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023256744648, "Misura 3"), "#,##0.00", 13)
      ec_perqta.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023257056994, "Prz/Qta"), "#,##0.00", 13)
      ec_umprz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023259087243, "Prezzi x u.d.m."), 3, False)
      ec_fase.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023259243416, "Fase"), tabartfasi)
      ec_fase.ArtiPerFase = ec_codart
      xxo_fase.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023259399589, "Descr. fase"), 0, True)
      ec_codlavo.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023259555762, "Codice lavoraz."), tablavo)
      xxo_codlavo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023259711935, "Descr. lavoraz."), 0, True)
      ec_pmtaskid.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023259868108, "Id Task"), "0", 9, 0, 999999999)
      xxo_pmtaskid.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023260024281, "Descr. Task"), 0, True)
      ec_pmsalcon.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128230023260805146, "Sal. Task"), dttTask, "val", "cod")
      ec_flprznet.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128786513480540000, "Prezzo netto"), "S", "N")
      ec_datini.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128786513500664000, "Dt iniz. comp. econ."), False)
      ec_datfin.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128786513528900000, "Dt fin. comp. econ."), False)
      xxo_prznet.NTSSetParamNUM(oMenu, oApp.Tr(Me, 131007777049600135, "Prezzo netto"), oApp.FormatPrzUn, 15)

      xxo_seleziona.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128618128548281250, "Seleziona"), "S", "N")
      ec_riga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128618128548437500, "Progr."), "0", 9, 0, 999999999)
      ec_conto.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128618128549687500, "Fornitore"), tabanagraf)
      xxo_conto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128618128549843750, "Descr. forn."), 0, True)
      ec_codvalu.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128618128550156250, "Valuta"), tabvalu)
      xxo_codvalu.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128618128550312500, "Descr. valuta"), 0, True)
      ec_cambio.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128618128550468750, "Cambio"), "#,##0.000000000", 20, 0, 99999999)
      ec_stato.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128618128550937500, "Stato"), dttStato, "val", "cod")
      ec_magimp.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128618128552656250, "Magaz impegni"), tabmaga)
      xxo_magimp.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128618128552812500, "Descr. magaz. imp."), 0, True)
      ec_datord.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128618128552968750, "Data mass. ord"), False)
      ec_flcom.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128618128556718750, "Da controllare"), "S", "N")
      ec_flforf.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128618128557031250, "A forfait"), "S", "N")
      ec_pmqtadis.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128618128559062500, "PMQTADIS"), oApp.FormatQta, 15)
      ec_pmvaldis.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128618128559218750, "PMVALDIS"), oApp.FormatQta, 15)
      ec_rdatipork.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128618128559375000, "Tipo RDA"), 1, False)
      ec_rdaanno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128618128559531250, "Anno RDA"), "0", 4, 0, 9999)
      ec_rdaserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128618128559687500, "Serie RDA"), CLN__STD.SerieMaxLen, False)
      ec_rdanum.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128618128559843750, "Num. RDA"), "0", 9, 0, 999999999)
      ec_rdariga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128618128560000000, "Riga RDA"), "0", 9, 0, 999999999)
      ec_offreq.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128618128560156250, "OFFREQ"), 1, False)
      ec_ortipork.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128618128560312500, "Tipo ord.gen."), 1, False)
      ec_oranno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128618128560468750, "Anno ord.gen."), "0", 4, 0, 9999)
      ec_orserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128618128560625000, "Serie ord.gen."), CLN__STD.SerieMaxLen, False)
      ec_ornum.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128618128560781250, "N° ord.gen."), "0", 9, 0, 999999999)
      ec_orriga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128618128560937500, "Riga ord.gen."), "0", 9, 0, 999999999)
      xxo_codtagl.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023261586011, "."), "0", 4, 0, 9999)

      '-------------------------------------------------
      'griglia TCO
      grvTco.NTSSetParam(oMenu, oApp.Tr(Me, 129048532917383639, "Griglia dettaglio taglie"))
      ec_quant01.NTSSetParamNUM(oMenu, "Quantità 1^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant02.NTSSetParamNUM(oMenu, "Quantità 2^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant03.NTSSetParamNUM(oMenu, "Quantità 3^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant04.NTSSetParamNUM(oMenu, "Quantità 4^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant05.NTSSetParamNUM(oMenu, "Quantità 5^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant06.NTSSetParamNUM(oMenu, "Quantità 6^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant07.NTSSetParamNUM(oMenu, "Quantità 7^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant08.NTSSetParamNUM(oMenu, "Quantità 8^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant09.NTSSetParamNUM(oMenu, "Quantità 9^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant10.NTSSetParamNUM(oMenu, "Quantità 10^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant11.NTSSetParamNUM(oMenu, "Quantità 11^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant12.NTSSetParamNUM(oMenu, "Quantità 12^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant13.NTSSetParamNUM(oMenu, "Quantità 13^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant14.NTSSetParamNUM(oMenu, "Quantità 14^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant15.NTSSetParamNUM(oMenu, "Quantità 15^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant16.NTSSetParamNUM(oMenu, "Quantità 16^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant17.NTSSetParamNUM(oMenu, "Quantità 17^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant18.NTSSetParamNUM(oMenu, "Quantità 18^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant19.NTSSetParamNUM(oMenu, "Quantità 19^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant20.NTSSetParamNUM(oMenu, "Quantità 20^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant21.NTSSetParamNUM(oMenu, "Quantità 21^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant22.NTSSetParamNUM(oMenu, "Quantità 22^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant23.NTSSetParamNUM(oMenu, "Quantità 23^ taglia", "#,##0", 6, -99999, 99999)
      ec_quant24.NTSSetParamNUM(oMenu, "Quantità 24^ taglia", "#,##0", 6, -99999, 99999)

      grvTco.NTSAllowDelete = False
      grvTco.NTSAllowUpdate = False
      grvTco.NTSAllowInsert = False
      grvTco.Enabled = False

      ec_codart.NTSSetRichiesto()
      ec_conto.NTSSetRichiesto()
      ec_quant.NTSSetRichiesto()
      ec_magaz.NTSSetRichiesto()
      ec_magimp.NTSSetRichiesto()
      ec_datcons.NTSSetRichiesto()

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

  Public Overridable Function SetStato(ByVal nStato As Integer) As Boolean
    Dim i As Integer = 0
    Dim strTmp As String = ""
    Try
      bCambioDelCambio = False

      i = Me.Text.IndexOf(" <")
      If i > -1 Then strTmp = Me.Text.Substring(i)

      Select Case nStato
        Case 0 'griglia non visibile
          Me.Text = oApp.Tr(Me, 128617998920156250, "GESTIONE PROPOSTE D'ORDINE") & strTmp

          pnGriglia.Visible = False
          pnTCO.Visible = False
          grTco.Visible = False

          GctlSetVisEnab(tlbNuovo, False)
          GctlSetVisEnab(tlbApri, False)
          tlbCancella.Enabled = False
          tlbSalva.Enabled = False
          tlbRecordNuovo.Enabled = False
          tlbRecordSalva.Enabled = False
          tlbRecordCancella.Enabled = False
          tlbRecordRipristina.Enabled = False
          tlbZoom.Enabled = False
          tlbZoomFornPrz.Enabled = False
          tlbMrp.Enabled = False
          tlbImpegni.Enabled = False
          tlbProgressivi.Enabled = False
          tlbLavorazioni.Enabled = False
          tlbMovimenti.Enabled = False
          tlbDettTco.Enabled = False
          tlbConfermatutto.Enabled = False
          tlbConfermaselez.Enabled = False
          tlbGeneraOrdini.Enabled = False
          tlbVisDetTco.Enabled = False
          tlbRicalcolaCollidaQta.Enabled = False
          tlbStampa.Enabled = False
          tlbStampaVideo.Enabled = False
          tblPrnImpCarta.Enabled = False
          tblPrnImpVideo.Enabled = False
          tblPrnAttCarta.Enabled = False
          tblPrnAttVideo.Enabled = False
          tlbSelezionaTutto.Enabled = False
          tlbDeselezionaTutto.Enabled = False
          tlbInvertiSelezione.Enabled = False

          oCleGsol.SvuotaTemporaneiImpegni()

        Case 1  'griglia visibile
          Select Case oCleGsol.strTipork
            Case Is = "O" : Me.Text = oApp.Tr(Me, 128617997918437500, "GESTIONE PROPOSTE D'ORDINE :  'Ordini Fornitori'") & strTmp
            Case Is = "H" : Me.Text = oApp.Tr(Me, 128617998020000000, "GESTIONE PROPOSTE D'ORDINE :  'Ordini di Produzione'") & strTmp
            Case Is = "Y" : Me.Text = oApp.Tr(Me, 128617998036718750, "GESTIONE PROPOSTE D'ORDINE :  'Impegni di Produzione'") & strTmp
            Case Is = "X" : Me.Text = oApp.Tr(Me, 128617998047656250, "GESTIONE PROPOSTE D'ORDINE :  'Impegni di Trasferimento'") & strTmp
          End Select
          pnGriglia.Visible = True
          If oCleGsol.bModTCO Then GctlSetVisEnab(pnTCO, True)
          'grTco.Visible = False

          tlbNuovo.Enabled = False
          tlbApri.Enabled = False
          GctlSetVisEnab(tlbCancella, False)
          GctlSetVisEnab(tlbSalva, False)
          GctlSetVisEnab(tlbRecordNuovo, False)
          GctlSetVisEnab(tlbRecordSalva, False)
          GctlSetVisEnab(tlbRecordCancella, False)
          GctlSetVisEnab(tlbRecordRipristina, False)
          GctlSetVisEnab(tlbZoom, False)
          GctlSetVisEnab(tlbZoomFornPrz, False)
          GctlSetVisEnab(tlbMrp, False)
          If oCleGsol.strTipork = "H" Then
            GctlSetVisEnab(tlbImpegni, False)
            GctlSetVisEnab(tlbLavorazioni, False)
          End If
          GctlSetVisEnab(tlbProgressivi, False)
          GctlSetVisEnab(tlbMovimenti, False)
          'tlbDettTco.Enabled = False
          GctlSetVisEnab(tlbConfermatutto, False)
          GctlSetVisEnab(tlbConfermaselez, False)
          GctlSetVisEnab(tlbGeneraOrdini, False)
          If oCleGsol.bModTCO Then GctlSetVisEnab(tlbVisDetTco, False)
          GctlSetVisEnab(tlbRicalcolaCollidaQta, False)
          GctlSetVisEnab(tlbStampa, False)
          GctlSetVisEnab(tlbStampaVideo, False)
          If oCleGsol.strTipork = "H" Then
            GctlSetVisEnab(tblPrnImpCarta, False)
            GctlSetVisEnab(tblPrnImpVideo, False)
            GctlSetVisEnab(tblPrnAttCarta, False)
            GctlSetVisEnab(tblPrnAttVideo, False)
          End If
          GctlSetVisEnab(tlbSelezionaTutto, False)
          GctlSetVisEnab(tlbDeselezionaTutto, False)
          GctlSetVisEnab(tlbInvertiSelezione, False)

          Select Case oCleGsol.strTipork
            Case "O"
              ec_magaz2.Enabled = False
              ec_magimp.Enabled = False
            Case "H"
              GctlSetVisEnab(ec_magimp, False)
              ec_magaz2.Enabled = False
            Case "X"
              GctlSetVisEnab(ec_magaz2, False)
              ec_magimp.Enabled = False
          End Select

          '-------------------------------------------------
          'carico impegni collegati se prop d'ordine di prod.
          oCleGsol.SvuotaTemporaneiImpegni()
          If oCleGsol.strTipork = "H" And Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
            If Not oCleGsol.ApriOrdListImpegni(NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga)) Then   'occhio alla prima riga quando faccio apri e/o quando cancello la riga sotto ...

            End If
          End If

          grvRighe.NTSMoveFirstRowColunn()
          grvRighe.Focus()
      End Select

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

#Region "Eventi di Form"
  Public Overridable Sub FRMORGSOL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      oCleGsol.bModTCO = CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCO)
      '-------------------------------------------------
      'nascondo quello che non serve se non c'è il TCO
      If oCleGsol.bModTCO = False Then
        pnTCO.Visible = False
        tlbDettTco.Visible = False
        tlbVisDetTco.Visible = False
      End If

      If CBool(oMenu.ModuliDittaDitt(DittaCorrente) And bsModRA) = True Then
        tlbGeneraOrdini.Visible = False
        tlbGeneraOrdini.Enabled = False
        tlbGeneraOrdini.ItemShortcut = Nothing
      End If

      '-------------------------------------------------
      SetStato(0)

      If Not oCleGsol.InitExt() Then
        Me.Close()
        Return
      End If
      If oCleGsol.bModuloCRM Then
        oCleGsol.bIsCRMUser = oMenu.IsCrmUser(DittaCorrente, oCleGsol.bAmm, oCleGsol.strAccvis, oCleGsol.strAccmod, oCleGsol.strRegvis, oCleGsol.strRegmod)
      End If

      '-------------------------------------------------
      'leggo dal database i dati
      'NB: se ci saranno dei controlli collegati a datasource, nel datatable sottostante
      '     un record deve esempre esserci, almeno in addnew...
      oCleGsol.strTipork = "O"
      If Not oCleGsol.ApriOrdlist(DittaCorrente, "", -1, 0, 0, IntSetDate("01/01/1900"), IntSetDate("31/12/2099"), _
                                  IntSetDate("01/01/1900"), IntSetDate("31/12/2099"), False, False, False, False, False, _
                                  False, False, 0, dsGsol) Then
        Me.Close()
        Return
      End If
      dcGsol.DataSource = dsGsol.Tables("CORPO")

      '-------------------------------------------------
      If Not oCleGsol.LeggiRegistro() Then
        Me.Close()
        Return
      End If

      lIIOrdl = oMenu.GetTblInstId("TTORDL", False)
      lIIAttivit = oMenu.GetTblInstId("TTATTIVIT", False)
      lIIAssris = oMenu.GetTblInstId("TTASSRIS", False)
      lIIOrlist = oMenu.GetTblInstId("TTORLIST", False)
      lIIOrlisttc = oMenu.GetTblInstId("TTORLISTTC", False)

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlTipoDoc = " "
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMORGSOL_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      If Not oCallParams Is Nothing Then
        If oCallParams.strParam <> "" Then
          '-------------------------------
          oCleGsol.strTipork = Trim(Mid(oCallParams.strParam, 6, 1))
          If Not oCleGsol.ApriOrdlist(DittaCorrente, "", 0, 0, 0, _
                                  IntSetDate("01/01/1900"), IntSetDate("31/12/2099"), _
                                  IntSetDate("01/01/1900"), IntSetDate("31/12/2099"), _
                                  True, True, True, True, True, True, True, _
                                  NTSCInt(Trim(Mid(oCallParams.strParam, 8, 9))), _
                                  dsGsol) Then Return

          If dsGsol.Tables("CORPO").Rows.Count = 0 Then
            oApp.MsgBoxInfo(oApp.Tr(Me, 128810682984844000, "Non esistono dati con queste caratteristiche."))
            oCallParams.strParam = ""
            Return
          End If

          '-------------------------------
          'applico le impostazioni da GCTL ed i valori di default: 
          'la griglia deve gi essere collegata al datatable
          GctlTipoDoc = oCleGsol.strTipork
          If strOldTipork <> oCleGsol.strTipork Then GctlSetRoules()
          strOldTipork = oCleGsol.strTipork
          If oCleGsol.strTipork = "X" Then oCleGsol.bDocEmesso = True Else oCleGsol.bDocEmesso = False

          '----------------------------------------------------
          'ricollego la griglia al datatable di movord
          grRighe.DataSource = Nothing
          '      dcGSol.datasource = Nothing
          dcGsol.DataSource = dsGsol.Tables("CORPO")
          grRighe.DataSource = dcGsol

          SetStato(1)

        End If
        oCallParams.strParam = ""
      End If    'If Not oCallParams Is Nothing Then

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMORGSOL_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If pnGriglia.Visible Then
        grvRighe.NTSMoveFirstRowColunn() 'diversamente in un ordine di produzione appena inserito PERDO gli impegni e le attività collegate se quando esco sono sull'ultima riga (quella bianca)!!!!!
        Me.Cursor = Cursors.WaitCursor
          If Not RecordSalva(False, Nothing) Then e.Cancel = True
      End If
      oMenu.ResetTblInstId("TTORDL", False, lIIOrdl)
      oMenu.ResetTblInstId("TTORLIST", False, lIIOrlist)
      oMenu.ResetTblInstId("TTATTIVIT", False, lIIAttivit)
      oMenu.ResetTblInstId("TTASSRIS", False, lIIAssris)
      oMenu.ResetTblInstId("TTORLISTTC", False, lIIOrlisttc)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Dim frmNuov As FRMORNUOV = Nothing
    Try
      oParApri = Nothing

      frmNuov = CType(NTSNewFormModal("FRMORNUOV"), FRMORNUOV)
      frmNuov.Init(oMenu, Nothing, DittaCorrente)
      frmNuov.ShowDialog()
      If frmNuov.bOk = False Then Return
      If frmNuov.opOF.Checked Then oCleGsol.strTipork = "O"
      If frmNuov.opOP.Checked Then oCleGsol.strTipork = "H"
      If frmNuov.opIT.Checked Then oCleGsol.strTipork = "X"
      If oCleGsol.strTipork = "X" Then oCleGsol.bDocEmesso = True Else oCleGsol.bDocEmesso = False
      dsGsol.Tables("CORPO").Clear() : dsGsol.Tables("CORPO").AcceptChanges()
      If oCleGsol.bModTCO Then
        dsGsol.Tables("CORPOTC").Clear() : dsGsol.Tables("CORPOTC").AcceptChanges()
      End If

      '-------------------------------
      'applico le impostazioni da GCTL ed i valori di default: 
      'la griglia deve gi essere collegata al datatable
      GctlTipoDoc = oCleGsol.strTipork
      If strOldTipork <> oCleGsol.strTipork Then GctlSetRoules()
      strOldTipork = oCleGsol.strTipork

      '----------------------------------------------------
      'ricollego la griglia al datatable di movord
      grRighe.DataSource = Nothing
      '  dcGSol.datasource = Nothing
      dcGsol.DataSource = dsGsol.Tables("CORPO")
      grRighe.DataSource = dcGsol

      oCleGsol.SettaTesta(Nothing)

      SetStato(1)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmNuov Is Nothing Then frmNuov.Dispose()
      frmNuov = Nothing
    End Try
  End Sub
  Public Overridable Sub tlbApri_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApri.ItemClick
    Try
      oParApri = Nothing

      '-------------------------
      'visualizzo la form per la selezione delle proposte d'ordine
      'in oParApri.strPar1 ricevo la stringa di selezione scadenze
      oParApri = New CLE__CLDP
      oParApri.strPar1 = ""
      oParApri.strPar2 = "BNORGSOL:SELDOCU"
      oMenu.RunChild("NTSInformatica", "FRMORSEOL", "", DittaCorrente, "", "BNORSEOL", oParApri, "", True, True)
      If oParApri.strParam = "" Then Return 'ho annullato

      '-------------------------------
      oCleGsol.strTipork = oParApri.strParam.Substring(0, 1)
      If Not oCleGsol.ApriOrdlist(DittaCorrente, oParApri.strPar1, NTSCInt(oParApri.dPar1), NTSCInt(oParApri.dPar2), NTSCInt(oParApri.dPar3), _
                              NTSCDate(oParApri.strPar2).ToShortDateString, NTSCDate(oParApri.strPar3).ToShortDateString, _
                              NTSCDate(oParApri.strPar4).ToShortDateString, NTSCDate(oParApri.strPar5).ToShortDateString, _
                              CBool(IIf(oParApri.strParam.Substring(1, 1) = "S", True, False)), _
                              CBool(IIf(oParApri.strParam.Substring(2, 1) = "S", True, False)), _
                              CBool(IIf(oParApri.strParam.Substring(6, 1) = "S", True, False)), _
                              CBool(IIf(oParApri.strParam.Substring(3, 1) = "S", True, False)), _
                              CBool(IIf(oParApri.strParam.Substring(4, 1) = "S", True, False)), _
                              CBool(IIf(oParApri.strParam.Substring(5, 1) = "S", True, False)), _
                              CBool(IIf(oParApri.strParam.Substring(7, 1) = "S", True, False)), _
                              0, dsGsol, NTSCInt(oParApri.strParam.Substring(8, 1))) Then Return

      If dsGsol.Tables("CORPO").Rows.Count = 0 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128786513591186000, "Non esistono dati con queste caratteristiche."))
        Return
      End If

      '-------------------------------
      'applico le impostazioni da GCTL ed i valori di default: 
      'la griglia deve gi essere collegata al datatable
      GctlTipoDoc = oCleGsol.strTipork
      If strOldTipork <> oCleGsol.strTipork Then GctlSetRoules()
      strOldTipork = oCleGsol.strTipork
      If oCleGsol.strTipork = "X" Then oCleGsol.bDocEmesso = True Else oCleGsol.bDocEmesso = False

      '----------------------------------------------------
      'ricollego la griglia al datatable di movord
      grRighe.DataSource = Nothing
      '   dcGSol.datasource = Nothing
      dcGsol.DataSource = dsGsol.Tables("CORPO")
      bInAssociazioneDatasource = True
      grRighe.DataSource = dcGsol
      bInAssociazioneDatasource = False

      SetStato(1)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Me.Cursor = Cursors.WaitCursor
      If Not RecordSalva(False, Nothing) Then Return

      If grvRighe.NTSGetColumnFilterStatus() Then grvRighe.DisableColumnSortFilter()
      GctlSaveConfigGrid()
      SetStato(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Try
      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128619938292968750, _
                            "Premendo Sì saranno cancellati tutti i dati visualizzati." & vbCrLf & _
                            "(se proposte d'ordine di produzione verrano cancellati anche i relativi impegni)" & vbCrLf & _
                            "Continuare?")) = Windows.Forms.DialogResult.No Then Return
      Me.Cursor = Cursors.WaitCursor
      If Not oCleGsol.RecordCancella(-1) Then Return

      If grvRighe.NTSGetColumnFilterStatus() Then grvRighe.DisableColumnSortFilter()

      SetStato(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbRecordNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordNuovo.ItemClick
    Try
      If grvRighe.NTSGetColumnFilterStatus() Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130670993790266128, "Funzione non disponibile quando Ã¨ presente un raggruppamento o un ordinamento di griglia."))
        Return
      End If

      grvRighe.NTSNuovo()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRecordSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordSalva.ItemClick
    Try
      If Not PosizioneInGrigliaSuIntestazioneGruppo() Then Return

      Me.Cursor = Cursors.WaitCursor
      RecordSalva(False, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub tlbRecordCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordCancella.ItemClick
    Dim dtrDeleted As DataRow = Nothing
    Dim lPos As Integer = grvRighe.FocusedRowHandle
    Try
      If grvRighe.Focused = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222115781250, "Posizionarsi prima nella griglia e selezionare la riga da cancellare"))
        Return
      End If
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then Return
      If Not PosizioneInGrigliaSuIntestazioneGruppo() Then Return

      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128619954807500000, "Confermi la cancellazione della riga?")) = Windows.Forms.DialogResult.No Then Return
      Me.Cursor = Cursors.WaitCursor
      oCleGsol.RecordCancella(NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga))
      dsGsol.Tables("CORPO").AcceptChanges()
      grRighe.Focus()
      If oCleGsol.dttEC.Rows.Count = 0 Then
        SetStato(0)
      Else
        If grvRighe.NTSGetCurrentDataRow Is Nothing Then
          'se ho cancellato l'ultima riga rimango sulla nuova ultima riga
          grvRighe.MoveLast()
        End If
        grvRighe_NTSFocusedRowChanged(Me, Nothing)

        If Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
          'se facevo le seguenti operazioni l'ultima modifca non veniva salvata:
          '- aprire la visualizzazione delle proposte di produzione 
          '- abilitare il sort/filtering in griglia 
          '- ordinare le righe per codice articolo 
          '- modificare la quantità sulla prima riga e freccia in giù
          '- cancellare la seconda riga 
          '- modificare la quantità sulla terza riga (divenuta seconda dopo la cancellazione della riga precedente) e freccia in su
          '- salvare la proposta ed uscire (oppure tornare in stato 0)
          dcGsol.MoveFirst()
          grvRighe.MovePrev()
          grvRighe.MoveNext()
        End If

      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Try
        If lPos >= grvRighe.RowCount Then
          grvRighe.MoveLast()
        Else
          grvRighe.FocusedRowHandle = lPos
        End If
      Catch ex As Exception
        grvRighe.MoveLast()
      End Try
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub tlbRecordRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordRipristina.ItemClick
    Try
      If grvRighe.Focused = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222115937500, "Posizionarsi prima nella griglia e selezionare la riga da ripristinare"))
        Return
      End If
      If Not PosizioneInGrigliaSuIntestazioneGruppo() Then Return

      If Not grvRighe.NTSRipristinaRigaCorrenteBefore(dcGsol, True) Then Return
      If oCleGsol.RecordRipristina(dcGsol.Position, dcGsol.Filter) Then
        grvRighe.NTSRipristinaRigaCorrenteAfter()
      End If
      GestisciGrigliaTCO()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim oParam As New CLE__PATB
    Try
      If Not PosizioneInGrigliaSuIntestazioneGruppo() Then Return

      If grvRighe.FocusedColumn.Equals(xxo_lottox) Then
        '------------------------------------
        'zoom lotti
        If NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart)).Trim = "" Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128786513676830000, "Indicare prima il codice articolo"))
          Return
        End If
        NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
        oParam.strTipo = NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart))
        'oParam.nMagaz = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_magaz))   'serve per visual solo i lotti aperti
        oParam.nAnno = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_fase))     'serve per visual solo i lotti aperti
        'oParam.strDatreg = NTSCDate(edet_datdoc.Text).ToShortDateString                          'serve per visual solo i lotti aperti
        NTSZOOM.ZoomStrIn("ZOOMANALOTTI", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvRighe.EditingValue) Then grvRighe.SetFocusedValue(NTSZOOM.strIn)

      ElseIf grvRighe.FocusedColumn.Equals(ec_fase) Then
        '------------------------------------
        'zoom fasi articoli
        SetFastZoom(NTSCStr(grvRighe.EditingValue), oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
        oParam.strTipo = NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart))
        NTSZOOM.ZoomStrIn("ZOOMARTFASI", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvRighe.EditingValue) Then grvRighe.SetFocusedValue(NTSZOOM.strIn)

      ElseIf grvRighe.FocusedColumn.Equals(ec_subcommeca) Then
        '------------------------------------
        'zoom sottocommesse
        If NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_commeca)) = 0 Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128038277237865342, "Indicare prima il codice commessa"))
          Return
        End If
        SetFastZoom(NTSCStr(grvRighe.EditingValue), oParam)    'abilito la gestione dello zoom veloce
        oParam.lCommessa = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_commeca))   'passo il codice commessa
        NTSZOOM.strIn = ""
        NTSZOOM.ZoomStrIn("ZOOMSUBCOMM", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvRighe.EditingValue) And NTSZOOM.strIn <> "" Then grvRighe.SetFocusedValue(NTSZOOM.strIn)

      ElseIf grvRighe.FocusedColumn.Equals(ec_pmtaskid) Then
        '------------------------------------
        'zoom Project management task ID
        SetFastZoom(NTSCStr(grvRighe.EditingValue), oParam)
        NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)

        'parametri per lo zoom
        Dim lInstid As Integer = 0
        Dim lCodCommessa As Integer = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_commeca)
        Dim bAttNonCompletate As Boolean = True
        Dim bSoloProgDate As Boolean = False
        '
        Dim bSoloUltimoLiv As Boolean = True
        Dim strTipoTask As String = ""
        Dim bSoloRilasciati As Boolean = False
        Dim strCodart As String = IIf(NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart) = "D", "", NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart)).ToString
        Dim lCodConto As Integer = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_contocontr)
        Dim lOperaio As Integer = 0
        Dim nCodlavo As Integer = 0
        Dim nCodCentroLav As Integer = 0
        Dim nCodCentroCa As Integer = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_codcena)
        Dim lCodForni As Integer = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_conto)
        Dim nNLayout As Integer = 4
        Dim lInstidPrevent As Integer = 0

        oParam.nTipologia = lInstid
        oParam.lCommessa = lCodCommessa

        oParam.bStanziamenti = bAttNonCompletate
        oParam.bLiv2 = bSoloProgDate
        If oCleGsol.bModPM Then
          oParam.strTipo = NTSCStr(bSoloUltimoLiv) & ";" & strTipoTask & ";" & NTSCStr(bSoloRilasciati) & ";" & _
                           strCodart & ";" & NTSCStr(lCodConto) & ";" & NTSCStr(lOperaio) & ";" & NTSCStr(nCodlavo) & ";" & _
                           NTSCStr(nCodCentroLav) & ";" & NTSCStr(nCodCentroCa) & ";" & NTSCStr(lCodForni) & ";" & _
                           NTSCStr(nNLayout) & ";" & NTSCStr(lInstidPrevent)
        Else
          oParam.strTipo = NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_subcommeca)
        End If
        NTSZOOM.ZoomStrIn("ZOOMTASK", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvRighe.EditingValue) Then grvRighe.SetFocusedValue(NTSZOOM.strIn)

      ElseIf grvRighe.FocusedColumn.Equals(ec_codart) Then
        NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
        oParam.bTipoProposto = False       'abilito la possibilitÃ  di selezionare + articoli
        NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oParam)
        If NTSZOOM.strIn = "*" Then
          'zoom multiselezione
          If Not oParam.oParam Is Nothing Then
            If CType(oParam.oParam, DataTable).Rows.Count > 0 Then
              'il primo articolo selezionato lo metto nella riga su cui sono, gli altri li accodo
              If NTSCStr(CType(oParam.oParam, DataTable).Rows(0)!codart) <> NTSCStr(grvRighe.GetFocusedValue) Then
                grvRighe.SetFocusedValue(NTSCStr(CType(oParam.oParam, DataTable).Rows(0)!codart))
                If CType(oParam.oParam, DataTable).Rows.Count > 1 Then
                  If NTSCInt(grvRighe.NTSGetCurrentDataRow!xxo_codtagl) = 0 Then
                    grvRighe.NTSGetCurrentDataRow!ec_quant = 1
                  End If
                End If
              End If
              CType(oParam.oParam, DataTable).Rows(0).Delete()
              CType(oParam.oParam, DataTable).AcceptChanges()
              If CType(oParam.oParam, DataTable).Rows.Count > 0 Then
                oCleGsol.bInInsertArticoDaZoom = True
                If Not RecordSalva(False, Nothing) Then Return
                For Each dtrT As DataRow In CType(oParam.oParam, DataTable).Rows
                  oCleGsol.dttEC.Rows.Add(oCleGsol.dttEC.NewRow)
                  With oCleGsol.dttEC.Rows(oCleGsol.dttEC.Rows.Count - 1)
                    'forzo la MovordOnAddNewRow
                    !codditt = "."
                    !codditt = DittaCorrente
                    !ec_codart = dtrT!codart.ToString
                    'imposto sempre la qta = 1 (altrimenti in carichi da prod e/o articoli conai quando in seguito al cambio della qta dovrÃ² riproporzionalizzare gli scarichi/dettaglio conai non lavorerebbe correttamente)
                    'solo se non Ã¨ un artoclo TCO, altrimenti non compilerebbe correttamente il dettaglio TCO
                    If NTSCInt(!xxo_codtagl) = 0 Then
                      !ec_quant = 1
                    End If
                  End With
                  'Devo passare la riga aggiunta, altrimenti aggiornerebbe la riga selezionata.
                  If Not oCleGsol.RecordSalva(oCleGsol.dttEC.Rows(oCleGsol.dttEC.Rows.Count - 1), False, Nothing) Then
                    oCleGsol.dttEC.Rows(oCleGsol.dttEC.Rows.Count - 1).Delete()
                  End If
                Next
                oCleGsol.bInInsertArticoDaZoom = False
              End If
              CType(oParam.oParam, DataTable).Clear()
            End If
          End If
        Else
          If NTSZOOM.strIn <> NTSCStr(grvRighe.EditingValue) Then grvRighe.SetFocusedValue(NTSZOOM.strIn)
        End If

      Else
        'zoom standard
        Dim ctrlTmp As Control = NTSFindControlForZoom()
        If ctrlTmp Is Nothing Then Return

        '------------------------------------
        'zoom standard di textbox e griglia
        NTSCallStandardZoom()
      End If
      grRighe.Focus()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbZoomFornPrz_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoomFornPrz.ItemClick
    Dim oParam As New CLE__PATB
    Dim strT() As String = Nothing
    Dim i As Integer = 0
    Try
      If NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart)).Trim = "" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128569627222968750, "Indicare prima il codice articolo"))
        Return
      End If

      If oCleGsol.strTipork = "H" And oCleGsol.bTerzista = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128625949012968750, "Funzione non utilizzabile negli ordini di produzione INTERNI"))
        Return
      End If
      If Not PosizioneInGrigliaSuIntestazioneGruppo() Then Return

      oParam.strCodart = NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart)).Trim
      oParam.strDatreg = NTSCDate(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_datord)).ToShortDateString
      'codice lavorazione
      oParam.nTipologia = 0
      If oCleGsol.dttATTIVIT.Rows.Count > 0 And oCleGsol.strTipork = "H" And oCleGsol.bTerzista Then
        oParam.nTipologia = NTSCInt(oCleGsol.dttATTIVIT.Select("", "at_fase ASC")(0)!at_codlavo)
      End If

      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMLISTINIHLFP", DittaCorrente, oParam)
      If NTSZOOM.strIn <> "" Then
        oCleGsol.bCambioFornitorePrezzi = True
        grvRighe.NTSGetCurrentDataRow!ec_conto = oParam.lContoCF
        oCleGsol.bCambioFornitorePrezzi = True
        grvRighe.NTSGetCurrentDataRow!ec_unmis = oParam.strDescr
        grvRighe.NTSGetCurrentDataRow!ec_codvalu = oParam.nValuta
        If oParam.nValuta <> 0 Then
          grvRighe.NTSGetCurrentDataRow!ec_prezvalc = oParam.dImporto
        Else
          grvRighe.NTSGetCurrentDataRow!ec_prezzo = oParam.dImporto
        End If
        strT = oParam.strOut.Split(";"c)
        For i = 0 To strT.Length - 1
          grvRighe.NTSGetCurrentDataRow("ec_scont" & (i + 1).ToString) = NTSCDec(strT(i))
        Next
      End If

      grRighe.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbMrp_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbMrp.ItemClick
    Dim strParam As String = ""
    Try
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128786513737982000, "Per poter effettuare l'operazione richiesta è necessario " & vbCrLf & _
                                                        "posizionarsi su di una riga con articolo impostato."))
        Return
      End If
      If NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart).Trim = "" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128619874261250000, "Per poter effettuare l'operazione richiesta è necessario " & vbCrLf & _
                                                        "posizionarsi su di una riga con articolo impostato."))
        Return
      End If
      If Not PosizioneInGrigliaSuIntestazioneGruppo() Then Return

      strParam = "P;" & oCleGsol.dttET.Rows(0)!et_tipork.ToString & ";" & _
                Microsoft.VisualBasic.Right(NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga).ToString.PadLeft(9, "0"c), 9) & ";" & _
                grvRighe.NTSGetCurrentDataRow!ec_codart.ToString.PadRight(CLN__STD.CodartMaxLen).Substring(0, CLN__STD.CodartMaxLen) & ";" & _
                Microsoft.VisualBasic.Right(NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_fase).ToString.PadLeft(4, "0"c), 4)
      oMenu.RunChild("BSDBNMRP", "CLSDBNMRP", oApp.Tr(Me, 128554247234454937, "Navigazione MRP"), DittaCorrente, "", "", Nothing, strParam, True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbImpegni_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpegni.ItemClick
    Dim frmImpe As FRMORIMP1 = Nothing
    Dim ds As New DataSet
    Dim i As Integer = 0

    Try
      frmImpe = CType(NTSNewFormModal("FRMORIMP1"), FRMORIMP1)

      If Not PosizioneInGrigliaSuIntestazioneGruppo() Then Return

      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      If Not RecordSalva(False, Nothing) Then Return
      If Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
        If Not oCleGsol.RecordSalva(dcGsol.Position, False, Nothing) Then Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCleGsol.dttET.Rows(0)!et_tipork.ToString.ToUpper <> "H" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128786513813954000, "Funzione disponibile solo per 'Ordini di produzione'"))
        Return
      End If
      If Not grRighe.Focused Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128786513870738000, "Posizionarsi prima su una riga di griglia"))
        Return
      End If
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128786513966678000, "Posizionarsi prima su una riga di griglia con codice articolo impostato"))
        Return
      End If
      If grvRighe.NTSGetCurrentDataRow!ec_codart.ToString.Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128786513981654000, "Posizionarsi prima su una riga di griglia con codice articolo impostato"))
        Return
      End If

      Me.Cursor = Cursors.WaitCursor

      '-------------------------------
      'tengo un puntatore alla riga H padre delle righe Y, inoltr eavviso che da questo momento la before/aftercolupdate sono riferite alle righe Y, non H
      oCleGsol.dtrHT = grvRighe.NTSGetCurrentDataRow

      '-------------------------------
      'clono la tabella perchè non devo far vedere gli altri impegni collegati
      ds.Tables.Add(dsGsol.Tables("CORPOIMP").Clone())
      For i = 0 To dsGsol.Tables("CORPOIMP").Rows.Count - 1
        If NTSCInt(dsGsol.Tables("CORPOIMP").Rows(i)!ec_rigaor) = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga) Then
          ds.Tables("CORPOIMP").ImportRow(dsGsol.Tables("CORPOIMP").Rows(i))
          dsGsol.Tables("CORPOIMP").Rows(i).Delete()
        End If
      Next
      dsGsol.Tables("CORPOIMP").AcceptChanges()
      ds.Tables.Add(dsGsol.Tables("CORPOIMPTC").Clone())
      For i = 0 To dsGsol.Tables("CORPOIMPTC").Rows.Count - 1
        ds.Tables("CORPOIMPTC").ImportRow(dsGsol.Tables("CORPOIMPTC").Rows(i))
        dsGsol.Tables("CORPOIMPTC").Rows(i).Delete()
      Next
      dsGsol.Tables("CORPOIMPTC").AcceptChanges()

      frmImpe.InitEntity(oMenu, oCleGsol, ds)
      frmImpe.ShowDialog()
      grvRighe.Focus()
      oCleGsol.bRiscriviImpegni = True

      '-------------------------------
      'riacquisisco gli impegni aggiornati
      For i = 0 To ds.Tables("CORPOIMP").Rows.Count - 1
        If ds.Tables("CORPOIMP").Rows(i).RowState <> DataRowState.Deleted Then
          If NTSCInt(ds.Tables("CORPOIMP").Rows(i)!ec_riga) > 0 Then
            dsGsol.Tables("CORPOIMP").ImportRow(ds.Tables("CORPOIMP").Rows(i))
          Else
            ds.Tables("CORPOIMP").Rows(i).Delete()
          End If
        End If
      Next

      For i = 0 To ds.Tables("CORPOIMPTC").Rows.Count - 1
        If ds.Tables("CORPOIMPTC").Rows(i).RowState <> DataRowState.Deleted Then
          If NTSCInt(ds.Tables("CORPOIMPTC").Rows(i)!ec_riga) > 0 Then
            dsGsol.Tables("CORPOIMPTC").ImportRow(ds.Tables("CORPOIMPTC").Rows(i))
          Else
            ds.Tables("CORPOIMPTC").Rows(i).Delete()
          End If
        End If
      Next

      ds.Tables.Clear()
      dsGsol.Tables("CORPOIMP").AcceptChanges()
      dsGsol.Tables("CORPOIMPTC").AcceptChanges()
      oCleGsol.bHasChangesET = True

      '-------------------------------
      'Rivalorizza la riga
      oCleGsol.ValorizzaProduzione(grvRighe.NTSGetCurrentDataRow)
      oCleGsol.SettaValoriRiga(grvRighe.NTSGetCurrentDataRow)
      If grvRighe.NTSGetCurrentDataRow.RowState <> DataRowState.Unchanged Then
        Me.Cursor = Cursors.WaitCursor
        If Not RecordSalva(False, Nothing) Then Return
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmImpe Is Nothing Then frmImpe.Dispose()
      frmImpe = Nothing
      Me.Cursor = Cursors.Default
      oCleGsol.dtrHT = Nothing
    End Try
  End Sub
  Public Overridable Sub tlbLavorazioni_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbLavorazioni.ItemClick
    Dim frmTtat As FRMORTTA1 = Nothing
    Dim ds As New DataSet
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim dtrT1() As DataRow = Nothing
    Dim dtrAttivit() As DataRow = Nothing

    Try
      frmTtat = CType(NTSNewFormModal("FRMORTTA1"), FRMORTTA1)
      If Not PosizioneInGrigliaSuIntestazioneGruppo() Then Return
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      If Not RecordSalva(False, Nothing) Then Return
      If Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
        If Not oCleGsol.RecordSalva(dcGsol.Position, False, Nothing) Then Return
      End If

      If oCleGsol.dttET.Rows(0)!et_tipork.ToString.ToUpper <> "H" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128586969528593750, "Funzione disponibile solo per 'Ordini di produzione'"))
        Return
      End If
      If Not grRighe.Focused Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128578169086718750, "Posizionarsi prima su una riga di griglia"))
        Return
      End If
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128786513995850000, "Posizionarsi prima su una riga di griglia con codice articolo impostato"))
        Return
      End If
      If grvRighe.NTSGetCurrentDataRow!ec_codart.ToString.Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128586969062500000, "Posizionarsi prima su una riga di griglia con codice articolo impostato"))
        Return
      End If

      Me.Cursor = Cursors.WaitCursor

      '-------------------------------
      'tengo un puntatore alla riga H padre delle righe Y, inoltr eavviso che da questo momento la before/aftercolupdate sono riferite alle righe Y, non H
      oCleGsol.dtrHT = grvRighe.NTSGetCurrentDataRow

      '-------------------------------
      'clono la tabella perchè non devo far vedere le altre lavorazioni collegate
      'aggiungo alla tabella anche le colonne di assris, per fare in modo che le due tabelle vengano gestite come se fossero una
      dsGsol.Tables("ATTIVIT").AcceptChanges()
      dsGsol.Tables("ASSRIS").AcceptChanges()
      ds.Tables.Add(dsGsol.Tables("ATTIVIT").Clone())
      For i = 0 To dsGsol.Tables("ASSRIS").Columns.Count - 1
        If dsGsol.Tables("ASSRIS").Columns(i).ColumnName.ToLower <> "codditt" And dsGsol.Tables("ASSRIS").Columns(i).ColumnName.ToLower <> "ts" Then
          ds.Tables("ATTIVIT").Columns.Add(dsGsol.Tables("ASSRIS").Columns(i).ColumnName, dsGsol.Tables("ASSRIS").Columns(i).DataType)
          ds.Tables("ATTIVIT").Columns(dsGsol.Tables("ASSRIS").Columns(i).ColumnName).DefaultValue = dsGsol.Tables("ASSRIS").Columns(i).DefaultValue
        End If
      Next
      'importo i dati dalle due tabelle
      dtrAttivit = dsGsol.Tables("ATTIVIT").Select("at_riga = " & NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga).ToString, "at_fase")
      For i = 0 To dtrAttivit.Length - 1
        'carico attivit
        ds.Tables("ATTIVIT").ImportRow(dtrAttivit(i))
        dtrT1 = ds.Tables("ATTIVIT").Select("at_riga = " & dtrAttivit(i)!at_riga.ToString & _
                                            " AND at_fase = " & dtrAttivit(i)!at_fase.ToString)
        'devo aggiungere i valori di assris
        dtrT = dsGsol.Tables("ASSRIS").Select("as_riga = " & dtrT1(0)!at_riga.ToString & _
                                              " AND as_fase = " & dtrT1(0)!at_fase.ToString)
        If dtrT.Length > 0 Then
          For l = 0 To dsGsol.Tables("ASSRIS").Columns.Count - 1
            If dsGsol.Tables("ASSRIS").Columns(l).ColumnName.ToLower <> "codditt" And dsGsol.Tables("ASSRIS").Columns(l).ColumnName.ToLower <> "ts" Then
              dtrT1(0)(dsGsol.Tables("ASSRIS").Columns(l).ColumnName) = dtrT(0)(dsGsol.Tables("ASSRIS").Columns(l).ColumnName)
            End If
          Next
          dtrT(0).Delete()
        End If
        dtrAttivit(i).Delete()
      Next
      dsGsol.Tables("ATTIVIT").AcceptChanges()
      dsGsol.Tables("ASSRIS").AcceptChanges()
      ds.Tables("ATTIVIT").AcceptChanges()

      frmTtat.InitEntity(oMenu, CType(oCleGsol, CLEMGDOCU), ds)
      frmTtat.ShowDialog()
      grvRighe.Focus()
      oCleGsol.bRiscriviImpegni = True        'si potrebbe riscrivere solo se sono state fatte modifiche ...

      '-------------------------------
      'riacquisisco gli impegni aggiornati
      For i = 0 To ds.Tables("ATTIVIT").Rows.Count - 1
        If ds.Tables("ATTIVIT").Rows(i).RowState <> DataRowState.Deleted Then
          If NTSCInt(ds.Tables("ATTIVIT").Rows(i)!at_riga) > 0 Then
            dsGsol.Tables("ATTIVIT").ImportRow(ds.Tables("ATTIVIT").Rows(i))
            'devo importare anche assris, se settata
            If NTSCInt(ds.Tables("ATTIVIT").Rows(i)!as_codcent) <> 0 Then
              dsGsol.Tables("ASSRIS").ImportRow(ds.Tables("ATTIVIT").Rows(i))
            End If
          Else
            ds.Tables("ATTIVIT").Rows(i).Delete()
          End If
        End If
      Next
      ds.Tables.Clear()
      dsGsol.Tables("ATTIVIT").AcceptChanges()
      dsGsol.Tables("ASSRIS").AcceptChanges()
      oCleGsol.bHasChangesET = True

      '-------------------------------
      'Rivalorizza la riga
      oCleGsol.ValorizzaProduzione(grvRighe.NTSGetCurrentDataRow)
      oCleGsol.SettaValoriRiga(grvRighe.NTSGetCurrentDataRow)
      If grvRighe.NTSGetCurrentDataRow.RowState <> DataRowState.Unchanged Then
        Me.Cursor = Cursors.WaitCursor
        If Not RecordSalva(False, Nothing) Then Return
      End If

    Catch ex As Exception
      dsGsol.Tables("ATTIVIT").RejectChanges()
      dsGsol.Tables("ASSRIS").RejectChanges()
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmTtat Is Nothing Then frmTtat.Dispose()
      frmTtat = Nothing
      Me.Cursor = Cursors.Default
      oCleGsol.dtrHT = Nothing
    End Try
  End Sub
  Public Overridable Sub tlbMovimenti_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbMovimenti.ItemClick
    Dim strParam As String = ""
    Try
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128994809905034434, "Per poter effettuare l'operazione richiesta è necessario " & vbCrLf & _
                                                        "posizionarsi su di una riga con articolo e magazzino impostati."))
        Return
      End If
      If NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart).Trim = "" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128786514119714000, "Per poter effettuare l'operazione richiesta è necessario " & vbCrLf & _
                                                        "posizionarsi su di una riga con articolo e magazzino impostati."))
        Return
      End If
      If NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_magaz) = 0 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128619874726093750, "Per poter effettuare l'operazione richiesta è necessario " & vbCrLf & _
                                                        "posizionarsi su di una riga con articolo e magazzino impostati."))
        Return
      End If

      If Not PosizioneInGrigliaSuIntestazioneGruppo() Then Return

      strParam = "APRI:" & _
                 grvRighe.NTSGetCurrentDataRow!ec_codart.ToString.PadRight(CLN__STD.CodartMaxLen).Substring(0, CLN__STD.CodartMaxLen) & ";" & _
                 Microsoft.VisualBasic.Right(NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_magaz).ToString.PadLeft(4, "0"c), 4) & ";" & _
                 "000000000" & ";A"
      oMenu.RunChild("BSMGSCHE", "CLSMGSCHE", oApp.Tr(Me, 128619875234531250, "STAMPA SCHEDE ARTICOLI"), DittaCorrente, "", "", Nothing, strParam, True, True)


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbDettTco_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDettTco.ItemClick
    Dim dTmp As Decimal = 0
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim frmDqta As FRMORDQT2 = Nothing
    Try
      frmDqta = CType(NTSNewFormModal("FRMORDQT2"), FRMORDQT2)
      If oCleGsol.bModTCO = False Then Return
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then Return
      oCleGsol.GetMemDttArti(grvRighe.NTSGetCurrentDataRow!ec_codart.ToString)
      If oCleGsol.dttArti.Rows.Count = 0 Then Return

      If NTSCInt(oCleGsol.dttArti.Rows(0)!ar_codtagl) <> 0 Then
        If grvRighe.FocusedColumn.Name = "ec_quant" Then
          'mi sposto, altrimenti all'uscita da frmDqta il focus ritorna sulla colonna ec_quant e si riapre la form
          grvRighe.NTSMoveNextColunn()
          'grvRighe.FocusedColumn = ec_ump
        End If
        frmDqta.Init(oMenu, Nothing, DittaCorrente, Nothing)
        frmDqta.InitEntity(oCleGsol, grvRighe.NTSGetCurrentDataRow)
        frmDqta.dsGsor = dsGsol
        frmDqta.bVisDoc = False
        If grvRighe.NTSGetCurrentDataRow.RowState = DataRowState.Added Then frmDqta.bNewRow = True
        frmDqta.ShowDialog()

        '-----------------------
        'aggiorno il totale quantità
        dtrT = dsGsol.Tables("CORPOTC").Select("ec_riga = " & NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga).ToString)
        For i = 1 To 24
          dTmp += NTSCDec(dtrT(0)("ec_quant" & i.ToString.PadLeft(2, "0"c)))
        Next
        grvRighe.NTSGetCurrentDataRow!ec_quant = dTmp

        GestisciGrigliaTCO()
      Else
        oApp.MsgBoxErr(oApp.Tr(Me, 128716713542669829, "Articolo non gestito a Taglie e Colori"))
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If Not frmDqta Is Nothing Then frmDqta.Dispose()
      frmDqta = Nothing
    End Try

  End Sub
  Public Overridable Sub tlbProgressivi_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbProgressivi.ItemClick
    Dim oPar As New CLE__CLDP

    Try
      '--------------------------------------------------------------------------------------------------------------
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128994809974014394, "Attenzione!" & vbCrLf & _
          "Indicare un codice articolo valido prima di visualizzare i progressivi relativi."))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart).Trim = "" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128994807374108869, "Attenzione!" & vbCrLf & _
          "Indicare un codice articolo valido prima di visualizzare i progressivi relativi."))
        Return
      End If
      If Not PosizioneInGrigliaSuIntestazioneGruppo() Then Return
      '--------------------------------------------------------------------------------------------------------------
      oPar.strPar1 = "BNORGSOL"
      oPar.strPar2 = NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart)
      oPar.dPar1 = 0
      oMenu.RunChild("NTSInformatica", "FRMMGHLAP", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbConfermatutto_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbConfermatutto.ItemClick
    Try
      SettaConfermato(True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub tlbConfermaselez_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbConfermaselez.ItemClick
    Try
      SettaConfermato(False)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub tlbGeneraOrdini_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGeneraOrdini.ItemClick
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Dim strElenco As String = ""
    Dim strParam As String = ""
    Try
      Me.Cursor = Cursors.WaitCursor
      Me.ValidaLastControl()
 If Not RecordSalva(False, Nothing) Then Return

      '-------------------------------
      dtrT = dsGsol.Tables("CORPO").Select("xxo_seleziona = 'S'")
      If dtrT.Length = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128619879168125000, "Selezionare almeno una riga prima di passare alla generazione degli ordini."))
        Return
      End If

      If dtrT.Length > 1000 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128619879823125000, "Non è possibile selezionare più di mille righe." & vbCrLf & _
                                                       "Generazione Ordini da selezione Proposte d'ordine annullata."))
        Return
      End If

      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128619880236875000, "Confermare la generazione degli Ordini dalle Proposte d'Ordine selezionate?")) = Windows.Forms.DialogResult.No Then Return

      '-------------------------------
      For i = 0 To dtrT.Length - 1
        strElenco += ", " & NTSCInt(dtrT(i)!ec_riga).ToString
      Next
      strElenco = strElenco.Substring(2).Trim

      strParam = "ELAB;" & oCleGsol.strTipork & ";" & strElenco & ";"
      oMenu.RunChild("BSORGNOR", "CLSORGNOR", oApp.Tr(Me, 128619881894843750, "Generazione Ordini da proposte d'Ordine"), DittaCorrente, "", "", Nothing, strParam, True, True)

      '-------------------------------
      RicaricaGriglia()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub tlbVisDetTco_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbVisDetTco.ItemClick
    Dim frmDqta As FRMORDQT2 = Nothing
    Try
      frmDqta = CType(NTSNewFormModal("FRMORDQT2"), FRMORDQT2)
      Me.Cursor = Cursors.WaitCursor
        If Not RecordSalva(False, Nothing) Then Return
      If oCleGsol.dttEC.Select("xxo_codtagl <> 0").Length = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128619912560937500, "Nella griglia non sono presenti articoli di tipo 'Taglia e colore'"))
        Return
      End If

      frmDqta.Init(oMenu, Nothing, DittaCorrente, Nothing)
      frmDqta.InitEntity(oCleGsol, Nothing)
      frmDqta.dsGsor = dsGsol
      frmDqta.bVisDoc = True
      frmDqta.ShowDialog()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If Not frmDqta Is Nothing Then frmDqta.Dispose()
      frmDqta = Nothing
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub tlbRicalcolaCollidaQta_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRicalcolaCollidaQta.ItemClick
    Dim strErrore As String = ""
    Dim dQuant As Decimal = 0
    Try
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then Return
      If NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart).Trim = "" Then Return

      If Not PosizioneInGrigliaSuIntestazioneGruppo() Then Return

      '-----------------------------
      With grvRighe.NTSGetCurrentDataRow
        If NTSCStr(!ec_codart) = "D" Or NTSCStr(!ec_codart) = "M" Then
          oCleGsol.bInValidazQuant = True      'altrimenti al cambio dei colli viene ricalcolata la quantità
          grvRighe.NTSGetCurrentDataRow!ec_colli = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_quant)
          oCleGsol.bInValidazQuant = False
          Return
        End If

        If CType(oCleGsol.oCleComm, CLELBMENU).ConvQuantUM(DittaCorrente, !ec_codart.ToString, !ec_ump.ToString, _
                  NTSCDec(!ec_quant), NTSCDec(!ec_misura1), NTSCDec(!ec_misura2), _
                  NTSCDec(!ec_misura3), !ec_unmis.ToString, dQuant, strErrore, oApp.NDecQta) Then
          oCleGsol.bInValidazQuant = True      'altrimenti al cambio dei colli viene ricalcolata la quantità
          !ec_colli = dQuant
          oCleGsol.bInValidazQuant = False
          If strErrore <> "" Then oApp.MsgBoxErr(strErrore)
        End If

        If !ec_umprz.ToString = "S" Then     'ricalcolo il prezzo se unità di misura su colli
          'devo farlo perchè altrimenti il prezzo viene ricalcolato solo al cambio della quantità
          'ma in questo caso la qta non cambia perchè 'bInValidazQuant = True'
          oCleGsol.SettaPrezzoGsol(grvRighe.NTSGetCurrentDataRow)
          oCleGsol.SettaValoriRiga(grvRighe.NTSGetCurrentDataRow)
        End If
      End With

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      Me.Cursor = Cursors.WaitCursor
    If Not RecordSalva(False, Nothing) Then Return
      Stampa(1, True, True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      Me.Cursor = Cursors.WaitCursor
      If Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
        If Not oCleGsol.RecordSalva(dcGsol.Position, False, Nothing) Then Return
      End If
      Stampa(0, True, True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub tblPrnImpCarta_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tblPrnImpCarta.ItemClick
    Try
      Me.Cursor = Cursors.WaitCursor
       If Not RecordSalva(False, Nothing) Then Return
      Stampa(0, True, False)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub tblPrnImpVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tblPrnImpVideo.ItemClick
    Try
      Me.Cursor = Cursors.WaitCursor
      If Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
        If Not oCleGsol.RecordSalva(dcGsol.Position, False, Nothing) Then Return
      End If
      Stampa(0, True, False)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub tblPrnAttCarta_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tblPrnAttCarta.ItemClick
    Try
      If Not PosizioneInGrigliaSuIntestazioneGruppo() Then Return
      Me.Cursor = Cursors.WaitCursor
      If Not RecordSalva(False, Nothing) Then Return

      Stampa(0, False, True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub tblPrnAttVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tblPrnAttVideo.ItemClick
    Try
      If Not PosizioneInGrigliaSuIntestazioneGruppo() Then Return
      Me.Cursor = Cursors.WaitCursor
      If Not RecordSalva(False, Nothing) Then Return
      Stampa(0, False, True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
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

  Public Overridable Sub tlbSelezionaTutto_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSelezionaTutto.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      SelDesRighe("S")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub tlbDeselezionaTutto_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDeselezionaTutto.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      SelDesRighe("N")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub tlbInvertiSelezione_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbInvertiSelezione.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      SelDesRighe("I")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub tlbCancRigheSel_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancRigheSel.ItemClick
    Dim i As Integer
    Try
      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 130390078404355233, "Confermi la cancellazione delle righe selezionate?")) = Windows.Forms.DialogResult.No Then Return
      For i = dsGsol.Tables("CORPO").Rows.Count - 1 To 0 Step -1
        With dsGsol.Tables("CORPO").Rows(i)
          If NTSCStr(!xxo_seleziona) = "S" Then oCleGsol.RecordCancella(NTSCInt(!ec_riga))
        End With
      Next

      dsGsol.Tables("CORPO").AcceptChanges()
      grRighe.Focus()
      If oCleGsol.dttEC.Rows.Count = 0 Then
        SetStato(0)
      Else
        If grvRighe.NTSGetCurrentDataRow Is Nothing Then
          'se ho cancellato l'ultima riga rimango sulla nuova ultima riga
          grvRighe.MoveLast()
        End If
        grvRighe_NTSFocusedRowChanged(Me, Nothing)

        If Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
          'se facevo le seguenti operazioni l'ultima modifca non veniva salvata:
          '- aprire la visualizzazione delle proposte di produzione 
          '- abilitare il sort/filtering in griglia 
          '- ordinare le righe per codice articolo 
          '- modificare la quantità sulla prima riga e freccia in giù
          '- cancellare la seconda riga 
          '- modificare la quantità sulla terza riga (divenuta seconda dopo la cancellazione della riga precedente) e freccia in su
          '- salvare la proposta ed uscire (oppure tornare in stato 0)
          dcGsol.MoveFirst()
          grvRighe.MovePrev()
          grvRighe.MoveNext()
        End If
      End If

    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

#End Region

#Region "Eventi di Griglia"
  Public Overridable Sub grRighe_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grRighe.Enter
    Try
      If Not grvRighe.NTSGetCurrentDataRow Is Nothing Then oCleGsol.SettaTesta(grvRighe.NTSGetCurrentDataRow)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grRighe_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grRighe.Leave
    Try
      Me.Cursor = Cursors.WaitCursor
       If Not RecordSalva(False, Nothing) Then grRighe.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub grvRighe_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grvRighe.KeyDown
    Try
      If grvRighe.FocusedColumn.Name = "ec_cambio" Then
        If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Left And e.KeyCode <> Keys.Right And _
           e.KeyCode <> Keys.Up And e.KeyCode <> Keys.Down And e.KeyCode <> Keys.PageUp And _
           e.KeyCode <> Keys.PageDown And e.KeyCode <> Keys.Tab Then
          bCambioDelCambio = True
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub grvRighe_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvRighe.NTSBeforeRowUpdate
    Try
      If bInAssociazioneDatasource Then Return
      Me.Cursor = Cursors.WaitCursor
      '--------------------
      'se serve aggiorno il cambio nella tabella
      If grvRighe.FocusedColumn.Name = "ec_cambio" Then
        Dim dtrT As DataRow = oCleGsol.dttEC.Rows(dcGsol.Position)
        If NTSCDec(dtrT!ec_cambio) <> 0 And _
           NTSCInt(dtrT!ec_codvalu) <> 0 And bCambioDelCambio Then
          If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128786514171974000, "Inserire il cambio indicato nella tabella dei CAMBI per la valuta '|" & NTSCInt(dtrT!ec_codvalu) & "|' per la data |" & NTSCDate(grvRighe.NTSGetCurrentDataRow!ec_datord).ToShortDateString & "|")) = Windows.Forms.DialogResult.Yes Then
            oCleGsol.AggiornaCambio(NTSCInt(dtrT!ec_codvalu), NTSCDate(dtrT!ec_datord).ToShortDateString, NTSCDec(dtrT!ec_cambio), True)
          End If
        End If
      End If

    If Not RecordSalva(False, Nothing) Then e.Allow = False

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub grvRighe_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvRighe.NTSFocusedRowChanged
    '-------------------------------------------------
    'carico le unità di misura al cambio di riga
    Dim strTmp As String = ""

    Try
      If dsGsol Is Nothing Then Return
      If dsGsol.Tables("CORPO") Is Nothing Then Return
      If dsGsol.Tables("CORPO").Rows.Count = 0 Then Return

      GctlSetVisEnab(ec_codvalu, False)   'sblocco sempre, evenutalmente blocco dopo quando serve

      If grvRighe.GetFocusedRowCellValue(ec_riga) Is Nothing Then Return

      '-------------------------------------------------------
      'se sono su una nuova riga mi posiziono a sinistra
      If NTSCStr(grvRighe.GetFocusedRowCellValue(ec_riga)) = "" And grvRighe.NTSGetCurrentDataRow Is Nothing Then
        grvRighe.LeftCoord = 0
        grvRighe.FocusedColumn = ec_codart
      End If

      '-------------------------------------------------
      'visualizzo o meno la griglia TCO
      GestisciGrigliaTCO()

      If grvRighe.GetFocusedRowCellValue(ec_codart).ToString.Trim = "" Then Return

      '-------------------------------------------------
      'se l'articolo  gestito a taglia e colore blocco la colonna quantità
      If NTSCInt(grvRighe.GetFocusedRowCellValue(xxo_codtagl).ToString.Trim) <> 0 Then
        ec_quant.Enabled = False
      Else
        GctlSetVisEnab(ec_quant, False)
      End If

      '-------------------------------------------------
      GrvRighe_RowColChange()

      oCleGsol.SvuotaTemporaneiImpegni()
      If Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oCleGsol.SettaTesta(grvRighe.NTSGetCurrentDataRow)
        '-------------------------------------------------
        'carico impegni collegati se prop d'ordine di prod.
        If oCleGsol.strTipork = "H" Then
          If Not oCleGsol.ApriOrdListImpegni(NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga)) Then   'occhio alla prima riga quando faccio apri e/o quando cancello la riga sotto ...


          End If

          'ordini di produzione: una volta salvata la riga e ci sono dei figli il codice valuta non è più modificabile (il cambio si ...)
          If dsGsol.Tables("CORPOIMP").Select("ec_rigaor = " & NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga)).Length > 0 Then
            ec_codvalu.Enabled = False
          End If

        End If
      End If


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvRighe_NTSFocusedColumnChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles grvRighe.NTSFocusedColumnChanged
    Try
      GrvRighe_RowColChange()

      '--------------------------------------
      'visualizzo la form modale per il dettaglio TCO (solo su nuove righe e solo su articoli TCO)
      If oCleGsol.bModTCO Then
        If grvRighe.FocusedColumn.Name = "ec_quant" Then
          If Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
            If grvRighe.NTSGetCurrentDataRow.RowState = DataRowState.Added Then
              If NTSCInt(grvRighe.NTSGetCurrentDataRow!xxo_codtagl) <> 0 Then tlbDettTco_ItemClick(tlbDettTco, Nothing)
            End If
          End If
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function GrvRighe_RowColChange() As Boolean
    Try

      If oCleGsol Is Nothing Then Return True
      If dttUm Is Nothing Then Return True

      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        If Not ec_unmis.ColumnEdit Is Nothing Then CType(ec_unmis.ColumnEdit, NTSRepositoryItemComboBox).DataSource = dttUm
        ec_unmis.Enabled = False
      Else
        GctlSetVisEnab(ec_unmis, False)
      End If

      '--------------------------------------
      'compilo il combo delle unità di misura
      If Not ec_unmis.ColumnEdit Is Nothing Then
        CType(ec_unmis.ColumnEdit, NTSRepositoryItemComboBox).DataSource = dttUm
        If grvRighe.FocusedColumn.Name = "ec_unmis" And grvRighe.GetFocusedRowCellValue(ec_codart).ToString <> "" And NTSCStr(grvRighe.GetFocusedRowCellValue(ec_codart)).Trim <> "" Then
          grvRighe.liListCmb.Visible = False    'lo nascondo, visto che contiene tutte le unita di misura del db ...
          ec_unmis.NTSComboValueOk = oCleGsol.GetArticoUnMis(NTSCStr(grvRighe.GetFocusedRowCellValue(ec_codart)))
          'sarà BN__CHIL ha gestire ec_unmis.NTSComboValueOk
        End If
      End If    'If Not ec_unmis.ColumnEdit Is Nothing Then

      '----------------------------
      'blocco/sblocco a seconda della valuta
      If Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
        If NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_codvalu) = 0 Then
          ec_cambio.Enabled = False
          ec_prezvalc.Enabled = False
          GctlSetVisEnab(ec_prezzo, False)
        Else
          GctlSetVisEnab(ec_cambio, False)
          GctlSetVisEnab(ec_prezvalc, False)
          ec_prezzo.Enabled = False
        End If
      Else
        ec_cambio.Enabled = False
        ec_prezvalc.Enabled = False
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub grvRighe_NTSRowColChange(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSRowColChangeEventArgs) Handles grvRighe.NTSRowColChange
    Try
      If oCleGsol Is Nothing Then Return
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then Return


      '--------------------
      'se serve aggiorno il cambio nella tabella
      If e.CurrRow = e.PrevRow And e.PrevCol = "ec_cambio" Then
        If NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_cambio) <> 0 And _
           NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_codvalu) <> 0 And bCambioDelCambio Then
          If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128521436577992000, _
                              "Inserire il cambio indicato nella tabella dei CAMBI per la valuta '|" & _
                              NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_codvalu) & "|' per la data |" & NTSCDate(grvRighe.NTSGetCurrentDataRow!ec_datord).ToShortDateString & "|")) = Windows.Forms.DialogResult.Yes Then
            oCleGsol.AggiornaCambio(NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_codvalu), _
                                    NTSCDate(grvRighe.NTSGetCurrentDataRow!ec_datord).ToShortDateString, _
                                    NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_cambio), True)
          End If
        End If
      End If


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      bCambioDelCambio = False
    End Try
  End Sub

  Public Overridable Sub xxo_lottox_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles xxo_lottox.NTSZoomGest
    Dim oCZoo As New CLE__CZOO
    Dim bNuovo As Boolean = True
    Dim oTmp As New Control
    Dim oPar As New CLE__CLDP
    Try
      Me.ValidaLastControl()

      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128569683997968750, "Posizionarsi prima su una riga con codice articolo impostato"))
        Return
      End If
      If NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart)).Trim = "" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 129048537473498559, "Indicare prima il codice articolo"))
        Return
      End If

      If e.TipoEvento = "OPEN" Then
        bNuovo = False
        oPar.strParam = "APRI;" & NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart).PadRight(CLN__STD.CodartMaxLen) & ";" & NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_lotto).PadLeft(9, "0"c) & ";"
      Else
        oPar.strParam = "NUOV;" & NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart).PadRight(CLN__STD.CodartMaxLen) & ";" & "000000000" & ";"
      End If

      oTmp.Text = NTSCStr(grvRighe.NTSGetCurrentDataRow!xxo_lottox)
      NTSZOOM.OpenChildGest(oTmp, "ZOOMANALOTTI", DittaCorrente, bNuovo, oPar)

      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo
      grRighe.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

#Region "ALT+F2 e ALT+F3 rimappati"
  Public Overridable Sub ec_conto_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles ec_conto.NTSZoomGest
    Dim oCZoo As New CLE__CZOO
    Dim bNuovo As Boolean = True
    Dim oTmp As New Control
    Dim dttTmp As New DataTable
    Try
      Me.ValidaLastControl()
      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo

      If grvRighe.NTSGetCurrentDataRow Is Nothing Then Return
      If e.TipoEvento = "OPEN" Then
        If NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_conto) = 0 Then Return
        bNuovo = False
      End If

      oTmp.Text = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_conto).ToString
      oMenu.ValCodiceDb(oTmp.Text, DittaCorrente, "ANAGRA", "N", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then
        If dttTmp.Rows(0)!an_tipo.ToString = "C" Then
          NTSZOOM.OpenChildGest(oTmp, "ZOOMANAGRAC", DittaCorrente, bNuovo)
        Else
          NTSZOOM.OpenChildGest(oTmp, "ZOOMANAGRAF", DittaCorrente, bNuovo)
        End If
      Else
        NTSZOOM.OpenChildGest(oTmp, "ZOOMANAGRAF", DittaCorrente, bNuovo)
      End If
      grRighe.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Sub
#End Region

  Public Overridable Sub RicaricaGriglia()
    Try
      '-------------------------------
      'ricarico la griglia mantenendo i filtri impostati quando ho fattl 'apri'
      SetStato(0)

      If oParApri Is Nothing Then Return

      Me.Cursor = Cursors.WaitCursor
      oCleGsol.strTipork = oParApri.strParam.Substring(0, 1)
      If Not oCleGsol.ApriOrdlist(DittaCorrente, oParApri.strPar1, NTSCInt(oParApri.dPar1), NTSCInt(oParApri.dPar2), NTSCInt(oParApri.dPar3), _
                              NTSCDate(oParApri.strPar2).ToShortDateString, NTSCDate(oParApri.strPar3).ToShortDateString, _
                              NTSCDate(oParApri.strPar4).ToShortDateString, NTSCDate(oParApri.strPar5).ToShortDateString, _
                              CBool(IIf(oParApri.strParam.Substring(1, 1) = "S", True, False)), _
                              CBool(IIf(oParApri.strParam.Substring(2, 1) = "S", True, False)), _
                              CBool(IIf(oParApri.strParam.Substring(6, 1) = "S", True, False)), _
                              CBool(IIf(oParApri.strParam.Substring(3, 1) = "S", True, False)), _
                              CBool(IIf(oParApri.strParam.Substring(4, 1) = "S", True, False)), _
                              CBool(IIf(oParApri.strParam.Substring(5, 1) = "S", True, False)), _
                              CBool(IIf(oParApri.strParam.Substring(7, 1) = "S", True, False)), _
                              0, dsGsol) Then Return

      If dsGsol.Tables("CORPO").Rows.Count = 0 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128618118902968750, "Non esistono dati con queste caratteristiche."))
        Return
      End If

      '----------------------------------------------------
      'ricollego la griglia al datatable di movord
      grRighe.DataSource = Nothing
      dcGsol.DataSource = Nothing
      dcGsol.DataSource = dsGsol.Tables("CORPO")
      grRighe.DataSource = dcGsol

      SetStato(1)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Function GestisciGrigliaTCO() As Boolean
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Try
      If oCleGsol.bInUnload = True Then Return True
      If oCleGsol.bModTCO = False Then Return True

      '----------------------------
      'visualizzo o meno la griglia TCO ed imposto la caption delle colonne
      If grvRighe.FocusedRowHandle < 0 Then
        grTco.Visible = False
        tlbDettTco.Enabled = False
      ElseIf grvRighe.NTSGetCurrentDataRow Is Nothing Then
        grTco.Visible = False
        tlbDettTco.Enabled = False
        'ElseIf grvRighe.NTSGetCurrentDataRow.RowState = DataRowState.Added Then
        '  grTco.Visible = False
      ElseIf NTSCInt(grvRighe.NTSGetCurrentDataRow!xxo_codtagl) = 0 Then
        grTco.Visible = False
        tlbDettTco.Enabled = False
      Else
        GctlSetVisEnab(grTco, True)
        GctlSetVisEnab(tlbDettTco, False)
        '-------------------------
        'carico l'intestazione delle colonne e nascondo quelle che non servono
        oMenu.ValCodiceDb(NTSCInt(grvRighe.NTSGetCurrentDataRow!xxo_codtagl).ToString, DittaCorrente, "TABTAGL", "N", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then
          For i = 0 To grvTco.Columns.Count - 1
            l = NTSCInt(grvTco.Columns(i).Name.Substring(grvTco.Columns(i).Name.Length - 2))
            If l <> 0 Then
              grvTco.Columns(i).Caption = NTSCStr(dttTmp.Rows(0)("tb_dest" & l.ToString.PadLeft(2, "0"c)))
              If NTSCStr(grvTco.Columns(i).Caption).Trim = "" Then
                grvTco.Columns(i).Visible = False
              Else
                GctlSetVisEnab(grvTco.Columns(i), True)
              End If
            End If
          Next
        End If
        dttTmp.Clear()

        '-------------------------
        'collego i dati: se non presente creo la tab temporanea per la visualizzaz delle qta
        If dsGrvTCO.Tables.Count = 0 Then
          dsGrvTCO.Tables.Add(dsGsol.Tables("CORPOTC").Clone())
          dsGrvTCO.Tables(0).TableName = "TEMPTCO"
          dcGsorTCO.DataSource = dsGrvTCO.Tables("TEMPTCO")
          grTco.DataSource = dcGsorTCO
        End If

        dsGrvTCO.Tables("TEMPTCO").Clear()
        dtrT = dsGsol.Tables("CORPOTC").Select("ec_riga = " & NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga).ToString)
        If dtrT.Length > 0 Then dsGrvTCO.Tables("TEMPTCO").ImportRow(dtrT(0))
        dsGrvTCO.Tables("TEMPTCO").AcceptChanges()
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub Stampa(ByVal nDestin As Integer, ByVal bSoloImpegni As Boolean, ByVal bSoloAttivita As Boolean)
    Dim nPjob As Object = Nothing
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer = 0
    Dim strProgr As String = ""
    Dim strTipogestione As String = ""

    Try
      If dsGsol.Tables("CORPO").Rows.Count = 0 Then Return

      For i = 0 To dsGsol.Tables("CORPO").Rows.Count - 1
        strProgr += ", " & dsGsol.Tables("CORPO").Rows(i)!ec_riga.ToString
      Next
      strProgr = strProgr.Substring(2)
      If Not oCleGsol.RiempiTmpTable(lIIAssris, lIIAttivit, lIIOrlist, _
                                     lIIOrdl, lIIOrlisttc, strProgr, bSoloImpegni, bSoloAttivita) Then Return

      '--------------------------------------------------
      'preparo il motore di stampa
      strCrpe = "{TTORDL.codditt} = '" & DittaCorrente & "' And {TTORDL.instid}= " & lIIOrdl.ToString

      If oCleGsol.strTipork <> "H" Then
        Select Case oCleGsol.strTipork
          Case "O" : strTipogestione = oApp.Tr(Me, 128786514235154000, "--  ORDINI FORNITORI  --")
          Case "Y" : strTipogestione = oApp.Tr(Me, 128620679579062500, "--  IMPEGNI DI PRODUZIONE  --")
          Case "X" : strTipogestione = oApp.Tr(Me, 128620679123750000, "--  IMPEGNI DI TRASFERIMENTO  --")
        End Select
        nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSORGSOL", "Reports1", " ", 0, nDestin, "BSORGSOL.RPT", False, "Gestione Lista Ordini", False)
        If nPjob Is Nothing Then Return

        '--------------------------------------------------
        'lancio tutti gli eventuali reports (gestisce già il multireport)
        For i = LBound(CType(nPjob, Array), 2) To UBound(CType(nPjob, Array), 2)
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "TIPOGESTIONE", "'" & strTipogestione & "'")
          nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
          nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
        Next

      Else
        strTipogestione = oApp.Tr(Me, 128620680725312500, "--  ORDINI LAVORAZIONE  --")
        If bSoloImpegni = True Then
          strCrpe = "{TTORDL.codditt} = '" & DittaCorrente & "'" & _
                    " And {TTORDL.instid}= " & lIIOrdl & _
                    " And (IsNull({TTORLIST.instid}) OR {TTORLIST.instid}= " & lIIOrlist & ")"

          nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSORGSOL", "Reports2", " ", 0, nDestin, "BSORGSO1.RPT", False, "Gestione Lista Ordini", False)
          If nPjob Is Nothing Then GoTo PassaAStampaSuccessiva

          '--------------------------------------------------
          'lancio tutti gli eventuali reports (gestisce già il multireport)
          For i = LBound(CType(nPjob, Array), 2) To UBound(CType(nPjob, Array), 2)
            nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "TIPOGESTIONE", "'" & strTipogestione & "'")
            nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
            nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
          Next
        End If    'If bSoloImpegni = True Then

PassaAStampaSuccessiva:
        If bSoloAttivita = True Then
          strCrpe = "{TTORDL.codditt} = '" & DittaCorrente & "'" & _
                    " And {TTORDL.instid}= " & lIIOrdl & _
                    " And (IsNull({TTATTIVIT.instid}) Or {TTATTIVIT.instid}= " & lIIAttivit & _
                    ") And (IsNull({TTASSRIS.instid}) Or {TTASSRIS.instid}= " & lIIAssris & ")"

          nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSORGSOL", "Reports3", " ", 0, nDestin, "BSORGSO2.RPT", False, "Gestione Lista Ordini", False)
          If nPjob Is Nothing Then Return

          '--------------------------------------------------
          'lancio tutti gli eventuali reports (gestisce già il multireport)
          For i = LBound(CType(nPjob, Array), 2) To UBound(CType(nPjob, Array), 2)
            nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "TIPOGESTIONE", "'" & strTipogestione & "'")
            nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
            nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
          Next

        End If    'If bSoloAttivita = True Then
      End If    'If oCleGsol.strTipork <> "H" Then

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function SettaConfermato(ByVal bTutteLeRighe As Boolean) As Boolean
    'imposto il flag a 'confermato' su tutte le righe o solo quele selezionate
    Dim i As Integer = 0
    Dim strStato As String = ""
    Try
 If Not RecordSalva(False, Nothing) Then Return False

      If oCleGsol.strTipork = "H" Then
        If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128625684582031250, _
                    "Premendo 'Sì' sarà cambiato lo 'Stato riga' di tutti i dati visualizzati" & vbCrLf & _
                    "e degli Impegni collegati relativi con 'Stato riga' uguale a:" & vbCrLf & _
                    ". Generato" & vbCrLf & _
                    ". Emissione RDA" & vbCrLf & _
                    ". Approvazione RDA" & vbCrLf & _
                    ". Emissione RDO" & vbCrLf & _
                    "Confermare l'aggiornamento?")) = Windows.Forms.DialogResult.No Then Return False
      Else
        If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128625684869531250, _
                    "Premendo 'Sì' sarà cambiato lo 'Stato riga' di tutti i dati visualizzati" & vbCrLf & _
                    "con 'Stato riga' uguale a:" & vbCrLf & _
                    ". Generato" & vbCrLf & _
                    ". Emissione RDA" & vbCrLf & _
                    ". Approvazione RDA" & vbCrLf & _
                    ". Emissione RDO" & vbCrLf & _
                    "Confermare l'aggiornamento?")) = Windows.Forms.DialogResult.No Then Return False
      End If

      If oCleGsol.dttEC.Select("(ec_stato = ' ' OR ec_stato = 'P' OR ec_stato = 'Q' OR ec_stato = 'R') " & IIf(bTutteLeRighe, "", " AND xxo_seleziona = 'S'").ToString).Length > 0 Then
        Me.Cursor = Cursors.WaitCursor
        For i = 0 To oCleGsol.dttEC.Rows.Count - 1
          dcGsol.Position = i
          If grvRighe.NTSGetCurrentDataRow Is Nothing Then Exit For


          strStato = grvRighe.NTSGetCurrentDataRow!ec_stato.ToString
          If (strStato = " " Or strStato = "P" Or strStato = "Q" Or strStato = "R") Then
            If grvRighe.NTSGetCurrentDataRow!xxo_seleziona.ToString = "S" Or (bTutteLeRighe And grvRighe.NTSGetCurrentDataRow!xxo_seleziona.ToString = "N") Then
              grvRighe.NTSGetCurrentDataRow!ec_stato = "S"
              If Not RecordSalva(False, Nothing) Then Return False
            End If
          End If
          grvRighe.NTSGetCurrentDataRow!xxo_seleziona = "N"
          'grvRighe.NTSGetCurrentDataRow.AcceptChanges()
        Next
        oApp.MsgBoxInfo(oApp.Tr(Me, 128625678330625000, "Elaborazione completata"))
      End If



      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
      Me.Cursor = Cursors.Default
    End Try
  End Function

  Public Overrides Function NTSGetDataAutocompletamento(ByVal strTabName As String, ByVal strDescr As String, _
    ByVal IsCrmUser As Boolean, ByRef dsOut As DataSet) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      If grvRighe.FocusedColumn.Equals(ec_conto) Then strTabName = "ANAGRA_FOR"
      '--------------------------------------------------------------------------------------------------------------
      Return MyBase.NTSGetDataAutocompletamento(strTabName, strDescr, IsCrmUser, dsOut)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Sub SelDesRighe(ByVal strOperazione As String)
    Try
      '--------------------------------------------------------------------------------------------------------------
      For Each dtrRow As DataRow In dsGsol.Tables("CORPO").Select(grvRighe.RowFilter)
        dtrRow!xxo_seleziona = IIf(strOperazione = "I", IIf(NTSCStr(dtrRow!xxo_seleziona) = "S", "N", "S"), strOperazione).ToString
      Next
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Function RecordSalva(ByVal bDeleted As Boolean, ByVal dtrDeleted As DataRow) As Boolean
    Try
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then Return True
      'quando attivo un raggruppamento in griglia viene cambiata la dcGsol.position rimettendola = 0, 
      'poi viene scatenata la 'grvRighe_NTSBeforeRowUpdate' MA il dcGsol.position Ã¨ giÃ  = 0!!!!
      'il risultato Ã¨ che salvando si perdono impegni e lavorazioni collegate della prima riga
      'devo chiamare la RecordSalva passandogli la posisione della riga effettiva su cui ero entarto e non la 0.
      'non posso usare la e.RowHandle, perchÃ¨ restituisce sempre il numero di riga dall'alto, e non va bene in caso di griglia ordinata in modo personalizzato
      If grvRighe.GroupCount > 0 Then
        Dim nPosOk As Integer = dcGsol.Find("ec_riga", grvRighe.NTSGetCurrentDataRow!ec_riga)
        If Not oCleGsol.RecordSalva(nPosOk, bDeleted, dtrDeleted) Then Return False 'rimango sulla riga su cui sono
      Else
        If Not oCleGsol.RecordSalva(dcGsol.Position, bDeleted, dtrDeleted) Then Return False 'rimango sulla riga su cui sono
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function


  Public Overridable Function PosizioneInGrigliaSuIntestazioneGruppo() As Boolean
    Try
      If NTSCStr(grvRighe.GetFocusedRowCellValue(ec_riga)).Trim = "" And Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
        'se ho fatto un raggruppamento in griglia ed il focus Ã¨ sul raggruppamento
        oApp.MsgBoxErr(oApp.Tr(Me, 130657913731548339, "Posizionarsi prima su una riga di griglia con codice articolo impostato (non sull'intestazione di un raggruppamento)"))
        Return False
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
End Class
