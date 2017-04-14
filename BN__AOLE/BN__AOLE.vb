Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__AOLE

#Region "Variabili"
  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = bsModExtAll
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

  Public oCleAole As CLE__AOLE
  Public oCallParams As CLE__CLDP
  Public dsAole As DataSet
  Public dcAole As BindingSource = New BindingSource()
  Public bAmm As Boolean = False

  'Costanti per la visualizzazione dei files
  Public Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (ByVal hWnd As System.IntPtr, ByVal lpszOp As String, ByVal lpszFile As String, ByVal lpszParams As String, ByVal lpszDir As String, ByVal FsShowCmd As Integer) As Integer
  Public Const SW_SHOWNORMAL As Integer = 1
  Public Const SE_ERR_FNF As Integer = 2&
  Public Const SE_ERR_PNF As Integer = 3&
  Public Const SE_ERR_ACCESSDENIED As Integer = 5&
  Public Const SE_ERR_OOM As Integer = 8&
  Public Const SE_ERR_DLLNOTFOUND As Integer = 32&
  Public Const SE_ERR_SHARE As Integer = 26&
  Public Const SE_ERR_ASSOCINCOMPLETE As Integer = 27&
  Public Const SE_ERR_DDETIMEOUT As Integer = 28&
  Public Const SE_ERR_DDEFAIL As Integer = 29&
  Public Const SE_ERR_DDEBUSY As Integer = 30&
  Public Const SE_ERR_NOASSOC As Integer = 31&
  Public Const ERROR_BAD_FORMAT As Integer = 11&

