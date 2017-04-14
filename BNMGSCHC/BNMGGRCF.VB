Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGGRCF
#Region "Variabili"
  Public oCleSchc As CLEMGSCHC
  Public dsGrcf As New DataSet
  Public dcGrcf As BindingSource = New BindingSource()
  Public oCallParams As CLE__CLDP
  Public bControlloEsistenza As Boolean = False

  Public bNoModal As Boolean = False
  Public strGrcfWhereCRM As String = ""
  Public lPosition As Integer = 0

  Private components As System.ComponentModel.IContainer

  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents grGrcf As NTSInformatica.NTSGrid
  Public WithEvents grvGrcf As NTSInformatica.NTSGridView
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents km_codart As NTSInformatica.NTSGridColumn
  Public WithEvents km_magaz As NTSInformatica.NTSGridColumn
  Public WithEvents ar_descr As NTSInformatica.NTSGridColumn
  Public WithEvents ar_unmis As NTSInformatica.NTSGridColumn
  Public WithEvents Espr1 As NTSInformatica.NTSGridColumn
  Public WithEvents Espr2 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_przmedio As NTSInformatica.NTSGridColumn
  Public WithEvents mm_perqta As NTSInformatica.NTSGridColumn
  Public WithEvents km_fase As NTSInformatica.NTSGridColumn
  Public WithEvents descr As NTSInformatica.NTSGridColumn
  Public WithEvents quant01 As NTSInformatica.NTSGridColumn
  Public WithEvents quant02 As NTSInformatica.NTSGridColumn
  Public WithEvents quant03 As NTSInformatica.NTSGridColumn
  Public WithEvents quant04 As NTSInformatica.NTSGridColumn
  Public WithEvents quant05 As NTSInformatica.NTSGridColumn
  Public WithEvents quant06 As NTSInformatica.NTSGridColumn
  Public WithEvents quant07 As NTSInformatica.NTSGridColumn
  Public WithEvents quant08 As NTSInformatica.NTSGridColumn
  Public WithEvents quant09 As NTSInformatica.NTSGridColumn
  Public WithEvents quant10 As NTSInformatica.NTSGridColumn
  Public WithEvents quant11 As NTSInformatica.NTSGridColumn
  Public WithEvents quant12 As NTSInformatica.NTSGridColumn
  Public WithEvents quant13 As NTSInformatica.NTSGridColumn
  Public WithEvents quant14 As NTSInformatica.NTSGridColumn
  Public WithEvents quant15 As NTSInformatica.NTSGridColumn
  Public WithEvents quant16 As NTSInformatica.NTSGridColumn
  Public WithEvents quant17 As NTSInformatica.NTSGridColumn
  Public WithEvents quant18 As NTSInformatica.NTSGridColumn
  Public WithEvents quant19 As NTSInformatica.NTSGridColumn
  Public WithEvents quant20 As NTSInformatica.NTSGridColumn
  Public WithEvents quant21 As NTSInformatica.NTSGridColumn
  Public WithEvents quant22 As NTSInformatica.NTSGridColumn
  Public WithEvents quant23 As NTSInformatica.NTSGridColumn
  Public WithEvents quant24 As NTSInformatica.NTSGridColumn
  Public WithEvents tlbImpostastampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbConto As NTSInformatica.NTSLabel
  Public WithEvents lbCon As NTSInformatica.NTSLabel
  Public WithEvents lbDescrConto As NTSInformatica.NTSLabel
  Public WithEvents lbTotale As NTSInformatica.NTSLabel
  Public WithEvents lbTot As NTSInformatica.NTSLabel
  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbImpostaStp As NTSInformatica.NTSBarButtonItem
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

  Public Overridable Sub InitEntity(ByRef cleSchc As CLEMGSCHC)
    oCleSchc = cleSchc
    AddHandler oCleSchc.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGGRCF))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbPrimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrecedente = New NTSInformatica.NTSBarButtonItem
    Me.tlbSuccessivo = New NTSInformatica.NTSBarButtonItem
    Me.tlbUltimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.tlbImpostastampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbImpostaStp = New NTSInformatica.NTSBarButtonItem
    Me.grGrcf = New NTSInformatica.NTSGrid
    Me.grvGrcf = New NTSInformatica.NTSGridView
    Me.ar_desint = New NTSInformatica.NTSGridColumn
    Me.km_codart = New NTSInformatica.NTSGridColumn
    Me.km_magaz = New NTSInformatica.NTSGridColumn
    Me.ar_descr = New NTSInformatica.NTSGridColumn
    Me.ar_unmis = New NTSInformatica.NTSGridColumn
    Me.Espr1 = New NTSInformatica.NTSGridColumn
    Me.Espr2 = New NTSInformatica.NTSGridColumn
    Me.xx_przmedio = New NTSInformatica.NTSGridColumn
    Me.mm_perqta = New NTSInformatica.NTSGridColumn
    Me.km_fase = New NTSInformatica.NTSGridColumn
    Me.descr = New NTSInformatica.NTSGridColumn
    Me.quant01 = New NTSInformatica.NTSGridColumn
    Me.quant02 = New NTSInformatica.NTSGridColumn
    Me.quant03 = New NTSInformatica.NTSGridColumn
    Me.quant04 = New NTSInformatica.NTSGridColumn
    Me.quant05 = New NTSInformatica.NTSGridColumn
    Me.quant06 = New NTSInformatica.NTSGridColumn
    Me.quant07 = New NTSInformatica.NTSGridColumn
    Me.quant08 = New NTSInformatica.NTSGridColumn
    Me.quant09 = New NTSInformatica.NTSGridColumn
    Me.quant10 = New NTSInformatica.NTSGridColumn
    Me.quant11 = New NTSInformatica.NTSGridColumn
    Me.quant12 = New NTSInformatica.NTSGridColumn
    Me.quant13 = New NTSInformatica.NTSGridColumn
    Me.quant14 = New NTSInformatica.NTSGridColumn
    Me.quant15 = New NTSInformatica.NTSGridColumn
    Me.quant16 = New NTSInformatica.NTSGridColumn
    Me.quant17 = New NTSInformatica.NTSGridColumn
    Me.quant18 = New NTSInformatica.NTSGridColumn
    Me.quant19 = New NTSInformatica.NTSGridColumn
    Me.quant20 = New NTSInformatica.NTSGridColumn
    Me.quant21 = New NTSInformatica.NTSGridColumn
    Me.quant22 = New NTSInformatica.NTSGridColumn
    Me.quant23 = New NTSInformatica.NTSGridColumn
    Me.quant24 = New NTSInformatica.NTSGridColumn
    Me.lbCon = New NTSInformatica.NTSLabel
    Me.lbConto = New NTSInformatica.NTSLabel
    Me.lbDescrConto = New NTSInformatica.NTSLabel
    Me.lbTot = New NTSInformatica.NTSLabel
    Me.lbTotale = New NTSInformatica.NTSLabel
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.pnGrid = New NTSInformatica.NTSPanel
    Me.pbBottom = New NTSInformatica.NTSPanel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grGrcf, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvGrcf, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
    CType(Me.pbBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pbBottom.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbGuida, Me.tlbEsci, Me.tlbImpostastampante, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbUltimo, Me.tlbImpostaStp, Me.tlbStampaVideo})
    Me.NtsBarManager1.MaxItemId = 32
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbPrimo
    '
    Me.tlbPrimo.Caption = "Primo"
    Me.tlbPrimo.Glyph = CType(resources.GetObject("tlbPrimo.Glyph"), System.Drawing.Image)
    Me.tlbPrimo.Id = 18
    Me.tlbPrimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P))
    Me.tlbPrimo.Name = "tlbPrimo"
    Me.tlbPrimo.Visible = True
    '
    'tlbPrecedente
    '
    Me.tlbPrecedente.Caption = "Precedente"
    Me.tlbPrecedente.Glyph = CType(resources.GetObject("tlbPrecedente.Glyph"), System.Drawing.Image)
    Me.tlbPrecedente.Id = 19
    Me.tlbPrecedente.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
    Me.tlbPrecedente.Name = "tlbPrecedente"
    Me.tlbPrecedente.Visible = True
    '
    'tlbSuccessivo
    '
    Me.tlbSuccessivo.Caption = "Successivo"
    Me.tlbSuccessivo.Glyph = CType(resources.GetObject("tlbSuccessivo.Glyph"), System.Drawing.Image)
    Me.tlbSuccessivo.Id = 20
    Me.tlbSuccessivo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
    Me.tlbSuccessivo.Name = "tlbSuccessivo"
    Me.tlbSuccessivo.Visible = True
    '
    'tlbUltimo
    '
    Me.tlbUltimo.Caption = "Ultimo"
    Me.tlbUltimo.Glyph = CType(resources.GetObject("tlbUltimo.Glyph"), System.Drawing.Image)
    Me.tlbUltimo.Id = 21
    Me.tlbUltimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa anteprima a video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 31
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
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
    Me.tlbEsci.Id = 26
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'tlbImpostastampante
    '
    Me.tlbImpostastampante.Caption = "Imposta Stampante"
    Me.tlbImpostastampante.Id = 16
    Me.tlbImpostastampante.Name = "tlbImpostastampante"
    Me.tlbImpostastampante.Visible = True
    '
    'tlbImpostaStp
    '
    Me.tlbImpostaStp.Caption = "Imposta stampante"
    Me.tlbImpostaStp.Id = 28
    Me.tlbImpostaStp.Name = "tlbImpostaStp"
    Me.tlbImpostaStp.Visible = True
    '
    'grGrcf
    '
    Me.grGrcf.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grGrcf.EmbeddedNavigator.Name = ""
    Me.grGrcf.Location = New System.Drawing.Point(0, 0)
    Me.grGrcf.MainView = Me.grvGrcf
    Me.grGrcf.Name = "grGrcf"
    Me.grGrcf.Size = New System.Drawing.Size(718, 278)
    Me.grGrcf.TabIndex = 4
    Me.grGrcf.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvGrcf})
    '
    'grvGrcf
    '
    Me.grvGrcf.ActiveFilterEnabled = False
    '
    'ar_desint
    '
    Me.ar_desint.AppearanceCell.Options.UseBackColor = True
    Me.ar_desint.AppearanceCell.Options.UseTextOptions = True
    Me.ar_desint.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_desint.Caption = "Descr. interna"
    Me.ar_desint.Enabled = False
    Me.ar_desint.FieldName = "ar_desint"
    Me.ar_desint.Name = "ar_desint"
    Me.ar_desint.NTSRepositoryComboBox = Nothing
    Me.ar_desint.NTSRepositoryItemCheck = Nothing
    Me.ar_desint.NTSRepositoryItemMemo = Nothing
    Me.ar_desint.NTSRepositoryItemText = Nothing
    Me.ar_desint.OptionsColumn.AllowEdit = False
    Me.ar_desint.OptionsColumn.ReadOnly = True
    Me.grvGrcf.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.km_codart, Me.km_magaz, Me.ar_descr, Me.ar_unmis, Me.Espr1, Me.Espr2, Me.xx_przmedio, Me.mm_perqta, Me.km_fase, Me.descr, Me.quant01, Me.quant02, Me.quant03, Me.quant04, Me.quant05, Me.quant06, Me.quant07, Me.quant08, Me.quant09, Me.quant10, Me.quant11, Me.quant12, Me.quant13, Me.quant14, Me.quant15, Me.quant16, Me.quant17, Me.quant18, Me.quant19, Me.quant20, Me.quant21, Me.quant22, Me.quant23, Me.quant24, Me.ar_desint})
    Me.grvGrcf.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvGrcf.Enabled = True
    Me.grvGrcf.GridControl = Me.grGrcf
    Me.grvGrcf.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvGrcf.GroupPanelText = "Per raggruppare: Tasto DX sulla colonna -> Group by this column *** Per totali pa" & _
        "rziali/generali: sul piede di gruppo della colonna tasto DX -> SUM "
    Me.grvGrcf.MinRowHeight = 14
    Me.grvGrcf.Name = "grvGrcf"
    Me.grvGrcf.NTSAllowDelete = True
    Me.grvGrcf.NTSAllowInsert = True
    Me.grvGrcf.NTSAllowUpdate = True
    Me.grvGrcf.NTSMenuContext = Nothing
    Me.grvGrcf.OptionsCustomization.AllowRowSizing = True
    Me.grvGrcf.OptionsFilter.AllowFilterEditor = False
    Me.grvGrcf.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvGrcf.OptionsNavigation.UseTabKey = False
    Me.grvGrcf.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvGrcf.OptionsView.ColumnAutoWidth = False
    Me.grvGrcf.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvGrcf.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvGrcf.OptionsView.ShowGroupPanel = False
    Me.grvGrcf.RowHeight = 14
    '
    'km_codart
    '
    Me.km_codart.AppearanceCell.Options.UseBackColor = True
    Me.km_codart.AppearanceCell.Options.UseTextOptions = True
    Me.km_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_codart.AppearanceHeader.Options.UseTextOptions = True
    Me.km_codart.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.km_codart.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.km_codart.Caption = "Cod. Articolo"
    Me.km_codart.Enabled = False
    Me.km_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_codart.Name = "km_codart"
    Me.km_codart.NTSRepositoryComboBox = Nothing
    Me.km_codart.NTSRepositoryItemCheck = Nothing
    Me.km_codart.NTSRepositoryItemMemo = Nothing
    Me.km_codart.NTSRepositoryItemText = Nothing
    Me.km_codart.OptionsColumn.AllowEdit = False
    Me.km_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_codart.OptionsColumn.ReadOnly = True
    Me.km_codart.OptionsFilter.AllowFilter = False
    Me.km_codart.Visible = True
    Me.km_codart.VisibleIndex = 0
    Me.km_codart.Width = 83
    '
    'km_magaz
    '
    Me.km_magaz.AppearanceCell.Options.UseBackColor = True
    Me.km_magaz.AppearanceCell.Options.UseTextOptions = True
    Me.km_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_magaz.AppearanceHeader.Options.UseTextOptions = True
    Me.km_magaz.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.km_magaz.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.km_magaz.Caption = "Mag."
    Me.km_magaz.Enabled = False
    Me.km_magaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_magaz.Name = "km_magaz"
    Me.km_magaz.NTSRepositoryComboBox = Nothing
    Me.km_magaz.NTSRepositoryItemCheck = Nothing
    Me.km_magaz.NTSRepositoryItemMemo = Nothing
    Me.km_magaz.NTSRepositoryItemText = Nothing
    Me.km_magaz.OptionsColumn.AllowEdit = False
    Me.km_magaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_magaz.OptionsColumn.ReadOnly = True
    Me.km_magaz.OptionsFilter.AllowFilter = False
    Me.km_magaz.Visible = True
    Me.km_magaz.VisibleIndex = 1
    Me.km_magaz.Width = 35
    '
    'ar_descr
    '
    Me.ar_descr.AppearanceCell.Options.UseBackColor = True
    Me.ar_descr.AppearanceCell.Options.UseTextOptions = True
    Me.ar_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_descr.AppearanceHeader.Options.UseTextOptions = True
    Me.ar_descr.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.ar_descr.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.ar_descr.Caption = "Descrizione"
    Me.ar_descr.Enabled = False
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
    Me.ar_descr.VisibleIndex = 2
    Me.ar_descr.Width = 83
    '
    'ar_unmis
    '
    Me.ar_unmis.AppearanceCell.Options.UseBackColor = True
    Me.ar_unmis.AppearanceCell.Options.UseTextOptions = True
    Me.ar_unmis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_unmis.AppearanceHeader.Options.UseTextOptions = True
    Me.ar_unmis.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.ar_unmis.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.ar_unmis.Caption = "UM"
    Me.ar_unmis.Enabled = False
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
    Me.ar_unmis.Visible = True
    Me.ar_unmis.VisibleIndex = 3
    Me.ar_unmis.Width = 30
    '
    'Espr1
    '
    Me.Espr1.AppearanceCell.Options.UseBackColor = True
    Me.Espr1.AppearanceCell.Options.UseTextOptions = True
    Me.Espr1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.Espr1.AppearanceHeader.Options.UseTextOptions = True
    Me.Espr1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.Espr1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.Espr1.Caption = "Quantità"
    Me.Espr1.Enabled = False
    Me.Espr1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.Espr1.Name = "Espr1"
    Me.Espr1.NTSRepositoryComboBox = Nothing
    Me.Espr1.NTSRepositoryItemCheck = Nothing
    Me.Espr1.NTSRepositoryItemMemo = Nothing
    Me.Espr1.NTSRepositoryItemText = Nothing
    Me.Espr1.OptionsColumn.AllowEdit = False
    Me.Espr1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.Espr1.OptionsColumn.ReadOnly = True
    Me.Espr1.OptionsFilter.AllowFilter = False
    Me.Espr1.Visible = True
    Me.Espr1.VisibleIndex = 4
    Me.Espr1.Width = 83
    '
    'Espr2
    '
    Me.Espr2.AppearanceCell.Options.UseBackColor = True
    Me.Espr2.AppearanceCell.Options.UseTextOptions = True
    Me.Espr2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.Espr2.AppearanceHeader.Options.UseTextOptions = True
    Me.Espr2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.Espr2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.Espr2.Caption = "Totale Vend/Acq"
    Me.Espr2.Enabled = False
    Me.Espr2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.Espr2.Name = "Espr2"
    Me.Espr2.NTSRepositoryComboBox = Nothing
    Me.Espr2.NTSRepositoryItemCheck = Nothing
    Me.Espr2.NTSRepositoryItemMemo = Nothing
    Me.Espr2.NTSRepositoryItemText = Nothing
    Me.Espr2.OptionsColumn.AllowEdit = False
    Me.Espr2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.Espr2.OptionsColumn.ReadOnly = True
    Me.Espr2.OptionsFilter.AllowFilter = False
    Me.Espr2.Visible = True
    Me.Espr2.VisibleIndex = 5
    Me.Espr2.Width = 100
    '
    'xx_przmedio
    '
    Me.xx_przmedio.AppearanceCell.Options.UseBackColor = True
    Me.xx_przmedio.AppearanceCell.Options.UseTextOptions = True
    Me.xx_przmedio.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_przmedio.AppearanceHeader.Options.UseTextOptions = True
    Me.xx_przmedio.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.xx_przmedio.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.xx_przmedio.Caption = "Prezzo Medio"
    Me.xx_przmedio.Enabled = False
    Me.xx_przmedio.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_przmedio.Name = "xx_przmedio"
    Me.xx_przmedio.NTSRepositoryComboBox = Nothing
    Me.xx_przmedio.NTSRepositoryItemCheck = Nothing
    Me.xx_przmedio.NTSRepositoryItemMemo = Nothing
    Me.xx_przmedio.NTSRepositoryItemText = Nothing
    Me.xx_przmedio.OptionsColumn.AllowEdit = False
    Me.xx_przmedio.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_przmedio.OptionsColumn.ReadOnly = True
    Me.xx_przmedio.OptionsFilter.AllowFilter = False
    Me.xx_przmedio.Visible = True
    Me.xx_przmedio.VisibleIndex = 6
    Me.xx_przmedio.Width = 83
    '
    'mm_perqta
    '
    Me.mm_perqta.AppearanceCell.Options.UseBackColor = True
    Me.mm_perqta.AppearanceCell.Options.UseTextOptions = True
    Me.mm_perqta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_perqta.AppearanceHeader.Options.UseTextOptions = True
    Me.mm_perqta.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.mm_perqta.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.mm_perqta.Caption = "Prz/Qtà"
    Me.mm_perqta.Enabled = False
    Me.mm_perqta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_perqta.Name = "mm_perqta"
    Me.mm_perqta.NTSRepositoryComboBox = Nothing
    Me.mm_perqta.NTSRepositoryItemCheck = Nothing
    Me.mm_perqta.NTSRepositoryItemMemo = Nothing
    Me.mm_perqta.NTSRepositoryItemText = Nothing
    Me.mm_perqta.OptionsColumn.AllowEdit = False
    Me.mm_perqta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_perqta.OptionsColumn.ReadOnly = True
    Me.mm_perqta.OptionsFilter.AllowFilter = False
    Me.mm_perqta.Visible = True
    Me.mm_perqta.VisibleIndex = 7
    Me.mm_perqta.Width = 83
    '
    'km_fase
    '
    Me.km_fase.AppearanceCell.Options.UseBackColor = True
    Me.km_fase.AppearanceCell.Options.UseTextOptions = True
    Me.km_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_fase.AppearanceHeader.Options.UseTextOptions = True
    Me.km_fase.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.km_fase.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.km_fase.Caption = "Fase"
    Me.km_fase.Enabled = False
    Me.km_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_fase.Name = "km_fase"
    Me.km_fase.NTSRepositoryComboBox = Nothing
    Me.km_fase.NTSRepositoryItemCheck = Nothing
    Me.km_fase.NTSRepositoryItemMemo = Nothing
    Me.km_fase.NTSRepositoryItemText = Nothing
    Me.km_fase.OptionsColumn.AllowEdit = False
    Me.km_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_fase.OptionsColumn.ReadOnly = True
    Me.km_fase.OptionsFilter.AllowFilter = False
    Me.km_fase.Visible = True
    Me.km_fase.VisibleIndex = 8
    Me.km_fase.Width = 35
    '
    'descr
    '
    Me.descr.AppearanceCell.Options.UseBackColor = True
    Me.descr.AppearanceCell.Options.UseTextOptions = True
    Me.descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.descr.AppearanceHeader.Options.UseTextOptions = True
    Me.descr.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.descr.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.descr.Caption = "Descr. Fase"
    Me.descr.Enabled = False
    Me.descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.descr.Name = "descr"
    Me.descr.NTSRepositoryComboBox = Nothing
    Me.descr.NTSRepositoryItemCheck = Nothing
    Me.descr.NTSRepositoryItemMemo = Nothing
    Me.descr.NTSRepositoryItemText = Nothing
    Me.descr.OptionsColumn.AllowEdit = False
    Me.descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.descr.OptionsColumn.ReadOnly = True
    Me.descr.OptionsFilter.AllowFilter = False
    Me.descr.Visible = True
    Me.descr.VisibleIndex = 9
    Me.descr.Width = 83
    '
    'quant01
    '
    Me.quant01.AppearanceCell.Options.UseBackColor = True
    Me.quant01.AppearanceCell.Options.UseTextOptions = True
    Me.quant01.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.quant01.AppearanceHeader.Options.UseTextOptions = True
    Me.quant01.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.quant01.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.quant01.Caption = "Qtà 1 TC"
    Me.quant01.Enabled = False
    Me.quant01.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.quant01.Name = "quant01"
    Me.quant01.NTSRepositoryComboBox = Nothing
    Me.quant01.NTSRepositoryItemCheck = Nothing
    Me.quant01.NTSRepositoryItemMemo = Nothing
    Me.quant01.NTSRepositoryItemText = Nothing
    Me.quant01.OptionsColumn.AllowEdit = False
    Me.quant01.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.quant01.OptionsColumn.ReadOnly = True
    Me.quant01.OptionsFilter.AllowFilter = False
    Me.quant01.Visible = True
    Me.quant01.VisibleIndex = 10
    Me.quant01.Width = 83
    '
    'quant02
    '
    Me.quant02.AppearanceCell.Options.UseBackColor = True
    Me.quant02.AppearanceCell.Options.UseTextOptions = True
    Me.quant02.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.quant02.AppearanceHeader.Options.UseTextOptions = True
    Me.quant02.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.quant02.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.quant02.Caption = "Qtà 2 TC"
    Me.quant02.Enabled = False
    Me.quant02.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.quant02.Name = "quant02"
    Me.quant02.NTSRepositoryComboBox = Nothing
    Me.quant02.NTSRepositoryItemCheck = Nothing
    Me.quant02.NTSRepositoryItemMemo = Nothing
    Me.quant02.NTSRepositoryItemText = Nothing
    Me.quant02.OptionsColumn.AllowEdit = False
    Me.quant02.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.quant02.OptionsColumn.ReadOnly = True
    Me.quant02.OptionsFilter.AllowFilter = False
    Me.quant02.Visible = True
    Me.quant02.VisibleIndex = 11
    Me.quant02.Width = 83
    '
    'quant03
    '
    Me.quant03.AppearanceCell.Options.UseBackColor = True
    Me.quant03.AppearanceCell.Options.UseTextOptions = True
    Me.quant03.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.quant03.AppearanceHeader.Options.UseTextOptions = True
    Me.quant03.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.quant03.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.quant03.Caption = "Qtà 3 TC"
    Me.quant03.Enabled = False
    Me.quant03.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.quant03.Name = "quant03"
    Me.quant03.NTSRepositoryComboBox = Nothing
    Me.quant03.NTSRepositoryItemCheck = Nothing
    Me.quant03.NTSRepositoryItemMemo = Nothing
    Me.quant03.NTSRepositoryItemText = Nothing
    Me.quant03.OptionsColumn.AllowEdit = False
    Me.quant03.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.quant03.OptionsColumn.ReadOnly = True
    Me.quant03.OptionsFilter.AllowFilter = False
    Me.quant03.Visible = True
    Me.quant03.VisibleIndex = 12
    Me.quant03.Width = 83
    '
    'quant04
    '
    Me.quant04.AppearanceCell.Options.UseBackColor = True
    Me.quant04.AppearanceCell.Options.UseTextOptions = True
    Me.quant04.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.quant04.AppearanceHeader.Options.UseTextOptions = True
    Me.quant04.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.quant04.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.quant04.Caption = "Qtà 4 TC"
    Me.quant04.Enabled = False
    Me.quant04.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.quant04.Name = "quant04"
    Me.quant04.NTSRepositoryComboBox = Nothing
    Me.quant04.NTSRepositoryItemCheck = Nothing
    Me.quant04.NTSRepositoryItemMemo = Nothing
    Me.quant04.NTSRepositoryItemText = Nothing
    Me.quant04.OptionsColumn.AllowEdit = False
    Me.quant04.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.quant04.OptionsColumn.ReadOnly = True
    Me.quant04.OptionsFilter.AllowFilter = False
    Me.quant04.Visible = True
    Me.quant04.VisibleIndex = 13
    Me.quant04.Width = 83
    '
    'quant05
    '
    Me.quant05.AppearanceCell.Options.UseBackColor = True
    Me.quant05.AppearanceCell.Options.UseTextOptions = True
    Me.quant05.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.quant05.AppearanceHeader.Options.UseTextOptions = True
    Me.quant05.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.quant05.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.quant05.Caption = "Qtà 5 TC"
    Me.quant05.Enabled = False
    Me.quant05.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.quant05.Name = "quant05"
    Me.quant05.NTSRepositoryComboBox = Nothing
    Me.quant05.NTSRepositoryItemCheck = Nothing
    Me.quant05.NTSRepositoryItemMemo = Nothing
    Me.quant05.NTSRepositoryItemText = Nothing
    Me.quant05.OptionsColumn.AllowEdit = False
    Me.quant05.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.quant05.OptionsColumn.ReadOnly = True
    Me.quant05.OptionsFilter.AllowFilter = False
    Me.quant05.Visible = True
    Me.quant05.VisibleIndex = 14
    Me.quant05.Width = 83
    '
    'quant06
    '
    Me.quant06.AppearanceCell.Options.UseBackColor = True
    Me.quant06.AppearanceCell.Options.UseTextOptions = True
    Me.quant06.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.quant06.AppearanceHeader.Options.UseTextOptions = True
    Me.quant06.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.quant06.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.quant06.Caption = "Qtà 6 TC"
    Me.quant06.Enabled = False
    Me.quant06.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.quant06.Name = "quant06"
    Me.quant06.NTSRepositoryComboBox = Nothing
    Me.quant06.NTSRepositoryItemCheck = Nothing
    Me.quant06.NTSRepositoryItemMemo = Nothing
    Me.quant06.NTSRepositoryItemText = Nothing
    Me.quant06.OptionsColumn.AllowEdit = False
    Me.quant06.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.quant06.OptionsColumn.ReadOnly = True
    Me.quant06.OptionsFilter.AllowFilter = False
    Me.quant06.Visible = True
    Me.quant06.VisibleIndex = 15
    Me.quant06.Width = 83
    '
    'quant07
    '
    Me.quant07.AppearanceCell.Options.UseBackColor = True
    Me.quant07.AppearanceCell.Options.UseTextOptions = True
    Me.quant07.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.quant07.AppearanceHeader.Options.UseTextOptions = True
    Me.quant07.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.quant07.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.quant07.Caption = "Qtà 7 TC"
    Me.quant07.Enabled = False
    Me.quant07.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.quant07.Name = "quant07"
    Me.quant07.NTSRepositoryComboBox = Nothing
    Me.quant07.NTSRepositoryItemCheck = Nothing
    Me.quant07.NTSRepositoryItemMemo = Nothing
    Me.quant07.NTSRepositoryItemText = Nothing
    Me.quant07.OptionsColumn.AllowEdit = False
    Me.quant07.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.quant07.OptionsColumn.ReadOnly = True
    Me.quant07.OptionsFilter.AllowFilter = False
    Me.quant07.Visible = True
    Me.quant07.VisibleIndex = 16
    Me.quant07.Width = 83
    '
    'quant08
    '
    Me.quant08.AppearanceCell.Options.UseBackColor = True
    Me.quant08.AppearanceCell.Options.UseTextOptions = True
    Me.quant08.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.quant08.AppearanceHeader.Options.UseTextOptions = True
    Me.quant08.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.quant08.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.quant08.Caption = "Qtà 8 TC"
    Me.quant08.Enabled = False
    Me.quant08.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.quant08.Name = "quant08"
    Me.quant08.NTSRepositoryComboBox = Nothing
    Me.quant08.NTSRepositoryItemCheck = Nothing
    Me.quant08.NTSRepositoryItemMemo = Nothing
    Me.quant08.NTSRepositoryItemText = Nothing
    Me.quant08.OptionsColumn.AllowEdit = False
    Me.quant08.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.quant08.OptionsColumn.ReadOnly = True
    Me.quant08.OptionsFilter.AllowFilter = False
    Me.quant08.Visible = True
    Me.quant08.VisibleIndex = 17
    Me.quant08.Width = 83
    '
    'quant09
    '
    Me.quant09.AppearanceCell.Options.UseBackColor = True
    Me.quant09.AppearanceCell.Options.UseTextOptions = True
    Me.quant09.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.quant09.AppearanceHeader.Options.UseTextOptions = True
    Me.quant09.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.quant09.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.quant09.Caption = "Qtà 9 TC"
    Me.quant09.Enabled = False
    Me.quant09.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.quant09.Name = "quant09"
    Me.quant09.NTSRepositoryComboBox = Nothing
    Me.quant09.NTSRepositoryItemCheck = Nothing
    Me.quant09.NTSRepositoryItemMemo = Nothing
    Me.quant09.NTSRepositoryItemText = Nothing
    Me.quant09.OptionsColumn.AllowEdit = False
    Me.quant09.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.quant09.OptionsColumn.ReadOnly = True
    Me.quant09.OptionsFilter.AllowFilter = False
    Me.quant09.Visible = True
    Me.quant09.VisibleIndex = 18
    Me.quant09.Width = 83
    '
    'quant10
    '
    Me.quant10.AppearanceCell.Options.UseBackColor = True
    Me.quant10.AppearanceCell.Options.UseTextOptions = True
    Me.quant10.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.quant10.AppearanceHeader.Options.UseTextOptions = True
    Me.quant10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.quant10.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.quant10.Caption = "Qtà 10 TC"
    Me.quant10.Enabled = False
    Me.quant10.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.quant10.Name = "quant10"
    Me.quant10.NTSRepositoryComboBox = Nothing
    Me.quant10.NTSRepositoryItemCheck = Nothing
    Me.quant10.NTSRepositoryItemMemo = Nothing
    Me.quant10.NTSRepositoryItemText = Nothing
    Me.quant10.OptionsColumn.AllowEdit = False
    Me.quant10.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.quant10.OptionsColumn.ReadOnly = True
    Me.quant10.OptionsFilter.AllowFilter = False
    Me.quant10.Visible = True
    Me.quant10.VisibleIndex = 19
    Me.quant10.Width = 83
    '
    'quant11
    '
    Me.quant11.AppearanceCell.Options.UseBackColor = True
    Me.quant11.AppearanceCell.Options.UseTextOptions = True
    Me.quant11.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.quant11.AppearanceHeader.Options.UseTextOptions = True
    Me.quant11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.quant11.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.quant11.Caption = "Qtà 11 TC"
    Me.quant11.Enabled = False
    Me.quant11.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.quant11.Name = "quant11"
    Me.quant11.NTSRepositoryComboBox = Nothing
    Me.quant11.NTSRepositoryItemCheck = Nothing
    Me.quant11.NTSRepositoryItemMemo = Nothing
    Me.quant11.NTSRepositoryItemText = Nothing
    Me.quant11.OptionsColumn.AllowEdit = False
    Me.quant11.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.quant11.OptionsColumn.ReadOnly = True
    Me.quant11.OptionsFilter.AllowFilter = False
    Me.quant11.Visible = True
    Me.quant11.VisibleIndex = 20
    Me.quant11.Width = 83
    '
    'quant12
    '
    Me.quant12.AppearanceCell.Options.UseBackColor = True
    Me.quant12.AppearanceCell.Options.UseTextOptions = True
    Me.quant12.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.quant12.AppearanceHeader.Options.UseTextOptions = True
    Me.quant12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.quant12.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.quant12.Caption = "Qtà 12 TC"
    Me.quant12.Enabled = False
    Me.quant12.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.quant12.Name = "quant12"
    Me.quant12.NTSRepositoryComboBox = Nothing
    Me.quant12.NTSRepositoryItemCheck = Nothing
    Me.quant12.NTSRepositoryItemMemo = Nothing
    Me.quant12.NTSRepositoryItemText = Nothing
    Me.quant12.OptionsColumn.AllowEdit = False
    Me.quant12.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.quant12.OptionsColumn.ReadOnly = True
    Me.quant12.OptionsFilter.AllowFilter = False
    Me.quant12.Visible = True
    Me.quant12.VisibleIndex = 21
    Me.quant12.Width = 83
    '
    'quant13
    '
    Me.quant13.AppearanceCell.Options.UseBackColor = True
    Me.quant13.AppearanceCell.Options.UseTextOptions = True
    Me.quant13.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.quant13.AppearanceHeader.Options.UseTextOptions = True
    Me.quant13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.quant13.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.quant13.Caption = "Qtà 13 TC"
    Me.quant13.Enabled = False
    Me.quant13.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.quant13.Name = "quant13"
    Me.quant13.NTSRepositoryComboBox = Nothing
    Me.quant13.NTSRepositoryItemCheck = Nothing
    Me.quant13.NTSRepositoryItemMemo = Nothing
    Me.quant13.NTSRepositoryItemText = Nothing
    Me.quant13.OptionsColumn.AllowEdit = False
    Me.quant13.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.quant13.OptionsColumn.ReadOnly = True
    Me.quant13.OptionsFilter.AllowFilter = False
    Me.quant13.Visible = True
    Me.quant13.VisibleIndex = 22
    Me.quant13.Width = 83
    '
    'quant14
    '
    Me.quant14.AppearanceCell.Options.UseBackColor = True
    Me.quant14.AppearanceCell.Options.UseTextOptions = True
    Me.quant14.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.quant14.AppearanceHeader.Options.UseTextOptions = True
    Me.quant14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.quant14.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.quant14.Caption = "Qtà 14 TC"
    Me.quant14.Enabled = False
    Me.quant14.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.quant14.Name = "quant14"
    Me.quant14.NTSRepositoryComboBox = Nothing
    Me.quant14.NTSRepositoryItemCheck = Nothing
    Me.quant14.NTSRepositoryItemMemo = Nothing
    Me.quant14.NTSRepositoryItemText = Nothing
    Me.quant14.OptionsColumn.AllowEdit = False
    Me.quant14.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.quant14.OptionsColumn.ReadOnly = True
    Me.quant14.OptionsFilter.AllowFilter = False
    Me.quant14.Visible = True
    Me.quant14.VisibleIndex = 23
    Me.quant14.Width = 83
    '
    'quant15
    '
    Me.quant15.AppearanceCell.Options.UseBackColor = True
    Me.quant15.AppearanceCell.Options.UseTextOptions = True
    Me.quant15.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.quant15.AppearanceHeader.Options.UseTextOptions = True
    Me.quant15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.quant15.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.quant15.Caption = "Qtà 15 TC"
    Me.quant15.Enabled = False
    Me.quant15.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.quant15.Name = "quant15"
    Me.quant15.NTSRepositoryComboBox = Nothing
    Me.quant15.NTSRepositoryItemCheck = Nothing
    Me.quant15.NTSRepositoryItemMemo = Nothing
    Me.quant15.NTSRepositoryItemText = Nothing
    Me.quant15.OptionsColumn.AllowEdit = False
    Me.quant15.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.quant15.OptionsColumn.ReadOnly = True
    Me.quant15.OptionsFilter.AllowFilter = False
    Me.quant15.Visible = True
    Me.quant15.VisibleIndex = 24
    Me.quant15.Width = 83
    '
    'quant16
    '
    Me.quant16.AppearanceCell.Options.UseBackColor = True
    Me.quant16.AppearanceCell.Options.UseTextOptions = True
    Me.quant16.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.quant16.AppearanceHeader.Options.UseTextOptions = True
    Me.quant16.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.quant16.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.quant16.Caption = "Qtà 16 TC"
    Me.quant16.Enabled = False
    Me.quant16.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.quant16.Name = "quant16"
    Me.quant16.NTSRepositoryComboBox = Nothing
    Me.quant16.NTSRepositoryItemCheck = Nothing
    Me.quant16.NTSRepositoryItemMemo = Nothing
    Me.quant16.NTSRepositoryItemText = Nothing
    Me.quant16.OptionsColumn.AllowEdit = False
    Me.quant16.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.quant16.OptionsColumn.ReadOnly = True
    Me.quant16.OptionsFilter.AllowFilter = False
    Me.quant16.Visible = True
    Me.quant16.VisibleIndex = 25
    Me.quant16.Width = 83
    '
    'quant17
    '
    Me.quant17.AppearanceCell.Options.UseBackColor = True
    Me.quant17.AppearanceCell.Options.UseTextOptions = True
    Me.quant17.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.quant17.AppearanceHeader.Options.UseTextOptions = True
    Me.quant17.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.quant17.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.quant17.Caption = "Qtà 17 TC"
    Me.quant17.Enabled = False
    Me.quant17.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.quant17.Name = "quant17"
    Me.quant17.NTSRepositoryComboBox = Nothing
    Me.quant17.NTSRepositoryItemCheck = Nothing
    Me.quant17.NTSRepositoryItemMemo = Nothing
    Me.quant17.NTSRepositoryItemText = Nothing
    Me.quant17.OptionsColumn.AllowEdit = False
    Me.quant17.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.quant17.OptionsColumn.ReadOnly = True
    Me.quant17.OptionsFilter.AllowFilter = False
    Me.quant17.Visible = True
    Me.quant17.VisibleIndex = 26
    Me.quant17.Width = 83
    '
    'quant18
    '
    Me.quant18.AppearanceCell.Options.UseBackColor = True
    Me.quant18.AppearanceCell.Options.UseTextOptions = True
    Me.quant18.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.quant18.AppearanceHeader.Options.UseTextOptions = True
    Me.quant18.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.quant18.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.quant18.Caption = "Qtà 18 TC"
    Me.quant18.Enabled = False
    Me.quant18.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.quant18.Name = "quant18"
    Me.quant18.NTSRepositoryComboBox = Nothing
    Me.quant18.NTSRepositoryItemCheck = Nothing
    Me.quant18.NTSRepositoryItemMemo = Nothing
    Me.quant18.NTSRepositoryItemText = Nothing
    Me.quant18.OptionsColumn.AllowEdit = False
    Me.quant18.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.quant18.OptionsColumn.ReadOnly = True
    Me.quant18.OptionsFilter.AllowFilter = False
    Me.quant18.Visible = True
    Me.quant18.VisibleIndex = 27
    Me.quant18.Width = 83
    '
    'quant19
    '
    Me.quant19.AppearanceCell.Options.UseBackColor = True
    Me.quant19.AppearanceCell.Options.UseTextOptions = True
    Me.quant19.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.quant19.AppearanceHeader.Options.UseTextOptions = True
    Me.quant19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.quant19.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.quant19.Caption = "Qtà 19 TC"
    Me.quant19.Enabled = False
    Me.quant19.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.quant19.Name = "quant19"
    Me.quant19.NTSRepositoryComboBox = Nothing
    Me.quant19.NTSRepositoryItemCheck = Nothing
    Me.quant19.NTSRepositoryItemMemo = Nothing
    Me.quant19.NTSRepositoryItemText = Nothing
    Me.quant19.OptionsColumn.AllowEdit = False
    Me.quant19.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.quant19.OptionsColumn.ReadOnly = True
    Me.quant19.OptionsFilter.AllowFilter = False
    Me.quant19.Visible = True
    Me.quant19.VisibleIndex = 28
    Me.quant19.Width = 83
    '
    'quant20
    '
    Me.quant20.AppearanceCell.Options.UseBackColor = True
    Me.quant20.AppearanceCell.Options.UseTextOptions = True
    Me.quant20.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.quant20.AppearanceHeader.Options.UseTextOptions = True
    Me.quant20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.quant20.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.quant20.Caption = "Qtà 20 TC"
    Me.quant20.Enabled = False
    Me.quant20.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.quant20.Name = "quant20"
    Me.quant20.NTSRepositoryComboBox = Nothing
    Me.quant20.NTSRepositoryItemCheck = Nothing
    Me.quant20.NTSRepositoryItemMemo = Nothing
    Me.quant20.NTSRepositoryItemText = Nothing
    Me.quant20.OptionsColumn.AllowEdit = False
    Me.quant20.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.quant20.OptionsColumn.ReadOnly = True
    Me.quant20.OptionsFilter.AllowFilter = False
    Me.quant20.Visible = True
    Me.quant20.VisibleIndex = 29
    Me.quant20.Width = 83
    '
    'quant21
    '
    Me.quant21.AppearanceCell.Options.UseBackColor = True
    Me.quant21.AppearanceCell.Options.UseTextOptions = True
    Me.quant21.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.quant21.AppearanceHeader.Options.UseTextOptions = True
    Me.quant21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.quant21.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.quant21.Caption = "Qtà 21 TC"
    Me.quant21.Enabled = False
    Me.quant21.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.quant21.Name = "quant21"
    Me.quant21.NTSRepositoryComboBox = Nothing
    Me.quant21.NTSRepositoryItemCheck = Nothing
    Me.quant21.NTSRepositoryItemMemo = Nothing
    Me.quant21.NTSRepositoryItemText = Nothing
    Me.quant21.OptionsColumn.AllowEdit = False
    Me.quant21.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.quant21.OptionsColumn.ReadOnly = True
    Me.quant21.OptionsFilter.AllowFilter = False
    Me.quant21.Visible = True
    Me.quant21.VisibleIndex = 30
    Me.quant21.Width = 83
    '
    'quant22
    '
    Me.quant22.AppearanceCell.Options.UseBackColor = True
    Me.quant22.AppearanceCell.Options.UseTextOptions = True
    Me.quant22.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.quant22.AppearanceHeader.Options.UseTextOptions = True
    Me.quant22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.quant22.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.quant22.Caption = "Qtà 22 TC"
    Me.quant22.Enabled = False
    Me.quant22.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.quant22.Name = "quant22"
    Me.quant22.NTSRepositoryComboBox = Nothing
    Me.quant22.NTSRepositoryItemCheck = Nothing
    Me.quant22.NTSRepositoryItemMemo = Nothing
    Me.quant22.NTSRepositoryItemText = Nothing
    Me.quant22.OptionsColumn.AllowEdit = False
    Me.quant22.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.quant22.OptionsColumn.ReadOnly = True
    Me.quant22.OptionsFilter.AllowFilter = False
    Me.quant22.Visible = True
    Me.quant22.VisibleIndex = 31
    Me.quant22.Width = 83
    '
    'quant23
    '
    Me.quant23.AppearanceCell.Options.UseBackColor = True
    Me.quant23.AppearanceCell.Options.UseTextOptions = True
    Me.quant23.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.quant23.AppearanceHeader.Options.UseTextOptions = True
    Me.quant23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.quant23.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.quant23.Caption = "Qtà 23 TC"
    Me.quant23.Enabled = False
    Me.quant23.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.quant23.Name = "quant23"
    Me.quant23.NTSRepositoryComboBox = Nothing
    Me.quant23.NTSRepositoryItemCheck = Nothing
    Me.quant23.NTSRepositoryItemMemo = Nothing
    Me.quant23.NTSRepositoryItemText = Nothing
    Me.quant23.OptionsColumn.AllowEdit = False
    Me.quant23.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.quant23.OptionsColumn.ReadOnly = True
    Me.quant23.OptionsFilter.AllowFilter = False
    Me.quant23.Visible = True
    Me.quant23.VisibleIndex = 32
    Me.quant23.Width = 83
    '
    'quant24
    '
    Me.quant24.AppearanceCell.Options.UseBackColor = True
    Me.quant24.AppearanceCell.Options.UseTextOptions = True
    Me.quant24.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.quant24.AppearanceHeader.Options.UseTextOptions = True
    Me.quant24.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.quant24.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.quant24.Caption = "Qtà 24 TC"
    Me.quant24.Enabled = False
    Me.quant24.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.quant24.Name = "quant24"
    Me.quant24.NTSRepositoryComboBox = Nothing
    Me.quant24.NTSRepositoryItemCheck = Nothing
    Me.quant24.NTSRepositoryItemMemo = Nothing
    Me.quant24.NTSRepositoryItemText = Nothing
    Me.quant24.OptionsColumn.AllowEdit = False
    Me.quant24.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.quant24.OptionsColumn.ReadOnly = True
    Me.quant24.OptionsFilter.AllowFilter = False
    Me.quant24.Visible = True
    Me.quant24.VisibleIndex = 33
    Me.quant24.Width = 83
    '
    'lbCon
    '
    Me.lbCon.AutoSize = True
    Me.lbCon.BackColor = System.Drawing.Color.Transparent
    Me.lbCon.Location = New System.Drawing.Point(5, 12)
    Me.lbCon.Name = "lbCon"
    Me.lbCon.NTSDbField = ""
    Me.lbCon.Size = New System.Drawing.Size(36, 13)
    Me.lbCon.TabIndex = 5
    Me.lbCon.Text = "Conto"
    Me.lbCon.Tooltip = ""
    Me.lbCon.UseMnemonic = False
    '
    'lbConto
    '
    Me.lbConto.BackColor = System.Drawing.Color.Transparent
    Me.lbConto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbConto.Location = New System.Drawing.Point(63, 9)
    Me.lbConto.Name = "lbConto"
    Me.lbConto.NTSDbField = ""
    Me.lbConto.Size = New System.Drawing.Size(107, 20)
    Me.lbConto.TabIndex = 6
    Me.lbConto.TextAlign = System.Drawing.ContentAlignment.TopRight
    Me.lbConto.Tooltip = ""
    Me.lbConto.UseMnemonic = False
    '
    'lbDescrConto
    '
    Me.lbDescrConto.BackColor = System.Drawing.Color.Transparent
    Me.lbDescrConto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescrConto.Location = New System.Drawing.Point(176, 9)
    Me.lbDescrConto.Name = "lbDescrConto"
    Me.lbDescrConto.NTSDbField = ""
    Me.lbDescrConto.Size = New System.Drawing.Size(539, 20)
    Me.lbDescrConto.TabIndex = 7
    Me.lbDescrConto.Tooltip = ""
    Me.lbDescrConto.UseMnemonic = False
    '
    'lbTot
    '
    Me.lbTot.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbTot.BackColor = System.Drawing.Color.Transparent
    Me.lbTot.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbTot.Location = New System.Drawing.Point(608, 7)
    Me.lbTot.Name = "lbTot"
    Me.lbTot.NTSDbField = ""
    Me.lbTot.Size = New System.Drawing.Size(107, 20)
    Me.lbTot.TabIndex = 8
    Me.lbTot.TextAlign = System.Drawing.ContentAlignment.TopRight
    Me.lbTot.Tooltip = ""
    Me.lbTot.UseMnemonic = False
    '
    'lbTotale
    '
    Me.lbTotale.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbTotale.AutoSize = True
    Me.lbTotale.BackColor = System.Drawing.Color.Transparent
    Me.lbTotale.Location = New System.Drawing.Point(474, 10)
    Me.lbTotale.Name = "lbTotale"
    Me.lbTotale.NTSDbField = ""
    Me.lbTotale.Size = New System.Drawing.Size(117, 13)
    Me.lbTotale.TabIndex = 9
    Me.lbTotale.Text = "Totale Vendite/Acquisti"
    Me.lbTotale.Tooltip = ""
    Me.lbTotale.UseMnemonic = False
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.lbDescrConto)
    Me.pnTop.Controls.Add(Me.lbCon)
    Me.pnTop.Controls.Add(Me.lbConto)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(718, 35)
    Me.pnTop.TabIndex = 10
    Me.pnTop.Text = "NtsPanel1"
    '
    'pnGrid
    '
    Me.pnGrid.AllowDrop = True
    Me.pnGrid.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrid.Appearance.Options.UseBackColor = True
    Me.pnGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGrid.Controls.Add(Me.grGrcf)
    Me.pnGrid.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(0, 65)
    Me.pnGrid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGrid.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.NTSActiveTrasparency = True
    Me.pnGrid.Size = New System.Drawing.Size(718, 278)
    Me.pnGrid.TabIndex = 11
    Me.pnGrid.Text = "NtsPanel1"
    '
    'pbBottom
    '
    Me.pbBottom.AllowDrop = True
    Me.pbBottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pbBottom.Appearance.Options.UseBackColor = True
    Me.pbBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pbBottom.Controls.Add(Me.lbTot)
    Me.pbBottom.Controls.Add(Me.lbTotale)
    Me.pbBottom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pbBottom.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pbBottom.Location = New System.Drawing.Point(0, 343)
    Me.pbBottom.Name = "pbBottom"
    Me.pbBottom.NTSActiveTrasparency = True
    Me.pbBottom.Size = New System.Drawing.Size(718, 35)
    Me.pbBottom.TabIndex = 12
    Me.pbBottom.Text = "NtsPanel1"
    '
    'FRMMGGRCF
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(718, 378)
    Me.Controls.Add(Me.pnGrid)
    Me.Controls.Add(Me.pbBottom)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMMGGRCF"
    Me.Text = "STAMPA/VISUALIZZAZIONE SCHEDE CLIENTI/FORNITORI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grGrcf, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvGrcf, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    CType(Me.pbBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pbBottom.ResumeLayout(False)
    Me.pbBottom.PerformLayout()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      'Carico le immagini della Toolbar
      Try
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'Non gestisco l'errore: 
        'se non c'è una immagine prendo quella a design
      End Try
      grvGrcf.NTSSetParam(oMenu, oApp.Tr(Me, 128648458394375000, "Griglia visualizzazione schede Clienti/Fornitori"))
      km_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128650118980781250, "Cod. Articolo"), 0, True)
      km_magaz.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650118980937500, "Mag."), "0", 4, 0, 9999)
      ar_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128650118981093750, "Descrizione"), 0, True)
      Espr1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650118981406250, "Quantità"), "#,##0.00", 15)
      Espr2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650118981562500, "Totale Vend/Acq"), "#,##0.00", 15)
      mm_perqta.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650118981875000, "Prz/Qtà"), "#,##0.00", 15)
      km_fase.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650118982031250, "Fase"), "0", 4, 0, 9999)
      descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128650118982187500, "Descr. Fase"), 0, True)
      xx_przmedio.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650152768281250, "Prezzo Medio"), "#,##0.00", 15)
      ar_unmis.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128650165274375000, "UM"), 0, True)
      quant01.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650152768906250, "Qtà 1 TC"), "#,##0.00", 15)
      quant02.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650152769062500, "Qtà 2 TC"), "#,##0.00", 15)
      quant03.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650129891875000, "Qtà 3 TC"), "#,##0.00", 15)
      quant04.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650129892031250, "Qtà 4 TC"), "#,##0.00", 15)
      quant05.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650129892187500, "Qtà 5 TC"), "#,##0.00", 15)
      quant06.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650129892343750, "Qtà 6 TC"), "#,##0.00", 15)
      quant07.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650129892500000, "Qtà 7 TC"), "#,##0.00", 15)
      quant08.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650129892656250, "Qtà 8 TC"), "#,##0.00", 15)
      quant09.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650129892812500, "Qtà 9 TC"), "#,##0.00", 15)
      quant10.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650118983750000, "Qtà 10 TC"), "#,##0.00", 15)
      quant11.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650118983906250, "Qtà 11 TC"), "#,##0.00", 15)
      quant12.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650118984062500, "Qtà 12 TC"), "#,##0.00", 15)
      quant13.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650118984218750, "Qtà 13 TC"), "#,##0.00", 15)
      quant14.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650118984375000, "Qtà 14 TC"), "#,##0.00", 15)
      quant15.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650118984531250, "Qtà 15 TC"), "#,##0.00", 15)
      quant16.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650118984687500, "Qtà 16 TC"), "#,##0.00", 15)
      quant17.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650118984843750, "Qtà 17 TC"), "#,##0.00", 15)
      quant18.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650118985000000, "Qtà 18 TC"), "#,##0.00", 15)
      quant19.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650118985156250, "Qtà 19 TC"), "#,##0.00", 15)
      quant20.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650118985312500, "Qtà 20 TC"), "#,##0.00", 15)
      quant21.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650118985468750, "Qtà 21 TC"), "#,##0.00", 15)
      quant22.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650118985625000, "Qtà 22 TC"), "#,##0.00", 15)
      quant23.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650118985781250, "Qtà 23 TC"), "#,##0.00", 15)
      quant24.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128650118985937500, "Qtà 24 TC"), "#,##0.00", 15)

      grvGrcf.NTSAllowDelete = False
      grvGrcf.NTSAllowInsert = False
      grvGrcf.NTSAllowUpdate = False

      '-------------------------------------------------
      'chiamo lo script per inizializzare i controlli caricati con source ext
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

