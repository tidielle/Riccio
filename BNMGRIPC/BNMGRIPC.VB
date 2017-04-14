Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGRIPC

#Region "Variabili"
  Public oCleRipc As CLEMGRIPC
  Public oCallParams As CLE__CLDP

  Public strWhereArtico As String = "."
  Public strMsg As String = ""

  Private components As System.ComponentModel.IContainer

  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbElabora As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbStatus As NTSInformatica.NTSLabel
  Public WithEvents edTotale As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTotale As NTSInformatica.NTSLabel
  Public WithEvents edListino As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbListino As NTSInformatica.NTSLabel
  Public WithEvents edCodice As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDescrCodice As NTSInformatica.NTSLabel
  Public WithEvents lbCodice As NTSInformatica.NTSLabel
  Public WithEvents lbRipart As NTSInformatica.NTSLabel
  Public WithEvents cbRipart As NTSInformatica.NTSComboBox
  Public WithEvents edDataElab As NTSInformatica.NTSTextBoxData
  Public WithEvents lbDataElab As NTSInformatica.NTSLabel
  Public WithEvents edAData As NTSInformatica.NTSTextBoxData
  Public WithEvents lbAData As NTSInformatica.NTSLabel
  Public WithEvents edDaData As NTSInformatica.NTSTextBoxData
  Public WithEvents lbDaData As NTSInformatica.NTSLabel
  Public WithEvents fmAggArticoli As NTSInformatica.NTSGroupBox
  Public WithEvents ckAggArticoli As NTSInformatica.NTSCheckBox
  Public WithEvents cmdSelez As NTSInformatica.NTSButton
  Public WithEvents fmAvan As NTSInformatica.NTSGroupBox
  Public WithEvents optAvan0 As NTSInformatica.NTSRadioButton
  Public WithEvents optAvan2 As NTSInformatica.NTSRadioButton
  Public WithEvents optAvan1 As NTSInformatica.NTSRadioButton
#End Region

