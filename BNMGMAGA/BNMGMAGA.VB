Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGMAGA
  Private Moduli_P As Integer = CLN__STD.bsModMG + CLN__STD.bsModVE
  Private ModuliExt_P As Integer = CLN__STD.bsModExtMGE
  Private ModuliSup_P As Integer = 0
  Private ModuliSupExt_P As Integer = 0
  Private ModuliPtn_P As Integer = 0
  Private ModuliPtnExt_P As Integer = 0

  Public ReadOnly Property Moduli() As Integer
    Get
      Return Moduli_P
    End Get
  End Property
  Public ReadOnly Property ModuliExt() As Integer
    Get
      Return ModuliExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliSup() As Integer
    Get
      Return ModuliSup_P
    End Get
  End Property
  Public ReadOnly Property ModuliSupExt() As Integer
    Get
      Return ModuliSupExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtn() As Integer
    Get
      Return ModuliPtn_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtnExt() As Integer
    Get
      Return ModuliPtnExt_P
    End Get
  End Property


  Public oCleMaga As CLEMGMAGA
  Public oCallParams As CLE__CLDP
  Public dsMaga As DataSet
  Public dcMaga As BindingSource = New BindingSource()

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents grMaga As NTSInformatica.NTSGrid
  Public WithEvents grvMaga As NTSInformatica.NTSGridView
  Public WithEvents tb_codmaga As NTSInformatica.NTSGridColumn
  Public WithEvents tb_desmaga As NTSInformatica.NTSGridColumn
  Public WithEvents tb_flclavo As NTSInformatica.NTSGridColumn
  Public WithEvents tb_magconto As NTSInformatica.NTSGridColumn
  Public WithEvents tb_appscmin As NTSInformatica.NTSGridColumn
  Public WithEvents RepositoryItemComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
  Public WithEvents RepositoryItemButtonEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
  Public WithEvents xx_magconto As NTSInformatica.NTSGridColumn
  Public WithEvents tb_datacs As NTSInformatica.NTSGridColumn
  Public WithEvents tb_consmrp As NTSInformatica.NTSGridColumn
  Public WithEvents tb_magass As NTSInformatica.NTSGridColumn
  Public WithEvents xx_magass As NTSInformatica.NTSGridColumn
  Public WithEvents tb_gescl As NTSInformatica.NTSGridColumn
  Public WithEvents tb_coddest As NTSInformatica.NTSGridColumn
  Public WithEvents xx_coddest As NTSInformatica.NTSGridColumn
  Public WithEvents tb_applotti As NTSInformatica.NTSGridColumn
  Public WithEvents tb_codstab As NTSInformatica.NTSGridColumn
  Public WithEvents xx_desstab As NTSInformatica.NTSGridColumn
  Public WithEvents tb_gestubi As NTSInformatica.NTSGridColumn
  Public WithEvents tb_dubicaz As NTSInformatica.NTSGridColumn
  Public WithEvents tb_gestmat As NTSInformatica.NTSGridColumn
  Public WithEvents tb_dmatric As NTSInformatica.NTSGridColumn
  Public WithEvents tb_gestlot As NTSInformatica.NTSGridColumn
  Public WithEvents tb_dlottox As NTSInformatica.NTSGridColumn
  Public WithEvents tb_dubicst As NTSInformatica.NTSGridColumn
  Public WithEvents tb_dubicpr As NTSInformatica.NTSGridColumn
  Public WithEvents tb_dubicri As NTSInformatica.NTSGridColumn
  Public WithEvents tb_dubicus As NTSInformatica.NTSGridColumn
  Public WithEvents tb_progr As NTSInformatica.NTSGridColumn
  Public WithEvents tb_webvis As NTSInformatica.NTSGridColumn
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem


  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGMAGA))
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
    Me.grMaga = New NTSInformatica.NTSGrid
    Me.grvMaga = New NTSInformatica.NTSGridView
    Me.tb_codmaga = New NTSInformatica.NTSGridColumn
    Me.tb_desmaga = New NTSInformatica.NTSGridColumn
    Me.tb_flclavo = New NTSInformatica.NTSGridColumn
    Me.RepositoryItemComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Me.tb_magconto = New NTSInformatica.NTSGridColumn
    Me.RepositoryItemButtonEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Me.tb_appscmin = New NTSInformatica.NTSGridColumn
    Me.xx_magconto = New NTSInformatica.NTSGridColumn
    Me.tb_consmrp = New NTSInformatica.NTSGridColumn
    Me.tb_magass = New NTSInformatica.NTSGridColumn
    Me.xx_magass = New NTSInformatica.NTSGridColumn
    Me.tb_gescl = New NTSInformatica.NTSGridColumn
    Me.tb_coddest = New NTSInformatica.NTSGridColumn
    Me.xx_coddest = New NTSInformatica.NTSGridColumn
    Me.tb_applotti = New NTSInformatica.NTSGridColumn
    Me.tb_datacs = New NTSInformatica.NTSGridColumn
    Me.tb_progr = New NTSInformatica.NTSGridColumn
    Me.tb_codstab = New NTSInformatica.NTSGridColumn
    Me.xx_desstab = New NTSInformatica.NTSGridColumn
    Me.tb_gestubi = New NTSInformatica.NTSGridColumn
    Me.tb_dubicaz = New NTSInformatica.NTSGridColumn
    Me.tb_gestmat = New NTSInformatica.NTSGridColumn
    Me.tb_dmatric = New NTSInformatica.NTSGridColumn
    Me.tb_gestlot = New NTSInformatica.NTSGridColumn
    Me.tb_dlottox = New NTSInformatica.NTSGridColumn
    Me.tb_dubicst = New NTSInformatica.NTSGridColumn
    Me.tb_dubicpr = New NTSInformatica.NTSGridColumn
    Me.tb_dubicri = New NTSInformatica.NTSGridColumn
    Me.tb_dubicus = New NTSInformatica.NTSGridColumn
    Me.tb_webvis = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grMaga, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvMaga, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.RepositoryItemButtonEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.frmAuto.Appearance.BackColor = System.Drawing.SystemColors.GradientActiveCaption
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti, Me.tlbImpostaStampante})
    Me.NtsBarManager1.MaxItemId = 17
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
    Me.tlbZoom.Id = 13
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 15
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.Id = 16
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 4
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 5
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
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
    'grMaga
    '
    Me.grMaga.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grMaga.EmbeddedNavigator.Name = ""
    Me.grMaga.Location = New System.Drawing.Point(0, 26)
    Me.grMaga.MainView = Me.grvMaga
    Me.grMaga.Name = "grMaga"
    Me.grMaga.Size = New System.Drawing.Size(648, 416)
    Me.grMaga.TabIndex = 5
    Me.grMaga.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvMaga})
    '
    'grvMaga
    '
    Me.grvMaga.ActiveFilterEnabled = False
    Me.grvMaga.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tb_codmaga, Me.tb_desmaga, Me.tb_flclavo, Me.tb_magconto, Me.tb_appscmin, Me.xx_magconto, Me.tb_consmrp, Me.tb_magass, Me.xx_magass, Me.tb_gescl, Me.tb_coddest, Me.xx_coddest, Me.tb_applotti, Me.tb_datacs, Me.tb_progr, Me.tb_codstab, Me.xx_desstab, Me.tb_gestubi, Me.tb_dubicaz, Me.tb_gestmat, Me.tb_dmatric, Me.tb_gestlot, Me.tb_dlottox, Me.tb_dubicst, Me.tb_dubicpr, Me.tb_dubicri, Me.tb_dubicus, Me.tb_webvis})
    Me.grvMaga.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvMaga.Enabled = True
    Me.grvMaga.GridControl = Me.grMaga
    Me.grvMaga.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvMaga.MinRowHeight = 14
    Me.grvMaga.Name = "grvMaga"
    Me.grvMaga.NTSAllowDelete = True
    Me.grvMaga.NTSAllowInsert = True
    Me.grvMaga.NTSAllowUpdate = True
    Me.grvMaga.NTSMenuContext = Nothing
    Me.grvMaga.OptionsCustomization.AllowRowSizing = True
    Me.grvMaga.OptionsFilter.AllowFilterEditor = False
    Me.grvMaga.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvMaga.OptionsNavigation.UseTabKey = False
    Me.grvMaga.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvMaga.OptionsView.ColumnAutoWidth = False
    Me.grvMaga.OptionsView.EnableAppearanceEvenRow = True
    Me.grvMaga.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvMaga.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvMaga.OptionsView.ShowGroupPanel = False
    Me.grvMaga.RowHeight = 16
    '
    'tb_codmaga
    '
    Me.tb_codmaga.AppearanceCell.Options.UseBackColor = True
    Me.tb_codmaga.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codmaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codmaga.Caption = "Codice"
    Me.tb_codmaga.Enabled = True
    Me.tb_codmaga.FieldName = "tb_codmaga"
    Me.tb_codmaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codmaga.Name = "tb_codmaga"
    Me.tb_codmaga.NTSRepositoryComboBox = Nothing
    Me.tb_codmaga.NTSRepositoryItemCheck = Nothing
    Me.tb_codmaga.NTSRepositoryItemMemo = Nothing
    Me.tb_codmaga.NTSRepositoryItemText = Nothing
    Me.tb_codmaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codmaga.OptionsFilter.AllowFilter = False
    Me.tb_codmaga.Visible = True
    Me.tb_codmaga.VisibleIndex = 0
    Me.tb_codmaga.Width = 49
    '
    'tb_desmaga
    '
    Me.tb_desmaga.AppearanceCell.Options.UseBackColor = True
    Me.tb_desmaga.AppearanceCell.Options.UseTextOptions = True
    Me.tb_desmaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_desmaga.Caption = "Descrizione"
    Me.tb_desmaga.Enabled = True
    Me.tb_desmaga.FieldName = "tb_desmaga"
    Me.tb_desmaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_desmaga.Name = "tb_desmaga"
    Me.tb_desmaga.NTSRepositoryComboBox = Nothing
    Me.tb_desmaga.NTSRepositoryItemCheck = Nothing
    Me.tb_desmaga.NTSRepositoryItemMemo = Nothing
    Me.tb_desmaga.NTSRepositoryItemText = Nothing
    Me.tb_desmaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_desmaga.OptionsFilter.AllowFilter = False
    Me.tb_desmaga.Visible = True
    Me.tb_desmaga.VisibleIndex = 1
    Me.tb_desmaga.Width = 132
    '
    'tb_flclavo
    '
    Me.tb_flclavo.AppearanceCell.Options.UseBackColor = True
    Me.tb_flclavo.AppearanceCell.Options.UseTextOptions = True
    Me.tb_flclavo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_flclavo.Caption = "Tipo magazzino"
    Me.tb_flclavo.ColumnEdit = Me.RepositoryItemComboBox1
    Me.tb_flclavo.Enabled = True
    Me.tb_flclavo.FieldName = "tb_flclavo"
    Me.tb_flclavo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_flclavo.Name = "tb_flclavo"
    Me.tb_flclavo.NTSRepositoryComboBox = Nothing
    Me.tb_flclavo.NTSRepositoryItemCheck = Nothing
    Me.tb_flclavo.NTSRepositoryItemMemo = Nothing
    Me.tb_flclavo.NTSRepositoryItemText = Nothing
    Me.tb_flclavo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_flclavo.OptionsFilter.AllowFilter = False
    Me.tb_flclavo.Visible = True
    Me.tb_flclavo.VisibleIndex = 2
    Me.tb_flclavo.Width = 106
    '
    'RepositoryItemComboBox1
    '
    Me.RepositoryItemComboBox1.AutoHeight = False
    Me.RepositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.RepositoryItemComboBox1.Name = "RepositoryItemComboBox1"
    '
    'tb_magconto
    '
    Me.tb_magconto.AppearanceCell.Options.UseBackColor = True
    Me.tb_magconto.AppearanceCell.Options.UseTextOptions = True
    Me.tb_magconto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_magconto.Caption = "Conto C/F"
    Me.tb_magconto.ColumnEdit = Me.RepositoryItemButtonEdit1
    Me.tb_magconto.Enabled = True
    Me.tb_magconto.FieldName = "tb_magconto"
    Me.tb_magconto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_magconto.Name = "tb_magconto"
    Me.tb_magconto.NTSRepositoryComboBox = Nothing
    Me.tb_magconto.NTSRepositoryItemCheck = Nothing
    Me.tb_magconto.NTSRepositoryItemMemo = Nothing
    Me.tb_magconto.NTSRepositoryItemText = Nothing
    Me.tb_magconto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_magconto.OptionsFilter.AllowFilter = False
    Me.tb_magconto.Visible = True
    Me.tb_magconto.VisibleIndex = 3
    Me.tb_magconto.Width = 105
    '
    'RepositoryItemButtonEdit1
    '
    Me.RepositoryItemButtonEdit1.AutoHeight = False
    Me.RepositoryItemButtonEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.RepositoryItemButtonEdit1.Name = "RepositoryItemButtonEdit1"
    '
    'tb_appscmin
    '
    Me.tb_appscmin.AppearanceCell.Options.UseBackColor = True
    Me.tb_appscmin.AppearanceCell.Options.UseTextOptions = True
    Me.tb_appscmin.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_appscmin.Caption = "Appl. punto riord."
    Me.tb_appscmin.Enabled = True
    Me.tb_appscmin.FieldName = "tb_appscmin"
    Me.tb_appscmin.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_appscmin.Name = "tb_appscmin"
    Me.tb_appscmin.NTSRepositoryComboBox = Nothing
    Me.tb_appscmin.NTSRepositoryItemCheck = Nothing
    Me.tb_appscmin.NTSRepositoryItemMemo = Nothing
    Me.tb_appscmin.NTSRepositoryItemText = Nothing
    Me.tb_appscmin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_appscmin.OptionsFilter.AllowFilter = False
    Me.tb_appscmin.Visible = True
    Me.tb_appscmin.VisibleIndex = 5
    Me.tb_appscmin.Width = 169
    '
    'xx_magconto
    '
    Me.xx_magconto.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.xx_magconto.AppearanceCell.Options.UseBackColor = True
    Me.xx_magconto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_magconto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_magconto.Caption = "Denomin. conto"
    Me.xx_magconto.Enabled = False
    Me.xx_magconto.FieldName = "xx_magconto"
    Me.xx_magconto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_magconto.Name = "xx_magconto"
    Me.xx_magconto.NTSRepositoryComboBox = Nothing
    Me.xx_magconto.NTSRepositoryItemCheck = Nothing
    Me.xx_magconto.NTSRepositoryItemMemo = Nothing
    Me.xx_magconto.NTSRepositoryItemText = Nothing
    Me.xx_magconto.OptionsColumn.AllowEdit = False
    Me.xx_magconto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_magconto.OptionsColumn.ReadOnly = True
    Me.xx_magconto.OptionsFilter.AllowFilter = False
    Me.xx_magconto.Visible = True
    Me.xx_magconto.VisibleIndex = 4
    Me.xx_magconto.Width = 159
    '
    'tb_consmrp
    '
    Me.tb_consmrp.AppearanceCell.Options.UseBackColor = True
    Me.tb_consmrp.AppearanceCell.Options.UseTextOptions = True
    Me.tb_consmrp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_consmrp.Caption = "Considera MRP"
    Me.tb_consmrp.Enabled = True
    Me.tb_consmrp.FieldName = "tb_consmrp"
    Me.tb_consmrp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_consmrp.Name = "tb_consmrp"
    Me.tb_consmrp.NTSRepositoryComboBox = Nothing
    Me.tb_consmrp.NTSRepositoryItemCheck = Nothing
    Me.tb_consmrp.NTSRepositoryItemMemo = Nothing
    Me.tb_consmrp.NTSRepositoryItemText = Nothing
    Me.tb_consmrp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_consmrp.OptionsFilter.AllowFilter = False
    Me.tb_consmrp.Visible = True
    Me.tb_consmrp.VisibleIndex = 6
    '
    'tb_magass
    '
    Me.tb_magass.AppearanceCell.Options.UseBackColor = True
    Me.tb_magass.AppearanceCell.Options.UseTextOptions = True
    Me.tb_magass.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_magass.Caption = "Magazzino assim."
    Me.tb_magass.Enabled = True
    Me.tb_magass.FieldName = "tb_magass"
    Me.tb_magass.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_magass.Name = "tb_magass"
    Me.tb_magass.NTSRepositoryComboBox = Nothing
    Me.tb_magass.NTSRepositoryItemCheck = Nothing
    Me.tb_magass.NTSRepositoryItemMemo = Nothing
    Me.tb_magass.NTSRepositoryItemText = Nothing
    Me.tb_magass.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_magass.OptionsFilter.AllowFilter = False
    Me.tb_magass.Visible = True
    Me.tb_magass.VisibleIndex = 7
    '
    'xx_magass
    '
    Me.xx_magass.AppearanceCell.Options.UseBackColor = True
    Me.xx_magass.AppearanceCell.Options.UseTextOptions = True
    Me.xx_magass.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_magass.Caption = "Descr. magaz. assim."
    Me.xx_magass.Enabled = False
    Me.xx_magass.FieldName = "xx_magass"
    Me.xx_magass.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_magass.Name = "xx_magass"
    Me.xx_magass.NTSRepositoryComboBox = Nothing
    Me.xx_magass.NTSRepositoryItemCheck = Nothing
    Me.xx_magass.NTSRepositoryItemMemo = Nothing
    Me.xx_magass.NTSRepositoryItemText = Nothing
    Me.xx_magass.OptionsColumn.AllowEdit = False
    Me.xx_magass.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_magass.OptionsColumn.ReadOnly = True
    Me.xx_magass.OptionsFilter.AllowFilter = False
    Me.xx_magass.Visible = True
    Me.xx_magass.VisibleIndex = 8
    '
    'tb_gescl
    '
    Me.tb_gescl.AppearanceCell.Options.UseBackColor = True
    Me.tb_gescl.AppearanceCell.Options.UseTextOptions = True
    Me.tb_gescl.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_gescl.Caption = "Gestione c/to lav."
    Me.tb_gescl.Enabled = True
    Me.tb_gescl.FieldName = "tb_gescl"
    Me.tb_gescl.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_gescl.Name = "tb_gescl"
    Me.tb_gescl.NTSRepositoryComboBox = Nothing
    Me.tb_gescl.NTSRepositoryItemCheck = Nothing
    Me.tb_gescl.NTSRepositoryItemMemo = Nothing
    Me.tb_gescl.NTSRepositoryItemText = Nothing
    Me.tb_gescl.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_gescl.OptionsFilter.AllowFilter = False
    Me.tb_gescl.Visible = True
    Me.tb_gescl.VisibleIndex = 9
    '
    'tb_coddest
    '
    Me.tb_coddest.AppearanceCell.Options.UseBackColor = True
    Me.tb_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.tb_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_coddest.Caption = "Destinazione"
    Me.tb_coddest.Enabled = True
    Me.tb_coddest.FieldName = "tb_coddest"
    Me.tb_coddest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_coddest.Name = "tb_coddest"
    Me.tb_coddest.NTSRepositoryComboBox = Nothing
    Me.tb_coddest.NTSRepositoryItemCheck = Nothing
    Me.tb_coddest.NTSRepositoryItemMemo = Nothing
    Me.tb_coddest.NTSRepositoryItemText = Nothing
    Me.tb_coddest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_coddest.OptionsFilter.AllowFilter = False
    Me.tb_coddest.Visible = True
    Me.tb_coddest.VisibleIndex = 10
    '
    'xx_coddest
    '
    Me.xx_coddest.AppearanceCell.Options.UseBackColor = True
    Me.xx_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.xx_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_coddest.Caption = "Descrizione destinazione"
    Me.xx_coddest.Enabled = False
    Me.xx_coddest.FieldName = "xx_coddest"
    Me.xx_coddest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_coddest.Name = "xx_coddest"
    Me.xx_coddest.NTSRepositoryComboBox = Nothing
    Me.xx_coddest.NTSRepositoryItemCheck = Nothing
    Me.xx_coddest.NTSRepositoryItemMemo = Nothing
    Me.xx_coddest.NTSRepositoryItemText = Nothing
    Me.xx_coddest.OptionsColumn.AllowEdit = False
    Me.xx_coddest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_coddest.OptionsColumn.ReadOnly = True
    Me.xx_coddest.OptionsFilter.AllowFilter = False
    Me.xx_coddest.Visible = True
    Me.xx_coddest.VisibleIndex = 11
    '
    'tb_applotti
    '
    Me.tb_applotti.AppearanceCell.Options.UseBackColor = True
    Me.tb_applotti.AppearanceCell.Options.UseTextOptions = True
    Me.tb_applotti.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_applotti.Caption = "Appl. pol. a lotto"
    Me.tb_applotti.Enabled = True
    Me.tb_applotti.FieldName = "tb_applotti"
    Me.tb_applotti.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_applotti.Name = "tb_applotti"
    Me.tb_applotti.NTSRepositoryComboBox = Nothing
    Me.tb_applotti.NTSRepositoryItemCheck = Nothing
    Me.tb_applotti.NTSRepositoryItemMemo = Nothing
    Me.tb_applotti.NTSRepositoryItemText = Nothing
    Me.tb_applotti.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_applotti.OptionsFilter.AllowFilter = False
    Me.tb_applotti.Visible = True
    Me.tb_applotti.VisibleIndex = 12
    '
    'tb_datacs
    '
    Me.tb_datacs.AppearanceCell.Options.UseBackColor = True
    Me.tb_datacs.AppearanceCell.Options.UseTextOptions = True
    Me.tb_datacs.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_datacs.Caption = "Data reg. ultimo agg. registro conto lavoro"
    Me.tb_datacs.DisplayFormat.FormatString = "dd/MM/yyy"
    Me.tb_datacs.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
    Me.tb_datacs.Enabled = True
    Me.tb_datacs.FieldName = "tb_datacs"
    Me.tb_datacs.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_datacs.Name = "tb_datacs"
    Me.tb_datacs.NTSRepositoryComboBox = Nothing
    Me.tb_datacs.NTSRepositoryItemCheck = Nothing
    Me.tb_datacs.NTSRepositoryItemMemo = Nothing
    Me.tb_datacs.NTSRepositoryItemText = Nothing
    Me.tb_datacs.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_datacs.OptionsFilter.AllowFilter = False
    Me.tb_datacs.Visible = True
    Me.tb_datacs.VisibleIndex = 13
    '
    'tb_progr
    '
    Me.tb_progr.AppearanceCell.Options.UseBackColor = True
    Me.tb_progr.AppearanceCell.Options.UseTextOptions = True
    Me.tb_progr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_progr.Caption = "Numero progressivo ultimo agg. registro conto lavoro"
    Me.tb_progr.Enabled = True
    Me.tb_progr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_progr.Name = "tb_progr"
    Me.tb_progr.NTSRepositoryComboBox = Nothing
    Me.tb_progr.NTSRepositoryItemCheck = Nothing
    Me.tb_progr.NTSRepositoryItemMemo = Nothing
    Me.tb_progr.NTSRepositoryItemText = Nothing
    Me.tb_progr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_progr.OptionsFilter.AllowFilter = False
    Me.tb_progr.Visible = True
    Me.tb_progr.VisibleIndex = 14
    '
    'tb_codstab
    '
    Me.tb_codstab.AppearanceCell.Options.UseBackColor = True
    Me.tb_codstab.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codstab.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codstab.Caption = "Stabilimento"
    Me.tb_codstab.Enabled = True
    Me.tb_codstab.FieldName = "tb_codstab"
    Me.tb_codstab.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codstab.Name = "tb_codstab"
    Me.tb_codstab.NTSRepositoryComboBox = Nothing
    Me.tb_codstab.NTSRepositoryItemCheck = Nothing
    Me.tb_codstab.NTSRepositoryItemMemo = Nothing
    Me.tb_codstab.NTSRepositoryItemText = Nothing
    Me.tb_codstab.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codstab.OptionsFilter.AllowFilter = False
    Me.tb_codstab.Visible = True
    Me.tb_codstab.VisibleIndex = 15
    '
    'xx_desstab
    '
    Me.xx_desstab.AppearanceCell.Options.UseBackColor = True
    Me.xx_desstab.AppearanceCell.Options.UseTextOptions = True
    Me.xx_desstab.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_desstab.Caption = "Desc. stabilimento"
    Me.xx_desstab.Enabled = False
    Me.xx_desstab.FieldName = "xx_desstab"
    Me.xx_desstab.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_desstab.Name = "xx_desstab"
    Me.xx_desstab.NTSRepositoryComboBox = Nothing
    Me.xx_desstab.NTSRepositoryItemCheck = Nothing
    Me.xx_desstab.NTSRepositoryItemMemo = Nothing
    Me.xx_desstab.NTSRepositoryItemText = Nothing
    Me.xx_desstab.OptionsColumn.AllowEdit = False
    Me.xx_desstab.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_desstab.OptionsColumn.ReadOnly = True
    Me.xx_desstab.OptionsFilter.AllowFilter = False
    Me.xx_desstab.Visible = True
    Me.xx_desstab.VisibleIndex = 16
    '
    'tb_gestubi
    '
    Me.tb_gestubi.AppearanceCell.Options.UseBackColor = True
    Me.tb_gestubi.AppearanceCell.Options.UseTextOptions = True
    Me.tb_gestubi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_gestubi.Caption = "Gest. ubicazioni dinamiche"
    Me.tb_gestubi.Enabled = True
    Me.tb_gestubi.FieldName = "tb_gestubi"
    Me.tb_gestubi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_gestubi.Name = "tb_gestubi"
    Me.tb_gestubi.NTSRepositoryComboBox = Nothing
    Me.tb_gestubi.NTSRepositoryItemCheck = Nothing
    Me.tb_gestubi.NTSRepositoryItemMemo = Nothing
    Me.tb_gestubi.NTSRepositoryItemText = Nothing
    Me.tb_gestubi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_gestubi.OptionsFilter.AllowFilter = False
    Me.tb_gestubi.Visible = True
    Me.tb_gestubi.VisibleIndex = 17
    '
    'tb_dubicaz
    '
    Me.tb_dubicaz.AppearanceCell.Options.UseBackColor = True
    Me.tb_dubicaz.AppearanceCell.Options.UseTextOptions = True
    Me.tb_dubicaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_dubicaz.Caption = "Ubic. din. di default"
    Me.tb_dubicaz.Enabled = True
    Me.tb_dubicaz.FieldName = "tb_dubicaz"
    Me.tb_dubicaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_dubicaz.Name = "tb_dubicaz"
    Me.tb_dubicaz.NTSRepositoryComboBox = Nothing
    Me.tb_dubicaz.NTSRepositoryItemCheck = Nothing
    Me.tb_dubicaz.NTSRepositoryItemMemo = Nothing
    Me.tb_dubicaz.NTSRepositoryItemText = Nothing
    Me.tb_dubicaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_dubicaz.OptionsFilter.AllowFilter = False
    Me.tb_dubicaz.Visible = True
    Me.tb_dubicaz.VisibleIndex = 18
    '
    'tb_gestmat
    '
    Me.tb_gestmat.AppearanceCell.Options.UseBackColor = True
    Me.tb_gestmat.AppearanceCell.Options.UseTextOptions = True
    Me.tb_gestmat.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_gestmat.Caption = "Gest. matricole"
    Me.tb_gestmat.Enabled = True
    Me.tb_gestmat.FieldName = "tb_gestmat"
    Me.tb_gestmat.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_gestmat.Name = "tb_gestmat"
    Me.tb_gestmat.NTSRepositoryComboBox = Nothing
    Me.tb_gestmat.NTSRepositoryItemCheck = Nothing
    Me.tb_gestmat.NTSRepositoryItemMemo = Nothing
    Me.tb_gestmat.NTSRepositoryItemText = Nothing
    Me.tb_gestmat.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_gestmat.OptionsFilter.AllowFilter = False
    Me.tb_gestmat.Visible = True
    Me.tb_gestmat.VisibleIndex = 19
    '
    'tb_dmatric
    '
    Me.tb_dmatric.AppearanceCell.Options.UseBackColor = True
    Me.tb_dmatric.AppearanceCell.Options.UseTextOptions = True
    Me.tb_dmatric.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_dmatric.Caption = "Matricola di default"
    Me.tb_dmatric.Enabled = True
    Me.tb_dmatric.FieldName = "tb_dmatric"
    Me.tb_dmatric.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_dmatric.Name = "tb_dmatric"
    Me.tb_dmatric.NTSRepositoryComboBox = Nothing
    Me.tb_dmatric.NTSRepositoryItemCheck = Nothing
    Me.tb_dmatric.NTSRepositoryItemMemo = Nothing
    Me.tb_dmatric.NTSRepositoryItemText = Nothing
    Me.tb_dmatric.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_dmatric.OptionsFilter.AllowFilter = False
    Me.tb_dmatric.Visible = True
    Me.tb_dmatric.VisibleIndex = 20
    '
    'tb_gestlot
    '
    Me.tb_gestlot.AppearanceCell.Options.UseBackColor = True
    Me.tb_gestlot.AppearanceCell.Options.UseTextOptions = True
    Me.tb_gestlot.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_gestlot.Caption = "Gest. lotti"
    Me.tb_gestlot.Enabled = True
    Me.tb_gestlot.FieldName = "tb_gestlot"
    Me.tb_gestlot.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_gestlot.Name = "tb_gestlot"
    Me.tb_gestlot.NTSRepositoryComboBox = Nothing
    Me.tb_gestlot.NTSRepositoryItemCheck = Nothing
    Me.tb_gestlot.NTSRepositoryItemMemo = Nothing
    Me.tb_gestlot.NTSRepositoryItemText = Nothing
    Me.tb_gestlot.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_gestlot.OptionsFilter.AllowFilter = False
    Me.tb_gestlot.Visible = True
    Me.tb_gestlot.VisibleIndex = 21
    '
    'tb_dlottox
    '
    Me.tb_dlottox.AppearanceCell.Options.UseBackColor = True
    Me.tb_dlottox.AppearanceCell.Options.UseTextOptions = True
    Me.tb_dlottox.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_dlottox.Caption = "Lotto di default"
    Me.tb_dlottox.Enabled = True
    Me.tb_dlottox.FieldName = "tb_dlottox"
    Me.tb_dlottox.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_dlottox.Name = "tb_dlottox"
    Me.tb_dlottox.NTSRepositoryComboBox = Nothing
    Me.tb_dlottox.NTSRepositoryItemCheck = Nothing
    Me.tb_dlottox.NTSRepositoryItemMemo = Nothing
    Me.tb_dlottox.NTSRepositoryItemText = Nothing
    Me.tb_dlottox.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_dlottox.OptionsFilter.AllowFilter = False
    Me.tb_dlottox.Visible = True
    Me.tb_dlottox.VisibleIndex = 22
    '
    'tb_dubicst
    '
    Me.tb_dubicst.AppearanceCell.Options.UseBackColor = True
    Me.tb_dubicst.AppearanceCell.Options.UseTextOptions = True
    Me.tb_dubicst.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_dubicst.Caption = "Ubic. di stock"
    Me.tb_dubicst.Enabled = True
    Me.tb_dubicst.FieldName = "tb_dubicst"
    Me.tb_dubicst.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_dubicst.Name = "tb_dubicst"
    Me.tb_dubicst.NTSRepositoryComboBox = Nothing
    Me.tb_dubicst.NTSRepositoryItemCheck = Nothing
    Me.tb_dubicst.NTSRepositoryItemMemo = Nothing
    Me.tb_dubicst.NTSRepositoryItemText = Nothing
    Me.tb_dubicst.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_dubicst.OptionsFilter.AllowFilter = False
    Me.tb_dubicst.Visible = True
    Me.tb_dubicst.VisibleIndex = 23
    '
    'tb_dubicpr
    '
    Me.tb_dubicpr.AppearanceCell.Options.UseBackColor = True
    Me.tb_dubicpr.AppearanceCell.Options.UseTextOptions = True
    Me.tb_dubicpr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_dubicpr.Caption = "Ubic. di produzione"
    Me.tb_dubicpr.Enabled = True
    Me.tb_dubicpr.FieldName = "tb_dubicpr"
    Me.tb_dubicpr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_dubicpr.Name = "tb_dubicpr"
    Me.tb_dubicpr.NTSRepositoryComboBox = Nothing
    Me.tb_dubicpr.NTSRepositoryItemCheck = Nothing
    Me.tb_dubicpr.NTSRepositoryItemMemo = Nothing
    Me.tb_dubicpr.NTSRepositoryItemText = Nothing
    Me.tb_dubicpr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_dubicpr.OptionsFilter.AllowFilter = False
    Me.tb_dubicpr.Visible = True
    Me.tb_dubicpr.VisibleIndex = 24
    '
    'tb_dubicri
    '
    Me.tb_dubicri.AppearanceCell.Options.UseBackColor = True
    Me.tb_dubicri.AppearanceCell.Options.UseTextOptions = True
    Me.tb_dubicri.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_dubicri.Caption = "Ubic. di ricevimento"
    Me.tb_dubicri.Enabled = True
    Me.tb_dubicri.FieldName = "tb_dubicri"
    Me.tb_dubicri.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_dubicri.Name = "tb_dubicri"
    Me.tb_dubicri.NTSRepositoryComboBox = Nothing
    Me.tb_dubicri.NTSRepositoryItemCheck = Nothing
    Me.tb_dubicri.NTSRepositoryItemMemo = Nothing
    Me.tb_dubicri.NTSRepositoryItemText = Nothing
    Me.tb_dubicri.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_dubicri.OptionsFilter.AllowFilter = False
    Me.tb_dubicri.Visible = True
    Me.tb_dubicri.VisibleIndex = 25
    '
    'tb_dubicus
    '
    Me.tb_dubicus.AppearanceCell.Options.UseBackColor = True
    Me.tb_dubicus.AppearanceCell.Options.UseTextOptions = True
    Me.tb_dubicus.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_dubicus.Caption = "Ubic. di uscita"
    Me.tb_dubicus.Enabled = True
    Me.tb_dubicus.FieldName = "tb_dubicus"
    Me.tb_dubicus.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_dubicus.Name = "tb_dubicus"
    Me.tb_dubicus.NTSRepositoryComboBox = Nothing
    Me.tb_dubicus.NTSRepositoryItemCheck = Nothing
    Me.tb_dubicus.NTSRepositoryItemMemo = Nothing
    Me.tb_dubicus.NTSRepositoryItemText = Nothing
    Me.tb_dubicus.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_dubicus.OptionsFilter.AllowFilter = False
    Me.tb_dubicus.Visible = True
    Me.tb_dubicus.VisibleIndex = 26
    '
    'tb_webvis
    '
    Me.tb_webvis.AppearanceCell.Options.UseBackColor = True
    Me.tb_webvis.AppearanceCell.Options.UseTextOptions = True
    Me.tb_webvis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_webvis.Caption = "Magazzino visibile dall'applicazione esterna"
    Me.tb_webvis.Enabled = True
    Me.tb_webvis.FieldName = "tb_webvis"
    Me.tb_webvis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_webvis.Name = "tb_webvis"
    Me.tb_webvis.NTSRepositoryComboBox = Nothing
    Me.tb_webvis.NTSRepositoryItemCheck = Nothing
    Me.tb_webvis.NTSRepositoryItemMemo = Nothing
    Me.tb_webvis.NTSRepositoryItemText = Nothing
    Me.tb_webvis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_webvis.OptionsFilter.AllowFilter = False
    Me.tb_webvis.Visible = True
    Me.tb_webvis.VisibleIndex = 27
    '
    'FRMMGMAGA
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.grMaga)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMMGMAGA"
    Me.NTSLastControlFocussed = Me.grMaga
    Me.Text = "MAGAZZINI / DEPOSITI / TERZISTI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grMaga, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvMaga, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.RepositoryItemButtonEdit1, System.ComponentModel.ISupportInitialize).EndInit()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGMAGA", "BEMGMAGA", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 127791222114375000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleMaga = CType(oTmp, CLEMGMAGA)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGMAGA", strRemoteServer, strRemotePort)
    AddHandler oCleMaga.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleMaga.Init(oApp, oScript, oMenu.oCleComm, "TABMAGA", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

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
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      Dim dttTipo As New DataTable()

      dttTipo.Columns.Add("cod", GetType(String))
      dttTipo.Columns.Add("val", GetType(String))
      dttTipo.Rows.Add(New Object() {" ", "Propria"})
      dttTipo.Rows.Add(New Object() {"C", "Altrui"})
      dttTipo.Rows.Add(New Object() {"F", "Propria presso terzi"})
      dttTipo.Rows.Add(New Object() {"X", "Altrui presso terzi"})
      dttTipo.AcceptChanges()

      grvMaga.NTSSetParam(oMenu, oApp.Tr(Me, 128230023168975422, "Griglia Magazzini/depositi/terzisti"))
      tb_codmaga.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023169131595, "Codice magazzino"), CLN__STD.tabmaga)
      tb_desmaga.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023169287768, "Descrizione magazzino"), 50, False)
      tb_flclavo.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128230023169443941, "Tipo magazzino"), dttTipo, "val", "cod")
      tb_magconto.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023169756287, "Conto C/F"), CLN__STD.tabanagraf)
      xx_magconto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023169912460, "Denomin. conto"), 30)
      tb_appscmin.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023170068633, "Appl. punto riord."), "S", "N")
      tb_datacs.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128230023170224806, "Data ultimo agg. registro conto lavoro"), True)
      tb_progr.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023170224996, "Numero progressivo agg. registro conto lavoro"), "0", 8, 0, 99999999)
      tb_consmrp.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023170380979, "Considera MRP"), "S", "N")
      tb_magass.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023170537152, "Magazzino assimilato"), CLN__STD.tabmaga)
      xx_magass.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023170693325, "Descrizione magazzino assimilato"), 30)
      tb_gescl.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023170849498, "Gestione c/to lavoro"), "S", "N")
      tb_coddest.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023171005671, "Destinazione diversa"), CLN__STD.tabdestdiv)
      tb_coddest.CliePerDestdiv = tb_magconto
      xx_coddest.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023171161844, "Descrizione destinazione diversa"), 30)
      tb_applotti.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023171318017, "Appl. polit. riord."), "S", "N")
      tb_codstab.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128788179580244324, "Stabilimento"), tabstab)
      tb_gestubi.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128788179580556828, "Gest. ubicazioni dinamiche"), "S", "N")
      tb_dubicaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788179580713080, "Ubic. din. di default"), 18, False)
      tb_gestmat.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128788179580869332, "Gest. matricole"), "S", "N")
      tb_gestlot.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128788179581181836, "Gest. lotti"), "S", "N")
      tb_dubicst.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788179581494340, "Ubic. di stock"), 18, False)
      tb_dubicpr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788179581650592, "Ubic. di produzione"), 18, False)
      tb_dubicri.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788179581806844, "Ubic. di ricevimento"), 18, False)
      tb_dubicus.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788179581963096, "Ubic. di uscita"), 18, False)
      xx_desstab.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788180693227320, "Desc. stabilimento"), 0, True)
      tb_dlottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788215307850863, "Lotto di default"), 50, True) ' zoom non gestibile (manca il codice articolo)
      tb_dmatric.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128788215307538357, "Matricola di default"), 30, False) ' Al momento non gestisce lo zoom
      tb_webvis.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130415263711381874, "Magazzino visibile dall'applicazione esterna"), "S", "N")

      tb_codmaga.NTSSetParamZoom("")     'in questo modo non faccio apparire lo zoom a fianco del codice magazzino (lo zoom è stato abilitato da NTSSetParamTabe)
      tb_codmaga.NTSSetRichiesto()
      tb_desmaga.NTSSetRichiesto()

      tb_dubicaz.NTSForzaVisZoom = CBool(oCleMaga.ModuliSupDittaDitt(DittaCorrente) And bsModSupRME)

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
  Public Overridable Sub FRMMGMAGA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      'Il default da proporre sulle nuove righe (va impostato prima della SetDefaultValue)
      If Not oCallParams Is Nothing Then
        If oCallParams.dPar1 > 0 Then oCleMaga.lCodStab = NTSCInt(oCallParams.dPar1)
      End If

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleMaga.Apri(DittaCorrente, dsMaga) Then
        Me.Close()
        Return
      End If
      dcMaga.DataSource = dsMaga.Tables("TABMAGA")
      dsMaga.AcceptChanges()

      grMaga.DataSource = dcMaga

      If CBool(bsModSupRME And oMenu.ModuliSupDittaDitt(DittaCorrente)) Then
        oCleMaga.bModRME = True
      Else
        oCleMaga.bModRME = False
      End If

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      'e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipo documento, ecc ...
      '(Ex: CTRL+ALT+F4)
      'GctlTipoDoc = ""
      GctlSetRoules()

      '-------------------------------------------------
      'imposto i valori di default per le nuove righe
      'questa funzione può sempre essere settata, anche, ad esempio, al cambio del tipork in veboll
      'GctlApplicaDefaultValue()

      'solo per prova: annulla le impostazioni della GCTL
      'GctlRestoreStandardValue()

      '--------------------------------------------
      'sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
      If Not oCallParams Is Nothing Then
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
          If grvMaga.NTSAllowInsert Then
            grvMaga.NTSNuovo()
          End If
        ElseIf Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) <> "" Then
          Dim lPos As Integer = dcMaga.Find("tb_codmaga", Microsoft.VisualBasic.Mid(oCallParams.strParam, 6))
          If lPos >= 0 Then dcMaga.Position = lPos
        End If

        'dagli stabilimento filtro solo i magazzini di quello stabilimento (da applicare al bindingsource dopo che è stato riempito)
        If oCallParams.dPar1 > 0 Then dcMaga.Filter = "tb_codstab = " & oCallParams.dPar1
      End If  'If Not oCallParams Is Nothing Then
      '--------------------------------------------------------------------------------------------------------------
      If (CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupCEC)) And (CLN__STD.FRIENDLY = False) Then
      Else
        tb_webvis.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione Friendly nasconde, sempre, alcune colonne
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = True Then
        tb_appscmin.Visible = False
        tb_consmrp.Visible = False
        tb_magass.Visible = False
        xx_magass.Visible = False
        tb_gescl.Visible = False
        tb_applotti.Visible = False
        tb_datacs.Visible = False
        tb_progr.Visible = False
        tb_codstab.Visible = False
        xx_desstab.Visible = False
        tb_gestubi.Visible = False
        tb_dubicaz.Visible = False
        tb_gestmat.Visible = False
        tb_dmatric.Visible = False
        tb_gestlot.Visible = False
        tb_dlottox.Visible = False
        tb_dubicst.Visible = False
        tb_dubicpr.Visible = False
        tb_dubicri.Visible = False
        tb_dubicus.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGMAGA_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGMAGA_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcMaga.Dispose()
      dsMaga.Dispose()
    Catch
    End Try
  End Sub
