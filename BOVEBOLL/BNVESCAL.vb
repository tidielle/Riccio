Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVESCAL
  Public oCallParams As CLE__CLDP
  Public dcImpe As BindingSource = New BindingSource()
  Public lRigaCopiata As Integer = 0                      'utilizzata dalle voci di menu copia/incolla riga per memorizzare la riga da copiare

  'passati dal chiamante
  Public frmParent As FRMVEBOLL                           'FORM PARENT: non passato da form _T, _X, _F
  Public oCleBoll As CLEVEBOLL
  Public dttUm As DataTable = Nothing                    'elenco delle unità di misura utilizzate in artico
  Public bEt_Scorpo As Boolean = False
  Public nEt_valuta As Integer = 0
  Public nEt_listino As Integer = 0
  Public dtEt_datdoc As Date = Nothing
  Public nAnnoDoc As Integer = 0
  Public strSerieDoc As String = ""
  Public lNumdoc As Integer = 0
  Public nEt_magaz As Integer = 0
  Public lEt_conto As Integer = 0

  Private components As System.ComponentModel.IContainer


  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMVESCAL))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbInsRiga = New NTSInformatica.NTSBarButtonItem
    Me.tlbCopiaRiga = New NTSInformatica.NTSBarMenuItem
    Me.tlbIncollaRiga = New NTSInformatica.NTSBarMenuItem
    Me.tlbSelezLotto = New NTSInformatica.NTSBarMenuItem
    Me.tlbBolleCtoLav = New NTSInformatica.NTSBarMenuItem
    Me.tlbDetMatricole = New NTSInformatica.NTSBarMenuItem
    Me.tlbRiordina = New NTSInformatica.NTSBarButtonItem
    Me.tlbSeleziona = New NTSInformatica.NTSBarSubItem
    Me.tlbDaLista = New NTSInformatica.NTSBarMenuItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.tlbSelezUbicazione = New NTSInformatica.NTSBarMenuItem
    Me.pnCorpo = New NTSInformatica.NTSPanel
    Me.edUltCost = New NTSInformatica.NTSTextBoxNum
    Me.edPreList = New NTSInformatica.NTSTextBoxNum
    Me.edDispNetta = New NTSInformatica.NTSTextBoxNum
    Me.edDispon = New NTSInformatica.NTSTextBoxNum
    Me.lbDispon = New NTSInformatica.NTSLabel
    Me.lbPreList = New NTSInformatica.NTSLabel
    Me.pnGriglia = New NTSInformatica.NTSPanel
    Me.grRighe = New NTSInformatica.NTSGrid
    Me.grvRighe = New NTSInformatica.NTSGridView
    Me.ec_riga = New NTSInformatica.NTSGridColumn
    Me.ec_matric = New NTSInformatica.NTSGridColumn
    Me.ec_codart = New NTSInformatica.NTSGridColumn
    Me.ec_descr = New NTSInformatica.NTSGridColumn
    Me.ec_desint = New NTSInformatica.NTSGridColumn
    Me.ec_unmis = New NTSInformatica.NTSGridColumn
    Me.ec_colli = New NTSInformatica.NTSGridColumn
    Me.ec_ump = New NTSInformatica.NTSGridColumn
    Me.ec_quant = New NTSInformatica.NTSGridColumn
    Me.ec_preziva = New NTSInformatica.NTSGridColumn
    Me.ec_prezvalc = New NTSInformatica.NTSGridColumn
    Me.ec_prezzo = New NTSInformatica.NTSGridColumn
    Me.ec_causale = New NTSInformatica.NTSGridColumn
    Me.xxo_causale = New NTSInformatica.NTSGridColumn
    Me.ec_magaz = New NTSInformatica.NTSGridColumn
    Me.xxo_magaz = New NTSInformatica.NTSGridColumn
    Me.ec_ricimp = New NTSInformatica.NTSGridColumn
    Me.ec_provv = New NTSInformatica.NTSGridColumn
    Me.ec_vprovv = New NTSInformatica.NTSGridColumn
    Me.ec_provv2 = New NTSInformatica.NTSGridColumn
    Me.ec_vprovv2 = New NTSInformatica.NTSGridColumn
    Me.ec_controp = New NTSInformatica.NTSGridColumn
    Me.xxo_controp = New NTSInformatica.NTSGridColumn
    Me.ec_codiva = New NTSInformatica.NTSGridColumn
    Me.xxo_codiva = New NTSInformatica.NTSGridColumn
    Me.ec_stasino = New NTSInformatica.NTSGridColumn
    Me.ec_codcfam = New NTSInformatica.NTSGridColumn
    Me.xxo_codcfam = New NTSInformatica.NTSGridColumn
    Me.ec_commeca = New NTSInformatica.NTSGridColumn
    Me.xxo_commeca = New NTSInformatica.NTSGridColumn
    Me.ec_subcommeca = New NTSInformatica.NTSGridColumn
    Me.xxo_lottox = New NTSInformatica.NTSGridColumn
    Me.ec_codcena = New NTSInformatica.NTSGridColumn
    Me.xxo_codcena = New NTSInformatica.NTSGridColumn
    Me.ec_note = New NTSInformatica.NTSGridColumn
    Me.ec_valore = New NTSInformatica.NTSGridColumn
    Me.ec_contocontr = New NTSInformatica.NTSGridColumn
    Me.xxo_contocon = New NTSInformatica.NTSGridColumn
    Me.ec_codclie = New NTSInformatica.NTSGridColumn
    Me.xxo_codclie = New NTSInformatica.NTSGridColumn
    Me.ec_misura1 = New NTSInformatica.NTSGridColumn
    Me.ec_misura2 = New NTSInformatica.NTSGridColumn
    Me.ec_misura3 = New NTSInformatica.NTSGridColumn
    Me.xxo_codarfo = New NTSInformatica.NTSGridColumn
    Me.ec_perqta = New NTSInformatica.NTSGridColumn
    Me.ec_fase = New NTSInformatica.NTSGridColumn
    Me.xxo_fase = New NTSInformatica.NTSGridColumn
    Me.ec_ubicaz = New NTSInformatica.NTSGridColumn
    Me.xxo_codtagl = New NTSInformatica.NTSGridColumn
    Me.ec_ortipo = New NTSInformatica.NTSGridColumn
    Me.ec_oranno = New NTSInformatica.NTSGridColumn
    Me.ec_orserie = New NTSInformatica.NTSGridColumn
    Me.ec_ornum = New NTSInformatica.NTSGridColumn
    Me.ec_orriga = New NTSInformatica.NTSGridColumn
    Me.ec_salcon = New NTSInformatica.NTSGridColumn
    Me.ec_prtipo = New NTSInformatica.NTSGridColumn
    Me.ec_pranno = New NTSInformatica.NTSGridColumn
    Me.ec_prserie = New NTSInformatica.NTSGridColumn
    Me.ec_prnum = New NTSInformatica.NTSGridColumn
    Me.ec_prriga = New NTSInformatica.NTSGridColumn
    Me.ec_cltipo = New NTSInformatica.NTSGridColumn
    Me.ec_clanno = New NTSInformatica.NTSGridColumn
    Me.ec_clserie = New NTSInformatica.NTSGridColumn
    Me.ec_clnum = New NTSInformatica.NTSGridColumn
    Me.ec_clriga = New NTSInformatica.NTSGridColumn
    Me.ec_nptipo = New NTSInformatica.NTSGridColumn
    Me.ec_npanno = New NTSInformatica.NTSGridColumn
    Me.ec_npserie = New NTSInformatica.NTSGridColumn
    Me.ec_npnum = New NTSInformatica.NTSGridColumn
    Me.ec_npriga = New NTSInformatica.NTSGridColumn
    Me.ec_npqtadis = New NTSInformatica.NTSGridColumn
    Me.ec_npcoldis = New NTSInformatica.NTSGridColumn
    Me.ec_npvaldis = New NTSInformatica.NTSGridColumn
    Me.ec_npsalcon = New NTSInformatica.NTSGridColumn
    Me.ec_nprcoleva = New NTSInformatica.NTSGridColumn
    Me.ec_nprquaeva = New NTSInformatica.NTSGridColumn
    Me.ec_nprflevas = New NTSInformatica.NTSGridColumn
    Me.ec_nprvalore = New NTSInformatica.NTSGridColumn
    Me.ec_valorev = New NTSInformatica.NTSGridColumn
    Me.ec_tctaglia = New NTSInformatica.NTSGridColumn
    Me.ec_coddivi = New NTSInformatica.NTSGridColumn
    Me.xxo_coddivi = New NTSInformatica.NTSGridColumn
    xxo_tctagliaf = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnCorpo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCorpo.SuspendLayout()
    CType(Me.edUltCost.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPreList.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDispNetta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDispon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnGriglia, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGriglia.SuspendLayout()
    CType(Me.grRighe, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvRighe, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
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
    'xxo_tctagliaf
    '
    xxo_tctagliaf.AppearanceCell.Options.UseBackColor = True
    xxo_tctagliaf.AppearanceCell.Options.UseTextOptions = True
    xxo_tctagliaf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    xxo_tctagliaf.Caption = "Taglia figlio"
    xxo_tctagliaf.Enabled = True
    xxo_tctagliaf.FieldName = "xxo_tctagliaf"
    xxo_tctagliaf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    xxo_tctagliaf.Name = "xxo_tctagliaf"
    xxo_tctagliaf.NTSRepositoryComboBox = Nothing
    xxo_tctagliaf.NTSRepositoryItemCheck = Nothing
    xxo_tctagliaf.NTSRepositoryItemMemo = Nothing
    xxo_tctagliaf.NTSRepositoryItemText = Nothing
    xxo_tctagliaf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    xxo_tctagliaf.OptionsFilter.AllowFilter = False
    xxo_tctagliaf.Visible = True
    xxo_tctagliaf.VisibleIndex = 46
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti, Me.tlbInsRiga, Me.tlbCopiaRiga, Me.tlbIncollaRiga, Me.tlbSelezLotto, Me.tlbSelezUbicazione, Me.tlbBolleCtoLav, Me.tlbDetMatricole, Me.tlbSeleziona, Me.tlbDaLista, Me.tlbRiordina})
    Me.NtsBarManager1.MaxItemId = 27
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci, True)})
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
    Me.tlbRipristina.Id = 3
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.Id = 2
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
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
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbInsRiga), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCopiaRiga), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbIncollaRiga), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSelezLotto, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbBolleCtoLav), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDetMatricole), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRiordina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSeleziona, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbInsRiga
    '
    Me.tlbInsRiga.Caption = "Inserisci riga"
    Me.tlbInsRiga.Id = 16
    Me.tlbInsRiga.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F12))
    Me.tlbInsRiga.Name = "tlbInsRiga"
    Me.tlbInsRiga.Visible = True
    '
    'tlbCopiaRiga
    '
    Me.tlbCopiaRiga.Caption = "Copia riga"
    Me.tlbCopiaRiga.Id = 17
    Me.tlbCopiaRiga.Name = "tlbCopiaRiga"
    Me.tlbCopiaRiga.NTSIsCheckBox = False
    Me.tlbCopiaRiga.Visible = True
    '
    'tlbIncollaRiga
    '
    Me.tlbIncollaRiga.Caption = "Incolla riga"
    Me.tlbIncollaRiga.Id = 19
    Me.tlbIncollaRiga.Name = "tlbIncollaRiga"
    Me.tlbIncollaRiga.NTSIsCheckBox = False
    Me.tlbIncollaRiga.Visible = True
    '
    'tlbSelezLotto
    '
    Me.tlbSelezLotto.Caption = "Seleziona lotto/ubicazione aperti"
    Me.tlbSelezLotto.Id = 20
    Me.tlbSelezLotto.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F7))
    Me.tlbSelezLotto.Name = "tlbSelezLotto"
    Me.tlbSelezLotto.NTSIsCheckBox = False
    Me.tlbSelezLotto.Visible = True
    '
    'tlbBolleCtoLav
    '
    Me.tlbBolleCtoLav.Caption = "Seleziona bolle c/to lav. aperte"
    Me.tlbBolleCtoLav.Id = 22
    Me.tlbBolleCtoLav.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F11))
    Me.tlbBolleCtoLav.Name = "tlbBolleCtoLav"
    Me.tlbBolleCtoLav.NTSIsCheckBox = False
    Me.tlbBolleCtoLav.Visible = True
    '
    'tlbDetMatricole
    '
    Me.tlbDetMatricole.Caption = "Dettaglio matricole"
    Me.tlbDetMatricole.Id = 23
    Me.tlbDetMatricole.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F11))
    Me.tlbDetMatricole.Name = "tlbDetMatricole"
    Me.tlbDetMatricole.NTSIsCheckBox = False
    Me.tlbDetMatricole.Visible = True
    '
    'tlbRiordina
    '
    Me.tlbRiordina.Caption = "Riordina righe"
    Me.tlbRiordina.Id = 26
    Me.tlbRiordina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.F12))
    Me.tlbRiordina.Name = "tlbRiordina"
    Me.tlbRiordina.Visible = True
    '
    'tlbSeleziona
    '
    Me.tlbSeleziona.Caption = "SELEZIONA"
    Me.tlbSeleziona.Id = 24
    Me.tlbSeleziona.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDaLista)})
    Me.tlbSeleziona.Name = "tlbSeleziona"
    Me.tlbSeleziona.Visible = True
    '
    'tlbDaLista
    '
    Me.tlbDaLista.Caption = "Da lista selezionata"
    Me.tlbDaLista.Id = 25
    Me.tlbDaLista.Name = "tlbDaLista"
    Me.tlbDaLista.NTSIsCheckBox = False
    Me.tlbDaLista.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 12
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'tlbSelezUbicazione
    '
    Me.tlbSelezUbicazione.Caption = "Seleziona ubicazioni aperte"
    Me.tlbSelezUbicazione.Id = 21
    Me.tlbSelezUbicazione.Name = "tlbSelezUbicazione"
    Me.tlbSelezUbicazione.NTSIsCheckBox = False
    Me.tlbSelezUbicazione.Visible = True
    '
    'pnCorpo
    '
    Me.pnCorpo.AllowDrop = True
    Me.pnCorpo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCorpo.Appearance.Options.UseBackColor = True
    Me.pnCorpo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCorpo.Controls.Add(Me.edUltCost)
    Me.pnCorpo.Controls.Add(Me.edPreList)
    Me.pnCorpo.Controls.Add(Me.edDispNetta)
    Me.pnCorpo.Controls.Add(Me.edDispon)
    Me.pnCorpo.Controls.Add(Me.lbDispon)
    Me.pnCorpo.Controls.Add(Me.lbPreList)
    Me.pnCorpo.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCorpo.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnCorpo.Location = New System.Drawing.Point(0, 30)
    Me.pnCorpo.Name = "pnCorpo"
    Me.pnCorpo.NTSActiveTrasparency = True
    Me.pnCorpo.Size = New System.Drawing.Size(740, 41)
    Me.pnCorpo.TabIndex = 5
    '
    'edUltCost
    '
    Me.edUltCost.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edUltCost.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edUltCost.EditValue = "0"
    Me.edUltCost.Enabled = False
    Me.edUltCost.Location = New System.Drawing.Point(195, 11)
    Me.edUltCost.Name = "edUltCost"
    Me.edUltCost.NTSDbField = ""
    Me.edUltCost.NTSFormat = "0"
    Me.edUltCost.NTSForzaVisZoom = False
    Me.edUltCost.NTSOldValue = ""
    Me.edUltCost.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edUltCost.Properties.Appearance.Options.UseBackColor = True
    Me.edUltCost.Properties.Appearance.Options.UseTextOptions = True
    Me.edUltCost.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edUltCost.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUltCost.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUltCost.Properties.AutoHeight = False
    Me.edUltCost.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUltCost.Properties.MaxLength = 65536
    Me.edUltCost.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUltCost.Size = New System.Drawing.Size(80, 20)
    Me.edUltCost.TabIndex = 48
    '
    'edPreList
    '
    Me.edPreList.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edPreList.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPreList.EditValue = "0"
    Me.edPreList.Enabled = False
    Me.edPreList.Location = New System.Drawing.Point(109, 11)
    Me.edPreList.Name = "edPreList"
    Me.edPreList.NTSDbField = ""
    Me.edPreList.NTSFormat = "0"
    Me.edPreList.NTSForzaVisZoom = False
    Me.edPreList.NTSOldValue = ""
    Me.edPreList.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edPreList.Properties.Appearance.Options.UseBackColor = True
    Me.edPreList.Properties.Appearance.Options.UseTextOptions = True
    Me.edPreList.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPreList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPreList.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPreList.Properties.AutoHeight = False
    Me.edPreList.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPreList.Properties.MaxLength = 65536
    Me.edPreList.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPreList.Size = New System.Drawing.Size(80, 20)
    Me.edPreList.TabIndex = 47
    '
    'edDispNetta
    '
    Me.edDispNetta.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edDispNetta.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDispNetta.EditValue = "0"
    Me.edDispNetta.Enabled = False
    Me.edDispNetta.Location = New System.Drawing.Point(635, 11)
    Me.edDispNetta.Name = "edDispNetta"
    Me.edDispNetta.NTSDbField = ""
    Me.edDispNetta.NTSFormat = "0"
    Me.edDispNetta.NTSForzaVisZoom = False
    Me.edDispNetta.NTSOldValue = ""
    Me.edDispNetta.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDispNetta.Properties.Appearance.Options.UseBackColor = True
    Me.edDispNetta.Properties.Appearance.Options.UseTextOptions = True
    Me.edDispNetta.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDispNetta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDispNetta.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDispNetta.Properties.AutoHeight = False
    Me.edDispNetta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDispNetta.Properties.MaxLength = 65536
    Me.edDispNetta.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDispNetta.Size = New System.Drawing.Size(99, 20)
    Me.edDispNetta.TabIndex = 46
    '
    'edDispon
    '
    Me.edDispon.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edDispon.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDispon.EditValue = "0"
    Me.edDispon.Enabled = False
    Me.edDispon.Location = New System.Drawing.Point(525, 11)
    Me.edDispon.Name = "edDispon"
    Me.edDispon.NTSDbField = ""
    Me.edDispon.NTSFormat = "0"
    Me.edDispon.NTSForzaVisZoom = False
    Me.edDispon.NTSOldValue = "0"
    Me.edDispon.Properties.Appearance.Options.UseTextOptions = True
    Me.edDispon.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDispon.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDispon.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDispon.Properties.AutoHeight = False
    Me.edDispon.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDispon.Properties.MaxLength = 65536
    Me.edDispon.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDispon.Size = New System.Drawing.Size(104, 20)
    Me.edDispon.TabIndex = 45
    '
    'lbDispon
    '
    Me.lbDispon.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbDispon.AutoSize = True
    Me.lbDispon.BackColor = System.Drawing.Color.Transparent
    Me.lbDispon.Location = New System.Drawing.Point(409, 14)
    Me.lbDispon.Name = "lbDispon"
    Me.lbDispon.NTSDbField = ""
    Me.lbDispon.Size = New System.Drawing.Size(110, 13)
    Me.lbDispon.TabIndex = 5
    Me.lbDispon.Text = "Disponib./ disp. netta"
    Me.lbDispon.Tooltip = ""
    Me.lbDispon.UseMnemonic = False
    '
    'lbPreList
    '
    Me.lbPreList.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbPreList.AutoSize = True
    Me.lbPreList.BackColor = System.Drawing.Color.Transparent
    Me.lbPreList.Location = New System.Drawing.Point(11, 14)
    Me.lbPreList.Name = "lbPreList"
    Me.lbPreList.NTSDbField = ""
    Me.lbPreList.Size = New System.Drawing.Size(92, 13)
    Me.lbPreList.TabIndex = 3
    Me.lbPreList.Text = "Listino / ult. costo"
    Me.lbPreList.Tooltip = ""
    Me.lbPreList.UseMnemonic = False
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
    Me.pnGriglia.Location = New System.Drawing.Point(0, 71)
    Me.pnGriglia.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGriglia.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGriglia.Name = "pnGriglia"
    Me.pnGriglia.NTSActiveTrasparency = True
    Me.pnGriglia.Size = New System.Drawing.Size(740, 371)
    Me.pnGriglia.TabIndex = 6
    '
    'grRighe
    '
    Me.grRighe.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grRighe.EmbeddedNavigator.Name = ""
    Me.grRighe.Location = New System.Drawing.Point(0, 0)
    Me.grRighe.MainView = Me.grvRighe
    Me.grRighe.Name = "grRighe"
    Me.grRighe.Size = New System.Drawing.Size(740, 371)
    Me.grRighe.TabIndex = 2
    Me.grRighe.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvRighe})
    '
    'grvRighe
    '
    Me.grvRighe.ActiveFilterEnabled = False
    Me.grvRighe.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ec_riga, Me.ec_matric, Me.ec_codart, Me.ec_descr, Me.ec_desint, Me.ec_unmis, Me.ec_colli, Me.ec_ump, Me.ec_quant, Me.ec_preziva, Me.ec_prezvalc, Me.ec_prezzo, Me.ec_causale, Me.xxo_causale, Me.ec_magaz, Me.xxo_magaz, Me.ec_ricimp, Me.ec_provv, Me.ec_vprovv, Me.ec_provv2, Me.ec_vprovv2, Me.ec_controp, Me.xxo_controp, Me.ec_codiva, Me.xxo_codiva, Me.ec_stasino, Me.ec_codcfam, Me.xxo_codcfam, Me.ec_commeca, Me.xxo_commeca, Me.ec_subcommeca, Me.xxo_lottox, Me.ec_codcena, Me.xxo_codcena, Me.ec_note, Me.ec_valore, Me.ec_contocontr, Me.xxo_contocon, Me.ec_codclie, Me.xxo_codclie, Me.ec_misura1, Me.ec_misura2, Me.ec_misura3, Me.xxo_codarfo, Me.ec_perqta, Me.ec_fase, Me.xxo_fase, Me.ec_ubicaz, Me.xxo_codtagl, Me.ec_ortipo, Me.ec_oranno, Me.ec_orserie, Me.ec_ornum, Me.ec_orriga, Me.ec_salcon, Me.ec_prtipo, Me.ec_pranno, Me.ec_prserie, Me.ec_prnum, Me.ec_prriga, Me.ec_cltipo, Me.ec_clanno, Me.ec_clserie, Me.ec_clnum, Me.ec_clriga, Me.ec_nptipo, Me.ec_npanno, Me.ec_npserie, Me.ec_npnum, Me.ec_npriga, Me.ec_npqtadis, Me.ec_npcoldis, Me.ec_npvaldis, Me.ec_npsalcon, Me.ec_nprcoleva, Me.ec_nprquaeva, Me.ec_nprflevas, Me.ec_nprvalore, Me.ec_valorev, Me.ec_tctaglia, Me.ec_coddivi, Me.xxo_coddivi, xxo_tctagliaf})
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
    'ec_riga
    '
    Me.ec_riga.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.ec_riga.AppearanceCell.Options.UseBackColor = True
    Me.ec_riga.AppearanceCell.Options.UseTextOptions = True
    Me.ec_riga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_riga.Caption = "Riga"
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
    Me.ec_riga.VisibleIndex = 0
    Me.ec_riga.Width = 32
    '
    'ec_matric
    '
    Me.ec_matric.AppearanceCell.Options.UseBackColor = True
    Me.ec_matric.AppearanceCell.Options.UseTextOptions = True
    Me.ec_matric.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_matric.Caption = "Barcode"
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
    Me.ec_matric.Width = 73
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
    Me.ec_codart.VisibleIndex = 1
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
    Me.ec_descr.VisibleIndex = 2
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
    Me.ec_unmis.VisibleIndex = 3
    Me.ec_unmis.Width = 39
    '
    'ec_colli
    '
    Me.ec_colli.AppearanceCell.Options.UseBackColor = True
    Me.ec_colli.AppearanceCell.Options.UseTextOptions = True
    Me.ec_colli.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_colli.Caption = "Colli"
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
    Me.ec_colli.VisibleIndex = 4
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
    Me.ec_ump.VisibleIndex = 5
    '
    'ec_quant
    '
    Me.ec_quant.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant.Caption = "Q.tà"
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
    Me.ec_quant.VisibleIndex = 6
    '
    'ec_preziva
    '
    Me.ec_preziva.AppearanceCell.Options.UseBackColor = True
    Me.ec_preziva.AppearanceCell.Options.UseTextOptions = True
    Me.ec_preziva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_preziva.Caption = "Prezzo IVA inc."
    Me.ec_preziva.Enabled = True
    Me.ec_preziva.FieldName = "ec_preziva"
    Me.ec_preziva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_preziva.Name = "ec_preziva"
    Me.ec_preziva.NTSRepositoryComboBox = Nothing
    Me.ec_preziva.NTSRepositoryItemCheck = Nothing
    Me.ec_preziva.NTSRepositoryItemMemo = Nothing
    Me.ec_preziva.NTSRepositoryItemText = Nothing
    Me.ec_preziva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_preziva.OptionsFilter.AllowFilter = False
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
    Me.ec_prezzo.VisibleIndex = 7
    '
    'ec_causale
    '
    Me.ec_causale.AppearanceCell.Options.UseBackColor = True
    Me.ec_causale.AppearanceCell.Options.UseTextOptions = True
    Me.ec_causale.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_causale.Caption = "Causale"
    Me.ec_causale.Enabled = True
    Me.ec_causale.FieldName = "ec_causale"
    Me.ec_causale.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_causale.Name = "ec_causale"
    Me.ec_causale.NTSRepositoryComboBox = Nothing
    Me.ec_causale.NTSRepositoryItemCheck = Nothing
    Me.ec_causale.NTSRepositoryItemMemo = Nothing
    Me.ec_causale.NTSRepositoryItemText = Nothing
    Me.ec_causale.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_causale.OptionsFilter.AllowFilter = False
    Me.ec_causale.Visible = True
    Me.ec_causale.VisibleIndex = 8
    '
    'xxo_causale
    '
    Me.xxo_causale.AppearanceCell.Options.UseBackColor = True
    Me.xxo_causale.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_causale.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_causale.Caption = "Descr. causale"
    Me.xxo_causale.Enabled = False
    Me.xxo_causale.FieldName = "xxo_causale"
    Me.xxo_causale.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_causale.Name = "xxo_causale"
    Me.xxo_causale.NTSRepositoryComboBox = Nothing
    Me.xxo_causale.NTSRepositoryItemCheck = Nothing
    Me.xxo_causale.NTSRepositoryItemMemo = Nothing
    Me.xxo_causale.NTSRepositoryItemText = Nothing
    Me.xxo_causale.OptionsColumn.AllowEdit = False
    Me.xxo_causale.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_causale.OptionsColumn.ReadOnly = True
    Me.xxo_causale.OptionsFilter.AllowFilter = False
    Me.xxo_causale.Visible = True
    Me.xxo_causale.VisibleIndex = 9
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
    Me.ec_magaz.VisibleIndex = 10
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
    Me.xxo_magaz.VisibleIndex = 11
    '
    'ec_ricimp
    '
    Me.ec_ricimp.AppearanceCell.Options.UseBackColor = True
    Me.ec_ricimp.AppearanceCell.Options.UseTextOptions = True
    Me.ec_ricimp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_ricimp.Caption = "Provv. a val."
    Me.ec_ricimp.Enabled = True
    Me.ec_ricimp.FieldName = "ec_ricimp"
    Me.ec_ricimp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_ricimp.Name = "ec_ricimp"
    Me.ec_ricimp.NTSRepositoryComboBox = Nothing
    Me.ec_ricimp.NTSRepositoryItemCheck = Nothing
    Me.ec_ricimp.NTSRepositoryItemMemo = Nothing
    Me.ec_ricimp.NTSRepositoryItemText = Nothing
    Me.ec_ricimp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_ricimp.OptionsFilter.AllowFilter = False
    Me.ec_ricimp.UnboundType = DevExpress.Data.UnboundColumnType.[Integer]
    '
    'ec_provv
    '
    Me.ec_provv.AppearanceCell.Options.UseBackColor = True
    Me.ec_provv.AppearanceCell.Options.UseTextOptions = True
    Me.ec_provv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_provv.Caption = "Provv. 1"
    Me.ec_provv.Enabled = True
    Me.ec_provv.FieldName = "ec_provv"
    Me.ec_provv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_provv.Name = "ec_provv"
    Me.ec_provv.NTSRepositoryComboBox = Nothing
    Me.ec_provv.NTSRepositoryItemCheck = Nothing
    Me.ec_provv.NTSRepositoryItemMemo = Nothing
    Me.ec_provv.NTSRepositoryItemText = Nothing
    Me.ec_provv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_provv.OptionsFilter.AllowFilter = False
    Me.ec_provv.Visible = True
    Me.ec_provv.VisibleIndex = 12
    '
    'ec_vprovv
    '
    Me.ec_vprovv.AppearanceCell.Options.UseBackColor = True
    Me.ec_vprovv.AppearanceCell.Options.UseTextOptions = True
    Me.ec_vprovv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_vprovv.Caption = "Importo provv. 1"
    Me.ec_vprovv.Enabled = True
    Me.ec_vprovv.FieldName = "ec_vprovv"
    Me.ec_vprovv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_vprovv.Name = "ec_vprovv"
    Me.ec_vprovv.NTSRepositoryComboBox = Nothing
    Me.ec_vprovv.NTSRepositoryItemCheck = Nothing
    Me.ec_vprovv.NTSRepositoryItemMemo = Nothing
    Me.ec_vprovv.NTSRepositoryItemText = Nothing
    Me.ec_vprovv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_vprovv.OptionsFilter.AllowFilter = False
    '
    'ec_provv2
    '
    Me.ec_provv2.AppearanceCell.Options.UseBackColor = True
    Me.ec_provv2.AppearanceCell.Options.UseTextOptions = True
    Me.ec_provv2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_provv2.Caption = "Provv. 2"
    Me.ec_provv2.Enabled = True
    Me.ec_provv2.FieldName = "ec_provv2"
    Me.ec_provv2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_provv2.Name = "ec_provv2"
    Me.ec_provv2.NTSRepositoryComboBox = Nothing
    Me.ec_provv2.NTSRepositoryItemCheck = Nothing
    Me.ec_provv2.NTSRepositoryItemMemo = Nothing
    Me.ec_provv2.NTSRepositoryItemText = Nothing
    Me.ec_provv2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_provv2.OptionsFilter.AllowFilter = False
    Me.ec_provv2.Visible = True
    Me.ec_provv2.VisibleIndex = 13
    '
    'ec_vprovv2
    '
    Me.ec_vprovv2.AppearanceCell.Options.UseBackColor = True
    Me.ec_vprovv2.AppearanceCell.Options.UseTextOptions = True
    Me.ec_vprovv2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_vprovv2.Caption = "Importo provv. 2"
    Me.ec_vprovv2.Enabled = True
    Me.ec_vprovv2.FieldName = "ec_vprovv2"
    Me.ec_vprovv2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_vprovv2.Name = "ec_vprovv2"
    Me.ec_vprovv2.NTSRepositoryComboBox = Nothing
    Me.ec_vprovv2.NTSRepositoryItemCheck = Nothing
    Me.ec_vprovv2.NTSRepositoryItemMemo = Nothing
    Me.ec_vprovv2.NTSRepositoryItemText = Nothing
    Me.ec_vprovv2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_vprovv2.OptionsFilter.AllowFilter = False
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
    Me.ec_controp.VisibleIndex = 14
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
    Me.xxo_controp.VisibleIndex = 15
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
    Me.ec_codiva.VisibleIndex = 16
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
    Me.xxo_codiva.VisibleIndex = 17
    '
    'ec_stasino
    '
    Me.ec_stasino.AppearanceCell.Options.UseBackColor = True
    Me.ec_stasino.AppearanceCell.Options.UseTextOptions = True
    Me.ec_stasino.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_stasino.Caption = "Stampa riga"
    Me.ec_stasino.Enabled = True
    Me.ec_stasino.FieldName = "ec_stasino"
    Me.ec_stasino.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_stasino.Name = "ec_stasino"
    Me.ec_stasino.NTSRepositoryComboBox = Nothing
    Me.ec_stasino.NTSRepositoryItemCheck = Nothing
    Me.ec_stasino.NTSRepositoryItemMemo = Nothing
    Me.ec_stasino.NTSRepositoryItemText = Nothing
    Me.ec_stasino.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_stasino.OptionsFilter.AllowFilter = False
    Me.ec_stasino.Visible = True
    Me.ec_stasino.VisibleIndex = 18
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
    Me.ec_codcfam.VisibleIndex = 19
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
    Me.xxo_codcfam.VisibleIndex = 20
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
    Me.ec_commeca.VisibleIndex = 21
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
    Me.ec_subcommeca.VisibleIndex = 22
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
    Me.xxo_lottox.VisibleIndex = 23
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
    Me.ec_codcena.VisibleIndex = 24
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
    Me.xxo_codcena.VisibleIndex = 25
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
    Me.ec_note.VisibleIndex = 26
    '
    'ec_valore
    '
    Me.ec_valore.AppearanceCell.Options.UseBackColor = True
    Me.ec_valore.AppearanceCell.Options.UseTextOptions = True
    Me.ec_valore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_valore.Caption = "Valore riga"
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
    Me.ec_valore.VisibleIndex = 27
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
    'xxo_codarfo
    '
    Me.xxo_codarfo.AppearanceCell.Options.UseBackColor = True
    Me.xxo_codarfo.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_codarfo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_codarfo.Caption = "Cod.Art.cli/forn"
    Me.xxo_codarfo.Enabled = True
    Me.xxo_codarfo.FieldName = "xxo_codarfo"
    Me.xxo_codarfo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_codarfo.Name = "xxo_codarfo"
    Me.xxo_codarfo.NTSRepositoryComboBox = Nothing
    Me.xxo_codarfo.NTSRepositoryItemCheck = Nothing
    Me.xxo_codarfo.NTSRepositoryItemMemo = Nothing
    Me.xxo_codarfo.NTSRepositoryItemText = Nothing
    Me.xxo_codarfo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_codarfo.OptionsFilter.AllowFilter = False
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
    Me.ec_perqta.Visible = True
    Me.ec_perqta.VisibleIndex = 28
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
    'ec_ubicaz
    '
    Me.ec_ubicaz.AppearanceCell.Options.UseBackColor = True
    Me.ec_ubicaz.AppearanceCell.Options.UseTextOptions = True
    Me.ec_ubicaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_ubicaz.Caption = "Ubicazione"
    Me.ec_ubicaz.Enabled = True
    Me.ec_ubicaz.FieldName = "ec_ubicaz"
    Me.ec_ubicaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_ubicaz.Name = "ec_ubicaz"
    Me.ec_ubicaz.NTSRepositoryComboBox = Nothing
    Me.ec_ubicaz.NTSRepositoryItemCheck = Nothing
    Me.ec_ubicaz.NTSRepositoryItemMemo = Nothing
    Me.ec_ubicaz.NTSRepositoryItemText = Nothing
    Me.ec_ubicaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_ubicaz.OptionsFilter.AllowFilter = False
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
    'ec_ortipo
    '
    Me.ec_ortipo.AppearanceCell.Options.UseBackColor = True
    Me.ec_ortipo.AppearanceCell.Options.UseTextOptions = True
    Me.ec_ortipo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_ortipo.Caption = "Ord. tipo"
    Me.ec_ortipo.Enabled = False
    Me.ec_ortipo.FieldName = "ec_ortipo"
    Me.ec_ortipo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_ortipo.Name = "ec_ortipo"
    Me.ec_ortipo.NTSRepositoryComboBox = Nothing
    Me.ec_ortipo.NTSRepositoryItemCheck = Nothing
    Me.ec_ortipo.NTSRepositoryItemMemo = Nothing
    Me.ec_ortipo.NTSRepositoryItemText = Nothing
    Me.ec_ortipo.OptionsColumn.AllowEdit = False
    Me.ec_ortipo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_ortipo.OptionsColumn.ReadOnly = True
    Me.ec_ortipo.OptionsFilter.AllowFilter = False
    Me.ec_ortipo.Visible = True
    Me.ec_ortipo.VisibleIndex = 29
    '
    'ec_oranno
    '
    Me.ec_oranno.AppearanceCell.Options.UseBackColor = True
    Me.ec_oranno.AppearanceCell.Options.UseTextOptions = True
    Me.ec_oranno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_oranno.Caption = "Ord. anno"
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
    Me.ec_oranno.Visible = True
    Me.ec_oranno.VisibleIndex = 30
    '
    'ec_orserie
    '
    Me.ec_orserie.AppearanceCell.Options.UseBackColor = True
    Me.ec_orserie.AppearanceCell.Options.UseTextOptions = True
    Me.ec_orserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_orserie.Caption = "Ord. serie"
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
    Me.ec_orserie.Visible = True
    Me.ec_orserie.VisibleIndex = 31
    '
    'ec_ornum
    '
    Me.ec_ornum.AppearanceCell.Options.UseBackColor = True
    Me.ec_ornum.AppearanceCell.Options.UseTextOptions = True
    Me.ec_ornum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_ornum.Caption = "Ord. num"
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
    Me.ec_ornum.Visible = True
    Me.ec_ornum.VisibleIndex = 32
    '
    'ec_orriga
    '
    Me.ec_orriga.AppearanceCell.Options.UseBackColor = True
    Me.ec_orriga.AppearanceCell.Options.UseTextOptions = True
    Me.ec_orriga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_orriga.Caption = "Ord. riga"
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
    Me.ec_orriga.Visible = True
    Me.ec_orriga.VisibleIndex = 33
    '
    'ec_salcon
    '
    Me.ec_salcon.AppearanceCell.Options.UseBackColor = True
    Me.ec_salcon.AppearanceCell.Options.UseTextOptions = True
    Me.ec_salcon.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_salcon.Caption = "Saldo ordine"
    Me.ec_salcon.Enabled = False
    Me.ec_salcon.FieldName = "ec_salcon"
    Me.ec_salcon.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_salcon.Name = "ec_salcon"
    Me.ec_salcon.NTSRepositoryComboBox = Nothing
    Me.ec_salcon.NTSRepositoryItemCheck = Nothing
    Me.ec_salcon.NTSRepositoryItemMemo = Nothing
    Me.ec_salcon.NTSRepositoryItemText = Nothing
    Me.ec_salcon.OptionsColumn.AllowEdit = False
    Me.ec_salcon.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_salcon.OptionsColumn.ReadOnly = True
    Me.ec_salcon.OptionsFilter.AllowFilter = False
    Me.ec_salcon.Visible = True
    Me.ec_salcon.VisibleIndex = 34
    '
    'ec_prtipo
    '
    Me.ec_prtipo.AppearanceCell.Options.UseBackColor = True
    Me.ec_prtipo.AppearanceCell.Options.UseTextOptions = True
    Me.ec_prtipo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_prtipo.Caption = "Imp. tipo"
    Me.ec_prtipo.Enabled = False
    Me.ec_prtipo.FieldName = "ec_prtipo"
    Me.ec_prtipo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_prtipo.Name = "ec_prtipo"
    Me.ec_prtipo.NTSRepositoryComboBox = Nothing
    Me.ec_prtipo.NTSRepositoryItemCheck = Nothing
    Me.ec_prtipo.NTSRepositoryItemMemo = Nothing
    Me.ec_prtipo.NTSRepositoryItemText = Nothing
    Me.ec_prtipo.OptionsColumn.AllowEdit = False
    Me.ec_prtipo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_prtipo.OptionsColumn.ReadOnly = True
    Me.ec_prtipo.OptionsFilter.AllowFilter = False
    '
    'ec_pranno
    '
    Me.ec_pranno.AppearanceCell.Options.UseBackColor = True
    Me.ec_pranno.AppearanceCell.Options.UseTextOptions = True
    Me.ec_pranno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_pranno.Caption = "Imp anno"
    Me.ec_pranno.Enabled = False
    Me.ec_pranno.FieldName = "ec_pranno"
    Me.ec_pranno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_pranno.Name = "ec_pranno"
    Me.ec_pranno.NTSRepositoryComboBox = Nothing
    Me.ec_pranno.NTSRepositoryItemCheck = Nothing
    Me.ec_pranno.NTSRepositoryItemMemo = Nothing
    Me.ec_pranno.NTSRepositoryItemText = Nothing
    Me.ec_pranno.OptionsColumn.AllowEdit = False
    Me.ec_pranno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_pranno.OptionsColumn.ReadOnly = True
    Me.ec_pranno.OptionsFilter.AllowFilter = False
    '
    'ec_prserie
    '
    Me.ec_prserie.AppearanceCell.Options.UseBackColor = True
    Me.ec_prserie.AppearanceCell.Options.UseTextOptions = True
    Me.ec_prserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_prserie.Caption = "Imp. serie"
    Me.ec_prserie.Enabled = False
    Me.ec_prserie.FieldName = "ec_prserie"
    Me.ec_prserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_prserie.Name = "ec_prserie"
    Me.ec_prserie.NTSRepositoryComboBox = Nothing
    Me.ec_prserie.NTSRepositoryItemCheck = Nothing
    Me.ec_prserie.NTSRepositoryItemMemo = Nothing
    Me.ec_prserie.NTSRepositoryItemText = Nothing
    Me.ec_prserie.OptionsColumn.AllowEdit = False
    Me.ec_prserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_prserie.OptionsColumn.ReadOnly = True
    Me.ec_prserie.OptionsFilter.AllowFilter = False
    '
    'ec_prnum
    '
    Me.ec_prnum.AppearanceCell.Options.UseBackColor = True
    Me.ec_prnum.AppearanceCell.Options.UseTextOptions = True
    Me.ec_prnum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_prnum.Caption = "Imp. num"
    Me.ec_prnum.Enabled = False
    Me.ec_prnum.FieldName = "ec_prnum"
    Me.ec_prnum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_prnum.Name = "ec_prnum"
    Me.ec_prnum.NTSRepositoryComboBox = Nothing
    Me.ec_prnum.NTSRepositoryItemCheck = Nothing
    Me.ec_prnum.NTSRepositoryItemMemo = Nothing
    Me.ec_prnum.NTSRepositoryItemText = Nothing
    Me.ec_prnum.OptionsColumn.AllowEdit = False
    Me.ec_prnum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_prnum.OptionsColumn.ReadOnly = True
    Me.ec_prnum.OptionsFilter.AllowFilter = False
    '
    'ec_prriga
    '
    Me.ec_prriga.AppearanceCell.Options.UseBackColor = True
    Me.ec_prriga.AppearanceCell.Options.UseTextOptions = True
    Me.ec_prriga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_prriga.Caption = "Imp. riga"
    Me.ec_prriga.Enabled = False
    Me.ec_prriga.FieldName = "ec_prriga"
    Me.ec_prriga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_prriga.Name = "ec_prriga"
    Me.ec_prriga.NTSRepositoryComboBox = Nothing
    Me.ec_prriga.NTSRepositoryItemCheck = Nothing
    Me.ec_prriga.NTSRepositoryItemMemo = Nothing
    Me.ec_prriga.NTSRepositoryItemText = Nothing
    Me.ec_prriga.OptionsColumn.AllowEdit = False
    Me.ec_prriga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_prriga.OptionsColumn.ReadOnly = True
    Me.ec_prriga.OptionsFilter.AllowFilter = False
    '
    'ec_cltipo
    '
    Me.ec_cltipo.AppearanceCell.Options.UseBackColor = True
    Me.ec_cltipo.AppearanceCell.Options.UseTextOptions = True
    Me.ec_cltipo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_cltipo.Caption = "C/lav. tipo"
    Me.ec_cltipo.Enabled = True
    Me.ec_cltipo.FieldName = "ec_cltipo"
    Me.ec_cltipo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_cltipo.Name = "ec_cltipo"
    Me.ec_cltipo.NTSRepositoryComboBox = Nothing
    Me.ec_cltipo.NTSRepositoryItemCheck = Nothing
    Me.ec_cltipo.NTSRepositoryItemMemo = Nothing
    Me.ec_cltipo.NTSRepositoryItemText = Nothing
    Me.ec_cltipo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_cltipo.OptionsFilter.AllowFilter = False
    Me.ec_cltipo.Visible = True
    Me.ec_cltipo.VisibleIndex = 35
    '
    'ec_clanno
    '
    Me.ec_clanno.AppearanceCell.Options.UseBackColor = True
    Me.ec_clanno.AppearanceCell.Options.UseTextOptions = True
    Me.ec_clanno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_clanno.Caption = "C/lav. anno"
    Me.ec_clanno.Enabled = True
    Me.ec_clanno.FieldName = "ec_clanno"
    Me.ec_clanno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_clanno.Name = "ec_clanno"
    Me.ec_clanno.NTSRepositoryComboBox = Nothing
    Me.ec_clanno.NTSRepositoryItemCheck = Nothing
    Me.ec_clanno.NTSRepositoryItemMemo = Nothing
    Me.ec_clanno.NTSRepositoryItemText = Nothing
    Me.ec_clanno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_clanno.OptionsFilter.AllowFilter = False
    Me.ec_clanno.Visible = True
    Me.ec_clanno.VisibleIndex = 36
    '
    'ec_clserie
    '
    Me.ec_clserie.AppearanceCell.Options.UseBackColor = True
    Me.ec_clserie.AppearanceCell.Options.UseTextOptions = True
    Me.ec_clserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_clserie.Caption = "C/lav. serie"
    Me.ec_clserie.Enabled = True
    Me.ec_clserie.FieldName = "ec_clserie"
    Me.ec_clserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_clserie.Name = "ec_clserie"
    Me.ec_clserie.NTSRepositoryComboBox = Nothing
    Me.ec_clserie.NTSRepositoryItemCheck = Nothing
    Me.ec_clserie.NTSRepositoryItemMemo = Nothing
    Me.ec_clserie.NTSRepositoryItemText = Nothing
    Me.ec_clserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_clserie.OptionsFilter.AllowFilter = False
    Me.ec_clserie.Visible = True
    Me.ec_clserie.VisibleIndex = 37
    '
    'ec_clnum
    '
    Me.ec_clnum.AppearanceCell.Options.UseBackColor = True
    Me.ec_clnum.AppearanceCell.Options.UseTextOptions = True
    Me.ec_clnum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_clnum.Caption = "C/lav. num."
    Me.ec_clnum.Enabled = True
    Me.ec_clnum.FieldName = "ec_clnum"
    Me.ec_clnum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_clnum.Name = "ec_clnum"
    Me.ec_clnum.NTSRepositoryComboBox = Nothing
    Me.ec_clnum.NTSRepositoryItemCheck = Nothing
    Me.ec_clnum.NTSRepositoryItemMemo = Nothing
    Me.ec_clnum.NTSRepositoryItemText = Nothing
    Me.ec_clnum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_clnum.OptionsFilter.AllowFilter = False
    Me.ec_clnum.Visible = True
    Me.ec_clnum.VisibleIndex = 38
    '
    'ec_clriga
    '
    Me.ec_clriga.AppearanceCell.Options.UseBackColor = True
    Me.ec_clriga.AppearanceCell.Options.UseTextOptions = True
    Me.ec_clriga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_clriga.Caption = "C/lav. riga"
    Me.ec_clriga.Enabled = True
    Me.ec_clriga.FieldName = "ec_clriga"
    Me.ec_clriga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_clriga.Name = "ec_clriga"
    Me.ec_clriga.NTSRepositoryComboBox = Nothing
    Me.ec_clriga.NTSRepositoryItemCheck = Nothing
    Me.ec_clriga.NTSRepositoryItemMemo = Nothing
    Me.ec_clriga.NTSRepositoryItemText = Nothing
    Me.ec_clriga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_clriga.OptionsFilter.AllowFilter = False
    Me.ec_clriga.Visible = True
    Me.ec_clriga.VisibleIndex = 39
    '
    'ec_nptipo
    '
    Me.ec_nptipo.AppearanceCell.Options.UseBackColor = True
    Me.ec_nptipo.AppearanceCell.Options.UseTextOptions = True
    Me.ec_nptipo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_nptipo.Caption = "N.pr. tipo"
    Me.ec_nptipo.Enabled = False
    Me.ec_nptipo.FieldName = "ec_nptipo"
    Me.ec_nptipo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_nptipo.Name = "ec_nptipo"
    Me.ec_nptipo.NTSRepositoryComboBox = Nothing
    Me.ec_nptipo.NTSRepositoryItemCheck = Nothing
    Me.ec_nptipo.NTSRepositoryItemMemo = Nothing
    Me.ec_nptipo.NTSRepositoryItemText = Nothing
    Me.ec_nptipo.OptionsColumn.AllowEdit = False
    Me.ec_nptipo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_nptipo.OptionsColumn.ReadOnly = True
    Me.ec_nptipo.OptionsFilter.AllowFilter = False
    Me.ec_nptipo.Visible = True
    Me.ec_nptipo.VisibleIndex = 40
    '
    'ec_npanno
    '
    Me.ec_npanno.AppearanceCell.Options.UseBackColor = True
    Me.ec_npanno.AppearanceCell.Options.UseTextOptions = True
    Me.ec_npanno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_npanno.Caption = "N.pr. anno"
    Me.ec_npanno.Enabled = False
    Me.ec_npanno.FieldName = "ec_npanno"
    Me.ec_npanno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_npanno.Name = "ec_npanno"
    Me.ec_npanno.NTSRepositoryComboBox = Nothing
    Me.ec_npanno.NTSRepositoryItemCheck = Nothing
    Me.ec_npanno.NTSRepositoryItemMemo = Nothing
    Me.ec_npanno.NTSRepositoryItemText = Nothing
    Me.ec_npanno.OptionsColumn.AllowEdit = False
    Me.ec_npanno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_npanno.OptionsColumn.ReadOnly = True
    Me.ec_npanno.OptionsFilter.AllowFilter = False
    Me.ec_npanno.Visible = True
    Me.ec_npanno.VisibleIndex = 41
    '
    'ec_npserie
    '
    Me.ec_npserie.AppearanceCell.Options.UseBackColor = True
    Me.ec_npserie.AppearanceCell.Options.UseTextOptions = True
    Me.ec_npserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_npserie.Caption = "N.pr. serie"
    Me.ec_npserie.Enabled = False
    Me.ec_npserie.FieldName = "ec_npserie"
    Me.ec_npserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_npserie.Name = "ec_npserie"
    Me.ec_npserie.NTSRepositoryComboBox = Nothing
    Me.ec_npserie.NTSRepositoryItemCheck = Nothing
    Me.ec_npserie.NTSRepositoryItemMemo = Nothing
    Me.ec_npserie.NTSRepositoryItemText = Nothing
    Me.ec_npserie.OptionsColumn.AllowEdit = False
    Me.ec_npserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_npserie.OptionsColumn.ReadOnly = True
    Me.ec_npserie.OptionsFilter.AllowFilter = False
    Me.ec_npserie.Visible = True
    Me.ec_npserie.VisibleIndex = 42
    '
    'ec_npnum
    '
    Me.ec_npnum.AppearanceCell.Options.UseBackColor = True
    Me.ec_npnum.AppearanceCell.Options.UseTextOptions = True
    Me.ec_npnum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_npnum.Caption = "N.pr. num"
    Me.ec_npnum.Enabled = False
    Me.ec_npnum.FieldName = "ec_npnum"
    Me.ec_npnum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_npnum.Name = "ec_npnum"
    Me.ec_npnum.NTSRepositoryComboBox = Nothing
    Me.ec_npnum.NTSRepositoryItemCheck = Nothing
    Me.ec_npnum.NTSRepositoryItemMemo = Nothing
    Me.ec_npnum.NTSRepositoryItemText = Nothing
    Me.ec_npnum.OptionsColumn.AllowEdit = False
    Me.ec_npnum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_npnum.OptionsColumn.ReadOnly = True
    Me.ec_npnum.OptionsFilter.AllowFilter = False
    Me.ec_npnum.Visible = True
    Me.ec_npnum.VisibleIndex = 43
    '
    'ec_npriga
    '
    Me.ec_npriga.AppearanceCell.Options.UseBackColor = True
    Me.ec_npriga.AppearanceCell.Options.UseTextOptions = True
    Me.ec_npriga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_npriga.Caption = "N.pr. riga"
    Me.ec_npriga.Enabled = False
    Me.ec_npriga.FieldName = "ec_npriga"
    Me.ec_npriga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_npriga.Name = "ec_npriga"
    Me.ec_npriga.NTSRepositoryComboBox = Nothing
    Me.ec_npriga.NTSRepositoryItemCheck = Nothing
    Me.ec_npriga.NTSRepositoryItemMemo = Nothing
    Me.ec_npriga.NTSRepositoryItemText = Nothing
    Me.ec_npriga.OptionsColumn.AllowEdit = False
    Me.ec_npriga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_npriga.OptionsColumn.ReadOnly = True
    Me.ec_npriga.OptionsFilter.AllowFilter = False
    Me.ec_npriga.Visible = True
    Me.ec_npriga.VisibleIndex = 44
    '
    'ec_npqtadis
    '
    Me.ec_npqtadis.AppearanceCell.Options.UseBackColor = True
    Me.ec_npqtadis.AppearanceCell.Options.UseTextOptions = True
    Me.ec_npqtadis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_npqtadis.Caption = "N.pr. qta disimp."
    Me.ec_npqtadis.Enabled = False
    Me.ec_npqtadis.FieldName = "ec_npqtadis"
    Me.ec_npqtadis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_npqtadis.Name = "ec_npqtadis"
    Me.ec_npqtadis.NTSRepositoryComboBox = Nothing
    Me.ec_npqtadis.NTSRepositoryItemCheck = Nothing
    Me.ec_npqtadis.NTSRepositoryItemMemo = Nothing
    Me.ec_npqtadis.NTSRepositoryItemText = Nothing
    Me.ec_npqtadis.OptionsColumn.AllowEdit = False
    Me.ec_npqtadis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_npqtadis.OptionsColumn.ReadOnly = True
    Me.ec_npqtadis.OptionsFilter.AllowFilter = False
    '
    'ec_npcoldis
    '
    Me.ec_npcoldis.AppearanceCell.Options.UseBackColor = True
    Me.ec_npcoldis.AppearanceCell.Options.UseTextOptions = True
    Me.ec_npcoldis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_npcoldis.Caption = "N.pr. colli disimp."
    Me.ec_npcoldis.Enabled = False
    Me.ec_npcoldis.FieldName = "ec_npcoldis"
    Me.ec_npcoldis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_npcoldis.Name = "ec_npcoldis"
    Me.ec_npcoldis.NTSRepositoryComboBox = Nothing
    Me.ec_npcoldis.NTSRepositoryItemCheck = Nothing
    Me.ec_npcoldis.NTSRepositoryItemMemo = Nothing
    Me.ec_npcoldis.NTSRepositoryItemText = Nothing
    Me.ec_npcoldis.OptionsColumn.AllowEdit = False
    Me.ec_npcoldis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_npcoldis.OptionsColumn.ReadOnly = True
    Me.ec_npcoldis.OptionsFilter.AllowFilter = False
    '
    'ec_npvaldis
    '
    Me.ec_npvaldis.AppearanceCell.Options.UseBackColor = True
    Me.ec_npvaldis.AppearanceCell.Options.UseTextOptions = True
    Me.ec_npvaldis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_npvaldis.Caption = "N.pr. val. disimp."
    Me.ec_npvaldis.Enabled = False
    Me.ec_npvaldis.FieldName = "ec_npvaldis"
    Me.ec_npvaldis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_npvaldis.Name = "ec_npvaldis"
    Me.ec_npvaldis.NTSRepositoryComboBox = Nothing
    Me.ec_npvaldis.NTSRepositoryItemCheck = Nothing
    Me.ec_npvaldis.NTSRepositoryItemMemo = Nothing
    Me.ec_npvaldis.NTSRepositoryItemText = Nothing
    Me.ec_npvaldis.OptionsColumn.AllowEdit = False
    Me.ec_npvaldis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_npvaldis.OptionsColumn.ReadOnly = True
    Me.ec_npvaldis.OptionsFilter.AllowFilter = False
    '
    'ec_npsalcon
    '
    Me.ec_npsalcon.AppearanceCell.Options.UseBackColor = True
    Me.ec_npsalcon.AppearanceCell.Options.UseTextOptions = True
    Me.ec_npsalcon.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_npsalcon.Caption = "N.pr. evasa"
    Me.ec_npsalcon.Enabled = False
    Me.ec_npsalcon.FieldName = "ec_npsalcon"
    Me.ec_npsalcon.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_npsalcon.Name = "ec_npsalcon"
    Me.ec_npsalcon.NTSRepositoryComboBox = Nothing
    Me.ec_npsalcon.NTSRepositoryItemCheck = Nothing
    Me.ec_npsalcon.NTSRepositoryItemMemo = Nothing
    Me.ec_npsalcon.NTSRepositoryItemText = Nothing
    Me.ec_npsalcon.OptionsColumn.AllowEdit = False
    Me.ec_npsalcon.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_npsalcon.OptionsColumn.ReadOnly = True
    Me.ec_npsalcon.OptionsFilter.AllowFilter = False
    '
    'ec_nprcoleva
    '
    Me.ec_nprcoleva.AppearanceCell.Options.UseBackColor = True
    Me.ec_nprcoleva.AppearanceCell.Options.UseTextOptions = True
    Me.ec_nprcoleva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_nprcoleva.Caption = "Colli evasi"
    Me.ec_nprcoleva.Enabled = False
    Me.ec_nprcoleva.FieldName = "ec_nprcoleva"
    Me.ec_nprcoleva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_nprcoleva.Name = "ec_nprcoleva"
    Me.ec_nprcoleva.NTSRepositoryComboBox = Nothing
    Me.ec_nprcoleva.NTSRepositoryItemCheck = Nothing
    Me.ec_nprcoleva.NTSRepositoryItemMemo = Nothing
    Me.ec_nprcoleva.NTSRepositoryItemText = Nothing
    Me.ec_nprcoleva.OptionsColumn.AllowEdit = False
    Me.ec_nprcoleva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_nprcoleva.OptionsColumn.ReadOnly = True
    Me.ec_nprcoleva.OptionsFilter.AllowFilter = False
    '
    'ec_nprquaeva
    '
    Me.ec_nprquaeva.AppearanceCell.Options.UseBackColor = True
    Me.ec_nprquaeva.AppearanceCell.Options.UseTextOptions = True
    Me.ec_nprquaeva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_nprquaeva.Caption = "Q.tà evasa"
    Me.ec_nprquaeva.Enabled = False
    Me.ec_nprquaeva.FieldName = "ec_nprquaeva"
    Me.ec_nprquaeva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_nprquaeva.Name = "ec_nprquaeva"
    Me.ec_nprquaeva.NTSRepositoryComboBox = Nothing
    Me.ec_nprquaeva.NTSRepositoryItemCheck = Nothing
    Me.ec_nprquaeva.NTSRepositoryItemMemo = Nothing
    Me.ec_nprquaeva.NTSRepositoryItemText = Nothing
    Me.ec_nprquaeva.OptionsColumn.AllowEdit = False
    Me.ec_nprquaeva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_nprquaeva.OptionsColumn.ReadOnly = True
    Me.ec_nprquaeva.OptionsFilter.AllowFilter = False
    '
    'ec_nprflevas
    '
    Me.ec_nprflevas.AppearanceCell.Options.UseBackColor = True
    Me.ec_nprflevas.AppearanceCell.Options.UseTextOptions = True
    Me.ec_nprflevas.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_nprflevas.Caption = "Evaso"
    Me.ec_nprflevas.Enabled = False
    Me.ec_nprflevas.FieldName = "ec_nprflevas"
    Me.ec_nprflevas.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_nprflevas.Name = "ec_nprflevas"
    Me.ec_nprflevas.NTSRepositoryComboBox = Nothing
    Me.ec_nprflevas.NTSRepositoryItemCheck = Nothing
    Me.ec_nprflevas.NTSRepositoryItemMemo = Nothing
    Me.ec_nprflevas.NTSRepositoryItemText = Nothing
    Me.ec_nprflevas.OptionsColumn.AllowEdit = False
    Me.ec_nprflevas.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_nprflevas.OptionsColumn.ReadOnly = True
    Me.ec_nprflevas.OptionsFilter.AllowFilter = False
    '
    'ec_nprvalore
    '
    Me.ec_nprvalore.AppearanceCell.Options.UseBackColor = True
    Me.ec_nprvalore.AppearanceCell.Options.UseTextOptions = True
    Me.ec_nprvalore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_nprvalore.Caption = "Valore nota prel."
    Me.ec_nprvalore.Enabled = False
    Me.ec_nprvalore.FieldName = "ec_nprvalore"
    Me.ec_nprvalore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_nprvalore.Name = "ec_nprvalore"
    Me.ec_nprvalore.NTSRepositoryComboBox = Nothing
    Me.ec_nprvalore.NTSRepositoryItemCheck = Nothing
    Me.ec_nprvalore.NTSRepositoryItemMemo = Nothing
    Me.ec_nprvalore.NTSRepositoryItemText = Nothing
    Me.ec_nprvalore.OptionsColumn.AllowEdit = False
    Me.ec_nprvalore.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_nprvalore.OptionsColumn.ReadOnly = True
    Me.ec_nprvalore.OptionsFilter.AllowFilter = False
    '
    'ec_valorev
    '
    Me.ec_valorev.AppearanceCell.Options.UseBackColor = True
    Me.ec_valorev.AppearanceCell.Options.UseTextOptions = True
    Me.ec_valorev.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_valorev.Caption = "Valore in val."
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
    'ec_tctaglia
    '
    Me.ec_tctaglia.AppearanceCell.Options.UseBackColor = True
    Me.ec_tctaglia.AppearanceCell.Options.UseTextOptions = True
    Me.ec_tctaglia.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_tctaglia.Caption = "Taglia padre"
    Me.ec_tctaglia.Enabled = True
    Me.ec_tctaglia.FieldName = "ec_tctaglia"
    Me.ec_tctaglia.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_tctaglia.Name = "ec_tctaglia"
    Me.ec_tctaglia.NTSRepositoryComboBox = Nothing
    Me.ec_tctaglia.NTSRepositoryItemCheck = Nothing
    Me.ec_tctaglia.NTSRepositoryItemMemo = Nothing
    Me.ec_tctaglia.NTSRepositoryItemText = Nothing
    Me.ec_tctaglia.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_tctaglia.OptionsFilter.AllowFilter = False
    Me.ec_tctaglia.Visible = True
    Me.ec_tctaglia.VisibleIndex = 45
    '
    'ec_coddivi
    '
    Me.ec_coddivi.AppearanceCell.Options.UseBackColor = True
    Me.ec_coddivi.AppearanceCell.Options.UseTextOptions = True
    Me.ec_coddivi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_coddivi.Caption = "Divisione CA"
    Me.ec_coddivi.Enabled = True
    Me.ec_coddivi.FieldName = "ec_coddivi"
    Me.ec_coddivi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_coddivi.Name = "ec_coddivi"
    Me.ec_coddivi.NTSRepositoryComboBox = Nothing
    Me.ec_coddivi.NTSRepositoryItemCheck = Nothing
    Me.ec_coddivi.NTSRepositoryItemMemo = Nothing
    Me.ec_coddivi.NTSRepositoryItemText = Nothing
    Me.ec_coddivi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_coddivi.OptionsFilter.AllowFilter = False
    '
    'xxo_coddivi
    '
    Me.xxo_coddivi.AppearanceCell.Options.UseBackColor = True
    Me.xxo_coddivi.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_coddivi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_coddivi.Caption = "Descr. divis. CA"
    Me.xxo_coddivi.Enabled = False
    Me.xxo_coddivi.FieldName = "xxo_coddivi"
    Me.xxo_coddivi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_coddivi.Name = "xxo_coddivi"
    Me.xxo_coddivi.NTSRepositoryComboBox = Nothing
    Me.xxo_coddivi.NTSRepositoryItemCheck = Nothing
    Me.xxo_coddivi.NTSRepositoryItemMemo = Nothing
    Me.xxo_coddivi.NTSRepositoryItemText = Nothing
    Me.xxo_coddivi.OptionsColumn.AllowEdit = False
    Me.xxo_coddivi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_coddivi.OptionsColumn.ReadOnly = True
    Me.xxo_coddivi.OptionsFilter.AllowFilter = False
    '
    'FRMVESCAL
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(740, 442)
    Me.Controls.Add(Me.pnGriglia)
    Me.Controls.Add(Me.pnCorpo)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMVESCAL"
    Me.Text = "SCARICHI COLLEGATI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnCorpo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCorpo.ResumeLayout(False)
    Me.pnCorpo.PerformLayout()
    CType(Me.edUltCost.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPreList.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDispNetta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDispon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnGriglia, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGriglia.ResumeLayout(False)
    CType(Me.grRighe, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvRighe, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overloads Function InitEntity(ByRef Menu As CLE__MENU, ByRef frmVEBOLL As FRMVEBOLL, ByRef ds As DataSet) As Boolean
    Dim dttSc As New DataTable
    oMenu = Menu
    oApp = oMenu.App
    frmParent = frmVEBOLL       'form PARENT
    DittaCorrente = oCleBoll.strDittaCorrente
    Me.GctlTipoDoc = "U"

    InitializeComponent()
    Me.MinimumSize = Me.Size

    '-------------------------------
    'leggo dal database i dati e collego il NTSBinding
    ds.AcceptChanges()
    oCleBoll.dsImpe = ds
    oCleBoll.CorpoImpSetDataTable(DittaCorrente, oCleBoll.dsImpe.Tables("CORPOIMP"))
    dcImpe.DataSource = oCleBoll.dsImpe.Tables("CORPOIMP")
    oCleBoll.dsImpe.AcceptChanges()

    '------------------------------
    'collego gli eventi dell'entity
    AddHandler oCleBoll.RemoteEvent, AddressOf GestisciEventiEntity

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim i As Integer = 0
    Dim dttStasino As New DataTable()
    Dim dttKit As New DataTable()
    Dim dttOatipo As New DataTable()
    Dim dttTask As New DataTable()
    Dim dttTipork As New DataTable()
    Dim dttCltipo As New DataTable()
    Dim dttNptipo As New DataTable()

    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\recnew.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\recagg.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\recdelete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\recrestore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      edUltCost.NTSSetParam(oMenu, oApp.Tr(Me, 128230023236442158, "Ultimo costo"), oApp.FormatPrzUn)
      edPreList.NTSSetParam(oMenu, oApp.Tr(Me, 128230023236598331, "Prezzo di listino"), oApp.FormatPrzUn)
      edDispNetta.NTSSetParam(oMenu, oApp.Tr(Me, 128230023236754504, "Disponibilità netta"), oCleBoll.strFormatQtaEsistCorpo)
      edDispon.NTSSetParam(oMenu, oApp.Tr(Me, 128230023236910677, "Disponibilità"), oCleBoll.strFormatQtaEsistCorpo)


      '-------------------------------------------------
      'la griglia
      dttStasino.Columns.Add("cod", GetType(String))
      dttStasino.Columns.Add("val", GetType(String))
      dttStasino.Rows.Add(New Object() {"S", "Si"})
      dttStasino.Rows.Add(New Object() {"N", "No"})
      dttStasino.Rows.Add(New Object() {"B", "Solo in bolla"})
      dttStasino.Rows.Add(New Object() {"D", "Solo in fattura"})
      dttStasino.Rows.Add(New Object() {"O", "Omaggi (imponibile)"})
      dttStasino.Rows.Add(New Object() {"M", "Sconto merce"})
      dttStasino.Rows.Add(New Object() {"X", "Sconto merce NC"})
      dttStasino.Rows.Add(New Object() {"P", "Omaggi (imp. + IVA)"})
      dttStasino.AcceptChanges()

      dttTipork.Columns.Add("cod", GetType(String))
      dttTipork.Columns.Add("val", GetType(String))
      dttTipork.Rows.Add(New Object() {"#", "Impegno di commessa"})
      dttTipork.Rows.Add(New Object() {"$", "Ord. for. aperto"})
      dttTipork.Rows.Add(New Object() {"H", "Ordine di produzione"})
      dttTipork.Rows.Add(New Object() {"O", "Ordine fornitore"})
      dttTipork.Rows.Add(New Object() {"Q", "Preventivo"})
      dttTipork.Rows.Add(New Object() {"R", "Impegno cliente"})
      dttTipork.Rows.Add(New Object() {"V", "Imp.cli.aperto"})
      dttTipork.Rows.Add(New Object() {"X", "Impegno Trasferimento"})
      dttTipork.Rows.Add(New Object() {"Y", "Impegno di produzione"})
      dttTipork.AcceptChanges()

      dttKit.Columns.Add("cod", GetType(String))
      dttKit.Columns.Add("val", GetType(String))
      dttKit.Rows.Add(New Object() {" ", "Nessuno"})
      dttKit.Rows.Add(New Object() {"A", "Analitico"})
      dttKit.Rows.Add(New Object() {"S", "Sintetico"})
      dttKit.Rows.Add(New Object() {"B", "Comp. analitica"})
      dttKit.Rows.Add(New Object() {"T", "Comp. sintetica"})
      dttKit.AcceptChanges()

      dttOatipo.Columns.Add("cod", GetType(String))
      dttOatipo.Columns.Add("val", GetType(String))
      dttOatipo.Rows.Add(New Object() {"O", "Ordine fornitore"})
      dttOatipo.Rows.Add(New Object() {"R", "Impegno cliente"})
      dttOatipo.Rows.Add(New Object() {"V", "Impegno cliente aperto"})
      dttOatipo.Rows.Add(New Object() {"$", "Ordine fornitore aperto"})
      dttOatipo.AcceptChanges()

      dttTask.Columns.Add("cod", GetType(String))
      dttTask.Columns.Add("val", GetType(String))
      dttTask.Rows.Add(New Object() {"S", "Saldato"})
      dttTask.Rows.Add(New Object() {"C", "Aperto"})
      dttTask.Rows.Add(New Object() {"Q", "Q"})
      dttTask.Rows.Add(New Object() {"V", "B"})
      dttTask.AcceptChanges()

      dttCltipo.Columns.Add("cod", GetType(String))
      dttCltipo.Columns.Add("val", GetType(String))
      dttCltipo.Rows.Add(New Object() {"", ""})
      dttCltipo.Rows.Add(New Object() {"A", "Fattura Immediata Emessa"})
      dttCltipo.Rows.Add(New Object() {"B", "D.D.T. Emesso"})
      dttCltipo.Rows.Add(New Object() {"C", "Corrispettivo Emesso"})
      dttCltipo.Rows.Add(New Object() {"E", "Nota di Addebito Emessa"})
      dttCltipo.Rows.Add(New Object() {"J", "Nota di Accredito Ricevuta"})
      dttCltipo.Rows.Add(New Object() {"L", "Fattura Immediata Ricevuta"})
      dttCltipo.Rows.Add(New Object() {"M", "D.D.T. Ricevuto"})
      dttCltipo.Rows.Add(New Object() {"N", "Nota di Accerdito Emessa"})
      dttCltipo.Rows.Add(New Object() {"Z", "Bolla di Movimentazione Interna"})
      dttCltipo.Rows.Add(New Object() {"T", "Carico da Produzione"})
      dttCltipo.AcceptChanges()

      dttNptipo.Columns.Add("cod", GetType(String))
      dttNptipo.Columns.Add("val", GetType(String))
      dttNptipo.Rows.Add(New Object() {"", ""})
      dttNptipo.Rows.Add(New Object() {"W", "Nota di Prelievo"})
      dttNptipo.AcceptChanges()

      '-------------------------------------------------
      'carico le unità di misura nella colonna colli: caso particolare
      'carico tutte le unità di misura gestite dagli articoli, 
      'poi al cambio di riga filtrero nel combo solo quelle gestite dall'articolo in analisi
      dttUm = oCleBoll.CaricaUnMis()
      dttUm.AcceptChanges()

      '-------------------------------------------------
      grvRighe.NTSMenuContext = tlbStrumenti

      grvRighe.NTSSetParam(oMenu, oApp.Tr(Me, 128230023247061922, "Griglia righe scarichi"))
      ec_riga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023247218095, "Riga"), "0", 9, 0, 999999999)
      ec_matric.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023247374268, "Barcode"), 255, False)
      ec_codart.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128230023247530441, "Cod. Art."), tabartico, False)
      ec_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023247686614, "Descrizione"), 40, True)
      ec_desint.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023247842787, "Descr.interna"), 40, True)
      ec_unmis.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128230023247998960, "U.M."), dttUm, "tb_codumis", "tb_codumis")
      ec_colli.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023248155133, "Colli"), oApp.FormatQta, 13)
      ec_ump.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023248623652, "UMP"), 3, False)
      ec_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023248779825, "Q.tà  ordin."), oApp.FormatQta, 13)
      ec_preziva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023249873036, "Prezzo IVA inc."), oApp.FormatPrzUn, 13)
      ec_prezvalc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023250029209, "Prezzo valuta"), oApp.FormatPrzUnVal, 13)
      ec_prezzo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023250185382, "Prezzo"), oApp.FormatPrzUn, 13)
      ec_magaz.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023251278593, "Magazzino"), tabmaga)
      xxo_magaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023251434766, "Descr. magazzino"), 0, True)
      ec_ricimp.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023252215631, "Provv. a val."), "S", "N")
      ec_provv.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023252371804, "Provv. 1"), oApp.FormatSconti, 6)
      ec_vprovv.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023252527977, "Importo provv. 1"), oApp.FormatImporti, 13)
      ec_provv2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023252684150, "Provv. 2"), oApp.FormatSconti, 6)
      ec_vprovv2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023252840323, "Importo provv. 2"), oApp.FormatImporti, 13)
      ec_controp.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023252996496, "Controp."), tabcove)
      xxo_controp.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023253152669, "Descr. controp."), 0, True)
      ec_codiva.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023253308842, "Cod. IVA"), tabciva)
      xxo_codiva.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023253465015, "Descr. IVA"), 0, True)
      ec_stasino.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128230023253621188, "Stampa riga"), dttStasino, "val", "cod")
      ec_codcfam.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128230023253777361, "Linea/Fam."), tabcfam, False)
      xxo_codcfam.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023253933534, "Descr. linea/fam"), 0, True)
      ec_commeca.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023254089707, "Comm. C.A."), tabcommess)
      xxo_commeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023254245880, "Descr. commessa"), 0, True)
      ec_subcommeca.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128230023254402053, "Sub C."), tabsubcomm, True)
      If oCleBoll.bLottoNew = False Then
        xxo_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130420303600201011, "Lotto"), 9, True)
      Else
        xxo_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130420303897077911, "Lotto"), 50, True)
      End If
      ec_codcena.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023254714399, "Centro C.A."), tabcena)
      xxo_codcena.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023254870572, "Descr. centro"), 0, True)
      ec_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023255026745, "Note"), 0, True)
      ec_valore.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023255495264, "Valore riga"), oApp.FormatImporti, 13)
      ec_contocontr.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023255651437, "Conto controp."), tabanagra)
      xxo_contocon.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023255807610, "Descr. conto controp."), 0, True)
      ec_codclie.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023256119956, "Cod. cliente"), tabanagra)
      xxo_codclie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023256276129, "Descr. cliente"), 0, True)
      ec_misura1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023256432302, "Misura 1"), oApp.FormatQta, 13)
      ec_misura2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023256588475, "Misura 2"), oApp.FormatQta, 13)
      ec_misura3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023256744648, "Misura 3"), oApp.FormatQta, 13)
      xxo_codarfo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023256900821, "Cod.Art.cli/forn"), 0, True)
      ec_perqta.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023257056994, "Prz/Qta"), "#,##0.00", 13)
      ec_fase.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023259243416, "Fase"), tabartfasi)
      ec_fase.ArtiPerFase = ec_codart
      xxo_fase.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023259399589, "Descr. fase"), 0, True)
      ec_ubicaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023261429838, "Ubicazione"), 18, False)
      xxo_codtagl.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023261586011, "."), "0", 4, 0, 9999)
      ec_causale.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128654458149843750, "Causale"), tabcaum)
      xxo_causale.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128654458150000000, "Descr. causale"), 0)
      ec_ortipo.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128654458159218750, "Ord. tipo"), dttTipork, "val", "cod")
      ec_oranno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128654458159375000, "Ord. anno"), "0", 4, 0, 9999)
      ec_orserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128654458159531250, "Ord. serie"), CLN__STD.SerieMaxLen, True)
      ec_ornum.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128654458159687500, "Ord. num"), "0", 9, 0, 999999999)
      ec_orriga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128654458159843750, "Ord. riga"), "0", 9, 0, 999999999)
      ec_salcon.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128654458160000000, "Saldo ordine"), "S", "C")
      ec_prtipo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128654458161250000, "Imp. tipo"), 1, True)
      ec_pranno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128654458161406250, "Imp anno"), "0", 4, 0, 9999)
      ec_prserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128654458161562500, "Imp. serie"), CLN__STD.SerieMaxLen, True)
      ec_prnum.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128654458161718750, "Imp. num"), "0", 9, 0, 999999999)
      ec_prriga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128654458161875000, "Imp. riga"), "0", 9, 0, 999999999)
      ec_cltipo.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128654458162031250, "C/lav. tipo"), dttCltipo, "val", "cod")
      ec_clanno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128654458162187500, "C/lav. anno"), "0", 4, 0, 9999)
      ec_clserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128654458162343750, "C/lav. serie"), CLN__STD.SerieMaxLen, True)
      ec_clnum.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128654458162500000, "C/lav. num."), "0", 9, 0, 999999999)
      ec_clriga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128654458162656250, "C/lav. riga"), "0", 9, 0, 999999999)
      ec_nptipo.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128654458162812500, "N.pr. tipo"), dttNptipo, "val", "cod")
      ec_npanno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128654458162968750, "N.pr. anno"), "0", 4, 0, 9999)
      ec_npserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128654458163125000, "N.pr. serie"), CLN__STD.SerieMaxLen, True)
      ec_npnum.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128654458163281250, "N.pr. num"), "0", 9, 0, 999999999)
      ec_npriga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128654458163437500, "N.pr. riga"), "0", 9, 0, 999999999)
      ec_npqtadis.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128654458163593750, "N.pr. qta disimp."), oApp.FormatQta, 15)
      ec_npcoldis.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128654458163750000, "N.pr. colli disimp."), oApp.FormatQta, 15)
      ec_npvaldis.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128654458163906250, "N.pr. val. disimp."), oApp.FormatImporti, 15)
      ec_npsalcon.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128654458164062500, "N.pr. evasa"), "S", "C")
      ec_nprcoleva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128654458164218750, "Colli evasi"), oApp.FormatQta, 15)
      ec_nprquaeva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128654458164375000, "Q.tà evasa"), oApp.FormatQta, 15)
      ec_nprflevas.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128654458164531250, "Evaso"), "S", "C")
      ec_nprvalore.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128654458164687500, "Valore nota prel."), oApp.FormatImporti, 15)
      ec_valorev.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128654458167031250, "Valore in val."), oApp.FormatImporti, 15)
      ec_tctaglia.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128587043410781250, "Taglia padre"), 5, False)
      ec_coddivi.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 130420304161923356, "Divisione C/A"), tabdivi)
      xxo_coddivi.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129270307322824062, "Descr. Divisione CA"), 0, True)
      xxo_tctagliaf.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048536071577200, "Taglia figlio"), 4, False)

      ec_prezzo.NTSSetParamZoom("ZOOMPREZZO")
      ec_prezvalc.NTSSetParamZoom("ZOOMPREZZO")
      ec_preziva.NTSSetParamZoom("ZOOMPREZZO")
      xxo_lottox.NTSSetParamZoom("ZOOMANALOTTI")

      ec_codart.NTSSetRichiesto()
      ec_quant.NTSSetRichiesto()
      ec_prezzo.NTSSetRichiesto()
      ec_codiva.NTSSetRichiesto()
      ec_controp.NTSSetRichiesto()
      ec_causale.NTSSetRichiesto()
      ec_magaz.NTSSetRichiesto()

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
  Public Overridable Sub FRMVESCAL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      grRighe.DataSource = dcImpe

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      '-------------------------------------------------
      'visualizzo/nascondo il cod taglia al bisogno
      If NTSCInt(oCleBoll.dtrHT!xxo_codtagl) = 0 Then
        ec_tctaglia.Visible = False
        ec_tctaglia.Enabled = False
        xxo_tctagliaf.Visible = False
        xxo_tctagliaf.Enabled = False
      Else
        GctlSetVisEnab(ec_tctaglia, True)
        GctlSetVisEnab(ec_tctaglia, False)
        If oCleBoll.bGestioneAbbinamentiTaglie Then
          GctlSetVisEnab(xxo_tctagliaf, True)
          GctlSetVisEnab(xxo_tctagliaf, False)
        Else
          xxo_tctagliaf.Visible = False
          xxo_tctagliaf.Enabled = False
        End If
      End If

      If NTSCInt(oCleBoll.dttET.Rows(0)!et_valuta) <> 0 Then
        GctlSetVisEnab(ec_prezvalc, True)
      Else
        ec_prezvalc.Visible = False
      End If

      'come in vb6
      ec_nprcoleva.Visible = False
      ec_nprquaeva.Visible = False
      ec_nprflevas.Visible = False
      ec_nprcoleva.Enabled = False
      ec_nprquaeva.Enabled = False
      ec_nprflevas.Enabled = False

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMVESCAL_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try

  End Sub

  Public Overridable Sub FRMVESCAL_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    'salvo l'impostazione della griglia. devo farlo anche al cambio del tipo documento (ad esempio in bsveboll
    Try
      dcImpe.Dispose()
      oCleBoll.dsImpe.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvRighe.NTSNuovo()

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
    Dim dtrDeleted As DataRow = Nothing
    Try
      If grvRighe.Focused = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129055188534751153, "Posizionarsi prima nella griglia e selezionare la riga da cancellare"))
        Return
      End If
      Me.Cursor = Cursors.WaitCursor
      If Not oCleBoll.CorpoImpTestPreCancella(dcImpe.Position) Then Return
      If Not grvRighe.NTSDeleteRigaCorrente(dcImpe, True, dtrDeleted) Then Return
      If Not oCleBoll.CorpoImpRecordSalva(dcImpe.Position, True, dtrDeleted) Then Return

      grvRighe_NTSFocusedRowChanged(grvRighe, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvRighe.NTSRipristinaRigaCorrenteBefore(dcImpe, True) Then Return
      oCleBoll.CorpoImpRipristina(dcImpe.Position, dcImpe.Filter)
      grvRighe.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    'SendKeys.SendWait("{F5}")
    'se faccio la riga sopra va in un loop infinito....
    'devo vedere quale controllo ha il focus, quindi lanciare lo zoom direttamente sul controllo, senza forzare l'F5
    Dim ctrlTmp As Control = Nothing
    Dim oParam As New CLE__PATB

    Try
      '------------------------------------
      'zoom standard
      ctrlTmp = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      If ctrlTmp.GetType.ToString.IndexOf("NTSGrid") > -1 Then
        SetFastZoom(NTSCStr(grvRighe.EditingValue), oParam)    'abilito la gestione dello zoom veloce

        If grvRighe.FocusedColumn.Equals(ec_codart) Then
          '------------------------------------
          'colonna particolare: codice articolo
          NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
          oParam.nMagaz = 0
          oParam.nListino = nEt_listino
          oParam.lContoCF = oCleBoll.lContoCF
          oParam.bTipoProposto = False       'abilito la possibilità di selezionare + articoli
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
                  oCleBoll.bInInsertArticoDaZoom = True
                  If Not oCleBoll.CorpoImpRecordSalva(dcImpe.Position, False, Nothing) Then Return
                  For Each dtrT As DataRow In CType(oParam.oParam, DataTable).Rows
                    oCleBoll.dsImpe.Tables("CORPOIMP").Rows.Add(oCleBoll.dsImpe.Tables("CORPOIMP").NewRow)
                    With oCleBoll.dsImpe.Tables("CORPOIMP").Rows(oCleBoll.dsImpe.Tables("CORPOIMP").Rows.Count - 1)
                      'forzo la MovordOnAddNewRow
                      !codditt = "."
                      !codditt = DittaCorrente
                      !ec_codart = dtrT!codart.ToString
                      If NTSCInt(!xxo_codtagl) = 0 Then
                        !ec_quant = 1
                      End If
                    End With
                    If Not oCleBoll.CorpoImpRecordSalva(oCleBoll.dsImpe.Tables("CORPOIMP").Rows.Count - 1, False, Nothing) Then
                      oCleBoll.dsImpe.Tables("CORPOIMP").Rows(oCleBoll.dsImpe.Tables("CORPOIMP").Rows.Count - 1).Delete()
                    End If
                  Next
                  oCleBoll.bInInsertArticoDaZoom = False
                End If
                CType(oParam.oParam, DataTable).Clear()
              End If
            End If
          Else
            If NTSZOOM.strIn <> NTSCStr(grvRighe.EditingValue) Then grvRighe.SetFocusedValue(NTSZOOM.strIn)
          End If

        ElseIf grvRighe.FocusedColumn.Equals(ec_fase) Then
          '------------------------------------
          'zoom fasi articoli
          NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
          oParam.strTipo = NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart))
          NTSZOOM.ZoomStrIn("ZOOMARTFASI", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvRighe.EditingValue) Then grvRighe.SetFocusedValue(NTSZOOM.strIn)

        ElseIf grvRighe.FocusedColumn.Equals(ec_subcommeca) Then
          '------------------------------------
          'zoom subcommessa
          If NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_commeca)) = 0 Then
            oApp.MsgBoxInfo(oApp.Tr(Me, 129055188649446727, "Indicare prima il codice commessa"))
            Return
          End If
          NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
          oParam.lCommessa = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_commeca))   'passo il codice della commessa
          NTSZOOM.ZoomStrIn("ZOOMSUBCOMM", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvRighe.EditingValue) Then grvRighe.SetFocusedValue(NTSZOOM.strIn)

        ElseIf grvRighe.FocusedColumn.Equals(xxo_lottox) Then
          '------------------------------------
          'zoom lotti
          If NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart)).Trim = "" Then
            oApp.MsgBoxInfo(oApp.Tr(Me, 128776116955716000, "Indicare prima il codice articolo"))
            Return
          End If
          NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
          oParam.strTipo = NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart))
          'oParam.nMagaz = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_magaz))   'serve per visual solo i lotti aperti
          oParam.nAnno = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_fase))     'serve per visual solo i lotti aperti
          'oParam.strDatreg = NTSCDate(edet_datdoc.Text).ToShortDateString                          'serve per visual solo i lotti aperti
          NTSZOOM.ZoomStrIn("ZOOMANALOTTI", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvRighe.EditingValue) Then grvRighe.SetFocusedValue(NTSZOOM.strIn)

        ElseIf grvRighe.FocusedColumn.Equals(ec_ubicaz) Then
          '------------------------------------
          'zoom ubicazioni
          If NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart)).Trim = "" Then
            oApp.MsgBoxInfo(oApp.Tr(Me, 128776116971628000, "Indicare prima il codice articolo"))
            Return
          End If
          NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
          oParam.strTipo = NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart))
          oParam.nMagaz = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_magaz))   'serve per visual solo i lotti aperti
          oParam.nAnno = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_fase))     'serve per visual solo i lotti aperti
          'oParam.strDatreg = NTSCDate(edet_datdoc.Text).ToShortDateString                          'serve per visual solo i lotti aperti
          NTSZOOM.ZoomStrIn("ZOOMUBICAZ", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvRighe.EditingValue) Then grvRighe.SetFocusedValue(NTSZOOM.strIn)

        ElseIf grvRighe.FocusedColumn.Equals(ec_codclie) Then
          '------------------------------------
          'zoom cliente di griglia
          NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
          oParam.bVisGriglia = True
          oParam.bTipoProposto = True
          oParam.nMastro = 0
          oParam.strTipo = "C"
          NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvRighe.EditingValue) Then grvRighe.SetFocusedValue(NTSZOOM.strIn)

        ElseIf grvRighe.FocusedColumn.Equals(ec_prezzo) Then
          '------------------------------------
          'zoom listini
          If bEt_Scorpo = False And nEt_valuta = 0 Then ApriVisualizzaListini(0)

        ElseIf grvRighe.FocusedColumn.Equals(ec_prezvalc) Then
          '------------------------------------
          'zoom listini
          If oCleBoll.dttET.Rows(0)!et_tipork.ToString = "T" And Not oCleBoll.bTerzista Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128569600610000000, "Prezzo in valuta non modificabile per le righe di ordine di lavorazioni interne."))
            Return
          Else
            If nEt_valuta = 0 Then ApriVisualizzaListini(2)
          End If

        ElseIf grvRighe.FocusedColumn.Equals(ec_fase) Then
          '------------------------------------
          'zoom Project management task ID
          If oCleBoll.bModPM Then
            'da fare
          End If
        Else
          '------------------------------------
          'zoom standard di textbox e griglia
          NTSCallStandardZoom()
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCopiaRiga_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCopiaRiga.ItemClick
    Try
      If grRighe.ContainsFocus = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128557648377450788, "Posizionarsi nella griglia del CORPO del documento sulla riga da copiare"))
        Return
      End If

      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128557648423470788, "Posizionarsi nella griglia del CORPO del documento sulla riga da copiare"))
        Return
      End If

      If Not oCleBoll.CorpoImpRecordSalva(dcImpe.Position, False, Nothing) Then Return

      lRigaCopiata = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbIncollaRiga_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbIncollaRiga.ItemClick
    Dim dtrT() As DataRow = Nothing
    Try
      If lRigaCopiata = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128557650814794788, "Utilizzare prima la funzione 'Copia riga'"))
        Return
      End If

      If grRighe.ContainsFocus = False Or Not grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128557650845370788, "Posizionarsi sull'ultima riga della griglia del CORPO del documento"))
        Return
      End If

      dtrT = oCleBoll.dsImpe.Tables("CORPOIMP").Select("ec_riga = " & lRigaCopiata.ToString)
      If dtrT.Length = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128557652623926788, "Impossibile incollare la riga: non è stata trovata la riga n° |" & lRigaCopiata.ToString & "| copiata."))
        Return
      End If

      oCleBoll.dsImpe.Tables("CORPOIMP").Rows.Add(oCleBoll.dsImpe.Tables("CORPOIMP").NewRow)
      With oCleBoll.dsImpe.Tables("CORPOIMP").Rows(oCleBoll.dsImpe.Tables("CORPOIMP").Rows.Count - 1)
        'forzo la MovordOnAddNewRow
        !codditt = "."
        !codditt = DittaCorrente
        !ec_causale = dtrT(0)!ec_causale
        !ec_causale2 = dtrT(0)!ec_causale2
        !ec_magaz = dtrT(0)!ec_magaz    'sempre prima di impostare l'articolo, altrimenti non riesce a proporre l'ubicazione dinamica dal magazzino in validaz articolo
        !ec_magaz2 = dtrT(0)!ec_magaz2
        !ec_codart = dtrT(0)!ec_codart
        !ec_fase = dtrT(0)!ec_fase
        !ec_unmis = dtrT(0)!ec_unmis
        !ec_descr = dtrT(0)!ec_descr
        !ec_colli = dtrT(0)!ec_colli
        !ec_quant = dtrT(0)!ec_quant
        If NTSCDec(dtrT(0)!ec_preziva) <> 0 Then !ec_preziva = dtrT(0)!ec_preziva
        If NTSCDec(dtrT(0)!ec_prezvalc) <> 0 Then !ec_prezvalc = dtrT(0)!ec_prezvalc
        !ec_prezzo = dtrT(0)!ec_prezzo
        !ec_scont1 = dtrT(0)!ec_scont1
        !ec_scont2 = dtrT(0)!ec_scont2
        !ec_scont3 = dtrT(0)!ec_scont3
        !ec_scont4 = dtrT(0)!ec_scont4
        !ec_scont5 = dtrT(0)!ec_scont5
        !ec_scont6 = dtrT(0)!ec_scont6
        !ec_codiva = dtrT(0)!ec_codiva
        !ec_commen = dtrT(0)!ec_commen
        !ec_note = dtrT(0)!ec_note
        !ec_controp = dtrT(0)!ec_controp
        !ec_stasino = dtrT(0)!ec_stasino
        !ec_provv = dtrT(0)!ec_provv
        !ec_provv2 = dtrT(0)!ec_provv2
        !ec_prelist = dtrT(0)!ec_prelist
        !ec_codcfam = dtrT(0)!ec_codcfam
        !ec_commeca = dtrT(0)!ec_commeca
        !ec_subcommeca = dtrT(0)!ec_subcommeca
        !ec_codcena = dtrT(0)!ec_codcena
        !ec_coddivi = dtrT(0)!ec_coddivi
        !ec_desint = dtrT(0)!ec_desint
        !ec_codvuo = dtrT(0)!ec_codvuo
        !ec_matric = dtrT(0)!ec_matric
        !ec_lotto = dtrT(0)!ec_lotto
        !ec_codnomc = dtrT(0)!ec_codnomc
        !ec_massakg = dtrT(0)!ec_massakg
        !ec_massaum2 = dtrT(0)!ec_massaum2
        !ec_valstat = dtrT(0)!ec_valstat
        !ec_proorig = dtrT(0)!ec_proorig
        !ec_percvst = dtrT(0)!ec_percvst
        !ec_numpac = dtrT(0)!ec_numpac
        !ec_codclie = dtrT(0)!ec_codclie
        !ec_ortipo = dtrT(0)!ec_ortipo
        !ec_oranno = dtrT(0)!ec_oranno
        !ec_orserie = dtrT(0)!ec_orserie
        !ec_ornum = dtrT(0)!ec_ornum
        !ec_orriga = dtrT(0)!ec_orriga
        !ec_salcon = dtrT(0)!ec_salcon
        !ec_valore = dtrT(0)!ec_valore
        !ec_valorev = dtrT(0)!ec_valorev
        !ec_prtipo = dtrT(0)!ec_prtipo
        !ec_pranno = dtrT(0)!ec_pranno
        !ec_prserie = dtrT(0)!ec_prserie
        !ec_prnum = dtrT(0)!ec_prnum
        !ec_prriga = dtrT(0)!ec_prriga
        !ec_cltipo = dtrT(0)!ec_cltipo
        !ec_clanno = dtrT(0)!ec_clanno
        !ec_clserie = dtrT(0)!ec_clserie
        !ec_clnum = dtrT(0)!ec_clnum
        !ec_clriga = dtrT(0)!ec_clriga
        !ec_nptipo = dtrT(0)!ec_nptipo
        !ec_npanno = dtrT(0)!ec_npanno
        !ec_npserie = dtrT(0)!ec_npserie
        !ec_npnum = dtrT(0)!ec_npnum
        !ec_npriga = dtrT(0)!ec_npriga
        !ec_ubicaz = dtrT(0)!ec_ubicaz
        !ec_ricimp = dtrT(0)!ec_ricimp
        !ec_misura1 = dtrT(0)!ec_misura1
        !ec_misura2 = dtrT(0)!ec_misura2
        !ec_misura3 = dtrT(0)!ec_misura3
        !ec_umprz = dtrT(0)!ec_umprz
        !ec_tctaglia = dtrT(0)!ec_tctaglia
        !ec_tcindtagl = dtrT(0)!ec_tcindtagl
        !xxo_tcindtaglf = dtrT(0)!xxo_tcindtaglf
        !xxo_tctagliaf = dtrT(0)!xxo_tctagliaf
      End With

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbInsRiga_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbInsRiga.ItemClick
    Dim lPrevRiga As Integer = 0
    Dim lNewRiga As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Try
      If oCleBoll.dsImpe.Tables("CORPOIMP").Rows.Count = 0 Then Return
      If grvRighe.FocusedRowHandle < 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128524040843028000, "Posizionarsi prima su una riga già compilata"))
        Return
      End If
      Me.Cursor = Cursors.WaitCursor
      If Not oCleBoll.CorpoImpRecordSalva(dcImpe.Position, False, Nothing) Then Return

      dtrT = oCleBoll.dsImpe.Tables("CORPOIMP").Select("ec_riga < " & NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga).ToString, "ec_riga DESC")
      If dtrT.Length > 0 Then lPrevRiga = NTSCInt(dtrT(0)!ec_riga)
      lNewRiga = NTSCInt(Fix((NTSCDec((NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga) - lPrevRiga) / 2) + lPrevRiga)))

      If oCleBoll.dsImpe.Tables("CORPOIMP").Select("ec_riga = " & lNewRiga.ToString).Length > 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128524045125384000, "Impossibile inserire una riga intermedia; riga |" & lNewRiga.ToString & "| già presente nel CORPOIMP del documento"))
        Return
      End If

      oCleBoll.dsImpe.Tables("CORPOIMP").Rows.Add(oCleBoll.dsImpe.Tables("CORPOIMP").NewRow)
      'forzo la MovordOnAddNewRow
      oCleBoll.dsImpe.Tables("CORPOIMP").Rows(oCleBoll.dsImpe.Tables("CORPOIMP").Rows.Count - 1)!codditt = "."
      oCleBoll.dsImpe.Tables("CORPOIMP").Rows(oCleBoll.dsImpe.Tables("CORPOIMP").Rows.Count - 1)!codditt = DittaCorrente

      oCleBoll.dsImpe.Tables("CORPOIMP").Rows(oCleBoll.dsImpe.Tables("CORPOIMP").Rows.Count - 1)!ec_riga = lNewRiga
      oCleBoll.lCrigheYT -= oCleBoll.nIncremContatoreRiga

      'ora mi ci devo posizionare sopra ...
      For i = 0 To grvRighe.RowCount - 1
        If NTSCInt(grvRighe.GetRowCellValue(i, "ec_riga")) = lNewRiga Then
          grvRighe.FocusedRowHandle = i
          grvRighe.NTSGetCurrentDataRow.SetAdded()
          Exit For
        End If
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbSelezLotto_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSelezLotto.ItemClick
    Dim oParam As New CLE__PATB
    Dim dDaAss As Decimal = 0
    Try
      If NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart)).Trim = "" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128776116987228000, "Indicare prima il codice articolo"))
        Return
      End If
      If grvRighe.NTSGetCurrentDataRow!ec_umprz.ToString = "S" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128572288546562500, "Selezione non consentita in presenza di una gestione dei prezzi" & vbCrLf & _
               "riferiti ad una unità di misura diversa dalla principale."))
        Return
      End If

      NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
      oParam.strTipo = NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart))
      oParam.nMagaz = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_magaz))   'serve per visual solo i lotti aperti
      oParam.nAnno = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_fase))     'serve per visual solo i lotti aperti
      oParam.strDatreg = dtEt_datdoc.ToShortDateString                                         'serve per visual solo i lotti aperti
      NTSZOOM.ZoomStrIn("ZOOMANALOTTI", DittaCorrente, oParam)

      If oParam.bFlag1 = False Then Return

      If grvRighe.NTSGetCurrentDataRow!xxo_geslotti.ToString = "S" Then
        grvRighe.NTSGetCurrentDataRow!xxo_lottox = oParam.strOut
      End If
      If oCleBoll.GetMemDttArti(NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart))).Rows(0)!ar_gesubic.ToString = "S" Then
        grvRighe.NTSGetCurrentDataRow!ec_ubicaz = oParam.strDescr
      End If

      If oParam.dImporto > 0 Then         'disponbilità netta come da zoom
        If NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_quant) = 0 Then
          'Riporta la q.ta ordinata
          grvRighe.NTSGetCurrentDataRow!ec_unmis = NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_ump)
          grvRighe.NTSGetCurrentDataRow!ec_colli = oParam.dImporto
        End If
      End If


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbBolleCtoLav_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbBolleCtoLav.ItemClick
    'nTipoCol = 0: listino normale, 1 = prezzi iva compresa, 2 = prezzi in valuta
    Dim oPar As CLE__CLDP = Nothing
    Dim strT() As String = Nothing
    Dim i As Integer = 0
    Dim dttMcla As New DataTable
    Dim dSaldoqta As Decimal = 0
    Dim dQuantConv As Decimal = 0
    Dim strError As String = ""
    Dim nMagaz As Integer = 0

    Try
      '----------------------
      If Not grRighe.Focused Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128680972342812500, "Posizionarsi prima su una riga di griglia"))
        Return
      End If

      If NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart)).Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128680973032031250, "Inserire prima il codice articolo"))
        Return
      End If

      '----------------------
      'creo i saldi
      If Not oCleBoll.CreaSalClPerZoom(grvRighe.NTSGetCurrentDataRow, dttMcla, nMagaz) Then Return

      '----------------------
      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BNVEBOLL"
      oPar.strPar2 = "SALCL"
      oPar.strPar1 = grvRighe.NTSGetCurrentDataRow!ec_codart.ToString
      oPar.dPar1 = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_fase)
      oPar.ctlPar1 = dttMcla
      oPar.dPar2 = nMagaz
      oPar.bPar1 = False      'valore di ritorno

      oMenu.RunChild("NTSInformatica", "FRMMGHLCL", "", DittaCorrente, "", "BNMGDOCU", oPar, "", True, True)

      '----------------------
      'Esce se annullo la finestra
      If oPar.bPar1 = False Then Return

      '----------------------
      'Riporta gli estremi del c/to lavoro
      dSaldoqta = oPar.dPar1
      With grvRighe.NTSGetCurrentDataRow
        !ec_cltipo = oPar.strPar1
        !ec_clanno = NTSCInt(oPar.dPar2)
        !ec_clserie = oPar.strPar2
        !ec_clnum = NTSCInt(oPar.dPar3)
        !ec_clriga = NTSCInt(oPar.dPar4)
      End With

      '----------------------
      'Riporta il lotto (tranne se l'opzione è abilitata)
      If Not oCleBoll.bNonRiportareLottiDaCLav Then
        'Legge il lotto dalla riga (solo se la riga non l'ha già attribuito)
        If NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_lotto) = 0 Then
          grvRighe.NTSGetCurrentDataRow!ec_lotto = oCleBoll.LeggiLottoRiga(grvRighe.NTSGetCurrentDataRow!ec_cltipo.ToString, NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_clanno), _
                                   grvRighe.NTSGetCurrentDataRow!ec_clserie.ToString, NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_clnum), _
                                   NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_clriga))
        End If
      End If

      '----------------------
      'riporta la quantità ed i colli
      If NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_quant) = 0 And dSaldoqta > 0 Then
        If grvRighe.NTSGetCurrentDataRow!ec_umprz.ToString <> "S" Then
          grvRighe.NTSGetCurrentDataRow!ec_unmis = grvRighe.NTSGetCurrentDataRow!ec_ump
          grvRighe.NTSGetCurrentDataRow!ec_colli = dSaldoqta
        Else
          With grvRighe.NTSGetCurrentDataRow
            If Not CType(oCleBoll.oCleComm, CLELBMENU).ConvQuantUM(DittaCorrente, !ec_codart.ToString, !ec_ump.ToString, _
                                    dSaldoqta, NTSCDec(!ec_misura1), NTSCDec(!ec_misura2), NTSCDec(!ec_misura3), _
                                    !ec_unmis.ToString, dQuantConv, strError, 3) Then
              If strError <> "" Then oApp.MsgBoxErr(strError)
              grvRighe.NTSGetCurrentDataRow!ec_colli = 0
            Else
              grvRighe.NTSGetCurrentDataRow!ec_colli = dQuantConv
            End If
          End With
          grvRighe.NTSGetCurrentDataRow!ec_quant = dSaldoqta
        End If
      Else
        If NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_quant) > dSaldoqta Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128696690557968750, "Attenzione: la quantità indicata in riga documento è superiore a quella disponibile per chiusura riga di conto lavorazione !"))
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      dttMcla.Clear()
    End Try
  End Sub

  Public Overridable Sub tlbDetMatricole_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDetMatricole.ItemClick
    Dim ds As New DataSet
    Dim i As Integer = 0
    Dim oPar As CLE__CLDP = Nothing

    Try
      If Not grRighe.Focused Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128578169086718750, "Posizionarsi prima su una riga di griglia"))
        Return
      End If
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776117116622000, "Posizionarsi prima su una riga di griglia con codice articolo impostato"))
        Return
      End If
      If grvRighe.NTSGetCurrentDataRow!ec_codart.ToString.Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128586969062500000, "Posizionarsi prima su una riga di griglia con codice articolo impostato"))
        Return
      End If

      If grvRighe.NTSGetCurrentDataRow!xxo_gestmatr.ToString = "N" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128696812554375000, "L'articolo non è gestito a matricole"))
        Return
      End If

      If grvRighe.NTSGetCurrentDataRow!ec_tipork.ToString = "W" And oCleBoll.nGestioneMatricSuNoteDiPrel = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128696829465156250, "Impossibile inserire matricole su documenti di tipo Nota di prelievo."))
        Return
      End If

      If Not oCleBoll.CorpoImpRecordSalva(dcImpe.Position, False, Nothing) Then Return

      Me.Cursor = Cursors.WaitCursor

      '-------------------------------
      'clono la tabella perchè non devo far vedere gli altri impegni collegati
      ds.Tables.Add(oCleBoll.dsShared.Tables("MOVMATR").Clone())
      ds.Tables(0).TableName = "MOVMATR"
      For i = 0 To oCleBoll.dsShared.Tables("MOVMATR").Rows.Count - 1
        If NTSCInt(oCleBoll.dsShared.Tables("MOVMATR").Rows(i)!mma_riga) = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga) And _
           NTSCStr(oCleBoll.dsShared.Tables("MOVMATR").Rows(i)!mma_tipork) = NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_tipork) Then
          ds.Tables("MOVMATR").ImportRow(oCleBoll.dsShared.Tables("MOVMATR").Rows(i))
          oCleBoll.dsShared.Tables("MOVMATR").Rows(i).Delete()
        End If
      Next
      oCleBoll.dsShared.Tables("MOVMATR").AcceptChanges()

      '----------------------
      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BNVEBOLL"
      'passo gli estremi del documento e il datatable con le matricole già impostate
      oPar.strPar1 = grvRighe.NTSGetCurrentDataRow!ec_tipork.ToString
      oPar.dPar1 = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_anno)
      oPar.strPar2 = grvRighe.NTSGetCurrentDataRow!ec_serie.ToString
      oPar.dPar2 = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_numdoc)
      oPar.dPar3 = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga)
      oPar.ctlPar1 = ds     'matricole relative alla riga su cui sono
      oPar.ctlPar2 = oCleBoll.dsShared 'il dataset completo senza le matricole della riga su cui sono
      oPar.dPar4 = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_quant)
      oPar.dPar5 = NTSCInt(grvRighe.NTSGetCurrentDataRow!xxo_esist)
      oPar.strPar3 = NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart)
      oPar.bAddNew = oCleBoll.bNew
      oPar.ctlPar3 = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_fase)
      oPar.ctlPar4 = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_magaz)
      oPar.ctlPar5 = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_magaz2)

      oMenu.RunChild("NTSInformatica", "FRMMGMATR", "", DittaCorrente, "", "BNMGMATR", oPar, "", True, True)

      grvRighe.Focus()

      '-------------------------------
      'riacquisisco gli impegni aggiornati
      For i = 0 To ds.Tables("MOVMATR").Rows.Count - 1
        If ds.Tables("MOVMATR").Rows(i).RowState <> DataRowState.Deleted Then
          If NTSCInt(ds.Tables("MOVMATR").Rows(i)!mma_riga) > 0 Then
            oCleBoll.dsShared.Tables("MOVMATR").ImportRow(ds.Tables("MOVMATR").Rows(i))
          Else
            ds.Tables("MOVMATR").Rows(i).Delete()
          End If
        End If
      Next
      ds.Tables.Clear()
      oCleBoll.dsShared.Tables("MOVMATR").AcceptChanges()
      oCleBoll.bHasChangesET = True

      AbilitaDisabilitaRigaNum()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
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

  Public Overridable Sub tlbDaLista_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDaLista.ItemClick
    Dim oPar As New CLE__CLDP

    Try
      '--------------------------------------------------------------------------------------------------------------
      oPar.dPar1 = nEt_magaz
      oPar.dPar2 = 0
      '--------------------------------------------------------------------------------------------------------------
      oMenu.RunChild("NTSInformatica", "FRMMGSELI", "", DittaCorrente, "", "BNMGDOCU", oPar, "", True, True)
      '--------------------------------------------------------------------------------------------------------------
      If oPar.dPar2 = 0 Then Return
      '--------------------------------------------------------------------------------------------------------------
      oCleBoll.ImportaDaLista(NTSCInt(oPar.dPar2), oPar.bPar1, True)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbRiordina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRiordina.ItemClick
    Dim dttTmp As New DataTable
    Dim strSort As String = ""
    Try
      'prima di riordinare le righe devono essere salvate
      Me.ValidaLastControl()
      If Not oCleBoll.CorpoImpRecordSalva(dcImpe.Position, False, Nothing) Then Return

      'se le righe sono filtrate devo rimuovere il filtro
      grvRighe.DisableColumnSortFilter()

      'non posso applicare la sort: devo clonare il datatable, svuotarlo e riempirlo nuovamente
      Select Case oMenu.GetSettingBusDitt(DittaCorrente, "BNVEBOLL", "RECENT", ".", "RecordOrdinaModo", "0", " ", "0")
        Case "1" : strSort = "ec_codart"
        Case "2" : strSort = "ec_descr"
        Case "3" : strSort = "ec_commeca"
        Case Else : strSort = "ec_riga"
      End Select

      dttTmp = oCleBoll.dsImpe.Tables("CORPOIMP").Copy
      oCleBoll.dsImpe.Tables("CORPOIMP").Clear()
      For Each dtrT As DataRow In dttTmp.Select("", strSort)
        oCleBoll.dsImpe.Tables("CORPOIMP").ImportRow(dtrT)
      Next
      oCleBoll.dsImpe.Tables("CORPOIMP").AcceptChanges()

      'mi posiziono sulla prima riga
      grvRighe.NTSMoveFirstRowColunn()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      dttTmp.Clear()
    End Try
  End Sub
