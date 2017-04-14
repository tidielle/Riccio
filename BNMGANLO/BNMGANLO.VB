Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGANLO

#Region "Variabili"
  Public oCleAnlo As CLEMGANLO
  Public oCallParams As CLE__CLDP
  Public dsAnlo As DataSet
  Public dcAnlo As BindingSource = New BindingSource()
  Public bAfterRipristina As Boolean = False
  Public bPrimo As Boolean = True
  Public bNew As Boolean

  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbApri As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnDescrizioneArticolo As NTSInformatica.NTSPanel
  Public WithEvents lbDescrizioneArticolo As NTSInformatica.NTSLabel
  Public WithEvents pnGriglia As NTSInformatica.NTSPanel
  Public WithEvents grAnlo As NTSInformatica.NTSGrid
  Public WithEvents grvAnlo As NTSInformatica.NTSGridView
  Public WithEvents alo_lotto As NTSInformatica.NTSGridColumn
  Public WithEvents alo_descr As NTSInformatica.NTSGridColumn
  Public WithEvents alo_codalf As NTSInformatica.NTSGridColumn
  Public WithEvents alo_dtprep As NTSInformatica.NTSGridColumn
  Public WithEvents alo_dtscad As NTSInformatica.NTSGridColumn
  Public WithEvents alo_note As NTSInformatica.NTSGridColumn

  Private components As System.ComponentModel.IContainer
#End Region

#Region "Moduli"
  Private Moduli_P As Integer = CLN__STD.bsModMG + CLN__STD.bsModSQ
  Private ModuliExt_P As Integer = 0
  Private ModuliSup_P As Integer = 0
  Private ModuliSupExt_P As Integer = 0
  Private ModuliPtn_P As Integer = 0
  Public WithEvents tlbOle As NTSInformatica.NTSBarButtonItem
  Public WithEvents alo_codpqua As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codpqua As NTSInformatica.NTSGridColumn
  Public WithEvents tlbParametri As NTSInformatica.NTSBarButtonItem
  Public WithEvents alo_lottox As NTSInformatica.NTSGridColumn
  Public WithEvents alo_nazorig As NTSInformatica.NTSGridColumn
  Public WithEvents alo_nazprov As NTSInformatica.NTSGridColumn
  Public WithEvents alo_descr2 As NTSInformatica.NTSGridColumn
  Public WithEvents alo_note2 As NTSInformatica.NTSGridColumn
  Public WithEvents alo_ultagg As NTSInformatica.NTSGridColumn
  Public WithEvents alo_dtlibera As NTSInformatica.NTSGridColumn
  Public WithEvents alo_codlotx As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codlotx As NTSInformatica.NTSGridColumn
  Public WithEvents tlbDeleteEx As NTSInformatica.NTSBarButtonItem
  Public WithEvents alo_dtcarbus As NTSInformatica.NTSGridColumn
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
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
#End Region

