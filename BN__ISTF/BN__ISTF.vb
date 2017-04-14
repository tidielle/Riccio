Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__ISTF
  Public oCleIstf As CLE__ISTF
  Public oCallParams As CLE__CLDP

  Public bGestAnaext As Boolean
  Public bOnLoading As Boolean = False

  Public dRiga As Decimal = 0

  Public dsIstf As DataSet
  Public dsStca As DataSet
  Public dttStax As New DataTable

  Public dcIstf As BindingSource = New BindingSource
  Public dcStca As BindingSource = New BindingSource

#Region "Moduli"
  Private Moduli_P As Integer = CLN__STD.bsModAll
  Private ModuliExt_P As Integer = CLN__STD.bsModExtAll
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

#Region "Dichiarazione controlli"
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
  Public WithEvents tlbDesLingua As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCalcolaScadenze As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
#End Region

#Region "Inizializzazione"
  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    '----------------------------------------------------------------------------------------------------------------
    oMenu = Menu
    oApp = oMenu.App
    oCallParams = Param
    If Ditta <> "" Then DittaCorrente = Ditta Else DittaCorrente = oApp.Ditta
    '----------------------------------------------------------------------------------------------------------------
    Me.GctlTipoDoc = ""
    '----------------------------------------------------------------------------------------------------------------
    InitializeComponent()
    Me.MinimumSize = Me.Size
    '----------------------------------------------------------------------------------------------------------------
    '--- Creo e attivo l'entity e inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    '----------------------------------------------------------------------------------------------------------------
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__ISTF", "BE__ISTF", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 127791222149375000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleIstf = CType(oTmp, CLE__ISTF)
    '----------------------------------------------------------------------------------------------------------------
    bRemoting = Menu.Remoting("BN__ISTF", strRemoteServer, strRemotePort)
    AddHandler oCleIstf.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleIstf.Init(oApp, NTSScript, oMenu.oCleComm, "PARSTAF", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    '----------------------------------------------------------------------------------------------------------------
    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__ISTF))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecordNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecordSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecordRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecordCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrecedente = New NTSInformatica.NTSBarButtonItem
    Me.tlbSuccessivo = New NTSInformatica.NTSBarButtonItem
    Me.tlbUltimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.tlbDesLingua = New NTSInformatica.NTSBarButtonItem
    Me.tlbCalcolaScadenze = New NTSInformatica.NTSBarButtonItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.lbPf_codform = New NTSInformatica.NTSLabel
    Me.lbPf_titstam = New NTSInformatica.NTSLabel
    Me.lbPf_codquery = New NTSInformatica.NTSLabel
    Me.lbPf_maxcolo = New NTSInformatica.NTSLabel
    Me.lbPf_order = New NTSInformatica.NTSLabel
    Me.lbSelezione = New NTSInformatica.NTSLabel
    Me.edPf_codform = New NTSInformatica.NTSTextBoxNum
    Me.edPf_titstam = New NTSInformatica.NTSTextBoxStr
    Me.edPf_maxcolo = New NTSInformatica.NTSTextBoxNum
    Me.cbPf_codquery = New NTSInformatica.NTSComboBox
    Me.cbPf_order = New NTSInformatica.NTSComboBox
    Me.ckPf_statot = New NTSInformatica.NTSCheckBox
    Me.grStca = New NTSInformatica.NTSGrid
    Me.grvStca = New NTSInformatica.NTSGridView
    Me.pfc_riga = New NTSInformatica.NTSGridColumn
    Me.pfc_nomcampo = New NTSInformatica.NTSGridColumn
    Me.pfc_size = New NTSInformatica.NTSGridColumn
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.cmdFiltri = New NTSInformatica.NTSButton
    Me.pnFill = New NTSInformatica.NTSPanel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPf_codform.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPf_titstam.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPf_maxcolo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPf_codquery.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPf_order.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckPf_statot.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grStca, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvStca, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.pnFill, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFill.SuspendLayout()
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
    'NtsBarManager1
    '
    Me.NtsBarManager1.AllowCustomization = False
    Me.NtsBarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.tlbMain})
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlTop)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlBottom)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlLeft)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlRight)
    Me.NtsBarManager1.Form = Me
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbGuida, Me.tlbEsci, Me.tlbUltimo, Me.tlbDesLingua, Me.tlbCalcolaScadenze, Me.tlbImpostaStampante, Me.tlbRecordNuovo, Me.tlbRecordSalva, Me.tlbRecordRipristina, Me.tlbRecordCancella})
    Me.NtsBarManager1.MaxItemId = 30
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordNuovo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbZoom.Id = 4
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbRecordNuovo
    '
    Me.tlbRecordNuovo.Caption = "Nuova riga"
    Me.tlbRecordNuovo.Glyph = CType(resources.GetObject("tlbRecordNuovo.Glyph"), System.Drawing.Image)
    Me.tlbRecordNuovo.Id = 26
    Me.tlbRecordNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F2))
    Me.tlbRecordNuovo.Name = "tlbRecordNuovo"
    Me.tlbRecordNuovo.Visible = True
    '
    'tlbRecordSalva
    '
    Me.tlbRecordSalva.Caption = "Salva riga"
    Me.tlbRecordSalva.Glyph = CType(resources.GetObject("tlbRecordSalva.Glyph"), System.Drawing.Image)
    Me.tlbRecordSalva.Id = 27
    Me.tlbRecordSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F9))
    Me.tlbRecordSalva.Name = "tlbRecordSalva"
    Me.tlbRecordSalva.Visible = True
    '
    'tlbRecordRipristina
    '
    Me.tlbRecordRipristina.Caption = "Ripristina riga"
    Me.tlbRecordRipristina.Glyph = CType(resources.GetObject("tlbRecordRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRecordRipristina.Id = 28
    Me.tlbRecordRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F8))
    Me.tlbRecordRipristina.Name = "tlbRecordRipristina"
    Me.tlbRecordRipristina.Visible = True
    '
    'tlbRecordCancella
    '
    Me.tlbRecordCancella.Caption = "Cancella riga"
    Me.tlbRecordCancella.Glyph = CType(resources.GetObject("tlbRecordCancella.Glyph"), System.Drawing.Image)
    Me.tlbRecordCancella.Id = 29
    Me.tlbRecordCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F4))
    Me.tlbRecordCancella.Name = "tlbRecordCancella"
    Me.tlbRecordCancella.Visible = True
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
    'tlbDesLingua
    '
    Me.tlbDesLingua.Caption = "Descrizioni in lingua"
    Me.tlbDesLingua.Id = 23
    Me.tlbDesLingua.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F7))
    Me.tlbDesLingua.Name = "tlbDesLingua"
    Me.tlbDesLingua.Visible = True
    '
    'tlbCalcolaScadenze
    '
    Me.tlbCalcolaScadenze.Caption = "Calcola scadenze"
    Me.tlbCalcolaScadenze.Id = 24
    Me.tlbCalcolaScadenze.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbCalcolaScadenze.Name = "tlbCalcolaScadenze"
    Me.tlbCalcolaScadenze.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta stampante"
    Me.tlbImpostaStampante.Id = 25
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'lbPf_codform
    '
    Me.lbPf_codform.AutoSize = True
    Me.lbPf_codform.BackColor = System.Drawing.Color.Transparent
    Me.lbPf_codform.Location = New System.Drawing.Point(12, 8)
    Me.lbPf_codform.Name = "lbPf_codform"
    Me.lbPf_codform.NTSDbField = ""
    Me.lbPf_codform.Size = New System.Drawing.Size(129, 13)
    Me.lbPf_codform.TabIndex = 4
    Me.lbPf_codform.Text = "Codice formato di stampa"
    Me.lbPf_codform.Tooltip = ""
    Me.lbPf_codform.UseMnemonic = False
    '
    'lbPf_titstam
    '
    Me.lbPf_titstam.AutoSize = True
    Me.lbPf_titstam.BackColor = System.Drawing.Color.Transparent
    Me.lbPf_titstam.Location = New System.Drawing.Point(12, 31)
    Me.lbPf_titstam.Name = "lbPf_titstam"
    Me.lbPf_titstam.NTSDbField = ""
    Me.lbPf_titstam.Size = New System.Drawing.Size(96, 13)
    Me.lbPf_titstam.TabIndex = 5
    Me.lbPf_titstam.Text = "Titolo della stampa"
    Me.lbPf_titstam.Tooltip = ""
    Me.lbPf_titstam.UseMnemonic = False
    '
    'lbPf_codquery
    '
    Me.lbPf_codquery.AutoSize = True
    Me.lbPf_codquery.BackColor = System.Drawing.Color.Transparent
    Me.lbPf_codquery.Location = New System.Drawing.Point(12, 54)
    Me.lbPf_codquery.Name = "lbPf_codquery"
    Me.lbPf_codquery.NTSDbField = ""
    Me.lbPf_codquery.Size = New System.Drawing.Size(94, 13)
    Me.lbPf_codquery.TabIndex = 6
    Me.lbPf_codquery.Text = "Query di partenza"
    Me.lbPf_codquery.Tooltip = ""
    Me.lbPf_codquery.UseMnemonic = False
    '
    'lbPf_maxcolo
    '
    Me.lbPf_maxcolo.AutoSize = True
    Me.lbPf_maxcolo.BackColor = System.Drawing.Color.Transparent
    Me.lbPf_maxcolo.Location = New System.Drawing.Point(12, 77)
    Me.lbPf_maxcolo.Name = "lbPf_maxcolo"
    Me.lbPf_maxcolo.NTSDbField = ""
    Me.lbPf_maxcolo.Size = New System.Drawing.Size(113, 13)
    Me.lbPf_maxcolo.TabIndex = 7
    Me.lbPf_maxcolo.Text = "N° massimo di colonne"
    Me.lbPf_maxcolo.Tooltip = ""
    Me.lbPf_maxcolo.UseMnemonic = False
    '
    'lbPf_order
    '
    Me.lbPf_order.AutoSize = True
    Me.lbPf_order.BackColor = System.Drawing.Color.Transparent
    Me.lbPf_order.Location = New System.Drawing.Point(12, 100)
    Me.lbPf_order.Name = "lbPf_order"
    Me.lbPf_order.NTSDbField = ""
    Me.lbPf_order.Size = New System.Drawing.Size(88, 13)
    Me.lbPf_order.TabIndex = 8
    Me.lbPf_order.Text = "Ordine di stampa"
    Me.lbPf_order.Tooltip = ""
    Me.lbPf_order.UseMnemonic = False
    '
    'lbSelezione
    '
    Me.lbSelezione.AutoSize = True
    Me.lbSelezione.BackColor = System.Drawing.Color.Transparent
    Me.lbSelezione.Location = New System.Drawing.Point(4, 156)
    Me.lbSelezione.Name = "lbSelezione"
    Me.lbSelezione.NTSDbField = ""
    Me.lbSelezione.Size = New System.Drawing.Size(145, 13)
    Me.lbSelezione.TabIndex = 9
    Me.lbSelezione.Text = "Selezione campi da stampare"
    Me.lbSelezione.Tooltip = ""
    Me.lbSelezione.UseMnemonic = False
    '
    'edPf_codform
    '
    Me.edPf_codform.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPf_codform.Location = New System.Drawing.Point(190, 5)
    Me.edPf_codform.Name = "edPf_codform"
    Me.edPf_codform.NTSDbField = ""
    Me.edPf_codform.NTSFormat = "0"
    Me.edPf_codform.NTSForzaVisZoom = False
    Me.edPf_codform.NTSOldValue = ""
    Me.edPf_codform.Properties.Appearance.Options.UseTextOptions = True
    Me.edPf_codform.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPf_codform.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPf_codform.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPf_codform.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPf_codform.Properties.MaxLength = 65536
    Me.edPf_codform.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPf_codform.Size = New System.Drawing.Size(44, 20)
    Me.edPf_codform.TabIndex = 10
    '
    'edPf_titstam
    '
    Me.edPf_titstam.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPf_titstam.Location = New System.Drawing.Point(190, 28)
    Me.edPf_titstam.Name = "edPf_titstam"
    Me.edPf_titstam.NTSDbField = ""
    Me.edPf_titstam.NTSForzaVisZoom = False
    Me.edPf_titstam.NTSOldValue = ""
    Me.edPf_titstam.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPf_titstam.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPf_titstam.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPf_titstam.Properties.MaxLength = 65536
    Me.edPf_titstam.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPf_titstam.Size = New System.Drawing.Size(368, 20)
    Me.edPf_titstam.TabIndex = 11
    '
    'edPf_maxcolo
    '
    Me.edPf_maxcolo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPf_maxcolo.Location = New System.Drawing.Point(190, 74)
    Me.edPf_maxcolo.Name = "edPf_maxcolo"
    Me.edPf_maxcolo.NTSDbField = ""
    Me.edPf_maxcolo.NTSFormat = "0"
    Me.edPf_maxcolo.NTSForzaVisZoom = False
    Me.edPf_maxcolo.NTSOldValue = ""
    Me.edPf_maxcolo.Properties.Appearance.Options.UseTextOptions = True
    Me.edPf_maxcolo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPf_maxcolo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPf_maxcolo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPf_maxcolo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPf_maxcolo.Properties.MaxLength = 65536
    Me.edPf_maxcolo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPf_maxcolo.Size = New System.Drawing.Size(44, 20)
    Me.edPf_maxcolo.TabIndex = 12
    '
    'cbPf_codquery
    '
    Me.cbPf_codquery.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPf_codquery.DataSource = Nothing
    Me.cbPf_codquery.DisplayMember = ""
    Me.cbPf_codquery.Location = New System.Drawing.Point(190, 51)
    Me.cbPf_codquery.Name = "cbPf_codquery"
    Me.cbPf_codquery.NTSDbField = ""
    Me.cbPf_codquery.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPf_codquery.Properties.DropDownRows = 30
    Me.cbPf_codquery.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPf_codquery.SelectedValue = ""
    Me.cbPf_codquery.Size = New System.Drawing.Size(368, 20)
    Me.cbPf_codquery.TabIndex = 13
    Me.cbPf_codquery.ValueMember = ""
    '
    'cbPf_order
    '
    Me.cbPf_order.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPf_order.DataSource = Nothing
    Me.cbPf_order.DisplayMember = ""
    Me.cbPf_order.Location = New System.Drawing.Point(190, 97)
    Me.cbPf_order.Name = "cbPf_order"
    Me.cbPf_order.NTSDbField = ""
    Me.cbPf_order.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPf_order.Properties.DropDownRows = 30
    Me.cbPf_order.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPf_order.SelectedValue = ""
    Me.cbPf_order.Size = New System.Drawing.Size(235, 20)
    Me.cbPf_order.TabIndex = 14
    Me.cbPf_order.ValueMember = ""
    '
    'ckPf_statot
    '
    Me.ckPf_statot.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckPf_statot.Location = New System.Drawing.Point(15, 123)
    Me.ckPf_statot.Name = "ckPf_statot"
    Me.ckPf_statot.NTSCheckValue = "S"
    Me.ckPf_statot.NTSUnCheckValue = "N"
    Me.ckPf_statot.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPf_statot.Properties.Appearance.Options.UseBackColor = True
    Me.ckPf_statot.Properties.Caption = "Stampa totali"
    Me.ckPf_statot.Size = New System.Drawing.Size(93, 19)
    Me.ckPf_statot.TabIndex = 15
    '
    'grStca
    '
    Me.grStca.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grStca.EmbeddedNavigator.Name = ""
    Me.grStca.Location = New System.Drawing.Point(0, 0)
    Me.grStca.MainView = Me.grvStca
    Me.grStca.Name = "grStca"
    Me.grStca.Size = New System.Drawing.Size(570, 261)
    Me.grStca.TabIndex = 16
    Me.grStca.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvStca})
    '
    'grvStca
    '
    Me.grvStca.ActiveFilterEnabled = False
    Me.grvStca.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.pfc_riga, Me.pfc_nomcampo, Me.pfc_size})
    Me.grvStca.Enabled = True
    Me.grvStca.GridControl = Me.grStca
    Me.grvStca.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvStca.Name = "grvStca"
    Me.grvStca.NTSAllowDelete = True
    Me.grvStca.NTSAllowInsert = True
    Me.grvStca.NTSAllowUpdate = True
    Me.grvStca.NTSMenuContext = Nothing
    Me.grvStca.OptionsCustomization.AllowRowSizing = True
    Me.grvStca.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvStca.OptionsNavigation.UseTabKey = False
    Me.grvStca.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvStca.OptionsView.ColumnAutoWidth = False
    Me.grvStca.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvStca.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvStca.OptionsView.ShowGroupPanel = False
    Me.grvStca.RowHeight = 14
    '
    'pfc_riga
    '
    Me.pfc_riga.AppearanceCell.Options.UseBackColor = True
    Me.pfc_riga.AppearanceCell.Options.UseTextOptions = True
    Me.pfc_riga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pfc_riga.Caption = "Sequenza"
    Me.pfc_riga.Enabled = True
    Me.pfc_riga.FieldName = "pfc_riga"
    Me.pfc_riga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pfc_riga.Name = "pfc_riga"
    Me.pfc_riga.NTSRepositoryComboBox = Nothing
    Me.pfc_riga.NTSRepositoryItemCheck = Nothing
    Me.pfc_riga.NTSRepositoryItemMemo = Nothing
    Me.pfc_riga.NTSRepositoryItemText = Nothing
    Me.pfc_riga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pfc_riga.OptionsFilter.AllowFilter = False
    Me.pfc_riga.Visible = True
    Me.pfc_riga.VisibleIndex = 0
    '
    'pfc_nomcampo
    '
    Me.pfc_nomcampo.AppearanceCell.Options.UseBackColor = True
    Me.pfc_nomcampo.AppearanceCell.Options.UseTextOptions = True
    Me.pfc_nomcampo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pfc_nomcampo.Caption = "Nome campo"
    Me.pfc_nomcampo.Enabled = True
    Me.pfc_nomcampo.FieldName = "pfc_nomcampo"
    Me.pfc_nomcampo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pfc_nomcampo.Name = "pfc_nomcampo"
    Me.pfc_nomcampo.NTSRepositoryComboBox = Nothing
    Me.pfc_nomcampo.NTSRepositoryItemCheck = Nothing
    Me.pfc_nomcampo.NTSRepositoryItemMemo = Nothing
    Me.pfc_nomcampo.NTSRepositoryItemText = Nothing
    Me.pfc_nomcampo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pfc_nomcampo.OptionsFilter.AllowFilter = False
    Me.pfc_nomcampo.Visible = True
    Me.pfc_nomcampo.VisibleIndex = 1
    '
    'pfc_size
    '
    Me.pfc_size.AppearanceCell.Options.UseBackColor = True
    Me.pfc_size.AppearanceCell.Options.UseTextOptions = True
    Me.pfc_size.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pfc_size.Caption = "N° colonne"
    Me.pfc_size.Enabled = True
    Me.pfc_size.FieldName = "pfc_size"
    Me.pfc_size.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pfc_size.Name = "pfc_size"
    Me.pfc_size.NTSRepositoryComboBox = Nothing
    Me.pfc_size.NTSRepositoryItemCheck = Nothing
    Me.pfc_size.NTSRepositoryItemMemo = Nothing
    Me.pfc_size.NTSRepositoryItemText = Nothing
    Me.pfc_size.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pfc_size.OptionsFilter.AllowFilter = False
    Me.pfc_size.Visible = True
    Me.pfc_size.VisibleIndex = 2
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.cmdFiltri)
    Me.pnTop.Controls.Add(Me.edPf_codform)
    Me.pnTop.Controls.Add(Me.lbPf_codform)
    Me.pnTop.Controls.Add(Me.ckPf_statot)
    Me.pnTop.Controls.Add(Me.lbPf_titstam)
    Me.pnTop.Controls.Add(Me.cbPf_order)
    Me.pnTop.Controls.Add(Me.lbPf_codquery)
    Me.pnTop.Controls.Add(Me.cbPf_codquery)
    Me.pnTop.Controls.Add(Me.lbPf_maxcolo)
    Me.pnTop.Controls.Add(Me.edPf_maxcolo)
    Me.pnTop.Controls.Add(Me.lbPf_order)
    Me.pnTop.Controls.Add(Me.edPf_titstam)
    Me.pnTop.Controls.Add(Me.lbSelezione)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.Size = New System.Drawing.Size(570, 173)
    Me.pnTop.TabIndex = 17
    Me.pnTop.Text = "NtsPanel1"
    '
    'cmdFiltri
    '
    Me.cmdFiltri.ImageText = ""
    Me.cmdFiltri.Location = New System.Drawing.Point(432, 95)
    Me.cmdFiltri.Name = "cmdFiltri"
    Me.cmdFiltri.Size = New System.Drawing.Size(126, 23)
    Me.cmdFiltri.TabIndex = 16
    Me.cmdFiltri.Text = "Imposta filtri"
    '
    'pnFill
    '
    Me.pnFill.AllowDrop = True
    Me.pnFill.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnFill.Appearance.Options.UseBackColor = True
    Me.pnFill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnFill.Controls.Add(Me.grStca)
    Me.pnFill.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnFill.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnFill.Location = New System.Drawing.Point(0, 203)
    Me.pnFill.Name = "pnFill"
    Me.pnFill.Size = New System.Drawing.Size(570, 261)
    Me.pnFill.TabIndex = 18
    Me.pnFill.Text = "NtsPanel1"
    '
    'FRM__ISTF
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(570, 464)
    Me.Controls.Add(Me.pnFill)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRM__ISTF"
    Me.Text = "IMPOSTAZIONE STAMPE PARAMETRICHE PREDEFINITE"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPf_codform.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPf_titstam.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPf_maxcolo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPf_codquery.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPf_order.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckPf_statot.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grStca, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvStca, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.pnFill, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFill.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, False)
    '----------------------------------------------------------------------------------------------------------------
    Dim i As Integer = 0

    Try
      '--------------------------------------------------------------------------------------------------------------
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbRecordNuovo.GlyphPath = (oApp.ChildImageDir & "\recnew.gif")
        tlbRecordSalva.GlyphPath = (oApp.ChildImageDir & "\recagg.gif")
        tlbRecordRipristina.GlyphPath = (oApp.ChildImageDir & "\recrestore.gif")
        tlbRecordCancella.GlyphPath = (oApp.ChildImageDir & "\recdelete.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
      End Try
      '--------------------------------------------------------------------------------------------------------------
      tlbMain.NTSSetToolTip()
      '--------------------------------------------------------------------------------------------------------------
      edPf_codform.NTSSetParam(oMenu, oApp.Tr(Me, 129314595014429131, "Codice formato di stampa"), "0", 4, 0, 9999)
      edPf_maxcolo.NTSSetParam(oMenu, oApp.Tr(Me, 129314595015679123, "N° massimo di colonne"), "0", 3, 0, 255)
      edPf_titstam.NTSSetParam(oMenu, oApp.Tr(Me, 129314595015991621, "Titolo della stampa"), 40, True)
      cbPf_order.NTSSetParam(oApp.Tr(Me, 129314595015054127, "Ordine di stampa"))
      cbPf_codquery.NTSSetParam(oApp.Tr(Me, 129314595015366625, "Query di partenza"))
      ckPf_statot.NTSSetParam(oMenu, oApp.Tr(Me, 129314595014741629, "Stampa totali"), "S", "N")
      '--------------------------------------------------------------------------------------------------------------
      grvStca.NTSSetParam(oMenu, oApp.Tr(Me, 129314594995679251, "Selezione campi da stampare"))
      pfc_riga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129322308282542901, "Sequenza"), "#,##0.00", 15, 1, 999999999)
      pfc_size.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129322308282855401, "N° colonne"), "0", 4, 0, 9999)
      pfc_nomcampo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129322311871605401, "Nome campo"), 0, True)
      '--------------------------------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, True)
    '----------------------------------------------------------------------------------------------------------------
  End Sub
