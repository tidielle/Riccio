Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__STAT

  Public oCleStat As CLE__STAT
  Public oCallParams As CLE__CLDP
  Public dsStat As DataSet
  Public dcStat As BindingSource = New BindingSource()

  Private components As System.ComponentModel.IContainer
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
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents grStat As NTSInformatica.NTSGrid
  Public WithEvents grvStat As NTSInformatica.NTSGridView
  Public WithEvents tb_codstat As NTSInformatica.NTSGridColumn
  Public WithEvents tb_desstat As NTSInformatica.NTSGridColumn
  Public WithEvents tb_siglacee As NTSInformatica.NTSGridColumn
  Public WithEvents tb_cod770 As NTSInformatica.NTSGridColumn
  Public WithEvents tb_codvstat As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codvstat As NTSInformatica.NTSGridColumn
  Public WithEvents tb_ue As NTSInformatica.NTSGridColumn
  Public WithEvents tb_siglaiso As NTSInformatica.NTSGridColumn
  Public WithEvents tb_codpercf As NTSInformatica.NTSGridColumn
  Public WithEvents tb_codnaz As NTSInformatica.NTSGridColumn
  Public WithEvents tb_soppresso As NTSInformatica.NTSGridColumn
  Public WithEvents tb_datini As NTSInformatica.NTSGridColumn
  Public WithEvents tb_datfin As NTSInformatica.NTSGridColumn
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__STAT))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarMenuItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grStat = New NTSInformatica.NTSGrid
    Me.grvStat = New NTSInformatica.NTSGridView
    Me.tb_codstat = New NTSInformatica.NTSGridColumn
    Me.tb_desstat = New NTSInformatica.NTSGridColumn
    Me.tb_siglacee = New NTSInformatica.NTSGridColumn
    Me.tb_cod770 = New NTSInformatica.NTSGridColumn
    Me.tb_codvstat = New NTSInformatica.NTSGridColumn
    Me.xx_codvstat = New NTSInformatica.NTSGridColumn
    Me.tb_ue = New NTSInformatica.NTSGridColumn
    Me.tb_siglaiso = New NTSInformatica.NTSGridColumn
    Me.tb_codpercf = New NTSInformatica.NTSGridColumn
    Me.tb_codnaz = New NTSInformatica.NTSGridColumn
    Me.tb_soppresso = New NTSInformatica.NTSGridColumn
    Me.tb_datini = New NTSInformatica.NTSGridColumn
    Me.tb_datfin = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grStat, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvStat, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    Me.frmPopup.ClientSize = New System.Drawing.Size(30, 56)
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 11
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
    Me.tlbStrumenti.Id = 5
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta stampante"
    Me.tlbImpostaStampante.Id = 6
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.NTSIsCheckBox = False
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 7
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa Video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 8
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
    'grStat
    '
    Me.grStat.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grStat.EmbeddedNavigator.Name = ""
    Me.grStat.Location = New System.Drawing.Point(0, 26)
    Me.grStat.MainView = Me.grvStat
    Me.grStat.Name = "grStat"
    Me.grStat.Size = New System.Drawing.Size(500, 240)
    Me.grStat.TabIndex = 4
    Me.grStat.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvStat})
    '
    'grvStat
    '
    Me.grvStat.ActiveFilterEnabled = False
    Me.grvStat.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tb_codstat, Me.tb_desstat, Me.tb_siglacee, Me.tb_cod770, Me.tb_codvstat, Me.xx_codvstat, Me.tb_ue, Me.tb_siglaiso, Me.tb_codpercf, Me.tb_codnaz, Me.tb_soppresso, Me.tb_datini, Me.tb_datfin})
    Me.grvStat.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvStat.Enabled = True
    Me.grvStat.GridControl = Me.grStat
    Me.grvStat.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvStat.Name = "grvStat"
    Me.grvStat.NTSAllowDelete = True
    Me.grvStat.NTSAllowInsert = True
    Me.grvStat.NTSAllowUpdate = True
    Me.grvStat.NTSMenuContext = Nothing
    Me.grvStat.OptionsCustomization.AllowRowSizing = True
    Me.grvStat.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvStat.OptionsNavigation.UseTabKey = False
    Me.grvStat.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvStat.OptionsView.ColumnAutoWidth = False
    Me.grvStat.OptionsView.EnableAppearanceEvenRow = True
    Me.grvStat.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvStat.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvStat.OptionsView.ShowGroupPanel = False
    Me.grvStat.RowHeight = 16
    '
    'tb_codstat
    '
    Me.tb_codstat.AppearanceCell.Options.UseBackColor = True
    Me.tb_codstat.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codstat.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codstat.Caption = "Sigla Stato"
    Me.tb_codstat.Enabled = True
    Me.tb_codstat.FieldName = "tb_codstat"
    Me.tb_codstat.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codstat.Name = "tb_codstat"
    Me.tb_codstat.NTSRepositoryComboBox = Nothing
    Me.tb_codstat.NTSRepositoryItemCheck = Nothing
    Me.tb_codstat.NTSRepositoryItemMemo = Nothing
    Me.tb_codstat.NTSRepositoryItemText = Nothing
    Me.tb_codstat.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codstat.OptionsFilter.AllowFilter = False
    Me.tb_codstat.Visible = True
    Me.tb_codstat.VisibleIndex = 0
    '
    'tb_desstat
    '
    Me.tb_desstat.AppearanceCell.Options.UseBackColor = True
    Me.tb_desstat.AppearanceCell.Options.UseTextOptions = True
    Me.tb_desstat.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_desstat.Caption = "Descrizione"
    Me.tb_desstat.Enabled = True
    Me.tb_desstat.FieldName = "tb_desstat"
    Me.tb_desstat.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_desstat.Name = "tb_desstat"
    Me.tb_desstat.NTSRepositoryComboBox = Nothing
    Me.tb_desstat.NTSRepositoryItemCheck = Nothing
    Me.tb_desstat.NTSRepositoryItemMemo = Nothing
    Me.tb_desstat.NTSRepositoryItemText = Nothing
    Me.tb_desstat.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_desstat.OptionsFilter.AllowFilter = False
    Me.tb_desstat.Visible = True
    Me.tb_desstat.VisibleIndex = 1
    '
    'tb_siglacee
    '
    Me.tb_siglacee.AppearanceCell.Options.UseBackColor = True
    Me.tb_siglacee.AppearanceCell.Options.UseTextOptions = True
    Me.tb_siglacee.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_siglacee.Caption = "Sigla CEE"
    Me.tb_siglacee.Enabled = True
    Me.tb_siglacee.FieldName = "tb_siglacee"
    Me.tb_siglacee.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_siglacee.Name = "tb_siglacee"
    Me.tb_siglacee.NTSRepositoryComboBox = Nothing
    Me.tb_siglacee.NTSRepositoryItemCheck = Nothing
    Me.tb_siglacee.NTSRepositoryItemMemo = Nothing
    Me.tb_siglacee.NTSRepositoryItemText = Nothing
    Me.tb_siglacee.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_siglacee.OptionsFilter.AllowFilter = False
    Me.tb_siglacee.Visible = True
    Me.tb_siglacee.VisibleIndex = 2
    '
    'tb_cod770
    '
    Me.tb_cod770.AppearanceCell.Options.UseBackColor = True
    Me.tb_cod770.AppearanceCell.Options.UseTextOptions = True
    Me.tb_cod770.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_cod770.Caption = "Cod. stato"
    Me.tb_cod770.Enabled = True
    Me.tb_cod770.FieldName = "tb_cod770"
    Me.tb_cod770.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_cod770.Name = "tb_cod770"
    Me.tb_cod770.NTSRepositoryComboBox = Nothing
    Me.tb_cod770.NTSRepositoryItemCheck = Nothing
    Me.tb_cod770.NTSRepositoryItemMemo = Nothing
    Me.tb_cod770.NTSRepositoryItemText = Nothing
    Me.tb_cod770.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_cod770.OptionsFilter.AllowFilter = False
    Me.tb_cod770.Visible = True
    Me.tb_cod770.VisibleIndex = 3
    '
    'tb_codvstat
    '
    Me.tb_codvstat.AppearanceCell.Options.UseBackColor = True
    Me.tb_codvstat.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codvstat.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codvstat.Caption = "Cod. valuta corr."
    Me.tb_codvstat.Enabled = True
    Me.tb_codvstat.FieldName = "tb_codvstat"
    Me.tb_codvstat.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codvstat.Name = "tb_codvstat"
    Me.tb_codvstat.NTSRepositoryComboBox = Nothing
    Me.tb_codvstat.NTSRepositoryItemCheck = Nothing
    Me.tb_codvstat.NTSRepositoryItemMemo = Nothing
    Me.tb_codvstat.NTSRepositoryItemText = Nothing
    Me.tb_codvstat.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codvstat.OptionsFilter.AllowFilter = False
    Me.tb_codvstat.Visible = True
    Me.tb_codvstat.VisibleIndex = 4
    '
    'xx_codvstat
    '
    Me.xx_codvstat.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.xx_codvstat.AppearanceCell.Options.UseBackColor = True
    Me.xx_codvstat.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codvstat.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codvstat.Caption = "Descr. valuta"
    Me.xx_codvstat.Enabled = False
    Me.xx_codvstat.FieldName = "xx_codvstat"
    Me.xx_codvstat.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codvstat.Name = "xx_codvstat"
    Me.xx_codvstat.NTSRepositoryComboBox = Nothing
    Me.xx_codvstat.NTSRepositoryItemCheck = Nothing
    Me.xx_codvstat.NTSRepositoryItemMemo = Nothing
    Me.xx_codvstat.NTSRepositoryItemText = Nothing
    Me.xx_codvstat.OptionsColumn.AllowEdit = False
    Me.xx_codvstat.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codvstat.OptionsColumn.ReadOnly = True
    Me.xx_codvstat.OptionsFilter.AllowFilter = False
    Me.xx_codvstat.Visible = True
    Me.xx_codvstat.VisibleIndex = 5
    '
    'tb_ue
    '
    Me.tb_ue.AppearanceCell.Options.UseBackColor = True
    Me.tb_ue.AppearanceCell.Options.UseTextOptions = True
    Me.tb_ue.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_ue.Caption = "Stato della UE"
    Me.tb_ue.Enabled = True
    Me.tb_ue.FieldName = "tb_ue"
    Me.tb_ue.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_ue.Name = "tb_ue"
    Me.tb_ue.NTSRepositoryComboBox = Nothing
    Me.tb_ue.NTSRepositoryItemCheck = Nothing
    Me.tb_ue.NTSRepositoryItemMemo = Nothing
    Me.tb_ue.NTSRepositoryItemText = Nothing
    Me.tb_ue.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_ue.OptionsFilter.AllowFilter = False
    Me.tb_ue.Visible = True
    Me.tb_ue.VisibleIndex = 6
    '
    'tb_siglaiso
    '
    Me.tb_siglaiso.AppearanceCell.Options.UseBackColor = True
    Me.tb_siglaiso.AppearanceCell.Options.UseTextOptions = True
    Me.tb_siglaiso.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_siglaiso.Caption = "Sigla ISO"
    Me.tb_siglaiso.Enabled = True
    Me.tb_siglaiso.FieldName = "tb_siglaiso"
    Me.tb_siglaiso.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_siglaiso.Name = "tb_siglaiso"
    Me.tb_siglaiso.NTSRepositoryComboBox = Nothing
    Me.tb_siglaiso.NTSRepositoryItemCheck = Nothing
    Me.tb_siglaiso.NTSRepositoryItemMemo = Nothing
    Me.tb_siglaiso.NTSRepositoryItemText = Nothing
    Me.tb_siglaiso.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_siglaiso.OptionsFilter.AllowFilter = False
    Me.tb_siglaiso.Visible = True
    Me.tb_siglaiso.VisibleIndex = 7
    '
    'tb_codpercf
    '
    Me.tb_codpercf.AppearanceCell.Options.UseBackColor = True
    Me.tb_codpercf.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codpercf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codpercf.Caption = "Cod.Alf.Cod.Fis."
    Me.tb_codpercf.Enabled = True
    Me.tb_codpercf.FieldName = "tb_codpercf"
    Me.tb_codpercf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codpercf.Name = "tb_codpercf"
    Me.tb_codpercf.NTSRepositoryComboBox = Nothing
    Me.tb_codpercf.NTSRepositoryItemCheck = Nothing
    Me.tb_codpercf.NTSRepositoryItemMemo = Nothing
    Me.tb_codpercf.NTSRepositoryItemText = Nothing
    Me.tb_codpercf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codpercf.OptionsFilter.AllowFilter = False
    Me.tb_codpercf.Visible = True
    Me.tb_codpercf.VisibleIndex = 8
    '
    'tb_codnaz
    '
    Me.tb_codnaz.AppearanceCell.Options.UseBackColor = True
    Me.tb_codnaz.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codnaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codnaz.Caption = "Cod.nazionalità"
    Me.tb_codnaz.Enabled = True
    Me.tb_codnaz.FieldName = "tb_codnaz"
    Me.tb_codnaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codnaz.Name = "tb_codnaz"
    Me.tb_codnaz.NTSRepositoryComboBox = Nothing
    Me.tb_codnaz.NTSRepositoryItemCheck = Nothing
    Me.tb_codnaz.NTSRepositoryItemMemo = Nothing
    Me.tb_codnaz.NTSRepositoryItemText = Nothing
    Me.tb_codnaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codnaz.OptionsFilter.AllowFilter = False
    Me.tb_codnaz.Visible = True
    Me.tb_codnaz.VisibleIndex = 9
    '
    'tb_soppresso
    '
    Me.tb_soppresso.AppearanceCell.Options.UseBackColor = True
    Me.tb_soppresso.AppearanceCell.Options.UseTextOptions = True
    Me.tb_soppresso.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_soppresso.Caption = "Soppresso"
    Me.tb_soppresso.Enabled = True
    Me.tb_soppresso.FieldName = "tb_soppresso"
    Me.tb_soppresso.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_soppresso.Name = "tb_soppresso"
    Me.tb_soppresso.NTSRepositoryComboBox = Nothing
    Me.tb_soppresso.NTSRepositoryItemCheck = Nothing
    Me.tb_soppresso.NTSRepositoryItemMemo = Nothing
    Me.tb_soppresso.NTSRepositoryItemText = Nothing
    Me.tb_soppresso.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_soppresso.OptionsFilter.AllowFilter = False
    Me.tb_soppresso.Visible = True
    Me.tb_soppresso.VisibleIndex = 10
    '
    'tb_datini
    '
    Me.tb_datini.AppearanceCell.Options.UseBackColor = True
    Me.tb_datini.AppearanceCell.Options.UseTextOptions = True
    Me.tb_datini.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_datini.Caption = "Data Istituzione"
    Me.tb_datini.Enabled = True
    Me.tb_datini.FieldName = "tb_datini"
    Me.tb_datini.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_datini.Name = "tb_datini"
    Me.tb_datini.NTSRepositoryComboBox = Nothing
    Me.tb_datini.NTSRepositoryItemCheck = Nothing
    Me.tb_datini.NTSRepositoryItemMemo = Nothing
    Me.tb_datini.NTSRepositoryItemText = Nothing
    Me.tb_datini.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_datini.OptionsFilter.AllowFilter = False
    Me.tb_datini.Visible = True
    Me.tb_datini.VisibleIndex = 11
    '
    'tb_datfin
    '
    Me.tb_datfin.AppearanceCell.Options.UseBackColor = True
    Me.tb_datfin.AppearanceCell.Options.UseTextOptions = True
    Me.tb_datfin.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_datfin.Caption = "Data Soppressione"
    Me.tb_datfin.Enabled = True
    Me.tb_datfin.FieldName = "tb_datfin"
    Me.tb_datfin.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_datfin.Name = "tb_datfin"
    Me.tb_datfin.NTSRepositoryComboBox = Nothing
    Me.tb_datfin.NTSRepositoryItemCheck = Nothing
    Me.tb_datfin.NTSRepositoryItemMemo = Nothing
    Me.tb_datfin.NTSRepositoryItemText = Nothing
    Me.tb_datfin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_datfin.OptionsFilter.AllowFilter = False
    Me.tb_datfin.Visible = True
    Me.tb_datfin.VisibleIndex = 12
    '
    'FRM__STAT
    '
    Me.ClientSize = New System.Drawing.Size(500, 266)
    Me.Controls.Add(Me.grStat)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.HelpButton = True
    Me.Name = "FRM__STAT"
    Me.NTSLastControlFocussed = Me.grStat
    Me.Text = "SIGLE STATI ESTERI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grStat, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvStat, System.ComponentModel.ISupportInitialize).EndInit()
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

    '------------------------------------------------
    'creo e attivo l'entity e inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__STAT", "BE__STAT", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128178267964062500, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleStat = CType(oTmp, CLE__STAT)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__STAT", strRemoteServer, strRemotePort)
    AddHandler oCleStat.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleStat.Init(oApp, oScript, oMenu.oCleComm, "TABSTAT", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Sub InitControls()
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
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      Dim dttTipo As New DataTable()

      grvStat.NTSSetParam(oMenu, oApp.Tr(Me, 128230023868474289, "Griglia Sigle Stati Esteri"))
      tb_codstat.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128230023868630462, "Sigla Stato"), CLN__STD.tabstat, False)
      tb_desstat.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023868786635, "Descrizione"), 20, True)
      tb_siglacee.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023868942808, "Sigla CEE"), 3, True)
      tb_cod770.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023869098981, "Cod. stato"), "0", 4, 0, 9999)
      tb_codvstat.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023869255154, "Cod. valuta corr."), CLN__STD.tabvalu)
      xx_codvstat.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023869411327, "Descr. valuta"), 10)
      tb_ue.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023869567500, "Stato della UE"), "S", "N")
      tb_siglaiso.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023869723673, "Sigla ISO"), 3, True)
      tb_codpercf.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023869879846, "Cod.Alf.Cod.Fis."), 4, True)
      tb_codnaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023870036019, "Cod.nazionalità"), 3, True)
      tb_soppresso.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023870192192, "Soppresso"), "S", "N")
      tb_datini.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128230023870348365, "Data Istituzione"), True)
      tb_datfin.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128230023870504538, "Data Soppressione"), True)

      tb_codstat.NTSSetParamZoom("")     'in questo modo non faccio apparire lo zoom a fianco del codice stato (lo zoom è stato abilitato da NTSSetParamTabe)
      tb_codstat.NTSSetRichiesto()
      tb_desstat.NTSSetRichiesto()

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

  Public Overridable Sub FRM__STAT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleStat.Apri(DittaCorrente, dsStat) Then Me.Close()
      dcStat.DataSource = dsStat.Tables("TABSTAT")
      dsStat.AcceptChanges()

      grStat.DataSource = dcStat

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      'e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipo documento, ecc ...
      '(Ex: CTRL+ALT+F4)
      'GctlTipoDoc = ""
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione Friendly nasconde, sempre, alcune colonne
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = True Then
        tb_codvstat.Visible = False
        xx_codvstat.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__STAT_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Dim i As Integer
    Try
      '--------------------------------------------
      'sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
      If Not oCallParams Is Nothing Then
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
          If grvStat.NTSAllowInsert Then
            grvStat.NTSNuovo()
          End If
        ElseIf Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) <> "" Then
          Dim strParam As String = Microsoft.VisualBasic.Mid(oCallParams.strParam, 6)
          For i = 0 To dcStat.List.Count - 1
            If CType(dcStat.Item(i), DataRowView)!tb_codstat.ToString = strParam Then
              dcStat.Position = i
              Exit For
            End If
          Next
        End If
      End If  'If Not oCallParams Is Nothing Then
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__STAT_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcStat.Dispose()
      dsStat.Dispose()
    Catch
    End Try
  End Sub

  Public Overridable Sub FRM__STAT_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvStat.NTSSalvaRigaCorrente(dcStat, oCleStat.RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleStat.Salva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleStat.Ripristina(dcStat.Position, dcStat.Filter)
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

  Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer

    Try
      '--------------------------------------------------
      'preparo il motore di stampa
      strCrpe = ""
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BS--STAT", "Reports1", " ", 0, nDestin, "BS--STAT.RPT", False, "Sigle Stati Esteri", False)
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

#Region "Eventi toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvStat.NTSNuovo()
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
    Try
      If Not grvStat.NTSDeleteRigaCorrente(dcStat, True) Then Return
      oCleStat.Salva(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvStat.NTSRipristinaRigaCorrenteBefore(dcStat, True) Then Return
      oCleStat.Ripristina(dcStat.Position, dcStat.Filter)
      grvStat.NTSRipristinaRigaCorrenteAfter()
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

    Dim oParam As New CLE__PATB
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing

    Try
      'zoom standard di textbox e griglia
      NTSCallStandardZoom()
    Catch ex As Exception
      '-------------------------------------------------
      strErr = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    Try
      oMenu.ReportImposta(Me)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      If Not Salva() Then Return
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      If Not Salva() Then Return
      Stampa(0)
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

  Public Overridable Sub grvStat_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvStat.NTSBeforeRowUpdate
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

  Public Overridable Sub grvStat_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvStat.NTSFocusedRowChanged
    Try
      '-------------------------------------------------
      'se il codice stato è diverso da 0 blocco la colonna, diversamente la rendo editabile
      If NTSCStr(grvStat.GetFocusedRowCellValue(tb_codstat).ToString.Trim) <> "" Then
        tb_codstat.Enabled = False
      Else
        GctlSetVisEnab(tb_codstat, False)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  
End Class