#End Region


  Public Overridable Sub grvMaga_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvMaga.NTSBeforeRowUpdate
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

  Public Overridable Sub grvMaga_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvMaga.NTSFocusedRowChanged
    Try
      '-------------------------------------------------
      'se il codice magazzino è diverso da 0 blocco la colonna, diversamente la rendo editabile
      If NTSCInt(grvMaga.GetFocusedRowCellValue(tb_codmaga).ToString.Trim) <> 0 Then
        tb_codmaga.Enabled = False
      Else
        GctlSetVisEnab(tb_codmaga, False)
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
      dRes = grvMaga.NTSSalvaRigaCorrente(dcMaga, oCleMaga.RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleMaga.Salva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleMaga.Ripristina(dcMaga.Position, dcMaga.Filter)
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

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer

    Try
      '--------------------------------------------------
      'preparo il motore di stampa
      strCrpe = "{tabmaga.codditt} = '" & oApp.Ditta & "'"
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGMAGA", "Reports1", " ", 0, nDestin, "BSMGMAGA.RPT", False, "Magazzini/depositi/terzisti", False)
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

  Public Overridable Sub tb_coddest_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles tb_coddest.NTSZoomGest
    Try
      'per il momenento non c'è la possibilità di andare in gestione della destinazione diversa
      'se si cambia qualche dato occorrerebbe contemplare l'aggiornamento delle eventuali anagrafiche generali...
      'oApp.MsgBoxErr("da fare: chiamata per poter gestire ALT+F2 o ALT+F3")
      'e.ZoomHandled = True        'non facci eseguire la routine standard del ALT+F2 o ALT+F3
      'limite superato: basta impostare tb_coddest.CliePerDestdiv = tb_magconto
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvMaga.NTSNuovo()
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
      If Not grvMaga.NTSDeleteRigaCorrente(dcMaga, True) Then Return
      oCleMaga.Salva(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvMaga.NTSRipristinaRigaCorrenteBefore(dcMaga, True) Then Return
      oCleMaga.Ripristina(dcMaga.Position, dcMaga.Filter)
      grvMaga.NTSRipristinaRigaCorrenteAfter()
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
      If grvMaga.FocusedColumn.Name = "tb_coddest" Then
        'caso specifico per lo zoom su destdiv (devo passare il fornitore)
        SetFastZoom(NTSCStr(grvMaga.EditingValue), oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = NTSCStr(grvMaga.EditingValue)
        oParam.lContoCF = NTSCInt(grvMaga.GetFocusedRowCellValue("tb_magconto"))   'passo il conto cliente/fornitore
        NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvMaga.EditingValue) Then grvMaga.SetFocusedValue(NTSZOOM.strIn)
      ElseIf grvMaga.FocusedColumn.Name = "tb_magconto" Then
        '--------------------------------------------
        'zoom anagra
        SetFastZoom(NTSCStr(grvMaga.EditingValue), oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = NTSCStr(grvMaga.EditingValue)
        oParam.bVisGriglia = True
        oParam.strTipo = "F"
        oParam.bTipoProposto = True
        NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvMaga.EditingValue) Then grvMaga.SetFocusedValue(NTSZOOM.strIn)
      ElseIf grvMaga.FocusedColumn.Name = "tb_dubicaz" Then
        If grvMaga.NTSGetCurrentDataRow Is Nothing Then Return
        If Not CBool(oCleMaga.ModuliSupDittaDitt(DittaCorrente) And bsModSupRME) Then Return

        oCallParams = New CLE__CLDP
        oCallParams.strPar1 = ""
        oCallParams.strPar2 = ""
        oCallParams.dPar1 = NTSCInt(grvMaga.NTSGetCurrentDataRow!tb_codmaga)
        oCallParams.bPar1 = False

        'Chiamo la dll
        oMenu.RunChild("NTSInformatica", "FRMRMSIUB", oApp.Tr(Me, 128885197022015890, "Situazione"), DittaCorrente, "", "BNRMSIUB", oCallParams, Nothing, True, True)

        If oCallParams.strPar3.Trim("-"c).Trim <> "" Then grvMaga.NTSGetCurrentDataRow!tb_dubicaz = oCallParams.strPar3.PadRight(12, "-"c)
      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        NTSCallStandardZoom()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      strErr = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    Try
      oMenu.ReportImposta(Me)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

End Class