#End Region

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__AOLE))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbRiparti = New NTSInformatica.NTSBarButtonItem
    Me.tlbApriCon = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grAole = New NTSInformatica.NTSGrid
    Me.grvAole = New NTSInformatica.NTSGridView
    Me.ao_tipo = New NTSInformatica.NTSGridColumn
    Me.ao_tipork = New NTSInformatica.NTSGridColumn
    Me.ao_descr = New NTSInformatica.NTSGridColumn
    Me.ao_descr2 = New NTSInformatica.NTSGridColumn
    Me.ao_cartella = New NTSInformatica.NTSGridColumn
    Me.ao_nomedoc = New NTSInformatica.NTSGridColumn
    Me.xx_nomedoc = New NTSInformatica.NTSGridColumn
    Me.ao_argom = New NTSInformatica.NTSGridColumn
    Me.ao_ubicaz = New NTSInformatica.NTSGridColumn
    Me.ao_autore = New NTSInformatica.NTSGridColumn
    Me.ao_redattore = New NTSInformatica.NTSGridColumn
    Me.ao_codice = New NTSInformatica.NTSGridColumn
    Me.xx_descr1 = New NTSInformatica.NTSGridColumn
    Me.ao_controp = New NTSInformatica.NTSGridColumn
    Me.xx_descovg = New NTSInformatica.NTSGridColumn
    Me.ao_strcod = New NTSInformatica.NTSGridColumn
    Me.xx_descr = New NTSInformatica.NTSGridColumn
    Me.ao_tipodoc = New NTSInformatica.NTSGridColumn
    Me.ao_annodoc = New NTSInformatica.NTSGridColumn
    Me.ao_seriedoc = New NTSInformatica.NTSGridColumn
    Me.ao_numdoc = New NTSInformatica.NTSGridColumn
    Me.ao_rigadoc = New NTSInformatica.NTSGridColumn
    Me.ao_commeca = New NTSInformatica.NTSGridColumn
    Me.xx_descr1_commess = New NTSInformatica.NTSGridColumn
    Me.xx_lottox = New NTSInformatica.NTSGridColumn
    Me.ao_matric = New NTSInformatica.NTSGridColumn
    Me.ao_ultagg = New NTSInformatica.NTSGridColumn
    Me.ao_datins = New NTSInformatica.NTSGridColumn
    Me.ao_codlead = New NTSInformatica.NTSGridColumn
    Me.xx_descr1_lead = New NTSInformatica.NTSGridColumn
    Me.ao_codoppo = New NTSInformatica.NTSGridColumn
    Me.xx_oggetto = New NTSInformatica.NTSGridColumn
    Me.ao_codchia = New NTSInformatica.NTSGridColumn
    Me.xx_oggetto_nnchiam = New NTSInformatica.NTSGridColumn
    Me.ao_numcontr = New NTSInformatica.NTSGridColumn
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.ao_progress = New NTSInformatica.NTSGridColumn
    Me.ao_classe = New NTSInformatica.NTSGridColumn
    Me.ao_classeole = New NTSInformatica.NTSGridColumn
    Me.ao_ole = New NTSInformatica.NTSGridColumn
    Me.ao_progresl = New NTSInformatica.NTSGridColumn
    Me.xx_oggetto_off = New NTSInformatica.NTSGridColumn
    Me.NtsGridView1 = New NTSInformatica.NTSGridView
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grAole, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvAole, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbRiparti, Me.tlbApriCon, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 11
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, False, Me.tlbRiparti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApriCon), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbNuovo.Id = 1
    Me.tlbNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F2))
    Me.tlbNuovo.Name = "tlbNuovo"
    Me.tlbNuovo.Visible = True
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.GlyphPath = ""
    Me.tlbSalva.Id = 2
    Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F9))
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.GlyphPath = ""
    Me.tlbRipristina.Id = 4
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F8))
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.GlyphPath = ""
    Me.tlbCancella.Id = 3
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F4))
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.GlyphPath = ""
    Me.tlbZoom.Id = 5
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbRiparti
    '
    Me.tlbRiparti.Caption = "Riparti"
    Me.tlbRiparti.Glyph = CType(resources.GetObject("tlbRiparti.Glyph"), System.Drawing.Image)
    Me.tlbRiparti.GlyphPath = ""
    Me.tlbRiparti.Id = 6
    Me.tlbRiparti.Name = "tlbRiparti"
    Me.tlbRiparti.Visible = False
    '
    'tlbApriCon
    '
    Me.tlbApriCon.Caption = "Apri"
    Me.tlbApriCon.Glyph = CType(resources.GetObject("tlbApriCon.Glyph"), System.Drawing.Image)
    Me.tlbApriCon.GlyphPath = ""
    Me.tlbApriCon.Id = 8
    Me.tlbApriCon.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F7))
    Me.tlbApriCon.Name = "tlbApriCon"
    Me.tlbApriCon.Visible = True
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
    'grAole
    '
    Me.grAole.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '
    '
    '
    Me.grAole.EmbeddedNavigator.Name = ""
    Me.grAole.Location = New System.Drawing.Point(0, 28)
    Me.grAole.MainView = Me.grvAole
    Me.grAole.Name = "grAole"
    Me.grAole.Size = New System.Drawing.Size(792, 539)
    Me.grAole.TabIndex = 4
    Me.grAole.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvAole, Me.NtsGridView1})
    '
    'grvAole
    '
    Me.grvAole.ActiveFilterEnabled = False
    Me.grvAole.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ao_tipo, Me.ao_tipork, Me.ao_descr, Me.ao_descr2, Me.ao_cartella, Me.ao_nomedoc, Me.xx_nomedoc, Me.ao_argom, Me.ao_ubicaz, Me.ao_autore, Me.ao_redattore, Me.ao_codice, Me.xx_descr1, Me.ao_controp, Me.xx_descovg, Me.ao_strcod, Me.xx_descr, Me.ao_tipodoc, Me.ao_annodoc, Me.ao_seriedoc, Me.ao_numdoc, Me.ao_rigadoc, Me.ao_commeca, Me.xx_descr1_commess, Me.xx_lottox, Me.ao_matric, Me.ao_ultagg, Me.ao_datins, Me.ao_codlead, Me.xx_descr1_lead, Me.ao_codoppo, Me.xx_oggetto, Me.ao_codchia, Me.xx_oggetto_nnchiam, Me.ao_numcontr, Me.codditt, Me.ao_progress, Me.ao_classe, Me.ao_classeole, Me.ao_ole, Me.ao_progresl, Me.xx_oggetto_off})
    Me.grvAole.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvAole.Enabled = True
    Me.grvAole.GridControl = Me.grAole
    Me.grvAole.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvAole.MinRowHeight = 14
    Me.grvAole.Name = "grvAole"
    Me.grvAole.NTSAllowDelete = True
    Me.grvAole.NTSAllowInsert = True
    Me.grvAole.NTSAllowUpdate = True
    Me.grvAole.NTSMenuContext = Nothing
    Me.grvAole.OptionsCustomization.AllowRowSizing = True
    Me.grvAole.OptionsFilter.AllowFilterEditor = False
    Me.grvAole.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvAole.OptionsNavigation.UseTabKey = False
    Me.grvAole.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvAole.OptionsView.ColumnAutoWidth = False
    Me.grvAole.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvAole.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvAole.OptionsView.ShowGroupPanel = False
    Me.grvAole.RowHeight = 14
    '
    'ao_tipo
    '
    Me.ao_tipo.AppearanceCell.Options.UseBackColor = True
    Me.ao_tipo.AppearanceCell.Options.UseTextOptions = True
    Me.ao_tipo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_tipo.Caption = "Tipo record"
    Me.ao_tipo.Enabled = False
    Me.ao_tipo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_tipo.Name = "ao_tipo"
    Me.ao_tipo.NTSRepositoryComboBox = Nothing
    Me.ao_tipo.NTSRepositoryItemCheck = Nothing
    Me.ao_tipo.NTSRepositoryItemMemo = Nothing
    Me.ao_tipo.NTSRepositoryItemText = Nothing
    Me.ao_tipo.OptionsColumn.AllowEdit = False
    Me.ao_tipo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_tipo.OptionsColumn.ReadOnly = True
    Me.ao_tipo.OptionsFilter.AllowFilter = False
    Me.ao_tipo.Visible = True
    Me.ao_tipo.VisibleIndex = 0
    '
    'ao_tipork
    '
    Me.ao_tipork.AppearanceCell.Options.UseBackColor = True
    Me.ao_tipork.AppearanceCell.Options.UseTextOptions = True
    Me.ao_tipork.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_tipork.Caption = "Tipo"
    Me.ao_tipork.Enabled = False
    Me.ao_tipork.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_tipork.Name = "ao_tipork"
    Me.ao_tipork.NTSRepositoryComboBox = Nothing
    Me.ao_tipork.NTSRepositoryItemCheck = Nothing
    Me.ao_tipork.NTSRepositoryItemMemo = Nothing
    Me.ao_tipork.NTSRepositoryItemText = Nothing
    Me.ao_tipork.OptionsColumn.AllowEdit = False
    Me.ao_tipork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_tipork.OptionsColumn.ReadOnly = True
    Me.ao_tipork.OptionsFilter.AllowFilter = False
    '
    'ao_descr
    '
    Me.ao_descr.AppearanceCell.Options.UseBackColor = True
    Me.ao_descr.AppearanceCell.Options.UseTextOptions = True
    Me.ao_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_descr.Caption = "Descrizione"
    Me.ao_descr.Enabled = True
    Me.ao_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_descr.Name = "ao_descr"
    Me.ao_descr.NTSRepositoryComboBox = Nothing
    Me.ao_descr.NTSRepositoryItemCheck = Nothing
    Me.ao_descr.NTSRepositoryItemMemo = Nothing
    Me.ao_descr.NTSRepositoryItemText = Nothing
    Me.ao_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_descr.OptionsFilter.AllowFilter = False
    Me.ao_descr.Visible = True
    Me.ao_descr.VisibleIndex = 1
    '
    'ao_descr2
    '
    Me.ao_descr2.AppearanceCell.Options.UseBackColor = True
    Me.ao_descr2.AppearanceCell.Options.UseTextOptions = True
    Me.ao_descr2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_descr2.Caption = "Descrizione 2"
    Me.ao_descr2.Enabled = True
    Me.ao_descr2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_descr2.Name = "ao_descr2"
    Me.ao_descr2.NTSRepositoryComboBox = Nothing
    Me.ao_descr2.NTSRepositoryItemCheck = Nothing
    Me.ao_descr2.NTSRepositoryItemMemo = Nothing
    Me.ao_descr2.NTSRepositoryItemText = Nothing
    Me.ao_descr2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_descr2.OptionsFilter.AllowFilter = False
    Me.ao_descr2.Visible = True
    Me.ao_descr2.VisibleIndex = 2
    '
    'ao_cartella
    '
    Me.ao_cartella.AppearanceCell.Options.UseBackColor = True
    Me.ao_cartella.AppearanceCell.Options.UseTextOptions = True
    Me.ao_cartella.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_cartella.Caption = "Cartella"
    Me.ao_cartella.Enabled = True
    Me.ao_cartella.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_cartella.Name = "ao_cartella"
    Me.ao_cartella.NTSRepositoryComboBox = Nothing
    Me.ao_cartella.NTSRepositoryItemCheck = Nothing
    Me.ao_cartella.NTSRepositoryItemMemo = Nothing
    Me.ao_cartella.NTSRepositoryItemText = Nothing
    Me.ao_cartella.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_cartella.OptionsFilter.AllowFilter = False
    Me.ao_cartella.Visible = True
    Me.ao_cartella.VisibleIndex = 3
    '
    'ao_nomedoc
    '
    Me.ao_nomedoc.AppearanceCell.Options.UseBackColor = True
    Me.ao_nomedoc.AppearanceCell.Options.UseTextOptions = True
    Me.ao_nomedoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_nomedoc.Caption = "Nome file"
    Me.ao_nomedoc.Enabled = True
    Me.ao_nomedoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_nomedoc.Name = "ao_nomedoc"
    Me.ao_nomedoc.NTSRepositoryComboBox = Nothing
    Me.ao_nomedoc.NTSRepositoryItemCheck = Nothing
    Me.ao_nomedoc.NTSRepositoryItemMemo = Nothing
    Me.ao_nomedoc.NTSRepositoryItemText = Nothing
    Me.ao_nomedoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_nomedoc.OptionsFilter.AllowFilter = False
    Me.ao_nomedoc.Visible = True
    Me.ao_nomedoc.VisibleIndex = 4
    '
    'xx_nomedoc
    '
    Me.xx_nomedoc.AppearanceCell.Options.UseBackColor = True
    Me.xx_nomedoc.AppearanceCell.Options.UseTextOptions = True
    Me.xx_nomedoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_nomedoc.Caption = "Percorso fisico"
    Me.xx_nomedoc.Enabled = False
    Me.xx_nomedoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_nomedoc.Name = "xx_nomedoc"
    Me.xx_nomedoc.NTSRepositoryComboBox = Nothing
    Me.xx_nomedoc.NTSRepositoryItemCheck = Nothing
    Me.xx_nomedoc.NTSRepositoryItemMemo = Nothing
    Me.xx_nomedoc.NTSRepositoryItemText = Nothing
    Me.xx_nomedoc.OptionsColumn.AllowEdit = False
    Me.xx_nomedoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_nomedoc.OptionsColumn.ReadOnly = True
    Me.xx_nomedoc.OptionsFilter.AllowFilter = False
    Me.xx_nomedoc.Visible = True
    Me.xx_nomedoc.VisibleIndex = 5
    '
    'ao_argom
    '
    Me.ao_argom.AppearanceCell.Options.UseBackColor = True
    Me.ao_argom.AppearanceCell.Options.UseTextOptions = True
    Me.ao_argom.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_argom.Caption = "Argomento"
    Me.ao_argom.Enabled = True
    Me.ao_argom.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_argom.Name = "ao_argom"
    Me.ao_argom.NTSRepositoryComboBox = Nothing
    Me.ao_argom.NTSRepositoryItemCheck = Nothing
    Me.ao_argom.NTSRepositoryItemMemo = Nothing
    Me.ao_argom.NTSRepositoryItemText = Nothing
    Me.ao_argom.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_argom.OptionsFilter.AllowFilter = False
    Me.ao_argom.Visible = True
    Me.ao_argom.VisibleIndex = 6
    '
    'ao_ubicaz
    '
    Me.ao_ubicaz.AppearanceCell.Options.UseBackColor = True
    Me.ao_ubicaz.AppearanceCell.Options.UseTextOptions = True
    Me.ao_ubicaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_ubicaz.Caption = "Ubicazione"
    Me.ao_ubicaz.Enabled = True
    Me.ao_ubicaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_ubicaz.Name = "ao_ubicaz"
    Me.ao_ubicaz.NTSRepositoryComboBox = Nothing
    Me.ao_ubicaz.NTSRepositoryItemCheck = Nothing
    Me.ao_ubicaz.NTSRepositoryItemMemo = Nothing
    Me.ao_ubicaz.NTSRepositoryItemText = Nothing
    Me.ao_ubicaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_ubicaz.OptionsFilter.AllowFilter = False
    Me.ao_ubicaz.Visible = True
    Me.ao_ubicaz.VisibleIndex = 7
    '
    'ao_autore
    '
    Me.ao_autore.AppearanceCell.Options.UseBackColor = True
    Me.ao_autore.AppearanceCell.Options.UseTextOptions = True
    Me.ao_autore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_autore.Caption = "Autore"
    Me.ao_autore.Enabled = True
    Me.ao_autore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_autore.Name = "ao_autore"
    Me.ao_autore.NTSRepositoryComboBox = Nothing
    Me.ao_autore.NTSRepositoryItemCheck = Nothing
    Me.ao_autore.NTSRepositoryItemMemo = Nothing
    Me.ao_autore.NTSRepositoryItemText = Nothing
    Me.ao_autore.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_autore.OptionsFilter.AllowFilter = False
    Me.ao_autore.Visible = True
    Me.ao_autore.VisibleIndex = 8
    '
    'ao_redattore
    '
    Me.ao_redattore.AppearanceCell.Options.UseBackColor = True
    Me.ao_redattore.AppearanceCell.Options.UseTextOptions = True
    Me.ao_redattore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_redattore.Caption = "Redattore"
    Me.ao_redattore.Enabled = True
    Me.ao_redattore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_redattore.Name = "ao_redattore"
    Me.ao_redattore.NTSRepositoryComboBox = Nothing
    Me.ao_redattore.NTSRepositoryItemCheck = Nothing
    Me.ao_redattore.NTSRepositoryItemMemo = Nothing
    Me.ao_redattore.NTSRepositoryItemText = Nothing
    Me.ao_redattore.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_redattore.OptionsFilter.AllowFilter = False
    Me.ao_redattore.Visible = True
    Me.ao_redattore.VisibleIndex = 9
    '
    'ao_codice
    '
    Me.ao_codice.AppearanceCell.Options.UseBackColor = True
    Me.ao_codice.AppearanceCell.Options.UseTextOptions = True
    Me.ao_codice.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_codice.Caption = "Codice"
    Me.ao_codice.Enabled = False
    Me.ao_codice.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_codice.Name = "ao_codice"
    Me.ao_codice.NTSRepositoryComboBox = Nothing
    Me.ao_codice.NTSRepositoryItemCheck = Nothing
    Me.ao_codice.NTSRepositoryItemMemo = Nothing
    Me.ao_codice.NTSRepositoryItemText = Nothing
    Me.ao_codice.OptionsColumn.AllowEdit = False
    Me.ao_codice.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_codice.OptionsColumn.ReadOnly = True
    Me.ao_codice.OptionsFilter.AllowFilter = False
    Me.ao_codice.Visible = True
    Me.ao_codice.VisibleIndex = 10
    '
    'xx_descr1
    '
    Me.xx_descr1.AppearanceCell.Options.UseBackColor = True
    Me.xx_descr1.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descr1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descr1.Caption = "Descrizione cliente"
    Me.xx_descr1.Enabled = False
    Me.xx_descr1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descr1.Name = "xx_descr1"
    Me.xx_descr1.NTSRepositoryComboBox = Nothing
    Me.xx_descr1.NTSRepositoryItemCheck = Nothing
    Me.xx_descr1.NTSRepositoryItemMemo = Nothing
    Me.xx_descr1.NTSRepositoryItemText = Nothing
    Me.xx_descr1.OptionsColumn.AllowEdit = False
    Me.xx_descr1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descr1.OptionsColumn.ReadOnly = True
    Me.xx_descr1.OptionsFilter.AllowFilter = False
    Me.xx_descr1.Visible = True
    Me.xx_descr1.VisibleIndex = 11
    '
    'ao_controp
    '
    Me.ao_controp.AppearanceCell.Options.UseBackColor = True
    Me.ao_controp.AppearanceCell.Options.UseTextOptions = True
    Me.ao_controp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_controp.Caption = "Controp."
    Me.ao_controp.Enabled = False
    Me.ao_controp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_controp.Name = "ao_controp"
    Me.ao_controp.NTSRepositoryComboBox = Nothing
    Me.ao_controp.NTSRepositoryItemCheck = Nothing
    Me.ao_controp.NTSRepositoryItemMemo = Nothing
    Me.ao_controp.NTSRepositoryItemText = Nothing
    Me.ao_controp.OptionsColumn.AllowEdit = False
    Me.ao_controp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_controp.OptionsColumn.ReadOnly = True
    Me.ao_controp.OptionsFilter.AllowFilter = False
    Me.ao_controp.Visible = True
    Me.ao_controp.VisibleIndex = 12
    '
    'xx_descovg
    '
    Me.xx_descovg.AppearanceCell.Options.UseBackColor = True
    Me.xx_descovg.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descovg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descovg.Caption = "Descrizione controp."
    Me.xx_descovg.Enabled = False
    Me.xx_descovg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descovg.Name = "xx_descovg"
    Me.xx_descovg.NTSRepositoryComboBox = Nothing
    Me.xx_descovg.NTSRepositoryItemCheck = Nothing
    Me.xx_descovg.NTSRepositoryItemMemo = Nothing
    Me.xx_descovg.NTSRepositoryItemText = Nothing
    Me.xx_descovg.OptionsColumn.AllowEdit = False
    Me.xx_descovg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descovg.OptionsColumn.ReadOnly = True
    Me.xx_descovg.OptionsFilter.AllowFilter = False
    Me.xx_descovg.Visible = True
    Me.xx_descovg.VisibleIndex = 13
    '
    'ao_strcod
    '
    Me.ao_strcod.AppearanceCell.Options.UseBackColor = True
    Me.ao_strcod.AppearanceCell.Options.UseTextOptions = True
    Me.ao_strcod.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_strcod.Caption = "Articolo D.B."
    Me.ao_strcod.Enabled = False
    Me.ao_strcod.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_strcod.Name = "ao_strcod"
    Me.ao_strcod.NTSRepositoryComboBox = Nothing
    Me.ao_strcod.NTSRepositoryItemCheck = Nothing
    Me.ao_strcod.NTSRepositoryItemMemo = Nothing
    Me.ao_strcod.NTSRepositoryItemText = Nothing
    Me.ao_strcod.OptionsColumn.AllowEdit = False
    Me.ao_strcod.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_strcod.OptionsColumn.ReadOnly = True
    Me.ao_strcod.OptionsFilter.AllowFilter = False
    Me.ao_strcod.Visible = True
    Me.ao_strcod.VisibleIndex = 14
    '
    'xx_descr
    '
    Me.xx_descr.AppearanceCell.Options.UseBackColor = True
    Me.xx_descr.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descr.Caption = "Descrizione articolo"
    Me.xx_descr.Enabled = False
    Me.xx_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descr.Name = "xx_descr"
    Me.xx_descr.NTSRepositoryComboBox = Nothing
    Me.xx_descr.NTSRepositoryItemCheck = Nothing
    Me.xx_descr.NTSRepositoryItemMemo = Nothing
    Me.xx_descr.NTSRepositoryItemText = Nothing
    Me.xx_descr.OptionsColumn.AllowEdit = False
    Me.xx_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descr.OptionsColumn.ReadOnly = True
    Me.xx_descr.OptionsFilter.AllowFilter = False
    Me.xx_descr.Visible = True
    Me.xx_descr.VisibleIndex = 15
    '
    'ao_tipodoc
    '
    Me.ao_tipodoc.AppearanceCell.Options.UseBackColor = True
    Me.ao_tipodoc.AppearanceCell.Options.UseTextOptions = True
    Me.ao_tipodoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_tipodoc.Caption = "Tipo doc."
    Me.ao_tipodoc.Enabled = False
    Me.ao_tipodoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_tipodoc.Name = "ao_tipodoc"
    Me.ao_tipodoc.NTSRepositoryComboBox = Nothing
    Me.ao_tipodoc.NTSRepositoryItemCheck = Nothing
    Me.ao_tipodoc.NTSRepositoryItemMemo = Nothing
    Me.ao_tipodoc.NTSRepositoryItemText = Nothing
    Me.ao_tipodoc.OptionsColumn.AllowEdit = False
    Me.ao_tipodoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_tipodoc.OptionsColumn.ReadOnly = True
    Me.ao_tipodoc.OptionsFilter.AllowFilter = False
    Me.ao_tipodoc.Visible = True
    Me.ao_tipodoc.VisibleIndex = 16
    '
    'ao_annodoc
    '
    Me.ao_annodoc.AppearanceCell.Options.UseBackColor = True
    Me.ao_annodoc.AppearanceCell.Options.UseTextOptions = True
    Me.ao_annodoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_annodoc.Caption = "Anno doc."
    Me.ao_annodoc.Enabled = False
    Me.ao_annodoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_annodoc.Name = "ao_annodoc"
    Me.ao_annodoc.NTSRepositoryComboBox = Nothing
    Me.ao_annodoc.NTSRepositoryItemCheck = Nothing
    Me.ao_annodoc.NTSRepositoryItemMemo = Nothing
    Me.ao_annodoc.NTSRepositoryItemText = Nothing
    Me.ao_annodoc.OptionsColumn.AllowEdit = False
    Me.ao_annodoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_annodoc.OptionsColumn.ReadOnly = True
    Me.ao_annodoc.OptionsFilter.AllowFilter = False
    Me.ao_annodoc.Visible = True
    Me.ao_annodoc.VisibleIndex = 17
    '
    'ao_seriedoc
    '
    Me.ao_seriedoc.AppearanceCell.Options.UseBackColor = True
    Me.ao_seriedoc.AppearanceCell.Options.UseTextOptions = True
    Me.ao_seriedoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_seriedoc.Caption = "Serie doc."
    Me.ao_seriedoc.Enabled = False
    Me.ao_seriedoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_seriedoc.Name = "ao_seriedoc"
    Me.ao_seriedoc.NTSRepositoryComboBox = Nothing
    Me.ao_seriedoc.NTSRepositoryItemCheck = Nothing
    Me.ao_seriedoc.NTSRepositoryItemMemo = Nothing
    Me.ao_seriedoc.NTSRepositoryItemText = Nothing
    Me.ao_seriedoc.OptionsColumn.AllowEdit = False
    Me.ao_seriedoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_seriedoc.OptionsColumn.ReadOnly = True
    Me.ao_seriedoc.OptionsFilter.AllowFilter = False
    Me.ao_seriedoc.Visible = True
    Me.ao_seriedoc.VisibleIndex = 18
    '
    'ao_numdoc
    '
    Me.ao_numdoc.AppearanceCell.Options.UseBackColor = True
    Me.ao_numdoc.AppearanceCell.Options.UseTextOptions = True
    Me.ao_numdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_numdoc.Caption = "Num. doc."
    Me.ao_numdoc.Enabled = False
    Me.ao_numdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_numdoc.Name = "ao_numdoc"
    Me.ao_numdoc.NTSRepositoryComboBox = Nothing
    Me.ao_numdoc.NTSRepositoryItemCheck = Nothing
    Me.ao_numdoc.NTSRepositoryItemMemo = Nothing
    Me.ao_numdoc.NTSRepositoryItemText = Nothing
    Me.ao_numdoc.OptionsColumn.AllowEdit = False
    Me.ao_numdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_numdoc.OptionsColumn.ReadOnly = True
    Me.ao_numdoc.OptionsFilter.AllowFilter = False
    Me.ao_numdoc.Visible = True
    Me.ao_numdoc.VisibleIndex = 19
    '
    'ao_rigadoc
    '
    Me.ao_rigadoc.AppearanceCell.Options.UseBackColor = True
    Me.ao_rigadoc.AppearanceCell.Options.UseTextOptions = True
    Me.ao_rigadoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_rigadoc.Caption = "Riga doc."
    Me.ao_rigadoc.Enabled = False
    Me.ao_rigadoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_rigadoc.Name = "ao_rigadoc"
    Me.ao_rigadoc.NTSRepositoryComboBox = Nothing
    Me.ao_rigadoc.NTSRepositoryItemCheck = Nothing
    Me.ao_rigadoc.NTSRepositoryItemMemo = Nothing
    Me.ao_rigadoc.NTSRepositoryItemText = Nothing
    Me.ao_rigadoc.OptionsColumn.AllowEdit = False
    Me.ao_rigadoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_rigadoc.OptionsColumn.ReadOnly = True
    Me.ao_rigadoc.OptionsFilter.AllowFilter = False
    Me.ao_rigadoc.Visible = True
    Me.ao_rigadoc.VisibleIndex = 20
    '
    'ao_commeca
    '
    Me.ao_commeca.AppearanceCell.Options.UseBackColor = True
    Me.ao_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.ao_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_commeca.Caption = "Commessa"
    Me.ao_commeca.Enabled = False
    Me.ao_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_commeca.Name = "ao_commeca"
    Me.ao_commeca.NTSRepositoryComboBox = Nothing
    Me.ao_commeca.NTSRepositoryItemCheck = Nothing
    Me.ao_commeca.NTSRepositoryItemMemo = Nothing
    Me.ao_commeca.NTSRepositoryItemText = Nothing
    Me.ao_commeca.OptionsColumn.AllowEdit = False
    Me.ao_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_commeca.OptionsColumn.ReadOnly = True
    Me.ao_commeca.OptionsFilter.AllowFilter = False
    Me.ao_commeca.Visible = True
    Me.ao_commeca.VisibleIndex = 21
    '
    'xx_descr1_commess
    '
    Me.xx_descr1_commess.AppearanceCell.Options.UseBackColor = True
    Me.xx_descr1_commess.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descr1_commess.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descr1_commess.Caption = "Descrizione commessa"
    Me.xx_descr1_commess.Enabled = False
    Me.xx_descr1_commess.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descr1_commess.Name = "xx_descr1_commess"
    Me.xx_descr1_commess.NTSRepositoryComboBox = Nothing
    Me.xx_descr1_commess.NTSRepositoryItemCheck = Nothing
    Me.xx_descr1_commess.NTSRepositoryItemMemo = Nothing
    Me.xx_descr1_commess.NTSRepositoryItemText = Nothing
    Me.xx_descr1_commess.OptionsColumn.AllowEdit = False
    Me.xx_descr1_commess.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descr1_commess.OptionsColumn.ReadOnly = True
    Me.xx_descr1_commess.OptionsFilter.AllowFilter = False
    Me.xx_descr1_commess.Visible = True
    Me.xx_descr1_commess.VisibleIndex = 22
    '
    'xx_lottox
    '
    Me.xx_lottox.AppearanceCell.Options.UseBackColor = True
    Me.xx_lottox.AppearanceCell.Options.UseTextOptions = True
    Me.xx_lottox.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_lottox.Caption = "Lotto"
    Me.xx_lottox.Enabled = True
    Me.xx_lottox.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_lottox.Name = "xx_lottox"
    Me.xx_lottox.NTSRepositoryComboBox = Nothing
    Me.xx_lottox.NTSRepositoryItemCheck = Nothing
    Me.xx_lottox.NTSRepositoryItemMemo = Nothing
    Me.xx_lottox.NTSRepositoryItemText = Nothing
    Me.xx_lottox.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_lottox.OptionsFilter.AllowFilter = False
    Me.xx_lottox.Visible = True
    Me.xx_lottox.VisibleIndex = 23
    '
    'ao_matric
    '
    Me.ao_matric.AppearanceCell.Options.UseBackColor = True
    Me.ao_matric.AppearanceCell.Options.UseTextOptions = True
    Me.ao_matric.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_matric.Caption = "Matricola"
    Me.ao_matric.Enabled = True
    Me.ao_matric.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_matric.Name = "ao_matric"
    Me.ao_matric.NTSRepositoryComboBox = Nothing
    Me.ao_matric.NTSRepositoryItemCheck = Nothing
    Me.ao_matric.NTSRepositoryItemMemo = Nothing
    Me.ao_matric.NTSRepositoryItemText = Nothing
    Me.ao_matric.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_matric.OptionsFilter.AllowFilter = False
    Me.ao_matric.Visible = True
    Me.ao_matric.VisibleIndex = 24
    '
    'ao_ultagg
    '
    Me.ao_ultagg.AppearanceCell.Options.UseBackColor = True
    Me.ao_ultagg.AppearanceCell.Options.UseTextOptions = True
    Me.ao_ultagg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_ultagg.Caption = "Ultimo agg."
    Me.ao_ultagg.Enabled = False
    Me.ao_ultagg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_ultagg.Name = "ao_ultagg"
    Me.ao_ultagg.NTSRepositoryComboBox = Nothing
    Me.ao_ultagg.NTSRepositoryItemCheck = Nothing
    Me.ao_ultagg.NTSRepositoryItemMemo = Nothing
    Me.ao_ultagg.NTSRepositoryItemText = Nothing
    Me.ao_ultagg.OptionsColumn.AllowEdit = False
    Me.ao_ultagg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_ultagg.OptionsColumn.ReadOnly = True
    Me.ao_ultagg.OptionsFilter.AllowFilter = False
    Me.ao_ultagg.Visible = True
    Me.ao_ultagg.VisibleIndex = 25
    '
    'ao_datins
    '
    Me.ao_datins.AppearanceCell.Options.UseBackColor = True
    Me.ao_datins.AppearanceCell.Options.UseTextOptions = True
    Me.ao_datins.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_datins.Caption = "Data inserimento"
    Me.ao_datins.Enabled = False
    Me.ao_datins.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_datins.Name = "ao_datins"
    Me.ao_datins.NTSRepositoryComboBox = Nothing
    Me.ao_datins.NTSRepositoryItemCheck = Nothing
    Me.ao_datins.NTSRepositoryItemMemo = Nothing
    Me.ao_datins.NTSRepositoryItemText = Nothing
    Me.ao_datins.OptionsColumn.AllowEdit = False
    Me.ao_datins.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_datins.OptionsColumn.ReadOnly = True
    Me.ao_datins.OptionsFilter.AllowFilter = False
    Me.ao_datins.Visible = True
    Me.ao_datins.VisibleIndex = 26
    '
    'ao_codlead
    '
    Me.ao_codlead.AppearanceCell.Options.UseBackColor = True
    Me.ao_codlead.AppearanceCell.Options.UseTextOptions = True
    Me.ao_codlead.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_codlead.Caption = "Codice lead"
    Me.ao_codlead.Enabled = False
    Me.ao_codlead.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_codlead.Name = "ao_codlead"
    Me.ao_codlead.NTSRepositoryComboBox = Nothing
    Me.ao_codlead.NTSRepositoryItemCheck = Nothing
    Me.ao_codlead.NTSRepositoryItemMemo = Nothing
    Me.ao_codlead.NTSRepositoryItemText = Nothing
    Me.ao_codlead.OptionsColumn.AllowEdit = False
    Me.ao_codlead.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_codlead.OptionsColumn.ReadOnly = True
    Me.ao_codlead.OptionsFilter.AllowFilter = False
    Me.ao_codlead.Visible = True
    Me.ao_codlead.VisibleIndex = 27
    '
    'xx_descr1_lead
    '
    Me.xx_descr1_lead.AppearanceCell.Options.UseBackColor = True
    Me.xx_descr1_lead.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descr1_lead.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descr1_lead.Caption = "Descrizione lead"
    Me.xx_descr1_lead.Enabled = False
    Me.xx_descr1_lead.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descr1_lead.Name = "xx_descr1_lead"
    Me.xx_descr1_lead.NTSRepositoryComboBox = Nothing
    Me.xx_descr1_lead.NTSRepositoryItemCheck = Nothing
    Me.xx_descr1_lead.NTSRepositoryItemMemo = Nothing
    Me.xx_descr1_lead.NTSRepositoryItemText = Nothing
    Me.xx_descr1_lead.OptionsColumn.AllowEdit = False
    Me.xx_descr1_lead.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descr1_lead.OptionsColumn.ReadOnly = True
    Me.xx_descr1_lead.OptionsFilter.AllowFilter = False
    Me.xx_descr1_lead.Visible = True
    Me.xx_descr1_lead.VisibleIndex = 28
    '
    'ao_codoppo
    '
    Me.ao_codoppo.AppearanceCell.Options.UseBackColor = True
    Me.ao_codoppo.AppearanceCell.Options.UseTextOptions = True
    Me.ao_codoppo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_codoppo.Caption = "Opportunità"
    Me.ao_codoppo.Enabled = False
    Me.ao_codoppo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_codoppo.Name = "ao_codoppo"
    Me.ao_codoppo.NTSRepositoryComboBox = Nothing
    Me.ao_codoppo.NTSRepositoryItemCheck = Nothing
    Me.ao_codoppo.NTSRepositoryItemMemo = Nothing
    Me.ao_codoppo.NTSRepositoryItemText = Nothing
    Me.ao_codoppo.OptionsColumn.AllowEdit = False
    Me.ao_codoppo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_codoppo.OptionsColumn.ReadOnly = True
    Me.ao_codoppo.OptionsFilter.AllowFilter = False
    Me.ao_codoppo.Visible = True
    Me.ao_codoppo.VisibleIndex = 29
    '
    'xx_oggetto
    '
    Me.xx_oggetto.AppearanceCell.Options.UseBackColor = True
    Me.xx_oggetto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_oggetto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_oggetto.Caption = "Descrizione opp."
    Me.xx_oggetto.Enabled = False
    Me.xx_oggetto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_oggetto.Name = "xx_oggetto"
    Me.xx_oggetto.NTSRepositoryComboBox = Nothing
    Me.xx_oggetto.NTSRepositoryItemCheck = Nothing
    Me.xx_oggetto.NTSRepositoryItemMemo = Nothing
    Me.xx_oggetto.NTSRepositoryItemText = Nothing
    Me.xx_oggetto.OptionsColumn.AllowEdit = False
    Me.xx_oggetto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_oggetto.OptionsColumn.ReadOnly = True
    Me.xx_oggetto.OptionsFilter.AllowFilter = False
    Me.xx_oggetto.Visible = True
    Me.xx_oggetto.VisibleIndex = 30
    '
    'ao_codchia
    '
    Me.ao_codchia.AppearanceCell.Options.UseBackColor = True
    Me.ao_codchia.AppearanceCell.Options.UseTextOptions = True
    Me.ao_codchia.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_codchia.Caption = "Chiamata"
    Me.ao_codchia.Enabled = False
    Me.ao_codchia.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_codchia.Name = "ao_codchia"
    Me.ao_codchia.NTSRepositoryComboBox = Nothing
    Me.ao_codchia.NTSRepositoryItemCheck = Nothing
    Me.ao_codchia.NTSRepositoryItemMemo = Nothing
    Me.ao_codchia.NTSRepositoryItemText = Nothing
    Me.ao_codchia.OptionsColumn.AllowEdit = False
    Me.ao_codchia.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_codchia.OptionsColumn.ReadOnly = True
    Me.ao_codchia.OptionsFilter.AllowFilter = False
    Me.ao_codchia.Visible = True
    Me.ao_codchia.VisibleIndex = 31
    '
    'xx_oggetto_nnchiam
    '
    Me.xx_oggetto_nnchiam.AppearanceCell.Options.UseBackColor = True
    Me.xx_oggetto_nnchiam.AppearanceCell.Options.UseTextOptions = True
    Me.xx_oggetto_nnchiam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_oggetto_nnchiam.Caption = "Oggetto chiamata"
    Me.xx_oggetto_nnchiam.Enabled = False
    Me.xx_oggetto_nnchiam.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_oggetto_nnchiam.Name = "xx_oggetto_nnchiam"
    Me.xx_oggetto_nnchiam.NTSRepositoryComboBox = Nothing
    Me.xx_oggetto_nnchiam.NTSRepositoryItemCheck = Nothing
    Me.xx_oggetto_nnchiam.NTSRepositoryItemMemo = Nothing
    Me.xx_oggetto_nnchiam.NTSRepositoryItemText = Nothing
    Me.xx_oggetto_nnchiam.OptionsColumn.AllowEdit = False
    Me.xx_oggetto_nnchiam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_oggetto_nnchiam.OptionsColumn.ReadOnly = True
    Me.xx_oggetto_nnchiam.OptionsFilter.AllowFilter = False
    Me.xx_oggetto_nnchiam.Visible = True
    Me.xx_oggetto_nnchiam.VisibleIndex = 32
    '
    'ao_numcontr
    '
    Me.ao_numcontr.AppearanceCell.Options.UseBackColor = True
    Me.ao_numcontr.AppearanceCell.Options.UseTextOptions = True
    Me.ao_numcontr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_numcontr.Caption = "Contratto"
    Me.ao_numcontr.Enabled = False
    Me.ao_numcontr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_numcontr.Name = "ao_numcontr"
    Me.ao_numcontr.NTSRepositoryComboBox = Nothing
    Me.ao_numcontr.NTSRepositoryItemCheck = Nothing
    Me.ao_numcontr.NTSRepositoryItemMemo = Nothing
    Me.ao_numcontr.NTSRepositoryItemText = Nothing
    Me.ao_numcontr.OptionsColumn.AllowEdit = False
    Me.ao_numcontr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_numcontr.OptionsColumn.ReadOnly = True
    Me.ao_numcontr.OptionsFilter.AllowFilter = False
    Me.ao_numcontr.Visible = True
    Me.ao_numcontr.VisibleIndex = 33
    '
    'codditt
    '
    Me.codditt.AppearanceCell.Options.UseBackColor = True
    Me.codditt.AppearanceCell.Options.UseTextOptions = True
    Me.codditt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.codditt.Caption = "Codice ditta"
    Me.codditt.Enabled = True
    Me.codditt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.codditt.Name = "codditt"
    Me.codditt.NTSRepositoryComboBox = Nothing
    Me.codditt.NTSRepositoryItemCheck = Nothing
    Me.codditt.NTSRepositoryItemMemo = Nothing
    Me.codditt.NTSRepositoryItemText = Nothing
    Me.codditt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.codditt.OptionsFilter.AllowFilter = False
    '
    'ao_progress
    '
    Me.ao_progress.AppearanceCell.Options.UseBackColor = True
    Me.ao_progress.AppearanceCell.Options.UseTextOptions = True
    Me.ao_progress.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_progress.Caption = "Numero progressivo"
    Me.ao_progress.Enabled = True
    Me.ao_progress.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_progress.Name = "ao_progress"
    Me.ao_progress.NTSRepositoryComboBox = Nothing
    Me.ao_progress.NTSRepositoryItemCheck = Nothing
    Me.ao_progress.NTSRepositoryItemMemo = Nothing
    Me.ao_progress.NTSRepositoryItemText = Nothing
    Me.ao_progress.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_progress.OptionsFilter.AllowFilter = False
    '
    'ao_classe
    '
    Me.ao_classe.AppearanceCell.Options.UseBackColor = True
    Me.ao_classe.AppearanceCell.Options.UseTextOptions = True
    Me.ao_classe.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_classe.Caption = "Classe oggetto allegato"
    Me.ao_classe.Enabled = True
    Me.ao_classe.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_classe.Name = "ao_classe"
    Me.ao_classe.NTSRepositoryComboBox = Nothing
    Me.ao_classe.NTSRepositoryItemCheck = Nothing
    Me.ao_classe.NTSRepositoryItemMemo = Nothing
    Me.ao_classe.NTSRepositoryItemText = Nothing
    Me.ao_classe.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_classe.OptionsFilter.AllowFilter = False
    '
    'ao_classeole
    '
    Me.ao_classeole.AppearanceCell.Options.UseBackColor = True
    Me.ao_classeole.AppearanceCell.Options.UseTextOptions = True
    Me.ao_classeole.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_classeole.Caption = "Classe ole dell'oggetto allegato"
    Me.ao_classeole.Enabled = True
    Me.ao_classeole.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_classeole.Name = "ao_classeole"
    Me.ao_classeole.NTSRepositoryComboBox = Nothing
    Me.ao_classeole.NTSRepositoryItemCheck = Nothing
    Me.ao_classeole.NTSRepositoryItemMemo = Nothing
    Me.ao_classeole.NTSRepositoryItemText = Nothing
    Me.ao_classeole.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_classeole.OptionsFilter.AllowFilter = False
    '
    'ao_ole
    '
    Me.ao_ole.AppearanceCell.Options.UseBackColor = True
    Me.ao_ole.AppearanceCell.Options.UseTextOptions = True
    Me.ao_ole.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_ole.Caption = "Oggetto OLE"
    Me.ao_ole.Enabled = True
    Me.ao_ole.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_ole.Name = "ao_ole"
    Me.ao_ole.NTSRepositoryComboBox = Nothing
    Me.ao_ole.NTSRepositoryItemCheck = Nothing
    Me.ao_ole.NTSRepositoryItemMemo = Nothing
    Me.ao_ole.NTSRepositoryItemText = Nothing
    Me.ao_ole.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_ole.OptionsFilter.AllowFilter = False
    '
    'ao_progresl
    '
    Me.ao_progresl.AppearanceCell.Options.UseBackColor = True
    Me.ao_progresl.AppearanceCell.Options.UseTextOptions = True
    Me.ao_progresl.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ao_progresl.Caption = "Progressivo"
    Me.ao_progresl.Enabled = True
    Me.ao_progresl.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ao_progresl.Name = "ao_progresl"
    Me.ao_progresl.NTSRepositoryComboBox = Nothing
    Me.ao_progresl.NTSRepositoryItemCheck = Nothing
    Me.ao_progresl.NTSRepositoryItemMemo = Nothing
    Me.ao_progresl.NTSRepositoryItemText = Nothing
    Me.ao_progresl.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ao_progresl.OptionsFilter.AllowFilter = False
    '
    'xx_oggetto_off
    '
    Me.xx_oggetto_off.AppearanceCell.Options.UseBackColor = True
    Me.xx_oggetto_off.AppearanceCell.Options.UseTextOptions = True
    Me.xx_oggetto_off.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_oggetto_off.Caption = "Oggetto offerta"
    Me.xx_oggetto_off.Enabled = False
    Me.xx_oggetto_off.FieldName = "xx_oggetto_off"
    Me.xx_oggetto_off.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_oggetto_off.Name = "xx_oggetto_off"
    Me.xx_oggetto_off.NTSRepositoryComboBox = Nothing
    Me.xx_oggetto_off.NTSRepositoryItemCheck = Nothing
    Me.xx_oggetto_off.NTSRepositoryItemMemo = Nothing
    Me.xx_oggetto_off.NTSRepositoryItemText = Nothing
    Me.xx_oggetto_off.OptionsColumn.AllowEdit = False
    Me.xx_oggetto_off.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_oggetto_off.OptionsColumn.ReadOnly = True
    Me.xx_oggetto_off.OptionsFilter.AllowFilter = False
    Me.xx_oggetto_off.Visible = True
    Me.xx_oggetto_off.VisibleIndex = 34
    '
    'NtsGridView1
    '
    Me.NtsGridView1.ActiveFilterEnabled = False
    Me.NtsGridView1.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.NtsGridView1.Enabled = True
    Me.NtsGridView1.GridControl = Me.grAole
    Me.NtsGridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.NtsGridView1.MinRowHeight = 14
    Me.NtsGridView1.Name = "NtsGridView1"
    Me.NtsGridView1.NTSAllowDelete = True
    Me.NtsGridView1.NTSAllowInsert = True
    Me.NtsGridView1.NTSAllowUpdate = True
    Me.NtsGridView1.NTSMenuContext = Nothing
    Me.NtsGridView1.OptionsCustomization.AllowRowSizing = True
    Me.NtsGridView1.OptionsFilter.AllowFilterEditor = False
    Me.NtsGridView1.OptionsNavigation.EnterMoveNextColumn = True
    Me.NtsGridView1.OptionsNavigation.UseTabKey = False
    Me.NtsGridView1.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.NtsGridView1.OptionsView.ColumnAutoWidth = False
    Me.NtsGridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.NtsGridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.NtsGridView1.OptionsView.ShowGroupPanel = False
    Me.NtsGridView1.RowHeight = 14
    '
    'FRM__AOLE
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(792, 566)
    Me.Controls.Add(Me.grAole)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRM__AOLE"
    Me.Text = "OGGETTI OLE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grAole, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvAole, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsGridView1, System.ComponentModel.ISupportInitialize).EndInit()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__AOLE", "BE__AOLE", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 127791222114375000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleAole = CType(oTmp, CLE__AOLE)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__AOLE", strRemoteServer, strRemotePort)
    AddHandler oCleAole.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleAole.Init(oApp, oScript, oMenu.oCleComm, "ALLOLE", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\recnew.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\recagg.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\recdelete.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbRiparti.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbApriCon.GlyphPath = (oApp.ChildImageDir & "\open.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      Dim dttTipo As New DataTable()
      dttTipo.Columns.Add("cod", GetType(String))
      dttTipo.Columns.Add("val", GetType(String))
      dttTipo.Rows.Add(New Object() {"C", "Conto"})
      dttTipo.Rows.Add(New Object() {"A", "Articolo"})
      dttTipo.Rows.Add(New Object() {"D", "Distinta base"})
      dttTipo.Rows.Add(New Object() {"F", "FAQ"})
      dttTipo.Rows.Add(New Object() {"O", "Ordine"})
      dttTipo.Rows.Add(New Object() {"K", "Commessa"})
      dttTipo.Rows.Add(New Object() {"M", "Documento di magazzino"})
      dttTipo.Rows.Add(New Object() {"P", "Partite contabili"})
      dttTipo.Rows.Add(New Object() {"N", "Matricole"})
      dttTipo.Rows.Add(New Object() {"L", "Lotto"})
      dttTipo.Rows.Add(New Object() {"R", "Lead"})
      dttTipo.Rows.Add(New Object() {"J", "Opportunità"})
      dttTipo.Rows.Add(New Object() {"Y", "Chiamata"})
      dttTipo.Rows.Add(New Object() {"X", "Contratto"})
      dttTipo.Rows.Add(New Object() {"!", "Offerta"})
      dttTipo.Rows.Add(New Object() {"V", "Altro"})
      dttTipo.AcceptChanges()

      Dim dttTipoRK As New DataTable()
      dttTipoRK.Columns.Add("cod", GetType(String))
      dttTipoRK.Columns.Add("val", GetType(String))
      dttTipoRK.Rows.Add(New Object() {"O", "Oggetto OLE"})
      dttTipoRK.Rows.Add(New Object() {"F", "File documento"})
      dttTipoRK.AcceptChanges()

      Dim dttTipoDoc As New DataTable()
      dttTipoDoc.Columns.Add("cod", GetType(String))
      dttTipoDoc.Columns.Add("val", GetType(String))

      dttTipoDoc.Rows.Add(New Object() {"Z", "Bolla di movimentazine interna"})
      dttTipoDoc.Rows.Add(New Object() {"T", "Carico da produzione"})
      dttTipoDoc.Rows.Add(New Object() {"C", "Corrispettivo emeso"})
      dttTipoDoc.Rows.Add(New Object() {"B", "D.D.T. emesso"})
      dttTipoDoc.Rows.Add(New Object() {"M", "D.D.T. ricevuto"})
      dttTipoDoc.Rows.Add(New Object() {"S", "Fattura/ricevuta fiscale emessa"})
      dttTipoDoc.Rows.Add(New Object() {"P", "Fattura/ricevuta fiscale differita"})
      dttTipoDoc.Rows.Add(New Object() {"D", "Fattura differita emessa"})
      dttTipoDoc.Rows.Add(New Object() {"K", "Fattura differita ricevuta"})
      dttTipoDoc.Rows.Add(New Object() {"A", "Fattura immediata emessa"})
      dttTipoDoc.Rows.Add(New Object() {"L", "Fattura immediata ricevuta"})
      dttTipoDoc.Rows.Add(New Object() {"V", "Impegno cliente aperto"})
      dttTipoDoc.Rows.Add(New Object() {"R", "Impegno cliente"})
      dttTipoDoc.Rows.Add(New Object() {"Y", "Impegno di produzione"})
      dttTipoDoc.Rows.Add(New Object() {"X", "Impegno di trasferimento"})
      dttTipoDoc.Rows.Add(New Object() {"N", "Nota di accredito emessa"})
      dttTipoDoc.Rows.Add(New Object() {"J", "Nota di accredito ricevuta"})
      dttTipoDoc.Rows.Add(New Object() {"E", "Nota di addebito emessa"})
      dttTipoDoc.Rows.Add(New Object() {"$", "Ordine fornitore aperto"})
      dttTipoDoc.Rows.Add(New Object() {"H", "Ordine di produzione"})
      dttTipoDoc.Rows.Add(New Object() {"O", "Ordine fornitore"})
      dttTipoDoc.Rows.Add(New Object() {"Q", "Preventivo"})
      dttTipoDoc.Rows.Add(New Object() {"F", "Ricevuta fiscale emessa"})
      dttTipoDoc.Rows.Add(New Object() {"I", "Riemissione ricevuta fiscale"})
      dttTipoDoc.Rows.Add(New Object() {"U", "Scarico a produzione"})
      dttTipoDoc.Rows.Add(New Object() {"!", "Offerta"})
      dttTipoDoc.AcceptChanges()

      grvAole.NTSSetParam(oMenu, oApp.Tr(Me, 128617265404788262, "Griglia Oggetti OLE"))
      ao_tipo.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128617876919126350, "Tipo record"), dttTipo, "val", "cod")
      ao_tipork.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128617876919284225, "Tipo"), dttTipoRK, "val", "cod")
      ao_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617876919442100, "Descrizione"), 50, True)
      ao_descr2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617876919599975, "Descrizione 2"), 50, True)
      ao_cartella.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617876919757850, "Cartella"), 255, True)
      ao_nomedoc.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617876919915725, "Nome file"), 255, True)
      xx_nomedoc.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617876920073600, "Percorso fisico"), 255, True)
      ao_argom.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617876920705100, "Argomento"), 30, True)
      ao_ubicaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617876920862975, "Ubicazione"), 15, True)
      ao_autore.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617876921020850, "Autore"), 30, True)
      ao_redattore.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617876921178725, "Redattore"), 30, True)
      ao_codice.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128617876921336600, "Codice"), tabanagrac)
      xx_descr1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617876921494475, "Descrizione cliente"), 0, True)
      ao_controp.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128617876921652350, "Controp."), CLN__STD.tabcovg)
      xx_descovg.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617876921810225, "Descrizione controp."), 0, True)
      ao_strcod.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128617876921968100, "Articolo D.B."), tabartico, False)
      If oCleAole.bLottoNew Then
        xx_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129513246669085579, "Lotto"), 50, True)
      Else
        xx_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617876923388975, "Lotto"), 9, True)
      End If
      xx_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617876922125975, "Descrizione articolo"), 0, True)
      ao_tipodoc.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128617876922283850, "Tipo doc."), dttTipoDoc, "val", "cod")
      ao_annodoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617876922441725, "Anno doc."), "0", 4, 0, 9999)
      ao_seriedoc.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617876922599600, "Serie doc."), CLN__STD.SerieMaxLen, False)
      ao_numdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617876922757475, "Num. doc."), "0", 9, 0, 999999999)
      ao_rigadoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617876922915350, "Riga doc."), "#,##0.00", 15)
      ao_commeca.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128617876923073225, "Commessa"), tabcommess)
      xx_descr1_commess.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617876923231100, "Descrizione commessa"), 0, True)
      ao_matric.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617876923546850, "Matricola"), 18, False)
      ao_ultagg.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128617876923704725, "Ultimo agg."), False)
      ao_datins.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128617876923862600, "Data inserimento"), False)
      ao_codlead.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128617876924020475, "Codice lead"), tableads)
      xx_descr1_lead.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617876924178350, "Descrizione lead"), 0, True)
      ao_codoppo.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128617876924336225, "Opportunità"), tabopportun)
      xx_oggetto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617876924494100, "Descrizione opp."), 0, True)
      ao_codchia.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617876924651975, "Chiamata"), "0", 9, 0, 999999999)
      xx_oggetto_nnchiam.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617876924809850, "Oggetto chiamata"), 0, True)
      xx_oggetto_off.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617876924809851, "Oggetto offerta"), 0, True)
      ao_numcontr.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617876924967725, "Contratto"), "0", 9, 0, 999999999)
      codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617876925125600, "Codice ditta"), 12, False)
      ao_progress.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617876925283475, "Numero progressivo"), "0", 9, 0, 999999999)
      ao_classe.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617876925441350, "Classe oggetto allegato"), 50, False)
      ao_classeole.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128617876925599225, "Classe ole dell'oggetto allegato"), 50, False)
      ao_ole.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617876925757100, "Oggetto OLE"), "#,##0.00", 15)
      ao_progresl.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128617876925914975, "Progressivo"), "0", 9, 0, 999999999)

      ao_nomedoc.NTSSetParamZoom("..")
      ao_cartella.NTSSetParamZoom("..")
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

