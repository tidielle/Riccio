Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMCIANAC
  Public oCleSotc As CLE__SOTC
  Public oCallParams As CLE__CLDP
  Public dsAnac As DataSet
  Public dcAnac As BindingSource = New BindingSource()

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
  Public WithEvents grAnac As NTSInformatica.NTSGrid
  Public WithEvents grvAnac As NTSInformatica.NTSGridView
  Public WithEvents anc_contoca As NTSInformatica.NTSGridColumn
  Public WithEvents xx_contoca As NTSInformatica.NTSGridColumn
  Public WithEvents anc_codcena As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codcena As NTSInformatica.NTSGridColumn
  Public WithEvents anc_codcfam As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codcfam As NTSInformatica.NTSGridColumn
  Public WithEvents anc_perc As NTSInformatica.NTSGridColumn
  Public WithEvents anc_conto As NTSInformatica.NTSGridColumn
  Public WithEvents codditt As NTSInformatica.NTSGridColumn

  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMCIANAC))
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
    Me.grAnac = New NTSInformatica.NTSGrid
    Me.grvAnac = New NTSInformatica.NTSGridView
    Me.anc_contoca = New NTSInformatica.NTSGridColumn
    Me.xx_contoca = New NTSInformatica.NTSGridColumn
    Me.anc_codcena = New NTSInformatica.NTSGridColumn
    Me.xx_codcena = New NTSInformatica.NTSGridColumn
    Me.anc_codcfam = New NTSInformatica.NTSGridColumn
    Me.xx_codcfam = New NTSInformatica.NTSGridColumn
    Me.anc_perc = New NTSInformatica.NTSGridColumn
    Me.anc_conto = New NTSInformatica.NTSGridColumn
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbZoom2 = New NTSInformatica.NTSBarMenuItem
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grAnac, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvAnac, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(231, Byte), Integer))
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti, Me.tlbZoom2})
    Me.NtsBarManager1.MaxItemId = 19
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'grAnac
    '
    Me.grAnac.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grAnac.EmbeddedNavigator.Name = ""
    Me.grAnac.Location = New System.Drawing.Point(0, 26)
    Me.grAnac.MainView = Me.grvAnac
    Me.grAnac.Name = "grAnac"
    Me.grAnac.Size = New System.Drawing.Size(648, 416)
    Me.grAnac.TabIndex = 5
    Me.grAnac.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvAnac})
    '
    'grvAnac
    '
    Me.grvAnac.ActiveFilterEnabled = False
    Me.grvAnac.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.anc_contoca, Me.xx_contoca, Me.anc_codcena, Me.xx_codcena, Me.anc_codcfam, Me.xx_codcfam, Me.anc_perc, Me.anc_conto, Me.codditt})
    Me.grvAnac.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvAnac.Enabled = True
    Me.grvAnac.GridControl = Me.grAnac
    Me.grvAnac.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvAnac.Name = "grvAnac"
    Me.grvAnac.NTSAllowDelete = True
    Me.grvAnac.NTSAllowInsert = True
    Me.grvAnac.NTSAllowUpdate = True
    Me.grvAnac.NTSMenuContext = Nothing
    Me.grvAnac.OptionsCustomization.AllowRowSizing = True
    Me.grvAnac.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvAnac.OptionsNavigation.UseTabKey = False
    Me.grvAnac.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvAnac.OptionsView.ColumnAutoWidth = False
    Me.grvAnac.OptionsView.EnableAppearanceEvenRow = True
    Me.grvAnac.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvAnac.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvAnac.OptionsView.ShowGroupPanel = False
    Me.grvAnac.RowHeight = 16
    '
    'anc_contoca
    '
    Me.anc_contoca.AppearanceCell.Options.UseBackColor = True
    Me.anc_contoca.AppearanceCell.Options.UseTextOptions = True
    Me.anc_contoca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.anc_contoca.Caption = "Conto CA"
    Me.anc_contoca.Enabled = True
    Me.anc_contoca.FieldName = "anc_contoca"
    Me.anc_contoca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.anc_contoca.Name = "anc_contoca"
    Me.anc_contoca.NTSRepositoryComboBox = Nothing
    Me.anc_contoca.NTSRepositoryItemCheck = Nothing
    Me.anc_contoca.NTSRepositoryItemText = Nothing
    Me.anc_contoca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.anc_contoca.OptionsFilter.AllowFilter = False
    Me.anc_contoca.Visible = True
    Me.anc_contoca.VisibleIndex = 0
    Me.anc_contoca.Width = 70
    '
    'xx_contoca
    '
    Me.xx_contoca.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
    Me.xx_contoca.AppearanceCell.Options.UseBackColor = True
    Me.xx_contoca.AppearanceCell.Options.UseTextOptions = True
    Me.xx_contoca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_contoca.Caption = "Descrizione conto"
    Me.xx_contoca.Enabled = False
    Me.xx_contoca.FieldName = "xx_contoca"
    Me.xx_contoca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_contoca.Name = "xx_contoca"
    Me.xx_contoca.NTSRepositoryComboBox = Nothing
    Me.xx_contoca.NTSRepositoryItemCheck = Nothing
    Me.xx_contoca.NTSRepositoryItemText = Nothing
    Me.xx_contoca.OptionsColumn.AllowEdit = False
    Me.xx_contoca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_contoca.OptionsColumn.ReadOnly = True
    Me.xx_contoca.OptionsFilter.AllowFilter = False
    Me.xx_contoca.Visible = True
    Me.xx_contoca.VisibleIndex = 1
    Me.xx_contoca.Width = 70
    '
    'anc_codcena
    '
    Me.anc_codcena.AppearanceCell.Options.UseBackColor = True
    Me.anc_codcena.AppearanceCell.Options.UseTextOptions = True
    Me.anc_codcena.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.anc_codcena.Caption = "Centro"
    Me.anc_codcena.Enabled = True
    Me.anc_codcena.FieldName = "anc_codcena"
    Me.anc_codcena.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.anc_codcena.Name = "anc_codcena"
    Me.anc_codcena.NTSRepositoryComboBox = Nothing
    Me.anc_codcena.NTSRepositoryItemCheck = Nothing
    Me.anc_codcena.NTSRepositoryItemText = Nothing
    Me.anc_codcena.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.anc_codcena.OptionsFilter.AllowFilter = False
    Me.anc_codcena.Visible = True
    Me.anc_codcena.VisibleIndex = 2
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
    Me.xx_codcena.NTSRepositoryItemText = Nothing
    Me.xx_codcena.OptionsColumn.AllowEdit = False
    Me.xx_codcena.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codcena.OptionsColumn.ReadOnly = True
    Me.xx_codcena.OptionsFilter.AllowFilter = False
    Me.xx_codcena.Visible = True
    Me.xx_codcena.VisibleIndex = 3
    '
    'anc_codcfam
    '
    Me.anc_codcfam.AppearanceCell.Options.UseBackColor = True
    Me.anc_codcfam.AppearanceCell.Options.UseTextOptions = True
    Me.anc_codcfam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.anc_codcfam.Caption = "Linea"
    Me.anc_codcfam.Enabled = True
    Me.anc_codcfam.FieldName = "anc_codcfam"
    Me.anc_codcfam.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.anc_codcfam.Name = "anc_codcfam"
    Me.anc_codcfam.NTSRepositoryComboBox = Nothing
    Me.anc_codcfam.NTSRepositoryItemCheck = Nothing
    Me.anc_codcfam.NTSRepositoryItemText = Nothing
    Me.anc_codcfam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.anc_codcfam.OptionsFilter.AllowFilter = False
    Me.anc_codcfam.Visible = True
    Me.anc_codcfam.VisibleIndex = 4
    '
    'xx_codcfam
    '
    Me.xx_codcfam.AppearanceCell.Options.UseBackColor = True
    Me.xx_codcfam.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codcfam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codcfam.Caption = "Descr. linea"
    Me.xx_codcfam.Enabled = False
    Me.xx_codcfam.FieldName = "xx_codcfam"
    Me.xx_codcfam.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codcfam.Name = "xx_codcfam"
    Me.xx_codcfam.NTSRepositoryComboBox = Nothing
    Me.xx_codcfam.NTSRepositoryItemCheck = Nothing
    Me.xx_codcfam.NTSRepositoryItemText = Nothing
    Me.xx_codcfam.OptionsColumn.AllowEdit = False
    Me.xx_codcfam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codcfam.OptionsColumn.ReadOnly = True
    Me.xx_codcfam.OptionsFilter.AllowFilter = False
    Me.xx_codcfam.Visible = True
    Me.xx_codcfam.VisibleIndex = 5
    '
    'anc_perc
    '
    Me.anc_perc.AppearanceCell.Options.UseBackColor = True
    Me.anc_perc.AppearanceCell.Options.UseTextOptions = True
    Me.anc_perc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.anc_perc.Caption = "Percentuale"
    Me.anc_perc.Enabled = True
    Me.anc_perc.FieldName = "anc_perc"
    Me.anc_perc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.anc_perc.Name = "anc_perc"
    Me.anc_perc.NTSRepositoryComboBox = Nothing
    Me.anc_perc.NTSRepositoryItemCheck = Nothing
    Me.anc_perc.NTSRepositoryItemText = Nothing
    Me.anc_perc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.anc_perc.OptionsFilter.AllowFilter = False
    Me.anc_perc.Visible = True
    Me.anc_perc.VisibleIndex = 6
    '
    'anc_conto
    '
    Me.anc_conto.AppearanceCell.Options.UseBackColor = True
    Me.anc_conto.AppearanceCell.Options.UseTextOptions = True
    Me.anc_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.anc_conto.Caption = "ANC_CONTO"
    Me.anc_conto.Enabled = False
    Me.anc_conto.FieldName = "anc_conto"
    Me.anc_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.anc_conto.Name = "anc_conto"
    Me.anc_conto.NTSRepositoryComboBox = Nothing
    Me.anc_conto.NTSRepositoryItemCheck = Nothing
    Me.anc_conto.NTSRepositoryItemText = Nothing
    Me.anc_conto.OptionsColumn.AllowEdit = False
    Me.anc_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.anc_conto.OptionsColumn.ReadOnly = True
    Me.anc_conto.OptionsFilter.AllowFilter = False
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
    Me.codditt.NTSRepositoryItemText = Nothing
    Me.codditt.OptionsColumn.AllowEdit = False
    Me.codditt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.codditt.OptionsColumn.ReadOnly = True
    Me.codditt.OptionsFilter.AllowFilter = False
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 17
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom2)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbZoom2
    '
    Me.tlbZoom2.Caption = "Zoom centri/linee"
    Me.tlbZoom2.Id = 18
    Me.tlbZoom2.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F5))
    Me.tlbZoom2.Name = "tlbZoom2"
    Me.tlbZoom2.NTSIsCheckBox = False
    Me.tlbZoom2.Visible = True
    '
    'FRMCIANAC
    '
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.grAnac)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMCIANAC"
    Me.NTSLastControlFocussed = Me.grAnac
    Me.Text = "Ripartizioni in Contabilità Analitica"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grAnac, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvAnac, System.ComponentModel.ISupportInitialize).EndInit()
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

  Public Overridable Sub InitEntity(ByRef cleSotc As CLE__SOTC)
    oCleSotc = cleSotc
    AddHandler oCleSotc.RemoteEvent, AddressOf GestisciEventiEntity
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

      grvAnac.NTSSetParam(oMenu, oApp.Tr(Me, 129000663747146197, "Ripartizioni in Contabilità Analitica"))
      anc_contoca.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129000663849653413, "Conto CA"), tabanagca)
      xx_contoca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129000663886218487, "Descrizione conto"), 0, True)
      anc_codcena.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129000663922314778, "Centro"), tabcena)
      xx_codcena.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129000663962161333, "Descr. centro"), 0, True)
      anc_codcfam.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 129000663998257624, "Linea"), tabcfam, False)
      xx_codcfam.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129000664026228343, "Descr. linea"), 0, True)
      anc_perc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129000664052948974, "Percentuale"), "#,##0.00", 15, 0, 100)
      anc_conto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129000664074981775, "ANC_CONTO"), "0", 9, 0, 999999999)
      codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129000664114828330, "codditt"), 12, False)

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
  Public Overridable Sub FRMCIANAC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleSotc.AnacApri(DittaCorrente, dsAnac) Then Me.Close()
      dcAnac.DataSource = dsAnac.Tables("ANACENT")
      dsAnac.AcceptChanges()

      grAnac.DataSource = dcAnac

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMCIANAC_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub FRMCIANAC_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcAnac.Dispose()
      dsAnac.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvAnac.NTSNuovo()

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
      If Not grvAnac.NTSDeleteRigaCorrente(dcAnac, True) Then Return
      oCleSotc.AnacSalva(True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvAnac.NTSRipristinaRigaCorrenteBefore(dcAnac, True) Then Return
      oCleSotc.AnacRipristina(dcAnac.Position, dcAnac.Filter)
      grvAnac.NTSRipristinaRigaCorrenteAfter()
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

      If grvAnac.FocusedColumn.Name = "anc_codcena" Then

        If grvAnac.NTSGetCurrentDataRow() Is Nothing Then Exit Sub
        If NTSCInt(grvAnac.NTSGetCurrentDataRow()!anc_contoca) = 0 Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128584226509940328, "Indicare il Conto prima di passare alla selezione di un Centro di C.A."))
          Exit Sub
        End If

        oApp.MsgBoxInfo(oApp.Tr(Me, 129000664991280554, "Zoom attualmente non abilitato"))

      ElseIf grvAnac.FocusedColumn.Name = "anc_codcfam" Then

        If grvAnac.NTSGetCurrentDataRow() Is Nothing Then Exit Sub
        If NTSCInt(grvAnac.NTSGetCurrentDataRow()!anc_contoca) = 0 Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128584226557911227, "Indicare il Conto prima di passare alla selezione di una Linea."))
          Exit Sub
        End If

        oApp.MsgBoxInfo(oApp.Tr(Me, 129000665043157546, "Zoom attualmente non abilitato"))

      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        NTSCallStandardZoom()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom2_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom2.ItemClick
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
    If Not Salva() Then Return
    Me.Close()
  End Sub

#End Region

#Region "EventiGriglia"
  Public Overridable Sub grvAnac_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvAnac.NTSBeforeRowUpdate
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

  Public Overridable Sub grvAnac_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvAnac.NTSFocusedRowChanged
    'blocco le colonne non modificabili
    Dim dtrT As DataRow = Nothing
    Try
      If oCleSotc Is Nothing Then Return

      dtrT = grvAnac.NTSGetCurrentDataRow
      '------------------------------------
      'sono su una nuova riga
      If dtrT Is Nothing Then
        anc_contoca.Enabled = True
        anc_codcena.Enabled = True
        anc_codcfam.Enabled = True
        Return
      End If

      If NTSCInt(dtrT!anc_contoca) <> 0 Then
        anc_contoca.Enabled = False
        anc_codcena.Enabled = False
        anc_codcfam.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvAnac.NTSSalvaRigaCorrente(dcAnac, oCleSotc.AnacRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleSotc.AnacSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleSotc.AnacRipristina(dcAnac.Position, dcAnac.Filter)
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

End Class
