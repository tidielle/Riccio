Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGGSLD
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
  Public oCleGsld As CLEMGGSLD
  Public oCallParams As CLE__CLDP
  Public dsGsld As DataSet
  Public dcGsld As BindingSource = New BindingSource()

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
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbApri As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents grGsld As NTSInformatica.NTSGrid
  Public WithEvents grvGsld As NTSInformatica.NTSGridView
  Public WithEvents xx_lottox As NTSInformatica.NTSGridColumn
  Public WithEvents ld_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents xx_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents ld_fase As NTSInformatica.NTSGridColumn
  Public WithEvents xx_fase As NTSInformatica.NTSGridColumn
  Public WithEvents ld_ubicaz As NTSInformatica.NTSGridColumn
  Public WithEvents ld_quant As NTSInformatica.NTSGridColumn
  Public WithEvents ld_valore As NTSInformatica.NTSGridColumn
  Public WithEvents ld_qtacar As NTSInformatica.NTSGridColumn
  Public WithEvents ld_vqtacar As NTSInformatica.NTSGridColumn
  Public WithEvents ld_giaini As NTSInformatica.NTSGridColumn
  Public WithEvents ld_vgiaini As NTSInformatica.NTSGridColumn
  Public WithEvents pnPanel1 As NTSInformatica.NTSPanel
  Public WithEvents pnPanel2 As NTSInformatica.NTSPanel
  Public WithEvents lbMagaz As NTSInformatica.NTSLabel
  Public WithEvents lbCodart As NTSInformatica.NTSLabel
  Public WithEvents ld_tipval As NTSInformatica.NTSGridColumn
  Public WithEvents xx_tipval As NTSInformatica.NTSGridColumn

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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGGSLD", "BEMGGSLD", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128565280057686734, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleGsld = CType(oTmp, CLEMGGSLD)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGGSLD", strRemoteServer, strRemotePort)
    AddHandler oCleGsld.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleGsld.Init(oApp, oScript, oMenu.oCleComm, "LOTCDEF", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '--------------------------------------------------------------------------------------------------------------
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
      '--------------------------------------------------------------------------------------------------------------
      tlbMain.NTSSetToolTip()
      '--------------------------------------------------------------------------------------------------------------
      grvGsld.NTSSetParam(oMenu, oApp.Tr(Me, 128565406460393907, "Griglia Progressivi Definitivi Articolo/Lotto/Commessa/Fase/Ubicazione"))
      xx_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128565406488518727, "Lotto"), 50, False)
      ld_commeca.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128566138613060674, "Commessa"), CLN__STD.tabcommess)
      xx_commeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128565406963984434, "Descrizione commessa"), 40, True)
      ld_fase.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128566139305859872, "Fase"), CLN__STD.tabartfasi)
      xx_fase.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128565407767573041, "Descrizione fase"), 40, True)
      ld_ubicaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128565408709754511, "Ubicazione"), 18, False)
      ld_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128565409384912250, "Quantità"), "0.000", 13, -9999999999999D, 9999999999999D)
      ld_valore.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128565409815696257, "Valore"), "0.00", 13, -9999999999999D, 9999999999999D)
      ld_qtacar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128566047517157974, "Quantità carichi"), "0.000", 13, -9999999999999D, 9999999999999D)
      ld_vqtacar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128566047755751724, "Valore carichi"), "0.00", 13, -9999999999999D, 9999999999999D)
      ld_giaini.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128565410244917754, "Giacenza iniziale"), "0.000", 13, -9999999999999D, 9999999999999D)
      ld_vgiaini.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128565410419606372, "Valore giacenza iniziale"), "0.00", 13, -9999999999999D, 9999999999999D)
      ld_tipval.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128980986771154465, "Valorizzazione"), CLN__STD.tablist, False)
      xx_tipval.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128980986798810715, "Descrizione valorizzazione"), 50, True)
      '--------------------------------------------------------------------------------------------------------------
      ld_tipval.NTSSetRichiesto()
      '--------------------------------------------------------------------------------------------------------------
      xx_lottox.NTSSetParamZoom("ZOOMLOTTI")
      '--------------------------------------------------------------------------------------------------------------
      lbMagaz.Text = oApp.Tr(Me, 129080279634518202, "Magazzino/Descrizione")
      lbCodart.Text = oApp.Tr(Me, 129080280204078612, "Articolo/Descrizione")

      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGGSLD))
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
    Me.grGsld = New NTSInformatica.NTSGrid
    Me.grvGsld = New NTSInformatica.NTSGridView
    Me.ld_tipval = New NTSInformatica.NTSGridColumn
    Me.xx_tipval = New NTSInformatica.NTSGridColumn
    Me.xx_lottox = New NTSInformatica.NTSGridColumn
    Me.ld_commeca = New NTSInformatica.NTSGridColumn
    Me.xx_commeca = New NTSInformatica.NTSGridColumn
    Me.ld_fase = New NTSInformatica.NTSGridColumn
    Me.xx_fase = New NTSInformatica.NTSGridColumn
    Me.ld_ubicaz = New NTSInformatica.NTSGridColumn
    Me.ld_quant = New NTSInformatica.NTSGridColumn
    Me.ld_valore = New NTSInformatica.NTSGridColumn
    Me.ld_qtacar = New NTSInformatica.NTSGridColumn
    Me.ld_vqtacar = New NTSInformatica.NTSGridColumn
    Me.ld_giaini = New NTSInformatica.NTSGridColumn
    Me.ld_vgiaini = New NTSInformatica.NTSGridColumn
    Me.pnPanel1 = New NTSInformatica.NTSPanel
    Me.lbMagaz = New NTSInformatica.NTSLabel
    Me.lbCodart = New NTSInformatica.NTSLabel
    Me.pnPanel2 = New NTSInformatica.NTSPanel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grGsld, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvGsld, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'grGsld
    '
    Me.grGsld.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grGsld.EmbeddedNavigator.Name = ""
    Me.grGsld.Location = New System.Drawing.Point(0, 0)
    Me.grGsld.MainView = Me.grvGsld
    Me.grGsld.Name = "grGsld"
    Me.grGsld.Size = New System.Drawing.Size(738, 208)
    Me.grGsld.TabIndex = 4
    Me.grGsld.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvGsld})
    '
    'grvGsld
    '
    Me.grvGsld.ActiveFilterEnabled = False
    '
    'ld_tipval
    '
    Me.ld_tipval.AppearanceCell.Options.UseBackColor = True
    Me.ld_tipval.AppearanceCell.Options.UseTextOptions = True
    Me.ld_tipval.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ld_tipval.Caption = "Valorizzazione"
    Me.ld_tipval.Enabled = True
    Me.ld_tipval.FieldName = "ld_tipval"
    Me.ld_tipval.Name = "ld_tipval"
    Me.ld_tipval.NTSRepositoryComboBox = Nothing
    Me.ld_tipval.NTSRepositoryItemCheck = Nothing
    Me.ld_tipval.NTSRepositoryItemMemo = Nothing
    Me.ld_tipval.NTSRepositoryItemText = Nothing
    Me.ld_tipval.Visible = True
    Me.ld_tipval.VisibleIndex = 11
    '
    'xx_tipval
    '
    Me.xx_tipval.AppearanceCell.Options.UseBackColor = True
    Me.xx_tipval.AppearanceCell.Options.UseTextOptions = True
    Me.xx_tipval.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_tipval.Caption = "Descrizione"
    Me.xx_tipval.Enabled = True
    Me.xx_tipval.FieldName = "xx_tipval"
    Me.xx_tipval.Name = "xx_tipval"
    Me.xx_tipval.NTSRepositoryComboBox = Nothing
    Me.xx_tipval.NTSRepositoryItemCheck = Nothing
    Me.xx_tipval.NTSRepositoryItemMemo = Nothing
    Me.xx_tipval.NTSRepositoryItemText = Nothing
    Me.xx_tipval.Visible = True
    Me.xx_tipval.VisibleIndex = 12
    Me.grvGsld.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_lottox, Me.ld_commeca, Me.xx_commeca, Me.ld_fase, Me.xx_fase, Me.ld_ubicaz, Me.ld_quant, Me.ld_valore, Me.ld_qtacar, Me.ld_vqtacar, Me.ld_giaini, Me.ld_vgiaini, Me.ld_tipval, Me.xx_tipval})
    Me.grvGsld.CustomizationFormBounds = New System.Drawing.Rectangle(592, 326, 208, 170)
    Me.grvGsld.Enabled = True
    Me.grvGsld.GridControl = Me.grGsld
    Me.grvGsld.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvGsld.Name = "grvGsld"
    Me.grvGsld.NTSAllowDelete = True
    Me.grvGsld.NTSAllowInsert = True
    Me.grvGsld.NTSAllowUpdate = True
    Me.grvGsld.NTSMenuContext = Nothing
    Me.grvGsld.OptionsCustomization.AllowRowSizing = True
    Me.grvGsld.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvGsld.OptionsNavigation.UseTabKey = False
    Me.grvGsld.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvGsld.OptionsView.ColumnAutoWidth = False
    Me.grvGsld.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvGsld.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvGsld.OptionsView.ShowGroupPanel = False
    Me.grvGsld.RowHeight = 14
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
    Me.xx_lottox.NTSRepositoryItemMemo = Nothing
    Me.xx_lottox.NTSRepositoryItemText = Nothing
    Me.xx_lottox.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_lottox.OptionsFilter.AllowFilter = False
    '
    'ld_commeca
    '
    Me.ld_commeca.AppearanceCell.Options.UseBackColor = True
    Me.ld_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.ld_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ld_commeca.Caption = "Commessa"
    Me.ld_commeca.Enabled = True
    Me.ld_commeca.FieldName = "ld_commeca"
    Me.ld_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ld_commeca.Name = "ld_commeca"
    Me.ld_commeca.NTSRepositoryComboBox = Nothing
    Me.ld_commeca.NTSRepositoryItemCheck = Nothing
    Me.ld_commeca.NTSRepositoryItemMemo = Nothing
    Me.ld_commeca.NTSRepositoryItemText = Nothing
    Me.ld_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ld_commeca.OptionsFilter.AllowFilter = False
    Me.ld_commeca.Visible = True
    Me.ld_commeca.VisibleIndex = 0
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
    Me.xx_commeca.NTSRepositoryItemMemo = Nothing
    Me.xx_commeca.NTSRepositoryItemText = Nothing
    Me.xx_commeca.OptionsColumn.AllowEdit = False
    Me.xx_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_commeca.OptionsColumn.ReadOnly = True
    Me.xx_commeca.OptionsFilter.AllowFilter = False
    Me.xx_commeca.Visible = True
    Me.xx_commeca.VisibleIndex = 1
    '
    'ld_fase
    '
    Me.ld_fase.AppearanceCell.Options.UseBackColor = True
    Me.ld_fase.AppearanceCell.Options.UseTextOptions = True
    Me.ld_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ld_fase.Caption = "Fase"
    Me.ld_fase.Enabled = True
    Me.ld_fase.FieldName = "ld_fase"
    Me.ld_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ld_fase.Name = "ld_fase"
    Me.ld_fase.NTSRepositoryComboBox = Nothing
    Me.ld_fase.NTSRepositoryItemCheck = Nothing
    Me.ld_fase.NTSRepositoryItemMemo = Nothing
    Me.ld_fase.NTSRepositoryItemText = Nothing
    Me.ld_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ld_fase.OptionsFilter.AllowFilter = False
    Me.ld_fase.Visible = True
    Me.ld_fase.VisibleIndex = 2
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
    Me.xx_fase.NTSRepositoryItemMemo = Nothing
    Me.xx_fase.NTSRepositoryItemText = Nothing
    Me.xx_fase.OptionsColumn.AllowEdit = False
    Me.xx_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_fase.OptionsColumn.ReadOnly = True
    Me.xx_fase.OptionsFilter.AllowFilter = False
    Me.xx_fase.Visible = True
    Me.xx_fase.VisibleIndex = 3
    '
    'ld_ubicaz
    '
    Me.ld_ubicaz.AppearanceCell.Options.UseBackColor = True
    Me.ld_ubicaz.AppearanceCell.Options.UseTextOptions = True
    Me.ld_ubicaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ld_ubicaz.Caption = "Ubicazione"
    Me.ld_ubicaz.Enabled = True
    Me.ld_ubicaz.FieldName = "ld_ubicaz"
    Me.ld_ubicaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ld_ubicaz.Name = "ld_ubicaz"
    Me.ld_ubicaz.NTSRepositoryComboBox = Nothing
    Me.ld_ubicaz.NTSRepositoryItemCheck = Nothing
    Me.ld_ubicaz.NTSRepositoryItemMemo = Nothing
    Me.ld_ubicaz.NTSRepositoryItemText = Nothing
    Me.ld_ubicaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ld_ubicaz.OptionsFilter.AllowFilter = False
    Me.ld_ubicaz.Visible = True
    Me.ld_ubicaz.VisibleIndex = 4
    '
    'ld_quant
    '
    Me.ld_quant.AppearanceCell.Options.UseBackColor = True
    Me.ld_quant.AppearanceCell.Options.UseTextOptions = True
    Me.ld_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ld_quant.Caption = "Quantità"
    Me.ld_quant.Enabled = True
    Me.ld_quant.FieldName = "ld_quant"
    Me.ld_quant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ld_quant.Name = "ld_quant"
    Me.ld_quant.NTSRepositoryComboBox = Nothing
    Me.ld_quant.NTSRepositoryItemCheck = Nothing
    Me.ld_quant.NTSRepositoryItemMemo = Nothing
    Me.ld_quant.NTSRepositoryItemText = Nothing
    Me.ld_quant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ld_quant.OptionsFilter.AllowFilter = False
    Me.ld_quant.Visible = True
    Me.ld_quant.VisibleIndex = 5
    '
    'ld_valore
    '
    Me.ld_valore.AppearanceCell.Options.UseBackColor = True
    Me.ld_valore.AppearanceCell.Options.UseTextOptions = True
    Me.ld_valore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ld_valore.Caption = "Valore"
    Me.ld_valore.Enabled = True
    Me.ld_valore.FieldName = "ld_valore"
    Me.ld_valore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ld_valore.Name = "ld_valore"
    Me.ld_valore.NTSRepositoryComboBox = Nothing
    Me.ld_valore.NTSRepositoryItemCheck = Nothing
    Me.ld_valore.NTSRepositoryItemMemo = Nothing
    Me.ld_valore.NTSRepositoryItemText = Nothing
    Me.ld_valore.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ld_valore.OptionsFilter.AllowFilter = False
    Me.ld_valore.Visible = True
    Me.ld_valore.VisibleIndex = 6
    '
    'ld_qtacar
    '
    Me.ld_qtacar.AppearanceCell.Options.UseBackColor = True
    Me.ld_qtacar.AppearanceCell.Options.UseTextOptions = True
    Me.ld_qtacar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ld_qtacar.Caption = "Qtà carichi"
    Me.ld_qtacar.Enabled = True
    Me.ld_qtacar.FieldName = "ld_qtacar"
    Me.ld_qtacar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ld_qtacar.Name = "ld_qtacar"
    Me.ld_qtacar.NTSRepositoryComboBox = Nothing
    Me.ld_qtacar.NTSRepositoryItemCheck = Nothing
    Me.ld_qtacar.NTSRepositoryItemMemo = Nothing
    Me.ld_qtacar.NTSRepositoryItemText = Nothing
    Me.ld_qtacar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ld_qtacar.OptionsFilter.AllowFilter = False
    Me.ld_qtacar.Visible = True
    Me.ld_qtacar.VisibleIndex = 7
    '
    'ld_vqtacar
    '
    Me.ld_vqtacar.AppearanceCell.Options.UseBackColor = True
    Me.ld_vqtacar.AppearanceCell.Options.UseTextOptions = True
    Me.ld_vqtacar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ld_vqtacar.Caption = "Valore carichi"
    Me.ld_vqtacar.Enabled = True
    Me.ld_vqtacar.FieldName = "ld_vqtacar"
    Me.ld_vqtacar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ld_vqtacar.Name = "ld_vqtacar"
    Me.ld_vqtacar.NTSRepositoryComboBox = Nothing
    Me.ld_vqtacar.NTSRepositoryItemCheck = Nothing
    Me.ld_vqtacar.NTSRepositoryItemMemo = Nothing
    Me.ld_vqtacar.NTSRepositoryItemText = Nothing
    Me.ld_vqtacar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ld_vqtacar.OptionsFilter.AllowFilter = False
    Me.ld_vqtacar.Visible = True
    Me.ld_vqtacar.VisibleIndex = 8
    '
    'ld_giaini
    '
    Me.ld_giaini.AppearanceCell.Options.UseBackColor = True
    Me.ld_giaini.AppearanceCell.Options.UseTextOptions = True
    Me.ld_giaini.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ld_giaini.Caption = "Giacenza iniziale"
    Me.ld_giaini.Enabled = True
    Me.ld_giaini.FieldName = "ld_giaini"
    Me.ld_giaini.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ld_giaini.Name = "ld_giaini"
    Me.ld_giaini.NTSRepositoryComboBox = Nothing
    Me.ld_giaini.NTSRepositoryItemCheck = Nothing
    Me.ld_giaini.NTSRepositoryItemMemo = Nothing
    Me.ld_giaini.NTSRepositoryItemText = Nothing
    Me.ld_giaini.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ld_giaini.OptionsFilter.AllowFilter = False
    Me.ld_giaini.Visible = True
    Me.ld_giaini.VisibleIndex = 9
    '
    'ld_vgiaini
    '
    Me.ld_vgiaini.AppearanceCell.Options.UseBackColor = True
    Me.ld_vgiaini.AppearanceCell.Options.UseTextOptions = True
    Me.ld_vgiaini.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ld_vgiaini.Caption = "Val. giac. iniziale"
    Me.ld_vgiaini.Enabled = True
    Me.ld_vgiaini.FieldName = "ld_vgiaini"
    Me.ld_vgiaini.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ld_vgiaini.Name = "ld_vgiaini"
    Me.ld_vgiaini.NTSRepositoryComboBox = Nothing
    Me.ld_vgiaini.NTSRepositoryItemCheck = Nothing
    Me.ld_vgiaini.NTSRepositoryItemMemo = Nothing
    Me.ld_vgiaini.NTSRepositoryItemText = Nothing
    Me.ld_vgiaini.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ld_vgiaini.OptionsFilter.AllowFilter = False
    Me.ld_vgiaini.Visible = True
    Me.ld_vgiaini.VisibleIndex = 10
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
    Me.pnPanel2.Controls.Add(Me.grGsld)
    Me.pnPanel2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPanel2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPanel2.Location = New System.Drawing.Point(0, 91)
    Me.pnPanel2.Name = "pnPanel2"
    Me.pnPanel2.Size = New System.Drawing.Size(738, 208)
    Me.pnPanel2.TabIndex = 6
    '
    'FRMMGGSLD
    '
    Me.ClientSize = New System.Drawing.Size(738, 299)
    Me.Controls.Add(Me.pnPanel2)
    Me.Controls.Add(Me.pnPanel1)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMMGGSLD"
    Me.Text = "GESTIONE PROGRESSIVI DEFINITIVI ARTICOLO/LOTTO/COMMESSA/FASE/UBICAZIONE"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grGsld, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvGsld, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPanel1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPanel1.ResumeLayout(False)
    Me.pnPanel1.PerformLayout()
    CType(Me.pnPanel2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPanel2.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

#Region "FORM"
  Public Overridable Sub FRMMGGSLD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

  Public Overridable Sub FRMMGGSLD_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcGsld.Dispose()
      dsGsld.Dispose()
    Catch
    End Try
  End Sub

  Public Overridable Sub FRMMGGSLD_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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
    Dim fmSsld As FRMMGSSLD = Nothing
    Try
      If pnPanel1.Visible = True Then
        If Not Salva() Then Return
        ChiudiDs()
        SetStato0()
      End If

      fmSsld = CType(NTSNewFormModal("FRMMGSSLD"), FRMMGSSLD)
      fmSsld.Init(oMenu, Nothing, DittaCorrente)
      fmSsld.Text = "Nuovo Progressivo Definitivo Articolo/Lotto/Commessa/Fase/Ubicazione"
      fmSsld.InitEntity(oCleGsld)
      fmSsld.ShowDialog(Me)
      If fmSsld.bAnnullato = True Then Return

      strCodart = NTSCStr(fmSsld.edLd_codart.Text)
      nMagaz = NTSCInt(fmSsld.edLd_magaz.Text)

      If Not oCleGsld.Nuovo(DittaCorrente, strCodart, nMagaz, dsGsld) Then Return

      dcGsld.DataSource = dsGsld.Tables("LOTCDEF")
      dsGsld.AcceptChanges()

      grGsld.DataSource = dcGsld

      SetStato1()

      SettaDescrizioni(fmSsld.edLd_codart.Text, fmSsld.lbXx_codart.Text.Trim, _
        NTSCInt(fmSsld.edLd_magaz.Text), fmSsld.lbXx_magaz.Text.Trim)

      grvGsld_NTSFocusedRowChanged(grGsld, Nothing)

      grvGsld.Focus()
      grvGsld.NTSNuovo()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not fmSsld Is Nothing Then fmSsld.Dispose()
      fmSsld = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbApri_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApri.ItemClick
    Dim fmSsld As FRMMGSSLD = Nothing
    Try
      If grGsld.Visible = True Then
        If Not Salva() Then Return
        ChiudiDs()
        SetStato0()
      End If

      fmSsld = CType(NTSNewFormModal("FRMMGSSLD"), FRMMGSSLD)
      fmSsld.Init(oMenu, Nothing, DittaCorrente)
      fmSsld.Text = "Apri Progressivo Definitivo Articolo/Lotto/Commessa/Fase/Ubicazione"
      fmSsld.InitEntity(oCleGsld)
      fmSsld.ShowDialog(Me)
      If fmSsld.bAnnullato = True Then Return

      strCodart = NTSCStr(fmSsld.edLd_codart.Text)
      nMagaz = NTSCInt(fmSsld.edLd_magaz.Text)

      If Not oCleGsld.Apri(DittaCorrente, strCodart, nMagaz, dsGsld) Then Return

      dcGsld.DataSource = dsGsld.Tables("LOTCDEF")
      dsGsld.AcceptChanges()

      grGsld.DataSource = dcGsld

      SetStato1()

      SettaDescrizioni(fmSsld.edLd_codart.Text, fmSsld.lbXx_codart.Text.Trim, _
        NTSCInt(fmSsld.edLd_magaz.Text), fmSsld.lbXx_magaz.Text.Trim)

      grvGsld_NTSFocusedRowChanged(grGsld, Nothing)

      dcGsld.MoveFirst()
      grvGsld.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not fmSsld Is Nothing Then fmSsld.Dispose()
      fmSsld = Nothing
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
      If grvGsld.FocusedColumn.Equals(ld_fase) Then
        NTSZOOM.strIn = NTSCStr(grvGsld.GetFocusedValue)
        oParam.strTipo = strCodart
        NTSZOOM.ZoomStrIn("ZOOMARTFASI", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvGsld.GetFocusedValue) Then grvGsld.SetFocusedValue(NTSZOOM.strIn)
      ElseIf grvGsld.FocusedColumn.Equals(xx_lottox) Then
        NTSZOOM.strIn = NTSCStr(grvGsld.GetFocusedValue)
        oParam.strTipo = strCodart
        NTSZOOM.ZoomStrIn("ZOOMANALOTTI", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvGsld.GetFocusedValue) Then grvGsld.SetFocusedValue(NTSZOOM.strIn)
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
      If Not grvGsld.NTSRipristinaRigaCorrenteBefore(dcGsld, True) Then Return
      oCleGsld.Ripristina(dcGsld.Position, dcGsld.Filter)
      grvGsld.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Try
      If Not grvGsld.NTSDeleteRigaCorrente(dcGsld, True) Then Return
      oCleGsld.Salva(True)
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
      dcGsld.MoveFirst()
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
      dcGsld.MovePrevious()
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
      dcGsld.MoveNext()
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
      dcGsld.MoveLast()
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
  Public Overridable Sub grvGsld_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvGsld.NTSBeforeRowUpdate
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

  Public Overridable Sub grvGsld_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvGsld.NTSFocusedRowChanged
    Try
      '----------------------------------------------------------------------------------
      If bGeslotti = True Then
        If NTSCStr(grvGsld.GetFocusedRowCellValue(xx_lottox)) <> "" Then
          xx_lottox.Enabled = False
        Else
          GctlSetVisEnab(xx_lottox, False)
        End If
      End If
      '----------------------------------------------------------------------------------
      If bGescomm = True Then
        Dim nCommeca As Integer = NTSCInt(grvGsld.GetFocusedRowCellValue(ld_commeca))
        If nCommeca <> 0 Then
          ld_commeca.Enabled = False
        Else
          GctlSetVisEnab(ld_commeca, False)
        End If
      End If
      '----------------------------------------------------------------------------------
      If bGesfasi = True Then
        Dim nFase As Integer = NTSCInt(grvGsld.GetFocusedRowCellValue(ld_fase))
        If nFase <> 0 Then
          ld_fase.Enabled = False
        Else
          GctlSetVisEnab(ld_fase, False)
        End If
      End If
      '----------------------------------------------------------------------------------
      If bGesubic = True Then
        Dim strUbicaz As String = NTSCStr(grvGsld.GetFocusedRowCellValue(ld_ubicaz))
        If strUbicaz.Trim <> "" Then
          ld_ubicaz.Enabled = False
        Else
          GctlSetVisEnab(ld_ubicaz, False)
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
        If grvGsld.NTSGetCurrentDataRow Is Nothing Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129519320958560691, "Posizionarsi prima su una riga di griglia con lotto impostato"))
          e.ZoomHandled = True
          Return
        End If
        bNuovo = False
        oPar.strParam = "APRI;" & oCleGsld.strCodar.PadRight(CLN__STD.CodartMaxLen) & ";" & NTSCStr(grvGsld.NTSGetCurrentDataRow!ld_lotto).PadLeft(9, "0"c) & ";"
      Else
        oPar.strParam = "NUOV;" & oCleGsld.strCodar.PadRight(CLN__STD.CodartMaxLen) & ";" & "000000000" & ";"
      End If

      oTmp.Text = ""
      NTSZOOM.OpenChildGest(oTmp, "ZOOMANALOTTI", DittaCorrente, bNuovo, oPar)

      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo
      grGsld.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub ChiudiDs()
    Try
      If grGsld.Visible = True Then
        dcGsld.Clear()
        dsGsld.Clear()
      End If
    Catch
    End Try
  End Sub

  Public Overridable Function Salva() As Boolean
    Dim i As Integer = 0
    Dim strMsg As String = ""
    Dim dRes As DialogResult
    Dim dttArtdef As New DataTable
    Dim dttArtdefx As New DataTable

    Try
      '----------------------------------------------------------------------------------
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus      
      '----------------------------------------------------------------------------------
      '--- Se non esistono dati in ARTDEF e/o ARTDEFX, chiede conferma prima di
      '--- aggiornare
      '----------------------------------------------------------------------------------
      If oCleGsld.RecordIsChanged Then
        oCleGsld.RitornaArtdefArtdefx(grvGsld.NTSGetCurrentDataRow!ld_codart.ToString, _
          NTSCInt(grvGsld.NTSGetCurrentDataRow!ld_fase), _
          NTSCInt(grvGsld.NTSGetCurrentDataRow!ld_magaz), _
          dttArtdef, dttArtdefx)
        If (dttArtdef.Rows.Count = 0) Or (dttArtdefx.Rows.Count = 0) Then
          strMsg = oApp.Tr(Me, 129071797079907730, "Attenzione!" & vbCrLf & _
            "Per l'articolo '|" & strCodart & "|'" & vbCrLf & _
            "Magazzino '|" & nMagaz.ToString & "|'" & vbCrLf & _
            "Fase '|" & grvGsld.NTSGetCurrentDataRow!ld_fase.ToString & "|'" & "|" & IIf(NTSCInt(grvGsld.NTSGetCurrentDataRow!ld_fase) <> 0, " - " & grvGsld.NTSGetCurrentDataRow!xx_fase.ToString, "").ToString & "|" & vbCrLf & _
            "Commessa '|" & grvGsld.NTSGetCurrentDataRow!ld_commeca.ToString & "|'" & "|" & IIf(NTSCInt(grvGsld.NTSGetCurrentDataRow!ld_commeca) <> 0, " - " & grvGsld.NTSGetCurrentDataRow!xx_commeca.ToString, "").ToString & "|" & vbCrLf & _
            "Lotto '|" & grvGsld.NTSGetCurrentDataRow!xx_lottox.ToString & "|'" & vbCrLf & _
            "Ubicazione '|" & grvGsld.NTSGetCurrentDataRow!ld_ubicaz.ToString & "|'" & vbCrLf & _
            "NON esistono dati relativi in: " & vbCrLf)
          If dttArtdefx.Rows.Count = 0 Then
            strMsg += oApp.Tr(Me, 129071797117096896, " --> PROGRESSIVI DEFINITIVI ARTICOLI" & vbCrLf)
          End If
          If dttArtdef.Rows.Count = 0 Then
            strMsg += oApp.Tr(Me, 129071797142723044, " --> PROGRESSIVI CORRENTI ARTICOLI/MAGAZZINO" & vbCrLf)
          End If
          strMsg += oApp.Tr(Me, 129071797168192935, "E' possibile solo ripristinare.")
          oApp.MsgBoxErr(strMsg)
          Return False
        Else
          strMsg = oApp.Tr(Me, 129071800444589711, "Attenzione!" & vbCrLf & _
            "Ricordarsi di sistemare le quantità e i valori esistenza in:" & vbCrLf & _
            " --> PROGRESSIVI DEFINITIVI ARTICOLI:" & vbCrLf & _
            "      articolo: |" & grvGsld.NTSGetCurrentDataRow!ld_codart.ToString & "| / fase: |" & grvGsld.NTSGetCurrentDataRow!ld_magaz.ToString & "|" & vbCrLf & _
            " --> PROGRESSIVI CORRENTI ARTICOLI/MAGAZZINO" & vbCrLf)
          For i = 0 To (dttArtdef.Rows.Count - 1)
            With dttArtdef.Rows(i)
              strMsg += oApp.Tr(Me, 129071800998208262, "      mag.: |" & grvGsld.NTSGetCurrentDataRow!ld_magaz.ToString & "| / " & _
                "fase: |" & grvGsld.NTSGetCurrentDataRow!ld_fase.ToString & "|")
            End With
          Next
          oApp.MsgBoxInfo(strMsg)
        End If
      End If
      '----------------------------------------------------------------------------------
      dRes = grvGsld.NTSSalvaRigaCorrente(dcGsld, oCleGsld.RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False
          '------------------------------------------------------------------------------
          '--- Test specifico per questo programma!!!
          '------------------------------------------------------------------------------
          If Not oCleGsld.TestPreSalva1(bGeslotti, bGescomm, bGesfasi, bGesubic) Then
            Return False
          End If
          '------------------------------------------------------------------------------
          If Not oCleGsld.Salva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleGsld.Ripristina(dcGsld.Position, dcGsld.Filter)
        Case System.Windows.Forms.DialogResult.Cancel
          'non posso fare nulla
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          'la riga non ha subito modifiche
      End Select
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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
    GctlSetVisEnab(ld_commeca, True)
    GctlSetVisEnab(xx_commeca, True)
    GctlSetVisEnab(ld_fase, True)
    GctlSetVisEnab(xx_fase, True)
    GctlSetVisEnab(ld_ubicaz, True)
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
    oCleGsld.SettaFlags(DittaCorrente, strCodart, bGeslotti, bGescomm, bGesfasi, bGesubic)
    If bGeslotti = False Then xx_lottox.Visible = False
    If bGescomm = False Then
      ld_commeca.Visible = False
      xx_commeca.Visible = False
    End If
    If bGesfasi = False Then
      ld_fase.Visible = False
      xx_fase.Visible = False
    End If
    If bGesubic = False Then ld_ubicaz.Visible = False
    '----------------------------------------------------------------------------------
    grGsld.Focus()
  End Sub

  Public Overridable Sub SettaDescrizioni(ByVal strCodart As String, ByVal strDesart As String, _
    ByVal nMagaz As Integer, ByVal strDesmagaz As String)
    Try
      lbCodart.Text = oApp.Tr(Me, 129080280356430162, "Articolo: '|" & strCodart & "|'")
      If strDesart <> "" Then lbCodart.Text = lbCodart.Text & " - " & strDesart
      If nMagaz <> 0 Then
        lbMagaz.Text = oApp.Tr(Me, 129080279858279658, "Magazzino: '|" & nMagaz.ToString & "|'")
        If strDesmagaz <> "" Then lbMagaz.Text = lbMagaz.Text & " - " & strDesmagaz
      End If
    Catch ex As Exception

    End Try
  End Sub

End Class