#Region "Form"

  Public Overridable Sub FRM__AOLE_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Dim strTipoOgg As String = ""
    Dim nInd As Integer = 0
    Dim strCodOgg As String = ""
    Dim dttTmp As New DataTable
    Dim strT() As String = Nothing

    Try
      oMenu.ValCodiceDb(DittaCorrente, DittaCorrente, "ANADITAC", "S", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then
        oCleAole.bLottoNew = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ac_lotti2) = "S", True, False))
      End If
      dttTmp.Clear()

      'predisposizione controlli
      InitControls()
      oCleAole.strPath = oMenu.GetSettingBus("BS--AOLE", "OPZIONI", ".", "Cartella_per_documenti", oApp.OfficeDir, " ", oApp.OfficeDir)

      '--------------------------------------------------------------------------------------------------------------
      '--- Determina la presenza o meno del modulo CRM
      '--------------------------------------------------------------------------------------------------------------
      oCleAole.bModuloCRM = False
      oCleAole.bModuloCS = False
      If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And CLN__STD.bsModExtCRM) Then oCleAole.bModuloCRM = True
      If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And CLN__STD.bsModSupWCR) Then oCleAole.bModuloCRM = True
      If CBool(oMenu.ModuliDittaDitt(DittaCorrente) And CLN__STD.bsModAS) Then oCleAole.bModuloCS = True
      '--------------------------------------------------------------------------------------------------------------
      '--- Determina se l'utente è un CRM User
      '--------------------------------------------------------------------------------------------------------------
      If oCleAole.bModuloCRM = True Then oCleAole.bIsCRMUser = oMenu.IsCrmUser(DittaCorrente, bAmm)
      '--------------------------------------------------------------------------------------------------------------

      If Not oCallParams Is Nothing Then
        If oCallParams.strParam <> "" Then
          If InStr(oCallParams.strParam, "§") > 0 Then
            'nuovo metodo §
            strT = oCallParams.strParam.Split("§"c)

            strTipoOgg = strT(1)
            oCleAole.lProgr = NTSCInt(strT(2))
            strCodOgg = strT(3)
            oCleAole.NuovoMetSettaVar(strT, strTipoOgg)
          Else
            strTipoOgg = Mid(oCallParams.strParam, 6, 1)
            nInd = InStr(8, oCallParams.strParam, ",")
            strCodOgg = oCallParams.strParam.Substring(7, Len(oCallParams.strParam) - 7)
            oCleAole.lProgr = NTSCInt(Mid(oCallParams.strParam, 114, 6))
            oCleAole.SettaVar(strCodOgg, strTipoOgg)
          End If
        End If
        oCleAole.strQuery = oCallParams.strPar1
        oCleAole.dttParam = CType(oCallParams.ctlPar1, DataTable)
        oCleAole.SettaCampi()
      Else
        oApp.MsgBoxErr(oApp.Tr(Me, 129246934077953208, "Programma non avviabile direttamente." & vbCrLf & _
                                                       "Accedere dal programma 'Oggetti OLE' o tramite le voci di menù presenti nei vari programmi di business."))
        Me.Close()
        Return
      End If

      'leggo dal DB i dati e collego il NTSBindingNavigator
      If Not oCleAole.Apri(DittaCorrente, dsAole) Then
        Me.Close()
        Return
      End If
      dcAole.DataSource = dsAole.Tables("ALLOLE")
      dsAole.AcceptChanges()

      grAole.DataSource = dcAole

      'applico le regole della Gctl
      GctlSetRoules()

      'se sono stati passati dei parametri, nascondo le colonne tipo, tanto non sono modificabili
      'eccetto se chiamato da bs--clie (C) o bscrglea (R) perchè in quel caso mostro sempre tutti
      'i record del conto e del lead (quindi per capirci qualche cosa le due colonne servono ...)
      'If oCleAole.strPrgParent = "BN__ROLE" Or oCleAole.strTipoOgg = "C" Or oCleAole.strTipoOgg = "R" Then
      '  GctlSetVisEnab(ao_tipo, True)
      '  GctlSetVisEnab(ao_tipork, True)
      'Else
      '  ao_tipo.Visible = False
      '  ao_tipork.Visible = False
      'End If

    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
    End Try
  End Sub

  Public Overridable Sub FRM__AOLE_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRM__AOLE_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcAole.Dispose()
      dsAole.Dispose()
    Catch ex As Exception
    End Try
  End Sub

