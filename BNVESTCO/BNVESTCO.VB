Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVESTCO
  Public oCleStco As CLEVESTCO
  Public oCallParams As CLE__CLDP

  Public dsStco As DataSet
  Public dcStco As BindingSource = New BindingSource

  Public lInstID As Integer = -1

  Public Const bsDbAzienda As Boolean = False

#Region "Moduli"
  Private Moduli_P As Integer = bsModVE + bsModMG
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

  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbSep2 As NTSInformatica.NTSLabel
  Public WithEvents lbSep1 As NTSInformatica.NTSLabel
  Public WithEvents lbAlvett As NTSInformatica.NTSLabel
  Public WithEvents lbAlndo As NTSInformatica.NTSLabel
  Public WithEvents lbAlconto As NTSInformatica.NTSLabel
  Public WithEvents lbAlldat As NTSInformatica.NTSLabel
  Public WithEvents lbDalvet As NTSInformatica.NTSLabel
  Public WithEvents lbDalndo As NTSInformatica.NTSLabel
  Public WithEvents lbDaldat As NTSInformatica.NTSLabel
  Public WithEvents lbDalConto As NTSInformatica.NTSLabel
  Public WithEvents lbAnno As NTSInformatica.NTSLabel
  Public WithEvents edAlseri As NTSInformatica.NTSTextBoxStr
  Public WithEvents edDalser As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAlldat As NTSInformatica.NTSTextBoxData
  Public WithEvents edDaldat As NTSInformatica.NTSTextBoxData
  Public WithEvents edAlvett As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAldocu As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDaldoc As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAlcont As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDalcon As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAnno As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDalvet As NTSInformatica.NTSTextBoxNum
  Public WithEvents fmTipdoc As NTSInformatica.NTSGroupBox
  Public WithEvents opImpcli As NTSInformatica.NTSRadioButton
  Public WithEvents opDdteme As NTSInformatica.NTSRadioButton
  Public WithEvents opFatimm As NTSInformatica.NTSRadioButton
  Public WithEvents fmStapes As NTSInformatica.NTSGroupBox
  Public WithEvents ckStapes As NTSInformatica.NTSCheckBox
  Public WithEvents pnControlli As NTSInformatica.NTSPanel
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNVESTCO", "BEVESTCO", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128589409211093750, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleStco = CType(oTmp, CLEVESTCO)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNVESTCO", strRemoteServer, strRemotePort)
    AddHandler oCleStco.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleStco.Init(oApp, NTSScript, oMenu.oCleComm, "TABANAZ", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMVESTCO))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.lbAnno = New NTSInformatica.NTSLabel
    Me.lbDalConto = New NTSInformatica.NTSLabel
    Me.lbDaldat = New NTSInformatica.NTSLabel
    Me.lbDalndo = New NTSInformatica.NTSLabel
    Me.lbDalvet = New NTSInformatica.NTSLabel
    Me.lbAlldat = New NTSInformatica.NTSLabel
    Me.lbAlconto = New NTSInformatica.NTSLabel
    Me.lbAlndo = New NTSInformatica.NTSLabel
    Me.lbAlvett = New NTSInformatica.NTSLabel
    Me.lbSep1 = New NTSInformatica.NTSLabel
    Me.lbSep2 = New NTSInformatica.NTSLabel
    Me.edDalvet = New NTSInformatica.NTSTextBoxNum
    Me.edAnno = New NTSInformatica.NTSTextBoxNum
    Me.edDalcon = New NTSInformatica.NTSTextBoxNum
    Me.edAlcont = New NTSInformatica.NTSTextBoxNum
    Me.edDaldoc = New NTSInformatica.NTSTextBoxNum
    Me.edAldocu = New NTSInformatica.NTSTextBoxNum
    Me.edAlvett = New NTSInformatica.NTSTextBoxNum
    Me.edDaldat = New NTSInformatica.NTSTextBoxData
    Me.edAlldat = New NTSInformatica.NTSTextBoxData
    Me.edDalser = New NTSInformatica.NTSTextBoxStr
    Me.edAlseri = New NTSInformatica.NTSTextBoxStr
    Me.fmTipdoc = New NTSInformatica.NTSGroupBox
    Me.opImpcli = New NTSInformatica.NTSRadioButton
    Me.opDdteme = New NTSInformatica.NTSRadioButton
    Me.opFatimm = New NTSInformatica.NTSRadioButton
    Me.fmStapes = New NTSInformatica.NTSGroupBox
    Me.ckStapes = New NTSInformatica.NTSCheckBox
    Me.pnControlli = New NTSInformatica.NTSPanel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDalvet.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDalcon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAlcont.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDaldoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAldocu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAlvett.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDaldat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAlldat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDalser.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAlseri.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmTipdoc, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmTipdoc.SuspendLayout()
    CType(Me.opImpcli.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opDdteme.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opFatimm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmStapes, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmStapes.SuspendLayout()
    CType(Me.ckStapes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnControlli, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnControlli.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti, Me.tlbImpostaStampante})
    Me.NtsBarManager1.MaxItemId = 7
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.GlyphPath = ""
    Me.tlbStampa.Id = 0
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "StampaVideo"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.GlyphPath = ""
    Me.tlbStampaVideo.Id = 1
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
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
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 5
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.GlyphPath = ""
    Me.tlbImpostaStampante.Id = 6
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
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
    'lbAnno
    '
    Me.lbAnno.AutoSize = True
    Me.lbAnno.BackColor = System.Drawing.Color.Transparent
    Me.lbAnno.Location = New System.Drawing.Point(14, 13)
    Me.lbAnno.Name = "lbAnno"
    Me.lbAnno.NTSDbField = ""
    Me.lbAnno.Size = New System.Drawing.Size(32, 13)
    Me.lbAnno.TabIndex = 4
    Me.lbAnno.Text = "Anno"
    Me.lbAnno.Tooltip = ""
    Me.lbAnno.UseMnemonic = False
    '
    'lbDalConto
    '
    Me.lbDalConto.AutoSize = True
    Me.lbDalConto.BackColor = System.Drawing.Color.Transparent
    Me.lbDalConto.Location = New System.Drawing.Point(14, 35)
    Me.lbDalConto.Name = "lbDalConto"
    Me.lbDalConto.NTSDbField = ""
    Me.lbDalConto.Size = New System.Drawing.Size(52, 13)
    Me.lbDalConto.TabIndex = 5
    Me.lbDalConto.Text = "Dal conto"
    Me.lbDalConto.Tooltip = ""
    Me.lbDalConto.UseMnemonic = False
    '
    'lbDaldat
    '
    Me.lbDaldat.AutoSize = True
    Me.lbDaldat.BackColor = System.Drawing.Color.Transparent
    Me.lbDaldat.Location = New System.Drawing.Point(14, 58)
    Me.lbDaldat.Name = "lbDaldat"
    Me.lbDaldat.NTSDbField = ""
    Me.lbDaldat.Size = New System.Drawing.Size(55, 13)
    Me.lbDaldat.TabIndex = 6
    Me.lbDaldat.Text = "Dalla data"
    Me.lbDaldat.Tooltip = ""
    Me.lbDaldat.UseMnemonic = False
    '
    'lbDalndo
    '
    Me.lbDalndo.AutoSize = True
    Me.lbDalndo.BackColor = System.Drawing.Color.Transparent
    Me.lbDalndo.Location = New System.Drawing.Point(14, 79)
    Me.lbDalndo.Name = "lbDalndo"
    Me.lbDalndo.NTSDbField = ""
    Me.lbDalndo.Size = New System.Drawing.Size(142, 13)
    Me.lbDalndo.TabIndex = 7
    Me.lbDalndo.Text = "Da serie/numero documento"
    Me.lbDalndo.Tooltip = ""
    Me.lbDalndo.UseMnemonic = False
    '
    'lbDalvet
    '
    Me.lbDalvet.AutoSize = True
    Me.lbDalvet.BackColor = System.Drawing.Color.Transparent
    Me.lbDalvet.Location = New System.Drawing.Point(14, 101)
    Me.lbDalvet.Name = "lbDalvet"
    Me.lbDalvet.NTSDbField = ""
    Me.lbDalvet.Size = New System.Drawing.Size(61, 13)
    Me.lbDalvet.TabIndex = 8
    Me.lbDalvet.Text = "Dal vettore"
    Me.lbDalvet.Tooltip = ""
    Me.lbDalvet.UseMnemonic = False
    '
    'lbAlldat
    '
    Me.lbAlldat.AutoSize = True
    Me.lbAlldat.BackColor = System.Drawing.Color.Transparent
    Me.lbAlldat.Location = New System.Drawing.Point(353, 58)
    Me.lbAlldat.Name = "lbAlldat"
    Me.lbAlldat.NTSDbField = ""
    Me.lbAlldat.Size = New System.Drawing.Size(48, 13)
    Me.lbAlldat.TabIndex = 9
    Me.lbAlldat.Text = "alla data"
    Me.lbAlldat.Tooltip = ""
    Me.lbAlldat.UseMnemonic = False
    '
    'lbAlconto
    '
    Me.lbAlconto.AutoSize = True
    Me.lbAlconto.BackColor = System.Drawing.Color.Transparent
    Me.lbAlconto.Location = New System.Drawing.Point(353, 35)
    Me.lbAlconto.Name = "lbAlconto"
    Me.lbAlconto.NTSDbField = ""
    Me.lbAlconto.Size = New System.Drawing.Size(45, 13)
    Me.lbAlconto.TabIndex = 10
    Me.lbAlconto.Text = "al conto"
    Me.lbAlconto.Tooltip = ""
    Me.lbAlconto.UseMnemonic = False
    '
    'lbAlndo
    '
    Me.lbAlndo.AutoSize = True
    Me.lbAlndo.BackColor = System.Drawing.Color.Transparent
    Me.lbAlndo.Location = New System.Drawing.Point(353, 79)
    Me.lbAlndo.Name = "lbAlndo"
    Me.lbAlndo.NTSDbField = ""
    Me.lbAlndo.Size = New System.Drawing.Size(135, 13)
    Me.lbAlndo.TabIndex = 11
    Me.lbAlndo.Text = "a serie/numero documento"
    Me.lbAlndo.Tooltip = ""
    Me.lbAlndo.UseMnemonic = False
    '
    'lbAlvett
    '
    Me.lbAlvett.AutoSize = True
    Me.lbAlvett.BackColor = System.Drawing.Color.Transparent
    Me.lbAlvett.Location = New System.Drawing.Point(353, 105)
    Me.lbAlvett.Name = "lbAlvett"
    Me.lbAlvett.NTSDbField = ""
    Me.lbAlvett.Size = New System.Drawing.Size(54, 13)
    Me.lbAlvett.TabIndex = 12
    Me.lbAlvett.Text = "al vettore"
    Me.lbAlvett.Tooltip = ""
    Me.lbAlvett.UseMnemonic = False
    '
    'lbSep1
    '
    Me.lbSep1.AutoSize = True
    Me.lbSep1.BackColor = System.Drawing.Color.Transparent
    Me.lbSep1.Location = New System.Drawing.Point(226, 79)
    Me.lbSep1.Name = "lbSep1"
    Me.lbSep1.NTSDbField = ""
    Me.lbSep1.Size = New System.Drawing.Size(11, 13)
    Me.lbSep1.TabIndex = 13
    Me.lbSep1.Text = "/"
    Me.lbSep1.Tooltip = ""
    Me.lbSep1.UseMnemonic = False
    '
    'lbSep2
    '
    Me.lbSep2.AutoSize = True
    Me.lbSep2.BackColor = System.Drawing.Color.Transparent
    Me.lbSep2.Location = New System.Drawing.Point(546, 79)
    Me.lbSep2.Name = "lbSep2"
    Me.lbSep2.NTSDbField = ""
    Me.lbSep2.Size = New System.Drawing.Size(11, 13)
    Me.lbSep2.TabIndex = 14
    Me.lbSep2.Text = "/"
    Me.lbSep2.Tooltip = ""
    Me.lbSep2.UseMnemonic = False
    '
    'edDalvet
    '
    Me.edDalvet.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDalvet.EditValue = "0"
    Me.edDalvet.Location = New System.Drawing.Point(175, 98)
    Me.edDalvet.Name = "edDalvet"
    Me.edDalvet.NTSDbField = ""
    Me.edDalvet.NTSFormat = "0"
    Me.edDalvet.NTSForzaVisZoom = False
    Me.edDalvet.NTSOldValue = ""
    Me.edDalvet.Properties.Appearance.Options.UseTextOptions = True
    Me.edDalvet.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDalvet.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDalvet.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDalvet.Properties.AutoHeight = False
    Me.edDalvet.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDalvet.Properties.MaxLength = 65536
    Me.edDalvet.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDalvet.Size = New System.Drawing.Size(61, 20)
    Me.edDalvet.TabIndex = 15
    Me.edDalvet.ToolTip = "0"
    '
    'edAnno
    '
    Me.edAnno.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnno.EditValue = "2008"
    Me.edAnno.Location = New System.Drawing.Point(175, 10)
    Me.edAnno.Name = "edAnno"
    Me.edAnno.NTSDbField = ""
    Me.edAnno.NTSFormat = "0"
    Me.edAnno.NTSForzaVisZoom = False
    Me.edAnno.NTSOldValue = "2008"
    Me.edAnno.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnno.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnno.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnno.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnno.Properties.AutoHeight = False
    Me.edAnno.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnno.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnno.Size = New System.Drawing.Size(61, 20)
    Me.edAnno.TabIndex = 16
    '
    'edDalcon
    '
    Me.edDalcon.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDalcon.EditValue = "0"
    Me.edDalcon.Location = New System.Drawing.Point(175, 32)
    Me.edDalcon.Name = "edDalcon"
    Me.edDalcon.NTSDbField = ""
    Me.edDalcon.NTSFormat = "0"
    Me.edDalcon.NTSForzaVisZoom = False
    Me.edDalcon.NTSOldValue = ""
    Me.edDalcon.Properties.Appearance.Options.UseTextOptions = True
    Me.edDalcon.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDalcon.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDalcon.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDalcon.Properties.AutoHeight = False
    Me.edDalcon.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDalcon.Properties.MaxLength = 65536
    Me.edDalcon.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDalcon.Size = New System.Drawing.Size(92, 20)
    Me.edDalcon.TabIndex = 17
    '
    'edAlcont
    '
    Me.edAlcont.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAlcont.EditValue = "999999999"
    Me.edAlcont.Location = New System.Drawing.Point(494, 32)
    Me.edAlcont.Name = "edAlcont"
    Me.edAlcont.NTSDbField = ""
    Me.edAlcont.NTSFormat = "0"
    Me.edAlcont.NTSForzaVisZoom = False
    Me.edAlcont.NTSOldValue = "999999999"
    Me.edAlcont.Properties.Appearance.Options.UseTextOptions = True
    Me.edAlcont.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAlcont.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAlcont.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAlcont.Properties.AutoHeight = False
    Me.edAlcont.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAlcont.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAlcont.Size = New System.Drawing.Size(92, 20)
    Me.edAlcont.TabIndex = 18
    '
    'edDaldoc
    '
    Me.edDaldoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDaldoc.EditValue = "0"
    Me.edDaldoc.Location = New System.Drawing.Point(241, 76)
    Me.edDaldoc.Name = "edDaldoc"
    Me.edDaldoc.NTSDbField = ""
    Me.edDaldoc.NTSFormat = "0"
    Me.edDaldoc.NTSForzaVisZoom = False
    Me.edDaldoc.NTSOldValue = ""
    Me.edDaldoc.Properties.Appearance.Options.UseTextOptions = True
    Me.edDaldoc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDaldoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDaldoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDaldoc.Properties.AutoHeight = False
    Me.edDaldoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDaldoc.Properties.MaxLength = 65536
    Me.edDaldoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDaldoc.Size = New System.Drawing.Size(92, 20)
    Me.edDaldoc.TabIndex = 19
    '
    'edAldocu
    '
    Me.edAldocu.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAldocu.EditValue = "99999999"
    Me.edAldocu.Location = New System.Drawing.Point(563, 76)
    Me.edAldocu.Name = "edAldocu"
    Me.edAldocu.NTSDbField = ""
    Me.edAldocu.NTSFormat = "0"
    Me.edAldocu.NTSForzaVisZoom = False
    Me.edAldocu.NTSOldValue = "99999999"
    Me.edAldocu.Properties.Appearance.Options.UseTextOptions = True
    Me.edAldocu.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAldocu.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAldocu.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAldocu.Properties.AutoHeight = False
    Me.edAldocu.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAldocu.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAldocu.Size = New System.Drawing.Size(92, 20)
    Me.edAldocu.TabIndex = 20
    '
    'edAlvett
    '
    Me.edAlvett.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAlvett.EditValue = "9999"
    Me.edAlvett.Location = New System.Drawing.Point(494, 98)
    Me.edAlvett.Name = "edAlvett"
    Me.edAlvett.NTSDbField = ""
    Me.edAlvett.NTSFormat = "0"
    Me.edAlvett.NTSForzaVisZoom = False
    Me.edAlvett.NTSOldValue = "9999"
    Me.edAlvett.Properties.Appearance.Options.UseTextOptions = True
    Me.edAlvett.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAlvett.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAlvett.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAlvett.Properties.AutoHeight = False
    Me.edAlvett.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAlvett.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAlvett.Size = New System.Drawing.Size(69, 20)
    Me.edAlvett.TabIndex = 21
    Me.edAlvett.ToolTip = "9999"
    '
    'edDaldat
    '
    Me.edDaldat.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDaldat.EditValue = "01/01/1900"
    Me.edDaldat.Location = New System.Drawing.Point(175, 54)
    Me.edDaldat.Name = "edDaldat"
    Me.edDaldat.NTSDbField = ""
    Me.edDaldat.NTSForzaVisZoom = False
    Me.edDaldat.NTSOldValue = ""
    Me.edDaldat.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDaldat.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDaldat.Properties.AutoHeight = False
    Me.edDaldat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDaldat.Properties.MaxLength = 65536
    Me.edDaldat.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDaldat.Size = New System.Drawing.Size(92, 20)
    Me.edDaldat.TabIndex = 22
    '
    'edAlldat
    '
    Me.edAlldat.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAlldat.EditValue = "31/12/2099"
    Me.edAlldat.Location = New System.Drawing.Point(494, 55)
    Me.edAlldat.Name = "edAlldat"
    Me.edAlldat.NTSDbField = ""
    Me.edAlldat.NTSForzaVisZoom = False
    Me.edAlldat.NTSOldValue = ""
    Me.edAlldat.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAlldat.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAlldat.Properties.AutoHeight = False
    Me.edAlldat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAlldat.Properties.MaxLength = 65536
    Me.edAlldat.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAlldat.Size = New System.Drawing.Size(92, 20)
    Me.edAlldat.TabIndex = 23
    '
    'edDalser
    '
    Me.edDalser.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edDalser.EditValue = " "
    Me.edDalser.Location = New System.Drawing.Point(175, 76)
    Me.edDalser.Name = "edDalser"
    Me.edDalser.NTSDbField = ""
    Me.edDalser.NTSForzaVisZoom = False
    Me.edDalser.NTSOldValue = " "
    Me.edDalser.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDalser.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDalser.Properties.AutoHeight = False
    Me.edDalser.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDalser.Properties.MaxLength = 65536
    Me.edDalser.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDalser.Size = New System.Drawing.Size(45, 20)
    Me.edDalser.TabIndex = 24
    '
    'edAlseri
    '
    Me.edAlseri.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAlseri.EditValue = "z"
    Me.edAlseri.Location = New System.Drawing.Point(494, 76)
    Me.edAlseri.Name = "edAlseri"
    Me.edAlseri.NTSDbField = ""
    Me.edAlseri.NTSForzaVisZoom = False
    Me.edAlseri.NTSOldValue = "z"
    Me.edAlseri.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAlseri.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAlseri.Properties.AutoHeight = False
    Me.edAlseri.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAlseri.Properties.MaxLength = 65536
    Me.edAlseri.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAlseri.Size = New System.Drawing.Size(45, 20)
    Me.edAlseri.TabIndex = 25
    '
    'fmTipdoc
    '
    Me.fmTipdoc.AllowDrop = True
    Me.fmTipdoc.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmTipdoc.Appearance.Options.UseBackColor = True
    Me.fmTipdoc.Controls.Add(Me.opImpcli)
    Me.fmTipdoc.Controls.Add(Me.opDdteme)
    Me.fmTipdoc.Controls.Add(Me.opFatimm)
    Me.fmTipdoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmTipdoc.Location = New System.Drawing.Point(17, 124)
    Me.fmTipdoc.Name = "fmTipdoc"
    Me.fmTipdoc.Size = New System.Drawing.Size(220, 91)
    Me.fmTipdoc.TabIndex = 26
    Me.fmTipdoc.Text = "Tipo documento:"
    '
    'opImpcli
    '
    Me.opImpcli.Cursor = System.Windows.Forms.Cursors.Default
    Me.opImpcli.Location = New System.Drawing.Point(6, 64)
    Me.opImpcli.Name = "opImpcli"
    Me.opImpcli.NTSCheckValue = "S"
    Me.opImpcli.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opImpcli.Properties.Appearance.Options.UseBackColor = True
    Me.opImpcli.Properties.AutoHeight = False
    Me.opImpcli.Properties.Caption = "Impegni Clienti"
    Me.opImpcli.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opImpcli.Size = New System.Drawing.Size(103, 19)
    Me.opImpcli.TabIndex = 2
    '
    'opDdteme
    '
    Me.opDdteme.Cursor = System.Windows.Forms.Cursors.Default
    Me.opDdteme.Location = New System.Drawing.Point(6, 44)
    Me.opDdteme.Name = "opDdteme"
    Me.opDdteme.NTSCheckValue = "S"
    Me.opDdteme.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opDdteme.Properties.Appearance.Options.UseBackColor = True
    Me.opDdteme.Properties.AutoHeight = False
    Me.opDdteme.Properties.Caption = "D.D.T. Emessi"
    Me.opDdteme.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opDdteme.Size = New System.Drawing.Size(103, 19)
    Me.opDdteme.TabIndex = 1
    '
    'opFatimm
    '
    Me.opFatimm.Cursor = System.Windows.Forms.Cursors.Default
    Me.opFatimm.EditValue = True
    Me.opFatimm.Location = New System.Drawing.Point(6, 23)
    Me.opFatimm.Name = "opFatimm"
    Me.opFatimm.NTSCheckValue = "S"
    Me.opFatimm.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opFatimm.Properties.Appearance.Options.UseBackColor = True
    Me.opFatimm.Properties.AutoHeight = False
    Me.opFatimm.Properties.Caption = "Fatture Immediate Emesse"
    Me.opFatimm.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opFatimm.Size = New System.Drawing.Size(167, 19)
    Me.opFatimm.TabIndex = 0
    '
    'fmStapes
    '
    Me.fmStapes.AllowDrop = True
    Me.fmStapes.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmStapes.Appearance.Options.UseBackColor = True
    Me.fmStapes.Controls.Add(Me.ckStapes)
    Me.fmStapes.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmStapes.Location = New System.Drawing.Point(243, 182)
    Me.fmStapes.Name = "fmStapes"
    Me.fmStapes.ShowCaption = False
    Me.fmStapes.Size = New System.Drawing.Size(131, 33)
    Me.fmStapes.TabIndex = 27
    '
    'ckStapes
    '
    Me.ckStapes.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckStapes.Location = New System.Drawing.Point(13, 7)
    Me.ckStapes.Name = "ckStapes"
    Me.ckStapes.NTSCheckValue = "S"
    Me.ckStapes.NTSUnCheckValue = "N"
    Me.ckStapes.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckStapes.Properties.Appearance.Options.UseBackColor = True
    Me.ckStapes.Properties.AutoHeight = False
    Me.ckStapes.Properties.Caption = "Stampa peso"
    Me.ckStapes.Size = New System.Drawing.Size(114, 19)
    Me.ckStapes.TabIndex = 0
    '
    'pnControlli
    '
    Me.pnControlli.AllowDrop = True
    Me.pnControlli.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnControlli.Appearance.Options.UseBackColor = True
    Me.pnControlli.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnControlli.Controls.Add(Me.fmStapes)
    Me.pnControlli.Controls.Add(Me.fmTipdoc)
    Me.pnControlli.Controls.Add(Me.edAnno)
    Me.pnControlli.Controls.Add(Me.lbAnno)
    Me.pnControlli.Controls.Add(Me.edAlseri)
    Me.pnControlli.Controls.Add(Me.lbDalConto)
    Me.pnControlli.Controls.Add(Me.edDalser)
    Me.pnControlli.Controls.Add(Me.lbDaldat)
    Me.pnControlli.Controls.Add(Me.edAlldat)
    Me.pnControlli.Controls.Add(Me.lbDalndo)
    Me.pnControlli.Controls.Add(Me.edDaldat)
    Me.pnControlli.Controls.Add(Me.lbDalvet)
    Me.pnControlli.Controls.Add(Me.edAlvett)
    Me.pnControlli.Controls.Add(Me.lbAlldat)
    Me.pnControlli.Controls.Add(Me.edAldocu)
    Me.pnControlli.Controls.Add(Me.lbAlconto)
    Me.pnControlli.Controls.Add(Me.edDaldoc)
    Me.pnControlli.Controls.Add(Me.lbAlndo)
    Me.pnControlli.Controls.Add(Me.edAlcont)
    Me.pnControlli.Controls.Add(Me.lbAlvett)
    Me.pnControlli.Controls.Add(Me.edDalcon)
    Me.pnControlli.Controls.Add(Me.lbSep1)
    Me.pnControlli.Controls.Add(Me.lbSep2)
    Me.pnControlli.Controls.Add(Me.edDalvet)
    Me.pnControlli.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnControlli.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnControlli.Location = New System.Drawing.Point(0, 30)
    Me.pnControlli.Name = "pnControlli"
    Me.pnControlli.NTSActiveTrasparency = True
    Me.pnControlli.Size = New System.Drawing.Size(672, 227)
    Me.pnControlli.TabIndex = 29
    Me.pnControlli.Text = "NtsPanel2"
    '
    'FRMVESTCO
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(672, 257)
    Me.Controls.Add(Me.pnControlli)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.MaximizeBox = False
    Me.Name = "FRMVESTCO"
    Me.Text = "STAMPA ETICHETTE SOVRACOLLO"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDalvet.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDalcon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAlcont.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDaldoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAldocu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAlvett.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDaldat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAlldat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDalser.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAlseri.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmTipdoc, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmTipdoc.ResumeLayout(False)
    Me.fmTipdoc.PerformLayout()
    CType(Me.opImpcli.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opDdteme.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opFatimm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmStapes, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmStapes.ResumeLayout(False)
    Me.fmStapes.PerformLayout()
    CType(Me.ckStapes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnControlli, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnControlli.ResumeLayout(False)
    Me.pnControlli.PerformLayout()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, False)
    '----------------------------------------------------------------------------------------------------------------
    Try
      '--------------------------------------------------------------------------------------------------------------
      Try
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
      End Try
      '--------------------------------------------------------------------------------------------------------------
      ckStapes.NTSSetParam(oMenu, oApp.Tr(Me, 128642145255625000, "Stampa peso"), "S", "N")
      opImpcli.NTSSetParam(oMenu, oApp.Tr(Me, 128642145255781250, "Impegni Clienti"), "I")
      opDdteme.NTSSetParam(oMenu, oApp.Tr(Me, 128642145255937500, "D.D.T. Emessi"), "D")
      opFatimm.NTSSetParam(oMenu, oApp.Tr(Me, 128744946307796935, "Fatture Immediate Emesse"), "F")
      edAnno.NTSSetParam(oMenu, oApp.Tr(Me, 128642145257500000, "Anno"), "0", 4, 1900, 2099)
      edDalcon.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128642145257343750, "Dal conto"), tabanagrac)
      edAlcont.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128642145257187500, "al conto"), tabanagrac)
      edDaldoc.NTSSetParam(oMenu, oApp.Tr(Me, 128642145257031250, "Dal numero documento"), "0", 9, 0, 999999999)
      edAldocu.NTSSetParam(oMenu, oApp.Tr(Me, 128642145256875000, "al numero documento"), "0", 9, 0, 999999999)
      edDalser.NTSSetParam(oMenu, oApp.Tr(Me, 128642145256250000, "Dalla serie"), CLN__STD.SerieMaxLen)
      edAlseri.NTSSetParam(oMenu, oApp.Tr(Me, 128744946421859435, "alla serie"), CLN__STD.SerieMaxLen)
      edDaldat.NTSSetParam(oMenu, oApp.Tr(Me, 128642145256562500, "Dalla data"), True)
      edAlldat.NTSSetParam(oMenu, oApp.Tr(Me, 128642145256406250, "alla data"), False)
      edDalvet.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128642145257656250, "dal vettore"), tabvett)
      edAlvett.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128642145256718750, "al vettore"), tabvett)
      '--------------------------------------------------------------------------------------------------------------
      edAlseri.Text = "Z".PadRight(CLN__STD.SerieMaxLen, "Z"c)
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
  Public Overridable Sub FRMVESTCO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim strPar() As String

    Try
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      edAnno.Text = NTSCStr(Date.Now.Year)
      edAlldat.Text = IntSetDate("31/12/" + edAnno.Text)
      edDaldat.Text = IntSetDate("01/01/" + edAnno.Text)
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
      GctlApplicaDefaultValue()
      '--------------------------------------------------------------------------------------------------------------
      '--- Acquisisce InstID
      '--------------------------------------------------------------------------------------------------------------
      lInstID = oMenu.GetTblInstId("TTSTCO", bsDbAzienda)
      '--------------------------------------------------------------------------------------------------------------
      '--- Sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
      '--------------------------------------------------------------------------------------------------------------
      If Not oCallParams Is Nothing Then
        strPar = oCallParams.strParam.Split(";"c)
        Select Case Microsoft.VisualBasic.Left(oCallParams.strParam, 4)
          Case "DOC;"
            Select Case strPar(1)
              Case "R" : opImpcli.Checked = True
              Case "A" : opFatimm.Checked = True
              Case "B" : opDdteme.Checked = True
            End Select
            edAnno.Text = strPar(2)
            edDalser.Text = strPar(3)
            edAlseri.Text = strPar(3)
            edDaldoc.Text = strPar(4)
            edAldocu.Text = strPar(4)
          Case "ELA;"
            Select Case strPar(1)
              Case "R" : opImpcli.Checked = True
              Case "A" : opFatimm.Checked = True
              Case "B" : opDdteme.Checked = True
            End Select
            edAnno.Text = strPar(2)
            edDalser.Text = strPar(3)
            edAlseri.Text = strPar(3)
            edDaldoc.Text = strPar(4).Trim
            edAldocu.Text = strPar(6).Trim
        End Select
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMVESTCO_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Cancella i dati contenuti nella tabella temporanea (che hanno stesso InstID)
      '--------------------------------------------------------------------------------------------------------------
      oMenu.ResetTblInstId("TTSTCO", False, lInstID)
      '--------------------------------------------------------------------------------------------------------------
      dcStco.Dispose()
      dsStco.Dispose()
      '--------------------------------------------------------------------------------------------------------------
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
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

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      '------------------------------------
      'zoom standard di textbox e griglia
      NTSCallStandardZoom()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    Try
      oMenu.ReportImposta(Me)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
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
#End Region

