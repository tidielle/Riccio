Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__DESG
  Public oCleClie As CLE__CLIE
  Public dsDesg As DataSet
  Public oCallParams As CLE__CLDP
  Public dcDesg As BindingSource = New BindingSource

  Public nMaxLen As Integer

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbDd_coddest As NTSInformatica.NTSLabel
  Public WithEvents edDd_coddest As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDd_nomdest As NTSInformatica.NTSLabel
  Public WithEvents edDd_nomdest As NTSInformatica.NTSTextBoxStr
  Public WithEvents edDd_nomdest2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDd_inddest As NTSInformatica.NTSLabel
  Public WithEvents edDd_inddest As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDd_capdest As NTSInformatica.NTSLabel
  Public WithEvents edDd_capdest As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDd_locdest As NTSInformatica.NTSLabel
  Public WithEvents edDd_locdest As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDd_prodest As NTSInformatica.NTSLabel
  Public WithEvents edDd_prodest As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDd_turno As NTSInformatica.NTSLabel
  Public WithEvents edDd_turno As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDd_telef As NTSInformatica.NTSLabel
  Public WithEvents edDd_telef As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDd_codfis As NTSInformatica.NTSLabel
  Public WithEvents edDd_codfis As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDd_pariva As NTSInformatica.NTSLabel
  Public WithEvents edDd_pariva As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDd_faxtlx As NTSInformatica.NTSLabel
  Public WithEvents edDd_faxtlx As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDd_email As NTSInformatica.NTSLabel
  Public WithEvents edDd_email As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDd_usaem As NTSInformatica.NTSLabel
  Public WithEvents cbDd_usaem As NTSInformatica.NTSComboBox
  Public WithEvents lbDd_stato As NTSInformatica.NTSLabel
  Public WithEvents edDd_stato As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDd_codcomu As NTSInformatica.NTSLabel
  Public WithEvents edDd_codcomu As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDd_codfisest As NTSInformatica.NTSLabel
  Public WithEvents edDd_codfisest As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDd_statofed As NTSInformatica.NTSLabel
  Public WithEvents edDd_statofed As NTSInformatica.NTSTextBoxStr

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__DESG))
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
    Me.tlbImportaIndir = New NTSInformatica.NTSBarMenuItem
    Me.tlbApriLead = New NTSInformatica.NTSBarButtonItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarMenuItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.lbDd_coddest = New NTSInformatica.NTSLabel
    Me.edDd_coddest = New NTSInformatica.NTSTextBoxNum
    Me.lbDd_nomdest = New NTSInformatica.NTSLabel
    Me.edDd_nomdest = New NTSInformatica.NTSTextBoxStr
    Me.edDd_nomdest2 = New NTSInformatica.NTSTextBoxStr
    Me.lbDd_inddest = New NTSInformatica.NTSLabel
    Me.edDd_inddest = New NTSInformatica.NTSTextBoxStr
    Me.lbDd_capdest = New NTSInformatica.NTSLabel
    Me.edDd_capdest = New NTSInformatica.NTSTextBoxStr
    Me.lbDd_locdest = New NTSInformatica.NTSLabel
    Me.edDd_locdest = New NTSInformatica.NTSTextBoxStr
    Me.lbDd_prodest = New NTSInformatica.NTSLabel
    Me.edDd_prodest = New NTSInformatica.NTSTextBoxStr
    Me.lbDd_turno = New NTSInformatica.NTSLabel
    Me.edDd_turno = New NTSInformatica.NTSTextBoxStr
    Me.lbDd_telef = New NTSInformatica.NTSLabel
    Me.edDd_telef = New NTSInformatica.NTSTextBoxStr
    Me.lbDd_codfis = New NTSInformatica.NTSLabel
    Me.edDd_codfis = New NTSInformatica.NTSTextBoxStr
    Me.lbDd_pariva = New NTSInformatica.NTSLabel
    Me.edDd_pariva = New NTSInformatica.NTSTextBoxStr
    Me.lbDd_faxtlx = New NTSInformatica.NTSLabel
    Me.edDd_faxtlx = New NTSInformatica.NTSTextBoxStr
    Me.lbDd_email = New NTSInformatica.NTSLabel
    Me.edDd_email = New NTSInformatica.NTSTextBoxStr
    Me.lbDd_usaem = New NTSInformatica.NTSLabel
    Me.cbDd_usaem = New NTSInformatica.NTSComboBox
    Me.lbDd_stato = New NTSInformatica.NTSLabel
    Me.edDd_stato = New NTSInformatica.NTSTextBoxStr
    Me.lbDd_codcomu = New NTSInformatica.NTSLabel
    Me.edDd_codcomu = New NTSInformatica.NTSTextBoxStr
    Me.lbDd_codfisest = New NTSInformatica.NTSLabel
    Me.edDd_codfisest = New NTSInformatica.NTSTextBoxStr
    Me.lbDd_statofed = New NTSInformatica.NTSLabel
    Me.edDd_statofed = New NTSInformatica.NTSTextBoxStr
    Me.pnSx = New NTSInformatica.NTSPanel
    Me.lbXx_codcomu = New NTSInformatica.NTSLabel
    Me.lbXx_stato = New NTSInformatica.NTSLabel
    Me.edDd_coduffpa = New NTSInformatica.NTSTextBoxStr
    Me.lbDd_coduffpa = New NTSInformatica.NTSLabel
    Me.pnDx = New NTSInformatica.NTSPanel
    Me.lbDd_longitud = New NTSInformatica.NTSLabel
    Me.lbDd_latitud = New NTSInformatica.NTSLabel
    Me.edDd_longitud = New NTSInformatica.NTSTextBoxStr
    Me.edDd_latitud = New NTSInformatica.NTSTextBoxStr
    Me.tsDesg = New NTSInformatica.NTSTabControl
    Me.NtsTabPage2 = New NTSInformatica.NTSTabPage
    Me.pnTab2 = New NTSInformatica.NTSPanel
    Me.cmdEstensioni = New NTSInformatica.NTSButton
    Me.pbTab2Sx = New NTSInformatica.NTSPanel
    Me.lbXx_porto = New NTSInformatica.NTSLabel
    Me.edDd_porto = New NTSInformatica.NTSTextBoxStr
    Me.lbDd_porto = New NTSInformatica.NTSLabel
    Me.lbDd_acuradi = New NTSInformatica.NTSLabel
    Me.cbDd_acuradi = New NTSInformatica.NTSComboBox
    Me.edDd_listino = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_listinoDes = New NTSInformatica.NTSLabel
    Me.lbDd_listino = New NTSInformatica.NTSLabel
    Me.cbDd_status = New NTSInformatica.NTSComboBox
    Me.lbDd_status = New NTSInformatica.NTSLabel
    Me.lbXx_vett2 = New NTSInformatica.NTSLabel
    Me.edDd_vett2 = New NTSInformatica.NTSTextBoxNum
    Me.lbDd_vett2 = New NTSInformatica.NTSLabel
    Me.lbXx_vett = New NTSInformatica.NTSLabel
    Me.edDd_vett = New NTSInformatica.NTSTextBoxNum
    Me.lbDd_vett = New NTSInformatica.NTSLabel
    Me.lbXx_listino = New NTSInformatica.NTSLabel
    Me.lbXx_codzona = New NTSInformatica.NTSLabel
    Me.edDd_codzona = New NTSInformatica.NTSTextBoxNum
    Me.lbDd_codzona = New NTSInformatica.NTSLabel
    Me.lbXx_agente = New NTSInformatica.NTSLabel
    Me.edDd_agente = New NTSInformatica.NTSTextBoxNum
    Me.lbDd_agente = New NTSInformatica.NTSLabel
    Me.lbXx_agente2 = New NTSInformatica.NTSLabel
    Me.edDd_agente2 = New NTSInformatica.NTSTextBoxNum
    Me.lbDd_agente2 = New NTSInformatica.NTSLabel
    Me.NtsTabPage1 = New NTSInformatica.NTSTabPage
    Me.pnTab1 = New NTSInformatica.NTSPanel
    Me.NtsTabPage3 = New NTSInformatica.NTSTabPage
    Me.pnTab3 = New NTSInformatica.NTSPanel
    Me.edDd_note = New NTSInformatica.NTSMemoBox
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.lbTitle = New NTSInformatica.NTSLabel
    Me.lbDd_codlead = New NTSInformatica.NTSLabel
    Me.lbLead = New NTSInformatica.NTSLabel
    Me.cmdCreaDaLead = New NTSInformatica.NTSButton
    Me.tlbGoogleMaps = New NTSInformatica.NTSBarButtonItem
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDd_coddest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDd_nomdest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDd_nomdest2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDd_inddest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDd_capdest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDd_locdest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDd_prodest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDd_turno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDd_telef.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDd_codfis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDd_pariva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDd_faxtlx.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDd_email.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbDd_usaem.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDd_stato.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDd_codcomu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDd_codfisest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDd_statofed.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnSx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSx.SuspendLayout()
    CType(Me.edDd_coduffpa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnDx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDx.SuspendLayout()
    CType(Me.edDd_longitud.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDd_latitud.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tsDesg, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsDesg.SuspendLayout()
    Me.NtsTabPage2.SuspendLayout()
    CType(Me.pnTab2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTab2.SuspendLayout()
    CType(Me.pbTab2Sx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pbTab2Sx.SuspendLayout()
    CType(Me.edDd_porto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbDd_acuradi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDd_listino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbDd_status.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDd_vett2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDd_vett.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDd_codzona.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDd_agente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDd_agente2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage1.SuspendLayout()
    CType(Me.pnTab1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTab1.SuspendLayout()
    Me.NtsTabPage3.SuspendLayout()
    CType(Me.pnTab3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTab3.SuspendLayout()
    CType(Me.edDd_note.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbRipristina, Me.tlbZoom, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbGuida, Me.tlbEsci, Me.tlbUltimo, Me.tlbCancella, Me.tlbStrumenti, Me.tlbImportaIndir, Me.tlbImpostaStampante, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbApriLead, Me.tlbGoogleMaps})
    Me.NtsBarManager1.MaxItemId = 34
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbCancella.Id = 26
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.GlyphPath = ""
    Me.tlbZoom.Id = 4
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbPrimo
    '
    Me.tlbPrimo.Caption = "Primo"
    Me.tlbPrimo.Glyph = CType(resources.GetObject("tlbPrimo.Glyph"), System.Drawing.Image)
    Me.tlbPrimo.GlyphPath = ""
    Me.tlbPrimo.Id = 5
    Me.tlbPrimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P))
    Me.tlbPrimo.Name = "tlbPrimo"
    Me.tlbPrimo.Visible = True
    '
    'tlbPrecedente
    '
    Me.tlbPrecedente.Caption = "Precedente"
    Me.tlbPrecedente.Glyph = CType(resources.GetObject("tlbPrecedente.Glyph"), System.Drawing.Image)
    Me.tlbPrecedente.GlyphPath = ""
    Me.tlbPrecedente.Id = 6
    Me.tlbPrecedente.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
    Me.tlbPrecedente.Name = "tlbPrecedente"
    Me.tlbPrecedente.Visible = True
    '
    'tlbSuccessivo
    '
    Me.tlbSuccessivo.Caption = "Successivo"
    Me.tlbSuccessivo.Glyph = CType(resources.GetObject("tlbSuccessivo.Glyph"), System.Drawing.Image)
    Me.tlbSuccessivo.GlyphPath = ""
    Me.tlbSuccessivo.Id = 7
    Me.tlbSuccessivo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
    Me.tlbSuccessivo.Name = "tlbSuccessivo"
    Me.tlbSuccessivo.Visible = True
    '
    'tlbUltimo
    '
    Me.tlbUltimo.Caption = "Ultimo"
    Me.tlbUltimo.Glyph = CType(resources.GetObject("tlbUltimo.Glyph"), System.Drawing.Image)
    Me.tlbUltimo.GlyphPath = ""
    Me.tlbUltimo.Id = 20
    Me.tlbUltimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 27
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImportaIndir), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApriLead), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGoogleMaps), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImportaIndir
    '
    Me.tlbImportaIndir.Caption = "Importa indirizzo"
    Me.tlbImportaIndir.GlyphPath = ""
    Me.tlbImportaIndir.Id = 28
    Me.tlbImportaIndir.Name = "tlbImportaIndir"
    Me.tlbImportaIndir.NTSIsCheckBox = False
    Me.tlbImportaIndir.Visible = True
    '
    'tlbApriLead
    '
    Me.tlbApriLead.Caption = "Apri lead associato"
    Me.tlbApriLead.GlyphPath = ""
    Me.tlbApriLead.Id = 32
    Me.tlbApriLead.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
    Me.tlbApriLead.Name = "tlbApriLead"
    Me.tlbApriLead.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.GlyphPath = ""
    Me.tlbImpostaStampante.Id = 29
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.NTSIsCheckBox = False
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.GlyphPath = ""
    Me.tlbStampa.Id = 30
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa a Video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.GlyphPath = ""
    Me.tlbStampaVideo.Id = 31
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 18
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 19
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'lbDd_coddest
    '
    Me.lbDd_coddest.AutoSize = True
    Me.lbDd_coddest.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_coddest.Location = New System.Drawing.Point(6, 26)
    Me.lbDd_coddest.Name = "lbDd_coddest"
    Me.lbDd_coddest.NTSDbField = ""
    Me.lbDd_coddest.Size = New System.Drawing.Size(39, 13)
    Me.lbDd_coddest.TabIndex = 10
    Me.lbDd_coddest.Text = "Codice"
    Me.lbDd_coddest.Tooltip = ""
    Me.lbDd_coddest.UseMnemonic = False
    '
    'edDd_coddest
    '
    Me.edDd_coddest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDd_coddest.EditValue = "0"
    Me.edDd_coddest.Location = New System.Drawing.Point(105, 23)
    Me.edDd_coddest.Name = "edDd_coddest"
    Me.edDd_coddest.NTSDbField = ""
    Me.edDd_coddest.NTSFormat = "0"
    Me.edDd_coddest.NTSForzaVisZoom = False
    Me.edDd_coddest.NTSOldValue = ""
    Me.edDd_coddest.Properties.Appearance.Options.UseTextOptions = True
    Me.edDd_coddest.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDd_coddest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDd_coddest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDd_coddest.Properties.AutoHeight = False
    Me.edDd_coddest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDd_coddest.Properties.MaxLength = 65536
    Me.edDd_coddest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_coddest.Size = New System.Drawing.Size(62, 20)
    Me.edDd_coddest.TabIndex = 500
    '
    'lbDd_nomdest
    '
    Me.lbDd_nomdest.AutoSize = True
    Me.lbDd_nomdest.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_nomdest.Location = New System.Drawing.Point(3, 10)
    Me.lbDd_nomdest.Name = "lbDd_nomdest"
    Me.lbDd_nomdest.NTSDbField = ""
    Me.lbDd_nomdest.Size = New System.Drawing.Size(61, 13)
    Me.lbDd_nomdest.TabIndex = 13
    Me.lbDd_nomdest.Text = "Descrizione"
    Me.lbDd_nomdest.Tooltip = ""
    Me.lbDd_nomdest.UseMnemonic = False
    '
    'edDd_nomdest
    '
    Me.edDd_nomdest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDd_nomdest.EditValue = ""
    Me.edDd_nomdest.Location = New System.Drawing.Point(102, 7)
    Me.edDd_nomdest.Name = "edDd_nomdest"
    Me.edDd_nomdest.NTSDbField = ""
    Me.edDd_nomdest.NTSForzaVisZoom = False
    Me.edDd_nomdest.NTSOldValue = ""
    Me.edDd_nomdest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDd_nomdest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDd_nomdest.Properties.AutoHeight = False
    Me.edDd_nomdest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDd_nomdest.Properties.MaxLength = 65536
    Me.edDd_nomdest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_nomdest.Size = New System.Drawing.Size(278, 20)
    Me.edDd_nomdest.TabIndex = 503
    '
    'edDd_nomdest2
    '
    Me.edDd_nomdest2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDd_nomdest2.EditValue = ""
    Me.edDd_nomdest2.Location = New System.Drawing.Point(102, 32)
    Me.edDd_nomdest2.Name = "edDd_nomdest2"
    Me.edDd_nomdest2.NTSDbField = ""
    Me.edDd_nomdest2.NTSForzaVisZoom = False
    Me.edDd_nomdest2.NTSOldValue = ""
    Me.edDd_nomdest2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDd_nomdest2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDd_nomdest2.Properties.AutoHeight = False
    Me.edDd_nomdest2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDd_nomdest2.Properties.MaxLength = 65536
    Me.edDd_nomdest2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_nomdest2.Size = New System.Drawing.Size(278, 20)
    Me.edDd_nomdest2.TabIndex = 504
    '
    'lbDd_inddest
    '
    Me.lbDd_inddest.AutoSize = True
    Me.lbDd_inddest.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_inddest.Location = New System.Drawing.Point(3, 60)
    Me.lbDd_inddest.Name = "lbDd_inddest"
    Me.lbDd_inddest.NTSDbField = ""
    Me.lbDd_inddest.Size = New System.Drawing.Size(47, 13)
    Me.lbDd_inddest.TabIndex = 15
    Me.lbDd_inddest.Text = "Indirizzo"
    Me.lbDd_inddest.Tooltip = ""
    Me.lbDd_inddest.UseMnemonic = False
    '
    'edDd_inddest
    '
    Me.edDd_inddest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDd_inddest.EditValue = ""
    Me.edDd_inddest.Location = New System.Drawing.Point(102, 57)
    Me.edDd_inddest.Name = "edDd_inddest"
    Me.edDd_inddest.NTSDbField = ""
    Me.edDd_inddest.NTSForzaVisZoom = False
    Me.edDd_inddest.NTSOldValue = ""
    Me.edDd_inddest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDd_inddest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDd_inddest.Properties.AutoHeight = False
    Me.edDd_inddest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDd_inddest.Properties.MaxLength = 65536
    Me.edDd_inddest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_inddest.Size = New System.Drawing.Size(278, 20)
    Me.edDd_inddest.TabIndex = 505
    '
    'lbDd_capdest
    '
    Me.lbDd_capdest.AutoSize = True
    Me.lbDd_capdest.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_capdest.Location = New System.Drawing.Point(3, 135)
    Me.lbDd_capdest.Name = "lbDd_capdest"
    Me.lbDd_capdest.NTSDbField = ""
    Me.lbDd_capdest.Size = New System.Drawing.Size(26, 13)
    Me.lbDd_capdest.TabIndex = 16
    Me.lbDd_capdest.Text = "Cap"
    Me.lbDd_capdest.Tooltip = ""
    Me.lbDd_capdest.UseMnemonic = False
    '
    'edDd_capdest
    '
    Me.edDd_capdest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDd_capdest.EditValue = ""
    Me.edDd_capdest.Location = New System.Drawing.Point(102, 132)
    Me.edDd_capdest.Name = "edDd_capdest"
    Me.edDd_capdest.NTSDbField = ""
    Me.edDd_capdest.NTSForzaVisZoom = False
    Me.edDd_capdest.NTSOldValue = ""
    Me.edDd_capdest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDd_capdest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDd_capdest.Properties.AutoHeight = False
    Me.edDd_capdest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDd_capdest.Properties.MaxLength = 65536
    Me.edDd_capdest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_capdest.Size = New System.Drawing.Size(62, 20)
    Me.edDd_capdest.TabIndex = 506
    '
    'lbDd_locdest
    '
    Me.lbDd_locdest.AutoSize = True
    Me.lbDd_locdest.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_locdest.Location = New System.Drawing.Point(3, 110)
    Me.lbDd_locdest.Name = "lbDd_locdest"
    Me.lbDd_locdest.NTSDbField = ""
    Me.lbDd_locdest.Size = New System.Drawing.Size(67, 13)
    Me.lbDd_locdest.TabIndex = 17
    Me.lbDd_locdest.Text = "Città/località"
    Me.lbDd_locdest.Tooltip = ""
    Me.lbDd_locdest.UseMnemonic = False
    '
    'edDd_locdest
    '
    Me.edDd_locdest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDd_locdest.EditValue = ""
    Me.edDd_locdest.Location = New System.Drawing.Point(102, 107)
    Me.edDd_locdest.Name = "edDd_locdest"
    Me.edDd_locdest.NTSDbField = ""
    Me.edDd_locdest.NTSForzaVisZoom = False
    Me.edDd_locdest.NTSOldValue = ""
    Me.edDd_locdest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDd_locdest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDd_locdest.Properties.AutoHeight = False
    Me.edDd_locdest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDd_locdest.Properties.MaxLength = 65536
    Me.edDd_locdest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_locdest.Size = New System.Drawing.Size(278, 20)
    Me.edDd_locdest.TabIndex = 507
    '
    'lbDd_prodest
    '
    Me.lbDd_prodest.AutoSize = True
    Me.lbDd_prodest.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_prodest.Location = New System.Drawing.Point(170, 135)
    Me.lbDd_prodest.Name = "lbDd_prodest"
    Me.lbDd_prodest.NTSDbField = ""
    Me.lbDd_prodest.Size = New System.Drawing.Size(50, 13)
    Me.lbDd_prodest.TabIndex = 18
    Me.lbDd_prodest.Text = "Provincia"
    Me.lbDd_prodest.Tooltip = ""
    Me.lbDd_prodest.UseMnemonic = False
    '
    'edDd_prodest
    '
    Me.edDd_prodest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDd_prodest.EditValue = ""
    Me.edDd_prodest.Location = New System.Drawing.Point(329, 132)
    Me.edDd_prodest.Name = "edDd_prodest"
    Me.edDd_prodest.NTSDbField = ""
    Me.edDd_prodest.NTSForzaVisZoom = False
    Me.edDd_prodest.NTSOldValue = ""
    Me.edDd_prodest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDd_prodest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDd_prodest.Properties.AutoHeight = False
    Me.edDd_prodest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDd_prodest.Properties.MaxLength = 65536
    Me.edDd_prodest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_prodest.Size = New System.Drawing.Size(51, 20)
    Me.edDd_prodest.TabIndex = 508
    '
    'lbDd_turno
    '
    Me.lbDd_turno.AutoSize = True
    Me.lbDd_turno.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_turno.Location = New System.Drawing.Point(3, 100)
    Me.lbDd_turno.Name = "lbDd_turno"
    Me.lbDd_turno.NTSDbField = ""
    Me.lbDd_turno.Size = New System.Drawing.Size(89, 13)
    Me.lbDd_turno.TabIndex = 19
    Me.lbDd_turno.Text = "Turno di chiusura"
    Me.lbDd_turno.Tooltip = ""
    Me.lbDd_turno.UseMnemonic = False
    '
    'edDd_turno
    '
    Me.edDd_turno.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDd_turno.EditValue = ""
    Me.edDd_turno.Location = New System.Drawing.Point(143, 97)
    Me.edDd_turno.Name = "edDd_turno"
    Me.edDd_turno.NTSDbField = ""
    Me.edDd_turno.NTSForzaVisZoom = False
    Me.edDd_turno.NTSOldValue = ""
    Me.edDd_turno.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDd_turno.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDd_turno.Properties.AutoHeight = False
    Me.edDd_turno.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDd_turno.Properties.MaxLength = 65536
    Me.edDd_turno.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_turno.Size = New System.Drawing.Size(125, 20)
    Me.edDd_turno.TabIndex = 509
    '
    'lbDd_telef
    '
    Me.lbDd_telef.AutoSize = True
    Me.lbDd_telef.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_telef.Location = New System.Drawing.Point(3, 12)
    Me.lbDd_telef.Name = "lbDd_telef"
    Me.lbDd_telef.NTSDbField = ""
    Me.lbDd_telef.Size = New System.Drawing.Size(49, 13)
    Me.lbDd_telef.TabIndex = 20
    Me.lbDd_telef.Text = "Telefono"
    Me.lbDd_telef.Tooltip = ""
    Me.lbDd_telef.UseMnemonic = False
    '
    'edDd_telef
    '
    Me.edDd_telef.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDd_telef.EditValue = ""
    Me.edDd_telef.Location = New System.Drawing.Point(143, 9)
    Me.edDd_telef.Name = "edDd_telef"
    Me.edDd_telef.NTSDbField = ""
    Me.edDd_telef.NTSForzaVisZoom = False
    Me.edDd_telef.NTSOldValue = ""
    Me.edDd_telef.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDd_telef.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDd_telef.Properties.AutoHeight = False
    Me.edDd_telef.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDd_telef.Properties.MaxLength = 65536
    Me.edDd_telef.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_telef.Size = New System.Drawing.Size(125, 20)
    Me.edDd_telef.TabIndex = 510
    '
    'lbDd_codfis
    '
    Me.lbDd_codfis.AutoSize = True
    Me.lbDd_codfis.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_codfis.Location = New System.Drawing.Point(3, 122)
    Me.lbDd_codfis.Name = "lbDd_codfis"
    Me.lbDd_codfis.NTSDbField = ""
    Me.lbDd_codfis.Size = New System.Drawing.Size(63, 13)
    Me.lbDd_codfis.TabIndex = 21
    Me.lbDd_codfis.Text = "Cod. fiscale"
    Me.lbDd_codfis.Tooltip = ""
    Me.lbDd_codfis.UseMnemonic = False
    Me.lbDd_codfis.Visible = False
    '
    'edDd_codfis
    '
    Me.edDd_codfis.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDd_codfis.EditValue = ""
    Me.edDd_codfis.Location = New System.Drawing.Point(143, 119)
    Me.edDd_codfis.Name = "edDd_codfis"
    Me.edDd_codfis.NTSDbField = ""
    Me.edDd_codfis.NTSForzaVisZoom = False
    Me.edDd_codfis.NTSOldValue = ""
    Me.edDd_codfis.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDd_codfis.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDd_codfis.Properties.AutoHeight = False
    Me.edDd_codfis.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDd_codfis.Properties.MaxLength = 65536
    Me.edDd_codfis.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_codfis.Size = New System.Drawing.Size(125, 20)
    Me.edDd_codfis.TabIndex = 511
    Me.edDd_codfis.Visible = False
    '
    'lbDd_pariva
    '
    Me.lbDd_pariva.AutoSize = True
    Me.lbDd_pariva.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_pariva.Location = New System.Drawing.Point(3, 144)
    Me.lbDd_pariva.Name = "lbDd_pariva"
    Me.lbDd_pariva.NTSDbField = ""
    Me.lbDd_pariva.Size = New System.Drawing.Size(59, 13)
    Me.lbDd_pariva.TabIndex = 22
    Me.lbDd_pariva.Text = "Partita IVA"
    Me.lbDd_pariva.Tooltip = ""
    Me.lbDd_pariva.UseMnemonic = False
    Me.lbDd_pariva.Visible = False
    '
    'edDd_pariva
    '
    Me.edDd_pariva.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDd_pariva.EditValue = ""
    Me.edDd_pariva.Location = New System.Drawing.Point(143, 141)
    Me.edDd_pariva.Name = "edDd_pariva"
    Me.edDd_pariva.NTSDbField = ""
    Me.edDd_pariva.NTSForzaVisZoom = False
    Me.edDd_pariva.NTSOldValue = ""
    Me.edDd_pariva.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDd_pariva.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDd_pariva.Properties.AutoHeight = False
    Me.edDd_pariva.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDd_pariva.Properties.MaxLength = 65536
    Me.edDd_pariva.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_pariva.Size = New System.Drawing.Size(125, 20)
    Me.edDd_pariva.TabIndex = 512
    Me.edDd_pariva.Visible = False
    '
    'lbDd_faxtlx
    '
    Me.lbDd_faxtlx.AutoSize = True
    Me.lbDd_faxtlx.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_faxtlx.Location = New System.Drawing.Point(3, 34)
    Me.lbDd_faxtlx.Name = "lbDd_faxtlx"
    Me.lbDd_faxtlx.NTSDbField = ""
    Me.lbDd_faxtlx.Size = New System.Drawing.Size(25, 13)
    Me.lbDd_faxtlx.TabIndex = 23
    Me.lbDd_faxtlx.Text = "Fax"
    Me.lbDd_faxtlx.Tooltip = ""
    Me.lbDd_faxtlx.UseMnemonic = False
    '
    'edDd_faxtlx
    '
    Me.edDd_faxtlx.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDd_faxtlx.EditValue = ""
    Me.edDd_faxtlx.Location = New System.Drawing.Point(143, 31)
    Me.edDd_faxtlx.Name = "edDd_faxtlx"
    Me.edDd_faxtlx.NTSDbField = ""
    Me.edDd_faxtlx.NTSForzaVisZoom = False
    Me.edDd_faxtlx.NTSOldValue = ""
    Me.edDd_faxtlx.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDd_faxtlx.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDd_faxtlx.Properties.AutoHeight = False
    Me.edDd_faxtlx.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDd_faxtlx.Properties.MaxLength = 65536
    Me.edDd_faxtlx.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_faxtlx.Size = New System.Drawing.Size(125, 20)
    Me.edDd_faxtlx.TabIndex = 513
    '
    'lbDd_email
    '
    Me.lbDd_email.AutoSize = True
    Me.lbDd_email.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_email.Location = New System.Drawing.Point(3, 56)
    Me.lbDd_email.Name = "lbDd_email"
    Me.lbDd_email.NTSDbField = ""
    Me.lbDd_email.Size = New System.Drawing.Size(35, 13)
    Me.lbDd_email.TabIndex = 24
    Me.lbDd_email.Text = "E-mail"
    Me.lbDd_email.Tooltip = ""
    Me.lbDd_email.UseMnemonic = False
    '
    'edDd_email
    '
    Me.edDd_email.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDd_email.EditValue = ""
    Me.edDd_email.Location = New System.Drawing.Point(143, 53)
    Me.edDd_email.Name = "edDd_email"
    Me.edDd_email.NTSDbField = ""
    Me.edDd_email.NTSForzaVisZoom = False
    Me.edDd_email.NTSOldValue = ""
    Me.edDd_email.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDd_email.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDd_email.Properties.AutoHeight = False
    Me.edDd_email.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDd_email.Properties.MaxLength = 65536
    Me.edDd_email.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_email.Size = New System.Drawing.Size(125, 20)
    Me.edDd_email.TabIndex = 514
    '
    'lbDd_usaem
    '
    Me.lbDd_usaem.AutoSize = True
    Me.lbDd_usaem.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_usaem.Location = New System.Drawing.Point(3, 78)
    Me.lbDd_usaem.Name = "lbDd_usaem"
    Me.lbDd_usaem.NTSDbField = ""
    Me.lbDd_usaem.Size = New System.Drawing.Size(134, 13)
    Me.lbDd_usaem.TabIndex = 25
    Me.lbDd_usaem.Text = "Modalità di corrispondenza"
    Me.lbDd_usaem.Tooltip = ""
    Me.lbDd_usaem.UseMnemonic = False
    '
    'cbDd_usaem
    '
    Me.cbDd_usaem.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbDd_usaem.DataSource = Nothing
    Me.cbDd_usaem.DisplayMember = ""
    Me.cbDd_usaem.Location = New System.Drawing.Point(143, 75)
    Me.cbDd_usaem.Name = "cbDd_usaem"
    Me.cbDd_usaem.NTSDbField = ""
    Me.cbDd_usaem.Properties.AutoHeight = False
    Me.cbDd_usaem.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbDd_usaem.Properties.DropDownRows = 30
    Me.cbDd_usaem.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbDd_usaem.SelectedValue = ""
    Me.cbDd_usaem.Size = New System.Drawing.Size(125, 20)
    Me.cbDd_usaem.TabIndex = 515
    Me.cbDd_usaem.ValueMember = ""
    '
    'lbDd_stato
    '
    Me.lbDd_stato.AutoSize = True
    Me.lbDd_stato.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_stato.Location = New System.Drawing.Point(3, 159)
    Me.lbDd_stato.Name = "lbDd_stato"
    Me.lbDd_stato.NTSDbField = ""
    Me.lbDd_stato.Size = New System.Drawing.Size(33, 13)
    Me.lbDd_stato.TabIndex = 26
    Me.lbDd_stato.Text = "Stato"
    Me.lbDd_stato.Tooltip = ""
    Me.lbDd_stato.UseMnemonic = False
    '
    'edDd_stato
    '
    Me.edDd_stato.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDd_stato.EditValue = ""
    Me.edDd_stato.Location = New System.Drawing.Point(102, 157)
    Me.edDd_stato.Name = "edDd_stato"
    Me.edDd_stato.NTSDbField = ""
    Me.edDd_stato.NTSForzaVisZoom = False
    Me.edDd_stato.NTSOldValue = ""
    Me.edDd_stato.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDd_stato.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDd_stato.Properties.AutoHeight = False
    Me.edDd_stato.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDd_stato.Properties.MaxLength = 65536
    Me.edDd_stato.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_stato.Size = New System.Drawing.Size(62, 20)
    Me.edDd_stato.TabIndex = 516
    '
    'lbDd_codcomu
    '
    Me.lbDd_codcomu.AutoSize = True
    Me.lbDd_codcomu.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_codcomu.Location = New System.Drawing.Point(3, 85)
    Me.lbDd_codcomu.Name = "lbDd_codcomu"
    Me.lbDd_codcomu.NTSDbField = ""
    Me.lbDd_codcomu.Size = New System.Drawing.Size(70, 13)
    Me.lbDd_codcomu.TabIndex = 27
    Me.lbDd_codcomu.Text = "Cod. comune"
    Me.lbDd_codcomu.Tooltip = ""
    Me.lbDd_codcomu.UseMnemonic = False
    '
    'edDd_codcomu
    '
    Me.edDd_codcomu.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDd_codcomu.EditValue = ""
    Me.edDd_codcomu.Location = New System.Drawing.Point(102, 82)
    Me.edDd_codcomu.Name = "edDd_codcomu"
    Me.edDd_codcomu.NTSDbField = ""
    Me.edDd_codcomu.NTSForzaVisZoom = False
    Me.edDd_codcomu.NTSOldValue = ""
    Me.edDd_codcomu.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDd_codcomu.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDd_codcomu.Properties.AutoHeight = False
    Me.edDd_codcomu.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDd_codcomu.Properties.MaxLength = 65536
    Me.edDd_codcomu.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_codcomu.Size = New System.Drawing.Size(62, 20)
    Me.edDd_codcomu.TabIndex = 517
    '
    'lbDd_codfisest
    '
    Me.lbDd_codfisest.AutoSize = True
    Me.lbDd_codfisest.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_codfisest.Location = New System.Drawing.Point(3, 166)
    Me.lbDd_codfisest.Name = "lbDd_codfisest"
    Me.lbDd_codfisest.NTSDbField = ""
    Me.lbDd_codfisest.Size = New System.Drawing.Size(97, 13)
    Me.lbDd_codfisest.TabIndex = 28
    Me.lbDd_codfisest.Text = "Cod. fiscale estero"
    Me.lbDd_codfisest.Tooltip = ""
    Me.lbDd_codfisest.UseMnemonic = False
    Me.lbDd_codfisest.Visible = False
    '
    'edDd_codfisest
    '
    Me.edDd_codfisest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDd_codfisest.EditValue = ""
    Me.edDd_codfisest.Location = New System.Drawing.Point(143, 163)
    Me.edDd_codfisest.Name = "edDd_codfisest"
    Me.edDd_codfisest.NTSDbField = ""
    Me.edDd_codfisest.NTSForzaVisZoom = False
    Me.edDd_codfisest.NTSOldValue = ""
    Me.edDd_codfisest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDd_codfisest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDd_codfisest.Properties.AutoHeight = False
    Me.edDd_codfisest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDd_codfisest.Properties.MaxLength = 65536
    Me.edDd_codfisest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_codfisest.Size = New System.Drawing.Size(125, 20)
    Me.edDd_codfisest.TabIndex = 518
    Me.edDd_codfisest.Visible = False
    '
    'lbDd_statofed
    '
    Me.lbDd_statofed.AutoSize = True
    Me.lbDd_statofed.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_statofed.Location = New System.Drawing.Point(3, 187)
    Me.lbDd_statofed.Name = "lbDd_statofed"
    Me.lbDd_statofed.NTSDbField = ""
    Me.lbDd_statofed.Size = New System.Drawing.Size(93, 13)
    Me.lbDd_statofed.TabIndex = 29
    Me.lbDd_statofed.Text = "Stato fed./contea"
    Me.lbDd_statofed.Tooltip = ""
    Me.lbDd_statofed.UseMnemonic = False
    '
    'edDd_statofed
    '
    Me.edDd_statofed.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDd_statofed.EditValue = ""
    Me.edDd_statofed.Location = New System.Drawing.Point(102, 184)
    Me.edDd_statofed.Name = "edDd_statofed"
    Me.edDd_statofed.NTSDbField = ""
    Me.edDd_statofed.NTSForzaVisZoom = False
    Me.edDd_statofed.NTSOldValue = ""
    Me.edDd_statofed.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDd_statofed.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDd_statofed.Properties.AutoHeight = False
    Me.edDd_statofed.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDd_statofed.Properties.MaxLength = 65536
    Me.edDd_statofed.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_statofed.Size = New System.Drawing.Size(278, 20)
    Me.edDd_statofed.TabIndex = 519
    '
    'pnSx
    '
    Me.pnSx.AllowDrop = True
    Me.pnSx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSx.Appearance.Options.UseBackColor = True
    Me.pnSx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSx.Controls.Add(Me.lbXx_codcomu)
    Me.pnSx.Controls.Add(Me.lbXx_stato)
    Me.pnSx.Controls.Add(Me.edDd_coduffpa)
    Me.pnSx.Controls.Add(Me.lbDd_coduffpa)
    Me.pnSx.Controls.Add(Me.edDd_nomdest)
    Me.pnSx.Controls.Add(Me.lbDd_nomdest)
    Me.pnSx.Controls.Add(Me.edDd_prodest)
    Me.pnSx.Controls.Add(Me.lbDd_prodest)
    Me.pnSx.Controls.Add(Me.edDd_statofed)
    Me.pnSx.Controls.Add(Me.edDd_locdest)
    Me.pnSx.Controls.Add(Me.lbDd_statofed)
    Me.pnSx.Controls.Add(Me.lbDd_locdest)
    Me.pnSx.Controls.Add(Me.lbDd_capdest)
    Me.pnSx.Controls.Add(Me.edDd_codcomu)
    Me.pnSx.Controls.Add(Me.lbDd_codcomu)
    Me.pnSx.Controls.Add(Me.edDd_capdest)
    Me.pnSx.Controls.Add(Me.lbDd_stato)
    Me.pnSx.Controls.Add(Me.edDd_stato)
    Me.pnSx.Controls.Add(Me.edDd_nomdest2)
    Me.pnSx.Controls.Add(Me.edDd_inddest)
    Me.pnSx.Controls.Add(Me.lbDd_inddest)
    Me.pnSx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnSx.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnSx.Location = New System.Drawing.Point(0, 0)
    Me.pnSx.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnSx.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnSx.Name = "pnSx"
    Me.pnSx.NTSActiveTrasparency = True
    Me.pnSx.Size = New System.Drawing.Size(400, 232)
    Me.pnSx.TabIndex = 522
    Me.pnSx.Text = "NtsPanel1"
    '
    'lbXx_codcomu
    '
    Me.lbXx_codcomu.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codcomu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codcomu.Location = New System.Drawing.Point(170, 82)
    Me.lbXx_codcomu.Name = "lbXx_codcomu"
    Me.lbXx_codcomu.NTSDbField = ""
    Me.lbXx_codcomu.Size = New System.Drawing.Size(210, 20)
    Me.lbXx_codcomu.TabIndex = 582
    Me.lbXx_codcomu.Text = "xx_codcomu"
    Me.lbXx_codcomu.Tooltip = ""
    Me.lbXx_codcomu.UseMnemonic = False
    '
    'lbXx_stato
    '
    Me.lbXx_stato.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_stato.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_stato.Location = New System.Drawing.Point(171, 157)
    Me.lbXx_stato.Name = "lbXx_stato"
    Me.lbXx_stato.NTSDbField = ""
    Me.lbXx_stato.Size = New System.Drawing.Size(209, 20)
    Me.lbXx_stato.TabIndex = 581
    Me.lbXx_stato.Text = "xx_stato"
    Me.lbXx_stato.Tooltip = ""
    Me.lbXx_stato.UseMnemonic = False
    '
    'edDd_coduffpa
    '
    Me.edDd_coduffpa.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edDd_coduffpa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDd_coduffpa.EditValue = ""
    Me.edDd_coduffpa.Location = New System.Drawing.Point(102, 210)
    Me.edDd_coduffpa.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edDd_coduffpa.Name = "edDd_coduffpa"
    Me.edDd_coduffpa.NTSDbField = ""
    Me.edDd_coduffpa.NTSForzaVisZoom = False
    Me.edDd_coduffpa.NTSOldValue = ""
    Me.edDd_coduffpa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDd_coduffpa.Properties.Appearance.Options.UseBackColor = True
    Me.edDd_coduffpa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDd_coduffpa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDd_coduffpa.Properties.AutoHeight = False
    Me.edDd_coduffpa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDd_coduffpa.Properties.MaxLength = 65536
    Me.edDd_coduffpa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_coduffpa.Size = New System.Drawing.Size(278, 20)
    Me.edDd_coduffpa.TabIndex = 686
    '
    'lbDd_coduffpa
    '
    Me.lbDd_coduffpa.AutoSize = True
    Me.lbDd_coduffpa.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_coduffpa.Location = New System.Drawing.Point(3, 213)
    Me.lbDd_coduffpa.Name = "lbDd_coduffpa"
    Me.lbDd_coduffpa.NTSDbField = ""
    Me.lbDd_coduffpa.Size = New System.Drawing.Size(87, 13)
    Me.lbDd_coduffpa.TabIndex = 685
    Me.lbDd_coduffpa.Text = "Codice ufficio PA"
    Me.lbDd_coduffpa.Tooltip = ""
    Me.lbDd_coduffpa.UseMnemonic = False
    '
    'pnDx
    '
    Me.pnDx.AllowDrop = True
    Me.pnDx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnDx.Appearance.Options.UseBackColor = True
    Me.pnDx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnDx.Controls.Add(Me.lbDd_longitud)
    Me.pnDx.Controls.Add(Me.lbDd_latitud)
    Me.pnDx.Controls.Add(Me.edDd_longitud)
    Me.pnDx.Controls.Add(Me.edDd_latitud)
    Me.pnDx.Controls.Add(Me.edDd_turno)
    Me.pnDx.Controls.Add(Me.lbDd_turno)
    Me.pnDx.Controls.Add(Me.lbDd_pariva)
    Me.pnDx.Controls.Add(Me.lbDd_codfisest)
    Me.pnDx.Controls.Add(Me.cbDd_usaem)
    Me.pnDx.Controls.Add(Me.edDd_codfisest)
    Me.pnDx.Controls.Add(Me.lbDd_usaem)
    Me.pnDx.Controls.Add(Me.lbDd_email)
    Me.pnDx.Controls.Add(Me.lbDd_faxtlx)
    Me.pnDx.Controls.Add(Me.edDd_email)
    Me.pnDx.Controls.Add(Me.lbDd_telef)
    Me.pnDx.Controls.Add(Me.edDd_faxtlx)
    Me.pnDx.Controls.Add(Me.edDd_pariva)
    Me.pnDx.Controls.Add(Me.edDd_telef)
    Me.pnDx.Controls.Add(Me.edDd_codfis)
    Me.pnDx.Controls.Add(Me.lbDd_codfis)
    Me.pnDx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnDx.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnDx.Location = New System.Drawing.Point(400, 0)
    Me.pnDx.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnDx.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnDx.Name = "pnDx"
    Me.pnDx.NTSActiveTrasparency = True
    Me.pnDx.Size = New System.Drawing.Size(280, 232)
    Me.pnDx.TabIndex = 523
    Me.pnDx.Text = "NtsPanel1"
    '
    'lbDd_longitud
    '
    Me.lbDd_longitud.AutoSize = True
    Me.lbDd_longitud.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_longitud.Location = New System.Drawing.Point(3, 210)
    Me.lbDd_longitud.Name = "lbDd_longitud"
    Me.lbDd_longitud.NTSDbField = ""
    Me.lbDd_longitud.Size = New System.Drawing.Size(62, 13)
    Me.lbDd_longitud.TabIndex = 560
    Me.lbDd_longitud.Text = "Longitudine"
    Me.lbDd_longitud.Tooltip = ""
    Me.lbDd_longitud.UseMnemonic = False
    '
    'lbDd_latitud
    '
    Me.lbDd_latitud.AutoSize = True
    Me.lbDd_latitud.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_latitud.Location = New System.Drawing.Point(3, 188)
    Me.lbDd_latitud.Name = "lbDd_latitud"
    Me.lbDd_latitud.NTSDbField = ""
    Me.lbDd_latitud.Size = New System.Drawing.Size(54, 13)
    Me.lbDd_latitud.TabIndex = 559
    Me.lbDd_latitud.Text = "Latitudine"
    Me.lbDd_latitud.Tooltip = ""
    Me.lbDd_latitud.UseMnemonic = False
    '
    'edDd_longitud
    '
    Me.edDd_longitud.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDd_longitud.EditValue = ""
    Me.edDd_longitud.Location = New System.Drawing.Point(143, 207)
    Me.edDd_longitud.Name = "edDd_longitud"
    Me.edDd_longitud.NTSDbField = ""
    Me.edDd_longitud.NTSForzaVisZoom = False
    Me.edDd_longitud.NTSOldValue = ""
    Me.edDd_longitud.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDd_longitud.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDd_longitud.Properties.AutoHeight = False
    Me.edDd_longitud.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDd_longitud.Properties.MaxLength = 65536
    Me.edDd_longitud.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_longitud.Size = New System.Drawing.Size(125, 20)
    Me.edDd_longitud.TabIndex = 558
    '
    'edDd_latitud
    '
    Me.edDd_latitud.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDd_latitud.EditValue = ""
    Me.edDd_latitud.Location = New System.Drawing.Point(143, 185)
    Me.edDd_latitud.Name = "edDd_latitud"
    Me.edDd_latitud.NTSDbField = ""
    Me.edDd_latitud.NTSForzaVisZoom = False
    Me.edDd_latitud.NTSOldValue = ""
    Me.edDd_latitud.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDd_latitud.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDd_latitud.Properties.AutoHeight = False
    Me.edDd_latitud.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDd_latitud.Properties.MaxLength = 65536
    Me.edDd_latitud.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_latitud.Size = New System.Drawing.Size(125, 20)
    Me.edDd_latitud.TabIndex = 557
    '
    'tsDesg
    '
    Me.tsDesg.Location = New System.Drawing.Point(0, 79)
    Me.tsDesg.Name = "tsDesg"
    Me.tsDesg.SelectedTabPage = Me.NtsTabPage2
    Me.tsDesg.Size = New System.Drawing.Size(689, 262)
    Me.tsDesg.TabIndex = 524
    Me.tsDesg.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.NtsTabPage1, Me.NtsTabPage2, Me.NtsTabPage3})
    Me.tsDesg.Text = "NtsTabControl1"
    '
    'NtsTabPage2
    '
    Me.NtsTabPage2.AllowDrop = True
    Me.NtsTabPage2.Controls.Add(Me.pnTab2)
    Me.NtsTabPage2.Enable = True
    Me.NtsTabPage2.Name = "NtsTabPage2"
    Me.NtsTabPage2.Size = New System.Drawing.Size(680, 232)
    Me.NtsTabPage2.Text = "&2 - Altri dati"
    '
    'pnTab2
    '
    Me.pnTab2.AllowDrop = True
    Me.pnTab2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTab2.Appearance.Options.UseBackColor = True
    Me.pnTab2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTab2.Controls.Add(Me.cmdEstensioni)
    Me.pnTab2.Controls.Add(Me.pbTab2Sx)
    Me.pnTab2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTab2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTab2.Location = New System.Drawing.Point(0, 0)
    Me.pnTab2.Name = "pnTab2"
    Me.pnTab2.NTSActiveTrasparency = True
    Me.pnTab2.Size = New System.Drawing.Size(680, 232)
    Me.pnTab2.TabIndex = 1
    Me.pnTab2.Text = "NtsPanel1"
    '
    'cmdEstensioni
    '
    Me.cmdEstensioni.ImagePath = ""
    Me.cmdEstensioni.ImageText = ""
    Me.cmdEstensioni.Location = New System.Drawing.Point(543, 7)
    Me.cmdEstensioni.Name = "cmdEstensioni"
    Me.cmdEstensioni.NTSContextMenu = Nothing
    Me.cmdEstensioni.Size = New System.Drawing.Size(125, 26)
    Me.cmdEstensioni.TabIndex = 596
    Me.cmdEstensioni.Text = "Estensioni anagr."
    '
    'pbTab2Sx
    '
    Me.pbTab2Sx.AllowDrop = True
    Me.pbTab2Sx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pbTab2Sx.Appearance.Options.UseBackColor = True
    Me.pbTab2Sx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pbTab2Sx.Controls.Add(Me.lbXx_porto)
    Me.pbTab2Sx.Controls.Add(Me.edDd_porto)
    Me.pbTab2Sx.Controls.Add(Me.lbDd_porto)
    Me.pbTab2Sx.Controls.Add(Me.lbDd_acuradi)
    Me.pbTab2Sx.Controls.Add(Me.cbDd_acuradi)
    Me.pbTab2Sx.Controls.Add(Me.edDd_listino)
    Me.pbTab2Sx.Controls.Add(Me.lbXx_listinoDes)
    Me.pbTab2Sx.Controls.Add(Me.lbDd_listino)
    Me.pbTab2Sx.Controls.Add(Me.cbDd_status)
    Me.pbTab2Sx.Controls.Add(Me.lbDd_status)
    Me.pbTab2Sx.Controls.Add(Me.lbXx_vett2)
    Me.pbTab2Sx.Controls.Add(Me.edDd_vett2)
    Me.pbTab2Sx.Controls.Add(Me.lbDd_vett2)
    Me.pbTab2Sx.Controls.Add(Me.lbXx_vett)
    Me.pbTab2Sx.Controls.Add(Me.edDd_vett)
    Me.pbTab2Sx.Controls.Add(Me.lbDd_vett)
    Me.pbTab2Sx.Controls.Add(Me.lbXx_listino)
    Me.pbTab2Sx.Controls.Add(Me.lbXx_codzona)
    Me.pbTab2Sx.Controls.Add(Me.edDd_codzona)
    Me.pbTab2Sx.Controls.Add(Me.lbDd_codzona)
    Me.pbTab2Sx.Controls.Add(Me.lbXx_agente)
    Me.pbTab2Sx.Controls.Add(Me.edDd_agente)
    Me.pbTab2Sx.Controls.Add(Me.lbDd_agente)
    Me.pbTab2Sx.Controls.Add(Me.lbXx_agente2)
    Me.pbTab2Sx.Controls.Add(Me.edDd_agente2)
    Me.pbTab2Sx.Controls.Add(Me.lbDd_agente2)
    Me.pbTab2Sx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pbTab2Sx.Dock = System.Windows.Forms.DockStyle.Left
    Me.pbTab2Sx.Location = New System.Drawing.Point(0, 0)
    Me.pbTab2Sx.Name = "pbTab2Sx"
    Me.pbTab2Sx.NTSActiveTrasparency = True
    Me.pbTab2Sx.Size = New System.Drawing.Size(386, 232)
    Me.pbTab2Sx.TabIndex = 0
    Me.pbTab2Sx.Text = "NtsPanel1"
    '
    'lbXx_porto
    '
    Me.lbXx_porto.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_porto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_porto.Location = New System.Drawing.Point(170, 51)
    Me.lbXx_porto.Name = "lbXx_porto"
    Me.lbXx_porto.NTSDbField = ""
    Me.lbXx_porto.Size = New System.Drawing.Size(204, 20)
    Me.lbXx_porto.TabIndex = 691
    Me.lbXx_porto.Text = "lbXx_porto"
    Me.lbXx_porto.Tooltip = ""
    Me.lbXx_porto.UseMnemonic = False
    '
    'edDd_porto
    '
    Me.edDd_porto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDd_porto.EditValue = ""
    Me.edDd_porto.Location = New System.Drawing.Point(102, 51)
    Me.edDd_porto.Name = "edDd_porto"
    Me.edDd_porto.NTSDbField = ""
    Me.edDd_porto.NTSForzaVisZoom = False
    Me.edDd_porto.NTSOldValue = ""
    Me.edDd_porto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDd_porto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDd_porto.Properties.AutoHeight = False
    Me.edDd_porto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDd_porto.Properties.MaxLength = 65536
    Me.edDd_porto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_porto.Size = New System.Drawing.Size(62, 20)
    Me.edDd_porto.TabIndex = 690
    '
    'lbDd_porto
    '
    Me.lbDd_porto.AutoSize = True
    Me.lbDd_porto.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_porto.Location = New System.Drawing.Point(9, 54)
    Me.lbDd_porto.Name = "lbDd_porto"
    Me.lbDd_porto.NTSDbField = ""
    Me.lbDd_porto.Size = New System.Drawing.Size(33, 13)
    Me.lbDd_porto.TabIndex = 689
    Me.lbDd_porto.Text = "Porto"
    Me.lbDd_porto.Tooltip = ""
    Me.lbDd_porto.UseMnemonic = False
    '
    'lbDd_acuradi
    '
    Me.lbDd_acuradi.AutoSize = True
    Me.lbDd_acuradi.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_acuradi.Location = New System.Drawing.Point(9, 77)
    Me.lbDd_acuradi.Name = "lbDd_acuradi"
    Me.lbDd_acuradi.NTSDbField = ""
    Me.lbDd_acuradi.Size = New System.Drawing.Size(71, 13)
    Me.lbDd_acuradi.TabIndex = 688
    Me.lbDd_acuradi.Text = "Trasp. a cura"
    Me.lbDd_acuradi.Tooltip = ""
    Me.lbDd_acuradi.UseMnemonic = False
    '
    'cbDd_acuradi
    '
    Me.cbDd_acuradi.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbDd_acuradi.DataSource = Nothing
    Me.cbDd_acuradi.DisplayMember = ""
    Me.cbDd_acuradi.Location = New System.Drawing.Point(102, 74)
    Me.cbDd_acuradi.Name = "cbDd_acuradi"
    Me.cbDd_acuradi.NTSDbField = ""
    Me.cbDd_acuradi.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbDd_acuradi.Properties.Appearance.Options.UseBackColor = True
    Me.cbDd_acuradi.Properties.AutoHeight = False
    Me.cbDd_acuradi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbDd_acuradi.Properties.DropDownRows = 30
    Me.cbDd_acuradi.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbDd_acuradi.SelectedValue = ""
    Me.cbDd_acuradi.Size = New System.Drawing.Size(272, 20)
    Me.cbDd_acuradi.TabIndex = 687
    Me.cbDd_acuradi.ValueMember = ""
    '
    'edDd_listino
    '
    Me.edDd_listino.EditValue = "0"
    Me.edDd_listino.Location = New System.Drawing.Point(102, 189)
    Me.edDd_listino.Name = "edDd_listino"
    Me.edDd_listino.NTSDbField = ""
    Me.edDd_listino.NTSFormat = "0"
    Me.edDd_listino.NTSForzaVisZoom = False
    Me.edDd_listino.NTSOldValue = ""
    Me.edDd_listino.Properties.Appearance.Options.UseTextOptions = True
    Me.edDd_listino.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDd_listino.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDd_listino.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDd_listino.Properties.AutoHeight = False
    Me.edDd_listino.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDd_listino.Properties.MaxLength = 65536
    Me.edDd_listino.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_listino.Size = New System.Drawing.Size(62, 20)
    Me.edDd_listino.TabIndex = 679
    '
    'lbXx_listinoDes
    '
    Me.lbXx_listinoDes.AutoSize = True
    Me.lbXx_listinoDes.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_listinoDes.Location = New System.Drawing.Point(100, 212)
    Me.lbXx_listinoDes.Name = "lbXx_listinoDes"
    Me.lbXx_listinoDes.NTSDbField = ""
    Me.lbXx_listinoDes.Size = New System.Drawing.Size(272, 13)
    Me.lbXx_listinoDes.TabIndex = 678
    Me.lbXx_listinoDes.Text = "Listino -9999 = Usa Listino indicato in anagrafica cliente"
    Me.lbXx_listinoDes.Tooltip = ""
    Me.lbXx_listinoDes.UseMnemonic = False
    '
    'lbDd_listino
    '
    Me.lbDd_listino.AutoSize = True
    Me.lbDd_listino.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_listino.Location = New System.Drawing.Point(9, 192)
    Me.lbDd_listino.Name = "lbDd_listino"
    Me.lbDd_listino.NTSDbField = ""
    Me.lbDd_listino.Size = New System.Drawing.Size(37, 13)
    Me.lbDd_listino.TabIndex = 678
    Me.lbDd_listino.Text = "Listino"
    Me.lbDd_listino.Tooltip = ""
    Me.lbDd_listino.UseMnemonic = False
    '
    'cbDd_status
    '
    Me.cbDd_status.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cbDd_status.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbDd_status.DataSource = Nothing
    Me.cbDd_status.DisplayMember = ""
    Me.cbDd_status.Location = New System.Drawing.Point(102, 166)
    Me.cbDd_status.Name = "cbDd_status"
    Me.cbDd_status.NTSDbField = ""
    Me.cbDd_status.Properties.AutoHeight = False
    Me.cbDd_status.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbDd_status.Properties.DropDownRows = 30
    Me.cbDd_status.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbDd_status.SelectedValue = ""
    Me.cbDd_status.Size = New System.Drawing.Size(180, 20)
    Me.cbDd_status.TabIndex = 677
    Me.cbDd_status.ValueMember = ""
    '
    'lbDd_status
    '
    Me.lbDd_status.AutoSize = True
    Me.lbDd_status.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_status.Location = New System.Drawing.Point(9, 169)
    Me.lbDd_status.Name = "lbDd_status"
    Me.lbDd_status.NTSDbField = ""
    Me.lbDd_status.Size = New System.Drawing.Size(38, 13)
    Me.lbDd_status.TabIndex = 676
    Me.lbDd_status.Text = "Status"
    Me.lbDd_status.Tooltip = ""
    Me.lbDd_status.UseMnemonic = False
    '
    'lbXx_vett2
    '
    Me.lbXx_vett2.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_vett2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_vett2.Location = New System.Drawing.Point(170, 120)
    Me.lbXx_vett2.Name = "lbXx_vett2"
    Me.lbXx_vett2.NTSDbField = ""
    Me.lbXx_vett2.Size = New System.Drawing.Size(204, 20)
    Me.lbXx_vett2.TabIndex = 662
    Me.lbXx_vett2.Text = "lbXx_vett2"
    Me.lbXx_vett2.Tooltip = ""
    Me.lbXx_vett2.UseMnemonic = False
    '
    'edDd_vett2
    '
    Me.edDd_vett2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDd_vett2.EditValue = "0"
    Me.edDd_vett2.Location = New System.Drawing.Point(102, 120)
    Me.edDd_vett2.Name = "edDd_vett2"
    Me.edDd_vett2.NTSDbField = ""
    Me.edDd_vett2.NTSFormat = "0"
    Me.edDd_vett2.NTSForzaVisZoom = False
    Me.edDd_vett2.NTSOldValue = ""
    Me.edDd_vett2.Properties.Appearance.Options.UseTextOptions = True
    Me.edDd_vett2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDd_vett2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDd_vett2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDd_vett2.Properties.AutoHeight = False
    Me.edDd_vett2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDd_vett2.Properties.MaxLength = 65536
    Me.edDd_vett2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_vett2.Size = New System.Drawing.Size(62, 20)
    Me.edDd_vett2.TabIndex = 663
    '
    'lbDd_vett2
    '
    Me.lbDd_vett2.AutoSize = True
    Me.lbDd_vett2.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_vett2.Location = New System.Drawing.Point(9, 123)
    Me.lbDd_vett2.Name = "lbDd_vett2"
    Me.lbDd_vett2.NTSDbField = ""
    Me.lbDd_vett2.Size = New System.Drawing.Size(52, 13)
    Me.lbDd_vett2.TabIndex = 661
    Me.lbDd_vett2.Text = "Vettore 2"
    Me.lbDd_vett2.Tooltip = ""
    Me.lbDd_vett2.UseMnemonic = False
    '
    'lbXx_vett
    '
    Me.lbXx_vett.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_vett.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_vett.Location = New System.Drawing.Point(170, 97)
    Me.lbXx_vett.Name = "lbXx_vett"
    Me.lbXx_vett.NTSDbField = ""
    Me.lbXx_vett.Size = New System.Drawing.Size(204, 20)
    Me.lbXx_vett.TabIndex = 659
    Me.lbXx_vett.Text = "lbXx_vett"
    Me.lbXx_vett.Tooltip = ""
    Me.lbXx_vett.UseMnemonic = False
    '
    'edDd_vett
    '
    Me.edDd_vett.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDd_vett.EditValue = "0"
    Me.edDd_vett.Location = New System.Drawing.Point(102, 97)
    Me.edDd_vett.Name = "edDd_vett"
    Me.edDd_vett.NTSDbField = ""
    Me.edDd_vett.NTSFormat = "0"
    Me.edDd_vett.NTSForzaVisZoom = False
    Me.edDd_vett.NTSOldValue = ""
    Me.edDd_vett.Properties.Appearance.Options.UseTextOptions = True
    Me.edDd_vett.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDd_vett.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDd_vett.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDd_vett.Properties.AutoHeight = False
    Me.edDd_vett.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDd_vett.Properties.MaxLength = 65536
    Me.edDd_vett.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_vett.Size = New System.Drawing.Size(62, 20)
    Me.edDd_vett.TabIndex = 660
    '
    'lbDd_vett
    '
    Me.lbDd_vett.AutoSize = True
    Me.lbDd_vett.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_vett.Location = New System.Drawing.Point(9, 100)
    Me.lbDd_vett.Name = "lbDd_vett"
    Me.lbDd_vett.NTSDbField = ""
    Me.lbDd_vett.Size = New System.Drawing.Size(52, 13)
    Me.lbDd_vett.TabIndex = 658
    Me.lbDd_vett.Text = "Vettore 1"
    Me.lbDd_vett.Tooltip = ""
    Me.lbDd_vett.UseMnemonic = False
    '
    'lbXx_listino
    '
    Me.lbXx_listino.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_listino.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_listino.Location = New System.Drawing.Point(170, 189)
    Me.lbXx_listino.Name = "lbXx_listino"
    Me.lbXx_listino.NTSDbField = ""
    Me.lbXx_listino.Size = New System.Drawing.Size(204, 20)
    Me.lbXx_listino.TabIndex = 636
    Me.lbXx_listino.Text = "lbXx_listino"
    Me.lbXx_listino.Tooltip = ""
    Me.lbXx_listino.UseMnemonic = False
    '
    'lbXx_codzona
    '
    Me.lbXx_codzona.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codzona.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codzona.Location = New System.Drawing.Point(170, 143)
    Me.lbXx_codzona.Name = "lbXx_codzona"
    Me.lbXx_codzona.NTSDbField = ""
    Me.lbXx_codzona.Size = New System.Drawing.Size(204, 20)
    Me.lbXx_codzona.TabIndex = 636
    Me.lbXx_codzona.Text = "lbXx_zona"
    Me.lbXx_codzona.Tooltip = ""
    Me.lbXx_codzona.UseMnemonic = False
    '
    'edDd_codzona
    '
    Me.edDd_codzona.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDd_codzona.EditValue = "0"
    Me.edDd_codzona.Location = New System.Drawing.Point(102, 143)
    Me.edDd_codzona.Name = "edDd_codzona"
    Me.edDd_codzona.NTSDbField = ""
    Me.edDd_codzona.NTSFormat = "0"
    Me.edDd_codzona.NTSForzaVisZoom = False
    Me.edDd_codzona.NTSOldValue = ""
    Me.edDd_codzona.Properties.Appearance.Options.UseTextOptions = True
    Me.edDd_codzona.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDd_codzona.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDd_codzona.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDd_codzona.Properties.AutoHeight = False
    Me.edDd_codzona.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDd_codzona.Properties.MaxLength = 65536
    Me.edDd_codzona.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_codzona.Size = New System.Drawing.Size(62, 20)
    Me.edDd_codzona.TabIndex = 637
    '
    'lbDd_codzona
    '
    Me.lbDd_codzona.AutoSize = True
    Me.lbDd_codzona.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_codzona.Location = New System.Drawing.Point(9, 146)
    Me.lbDd_codzona.Name = "lbDd_codzona"
    Me.lbDd_codzona.NTSDbField = ""
    Me.lbDd_codzona.Size = New System.Drawing.Size(31, 13)
    Me.lbDd_codzona.TabIndex = 635
    Me.lbDd_codzona.Text = "Zona"
    Me.lbDd_codzona.Tooltip = ""
    Me.lbDd_codzona.UseMnemonic = False
    '
    'lbXx_agente
    '
    Me.lbXx_agente.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_agente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_agente.Location = New System.Drawing.Point(170, 7)
    Me.lbXx_agente.Name = "lbXx_agente"
    Me.lbXx_agente.NTSDbField = ""
    Me.lbXx_agente.Size = New System.Drawing.Size(204, 20)
    Me.lbXx_agente.TabIndex = 633
    Me.lbXx_agente.Text = "lbXx_agente"
    Me.lbXx_agente.Tooltip = ""
    Me.lbXx_agente.UseMnemonic = False
    '
    'edDd_agente
    '
    Me.edDd_agente.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDd_agente.EditValue = "0"
    Me.edDd_agente.Location = New System.Drawing.Point(102, 7)
    Me.edDd_agente.Name = "edDd_agente"
    Me.edDd_agente.NTSDbField = ""
    Me.edDd_agente.NTSFormat = "0"
    Me.edDd_agente.NTSForzaVisZoom = False
    Me.edDd_agente.NTSOldValue = ""
    Me.edDd_agente.Properties.Appearance.Options.UseTextOptions = True
    Me.edDd_agente.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDd_agente.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDd_agente.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDd_agente.Properties.AutoHeight = False
    Me.edDd_agente.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDd_agente.Properties.MaxLength = 65536
    Me.edDd_agente.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_agente.Size = New System.Drawing.Size(62, 20)
    Me.edDd_agente.TabIndex = 634
    '
    'lbDd_agente
    '
    Me.lbDd_agente.AutoSize = True
    Me.lbDd_agente.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_agente.Location = New System.Drawing.Point(9, 10)
    Me.lbDd_agente.Name = "lbDd_agente"
    Me.lbDd_agente.NTSDbField = ""
    Me.lbDd_agente.Size = New System.Drawing.Size(51, 13)
    Me.lbDd_agente.TabIndex = 632
    Me.lbDd_agente.Text = "Agente 1"
    Me.lbDd_agente.Tooltip = ""
    Me.lbDd_agente.UseMnemonic = False
    '
    'lbXx_agente2
    '
    Me.lbXx_agente2.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_agente2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_agente2.Location = New System.Drawing.Point(170, 29)
    Me.lbXx_agente2.Name = "lbXx_agente2"
    Me.lbXx_agente2.NTSDbField = ""
    Me.lbXx_agente2.Size = New System.Drawing.Size(204, 20)
    Me.lbXx_agente2.TabIndex = 630
    Me.lbXx_agente2.Text = "lbXx_agente2"
    Me.lbXx_agente2.Tooltip = ""
    Me.lbXx_agente2.UseMnemonic = False
    '
    'edDd_agente2
    '
    Me.edDd_agente2.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edDd_agente2.EditValue = "0"
    Me.edDd_agente2.Location = New System.Drawing.Point(102, 29)
    Me.edDd_agente2.Name = "edDd_agente2"
    Me.edDd_agente2.NTSDbField = ""
    Me.edDd_agente2.NTSFormat = "0"
    Me.edDd_agente2.NTSForzaVisZoom = False
    Me.edDd_agente2.NTSOldValue = ""
    Me.edDd_agente2.Properties.Appearance.Options.UseTextOptions = True
    Me.edDd_agente2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDd_agente2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDd_agente2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDd_agente2.Properties.AutoHeight = False
    Me.edDd_agente2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDd_agente2.Properties.MaxLength = 65536
    Me.edDd_agente2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_agente2.Size = New System.Drawing.Size(62, 20)
    Me.edDd_agente2.TabIndex = 631
    '
    'lbDd_agente2
    '
    Me.lbDd_agente2.AutoSize = True
    Me.lbDd_agente2.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_agente2.Location = New System.Drawing.Point(9, 32)
    Me.lbDd_agente2.Name = "lbDd_agente2"
    Me.lbDd_agente2.NTSDbField = ""
    Me.lbDd_agente2.Size = New System.Drawing.Size(51, 13)
    Me.lbDd_agente2.TabIndex = 629
    Me.lbDd_agente2.Text = "Agente 2"
    Me.lbDd_agente2.Tooltip = ""
    Me.lbDd_agente2.UseMnemonic = False
    '
    'NtsTabPage1
    '
    Me.NtsTabPage1.AllowDrop = True
    Me.NtsTabPage1.Controls.Add(Me.pnTab1)
    Me.NtsTabPage1.Enable = True
    Me.NtsTabPage1.Name = "NtsTabPage1"
    Me.NtsTabPage1.Size = New System.Drawing.Size(680, 232)
    Me.NtsTabPage1.Text = "&1 - principale"
    '
    'pnTab1
    '
    Me.pnTab1.AllowDrop = True
    Me.pnTab1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTab1.Appearance.Options.UseBackColor = True
    Me.pnTab1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTab1.Controls.Add(Me.pnSx)
    Me.pnTab1.Controls.Add(Me.pnDx)
    Me.pnTab1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTab1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTab1.Location = New System.Drawing.Point(0, 0)
    Me.pnTab1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTab1.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTab1.Name = "pnTab1"
    Me.pnTab1.NTSActiveTrasparency = True
    Me.pnTab1.Size = New System.Drawing.Size(680, 232)
    Me.pnTab1.TabIndex = 0
    Me.pnTab1.Text = "NtsPanel1"
    '
    'NtsTabPage3
    '
    Me.NtsTabPage3.AllowDrop = True
    Me.NtsTabPage3.Controls.Add(Me.pnTab3)
    Me.NtsTabPage3.Enable = True
    Me.NtsTabPage3.Name = "NtsTabPage3"
    Me.NtsTabPage3.Size = New System.Drawing.Size(680, 232)
    Me.NtsTabPage3.Text = "&3 - Note"
    '
    'pnTab3
    '
    Me.pnTab3.AllowDrop = True
    Me.pnTab3.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTab3.Appearance.Options.UseBackColor = True
    Me.pnTab3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTab3.Controls.Add(Me.edDd_note)
    Me.pnTab3.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTab3.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTab3.Location = New System.Drawing.Point(0, 0)
    Me.pnTab3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTab3.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTab3.Name = "pnTab3"
    Me.pnTab3.NTSActiveTrasparency = True
    Me.pnTab3.Size = New System.Drawing.Size(680, 232)
    Me.pnTab3.TabIndex = 1
    Me.pnTab3.Text = "NtsPanel1"
    '
    'edDd_note
    '
    Me.edDd_note.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDd_note.Dock = System.Windows.Forms.DockStyle.Fill
    Me.edDd_note.Location = New System.Drawing.Point(0, 0)
    Me.edDd_note.Name = "edDd_note"
    Me.edDd_note.NTSDbField = ""
    Me.edDd_note.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDd_note.Size = New System.Drawing.Size(680, 232)
    Me.edDd_note.TabIndex = 0
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.lbTitle)
    Me.pnTop.Controls.Add(Me.lbDd_codlead)
    Me.pnTop.Controls.Add(Me.lbLead)
    Me.pnTop.Controls.Add(Me.cmdCreaDaLead)
    Me.pnTop.Controls.Add(Me.edDd_coddest)
    Me.pnTop.Controls.Add(Me.lbDd_coddest)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(689, 43)
    Me.pnTop.TabIndex = 525
    Me.pnTop.Text = "NtsPanel1"
    '
    'lbTitle
    '
    Me.lbTitle.AutoSize = True
    Me.lbTitle.BackColor = System.Drawing.Color.Transparent
    Me.lbTitle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbTitle.Location = New System.Drawing.Point(3, 3)
    Me.lbTitle.Name = "lbTitle"
    Me.lbTitle.NTSDbField = ""
    Me.lbTitle.Size = New System.Drawing.Size(77, 13)
    Me.lbTitle.TabIndex = 596
    Me.lbTitle.Text = "Altri indirizzi"
    Me.lbTitle.Tooltip = ""
    Me.lbTitle.UseMnemonic = False
    '
    'lbDd_codlead
    '
    Me.lbDd_codlead.AutoSize = True
    Me.lbDd_codlead.BackColor = System.Drawing.Color.Transparent
    Me.lbDd_codlead.Location = New System.Drawing.Point(469, 26)
    Me.lbDd_codlead.Name = "lbDd_codlead"
    Me.lbDd_codlead.NTSDbField = ""
    Me.lbDd_codlead.Size = New System.Drawing.Size(13, 13)
    Me.lbDd_codlead.TabIndex = 595
    Me.lbDd_codlead.Text = "0"
    Me.lbDd_codlead.Tooltip = ""
    Me.lbDd_codlead.UseMnemonic = False
    '
    'lbLead
    '
    Me.lbLead.AutoSize = True
    Me.lbLead.BackColor = System.Drawing.Color.Transparent
    Me.lbLead.Location = New System.Drawing.Point(406, 26)
    Me.lbLead.Name = "lbLead"
    Me.lbLead.NTSDbField = ""
    Me.lbLead.Size = New System.Drawing.Size(57, 13)
    Me.lbLead.TabIndex = 594
    Me.lbLead.Text = "Cod.Lead:"
    Me.lbLead.Tooltip = ""
    Me.lbLead.UseMnemonic = False
    '
    'cmdCreaDaLead
    '
    Me.cmdCreaDaLead.ImagePath = ""
    Me.cmdCreaDaLead.ImageText = ""
    Me.cmdCreaDaLead.Location = New System.Drawing.Point(546, 14)
    Me.cmdCreaDaLead.Name = "cmdCreaDaLead"
    Me.cmdCreaDaLead.NTSContextMenu = Nothing
    Me.cmdCreaDaLead.Size = New System.Drawing.Size(125, 26)
    Me.cmdCreaDaLead.TabIndex = 504
    Me.cmdCreaDaLead.Text = "Crea da lead"
    '
    'tlbGoogleMaps
    '
    Me.tlbGoogleMaps.Caption = "Localizza posizione"
    Me.tlbGoogleMaps.GlyphPath = ""
    Me.tlbGoogleMaps.Id = 33
    Me.tlbGoogleMaps.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G))
    Me.tlbGoogleMaps.Name = "tlbGoogleMaps"
    Me.tlbGoogleMaps.Visible = True
    '
    'FRM__DESG
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(689, 341)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.tsDesg)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__DESG"
    Me.Text = "DESTINAZIONI DIVERSE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDd_coddest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDd_nomdest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDd_nomdest2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDd_inddest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDd_capdest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDd_locdest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDd_prodest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDd_turno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDd_telef.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDd_codfis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDd_pariva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDd_faxtlx.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDd_email.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbDd_usaem.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDd_stato.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDd_codcomu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDd_codfisest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDd_statofed.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnSx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSx.ResumeLayout(False)
    Me.pnSx.PerformLayout()
    CType(Me.edDd_coduffpa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnDx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDx.ResumeLayout(False)
    Me.pnDx.PerformLayout()
    CType(Me.edDd_longitud.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDd_latitud.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tsDesg, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsDesg.ResumeLayout(False)
    Me.NtsTabPage2.ResumeLayout(False)
    CType(Me.pnTab2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTab2.ResumeLayout(False)
    CType(Me.pbTab2Sx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pbTab2Sx.ResumeLayout(False)
    Me.pbTab2Sx.PerformLayout()
    CType(Me.edDd_porto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbDd_acuradi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDd_listino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbDd_status.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDd_vett2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDd_vett.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDd_codzona.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDd_agente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDd_agente2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage1.ResumeLayout(False)
    CType(Me.pnTab1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTab1.ResumeLayout(False)
    Me.NtsTabPage3.ResumeLayout(False)
    CType(Me.pnTab3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTab3.ResumeLayout(False)
    CType(Me.edDd_note.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
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

    Return True
  End Function

  Public Overridable Sub InitEntity(ByRef cleAnag As CLE__CLIE, ByRef ds As DataSet, ByVal lCodDest As Integer)
    Dim i As Integer = 0
    Try
      oCleClie = cleAnag
      oCleClie.lCodDestNew = lCodDest
      AddHandler oCleClie.RemoteEvent, AddressOf GestisciEventiEntity

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      dsDesg = ds
      oCleClie.DesgSetDataTable(DittaCorrente, dsDesg.Tables("DESTDIV"))
      dcDesg.DataSource = dsDesg.Tables("DESTDIV")
      dsDesg.AcceptChanges()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim i As Integer = 0
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'Ã¨ una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      Dim dttTipoSend As New DataTable()
      dttTipoSend.Columns.Add("cod", GetType(String))
      dttTipoSend.Columns.Add("val", GetType(String))
      dttTipoSend.Rows.Add(New Object() {"S", "E-mail Internet"})
      dttTipoSend.Rows.Add(New Object() {"X", "Fax service Win XP/2003"})
      'dttTipoSend.Rows.Add(New Object() {"Y", "Fax service Win 2000 (locale)"}) non più supportato
      dttTipoSend.Rows.Add(New Object() {"N", "Microsoft Fax (mapi)"})
      dttTipoSend.Rows.Add(New Object() {"Z", "Zetafax MAPI"})
      dttTipoSend.Rows.Add(New Object() {"H", "HylaFAX"})
      cbDd_usaem.DataSource = dttTipoSend
      cbDd_usaem.ValueMember = "cod"
      cbDd_usaem.DisplayMember = "val"

      Dim dttStatus As New DataTable()
      dttStatus.Columns.Add("cod", GetType(String))
      dttStatus.Columns.Add("val", GetType(String))
      dttStatus.Rows.Add(New Object() {"A", "Attivo"})
      dttStatus.Rows.Add(New Object() {"I", "Inattivo"})
      dttStatus.Rows.Add(New Object() {"P", "Potenziale"})
      dttStatus.AcceptChanges()
      cbDd_status.DataSource = dttStatus
      cbDd_status.ValueMember = "cod"
      cbDd_status.DisplayMember = "val"

      Dim dttTrasporto As New DataTable()
      dttTrasporto.Columns.Add("cod", GetType(String))
      dttTrasporto.Columns.Add("val", GetType(String))
      dttTrasporto.Rows.Add(New Object() {" ", "(Nessuno)"})
      dttTrasporto.Rows.Add(New Object() {"D", "Destinatario"})
      dttTrasporto.Rows.Add(New Object() {"M", "Mittente"})
      dttTrasporto.Rows.Add(New Object() {"V", "Vettore"})
      dttTrasporto.Rows.Add(New Object() {"X", "Usa impostazione generale cliente"})
      cbDd_acuradi.DataSource = dttTrasporto
      cbDd_acuradi.ValueMember = "cod"
      cbDd_acuradi.DisplayMember = "val"

      '-------------------------------------------------
      'completo le informazioni dei controlli
      edDd_coddest.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128738066789694000, "Codice"), tabdestdiv)
      edDd_nomdest.NTSSetParam(oMenu, oApp.Tr(Me, 128738066798118000, "Descrizione"), 50, False)
      edDd_nomdest2.NTSSetParam(oMenu, oApp.Tr(Me, 128738066808258000, "Descrizione 2"), 128, True)
      edDd_inddest.NTSSetParam(oMenu, oApp.Tr(Me, 128738066817618000, "Indirizzo"), 70, True)
      edDd_capdest.NTSSetParam(oMenu, oApp.Tr(Me, 128738066827134000, "Cap"), 9, True)
      edDd_locdest.NTSSetParam(oMenu, oApp.Tr(Me, 128738066836026000, "Città"), 50, True)
      edDd_prodest.NTSSetParam(oMenu, oApp.Tr(Me, 128738066845230000, "Provincia"), 2, True)
      edDd_turno.NTSSetParam(oMenu, oApp.Tr(Me, 128738066855370000, "Turno di chiusura"), 15, True)
      edDd_telef.NTSSetParam(oMenu, oApp.Tr(Me, 128738066865822000, "Telefono"), 18, True)
      edDd_codfis.NTSSetParam(oMenu, oApp.Tr(Me, 128738066874870000, "Cod. fiscale"), 16, True)
      edDd_pariva.NTSSetParam(oMenu, oApp.Tr(Me, 128738066884074000, "Partita IVA"), 11, True)
      edDd_faxtlx.NTSSetParam(oMenu, oApp.Tr(Me, 128738066894838000, "Fax"), 18, True)
      edDd_email.NTSSetParam(oMenu, oApp.Tr(Me, 128738066903886000, "E-mail"), 100, True)
      cbDd_usaem.NTSSetParam(oApp.Tr(Me, 128738066915430000, "Modalità di corrispondenza"))
      edDd_stato.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128738066927442000, "Stato"), tabstat, True)
      edDd_codcomu.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128738066937270000, "Cod. comune"), tabcomuni, True)
      edDd_codfisest.NTSSetParam(oMenu, oApp.Tr(Me, 128738066947878000, "Cod. fiscale estero"), 25, True)
      edDd_statofed.NTSSetParam(oMenu, oApp.Tr(Me, 128738066968938000, "Stato federato/contea"), 30, True)
      edDd_agente.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128738066981574000, "Agente"), tabcage)
      edDd_agente2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128738066990778000, "Agente2 "), tabcage)
      edDd_note.NTSSetParam(oMenu, oApp.Tr(Me, 128738067000762000, "Note"), 0, True)
      edDd_codzona.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128738067010122000, "Zona geografica"), tabzone)
      edDd_vett2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128738067019950000, "Vettore 2"), tabvett)
      edDd_vett.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128738067034770000, "Vettore"), tabvett)
      cbDd_acuradi.NTSSetParam(oApp.Tr(Me, 129055173884392804, "Tipo trasporto"))
      edDd_latitud.NTSSetParam(oMenu, oApp.Tr(Me, 130420233729993949, "Latitudine"), 15, True)
      edDd_longitud.NTSSetParam(oMenu, oApp.Tr(Me, 130420234084258039, "Longitudine"), 15, True)
      cbDd_status.NTSSetParam(oApp.Tr(Me, 129965940992904949, "Status"))
      edDd_listino.NTSSetParam(oMenu, oApp.Tr(Me, 130408332566724642, "Listino"), "0", 4, -9999, 9999)
      edDd_coduffpa.NTSSetParam(oMenu, oApp.Tr(Me, 130618959966287538, "Codice ufficio PA"), 50, True)
      edDd_porto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128371005396316000, "Porto"), tabport, True)

      edDd_coddest.NTSSetParamZoom("")
      edDd_nomdest.NTSSetParamZoom("ZOOMDESTDIV")
      edDd_listino.NTSSetParamZoom("ZOOMTABLIST")
      'edDd_nomdest.NTSForzaVisZoom = True
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
      'se i controlli erano già  stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      edDd_coddest.NTSDbField = "DESTDIV.dd_coddest"
      edDd_nomdest.NTSDbField = "DESTDIV.dd_nomdest"
      edDd_nomdest2.NTSDbField = "DESTDIV.dd_nomdest2"
      edDd_inddest.NTSDbField = "DESTDIV.dd_inddest"
      edDd_capdest.NTSDbField = "DESTDIV.dd_capdest"
      edDd_locdest.NTSDbField = "DESTDIV.dd_locdest"
      edDd_prodest.NTSDbField = "DESTDIV.dd_prodest"
      edDd_turno.NTSDbField = "DESTDIV.dd_turno"
      edDd_telef.NTSDbField = "DESTDIV.dd_telef"
      edDd_codfis.NTSDbField = "DESTDIV.dd_codfis"
      edDd_pariva.NTSDbField = "DESTDIV.dd_pariva"
      edDd_faxtlx.NTSDbField = "DESTDIV.dd_faxtlx"
      edDd_email.NTSDbField = "DESTDIV.dd_email"
      cbDd_usaem.NTSDbField = "DESTDIV.dd_usaem"
      edDd_stato.NTSDbField = "DESTDIV.dd_stato"
      edDd_codcomu.NTSDbField = "DESTDIV.dd_codcomu"
      edDd_codfisest.NTSDbField = "DESTDIV.dd_codfisest"
      edDd_statofed.NTSDbField = "DESTDIV.dd_statofed"
      lbXx_codcomu.NTSDbField = "DESTDIV.xx_codcomu"
      lbXx_stato.NTSDbField = "DESTDIV.xx_stato"
      edDd_note.NTSDbField = "DESTDIV.dd_note"
      edDd_agente.NTSDbField = "DESTDIV.dd_agente"
      edDd_agente2.NTSDbField = "DESTDIV.dd_agente2"
      edDd_vett.NTSDbField = "DESTDIV.dd_vett"
      edDd_vett2.NTSDbField = "DESTDIV.dd_vett2"
      cbDd_acuradi.NTSDbField = "DESTDIV.dd_acuradi"
      edDd_codzona.NTSDbField = "DESTDIV.dd_codzona"
      lbXx_agente.NTSDbField = "DESTDIV.xx_agente"
      lbXx_agente2.NTSDbField = "DESTDIV.xx_agente2"
      lbXx_vett.NTSDbField = "DESTDIV.xx_vett"
      lbXx_vett2.NTSDbField = "DESTDIV.xx_vett2"
      lbXx_codzona.NTSDbField = "DESTDIV.xx_codzona"
      lbDd_codlead.NTSDbField = "DESTDIV.dd_codlead"
      edDd_latitud.NTSDbField = "DESTDIV.dd_latitud"
      edDd_longitud.NTSDbField = "DESTDIV.dd_longitud"
      cbDd_status.NTSDbField = "DESTDIV.xx_lestatus"
      edDd_listino.NTSDbField = "DESTDIV.dd_listino"
      lbXx_listino.NTSDbField = "DESTDIV.xx_listino"
      edDd_coduffpa.NTSDbField = "DESTDIV.dd_coduffpa"
      lbXx_porto.NTSDbField = "DESTDIV.xx_porto"
      edDd_porto.NTSDbField = "DESTDIV.dd_porto"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcDesg, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRM__DESG_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim i As Integer = 0
    Dim bEmptyTable As Boolean = False
    Try
      tsDesg.SelectedTabPageIndex = 0

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      If oCleClie.strGestAnaext.IndexOf(IIf(oCleClie.strTipoConto = "C", "D", "E").ToString) > -1 Then
        GctlSetVisEnab(cmdEstensioni, True)
      Else
        cmdEstensioni.Visible = False
      End If

      If (CBool(oMenu.ModuliDittaDitt(DittaCorrente) And CLN__STD.bsModAS) = False And _
         ((CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And CLN__STD.bsModExtCRM) = False) And _
           CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And CLN__STD.bsModSupWCR) = False)) Or _
         oCleClie.strTipoConto <> "C" Then
        lbDd_codlead.Visible = False
        lbLead.Visible = False
        cmdCreaDaLead.Visible = False
        lbDd_status.Visible = False
        cbDd_status.Visible = False
      End If

      If dsDesg.Tables("DESTDIV").Rows.Count = 0 Then
        bEmptyTable = True
        edDd_coddest.Enabled = True
        GctlSetVisEnab(cmdCreaDaLead, False)
        tlbNuovo_ItemClick(tlbNuovo, Nothing)
      Else
        bEmptyTable = False
        If oCleClie.lCodDestNew <> 0 Then tlbNuovo.Enabled = False
        edDd_coddest.Enabled = False
        cmdCreaDaLead.Enabled = False
      End If

      dcDesg.ResetBindings(False)
      dcDesg.MoveFirst()

      '--------------------------------------------
      'sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
      If Not oCallParams Is Nothing Then
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
          edDd_coddest.Enabled = True
          GctlSetVisEnab(cmdCreaDaLead, False)
          If bEmptyTable = False Then tlbNuovo_ItemClick(Me, Nothing)
        ElseIf Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) <> "" Then
          For i = 0 To dcDesg.List.Count - 1
            If NTSCInt(CType(dcDesg.Item(i), DataRowView)!dd_coddest) = NTSCInt(Microsoft.VisualBasic.Mid(oCallParams.strParam, 6)) Then
              dcDesg.Position = i
              Exit For
            End If
          Next
        End If
      End If    'If Not oCallParams Is Nothing Then

      lbTitle.Text = oCallParams.strPar1.Replace("&", "")
      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione Freindly nasconde, sempre, alcuni controlli
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = True Then
        tlbApriLead.Visible = False
        cmdCreaDaLead.Visible = False
        cmdEstensioni.Visible = False
        lbDd_acuradi.Visible = False
        cbDd_acuradi.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__DESG_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleClie.lCoddestDaGestioneZoom <> 0 Then
        For i As Integer = 0 To dcDesg.List.Count - 1
          If NTSCInt(CType(dcDesg.Item(i), DataRowView)!dd_coddest) = oCleClie.lCoddestDaGestioneZoom Then
            dcDesg.Position = i
            Exit For
          End If
        Next
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRM__DESG_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva(True, True) Then e.Cancel = True
  End Sub

  Public Overridable Sub FRM__DESG_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      '--------------------------------------------------------------------------------------------------------------
      oMenu.ResetTblInstId("TTDESTDIV", False, oCleClie.lIITtdestdiv)
      '--------------------------------------------------------------------------------------------------------------
      dcDesg.Dispose()
      dsDesg.Dispose()
      '--------------------------------------------------------------------------------------------------------------
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      '-------------------------------------------------
      'creo una nuova forma di pagamento
      If Not Salva(False, True) Then Return
      oCleClie.DesgNuovo()
      dcDesg.MoveLast()
      edDd_coddest.Enabled = True
      GctlSetVisEnab(cmdCreaDaLead, False)
      edDd_coddest.Focus()

      '-------------------------------------------------
      'imposto i valori di default come impostato nella GCTL
      Me.GctlApplicaDefaultValue()

      If oCleClie.lCodDestNew <> 0 Then
        edDd_coddest.Enabled = False
        cmdCreaDaLead.Enabled = False
      End If


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva(False, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Dim bRemovBinding As Boolean = False
    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleClie.DesgTestPreCancella(NTSCInt(dsDesg.Tables("DESTDIV").Rows(dcDesg.Position)!dd_conto), _
        NTSCInt(edDd_coddest.Text)) = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      'cancello la forma di pagamento
      Dim dlgRes As DialogResult
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127791952085312500, "Cancellare la destinazione?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes

          If dsDesg.Tables("DESTDIV").Rows.Count = 1 Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          'memorizzo la destinazione diversa cancellata: 
          'mi servirà per rimuovere il lead collegato in fase di salvataggio
          oCleClie.dttDestdivDeleted.ImportRow(CType(dcDesg.Current, DataRowView).Row)
          dcDesg.RemoveAt(dcDesg.Position)
          oCleClie.DesgSalva(True)

          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcDesg, Me)
            bRemovBinding = False
            edDd_coddest.Enabled = True
            GctlSetVisEnab(cmdCreaDaLead, False)
          Else
            edDd_coddest.Enabled = False
            cmdCreaDaLead.Enabled = False
          End If

          Return
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcDesg, Me)
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
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128275017320420000, "Ripristinare le modifiche apportate?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          If dsDesg.Tables("DESTDIV").Rows.Count = 1 And dsDesg.Tables("DESTDIV").Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          oCleClie.DesgRipristina(dcDesg.Position, dcDesg.Filter)

          If bRemovBinding Then
            tlbNuovo.Enabled = True
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcDesg, Me)
            bRemovBinding = False
            If oCleClie.lCodDestNew = 0 Then
              edDd_coddest.Enabled = True
              GctlSetVisEnab(cmdCreaDaLead, False)
            End If
          Else
            edDd_coddest.Enabled = False
            cmdCreaDaLead.Enabled = False
          End If
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcDesg, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim i As Integer = 0
    Dim strTmp As String = ""
    Dim oParam As New CLE__PATB
    Dim ds As New DataSet

    Try
      If edDd_nomdest.ContainsFocus Then
        If Not Salva(False, True) Then Return
        'creo un dataset contenente tutte le destinazioni diverse che ho in memoria
        ds.Tables.Add(dsDesg.Tables("DESTDIV").Clone)
        ds.Tables(0).TableName = "DESTDIV"
        For i = 0 To dsDesg.Tables("DESTDIV").Rows.Count - 1
          ds.Tables(0).ImportRow(dsDesg.Tables("DESTDIV").Rows(i))
        Next
        ds.Tables(0).AcceptChanges()
        NTSZOOM.strIn = edDd_nomdest.Text
        oParam.lContoCF = 1   'passo il conto cliente/fornitore solo per dire che non è zoom su anazul
        oParam.oParam = ds
        NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edDd_coddest.Text Then
          For i = 0 To dcDesg.List.Count - 1
            If CType(dcDesg.Item(i), DataRowView)!dd_coddest.ToString = NTSZOOM.strIn Then
              dcDesg.Position = i
              Exit For
            End If
          Next
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

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub

  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    '-------------------------------------------------
    'vado sul primo record
    If Not Salva(False, True) Then Return
    dcDesg.MoveFirst()
  End Sub

  Public Overridable Sub tlbPrecedente_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrecedente.ItemClick
    '-------------------------------------------------
    'vado sul record precedente
    If Not Salva(False, True) Then Return
    dcDesg.MovePrevious()
  End Sub

  Public Overridable Sub tlbSuccessivo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSuccessivo.ItemClick
    '-------------------------------------------------
    'vado sul record successivo
    If Not Salva(False, True) Then Return
    dcDesg.MoveNext()
  End Sub

  Public Overridable Sub tlbUltimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbUltimo.ItemClick
    '-------------------------------------------------
    'vado sull'ultimo record
    If Not Salva(False, True) Then Return
    dcDesg.MoveLast()
  End Sub

  Public Overridable Sub tlbApriLead_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApriLead.ItemClick
    Dim oPar As New CLE__CLDP

    Try
      If NTSCInt(lbDd_codlead.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129518250018194760, "Cliente/fornitore non collegato a nessun lead"))
        Return
      End If
      oPar.strParam = lbDd_codlead.Text.PadLeft(9, CChar("0"))
      oMenu.RunChild("NTSInformatica", "FRMCRLEAD", oApp.Tr(Me, 130420234734653779, "Gestione leads"), DittaCorrente, "", "BNCRLEAD", oPar, "", True, True)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbImportaIndir_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImportaIndir.ItemClick
    Dim oParam As New CLE__PATB
    Dim dttTmp As New DataTable
    Try
      If edDd_coddest.Enabled = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128800523729474500, "Funzione utilizzabile solo su nuove destinazioni"))
        Return
      End If

      oParam.bTipoProposto = True
      NTSZOOM.ZoomStrIn("ZOOMANAGRAF", DittaCorrente, oParam)
      oMenu.ValCodiceDb(NTSZOOM.strIn.ToString, DittaCorrente, "ANAGRA", "N", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then
        edDd_coddest.NTSTextDB = NTSZOOM.strIn
        edDd_nomdest.NTSTextDB = NTSCStr(dttTmp.Rows(0)!an_descr1)
        edDd_nomdest2.NTSTextDB = NTSCStr(dttTmp.Rows(0)!an_descr2)
        edDd_inddest.NTSTextDB = NTSCStr(dttTmp.Rows(0)!an_indir)
        edDd_capdest.NTSTextDB = NTSCStr(dttTmp.Rows(0)!an_cap)
        edDd_locdest.NTSTextDB = NTSCStr(dttTmp.Rows(0)!an_citta)
        edDd_prodest.NTSTextDB = NTSCStr(dttTmp.Rows(0)!an_prov)
        edDd_telef.NTSTextDB = NTSCStr(dttTmp.Rows(0)!an_telef)
        edDd_codzona.NTSTextDB = NTSCStr(dttTmp.Rows(0)!an_zona)
        edDd_codfis.NTSTextDB = NTSCStr(dttTmp.Rows(0)!an_codfis)
        edDd_pariva.NTSTextDB = NTSCStr(dttTmp.Rows(0)!an_pariva)
        edDd_faxtlx.NTSTextDB = NTSCStr(dttTmp.Rows(0)!an_faxtlx)
        edDd_agente.NTSTextDB = NTSCStr(dttTmp.Rows(0)!an_agente)
        edDd_agente2.NTSTextDB = NTSCStr(dttTmp.Rows(0)!an_agente2)
        edDd_email.NTSTextDB = NTSCStr(dttTmp.Rows(0)!an_email)
        cbDd_usaem.SelectedValue = NTSCStr(dttTmp.Rows(0)!an_usaem)
        edDd_vett.NTSTextDB = NTSCStr(dttTmp.Rows(0)!an_vett)
        edDd_vett2.NTSTextDB = NTSCStr(dttTmp.Rows(0)!an_vett2)
        edDd_stato.NTSTextDB = NTSCStr(dttTmp.Rows(0)!an_stato)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      dttTmp.Clear()
    End Try
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    Try
      oMenu.ReportImposta(Me)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not Salva(False, True) Then Return
      '--------------------------------------------------------------------------------------------------------------
      Stampa(1)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not Salva(False, True) Then Return
      '--------------------------------------------------------------------------------------------------------------
      Stampa(0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

#End Region

  Public Overridable Function Salva(ByVal bEsci As Boolean, ByVal bAsk As Boolean) As Boolean
    Dim dRes As DialogResult
    Try
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      '-------------------------------------------------
      'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
      If dsDesg.Tables("DESTDIV").Select("", "", DataViewRowState.Added Or DataViewRowState.ModifiedCurrent).Length > 0 Then
        If GctlControllaOutNotEqual() = False Then Return False

        If bAsk Then
          dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128275017320576000, "Confermi il salvataggio?"))
        Else
          dRes = System.Windows.Forms.DialogResult.Yes
        End If

        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oCleClie.DesgSalva(False) Then Return False
          If dsDesg.Tables("DESTDIV").Rows.Count > 0 Then
            edDd_coddest.Enabled = False
            cmdCreaDaLead.Enabled = False
            If oCleClie.lCodDestNew <> 0 Then tlbNuovo.Enabled = False
          End If
        End If
        If dRes = System.Windows.Forms.DialogResult.No Then

          If bEsci Then
            oCleClie.DesgRipristina(dcDesg.Position, dcDesg.Filter)
          Else
            tlbRipristina_ItemClick(Nothing, Nothing)
          End If
        End If
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub cmdCreaDaLead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreaDaLead.Click
    Dim lCodLead As Integer = 0
    Dim oParam As New CLE__PATB
    Try
      If NTSCInt(edDd_coddest.Text) = 0 Then
                oApp.MsgBoxErr(oApp.Tr(Me, 128738066491470000, "Indicare prima il codice destinazione"))
        Return
      End If

      '---------------------------
      'zoom lead
      oParam.nAnno = -1               'devo visualizzare solo i lead non collegati a clienti 
      oParam.strIn = ""               'valore in input
      NTSZOOM.ZoomStrIn("ZOOMLEADS", DittaCorrente, oParam)
      lCodLead = NTSCInt(NTSZOOM.strIn)
      If lCodLead <> 0 Then
        If edDd_nomdest.Text.Trim = "" Then edDd_nomdest.NTSTextDB = "."
        Salva(False, False)
        oCleClie.DesgAgganciaLead(lCodLead, CType(dcDesg.Current, DataRowView).Row)
        dcDesg.ResetBindings(False)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdEstensioni_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEstensioni.Click
    Dim oPar As New CLE__CLDP
    Try
      If NTSCInt(edDd_coddest.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128383007687014000, "Indicare prima il codice destinazione"))
        Return
      End If
      If edDd_coddest.Enabled = True Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128383008555778000, "Prima salvare la destinazione"))
        Return
      End If

      oPar.Ditta = DittaCorrente
      oPar.bAddNew = oCleClie.bNew
      oPar.strNomProg = "BN__CLIE"
      oPar.dPar1 = NTSCInt(edDd_coddest.Text)    'codice destinazione diversa
      oPar.strPar1 = IIf(oCleClie.strTipoConto = "C", "D", "E").ToString
      oPar.ctlPar1 = oCleClie.dsShared.Tables("ANAEXTDD")
      oMenu.RunChild("NTSInformatica", "FRM__ANEX", "", DittaCorrente, "", "BN__ANEX", oPar, "", True, True)
      'restituisce oCleClie.dsShared.Tables("ANAEXTDD") aggiornato
      oCleClie.bHasChanges = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If dsDesg.Tables("DESTDIV").Rows.Count = 0 Then Return
      '--------------------------------------------------------------------------------------------------------------
      If oCleClie.RiempiTTDESTDIVPerStampa(dsDesg.Tables("DESTDIV")) = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      strCrpe = "{TTDESTDIV.codditt} = " & ConvStrRpt(DittaCorrente) & _
        " And {TTDESTDIV.instid} = " & oCleClie.lIITtdestdiv & _
        " And {TTDESTDIV.dd_conto} = " & NTSCInt(dsDesg.Tables("DESTDIV").Rows(0)!dd_conto)
      '--------------------------------------------------------------------------------------------------------------
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSVEDESD", "Reports1", " ", 0, nDestin, "BSVEDESD.RPT", False, _
        "Destinazioni diverse conto '" & dsDesg.Tables("DESTDIV").Rows(0)!dd_conto.ToString & "'", False)
      '--------------------------------------------------------------------------------------------------------------
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

  Public Overridable Sub tlbGoogleMaps_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGoogleMaps.ItemClick
    Dim strQuery As String = ""
    Dim strTmp As String = ""
    Try
      Me.ValidaLastControl()

      If edDd_latitud.Text.Trim <> "" And edDd_longitud.Text.Trim <> "" Then
        strQuery = edDd_latitud.Text.Trim.Replace(",", ".") & "," & edDd_longitud.Text.Trim.Replace(",", ".")
      Else
        If edDd_locdest.Text.Trim <> "" Then ' edAn_indir.Text.Trim <> "" And edAn_prov.Text.Trim <> "" And  
          strQuery = edDd_inddest.Text.Replace(" ", "%20") & ","
          strQuery &= "%20" & edDd_locdest.Text.Replace(" ", "%20") & ","
          strQuery &= "%20" & edDd_prodest.Text.Replace(" ", "%20") & ","
        Else
          oApp.MsgBoxErr(oApp.Tr(Me, 128907642952895815, "Specificare la città (oppure latitudine e longitudine) per poter procedere alla localizzazione"))
          Return
        End If
        If edDd_stato.Text = "" Then
          strQuery &= "%20Italia"
        Else
          If Not oMenu.ValCodiceDb(edDd_stato.Text, DittaCorrente, "TABSTAT", "S", strTmp) Then
            strQuery &= "%20Italia"
          Else
            strQuery &= "%20" & strTmp
          End If
        End If
      End If

      If CLN__STD.IsBis Then
        IS_ExecOnSbc("", "http://maps.google.it/maps?q=" & strQuery)
      Else
        NTSProcessStart("http://maps.google.it/maps?q=" & strQuery, "")
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
End Class