#Region "Inizializzazione Controlli"
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGANLO", "BEMGANLO", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128565280057686734, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleAnlo = CType(oTmp, CLEMGANLO)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGANLO", strRemoteServer, strRemotePort)
    AddHandler oCleAnlo.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleAnlo.Init(oApp, oScript, oMenu.oCleComm, "ANALOTTI", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGANLO))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbApri = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbDeleteEx = New NTSInformatica.NTSBarButtonItem
    Me.tlbParametri = New NTSInformatica.NTSBarButtonItem
    Me.tlbOle = New NTSInformatica.NTSBarButtonItem
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
    Me.pnDescrizioneArticolo = New NTSInformatica.NTSPanel
    Me.lbDescrizioneArticolo = New NTSInformatica.NTSLabel
    Me.pnGriglia = New NTSInformatica.NTSPanel
    Me.grAnlo = New NTSInformatica.NTSGrid
    Me.grvAnlo = New NTSInformatica.NTSGridView
    Me.alo_lottox = New NTSInformatica.NTSGridColumn
    Me.alo_lotto = New NTSInformatica.NTSGridColumn
    Me.alo_descr = New NTSInformatica.NTSGridColumn
    Me.alo_codalf = New NTSInformatica.NTSGridColumn
    Me.alo_dtcarbus = New NTSInformatica.NTSGridColumn
    Me.alo_dtprep = New NTSInformatica.NTSGridColumn
    Me.alo_dtscad = New NTSInformatica.NTSGridColumn
    Me.alo_note = New NTSInformatica.NTSGridColumn
    Me.alo_codpqua = New NTSInformatica.NTSGridColumn
    Me.xx_codpqua = New NTSInformatica.NTSGridColumn
    Me.alo_nazorig = New NTSInformatica.NTSGridColumn
    Me.alo_nazprov = New NTSInformatica.NTSGridColumn
    Me.alo_descr2 = New NTSInformatica.NTSGridColumn
    Me.alo_note2 = New NTSInformatica.NTSGridColumn
    Me.alo_ultagg = New NTSInformatica.NTSGridColumn
    Me.alo_dtlibera = New NTSInformatica.NTSGridColumn
    Me.alo_codlotx = New NTSInformatica.NTSGridColumn
    Me.xx_codlotx = New NTSInformatica.NTSGridColumn
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnDescrizioneArticolo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDescrizioneArticolo.SuspendLayout()
    CType(Me.pnGriglia, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGriglia.SuspendLayout()
    CType(Me.grAnlo, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvAnlo, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbApri, Me.tlbSalva, Me.tlbRipristina, Me.tlbCancella, Me.tlbEsci, Me.tlbGuida, Me.tlbOle, Me.tlbParametri, Me.tlbDeleteEx, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbStampa, Me.tlbStampaVideo})
    Me.NtsBarManager1.MaxItemId = 19
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApri), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbParametri, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbOle), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbApri.Id = 1
    Me.tlbApri.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3)
    Me.tlbApri.Name = "tlbApri"
    Me.tlbApri.Visible = True
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.GlyphPath = ""
    Me.tlbSalva.Id = 3
    Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.GlyphPath = ""
    Me.tlbRipristina.Id = 4
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.GlyphPath = ""
    Me.tlbCancella.Id = 5
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbDeleteEx
    '
    Me.tlbDeleteEx.Caption = "Cancella tutte le anagrafiche inutilizzate e/o create per errore"
    Me.tlbDeleteEx.GlyphPath = ""
    Me.tlbDeleteEx.Id = 14
    Me.tlbDeleteEx.Name = "tlbDeleteEx"
    Me.tlbDeleteEx.Visible = True
    '
    'tlbParametri
    '
    Me.tlbParametri.Caption = "Parametri Qualità Lotto"
    Me.tlbParametri.Glyph = CType(resources.GetObject("tlbParametri.Glyph"), System.Drawing.Image)
    Me.tlbParametri.GlyphPath = ""
    Me.tlbParametri.Id = 13
    Me.tlbParametri.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F7))
    Me.tlbParametri.Name = "tlbParametri"
    Me.tlbParametri.Visible = True
    '
    'tlbOle
    '
    Me.tlbOle.Caption = "Ole"
    Me.tlbOle.Glyph = CType(resources.GetObject("tlbOle.Glyph"), System.Drawing.Image)
    Me.tlbOle.GlyphPath = ""
    Me.tlbOle.Id = 12
    Me.tlbOle.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O))
    Me.tlbOle.Name = "tlbOle"
    Me.tlbOle.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 15
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDeleteEx)})
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
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.GlyphPath = ""
    Me.tlbStampa.Id = 17
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa Video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.GlyphPath = ""
    Me.tlbStampaVideo.Id = 18
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
    Me.tlbEsci.Id = 10
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnDescrizioneArticolo
    '
    Me.pnDescrizioneArticolo.AllowDrop = True
    Me.pnDescrizioneArticolo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnDescrizioneArticolo.Appearance.ForeColor = System.Drawing.Color.Black
    Me.pnDescrizioneArticolo.Appearance.Options.UseBackColor = True
    Me.pnDescrizioneArticolo.Appearance.Options.UseForeColor = True
    Me.pnDescrizioneArticolo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnDescrizioneArticolo.Controls.Add(Me.lbDescrizioneArticolo)
    Me.pnDescrizioneArticolo.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnDescrizioneArticolo.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnDescrizioneArticolo.Location = New System.Drawing.Point(0, 30)
    Me.pnDescrizioneArticolo.Name = "pnDescrizioneArticolo"
    Me.pnDescrizioneArticolo.NTSActiveTrasparency = True
    Me.pnDescrizioneArticolo.Size = New System.Drawing.Size(719, 40)
    Me.pnDescrizioneArticolo.TabIndex = 6
    '
    'lbDescrizioneArticolo
    '
    Me.lbDescrizioneArticolo.AutoSize = True
    Me.lbDescrizioneArticolo.BackColor = System.Drawing.Color.Transparent
    Me.lbDescrizioneArticolo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbDescrizioneArticolo.ForeColor = System.Drawing.Color.Black
    Me.lbDescrizioneArticolo.Location = New System.Drawing.Point(12, 12)
    Me.lbDescrizioneArticolo.Name = "lbDescrizioneArticolo"
    Me.lbDescrizioneArticolo.NTSDbField = ""
    Me.lbDescrizioneArticolo.Size = New System.Drawing.Size(122, 13)
    Me.lbDescrizioneArticolo.TabIndex = 0
    Me.lbDescrizioneArticolo.Text = "Articolo/Descrizione"
    Me.lbDescrizioneArticolo.Tooltip = ""
    Me.lbDescrizioneArticolo.UseMnemonic = False
    '
    'pnGriglia
    '
    Me.pnGriglia.AllowDrop = True
    Me.pnGriglia.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGriglia.Appearance.Options.UseBackColor = True
    Me.pnGriglia.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGriglia.Controls.Add(Me.grAnlo)
    Me.pnGriglia.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGriglia.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGriglia.Location = New System.Drawing.Point(0, 70)
    Me.pnGriglia.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGriglia.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGriglia.Name = "pnGriglia"
    Me.pnGriglia.NTSActiveTrasparency = True
    Me.pnGriglia.Size = New System.Drawing.Size(719, 230)
    Me.pnGriglia.TabIndex = 7
    '
    'grAnlo
    '
    Me.grAnlo.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grAnlo.EmbeddedNavigator.Name = ""
    Me.grAnlo.Location = New System.Drawing.Point(0, 0)
    Me.grAnlo.MainView = Me.grvAnlo
    Me.grAnlo.Name = "grAnlo"
    Me.grAnlo.Size = New System.Drawing.Size(719, 230)
    Me.grAnlo.TabIndex = 4
    Me.grAnlo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvAnlo})
    '
    'grvAnlo
    '
    Me.grvAnlo.ActiveFilterEnabled = False
    Me.grvAnlo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.alo_lottox, Me.alo_lotto, Me.alo_descr, Me.alo_codalf, Me.alo_dtcarbus, Me.alo_dtprep, Me.alo_dtscad, Me.alo_note, Me.alo_codpqua, Me.xx_codpqua, Me.alo_nazorig, Me.alo_nazprov, Me.alo_descr2, Me.alo_note2, Me.alo_ultagg, Me.alo_dtlibera, Me.alo_codlotx, Me.xx_codlotx})
    Me.grvAnlo.CustomizationFormBounds = New System.Drawing.Rectangle(592, 326, 208, 170)
    Me.grvAnlo.Enabled = True
    Me.grvAnlo.GridControl = Me.grAnlo
    Me.grvAnlo.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvAnlo.MinRowHeight = 14
    Me.grvAnlo.Name = "grvAnlo"
    Me.grvAnlo.NTSAllowDelete = True
    Me.grvAnlo.NTSAllowInsert = True
    Me.grvAnlo.NTSAllowUpdate = True
    Me.grvAnlo.NTSMenuContext = Nothing
    Me.grvAnlo.OptionsCustomization.AllowRowSizing = True
    Me.grvAnlo.OptionsFilter.AllowFilterEditor = False
    Me.grvAnlo.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvAnlo.OptionsNavigation.UseTabKey = False
    Me.grvAnlo.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvAnlo.OptionsView.ColumnAutoWidth = False
    Me.grvAnlo.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvAnlo.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvAnlo.OptionsView.ShowGroupPanel = False
    Me.grvAnlo.RowHeight = 14
    '
    'alo_lottox
    '
    Me.alo_lottox.AppearanceCell.Options.UseBackColor = True
    Me.alo_lottox.AppearanceCell.Options.UseTextOptions = True
    Me.alo_lottox.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alo_lottox.Caption = "Lotto"
    Me.alo_lottox.Enabled = False
    Me.alo_lottox.FieldName = "alo_lottox"
    Me.alo_lottox.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alo_lottox.Name = "alo_lottox"
    Me.alo_lottox.NTSRepositoryComboBox = Nothing
    Me.alo_lottox.NTSRepositoryItemCheck = Nothing
    Me.alo_lottox.NTSRepositoryItemMemo = Nothing
    Me.alo_lottox.NTSRepositoryItemText = Nothing
    Me.alo_lottox.OptionsColumn.AllowEdit = False
    Me.alo_lottox.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alo_lottox.OptionsColumn.ReadOnly = True
    Me.alo_lottox.OptionsFilter.AllowFilter = False
    Me.alo_lottox.Visible = True
    Me.alo_lottox.VisibleIndex = 0
    '
    'alo_lotto
    '
    Me.alo_lotto.AppearanceCell.Options.UseBackColor = True
    Me.alo_lotto.AppearanceCell.Options.UseTextOptions = True
    Me.alo_lotto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alo_lotto.Caption = "Lotto (num)"
    Me.alo_lotto.Enabled = False
    Me.alo_lotto.FieldName = "alo_lotto"
    Me.alo_lotto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alo_lotto.Name = "alo_lotto"
    Me.alo_lotto.NTSRepositoryComboBox = Nothing
    Me.alo_lotto.NTSRepositoryItemCheck = Nothing
    Me.alo_lotto.NTSRepositoryItemMemo = Nothing
    Me.alo_lotto.NTSRepositoryItemText = Nothing
    Me.alo_lotto.OptionsColumn.AllowEdit = False
    Me.alo_lotto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alo_lotto.OptionsColumn.ReadOnly = True
    Me.alo_lotto.OptionsFilter.AllowFilter = False
    Me.alo_lotto.Width = 63
    '
    'alo_descr
    '
    Me.alo_descr.AppearanceCell.Options.UseBackColor = True
    Me.alo_descr.AppearanceCell.Options.UseTextOptions = True
    Me.alo_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alo_descr.Caption = "Descrizione"
    Me.alo_descr.Enabled = True
    Me.alo_descr.FieldName = "alo_descr"
    Me.alo_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alo_descr.Name = "alo_descr"
    Me.alo_descr.NTSRepositoryComboBox = Nothing
    Me.alo_descr.NTSRepositoryItemCheck = Nothing
    Me.alo_descr.NTSRepositoryItemMemo = Nothing
    Me.alo_descr.NTSRepositoryItemText = Nothing
    Me.alo_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alo_descr.OptionsFilter.AllowFilter = False
    Me.alo_descr.Visible = True
    Me.alo_descr.VisibleIndex = 1
    Me.alo_descr.Width = 125
    '
    'alo_codalf
    '
    Me.alo_codalf.AppearanceCell.Options.UseBackColor = True
    Me.alo_codalf.AppearanceCell.Options.UseTextOptions = True
    Me.alo_codalf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alo_codalf.Caption = "ID lotto alfanum."
    Me.alo_codalf.Enabled = True
    Me.alo_codalf.FieldName = "alo_codalf"
    Me.alo_codalf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alo_codalf.Name = "alo_codalf"
    Me.alo_codalf.NTSRepositoryComboBox = Nothing
    Me.alo_codalf.NTSRepositoryItemCheck = Nothing
    Me.alo_codalf.NTSRepositoryItemMemo = Nothing
    Me.alo_codalf.NTSRepositoryItemText = Nothing
    Me.alo_codalf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alo_codalf.OptionsFilter.AllowFilter = False
    Me.alo_codalf.Visible = True
    Me.alo_codalf.VisibleIndex = 2
    Me.alo_codalf.Width = 125
    '
    'alo_dtcarbus
    '
    Me.alo_dtcarbus.AppearanceCell.Options.UseBackColor = True
    Me.alo_dtcarbus.AppearanceCell.Options.UseTextOptions = True
    Me.alo_dtcarbus.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alo_dtcarbus.Caption = "Data carico in Bus"
    Me.alo_dtcarbus.Enabled = True
    Me.alo_dtcarbus.FieldName = "alo_dtcarbus"
    Me.alo_dtcarbus.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alo_dtcarbus.Name = "alo_dtcarbus"
    Me.alo_dtcarbus.NTSRepositoryComboBox = Nothing
    Me.alo_dtcarbus.NTSRepositoryItemCheck = Nothing
    Me.alo_dtcarbus.NTSRepositoryItemMemo = Nothing
    Me.alo_dtcarbus.NTSRepositoryItemText = Nothing
    Me.alo_dtcarbus.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alo_dtcarbus.OptionsFilter.AllowFilter = False
    Me.alo_dtcarbus.Visible = True
    Me.alo_dtcarbus.VisibleIndex = 3
    '
    'alo_dtprep
    '
    Me.alo_dtprep.AppearanceCell.Options.UseBackColor = True
    Me.alo_dtprep.AppearanceCell.Options.UseTextOptions = True
    Me.alo_dtprep.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alo_dtprep.Caption = "Dt. prep./prod."
    Me.alo_dtprep.Enabled = True
    Me.alo_dtprep.FieldName = "alo_dtprep"
    Me.alo_dtprep.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alo_dtprep.Name = "alo_dtprep"
    Me.alo_dtprep.NTSRepositoryComboBox = Nothing
    Me.alo_dtprep.NTSRepositoryItemCheck = Nothing
    Me.alo_dtprep.NTSRepositoryItemMemo = Nothing
    Me.alo_dtprep.NTSRepositoryItemText = Nothing
    Me.alo_dtprep.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alo_dtprep.OptionsFilter.AllowFilter = False
    Me.alo_dtprep.Visible = True
    Me.alo_dtprep.VisibleIndex = 4
    Me.alo_dtprep.Width = 90
    '
    'alo_dtscad
    '
    Me.alo_dtscad.AppearanceCell.Options.UseBackColor = True
    Me.alo_dtscad.AppearanceCell.Options.UseTextOptions = True
    Me.alo_dtscad.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alo_dtscad.Caption = "Dt. scadenza"
    Me.alo_dtscad.Enabled = True
    Me.alo_dtscad.FieldName = "alo_dtscad"
    Me.alo_dtscad.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alo_dtscad.Name = "alo_dtscad"
    Me.alo_dtscad.NTSRepositoryComboBox = Nothing
    Me.alo_dtscad.NTSRepositoryItemCheck = Nothing
    Me.alo_dtscad.NTSRepositoryItemMemo = Nothing
    Me.alo_dtscad.NTSRepositoryItemText = Nothing
    Me.alo_dtscad.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alo_dtscad.OptionsFilter.AllowFilter = False
    Me.alo_dtscad.Visible = True
    Me.alo_dtscad.VisibleIndex = 5
    Me.alo_dtscad.Width = 76
    '
    'alo_note
    '
    Me.alo_note.AppearanceCell.Options.UseBackColor = True
    Me.alo_note.AppearanceCell.Options.UseTextOptions = True
    Me.alo_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alo_note.Caption = "Note"
    Me.alo_note.Enabled = True
    Me.alo_note.FieldName = "alo_note"
    Me.alo_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alo_note.Name = "alo_note"
    Me.alo_note.NTSRepositoryComboBox = Nothing
    Me.alo_note.NTSRepositoryItemCheck = Nothing
    Me.alo_note.NTSRepositoryItemMemo = Nothing
    Me.alo_note.NTSRepositoryItemText = Nothing
    Me.alo_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alo_note.OptionsFilter.AllowFilter = False
    Me.alo_note.Visible = True
    Me.alo_note.VisibleIndex = 6
    Me.alo_note.Width = 211
    '
    'alo_codpqua
    '
    Me.alo_codpqua.AppearanceCell.Options.UseBackColor = True
    Me.alo_codpqua.AppearanceCell.Options.UseTextOptions = True
    Me.alo_codpqua.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alo_codpqua.Caption = "C. parametro qualità lotto"
    Me.alo_codpqua.Enabled = True
    Me.alo_codpqua.FieldName = "alo_codpqua"
    Me.alo_codpqua.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alo_codpqua.Name = "alo_codpqua"
    Me.alo_codpqua.NTSRepositoryComboBox = Nothing
    Me.alo_codpqua.NTSRepositoryItemCheck = Nothing
    Me.alo_codpqua.NTSRepositoryItemMemo = Nothing
    Me.alo_codpqua.NTSRepositoryItemText = Nothing
    Me.alo_codpqua.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alo_codpqua.OptionsFilter.AllowFilter = False
    Me.alo_codpqua.Visible = True
    Me.alo_codpqua.VisibleIndex = 7
    '
    'xx_codpqua
    '
    Me.xx_codpqua.AppearanceCell.Options.UseBackColor = True
    Me.xx_codpqua.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codpqua.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codpqua.Caption = "Descr. par. qualità lotto"
    Me.xx_codpqua.Enabled = False
    Me.xx_codpqua.FieldName = "xx_codpqua"
    Me.xx_codpqua.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codpqua.Name = "xx_codpqua"
    Me.xx_codpqua.NTSRepositoryComboBox = Nothing
    Me.xx_codpqua.NTSRepositoryItemCheck = Nothing
    Me.xx_codpqua.NTSRepositoryItemMemo = Nothing
    Me.xx_codpqua.NTSRepositoryItemText = Nothing
    Me.xx_codpqua.OptionsColumn.AllowEdit = False
    Me.xx_codpqua.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codpqua.OptionsColumn.ReadOnly = True
    Me.xx_codpqua.OptionsFilter.AllowFilter = False
    Me.xx_codpqua.Visible = True
    Me.xx_codpqua.VisibleIndex = 8
    '
    'alo_nazorig
    '
    Me.alo_nazorig.AppearanceCell.Options.UseBackColor = True
    Me.alo_nazorig.AppearanceCell.Options.UseTextOptions = True
    Me.alo_nazorig.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alo_nazorig.Caption = "Naz. origine"
    Me.alo_nazorig.Enabled = True
    Me.alo_nazorig.FieldName = "alo_nazorig"
    Me.alo_nazorig.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alo_nazorig.Name = "alo_nazorig"
    Me.alo_nazorig.NTSRepositoryComboBox = Nothing
    Me.alo_nazorig.NTSRepositoryItemCheck = Nothing
    Me.alo_nazorig.NTSRepositoryItemMemo = Nothing
    Me.alo_nazorig.NTSRepositoryItemText = Nothing
    Me.alo_nazorig.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alo_nazorig.OptionsFilter.AllowFilter = False
    Me.alo_nazorig.Visible = True
    Me.alo_nazorig.VisibleIndex = 9
    '
    'alo_nazprov
    '
    Me.alo_nazprov.AppearanceCell.Options.UseBackColor = True
    Me.alo_nazprov.AppearanceCell.Options.UseTextOptions = True
    Me.alo_nazprov.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alo_nazprov.Caption = "Naz. proven."
    Me.alo_nazprov.Enabled = True
    Me.alo_nazprov.FieldName = "alo_nazprov"
    Me.alo_nazprov.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alo_nazprov.Name = "alo_nazprov"
    Me.alo_nazprov.NTSRepositoryComboBox = Nothing
    Me.alo_nazprov.NTSRepositoryItemCheck = Nothing
    Me.alo_nazprov.NTSRepositoryItemMemo = Nothing
    Me.alo_nazprov.NTSRepositoryItemText = Nothing
    Me.alo_nazprov.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alo_nazprov.OptionsFilter.AllowFilter = False
    Me.alo_nazprov.Visible = True
    Me.alo_nazprov.VisibleIndex = 10
    '
    'alo_descr2
    '
    Me.alo_descr2.AppearanceCell.Options.UseBackColor = True
    Me.alo_descr2.AppearanceCell.Options.UseTextOptions = True
    Me.alo_descr2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alo_descr2.Caption = "Descriz. interna"
    Me.alo_descr2.Enabled = True
    Me.alo_descr2.FieldName = "alo_descr2"
    Me.alo_descr2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alo_descr2.Name = "alo_descr2"
    Me.alo_descr2.NTSRepositoryComboBox = Nothing
    Me.alo_descr2.NTSRepositoryItemCheck = Nothing
    Me.alo_descr2.NTSRepositoryItemMemo = Nothing
    Me.alo_descr2.NTSRepositoryItemText = Nothing
    Me.alo_descr2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alo_descr2.OptionsFilter.AllowFilter = False
    Me.alo_descr2.Visible = True
    Me.alo_descr2.VisibleIndex = 11
    '
    'alo_note2
    '
    Me.alo_note2.AppearanceCell.Options.UseBackColor = True
    Me.alo_note2.AppearanceCell.Options.UseTextOptions = True
    Me.alo_note2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alo_note2.Caption = "Note 2"
    Me.alo_note2.Enabled = True
    Me.alo_note2.FieldName = "alo_note2"
    Me.alo_note2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alo_note2.Name = "alo_note2"
    Me.alo_note2.NTSRepositoryComboBox = Nothing
    Me.alo_note2.NTSRepositoryItemCheck = Nothing
    Me.alo_note2.NTSRepositoryItemMemo = Nothing
    Me.alo_note2.NTSRepositoryItemText = Nothing
    Me.alo_note2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alo_note2.OptionsFilter.AllowFilter = False
    Me.alo_note2.Visible = True
    Me.alo_note2.VisibleIndex = 12
    '
    'alo_ultagg
    '
    Me.alo_ultagg.AppearanceCell.Options.UseBackColor = True
    Me.alo_ultagg.AppearanceCell.Options.UseTextOptions = True
    Me.alo_ultagg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alo_ultagg.Caption = "Dt. ult. agg."
    Me.alo_ultagg.Enabled = True
    Me.alo_ultagg.FieldName = "alo_ultagg"
    Me.alo_ultagg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alo_ultagg.Name = "alo_ultagg"
    Me.alo_ultagg.NTSRepositoryComboBox = Nothing
    Me.alo_ultagg.NTSRepositoryItemCheck = Nothing
    Me.alo_ultagg.NTSRepositoryItemMemo = Nothing
    Me.alo_ultagg.NTSRepositoryItemText = Nothing
    Me.alo_ultagg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alo_ultagg.OptionsFilter.AllowFilter = False
    Me.alo_ultagg.Visible = True
    Me.alo_ultagg.VisibleIndex = 13
    '
    'alo_dtlibera
    '
    Me.alo_dtlibera.AppearanceCell.Options.UseBackColor = True
    Me.alo_dtlibera.AppearanceCell.Options.UseTextOptions = True
    Me.alo_dtlibera.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alo_dtlibera.Caption = "Dt. libera"
    Me.alo_dtlibera.Enabled = True
    Me.alo_dtlibera.FieldName = "alo_dtlibera"
    Me.alo_dtlibera.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alo_dtlibera.Name = "alo_dtlibera"
    Me.alo_dtlibera.NTSRepositoryComboBox = Nothing
    Me.alo_dtlibera.NTSRepositoryItemCheck = Nothing
    Me.alo_dtlibera.NTSRepositoryItemMemo = Nothing
    Me.alo_dtlibera.NTSRepositoryItemText = Nothing
    Me.alo_dtlibera.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alo_dtlibera.OptionsFilter.AllowFilter = False
    Me.alo_dtlibera.Visible = True
    Me.alo_dtlibera.VisibleIndex = 14
    '
    'alo_codlotx
    '
    Me.alo_codlotx.AppearanceCell.Options.UseBackColor = True
    Me.alo_codlotx.AppearanceCell.Options.UseTextOptions = True
    Me.alo_codlotx.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alo_codlotx.Caption = "cod. mod. creaz. lotto"
    Me.alo_codlotx.Enabled = False
    Me.alo_codlotx.FieldName = "alo_codlotx"
    Me.alo_codlotx.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alo_codlotx.Name = "alo_codlotx"
    Me.alo_codlotx.NTSRepositoryComboBox = Nothing
    Me.alo_codlotx.NTSRepositoryItemCheck = Nothing
    Me.alo_codlotx.NTSRepositoryItemMemo = Nothing
    Me.alo_codlotx.NTSRepositoryItemText = Nothing
    Me.alo_codlotx.OptionsColumn.AllowEdit = False
    Me.alo_codlotx.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alo_codlotx.OptionsColumn.ReadOnly = True
    Me.alo_codlotx.OptionsFilter.AllowFilter = False
    Me.alo_codlotx.Visible = True
    Me.alo_codlotx.VisibleIndex = 15
    '
    'xx_codlotx
    '
    Me.xx_codlotx.AppearanceCell.Options.UseBackColor = True
    Me.xx_codlotx.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codlotx.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codlotx.Caption = "Des. mod. creaz. lotto"
    Me.xx_codlotx.Enabled = False
    Me.xx_codlotx.FieldName = "xx_codlotx"
    Me.xx_codlotx.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codlotx.Name = "xx_codlotx"
    Me.xx_codlotx.NTSRepositoryComboBox = Nothing
    Me.xx_codlotx.NTSRepositoryItemCheck = Nothing
    Me.xx_codlotx.NTSRepositoryItemMemo = Nothing
    Me.xx_codlotx.NTSRepositoryItemText = Nothing
    Me.xx_codlotx.OptionsColumn.AllowEdit = False
    Me.xx_codlotx.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codlotx.OptionsColumn.ReadOnly = True
    Me.xx_codlotx.OptionsFilter.AllowFilter = False
    Me.xx_codlotx.Visible = True
    Me.xx_codlotx.VisibleIndex = 16
    '
    'FRMMGANLO
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(719, 300)
    Me.Controls.Add(Me.pnGriglia)
    Me.Controls.Add(Me.pnDescrizioneArticolo)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMMGANLO"
    Me.Text = "ANAGRAFICA LOTTI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnDescrizioneArticolo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDescrizioneArticolo.ResumeLayout(False)
    Me.pnDescrizioneArticolo.PerformLayout()
    CType(Me.pnGriglia, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGriglia.ResumeLayout(False)
    CType(Me.grAnlo, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvAnlo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '----------------------------------------------------------------------------------
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbApri.GlyphPath = (oApp.ChildImageDir & "\open.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\recagg.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\recrestore.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\recdelete.gif")
        tlbOle.GlyphPath = (oApp.ChildImageDir & "\ole_1.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbParametri.GlyphPath = (oApp.ChildImageDir & "\ordini.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      '----------------------------------------------------------------------------------
      tlbMain.NTSSetToolTip()
      '----------------------------------------------------------------------------------
      grvAnlo.NTSSetParam(oMenu, oApp.Tr(Me, 128715581046690858, "Griglia Anagrafica Lotti"))
      grvAnlo.NTSAllowInsert = False
      alo_lotto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128715581274347108, "Lotto"), "0", 9, 0, 999999999) 'vecchio, non modificabile
      alo_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128715581539034608, "Descrizione"), 40, True)
      alo_codalf.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128715582406387733, "ID lotto alfanumerico"), 18, False)
      alo_dtprep.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128715583044043983, "Data preparazione/produzione"), True)
      alo_dtscad.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128715583632168983, "Data scadenza"), False)
      alo_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128715878637136736, "Note"), 0, True)
      alo_codpqua.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129376509206271846, "C. parametro qualità lotto"), tabpqua)
      xx_codpqua.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129376509727678096, "Descr. par. qualità lotto"), 0)
      alo_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129512458216599975, "Lotto"), 50, False) 'nuova chiave alfanumerica
      alo_nazorig.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129512458216687860, "Naz. origine"), 3, True)
      alo_nazprov.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129512458216697625, "Naz. proven."), 3, True)
      alo_descr2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129512458216707390, "Descriz. interna"), 40, True)
      alo_note2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129512458216717155, "Note 2"), 255, True)
      alo_ultagg.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129512458216726920, "Dt. ult. agg."), True)
      alo_dtlibera.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129512458216736685, "Dt. libera"), True)
      alo_codlotx.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129512458216756215, "cod. mod. creaz. lotto"), tablotx)
      xx_codlotx.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129512458216765980, "Des. mod. creaz. lotto"), 0, True)
      alo_dtcarbus.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129527706460546875, "Data primo carico in Bus"), False)

      '----------------------------------------------------------------------------------
      '--- Chiamo lo script per inizializzare i controlli caricati con source ext
      '----------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub
