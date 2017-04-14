Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__CIVA

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

#Region "Variabili"
  Public oCleCiva As CLE__CIVA
  Public dsCiva As DataSet
  Public dcCiva As BindingSource = New BindingSource()
  Public oCallParams As CLE__CLDP
  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnCiva As NTSInformatica.NTSPanel
  Public WithEvents grCiva As NTSInformatica.NTSGrid
  Public WithEvents grvCiva As NTSInformatica.NTSGridView
  Public WithEvents tb_codciva As NTSInformatica.NTSGridColumn
  Public WithEvents tb_desciva As NTSInformatica.NTSGridColumn
  Public WithEvents tb_tipiva As NTSInformatica.NTSGridColumn
  Public WithEvents tb_aliq As NTSInformatica.NTSGridColumn
  Public WithEvents tb_inded As NTSInformatica.NTSGridColumn
  Public WithEvents tb_aliqmono As NTSInformatica.NTSGridColumn
  Public WithEvents tb_tipel As NTSInformatica.NTSGridColumn
  Public WithEvents tb_codtpel As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codtpel As NTSInformatica.NTSGridColumn
  Public WithEvents tb_tipacq As NTSInformatica.NTSGridColumn
  Public WithEvents xx_tipacq As NTSInformatica.NTSGridColumn
  Public WithEvents tb_monofissa As NTSInformatica.NTSGridColumn
  Public WithEvents tb_desest As NTSInformatica.NTSGridColumn
  Public WithEvents tb_percomp As NTSInformatica.NTSGridColumn
  Public WithEvents tb_vp11 As NTSInformatica.NTSGridColumn
  Public WithEvents tb_vp12 As NTSInformatica.NTSGridColumn
  Public WithEvents tb_vp21 As NTSInformatica.NTSGridColumn
  Public WithEvents tb_vp22 As NTSInformatica.NTSGridColumn
  Public WithEvents tb_volaff As NTSInformatica.NTSGridColumn
  Public WithEvents tb_espror As NTSInformatica.NTSGridColumn
  Public WithEvents tb_mvcodiva As NTSInformatica.NTSGridColumn
  Public WithEvents xx_mvcodiva As NTSInformatica.NTSGridColumn
  Public WithEvents tb_tpaggpl As NTSInformatica.NTSGridColumn
  Public WithEvents tb_art73t As NTSInformatica.NTSGridColumn
  Public WithEvents tb_aliqdetr As NTSInformatica.NTSGridColumn
  Public WithEvents tb_codindetr As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codindetr As NTSInformatica.NTSGridColumn
  Public WithEvents tb_coddetr As NTSInformatica.NTSGridColumn
  Public WithEvents xx_coddetr As NTSInformatica.NTSGridColumn
  Public WithEvents tb_aliqagr As NTSInformatica.NTSGridColumn
  Public WithEvents tb_codagr As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codagr As NTSInformatica.NTSGridColumn
  Public WithEvents tb_art838 As NTSInformatica.NTSGridColumn
  Public WithEvents tb_codteor As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codteor As NTSInformatica.NTSGridColumn
  Public WithEvents tb_passint As NTSInformatica.NTSGridColumn
  Public WithEvents tb_rilanven As NTSInformatica.NTSGridColumn
  Public WithEvents tb_rilanacq As NTSInformatica.NTSGridColumn
  Public WithEvents tb_estrdat1 As NTSInformatica.NTSGridColumn
  Public WithEvents tb_estrdat2 As NTSInformatica.NTSGridColumn
  Public WithEvents tb_revcharge As NTSInformatica.NTSGridColumn
  Public WithEvents tlbDuplica As NTSInformatica.NTSBarButtonItem
  Public WithEvents tb_reparto As NTSInformatica.NTSGridColumn
  Public WithEvents xx_reparto As NTSInformatica.NTSGridColumn
  Public WithEvents tb_codcivaal As NTSInformatica.NTSGridColumn
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

    InitializeComponent()
    Me.MinimumSize = Me.Size

    '------------------------------------------------
    'Creo e attivo l'entity, in più inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY stesso
    Dim strErr As String = ""
    Dim objTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__CIVA", "BE__CIVA", objTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128182574024687500, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleCiva = CType(objTmp, CLE__CIVA)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__CIVA", strRemoteServer, strRemotePort)
    AddHandler oCleCiva.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleCiva.Init(oApp, NTSScript, oMenu.oCleComm, "TABCIVA", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__CIVA))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbDuplica = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
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
    Me.pnCiva = New NTSInformatica.NTSPanel
    Me.grCiva = New NTSInformatica.NTSGrid
    Me.grvCiva = New NTSInformatica.NTSGridView
    Me.tb_reparto = New NTSInformatica.NTSGridColumn
    Me.xx_reparto = New NTSInformatica.NTSGridColumn
    Me.tb_codciva = New NTSInformatica.NTSGridColumn
    Me.tb_desciva = New NTSInformatica.NTSGridColumn
    Me.tb_tipiva = New NTSInformatica.NTSGridColumn
    Me.tb_aliq = New NTSInformatica.NTSGridColumn
    Me.tb_inded = New NTSInformatica.NTSGridColumn
    Me.tb_aliqmono = New NTSInformatica.NTSGridColumn
    Me.tb_tipel = New NTSInformatica.NTSGridColumn
    Me.tb_codtpel = New NTSInformatica.NTSGridColumn
    Me.xx_codtpel = New NTSInformatica.NTSGridColumn
    Me.tb_tipacq = New NTSInformatica.NTSGridColumn
    Me.xx_tipacq = New NTSInformatica.NTSGridColumn
    Me.tb_monofissa = New NTSInformatica.NTSGridColumn
    Me.tb_desest = New NTSInformatica.NTSGridColumn
    Me.tb_percomp = New NTSInformatica.NTSGridColumn
    Me.tb_vp11 = New NTSInformatica.NTSGridColumn
    Me.tb_vp12 = New NTSInformatica.NTSGridColumn
    Me.tb_vp21 = New NTSInformatica.NTSGridColumn
    Me.tb_vp22 = New NTSInformatica.NTSGridColumn
    Me.tb_volaff = New NTSInformatica.NTSGridColumn
    Me.tb_espror = New NTSInformatica.NTSGridColumn
    Me.tb_mvcodiva = New NTSInformatica.NTSGridColumn
    Me.xx_mvcodiva = New NTSInformatica.NTSGridColumn
    Me.tb_tpaggpl = New NTSInformatica.NTSGridColumn
    Me.tb_art73t = New NTSInformatica.NTSGridColumn
    Me.tb_aliqdetr = New NTSInformatica.NTSGridColumn
    Me.tb_codindetr = New NTSInformatica.NTSGridColumn
    Me.xx_codindetr = New NTSInformatica.NTSGridColumn
    Me.tb_coddetr = New NTSInformatica.NTSGridColumn
    Me.xx_coddetr = New NTSInformatica.NTSGridColumn
    Me.tb_aliqagr = New NTSInformatica.NTSGridColumn
    Me.tb_codagr = New NTSInformatica.NTSGridColumn
    Me.xx_codagr = New NTSInformatica.NTSGridColumn
    Me.tb_art838 = New NTSInformatica.NTSGridColumn
    Me.tb_codteor = New NTSInformatica.NTSGridColumn
    Me.xx_codteor = New NTSInformatica.NTSGridColumn
    Me.tb_passint = New NTSInformatica.NTSGridColumn
    Me.tb_rilanven = New NTSInformatica.NTSGridColumn
    Me.tb_rilanacq = New NTSInformatica.NTSGridColumn
    Me.tb_estrdat1 = New NTSInformatica.NTSGridColumn
    Me.tb_estrdat2 = New NTSInformatica.NTSGridColumn
    Me.tb_codcivaal = New NTSInformatica.NTSGridColumn
    Me.tb_revcharge = New NTSInformatica.NTSGridColumn
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnCiva, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCiva.SuspendLayout()
    CType(Me.grCiva, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvCiva, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbDuplica})
    Me.NtsBarManager1.MaxItemId = 16
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDuplica), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbDuplica
    '
    Me.tlbDuplica.Caption = "Duplica"
    Me.tlbDuplica.Glyph = CType(resources.GetObject("tlbDuplica.Glyph"), System.Drawing.Image)
    Me.tlbDuplica.GlyphPath = ""
    Me.tlbDuplica.Id = 15
    Me.tlbDuplica.Name = "tlbDuplica"
    Me.tlbDuplica.Visible = True
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
    Me.tlbRipristina.Id = 3
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.GlyphPath = ""
    Me.tlbCancella.Id = 2
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
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 9
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta stampante"
    Me.tlbImpostaStampante.GlyphPath = ""
    Me.tlbImpostaStampante.Id = 10
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.GlyphPath = ""
    Me.tlbStampa.Id = 11
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa a video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.GlyphPath = ""
    Me.tlbStampaVideo.Id = 12
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 13
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 14
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnCiva
    '
    Me.pnCiva.AllowDrop = True
    Me.pnCiva.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCiva.Appearance.Options.UseBackColor = True
    Me.pnCiva.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCiva.Controls.Add(Me.grCiva)
    Me.pnCiva.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCiva.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnCiva.Location = New System.Drawing.Point(0, 30)
    Me.pnCiva.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnCiva.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnCiva.Name = "pnCiva"
    Me.pnCiva.NTSActiveTrasparency = True
    Me.pnCiva.Size = New System.Drawing.Size(688, 374)
    Me.pnCiva.TabIndex = 0
    Me.pnCiva.Text = "pnCiva"
    '
    'grCiva
    '
    Me.grCiva.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grCiva.EmbeddedNavigator.Name = ""
    Me.grCiva.Location = New System.Drawing.Point(0, 0)
    Me.grCiva.MainView = Me.grvCiva
    Me.grCiva.Name = "grCiva"
    Me.grCiva.Size = New System.Drawing.Size(688, 374)
    Me.grCiva.TabIndex = 1
    Me.grCiva.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvCiva})
    '
    'grvCiva
    '
    Me.grvCiva.ActiveFilterEnabled = False
    '
    'tb_reparto
    '
    Me.tb_reparto.AppearanceCell.Options.UseBackColor = True
    Me.tb_reparto.AppearanceCell.Options.UseTextOptions = True
    Me.tb_reparto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_reparto.Caption = "Reparto Scontrino"
    Me.tb_reparto.Enabled = True
    Me.tb_reparto.FieldName = "tb_reparto"
    Me.tb_reparto.Name = "tb_reparto"
    Me.tb_reparto.NTSRepositoryComboBox = Nothing
    Me.tb_reparto.NTSRepositoryItemCheck = Nothing
    Me.tb_reparto.NTSRepositoryItemMemo = Nothing
    Me.tb_reparto.NTSRepositoryItemText = Nothing
    Me.tb_reparto.Visible = True
    Me.tb_reparto.VisibleIndex = 33
    '
    'xx_reparto
    '
    Me.xx_reparto.AppearanceCell.Options.UseBackColor = True
    Me.xx_reparto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_reparto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_reparto.Caption = "Descr. Reparto"
    Me.xx_reparto.Enabled = False
    Me.xx_reparto.FieldName = "xx_reparto"
    Me.xx_reparto.Name = "xx_reparto"
    Me.xx_reparto.NTSRepositoryComboBox = Nothing
    Me.xx_reparto.NTSRepositoryItemCheck = Nothing
    Me.xx_reparto.NTSRepositoryItemMemo = Nothing
    Me.xx_reparto.NTSRepositoryItemText = Nothing
    Me.xx_reparto.OptionsColumn.AllowEdit = False
    Me.xx_reparto.OptionsColumn.ReadOnly = True
    Me.xx_reparto.Visible = True
    Me.xx_reparto.VisibleIndex = 34
    Me.grvCiva.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tb_codciva, Me.tb_desciva, Me.tb_tipiva, Me.tb_aliq, Me.tb_inded, Me.tb_aliqmono, Me.tb_tipel, Me.tb_codtpel, Me.xx_codtpel, Me.tb_tipacq, Me.xx_tipacq, Me.tb_monofissa, Me.tb_desest, Me.tb_percomp, Me.tb_vp11, Me.tb_vp12, Me.tb_vp21, Me.tb_vp22, Me.tb_volaff, Me.tb_espror, Me.tb_mvcodiva, Me.xx_mvcodiva, Me.tb_tpaggpl, Me.tb_art73t, Me.tb_aliqdetr, Me.tb_codindetr, Me.xx_codindetr, Me.tb_coddetr, Me.xx_coddetr, Me.tb_aliqagr, Me.tb_codagr, Me.xx_codagr, Me.tb_art838, Me.tb_codteor, Me.xx_codteor, Me.tb_passint, Me.tb_rilanven, Me.tb_rilanacq, Me.tb_estrdat1, Me.tb_estrdat2, Me.tb_codcivaal, Me.tb_revcharge, Me.tb_reparto, Me.xx_reparto})
    Me.grvCiva.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvCiva.Enabled = True
    Me.grvCiva.GridControl = Me.grCiva
    Me.grvCiva.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvCiva.MinRowHeight = 14
    Me.grvCiva.Name = "grvCiva"
    Me.grvCiva.NTSAllowDelete = True
    Me.grvCiva.NTSAllowInsert = True
    Me.grvCiva.NTSAllowUpdate = True
    Me.grvCiva.NTSMenuContext = Nothing
    Me.grvCiva.OptionsCustomization.AllowRowSizing = True
    Me.grvCiva.OptionsFilter.AllowFilterEditor = False
    Me.grvCiva.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvCiva.OptionsNavigation.UseTabKey = False
    Me.grvCiva.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvCiva.OptionsView.ColumnAutoWidth = False
    Me.grvCiva.OptionsView.EnableAppearanceEvenRow = True
    Me.grvCiva.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvCiva.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvCiva.OptionsView.ShowGroupPanel = False
    Me.grvCiva.RowHeight = 16
    '
    'tb_codciva
    '
    Me.tb_codciva.AppearanceCell.Options.UseBackColor = True
    Me.tb_codciva.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codciva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codciva.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_codciva.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_codciva.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_codciva.Caption = "Cod. IVA"
    Me.tb_codciva.Enabled = True
    Me.tb_codciva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codciva.Name = "tb_codciva"
    Me.tb_codciva.NTSRepositoryComboBox = Nothing
    Me.tb_codciva.NTSRepositoryItemCheck = Nothing
    Me.tb_codciva.NTSRepositoryItemMemo = Nothing
    Me.tb_codciva.NTSRepositoryItemText = Nothing
    Me.tb_codciva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codciva.OptionsFilter.AllowFilter = False
    Me.tb_codciva.Visible = True
    Me.tb_codciva.VisibleIndex = 0
    '
    'tb_desciva
    '
    Me.tb_desciva.AppearanceCell.Options.UseBackColor = True
    Me.tb_desciva.AppearanceCell.Options.UseTextOptions = True
    Me.tb_desciva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_desciva.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_desciva.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_desciva.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_desciva.Caption = "Descr. IVA"
    Me.tb_desciva.Enabled = True
    Me.tb_desciva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_desciva.Name = "tb_desciva"
    Me.tb_desciva.NTSRepositoryComboBox = Nothing
    Me.tb_desciva.NTSRepositoryItemCheck = Nothing
    Me.tb_desciva.NTSRepositoryItemMemo = Nothing
    Me.tb_desciva.NTSRepositoryItemText = Nothing
    Me.tb_desciva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_desciva.OptionsFilter.AllowFilter = False
    Me.tb_desciva.Visible = True
    Me.tb_desciva.VisibleIndex = 1
    '
    'tb_tipiva
    '
    Me.tb_tipiva.AppearanceCell.Options.UseBackColor = True
    Me.tb_tipiva.AppearanceCell.Options.UseTextOptions = True
    Me.tb_tipiva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_tipiva.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_tipiva.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_tipiva.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_tipiva.Caption = "Tipo IVA"
    Me.tb_tipiva.Enabled = True
    Me.tb_tipiva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_tipiva.Name = "tb_tipiva"
    Me.tb_tipiva.NTSRepositoryComboBox = Nothing
    Me.tb_tipiva.NTSRepositoryItemCheck = Nothing
    Me.tb_tipiva.NTSRepositoryItemMemo = Nothing
    Me.tb_tipiva.NTSRepositoryItemText = Nothing
    Me.tb_tipiva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_tipiva.OptionsFilter.AllowFilter = False
    Me.tb_tipiva.Visible = True
    Me.tb_tipiva.VisibleIndex = 2
    '
    'tb_aliq
    '
    Me.tb_aliq.AppearanceCell.Options.UseBackColor = True
    Me.tb_aliq.AppearanceCell.Options.UseTextOptions = True
    Me.tb_aliq.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_aliq.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_aliq.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_aliq.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_aliq.Caption = "Aliquota"
    Me.tb_aliq.Enabled = True
    Me.tb_aliq.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_aliq.Name = "tb_aliq"
    Me.tb_aliq.NTSRepositoryComboBox = Nothing
    Me.tb_aliq.NTSRepositoryItemCheck = Nothing
    Me.tb_aliq.NTSRepositoryItemMemo = Nothing
    Me.tb_aliq.NTSRepositoryItemText = Nothing
    Me.tb_aliq.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_aliq.OptionsFilter.AllowFilter = False
    Me.tb_aliq.Visible = True
    Me.tb_aliq.VisibleIndex = 3
    '
    'tb_inded
    '
    Me.tb_inded.AppearanceCell.Options.UseBackColor = True
    Me.tb_inded.AppearanceCell.Options.UseTextOptions = True
    Me.tb_inded.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_inded.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_inded.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_inded.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_inded.Caption = "% Indetr."
    Me.tb_inded.Enabled = True
    Me.tb_inded.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_inded.Name = "tb_inded"
    Me.tb_inded.NTSRepositoryComboBox = Nothing
    Me.tb_inded.NTSRepositoryItemCheck = Nothing
    Me.tb_inded.NTSRepositoryItemMemo = Nothing
    Me.tb_inded.NTSRepositoryItemText = Nothing
    Me.tb_inded.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_inded.OptionsFilter.AllowFilter = False
    Me.tb_inded.Visible = True
    Me.tb_inded.VisibleIndex = 4
    '
    'tb_aliqmono
    '
    Me.tb_aliqmono.AppearanceCell.Options.UseBackColor = True
    Me.tb_aliqmono.AppearanceCell.Options.UseTextOptions = True
    Me.tb_aliqmono.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_aliqmono.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_aliqmono.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_aliqmono.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_aliqmono.Caption = "Aliq. monofase"
    Me.tb_aliqmono.Enabled = True
    Me.tb_aliqmono.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_aliqmono.Name = "tb_aliqmono"
    Me.tb_aliqmono.NTSRepositoryComboBox = Nothing
    Me.tb_aliqmono.NTSRepositoryItemCheck = Nothing
    Me.tb_aliqmono.NTSRepositoryItemMemo = Nothing
    Me.tb_aliqmono.NTSRepositoryItemText = Nothing
    Me.tb_aliqmono.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_aliqmono.OptionsFilter.AllowFilter = False
    '
    'tb_tipel
    '
    Me.tb_tipel.AppearanceCell.Options.UseBackColor = True
    Me.tb_tipel.AppearanceCell.Options.UseTextOptions = True
    Me.tb_tipel.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_tipel.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_tipel.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_tipel.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_tipel.Caption = "Tipo elenco"
    Me.tb_tipel.Enabled = True
    Me.tb_tipel.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_tipel.Name = "tb_tipel"
    Me.tb_tipel.NTSRepositoryComboBox = Nothing
    Me.tb_tipel.NTSRepositoryItemCheck = Nothing
    Me.tb_tipel.NTSRepositoryItemMemo = Nothing
    Me.tb_tipel.NTSRepositoryItemText = Nothing
    Me.tb_tipel.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_tipel.OptionsFilter.AllowFilter = False
    '
    'tb_codtpel
    '
    Me.tb_codtpel.AppearanceCell.Options.UseBackColor = True
    Me.tb_codtpel.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codtpel.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codtpel.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_codtpel.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_codtpel.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_codtpel.Caption = "N° elenco"
    Me.tb_codtpel.Enabled = True
    Me.tb_codtpel.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codtpel.Name = "tb_codtpel"
    Me.tb_codtpel.NTSRepositoryComboBox = Nothing
    Me.tb_codtpel.NTSRepositoryItemCheck = Nothing
    Me.tb_codtpel.NTSRepositoryItemMemo = Nothing
    Me.tb_codtpel.NTSRepositoryItemText = Nothing
    Me.tb_codtpel.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codtpel.OptionsFilter.AllowFilter = False
    '
    'xx_codtpel
    '
    Me.xx_codtpel.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.xx_codtpel.AppearanceCell.Options.UseBackColor = True
    Me.xx_codtpel.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codtpel.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codtpel.AppearanceHeader.Options.UseTextOptions = True
    Me.xx_codtpel.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.xx_codtpel.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.xx_codtpel.Caption = "Descr. elenco"
    Me.xx_codtpel.Enabled = False
    Me.xx_codtpel.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codtpel.Name = "xx_codtpel"
    Me.xx_codtpel.NTSRepositoryComboBox = Nothing
    Me.xx_codtpel.NTSRepositoryItemCheck = Nothing
    Me.xx_codtpel.NTSRepositoryItemMemo = Nothing
    Me.xx_codtpel.NTSRepositoryItemText = Nothing
    Me.xx_codtpel.OptionsColumn.AllowEdit = False
    Me.xx_codtpel.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codtpel.OptionsColumn.ReadOnly = True
    Me.xx_codtpel.OptionsFilter.AllowFilter = False
    '
    'tb_tipacq
    '
    Me.tb_tipacq.AppearanceCell.Options.UseBackColor = True
    Me.tb_tipacq.AppearanceCell.Options.UseTextOptions = True
    Me.tb_tipacq.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_tipacq.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_tipacq.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_tipacq.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_tipacq.Caption = "Tipo oper. IVA"
    Me.tb_tipacq.Enabled = True
    Me.tb_tipacq.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_tipacq.Name = "tb_tipacq"
    Me.tb_tipacq.NTSRepositoryComboBox = Nothing
    Me.tb_tipacq.NTSRepositoryItemCheck = Nothing
    Me.tb_tipacq.NTSRepositoryItemMemo = Nothing
    Me.tb_tipacq.NTSRepositoryItemText = Nothing
    Me.tb_tipacq.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_tipacq.OptionsFilter.AllowFilter = False
    Me.tb_tipacq.Visible = True
    Me.tb_tipacq.VisibleIndex = 5
    '
    'xx_tipacq
    '
    Me.xx_tipacq.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.xx_tipacq.AppearanceCell.Options.UseBackColor = True
    Me.xx_tipacq.AppearanceCell.Options.UseTextOptions = True
    Me.xx_tipacq.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_tipacq.AppearanceHeader.Options.UseTextOptions = True
    Me.xx_tipacq.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.xx_tipacq.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.xx_tipacq.Caption = "Descr. tipo oper. IVA"
    Me.xx_tipacq.Enabled = False
    Me.xx_tipacq.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_tipacq.Name = "xx_tipacq"
    Me.xx_tipacq.NTSRepositoryComboBox = Nothing
    Me.xx_tipacq.NTSRepositoryItemCheck = Nothing
    Me.xx_tipacq.NTSRepositoryItemMemo = Nothing
    Me.xx_tipacq.NTSRepositoryItemText = Nothing
    Me.xx_tipacq.OptionsColumn.AllowEdit = False
    Me.xx_tipacq.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_tipacq.OptionsColumn.ReadOnly = True
    Me.xx_tipacq.OptionsFilter.AllowFilter = False
    Me.xx_tipacq.Visible = True
    Me.xx_tipacq.VisibleIndex = 6
    '
    'tb_monofissa
    '
    Me.tb_monofissa.AppearanceCell.Options.UseBackColor = True
    Me.tb_monofissa.AppearanceCell.Options.UseTextOptions = True
    Me.tb_monofissa.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_monofissa.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_monofissa.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_monofissa.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_monofissa.Caption = "Monofase fissa"
    Me.tb_monofissa.Enabled = True
    Me.tb_monofissa.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_monofissa.Name = "tb_monofissa"
    Me.tb_monofissa.NTSRepositoryComboBox = Nothing
    Me.tb_monofissa.NTSRepositoryItemCheck = Nothing
    Me.tb_monofissa.NTSRepositoryItemMemo = Nothing
    Me.tb_monofissa.NTSRepositoryItemText = Nothing
    Me.tb_monofissa.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_monofissa.OptionsFilter.AllowFilter = False
    '
    'tb_desest
    '
    Me.tb_desest.AppearanceCell.Options.UseBackColor = True
    Me.tb_desest.AppearanceCell.Options.UseTextOptions = True
    Me.tb_desest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_desest.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_desest.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_desest.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_desest.Caption = "Descr. estesa"
    Me.tb_desest.Enabled = True
    Me.tb_desest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_desest.Name = "tb_desest"
    Me.tb_desest.NTSRepositoryComboBox = Nothing
    Me.tb_desest.NTSRepositoryItemCheck = Nothing
    Me.tb_desest.NTSRepositoryItemMemo = Nothing
    Me.tb_desest.NTSRepositoryItemText = Nothing
    Me.tb_desest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_desest.OptionsFilter.AllowFilter = False
    Me.tb_desest.Visible = True
    Me.tb_desest.VisibleIndex = 7
    '
    'tb_percomp
    '
    Me.tb_percomp.AppearanceCell.Options.UseBackColor = True
    Me.tb_percomp.AppearanceCell.Options.UseTextOptions = True
    Me.tb_percomp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_percomp.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_percomp.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_percomp.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_percomp.Caption = "% Compens."
    Me.tb_percomp.Enabled = True
    Me.tb_percomp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_percomp.Name = "tb_percomp"
    Me.tb_percomp.NTSRepositoryComboBox = Nothing
    Me.tb_percomp.NTSRepositoryItemCheck = Nothing
    Me.tb_percomp.NTSRepositoryItemMemo = Nothing
    Me.tb_percomp.NTSRepositoryItemText = Nothing
    Me.tb_percomp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_percomp.OptionsFilter.AllowFilter = False
    Me.tb_percomp.Visible = True
    Me.tb_percomp.VisibleIndex = 8
    '
    'tb_vp11
    '
    Me.tb_vp11.AppearanceCell.Options.UseBackColor = True
    Me.tb_vp11.AppearanceCell.Options.UseTextOptions = True
    Me.tb_vp11.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_vp11.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_vp11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_vp11.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_vp11.Caption = "VP1/1"
    Me.tb_vp11.Enabled = True
    Me.tb_vp11.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_vp11.Name = "tb_vp11"
    Me.tb_vp11.NTSRepositoryComboBox = Nothing
    Me.tb_vp11.NTSRepositoryItemCheck = Nothing
    Me.tb_vp11.NTSRepositoryItemMemo = Nothing
    Me.tb_vp11.NTSRepositoryItemText = Nothing
    Me.tb_vp11.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_vp11.OptionsFilter.AllowFilter = False
    '
    'tb_vp12
    '
    Me.tb_vp12.AppearanceCell.Options.UseBackColor = True
    Me.tb_vp12.AppearanceCell.Options.UseTextOptions = True
    Me.tb_vp12.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_vp12.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_vp12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_vp12.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_vp12.Caption = "VP1/2"
    Me.tb_vp12.Enabled = True
    Me.tb_vp12.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_vp12.Name = "tb_vp12"
    Me.tb_vp12.NTSRepositoryComboBox = Nothing
    Me.tb_vp12.NTSRepositoryItemCheck = Nothing
    Me.tb_vp12.NTSRepositoryItemMemo = Nothing
    Me.tb_vp12.NTSRepositoryItemText = Nothing
    Me.tb_vp12.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_vp12.OptionsFilter.AllowFilter = False
    '
    'tb_vp21
    '
    Me.tb_vp21.AppearanceCell.Options.UseBackColor = True
    Me.tb_vp21.AppearanceCell.Options.UseTextOptions = True
    Me.tb_vp21.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_vp21.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_vp21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_vp21.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_vp21.Caption = "VP2/1"
    Me.tb_vp21.Enabled = True
    Me.tb_vp21.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_vp21.Name = "tb_vp21"
    Me.tb_vp21.NTSRepositoryComboBox = Nothing
    Me.tb_vp21.NTSRepositoryItemCheck = Nothing
    Me.tb_vp21.NTSRepositoryItemMemo = Nothing
    Me.tb_vp21.NTSRepositoryItemText = Nothing
    Me.tb_vp21.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_vp21.OptionsFilter.AllowFilter = False
    '
    'tb_vp22
    '
    Me.tb_vp22.AppearanceCell.Options.UseBackColor = True
    Me.tb_vp22.AppearanceCell.Options.UseTextOptions = True
    Me.tb_vp22.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_vp22.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_vp22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_vp22.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_vp22.Caption = "VP2/2"
    Me.tb_vp22.Enabled = True
    Me.tb_vp22.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_vp22.Name = "tb_vp22"
    Me.tb_vp22.NTSRepositoryComboBox = Nothing
    Me.tb_vp22.NTSRepositoryItemCheck = Nothing
    Me.tb_vp22.NTSRepositoryItemMemo = Nothing
    Me.tb_vp22.NTSRepositoryItemText = Nothing
    Me.tb_vp22.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_vp22.OptionsFilter.AllowFilter = False
    '
    'tb_volaff
    '
    Me.tb_volaff.AppearanceCell.Options.UseBackColor = True
    Me.tb_volaff.AppearanceCell.Options.UseTextOptions = True
    Me.tb_volaff.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_volaff.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_volaff.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_volaff.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_volaff.Caption = "Incr. volume affari"
    Me.tb_volaff.Enabled = True
    Me.tb_volaff.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_volaff.Name = "tb_volaff"
    Me.tb_volaff.NTSRepositoryComboBox = Nothing
    Me.tb_volaff.NTSRepositoryItemCheck = Nothing
    Me.tb_volaff.NTSRepositoryItemMemo = Nothing
    Me.tb_volaff.NTSRepositoryItemText = Nothing
    Me.tb_volaff.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_volaff.OptionsFilter.AllowFilter = False
    Me.tb_volaff.Visible = True
    Me.tb_volaff.VisibleIndex = 9
    '
    'tb_espror
    '
    Me.tb_espror.AppearanceCell.Options.UseBackColor = True
    Me.tb_espror.AppearanceCell.Options.UseTextOptions = True
    Me.tb_espror.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_espror.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_espror.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_espror.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_espror.Caption = "Esenti pro-rata"
    Me.tb_espror.Enabled = True
    Me.tb_espror.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_espror.Name = "tb_espror"
    Me.tb_espror.NTSRepositoryComboBox = Nothing
    Me.tb_espror.NTSRepositoryItemCheck = Nothing
    Me.tb_espror.NTSRepositoryItemMemo = Nothing
    Me.tb_espror.NTSRepositoryItemText = Nothing
    Me.tb_espror.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_espror.OptionsFilter.AllowFilter = False
    Me.tb_espror.Visible = True
    Me.tb_espror.VisibleIndex = 10
    '
    'tb_mvcodiva
    '
    Me.tb_mvcodiva.AppearanceCell.Options.UseBackColor = True
    Me.tb_mvcodiva.AppearanceCell.Options.UseTextOptions = True
    Me.tb_mvcodiva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_mvcodiva.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_mvcodiva.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_mvcodiva.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_mvcodiva.Caption = "Cod. IVA per saldo aliq."
    Me.tb_mvcodiva.Enabled = True
    Me.tb_mvcodiva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_mvcodiva.Name = "tb_mvcodiva"
    Me.tb_mvcodiva.NTSRepositoryComboBox = Nothing
    Me.tb_mvcodiva.NTSRepositoryItemCheck = Nothing
    Me.tb_mvcodiva.NTSRepositoryItemMemo = Nothing
    Me.tb_mvcodiva.NTSRepositoryItemText = Nothing
    Me.tb_mvcodiva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_mvcodiva.OptionsFilter.AllowFilter = False
    Me.tb_mvcodiva.Visible = True
    Me.tb_mvcodiva.VisibleIndex = 11
    '
    'xx_mvcodiva
    '
    Me.xx_mvcodiva.AppearanceCell.Options.UseBackColor = True
    Me.xx_mvcodiva.AppearanceCell.Options.UseTextOptions = True
    Me.xx_mvcodiva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_mvcodiva.AppearanceHeader.Options.UseTextOptions = True
    Me.xx_mvcodiva.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.xx_mvcodiva.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.xx_mvcodiva.Caption = "Descr. IVA per saldo aliq."
    Me.xx_mvcodiva.Enabled = True
    Me.xx_mvcodiva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_mvcodiva.Name = "xx_mvcodiva"
    Me.xx_mvcodiva.NTSRepositoryComboBox = Nothing
    Me.xx_mvcodiva.NTSRepositoryItemCheck = Nothing
    Me.xx_mvcodiva.NTSRepositoryItemMemo = Nothing
    Me.xx_mvcodiva.NTSRepositoryItemText = Nothing
    Me.xx_mvcodiva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_mvcodiva.OptionsFilter.AllowFilter = False
    Me.xx_mvcodiva.Visible = True
    Me.xx_mvcodiva.VisibleIndex = 12
    '
    'tb_tpaggpl
    '
    Me.tb_tpaggpl.AppearanceCell.Options.UseBackColor = True
    Me.tb_tpaggpl.AppearanceCell.Options.UseTextOptions = True
    Me.tb_tpaggpl.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_tpaggpl.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_tpaggpl.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_tpaggpl.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_tpaggpl.Caption = "Tipo agg. plafond"
    Me.tb_tpaggpl.Enabled = True
    Me.tb_tpaggpl.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_tpaggpl.Name = "tb_tpaggpl"
    Me.tb_tpaggpl.NTSRepositoryComboBox = Nothing
    Me.tb_tpaggpl.NTSRepositoryItemCheck = Nothing
    Me.tb_tpaggpl.NTSRepositoryItemMemo = Nothing
    Me.tb_tpaggpl.NTSRepositoryItemText = Nothing
    Me.tb_tpaggpl.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_tpaggpl.OptionsFilter.AllowFilter = False
    Me.tb_tpaggpl.Visible = True
    Me.tb_tpaggpl.VisibleIndex = 13
    '
    'tb_art73t
    '
    Me.tb_art73t.AppearanceCell.Options.UseBackColor = True
    Me.tb_art73t.AppearanceCell.Options.UseTextOptions = True
    Me.tb_art73t.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_art73t.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_art73t.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_art73t.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_art73t.Caption = "Tipo gest. art. 74ter"
    Me.tb_art73t.Enabled = True
    Me.tb_art73t.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_art73t.Name = "tb_art73t"
    Me.tb_art73t.NTSRepositoryComboBox = Nothing
    Me.tb_art73t.NTSRepositoryItemCheck = Nothing
    Me.tb_art73t.NTSRepositoryItemMemo = Nothing
    Me.tb_art73t.NTSRepositoryItemText = Nothing
    Me.tb_art73t.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_art73t.OptionsFilter.AllowFilter = False
    Me.tb_art73t.Visible = True
    Me.tb_art73t.VisibleIndex = 14
    '
    'tb_aliqdetr
    '
    Me.tb_aliqdetr.AppearanceCell.Options.UseBackColor = True
    Me.tb_aliqdetr.AppearanceCell.Options.UseTextOptions = True
    Me.tb_aliqdetr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_aliqdetr.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_aliqdetr.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_aliqdetr.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_aliqdetr.Caption = "Aliq. detraibile"
    Me.tb_aliqdetr.Enabled = True
    Me.tb_aliqdetr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_aliqdetr.Name = "tb_aliqdetr"
    Me.tb_aliqdetr.NTSRepositoryComboBox = Nothing
    Me.tb_aliqdetr.NTSRepositoryItemCheck = Nothing
    Me.tb_aliqdetr.NTSRepositoryItemMemo = Nothing
    Me.tb_aliqdetr.NTSRepositoryItemText = Nothing
    Me.tb_aliqdetr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_aliqdetr.OptionsFilter.AllowFilter = False
    Me.tb_aliqdetr.Visible = True
    Me.tb_aliqdetr.VisibleIndex = 15
    '
    'tb_codindetr
    '
    Me.tb_codindetr.AppearanceCell.Options.UseBackColor = True
    Me.tb_codindetr.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codindetr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codindetr.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_codindetr.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_codindetr.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_codindetr.Caption = "Cod. IVA indetr. corrisp."
    Me.tb_codindetr.Enabled = True
    Me.tb_codindetr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codindetr.Name = "tb_codindetr"
    Me.tb_codindetr.NTSRepositoryComboBox = Nothing
    Me.tb_codindetr.NTSRepositoryItemCheck = Nothing
    Me.tb_codindetr.NTSRepositoryItemMemo = Nothing
    Me.tb_codindetr.NTSRepositoryItemText = Nothing
    Me.tb_codindetr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codindetr.OptionsFilter.AllowFilter = False
    Me.tb_codindetr.Visible = True
    Me.tb_codindetr.VisibleIndex = 16
    '
    'xx_codindetr
    '
    Me.xx_codindetr.AppearanceCell.Options.UseBackColor = True
    Me.xx_codindetr.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codindetr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codindetr.AppearanceHeader.Options.UseTextOptions = True
    Me.xx_codindetr.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.xx_codindetr.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.xx_codindetr.Caption = "Descr. IVA indetr. corrisp."
    Me.xx_codindetr.Enabled = True
    Me.xx_codindetr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codindetr.Name = "xx_codindetr"
    Me.xx_codindetr.NTSRepositoryComboBox = Nothing
    Me.xx_codindetr.NTSRepositoryItemCheck = Nothing
    Me.xx_codindetr.NTSRepositoryItemMemo = Nothing
    Me.xx_codindetr.NTSRepositoryItemText = Nothing
    Me.xx_codindetr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codindetr.OptionsFilter.AllowFilter = False
    Me.xx_codindetr.Visible = True
    Me.xx_codindetr.VisibleIndex = 17
    '
    'tb_coddetr
    '
    Me.tb_coddetr.AppearanceCell.Options.UseBackColor = True
    Me.tb_coddetr.AppearanceCell.Options.UseTextOptions = True
    Me.tb_coddetr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_coddetr.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_coddetr.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_coddetr.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_coddetr.Caption = "Cod. IVA detr. corrisp."
    Me.tb_coddetr.Enabled = True
    Me.tb_coddetr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_coddetr.Name = "tb_coddetr"
    Me.tb_coddetr.NTSRepositoryComboBox = Nothing
    Me.tb_coddetr.NTSRepositoryItemCheck = Nothing
    Me.tb_coddetr.NTSRepositoryItemMemo = Nothing
    Me.tb_coddetr.NTSRepositoryItemText = Nothing
    Me.tb_coddetr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_coddetr.OptionsFilter.AllowFilter = False
    Me.tb_coddetr.Visible = True
    Me.tb_coddetr.VisibleIndex = 18
    '
    'xx_coddetr
    '
    Me.xx_coddetr.AppearanceCell.Options.UseBackColor = True
    Me.xx_coddetr.AppearanceCell.Options.UseTextOptions = True
    Me.xx_coddetr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_coddetr.AppearanceHeader.Options.UseTextOptions = True
    Me.xx_coddetr.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.xx_coddetr.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.xx_coddetr.Caption = "Descr. IVA detr. corrisp."
    Me.xx_coddetr.Enabled = True
    Me.xx_coddetr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_coddetr.Name = "xx_coddetr"
    Me.xx_coddetr.NTSRepositoryComboBox = Nothing
    Me.xx_coddetr.NTSRepositoryItemCheck = Nothing
    Me.xx_coddetr.NTSRepositoryItemMemo = Nothing
    Me.xx_coddetr.NTSRepositoryItemText = Nothing
    Me.xx_coddetr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_coddetr.OptionsFilter.AllowFilter = False
    Me.xx_coddetr.Visible = True
    Me.xx_coddetr.VisibleIndex = 19
    '
    'tb_aliqagr
    '
    Me.tb_aliqagr.AppearanceCell.Options.UseBackColor = True
    Me.tb_aliqagr.AppearanceCell.Options.UseTextOptions = True
    Me.tb_aliqagr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_aliqagr.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_aliqagr.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_aliqagr.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_aliqagr.Caption = "Aliq. agricola"
    Me.tb_aliqagr.Enabled = True
    Me.tb_aliqagr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_aliqagr.Name = "tb_aliqagr"
    Me.tb_aliqagr.NTSRepositoryComboBox = Nothing
    Me.tb_aliqagr.NTSRepositoryItemCheck = Nothing
    Me.tb_aliqagr.NTSRepositoryItemMemo = Nothing
    Me.tb_aliqagr.NTSRepositoryItemText = Nothing
    Me.tb_aliqagr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_aliqagr.OptionsFilter.AllowFilter = False
    Me.tb_aliqagr.Visible = True
    Me.tb_aliqagr.VisibleIndex = 20
    '
    'tb_codagr
    '
    Me.tb_codagr.AppearanceCell.Options.UseBackColor = True
    Me.tb_codagr.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codagr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codagr.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_codagr.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_codagr.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_codagr.Caption = "Cod. IVA agricola corrisp."
    Me.tb_codagr.Enabled = True
    Me.tb_codagr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codagr.Name = "tb_codagr"
    Me.tb_codagr.NTSRepositoryComboBox = Nothing
    Me.tb_codagr.NTSRepositoryItemCheck = Nothing
    Me.tb_codagr.NTSRepositoryItemMemo = Nothing
    Me.tb_codagr.NTSRepositoryItemText = Nothing
    Me.tb_codagr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codagr.OptionsFilter.AllowFilter = False
    Me.tb_codagr.Visible = True
    Me.tb_codagr.VisibleIndex = 21
    '
    'xx_codagr
    '
    Me.xx_codagr.AppearanceCell.Options.UseBackColor = True
    Me.xx_codagr.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codagr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codagr.AppearanceHeader.Options.UseTextOptions = True
    Me.xx_codagr.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.xx_codagr.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.xx_codagr.Caption = "Descr. IVA agricola corrisp."
    Me.xx_codagr.Enabled = True
    Me.xx_codagr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codagr.Name = "xx_codagr"
    Me.xx_codagr.NTSRepositoryComboBox = Nothing
    Me.xx_codagr.NTSRepositoryItemCheck = Nothing
    Me.xx_codagr.NTSRepositoryItemMemo = Nothing
    Me.xx_codagr.NTSRepositoryItemText = Nothing
    Me.xx_codagr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codagr.OptionsFilter.AllowFilter = False
    Me.xx_codagr.Visible = True
    Me.xx_codagr.VisibleIndex = 22
    '
    'tb_art838
    '
    Me.tb_art838.AppearanceCell.Options.UseBackColor = True
    Me.tb_art838.AppearanceCell.Options.UseTextOptions = True
    Me.tb_art838.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_art838.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_art838.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_art838.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_art838.Caption = "Art. 8/38quat/72/34 u.c."
    Me.tb_art838.Enabled = True
    Me.tb_art838.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_art838.Name = "tb_art838"
    Me.tb_art838.NTSRepositoryComboBox = Nothing
    Me.tb_art838.NTSRepositoryItemCheck = Nothing
    Me.tb_art838.NTSRepositoryItemMemo = Nothing
    Me.tb_art838.NTSRepositoryItemText = Nothing
    Me.tb_art838.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_art838.OptionsFilter.AllowFilter = False
    Me.tb_art838.Visible = True
    Me.tb_art838.VisibleIndex = 23
    '
    'tb_codteor
    '
    Me.tb_codteor.AppearanceCell.Options.UseBackColor = True
    Me.tb_codteor.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codteor.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codteor.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_codteor.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_codteor.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_codteor.Caption = "Cod. IVA calcolo teorico"
    Me.tb_codteor.Enabled = True
    Me.tb_codteor.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codteor.Name = "tb_codteor"
    Me.tb_codteor.NTSRepositoryComboBox = Nothing
    Me.tb_codteor.NTSRepositoryItemCheck = Nothing
    Me.tb_codteor.NTSRepositoryItemMemo = Nothing
    Me.tb_codteor.NTSRepositoryItemText = Nothing
    Me.tb_codteor.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codteor.OptionsFilter.AllowFilter = False
    Me.tb_codteor.Visible = True
    Me.tb_codteor.VisibleIndex = 24
    '
    'xx_codteor
    '
    Me.xx_codteor.AppearanceCell.Options.UseBackColor = True
    Me.xx_codteor.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codteor.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codteor.AppearanceHeader.Options.UseTextOptions = True
    Me.xx_codteor.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.xx_codteor.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.xx_codteor.Caption = "Descr. IVA calcolo teorico"
    Me.xx_codteor.Enabled = True
    Me.xx_codteor.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codteor.Name = "xx_codteor"
    Me.xx_codteor.NTSRepositoryComboBox = Nothing
    Me.xx_codteor.NTSRepositoryItemCheck = Nothing
    Me.xx_codteor.NTSRepositoryItemMemo = Nothing
    Me.xx_codteor.NTSRepositoryItemText = Nothing
    Me.xx_codteor.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codteor.OptionsFilter.AllowFilter = False
    Me.xx_codteor.Visible = True
    Me.xx_codteor.VisibleIndex = 25
    '
    'tb_passint
    '
    Me.tb_passint.AppearanceCell.Options.UseBackColor = True
    Me.tb_passint.AppearanceCell.Options.UseTextOptions = True
    Me.tb_passint.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_passint.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_passint.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_passint.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_passint.Caption = "Ammontare passaggi interni"
    Me.tb_passint.Enabled = True
    Me.tb_passint.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_passint.Name = "tb_passint"
    Me.tb_passint.NTSRepositoryComboBox = Nothing
    Me.tb_passint.NTSRepositoryItemCheck = Nothing
    Me.tb_passint.NTSRepositoryItemMemo = Nothing
    Me.tb_passint.NTSRepositoryItemText = Nothing
    Me.tb_passint.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_passint.OptionsFilter.AllowFilter = False
    Me.tb_passint.Visible = True
    Me.tb_passint.VisibleIndex = 26
    '
    'tb_rilanven
    '
    Me.tb_rilanven.AppearanceCell.Options.UseBackColor = True
    Me.tb_rilanven.AppearanceCell.Options.UseTextOptions = True
    Me.tb_rilanven.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_rilanven.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_rilanven.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_rilanven.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_rilanven.Caption = "Cod. rilancio IVA vendite"
    Me.tb_rilanven.Enabled = True
    Me.tb_rilanven.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_rilanven.Name = "tb_rilanven"
    Me.tb_rilanven.NTSRepositoryComboBox = Nothing
    Me.tb_rilanven.NTSRepositoryItemCheck = Nothing
    Me.tb_rilanven.NTSRepositoryItemMemo = Nothing
    Me.tb_rilanven.NTSRepositoryItemText = Nothing
    Me.tb_rilanven.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_rilanven.OptionsFilter.AllowFilter = False
    Me.tb_rilanven.Visible = True
    Me.tb_rilanven.VisibleIndex = 27
    '
    'tb_rilanacq
    '
    Me.tb_rilanacq.AppearanceCell.Options.UseBackColor = True
    Me.tb_rilanacq.AppearanceCell.Options.UseTextOptions = True
    Me.tb_rilanacq.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_rilanacq.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_rilanacq.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_rilanacq.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_rilanacq.Caption = "Tipo operazione"
    Me.tb_rilanacq.Enabled = True
    Me.tb_rilanacq.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_rilanacq.Name = "tb_rilanacq"
    Me.tb_rilanacq.NTSRepositoryComboBox = Nothing
    Me.tb_rilanacq.NTSRepositoryItemCheck = Nothing
    Me.tb_rilanacq.NTSRepositoryItemMemo = Nothing
    Me.tb_rilanacq.NTSRepositoryItemText = Nothing
    Me.tb_rilanacq.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_rilanacq.OptionsFilter.AllowFilter = False
    Me.tb_rilanacq.Visible = True
    Me.tb_rilanacq.VisibleIndex = 28
    '
    'tb_estrdat1
    '
    Me.tb_estrdat1.AppearanceCell.Options.UseBackColor = True
    Me.tb_estrdat1.AppearanceCell.Options.UseTextOptions = True
    Me.tb_estrdat1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_estrdat1.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_estrdat1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_estrdat1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_estrdat1.Caption = "Sigla per estrazione dati (1)"
    Me.tb_estrdat1.Enabled = True
    Me.tb_estrdat1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_estrdat1.Name = "tb_estrdat1"
    Me.tb_estrdat1.NTSRepositoryComboBox = Nothing
    Me.tb_estrdat1.NTSRepositoryItemCheck = Nothing
    Me.tb_estrdat1.NTSRepositoryItemMemo = Nothing
    Me.tb_estrdat1.NTSRepositoryItemText = Nothing
    Me.tb_estrdat1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_estrdat1.OptionsFilter.AllowFilter = False
    Me.tb_estrdat1.Visible = True
    Me.tb_estrdat1.VisibleIndex = 29
    '
    'tb_estrdat2
    '
    Me.tb_estrdat2.AppearanceCell.Options.UseBackColor = True
    Me.tb_estrdat2.AppearanceCell.Options.UseTextOptions = True
    Me.tb_estrdat2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_estrdat2.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_estrdat2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_estrdat2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_estrdat2.Caption = "Elenchi Iva"
    Me.tb_estrdat2.Enabled = True
    Me.tb_estrdat2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_estrdat2.Name = "tb_estrdat2"
    Me.tb_estrdat2.NTSRepositoryComboBox = Nothing
    Me.tb_estrdat2.NTSRepositoryItemCheck = Nothing
    Me.tb_estrdat2.NTSRepositoryItemMemo = Nothing
    Me.tb_estrdat2.NTSRepositoryItemText = Nothing
    Me.tb_estrdat2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_estrdat2.OptionsFilter.AllowFilter = False
    Me.tb_estrdat2.Visible = True
    Me.tb_estrdat2.VisibleIndex = 30
    '
    'tb_codcivaal
    '
    Me.tb_codcivaal.AppearanceCell.Options.UseBackColor = True
    Me.tb_codcivaal.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codcivaal.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codcivaal.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_codcivaal.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_codcivaal.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_codcivaal.Caption = "Cod. alfanumerico"
    Me.tb_codcivaal.Enabled = True
    Me.tb_codcivaal.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codcivaal.Name = "tb_codcivaal"
    Me.tb_codcivaal.NTSRepositoryComboBox = Nothing
    Me.tb_codcivaal.NTSRepositoryItemCheck = Nothing
    Me.tb_codcivaal.NTSRepositoryItemMemo = Nothing
    Me.tb_codcivaal.NTSRepositoryItemText = Nothing
    Me.tb_codcivaal.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codcivaal.OptionsFilter.AllowFilter = False
    Me.tb_codcivaal.Visible = True
    Me.tb_codcivaal.VisibleIndex = 31
    '
    'tb_revcharge
    '
    Me.tb_revcharge.AppearanceCell.Options.UseBackColor = True
    Me.tb_revcharge.AppearanceCell.Options.UseTextOptions = True
    Me.tb_revcharge.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_revcharge.Caption = "Reverse charge acq. Ita"
    Me.tb_revcharge.Enabled = True
    Me.tb_revcharge.FieldName = "tb_revcharge"
    Me.tb_revcharge.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_revcharge.Name = "tb_revcharge"
    Me.tb_revcharge.NTSRepositoryComboBox = Nothing
    Me.tb_revcharge.NTSRepositoryItemCheck = Nothing
    Me.tb_revcharge.NTSRepositoryItemMemo = Nothing
    Me.tb_revcharge.NTSRepositoryItemText = Nothing
    Me.tb_revcharge.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_revcharge.OptionsFilter.AllowFilter = False
    Me.tb_revcharge.Visible = True
    Me.tb_revcharge.VisibleIndex = 32
    '
    'FRM__CIVA
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(688, 404)
    Me.Controls.Add(Me.pnCiva)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.HelpButton = True
    Me.Name = "FRM__CIVA"
    Me.NTSLastControlFocussed = Me.grCiva
    Me.Text = "CODICI IVA"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnCiva, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCiva.ResumeLayout(False)
    CType(Me.grCiva, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvCiva, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

#Region "Form"

  Public Overridable Sub FRM__CIVA_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcCiva.Dispose()
      If Not dsCiva Is Nothing Then dsCiva.Dispose()
    Catch ex As Exception

    End Try
  End Sub

  Public Overridable Sub FRM__CIVA_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__CIVA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      oCleCiva.bSpesometro = CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtCON) OrElse _
                             CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtANB)

      'Predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'Leggo dal database i dati e collego l'NTSBinding
      If oCleCiva.Apri(DittaCorrente, dsCiva) Then
        dcCiva.DataSource = dsCiva.Tables("TABCIVA")
        dsCiva.AcceptChanges()

        grCiva.DataSource = dcCiva

        '-------------------------------------------------
        'Applico le impostazioni personalizzate della form (Ex: CTRL+ALT+F2)
        'questa funzione può essere chiamata quando si vuole,
        'ad esempio dopo aver impostato la ditta, cambiato il tipo documento, ecc ...
        '(Ex: CTRL+ALT+F4)
        'GctlTipoDoc = ""
        GctlSetRoules()

        '--------------------------------------------
        'sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
        If Not oCallParams Is Nothing Then
          If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
            If grvCiva.NTSAllowInsert Then
              grvCiva.NTSNuovo()
            End If
          ElseIf Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) <> "" Then
            For i As Integer = 0 To dcCiva.List.Count - 1
              If CType(dcCiva.Item(i), DataRowView)!tb_codciva.ToString = Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) Then
                dcCiva.Position = i
                Exit For
              End If
            Next
          End If
        End If  'If Not oCallParams Is Nothing Then
      Else
        Me.Close()
      End If

      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione Friendly nasconde, sempre, alcune colonne
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = True Then
        tb_aliqmono.Visible = False
        tb_tipel.Visible = False
        tb_codtpel.Visible = False
        xx_codtpel.Visible = False
        tb_monofissa.Visible = False
        tb_percomp.Visible = False
        tb_vp11.Visible = False
        tb_vp12.Visible = False
        tb_vp21.Visible = False
        tb_vp22.Visible = False
        tb_mvcodiva.Visible = False
        xx_mvcodiva.Visible = False
        tb_tpaggpl.Visible = False
        tb_art73t.Visible = False
        tb_aliqdetr.Visible = False
        tb_codindetr.Visible = False
        xx_codindetr.Visible = False
        tb_coddetr.Visible = False
        xx_coddetr.Visible = False
        tb_aliqagr.Visible = False
        tb_codagr.Visible = False
        xx_codagr.Visible = False
        tb_art838.Visible = False
        tb_codteor.Visible = False
        xx_codteor.Visible = False
        tb_passint.Visible = False
        tb_rilanven.Visible = False
        tb_rilanacq.Visible = False
        tb_estrdat1.Visible = False
        tb_codcivaal.Visible = False
        tb_volaff.Visible = False
        tb_espror.Visible = False
        tb_reparto.Visible = False
        xx_reparto.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

