Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORTTA1
  Public oCleDocu As CLEMGDOCU
  Public oCallParams As CLE__CLDP
  Public dcTtat As BindingSource = New BindingSource()

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
  Public WithEvents at_coddb As NTSInformatica.NTSGridColumn
  Public WithEvents at_fase As NTSInformatica.NTSGridColumn
  Public WithEvents xx_coddb As NTSInformatica.NTSGridColumn
  Public WithEvents at_codlavo As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codlavo As NTSInformatica.NTSGridColumn
  Public WithEvents at_idproj As NTSInformatica.NTSGridColumn
  Public WithEvents xx_tempattpr As NTSInformatica.NTSGridColumn
  Public WithEvents xx_tempesepr As NTSInformatica.NTSGridColumn
  Public WithEvents xx_tempattees As NTSInformatica.NTSGridColumn
  Public WithEvents xx_tempesees As NTSInformatica.NTSGridColumn
  Public WithEvents at_flevas As NTSInformatica.NTSGridColumn
  Public WithEvents at_dtminima As NTSInformatica.NTSGridColumn
  Public WithEvents at_suanno As NTSInformatica.NTSGridColumn
  Public WithEvents at_suserie As NTSInformatica.NTSGridColumn
  Public WithEvents at_sunumord As NTSInformatica.NTSGridColumn
  Public WithEvents at_suriga As NTSInformatica.NTSGridColumn
  Public WithEvents at_sufase As NTSInformatica.NTSGridColumn
  Public WithEvents at_relazsuc As NTSInformatica.NTSGridColumn
  Public WithEvents at_qtapr As NTSInformatica.NTSGridColumn
  Public WithEvents at_qtaes As NTSInformatica.NTSGridColumn
  Public WithEvents at_datcons As NTSInformatica.NTSGridColumn
  Public WithEvents at_terzista As NTSInformatica.NTSGridColumn
  Public WithEvents at_suolprogr As NTSInformatica.NTSGridColumn
  Public WithEvents at_note As NTSInformatica.NTSGridColumn
  Public WithEvents at_riga As NTSInformatica.NTSGridColumn
  Public WithEvents as_codcent As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codcent As NTSInformatica.NTSGridColumn
  Public WithEvents as_percent As NTSInformatica.NTSGridColumn
  Public WithEvents as_valore As NTSInformatica.NTSGridColumn
  Public WithEvents as_valmo As NTSInformatica.NTSGridColumn
  Public WithEvents as_controp As NTSInformatica.NTSGridColumn
  Public WithEvents xx_controp As NTSInformatica.NTSGridColumn
  Public WithEvents as_contocontr As NTSInformatica.NTSGridColumn
  Public WithEvents as_codcena As NTSInformatica.NTSGridColumn
  Public WithEvents as_cmacora As NTSInformatica.NTSGridColumn
  Public WithEvents as_pagaora As NTSInformatica.NTSGridColumn
  Public WithEvents as_cmacoratt As NTSInformatica.NTSGridColumn
  Public WithEvents as_pagaoratt As NTSInformatica.NTSGridColumn
  Public WithEvents as_valorev As NTSInformatica.NTSGridColumn
  Public WithEvents xx_tempattprm As NTSInformatica.NTSGridColumn
  Public WithEvents xx_tempeseprm As NTSInformatica.NTSGridColumn
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem



  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMORTTA1))
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
    Me.xx_tempattprm = New NTSInformatica.NTSGridColumn
    Me.xx_tempeseprm = New NTSInformatica.NTSGridColumn
    Me.at_fase = New NTSInformatica.NTSGridColumn
    Me.as_codcent = New NTSInformatica.NTSGridColumn
    Me.xx_codcent = New NTSInformatica.NTSGridColumn
    Me.at_coddb = New NTSInformatica.NTSGridColumn
    Me.xx_coddb = New NTSInformatica.NTSGridColumn
    Me.at_codlavo = New NTSInformatica.NTSGridColumn
    Me.xx_codlavo = New NTSInformatica.NTSGridColumn
    Me.at_idproj = New NTSInformatica.NTSGridColumn
    Me.xx_tempattpr = New NTSInformatica.NTSGridColumn
    Me.xx_tempesepr = New NTSInformatica.NTSGridColumn
    Me.xx_tempattees = New NTSInformatica.NTSGridColumn
    Me.xx_tempesees = New NTSInformatica.NTSGridColumn
    Me.at_flevas = New NTSInformatica.NTSGridColumn
    Me.at_dtminima = New NTSInformatica.NTSGridColumn
    Me.as_controp = New NTSInformatica.NTSGridColumn
    Me.xx_controp = New NTSInformatica.NTSGridColumn
    Me.at_suanno = New NTSInformatica.NTSGridColumn
    Me.at_suserie = New NTSInformatica.NTSGridColumn
    Me.at_sunumord = New NTSInformatica.NTSGridColumn
    Me.at_suriga = New NTSInformatica.NTSGridColumn
    Me.at_sufase = New NTSInformatica.NTSGridColumn
    Me.at_relazsuc = New NTSInformatica.NTSGridColumn
    Me.at_qtapr = New NTSInformatica.NTSGridColumn
    Me.at_qtaes = New NTSInformatica.NTSGridColumn
    Me.at_datcons = New NTSInformatica.NTSGridColumn
    Me.at_terzista = New NTSInformatica.NTSGridColumn
    Me.at_suolprogr = New NTSInformatica.NTSGridColumn
    Me.at_note = New NTSInformatica.NTSGridColumn
    Me.at_riga = New NTSInformatica.NTSGridColumn
    Me.as_percent = New NTSInformatica.NTSGridColumn
    Me.as_valore = New NTSInformatica.NTSGridColumn
    Me.as_valmo = New NTSInformatica.NTSGridColumn
    Me.as_contocontr = New NTSInformatica.NTSGridColumn
    Me.as_codcena = New NTSInformatica.NTSGridColumn
    Me.as_cmacora = New NTSInformatica.NTSGridColumn
    Me.as_pagaora = New NTSInformatica.NTSGridColumn
    Me.as_cmacoratt = New NTSInformatica.NTSGridColumn
    Me.as_pagaoratt = New NTSInformatica.NTSGridColumn
    Me.as_valorev = New NTSInformatica.NTSGridColumn
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
    '
    'xx_tempattprm
    '
    Me.xx_tempattprm.AppearanceCell.Options.UseBackColor = True
    Me.xx_tempattprm.AppearanceCell.Options.UseTextOptions = True
    Me.xx_tempattprm.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_tempattprm.Caption = "Tempo attr. prev. minuti"
    Me.xx_tempattprm.Enabled = True
    Me.xx_tempattprm.FieldName = "xx_tempattprm"
    Me.xx_tempattprm.Name = "xx_tempattprm"
    Me.xx_tempattprm.NTSRepositoryComboBox = Nothing
    Me.xx_tempattprm.NTSRepositoryItemCheck = Nothing
    Me.xx_tempattprm.NTSRepositoryItemText = Nothing
    '
    'xx_tempeseprm
    '
    Me.xx_tempeseprm.AppearanceCell.Options.UseBackColor = True
    Me.xx_tempeseprm.AppearanceCell.Options.UseTextOptions = True
    Me.xx_tempeseprm.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_tempeseprm.Caption = "Tempo esec. prev. minuti"
    Me.xx_tempeseprm.Enabled = True
    Me.xx_tempeseprm.FieldName = "xx_tempeseprm"
    Me.xx_tempeseprm.Name = "xx_tempeseprm"
    Me.xx_tempeseprm.NTSRepositoryComboBox = Nothing
    Me.xx_tempeseprm.NTSRepositoryItemCheck = Nothing
    Me.xx_tempeseprm.NTSRepositoryItemText = Nothing
    Me.grvRighe.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.at_fase, Me.as_codcent, Me.xx_codcent, Me.at_coddb, Me.xx_coddb, Me.at_codlavo, Me.xx_codlavo, Me.at_idproj, Me.xx_tempattpr, Me.xx_tempesepr, Me.xx_tempattees, Me.xx_tempesees, Me.at_flevas, Me.at_dtminima, Me.as_controp, Me.xx_controp, Me.at_suanno, Me.at_suserie, Me.at_sunumord, Me.at_suriga, Me.at_sufase, Me.at_relazsuc, Me.at_qtapr, Me.at_qtaes, Me.at_datcons, Me.at_terzista, Me.at_suolprogr, Me.at_note, Me.at_riga, Me.as_percent, Me.as_valore, Me.as_valmo, Me.as_contocontr, Me.as_codcena, Me.as_cmacora, Me.as_pagaora, Me.as_cmacoratt, Me.as_pagaoratt, Me.as_valorev, Me.xx_tempattprm, Me.xx_tempeseprm})
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
    'at_fase
    '
    Me.at_fase.AppearanceCell.Options.UseBackColor = True
    Me.at_fase.AppearanceCell.Options.UseTextOptions = True
    Me.at_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.at_fase.Caption = "Fase"
    Me.at_fase.Enabled = True
    Me.at_fase.FieldName = "at_fase"
    Me.at_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.at_fase.Name = "at_fase"
    Me.at_fase.NTSRepositoryComboBox = Nothing
    Me.at_fase.NTSRepositoryItemCheck = Nothing
    Me.at_fase.NTSRepositoryItemText = Nothing
    Me.at_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.at_fase.OptionsFilter.AllowFilter = False
    Me.at_fase.Visible = True
    Me.at_fase.VisibleIndex = 0
    Me.at_fase.Width = 70
    '
    'as_codcent
    '
    Me.as_codcent.AppearanceCell.Options.UseBackColor = True
    Me.as_codcent.AppearanceCell.Options.UseTextOptions = True
    Me.as_codcent.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.as_codcent.Caption = "Centro di lavoro"
    Me.as_codcent.Enabled = True
    Me.as_codcent.FieldName = "as_codcent"
    Me.as_codcent.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.as_codcent.Name = "as_codcent"
    Me.as_codcent.NTSRepositoryComboBox = Nothing
    Me.as_codcent.NTSRepositoryItemCheck = Nothing
    Me.as_codcent.NTSRepositoryItemText = Nothing
    Me.as_codcent.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.as_codcent.OptionsFilter.AllowFilter = False
    Me.as_codcent.Visible = True
    Me.as_codcent.VisibleIndex = 1
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
    Me.xx_codcent.NTSRepositoryItemText = Nothing
    Me.xx_codcent.OptionsColumn.AllowEdit = False
    Me.xx_codcent.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codcent.OptionsColumn.ReadOnly = True
    Me.xx_codcent.OptionsFilter.AllowFilter = False
    Me.xx_codcent.Visible = True
    Me.xx_codcent.VisibleIndex = 2
    '
    'at_coddb
    '
    Me.at_coddb.AppearanceCell.Options.UseBackColor = True
    Me.at_coddb.AppearanceCell.Options.UseTextOptions = True
    Me.at_coddb.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.at_coddb.Caption = "Distinta base"
    Me.at_coddb.Enabled = False
    Me.at_coddb.FieldName = "at_coddb"
    Me.at_coddb.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.at_coddb.Name = "at_coddb"
    Me.at_coddb.NTSRepositoryComboBox = Nothing
    Me.at_coddb.NTSRepositoryItemCheck = Nothing
    Me.at_coddb.NTSRepositoryItemText = Nothing
    Me.at_coddb.OptionsColumn.AllowEdit = False
    Me.at_coddb.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.at_coddb.OptionsColumn.ReadOnly = True
    Me.at_coddb.OptionsFilter.AllowFilter = False
    Me.at_coddb.Visible = True
    Me.at_coddb.VisibleIndex = 3
    Me.at_coddb.Width = 70
    '
    'xx_coddb
    '
    Me.xx_coddb.AppearanceCell.Options.UseBackColor = True
    Me.xx_coddb.AppearanceCell.Options.UseTextOptions = True
    Me.xx_coddb.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_coddb.Caption = "Descriz. distinta base"
    Me.xx_coddb.Enabled = False
    Me.xx_coddb.FieldName = "xx_coddb"
    Me.xx_coddb.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_coddb.Name = "xx_coddb"
    Me.xx_coddb.NTSRepositoryComboBox = Nothing
    Me.xx_coddb.NTSRepositoryItemCheck = Nothing
    Me.xx_coddb.NTSRepositoryItemText = Nothing
    Me.xx_coddb.OptionsColumn.AllowEdit = False
    Me.xx_coddb.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_coddb.OptionsColumn.ReadOnly = True
    Me.xx_coddb.OptionsFilter.AllowFilter = False
    Me.xx_coddb.Visible = True
    Me.xx_coddb.VisibleIndex = 4
    '
    'at_codlavo
    '
    Me.at_codlavo.AppearanceCell.Options.UseBackColor = True
    Me.at_codlavo.AppearanceCell.Options.UseTextOptions = True
    Me.at_codlavo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.at_codlavo.Caption = "Cod. lavor."
    Me.at_codlavo.Enabled = True
    Me.at_codlavo.FieldName = "at_codlavo"
    Me.at_codlavo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.at_codlavo.Name = "at_codlavo"
    Me.at_codlavo.NTSRepositoryComboBox = Nothing
    Me.at_codlavo.NTSRepositoryItemCheck = Nothing
    Me.at_codlavo.NTSRepositoryItemText = Nothing
    Me.at_codlavo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.at_codlavo.OptionsFilter.AllowFilter = False
    Me.at_codlavo.Visible = True
    Me.at_codlavo.VisibleIndex = 5
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
    Me.xx_codlavo.NTSRepositoryItemText = Nothing
    Me.xx_codlavo.OptionsColumn.AllowEdit = False
    Me.xx_codlavo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codlavo.OptionsColumn.ReadOnly = True
    Me.xx_codlavo.OptionsFilter.AllowFilter = False
    Me.xx_codlavo.Visible = True
    Me.xx_codlavo.VisibleIndex = 6
    '
    'at_idproj
    '
    Me.at_idproj.AppearanceCell.Options.UseBackColor = True
    Me.at_idproj.AppearanceCell.Options.UseTextOptions = True
    Me.at_idproj.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.at_idproj.Caption = "Riga Project"
    Me.at_idproj.Enabled = False
    Me.at_idproj.FieldName = "at_idproj"
    Me.at_idproj.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.at_idproj.Name = "at_idproj"
    Me.at_idproj.NTSRepositoryComboBox = Nothing
    Me.at_idproj.NTSRepositoryItemCheck = Nothing
    Me.at_idproj.NTSRepositoryItemText = Nothing
    Me.at_idproj.OptionsColumn.AllowEdit = False
    Me.at_idproj.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.at_idproj.OptionsColumn.ReadOnly = True
    Me.at_idproj.OptionsFilter.AllowFilter = False
    '
    'xx_tempattpr
    '
    Me.xx_tempattpr.AppearanceCell.Options.UseBackColor = True
    Me.xx_tempattpr.AppearanceCell.Options.UseTextOptions = True
    Me.xx_tempattpr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_tempattpr.Caption = "Attrezz.previsto"
    Me.xx_tempattpr.Enabled = True
    Me.xx_tempattpr.FieldName = "xx_tempattpr"
    Me.xx_tempattpr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_tempattpr.Name = "xx_tempattpr"
    Me.xx_tempattpr.NTSRepositoryComboBox = Nothing
    Me.xx_tempattpr.NTSRepositoryItemCheck = Nothing
    Me.xx_tempattpr.NTSRepositoryItemText = Nothing
    Me.xx_tempattpr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_tempattpr.OptionsFilter.AllowFilter = False
    Me.xx_tempattpr.Visible = True
    Me.xx_tempattpr.VisibleIndex = 7
    '
    'xx_tempesepr
    '
    Me.xx_tempesepr.AppearanceCell.Options.UseBackColor = True
    Me.xx_tempesepr.AppearanceCell.Options.UseTextOptions = True
    Me.xx_tempesepr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_tempesepr.Caption = "Esecuz.previsto"
    Me.xx_tempesepr.Enabled = True
    Me.xx_tempesepr.FieldName = "xx_tempesepr"
    Me.xx_tempesepr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_tempesepr.Name = "xx_tempesepr"
    Me.xx_tempesepr.NTSRepositoryComboBox = Nothing
    Me.xx_tempesepr.NTSRepositoryItemCheck = Nothing
    Me.xx_tempesepr.NTSRepositoryItemText = Nothing
    Me.xx_tempesepr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_tempesepr.OptionsFilter.AllowFilter = False
    Me.xx_tempesepr.Visible = True
    Me.xx_tempesepr.VisibleIndex = 8
    '
    'xx_tempattees
    '
    Me.xx_tempattees.AppearanceCell.Options.UseBackColor = True
    Me.xx_tempattees.AppearanceCell.Options.UseTextOptions = True
    Me.xx_tempattees.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_tempattees.Caption = "Attrezz.effettuato"
    Me.xx_tempattees.Enabled = False
    Me.xx_tempattees.FieldName = "xx_tempattees"
    Me.xx_tempattees.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_tempattees.Name = "xx_tempattees"
    Me.xx_tempattees.NTSRepositoryComboBox = Nothing
    Me.xx_tempattees.NTSRepositoryItemCheck = Nothing
    Me.xx_tempattees.NTSRepositoryItemText = Nothing
    Me.xx_tempattees.OptionsColumn.AllowEdit = False
    Me.xx_tempattees.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_tempattees.OptionsColumn.ReadOnly = True
    Me.xx_tempattees.OptionsFilter.AllowFilter = False
    Me.xx_tempattees.Visible = True
    Me.xx_tempattees.VisibleIndex = 9
    '
    'xx_tempesees
    '
    Me.xx_tempesees.AppearanceCell.Options.UseBackColor = True
    Me.xx_tempesees.AppearanceCell.Options.UseTextOptions = True
    Me.xx_tempesees.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_tempesees.Caption = "Esecuz.effettuata"
    Me.xx_tempesees.Enabled = False
    Me.xx_tempesees.FieldName = "xx_tempesees"
    Me.xx_tempesees.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_tempesees.Name = "xx_tempesees"
    Me.xx_tempesees.NTSRepositoryComboBox = Nothing
    Me.xx_tempesees.NTSRepositoryItemCheck = Nothing
    Me.xx_tempesees.NTSRepositoryItemText = Nothing
    Me.xx_tempesees.OptionsColumn.AllowEdit = False
    Me.xx_tempesees.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_tempesees.OptionsColumn.ReadOnly = True
    Me.xx_tempesees.OptionsFilter.AllowFilter = False
    Me.xx_tempesees.Visible = True
    Me.xx_tempesees.VisibleIndex = 10
    '
    'at_flevas
    '
    Me.at_flevas.AppearanceCell.Options.UseBackColor = True
    Me.at_flevas.AppearanceCell.Options.UseTextOptions = True
    Me.at_flevas.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.at_flevas.Caption = "Eseguito"
    Me.at_flevas.Enabled = True
    Me.at_flevas.FieldName = "at_flevas"
    Me.at_flevas.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.at_flevas.Name = "at_flevas"
    Me.at_flevas.NTSRepositoryComboBox = Nothing
    Me.at_flevas.NTSRepositoryItemCheck = Nothing
    Me.at_flevas.NTSRepositoryItemText = Nothing
    Me.at_flevas.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.at_flevas.OptionsFilter.AllowFilter = False
    Me.at_flevas.Visible = True
    Me.at_flevas.VisibleIndex = 11
    '
    'at_dtminima
    '
    Me.at_dtminima.AppearanceCell.Options.UseBackColor = True
    Me.at_dtminima.AppearanceCell.Options.UseTextOptions = True
    Me.at_dtminima.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.at_dtminima.Caption = "Dt.min.inizio lav."
    Me.at_dtminima.Enabled = True
    Me.at_dtminima.FieldName = "at_dtminima"
    Me.at_dtminima.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.at_dtminima.Name = "at_dtminima"
    Me.at_dtminima.NTSRepositoryComboBox = Nothing
    Me.at_dtminima.NTSRepositoryItemCheck = Nothing
    Me.at_dtminima.NTSRepositoryItemText = Nothing
    Me.at_dtminima.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.at_dtminima.OptionsFilter.AllowFilter = False
    Me.at_dtminima.Visible = True
    Me.at_dtminima.VisibleIndex = 12
    '
    'as_controp
    '
    Me.as_controp.AppearanceCell.Options.UseBackColor = True
    Me.as_controp.AppearanceCell.Options.UseTextOptions = True
    Me.as_controp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.as_controp.Caption = "Contropartita"
    Me.as_controp.Enabled = True
    Me.as_controp.FieldName = "as_controp"
    Me.as_controp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.as_controp.Name = "as_controp"
    Me.as_controp.NTSRepositoryComboBox = Nothing
    Me.as_controp.NTSRepositoryItemCheck = Nothing
    Me.as_controp.NTSRepositoryItemText = Nothing
    Me.as_controp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.as_controp.OptionsFilter.AllowFilter = False
    Me.as_controp.Visible = True
    Me.as_controp.VisibleIndex = 13
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
    Me.xx_controp.NTSRepositoryItemText = Nothing
    Me.xx_controp.OptionsColumn.AllowEdit = False
    Me.xx_controp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_controp.OptionsColumn.ReadOnly = True
    Me.xx_controp.OptionsFilter.AllowFilter = False
    Me.xx_controp.Visible = True
    Me.xx_controp.VisibleIndex = 14
    '
    'at_suanno
    '
    Me.at_suanno.AppearanceCell.Options.UseBackColor = True
    Me.at_suanno.AppearanceCell.Options.UseTextOptions = True
    Me.at_suanno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.at_suanno.Caption = "Anno succ."
    Me.at_suanno.Enabled = True
    Me.at_suanno.FieldName = "at_suanno"
    Me.at_suanno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.at_suanno.Name = "at_suanno"
    Me.at_suanno.NTSRepositoryComboBox = Nothing
    Me.at_suanno.NTSRepositoryItemCheck = Nothing
    Me.at_suanno.NTSRepositoryItemText = Nothing
    Me.at_suanno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.at_suanno.OptionsFilter.AllowFilter = False
    Me.at_suanno.Visible = True
    Me.at_suanno.VisibleIndex = 15
    '
    'at_suserie
    '
    Me.at_suserie.AppearanceCell.Options.UseBackColor = True
    Me.at_suserie.AppearanceCell.Options.UseTextOptions = True
    Me.at_suserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.at_suserie.Caption = "Serie succ."
    Me.at_suserie.Enabled = True
    Me.at_suserie.FieldName = "at_suserie"
    Me.at_suserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.at_suserie.Name = "at_suserie"
    Me.at_suserie.NTSRepositoryComboBox = Nothing
    Me.at_suserie.NTSRepositoryItemCheck = Nothing
    Me.at_suserie.NTSRepositoryItemText = Nothing
    Me.at_suserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.at_suserie.OptionsFilter.AllowFilter = False
    Me.at_suserie.Visible = True
    Me.at_suserie.VisibleIndex = 16
    '
    'at_sunumord
    '
    Me.at_sunumord.AppearanceCell.Options.UseBackColor = True
    Me.at_sunumord.AppearanceCell.Options.UseTextOptions = True
    Me.at_sunumord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.at_sunumord.Caption = "N.Ord.succ."
    Me.at_sunumord.Enabled = True
    Me.at_sunumord.FieldName = "at_sunumord"
    Me.at_sunumord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.at_sunumord.Name = "at_sunumord"
    Me.at_sunumord.NTSRepositoryComboBox = Nothing
    Me.at_sunumord.NTSRepositoryItemCheck = Nothing
    Me.at_sunumord.NTSRepositoryItemText = Nothing
    Me.at_sunumord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.at_sunumord.OptionsFilter.AllowFilter = False
    Me.at_sunumord.Visible = True
    Me.at_sunumord.VisibleIndex = 17
    '
    'at_suriga
    '
    Me.at_suriga.AppearanceCell.Options.UseBackColor = True
    Me.at_suriga.AppearanceCell.Options.UseTextOptions = True
    Me.at_suriga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.at_suriga.Caption = "Riga succ."
    Me.at_suriga.Enabled = True
    Me.at_suriga.FieldName = "at_suriga"
    Me.at_suriga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.at_suriga.Name = "at_suriga"
    Me.at_suriga.NTSRepositoryComboBox = Nothing
    Me.at_suriga.NTSRepositoryItemCheck = Nothing
    Me.at_suriga.NTSRepositoryItemText = Nothing
    Me.at_suriga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.at_suriga.OptionsFilter.AllowFilter = False
    Me.at_suriga.Visible = True
    Me.at_suriga.VisibleIndex = 18
    '
    'at_sufase
    '
    Me.at_sufase.AppearanceCell.Options.UseBackColor = True
    Me.at_sufase.AppearanceCell.Options.UseTextOptions = True
    Me.at_sufase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.at_sufase.Caption = "Fase succ."
    Me.at_sufase.Enabled = True
    Me.at_sufase.FieldName = "at_sufase"
    Me.at_sufase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.at_sufase.Name = "at_sufase"
    Me.at_sufase.NTSRepositoryComboBox = Nothing
    Me.at_sufase.NTSRepositoryItemCheck = Nothing
    Me.at_sufase.NTSRepositoryItemText = Nothing
    Me.at_sufase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.at_sufase.OptionsFilter.AllowFilter = False
    Me.at_sufase.Visible = True
    Me.at_sufase.VisibleIndex = 19
    '
    'at_relazsuc
    '
    Me.at_relazsuc.AppearanceCell.Options.UseBackColor = True
    Me.at_relazsuc.AppearanceCell.Options.UseTextOptions = True
    Me.at_relazsuc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.at_relazsuc.Caption = "Relaz.succ."
    Me.at_relazsuc.Enabled = True
    Me.at_relazsuc.FieldName = "at_relazsuc"
    Me.at_relazsuc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.at_relazsuc.Name = "at_relazsuc"
    Me.at_relazsuc.NTSRepositoryComboBox = Nothing
    Me.at_relazsuc.NTSRepositoryItemCheck = Nothing
    Me.at_relazsuc.NTSRepositoryItemText = Nothing
    Me.at_relazsuc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.at_relazsuc.OptionsFilter.AllowFilter = False
    Me.at_relazsuc.Visible = True
    Me.at_relazsuc.VisibleIndex = 20
    '
    'at_qtapr
    '
    Me.at_qtapr.AppearanceCell.Options.UseBackColor = True
    Me.at_qtapr.AppearanceCell.Options.UseTextOptions = True
    Me.at_qtapr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.at_qtapr.Caption = "Qtà prevista"
    Me.at_qtapr.Enabled = True
    Me.at_qtapr.FieldName = "at_qtapr"
    Me.at_qtapr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.at_qtapr.Name = "at_qtapr"
    Me.at_qtapr.NTSRepositoryComboBox = Nothing
    Me.at_qtapr.NTSRepositoryItemCheck = Nothing
    Me.at_qtapr.NTSRepositoryItemText = Nothing
    Me.at_qtapr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.at_qtapr.OptionsFilter.AllowFilter = False
    Me.at_qtapr.Visible = True
    Me.at_qtapr.VisibleIndex = 21
    '
    'at_qtaes
    '
    Me.at_qtaes.AppearanceCell.Options.UseBackColor = True
    Me.at_qtaes.AppearanceCell.Options.UseTextOptions = True
    Me.at_qtaes.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.at_qtaes.Caption = "Qtà trattata"
    Me.at_qtaes.Enabled = False
    Me.at_qtaes.FieldName = "at_qtaes"
    Me.at_qtaes.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.at_qtaes.Name = "at_qtaes"
    Me.at_qtaes.NTSRepositoryComboBox = Nothing
    Me.at_qtaes.NTSRepositoryItemCheck = Nothing
    Me.at_qtaes.NTSRepositoryItemText = Nothing
    Me.at_qtaes.OptionsColumn.AllowEdit = False
    Me.at_qtaes.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.at_qtaes.OptionsColumn.ReadOnly = True
    Me.at_qtaes.OptionsFilter.AllowFilter = False
    Me.at_qtaes.Visible = True
    Me.at_qtaes.VisibleIndex = 22
    '
    'at_datcons
    '
    Me.at_datcons.AppearanceCell.Options.UseBackColor = True
    Me.at_datcons.AppearanceCell.Options.UseTextOptions = True
    Me.at_datcons.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.at_datcons.Caption = "Data consegna"
    Me.at_datcons.Enabled = True
    Me.at_datcons.FieldName = "at_datcons"
    Me.at_datcons.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.at_datcons.Name = "at_datcons"
    Me.at_datcons.NTSRepositoryComboBox = Nothing
    Me.at_datcons.NTSRepositoryItemCheck = Nothing
    Me.at_datcons.NTSRepositoryItemText = Nothing
    Me.at_datcons.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.at_datcons.OptionsFilter.AllowFilter = False
    Me.at_datcons.Visible = True
    Me.at_datcons.VisibleIndex = 23
    '
    'at_terzista
    '
    Me.at_terzista.AppearanceCell.Options.UseBackColor = True
    Me.at_terzista.AppearanceCell.Options.UseTextOptions = True
    Me.at_terzista.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.at_terzista.Caption = "Terzista"
    Me.at_terzista.Enabled = True
    Me.at_terzista.FieldName = "at_terzista"
    Me.at_terzista.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.at_terzista.Name = "at_terzista"
    Me.at_terzista.NTSRepositoryComboBox = Nothing
    Me.at_terzista.NTSRepositoryItemCheck = Nothing
    Me.at_terzista.NTSRepositoryItemText = Nothing
    Me.at_terzista.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.at_terzista.OptionsFilter.AllowFilter = False
    Me.at_terzista.Visible = True
    Me.at_terzista.VisibleIndex = 24
    '
    'at_suolprogr
    '
    Me.at_suolprogr.AppearanceCell.Options.UseBackColor = True
    Me.at_suolprogr.AppearanceCell.Options.UseTextOptions = True
    Me.at_suolprogr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.at_suolprogr.Caption = "Num. proposta succ."
    Me.at_suolprogr.Enabled = True
    Me.at_suolprogr.FieldName = "at_suolprogr"
    Me.at_suolprogr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.at_suolprogr.Name = "at_suolprogr"
    Me.at_suolprogr.NTSRepositoryComboBox = Nothing
    Me.at_suolprogr.NTSRepositoryItemCheck = Nothing
    Me.at_suolprogr.NTSRepositoryItemText = Nothing
    Me.at_suolprogr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.at_suolprogr.OptionsFilter.AllowFilter = False
    Me.at_suolprogr.Visible = True
    Me.at_suolprogr.VisibleIndex = 25
    '
    'at_note
    '
    Me.at_note.AppearanceCell.Options.UseBackColor = True
    Me.at_note.AppearanceCell.Options.UseTextOptions = True
    Me.at_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.at_note.Caption = "Note"
    Me.at_note.Enabled = True
    Me.at_note.FieldName = "at_note"
    Me.at_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.at_note.Name = "at_note"
    Me.at_note.NTSRepositoryComboBox = Nothing
    Me.at_note.NTSRepositoryItemCheck = Nothing
    Me.at_note.NTSRepositoryItemText = Nothing
    Me.at_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.at_note.OptionsFilter.AllowFilter = False
    Me.at_note.Visible = True
    Me.at_note.VisibleIndex = 26
    '
    'at_riga
    '
    Me.at_riga.AppearanceCell.Options.UseBackColor = True
    Me.at_riga.AppearanceCell.Options.UseTextOptions = True
    Me.at_riga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.at_riga.Caption = "Riga"
    Me.at_riga.Enabled = False
    Me.at_riga.FieldName = "at_riga"
    Me.at_riga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.at_riga.Name = "at_riga"
    Me.at_riga.NTSRepositoryComboBox = Nothing
    Me.at_riga.NTSRepositoryItemCheck = Nothing
    Me.at_riga.NTSRepositoryItemText = Nothing
    Me.at_riga.OptionsColumn.AllowEdit = False
    Me.at_riga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.at_riga.OptionsColumn.ReadOnly = True
    Me.at_riga.OptionsFilter.AllowFilter = False
    '
    'as_percent
    '
    Me.as_percent.AppearanceCell.Options.UseBackColor = True
    Me.as_percent.AppearanceCell.Options.UseTextOptions = True
    Me.as_percent.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.as_percent.Caption = "Percentuale"
    Me.as_percent.Enabled = False
    Me.as_percent.FieldName = "as_percent"
    Me.as_percent.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.as_percent.Name = "as_percent"
    Me.as_percent.NTSRepositoryComboBox = Nothing
    Me.as_percent.NTSRepositoryItemCheck = Nothing
    Me.as_percent.NTSRepositoryItemText = Nothing
    Me.as_percent.OptionsColumn.AllowEdit = False
    Me.as_percent.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.as_percent.OptionsColumn.ReadOnly = True
    Me.as_percent.OptionsFilter.AllowFilter = False
    Me.as_percent.Visible = True
    Me.as_percent.VisibleIndex = 27
    '
    'as_valore
    '
    Me.as_valore.AppearanceCell.Options.UseBackColor = True
    Me.as_valore.AppearanceCell.Options.UseTextOptions = True
    Me.as_valore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.as_valore.Caption = "Costo tot. macch."
    Me.as_valore.Enabled = True
    Me.as_valore.FieldName = "as_valore"
    Me.as_valore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.as_valore.Name = "as_valore"
    Me.as_valore.NTSRepositoryComboBox = Nothing
    Me.as_valore.NTSRepositoryItemCheck = Nothing
    Me.as_valore.NTSRepositoryItemText = Nothing
    Me.as_valore.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.as_valore.OptionsFilter.AllowFilter = False
    Me.as_valore.Visible = True
    Me.as_valore.VisibleIndex = 28
    '
    'as_valmo
    '
    Me.as_valmo.AppearanceCell.Options.UseBackColor = True
    Me.as_valmo.AppearanceCell.Options.UseTextOptions = True
    Me.as_valmo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.as_valmo.Caption = "Costo tot. manod."
    Me.as_valmo.Enabled = True
    Me.as_valmo.FieldName = "as_valmo"
    Me.as_valmo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.as_valmo.Name = "as_valmo"
    Me.as_valmo.NTSRepositoryComboBox = Nothing
    Me.as_valmo.NTSRepositoryItemCheck = Nothing
    Me.as_valmo.NTSRepositoryItemText = Nothing
    Me.as_valmo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.as_valmo.OptionsFilter.AllowFilter = False
    Me.as_valmo.Visible = True
    Me.as_valmo.VisibleIndex = 29
    '
    'as_contocontr
    '
    Me.as_contocontr.AppearanceCell.Options.UseBackColor = True
    Me.as_contocontr.AppearanceCell.Options.UseTextOptions = True
    Me.as_contocontr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.as_contocontr.Caption = "Conto controp."
    Me.as_contocontr.Enabled = False
    Me.as_contocontr.FieldName = "as_contocontr"
    Me.as_contocontr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.as_contocontr.Name = "as_contocontr"
    Me.as_contocontr.NTSRepositoryComboBox = Nothing
    Me.as_contocontr.NTSRepositoryItemCheck = Nothing
    Me.as_contocontr.NTSRepositoryItemText = Nothing
    Me.as_contocontr.OptionsColumn.AllowEdit = False
    Me.as_contocontr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.as_contocontr.OptionsColumn.ReadOnly = True
    Me.as_contocontr.OptionsFilter.AllowFilter = False
    Me.as_contocontr.Visible = True
    Me.as_contocontr.VisibleIndex = 30
    '
    'as_codcena
    '
    Me.as_codcena.AppearanceCell.Options.UseBackColor = True
    Me.as_codcena.AppearanceCell.Options.UseTextOptions = True
    Me.as_codcena.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.as_codcena.Caption = "Centro C.A."
    Me.as_codcena.Enabled = True
    Me.as_codcena.FieldName = "as_codcena"
    Me.as_codcena.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.as_codcena.Name = "as_codcena"
    Me.as_codcena.NTSRepositoryComboBox = Nothing
    Me.as_codcena.NTSRepositoryItemCheck = Nothing
    Me.as_codcena.NTSRepositoryItemText = Nothing
    Me.as_codcena.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.as_codcena.OptionsFilter.AllowFilter = False
    Me.as_codcena.Visible = True
    Me.as_codcena.VisibleIndex = 31
    '
    'as_cmacora
    '
    Me.as_cmacora.AppearanceCell.Options.UseBackColor = True
    Me.as_cmacora.AppearanceCell.Options.UseTextOptions = True
    Me.as_cmacora.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.as_cmacora.Caption = "Costo mach. orario"
    Me.as_cmacora.Enabled = True
    Me.as_cmacora.FieldName = "as_cmacora"
    Me.as_cmacora.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.as_cmacora.Name = "as_cmacora"
    Me.as_cmacora.NTSRepositoryComboBox = Nothing
    Me.as_cmacora.NTSRepositoryItemCheck = Nothing
    Me.as_cmacora.NTSRepositoryItemText = Nothing
    Me.as_cmacora.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.as_cmacora.OptionsFilter.AllowFilter = False
    '
    'as_pagaora
    '
    Me.as_pagaora.AppearanceCell.Options.UseBackColor = True
    Me.as_pagaora.AppearanceCell.Options.UseTextOptions = True
    Me.as_pagaora.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.as_pagaora.Caption = "Costo manod. orario"
    Me.as_pagaora.Enabled = True
    Me.as_pagaora.FieldName = "as_pagaora"
    Me.as_pagaora.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.as_pagaora.Name = "as_pagaora"
    Me.as_pagaora.NTSRepositoryComboBox = Nothing
    Me.as_pagaora.NTSRepositoryItemCheck = Nothing
    Me.as_pagaora.NTSRepositoryItemText = Nothing
    Me.as_pagaora.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.as_pagaora.OptionsFilter.AllowFilter = False
    '
    'as_cmacoratt
    '
    Me.as_cmacoratt.AppearanceCell.Options.UseBackColor = True
    Me.as_cmacoratt.AppearanceCell.Options.UseTextOptions = True
    Me.as_cmacoratt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.as_cmacoratt.Caption = "Costo mach. orario attrez."
    Me.as_cmacoratt.Enabled = True
    Me.as_cmacoratt.FieldName = "as_cmacoratt"
    Me.as_cmacoratt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.as_cmacoratt.Name = "as_cmacoratt"
    Me.as_cmacoratt.NTSRepositoryComboBox = Nothing
    Me.as_cmacoratt.NTSRepositoryItemCheck = Nothing
    Me.as_cmacoratt.NTSRepositoryItemText = Nothing
    Me.as_cmacoratt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.as_cmacoratt.OptionsFilter.AllowFilter = False
    '
    'as_pagaoratt
    '
    Me.as_pagaoratt.AppearanceCell.Options.UseBackColor = True
    Me.as_pagaoratt.AppearanceCell.Options.UseTextOptions = True
    Me.as_pagaoratt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.as_pagaoratt.Caption = "Costo manod. orario attrez."
    Me.as_pagaoratt.Enabled = True
    Me.as_pagaoratt.FieldName = "as_pagaoratt"
    Me.as_pagaoratt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.as_pagaoratt.Name = "as_pagaoratt"
    Me.as_pagaoratt.NTSRepositoryComboBox = Nothing
    Me.as_pagaoratt.NTSRepositoryItemCheck = Nothing
    Me.as_pagaoratt.NTSRepositoryItemText = Nothing
    Me.as_pagaoratt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.as_pagaoratt.OptionsFilter.AllowFilter = False
    '
    'as_valorev
    '
    Me.as_valorev.AppearanceCell.Options.UseBackColor = True
    Me.as_valorev.AppearanceCell.Options.UseTextOptions = True
    Me.as_valorev.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.as_valorev.Caption = "Costo totale in val."
    Me.as_valorev.Enabled = False
    Me.as_valorev.FieldName = "as_valorev"
    Me.as_valorev.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.as_valorev.Name = "as_valorev"
    Me.as_valorev.NTSRepositoryComboBox = Nothing
    Me.as_valorev.NTSRepositoryItemCheck = Nothing
    Me.as_valorev.NTSRepositoryItemText = Nothing
    Me.as_valorev.OptionsColumn.AllowEdit = False
    Me.as_valorev.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.as_valorev.OptionsColumn.ReadOnly = True
    Me.as_valorev.OptionsFilter.AllowFilter = False
    Me.as_valorev.Visible = True
    Me.as_valorev.VisibleIndex = 32
    '
    'FRMORTTA1
    '
    Me.ClientSize = New System.Drawing.Size(740, 442)
    Me.Controls.Add(Me.grRighe)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMORTTA1"
    Me.Text = "LAVORAZIONI COLLEGATE"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grRighe, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvRighe, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overloads Function InitEntity(ByRef Menu As CLE__MENU, ByRef CleDocu As CLEMGDOCU, ByRef ds As DataSet) As Boolean
    oMenu = Menu
    oApp = oMenu.App
    oCleDocu = CleDocu
    DittaCorrente = oCleDocu.strDittaCorrente
    Me.GctlTipoDoc = "H"

    InitializeComponent()
    Me.MinimumSize = Me.Size

    '-------------------------------
    'leggo dal database i dati e collego il NTSBinding
    ds.AcceptChanges()
    oCleDocu.dsTtat = ds      'sia attivit che assris sono contenuti nello stesso datatable!!!!
    oCleDocu.dsTtas = ds
    oCleDocu.AttivitSetDataTable(DittaCorrente, oCleDocu.dsTtat.Tables("ATTIVIT"))
    dcTtat.DataSource = oCleDocu.dsTtat.Tables("ATTIVIT")
    oCleDocu.dsTtat.AcceptChanges()

    '------------------------------
    'collego gli eventi dell'entity
    AddHandler oCleDocu.RemoteEvent, AddressOf GestisciEventiEntity

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim i As Integer = 0
    Dim dttStasino As New DataTable()
    Dim dttKit As New DataTable()
    Dim dttOatipo As New DataTable()
    Dim dttTask As New DataTable()
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

      Dim dttRelaz As New DataTable()
      dttRelaz.Columns.Add("cod", GetType(String))
      dttRelaz.Columns.Add("val", GetType(String))
      dttRelaz.Rows.Add(New Object() {"FI", "Fine-Inizio"})
      dttRelaz.Rows.Add(New Object() {"II", "Inizio-Inizio"})
      dttRelaz.Rows.Add(New Object() {"IF", "Inizio-Fine"})
      dttRelaz.Rows.Add(New Object() {"FF", "Fine-Fine"})
      dttRelaz.AcceptChanges()

      grvRighe.NTSSetParam(oMenu, oApp.Tr(Me, 128589605282500000, "Lavorazioni collegate"))
      'attivit
      at_fase.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128589605288906250, "Fase"), "0", 4, 0, 9999)
      at_coddb.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128589605302968750, "Distinta base"), CLN__STD.CodartMaxLen, False)
      xx_coddb.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128589605317968750, "Descriz. distinta base"), 0, True)
      at_codlavo.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128589605335000000, "Cod. lavor."), tablavo)
      xx_codlavo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128589605349531250, "Descr. lavorazione"), 0, True)
      at_idproj.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128589605365156250, "Riga Project"), "0", 9, 0, 999999999)
      xx_tempattpr.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128589605382968750, "Attrezz.previsto"), "#,##0.0000", 9, 0, 999999999)
      xx_tempesepr.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128589605400468750, "Esecuz.previsto"), "#,##0.0000", 9, 0, 999999999)
      xx_tempattees.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128589605410468750, "Attrezz.effettuato"), "#,##0.0000", 9, 0, 999999999)
      xx_tempesees.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128589605420468750, "Esecuz.effettuata"), "#,##0.0000", 9, 0, 999999999)
      at_flevas.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128589605430312500, "Eseguito"), "S", "C")
      at_dtminima.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128589605439531250, "Dt.min.inizio lav."), False)
      at_suanno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128589605449843750, "Anno succ."), "0", 4, 0, 2099)
      at_suserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128589605458437500, "Serie succ."), CLN__STD.SerieMaxLen, False)
      at_sunumord.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128589605467343750, "N.Ord.succ."), "0", 9, 0, 999999999)
      at_suriga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128589605477343750, "Riga succ."), "0", 9, 0, 999999999)
      at_sufase.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128589605486718750, "Fase succ."), "0", 4, 0, 9999)
      at_relazsuc.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128589605495156250, "Relaz.succ."), dttRelaz, "val", "cod")
      at_qtapr.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128589605503906250, "Qtà prev.da lavor."), oApp.FormatQta, 15)
      at_qtaes.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128589605512343750, "Qtà trattata"), oApp.FormatQta, 15)
      at_datcons.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128589605520937500, "Data consegna"), False)
      at_terzista.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128589605530000000, "Terzista"), "S", "N")
      at_suolprogr.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128589605555000000, "Num. proposta succ."), "0", 9, 0, 999999999)
      at_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128589605592656250, "Note"), 0, True)
      xx_tempattprm.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128786511847498000, "Attrezz.previsto in minuti"), "0.00", 9, 0, 999999999)
      xx_tempeseprm.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128786511859354000, "Esecuz.previsto in minuti"), "0.00", 9, 0, 999999999)

      'assris
      as_codcent.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128590430177500000, "Centro di lavoro"), tabcent)
      xx_codcent.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128590430177656250, "Descr. centro"), 0, True)
      as_controp.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128590430180156250, "Contropartita"), tabcove)
      xx_controp.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128590430180312500, "Descr. controp."), 0, True)
      as_valore.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128590430180468750, "Costo tot. macch."), oApp.FormatImporti, 15)
      at_riga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128590430182500000, "Riga"), "0", 9, 0, 999999999)
      as_percent.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128590430182656250, "Percentuale"), oApp.FormatSconti, 6, 0, 100)
      as_valmo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128590430182812500, "Costo tot. manod."), oApp.FormatImporti, 15)
      as_contocontr.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128590430182968750, "Conto controp."), tabanagra)
      as_codcena.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128590430183125000, "Centro C.A."), tabcena)
      as_cmacora.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128590430183281250, "Costo mach. orario"), oApp.FormatImporti, 15)
      as_pagaora.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128590430183437500, "Costo manod. orario"), oApp.FormatImporti, 15)
      as_cmacoratt.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128590430183593750, "Costo mach. orario attrez."), oApp.FormatImporti, 15)
      as_pagaoratt.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128590430183750000, "Costo manod. orario attrez."), oApp.FormatImporti, 15)
      as_valorev.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128590430183906250, "Costo totale in val."), oApp.FormatImpVal, 15)

      at_fase.NTSSetRichiesto()
      at_codlavo.NTSSetRichiesto()
      as_codcent.NTSSetRichiesto()
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
  Public Overridable Sub FRMORTTA1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      grRighe.DataSource = dcTtat
      at_coddb.Visible = False
      xx_coddb.Visible = False

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      grvRighe_NTSFocusedRowChanged(grvRighe, Nothing)

      If oCleDocu.bTerzista Then
        as_valore.Enabled = False
        as_valorev.Enabled = False
      Else
        GctlSetVisEnab(as_valore, False)
        as_valorev.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMORTTA1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try

  End Sub

  Public Overridable Sub FRMORTTA1_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    'salvo l'impostazione della griglia. devo farlo anche al cambio del tipo documento (ad esempio in bsveboll
    Try
      dcTtat.Dispose()
      oCleDocu.dsTtat.Dispose()
      oCleDocu.dsTtas.Dispose()
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
                oApp.MsgBoxErr(oApp.Tr(Me, 129048532621270939, "Posizionarsi prima nella griglia e selezionare la riga da cancellare"))
        Return
      End If
      Me.Cursor = Cursors.WaitCursor
      If Not oCleDocu.AttivitTestPreCancella(dcTtat.Position) Then Return
      If Not grvRighe.NTSDeleteRigaCorrente(dcTtat, True, dtrDeleted) Then Return
      If Not oCleDocu.AttivitRecordSalva(dcTtat.Position, True, dtrDeleted) Then Return

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
      If Not grvRighe.NTSRipristinaRigaCorrenteBefore(dcTtat, True) Then Return
      oCleDocu.AttivitRipristina(dcTtat.Position, dcTtat.Filter)
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
    'Dim oParam As New CLE__PATB

    Try
      ''------------------------------------
      ''zoom standard
      'ctrlTmp = NTSFindControlForZoom()
      'If ctrlTmp Is Nothing Then Return
      'If ctrlTmp.GetType.ToString.IndexOf("NTSGrid") > -1 Then
      '  SetFastZoom(NTSCStr(CType(CType(ctrlTmp, NTSGrid).DefaultView, NTSGridView).EditingValue), oParam)    'abilito la gestione dello zoom veloce


      '------------------------------------
      'zoom standard di textbox e griglia
      NTSCallStandardZoom()
      'End If

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

  Public Overridable Sub grvRighe_NTSFocusedColumnChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles grvRighe.NTSFocusedColumnChanged
    Dim strTmp As String = ""
    Try
      If oCleDocu.dsTtat Is Nothing Then Return
      If oCleDocu.dsTtat.Tables("ATTIVIT") Is Nothing Then Return
      If oCleDocu.dsTtat.Tables("ATTIVIT").Rows.Count = 0 Then Return
      If grvRighe.GetFocusedRowCellValue(at_coddb) Is Nothing Then Return

      strTmp = TestSerieMaxLen(NTSCStr(grvRighe.GetFocusedRowCellValue(at_suserie)), False)
      If strTmp <> NTSCStr(grvRighe.GetFocusedRowCellValue(at_suserie)) Then grvRighe.SetFocusedRowCellValue(at_suserie, strTmp)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvRighe_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvRighe.NTSFocusedRowChanged
    '-------------------------------------------------
    'carico le unità di misura al cambio di riga
    Dim strTmp As String = ""

    Try
      If oCleDocu.dsTtat Is Nothing Then Return
      If oCleDocu.dsTtat.Tables("ATTIVIT") Is Nothing Then Return
      If oCleDocu.dsTtat.Tables("ATTIVIT").Rows.Count = 0 Then Return
      If grvRighe.GetFocusedRowCellValue(at_coddb) Is Nothing Then Return

      strTmp = TestSerieMaxLen(NTSCStr(grvRighe.GetFocusedRowCellValue(at_suserie)), False)
      If strTmp <> NTSCStr(grvRighe.GetFocusedRowCellValue(at_suserie)) Then grvRighe.SetFocusedRowCellValue(at_suserie, strTmp)

      '-------------------------------------------------------
      'se sono su una nuova riga mi posiziono a sinistra
      If NTSCInt(grvRighe.GetFocusedRowCellValue(at_riga)) = 0 Then
        GctlSetVisEnab(at_fase, False)
        'grvRighe.FocusedColumn = at_fase
      Else
        at_fase.Enabled = False
      End If    'If grvRighe.GetFocusedRowCellValue(at_riga).FormattedValue.ToString = "" Then

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

  Public Overridable Function Salva() As Boolean
    Try
      If Not oCleDocu.AttivitRecordSalva(dcTtat.Position, False, Nothing) Then Return False
      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function



End Class