#Region "Moduli"
  Private Moduli_P As Integer = bsModMG Or bsModVE
  Private ModuliExt_P As Integer = 0
  Private ModuliSup_P As Integer = 0
  Private ModuliSupExt_P As Integer = 0
  Private ModuliPtn_P As Integer = 0
  Public WithEvents ckAggiungiAListino As NTSInformatica.NTSCheckBox
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGRIPC", "BEMGRIPC", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128759632915560135, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleRipc = CType(oTmp, CLEMGRIPC)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGRIPC", strRemoteServer, strRemotePort)
    AddHandler oCleRipc.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleRipc.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGRIPC))
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
    Me.lbStatus = New NTSInformatica.NTSLabel
    Me.edTotale = New NTSInformatica.NTSTextBoxNum
    Me.lbTotale = New NTSInformatica.NTSLabel
    Me.edListino = New NTSInformatica.NTSTextBoxNum
    Me.lbListino = New NTSInformatica.NTSLabel
    Me.edCodice = New NTSInformatica.NTSTextBoxNum
    Me.lbCodice = New NTSInformatica.NTSLabel
    Me.cbRipart = New NTSInformatica.NTSComboBox
    Me.lbRipart = New NTSInformatica.NTSLabel
    Me.edDataElab = New NTSInformatica.NTSTextBoxData
    Me.lbDataElab = New NTSInformatica.NTSLabel
    Me.edDaData = New NTSInformatica.NTSTextBoxData
    Me.lbDaData = New NTSInformatica.NTSLabel
    Me.edAData = New NTSInformatica.NTSTextBoxData
    Me.lbAData = New NTSInformatica.NTSLabel
    Me.fmAggArticoli = New NTSInformatica.NTSGroupBox
    Me.ckAggArticoli = New NTSInformatica.NTSCheckBox
    Me.cmdSelez = New NTSInformatica.NTSButton
    Me.fmAvan = New NTSInformatica.NTSGroupBox
    Me.optAvan2 = New NTSInformatica.NTSRadioButton
    Me.optAvan1 = New NTSInformatica.NTSRadioButton
    Me.optAvan0 = New NTSInformatica.NTSRadioButton
    Me.lbDescrCodice = New NTSInformatica.NTSLabel
    Me.ckAggiungiAListino = New NTSInformatica.NTSCheckBox
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTotale.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edListino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbRipart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataElab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDaData.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAData.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmAggArticoli, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmAggArticoli.SuspendLayout()
    CType(Me.ckAggArticoli.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmAvan, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmAvan.SuspendLayout()
    CType(Me.optAvan2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optAvan1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optAvan0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAggiungiAListino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbElabora, Me.tlbZoom, Me.tlbGuida, Me.tlbEsci})
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
    Me.tlbElabora.GlyphPath = ""
    Me.tlbElabora.Id = 0
    Me.tlbElabora.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbElabora.Name = "tlbElabora"
    Me.tlbElabora.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.GlyphPath = ""
    Me.tlbZoom.Id = 1
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 2
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 3
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'lbStatus
    '
    Me.lbStatus.BackColor = System.Drawing.Color.Transparent
    Me.lbStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbStatus.Location = New System.Drawing.Point(12, 331)
    Me.lbStatus.Name = "lbStatus"
    Me.lbStatus.NTSDbField = ""
    Me.lbStatus.Size = New System.Drawing.Size(542, 20)
    Me.lbStatus.TabIndex = 115
    Me.lbStatus.Text = "Pronto."
    Me.lbStatus.Tooltip = ""
    Me.lbStatus.UseMnemonic = False
    '
    'edTotale
    '
    Me.edTotale.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTotale.Location = New System.Drawing.Point(248, 44)
    Me.edTotale.Name = "edTotale"
    Me.edTotale.NTSDbField = ""
    Me.edTotale.NTSFormat = "0"
    Me.edTotale.NTSForzaVisZoom = False
    Me.edTotale.NTSOldValue = ""
    Me.edTotale.Properties.Appearance.Options.UseTextOptions = True
    Me.edTotale.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTotale.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTotale.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTotale.Properties.AutoHeight = False
    Me.edTotale.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTotale.Properties.MaxLength = 65536
    Me.edTotale.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTotale.Size = New System.Drawing.Size(127, 20)
    Me.edTotale.TabIndex = 121
    '
    'lbTotale
    '
    Me.lbTotale.BackColor = System.Drawing.Color.Transparent
    Me.lbTotale.Location = New System.Drawing.Point(12, 47)
    Me.lbTotale.Name = "lbTotale"
    Me.lbTotale.NTSDbField = ""
    Me.lbTotale.Size = New System.Drawing.Size(102, 20)
    Me.lbTotale.TabIndex = 120
    Me.lbTotale.Text = "Totale da ripartire"
    Me.lbTotale.Tooltip = ""
    Me.lbTotale.UseMnemonic = False
    '
    'edListino
    '
    Me.edListino.Cursor = System.Windows.Forms.Cursors.Default
    Me.edListino.Location = New System.Drawing.Point(248, 67)
    Me.edListino.Name = "edListino"
    Me.edListino.NTSDbField = ""
    Me.edListino.NTSFormat = "0"
    Me.edListino.NTSForzaVisZoom = False
    Me.edListino.NTSOldValue = ""
    Me.edListino.Properties.Appearance.Options.UseTextOptions = True
    Me.edListino.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edListino.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edListino.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edListino.Properties.AutoHeight = False
    Me.edListino.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edListino.Properties.MaxLength = 65536
    Me.edListino.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edListino.Size = New System.Drawing.Size(60, 20)
    Me.edListino.TabIndex = 123
    '
    'lbListino
    '
    Me.lbListino.BackColor = System.Drawing.Color.Transparent
    Me.lbListino.Location = New System.Drawing.Point(12, 70)
    Me.lbListino.Name = "lbListino"
    Me.lbListino.NTSDbField = ""
    Me.lbListino.Size = New System.Drawing.Size(114, 20)
    Me.lbListino.TabIndex = 122
    Me.lbListino.Text = "Listino da aggiornare"
    Me.lbListino.Tooltip = ""
    Me.lbListino.UseMnemonic = False
    '
    'edCodice
    '
    Me.edCodice.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodice.Location = New System.Drawing.Point(248, 90)
    Me.edCodice.Name = "edCodice"
    Me.edCodice.NTSDbField = ""
    Me.edCodice.NTSFormat = "0"
    Me.edCodice.NTSForzaVisZoom = False
    Me.edCodice.NTSOldValue = ""
    Me.edCodice.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodice.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodice.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodice.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodice.Properties.AutoHeight = False
    Me.edCodice.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodice.Properties.MaxLength = 65536
    Me.edCodice.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodice.Size = New System.Drawing.Size(60, 20)
    Me.edCodice.TabIndex = 125
    '
    'lbCodice
    '
    Me.lbCodice.BackColor = System.Drawing.Color.Transparent
    Me.lbCodice.Location = New System.Drawing.Point(12, 93)
    Me.lbCodice.Name = "lbCodice"
    Me.lbCodice.NTSDbField = ""
    Me.lbCodice.Size = New System.Drawing.Size(230, 20)
    Me.lbCodice.TabIndex = 124
    Me.lbCodice.Text = "Codice Lavorazione/Tipo Costo da aggiungere"
    Me.lbCodice.Tooltip = ""
    Me.lbCodice.UseMnemonic = False
    '
    'cbRipart
    '
    Me.cbRipart.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.cbRipart.DataSource = Nothing
    Me.cbRipart.DisplayMember = ""
    Me.cbRipart.Location = New System.Drawing.Point(248, 113)
    Me.cbRipart.Name = "cbRipart"
    Me.cbRipart.NTSDbField = ""
    Me.cbRipart.Properties.AutoHeight = False
    Me.cbRipart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbRipart.Properties.DropDownRows = 30
    Me.cbRipart.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbRipart.SelectedValue = ""
    Me.cbRipart.Size = New System.Drawing.Size(221, 20)
    Me.cbRipart.TabIndex = 127
    Me.cbRipart.ValueMember = ""
    '
    'lbRipart
    '
    Me.lbRipart.BackColor = System.Drawing.Color.Transparent
    Me.lbRipart.Location = New System.Drawing.Point(12, 116)
    Me.lbRipart.Name = "lbRipart"
    Me.lbRipart.NTSDbField = ""
    Me.lbRipart.Size = New System.Drawing.Size(102, 20)
    Me.lbRipart.TabIndex = 128
    Me.lbRipart.Text = "Base di ripartizione"
    Me.lbRipart.Tooltip = ""
    Me.lbRipart.UseMnemonic = False
    '
    'edDataElab
    '
    Me.edDataElab.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataElab.Location = New System.Drawing.Point(248, 136)
    Me.edDataElab.Name = "edDataElab"
    Me.edDataElab.NTSDbField = ""
    Me.edDataElab.NTSForzaVisZoom = False
    Me.edDataElab.NTSOldValue = ""
    Me.edDataElab.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataElab.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataElab.Properties.AutoHeight = False
    Me.edDataElab.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataElab.Properties.MaxLength = 65536
    Me.edDataElab.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataElab.Size = New System.Drawing.Size(100, 20)
    Me.edDataElab.TabIndex = 130
    '
    'lbDataElab
    '
    Me.lbDataElab.BackColor = System.Drawing.Color.Transparent
    Me.lbDataElab.Location = New System.Drawing.Point(12, 139)
    Me.lbDataElab.Name = "lbDataElab"
    Me.lbDataElab.NTSDbField = ""
    Me.lbDataElab.Size = New System.Drawing.Size(189, 20)
    Me.lbDataElab.TabIndex = 129
    Me.lbDataElab.Text = "Data elaborazione"
    Me.lbDataElab.Tooltip = ""
    Me.lbDataElab.UseMnemonic = False
    '
    'edDaData
    '
    Me.edDaData.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDaData.Location = New System.Drawing.Point(248, 159)
    Me.edDaData.Name = "edDaData"
    Me.edDaData.NTSDbField = ""
    Me.edDaData.NTSForzaVisZoom = False
    Me.edDaData.NTSOldValue = ""
    Me.edDaData.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDaData.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDaData.Properties.AutoHeight = False
    Me.edDaData.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDaData.Properties.MaxLength = 65536
    Me.edDaData.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDaData.Size = New System.Drawing.Size(100, 20)
    Me.edDaData.TabIndex = 132
    '
    'lbDaData
    '
    Me.lbDaData.BackColor = System.Drawing.Color.Transparent
    Me.lbDaData.Location = New System.Drawing.Point(12, 162)
    Me.lbDaData.Name = "lbDaData"
    Me.lbDaData.NTSDbField = ""
    Me.lbDaData.Size = New System.Drawing.Size(189, 20)
    Me.lbDaData.TabIndex = 131
    Me.lbDaData.Text = "Periodo base di riferimento di riga dal"
    Me.lbDaData.Tooltip = ""
    Me.lbDaData.UseMnemonic = False
    '
    'edAData
    '
    Me.edAData.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAData.Location = New System.Drawing.Point(454, 159)
    Me.edAData.Name = "edAData"
    Me.edAData.NTSDbField = ""
    Me.edAData.NTSForzaVisZoom = False
    Me.edAData.NTSOldValue = ""
    Me.edAData.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAData.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAData.Properties.AutoHeight = False
    Me.edAData.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAData.Properties.MaxLength = 65536
    Me.edAData.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAData.Size = New System.Drawing.Size(100, 20)
    Me.edAData.TabIndex = 134
    '
    'lbAData
    '
    Me.lbAData.BackColor = System.Drawing.Color.Transparent
    Me.lbAData.Location = New System.Drawing.Point(422, 162)
    Me.lbAData.Name = "lbAData"
    Me.lbAData.NTSDbField = ""
    Me.lbAData.Size = New System.Drawing.Size(21, 20)
    Me.lbAData.TabIndex = 133
    Me.lbAData.Text = "al"
    Me.lbAData.Tooltip = ""
    Me.lbAData.UseMnemonic = False
    '
    'fmAggArticoli
    '
    Me.fmAggArticoli.AllowDrop = True
    Me.fmAggArticoli.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmAggArticoli.Appearance.Options.UseBackColor = True
    Me.fmAggArticoli.Controls.Add(Me.ckAggiungiAListino)
    Me.fmAggArticoli.Controls.Add(Me.ckAggArticoli)
    Me.fmAggArticoli.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmAggArticoli.Location = New System.Drawing.Point(15, 185)
    Me.fmAggArticoli.Name = "fmAggArticoli"
    Me.fmAggArticoli.ShowCaption = False
    Me.fmAggArticoli.Size = New System.Drawing.Size(377, 51)
    Me.fmAggArticoli.TabIndex = 135
    '
    'ckAggArticoli
    '
    Me.ckAggArticoli.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAggArticoli.Location = New System.Drawing.Point(18, 2)
    Me.ckAggArticoli.Name = "ckAggArticoli"
    Me.ckAggArticoli.NTSCheckValue = "S"
    Me.ckAggArticoli.NTSUnCheckValue = "N"
    Me.ckAggArticoli.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAggArticoli.Properties.Appearance.Options.UseBackColor = True
    Me.ckAggArticoli.Properties.AutoHeight = False
    Me.ckAggArticoli.Properties.Caption = "Aggiorna articoli non movimentati"
    Me.ckAggArticoli.Size = New System.Drawing.Size(189, 18)
    Me.ckAggArticoli.TabIndex = 11
    '
    'cmdSelez
    '
    Me.cmdSelez.ImagePath = ""
    Me.cmdSelez.ImageText = ""
    Me.cmdSelez.Location = New System.Drawing.Point(404, 193)
    Me.cmdSelez.Name = "cmdSelez"
    Me.cmdSelez.NTSContextMenu = Nothing
    Me.cmdSelez.Size = New System.Drawing.Size(150, 27)
    Me.cmdSelez.TabIndex = 136
    Me.cmdSelez.Text = "Seleziona articoli"
    '
    'fmAvan
    '
    Me.fmAvan.AllowDrop = True
    Me.fmAvan.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmAvan.Appearance.Options.UseBackColor = True
    Me.fmAvan.Controls.Add(Me.optAvan2)
    Me.fmAvan.Controls.Add(Me.optAvan1)
    Me.fmAvan.Controls.Add(Me.optAvan0)
    Me.fmAvan.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmAvan.Location = New System.Drawing.Point(15, 239)
    Me.fmAvan.Name = "fmAvan"
    Me.fmAvan.Size = New System.Drawing.Size(377, 89)
    Me.fmAvan.TabIndex = 137
    Me.fmAvan.Text = "Quantità in stato di avanzamento"
    '
    'optAvan2
    '
    Me.optAvan2.Cursor = System.Windows.Forms.Cursors.Default
    Me.optAvan2.Location = New System.Drawing.Point(18, 64)
    Me.optAvan2.Name = "optAvan2"
    Me.optAvan2.NTSCheckValue = "S"
    Me.optAvan2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optAvan2.Properties.Appearance.Options.UseBackColor = True
    Me.optAvan2.Properties.AutoHeight = False
    Me.optAvan2.Properties.Caption = "Considera il venduto + la quantità più alta fra budget e IC inevasi"
    Me.optAvan2.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optAvan2.Size = New System.Drawing.Size(342, 18)
    Me.optAvan2.TabIndex = 3
    '
    'optAvan1
    '
    Me.optAvan1.Cursor = System.Windows.Forms.Cursors.Default
    Me.optAvan1.Location = New System.Drawing.Point(18, 44)
    Me.optAvan1.Name = "optAvan1"
    Me.optAvan1.NTSCheckValue = "S"
    Me.optAvan1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optAvan1.Properties.Appearance.Options.UseBackColor = True
    Me.optAvan1.Properties.AutoHeight = False
    Me.optAvan1.Properties.Caption = "Considera il venduto + gli impegni cliente inevasi"
    Me.optAvan1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optAvan1.Size = New System.Drawing.Size(257, 18)
    Me.optAvan1.TabIndex = 2
    '
    'optAvan0
    '
    Me.optAvan0.Cursor = System.Windows.Forms.Cursors.Default
    Me.optAvan0.Location = New System.Drawing.Point(18, 23)
    Me.optAvan0.Name = "optAvan0"
    Me.optAvan0.NTSCheckValue = "S"
    Me.optAvan0.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optAvan0.Properties.Appearance.Options.UseBackColor = True
    Me.optAvan0.Properties.AutoHeight = False
    Me.optAvan0.Properties.Caption = "Considera il venduto + budget"
    Me.optAvan0.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optAvan0.Size = New System.Drawing.Size(168, 18)
    Me.optAvan0.TabIndex = 1
    '
    'lbDescrCodice
    '
    Me.lbDescrCodice.BackColor = System.Drawing.Color.Transparent
    Me.lbDescrCodice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescrCodice.Location = New System.Drawing.Point(314, 90)
    Me.lbDescrCodice.Name = "lbDescrCodice"
    Me.lbDescrCodice.NTSDbField = ""
    Me.lbDescrCodice.Size = New System.Drawing.Size(240, 20)
    Me.lbDescrCodice.TabIndex = 138
    Me.lbDescrCodice.Tooltip = ""
    Me.lbDescrCodice.UseMnemonic = False
    '
    'ckAggiungiAListino
    '
    Me.ckAggiungiAListino.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAggiungiAListino.Location = New System.Drawing.Point(18, 25)
    Me.ckAggiungiAListino.Name = "ckAggiungiAListino"
    Me.ckAggiungiAListino.NTSCheckValue = "S"
    Me.ckAggiungiAListino.NTSUnCheckValue = "N"
    Me.ckAggiungiAListino.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAggiungiAListino.Properties.Appearance.Options.UseBackColor = True
    Me.ckAggiungiAListino.Properties.AutoHeight = False
    Me.ckAggiungiAListino.Properties.Caption = "Aggiungi a listino"
    Me.ckAggiungiAListino.Size = New System.Drawing.Size(189, 19)
    Me.ckAggiungiAListino.TabIndex = 13
    '
    'FRMMGRIPC
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(570, 351)
    Me.Controls.Add(Me.lbDescrCodice)
    Me.Controls.Add(Me.fmAvan)
    Me.Controls.Add(Me.cmdSelez)
    Me.Controls.Add(Me.fmAggArticoli)
    Me.Controls.Add(Me.edAData)
    Me.Controls.Add(Me.lbAData)
    Me.Controls.Add(Me.edDaData)
    Me.Controls.Add(Me.lbDaData)
    Me.Controls.Add(Me.edDataElab)
    Me.Controls.Add(Me.lbDataElab)
    Me.Controls.Add(Me.lbRipart)
    Me.Controls.Add(Me.cbRipart)
    Me.Controls.Add(Me.edCodice)
    Me.Controls.Add(Me.lbCodice)
    Me.Controls.Add(Me.edListino)
    Me.Controls.Add(Me.lbListino)
    Me.Controls.Add(Me.edTotale)
    Me.Controls.Add(Me.lbTotale)
    Me.Controls.Add(Me.lbStatus)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
    Me.MaximizeBox = False
    Me.Name = "FRMMGRIPC"
    Me.Text = "RIPARTIZIONE COSTI FISSI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTotale.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edListino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbRipart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataElab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDaData.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAData.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmAggArticoli, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmAggArticoli.ResumeLayout(False)
    Me.fmAggArticoli.PerformLayout()
    CType(Me.ckAggArticoli.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmAvan, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmAvan.ResumeLayout(False)
    Me.fmAvan.PerformLayout()
    CType(Me.optAvan2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optAvan1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optAvan0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAggiungiAListino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

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
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()
      '----------------------------------------------------------------------------------
      edTotale.NTSSetParam(oMenu, oApp.Tr(Me, 128759659875413300, "Totale da ripartire"), oApp.FormatImporti, 13, 0, 9999999999999)
      edListino.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128759668243225800, "Listino da aggiornare"), tablist)
      edCodice.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128759670174319550, "Codice Lavorazione/Tipo costo da aggiornare"), tablavo)

      cbRipart.NTSSetParam(oApp.Tr(Me, 128759671878225800, "Base di ripartizione"))
      edDataElab.NTSSetParam(oMenu, oApp.Tr(Me, 128759672107132050, "Data elaborazione"), False)
      edDaData.NTSSetParam(oMenu, oApp.Tr(Me, 128759672707757050, "Periodo base di riferimento di riga iniziale"), False)
      edAData.NTSSetParam(oMenu, oApp.Tr(Me, 128759673067288300, "Periodo base di riferimento di riga finale"), False)
      ckAggArticoli.NTSSetParam(oMenu, oApp.Tr(Me, 128759673281663300, "Aggiorna articoli non movimentati"), "S", "N")
      ckAggiungiAListino.NTSSetParam(oMenu, oApp.Tr(Me, 130803194146371331, "Aggiungi il valore ripartito al lisino"), "S", "N")
      optAvan0.NTSSetParam(oMenu, oApp.Tr(Me, 128759674122288300, "Considera il venduto + budget"), "S")
      optAvan1.NTSSetParam(oMenu, oApp.Tr(Me, 128759674141350800, "Considera il venduto + gli impegni cliente inevasi"), "S")
      optAvan2.NTSSetParam(oMenu, oApp.Tr(Me, 128759674159163300, "Considera il venduto + la quantità più alta fra budget e IC inevasi"), "S")
      '----------------------------------------------------------------------------------
      edListino.NTSForzaVisZoom = True
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub
#End Region

