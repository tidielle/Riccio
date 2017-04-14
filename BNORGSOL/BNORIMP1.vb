Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORIMP1
  Public oCleGsol As CLEORGSOL
  Public oCallParams As CLE__CLDP
  Public dcImpe As BindingSource = New BindingSource()
  Public lRigaCopiata As Integer = 0                      'utilizzata dalle voci di menu copia/incolla riga per memorizzare la riga da copiare
  Public dttUm As DataTable = Nothing                    'elenco delle unità di misura utilizzate in artico

  Private components As System.ComponentModel.IContainer


  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMORIMP1))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbNavigazMrp = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grRighe = New NTSInformatica.NTSGrid
    Me.grvRighe = New NTSInformatica.NTSGridView
    Me.xxo_tctagliaf = New NTSInformatica.NTSGridColumn
    Me.ec_riga = New NTSInformatica.NTSGridColumn
    Me.ec_codart = New NTSInformatica.NTSGridColumn
    Me.ec_descr = New NTSInformatica.NTSGridColumn
    Me.ec_desint = New NTSInformatica.NTSGridColumn
    Me.ec_unmis = New NTSInformatica.NTSGridColumn
    Me.ec_colli = New NTSInformatica.NTSGridColumn
    Me.ec_ump = New NTSInformatica.NTSGridColumn
    Me.ec_quant = New NTSInformatica.NTSGridColumn
    Me.ec_datcons = New NTSInformatica.NTSGridColumn
    Me.ec_prezzo = New NTSInformatica.NTSGridColumn
    Me.ec_stato = New NTSInformatica.NTSGridColumn
    Me.ec_magaz = New NTSInformatica.NTSGridColumn
    Me.xxo_magaz = New NTSInformatica.NTSGridColumn
    Me.ec_datord = New NTSInformatica.NTSGridColumn
    Me.ec_controp = New NTSInformatica.NTSGridColumn
    Me.xxo_controp = New NTSInformatica.NTSGridColumn
    Me.ec_codiva = New NTSInformatica.NTSGridColumn
    Me.xxo_codiva = New NTSInformatica.NTSGridColumn
    Me.ec_codcfam = New NTSInformatica.NTSGridColumn
    Me.xxo_codcfam = New NTSInformatica.NTSGridColumn
    Me.ec_commeca = New NTSInformatica.NTSGridColumn
    Me.xxo_commeca = New NTSInformatica.NTSGridColumn
    Me.ec_subcommeca = New NTSInformatica.NTSGridColumn
    Me.ec_codcena = New NTSInformatica.NTSGridColumn
    Me.xxo_codcena = New NTSInformatica.NTSGridColumn
    Me.xxo_lottox = New NTSInformatica.NTSGridColumn
    Me.ec_note = New NTSInformatica.NTSGridColumn
    Me.ec_valore = New NTSInformatica.NTSGridColumn
    Me.ec_contocontr = New NTSInformatica.NTSGridColumn
    Me.xxo_contocon = New NTSInformatica.NTSGridColumn
    Me.ec_codclie = New NTSInformatica.NTSGridColumn
    Me.xxo_codclie = New NTSInformatica.NTSGridColumn
    Me.ec_misura1 = New NTSInformatica.NTSGridColumn
    Me.ec_misura2 = New NTSInformatica.NTSGridColumn
    Me.ec_misura3 = New NTSInformatica.NTSGridColumn
    Me.ec_perqta = New NTSInformatica.NTSGridColumn
    Me.ec_flcom = New NTSInformatica.NTSGridColumn
    Me.ec_flprznet = New NTSInformatica.NTSGridColumn
    Me.ec_flforf = New NTSInformatica.NTSGridColumn
    Me.ec_matric = New NTSInformatica.NTSGridColumn
    Me.ec_umprz = New NTSInformatica.NTSGridColumn
    Me.ec_fase = New NTSInformatica.NTSGridColumn
    Me.xxo_fase = New NTSInformatica.NTSGridColumn
    Me.ec_codlavo = New NTSInformatica.NTSGridColumn
    Me.xxo_codlavo = New NTSInformatica.NTSGridColumn
    Me.ec_datini = New NTSInformatica.NTSGridColumn
    Me.ec_datfin = New NTSInformatica.NTSGridColumn
    Me.ec_valorev = New NTSInformatica.NTSGridColumn
    Me.ec_pmtaskid = New NTSInformatica.NTSGridColumn
    Me.ec_pmsalcon = New NTSInformatica.NTSGridColumn
    Me.ec_pmqtadis = New NTSInformatica.NTSGridColumn
    Me.ec_pmvaldis = New NTSInformatica.NTSGridColumn
    Me.ec_rdatipork = New NTSInformatica.NTSGridColumn
    Me.ec_rdaanno = New NTSInformatica.NTSGridColumn
    Me.ec_rdaserie = New NTSInformatica.NTSGridColumn
    Me.ec_rdanum = New NTSInformatica.NTSGridColumn
    Me.ec_rdariga = New NTSInformatica.NTSGridColumn
    Me.ec_offreq = New NTSInformatica.NTSGridColumn
    Me.ec_ortipork = New NTSInformatica.NTSGridColumn
    Me.ec_oranno = New NTSInformatica.NTSGridColumn
    Me.ec_orserie = New NTSInformatica.NTSGridColumn
    Me.ec_ornum = New NTSInformatica.NTSGridColumn
    Me.ec_orriga = New NTSInformatica.NTSGridColumn
    Me.xxo_codtagl = New NTSInformatica.NTSGridColumn
    Me.ec_tctaglia = New NTSInformatica.NTSGridColumn
    Me.ec_prezvalc = New NTSInformatica.NTSGridColumn
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbNavigazMrp, Me.tlbEsci, Me.tlbZoom})
    Me.NtsBarManager1.MaxItemId = 22
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNavigazMrp), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci, True)})
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
    'tlbNavigazMrp
    '
    Me.tlbNavigazMrp.Caption = "Navigazione MRP"
    Me.tlbNavigazMrp.Glyph = CType(resources.GetObject("tlbNavigazMrp.Glyph"), System.Drawing.Image)
    Me.tlbNavigazMrp.Id = 5
    Me.tlbNavigazMrp.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N))
    Me.tlbNavigazMrp.Name = "tlbNavigazMrp"
    Me.tlbNavigazMrp.Visible = True
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
    Me.grRighe.Location = New System.Drawing.Point(0, 30)
    Me.grRighe.MainView = Me.grvRighe
    Me.grRighe.Name = "grRighe"
    Me.grRighe.Size = New System.Drawing.Size(740, 412)
    Me.grRighe.TabIndex = 4
    Me.grRighe.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvRighe})
    '
    'grvRighe
    '
    Me.grvRighe.ActiveFilterEnabled = False
    '
    'xxo_tctagliaf
    '
    Me.xxo_tctagliaf.AppearanceCell.Options.UseBackColor = True
    Me.xxo_tctagliaf.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_tctagliaf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_tctagliaf.Caption = "Taglia figlio"
    Me.xxo_tctagliaf.Enabled = True
    Me.xxo_tctagliaf.FieldName = "xxo_tctagliaf"
    Me.xxo_tctagliaf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_tctagliaf.Name = "xxo_tctagliaf"
    Me.xxo_tctagliaf.NTSRepositoryComboBox = Nothing
    Me.xxo_tctagliaf.NTSRepositoryItemCheck = Nothing
    Me.xxo_tctagliaf.NTSRepositoryItemMemo = Nothing
    Me.xxo_tctagliaf.NTSRepositoryItemText = Nothing
    Me.xxo_tctagliaf.Visible = True
    Me.xxo_tctagliaf.VisibleIndex = 29
    Me.grvRighe.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ec_riga, Me.ec_codart, Me.ec_descr, Me.ec_desint, Me.ec_unmis, Me.ec_colli, Me.ec_ump, Me.ec_quant, Me.ec_datcons, Me.ec_prezzo, Me.ec_stato, Me.ec_magaz, Me.xxo_magaz, Me.ec_datord, Me.ec_controp, Me.xxo_controp, Me.ec_codiva, Me.xxo_codiva, Me.ec_codcfam, Me.xxo_codcfam, Me.ec_commeca, Me.xxo_commeca, Me.ec_subcommeca, Me.ec_codcena, Me.xxo_codcena, Me.xxo_lottox, Me.ec_note, Me.ec_valore, Me.ec_contocontr, Me.xxo_contocon, Me.ec_codclie, Me.xxo_codclie, Me.ec_misura1, Me.ec_misura2, Me.ec_misura3, Me.ec_perqta, Me.ec_flcom, Me.ec_flprznet, Me.ec_flforf, Me.ec_matric, Me.ec_umprz, Me.ec_fase, Me.xxo_fase, Me.ec_codlavo, Me.xxo_codlavo, Me.ec_datini, Me.ec_datfin, Me.ec_valorev, Me.ec_pmtaskid, Me.ec_pmtaskid, Me.ec_pmsalcon, Me.ec_pmqtadis, Me.ec_pmvaldis, Me.ec_rdatipork, Me.ec_rdaanno, Me.ec_rdaserie, Me.ec_rdanum, Me.ec_rdariga, Me.ec_offreq, Me.ec_ortipork, Me.ec_oranno, Me.ec_orserie, Me.ec_ornum, Me.ec_orriga, Me.xxo_codtagl, Me.ec_tctaglia, Me.ec_prezvalc, Me.xxo_tctagliaf})
    Me.grvRighe.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvRighe.Enabled = True
    Me.grvRighe.GridControl = Me.grRighe
    Me.grvRighe.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvRighe.MinRowHeight = 14
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
    'ec_riga
    '
    Me.ec_riga.AppearanceCell.Options.UseBackColor = True
    Me.ec_riga.AppearanceCell.Options.UseTextOptions = True
    Me.ec_riga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_riga.Caption = "Progr."
    Me.ec_riga.Enabled = False
    Me.ec_riga.FieldName = "ec_riga"
    Me.ec_riga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_riga.Name = "ec_riga"
    Me.ec_riga.NTSRepositoryComboBox = Nothing
    Me.ec_riga.NTSRepositoryItemCheck = Nothing
    Me.ec_riga.NTSRepositoryItemMemo = Nothing
    Me.ec_riga.NTSRepositoryItemText = Nothing
    Me.ec_riga.OptionsColumn.AllowEdit = False
    Me.ec_riga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_riga.OptionsColumn.ReadOnly = True
    Me.ec_riga.OptionsFilter.AllowFilter = False
    Me.ec_riga.Visible = True
    Me.ec_riga.VisibleIndex = 0
    Me.ec_riga.Width = 73
    '
    'ec_codart
    '
    Me.ec_codart.AppearanceCell.Options.UseBackColor = True
    Me.ec_codart.AppearanceCell.Options.UseTextOptions = True
    Me.ec_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_codart.Caption = "Cod. Art."
    Me.ec_codart.Enabled = True
    Me.ec_codart.FieldName = "ec_codart"
    Me.ec_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_codart.Name = "ec_codart"
    Me.ec_codart.NTSRepositoryComboBox = Nothing
    Me.ec_codart.NTSRepositoryItemCheck = Nothing
    Me.ec_codart.NTSRepositoryItemMemo = Nothing
    Me.ec_codart.NTSRepositoryItemText = Nothing
    Me.ec_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_codart.OptionsFilter.AllowFilter = False
    Me.ec_codart.Visible = True
    Me.ec_codart.VisibleIndex = 1
    Me.ec_codart.Width = 64
    '
    'ec_descr
    '
    Me.ec_descr.AppearanceCell.Options.UseBackColor = True
    Me.ec_descr.AppearanceCell.Options.UseTextOptions = True
    Me.ec_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_descr.Caption = "Descrizione"
    Me.ec_descr.Enabled = True
    Me.ec_descr.FieldName = "ec_descr"
    Me.ec_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_descr.Name = "ec_descr"
    Me.ec_descr.NTSRepositoryComboBox = Nothing
    Me.ec_descr.NTSRepositoryItemCheck = Nothing
    Me.ec_descr.NTSRepositoryItemMemo = Nothing
    Me.ec_descr.NTSRepositoryItemText = Nothing
    Me.ec_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_descr.OptionsFilter.AllowFilter = False
    Me.ec_descr.Visible = True
    Me.ec_descr.VisibleIndex = 2
    Me.ec_descr.Width = 139
    '
    'ec_desint
    '
    Me.ec_desint.AppearanceCell.Options.UseBackColor = True
    Me.ec_desint.AppearanceCell.Options.UseTextOptions = True
    Me.ec_desint.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_desint.Caption = "Descr.interna"
    Me.ec_desint.Enabled = True
    Me.ec_desint.FieldName = "ec_desint"
    Me.ec_desint.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_desint.Name = "ec_desint"
    Me.ec_desint.NTSRepositoryComboBox = Nothing
    Me.ec_desint.NTSRepositoryItemCheck = Nothing
    Me.ec_desint.NTSRepositoryItemMemo = Nothing
    Me.ec_desint.NTSRepositoryItemText = Nothing
    Me.ec_desint.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_desint.OptionsFilter.AllowFilter = False
    Me.ec_desint.Width = 126
    '
    'ec_unmis
    '
    Me.ec_unmis.AppearanceCell.Options.UseBackColor = True
    Me.ec_unmis.AppearanceCell.Options.UseTextOptions = True
    Me.ec_unmis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_unmis.Caption = "U.M."
    Me.ec_unmis.Enabled = True
    Me.ec_unmis.FieldName = "ec_unmis"
    Me.ec_unmis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_unmis.Name = "ec_unmis"
    Me.ec_unmis.NTSRepositoryComboBox = Nothing
    Me.ec_unmis.NTSRepositoryItemCheck = Nothing
    Me.ec_unmis.NTSRepositoryItemMemo = Nothing
    Me.ec_unmis.NTSRepositoryItemText = Nothing
    Me.ec_unmis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_unmis.OptionsFilter.AllowFilter = False
    Me.ec_unmis.Visible = True
    Me.ec_unmis.VisibleIndex = 3
    Me.ec_unmis.Width = 39
    '
    'ec_colli
    '
    Me.ec_colli.AppearanceCell.Options.UseBackColor = True
    Me.ec_colli.AppearanceCell.Options.UseTextOptions = True
    Me.ec_colli.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_colli.Caption = "Colli ord."
    Me.ec_colli.Enabled = True
    Me.ec_colli.FieldName = "ec_colli"
    Me.ec_colli.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_colli.Name = "ec_colli"
    Me.ec_colli.NTSRepositoryComboBox = Nothing
    Me.ec_colli.NTSRepositoryItemCheck = Nothing
    Me.ec_colli.NTSRepositoryItemMemo = Nothing
    Me.ec_colli.NTSRepositoryItemText = Nothing
    Me.ec_colli.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_colli.OptionsFilter.AllowFilter = False
    Me.ec_colli.Visible = True
    Me.ec_colli.VisibleIndex = 4
    Me.ec_colli.Width = 54
    '
    'ec_ump
    '
    Me.ec_ump.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.ec_ump.AppearanceCell.Options.UseBackColor = True
    Me.ec_ump.AppearanceCell.Options.UseTextOptions = True
    Me.ec_ump.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_ump.Caption = "UMP"
    Me.ec_ump.Enabled = False
    Me.ec_ump.FieldName = "ec_ump"
    Me.ec_ump.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_ump.Name = "ec_ump"
    Me.ec_ump.NTSRepositoryComboBox = Nothing
    Me.ec_ump.NTSRepositoryItemCheck = Nothing
    Me.ec_ump.NTSRepositoryItemMemo = Nothing
    Me.ec_ump.NTSRepositoryItemText = Nothing
    Me.ec_ump.OptionsColumn.AllowEdit = False
    Me.ec_ump.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_ump.OptionsColumn.ReadOnly = True
    Me.ec_ump.OptionsFilter.AllowFilter = False
    Me.ec_ump.Visible = True
    Me.ec_ump.VisibleIndex = 5
    '
    'ec_quant
    '
    Me.ec_quant.AppearanceCell.Options.UseBackColor = True
    Me.ec_quant.AppearanceCell.Options.UseTextOptions = True
    Me.ec_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_quant.Caption = "Q.tà ordin."
    Me.ec_quant.Enabled = True
    Me.ec_quant.FieldName = "ec_quant"
    Me.ec_quant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_quant.Name = "ec_quant"
    Me.ec_quant.NTSRepositoryComboBox = Nothing
    Me.ec_quant.NTSRepositoryItemCheck = Nothing
    Me.ec_quant.NTSRepositoryItemMemo = Nothing
    Me.ec_quant.NTSRepositoryItemText = Nothing
    Me.ec_quant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_quant.OptionsFilter.AllowFilter = False
    Me.ec_quant.Visible = True
    Me.ec_quant.VisibleIndex = 6
    '
    'ec_datcons
    '
    Me.ec_datcons.AppearanceCell.Options.UseBackColor = True
    Me.ec_datcons.AppearanceCell.Options.UseTextOptions = True
    Me.ec_datcons.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_datcons.Caption = "Data cons."
    Me.ec_datcons.Enabled = True
    Me.ec_datcons.FieldName = "ec_datcons"
    Me.ec_datcons.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_datcons.Name = "ec_datcons"
    Me.ec_datcons.NTSRepositoryComboBox = Nothing
    Me.ec_datcons.NTSRepositoryItemCheck = Nothing
    Me.ec_datcons.NTSRepositoryItemMemo = Nothing
    Me.ec_datcons.NTSRepositoryItemText = Nothing
    Me.ec_datcons.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_datcons.OptionsFilter.AllowFilter = False
    Me.ec_datcons.Visible = True
    Me.ec_datcons.VisibleIndex = 7
    '
    'ec_prezzo
    '
    Me.ec_prezzo.AppearanceCell.Options.UseBackColor = True
    Me.ec_prezzo.AppearanceCell.Options.UseTextOptions = True
    Me.ec_prezzo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_prezzo.Caption = "Prezzo"
    Me.ec_prezzo.Enabled = True
    Me.ec_prezzo.FieldName = "ec_prezzo"
    Me.ec_prezzo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_prezzo.Name = "ec_prezzo"
    Me.ec_prezzo.NTSRepositoryComboBox = Nothing
    Me.ec_prezzo.NTSRepositoryItemCheck = Nothing
    Me.ec_prezzo.NTSRepositoryItemMemo = Nothing
    Me.ec_prezzo.NTSRepositoryItemText = Nothing
    Me.ec_prezzo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_prezzo.OptionsFilter.AllowFilter = False
    Me.ec_prezzo.Visible = True
    Me.ec_prezzo.VisibleIndex = 8
    '
    'ec_stato
    '
    Me.ec_stato.AppearanceCell.Options.UseBackColor = True
    Me.ec_stato.AppearanceCell.Options.UseTextOptions = True
    Me.ec_stato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_stato.Caption = "Stato"
    Me.ec_stato.Enabled = True
    Me.ec_stato.FieldName = "ec_stato"
    Me.ec_stato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_stato.Name = "ec_stato"
    Me.ec_stato.NTSRepositoryComboBox = Nothing
    Me.ec_stato.NTSRepositoryItemCheck = Nothing
    Me.ec_stato.NTSRepositoryItemMemo = Nothing
    Me.ec_stato.NTSRepositoryItemText = Nothing
    Me.ec_stato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_stato.OptionsFilter.AllowFilter = False
    Me.ec_stato.Visible = True
    Me.ec_stato.VisibleIndex = 9
    '
    'ec_magaz
    '
    Me.ec_magaz.AppearanceCell.Options.UseBackColor = True
    Me.ec_magaz.AppearanceCell.Options.UseTextOptions = True
    Me.ec_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_magaz.Caption = "Magazzino"
    Me.ec_magaz.Enabled = True
    Me.ec_magaz.FieldName = "ec_magaz"
    Me.ec_magaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_magaz.Name = "ec_magaz"
    Me.ec_magaz.NTSRepositoryComboBox = Nothing
    Me.ec_magaz.NTSRepositoryItemCheck = Nothing
    Me.ec_magaz.NTSRepositoryItemMemo = Nothing
    Me.ec_magaz.NTSRepositoryItemText = Nothing
    Me.ec_magaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_magaz.OptionsFilter.AllowFilter = False
    Me.ec_magaz.Visible = True
    Me.ec_magaz.VisibleIndex = 10
    '
    'xxo_magaz
    '
    Me.xxo_magaz.AppearanceCell.Options.UseBackColor = True
    Me.xxo_magaz.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_magaz.Caption = "Descr. magazzino"
    Me.xxo_magaz.Enabled = False
    Me.xxo_magaz.FieldName = "xxo_magaz"
    Me.xxo_magaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_magaz.Name = "xxo_magaz"
    Me.xxo_magaz.NTSRepositoryComboBox = Nothing
    Me.xxo_magaz.NTSRepositoryItemCheck = Nothing
    Me.xxo_magaz.NTSRepositoryItemMemo = Nothing
    Me.xxo_magaz.NTSRepositoryItemText = Nothing
    Me.xxo_magaz.OptionsColumn.AllowEdit = False
    Me.xxo_magaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_magaz.OptionsColumn.ReadOnly = True
    Me.xxo_magaz.OptionsFilter.AllowFilter = False
    Me.xxo_magaz.Visible = True
    Me.xxo_magaz.VisibleIndex = 11
    '
    'ec_datord
    '
    Me.ec_datord.AppearanceCell.Options.UseBackColor = True
    Me.ec_datord.AppearanceCell.Options.UseTextOptions = True
    Me.ec_datord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_datord.Caption = "Data mass. ord"
    Me.ec_datord.Enabled = True
    Me.ec_datord.FieldName = "ec_datord"
    Me.ec_datord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_datord.Name = "ec_datord"
    Me.ec_datord.NTSRepositoryComboBox = Nothing
    Me.ec_datord.NTSRepositoryItemCheck = Nothing
    Me.ec_datord.NTSRepositoryItemMemo = Nothing
    Me.ec_datord.NTSRepositoryItemText = Nothing
    Me.ec_datord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_datord.OptionsFilter.AllowFilter = False
    Me.ec_datord.Visible = True
    Me.ec_datord.VisibleIndex = 12
    '
    'ec_controp
    '
    Me.ec_controp.AppearanceCell.Options.UseBackColor = True
    Me.ec_controp.AppearanceCell.Options.UseTextOptions = True
    Me.ec_controp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_controp.Caption = "Controp."
    Me.ec_controp.Enabled = True
    Me.ec_controp.FieldName = "ec_controp"
    Me.ec_controp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_controp.Name = "ec_controp"
    Me.ec_controp.NTSRepositoryComboBox = Nothing
    Me.ec_controp.NTSRepositoryItemCheck = Nothing
    Me.ec_controp.NTSRepositoryItemMemo = Nothing
    Me.ec_controp.NTSRepositoryItemText = Nothing
    Me.ec_controp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_controp.OptionsFilter.AllowFilter = False
    Me.ec_controp.Visible = True
    Me.ec_controp.VisibleIndex = 13
    '
    'xxo_controp
    '
    Me.xxo_controp.AppearanceCell.Options.UseBackColor = True
    Me.xxo_controp.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_controp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_controp.Caption = "Descr. controp."
    Me.xxo_controp.Enabled = False
    Me.xxo_controp.FieldName = "xxo_controp"
    Me.xxo_controp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_controp.Name = "xxo_controp"
    Me.xxo_controp.NTSRepositoryComboBox = Nothing
    Me.xxo_controp.NTSRepositoryItemCheck = Nothing
    Me.xxo_controp.NTSRepositoryItemMemo = Nothing
    Me.xxo_controp.NTSRepositoryItemText = Nothing
    Me.xxo_controp.OptionsColumn.AllowEdit = False
    Me.xxo_controp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_controp.OptionsColumn.ReadOnly = True
    Me.xxo_controp.OptionsFilter.AllowFilter = False
    Me.xxo_controp.Visible = True
    Me.xxo_controp.VisibleIndex = 14
    '
    'ec_codiva
    '
    Me.ec_codiva.AppearanceCell.Options.UseBackColor = True
    Me.ec_codiva.AppearanceCell.Options.UseTextOptions = True
    Me.ec_codiva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_codiva.Caption = "Cod. IVA"
    Me.ec_codiva.Enabled = True
    Me.ec_codiva.FieldName = "ec_codiva"
    Me.ec_codiva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_codiva.Name = "ec_codiva"
    Me.ec_codiva.NTSRepositoryComboBox = Nothing
    Me.ec_codiva.NTSRepositoryItemCheck = Nothing
    Me.ec_codiva.NTSRepositoryItemMemo = Nothing
    Me.ec_codiva.NTSRepositoryItemText = Nothing
    Me.ec_codiva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_codiva.OptionsFilter.AllowFilter = False
    Me.ec_codiva.Visible = True
    Me.ec_codiva.VisibleIndex = 15
    '
    'xxo_codiva
    '
    Me.xxo_codiva.AppearanceCell.Options.UseBackColor = True
    Me.xxo_codiva.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_codiva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_codiva.Caption = "Descr. IVA"
    Me.xxo_codiva.Enabled = False
    Me.xxo_codiva.FieldName = "xxo_codiva"
    Me.xxo_codiva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_codiva.Name = "xxo_codiva"
    Me.xxo_codiva.NTSRepositoryComboBox = Nothing
    Me.xxo_codiva.NTSRepositoryItemCheck = Nothing
    Me.xxo_codiva.NTSRepositoryItemMemo = Nothing
    Me.xxo_codiva.NTSRepositoryItemText = Nothing
    Me.xxo_codiva.OptionsColumn.AllowEdit = False
    Me.xxo_codiva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_codiva.OptionsColumn.ReadOnly = True
    Me.xxo_codiva.OptionsFilter.AllowFilter = False
    Me.xxo_codiva.Visible = True
    Me.xxo_codiva.VisibleIndex = 16
    '
    'ec_codcfam
    '
    Me.ec_codcfam.AppearanceCell.Options.UseBackColor = True
    Me.ec_codcfam.AppearanceCell.Options.UseTextOptions = True
    Me.ec_codcfam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_codcfam.Caption = "Linea/Fam."
    Me.ec_codcfam.Enabled = True
    Me.ec_codcfam.FieldName = "ec_codcfam"
    Me.ec_codcfam.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_codcfam.Name = "ec_codcfam"
    Me.ec_codcfam.NTSRepositoryComboBox = Nothing
    Me.ec_codcfam.NTSRepositoryItemCheck = Nothing
    Me.ec_codcfam.NTSRepositoryItemMemo = Nothing
    Me.ec_codcfam.NTSRepositoryItemText = Nothing
    Me.ec_codcfam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_codcfam.OptionsFilter.AllowFilter = False
    Me.ec_codcfam.Visible = True
    Me.ec_codcfam.VisibleIndex = 17
    '
    'xxo_codcfam
    '
    Me.xxo_codcfam.AppearanceCell.Options.UseBackColor = True
    Me.xxo_codcfam.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_codcfam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_codcfam.Caption = "Descr. linea/fam"
    Me.xxo_codcfam.Enabled = False
    Me.xxo_codcfam.FieldName = "xxo_codcfam"
    Me.xxo_codcfam.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_codcfam.Name = "xxo_codcfam"
    Me.xxo_codcfam.NTSRepositoryComboBox = Nothing
    Me.xxo_codcfam.NTSRepositoryItemCheck = Nothing
    Me.xxo_codcfam.NTSRepositoryItemMemo = Nothing
    Me.xxo_codcfam.NTSRepositoryItemText = Nothing
    Me.xxo_codcfam.OptionsColumn.AllowEdit = False
    Me.xxo_codcfam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_codcfam.OptionsColumn.ReadOnly = True
    Me.xxo_codcfam.OptionsFilter.AllowFilter = False
    Me.xxo_codcfam.Visible = True
    Me.xxo_codcfam.VisibleIndex = 18
    '
    'ec_commeca
    '
    Me.ec_commeca.AppearanceCell.Options.UseBackColor = True
    Me.ec_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.ec_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_commeca.Caption = "Comm. C.A."
    Me.ec_commeca.Enabled = True
    Me.ec_commeca.FieldName = "ec_commeca"
    Me.ec_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_commeca.Name = "ec_commeca"
    Me.ec_commeca.NTSRepositoryComboBox = Nothing
    Me.ec_commeca.NTSRepositoryItemCheck = Nothing
    Me.ec_commeca.NTSRepositoryItemMemo = Nothing
    Me.ec_commeca.NTSRepositoryItemText = Nothing
    Me.ec_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_commeca.OptionsFilter.AllowFilter = False
    Me.ec_commeca.Visible = True
    Me.ec_commeca.VisibleIndex = 19
    '
    'xxo_commeca
    '
    Me.xxo_commeca.AppearanceCell.Options.UseBackColor = True
    Me.xxo_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_commeca.Caption = "Descr. commessa"
    Me.xxo_commeca.Enabled = False
    Me.xxo_commeca.FieldName = "xxo_commeca"
    Me.xxo_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_commeca.Name = "xxo_commeca"
    Me.xxo_commeca.NTSRepositoryComboBox = Nothing
    Me.xxo_commeca.NTSRepositoryItemCheck = Nothing
    Me.xxo_commeca.NTSRepositoryItemMemo = Nothing
    Me.xxo_commeca.NTSRepositoryItemText = Nothing
    Me.xxo_commeca.OptionsColumn.AllowEdit = False
    Me.xxo_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_commeca.OptionsColumn.ReadOnly = True
    Me.xxo_commeca.OptionsFilter.AllowFilter = False
    Me.xxo_commeca.Visible = True
    Me.xxo_commeca.VisibleIndex = 20
    '
    'ec_subcommeca
    '
    Me.ec_subcommeca.AppearanceCell.Options.UseBackColor = True
    Me.ec_subcommeca.AppearanceCell.Options.UseTextOptions = True
    Me.ec_subcommeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_subcommeca.Caption = "Sub C."
    Me.ec_subcommeca.Enabled = True
    Me.ec_subcommeca.FieldName = "ec_subcommeca"
    Me.ec_subcommeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_subcommeca.Name = "ec_subcommeca"
    Me.ec_subcommeca.NTSRepositoryComboBox = Nothing
    Me.ec_subcommeca.NTSRepositoryItemCheck = Nothing
    Me.ec_subcommeca.NTSRepositoryItemMemo = Nothing
    Me.ec_subcommeca.NTSRepositoryItemText = Nothing
    Me.ec_subcommeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_subcommeca.OptionsFilter.AllowFilter = False
    Me.ec_subcommeca.Visible = True
    Me.ec_subcommeca.VisibleIndex = 21
    '
    'ec_codcena
    '
    Me.ec_codcena.AppearanceCell.Options.UseBackColor = True
    Me.ec_codcena.AppearanceCell.Options.UseTextOptions = True
    Me.ec_codcena.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_codcena.Caption = "Centro C.A."
    Me.ec_codcena.Enabled = True
    Me.ec_codcena.FieldName = "ec_codcena"
    Me.ec_codcena.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_codcena.Name = "ec_codcena"
    Me.ec_codcena.NTSRepositoryComboBox = Nothing
    Me.ec_codcena.NTSRepositoryItemCheck = Nothing
    Me.ec_codcena.NTSRepositoryItemMemo = Nothing
    Me.ec_codcena.NTSRepositoryItemText = Nothing
    Me.ec_codcena.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_codcena.OptionsFilter.AllowFilter = False
    Me.ec_codcena.Visible = True
    Me.ec_codcena.VisibleIndex = 22
    '
    'xxo_codcena
    '
    Me.xxo_codcena.AppearanceCell.Options.UseBackColor = True
    Me.xxo_codcena.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_codcena.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_codcena.Caption = "Descr. centro"
    Me.xxo_codcena.Enabled = False
    Me.xxo_codcena.FieldName = "xxo_codcena"
    Me.xxo_codcena.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_codcena.Name = "xxo_codcena"
    Me.xxo_codcena.NTSRepositoryComboBox = Nothing
    Me.xxo_codcena.NTSRepositoryItemCheck = Nothing
    Me.xxo_codcena.NTSRepositoryItemMemo = Nothing
    Me.xxo_codcena.NTSRepositoryItemText = Nothing
    Me.xxo_codcena.OptionsColumn.AllowEdit = False
    Me.xxo_codcena.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_codcena.OptionsColumn.ReadOnly = True
    Me.xxo_codcena.OptionsFilter.AllowFilter = False
    Me.xxo_codcena.Visible = True
    Me.xxo_codcena.VisibleIndex = 23
    '
    'xxo_lottox
    '
    Me.xxo_lottox.AppearanceCell.Options.UseBackColor = True
    Me.xxo_lottox.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_lottox.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_lottox.Caption = "Lotto"
    Me.xxo_lottox.Enabled = True
    Me.xxo_lottox.FieldName = "xxo_lottox"
    Me.xxo_lottox.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_lottox.Name = "xxo_lottox"
    Me.xxo_lottox.NTSRepositoryComboBox = Nothing
    Me.xxo_lottox.NTSRepositoryItemCheck = Nothing
    Me.xxo_lottox.NTSRepositoryItemMemo = Nothing
    Me.xxo_lottox.NTSRepositoryItemText = Nothing
    Me.xxo_lottox.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_lottox.OptionsFilter.AllowFilter = False
    Me.xxo_lottox.Visible = True
    Me.xxo_lottox.VisibleIndex = 24
    '
    'ec_note
    '
    Me.ec_note.AppearanceCell.Options.UseBackColor = True
    Me.ec_note.AppearanceCell.Options.UseTextOptions = True
    Me.ec_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_note.Caption = "Note"
    Me.ec_note.Enabled = True
    Me.ec_note.FieldName = "ec_note"
    Me.ec_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_note.Name = "ec_note"
    Me.ec_note.NTSRepositoryComboBox = Nothing
    Me.ec_note.NTSRepositoryItemCheck = Nothing
    Me.ec_note.NTSRepositoryItemMemo = Nothing
    Me.ec_note.NTSRepositoryItemText = Nothing
    Me.ec_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_note.OptionsFilter.AllowFilter = False
    Me.ec_note.Visible = True
    Me.ec_note.VisibleIndex = 25
    '
    'ec_valore
    '
    Me.ec_valore.AppearanceCell.Options.UseBackColor = True
    Me.ec_valore.AppearanceCell.Options.UseTextOptions = True
    Me.ec_valore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_valore.Caption = "Valore"
    Me.ec_valore.Enabled = False
    Me.ec_valore.FieldName = "ec_valore"
    Me.ec_valore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_valore.Name = "ec_valore"
    Me.ec_valore.NTSRepositoryComboBox = Nothing
    Me.ec_valore.NTSRepositoryItemCheck = Nothing
    Me.ec_valore.NTSRepositoryItemMemo = Nothing
    Me.ec_valore.NTSRepositoryItemText = Nothing
    Me.ec_valore.OptionsColumn.AllowEdit = False
    Me.ec_valore.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_valore.OptionsColumn.ReadOnly = True
    Me.ec_valore.OptionsFilter.AllowFilter = False
    Me.ec_valore.Visible = True
    Me.ec_valore.VisibleIndex = 26
    '
    'ec_contocontr
    '
    Me.ec_contocontr.AppearanceCell.Options.UseBackColor = True
    Me.ec_contocontr.AppearanceCell.Options.UseTextOptions = True
    Me.ec_contocontr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_contocontr.Caption = "Conto controp."
    Me.ec_contocontr.Enabled = False
    Me.ec_contocontr.FieldName = "ec_contocontr"
    Me.ec_contocontr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_contocontr.Name = "ec_contocontr"
    Me.ec_contocontr.NTSRepositoryComboBox = Nothing
    Me.ec_contocontr.NTSRepositoryItemCheck = Nothing
    Me.ec_contocontr.NTSRepositoryItemMemo = Nothing
    Me.ec_contocontr.NTSRepositoryItemText = Nothing
    Me.ec_contocontr.OptionsColumn.AllowEdit = False
    Me.ec_contocontr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_contocontr.OptionsColumn.ReadOnly = True
    Me.ec_contocontr.OptionsFilter.AllowFilter = False
    '
    'xxo_contocon
    '
    Me.xxo_contocon.AppearanceCell.Options.UseBackColor = True
    Me.xxo_contocon.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_contocon.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_contocon.Caption = "Descr. conto controp."
    Me.xxo_contocon.Enabled = False
    Me.xxo_contocon.FieldName = "xxo_contocon"
    Me.xxo_contocon.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_contocon.Name = "xxo_contocon"
    Me.xxo_contocon.NTSRepositoryComboBox = Nothing
    Me.xxo_contocon.NTSRepositoryItemCheck = Nothing
    Me.xxo_contocon.NTSRepositoryItemMemo = Nothing
    Me.xxo_contocon.NTSRepositoryItemText = Nothing
    Me.xxo_contocon.OptionsColumn.AllowEdit = False
    Me.xxo_contocon.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_contocon.OptionsColumn.ReadOnly = True
    Me.xxo_contocon.OptionsFilter.AllowFilter = False
    '
    'ec_codclie
    '
    Me.ec_codclie.AppearanceCell.Options.UseBackColor = True
    Me.ec_codclie.AppearanceCell.Options.UseTextOptions = True
    Me.ec_codclie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_codclie.Caption = "Cod. cliente"
    Me.ec_codclie.Enabled = True
    Me.ec_codclie.FieldName = "ec_codclie"
    Me.ec_codclie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_codclie.Name = "ec_codclie"
    Me.ec_codclie.NTSRepositoryComboBox = Nothing
    Me.ec_codclie.NTSRepositoryItemCheck = Nothing
    Me.ec_codclie.NTSRepositoryItemMemo = Nothing
    Me.ec_codclie.NTSRepositoryItemText = Nothing
    Me.ec_codclie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_codclie.OptionsFilter.AllowFilter = False
    '
    'xxo_codclie
    '
    Me.xxo_codclie.AppearanceCell.Options.UseBackColor = True
    Me.xxo_codclie.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_codclie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_codclie.Caption = "Descr. cliente"
    Me.xxo_codclie.Enabled = False
    Me.xxo_codclie.FieldName = "xxo_codclie"
    Me.xxo_codclie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_codclie.Name = "xxo_codclie"
    Me.xxo_codclie.NTSRepositoryComboBox = Nothing
    Me.xxo_codclie.NTSRepositoryItemCheck = Nothing
    Me.xxo_codclie.NTSRepositoryItemMemo = Nothing
    Me.xxo_codclie.NTSRepositoryItemText = Nothing
    Me.xxo_codclie.OptionsColumn.AllowEdit = False
    Me.xxo_codclie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_codclie.OptionsColumn.ReadOnly = True
    Me.xxo_codclie.OptionsFilter.AllowFilter = False
    '
    'ec_misura1
    '
    Me.ec_misura1.AppearanceCell.Options.UseBackColor = True
    Me.ec_misura1.AppearanceCell.Options.UseTextOptions = True
    Me.ec_misura1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_misura1.Caption = "Misura 1"
    Me.ec_misura1.Enabled = True
    Me.ec_misura1.FieldName = "ec_misura1"
    Me.ec_misura1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_misura1.Name = "ec_misura1"
    Me.ec_misura1.NTSRepositoryComboBox = Nothing
    Me.ec_misura1.NTSRepositoryItemCheck = Nothing
    Me.ec_misura1.NTSRepositoryItemMemo = Nothing
    Me.ec_misura1.NTSRepositoryItemText = Nothing
    Me.ec_misura1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_misura1.OptionsFilter.AllowFilter = False
    '
    'ec_misura2
    '
    Me.ec_misura2.AppearanceCell.Options.UseBackColor = True
    Me.ec_misura2.AppearanceCell.Options.UseTextOptions = True
    Me.ec_misura2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_misura2.Caption = "Misura 2"
    Me.ec_misura2.Enabled = True
    Me.ec_misura2.FieldName = "ec_misura2"
    Me.ec_misura2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_misura2.Name = "ec_misura2"
    Me.ec_misura2.NTSRepositoryComboBox = Nothing
    Me.ec_misura2.NTSRepositoryItemCheck = Nothing
    Me.ec_misura2.NTSRepositoryItemMemo = Nothing
    Me.ec_misura2.NTSRepositoryItemText = Nothing
    Me.ec_misura2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_misura2.OptionsFilter.AllowFilter = False
    '
    'ec_misura3
    '
    Me.ec_misura3.AppearanceCell.Options.UseBackColor = True
    Me.ec_misura3.AppearanceCell.Options.UseTextOptions = True
    Me.ec_misura3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_misura3.Caption = "Misura 3"
    Me.ec_misura3.Enabled = True
    Me.ec_misura3.FieldName = "ec_misura3"
    Me.ec_misura3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_misura3.Name = "ec_misura3"
    Me.ec_misura3.NTSRepositoryComboBox = Nothing
    Me.ec_misura3.NTSRepositoryItemCheck = Nothing
    Me.ec_misura3.NTSRepositoryItemMemo = Nothing
    Me.ec_misura3.NTSRepositoryItemText = Nothing
    Me.ec_misura3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_misura3.OptionsFilter.AllowFilter = False
    '
    'ec_perqta
    '
    Me.ec_perqta.AppearanceCell.Options.UseBackColor = True
    Me.ec_perqta.AppearanceCell.Options.UseTextOptions = True
    Me.ec_perqta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_perqta.Caption = "Prz/Qta"
    Me.ec_perqta.Enabled = False
    Me.ec_perqta.FieldName = "ec_perqta"
    Me.ec_perqta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_perqta.Name = "ec_perqta"
    Me.ec_perqta.NTSRepositoryComboBox = Nothing
    Me.ec_perqta.NTSRepositoryItemCheck = Nothing
    Me.ec_perqta.NTSRepositoryItemMemo = Nothing
    Me.ec_perqta.NTSRepositoryItemText = Nothing
    Me.ec_perqta.OptionsColumn.AllowEdit = False
    Me.ec_perqta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_perqta.OptionsColumn.ReadOnly = True
    Me.ec_perqta.OptionsFilter.AllowFilter = False
    '
    'ec_flcom
    '
    Me.ec_flcom.AppearanceCell.Options.UseBackColor = True
    Me.ec_flcom.AppearanceCell.Options.UseTextOptions = True
    Me.ec_flcom.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_flcom.Caption = "Da controllare"
    Me.ec_flcom.Enabled = True
    Me.ec_flcom.FieldName = "ec_flcom"
    Me.ec_flcom.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_flcom.Name = "ec_flcom"
    Me.ec_flcom.NTSRepositoryComboBox = Nothing
    Me.ec_flcom.NTSRepositoryItemCheck = Nothing
    Me.ec_flcom.NTSRepositoryItemMemo = Nothing
    Me.ec_flcom.NTSRepositoryItemText = Nothing
    Me.ec_flcom.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_flcom.OptionsFilter.AllowFilter = False
    '
    'ec_flprznet
    '
    Me.ec_flprznet.AppearanceCell.Options.UseBackColor = True
    Me.ec_flprznet.AppearanceCell.Options.UseTextOptions = True
    Me.ec_flprznet.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_flprznet.Caption = "Prezzo netto"
    Me.ec_flprznet.Enabled = True
    Me.ec_flprznet.FieldName = "ec_flprznet"
    Me.ec_flprznet.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_flprznet.Name = "ec_flprznet"
    Me.ec_flprznet.NTSRepositoryComboBox = Nothing
    Me.ec_flprznet.NTSRepositoryItemCheck = Nothing
    Me.ec_flprznet.NTSRepositoryItemMemo = Nothing
    Me.ec_flprznet.NTSRepositoryItemText = Nothing
    Me.ec_flprznet.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_flprznet.OptionsFilter.AllowFilter = False
    '
    'ec_flforf
    '
    Me.ec_flforf.AppearanceCell.Options.UseBackColor = True
    Me.ec_flforf.AppearanceCell.Options.UseTextOptions = True
    Me.ec_flforf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_flforf.Caption = "A forfait"
    Me.ec_flforf.Enabled = True
    Me.ec_flforf.FieldName = "ec_flforf"
    Me.ec_flforf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_flforf.Name = "ec_flforf"
    Me.ec_flforf.NTSRepositoryComboBox = Nothing
    Me.ec_flforf.NTSRepositoryItemCheck = Nothing
    Me.ec_flforf.NTSRepositoryItemMemo = Nothing
    Me.ec_flforf.NTSRepositoryItemText = Nothing
    Me.ec_flforf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_flforf.OptionsFilter.AllowFilter = False
    '
    'ec_matric
    '
    Me.ec_matric.AppearanceCell.Options.UseBackColor = True
    Me.ec_matric.AppearanceCell.Options.UseTextOptions = True
    Me.ec_matric.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_matric.Caption = "Matricola"
    Me.ec_matric.Enabled = True
    Me.ec_matric.FieldName = "ec_matric"
    Me.ec_matric.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_matric.Name = "ec_matric"
    Me.ec_matric.NTSRepositoryComboBox = Nothing
    Me.ec_matric.NTSRepositoryItemCheck = Nothing
    Me.ec_matric.NTSRepositoryItemMemo = Nothing
    Me.ec_matric.NTSRepositoryItemText = Nothing
    Me.ec_matric.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_matric.OptionsFilter.AllowFilter = False
    '
    'ec_umprz
    '
    Me.ec_umprz.AppearanceCell.Options.UseBackColor = True
    Me.ec_umprz.AppearanceCell.Options.UseTextOptions = True
    Me.ec_umprz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_umprz.Caption = "Prezzi x u.d.m."
    Me.ec_umprz.Enabled = False
    Me.ec_umprz.FieldName = "ec_umprz"
    Me.ec_umprz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_umprz.Name = "ec_umprz"
    Me.ec_umprz.NTSRepositoryComboBox = Nothing
    Me.ec_umprz.NTSRepositoryItemCheck = Nothing
    Me.ec_umprz.NTSRepositoryItemMemo = Nothing
    Me.ec_umprz.NTSRepositoryItemText = Nothing
    Me.ec_umprz.OptionsColumn.AllowEdit = False
    Me.ec_umprz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_umprz.OptionsColumn.ReadOnly = True
    Me.ec_umprz.OptionsFilter.AllowFilter = False
    '
    'ec_fase
    '
    Me.ec_fase.AppearanceCell.Options.UseBackColor = True
    Me.ec_fase.AppearanceCell.Options.UseTextOptions = True
    Me.ec_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_fase.Caption = "Fase"
    Me.ec_fase.Enabled = True
    Me.ec_fase.FieldName = "ec_fase"
    Me.ec_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_fase.Name = "ec_fase"
    Me.ec_fase.NTSRepositoryComboBox = Nothing
    Me.ec_fase.NTSRepositoryItemCheck = Nothing
    Me.ec_fase.NTSRepositoryItemMemo = Nothing
    Me.ec_fase.NTSRepositoryItemText = Nothing
    Me.ec_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_fase.OptionsFilter.AllowFilter = False
    '
    'xxo_fase
    '
    Me.xxo_fase.AppearanceCell.Options.UseBackColor = True
    Me.xxo_fase.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_fase.Caption = "Descr. fase"
    Me.xxo_fase.Enabled = False
    Me.xxo_fase.FieldName = "xxo_fase"
    Me.xxo_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_fase.Name = "xxo_fase"
    Me.xxo_fase.NTSRepositoryComboBox = Nothing
    Me.xxo_fase.NTSRepositoryItemCheck = Nothing
    Me.xxo_fase.NTSRepositoryItemMemo = Nothing
    Me.xxo_fase.NTSRepositoryItemText = Nothing
    Me.xxo_fase.OptionsColumn.AllowEdit = False
    Me.xxo_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_fase.OptionsColumn.ReadOnly = True
    Me.xxo_fase.OptionsFilter.AllowFilter = False
    '
    'ec_codlavo
    '
    Me.ec_codlavo.AppearanceCell.Options.UseBackColor = True
    Me.ec_codlavo.AppearanceCell.Options.UseTextOptions = True
    Me.ec_codlavo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_codlavo.Caption = "Codice lavoraz."
    Me.ec_codlavo.Enabled = False
    Me.ec_codlavo.FieldName = "ec_codlavo"
    Me.ec_codlavo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_codlavo.Name = "ec_codlavo"
    Me.ec_codlavo.NTSRepositoryComboBox = Nothing
    Me.ec_codlavo.NTSRepositoryItemCheck = Nothing
    Me.ec_codlavo.NTSRepositoryItemMemo = Nothing
    Me.ec_codlavo.NTSRepositoryItemText = Nothing
    Me.ec_codlavo.OptionsColumn.AllowEdit = False
    Me.ec_codlavo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_codlavo.OptionsColumn.ReadOnly = True
    Me.ec_codlavo.OptionsFilter.AllowFilter = False
    '
    'xxo_codlavo
    '
    Me.xxo_codlavo.AppearanceCell.Options.UseBackColor = True
    Me.xxo_codlavo.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_codlavo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_codlavo.Caption = "Descr. lavoraz."
    Me.xxo_codlavo.Enabled = False
    Me.xxo_codlavo.FieldName = "xxo_codlavo"
    Me.xxo_codlavo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_codlavo.Name = "xxo_codlavo"
    Me.xxo_codlavo.NTSRepositoryComboBox = Nothing
    Me.xxo_codlavo.NTSRepositoryItemCheck = Nothing
    Me.xxo_codlavo.NTSRepositoryItemMemo = Nothing
    Me.xxo_codlavo.NTSRepositoryItemText = Nothing
    Me.xxo_codlavo.OptionsColumn.AllowEdit = False
    Me.xxo_codlavo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_codlavo.OptionsColumn.ReadOnly = True
    Me.xxo_codlavo.OptionsFilter.AllowFilter = False
    '
    'ec_datini
    '
    Me.ec_datini.AppearanceCell.Options.UseBackColor = True
    Me.ec_datini.AppearanceCell.Options.UseTextOptions = True
    Me.ec_datini.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_datini.Caption = "Dt iniz. comp. econ."
    Me.ec_datini.Enabled = True
    Me.ec_datini.FieldName = "ec_datini"
    Me.ec_datini.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_datini.Name = "ec_datini"
    Me.ec_datini.NTSRepositoryComboBox = Nothing
    Me.ec_datini.NTSRepositoryItemCheck = Nothing
    Me.ec_datini.NTSRepositoryItemMemo = Nothing
    Me.ec_datini.NTSRepositoryItemText = Nothing
    Me.ec_datini.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_datini.OptionsFilter.AllowFilter = False
    '
    'ec_datfin
    '
    Me.ec_datfin.AppearanceCell.Options.UseBackColor = True
    Me.ec_datfin.AppearanceCell.Options.UseTextOptions = True
    Me.ec_datfin.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_datfin.Caption = "Dt fin. comp. econ."
    Me.ec_datfin.Enabled = True
    Me.ec_datfin.FieldName = "ec_datfin"
    Me.ec_datfin.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_datfin.Name = "ec_datfin"
    Me.ec_datfin.NTSRepositoryComboBox = Nothing
    Me.ec_datfin.NTSRepositoryItemCheck = Nothing
    Me.ec_datfin.NTSRepositoryItemMemo = Nothing
    Me.ec_datfin.NTSRepositoryItemText = Nothing
    Me.ec_datfin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_datfin.OptionsFilter.AllowFilter = False
    '
    'ec_valorev
    '
    Me.ec_valorev.AppearanceCell.Options.UseBackColor = True
    Me.ec_valorev.AppearanceCell.Options.UseTextOptions = True
    Me.ec_valorev.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_valorev.Caption = "Valore in valuta"
    Me.ec_valorev.Enabled = False
    Me.ec_valorev.FieldName = "ec_valorev"
    Me.ec_valorev.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_valorev.Name = "ec_valorev"
    Me.ec_valorev.NTSRepositoryComboBox = Nothing
    Me.ec_valorev.NTSRepositoryItemCheck = Nothing
    Me.ec_valorev.NTSRepositoryItemMemo = Nothing
    Me.ec_valorev.NTSRepositoryItemText = Nothing
    Me.ec_valorev.OptionsColumn.AllowEdit = False
    Me.ec_valorev.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_valorev.OptionsColumn.ReadOnly = True
    Me.ec_valorev.OptionsFilter.AllowFilter = False
    '
    'ec_pmtaskid
    '
    Me.ec_pmtaskid.AppearanceCell.Options.UseBackColor = True
    Me.ec_pmtaskid.AppearanceCell.Options.UseTextOptions = True
    Me.ec_pmtaskid.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_pmtaskid.Caption = "Descr. Task"
    Me.ec_pmtaskid.Enabled = False
    Me.ec_pmtaskid.FieldName = "ec_pmtaskid"
    Me.ec_pmtaskid.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_pmtaskid.Name = "ec_pmtaskid"
    Me.ec_pmtaskid.NTSRepositoryComboBox = Nothing
    Me.ec_pmtaskid.NTSRepositoryItemCheck = Nothing
    Me.ec_pmtaskid.NTSRepositoryItemMemo = Nothing
    Me.ec_pmtaskid.NTSRepositoryItemText = Nothing
    Me.ec_pmtaskid.OptionsColumn.AllowEdit = False
    Me.ec_pmtaskid.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_pmtaskid.OptionsColumn.ReadOnly = True
    Me.ec_pmtaskid.OptionsFilter.AllowFilter = False
    '
    'ec_pmsalcon
    '
    Me.ec_pmsalcon.AppearanceCell.Options.UseBackColor = True
    Me.ec_pmsalcon.AppearanceCell.Options.UseTextOptions = True
    Me.ec_pmsalcon.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_pmsalcon.Caption = "Sal. Task"
    Me.ec_pmsalcon.Enabled = True
    Me.ec_pmsalcon.FieldName = "ec_pmsalcon"
    Me.ec_pmsalcon.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_pmsalcon.Name = "ec_pmsalcon"
    Me.ec_pmsalcon.NTSRepositoryComboBox = Nothing
    Me.ec_pmsalcon.NTSRepositoryItemCheck = Nothing
    Me.ec_pmsalcon.NTSRepositoryItemMemo = Nothing
    Me.ec_pmsalcon.NTSRepositoryItemText = Nothing
    Me.ec_pmsalcon.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_pmsalcon.OptionsFilter.AllowFilter = False
    '
    'ec_pmqtadis
    '
    Me.ec_pmqtadis.AppearanceCell.Options.UseBackColor = True
    Me.ec_pmqtadis.AppearanceCell.Options.UseTextOptions = True
    Me.ec_pmqtadis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_pmqtadis.Caption = "PMQTADIS"
    Me.ec_pmqtadis.Enabled = False
    Me.ec_pmqtadis.FieldName = "ec_pmqtadis"
    Me.ec_pmqtadis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_pmqtadis.Name = "ec_pmqtadis"
    Me.ec_pmqtadis.NTSRepositoryComboBox = Nothing
    Me.ec_pmqtadis.NTSRepositoryItemCheck = Nothing
    Me.ec_pmqtadis.NTSRepositoryItemMemo = Nothing
    Me.ec_pmqtadis.NTSRepositoryItemText = Nothing
    Me.ec_pmqtadis.OptionsColumn.AllowEdit = False
    Me.ec_pmqtadis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_pmqtadis.OptionsColumn.ReadOnly = True
    Me.ec_pmqtadis.OptionsFilter.AllowFilter = False
    '
    'ec_pmvaldis
    '
    Me.ec_pmvaldis.AppearanceCell.Options.UseBackColor = True
    Me.ec_pmvaldis.AppearanceCell.Options.UseTextOptions = True
    Me.ec_pmvaldis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_pmvaldis.Caption = "PMVALDIS"
    Me.ec_pmvaldis.Enabled = False
    Me.ec_pmvaldis.FieldName = "ec_pmvaldis"
    Me.ec_pmvaldis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_pmvaldis.Name = "ec_pmvaldis"
    Me.ec_pmvaldis.NTSRepositoryComboBox = Nothing
    Me.ec_pmvaldis.NTSRepositoryItemCheck = Nothing
    Me.ec_pmvaldis.NTSRepositoryItemMemo = Nothing
    Me.ec_pmvaldis.NTSRepositoryItemText = Nothing
    Me.ec_pmvaldis.OptionsColumn.AllowEdit = False
    Me.ec_pmvaldis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_pmvaldis.OptionsColumn.ReadOnly = True
    Me.ec_pmvaldis.OptionsFilter.AllowFilter = False
    '
    'ec_rdatipork
    '
    Me.ec_rdatipork.AppearanceCell.Options.UseBackColor = True
    Me.ec_rdatipork.AppearanceCell.Options.UseTextOptions = True
    Me.ec_rdatipork.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_rdatipork.Caption = "Tipo RDA"
    Me.ec_rdatipork.Enabled = False
    Me.ec_rdatipork.FieldName = "ec_rdatipork"
    Me.ec_rdatipork.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_rdatipork.Name = "ec_rdatipork"
    Me.ec_rdatipork.NTSRepositoryComboBox = Nothing
    Me.ec_rdatipork.NTSRepositoryItemCheck = Nothing
    Me.ec_rdatipork.NTSRepositoryItemMemo = Nothing
    Me.ec_rdatipork.NTSRepositoryItemText = Nothing
    Me.ec_rdatipork.OptionsColumn.AllowEdit = False
    Me.ec_rdatipork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_rdatipork.OptionsColumn.ReadOnly = True
    Me.ec_rdatipork.OptionsFilter.AllowFilter = False
    '
    'ec_rdaanno
    '
    Me.ec_rdaanno.AppearanceCell.Options.UseBackColor = True
    Me.ec_rdaanno.AppearanceCell.Options.UseTextOptions = True
    Me.ec_rdaanno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_rdaanno.Caption = "Anno RDA"
    Me.ec_rdaanno.Enabled = False
    Me.ec_rdaanno.FieldName = "ec_rdaanno"
    Me.ec_rdaanno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_rdaanno.Name = "ec_rdaanno"
    Me.ec_rdaanno.NTSRepositoryComboBox = Nothing
    Me.ec_rdaanno.NTSRepositoryItemCheck = Nothing
    Me.ec_rdaanno.NTSRepositoryItemMemo = Nothing
    Me.ec_rdaanno.NTSRepositoryItemText = Nothing
    Me.ec_rdaanno.OptionsColumn.AllowEdit = False
    Me.ec_rdaanno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_rdaanno.OptionsColumn.ReadOnly = True
    Me.ec_rdaanno.OptionsFilter.AllowFilter = False
    '
    'ec_rdaserie
    '
    Me.ec_rdaserie.AppearanceCell.Options.UseBackColor = True
    Me.ec_rdaserie.AppearanceCell.Options.UseTextOptions = True
    Me.ec_rdaserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_rdaserie.Caption = "Serie RDA"
    Me.ec_rdaserie.Enabled = False
    Me.ec_rdaserie.FieldName = "ec_rdaserie"
    Me.ec_rdaserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_rdaserie.Name = "ec_rdaserie"
    Me.ec_rdaserie.NTSRepositoryComboBox = Nothing
    Me.ec_rdaserie.NTSRepositoryItemCheck = Nothing
    Me.ec_rdaserie.NTSRepositoryItemMemo = Nothing
    Me.ec_rdaserie.NTSRepositoryItemText = Nothing
    Me.ec_rdaserie.OptionsColumn.AllowEdit = False
    Me.ec_rdaserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_rdaserie.OptionsColumn.ReadOnly = True
    Me.ec_rdaserie.OptionsFilter.AllowFilter = False
    '
    'ec_rdanum
    '
    Me.ec_rdanum.AppearanceCell.Options.UseBackColor = True
    Me.ec_rdanum.AppearanceCell.Options.UseTextOptions = True
    Me.ec_rdanum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_rdanum.Caption = "Num. RDA"
    Me.ec_rdanum.Enabled = False
    Me.ec_rdanum.FieldName = "ec_rdanum"
    Me.ec_rdanum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_rdanum.Name = "ec_rdanum"
    Me.ec_rdanum.NTSRepositoryComboBox = Nothing
    Me.ec_rdanum.NTSRepositoryItemCheck = Nothing
    Me.ec_rdanum.NTSRepositoryItemMemo = Nothing
    Me.ec_rdanum.NTSRepositoryItemText = Nothing
    Me.ec_rdanum.OptionsColumn.AllowEdit = False
    Me.ec_rdanum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_rdanum.OptionsColumn.ReadOnly = True
    Me.ec_rdanum.OptionsFilter.AllowFilter = False
    '
    'ec_rdariga
    '
    Me.ec_rdariga.AppearanceCell.Options.UseBackColor = True
    Me.ec_rdariga.AppearanceCell.Options.UseTextOptions = True
    Me.ec_rdariga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_rdariga.Caption = "Riga RDA"
    Me.ec_rdariga.Enabled = False
    Me.ec_rdariga.FieldName = "ec_rdariga"
    Me.ec_rdariga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_rdariga.Name = "ec_rdariga"
    Me.ec_rdariga.NTSRepositoryComboBox = Nothing
    Me.ec_rdariga.NTSRepositoryItemCheck = Nothing
    Me.ec_rdariga.NTSRepositoryItemMemo = Nothing
    Me.ec_rdariga.NTSRepositoryItemText = Nothing
    Me.ec_rdariga.OptionsColumn.AllowEdit = False
    Me.ec_rdariga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_rdariga.OptionsColumn.ReadOnly = True
    Me.ec_rdariga.OptionsFilter.AllowFilter = False
    '
    'ec_offreq
    '
    Me.ec_offreq.AppearanceCell.Options.UseBackColor = True
    Me.ec_offreq.AppearanceCell.Options.UseTextOptions = True
    Me.ec_offreq.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_offreq.Caption = "OFFREQ"
    Me.ec_offreq.Enabled = False
    Me.ec_offreq.FieldName = "ec_offreq"
    Me.ec_offreq.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_offreq.Name = "ec_offreq"
    Me.ec_offreq.NTSRepositoryComboBox = Nothing
    Me.ec_offreq.NTSRepositoryItemCheck = Nothing
    Me.ec_offreq.NTSRepositoryItemMemo = Nothing
    Me.ec_offreq.NTSRepositoryItemText = Nothing
    Me.ec_offreq.OptionsColumn.AllowEdit = False
    Me.ec_offreq.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_offreq.OptionsColumn.ReadOnly = True
    Me.ec_offreq.OptionsFilter.AllowFilter = False
    '
    'ec_ortipork
    '
    Me.ec_ortipork.AppearanceCell.Options.UseBackColor = True
    Me.ec_ortipork.AppearanceCell.Options.UseTextOptions = True
    Me.ec_ortipork.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_ortipork.Caption = "Tipo ord.gen."
    Me.ec_ortipork.Enabled = False
    Me.ec_ortipork.FieldName = "ec_ortipork"
    Me.ec_ortipork.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_ortipork.Name = "ec_ortipork"
    Me.ec_ortipork.NTSRepositoryComboBox = Nothing
    Me.ec_ortipork.NTSRepositoryItemCheck = Nothing
    Me.ec_ortipork.NTSRepositoryItemMemo = Nothing
    Me.ec_ortipork.NTSRepositoryItemText = Nothing
    Me.ec_ortipork.OptionsColumn.AllowEdit = False
    Me.ec_ortipork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_ortipork.OptionsColumn.ReadOnly = True
    Me.ec_ortipork.OptionsFilter.AllowFilter = False
    '
    'ec_oranno
    '
    Me.ec_oranno.AppearanceCell.Options.UseBackColor = True
    Me.ec_oranno.AppearanceCell.Options.UseTextOptions = True
    Me.ec_oranno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_oranno.Caption = "Anno ord.gen."
    Me.ec_oranno.Enabled = False
    Me.ec_oranno.FieldName = "ec_oranno"
    Me.ec_oranno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_oranno.Name = "ec_oranno"
    Me.ec_oranno.NTSRepositoryComboBox = Nothing
    Me.ec_oranno.NTSRepositoryItemCheck = Nothing
    Me.ec_oranno.NTSRepositoryItemMemo = Nothing
    Me.ec_oranno.NTSRepositoryItemText = Nothing
    Me.ec_oranno.OptionsColumn.AllowEdit = False
    Me.ec_oranno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_oranno.OptionsColumn.ReadOnly = True
    Me.ec_oranno.OptionsFilter.AllowFilter = False
    '
    'ec_orserie
    '
    Me.ec_orserie.AppearanceCell.Options.UseBackColor = True
    Me.ec_orserie.AppearanceCell.Options.UseTextOptions = True
    Me.ec_orserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_orserie.Caption = "Serie ord.gen."
    Me.ec_orserie.Enabled = False
    Me.ec_orserie.FieldName = "ec_orserie"
    Me.ec_orserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_orserie.Name = "ec_orserie"
    Me.ec_orserie.NTSRepositoryComboBox = Nothing
    Me.ec_orserie.NTSRepositoryItemCheck = Nothing
    Me.ec_orserie.NTSRepositoryItemMemo = Nothing
    Me.ec_orserie.NTSRepositoryItemText = Nothing
    Me.ec_orserie.OptionsColumn.AllowEdit = False
    Me.ec_orserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_orserie.OptionsColumn.ReadOnly = True
    Me.ec_orserie.OptionsFilter.AllowFilter = False
    '
    'ec_ornum
    '
    Me.ec_ornum.AppearanceCell.Options.UseBackColor = True
    Me.ec_ornum.AppearanceCell.Options.UseTextOptions = True
    Me.ec_ornum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_ornum.Caption = "N° ord.gen."
    Me.ec_ornum.Enabled = False
    Me.ec_ornum.FieldName = "ec_ornum"
    Me.ec_ornum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_ornum.Name = "ec_ornum"
    Me.ec_ornum.NTSRepositoryComboBox = Nothing
    Me.ec_ornum.NTSRepositoryItemCheck = Nothing
    Me.ec_ornum.NTSRepositoryItemMemo = Nothing
    Me.ec_ornum.NTSRepositoryItemText = Nothing
    Me.ec_ornum.OptionsColumn.AllowEdit = False
    Me.ec_ornum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_ornum.OptionsColumn.ReadOnly = True
    Me.ec_ornum.OptionsFilter.AllowFilter = False
    '
    'ec_orriga
    '
    Me.ec_orriga.AppearanceCell.Options.UseBackColor = True
    Me.ec_orriga.AppearanceCell.Options.UseTextOptions = True
    Me.ec_orriga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_orriga.Caption = "Riga ord.gen."
    Me.ec_orriga.Enabled = False
    Me.ec_orriga.FieldName = "ec_orriga"
    Me.ec_orriga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_orriga.Name = "ec_orriga"
    Me.ec_orriga.NTSRepositoryComboBox = Nothing
    Me.ec_orriga.NTSRepositoryItemCheck = Nothing
    Me.ec_orriga.NTSRepositoryItemMemo = Nothing
    Me.ec_orriga.NTSRepositoryItemText = Nothing
    Me.ec_orriga.OptionsColumn.AllowEdit = False
    Me.ec_orriga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_orriga.OptionsColumn.ReadOnly = True
    Me.ec_orriga.OptionsFilter.AllowFilter = False
    '
    'xxo_codtagl
    '
    Me.xxo_codtagl.AppearanceCell.Options.UseBackColor = True
    Me.xxo_codtagl.AppearanceCell.Options.UseTextOptions = True
    Me.xxo_codtagl.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xxo_codtagl.Enabled = False
    Me.xxo_codtagl.FieldName = "xxo_codtagl"
    Me.xxo_codtagl.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxo_codtagl.Name = "xxo_codtagl"
    Me.xxo_codtagl.NTSRepositoryComboBox = Nothing
    Me.xxo_codtagl.NTSRepositoryItemCheck = Nothing
    Me.xxo_codtagl.NTSRepositoryItemMemo = Nothing
    Me.xxo_codtagl.NTSRepositoryItemText = Nothing
    Me.xxo_codtagl.OptionsColumn.AllowEdit = False
    Me.xxo_codtagl.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxo_codtagl.OptionsColumn.ReadOnly = True
    Me.xxo_codtagl.OptionsFilter.AllowFilter = False
    '
    'ec_tctaglia
    '
    Me.ec_tctaglia.AppearanceCell.Options.UseBackColor = True
    Me.ec_tctaglia.AppearanceCell.Options.UseTextOptions = True
    Me.ec_tctaglia.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_tctaglia.Caption = "Taglia padre"
    Me.ec_tctaglia.Enabled = False
    Me.ec_tctaglia.FieldName = "ec_tctaglia"
    Me.ec_tctaglia.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_tctaglia.Name = "ec_tctaglia"
    Me.ec_tctaglia.NTSRepositoryComboBox = Nothing
    Me.ec_tctaglia.NTSRepositoryItemCheck = Nothing
    Me.ec_tctaglia.NTSRepositoryItemMemo = Nothing
    Me.ec_tctaglia.NTSRepositoryItemText = Nothing
    Me.ec_tctaglia.OptionsColumn.AllowEdit = False
    Me.ec_tctaglia.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_tctaglia.OptionsColumn.ReadOnly = True
    Me.ec_tctaglia.OptionsFilter.AllowFilter = False
    Me.ec_tctaglia.Visible = True
    Me.ec_tctaglia.VisibleIndex = 27
    '
    'ec_prezvalc
    '
    Me.ec_prezvalc.AppearanceCell.Options.UseBackColor = True
    Me.ec_prezvalc.AppearanceCell.Options.UseTextOptions = True
    Me.ec_prezvalc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ec_prezvalc.Caption = "Prezzo in valuta"
    Me.ec_prezvalc.Enabled = True
    Me.ec_prezvalc.FieldName = "ec_prezvalc"
    Me.ec_prezvalc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ec_prezvalc.Name = "ec_prezvalc"
    Me.ec_prezvalc.NTSRepositoryComboBox = Nothing
    Me.ec_prezvalc.NTSRepositoryItemCheck = Nothing
    Me.ec_prezvalc.NTSRepositoryItemMemo = Nothing
    Me.ec_prezvalc.NTSRepositoryItemText = Nothing
    Me.ec_prezvalc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ec_prezvalc.OptionsFilter.AllowFilter = False
    Me.ec_prezvalc.Visible = True
    Me.ec_prezvalc.VisibleIndex = 28
    '
    'FRMORIMP1
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(740, 442)
    Me.Controls.Add(Me.grRighe)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMORIMP1"
    Me.Text = "IMPEGNI COLLEGATI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grRighe, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvRighe, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overloads Function InitEntity(ByRef Menu As CLE__MENU, ByRef CleGsol As CLEORGSOL, ByRef ds As DataSet) As Boolean
    Dim dttSc As New DataTable
    oMenu = Menu
    oApp = oMenu.App
    oCleGsol = CleGsol
    DittaCorrente = oCleGsol.strDittaCorrente
    dttUm = oCleGsol.CaricaUnMis()
    Me.GctlTipoDoc = "Y"

    InitializeComponent()
    Me.MinimumSize = Me.Size

    '-------------------------------
    'leggo dal database i dati e collego il NTSBinding
    ds.AcceptChanges()
    oCleGsol.dsImpe = ds
    oCleGsol.CorpoImpSetDataTable(DittaCorrente, oCleGsol.dsImpe.Tables("CORPOIMP"))
    dcImpe.DataSource = oCleGsol.dsImpe.Tables("CORPOIMP")
    oCleGsol.dsImpe.AcceptChanges()

    '------------------------------
    'collego gli eventi dell'entity
    AddHandler oCleGsol.RemoteEvent, AddressOf GestisciEventiEntity

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttTask As New DataTable()
    Dim dttStato As New DataTable()
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\recnew.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\recagg.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\recdelete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\recrestore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbNavigazMrp.GlyphPath = (oApp.ChildImageDir & "\movmrp.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      dttTask.Columns.Add("cod", GetType(String))
      dttTask.Columns.Add("val", GetType(String))
      dttTask.Rows.Add(New Object() {"S", "Saldato"})
      dttTask.Rows.Add(New Object() {"C", "Aperto"})
      dttTask.Rows.Add(New Object() {"Q", "Q"})
      dttTask.Rows.Add(New Object() {"V", "B"})
      dttTask.AcceptChanges()

      dttStato.Columns.Add("cod", GetType(String))
      dttStato.Columns.Add("val", GetType(String))
      dttStato.Rows.Add(New Object() {" ", "Generato"})
      dttStato.Rows.Add(New Object() {"P", "Emessione RDA"})
      dttStato.Rows.Add(New Object() {"Q", "Approvazione RDA"})
      dttStato.Rows.Add(New Object() {"R", "Emissione RDO"})
      dttStato.Rows.Add(New Object() {"S", "Confermato"})
      dttStato.Rows.Add(New Object() {"T", "Emesso ordine"})
      dttStato.Rows.Add(New Object() {"F", "Congelato"})
      dttStato.Rows.Add(New Object() {"X", "Temporaneo"})
      dttStato.AcceptChanges()

      '-------------------------------------------------
      'carico le unità di misura nella colonna colli: caso particolare
      'carico tutte le unità di misura gestite dagli articoli, 
      'poi al cambio di riga filtrero nel combo solo quelle gestite dall'articolo in analisi
      dttUm = oCleGsol.CaricaUnMis()
      dttUm.AcceptChanges()

      grvRighe.NTSSetParam(oMenu, "GESTIONE PROPOSTE IMPEGNI DI PRODUZIONE")
      grvRighe.NTSSetParam(oMenu, oApp.Tr(Me, 129048533112552379, "Griglia righe ordine"))
      ec_matric.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048533139741619, "Barcode"), 30, False)
      ec_codart.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 129048533606482543, "Cod. Art."), tabartico, False)
      ec_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048533634452367, "Descrizione"), 40, True)
      ec_desint.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048533684923055, "Descr.interna"), 40, True)
      ec_unmis.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129048533717580559, "U.M."), dttUm, "tb_codumis", "tb_codumis")
      ec_colli.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048533741643983, "Colli ord."), oApp.FormatQta, 13)
      ec_ump.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048533764613615, "UMP"), 3, False)
      ec_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048533788677039, "Q.tà  ordin."), oApp.FormatQta, 13)
      ec_prezzo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048533809615343, "Prezzo"), oApp.FormatPrzUn, 13)
      ec_prezvalc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048533851179439, "Prezzo in valuta"), oApp.FormatPrzUnVal, 13)
      ec_magaz.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129048533885086991, "Magazzino"), tabmaga)
      xxo_magaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048533923213455, "Descr. magazzino"), 0, True)
      ec_datcons.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129048533945245551, "Data cons."), False)
      ec_controp.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129048533966652623, "Controp."), tabcove)
      xxo_controp.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048533993372399, "Descr. controp."), 0, True)
      ec_codiva.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129048534015560751, "Cod. IVA"), tabciva)
      xxo_codiva.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048534038061615, "Descr. IVA"), 0, True)
      ec_codcfam.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 129048534066031439, "Linea/Fam."), tabcfam, True)
      xxo_codcfam.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048534088532303, "Descr. linea/fam"), 0, True)
      ec_commeca.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129048534112595727, "Comm. C.A."), tabcommess)
      xxo_commeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048534131346447, "Descr. commessa"), 0, True)
      ec_subcommeca.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 129048534151815983, "Sub C."), tabsubcomm, True)
      If oCleGsol.bLottoNew = False Then
        xxo_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130420272437345320, "Lotto"), 9, True)
      Else
        xxo_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130420272800941397, "Lotto"), 50, True)
      End If
      ec_codcena.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129048534198380271, "Centro C.A."), tabcena)
      xxo_codcena.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048534229475215, "Descr. centro"), 0, True)
      ec_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048534256194991, "Note"), 0, True)
      ec_valore.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048534296509039, "Valore"), oApp.FormatImporti, 13)
      ec_valorev.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048534319478671, "Valore in valuta"), oApp.FormatImpVal, 13)
      ec_contocontr.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129048534343073327, "Conto controp."), tabanagra)
      xxo_contocon.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048534366355471, "Descr. conto controp."), 0, True)
      ec_codclie.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129048534386668751, "Cod. cliente"), tabanagrac)
      xxo_codclie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048534408700847, "Descr. cliente"), 0, True)
      ec_misura1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048534426982799, "Misura 1"), "#,##0.00", 13)
      ec_misura2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048534444952239, "Misura 2"), "#,##0.00", 13)
      ec_misura3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048534468546895, "Misura 3"), "#,##0.00", 13)
      ec_perqta.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048537037234599, "Prz/Qta"), "#,##0.00", 13)
      ec_umprz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048534489172687, "Prezzi x u.d.m."), 3, False)
      ec_fase.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129048534557456559, "Fase"), tabartfasi)
      ec_fase.ArtiPerFase = ec_codart
      xxo_fase.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048534588395247, "Descr. fase"), 0, True)
      ec_codlavo.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129048534606833455, "Codice lavoraz."), tablavo)
      xxo_codlavo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048534628240527, "Descr. lavoraz."), 0, True)
      ec_pmtaskid.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048534660429263, "Id Task"), "0", 9, 0, 999999999)
      ec_pmtaskid.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048534687305295, "Descr. Task"), 0, True)
      ec_pmsalcon.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129048534709806159, "Sal. Task"), dttTask, "val", "cod")
      ec_flprznet.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129048534731838255, "Prezzo netto"), "S", "N")
      ec_datini.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129048534751370255, "Dt iniz. comp. econ."), False)
      ec_datfin.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129048534770745999, "Dt fin. comp. econ."), False)

      ec_riga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048534833873423, "Progr."), "0", 9, 0, 999999999)
      ec_stato.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129048534856061775, "Stato"), dttStato, "val", "cod")
      ec_datord.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129048534884812879, "Data mass. ord"), False)
      ec_flcom.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129048534903407343, "Da controllare"), "S", "N")
      ec_flforf.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129048534922939343, "A forfait"), "S", "N")
      ec_pmqtadis.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048534941221295, "PMQTADIS"), oApp.FormatQta, 15)
      ec_pmvaldis.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048534962159599, "PMVALDIS"), oApp.FormatQta, 15)
      ec_rdatipork.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048534995910895, "Tipo RDA"), 1, False)
      ec_rdaanno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048535017630479, "Anno RDA"), "0", 4, 0, 9999)
      ec_rdaserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048535042943951, "Serie RDA"), CLN__STD.SerieMaxLen, False)
      ec_rdanum.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048535062007183, "Num. RDA"), "0", 9, 0, 999999999)
      ec_rdariga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048535082945487, "Riga RDA"), "0", 9, 0, 999999999)
      ec_offreq.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048535103883791, "OFFREQ"), 1, False)
      ec_ortipork.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048535126853423, "Tipo ord.gen."), 1, False)
      ec_oranno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048535148573007, "Anno ord.gen."), "0", 4, 0, 9999)
      ec_orserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048535168417519, "Serie ord.gen."), CLN__STD.SerieMaxLen, False)
      ec_ornum.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048535188105775, "N° ord.gen."), "0", 9, 0, 999999999)
      ec_orriga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048535208106543, "Riga ord.gen."), "0", 9, 0, 999999999)
      xxo_codtagl.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048536043607375, "."), "0", 4, 0, 9999)
      ec_tctaglia.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048536071577199, "Taglia padre"), 4, False)
      xxo_tctagliaf.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129048536071577200, "Taglia figlio"), 4, False)

      ec_prezzo.NTSSetParamZoom("ZOOMPREZZO")
      ec_prezvalc.NTSSetParamZoom("ZOOMPREZZO")
      xxo_lottox.NTSSetParamZoom("ZOOMANALOTTI")

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
  Public Overridable Sub FRMORIMP1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      grRighe.DataSource = dcImpe

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      '-------------------------------------------------
      'visualizzo/nascondo il cod taglia al bisogno
      If NTSCInt(oCleGsol.dtrHT!xxo_codtagl) = 0 Then
        ec_tctaglia.Visible = False
        xxo_tctagliaf.Visible = False
      Else
        GctlSetVisEnab(ec_tctaglia, True)
        If oCleGsol.bGestioneAbbinamentiTaglie Then
          GctlSetVisEnab(xxo_tctagliaf, True)
        Else
          xxo_tctagliaf.Visible = False
        End If
      End If

      If NTSCInt(oCleGsol.dttET.Rows(0)!et_valuta) <> 0 Then
        GctlSetVisEnab(ec_prezvalc, True)
        ec_prezzo.Enabled = False
      Else
        ec_prezvalc.Visible = False
        GctlSetVisEnab(ec_prezzo, True)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMORIMP1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try

  End Sub

  Public Overridable Sub FRMORIMP1_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    'salvo l'impostazione della griglia. devo farlo anche al cambio del tipo documento (ad esempio in bsveboll
    Try
      dcImpe.Dispose()
      oCleGsol.dsImpe.Dispose()
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
        oApp.MsgBoxErr(oApp.Tr(Me, 129048537213333984, "Posizionarsi prima nella griglia e selezionare la riga da cancellare"))
        Return
      End If
      Me.Cursor = Cursors.WaitCursor
      If Not oCleGsol.CorpoImpTestPreCancella(dcImpe.Position) Then Return
      If Not grvRighe.NTSDeleteRigaCorrente(dcImpe, True, dtrDeleted) Then Return
      If Not oCleGsol.CorpoImpRecordSalva(dcImpe.Position, True, dtrDeleted) Then Return

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
      If Not grvRighe.NTSRipristinaRigaCorrenteBefore(dcImpe, True) Then Return
      oCleGsol.CorpoImpRipristina(dcImpe.Position, dcImpe.Filter)
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
    Dim ctrlTmp As Control = Nothing
    Dim oParam As New CLE__PATB

    Try
      '------------------------------------
      'zoom standard
      ctrlTmp = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      If ctrlTmp.GetType.ToString.IndexOf("NTSGrid") > -1 Then
        SetFastZoom(NTSCStr(grvRighe.EditingValue), oParam)    'abilito la gestione dello zoom veloce

        If grvRighe.FocusedColumn.Equals(ec_codart) Then
          '------------------------------------
          'colonna particolare: codice articolo
          NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
          oParam.nMagaz = 0
          oParam.nListino = 0
          oParam.lContoCF = oCleGsol.lContoCF
          oParam.bTipoProposto = False       'abilito la possibilità di selezionare + articoli
          NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oParam)
          If NTSZOOM.strIn = "*" Then
            'zoom multiselezione
            If Not oParam.oParam Is Nothing Then
              If CType(oParam.oParam, DataTable).Rows.Count > 0 Then
                'il primo articolo selezionato lo metto nella riga su cui sono, gli altri li accodo
                If NTSCStr(CType(oParam.oParam, DataTable).Rows(0)!codart) <> NTSCStr(grvRighe.GetFocusedValue) Then
                  grvRighe.SetFocusedValue(NTSCStr(CType(oParam.oParam, DataTable).Rows(0)!codart))
                End If
                CType(oParam.oParam, DataTable).Rows(0).Delete()
                CType(oParam.oParam, DataTable).AcceptChanges()
                If CType(oParam.oParam, DataTable).Rows.Count > 0 Then
                  oCleGsol.bInInsertArticoDaZoom = True
                  If Not oCleGsol.CorpoImpRecordSalva(dcImpe.Position, False, Nothing) Then Return
                  For Each dtrT As DataRow In CType(oParam.oParam, DataTable).Rows
                    oCleGsol.dsImpe.Tables("CORPOIMP").Rows.Add(oCleGsol.dsImpe.Tables("CORPOIMP").NewRow)
                    With oCleGsol.dsImpe.Tables("CORPOIMP").Rows(oCleGsol.dsImpe.Tables("CORPOIMP").Rows.Count - 1)
                      'forzo la MovordOnAddNewRow
                      !codditt = "."
                      !codditt = DittaCorrente
                      !ec_codart = dtrT!codart.ToString
                    End With
                    If Not oCleGsol.CorpoImpRecordSalva(oCleGsol.dsImpe.Tables("CORPOIMP").Rows.Count - 1, False, Nothing) Then
                      oCleGsol.dsImpe.Tables("CORPOIMP").Rows(oCleGsol.dsImpe.Tables("CORPOIMP").Rows.Count - 1).Delete()
                    End If
                  Next
                  oCleGsol.bInInsertArticoDaZoom = False
                End If
                CType(oParam.oParam, DataTable).Clear()
              End If
            End If
          Else
            If NTSZOOM.strIn <> NTSCStr(grvRighe.EditingValue) Then grvRighe.SetFocusedValue(NTSZOOM.strIn)
          End If

        ElseIf grvRighe.FocusedColumn.Equals(ec_fase) Then
          '------------------------------------
          'zoom fasi articoli
          NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
          oParam.strTipo = NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart))
          NTSZOOM.ZoomStrIn("ZOOMARTFASI", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvRighe.EditingValue) Then grvRighe.SetFocusedValue(NTSZOOM.strIn)

        ElseIf grvRighe.FocusedColumn.Equals(ec_subcommeca) Then
          '------------------------------------
          'zoom subcommessa
          If NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_commeca)) = 0 Then
            oApp.MsgBoxInfo(oApp.Tr(Me, 129048537301930569, "Indicare prima il codice commessa"))
            Return
          End If
          NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
          oParam.lCommessa = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_commeca))   'passo il codice della commessa
          NTSZOOM.ZoomStrIn("ZOOMSUBCOMM", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvRighe.EditingValue) Then grvRighe.SetFocusedValue(NTSZOOM.strIn)

        ElseIf grvRighe.FocusedColumn.Equals(xxo_lottox) Then
          '------------------------------------
          'zoom lotti
          If NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart)).Trim = "" Then
            oApp.MsgBoxInfo(oApp.Tr(Me, 128786514436452000, "Indicare prima il codice articolo"))
            Return
          End If
          NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
          oParam.strTipo = NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart))
          'oParam.nMagaz = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_magaz))   'serve per visual solo i lotti aperti
          oParam.nAnno = NTSCInt(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_fase))     'serve per visual solo i lotti aperti
          'oParam.strDatreg = NTSCDate(edet_datdoc.Text).ToShortDateString                          'serve per visual solo i lotti aperti
          NTSZOOM.ZoomStrIn("ZOOMANALOTTI", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvRighe.EditingValue) Then grvRighe.SetFocusedValue(NTSZOOM.strIn)

        ElseIf grvRighe.FocusedColumn.Equals(ec_codclie) Then
          '------------------------------------
          'zoom cliente di griglia
          NTSZOOM.strIn = NTSCStr(grvRighe.EditingValue)
          oParam.bVisGriglia = True
          oParam.bTipoProposto = True
          oParam.nMastro = 0
          oParam.strTipo = "C"
          NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvRighe.EditingValue) Then grvRighe.SetFocusedValue(NTSZOOM.strIn)

        ElseIf grvRighe.FocusedColumn.Equals(ec_fase) Then
          '------------------------------------
          'zoom Project management task ID
          If oCleGsol.bModPM Then
            'da fare
          End If
        Else
          '------------------------------------
          'zoom standard di textbox e griglia
          NTSCallStandardZoom()
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbNavigazMrp_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNavigazMrp.ItemClick
    Dim strParam As String = ""
    Try
      Select Case oCleGsol.dttET.Rows(0)!et_tipork.ToString
        Case "$", "O", "H", "X", "R", "#", "V"
          'posso continuare
        Case Else
          oApp.MsgBoxInfo(oApp.Tr(Me, 128554237518494937, "Funzione non disponibile per questo tipo di documento"))
      End Select

      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128554245705966937, "Per poter effettuare l'operazione richiesta è necessario " & vbCrLf & _
                                                        "posizionarsi su di una riga nel CORPOIMP di un ordine/impegno."))
        Return
      End If

      'strParam = "O;" & oCleGsol.dttET.Rows(0)!et_tipork.ToString & ";" & _
      '          frmParent.edAnnoDoc.Text & ";" & _
      '          frmParent.edSerieDoc.Text.PadRight(1).Substring(0, 1) & ";" & _
      '          Microsoft.VisualBasic.Right(NTSCInt(frmParent.edNumDoc.Text).ToString.PadLeft(9, "0"c), 9) & ";" & _
      '          Microsoft.VisualBasic.Right(NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_riga).ToString.PadLeft(9, "0"c), 9) & ";" & _
      '          grvRighe.NTSGetCurrentDataRow!ec_codart.ToString.PadRight(18).Substring(0, 18) & ";" & _
      '          Microsoft.VisualBasic.Right(NTSCInt(grvRighe.NTSGetCurrentDataRow!ec_fase).ToString.PadLeft(4, "0"c), 4)
      oMenu.RunChild("BSDBNMRP", "CLSDBNMRP", oApp.Tr(Me, 129048537384901974, "Navigazione MRP"), DittaCorrente, "", "", Nothing, strParam, True, True)


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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
      If oCleGsol.dsImpe Is Nothing Then Return
      If oCleGsol.dsImpe.Tables("CORPOIMP") Is Nothing Then Return
      If oCleGsol.dsImpe.Tables("CORPOIMP").Rows.Count = 0 Then Return
      If grvRighe.GetFocusedRowCellValue(ec_riga) Is Nothing Then Return

      '-------------------------------------------------------
      'se sono su una nuova riga mi posiziono a sinistra
      If grvRighe.GetFocusedRowCellValue(ec_riga).ToString = "" And grvRighe.NTSGetCurrentDataRow Is Nothing Then
        grvRighe.LeftCoord = 0
        If ec_matric.Visible Then
          grvRighe.FocusedColumn = ec_matric
        Else
          grvRighe.FocusedColumn = ec_codart
        End If
        'For n = grvRighe.FocusedColumn.VisibleIndex To 1 Step -1
        '  SendKeys.Send("{LEFT}")
        'Next
        'If ec_riga.Visible Then SendKeys.Send("{RIGHT}")
      End If    'If grvRighe.GetFocusedRowCellValue(ec_riga").FormattedValue.ToString = "" Then

      If grvRighe.GetFocusedRowCellValue(ec_codart).ToString.Trim = "" Then Return

      '-------------------------------------------------
      GrvRighe_RowColChange()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvRighe_NTSFocusedColumnChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles grvRighe.NTSFocusedColumnChanged
    Try
      GrvRighe_RowColChange()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function GrvRighe_RowColChange() As Boolean
    Try

      If oCleGsol Is Nothing Then Return True
      If dttUm Is Nothing Then Return True

      GctlSetVisEnab(ec_tctaglia, False)

      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        If Not ec_unmis.ColumnEdit Is Nothing Then CType(ec_unmis.ColumnEdit, NTSRepositoryItemComboBox).DataSource = dttUm
        ec_unmis.Enabled = False
      Else
        GctlSetVisEnab(ec_unmis, False)
      End If

      '--------------------------------------
      'compilo il combo delle unità di misura
      If Not ec_unmis.ColumnEdit Is Nothing Then
        CType(ec_unmis.ColumnEdit, NTSRepositoryItemComboBox).DataSource = dttUm
        If grvRighe.FocusedColumn.Name = "ec_unmis" And grvRighe.GetFocusedRowCellValue(ec_codart).ToString <> "" And NTSCStr(grvRighe.GetFocusedRowCellValue(ec_codart)).Trim <> "" Then
          grvRighe.liListCmb.Visible = False    'lo nascondo, visto che contiene tutte le unita di misura del db ...
          ec_unmis.NTSComboValueOk = oCleGsol.GetArticoUnMis(NTSCStr(grvRighe.GetFocusedRowCellValue(ec_codart)))
          'sarà BN__CHIL ha gestire ec_unmis.NTSComboValueOk
        End If
      End If    'If Not ec_unmis.ColumnEdit Is Nothing Then

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub xxo_lottox_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles xxo_lottox.NTSZoomGest
    Dim oCZoo As New CLE__CZOO
    Dim bNuovo As Boolean = True
    Dim oTmp As New Control
    Dim oPar As New CLE__CLDP
    Try
      Me.ValidaLastControl()

      If grvRighe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130420273044849208, "Posizionarsi prima su una riga con codice articolo impostato"))
        Return
      End If
      If NTSCStr(grvRighe.GetRowCellValue(grvRighe.FocusedRowHandle, ec_codart)).Trim = "" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130420277434877304, "Indicare prima il codice articolo"))
        Return
      End If

      If e.TipoEvento = "OPEN" Then
        bNuovo = False
        oPar.strParam = "APRI;" & NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart).PadRight(CLN__STD.CodartMaxLen) & ";" & NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_lotto).PadLeft(9, "0"c) & ";"
      Else
        oPar.strParam = "NUOV;" & NTSCStr(grvRighe.NTSGetCurrentDataRow!ec_codart).PadRight(CLN__STD.CodartMaxLen) & ";" & "000000000" & ";"
      End If

      oTmp.Text = NTSCStr(grvRighe.NTSGetCurrentDataRow!xxo_lottox)
      NTSZOOM.OpenChildGest(oTmp, "ZOOMANALOTTI", DittaCorrente, bNuovo, oPar)

      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo
      grRighe.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

  Public Overridable Function Salva() As Boolean
    Try
      If Not oCleGsol.CorpoImpRecordSalva(dcImpe.Position, False, Nothing) Then Return False
      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

End Class
