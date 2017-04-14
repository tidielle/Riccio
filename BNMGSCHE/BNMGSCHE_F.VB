Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGSCHE_F
  Public oCleSche As CLEMGSCHE
  Public oCallParams As CLE__CLDP

  Public bOnLoadingFromSettings As Boolean = False

#Region "Moduli"
  Private Moduli_P As Integer = bsModMG + bsModVE
  Private ModuliExt_P As Integer = bsModExtMGE
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

#Region "Dichiarazione Controlli"
  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
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
  Public WithEvents tlbStampaGriglia As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbStatus As NTSInformatica.NTSLabel
  Public WithEvents fmPrelmovimenti As NTSInformatica.NTSGroupBox
  Public WithEvents ckSerie As NTSInformatica.NTSCheckBox
  Public WithEvents cbTipodoc As NTSInformatica.NTSComboBox
  Public WithEvents ckSalto As NTSInformatica.NTSCheckBox
  Public WithEvents edSerie As NTSInformatica.NTSTextBoxStr
#End Region

#Region "Inizializzazione"
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGSCHE", "BEMGSCHE", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128496233436616000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleSche = CType(oTmp, CLEMGSCHE)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGSCHE", strRemoteServer, strRemotePort)
    AddHandler oCleSche.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleSche.Init(oApp, oScript, oMenu.oCleComm, "MOVMAG", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGSCHE_F))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaGriglia = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.lbStatus = New NTSInformatica.NTSLabel
    Me.fmPrelmovimenti = New NTSInformatica.NTSGroupBox
    Me.ckSerie = New NTSInformatica.NTSCheckBox
    Me.edSerie = New NTSInformatica.NTSTextBoxStr
    Me.cbTipodoc = New NTSInformatica.NTSComboBox
    Me.edDaconto = New NTSInformatica.NTSTextBoxNum
    Me.lbAconto = New NTSInformatica.NTSLabel
    Me.edAconto = New NTSInformatica.NTSTextBoxNum
    Me.lbDaconto = New NTSInformatica.NTSLabel
    Me.lbXx_codcfam = New NTSInformatica.NTSLabel
    Me.edDatini = New NTSInformatica.NTSTextBoxData
    Me.lbDatini = New NTSInformatica.NTSLabel
    Me.edDatfin = New NTSInformatica.NTSTextBoxData
    Me.lbDatfin = New NTSInformatica.NTSLabel
    Me.ckSaldiIniziali = New NTSInformatica.NTSCheckBox
    Me.ckStorico = New NTSInformatica.NTSCheckBox
    Me.lbDamagaz = New NTSInformatica.NTSLabel
    Me.edDamagaz = New NTSInformatica.NTSTextBoxNum
    Me.edAmagaz = New NTSInformatica.NTSTextBoxNum
    Me.lbAmagaz = New NTSInformatica.NTSLabel
    Me.lbCodcfam = New NTSInformatica.NTSLabel
    Me.edCodcfam = New NTSInformatica.NTSTextBoxStr
    Me.lbSep = New NTSInformatica.NTSLabel
    Me.edSottogr = New NTSInformatica.NTSTextBoxNum
    Me.edGruppo = New NTSInformatica.NTSTextBoxNum
    Me.lbGruppo = New NTSInformatica.NTSLabel
    Me.edDacodart = New NTSInformatica.NTSTextBoxStr
    Me.lbDacodart = New NTSInformatica.NTSLabel
    Me.lbAcodart = New NTSInformatica.NTSLabel
    Me.edAcodart = New NTSInformatica.NTSTextBoxStr
    Me.cbIncludi = New NTSInformatica.NTSComboBox
    Me.lbIncludi = New NTSInformatica.NTSLabel
    Me.ckStampaFiltri = New NTSInformatica.NTSCheckBox
    Me.ckSalto = New NTSInformatica.NTSCheckBox
    Me.lbTipo = New NTSInformatica.NTSLabel
    Me.cbTipoStampa = New NTSInformatica.NTSComboBox
    Me.lbTipoStampa = New NTSInformatica.NTSLabel
    Me.pnTipoStampa = New NTSInformatica.NTSPanel
    Me.opTipoStampa1 = New NTSInformatica.NTSRadioButton
    Me.opTipoStampa0 = New NTSInformatica.NTSRadioButton
    Me.fmCheck = New NTSInformatica.NTSGroupBox
    Me.pnRight = New NTSInformatica.NTSPanel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmPrelmovimenti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmPrelmovimenti.SuspendLayout()
    CType(Me.ckSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipodoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDaconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatfin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSaldiIniziali.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckStorico.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDamagaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAmagaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodcfam.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSottogr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edGruppo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDacodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAcodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbIncludi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckStampaFiltri.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSalto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipoStampa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTipoStampa, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTipoStampa.SuspendLayout()
    CType(Me.opTipoStampa1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opTipoStampa0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmCheck, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmCheck.SuspendLayout()
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnRight.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbStampaGriglia})
    Me.NtsBarManager1.MaxItemId = 21
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaGriglia), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
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
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.Id = 16
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 4
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 5
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbStampaGriglia
    '
    Me.tlbStampaGriglia.Caption = "Stampa su griglia"
    Me.tlbStampaGriglia.Glyph = CType(resources.GetObject("tlbStampaGriglia.Glyph"), System.Drawing.Image)
    Me.tlbStampaGriglia.Id = 17
    Me.tlbStampaGriglia.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F11)
    Me.tlbStampaGriglia.Name = "tlbStampaGriglia"
    Me.tlbStampaGriglia.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 11
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 12
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'lbStatus
    '
    Me.lbStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbStatus.AutoSize = True
    Me.lbStatus.BackColor = System.Drawing.Color.Transparent
    Me.lbStatus.Location = New System.Drawing.Point(9, 313)
    Me.lbStatus.Name = "lbStatus"
    Me.lbStatus.NTSDbField = ""
    Me.lbStatus.Size = New System.Drawing.Size(43, 13)
    Me.lbStatus.TabIndex = 34
    Me.lbStatus.Text = "Pronto."
    Me.lbStatus.Tooltip = ""
    Me.lbStatus.UseMnemonic = False
    '
    'fmPrelmovimenti
    '
    Me.fmPrelmovimenti.AllowDrop = True
    Me.fmPrelmovimenti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmPrelmovimenti.Appearance.Options.UseBackColor = True
    Me.fmPrelmovimenti.Controls.Add(Me.ckSerie)
    Me.fmPrelmovimenti.Controls.Add(Me.edSerie)
    Me.fmPrelmovimenti.Controls.Add(Me.cbTipodoc)
    Me.fmPrelmovimenti.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmPrelmovimenti.Location = New System.Drawing.Point(16, 226)
    Me.fmPrelmovimenti.Name = "fmPrelmovimenti"
    Me.fmPrelmovimenti.Size = New System.Drawing.Size(525, 62)
    Me.fmPrelmovimenti.TabIndex = 82
    Me.fmPrelmovimenti.Text = "Preleva movimenti:"
    '
    'ckSerie
    '
    Me.ckSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSerie.Location = New System.Drawing.Point(267, 29)
    Me.ckSerie.Name = "ckSerie"
    Me.ckSerie.NTSCheckValue = "S"
    Me.ckSerie.NTSUnCheckValue = "N"
    Me.ckSerie.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSerie.Properties.Appearance.Options.UseBackColor = True
    Me.ckSerie.Properties.Caption = "Serie"
    Me.ckSerie.Size = New System.Drawing.Size(47, 19)
    Me.ckSerie.TabIndex = 41
    '
    'edSerie
    '
    Me.edSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSerie.Enabled = False
    Me.edSerie.Location = New System.Drawing.Point(317, 28)
    Me.edSerie.Name = "edSerie"
    Me.edSerie.NTSDbField = ""
    Me.edSerie.NTSForzaVisZoom = False
    Me.edSerie.NTSOldValue = ""
    Me.edSerie.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSerie.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSerie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSerie.Properties.MaxLength = 65536
    Me.edSerie.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSerie.Size = New System.Drawing.Size(45, 20)
    Me.edSerie.TabIndex = 61
    '
    'cbTipodoc
    '
    Me.cbTipodoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipodoc.DataSource = Nothing
    Me.cbTipodoc.DisplayMember = ""
    Me.cbTipodoc.Location = New System.Drawing.Point(18, 28)
    Me.cbTipodoc.Name = "cbTipodoc"
    Me.cbTipodoc.NTSDbField = ""
    Me.cbTipodoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipodoc.Properties.DropDownRows = 30
    Me.cbTipodoc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipodoc.SelectedValue = ""
    Me.cbTipodoc.Size = New System.Drawing.Size(243, 20)
    Me.cbTipodoc.TabIndex = 0
    Me.cbTipodoc.ValueMember = ""
    '
    'edDaconto
    '
    Me.edDaconto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDaconto.EditValue = "0"
    Me.edDaconto.Location = New System.Drawing.Point(131, 112)
    Me.edDaconto.Name = "edDaconto"
    Me.edDaconto.NTSDbField = ""
    Me.edDaconto.NTSFormat = "0"
    Me.edDaconto.NTSForzaVisZoom = False
    Me.edDaconto.NTSOldValue = ""
    Me.edDaconto.Properties.Appearance.Options.UseTextOptions = True
    Me.edDaconto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDaconto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDaconto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDaconto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDaconto.Properties.MaxLength = 65536
    Me.edDaconto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDaconto.Size = New System.Drawing.Size(83, 20)
    Me.edDaconto.TabIndex = 152
    '
    'lbAconto
    '
    Me.lbAconto.AutoSize = True
    Me.lbAconto.BackColor = System.Drawing.Color.Transparent
    Me.lbAconto.Location = New System.Drawing.Point(223, 116)
    Me.lbAconto.Name = "lbAconto"
    Me.lbAconto.NTSDbField = ""
    Me.lbAconto.Size = New System.Drawing.Size(11, 13)
    Me.lbAconto.TabIndex = 154
    Me.lbAconto.Text = "/"
    Me.lbAconto.Tooltip = ""
    Me.lbAconto.UseMnemonic = False
    '
    'edAconto
    '
    Me.edAconto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAconto.EditValue = "0"
    Me.edAconto.Location = New System.Drawing.Point(241, 112)
    Me.edAconto.Name = "edAconto"
    Me.edAconto.NTSDbField = ""
    Me.edAconto.NTSFormat = "0"
    Me.edAconto.NTSForzaVisZoom = False
    Me.edAconto.NTSOldValue = ""
    Me.edAconto.Properties.Appearance.Options.UseTextOptions = True
    Me.edAconto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAconto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAconto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAconto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAconto.Properties.MaxLength = 65536
    Me.edAconto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAconto.Size = New System.Drawing.Size(84, 20)
    Me.edAconto.TabIndex = 153
    '
    'lbDaconto
    '
    Me.lbDaconto.AutoSize = True
    Me.lbDaconto.BackColor = System.Drawing.Color.Transparent
    Me.lbDaconto.Location = New System.Drawing.Point(12, 116)
    Me.lbDaconto.Name = "lbDaconto"
    Me.lbDaconto.NTSDbField = ""
    Me.lbDaconto.Size = New System.Drawing.Size(60, 13)
    Me.lbDaconto.TabIndex = 151
    Me.lbDaconto.Text = "Da/a conto"
    Me.lbDaconto.Tooltip = ""
    Me.lbDaconto.UseMnemonic = False
    '
    'lbXx_codcfam
    '
    Me.lbXx_codcfam.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codcfam.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codcfam.Location = New System.Drawing.Point(221, 156)
    Me.lbXx_codcfam.Name = "lbXx_codcfam"
    Me.lbXx_codcfam.NTSDbField = ""
    Me.lbXx_codcfam.Size = New System.Drawing.Size(284, 20)
    Me.lbXx_codcfam.TabIndex = 146
    Me.lbXx_codcfam.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_codcfam.Tooltip = ""
    Me.lbXx_codcfam.UseMnemonic = False
    '
    'edDatini
    '
    Me.edDatini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatini.EditValue = ""
    Me.edDatini.Location = New System.Drawing.Point(241, 46)
    Me.edDatini.Name = "edDatini"
    Me.edDatini.NTSDbField = ""
    Me.edDatini.NTSForzaVisZoom = False
    Me.edDatini.NTSOldValue = ""
    Me.edDatini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatini.Properties.MaxLength = 65536
    Me.edDatini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatini.Size = New System.Drawing.Size(84, 20)
    Me.edDatini.TabIndex = 131
    '
    'lbDatini
    '
    Me.lbDatini.AutoSize = True
    Me.lbDatini.BackColor = System.Drawing.Color.Transparent
    Me.lbDatini.Location = New System.Drawing.Point(183, 49)
    Me.lbDatini.Name = "lbDatini"
    Me.lbDatini.NTSDbField = ""
    Me.lbDatini.Size = New System.Drawing.Size(55, 13)
    Me.lbDatini.TabIndex = 132
    Me.lbDatini.Text = "Da/a data"
    Me.lbDatini.Tooltip = ""
    Me.lbDatini.UseMnemonic = False
    '
    'edDatfin
    '
    Me.edDatfin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatfin.Location = New System.Drawing.Point(347, 44)
    Me.edDatfin.Name = "edDatfin"
    Me.edDatfin.NTSDbField = ""
    Me.edDatfin.NTSForzaVisZoom = False
    Me.edDatfin.NTSOldValue = ""
    Me.edDatfin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatfin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatfin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatfin.Properties.MaxLength = 65536
    Me.edDatfin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatfin.Size = New System.Drawing.Size(84, 20)
    Me.edDatfin.TabIndex = 133
    '
    'lbDatfin
    '
    Me.lbDatfin.AutoSize = True
    Me.lbDatfin.BackColor = System.Drawing.Color.Transparent
    Me.lbDatfin.Location = New System.Drawing.Point(330, 49)
    Me.lbDatfin.Name = "lbDatfin"
    Me.lbDatfin.NTSDbField = ""
    Me.lbDatfin.Size = New System.Drawing.Size(11, 13)
    Me.lbDatfin.TabIndex = 134
    Me.lbDatfin.Text = "/"
    Me.lbDatfin.Tooltip = ""
    Me.lbDatfin.UseMnemonic = False
    '
    'ckSaldiIniziali
    '
    Me.ckSaldiIniziali.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSaldiIniziali.EditValue = True
    Me.ckSaldiIniziali.Enabled = False
    Me.ckSaldiIniziali.Location = New System.Drawing.Point(433, 46)
    Me.ckSaldiIniziali.Name = "ckSaldiIniziali"
    Me.ckSaldiIniziali.NTSCheckValue = "S"
    Me.ckSaldiIniziali.NTSUnCheckValue = "N"
    Me.ckSaldiIniziali.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSaldiIniziali.Properties.Appearance.Options.UseBackColor = True
    Me.ckSaldiIniziali.Properties.Caption = "Calcola saldi iniziali"
    Me.ckSaldiIniziali.Size = New System.Drawing.Size(111, 19)
    Me.ckSaldiIniziali.TabIndex = 130
    '
    'ckStorico
    '
    Me.ckStorico.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckStorico.Location = New System.Drawing.Point(11, 45)
    Me.ckStorico.Name = "ckStorico"
    Me.ckStorico.NTSCheckValue = "S"
    Me.ckStorico.NTSUnCheckValue = "N"
    Me.ckStorico.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckStorico.Properties.Appearance.Options.UseBackColor = True
    Me.ckStorico.Properties.Caption = "Considera lo storico"
    Me.ckStorico.Size = New System.Drawing.Size(123, 19)
    Me.ckStorico.TabIndex = 129
    '
    'lbDamagaz
    '
    Me.lbDamagaz.AutoSize = True
    Me.lbDamagaz.BackColor = System.Drawing.Color.Transparent
    Me.lbDamagaz.Location = New System.Drawing.Point(12, 71)
    Me.lbDamagaz.Name = "lbDamagaz"
    Me.lbDamagaz.NTSDbField = ""
    Me.lbDamagaz.Size = New System.Drawing.Size(83, 13)
    Me.lbDamagaz.TabIndex = 115
    Me.lbDamagaz.Text = "Da/a magazzino"
    Me.lbDamagaz.Tooltip = ""
    Me.lbDamagaz.UseMnemonic = False
    '
    'edDamagaz
    '
    Me.edDamagaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDamagaz.EditValue = "0"
    Me.edDamagaz.Location = New System.Drawing.Point(131, 68)
    Me.edDamagaz.Name = "edDamagaz"
    Me.edDamagaz.NTSDbField = ""
    Me.edDamagaz.NTSFormat = "0"
    Me.edDamagaz.NTSForzaVisZoom = False
    Me.edDamagaz.NTSOldValue = ""
    Me.edDamagaz.Properties.Appearance.Options.UseTextOptions = True
    Me.edDamagaz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDamagaz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDamagaz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDamagaz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDamagaz.Properties.MaxLength = 65536
    Me.edDamagaz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDamagaz.Size = New System.Drawing.Size(84, 20)
    Me.edDamagaz.TabIndex = 116
    '
    'edAmagaz
    '
    Me.edAmagaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAmagaz.EditValue = "0"
    Me.edAmagaz.Location = New System.Drawing.Point(241, 68)
    Me.edAmagaz.Name = "edAmagaz"
    Me.edAmagaz.NTSDbField = ""
    Me.edAmagaz.NTSFormat = "0"
    Me.edAmagaz.NTSForzaVisZoom = False
    Me.edAmagaz.NTSOldValue = ""
    Me.edAmagaz.Properties.Appearance.Options.UseTextOptions = True
    Me.edAmagaz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAmagaz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAmagaz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAmagaz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAmagaz.Properties.MaxLength = 65536
    Me.edAmagaz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAmagaz.Size = New System.Drawing.Size(84, 20)
    Me.edAmagaz.TabIndex = 117
    '
    'lbAmagaz
    '
    Me.lbAmagaz.AutoSize = True
    Me.lbAmagaz.BackColor = System.Drawing.Color.Transparent
    Me.lbAmagaz.Location = New System.Drawing.Point(224, 71)
    Me.lbAmagaz.Name = "lbAmagaz"
    Me.lbAmagaz.NTSDbField = ""
    Me.lbAmagaz.Size = New System.Drawing.Size(11, 13)
    Me.lbAmagaz.TabIndex = 118
    Me.lbAmagaz.Text = "/"
    Me.lbAmagaz.Tooltip = ""
    Me.lbAmagaz.UseMnemonic = False
    '
    'lbCodcfam
    '
    Me.lbCodcfam.AutoSize = True
    Me.lbCodcfam.BackColor = System.Drawing.Color.Transparent
    Me.lbCodcfam.Location = New System.Drawing.Point(12, 159)
    Me.lbCodcfam.Name = "lbCodcfam"
    Me.lbCodcfam.NTSDbField = ""
    Me.lbCodcfam.Size = New System.Drawing.Size(110, 13)
    Me.lbCodcfam.TabIndex = 128
    Me.lbCodcfam.Text = "Linea/famiglia articolo"
    Me.lbCodcfam.Tooltip = ""
    Me.lbCodcfam.UseMnemonic = False
    '
    'edCodcfam
    '
    Me.edCodcfam.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodcfam.Location = New System.Drawing.Point(132, 156)
    Me.edCodcfam.Name = "edCodcfam"
    Me.edCodcfam.NTSDbField = ""
    Me.edCodcfam.NTSForzaVisZoom = False
    Me.edCodcfam.NTSOldValue = ""
    Me.edCodcfam.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodcfam.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodcfam.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodcfam.Properties.MaxLength = 65536
    Me.edCodcfam.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodcfam.Size = New System.Drawing.Size(83, 20)
    Me.edCodcfam.TabIndex = 127
    '
    'lbSep
    '
    Me.lbSep.AutoSize = True
    Me.lbSep.BackColor = System.Drawing.Color.Transparent
    Me.lbSep.Location = New System.Drawing.Point(224, 137)
    Me.lbSep.Name = "lbSep"
    Me.lbSep.NTSDbField = ""
    Me.lbSep.Size = New System.Drawing.Size(11, 13)
    Me.lbSep.TabIndex = 126
    Me.lbSep.Text = "/"
    Me.lbSep.Tooltip = ""
    Me.lbSep.UseMnemonic = False
    '
    'edSottogr
    '
    Me.edSottogr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSottogr.EditValue = "0"
    Me.edSottogr.Location = New System.Drawing.Point(241, 134)
    Me.edSottogr.Name = "edSottogr"
    Me.edSottogr.NTSDbField = ""
    Me.edSottogr.NTSFormat = "0"
    Me.edSottogr.NTSForzaVisZoom = False
    Me.edSottogr.NTSOldValue = ""
    Me.edSottogr.Properties.Appearance.Options.UseTextOptions = True
    Me.edSottogr.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSottogr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSottogr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSottogr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSottogr.Properties.MaxLength = 65536
    Me.edSottogr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSottogr.Size = New System.Drawing.Size(84, 20)
    Me.edSottogr.TabIndex = 125
    '
    'edGruppo
    '
    Me.edGruppo.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edGruppo.EditValue = "0"
    Me.edGruppo.Location = New System.Drawing.Point(132, 134)
    Me.edGruppo.Name = "edGruppo"
    Me.edGruppo.NTSDbField = ""
    Me.edGruppo.NTSFormat = "0"
    Me.edGruppo.NTSForzaVisZoom = False
    Me.edGruppo.NTSOldValue = ""
    Me.edGruppo.Properties.Appearance.Options.UseTextOptions = True
    Me.edGruppo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edGruppo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edGruppo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edGruppo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edGruppo.Properties.MaxLength = 65536
    Me.edGruppo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edGruppo.Size = New System.Drawing.Size(83, 20)
    Me.edGruppo.TabIndex = 124
    '
    'lbGruppo
    '
    Me.lbGruppo.AutoSize = True
    Me.lbGruppo.BackColor = System.Drawing.Color.Transparent
    Me.lbGruppo.Location = New System.Drawing.Point(13, 137)
    Me.lbGruppo.Name = "lbGruppo"
    Me.lbGruppo.NTSDbField = ""
    Me.lbGruppo.Size = New System.Drawing.Size(106, 13)
    Me.lbGruppo.TabIndex = 123
    Me.lbGruppo.Text = "Gruppo/Sottogruppo"
    Me.lbGruppo.Tooltip = ""
    Me.lbGruppo.UseMnemonic = False
    '
    'edDacodart
    '
    Me.edDacodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDacodart.Location = New System.Drawing.Point(131, 90)
    Me.edDacodart.Name = "edDacodart"
    Me.edDacodart.NTSDbField = ""
    Me.edDacodart.NTSForzaVisZoom = False
    Me.edDacodart.NTSOldValue = ""
    Me.edDacodart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDacodart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDacodart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDacodart.Properties.MaxLength = 65536
    Me.edDacodart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDacodart.Size = New System.Drawing.Size(194, 20)
    Me.edDacodart.TabIndex = 119
    '
    'lbDacodart
    '
    Me.lbDacodart.AutoSize = True
    Me.lbDacodart.BackColor = System.Drawing.Color.Transparent
    Me.lbDacodart.Location = New System.Drawing.Point(12, 93)
    Me.lbDacodart.Name = "lbDacodart"
    Me.lbDacodart.NTSDbField = ""
    Me.lbDacodart.Size = New System.Drawing.Size(68, 13)
    Me.lbDacodart.TabIndex = 120
    Me.lbDacodart.Text = "Da/a articolo"
    Me.lbDacodart.Tooltip = ""
    Me.lbDacodart.UseMnemonic = False
    '
    'lbAcodart
    '
    Me.lbAcodart.AutoSize = True
    Me.lbAcodart.BackColor = System.Drawing.Color.Transparent
    Me.lbAcodart.Location = New System.Drawing.Point(330, 93)
    Me.lbAcodart.Name = "lbAcodart"
    Me.lbAcodart.NTSDbField = ""
    Me.lbAcodart.Size = New System.Drawing.Size(11, 13)
    Me.lbAcodart.TabIndex = 121
    Me.lbAcodart.Text = "/"
    Me.lbAcodart.Tooltip = ""
    Me.lbAcodart.UseMnemonic = False
    '
    'edAcodart
    '
    Me.edAcodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAcodart.Location = New System.Drawing.Point(347, 90)
    Me.edAcodart.Name = "edAcodart"
    Me.edAcodart.NTSDbField = ""
    Me.edAcodart.NTSForzaVisZoom = False
    Me.edAcodart.NTSOldValue = ""
    Me.edAcodart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAcodart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAcodart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAcodart.Properties.MaxLength = 65536
    Me.edAcodart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAcodart.Size = New System.Drawing.Size(194, 20)
    Me.edAcodart.TabIndex = 122
    '
    'cbIncludi
    '
    Me.cbIncludi.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbIncludi.DataSource = Nothing
    Me.cbIncludi.DisplayMember = ""
    Me.cbIncludi.Location = New System.Drawing.Point(132, 200)
    Me.cbIncludi.Name = "cbIncludi"
    Me.cbIncludi.NTSDbField = ""
    Me.cbIncludi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbIncludi.Properties.DropDownRows = 30
    Me.cbIncludi.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbIncludi.SelectedValue = ""
    Me.cbIncludi.Size = New System.Drawing.Size(206, 20)
    Me.cbIncludi.TabIndex = 120
    Me.cbIncludi.ValueMember = ""
    '
    'lbIncludi
    '
    Me.lbIncludi.AutoSize = True
    Me.lbIncludi.BackColor = System.Drawing.Color.Transparent
    Me.lbIncludi.Location = New System.Drawing.Point(13, 203)
    Me.lbIncludi.Name = "lbIncludi"
    Me.lbIncludi.NTSDbField = ""
    Me.lbIncludi.Size = New System.Drawing.Size(38, 13)
    Me.lbIncludi.TabIndex = 129
    Me.lbIncludi.Text = "Includi"
    Me.lbIncludi.Tooltip = ""
    Me.lbIncludi.UseMnemonic = False
    '
    'ckStampaFiltri
    '
    Me.ckStampaFiltri.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckStampaFiltri.Location = New System.Drawing.Point(14, 32)
    Me.ckStampaFiltri.Name = "ckStampaFiltri"
    Me.ckStampaFiltri.NTSCheckValue = "S"
    Me.ckStampaFiltri.NTSUnCheckValue = "N"
    Me.ckStampaFiltri.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckStampaFiltri.Properties.Appearance.Options.UseBackColor = True
    Me.ckStampaFiltri.Properties.Caption = "Stampa i filtri impostati nel report"
    Me.ckStampaFiltri.Size = New System.Drawing.Size(190, 19)
    Me.ckStampaFiltri.TabIndex = 63
    '
    'ckSalto
    '
    Me.ckSalto.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSalto.Location = New System.Drawing.Point(14, 10)
    Me.ckSalto.Name = "ckSalto"
    Me.ckSalto.NTSCheckValue = "S"
    Me.ckSalto.NTSUnCheckValue = "N"
    Me.ckSalto.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSalto.Properties.Appearance.Options.UseBackColor = True
    Me.ckSalto.Properties.Caption = "Salto pagina per ogni articolo"
    Me.ckSalto.Size = New System.Drawing.Size(170, 19)
    Me.ckSalto.TabIndex = 46
    '
    'lbTipo
    '
    Me.lbTipo.AutoSize = True
    Me.lbTipo.BackColor = System.Drawing.Color.Transparent
    Me.lbTipo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbTipo.Location = New System.Drawing.Point(12, 181)
    Me.lbTipo.Name = "lbTipo"
    Me.lbTipo.NTSDbField = ""
    Me.lbTipo.Size = New System.Drawing.Size(84, 13)
    Me.lbTipo.TabIndex = 119
    Me.lbTipo.Text = "TIPO STAMPA"
    Me.lbTipo.Tooltip = ""
    Me.lbTipo.UseMnemonic = False
    '
    'cbTipoStampa
    '
    Me.cbTipoStampa.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipoStampa.DataSource = Nothing
    Me.cbTipoStampa.DisplayMember = ""
    Me.cbTipoStampa.Location = New System.Drawing.Point(132, 178)
    Me.cbTipoStampa.Name = "cbTipoStampa"
    Me.cbTipoStampa.NTSDbField = ""
    Me.cbTipoStampa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipoStampa.Properties.DropDownRows = 30
    Me.cbTipoStampa.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipoStampa.SelectedValue = ""
    Me.cbTipoStampa.Size = New System.Drawing.Size(206, 20)
    Me.cbTipoStampa.TabIndex = 118
    Me.cbTipoStampa.ValueMember = ""
    '
    'lbTipoStampa
    '
    Me.lbTipoStampa.AutoSize = True
    Me.lbTipoStampa.BackColor = System.Drawing.Color.Transparent
    Me.lbTipoStampa.Location = New System.Drawing.Point(4, 237)
    Me.lbTipoStampa.Name = "lbTipoStampa"
    Me.lbTipoStampa.NTSDbField = ""
    Me.lbTipoStampa.Size = New System.Drawing.Size(76, 13)
    Me.lbTipoStampa.TabIndex = 95
    Me.lbTipoStampa.Text = "Tipo di stampa"
    Me.lbTipoStampa.Tooltip = ""
    Me.lbTipoStampa.UseMnemonic = False
    '
    'pnTipoStampa
    '
    Me.pnTipoStampa.AllowDrop = True
    Me.pnTipoStampa.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTipoStampa.Appearance.Options.UseBackColor = True
    Me.pnTipoStampa.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTipoStampa.Controls.Add(Me.opTipoStampa1)
    Me.pnTipoStampa.Controls.Add(Me.opTipoStampa0)
    Me.pnTipoStampa.Cursor = System.Windows.Forms.Cursors.Hand
    Me.pnTipoStampa.Location = New System.Drawing.Point(86, 230)
    Me.pnTipoStampa.Name = "pnTipoStampa"
    Me.pnTipoStampa.NTSActiveTrasparency = True
    Me.pnTipoStampa.Size = New System.Drawing.Size(145, 28)
    Me.pnTipoStampa.TabIndex = 94
    Me.pnTipoStampa.Text = "NtsPanel1"
    '
    'opTipoStampa1
    '
    Me.opTipoStampa1.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTipoStampa1.EditValue = True
    Me.opTipoStampa1.Location = New System.Drawing.Point(65, 5)
    Me.opTipoStampa1.Name = "opTipoStampa1"
    Me.opTipoStampa1.NTSCheckValue = "S"
    Me.opTipoStampa1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTipoStampa1.Properties.Appearance.Options.UseBackColor = True
    Me.opTipoStampa1.Properties.Caption = "Completo"
    Me.opTipoStampa1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTipoStampa1.Size = New System.Drawing.Size(75, 19)
    Me.opTipoStampa1.TabIndex = 1
    '
    'opTipoStampa0
    '
    Me.opTipoStampa0.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTipoStampa0.Location = New System.Drawing.Point(3, 5)
    Me.opTipoStampa0.Name = "opTipoStampa0"
    Me.opTipoStampa0.NTSCheckValue = "S"
    Me.opTipoStampa0.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTipoStampa0.Properties.Appearance.Options.UseBackColor = True
    Me.opTipoStampa0.Properties.Caption = "Ridotto"
    Me.opTipoStampa0.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTipoStampa0.Size = New System.Drawing.Size(65, 19)
    Me.opTipoStampa0.TabIndex = 0
    '
    'fmCheck
    '
    Me.fmCheck.AllowDrop = True
    Me.fmCheck.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmCheck.Appearance.Options.UseBackColor = True
    Me.fmCheck.Controls.Add(Me.ckStampaFiltri)
    Me.fmCheck.Controls.Add(Me.ckSalto)
    Me.fmCheck.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmCheck.Location = New System.Drawing.Point(7, 14)
    Me.fmCheck.Name = "fmCheck"
    Me.fmCheck.ShowCaption = False
    Me.fmCheck.Size = New System.Drawing.Size(224, 62)
    Me.fmCheck.TabIndex = 119
    '
    'pnRight
    '
    Me.pnRight.AllowDrop = True
    Me.pnRight.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnRight.Appearance.Options.UseBackColor = True
    Me.pnRight.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnRight.Controls.Add(Me.fmCheck)
    Me.pnRight.Controls.Add(Me.pnTipoStampa)
    Me.pnRight.Controls.Add(Me.lbTipoStampa)
    Me.pnRight.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnRight.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnRight.Location = New System.Drawing.Point(550, 30)
    Me.pnRight.Name = "pnRight"
    Me.pnRight.NTSActiveTrasparency = True
    Me.pnRight.Size = New System.Drawing.Size(242, 305)
    Me.pnRight.TabIndex = 155
    Me.pnRight.Text = "NtsPanel1"
    '
    'FRMMGSCHE_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(792, 335)
    Me.Controls.Add(Me.pnRight)
    Me.Controls.Add(Me.cbIncludi)
    Me.Controls.Add(Me.lbTipo)
    Me.Controls.Add(Me.lbIncludi)
    Me.Controls.Add(Me.edDaconto)
    Me.Controls.Add(Me.cbTipoStampa)
    Me.Controls.Add(Me.lbAconto)
    Me.Controls.Add(Me.edAconto)
    Me.Controls.Add(Me.lbDaconto)
    Me.Controls.Add(Me.lbXx_codcfam)
    Me.Controls.Add(Me.lbStatus)
    Me.Controls.Add(Me.edDatini)
    Me.Controls.Add(Me.lbDatini)
    Me.Controls.Add(Me.fmPrelmovimenti)
    Me.Controls.Add(Me.edDatfin)
    Me.Controls.Add(Me.lbDatfin)
    Me.Controls.Add(Me.ckSaldiIniziali)
    Me.Controls.Add(Me.ckStorico)
    Me.Controls.Add(Me.edAcodart)
    Me.Controls.Add(Me.lbDamagaz)
    Me.Controls.Add(Me.lbAcodart)
    Me.Controls.Add(Me.edDamagaz)
    Me.Controls.Add(Me.lbDacodart)
    Me.Controls.Add(Me.edAmagaz)
    Me.Controls.Add(Me.edDacodart)
    Me.Controls.Add(Me.lbAmagaz)
    Me.Controls.Add(Me.lbGruppo)
    Me.Controls.Add(Me.lbCodcfam)
    Me.Controls.Add(Me.edGruppo)
    Me.Controls.Add(Me.edCodcfam)
    Me.Controls.Add(Me.edSottogr)
    Me.Controls.Add(Me.lbSep)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRMMGSCHE_F"
    Me.Text = "STAMPA/VISUALIZZAZIONE SCHEDE ARTICOLI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmPrelmovimenti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmPrelmovimenti.ResumeLayout(False)
    Me.fmPrelmovimenti.PerformLayout()
    CType(Me.ckSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipodoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDaconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatfin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSaldiIniziali.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckStorico.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDamagaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAmagaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodcfam.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSottogr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edGruppo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDacodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAcodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbIncludi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckStampaFiltri.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSalto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipoStampa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTipoStampa, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTipoStampa.ResumeLayout(False)
    Me.pnTipoStampa.PerformLayout()
    CType(Me.opTipoStampa1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opTipoStampa0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmCheck, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmCheck.ResumeLayout(False)
    Me.fmCheck.PerformLayout()
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnRight.ResumeLayout(False)
    Me.pnRight.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Public Overridable Sub InitControls()
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, False)
    '----------------------------------------------------------------------------------------------------------------
    Try
      '--------------------------------------------------------------------------------------------------------------
      Try
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbStampaGriglia.GlyphPath = (oApp.ChildImageDir & "\prngrid.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
      End Try
      '--------------------------------------------------------------------------------------------------------------
      tlbMain.NTSSetToolTip()
      '--------------------------------------------------------------------------------------------------------------
      opTipoStampa0.NTSSetParam(oMenu, oApp.Tr(Me, 128735523170922000, "Ridotto"), "S")
      opTipoStampa1.NTSSetParam(oMenu, oApp.Tr(Me, 128595426229218750, "Completo"), "S")
      ckStorico.NTSSetParam(oMenu, oApp.Tr(Me, 128644905943641169, "Considera lo storico"), "S", "N")
      edDatini.NTSSetParam(oMenu, oApp.Tr(Me, 128644905942858714, "Dalla data"), False)
      edDatfin.NTSSetParam(oMenu, oApp.Tr(Me, 128644905943171696, "alla data"), False)
      ckSaldiIniziali.NTSSetParam(oMenu, oApp.Tr(Me, 128595426238125000, "C&onsidera solo le righe non evase"), "S", "N")
      edAmagaz.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426237500000, "a magazzino"), tabmaga)
      edDamagaz.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426237656250, "Da Magazzino"), tabmaga)
      edAcodart.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426234218750, "a codice articolo"), tabartico, False)
      edDacodart.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426234687500, "Da codice articolo"), tabartico, True)
      edAconto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426235000000, "a conto"), tabanagrac)
      edDaconto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426235156250, "Da conto"), tabanagrac)
      edGruppo.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426232656250, "Gruppo"), tabgmer)
      edSottogr.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426232500000, "Sottogruppo"), tabsgme)
      edCodcfam.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426231250000, "Linea/famiglia articolo"), tabcfam, True)
      cbTipoStampa.NTSSetParam(oApp.Tr(Me, 129640284364511718, "TIPO STAMPA"))
      cbIncludi.NTSSetParam(oApp.Tr(Me, 129640901980788242, "Includi"))
      ckSalto.NTSSetParam(oMenu, oApp.Tr(Me, 128644905940198367, "Salto pagina per ogni articolo"), "S", "N")
      ckStampaFiltri.NTSSetParam(oMenu, oApp.Tr(Me, 129640929964742026, "Stampa i filtri impostati nel report"), "S", "N")
      cbTipodoc.NTSSetParam(oApp.Tr(Me, 128595426229375000, "Preleva movimenti"))
      ckSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128595426230312500, "Serie"), "S", "N")
      edSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128595426232187500, "Serie"), CLN__STD.SerieMaxLen, True)
      '--------------------------------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, True)
    '----------------------------------------------------------------------------------------------------------------
  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRMMGSCHE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      oMenu.ValCodiceDb(DittaCorrente, DittaCorrente, "ANADITAC", "S", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then
        oCleSche.bLottoNew = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ac_lotti2) = "S", True, False))
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      oCleSche.bModTCO = CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCO)
      If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtCRM) Or _
         CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And CLN__STD.bsModSupWCR) Then
        oCleSche.bModuloCRM = True
      Else
        oCleSche.bModuloCRM = False
      End If
      oCleSche.bModuloCRM = False
      oCleSche.bLogisticaEstesa = (CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtLEX) Or CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCP))
      '--------------------------------------------------------------------------------------------------------------
      edDacodart.Text = " " 'per SBS
      CaricaCombo()
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleSche.LeggiDatiDitta(DittaCorrente) Then
        Me.Close()
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Setta i parametri passati dal chiamante (arti, clie, ...)
      '--------------------------------------------------------------------------------------------------------------
      oCleSche.nScheFase = 0
      oCleSche.bScheGestDaCons = False
      If Not oCallParams Is Nothing Then
        If oCallParams.strParam <> "" Then
          oCleSche.bScheDagest = True
          Select Case Mid(oCallParams.strParam, 1, 4)
            Case "APRI"
              oCleSche.strScheCodart = Trim(Mid(oCallParams.strParam, 6, CLN__STD.CodartMaxLen))
              oCleSche.nScheMagaz = NTSCInt(Trim(Mid(oCallParams.strParam, CLN__STD.CodartMaxLen + 7, 4)))
              oCleSche.lScheConto = NTSCInt(Trim(Mid(oCallParams.strParam, CLN__STD.CodartMaxLen + 12, 9)))
              oCleSche.strScheOrdin = Trim(Mid(oCallParams.strParam, CLN__STD.CodartMaxLen + 22, 1))
              If Mid(oCallParams.strParam, CLN__STD.CodartMaxLen + 24, 4) <> "" Then
                oCleSche.nScheFase = NTSCInt(Mid(oCallParams.strParam, CLN__STD.CodartMaxLen + 24, 4))
              Else
                oCleSche.nScheFase = 0
              End If
            Case "APRD"
              oCleSche.bScheGestDaCons = True
              oCleSche.lScheConto = NTSCInt(Trim(Mid(oCallParams.strParam, 6, 9)))
              oCleSche.strScheCodart = Trim(Mid(oCallParams.strParam, 16, CLN__STD.CodartMaxLen))
              oCleSche.strScheTipork = Mid(oCallParams.strParam, CLN__STD.CodartMaxLen + 17, 1)
              oCleSche.strScheDatini = Trim(Mid(oCallParams.strParam, CLN__STD.CodartMaxLen + 19, 10))
              oCleSche.strScheDatfin = Trim(Mid(oCallParams.strParam, CLN__STD.CodartMaxLen + 30, 10))
              oCleSche.strScheOrdin = Trim(Mid(oCallParams.strParam, CLN__STD.CodartMaxLen + 41, 1))
              oCleSche.bScheFiltri = CBool(Mid(oCallParams.strParam, CLN__STD.CodartMaxLen + 43, 1))
          End Select
        Else
          oCleSche.bScheDagest = False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- CRM: se l'operatore non è stato codificato e non ha un ruolo non può operare
      '--------------------------------------------------------------------------------------------------------------
      If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And CLN__STD.bsModExtCRM) Or _
         CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And CLN__STD.bsModSupWCR) Then oCleSche.bModuloCRM = False
      If oCleSche.bModuloCRM Then
        oCleSche.bIsCRMUser = oMenu.IsCrmUser(DittaCorrente, oCleSche.bAmm, oCleSche.strAccvis, oCleSche.strAccmod, oCleSche.strRegvis, oCleSche.strRegmod)
        If oCleSche.bIsCRMUser Then
          oCleSche.lCodorgaOperat = oMenu.RitornaCodorgaDaOpnome(DittaCorrente, oCleSche.nCodcageoperat)
          If oCleSche.lCodorgaOperat = 0 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 127791222142500000, "Attenzione!" & vbCrLf & "L'operatore '|" & oApp.User.Nome & _
                 "|' (CRM) non è associato all'organizzazione della ditta corrente '|" & DittaCorrente & "|'." & vbCrLf & _
                 "Impossibile continuare."))
            Me.Close()
            Return
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleSche.strScheCodart = "" Then
        edDacodart.Text = NTSCStr(oCleSche.strScheCodart)
        edAcodart.Text = NTSCStr(oCleSche.strScheCodart)
      Else
        edDacodart.Text = "".PadLeft(CLN__STD.CodartMaxLen, " "c)
        edAcodart.Text = "".PadLeft(CLN__STD.CodartMaxLen, "z"c)
      End If
      If Not oCleSche.nScheMagaz = 0 Then
        edDamagaz.Text = NTSCStr(oCleSche.nScheMagaz)
        edAmagaz.Text = NTSCStr(oCleSche.nScheMagaz)
      Else
        edDamagaz.Text = "0"
        edAmagaz.Text = "9999"
      End If
      If Not oCleSche.lScheConto = 0 Then
        edDaconto.Text = NTSCStr(oCleSche.lScheConto)
        edAconto.Text = NTSCStr(oCleSche.lScheConto)
      Else
        edDaconto.Text = "0"
        edAconto.Text = "999999999"
      End If
      Select Case oCleSche.strScheOrdin
        Case "A" : cbTipoStampa.SelectedValue = "A"
        Case "C" : cbTipoStampa.SelectedValue = "B"
      End Select
      '--------------------------------------------------------------------------------------------------------------
      oCleSche.strTTStschea = "TTSTSCHEA"
      oCleSche.lIITTStschea = oMenu.GetTblInstId("TTSTSCHEA", False)
      '--------------------------------------------------------------------------------------------------------------
      If Not NTSCStr(oCleSche.dttTabanaz.Rows(0)!tb_dtulap) = "" Then
        edDatini.Text = NTSCStr(DateAdd("d", 1, NTSCStr(oCleSche.dttTabanaz.Rows(0)!tb_dtulap)))
        oCleSche.strDtulap = NTSCStr(oCleSche.dttTabanaz.Rows(0)!tb_dtulap)
      Else
        edDatini.Text = IntSetDate("01/01/1900")
        oCleSche.strDtulap = IntSetDate("01/01/1900")
      End If
      edDatini.Enabled = False
      edDatfin.Text = IntSetDate("31/12/2099")
      cbTipodoc.SelectedValue = "1"
      cbIncludi.SelectedValue = "A"
      edSerie.Text = ""
      edGruppo.Text = "0"
      edSottogr.Text = "0"
      edCodcfam.Text = ""
      lbXx_codcfam.Text = ""
      '--------------------------------------------------------------------------------------------------------------
      If (NTSCStr(cbTipodoc.SelectedValue) = "1") And _
         (NTSCStr(cbTipoStampa.SelectedValue) = "A") And _
         (ckStorico.Checked = True) Then
        GctlSetVisEnab(ckSaldiIniziali, False)
      Else
        ckSaldiIniziali.Checked = True
        ckSaldiIniziali.Enabled = False
      End If
      If oCleSche.bScheGestDaCons = True Then
        If oCleSche.lScheConto = 0 Then
          edDaconto.Text = "0"
          edAconto.Text = "999999999"
        Else
          edDaconto.Text = NTSCStr(oCleSche.lScheConto)
          edAconto.Text = NTSCStr(oCleSche.lScheConto)
        End If
        If oCleSche.strScheCodart = "" Then
          edDacodart.Text = "".PadLeft(CLN__STD.CodartMaxLen, " "c)
          edAcodart.Text = "".PadLeft(CLN__STD.CodartMaxLen, "z"c)
        Else
          edDacodart.Text = oCleSche.strScheCodart
          edAcodart.Text = oCleSche.strScheCodart
        End If
        If oCleSche.strScheTipork <> "§" Then cbTipodoc.SelectedValue = oCleSche.strScheTipork
        If oCleSche.strScheDatini <> "" Then
          ckStorico.Checked = True
          If Microsoft.VisualBasic.Right(oCleSche.strScheDatini, 3) = "/00" Then
            oCleSche.strScheDatini = Microsoft.VisualBasic.Left(oCleSche.strScheDatini, 6) & "1900"
          End If
          edDatini.Text = oCleSche.strScheDatini
        End If
        If oCleSche.strScheDatfin <> "" Then edDatfin.Text = oCleSche.strScheDatfin
        If oCleSche.bScheFiltri = False Then
          ReportSuGriglia()
          Return
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      lbStatus.Text = oApp.Tr(Me, 128735523680418000, "Pronto.")
      Me.Refresh()
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
      GctlApplicaDefaultValue()
      '--------------------------------------------------------------------------------------------------------------
      If (NTSCStr(cbTipoStampa.SelectedValue) = "A") And (ckStampaFiltri.Enabled = True) Then
        ckStampaFiltri.Checked = CBool(oMenu.GetSettingBus("BSMGSCHE", "RECENT", ".", "StampaFiltriInReport", "0", " ", "0"))
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Sub

  Public Overridable Sub FRMMGSCHE_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    Try
      SvuotaTmpTable()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      NTSCallStandardZoom()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      Stampa(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaGriglia_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaGriglia.ItemClick
    Try
      ReportSuGriglia()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
      lbStatus.Text = oApp.Tr(Me, 128498073391304728, "Pronto.")
      Me.Refresh()
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
#End Region

#Region "Eventi TextBox"
  Public Overridable Sub edGruppo_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edGruppo.Validated
    Dim strTmp As String = ""
    Try
      If oCleSche Is Nothing Then Return

      If Not oCleSche.edGruppo_Validated(NTSCInt(edGruppo.Text), strTmp) Then
        edGruppo.Text = NTSCStr(edGruppo.OldEditValue)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edSottogr_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSottogr.Validated
    Dim strTmp As String = ""
    Try
      If oCleSche Is Nothing Then Return

      If Not oCleSche.edSottogr_Validated(NTSCInt(edSottogr.Text), strTmp) Then
        edSottogr.Text = NTSCStr(edSottogr.OldEditValue)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edCodcfam_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodcfam.Validated
    Dim strTmp As String = ""
    Try
      If oCleSche Is Nothing Then Return

      If Not oCleSche.edCodcfam_Validated(NTSCStr(edCodcfam.Text), strTmp) Then
        edCodcfam.Text = NTSCStr(edCodcfam.OldEditValue)
      Else
        lbXx_codcfam.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edDacodart_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDacodart.Validated
    Try
      If edDacodart.Text <> "".PadLeft(CLN__STD.CodartMaxLen) Then
        edDacodart.Text = UCase(edDacodart.Text)
        edAcodart.Text = edDacodart.Text
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edDaconto_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDaconto.Validated
    Try
      If NTSCInt(edDaconto.Text) <> 0 Then edAconto.Text = edDaconto.Text

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edAcodart_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAcodart.Validated
    Try
      If Not edAcodart.Text = "".PadLeft(CLN__STD.CodartMaxLen, "z"c) Then edAcodart.Text = UCase(edAcodart.Text)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edSerie_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSerie.Validated
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edSerie.Text, False)
      If strTmp <> edSerie.Text Then edSerie.Text = strTmp

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi CheckBox"
  Public Overridable Sub ckSerie_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSerie.CheckedChanged
    Try
      If ckSerie.Checked = True Then
        GctlSetVisEnab(edSerie, False)
        edSerie.Text = " "
      Else
        edSerie.Enabled = False
        edSerie.Text = ""
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckStorico_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckStorico.CheckedChanged
    Try
      If ckStorico.Checked = False Then
        If Not NTSCStr(oCleSche.dttTabanaz.Rows(0)!tb_dtulap) = "" Then
          edDatini.Text = NTSCStr(DateAdd("d", 1, NTSCDate(oCleSche.dttTabanaz.Rows(0)!tb_dtulap)))
        Else
          edDatini.Text = IntSetDate("01/01/1900")
        End If
        edDatini.Enabled = False
        ckSaldiIniziali.Checked = True
        ckSaldiIniziali.Enabled = False
      Else
        If (NTSCStr(cbTipodoc.SelectedValue) = "1") And (NTSCStr(cbTipoStampa.SelectedValue) = "A") Then
          GctlSetVisEnab(ckSaldiIniziali, False)
        Else
          ckSaldiIniziali.Checked = True
          ckSaldiIniziali.Enabled = False
        End If
        GctlSetVisEnab(edDatini, False)
        If bOnLoadingFromSettings = False Then edDatini.Text = IntSetDate("01/01/1900")
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi ComboBox"
  Public Overridable Sub cbTipodoc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTipodoc.SelectedIndexChanged
    Try
      Select Case NTSCStr(cbTipodoc.SelectedValue)
        Case "1"
          If (NTSCStr(cbTipoStampa.SelectedValue) = "A") And (ckStorico.Checked = True) Then
            GctlSetVisEnab(ckSaldiIniziali, False)
          Else
            ckSaldiIniziali.Checked = True
            ckSaldiIniziali.Enabled = False
          End If
          ckSerie.Checked = False
          ckSerie.Enabled = False
          If NTSCStr(cbTipoStampa.SelectedValue) = "A" Then
            cbIncludi.SelectedValue = "A"
            GctlSetVisEnab(cbIncludi, False)
          Else
            cbIncludi.SelectedValue = "A"
            cbIncludi.Enabled = False
          End If
        Case Else
          If (NTSCStr(cbTipoStampa.SelectedValue) = "A") And (ckStorico.Checked = True) Then
            GctlSetVisEnab(ckSaldiIniziali, False)
          Else
            ckSaldiIniziali.Checked = True
            ckSaldiIniziali.Enabled = False
          End If
          GctlSetVisEnab(ckSerie, False)
          If NTSCStr(cbTipoStampa.SelectedValue) = "A" Then
            cbIncludi.SelectedValue = "A"
            GctlSetVisEnab(cbIncludi, False)
          Else
            cbIncludi.SelectedValue = "A"
            cbIncludi.Enabled = False
          End If
      End Select
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cbTipoStampa_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTipoStampa.SelectedIndexChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      ScelteOptionButton()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Azioni su Tabelle Temporanee"
  Public Overridable Function SaldoStorico() As Boolean
    Dim bPrelevaSolo As Boolean = False
    Dim strError As String = ""

    Try
      If NTSCStr(cbTipodoc.SelectedValue) <> "1" Then bPrelevaSolo = True

      lbStatus.Text = oApp.Tr(Me, 129080485546996984, "Determinazione saldo storico articoli movimentati ...")
      Me.Refresh()

      If Not oCleSche.SaldoStorico(edDatini.Text, edDatfin.Text, edDamagaz.Text, _
                         edAmagaz.Text, edDacodart.Text, edAcodart.Text, edDaconto.Text, _
                         edAconto.Text, "0", "9999", ckStorico.Checked, False, False, _
                         "", "".PadLeft(9, "9"c), _
                         False, False, "0", "999999999", False, False, "".PadLeft(30), "".PadLeft(30, "z"c), _
                         bPrelevaSolo, cbTipodoc.SelectedValue, ckSerie.Checked, IIf(ckSerie.Checked = True, edSerie.Text, "").ToString, _
                         "0", edGruppo.Text, edSottogr.Text, "0", "999", edCodcfam.Text, False, _
                         "0", "0", ckSaldiIniziali.Checked, strError, "", "", "", "", "", "", "A", 0, "A", 0) Then
        If strError <> "" Then oApp.MsgBoxErr(strError)
        Return False
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function SaldoStoricoNonMov() As Boolean
    Dim bPrelevaSolo As Boolean = False
    Dim bEsist As Boolean = CBool(IIf(NTSCStr(cbIncludi.SelectedValue) = "C", True, False))
    Dim strError As String = ""

    Try
      If NTSCStr(cbTipodoc.SelectedValue) <> "1" Then bPrelevaSolo = True

      lbStatus.Text = oApp.Tr(Me, 129080485680746128, "Determinazione saldo storico articoli non movimentati ...")
      Me.Refresh()

      If Not oCleSche.SaldoStoricoNonMov(edDatini.Text, edDatfin.Text, edDamagaz.Text, _
                   edAmagaz.Text, edDacodart.Text, edAcodart.Text, edDaconto.Text, _
                   edAconto.Text, "0", "9999", ckStorico.Checked, False, False, _
                   "", "".PadLeft(9, "9"c), _
                   False, False, "0", "999999999", False, False, "".PadLeft(30), "".PadLeft(30, "z"c), _
                   bPrelevaSolo, cbTipodoc.SelectedValue, ckSerie.Checked, IIf(ckSerie.Checked = True, edSerie.Text, "").ToString, _
                   "0", edGruppo.Text, edSottogr.Text, "0", "999", edCodcfam.Text, False, _
                   "0", "0", ckSaldiIniziali.Checked, bEsist, strError, "", "", "", "", "", "", "A", 0, "A", 0) Then
        If strError <> "" Then oApp.MsgBoxErr(strError)
        Return False
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function SvuotaTmpTable() As Boolean
    Try
      oMenu.ResetTblInstId("TTSTSCHEA", False, oCleSche.lIITTStschea)
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
#End Region

  Public Overridable Sub CaricaCombo()
    Dim dttTipodoc As New DataTable()
    Dim dttTipoStampa As New DataTable()
    Dim dttIncludi As New DataTable()

    Try
      '--------------------------------------------------------------------------------------------------------------
      dttTipodoc.Columns.Add("cod", GetType(String))
      dttTipodoc.Columns.Add("val", GetType(String))
      dttTipodoc.Rows.Add(New Object() {"1", "Tutti"})
      dttTipodoc.Rows.Add(New Object() {"A", "Fatture Immediate emesse"})
      dttTipodoc.Rows.Add(New Object() {"B", "D.D.T. emessi"})
      dttTipodoc.Rows.Add(New Object() {"C", "Corrispettivi emessi"})
      dttTipodoc.Rows.Add(New Object() {"E", "Note di Addebito emesse"})
      dttTipodoc.Rows.Add(New Object() {"F", "Ricevute Fiscali emesse"})
      dttTipodoc.Rows.Add(New Object() {"I", "Riemissione Ricevute Fiscali"})
      dttTipodoc.Rows.Add(New Object() {"J", "Note di Accredito ricevute"})
      dttTipodoc.Rows.Add(New Object() {"L", "Fatture Immediate ricevute"})
      dttTipodoc.Rows.Add(New Object() {"M", "D.D.T. ricevuti"})
      dttTipodoc.Rows.Add(New Object() {"N", "Note di Accredito emesse"})
      dttTipodoc.Rows.Add(New Object() {"S", "Fatture/Ricevute Fiscali emesse"})
      dttTipodoc.Rows.Add(New Object() {"Z", "Bolle di Movimentazione Interna"})
      dttTipodoc.AcceptChanges()
      cbTipodoc.DataSource = dttTipodoc
      cbTipodoc.ValueMember = "cod"
      cbTipodoc.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
      dttTipoStampa.Columns.Add("cod", GetType(String))
      dttTipoStampa.Columns.Add("val", GetType(String))
      dttTipoStampa.Rows.Add(New Object() {"A", "Per Articolo"})
      dttTipoStampa.Rows.Add(New Object() {"B", "Per Conto"})
      dttTipoStampa.AcceptChanges()
      cbTipoStampa.DataSource = dttTipoStampa
      cbTipoStampa.ValueMember = "cod"
      cbTipoStampa.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
      dttIncludi.Columns.Add("cod", GetType(String))
      dttIncludi.Columns.Add("val", GetType(String))
      dttIncludi.Rows.Add(New Object() {"A", "Solo articoli movimentati"})
      dttIncludi.Rows.Add(New Object() {"B", "Tutti gli articoli"})
      dttIncludi.Rows.Add(New Object() {"C", "Articoli in esistenza alla data inizio"})
      dttIncludi.AcceptChanges()
      cbIncludi.DataSource = dttIncludi
      cbIncludi.ValueMember = "cod"
      cbIncludi.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Function CheckIntervalli() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCDate(edDatini.Text) > NTSCDate(edDatfin.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128595499747689918, "La data iniziale non può essere superiore a quella finale."))
        edDatfin.Text = IntSetDate("31/12/2099")
        Return False
      End If
      If NTSCInt(edDamagaz.Text) > NTSCInt(edAmagaz.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128595499815189918, "Il magazzino di partenza non può essere superiore a quello di arrivo."))
        edDamagaz.Text = "0"
        edAmagaz.Text = "9999"
        Return False
      End If
      If UCase(edDacodart.Text) > UCase(edAcodart.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128595499598002418, "L'articolo di partenza non può essere superiore a quello di arrivo."))
        edDacodart.Text = "".PadLeft(CLN__STD.CodartMaxLen)
        edAcodart.Text = "".PadLeft(CLN__STD.CodartMaxLen, "z"c)
        Return False
      End If
      If NTSCInt(edDaconto.Text) > NTSCInt(edAconto.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128595499709721168, "Il conto di partenza non può essere superiore a quello di arrivo."))
        edDaconto.Text = "0"
        edAconto.Text = "999999999"
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function ComponiFormula(ByVal strTab As String) As String
    Dim strC As String = ""
    Dim strTipork As String = ""
    Dim strAggwhereCRM As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      ComponiFormula = ""
      '--------------------------------------------------------------------------------------------------------------
      '--- Se crm, allora mette anche solo i clienti che sono nel potere di visibilità dell'utente...
      '--------------------------------------------------------------------------------------------------------------
      If (oCleSche.bModuloCRM = True) And (oCleSche.bIsCRMUser = True) Then
        strAggwhereCRM = " And ("
        If oCleSche.strAccvis <> "T" Then
          strAggwhereCRM += "({anagra.an_tipo} = 'C' And {leads.le_coddest} = 0"
          Select Case oCleSche.strAccvis
            Case "P" : strAggwhereCRM += " And {leads.le_opinc} = " & oCleSche.lCodorgaOperat
            Case "C" : strAggwhereCRM += " And {leads.le_opinc} In [" & oCleSche.strRegvis & "]"
          End Select
          strAggwhereCRM += ")"
        Else '--- Tutti i clienti
          strAggwhereCRM += " {anagra.an_tipo} = 'C'"
        End If
        If oCleSche.bAmm = False Then
          strAggwhereCRM += " And {anagra.an_tipo} <> 'F'"
        Else
          strAggwhereCRM += " Or {anagra.an_tipo} <> 'C'"
        End If
        strAggwhereCRM += ")"
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(cbIncludi.SelectedValue) = "A" Then
        strC = "{" & strTab & ".codditt} = '" & DittaCorrente & "'"
        If (NTSCDate(edDatini.Text) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
           (NTSCDate(edDatfin.Text) <> NTSCDate(IntSetDate("31/12/2099"))) Then
          strC += " And {" & strTab & ".km_aammgg} In " & ConvDataRpt(edDatini.Text) & " To " & ConvDataRpt(edDatfin.Text)
        End If
        If (NTSCInt(edDamagaz.Text) <> 0) Or (NTSCInt(edAmagaz.Text) <> 9999) Then
          strC += " And {" & strTab & ".km_magaz} In " & NTSCInt(edDamagaz.Text) & " To " & NTSCInt(edAmagaz.Text)
        End If
        If (edDacodart.Text <> "".PadLeft(18)) Or (edAcodart.Text <> "".PadLeft(18, "z"c)) Then
          strC += " And {" & strTab & ".km_codart} In '" & edDacodart.Text & "' To '" & edAcodart.Text & "'"
        End If
        If (NTSCInt(edDaconto.Text) <> 0) Or (NTSCInt(edAconto.Text) <> 999999999) Then
          strC += " And {" & strTab & ".km_conto} In " & NTSCInt(edDaconto.Text) & " To " & NTSCInt(edAconto.Text)
        End If
        If NTSCStr(cbTipodoc.SelectedValue) <> "1" Then
          strTipork = UCase(NTSCStr(cbTipodoc.SelectedValue))
          strC += " and {" & strTab & ".km_tipork} = '" & strTipork & "'"
          If ckSerie.Checked = True Then
            If edSerie.Text.Length > 0 Then strC += " and {" & strTab & ".km_serie} = '" & edSerie.Text & "'"
          End If
        End If
        strC += " and "
      End If '--- If NTSCStr(cbIncludi.SelectedValue) = "A" Then
      '--------------------------------------------------------------------------------------------------------------
      strC += " {ARTICO.ar_stasche} = 'S'"
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edGruppo.Text) <> 0 Then strC += " and {ARTICO.ar_gruppo} = " & NTSCInt(edGruppo.Text)
      If NTSCInt(edSottogr.Text) <> 0 Then strC += " and {ARTICO.ar_sotgru} = " & NTSCInt(edSottogr.Text)
      If edCodcfam.Text.Trim <> "" Then strC += " And {artico.ar_famprod} = '" & edCodcfam.Text & "'"
      '--------------------------------------------------------------------------------------------------------------
      '--- Aggiunge eventuali condizioni CRM
      '--------------------------------------------------------------------------------------------------------------
      strC += strAggwhereCRM

      If strTab = "KEYMAG" Then
        If NTSCStr(cbTipoStampa.SelectedValue) = "A" Then strC += " and {TTSTSCHEA.instid} = " & oCleSche.lIITTStschea
      End If

      '--------------------------------------------------------------------------------------------------------------
      ComponiFormula = strC
      '--------------------------------------------------------------------------------------------------------------
      ComponiFormula = strC
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      ComponiFormula = ""
      '--------------------------------------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Function

  Public Overloads Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    '---------------------------------
    'questa funzione riceve gli eventi dall'ENTITY: rimappata rispetto a quella standard di FRM__CHILD
    'prima eseguo quella standard
    Dim strTmp() As String
    Dim i As Integer = 0

    If Not IsMyThrowRemoteEvent() Then Return 'il messaggio non è per questa form ...
    MyBase.GestisciEventiEntity(sender, e)

    Try
      '---------------------------------
      'adesso gestisco le specifiche
      'devo inserire delle funzioni qui sotto per fare in modo che al variare di dati nell'entity delle informazioni 
      'legate all'interfaccia grafica (ui) vengano allineate a quanto richiesto dall'entity

      If e.TipoEvento.Length < 10 Then Return
      strTmp = e.TipoEvento.Split(CType("|", Char))
      For i = 0 To strTmp.Length - 1
        Select Case strTmp(i).Substring(0, 10)
          Case "STATUSBAR:"
            lbStatus.Text = e.Message
            lbStatus.Refresh() 'Me.Refresh ridisegna tutto il programma. Le prestazioni sono decisamente peggiori.
        End Select
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function ReplaceSQLApice(ByVal strIn As String) As String
    Try
      Dim i As Integer
      i = InStr(1, strIn, "'")
      While i > 0
        strIn = Microsoft.VisualBasic.Left(strIn, i - 1) + "' + ""'"" + '" + Mid(strIn, i + 1)
        i = i + 11
        i = InStr(i, strIn, "'")
      End While

      ReplaceSQLApice = strIn

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return ""
    End Try
  End Function

  Public Overridable Sub ReportSuGriglia()
    Dim oParam As New CLE__CLDP
    Dim frmGrsc As FRMMGGRSC = Nothing
    Dim frmGrlo As FRMMGGRLO = Nothing
    Dim frmGrma As FRMMGGRMA = Nothing
    Dim frmGrnp As FRMMGGRNP = Nothing
    Dim oPar As New CLE__PATB
    Dim strParam As String = ""
    Dim strSTMSerie As String = ""
    Dim strSTMTipork As String = ""
    Dim strTipoMatr As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      If (NTSCStr(cbTipoStampa.SelectedValue) = "A") And _
         (NTSCStr(cbIncludi.SelectedValue) = "C" Or NTSCStr(cbIncludi.SelectedValue) = "B") Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128644786523647457, "Stampa su gliglia non diponibile con le scelte che includono in stampa 'Tutti gli articoli' o 'Articoli in esistenza alla data inizio'."))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.WaitCursor
      '--------------------------------------------------------------------------------------------------------------
      If Not SvuotaTmpTable() Then Return
      If Not CheckIntervalli() Then Return
      '--------------------------------------------------------------------------------------------------------------
      lbStatus.Text = oApp.Tr(Me, 129080484846821870, "Selezione dati in corso...")
      Me.Refresh()
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.WaitCursor
      '--------------------------------------------------------------------------------------------------------------
      If ckStorico.Checked = True Then oCleSche.bScarStorico = True Else oCleSche.bScarStorico = False
      oCleSche.strScarDatini = edDatini.Text
      oCleSche.strScarDatfin = edDatfin.Text
      oCleSche.nScarDamagaz = NTSCInt(edDamagaz.Text)
      oCleSche.nScarAmagaz = NTSCInt(edAmagaz.Text)
      oCleSche.strScarDacodart = edDacodart.Text
      oCleSche.strScarAcodart = edAcodart.Text
      oCleSche.lScarDaconto = NTSCInt(edDaconto.Text)
      oCleSche.lScarAconto = NTSCInt(edAconto.Text)
      oCleSche.nScarCodmarcini = 0
      oCleSche.nScarCodmarcfin = 999
      oCleSche.nScarFaseini = 0
      oCleSche.nScarFasefin = 9999
      oCleSche.lScarDacomme = 0
      oCleSche.lScarAcomme = 999999999
      If NTSCStr(cbTipodoc.SelectedValue) <> "1" Then
        oCleSche.strScarTipodoc = UCase(NTSCStr(cbTipodoc.SelectedValue))
      Else
        oCleSche.strScarTipodoc = ""
      End If
      oCleSche.nScarCausale = 0
      oCleSche.nScarGruppo = NTSCInt(edGruppo.Text)
      oCleSche.nScarSottogr = NTSCInt(edSottogr.Text)
      oCleSche.strScarSerie = IIf(ckSerie.Checked = True, edSerie.Text, "").ToString
      Select Case NTSCStr(cbTipoStampa.SelectedValue)
        Case "A" : oCleSche.strScarOrdin = "A"
        Case "B" : oCleSche.strScarOrdin = "C"
      End Select
      oCleSche.bScarDaveboll = False
      If (ckStorico.Checked = True) And _
         (NTSCStr(cbTipoStampa.SelectedValue) = "A") And _
         (ckSaldiIniziali.Checked = False) Then
        oCleSche.bGrscSaldiIniziali = False
      Else
        oCleSche.bGrscSaldiIniziali = True
      End If
      oCleSche.strGrscCodcfam = Trim(edCodcfam.Text)
      oCleSche.nGrscAnnotco = 0
      oCleSche.nGrscCodstag = 0
      oCleSche.strScarDalotto = ""
      oCleSche.strScarAlotto = "".PadLeft(9, "9"c)
      oCleSche.strAltriFiltri = ""
      frmGrsc = CType(NTSNewFormModal("FRMMGGRSC"), FRMMGGRSC)
      frmGrsc.Init(oMenu, oParam, DittaCorrente)
      frmGrsc.InitEntity(oCleSche)
      frmGrsc.ShowDialog()
      '--------------------------------------------------------------------------------------------------------------
      If oCleSche.bScheGestDaCons = True Then
        Me.Visible = True
        Me.Close()
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      '--------------------------------------------------------------------------------------------------------------
      If Not frmGrsc Is Nothing Then frmGrsc.Dispose()
      frmGrsc = Nothing
      '--------------------------------------------------------------------------------------------------------------
      If Not frmGrlo Is Nothing Then frmGrlo.Dispose()
      frmGrlo = Nothing
      '--------------------------------------------------------------------------------------------------------------
      If Not frmGrma Is Nothing Then frmGrma.Dispose()
      frmGrma = Nothing
      '--------------------------------------------------------------------------------------------------------------
      If Not frmGrnp Is Nothing Then frmGrnp.Dispose()
      frmGrnp = Nothing
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.Default
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ScelteOptionButton()
    Try
      '--------------------------------------------------------------------------------------------------------------
      If (NTSCStr(cbTipodoc.SelectedValue) = "1") And (NTSCStr(cbTipoStampa.SelectedValue) = "A") And _
         (ckStorico.Checked = True) Then
        GctlSetVisEnab(ckSaldiIniziali, False)
      Else
        ckSaldiIniziali.Checked = True
        ckSaldiIniziali.Enabled = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(cbTipoStampa.SelectedValue) = "A" Then
        GctlSetVisEnab(ckSalto, False)
        cbIncludi.SelectedValue = "A"
        GctlSetVisEnab(cbIncludi, False)
      Else
        ckSalto.Enabled = False
        cbIncludi.SelectedValue = "A"
        cbIncludi.Enabled = False
      End If
      GctlSetVisEnab(ckStorico, False)
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCStr(cbTipoStampa.SelectedValue)
        Case "A"
          GctlSetVisEnab(ckStampaFiltri, False)
        Case Else
          ckStampaFiltri.Checked = False
          ckStampaFiltri.Enabled = False
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer
    Dim strNomRpt As String = ""
    Dim strKey As String = ""
    Dim strKey1 As String = ""
    Dim strData As String = ""
    Dim strDataIn As String = ""
    Dim strDataFin As String = ""
    Dim sectionCode As Integer
    Dim nVisible As Integer
    Dim nNewPageBefore As Integer
    Dim nNewPageAfter As Integer
    Dim nKeepTogether As Integer
    Dim nSuppressBlankSection As Integer
    Dim nResetPageNAfter As Integer
    Dim nPrintAtBottomOfPage As Integer
    Dim lBackgroudColor As Integer
    Dim nUnderlaySection As Integer
    Dim nShowArea As Integer
    Dim nFreeFormPlacement As Integer
    Dim strError As String = ""
    Dim strTmp As String = ""
    Dim strLottoAlfanumerico As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If ckStampaFiltri.Enabled = True Then
        oMenu.SaveSettingBusDitt(DittaCorrente, "BSMGSCHE", "RECENT", ".", "StampaFiltriInReport", IIf(ckStampaFiltri.Checked = True, "-1", "0").ToString, " ", ".N.", ".N.", ".N.")
      End If
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      If Not SvuotaTmpTable() Then Return
      '--------------------------------------------------------------------------------------------------------------
      If Not CheckIntervalli() Then Return
      '--------------------------------------------------------------------------------------------------------------
      oCleSche.strScarDalotto = ""
      oCleSche.strScarAlotto = "".PadLeft(9, "9"c)
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(cbTipoStampa.SelectedValue) = "A" Then
        If NTSCStr(cbIncludi.SelectedValue) = "A" Then
          If Not SaldoStorico() Then Return
        Else
          If Not SaldoStoricoNonMov() Then Return
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      lbStatus.Text = oApp.Tr(Me, 129080484660564718, "Stampa in corso...")
      Me.Refresh()
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCStr(cbTipoStampa.SelectedValue)
        Case "A"
          If opTipoStampa0.Checked = True Then
            If NTSCStr(cbIncludi.SelectedValue) = "A" Then
              strNomRpt = "BSMGSCH11.RPT" : strKey1 = "Reports11"
            Else
              strNomRpt = "BSMGSCH13.RPT" : strKey1 = "Reports13"
            End If
          Else
            If NTSCStr(cbIncludi.SelectedValue) = "A" Then
              strNomRpt = "BSMGSCHE.RPT" : strKey1 = "Reports1"
            Else
              strNomRpt = "BSMGSCH14.RPT" : strKey1 = "Reports14"
            End If
          End If
        Case "B"
          If opTipoStampa0.Checked = True Then
            strNomRpt = "BSMGSCH12.RPT" : strKey1 = "Reports12"
          Else
            strNomRpt = "BSMGSCH1.RPT" : strKey1 = "Reports2"
          End If
      End Select
      '--------------------------------------------------------------------------------------------------------------
      strData = "'Dal " & edDatini.Text & " al " & edDatfin.Text & "'"
      strDataIn = "'" & (edDatini.Text) & "'"
      strDataFin = "'" & edDatfin.Text & "'"
      '--------------------------------------------------------------------------------------------------------------
      '--- Si comporta diversamente se per articolo oppure no...
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(cbTipoStampa.SelectedValue) <> "A" Then
        nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGSCHE", strKey1, " ", 0, nDestin, strNomRpt, False, "Stampa/Visualizzazione schede articoli", False)
      Else ' per articolo
        nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGSCHE", strKey1, " ", 0, nDestin, strNomRpt, False, "Stampa/Visualizzazione schede articoli", False)
      End If