#Region "Form"
  Public Overridable Sub FRMPRRIPC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim strTmp As String = ""

    Try
      '----------------------------------------------------------------------------------
      InitControls()
      '----------------------------------------------------------------------------------
      CaricaCombo()
      '----------------------------------------------------------------------------------
      oCleRipc.bSariaf = CBool(oMenu.GetSettingBus("BSCGPRIN", "OPZIONI", ".", "Sariaf", "0", " ", "0"))
      '----------------------------------------------------------------------------------
      edTotale.Text = "0"
      edListino.Text = oCleRipc.RitornaListino.ToString
      edCodice.Text = NTSCStr(oMenu.GetSettingBus("BSMGRIPC", "RECENT", ".", "Codice", "0", " ", "0"))
      cbRipart.SelectedValue = "0"
      edDataElab.Text = oMenu.GetSettingBus("BSMGRIPC", "RECENT", ".", "DataElab", DateTime.Now.ToShortDateString, " ", DateTime.Now.ToShortDateString)
      strTmp = IntSetDate("01/01/" & DateTime.Now.Year.ToString())
      edDaData.Text = oMenu.GetSettingBus("BSMGRIPC", "RECENT", ".", "DaData", strTmp, " ", strTmp)
      strTmp = IntSetDate("31/12/" & DateTime.Now.Year.ToString())
      edAData.Text = oMenu.GetSettingBus("BSMGRIPC", "RECENT", ".", "AData", strTmp, " ", strTmp)
      ckAggArticoli.Checked = False
      optAvan2.Checked = True
      '----------------------------------------------------------------------------------
      GctlSetRoules()
      '----------------------------------------------------------------------------------
      GctlApplicaDefaultValue()
      '----------------------------------------------------------------------------------
      lbStatus.Text = oApp.Tr(Me, 128759686259952110, "Pronto.")
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMMGRIPC_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    oMenu.SaveSettingBus("BSMGRIPC", "RECENT", ".", "Listino", edListino.Text, " ", "NS.", "NS.", "...")
    oMenu.SaveSettingBus("BSMGRIPC", "RECENT", ".", "Codice", edCodice.Text, " ", "NS.", "NS.", "...")
    oMenu.SaveSettingBus("BSMGRIPC", "RECENT", ".", "DataElab", edDataElab.Text, " ", "NS.", "NS.", "...")
    oMenu.SaveSettingBus("BSMGRIPC", "RECENT", ".", "DaData", edDaData.Text, " ", "NS.", "NS.", "...")
    oMenu.SaveSettingBus("BSMGRIPC", "RECENT", ".", "AData", edAData.Text, " ", "NS.", "NS.", "...")
  End Sub