#Region "Griglia"

  Public Overridable Sub grvCiva_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvCiva.NTSFocusedRowChanged
    Try
      'Se il codice magazzino è diverso da 0 blocco la colonna, diversamente la rendo editabile
      If NTSCInt(grvCiva.GetFocusedRowCellValue(tb_codciva).ToString.Trim) <> 0 Then
        tb_codciva.Enabled = False
      Else
        GctlSetVisEnab(tb_codciva, False)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvCiva_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvCiva.NTSBeforeRowUpdate
    Try
      If Not Salva() Then
        'Rimango sulla riga su cui sono
        e.Allow = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tb_codtpel_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles tb_codtpel.NTSZoomGest
    Dim oCZoo As New CLE__CZOO
    Dim bNuovo As Boolean = True
    Dim oTmp As New Control
    Dim oPar As New CLE__CLDP
    Try
      Me.ValidaLastControl()

      If e.TipoEvento = "OPEN" Then
        bNuovo = False
        oPar.strParam = "APRI;"
      Else
        oPar.strParam = "NUOV;"
      End If
      oPar.strParam += NTSCStr(grvCiva.NTSGetCurrentDataRow!tb_tipel) & ";"
      oTmp.Text = NTSCStr(grvCiva.NTSGetCurrentDataRow!tb_codtpel)
      oPar.strParam += oTmp.Text

      NTSZOOM.OpenChildGest(oTmp, "ZOOMTABSMEL", DittaCorrente, bNuovo, oPar)

      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo
      grCiva.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try

  End Sub

