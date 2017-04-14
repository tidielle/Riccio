Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVELAVC
  Public oCleBoll As CLEVEBOLL
  Public oCallParams As CLE__CLDP
  Public dcLavc As BindingSource = New BindingSource()
  Public frmParent As FRMVEBOLL                           'FORM PARENT: non passato da form _T, _X, _F, ecc

  Private components As System.ComponentModel.IContainer

  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents grRighe As NTSInformatica.NTSGrid
  Public WithEvents grvRighe As NTSInformatica.NTSGridView
  Public WithEvents lce_rigaa As NTSInformatica.NTSGridColumn
  Public WithEvents lce_codart As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codart As NTSInformatica.NTSGridColumn
  Public WithEvents lce_codlavo As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codlavo As NTSInformatica.NTSGridColumn
  Public WithEvents lce_oranno As NTSInformatica.NTSGridColumn
  Public WithEvents lce_orserie As NTSInformatica.NTSGridColumn
  Public WithEvents lce_ornum As NTSInformatica.NTSGridColumn
  Public WithEvents lce_orriga As NTSInformatica.NTSGridColumn
  Public WithEvents lce_orfase As NTSInformatica.NTSGridColumn
  Public WithEvents lce_qtaes As NTSInformatica.NTSGridColumn
  Public WithEvents lce_qtascart As NTSInformatica.NTSGridColumn
  Public WithEvents lce_flevas As NTSInformatica.NTSGridColumn
  Public WithEvents xx_tempatte As NTSInformatica.NTSGridColumn
  Public WithEvents xx_tempese As NTSInformatica.NTSGridColumn
  Public WithEvents lce_codcent As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codcent As NTSInformatica.NTSGridColumn
  Public WithEvents lce_start As NTSInformatica.NTSGridColumn
  Public WithEvents lce_stop As NTSInformatica.NTSGridColumn
  Public WithEvents lce_cmacora As NTSInformatica.NTSGridColumn
  Public WithEvents lce_pagaora As NTSInformatica.NTSGridColumn
  Public WithEvents lce_cmacoratt As NTSInformatica.NTSGridColumn
  Public WithEvents lce_pagaoratt As NTSInformatica.NTSGridColumn
  Public WithEvents lce_valore As NTSInformatica.NTSGridColumn
  Public WithEvents lce_valmo As NTSInformatica.NTSGridColumn
  Public WithEvents lce_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents xx_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents lce_subcommeca As NTSInformatica.NTSGridColumn
  Public WithEvents lce_controp As NTSInformatica.NTSGridColumn
  Public WithEvents xx_controp As NTSInformatica.NTSGridColumn
  Public WithEvents lce_contocontr As NTSInformatica.NTSGridColumn
  Public WithEvents lce_codcena As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codcena As NTSInformatica.NTSGridColumn
  Public WithEvents lce_codcfam As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codcfam As NTSInformatica.NTSGridColumn
  Public WithEvents lce_ganno As NTSInformatica.NTSGridColumn
  Public WithEvents lce_gserie As NTSInformatica.NTSGridColumn
  Public WithEvents lce_gnumdoc As NTSInformatica.NTSGridColumn
  Public WithEvents lce_grigaa As NTSInformatica.NTSGridColumn
  Public WithEvents lce_note As NTSInformatica.NTSGridColumn
  Public WithEvents lce_valorev As NTSInformatica.NTSGridColumn
  Public WithEvents lce_codcope As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codcope As NTSInformatica.NTSGridColumn
  Public WithEvents xx_tempattem As NTSInformatica.NTSGridColumn
  Public WithEvents xx_tempesem As NTSInformatica.NTSGridColumn
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem



  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMVELAVC))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grRighe = New NTSInformatica.NTSGrid
    Me.grvRighe = New NTSInformatica.NTSGridView
    Me.lce_rigaa = New NTSInformatica.NTSGridColumn
    Me.lce_codart = New NTSInformatica.NTSGridColumn
    Me.xx_codart = New NTSInformatica.NTSGridColumn
    Me.lce_codcent = New NTSInformatica.NTSGridColumn
    Me.xx_codcent = New NTSInformatica.NTSGridColumn
    Me.lce_codlavo = New NTSInformatica.NTSGridColumn
    Me.xx_codlavo = New NTSInformatica.NTSGridColumn
    Me.lce_qtaes = New NTSInformatica.NTSGridColumn
    Me.lce_qtascart = New NTSInformatica.NTSGridColumn
    Me.xx_tempatte = New NTSInformatica.NTSGridColumn
    Me.xx_tempese = New NTSInformatica.NTSGridColumn
    Me.lce_valore = New NTSInformatica.NTSGridColumn
    Me.lce_valmo = New NTSInformatica.NTSGridColumn
    Me.lce_oranno = New NTSInformatica.NTSGridColumn
    Me.lce_orserie = New NTSInformatica.NTSGridColumn
    Me.lce_ornum = New NTSInformatica.NTSGridColumn
    Me.lce_orriga = New NTSInformatica.NTSGridColumn
    Me.lce_orfase = New NTSInformatica.NTSGridColumn
    Me.lce_flevas = New NTSInformatica.NTSGridColumn
    Me.lce_start = New NTSInformatica.NTSGridColumn
    Me.lce_stop = New NTSInformatica.NTSGridColumn
    Me.lce_cmacora = New NTSInformatica.NTSGridColumn
    Me.lce_pagaora = New NTSInformatica.NTSGridColumn
    Me.lce_cmacoratt = New NTSInformatica.NTSGridColumn
    Me.lce_pagaoratt = New NTSInformatica.NTSGridColumn
    Me.lce_commeca = New NTSInformatica.NTSGridColumn
    Me.xx_commeca = New NTSInformatica.NTSGridColumn
    Me.lce_subcommeca = New NTSInformatica.NTSGridColumn
    Me.lce_controp = New NTSInformatica.NTSGridColumn
    Me.xx_controp = New NTSInformatica.NTSGridColumn
    Me.lce_contocontr = New NTSInformatica.NTSGridColumn
    Me.lce_codcena = New NTSInformatica.NTSGridColumn
    Me.xx_codcena = New NTSInformatica.NTSGridColumn
    Me.lce_codcfam = New NTSInformatica.NTSGridColumn
    Me.xx_codcfam = New NTSInformatica.NTSGridColumn
    Me.lce_ganno = New NTSInformatica.NTSGridColumn
    Me.lce_gserie = New NTSInformatica.NTSGridColumn
    Me.lce_gnumdoc = New NTSInformatica.NTSGridColumn
    Me.lce_grigaa = New NTSInformatica.NTSGridColumn
    Me.lce_note = New NTSInformatica.NTSGridColumn
    Me.lce_valorev = New NTSInformatica.NTSGridColumn
    Me.lce_codcope = New NTSInformatica.NTSGridColumn
    Me.xx_codcope = New NTSInformatica.NTSGridColumn
    Me.xx_tempattem = New NTSInformatica.NTSGridColumn
    Me.xx_tempesem = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grRighe, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvRighe, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbEsci, Me.tlbZoom})
    Me.NtsBarManager1.MaxItemId = 22
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci, True)})
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
    Me.tlbZoom.Id = 13
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 12
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'grRighe
    '
    Me.grRighe.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grRighe.EmbeddedNavigator.Name = ""
    Me.grRighe.Location = New System.Drawing.Point(0, 26)
    Me.grRighe.MainView = Me.grvRighe
    Me.grRighe.Name = "grRighe"
    Me.grRighe.Size = New System.Drawing.Size(740, 416)
    Me.grRighe.TabIndex = 6
    Me.grRighe.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvRighe})
    '
    'grvRighe
    '
    Me.grvRighe.ActiveFilterEnabled = False
    Me.grvRighe.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.lce_rigaa, Me.lce_codart, Me.xx_codart, Me.lce_codcent, Me.xx_codcent, Me.lce_codlavo, Me.xx_codlavo, Me.lce_qtaes, Me.lce_qtascart, Me.xx_tempatte, Me.xx_tempese, Me.lce_valore, Me.lce_valmo, Me.lce_oranno, Me.lce_orserie, Me.lce_ornum, Me.lce_orriga, Me.lce_orfase, Me.lce_flevas, Me.lce_start, Me.lce_stop, Me.lce_cmacora, Me.lce_pagaora, Me.lce_cmacoratt, Me.lce_pagaoratt, Me.lce_commeca, Me.xx_commeca, Me.lce_subcommeca, Me.lce_controp, Me.xx_controp, Me.lce_contocontr, Me.lce_codcena, Me.xx_codcena, Me.lce_codcfam, Me.xx_codcfam, Me.lce_ganno, Me.lce_gserie, Me.lce_gnumdoc, Me.lce_grigaa, Me.lce_note, Me.lce_valorev, Me.lce_codcope, Me.xx_codcope, Me.xx_tempattem, Me.xx_tempesem})
    Me.grvRighe.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvRighe.Enabled = True
    Me.grvRighe.GridControl = Me.grRighe
    Me.grvRighe.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvRighe.Name = "grvRighe"
    Me.grvRighe.NTSAllowDelete = True
    Me.grvRighe.NTSAllowInsert = True
    Me.grvRighe.NTSAllowUpdate = True
    Me.grvRighe.NTSMenuContext = Nothing
    Me.grvRighe.OptionsCustomization.AllowRowSizing = True
    Me.grvRighe.OptionsFilter.AllowFilterEditor = False
    Me.grvRighe.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvRighe.OptionsNavigation.UseTabKey = False
    Me.grvRighe.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvRighe.OptionsView.ColumnAutoWidth = False
    Me.grvRighe.OptionsView.EnableAppearanceEvenRow = True
    Me.grvRighe.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvRighe.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvRighe.OptionsView.ShowGroupPanel = False
    Me.grvRighe.RowHeight = 16
    '
    'lce_rigaa
    '
    Me.lce_rigaa.AppearanceCell.Options.UseBackColor = True
    Me.lce_rigaa.AppearanceCell.Options.UseTextOptions = True
    Me.lce_rigaa.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_rigaa.Caption = "Riga"
    Me.lce_rigaa.Enabled = True
    Me.lce_rigaa.FieldName = "lce_rigaa"
    Me.lce_rigaa.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_rigaa.Name = "lce_rigaa"
    Me.lce_rigaa.NTSRepositoryComboBox = Nothing
    Me.lce_rigaa.NTSRepositoryItemCheck = Nothing
    Me.lce_rigaa.NTSRepositoryItemMemo = Nothing
    Me.lce_rigaa.NTSRepositoryItemText = Nothing
    Me.lce_rigaa.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_rigaa.OptionsFilter.AllowFilter = False
    Me.lce_rigaa.Visible = True
    Me.lce_rigaa.VisibleIndex = 0
    '
    'lce_codart
    '
    Me.lce_codart.AppearanceCell.Options.UseBackColor = True
    Me.lce_codart.AppearanceCell.Options.UseTextOptions = True
    Me.lce_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_codart.Caption = "Articolo"
    Me.lce_codart.Enabled = False
    Me.lce_codart.FieldName = "lce_codart"
    Me.lce_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_codart.Name = "lce_codart"
    Me.lce_codart.NTSRepositoryComboBox = Nothing
    Me.lce_codart.NTSRepositoryItemCheck = Nothing
    Me.lce_codart.NTSRepositoryItemMemo = Nothing
    Me.lce_codart.NTSRepositoryItemText = Nothing
    Me.lce_codart.OptionsColumn.AllowEdit = False
    Me.lce_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_codart.OptionsColumn.ReadOnly = True
    Me.lce_codart.OptionsFilter.AllowFilter = False
    '
    'xx_codart
    '
    Me.xx_codart.AppearanceCell.Options.UseBackColor = True
    Me.xx_codart.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codart.Caption = "Descr. art."
    Me.xx_codart.Enabled = False
    Me.xx_codart.FieldName = "xx_codart"
    Me.xx_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codart.Name = "xx_codart"
    Me.xx_codart.NTSRepositoryComboBox = Nothing
    Me.xx_codart.NTSRepositoryItemCheck = Nothing
    Me.xx_codart.NTSRepositoryItemMemo = Nothing
    Me.xx_codart.NTSRepositoryItemText = Nothing
    Me.xx_codart.OptionsColumn.AllowEdit = False
    Me.xx_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codart.OptionsColumn.ReadOnly = True
    Me.xx_codart.OptionsFilter.AllowFilter = False
    '
    'lce_codcent
    '
    Me.lce_codcent.AppearanceCell.Options.UseBackColor = True
    Me.lce_codcent.AppearanceCell.Options.UseTextOptions = True
    Me.lce_codcent.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_codcent.Caption = "Centro"
    Me.lce_codcent.Enabled = True
    Me.lce_codcent.FieldName = "lce_codcent"
    Me.lce_codcent.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_codcent.Name = "lce_codcent"
    Me.lce_codcent.NTSRepositoryComboBox = Nothing
    Me.lce_codcent.NTSRepositoryItemCheck = Nothing
    Me.lce_codcent.NTSRepositoryItemMemo = Nothing
    Me.lce_codcent.NTSRepositoryItemText = Nothing
    Me.lce_codcent.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_codcent.OptionsFilter.AllowFilter = False
    Me.lce_codcent.Visible = True
    Me.lce_codcent.VisibleIndex = 1
    '
    'xx_codcent
    '
    Me.xx_codcent.AppearanceCell.Options.UseBackColor = True
    Me.xx_codcent.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codcent.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codcent.Caption = "Descr. centro"
    Me.xx_codcent.Enabled = False
    Me.xx_codcent.FieldName = "xx_codcent"
    Me.xx_codcent.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codcent.Name = "xx_codcent"
    Me.xx_codcent.NTSRepositoryComboBox = Nothing
    Me.xx_codcent.NTSRepositoryItemCheck = Nothing
    Me.xx_codcent.NTSRepositoryItemMemo = Nothing
    Me.xx_codcent.NTSRepositoryItemText = Nothing
    Me.xx_codcent.OptionsColumn.AllowEdit = False
    Me.xx_codcent.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codcent.OptionsColumn.ReadOnly = True
    Me.xx_codcent.OptionsFilter.AllowFilter = False
    Me.xx_codcent.Visible = True
    Me.xx_codcent.VisibleIndex = 2
    '
    'lce_codlavo
    '
    Me.lce_codlavo.AppearanceCell.Options.UseBackColor = True
    Me.lce_codlavo.AppearanceCell.Options.UseTextOptions = True
    Me.lce_codlavo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_codlavo.Caption = "Lavoraz."
    Me.lce_codlavo.Enabled = True
    Me.lce_codlavo.FieldName = "lce_codlavo"
    Me.lce_codlavo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_codlavo.Name = "lce_codlavo"
    Me.lce_codlavo.NTSRepositoryComboBox = Nothing
    Me.lce_codlavo.NTSRepositoryItemCheck = Nothing
    Me.lce_codlavo.NTSRepositoryItemMemo = Nothing
    Me.lce_codlavo.NTSRepositoryItemText = Nothing
    Me.lce_codlavo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_codlavo.OptionsFilter.AllowFilter = False
    Me.lce_codlavo.Visible = True
    Me.lce_codlavo.VisibleIndex = 3
    '
    'xx_codlavo
    '
    Me.xx_codlavo.AppearanceCell.Options.UseBackColor = True
    Me.xx_codlavo.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codlavo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codlavo.Caption = "Descr. lav."
    Me.xx_codlavo.Enabled = False
    Me.xx_codlavo.FieldName = "xx_codlavo"
    Me.xx_codlavo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codlavo.Name = "xx_codlavo"
    Me.xx_codlavo.NTSRepositoryComboBox = Nothing
    Me.xx_codlavo.NTSRepositoryItemCheck = Nothing
    Me.xx_codlavo.NTSRepositoryItemMemo = Nothing
    Me.xx_codlavo.NTSRepositoryItemText = Nothing
    Me.xx_codlavo.OptionsColumn.AllowEdit = False
    Me.xx_codlavo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codlavo.OptionsColumn.ReadOnly = True
    Me.xx_codlavo.OptionsFilter.AllowFilter = False
    Me.xx_codlavo.Visible = True
    Me.xx_codlavo.VisibleIndex = 4
    '
    'lce_qtaes
    '
    Me.lce_qtaes.AppearanceCell.Options.UseBackColor = True
    Me.lce_qtaes.AppearanceCell.Options.UseTextOptions = True
    Me.lce_qtaes.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_qtaes.Caption = "Qta eseg."
    Me.lce_qtaes.Enabled = True
    Me.lce_qtaes.FieldName = "lce_qtaes"
    Me.lce_qtaes.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_qtaes.Name = "lce_qtaes"
    Me.lce_qtaes.NTSRepositoryComboBox = Nothing
    Me.lce_qtaes.NTSRepositoryItemCheck = Nothing
    Me.lce_qtaes.NTSRepositoryItemMemo = Nothing
    Me.lce_qtaes.NTSRepositoryItemText = Nothing
    Me.lce_qtaes.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_qtaes.OptionsFilter.AllowFilter = False
    Me.lce_qtaes.Visible = True
    Me.lce_qtaes.VisibleIndex = 5
    '
    'lce_qtascart
    '
    Me.lce_qtascart.AppearanceCell.Options.UseBackColor = True
    Me.lce_qtascart.AppearanceCell.Options.UseTextOptions = True
    Me.lce_qtascart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_qtascart.Caption = "Qta scart."
    Me.lce_qtascart.Enabled = True
    Me.lce_qtascart.FieldName = "lce_qtascart"
    Me.lce_qtascart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_qtascart.Name = "lce_qtascart"
    Me.lce_qtascart.NTSRepositoryComboBox = Nothing
    Me.lce_qtascart.NTSRepositoryItemCheck = Nothing
    Me.lce_qtascart.NTSRepositoryItemMemo = Nothing
    Me.lce_qtascart.NTSRepositoryItemText = Nothing
    Me.lce_qtascart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_qtascart.OptionsFilter.AllowFilter = False
    Me.lce_qtascart.Visible = True
    Me.lce_qtascart.VisibleIndex = 6
    '
    'xx_tempatte
    '
    Me.xx_tempatte.AppearanceCell.Options.UseBackColor = True
    Me.xx_tempatte.AppearanceCell.Options.UseTextOptions = True
    Me.xx_tempatte.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_tempatte.Caption = "Tempo att."
    Me.xx_tempatte.Enabled = True
    Me.xx_tempatte.FieldName = "xx_tempatte"
    Me.xx_tempatte.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_tempatte.Name = "xx_tempatte"
    Me.xx_tempatte.NTSRepositoryComboBox = Nothing
    Me.xx_tempatte.NTSRepositoryItemCheck = Nothing
    Me.xx_tempatte.NTSRepositoryItemMemo = Nothing
    Me.xx_tempatte.NTSRepositoryItemText = Nothing
    Me.xx_tempatte.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_tempatte.OptionsFilter.AllowFilter = False
    Me.xx_tempatte.Visible = True
    Me.xx_tempatte.VisibleIndex = 7
    '
    'xx_tempese
    '
    Me.xx_tempese.AppearanceCell.Options.UseBackColor = True
    Me.xx_tempese.AppearanceCell.Options.UseTextOptions = True
    Me.xx_tempese.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_tempese.Caption = "Tempo esec."
    Me.xx_tempese.Enabled = True
    Me.xx_tempese.FieldName = "xx_tempese"
    Me.xx_tempese.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_tempese.Name = "xx_tempese"
    Me.xx_tempese.NTSRepositoryComboBox = Nothing
    Me.xx_tempese.NTSRepositoryItemCheck = Nothing
    Me.xx_tempese.NTSRepositoryItemMemo = Nothing
    Me.xx_tempese.NTSRepositoryItemText = Nothing
    Me.xx_tempese.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_tempese.OptionsFilter.AllowFilter = False
    Me.xx_tempese.Visible = True
    Me.xx_tempese.VisibleIndex = 8
    '
    'lce_valore
    '
    Me.lce_valore.AppearanceCell.Options.UseBackColor = True
    Me.lce_valore.AppearanceCell.Options.UseTextOptions = True
    Me.lce_valore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_valore.Caption = "COSTO TOT. macchina"
    Me.lce_valore.Enabled = False
    Me.lce_valore.FieldName = "lce_valore"
    Me.lce_valore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_valore.Name = "lce_valore"
    Me.lce_valore.NTSRepositoryComboBox = Nothing
    Me.lce_valore.NTSRepositoryItemCheck = Nothing
    Me.lce_valore.NTSRepositoryItemMemo = Nothing
    Me.lce_valore.NTSRepositoryItemText = Nothing
    Me.lce_valore.OptionsColumn.AllowEdit = False
    Me.lce_valore.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_valore.OptionsColumn.ReadOnly = True
    Me.lce_valore.OptionsFilter.AllowFilter = False
    Me.lce_valore.Visible = True
    Me.lce_valore.VisibleIndex = 9
    '
    'lce_valmo
    '
    Me.lce_valmo.AppearanceCell.Options.UseBackColor = True
    Me.lce_valmo.AppearanceCell.Options.UseTextOptions = True
    Me.lce_valmo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_valmo.Caption = "COSTO TOT. manod."
    Me.lce_valmo.Enabled = False
    Me.lce_valmo.FieldName = "lce_valmo"
    Me.lce_valmo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_valmo.Name = "lce_valmo"
    Me.lce_valmo.NTSRepositoryComboBox = Nothing
    Me.lce_valmo.NTSRepositoryItemCheck = Nothing
    Me.lce_valmo.NTSRepositoryItemMemo = Nothing
    Me.lce_valmo.NTSRepositoryItemText = Nothing
    Me.lce_valmo.OptionsColumn.AllowEdit = False
    Me.lce_valmo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_valmo.OptionsColumn.ReadOnly = True
    Me.lce_valmo.OptionsFilter.AllowFilter = False
    Me.lce_valmo.Visible = True
    Me.lce_valmo.VisibleIndex = 10
    '
    'lce_oranno
    '
    Me.lce_oranno.AppearanceCell.Options.UseBackColor = True
    Me.lce_oranno.AppearanceCell.Options.UseTextOptions = True
    Me.lce_oranno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_oranno.Caption = "Ord. Anno"
    Me.lce_oranno.Enabled = False
    Me.lce_oranno.FieldName = "lce_oranno"
    Me.lce_oranno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_oranno.Name = "lce_oranno"
    Me.lce_oranno.NTSRepositoryComboBox = Nothing
    Me.lce_oranno.NTSRepositoryItemCheck = Nothing
    Me.lce_oranno.NTSRepositoryItemMemo = Nothing
    Me.lce_oranno.NTSRepositoryItemText = Nothing
    Me.lce_oranno.OptionsColumn.AllowEdit = False
    Me.lce_oranno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_oranno.OptionsColumn.ReadOnly = True
    Me.lce_oranno.OptionsFilter.AllowFilter = False
    Me.lce_oranno.Visible = True
    Me.lce_oranno.VisibleIndex = 11
    '
    'lce_orserie
    '
    Me.lce_orserie.AppearanceCell.Options.UseBackColor = True
    Me.lce_orserie.AppearanceCell.Options.UseTextOptions = True
    Me.lce_orserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_orserie.Caption = "Ord. Serie"
    Me.lce_orserie.Enabled = False
    Me.lce_orserie.FieldName = "lce_orserie"
    Me.lce_orserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_orserie.Name = "lce_orserie"
    Me.lce_orserie.NTSRepositoryComboBox = Nothing
    Me.lce_orserie.NTSRepositoryItemCheck = Nothing
    Me.lce_orserie.NTSRepositoryItemMemo = Nothing
    Me.lce_orserie.NTSRepositoryItemText = Nothing
    Me.lce_orserie.OptionsColumn.AllowEdit = False
    Me.lce_orserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_orserie.OptionsColumn.ReadOnly = True
    Me.lce_orserie.OptionsFilter.AllowFilter = False
    Me.lce_orserie.Visible = True
    Me.lce_orserie.VisibleIndex = 12
    '
    'lce_ornum
    '
    Me.lce_ornum.AppearanceCell.Options.UseBackColor = True
    Me.lce_ornum.AppearanceCell.Options.UseTextOptions = True
    Me.lce_ornum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_ornum.Caption = "Ord. Num."
    Me.lce_ornum.Enabled = False
    Me.lce_ornum.FieldName = "lce_ornum"
    Me.lce_ornum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_ornum.Name = "lce_ornum"
    Me.lce_ornum.NTSRepositoryComboBox = Nothing
    Me.lce_ornum.NTSRepositoryItemCheck = Nothing
    Me.lce_ornum.NTSRepositoryItemMemo = Nothing
    Me.lce_ornum.NTSRepositoryItemText = Nothing
    Me.lce_ornum.OptionsColumn.AllowEdit = False
    Me.lce_ornum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_ornum.OptionsColumn.ReadOnly = True
    Me.lce_ornum.OptionsFilter.AllowFilter = False
    Me.lce_ornum.Visible = True
    Me.lce_ornum.VisibleIndex = 13
    '
    'lce_orriga
    '
    Me.lce_orriga.AppearanceCell.Options.UseBackColor = True
    Me.lce_orriga.AppearanceCell.Options.UseTextOptions = True
    Me.lce_orriga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_orriga.Caption = "Ord. Riga"
    Me.lce_orriga.Enabled = False
    Me.lce_orriga.FieldName = "lce_orriga"
    Me.lce_orriga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_orriga.Name = "lce_orriga"
    Me.lce_orriga.NTSRepositoryComboBox = Nothing
    Me.lce_orriga.NTSRepositoryItemCheck = Nothing
    Me.lce_orriga.NTSRepositoryItemMemo = Nothing
    Me.lce_orriga.NTSRepositoryItemText = Nothing
    Me.lce_orriga.OptionsColumn.AllowEdit = False
    Me.lce_orriga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_orriga.OptionsColumn.ReadOnly = True
    Me.lce_orriga.OptionsFilter.AllowFilter = False
    Me.lce_orriga.Visible = True
    Me.lce_orriga.VisibleIndex = 14
    '
    'lce_orfase
    '
    Me.lce_orfase.AppearanceCell.Options.UseBackColor = True
    Me.lce_orfase.AppearanceCell.Options.UseTextOptions = True
    Me.lce_orfase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_orfase.Caption = "Ord. Fase"
    Me.lce_orfase.Enabled = False
    Me.lce_orfase.FieldName = "lce_orfase"
    Me.lce_orfase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_orfase.Name = "lce_orfase"
    Me.lce_orfase.NTSRepositoryComboBox = Nothing
    Me.lce_orfase.NTSRepositoryItemCheck = Nothing
    Me.lce_orfase.NTSRepositoryItemMemo = Nothing
    Me.lce_orfase.NTSRepositoryItemText = Nothing
    Me.lce_orfase.OptionsColumn.AllowEdit = False
    Me.lce_orfase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_orfase.OptionsColumn.ReadOnly = True
    Me.lce_orfase.OptionsFilter.AllowFilter = False
    Me.lce_orfase.Visible = True
    Me.lce_orfase.VisibleIndex = 15
    '
    'lce_flevas
    '
    Me.lce_flevas.AppearanceCell.Options.UseBackColor = True
    Me.lce_flevas.AppearanceCell.Options.UseTextOptions = True
    Me.lce_flevas.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_flevas.Caption = "A saldo"
    Me.lce_flevas.Enabled = False
    Me.lce_flevas.FieldName = "lce_flevas"
    Me.lce_flevas.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_flevas.Name = "lce_flevas"
    Me.lce_flevas.NTSRepositoryComboBox = Nothing
    Me.lce_flevas.NTSRepositoryItemCheck = Nothing
    Me.lce_flevas.NTSRepositoryItemMemo = Nothing
    Me.lce_flevas.NTSRepositoryItemText = Nothing
    Me.lce_flevas.OptionsColumn.AllowEdit = False
    Me.lce_flevas.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_flevas.OptionsColumn.ReadOnly = True
    Me.lce_flevas.OptionsFilter.AllowFilter = False
    Me.lce_flevas.Visible = True
    Me.lce_flevas.VisibleIndex = 16
    '
    'lce_start
    '
    Me.lce_start.AppearanceCell.Options.UseBackColor = True
    Me.lce_start.AppearanceCell.Options.UseTextOptions = True
    Me.lce_start.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_start.Caption = "Inizio"
    Me.lce_start.Enabled = False
    Me.lce_start.FieldName = "lce_start"
    Me.lce_start.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_start.Name = "lce_start"
    Me.lce_start.NTSRepositoryComboBox = Nothing
    Me.lce_start.NTSRepositoryItemCheck = Nothing
    Me.lce_start.NTSRepositoryItemMemo = Nothing
    Me.lce_start.NTSRepositoryItemText = Nothing
    Me.lce_start.OptionsColumn.AllowEdit = False
    Me.lce_start.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_start.OptionsColumn.ReadOnly = True
    Me.lce_start.OptionsFilter.AllowFilter = False
    '
    'lce_stop
    '
    Me.lce_stop.AppearanceCell.Options.UseBackColor = True
    Me.lce_stop.AppearanceCell.Options.UseTextOptions = True
    Me.lce_stop.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_stop.Caption = "Fine"
    Me.lce_stop.Enabled = False
    Me.lce_stop.FieldName = "lce_stop"
    Me.lce_stop.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_stop.Name = "lce_stop"
    Me.lce_stop.NTSRepositoryComboBox = Nothing
    Me.lce_stop.NTSRepositoryItemCheck = Nothing
    Me.lce_stop.NTSRepositoryItemMemo = Nothing
    Me.lce_stop.NTSRepositoryItemText = Nothing
    Me.lce_stop.OptionsColumn.AllowEdit = False
    Me.lce_stop.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_stop.OptionsColumn.ReadOnly = True
    Me.lce_stop.OptionsFilter.AllowFilter = False
    '
    'lce_cmacora
    '
    Me.lce_cmacora.AppearanceCell.Options.UseBackColor = True
    Me.lce_cmacora.AppearanceCell.Options.UseTextOptions = True
    Me.lce_cmacora.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_cmacora.Caption = "Costo orario macchina"
    Me.lce_cmacora.Enabled = True
    Me.lce_cmacora.FieldName = "lce_cmacora"
    Me.lce_cmacora.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_cmacora.Name = "lce_cmacora"
    Me.lce_cmacora.NTSRepositoryComboBox = Nothing
    Me.lce_cmacora.NTSRepositoryItemCheck = Nothing
    Me.lce_cmacora.NTSRepositoryItemMemo = Nothing
    Me.lce_cmacora.NTSRepositoryItemText = Nothing
    Me.lce_cmacora.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_cmacora.OptionsFilter.AllowFilter = False
    Me.lce_cmacora.Visible = True
    Me.lce_cmacora.VisibleIndex = 17
    '
    'lce_pagaora
    '
    Me.lce_pagaora.AppearanceCell.Options.UseBackColor = True
    Me.lce_pagaora.AppearanceCell.Options.UseTextOptions = True
    Me.lce_pagaora.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_pagaora.Caption = "Costo orario manod."
    Me.lce_pagaora.Enabled = True
    Me.lce_pagaora.FieldName = "lce_pagaora"
    Me.lce_pagaora.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_pagaora.Name = "lce_pagaora"
    Me.lce_pagaora.NTSRepositoryComboBox = Nothing
    Me.lce_pagaora.NTSRepositoryItemCheck = Nothing
    Me.lce_pagaora.NTSRepositoryItemMemo = Nothing
    Me.lce_pagaora.NTSRepositoryItemText = Nothing
    Me.lce_pagaora.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_pagaora.OptionsFilter.AllowFilter = False
    Me.lce_pagaora.Visible = True
    Me.lce_pagaora.VisibleIndex = 18
    '
    'lce_cmacoratt
    '
    Me.lce_cmacoratt.AppearanceCell.Options.UseBackColor = True
    Me.lce_cmacoratt.AppearanceCell.Options.UseTextOptions = True
    Me.lce_cmacoratt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_cmacoratt.Caption = "Costo or. macchina attr."
    Me.lce_cmacoratt.Enabled = True
    Me.lce_cmacoratt.FieldName = "lce_cmacoratt"
    Me.lce_cmacoratt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_cmacoratt.Name = "lce_cmacoratt"
    Me.lce_cmacoratt.NTSRepositoryComboBox = Nothing
    Me.lce_cmacoratt.NTSRepositoryItemCheck = Nothing
    Me.lce_cmacoratt.NTSRepositoryItemMemo = Nothing
    Me.lce_cmacoratt.NTSRepositoryItemText = Nothing
    Me.lce_cmacoratt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_cmacoratt.OptionsFilter.AllowFilter = False
    Me.lce_cmacoratt.Visible = True
    Me.lce_cmacoratt.VisibleIndex = 19
    '
    'lce_pagaoratt
    '
    Me.lce_pagaoratt.AppearanceCell.Options.UseBackColor = True
    Me.lce_pagaoratt.AppearanceCell.Options.UseTextOptions = True
    Me.lce_pagaoratt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_pagaoratt.Caption = "Costo or. manod. attr."
    Me.lce_pagaoratt.Enabled = True
    Me.lce_pagaoratt.FieldName = "lce_pagaoratt"
    Me.lce_pagaoratt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_pagaoratt.Name = "lce_pagaoratt"
    Me.lce_pagaoratt.NTSRepositoryComboBox = Nothing
    Me.lce_pagaoratt.NTSRepositoryItemCheck = Nothing
    Me.lce_pagaoratt.NTSRepositoryItemMemo = Nothing
    Me.lce_pagaoratt.NTSRepositoryItemText = Nothing
    Me.lce_pagaoratt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_pagaoratt.OptionsFilter.AllowFilter = False
    Me.lce_pagaoratt.Visible = True
    Me.lce_pagaoratt.VisibleIndex = 20
    '
    'lce_commeca
    '
    Me.lce_commeca.AppearanceCell.Options.UseBackColor = True
    Me.lce_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.lce_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_commeca.Caption = "Commessa"
    Me.lce_commeca.Enabled = True
    Me.lce_commeca.FieldName = "lce_commeca"
    Me.lce_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_commeca.Name = "lce_commeca"
    Me.lce_commeca.NTSRepositoryComboBox = Nothing
    Me.lce_commeca.NTSRepositoryItemCheck = Nothing
    Me.lce_commeca.NTSRepositoryItemMemo = Nothing
    Me.lce_commeca.NTSRepositoryItemText = Nothing
    Me.lce_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_commeca.OptionsFilter.AllowFilter = False
    Me.lce_commeca.Visible = True
    Me.lce_commeca.VisibleIndex = 21
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
    Me.xx_commeca.VisibleIndex = 22
    '
    'lce_subcommeca
    '
    Me.lce_subcommeca.AppearanceCell.Options.UseBackColor = True
    Me.lce_subcommeca.AppearanceCell.Options.UseTextOptions = True
    Me.lce_subcommeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_subcommeca.Caption = "Subcomm."
    Me.lce_subcommeca.Enabled = True
    Me.lce_subcommeca.FieldName = "lce_subcommeca"
    Me.lce_subcommeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_subcommeca.Name = "lce_subcommeca"
    Me.lce_subcommeca.NTSRepositoryComboBox = Nothing
    Me.lce_subcommeca.NTSRepositoryItemCheck = Nothing
    Me.lce_subcommeca.NTSRepositoryItemMemo = Nothing
    Me.lce_subcommeca.NTSRepositoryItemText = Nothing
    Me.lce_subcommeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_subcommeca.OptionsFilter.AllowFilter = False
    Me.lce_subcommeca.Visible = True
    Me.lce_subcommeca.VisibleIndex = 23
    '
    'lce_controp
    '
    Me.lce_controp.AppearanceCell.Options.UseBackColor = True
    Me.lce_controp.AppearanceCell.Options.UseTextOptions = True
    Me.lce_controp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_controp.Caption = "Contropartita"
    Me.lce_controp.Enabled = True
    Me.lce_controp.FieldName = "lce_controp"
    Me.lce_controp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_controp.Name = "lce_controp"
    Me.lce_controp.NTSRepositoryComboBox = Nothing
    Me.lce_controp.NTSRepositoryItemCheck = Nothing
    Me.lce_controp.NTSRepositoryItemMemo = Nothing
    Me.lce_controp.NTSRepositoryItemText = Nothing
    Me.lce_controp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_controp.OptionsFilter.AllowFilter = False
    Me.lce_controp.Visible = True
    Me.lce_controp.VisibleIndex = 24
    '
    'xx_controp
    '
    Me.xx_controp.AppearanceCell.Options.UseBackColor = True
    Me.xx_controp.AppearanceCell.Options.UseTextOptions = True
    Me.xx_controp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_controp.Caption = "Descr. controp."
    Me.xx_controp.Enabled = False
    Me.xx_controp.FieldName = "xx_controp"
    Me.xx_controp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_controp.Name = "xx_controp"
    Me.xx_controp.NTSRepositoryComboBox = Nothing
    Me.xx_controp.NTSRepositoryItemCheck = Nothing
    Me.xx_controp.NTSRepositoryItemMemo = Nothing
    Me.xx_controp.NTSRepositoryItemText = Nothing
    Me.xx_controp.OptionsColumn.AllowEdit = False
    Me.xx_controp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_controp.OptionsColumn.ReadOnly = True
    Me.xx_controp.OptionsFilter.AllowFilter = False
    Me.xx_controp.Visible = True
    Me.xx_controp.VisibleIndex = 25
    '
    'lce_contocontr
    '
    Me.lce_contocontr.AppearanceCell.Options.UseBackColor = True
    Me.lce_contocontr.AppearanceCell.Options.UseTextOptions = True
    Me.lce_contocontr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_contocontr.Caption = "Conto controp."
    Me.lce_contocontr.Enabled = False
    Me.lce_contocontr.FieldName = "lce_contocontr"
    Me.lce_contocontr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_contocontr.Name = "lce_contocontr"
    Me.lce_contocontr.NTSRepositoryComboBox = Nothing
    Me.lce_contocontr.NTSRepositoryItemCheck = Nothing
    Me.lce_contocontr.NTSRepositoryItemMemo = Nothing
    Me.lce_contocontr.NTSRepositoryItemText = Nothing
    Me.lce_contocontr.OptionsColumn.AllowEdit = False
    Me.lce_contocontr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_contocontr.OptionsColumn.ReadOnly = True
    Me.lce_contocontr.OptionsFilter.AllowFilter = False
    '
    'lce_codcena
    '
    Me.lce_codcena.AppearanceCell.Options.UseBackColor = True
    Me.lce_codcena.AppearanceCell.Options.UseTextOptions = True
    Me.lce_codcena.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_codcena.Caption = "Centro"
    Me.lce_codcena.Enabled = True
    Me.lce_codcena.FieldName = "lce_codcena"
    Me.lce_codcena.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_codcena.Name = "lce_codcena"
    Me.lce_codcena.NTSRepositoryComboBox = Nothing
    Me.lce_codcena.NTSRepositoryItemCheck = Nothing
    Me.lce_codcena.NTSRepositoryItemMemo = Nothing
    Me.lce_codcena.NTSRepositoryItemText = Nothing
    Me.lce_codcena.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_codcena.OptionsFilter.AllowFilter = False
    Me.lce_codcena.Visible = True
    Me.lce_codcena.VisibleIndex = 26
    '
    'xx_codcena
    '
    Me.xx_codcena.AppearanceCell.Options.UseBackColor = True
    Me.xx_codcena.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codcena.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codcena.Caption = "Descr. centro"
    Me.xx_codcena.Enabled = False
    Me.xx_codcena.FieldName = "xx_codcena"
    Me.xx_codcena.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codcena.Name = "xx_codcena"
    Me.xx_codcena.NTSRepositoryComboBox = Nothing
    Me.xx_codcena.NTSRepositoryItemCheck = Nothing
    Me.xx_codcena.NTSRepositoryItemMemo = Nothing
    Me.xx_codcena.NTSRepositoryItemText = Nothing
    Me.xx_codcena.OptionsColumn.AllowEdit = False
    Me.xx_codcena.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codcena.OptionsColumn.ReadOnly = True
    Me.xx_codcena.OptionsFilter.AllowFilter = False
    Me.xx_codcena.Visible = True
    Me.xx_codcena.VisibleIndex = 27
    '
    'lce_codcfam
    '
    Me.lce_codcfam.AppearanceCell.Options.UseBackColor = True
    Me.lce_codcfam.AppearanceCell.Options.UseTextOptions = True
    Me.lce_codcfam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_codcfam.Caption = "Linea/fam."
    Me.lce_codcfam.Enabled = True
    Me.lce_codcfam.FieldName = "lce_codcfam"
    Me.lce_codcfam.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_codcfam.Name = "lce_codcfam"
    Me.lce_codcfam.NTSRepositoryComboBox = Nothing
    Me.lce_codcfam.NTSRepositoryItemCheck = Nothing
    Me.lce_codcfam.NTSRepositoryItemMemo = Nothing
    Me.lce_codcfam.NTSRepositoryItemText = Nothing
    Me.lce_codcfam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_codcfam.OptionsFilter.AllowFilter = False
    Me.lce_codcfam.Visible = True
    Me.lce_codcfam.VisibleIndex = 28
    '
    'xx_codcfam
    '
    Me.xx_codcfam.AppearanceCell.Options.UseBackColor = True
    Me.xx_codcfam.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codcfam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codcfam.Caption = "Descr. linea/fam."
    Me.xx_codcfam.Enabled = False
    Me.xx_codcfam.FieldName = "xx_codcfam"
    Me.xx_codcfam.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codcfam.Name = "xx_codcfam"
    Me.xx_codcfam.NTSRepositoryComboBox = Nothing
    Me.xx_codcfam.NTSRepositoryItemCheck = Nothing
    Me.xx_codcfam.NTSRepositoryItemMemo = Nothing
    Me.xx_codcfam.NTSRepositoryItemText = Nothing
    Me.xx_codcfam.OptionsColumn.AllowEdit = False
    Me.xx_codcfam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codcfam.OptionsColumn.ReadOnly = True
    Me.xx_codcfam.OptionsFilter.AllowFilter = False
    Me.xx_codcfam.Visible = True
    Me.xx_codcfam.VisibleIndex = 29
    '
    'lce_ganno
    '
    Me.lce_ganno.AppearanceCell.Options.UseBackColor = True
    Me.lce_ganno.AppearanceCell.Options.UseTextOptions = True
    Me.lce_ganno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_ganno.Caption = "Anno avanz. lavor."
    Me.lce_ganno.Enabled = False
    Me.lce_ganno.FieldName = "lce_ganno"
    Me.lce_ganno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_ganno.Name = "lce_ganno"
    Me.lce_ganno.NTSRepositoryComboBox = Nothing
    Me.lce_ganno.NTSRepositoryItemCheck = Nothing
    Me.lce_ganno.NTSRepositoryItemMemo = Nothing
    Me.lce_ganno.NTSRepositoryItemText = Nothing
    Me.lce_ganno.OptionsColumn.AllowEdit = False
    Me.lce_ganno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_ganno.OptionsColumn.ReadOnly = True
    Me.lce_ganno.OptionsFilter.AllowFilter = False
    '
    'lce_gserie
    '
    Me.lce_gserie.AppearanceCell.Options.UseBackColor = True
    Me.lce_gserie.AppearanceCell.Options.UseTextOptions = True
    Me.lce_gserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_gserie.Caption = "Sere avanz. lavor."
    Me.lce_gserie.Enabled = False
    Me.lce_gserie.FieldName = "lce_gserie"
    Me.lce_gserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_gserie.Name = "lce_gserie"
    Me.lce_gserie.NTSRepositoryComboBox = Nothing
    Me.lce_gserie.NTSRepositoryItemCheck = Nothing
    Me.lce_gserie.NTSRepositoryItemMemo = Nothing
    Me.lce_gserie.NTSRepositoryItemText = Nothing
    Me.lce_gserie.OptionsColumn.AllowEdit = False
    Me.lce_gserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_gserie.OptionsColumn.ReadOnly = True
    Me.lce_gserie.OptionsFilter.AllowFilter = False
    '
    'lce_gnumdoc
    '
    Me.lce_gnumdoc.AppearanceCell.Options.UseBackColor = True
    Me.lce_gnumdoc.AppearanceCell.Options.UseTextOptions = True
    Me.lce_gnumdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_gnumdoc.Caption = "Num. avanz. lavor."
    Me.lce_gnumdoc.Enabled = False
    Me.lce_gnumdoc.FieldName = "lce_gnumdoc"
    Me.lce_gnumdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_gnumdoc.Name = "lce_gnumdoc"
    Me.lce_gnumdoc.NTSRepositoryComboBox = Nothing
    Me.lce_gnumdoc.NTSRepositoryItemCheck = Nothing
    Me.lce_gnumdoc.NTSRepositoryItemMemo = Nothing
    Me.lce_gnumdoc.NTSRepositoryItemText = Nothing
    Me.lce_gnumdoc.OptionsColumn.AllowEdit = False
    Me.lce_gnumdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_gnumdoc.OptionsColumn.ReadOnly = True
    Me.lce_gnumdoc.OptionsFilter.AllowFilter = False
    '
    'lce_grigaa
    '
    Me.lce_grigaa.AppearanceCell.Options.UseBackColor = True
    Me.lce_grigaa.AppearanceCell.Options.UseTextOptions = True
    Me.lce_grigaa.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_grigaa.Caption = "Riga avanz. lavor."
    Me.lce_grigaa.Enabled = False
    Me.lce_grigaa.FieldName = "lce_grigaa"
    Me.lce_grigaa.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_grigaa.Name = "lce_grigaa"
    Me.lce_grigaa.NTSRepositoryComboBox = Nothing
    Me.lce_grigaa.NTSRepositoryItemCheck = Nothing
    Me.lce_grigaa.NTSRepositoryItemMemo = Nothing
    Me.lce_grigaa.NTSRepositoryItemText = Nothing
    Me.lce_grigaa.OptionsColumn.AllowEdit = False
    Me.lce_grigaa.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_grigaa.OptionsColumn.ReadOnly = True
    Me.lce_grigaa.OptionsFilter.AllowFilter = False
    '
    'lce_note
    '
    Me.lce_note.AppearanceCell.Options.UseBackColor = True
    Me.lce_note.AppearanceCell.Options.UseTextOptions = True
    Me.lce_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_note.Caption = "Note"
    Me.lce_note.Enabled = True
    Me.lce_note.FieldName = "lce_note"
    Me.lce_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_note.Name = "lce_note"
    Me.lce_note.NTSRepositoryComboBox = Nothing
    Me.lce_note.NTSRepositoryItemCheck = Nothing
    Me.lce_note.NTSRepositoryItemMemo = Nothing
    Me.lce_note.NTSRepositoryItemText = Nothing
    Me.lce_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_note.OptionsFilter.AllowFilter = False
    '
    'lce_valorev
    '
    Me.lce_valorev.AppearanceCell.Options.UseBackColor = True
    Me.lce_valorev.AppearanceCell.Options.UseTextOptions = True
    Me.lce_valorev.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_valorev.Caption = "COSTO TOT. in valuta"
    Me.lce_valorev.Enabled = False
    Me.lce_valorev.FieldName = "lce_valorev"
    Me.lce_valorev.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_valorev.Name = "lce_valorev"
    Me.lce_valorev.NTSRepositoryComboBox = Nothing
    Me.lce_valorev.NTSRepositoryItemCheck = Nothing
    Me.lce_valorev.NTSRepositoryItemMemo = Nothing
    Me.lce_valorev.NTSRepositoryItemText = Nothing
    Me.lce_valorev.OptionsColumn.AllowEdit = False
    Me.lce_valorev.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_valorev.OptionsColumn.ReadOnly = True
    Me.lce_valorev.OptionsFilter.AllowFilter = False
    '
    'lce_codcope
    '
    Me.lce_codcope.AppearanceCell.Options.UseBackColor = True
    Me.lce_codcope.AppearanceCell.Options.UseTextOptions = True
    Me.lce_codcope.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lce_codcope.Caption = "Operaio"
    Me.lce_codcope.Enabled = True
    Me.lce_codcope.FieldName = "lce_codcope"
    Me.lce_codcope.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lce_codcope.Name = "lce_codcope"
    Me.lce_codcope.NTSRepositoryComboBox = Nothing
    Me.lce_codcope.NTSRepositoryItemCheck = Nothing
    Me.lce_codcope.NTSRepositoryItemMemo = Nothing
    Me.lce_codcope.NTSRepositoryItemText = Nothing
    Me.lce_codcope.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lce_codcope.OptionsFilter.AllowFilter = False
    Me.lce_codcope.Visible = True
    Me.lce_codcope.VisibleIndex = 30
    '
    'xx_codcope
    '
    Me.xx_codcope.AppearanceCell.Options.UseBackColor = True
    Me.xx_codcope.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codcope.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codcope.Caption = "Descr. operaio"
    Me.xx_codcope.Enabled = False
    Me.xx_codcope.FieldName = "xx_codcope"
    Me.xx_codcope.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codcope.Name = "xx_codcope"
    Me.xx_codcope.NTSRepositoryComboBox = Nothing
    Me.xx_codcope.NTSRepositoryItemCheck = Nothing
    Me.xx_codcope.NTSRepositoryItemMemo = Nothing
    Me.xx_codcope.NTSRepositoryItemText = Nothing
    Me.xx_codcope.OptionsColumn.AllowEdit = False
    Me.xx_codcope.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codcope.OptionsColumn.ReadOnly = True
    Me.xx_codcope.OptionsFilter.AllowFilter = False
    Me.xx_codcope.Visible = True
    Me.xx_codcope.VisibleIndex = 31
    '
    'xx_tempattem
    '
    Me.xx_tempattem.AppearanceCell.Options.UseBackColor = True
    Me.xx_tempattem.AppearanceCell.Options.UseTextOptions = True
    Me.xx_tempattem.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_tempattem.Caption = "Tempo attr. minuti"
    Me.xx_tempattem.Enabled = True
    Me.xx_tempattem.FieldName = "xx_tempattem"
    Me.xx_tempattem.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_tempattem.Name = "xx_tempattem"
    Me.xx_tempattem.NTSRepositoryComboBox = Nothing
    Me.xx_tempattem.NTSRepositoryItemCheck = Nothing
    Me.xx_tempattem.NTSRepositoryItemMemo = Nothing
    Me.xx_tempattem.NTSRepositoryItemText = Nothing
    Me.xx_tempattem.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_tempattem.OptionsFilter.AllowFilter = False
    '
    'xx_tempesem
    '
    Me.xx_tempesem.AppearanceCell.Options.UseBackColor = True
    Me.xx_tempesem.AppearanceCell.Options.UseTextOptions = True
    Me.xx_tempesem.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_tempesem.Caption = "Tempo esec. minuti"
    Me.xx_tempesem.Enabled = True
    Me.xx_tempesem.FieldName = "xx_tempesem"
    Me.xx_tempesem.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_tempesem.Name = "xx_tempesem"
    Me.xx_tempesem.NTSRepositoryComboBox = Nothing
    Me.xx_tempesem.NTSRepositoryItemCheck = Nothing
    Me.xx_tempesem.NTSRepositoryItemMemo = Nothing
    Me.xx_tempesem.NTSRepositoryItemText = Nothing
    Me.xx_tempesem.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_tempesem.OptionsFilter.AllowFilter = False
    '
    'FRMVELAVC
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(740, 442)
    Me.Controls.Add(Me.grRighe)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMVELAVC"
    Me.Text = "LAVORAZIONI COLLEGATI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grRighe, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvRighe, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overloads Function InitEntity(ByRef Menu As CLE__MENU, ByRef frmVEBOLL As FRMVEBOLL, ByRef ds As DataSet) As Boolean
    oMenu = Menu
    oApp = oMenu.App
    frmParent = frmVEBOLL       'form PARENT
    DittaCorrente = oCleBoll.strDittaCorrente
    Me.GctlTipoDoc = "T"

    InitializeComponent()
    Me.MinimumSize = Me.Size

    '-------------------------------
    'leggo dal database i dati e collego il NTSBinding
    ds.AcceptChanges()
    oCleBoll.dsLavc = ds
    oCleBoll.LavcentSetDataTable(DittaCorrente, oCleBoll.dsLavc.Tables("LAVCENT"))
    dcLavc.DataSource = oCleBoll.dsLavc.Tables("LAVCENT")
    oCleBoll.dsLavc.AcceptChanges()

    '------------------------------
    'collego gli eventi dell'entity
    AddHandler oCleBoll.RemoteEvent, AddressOf GestisciEventiEntity

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim i As Integer = 0

    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvRighe.NTSSetParam(oMenu, oApp.Tr(Me, 128589605282500000, "Lavorazioni collegate"))
      lce_rigaa.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128691557130312500, "Riga"), "0", 9, 0, 999999999)
      lce_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128691557130468750, "Articolo"), CLN__STD.CodartMaxLen, False)
      xx_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128691557130625000, "Descr. art."), 0, True)
      lce_codlavo.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128691557130781250, "Lavoraz."), tablavo)
      xx_codlavo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128691557130937500, "Descr. lav."), 0, True)
      lce_oranno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128691557131093750, "Ord. Anno"), "0", 4, 0, 9999)
      lce_orserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128691557131250000, "Ord. Serie"), CLN__STD.SerieMaxLen, True)
      lce_ornum.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128691557131406250, "Ord. Num."), "0", 9, 0, 999999999)
      lce_orriga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128691557131562500, "Ord. Riga"), "0", 9, 0, 999999999)
      lce_orfase.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128691557131718750, "Ord. Fase"), "0", 4, 0, 9999)
      lce_qtaes.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128691557131875000, "Qta eseg."), oApp.FormatQta, 15)
      lce_qtascart.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128691557132031250, "Qta scart."), oApp.FormatQta, 15)
      lce_flevas.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128691557132187500, "A saldo"), "S", "C")
      xx_tempatte.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128691557132343750, "Tempo att."), "#,##0.0000", 9, 0, 999999999)
      xx_tempese.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128691557132500000, "Tempo esec."), "#,##0.0000", 9, 0, 999999999)
      lce_codcent.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128691557132656250, "Centro"), tabcent)
      xx_codcent.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128691557132812500, "Descr. centro"), 0, True)
      lce_start.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128691557132968750, "Inizio"), True, "X", NTSCDate(IntSetDate("01/01/1900")), NTSCDate(IntSetDate("31/12/2099")))
      lce_stop.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128691557133437500, "Fine"), True, "X", NTSCDate(IntSetDate("01/01/1900")), NTSCDate(IntSetDate("31/12/2099")))
      lce_cmacora.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128691557133593750, "Costo orario macchina"), oApp.FormatImporti, 15)
      lce_pagaora.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128691557133750000, "Costo orario manod."), oApp.FormatImporti, 15)
      lce_cmacoratt.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128691557133906250, "Costo or. macchina attr."), oApp.FormatImporti, 15)
      lce_pagaoratt.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128691557134062500, "Costo or. manod. attr."), oApp.FormatImporti, 15)
      lce_valore.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128691557134218750, "Costo tot. macchina"), oApp.FormatImporti, 15)
      lce_valmo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128691557134375000, "Costo tot. manod."), oApp.FormatImporti, 15)
      lce_commeca.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128691557134531250, "Commessa"), tabcommess)
      xx_commeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128691557134687500, "Descr. commessa"), 0, True)
      lce_subcommeca.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128691557134843750, "Subcomm."), tabsubcomm, True)
      lce_controp.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128691557135000000, "Contropartita"), tabcove)
      xx_controp.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128691557135156250, "Descr. controp."), 0, True)
      lce_contocontr.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128691557135312500, "Conto controp."), tabanagra)
      lce_codcena.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128691557135468750, "Centro"), tabcent)
      xx_codcena.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128691557135625000, "Descr. centro"), 0, True)
      lce_codcfam.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128691557135781250, "Linea/fam."), tabcfam, False)
      xx_codcfam.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128691557135937500, "Descr. linea/fam."), 0, True)
      lce_ganno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128691557136093750, "Anno avanz. lavor."), "0", 4, 0, 9999)
      lce_gserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128691557136250000, "Sere avanz. lavor."), CLN__STD.SerieMaxLen, False)
      lce_gnumdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128691557136406250, "Num. avanz. lavor."), "0", 9, 0, 999999999)
      lce_grigaa.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128691557136562500, "Riga avanz. lavor."), "0", 9, 0, 999999999)
      lce_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128691557136718750, "Note"), 0, True)
      lce_valorev.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128691557136875000, "Costo tot. in valuta"), oApp.FormatImpVal, 15)
      lce_codcope.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128691557137031250, "Operaio"), tabcope)
      xx_codcope.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128691557137187500, "Descr. operaio"), 0, True)
      xx_tempattem.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128786510026108000, "Tempo att. in minuti"), "0.00", 9, 0, 999999999)
      xx_tempesem.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128786510042020000, "Tempo esec. in minuti"), "0.00", 9, 0, 999999999)


      lce_codlavo.NTSSetRichiesto()
      lce_codcent.NTSSetRichiesto()
      If Not oCleBoll.bTerzista Then
        xx_tempatte.NTSSetRichiesto()
        xx_tempese.NTSSetRichiesto()
      End If
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

