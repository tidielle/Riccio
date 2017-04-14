Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMCFDISD
  Public oCleAnaz As CLE__ANAZ
  Public oCallParams As CLE__CLDP
  Public dsDisd As DataSet
  Public dcDisd As BindingSource = New BindingSource()

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
  Public WithEvents grDisd As NTSInformatica.NTSGrid
  Public WithEvents grvDisd As NTSInformatica.NTSGridView
  Public WithEvents asd_numesco As NTSInformatica.NTSGridColumn
  Public WithEvents asd_numprogr As NTSInformatica.NTSGridColumn
  Public WithEvents asd_desunpro As NTSInformatica.NTSGridColumn
  Public WithEvents asd_progalm As NTSInformatica.NTSGridColumn
  Public WithEvents asd_coddest As NTSInformatica.NTSGridColumn
  Public WithEvents xx_coddest As NTSInformatica.NTSGridColumn
  Public WithEvents asd_ascodatt As NTSInformatica.NTSGridColumn
  Public WithEvents xx_ascodatt As NTSInformatica.NTSGridColumn

  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMCFDISD))
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
    Me.grDisd = New NTSInformatica.NTSGrid
    Me.grvDisd = New NTSInformatica.NTSGridView
    Me.xx_coddest = New NTSInformatica.NTSGridColumn
    Me.asd_ascodatt = New NTSInformatica.NTSGridColumn
    Me.xx_ascodatt = New NTSInformatica.NTSGridColumn
    Me.asd_numesco = New NTSInformatica.NTSGridColumn
    Me.asd_numprogr = New NTSInformatica.NTSGridColumn
    Me.asd_desunpro = New NTSInformatica.NTSGridColumn
    Me.asd_progalm = New NTSInformatica.NTSGridColumn
    Me.asd_coddest = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grDisd, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvDisd, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
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
    'grDisd
    '
    Me.grDisd.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grDisd.EmbeddedNavigator.Name = ""
    Me.grDisd.Location = New System.Drawing.Point(0, 26)
    Me.grDisd.MainView = Me.grvDisd
    Me.grDisd.Name = "grDisd"
    Me.grDisd.Size = New System.Drawing.Size(648, 416)
    Me.grDisd.TabIndex = 5
    Me.grDisd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDisd})
    '
    'grvDisd
    '
    Me.grvDisd.ActiveFilterEnabled = False
    '
    'xx_coddest
    '
    Me.xx_coddest.AppearanceCell.Options.UseBackColor = True
    Me.xx_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.xx_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_coddest.Caption = "Descr. sede anagr."
    Me.xx_coddest.Enabled = False
    Me.xx_coddest.FieldName = "xx_coddest"
    Me.xx_coddest.Name = "xx_coddest"
    Me.xx_coddest.NTSRepositoryComboBox = Nothing
    Me.xx_coddest.NTSRepositoryItemCheck = Nothing
    Me.xx_coddest.NTSRepositoryItemText = Nothing
    Me.xx_coddest.OptionsColumn.AllowEdit = False
    Me.xx_coddest.OptionsColumn.ReadOnly = True
    Me.xx_coddest.Visible = True
    Me.xx_coddest.VisibleIndex = 5
    '
    'asd_ascodatt
    '
    Me.asd_ascodatt.AppearanceCell.Options.UseBackColor = True
    Me.asd_ascodatt.AppearanceCell.Options.UseTextOptions = True
    Me.asd_ascodatt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.asd_ascodatt.Caption = "Cod. attività"
    Me.asd_ascodatt.Enabled = True
    Me.asd_ascodatt.FieldName = "asd_ascodatt"
    Me.asd_ascodatt.Name = "asd_ascodatt"
    Me.asd_ascodatt.NTSRepositoryComboBox = Nothing
    Me.asd_ascodatt.NTSRepositoryItemCheck = Nothing
    Me.asd_ascodatt.NTSRepositoryItemText = Nothing
    Me.asd_ascodatt.Visible = True
    Me.asd_ascodatt.VisibleIndex = 6
    '
    'xx_ascodatt
    '
    Me.xx_ascodatt.AppearanceCell.Options.UseBackColor = True
    Me.xx_ascodatt.AppearanceCell.Options.UseTextOptions = True
    Me.xx_ascodatt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_ascodatt.Caption = "Descr. attività"
    Me.xx_ascodatt.Enabled = False
    Me.xx_ascodatt.FieldName = "xx_ascodatt"
    Me.xx_ascodatt.Name = "xx_ascodatt"
    Me.xx_ascodatt.NTSRepositoryComboBox = Nothing
    Me.xx_ascodatt.NTSRepositoryItemCheck = Nothing
    Me.xx_ascodatt.NTSRepositoryItemText = Nothing
    Me.xx_ascodatt.OptionsColumn.AllowEdit = False
    Me.xx_ascodatt.OptionsColumn.ReadOnly = True
    Me.xx_ascodatt.Visible = True
    Me.xx_ascodatt.VisibleIndex = 7
    Me.grvDisd.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.asd_numesco, Me.asd_numprogr, Me.asd_desunpro, Me.asd_progalm, Me.asd_coddest, Me.xx_coddest, Me.asd_ascodatt, Me.xx_ascodatt})
    Me.grvDisd.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvDisd.Enabled = True
    Me.grvDisd.GridControl = Me.grDisd
    Me.grvDisd.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvDisd.Name = "grvDisd"
    Me.grvDisd.NTSAllowDelete = True
    Me.grvDisd.NTSAllowInsert = True
    Me.grvDisd.NTSAllowUpdate = True
    Me.grvDisd.NTSMenuContext = Nothing
    Me.grvDisd.OptionsCustomization.AllowRowSizing = True
    Me.grvDisd.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvDisd.OptionsNavigation.UseTabKey = False
    Me.grvDisd.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvDisd.OptionsView.ColumnAutoWidth = False
    Me.grvDisd.OptionsView.EnableAppearanceEvenRow = True
    Me.grvDisd.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvDisd.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvDisd.OptionsView.ShowGroupPanel = False
    Me.grvDisd.RowHeight = 16
    '
    'asd_numesco
    '
    Me.asd_numesco.AppearanceCell.Options.UseBackColor = True
    Me.asd_numesco.AppearanceCell.Options.UseTextOptions = True
    Me.asd_numesco.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.asd_numesco.Caption = "Esercizio"
    Me.asd_numesco.Enabled = True
    Me.asd_numesco.FieldName = "asd_numesco"
    Me.asd_numesco.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.asd_numesco.Name = "asd_numesco"
    Me.asd_numesco.NTSRepositoryComboBox = Nothing
    Me.asd_numesco.NTSRepositoryItemCheck = Nothing
    Me.asd_numesco.NTSRepositoryItemText = Nothing
    Me.asd_numesco.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.asd_numesco.OptionsFilter.AllowFilter = False
    Me.asd_numesco.Visible = True
    Me.asd_numesco.VisibleIndex = 0
    Me.asd_numesco.Width = 70
    '
    'asd_numprogr
    '
    Me.asd_numprogr.AppearanceCell.Options.UseBackColor = True
    Me.asd_numprogr.AppearanceCell.Options.UseTextOptions = True
    Me.asd_numprogr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.asd_numprogr.Caption = "Progressivo"
    Me.asd_numprogr.Enabled = True
    Me.asd_numprogr.FieldName = "asd_numprogr"
    Me.asd_numprogr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.asd_numprogr.Name = "asd_numprogr"
    Me.asd_numprogr.NTSRepositoryComboBox = Nothing
    Me.asd_numprogr.NTSRepositoryItemCheck = Nothing
    Me.asd_numprogr.NTSRepositoryItemText = Nothing
    Me.asd_numprogr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.asd_numprogr.OptionsFilter.AllowFilter = False
    Me.asd_numprogr.Visible = True
    Me.asd_numprogr.VisibleIndex = 1
    Me.asd_numprogr.Width = 70
    '
    'asd_desunpro
    '
    Me.asd_desunpro.AppearanceCell.Options.UseBackColor = True
    Me.asd_desunpro.AppearanceCell.Options.UseTextOptions = True
    Me.asd_desunpro.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.asd_desunpro.Caption = "Descr. progressivo"
    Me.asd_desunpro.Enabled = True
    Me.asd_desunpro.FieldName = "asd_desunpro"
    Me.asd_desunpro.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.asd_desunpro.Name = "asd_desunpro"
    Me.asd_desunpro.NTSRepositoryComboBox = Nothing
    Me.asd_desunpro.NTSRepositoryItemCheck = Nothing
    Me.asd_desunpro.NTSRepositoryItemText = Nothing
    Me.asd_desunpro.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.asd_desunpro.OptionsFilter.AllowFilter = False
    Me.asd_desunpro.Visible = True
    Me.asd_desunpro.VisibleIndex = 2
    Me.asd_desunpro.Width = 70
    '
    'asd_progalm
    '
    Me.asd_progalm.AppearanceCell.Options.UseBackColor = True
    Me.asd_progalm.AppearanceCell.Options.UseTextOptions = True
    Me.asd_progalm.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.asd_progalm.Caption = "Numero progr.rigo all.M"
    Me.asd_progalm.Enabled = True
    Me.asd_progalm.FieldName = "asd_progalm"
    Me.asd_progalm.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.asd_progalm.Name = "asd_progalm"
    Me.asd_progalm.NTSRepositoryComboBox = Nothing
    Me.asd_progalm.NTSRepositoryItemCheck = Nothing
    Me.asd_progalm.NTSRepositoryItemText = Nothing
    Me.asd_progalm.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.asd_progalm.OptionsFilter.AllowFilter = False
    Me.asd_progalm.Visible = True
    Me.asd_progalm.VisibleIndex = 3
    Me.asd_progalm.Width = 70
    '
    'asd_coddest
    '
    Me.asd_coddest.AppearanceCell.Options.UseBackColor = True
    Me.asd_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.asd_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.asd_coddest.Caption = "Sede anagrafica"
    Me.asd_coddest.Enabled = True
    Me.asd_coddest.FieldName = "asd_coddest"
    Me.asd_coddest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.asd_coddest.Name = "asd_coddest"
    Me.asd_coddest.NTSRepositoryComboBox = Nothing
    Me.asd_coddest.NTSRepositoryItemCheck = Nothing
    Me.asd_coddest.NTSRepositoryItemText = Nothing
    Me.asd_coddest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.asd_coddest.OptionsFilter.AllowFilter = False
    Me.asd_coddest.Visible = True
    Me.asd_coddest.VisibleIndex = 4
    Me.asd_coddest.Width = 70
    '
    'FRMCFDISD
    '
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.grDisd)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMCFDISD"
    Me.NTSLastControlFocussed = Me.grDisd
    Me.Text = "STUDI - DETTAGLI UNITA' PRODUTTIVE"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grDisd, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvDisd, System.ComponentModel.ISupportInitialize).EndInit()
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

  Public Overridable Sub InitEntity(ByRef cleAnaz As CLE__ANAZ)
    oCleAnaz = cleAnaz
    AddHandler oCleAnaz.RemoteEvent, AddressOf GestisciEventiEntity
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

      grvDisd.NTSSetParam(oMenu, "STUDI - DETTAGLI UNITA' PRODUTTIVE")
      asd_numesco.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128644966336875000, "Esercizio"), tabesco)
      asd_numprogr.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128647539192343750, "Progressivo"), "0", 4, 0, 9999)
      asd_desunpro.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128647539192500000, "Descr. progressivo"), 50, False)
      asd_progalm.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128647539192656250, "Numero progr.rigo all.M"), "0", 4, 0, 9999)
      asd_coddest.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128647539192812500, "Sede anagrafica"), tabdestdiv)
      xx_coddest.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128647539192968750, "Descr. sede anagr."), 0, True)
      asd_ascodatt.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128647539193125000, "Cod. attività"), tabatec, True)
      xx_ascodatt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128647539193281250, "Descr. attività"), 0, True)

      asd_numesco.NTSForzaVisZoom = False

      asd_numesco.NTSSetRichiesto()
      asd_numprogr.NTSSetRichiesto()
      asd_progalm.NTSSetRichiesto()

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
  Public Overridable Sub FRMCFDISD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      dsDisd = oCleAnaz.dsShared
      dcDisd.DataSource = dsDisd.Tables("ANADITASD")
      dsDisd.Tables("ANADITASD").AcceptChanges()

      grDisd.DataSource = dcDisd

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      'devo bloccare/sbloccare i campi chiave se necessario
      grvDisd_NTSFocusedRowChanged(grvDisd, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMCFDISD_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub FRMCFDISD_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcDisd.Dispose()
      dsDisd.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvDisd.NTSNuovo()

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
      If Not grvDisd.NTSDeleteRigaCorrente(dcDisd, True) Then Return
      oCleAnaz.DisdSalva(True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvDisd.NTSRipristinaRigaCorrenteBefore(dcDisd, True) Then Return
      oCleAnaz.DisdRipristina(dcDisd.Position, dcDisd.Filter)
      grvDisd.NTSRipristinaRigaCorrenteAfter()
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
      Dim strTmp As String = ""
      Dim i As Integer = 0
      Dim ds As New DataSet

      If grvDisd.FocusedColumn.Name = "asd_coddest" Then
        'non posso fare lo zoom standard, visto che potrei selez. una destinaz. diversa appena inserita e non ancora salvata ...
        'creo un dataset contenente tutte le destinazioni diverse che ho in memoria
        ds.Tables.Add(oCleAnaz.dsShared.Tables("ANAZUL").Clone)
        ds.Tables(0).TableName = "DESTDIV"
        For i = 0 To oCleAnaz.dsShared.Tables("ANAZUL").Rows.Count - 1
          ds.Tables(0).ImportRow(oCleAnaz.dsShared.Tables("ANAZUL").Rows(i))
        Next
        'rinomino le colonne per farle uguali a quelle dello zoom
        For i = 0 To ds.Tables("DESTDIV").Columns.Count - 1
          If ds.Tables("DESTDIV").Columns(i).ColumnName.ToLower.Substring(0, 2) = "ul" Then
            ds.Tables("DESTDIV").Columns(i).ColumnName = "dd_" & ds.Tables("DESTDIV").Columns(i).ColumnName.Substring(3)
          End If
        Next
        ds.Tables(0).AcceptChanges()
        NTSZOOM.strIn = NTSCStr(grvDisd.EditingValue)
        oParam.lContoCF = 1   'passo il conto cliente/fornitore solo per dire che non è zoom su anazul
        oParam.oParam = ds
        NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvDisd.EditingValue) Then grvDisd.SetFocusedValue(NTSZOOM.strIn)
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

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub
#End Region

  Public Overridable Sub grvDisd_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvDisd.NTSBeforeRowUpdate
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

  Public Overridable Sub grvDisd_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvDisd.NTSFocusedRowChanged
    Try
      If NTSCInt(grvDisd.GetFocusedRowCellValue("asd_numesco")) <> 0 Then
        asd_numesco.Enabled = False
        asd_numprogr.Enabled = False
      Else
        GctlSetVisEnab(asd_numesco, False)
        GctlSetVisEnab(asd_numprogr, False)
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
      dRes = grvDisd.NTSSalvaRigaCorrente(dcDisd, oCleAnaz.DisdRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleAnaz.DisdSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleAnaz.DisdRipristina(dcDisd.Position, dcDisd.Filter)
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

  '--- Gestione tasti apri e nuovo zoom veloce destinazione
  Public Overridable Sub asd_coddest_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles asd_coddest.NTSZoomGest
    Dim oTmp As New Control
    Dim oCallParamsTmp As New CLE__CLDP
    Dim ds As New DataSet
    Dim i As Integer = 0
    Dim nCodDestTmp As Integer = 0
    Dim nTipo As Integer = 0
    Dim frmdesg As FRM__DESD = Nothing
    Try
      frmdesg = CType(NTSNewFormModal("FRM__DESD"), FRM__DESD)

      Me.ValidaLastControl()
      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo

      If e.TipoEvento = "OPEN" Then
        If IsNumeric(dsDisd.Tables("ANADITASD").Rows(dcDisd.Position)!tb_ucoddest) Then
          nCodDestTmp = NTSCInt(dsDisd.Tables("ANADITASD").Rows(dcDisd.Position)!tb_ucoddest)
        End If
        oCallParamsTmp.strParam = "APRI;" & nCodDestTmp
      Else
        nCodDestTmp = 0
        oCallParamsTmp.strParam = "NUOV;0"
      End If

      oTmp.Text = NTSCStr(nCodDestTmp)

      oCallParamsTmp.strPar1 = "Altri indirizzi"
      nTipo = 0
      If nCodDestTmp > 0 Then
        For i = 0 To oCleAnaz.dsShared.Tables("ANAZUL").Rows.Count - 1
          If nCodDestTmp = NTSCInt(oCleAnaz.dsShared.Tables("ANAZUL").Rows(i)!ul_coddest) Then
            Select Case NTSCInt(oCleAnaz.dsShared.Tables("ANAZUL").Rows(i)!ul_coddest)
              Case oCleAnaz.lDestdomf
                oCallParamsTmp.strPar1 = "Domicilio fiscale per provv. amministr."
                nTipo = oCleAnaz.lDestdomf
              Case oCleAnaz.lDestsedel
                oCallParamsTmp.strPar1 = "Resid./Domic. fisc./Sede legale in Italia"
                nTipo = oCleAnaz.lDestsedel
              Case oCleAnaz.lDestresan
                oCallParamsTmp.strPar1 = "Residenza/Sede legale estera"
                nTipo = oCleAnaz.lDestresan
              Case oCleAnaz.lDestcorr
                oCallParamsTmp.strPar1 = "Luogo di esercizio attiv. all'estero"
                nTipo = oCleAnaz.lDestcorr
            End Select
          End If
        Next
      End If

      '-------------------------------
      'clono latabella perchè negli altri indirizzi non devo far vedere gli indirizzi collegati alle destinazioni particolari 
      ds.Tables.Add(oCleAnaz.dsShared.Tables("ANAZUL").Clone())
      Select Case nTipo
        Case oCleAnaz.lDestdomf, oCleAnaz.lDestsedel, oCleAnaz.lDestresan, oCleAnaz.lDestcorr
          For i = 0 To oCleAnaz.dsShared.Tables("ANAZUL").Rows.Count - 1
            If NTSCInt(oCleAnaz.dsShared.Tables("ANAZUL").Rows(i)!ul_coddest) = nTipo Then
              ds.Tables("ANAZUL").ImportRow(oCleAnaz.dsShared.Tables("ANAZUL").Rows(i))
              oCleAnaz.dsShared.Tables("ANAZUL").Rows(i).Delete()
            End If
          Next
        Case Else
          For i = 0 To oCleAnaz.dsShared.Tables("ANAZUL").Rows.Count - 1
            If NTSCInt(oCleAnaz.dsShared.Tables("ANAZUL").Rows(i)!ul_coddest) <> oCleAnaz.lDestdomf And _
                NTSCInt(oCleAnaz.dsShared.Tables("ANAZUL").Rows(i)!ul_coddest) <> oCleAnaz.lDestsedel And _
                NTSCInt(oCleAnaz.dsShared.Tables("ANAZUL").Rows(i)!ul_coddest) <> oCleAnaz.lDestresan And _
                NTSCInt(oCleAnaz.dsShared.Tables("ANAZUL").Rows(i)!ul_coddest) <> oCleAnaz.lDestcorr Then
              ds.Tables("ANAZUL").ImportRow(oCleAnaz.dsShared.Tables("ANAZUL").Rows(i))
              oCleAnaz.dsShared.Tables("ANAZUL").Rows(i).Delete()
            End If
          Next
      End Select
      oCleAnaz.dsShared.Tables("ANAZUL").AcceptChanges()

      frmdesg.Init(oMenu, oCallParamsTmp, DittaCorrente)
      frmdesg.InitEntity(oCleAnaz, ds, nTipo)
      If NTSCInt(oCleAnaz.dsShared.Tables("TABANAZ").Rows(0)!tb_azcodanag) <> 0 Then frmdesg.tlbCancella.Enabled = False
      frmdesg.ShowDialog()

      '-------------------------------
      'riacquisisco gli indirizzi
      For i = 0 To ds.Tables("ANAZUL").Rows.Count - 1
        If ds.Tables("ANAZUL").Rows(i).RowState <> DataRowState.Deleted Then
          If NTSCInt(ds.Tables("ANAZUL").Rows(i)!ul_coddest) > 0 Then
            oCleAnaz.dsShared.Tables("ANAZUL").ImportRow(ds.Tables("ANAZUL").Rows(i))
          Else
            ds.Tables("ANAZUL").Rows(i).Delete()
          End If
        End If
      Next
      ds.Tables.Clear()
      oCleAnaz.dsShared.Tables("ANAZUL").AcceptChanges()
      oCleAnaz.bHasChanges = True
      'senza la riga sotto se cambio solo le destinazioni diverse non salva
      If oCleAnaz.dsShared.Tables("TABANAZ").Rows(0).RowState = DataRowState.Unchanged Then oCleAnaz.dsShared.Tables("TABANAZ").Rows(0).SetModified()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmdesg Is Nothing Then frmdesg.Dispose()
      frmdesg = Nothing
    End Try
  End Sub
  '---

End Class
