Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGCKIT
#Region "Variabili"
  Public oCleArmd As CLEMGARMD
  Public oCallParams As CLE__CLDP
  Public dsCkit As New DataSet
  Public dcCkit As New BindingSource()
  Public dttCkit As DataTable = Nothing
  Public dtrCkit() As DataRow = Nothing
  Public components As System.ComponentModel.IContainer

  Public dttUm As DataTable = Nothing                    'elenco delle unità di misura utilizzate in artico

  Public bServer As Boolean

  Public WithEvents grCkit As NTSInformatica.NTSGrid
  Public WithEvents grvCkit As NTSInformatica.NTSGridView
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
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem

  Public WithEvents codditt As NTSInformatica.NTSGridColumn
  Public WithEvents ak_codart As NTSInformatica.NTSGridColumn
  Public WithEvents ak_riga As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codfigli As NTSInformatica.NTSGridColumn
  Public WithEvents ak_unmis As NTSInformatica.NTSGridColumn
  Public WithEvents ak_colli As NTSInformatica.NTSGridColumn
  Public WithEvents ak_ump As NTSInformatica.NTSGridColumn
  Public WithEvents ak_codfigli As NTSInformatica.NTSGridColumn
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents ak_quant As NTSInformatica.NTSGridColumn
  Public WithEvents ak_desfigli As NTSInformatica.NTSGridColumn
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
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
    'creo e attivo l'entity e inizializzo la funzione che dovrÃ  rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGARMD", "BEMGARMD", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128550728307822408, "ERRORE in fase di creazione Entity:" & vbCrLf & strErr))
      Return False
    End If
    oCleArmd = CType(oTmp, CLEMGARMD)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGARMD", strRemoteServer, strRemotePort)
    AddHandler oCleArmd.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleArmd.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    oCleArmd.strPrgParent = oCallParams.strPar1
    oCleArmd.strCkitCodart = oCallParams.strPar2

    Return True
  End Function
  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGCKIT))
    Me.grCkit = New NTSInformatica.NTSGrid
    Me.grvCkit = New NTSInformatica.NTSGridView
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.ak_codart = New NTSInformatica.NTSGridColumn
    Me.ak_riga = New NTSInformatica.NTSGridColumn
    Me.ak_codfigli = New NTSInformatica.NTSGridColumn
    Me.xx_codfigli = New NTSInformatica.NTSGridColumn
    Me.ak_unmis = New NTSInformatica.NTSGridColumn
    Me.ak_colli = New NTSInformatica.NTSGridColumn
    Me.ak_ump = New NTSInformatica.NTSGridColumn
    Me.ak_quant = New NTSInformatica.NTSGridColumn
    Me.ak_desfigli = New NTSInformatica.NTSGridColumn
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
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
    CType(Me.grCkit, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvCkit, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Image = CType(resources.GetObject("frmPopup.Appearance.Image"), System.Drawing.Image)
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    Me.frmPopup.ClientSize = New System.Drawing.Size(77, 21)
    '
    'grCkit
    '
    Me.grCkit.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grCkit.EmbeddedNavigator.Name = ""
    Me.grCkit.Location = New System.Drawing.Point(0, 30)
    Me.grCkit.MainView = Me.grvCkit
    Me.grCkit.Name = "grCkit"
    Me.grCkit.Size = New System.Drawing.Size(667, 336)
    Me.grCkit.TabIndex = 0
    Me.grCkit.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvCkit})
    '
    'grvCkit
    '
    Me.grvCkit.ActiveFilterEnabled = False
    Me.grvCkit.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.codditt, Me.ak_codart, Me.ak_riga, Me.ak_codfigli, Me.xx_codfigli, Me.ak_unmis, Me.ak_colli, Me.ak_ump, Me.ak_quant, Me.ak_desfigli})
    Me.grvCkit.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvCkit.Enabled = True
    Me.grvCkit.GridControl = Me.grCkit
    Me.grvCkit.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvCkit.Name = "grvCkit"
    Me.grvCkit.NTSAllowDelete = True
    Me.grvCkit.NTSAllowInsert = True
    Me.grvCkit.NTSAllowUpdate = True
    Me.grvCkit.NTSMenuContext = Nothing
    Me.grvCkit.OptionsCustomization.AllowRowSizing = True
    Me.grvCkit.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvCkit.OptionsNavigation.UseTabKey = False
    Me.grvCkit.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvCkit.OptionsView.ColumnAutoWidth = False
    Me.grvCkit.OptionsView.EnableAppearanceEvenRow = True
    Me.grvCkit.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvCkit.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvCkit.OptionsView.ShowGroupPanel = False
    Me.grvCkit.RowHeight = 16
    '
    'codditt
    '
    Me.codditt.AppearanceCell.Options.UseBackColor = True
    Me.codditt.AppearanceCell.Options.UseTextOptions = True
    Me.codditt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.codditt.Caption = "Cod. ditta"
    Me.codditt.Enabled = False
    Me.codditt.FieldName = "codditt"
    Me.codditt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.codditt.Name = "codditt"
    Me.codditt.NTSRepositoryComboBox = Nothing
    Me.codditt.NTSRepositoryItemCheck = Nothing
    Me.codditt.NTSRepositoryItemText = Nothing
    Me.codditt.OptionsColumn.AllowEdit = False
    Me.codditt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.codditt.OptionsColumn.ReadOnly = True
    Me.codditt.OptionsFilter.AllowFilter = False
    '
    'ak_codart
    '
    Me.ak_codart.AppearanceCell.Options.UseBackColor = True
    Me.ak_codart.AppearanceCell.Options.UseTextOptions = True
    Me.ak_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ak_codart.Caption = "Codice articolo"
    Me.ak_codart.Enabled = False
    Me.ak_codart.FieldName = "ak_codart"
    Me.ak_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ak_codart.Name = "ak_codart"
    Me.ak_codart.NTSRepositoryComboBox = Nothing
    Me.ak_codart.NTSRepositoryItemCheck = Nothing
    Me.ak_codart.NTSRepositoryItemText = Nothing
    Me.ak_codart.OptionsColumn.AllowEdit = False
    Me.ak_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ak_codart.OptionsColumn.ReadOnly = True
    Me.ak_codart.OptionsFilter.AllowFilter = False
    '
    'ak_riga
    '
    Me.ak_riga.AppearanceCell.Options.UseBackColor = True
    Me.ak_riga.AppearanceCell.Options.UseTextOptions = True
    Me.ak_riga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ak_riga.Caption = "Riga"
    Me.ak_riga.Enabled = False
    Me.ak_riga.FieldName = "ak_riga"
    Me.ak_riga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ak_riga.Name = "ak_riga"
    Me.ak_riga.NTSRepositoryComboBox = Nothing
    Me.ak_riga.NTSRepositoryItemCheck = Nothing
    Me.ak_riga.NTSRepositoryItemText = Nothing
    Me.ak_riga.OptionsColumn.AllowEdit = False
    Me.ak_riga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ak_riga.OptionsColumn.ReadOnly = True
    Me.ak_riga.OptionsFilter.AllowFilter = False
    '
    'ak_codfigli
    '
    Me.ak_codfigli.AppearanceCell.Options.UseBackColor = True
    Me.ak_codfigli.AppearanceCell.Options.UseTextOptions = True
    Me.ak_codfigli.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ak_codfigli.Caption = "Cod. Figli"
    Me.ak_codfigli.Enabled = True
    Me.ak_codfigli.FieldName = "ak_codfigli"
    Me.ak_codfigli.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ak_codfigli.Name = "ak_codfigli"
    Me.ak_codfigli.NTSRepositoryComboBox = Nothing
    Me.ak_codfigli.NTSRepositoryItemCheck = Nothing
    Me.ak_codfigli.NTSRepositoryItemText = Nothing
    Me.ak_codfigli.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ak_codfigli.OptionsFilter.AllowFilter = False
    Me.ak_codfigli.Visible = True
    Me.ak_codfigli.VisibleIndex = 0
    '
    'xx_codfigli
    '
    Me.xx_codfigli.AppearanceCell.Options.UseBackColor = True
    Me.xx_codfigli.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codfigli.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codfigli.Caption = "Descrizione figli"
    Me.xx_codfigli.Enabled = False
    Me.xx_codfigli.FieldName = "xx_codfigli"
    Me.xx_codfigli.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codfigli.Name = "xx_codfigli"
    Me.xx_codfigli.NTSRepositoryComboBox = Nothing
    Me.xx_codfigli.NTSRepositoryItemCheck = Nothing
    Me.xx_codfigli.NTSRepositoryItemText = Nothing
    Me.xx_codfigli.OptionsColumn.AllowEdit = False
    Me.xx_codfigli.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codfigli.OptionsColumn.ReadOnly = True
    Me.xx_codfigli.OptionsFilter.AllowFilter = False
    Me.xx_codfigli.Visible = True
    Me.xx_codfigli.VisibleIndex = 1
    '
    'ak_unmis
    '
    Me.ak_unmis.AppearanceCell.Options.UseBackColor = True
    Me.ak_unmis.AppearanceCell.Options.UseTextOptions = True
    Me.ak_unmis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ak_unmis.Caption = "U.M."
    Me.ak_unmis.Enabled = True
    Me.ak_unmis.FieldName = "ak_unmis"
    Me.ak_unmis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ak_unmis.Name = "ak_unmis"
    Me.ak_unmis.NTSRepositoryComboBox = Nothing
    Me.ak_unmis.NTSRepositoryItemCheck = Nothing
    Me.ak_unmis.NTSRepositoryItemText = Nothing
    Me.ak_unmis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ak_unmis.OptionsFilter.AllowFilter = False
    Me.ak_unmis.Visible = True
    Me.ak_unmis.VisibleIndex = 2
    '
    'ak_colli
    '
    Me.ak_colli.AppearanceCell.Options.UseBackColor = True
    Me.ak_colli.AppearanceCell.Options.UseTextOptions = True
    Me.ak_colli.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ak_colli.Caption = "Colli"
    Me.ak_colli.Enabled = True
    Me.ak_colli.FieldName = "ak_colli"
    Me.ak_colli.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ak_colli.Name = "ak_colli"
    Me.ak_colli.NTSRepositoryComboBox = Nothing
    Me.ak_colli.NTSRepositoryItemCheck = Nothing
    Me.ak_colli.NTSRepositoryItemText = Nothing
    Me.ak_colli.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ak_colli.OptionsFilter.AllowFilter = False
    Me.ak_colli.Visible = True
    Me.ak_colli.VisibleIndex = 3
    '
    'ak_ump
    '
    Me.ak_ump.AppearanceCell.Options.UseBackColor = True
    Me.ak_ump.AppearanceCell.Options.UseTextOptions = True
    Me.ak_ump.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ak_ump.Caption = "U.M.P."
    Me.ak_ump.Enabled = True
    Me.ak_ump.FieldName = "ak_ump"
    Me.ak_ump.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ak_ump.Name = "ak_ump"
    Me.ak_ump.NTSRepositoryComboBox = Nothing
    Me.ak_ump.NTSRepositoryItemCheck = Nothing
    Me.ak_ump.NTSRepositoryItemText = Nothing
    Me.ak_ump.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ak_ump.OptionsFilter.AllowFilter = False
    Me.ak_ump.Visible = True
    Me.ak_ump.VisibleIndex = 4
    '
    'ak_quant
    '
    Me.ak_quant.AppearanceCell.Options.UseBackColor = True
    Me.ak_quant.AppearanceCell.Options.UseTextOptions = True
    Me.ak_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ak_quant.Caption = "Quantità"
    Me.ak_quant.Enabled = True
    Me.ak_quant.FieldName = "ak_quant"
    Me.ak_quant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ak_quant.Name = "ak_quant"
    Me.ak_quant.NTSRepositoryComboBox = Nothing
    Me.ak_quant.NTSRepositoryItemCheck = Nothing
    Me.ak_quant.NTSRepositoryItemText = Nothing
    Me.ak_quant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ak_quant.OptionsFilter.AllowFilter = False
    Me.ak_quant.Visible = True
    Me.ak_quant.VisibleIndex = 5
    '
    'ak_desfigli
    '
    Me.ak_desfigli.AppearanceCell.Options.UseBackColor = True
    Me.ak_desfigli.AppearanceCell.Options.UseTextOptions = True
    Me.ak_desfigli.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ak_desfigli.Caption = "Descr. aggiuntiva"
    Me.ak_desfigli.Enabled = True
    Me.ak_desfigli.FieldName = "ak_desfigli"
    Me.ak_desfigli.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ak_desfigli.Name = "ak_desfigli"
    Me.ak_desfigli.NTSRepositoryComboBox = Nothing
    Me.ak_desfigli.NTSRepositoryItemCheck = Nothing
    Me.ak_desfigli.NTSRepositoryItemText = Nothing
    Me.ak_desfigli.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ak_desfigli.OptionsFilter.AllowFilter = False
    Me.ak_desfigli.Visible = True
    Me.ak_desfigli.VisibleIndex = 6
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbGuida, Me.tlbEsci, Me.tlbStampaVideo, Me.tlbStampa, Me.tlbStrumenti, Me.tlbImpostaStampante})
    Me.NtsBarManager1.MaxItemId = 16
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 4
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 14
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta stampante"
    Me.tlbImpostaStampante.Id = 15
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 12
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 11
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
    'FRMMGCKIT
    '
    Me.ClientSize = New System.Drawing.Size(667, 366)
    Me.Controls.Add(Me.grCkit)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.HelpContext = ""
    Me.Name = "FRMMGCKIT"
    Me.NTSLastControlFocussed = Me.grCkit
    Me.Text = "Composizione Kit"
    CType(Me.grCkit, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvCkit, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
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
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        '  'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      '-------------------------------------------------
      'carico le unità di misura nella colonna colli: caso particolare
      'carico tutte le unità di misura gestite dagli articoli, 
      'poi al cambio di riga filtrero nel combo solo quelle gestite dall'articolo in analisi
      dttUm = oCleArmd.CaricaUnMis()
      dttUm.AcceptChanges()

      grvCkit.NTSSetParam(oMenu, oApp.Tr(Me, 128828970771044358, "Composizione Kit"))
      codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828970787606964, "Cod. ditta"), 12, False)
      ak_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828970803857068, "Codice articolo"), CLN__STD.CodartMaxLen, False)
      ak_riga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828970817294654, "Riga"), "0", 15)
      ak_codfigli.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128828970831044742, "Cod. Figli"), tabartico, False)
      xx_codfigli.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828970848701105, "Descrizione figli"), 0, True)
      ak_unmis.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128828970863232448, "U.M."), dttUm, "tb_codumis", "tb_codumis")
      ak_colli.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828970877451289, "Colli"), "#,##0.000", 15, -9999999999, 9999999999)
      ak_ump.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828970895263903, "U.M.P."), 3, False)
      ak_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828970912764015, "Quantità"), "#,##0.000", 15, -9999999999, 9999999999)
      ak_desfigli.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828970930576629, "Descr. aggiuntiva"), 40, True)

      ak_codfigli.NTSSetRichiesto()

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

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvCkit.NTSSalvaRigaCorrente(dcCkit, oCleArmd.CkitRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleArmd.CkitSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleArmd.CkitRipristina(dcCkit.Position, dcCkit.Filter)
        Case System.Windows.Forms.DialogResult.Cancel
          'non posso fare nulla
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          'la riga non ha subito modifiche
      End Select
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

#Region "EventiForm"
  Public Overridable Sub FRMMGCKIT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      oCleArmd.bConsentiModificaArticoliKit = CBool(oMenu.GetSettingBus("OPZIONI", ".", ".", "ConsentiModificaArticoliKit", "0", " ", "0"))
      '--------------------------------------------------------------------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleArmd.CkitApri(DittaCorrente, oCleArmd.strCkitCodart, dsCkit) Then Me.Close()

      dcCkit.DataSource = dsCkit.Tables("ARTKIT")
      grCkit.DataSource = dcCkit

      ak_riga.Enabled = False

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      Me.Cursor = Cursors.Default

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGCKIT_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcCkit.Dispose()
      dsCkit.Dispose()
    Catch
    End Try
  End Sub
  Public Overridable Sub FRMMGCKIT_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvCkit.NTSNuovo()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Try
      If Not grvCkit.NTSDeleteRigaCorrente(dcCkit, True) Then Return
      oCleArmd.CkitSalva(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvCkit.NTSRipristinaRigaCorrenteBefore(dcCkit, True) Then Return
      oCleArmd.CkitRipristina(dcCkit.Position, dcCkit.Filter)
      grvCkit.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim ctrlTmp As Control = Nothing
    Dim oParam As New CLE__PATB

    Try
      '------------------------------------
      'zoom standard
      ctrlTmp = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      If ctrlTmp.GetType.ToString.IndexOf("NTSGrid") > -1 Then
        SetFastZoom(NTSCStr(CType(CType(ctrlTmp, NTSGrid).DefaultView, NTSGridView).EditingValue), oParam)    'abilito la gestione dello zoom veloce

        If grvCkit.FocusedColumn.Equals(ak_codfigli) Then
          'colonna particolare: codice articolo
          NTSZOOM.strIn = NTSCStr(grvCkit.GetFocusedValue)
          oParam.nMagaz = 0
          oParam.nListino = 0
          NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvCkit.GetFocusedValue) Then grvCkit.SetFocusedValue(NTSZOOM.strIn)
        Else
          '------------------------------------
          'zoom standard di textbox e griglia
          NTSCallStandardZoom()
        End If
      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        NTSCallStandardZoom()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      Stampa(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub
  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    If Not Salva() Then Return
    Me.Close()
  End Sub
#End Region

#Region "EventiGriglia"
  Public Overridable Sub grvCkit_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvCkit.NTSBeforeRowUpdate
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

  Public Overridable Sub grvCkit_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvCkit.NTSFocusedRowChanged
    'blocco le colonne non modificabili
    Dim dtrT As DataRow = Nothing
    Try
      If oCleArmd Is Nothing Then Return

      dtrT = grvCkit.NTSGetCurrentDataRow
      '------------------------------------
      'sono su una nuova riga
      If dtrT Is Nothing Then
        ak_codfigli.Enabled = True
        Return
      End If

      If (NTSCStr(dtrT!ak_codfigli).Trim <> "") And (oCleArmd.bConsentiModificaArticoliKit = False) Then
        ak_codfigli.Enabled = False
      End If

      RiposizionaUnmis()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvCkit_NTSFocusedColumnChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles grvCkit.NTSFocusedColumnChanged
    Try
      RiposizionaUnmis()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function RiposizionaUnmis() As Boolean
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Dim dcTmp As New BindingSource
    Dim strT() As String = Nothing
    Try
      If NTSInFormClosing Then Return True
      If oCleArmd Is Nothing Then Return True
      If dttUm Is Nothing Then Return True

      If grvCkit.NTSGetCurrentDataRow Is Nothing Then
        If Not ak_unmis.ColumnEdit Is Nothing Then CType(ak_unmis.ColumnEdit, NTSRepositoryItemComboBox).DataSource = dttUm
        ak_unmis.Enabled = False
      Else
        GctlSetVisEnab(ak_unmis, False)
      End If

      '--------------------------------------
      'compilo il combo delle unità di misura
      CType(ak_unmis.ColumnEdit, NTSRepositoryItemComboBox).DataSource = dttUm
      If grvCkit.FocusedColumn.Name = "ak_unmis" And grvCkit.GetFocusedRowCellValue(ak_codfigli).ToString <> "" And NTSCStr(grvCkit.GetFocusedRowCellValue(ak_codfigli)).Trim <> "" Then
        grvCkit.liListCmb.Visible = False    'lo nascondo, visto che contiene tutte le unita di misura del db ...
        grCkit.Refresh()
        strTmp = oCleArmd.GetArticoUnMis(NTSCStr(grvCkit.GetFocusedRowCellValue(ak_codfigli)))
        strT = strTmp.Split("§"c)
        dttTmp.Columns.Add("tb_codumis", GetType(String))
        For i As Integer = 0 To strT.Length - 1
          dttTmp.Rows.Add(New Object() {strT(i)})
        Next
        dttTmp.AcceptChanges()
        CType(ak_unmis.ColumnEdit, NTSRepositoryItemComboBox).DataSource = dttTmp
        'attenzione: riagganciando a liListCmb il nuovo datatable viene resettato il valore contenuto nella cella della griglia:
        'memorizzo il valore corrente, disabilito la before/aftercolupdate, associo il datatable e reimposto il valore precedente
        dcTmp.DataSource = dttTmp
        For i As Integer = 0 To dcTmp.List.Count - 1
          If NTSCStr(CType(dcTmp.Current, DataRowView).Row!tb_codumis).ToUpper <> NTSCStr(grvCkit.GetFocusedRowCellValue(ak_unmis)).ToUpper Then
            dcTmp.MoveNext()
          Else
            Exit For
          End If
        Next
        grvCkit.liListCmb.DataSource = dcTmp
        grvCkit.liListCmb.Visible = True
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
#End Region

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer

    Try
      '--------------------------------------------------
      'preparo il motore di stampa
      strCrpe = "{ARTKIT.codditt} = " & CStrSQL(DittaCorrente) & _
        " And {ARTKIT.ak_codart} = " & CStrSQL(oCleArmd.strCkitCodart)
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGARTI", "Reports1", " ", 0, nDestin, "BSMGCOKN.RPT", False, "Promozioni", False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce già il multireport)
      For i = 1 To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class