Salta:
      strCrpe = ComponiFormula("KEYMAG")
      '--------------------------------------------------------------------------------------------------------------
      If nPjob Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      '--- Lancio tutti gli eventuali reports (gestisce già il multireport)
      '--------------------------------------------------------------------------------------------------------------
      For i = 1 To UBound(CType(nPjob, Array), 2)
        '------------------------------------------------------------------------------------------------------------
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        '------------------------------------------------------------------------------------------------------------
        If i = 1 Then
          '----------------------------------------------------------------------------------------------------------
          '--- Il primo report (quello standard)
          '----------------------------------------------------------------------------------------------------------
          If (NTSCStr(cbTipoStampa.SelectedValue) = "A") And (ckSalto.Checked = True) Then
            sectionCode = oMenu.PE_GROUPHEADER
            nRis = oMenu.PEGetSectionFormat(NTSCInt(CType(nPjob, Array).GetValue(0, i)), sectionCode, nVisible, nNewPageBefore, nNewPageAfter, nKeepTogether, nSuppressBlankSection, nResetPageNAfter, nPrintAtBottomOfPage, lBackgroudColor, nUnderlaySection, nShowArea, nFreeFormPlacement)
            nNewPageBefore = 1
            nRis = oMenu.PESetSectionFormat(NTSCInt(CType(nPjob, Array).GetValue(0, i)), sectionCode, nVisible, nNewPageBefore, nNewPageAfter, nKeepTogether, nSuppressBlankSection, nResetPageNAfter, nPrintAtBottomOfPage, lBackgroudColor, nUnderlaySection, nShowArea, nFreeFormPlacement)
          End If
        End If
        '------------------------------------------------------------------------------------------------------------
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DATA", strData)
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DATAIN", strDataIn)
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DATAFIN", strDataFin)
        '------------------------------------------------------------------------------------------------------------
        '--- Passa alcune formule per i filtri sui report
        '------------------------------------------------------------------------------------------------------------
        If NTSCStr(cbIncludi.SelectedValue) = "C" Then
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "FILTDATAIN", ConvDataRpt(edDatini.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "FILTDATAFIN", ConvDataRpt(edDatfin.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "FILTDACONTO", edDaconto.Text)
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "FILTACONTO", edAconto.Text)
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "FILTTIPO", NTSCStr(IIf(NTSCStr(cbTipodoc.SelectedValue) <> "1", ConvStrRpt(UCase(NTSCStr(cbTipodoc.SelectedValue))), "'ALL'")))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "FILTSERIE", NTSCStr(IIf(NTSCStr(cbTipodoc.SelectedValue) <> "1" And ckSerie.Checked = True And Len(edSerie.Text) > 0, Trim(edSerie.Text), "'ALL'")))
        End If
        '------------------------------------------------------------------------------------------------------------
        If ckStampaFiltri.Checked = True Then
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "OMETTISEZIONEFILTRI", ConvStrRpt("N"))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "CKSTORICO", ConvStrRpt(IIf(ckStorico.Checked = True, "Sì", "No").ToString))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDDATINI", ConvStrRpt(edDatini.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDDATFIN", ConvStrRpt(edDatfin.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDDAMAGAZ", ConvStrRpt(edDamagaz.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDAMAGAZ", ConvStrRpt(edAmagaz.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDDACODART", ConvStrRpt(edDacodart.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDACODART", ConvStrRpt(edAcodart.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDDACONTO", ConvStrRpt(edDaconto.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDACONTO", ConvStrRpt(edAconto.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDGRUPPO", ConvStrRpt(edGruppo.Text))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDSOTTOGR", ConvStrRpt(edSottogr.Text))
          strTmp = ""
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDCAUSALE", ConvStrRpt(strTmp))
          strTmp = ""
          If edCodcfam.Text.Trim <> "" Then
            strTmp = edCodcfam.Text & IIf(lbXx_codcfam.Text.Trim <> "", " - ", "").ToString & lbXx_codcfam.Text.Trim
          End If
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDCODCFAM", ConvStrRpt(strTmp))
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "CKSELANNOSTAG", ConvStrRpt("No"))
          strTmp = ""
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDANNOTCO", ConvStrRpt(strTmp))
          strTmp = ""
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "EDCODSTAG", ConvStrRpt(strTmp))
          strTmp = ""
          If NTSCStr(cbTipodoc.SelectedValue) = "A" Then strTmp = "Da tutti i documenti di magazzino"
          If NTSCStr(cbTipodoc.SelectedValue) <> "1" Then
            strTmp = "Solo da " & cbTipodoc.SelectedItem.ToString
            If ckSerie.Checked = True Then strTmp += " (serie: " & IIf(edSerie.Text.Trim <> "", edSerie.Text, "nulla").ToString & ")"
          End If
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "OPPRELEVA", ConvStrRpt(strTmp))
          If NTSCStr(cbIncludi.SelectedValue) = "A" Then strTmp = "Solo articoli movimentati"
          If NTSCStr(cbIncludi.SelectedValue) = "B" Then strTmp = "Tutti gli articoli"
          If NTSCStr(cbIncludi.SelectedValue) = "C" Then strTmp = "Articoli in esistenza alla data inizio"
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "OPINCLUDI", ConvStrRpt(strTmp))
        Else
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "OMETTISEZIONEFILTRI", ConvStrRpt("S"))
        End If
        strLottoAlfanumerico = "'S'"
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "STPLOTTI", strLottoAlfanumerico)
        '------------------------------------------------------------------------------------------------------------
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next
      '--------------------------------------------------------------------------------------------------------------
      lbStatus.Text = oApp.Tr(Me, 128735523639234000, "Pronto.")
      Me.Refresh()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
      lbStatus.Text = oApp.Tr(Me, 128735523664038000, "Pronto.")
      Me.Refresh()
    End Try
  End Sub

End Class