#End Region

#Region "Eventi di griglia"
  Public Overridable Sub grvRighe_NTSCellValueChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvRighe.NTSCellValueChanged
    Try
      Select Case grvRighe.FocusedColumn.Name.ToUpper
        Case "EC_CODART"
          edPreList.Text = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_prelist).ToString

          '-------------------------------------------------
          'rileggo la disponibilità e l'ultimo costo
          LeggiDisponibilitaArticolo(grvRighe.GetFocusedRowCellValue(ec_codart).ToString, _
                                     NTSCInt(grvRighe.GetFocusedRowCellValue(ec_magaz).ToString), _
                                     NTSCInt(grvRighe.GetFocusedRowCellValue(ec_fase).ToString), _
                                     NTSCInt(grvRighe.GetFocusedRowCellValue(ec_commeca).ToString))
        Case "EC_SCONT1", "EC_SCONT2", "EC_SCONT3", "EC_SCONT4", "EC_SCONT5", "EC_SCONT6", _
             "EC_PREZZO", "EC_PREZVALC", "EC_PREZIVA", "EC_STASINO", "EC_CODIVA"
          edPreList.Text = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_prelist).ToString

        Case "EC_UBICAZ"
          If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupRME) Then
            With grvRighe.NTSGetCurrentDataRow
              Dim strUbicaz As String = NTSCStr(!ec_ubicaz)
              If strUbicaz = "".PadLeft(12, "-"c) Then strUbicaz = " " : !ec_ubicaz = " "
              If strUbicaz <> "" Then
                If oCleBoll.TrascodificaUbicazione(NTSCInt(!ec_magaz), strUbicaz, True, True, True) Then
                  If strUbicaz <> "".PadRight(12, "-"c) Then !ec_ubicaz = strUbicaz
                Else
                  oApp.MsgBoxErr(oApp.Tr(Me, 128874502996866378, "Ubicazione non presente nel magazzino |" & NTSCInt(!ec_magaz) & "|"))
                  !ec_ubicaz = " "
                End If
              End If
            End With
          End If

      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvRighe_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvRighe.NTSBeforeRowUpdate
    Try
      Me.Cursor = Cursors.WaitCursor
      If Not Salva() Then e.Allow = False 'rimango sulla riga su cui sono

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
      If oCleBoll.dsImpe Is Nothing Then Return
      If oCleBoll.dsImpe.Tables("CORPOIMP") Is Nothing Then Return
      If oCleBoll.dsImpe.Tables("CORPOIMP").Rows.Count = 0 Then Return
      If grvRighe.GetFocusedRowCellValue(ec_riga) Is Nothing Then Return

      '-------------------------------------------------------
      'se sono su una nuova riga mi posiziono a sinistra
      If grvRighe.GetFocusedRowCellValue(ec_riga).ToString = "" And grvRighe.NTSGetCurrentDataRow Is Nothing Then
        grvRighe.LeftCoord = 0
        If ec_matric.Visible Then
          grvRighe.FocusedColumn = ec_matric
        Else
          grvRighe.FocusedColumn = ec_codart
        End If
        'For n = grvRighe.FocusedColumn.VisibleIndex To 1 Step -1
        '  SendKeys.Send("{LEFT}")
        'Next
        'If ec_riga.Visible Then SendKeys.Send("{RIGHT}")
      End If    'If grvRighe.GetFocusedRowCellValue(ec_riga").FormattedValue.ToString = "" Then

      If grvRighe.GetFocusedRowCellValue(ec_codart).ToString.Trim = "" Then Return

      '-------------------------------------------------
      'rileggo la disponibilità e l'ultimo costo
      LeggiDisponibilitaArticolo(grvRighe.GetFocusedRowCellValue(ec_codart).ToString, _
                                 NTSCInt(grvRighe.GetFocusedRowCellValue(ec_magaz).ToString), _
                                 NTSCInt(grvRighe.GetFocusedRowCellValue(ec_fase).ToString), _
                                 NTSCInt(grvRighe.GetFocusedRowCellValue(ec_commeca).ToString))

      edPreList.Text = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_prelist).ToString

      '-------------------------------------------------
      GrvRighe_RowColChange()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvRighe_NTSFocusedColumnChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles grvRighe.NTSFocusedColumnChanged
    Try
      GrvRighe_RowColChange()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function GrvRighe_RowColChange() As Boolean
    Try

      If oCleBoll Is Nothing Then Return True
      If dttUm Is Nothing Then Return True

      '--------------------------------------
      'Se evasione (in conto o a saldo) di un impegno di produzione
      'la colonna relativa a 'Taglia' è bloccata
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        GctlSetVisEnab(ec_tctaglia, False)
        GctlSetVisEnab(xxo_tctagliaf, False)
      Else
        If oCleBoll.bModTCO Then
          If (NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_ornum) > 0) Then
            ec_tctaglia.Enabled = False
            xxo_tctagliaf.Enabled = False
          Else
            GctlSetVisEnab(ec_tctaglia, False)
            GctlSetVisEnab(xxo_tctagliaf, False)
          End If
        End If
      End If
      '--------------------------------------

      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        If Not ec_unmis.ColumnEdit Is Nothing Then CType(ec_unmis.ColumnEdit, NTSRepositoryItemComboBox).DataSource = dttUm
        ec_unmis.Enabled = False
      Else
        GctlSetVisEnab(ec_unmis, False)
      End If

      '--------------------------------------
      'se posso rendo editabile il campo 'numero di riga'
      AbilitaDisabilitaRigaNum()

      '--------------------------------------
      'Disabilita la modifica dell'unità di misura se riga proveniente da un ordine
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        ec_salcon.Enabled = False
        ec_npsalcon.Enabled = False
      Else
        If NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_ornum) > 0 Or NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_npnum) > 0 Then
          If Not ec_unmis.ColumnEdit Is Nothing Then CType(ec_unmis.ColumnEdit, NTSRepositoryItemComboBox).DataSource = dttUm
          ec_unmis.Enabled = False
        Else
          GctlSetVisEnab(ec_unmis, False)
        End If
        If NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_ornum) = 0 Then
          ec_salcon.Enabled = False
        Else
          GctlSetVisEnab(ec_salcon, False)
        End If
        If NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_npnum) = 0 Then
          ec_npsalcon.Enabled = False
        Else
          GctlSetVisEnab(ec_npsalcon, False)
        End If
      End If

      '--------------------------------------
      'compilo il combo delle unità di misura
      If Not ec_unmis.ColumnEdit Is Nothing Then
        CType(ec_unmis.ColumnEdit, NTSRepositoryItemComboBox).DataSource = dttUm
        If grvRighe.FocusedColumn.Name = "ec_unmis" And grvRighe.GetFocusedRowCellValue(ec_codart).ToString <> "" And NTSCStr(grvRighe.GetFocusedRowCellValue(ec_codart)).Trim <> "" Then
          grvRighe.liListCmb.Visible = False    'lo nascondo, visto che contiene tutte le unita di misura del db ...
          ec_unmis.NTSComboValueOk = oCleBoll.GetArticoUnMis(NTSCStr(grvRighe.GetFocusedRowCellValue(ec_codart)))
          'sarà BN__CHIL ha gestire ec_unmis.NTSComboValueOk
        End If
      End If    'If Not ec_unmis.ColumnEdit Is Nothing Then

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

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
        oApp.MsgBoxInfo(oApp.Tr(Me, 128776117004232000, "Indicare prima il codice articolo"))
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

      If oPar.strParam.Trim <> "" And _
        Microsoft.VisualBasic.Left(oPar.strParam, 5).ToUpper <> "APRI;" And _
        Microsoft.VisualBasic.Left(oPar.strParam, 5).ToUpper <> "NUOV;" Then
        grvRighe.NTSGetCurrentDataRow!xxo_lottox = oPar.strParam
        grvRighe.RefreshEditor(True)
      End If

      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo
      grRighe.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

  Public Overridable Function AbilitaDisabilitaRigaNum() As Boolean
    Try
      ec_riga.Enabled = False
      If oCleBoll.AbilitaDisabilitaRigaNum(grvRighe.NTSGetCurrentDataRow) Then
        GctlSetVisEnab(ec_riga, False)
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Function Salva() As Boolean
    Try
      If Not oCleBoll.CorpoImpRecordSalva(dcImpe.Position, False, Nothing) Then Return False
      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub LeggiDisponibilitaArticolo(ByVal strCodart As String, ByVal nMagaz As Integer, _
                              ByVal nFase As Integer, ByVal lCommeca As Integer, _
                              Optional ByVal strGescomm As String = "?")
    Dim dDisponibilita As Decimal = -1
    Dim dDisponibilitaNetta As Decimal = -1
    Dim dUltCost As Decimal = -1

    Try
      oCleBoll.Leggidisponibilita(strCodart, nMagaz, nFase, lCommeca, dDisponibilita, dDisponibilitaNetta, dUltCost, strGescomm)
      If dDisponibilita = -1 And dDisponibilitaNetta = -1 Then
        edDispon.Text = "999999999"
        edDispNetta.Text = "999999999"
      Else
        edDispon.Text = dDisponibilita.ToString
        edDispNetta.Text = dDisponibilitaNetta.ToString
      End If
      If dUltCost = -1 Then
        edUltCost.Text = "999999999"
      Else
        edUltCost.Text = dUltCost.ToString
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function ApriVisualizzaListini(ByVal nTipoCol As Integer) As Boolean
    'nTipoCol = 0: listino normale, 1 = prezzi iva compresa, 2 = prezzi in valuta
    Dim oPar As CLE__CLDP = Nothing
    Dim strT() As String = Nothing
    Dim i As Integer = 0
    Try
      '----------------------
      If Not grRighe.Focused Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128776117056562000, "Posizionarsi prima su una riga di griglia nella colonna 'codice articolo'"))
        Return False
      End If
      If NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_umprz).Trim = "S" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128569718357656250, "Funzionalità non consentita in presenza di una gestione dei prezzi" & vbCrLf & _
               "riferiti ad una unità di misura diversa dalla principale."))
        Return False
      End If

      '----------------------
      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BNVEBOLL"
      oPar.strPar1 = grvRighe.NTSGetCurrentDataRow!ec_codart.ToString
      oPar.strPar2 = dtEt_datdoc.ToShortDateString
      oPar.dPar1 = NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_fase)
      oPar.dPar3 = lEt_conto
      oPar.dPar4 = 0        'ritorna il prezzo
      oPar.dPar5 = 0        'ritorna la valuta

      oMenu.RunChild("NTSInformatica", "FRMMGLIST", "", DittaCorrente, "", "BNMGDOCU", oPar, "", True, True)

      '----------------------
      'Esce se annullo la finestra
      If oPar.dPar4 = 0 Then Return False

      '----------------------
      'Riporta i prezzi praticati in precedenza
      If nEt_valuta <> NTSCInt(oPar.dPar5) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128570697940468750, "Non è possibile riportare il prezzo in quanto il prezzo selezionato riporta una valuta (|" & oPar.dPar5.ToString & "|) diversa da quella del documento corrente (|" & nEt_valuta & "|)."))
        Return False
      Else
        If nEt_valuta <> 0 Then
          grvRighe.NTSGetCurrentDataRow!ec_prezvalc = oPar.dPar4
        Else
          If nTipoCol = 0 Then
            grvRighe.NTSGetCurrentDataRow!ec_prezzo = oPar.dPar4
          Else
            grvRighe.NTSGetCurrentDataRow!ec_preziva = oPar.dPar4
          End If
        End If
        grvRighe.NTSMoveNextColunn()
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
End Class
