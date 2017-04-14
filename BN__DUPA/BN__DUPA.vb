#Region "Imports"
Imports System.Data
Imports NTSInformatica.CLN__STD
#End Region

Public Class FRM__DUPA

#Region "Moduli"
  Private Moduli_P As Integer = bsModAll
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
#End Region

#Region "Variabili"
  Public oCleDupa As CLE__DUPA
  Public dsDupa As DataSet
  Public oCallParams As CLE__CLDP
  Public dcDupa As BindingSource = New BindingSource
  Public strTabella As String = "AZIENDE"
  Public oSql7 As Object = Nothing
  Public strDirNewDb As String = "C:\"
  Public strDirNewDbLog As String = "C:\"
#End Region

#Region "Variaribli Componenti Form e InitializeComponent"
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
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancellaData As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnGals As NTSInformatica.NTSPanel
  Public WithEvents tlbRicaricaAlert As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbCodaz As NTSInformatica.NTSLabel
  Public WithEvents edCodaz As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDesaz As NTSInformatica.NTSLabel
  Public WithEvents edDesaz As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbExt As NTSInformatica.NTSLabel
  Public WithEvents edExt As NTSInformatica.NTSTextBoxStr
  Public WithEvents fmImpostazioneFonteDati As NTSInformatica.NTSGroupBox
  Public WithEvents edAdoprovider As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAdoprovider As NTSInformatica.NTSLabel
  Public WithEvents cmdCopia As NTSInformatica.NTSButton
  Public WithEvents cmdCreaStruttura As NTSInformatica.NTSButton
  Public WithEvents edConnect As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbConnect As NTSInformatica.NTSLabel
  Public WithEvents lbSubdatatype As NTSInformatica.NTSLabel
  Public WithEvents edAdoconnect As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAdoconnect As NTSInformatica.NTSLabel
  Public WithEvents cmdGestBack As NTSInformatica.NTSButton
  Public WithEvents cmdRicreaSP As NTSInformatica.NTSButton
  Public WithEvents cmdCreaAllegate As NTSInformatica.NTSButton
  Public WithEvents cmdCreaDSN As NTSInformatica.NTSButton
  Public WithEvents cmdImportaDati As NTSInformatica.NTSButton
  Public WithEvents cbSubdatatype As NTSInformatica.NTSComboBox
  Public WithEvents NtsBarButtonItem1 As NTSInformatica.NTSBarButtonItem
  Public WithEvents NtsBarSubItem1 As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbEUR As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbLIT As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCHF As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGBP As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUSD As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbYEN As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbValuta As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbDsnArcproc As NTSInformatica.NTSBarButtonItem
  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__DUPA))
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
    Me.tlbValuta = New NTSInformatica.NTSBarSubItem
    Me.tlbEUR = New NTSInformatica.NTSBarButtonItem
    Me.tlbLIT = New NTSInformatica.NTSBarButtonItem
    Me.tlbCHF = New NTSInformatica.NTSBarButtonItem
    Me.tlbGBP = New NTSInformatica.NTSBarButtonItem
    Me.tlbUSD = New NTSInformatica.NTSBarButtonItem
    Me.tlbYEN = New NTSInformatica.NTSBarButtonItem
    Me.tlbDsnArcproc = New NTSInformatica.NTSBarButtonItem
    Me.tlbRicreaSPArcproc = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancellaData = New NTSInformatica.NTSBarButtonItem
    Me.tlbRicaricaAlert = New NTSInformatica.NTSBarButtonItem
    Me.NtsBarButtonItem1 = New NTSInformatica.NTSBarButtonItem
    Me.NtsBarSubItem1 = New NTSInformatica.NTSBarSubItem
    Me.pnGals = New NTSInformatica.NTSPanel
    Me.ckAzOpGrup = New NTSInformatica.NTSCheckBox
    Me.edAz_rdsservername = New NTSInformatica.NTSTextBoxStr
    Me.lbAz_rdsservername = New NTSInformatica.NTSLabel
    Me.cmdGestBack = New NTSInformatica.NTSButton
    Me.cmdRicreaSP = New NTSInformatica.NTSButton
    Me.cmdCopia = New NTSInformatica.NTSButton
    Me.cmdCreaDSN = New NTSInformatica.NTSButton
    Me.cmdImportaDati = New NTSInformatica.NTSButton
    Me.cmdCreaStruttura = New NTSInformatica.NTSButton
    Me.lbExt = New NTSInformatica.NTSLabel
    Me.lbDesaz = New NTSInformatica.NTSLabel
    Me.lbCodaz = New NTSInformatica.NTSLabel
    Me.edExt = New NTSInformatica.NTSTextBoxStr
    Me.edDesaz = New NTSInformatica.NTSTextBoxStr
    Me.edCodaz = New NTSInformatica.NTSTextBoxStr
    Me.fmImpostazioneFonteDati = New NTSInformatica.NTSGroupBox
    Me.cbSubdatatype = New NTSInformatica.NTSComboBox
    Me.edConnect = New NTSInformatica.NTSTextBoxStr
    Me.cmdCreaAllegate = New NTSInformatica.NTSButton
    Me.lbConnect = New NTSInformatica.NTSLabel
    Me.lbSubdatatype = New NTSInformatica.NTSLabel
    Me.edAdoconnect = New NTSInformatica.NTSTextBoxStr
    Me.lbAdoconnect = New NTSInformatica.NTSLabel
    Me.edAdoprovider = New NTSInformatica.NTSTextBoxStr
    Me.lbAdoprovider = New NTSInformatica.NTSLabel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnGals, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGals.SuspendLayout()
    CType(Me.ckAzOpGrup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAz_rdsservername.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edExt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDesaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmImpostazioneFonteDati, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmImpostazioneFonteDati.SuspendLayout()
    CType(Me.cbSubdatatype.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edConnect.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdoconnect.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdoprovider.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbGuida, Me.tlbEsci, Me.tlbUltimo, Me.tlbImpostaStampante, Me.tlbCancellaData, Me.tlbRicaricaAlert, Me.NtsBarButtonItem1, Me.NtsBarSubItem1, Me.tlbStrumenti, Me.tlbEUR, Me.tlbLIT, Me.tlbCHF, Me.tlbGBP, Me.tlbUSD, Me.tlbYEN, Me.tlbValuta, Me.tlbDsnArcproc, Me.tlbRicreaSPArcproc})
    Me.NtsBarManager1.MaxItemId = 46
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbStrumenti.Id = 31
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbValuta), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDsnArcproc, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRicreaSPArcproc)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbValuta
    '
    Me.tlbValuta.Caption = "Valuta di conto nuovi database"
    Me.tlbValuta.Id = 43
    Me.tlbValuta.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEUR), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbLIT), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCHF), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGBP), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUSD), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbYEN)})
    Me.tlbValuta.Name = "tlbValuta"
    Me.tlbValuta.Visible = True
    '
    'tlbEUR
    '
    Me.tlbEUR.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbEUR.Caption = "Euro"
    Me.tlbEUR.Id = 32
    Me.tlbEUR.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
    Me.tlbEUR.Name = "tlbEUR"
    Me.tlbEUR.Visible = True
    '
    'tlbLIT
    '
    Me.tlbLIT.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbLIT.Caption = "Lire italiane"
    Me.tlbLIT.Id = 33
    Me.tlbLIT.Name = "tlbLIT"
    Me.tlbLIT.Visible = True
    '
    'tlbCHF
    '
    Me.tlbCHF.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbCHF.Caption = "Franco svizzero"
    Me.tlbCHF.Id = 34
    Me.tlbCHF.Name = "tlbCHF"
    Me.tlbCHF.Visible = True
    '
    'tlbGBP
    '
    Me.tlbGBP.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbGBP.Caption = "Lira sterlina"
    Me.tlbGBP.Id = 35
    Me.tlbGBP.Name = "tlbGBP"
    Me.tlbGBP.Visible = True
    '
    'tlbUSD
    '
    Me.tlbUSD.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbUSD.Caption = "Dollaro U.S.A."
    Me.tlbUSD.Id = 36
    Me.tlbUSD.Name = "tlbUSD"
    Me.tlbUSD.Visible = True
    '
    'tlbYEN
    '
    Me.tlbYEN.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbYEN.Caption = "Yen giapponese"
    Me.tlbYEN.Id = 37
    Me.tlbYEN.Name = "tlbYEN"
    Me.tlbYEN.Visible = True
    '
    'tlbDsnArcproc
    '
    Me.tlbDsnArcproc.Caption = "Crea server DSN per ARCPROC"
    Me.tlbDsnArcproc.Id = 44
    Me.tlbDsnArcproc.Name = "tlbDsnArcproc"
    Me.tlbDsnArcproc.Visible = True
    '
    'tlbRicreaSPArcproc
    '
    Me.tlbRicreaSPArcproc.Caption = "Ricarica stored procedure per ARCPROC"
    Me.tlbRicreaSPArcproc.Id = 45
    Me.tlbRicreaSPArcproc.Name = "tlbRicreaSPArcproc"
    Me.tlbRicreaSPArcproc.Visible = True
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
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta stampante"
    Me.tlbImpostaStampante.Id = 25
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbCancellaData
    '
    Me.tlbCancellaData.Caption = "Cancella data ultima esecuzione schedulata"
    Me.tlbCancellaData.Id = 26
    Me.tlbCancellaData.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F4))
    Me.tlbCancellaData.Name = "tlbCancellaData"
    Me.tlbCancellaData.Visible = True
    '
    'tlbRicaricaAlert
    '
    Me.tlbRicaricaAlert.Caption = "Ricarica tipi alert"
    Me.tlbRicaricaAlert.Id = 28
    Me.tlbRicaricaAlert.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F3))
    Me.tlbRicaricaAlert.Name = "tlbRicaricaAlert"
    Me.tlbRicaricaAlert.Visible = True
    '
    'NtsBarButtonItem1
    '
    Me.NtsBarButtonItem1.Caption = "tlbStrumenti"
    Me.NtsBarButtonItem1.Id = 29
    Me.NtsBarButtonItem1.Name = "NtsBarButtonItem1"
    Me.NtsBarButtonItem1.Visible = True
    '
    'NtsBarSubItem1
    '
    Me.NtsBarSubItem1.Caption = "tlbStrumenti"
    Me.NtsBarSubItem1.Id = 30
    Me.NtsBarSubItem1.Name = "NtsBarSubItem1"
    Me.NtsBarSubItem1.Visible = True
    '
    'pnGals
    '
    Me.pnGals.AllowDrop = True
    Me.pnGals.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGals.Appearance.Options.UseBackColor = True
    Me.pnGals.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGals.Controls.Add(Me.ckAzOpGrup)
    Me.pnGals.Controls.Add(Me.edAz_rdsservername)
    Me.pnGals.Controls.Add(Me.lbAz_rdsservername)
    Me.pnGals.Controls.Add(Me.cmdGestBack)
    Me.pnGals.Controls.Add(Me.cmdRicreaSP)
    Me.pnGals.Controls.Add(Me.cmdCopia)
    Me.pnGals.Controls.Add(Me.cmdCreaDSN)
    Me.pnGals.Controls.Add(Me.cmdImportaDati)
    Me.pnGals.Controls.Add(Me.cmdCreaStruttura)
    Me.pnGals.Controls.Add(Me.lbExt)
    Me.pnGals.Controls.Add(Me.lbDesaz)
    Me.pnGals.Controls.Add(Me.lbCodaz)
    Me.pnGals.Controls.Add(Me.edExt)
    Me.pnGals.Controls.Add(Me.edDesaz)
    Me.pnGals.Controls.Add(Me.edCodaz)
    Me.pnGals.Controls.Add(Me.fmImpostazioneFonteDati)
    Me.pnGals.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGals.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGals.Location = New System.Drawing.Point(0, 30)
    Me.pnGals.Name = "pnGals"
    Me.pnGals.NTSActiveTrasparency = True
    Me.pnGals.Size = New System.Drawing.Size(668, 199)
    Me.pnGals.TabIndex = 5
    Me.pnGals.Text = "NtsPanel1"
    '
    'ckAzOpGrup
    '
    Me.ckAzOpGrup.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAzOpGrup.Location = New System.Drawing.Point(101, 34)
    Me.ckAzOpGrup.Name = "ckAzOpGrup"
    Me.ckAzOpGrup.NTSCheckValue = "S"
    Me.ckAzOpGrup.NTSUnCheckValue = "N"
    Me.ckAzOpGrup.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAzOpGrup.Properties.Appearance.Options.UseBackColor = True
    Me.ckAzOpGrup.Properties.Caption = "Non proporre questo database selezionato negli aggiornamenti"
    Me.ckAzOpGrup.Size = New System.Drawing.Size(346, 19)
    Me.ckAzOpGrup.TabIndex = 527
    '
    'edAz_rdsservername
    '
    Me.edAz_rdsservername.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAz_rdsservername.Location = New System.Drawing.Point(101, 82)
    Me.edAz_rdsservername.Name = "edAz_rdsservername"
    Me.edAz_rdsservername.NTSDbField = ""
    Me.edAz_rdsservername.NTSForzaVisZoom = False
    Me.edAz_rdsservername.NTSOldValue = ""
    Me.edAz_rdsservername.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAz_rdsservername.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAz_rdsservername.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAz_rdsservername.Properties.MaxLength = 65536
    Me.edAz_rdsservername.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAz_rdsservername.Size = New System.Drawing.Size(377, 20)
    Me.edAz_rdsservername.TabIndex = 525
    '
    'lbAz_rdsservername
    '
    Me.lbAz_rdsservername.AutoSize = True
    Me.lbAz_rdsservername.BackColor = System.Drawing.Color.Transparent
    Me.lbAz_rdsservername.Location = New System.Drawing.Point(14, 85)
    Me.lbAz_rdsservername.Name = "lbAz_rdsservername"
    Me.lbAz_rdsservername.NTSDbField = ""
    Me.lbAz_rdsservername.Size = New System.Drawing.Size(86, 13)
    Me.lbAz_rdsservername.TabIndex = 526
    Me.lbAz_rdsservername.Text = "URL sito azienda"
    Me.lbAz_rdsservername.Tooltip = ""
    Me.lbAz_rdsservername.UseMnemonic = False
    '
    'cmdGestBack
    '
    Me.cmdGestBack.ImageText = ""
    Me.cmdGestBack.Location = New System.Drawing.Point(508, 11)
    Me.cmdGestBack.Name = "cmdGestBack"
    Me.cmdGestBack.Size = New System.Drawing.Size(149, 26)
    Me.cmdGestBack.TabIndex = 524
    Me.cmdGestBack.Text = "Gestione bac&kup"
    '
    'cmdRicreaSP
    '
    Me.cmdRicreaSP.ImageText = ""
    Me.cmdRicreaSP.Location = New System.Drawing.Point(508, 107)
    Me.cmdRicreaSP.Name = "cmdRicreaSP"
    Me.cmdRicreaSP.Size = New System.Drawing.Size(149, 26)
    Me.cmdRicreaSP.TabIndex = 524
    Me.cmdRicreaSP.Text = "Ricarica stored &procedure"
    '
    'cmdCopia
    '
    Me.cmdCopia.ImageText = ""
    Me.cmdCopia.Location = New System.Drawing.Point(484, 43)
    Me.cmdCopia.Name = "cmdCopia"
    Me.cmdCopia.Size = New System.Drawing.Size(24, 23)
    Me.cmdCopia.TabIndex = 524
    Me.cmdCopia.Text = "C&rea da archivio esterno"
    Me.cmdCopia.Visible = False
    '
    'cmdCreaDSN
    '
    Me.cmdCreaDSN.ImageText = ""
    Me.cmdCreaDSN.Location = New System.Drawing.Point(508, 75)
    Me.cmdCreaDSN.Name = "cmdCreaDSN"
    Me.cmdCreaDSN.Size = New System.Drawing.Size(149, 26)
    Me.cmdCreaDSN.TabIndex = 524
    Me.cmdCreaDSN.Text = "Crea &server DSN"
    '
    'cmdImportaDati
    '
    Me.cmdImportaDati.ImageText = ""
    Me.cmdImportaDati.Location = New System.Drawing.Point(508, 43)
    Me.cmdImportaDati.Name = "cmdImportaDati"
    Me.cmdImportaDati.Size = New System.Drawing.Size(149, 26)
    Me.cmdImportaDati.TabIndex = 524
    Me.cmdImportaDati.Text = "&Importa dati da altro DB"
    '
    'cmdCreaStruttura
    '
    Me.cmdCreaStruttura.ImageText = ""
    Me.cmdCreaStruttura.Location = New System.Drawing.Point(484, 14)
    Me.cmdCreaStruttura.Name = "cmdCreaStruttura"
    Me.cmdCreaStruttura.Size = New System.Drawing.Size(24, 23)
    Me.cmdCreaStruttura.TabIndex = 524
    Me.cmdCreaStruttura.Text = "&Crea struttura"
    Me.cmdCreaStruttura.Visible = False
    '
    'lbExt
    '
    Me.lbExt.AutoSize = True
    Me.lbExt.BackColor = System.Drawing.Color.Transparent
    Me.lbExt.Location = New System.Drawing.Point(244, 14)
    Me.lbExt.Name = "lbExt"
    Me.lbExt.NTSDbField = ""
    Me.lbExt.Size = New System.Drawing.Size(119, 13)
    Me.lbExt.TabIndex = 517
    Me.lbExt.Text = "Codice ditta predefinito"
    Me.lbExt.Tooltip = ""
    Me.lbExt.UseMnemonic = False
    '
    'lbDesaz
    '
    Me.lbDesaz.AutoSize = True
    Me.lbDesaz.BackColor = System.Drawing.Color.Transparent
    Me.lbDesaz.Location = New System.Drawing.Point(14, 59)
    Me.lbDesaz.Name = "lbDesaz"
    Me.lbDesaz.NTSDbField = ""
    Me.lbDesaz.Size = New System.Drawing.Size(81, 13)
    Me.lbDesaz.TabIndex = 517
    Me.lbDesaz.Text = "Ragione sociale"
    Me.lbDesaz.Tooltip = ""
    Me.lbDesaz.UseMnemonic = False
    '
    'lbCodaz
    '
    Me.lbCodaz.AutoSize = True
    Me.lbCodaz.BackColor = System.Drawing.Color.Transparent
    Me.lbCodaz.Location = New System.Drawing.Point(14, 14)
    Me.lbCodaz.Name = "lbCodaz"
    Me.lbCodaz.NTSDbField = ""
    Me.lbCodaz.Size = New System.Drawing.Size(79, 13)
    Me.lbCodaz.TabIndex = 517
    Me.lbCodaz.Text = "Codice azienda"
    Me.lbCodaz.Tooltip = ""
    Me.lbCodaz.UseMnemonic = False
    '
    'edExt
    '
    Me.edExt.Cursor = System.Windows.Forms.Cursors.Default
    Me.edExt.Location = New System.Drawing.Point(369, 11)
    Me.edExt.Name = "edExt"
    Me.edExt.NTSDbField = ""
    Me.edExt.NTSForzaVisZoom = False
    Me.edExt.NTSOldValue = ""
    Me.edExt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edExt.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edExt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edExt.Properties.MaxLength = 65536
    Me.edExt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edExt.Size = New System.Drawing.Size(109, 20)
    Me.edExt.TabIndex = 518
    '
    'edDesaz
    '
    Me.edDesaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDesaz.Location = New System.Drawing.Point(101, 56)
    Me.edDesaz.Name = "edDesaz"
    Me.edDesaz.NTSDbField = ""
    Me.edDesaz.NTSForzaVisZoom = False
    Me.edDesaz.NTSOldValue = ""
    Me.edDesaz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDesaz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDesaz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDesaz.Properties.MaxLength = 65536
    Me.edDesaz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDesaz.Size = New System.Drawing.Size(377, 20)
    Me.edDesaz.TabIndex = 518
    '
    'edCodaz
    '
    Me.edCodaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodaz.Location = New System.Drawing.Point(101, 11)
    Me.edCodaz.Name = "edCodaz"
    Me.edCodaz.NTSDbField = ""
    Me.edCodaz.NTSForzaVisZoom = False
    Me.edCodaz.NTSOldValue = ""
    Me.edCodaz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodaz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodaz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodaz.Properties.MaxLength = 65536
    Me.edCodaz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodaz.Size = New System.Drawing.Size(100, 20)
    Me.edCodaz.TabIndex = 518
    '
    'fmImpostazioneFonteDati
    '
    Me.fmImpostazioneFonteDati.AllowDrop = True
    Me.fmImpostazioneFonteDati.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmImpostazioneFonteDati.Appearance.Options.UseBackColor = True
    Me.fmImpostazioneFonteDati.Controls.Add(Me.cbSubdatatype)
    Me.fmImpostazioneFonteDati.Controls.Add(Me.edConnect)
    Me.fmImpostazioneFonteDati.Controls.Add(Me.cmdCreaAllegate)
    Me.fmImpostazioneFonteDati.Controls.Add(Me.lbConnect)
    Me.fmImpostazioneFonteDati.Controls.Add(Me.lbSubdatatype)
    Me.fmImpostazioneFonteDati.Controls.Add(Me.edAdoconnect)
    Me.fmImpostazioneFonteDati.Controls.Add(Me.lbAdoconnect)
    Me.fmImpostazioneFonteDati.Controls.Add(Me.edAdoprovider)
    Me.fmImpostazioneFonteDati.Controls.Add(Me.lbAdoprovider)
    Me.fmImpostazioneFonteDati.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmImpostazioneFonteDati.Location = New System.Drawing.Point(12, 108)
    Me.fmImpostazioneFonteDati.Name = "fmImpostazioneFonteDati"
    Me.fmImpostazioneFonteDati.Size = New System.Drawing.Size(475, 87)
    Me.fmImpostazioneFonteDati.TabIndex = 522
    Me.fmImpostazioneFonteDati.Text = "Impostazione fonte dati:"
    '
    'cbSubdatatype
    '
    Me.cbSubdatatype.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbSubdatatype.DataSource = Nothing
    Me.cbSubdatatype.DisplayMember = ""
    Me.cbSubdatatype.Enabled = False
    Me.cbSubdatatype.Location = New System.Drawing.Point(324, 29)
    Me.cbSubdatatype.Name = "cbSubdatatype"
    Me.cbSubdatatype.NTSDbField = ""
    Me.cbSubdatatype.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbSubdatatype.Properties.DropDownRows = 30
    Me.cbSubdatatype.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbSubdatatype.SelectedValue = ""
    Me.cbSubdatatype.Size = New System.Drawing.Size(139, 20)
    Me.cbSubdatatype.TabIndex = 519
    Me.cbSubdatatype.ValueMember = ""
    '
    'edConnect
    '
    Me.edConnect.Cursor = System.Windows.Forms.Cursors.Default
    Me.edConnect.Location = New System.Drawing.Point(118, 82)
    Me.edConnect.Name = "edConnect"
    Me.edConnect.NTSDbField = ""
    Me.edConnect.NTSForzaVisZoom = False
    Me.edConnect.NTSOldValue = ""
    Me.edConnect.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edConnect.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edConnect.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edConnect.Properties.MaxLength = 65536
    Me.edConnect.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edConnect.Size = New System.Drawing.Size(345, 20)
    Me.edConnect.TabIndex = 518
    Me.edConnect.Visible = False
    '
    'cmdCreaAllegate
    '
    Me.cmdCreaAllegate.ImageText = ""
    Me.cmdCreaAllegate.Location = New System.Drawing.Point(317, 79)
    Me.cmdCreaAllegate.Name = "cmdCreaAllegate"
    Me.cmdCreaAllegate.Size = New System.Drawing.Size(149, 23)
    Me.cmdCreaAllegate.TabIndex = 524
    Me.cmdCreaAllegate.Text = "Crea &tabelle allegate"
    Me.cmdCreaAllegate.Visible = False
    '
    'lbConnect
    '
    Me.lbConnect.AutoSize = True
    Me.lbConnect.BackColor = System.Drawing.Color.Transparent
    Me.lbConnect.Location = New System.Drawing.Point(6, 85)
    Me.lbConnect.Name = "lbConnect"
    Me.lbConnect.NTSDbField = ""
    Me.lbConnect.Size = New System.Drawing.Size(108, 13)
    Me.lbConnect.TabIndex = 517
    Me.lbConnect.Text = "Opzione connessione"
    Me.lbConnect.Tooltip = ""
    Me.lbConnect.UseMnemonic = False
    Me.lbConnect.Visible = False
    '
    'lbSubdatatype
    '
    Me.lbSubdatatype.AutoSize = True
    Me.lbSubdatatype.BackColor = System.Drawing.Color.Transparent
    Me.lbSubdatatype.Location = New System.Drawing.Point(277, 33)
    Me.lbSubdatatype.Name = "lbSubdatatype"
    Me.lbSubdatatype.NTSDbField = ""
    Me.lbSubdatatype.Size = New System.Drawing.Size(43, 13)
    Me.lbSubdatatype.TabIndex = 517
    Me.lbSubdatatype.Text = "Tipo DB"
    Me.lbSubdatatype.Tooltip = ""
    Me.lbSubdatatype.UseMnemonic = False
    '
    'edAdoconnect
    '
    Me.edAdoconnect.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdoconnect.Location = New System.Drawing.Point(89, 56)
    Me.edAdoconnect.Name = "edAdoconnect"
    Me.edAdoconnect.NTSDbField = ""
    Me.edAdoconnect.NTSForzaVisZoom = False
    Me.edAdoconnect.NTSOldValue = ""
    Me.edAdoconnect.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdoconnect.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdoconnect.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAdoconnect.Properties.MaxLength = 65536
    Me.edAdoconnect.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdoconnect.Size = New System.Drawing.Size(374, 20)
    Me.edAdoconnect.TabIndex = 518
    '
    'lbAdoconnect
    '
    Me.lbAdoconnect.AutoSize = True
    Me.lbAdoconnect.BackColor = System.Drawing.Color.Transparent
    Me.lbAdoconnect.Location = New System.Drawing.Point(6, 59)
    Me.lbAdoconnect.Name = "lbAdoconnect"
    Me.lbAdoconnect.NTSDbField = ""
    Me.lbAdoconnect.Size = New System.Drawing.Size(87, 13)
    Me.lbAdoconnect.TabIndex = 517
    Me.lbAdoconnect.Text = "Stringa conness."
    Me.lbAdoconnect.Tooltip = ""
    Me.lbAdoconnect.UseMnemonic = False
    '
    'edAdoprovider
    '
    Me.edAdoprovider.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdoprovider.Enabled = False
    Me.edAdoprovider.Location = New System.Drawing.Point(89, 29)
    Me.edAdoprovider.Name = "edAdoprovider"
    Me.edAdoprovider.NTSDbField = ""
    Me.edAdoprovider.NTSForzaVisZoom = False
    Me.edAdoprovider.NTSOldValue = ""
    Me.edAdoprovider.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdoprovider.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdoprovider.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAdoprovider.Properties.MaxLength = 65536
    Me.edAdoprovider.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdoprovider.Size = New System.Drawing.Size(139, 20)
    Me.edAdoprovider.TabIndex = 518
    '
    'lbAdoprovider
    '
    Me.lbAdoprovider.AutoSize = True
    Me.lbAdoprovider.BackColor = System.Drawing.Color.Transparent
    Me.lbAdoprovider.Location = New System.Drawing.Point(6, 33)
    Me.lbAdoprovider.Name = "lbAdoprovider"
    Me.lbAdoprovider.NTSDbField = ""
    Me.lbAdoprovider.Size = New System.Drawing.Size(77, 13)
    Me.lbAdoprovider.TabIndex = 517
    Me.lbAdoprovider.Text = "Nome provider"
    Me.lbAdoprovider.Tooltip = ""
    Me.lbAdoprovider.UseMnemonic = False
    '
    'FRM__DUPA
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(668, 229)
    Me.Controls.Add(Me.pnGals)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.LookAndFeel.SkinName = "Money Twins"
    Me.MaximizeBox = False
    Me.Name = "FRM__DUPA"
    Me.Text = "GESTIONE AZIENDE/DATABASE"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnGals, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGals.ResumeLayout(False)
    Me.pnGals.PerformLayout()
    CType(Me.ckAzOpGrup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAz_rdsservername.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edExt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDesaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmImpostazioneFonteDati, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmImpostazioneFonteDati.ResumeLayout(False)
    Me.fmImpostazioneFonteDati.PerformLayout()
    CType(Me.cbSubdatatype.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edConnect.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdoconnect.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdoprovider.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
#End Region

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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__DUPA", "BE__DUPA", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128738985928593750, "ERRORE in fase di creazione Entity:" & vbCrLf) & strErr)
      Return False
    End If
    oCleDupa = CType(oTmp, CLE__DUPA)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__DUPA", strRemoteServer, strRemotePort)
    AddHandler oCleDupa.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleDupa.Init(oApp, oScript, oMenu.oCleComm, strTabella, bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub LoadImage()
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
      tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
      tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
    Catch ex As Exception
    End Try
  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      LoadImage()
      tlbMain.NTSSetToolTip()

      edCodaz.NTSSetParam(oMenu, oApp.Tr(Me, 128230023372625010, "Codice azienda"), 25, True)
      edDesaz.NTSSetParam(oMenu, oApp.Tr(Me, 128230023372625011, "Descrizione azienda"), 50, True)
      edExt.NTSSetParam(oMenu, oApp.Tr(Me, 128230023372698712, "Ditta predefinita"), 12, True)

      edAdoprovider.NTSSetParam(oMenu, oApp.Tr(Me, 128230023372625013, "Nome oledb provider"), 50, True)
      edAdoconnect.NTSSetParam(oMenu, oApp.Tr(Me, 128230023372625014, "Stringa conness. OLEDB provider"), 200, True)
      cbSubdatatype.NTSSetParam(oApp.Tr(Me, 128230023372625015, "DB sottostante"))
      edConnect.NTSSetParam(oMenu, oApp.Tr(Me, 128230023372625016, "Opzione connessione"), 200, True)
      edAz_rdsservername.NTSSetParam(oMenu, oApp.Tr(Me, 130003105670711233, "URL sito aziendale"), 100, True)
      ckAzOpGrup.NTSSetParam(oMenu, oApp.Tr(Me, 130226021794589410, "Non proporre questo database selezionato nelle conversioni"), 99, 0)

      tlbDsnArcproc.Caption = oApp.Tr(Me, 128230023372625012, "Crea Server DSN per ") & oApp.DbAp.Nome
      tlbRicreaSPArcproc.Caption = oApp.Tr(Me, 128230023372623412, "Ricarica stored procedure per ") & oApp.DbAp.Nome

      edDesaz.NTSSetParamZoom("..")
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
  Public Overridable Sub CaricaCombo()
    Dim dttSubdatatype As New DataTable()
    Try
      dttSubdatatype.Columns.Add("cod", GetType(String))
      dttSubdatatype.Columns.Add("val", GetType(String))
      dttSubdatatype.Rows.Add(New Object() {"(Nessuno)", "(Nessuno)"})
      dttSubdatatype.Rows.Add(New Object() {"SQLServer7", "SQL Server/MSDE"})
      dttSubdatatype.AcceptChanges()
      cbSubdatatype.DataSource = dttSubdatatype
      cbSubdatatype.ValueMember = "cod"
      cbSubdatatype.DisplayMember = "val"
      cbSubdatatype.SelectedIndex = 1
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano già  stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      edCodaz.NTSDbField = strTabella & ".AzCodaz"
      edDesaz.NTSDbField = strTabella & ".AzDescr"
      edExt.NTSDbField = strTabella & ".AzExt"
      edAz_rdsservername.NTSDbField = strTabella & ".az_rdsservername"

      edAdoprovider.NTSDbField = strTabella & ".az_adoprovider"
      edAdoconnect.NTSDbField = strTabella & ".az_adoconnect"
      cbSubdatatype.NTSDbField = strTabella & ".AzSubdatatype"
      edConnect.NTSDbField = strTabella & ".AzConnect"
      ckAzOpGrup.NTSText.NTSDbField = strTabella & ".AzOpGrup"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcDupa, Me)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRM__DUPA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      CaricaCombo()
      InitControls()
      tlbEUR.Down = True
      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      If Not oCleDupa.Apri(DittaCorrente, dsDupa) Then
        Me.Close()
        Return
      End If

      dcDupa.DataSource = dsDupa.Tables(strTabella)
      dsDupa.AcceptChanges()

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()

      oCleDupa.bHasChanges = False
      dsDupa.AcceptChanges()

      '--------------------------------------------
      'sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
      If Not oCallParams Is Nothing Then
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
          tlbNuovo_ItemClick(tlbNuovo, Nothing)
        ElseIf Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) <> "" Then
          For i As Integer = 0 To dcDupa.List.Count - 1
            If CType(dcDupa.Item(i), DataRowView)!AzCodaz.ToString = Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) Then
              dcDupa.Position = i
              Exit For
            End If
          Next
        End If
      Else
        If dsDupa.Tables(strTabella).Rows.Count = 0 Then tlbNuovo_ItemClick(Nothing, Nothing)
        dcDupa.ResetBindings(False)
        dcDupa.MoveFirst()
      End If    'If Not oCallParams Is Nothing Then

      '----------------------
      'inizializzazione form
      SetControls()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione Friendly, nasconde alcuni controlli
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = True Then
        tlbNuovo.Enabled = False
        tlbCancella.Enabled = False
        tlbNuovo.Visible = False
        tlbCancella.Visible = False
        lbAz_rdsservername.Visible = False
        edAz_rdsservername.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRM__DUPA_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
    Try
      Me.Text = UCase(oApp.Tr(Me, 129024303086239014, "GESTIONE AZIENDE/DATABASE"))
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub FRM__DUPA_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva(True) Then e.Cancel = True
  End Sub
  Public Overridable Sub FRM__DUPA_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcDupa.Dispose()
      If Not dsDupa Is Nothing Then dsDupa.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Dim oCallParamTmp As New CLE__CLDP
    Dim frmAlta As FRM__WIZA = Nothing
    Dim bUnicode As Boolean = False
    Try
      frmAlta = CType(NTSNewFormModal("FRM__WIZA"), FRM__WIZA)

      If Not Salva() Then Return

      oCleDupa.Nuovo()
      dcDupa.MoveLast()

      SetControls()

      oCallParamTmp.strParam = ""
      'Apre la modale
      frmAlta.Init(oMenu, oCallParamTmp, DittaCorrente)
      frmAlta.InitEntity(oCleDupa)
      frmAlta.ShowDialog()
      strDirNewDb = frmAlta.edMdf.Text 'directory per nuovi database
      strDirNewDbLog = frmAlta.edLdf.Text 'directory per nuovi database
      bUnicode = frmAlta.ckUnicode.Checked
      frmAlta.Dispose()
      frmAlta = Nothing

      Select Case oCallParamTmp.strParam
        Case "N"
          If Not oCleDupa.CheckPermessiSqlServerUser(edAdoconnect.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129961634016000228, "L'utente di SQL Server non ha i permessi pre creare il database. Operazione annullata."))
            Return
          End If
          CreaStruttura(bUnicode)
        Case "C", "A"
          'eseguo la stored procedure per fare l'attach del DB
          If oCallParamTmp.strParam = "A" Then
            If Not oCleDupa.CheckPermessiSqlServerUser(edAdoconnect.Text) Then
              oApp.MsgBoxErr(oApp.Tr(Me, 129961633681471027, "L'utente di SQL Server non ha i permessi pre creare il database. Operazione annullata."))
              Return
            End If
            If Not oCleDupa.CollegaDB(edAdoconnect.Text, oCallParamTmp.strPar4, oCallParamTmp.strPar5) Then Return
          End If
          If Not oCleDupa.Salva(False) Then Return
      End Select
      'Creazione Server DSN
      oCleDupa.CreateSQLServerSystemDSN(edCodaz.Text, oCleDupa.EstraiParametroDaConnectionString(IIf(edAdoconnect.Text = "", oApp.PrcConnect, edAdoconnect.Text), "SERVER"), _
                                        edCodaz.Text, CBool(IIf(InStr(IIf(edAdoconnect.Text = "", oApp.PrcConnect, edAdoconnect.Text), "Trusted_Connection") > 0, True, False)))


      edCodaz.Focus()
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
      If StatoNuovo() Then Return

      If UCase(NTSCStr(dsDupa.Tables(strTabella).Rows(dcDupa.Position)!AzCodaz)) = UCase(oApp.Db.Nome) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128762520866250000, "ATTENZIONE" & vbCrLf & _
            "E' impossibile cancellare l'azienda-database su cui si sta attualmente lavorando !" & vbCrLf & _
            "E' necessario posizionarsi prima su un'altra azienda-database."))
        Return
      End If

      Dim dlgRes As DialogResult
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128738985928750000, "Si conferma la cancellazione del record?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128738985928750001, "Si desidera Cancellare anche i Database?")) = Windows.Forms.DialogResult.Yes Then
            If Not CancellaDatabase() Then
              If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128738985928750110, "Si sono verificati degli errori in fase di cancellazione dei database. Si vuole comunque procedere con la cancellazione del record?")) = Windows.Forms.DialogResult.No Then
                Return
              End If
            End If
          End If

          If dsDupa.Tables(strTabella).Rows.Count = 1 Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          dcDupa.RemoveAt(dcDupa.Position)
          If Not oCleDupa.Salva(True) Then
            If dsDupa.Tables(strTabella).Rows.Count = 1 And bRemovBinding = True Then
              If bRemovBinding Then NTSFormAddDataBinding(dcDupa, Me)
              bRemovBinding = False
            End If
          End If

          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcDupa, Me)
            bRemovBinding = False
          End If

      End Select

      SetControls()
    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcDupa, Me)
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
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128738985928906250, "Ripristinare le modifiche apportate?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          If dsDupa.Tables(strTabella).Rows.Count = 1 And dsDupa.Tables(strTabella).Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          If Not oCleDupa.Ripristina(dcDupa.Position, dcDupa.Filter) Then
            If dsDupa.Tables(strTabella).Rows.Count = 1 And bRemovBinding = True Then
              If bRemovBinding Then NTSFormAddDataBinding(dcDupa, Me)
              bRemovBinding = False
            End If
          End If

          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcDupa, Me)
            bRemovBinding = False
          End If

          SetControls()

      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcDupa, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim ctrlTmp As Control = NTSFindControlForZoom()
    If ctrlTmp Is Nothing Then Return

    Try

      If edDesaz.Focused Then
        If StatoNuovo() Then Return
        'If Not Salva(False) Then Return
        '--------------------------------------------
        'zoom per la ricerca delle commesse
        NTSZOOM.strIn = edDesaz.Text
        NTSZOOM.ZoomStrIn("ZOOMAZIENDE", DittaCorrente, New CLE__PATB)
        posizionaAfterZoom(NTSZOOM.strIn)
      Else
        If edExt.Focused Then
          If edAdoprovider.Text <> "SQLOLEDB" Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128230023372672312, "Funzione non disponibile per database di tipo: |" & edAdoprovider.Text & "|"))
            Return
          End If
          Dim dsTmp As New DataSet
          Dim strTmp As String = NTSCStr(CType(dcDupa.Current, DataRowView).Row!az_adoconnect)
          If strTmp.ToUpper.IndexOf("SERVER") = -1 Then Decodifica(strTmp)
          If oCleDupa.LeggiDitte(strTmp, dsTmp) Then
            If dsTmp.Tables("tabanaz").Rows.Count = 0 Then
              oApp.MsgBoxInfo(oApp.Tr(Me, 128230023372645542, "Non è presente nessuna ditta nel database selezionato."))
              Return
            End If
            Dim strMsg As String = ""
            For i As Integer = 0 To dsTmp.Tables("tabanaz").Rows.Count - 1
              strMsg = strMsg & vbCrLf & dsTmp.Tables("tabanaz").Rows(i)!codditt & " - " & dsTmp.Tables("tabanaz").Rows(i)!tb_azrags1
            Next
            oApp.MsgBoxInfo(oApp.Tr(Me, 128230023372627312, "Ditte presenti nel database selezionato: |") & strMsg & "|")
          Else
            oApp.MsgBoxInfo(oApp.Tr(Me, 128230023372645642, "Impossibile connetteri al database. Controllare la stringa di connessione."))
            Return
          End If
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub posizionaAfterZoom(ByVal strZoom As String)
    Try
      For i As Integer = 0 To dcDupa.List.Count - 1
        If CType(dcDupa.Item(i), DataRowView)!AzCodaz.ToString = strZoom Then
          dcDupa.Position = i
          Exit For
        End If
      Next

      SetControls()
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

  Public Overridable Sub tlbNavigazione_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick, tlbPrecedente.ItemClick, tlbSuccessivo.ItemClick, tlbUltimo.ItemClick
    Try
      If Not Salva() Then Return

      Select Case e.Item.Name
        Case tlbPrimo.Name
          dcDupa.MoveFirst()
        Case tlbPrecedente.Name
          dcDupa.MovePrevious()
        Case tlbSuccessivo.Name
          dcDupa.MoveNext()
        Case tlbUltimo.Name
          dcDupa.MoveLast()
      End Select

      SetControls()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDsnArcproc_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDsnArcproc.ItemClick
    Dim nRet As Integer = 0
    Try
      nRet = oCleDupa.CreateSQLServerSystemDSN(oApp.DbAp.Nome, oCleDupa.EstraiParametroDaConnectionString(oApp.PrcConnect, "SERVER"), oApp.DbAp.Nome, CBool(IIf(InStr(oApp.PrcConnect, "Trusted_Connection") > 0, True, False)))
      If nRet <> 1 Then Return
      oApp.MsgBoxInfo(oApp.Tr(Me, 128230023371649102, "Operazione eseguita correttamente."))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Function Salva(Optional ByVal bEsci As Boolean = False, Optional ByVal bAsk As Boolean = True) As Boolean
    Dim dRes As DialogResult
    Try
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      If oCleDupa.RecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        dRes = Windows.Forms.DialogResult.Yes
        If bAsk Then
          If bEsci Then
            If StatoNuovo() Then
              dRes = oApp.MsgBoxInfoYesNoCancel_DefYes(oApp.Tr(Me, 128762516454062500, "Confermi il salvataggio del nuovo record prima di uscire?"))
            Else
              dRes = oApp.MsgBoxInfoYesNoCancel_DefYes(oApp.Tr(Me, 128762516028750000, "Confermi il salvataggio prima di uscire?"))
            End If
          Else
            If StatoNuovo() Then
              dRes = oApp.MsgBoxInfoYesNoCancel_DefYes(oApp.Tr(Me, 128738985929062500, "Confermi il salvataggio del nuovo record?"))
            Else
              dRes = oApp.MsgBoxInfoYesNoCancel_DefYes(oApp.Tr(Me, 128762516748125000, "Confermi il salvataggio?"))
            End If
          End If
        End If
        Select Case dRes
          Case Windows.Forms.DialogResult.Yes
            If Not oCleDupa.Salva(False) Then Return False
            SetControls()
          Case Windows.Forms.DialogResult.No
            tlbRipristina_ItemClick(Nothing, Nothing)
          Case Windows.Forms.DialogResult.Cancel
            Return False
        End Select

      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub SetControls()
    Try
      If StatoNuovo() Then
        If Not edCodaz.Enabled Then GctlSetVisEnab(edCodaz, False)
        If Not cmdCreaStruttura.Enabled Then GctlSetVisEnab(cmdCreaStruttura, False)
        If Not cmdCopia.Enabled Then GctlSetVisEnab(cmdCopia, False)
        cmdGestBack.Enabled = False
        cmdImportaDati.Enabled = False
        cmdCreaAllegate.Enabled = False
        cmdCreaDSN.Enabled = False
        cmdRicreaSP.Enabled = False
      Else
        edCodaz.Enabled = False
        cmdCreaStruttura.Enabled = False
        cmdCopia.Enabled = False
        If Not cmdGestBack.Enabled Then GctlSetVisEnab(cmdGestBack, False)
        If Not cmdImportaDati.Enabled Then GctlSetVisEnab(cmdImportaDati, False)
        If Not cmdCreaAllegate.Enabled Then GctlSetVisEnab(cmdCreaAllegate, False)
        If Not cmdCreaDSN.Enabled Then GctlSetVisEnab(cmdCreaDSN, False)
        If Not cmdRicreaSP.Enabled Then GctlSetVisEnab(cmdRicreaSP, False)
        If edAdoprovider.Text <> "SQLOLEDB" Then
          cmdCreaAllegate.Enabled = False
          cmdRicreaSP.Enabled = False
          cmdGestBack.Enabled = False
          cmdImportaDati.Enabled = False
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdCreaStruttura_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreaStruttura.Click
    Try
      CreaStruttura(False)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub CreaStruttura(ByVal bUnicode As Boolean)
    Dim frmProg As FRM__PROG = Nothing
    'Dim bApriSql7 As Boolean = False
    Dim nRis As Integer = 0
    Try
      frmProg = CType(NTSNewFormModal("FRM__PROG"), FRM__PROG)

      If Not oCleDupa.TestPreSalva Then Return

      'bApriSql7 = ApriSql7()
      'If Not bApriSql7 Then Return

      'If Not ConnettiSql7() Then Return

      'Inizializzazione dell'oggetto
      'oSql7.nomeDB = edCodaz.Text
      'oSql7.DataD = "business"
      'oSql7.LogD = "buslog"

      'Apre la modale per mostrare l'avanzamento della creazione del db
      frmProg.PassaDatiCreaDB(edAdoconnect.Text, dsDupa.Tables("AZIENDE"), strDirNewDb, edCodaz.Text, strDirNewDbLog, edExt.Text)
      frmProg.Init(oMenu, oCallParams, DittaCorrente)
      frmProg.InitEntity(oCleDupa, oSql7, "N")
      frmProg.bUnicode = bUnicode
      frmProg.ShowDialog()
      nRis = frmProg.nRis

      If nRis <> 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128230023375629906, "Creazione del Database NON avvenuta. Verificare il file Busadmintools.log per eventuali messaggi di errore."))
        Return
      End If


      'dsDupa.Tables(strTabella).Rows(dcDupa.Position)!azconnect = oSql7.Connection
      'dsDupa.Tables(strTabella).Rows(dcDupa.Position)!az_adoconnect = oSql7.AdoConnection


      'Disconnessione
      'If DisconnettiSql7() Then

      'Crea i record in tabinst e tabanaz
      If AggInstAnaz() Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128230023372629916, "Creazione azienda |" & UCase(edCodaz.Text) & "| terminata."))
        Salva(False, False)
      End If

      'End If

    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      'If bApriSql7 Then ChiudiSql7()
    End Try
  End Sub


  Public Overridable Function AggInstAnaz() As Boolean
    Dim strMod As String
    Dim strModExt As String
    Dim strTmp As String = "E"
    Dim bSvuotaTabelle As Boolean = False
    Dim bCreaContropartiteAutomatico As Boolean = False
    Try

      'Moduli
      strMod = GetSettingReg("Business", oApp.Profilo & "\ActKey", "Moduli", "")
      If strMod = "" Or Len(strMod) < 30 Then strMod = "SSSSSSSSSSSSSSSSSSSSSSSSSSSSSS"
      strModExt = GetSettingReg("Business", oApp.Profilo & "\ActKey", "ModuliExt", "")
      If strModExt = "" Or Len(strModExt) < 30 Then strModExt = "NNNNNNNNNNNNNNNNNNNNNNNNNNNNNN"

      'Valuta
      If tlbLIT.Down = True Then strTmp = "L"
      If tlbEUR.Down = True Then strTmp = "E"
      If tlbCHF.Down = True Then strTmp = "C"
      If tlbGBP.Down = True Then strTmp = "S"
      If tlbUSD.Down = True Then strTmp = "U"
      If tlbYEN.Down = True Then strTmp = "Y"

      ' Chiede se non si vogliono i dati standard, svuoto le tabelle distribuite
      If System.Windows.Forms.DialogResult.Yes = oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128230023372629909, "Svuoto le tabelle contenenti i dati STANDARD (piano dei conti, codici IVA, contropartite generiche, ...)")) Then
        bSvuotaTabelle = True
      End If
      'Chiedo se nel nuovo database quando creo un sottoconto pdc nuovo devo creare anceh la rispettiva contropartita in automatico
      'con in fatto che le anagrafiche PDC e ditta sono state unificate in un unico programma, (così come è stato unificato bnvecove)
      'questa funzionalità è stata rimossa
      bCreaContropartiteAutomatico = False
      'If System.Windows.Forms.DialogResult.Yes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128230023372629907, "Si desidera creare in automatico le contropartite PDC alla creazione dei nuovi sottoconti PDC?")) Then
      '  bCreaContropartiteAutomatico = True
      'End If

      'Chiamata per inizializzazione azienda
      Dim strTmp1 As String = NTSCStr(CType(dcDupa.Current, DataRowView).Row!az_adoconnect)
      If strTmp1.ToUpper.IndexOf("SERVER") = -1 Then Decodifica(strTmp1)
      Return oCleDupa.AggInstAnaz(strTmp1, strTmp, strMod, strModExt, edDesaz.Text, bSvuotaTabelle, bCreaContropartiteAutomatico)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function


  Public Overridable Sub cmdCopia_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopia.Click
    Try
      Return

    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try

  End Sub


  Public Overridable Sub cmdGestBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGestBack.Click
    'Dim bApriSql7 As Boolean = False
    Dim frmBack As FRM__BACK = Nothing
    Dim oCallP As New CLE__CLDP
    Dim strAdoconnect As String = ""
    Try
      'bApriSql7 = ApriSql7()
      'If Not bApriSql7 Then Return

      ''Inizializzazione parametri di connessione del'oggetto
      'oSql7.Server = oCleDupa.EstraiParametroDaConnectionString(IIf(edAdoconnect.Text = "", oApp.PrcConnect, edAdoconnect.Text), "SERVER")
      'If InStr(IIf(edAdoconnect.Text = "", oApp.PrcConnect, edAdoconnect.Text), "Trusted_Connection") > 0 Then
      '  oSql7.Integrated = True
      '  oSql7.Login = ""
      '  oSql7.Password = ""
      'Else
      '  oSql7.Integrated = False
      '  oSql7.Login = oCleDupa.EstraiParametroDaConnectionString(IIf(edAdoconnect.Text = "", oApp.PrcConnect, edAdoconnect.Text), "UID")
      '  oSql7.Password = oCleDupa.EstraiParametroDaConnectionString(IIf(edAdoconnect.Text = "", oApp.PrcConnect, edAdoconnect.Text), "PWD")
      'End If
      'oSql7.nomeDB = IIf(edAdoconnect.Text = "", oApp.DbAp.Nome, edCodaz.Text)

      ''Getione backup
      'oSql7.GestioneBackup()

      strAdoconnect = NTSCStr(CType(dcDupa.Current, DataRowView).Row!az_adoconnect)
      If strAdoconnect.ToUpper.IndexOf("SERVER") = -1 Then Decodifica(strAdoconnect)

      oCallP = New CLE__CLDP
      'server
      oCallP.strPar1 = oCleDupa.EstraiParametroDaConnectionString(IIf(strAdoconnect = "", oApp.PrcConnect, strAdoconnect), "SERVER")
      'connessione trusted
      oCallP.bPar1 = CBool(InStr(IIf(strAdoconnect = "", oApp.PrcConnect, strAdoconnect), "Trusted_Connection") > 0)
      'user
      oCallP.strPar2 = oCleDupa.EstraiParametroDaConnectionString(IIf(strAdoconnect = "", oApp.PrcConnect, strAdoconnect), "UID")
      'pwd
      oCallP.strPar3 = oCleDupa.EstraiParametroDaConnectionString(IIf(strAdoconnect = "", oApp.PrcConnect, strAdoconnect), "PWD")
      'nome db
      oCallP.strPar4 = IIf(strAdoconnect = "", oApp.DbAp.Nome, edCodaz.Text)

      frmBack = CType(NTSNewFormModal("FRM__BACK"), FRM__BACK)
      frmBack.Init(oMenu, oCallP, DittaCorrente)
      frmBack.ShowDialog()

    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      'If bApriSql7 Then ChiudiSql7()
      If Not frmBack Is Nothing Then
        frmBack.Dispose()
        frmBack = Nothing
      End If
    End Try
  End Sub


  Public Overridable Function StatoNuovo() As Boolean
    Try
      If StatoVuoto() Then Return False
      If Not dsDupa.Tables(strTabella).Rows.Count > 0 Then Return False
      If dsDupa.Tables(strTabella).Rows(dcDupa.Position).RowState = DataRowState.Added Then Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function StatoVuoto() As Boolean
    Try
      If dsDupa Is Nothing Then Return True
      If dsDupa.Tables(strTabella) Is Nothing Then Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function ApriSql7() As Boolean
    Try
      'Configurazione dell'oggetto
      'oSql7 = CreateObject("BS__SQL7.CLS__SQL7")
      'oSql7.Init(oApp.Dir, oApp.RptDir, oApp.PrcConnect, oApp.PrcExt, oApp.PrcDir, oApp.PrcDataType, oApp.PrcPrefix, oApp.DbAp.Tipo, oApp.CmbConnect, oApp.CmbExt, oApp.CmbDir, oApp.CmbDataType, oApp.CmbPrefix, oApp.DbCmb.Tipo)
      'oSql7.InitExt(oApp.User.Nome, oApp.User.Pwd, oApp.Db.Nome, oApp.Db.Ditta, "", oApp.Db.Connect, "", oApp.Db.Tipo)
      'oSql7.IsCalling = "BS__DUPA"
      'oSql7.Comando = oApp.Profilo

      'Apertura delle connessioni dei database COMBO e ARCPROC necessari al funzionamento dell'oggetto 
      'oSql7.ApriDatabaseNET()

      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      ChiudiSql7()
    End Try
  End Function
  Public Overridable Function ChiudiSql7() As Boolean
    Try
      'If Not oSql7 Is Nothing Then
      'oSql7.ChiudiDatabaseNET()
      'oSql7 = Nothing
      'End If

      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function ConnettiSql7() As Boolean
    Dim nRis As Integer = 0
    Dim bMostraConetti As Boolean = False
    Try
      ''Connessione
      'Do
      '  oSql7.Server = oCleDupa.EstraiParametroDaConnectionString(IIf(edAdoconnect.Text = "", oApp.PrcConnect, edAdoconnect.Text), "SERVER")
      '  If InStr(IIf(edAdoconnect.Text = "", oApp.PrcConnect, edAdoconnect.Text), "Trusted_Connection") > 0 Then
      '    oSql7.Integrated = True
      '    oSql7.Login = ""
      '    oSql7.Password = ""
      '  Else
      '    oSql7.Integrated = False
      '    oSql7.Login = oCleDupa.EstraiParametroDaConnectionString(IIf(edAdoconnect.Text = "", oApp.PrcConnect, edAdoconnect.Text), "UID")
      '    oSql7.Password = oCleDupa.EstraiParametroDaConnectionString(IIf(edAdoconnect.Text = "", oApp.PrcConnect, edAdoconnect.Text), "PWD")
      '  End If
      '  oSql7.nomeDB = edCodaz.Text
      '  nRis = oSql7.Connetti(bMostraConetti)
      '  If nRis = 1 Then Return False
      '  If nRis = 0 Then
      '    If Not oSql7.Connected Then Return False
      '  End If
      '  bMostraConetti = True
      'Loop Until nRis = -1

      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function DisconnettiSql7() As Boolean
    Dim nRis As Integer = 0
    Try
      'nRis = oSql7.Disconnetti
      ''Disconnessione
      'If nRis <> 0 Then
      '  oApp.MsgBoxErr(oApp.Tr(Me, 128230023372629732, "Errore nella disconnessione dal database."))
      '  Return False
      'Else
      '  oApp.MsgBoxInfo(oApp.Tr(Me, 128230023372629733, "Operazione Terminata Correttamente."))
      'End If

      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Sub cmdCreaDSN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreaDSN.Click
    Dim strAdoConnect As String = ""
    Try
      strAdoConnect = NTSCStr(CType(dcDupa.Current, DataRowView).Row!az_adoconnect)
      If strAdoConnect.ToUpper.IndexOf("SERVER") = -1 Then Decodifica(strAdoConnect)

      Select Case cbSubdatatype.SelectedValue
        Case "SQLServer7"
          Dim nRet As Integer = 0
          nRet = oCleDupa.CreateSQLServerSystemDSN(edCodaz.Text, oCleDupa.EstraiParametroDaConnectionString(IIf(strAdoConnect = "", oApp.PrcConnect, strAdoConnect), "SERVER"), edCodaz.Text, CBool(IIf(InStr(strAdoConnect.ToLower, "trusted_connection") > 0, True, False)))
          If nRet = 0 Then Return
          'If CBool(IIf(InStr(strAdoConnect.ToLower, "trusted_connection") > 0, True, False)) Then
          '  dsDupa.Tables(strTabella).Rows(dcDupa.Position)!azconnect = "ODBC;Driver={SQL Server};Server=" & oCleDupa.EstraiParametroDaConnectionString(IIf(strAdoConnect = "", oApp.PrcConnect, strAdoConnect), "SERVER") & _
          '   ";Database=" & edCodaz.Text & _
          '   ";Trusted_Connection=Yes;LANGUAGE=us_english;APP=Business;"
          'Else
          '  dsDupa.Tables(strTabella).Rows(dcDupa.Position)!azconnect = "ODBC;Driver={SQL Server};Server=" & oCleDupa.EstraiParametroDaConnectionString(IIf(strAdoConnect = "", oApp.PrcConnect, strAdoConnect), "SERVER") & _
          '   ";Database=" & edCodaz.Text & _
          '   ";UID=" & oCleDupa.EstraiParametroDaConnectionString(IIf(strAdoConnect = "", oApp.PrcConnect, strAdoConnect), "UID") & _
          '   ";pwd=" & oCleDupa.EstraiParametroDaConnectionString(IIf(strAdoConnect = "", oApp.PrcConnect, strAdoConnect), "PWD") & _
          '   ";LANGUAGE=us_english;APP=Business;"
          'End If
          If oCleDupa.IsBusHKLM And nRet = 1 Then
            oApp.MsgBoxInfo(oApp.Tr(Me, 129089762963759765, "DSN 'di sistema' creato correttamente."))
          Else
            'nret = 3: dovevo crearlo di systema ma si è verificato un errore e l'ho creato utente
            oApp.MsgBoxInfo(oApp.Tr(Me, 129089762977509765, "DSN 'utente' creato correttamente."))
          End If
        Case "(Nessuno)"
          oCleDupa.CreateAccessSystemDSN(edCodaz.Text, oApp.Dir & "\" & edCodaz.Text & ".MDB", "", "")
          oApp.MsgBoxInfo(oApp.Tr(Me, 128230023372649902, "Operazione eseguita correttamente."))
        Case Else
          oApp.MsgBoxErr(oApp.Tr(Me, 128230023372629902, "Operazione NON disponibile per questo tipo di Database."))
          Return
      End Select
      Salva(False, False)

    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbEuro_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEUR.ItemClick, tlbEUR.ItemClick
    tlbLIT.Down = False
    tlbCHF.Down = False
    tlbGBP.Down = False
    tlbUSD.Down = False
    tlbYEN.Down = False
  End Sub
  Public Overridable Sub tlbLireItaliane_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbLIT.ItemClick, tlbLIT.ItemClick
    tlbEUR.Down = False
    tlbCHF.Down = False
    tlbGBP.Down = False
    tlbUSD.Down = False
    tlbYEN.Down = False
  End Sub
  Public Overridable Sub tlbFrancoSvizzero_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCHF.ItemClick, tlbCHF.ItemClick
    tlbEUR.Down = False
    tlbLIT.Down = False
    tlbGBP.Down = False
    tlbUSD.Down = False
    tlbYEN.Down = False
  End Sub
  Public Overridable Sub tlbLireSterlina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGBP.ItemClick, tlbGBP.ItemClick
    tlbEUR.Down = False
    tlbLIT.Down = False
    tlbCHF.Down = False
    tlbUSD.Down = False
    tlbYEN.Down = False
  End Sub
  Public Overridable Sub tlbDollaro_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbUSD.ItemClick, tlbUSD.ItemClick
    tlbEUR.Down = False
    tlbLIT.Down = False
    tlbCHF.Down = False
    tlbGBP.Down = False
    tlbYEN.Down = False
  End Sub
  Public Overridable Sub tlbYen_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbYEN.ItemClick
    tlbEUR.Down = False
    tlbLIT.Down = False
    tlbCHF.Down = False
    tlbGBP.Down = False
    tlbUSD.Down = False
  End Sub

  Public Overridable Sub cmdCreaAllegate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreaAllegate.Click
    Try
      'DA NET 2013 NON SERVONO PIU', per cui per crearle, se proprio uno vuole, deve passare per BusSqlMan.exe

    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Function CreaTabelleAllegate() As Boolean
    Try
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Sub cmdRicreaSP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRicreaSP.Click
    Try
      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 129763716246050940, "ATTENZIONE: questa funzione deve essere utilizzata mentre nessun operatore lavora sul database selezionato. Proseguo?")) <> Windows.Forms.DialogResult.Yes Then Return

      Me.Cursor = Cursors.WaitCursor

      Dim strTmp As String = NTSCStr(CType(dcDupa.Current, DataRowView).Row!az_adoconnect)
      If strTmp.ToUpper.IndexOf("SERVER") = -1 Then Decodifica(strTmp)
      oCleDupa.RicreaStoredProcedure(strTmp, False)
      Me.Cursor = Cursors.Default
      oApp.MsgBoxInfo(oApp.Tr(Me, 129763713574618225, "Elaborazione completata"))

    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub tlbRicreaSPArcproc_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRicreaSPArcproc.ItemClick
    Try
      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 129024303331247618, "ATTENZIONE: questa funzione deve essere utilizzata mentre nessun operatore lavora sull'archivio procedura corrente. Proseguo?")) <> Windows.Forms.DialogResult.Yes Then Return

      Me.Cursor = Cursors.WaitCursor

      oCleDupa.RicreaStoredProcedure(oApp.DbAp.Connect, True)
      Me.Cursor = Cursors.Default
      oApp.MsgBoxInfo(oApp.Tr(Me, 129763715216193123, "Elaborazione completata"))

    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Function ConnettiArcprocSql7() As Boolean
    Dim nRis As Integer = 0
    Dim bMostraConetti As Boolean = False
    Try
      'Connessione
      'Do
      '  oSql7.Server = oCleDupa.EstraiParametroDaConnectionString(oApp.PrcConnect, "SERVER")
      '  If InStr(oApp.PrcConnect, "Trusted_Connection") > 0 Then
      '    oSql7.Integrated = True
      '    oSql7.Login = ""
      '    oSql7.Password = ""
      '  Else
      '    oSql7.Integrated = False
      '    oSql7.Login = oCleDupa.EstraiParametroDaConnectionString(oApp.PrcConnect, "UID")
      '    oSql7.Password = oCleDupa.EstraiParametroDaConnectionString(oApp.PrcConnect, "PWD")
      '  End If
      '  oSql7.nomeDB = oApp.DbAp.Nome
      '  nRis = oSql7.Connetti(bMostraConetti)
      '  If nRis = 1 Then Return False
      '  If nRis = 0 Then
      '    If Not oSql7.Connected Then Return False
      '  End If
      '  bMostraConetti = True
      'Loop Until nRis = -1

      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function CancellaDatabase() As Boolean
    Try
      Select Case cbSubdatatype.SelectedValue
        Case "SQLServer7"
          Me.Cursor = Cursors.WaitCursor

          Dim strTmp As String = NTSCStr(CType(dcDupa.Current, DataRowView).Row!az_adoconnect)
          If strTmp.ToUpper.IndexOf("SERVER") = -1 Then Decodifica(strTmp)
          If Not oCleDupa.CancellaDb(strTmp) Then Return False

          oCleDupa.DeleteAccessSystemDSN(edCodaz.Text, oApp.Dir & "\" & edCodaz.Text & ".MDB", "", "")

          oApp.MsgBoxInfo(oApp.Tr(Me, 129778475227556241, "Database cancellato correttamente"))

          'cancello le tabelle allegate, se presenti
          If System.IO.File.Exists(oApp.Dir & "\" & edCodaz.Text & ".MDB") Then
            System.IO.File.Delete(oApp.Dir & "\" & edCodaz.Text & ".MDB")
            oApp.MsgBoxInfo(oApp.Tr(Me, 129778474030270134, "Tabelle allegate al database cancellate correttamente"))
          End If

        Case "(Nessuno)"
          If Len(Dir(oApp.Dir & "\" & edCodaz.Text & ".MDB")) > 0 Then
            Kill(oApp.Dir & "\" & edCodaz.Text & ".MDB")
          Else
            oApp.MsgBoxErr(oApp.Tr(Me, 12823002337269912, "Nessun database da eliminare"))
          End If

          oCleDupa.DeleteAccessSystemDSN(edCodaz.Text, oApp.Dir & "\" & edCodaz.Text & ".MDB", "", "")

        Case Else
          oApp.MsgBoxErr(oApp.Tr(Me, 12823002337269913, "Operazione NON disponibile per questo tipo di Database."))
          Return False
      End Select

      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Function

  Public Overridable Sub cmdImportaDati_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImportaDati.Click
    Dim oCallParamTmp As New CLE__CLDP
    Dim frmAlta As FRM__IMPO = Nothing
    Try
      frmAlta = CType(NTSNewFormModal("FRM__IMPO"), FRM__IMPO)
      oCallParamTmp.strPar1 = edCodaz.Text
      'Apre la modale
      frmAlta.Init(oMenu, oCallParamTmp, DittaCorrente)
      frmAlta.InitEntity(oCleDupa)
      frmAlta.ShowDialog()
      frmAlta.Dispose()
      frmAlta = Nothing
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edAdoconnect_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAdoconnect.Enter
    Try

    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edAdoconnect_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAdoconnect.Validated
    Try
      Dim strTmp As String = NTSCStr(CType(dcDupa.Current, DataRowView).Row!az_adoconnect)
      If strTmp.ToUpper.IndexOf("SERVER") = -1 Then Decodifica(strTmp)
      edConnect.NTSTextDB = "ODBC;Driver={SQL Server};" & strTmp
      If edAdoconnect.Text.ToUpper.IndexOf("SERVER") = -1 Then
        strTmp = edConnect.Text
        Codifica(strTmp)
        edConnect.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

End Class


