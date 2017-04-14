Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMPRCAGE_F
#Region "Moduli"
  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = bsModExtAll
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
  Public oCleCage As CLEPRCAGE
  Public oCallParams As CLE__CLDP
  Public dsCage As DataSet
  Public dcCage As New BindingSource
  Public bNuovo As Boolean
  Private components As System.ComponentModel.IContainer

  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
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
  Public WithEvents pnAgente As NTSInformatica.NTSPanel
  Public WithEvents pnPerprovv As NTSInformatica.NTSPanel
  Public WithEvents lbDesAnag As NTSInformatica.NTSLabel
  Public WithEvents cbTb_tippro As NTSInformatica.NTSComboBox
  Public WithEvents cbTb_tiptab As NTSInformatica.NTSComboBox
  Public WithEvents edTb_giorni As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_codforn As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_descage As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_codcage As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDecor As NTSInformatica.NTSLabel
  Public WithEvents lbNominativo As NTSInformatica.NTSLabel
  Public WithEvents lbMod As NTSInformatica.NTSLabel
  Public WithEvents lbPerc As NTSInformatica.NTSLabel
  Public WithEvents lbCodanag As NTSInformatica.NTSLabel
  Public WithEvents lbCodcage As NTSInformatica.NTSLabel
  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents ceProvv As NTSInformatica.NTSXXPROV
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNPRCAGE", "BEPRCAGE", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128626743371406250, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleCage = CType(oTmp, CLEPRCAGE)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNPRCAGE", strRemoteServer, strRemotePort)
    AddHandler oCleCage.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleCage.Init(oApp, oScript, oMenu.oCleComm, "TABCAGE", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)

    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      cbTb_tippro.NTSSetParam(oApp.Tr(Me, 128726175105141790, "Mod. corresponsione"))
      cbTb_tiptab.NTSSetParam(oApp.Tr(Me, 128726175105297556, "Percentuali riferite a"))
      edTb_giorni.NTSSetParam(oMenu, oApp.Tr(Me, 128726175105609088, "Decorrenza matur. provv. (gg)"), "0", 3, 0, 999)
      edTb_codforn.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128726175105920620, "Codice anagrafico"), tabanagraf)
      edTb_descage.NTSSetParam(oMenu, oApp.Tr(Me, 128726175106076386, "Nominativo"), 50)
      edTb_descage.NTSForzaVisZoom = True
      edTb_codcage.NTSSetParam(oMenu, oApp.Tr(Me, 128726175106232152, "Codice Agente"), "0", 4, 0, 9999)

      ceProvv.NTSSetParam(oMenu, "Provvigioni", "BNPRCAGE", Nothing)
      ceProvv.tlbProvEsci.Enabled = False
      ApriGriglia(True)

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
  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMPRCAGE_F))
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
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.ceProvv = New NTSInformatica.NTSXXPROV
    Me.pnPerprovv = New NTSInformatica.NTSPanel
    Me.pnAgente = New NTSInformatica.NTSPanel
    Me.lbDesAnag = New NTSInformatica.NTSLabel
    Me.cbTb_tippro = New NTSInformatica.NTSComboBox
    Me.cbTb_tiptab = New NTSInformatica.NTSComboBox
    Me.edTb_giorni = New NTSInformatica.NTSTextBoxNum
    Me.edTb_codforn = New NTSInformatica.NTSTextBoxNum
    Me.edTb_descage = New NTSInformatica.NTSTextBoxStr
    Me.edTb_codcage = New NTSInformatica.NTSTextBoxNum
    Me.lbDecor = New NTSInformatica.NTSLabel
    Me.lbNominativo = New NTSInformatica.NTSLabel
    Me.lbMod = New NTSInformatica.NTSLabel
    Me.lbPerc = New NTSInformatica.NTSLabel
    Me.lbCodanag = New NTSInformatica.NTSLabel
    Me.lbCodcage = New NTSInformatica.NTSLabel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnPerprovv, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPerprovv.SuspendLayout()
    CType(Me.pnAgente, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAgente.SuspendLayout()
    CType(Me.cbTb_tippro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_tiptab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_giorni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codforn.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_descage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codcage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbNuovo, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbUltimo, Me.tlbZoom})
    Me.NtsBarManager1.MaxItemId = 27
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbNuovo.Id = 18
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
    Me.tlbZoom.Id = 26
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbPrimo
    '
    Me.tlbPrimo.Caption = "Primo"
    Me.tlbPrimo.Glyph = CType(resources.GetObject("tlbPrimo.Glyph"), System.Drawing.Image)
    Me.tlbPrimo.Id = 20
    Me.tlbPrimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P))
    Me.tlbPrimo.Name = "tlbPrimo"
    Me.tlbPrimo.Visible = True
    '
    'tlbPrecedente
    '
    Me.tlbPrecedente.Caption = "Precedente"
    Me.tlbPrecedente.Glyph = CType(resources.GetObject("tlbPrecedente.Glyph"), System.Drawing.Image)
    Me.tlbPrecedente.Id = 21
    Me.tlbPrecedente.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
    Me.tlbPrecedente.Name = "tlbPrecedente"
    Me.tlbPrecedente.Visible = True
    '
    'tlbSuccessivo
    '
    Me.tlbSuccessivo.Caption = "Sucessivo"
    Me.tlbSuccessivo.Glyph = CType(resources.GetObject("tlbSuccessivo.Glyph"), System.Drawing.Image)
    Me.tlbSuccessivo.Id = 22
    Me.tlbSuccessivo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
    Me.tlbSuccessivo.Name = "tlbSuccessivo"
    Me.tlbSuccessivo.Visible = True
    '
    'tlbUltimo
    '
    Me.tlbUltimo.Caption = "Ultimo"
    Me.tlbUltimo.Glyph = CType(resources.GetObject("tlbUltimo.Glyph"), System.Drawing.Image)
    Me.tlbUltimo.Id = 23
    Me.tlbUltimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
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
    'ceProvv
    '
    Me.ceProvv.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ceProvv.Location = New System.Drawing.Point(0, 0)
    Me.ceProvv.MinimumSize = New System.Drawing.Size(504, 294)
    Me.ceProvv.Name = "ceProvv"
    Me.ceProvv.PerClasseArt = 0
    Me.ceProvv.PerClasseCli = 0
    Me.ceProvv.PerCodart = ""
    Me.ceProvv.PerCodartRoot = ""
    Me.ceProvv.PerCodcage = 0
    Me.ceProvv.PerConto = 0
    Me.ceProvv.Size = New System.Drawing.Size(556, 299)
    Me.ceProvv.strNomeCampo = ""
    Me.ceProvv.TabIndex = 1
    Me.ceProvv.TipoProvv = 0
    '
    'pnPerprovv
    '
    Me.pnPerprovv.AllowDrop = True
    Me.pnPerprovv.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPerprovv.Appearance.Options.UseBackColor = True
    Me.pnPerprovv.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPerprovv.Controls.Add(Me.ceProvv)
    Me.pnPerprovv.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPerprovv.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPerprovv.Location = New System.Drawing.Point(0, 148)
    Me.pnPerprovv.Name = "pnPerprovv"
    Me.pnPerprovv.NTSActiveTrasparency = True
    Me.pnPerprovv.Size = New System.Drawing.Size(556, 299)
    Me.pnPerprovv.TabIndex = 0
    Me.pnPerprovv.Text = "NtsPanel1"
    '
    'pnAgente
    '
    Me.pnAgente.AllowDrop = True
    Me.pnAgente.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAgente.Appearance.Options.UseBackColor = True
    Me.pnAgente.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAgente.Controls.Add(Me.lbDesAnag)
    Me.pnAgente.Controls.Add(Me.cbTb_tippro)
    Me.pnAgente.Controls.Add(Me.cbTb_tiptab)
    Me.pnAgente.Controls.Add(Me.edTb_giorni)
    Me.pnAgente.Controls.Add(Me.edTb_codforn)
    Me.pnAgente.Controls.Add(Me.edTb_descage)
    Me.pnAgente.Controls.Add(Me.edTb_codcage)
    Me.pnAgente.Controls.Add(Me.lbDecor)
    Me.pnAgente.Controls.Add(Me.lbNominativo)
    Me.pnAgente.Controls.Add(Me.lbMod)
    Me.pnAgente.Controls.Add(Me.lbPerc)
    Me.pnAgente.Controls.Add(Me.lbCodanag)
    Me.pnAgente.Controls.Add(Me.lbCodcage)
    Me.pnAgente.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnAgente.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnAgente.Location = New System.Drawing.Point(0, 30)
    Me.pnAgente.Name = "pnAgente"
    Me.pnAgente.NTSActiveTrasparency = True
    Me.pnAgente.Size = New System.Drawing.Size(556, 118)
    Me.pnAgente.TabIndex = 5
    Me.pnAgente.Text = "NtsPanel1"
    '
    'lbDesAnag
    '
    Me.lbDesAnag.BackColor = System.Drawing.Color.Transparent
    Me.lbDesAnag.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesAnag.Location = New System.Drawing.Point(237, 49)
    Me.lbDesAnag.Name = "lbDesAnag"
    Me.lbDesAnag.NTSDbField = ""
    Me.lbDesAnag.Size = New System.Drawing.Size(299, 20)
    Me.lbDesAnag.TabIndex = 16
    Me.lbDesAnag.Tooltip = ""
    Me.lbDesAnag.UseMnemonic = False
    '
    'cbTb_tippro
    '
    Me.cbTb_tippro.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_tippro.DataSource = Nothing
    Me.cbTb_tippro.DisplayMember = ""
    Me.cbTb_tippro.Location = New System.Drawing.Point(131, 93)
    Me.cbTb_tippro.Name = "cbTb_tippro"
    Me.cbTb_tippro.NTSDbField = ""
    Me.cbTb_tippro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_tippro.Properties.DropDownRows = 30
    Me.cbTb_tippro.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_tippro.SelectedValue = ""
    Me.cbTb_tippro.Size = New System.Drawing.Size(144, 20)
    Me.cbTb_tippro.TabIndex = 15
    Me.cbTb_tippro.ValueMember = ""
    '
    'cbTb_tiptab
    '
    Me.cbTb_tiptab.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_tiptab.DataSource = Nothing
    Me.cbTb_tiptab.DisplayMember = ""
    Me.cbTb_tiptab.Location = New System.Drawing.Point(131, 71)
    Me.cbTb_tiptab.Name = "cbTb_tiptab"
    Me.cbTb_tiptab.NTSDbField = ""
    Me.cbTb_tiptab.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_tiptab.Properties.DropDownRows = 30
    Me.cbTb_tiptab.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_tiptab.SelectedValue = ""
    Me.cbTb_tiptab.Size = New System.Drawing.Size(144, 20)
    Me.cbTb_tiptab.TabIndex = 14
    Me.cbTb_tiptab.ValueMember = ""
    '
    'edTb_giorni
    '
    Me.edTb_giorni.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edTb_giorni.EditValue = "0"
    Me.edTb_giorni.Location = New System.Drawing.Point(471, 93)
    Me.edTb_giorni.Name = "edTb_giorni"
    Me.edTb_giorni.NTSDbField = ""
    Me.edTb_giorni.NTSFormat = "0"
    Me.edTb_giorni.NTSForzaVisZoom = False
    Me.edTb_giorni.NTSOldValue = ""
    Me.edTb_giorni.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_giorni.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_giorni.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_giorni.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_giorni.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_giorni.Properties.MaxLength = 65536
    Me.edTb_giorni.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_giorni.Size = New System.Drawing.Size(65, 20)
    Me.edTb_giorni.TabIndex = 12
    '
    'edTb_codforn
    '
    Me.edTb_codforn.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codforn.EditValue = "0"
    Me.edTb_codforn.Location = New System.Drawing.Point(131, 49)
    Me.edTb_codforn.Name = "edTb_codforn"
    Me.edTb_codforn.NTSDbField = ""
    Me.edTb_codforn.NTSFormat = "0"
    Me.edTb_codforn.NTSForzaVisZoom = False
    Me.edTb_codforn.NTSOldValue = ""
    Me.edTb_codforn.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codforn.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codforn.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codforn.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codforn.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codforn.Properties.MaxLength = 65536
    Me.edTb_codforn.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codforn.Size = New System.Drawing.Size(100, 20)
    Me.edTb_codforn.TabIndex = 10
    '
    'edTb_descage
    '
    Me.edTb_descage.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_descage.Location = New System.Drawing.Point(131, 27)
    Me.edTb_descage.Name = "edTb_descage"
    Me.edTb_descage.NTSDbField = ""
    Me.edTb_descage.NTSForzaVisZoom = False
    Me.edTb_descage.NTSOldValue = ""
    Me.edTb_descage.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_descage.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_descage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_descage.Properties.MaxLength = 65536
    Me.edTb_descage.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_descage.Size = New System.Drawing.Size(236, 20)
    Me.edTb_descage.TabIndex = 9
    '
    'edTb_codcage
    '
    Me.edTb_codcage.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codcage.EditValue = "0"
    Me.edTb_codcage.Enabled = False
    Me.edTb_codcage.Location = New System.Drawing.Point(131, 5)
    Me.edTb_codcage.Name = "edTb_codcage"
    Me.edTb_codcage.NTSDbField = ""
    Me.edTb_codcage.NTSFormat = "0"
    Me.edTb_codcage.NTSForzaVisZoom = False
    Me.edTb_codcage.NTSOldValue = ""
    Me.edTb_codcage.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codcage.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codcage.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codcage.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codcage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codcage.Properties.MaxLength = 65536
    Me.edTb_codcage.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codcage.Size = New System.Drawing.Size(57, 20)
    Me.edTb_codcage.TabIndex = 8
    '
    'lbDecor
    '
    Me.lbDecor.AutoSize = True
    Me.lbDecor.BackColor = System.Drawing.Color.Transparent
    Me.lbDecor.Location = New System.Drawing.Point(316, 96)
    Me.lbDecor.Name = "lbDecor"
    Me.lbDecor.NTSDbField = ""
    Me.lbDecor.Size = New System.Drawing.Size(149, 13)
    Me.lbDecor.TabIndex = 6
    Me.lbDecor.Text = "Decorrenza matur.provv.(gg)"
    Me.lbDecor.Tooltip = ""
    Me.lbDecor.UseMnemonic = False
    '
    'lbNominativo
    '
    Me.lbNominativo.AutoSize = True
    Me.lbNominativo.BackColor = System.Drawing.Color.Transparent
    Me.lbNominativo.Location = New System.Drawing.Point(12, 30)
    Me.lbNominativo.Name = "lbNominativo"
    Me.lbNominativo.NTSDbField = ""
    Me.lbNominativo.Size = New System.Drawing.Size(60, 13)
    Me.lbNominativo.TabIndex = 5
    Me.lbNominativo.Text = "Nominativo"
    Me.lbNominativo.Tooltip = ""
    Me.lbNominativo.UseMnemonic = False
    '
    'lbMod
    '
    Me.lbMod.AutoSize = True
    Me.lbMod.BackColor = System.Drawing.Color.Transparent
    Me.lbMod.Location = New System.Drawing.Point(12, 96)
    Me.lbMod.Name = "lbMod"
    Me.lbMod.NTSDbField = ""
    Me.lbMod.Size = New System.Drawing.Size(107, 13)
    Me.lbMod.TabIndex = 3
    Me.lbMod.Text = "Mod. corresponsione"
    Me.lbMod.Tooltip = ""
    Me.lbMod.UseMnemonic = False
    '
    'lbPerc
    '
    Me.lbPerc.AutoSize = True
    Me.lbPerc.BackColor = System.Drawing.Color.Transparent
    Me.lbPerc.Location = New System.Drawing.Point(11, 74)
    Me.lbPerc.Name = "lbPerc"
    Me.lbPerc.NTSDbField = ""
    Me.lbPerc.Size = New System.Drawing.Size(104, 13)
    Me.lbPerc.TabIndex = 2
    Me.lbPerc.Text = "Percentuali riferite a"
    Me.lbPerc.Tooltip = ""
    Me.lbPerc.UseMnemonic = False
    '
    'lbCodanag
    '
    Me.lbCodanag.AutoSize = True
    Me.lbCodanag.BackColor = System.Drawing.Color.Transparent
    Me.lbCodanag.Location = New System.Drawing.Point(12, 52)
    Me.lbCodanag.Name = "lbCodanag"
    Me.lbCodanag.NTSDbField = ""
    Me.lbCodanag.Size = New System.Drawing.Size(93, 13)
    Me.lbCodanag.TabIndex = 1
    Me.lbCodanag.Text = "Codice anagrafico"
    Me.lbCodanag.Tooltip = ""
    Me.lbCodanag.UseMnemonic = False
    '
    'lbCodcage
    '
    Me.lbCodcage.AutoSize = True
    Me.lbCodcage.BackColor = System.Drawing.Color.Transparent
    Me.lbCodcage.Location = New System.Drawing.Point(12, 9)
    Me.lbCodcage.Name = "lbCodcage"
    Me.lbCodcage.NTSDbField = ""
    Me.lbCodcage.Size = New System.Drawing.Size(76, 13)
    Me.lbCodcage.TabIndex = 0
    Me.lbCodcage.Text = "Codice agente"
    Me.lbCodcage.Tooltip = ""
    Me.lbCodcage.UseMnemonic = False
    '
    'FRMPRCAGE_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(556, 447)
    Me.Controls.Add(Me.pnPerprovv)
    Me.Controls.Add(Me.pnAgente)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMPRCAGE_F"
    Me.Text = "TABELLA CODICI AGENTE"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPerprovv, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPerprovv.ResumeLayout(False)
    CType(Me.pnAgente, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAgente.ResumeLayout(False)
    Me.pnAgente.PerformLayout()
    CType(Me.cbTb_tippro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_tiptab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_giorni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codforn.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_descage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codcage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano giÃ  stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      cbTb_tippro.NTSDbField = "tabcage.tb_tippro"
      cbTb_tiptab.NTSDbField = "tabcage.tb_tiptab"
      edTb_giorni.NTSDbField = "tabcage.tb_giorni"
      edTb_codforn.NTSDbField = "tabcage.tb_codforn"
      edTb_descage.NTSDbField = "tabcage.tb_descage"
      edTb_codcage.NTSDbField = "tabcage.tb_codcage"
      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcCage, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi di Form"
  Public Overridable Sub FRMPRCAGE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '--------------------------------------------------------------------------------------------------------------
      CaricaCombo()
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleCage.Apri(DittaCorrente, dsCage) Then Return
      dcCage.DataSource = dsCage.Tables("TABCAGE")
      dsCage.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      Bindcontrols()
      '--------------------------------------------------------------------------------------------------------------
      If dsCage.Tables("TABCAGE").Rows.Count > 0 Then edTb_codcage.Enabled = False
      If dsCage.Tables("TABCAGE").Rows.Count = 0 Then tlbNuovo_ItemClick(tlbNuovo, Nothing)
      dcCage.ResetBindings(False)
      dcCage.MoveFirst()
      '--------------------------------------------------------------------------------------------------------------
      If Not oCallParams Is Nothing Then
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
          If Not Salva() Then Return
          oCleCage.Nuovo()
          dcCage.MoveLast()
          edTb_codcage.Enabled = True
        ElseIf Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) <> "" Then
          For i As Integer = 0 To dcCage.List.Count - 1
            If CType(dcCage.Item(i), DataRowView)!tb_codcage.ToString = Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) Then
              dcCage.Position = i
              DecodAfterPosition()
              Exit For
            End If
          Next
        End If
      End If  'If Not oCallParams Is Nothing Then
      '--------------------------------------------------------------------------------------------------------------
      '--- Apre la griglia
      '--------------------------------------------------------------------------------------------------------------
      ApriGriglia(False)
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMPRCAGE_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub FRMPRCAGE_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcCage.Dispose()
      dsCage.Dispose()
    Catch
    End Try
  End Sub