#End Region

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttTipiva As New DataTable()
    Dim dttTipel As New DataTable()
    Dim dttTpaggpl As New DataTable()
    Dim dttArt73t As New DataTable()
    Dim dttAliqdetr As New DataTable()
    Dim dttEliva As New DataTable
    Dim dttRilanacq As New DataTable
    Dim dttRevcharge As New DataTable

    Try
      'Carico le immagini della Toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbDuplica.GlyphPath = (oApp.ChildImageDir & "\duplica.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'Non gestisco l'errore: 
        'se non c'è una immagine prendo quella a design
      End Try
      tlbMain.NTSSetToolTip()

      'E' una sorta di CaricaCombo inserita qui...
      dttTipiva.Columns.Add("cod", GetType(Short))
      dttTipiva.Columns.Add("val", GetType(String))
      dttTipiva.Rows.Add(New Object() {1, "Op. imponibili"})
      dttTipiva.Rows.Add(New Object() {2, "Op. esenti/non imponib."})
      dttTipiva.Rows.Add(New Object() {3, "Acq. art. 8 2° comma"})
      dttTipiva.Rows.Add(New Object() {4, "Fuori campo IVA"})
      'dttTipiva.Rows.Add(New Object() {5, "********"})
      dttTipiva.Rows.Add(New Object() {6, "********"})
      dttTipiva.Rows.Add(New Object() {7, "********"})
      dttTipiva.Rows.Add(New Object() {8, "Da ventilare"})
      dttTipiva.AcceptChanges()

      dttTipel.Columns.Add("cod", GetType(String))
      dttTipel.Columns.Add("val", GetType(String))
      dttTipel.Rows.Add(New Object() {"A", "Acquisti"})
      dttTipel.Rows.Add(New Object() {"V", "Vendite"})
      dttTipel.AcceptChanges()

      dttTpaggpl.Columns.Add("cod", GetType(String))
      dttTpaggpl.Columns.Add("val", GetType(String))
      dttTpaggpl.Rows.Add(New Object() {"N", "No"})
      dttTpaggpl.Rows.Add(New Object() {"I", "Acq. interno"})
      dttTpaggpl.Rows.Add(New Object() {"E", "Acq. estero"})
      dttTpaggpl.Rows.Add(New Object() {"V", "Vendite"})
      dttTpaggpl.AcceptChanges()

      dttArt73t.Columns.Add("cod", GetType(String))
      dttArt73t.Columns.Add("val", GetType(String))
      dttArt73t.Rows.Add(New Object() {"N", "No"})
      dttArt73t.Rows.Add(New Object() {"1", "Acq./Vend. UE"})
      dttArt73t.Rows.Add(New Object() {"2", "Acq./Vend. no UE"})
      dttArt73t.Rows.Add(New Object() {"3", "Corr. viaggi misti UE/no UE"})
      dttArt73t.Rows.Add(New Object() {"4", "Corr. viaggi misti UE"})
      dttArt73t.Rows.Add(New Object() {"5", "Corr. viaggi misti no UE"})
      dttArt73t.AcceptChanges()

      dttAliqdetr.Columns.Add("cod", GetType(String))
      dttAliqdetr.Columns.Add("val", GetType(String))
      dttAliqdetr.Rows.Add(New Object() {"S", "Si"})
      dttAliqdetr.Rows.Add(New Object() {"N", "No"})
      dttAliqdetr.Rows.Add(New Object() {"P", "Parzialmente"})
      dttAliqdetr.AcceptChanges()


      dttEliva.Columns.Add("cod", GetType(String))
      dttEliva.Columns.Add("val", GetType(String))
      dttEliva.Rows.Add(New Object() {" ", "Vedi tipo Iva"})
      dttEliva.Rows.Add(New Object() {"1", "Operazioni imponibili"})
      dttEliva.Rows.Add(New Object() {"2", "Operazioni non imponibili"})
      dttEliva.Rows.Add(New Object() {"3", "Operazioni esenti"})
      dttEliva.Rows.Add(New Object() {"4", "Operazioni imponibili con Iva non esposta in fattura"})
      dttEliva.Rows.Add(New Object() {"5", "Imponibile con Iva a margine"})
      dttEliva.Rows.Add(New Object() {"6", "Escluso da Elenchi Iva"})
      dttEliva.AcceptChanges()

      dttRilanacq.Columns.Add("cod", GetType(String))
      dttRilanacq.Columns.Add("val", GetType(String))
      dttRilanacq.Rows.Add(New Object() {" ", "(Non definito)"})
      dttRilanacq.Rows.Add(New Object() {"M", "Merci"})
      dttRilanacq.Rows.Add(New Object() {"S", "Servizi"})
      dttRilanacq.Rows.Add(New Object() {"A", "Noleggio/Leasing Autovettura"})
      dttRilanacq.Rows.Add(New Object() {"B", "Noleggio/Leasing Caravan"})
      dttRilanacq.Rows.Add(New Object() {"C", "Noleggio/Leasing Altri veicoli"})
      dttRilanacq.Rows.Add(New Object() {"D", "Noleggio/Leasing Unità da diporto"})
      dttRilanacq.Rows.Add(New Object() {"E", "Noleggio/Leasing Aeromobili"})
      dttRilanacq.AcceptChanges()

      dttRevcharge.Columns.Add("cod", GetType(String))
      dttRevcharge.Columns.Add("val", GetType(String))
      dttRevcharge.Rows.Add(New Object() {"N", "No"})
      dttRevcharge.Rows.Add(New Object() {"A", "Acq. da non resid. Rapp.fisc."})
      dttRevcharge.Rows.Add(New Object() {"D", "Quote gas"})
      dttRevcharge.Rows.Add(New Object() {"E", "Certificati gas e energia"})
      dttRevcharge.Rows.Add(New Object() {"G", "Gas e energia da rivendita"})
      dttRevcharge.Rows.Add(New Object() {"F", "Fabbricati compravendita"})
      dttRevcharge.Rows.Add(New Object() {"H", "Hi-tech telefonia"})
      dttRevcharge.Rows.Add(New Object() {"I", "Hi-tech computer"})
      dttRevcharge.Rows.Add(New Object() {"M", "Manutenzione edifici"})
      dttRevcharge.Rows.Add(New Object() {"O", "Oro da investimento"})
      dttRevcharge.Rows.Add(New Object() {"Q", "Oro industriale"})
      dttRevcharge.Rows.Add(New Object() {"P", "Pallet"})
      dttRevcharge.Rows.Add(New Object() {"R", "Rottami ferrosi"})
      dttRevcharge.Rows.Add(New Object() {"S", "Subappalto"})
      dttRevcharge.Rows.Add(New Object() {"T", "Supermercati"})
      dttRevcharge.Rows.Add(New Object() {"V", "Prod. lapidei"})
      dttRevcharge.AcceptChanges()


      grvCiva.NTSSetParam(oMenu, oApp.Tr(Me, 128230023372468841, "Griglia Codici IVA"))

      tb_codciva.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023372625014, "Cod. IVA"), tabciva)
      tb_desciva.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023372781187, "Descr. IVA"), 20, False)
      tb_tipiva.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128230023372937360, "Tipo IVA"), dttTipiva, "val", "cod")
      tb_aliq.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023373093533, "Aliquota"), oApp.FormatSconti, 6, 0, 100)
      tb_inded.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023373249706, "% Inded."), oApp.FormatSconti, 6, 0, 100)
      tb_aliqmono.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023373405879, "Aliq. monofase"), oApp.FormatSconti, 6, 0, 100)
      tb_tipel.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128230023373562052, "Tipo elenco"), dttTipel, "val", "cod")
      tb_codtpel.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023373718225, "N° elenco"), tabsmel)
      xx_codtpel.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023373874398, "Descr. elenco"), 20)
      tb_tipacq.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128230023374030571, "Tipo oper. IVA"), tabtacq, False)
      xx_tipacq.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023374186744, "Descr. tipo oper. IVA"), 30)
      tb_monofissa.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023374342917, "Monofase fissa"), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      tb_desest.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023374499090, "Descr. estesa"), 255, True)
      tb_percomp.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023374655263, "% Compens."), oApp.FormatSconti, 6, 0, 100)
      tb_vp11.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023374811436, "VP1/1"), "S", "N")
      tb_vp12.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023374967609, "VP1/2"), "S", "N")
      tb_vp21.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023375123782, "VP2/1"), "S", "N")
      tb_vp22.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023375279955, "VP2/2"), "S", "N")
      tb_volaff.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023375436128, "Incr. volume affari"), "S", "N")
      tb_espror.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023375592301, "Esenti pro-rata"), "S", "N")
      tb_mvcodiva.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023375748474, "Cod. IVA per saldo aliquota"), tabciva)
      xx_mvcodiva.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023375904647, "Descr. IVA per saldo aliquota"), 20)
      tb_tpaggpl.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128230023376060820, "Tipo agg. plafond"), dttTpaggpl, "val", "cod")
      tb_art73t.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128230023376216993, "Tipo gest. art. 74ter"), dttArt73t, "val", "cod")
      tb_aliqdetr.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128230023376373166, "Aliq. detraibile"), dttAliqdetr, "val", "cod")
      tb_codindetr.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023376529339, "Cod. IVA indetraibile corrisp."), tabciva)
      xx_codindetr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023376685512, "Descr. IVA indetraibile corrisp."), 20)
      tb_coddetr.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023376841685, "Cod. IVA detraibile corrispondente"), tabciva)
      xx_coddetr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023376997858, "Descr. IVA detraibile corrispondente"), 20)
      tb_aliqagr.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023377154031, "Aliq. agricola"), "S", "N")
      tb_codagr.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023377310204, "Cod. IVA agricola corrisp."), tabciva)
      xx_codagr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023377466377, "Descr. IVA agricola corrisp."), 20)
      tb_art838.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023377622550, "Art. 8/38quat/72/34 u.c."), "S", "N")
      tb_codteor.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023377778723, "Cod. IVA calcolo teorico"), tabciva)
      xx_codteor.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023377934896, "Descr. IVA calcolo teorico"), 20)
      tb_passint.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023378091069, "Ammontare passaggi interni"), "S", "N")
      tb_rilanven.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023378247242, "Cod. rilancio IVA vendite"), 4, True)
      tb_rilanacq.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129467512951049618, "Tipo operazione"), dttRilanacq, "val", "cod")
      tb_estrdat1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023378559588, "Sigla per estraz. dati (1)"), 4, True)
      If oCleCiva.bSpesometro Then
        tb_estrdat2.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128230023378715761, "Elenchi Iva"), dttEliva, "val", "cod")
      Else
        tb_estrdat2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130985335788392789, "Sigla per estrazione dati (2)"), 4, True)
        tb_estrdat2.Caption = oApp.Tr(Me, 130985336158421526, "Sigla per estrazione dati (2)")
      End If


      tb_codcivaal.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023378871934, "Cod. alfanumerico"), 4, True)
      tb_revcharge.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129774897580588000, "Reverse charge acquisti Italia"), dttRevcharge, "val", "cod")
      tb_reparto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128788388068404299, "Reparto Scontrino"), "0", 3, 0, 999)

      'In questo modo non faccio apparire lo zoom a fianco del codice magazzino (lo zoom era stato abilitato dalla NTSSetParamTabe)
      tb_codciva.NTSSetParamZoom("")
      tb_codtpel.NTSSetParamZoom("ZOOMTABSMEL")
      tb_reparto.NTSSetParamZoom("ZOOMTABREAR")

      tb_codciva.NTSSetRichiesto()
      tb_desciva.NTSSetRichiesto()

      xx_mvcodiva.Enabled = False
      xx_codagr.Enabled = False

      'Chiamo lo script per inizializzare i controlli caricati con Source Extender
      NTSScriptExec("InitControls", Me, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Public Overridable Function Salva() As Boolean
    Try

      Me.ValidaLastControl()      'Valido l'ultimo controllo che ha il focus

      Select Case grvCiva.NTSSalvaRigaCorrente(dcCiva, oCleCiva.RecordIsChanged, False)
        Case System.Windows.Forms.DialogResult.Yes
          'Salvo
          '-------------------------------------------------
          'Controllo che i campi abbiano un valore diverso da quello 
          'impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleCiva.Salva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'Ripristino
          tlbRipristina_ItemClick(Nothing, Nothing)
        Case System.Windows.Forms.DialogResult.Cancel
          'Non posso fare nulla
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          'La riga non ha subito modifiche
      End Select

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim frmSrpt As FRM__SRPT = Nothing
    Dim oPar As CLE__CLDP
    Dim strNomeRpt As String = ""
    Dim strTitleRpt As String = ""
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim i As Integer

    Try

      If Not Salva() Then Return

      If tb_aliqmono.Visible = False Then
        frmSrpt = CType(NTSNewFormModal("FRM__SRPT"), FRM__SRPT)

        oPar = New CLE__CLDP
        frmSrpt.Init(oMenu, oPar, DittaCorrente)
        frmSrpt.ShowDialog()
        If oPar.bPar1 = True Then Return
        If oPar.dPar1 = 1 Then
          strNomeRpt = "BS--CIV2.RPT"
        Else
          strNomeRpt = "BS--CIVA.RPT"
        End If
        strTitleRpt = "Codici IVA"
      Else
        strNomeRpt = "BS--CIV1.RPT"
        strTitleRpt = "Codici IVA (elenchi I/E RSM)"
      End If

      '--------------------------------------------------
      'Preparo il motore di stampa
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BS--CIVA", "Reports1", " ", 0, nDestin, strNomeRpt, False, strTitleRpt, False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'Lancio tutti gli eventuali reports (gestisce già il multireport)
      For i = 1 To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i))))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmSrpt Is Nothing Then frmSrpt.Dispose()
      frmSrpt = Nothing
    End Try
  End Sub


