Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGGSAL
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
  Public oCleGsal As CLEMGGSAL
  Public oCallParams As CLE__CLDP
  Public dsGsal As DataSet
  Public dcGsal As BindingSource = New BindingSource()

  Public strCodart As String
  Public nFase As Integer

  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbApri As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents grGsal As NTSInformatica.NTSGrid
  Public WithEvents grvGsal As NTSInformatica.NTSGridView
  Public WithEvents al_anno As NTSInformatica.NTSGridColumn
  Public WithEvents xx_anno As NTSInformatica.NTSGridColumn
  Public WithEvents al_esist As NTSInformatica.NTSGridColumn
  Public WithEvents al_valore As NTSInformatica.NTSGridColumn
  Public WithEvents al_qtadelta As NTSInformatica.NTSGridColumn
  Public WithEvents al_valdelta As NTSInformatica.NTSGridColumn
  Public WithEvents al_tipval As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn17 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_tipval As NTSInformatica.NTSGridColumn
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGGSAL", "BEMGGSAL", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128559147261038798, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleGsal = CType(oTmp, CLEMGGSAL)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGGSAL", strRemoteServer, strRemotePort)
    AddHandler oCleGsal.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleGsal.Init(oApp, oScript, oMenu.oCleComm, "ARTLIF", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '--------------------------------------------------------------------------------------------------------------
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbApri.GlyphPath = (oApp.ChildImageDir & "\open.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\recagg.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\recrestore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\recdelete.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      '--------------------------------------------------------------------------------------------------------------
      tlbMain.NTSSetToolTip()
      '--------------------------------------------------------------------------------------------------------------
      grvGsal.NTSSetParam(oMenu, oApp.Tr(Me, 128559189446424977, "Griglia Storico LIFO"))
      al_anno.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128559189818451465, "Esercizio"), CLN__STD.tabesco)
      xx_anno.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128559192074985081, "Descrizione esercizio"), 30, True)
      al_esist.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128559192096547305, "Esistenza"), "0.000", 13, -9999999999999D, 9999999999999D)
      al_valore.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128559192523104345, "Valore esistenza"), "0.00", 13, -9999999999999D, 9999999999999D)
      al_qtadelta.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128559192879974777, "Quantità delta"), "0.000", 13, -9999999999999D, 9999999999999D)
      al_valdelta.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128559193034191553, "Valore delta"), "0.00", 13, -9999999999999D, 9999999999999D)
      al_tipval.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128980846308560025, "Valorizzazione"), CLN__STD.tablist, False)
      xx_tipval.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128980846551528775, "Descrizione valorizzazione"), 50, True)
      '--------------------------------------------------------------------------------------------------------------
      al_tipval.NTSSetRichiesto()
      '--------------------------------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGGSAL))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbApri = New NTSInformatica.NTSBarButtonItem
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
    Me.grGsal = New NTSInformatica.NTSGrid
    Me.grvGsal = New NTSInformatica.NTSGridView
    Me.al_anno = New NTSInformatica.NTSGridColumn
    Me.xx_anno = New NTSInformatica.NTSGridColumn
    Me.al_esist = New NTSInformatica.NTSGridColumn
    Me.al_valore = New NTSInformatica.NTSGridColumn
    Me.al_qtadelta = New NTSInformatica.NTSGridColumn
    Me.al_valdelta = New NTSInformatica.NTSGridColumn
    Me.al_tipval = New NTSInformatica.NTSGridColumn
    Me.xx_tipval = New NTSInformatica.NTSGridColumn
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grGsal, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvGsal, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbApri, Me.tlbSalva, Me.tlbRipristina, Me.tlbCancella, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbUltimo, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom})
    Me.NtsBarManager1.MaxItemId = 13
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
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.Id = 2
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
    Me.tlbCancella.Id = 4
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
    'grGsal
    '
    Me.grGsal.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grGsal.EmbeddedNavigator.Name = ""
    Me.grGsal.Location = New System.Drawing.Point(0, 30)
    Me.grGsal.MainView = Me.grvGsal
    Me.grGsal.Name = "grGsal"
    Me.grGsal.Size = New System.Drawing.Size(682, 271)
    Me.grGsal.TabIndex = 4
    Me.grGsal.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvGsal})
    '
    'grvGsal
    '
    Me.grvGsal.ActiveFilterEnabled = False
    Me.grvGsal.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.al_anno, Me.xx_anno, Me.al_esist, Me.al_valore, Me.al_qtadelta, Me.al_valdelta, Me.al_tipval, Me.xx_tipval})
    Me.grvGsal.CustomizationFormBounds = New System.Drawing.Rectangle(592, 326, 208, 170)
    Me.grvGsal.Enabled = True
    Me.grvGsal.GridControl = Me.grGsal
    Me.grvGsal.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvGsal.Name = "grvGsal"
    Me.grvGsal.NTSAllowDelete = True
    Me.grvGsal.NTSAllowInsert = True
    Me.grvGsal.NTSAllowUpdate = True
    Me.grvGsal.NTSMenuContext = Nothing
    Me.grvGsal.OptionsCustomization.AllowRowSizing = True
    Me.grvGsal.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvGsal.OptionsNavigation.UseTabKey = False
    Me.grvGsal.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvGsal.OptionsView.ColumnAutoWidth = False
    Me.grvGsal.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvGsal.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvGsal.OptionsView.ShowGroupPanel = False
    Me.grvGsal.RowHeight = 14
    '
    'al_anno
    '
    Me.al_anno.AppearanceCell.Options.UseBackColor = True
    Me.al_anno.AppearanceCell.Options.UseTextOptions = True
    Me.al_anno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.al_anno.Caption = "Esercizio"
    Me.al_anno.Enabled = True
    Me.al_anno.FieldName = "al_anno"
    Me.al_anno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.al_anno.Name = "al_anno"
    Me.al_anno.NTSRepositoryComboBox = Nothing
    Me.al_anno.NTSRepositoryItemCheck = Nothing
    Me.al_anno.NTSRepositoryItemMemo = Nothing
    Me.al_anno.NTSRepositoryItemText = Nothing
    Me.al_anno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.al_anno.OptionsFilter.AllowFilter = False
    Me.al_anno.Visible = True
    Me.al_anno.VisibleIndex = 0
    Me.al_anno.Width = 52
    '
    'xx_anno
    '
    Me.xx_anno.AppearanceCell.Options.UseBackColor = True
    Me.xx_anno.AppearanceCell.Options.UseTextOptions = True
    Me.xx_anno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_anno.Caption = "Descr. esercizio"
    Me.xx_anno.Enabled = False
    Me.xx_anno.FieldName = "xx_anno"
    Me.xx_anno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_anno.Name = "xx_anno"
    Me.xx_anno.NTSRepositoryComboBox = Nothing
    Me.xx_anno.NTSRepositoryItemCheck = Nothing
    Me.xx_anno.NTSRepositoryItemMemo = Nothing
    Me.xx_anno.NTSRepositoryItemText = Nothing
    Me.xx_anno.OptionsColumn.AllowEdit = False
    Me.xx_anno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_anno.OptionsColumn.ReadOnly = True
    Me.xx_anno.OptionsFilter.AllowFilter = False
    Me.xx_anno.Visible = True
    Me.xx_anno.VisibleIndex = 1
    Me.xx_anno.Width = 87
    '
    'al_esist
    '
    Me.al_esist.AppearanceCell.Options.UseBackColor = True
    Me.al_esist.AppearanceCell.Options.UseTextOptions = True
    Me.al_esist.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.al_esist.Caption = "Esistenza"
    Me.al_esist.Enabled = True
    Me.al_esist.FieldName = "al_esist"
    Me.al_esist.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.al_esist.Name = "al_esist"
    Me.al_esist.NTSRepositoryComboBox = Nothing
    Me.al_esist.NTSRepositoryItemCheck = Nothing
    Me.al_esist.NTSRepositoryItemMemo = Nothing
    Me.al_esist.NTSRepositoryItemText = Nothing
    Me.al_esist.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.al_esist.OptionsFilter.AllowFilter = False
    Me.al_esist.Visible = True
    Me.al_esist.VisibleIndex = 2
    '
    'al_valore
    '
    Me.al_valore.AppearanceCell.Options.UseBackColor = True
    Me.al_valore.AppearanceCell.Options.UseTextOptions = True
    Me.al_valore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.al_valore.Caption = "Valore esistenza"
    Me.al_valore.Enabled = True
    Me.al_valore.FieldName = "al_valore"
    Me.al_valore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.al_valore.Name = "al_valore"
    Me.al_valore.NTSRepositoryComboBox = Nothing
    Me.al_valore.NTSRepositoryItemCheck = Nothing
    Me.al_valore.NTSRepositoryItemMemo = Nothing
    Me.al_valore.NTSRepositoryItemText = Nothing
    Me.al_valore.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.al_valore.OptionsFilter.AllowFilter = False
    Me.al_valore.Visible = True
    Me.al_valore.VisibleIndex = 3
    '
    'al_qtadelta
    '
    Me.al_qtadelta.AppearanceCell.Options.UseBackColor = True
    Me.al_qtadelta.AppearanceCell.Options.UseTextOptions = True
    Me.al_qtadelta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.al_qtadelta.Caption = "Qtà delta"
    Me.al_qtadelta.Enabled = True
    Me.al_qtadelta.FieldName = "al_qtadelta"
    Me.al_qtadelta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.al_qtadelta.Name = "al_qtadelta"
    Me.al_qtadelta.NTSRepositoryComboBox = Nothing
    Me.al_qtadelta.NTSRepositoryItemCheck = Nothing
    Me.al_qtadelta.NTSRepositoryItemMemo = Nothing
    Me.al_qtadelta.NTSRepositoryItemText = Nothing
    Me.al_qtadelta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.al_qtadelta.OptionsFilter.AllowFilter = False
    Me.al_qtadelta.Visible = True
    Me.al_qtadelta.VisibleIndex = 4
    '
    'al_valdelta
    '
    Me.al_valdelta.AppearanceCell.Options.UseBackColor = True
    Me.al_valdelta.AppearanceCell.Options.UseTextOptions = True
    Me.al_valdelta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.al_valdelta.Caption = "Valore delta"
    Me.al_valdelta.Enabled = True
    Me.al_valdelta.FieldName = "al_valdelta"
    Me.al_valdelta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.al_valdelta.Name = "al_valdelta"
    Me.al_valdelta.NTSRepositoryComboBox = Nothing
    Me.al_valdelta.NTSRepositoryItemCheck = Nothing
    Me.al_valdelta.NTSRepositoryItemMemo = Nothing
    Me.al_valdelta.NTSRepositoryItemText = Nothing
    Me.al_valdelta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.al_valdelta.OptionsFilter.AllowFilter = False
    Me.al_valdelta.Visible = True
    Me.al_valdelta.VisibleIndex = 5
    '
    'al_tipval
    '
    Me.al_tipval.AppearanceCell.Options.UseBackColor = True
    Me.al_tipval.AppearanceCell.Options.UseTextOptions = True
    Me.al_tipval.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.al_tipval.Caption = "Valorizzazione"
    Me.al_tipval.Enabled = True
    Me.al_tipval.FieldName = "al_tipval"
    Me.al_tipval.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.al_tipval.Name = "al_tipval"
    Me.al_tipval.NTSRepositoryComboBox = Nothing
    Me.al_tipval.NTSRepositoryItemCheck = Nothing
    Me.al_tipval.NTSRepositoryItemMemo = Nothing
    Me.al_tipval.NTSRepositoryItemText = Nothing
    Me.al_tipval.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.al_tipval.OptionsFilter.AllowFilter = False
    Me.al_tipval.Visible = True
    Me.al_tipval.VisibleIndex = 6
    '
    'xx_tipval
    '
    Me.xx_tipval.AppearanceCell.Options.UseBackColor = True
    Me.xx_tipval.AppearanceCell.Options.UseTextOptions = True
    Me.xx_tipval.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_tipval.Caption = "Descrizione"
    Me.xx_tipval.Enabled = True
    Me.xx_tipval.FieldName = "xx_tipval"
    Me.xx_tipval.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_tipval.Name = "xx_tipval"
    Me.xx_tipval.NTSRepositoryComboBox = Nothing
    Me.xx_tipval.NTSRepositoryItemCheck = Nothing
    Me.xx_tipval.NTSRepositoryItemMemo = Nothing
    Me.xx_tipval.NTSRepositoryItemText = Nothing
    Me.xx_tipval.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_tipval.OptionsFilter.AllowFilter = False
    Me.xx_tipval.Visible = True
    Me.xx_tipval.VisibleIndex = 7
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 12
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'FRMMGGSAL
    '
    Me.ClientSize = New System.Drawing.Size(682, 301)
    Me.Controls.Add(Me.grGsal)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMMGGSAL"
    Me.Text = "STORICO LIFO"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grGsal, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvGsal, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