#End Region

  Public Overridable Sub CaricaCombo()
    Dim dttTiptab As New DataTable()
    Dim dttTippro As New DataTable()

    Try
      dttTiptab.Columns.Add("cod", GetType(String))
      dttTiptab.Columns.Add("val", GetType(String))
      dttTiptab.Rows.Add(New Object() {"A", "Articolo"})
      dttTiptab.Rows.Add(New Object() {"C", "Cliente"})
      dttTiptab.Rows.Add(New Object() {"T", "Sconti/provv."})
      dttTiptab.AcceptChanges()
      cbTb_tiptab.DataSource = dttTiptab
      cbTb_tiptab.ValueMember = "cod"
      cbTb_tiptab.DisplayMember = "val"

      dttTippro.Columns.Add("cod", GetType(String))
      dttTippro.Columns.Add("val", GetType(String))
      dttTippro.Rows.Add(New Object() {"F", "Fatturato"})
      dttTippro.Rows.Add(New Object() {"I", "Incassato"})
      dttTippro.Rows.Add(New Object() {"M", "Maturato"})
      dttTippro.Rows.Add(New Object() {"T", "Incassato totale"})
      dttTippro.AcceptChanges()
      cbTb_tippro.DataSource = dttTippro
      cbTb_tippro.ValueMember = "cod"
      cbTb_tippro.DisplayMember = "val"
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      '-------------------------------------------------
      'creo un nuovo record
      If Not Salva() Then Return
      oCleCage.Nuovo()
      dcCage.MoveLast()
      edTb_codcage.Enabled = True
      Aggiorna(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      If Salva() Then
        edTb_codcage.Enabled = False
        ApriGriglia(False)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Dim i As Integer = 0
    Try
      Cancella()

      Aggiorna(False)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Dim bRemovBinding As Boolean = False
    Try
      Me.ValidaLastControl()          'se non valido il controllo su cui sono, quando modifico il controllo e, senza uscire, faccio 'ripristina' il controllo rimane sporco

      '-------------------------------------------------
      'ripristino
      Dim dlgRes As DialogResult
      If Not sender Is Nothing Then
        'chiamato facendo pressione sulla funzione 'ripristina'
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128589410346250000, "Ripristinare le modifiche apportate al record?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes

          If dsCage.Tables("TABCAGE").Rows.Count = 1 AndAlso dsCage.Tables("TABCAGE").Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          oCleCage.Ripristina(dcCage.Position, dcCage.Filter)

          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcCage, Me)
            bRemovBinding = False
            edTb_codcage.Enabled = True
          Else
            edTb_codcage.Enabled = False
          End If

          ceProvv.Ripristina()

          Aggiorna(False)
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim oParam As New CLE__PATB
    Try
      'SendKeys.SendWait("{F5}")
      'se faccio la riga sopra va in un loop infinito....
      'devo vedere quale controllo ha il focus, quindi lanciare lo zoom direttamente sul controllo, senza forzare l'F5
      Dim i As Integer = 0
      If edTb_descage.Focused Then
        'lancio lo zoom solo per riposizionare il BindingSource
        Me.ValidaLastControl()            'altrimeti se cambio la descrizione e poi faccio zoom per spostarmi non chiede di salvare !!!

        NTSZOOM.strIn = edTb_codcage.Text
        NTSZOOM.ZoomStrIn("ZOOMTABCAGE", DittaCorrente, oParam)

        If NTSZOOM.strIn <> edTb_codcage.Text Then
          'mi sposto sul record selezionato
          For i = 0 To dcCage.List.Count - 1
            If CType(dcCage.Item(i), DataRowView)!tb_codcage.ToString = NTSZOOM.strIn Then
              dcCage.Position = i
              Exit For
            End If
          Next
          Aggiorna(False)
        End If
      ElseIf edTb_codforn.Focused Then
        SetFastZoom(edTb_codforn.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edTb_codforn.Text
        oParam.bVisGriglia = True
        oParam.strTipo = "F"
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_codforn.Text Then edTb_codforn.NTSTextDB = NTSZOOM.strIn
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

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      If Not Salva() Then Return
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      If Not Salva() Then Return
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
    Me.Close()
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub

  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    Try
      '-------------------------------------------------
      'vado sul primo record
      If Not Salva() Then Return
      dcCage.MoveFirst()

      Aggiorna(False)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbPrecedente_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrecedente.ItemClick
    Try
      '-------------------------------------------------
      'vado sul record precedente
      If Not Salva() Then Return
      dcCage.MovePrevious()

      Aggiorna(False)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSuccessivo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSuccessivo.ItemClick
    Try
      '-------------------------------------------------
      'vado sul record successivo
      If Not Salva() Then Return
      dcCage.MoveNext()

      Aggiorna(False)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbUltimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbUltimo.ItemClick
    Try
      '-------------------------------------------------
      'vado sull'ultimo record
      If Not Salva() Then Return
      dcCage.MoveLast()

      Aggiorna(False)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Function Salva() As Boolean
    Dim dRes As DialogResult

    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      If oCleCage.RecordIsChanged Then
        If GctlControllaOutNotEqual() = False Then Return False
        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128589412364687500, "Salvo la tabella codici agenti?"))
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oCleCage.Salva(False) Then Return False
          If dsCage.Tables("TABCAGE").Rows.Count > 0 Then
            edTb_codcage.Enabled = False
          End If
        End If
        If dRes = System.Windows.Forms.DialogResult.No Then
          tlbRipristina_ItemClick(Nothing, Nothing)
          Return True
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return ceProvv.Salva
      '--------------------------------------------------------------------------------------------------------------
      Aggiorna(False)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function Cancella() As Boolean
    Dim bRemovBinding As Boolean = False
    Try
      '-------------------------------------------------
      'cancello
      Select Case oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128589410075156250, "Confermare la cancellazione del record?"))
        Case Windows.Forms.DialogResult.No
          Return False
        Case Windows.Forms.DialogResult.Yes
          If dsCage.Tables("TABCAGE").Rows.Count = 1 Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          oCleCage.CancellaProv(NTSCInt(edTb_codcage.Text))
          dcCage.RemoveAt(dcCage.Position)
          oCleCage.Salva(True)

          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcCage, Me)
            bRemovBinding = False
            edTb_codcage.Enabled = True
          Else
            edTb_codcage.Enabled = False
          End If
      End Select

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer

    Try
      '--------------------------------------------------
      'preparo il motore di stampa
      strCrpe = "{TABCAGE.codditt} = '" & DittaCorrente & "'"

      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSPRCAGE", "Reports1", " ", 0, nDestin, "BSPRCAGE.RPT", False, "Codici agenti", False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce già il multireport)
      For i = LBound(CType(nPjob, Array), 2) To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Validated/Changed"
  Public Overridable Sub edTb_codforn_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTb_codforn.Validated
    Try
      If edTb_codforn.Text = "0" Then
        lbDesAnag.Text = ""
      End If

      If Not oCleCage.ValCodiceDB(edTb_codforn.Text, "N", "ANAGRA", lbDesAnag.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129472396764093093, "Codice anagrafico inesistente"))
        edTb_codforn.Text = "0"
        lbDesAnag.Text = ""
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cbTb_tiptab_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTb_tiptab.SelectedValueChanged
    Try
      Select Case cbTb_tiptab.SelectedValue
        Case "A", "T" : ceProvv.strRifer = "A"
        Case "C" : ceProvv.strRifer = "C"
      End Select

      ceProvv.cbTipoProv_SelectedIndexChanged(Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub Aggiorna(ByVal bNuovo As Boolean)
    Try
      If edTb_codforn.Text = "0" Then
        lbDesAnag.Text = ""
      End If
      If Not oCleCage.ValCodiceDB(edTb_codforn.Text, "N", "ANAGRA", lbDesAnag.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128726803304325682, "Codice anagrafico inesistente"))
        edTb_codforn.Text = "0"
        lbDesAnag.Text = ""
      End If

      ApriGriglia(bNuovo)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub DecodAfterPosition()
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      lbDesAnag.Text = ""
      If edTb_codforn.Text <> "0" Then
        If oCleCage.ValCodiceDB(edTb_codforn.Text, "N", "ANAGRA", strTmp) = True Then lbDesAnag.Text = strTmp
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub ApriGriglia(ByVal bNuovo As Boolean)
    Try
      '--------------------------------------------------------------------------------------------------------------
      ceProvv.TipoProvv = 6
      ceProvv.per_codcage.Visible = False
      ceProvv.xx_codcage.Visible = False
      '--------------------------------------------------------------------------------------------------------------
      If bNuovo = False Then
        '------------------------------------------------------------------------------------------------------------
        '--- Appro la griglia
        '------------------------------------------------------------------------------------------------------------
        ceProvv.PerCodcage = NTSCInt(edTb_codcage.Text)
      Else
        '------------------------------------------------------------------------------------------------------------
        '--- Se nuovo carica l'agente 0 cioè svuota la griglia per il bug corretto il xx/yy
        '--- apro la griglia
        '------------------------------------------------------------------------------------------------------------
        ceProvv.PerCodcage = 0
      End If
      '--------------------------------------------------------------------------------------------------------------
      ceProvv.ApriProvvigioni()
      '--------------------------------------------------------------------------------------------------------------
      ceProvv.Enabled = Not bNuovo
      pnPerprovv.Visible = Not bNuovo
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class