#Region "Toolbar"

  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvCiva.NTSNuovo()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDuplica_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDuplica.ItemClick
    Dim frmDuar As FRM__DUAR = Nothing
    Dim strCodOld As String = ""
    Try
      If Not Salva() Then Return

      'test su riga valida
      If grvCiva.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130723482390952496, "Posizionarsi su un record valido"))
        Return
      Else
        strCodOld = NTSCStr(grvCiva.NTSGetCurrentDataRow()!tb_codciva)
      End If

      frmDuar = CType(NTSNewFormModal("FRM__DUAR"), FRM__DUAR)
      frmDuar.Init(oMenu, Nothing, DittaCorrente)
      frmDuar.oCleCiva = oCleCiva
      frmDuar.ShowDialog()
      If frmDuar.bOk = False Or frmDuar.strCodDupl = "" Then
        'annullato
        Return
      End If

      If Not oCleCiva.Duplica(frmDuar.strCodDupl, strCodOld) Then
        Return
      End If

      dcCiva.MoveLast()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmDuar Is Nothing Then frmDuar.Dispose()
      frmDuar = Nothing
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
    Try
      If grvCiva.NTSGetCurrentDataRow Is Nothing Then Return

      If oCleCiva.TestPreCancella(grvCiva.NTSGetCurrentDataRow) Then
        If grvCiva.NTSDeleteRigaCorrente(dcCiva, True) Then oCleCiva.Salva(True)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If grvCiva.NTSRipristinaRigaCorrenteBefore(dcCiva, CType(IIf(sender Is Nothing, False, True), Boolean)) Then
        oCleCiva.Ripristina(dcCiva.Position, dcCiva.Filter)
        grvCiva.NTSRipristinaRigaCorrenteAfter()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim oParam As New CLE__PATB

    Try
      If grvCiva.FocusedColumn.Name = "tb_codtpel" Then
        '--------------------------------------------
        'zoom tabsmel
        SetFastZoom(NTSCStr(grvCiva.EditingValue), oParam)    'abilito la gestione dello zoom veloce
        oParam.bVisGriglia = True
        oParam.strTipo = NTSCStr(grvCiva.GetFocusedRowCellValue("tb_tipel"))
        NTSZOOM.strIn = NTSCStr(grvCiva.EditingValue)
        NTSZOOM.ZoomStrIn("ZOOMTABSMEL", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvCiva.EditingValue) Then grvCiva.SetFocusedValue(NTSZOOM.strIn)

      Else
        NTSCallStandardZoom()
      End If

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

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
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

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    Try
      oMenu.ReportImposta(Me)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

End Class