#Region "Eventi TextBox"
  Public Overridable Sub edDalcon_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDalcon.Validated
    Try
      '--------------------------------------------------------------------------------------------------------------
      If edDalcon.Text <> "0" Then edAlcont.Text = edDalcon.Text
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edAlcont_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAlcont.Validated
    Try
    Catch ex As Exception
    End Try
  End Sub

  Public Overridable Sub edDaldat_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDaldat.Validated
    Try
    Catch ex As Exception
    End Try
  End Sub

  Public Overridable Sub edAlldat_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAlldat.Validated
    Try
    Catch ex As Exception
    End Try
  End Sub

  Public Overridable Sub edDalser_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDalser.Validated
    Try
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edDaldoc_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDaldoc.Validated
    Try
    Catch ex As Exception
    End Try
  End Sub

  Public Overridable Sub edAlseri_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAlseri.Validated
    Try
    Catch ex As Exception
    End Try
  End Sub

  Public Overridable Sub edAldocu_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAldocu.Validated
    Try
    Catch ex As Exception
    End Try
  End Sub

  Public Overridable Sub edDalvet_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDalvet.Validated
    Try
    Catch ex As Exception
    End Try
  End Sub

  Public Overridable Sub edAlvett_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAlvett.Validated
    Try
    Catch ex As Exception
    End Try
  End Sub