#End Region

#Region "Eventi griglia"

  Public Overridable Sub grvAole_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvAole.GotFocus
    Try
      'se sono stati passati dei parametri, mi posiziono subito nella colonna 'descrizione'
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub grvAole_NTSBeforeRowUpdate(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvAole.NTSBeforeRowUpdate
    Try
      If Not Salva() Then
        'rimango sulla riga su cui sono!
        e.Allow = False
      End If
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub grvAole_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvAole.NTSFocusedRowChanged
    Try
      'se passo ad una riga già esistente blocco la colonna Tipo Record
      If NTSCInt(grvAole.GetFocusedRowCellValue(ao_progress).ToString.Trim) <> 0 Then
        DisabilitaCampi()
      Else
        If oCleAole.strPrgParent = "BN__ROLE" Then
          GctlSetVisEnab(ao_tipo, False)
        Else
          ao_tipo.Enabled = False   'per ora forzo il blocco: altrimenti qunado isnerisco qualche cosa da mgarti, clie, ecc mi darebbe la possibilità di impostare un tipo diverso ...
        End If
      End If
    Catch ex As Exception
      Dim strerr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub grvRighe_NTSCellValueChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvAole.NTSCellValueChanged
    Dim strColName As String = ""
    Dim strVal As String = ""
    Try
      strColName = grvAole.FocusedColumn.Name

      Select Case strColName.ToUpper
        Case "AO_TIPO"
          'se cambio il valore devo anche andare a ri-azzerare le colonne
          oCleAole.SvuotaCampi(dsAole)

          'ho cambiato il valore a mano: devo abilitare/disabilitare le colonne giuste per abilitare lo zoom
          With grvAole.NTSGetCurrentDataRow
            strVal = !ao_tipo.ToString
            DisabilitaCampi()
            Select Case strVal
              Case "C"
                ao_codice.Enabled = True
              Case "P"
                ao_controp.Enabled = True
              Case "V"
                AbilitaCampi()
              Case "A", "D"
                ao_strcod.Enabled = True
              Case "K"
                ao_commeca.Enabled = True
              Case "R"
                ao_codlead.Enabled = True
              Case "J"
                ao_codoppo.Enabled = True
              Case "Y"
                ao_codchia.Enabled = True
              Case "O"
                ao_tipodoc.Enabled = True
                ao_annodoc.Enabled = True
                ao_seriedoc.Enabled = True
                ao_numdoc.Enabled = True
              Case "M"
                ao_tipodoc.Enabled = True
                ao_annodoc.Enabled = True
                ao_seriedoc.Enabled = True
                ao_numdoc.Enabled = True
              Case "N"
                ao_matric.Enabled = True
              Case "L"
                xx_lottox.Enabled = True
              Case "X"
                ao_numcontr.Enabled = True
              Case "!"
                ao_tipodoc.Enabled = True
                ao_annodoc.Enabled = True
                ao_seriedoc.Enabled = True
                ao_numdoc.Enabled = True
              Case Else
                'non ci dovrebbero essere altri casi...
            End Select
          End With
      End Select

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub ao_cartella_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles ao_cartella.NTSZoomGest
    Try
      '--------------------------------------------------------------------------------------------------------------
      e.ZoomHandled = True
      '--------------------------------------------------------------------------------------------------------------
      If grvAole.GetFocusedRowCellValue(ao_cartella).ToString.Trim = "" Then Return
      If System.IO.Directory.Exists(grvAole.GetFocusedRowCellValue(ao_cartella).ToString.Trim) = False Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130155061838569946, "Attenzione!" & vbCrLf & _
          "La cartella indicata non esiste." & vbCrLf & _
          "Apertura non possibile."))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      ShellExecute(Me.Handle, "Open", "", "", grvAole.GetFocusedRowCellValue(ao_cartella).ToString.Trim, SW_SHOWNORMAL)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub ao_nomedoc_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles ao_nomedoc.NTSZoomGest
    Try
      '--------------------------------------------------------------------------------------------------------------
      e.ZoomHandled = True
      '--------------------------------------------------------------------------------------------------------------
      ApriFile(grvAole.GetFocusedRowCellValue(ao_nomedoc).ToString.Trim)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub DisabilitaCampi()
    ao_tipo.Enabled = False
    ao_tipork.Enabled = False
    ao_codice.Enabled = False
    ao_controp.Enabled = False
    ao_strcod.Enabled = False
    ao_tipodoc.Enabled = False
    ao_annodoc.Enabled = False
    ao_seriedoc.Enabled = False
    ao_numdoc.Enabled = False
    ao_rigadoc.Enabled = False
    ao_commeca.Enabled = False
    xx_lottox.Enabled = False
    ao_codlead.Enabled = False
    ao_codoppo.Enabled = False
    ao_codchia.Enabled = False
    ao_numcontr.Enabled = False
  End Sub

  Public Overridable Sub AbilitaCampi()
    ao_tipo.Enabled = True
    ao_tipork.Enabled = True
    ao_codice.Enabled = True
    ao_controp.Enabled = True
    ao_strcod.Enabled = True
    ao_tipodoc.Enabled = True
    ao_annodoc.Enabled = True
    ao_seriedoc.Enabled = True
    ao_numdoc.Enabled = True
    ao_rigadoc.Enabled = True
    ao_commeca.Enabled = True
    xx_lottox.Enabled = True
    ao_codlead.Enabled = True
    ao_codoppo.Enabled = True
    ao_codchia.Enabled = True
    ao_numcontr.Enabled = True
  End Sub

