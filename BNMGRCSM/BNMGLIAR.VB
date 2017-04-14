Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGLIAR
  Public oCleRcsm As CLEMGRCSM
  Public oCallParams As CLE__CLDP

  Public bAnnullato As Boolean = False
  Public bOk As Boolean = False
  Public bTrend As Boolean = False

  Public strAammggIni As String = ""
  Public strAammggFin As String = ""
  Public strPrimoPeriodoIni As String = ""
  Public strPrimoPeriodoFin As String = ""
  Public strSecondoPeriodoIni As String = ""
  Public strSecondoPeriodoFin As String = ""

  Public dsLiar As DataSet
  Public dcLiar As BindingSource = New BindingSource()

  Private components As System.ComponentModel.IContainer

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

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '--------------------------------------------------------------------------------------------------------------
      Try
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      '--------------------------------------------------------------------------------------------------------------
      tlbMain.NTSSetToolTip()
      '--------------------------------------------------------------------------------------------------------------
      Dim dttPolriord As New DataTable()
      dttPolriord.Columns.Add("cod", GetType(String))
      dttPolriord.Columns.Add("val", GetType(String))
      dttPolriord.Rows.Add(New Object() {"F", "Su fabbisogno con lotto"})
      dttPolriord.Rows.Add(New Object() {"G", "Su fabbisogno puro"})
      dttPolriord.Rows.Add(New Object() {"M", "A punto di riordino con lotto"})
      dttPolriord.Rows.Add(New Object() {"N", "A punto di riordino a ric. scorta"})
      dttPolriord.Rows.Add(New Object() {"O", "Su fabbisogno con lotto minimo"})
      dttPolriord.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      grvLiar.NTSSetParam(oMenu, oApp.Tr(Me, 128577287111245259, "Griglia Lista Articoli Movimentati"))
      '--------------------------------------------------------------------------------------------------------------
      ar_codart.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 130175053184041896, "Articolo"), tabartico, True)
      ar_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130175053507374624, "Descrizione articolo"), 0, True)
      ar_polriord.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128577398402576142, "Politica di riordino"), dttPolriord, "val", "cod")
      ar_conver.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128577295563345510, "Qtà scartata"), oApp.FormatQta, 0, -999999999, 999999999)
      ar_qtacon2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128577295582350146, "Qtà media giornaliera"), oApp.FormatQta, 0, -999999999, 999999999)
      ar_pesoca.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128577295600270756, "Nuova scorta minima"), oApp.FormatQta, 0, -999999999, 999999999)
      ar_scomin.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128577295616609274, "Scorta minima originaria"), oApp.FormatQta, 0, -999999999, 999999999)
      ar_rrfence.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128577295636746766, "RR Fence"), oApp.FormatQta, 0, -999999999, 999999999)
      ar_minord.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128577295655184974, "Qtà lotto standard"), oApp.FormatQta, 0, -999999999, 999999999)
      ar_scomax.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128577295671572322, "Scorta massima originaria"), oApp.FormatQta, 0, -999999999, 999999999)
      ar_misura1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128577295701036344, "Qtà impegni residui"), oApp.FormatQta, 0, -999999999, 999999999)
      ar_misura2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128577295718185440, "Qtà imp. tot. periodo"), oApp.FormatQta, 0, -999999999, 999999999)
      ar_ricar1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128577295744348554, "Esistenza"), oApp.FormatQta, 0, -999999999, 999999999)
      ar_ricar2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128577295773812576, "Disp. netta"), oApp.FormatQta, 0, -999999999, 999999999)
      ar_misura3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128577295803442620, "Disponibilità"), oApp.FormatQta, 0, -999999999, 999999999)
      ar_pesonet.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129006800399887270, "Scarichi rettificati col trend"), oApp.FormatQta, 0, -999999999, 999999999)
      ar_volume.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129006887392206015, "Percentuale incremento/decremento trend"), "+#,##0.00;-#,##0.00", 0, -999999999, 999999999)
      ar_magaz.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 130175052485118574, "Magazzino"), tabmaga)
      xx_magaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130175053765431408, "Descrizione magazzino"), 0, True)
      ar_fase.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130175053875972762, "Fase"), "0", 4, 0, 9999)
      xx_fase.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130175054166628454, "Descrizione fase"), 0, True)
      '--------------------------------------------------------------------------------------------------------------
      ar_pesoca.NTSSetRichiesto()
      '--------------------------------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try

    InitControlsBeginEndInit(Me, True)
  End Sub

  Public Overridable Sub InitEntity(ByRef cleRcsm As CLEMGRCSM)
    oCleRcsm = cleRcsm
    AddHandler oCleRcsm.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGLIAR))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarMenuItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grLiar = New NTSInformatica.NTSGrid
    Me.grvLiar = New NTSInformatica.NTSGridView
    Me.ar_magaz = New NTSInformatica.NTSGridColumn
    Me.xx_magaz = New NTSInformatica.NTSGridColumn
    Me.ar_fase = New NTSInformatica.NTSGridColumn
    Me.xx_fase = New NTSInformatica.NTSGridColumn
    Me.ar_codart = New NTSInformatica.NTSGridColumn
    Me.ar_descr = New NTSInformatica.NTSGridColumn
    Me.ar_unmis = New NTSInformatica.NTSGridColumn
    Me.ar_conver = New NTSInformatica.NTSGridColumn
    Me.ar_qtacon2 = New NTSInformatica.NTSGridColumn
    Me.ar_pesoca = New NTSInformatica.NTSGridColumn
    Me.ar_scomin = New NTSInformatica.NTSGridColumn
    Me.ar_rrfence = New NTSInformatica.NTSGridColumn
    Me.ar_polriord = New NTSInformatica.NTSGridColumn
    Me.ar_minord = New NTSInformatica.NTSGridColumn
    Me.ar_sublotto = New NTSInformatica.NTSGridColumn
    Me.ar_scomax = New NTSInformatica.NTSGridColumn
    Me.ar_misura1 = New NTSInformatica.NTSGridColumn
    Me.ar_misura2 = New NTSInformatica.NTSGridColumn
    Me.ar_ricar1 = New NTSInformatica.NTSGridColumn
    Me.ar_ricar2 = New NTSInformatica.NTSGridColumn
    Me.ar_misura3 = New NTSInformatica.NTSGridColumn
    Me.ar_pesonet = New NTSInformatica.NTSGridColumn
    Me.ar_volume = New NTSInformatica.NTSGridColumn
    Me.pnPanel1 = New NTSInformatica.NTSPanel
    Me.pnPanel2 = New NTSInformatica.NTSPanel
    Me.edAammggFin = New NTSInformatica.NTSLabel
    Me.edAammggIni = New NTSInformatica.NTSLabel
    Me.lbAammggFin = New NTSInformatica.NTSLabel
    Me.lbAammggIni = New NTSInformatica.NTSLabel
    Me.pnPulsanti = New NTSInformatica.NTSPanel
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.fmTrend = New NTSInformatica.NTSGroupBox
    Me.edSecondoPeriodoFin = New NTSInformatica.NTSLabel
    Me.edSecondoPeriodoIni = New NTSInformatica.NTSLabel
    Me.edPrimoPeriodoFin = New NTSInformatica.NTSLabel
    Me.edPrimoPeriodoIni = New NTSInformatica.NTSLabel
    Me.lbSecondoPeriodoFin = New NTSInformatica.NTSLabel
    Me.lbPrimoPeriodoFin = New NTSInformatica.NTSLabel
    Me.lbSecondoPeriodoIni = New NTSInformatica.NTSLabel
    Me.lbPrimoPeriodoIni = New NTSInformatica.NTSLabel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grLiar, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvLiar, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPanel1.SuspendLayout()
    CType(Me.pnPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPanel2.SuspendLayout()
    CType(Me.pnPulsanti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPulsanti.SuspendLayout()
    CType(Me.fmTrend, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmTrend.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbRipristina, Me.tlbCancella, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbGuida, Me.tlbEsci, Me.tlbStampa, Me.tlbStampaVideo})
    Me.NtsBarManager1.MaxItemId = 13
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.Id = 0
    Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.Id = 1
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
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 7
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.Id = 8
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.NTSIsCheckBox = False
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
    Me.tlbStampaVideo.Caption = "Stampa a Video"
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
    Me.tlbGuida.Id = 9
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 10
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'grLiar
    '
    Me.grLiar.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grLiar.EmbeddedNavigator.Name = ""
    Me.grLiar.Location = New System.Drawing.Point(0, 0)
    Me.grLiar.MainView = Me.grvLiar
    Me.grLiar.Name = "grLiar"
    Me.grLiar.Size = New System.Drawing.Size(724, 333)
    Me.grLiar.TabIndex = 4
    Me.grLiar.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvLiar})
    '
    'grvLiar
    '
    Me.grvLiar.ActiveFilterEnabled = False
    '
    'ar_magaz
    '
    Me.ar_magaz.AppearanceCell.Options.UseBackColor = True
    Me.ar_magaz.AppearanceCell.Options.UseTextOptions = True
    Me.ar_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_magaz.Caption = "Magazzino"
    Me.ar_magaz.Enabled = False
    Me.ar_magaz.FieldName = "ar_magaz"
    Me.ar_magaz.Name = "ar_magaz"
    Me.ar_magaz.NTSRepositoryComboBox = Nothing
    Me.ar_magaz.NTSRepositoryItemCheck = Nothing
    Me.ar_magaz.NTSRepositoryItemMemo = Nothing
    Me.ar_magaz.NTSRepositoryItemText = Nothing
    Me.ar_magaz.OptionsColumn.AllowEdit = False
    Me.ar_magaz.OptionsColumn.ReadOnly = True
    Me.ar_magaz.Visible = True
    Me.ar_magaz.VisibleIndex = 18
    '
    'xx_magaz
    '
    Me.xx_magaz.AppearanceCell.Options.UseBackColor = True
    Me.xx_magaz.AppearanceCell.Options.UseTextOptions = True
    Me.xx_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_magaz.Caption = "Descr. magazz."
    Me.xx_magaz.Enabled = False
    Me.xx_magaz.FieldName = "xx_magaz"
    Me.xx_magaz.Name = "xx_magaz"
    Me.xx_magaz.NTSRepositoryComboBox = Nothing
    Me.xx_magaz.NTSRepositoryItemCheck = Nothing
    Me.xx_magaz.NTSRepositoryItemMemo = Nothing
    Me.xx_magaz.NTSRepositoryItemText = Nothing
    Me.xx_magaz.OptionsColumn.AllowEdit = False
    Me.xx_magaz.OptionsColumn.ReadOnly = True
    Me.xx_magaz.Visible = True
    Me.xx_magaz.VisibleIndex = 19
    '
    'ar_fase
    '
    Me.ar_fase.AppearanceCell.Options.UseBackColor = True
    Me.ar_fase.AppearanceCell.Options.UseTextOptions = True
    Me.ar_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_fase.Caption = "Fase"
    Me.ar_fase.Enabled = False
    Me.ar_fase.FieldName = "ar_fase"
    Me.ar_fase.Name = "ar_fase"
    Me.ar_fase.NTSRepositoryComboBox = Nothing
    Me.ar_fase.NTSRepositoryItemCheck = Nothing
    Me.ar_fase.NTSRepositoryItemMemo = Nothing
    Me.ar_fase.NTSRepositoryItemText = Nothing
    Me.ar_fase.OptionsColumn.AllowEdit = False
    Me.ar_fase.OptionsColumn.ReadOnly = True
    Me.ar_fase.Visible = True
    Me.ar_fase.VisibleIndex = 20
    '
    'xx_fase
    '
    Me.xx_fase.AppearanceCell.Options.UseBackColor = True
    Me.xx_fase.AppearanceCell.Options.UseTextOptions = True
    Me.xx_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_fase.Caption = "Descr. fase"
    Me.xx_fase.Enabled = False
    Me.xx_fase.FieldName = "xx_fase"
    Me.xx_fase.Name = "xx_fase"
    Me.xx_fase.NTSRepositoryComboBox = Nothing
    Me.xx_fase.NTSRepositoryItemCheck = Nothing
    Me.xx_fase.NTSRepositoryItemMemo = Nothing
    Me.xx_fase.NTSRepositoryItemText = Nothing
    Me.xx_fase.OptionsColumn.AllowEdit = False
    Me.xx_fase.OptionsColumn.ReadOnly = True
    Me.xx_fase.Visible = True
    Me.xx_fase.VisibleIndex = 21
    Me.grvLiar.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ar_codart, Me.ar_descr, Me.ar_unmis, Me.ar_conver, Me.ar_qtacon2, Me.ar_pesoca, Me.ar_scomin, Me.ar_rrfence, Me.ar_polriord, Me.ar_minord, Me.ar_sublotto, Me.ar_scomax, Me.ar_misura1, Me.ar_misura2, Me.ar_ricar1, Me.ar_ricar2, Me.ar_misura3, Me.ar_pesonet, Me.ar_volume, Me.ar_magaz, Me.xx_magaz, Me.ar_fase, Me.xx_fase})
    Me.grvLiar.CustomizationFormBounds = New System.Drawing.Rectangle(592, 326, 208, 170)
    Me.grvLiar.Enabled = True
    Me.grvLiar.GridControl = Me.grLiar
    Me.grvLiar.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvLiar.Name = "grvLiar"
    Me.grvLiar.NTSAllowDelete = True
    Me.grvLiar.NTSAllowInsert = True
    Me.grvLiar.NTSAllowUpdate = True
    Me.grvLiar.NTSMenuContext = Nothing
    Me.grvLiar.OptionsCustomization.AllowRowSizing = True
    Me.grvLiar.OptionsFilter.AllowFilterEditor = False
    Me.grvLiar.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvLiar.OptionsNavigation.UseTabKey = False
    Me.grvLiar.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvLiar.OptionsView.ColumnAutoWidth = False
    Me.grvLiar.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvLiar.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvLiar.OptionsView.ShowGroupPanel = False
    Me.grvLiar.RowHeight = 14
    '
    'ar_codart
    '
    Me.ar_codart.AppearanceCell.Options.UseBackColor = True
    Me.ar_codart.AppearanceCell.Options.UseTextOptions = True
    Me.ar_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_codart.Caption = "Articolo"
    Me.ar_codart.Enabled = False
    Me.ar_codart.FieldName = "ar_codart"
    Me.ar_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_codart.Name = "ar_codart"
    Me.ar_codart.NTSRepositoryComboBox = Nothing
    Me.ar_codart.NTSRepositoryItemCheck = Nothing
    Me.ar_codart.NTSRepositoryItemMemo = Nothing
    Me.ar_codart.NTSRepositoryItemText = Nothing
    Me.ar_codart.OptionsColumn.AllowEdit = False
    Me.ar_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_codart.OptionsColumn.ReadOnly = True
    Me.ar_codart.OptionsFilter.AllowFilter = False
    Me.ar_codart.Visible = True
    Me.ar_codart.VisibleIndex = 0
    '
    'ar_descr
    '
    Me.ar_descr.AppearanceCell.Options.UseBackColor = True
    Me.ar_descr.AppearanceCell.Options.UseTextOptions = True
    Me.ar_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_descr.Caption = "Descrizione"
    Me.ar_descr.Enabled = False
    Me.ar_descr.FieldName = "ar_descr"
    Me.ar_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_descr.Name = "ar_descr"
    Me.ar_descr.NTSRepositoryComboBox = Nothing
    Me.ar_descr.NTSRepositoryItemCheck = Nothing
    Me.ar_descr.NTSRepositoryItemMemo = Nothing
    Me.ar_descr.NTSRepositoryItemText = Nothing
    Me.ar_descr.OptionsColumn.AllowEdit = False
    Me.ar_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_descr.OptionsColumn.ReadOnly = True
    Me.ar_descr.OptionsFilter.AllowFilter = False
    Me.ar_descr.Visible = True
    Me.ar_descr.VisibleIndex = 1
    '
    'ar_unmis
    '
    Me.ar_unmis.AppearanceCell.Options.UseBackColor = True
    Me.ar_unmis.AppearanceCell.Options.UseTextOptions = True
    Me.ar_unmis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_unmis.Caption = "U.M."
    Me.ar_unmis.Enabled = False
    Me.ar_unmis.FieldName = "ar_unmis"
    Me.ar_unmis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_unmis.Name = "ar_unmis"
    Me.ar_unmis.NTSRepositoryComboBox = Nothing
    Me.ar_unmis.NTSRepositoryItemCheck = Nothing
    Me.ar_unmis.NTSRepositoryItemMemo = Nothing
    Me.ar_unmis.NTSRepositoryItemText = Nothing
    Me.ar_unmis.OptionsColumn.AllowEdit = False
    Me.ar_unmis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_unmis.OptionsColumn.ReadOnly = True
    Me.ar_unmis.OptionsFilter.AllowFilter = False
    '
    'ar_conver
    '
    Me.ar_conver.AppearanceCell.Options.UseBackColor = True
    Me.ar_conver.AppearanceCell.Options.UseTextOptions = True
    Me.ar_conver.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_conver.Caption = "Qtà scaricata"
    Me.ar_conver.Enabled = False
    Me.ar_conver.FieldName = "ar_conver"
    Me.ar_conver.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_conver.Name = "ar_conver"
    Me.ar_conver.NTSRepositoryComboBox = Nothing
    Me.ar_conver.NTSRepositoryItemCheck = Nothing
    Me.ar_conver.NTSRepositoryItemMemo = Nothing
    Me.ar_conver.NTSRepositoryItemText = Nothing
    Me.ar_conver.OptionsColumn.AllowEdit = False
    Me.ar_conver.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_conver.OptionsColumn.ReadOnly = True
    Me.ar_conver.OptionsFilter.AllowFilter = False
    Me.ar_conver.Visible = True
    Me.ar_conver.VisibleIndex = 2
    '
    'ar_qtacon2
    '
    Me.ar_qtacon2.AppearanceCell.Options.UseBackColor = True
    Me.ar_qtacon2.AppearanceCell.Options.UseTextOptions = True
    Me.ar_qtacon2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_qtacon2.Caption = "Qtà media giornaliera"
    Me.ar_qtacon2.Enabled = False
    Me.ar_qtacon2.FieldName = "ar_qtacon2"
    Me.ar_qtacon2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_qtacon2.Name = "ar_qtacon2"
    Me.ar_qtacon2.NTSRepositoryComboBox = Nothing
    Me.ar_qtacon2.NTSRepositoryItemCheck = Nothing
    Me.ar_qtacon2.NTSRepositoryItemMemo = Nothing
    Me.ar_qtacon2.NTSRepositoryItemText = Nothing
    Me.ar_qtacon2.OptionsColumn.AllowEdit = False
    Me.ar_qtacon2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_qtacon2.OptionsColumn.ReadOnly = True
    Me.ar_qtacon2.OptionsFilter.AllowFilter = False
    Me.ar_qtacon2.Visible = True
    Me.ar_qtacon2.VisibleIndex = 3
    '
    'ar_pesoca
    '
    Me.ar_pesoca.AppearanceCell.Options.UseBackColor = True
    Me.ar_pesoca.AppearanceCell.Options.UseTextOptions = True
    Me.ar_pesoca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_pesoca.Caption = "Nuova scorta minima"
    Me.ar_pesoca.Enabled = True
    Me.ar_pesoca.FieldName = "ar_pesoca"
    Me.ar_pesoca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_pesoca.Name = "ar_pesoca"
    Me.ar_pesoca.NTSRepositoryComboBox = Nothing
    Me.ar_pesoca.NTSRepositoryItemCheck = Nothing
    Me.ar_pesoca.NTSRepositoryItemMemo = Nothing
    Me.ar_pesoca.NTSRepositoryItemText = Nothing
    Me.ar_pesoca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_pesoca.OptionsFilter.AllowFilter = False
    Me.ar_pesoca.Visible = True
    Me.ar_pesoca.VisibleIndex = 4
    '
    'ar_scomin
    '
    Me.ar_scomin.AppearanceCell.Options.UseBackColor = True
    Me.ar_scomin.AppearanceCell.Options.UseTextOptions = True
    Me.ar_scomin.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_scomin.Caption = "Scorta minima originaria"
    Me.ar_scomin.Enabled = False
    Me.ar_scomin.FieldName = "ar_scomin"
    Me.ar_scomin.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_scomin.Name = "ar_scomin"
    Me.ar_scomin.NTSRepositoryComboBox = Nothing
    Me.ar_scomin.NTSRepositoryItemCheck = Nothing
    Me.ar_scomin.NTSRepositoryItemMemo = Nothing
    Me.ar_scomin.NTSRepositoryItemText = Nothing
    Me.ar_scomin.OptionsColumn.AllowEdit = False
    Me.ar_scomin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_scomin.OptionsColumn.ReadOnly = True
    Me.ar_scomin.OptionsFilter.AllowFilter = False
    Me.ar_scomin.Visible = True
    Me.ar_scomin.VisibleIndex = 5
    '
    'ar_rrfence
    '
    Me.ar_rrfence.AppearanceCell.Options.UseBackColor = True
    Me.ar_rrfence.AppearanceCell.Options.UseTextOptions = True
    Me.ar_rrfence.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_rrfence.Caption = "RR Fence"
    Me.ar_rrfence.Enabled = False
    Me.ar_rrfence.FieldName = "ar_rrfence"
    Me.ar_rrfence.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_rrfence.Name = "ar_rrfence"
    Me.ar_rrfence.NTSRepositoryComboBox = Nothing
    Me.ar_rrfence.NTSRepositoryItemCheck = Nothing
    Me.ar_rrfence.NTSRepositoryItemMemo = Nothing
    Me.ar_rrfence.NTSRepositoryItemText = Nothing
    Me.ar_rrfence.OptionsColumn.AllowEdit = False
    Me.ar_rrfence.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_rrfence.OptionsColumn.ReadOnly = True
    Me.ar_rrfence.OptionsFilter.AllowFilter = False
    Me.ar_rrfence.Visible = True
    Me.ar_rrfence.VisibleIndex = 6
    '
    'ar_polriord
    '
    Me.ar_polriord.AppearanceCell.Options.UseBackColor = True
    Me.ar_polriord.AppearanceCell.Options.UseTextOptions = True
    Me.ar_polriord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_polriord.Caption = "Politica di riordino"
    Me.ar_polriord.Enabled = False
    Me.ar_polriord.FieldName = "ar_polriord"
    Me.ar_polriord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_polriord.Name = "ar_polriord"
    Me.ar_polriord.NTSRepositoryComboBox = Nothing
    Me.ar_polriord.NTSRepositoryItemCheck = Nothing
    Me.ar_polriord.NTSRepositoryItemMemo = Nothing
    Me.ar_polriord.NTSRepositoryItemText = Nothing
    Me.ar_polriord.OptionsColumn.AllowEdit = False
    Me.ar_polriord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_polriord.OptionsColumn.ReadOnly = True
    Me.ar_polriord.OptionsFilter.AllowFilter = False
    Me.ar_polriord.Visible = True
    Me.ar_polriord.VisibleIndex = 7
    '
    'ar_minord
    '
    Me.ar_minord.AppearanceCell.Options.UseBackColor = True
    Me.ar_minord.AppearanceCell.Options.UseTextOptions = True
    Me.ar_minord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_minord.Caption = "Qtà lotto standard"
    Me.ar_minord.Enabled = False
    Me.ar_minord.FieldName = "ar_minord"
    Me.ar_minord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_minord.Name = "ar_minord"
    Me.ar_minord.NTSRepositoryComboBox = Nothing
    Me.ar_minord.NTSRepositoryItemCheck = Nothing
    Me.ar_minord.NTSRepositoryItemMemo = Nothing
    Me.ar_minord.NTSRepositoryItemText = Nothing
    Me.ar_minord.OptionsColumn.AllowEdit = False
    Me.ar_minord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_minord.OptionsColumn.ReadOnly = True
    Me.ar_minord.OptionsFilter.AllowFilter = False
    Me.ar_minord.Visible = True
    Me.ar_minord.VisibleIndex = 8
    '
    'ar_sublotto
    '
    Me.ar_sublotto.AppearanceCell.Options.UseBackColor = True
    Me.ar_sublotto.AppearanceCell.Options.UseTextOptions = True
    Me.ar_sublotto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_sublotto.Caption = "Sottolotto"
    Me.ar_sublotto.Enabled = False
    Me.ar_sublotto.FieldName = "ar_sublotto"
    Me.ar_sublotto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_sublotto.Name = "ar_sublotto"
    Me.ar_sublotto.NTSRepositoryComboBox = Nothing
    Me.ar_sublotto.NTSRepositoryItemCheck = Nothing
    Me.ar_sublotto.NTSRepositoryItemMemo = Nothing
    Me.ar_sublotto.NTSRepositoryItemText = Nothing
    Me.ar_sublotto.OptionsColumn.AllowEdit = False
    Me.ar_sublotto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_sublotto.OptionsColumn.ReadOnly = True
    Me.ar_sublotto.OptionsFilter.AllowFilter = False
    Me.ar_sublotto.Visible = True
    Me.ar_sublotto.VisibleIndex = 9
    '
    'ar_scomax
    '
    Me.ar_scomax.AppearanceCell.Options.UseBackColor = True
    Me.ar_scomax.AppearanceCell.Options.UseTextOptions = True
    Me.ar_scomax.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_scomax.Caption = "Scorta massima originaria"
    Me.ar_scomax.Enabled = False
    Me.ar_scomax.FieldName = "ar_scomax"
    Me.ar_scomax.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_scomax.Name = "ar_scomax"
    Me.ar_scomax.NTSRepositoryComboBox = Nothing
    Me.ar_scomax.NTSRepositoryItemCheck = Nothing
    Me.ar_scomax.NTSRepositoryItemMemo = Nothing
    Me.ar_scomax.NTSRepositoryItemText = Nothing
    Me.ar_scomax.OptionsColumn.AllowEdit = False
    Me.ar_scomax.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_scomax.OptionsColumn.ReadOnly = True
    Me.ar_scomax.OptionsFilter.AllowFilter = False
    Me.ar_scomax.Visible = True
    Me.ar_scomax.VisibleIndex = 10
    '
    'ar_misura1
    '
    Me.ar_misura1.AppearanceCell.Options.UseBackColor = True
    Me.ar_misura1.AppearanceCell.Options.UseTextOptions = True
    Me.ar_misura1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_misura1.Caption = "Qtà impegni residui"
    Me.ar_misura1.Enabled = False
    Me.ar_misura1.FieldName = "ar_misura1"
    Me.ar_misura1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_misura1.Name = "ar_misura1"
    Me.ar_misura1.NTSRepositoryComboBox = Nothing
    Me.ar_misura1.NTSRepositoryItemCheck = Nothing
    Me.ar_misura1.NTSRepositoryItemMemo = Nothing
    Me.ar_misura1.NTSRepositoryItemText = Nothing
    Me.ar_misura1.OptionsColumn.AllowEdit = False
    Me.ar_misura1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_misura1.OptionsColumn.ReadOnly = True
    Me.ar_misura1.OptionsFilter.AllowFilter = False
    Me.ar_misura1.Visible = True
    Me.ar_misura1.VisibleIndex = 11
    '
    'ar_misura2
    '
    Me.ar_misura2.AppearanceCell.Options.UseBackColor = True
    Me.ar_misura2.AppearanceCell.Options.UseTextOptions = True
    Me.ar_misura2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_misura2.Caption = "Qtà imp. tot. periodo"
    Me.ar_misura2.Enabled = False
    Me.ar_misura2.FieldName = "ar_misura2"
    Me.ar_misura2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_misura2.Name = "ar_misura2"
    Me.ar_misura2.NTSRepositoryComboBox = Nothing
    Me.ar_misura2.NTSRepositoryItemCheck = Nothing
    Me.ar_misura2.NTSRepositoryItemMemo = Nothing
    Me.ar_misura2.NTSRepositoryItemText = Nothing
    Me.ar_misura2.OptionsColumn.AllowEdit = False
    Me.ar_misura2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_misura2.OptionsColumn.ReadOnly = True
    Me.ar_misura2.OptionsFilter.AllowFilter = False
    Me.ar_misura2.Visible = True
    Me.ar_misura2.VisibleIndex = 12
    '
    'ar_ricar1
    '
    Me.ar_ricar1.AppearanceCell.Options.UseBackColor = True
    Me.ar_ricar1.AppearanceCell.Options.UseTextOptions = True
    Me.ar_ricar1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_ricar1.Caption = "Esistenza"
    Me.ar_ricar1.Enabled = False
    Me.ar_ricar1.FieldName = "ar_ricar1"
    Me.ar_ricar1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_ricar1.Name = "ar_ricar1"
    Me.ar_ricar1.NTSRepositoryComboBox = Nothing
    Me.ar_ricar1.NTSRepositoryItemCheck = Nothing
    Me.ar_ricar1.NTSRepositoryItemMemo = Nothing
    Me.ar_ricar1.NTSRepositoryItemText = Nothing
    Me.ar_ricar1.OptionsColumn.AllowEdit = False
    Me.ar_ricar1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_ricar1.OptionsColumn.ReadOnly = True
    Me.ar_ricar1.OptionsFilter.AllowFilter = False
    Me.ar_ricar1.Visible = True
    Me.ar_ricar1.VisibleIndex = 13
    '
    'ar_ricar2
    '
    Me.ar_ricar2.AppearanceCell.Options.UseBackColor = True
    Me.ar_ricar2.AppearanceCell.Options.UseTextOptions = True
    Me.ar_ricar2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_ricar2.Caption = "Disp. netta"
    Me.ar_ricar2.Enabled = False
    Me.ar_ricar2.FieldName = "ar_ricar2"
    Me.ar_ricar2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_ricar2.Name = "ar_ricar2"
    Me.ar_ricar2.NTSRepositoryComboBox = Nothing
    Me.ar_ricar2.NTSRepositoryItemCheck = Nothing
    Me.ar_ricar2.NTSRepositoryItemMemo = Nothing
    Me.ar_ricar2.NTSRepositoryItemText = Nothing
    Me.ar_ricar2.OptionsColumn.AllowEdit = False
    Me.ar_ricar2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_ricar2.OptionsColumn.ReadOnly = True
    Me.ar_ricar2.OptionsFilter.AllowFilter = False
    Me.ar_ricar2.Visible = True
    Me.ar_ricar2.VisibleIndex = 14
    '
    'ar_misura3
    '
    Me.ar_misura3.AppearanceCell.Options.UseBackColor = True
    Me.ar_misura3.AppearanceCell.Options.UseTextOptions = True
    Me.ar_misura3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_misura3.Caption = "Disponibilità"
    Me.ar_misura3.Enabled = False
    Me.ar_misura3.FieldName = "ar_misura3"
    Me.ar_misura3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_misura3.Name = "ar_misura3"
    Me.ar_misura3.NTSRepositoryComboBox = Nothing
    Me.ar_misura3.NTSRepositoryItemCheck = Nothing
    Me.ar_misura3.NTSRepositoryItemMemo = Nothing
    Me.ar_misura3.NTSRepositoryItemText = Nothing
    Me.ar_misura3.OptionsColumn.AllowEdit = False
    Me.ar_misura3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_misura3.OptionsColumn.ReadOnly = True
    Me.ar_misura3.OptionsFilter.AllowFilter = False
    Me.ar_misura3.Visible = True
    Me.ar_misura3.VisibleIndex = 15
    '
    'ar_pesonet
    '
    Me.ar_pesonet.AppearanceCell.Options.UseBackColor = True
    Me.ar_pesonet.AppearanceCell.Options.UseTextOptions = True
    Me.ar_pesonet.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_pesonet.Caption = "Scar. rettif. col trend"
    Me.ar_pesonet.Enabled = False
    Me.ar_pesonet.FieldName = "ar_pesonet"
    Me.ar_pesonet.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_pesonet.Name = "ar_pesonet"
    Me.ar_pesonet.NTSRepositoryComboBox = Nothing
    Me.ar_pesonet.NTSRepositoryItemCheck = Nothing
    Me.ar_pesonet.NTSRepositoryItemMemo = Nothing
    Me.ar_pesonet.NTSRepositoryItemText = Nothing
    Me.ar_pesonet.OptionsColumn.AllowEdit = False
    Me.ar_pesonet.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_pesonet.OptionsColumn.ReadOnly = True
    Me.ar_pesonet.OptionsFilter.AllowFilter = False
    Me.ar_pesonet.Visible = True
    Me.ar_pesonet.VisibleIndex = 16
    '
    'ar_volume
    '
    Me.ar_volume.AppearanceCell.Options.UseBackColor = True
    Me.ar_volume.AppearanceCell.Options.UseTextOptions = True
    Me.ar_volume.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_volume.Caption = "% incr./dec. trend"
    Me.ar_volume.Enabled = False
    Me.ar_volume.FieldName = "ar_volume"
    Me.ar_volume.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_volume.Name = "ar_volume"
    Me.ar_volume.NTSRepositoryComboBox = Nothing
    Me.ar_volume.NTSRepositoryItemCheck = Nothing
    Me.ar_volume.NTSRepositoryItemMemo = Nothing
    Me.ar_volume.NTSRepositoryItemText = Nothing
    Me.ar_volume.OptionsColumn.AllowEdit = False
    Me.ar_volume.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_volume.OptionsColumn.ReadOnly = True
    Me.ar_volume.OptionsFilter.AllowFilter = False
    Me.ar_volume.Visible = True
    Me.ar_volume.VisibleIndex = 17
    '
    'pnPanel1
    '
    Me.pnPanel1.AllowDrop = True
    Me.pnPanel1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPanel1.Appearance.Options.UseBackColor = True
    Me.pnPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPanel1.Controls.Add(Me.grLiar)
    Me.pnPanel1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPanel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPanel1.Location = New System.Drawing.Point(0, 30)
    Me.pnPanel1.Name = "pnPanel1"
    Me.pnPanel1.Size = New System.Drawing.Size(724, 333)
    Me.pnPanel1.TabIndex = 5
    Me.pnPanel1.Text = "NtsPanel1"
    '
    'pnPanel2
    '
    Me.pnPanel2.AllowDrop = True
    Me.pnPanel2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPanel2.Appearance.Options.UseBackColor = True
    Me.pnPanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPanel2.Controls.Add(Me.edAammggFin)
    Me.pnPanel2.Controls.Add(Me.edAammggIni)
    Me.pnPanel2.Controls.Add(Me.lbAammggFin)
    Me.pnPanel2.Controls.Add(Me.lbAammggIni)
    Me.pnPanel2.Controls.Add(Me.pnPulsanti)
    Me.pnPanel2.Controls.Add(Me.fmTrend)
    Me.pnPanel2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPanel2.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnPanel2.Location = New System.Drawing.Point(0, 363)
    Me.pnPanel2.Name = "pnPanel2"
    Me.pnPanel2.Size = New System.Drawing.Size(724, 110)
    Me.pnPanel2.TabIndex = 6
    Me.pnPanel2.Text = "NtsPanel1"
    '
    'edAammggFin
    '
    Me.edAammggFin.BackColor = System.Drawing.Color.Transparent
    Me.edAammggFin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.edAammggFin.Location = New System.Drawing.Point(442, 9)
    Me.edAammggFin.Name = "edAammggFin"
    Me.edAammggFin.NTSDbField = ""
    Me.edAammggFin.Size = New System.Drawing.Size(82, 20)
    Me.edAammggFin.TabIndex = 34
    Me.edAammggFin.Tooltip = ""
    Me.edAammggFin.UseMnemonic = False
    '
    'edAammggIni
    '
    Me.edAammggIni.BackColor = System.Drawing.Color.Transparent
    Me.edAammggIni.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.edAammggIni.Location = New System.Drawing.Point(180, 9)
    Me.edAammggIni.Name = "edAammggIni"
    Me.edAammggIni.NTSDbField = ""
    Me.edAammggIni.Size = New System.Drawing.Size(82, 20)
    Me.edAammggIni.TabIndex = 33
    Me.edAammggIni.Tooltip = ""
    Me.edAammggIni.UseMnemonic = False
    '
    'lbAammggFin
    '
    Me.lbAammggFin.AutoSize = True
    Me.lbAammggFin.BackColor = System.Drawing.Color.Transparent
    Me.lbAammggFin.Location = New System.Drawing.Point(279, 10)
    Me.lbAammggFin.Name = "lbAammggFin"
    Me.lbAammggFin.NTSDbField = ""
    Me.lbAammggFin.Size = New System.Drawing.Size(153, 13)
    Me.lbAammggFin.TabIndex = 32
    Me.lbAammggFin.Text = "a data movimenti di magazzino"
    Me.lbAammggFin.Tooltip = ""
    Me.lbAammggFin.UseMnemonic = False
    '
    'lbAammggIni
    '
    Me.lbAammggIni.AutoSize = True
    Me.lbAammggIni.BackColor = System.Drawing.Color.Transparent
    Me.lbAammggIni.Location = New System.Drawing.Point(14, 10)
    Me.lbAammggIni.Name = "lbAammggIni"
    Me.lbAammggIni.NTSDbField = ""
    Me.lbAammggIni.Size = New System.Drawing.Size(160, 13)
    Me.lbAammggIni.TabIndex = 31
    Me.lbAammggIni.Text = "Da data movimenti di magazzino"
    Me.lbAammggIni.Tooltip = ""
    Me.lbAammggIni.UseMnemonic = False
    '
    'pnPulsanti
    '
    Me.pnPulsanti.AllowDrop = True
    Me.pnPulsanti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPulsanti.Appearance.Options.UseBackColor = True
    Me.pnPulsanti.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPulsanti.Controls.Add(Me.cmdOk)
    Me.pnPulsanti.Controls.Add(Me.cmdAnnulla)
    Me.pnPulsanti.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPulsanti.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnPulsanti.Location = New System.Drawing.Point(550, 0)
    Me.pnPulsanti.Name = "pnPulsanti"
    Me.pnPulsanti.Size = New System.Drawing.Size(174, 110)
    Me.pnPulsanti.TabIndex = 30
    Me.pnPulsanti.Text = "NtsPanel1"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(13, 29)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(153, 23)
    Me.cmdOk.TabIndex = 0
    Me.cmdOk.Text = "Procedi"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(13, 58)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(153, 23)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "Annulla"
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
    Me.fmTrend.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmTrend.Location = New System.Drawing.Point(3, 36)
    Me.fmTrend.Name = "fmTrend"
    Me.fmTrend.Size = New System.Drawing.Size(521, 71)
    Me.fmTrend.TabIndex = 29
    Me.fmTrend.Text = "L'elaborazione è stata effettuata considerando il trend delle vendite confrontand" & _
        "o i seguenti periodi:"
    '
    'edSecondoPeriodoFin
    '
    Me.edSecondoPeriodoFin.BackColor = System.Drawing.Color.Transparent
    Me.edSecondoPeriodoFin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.edSecondoPeriodoFin.Location = New System.Drawing.Point(304, 48)
    Me.edSecondoPeriodoFin.Name = "edSecondoPeriodoFin"
    Me.edSecondoPeriodoFin.NTSDbField = ""
    Me.edSecondoPeriodoFin.Size = New System.Drawing.Size(82, 20)
    Me.edSecondoPeriodoFin.TabIndex = 22
    Me.edSecondoPeriodoFin.Tooltip = ""
    Me.edSecondoPeriodoFin.UseMnemonic = False
    '
    'edSecondoPeriodoIni
    '
    Me.edSecondoPeriodoIni.BackColor = System.Drawing.Color.Transparent
    Me.edSecondoPeriodoIni.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.edSecondoPeriodoIni.Location = New System.Drawing.Point(177, 48)
    Me.edSecondoPeriodoIni.Name = "edSecondoPeriodoIni"
    Me.edSecondoPeriodoIni.NTSDbField = ""
    Me.edSecondoPeriodoIni.Size = New System.Drawing.Size(82, 20)
    Me.edSecondoPeriodoIni.TabIndex = 21
    Me.edSecondoPeriodoIni.Tooltip = ""
    Me.edSecondoPeriodoIni.UseMnemonic = False
    '
    'edPrimoPeriodoFin
    '
    Me.edPrimoPeriodoFin.BackColor = System.Drawing.Color.Transparent
    Me.edPrimoPeriodoFin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.edPrimoPeriodoFin.Location = New System.Drawing.Point(304, 25)
    Me.edPrimoPeriodoFin.Name = "edPrimoPeriodoFin"
    Me.edPrimoPeriodoFin.NTSDbField = ""
    Me.edPrimoPeriodoFin.Size = New System.Drawing.Size(82, 20)
    Me.edPrimoPeriodoFin.TabIndex = 20
    Me.edPrimoPeriodoFin.Tooltip = ""
    Me.edPrimoPeriodoFin.UseMnemonic = False
    '
    'edPrimoPeriodoIni
    '
    Me.edPrimoPeriodoIni.BackColor = System.Drawing.Color.Transparent
    Me.edPrimoPeriodoIni.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.edPrimoPeriodoIni.Location = New System.Drawing.Point(177, 25)
    Me.edPrimoPeriodoIni.Name = "edPrimoPeriodoIni"
    Me.edPrimoPeriodoIni.NTSDbField = ""
    Me.edPrimoPeriodoIni.Size = New System.Drawing.Size(82, 20)
    Me.edPrimoPeriodoIni.TabIndex = 19
    Me.edPrimoPeriodoIni.Tooltip = ""
    Me.edPrimoPeriodoIni.UseMnemonic = False
    '
    'lbSecondoPeriodoFin
    '
    Me.lbSecondoPeriodoFin.AutoSize = True
    Me.lbSecondoPeriodoFin.BackColor = System.Drawing.Color.Transparent
    Me.lbSecondoPeriodoFin.Location = New System.Drawing.Point(276, 49)
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
    Me.lbPrimoPeriodoFin.Location = New System.Drawing.Point(276, 26)
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
    Me.lbSecondoPeriodoIni.Location = New System.Drawing.Point(11, 49)
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
    Me.lbPrimoPeriodoIni.Location = New System.Drawing.Point(11, 26)
    Me.lbPrimoPeriodoIni.Name = "lbPrimoPeriodoIni"
    Me.lbPrimoPeriodoIni.NTSDbField = ""
    Me.lbPrimoPeriodoIni.Size = New System.Drawing.Size(22, 13)
    Me.lbPrimoPeriodoIni.TabIndex = 11
    Me.lbPrimoPeriodoIni.Text = "Dal"
    Me.lbPrimoPeriodoIni.Tooltip = ""
    Me.lbPrimoPeriodoIni.UseMnemonic = False
    '
    'FRMMGLIAR
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(724, 473)
    Me.Controls.Add(Me.pnPanel1)
    Me.Controls.Add(Me.pnPanel2)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.MinimizeBox = False
    Me.Name = "FRMMGLIAR"
    Me.Text = "LISTA ARTICOLI MOVIMENTATI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grLiar, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvLiar, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPanel1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPanel1.ResumeLayout(False)
    CType(Me.pnPanel2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPanel2.ResumeLayout(False)
    Me.pnPanel2.PerformLayout()
    CType(Me.pnPulsanti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPulsanti.ResumeLayout(False)
    CType(Me.fmTrend, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmTrend.ResumeLayout(False)
    Me.fmTrend.PerformLayout()
    Me.ResumeLayout(False)

  End Sub

#Region "FORM"
  Public Overridable Sub FRMMGGSLD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '----------------------------------------------------------------------------------
      InitControls()
      '----------------------------------------------------------------------------------
      GctlSetRoules()
      '----------------------------------------------------------------------------------
      If Not oCleRcsm.ApriTTARTICOX(DittaCorrente, oCleRcsm.lIITTArticox, dsLiar) Then Return
      '----------------------------------------------------------------------------------
      edAammggIni.Text = strAammggIni
      edAammggFin.Text = strAammggFin
      fmTrend.Visible = bTrend
      edPrimoPeriodoIni.Text = strPrimoPeriodoIni
      edPrimoPeriodoFin.Text = strPrimoPeriodoFin
      edSecondoPeriodoIni.Text = strSecondoPeriodoIni
      edSecondoPeriodoFin.Text = strSecondoPeriodoFin
      '----------------------------------------------------------------------------------
      dcLiar.DataSource = dsLiar.Tables("TTARTINVF")
      dsLiar.AcceptChanges()
      '----------------------------------------------------------------------------------
      grLiar.DataSource = dcLiar
      '----------------------------------------------------------------------------------
      dcLiar.MoveFirst()
      grvLiar.Focus()
      '----------------------------------------------------------------------------------
      If oCleRcsm.bAggiornaARTMAGA = False Then
        ar_magaz.Visible = False
        xx_magaz.Visible = False
        ar_fase.Visible = False
        xx_fase.Visible = False
      End If
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGGSLD_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcLiar.Dispose()
      dsLiar.Dispose()
    Catch
    End Try
  End Sub

  Public Overridable Sub FRMMGGSLD_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      '----------------------------------------------------------------------------------
      If bOk = False And bAnnullato = False Then
        e.Cancel = True
        Return
      End If
      '----------------------------------------------------------------------------------
      If bOk = True Then
        If Not Salva() Then e.Cancel = True
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
  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva()
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvLiar.NTSRipristinaRigaCorrenteBefore(dcLiar, True) Then Return
      oCleRcsm.Ripristina(dcLiar.Position, dcLiar.Filter)
      grvLiar.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Try
      If Not grvLiar.NTSDeleteRigaCorrente(dcLiar, True) Then Return
      oCleRcsm.Salva(True)
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      '----------------------------------------------------------------------------------
      Stampa(1)
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      '----------------------------------------------------------------------------------
      Stampa(0)
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Try
      '----------------------------------------------------------------------------------
      cmdAnnulla_Click(cmdAnnulla, Nothing)
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi Griglia"
  Public Overridable Sub grvLiar_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvLiar.NTSFocusedRowChanged
    Try
      If e.FocusedRowHandle < 0 Then
        grvLiar.FocusedRowHandle = e.PrevFocusedRowHandle
      End If
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "PULSANTI"
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    '---------------------------------------------------------------------------------------
    If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128577307575525510, "Annullare l'elaborazione degli articoli visualizzati?")) = Windows.Forms.DialogResult.No Then Return
    '---------------------------------------------------------------------------------------
    bAnnullato = True
    '---------------------------------------------------------------------------------------
    Me.Close()
    '---------------------------------------------------------------------------------------
  End Sub

  Public Overridable Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Dim strMsg As String = ""
    '---------------------------------------------------------------------------------------
    If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128577300465359912, "Procedere con l'elaborazione degli articoli visualizzati?")) = Windows.Forms.DialogResult.No Then Return
    '---------------------------------------------------------------------------------------
    bOk = True
    '---------------------------------------------------------------------------------------
    Me.Close()
    '---------------------------------------------------------------------------------------
  End Sub