#End Region

#Region "Form"
  Public Overridable Sub FRMMGANLO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim i As Integer = 0
    Dim dttTmp As New DataTable
    Try
      '----------------------------------------------------------------------------------
      InitControls()
      '----------------------------------------------------------------------------------
      SetStato0()
      '----------------------------------------------------------------------------------
      oCleAnlo.strGenNumLotto = NTSCStr(oMenu.GetSettingBus("BSMGANLO", "OPZIONI", ".", "GenNumLotto", "N", " ", "N"))

      oMenu.ValCodiceDb(DittaCorrente, DittaCorrente, "ANADITAC", "S", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then
        oCleAnlo.bLottoNew = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ac_lotti2) = "S", True, False))
      End If
      dttTmp.Clear()
      oCleAnlo.bLottoUnivoco = CBool(oMenu.GetSettingBus("OPZIONI", ".", ".", "LottoUnivoco", "0", " ", "0"))

      '----------------------------------------------------------------------------------
      GctlSetRoules()
      '----------------------------------------------------------------------------------
      '--- Sono stato chiamato da un altro child
      '----------------------------------------------------------------------------------
      If Not oCallParams Is Nothing Then
        tlbNuovo.Enabled = False
        tlbApri.Enabled = False
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
          tlbNuovo_ItemClick(Me, Nothing)
        ElseIf Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) <> "" Then
          tlbApri_ItemClick(Me, Nothing)
        End If
      End If
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
    End Try
  End Sub

  Public Overridable Sub FRMMGANLO_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcAnlo.Dispose()
      dsAnlo.Dispose()
    Catch
    End Try
  End Sub

  Public Overridable Sub FRMMGANLO_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If oCleAnlo.RecordIsChanged Then
        Select Case oApp.MsgBoxInfoYesNoCancel_DefYes(oApp.Tr(Me, 130089597464870975, "Salvare le eventuali modifiche prima di uscire?"))
          Case Windows.Forms.DialogResult.Cancel
            e.Cancel = True
          Case Windows.Forms.DialogResult.No
            Try
              oCallParams.strParam = ""
            Catch ex As Exception
            End Try
            Return
          Case Windows.Forms.DialogResult.Yes
            If Not Salva() Then e.Cancel = True
        End Select
      End If
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Griglia"
  Public Overridable Sub grvAnlo_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvAnlo.NTSBeforeRowUpdate
    Try
      If Not Salva() Then
        e.Allow = False
      End If
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub grvAnlo_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvAnlo.NTSFocusedRowChanged
    Try
      '----------------------------------------------------------------------------------
      '--- Se il numero di lotto è diverso da 0 blocco la colonna,
      '--- diversamente la rendo editabile
      '----------------------------------------------------------------------------------
      'con la gestione lotti alfanum. non è possibile aggiungere righe direttamente in griglia!!!!
      'If NTSCStr(grvAnlo.GetFocusedRowCellValue(alo_lottox)).Trim <> "" Then
      '  alo_lottox.Enabled = False
      '  alo_codpqua.Enabled = False
      'Else
      '  GctlSetVisEnab(alo_lottox, False)
      '  GctlSetVisEnab(alo_codpqua, False)
      '  If grvAnlo.GetFocusedRowCellValue(alo_lottox).ToString.Trim = "" Then
      '    grvAnlo.FocusedColumn = alo_lottox
      '    If oCleAnlo.strGenNumLotto <> "N" Then
      '      If Not bAfterRipristina Then ' Dopo un ripristina sulla riga vuota non devo subito ricreare una riga
      '        grvAnlo.SetFocusedValue(oCleAnlo.ImpostaNuovoLotto)
      '        grvAnlo.NTSMoveNextColunn()
      '      Else
      '        If bPrimo Then
      '          bPrimo = False
      '        Else
      '          bPrimo = True
      '          bAfterRipristina = False
      '        End If
      '      End If
      '    End If
      '  End If
      'End If
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Dim fmSelo As FRMMGSELO = Nothing
    Try

      '----------------------------------------------------------------------------------
      If pnDescrizioneArticolo.Visible = True Then
        If Not Salva() Then Return
        ChiudiDs()
        SetStato0()
      End If
      '----------------------------------------------------------------------------------
      fmSelo = CType(NTSNewFormModal("FRMMGSELO"), FRMMGSELO)
      fmSelo.bNew = True
      fmSelo.Init(oMenu, Nothing, DittaCorrente)
      fmSelo.Text = oApp.Tr(Me, 129376615091681782, "Nuova Anagrafica Articolo/Lotto")
      If Not oCallParams Is Nothing Then
        fmSelo.bDaProgramma = True
        fmSelo.strCodart = Microsoft.VisualBasic.Mid(oCallParams.strParam, 6, CLN__STD.CodartMaxLen).Trim
      End If
      fmSelo.InitEntity(oCleAnlo)
      fmSelo.ShowDialog(Me)
      If fmSelo.bAnnullato = True Then
        If oCallParams Is Nothing Then
          Return
        Else
          oCallParams.strParam = ""
          Me.Close()
          Return
        End If
      End If
      bNew = True
      '----------------------------------------------------------------------------------
      If Not oCleAnlo.Nuovo(oCleAnlo.strCodart, oCleAnlo.lLotto, dsAnlo) Then Return
      '----------------------------------------------------------------------------------
      dcAnlo.DataSource = dsAnlo.Tables("ANALOTTI")
      dsAnlo.AcceptChanges()
      dcAnlo.MoveFirst()
      '----------------------------------------------------------------------------------
      grAnlo.DataSource = dcAnlo
      '----------------------------------------------------------------------------------
      SettaDescrizioni()
      '----------------------------------------------------------------------------------
      grvAnlo.Focus()
      grvAnlo.NTSNuovo()
      '----------------------------------------------------------------------------------
      oCleAnlo.ImpostaNuovaRiga()
      GctlApplicaDefaultValueGrid(grvAnlo)

      SetStato1()
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      If Not fmSelo Is Nothing Then fmSelo.Dispose()
      fmSelo = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbApri_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApri.ItemClick
    Dim fmSelo As FRMMGSELO = Nothing
    Try
      '----------------------------------------------------------------------------------
      If pnDescrizioneArticolo.Visible = True Then
        If Not Salva() Then Return
        ChiudiDs()
        SetStato0()
      End If
      '----------------------------------------------------------------------------------
      '--- Se chiamato da programma esterno, salta la chiamata alla finestra di apertura
      '----------------------------------------------------------------------------------
      If oCallParams Is Nothing Then
        fmSelo = CType(NTSNewFormModal("FRMMGSELO"), FRMMGSELO)
        fmSelo.bNew = False
        fmSelo.Init(oMenu, Nothing, DittaCorrente)
        fmSelo.Text = oApp.Tr(Me, 129376614990282022, "Apri Anagrafica Articolo/Lotto")
        fmSelo.InitEntity(oCleAnlo)
        fmSelo.ShowDialog(Me)
        If fmSelo.bAnnullato = True Then Return
        bNew = False
      Else
        oCleAnlo.strCodart = Microsoft.VisualBasic.Mid(oCallParams.strParam, 6, CLN__STD.CodartMaxLen).Trim
        oCleAnlo.lLotto = NTSCInt(Microsoft.VisualBasic.Mid(oCallParams.strParam, 6 + CLN__STD.CodartMaxLen + 1, 9))
      End If
      '----------------------------------------------------------------------------------
      If Not oCleAnlo.Apri(oCleAnlo.strCodart, oCleAnlo.lLotto, dsAnlo) Then Return
      '----------------------------------------------------------------------------------
      dcAnlo.DataSource = dsAnlo.Tables("ANALOTTI")
      dsAnlo.AcceptChanges()
      dcAnlo.MoveFirst()
      '----------------------------------------------------------------------------------
      grAnlo.DataSource = dcAnlo
      '----------------------------------------------------------------------------------
      SetStato1()
      '----------------------------------------------------------------------------------
      SettaDescrizioni()
      '----------------------------------------------------------------------------------
      grvAnlo.Focus()
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      If Not fmSelo Is Nothing Then fmSelo.Dispose()
      fmSelo = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvAnlo.NTSRipristinaRigaCorrenteBefore(dcAnlo, True) Then Return
      bAfterRipristina = True
      oCleAnlo.Ripristina(dcAnlo.Position, dcAnlo.Filter)
      grvAnlo.NTSRipristinaRigaCorrenteAfter()

      If grvAnlo.RowCount = 0 Then SetStato0()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Try
      If Not grvAnlo.NTSDeleteRigaCorrente(dcAnlo, True) Then Return
      If Not oCleAnlo.Salva(True) Then
        dsAnlo.Tables("ANALOTTI").RejectChanges()
        Return
      End If

      If grvAnlo.RowCount = 0 Then SetStato0()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbDeleteEx_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDeleteEx.ItemClick
    Try
      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 129526920654179687, "Confermi la cancellazione delle anagrafiche lotti inutilizzate e/o create per errore?" & vbCrLf & _
                                                                    "(Nessuno deve lavorare in Magazzino/Ordini/Proposte d'ordine/Liste selezionate articoli/Liste di produzione/Progressivi articoli/Picking)")) = Windows.Forms.DialogResult.No Then Return

      Me.Cursor = Cursors.WaitCursor
      oCleAnlo.DeleteAnalotti()

      oApp.MsgBoxInfo(oApp.Tr(Me, 129526920353388671, "Elaborazione completata"))
      Me.Close()

    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbOle_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbOle.ItemClick
    Dim strParam As String = ""
    Dim strCodart As String
    Dim strLotto As String
    Try
      If Not Salva() Then Exit Sub

      If grvAnlo.NTSGetCurrentDataRow Is Nothing Then Return

      strCodart = oCleAnlo.strCodart
      strLotto = NTSCStr(grvAnlo.NTSGetCurrentDataRow()!alo_lotto)
      If strCodart.Trim <> "" Then
        strParam = "APRI§L§0§" & strCodart & "§" & strLotto
        oMenu.RunChild("BS__AOLE", "CLS__AOLE", oApp.Tr(Me, 128575012873982601, "Oggetti associati"), DittaCorrente, "", "", Nothing, strParam, True, True)
      End If

    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Try
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbParametri_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbParametri.ItemClick
    Dim fmValo As FRMSQVALO = Nothing
    Try
      If grvAnlo.NTSGetCurrentDataRow Is Nothing Then Return

      If NTSCInt(grvAnlo.NTSGetCurrentDataRow!alo_codpqua) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129376523548771846, "Indicare un codice parametro qualità valido prima di passare all'inserimento dei valori."))
        Return
      End If

      If Not Salva() Then Return

      If Not oCleAnlo.VerificaCampqua(NTSCInt(grvAnlo.NTSGetCurrentDataRow!alo_codpqua)) Then Return

      fmValo = CType(NTSNewFormModal("FRMSQVALO"), FRMSQVALO)
      If Not fmValo.Init(oMenu, Nothing, DittaCorrente) Then Return
      fmValo.InitEntity(oCleAnlo)
      fmValo.strValoCodart = NTSCStr(grvAnlo.NTSGetCurrentDataRow!alo_codart)
      fmValo.lValoLotto = NTSCInt(grvAnlo.NTSGetCurrentDataRow!alo_lotto)
      fmValo.nValoCodpqua = NTSCInt(grvAnlo.NTSGetCurrentDataRow!alo_codpqua)
      fmValo.bValoAnnulla = False

      fmValo.ShowDialog()

      If fmValo.bValoAnnulla Then
        If Not oCleAnlo.CancellaVallotti(NTSCStr(grvAnlo.NTSGetCurrentDataRow!alo_codart), NTSCInt(grvAnlo.NTSGetCurrentDataRow!alo_lotto)) Then Return
      Else
        alo_codpqua.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If Not fmValo Is Nothing Then fmValo.Dispose()
      fmValo = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      Stampa(1)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      Stampa(0)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Funzioni"
  Public Overridable Sub ChiudiDs()
    Try
      If grAnlo.Visible = True Then
        dcAnlo.Clear()
        dsAnlo.Clear()
      End If
    Catch
    End Try
  End Sub

  Public Overridable Function Salva() As Boolean
    Try
      '----------------------------------------------------------------------------------
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus 
      grvAnlo.NTSMoveFirstColunn()
      '----------------------------------------------------------------------------------
      Dim dRes As DialogResult
      If oCleAnlo.RecordIsChanged Then
        If dsAnlo.Tables("ANALOTTI").Select("", "", DataViewRowState.Added).Length > 0 Then
          grvAnlo.NTSAllowInsert = True 'diversamente se si applicava un filtro sulle colonne e la ricerca poteva restituire più record il filtro inspiegabilmente non veniva applicato
        End If
      End If
      dRes = grvAnlo.NTSSalvaRigaCorrente(dcAnlo, oCleAnlo.RecordIsChanged, False)
      grvAnlo.NTSAllowInsert = False

      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False
          '------------------------------------------------------------------------------
          If Not oCleAnlo.Salva(False) Then Return False
          'se sono stato chiamato da zoom lotti devo chiudermi e restituire il numero di lotto creato
          If Not oCallParams Is Nothing Then
            If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
              If oCleAnlo.bLottoNew Then
                oCallParams.strParam = NTSCStr(grvAnlo.NTSGetCurrentDataRow!alo_lottox).ToString
              Else
                oCallParams.strParam = NTSCInt(grvAnlo.NTSGetCurrentDataRow!alo_lotto).ToString
              End If

              Me.Close()
              Return True
            End If
          End If
          '------------------------------------------------------------------------------
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleAnlo.Ripristina(dcAnlo.Position, dcAnlo.Filter)
        Case System.Windows.Forms.DialogResult.Cancel
          'non posso fare nulla
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          'la riga non ha subito modifiche
      End Select
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Sub SetStato0()
    pnDescrizioneArticolo.Visible = False
    pnGriglia.Visible = False
    tlbSalva.Enabled = False
    tlbRipristina.Enabled = False
    tlbCancella.Enabled = False
    tlbParametri.Enabled = False
    tlbStrumenti.Enabled = False
    tlbStampa.Enabled = False
    tlbStampaVideo.Enabled = False
  End Sub

  Public Overridable Sub SetStato1()
    GctlSetVisEnab(pnGriglia, True)
    GctlSetVisEnab(pnDescrizioneArticolo, True)

    GctlSetVisEnab(tlbSalva, False)
    GctlSetVisEnab(tlbRipristina, False)
    GctlSetVisEnab(tlbCancella, False)
    GctlSetVisEnab(tlbParametri, False)
    GctlSetVisEnab(tlbStrumenti, False)
    GctlSetVisEnab(tlbStampa, False)
    GctlSetVisEnab(tlbStampaVideo, False)
    '----------------------------------------------------------------------------------
    grAnlo.Focus()
    '----------------------------------------------------------------------------------
  End Sub

  Public Overridable Sub SettaDescrizioni()
    Try
      lbDescrizioneArticolo.Text = oApp.Tr(Me, 130421079763252512, "Articolo: '|" & oCleAnlo.strCodart & "|'")
      If oCleAnlo.strDescodart.Trim <> "" Then
        lbDescrizioneArticolo.Text += " - " & oCleAnlo.strDescodart
      End If
    Catch ex As Exception

    End Try
  End Sub
#End Region

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""

    Try

      '--------------------------------------------------------------------------------------------------------------
      strCrpe = "{ANALOTTI.codditt} = " & ConvStrRpt(DittaCorrente) & _
        " And {ANALOTTI.alo_codart} = " & ConvStrRpt(oCleAnlo.strCodart)
      '--------------------------------------------------------------------------------------------------------------
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGANLO", "Reports1", " ", 0, nDestin, "BSMGANLO.RPT", False, "Anagrafica Lotti", False)
      If nPjob Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = 1 To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class