#Region "Eventi di Form"
  Public Overridable Sub FRMVELAVC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      grRighe.DataSource = dcLavc

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      grvRighe_NTSFocusedRowChanged(grvRighe, Nothing)

      If oCleBoll.bTerzista Then
        lce_valore.Enabled = False
        lce_valmo.Enabled = False
        lce_cmacora.Enabled = False
        lce_cmacoratt.Enabled = False
        lce_pagaora.Enabled = False
        lce_pagaoratt.Enabled = False
      Else
        'Ho sentito con Davide: rispetto a VB6 il valore riga è sempre bloccato, anche se la lavoraz. è interna
        lce_valore.Enabled = False
        lce_valmo.Enabled = False
        'GctlSetVisEnab(lce_valore, False)
        'GctlSetVisEnab(lce_valmo, False)
        GctlSetVisEnab(lce_cmacora, False)
        GctlSetVisEnab(lce_cmacoratt, False)
        GctlSetVisEnab(lce_pagaora, False)
        GctlSetVisEnab(lce_pagaoratt, False)
      End If

      If oCleBoll.bNew = False And oCleBoll.bCarProdInAvlavp Then
        grvRighe.Enabled = False
      Else
        grvRighe.Enabled = True
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMVELAVC_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try

  End Sub

  Public Overridable Sub FRMVELAVC_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    'salvo l'impostazione della griglia. devo farlo anche al cambio del tipo documento (ad esempio in bsveboll
    Try
      dcLavc.Dispose()
      oCleBoll.dsLavc.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvRighe.NTSNuovo()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
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

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Dim dtrDeleted As DataRow = Nothing
    Try
      If grvRighe.Focused = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129055188198477481, "Posizionarsi prima nella griglia e selezionare la riga da cancellare"))
        Return
      End If
      Me.Cursor = Cursors.WaitCursor
      If Not oCleBoll.LavcentTestPreCancella(dcLavc.Position) Then Return
      If Not grvRighe.NTSDeleteRigaCorrente(dcLavc, True, dtrDeleted) Then Return
      If Not oCleBoll.LavcentRecordSalva(dcLavc.Position, True, dtrDeleted) Then Return

      grvRighe_NTSFocusedRowChanged(grvRighe, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvRighe.NTSRipristinaRigaCorrenteBefore(dcLavc, True) Then Return
      oCleBoll.LavcentRipristina(dcLavc.Position, dcLavc.Filter)
      grvRighe.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    'SendKeys.SendWait("{F5}")
    'se faccio la riga sopra va in un loop infinito....
    'devo vedere quale controllo ha il focus, quindi lanciare lo zoom direttamente sul controllo, senza forzare l'F5
    'Dim ctrlTmp As Control = Nothing
    Dim oParam As New CLE__PATB

    Try

      If grvRighe.FocusedColumn.Equals(lce_subcommeca) Then
        '------------------------------------
        'zoom subcommessa
        If NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, lce_commeca)) = 0 Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 129055188292234081, "Indicare prima il codice commessa"))
          Return
        End If
        NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
        oParam.lCommessa = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, lce_commeca))   'passo il codice della commessa
        NTSZOOM.ZoomStrIn("ZOOMSUBCOMM", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvRighe.EditingValue) Then grvRighe.SetFocusedValue(NTSZOOM.strIn)
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

