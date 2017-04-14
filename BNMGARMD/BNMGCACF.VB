Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGCACF
#Region "Variabili"
  Public oCleArmd As CLEMGARMD
  Public oCallParams As CLE__CLDP
  Public dsCacf As New DataSet
  Public dcCacf As New BindingSource()
  Public dttCacf As DataTable = Nothing
  Public dtrCacf() As DataRow = Nothing
  Private components As System.ComponentModel.IContainer

  Public bServer As Boolean

  Public WithEvents grCacf As NTSInformatica.NTSGrid
  Public WithEvents grvCacf As NTSInformatica.NTSGridView
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
  Public WithEvents caf_codart As NTSInformatica.NTSGridColumn
  Public WithEvents caf_conto As NTSInformatica.NTSGridColumn
  Public WithEvents xx_conto As NTSInformatica.NTSGridColumn
  Public WithEvents caf_desnote As NTSInformatica.NTSGridColumn
  Public WithEvents caf_rrfence As NTSInformatica.NTSGridColumn
  Public WithEvents caf_fcorrlt As NTSInformatica.NTSGridColumn
  Public WithEvents caf_ultagg As NTSInformatica.NTSGridColumn
  Public WithEvents caf_codarfo As NTSInformatica.NTSGridColumn
  Public WithEvents caf_opnome As NTSInformatica.NTSGridColumn
  Public WithEvents caf_percrip As NTSInformatica.NTSGridColumn
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbCheckEAN13 As NTSInformatica.NTSBarButtonItem
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
    'creo e attivo l'entity e inizializzo la funzione che dovrà  rilevare gli eventi dall'ENTITY
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
    oCleArmd.strCacfCodart = oCallParams.strPar2

    Return True
  End Function
  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGCACF))
    Me.grCacf = New NTSInformatica.NTSGrid
    Me.grvCacf = New NTSInformatica.NTSGridView
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.caf_codart = New NTSInformatica.NTSGridColumn
    Me.caf_conto = New NTSInformatica.NTSGridColumn
    Me.xx_conto = New NTSInformatica.NTSGridColumn
    Me.caf_codarfo = New NTSInformatica.NTSGridColumn
    Me.caf_desnote = New NTSInformatica.NTSGridColumn
    Me.caf_rrfence = New NTSInformatica.NTSGridColumn
    Me.caf_fcorrlt = New NTSInformatica.NTSGridColumn
    Me.caf_ultagg = New NTSInformatica.NTSGridColumn
    Me.caf_opnome = New NTSInformatica.NTSGridColumn
    Me.caf_percrip = New NTSInformatica.NTSGridColumn
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbCheckEAN13 = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    CType(Me.grCacf, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvCacf, System.ComponentModel.ISupportInitialize).BeginInit()
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
    '
    'frmAuto
    '
    Me.frmAuto.Appearance.BackColor = System.Drawing.Color.Black
    Me.frmAuto.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmAuto.Appearance.Options.UseBackColor = True
    Me.frmAuto.Appearance.Options.UseImage = True
    '
    'grCacf
    '
    Me.grCacf.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grCacf.EmbeddedNavigator.Name = ""
    Me.grCacf.Location = New System.Drawing.Point(0, 30)
    Me.grCacf.MainView = Me.grvCacf
    Me.grCacf.Name = "grCacf"
    Me.grCacf.Size = New System.Drawing.Size(667, 336)
    Me.grCacf.TabIndex = 0
    Me.grCacf.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvCacf})
    '
    'grvCacf
    '
    Me.grvCacf.ActiveFilterEnabled = False
    Me.grvCacf.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.codditt, Me.caf_codart, Me.caf_conto, Me.xx_conto, Me.caf_codarfo, Me.caf_desnote, Me.caf_rrfence, Me.caf_fcorrlt, Me.caf_ultagg, Me.caf_opnome, Me.caf_percrip})
    Me.grvCacf.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvCacf.Enabled = True
    Me.grvCacf.GridControl = Me.grCacf
    Me.grvCacf.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvCacf.MinRowHeight = 14
    Me.grvCacf.Name = "grvCacf"
    Me.grvCacf.NTSAllowDelete = True
    Me.grvCacf.NTSAllowInsert = True
    Me.grvCacf.NTSAllowUpdate = True
    Me.grvCacf.NTSMenuContext = Nothing
    Me.grvCacf.OptionsCustomization.AllowRowSizing = True
    Me.grvCacf.OptionsFilter.AllowFilterEditor = False
    Me.grvCacf.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvCacf.OptionsNavigation.UseTabKey = False
    Me.grvCacf.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvCacf.OptionsView.ColumnAutoWidth = False
    Me.grvCacf.OptionsView.EnableAppearanceEvenRow = True
    Me.grvCacf.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvCacf.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvCacf.OptionsView.ShowGroupPanel = False
    Me.grvCacf.RowHeight = 16
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
    Me.codditt.NTSRepositoryItemMemo = Nothing
    Me.codditt.NTSRepositoryItemText = Nothing
    Me.codditt.OptionsColumn.AllowEdit = False
    Me.codditt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.codditt.OptionsColumn.ReadOnly = True
    Me.codditt.OptionsFilter.AllowFilter = False
    '
    'caf_codart
    '
    Me.caf_codart.AppearanceCell.Options.UseBackColor = True
    Me.caf_codart.AppearanceCell.Options.UseTextOptions = True
    Me.caf_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.caf_codart.Caption = "Codice art."
    Me.caf_codart.Enabled = False
    Me.caf_codart.FieldName = "caf_codart"
    Me.caf_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.caf_codart.Name = "caf_codart"
    Me.caf_codart.NTSRepositoryComboBox = Nothing
    Me.caf_codart.NTSRepositoryItemCheck = Nothing
    Me.caf_codart.NTSRepositoryItemMemo = Nothing
    Me.caf_codart.NTSRepositoryItemText = Nothing
    Me.caf_codart.OptionsColumn.AllowEdit = False
    Me.caf_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.caf_codart.OptionsColumn.ReadOnly = True
    Me.caf_codart.OptionsFilter.AllowFilter = False
    '
    'caf_conto
    '
    Me.caf_conto.AppearanceCell.Options.UseBackColor = True
    Me.caf_conto.AppearanceCell.Options.UseTextOptions = True
    Me.caf_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.caf_conto.Caption = "Cliente/fornitore"
    Me.caf_conto.Enabled = True
    Me.caf_conto.FieldName = "caf_conto"
    Me.caf_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.caf_conto.Name = "caf_conto"
    Me.caf_conto.NTSRepositoryComboBox = Nothing
    Me.caf_conto.NTSRepositoryItemCheck = Nothing
    Me.caf_conto.NTSRepositoryItemMemo = Nothing
    Me.caf_conto.NTSRepositoryItemText = Nothing
    Me.caf_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.caf_conto.OptionsFilter.AllowFilter = False
    Me.caf_conto.Visible = True
    Me.caf_conto.VisibleIndex = 0
    '
    'xx_conto
    '
    Me.xx_conto.AppearanceCell.Options.UseBackColor = True
    Me.xx_conto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_conto.Caption = "Descrizione conto"
    Me.xx_conto.Enabled = False
    Me.xx_conto.FieldName = "xx_conto"
    Me.xx_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_conto.Name = "xx_conto"
    Me.xx_conto.NTSRepositoryComboBox = Nothing
    Me.xx_conto.NTSRepositoryItemCheck = Nothing
    Me.xx_conto.NTSRepositoryItemMemo = Nothing
    Me.xx_conto.NTSRepositoryItemText = Nothing
    Me.xx_conto.OptionsColumn.AllowEdit = False
    Me.xx_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_conto.OptionsColumn.ReadOnly = True
    Me.xx_conto.OptionsFilter.AllowFilter = False
    Me.xx_conto.Visible = True
    Me.xx_conto.VisibleIndex = 1
    '
    'caf_codarfo
    '
    Me.caf_codarfo.AppearanceCell.Options.UseBackColor = True
    Me.caf_codarfo.AppearanceCell.Options.UseTextOptions = True
    Me.caf_codarfo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.caf_codarfo.Caption = "Codice articolo"
    Me.caf_codarfo.Enabled = True
    Me.caf_codarfo.FieldName = "caf_codarfo"
    Me.caf_codarfo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.caf_codarfo.Name = "caf_codarfo"
    Me.caf_codarfo.NTSRepositoryComboBox = Nothing
    Me.caf_codarfo.NTSRepositoryItemCheck = Nothing
    Me.caf_codarfo.NTSRepositoryItemMemo = Nothing
    Me.caf_codarfo.NTSRepositoryItemText = Nothing
    Me.caf_codarfo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.caf_codarfo.OptionsFilter.AllowFilter = False
    Me.caf_codarfo.Visible = True
    Me.caf_codarfo.VisibleIndex = 2
    '
    'caf_desnote
    '
    Me.caf_desnote.AppearanceCell.Options.UseBackColor = True
    Me.caf_desnote.AppearanceCell.Options.UseTextOptions = True
    Me.caf_desnote.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.caf_desnote.Caption = "Note"
    Me.caf_desnote.Enabled = True
    Me.caf_desnote.FieldName = "caf_desnote"
    Me.caf_desnote.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.caf_desnote.Name = "caf_desnote"
    Me.caf_desnote.NTSRepositoryComboBox = Nothing
    Me.caf_desnote.NTSRepositoryItemCheck = Nothing
    Me.caf_desnote.NTSRepositoryItemMemo = Nothing
    Me.caf_desnote.NTSRepositoryItemText = Nothing
    Me.caf_desnote.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.caf_desnote.OptionsFilter.AllowFilter = False
    Me.caf_desnote.Visible = True
    Me.caf_desnote.VisibleIndex = 3
    '
    'caf_rrfence
    '
    Me.caf_rrfence.AppearanceCell.Options.UseBackColor = True
    Me.caf_rrfence.AppearanceCell.Options.UseTextOptions = True
    Me.caf_rrfence.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.caf_rrfence.Caption = "R.R. Fence"
    Me.caf_rrfence.Enabled = True
    Me.caf_rrfence.FieldName = "caf_rrfence"
    Me.caf_rrfence.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.caf_rrfence.Name = "caf_rrfence"
    Me.caf_rrfence.NTSRepositoryComboBox = Nothing
    Me.caf_rrfence.NTSRepositoryItemCheck = Nothing
    Me.caf_rrfence.NTSRepositoryItemMemo = Nothing
    Me.caf_rrfence.NTSRepositoryItemText = Nothing
    Me.caf_rrfence.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.caf_rrfence.OptionsFilter.AllowFilter = False
    '
    'caf_fcorrlt
    '
    Me.caf_fcorrlt.AppearanceCell.Options.UseBackColor = True
    Me.caf_fcorrlt.AppearanceCell.Options.UseTextOptions = True
    Me.caf_fcorrlt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.caf_fcorrlt.Caption = "Fattore correzione"
    Me.caf_fcorrlt.Enabled = True
    Me.caf_fcorrlt.FieldName = "caf_fcorrlt"
    Me.caf_fcorrlt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.caf_fcorrlt.Name = "caf_fcorrlt"
    Me.caf_fcorrlt.NTSRepositoryComboBox = Nothing
    Me.caf_fcorrlt.NTSRepositoryItemCheck = Nothing
    Me.caf_fcorrlt.NTSRepositoryItemMemo = Nothing
    Me.caf_fcorrlt.NTSRepositoryItemText = Nothing
    Me.caf_fcorrlt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.caf_fcorrlt.OptionsFilter.AllowFilter = False
    '
    'caf_ultagg
    '
    Me.caf_ultagg.AppearanceCell.Options.UseBackColor = True
    Me.caf_ultagg.AppearanceCell.Options.UseTextOptions = True
    Me.caf_ultagg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.caf_ultagg.Caption = "Data ult agg (nascosto)"
    Me.caf_ultagg.Enabled = False
    Me.caf_ultagg.FieldName = "caf_ultagg"
    Me.caf_ultagg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.caf_ultagg.Name = "caf_ultagg"
    Me.caf_ultagg.NTSRepositoryComboBox = Nothing
    Me.caf_ultagg.NTSRepositoryItemCheck = Nothing
    Me.caf_ultagg.NTSRepositoryItemMemo = Nothing
    Me.caf_ultagg.NTSRepositoryItemText = Nothing
    Me.caf_ultagg.OptionsColumn.AllowEdit = False
    Me.caf_ultagg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.caf_ultagg.OptionsColumn.ReadOnly = True
    Me.caf_ultagg.OptionsFilter.AllowFilter = False
    '
    'caf_opnome
    '
    Me.caf_opnome.AppearanceCell.Options.UseBackColor = True
    Me.caf_opnome.AppearanceCell.Options.UseTextOptions = True
    Me.caf_opnome.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.caf_opnome.Caption = "Operatore (nascosto)"
    Me.caf_opnome.Enabled = False
    Me.caf_opnome.FieldName = "caf_opnome"
    Me.caf_opnome.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.caf_opnome.Name = "caf_opnome"
    Me.caf_opnome.NTSRepositoryComboBox = Nothing
    Me.caf_opnome.NTSRepositoryItemCheck = Nothing
    Me.caf_opnome.NTSRepositoryItemMemo = Nothing
    Me.caf_opnome.NTSRepositoryItemText = Nothing
    Me.caf_opnome.OptionsColumn.AllowEdit = False
    Me.caf_opnome.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.caf_opnome.OptionsColumn.ReadOnly = True
    Me.caf_opnome.OptionsFilter.AllowFilter = False
    '
    'caf_percrip
    '
    Me.caf_percrip.AppearanceCell.Options.UseBackColor = True
    Me.caf_percrip.AppearanceCell.Options.UseTextOptions = True
    Me.caf_percrip.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.caf_percrip.Caption = "% di ripartizione qtà"
    Me.caf_percrip.Enabled = True
    Me.caf_percrip.FieldName = "caf_percrip"
    Me.caf_percrip.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.caf_percrip.Name = "caf_percrip"
    Me.caf_percrip.NTSRepositoryComboBox = Nothing
    Me.caf_percrip.NTSRepositoryItemCheck = Nothing
    Me.caf_percrip.NTSRepositoryItemMemo = Nothing
    Me.caf_percrip.NTSRepositoryItemText = Nothing
    Me.caf_percrip.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.caf_percrip.OptionsFilter.AllowFilter = False
    Me.caf_percrip.Visible = True
    Me.caf_percrip.VisibleIndex = 4
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbCheckEAN13})
    Me.NtsBarManager1.MaxItemId = 18
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbStrumenti.Id = 16
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCheckEAN13)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbCheckEAN13
    '
    Me.tlbCheckEAN13.Caption = "Controlla Ean13"
    Me.tlbCheckEAN13.Id = 17
    Me.tlbCheckEAN13.Name = "tlbCheckEAN13"
    Me.tlbCheckEAN13.Visible = True
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
    'FRMMGCACF
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(667, 366)
    Me.Controls.Add(Me.grCacf)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.HelpContext = ""
    Me.Name = "FRMMGCACF"
    Me.NTSLastControlFocussed = Me.grCacf
    Me.Text = "CORRISPONDENZA CODICI ARTICOLI C/F"
    CType(Me.grCacf, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvCacf, System.ComponentModel.ISupportInitialize).EndInit()
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
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        '  'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvCacf.NTSSetParam(oMenu, oApp.Tr(Me, 128828970324947753, "Corrispondenza codici articoli C/F"))
      codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828970338697841, "Cod. ditta"), 12, False)
      caf_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828970352760431, "Codice art."), CLN__STD.CodartMaxLen, False)
      caf_conto.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128828970366823021, "Cliente/fornitore"), tabanagra)
      xx_conto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828970382448121, "Descrizione conto"), 0, True)
      caf_codarfo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828970396510711, "Codice articolo"), 50, True)
      caf_desnote.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828970430417178, "Note"), 50, True)
      caf_rrfence.NTSSetParamNUM(oMenu, "R.R. Fence", "0", 3, 0, 999)
      caf_fcorrlt.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128788388949737984, "Fattore correz. L.T."), "#,##0.000000000", 10, 0, NTSCDec(9999.99999))
      caf_ultagg.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128828970492448825, "Data ult agg (nascosto)"), False)
      caf_opnome.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828970507136419, "Operatore (nascosto)"), 20, False)
      caf_percrip.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828970527605300, "% di ripartizione qtà"), "#,##0.00", 15, 0, 100)

      caf_conto.NTSSetParamZoom("ZOOMANAGRACF")

      caf_codarfo.NTSForzaVisZoom = True
      caf_codarfo.NTSSetParamZoom("ZOOMARTICO")

      caf_conto.NTSSetRichiesto()

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
      dRes = grvCacf.NTSSalvaRigaCorrente(dcCacf, oCleArmd.CacfRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleArmd.CacfSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleArmd.CacfRipristina(dcCacf.Position, dcCacf.Filter)
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
  Public Overridable Sub FRMMGCACF_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------------------------------
      'Legge opzioni di registro
      oCleArmd.bBDPKExt = False
      If Not oCleArmd.strPrgParent = "BNMGARTV" Then
        oCleArmd.bBDPKExt = CBool(NTSCInt(oMenu.GetSettingBus("BSMGARTI", "Opzioni", ".", "BD_PKCodarfoExt", "0", " ", "0"))) 'NON DOCUMENTARE
      End If

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleArmd.CacfApri(DittaCorrente, oCleArmd.strCacfCodart, dsCacf) Then Me.Close()

      dcCacf.DataSource = dsCacf.Tables("CODARFO")
      grCacf.DataSource = dcCacf

      caf_conto.Enabled = False

      If oCleArmd.bBDPKExt Then
        caf_desnote.Caption = "Barcode"
        tlbCheckEAN13.Visible = True
      Else
        tlbCheckEAN13.Visible = False
      End If

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
  Public Overridable Sub FRMMGCACF_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcCacf.Dispose()
      dsCacf.Dispose()
    Catch
    End Try
  End Sub
  Public Overridable Sub FRMMGCACF_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvCacf.NTSNuovo()
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
      If grvCacf.NTSGetCurrentDataRow Is Nothing Then Return
      If Not oCleArmd.CacfTestPreCancella(grvCacf.NTSGetCurrentDataRow) Then Return
      If Not grvCacf.NTSDeleteRigaCorrente(dcCacf, True) Then Return
      oCleArmd.CacfSalva(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvCacf.NTSRipristinaRigaCorrenteBefore(dcCacf, True) Then Return
      oCleArmd.CacfRipristina(dcCacf.Position, dcCacf.Filter)
      grvCacf.NTSRipristinaRigaCorrenteAfter()
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
      '------------------------------------------------
      'questa funzione viene sempre scatenata quando premo F5 o ALT+F2 o ALT+F3 sulla griglia
      'zoom standard
      ctrlTmp = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return

      If ctrlTmp.GetType.ToString.IndexOf("NTSGrid") > -1 Then
        SetFastZoom(NTSCStr(CType(CType(ctrlTmp, NTSGrid).DefaultView, NTSGridView).EditingValue), oParam)

        If grCacf.Focused And grvCacf.FocusedColumn.Name.ToLower = "caf_conto" Then
          'zoom su anagra
          SetFastZoom(NTSCStr(CType(CType(ctrlTmp, NTSGrid).DefaultView, NTSGridView).EditingValue), oParam)
          oParam.bVisGriglia = True
          oParam.strTipo = "F"
          oParam.bTipoProposto = True
          NTSZOOM.strIn = NTSCStr(grvCacf.EditingValue)
          NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvCacf.EditingValue) Then grvCacf.SetFocusedValue(NTSZOOM.strIn)
          Return
        End If
      End If

      '------------------------------------
      'zoom standard di textbox e griglia
      NTSCallStandardZoom()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCheckEAN13_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCheckEAN13.ItemClick
    Dim strDesnote As String
    Try
      If Not grvCacf.NTSGetCurrentDataRow() Is Nothing Then
        strDesnote = NTSCStr(grvCacf.NTSGetCurrentDataRow()!caf_desnote)
      Else
        Exit Sub
      End If

      If oCleArmd.CheckEAN13(strDesnote) Then
        dsCacf.Tables("CODARFO").Rows(dcCacf.Position)!caf_desnote = strDesnote
      Else
        Exit Sub
      End If

      If Not Salva() Then Exit Sub

      oCleArmd.CacfInsertBarcode(strDesnote)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
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
  Public Overridable Sub grvCacf_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvCacf.NTSBeforeRowUpdate
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

  Public Overridable Sub grvCacf_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvCacf.NTSFocusedRowChanged
    'blocco le colonne non modificabili
    Dim dtrT As DataRow = Nothing
    Try
      If oCleArmd Is Nothing Then Return

      dtrT = grvCacf.NTSGetCurrentDataRow
      '------------------------------------
      'sono su una nuova riga
      If dtrT Is Nothing Then
        caf_conto.Enabled = True
        Return
      End If

      If NTSCInt(dtrT!caf_conto) <> 0 Then
        caf_conto.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

End Class