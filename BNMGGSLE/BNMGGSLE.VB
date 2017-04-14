Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGGSLE
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
  Public oCleGsle As CLEMGGSLE
  Public oCallParams As CLE__CLDP
  Public dsGsle As DataSet
  Public dcGsle As BindingSource = New BindingSource()

  '--------------------------------------------------------------------------------------
  '--- Flags che indicano la gestione dell'articolo, secondo i quali vengono
  '--- visualizzate/nascoste le colonne in Stato1
  '--------------------------------------------------------------------------------------
  Public bGeslotti As Boolean = False
  Public bGescomm As Boolean = False
  Public bGesfasi As Boolean = False
  Public bGesubic As Boolean = False
  '--------------------------------------------------------------------------------------
  '--- Passati dalla modale di Nuovo/Apri
  '--------------------------------------------------------------------------------------
  Public strCodart As String = ""
  Public nMagaz As Integer = 0
  '--------------------------------------------------------------------------------------
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGGSLE", "BEMGGSLE", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128571445337061649, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleGsle = CType(oTmp, CLEMGGSLE)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGGSLE", strRemoteServer, strRemotePort)
    AddHandler oCleGsle.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleGsle.Init(oApp, oScript, oMenu.oCleComm, "LOTCPRO", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '----------------------------------------------------------------------------------
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbApri.GlyphPath = (oApp.ChildImageDir & "\open.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\recagg.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\recrestore.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\recdelete.gif")
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvGsle.NTSSetParam(oMenu, oApp.Tr(Me, 128571446331222685, "Griglia Progressivi Correnti Articolo/Lotto/Commessa/Fase/Ubicazione"))
      xx_lottox.NTSSetParamstr(oMenu, oApp.Tr(Me, 128571446536162333, "Lotto"), 50, True)
      lp_commeca.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128571446572401661, "Commessa"), CLN__STD.tabcommess)
      xx_commeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128571446598331525, "Descrizione commessa"), 40, True)
      lp_fase.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128571446619731473, "Fase"), CLN__STD.tabartfasi)
      xx_fase.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128571446721264073, "Descrizione fase"), 40, True)
      lp_ubicaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128571446750161813, "Ubicazione"), 18, False)
      lp_esist.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128571446868564445, "Esistenza"), "0.000", 13, -9999999999999D, 9999999999999D)
      lp_qtalif.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128571447055540633, "Qtà carichi"), "0.000", 13, -9999999999999D, 9999999999999D)
      lp_vqtalif.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128571447310777969, "Valore carichi"), "0.00", 13, -9999999999999D, 9999999999999D)
      lp_giaini.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128571447599973448, "Giacenza iniziale"), "0.000", 13, -9999999999999D, 9999999999999D)
      lp_vgiaini.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128571447771153703, "Valore giacenza iniziale"), "0.00", 13, -9999999999999D, 9999999999999D)
      lp_prenot.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128571448006585177, "Prenotato"), "0.000", 13, -9999999999999D, 9999999999999D)
      lp_vprenot.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128571448109918646, "Valore prenotato"), "0.00", 13, -9999999999999D, 9999999999999D)
      lp_ordin.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128571448318774190, "Ordinato"), "0.000", 13, -9999999999999D, 9999999999999D)
      lp_vordin.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128571448420075382, "Valore ordinato"), "0.00", 13, -9999999999999D, 9999999999999D)
      lp_impeg.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128571448633464467, "Impegnato"), "0.000", 13, -9999999999999D, 9999999999999D)
      lp_vimpeg.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128571448741331477, "Valore impegnato"), "0.00", 13, -9999999999999D, 9999999999999D)

      xx_lottox.NTSSetParamZoom("ZOOMLOTTI")

      Me.lbMagaz.Text = oApp.Tr(Me, 129080277488939604, "Magazzino/Descrizione")
      Me.lbCodart.Text = oApp.Tr(Me, 129080277673949076, "Articolo/Descrizione")
      '----------------------------------------------------------------------------------
      '--- Chiamo lo script per inizializzare i controlli caricati con source ext
      '----------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGGSLE))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbApri = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
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
    Me.grGsle = New NTSInformatica.NTSGrid
    Me.grvGsle = New NTSInformatica.NTSGridView
    Me.xx_lottox = New NTSInformatica.NTSGridColumn
    Me.lp_commeca = New NTSInformatica.NTSGridColumn
    Me.xx_commeca = New NTSInformatica.NTSGridColumn
    Me.lp_fase = New NTSInformatica.NTSGridColumn
    Me.xx_fase = New NTSInformatica.NTSGridColumn
    Me.lp_ubicaz = New NTSInformatica.NTSGridColumn
    Me.lp_esist = New NTSInformatica.NTSGridColumn
    Me.lp_qtalif = New NTSInformatica.NTSGridColumn
    Me.lp_vqtalif = New NTSInformatica.NTSGridColumn
    Me.lp_giaini = New NTSInformatica.NTSGridColumn
    Me.lp_vgiaini = New NTSInformatica.NTSGridColumn
    Me.lp_prenot = New NTSInformatica.NTSGridColumn
    Me.lp_vprenot = New NTSInformatica.NTSGridColumn
    Me.lp_ordin = New NTSInformatica.NTSGridColumn
    Me.lp_vordin = New NTSInformatica.NTSGridColumn
    Me.lp_impeg = New NTSInformatica.NTSGridColumn
    Me.lp_vimpeg = New NTSInformatica.NTSGridColumn
    Me.pnPanel1 = New NTSInformatica.NTSPanel
    Me.lbMagaz = New NTSInformatica.NTSLabel
    Me.lbCodart = New NTSInformatica.NTSLabel
    Me.pnPanel2 = New NTSInformatica.NTSPanel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grGsle, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvGsle, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPanel1.SuspendLayout()
    CType(Me.pnPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPanel2.SuspendLayout()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbApri, Me.tlbZoom, Me.tlbSalva, Me.tlbRipristina, Me.tlbCancella, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbUltimo, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 12
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApri), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbApri
    '
    Me.tlbApri.Caption = "Apri"
    Me.tlbApri.Glyph = CType(resources.GetObject("tlbApri.Glyph"), System.Drawing.Image)
    Me.tlbApri.Id = 1
    Me.tlbApri.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3)
    Me.tlbApri.Name = "tlbApri"
    Me.tlbApri.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 2
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.Id = 3
    Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.Id = 4
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.Id = 5
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbPrimo
    '
    Me.tlbPrimo.Caption = "Primo"
    Me.tlbPrimo.Glyph = CType(resources.GetObject("tlbPrimo.Glyph"), System.Drawing.Image)
    Me.tlbPrimo.Id = 6
    Me.tlbPrimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P))
    Me.tlbPrimo.Name = "tlbPrimo"
    Me.tlbPrimo.Visible = True
    '
    'tlbPrecedente
    '
    Me.tlbPrecedente.Caption = "Precedente"
    Me.tlbPrecedente.Glyph = CType(resources.GetObject("tlbPrecedente.Glyph"), System.Drawing.Image)
    Me.tlbPrecedente.Id = 7
    Me.tlbPrecedente.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
    Me.tlbPrecedente.Name = "tlbPrecedente"
    Me.tlbPrecedente.Visible = True
    '
    'tlbSuccessivo
    '
    Me.tlbSuccessivo.Caption = "Successivo"
    Me.tlbSuccessivo.Glyph = CType(resources.GetObject("tlbSuccessivo.Glyph"), System.Drawing.Image)
    Me.tlbSuccessivo.Id = 8
    Me.tlbSuccessivo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
    Me.tlbSuccessivo.Name = "tlbSuccessivo"
    Me.tlbSuccessivo.Visible = True
    '
    'tlbUltimo
    '
    Me.tlbUltimo.Caption = "Ultimo"
    Me.tlbUltimo.Glyph = CType(resources.GetObject("tlbUltimo.Glyph"), System.Drawing.Image)
    Me.tlbUltimo.Id = 9
    Me.tlbUltimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 10
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 11
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'grGsle
    '
    Me.grGsle.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grGsle.EmbeddedNavigator.Name = ""
    Me.grGsle.Location = New System.Drawing.Point(0, 0)
    Me.grGsle.MainView = Me.grvGsle
    Me.grGsle.Name = "grGsle"
    Me.grGsle.Size = New System.Drawing.Size(738, 208)
    Me.grGsle.TabIndex = 4
    Me.grGsle.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvGsle})
    '
    'grvGsle
    '
    Me.grvGsle.ActiveFilterEnabled = False
    Me.grvGsle.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_lottox, Me.lp_commeca, Me.xx_commeca, Me.lp_fase, Me.xx_fase, Me.lp_ubicaz, Me.lp_esist, Me.lp_qtalif, Me.lp_vqtalif, Me.lp_giaini, Me.lp_vgiaini, Me.lp_prenot, Me.lp_vprenot, Me.lp_ordin, Me.lp_vordin, Me.lp_impeg, Me.lp_vimpeg})
    Me.grvGsle.CustomizationFormBounds = New System.Drawing.Rectangle(592, 326, 208, 170)
    Me.grvGsle.Enabled = True
    Me.grvGsle.GridControl = Me.grGsle
    Me.grvGsle.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvGsle.Name = "grvGsle"
    Me.grvGsle.NTSAllowDelete = True
    Me.grvGsle.NTSAllowInsert = True
    Me.grvGsle.NTSAllowUpdate = True
    Me.grvGsle.NTSMenuContext = Nothing
    Me.grvGsle.OptionsCustomization.AllowRowSizing = True
    Me.grvGsle.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvGsle.OptionsNavigation.UseTabKey = False
    Me.grvGsle.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvGsle.OptionsView.ColumnAutoWidth = False
    Me.grvGsle.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvGsle.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvGsle.OptionsView.ShowGroupPanel = False
    Me.grvGsle.RowHeight = 14
    '
    'xx_lottox
    '
    Me.xx_lottox.AppearanceCell.Options.UseBackColor = True
    Me.xx_lottox.AppearanceCell.Options.UseTextOptions = True
    Me.xx_lottox.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_lottox.Caption = "Lotto"
    Me.xx_lottox.Enabled = True
    Me.xx_lottox.FieldName = "xx_lottox"
    Me.xx_lottox.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_lottox.Name = "xx_lottox"
    Me.xx_lottox.NTSRepositoryComboBox = Nothing
    Me.xx_lottox.NTSRepositoryItemCheck = Nothing
    Me.xx_lottox.NTSRepositoryItemText = Nothing
    Me.xx_lottox.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_lottox.OptionsFilter.AllowFilter = False
    '
    'lp_commeca
    '
    Me.lp_commeca.AppearanceCell.Options.UseBackColor = True
    Me.lp_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.lp_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lp_commeca.Caption = "Commessa"
    Me.lp_commeca.Enabled = True
    Me.lp_commeca.FieldName = "lp_commeca"
    Me.lp_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lp_commeca.Name = "lp_commeca"
    Me.lp_commeca.NTSRepositoryComboBox = Nothing
    Me.lp_commeca.NTSRepositoryItemCheck = Nothing
    Me.lp_commeca.NTSRepositoryItemText = Nothing
    Me.lp_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lp_commeca.OptionsFilter.AllowFilter = False
    Me.lp_commeca.Visible = True
    Me.lp_commeca.VisibleIndex = 0
    '
    'xx_commeca
    '
    Me.xx_commeca.AppearanceCell.Options.UseBackColor = True
    Me.xx_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.xx_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_commeca.Caption = "Descr. commessa"
    Me.xx_commeca.Enabled = False
    Me.xx_commeca.FieldName = "xx_commeca"
    Me.xx_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_commeca.Name = "xx_commeca"
    Me.xx_commeca.NTSRepositoryComboBox = Nothing
    Me.xx_commeca.NTSRepositoryItemCheck = Nothing
    Me.xx_commeca.NTSRepositoryItemText = Nothing
    Me.xx_commeca.OptionsColumn.AllowEdit = False
    Me.xx_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_commeca.OptionsColumn.ReadOnly = True
    Me.xx_commeca.OptionsFilter.AllowFilter = False
    Me.xx_commeca.Visible = True
    Me.xx_commeca.VisibleIndex = 1
    '
    'lp_fase
    '
    Me.lp_fase.AppearanceCell.Options.UseBackColor = True
    Me.lp_fase.AppearanceCell.Options.UseTextOptions = True
    Me.lp_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lp_fase.Caption = "Fase"
    Me.lp_fase.Enabled = True
    Me.lp_fase.FieldName = "lp_fase"
    Me.lp_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lp_fase.Name = "lp_fase"
    Me.lp_fase.NTSRepositoryComboBox = Nothing
    Me.lp_fase.NTSRepositoryItemCheck = Nothing
    Me.lp_fase.NTSRepositoryItemText = Nothing
    Me.lp_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lp_fase.OptionsFilter.AllowFilter = False
    Me.lp_fase.Visible = True
    Me.lp_fase.VisibleIndex = 2
    '
    'xx_fase
    '
    Me.xx_fase.AppearanceCell.Options.UseBackColor = True
    Me.xx_fase.AppearanceCell.Options.UseTextOptions = True
    Me.xx_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_fase.Caption = "Descr. fase"
    Me.xx_fase.Enabled = False
    Me.xx_fase.FieldName = "xx_fase"
    Me.xx_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_fase.Name = "xx_fase"
    Me.xx_fase.NTSRepositoryComboBox = Nothing
    Me.xx_fase.NTSRepositoryItemCheck = Nothing
    Me.xx_fase.NTSRepositoryItemText = Nothing
    Me.xx_fase.OptionsColumn.AllowEdit = False
    Me.xx_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_fase.OptionsColumn.ReadOnly = True
    Me.xx_fase.OptionsFilter.AllowFilter = False
    Me.xx_fase.Visible = True
    Me.xx_fase.VisibleIndex = 3
    '
    'lp_ubicaz
    '
    Me.lp_ubicaz.AppearanceCell.Options.UseBackColor = True
    Me.lp_ubicaz.AppearanceCell.Options.UseTextOptions = True
    Me.lp_ubicaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lp_ubicaz.Caption = "Ubicazione"
    Me.lp_ubicaz.Enabled = True
    Me.lp_ubicaz.FieldName = "lp_ubicaz"
    Me.lp_ubicaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lp_ubicaz.Name = "lp_ubicaz"
    Me.lp_ubicaz.NTSRepositoryComboBox = Nothing
    Me.lp_ubicaz.NTSRepositoryItemCheck = Nothing
    Me.lp_ubicaz.NTSRepositoryItemText = Nothing
    Me.lp_ubicaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lp_ubicaz.OptionsFilter.AllowFilter = False
    Me.lp_ubicaz.Visible = True
    Me.lp_ubicaz.VisibleIndex = 4
    '
    'lp_esist
    '
    Me.lp_esist.AppearanceCell.Options.UseBackColor = True
    Me.lp_esist.AppearanceCell.Options.UseTextOptions = True
    Me.lp_esist.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lp_esist.Caption = "Esistenza"
    Me.lp_esist.Enabled = True
    Me.lp_esist.FieldName = "lp_esist"
    Me.lp_esist.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lp_esist.Name = "lp_esist"
    Me.lp_esist.NTSRepositoryComboBox = Nothing
    Me.lp_esist.NTSRepositoryItemCheck = Nothing
    Me.lp_esist.NTSRepositoryItemText = Nothing
    Me.lp_esist.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lp_esist.OptionsFilter.AllowFilter = False
    Me.lp_esist.Visible = True
    Me.lp_esist.VisibleIndex = 5
    '
    'lp_qtalif
    '
    Me.lp_qtalif.AppearanceCell.Options.UseBackColor = True
    Me.lp_qtalif.AppearanceCell.Options.UseTextOptions = True
    Me.lp_qtalif.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lp_qtalif.Caption = "Qtà carichi"
    Me.lp_qtalif.Enabled = True
    Me.lp_qtalif.FieldName = "lp_qtalif"
    Me.lp_qtalif.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lp_qtalif.Name = "lp_qtalif"
    Me.lp_qtalif.NTSRepositoryComboBox = Nothing
    Me.lp_qtalif.NTSRepositoryItemCheck = Nothing
    Me.lp_qtalif.NTSRepositoryItemText = Nothing
    Me.lp_qtalif.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lp_qtalif.OptionsFilter.AllowFilter = False
    Me.lp_qtalif.Visible = True
    Me.lp_qtalif.VisibleIndex = 6
    '
    'lp_vqtalif
    '
    Me.lp_vqtalif.AppearanceCell.Options.UseBackColor = True
    Me.lp_vqtalif.AppearanceCell.Options.UseTextOptions = True
    Me.lp_vqtalif.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lp_vqtalif.Caption = "Valore carichi"
    Me.lp_vqtalif.Enabled = True
    Me.lp_vqtalif.FieldName = "lp_vqtalif"
    Me.lp_vqtalif.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lp_vqtalif.Name = "lp_vqtalif"
    Me.lp_vqtalif.NTSRepositoryComboBox = Nothing
    Me.lp_vqtalif.NTSRepositoryItemCheck = Nothing
    Me.lp_vqtalif.NTSRepositoryItemText = Nothing
    Me.lp_vqtalif.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lp_vqtalif.OptionsFilter.AllowFilter = False
    Me.lp_vqtalif.Visible = True
    Me.lp_vqtalif.VisibleIndex = 7
    '
    'lp_giaini
    '
    Me.lp_giaini.AppearanceCell.Options.UseBackColor = True
    Me.lp_giaini.AppearanceCell.Options.UseTextOptions = True
    Me.lp_giaini.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lp_giaini.Caption = "Giacenza iniziale"
    Me.lp_giaini.Enabled = True
    Me.lp_giaini.FieldName = "lp_giaini"
    Me.lp_giaini.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lp_giaini.Name = "lp_giaini"
    Me.lp_giaini.NTSRepositoryComboBox = Nothing
    Me.lp_giaini.NTSRepositoryItemCheck = Nothing
    Me.lp_giaini.NTSRepositoryItemText = Nothing
    Me.lp_giaini.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lp_giaini.OptionsFilter.AllowFilter = False
    Me.lp_giaini.Visible = True
    Me.lp_giaini.VisibleIndex = 8
    '
    'lp_vgiaini
    '
    Me.lp_vgiaini.AppearanceCell.Options.UseBackColor = True
    Me.lp_vgiaini.AppearanceCell.Options.UseTextOptions = True
    Me.lp_vgiaini.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lp_vgiaini.Caption = "Valore giacenza iniziale"
    Me.lp_vgiaini.Enabled = True
    Me.lp_vgiaini.FieldName = "lp_vgiaini"
    Me.lp_vgiaini.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lp_vgiaini.Name = "lp_vgiaini"
    Me.lp_vgiaini.NTSRepositoryComboBox = Nothing
    Me.lp_vgiaini.NTSRepositoryItemCheck = Nothing
    Me.lp_vgiaini.NTSRepositoryItemText = Nothing
    Me.lp_vgiaini.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lp_vgiaini.OptionsFilter.AllowFilter = False
    Me.lp_vgiaini.Visible = True
    Me.lp_vgiaini.VisibleIndex = 9
    '
    'lp_prenot
    '
    Me.lp_prenot.AppearanceCell.Options.UseBackColor = True
    Me.lp_prenot.AppearanceCell.Options.UseTextOptions = True
    Me.lp_prenot.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lp_prenot.Caption = "Prenotato"
    Me.lp_prenot.Enabled = True
    Me.lp_prenot.FieldName = "lp_prenot"
    Me.lp_prenot.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lp_prenot.Name = "lp_prenot"
    Me.lp_prenot.NTSRepositoryComboBox = Nothing
    Me.lp_prenot.NTSRepositoryItemCheck = Nothing
    Me.lp_prenot.NTSRepositoryItemText = Nothing
    Me.lp_prenot.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lp_prenot.OptionsFilter.AllowFilter = False
    Me.lp_prenot.Visible = True
    Me.lp_prenot.VisibleIndex = 10
    '
    'lp_vprenot
    '
    Me.lp_vprenot.AppearanceCell.Options.UseBackColor = True
    Me.lp_vprenot.AppearanceCell.Options.UseTextOptions = True
    Me.lp_vprenot.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lp_vprenot.Caption = "Valore prenotato"
    Me.lp_vprenot.Enabled = True
    Me.lp_vprenot.FieldName = "lp_vprenot"
    Me.lp_vprenot.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lp_vprenot.Name = "lp_vprenot"
    Me.lp_vprenot.NTSRepositoryComboBox = Nothing
    Me.lp_vprenot.NTSRepositoryItemCheck = Nothing
    Me.lp_vprenot.NTSRepositoryItemText = Nothing
    Me.lp_vprenot.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lp_vprenot.OptionsFilter.AllowFilter = False
    Me.lp_vprenot.Visible = True
    Me.lp_vprenot.VisibleIndex = 11
    '
    'lp_ordin
    '
    Me.lp_ordin.AppearanceCell.Options.UseBackColor = True
    Me.lp_ordin.AppearanceCell.Options.UseTextOptions = True
    Me.lp_ordin.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lp_ordin.Caption = "Ordinato"
    Me.lp_ordin.Enabled = True
    Me.lp_ordin.FieldName = "lp_ordin"
    Me.lp_ordin.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lp_ordin.Name = "lp_ordin"
    Me.lp_ordin.NTSRepositoryComboBox = Nothing
    Me.lp_ordin.NTSRepositoryItemCheck = Nothing
    Me.lp_ordin.NTSRepositoryItemText = Nothing
    Me.lp_ordin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lp_ordin.OptionsFilter.AllowFilter = False
    Me.lp_ordin.Visible = True
    Me.lp_ordin.VisibleIndex = 12
    '
    'lp_vordin
    '
    Me.lp_vordin.AppearanceCell.Options.UseBackColor = True
    Me.lp_vordin.AppearanceCell.Options.UseTextOptions = True
    Me.lp_vordin.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lp_vordin.Caption = "Valore ordinato"
    Me.lp_vordin.Enabled = True
    Me.lp_vordin.FieldName = "lp_vordin"
    Me.lp_vordin.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lp_vordin.Name = "lp_vordin"
    Me.lp_vordin.NTSRepositoryComboBox = Nothing
    Me.lp_vordin.NTSRepositoryItemCheck = Nothing
    Me.lp_vordin.NTSRepositoryItemText = Nothing
    Me.lp_vordin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lp_vordin.OptionsFilter.AllowFilter = False
    Me.lp_vordin.Visible = True
    Me.lp_vordin.VisibleIndex = 13
    '
    'lp_impeg
    '
    Me.lp_impeg.AppearanceCell.Options.UseBackColor = True
    Me.lp_impeg.AppearanceCell.Options.UseTextOptions = True
    Me.lp_impeg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lp_impeg.Caption = "Impegnato"
    Me.lp_impeg.Enabled = True
    Me.lp_impeg.FieldName = "lp_impeg"
    Me.lp_impeg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lp_impeg.Name = "lp_impeg"
    Me.lp_impeg.NTSRepositoryComboBox = Nothing
    Me.lp_impeg.NTSRepositoryItemCheck = Nothing
    Me.lp_impeg.NTSRepositoryItemText = Nothing
    Me.lp_impeg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lp_impeg.OptionsFilter.AllowFilter = False
    Me.lp_impeg.Visible = True
    Me.lp_impeg.VisibleIndex = 14
    '
    'lp_vimpeg
    '
    Me.lp_vimpeg.AppearanceCell.Options.UseBackColor = True
    Me.lp_vimpeg.AppearanceCell.Options.UseTextOptions = True
    Me.lp_vimpeg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lp_vimpeg.Caption = "Valore impegnato"
    Me.lp_vimpeg.Enabled = True
    Me.lp_vimpeg.FieldName = "lp_vimpeg"
    Me.lp_vimpeg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lp_vimpeg.Name = "lp_vimpeg"
    Me.lp_vimpeg.NTSRepositoryComboBox = Nothing
    Me.lp_vimpeg.NTSRepositoryItemCheck = Nothing
    Me.lp_vimpeg.NTSRepositoryItemText = Nothing
    Me.lp_vimpeg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lp_vimpeg.OptionsFilter.AllowFilter = False
    Me.lp_vimpeg.Visible = True
    Me.lp_vimpeg.VisibleIndex = 15
    '
    'pnPanel1
    '
    Me.pnPanel1.AllowDrop = True
    Me.pnPanel1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPanel1.Appearance.ForeColor = System.Drawing.Color.Black
    Me.pnPanel1.Appearance.Options.UseBackColor = True
    Me.pnPanel1.Appearance.Options.UseForeColor = True
    Me.pnPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPanel1.Controls.Add(Me.lbMagaz)
    Me.pnPanel1.Controls.Add(Me.lbCodart)
    Me.pnPanel1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPanel1.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnPanel1.Location = New System.Drawing.Point(0, 30)
    Me.pnPanel1.Name = "pnPanel1"
    Me.pnPanel1.Size = New System.Drawing.Size(738, 61)
    Me.pnPanel1.TabIndex = 5
    '
    'lbMagaz
    '
    Me.lbMagaz.AutoSize = True
    Me.lbMagaz.BackColor = System.Drawing.Color.Transparent
    Me.lbMagaz.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbMagaz.ForeColor = System.Drawing.Color.Black
    Me.lbMagaz.Location = New System.Drawing.Point(12, 36)
    Me.lbMagaz.Name = "lbMagaz"
    Me.lbMagaz.NTSDbField = ""
    Me.lbMagaz.Size = New System.Drawing.Size(138, 13)
    Me.lbMagaz.TabIndex = 1
    '
    'lbCodart
    '
    Me.lbCodart.AutoSize = True
    Me.lbCodart.BackColor = System.Drawing.Color.Transparent
    Me.lbCodart.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbCodart.ForeColor = System.Drawing.Color.Black
    Me.lbCodart.Location = New System.Drawing.Point(12, 12)
    Me.lbCodart.Name = "lbCodart"
    Me.lbCodart.NTSDbField = ""
    Me.lbCodart.Size = New System.Drawing.Size(122, 13)
    Me.lbCodart.TabIndex = 0
    '
    'pnPanel2
    '
    Me.pnPanel2.AllowDrop = True
    Me.pnPanel2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPanel2.Appearance.Options.UseBackColor = True
    Me.pnPanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPanel2.Controls.Add(Me.grGsle)
    Me.pnPanel2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPanel2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPanel2.Location = New System.Drawing.Point(0, 91)
    Me.pnPanel2.Name = "pnPanel2"
    Me.pnPanel2.Size = New System.Drawing.Size(738, 208)
    Me.pnPanel2.TabIndex = 6
    '
    'FRMMGGSLE
    '
    Me.ClientSize = New System.Drawing.Size(738, 299)
    Me.Controls.Add(Me.pnPanel2)
    Me.Controls.Add(Me.pnPanel1)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMMGGSLE"
    Me.Text = "Gestione Progressivi Correnti Articolo/Lotto/Commessa/Fase/Ubicazione"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grGsle, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvGsle, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPanel1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPanel1.ResumeLayout(False)
    Me.pnPanel1.PerformLayout()
    CType(Me.pnPanel2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPanel2.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

#Region "FORM"
  Public Overridable Sub FRMMGGSLE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      SetStato0()

      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGGSLE_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcGsle.Dispose()
      dsGsle.Dispose()
    Catch
    End Try
  End Sub

  Public Overridable Sub FRMMGGSLE_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "TOOLBAR"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Dim fmSsle As FRMMGSSLE = Nothing
    Try
      If pnPanel1.Visible = True Then
        If Not Salva() Then Return
        ChiudiDs()
        SetStato0()
      End If

      fmSsle = CType(NTSNewFormModal("FRMMGSSLE"), FRMMGSSLE)
      fmSsle.Init(oMenu, Nothing, DittaCorrente)
      fmSsle.Text = "Nuovo Progressivo Corrente Articolo/Lotto/Commessa/Fase/Ubicazione"
      fmSsle.InitEntity(oCleGsle)
      fmSsle.ShowDialog(Me)
      If fmSsle.bAnnullato = True Then Return

      strCodart = NTSCStr(fmSsle.edLp_codart.Text)
      nMagaz = NTSCInt(fmSsle.edLp_magaz.Text)

      If Not oCleGsle.Nuovo(DittaCorrente, strCodart, nMagaz, dsGsle) Then Return

      dcGsle.DataSource = dsGsle.Tables("LOTCPRO")
      dsGsle.AcceptChanges()

      grGsle.DataSource = dcGsle

      SetStato1()

      SettaDescrizioni(fmSsle.edLp_codart.Text, fmSsle.lbXx_codart.Text.Trim, _
        NTSCInt(fmSsle.edLp_magaz.Text), fmSsle.lbXx_magaz.Text.Trim)

      grvGsle_NTSFocusedRowChanged(grGsle, Nothing)

      grvGsle.Focus()
      grvGsle.NTSNuovo()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not fmSsle Is Nothing Then fmSsle.Dispose()
      fmSsle = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbApri_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApri.ItemClick
    Dim fmSsle As FRMMGSSLE = Nothing
    Try
      If grGsle.Visible = True Then
        If Not Salva() Then Return
        ChiudiDs()
        SetStato0()
      End If

      fmSsle = CType(NTSNewFormModal("FRMMGSSLE"), FRMMGSSLE)
      fmSsle.Init(oMenu, Nothing, DittaCorrente)
      fmSsle.Text = "Apri Progressivo Corrente Articolo/Lotto/Commessa/Fase/Ubicazione"
      fmSsle.InitEntity(oCleGsle)
      fmSsle.ShowDialog(Me)
      If fmSsle.bAnnullato = True Then Return

      strCodart = NTSCStr(fmSsle.edLp_codart.Text)
      nMagaz = NTSCInt(fmSsle.edLp_magaz.Text)

      If Not oCleGsle.Apri(DittaCorrente, strCodart, nMagaz, dsGsle) Then Return

      dcGsle.DataSource = dsGsle.Tables("LOTCPRO")
      dsGsle.AcceptChanges()

      grGsle.DataSource = dcGsle

      SetStato1()

      SettaDescrizioni(fmSsle.edLp_codart.Text, fmSsle.lbXx_codart.Text.Trim, _
        NTSCInt(fmSsle.edLp_magaz.Text), fmSsle.lbXx_magaz.Text.Trim)

      grvGsle_NTSFocusedRowChanged(grGsle, Nothing)

      dcGsle.MoveFirst()
      grvGsle.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not fmSsle Is Nothing Then fmSsle.Dispose()
      fmSsle = Nothing
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

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      '-------------------------------------------------------------------------------------
      '--- Per eventuali altri controlli caricati al volo
      '-------------------------------------------------------------------------------------
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      '-------------------------------------------------------------------------------------
      SetFastZoom(NTSCStr(CType(CType(ctrlTmp, NTSGrid).DefaultView, NTSGridView).EditingValue), oParam)    'abilito la gestione dello zoom veloce
      '-------------------------------------------------------------------------------------
      If grvGsle.FocusedColumn.Equals(lp_fase) Then
        NTSZOOM.strIn = NTSCStr(grvGsle.GetFocusedValue)
        oParam.strTipo = strCodart
        NTSZOOM.ZoomStrIn("ZOOMARTFASI", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvGsle.GetFocusedValue) Then grvGsle.SetFocusedValue(NTSZOOM.strIn)
      ElseIf grvGsle.FocusedColumn.Equals(xx_lottox) Then
        NTSZOOM.strIn = NTSCStr(grvGsle.GetFocusedValue)
        oParam.strTipo = strCodart
        NTSZOOM.ZoomStrIn("ZOOMANALOTTI", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvGsle.GetFocusedValue) Then grvGsle.SetFocusedValue(NTSZOOM.strIn)
      Else
        NTSCallStandardZoom()
      End If
    Catch ex As Exception
      '-------------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvGsle.NTSRipristinaRigaCorrenteBefore(dcGsle, True) Then Return
      oCleGsle.Ripristina(dcGsle.Position, dcGsle.Filter)
      grvGsle.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Try
      If Not grvGsle.NTSDeleteRigaCorrente(dcGsle, True) Then Return
      oCleGsle.Salva(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    Try
      '-------------------------------------------------
      'vado sul primo record
      If Not Salva() Then Return
      dcGsle.MoveFirst()
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
      dcGsle.MovePrevious()
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
      dcGsle.MoveNext()
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
      dcGsle.MoveLast()
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
      If Not Salva() Then Return
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "GRIGLIA"
  Public Overridable Sub grvGsle_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvGsle.NTSBeforeRowUpdate
    Try
      If Not Salva() Then
        'rimango sulla riga su cui sono
        e.Allow = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvGsle_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvGsle.NTSFocusedRowChanged
    Try
      '----------------------------------------------------------------------------------
      If bGeslotti = True Then
        If NTSCStr(grvGsle.GetFocusedRowCellValue(xx_lottox)) <> "" Then
          xx_lottox.Enabled = False
        Else
          GctlSetVisEnab(xx_lottox, False)
        End If
      End If
      '----------------------------------------------------------------------------------
      If bGescomm = True Then
        Dim nCommeca As Integer = NTSCInt(grvGsle.GetFocusedRowCellValue(lp_commeca))
        If nCommeca <> 0 Then
          lp_commeca.Enabled = False
        Else
          GctlSetVisEnab(lp_commeca, False)
        End If
      End If
      '----------------------------------------------------------------------------------
      If bGesfasi = True Then
        Dim nFase As Integer = NTSCInt(grvGsle.GetFocusedRowCellValue(lp_fase))
        If nFase <> 0 Then
          lp_fase.Enabled = False
        Else
          GctlSetVisEnab(lp_fase, False)
        End If
      End If
      '----------------------------------------------------------------------------------
      If bGesubic = True Then
        Dim strUbicaz As String = NTSCStr(grvGsle.GetFocusedRowCellValue(lp_ubicaz))
        If strUbicaz.Trim <> "" Then
          lp_ubicaz.Enabled = False
        Else
          GctlSetVisEnab(lp_ubicaz, False)
        End If
      End If
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub xxo_lottox_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles xx_lottox.NTSZoomGest
    Dim oCZoo As New CLE__CZOO
    Dim bNuovo As Boolean = True
    Dim oTmp As New Control
    Dim oPar As New CLE__CLDP
    Try
      Me.ValidaLastControl()

      If e.TipoEvento = "OPEN" Then
        If grvGsle.NTSGetCurrentDataRow Is Nothing Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129519320958560691, "Posizionarsi prima su una riga di griglia con lotto impostato"))
          e.ZoomHandled = True
          Return
        End If
        bNuovo = False
        oPar.strParam = "APRI;" & oCleGsle.strCodar.PadRight(CLN__STD.CodartMaxLen) & ";" & NTSCStr(grvGsle.NTSGetCurrentDataRow!lp_lotto).PadLeft(9, "0"c) & ";"
      Else
        oPar.strParam = "NUOV;" & oCleGsle.strCodar.PadRight(CLN__STD.CodartMaxLen) & ";" & "000000000" & ";"
      End If

      oTmp.Text = ""
      NTSZOOM.OpenChildGest(oTmp, "ZOOMANALOTTI", DittaCorrente, bNuovo, oPar)

      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo
      grGsle.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub ChiudiDs()
    Try
      If grGsle.Visible = True Then
        dcGsle.Clear()
        dsGsle.Clear()
      End If
    Catch
    End Try
  End Sub

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus      

      Dim dRes As DialogResult
      dRes = grvGsle.NTSSalvaRigaCorrente(dcGsle, oCleGsle.RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False
          '------------------------------------------------------------------------------
          '--- Test specifico per questo programma!!!
          '------------------------------------------------------------------------------
          If Not oCleGsle.TestPreSalva1(bGeslotti, bGescomm, bGesfasi, bGesubic) Then
            Return False
          End If
          '------------------------------------------------------------------------------
          If Not oCleGsle.Salva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleGsle.Ripristina(dcGsle.Position, dcGsle.Filter)
        Case System.Windows.Forms.DialogResult.Cancel
          'non posso fare nulla
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          'la riga non ha subito modifiche
      End Select
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub SetStato0()
    pnPanel1.Visible = False
    pnPanel2.Visible = False
    tlbZoom.Enabled = False
    tlbSalva.Enabled = False
    tlbRipristina.Enabled = False
    tlbCancella.Enabled = False
    tlbPrimo.Enabled = False
    tlbPrecedente.Enabled = False
    tlbSuccessivo.Enabled = False
    tlbUltimo.Enabled = False
    GctlSetVisEnab(xx_lottox, True)
    GctlSetVisEnab(lp_commeca, True)
    GctlSetVisEnab(xx_commeca, True)
    GctlSetVisEnab(lp_fase, True)
    GctlSetVisEnab(xx_fase, True)
    GctlSetVisEnab(lp_ubicaz, True)
  End Sub

  Public Overridable Sub SetStato1()
    GctlSetVisEnab(pnPanel1, True)
    GctlSetVisEnab(pnPanel2, True)
    GctlSetVisEnab(tlbZoom, False)
    GctlSetVisEnab(tlbSalva, False)
    GctlSetVisEnab(tlbRipristina, False)
    GctlSetVisEnab(tlbCancella, False)
    GctlSetVisEnab(tlbPrimo, False)
    GctlSetVisEnab(tlbPrecedente, False)
    GctlSetVisEnab(tlbSuccessivo, False)
    GctlSetVisEnab(tlbUltimo, False)
    '----------------------------------------------------------------------------------
    '--- A seconda delle varie gestioni dell'articolo, visualizza o meno le colonne
    '----------------------------------------------------------------------------------
    oCleGsle.SettaFlags(DittaCorrente, strCodart, bGeslotti, bGescomm, bGesfasi, bGesubic)
    If bGeslotti = False Then xx_lottox.Visible = False
    If bGescomm = False Then
      lp_commeca.Visible = False
      xx_commeca.Visible = False
    End If
    If bGesfasi = False Then
      lp_fase.Visible = False
      xx_fase.Visible = False
    End If
    If bGesubic = False Then lp_ubicaz.Visible = False
    '----------------------------------------------------------------------------------
    grGsle.Focus()
  End Sub

  Public Overridable Sub SettaDescrizioni(ByVal strCodart As String, ByVal strDesart As String, _
    ByVal nMagaz As Integer, ByVal strDesmagaz As String)
    Try
      lbCodart.Text = oApp.Tr(Me, 129080276611238418, "Articolo: '|" & strCodart & "|'")
      If strDesart <> "" Then lbCodart.Text = lbCodart.Text & " - " & strDesart
      If nMagaz <> 0 Then
        lbMagaz.Text = oApp.Tr(Me, 129080276989226520, "Magazzino: '|" & nMagaz.ToString & "|'")
        If strDesmagaz <> "" Then lbMagaz.Text = lbMagaz.Text & " - " & strDesmagaz
      End If
    Catch ex As Exception

    End Try
  End Sub
End Class