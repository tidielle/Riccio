Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__AIVA
  Public oCleAnaz As CLE__ANAZ
  Public oCallParams As CLE__CLDP
  Public dsAiva As DataSet
  Public dcAiva As BindingSource = New BindingSource()

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
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents grAiva As NTSInformatica.NTSGrid
  Public WithEvents grvAiva As NTSInformatica.NTSGridView
  Public WithEvents tb_codatti As NTSInformatica.NTSGridColumn
  Public WithEvents tb_desatti As NTSInformatica.NTSGridColumn
  Public WithEvents tb_ventilcorr As NTSInformatica.NTSGridColumn
  Public WithEvents tb_agric As NTSInformatica.NTSGridColumn
  Public WithEvents tb_74ter As NTSInformatica.NTSGridColumn
  Public WithEvents tb_aliq74ter As NTSInformatica.NTSGridColumn
  Public WithEvents tb_aliqesen As NTSInformatica.NTSGridColumn
  Public WithEvents tb_atmentri As NTSInformatica.NTSGridColumn
  Public WithEvents tb_acodista As NTSInformatica.NTSGridColumn
  Public WithEvents tb_dtinat As NTSInformatica.NTSGridColumn
  Public WithEvents tb_dtfiat As NTSInformatica.NTSGridColumn
  Public WithEvents tb_maggint As NTSInformatica.NTSGridColumn
  Public WithEvents tb_prorata As NTSInformatica.NTSGridColumn
  Public WithEvents tb_rilandiv As NTSInformatica.NTSGridColumn
  Public WithEvents tb_beniusat As NTSInformatica.NTSGridColumn
  Public WithEvents tb_regautotr As NTSInformatica.NTSGridColumn
  Public WithEvents tb_volaffpre As NTSInformatica.NTSGridColumn
  Public WithEvents tb_codusatdef As NTSInformatica.NTSGridColumn
  Public WithEvents xx_acodista As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codusatdef As NTSInformatica.NTSGridColumn
  Public WithEvents tb_aliq74ter2 As NTSInformatica.NTSGridColumn
  Public WithEvents tb_datfinaliq742 As NTSInformatica.NTSGridColumn


  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__AIVA))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.tb_codatti = New NTSInformatica.NTSGridColumn
    Me.tb_desatti = New NTSInformatica.NTSGridColumn
    Me.tb_ventilcorr = New NTSInformatica.NTSGridColumn
    Me.tb_agric = New NTSInformatica.NTSGridColumn
    Me.tb_74ter = New NTSInformatica.NTSGridColumn
    Me.tb_aliq74ter = New NTSInformatica.NTSGridColumn
    Me.tb_aliqesen = New NTSInformatica.NTSGridColumn
    Me.tb_atmentri = New NTSInformatica.NTSGridColumn
    Me.tb_acodista = New NTSInformatica.NTSGridColumn
    Me.tb_dtinat = New NTSInformatica.NTSGridColumn
    Me.tb_dtfiat = New NTSInformatica.NTSGridColumn
    Me.tb_maggint = New NTSInformatica.NTSGridColumn
    Me.tb_prorata = New NTSInformatica.NTSGridColumn
    Me.tb_rilandiv = New NTSInformatica.NTSGridColumn
    Me.tb_beniusat = New NTSInformatica.NTSGridColumn
    Me.tb_regautotr = New NTSInformatica.NTSGridColumn
    Me.tb_volaffpre = New NTSInformatica.NTSGridColumn
    Me.tb_codusatdef = New NTSInformatica.NTSGridColumn
    Me.xx_acodista = New NTSInformatica.NTSGridColumn
    Me.xx_codusatdef = New NTSInformatica.NTSGridColumn
    Me.grvAiva = New NTSInformatica.NTSGridView
    Me.tb_subforn = New NTSInformatica.NTSGridColumn
    Me.tb_aliq74ter2 = New NTSInformatica.NTSGridColumn
    Me.tb_datfinaliq742 = New NTSInformatica.NTSGridColumn
    Me.grAiva = New NTSInformatica.NTSGrid
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvAiva, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grAiva, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom})
    Me.NtsBarManager1.MaxItemId = 17
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbRipristina.Id = 2
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.Id = 3
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
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 11
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 12
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'tb_codatti
    '
    Me.tb_codatti.AppearanceCell.Options.UseBackColor = True
    Me.tb_codatti.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codatti.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codatti.Caption = "Codice"
    Me.tb_codatti.Enabled = True
    Me.tb_codatti.FieldName = "tb_codatti"
    Me.tb_codatti.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codatti.Name = "tb_codatti"
    Me.tb_codatti.NTSRepositoryComboBox = Nothing
    Me.tb_codatti.NTSRepositoryItemCheck = Nothing
    Me.tb_codatti.NTSRepositoryItemMemo = Nothing
    Me.tb_codatti.NTSRepositoryItemText = Nothing
    Me.tb_codatti.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codatti.OptionsFilter.AllowFilter = False
    Me.tb_codatti.Visible = True
    Me.tb_codatti.VisibleIndex = 0
    Me.tb_codatti.Width = 70
    '
    'tb_desatti
    '
    Me.tb_desatti.AppearanceCell.Options.UseBackColor = True
    Me.tb_desatti.AppearanceCell.Options.UseTextOptions = True
    Me.tb_desatti.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_desatti.Caption = "Descrizione"
    Me.tb_desatti.Enabled = True
    Me.tb_desatti.FieldName = "tb_desatti"
    Me.tb_desatti.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_desatti.Name = "tb_desatti"
    Me.tb_desatti.NTSRepositoryComboBox = Nothing
    Me.tb_desatti.NTSRepositoryItemCheck = Nothing
    Me.tb_desatti.NTSRepositoryItemMemo = Nothing
    Me.tb_desatti.NTSRepositoryItemText = Nothing
    Me.tb_desatti.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_desatti.OptionsFilter.AllowFilter = False
    Me.tb_desatti.Visible = True
    Me.tb_desatti.VisibleIndex = 1
    Me.tb_desatti.Width = 70
    '
    'tb_ventilcorr
    '
    Me.tb_ventilcorr.AppearanceCell.Options.UseBackColor = True
    Me.tb_ventilcorr.AppearanceCell.Options.UseTextOptions = True
    Me.tb_ventilcorr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_ventilcorr.Caption = "Ventilaz. corrispettivi"
    Me.tb_ventilcorr.Enabled = True
    Me.tb_ventilcorr.FieldName = "tb_ventilcorr"
    Me.tb_ventilcorr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_ventilcorr.Name = "tb_ventilcorr"
    Me.tb_ventilcorr.NTSRepositoryComboBox = Nothing
    Me.tb_ventilcorr.NTSRepositoryItemCheck = Nothing
    Me.tb_ventilcorr.NTSRepositoryItemMemo = Nothing
    Me.tb_ventilcorr.NTSRepositoryItemText = Nothing
    Me.tb_ventilcorr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_ventilcorr.OptionsFilter.AllowFilter = False
    Me.tb_ventilcorr.Visible = True
    Me.tb_ventilcorr.VisibleIndex = 2
    Me.tb_ventilcorr.Width = 70
    '
    'tb_agric
    '
    Me.tb_agric.AppearanceCell.Options.UseBackColor = True
    Me.tb_agric.AppearanceCell.Options.UseTextOptions = True
    Me.tb_agric.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_agric.Caption = "Regime agricolo"
    Me.tb_agric.Enabled = True
    Me.tb_agric.FieldName = "tb_agric"
    Me.tb_agric.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_agric.Name = "tb_agric"
    Me.tb_agric.NTSRepositoryComboBox = Nothing
    Me.tb_agric.NTSRepositoryItemCheck = Nothing
    Me.tb_agric.NTSRepositoryItemMemo = Nothing
    Me.tb_agric.NTSRepositoryItemText = Nothing
    Me.tb_agric.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_agric.OptionsFilter.AllowFilter = False
    Me.tb_agric.Visible = True
    Me.tb_agric.VisibleIndex = 3
    Me.tb_agric.Width = 70
    '
    'tb_74ter
    '
    Me.tb_74ter.AppearanceCell.Options.UseBackColor = True
    Me.tb_74ter.AppearanceCell.Options.UseTextOptions = True
    Me.tb_74ter.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_74ter.Caption = "Reg. art. 74 ter"
    Me.tb_74ter.Enabled = True
    Me.tb_74ter.FieldName = "tb_74ter"
    Me.tb_74ter.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_74ter.Name = "tb_74ter"
    Me.tb_74ter.NTSRepositoryComboBox = Nothing
    Me.tb_74ter.NTSRepositoryItemCheck = Nothing
    Me.tb_74ter.NTSRepositoryItemMemo = Nothing
    Me.tb_74ter.NTSRepositoryItemText = Nothing
    Me.tb_74ter.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_74ter.OptionsFilter.AllowFilter = False
    Me.tb_74ter.Visible = True
    Me.tb_74ter.VisibleIndex = 4
    Me.tb_74ter.Width = 70
    '
    'tb_aliq74ter
    '
    Me.tb_aliq74ter.AppearanceCell.Options.UseBackColor = True
    Me.tb_aliq74ter.AppearanceCell.Options.UseTextOptions = True
    Me.tb_aliq74ter.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_aliq74ter.Caption = "Aliq. 74 ter in vigore"
    Me.tb_aliq74ter.Enabled = True
    Me.tb_aliq74ter.FieldName = "tb_aliq74ter"
    Me.tb_aliq74ter.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_aliq74ter.Name = "tb_aliq74ter"
    Me.tb_aliq74ter.NTSRepositoryComboBox = Nothing
    Me.tb_aliq74ter.NTSRepositoryItemCheck = Nothing
    Me.tb_aliq74ter.NTSRepositoryItemMemo = Nothing
    Me.tb_aliq74ter.NTSRepositoryItemText = Nothing
    Me.tb_aliq74ter.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_aliq74ter.OptionsFilter.AllowFilter = False
    Me.tb_aliq74ter.Visible = True
    Me.tb_aliq74ter.VisibleIndex = 5
    Me.tb_aliq74ter.Width = 70
    '
    'tb_aliqesen
    '
    Me.tb_aliqesen.AppearanceCell.Options.UseBackColor = True
    Me.tb_aliqesen.AppearanceCell.Options.UseTextOptions = True
    Me.tb_aliqesen.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_aliqesen.Caption = "Perc. ind. op. esenti"
    Me.tb_aliqesen.Enabled = True
    Me.tb_aliqesen.FieldName = "tb_aliqesen"
    Me.tb_aliqesen.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_aliqesen.Name = "tb_aliqesen"
    Me.tb_aliqesen.NTSRepositoryComboBox = Nothing
    Me.tb_aliqesen.NTSRepositoryItemCheck = Nothing
    Me.tb_aliqesen.NTSRepositoryItemMemo = Nothing
    Me.tb_aliqesen.NTSRepositoryItemText = Nothing
    Me.tb_aliqesen.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_aliqesen.OptionsFilter.AllowFilter = False
    Me.tb_aliqesen.Visible = True
    Me.tb_aliqesen.VisibleIndex = 6
    Me.tb_aliqesen.Width = 70
    '
    'tb_atmentri
    '
    Me.tb_atmentri.AppearanceCell.Options.UseBackColor = True
    Me.tb_atmentri.AppearanceCell.Options.UseTextOptions = True
    Me.tb_atmentri.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_atmentri.Caption = "Periodicità"
    Me.tb_atmentri.Enabled = True
    Me.tb_atmentri.FieldName = "tb_atmentri"
    Me.tb_atmentri.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_atmentri.Name = "tb_atmentri"
    Me.tb_atmentri.NTSRepositoryComboBox = Nothing
    Me.tb_atmentri.NTSRepositoryItemCheck = Nothing
    Me.tb_atmentri.NTSRepositoryItemMemo = Nothing
    Me.tb_atmentri.NTSRepositoryItemText = Nothing
    Me.tb_atmentri.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_atmentri.OptionsFilter.AllowFilter = False
    Me.tb_atmentri.Visible = True
    Me.tb_atmentri.VisibleIndex = 7
    Me.tb_atmentri.Width = 70
    '
    'tb_acodista
    '
    Me.tb_acodista.AppearanceCell.Options.UseBackColor = True
    Me.tb_acodista.AppearanceCell.Options.UseTextOptions = True
    Me.tb_acodista.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_acodista.Caption = "Cod. ISTAT attività"
    Me.tb_acodista.Enabled = True
    Me.tb_acodista.FieldName = "tb_acodista"
    Me.tb_acodista.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_acodista.Name = "tb_acodista"
    Me.tb_acodista.NTSRepositoryComboBox = Nothing
    Me.tb_acodista.NTSRepositoryItemCheck = Nothing
    Me.tb_acodista.NTSRepositoryItemMemo = Nothing
    Me.tb_acodista.NTSRepositoryItemText = Nothing
    Me.tb_acodista.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_acodista.OptionsFilter.AllowFilter = False
    Me.tb_acodista.Visible = True
    Me.tb_acodista.VisibleIndex = 8
    Me.tb_acodista.Width = 70
    '
    'tb_dtinat
    '
    Me.tb_dtinat.AppearanceCell.Options.UseBackColor = True
    Me.tb_dtinat.AppearanceCell.Options.UseTextOptions = True
    Me.tb_dtinat.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_dtinat.Caption = "Data inizio attività"
    Me.tb_dtinat.Enabled = True
    Me.tb_dtinat.FieldName = "tb_dtinat"
    Me.tb_dtinat.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_dtinat.Name = "tb_dtinat"
    Me.tb_dtinat.NTSRepositoryComboBox = Nothing
    Me.tb_dtinat.NTSRepositoryItemCheck = Nothing
    Me.tb_dtinat.NTSRepositoryItemMemo = Nothing
    Me.tb_dtinat.NTSRepositoryItemText = Nothing
    Me.tb_dtinat.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_dtinat.OptionsFilter.AllowFilter = False
    Me.tb_dtinat.Visible = True
    Me.tb_dtinat.VisibleIndex = 10
    Me.tb_dtinat.Width = 70
    '
    'tb_dtfiat
    '
    Me.tb_dtfiat.AppearanceCell.Options.UseBackColor = True
    Me.tb_dtfiat.AppearanceCell.Options.UseTextOptions = True
    Me.tb_dtfiat.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_dtfiat.Caption = "Data fine attività"
    Me.tb_dtfiat.Enabled = True
    Me.tb_dtfiat.FieldName = "tb_dtfiat"
    Me.tb_dtfiat.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_dtfiat.Name = "tb_dtfiat"
    Me.tb_dtfiat.NTSRepositoryComboBox = Nothing
    Me.tb_dtfiat.NTSRepositoryItemCheck = Nothing
    Me.tb_dtfiat.NTSRepositoryItemMemo = Nothing
    Me.tb_dtfiat.NTSRepositoryItemText = Nothing
    Me.tb_dtfiat.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_dtfiat.OptionsFilter.AllowFilter = False
    Me.tb_dtfiat.Visible = True
    Me.tb_dtfiat.VisibleIndex = 11
    Me.tb_dtfiat.Width = 70
    '
    'tb_maggint
    '
    Me.tb_maggint.AppearanceCell.Options.UseBackColor = True
    Me.tb_maggint.AppearanceCell.Options.UseTextOptions = True
    Me.tb_maggint.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_maggint.Caption = "Maggioraz. per interessi"
    Me.tb_maggint.Enabled = True
    Me.tb_maggint.FieldName = "tb_maggint"
    Me.tb_maggint.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_maggint.Name = "tb_maggint"
    Me.tb_maggint.NTSRepositoryComboBox = Nothing
    Me.tb_maggint.NTSRepositoryItemCheck = Nothing
    Me.tb_maggint.NTSRepositoryItemMemo = Nothing
    Me.tb_maggint.NTSRepositoryItemText = Nothing
    Me.tb_maggint.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_maggint.OptionsFilter.AllowFilter = False
    Me.tb_maggint.Width = 70
    '
    'tb_prorata
    '
    Me.tb_prorata.AppearanceCell.Options.UseBackColor = True
    Me.tb_prorata.AppearanceCell.Options.UseTextOptions = True
    Me.tb_prorata.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_prorata.Caption = "Soggetto a pro-rata"
    Me.tb_prorata.Enabled = True
    Me.tb_prorata.FieldName = "tb_prorata"
    Me.tb_prorata.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_prorata.Name = "tb_prorata"
    Me.tb_prorata.NTSRepositoryComboBox = Nothing
    Me.tb_prorata.NTSRepositoryItemCheck = Nothing
    Me.tb_prorata.NTSRepositoryItemMemo = Nothing
    Me.tb_prorata.NTSRepositoryItemText = Nothing
    Me.tb_prorata.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_prorata.OptionsFilter.AllowFilter = False
    Me.tb_prorata.Visible = True
    Me.tb_prorata.VisibleIndex = 12
    Me.tb_prorata.Width = 70
    '
    'tb_rilandiv
    '
    Me.tb_rilandiv.AppearanceCell.Options.UseBackColor = True
    Me.tb_rilandiv.AppearanceCell.Options.UseTextOptions = True
    Me.tb_rilandiv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_rilandiv.Caption = "Rilancio dichiaraz. IVA"
    Me.tb_rilandiv.Enabled = True
    Me.tb_rilandiv.FieldName = "tb_rilandiv"
    Me.tb_rilandiv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_rilandiv.Name = "tb_rilandiv"
    Me.tb_rilandiv.NTSRepositoryComboBox = Nothing
    Me.tb_rilandiv.NTSRepositoryItemCheck = Nothing
    Me.tb_rilandiv.NTSRepositoryItemMemo = Nothing
    Me.tb_rilandiv.NTSRepositoryItemText = Nothing
    Me.tb_rilandiv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_rilandiv.OptionsFilter.AllowFilter = False
    Me.tb_rilandiv.Visible = True
    Me.tb_rilandiv.VisibleIndex = 13
    Me.tb_rilandiv.Width = 70
    '
    'tb_beniusat
    '
    Me.tb_beniusat.AppearanceCell.Options.UseBackColor = True
    Me.tb_beniusat.AppearanceCell.Options.UseTextOptions = True
    Me.tb_beniusat.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_beniusat.Caption = "Beni usati"
    Me.tb_beniusat.Enabled = True
    Me.tb_beniusat.FieldName = "tb_beniusat"
    Me.tb_beniusat.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_beniusat.Name = "tb_beniusat"
    Me.tb_beniusat.NTSRepositoryComboBox = Nothing
    Me.tb_beniusat.NTSRepositoryItemCheck = Nothing
    Me.tb_beniusat.NTSRepositoryItemMemo = Nothing
    Me.tb_beniusat.NTSRepositoryItemText = Nothing
    Me.tb_beniusat.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_beniusat.OptionsFilter.AllowFilter = False
    Me.tb_beniusat.Visible = True
    Me.tb_beniusat.VisibleIndex = 14
    Me.tb_beniusat.Width = 70
    '
    'tb_regautotr
    '
    Me.tb_regautotr.AppearanceCell.Options.UseBackColor = True
    Me.tb_regautotr.AppearanceCell.Options.UseTextOptions = True
    Me.tb_regautotr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_regautotr.Caption = "Autotrasportatore"
    Me.tb_regautotr.Enabled = True
    Me.tb_regautotr.FieldName = "tb_regautotr"
    Me.tb_regautotr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_regautotr.Name = "tb_regautotr"
    Me.tb_regautotr.NTSRepositoryComboBox = Nothing
    Me.tb_regautotr.NTSRepositoryItemCheck = Nothing
    Me.tb_regautotr.NTSRepositoryItemMemo = Nothing
    Me.tb_regautotr.NTSRepositoryItemText = Nothing
    Me.tb_regautotr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_regautotr.OptionsFilter.AllowFilter = False
    Me.tb_regautotr.Visible = True
    Me.tb_regautotr.VisibleIndex = 15
    Me.tb_regautotr.Width = 70
    '
    'tb_volaffpre
    '
    Me.tb_volaffpre.AppearanceCell.Options.UseBackColor = True
    Me.tb_volaffpre.AppearanceCell.Options.UseTextOptions = True
    Me.tb_volaffpre.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_volaffpre.Caption = "Vol. affari presunto (mod. AA7/9)"
    Me.tb_volaffpre.Enabled = True
    Me.tb_volaffpre.FieldName = "tb_volaffpre"
    Me.tb_volaffpre.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_volaffpre.Name = "tb_volaffpre"
    Me.tb_volaffpre.NTSRepositoryComboBox = Nothing
    Me.tb_volaffpre.NTSRepositoryItemCheck = Nothing
    Me.tb_volaffpre.NTSRepositoryItemMemo = Nothing
    Me.tb_volaffpre.NTSRepositoryItemText = Nothing
    Me.tb_volaffpre.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_volaffpre.OptionsFilter.AllowFilter = False
    Me.tb_volaffpre.Visible = True
    Me.tb_volaffpre.VisibleIndex = 16
    Me.tb_volaffpre.Width = 70
    '
    'tb_codusatdef
    '
    Me.tb_codusatdef.AppearanceCell.Options.UseBackColor = True
    Me.tb_codusatdef.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codusatdef.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codusatdef.Caption = "Cod. bene usato predef."
    Me.tb_codusatdef.Enabled = True
    Me.tb_codusatdef.FieldName = "tb_codusatdef"
    Me.tb_codusatdef.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codusatdef.Name = "tb_codusatdef"
    Me.tb_codusatdef.NTSRepositoryComboBox = Nothing
    Me.tb_codusatdef.NTSRepositoryItemCheck = Nothing
    Me.tb_codusatdef.NTSRepositoryItemMemo = Nothing
    Me.tb_codusatdef.NTSRepositoryItemText = Nothing
    Me.tb_codusatdef.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codusatdef.OptionsFilter.AllowFilter = False
    Me.tb_codusatdef.Visible = True
    Me.tb_codusatdef.VisibleIndex = 17
    Me.tb_codusatdef.Width = 70
    '
    'xx_acodista
    '
    Me.xx_acodista.AppearanceCell.Options.UseBackColor = True
    Me.xx_acodista.AppearanceCell.Options.UseTextOptions = True
    Me.xx_acodista.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_acodista.Caption = "Descr. ISTAT"
    Me.xx_acodista.Enabled = False
    Me.xx_acodista.FieldName = "xx_acodista"
    Me.xx_acodista.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_acodista.Name = "xx_acodista"
    Me.xx_acodista.NTSRepositoryComboBox = Nothing
    Me.xx_acodista.NTSRepositoryItemCheck = Nothing
    Me.xx_acodista.NTSRepositoryItemMemo = Nothing
    Me.xx_acodista.NTSRepositoryItemText = Nothing
    Me.xx_acodista.OptionsColumn.AllowEdit = False
    Me.xx_acodista.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_acodista.OptionsColumn.ReadOnly = True
    Me.xx_acodista.OptionsFilter.AllowFilter = False
    Me.xx_acodista.Visible = True
    Me.xx_acodista.VisibleIndex = 9
    Me.xx_acodista.Width = 70
    '
    'xx_codusatdef
    '
    Me.xx_codusatdef.AppearanceCell.Options.UseBackColor = True
    Me.xx_codusatdef.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codusatdef.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codusatdef.Caption = "Descr. bene usato predef."
    Me.xx_codusatdef.Enabled = False
    Me.xx_codusatdef.FieldName = "xx_codusatdef"
    Me.xx_codusatdef.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codusatdef.Name = "xx_codusatdef"
    Me.xx_codusatdef.NTSRepositoryComboBox = Nothing
    Me.xx_codusatdef.NTSRepositoryItemCheck = Nothing
    Me.xx_codusatdef.NTSRepositoryItemMemo = Nothing
    Me.xx_codusatdef.NTSRepositoryItemText = Nothing
    Me.xx_codusatdef.OptionsColumn.AllowEdit = False
    Me.xx_codusatdef.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codusatdef.OptionsColumn.ReadOnly = True
    Me.xx_codusatdef.OptionsFilter.AllowFilter = False
    Me.xx_codusatdef.Visible = True
    Me.xx_codusatdef.VisibleIndex = 18
    Me.xx_codusatdef.Width = 70
    '
    'grvAiva
    '
    Me.grvAiva.ActiveFilterEnabled = False
    '
    'tb_subforn
    '
    Me.tb_subforn.AppearanceCell.Options.UseBackColor = True
    Me.tb_subforn.AppearanceCell.Options.UseTextOptions = True
    Me.tb_subforn.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_subforn.Caption = "Iva di cassa"
    Me.tb_subforn.Enabled = True
    Me.tb_subforn.FieldName = "tb_subforn"
    Me.tb_subforn.Name = "tb_subforn"
    Me.tb_subforn.NTSRepositoryComboBox = Nothing
    Me.tb_subforn.NTSRepositoryItemCheck = Nothing
    Me.tb_subforn.NTSRepositoryItemMemo = Nothing
    Me.tb_subforn.NTSRepositoryItemText = Nothing
    Me.tb_subforn.Visible = True
    Me.tb_subforn.VisibleIndex = 21
    Me.grvAiva.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tb_codatti, Me.tb_desatti, Me.tb_ventilcorr, Me.tb_agric, Me.tb_74ter, Me.tb_aliq74ter, Me.tb_aliqesen, Me.tb_atmentri, Me.tb_acodista, Me.tb_dtinat, Me.tb_dtfiat, Me.tb_maggint, Me.tb_prorata, Me.tb_rilandiv, Me.tb_beniusat, Me.tb_regautotr, Me.tb_volaffpre, Me.tb_codusatdef, Me.xx_acodista, Me.xx_codusatdef, Me.tb_aliq74ter2, Me.tb_datfinaliq742, Me.tb_subforn})
    Me.grvAiva.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvAiva.Enabled = True
    Me.grvAiva.GridControl = Me.grAiva
    Me.grvAiva.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvAiva.MinRowHeight = 14
    Me.grvAiva.Name = "grvAiva"
    Me.grvAiva.NTSAllowDelete = True
    Me.grvAiva.NTSAllowInsert = True
    Me.grvAiva.NTSAllowUpdate = True
    Me.grvAiva.NTSMenuContext = Nothing
    Me.grvAiva.OptionsCustomization.AllowRowSizing = True
    Me.grvAiva.OptionsFilter.AllowFilterEditor = False
    Me.grvAiva.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvAiva.OptionsNavigation.UseTabKey = False
    Me.grvAiva.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvAiva.OptionsView.ColumnAutoWidth = False
    Me.grvAiva.OptionsView.EnableAppearanceEvenRow = True
    Me.grvAiva.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvAiva.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvAiva.OptionsView.ShowGroupPanel = False
    Me.grvAiva.RowHeight = 16
    '
    'tb_aliq74ter2
    '
    Me.tb_aliq74ter2.AppearanceCell.Options.UseBackColor = True
    Me.tb_aliq74ter2.AppearanceCell.Options.UseTextOptions = True
    Me.tb_aliq74ter2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_aliq74ter2.Caption = "Aliq. 74 Ter scagl. 2"
    Me.tb_aliq74ter2.Enabled = True
    Me.tb_aliq74ter2.FieldName = "tb_aliq74ter2"
    Me.tb_aliq74ter2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_aliq74ter2.Name = "tb_aliq74ter2"
    Me.tb_aliq74ter2.NTSRepositoryComboBox = Nothing
    Me.tb_aliq74ter2.NTSRepositoryItemCheck = Nothing
    Me.tb_aliq74ter2.NTSRepositoryItemMemo = Nothing
    Me.tb_aliq74ter2.NTSRepositoryItemText = Nothing
    Me.tb_aliq74ter2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_aliq74ter2.OptionsFilter.AllowFilter = False
    Me.tb_aliq74ter2.Visible = True
    Me.tb_aliq74ter2.VisibleIndex = 19
    '
    'tb_datfinaliq742
    '
    Me.tb_datfinaliq742.AppearanceCell.Options.UseBackColor = True
    Me.tb_datfinaliq742.AppearanceCell.Options.UseTextOptions = True
    Me.tb_datfinaliq742.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_datfinaliq742.Caption = "74 ter Data fine scagl. 2"
    Me.tb_datfinaliq742.Enabled = True
    Me.tb_datfinaliq742.FieldName = "tb_datfinaliq742"
    Me.tb_datfinaliq742.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_datfinaliq742.Name = "tb_datfinaliq742"
    Me.tb_datfinaliq742.NTSRepositoryComboBox = Nothing
    Me.tb_datfinaliq742.NTSRepositoryItemCheck = Nothing
    Me.tb_datfinaliq742.NTSRepositoryItemMemo = Nothing
    Me.tb_datfinaliq742.NTSRepositoryItemText = Nothing
    Me.tb_datfinaliq742.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_datfinaliq742.OptionsFilter.AllowFilter = False
    Me.tb_datfinaliq742.Visible = True
    Me.tb_datfinaliq742.VisibleIndex = 20
    '
    'grAiva
    '
    Me.grAiva.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grAiva.EmbeddedNavigator.Name = ""
    Me.grAiva.Location = New System.Drawing.Point(0, 26)
    Me.grAiva.MainView = Me.grvAiva
    Me.grAiva.Name = "grAiva"
    Me.grAiva.Size = New System.Drawing.Size(648, 416)
    Me.grAiva.TabIndex = 5
    Me.grAiva.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvAiva})
    '
    'FRM__AIVA
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.grAiva)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRM__AIVA"
    Me.NTSLastControlFocussed = Me.grAiva
    Me.Text = "ATTIVITA' IVA"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvAiva, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grAiva, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

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
  Public Overridable Sub InitEntity(ByRef cleAnaz As CLE__ANAZ, ByRef ds As DataSet, ByVal nAnnoAperto As Integer)
    oCleAnaz = cleAnaz
    oCleAnaz.nAnnoTabattiAperto = nAnnoAperto
    AddHandler oCleAnaz.RemoteEvent, AddressOf GestisciEventiEntity

    '-------------------------------------------------
    'leggo dal database i dati e collego il NTSBinding
    dsAiva = ds
    oCleAnaz.AivaSetDataTable(DittaCorrente, dsAiva.Tables("TABATTI"))
    dcAiva.DataSource = dsAiva.Tables("TABATTI")
    dsAiva.AcceptChanges()
    grAiva.DataSource = dcAiva

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
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      '---------------------
      Dim dttAgric As New DataTable()
      dttAgric.Columns.Add("cod", GetType(String))
      dttAgric.Columns.Add("val", GetType(String))
      dttAgric.Rows.Add(New Object() {"N", "(Nessuno)"})
      dttAgric.Rows.Add(New Object() {"S", "Agricoltura"})
      dttAgric.Rows.Add(New Object() {"G", "Agriturismo"})
      dttAgric.AcceptChanges()

      Dim dttAtmentri As New DataTable()
      dttAtmentri.Columns.Add("cod", GetType(String))
      dttAtmentri.Columns.Add("val", GetType(String))
      dttAtmentri.Rows.Add(New Object() {"M", "Mensile"})
      dttAtmentri.Rows.Add(New Object() {"T", "Trimestrale"})
      dttAtmentri.Rows.Add(New Object() {"A", "Annuale"})
      dttAtmentri.AcceptChanges()

      Dim dttProrata As New DataTable()
      dttProrata.Columns.Add("cod", GetType(String))
      dttProrata.Columns.Add("val", GetType(String))
      dttProrata.Rows.Add(New Object() {"N", "No"})
      dttProrata.Rows.Add(New Object() {"S", "Solo liquidazione Iva"})
      dttProrata.Rows.Add(New Object() {"P", "Liquidaz. IVA e Prima nota"})
      dttProrata.AcceptChanges()

      Dim dttBeniusat As New DataTable()
      dttBeniusat.Columns.Add("cod", GetType(String))
      dttBeniusat.Columns.Add("val", GetType(String))
      dttBeniusat.Rows.Add(New Object() {"N", "No"})
      dttBeniusat.Rows.Add(New Object() {"A", "Analitico"})
      dttBeniusat.Rows.Add(New Object() {"G", "Globale"})
      dttBeniusat.Rows.Add(New Object() {"F", "Forfettario"})
      dttBeniusat.Rows.Add(New Object() {"S", "Asta"})
      dttBeniusat.Rows.Add(New Object() {"T", "Totale"})
      dttBeniusat.AcceptChanges()

      grvAiva.NTSSetParam(oMenu, "ATTIVITA' IVA")
      tb_codatti.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128648439181250000, "Codice"), tabatti)
      tb_desatti.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128648439181406250, "Descrizione"), 60, True)
      tb_ventilcorr.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128648439181562500, "Ventilaz. corrispettivi"), "S", "N")
      tb_agric.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128648439181718750, "Regime agricolo"), dttAgric, "val", "cod")
      tb_74ter.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128648439181875000, "Reg. art. 74 ter"), "S", "N")
      tb_aliq74ter.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648439182031250, "Aliquota 74 ter"), oApp.FormatSconti, 6, 0, 100)
      tb_aliqesen.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648439182187500, "Perc. ind. op. esenti"), oApp.FormatSconti, 6, 0, 100)
      tb_atmentri.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128648439182343750, "Periodicità"), dttAtmentri, "val", "cod")
      tb_acodista.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128648439182500000, "Cod. ISTAT attività"), tabatec, True)
      tb_dtinat.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128648439182656250, "Data inizio attività"), True, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      tb_dtfiat.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128648439182812500, "Data fine attività"), True, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      tb_maggint.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128648439182968750, "Maggioraz. per interessi"), "S", "N")
      tb_prorata.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128648439183125000, "Soggetto a pro-rata"), dttProrata, "val", "cod")
      tb_rilandiv.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128648439183281250, "Rilancio dichiaraz. IVA"), 4, True)
      tb_beniusat.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128648439183437500, "Beni usati"), dttBeniusat, "val", "cod")
      tb_regautotr.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128648439183593750, "Autotrasportatore"), "S", "N")
      tb_volaffpre.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128648439183750000, "Vol. affari presunto (mod. AA7/9)"), oApp.FormatImporti, 20)
      tb_codusatdef.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128648439183906250, "Cod. bene usato predef."), tabusat)
      xx_acodista.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128654251023593750, "Descr. ISTAT"), 0, True)
      xx_codusatdef.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128654251023750000, "Descr. bene usato predef."), 0, True)
      tb_aliq74ter2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129772491837485257, "Aliquota 74 ter scaglione 2"), oApp.FormatSconti, 6, 0, 100)
      tb_datfinaliq742.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129772491996097538, "74 Ter data fine scaglione 2"), True, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      tb_subforn.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129956426900572819, "Iva di cassa"), "S", "N")

      tb_codatti.NTSForzaVisZoom = False
      tb_codatti.NTSSetRichiesto()
      tb_desatti.NTSSetRichiesto()
      tb_atmentri.NTSSetRichiesto()


      If CLN__STD.FRIENDLY = True Then
        If dsAiva.Tables("TABATTI").Rows.Count > 0 Then grvAiva.NTSAllowInsert = False
        grvAiva.NTSAllowDelete = False
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
  Public Overridable Sub FRM__AIVA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      Me.Text = oApp.Tr(Me, 129006949327203651, "ATTIVITA' IVA ANNO ") & oCleAnaz.nAnnoTabattiAperto.ToString

      'devo bloccare/sbloccare i campi chiave se necessario
      grvAiva_NTSFocusedRowChanged(grvAiva, Nothing)
      '--------------------------------------------------------------------------------------------------------------
      SetColonneFRIENDLY()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__AIVA_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvAiva.NTSNuovo()

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
      If grvAiva.NTSGetCurrentDataRow() Is Nothing Then Return
      If Not oCleAnaz.AivaBeforeCancella(NTSCInt(grvAiva.NTSGetCurrentDataRow()!tb_codatti)) Then Return
      If Not grvAiva.NTSDeleteRigaCorrente(dcAiva, True, dtrDeleted) Then Return
      oCleAnaz.AivaSalva(True, NTSCInt(dtrDeleted!tb_codatti))

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvAiva.NTSRipristinaRigaCorrenteBefore(dcAiva, True) Then Return
      oCleAnaz.AivaRipristina(dcAiva.Position, dcAiva.Filter)
      grvAiva.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      'zoom standard
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      '------------------------------------
      'zoom standard di textbox e griglia
      NTSCallStandardZoom()
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
    Me.Close()
  End Sub
