#Region "Imports"
Imports System.Data
Imports NTSInformatica.CLN__STD
#End Region

Public Class FRMMGPROM

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

  Public oCleProm As CLEMGPROM

  Public dsProm As DataSet
  Public dcProm As BindingSource = New BindingSource()
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
  Public WithEvents pnProm As NTSInformatica.NTSPanel
  Public WithEvents grProm As NTSInformatica.NTSGrid
  Public WithEvents grvProm As NTSInformatica.NTSGridView
  Public WithEvents tb_codtpro As NTSInformatica.NTSGridColumn
  Public WithEvents tb_destpro As NTSInformatica.NTSGridColumn
  Public WithEvents tb_classe As NTSInformatica.NTSGridColumn
  Public WithEvents tb_enne As NTSInformatica.NTSGridColumn
  Public WithEvents tb_emme As NTSInformatica.NTSGridColumn
  Public WithEvents tb_valsco As NTSInformatica.NTSGridColumn
  Public WithEvents tb_perc1 As NTSInformatica.NTSGridColumn
  Public WithEvents tb_perc2 As NTSInformatica.NTSGridColumn
  Public WithEvents tb_qtasco As NTSInformatica.NTSGridColumn
  Public WithEvents tb_qtalim As NTSInformatica.NTSGridColumn
  Public WithEvents tb_datscad As NTSInformatica.NTSGridColumn
  Public WithEvents tb_note As NTSInformatica.NTSGridColumn
  Public WithEvents tb_implim As NTSInformatica.NTSGridColumn
  Public WithEvents tb_datagg As NTSInformatica.NTSGridColumn
  Public WithEvents tlbDettagli As NTSInformatica.NTSBarButtonItem

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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGPROM", "BEMGPROM", objTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128182574024687500, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleProm = CType(objTmp, CLEMGPROM)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGPROM", strRemoteServer, strRemotePort)
    AddHandler oCleProm.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleProm.Init(oApp, NTSScript, oMenu.oCleComm, "TABTPRO", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      'Carico le immagini della Toolbar
      LoadImage()

      tlbMain.NTSSetToolTip()

      grvProm.NTSSetParam(oMenu, oApp.Tr(Me, 128438294955000000, "Griglia Promozioni Articoli"))

      tb_codtpro.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128510226882500000, "Codice"), tabtpro)
      tb_destpro.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128510226953593750, "Descrizione"), 20, False)
      tb_classe.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128510227005937500, "Classe"), "#,##0", 3, 0, 999)
      tb_enne.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128510227149531250, "N"), "#,##0", 8, 0, 99999999)
      tb_emme.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128510227192656250, "M"), "#,##0", 8, 0, 99999999)
      tb_valsco.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128510227244375000, "Sconto valore"), "#,##0.00", 11, 0, 99999999)
      tb_perc1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128510227278125000, "% Sconto 1"), "#,##0.00", 9, 0, 999999)
      tb_perc2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128510227328593750, "% Sconto 2"), "#,##0.00", 9, 0, 999999)
      tb_qtasco.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128510227367343750, "Pezzi in sconto"), "#,##0.000", 12, 0, 99999999)
      tb_implim.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128510227420937500, "Cifra limite"), "#,##0.00", 15, -99999999999, 99999999999)
      tb_qtalim.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128510227458906250, "N. Pezzi limite"), "#,##0.000", 12, 0, 99999999)
      tb_datagg.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128510227490937500, "Data inizio validità"), True)
      tb_datscad.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128510227530781250, "Data fine validità"), True)
      tb_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128510227585156250, "Note"), 0)

      tb_codtpro.NTSSetParamZoom("")

      tb_codtpro.NTSSetRichiesto()
      tb_destpro.NTSSetRichiesto()

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
      tlbDettagli.GlyphPath = (oApp.ChildImageDir & "\prngrid.gif")
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGPROM))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbDettagli = New NTSInformatica.NTSBarButtonItem
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
    Me.pnProm = New NTSInformatica.NTSPanel
    Me.grProm = New NTSInformatica.NTSGrid
    Me.grvProm = New NTSInformatica.NTSGridView
    Me.tb_codtpro = New NTSInformatica.NTSGridColumn
    Me.tb_destpro = New NTSInformatica.NTSGridColumn
    Me.tb_classe = New NTSInformatica.NTSGridColumn
    Me.tb_enne = New NTSInformatica.NTSGridColumn
    Me.tb_emme = New NTSInformatica.NTSGridColumn
    Me.tb_valsco = New NTSInformatica.NTSGridColumn
    Me.tb_perc1 = New NTSInformatica.NTSGridColumn
    Me.tb_perc2 = New NTSInformatica.NTSGridColumn
    Me.tb_qtasco = New NTSInformatica.NTSGridColumn
    Me.tb_implim = New NTSInformatica.NTSGridColumn
    Me.tb_qtalim = New NTSInformatica.NTSGridColumn
    Me.tb_datagg = New NTSInformatica.NTSGridColumn
    Me.tb_datscad = New NTSInformatica.NTSGridColumn
    Me.tb_note = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnProm, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnProm.SuspendLayout()
    CType(Me.grProm, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvProm, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    Me.frmPopup.ClientSize = New System.Drawing.Size(30, 56)
    '
    'DevXDefaultLookAndFeel
    '
    
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbDettagli})
    Me.NtsBarManager1.MaxItemId = 18
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDettagli, True), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.tlbStrumenti, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbDettagli
    '
    Me.tlbDettagli.Caption = "Dettagli"
    Me.tlbDettagli.Glyph = CType(resources.GetObject("tlbDettagli.Glyph"), System.Drawing.Image)
    Me.tlbDettagli.Id = 16
    Me.tlbDettagli.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D))
    Me.tlbDettagli.Name = "tlbDettagli"
    Me.tlbDettagli.Visible = True
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
    'pnProm
    '
    Me.pnProm.AllowDrop = True
    Me.pnProm.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnProm.Appearance.Options.UseBackColor = True
    Me.pnProm.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnProm.Controls.Add(Me.grProm)
    Me.pnProm.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnProm.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnProm.Location = New System.Drawing.Point(0, 30)
    Me.pnProm.Name = "pnProm"
    Me.pnProm.Size = New System.Drawing.Size(622, 374)
    Me.pnProm.TabIndex = 0
    Me.pnProm.Text = "pnProm"
    '
    'grProm
    '
    Me.grProm.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grProm.EmbeddedNavigator.Name = ""
    Me.grProm.Location = New System.Drawing.Point(0, 0)
    Me.grProm.MainView = Me.grvProm
    Me.grProm.Name = "grProm"
    Me.grProm.Size = New System.Drawing.Size(622, 374)
    Me.grProm.TabIndex = 1
    Me.grProm.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvProm})
    '
    'grvProm
    '
    Me.grvProm.ActiveFilterEnabled = False
    Me.grvProm.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tb_codtpro, Me.tb_destpro, Me.tb_classe, Me.tb_enne, Me.tb_emme, Me.tb_valsco, Me.tb_perc1, Me.tb_perc2, Me.tb_qtasco, Me.tb_implim, Me.tb_qtalim, Me.tb_datagg, Me.tb_datscad, Me.tb_note})
    Me.grvProm.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvProm.Enabled = True
    Me.grvProm.GridControl = Me.grProm
    Me.grvProm.Name = "grvProm"
    Me.grvProm.NTSAllowDelete = True
    Me.grvProm.NTSAllowInsert = True
    Me.grvProm.NTSAllowUpdate = True
    Me.grvProm.NTSMenuContext = Nothing
    Me.grvProm.OptionsCustomization.AllowRowSizing = True
    Me.grvProm.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvProm.OptionsNavigation.UseTabKey = False
    Me.grvProm.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvProm.OptionsView.ColumnAutoWidth = False
    Me.grvProm.OptionsView.EnableAppearanceEvenRow = True
    Me.grvProm.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvProm.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvProm.OptionsView.ShowGroupPanel = False
    Me.grvProm.RowHeight = 16
    '
    'tb_codtpro
    '
    Me.tb_codtpro.AppearanceCell.Options.UseBackColor = True
    Me.tb_codtpro.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codtpro.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_codtpro.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_codtpro.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_codtpro.Caption = "Codice"
    Me.tb_codtpro.Enabled = True
    Me.tb_codtpro.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codtpro.Name = "tb_codtpro"
    Me.tb_codtpro.NTSRepositoryComboBox = Nothing
    Me.tb_codtpro.NTSRepositoryItemCheck = Nothing
    Me.tb_codtpro.NTSRepositoryItemText = Nothing
    Me.tb_codtpro.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codtpro.OptionsFilter.AllowFilter = False
    Me.tb_codtpro.Visible = True
    Me.tb_codtpro.VisibleIndex = 0
    Me.tb_codtpro.Width = 61
    '
    'tb_destpro
    '
    Me.tb_destpro.AppearanceCell.Options.UseBackColor = True
    Me.tb_destpro.AppearanceCell.Options.UseTextOptions = True
    Me.tb_destpro.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_destpro.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_destpro.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_destpro.Caption = "Descrizione"
    Me.tb_destpro.Enabled = True
    Me.tb_destpro.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_destpro.Name = "tb_destpro"
    Me.tb_destpro.NTSRepositoryComboBox = Nothing
    Me.tb_destpro.NTSRepositoryItemCheck = Nothing
    Me.tb_destpro.NTSRepositoryItemText = Nothing
    Me.tb_destpro.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_destpro.OptionsFilter.AllowFilter = False
    Me.tb_destpro.Visible = True
    Me.tb_destpro.VisibleIndex = 1
    Me.tb_destpro.Width = 106
    '
    'tb_classe
    '
    Me.tb_classe.AppearanceCell.Options.UseBackColor = True
    Me.tb_classe.AppearanceCell.Options.UseTextOptions = True
    Me.tb_classe.Caption = "Classe"
    Me.tb_classe.Enabled = True
    Me.tb_classe.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_classe.Name = "tb_classe"
    Me.tb_classe.NTSRepositoryComboBox = Nothing
    Me.tb_classe.NTSRepositoryItemCheck = Nothing
    Me.tb_classe.NTSRepositoryItemText = Nothing
    Me.tb_classe.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_classe.OptionsFilter.AllowFilter = False
    '
    'tb_enne
    '
    Me.tb_enne.AppearanceCell.Options.UseBackColor = True
    Me.tb_enne.AppearanceCell.Options.UseTextOptions = True
    Me.tb_enne.Caption = "N"
    Me.tb_enne.Enabled = True
    Me.tb_enne.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_enne.Name = "tb_enne"
    Me.tb_enne.NTSRepositoryComboBox = Nothing
    Me.tb_enne.NTSRepositoryItemCheck = Nothing
    Me.tb_enne.NTSRepositoryItemText = Nothing
    Me.tb_enne.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_enne.OptionsFilter.AllowFilter = False
    '
    'tb_emme
    '
    Me.tb_emme.AppearanceCell.Options.UseBackColor = True
    Me.tb_emme.AppearanceCell.Options.UseTextOptions = True
    Me.tb_emme.Caption = "M"
    Me.tb_emme.Enabled = True
    Me.tb_emme.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_emme.Name = "tb_emme"
    Me.tb_emme.NTSRepositoryComboBox = Nothing
    Me.tb_emme.NTSRepositoryItemCheck = Nothing
    Me.tb_emme.NTSRepositoryItemText = Nothing
    Me.tb_emme.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_emme.OptionsFilter.AllowFilter = False
    '
    'tb_valsco
    '
    Me.tb_valsco.AppearanceCell.Options.UseBackColor = True
    Me.tb_valsco.AppearanceCell.Options.UseTextOptions = True
    Me.tb_valsco.Caption = "Sconto valore"
    Me.tb_valsco.Enabled = True
    Me.tb_valsco.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_valsco.Name = "tb_valsco"
    Me.tb_valsco.NTSRepositoryComboBox = Nothing
    Me.tb_valsco.NTSRepositoryItemCheck = Nothing
    Me.tb_valsco.NTSRepositoryItemText = Nothing
    Me.tb_valsco.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_valsco.OptionsFilter.AllowFilter = False
    '
    'tb_perc1
    '
    Me.tb_perc1.AppearanceCell.Options.UseBackColor = True
    Me.tb_perc1.AppearanceCell.Options.UseTextOptions = True
    Me.tb_perc1.Caption = "% Sconto 1"
    Me.tb_perc1.Enabled = True
    Me.tb_perc1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_perc1.Name = "tb_perc1"
    Me.tb_perc1.NTSRepositoryComboBox = Nothing
    Me.tb_perc1.NTSRepositoryItemCheck = Nothing
    Me.tb_perc1.NTSRepositoryItemText = Nothing
    Me.tb_perc1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_perc1.OptionsFilter.AllowFilter = False
    '
    'tb_perc2
    '
    Me.tb_perc2.AppearanceCell.Options.UseBackColor = True
    Me.tb_perc2.AppearanceCell.Options.UseTextOptions = True
    Me.tb_perc2.Caption = "% Sconto 2"
    Me.tb_perc2.Enabled = True
    Me.tb_perc2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_perc2.Name = "tb_perc2"
    Me.tb_perc2.NTSRepositoryComboBox = Nothing
    Me.tb_perc2.NTSRepositoryItemCheck = Nothing
    Me.tb_perc2.NTSRepositoryItemText = Nothing
    Me.tb_perc2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_perc2.OptionsFilter.AllowFilter = False
    '
    'tb_qtasco
    '
    Me.tb_qtasco.AppearanceCell.Options.UseBackColor = True
    Me.tb_qtasco.AppearanceCell.Options.UseTextOptions = True
    Me.tb_qtasco.Caption = "Pezzi in sconto"
    Me.tb_qtasco.Enabled = True
    Me.tb_qtasco.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_qtasco.Name = "tb_qtasco"
    Me.tb_qtasco.NTSRepositoryComboBox = Nothing
    Me.tb_qtasco.NTSRepositoryItemCheck = Nothing
    Me.tb_qtasco.NTSRepositoryItemText = Nothing
    Me.tb_qtasco.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_qtasco.OptionsFilter.AllowFilter = False
    '
    'tb_implim
    '
    Me.tb_implim.AppearanceCell.Options.UseBackColor = True
    Me.tb_implim.AppearanceCell.Options.UseTextOptions = True
    Me.tb_implim.Caption = "Cifra limite"
    Me.tb_implim.Enabled = True
    Me.tb_implim.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_implim.Name = "tb_implim"
    Me.tb_implim.NTSRepositoryComboBox = Nothing
    Me.tb_implim.NTSRepositoryItemCheck = Nothing
    Me.tb_implim.NTSRepositoryItemText = Nothing
    Me.tb_implim.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_implim.OptionsFilter.AllowFilter = False
    '
    'tb_qtalim
    '
    Me.tb_qtalim.AppearanceCell.Options.UseBackColor = True
    Me.tb_qtalim.AppearanceCell.Options.UseTextOptions = True
    Me.tb_qtalim.Caption = "N. Pezzi limite"
    Me.tb_qtalim.Enabled = True
    Me.tb_qtalim.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_qtalim.Name = "tb_qtalim"
    Me.tb_qtalim.NTSRepositoryComboBox = Nothing
    Me.tb_qtalim.NTSRepositoryItemCheck = Nothing
    Me.tb_qtalim.NTSRepositoryItemText = Nothing
    Me.tb_qtalim.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_qtalim.OptionsFilter.AllowFilter = False
    '
    'tb_datagg
    '
    Me.tb_datagg.AppearanceCell.Options.UseBackColor = True
    Me.tb_datagg.AppearanceCell.Options.UseTextOptions = True
    Me.tb_datagg.Caption = "Data inizio validità"
    Me.tb_datagg.Enabled = True
    Me.tb_datagg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_datagg.Name = "tb_datagg"
    Me.tb_datagg.NTSRepositoryComboBox = Nothing
    Me.tb_datagg.NTSRepositoryItemCheck = Nothing
    Me.tb_datagg.NTSRepositoryItemText = Nothing
    Me.tb_datagg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_datagg.OptionsFilter.AllowFilter = False
    Me.tb_datagg.Visible = True
    Me.tb_datagg.VisibleIndex = 2
    '
    'tb_datscad
    '
    Me.tb_datscad.AppearanceCell.Options.UseBackColor = True
    Me.tb_datscad.AppearanceCell.Options.UseTextOptions = True
    Me.tb_datscad.Caption = "Data fine validità"
    Me.tb_datscad.Enabled = True
    Me.tb_datscad.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_datscad.Name = "tb_datscad"
    Me.tb_datscad.NTSRepositoryComboBox = Nothing
    Me.tb_datscad.NTSRepositoryItemCheck = Nothing
    Me.tb_datscad.NTSRepositoryItemText = Nothing
    Me.tb_datscad.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_datscad.OptionsFilter.AllowFilter = False
    Me.tb_datscad.Visible = True
    Me.tb_datscad.VisibleIndex = 3
    '
    'tb_note
    '
    Me.tb_note.AppearanceCell.Options.UseBackColor = True
    Me.tb_note.AppearanceCell.Options.UseTextOptions = True
    Me.tb_note.Caption = "Note"
    Me.tb_note.Enabled = True
    Me.tb_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_note.Name = "tb_note"
    Me.tb_note.NTSRepositoryComboBox = Nothing
    Me.tb_note.NTSRepositoryItemCheck = Nothing
    Me.tb_note.NTSRepositoryItemText = Nothing
    Me.tb_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_note.OptionsFilter.AllowFilter = False
    Me.tb_note.Visible = True
    Me.tb_note.VisibleIndex = 4
    '
    'FRMMGPROM
    '
    Me.ClientSize = New System.Drawing.Size(622, 404)
    Me.Controls.Add(Me.pnProm)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.HelpButton = True
    Me.Name = "FRMMGPROM"
    Me.NTSLastControlFocussed = Me.grProm
    Me.Text = "PROMOZIONI ARTICOLI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnProm, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnProm.ResumeLayout(False)
    CType(Me.grProm, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvProm, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

#Region "Form"

  Public Overridable Sub FRMMGPROM_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcProm.Dispose()
      If Not dsProm Is Nothing Then dsProm.Dispose()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGPROM_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva(True) Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGPROM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try

      'Predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'Leggo dal database i dati e collego l'NTSBinding
      If oCleProm.Apri(DittaCorrente, dsProm) Then

        dcProm.DataSource = dsProm.Tables("TABTPRO")
        dsProm.AcceptChanges()

        grProm.DataSource = dcProm

        GctlSetRoules()

        AbilitaTlbSecondoGriglia()
        tlbSalva.Enabled = False
        tlbRipristina.Enabled = False

        '--------------------------------------------
        'sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
        If Not oCallParams Is Nothing Then
          If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
            If grvProm.NTSAllowInsert Then
              grvProm.NTSNuovo()
            End If
          ElseIf Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) <> "" Then
            For i As Integer = 0 To dcProm.List.Count - 1
              If CType(dcProm.Item(i), DataRowView)!tb_codtpro.ToString = Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) Then
                dcProm.Position = i
                Exit For
              End If
            Next
          End If
        End If  'If Not oCallParams Is Nothing Then

      Else
        Me.Close()
      End If


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