#End Region
  
#Region "Eventi di Form"
  Public Overridable Sub FRM__ISTF_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '--------------------------------------------------------------------------------------------------------------
      bOnLoading = True
      '--------------------------------------------------------------------------------------------------------------
      CaricaCombo()
      '--------------------------------------------------------------------------------------------------------------
      CreaCampiQF()
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      bGestAnaext = CBool(oMenu.GetSettingBus("BS--CLIE", "OPZIONI", ".", "GestAnaExt", "0", " ", "0"))
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleIstf.Apri(DittaCorrente, dsIstf) Then Me.Close()
      dcIstf.DataSource = dsIstf.Tables("PARSTAF")
      dsIstf.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      Bindcontrols()
      '--------------------------------------------------------------------------------------------------------------
      GetCodiceFormato()
      '--------------------------------------------------------------------------------------------------------------
      ConfiguraForm()
      '--------------------------------------------------------------------------------------------------------------
      If dsIstf.Tables("PARSTAF").Rows.Count = 0 Then tlbNuovo_ItemClick(tlbNuovo, Nothing)
      dcIstf.ResetBindings(False)
      dcIstf.MoveFirst()
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      bOnLoading = False
    End Try
  End Sub

  Public Overridable Sub FRM__ISTF_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Dim dtrRow As DataRow() = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      If Salva(False, True) = False Then e.Cancel = True
      '--------------------------------------------------------------------------------------------------------------
      If SalvaRiga(False) = False Then e.Cancel = True
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edPf_codform.Text) <> 0 Then
        If dsStca.Tables("PARSTCA").Rows.Count = 0 Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 129723948215495002, "Attenzione!" & vbCrLf & _
            "Selezionare almeno un campo da stampare."))
          e.Cancel = True
        Else
          '----------------------------------------------------------------------------------------------------------
          '--- Se la query di partenza è "Anagrafiche Clienti/Fornitori/Sottoconti con dati statistici/sintetici"
          '--- il Codice Conto deve essere selezionato
          '----------------------------------------------------------------------------------------------------------
          If NTSCInt(cbPf_codquery.SelectedValue) = 3 Then
            dtrRow = dsStca.Tables("PARSTCA").Select("pfc_nomcampo = 'anagra.an_conto'")
            If dtrRow.Length = 0 Then
              oApp.MsgBoxErr(oApp.Tr(Me, 129726630300800123, "Attenzione!" & vbCrLf & _
                "Per questo tipo di stampa il 'Codice Conto' deve essere indicato nella selezione."))
              e.Cancel = True
            End If
          End If
          '----------------------------------------------------------------------------------------------------------
          End If
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRM__ISTF_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      '--------------------------------------------------------------------------------------------------------------
      dcIstf.Dispose()
      dsIstf.Dispose()
      dcStca.Dispose()
      dsStca.Dispose()
      '--------------------------------------------------------------------------------------------------------------
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not Salva(False, False) Then Return
      If Not SalvaRiga(False) Then Return
      '--------------------------------------------------------------------------------------------------------------
      oCleIstf.Nuovo()
      dcIstf.MoveLast()
      '--------------------------------------------------------------------------------------------------------------
      ConfiguraForm()
      '--------------------------------------------------------------------------------------------------------------
      Me.GctlApplicaDefaultValue()
      '--------------------------------------------------------------------------------------------------------------
      GetCodiceFormato()
      '--------------------------------------------------------------------------------------------------------------
      cbPf_codquery.SelectedValue = "1"
      cbPf_order.SelectedValue = "anagra.an_conto"
      cbPf_order.NTSDbField = "anagra.an_conto"
      '--------------------------------------------------------------------------------------------------------------
      edPf_codform.Focus()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      Salva(False, False)
      SalvaRiga(False)
      '--------------------------------------------------------------------------------------------------------------
      GetCodiceFormato()
      '--------------------------------------------------------------------------------------------------------------
      ApriParstca()
      ApriParstafx()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Dim bRemovBinding As Boolean = False
    Dim dlgRes As DialogResult

    Try
      '--------------------------------------------------------------------------------------------------------------
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127791952085312500, "Cancellare l'impostazione di stampa?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No : Return
        Case Windows.Forms.DialogResult.Yes
          '----------------------------------------------------------------------------------------------------------
          oCleIstf.lPf_codform = NTSCInt(edPf_codform.Text)
          '----------------------------------------------------------------------------------------------------------
          If oCleIstf.CancellaParstca() = False Then Return
          '----------------------------------------------------------------------------------------------------------
          If Salva(True, False) = False Then Return
          '----------------------------------------------------------------------------------------------------------
          If dsIstf.Tables("PARSTAF").Rows.Count = 1 Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If
          '----------------------------------------------------------------------------------------------------------
          dcIstf.RemoveAt(dcIstf.Position)
          '----------------------------------------------------------------------------------------------------------
          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcIstf, Me)
            bRemovBinding = False
            GctlSetVisEnab(edPf_codform, False)
          Else
            edPf_codform.Enabled = False
          End If
          '----------------------------------------------------------------------------------------------------------
      End Select
      '--------------------------------------------------------------------------------------------------------------
      ConfiguraForm()
      '--------------------------------------------------------------------------------------------------------------
      GetCodiceFormato()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      If bRemovBinding Then NTSFormAddDataBinding(dcIstf, Me)
      '--------------------------------------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Dim bRemovBinding As Boolean = False

    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      Dim dlgRes As DialogResult
      If Not sender Is Nothing Then
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127791952835468750, _
          "Ripristinare le modifiche apportate all'impostazione di stampa?"))
      Else
        dlgRes = Windows.Forms.DialogResult.Yes
      End If
      '--------------------------------------------------------------------------------------------------------------
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No : Return
        Case Windows.Forms.DialogResult.Yes
          '----------------------------------------------------------------------------------------------------------
          If dsIstf.Tables("PARSTAF").Rows.Count = 1 And dsIstf.Tables("PARSTAF").Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If
          oCleIstf.Ripristina(dcIstf.Position, dcIstf.Filter)
          '----------------------------------------------------------------------------------------------------------
          If dsIstf.Tables("PARSTAF").Rows.Count <> 0 Then RecordRipristina(False)
          '----------------------------------------------------------------------------------------------------------
          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcIstf, Me)
            bRemovBinding = False
            GctlSetVisEnab(edPf_codform, False)
          End If
          '----------------------------------------------------------------------------------------------------------
      End Select
      '--------------------------------------------------------------------------------------------------------------
      GetCodiceFormato()
      '--------------------------------------------------------------------------------------------------------------
      If dsIstf.Tables("PARSTAF").Rows(0).RowState = DataRowState.Added Then cbPf_codquery.SelectedValue = "1"
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcIstf, Me)
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbRecordNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordNuovo.ItemClick
    Try
      grvStca.NTSNuovo()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbRecordSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordSalva.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      SalvaRiga(False)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbRecordRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordRipristina.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      RecordRipristina(True)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbRecordCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordCancella.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      dRiga = NTSCDec(grvStca.NTSGetCurrentDataRow!pfc_riga)
      '--------------------------------------------------------------------------------------------------------------
      If grvStca.NTSDeleteRigaCorrente(dcStca, True) = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      oCleIstf.bParstcaHasChanges = True
      '--------------------------------------------------------------------------------------------------------------
      If SalvaRiga(True) = False Then Return
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      If edPf_titstam.Focused Then
        SelezionaTitoloStampa
      Else
        NTSCallStandardZoom()
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not Salva(False, True) Then Return
      '--------------------------------------------------------------------------------------------------------------
      Me.Close()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      RecordSposta("primo")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbPrecedente_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrecedente.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      RecordSposta("precedente")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbSuccessivo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSuccessivo.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      RecordSposta("successivo")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbUltimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbUltimo.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      RecordSposta("ultimo")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi di griglia"
  Public Overridable Sub grvStca_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvStca.NTSBeforeRowUpdate
    Try
      '--------------------------------------------------------------------------------------------------------------
      If edPf_codform.Text.Trim = "" Then e.Allow = False
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edPf_codform.Text) = 0 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 129691215088347170, "Attenzione!" & vbCrLf & _
          "Indicare un codice formato di stampa valido."))
        e.Allow = False
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not grvStca.NTSGetCurrentDataRow Is Nothing Then dRiga = NTSCDec(grvStca.NTSGetCurrentDataRow!pfc_riga)
      '--------------------------------------------------------------------------------------------------------------
      If Not SalvaRiga(False) Then e.Allow = False
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub grvStca_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvStca.NTSFocusedRowChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If grvStca.NTSGetCurrentDataRow Is Nothing Then
        GctlSetVisEnab(pfc_riga, False)
      Else
        If grvStca.NTSGetCurrentDataRow.RowState = DataRowState.Added Then
          GctlSetVisEnab(pfc_riga, False)
        Else
          pfc_riga.Enabled = False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------      
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi ComboBox"
  Public Overridable Sub cbPf_codquery_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPf_codquery.SelectedIndexChanged
    Dim dttCombo As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If bOnLoading = True Then Return
      '--------------------------------------------------------------------------------------------------------------
      oCleIstf.nPf_codquery = NTSCInt(cbPf_codquery.SelectedValue)
      '--------------------------------------------------------------------------------------------------------------
      ComponiComboGriglia(dttCombo)
      ComponiComboOrder()
      '--------------------------------------------------------------------------------------------------------------
      '--- Reinizializzo il campo con il nuovo combo
      '--------------------------------------------------------------------------------------------------------------
      oCleIstf.bLoadinComboGriglia = True
      pfc_nomcampo.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129721455619757991, "Nome campo"), dttCombo, "cod", "val")
      oCleIstf.bLoadinComboGriglia = False
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cbPf_order_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPf_order.SelectedIndexChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bOnLoading = True Then Return
      '--------------------------------------------------------------------------------------------------------------
      If dsIstf.Tables("PARSTAF").Rows(dcIstf.Position).RowState = DataRowState.Deleted Then Return
      If dsIstf.Tables("PARSTAF").Rows(dcIstf.Position).RowState <> DataRowState.Added Then
        'cbPf_order.SelectedValue = NTSCStr(dsIstf.Tables("PARSTAF").Rows(dcIstf.Position)!pf_order)
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi Pulsanti"
  Public Overridable Sub cmdFiltri_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFiltri.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      If oCleIstf.bHasChanges = True Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 129981437736101318, "Attenzione!" & vbCrLf & _
          "Salvare l'impostazione prima di passare alla selezione dei filtri."))
        Return
      End If
      'If Not Salva(False, False) Then Return
      '--------------------------------------------------------------------------------------------------------------
      Filtri_ANAGRA()
      Filtri_ARTICO()
      '--------------------------------------------------------------------------------------------------------------
      oCleIstf.bHasChanges = True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Function ApriParstca() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleIstf.ApriParstca(NTSCInt(edPf_codform.Text), dsStca) Then Return False
      '--------------------------------------------------------------------------------------------------------------
      dcStca = New BindingSource
      dcStca.DataSource = dsStca.Tables("PARSTCA")
      grStca.DataSource = dcStca
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function ApriParstafx() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCleIstf.ApriParstafx(NTSCInt(edPf_codform.Text), dttStax)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Sub Bindcontrols()
    Try
      '--------------------------------------------------------------------------------------------------------------
      NTSFormClearDataBinding(Me)
      '--------------------------------------------------------------------------------------------------------------
      edPf_codform.NTSDbField = "PARSTAF.pf_codform"
      ckPf_statot.NTSText.NTSDbField = "PARSTAF.pf_statot"
      cbPf_order.NTSDbField = "PARSTAF.pf_order"
      cbPf_codquery.NTSDbField = "PARSTAF.pf_codquery"
      edPf_maxcolo.NTSDbField = "PARSTAF.pf_maxcolo"
      edPf_titstam.NTSDbField = "PARSTAF.pf_titstam"
      '--------------------------------------------------------------------------------------------------------------
      NTSFormAddDataBinding(dcIstf, Me)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub CaricaCombo()
    Dim dttCodQuery As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      dttCodQuery.Columns.Add("cod", GetType(Integer))
      dttCodQuery.Columns.Add("val", GetType(String))
      dttCodQuery.Rows.Add(New Object() {1, "Anagrafiche Clienti/Forn/Sottoconti"})
      dttCodQuery.Rows.Add(New Object() {2, "Anagrafiche Clienti/Forn/Sottoconti con dati contabili"})
      dttCodQuery.Rows.Add(New Object() {3, "Anagrafiche Clienti/Forn/Sottoconti con dati sintetici/statistici"})
      dttCodQuery.Rows.Add(New Object() {11, "Anagrafiche Articoli"})
      dttCodQuery.Rows.Add(New Object() {12, "Anagrafiche Articoli con dati cont. singoli magazzini ad oggi"})
      dttCodQuery.Rows.Add(New Object() {13, "Anagrafiche Articoli con dati cont. tutti i magazzini ad oggi"})
      dttCodQuery.Rows.Add(New Object() {14, "Anagrafiche Articoli con dati cont. singoli magazzini a data"})
      dttCodQuery.Rows.Add(New Object() {15, "Anagrafiche Articoli con dati cont. tutti i magazzini a data"})
      dttCodQuery.Rows.Add(New Object() {19, "Anagrafiche Articoli + Listini"})
      dttCodQuery.AcceptChanges()
      cbPf_codquery.DataSource = dttCodQuery
      cbPf_codquery.ValueMember = "cod"
      cbPf_codquery.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Function CreaCampiQF() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Rapprensenta la vecchia tabella CampiQF del combo.mdb.
      '--- E' stata portata per poter fare le selezioni direttamente in memoria.
      '--------------------------------------------------------------------------------------------------------------
      oCleIstf.dttCampiQF.Columns.Add("cb_codquery")
      oCleIstf.dttCampiQF.Columns.Add("cb_nomcampo")
      oCleIstf.dttCampiQF.Columns.Add("cb_descampo")
      oCleIstf.dttCampiQF.Columns.Add("cb_tipocampo")
      oCleIstf.dttCampiQF.Columns.Add("cb_size")
      oCleIstf.dttCampiQF.Columns.Add("cb_required")
      oCleIstf.dttCampiQF.Columns.Add("cb_ordinalposition", GetType(Integer))
      oCleIstf.dttCampiQF.Columns.Add("cb_format")
      oCleIstf.dttCampiQF.Columns.Add("cb_desstamp")
      '--------------------------------------------------------------------------------------------------------------
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_conto", "Codice conto", 4, 9, "No", 1, "", "Conto"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_tipo", "Tipo conto", 10, 1, "No", 2, "", "T"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_descr1", "Ragione sociale/descrizione (1.a parte)", 10, 30, "No", 3, "", "Descrizione 1"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_descr2", "Ragione sociale/descrizione (2.a parte)", 10, 30, "No", 4, "", "Descrizione 2"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_indir", "Indirizzo", 10, 35, "No", 5, "", "Indirizzo"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_cap", "Cap", 10, 5, "No", 6, "", "Cap"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_citta", "Città/località", 10, 28, "No", 7, "", "Città"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_prov", "Provincia", 10, 2, "No", 8, "", "Pr"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_codfis", "Codice fiscale", 10, 16, "No", 9, "", "Cod.fiscale"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_pariva", "Partita Iva", 10, 11, "No", 10, "", "Partita Iva"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_controp", "Codice contropartita abituale", 4, 9, "No", 11, "", "Controp."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra_1.an_descr1", "Descrizione contropartita abituale", 10, 30, "No", 12, "", "Descr.controp."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_alleg", "Allegati", 10, 1, "No", 13, "", "A"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_persfg", "Tipo soggetto (F/G)", 10, 1, "No", 14, "", "T"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_partite", "Gestione partite", 10, 1, "No", 15, "", "P"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_telef", "Numero telefono", 10, 18, "No", 16, "", "Num.telef."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_faxtlx", "Numero fax", 10, 18, "No", 17, "", "Num.fax"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_contatt", "Contatto", 10, 16, "No", 18, "", "Contatto"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_ultagg", "Data ultimo aggiorn.", 8, 10, "No", 19, "", "Dt.ult.agg."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_zona", "Codice zona", 3, 3, "No", 20, "", "Zon"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "tabzone.tb_deszone", "Descrizione zona", 10, 20, "No", 21, "", "Descr.zona"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_categ", "Codice categoria", 3, 3, "No", 22, "", "Cat"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "tabcate.tb_descate", "Descrizione categoria", 10, 20, "No", 23, "", "Descr.categ."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_codese", "Codice esenzione Iva", 3, 3, "No", 24, "", "Cod.es.Iva"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "tabciva.tb_desciva", "Descrizione esenzione Iva", 10, 20, "No", 25, "", "Descr.Iva"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_codpag", "Codice pagamento", 3, 3, "No", 26, "", "Pag"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "tabpaga.tb_despaga", "Descrizione pagamento", 10, 30, "No", 27, "", "Descr.pagam."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_scont1", "Sconto 1", 7, 5, "No", 28, "#,##0.00", "Sc.1"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_scont2", "Sconto 2", 7, 5, "No", 29, "#,##0.00", "Sc.2"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_agente", "Codice agente 1", 3, 3, "No", 30, "", "Age"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "tabcage.tb_descage", "Descrizione agente 1", 10, 20, "No", 31, "", "Descr.agente"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_banc1", "Banca appoggio (nome)", 10, 35, "No", 32, "", "Banca 1"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_banc2", "Banca appoggio (filiale/sede)", 10, 35, "No", 33, "", "Banca 2"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_abi", "Codice Abi banca d'appoggio", 4, 9, "No", 34, "", "Abi"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_cab", "Codice Cab banca d'appoggio", 4, 9, "No", 35, "", "Cab"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_rifriba", "Riferimento Riba", 10, 12, "No", 36, "", "Rif.Riba"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_spinc", "Addebito spese incasso", 10, 1, "No", 37, "", "I"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_bolli", "Addebito spese bolli", 10, 1, "No", 38, "", "B"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_numdic", "Numero dichiarazione d'intenti", 10, 7, "No", 39, "", "Dic.int."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_datdic", "Data dichiarazione d'intenti", 8, 10, "No", 40, "", "Dt.dic.int"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_listino", "Listino applicato", 3, 3, "No", 41, "", "Lst"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_vuoti", "Addebito cauzioni", 10, 1, "No", 42, "", "V"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_valuta", "Codice valuta", 3, 3, "No", 43, "", "Val"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "tabvalu.tb_desvalu", "Descrizione valuta", 10, 10, "No", 44, "", "Descr.val."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_claprov", "Classe provvigioni", 3, 3, "No", 45, "", "C.p"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_clascon", "Classe sconto", 3, 3, "No", 46, "", "C.s"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_note", "Note", 10, 30, "No", 47, "", "Note"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_porto", "Codice porto", 10, 1, "No", 48, "", "P"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "tabport.tb_desport", "Descrizione porto", 10, 20, "No", 49, "", "Descr.porto"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_vett", "Codice vettore", 3, 3, "No", 50, "", "Vet"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "tabvett.tb_desvett", "Descrizione vettore", 10, 40, "No", 51, "", "Descr.vett."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_fatt", "Tipo fatturazione", 10, 1, "No", 52, "", "F"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_fido", "Fido", 7, 12, "No", 53, "#,##0.00", "Fido"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_destin", "Codice destinazione merce", 4, 9, "No", 54, "", "Cod.dest."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "destdiv.dd_nomdest", "Nome destinazione", 10, 30, "No", 55, "", "Nome destin."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_scaden", "Gestione scadenziario", 10, 1, "No", 56, "", "S"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_dtaper", "Data acquisizione", 8, 10, "No", 57, "", "Dt.acq."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_stato", "Stato", 10, 3, "No", 58, "", "St"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "tabstat.tb_desstat", "Descrizione stato", 10, 20, "No", 59, "", "Descr.stato"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_agente2", "Codice agente 2", 3, 3, "No", 60, "", "Ag2"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "tabcage_1.tb_descage", "Descrizione agente 2", 10, 20, "No", 61, "", "Descr.agente"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_kpccee2", "Riferimento piano conti Cee (2)", 10, 8, "No", 62, "", "R.c.Cee2"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_kpccee", "Riferimento piano conti Cee (1)", 10, 8, "No", 63, "", "R.c.Cee1"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_flci", "Gestione contabilità analitica", 10, 1, "No", 64, "", "A"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_unmis", "Unità di misura (C.A.)", 10, 3, "No", 65, "", "UM"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_accperi", "Accettazione periodo", 10, 1, "No", 66, "", "P"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_codmast", "Codice mastro", 4, 9, "No", 67, "", "Cod.mast."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "tabmast.tb_desmast", "Descrizione mastro", 10, 30, "No", 68, "", "Descr.mastro"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_note2", "Note conto", 12, 255, "No", 69, "", "Note conto"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_blocco", "Tipo blocco del conto", 10, 1, "No", 70, "", "B"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_gcons", "Giorno della settimana consegna", 3, 1, "No", 71, "", "G"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_email", "Indirizzo posta elettronica", 10, 100, "No", 72, "", "Email"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_website", "Indirizzo Web Server", 10, 50, "No", 73, "", "Indir.Web"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_rifricd", "Riferimento piano conti riclassificato dare", 10, 8, "No", 74, "", "R.dare"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_rifrica", "Riferimento piano conti riclassificato avere", 10, 8, "No", 75, "", "R.avere"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_usaem", "Tipo di trasmiss.messaggi preferita", 10, 1, "No", 76, "", "T"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_codling", "Codice lingua", 3, 3, "No", 77, "", "Lin"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "tabling.tb_desling", "Descrizione lingua", 10, 20, "No", 78, "", "Descr.lingua"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_rating", "Rating di affidamento", 10, 3, "No", 79, "", "Rat"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_codbanc", "Codice nostra banca", 3, 3, "No", 80, "", "Ban"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "tabbanc.tb_desbanc", "Descrizione banca", 10, 30, "No", 81, "", "Descr.banca"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_agcontrop", "Aggiunta a codice contropartita articolo", 3, 3, "No", 82, "", "Con."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_codcana", "Canale di vendita", 3, 3, "No", 83, "", "Can"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_cell", "Cellulare", 10, 18, "No", 84, "", "Cellulare"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_iban", "IBAN (estero)", 10, 70, "No", 85, "", "IBAN (estero)"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_cin", "Cin", 10, 1, "No", 86, "", "C"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_prefiban", "Prefisso IBAN", 10, 4, "No", 87, "", "P.IB"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_trating", "Rating (tesoreria)", 10, 1, "No", 88, "", "Rating"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {1, "anagra.an_codvfde", "Voce Finanziaria (tesoreria)", 10, 4, "No", 89, "", "V.Fin."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {2, "Sum(prinot.pn_dare) As Dare", "Dare", 7, 12, "No", 100, "#,##0.00", "Dare"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {2, "Sum(prinot.pn_avere) As Avere", "Avere", 7, 12, "No", 101, "#,##0.00", "Avere"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {2, "Sum(prinot.pn_importo) As Importo", "Saldo", 7, 12, "No", 102, "#,##0.00", "Saldo"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {3, "#dSaldoContabile", "Saldo contabile esercizio di competenza (sintesi e stat.)", 7, 16, "No", 200, "#,##0.00", "Saldo cont."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {3, "#dRDinDistinta", "RD in distinta (sintesi e stat.)", 7, 16, "No", 201, "#,##0.00", "RD in dist."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {3, "#dRBrischio", "RB a rischio (sintesi e stat.)", 7, 16, "No", 202, "#,##0.00", "RB a rischio"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {3, "#dRischioPrimo", "Rischio primo (sintesi e stat.)", 7, 16, "No", 203, "#,##0.00", "Ris.primo"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {3, "#dFattnonCont", "Fatture non contabilizzate (sintesi e stat.)", 7, 16, "No", 204, "#,##0.00", "Fatt.non con"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {3, "#dBolledaFatt", "Bolle da fatturare (sintesi e stat.)", 7, 16, "No", 205, "#,##0.00", "Boll.da fatt"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {3, "#dOrdinidaEvad", "Ordini da evadere (sintesi e stat.)", 7, 16, "No", 206, "#,##0.00", "Ord.da evad."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {3, "#dRischioTotale", "Rischio totale (sintesi e stat.)", 7, 16, "No", 207, "#,##0.00", "Rischio tot."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {3, "#dSforamento", "Sforamento (sintesi e stat.)", 7, 16, "No", 208, "#,##0.00", "Sforamento"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {3, "#dRDScadute", "RD scadute (sintesi e stat.)", 7, 16, "No", 209, "#,##0.00", "RD scadute"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {3, "#dImpinsol12mesi", "Importo insoluti ultimi 12 mesi (sintesi e stat.)", 7, 16, "No", 210, "#,##0.00", "Im.in.12mesi"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {3, "#lNinsol12mesi", "N° insoluti ultimi 12 mesi (sintesi e stat.)", 4, 12, "No", 211, "", "N.in.12m."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {3, "#dImpinsolinessere", "Importo insoluti in essere (sintesi e stat.)", 7, 16, "No", 212, "#,##0.00", "Im.in.essere"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {3, "#lNinsolinessere", "N° insoluti in essere (sintesi e stat.)", 4, 12, "No", 213, "", "N.in.ess."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {3, "#dFattTotale", "Fatturato totale esercizio di competenza (sintesi e stat.)", 7, 16, "No", 216, "#,##0.00", "Fatt.tot."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "anagra.an_descr1", "Descrizione fornitore principale", 10, 30, "No", 12, "", "Descr.fornitore"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "anagra_1.an_descr1", "Descrizione fornitore alternativo (o produttore)", 10, 30, "No", 14, "", "Descr.forn.2"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artfasi.af_descr", "Descrizione fase articolo", 10, 40, "No", 92, "", "Descrizione fase articolo"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_areacm3", "Area occupata (cm3)", 4, 9, "No", 47, "", "Area occ."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_catlifo", "Codice categoria Lifo", 3, 3, "No", 23, "", "Lif"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_claprov", "Classe di provvigioni", 3, 3, "No", 24, "", "Cl.p"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_clascon", "Classe di sconto", 3, 3, "No", 25, "", "Cl.s"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_codalt", "Codice alternativo", 10, 18, "No", 2, "", "Cod.altern."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_codart", "Codice articolo", 10, 18, "No", 1, "", "Cod.articolo"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_coddb", "Codice distinta base", 10, 18, "No", 65, "", "Cod.dist.base"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_codiva", "Codice Iva", 3, 3, "No", 15, "", "Iva"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_codnomc", "Codice nomenclatura combinata", 10, 10, "No", 60, "", "Nom.comb."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_codpdon", "Codice relazione listini", 3, 3, "No", 50, "", "R.l"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_codroot", "Codice root", 10, 12, "No", 79, "", "Cod.root"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_codtipa", "Tipologia articolo", 3, 3, "No", 88, "", "Tip"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_codvar1", "Codice variante di 1.o livello", 10, 8, "No", 80, "", "C.1.a v."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_codvar2", "Codice variante di 2.o livello", 10, 8, "No", 81, "", "C.2.a v."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_codvar3", "Codice variante di 3.o livello", 10, 8, "No", 82, "", "C.3.a v."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_codvuo", "Codice vuoto/contenitore", 3, 3, "No", 48, "", "Vuo"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_confez2", "Seconda confezione", 10, 3, "No", 9, "", "Co2"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_contriva", "Codice contrassegno Iva", 10, 3, "No", 38, "", "C.i"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_controa", "Codice contropartita acquisti", 3, 3, "No", 33, "", "C.a"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_controp", "Codice contropartita vendita", 3, 3, "No", 21, "", "Con."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_contros", "Codice contropartita scarichi prod.", 3, 3, "No", 85, "", "S.p"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_conver", "Rapporto di conversione (tra un.mis.princ. e 2.a un.misura)", 7, 10, "No", 8, "#,##0.000", "Conver."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_critico", "Articolo-risorsa critica", 10, 1, "No", 78, "", "C"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_datins", "Data inserimento", 8, 10, "No", 40, "", "Dt.inser."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_descr", "Descrizione articolo", 10, 40, "No", 3, "", "Descr.art."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_desint", "Descrizione articolo (2.a parte)", 10, 40, "No", 4, "", "Descr.alt."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_famprod", "Codice famiglia/linea", 10, 4, "No", 44, "", "Fam."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_flricmar", "Ricarico/margine", 10, 1, "No", 49, "", "R"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_formula", "Formula conv.sostit. di qta con2", 12, 255, "No", 83, "", "Formula"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_forn", "Codice fornitore principale", 4, 9, "No", 11, "", "Fornitore"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_forn2", "Codice fornitore alternativo (o produttore)", 4, 9, "No", 13, "", "Fornit.2"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_fpfence", "Firm Planned Fence", 3, 3, "No", 74, "", "Fen"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_garacq", "Mesi garanzia in acquisto", 3, 2, "No", 54, "", "GA"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_garven", "Mesi garanzia in vendita", 3, 2, "No", 55, "", "GV"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_gescomm", "Gestione per commessa", 10, 1, "No", 73, "", "C"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_geslotti", "Gestione lotti", 10, 1, "No", 68, "", "L"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_gestser", "Gestione codice combinazione (CP2)", 10, 1, "No", 100, "", "G"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_gesvar", "Gestione varianti", 10, 1, "No", 56, "", "V"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_ggragg", "Giorni raggruppamento MRP", 3, 3, "No", 93, "", "Grp"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_ggrior", "Lead Time fence o tempo di riapprovvigionamento", 3, 3, "No", 30, "", "Rio"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_gruppo", "Codice gruppo merciologico", 3, 2, "No", 17, "", "Gr"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_inesaur", "In esaurimento", 10, 1, "No", 69, "", "E"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_livmindb", "Livello minimo distinta base", 3, 3, "No", 64, "", "D.b"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_makebuy", "Make or buy", 10, 1, "No", 95, "", "M"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_maxlotto", "Lotto massimo", 4, 9, "No", 97, "", "Max lotto"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_minord", "Lotto di produzione o di riapprovvigionamento", 4, 9, "No", 29, "", "Min.ord."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_note", "Note", 12, 255, "No", 70, "", "Note"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_numecr", "Numero mis.fiscale", 3, 3, "No", 46, "", "M.f"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_nummisure", "Numero misure gestite", 3, 3, "No", 84, "", "M.g"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_oragg", "Ora ultimo aggiornamento", 4, 9, "No", 39, "", "Ora u.agg"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_orins", "Ora inserimento", 4, 9, "No", 41, "", "Ora inser."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_paeorig", "Paese di origine", 10, 3, "No", 63, "", "P.o"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_percvst", "Percentuale per calcolo valore statistico", 7, 5, "No", 59, "#,##0.00", "V.sta"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_perqta", "Moltiplicatore quantità/prezzo", 7, 12, "No", 90, "#,##0", "Molt.qta/prz"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_perragg", "Periodo di raggruppamento", 10, 1, "No", 99, "", "P"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_pesoca", "Peso distr.costi da C.A.", 7, 9, "No", 87, "", "Peso C.A."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_pesolor", "Peso lordo (rif.ad unità di misura)", 7, 9, "No", 61, "#,##0.000000", "Peso lor."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_pesonet", "Peso netto (rif.ad unità di misura)", 7, 9, "No", 62, "#,##0.000000", "Peso net."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_polriord", "Politica di riordino", 10, 1, "No", 72, "", "R"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_prevar", "Prezzi diversi per ogni variante", 10, 1, "No", 57, "", "V"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_prorig", "Provincia di origine", 10, 2, "No", 58, "", "Po"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_qtacon2", "Quantità contenuta in 2.a confezione", 7, 10, "No", 10, "#,##0.000", "Q.in 2.a c"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_reparto", "Reparto ECR", 3, 3, "No", 35, "", "Rep"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_ricar1", "Prima percentuale ricarico/margine", 7, 5, "No", 52, "#,##0.00", "R.m.1"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_ricar2", "Seconda percentuale ricarico/margine", 7, 5, "No", 53, "#,##0.00", "R.M.2"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_ripriord", "Ripartizione ordini d'acquisto a fornitori diversi", 10, 1, "No", 98, "", "R"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_rrfence", "Ready to Release Fence", 3, 3, "No", 75, "", "RRF"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_scomax", "Scorta massima (quantità)", 4, 9, "No", 28, "", "Sc.max."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_scomin", "Scorta minima (quantità)", 4, 9, "No", 27, "", "Sc.min."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_sostit", "Codice articolo sostitutivo", 10, 18, "No", 31, "", "Art.sostit."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_sostituito", "Codice articolo sostituito", 10, 18, "No", 76, "", "Art.sostituito"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_sotgru", "Codice sottogruppo merciologico", 3, 3, "No", 19, "", "Sgr"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_stainv", "Stampa inventario", 10, 1, "No", 66, "", "I"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_stalist", "Articolo a listino", 10, 1, "No", 36, "", "L"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_stasche", "Stampa schede movimentazione", 10, 1, "No", 67, "", "M"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_sublotto", "Sottolotto", 4, 9, "No", 96, "", "S.lotto"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_tipitemcp3", "Tipo item (CP3)", 10, 1, "No", 101, "", "T"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_tipo", "Tipo articolo", 10, 1, "No", 5, "", "T"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_tipoopz", "Test opzione", 10, 1, "No", 71, "", "O"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_tipprom", "Codice promozione", 3, 3, "No", 42, "", "Pro"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_ubicaz", "Ubicazione fisica in magazzino", 10, 4, "No", 26, "", "Ubi."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_ultagg", "Data ultimo aggiornamento", 8, 10, "No", 37, "", "Ult.agg."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_unmis", "Unità di misura principale", 10, 3, "No", 6, "", "UMP"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_unmis2", "Seconda unità di misura", 10, 3, "No", 7, "", "Um2"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico.ar_volume", "Volume", 7, 9, "No", 94, "#,##0.0000", "Volume"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico_1.ar_descr", "Descrizione articolo sostitutivo", 10, 40, "No", 32, "", "Descr.art.sostit."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "artico_2.ar_descr", "Descrizione articolo sostituito", 10, 40, "No", 77, "", "Descr.art.sostituito"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "IIf(artico.ar_gesfasi = 'N', 0, artfasi.af_fase) As Fase", "Fase articolo", 3, 3, "No", 91, "", "Fas"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "tabcfam.tb_descfam", "Descrizione linea/famiglia", 10, 30, "No", 45, "", "Descr.linea"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "tabciva.tb_desciva", "Descrizione Iva", 10, 20, "No", 16, "", "Descr.Iva"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "tabcove.tb_descove", "Descrizione contropartita acquisti", 10, 30, "No", 34, "", "Descr.contr.acq."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "tabcove_1.tb_descove", "Descrizione contropartita vendite", 10, 30, "No", 22, "", "Descr.contr.vend."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "tabcove_2.tb_descove", "Descrizione contropartita scarichi prod.", 10, 30, "No", 86, "", "Descr.contr.sc.prod."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "tabgmer.tb_desgmer", "Descrizione gruppo merciologico", 10, 20, "No", 18, "", "Descr.gruppo"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "tabpdon.tb_despdon", "Descrizione relazione listini", 10, 30, "No", 51, "", "Descr.rel.list."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "tabsgme.tb_dessgme", "Descrizione sottogruppo merciologico", 10, 20, "No", 20, "", "Descr.sottogruppo"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "tabtipa.tb_destipa", "Descrizione tipologia", 10, 30, "No", 89, "", "Descr.tipologia"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {11, "tabtpro.tb_destpro", "Descrizione tipo di promozione", 10, 20, "No", 43, "", "Descr.tipo prom."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "((artpro.ap_esist + artpro.ap_ordin) - artpro.ap_impeg)", "Disponibilità (dati cont.)", 7, 12, "No", 129, "#,##0.000", "Disponib."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "(artpro.ap_esist - artpro.ap_impeg)", "Esistenza - Impegnato", 7, 12, "No", 134, "#,##0.000", "Esist-Impeg"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "(artpro.ap_esist - artpro.ap_prenot)", "Disponibilità netta (dati cont.)", 7, 12, "No", 130, "#,##0.000", "Disp.netta"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "(artprox.apx_ultcos * artpro.ap_esist) / artico.ar_perqta", "Val.esist.a ultimo costo (singoli mag.ad oggi)", 7, 12, "No", 131, "#,##0.00", "V.es.ult.cos"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "artpro.ap_carfor", "Carichi fornitore (dati cont.)", 7, 12, "No", 107, "#,##0.000", "Car.forn."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "artpro.ap_carpro", "Carichi da produzione (dati cont.)", 7, 12, "No", 108, "#,##0.000", "Car.produz."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "artpro.ap_carvar", "Carichi vari (dati cont.)", 7, 12, "No", 109, "#,##0.000", "Car.vari"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "artpro.ap_esist", "Esistenza (dati cont.)", 7, 12, "No", 103, "#,##0.000", "Esistenza"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "artpro.ap_giaini", "Giacenza iniziale (dati cont.)", 7, 12, "No", 115, "#,##0.000", "Giac.iniz."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "artpro.ap_impeg", "Impegnato (dati cont.)", 7, 12, "No", 106, "#,##0.000", "Impegnato"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "artpro.ap_magaz", "Codice magazzino (dati cont.)", 3, 3, "No", 101, "", "Mag"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "artpro.ap_ordin", "Ordinato (dati cont.)", 7, 12, "No", 105, "#,##0.000", "Ordinato"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "artpro.ap_prenot", "Prenotato (dati cont.)", 7, 12, "No", 104, "#,##0.000", "Prenotato"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "artpro.ap_rescli", "Resi da clienti (dati cont.)", 7, 12, "No", 110, "#,##0.000", "Resi da cl."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "artpro.ap_resfor", "Resi a fornitori (dati cont.)", 7, 12, "No", 114, "#,##0.000", "Resi a forn."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "artpro.ap_scacli", "Scarichi clienti (dati cont.)", 7, 12, "No", 111, "#,##0.000", "Scar.clienti"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "artpro.ap_scapro", "Scarichi a produzione (dati cont.)", 7, 12, "No", 112, "#,##0.000", "Scar.a prod."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "artpro.ap_scavar", "Scarichi vari (dati cont.)", 7, 12, "No", 113, "#,##0.000", "Scar. vari"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "artpro.ap_sommat", "Sommatoria per calc.giacenza iniziale (dati cont.)", 7, 12, "No", 128, "#,##0.00", "Sommatoria"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "artpro.ap_vcarfor", "Valore carichi fornitore (dati cont.)", 7, 12, "No", 119, "#,##0.00", "Val.car.forn"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "artpro.ap_vcarpro", "Valore carichi da produzione (dati cont.)", 7, 12, "No", 120, "#,##0.00", "V.car.prod."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "artpro.ap_vcarvar", "Valore carichi vari (dati cont.)", 7, 12, "No", 121, "#,##0.00", "Val.car.vari"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "artpro.ap_vgiaini", "Valore giacenza iniziale (dati cont.)", 7, 12, "No", 127, "#,##0.00", "V.giac.in."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "artpro.ap_vimpeg", "Valore impegnato (dati cont.)", 7, 12, "No", 118, "#,##0.00", "Val.impeg."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "artpro.ap_vordin", "Valore ordinato (dati cont.)", 7, 12, "No", 117, "#,##0.00", "Val.ordin."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "artpro.ap_vprenot", "Valore prenotato (dati cont.)", 7, 12, "No", 116, "#,##0.00", "Val.prenot."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "artpro.ap_vrescli", "Valore resi da clienti (dati cont.)", 7, 12, "No", 122, "#,##0.00", "V.resi da cl"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "artpro.ap_vresfor", "Valore resi a fornitori (dati cont.)", 7, 12, "No", 126, "#,##0.00", "V.resi forn."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "artpro.ap_vscacli", "Valore scarichi clienti (dati cont.)", 7, 12, "No", 123, "#,##0.00", "V.scar.cl."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "artpro.ap_vscapro", "Valore scarichi da produzione (dati cont.)", 7, 12, "No", 124, "#,##0.00", "V.scar.da pr"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "artpro.ap_vscavar", "Valore scarichi vari (dati cont.)", 7, 12, "No", 125, "#,##0.00", "Val.scar.vari"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "IIf((artprox.apx_qtalif + artprox.apx_giaini) > 0, (((artprox.apx_vqtalif + artprox.apx_vgiaini) / (artprox.apx_qtalif + artprox.apx_giaini)) * artpro.ap_esist), 0)", "Val.esist.costo medio globale (singoli mag.ad oggi)", 7, 12, "No", 133, "#,##0.00", "V.es.m.glob."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "IIf(artprox.apx_qtalif > 0, ((artprox.apx_vqtalif / artprox.apx_qtalif) * artpro.ap_esist), 0)", "Val.esist.costo medio anno (singoli mag.ad oggi)", 7, 12, "No", 132, "#,##0.00", "V.es.m.anno"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {12, "tabmaga.tb_desmaga", "Descrizione magazzino (dati cont.)", 10, 20, "No", 102, "", "Descr.magazz."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {13, "((artprox.apx_esist + artprox.apx_ordin) - artprox.apx_impeg)", "Disponibilità (dati cont.)", 7, 12, "No", 117, "#,##0.000", "Disponibil."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {13, "(artprox.apx_esist - artprox.apx_impeg)", "Esistenza - Impegnato", 7, 12, "No", 122, "#,##0.000", "Esist-Impeg"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {13, "(artprox.apx_esist - artprox.apx_prenot)", "Disponibilità netta (dati cont.)", 7, 12, "No", 118, "#,##0.000", "Disp.netta"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {13, "(artprox.apx_ultcos * artprox.apx_esist) / artico.ar_perqta", "Val.esist.a ultimo costo (tutti mag.ad oggi)", 7, 12, "No", 119, "#,##0.00", "V.e.ult.cos."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {13, "artprox.apx_dtulcar", "Data ultimo carico (dati cont.)", 8, 10, "No", 115, "", "Dt.ul.car."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {13, "artprox.apx_dtulsca", "Data ultimo scarico (dati cont.)", 8, 10, "No", 116, "", "Dt.ul.scar."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {13, "artprox.apx_esist", "Esistenza (dati cont.)", 7, 12, "No", 101, "#,##0.000", "Esistenza"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {13, "artprox.apx_giaini", "Giacenza iniziale (dati cont.)", 7, 12, "No", 105, "#,##0.000", "Giac.iniz."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {13, "artprox.apx_impeg", "Impegnato (dati cont.)", 7, 12, "No", 104, "#,##0.000", "Impeganto"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {13, "artprox.apx_ordin", "Ordinato (dati cont.)", 7, 12, "No", 103, "#,##0.000", "Ordinato"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {13, "artprox.apx_peucos", "Ultimo costo d'acquisto + spese (dati cont.)", 7, 12, "No", 113, "#,##0.00", "Pen.cos.acq."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {13, "artprox.apx_prenot", "Prenotato (dati cont.)", 7, 12, "No", 102, "#,##0.000", "Prenotato"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {13, "artprox.apx_qtalif", "Qtà carichi per calc. costo medio dell'anno (dati cont.)", 7, 12, "No", 110, "#.##0.000", "Qta.lifo"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {13, "artprox.apx_ultcos", "Ultimo costo d'acquisto (dati cont.)", 7, 12, "No", 112, "#,##0.00", "Ult.costo acq."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {13, "artprox.apx_ultpre", "Ultimo prezzo di vendita (dati cont.)", 7, 12, "No", 114, "#,##0.00", "Ult.prezzo"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {13, "artprox.apx_vgiaini", "Valore giacenza iniziale (dati cont.)", 7, 12, "No", 109, "#,##0.00", "V.giac.iniz."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {13, "artprox.apx_vimpeg", "Valore impegnato (dati cont.)", 7, 12, "No", 108, "#,##0.00", "Val.impeg."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {13, "artprox.apx_vordin", "Valore ordinato (dati cont.)", 7, 12, "No", 107, "#,##0.00", "Val.ordin."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {13, "artprox.apx_vprenot", "Valore prenotato (dati cont.)", 7, 12, "No", 106, "#,##0.00", "Val.prenot."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {13, "artprox.apx_vqtalif", "Val. carichi per calc. costo medio dell'anno (dati cont.)", 7, 12, "No", 111, "#,##0.00", "V.qta.lifo"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {13, "IIf((artprox.apx_qtalif + artprox.apx_giaini) > 0, (((artprox.apx_vqtalif + artprox.apx_vgiaini) / (artprox.apx_qtalif + artprox.apx_giaini)) * artprox.apx_esist), 0)", "Val.esist.costo medio globale (tutti mag.ad oggi)", 7, 12, "No", 121, "#,##0.00", "V.e.c.m.glob"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {13, "IIf(artprox.apx_qtalif > 0, ((artprox.apx_vqtalif / artprox.apx_qtalif) * artprox.apx_esist), 0)", "Val.esist.costo medio anno (tutti mag.ad oggi)", 7, 12, "No", 120, "#,##0.00", "V.e.c.m.anno"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {14, "(apx_ultcos * ap_esist) / artico.ar_perqta", "Val.esist.a ultimo costo (singoli mag.a data)", 7, 12, "No", 131, "#,##0.00", "V.es.ult.cos"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {14, ".ap_carfor", "Carichi da fornitori (dati cont.)", 7, 12, "No", 107, "#,##0.000", "Car.forn."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {14, ".ap_carpro", "Carichi da produzione (dati cont.)", 7, 12, "No", 108, "#,##0.000", "Car.prod."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {14, ".ap_carvar", "Carichi vari (dati cont.)", 7, 12, "No", 109, "#,##0.000", "Car.vari"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {14, ".ap_esist", "Esistenza (dati cont.)", 7, 12, "No", 103, "#,##0.000", "Esistenza"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {14, ".ap_giaini", "Giacenza iniziale (dati cont.)", 7, 12, "No", 115, "#,##0.000", "Giac.iniz."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {14, ".ap_magaz", "Codice magazzino (dati cont.)", 3, 3, "No", 101, "", "Mag"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {14, ".ap_rescli", "Resi da clienti (dati cont.)", 7, 12, "No", 110, "#,##0.000", "Resi cli."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {14, ".ap_resfor", "Resi a fornitori (dati cont.)", 7, 12, "No", 114, "#,##0.000", "Resi forn."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {14, ".ap_scacli", "Scarichi clienti (dati cont.)", 7, 12, "No", 111, "#,##0.000", "Scar.cli."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {14, ".ap_scapro", "Scarichi a produzione (dati cont.)", 7, 12, "No", 112, "#,##0.000", "Scar.prod."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {14, ".ap_scavar", "Scarichi vari (dati cont.)", 7, 12, "No", 113, "#,##0.000", "Scar.vari"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {14, ".ap_vcarfor", "Valore carichi fornitore (dati cont.)", 7, 12, "No", 119, "#,##0.00", "Val.car.for."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {14, ".ap_vcarpro", "Valore carichi da produzione (dati cont.)", 7, 12, "No", 120, "#,##0.00", "Val.car.pro."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {14, ".ap_vcarvar", "Valore carichi vari (dati cont.)", 7, 12, "No", 121, "#,##0.00", "Val.car.var."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {14, ".ap_vgiaini", "Valore giacenza iniziale (dati cont.)", 7, 12, "No", 127, "#,##0.00", "Val.gia.ini."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {14, ".ap_vrescli", "Valore resi da clienti (dati cont.)", 7, 12, "No", 122, "#,##0.00", "Val.res.cli."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {14, ".ap_vresfor", "Valore resi a fornitori (dati cont.)", 7, 12, "No", 126, "#,##0.00", "Val.res.for."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {14, ".ap_vscacli", "Valore scarichi clienti (dati cont.)", 7, 12, "No", 123, "#,##0.00", "Val.sca.cli."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {14, ".ap_vscapro", "Valore scarichi a produzione (dati cont.)", 7, 12, "No", 124, "#,##0.00", "Val.sca.pro."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {14, ".ap_vscavar", "Valore scarichi vari (dati cont.)", 7, 12, "No", 125, "#,##0.00", "Val.sca.vari"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {14, "IIf((apx_qtalif + apx_giaini) > 0, (((apx_vqtalif + apx_vgiaini) / (apx_qtalif + apx_giaini)) * ap_esist), 0)", "Val.esist.costo medio globale (singoli mag.a data)", 7, 12, "No", 133, "#,##0.00", "V.es.m.glob."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {14, "IIf(apx_qtalif > 0, ((apx_vqtalif / apx_qtalif) * ap_esist), 0)", "Val.esist.costo medio anno (singoli mag.a data)", 7, 12, "No", 132, "#,##0.00", "V.es.m.anno"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {14, "tabmaga.tb_desmaga", "Descrizione magazzino (dati cont.)", 10, 20, "No", 102, "", "Descr.magazz."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {15, "(apx_ultcos * apx_esist) / artico.ar_perqta", "Val.esist.a ultimo costo (tutti mag.a data)", 7, 12, "No", 119, "#,##0.00", "V.e.ult.cos."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {15, ".apx_dtulcar", "Data ultimo carico (dati cont.)", 8, 10, "No", 115, "", "Dt.ul.car."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {15, ".apx_dtulsca", "Data ultimo scarico (dati cont.)", 7, 10, "No", 116, "", "Dt.ul.sca."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {15, ".apx_esist", "Esistenza (dati cont.)", 7, 12, "No", 101, "#,##0.000", "Esistenza"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {15, ".apx_giaini", "Giacenza iniziale (dati cont.)", 7, 12, "No", 105, "#,##0.000", "Giac.iniz."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {15, ".apx_peucos", "Ultimo costo d'acquisto + spese (dati cont.)", 7, 12, "No", 113, "#,##0.00", "Pen.cos.acq."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {15, ".apx_qtalif", "Quantita' carichi per calcolo costo medio dell'anno (dati cont.)", 7, 12, "No", 110, "#,##0.00", "Qtà.lifo"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {15, ".apx_ultcos", "Ultimo costo d'acquisto (dati cont.)", 7, 12, "No", 112, "#,##0.00", "Ult.costo"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {15, ".apx_ultpre", "Ultimo prezzo di vendita (dati cont.)", 7, 12, "No", 114, "#,##0.00", "Ult.pr.vend."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {15, ".apx_vgiaini", "Valore giacenza iniziale (dati cont.)", 7, 12, "No", 109, "#,##0.00", "Val.gia.ini."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {15, ".apx_vqtalif", "Valore carichi per calcolo costo medio dell'anno (dati cont.)", 7, 12, "No", 111, "#,##0.00", "Val.qtà.lif."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {15, "IIf((apx_qtalif + apx_giaini) > 0, (((apx_vqtalif + apx_vgiaini) / (apx_qtalif + apx_giaini)) * apx_esist), 0)", "Val.esist.costo medio globale (tutti mag.a data)", 7, 12, "No", 121, "#,##0.00", "V.e.c.m.glob"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {15, "IIf(apx_qtalif > 0, ((apx_vqtalif / apx_qtalif) * apx_esist), 0)", "Val.esist.costo medio anno (tutti mag.a data)", 7, 12, "No", 120, "#,##0.00", "V.e.c.m.anno"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {19, "anagra_2.an_descr1", "Descrizione conto (listini)", 10, 30, "No", 102, "", "Descr.conto"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {19, "listini.lc_aquant", "A quantità (listini)", 7, 12, "No", 113, "#,##0.000", "A qtà"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {19, "listini.lc_codtpro", "Codice promozione (listini)", 3, 3, "No", 105, "", "Pro"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {19, "listini.lc_codvalu", "Codice valuta (listini)", 3, 3, "No", 103, "", "Val"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {19, "listini.lc_conto", "Codice cliente/fornitore (listini)", 4, 9, "No", 101, "", "Conto"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {19, "listini.lc_coddest", "Codice destinazione cliente/fornitore (listini)", 4, 9, "No", 101, "", "Cod.dest."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {19, "destdiv.dd_nomdest", "Descrizione destinazione (listini)", 10, 30, "No", 102, "", "Descr.destinaz."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {19, "listini.lc_daquant", "Da quantità (listini)", 7, 12, "No", 112, "#,##0.000", "Da qtà"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {19, "listini.lc_datagg", "Data aggiornamento/inizio validità (listini)", 8, 10, "No", 108, "", "Dt.agg."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {19, "listini.lc_datscad", "Data scadenza/fine validità prezzo (listini)", 8, 10, "No", 111, "", "Dt.scad."})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {19, "listini.lc_listino", "Listino", 3, 3, "No", 107, "", "Lst"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {19, "listini.lc_prezzo", "Prezzo (listini)", 7, 12, "No", 110, "#,##0.00", "Prezzo"})
      oCleIstf.dttCampiQF.Rows.Add(New Object() {19, "listini.lc_tipo", "Tipo (C/F) (listini)", 10, 1, "No", 109, "", "T"})
      '--------------------------------------------------------------------------------------------------------------
      oCleIstf.dttCampiQF.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function ComponiComboGriglia(ByRef dttCombo As DataTable) As Boolean
    Dim dtrRow() As DataRow = Nothing

    Try
      If cbPf_codquery.SelectedValue Is Nothing Then Return True
      '--------------------------------------------------------------------------------------------------------------
      '--- Riempie la lista dei campi da visualizzare nel combo della griglia
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCInt(cbPf_codquery.SelectedValue)
        Case 1 : dtrRow = oCleIstf.dttCampiQF.Select("cb_codquery = 1 AND cb_nomcampo <> 'anagra.an_scont1' AND cb_nomcampo <> 'anagra.an_scont2'", "cb_ordinalposition")
        Case 2 : dtrRow = oCleIstf.dttCampiQF.Select("cb_codquery IN(1, 2) AND cb_nomcampo <> 'anagra.an_scont1' AND cb_nomcampo <> 'anagra.an_scont2'", "cb_ordinalposition")
        Case 3 : dtrRow = oCleIstf.dttCampiQF.Select("cb_codquery IN (1, 3) AND cb_nomcampo <> 'anagra.an_scont1' AND cb_nomcampo <> 'anagra.an_scont2'", "cb_ordinalposition")
        Case 11 : dtrRow = oCleIstf.dttCampiQF.Select("cb_codquery = 11", "cb_ordinalposition")
        Case 12 : dtrRow = oCleIstf.dttCampiQF.Select("(cb_codquery IN (11, 12)) AND cb_nomcampo <> 'tabtipa.tb_destipa'", "cb_ordinalposition")
        Case 13 : dtrRow = oCleIstf.dttCampiQF.Select("(cb_codquery IN (11, 13))", "cb_ordinalposition")
        Case 14 : dtrRow = oCleIstf.dttCampiQF.Select("(cb_codquery IN (11, 14)) AND cb_nomcampo <> 'tabtipa.tb_destipa'", "cb_ordinalposition")
        Case 15 : dtrRow = oCleIstf.dttCampiQF.Select("(cb_codquery IN (11, 15))", "cb_ordinalposition")
        Case 19 : dtrRow = oCleIstf.dttCampiQF.Select("(cb_codquery IN (11, 19)) AND cb_nomcampo <> 'tabtipa.tb_destipa'", "cb_ordinalposition")
      End Select
      '--------------------------------------------------------------------------------------------------------------
      dttCombo = New DataTable
      dttCombo.Columns.Add("val")
      dttCombo.Columns.Add("cod")
      '--------------------------------------------------------------------------------------------------------------
      For z As Integer = 0 To dtrRow.Length - 1
        dttCombo.Rows.Add(New Object() {dtrRow(z)!cb_nomcampo, dtrRow(z)!cb_descampo})
      Next
      '--------------------------------------------------------------------------------------------------------------
      dttCombo.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function ComponiComboOrder() As Boolean
    Dim dttOrdin As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If cbPf_codquery.SelectedValue Is Nothing Then Return True
      '--------------------------------------------------------------------------------------------------------------
      dttOrdin.Columns.Add("cod", GetType(String))
      dttOrdin.Columns.Add("val", GetType(String))
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCInt(cbPf_codquery.SelectedValue)
        Case 1, 2, 3
          dttOrdin.Rows.Add(New Object() {"anagra.an_conto", "Conto"})
          dttOrdin.Rows.Add(New Object() {"anagra.an_descr1, anagra.an_conto", "Alfabetico"})
          dttOrdin.Rows.Add(New Object() {"anagra.an_zona, anagra.an_conto", "Zona"})
          dttOrdin.Rows.Add(New Object() {"anagra.an_categ, anagra.an_conto", "Categoria"})
          dttOrdin.Rows.Add(New Object() {"anagra.an_agente, anagra.an_conto", "Agente"})
          dttOrdin.Rows.Add(New Object() {"anagra.an_cap, anagra.an_conto", "Cap"})
        Case Else
          dttOrdin.Rows.Add(New Object() {"artico.ar_codart", "Articolo"})
          dttOrdin.Rows.Add(New Object() {"artico.ar_codalt, artico.ar_codart", "Codice alternativo"})
          dttOrdin.Rows.Add(New Object() {"artico.ar_descr, artico.ar_codart", "Descrizione"})
          dttOrdin.Rows.Add(New Object() {"artico.ar_gruppo, artico.ar_sotgru, artico.ar_codart", "Gruppo/sottogruppo"})
          dttOrdin.Rows.Add(New Object() {"artico.ar_forn, artico.ar_codart", "Fornitore"})
          dttOrdin.Rows.Add(New Object() {"artico.ar_ubicaz, artico.ar_codart", "Ubicazione"})
          dttOrdin.Rows.Add(New Object() {"artico.ar_famprod, artico.ar_codart", "Famiglia"})
          If NTSCInt(cbPf_codquery.SelectedValue) = 19 Then
            dttOrdin.Rows.Add(New Object() {"listini.lc_conto, listini.lc_coddest, artico.ar_codart", "Cl./forn.(listini)"})
          End If
      End Select
      '--------------------------------------------------------------------------------------------------------------
      dttOrdin.AcceptChanges()
      cbPf_order.DataSource = dttOrdin
      cbPf_order.ValueMember = "cod"
      cbPf_order.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
      If bOnLoading = False Then
        If (dsIstf.Tables("PARSTAF").Rows(dcIstf.Position).RowState <> DataRowState.Added) And _
           (dsIstf.Tables("PARSTAF").Rows(dcIstf.Position).RowState <> DataRowState.Deleted) Then
          cbPf_order.SelectedValue = NTSCStr(dsIstf.Tables("PARSTAF").Rows(dcIstf.Position)!pf_order)
        Else
          Select Case NTSCInt(cbPf_codquery.SelectedValue)
            Case 1, 2, 3
              cbPf_order.SelectedValue = "anagra.an_conto"
              cbPf_order.NTSDbField = "anagra.an_conto"
            Case Else
              cbPf_order.SelectedValue = "artico.ar_codart"
              cbPf_order.NTSDbField = "artico.ar_codart"
          End Select
        End If
      Else
        cbPf_order.SelectedValue = "anagra.an_conto"
        cbPf_order.NTSDbField = "anagra.an_conto"
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function ConfiguraForm() As Boolean
    Dim dttCombo As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edPf_codform.Text) = 0 Then
        GctlSetVisEnab(edPf_codform, False)
        GctlSetVisEnab(cbPf_codquery, False)
        grvStca.Enabled = False
      Else
        edPf_codform.Enabled = False
        cbPf_codquery.Enabled = False
        GctlSetVisEnab(grvStca, False)
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- I combo vengono cambiati in base al contesto, 
      '--- questo fa si che non siano personalizzabili tramite ctrl+shift+click.
      '--- è una cosa voluta, quindi per le personalizzazioni modificare i 2 metodi qui sotto.
      '--------------------------------------------------------------------------------------------------------------
      ComponiComboGriglia(dttCombo)
      ComponiComboOrder()
      '--------------------------------------------------------------------------------------------------------------
      '--- Reinizializzo il campo con il nuovo combo
      '--------------------------------------------------------------------------------------------------------------
      oCleIstf.bLoadinComboGriglia = True
      pfc_nomcampo.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129322350479751736, "Nome campo"), dttCombo, "cod", "val")
      oCleIstf.bLoadinComboGriglia = False
      '--------------------------------------------------------------------------------------------------------------
      ApriParstca()
      ApriParstafx()
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Sub Filtri_ANAGRA()
    Dim i As Integer = 0
    Dim strTmp As String = ""
    Dim oParam As New CLE__PATB
    Dim dttTmp As New DataTable
    Dim frmAna2 As FRM__ANA2 = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCInt(cbPf_codquery.SelectedValue)
        Case 1, 2, 3
        Case Else : Return
      End Select
      '--------------------------------------------------------------------------------------------------------------
      oParam.bTipoProposto = False
      oParam.bVisGriglia = False
      oParam.strAlfpar = "BN__ISTF"
      oParam.rFiltriAnagra = dsIstf.Tables("PARSTAF").Rows(dcIstf.Position)
      '--------------------------------------------------------------------------------------------------------------
      oParam.strCin = ""
      If oCleIstf.ApriParstafx(NTSCInt(edPf_codform.Text), dttTmp) = True Then
        For i = 2 To (dttTmp.Columns.Count - 1)
          oParam.strCin += NTSCStr(dttTmp.Rows(0)(dttTmp.Columns(i).ColumnName)) & "§"
        Next
      End If
      '--------------------------------------------------------------------------------------------------------------
      NTSZOOM.strIn = ""
      oParam.strTipo = "C"
      NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
      '--------------------------------------------------------------------------------------------------------------
      If NTSZOOM.strIn = "" Then GoTo Salta
      '--------------------------------------------------------------------------------------------------------------
      Try
        dttTmp = CType(oParam.oParam, DataTable)
        dttStax = dttTmp
      Catch ex As Exception
      End Try
      '--------------------------------------------------------------------------------------------------------------
      If Not oParam.rFiltriAnagra Is Nothing Then
        With dsIstf.Tables("PARSTAF").Rows(dcIstf.Position)
          !pf_1tipo = oParam.rFiltriAnagra!pf_1tipo
          !pf_contoini = oParam.rFiltriAnagra!pf_contoini
          !pf_contofin = oParam.rFiltriAnagra!pf_contofin
          !pf_capini = oParam.rFiltriAnagra!pf_capini
          !pf_capfin = oParam.rFiltriAnagra!pf_capfin
          !pf_zonaini = oParam.rFiltriAnagra!pf_zonaini
          !pf_zonafin = oParam.rFiltriAnagra!pf_zonafin
          !pf_categini = oParam.rFiltriAnagra!pf_categini
          !pf_categfin = oParam.rFiltriAnagra!pf_categfin
          !pf_agenteini = oParam.rFiltriAnagra!pf_agenteini
          !pf_agentefin = oParam.rFiltriAnagra!pf_agentefin
          !pf_codpagini = oParam.rFiltriAnagra!pf_codpagini
          !pf_codpagfin = oParam.rFiltriAnagra!pf_codpagfin
          !pf_dtaperini = oParam.rFiltriAnagra!pf_dtaperini
          !pf_dtaperfin = oParam.rFiltriAnagra!pf_dtaperfin
          !pf_ultaggini = oParam.rFiltriAnagra!pf_ultaggini
          !pf_ultaggfin = oParam.rFiltriAnagra!pf_ultaggfin
          !pf_testiva = oParam.rFiltriAnagra!pf_testiva
          !pf_codcand = oParam.rFiltriAnagra!pf_codcand
          !pf_codcana = oParam.rFiltriAnagra!pf_codcana
          !pf_listd = oParam.rFiltriAnagra!pf_listd
          !pf_lista = oParam.rFiltriAnagra!pf_lista
          !pf_provvd = oParam.rFiltriAnagra!pf_provvd
          !pf_provva = oParam.rFiltriAnagra!pf_provva
          !pf_scontid = oParam.rFiltriAnagra!pf_scontid
          !pf_scontia = oParam.rFiltriAnagra!pf_scontia
          !pf_codesed = oParam.rFiltriAnagra!pf_codesed
          !pf_codesea = oParam.rFiltriAnagra!pf_codesea
          !pf_lingd = oParam.rFiltriAnagra!pf_lingd
          !pf_linga = oParam.rFiltriAnagra!pf_linga
          !pf_vald = oParam.rFiltriAnagra!pf_vald
          !pf_vala = oParam.rFiltriAnagra!pf_vala
          !pf_ragd = oParam.rFiltriAnagra!pf_ragd
          !pf_stato = oParam.rFiltriAnagra!pf_stato
          !pf_blocco = oParam.rFiltriAnagra!pf_blocco
          !pf_privacy = oParam.rFiltriAnagra!pf_privacy
          !pf_status = oParam.rFiltriAnagra!pf_status
          !pf_perfatt = oParam.rFiltriAnagra!pf_perfatt
          !pf_pcons = oParam.rFiltriAnagra!pf_pcons
        End With
      End If
      '--------------------------------------------------------------------------------------------------------------
