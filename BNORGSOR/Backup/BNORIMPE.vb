Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORIMPE

  Public oCallParams As CLE__CLDP
  Public dcImpe As BindingSource = New BindingSource()
  Public lRigaCopiata As Integer = 0                      'utilizzata dalle voci di menu copia/incolla riga per memorizzare la riga da copiare
  Public frmParent As FRMORGSOR                          'non impostato dalle form _T, _F, _X, ecc
  'passate da form principale'FORM PARENT
  Public oCleGsor As CLEORGSOR
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMORIMPE))
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
    Me.tlbSeleziona = New NTSInformatica.NTSBarSubItem
    Me.tlbDaLista = New NTSInformatica.NTSBarMenuItem
    Me.tlbNavigazMrp = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.tlbSelezUbicazione = New NTSInformatica.NTSBarMenuItem
    Me.ec_riga = New NTSInformatica.NTSGridColumn
    Me.ec_matric = New NTSInformatica.NTSGridColumn
    Me.ec_codart = New NTSInformatica.NTSGridColumn
    Me.ec_descr = New NTSInformatica.NTSGridColumn
    Me.ec_desint = New NTSInformatica.NTSGridColumn
    Me.ec_unmis = New NTSInformatica.NTSGridColumn
    Me.ec_colli = New NTSInformatica.NTSGridColumn
    Me.ec_colpre = New NTSInformatica.NTSGridColumn
    Me.ec_coleva = New NTSInformatica.NTSGridColumn
    Me.ec_ump = New NTSInformatica.NTSGridColumn
    Me.ec_quant = New NTSInformatica.NTSGridColumn
    Me.ec_quapre = New NTSInformatica.NTSGridColumn
    Me.ec_quaeva = New NTSInformatica.NTSGridColumn
    Me.ec_flevapre = New NTSInformatica.NTSGridColumn
    Me.ec_flevas = New NTSInformatica.NTSGridColumn
    Me.ec_preziva = New NTSInformatica.NTSGridColumn
    Me.ec_prezvalc = New NTSInformatica.NTSGridColumn
    Me.ec_prezzo = New NTSInformatica.NTSGridColumn
    Me.ec_magaz = New NTSInformatica.NTSGridColumn
    Me.xxo_magaz = New NTSInformatica.NTSGridColumn
    Me.ec_datcons = New NTSInformatica.NTSGridColumn
    Me.ec_confermato = New NTSInformatica.NTSGridColumn
    Me.ec_rilasciato = New NTSInformatica.NTSGridColumn
    Me.ec_aperto = New NTSInformatica.NTSGridColumn
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
    Me.ec_magaz2 = New NTSInformatica.NTSGridColumn
    Me.xxo_magaz2 = New NTSInformatica.NTSGridColumn
    Me.ec_valore = New NTSInformatica.NTSGridColumn
    Me.ec_contocontr = New NTSInformatica.NTSGridColumn
    Me.xxo_contocon = New NTSInformatica.NTSGridColumn
    Me.ec_datconsor = New NTSInformatica.NTSGridColumn
    Me.ec_codclie = New NTSInformatica.NTSGridColumn
    Me.xxo_codclie = New NTSInformatica.NTSGridColumn
    Me.ec_misura1 = New NTSInformatica.NTSGridColumn
    Me.ec_misura2 = New NTSInformatica.NTSGridColumn
    Me.ec_misura3 = New NTSInformatica.NTSGridColumn
    Me.xxo_codarfo = New NTSInformatica.NTSGridColumn
    Me.ec_perqta = New NTSInformatica.NTSGridColumn
    Me.ec_valoremm = New NTSInformatica.NTSGridColumn
    Me.ec_flkit = New NTSInformatica.NTSGridColumn
    Me.ec_ktriga = New NTSInformatica.NTSGridColumn
    Me.ec_umprz = New NTSInformatica.NTSGridColumn
    Me.ec_fase = New NTSInformatica.NTSGridColumn
    Me.xxo_fase = New NTSInformatica.NTSGridColumn
    Me.ec_codlavo = New NTSInformatica.NTSGridColumn
    Me.xxo_codlavo = New NTSInformatica.NTSGridColumn
    Me.xxo_darave = New NTSInformatica.NTSGridColumn
    Me.ec_ubicaz = New NTSInformatica.NTSGridColumn
    Me.xxo_codtagl = New NTSInformatica.NTSGridColumn
    Me.ec_flprznet = New NTSInformatica.NTSGridColumn
    Me.ec_tctaglia = New NTSInformatica.NTSGridColumn
    Me.ec_datini = New NTSInformatica.NTSGridColumn
    Me.ec_datfin = New NTSInformatica.NTSGridColumn
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
    Me.xxo_tctagliaf = New NTSInformatica.NTSGridColumn
    Me.ec_coddivi = New NTSInformatica.NTSGridColumn
    Me.xxo_coddivi = New NTSInformatica.NTSGridColumn
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
    Me.frmAuto.Appearance.BackColor = System.Drawing.SystemColors.GradientActiveCaption
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbNavigazMrp, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti, Me.tlbInsRiga, Me.tlbCopiaRiga, Me.tlbIncollaRiga, Me.tlbSelezLotto, Me.tlbSelezUbicazione, Me.tlbSeleziona, Me.tlbDaLista})
    Me.NtsBarManager1.MaxItemId = 25
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNavigazMrp), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci, True)})
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
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbInsRiga), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCopiaRiga), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbIncollaRiga), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSelezLotto, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSeleziona, True)})
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
    Me.tlbSelezLotto.Caption = "Seleziona lotto/ubicazioni aperti"
    Me.tlbSelezLotto.Id = 20
    Me.tlbSelezLotto.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F7))
    Me.tlbSelezLotto.Name = "tlbSelezLotto"
    Me.tlbSelezLotto.NTSIsCheckBox = False
    Me.tlbSelezLotto.Visible = True
    '
    'tlbSeleziona
    '
    Me.tlbSeleziona.Caption = "SELEZIONA"
    Me.tlbSeleziona.Id = 23
    Me.tlbSeleziona.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDaLista)})
    Me.tlbSeleziona.Name = "tlbSeleziona"
    Me.tlbSeleziona.Visible = True
    '
    'tlbDaLista
    '
    Me.tlbDaLista.Caption = "Da lista selezionata"
    Me.tlbDaLista.Id = 24
    Me.tlbDaLista.Name = "tlbDaLista"
    Me.tlbDaLista.NTSIsCheckBox = False
    Me.tlbDaLista.Visible = True
    '
    'tlbNavigazMrp
    '
    Me.tlbNavigazMrp.Caption = "Navigazione MRP"
    Me.tlbNavigazMrp.Glyph = CType(resources.GetObject("tlbNavigazMrp.Glyph"), System.Drawing.Image)
    Me.tlbNavigazMrp.Id = 5
    Me.tlbNavigazMrp.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N))
    Me.tlbNavigazMrp.Name = "tlbNavigazMrp"
    Me.tlbNavigazMrp.Visible = True
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
    Me.ec_colli.VisibleIndex = 4
    Me.ec_colli.Width = 54
    '
    'ec_colpre
    '
    Me.ec_colpre.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.ec_colpre.AppearanceCell.Options.UseBackColor = True
    Me.ec_colpre.AppearanceCell.Options.UseTextOptions = True
    Me.ec_colpre.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_colpre.Caption = "Colli pren."
    Me.ec_colpre.Enabled = False
    Me.ec_colpre.FieldName = "ec_colpre"
    Me.ec_colpre.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_colpre.Name = "ec_colpre"
    Me.ec_colpre.NTSRepositoryComboBox = Nothing
    Me.ec_colpre.NTSRepositoryItemCheck = Nothing
    Me.ec_colpre.NTSRepositoryItemMemo = Nothing
    Me.ec_colpre.NTSRepositoryItemText = Nothing
    Me.ec_colpre.OptionsColumn.AllowEdit = False
    Me.ec_colpre.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_colpre.OptionsColumn.ReadOnly = True
    Me.ec_colpre.OptionsFilter.AllowFilter = False
    Me.ec_colpre.Visible = True
    Me.ec_colpre.VisibleIndex = 5
    Me.ec_colpre.Width = 54
    '
    'ec_coleva
    '
    Me.ec_coleva.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.ec_coleva.AppearanceCell.Options.UseBackColor = True
    Me.ec_coleva.AppearanceCell.Options.UseTextOptions = True
    Me.ec_coleva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_coleva.Caption = "Colli evasi"
    Me.ec_coleva.Enabled = False
    Me.ec_coleva.FieldName = "ec_coleva"
    Me.ec_coleva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_coleva.Name = "ec_coleva"
    Me.ec_coleva.NTSRepositoryComboBox = Nothing
    Me.ec_coleva.NTSRepositoryItemCheck = Nothing
    Me.ec_coleva.NTSRepositoryItemMemo = Nothing
    Me.ec_coleva.NTSRepositoryItemText = Nothing
    Me.ec_coleva.OptionsColumn.AllowEdit = False
    Me.ec_coleva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_coleva.OptionsColumn.ReadOnly = True
    Me.ec_coleva.OptionsFilter.AllowFilter = False
    Me.ec_coleva.Visible = True
    Me.ec_coleva.VisibleIndex = 6
    Me.ec_coleva.Width = 51
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
    Me.ec_ump.VisibleIndex = 7
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
    Me.ec_quant.VisibleIndex = 8
    '
    'ec_quapre
    '
    Me.ec_quapre.AppearanceCell.Options.UseBackColor = True
    Me.ec_quapre.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quapre.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quapre.Caption = "Q.tà pren."
    Me.ec_quapre.Enabled = False
    Me.ec_quapre.FieldName = "ec_quapre"
    Me.ec_quapre.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quapre.Name = "ec_quapre"
    Me.ec_quapre.NTSRepositoryComboBox = Nothing
    Me.ec_quapre.NTSRepositoryItemCheck = Nothing
    Me.ec_quapre.NTSRepositoryItemMemo = Nothing
    Me.ec_quapre.NTSRepositoryItemText = Nothing
    Me.ec_quapre.OptionsColumn.AllowEdit = False
    Me.ec_quapre.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quapre.OptionsColumn.ReadOnly = True
    Me.ec_quapre.OptionsFilter.AllowFilter = False
    Me.ec_quapre.Visible = True
    Me.ec_quapre.VisibleIndex = 9
    '
    'ec_quaeva
    '
    Me.ec_quaeva.AppearanceCell.Options.UseBackColor = True
    Me.ec_quaeva.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quaeva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quaeva.Caption = "Q.tà evasa"
    Me.ec_quaeva.Enabled = False
    Me.ec_quaeva.FieldName = "ec_quaeva"
    Me.ec_quaeva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quaeva.Name = "ec_quaeva"
    Me.ec_quaeva.NTSRepositoryComboBox = Nothing
    Me.ec_quaeva.NTSRepositoryItemCheck = Nothing
    Me.ec_quaeva.NTSRepositoryItemMemo = Nothing
    Me.ec_quaeva.NTSRepositoryItemText = Nothing
    Me.ec_quaeva.OptionsColumn.AllowEdit = False
    Me.ec_quaeva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quaeva.OptionsColumn.ReadOnly = True
    Me.ec_quaeva.OptionsFilter.AllowFilter = False
    Me.ec_quaeva.Visible = True
    Me.ec_quaeva.VisibleIndex = 10
    '
    'ec_flevapre
    '
    Me.ec_flevapre.AppearanceCell.Options.UseBackColor = True
    Me.ec_flevapre.AppearanceCell.Options.UseTextOptions = True
    Me.ec_flevapre.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_flevapre.Caption = "Pr. totale"
    Me.ec_flevapre.Enabled = True
    Me.ec_flevapre.FieldName = "ec_flevapre"
    Me.ec_flevapre.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_flevapre.Name = "ec_flevapre"
    Me.ec_flevapre.NTSRepositoryComboBox = Nothing
    Me.ec_flevapre.NTSRepositoryItemCheck = Nothing
    Me.ec_flevapre.NTSRepositoryItemMemo = Nothing
    Me.ec_flevapre.NTSRepositoryItemText = Nothing
    Me.ec_flevapre.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_flevapre.OptionsFilter.AllowFilter = False
    Me.ec_flevapre.Visible = True
    Me.ec_flevapre.VisibleIndex = 11
    '
    'ec_flevas
    '
    Me.ec_flevas.AppearanceCell.Options.UseBackColor = True
    Me.ec_flevas.AppearanceCell.Options.UseTextOptions = True
    Me.ec_flevas.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_flevas.Caption = "Evas. totale"
    Me.ec_flevas.Enabled = True
    Me.ec_flevas.FieldName = "ec_flevas"
    Me.ec_flevas.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_flevas.Name = "ec_flevas"
    Me.ec_flevas.NTSRepositoryComboBox = Nothing
    Me.ec_flevas.NTSRepositoryItemCheck = Nothing
    Me.ec_flevas.NTSRepositoryItemMemo = Nothing
    Me.ec_flevas.NTSRepositoryItemText = Nothing
    Me.ec_flevas.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_flevas.OptionsFilter.AllowFilter = False
    Me.ec_flevas.Visible = True
    Me.ec_flevas.VisibleIndex = 12
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
    Me.ec_prezzo.VisibleIndex = 13
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
    Me.ec_magaz.VisibleIndex = 14
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
    Me.xxo_magaz.VisibleIndex = 15
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
    Me.ec_datcons.VisibleIndex = 16
    '
    'ec_confermato
    '
    Me.ec_confermato.AppearanceCell.Options.UseBackColor = True
    Me.ec_confermato.AppearanceCell.Options.UseTextOptions = True
    Me.ec_confermato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_confermato.Caption = "Confermato"
    Me.ec_confermato.Enabled = True
    Me.ec_confermato.FieldName = "ec_confermato"
    Me.ec_confermato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_confermato.Name = "ec_confermato"
    Me.ec_confermato.NTSRepositoryComboBox = Nothing
    Me.ec_confermato.NTSRepositoryItemCheck = Nothing
    Me.ec_confermato.NTSRepositoryItemMemo = Nothing
    Me.ec_confermato.NTSRepositoryItemText = Nothing
    Me.ec_confermato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_confermato.OptionsFilter.AllowFilter = False
    Me.ec_confermato.Visible = True
    Me.ec_confermato.VisibleIndex = 17
    '
    'ec_rilasciato
    '
    Me.ec_rilasciato.AppearanceCell.Options.UseBackColor = True
    Me.ec_rilasciato.AppearanceCell.Options.UseTextOptions = True
    Me.ec_rilasciato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_rilasciato.Caption = "Rilasciato"
    Me.ec_rilasciato.Enabled = True
    Me.ec_rilasciato.FieldName = "ec_rilasciato"
    Me.ec_rilasciato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_rilasciato.Name = "ec_rilasciato"
    Me.ec_rilasciato.NTSRepositoryComboBox = Nothing
    Me.ec_rilasciato.NTSRepositoryItemCheck = Nothing
    Me.ec_rilasciato.NTSRepositoryItemMemo = Nothing
    Me.ec_rilasciato.NTSRepositoryItemText = Nothing
    Me.ec_rilasciato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_rilasciato.OptionsFilter.AllowFilter = False
    Me.ec_rilasciato.Visible = True
    Me.ec_rilasciato.VisibleIndex = 18
    '
    'ec_aperto
    '
    Me.ec_aperto.AppearanceCell.Options.UseBackColor = True
    Me.ec_aperto.AppearanceCell.Options.UseTextOptions = True
    Me.ec_aperto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_aperto.Caption = "Aperto"
    Me.ec_aperto.Enabled = True
    Me.ec_aperto.FieldName = "ec_aperto"
    Me.ec_aperto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_aperto.Name = "ec_aperto"
    Me.ec_aperto.NTSRepositoryComboBox = Nothing
    Me.ec_aperto.NTSRepositoryItemCheck = Nothing
    Me.ec_aperto.NTSRepositoryItemMemo = Nothing
    Me.ec_aperto.NTSRepositoryItemText = Nothing
    Me.ec_aperto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_aperto.OptionsFilter.AllowFilter = False
    Me.ec_aperto.Visible = True
    Me.ec_aperto.VisibleIndex = 19
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
    Me.ec_controp.VisibleIndex = 20
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
    Me.xxo_controp.VisibleIndex = 21
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
    Me.ec_codiva.VisibleIndex = 22
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
    Me.xxo_codiva.VisibleIndex = 23
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
    Me.ec_stasino.VisibleIndex = 24
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
    Me.ec_codcfam.VisibleIndex = 25
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
    Me.xxo_codcfam.VisibleIndex = 26
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
    Me.ec_commeca.VisibleIndex = 27
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
    Me.xxo_commeca.VisibleIndex = 28
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
    Me.ec_subcommeca.VisibleIndex = 29
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
    Me.xxo_lottox.VisibleIndex = 30
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
    Me.ec_codcena.VisibleIndex = 31
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
    Me.xxo_codcena.VisibleIndex = 32
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
    Me.ec_note.VisibleIndex = 33
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
    '
    'ec_valore
    '
    Me.ec_valore.AppearanceCell.Options.UseBackColor = True
    Me.ec_valore.AppearanceCell.Options.UseTextOptions = True
    Me.ec_valore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_valore.Caption = "Valore da evadere"
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
    Me.ec_valore.VisibleIndex = 34
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
    'ec_datconsor
    '
    Me.ec_datconsor.AppearanceCell.Options.UseBackColor = True
    Me.ec_datconsor.AppearanceCell.Options.UseTextOptions = True
    Me.ec_datconsor.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_datconsor.Caption = "Dt. consegna originaria"
    Me.ec_datconsor.Enabled = True
    Me.ec_datconsor.FieldName = "ec_datconsor"
    Me.ec_datconsor.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_datconsor.Name = "ec_datconsor"
    Me.ec_datconsor.NTSRepositoryComboBox = Nothing
    Me.ec_datconsor.NTSRepositoryItemCheck = Nothing
    Me.ec_datconsor.NTSRepositoryItemMemo = Nothing
    Me.ec_datconsor.NTSRepositoryItemText = Nothing
    Me.ec_datconsor.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_datconsor.OptionsFilter.AllowFilter = False
    Me.ec_datconsor.Visible = True
    Me.ec_datconsor.VisibleIndex = 35
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
    Me.ec_perqta.VisibleIndex = 36
    '
    'ec_valoremm
    '
    Me.ec_valoremm.AppearanceCell.Options.UseBackColor = True
    Me.ec_valoremm.AppearanceCell.Options.UseTextOptions = True
    Me.ec_valoremm.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_valoremm.Caption = "Valore riga totale"
    Me.ec_valoremm.Enabled = False
    Me.ec_valoremm.FieldName = "ec_valoremm"
    Me.ec_valoremm.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_valoremm.Name = "ec_valoremm"
    Me.ec_valoremm.NTSRepositoryComboBox = Nothing
    Me.ec_valoremm.NTSRepositoryItemCheck = Nothing
    Me.ec_valoremm.NTSRepositoryItemMemo = Nothing
    Me.ec_valoremm.NTSRepositoryItemText = Nothing
    Me.ec_valoremm.OptionsColumn.AllowEdit = False
    Me.ec_valoremm.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_valoremm.OptionsColumn.ReadOnly = True
    Me.ec_valoremm.OptionsFilter.AllowFilter = False
    Me.ec_valoremm.Visible = True
    Me.ec_valoremm.VisibleIndex = 37
    '
    'ec_flkit
    '
    Me.ec_flkit.AppearanceCell.Options.UseBackColor = True
    Me.ec_flkit.AppearanceCell.Options.UseTextOptions = True
    Me.ec_flkit.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_flkit.Caption = "Tipo Kit"
    Me.ec_flkit.Enabled = False
    Me.ec_flkit.FieldName = "ec_flkit"
    Me.ec_flkit.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_flkit.Name = "ec_flkit"
    Me.ec_flkit.NTSRepositoryComboBox = Nothing
    Me.ec_flkit.NTSRepositoryItemCheck = Nothing
    Me.ec_flkit.NTSRepositoryItemMemo = Nothing
    Me.ec_flkit.NTSRepositoryItemText = Nothing
    Me.ec_flkit.OptionsColumn.AllowEdit = False
    Me.ec_flkit.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_flkit.OptionsColumn.ReadOnly = True
    Me.ec_flkit.OptionsFilter.AllowFilter = False
    '
    'ec_ktriga
    '
    Me.ec_ktriga.AppearanceCell.Options.UseBackColor = True
    Me.ec_ktriga.AppearanceCell.Options.UseTextOptions = True
    Me.ec_ktriga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_ktriga.Caption = "Rif. riga kit"
    Me.ec_ktriga.Enabled = False
    Me.ec_ktriga.FieldName = "ec_ktriga"
    Me.ec_ktriga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_ktriga.Name = "ec_ktriga"
    Me.ec_ktriga.NTSRepositoryComboBox = Nothing
    Me.ec_ktriga.NTSRepositoryItemCheck = Nothing
    Me.ec_ktriga.NTSRepositoryItemMemo = Nothing
    Me.ec_ktriga.NTSRepositoryItemText = Nothing
    Me.ec_ktriga.OptionsColumn.AllowEdit = False
    Me.ec_ktriga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_ktriga.OptionsColumn.ReadOnly = True
    Me.ec_ktriga.OptionsFilter.AllowFilter = False
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
    'xxo_darave
    '
    Me.xxo_darave.AppearanceCell.Options.UseBackColor = True
    Me.xxo_darave.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_darave.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_darave.Caption = "Dare-Avere"
    Me.xxo_darave.Enabled = False
    Me.xxo_darave.FieldName = "xxo_darave"
    Me.xxo_darave.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_darave.Name = "xxo_darave"
    Me.xxo_darave.NTSRepositoryComboBox = Nothing
    Me.xxo_darave.NTSRepositoryItemCheck = Nothing
    Me.xxo_darave.NTSRepositoryItemMemo = Nothing
    Me.xxo_darave.NTSRepositoryItemText = Nothing
    Me.xxo_darave.OptionsColumn.AllowEdit = False
    Me.xxo_darave.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_darave.OptionsColumn.ReadOnly = True
    Me.xxo_darave.OptionsFilter.AllowFilter = False
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
    Me.ec_tctaglia.VisibleIndex = 38
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
    Me.edUltCost.Cursor = System.Windows.Forms.Cursors.Default
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
    '
    '
    '
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
    '
    'xxo_tctagliaf
    '
    Me.xxo_tctagliaf.AppearanceCell.Options.UseBackColor = True
    Me.xxo_tctagliaf.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_tctagliaf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_tctagliaf.Caption = "Taglia figlio"
    Me.xxo_tctagliaf.Enabled = True
    Me.xxo_tctagliaf.FieldName = "xxo_tctagliaf"
    Me.xxo_tctagliaf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_tctagliaf.Name = "xxo_tctagliaf"
    Me.xxo_tctagliaf.NTSRepositoryComboBox = Nothing
    Me.xxo_tctagliaf.NTSRepositoryItemCheck = Nothing
    Me.xxo_tctagliaf.NTSRepositoryItemMemo = Nothing
    Me.xxo_tctagliaf.NTSRepositoryItemText = Nothing
    Me.xxo_tctagliaf.Visible = True
    Me.xxo_tctagliaf.VisibleIndex = 39
    Me.grvRighe.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ec_riga, Me.ec_matric, Me.ec_codart, Me.ec_descr, Me.ec_desint, Me.ec_unmis, Me.ec_colli, Me.ec_colpre, Me.ec_coleva, Me.ec_ump, Me.ec_quant, Me.ec_quapre, Me.ec_quaeva, Me.ec_flevapre, Me.ec_flevas, Me.ec_preziva, Me.ec_prezvalc, Me.ec_prezzo, Me.ec_magaz, Me.xxo_magaz, Me.ec_datcons, Me.ec_confermato, Me.ec_rilasciato, Me.ec_aperto, Me.ec_ricimp, Me.ec_provv, Me.ec_vprovv, Me.ec_provv2, Me.ec_vprovv2, Me.ec_controp, Me.xxo_controp, Me.ec_codiva, Me.xxo_codiva, Me.ec_stasino, Me.ec_codcfam, Me.xxo_codcfam, Me.ec_commeca, Me.xxo_commeca, Me.ec_subcommeca, Me.xxo_lottox, Me.ec_codcena, Me.xxo_codcena, Me.ec_note, Me.ec_magaz2, Me.xxo_magaz2, Me.ec_valore, Me.ec_contocontr, Me.xxo_contocon, Me.ec_datconsor, Me.ec_codclie, Me.xxo_codclie, Me.ec_misura1, Me.ec_misura2, Me.ec_misura3, Me.xxo_codarfo, Me.ec_perqta, Me.ec_valoremm, Me.ec_flkit, Me.ec_ktriga, Me.ec_umprz, Me.ec_fase, Me.xxo_fase, Me.ec_codlavo, Me.xxo_codlavo, Me.xxo_darave, Me.ec_ubicaz, Me.xxo_codtagl, Me.ec_flprznet, Me.ec_tctaglia, Me.ec_datini, Me.ec_datfin, Me.ec_coddivi, Me.xxo_coddivi, Me.xxo_tctagliaf})
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
    Me.xxo_coddivi.Caption = "Descr. divisione CA"
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
    'FRMORIMPE
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(740, 442)
    Me.Controls.Add(Me.pnGriglia)
    Me.Controls.Add(Me.pnCorpo)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMORIMPE"
    Me.Text = "IMPEGNI COLLEGATI"
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

  Public Overloads Function InitEntity(ByRef Menu As CLE__MENU, ByRef frmOrgsor As FRMORGSOR, ByRef ds As DataSet) As Boolean
    Dim dttSc As New DataTable
    oMenu = Menu
    oApp = oMenu.App
    frmParent = frmOrgsor       'form PARENT
    DittaCorrente = oCleGsor.strDittaCorrente
    Me.GctlTipoDoc = "Y"

    InitializeComponent()
    Me.MinimumSize = Me.Size

    '-------------------------------
    'leggo dal database i dati e collego il NTSBinding
    ds.AcceptChanges()
    oCleGsor.dsImpe = ds
    oCleGsor.CorpoImpSetDataTable(DittaCorrente, oCleGsor.dsImpe.Tables("CORPOIMP"))
    dcImpe.DataSource = oCleGsor.dsImpe.Tables("CORPOIMP")
    oCleGsor.dsImpe.AcceptChanges()

    '------------------------------
    'collego gli eventi dell'entity
    AddHandler oCleGsor.RemoteEvent, AddressOf GestisciEventiEntity

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim i As Integer = 0
    Dim dttStasino As New DataTable()
    Dim dttKit As New DataTable()
    Dim dttOatipo As New DataTable()
    Dim dttTask As New DataTable()
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
        tlbNavigazMrp.GlyphPath = (oApp.ChildImageDir & "\movmrp.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      edUltCost.NTSSetParam(oMenu, oApp.Tr(Me, 128230023236442158, "Ultimo costo"), oApp.FormatPrzUn)
      edPreList.NTSSetParam(oMenu, oApp.Tr(Me, 128230023236598331, "Prezzo di listino"), oApp.FormatPrzUn)
      edDispNetta.NTSSetParam(oMenu, oApp.Tr(Me, 128230023236754504, "Disponibilità netta"), oCleGsor.strFormatQtaEsistCorpo)
      edDispon.NTSSetParam(oMenu, oApp.Tr(Me, 128230023236910677, "Disponibilità"), oCleGsor.strFormatQtaEsistCorpo)

      grvRighe.NTSSetParam(oMenu, oApp.Tr(Me, 128230023871441576, "IMPEGNI COLLEGATI"))
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

      '-------------------------------------------------
      'carico le unità di misura nella colonna colli: caso particolare
      'carico tutte le unità di misura gestite dagli articoli, 
      'poi al cambio di riga filtrero nel combo solo quelle gestite dall'articolo in analisi
      dttUm = oCleGsor.CaricaUnMis()
      dttUm.AcceptChanges()

      '-------------------------------------------------
      grvRighe.NTSMenuContext = tlbStrumenti

      ec_riga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023247218095, "Riga"), "0", 9, 0, 999999999)
      ec_matric.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023247374268, "Barcode"), 255, False)
      ec_codart.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128230023247530441, "Cod. Art."), tabartico, False)
      ec_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023247686614, "Descrizione"), 40, True)
      ec_desint.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023247842787, "Descr.interna"), 40, True)
      ec_unmis.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128230023247998960, "U.M."), dttUm, "tb_codumis", "tb_codumis")
      ec_colli.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023248155133, "Colli ord."), oApp.FormatQta, 13)
      ec_colpre.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023248311306, "Colli pren."), oApp.FormatQta, 13)
      ec_coleva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023248467479, "Colli evasi"), oApp.FormatQta, 13)
      ec_ump.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023248623652, "UMP"), 3, False)
      ec_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023248779825, "Q.tà  ordin."), oApp.FormatQta, 13)
      ec_quapre.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023249248344, "Q.tà  pren."), oApp.FormatQta, 13)
      ec_quaeva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023249404517, "Q.tà  evasa"), oApp.FormatQta, 13)
      ec_flevapre.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023249560690, "Pr. totale"), "S", "C")
      ec_flevas.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023249716863, "Evas. totale"), "S", "C")
      ec_preziva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023249873036, "Prezzo IVA inc."), oApp.FormatPrzUn, 13)
      ec_prezvalc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023250029209, "Prezzo valuta"), oApp.FormatPrzUnVal, 13)
      ec_prezzo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023250185382, "Prezzo"), oApp.FormatPrzUn, 13)
      ec_magaz.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023251278593, "Magazzino"), tabmaga)
      xxo_magaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023251434766, "Descr. magazzino"), 0, True)
      ec_datcons.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128230023251590939, "Data cons."), False)
      ec_confermato.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023251747112, "Confermato"), "S", "N")
      ec_rilasciato.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023251903285, "Rilasciato"), "S", "N")
      ec_aperto.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023252059458, "Aperto"), "S", "N")
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
      ec_codcfam.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128230023253777361, "Linea/Fam."), tabcfam, True)
      xxo_codcfam.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023253933534, "Descr. linea/fam"), 0, True)
      ec_commeca.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023254089707, "Comm. C.A."), tabcommess)
      xxo_commeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023254245880, "Descr. commessa"), 0, True)
      ec_subcommeca.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128230023254402053, "Sub C."), tabsubcomm, True)
      If oCleGsor.bLottoNew = False Then
        xxo_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023254558226, "Lotto"), 9, True)
      Else
        xxo_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129513869925818712, "Lotto"), 50, True)
      End If
      ec_codcena.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023254714399, "Centro C.A."), tabcena)
      xxo_codcena.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023254870572, "Descr. centro"), 0, True)
      ec_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023255026745, "Note"), 0, True)
      ec_magaz2.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023255182918, "Magaz 2"), tabmaga)
      xxo_magaz2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023255339091, "Descr. magaz. 2"), 0, True)
      ec_valore.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023255495264, "Valore da evadere"), oApp.FormatImporti, 13)
      ec_contocontr.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023255651437, "Conto controp."), tabanagra)
      xxo_contocon.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023255807610, "Descr. conto controp."), 0, True)
      ec_datconsor.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128230023255963783, "Dt. consegna originaria"), False)
      ec_codclie.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023256119956, "Cod. cliente"), tabanagra)
      xxo_codclie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023256276129, "Descr. cliente"), 0, True)
      ec_misura1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023256432302, "Misura 1"), oApp.FormatQta, 13)
      ec_misura2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023256588475, "Misura 2"), oApp.FormatQta, 13)
      ec_misura3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023256744648, "Misura 3"), oApp.FormatQta, 13)
      xxo_codarfo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023256900821, "Cod.Art.cli/forn"), 0, True)
      ec_perqta.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023257056994, "Prz/Qta"), "#,##0.00", 13)
      ec_valoremm.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023257213167, "Valore riga totale"), oApp.FormatImporti, 13)
      ec_flkit.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128230023257369340, "Tipo Kit"), dttKit, "val", "cod")
      ec_ktriga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023257525513, "Rif. riga kit"), "0", 9, 0, 999999999)
      ec_umprz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023259087243, "Prezzi x u.d.m."), 3, False)
      ec_fase.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023259243416, "Fase"), tabartfasi)
      ec_fase.ArtiPerFase = ec_codart
      xxo_fase.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023259399589, "Descr. fase"), 0, True)
      ec_codlavo.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023259555762, "Codice lavoraz."), tablavo)
      xxo_codlavo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023259711935, "Descr. lavoraz."), 0, True)
      xxo_darave.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023261273665, "Dare-Avere"), 1, True)
      ec_ubicaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023261429838, "Ubicazione"), 18, False)
      xxo_codtagl.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023261586011, "."), "0", 4, 0, 9999)
      ec_flprznet.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128776121960564000, "Prezzo netto"), "S", "N")
      ec_tctaglia.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128587043410781250, "Taglia padre"), 4, False)
      ec_datini.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128776121975384000, "Dt iniz. comp. econ."), False)
      ec_datfin.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128589571526406250, "Dt fin. comp. econ."), False)
      ec_coddivi.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129270305457101406, "Divisione CA"), tabdivi)
      xxo_coddivi.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129270307473302578, "Descr. Divisione CA"), 0, True)
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
  Public Overridable Sub FRMORIMPE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
      If NTSCInt(oCleGsor.dtrHT!xxo_codtagl) = 0 Then
        ec_tctaglia.Visible = False
        ec_tctaglia.Enabled = False
        xxo_tctagliaf.Visible = False
        xxo_tctagliaf.Enabled = False
      Else
        GctlSetVisEnab(ec_tctaglia, True)
        GctlSetVisEnab(ec_tctaglia, False)
        If oCleGsor.bGestioneAbbinamentiTaglie Then
          GctlSetVisEnab(xxo_tctagliaf, True)
          GctlSetVisEnab(xxo_tctagliaf, False)
        Else
          xxo_tctagliaf.Visible = False
          xxo_tctagliaf.Enabled = False
        End If
      End If

      If NTSCInt(oCleGsor.dttET.Rows(0)!et_valuta) <> 0 Then
        GctlSetVisEnab(ec_prezvalc, True)
      Else
        ec_prezvalc.Visible = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMORIMPE_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try

  End Sub

  Public Overridable Sub FRMORIMPE_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    'salvo l'impostazione della griglia. devo farlo anche al cambio del tipo documento (ad esempio in bsveboll
    Try
      dcImpe.Dispose()
      oCleGsor.dsImpe.Dispose()
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
        oApp.MsgBoxErr(oApp.Tr(Me, 129048512468947391, "Posizionarsi prima nella griglia e selezionare la riga da cancellare"))
        Return
      End If
      Me.Cursor = Cursors.WaitCursor
      If Not oCleGsor.CorpoImpTestPreCancella(dcImpe.Position) Then Return
      If Not grvRighe.NTSDeleteRigaCorrente(dcImpe, True, dtrDeleted) Then Return
      If Not oCleGsor.CorpoImpRecordSalva(dcImpe.Position, True, dtrDeleted) Then Return

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
      oCleGsor.CorpoImpRipristina(dcImpe.Position, dcImpe.Filter)
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
          oParam.lContoCF = oCleGsor.lContoCF
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
                  oCleGsor.bInInsertArticoDaZoom = True
                  If Not oCleGsor.CorpoImpRecordSalva(dcImpe.Position, False, Nothing) Then Return
                  For Each dtrT As DataRow In CType(oParam.oParam, DataTable).Rows
                    oCleGsor.dsImpe.Tables("CORPOIMP").Rows.Add(oCleGsor.dsImpe.Tables("CORPOIMP").NewRow)
                    With oCleGsor.dsImpe.Tables("CORPOIMP").Rows(oCleGsor.dsImpe.Tables("CORPOIMP").Rows.Count - 1)
                      'forzo la MovordOnAddNewRow
                      !codditt = "."
                      !codditt = DittaCorrente
                      !ec_codart = dtrT!codart.ToString
                      If NTSCInt(!xxo_codtagl) = 0 Then
                        !ec_quant = 1
                      End If
                    End With
                    If Not oCleGsor.CorpoImpRecordSalva(oCleGsor.dsImpe.Tables("CORPOIMP").Rows.Count - 1, False, Nothing) Then
                      oCleGsor.dsImpe.Tables("CORPOIMP").Rows(oCleGsor.dsImpe.Tables("CORPOIMP").Rows.Count - 1).Delete()
                    End If
                  Next
                  oCleGsor.bInInsertArticoDaZoom = False
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
            oApp.MsgBoxInfo(oApp.Tr(Me, 128038277237865342, "Indicare prima il codice commessa"))
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
            oApp.MsgBoxInfo(oApp.Tr(Me, 128776122047682000, "Indicare prima il codice articolo"))
            Return
          End If
          NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
          oParam.strTipo = NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart))
          'oParam.nMagaz = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_magaz))   'serve per visual solo i lotti aperti
          oParam.nAnno = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_fase))     'serve per visual solo i lotti aperti
          'oParam.strDatreg = NTSCDate(edet_datdoc.Text).ToShortDateString                          'serve per visual solo i lotti aperti
          NTSZOOM.ZoomStrIn("ZOOMANALOTTI", DittaCorrente, oParam)
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
          If oCleGsor.dttET.Rows(0)!et_tipork.ToString = "H" And Not oCleGsor.bTerzista Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128569600610000000, "Prezzo in valuta non modificabile per le righe di ordine di lavorazioni interne."))
            Return
          Else
            If nEt_valuta = 0 Then ApriVisualizzaListini(2)
          End If

        ElseIf grvRighe.FocusedColumn.Equals(ec_fase) Then
          '------------------------------------
          'zoom Project management task ID
          If oCleGsor.bModPM Then
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

      If Not oCleGsor.CorpoImpRecordSalva(dcImpe.Position, False, Nothing) Then Return

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

      dtrT = oCleGsor.dsImpe.Tables("CORPOIMP").Select("ec_riga = " & lRigaCopiata.ToString)
      If dtrT.Length = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128557652623926788, "Impossibile incollare la riga: non è stata trovata la riga n° |" & lRigaCopiata.ToString & "| copiata."))
        Return
      End If

      oCleGsor.dsImpe.Tables("CORPOIMP").Rows.Add(oCleGsor.dsImpe.Tables("CORPOIMP").NewRow)
      With oCleGsor.dsImpe.Tables("CORPOIMP").Rows(oCleGsor.dsImpe.Tables("CORPOIMP").Rows.Count - 1)
        'forzo la MovordOnAddNewRow
        !codditt = "."
        !codditt = DittaCorrente
        !ec_magaz = dtrT(0)!ec_magaz    'sempre prima di impostare l'articolo, altrimenti non riesce a proporre l'ubicazione dinamica dal magazzino in validaz articolo
        !ec_magaz2 = dtrT(0)!ec_magaz2
        !ec_codart = dtrT(0)!ec_codart
        !ec_fase = dtrT(0)!ec_fase
        !ec_datcons = dtrT(0)!ec_datcons
        !ec_unmis = dtrT(0)!ec_unmis
        !ec_descr = dtrT(0)!ec_descr
        !ec_colli = dtrT(0)!ec_colli
        !ec_quant = dtrT(0)!ec_quant
        !ec_preziva = dtrT(0)!ec_preziva
        !ec_prezvalc = dtrT(0)!ec_prezvalc
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
        !ec_confermato = dtrT(0)!ec_confermato
        !ec_lotto = dtrT(0)!ec_lotto
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
      If oCleGsor.dsImpe.Tables("CORPOIMP").Rows.Count = 0 Then Return
      If grvRighe.FocusedRowHandle < 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128524040843028000, "Posizionarsi prima su una riga già compilata"))
        Return
      End If
      Me.Cursor = Cursors.WaitCursor
      If Not oCleGsor.CorpoImpRecordSalva(dcImpe.Position, False, Nothing) Then Return

      dtrT = oCleGsor.dsImpe.Tables("CORPOIMP").Select("ec_riga < " & NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga).ToString, "ec_riga DESC")
      If dtrT.Length > 0 Then lPrevRiga = NTSCInt(dtrT(0)!ec_riga)
      lNewRiga = NTSCInt(Fix((NTSCDec((NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga) - lPrevRiga) / 2) + lPrevRiga)))

      If oCleGsor.dsImpe.Tables("CORPOIMP").Select("ec_riga = " & lNewRiga.ToString).Length > 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128524045125384000, "Impossibile inserire una riga intermedia; riga |" & lNewRiga.ToString & "| già presente nel CORPOIMP del documento"))
        Return
      End If

      oCleGsor.dsImpe.Tables("CORPOIMP").Rows.Add(oCleGsor.dsImpe.Tables("CORPOIMP").NewRow)
      'forzo la MovordOnAddNewRow
      oCleGsor.dsImpe.Tables("CORPOIMP").Rows(oCleGsor.dsImpe.Tables("CORPOIMP").Rows.Count - 1)!codditt = "."
      oCleGsor.dsImpe.Tables("CORPOIMP").Rows(oCleGsor.dsImpe.Tables("CORPOIMP").Rows.Count - 1)!codditt = DittaCorrente

      oCleGsor.dsImpe.Tables("CORPOIMP").Rows(oCleGsor.dsImpe.Tables("CORPOIMP").Rows.Count - 1)!ec_riga = lNewRiga
      oCleGsor.lCrigheYT -= oCleGsor.nIncremContatoreRiga

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
        oApp.MsgBoxInfo(oApp.Tr(Me, 128776122060942000, "Indicare prima il codice articolo"))
        Return
      End If
      If grvRighe.NTSGetCurrentDataRow!ec_umprz.ToString = "S" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128572288546562500, "Selezione non consentita in presenza di una gestione dei prezzi" & vbCrLf & _
               "riferiti ad una unità di misura diversa dalla principale."))
        Return
      End If
      If grvRighe.NTSGetCurrentDataRow!ec_flevas.ToString = "S" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128572288895000000, "Selezione non consentita sulle righe già evase a saldo."))
        Return
      End If
      If grvRighe.NTSGetCurrentDataRow!ec_flevapre.ToString = "S" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128572289283593750, "Selezione non consentita sulle righe già prenotate a saldo."))
        Return
      End If

      NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
      oParam.strTipo = NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart))
      oParam.nMagaz = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_magaz))   'serve per visual solo i lotti aperti
      oParam.nAnno = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_fase))     'serve per visual solo i lotti aperti
      oParam.strDatreg = dtEt_datdoc.ToShortDateString                          'serve per visual solo i lotti aperti
      NTSZOOM.ZoomStrIn("ZOOMANALOTTI", DittaCorrente, oParam)
      If oParam.bFlag1 = False Then Return

      If grvRighe.NTSGetCurrentDataRow!xxo_geslotti.ToString = "S" Then
        grvRighe.NTSGetCurrentDataRow!xxo_lottox = oParam.strOut
      End If
      If oCleGsor.GetMemDttArti(NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart))).Rows(0)!ar_gesubic.ToString = "S" Then
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

  Public Overridable Sub tlbNavigazMrp_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNavigazMrp.ItemClick
    Dim strParam As String = ""
    Try
      Select Case oCleGsor.dttET.Rows(0)!et_tipork.ToString
        Case "$", "O", "H", "X", "R", "#", "V"
          'posso continuare
        Case Else
          oApp.MsgBoxInfo(oApp.Tr(Me, 128554237518494937, "Funzione non disponibile per questo tipo di documento"))
      End Select

      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128554245705966937, "Per poter effettuare l'operazione richiesta è necessario " & vbCrLf & _
                                                        "posizionarsi su di una riga nel CORPOIMP di un ordine/impegno."))
        Return
      End If

      strParam = "O;" & oCleGsor.dttET.Rows(0)!et_tipork.ToString & ";" & _
                nAnnoDoc & ";" & _
                strSerieDoc.PadRight(1).Substring(0, 1) & ";" & _
                Microsoft.VisualBasic.Right(lNumdoc.ToString.PadLeft(9, "0"c), 9) & ";" & _
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
      oCleGsor.ImportaDaLista(NTSCInt(oPar.dPar2), oPar.bPar1, True)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi di griglia"
  Public Overridable Sub grvRighe_NTSCellValueChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvRighe.NTSCellValueChanged
    Try
      Select Case grvRighe.FocusedColumn.Name.ToUpper
        Case "EC_CODART"
          edPreList.Text = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_prelist).ToString

        Case "EC_SCONT1", "EC_SCONT2", "EC_SCONT3", "EC_SCONT4", "EC_SCONT5", "EC_SCONT6", _
             "EC_PREZZO", "EC_PREZVALC", "EC_PREZIVA", "EC_STASINO", "EC_CODIVA"
          edPreList.Text = NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_prelist).ToString

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
      If oCleGsor.dsImpe Is Nothing Then Return
      If oCleGsor.dsImpe.Tables("CORPOIMP") Is Nothing Then Return
      If oCleGsor.dsImpe.Tables("CORPOIMP").Rows.Count = 0 Then Return
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

      If oCleGsor Is Nothing Then Return True
      If dttUm Is Nothing Then Return True

      '--------------------------------------
      'Se colli e/o quantità prenotati e/o evasi sono diversi da zero,
      'la colonna relativa a 'Taglia' è bloccata
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        GctlSetVisEnab(ec_tctaglia, False)
        GctlSetVisEnab(xxo_tctagliaf, False)
      Else
        If oCleGsor.bModTCO Then
          If (NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_colpre) <> 0) Or _
             (NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_coleva) <> 0) Or _
             (NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_quapre) <> 0) Or _
             (NTSCDec(grvRighe.NTSGetCurrentDataRow!ec_quaeva) <> 0) Then
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
      'compilo il combo delle unità di misura
      If Not ec_unmis.ColumnEdit Is Nothing Then
        CType(ec_unmis.ColumnEdit, NTSRepositoryItemComboBox).DataSource = dttUm
        If grvRighe.FocusedColumn.Name = "ec_unmis" And grvRighe.GetFocusedRowCellValue(ec_codart).ToString <> "" And NTSCStr(grvRighe.GetFocusedRowCellValue(ec_codart)).Trim <> "" Then
          grvRighe.liListCmb.Visible = False    'lo nascondo, visto che contiene tutte le unita di misura del db ...
          ec_unmis.NTSComboValueOk = oCleGsor.GetArticoUnMis(NTSCStr(grvRighe.GetFocusedRowCellValue(ec_codart)))
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
        oApp.MsgBoxInfo(oApp.Tr(Me, 128776122074514000, "Indicare prima il codice articolo"))
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

      If oPar.strParam.Trim <> "" Then
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
      If oCleGsor.AbilitaDisabilitaRigaNum(grvRighe.NTSGetCurrentDataRow) Then
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
      If Not oCleGsor.CorpoImpRecordSalva(dcImpe.Position, False, Nothing) Then Return False
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
      oCleGsor.Leggidisponibilita(strCodart, nMagaz, nFase, lCommeca, dDisponibilita, dDisponibilitaNetta, dUltCost, strGescomm)
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
        oApp.MsgBoxErr(oApp.Tr(Me, 128578169086718750, "Posizionarsi prima su una riga di griglia nella colonna 'codice articolo'"))
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
      oPar.strNomProg = "BNORGSOR"
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