#Region "Griglia"

  Public Overridable Sub grvProm_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvProm.NTSFocusedRowChanged
    Try
      'Se il codice è diverso da 0 blocco la colonna, diversamente la rendo editabile
      If NTSCInt(grvProm.GetFocusedRowCellValue(tb_codtpro).ToString.Trim) <> 0 Then
        tb_codtpro.Enabled = False
      Else
        'tb_codtpro.Enabled = true '!!!CON QUESTA LA GRIGLIA NON FUNZIONA CORRETTAMENTE
        GctlSetVisEnab(tb_codtpro, False)
      End If
      If grvProm.FocusedRowHandle = -999998 Then
        tlbCancella.Enabled = False
        tlbDettagli.Enabled = False
      Else
        tlbCancella.Enabled = True
        tlbNuovo.Enabled = True
        tlbDettagli.Enabled = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvProm_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvProm.NTSBeforeRowUpdate
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

  Public Overridable Sub grvProm_CellValueChanging(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvProm.CellValueChanging
    Try
      tlbRipristina.Enabled = True
      tlbSalva.Enabled = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub grvProm_NTSNewRow(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grvProm.NTSNewRow
    Try
      tlbCancella.Enabled = False
      tlbNuovo.Enabled = False
      tlbDettagli.Enabled = False
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
      grvProm.NTSNuovo()
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
      If grvProm.NTSGetCurrentDataRow Is Nothing Then Return

      If Not oCleProm.TestPreCancella(grvProm.NTSGetCurrentDataRow) Then

        strAddText = oApp.Tr(Me, 128510849243281250, "Attenzione!" & vbCrLf & _
      "Esistono dei dettagli per questa promozione articolo, procedendo con la cancellazione" & vbCrLf & _
      "della promozione verranno cancellati anche suddetti dettaglio!" & vbCrLf & vbCrLf)

      End If

      If oApp.MsgBoxInfoYesNo_DefYes(strAddText & oApp.Tr(Me, 128514428272500000, "Vuoi procedere con la cancellazione del record?")) = Windows.Forms.DialogResult.Yes Then

        If grvProm.NTSDeleteRigaCorrente(dcProm, False) Then
          oCleProm.Salva(True)
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
      If Not grvProm.NTSRipristinaRigaCorrenteBefore(dcProm, CBool(IIf(sender Is Nothing, False, True))) Then Return
      oCleProm.Ripristina(dcProm.Position, dcProm.Filter)
      grvProm.NTSRipristinaRigaCorrenteAfter()
      AbilitaTlbSecondoGriglia()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDettagli_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDettagli.ItemClick
    Dim oParam As New CLE__CLDP
    Dim frmMgarpr As FRMMGARPR = Nothing
    Try
      If Not Salva(False) Then Return

      If grvProm.NTSGetCurrentDataRow Is Nothing Then Return

      oParam.ctlPar1 = grvProm.NTSGetCurrentDataRow
      frmMgarpr = CType(NTSNewFormModal("FRMMGARPR"), FRMMGARPR)
      frmMgarpr.Init(oMenu, oParam, DittaCorrente)
      frmMgarpr.InitEntity(oCleProm)
      frmMgarpr.ShowDialog()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmMgarpr Is Nothing Then frmMgarpr.Dispose()
      frmMgarpr = Nothing
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

      dRes = grvProm.NTSSalvaRigaCorrente(dcProm, oCleProm.RecordIsChanged, False)

      If bEsci Then
        If dRes = Windows.Forms.DialogResult.Yes Then
          If grvProm.NTSGetCurrentDataRow.RowState = DataRowState.Added Then
            dRes = oApp.MsgBoxInfoYesNoCancel_DefYes(oApp.Tr(Me, 128511037688750000, "Salvare la nuova lista promozione prima di uscire?"))
          Else
            dRes = oApp.MsgBoxInfoYesNoCancel_DefYes(oApp.Tr(Me, 128511039167656250, "Salvare le modifiche apportate alla promozione prima di uscire?"))
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

          If Not oCleProm.Salva(False) Then Return False

          AbilitaTlbSecondoGriglia()
          tb_codtpro.Enabled = False

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
    'Dim strCrpe As String = ""
    Dim i As Integer

    Try
      If Not Salva(False) Then Return

      '--------------------------------------------------
      'Preparo il motore di stampa
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGPROM", "Reports1", " ", 0, nDestin, "BSMGPROM.RPT", False, "PROMOZIONI ARTICOLI", False)
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
      If dsProm.Tables("TABTPRO").Rows.Count = 0 Then
        tlbDettagli.Enabled = False
        tlbStampa.Enabled = False
        tlbStampaVideo.Enabled = False
        tlbCancella.Enabled = False
      Else
        tlbDettagli.Enabled = True
        tlbStampa.Enabled = True
        tlbStampaVideo.Enabled = True
        tlbCancella.Enabled = True
      End If
      If oCleProm.RecordIsChanged Then
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