#Region "Form"
  Public Overridable Sub FRMMGGRCF_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcGrcf.Dispose()
      If Not dsGrcf Is Nothing Then dsGrcf.Dispose()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGGRCF_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim lTot As Decimal = 0

    Try
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      oCleSchc.strWhereCRM = strGrcfWhereCRM
      If oCleSchc.ApriGrcf(dsGrcf, lTot, lbConto.Text, lbDescrConto.Text) = False Then
        Me.Close()
        Return
      End If
      '------------------------------------------------------------------------------------------------------------
      dcGrcf.DataSource = dsGrcf.Tables("KEYMAG").Copy
      dsGrcf.AcceptChanges()
      grGrcf.DataSource = dcGrcf
      lbTot.Text = lTot.ToString(oApp.FormatImporti)
      '------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione Friendly, rende SEMPRE non visibili alcune colonne
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = True Then
        km_fase.Visible = False
        descr.Visible = False
        quant01.Visible = False
        quant02.Visible = False
        quant03.Visible = False
        quant04.Visible = False
        quant05.Visible = False
        quant06.Visible = False
        quant07.Visible = False
        quant08.Visible = False
        quant09.Visible = False
        quant10.Visible = False
        quant11.Visible = False
        quant12.Visible = False
        quant13.Visible = False
        quant14.Visible = False
        quant15.Visible = False
        quant16.Visible = False
        quant17.Visible = False
        quant18.Visible = False
        quant19.Visible = False
        quant20.Visible = False
        quant21.Visible = False
        quant22.Visible = False
        quant23.Visible = False
        quant24.Visible = False
      End If
      If CLN__STD.IsBis Then tlbStampaVideo.Visible = False
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMMGGRCF_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Toolbar"
  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    Try
      lPosition = 0
      CambiaConto(-999999)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbPrecedente_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrecedente.ItemClick
    Try
      lPosition -= 1
      CambiaConto(-1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSuccessivo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSuccessivo.ItemClick
    Try
      lPosition += 1
      CambiaConto(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbUltimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbUltimo.ItemClick
    Try
      lPosition = 999999
      CambiaConto(999999)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Dim strHeader As String = ""
    Dim strFooter As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strHeader = lbCon.Text & "".PadLeft(1) & lbConto.Text & "".PadLeft(1) & lbDescrConto.Text
      strFooter = lbTotale.Text & "".PadLeft(1) & lbTot.Text
      '--------------------------------------------------------------------------------------------------------------      
      grvGrcf.NTSPrintPreview(strHeader, strFooter)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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
#End Region

  Public Overridable Sub CambiaConto(ByVal lModificatore As Integer)
    Dim dTot As Decimal = 0

    Try
      '--------------------------------------------------------------------------------------------------------------
      oCleSchc.CambiaConto(dsGrcf, lbConto.Text, lbDescrConto.Text, dTot, lModificatore, oCleSchc.bModuloCrm, _
        oCleSchc.bIsCRMUser, oCleSchc.bIs15, oCleSchc.nCodcageAccdito, oCleSchc.lIITtsubqcrm, lPosition)
      dcGrcf.DataSource = dsGrcf.Tables("KEYMAG").Copy
      dsGrcf.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      grGrcf.DataSource = dcGrcf
      '--------------------------------------------------------------------------------------------------------------
      lbTot.Text = dTot.ToString(oApp.FormatImporti)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class