#End Region

  Public Overridable Function CheckIntervalli() As Boolean
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edDalcon.Text) > NTSCInt(edAlcont.Text) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128744947937911081, "Attenzione!" & vbCrLf & _
          "Il conto iniziale non può essere superiore a quello finale."))
        edDalcon.Text = "0"
        edAlcont.Text = "".PadLeft(9, "9"c)
        edDalcon.Focus()
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCDate(edDaldat.Text) > NTSCDate(edAlldat.Text) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128744946721078185, "Attenzione!" & vbCrLf & _
          "La data iniziale non può essere superiore a quella finale."))
        edDaldat.Text = IntSetDate("01/01/" + edAnno.Text)
        edAlldat.Text = IntSetDate("31/12/" + edAnno.Text)
        edDaldat.Focus()
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      strTmp = TestSerieMaxLen(edDalser.Text, False)
      If strTmp <> edDalser.Text Then edDalser.Text = strTmp
      If edDalser.Text > edAlseri.Text Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128744947738273423, "Attenzione!" & vbCrLf & _
          "La serie iniziale non può essere superiore a quella finale."))
        edDalser.Text = ""
        edDalser.Focus()
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (edDalser.Text = edAlseri.Text) And (NTSCInt(edDaldoc.Text) > NTSCInt(edAldocu.Text)) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128744947435067872, "Attenzione!" & vbCrLf & _
          "Il numero documento iniziale non può essere superiore a quello finale."))
        edDaldoc.Text = "0"
        edAldocu.Text = "".PadLeft(9, "9"c)
        edDaldoc.Focus()
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edDalvet.Text) > NTSCInt(edAlvett.Text) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128744947150920063, "Attenzione!" & vbCrLf & _
          "Il vettore iniziale non può essere superiore a quello finale."))
        edDalvet.Text = "0"
        edAlvett.Text = "".PadLeft(4, "9"c)
        edDalvet.Focus()
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim i As Integer = 0
    Dim j As Integer = 0
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim nPjob As Object

    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      If CheckIntervalli() = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      strCrpe = "{ttstco.codditt} = '" & DittaCorrente & "'" & _
        " And " & "{ttstco.instid} = " & lInstID
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSVESTCO", "Reports1", " ", 0, nDestin, "BSVESTCO.RPT", False, "Stampa etichette sovracollo", False)
      '--------------------------------------------------------------------------------------------------------------
      If nPjob Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      oMenu.ResetTblInstId("TTSTCO", False, lInstID)
      '--------------------------------------------------------------------------------------------------------------
      '--- Cancella i dati contenuti nella tabella temporanea (che hanno stesso InstID)
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleStco.InserDatiTT(lInstID, NTSCInt(edDalcon.Text), NTSCInt(edAlcont.Text), _
        NTSCDate(edDaldat.Text), NTSCDate(edAlldat.Text), _
        NTSCInt(edDaldoc.Text), NTSCInt(edAldocu.Text), _
        NTSCStr(edDalser.Text), NTSCStr(edAlseri.Text), _
        NTSCInt(edDalvet.Text), NTSCInt(edAlvett.Text), opFatimm.Checked, ckStapes.Checked, _
        opImpcli.Checked, NTSCInt(edAnno.Text)) Then Return
      '--------------------------------------------------------------------------------------------------------------
      For i = 1 To UBound(CType(nPjob, Array), 2)
        If i = 1 Then
          nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), strCrpe)
          nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
        Else
          If Trim$(NTSCStr(CType(nPjob, Array).GetValue(2, i))) <> "" Then
            nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, NTSCStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
          End If
          For j = 3 To 12
            If Trim$(NTSCStr(CType(nPjob, Array).GetValue(j, i))) <> "" Then
              nRis = oMenu.PESetFormula(CInt(CType(nPjob, Array).GetValue(0, i)), NTSCStr(CType(nPjob, Array).GetValue(j, i)), CrpeResolveFormula(Me, NTSCStr(CType(nPjob, Array).GetValue(j + 10, i))))
            End If
          Next j
          nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
        End If
      Next
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class