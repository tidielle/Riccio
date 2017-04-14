Partial Public Class FRMCIANA2
  Inherits FRM__CHIL

  <System.Diagnostics.DebuggerNonUserCode()> _
  Public Sub New()
    MyBase.New()
  End Sub

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing AndAlso components IsNot Nothing Then
      components.Dispose()
    End If
    MyBase.Dispose(disposing)
  End Sub

  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMCIANA2))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbDuplica = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grAna2 = New NTSInformatica.NTSGrid
    Me.grvAna2 = New NTSInformatica.NTSGridView
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.anc2_codpcca = New NTSInformatica.NTSGridColumn
    Me.anc2_contoca = New NTSInformatica.NTSGridColumn
    Me.xx_contoca = New NTSInformatica.NTSGridColumn
    Me.anc2_codcena = New NTSInformatica.NTSGridColumn
    Me.xx_codcena = New NTSInformatica.NTSGridColumn
    Me.anc2_codcfam = New NTSInformatica.NTSGridColumn
    Me.xx_codcfam = New NTSInformatica.NTSGridColumn
    Me.anc2_coddivi = New NTSInformatica.NTSGridColumn
    Me.xx_coddivi = New NTSInformatica.NTSGridColumn
    Me.anc2_codstab = New NTSInformatica.NTSGridColumn
    Me.xx_codstab = New NTSInformatica.NTSGridColumn
    Me.anc2_perc = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grAna2, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvAna2, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbRipristina, Me.tlbCancella, Me.tlbZoom, Me.tlbEsci, Me.tlbDuplica})
    Me.NtsBarManager1.MaxItemId = 8
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDuplica), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci, True)})
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
    'tlbDuplica
    '
    Me.tlbDuplica.Caption = "Duplica sulle altre ditte"
    Me.tlbDuplica.Glyph = CType(resources.GetObject("tlbDuplica.Glyph"), System.Drawing.Image)
    Me.tlbDuplica.Id = 7
    Me.tlbDuplica.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F2))
    Me.tlbDuplica.Name = "tlbDuplica"
    Me.tlbDuplica.Visible = True
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
    Me.tlbZoom.Id = 4
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 6
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'grAna2
    '
    Me.grAna2.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grAna2.EmbeddedNavigator.Name = ""
    Me.grAna2.Location = New System.Drawing.Point(0, 30)
    Me.grAna2.MainView = Me.grvAna2
    Me.grAna2.Name = "grAna2"
    Me.grAna2.Size = New System.Drawing.Size(947, 385)
    Me.grAna2.TabIndex = 6
    Me.grAna2.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvAna2})
    '
    'grvAna2
    '
    Me.grvAna2.ActiveFilterEnabled = False
    Me.grvAna2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.codditt, Me.anc2_codpcca, Me.anc2_contoca, Me.xx_contoca, Me.anc2_codcena, Me.xx_codcena, Me.anc2_codcfam, Me.xx_codcfam, Me.anc2_coddivi, Me.xx_coddivi, Me.anc2_codstab, Me.xx_codstab, Me.anc2_perc})
    Me.grvAna2.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvAna2.Enabled = True
    Me.grvAna2.GridControl = Me.grAna2
    Me.grvAna2.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvAna2.Name = "grvAna2"
    Me.grvAna2.NTSAllowDelete = True
    Me.grvAna2.NTSAllowInsert = True
    Me.grvAna2.NTSAllowUpdate = True
    Me.grvAna2.NTSMenuContext = Nothing
    Me.grvAna2.OptionsCustomization.AllowRowSizing = True
    Me.grvAna2.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvAna2.OptionsNavigation.UseTabKey = False
    Me.grvAna2.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvAna2.OptionsView.ColumnAutoWidth = False
    Me.grvAna2.OptionsView.EnableAppearanceEvenRow = True
    Me.grvAna2.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvAna2.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvAna2.OptionsView.ShowGroupPanel = False
    Me.grvAna2.RowHeight = 16
    '
    'codditt
    '
    Me.codditt.AppearanceCell.Options.UseBackColor = True
    Me.codditt.AppearanceCell.Options.UseTextOptions = True
    Me.codditt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.codditt.Caption = "Ditta"
    Me.codditt.Enabled = True
    Me.codditt.FieldName = "codditt"
    Me.codditt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.codditt.Name = "codditt"
    Me.codditt.NTSRepositoryComboBox = Nothing
    Me.codditt.NTSRepositoryItemCheck = Nothing
    Me.codditt.NTSRepositoryItemMemo = Nothing
    Me.codditt.NTSRepositoryItemText = Nothing
    Me.codditt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.codditt.OptionsFilter.AllowFilter = False
    Me.codditt.Visible = True
    Me.codditt.VisibleIndex = 0
    Me.codditt.Width = 70
    '
    'anc2_codpcca
    '
    Me.anc2_codpcca.AppearanceCell.Options.UseBackColor = True
    Me.anc2_codpcca.AppearanceCell.Options.UseTextOptions = True
    Me.anc2_codpcca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.anc2_codpcca.Caption = "PDC di CA"
    Me.anc2_codpcca.Enabled = False
    Me.anc2_codpcca.FieldName = "anc2_codpcca"
    Me.anc2_codpcca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.anc2_codpcca.Name = "anc2_codpcca"
    Me.anc2_codpcca.NTSRepositoryComboBox = Nothing
    Me.anc2_codpcca.NTSRepositoryItemCheck = Nothing
    Me.anc2_codpcca.NTSRepositoryItemMemo = Nothing
    Me.anc2_codpcca.NTSRepositoryItemText = Nothing
    Me.anc2_codpcca.OptionsColumn.AllowEdit = False
    Me.anc2_codpcca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.anc2_codpcca.OptionsColumn.ReadOnly = True
    Me.anc2_codpcca.OptionsFilter.AllowFilter = False
    Me.anc2_codpcca.Visible = True
    Me.anc2_codpcca.VisibleIndex = 1
    '
    'anc2_contoca
    '
    Me.anc2_contoca.AppearanceCell.Options.UseBackColor = True
    Me.anc2_contoca.AppearanceCell.Options.UseTextOptions = True
    Me.anc2_contoca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.anc2_contoca.Caption = "Conto CA"
    Me.anc2_contoca.Enabled = True
    Me.anc2_contoca.FieldName = "anc2_contoca"
    Me.anc2_contoca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.anc2_contoca.Name = "anc2_contoca"
    Me.anc2_contoca.NTSRepositoryComboBox = Nothing
    Me.anc2_contoca.NTSRepositoryItemCheck = Nothing
    Me.anc2_contoca.NTSRepositoryItemMemo = Nothing
    Me.anc2_contoca.NTSRepositoryItemText = Nothing
    Me.anc2_contoca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.anc2_contoca.OptionsFilter.AllowFilter = False
    Me.anc2_contoca.Visible = True
    Me.anc2_contoca.VisibleIndex = 2
    '
    'xx_contoca
    '
    Me.xx_contoca.AppearanceCell.Options.UseBackColor = True
    Me.xx_contoca.AppearanceCell.Options.UseTextOptions = True
    Me.xx_contoca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_contoca.Caption = "Descr. conto CA"
    Me.xx_contoca.CustomizationCaption = "Descr. conto CA"
    Me.xx_contoca.Enabled = False
    Me.xx_contoca.FieldName = "xx_contoca"
    Me.xx_contoca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_contoca.Name = "xx_contoca"
    Me.xx_contoca.NTSRepositoryComboBox = Nothing
    Me.xx_contoca.NTSRepositoryItemCheck = Nothing
    Me.xx_contoca.NTSRepositoryItemMemo = Nothing
    Me.xx_contoca.NTSRepositoryItemText = Nothing
    Me.xx_contoca.OptionsColumn.AllowEdit = False
    Me.xx_contoca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_contoca.OptionsColumn.ReadOnly = True
    Me.xx_contoca.OptionsFilter.AllowFilter = False
    Me.xx_contoca.Visible = True
    Me.xx_contoca.VisibleIndex = 3
    '
    'anc2_codcena
    '
    Me.anc2_codcena.AppearanceCell.Options.UseBackColor = True
    Me.anc2_codcena.AppearanceCell.Options.UseTextOptions = True
    Me.anc2_codcena.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.anc2_codcena.Caption = "Centro"
    Me.anc2_codcena.Enabled = True
    Me.anc2_codcena.FieldName = "anc2_codcena"
    Me.anc2_codcena.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.anc2_codcena.Name = "anc2_codcena"
    Me.anc2_codcena.NTSRepositoryComboBox = Nothing
    Me.anc2_codcena.NTSRepositoryItemCheck = Nothing
    Me.anc2_codcena.NTSRepositoryItemMemo = Nothing
    Me.anc2_codcena.NTSRepositoryItemText = Nothing
    Me.anc2_codcena.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.anc2_codcena.OptionsFilter.AllowFilter = False
    Me.anc2_codcena.Visible = True
    Me.anc2_codcena.VisibleIndex = 4
    '
    'xx_codcena
    '
    Me.xx_codcena.AppearanceCell.Options.UseBackColor = True
    Me.xx_codcena.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codcena.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codcena.Caption = "Descr. centro"
    Me.xx_codcena.CustomizationCaption = "Descr. centro"
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
    Me.xx_codcena.VisibleIndex = 5
    '
    'anc2_codcfam
    '
    Me.anc2_codcfam.AppearanceCell.Options.UseBackColor = True
    Me.anc2_codcfam.AppearanceCell.Options.UseTextOptions = True
    Me.anc2_codcfam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.anc2_codcfam.Caption = "Linea/famiglia"
    Me.anc2_codcfam.Enabled = True
    Me.anc2_codcfam.FieldName = "anc2_codcfam"
    Me.anc2_codcfam.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.anc2_codcfam.Name = "anc2_codcfam"
    Me.anc2_codcfam.NTSRepositoryComboBox = Nothing
    Me.anc2_codcfam.NTSRepositoryItemCheck = Nothing
    Me.anc2_codcfam.NTSRepositoryItemMemo = Nothing
    Me.anc2_codcfam.NTSRepositoryItemText = Nothing
    Me.anc2_codcfam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.anc2_codcfam.OptionsFilter.AllowFilter = False
    Me.anc2_codcfam.Visible = True
    Me.anc2_codcfam.VisibleIndex = 6
    '
    'xx_codcfam
    '
    Me.xx_codcfam.AppearanceCell.Options.UseBackColor = True
    Me.xx_codcfam.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codcfam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codcfam.Caption = "Descr. linea/famiglia"
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
    Me.xx_codcfam.VisibleIndex = 7
    '
    'anc2_coddivi
    '
    Me.anc2_coddivi.AppearanceCell.Options.UseBackColor = True
    Me.anc2_coddivi.AppearanceCell.Options.UseTextOptions = True
    Me.anc2_coddivi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.anc2_coddivi.Caption = "Divisione"
    Me.anc2_coddivi.Enabled = True
    Me.anc2_coddivi.FieldName = "anc2_coddivi"
    Me.anc2_coddivi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.anc2_coddivi.Name = "anc2_coddivi"
    Me.anc2_coddivi.NTSRepositoryComboBox = Nothing
    Me.anc2_coddivi.NTSRepositoryItemCheck = Nothing
    Me.anc2_coddivi.NTSRepositoryItemMemo = Nothing
    Me.anc2_coddivi.NTSRepositoryItemText = Nothing
    Me.anc2_coddivi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.anc2_coddivi.OptionsFilter.AllowFilter = False
    Me.anc2_coddivi.Visible = True
    Me.anc2_coddivi.VisibleIndex = 8
    '
    'xx_coddivi
    '
    Me.xx_coddivi.AppearanceCell.Options.UseBackColor = True
    Me.xx_coddivi.AppearanceCell.Options.UseTextOptions = True
    Me.xx_coddivi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_coddivi.Caption = "Descr. divisione"
    Me.xx_coddivi.Enabled = False
    Me.xx_coddivi.FieldName = "xx_coddivi"
    Me.xx_coddivi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_coddivi.Name = "xx_coddivi"
    Me.xx_coddivi.NTSRepositoryComboBox = Nothing
    Me.xx_coddivi.NTSRepositoryItemCheck = Nothing
    Me.xx_coddivi.NTSRepositoryItemMemo = Nothing
    Me.xx_coddivi.NTSRepositoryItemText = Nothing
    Me.xx_coddivi.OptionsColumn.AllowEdit = False
    Me.xx_coddivi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_coddivi.OptionsColumn.ReadOnly = True
    Me.xx_coddivi.OptionsFilter.AllowFilter = False
    Me.xx_coddivi.Visible = True
    Me.xx_coddivi.VisibleIndex = 9
    '
    'anc2_codstab
    '
    Me.anc2_codstab.AppearanceCell.Options.UseBackColor = True
    Me.anc2_codstab.AppearanceCell.Options.UseTextOptions = True
    Me.anc2_codstab.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.anc2_codstab.Caption = "Stab./filiale/neg."
    Me.anc2_codstab.Enabled = True
    Me.anc2_codstab.FieldName = "anc2_codstab"
    Me.anc2_codstab.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.anc2_codstab.Name = "anc2_codstab"
    Me.anc2_codstab.NTSRepositoryComboBox = Nothing
    Me.anc2_codstab.NTSRepositoryItemCheck = Nothing
    Me.anc2_codstab.NTSRepositoryItemMemo = Nothing
    Me.anc2_codstab.NTSRepositoryItemText = Nothing
    Me.anc2_codstab.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.anc2_codstab.OptionsFilter.AllowFilter = False
    Me.anc2_codstab.Visible = True
    Me.anc2_codstab.VisibleIndex = 10
    '
    'xx_codstab
    '
    Me.xx_codstab.AppearanceCell.Options.UseBackColor = True
    Me.xx_codstab.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codstab.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codstab.Caption = "Descr. stab./filiale/neg."
    Me.xx_codstab.Enabled = False
    Me.xx_codstab.FieldName = "xx_codstab"
    Me.xx_codstab.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codstab.Name = "xx_codstab"
    Me.xx_codstab.NTSRepositoryComboBox = Nothing
    Me.xx_codstab.NTSRepositoryItemCheck = Nothing
    Me.xx_codstab.NTSRepositoryItemMemo = Nothing
    Me.xx_codstab.NTSRepositoryItemText = Nothing
    Me.xx_codstab.OptionsColumn.AllowEdit = False
    Me.xx_codstab.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codstab.OptionsColumn.ReadOnly = True
    Me.xx_codstab.OptionsFilter.AllowFilter = False
    Me.xx_codstab.Visible = True
    Me.xx_codstab.VisibleIndex = 11
    '
    'anc2_perc
    '
    Me.anc2_perc.AppearanceCell.Options.UseBackColor = True
    Me.anc2_perc.AppearanceCell.Options.UseTextOptions = True
    Me.anc2_perc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.anc2_perc.Caption = "Percentuale"
    Me.anc2_perc.Enabled = True
    Me.anc2_perc.FieldName = "anc2_perc"
    Me.anc2_perc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.anc2_perc.Name = "anc2_perc"
    Me.anc2_perc.NTSRepositoryComboBox = Nothing
    Me.anc2_perc.NTSRepositoryItemCheck = Nothing
    Me.anc2_perc.NTSRepositoryItemMemo = Nothing
    Me.anc2_perc.NTSRepositoryItemText = Nothing
    Me.anc2_perc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.anc2_perc.OptionsFilter.AllowFilter = False
    Me.anc2_perc.Visible = True
    Me.anc2_perc.VisibleIndex = 12
    '
    'FRMCIANA2
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(947, 415)
    Me.Controls.Add(Me.grAna2)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.MinimizeBox = False
    Me.Name = "FRMCIANA2"
    Me.Text = "RIPARTIZIONI IN CONTABILITÀ ANALITICA"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grAna2, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvAna2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents grAna2 As NTSInformatica.NTSGrid
  Public WithEvents grvAna2 As NTSInformatica.NTSGridView
  Public WithEvents codditt As NTSInformatica.NTSGridColumn
  Public WithEvents anc2_contoca As NTSInformatica.NTSGridColumn
  Public WithEvents xx_contoca As NTSInformatica.NTSGridColumn
  Public WithEvents anc2_codcena As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codcena As NTSInformatica.NTSGridColumn
  Public WithEvents anc2_codcfam As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codcfam As NTSInformatica.NTSGridColumn
  Public WithEvents anc2_coddivi As NTSInformatica.NTSGridColumn
  Public WithEvents xx_coddivi As NTSInformatica.NTSGridColumn
  Public WithEvents anc2_codstab As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codstab As NTSInformatica.NTSGridColumn
  Public WithEvents anc2_perc As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn11 As NTSInformatica.NTSGridColumn
  Public WithEvents anc2_codpcca As NTSInformatica.NTSGridColumn
  Public WithEvents tlbDuplica As NTSInformatica.NTSBarButtonItem
End Class