#Region "Eventi di griglia"
  Public Overridable Sub grRighe_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grRighe.Leave
    Try
      Me.Cursor = Cursors.WaitCursor
      If Not Salva() Then
        grRighe.Focus()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub grvRighe_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvRighe.NTSBeforeRowUpdate
    Try
      Me.Cursor = Cursors.WaitCursor
      If Not Salva() Then e.Allow = False 'rimango sulla riga su cui sono

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub grvRighe_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvRighe.NTSFocusedRowChanged
    '-------------------------------------------------
    'carico le unità di misura al cambio di riga
    Dim strTmp As String = ""

    Try
      If oCleBoll.dsLavc Is Nothing Then Return
      If oCleBoll.dsLavc.Tables("LAVCENT") Is Nothing Then Return
      If oCleBoll.dsLavc.Tables("LAVCENT").Rows.Count = 0 Then Return

      '-------------------------------------------------------
      'se sono su una nuova riga mi posiziono a sinistra
      'If NTSCInt(grvRighe.GetFocusedRowCellValue(lce_rigaa)) = 0 Then
      '  grvRighe.FocusedColumn = lce_codcent
      'End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvRighe_NTSRowColChange(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSRowColChangeEventArgs) Handles grvRighe.NTSRowColChange
    Try
      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        lce_flevas.Enabled = False
      Else
        If NTSCInt(grvRighe.NTSGetCurrentDataRow!lce_ornum) = 0 Then
          lce_flevas.Enabled = False
        Else
          GctlSetVisEnab(lce_flevas, False)
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

#End Region

  Public Overridable Function Salva() As Boolean
    Try
      If Not oCleBoll.lavcentRecordSalva(dcLavc.Position, False, Nothing) Then Return False
      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

End Class
