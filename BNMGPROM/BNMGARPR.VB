#Region "Imports"
Imports System.Data
Imports NTSInformatica.CLN__STD
#End Region

Public Class FRMMGARPR

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

#Region "Variabili"

  Public oCleArpr As CLEMGPROM

  Public strWhereArtico As String = ""

  Public dsArpr As DataSet
  Public dcArpr As BindingSource = New BindingSource()
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
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnArpr As NTSInformatica.NTSPanel
  Public WithEvents grArpr As NTSInformatica.NTSGrid
  Public WithEvents grvArpr As NTSInformatica.NTSGridView
  Public WithEvents apr_codart As NTSInformatica.NTSGridColumn
  Public WithEvents xx_desart As NTSInformatica.NTSGridColumn
  Public WithEvents apr_coddpro As NTSInformatica.NTSGridColumn
  Public WithEvents xx_desdpro As NTSInformatica.NTSGridColumn
  Public WithEvents apr_note As NTSInformatica.NTSGridColumn
  Public WithEvents lbDescr As NTSInformatica.NTSLabel
  Public WithEvents cmdDesAll As NTSInformatica.NTSButton
  Public WithEvents cmdSelAll As NTSInformatica.NTSButton
  Public WithEvents cmdCancella As NTSInformatica.NTSButton
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents xx_checked As NTSInformatica.NTSGridColumn
  Public WithEvents btnInvertiSell As NTSInformatica.NTSButton
  Public WithEvents btnCancellaSelezionati As NTSInformatica.NTSButton
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents pnBottom As NTSInformatica.NTSPanel
  Public WithEvents tlbImposta As NTSInformatica.NTSBarButtonItem

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

    Return True
  End Function
  Public Overridable Sub InitEntity(ByVal oCleArpr As CLEMGPROM)
    Me.oCleArpr = oCleArpr
    AddHandler Me.oCleArpr.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      'Carico le immagini della Toolbar
      LoadImage()

      tlbMain.NTSSetToolTip()

      grvArpr.NTSSetParam(oMenu, oApp.Tr(Me, 128516998324375000, "Griglia Dettaglio Promozioni Articoli"))

      apr_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128517014915937500, "Articolo"), CLN__STD.CodartMaxLen, False)
      xx_desart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128517014993906250, "Descrizione articolo"), 40)
      apr_coddpro.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128517015042968750, "Dettaglio prom."), "#,##0", 3, 0, 999)
      xx_desdpro.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128517015145156250, "Descr. dettaglio prom."), 0)
      apr_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128517015206093750, "Note"), 0)
      xx_checked.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128517777081562500, "Seleziona"), "S", "N")

      apr_codart.NTSSetParamZoom("ZOOMARTICO")
      apr_coddpro.NTSSetParamZoom("ZOOMTABDPRO")

      apr_codart.NTSSetRichiesto()

      xx_desart.Enabled = False
      xx_desdpro.Enabled = False
      'Chiamo lo script per inizializzare i controlli caricati con Source Extender
      NTSScriptExec("InitControls", Me, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub
  Public Overridable Sub LoadImage()
    Try
      tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
    Catch ex As Exception
      'Non gestisco l'errore: 
      'se non c'è una immagine prendo quella a design
    End Try
    Try
      tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
    Catch ex As Exception
      'Non gestisco l'errore: 
      'se non c'è una immagine prendo quella a design
    End Try
    Try
      tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
    Catch ex As Exception
      'Non gestisco l'errore: 
      'se non c'è una immagine prendo quella a design
    End Try
    Try
      tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
    Catch ex As Exception
      'Non gestisco l'errore: 
      'se non c'è una immagine prendo quella a design
    End Try
    Try
      tlbImposta.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
    Catch ex As Exception
      'Non gestisco l'errore: 
      'se non c'è una immagine prendo quella a design
    End Try
    Try
      tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
    Catch ex As Exception
      'Non gestisco l'errore: 
      'se non c'è una immagine prendo quella a design
    End Try
    Try
      tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
    Catch ex As Exception
      'Non gestisco l'errore: 
      'se non c'è una immagine prendo quella a design
    End Try
    Try
      tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
    Catch ex As Exception
      'Non gestisco l'errore: 
      'se non c'è una immagine prendo quella a design
    End Try
    Try
      tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
    Catch ex As Exception
      'Non gestisco l'errore: 
      'se non c'è una immagine prendo quella a design
    End Try
    Try
      tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
    Catch ex As Exception
      'Non gestisco l'errore: 
      'se non c'è una immagine prendo quella a design
    End Try
  End Sub

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGARPR))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbImposta = New NTSInformatica.NTSBarButtonItem
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
    Me.pnArpr = New NTSInformatica.NTSPanel
    Me.btnInvertiSell = New NTSInformatica.NTSButton
    Me.cmdDesAll = New NTSInformatica.NTSButton
    Me.cmdSelAll = New NTSInformatica.NTSButton
    Me.btnCancellaSelezionati = New NTSInformatica.NTSButton
    Me.cmdCancella = New NTSInformatica.NTSButton
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.lbDescr = New NTSInformatica.NTSLabel
    Me.grArpr = New NTSInformatica.NTSGrid
    Me.grvArpr = New NTSInformatica.NTSGridView
    Me.xx_checked = New NTSInformatica.NTSGridColumn
    Me.apr_codart = New NTSInformatica.NTSGridColumn
    Me.xx_desart = New NTSInformatica.NTSGridColumn
    Me.apr_coddpro = New NTSInformatica.NTSGridColumn
    Me.xx_desdpro = New NTSInformatica.NTSGridColumn
    Me.apr_note = New NTSInformatica.NTSGridColumn
    Me.pnBottom = New NTSInformatica.NTSPanel
    Me.pnTop = New NTSInformatica.NTSPanel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnArpr, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnArpr.SuspendLayout()
    CType(Me.grArpr, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvArpr, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBottom.SuspendLayout()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbImposta})
    Me.NtsBarManager1.MaxItemId = 18
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImposta, True), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.tlbStrumenti, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbImposta
    '
    Me.tlbImposta.Caption = "Imposta dettaglio promozione"
    Me.tlbImposta.Glyph = CType(resources.GetObject("tlbImposta.Glyph"), System.Drawing.Image)
    Me.tlbImposta.Id = 16
    Me.tlbImposta.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I))
    Me.tlbImposta.Name = "tlbImposta"
    Me.tlbImposta.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 9
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta stampante"
    Me.tlbImpostaStampante.Id = 10
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 11
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa a video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 12
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 13
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 14
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnArpr
    '
    Me.pnArpr.AllowDrop = True
    Me.pnArpr.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnArpr.Appearance.Options.UseBackColor = True
    Me.pnArpr.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnArpr.Controls.Add(Me.grArpr)
    Me.pnArpr.Controls.Add(Me.pnTop)
    Me.pnArpr.Controls.Add(Me.pnBottom)
    Me.pnArpr.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnArpr.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnArpr.Location = New System.Drawing.Point(0, 30)
    Me.pnArpr.Name = "pnArpr"
    Me.pnArpr.NTSActiveTrasparency = True
    Me.pnArpr.Size = New System.Drawing.Size(799, 378)
    Me.pnArpr.TabIndex = 0
    Me.pnArpr.Text = "pnProm"
    '
    'btnInvertiSell
    '
    Me.btnInvertiSell.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnInvertiSell.ImageText = ""
    Me.btnInvertiSell.Location = New System.Drawing.Point(696, 5)
    Me.btnInvertiSell.Name = "btnInvertiSell"
    Me.btnInvertiSell.Size = New System.Drawing.Size(100, 23)
    Me.btnInvertiSell.TabIndex = 3
    Me.btnInvertiSell.Text = "Inve&rti selezione"
    '
    'cmdDesAll
    '
    Me.cmdDesAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdDesAll.ImageText = ""
    Me.cmdDesAll.Location = New System.Drawing.Point(590, 5)
    Me.cmdDesAll.Name = "cmdDesAll"
    Me.cmdDesAll.Size = New System.Drawing.Size(100, 23)
    Me.cmdDesAll.TabIndex = 3
    Me.cmdDesAll.Text = "&Deseleziona tutto"
    '
    'cmdSelAll
    '
    Me.cmdSelAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdSelAll.ImageText = ""
    Me.cmdSelAll.Location = New System.Drawing.Point(484, 5)
    Me.cmdSelAll.Name = "cmdSelAll"
    Me.cmdSelAll.Size = New System.Drawing.Size(100, 23)
    Me.cmdSelAll.TabIndex = 3
    Me.cmdSelAll.Text = "Seleziona t&utto"
    '
    'btnCancellaSelezionati
    '
    Me.btnCancellaSelezionati.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnCancellaSelezionati.ImageText = ""
    Me.btnCancellaSelezionati.Location = New System.Drawing.Point(378, 5)
    Me.btnCancellaSelezionati.Name = "btnCancellaSelezionati"
    Me.btnCancellaSelezionati.Size = New System.Drawing.Size(100, 23)
    Me.btnCancellaSelezionati.TabIndex = 3
    Me.btnCancellaSelezionati.Text = "Ca&ncella selezione"
    '
    'cmdCancella
    '
    Me.cmdCancella.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdCancella.ImageText = ""
    Me.cmdCancella.Location = New System.Drawing.Point(272, 5)
    Me.cmdCancella.Name = "cmdCancella"
    Me.cmdCancella.Size = New System.Drawing.Size(100, 23)
    Me.cmdCancella.TabIndex = 3
    Me.cmdCancella.Text = "&Cancella tutto"
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(166, 5)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.Size = New System.Drawing.Size(100, 23)
    Me.cmdSeleziona.TabIndex = 3
    Me.cmdSeleziona.Text = "&Seleziona articoli"
    '
    'lbDescr
    '
    Me.lbDescr.AutoSize = True
    Me.lbDescr.BackColor = System.Drawing.Color.Transparent
    Me.lbDescr.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
    Me.lbDescr.Location = New System.Drawing.Point(10, 8)
    Me.lbDescr.Name = "lbDescr"
    Me.lbDescr.NTSDbField = ""
    Me.lbDescr.Size = New System.Drawing.Size(51, 14)
    Me.lbDescr.TabIndex = 2
    Me.lbDescr.Text = "lbDescr"
    Me.lbDescr.Tooltip = ""
    Me.lbDescr.UseMnemonic = False
    '
    'grArpr
    '
    Me.grArpr.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grArpr.EmbeddedNavigator.Name = ""
    Me.grArpr.Location = New System.Drawing.Point(0, 31)
    Me.grArpr.MainView = Me.grvArpr
    Me.grArpr.Name = "grArpr"
    Me.grArpr.Size = New System.Drawing.Size(799, 316)
    Me.grArpr.TabIndex = 1
    Me.grArpr.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvArpr})
    '
    'grvArpr
    '
    Me.grvArpr.ActiveFilterEnabled = False
    Me.grvArpr.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_checked, Me.apr_codart, Me.xx_desart, Me.apr_coddpro, Me.xx_desdpro, Me.apr_note})
    Me.grvArpr.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvArpr.Enabled = True
    Me.grvArpr.GridControl = Me.grArpr
    Me.grvArpr.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvArpr.MinRowHeight = 14
    Me.grvArpr.Name = "grvArpr"
    Me.grvArpr.NTSAllowDelete = True
    Me.grvArpr.NTSAllowInsert = True
    Me.grvArpr.NTSAllowUpdate = True
    Me.grvArpr.NTSMenuContext = Nothing
    Me.grvArpr.OptionsCustomization.AllowRowSizing = True
    Me.grvArpr.OptionsFilter.AllowFilterEditor = False
    Me.grvArpr.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvArpr.OptionsNavigation.UseTabKey = False
    Me.grvArpr.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvArpr.OptionsView.ColumnAutoWidth = False
    Me.grvArpr.OptionsView.EnableAppearanceEvenRow = True
    Me.grvArpr.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvArpr.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvArpr.OptionsView.ShowGroupPanel = False
    Me.grvArpr.RowHeight = 16
    '
    'xx_checked
    '
    Me.xx_checked.AppearanceCell.Options.UseBackColor = True
    Me.xx_checked.AppearanceCell.Options.UseTextOptions = True
    Me.xx_checked.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_checked.Caption = "Seleziona"
    Me.xx_checked.Enabled = True
    Me.xx_checked.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_checked.Name = "xx_checked"
    Me.xx_checked.NTSRepositoryComboBox = Nothing
    Me.xx_checked.NTSRepositoryItemCheck = Nothing
    Me.xx_checked.NTSRepositoryItemMemo = Nothing
    Me.xx_checked.NTSRepositoryItemText = Nothing
    Me.xx_checked.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_checked.OptionsFilter.AllowFilter = False
    Me.xx_checked.Visible = True
    Me.xx_checked.VisibleIndex = 0
    '
    'apr_codart
    '
    Me.apr_codart.AppearanceCell.Options.UseBackColor = True
    Me.apr_codart.AppearanceCell.Options.UseTextOptions = True
    Me.apr_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apr_codart.AppearanceHeader.Options.UseTextOptions = True
    Me.apr_codart.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.apr_codart.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.apr_codart.Caption = "Articolo"
    Me.apr_codart.Enabled = True
    Me.apr_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apr_codart.Name = "apr_codart"
    Me.apr_codart.NTSRepositoryComboBox = Nothing
    Me.apr_codart.NTSRepositoryItemCheck = Nothing
    Me.apr_codart.NTSRepositoryItemMemo = Nothing
    Me.apr_codart.NTSRepositoryItemText = Nothing
    Me.apr_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apr_codart.OptionsFilter.AllowFilter = False
    Me.apr_codart.Visible = True
    Me.apr_codart.VisibleIndex = 1
    Me.apr_codart.Width = 162
    '
    'xx_desart
    '
    Me.xx_desart.AppearanceCell.Options.UseBackColor = True
    Me.xx_desart.AppearanceCell.Options.UseTextOptions = True
    Me.xx_desart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_desart.AppearanceHeader.Options.UseTextOptions = True
    Me.xx_desart.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.xx_desart.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.xx_desart.Caption = "Descrizione articolo"
    Me.xx_desart.Enabled = True
    Me.xx_desart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_desart.Name = "xx_desart"
    Me.xx_desart.NTSRepositoryComboBox = Nothing
    Me.xx_desart.NTSRepositoryItemCheck = Nothing
    Me.xx_desart.NTSRepositoryItemMemo = Nothing
    Me.xx_desart.NTSRepositoryItemText = Nothing
    Me.xx_desart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_desart.OptionsFilter.AllowFilter = False
    Me.xx_desart.Visible = True
    Me.xx_desart.VisibleIndex = 2
    Me.xx_desart.Width = 158
    '
    'apr_coddpro
    '
    Me.apr_coddpro.AppearanceCell.Options.UseBackColor = True
    Me.apr_coddpro.AppearanceCell.Options.UseTextOptions = True
    Me.apr_coddpro.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apr_coddpro.Caption = "Dettaglio prom."
    Me.apr_coddpro.Enabled = True
    Me.apr_coddpro.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apr_coddpro.Name = "apr_coddpro"
    Me.apr_coddpro.NTSRepositoryComboBox = Nothing
    Me.apr_coddpro.NTSRepositoryItemCheck = Nothing
    Me.apr_coddpro.NTSRepositoryItemMemo = Nothing
    Me.apr_coddpro.NTSRepositoryItemText = Nothing
    Me.apr_coddpro.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apr_coddpro.OptionsFilter.AllowFilter = False
    Me.apr_coddpro.Visible = True
    Me.apr_coddpro.VisibleIndex = 3
    Me.apr_coddpro.Width = 88
    '
    'xx_desdpro
    '
    Me.xx_desdpro.AppearanceCell.Options.UseBackColor = True
    Me.xx_desdpro.AppearanceCell.Options.UseTextOptions = True
    Me.xx_desdpro.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_desdpro.Caption = "Descr. dettaglio prom."
    Me.xx_desdpro.Enabled = True
    Me.xx_desdpro.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_desdpro.Name = "xx_desdpro"
    Me.xx_desdpro.NTSRepositoryComboBox = Nothing
    Me.xx_desdpro.NTSRepositoryItemCheck = Nothing
    Me.xx_desdpro.NTSRepositoryItemMemo = Nothing
    Me.xx_desdpro.NTSRepositoryItemText = Nothing
    Me.xx_desdpro.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_desdpro.OptionsFilter.AllowFilter = False
    Me.xx_desdpro.Visible = True
    Me.xx_desdpro.VisibleIndex = 4
    Me.xx_desdpro.Width = 122
    '
    'apr_note
    '
    Me.apr_note.AppearanceCell.Options.UseBackColor = True
    Me.apr_note.AppearanceCell.Options.UseTextOptions = True
    Me.apr_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apr_note.Caption = "Note"
    Me.apr_note.Enabled = True
    Me.apr_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apr_note.Name = "apr_note"
    Me.apr_note.NTSRepositoryComboBox = Nothing
    Me.apr_note.NTSRepositoryItemCheck = Nothing
    Me.apr_note.NTSRepositoryItemMemo = Nothing
    Me.apr_note.NTSRepositoryItemText = Nothing
    Me.apr_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apr_note.OptionsFilter.AllowFilter = False
    Me.apr_note.Visible = True
    Me.apr_note.VisibleIndex = 5
    Me.apr_note.Width = 172
    '
    'pnBottom
    '
    Me.pnBottom.AllowDrop = True
    Me.pnBottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBottom.Appearance.Options.UseBackColor = True
    Me.pnBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBottom.Controls.Add(Me.btnInvertiSell)
    Me.pnBottom.Controls.Add(Me.cmdSeleziona)
    Me.pnBottom.Controls.Add(Me.cmdDesAll)
    Me.pnBottom.Controls.Add(Me.cmdCancella)
    Me.pnBottom.Controls.Add(Me.cmdSelAll)
    Me.pnBottom.Controls.Add(Me.btnCancellaSelezionati)
    Me.pnBottom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnBottom.Location = New System.Drawing.Point(0, 347)
    Me.pnBottom.Name = "pnBottom"
    Me.pnBottom.NTSActiveTrasparency = True
    Me.pnBottom.Size = New System.Drawing.Size(799, 31)
    Me.pnBottom.TabIndex = 10
    Me.pnBottom.Text = "NtsPanel1"
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.lbDescr)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(799, 31)
    Me.pnTop.TabIndex = 11
    Me.pnTop.Text = "NtsPanel1"
    '
    'FRMMGARPR
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(799, 408)
    Me.Controls.Add(Me.pnArpr)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.HelpButton = True
    Me.Name = "FRMMGARPR"
    Me.NTSLastControlFocussed = Me.grArpr
    Me.Text = "DETTAGLIO PROMOZIONI ARTICOLI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnArpr, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnArpr.ResumeLayout(False)
    CType(Me.grArpr, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvArpr, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBottom.ResumeLayout(False)
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    Me.ResumeLayout(False)

  End Sub

#Region "Form"

  Public Overridable Sub FRMMGARPR_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcArpr.Dispose()
      If Not dsArpr Is Nothing Then
        If Not dsArpr.Tables("artprom") Is Nothing Then dsArpr.Tables.Remove("artprom")
        dsArpr.Dispose()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGARPR_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva(True) Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGARPR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try

      'Predispongo i controlli
      InitControls()

      If Not dsArpr Is Nothing Then If Not dsArpr.Tables("artprom") Is Nothing Then dsArpr.Tables.Remove("artprom")

      '-------------------------------------------------
      'Leggo dal database i dati e collego l'NTSBinding
      If oCleArpr.ApriDettaglio(DittaCorrente, NTSCInt(CType(oCallParams.ctlPar1, DataRow)!tb_codtpro), dsArpr) Then

        dcArpr.DataSource = dsArpr.Tables("artprom")
        dsArpr.AcceptChanges()

        grArpr.DataSource = dcArpr

        GctlSetRoules()

        lbDescr.Text = "PROMOZIONE--> '" & NTSCStr(CType(oCallParams.ctlPar1, DataRow)!tb_codtpro) & _
                       "' - " & UCase(NTSCStr(CType(oCallParams.ctlPar1, DataRow)!tb_destpro)) & _
                       "   valida dal " & NTSCDate(CType(oCallParams.ctlPar1, DataRow)!tb_datagg) & _
                       " al " & NTSCDate(CType(oCallParams.ctlPar1, DataRow)!tb_datscad)

        AbilitaTlbSecondoGriglia()
        tlbSalva.Enabled = False
        tlbRipristina.Enabled = False
      Else
        Me.Close()
      End If


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Dim oPar As CLE__PATB = Nothing
    Try

      If Not Salva(False) Then Return

      oPar = New CLE__PATB
      oPar.bVisGriglia = False
      oPar.strTipoArticolo = "N"
      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oPar)
      If oPar.CANCELZOOM = False Then
        strWhereArtico = oPar.strOut.Trim
      End If

      If strWhereArtico = "" Then Return

      oCleArpr.AggiungiArticoliDettaglioPromozione(strWhereArtico, NTSCInt(CType(oCallParams.ctlPar1, DataRow)!tb_codtpro))

      dcArpr.DataSource = dsArpr.Tables("artprom")
      dsArpr.AcceptChanges()
      grArpr.DataSource = dcArpr

      AbilitaTlbSecondoGriglia()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdCancella_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancella.Click
    Try
      If Not oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128520471304062500, "Cancellare tutti i record?")) = Windows.Forms.DialogResult.Yes Then Return

      oCleArpr.CancellaTuttiDettagliPromozione(NTSCInt(CType(oCallParams.ctlPar1, DataRow)!tb_codtpro))

      dcArpr.DataSource = dsArpr.Tables("artprom")
      dsArpr.AcceptChanges()
      grArpr.DataSource = dcArpr

      AbilitaTlbSecondoGriglia()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub btnCancellaSelezionati_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancellaSelezionati.Click
    Try
      If Not oCleArpr.AtLeastOneRowSelected Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128517901803281250, "Non sono state selezionate righe da cancellare."))
        Return
      End If

      If Not oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128520317088125000, "Vuoi procedere con la cancellazione dei record?")) = Windows.Forms.DialogResult.Yes Then Return

      dcArpr.MoveFirst()

      Dim deletedRecords As Integer = 0
      Dim maxRecords As Integer = dcArpr.Count

      For i As Integer = 0 To dcArpr.Count - 1
        If NTSCStr(CType(dcArpr.Item(dcArpr.Position), DataRowView).Item(xx_checked.Name)) = "S" Then

          If Not grvArpr.NTSDeleteRigaCorrente(dcArpr, False) Then
            dcArpr.MoveNext()
          Else
            deletedRecords += 1
          End If
        Else

          dcArpr.MoveNext()

        End If

      Next

      If deletedRecords = maxRecords Then
        oCleArpr.CancellaTuttiDettagliPromozione(NTSCInt(CType(oCallParams.ctlPar1, DataRow)!tb_codtpro))

        dcArpr.DataSource = dsArpr.Tables("artprom")
        dsArpr.AcceptChanges()
        grArpr.DataSource = dcArpr

        AbilitaTlbSecondoGriglia()
      Else
        oCleArpr.SalvaDettaglio(True)
        AbilitaTlbSecondoGriglia()

        dcArpr.MoveFirst()
      End If


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdSelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelAll.Click
    Dim i As Integer
    Try
      For i = 0 To grvArpr.RowCount - 1
        grvArpr.SetRowCellValue(i, "xx_checked", "S")
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdDesAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDesAll.Click
    Dim i As Integer
    Try
      For i = 0 To grvArpr.RowCount - 1
        grvArpr.SetRowCellValue(i, "xx_checked", "N")
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub btnInvertiSell_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInvertiSell.Click
    Dim i As Integer
    Dim dtrTmp As DataRow()
    Try
      dtrTmp = dsArpr.Tables("artprom").Select()
      For i = 0 To dtrTmp.Length - 1
        If NTSCStr(dtrTmp(i).Item(xx_checked.Name)) = "N" Then
          dtrTmp(i).Item(xx_checked.Name) = "S"
        Else
          dtrTmp(i).Item(xx_checked.Name) = "N"
        End If
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try

  End Sub