#End Region

  Public Overridable Sub grvAiva_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvAiva.NTSBeforeRowUpdate
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

  Public Overridable Sub grvAiva_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvAiva.NTSFocusedRowChanged
    Try
      If NTSCInt(grvAiva.GetFocusedRowCellValue(tb_codatti)) <> 0 Then
        tb_codatti.Enabled = False
      Else
        GctlSetVisEnab(tb_codatti, False)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvAiva.NTSSalvaRigaCorrente(dcAiva, oCleAnaz.AivaRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleAnaz.AivaSalva(False, 0) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleAnaz.AivaRipristina(dcAiva.Position, dcAiva.Filter)
        Case System.Windows.Forms.DialogResult.Cancel
          'non posso fare nulla
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          'la riga non ha subito modifiche
      End Select

      If (CLN__STD.FRIENDLY = True) And (dsAiva.Tables("TABATTI").Rows.Count > 0) Then grvAiva.NTSAllowInsert = False

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub SetColonneFRIENDLY()
    Try
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      tb_aliq74ter.Enabled = False
      tb_74ter.Enabled = False
      tb_prorata.Enabled = False
      tb_rilandiv.Enabled = False
      tb_beniusat.Enabled = False
      tb_regautotr.Enabled = False
      tb_volaffpre.Enabled = False
      tb_codusatdef.Enabled = False
      xx_codusatdef.Enabled = False
      tb_aliq74ter2.Enabled = False
      tb_datfinaliq742.Enabled = False
      tb_aliqesen.Enabled = False
      '--------------------------------------------------------------------------------------------------------------
      tb_aliq74ter.Visible = False
      tb_74ter.Visible = False
      tb_prorata.Visible = False
      tb_rilandiv.Visible = False
      tb_beniusat.Visible = False
      tb_regautotr.Visible = False
      tb_volaffpre.Visible = False
      tb_codusatdef.Visible = False
      xx_codusatdef.Visible = False
      tb_aliq74ter2.Visible = False
      tb_datfinaliq742.Visible = False
      tb_aliqesen.Visible = False
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class