Salta:
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(cbPf_codquery.SelectedValue) <> 1 Then
        Dim oParam1 As New CLE__CLDP
        frmAna2 = CType(NTSNewFormModal("FRM__ANA2"), FRM__ANA2)
        frmAna2.Init(oMenu, oParam1, DittaCorrente)
        frmAna2.InitEntity(oCleIstf)
        frmAna2.nEscomp = NTSCInt(dsIstf.Tables("PARSTAF").Rows(dcIstf.Position)!pf_escomp)
        frmAna2.nSaldo = NTSCInt(dsIstf.Tables("PARSTAF").Rows(dcIstf.Position)!pf_saldo)
        frmAna2.nMovimentato = NTSCInt(dsIstf.Tables("PARSTAF").Rows(dcIstf.Position)!pf_movimentato)
        frmAna2.ShowDialog()
        If frmAna2.bAnnullato = False Then
          With dsIstf.Tables("PARSTAF").Rows(dcIstf.Position)
            !pf_escomp = frmAna2.nEscomp
            !pf_saldo = frmAna2.nSaldo
            !pf_movimentato = frmAna2.nMovimentato
          End With
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      If Not frmAna2 Is Nothing Then frmAna2.Dispose()
      frmAna2 = Nothing
    End Try
  End Sub
  Public Overridable Sub Filtri_ARTICO()
    Dim i As Integer = 0
    Dim oParam As New CLE__PATB
    Dim dttTmp As New DataTable
    Dim frmArt2 As FRM__ART2 = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCInt(cbPf_codquery.SelectedValue)
        Case 1, 2, 3, 4 : Return
      End Select
      '--------------------------------------------------------------------------------------------------------------
      oParam.bVisGriglia = False
      oParam.strAlfpar = "BN__ISTF"
      oParam.strTipoArticolo = "N"
      oParam.rFiltriArtico = dsIstf.Tables("PARSTAF").Rows(dcIstf.Position)
      '--------------------------------------------------------------------------------------------------------------
      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oParam)
      '--------------------------------------------------------------------------------------------------------------
      If NTSZOOM.strIn = "" Then GoTo Salta
      '--------------------------------------------------------------------------------------------------------------
      Try
        dttTmp = CType(oParam.oParam, DataTable)
        dttStax = dttTmp
      Catch ex As Exception
      End Try
      '--------------------------------------------------------------------------------------------------------------
      If Not oParam.rFiltriArtico Is Nothing Then
        With dsIstf.Tables("PARSTAF").Rows(dcIstf.Position)
          !pf_stalist = oParam.rFiltriArtico!pf_stalist
          !pf_2tipo = oParam.rFiltriArtico!pf_2tipo
          !pf_codartini = oParam.rFiltriArtico!pf_codartini
          !pf_codartfin = oParam.rFiltriArtico!pf_codartfin
          !pf_codaltini = oParam.rFiltriArtico!pf_codaltini
          !pf_codaltfin = oParam.rFiltriArtico!pf_codaltfin
          !pf_gruppoini = oParam.rFiltriArtico!pf_gruppoini
          !pf_gruppofin = oParam.rFiltriArtico!pf_gruppofin
          !pf_fornini = oParam.rFiltriArtico!pf_fornini
          !pf_fornfin = oParam.rFiltriArtico!pf_fornfin
          !pf_famprodini = oParam.rFiltriArtico!pf_famprodini
          !pf_famprodfin = oParam.rFiltriArtico!pf_famprodfin

          !pf_codcla1 = oParam.rFiltriArtico!pf_codcla1
          !pf_codcla2 = oParam.rFiltriArtico!pf_codcla2
          !pf_codcla3 = oParam.rFiltriArtico!pf_codcla3
          !pf_codcla4 = oParam.rFiltriArtico!pf_codcla4
          !pf_codcla5 = oParam.rFiltriArtico!pf_codcla5

          !pf_filtro = oParam.rFiltriArtico!pf_filtro
          !pf_sottd = oParam.rFiltriArtico!pf_sottd
          !pf_sotta = oParam.rFiltriArtico!pf_sotta
          !pf_descrd = oParam.rFiltriArtico!pf_descrd
          !pf_descra = oParam.rFiltriArtico!pf_descra
          !pf_marcad = oParam.rFiltriArtico!pf_marcad
          !pf_marcaa = oParam.rFiltriArtico!pf_marcaa
          !pf_claprovd = oParam.rFiltriArtico!pf_claprovd
          !pf_claprova = oParam.rFiltriArtico!pf_claprova
          !pf_clascond = oParam.rFiltriArtico!pf_clascond
          !pf_clascona = oParam.rFiltriArtico!pf_clascona
          !pf_filtrodb = oParam.rFiltriArtico!pf_filtrodb
          !pf_filtroubic = oParam.rFiltriArtico!pf_filtroubic
          !pf_dataultagd = oParam.rFiltriArtico!pf_dataultagd
          !pf_dataultaga = oParam.rFiltriArtico!pf_dataultaga
          !pf_approvda = oParam.rFiltriArtico!pf_approvda
          !pf_approva = oParam.rFiltriArtico!pf_approva
          !pf_codivad = oParam.rFiltriArtico!pf_codivad
          !pf_codivaa = oParam.rFiltriArtico!pf_codivaa
          !pf_lotti = oParam.rFiltriArtico!pf_lotti
          !pf_commessa = oParam.rFiltriArtico!pf_commessa
          !pf_esaurito = oParam.rFiltriArtico!pf_esaurito
          !pf_varianti = oParam.rFiltriArtico!pf_varianti
          !pf_matricole = oParam.rFiltriArtico!pf_matricole
          !pf_codtipad = oParam.rFiltriArtico!pf_codtipad
          !pf_codtipaa = oParam.rFiltriArtico!pf_codtipaa
          !pf_magstockini = oParam.rFiltriArtico!pf_magstockini
          !pf_magstockfin = oParam.rFiltriArtico!pf_magstockfin
          !pf_magprodini = oParam.rFiltriArtico!pf_magprodini
          !pf_magprodfin = oParam.rFiltriArtico!pf_magprodfin
          !pf_gesubic = oParam.rFiltriArtico!pf_gesubic
          !pf_gesfasi = oParam.rFiltriArtico!pf_gesfasi
          !pf_critico = oParam.rFiltriArtico!pf_critico
          !pf_annod = oParam.rFiltriArtico!pf_annod
          !pf_annoa = oParam.rFiltriArtico!pf_annoa
          !pf_codstagd = oParam.rFiltriArtico!pf_codstagd
          !pf_codstaga = oParam.rFiltriArtico!pf_codstaga
          !pf_codtagld = oParam.rFiltriArtico!pf_codtagld
          !pf_codtagla = oParam.rFiltriArtico!pf_codtagla
        End With
      End If
      '--------------------------------------------------------------------------------------------------------------