#Region "FORM"
  Public Overridable Sub FRMMGGSAL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

  Public Overridable Sub FRMMGGSAL_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcGsal.Dispose()
      dsGsal.Dispose()
    Catch
    End Try
  End Sub

  Public Overridable Sub FRMMGGSAL_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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
    Dim fmCoaf As FRMMGCOAF = Nothing
    Try
      If grGsal.Visible = True Then
        If Not Salva() Then Return
        ChiudiDs()
        SetStato0()
      End If

      fmCoaf = CType(NTSNewFormModal("FRMMGCOAF"), FRMMGCOAF)
      fmCoaf.Init(oMenu, Nothing, DittaCorrente)
      fmCoaf.Text = "Nuovo Storico LIFO"
      fmCoaf.InitEntity(oCleGsal)
      fmCoaf.ShowDialog(Me)
      If fmCoaf.bAnnullato = True Then Return

      strCodart = NTSCStr(fmCoaf.edAl_codart.Text)
      nFase = NTSCInt(fmCoaf.edAl_fase.Text)

      If Not oCleGsal.Nuovo(DittaCorrente, strCodart, nFase, dsGsal) Then Return

      dcGsal.DataSource = dsGsal.Tables("ARTLIF")
      dsGsal.AcceptChanges()

      grGsal.DataSource = dcGsal

      SetStato1()

      CaptionForm(fmCoaf.edAl_codart.Text, fmCoaf.lbXx_codart.Text.Trim, _
        NTSCInt(fmCoaf.edAl_fase.Text), fmCoaf.lbXx_fase.Text.Trim)

      grvGsal.NTSNuovo()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not fmCoaf Is Nothing Then fmCoaf.Dispose()
      fmCoaf = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbApri_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApri.ItemClick
    Dim fmCoaf As FRMMGCOAF = Nothing
    Try
      If grGsal.Visible = True Then
        If Not Salva() Then Return
        ChiudiDs()
        SetStato0()
      End If

      fmCoaf = CType(NTSNewFormModal("FRMMGCOAF"), FRMMGCOAF)
      fmCoaf.Init(oMenu, Nothing, DittaCorrente)
      fmCoaf.Text = "Apri Storico LIFO"
      fmCoaf.InitEntity(oCleGsal)
      fmCoaf.ShowDialog(Me)
      If fmCoaf.bAnnullato = True Then Return

      strCodart = NTSCStr(fmCoaf.edAl_codart.Text)
      nFase = NTSCInt(fmCoaf.edAl_fase.Text)

      If Not oCleGsal.Apri(DittaCorrente, strCodart, nFase, dsGsal) Then Return

      dcGsal.DataSource = dsGsal.Tables("ARTLIF")
      dsGsal.AcceptChanges()

      grGsal.DataSource = dcGsal

      SetStato1()

      CaptionForm(fmCoaf.edAl_codart.Text, fmCoaf.lbXx_codart.Text.Trim, _
        NTSCInt(fmCoaf.edAl_fase.Text), fmCoaf.lbXx_fase.Text.Trim)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not fmCoaf Is Nothing Then fmCoaf.Dispose()
      fmCoaf = Nothing
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

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvGsal.NTSRipristinaRigaCorrenteBefore(dcGsal, True) Then Return
      oCleGsal.Ripristina(dcGsal.Position, dcGsal.Filter)
      grvGsal.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      NTSCallStandardZoom()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Try
      If Not grvGsal.NTSDeleteRigaCorrente(dcGsal, True) Then Return
      oCleGsal.Salva(True)
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
      dcGsal.MoveFirst()
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
      dcGsal.MovePrevious()
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
      dcGsal.MoveNext()
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
      dcGsal.MoveLast()
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
  Public Overridable Sub grvGsal_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvGsal.NTSBeforeRowUpdate
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

  Public Overridable Sub grvGsal_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvGsal.NTSFocusedRowChanged
    Try
      '-------------------------------------------------
      'se il codice esercizio è diverso da 0 blocco la colonna, diversamente la rendo editabile
      Dim nAnno As Integer = NTSCInt(grvGsal.GetFocusedRowCellValue(al_anno))
      If nAnno <> 0 Then
        al_anno.Enabled = False
      Else
        GctlSetVisEnab(al_anno, False)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub CaptionForm(ByVal strCodart As String, ByVal strDesart As String, _
    ByVal nFase As Integer, ByVal strDesfase As String)
    Try
      Me.Text = oApp.Tr(Me, 129080360281288511, "Storico LIFO --> Articolo: '|" & strCodart & "|'")
      If strDesart <> "" Then Me.Text = Me.Text & " - " & strDesart
      If nFase <> 0 Then
        Me.Text = Me.Text & oApp.Tr(Me, 129080361260551130, "   Fase: '|" & nFase.ToString & "|'")
        If strDesfase <> "" Then Me.Text = Me.Text & " - " & strDesfase
      End If
    Catch ex As Exception

    End Try
  End Sub

  Public Overridable Sub ChiudiDs()
    Try
      If grGsal.Visible = True Then
        dcGsal.Clear()
        dsGsal.Clear()
      End If
    Catch
    End Try
  End Sub

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvGsal.NTSSalvaRigaCorrente(dcGsal, oCleGsal.RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleGsal.Salva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleGsal.Ripristina(dcGsal.Position, dcGsal.Filter)
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
    grGsal.Visible = False
    tlbSalva.Enabled = False
    tlbRipristina.Enabled = False
    tlbZoom.Enabled = False
    tlbCancella.Enabled = False
    tlbPrimo.Enabled = False
    tlbPrecedente.Enabled = False
    tlbSuccessivo.Enabled = False
    tlbUltimo.Enabled = False
  End Sub

  Public Overridable Sub SetStato1()
    GctlSetVisEnab(grGsal, True)
    GctlSetVisEnab(tlbSalva, False)
    GctlSetVisEnab(tlbRipristina, False)
    GctlSetVisEnab(tlbZoom, False)
    GctlSetVisEnab(tlbCancella, False)
    GctlSetVisEnab(tlbPrimo, False)
    GctlSetVisEnab(tlbPrecedente, False)
    GctlSetVisEnab(tlbSuccessivo, False)
    GctlSetVisEnab(tlbUltimo, False)
    grGsal.Focus()
  End Sub

End Class