#End Region

#Region "Griglia"

  Public Overridable Sub grvArpr_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvArpr.NTSFocusedRowChanged
    Try
      'Se il codice è diverso da 0 blocco la colonna, diversamente la rendo editabile
      If NTSCStr(grvArpr.GetFocusedRowCellValue(apr_codart).ToString.Trim) <> "" And NTSCStr(grvArpr.GetFocusedRowCellValue(apr_codart).ToString.Trim) <> " " Then
        apr_codart.Enabled = False
      Else
        'tb_codtpro.Enabled = true '!!!CON QUESTA LA GRIGLIA NON FUNZIONA CORRETTAMENTE
        GctlSetVisEnab(apr_codart, False)
      End If
      If grvArpr.FocusedRowHandle = -999998 Then
        tlbCancella.Enabled = False
      Else
        tlbCancella.Enabled = True
        tlbNuovo.Enabled = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvArpr_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvArpr.NTSBeforeRowUpdate
    Try
      If Not Salva(False) Then
        'Rimango sulla riga su cui sono
        e.Allow = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvArpr_CellValueChanging(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvArpr.CellValueChanging
    Try
      tlbRipristina.Enabled = True
      tlbSalva.Enabled = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub grvArpr_NTSNewRow(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grvArpr.NTSNewRow
    Try
      tlbCancella.Enabled = False
      tlbNuovo.Enabled = False
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Toolbar"

  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvArpr.NTSNuovo()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva(False)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Dim strAddText As String = ""

    Try
      If grvArpr.NTSGetCurrentDataRow Is Nothing Then Return

      If oApp.MsgBoxInfoYesNo_DefYes(strAddText & oApp.Tr(Me, 128517025036250000, "Vuoi procedere con la cancellazione del record?")) = Windows.Forms.DialogResult.Yes Then

        If grvArpr.NTSDeleteRigaCorrente(dcArpr, False) Then
          oCleArpr.SalvaDettaglio(True)
          AbilitaTlbSecondoGriglia()
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvArpr.NTSRipristinaRigaCorrenteBefore(dcArpr, CBool(IIf(sender Is Nothing, False, True))) Then Return
      oCleArpr.RipristinaDettaglio(dcArpr.Position, dcArpr.Filter)
      grvArpr.NTSRipristinaRigaCorrenteAfter()
      AbilitaTlbSecondoGriglia()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbImposta_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImposta.ItemClick
    Dim oParam As New CLE__CLDP
    Dim frmSede As FRMMGSEDE = Nothing
    Try

      If Not oCleArpr.AtLeastOneRowSelected Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128733770219843750, "Non sono state selezionate righe sulle quali impostare il dettaglio promozione."))
        Return
      End If

      frmSede = CType(NTSNewFormModal("FRMMGSEDE"), FRMMGSEDE)
      frmSede.Init(oMenu, oParam, DittaCorrente)
      frmSede.InitEntity(oCleArpr)
      frmSede.ShowDialog()

      If Not frmSede.bOk Then Return

      If NTSCInt(frmSede.edTb_coddpro.Text) = 0 Then

        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128517911928750000, "Confermare " & _
        "l'azzeramento del dettaglio promozione sulle righe" & _
        " selezionate?")) = Windows.Forms.DialogResult.No Then Return


      Else

        Dim strMsg1 As String = oApp.Tr(Me, 128517896574062500, "Confermare l'impostazione del dettaglio:")
        Dim strMsg2 As String = oApp.Tr(Me, 128517899014843750, "sulle righe selezionate?")

        If oApp.MsgBoxInfoYesNo_DefYes(strMsg1 & vbCrLf & "   '" & _
            frmSede.edTb_coddpro.Text & "' - " & frmSede.lbTb_desdpro.Text & _
            vbCrLf & strMsg2) = Windows.Forms.DialogResult.No Then Return

      End If

      If Not oCleArpr.ImpostaCoddproSuRigheSelezionate(NTSCInt(frmSede.edTb_coddpro.Text)) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128517915067187500, "Errore durante il salvataggio dei codici di dettaglio promozione sulle righe selezionate!"))
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If Not frmSede Is Nothing Then frmSede.Dispose()
      frmSede = Nothing
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

  Public Overridable Function Salva(ByVal bEsci As Boolean) As Boolean
    Dim dRes As DialogResult
    Try

      Me.ValidaLastControl()      'Valido l'ultimo controllo che ha il focus

      dRes = grvArpr.NTSSalvaRigaCorrente(dcArpr, oCleArpr.RecordIsChangedDettaglio, False)

      If bEsci Then
        If dRes = Windows.Forms.DialogResult.Yes Then
          If grvArpr.NTSGetCurrentDataRow.RowState = DataRowState.Added Then
            dRes = oApp.MsgBoxInfoYesNoCancel_DefYes(oApp.Tr(Me, 128517973525312500, "Salvare il dettaglio promozione prima di uscire?"))
          Else
            dRes = oApp.MsgBoxInfoYesNoCancel_DefYes(oApp.Tr(Me, 128517973564843750, "Salvare le modifiche apportate al dettaglio promozione prima di uscire?"))
          End If

        End If
      End If

      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'Salvo
          '-------------------------------------------------
          'Controllo che i campi abbiano un valore diverso da quello 
          'impostato in GCTL.OutNotEqual
          If Not GctlControllaOutNotEqual() Then Return False
          If Not oCleArpr.SalvaDettaglio(False) Then
            Return False
          End If
          AbilitaTlbSecondoGriglia()

        Case System.Windows.Forms.DialogResult.No
          'Ripristino
          'oCleProm.Ripristina(dcCent.Position, dcCent.Filter)
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
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim i As Integer

    Try
      If Not Salva(False) Then Return

      '--------------------------------------------------
      'Preparo il motore di stampa
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGPROM", "Reports1", " ", 0, nDestin, "BSMGARPR.RPT", False, "DETTAGLIO PROMOZIONE ARTICOLI", False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'Lancio tutti gli eventuali reports (gestisce già il multireport)
      For i = LBound(CType(nPjob, Array), 2) To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i))))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub AbilitaTlbSecondoGriglia()
    Try
      If dsArpr.Tables("artprom").Rows.Count = 0 Then
        tlbImposta.Enabled = False
        tlbStampa.Enabled = False
        tlbStampaVideo.Enabled = False
        tlbCancella.Enabled = False
      Else
        tlbImposta.Enabled = True
        tlbStampa.Enabled = True
        tlbStampaVideo.Enabled = True
        tlbCancella.Enabled = True
      End If
      If oCleArpr.RecordIsChangedDettaglio Then
        tlbRipristina.Enabled = True
        tlbSalva.Enabled = True
      Else
        tlbRipristina.Enabled = False
        tlbSalva.Enabled = False
      End If
      tlbNuovo.Enabled = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class
