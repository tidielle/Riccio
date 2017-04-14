Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGDEPR
  Public oCleArmd As CLEMGARMD
  Public oCallParams As CLE__CLDP

  Public bOnLoading As Boolean = False
  Public bGescomm As Boolean = False
  Public bGeslotti As Boolean = False
  Public bGesubic As Boolean = False

  Public dsDepr As New DataSet
  Public dcDepr As New BindingSource()

#Region "Inizializzazione"
  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, _
    Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    '----------------------------------------------------------------------------------------------------------------
    oMenu = Menu
    oApp = oMenu.App
    oCallParams = Param
    '----------------------------------------------------------------------------------------------------------------
    If Ditta <> "" Then DittaCorrente = Ditta Else DittaCorrente = oApp.Ditta
    '----------------------------------------------------------------------------------------------------------------
    Me.GctlTipoDoc = ""
    '----------------------------------------------------------------------------------------------------------------
    InitializeComponent()
    Me.MinimumSize = Me.Size
    '----------------------------------------------------------------------------------------------------------------
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGARMD", "BEMGARMD", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128550728307822408, "ERRORE in fase di creazione Entity:" & vbCrLf & strErr))
      Return False
    End If
    oCleArmd = CType(oTmp, CLEMGARMD)
    '----------------------------------------------------------------------------------------------------------------
    bRemoting = Menu.Remoting("BNMGARMD", strRemoteServer, strRemotePort)
    AddHandler oCleArmd.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleArmd.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    '----------------------------------------------------------------------------------------------------------------
    oCleArmd.strPrgParent = oCallParams.strPar1
    oCleArmd.strHlapCodart = oCallParams.strPar2
    oCleArmd.dHlapFase = oCallParams.dPar1
    '----------------------------------------------------------------------------------------------------------------
    Return True
  End Function

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGDEPR))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbCommessa = New NTSInformatica.NTSBarButtonItem
    Me.tlbUbicazioni = New NTSInformatica.NTSBarButtonItem
    Me.tlbLotti = New NTSInformatica.NTSBarButtonItem
    Me.tlbDettTCO = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.lbXx_fase = New NTSInformatica.NTSLabel
    Me.lbXx_magaz = New NTSInformatica.NTSLabel
    Me.lbXx_codart = New NTSInformatica.NTSLabel
    Me.lbAr_fase = New NTSInformatica.NTSLabel
    Me.lbAr_magaz = New NTSInformatica.NTSLabel
    Me.lbAr_codart = New NTSInformatica.NTSLabel
    Me.lbFase = New NTSInformatica.NTSLabel
    Me.lbMagaz = New NTSInformatica.NTSLabel
    Me.lbCodart = New NTSInformatica.NTSLabel
    Me.grDepr = New NTSInformatica.NTSGrid
    Me.grvDepr = New NTSInformatica.NTSGridView
    Me.lp_commeca = New NTSInformatica.NTSGridColumn
    Me.xx_commeca = New NTSInformatica.NTSGridColumn
    Me.lp_lotto = New NTSInformatica.NTSGridColumn
    Me.xx_lotto = New NTSInformatica.NTSGridColumn
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
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.grDepr, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvDepr, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbCommessa, Me.tlbUbicazioni, Me.tlbLotti, Me.tlbEsci, Me.tlbDettTCO})
    Me.NtsBarManager1.MaxItemId = 29
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCommessa), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUbicazioni), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbLotti), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDettTCO, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci, True)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbCommessa
    '
    Me.tlbCommessa.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbCommessa.Caption = "Commessa"
    Me.tlbCommessa.GlyphPath = ""
    Me.tlbCommessa.Id = 24
    Me.tlbCommessa.Name = "tlbCommessa"
    Me.tlbCommessa.Visible = True
    '
    'tlbUbicazioni
    '
    Me.tlbUbicazioni.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbUbicazioni.Caption = "Ubicazioni"
    Me.tlbUbicazioni.GlyphPath = ""
    Me.tlbUbicazioni.Id = 25
    Me.tlbUbicazioni.Name = "tlbUbicazioni"
    Me.tlbUbicazioni.Visible = True
    '
    'tlbLotti
    '
    Me.tlbLotti.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbLotti.Caption = "Lotti"
    Me.tlbLotti.GlyphPath = ""
    Me.tlbLotti.Id = 26
    Me.tlbLotti.Name = "tlbLotti"
    Me.tlbLotti.Visible = True
    '
    'tlbDettTCO
    '
    Me.tlbDettTCO.Caption = "Dettaglio Taglie e Colori"
    Me.tlbDettTCO.Glyph = CType(resources.GetObject("tlbDettTCO.Glyph"), System.Drawing.Image)
    Me.tlbDettTCO.GlyphPath = ""
    Me.tlbDettTCO.Id = 28
    Me.tlbDettTCO.Name = "tlbDettTCO"
    Me.tlbDettTCO.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 27
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.lbXx_fase)
    Me.pnTop.Controls.Add(Me.lbXx_magaz)
    Me.pnTop.Controls.Add(Me.lbXx_codart)
    Me.pnTop.Controls.Add(Me.lbAr_fase)
    Me.pnTop.Controls.Add(Me.lbAr_magaz)
    Me.pnTop.Controls.Add(Me.lbAr_codart)
    Me.pnTop.Controls.Add(Me.lbFase)
    Me.pnTop.Controls.Add(Me.lbMagaz)
    Me.pnTop.Controls.Add(Me.lbCodart)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(604, 74)
    Me.pnTop.TabIndex = 15
    '
    'lbXx_fase
    '
    Me.lbXx_fase.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_fase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_fase.Location = New System.Drawing.Point(143, 50)
    Me.lbXx_fase.Name = "lbXx_fase"
    Me.lbXx_fase.NTSDbField = ""
    Me.lbXx_fase.Size = New System.Drawing.Size(453, 20)
    Me.lbXx_fase.TabIndex = 10
    Me.lbXx_fase.Tooltip = ""
    Me.lbXx_fase.UseMnemonic = False
    '
    'lbXx_magaz
    '
    Me.lbXx_magaz.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_magaz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_magaz.Location = New System.Drawing.Point(143, 27)
    Me.lbXx_magaz.Name = "lbXx_magaz"
    Me.lbXx_magaz.NTSDbField = ""
    Me.lbXx_magaz.Size = New System.Drawing.Size(453, 20)
    Me.lbXx_magaz.TabIndex = 9
    Me.lbXx_magaz.Tooltip = ""
    Me.lbXx_magaz.UseMnemonic = False
    '
    'lbXx_codart
    '
    Me.lbXx_codart.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codart.Location = New System.Drawing.Point(266, 4)
    Me.lbXx_codart.Name = "lbXx_codart"
    Me.lbXx_codart.NTSDbField = ""
    Me.lbXx_codart.Size = New System.Drawing.Size(330, 20)
    Me.lbXx_codart.TabIndex = 8
    Me.lbXx_codart.Tooltip = ""
    Me.lbXx_codart.UseMnemonic = False
    '
    'lbAr_fase
    '
    Me.lbAr_fase.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_fase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbAr_fase.Location = New System.Drawing.Point(82, 49)
    Me.lbAr_fase.Name = "lbAr_fase"
    Me.lbAr_fase.NTSDbField = ""
    Me.lbAr_fase.Size = New System.Drawing.Size(55, 20)
    Me.lbAr_fase.TabIndex = 7
    Me.lbAr_fase.TextAlign = System.Drawing.ContentAlignment.TopRight
    Me.lbAr_fase.Tooltip = ""
    Me.lbAr_fase.UseMnemonic = False
    '
    'lbAr_magaz
    '
    Me.lbAr_magaz.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_magaz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbAr_magaz.Location = New System.Drawing.Point(82, 27)
    Me.lbAr_magaz.Name = "lbAr_magaz"
    Me.lbAr_magaz.NTSDbField = ""
    Me.lbAr_magaz.Size = New System.Drawing.Size(55, 20)
    Me.lbAr_magaz.TabIndex = 6
    Me.lbAr_magaz.TextAlign = System.Drawing.ContentAlignment.TopRight
    Me.lbAr_magaz.Tooltip = ""
    Me.lbAr_magaz.UseMnemonic = False
    '
    'lbAr_codart
    '
    Me.lbAr_codart.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_codart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbAr_codart.Location = New System.Drawing.Point(82, 5)
    Me.lbAr_codart.Name = "lbAr_codart"
    Me.lbAr_codart.NTSDbField = ""
    Me.lbAr_codart.Size = New System.Drawing.Size(178, 20)
    Me.lbAr_codart.TabIndex = 5
    Me.lbAr_codart.Tooltip = ""
    Me.lbAr_codart.UseMnemonic = False
    '
    'lbFase
    '
    Me.lbFase.AutoSize = True
    Me.lbFase.BackColor = System.Drawing.Color.Transparent
    Me.lbFase.Location = New System.Drawing.Point(12, 52)
    Me.lbFase.Name = "lbFase"
    Me.lbFase.NTSDbField = ""
    Me.lbFase.Size = New System.Drawing.Size(30, 13)
    Me.lbFase.TabIndex = 4
    Me.lbFase.Text = "Fase"
    Me.lbFase.Tooltip = ""
    Me.lbFase.UseMnemonic = False
    '
    'lbMagaz
    '
    Me.lbMagaz.AutoSize = True
    Me.lbMagaz.BackColor = System.Drawing.Color.Transparent
    Me.lbMagaz.Location = New System.Drawing.Point(12, 30)
    Me.lbMagaz.Name = "lbMagaz"
    Me.lbMagaz.NTSDbField = ""
    Me.lbMagaz.Size = New System.Drawing.Size(57, 13)
    Me.lbMagaz.TabIndex = 3
    Me.lbMagaz.Text = "Magazzino"
    Me.lbMagaz.Tooltip = ""
    Me.lbMagaz.UseMnemonic = False
    '
    'lbCodart
    '
    Me.lbCodart.AutoSize = True
    Me.lbCodart.BackColor = System.Drawing.Color.Transparent
    Me.lbCodart.Location = New System.Drawing.Point(12, 8)
    Me.lbCodart.Name = "lbCodart"
    Me.lbCodart.NTSDbField = ""
    Me.lbCodart.Size = New System.Drawing.Size(43, 13)
    Me.lbCodart.TabIndex = 2
    Me.lbCodart.Text = "Articolo"
    Me.lbCodart.Tooltip = ""
    Me.lbCodart.UseMnemonic = False
    '
    'grDepr
    '
    Me.grDepr.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grDepr.EmbeddedNavigator.Name = ""
    Me.grDepr.Location = New System.Drawing.Point(0, 104)
    Me.grDepr.MainView = Me.grvDepr
    Me.grDepr.Name = "grDepr"
    Me.grDepr.Size = New System.Drawing.Size(604, 222)
    Me.grDepr.TabIndex = 16
    Me.grDepr.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDepr})
    '
    'grvDepr
    '
    Me.grvDepr.ActiveFilterEnabled = False
    Me.grvDepr.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.lp_commeca, Me.xx_commeca, Me.lp_lotto, Me.xx_lotto, Me.lp_ubicaz, Me.lp_esist, Me.lp_qtalif, Me.lp_vqtalif, Me.lp_giaini, Me.lp_vgiaini, Me.lp_prenot, Me.lp_vprenot, Me.lp_ordin, Me.lp_vordin, Me.lp_impeg, Me.lp_vimpeg})
    Me.grvDepr.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvDepr.Enabled = True
    Me.grvDepr.GridControl = Me.grDepr
    Me.grvDepr.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvDepr.MinRowHeight = 14
    Me.grvDepr.Name = "grvDepr"
    Me.grvDepr.NTSAllowDelete = True
    Me.grvDepr.NTSAllowInsert = True
    Me.grvDepr.NTSAllowUpdate = True
    Me.grvDepr.NTSMenuContext = Nothing
    Me.grvDepr.OptionsCustomization.AllowRowSizing = True
    Me.grvDepr.OptionsFilter.AllowFilterEditor = False
    Me.grvDepr.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvDepr.OptionsNavigation.UseTabKey = False
    Me.grvDepr.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvDepr.OptionsView.ColumnAutoWidth = False
    Me.grvDepr.OptionsView.EnableAppearanceEvenRow = True
    Me.grvDepr.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvDepr.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvDepr.OptionsView.ShowGroupPanel = False
    Me.grvDepr.RowHeight = 16
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
    Me.lp_commeca.NTSRepositoryItemMemo = Nothing
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
    Me.xx_commeca.Enabled = True
    Me.xx_commeca.FieldName = "xx_commeca"
    Me.xx_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_commeca.Name = "xx_commeca"
    Me.xx_commeca.NTSRepositoryComboBox = Nothing
    Me.xx_commeca.NTSRepositoryItemCheck = Nothing
    Me.xx_commeca.NTSRepositoryItemMemo = Nothing
    Me.xx_commeca.NTSRepositoryItemText = Nothing
    Me.xx_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_commeca.OptionsFilter.AllowFilter = False
    Me.xx_commeca.Visible = True
    Me.xx_commeca.VisibleIndex = 1
    '
    'lp_lotto
    '
    Me.lp_lotto.AppearanceCell.Options.UseBackColor = True
    Me.lp_lotto.AppearanceCell.Options.UseTextOptions = True
    Me.lp_lotto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lp_lotto.Caption = "Lotto"
    Me.lp_lotto.Enabled = True
    Me.lp_lotto.FieldName = "lp_lotto"
    Me.lp_lotto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lp_lotto.Name = "lp_lotto"
    Me.lp_lotto.NTSRepositoryComboBox = Nothing
    Me.lp_lotto.NTSRepositoryItemCheck = Nothing
    Me.lp_lotto.NTSRepositoryItemMemo = Nothing
    Me.lp_lotto.NTSRepositoryItemText = Nothing
    Me.lp_lotto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lp_lotto.OptionsFilter.AllowFilter = False
    Me.lp_lotto.Visible = True
    Me.lp_lotto.VisibleIndex = 2
    '
    'xx_lotto
    '
    Me.xx_lotto.AppearanceCell.Options.UseBackColor = True
    Me.xx_lotto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_lotto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_lotto.Caption = "Descr. lotto"
    Me.xx_lotto.Enabled = True
    Me.xx_lotto.FieldName = "xx_lotto"
    Me.xx_lotto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_lotto.Name = "xx_lotto"
    Me.xx_lotto.NTSRepositoryComboBox = Nothing
    Me.xx_lotto.NTSRepositoryItemCheck = Nothing
    Me.xx_lotto.NTSRepositoryItemMemo = Nothing
    Me.xx_lotto.NTSRepositoryItemText = Nothing
    Me.xx_lotto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_lotto.OptionsFilter.AllowFilter = False
    Me.xx_lotto.Visible = True
    Me.xx_lotto.VisibleIndex = 3
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
    Me.lp_ubicaz.NTSRepositoryItemMemo = Nothing
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
    Me.lp_esist.NTSRepositoryItemMemo = Nothing
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
    Me.lp_qtalif.Caption = "Qtà totale carichi"
    Me.lp_qtalif.Enabled = True
    Me.lp_qtalif.FieldName = "lp_qtalif"
    Me.lp_qtalif.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lp_qtalif.Name = "lp_qtalif"
    Me.lp_qtalif.NTSRepositoryComboBox = Nothing
    Me.lp_qtalif.NTSRepositoryItemCheck = Nothing
    Me.lp_qtalif.NTSRepositoryItemMemo = Nothing
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
    Me.lp_vqtalif.NTSRepositoryItemMemo = Nothing
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
    Me.lp_giaini.NTSRepositoryItemMemo = Nothing
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
    Me.lp_vgiaini.Caption = "Valore giac. iniziale"
    Me.lp_vgiaini.Enabled = True
    Me.lp_vgiaini.FieldName = "lp_vgiaini"
    Me.lp_vgiaini.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lp_vgiaini.Name = "lp_vgiaini"
    Me.lp_vgiaini.NTSRepositoryComboBox = Nothing
    Me.lp_vgiaini.NTSRepositoryItemCheck = Nothing
    Me.lp_vgiaini.NTSRepositoryItemMemo = Nothing
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
    Me.lp_prenot.NTSRepositoryItemMemo = Nothing
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
    Me.lp_vprenot.NTSRepositoryItemMemo = Nothing
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
    Me.lp_ordin.NTSRepositoryItemMemo = Nothing
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
    Me.lp_vordin.NTSRepositoryItemMemo = Nothing
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
    Me.lp_impeg.NTSRepositoryItemMemo = Nothing
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
    Me.lp_vimpeg.NTSRepositoryItemMemo = Nothing
    Me.lp_vimpeg.NTSRepositoryItemText = Nothing
    Me.lp_vimpeg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lp_vimpeg.OptionsFilter.AllowFilter = False
    Me.lp_vimpeg.Visible = True
    Me.lp_vimpeg.VisibleIndex = 15
    '
    'FRMMGDEPR
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(604, 326)
    Me.Controls.Add(Me.grDepr)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.MinimizeBox = False
    Me.Name = "FRMMGDEPR"
    Me.Text = "DETTAGLIO PROGRESSIVI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.grDepr, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvDepr, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, False)
    '----------------------------------------------------------------------------------------------------------------
    Try
      '--------------------------------------------------------------------------------------------------------------
      Try
        tlbDettTCO.GlyphPath = (oApp.ChildImageDir & "\tc.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
      End Try
      tlbMain.NTSSetToolTip()
      '--------------------------------------------------------------------------------------------------------------
      grvDepr.NTSSetParam(oMenu, oApp.Tr(Me, 128828971526205441, "Griglia Dettaglio Progressivi"))
      lp_commeca.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128828971611674738, "Commessa"), tabcommess)
      xx_commeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828971624799822, "Descrizione commessa"), 0, True)
      lp_lotto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130991381852735324, "Lotto"), "0", 9, 0, 999999999)
      xx_lotto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130991382169784452, "Descrizione lotto"), 0, True)
      lp_ubicaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130991382440534665, "Ubicazione"), 0, True)
      lp_esist.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130991382656851627, "Esistenza"), oApp.FormatQta, 15)
      lp_qtalif.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130991382974808568, "Quantità totale scarichi"), oApp.FormatQta, 15)
      lp_vqtalif.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130991383113508843, "Valore scarichi"), oApp.FormatImporti, 15)
      lp_giaini.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130991383307999776, "Giacenza iniziale"), oApp.FormatQta, 15)
      lp_vgiaini.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130991383453750779, "Valore giacenza iniziale"), oApp.FormatImporti, 15)
      lp_prenot.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130991383709305779, "Prenotato"), oApp.FormatQta, 15)
      lp_vprenot.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130991383732879839, "Valore prenotato"), oApp.FormatImporti, 15)
      lp_ordin.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130991383932956675, "Ordinato"), oApp.FormatQta, 15)
      lp_vordin.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130991383960056097, "Valore ordinato"), oApp.FormatImporti, 15)
      lp_impeg.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130991384145093964, "Impegnato"), oApp.FormatQta, 15)
      lp_vimpeg.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130991384170835980, "Valore impegnato"), oApp.FormatImporti, 15)
      '--------------------------------------------------------------------------------------------------------------
      grvDepr.NTSAllowInsert = False
      grvDepr.NTSAllowUpdate = False
      grvDepr.NTSAllowDelete = False
      grvDepr.Enabled = False
      '--------------------------------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, True)
    '----------------------------------------------------------------------------------------------------------------
  End Sub

  Public Overridable Sub InitEntity(ByVal cleArmd As CLEMGARMD)
    oCleArmd = cleArmd
    AddHandler oCleArmd.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRMMGDEPR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim bCommeca As Boolean = CBool(oMenu.GetSettingBus("BNMGDEPR", "RECENT", ".", "RaggrCommeca", "0", " ", "0"))
    Dim bLotto As Boolean = CBool(oMenu.GetSettingBus("BNMGDEPR", "RECENT", ".", "RaggrLotto", "0", " ", "0"))
    Dim bUbicaz As Boolean = CBool(oMenu.GetSettingBus("BNMGDEPR", "RECENT", ".", "RaggrUbicaz", "0", " ", "0"))
    Dim nTmp As Integer = 0
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      bOnLoading = True
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      lbAr_codart.Text = oCleArmd.strDeprCodart
      If oMenu.ValCodiceDb(oCleArmd.strDeprCodart, DittaCorrente, "ARTICO", "S", "", dttTmp) = True Then
        If dttTmp.Rows(0)!ar_descr.Equals(DBNull.Value) = False Then lbXx_codart.Text = NTSCStr(dttTmp.Rows(0)!ar_descr)
      End If
      lbAr_magaz.Text = oCleArmd.dDeprMagaz.ToString
      lbXx_magaz.Text = oCleArmd.strDeprDesMagaz
      lbAr_fase.Text = oCleArmd.dDeprFase.ToString
      lbXx_fase.Text = oCleArmd.DecodificaFaseArticolo
      '--------------------------------------------------------------------------------------------------------------
      With dttTmp.Rows(0)
        bGescomm = CBool(IIf(NTSCStr(!ar_gescomm) <> "N", True, False))
        bGeslotti = CBool(IIf(NTSCStr(!ar_geslotti) <> "N", True, False))
        bGesubic = CBool(IIf(NTSCStr(!ar_gesubic) <> "N", True, False))
        If NTSCInt(!ar_codtagl) = 0 Then tlbDettTCO.Enabled = False
      End With
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dttTmp.Rows(0)!ar_gescomm) = "N" Then
        lp_commeca.Visible = False
        xx_commeca.Visible = False
        tlbCommessa.Down = False
        tlbCommessa.Enabled = False
      Else
        nTmp += 1
      End If
      If NTSCStr(dttTmp.Rows(0)!ar_geslotti) = "N" Then
        lp_lotto.Visible = False
        xx_lotto.Visible = False
        tlbLotti.Down = False
        tlbLotti.Enabled = False
      Else
        nTmp += 1
      End If
      If NTSCStr(dttTmp.Rows(0)!ar_gesubic) = "N" Then
        lp_ubicaz.Visible = False
        tlbUbicazioni.Down = False
        tlbUbicazioni.Enabled = False
      Else
        nTmp += 1
      End If
      '--------------------------------------------------------------------------------------------------------------
      If nTmp <= 1 Then
        tlbCommessa.Down = False
        tlbLotti.Down = False
        tlbUbicazioni.Down = False
        tlbCommessa.Enabled = False
        tlbLotti.Enabled = False
        tlbUbicazioni.Enabled = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If bCommeca = True And tlbCommessa.Enabled = True Then tlbCommessa.Down = True
      If bLotto = True And tlbLotti.Enabled = True Then tlbLotti.Down = True
      If bUbicaz = True And tlbUbicazioni.Enabled = True Then tlbUbicazioni.Down = True
      '--------------------------------------------------------------------------------------------------------------
      ApriDataSet()
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      '--------------------------------------------------------------------------------------------------------------
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      bOnLoading = False
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGDEPR_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    Try
      '--------------------------------------------------------------------------------------------------------------
      oMenu.SaveSettingBus("BNMGDEPR", "RECENT", ".", "RaggrCommeca", IIf(tlbCommessa.Down = True, "-1", "0").ToString, " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("BNMGDEPR", "RECENT", ".", "RaggrLotto", IIf(tlbLotti.Down = True, "-1", "0").ToString, " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("BNMGDEPR", "RECENT", ".", "RaggrUbicaz", IIf(tlbUbicazioni.Down = True, "-1", "0").ToString, " ", "NS.", "...", "...")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMMGDEPR_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcDepr.Dispose()
      dsDepr.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbCommessa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCommessa.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bOnLoading = True Then Return
      '--------------------------------------------------------------------------------------------------------------
      ApriDataSet()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub tlbLotti_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbLotti.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bOnLoading = True Then Return
      '--------------------------------------------------------------------------------------------------------------
      ApriDataSet()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub tlbUbicazioni_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbUbicazioni.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bOnLoading = True Then Return
      '--------------------------------------------------------------------------------------------------------------
      ApriDataSet()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbDettTCO_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDettTCO.ItemClick
    Dim oParam As New CLE__CLDP
    Dim frmLotc As FRMMGLOTC = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      If grvDepr.NTSGetCurrentDataRow() Is Nothing Then Return
      If NTSCStr(grvDepr.NTSGetCurrentDataRow!lp_esist).Trim = "" Then Return
      oCleArmd.strLotcCodart = lbAr_codart.Text
      oCleArmd.strLotcDesCodart = lbXx_codart.Text
      oCleArmd.strLotcMagaz = lbAr_magaz.Text
      oCleArmd.strLotcDesMagaz = lbXx_magaz.Text
      oCleArmd.strLotcFase = lbAr_fase.Text
      oCleArmd.strLotcDesFase = lbXx_fase.Text
      '--------------------------------------------------------------------------------------------------------------
      frmLotc = CType(NTSNewFormModal("FRMMGLOTC"), FRMMGLOTC)
      frmLotc.Init(oMenu, oParam, DittaCorrente)
      frmLotc.InitEntity(oCleArmd)
      frmLotc.ShowDialog()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      If Not frmLotc Is Nothing Then
        frmLotc.Dispose()
        frmLotc = Nothing
      End If
    End Try
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub
#End Region

  Public Overridable Sub ApriDataSet()
    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not dsDepr.Tables("LOTCPRO") Is Nothing Then dsDepr.Tables("LOTCPRO").Clear()
      '--------------------------------------------------------------------------------------------------------------
      If oCleArmd.GetDataDepr(tlbCommessa.Down, tlbLotti.Down, tlbUbicazioni.Down, dsDepr) = False Then Me.Close()
      dcDepr.DataSource = dsDepr.Tables("LOTCPRO")
      grDepr.DataSource = dcDepr
      '--------------------------------------------------------------------------------------------------------------
      lp_commeca.Visible = True
      xx_commeca.Visible = True
      If (tlbCommessa.Down = False) And (tlbLotti.Down = True Or tlbUbicazioni.Down = True) Then
        lp_commeca.Visible = False
        xx_commeca.Visible = False
      End If
      lp_lotto.Visible = True
      xx_lotto.Visible = True
      If (tlbLotti.Down = False) And (tlbCommessa.Down = True Or tlbUbicazioni.Down = True) Then
        lp_lotto.Visible = False
        xx_lotto.Visible = False
      End If
      lp_ubicaz.Visible = True
      If (tlbUbicazioni.Down = False) And (tlbCommessa.Down = True Or tlbLotti.Down = True) Then
        lp_ubicaz.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If bGescomm = False Then
        lp_commeca.Visible = False
        xx_commeca.Visible = False
      End If
      If bGeslotti = False Then
        lp_lotto.Visible = False
        xx_lotto.Visible = False
      End If
      If bGesubic = False Then lp_ubicaz.Visible = False
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  
End Class