Salta:
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(cbPf_codquery.SelectedValue) > 11 Then
        Dim oParam1 As New CLE__CLDP
        frmArt2 = CType(NTSNewFormModal("FRM__ART2"), FRM__ART2)
        frmArt2.Init(oMenu, oParam1, DittaCorrente)
        frmArt2.InitEntity(oCleIstf)
        With dsIstf.Tables("PARSTAF").Rows(dcIstf.Position)
          frmArt2.nScortaminima = NTSCInt(!pf_scorta)
          frmArt2.nEsistenza = NTSCInt(!pf_esistenza)
          frmArt2.nUltimavendita = NTSCInt(!pf_ultvendita)
          frmArt2.strData = NTSCStr(!pf_dtultvendita)
          frmArt2.nCodvalu = NTSCInt(!pf_codvalu)
          frmArt2.nMagazini = NTSCInt(!pf_magazini)
          frmArt2.nMagazfin = NTSCInt(!pf_magazfin)
          frmArt2.nTipolistino = NTSCInt(!pf_tipolistino)
          frmArt2.nListino = NTSCInt(!pf_listino)
          frmArt2.lContoini = NTSCInt(!pf_listconini)
          frmArt2.lContofin = NTSCInt(!pf_listconfin)
          frmArt2.bCodtpro = NTSCInt(!pf_promozione)
          frmArt2.nCodtpro = NTSCInt(!pf_codtpro)
          frmArt2.bPrimoLiv = NTSCInt(!pf_primoliv)
        End With
        frmArt2.nTipoStampa = NTSCInt(cbPf_codquery.SelectedValue)
        frmArt2.ShowDialog()
        If frmArt2.bAnnullato = False Then
          With dsIstf.Tables("PARSTAF").Rows(dcIstf.Position)
            !pf_scorta = frmArt2.nScortaminima
            !pf_esistenza = frmArt2.nEsistenza
            !pf_ultvendita = frmArt2.nUltimavendita
            If frmArt2.strData.Trim <> "" Then
              !pf_dtultvendita = frmArt2.strData
            Else
              !pf_dtultvendita = DBNull.Value
            End If
            !pf_codvalu = frmArt2.nCodvalu
            !pf_magazini = frmArt2.nMagazini
            !pf_magazfin = frmArt2.nMagazfin
            !pf_tipolistino = frmArt2.nTipolistino
            !pf_listino = frmArt2.nListino
            !pf_listconini = frmArt2.lContoini
            !pf_listconfin = frmArt2.lContofin
            !pf_promozione = frmArt2.bCodtpro
            !pf_codtpro = frmArt2.nCodtpro
            !pf_primoliv = frmArt2.bPrimoLiv
          End With
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      If Not frmArt2 Is Nothing Then frmArt2.Dispose()
      frmArt2 = Nothing
    End Try
  End Sub

  Public Overridable Sub GetCodiceFormato()
    Try
      '--------------------------------------------------------------------------------------------------------------
      oCleIstf.lPf_codform = NTSCInt(edPf_codform.Text)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub RecordSposta(ByVal strDove As String)
    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not Salva(False, False) Then Return
      If Not SalvaRiga(False) Then Return
      '--------------------------------------------------------------------------------------------------------------
      If dsStca.Tables("PARSTCA").Rows.Count = 0 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 129723947424165788, "Attenzione!" & vbCrLf & _
          "Selezionare alemno un campo da stampare."))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      Select Case strDove
        Case "primo" : dcIstf.MoveFirst()
        Case "precedente" : dcIstf.MovePrevious()
        Case "successivo" : dcIstf.MoveNext()
        Case "ultimo" : dcIstf.MoveLast()
      End Select
      '--------------------------------------------------------------------------------------------------------------
      ConfiguraForm()
      '--------------------------------------------------------------------------------------------------------------
      GetCodiceFormato()
      '--------------------------------------------------------------------------------------------------------------
      oCleIstf.bHasChanges = False
      oCleIstf.bParstcaHasChanges = False
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub RecordRipristina(ByVal bAsk As Boolean)
    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not grvStca.NTSRipristinaRigaCorrenteBefore(dcStca, bAsk) Then Return
      oCleIstf.RipristinaParstca(dcStca.Position, dcStca.Filter)
      grvStca.NTSRipristinaRigaCorrenteAfter()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Function Salva(ByVal bDelete As Boolean, ByVal bOnUnloading As Boolean) As Boolean
    Dim dRes As DialogResult

    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------      
      If (oCleIstf.bHasChanges = True) Or (bDelete = True) Then
        '------------------------------------------------------------------------------------------------------------
        If GctlControllaOutNotEqual() = False Then Return False
        '------------------------------------------------------------------------------------------------------------
        If (cbPf_order.SelectedValue Is Nothing) And (bDelete = False) Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 129723944310639289, "Attenzione!" & vbCrLf & _
            "Indicare un ordinamento valido prima di salvare il formato di stampa."))
          Return False
        End If
        '------------------------------------------------------------------------------------------------------------
        If bDelete = False Then
          dRes = oApp.MsgBoxInfoYesNoCancel_DefYes(oApp.Tr(Me, 129682408144810892, "Salvare l'impostazione?"))
        Else
          dRes = Windows.Forms.DialogResult.Yes
        End If
        '------------------------------------------------------------------------------------------------------------
        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oCleIstf.SalvaImpostazione(bDelete, NTSCInt(edPf_codform.Text), dsIstf, dsStca, dttStax) Then Return False
          If dsIstf.Tables("PARSTAF").Rows.Count > 0 Then edPf_codform.Enabled = False
        End If
        If dRes = System.Windows.Forms.DialogResult.No Then
          If bOnUnloading = False Then
            tlbRipristina_ItemClick(Nothing, Nothing)
            Return False
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      oCleIstf.bHasChanges = False
      oCleIstf.bParstcaHasChanges = False
      GctlSetVisEnab(grvStca, False)
      '--------------------------------------------------------------------------------------------------------------
      If bDelete = False Then
        dRiga = 0
        If Not grvStca.NTSGetCurrentDataRow Is Nothing Then dRiga = NTSCDec(grvStca.NTSGetCurrentDataRow!pfc_riga)
        If dRiga <> 0 Then
          oCleIstf.bParstcaHasChanges = True
          SalvaRiga(False)
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function SalvaRiga(ByVal bDelete As Boolean) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------      
      If oCleIstf.bParstcaHasChanges = True Then
        '------------------------------------------------------------------------------------------------------------
        If GctlControllaOutNotEqual() = False Then Return False
        '------------------------------------------------------------------------------------------------------------
        If Not oCleIstf.SalvaRiga(bDelete, NTSCInt(edPf_codform.Text), dRiga, dsStca) Then Return False
        '------------------------------------------------------------------------------------------------------------
      End If
      '--------------------------------------------------------------------------------------------------------------
      oCleIstf.bParstcaHasChanges = False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Sub SelezionaTitoloStampa()
    Dim oParam As New CLE__CLDP
    Dim frmHlpf As FRM__HLPF = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      If oCleIstf.RecordIsChanged Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222149531250, _
          "Salvare o ripristinare le modifiche prima di selezionare una nuova impostazione di stampa."))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------      
      frmHlpf = CType(NTSNewFormModal("FRM__HLPF"), FRM__HLPF)
      frmHlpf.Init(oMenu, oParam, DittaCorrente)
      frmHlpf.InitEntity(oCleIstf)
      frmHlpf.ShowDialog()
      If oCleIstf.bHlpfAnnullato = True Then Return
      '--------------------------------------------------------------------------------------------------------------
      If oCleIstf.lHlpfCodform <> NTSCInt(edPf_codform.Text) Then
        '------------------------------------------------------------------------------------------------------------
        '--- Mi sposto sul record selezionato
        '------------------------------------------------------------------------------------------------------------
        For i As Integer = 0 To dcIstf.List.Count - 1
          If CType(dcIstf.Item(i), DataRowView)!pf_codform.ToString = oCleIstf.lHlpfCodform.ToString Then
            dcIstf.Position = i
            tlbSalva_ItemClick(Me, Nothing)
            Exit For
          End If
        Next
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      If Not frmHlpf Is Nothing Then frmHlpf.Dispose()
      frmHlpf = Nothing
    End Try
  End Sub
  
End Class