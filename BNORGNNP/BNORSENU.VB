Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORSENU

#Region "Variabili"
  Public oCleGnnp As CLEORGNNP
  Public oCallParams As CLE__CLDP
  Public dsSenu As New DataSet
  Public dcSenu As New BindingSource()
  Private components As System.ComponentModel.IContainer

  Public bAnnulla As Boolean

  Public WithEvents grSenu As NTSInformatica.NTSGrid
  Public WithEvents grvSenu As NTSInformatica.NTSGridView
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
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem

  Public WithEvents instid As NTSInformatica.NTSGridColumn
  Public WithEvents tt_moserie As NTSInformatica.NTSGridColumn
  Public WithEvents tt_monumord As NTSInformatica.NTSGridColumn
  Public WithEvents codditt As NTSInformatica.NTSGridColumn
  Public WithEvents tt_olprogr As NTSInformatica.NTSGridColumn
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents edSerie As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAnno As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSerie As NTSInformatica.NTSLabel
  Public WithEvents lbAnno As NTSInformatica.NTSLabel
  Public WithEvents pnFill As NTSInformatica.NTSPanel
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


    Return True
  End Function
  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMORSENU))
    Me.grSenu = New NTSInformatica.NTSGrid
    Me.grvSenu = New NTSInformatica.NTSGridView
    Me.instid = New NTSInformatica.NTSGridColumn
    Me.tt_moserie = New NTSInformatica.NTSGridColumn
    Me.tt_monumord = New NTSInformatica.NTSGridColumn
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.tt_olprogr = New NTSInformatica.NTSGridColumn
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.lbSerie = New NTSInformatica.NTSLabel
    Me.lbAnno = New NTSInformatica.NTSLabel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.edSerie = New NTSInformatica.NTSTextBoxStr
    Me.edAnno = New NTSInformatica.NTSTextBoxNum
    Me.pnFill = New NTSInformatica.NTSPanel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grSenu, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvSenu, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnFill, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFill.SuspendLayout()
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
    'frmAuto
    '
    Me.frmAuto.Appearance.BackColor = System.Drawing.Color.Black
    Me.frmAuto.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmAuto.Appearance.Options.UseBackColor = True
    Me.frmAuto.Appearance.Options.UseImage = True
    '
    'grSenu
    '
    Me.grSenu.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grSenu.EmbeddedNavigator.Name = ""
    Me.grSenu.Location = New System.Drawing.Point(0, 0)
    Me.grSenu.MainView = Me.grvSenu
    Me.grSenu.Name = "grSenu"
    Me.grSenu.Size = New System.Drawing.Size(305, 264)
    Me.grSenu.TabIndex = 0
    Me.grSenu.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvSenu})
    '
    'grvSenu
    '
    Me.grvSenu.ActiveFilterEnabled = False
    Me.grvSenu.Appearance.FocusedCell.BackColor = System.Drawing.Color.FloralWhite
    Me.grvSenu.Appearance.FocusedCell.Options.UseBackColor = True
    Me.grvSenu.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.instid, Me.tt_moserie, Me.tt_monumord, Me.codditt, Me.tt_olprogr})
    Me.grvSenu.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvSenu.Enabled = True
    Me.grvSenu.GridControl = Me.grSenu
    Me.grvSenu.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvSenu.MinRowHeight = 14
    Me.grvSenu.Name = "grvSenu"
    Me.grvSenu.NTSAllowDelete = True
    Me.grvSenu.NTSAllowInsert = True
    Me.grvSenu.NTSAllowUpdate = True
    Me.grvSenu.NTSMenuContext = Nothing
    Me.grvSenu.OptionsCustomization.AllowRowSizing = True
    Me.grvSenu.OptionsFilter.AllowFilterEditor = False
    Me.grvSenu.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvSenu.OptionsNavigation.UseTabKey = False
    Me.grvSenu.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvSenu.OptionsView.ColumnAutoWidth = False
    Me.grvSenu.OptionsView.EnableAppearanceEvenRow = True
    Me.grvSenu.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvSenu.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvSenu.OptionsView.ShowGroupPanel = False
    Me.grvSenu.RowHeight = 16
    '
    'instid
    '
    Me.instid.AppearanceCell.Options.UseBackColor = True
    Me.instid.AppearanceCell.Options.UseTextOptions = True
    Me.instid.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.instid.Caption = "instid"
    Me.instid.Enabled = False
    Me.instid.FieldName = "instid"
    Me.instid.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.instid.Name = "instid"
    Me.instid.NTSRepositoryComboBox = Nothing
    Me.instid.NTSRepositoryItemCheck = Nothing
    Me.instid.NTSRepositoryItemMemo = Nothing
    Me.instid.NTSRepositoryItemText = Nothing
    Me.instid.OptionsColumn.AllowEdit = False
    Me.instid.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.instid.OptionsColumn.ReadOnly = True
    Me.instid.OptionsFilter.AllowFilter = False
    '
    'tt_moserie
    '
    Me.tt_moserie.AppearanceCell.Options.UseBackColor = True
    Me.tt_moserie.AppearanceCell.Options.UseTextOptions = True
    Me.tt_moserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_moserie.Caption = "Serie"
    Me.tt_moserie.Enabled = True
    Me.tt_moserie.FieldName = "tt_moserie"
    Me.tt_moserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_moserie.Name = "tt_moserie"
    Me.tt_moserie.NTSRepositoryComboBox = Nothing
    Me.tt_moserie.NTSRepositoryItemCheck = Nothing
    Me.tt_moserie.NTSRepositoryItemMemo = Nothing
    Me.tt_moserie.NTSRepositoryItemText = Nothing
    Me.tt_moserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_moserie.OptionsFilter.AllowFilter = False
    Me.tt_moserie.Visible = True
    Me.tt_moserie.VisibleIndex = 0
    '
    'tt_monumord
    '
    Me.tt_monumord.AppearanceCell.Options.UseBackColor = True
    Me.tt_monumord.AppearanceCell.Options.UseTextOptions = True
    Me.tt_monumord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_monumord.Caption = "Numero Ordine"
    Me.tt_monumord.Enabled = True
    Me.tt_monumord.FieldName = "tt_monumord"
    Me.tt_monumord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_monumord.Name = "tt_monumord"
    Me.tt_monumord.NTSRepositoryComboBox = Nothing
    Me.tt_monumord.NTSRepositoryItemCheck = Nothing
    Me.tt_monumord.NTSRepositoryItemMemo = Nothing
    Me.tt_monumord.NTSRepositoryItemText = Nothing
    Me.tt_monumord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_monumord.OptionsFilter.AllowFilter = False
    Me.tt_monumord.Visible = True
    Me.tt_monumord.VisibleIndex = 1
    '
    'codditt
    '
    Me.codditt.AppearanceCell.Options.UseBackColor = True
    Me.codditt.AppearanceCell.Options.UseTextOptions = True
    Me.codditt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.codditt.Caption = "codditt"
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
    'tt_olprogr
    '
    Me.tt_olprogr.AppearanceCell.Options.UseBackColor = True
    Me.tt_olprogr.AppearanceCell.Options.UseTextOptions = True
    Me.tt_olprogr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_olprogr.Caption = "tt_olprogr"
    Me.tt_olprogr.Enabled = False
    Me.tt_olprogr.FieldName = "tt_olprogr"
    Me.tt_olprogr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_olprogr.Name = "tt_olprogr"
    Me.tt_olprogr.NTSRepositoryComboBox = Nothing
    Me.tt_olprogr.NTSRepositoryItemCheck = Nothing
    Me.tt_olprogr.NTSRepositoryItemMemo = Nothing
    Me.tt_olprogr.NTSRepositoryItemText = Nothing
    Me.tt_olprogr.OptionsColumn.AllowEdit = False
    Me.tt_olprogr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_olprogr.OptionsColumn.ReadOnly = True
    Me.tt_olprogr.OptionsFilter.AllowFilter = False
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 16
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbNuovo.GlyphPath = ""
    Me.tlbNuovo.Id = 0
    Me.tlbNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2)
    Me.tlbNuovo.Name = "tlbNuovo"
    Me.tlbNuovo.Visible = True
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.GlyphPath = ""
    Me.tlbSalva.Id = 1
    Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.GlyphPath = ""
    Me.tlbRipristina.Id = 3
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.GlyphPath = ""
    Me.tlbCancella.Id = 2
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 9
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 10
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.lbSerie)
    Me.pnTop.Controls.Add(Me.lbAnno)
    Me.pnTop.Controls.Add(Me.cmdAnnulla)
    Me.pnTop.Controls.Add(Me.cmdOk)
    Me.pnTop.Controls.Add(Me.edSerie)
    Me.pnTop.Controls.Add(Me.edAnno)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(305, 72)
    Me.pnTop.TabIndex = 4
    Me.pnTop.Text = "NtsPanel1"
    '
    'lbSerie
    '
    Me.lbSerie.AutoSize = True
    Me.lbSerie.BackColor = System.Drawing.Color.Transparent
    Me.lbSerie.Location = New System.Drawing.Point(13, 43)
    Me.lbSerie.Name = "lbSerie"
    Me.lbSerie.NTSDbField = ""
    Me.lbSerie.Size = New System.Drawing.Size(31, 13)
    Me.lbSerie.TabIndex = 5
    Me.lbSerie.Text = "Serie"
    Me.lbSerie.Tooltip = ""
    Me.lbSerie.UseMnemonic = False
    '
    'lbAnno
    '
    Me.lbAnno.AutoSize = True
    Me.lbAnno.BackColor = System.Drawing.Color.Transparent
    Me.lbAnno.Location = New System.Drawing.Point(13, 17)
    Me.lbAnno.Name = "lbAnno"
    Me.lbAnno.NTSDbField = ""
    Me.lbAnno.Size = New System.Drawing.Size(32, 13)
    Me.lbAnno.TabIndex = 4
    Me.lbAnno.Text = "Anno"
    Me.lbAnno.Tooltip = ""
    Me.lbAnno.UseMnemonic = False
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(221, 40)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(75, 25)
    Me.cmdAnnulla.TabIndex = 3
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImagePath = ""
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(221, 9)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.NTSContextMenu = Nothing
    Me.cmdOk.Size = New System.Drawing.Size(75, 25)
    Me.cmdOk.TabIndex = 2
    Me.cmdOk.Text = "&Conferma"
    '
    'edSerie
    '
    Me.edSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSerie.Location = New System.Drawing.Point(73, 40)
    Me.edSerie.Name = "edSerie"
    Me.edSerie.NTSDbField = ""
    Me.edSerie.NTSForzaVisZoom = False
    Me.edSerie.NTSOldValue = ""
    Me.edSerie.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSerie.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSerie.Properties.AutoHeight = False
    Me.edSerie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSerie.Properties.MaxLength = 65536
    Me.edSerie.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSerie.Size = New System.Drawing.Size(45, 20)
    Me.edSerie.TabIndex = 1
    '
    'edAnno
    '
    Me.edAnno.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnno.Location = New System.Drawing.Point(73, 14)
    Me.edAnno.Name = "edAnno"
    Me.edAnno.NTSDbField = ""
    Me.edAnno.NTSFormat = "0"
    Me.edAnno.NTSForzaVisZoom = False
    Me.edAnno.NTSOldValue = ""
    Me.edAnno.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnno.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnno.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnno.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnno.Properties.AutoHeight = False
    Me.edAnno.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnno.Properties.MaxLength = 65536
    Me.edAnno.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnno.Size = New System.Drawing.Size(91, 20)
    Me.edAnno.TabIndex = 0
    '
    'pnFill
    '
    Me.pnFill.AllowDrop = True
    Me.pnFill.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnFill.Appearance.Options.UseBackColor = True
    Me.pnFill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnFill.Controls.Add(Me.grSenu)
    Me.pnFill.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnFill.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnFill.Location = New System.Drawing.Point(0, 102)
    Me.pnFill.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnFill.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnFill.Name = "pnFill"
    Me.pnFill.NTSActiveTrasparency = True
    Me.pnFill.Size = New System.Drawing.Size(305, 264)
    Me.pnFill.TabIndex = 5
    Me.pnFill.Text = "NtsPanel2"
    '
    'FRMORSENU
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(305, 366)
    Me.Controls.Add(Me.pnFill)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMORSENU"
    Me.NTSLastControlFocussed = Me.grSenu
    Me.Text = "SELEZIONA PER NUMERO ORDINE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grSenu, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvSenu, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnFill, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFill.ResumeLayout(False)
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
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        '  'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvSenu.NTSSetParam(oMenu, oApp.Tr(Me, 128563535825894188, "Seleziona per numero ordine"))
      instid.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128720974022818676, "instid"), "0", 9, 0, 999999999)
      tt_moserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128720974022974939, "Serie"), CLN__STD.SerieMaxLen, False)
      tt_monumord.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128720974023131202, "Numero Ordine"), "0", 9, 0, 999999999)
      codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128720974023287465, "codditt"), 12, False)
      tt_olprogr.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128720974023443728, "tt_olprogr"), "0", 9, 0, 999999999)

      edSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128720974026412725, "Serie"), CLN__STD.SerieMaxLen)
      edAnno.NTSSetParam(oMenu, oApp.Tr(Me, 128720974026568988, "Anno"), "0", 4, 1900, 2099)

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
  Public Overridable Sub InitEntity(ByRef cleGnnp As CLEORGNNP)
    oCleGnnp = cleGnnp
    AddHandler oCleGnnp.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvSenu.NTSSalvaRigaCorrente(dcSenu, oCleGnnp.SenuRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleGnnp.SenuSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleGnnp.SenuRipristina(dcSenu.Position, dcSenu.Filter)
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
  Public Overridable Sub FRMORSENU_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      edAnno.Text = NTSCStr(oCleGnnp.nSenuAnno)
      bAnnulla = True
      oCleGnnp.lProg = 0

      edSerie.Visible = False
      lbSerie.Visible = False

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleGnnp.SenuApri(DittaCorrente, dsSenu) Then Me.Close()

      dcSenu.DataSource = dsSenu.Tables("TTOLTOMO")
      grSenu.DataSource = dcSenu

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
  Public Overridable Sub FRMORSENU_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcSenu.Dispose()
      dsSenu.Dispose()
    Catch
    End Try
  End Sub
  Public Overridable Sub FRMORSENU_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
    If bAnnulla = True Then oCleGnnp.nSenuAnno = 0
  End Sub

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      Me.ValidaLastControl()

      If Not Salva() Then Return

      If dsSenu.Tables("TTOLTOMO").Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128720981405556477, "Indicare almeno un Numero Ordine."))
        Exit Sub
      End If

      bAnnulla = False
      oCleGnnp.nSenuAnno = NTSCInt(edAnno.Text)

      Me.Close()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      oCleGnnp.nSenuAnno = 0
      bAnnulla = True

      Me.Close()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvSenu.NTSNuovo()
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
      If Not grvSenu.NTSDeleteRigaCorrente(dcSenu, True) Then Return
      oCleGnnp.SenuSalva(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvSenu.NTSRipristinaRigaCorrenteBefore(dcSenu, True) Then Return
      oCleGnnp.SenuRipristina(dcSenu.Position, dcSenu.Filter)
      grvSenu.NTSRipristinaRigaCorrenteAfter()
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
  Public Overridable Sub grvSenu_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvSenu.NTSBeforeRowUpdate
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

  Public Overridable Sub grvSenu_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvSenu.NTSFocusedRowChanged
    'blocco le colonne non modificabili
    Dim dtrT As DataRow = Nothing
    Try
      If oCleGnnp Is Nothing Then Return

      dtrT = grvSenu.NTSGetCurrentDataRow
      '------------------------------------
      'sono su una nuova riga
      If dtrT Is Nothing Then
        tt_moserie.Enabled = True
        tt_monumord.Enabled = True
        Return
      End If

      If NTSCInt(dtrT!tt_olprogr) <> 0 Then
        tt_moserie.Enabled = False
        tt_monumord.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

End Class