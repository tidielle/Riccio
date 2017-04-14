Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGRCSM
  Private Moduli_P As Integer = CLN__STD.bsModMG
  Private ModuliExt_P As Integer = CLN__STD.bsModExtMGE
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

#Region "VARIABILI"
  Public oCleRcsm As CLEMGRCSM
  Public oCallParams As CLE__CLDP

  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbElabora As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents lbAammggFin As NTSInformatica.NTSLabel
  Public WithEvents lbAammggIni As NTSInformatica.NTSLabel
  Public WithEvents edAammggFin As NTSInformatica.NTSTextBoxData
  Public WithEvents edAammggIni As NTSInformatica.NTSTextBoxData
  Public WithEvents ckNonmov As NTSInformatica.NTSCheckBox
  Public WithEvents lbScorteGG As NTSInformatica.NTSLabel
  Public WithEvents edScorteGG As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckForzaRRFence As NTSInformatica.NTSCheckBox
  Public WithEvents fmFrame1 As NTSInformatica.NTSGroupBox
  Public WithEvents opFlclavo2 As NTSInformatica.NTSRadioButton
  Public WithEvents opFlclavo1 As NTSInformatica.NTSRadioButton
  Public WithEvents opFlclavo0 As NTSInformatica.NTSRadioButton
  Public WithEvents lbCodmagFin As NTSInformatica.NTSLabel
  Public WithEvents lbCodmagIni As NTSInformatica.NTSLabel
  Public WithEvents lbDescodmagIni As NTSInformatica.NTSLabel
  Public WithEvents edCodmagFin As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCodmagIni As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDescodmagFin As NTSInformatica.NTSLabel
  Public WithEvents lbPercentualeMinima As NTSInformatica.NTSLabel
  Public WithEvents lbPercentualeMassima As NTSInformatica.NTSLabel
  Public WithEvents edPercentualeMinima As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPercentualeMassima As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbLabel2 As NTSInformatica.NTSLabel
  Public WithEvents lbLabel1 As NTSInformatica.NTSLabel
  Public WithEvents edArrotond As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbArrotond As NTSInformatica.NTSLabel
  Public WithEvents fmFrame2 As NTSInformatica.NTSGroupBox
  Public WithEvents ckConsideraV As NTSInformatica.NTSCheckBox
  Public WithEvents ckConsideraP As NTSInformatica.NTSCheckBox
  Public WithEvents ckConsideraA As NTSInformatica.NTSCheckBox
  Public WithEvents fmFrame3 As NTSInformatica.NTSGroupBox
  Public WithEvents opFiltroData0 As NTSInformatica.NTSRadioButton
  Public WithEvents opFiltroData1 As NTSInformatica.NTSRadioButton
  Public WithEvents fmFrame4 As NTSInformatica.NTSGroupBox
  Public WithEvents ckPolriord As NTSInformatica.NTSCheckBox
  Public WithEvents ckScomin As NTSInformatica.NTSCheckBox
  Public WithEvents fmFrame As NTSInformatica.NTSGroupBox
  Public WithEvents ckElaborazioneManuale As NTSInformatica.NTSCheckBox
  Public WithEvents lbInfo As NTSInformatica.NTSLabel
  Public WithEvents pnPanel1 As NTSInformatica.NTSPanel
  Public WithEvents fmTrend As NTSInformatica.NTSGroupBox
  Public WithEvents ckTrend As NTSInformatica.NTSCheckBox
  Public WithEvents lbSecondoPeriodoFin As NTSInformatica.NTSLabel
  Public WithEvents lbPrimoPeriodoFin As NTSInformatica.NTSLabel
  Public WithEvents lbSecondoPeriodoIni As NTSInformatica.NTSLabel
  Public WithEvents lbPrimoPeriodoIni As NTSInformatica.NTSLabel
  Public WithEvents edSecondoPeriodoFin As NTSInformatica.NTSTextBoxData
  Public WithEvents edSecondoPeriodoIni As NTSInformatica.NTSTextBoxData
  Public WithEvents edPrimoPeriodoFin As NTSInformatica.NTSTextBoxData
  Public WithEvents edPrimoPeriodoIni As NTSInformatica.NTSTextBoxData
  Public WithEvents ckEscludifestivi As NTSInformatica.NTSCheckBox
  Public WithEvents ckAggiornaARTMAGA As NTSInformatica.NTSCheckBox

  Private components As System.ComponentModel.IContainer
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
    'creo e attivo l'entity e inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGRCSM", "BEMGRCSM", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128575778434222652, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleRcsm = CType(oTmp, CLEMGRCSM)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGRCSM", strRemoteServer, strRemotePort)
    AddHandler oCleRcsm.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleRcsm.Init(oApp, oScript, oMenu.oCleComm, "TTARTINVF", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGRCSM))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbElabora = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.lbAammggIni = New NTSInformatica.NTSLabel
    Me.lbAammggFin = New NTSInformatica.NTSLabel
    Me.edAammggIni = New NTSInformatica.NTSTextBoxData
    Me.edAammggFin = New NTSInformatica.NTSTextBoxData
    Me.ckNonmov = New NTSInformatica.NTSCheckBox
    Me.lbScorteGG = New NTSInformatica.NTSLabel
    Me.edScorteGG = New NTSInformatica.NTSTextBoxNum
    Me.ckForzaRRFence = New NTSInformatica.NTSCheckBox
    Me.fmFrame1 = New NTSInformatica.NTSGroupBox
    Me.lbDescodmagFin = New NTSInformatica.NTSLabel
    Me.lbDescodmagIni = New NTSInformatica.NTSLabel
    Me.edCodmagFin = New NTSInformatica.NTSTextBoxNum
    Me.edCodmagIni = New NTSInformatica.NTSTextBoxNum
    Me.lbCodmagFin = New NTSInformatica.NTSLabel
    Me.lbCodmagIni = New NTSInformatica.NTSLabel
    Me.opFlclavo2 = New NTSInformatica.NTSRadioButton
    Me.opFlclavo1 = New NTSInformatica.NTSRadioButton
    Me.opFlclavo0 = New NTSInformatica.NTSRadioButton
    Me.lbPercentualeMassima = New NTSInformatica.NTSLabel
    Me.lbPercentualeMinima = New NTSInformatica.NTSLabel
    Me.edPercentualeMassima = New NTSInformatica.NTSTextBoxNum
    Me.edPercentualeMinima = New NTSInformatica.NTSTextBoxNum
    Me.lbLabel1 = New NTSInformatica.NTSLabel
    Me.lbLabel2 = New NTSInformatica.NTSLabel
    Me.edArrotond = New NTSInformatica.NTSTextBoxNum
    Me.lbArrotond = New NTSInformatica.NTSLabel
    Me.fmFrame2 = New NTSInformatica.NTSGroupBox
    Me.ckConsideraA = New NTSInformatica.NTSCheckBox
    Me.ckConsideraP = New NTSInformatica.NTSCheckBox
    Me.ckConsideraV = New NTSInformatica.NTSCheckBox
    Me.fmFrame3 = New NTSInformatica.NTSGroupBox
    Me.opFiltroData1 = New NTSInformatica.NTSRadioButton
    Me.opFiltroData0 = New NTSInformatica.NTSRadioButton
    Me.fmFrame4 = New NTSInformatica.NTSGroupBox
    Me.ckScomin = New NTSInformatica.NTSCheckBox
    Me.ckPolriord = New NTSInformatica.NTSCheckBox
    Me.fmFrame = New NTSInformatica.NTSGroupBox
    Me.ckElaborazioneManuale = New NTSInformatica.NTSCheckBox
    Me.lbInfo = New NTSInformatica.NTSLabel
    Me.pnPanel1 = New NTSInformatica.NTSPanel
    Me.fmTrend = New NTSInformatica.NTSGroupBox
    Me.edSecondoPeriodoFin = New NTSInformatica.NTSTextBoxData
    Me.edSecondoPeriodoIni = New NTSInformatica.NTSTextBoxData
    Me.edPrimoPeriodoFin = New NTSInformatica.NTSTextBoxData
    Me.edPrimoPeriodoIni = New NTSInformatica.NTSTextBoxData
    Me.lbSecondoPeriodoFin = New NTSInformatica.NTSLabel
    Me.lbPrimoPeriodoFin = New NTSInformatica.NTSLabel
    Me.lbSecondoPeriodoIni = New NTSInformatica.NTSLabel
    Me.lbPrimoPeriodoIni = New NTSInformatica.NTSLabel
    Me.ckTrend = New NTSInformatica.NTSCheckBox
    Me.ckEscludifestivi = New NTSInformatica.NTSCheckBox
    Me.ckAggiornaARTMAGA = New NTSInformatica.NTSCheckBox
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAammggIni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAammggFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNonmov.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edScorteGG.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckForzaRRFence.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmFrame1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmFrame1.SuspendLayout()
    CType(Me.edCodmagFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodmagIni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opFlclavo2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opFlclavo1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opFlclavo0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPercentualeMassima.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPercentualeMinima.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edArrotond.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmFrame2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmFrame2.SuspendLayout()
    CType(Me.ckConsideraA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckConsideraP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckConsideraV.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmFrame3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmFrame3.SuspendLayout()
    CType(Me.opFiltroData1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opFiltroData0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmFrame4, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmFrame4.SuspendLayout()
    CType(Me.ckScomin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckPolriord.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmFrame, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmFrame.SuspendLayout()
    CType(Me.ckElaborazioneManuale.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPanel1.SuspendLayout()
    CType(Me.fmTrend, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmTrend.SuspendLayout()
    CType(Me.edSecondoPeriodoFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSecondoPeriodoIni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPrimoPeriodoFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPrimoPeriodoIni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTrend.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckEscludifestivi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAggiornaARTMAGA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbZoom, Me.tlbElabora, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 4
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbElabora), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbElabora.Id = 1
    Me.tlbElabora.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbElabora.Name = "tlbElabora"
    Me.tlbElabora.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 0
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 2
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 3
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(12, 36)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.Size = New System.Drawing.Size(171, 23)
    Me.cmdSeleziona.TabIndex = 4
    Me.cmdSeleziona.Text = "Seleziona articoli"
    '
    'lbAammggIni
    '
    Me.lbAammggIni.AutoSize = True
    Me.lbAammggIni.BackColor = System.Drawing.Color.Transparent
    Me.lbAammggIni.Location = New System.Drawing.Point(13, 68)
    Me.lbAammggIni.Name = "lbAammggIni"
    Me.lbAammggIni.NTSDbField = ""
    Me.lbAammggIni.Size = New System.Drawing.Size(160, 13)
    Me.lbAammggIni.TabIndex = 5
    Me.lbAammggIni.Text = "Da data movimenti di magazzino"
    Me.lbAammggIni.Tooltip = ""
    Me.lbAammggIni.UseMnemonic = False
    '
    'lbAammggFin
    '
    Me.lbAammggFin.AutoSize = True
    Me.lbAammggFin.BackColor = System.Drawing.Color.Transparent
    Me.lbAammggFin.Location = New System.Drawing.Point(278, 68)
    Me.lbAammggFin.Name = "lbAammggFin"
    Me.lbAammggFin.NTSDbField = ""
    Me.lbAammggFin.Size = New System.Drawing.Size(153, 13)
    Me.lbAammggFin.TabIndex = 6
    Me.lbAammggFin.Text = "a data movimenti di magazzino"
    Me.lbAammggFin.Tooltip = ""
    Me.lbAammggFin.UseMnemonic = False
    '
    'edAammggIni
    '
    Me.edAammggIni.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAammggIni.Location = New System.Drawing.Point(179, 65)
    Me.edAammggIni.Name = "edAammggIni"
    Me.edAammggIni.NTSDbField = ""
    Me.edAammggIni.NTSForzaVisZoom = False
    Me.edAammggIni.NTSOldValue = ""
    Me.edAammggIni.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAammggIni.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAammggIni.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAammggIni.Properties.MaxLength = 65536
    Me.edAammggIni.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAammggIni.Size = New System.Drawing.Size(84, 20)
    Me.edAammggIni.TabIndex = 7
    '
    'edAammggFin
    '
    Me.edAammggFin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAammggFin.Location = New System.Drawing.Point(437, 65)
    Me.edAammggFin.Name = "edAammggFin"
    Me.edAammggFin.NTSDbField = ""
    Me.edAammggFin.NTSForzaVisZoom = False
    Me.edAammggFin.NTSOldValue = ""
    Me.edAammggFin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAammggFin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAammggFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAammggFin.Properties.MaxLength = 65536
    Me.edAammggFin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAammggFin.Size = New System.Drawing.Size(84, 20)
    Me.edAammggFin.TabIndex = 8
    '
    'ckNonmov
    '
    Me.ckNonmov.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckNonmov.Location = New System.Drawing.Point(543, 87)
    Me.ckNonmov.Name = "ckNonmov"
    Me.ckNonmov.NTSCheckValue = "S"
    Me.ckNonmov.NTSUnCheckValue = "N"
    Me.ckNonmov.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNonmov.Properties.Appearance.Options.UseBackColor = True
    Me.ckNonmov.Properties.Caption = "Anche articoli non movimentati nel periodo"
    Me.ckNonmov.Size = New System.Drawing.Size(229, 19)
    Me.ckNonmov.TabIndex = 9
    '
    'lbScorteGG
    '
    Me.lbScorteGG.AutoSize = True
    Me.lbScorteGG.BackColor = System.Drawing.Color.Transparent
    Me.lbScorteGG.Location = New System.Drawing.Point(13, 90)
    Me.lbScorteGG.Name = "lbScorteGG"
    Me.lbScorteGG.NTSDbField = ""
    Me.lbScorteGG.Size = New System.Drawing.Size(150, 13)
    Me.lbScorteGG.TabIndex = 10
    Me.lbScorteGG.Text = "Tempo copertura scorte in GG"
    Me.lbScorteGG.Tooltip = ""
    Me.lbScorteGG.UseMnemonic = False
    '
    'edScorteGG
    '
    Me.edScorteGG.Cursor = System.Windows.Forms.Cursors.Default
    Me.edScorteGG.Location = New System.Drawing.Point(179, 87)
    Me.edScorteGG.Name = "edScorteGG"
    Me.edScorteGG.NTSDbField = ""
    Me.edScorteGG.NTSFormat = "0"
    Me.edScorteGG.NTSForzaVisZoom = False
    Me.edScorteGG.NTSOldValue = ""
    Me.edScorteGG.Properties.Appearance.Options.UseTextOptions = True
    Me.edScorteGG.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edScorteGG.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edScorteGG.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edScorteGG.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edScorteGG.Properties.MaxLength = 65536
    Me.edScorteGG.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edScorteGG.Size = New System.Drawing.Size(51, 20)
    Me.edScorteGG.TabIndex = 11
    '
    'ckForzaRRFence
    '
    Me.ckForzaRRFence.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckForzaRRFence.Location = New System.Drawing.Point(281, 89)
    Me.ckForzaRRFence.Name = "ckForzaRRFence"
    Me.ckForzaRRFence.NTSCheckValue = "S"
    Me.ckForzaRRFence.NTSUnCheckValue = "N"
    Me.ckForzaRRFence.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckForzaRRFence.Properties.Appearance.Options.UseBackColor = True
    Me.ckForzaRRFence.Properties.Caption = "Forza anche su articoli con RR Fence indicato"
    Me.ckForzaRRFence.Size = New System.Drawing.Size(256, 19)
    Me.ckForzaRRFence.TabIndex = 12
    '
    'fmFrame1
    '
    Me.fmFrame1.AllowDrop = True
    Me.fmFrame1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmFrame1.Appearance.Options.UseBackColor = True
    Me.fmFrame1.Controls.Add(Me.lbDescodmagFin)
    Me.fmFrame1.Controls.Add(Me.lbDescodmagIni)
    Me.fmFrame1.Controls.Add(Me.edCodmagFin)
    Me.fmFrame1.Controls.Add(Me.edCodmagIni)
    Me.fmFrame1.Controls.Add(Me.lbCodmagFin)
    Me.fmFrame1.Controls.Add(Me.lbCodmagIni)
    Me.fmFrame1.Controls.Add(Me.opFlclavo2)
    Me.fmFrame1.Controls.Add(Me.opFlclavo1)
    Me.fmFrame1.Controls.Add(Me.opFlclavo0)
    Me.fmFrame1.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmFrame1.Location = New System.Drawing.Point(16, 113)
    Me.fmFrame1.Name = "fmFrame1"
    Me.fmFrame1.Size = New System.Drawing.Size(756, 100)
    Me.fmFrame1.TabIndex = 13
    Me.fmFrame1.Text = "Considera magazzini:"
    '
    'lbDescodmagFin
    '
    Me.lbDescodmagFin.BackColor = System.Drawing.Color.Transparent
    Me.lbDescodmagFin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescodmagFin.Location = New System.Drawing.Point(523, 71)
    Me.lbDescodmagFin.Name = "lbDescodmagFin"
    Me.lbDescodmagFin.NTSDbField = ""
    Me.lbDescodmagFin.Size = New System.Drawing.Size(216, 20)
    Me.lbDescodmagFin.TabIndex = 9
    Me.lbDescodmagFin.Tooltip = ""
    Me.lbDescodmagFin.UseMnemonic = False
    '
    'lbDescodmagIni
    '
    Me.lbDescodmagIni.BackColor = System.Drawing.Color.Transparent
    Me.lbDescodmagIni.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescodmagIni.Location = New System.Drawing.Point(220, 71)
    Me.lbDescodmagIni.Name = "lbDescodmagIni"
    Me.lbDescodmagIni.NTSDbField = ""
    Me.lbDescodmagIni.Size = New System.Drawing.Size(216, 20)
    Me.lbDescodmagIni.TabIndex = 8
    Me.lbDescodmagIni.Tooltip = ""
    Me.lbDescodmagIni.UseMnemonic = False
    '
    'edCodmagFin
    '
    Me.edCodmagFin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodmagFin.Enabled = False
    Me.edCodmagFin.Location = New System.Drawing.Point(466, 71)
    Me.edCodmagFin.Name = "edCodmagFin"
    Me.edCodmagFin.NTSDbField = ""
    Me.edCodmagFin.NTSFormat = "0"
    Me.edCodmagFin.NTSForzaVisZoom = False
    Me.edCodmagFin.NTSOldValue = ""
    Me.edCodmagFin.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodmagFin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodmagFin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodmagFin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodmagFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodmagFin.Properties.MaxLength = 65536
    Me.edCodmagFin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodmagFin.Size = New System.Drawing.Size(53, 20)
    Me.edCodmagFin.TabIndex = 7
    '
    'edCodmagIni
    '
    Me.edCodmagIni.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodmagIni.Enabled = False
    Me.edCodmagIni.Location = New System.Drawing.Point(161, 71)
    Me.edCodmagIni.Name = "edCodmagIni"
    Me.edCodmagIni.NTSDbField = ""
    Me.edCodmagIni.NTSFormat = "0"
    Me.edCodmagIni.NTSForzaVisZoom = False
    Me.edCodmagIni.NTSOldValue = ""
    Me.edCodmagIni.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodmagIni.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodmagIni.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodmagIni.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodmagIni.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodmagIni.Properties.MaxLength = 65536
    Me.edCodmagIni.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodmagIni.Size = New System.Drawing.Size(53, 20)
    Me.edCodmagIni.TabIndex = 5
    '
    'lbCodmagFin
    '
    Me.lbCodmagFin.AutoSize = True
    Me.lbCodmagFin.BackColor = System.Drawing.Color.Transparent
    Me.lbCodmagFin.Enabled = False
    Me.lbCodmagFin.Location = New System.Drawing.Point(447, 74)
    Me.lbCodmagFin.Name = "lbCodmagFin"
    Me.lbCodmagFin.NTSDbField = ""
    Me.lbCodmagFin.Size = New System.Drawing.Size(13, 13)
    Me.lbCodmagFin.TabIndex = 4
    Me.lbCodmagFin.Text = "a"
    Me.lbCodmagFin.Tooltip = ""
    Me.lbCodmagFin.UseMnemonic = False
    '
    'lbCodmagIni
    '
    Me.lbCodmagIni.AutoSize = True
    Me.lbCodmagIni.BackColor = System.Drawing.Color.Transparent
    Me.lbCodmagIni.Enabled = False
    Me.lbCodmagIni.Location = New System.Drawing.Point(138, 74)
    Me.lbCodmagIni.Name = "lbCodmagIni"
    Me.lbCodmagIni.NTSDbField = ""
    Me.lbCodmagIni.Size = New System.Drawing.Size(20, 13)
    Me.lbCodmagIni.TabIndex = 3
    Me.lbCodmagIni.Text = "Da"
    Me.lbCodmagIni.Tooltip = ""
    Me.lbCodmagIni.UseMnemonic = False
    '
    'opFlclavo2
    '
    Me.opFlclavo2.Cursor = System.Windows.Forms.Cursors.Default
    Me.opFlclavo2.Location = New System.Drawing.Point(15, 71)
    Me.opFlclavo2.Name = "opFlclavo2"
    Me.opFlclavo2.NTSCheckValue = "S"
    Me.opFlclavo2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opFlclavo2.Properties.Appearance.Options.UseBackColor = True
    Me.opFlclavo2.Properties.Caption = "Seleziona magazzini"
    Me.opFlclavo2.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opFlclavo2.Size = New System.Drawing.Size(119, 19)
    Me.opFlclavo2.TabIndex = 2
    '
    'opFlclavo1
    '
    Me.opFlclavo1.Cursor = System.Windows.Forms.Cursors.Default
    Me.opFlclavo1.Location = New System.Drawing.Point(15, 47)
    Me.opFlclavo1.Name = "opFlclavo1"
    Me.opFlclavo1.NTSCheckValue = "S"
    Me.opFlclavo1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opFlclavo1.Properties.Appearance.Options.UseBackColor = True
    Me.opFlclavo1.Properties.Caption = "Solo merce propria"
    Me.opFlclavo1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opFlclavo1.Size = New System.Drawing.Size(132, 19)
    Me.opFlclavo1.TabIndex = 1
    '
    'opFlclavo0
    '
    Me.opFlclavo0.Cursor = System.Windows.Forms.Cursors.Default
    Me.opFlclavo0.EditValue = True
    Me.opFlclavo0.Location = New System.Drawing.Point(15, 23)
    Me.opFlclavo0.Name = "opFlclavo0"
    Me.opFlclavo0.NTSCheckValue = "S"
    Me.opFlclavo0.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opFlclavo0.Properties.Appearance.Options.UseBackColor = True
    Me.opFlclavo0.Properties.Caption = "Tutti"
    Me.opFlclavo0.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opFlclavo0.Size = New System.Drawing.Size(75, 19)
    Me.opFlclavo0.TabIndex = 0
    '
    'lbPercentualeMassima
    '
    Me.lbPercentualeMassima.AutoSize = True
    Me.lbPercentualeMassima.BackColor = System.Drawing.Color.Transparent
    Me.lbPercentualeMassima.Location = New System.Drawing.Point(1, 13)
    Me.lbPercentualeMassima.Name = "lbPercentualeMassima"
    Me.lbPercentualeMassima.NTSDbField = ""
    Me.lbPercentualeMassima.Size = New System.Drawing.Size(283, 13)
    Me.lbPercentualeMassima.TabIndex = 14
    Me.lbPercentualeMassima.Text = "Percentuale massima variazione scorta minima in aumento"
    Me.lbPercentualeMassima.Tooltip = ""
    Me.lbPercentualeMassima.UseMnemonic = False
    '
    'lbPercentualeMinima
    '
    Me.lbPercentualeMinima.AutoSize = True
    Me.lbPercentualeMinima.BackColor = System.Drawing.Color.Transparent
    Me.lbPercentualeMinima.Location = New System.Drawing.Point(1, 35)
    Me.lbPercentualeMinima.Name = "lbPercentualeMinima"
    Me.lbPercentualeMinima.NTSDbField = ""
    Me.lbPercentualeMinima.Size = New System.Drawing.Size(296, 13)
    Me.lbPercentualeMinima.TabIndex = 15
    Me.lbPercentualeMinima.Text = "Percentuale massima variazione scorta minima in diminuzione"
    Me.lbPercentualeMinima.Tooltip = ""
    Me.lbPercentualeMinima.UseMnemonic = False
    '
    'edPercentualeMassima
    '
    Me.edPercentualeMassima.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPercentualeMassima.EditValue = "1"
    Me.edPercentualeMassima.Location = New System.Drawing.Point(305, 10)
    Me.edPercentualeMassima.Name = "edPercentualeMassima"
    Me.edPercentualeMassima.NTSDbField = ""
    Me.edPercentualeMassima.NTSFormat = "0"
    Me.edPercentualeMassima.NTSForzaVisZoom = False
    Me.edPercentualeMassima.NTSOldValue = "1"
    Me.edPercentualeMassima.Properties.Appearance.Options.UseTextOptions = True
    Me.edPercentualeMassima.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPercentualeMassima.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPercentualeMassima.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPercentualeMassima.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPercentualeMassima.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPercentualeMassima.Size = New System.Drawing.Size(61, 20)
    Me.edPercentualeMassima.TabIndex = 16
    '
    'edPercentualeMinima
    '
    Me.edPercentualeMinima.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPercentualeMinima.EditValue = "1"
    Me.edPercentualeMinima.Location = New System.Drawing.Point(305, 32)
    Me.edPercentualeMinima.Name = "edPercentualeMinima"
    Me.edPercentualeMinima.NTSDbField = ""
    Me.edPercentualeMinima.NTSFormat = "0"
    Me.edPercentualeMinima.NTSForzaVisZoom = False
    Me.edPercentualeMinima.NTSOldValue = "1"
    Me.edPercentualeMinima.Properties.Appearance.Options.UseTextOptions = True
    Me.edPercentualeMinima.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPercentualeMinima.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPercentualeMinima.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPercentualeMinima.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPercentualeMinima.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPercentualeMinima.Size = New System.Drawing.Size(61, 20)
    Me.edPercentualeMinima.TabIndex = 17
    '
    'lbLabel1
    '
    Me.lbLabel1.AutoSize = True
    Me.lbLabel1.BackColor = System.Drawing.Color.Transparent
    Me.lbLabel1.Location = New System.Drawing.Point(372, 13)
    Me.lbLabel1.Name = "lbLabel1"
    Me.lbLabel1.NTSDbField = ""
    Me.lbLabel1.Size = New System.Drawing.Size(18, 13)
    Me.lbLabel1.TabIndex = 18
    Me.lbLabel1.Text = "%"
    Me.lbLabel1.Tooltip = ""
    Me.lbLabel1.UseMnemonic = False
    '
    'lbLabel2
    '
    Me.lbLabel2.AutoSize = True
    Me.lbLabel2.BackColor = System.Drawing.Color.Transparent
    Me.lbLabel2.Location = New System.Drawing.Point(372, 35)
    Me.lbLabel2.Name = "lbLabel2"
    Me.lbLabel2.NTSDbField = ""
    Me.lbLabel2.Size = New System.Drawing.Size(18, 13)
    Me.lbLabel2.TabIndex = 19
    Me.lbLabel2.Text = "%"
    Me.lbLabel2.Tooltip = ""
    Me.lbLabel2.UseMnemonic = False
    '
    'edArrotond
    '
    Me.edArrotond.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edArrotond.EditValue = "1"
    Me.edArrotond.Location = New System.Drawing.Point(305, 54)
    Me.edArrotond.Name = "edArrotond"
    Me.edArrotond.NTSDbField = ""
    Me.edArrotond.NTSFormat = "0"
    Me.edArrotond.NTSForzaVisZoom = False
    Me.edArrotond.NTSOldValue = "1"
    Me.edArrotond.Properties.Appearance.Options.UseTextOptions = True
    Me.edArrotond.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edArrotond.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edArrotond.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edArrotond.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edArrotond.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edArrotond.Size = New System.Drawing.Size(61, 20)
    Me.edArrotond.TabIndex = 20
    '
    'lbArrotond
    '
    Me.lbArrotond.AutoSize = True
    Me.lbArrotond.BackColor = System.Drawing.Color.Transparent
    Me.lbArrotond.Location = New System.Drawing.Point(1, 57)
    Me.lbArrotond.Name = "lbArrotond"
    Me.lbArrotond.NTSDbField = ""
    Me.lbArrotond.Size = New System.Drawing.Size(134, 13)
    Me.lbArrotond.TabIndex = 21
    Me.lbArrotond.Text = "Arrotondamento di default"
    Me.lbArrotond.Tooltip = ""
    Me.lbArrotond.UseMnemonic = False
    '
    'fmFrame2
    '
    Me.fmFrame2.AllowDrop = True
    Me.fmFrame2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmFrame2.Appearance.Options.UseBackColor = True
    Me.fmFrame2.Controls.Add(Me.ckConsideraA)
    Me.fmFrame2.Controls.Add(Me.ckConsideraP)
    Me.fmFrame2.Controls.Add(Me.ckConsideraV)
    Me.fmFrame2.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmFrame2.Location = New System.Drawing.Point(16, 297)
    Me.fmFrame2.Name = "fmFrame2"
    Me.fmFrame2.Size = New System.Drawing.Size(189, 78)
    Me.fmFrame2.TabIndex = 22
    Me.fmFrame2.Text = "Considera:"
    '
    'ckConsideraA
    '
    Me.ckConsideraA.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckConsideraA.EditValue = True
    Me.ckConsideraA.Location = New System.Drawing.Point(15, 55)
    Me.ckConsideraA.Name = "ckConsideraA"
    Me.ckConsideraA.NTSCheckValue = "S"
    Me.ckConsideraA.NTSUnCheckValue = "N"
    Me.ckConsideraA.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckConsideraA.Properties.Appearance.Options.UseBackColor = True
    Me.ckConsideraA.Properties.Caption = "Altri scarichi"
    Me.ckConsideraA.Size = New System.Drawing.Size(127, 19)
    Me.ckConsideraA.TabIndex = 2
    '
    'ckConsideraP
    '
    Me.ckConsideraP.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckConsideraP.EditValue = True
    Me.ckConsideraP.Location = New System.Drawing.Point(15, 39)
    Me.ckConsideraP.Name = "ckConsideraP"
    Me.ckConsideraP.NTSCheckValue = "S"
    Me.ckConsideraP.NTSUnCheckValue = "N"
    Me.ckConsideraP.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckConsideraP.Properties.Appearance.Options.UseBackColor = True
    Me.ckConsideraP.Properties.Caption = "Scarichi a produzione"
    Me.ckConsideraP.Size = New System.Drawing.Size(127, 19)
    Me.ckConsideraP.TabIndex = 1
    '
    'ckConsideraV
    '
    Me.ckConsideraV.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckConsideraV.EditValue = True
    Me.ckConsideraV.Location = New System.Drawing.Point(15, 23)
    Me.ckConsideraV.Name = "ckConsideraV"
    Me.ckConsideraV.NTSCheckValue = "S"
    Me.ckConsideraV.NTSUnCheckValue = "N"
    Me.ckConsideraV.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckConsideraV.Properties.Appearance.Options.UseBackColor = True
    Me.ckConsideraV.Properties.Caption = "Scarichi vendite"
    Me.ckConsideraV.Size = New System.Drawing.Size(127, 19)
    Me.ckConsideraV.TabIndex = 0
    '
    'fmFrame3
    '
    Me.fmFrame3.AllowDrop = True
    Me.fmFrame3.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmFrame3.Appearance.Options.UseBackColor = True
    Me.fmFrame3.Controls.Add(Me.opFiltroData1)
    Me.fmFrame3.Controls.Add(Me.opFiltroData0)
    Me.fmFrame3.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmFrame3.Location = New System.Drawing.Point(211, 297)
    Me.fmFrame3.Name = "fmFrame3"
    Me.fmFrame3.Size = New System.Drawing.Size(195, 78)
    Me.fmFrame3.TabIndex = 23
    Me.fmFrame3.Text = "Tipo filtro data su Impegni:"
    '
    'opFiltroData1
    '
    Me.opFiltroData1.Cursor = System.Windows.Forms.Cursors.Default
    Me.opFiltroData1.Location = New System.Drawing.Point(15, 49)
    Me.opFiltroData1.Name = "opFiltroData1"
    Me.opFiltroData1.NTSCheckValue = "S"
    Me.opFiltroData1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opFiltroData1.Properties.Appearance.Options.UseBackColor = True
    Me.opFiltroData1.Properties.Caption = "Data consegna"
    Me.opFiltroData1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opFiltroData1.Size = New System.Drawing.Size(112, 19)
    Me.opFiltroData1.TabIndex = 1
    '
    'opFiltroData0
    '
    Me.opFiltroData0.Cursor = System.Windows.Forms.Cursors.Default
    Me.opFiltroData0.EditValue = True
    Me.opFiltroData0.Location = New System.Drawing.Point(15, 29)
    Me.opFiltroData0.Name = "opFiltroData0"
    Me.opFiltroData0.NTSCheckValue = "S"
    Me.opFiltroData0.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opFiltroData0.Properties.Appearance.Options.UseBackColor = True
    Me.opFiltroData0.Properties.Caption = "Data Ordine"
    Me.opFiltroData0.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opFiltroData0.Size = New System.Drawing.Size(112, 19)
    Me.opFiltroData0.TabIndex = 0
    '
    'fmFrame4
    '
    Me.fmFrame4.AllowDrop = True
    Me.fmFrame4.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmFrame4.Appearance.Options.UseBackColor = True
    Me.fmFrame4.Controls.Add(Me.ckAggiornaARTMAGA)
    Me.fmFrame4.Controls.Add(Me.ckScomin)
    Me.fmFrame4.Controls.Add(Me.ckPolriord)
    Me.fmFrame4.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmFrame4.Location = New System.Drawing.Point(16, 381)
    Me.fmFrame4.Name = "fmFrame4"
    Me.fmFrame4.Size = New System.Drawing.Size(390, 78)
    Me.fmFrame4.TabIndex = 24
    Me.fmFrame4.Text = "Altre opzioni:"
    '
    'ckScomin
    '
    Me.ckScomin.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckScomin.Location = New System.Drawing.Point(15, 38)
    Me.ckScomin.Name = "ckScomin"
    Me.ckScomin.NTSCheckValue = "S"
    Me.ckScomin.NTSUnCheckValue = "N"
    Me.ckScomin.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckScomin.Properties.Appearance.Options.UseBackColor = True
    Me.ckScomin.Properties.Caption = "Considera solo gli articoli con scorta minima pari a zero"
    Me.ckScomin.Size = New System.Drawing.Size(319, 19)
    Me.ckScomin.TabIndex = 1
    '
    'ckPolriord
    '
    Me.ckPolriord.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckPolriord.EditValue = True
    Me.ckPolriord.Location = New System.Drawing.Point(15, 20)
    Me.ckPolriord.Name = "ckPolriord"
    Me.ckPolriord.NTSCheckValue = "S"
    Me.ckPolriord.NTSUnCheckValue = "N"
    Me.ckPolriord.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPolriord.Properties.Appearance.Options.UseBackColor = True
    Me.ckPolriord.Properties.Caption = "Considera solo gli articoli con politica a punto di riordino"
    Me.ckPolriord.Size = New System.Drawing.Size(297, 19)
    Me.ckPolriord.TabIndex = 0
    '
    'fmFrame
    '
    Me.fmFrame.AllowDrop = True
    Me.fmFrame.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmFrame.Appearance.Options.UseBackColor = True
    Me.fmFrame.Controls.Add(Me.ckElaborazioneManuale)
    Me.fmFrame.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmFrame.Location = New System.Drawing.Point(547, 409)
    Me.fmFrame.Name = "fmFrame"
    Me.fmFrame.Size = New System.Drawing.Size(225, 50)
    Me.fmFrame.TabIndex = 25
    Me.fmFrame.Text = "Tipo elaborazione:"
    '
    'ckElaborazioneManuale
    '
    Me.ckElaborazioneManuale.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckElaborazioneManuale.EditValue = True
    Me.ckElaborazioneManuale.Location = New System.Drawing.Point(15, 23)
    Me.ckElaborazioneManuale.Name = "ckElaborazioneManuale"
    Me.ckElaborazioneManuale.NTSCheckValue = "S"
    Me.ckElaborazioneManuale.NTSUnCheckValue = "N"
    Me.ckElaborazioneManuale.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckElaborazioneManuale.Properties.Appearance.Options.UseBackColor = True
    Me.ckElaborazioneManuale.Properties.Caption = "Elaborazione manuale"
    Me.ckElaborazioneManuale.Size = New System.Drawing.Size(144, 19)
    Me.ckElaborazioneManuale.TabIndex = 0
    '
    'lbInfo
    '
    Me.lbInfo.BackColor = System.Drawing.Color.Transparent
    Me.lbInfo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbInfo.Location = New System.Drawing.Point(9, 481)
    Me.lbInfo.Name = "lbInfo"
    Me.lbInfo.NTSDbField = ""
    Me.lbInfo.Size = New System.Drawing.Size(770, 13)
    Me.lbInfo.TabIndex = 26
    Me.lbInfo.Text = "Nessuna elaborazione in corso"
    Me.lbInfo.Tooltip = ""
    Me.lbInfo.UseMnemonic = False
    '
    'pnPanel1
    '
    Me.pnPanel1.AllowDrop = True
    Me.pnPanel1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPanel1.Appearance.Options.UseBackColor = True
    Me.pnPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPanel1.Controls.Add(Me.lbPercentualeMassima)
    Me.pnPanel1.Controls.Add(Me.lbPercentualeMinima)
    Me.pnPanel1.Controls.Add(Me.edPercentualeMassima)
    Me.pnPanel1.Controls.Add(Me.edPercentualeMinima)
    Me.pnPanel1.Controls.Add(Me.lbLabel1)
    Me.pnPanel1.Controls.Add(Me.lbLabel2)
    Me.pnPanel1.Controls.Add(Me.lbArrotond)
    Me.pnPanel1.Controls.Add(Me.edArrotond)
    Me.pnPanel1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPanel1.Location = New System.Drawing.Point(12, 215)
    Me.pnPanel1.Name = "pnPanel1"
    Me.pnPanel1.Size = New System.Drawing.Size(394, 80)
    Me.pnPanel1.TabIndex = 27
    Me.pnPanel1.Text = "NtsPanel1"
    '
    'fmTrend
    '
    Me.fmTrend.AllowDrop = True
    Me.fmTrend.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmTrend.Appearance.Options.UseBackColor = True
    Me.fmTrend.Controls.Add(Me.edSecondoPeriodoFin)
    Me.fmTrend.Controls.Add(Me.edSecondoPeriodoIni)
    Me.fmTrend.Controls.Add(Me.edPrimoPeriodoFin)
    Me.fmTrend.Controls.Add(Me.edPrimoPeriodoIni)
    Me.fmTrend.Controls.Add(Me.lbSecondoPeriodoFin)
    Me.fmTrend.Controls.Add(Me.lbPrimoPeriodoFin)
    Me.fmTrend.Controls.Add(Me.lbSecondoPeriodoIni)
    Me.fmTrend.Controls.Add(Me.lbPrimoPeriodoIni)
    Me.fmTrend.Controls.Add(Me.ckTrend)
    Me.fmTrend.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmTrend.Location = New System.Drawing.Point(412, 297)
    Me.fmTrend.Name = "fmTrend"
    Me.fmTrend.Size = New System.Drawing.Size(360, 78)
    Me.fmTrend.TabIndex = 28
    '
    'edSecondoPeriodoFin
    '
    Me.edSecondoPeriodoFin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSecondoPeriodoFin.Enabled = False
    Me.edSecondoPeriodoFin.Location = New System.Drawing.Point(259, 48)
    Me.edSecondoPeriodoFin.Name = "edSecondoPeriodoFin"
    Me.edSecondoPeriodoFin.NTSDbField = ""
    Me.edSecondoPeriodoFin.NTSForzaVisZoom = False
    Me.edSecondoPeriodoFin.NTSOldValue = ""
    Me.edSecondoPeriodoFin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSecondoPeriodoFin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSecondoPeriodoFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSecondoPeriodoFin.Properties.MaxLength = 65536
    Me.edSecondoPeriodoFin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSecondoPeriodoFin.Size = New System.Drawing.Size(84, 20)
    Me.edSecondoPeriodoFin.TabIndex = 18
    '
    'edSecondoPeriodoIni
    '
    Me.edSecondoPeriodoIni.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSecondoPeriodoIni.Enabled = False
    Me.edSecondoPeriodoIni.Location = New System.Drawing.Point(127, 48)
    Me.edSecondoPeriodoIni.Name = "edSecondoPeriodoIni"
    Me.edSecondoPeriodoIni.NTSDbField = ""
    Me.edSecondoPeriodoIni.NTSForzaVisZoom = False
    Me.edSecondoPeriodoIni.NTSOldValue = ""
    Me.edSecondoPeriodoIni.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSecondoPeriodoIni.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSecondoPeriodoIni.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSecondoPeriodoIni.Properties.MaxLength = 65536
    Me.edSecondoPeriodoIni.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSecondoPeriodoIni.Size = New System.Drawing.Size(84, 20)
    Me.edSecondoPeriodoIni.TabIndex = 17
    '
    'edPrimoPeriodoFin
    '
    Me.edPrimoPeriodoFin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPrimoPeriodoFin.Enabled = False
    Me.edPrimoPeriodoFin.Location = New System.Drawing.Point(259, 25)
    Me.edPrimoPeriodoFin.Name = "edPrimoPeriodoFin"
    Me.edPrimoPeriodoFin.NTSDbField = ""
    Me.edPrimoPeriodoFin.NTSForzaVisZoom = False
    Me.edPrimoPeriodoFin.NTSOldValue = ""
    Me.edPrimoPeriodoFin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPrimoPeriodoFin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPrimoPeriodoFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPrimoPeriodoFin.Properties.MaxLength = 65536
    Me.edPrimoPeriodoFin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPrimoPeriodoFin.Size = New System.Drawing.Size(84, 20)
    Me.edPrimoPeriodoFin.TabIndex = 16
    '
    'edPrimoPeriodoIni
    '
    Me.edPrimoPeriodoIni.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPrimoPeriodoIni.Enabled = False
    Me.edPrimoPeriodoIni.Location = New System.Drawing.Point(127, 25)
    Me.edPrimoPeriodoIni.Name = "edPrimoPeriodoIni"
    Me.edPrimoPeriodoIni.NTSDbField = ""
    Me.edPrimoPeriodoIni.NTSForzaVisZoom = False
    Me.edPrimoPeriodoIni.NTSOldValue = ""
    Me.edPrimoPeriodoIni.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPrimoPeriodoIni.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPrimoPeriodoIni.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPrimoPeriodoIni.Properties.MaxLength = 65536
    Me.edPrimoPeriodoIni.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPrimoPeriodoIni.Size = New System.Drawing.Size(84, 20)
    Me.edPrimoPeriodoIni.TabIndex = 15
    '
    'lbSecondoPeriodoFin
    '
    Me.lbSecondoPeriodoFin.AutoSize = True
    Me.lbSecondoPeriodoFin.BackColor = System.Drawing.Color.Transparent
    Me.lbSecondoPeriodoFin.Enabled = False
    Me.lbSecondoPeriodoFin.Location = New System.Drawing.Point(238, 51)
    Me.lbSecondoPeriodoFin.Name = "lbSecondoPeriodoFin"
    Me.lbSecondoPeriodoFin.NTSDbField = ""
    Me.lbSecondoPeriodoFin.Size = New System.Drawing.Size(15, 13)
    Me.lbSecondoPeriodoFin.TabIndex = 14
    Me.lbSecondoPeriodoFin.Text = "al"
    Me.lbSecondoPeriodoFin.Tooltip = ""
    Me.lbSecondoPeriodoFin.UseMnemonic = False
    '
    'lbPrimoPeriodoFin
    '
    Me.lbPrimoPeriodoFin.AutoSize = True
    Me.lbPrimoPeriodoFin.BackColor = System.Drawing.Color.Transparent
    Me.lbPrimoPeriodoFin.Enabled = False
    Me.lbPrimoPeriodoFin.Location = New System.Drawing.Point(238, 31)
    Me.lbPrimoPeriodoFin.Name = "lbPrimoPeriodoFin"
    Me.lbPrimoPeriodoFin.NTSDbField = ""
    Me.lbPrimoPeriodoFin.Size = New System.Drawing.Size(15, 13)
    Me.lbPrimoPeriodoFin.TabIndex = 13
    Me.lbPrimoPeriodoFin.Text = "al"
    Me.lbPrimoPeriodoFin.Tooltip = ""
    Me.lbPrimoPeriodoFin.UseMnemonic = False
    '
    'lbSecondoPeriodoIni
    '
    Me.lbSecondoPeriodoIni.AutoSize = True
    Me.lbSecondoPeriodoIni.BackColor = System.Drawing.Color.Transparent
    Me.lbSecondoPeriodoIni.Enabled = False
    Me.lbSecondoPeriodoIni.Location = New System.Drawing.Point(11, 51)
    Me.lbSecondoPeriodoIni.Name = "lbSecondoPeriodoIni"
    Me.lbSecondoPeriodoIni.NTSDbField = ""
    Me.lbSecondoPeriodoIni.Size = New System.Drawing.Size(114, 13)
    Me.lbSecondoPeriodoIni.TabIndex = 12
    Me.lbSecondoPeriodoIni.Text = "Rispetto al periodo dal"
    Me.lbSecondoPeriodoIni.Tooltip = ""
    Me.lbSecondoPeriodoIni.UseMnemonic = False
    '
    'lbPrimoPeriodoIni
    '
    Me.lbPrimoPeriodoIni.AutoSize = True
    Me.lbPrimoPeriodoIni.BackColor = System.Drawing.Color.Transparent
    Me.lbPrimoPeriodoIni.Enabled = False
    Me.lbPrimoPeriodoIni.Location = New System.Drawing.Point(11, 28)
    Me.lbPrimoPeriodoIni.Name = "lbPrimoPeriodoIni"
    Me.lbPrimoPeriodoIni.NTSDbField = ""
    Me.lbPrimoPeriodoIni.Size = New System.Drawing.Size(78, 13)
    Me.lbPrimoPeriodoIni.TabIndex = 11
    Me.lbPrimoPeriodoIni.Text = "Del periodo dal"
    Me.lbPrimoPeriodoIni.Tooltip = ""
    Me.lbPrimoPeriodoIni.UseMnemonic = False
    '
    'ckTrend
    '
    Me.ckTrend.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTrend.Location = New System.Drawing.Point(14, 2)
    Me.ckTrend.Name = "ckTrend"
    Me.ckTrend.NTSCheckValue = "S"
    Me.ckTrend.NTSUnCheckValue = "N"
    Me.ckTrend.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTrend.Properties.Appearance.Options.UseBackColor = True
    Me.ckTrend.Properties.Caption = "Considera Trend Vendite"
    Me.ckTrend.Size = New System.Drawing.Size(144, 19)
    Me.ckTrend.TabIndex = 0
    '
    'ckEscludifestivi
    '
    Me.ckEscludifestivi.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckEscludifestivi.EditValue = True
    Me.ckEscludifestivi.Location = New System.Drawing.Point(543, 66)
    Me.ckEscludifestivi.Name = "ckEscludifestivi"
    Me.ckEscludifestivi.NTSCheckValue = "S"
    Me.ckEscludifestivi.NTSUnCheckValue = "N"
    Me.ckEscludifestivi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckEscludifestivi.Properties.Appearance.Options.UseBackColor = True
    Me.ckEscludifestivi.Properties.Caption = "Escludi sabati e domeniche"
    Me.ckEscludifestivi.Size = New System.Drawing.Size(159, 19)
    Me.ckEscludifestivi.TabIndex = 30
    '
    'ckAggiornaARTMAGA
    '
    Me.ckAggiornaARTMAGA.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAggiornaARTMAGA.Location = New System.Drawing.Point(15, 56)
    Me.ckAggiornaARTMAGA.Name = "ckAggiornaARTMAGA"
    Me.ckAggiornaARTMAGA.NTSCheckValue = "S"
    Me.ckAggiornaARTMAGA.NTSUnCheckValue = "N"
    Me.ckAggiornaARTMAGA.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAggiornaARTMAGA.Properties.Appearance.Options.UseBackColor = True
    Me.ckAggiornaARTMAGA.Properties.Caption = "Aggiorna la scorta minima per Articolo/Magazzino"
    Me.ckAggiornaARTMAGA.Size = New System.Drawing.Size(265, 19)
    Me.ckAggiornaARTMAGA.TabIndex = 3
    '
    'FRMMGRCSM
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(784, 505)
    Me.Controls.Add(Me.ckEscludifestivi)
    Me.Controls.Add(Me.fmTrend)
    Me.Controls.Add(Me.pnPanel1)
    Me.Controls.Add(Me.lbInfo)
    Me.Controls.Add(Me.fmFrame)
    Me.Controls.Add(Me.fmFrame4)
    Me.Controls.Add(Me.fmFrame3)
    Me.Controls.Add(Me.fmFrame2)
    Me.Controls.Add(Me.fmFrame1)
    Me.Controls.Add(Me.ckForzaRRFence)
    Me.Controls.Add(Me.edScorteGG)
    Me.Controls.Add(Me.lbScorteGG)
    Me.Controls.Add(Me.ckNonmov)
    Me.Controls.Add(Me.edAammggFin)
    Me.Controls.Add(Me.edAammggIni)
    Me.Controls.Add(Me.lbAammggFin)
    Me.Controls.Add(Me.lbAammggIni)
    Me.Controls.Add(Me.cmdSeleziona)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRMMGRCSM"
    Me.Text = "RICALCOLO SCORTA MINIMA"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAammggIni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAammggFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNonmov.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edScorteGG.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckForzaRRFence.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmFrame1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmFrame1.ResumeLayout(False)
    Me.fmFrame1.PerformLayout()
    CType(Me.edCodmagFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodmagIni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opFlclavo2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opFlclavo1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opFlclavo0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPercentualeMassima.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPercentualeMinima.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edArrotond.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmFrame2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmFrame2.ResumeLayout(False)
    Me.fmFrame2.PerformLayout()
    CType(Me.ckConsideraA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckConsideraP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckConsideraV.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmFrame3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmFrame3.ResumeLayout(False)
    Me.fmFrame3.PerformLayout()
    CType(Me.opFiltroData1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opFiltroData0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmFrame4, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmFrame4.ResumeLayout(False)
    Me.fmFrame4.PerformLayout()
    CType(Me.ckScomin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckPolriord.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmFrame, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmFrame.ResumeLayout(False)
    Me.fmFrame.PerformLayout()
    CType(Me.ckElaborazioneManuale.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPanel1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPanel1.ResumeLayout(False)
    Me.pnPanel1.PerformLayout()
    CType(Me.fmTrend, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmTrend.ResumeLayout(False)
    Me.fmTrend.PerformLayout()
    CType(Me.edSecondoPeriodoFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSecondoPeriodoIni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPrimoPeriodoFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPrimoPeriodoIni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTrend.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckEscludifestivi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAggiornaARTMAGA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '----------------------------------------------------------------------------------
      Try
        tlbElabora.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception

      End Try
      '----------------------------------------------------------------------------------
      tlbMain.NTSSetToolTip()
      '----------------------------------------------------------------------------------
      edAammggIni.NTSSetParam(oMenu, oApp.Tr(Me, 128575850639653435, "Da data movimenti di magazzino"), False)
      edAammggFin.NTSSetParam(oMenu, oApp.Tr(Me, 128575850927309685, "A data movimenti di magazzino"), False)
      ckNonmov.NTSSetParam(oMenu, oApp.Tr(Me, 128575851591853623, "Anche articoli non movimentati nel periodo"), "S", "N")
      edScorteGG.NTSSetParam(oMenu, oApp.Tr(Me, 128575851973860479, "Tempo copertura scorte in GG"), "", 3, 0, 999)
      ckForzaRRFence.NTSSetParam(oMenu, oApp.Tr(Me, 128575853527103715, "Forza anche su articoli con RR Fence indicato"), "S", "N")
      opFlclavo0.NTSSetParam(oMenu, oApp.Tr(Me, 128575854320268937, "Tutti"), "T")
      opFlclavo1.NTSSetParam(oMenu, oApp.Tr(Me, 128575854478194923, "Solo merce propria"), "S")
      opFlclavo2.NTSSetParam(oMenu, oApp.Tr(Me, 128575854496721025, "Seleziona magazzini"), "M")
      edCodmagIni.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128575855097642537, "Da magazzino"), tabmaga)
      edCodmagFin.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128575855314662589, "A magazzino"), tabmaga)
      edPercentualeMassima.NTSSetParam(oMenu, oApp.Tr(Me, 128575855619693833, "Percentuale massima variazione scorta minima in aumento"), oApp.FormatSconti, 10, NTSCDec(0.01), NTSCDec(9999.99))
      edPercentualeMinima.NTSSetParam(oMenu, oApp.Tr(Me, 128575856889058981, "Percentuale massima variazione scorta minima in diminuzione"), oApp.FormatSconti, 6, NTSCDec(0.01), 100)
      edArrotond.NTSSetParam(oMenu, oApp.Tr(Me, 128575857728143935, "Arrotondamento di default"), "#,##0.000", 10, NTSCDec(0.001), 1000)
      ckElaborazioneManuale.NTSSetParam(oMenu, oApp.Tr(Me, 128575858342083525, "Elaborazione manuale"), "S", "N")
      ckConsideraV.NTSSetParam(oMenu, oApp.Tr(Me, 128575858661656343, "Scarichi vendite"), "S", "N")
      ckConsideraP.NTSSetParam(oMenu, oApp.Tr(Me, 128992296473328914, "Scarichi a produzione"), "S", "N")
      ckConsideraA.NTSSetParam(oMenu, oApp.Tr(Me, 128992296487704190, "Altri scarihi"), "S", "N")
      opFiltroData0.NTSSetParam(oMenu, oApp.Tr(Me, 128992296496610611, "Data Ordine"), "O")
      opFiltroData1.NTSSetParam(oMenu, oApp.Tr(Me, 128575859294581037, "Data consegna"), "C")
      ckPolriord.NTSSetParam(oMenu, oApp.Tr(Me, 128575858836516573, "Considera solo gli articoli con politica a punto di riordino"), "S", "N")
      ckScomin.NTSSetParam(oMenu, oApp.Tr(Me, 128575859675396441, "Considera solo gli articoli con scorta minima pari a zero"), "S", "N")
      ckTrend.NTSSetParam(oMenu, oApp.Tr(Me, 129006150402562524, "Considera Trend Vendite"), "S", "N")
      edPrimoPeriodoIni.NTSSetParam(oMenu, oApp.Tr(Me, 129006150632909109, "Del periodo dal"), True)
      edPrimoPeriodoFin.NTSSetParam(oMenu, oApp.Tr(Me, 129006151012777374, "al"), True)
      edSecondoPeriodoIni.NTSSetParam(oMenu, oApp.Tr(Me, 129006151163041194, "Rispetto al periodo dal"), True)
      edSecondoPeriodoFin.NTSSetParam(oMenu, oApp.Tr(Me, 129006151351095564, "al"), True)
      ckEscludifestivi.NTSSetParam(oMenu, oApp.Tr(Me, 129688659261950308, "Escludi sabati e domeniche"), "S", "N")
      ckAggiornaARTMAGA.NTSSetParam(oMenu, oApp.Tr(Me, 130175048968176888, "Aggiorna la scorta minima per Articolo/Magazzino"), "S", "N")
      '----------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

#Region "FORM"
  Public Overridable Sub FRMMGRCSM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '   Dim dPercentualeMassima As Decimal = NTSCDec(9999.99)
    '   Dim dPercentualeMinima As Decimal = 100

    Try
      '----------------------------------------------------------------------------------
      InitControls()
      '----------------------------------------------------------------------------------
      edAammggIni.Text = NTSCStr(Now)
      edAammggFin.Text = NTSCStr(Now)
      edScorteGG.Text = "0"
      edCodmagIni.Text = "0"
      edCodmagFin.Text = "9999"
      edPercentualeMassima.Text = oMenu.GetSettingBus("BSMGRCSM", "RECENT", ".", "PercentualeMassima", "9999,99", " ", "9999,99")
      edPercentualeMinima.Text = oMenu.GetSettingBus("BSMGRCSM", "RECENT", ".", "PercentualeMinima", "100", " ", "100")
      '----------------------------------------------------------------------------------
      oCleRcsm.bSeleziona = False
      oCleRcsm.lIITTArticox = oMenu.GetTblInstId("TTARTINVF", False)
      '----------------------------------------------------------------------------------
      lbInfo.Text = oApp.Tr(Me, 129083027948769459, "Nessuna elaborazione in corso")
      '----------------------------------------------------------------------------------
      GctlSetRoules()
      GctlApplicaDefaultValue()
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGRCSM_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    oCleRcsm.SvuotaTmpTable("TTARTINVF", oCleRcsm.lIITTArticox)
  End Sub
#End Region

#Region "CONTROLLI"
  Public Overridable Sub edCodmagIni_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodmagIni.Validated
    Dim strTmp As String = ""
    Try
      '----------------------------------------------------------------------------------
      If Not oCleRcsm.edCodmaga_Validated(DittaCorrente, "edCodmagIni", NTSCInt(edCodmagIni.Text), strTmp) Then
        edCodmagIni.Text = NTSCStr(edCodmagIni.OldEditValue)
      Else
        lbDescodmagIni.Text = strTmp
      End If
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edCodmagFin_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodmagFin.Validated
    Dim strTmp As String = ""
    Try
      '----------------------------------------------------------------------------------
      If Not oCleRcsm.edCodmaga_Validated(DittaCorrente, "edCodmagFin", NTSCInt(edCodmagFin.Text), strTmp) Then
        edCodmagFin.Text = NTSCStr(edCodmagFin.OldEditValue)
      Else
        lbDescodmagFin.Text = strTmp
      End If
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub
#End Region

#Region "OPTION-BUTTON"
  Public Overridable Sub opFlclavo0_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opFlclavo0.CheckedChanged
    Try
      SettaControlli()
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub opFlclavo1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opFlclavo1.CheckedChanged
    Try
      SettaControlli()
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub opFlclavo2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opFlclavo2.CheckedChanged
    Try
      SettaControlli()
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub
#End Region

#Region "CHECKBOX"
  Public Overridable Sub ckTrend_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckTrend.CheckedChanged
    Try
      If ckTrend.Checked = True Then
        GctlSetVisEnab(lbPrimoPeriodoIni, False)
        GctlSetVisEnab(lbPrimoPeriodoFin, False)
        GctlSetVisEnab(lbSecondoPeriodoIni, False)
        GctlSetVisEnab(lbSecondoPeriodoFin, False)
        GctlSetVisEnab(edPrimoPeriodoIni, False)
        GctlSetVisEnab(edPrimoPeriodoFin, False)
        GctlSetVisEnab(edSecondoPeriodoIni, False)
        GctlSetVisEnab(edSecondoPeriodoFin, False)
        edPrimoPeriodoIni.Text = edAammggIni.Text
        edPrimoPeriodoFin.Text = edAammggFin.Text
        edSecondoPeriodoIni.Text = IntSetDate(CDate(edAammggIni.Text).Day & "/" & CDate(edAammggIni.Text).Month & "/" & (CDate(edAammggIni.Text).Year - 1))
        edSecondoPeriodoFin.Text = IntSetDate(CDate(edAammggFin.Text).Day & "/" & CDate(edAammggFin.Text).Month & "/" & (CDate(edAammggFin.Text).Year - 1))
        edPrimoPeriodoIni.Focus()
      Else
        edPrimoPeriodoIni.Text = "" : edPrimoPeriodoFin.Text = ""
        edSecondoPeriodoIni.Text = "" : edSecondoPeriodoFin.Text = ""
        edPrimoPeriodoIni.Enabled = False : edPrimoPeriodoFin.Enabled = False
        edSecondoPeriodoIni.Enabled = False : edSecondoPeriodoFin.Enabled = False
        lbPrimoPeriodoIni.Enabled = False : lbPrimoPeriodoFin.Enabled = False
        lbSecondoPeriodoIni.Enabled = False : lbSecondoPeriodoFin.Enabled = False
      End If
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub ckAggiornaARTMAGA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckAggiornaARTMAGA.CheckedChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      oCleRcsm.bAggiornaARTMAGA = ckAggiornaARTMAGA.Checked
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "PULSANTI"
  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Dim oPar As CLE__PATB = Nothing
    Try
      '----------------------------------------------------------------------------------
      oPar = New CLE__PATB
      oPar.bVisGriglia = False
      oPar.strTipoArticolo = "N"
      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oPar)
      oCleRcsm.strWhereFiar = oPar.strOut.Trim
      '----------------------------------------------------------------------------------
      If oCleRcsm.strWhereFiar = "" Then
        oCleRcsm.bSeleziona = False
      Else
        oCleRcsm.bSeleziona = True
      End If
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub
#End Region

#Region "TOOLBAR"
  Public Overridable Sub tlbElabora_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbElabora.ItemClick
    Try
      '----------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '----------------------------------------------------------------------------------
      Elabora()
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '----------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      '----------------------------------------------------------------------------------
      NTSCallStandardZoom()
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub

#End Region

#Region "FUNZIONI"
  Public Overridable Function CheckSelection() As Boolean
    Dim bOk As Boolean = False

    Try
      '--------------------------------------------------------------------------------------------------------------
      lbInfo.Text = oApp.Tr(Me, 129070718409416874, "Controllo validità ed esistenza dati in corso...")
      Me.Refresh()
      '--------------------------------------------------------------------------------------------------------------
      If oCleRcsm.bSeleziona = False Then
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128576528173694506, "Attenzione!" & vbCrLf & _
          "Non sono stati selezionati gli articoli da elaborare." & vbCrLf & _
          "Procedere adesso con la selezione degli articoli?")) = Windows.Forms.DialogResult.No Then
          Return False
        Else
          cmdSeleziona_Click(cmdSeleziona, Nothing)
          If oCleRcsm.bSeleziona = False Then Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If CDate(edAammggIni.Text) > CDate(edAammggFin.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129006804083441720, "Attenzione!" & vbCrLf & _
          "La data movimenti di magazzino iniziale non deve essere superiore a quella finale."))
        edAammggIni.Text = NTSCStr(Now)
        edAammggFin.Text = NTSCStr(Now)
        edAammggIni.Focus()
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If opFlclavo2.Checked = True Then
        If NTSCInt(edCodmagIni.Text) > NTSCInt(edCodmagFin.Text) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128576532794477290, "Attenzione!" & vbCrLf & _
            "Il codice magazzino iniziale non deve essere superiore a quello finale."))
          edCodmagIni.Text = "0" : lbDescodmagIni.Text = ""
          edCodmagFin.Text = "9999" : lbDescodmagFin.Text = ""
          edCodmagIni.Focus()
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (ckConsideraV.Checked = False) And _
         (ckConsideraP.Checked = False) And _
         (ckConsideraA.Checked = False) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128576529777914790, "Attenzione!" & vbCrLf & _
          "Indicare almeno un tipo di scarico prima di procedere all'elaborazione."))
        ckConsideraV.Focus()
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      bOk = True
      If ckAggiornaARTMAGA.Checked = False Then
        bOk = oCleRcsm.CheckEsistenzaDati(DittaCorrente, oCleRcsm.strWhereFiar, ckPolriord.Checked, ckScomin.Checked)
      Else
        bOk = oCleRcsm.CheckEsistenzaDatiARTMAGA(DittaCorrente, oCleRcsm.strWhereFiar, ckPolriord.Checked, _
          ckScomin.Checked, opFlclavo1.Checked, opFlclavo2.Checked, _
          NTSCInt(edCodmagIni.Text), NTSCInt(edCodmagFin.Text))
      End If
      If bOk = False Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128577337354315402, "Attenzione!" & vbCrLf & _
          "Non esistono articoli da elaborare con le caratteristiche richieste."))
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If ckTrend.Checked = True Then
        If (edPrimoPeriodoIni.Text.Trim = "") Or (edPrimoPeriodoFin.Text.Trim = "") Or _
           (edSecondoPeriodoIni.Text.Trim = "") Or (edSecondoPeriodoFin.Text.Trim = "") Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 129006676401533530, "Attenzione!" & vbCrLf & _
            "Indicare le date di confronto relative al trende delle vendite" & vbCrLf & _
            "altrimenti deselezionare la scelta."))
          If edPrimoPeriodoIni.Text.Trim = "" Then
            edPrimoPeriodoIni.Focus()
          Else
            If edPrimoPeriodoFin.Text.Trim = "" Then
              edPrimoPeriodoFin.Focus()
            Else
              If edSecondoPeriodoIni.Text.Trim = "" Then
                edSecondoPeriodoIni.Focus()
              Else
                edSecondoPeriodoFin.Focus()
              End If
            End If
          End If
          Return False
        End If
        If CDate(edPrimoPeriodoIni.Text) > CDate(edPrimoPeriodoFin.Text) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129006804725316720, "Attenzione!" & vbCrLf & _
            "La data del primo periodo trend vendite iniziale non deve essere superiore a quella finale."))
          edPrimoPeriodoIni.Text = edAammggIni.Text
          edPrimoPeriodoFin.Text = edAammggFin.Text
          edPrimoPeriodoIni.Focus()
          Return False
        End If
        If CDate(edSecondoPeriodoIni.Text) > CDate(edSecondoPeriodoFin.Text) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129006805942660470, "Attenzione!" & vbCrLf & _
            "La data del secondo periodo trend vendite iniziale non deve essere superiore a quella finale."))
          edSecondoPeriodoIni.Text = IntSetDate(CDate(edAammggIni.Text).Day & "/" & CDate(edAammggIni.Text).Month & "/" & (CDate(edAammggIni.Text).Year - 1))
          edSecondoPeriodoFin.Text = IntSetDate(CDate(edAammggFin.Text).Day & "/" & CDate(edAammggFin.Text).Month & "/" & (CDate(edAammggFin.Text).Year - 1))
          edSecondoPeriodoIni.Focus()
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------      
      Dim choice1 As String = oApp.Tr(Me, 129070717286128435, "manuale")
      Dim choice2 As String = oApp.Tr(Me, 129070717346753823, "automatica")
      If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128576689648225918, "Confermare l'elaborazione |" & _
        IIf(ckElaborazioneManuale.Checked = True, _
        choice1, choice2).ToString & _
        "| degli articoli secondo i dati selezionati?")) = Windows.Forms.DialogResult.No Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      lbInfo.Text = oApp.Tr(Me, 129070718778481736, "Nessuna elaborazione in corso")
      Me.Cursor = Cursors.Default
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      lbInfo.Text = oApp.Tr(Me, 129070718795200705, "Nessuna elaborazione in corso")
      Me.Cursor = Cursors.Default
    End Try
  End Function

  Public Overridable Sub Elabora()
    Dim lResult As Integer = 0
    Dim strMsg As String = ""
    Dim fmLiar As FRMMGLIAR = Nothing

    Try
      '----------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '----------------------------------------------------------------------------------
      '--- Controlla validità, esistenza dati e chiede conferma dell'elaborazione
      '----------------------------------------------------------------------------------
      If CheckSelection() = False Then Return
      '----------------------------------------------------------------------------------
      '--- Salva nel RECENT le percentuali di variazione scorta indicate in maschera
      '----------------------------------------------------------------------------------
      oMenu.SaveSettingBus("BSMGRCSM", "RECENT", ".", "PercentualeMassima", NTSCStr(edPercentualeMassima.Text), " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("BSMGRCSM", "RECENT", ".", "PercentualeMinima", NTSCStr(edPercentualeMinima.Text), " ", "NS.", "...", "...")
      '----------------------------------------------------------------------------------
      lbInfo.Text = oApp.Tr(Me, 129070719101925344, "Selezione articoli in corso...")
      Me.Refresh()
      '----------------------------------------------------------------------------------
      If oCleRcsm.RiempiTmpTable(DittaCorrente, oCleRcsm.strWhereFiar, _
                                oCleRcsm.lIITTArticox, edAammggIni.Text, edAammggFin.Text, _
                                ckPolriord.Checked, ckScomin.Checked, _
                                opFlclavo0.Checked, opFlclavo1.Checked, opFlclavo2.Checked, _
                                NTSCInt(edCodmagIni.Text), NTSCInt(edCodmagFin.Text), _
                                NTSCInt(edScorteGG.Text), ckForzaRRFence.Checked, _
                                ckConsideraV.Checked, ckConsideraP.Checked, ckConsideraA.Checked, _
                                opFiltroData0.Checked, opFiltroData1.Checked, _
                                ckNonmov.Checked, NTSCDec(edArrotond.Text), _
                                NTSCDec(edPercentualeMassima.Text), NTSCDec(edPercentualeMinima.Text), _
                                ckElaborazioneManuale.Checked, ckTrend.Checked, _
                                edPrimoPeriodoIni.Text, edPrimoPeriodoFin.Text, _
                                edSecondoPeriodoIni.Text, edSecondoPeriodoFin.Text, _
                                lResult, ckEscludifestivi.Checked) = False Then Return
      '----------------------------------------------------------------------------------
      '--- Se è stata scelta l'elaborazione manuale, lancia la griglia su TTARTINVF
      '--- (CON L'ELABORAZIONE AUTOMATICA, ARTICO E' STATO AGGIORNATO NELLA
      '--- "RiempiTmpTable" DEL DAL)
      '----------------------------------------------------------------------------------
      If ckElaborazioneManuale.Checked = True Then
        '--------------------------------------------------------------------------------
        fmLiar = CType(NTSNewFormModal("FRMMGLIAR"), FRMMGLIAR)
        fmLiar.Init(oMenu, Nothing, DittaCorrente)
        fmLiar.InitEntity(oCleRcsm)
        '--------------------------------------------------------------------------------
        fmLiar.strAammggIni = edAammggIni.Text
        fmLiar.strAammggFin = edAammggFin.Text
        fmLiar.bTrend = ckTrend.Checked
        fmLiar.strPrimoPeriodoIni = edPrimoPeriodoIni.Text
        fmLiar.strPrimoPeriodoFin = edPrimoPeriodoFin.Text
        fmLiar.strSecondoPeriodoIni = edSecondoPeriodoIni.Text
        fmLiar.strSecondoPeriodoFin = edSecondoPeriodoFin.Text
        '--------------------------------------------------------------------------------
        fmLiar.ShowDialog(Me)
        If fmLiar.bAnnullato = True Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128992296601456374, "Operazione annullata." & vbCrLf & _
            "Nessun articolo elaborato."))
          Return
        End If
        '--------------------------------------------------------------------------------
        If Not oCleRcsm.AggiornaScortaMinima(DittaCorrente, oCleRcsm.lIITTArticox, lResult) Then Return
        '--------------------------------------------------------------------------------
      End If
      '----------------------------------------------------------------------------------
      strMsg = oApp.Tr(Me, 129070719750062788, "Ricalcolo completato regolarmente." & vbCrLf)
      Select Case lResult
        Case 0 : strMsg += oApp.Tr(Me, 129070719786625990, "Nessun articolo elaborato.")
        Case 1 : strMsg += oApp.Tr(Me, 129070719828189288, "E' stato elaborato un articolo.")
        Case Else : strMsg += oApp.Tr(Me, 129070719849908455, "Sono stati elaborati |" & lResult.ToString & "| articoli.")
      End Select
      oApp.MsgBoxInfo(strMsg)
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      lbInfo.Text = oApp.Tr(Me, 129070719996473769, "Nessuna elaborazione in corso")
      Me.Cursor = Cursors.Default
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    Finally
      '----------------------------------------------------------------------------------
      If Not fmLiar Is Nothing Then fmLiar.Dispose()
      fmLiar = Nothing
      lbInfo.Text = oApp.Tr(Me, 129070720017880430, "Nessuna elaborazione in corso")
      Me.Cursor = Cursors.Default
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub SettaControlli()
    Try
      If opFlclavo2.Checked = True Then
        GctlSetVisEnab(lbCodmagIni, False)
        GctlSetVisEnab(lbCodmagFin, False)
        GctlSetVisEnab(edCodmagIni, False)
        GctlSetVisEnab(edCodmagFin, False)
        If edCodmagIni.Enabled = True Then edCodmagIni.Focus()
      Else
        lbCodmagIni.Enabled = False
        lbCodmagFin.Enabled = False
        edCodmagIni.Text = "0" : lbDescodmagIni.Text = ""
        edCodmagFin.Text = "9999" : lbDescodmagFin.Text = ""
        edCodmagIni.Enabled = False
        edCodmagFin.Enabled = False
      End If
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub
#End Region

End Class