#End Region

#Region "FUNZIONI"
  Public Overridable Function Salva() As Boolean
    Try
      '-------------------------------------------------------------------------------------
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus      
      '-------------------------------------------------------------------------------------
      Dim dRes As DialogResult
      dRes = grvLiar.NTSSalvaRigaCorrente(dcLiar, oCleRcsm.RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          '---------------------------------------------------------------------------------
          '--- Controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          '---------------------------------------------------------------------------------
          If GctlControllaOutNotEqual() = False Then Return False
          '---------------------------------------------------------------------------------
          If Not oCleRcsm.Salva(False) Then Return False
          '---------------------------------------------------------------------------------
        Case System.Windows.Forms.DialogResult.No
          '---------------------------------------------------------------------------------
          '--- Ripristino
          '---------------------------------------------------------------------------------
          oCleRcsm.Ripristina(dcLiar.Position, dcLiar.Filter)
        Case System.Windows.Forms.DialogResult.Cancel
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          '---------------------------------------------------------------------------------
          '--- La riga non ha subito modifiche
          '---------------------------------------------------------------------------------
      End Select
      Return True
    Catch ex As Exception
      '-------------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------------------------------------------
    End Try
  End Function

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nRis As Integer = 0
    Dim i As Integer
    Dim strCrpe As String = ""
    Dim strTipork As String = ""
    Dim strKey As String = ""
    Dim strNomRpt As String = ""
    Dim nPjob As Object
    Dim strParamAggiornaArtmaga As String = "0"
    Try
      '----------------------------------------------------------------------------------
      strCrpe = "{TTARTINVF.instid} = " & oCleRcsm.lIITTArticox & _
        " And {TTARTINVF.codditt} = " & ConvStrRpt(DittaCorrente)
      '----------------------------------------------------------------------------------
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGRCSM", "Reports1", " ", 0, nDestin, "BSMGRCSM.RPT", False, "Ricalcolo Scorta Minima (lista articoli)", False)
      '----------------------------------------------------------------------------------
      If nPjob Is Nothing Then Return
      '----------------------------------------------------------------------------------
      If oCleRcsm.bAggiornaARTMAGA Then
        strParamAggiornaArtmaga = "1"
      End If
      For i = LBound(CType(nPjob, Array), 2) To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "P_AGGIORNA_ARTMAGA", ConvStrRpt(strParamAggiornaArtmaga))
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    Finally
      '----------------------------------------------------------------------------------
      Me.Cursor = Cursors.Default
      '----------------------------------------------------------------------------------
    End Try
  End Sub
#End Region

End Class