#End Region

#Region "Eventi toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvAole.NTSNuovo()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvAole.NTSRipristinaRigaCorrenteBefore(dcAole, True) Then Return
      oCleAole.Ripristina(dcAole.Position, dcAole.Filter)
      grvAole.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Try
      'da glea e/o da cliente devo verificare se posso o meno apportare modifiche al record, esattamente come se lo dovessi aprire ...
      If oCleAole.strTipoOgg = "R" Or oCleAole.strTipoOgg = "C" Then
        If ControllaAccessoDocumento(False) = False Then Return
      End If

      If Not grvAole.NTSDeleteRigaCorrente(dcAole, True) Then Return
      oCleAole.Salva(True)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim oParam As New CLE__PATB
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    Dim strNomeCol As String = ""
    Dim Processo As Process = New Process

    Try
      strNomeCol = grvAole.FocusedColumn.Name
      Select Case strNomeCol
        Case "ao_cartella"
          Dim strPath As String
          Try
            FolderBrowserDialog = New NTSFolderBrowserDialog
            With FolderBrowserDialog
              .Description = "Scegli cartella"
              .SelectedPath = "C:\"
              .oMenu = oMenu
              If .ShowDialog = Windows.Forms.DialogResult.OK Then
                strPath = FolderBrowserDialog.SelectedPath
                grvAole.SetFocusedValue(strPath)
                grvAole.NTSGetCurrentDataRow!ao_cartella = strPath
              End If
            End With
          Catch ex As Exception
            strErr = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
          End Try
        Case "ao_nomedoc"
          Dim strFilePath As String
          Dim ApriFileDialog As New NTSOpenFileDialog
          Try
            With ApriFileDialog
              .Title = "Sfoglia"
              .Filter = "Tutti i file|*.*"
              .InitialDirectory = "C:\"
              .oMenu = oMenu
              If ApriFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                strFilePath = .FileName
                grvAole.SetFocusedValue(strFilePath)
                grvAole.NTSGetCurrentDataRow!xx_nomedoc = strFilePath
              End If
            End With
          Catch ex As Exception
            strErr = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
          Finally
            If Not ApriFileDialog Is Nothing Then ApriFileDialog.Dispose()
            ApriFileDialog = Nothing
          End Try

        Case "ao_codice"
          SetFastZoom(NTSCStr(grvAole.EditingValue), oParam)
          NTSZOOM.strIn = NTSCStr(grvAole.EditingValue)
          oParam.bVisGriglia = True
          oParam.strTipo = "C"
          oParam.bTipoProposto = True
          NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvAole.EditingValue) Then grvAole.SetFocusedValue(NTSZOOM.strIn)

        Case Else
          NTSCallStandardZoom()
      End Select
    Catch ex As Exception
      strErr = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbApriCon_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApriCon.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case grvAole.FocusedColumn.Name
        Case "ao_cartella"
          If grvAole.NTSGetCurrentDataRow Is Nothing Then
            oApp.MsgBoxErr(oApp.Tr(Me, 130601914611298244, "Posizionsrsi prima sulla una riga di griglia"))
          Else
            NTSProcessStart("explorer.exe", NTSCStr(grvAole.NTSGetCurrentDataRow!ao_cartella))
          End If
        Case Else
          ApriFile(grvAole.GetFocusedRowCellValue(xx_nomedoc).ToString.Trim)
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    Try
      SendKeys.Send("{F1}")
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Try
      If Not Salva() Then Return
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()

      'da glea e/o da cliente devo verificare se posso o meno apportare modifiche al record, esattamente come se lo dovessi aprire ...
      If (oCleAole.strTipoOgg = "R" Or oCleAole.strTipoOgg = "C") And oCleAole.RecordIsChanged Then
        If ControllaAccessoDocumento(False) = False Then Return False
      End If

      Dim dRes As DialogResult
      dRes = grvAole.NTSSalvaRigaCorrente(dcAole, oCleAole.RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          If GctlControllaOutNotEqual() = False Then Return False
          If Not oCleAole.Salva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          oCleAole.Ripristina(dcAole.Position, dcAole.Filter)
        Case System.Windows.Forms.DialogResult.Cancel
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          'la riga non ha subito modifiche...
      End Select
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function ControllaAccessoDocumento(ByVal bOpenfile As Boolean) As Boolean
    Dim bOk As Boolean = False
    Dim bAccmod As Boolean = False
    Dim lCodlead As Integer = 0
    Dim nCodcageAccdito As Integer = 0
    Dim dttTmp As New DataTable

    Try
      If grvAole Is Nothing Then Return True
      If grvAole.NTSGetCurrentDataRow Is Nothing Then Return True
      '--------------------------------------------------------------------------------------------------------------
      '--- Se Non è attivo il modulo CRM e/o l'utente NON è CRMUser, ritorna True senza fare altri controlli
      '--------------------------------------------------------------------------------------------------------------
      If (oCleAole.bModuloCRM = False) Or (oCleAole.bIsCRMUser = False) Then Return True
      If oCleAole.bIsCRMUser = False Then Return True
      '--------------------------------------------------------------------------------------------------------------
      '--- Se il tipo record NON è "File/Documento", esce, restituendo "True"
      '--------------------------------------------------------------------------------------------------------------
      If grvAole.NTSGetCurrentDataRow!ao_tipork.ToString <> "F" Then Return True
      '--------------------------------------------------------------------------------------------------------------
      '--- Preleva il codice Lead di riga e, se non esiste, il lead collegato al conto (con destinazione a zero)
      '--------------------------------------------------------------------------------------------------------------
      lCodlead = NTSCInt(grvAole.NTSGetCurrentDataRow!ao_codlead)
      If NTSCStr(grvAole.NTSGetCurrentDataRow!ao_tipo) = "J" Then lCodlead = 1

      If NTSCInt(grvAole.NTSGetCurrentDataRow!ao_codice) <> 0 Then
        oMenu.ValCodiceDb(NTSCInt(grvAole.NTSGetCurrentDataRow!ao_codice).ToString, DittaCorrente, "ANAGRA", "N", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then
          If dttTmp.Rows(0)!an_tipo.ToString.ToUpper = "C" Then
            'per i clienti giro standard CRM
            If (lCodlead = 0) And (NTSCInt(grvAole.NTSGetCurrentDataRow!ao_codice) <> 0) Then
              lCodlead = oCleAole.CercaLeadDaContoRiga(NTSCInt(grvAole.NTSGetCurrentDataRow!ao_codice))
            End If
          End If
        End If
      End If

      '--------------------------------------------------------------------------------------------------------------
      '--- Se il codice Lead è zero, avvisa e non permette l'epertura
      '--------------------------------------------------------------------------------------------------------------
      If lCodlead = 0 Then
        If dttTmp.Rows(0)!an_tipo.ToString.ToUpper = "F" Then
          'per i fornitori devo vedere se l'utente CRM ha la possibilità divedere i dati di fornitori
          If bAmm = False Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129120822898134766, "Attenzione!" & vbCrLf & _
              "L'utente non è autorizzato a trattare documenti intestati a fornitori."))
            Return False
          Else
            'è un documento intestato a fornitore: se posso vedere i fornitori posso fare tutto
            Return True
          End If
        Else
          If bOpenfile Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129031136974862790, "Attenzione!" & vbCrLf & _
              "Il conto di riga NON è associato ad alcun Lead" & vbCrLf & _
              "Accesso non consentito a questo file/documento."))
          Else
            oApp.MsgBoxErr(oApp.Tr(Me, 129050023341728516, "Attenzione!" & vbCrLf & _
              "Il conto di riga NON è associato ad alcun Lead" & vbCrLf & _
              "Inserimento/modifica/cancellazione non consentiti."))
          End If
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se Documento/Ordine-Impegno, controlla se l'utente è agente
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCStr(grvAole.NTSGetCurrentDataRow!ao_tipo)
        Case "M", "O" : nCodcageAccdito = oCleAole.RitornaAgenteDaAccdito()
      End Select
      '--------------------------------------------------------------------------------------------------------------
      '--- A seconda del tipo record, chiama i diversi controlli sul CRM
      '--- N.B.: l'utente deve avere accesso al Lead sia in visibilità che modificabilità
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCStr(grvAole.NTSGetCurrentDataRow!ao_tipo)
        Case "C", "P", "R", "X", "Y" '--- Conto, Partite Contabili, Lead, Chiamata, Contratto
          bOk = oMenu.CercaAccessiCrmDaLead(DittaCorrente, lCodlead, bAccmod)
        Case "M", "O" '--- Documenti/Ordini-Impegni
          bOk = oCleAole.CercaAccessiDaDocumentiOrdini(NTSCStr(grvAole.NTSGetCurrentDataRow!ao_tipodoc), _
            NTSCInt(grvAole.NTSGetCurrentDataRow!ao_annodoc), NTSCStr(grvAole.NTSGetCurrentDataRow!ao_seriedoc), _
            NTSCInt(grvAole.NTSGetCurrentDataRow!ao_numdoc), nCodcageAccdito, _
            NTSCStr(grvAole.NTSGetCurrentDataRow!ao_tipo))
          If bOk = True Then bAccmod = True
        Case "!" '--- Offerta
          bOk = oMenu.CercaAccessiCrmDaTestoff(DittaCorrente, "!", NTSCInt(grvAole.NTSGetCurrentDataRow!ao_annodoc), _
            NTSCStr(grvAole.NTSGetCurrentDataRow!ao_seriedoc), NTSCInt(grvAole.NTSGetCurrentDataRow!ao_numdoc), _
            NTSCInt(grvAole.NTSGetCurrentDataRow!ao_rigadoc), bAccmod)
        Case "J" '--- Opportunità
          bOk = oMenu.CercaAccessiCrmDaOpportun(DittaCorrente, NTSCInt(grvAole.NTSGetCurrentDataRow!ao_codoppo), bAccmod)
      End Select
      '--------------------------------------------------------------------------------------------------------------
      If (bOk = False) Or (bAccmod = False) Then
        If bOpenfile Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129031165849337252, "Attenzione!" & vbCrLf & _
            "Accesso non consentito a questo file/documento." & vbCrLf & _
            "Apertura non possibile."))
        Else
          oApp.MsgBoxErr(oApp.Tr(Me, 129050023711240234, "Attenzione!" & vbCrLf & _
            "Non si possiede l'autorizzazione in modifica della riga su cui si è posizionati." & vbCrLf & _
            "Inserimento/modifica/cancellazione non consentiti. ripristinare le modifiche apportate"))
        End If
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Sub ApriFile(ByVal strNomeFile As String)
    Dim RetValue As Integer = 0

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- In caso di modulo CRM attivo e utente CRM User, controlla l'accessibilità al documento che s'intende aprire
      '--------------------------------------------------------------------------------------------------------------
      If (oCleAole.strTipoOgg = "R") Or (oCleAole.strTipoOgg = "C") Then
        If ControllaAccessoDocumento(True) = False Then Return
      End If

      If System.IO.File.Exists(strNomeFile) = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130518705244141106, "File '|" & strNomeFile & "|' inesistente"))
        Return
      End If

      If CLN__STD.IsBis Then
        'faccio scaricare il file
        IS_ShowFileOnSbc(strNomeFile, False)
        Return
      End If

      '--------------------------------------------------------------------------------------------------------------
      RetValue = ShellExecute(Me.Handle, "Open", strNomeFile, "", "C:\", SW_SHOWNORMAL)
      If RetValue <= 32 Then
        'Si è verificato un errore
        Select Case RetValue
          Case SE_ERR_FNF
            oApp.MsgBoxErr(oApp.Tr(Me, 128563553287031250, "Impossibile aprire il 'Documento'." & vbCrLf & _
                   "File non trovato."))
          Case SE_ERR_ACCESSDENIED
            oApp.MsgBoxErr(oApp.Tr(Me, 128563553270468750, "Impossibile aprire il 'Documento'." & vbCrLf & _
                   "Accesso negato al file."))
          Case SE_ERR_NOASSOC
            oApp.MsgBoxErr(oApp.Tr(Me, 128563553243437500, "Impossibile aprire il 'Documento'." & vbCrLf & _
                   "Nessuna applicazione associata per questa estensione di file."))
          Case SE_ERR_ASSOCINCOMPLETE
            oApp.MsgBoxErr(oApp.Tr(Me, 128563553226718750, "Impossibile aprire il 'Documento'." & vbCrLf & _
                   "Associazione non valida o incompleta per questa estensione di file."))
          Case SE_ERR_OOM
            oApp.MsgBoxErr(oApp.Tr(Me, 128563553207500000, "Impossibile aprire il 'Documento'." & vbCrLf & _
                   "Memoria insufficiente per completare l'operazione."))
          Case SE_ERR_SHARE
            oApp.MsgBoxErr(oApp.Tr(Me, 128563553194062500, "Impossibile aprire il 'Documento'." & vbCrLf & _
                   "Si è verificata una violazione di condivisione."))
          Case ERROR_BAD_FORMAT
            oApp.MsgBoxErr(oApp.Tr(Me, 128563553181406250, "Impossibile aprire il 'Documento'." & vbCrLf & _
                   "File .EXE non valido o errore nell'immagine .EXE."))
          Case Else
            oApp.MsgBoxErr(oApp.Tr(Me, 128563553166093750, "Impossibile aprire il 'Documento'." & vbCrLf & _
                   "Errore sconosciuto."))
        End Select
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class