#End Region

#Region "Eventi Controlli"
  Public Overridable Sub edCodice_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodice.Validated
    Dim strTmp As String = ""

    Try
      If oCleRipc Is Nothing Then Return
      If Not oCleRipc.edCodice_Validated(NTSCInt(edCodice.Text), strTmp) Then
        edCodice.Text = NTSCStr(edCodice.OldEditValue)
      Else
        lbDescrCodice.Text = strTmp
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdSelez_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelez.Click
    Dim oPar As CLE__PATB = Nothing

    Try
      oPar = New CLE__PATB
      oPar.bVisGriglia = False
      oPar.strTipoArticolo = "N"
      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oPar)
      If oPar.CANCELZOOM = False Then strWhereArtico = oPar.strOut.Trim
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cbRipart_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRipart.SelectedValueChanged
    Try
      Select Case cbRipart.SelectedValue
        Case "4"
          ckAggArticoli.Checked = False
          ckAggArticoli.Enabled = False
        Case Else
          GctlSetVisEnab(ckAggArticoli, False)
      End Select
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "ToolBar"
  Public Overridable Sub tlbElabora_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbElabora.ItemClick
    Try
      Elabora()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB
      NTSCallStandardZoom()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub
#End Region

#Region "Functions"
  Public Overridable Function Elabora() As Boolean

    Try
      '----------------------------------------------------------------------------------
      '--- Controlla validità dei dati indicati
      '----------------------------------------------------------------------------------
      Me.ValidaLastControl()
      If NTSCDec(edTotale.Text) = 0 Then
        strMsg = "Attenzione!" & vbCrLf & _
          "Inserire un totale diverso da zero prima di procedere alla ripartizione."
        oApp.MsgBoxErr(oApp.Tr(Me, 128759924157516293, strMsg))
        Return False
      End If
      If CDate(edDaData.Text) > CDate(edAData.Text) Then
        strMsg = "La data di riferimento finale non può essere inferiore a quella iniziale."
        oApp.MsgBoxErr(oApp.Tr(Me, 128759920423922543, strMsg))
        Return False
      End If
      '----------------------------------------------------------------------------------
      '--- Controlla se sono stati selezionati gli articolo
      '----------------------------------------------------------------------------------
      If strWhereArtico.Trim = "." Then
        strMsg = "Attenzione!" & vbCrLf & _
          "Non è stata effettuata la selezione degli articoli." & vbCrLf & _
          "Selezionarli ora?"
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128759921612203793, strMsg)) = Windows.Forms.DialogResult.Yes Then
          cmdSelez_Click(cmdSelez, Nothing)
        End If
      End If
      If strWhereArtico.Trim = "." Then Return False
      '----------------------------------------------------------------------------------
      '--- Chiede conferma dell'elaborazione
      '----------------------------------------------------------------------------------
      strMsg = "Attenzione!" & vbCrLf & _
        "Il Listino |" & edListino.Text & "| sarà aggiornato." & vbCrLf & _
        "Procedere con l'elaborazione?"
      If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128759926092828793, strMsg)) = Windows.Forms.DialogResult.No Then
        Return False
      End If
      '----------------------------------------------------------------------------------
      Me.Cursor = Cursors.WaitCursor
      '----------------------------------------------------------------------------------
      If Not oCleRipc.Elabora(NTSCDec(edTotale.Text), NTSCInt(edListino.Text), _
            NTSCInt(edCodice.Text), NTSCInt(cbRipart.SelectedValue), edDataElab.Text, _
            edDaData.Text, edAData.Text, ckAggArticoli.Checked, strWhereArtico, _
            optAvan0.Checked, optAvan1.Checked, optAvan2.Checked, ckAggiungiAListino.Checked) Then Return False
      '----------------------------------------------------------------------------------
      oApp.MsgBoxInfo(oApp.Tr(Me, 128763084861606355, "Elaborazione completata regolarmente."))
      '----------------------------------------------------------------------------------
      Return True
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Function

  Public Overridable Sub CaricaCombo()
    Try
      Dim dttRipart As New DataTable()

      dttRipart.Columns.Add("cod", GetType(String))
      dttRipart.Columns.Add("val", GetType(String))
      dttRipart.Rows.Add(New Object() {"0", "Quantità venduta"})
      dttRipart.Rows.Add(New Object() {"1", "Quantità prodotta"})
      dttRipart.Rows.Add(New Object() {"2", "Quantità a budget"})
      dttRipart.Rows.Add(New Object() {"3", "Quantità in stato di avanzamento"})
      dttRipart.Rows.Add(New Object() {"4", "Valore carico"})
      dttRipart.AcceptChanges()
      cbRipart.DataSource = dttRipart
      cbRipart.ValueMember = "cod"
      cbRipart.DisplayMember = "val"
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overloads Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    'questa funzione riceve gli eventi dall'ENTITY: rimappata rispetto a quella standard di FRM__CHILD
    'prima eseguo quella standard
    Dim strTmp() As String
    Dim i As Integer = 0

    If Not IsMyThrowRemoteEvent() Then Return 'il messaggio non è per questa form ...
    MyBase.GestisciEventiEntity(sender, e)

    Try
      'adesso gestisco le specifiche
      'devo inserire delle funzioni qui sotto per fare in modo che al variare di dati nell'entity delle informazioni 
      'legate all'interfaccia grafica (ui) vengano allineate a quanto richiesto dall'entity

      If e.TipoEvento.Length < 10 Then Return
      strTmp = e.TipoEvento.Split(CType("|", Char))
      For i = 0 To strTmp.Length - 1
        Select Case strTmp(i).Substring(0, 10)
          Case "STATUSBAR:"
            lbStatus.Text = e.Message
            Me.Refresh()
        End Select
      Next
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region
End Class
