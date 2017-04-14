Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMCFDICE
  Public oCleAnaz As CLE__ANAZ
  Public oCallParams As CLE__CLDP
  Public dsDice As DataSet
  Public dcDice As BindingSource = New BindingSource()

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
  Public WithEvents grDice As NTSInformatica.NTSGrid
  Public WithEvents grvDice As NTSInformatica.NTSGridView
  Public WithEvents ae_numesco As NTSInformatica.NTSGridColumn
  Public WithEvents ae_stutpass As NTSInformatica.NTSGridColumn
  Public WithEvents ae_stutpesc As NTSInformatica.NTSGridColumn
  Public WithEvents ae_stutpeas As NTSInformatica.NTSGridColumn
  Public WithEvents ae_stutpinp As NTSInformatica.NTSGridColumn

  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMCFDICE))
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
    Me.grDice = New NTSInformatica.NTSGrid
    Me.grvDice = New NTSInformatica.NTSGridView
    Me.ae_numesco = New NTSInformatica.NTSGridColumn
    Me.ae_stutpass = New NTSInformatica.NTSGridColumn
    Me.ae_stutpesc = New NTSInformatica.NTSGridColumn
    Me.ae_stutpeas = New NTSInformatica.NTSGridColumn
    Me.ae_stutpinp = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grDice, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvDice, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'grDice
    '
    Me.grDice.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grDice.EmbeddedNavigator.Name = ""
    Me.grDice.Location = New System.Drawing.Point(0, 26)
    Me.grDice.MainView = Me.grvDice
    Me.grDice.Name = "grDice"
    Me.grDice.Size = New System.Drawing.Size(648, 416)
    Me.grDice.TabIndex = 5
    Me.grDice.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDice})
    '
    'grvDice
    '
    Me.grvDice.ActiveFilterEnabled = False
    Me.grvDice.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ae_numesco, Me.ae_stutpass, Me.ae_stutpesc, Me.ae_stutpeas, Me.ae_stutpinp})
    Me.grvDice.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvDice.Enabled = True
    Me.grvDice.GridControl = Me.grDice
    Me.grvDice.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvDice.Name = "grvDice"
    Me.grvDice.NTSAllowDelete = True
    Me.grvDice.NTSAllowInsert = True
    Me.grvDice.NTSAllowUpdate = True
    Me.grvDice.NTSMenuContext = Nothing
    Me.grvDice.OptionsCustomization.AllowRowSizing = True
    Me.grvDice.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvDice.OptionsNavigation.UseTabKey = False
    Me.grvDice.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvDice.OptionsView.ColumnAutoWidth = False
    Me.grvDice.OptionsView.EnableAppearanceEvenRow = True
    Me.grvDice.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvDice.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvDice.OptionsView.ShowGroupPanel = False
    Me.grvDice.RowHeight = 16
    '
    'ae_numesco
    '
    Me.ae_numesco.AppearanceCell.Options.UseBackColor = True
    Me.ae_numesco.AppearanceCell.Options.UseTextOptions = True
    Me.ae_numesco.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ae_numesco.Caption = "Esercizio"
    Me.ae_numesco.Enabled = True
    Me.ae_numesco.FieldName = "ae_numesco"
    Me.ae_numesco.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ae_numesco.Name = "ae_numesco"
    Me.ae_numesco.NTSRepositoryComboBox = Nothing
    Me.ae_numesco.NTSRepositoryItemCheck = Nothing
    Me.ae_numesco.NTSRepositoryItemText = Nothing
    Me.ae_numesco.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ae_numesco.OptionsFilter.AllowFilter = False
    Me.ae_numesco.Visible = True
    Me.ae_numesco.VisibleIndex = 0
    Me.ae_numesco.Width = 70
    '
    'ae_stutpass
    '
    Me.ae_stutpass.AppearanceCell.Options.UseBackColor = True
    Me.ae_stutpass.AppearanceCell.Options.UseTextOptions = True
    Me.ae_stutpass.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ae_stutpass.Caption = "Assogettamento"
    Me.ae_stutpass.Enabled = True
    Me.ae_stutpass.FieldName = "ae_stutpass"
    Me.ae_stutpass.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ae_stutpass.Name = "ae_stutpass"
    Me.ae_stutpass.NTSRepositoryComboBox = Nothing
    Me.ae_stutpass.NTSRepositoryItemCheck = Nothing
    Me.ae_stutpass.NTSRepositoryItemText = Nothing
    Me.ae_stutpass.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ae_stutpass.OptionsFilter.AllowFilter = False
    Me.ae_stutpass.Visible = True
    Me.ae_stutpass.VisibleIndex = 1
    Me.ae_stutpass.Width = 70
    '
    'ae_stutpesc
    '
    Me.ae_stutpesc.AppearanceCell.Options.UseBackColor = True
    Me.ae_stutpesc.AppearanceCell.Options.UseTextOptions = True
    Me.ae_stutpesc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ae_stutpesc.Caption = "Esclusione per assog. E"
    Me.ae_stutpesc.Enabled = True
    Me.ae_stutpesc.FieldName = "ae_stutpesc"
    Me.ae_stutpesc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ae_stutpesc.Name = "ae_stutpesc"
    Me.ae_stutpesc.NTSRepositoryComboBox = Nothing
    Me.ae_stutpesc.NTSRepositoryItemCheck = Nothing
    Me.ae_stutpesc.NTSRepositoryItemText = Nothing
    Me.ae_stutpesc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ae_stutpesc.OptionsFilter.AllowFilter = False
    Me.ae_stutpesc.Visible = True
    Me.ae_stutpesc.VisibleIndex = 2
    Me.ae_stutpesc.Width = 70
    '
    'ae_stutpeas
    '
    Me.ae_stutpeas.AppearanceCell.Options.UseBackColor = True
    Me.ae_stutpeas.AppearanceCell.Options.UseTextOptions = True
    Me.ae_stutpeas.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ae_stutpeas.Caption = "Esclusione per assog. S"
    Me.ae_stutpeas.Enabled = True
    Me.ae_stutpeas.FieldName = "ae_stutpeas"
    Me.ae_stutpeas.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ae_stutpeas.Name = "ae_stutpeas"
    Me.ae_stutpeas.NTSRepositoryComboBox = Nothing
    Me.ae_stutpeas.NTSRepositoryItemCheck = Nothing
    Me.ae_stutpeas.NTSRepositoryItemText = Nothing
    Me.ae_stutpeas.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ae_stutpeas.OptionsFilter.AllowFilter = False
    Me.ae_stutpeas.Visible = True
    Me.ae_stutpeas.VisibleIndex = 3
    Me.ae_stutpeas.Width = 70
    '
    'ae_stutpinp
    '
    Me.ae_stutpinp.AppearanceCell.Options.UseBackColor = True
    Me.ae_stutpinp.AppearanceCell.Options.UseTextOptions = True
    Me.ae_stutpinp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ae_stutpinp.Caption = "Modalità richiesta"
    Me.ae_stutpinp.Enabled = True
    Me.ae_stutpinp.FieldName = "ae_stutpinp"
    Me.ae_stutpinp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ae_stutpinp.Name = "ae_stutpinp"
    Me.ae_stutpinp.NTSRepositoryComboBox = Nothing
    Me.ae_stutpinp.NTSRepositoryItemCheck = Nothing
    Me.ae_stutpinp.NTSRepositoryItemText = Nothing
    Me.ae_stutpinp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ae_stutpinp.OptionsFilter.AllowFilter = False
    Me.ae_stutpinp.Visible = True
    Me.ae_stutpinp.VisibleIndex = 4
    Me.ae_stutpinp.Width = 70
    '
    'FRMCFDICE
    '
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.grDice)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMCFDICE"
    Me.NTSLastControlFocussed = Me.grDice
    Me.Text = "DATI AGGIUNTIVI CONTABILI/STUDI PER ESERCIZI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grDice, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvDice, System.ComponentModel.ISupportInitialize).EndInit()
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

      'esempio per colonna combo
      Dim dttStutpass As New DataTable()
      dttStutpass.Columns.Add("cod", GetType(String))
      dttStutpass.Columns.Add("val", GetType(String))
      dttStutpass.Rows.Add(New Object() {"E", "Escluso"})
      dttStutpass.Rows.Add(New Object() {"P", "Parametri"})
      dttStutpass.Rows.Add(New Object() {"G", "Gerico"})
      dttStutpass.Rows.Add(New Object() {"S", "Gerico AS"})
      dttStutpass.AcceptChanges()

      Dim stutpesc As New DataTable()
      stutpesc.Columns.Add("cod", GetType(Short))
      stutpesc.Columns.Add("val", GetType(String))
      stutpesc.Rows.Add(New Object() {0, "Nessuna esclusione"})
      stutpesc.Rows.Add(New Object() {1, "Att. ini/ces periodo d'imposta"})
      stutpesc.Rows.Add(New Object() {2, "Periodo  non normale attività"})
      stutpesc.Rows.Add(New Object() {3, "Periodo imp. <> 12 mesi"})
      stutpesc.Rows.Add(New Object() {4, "Altre cause"})
      stutpesc.Rows.Add(New Object() {5, "Ricavi < 51.645 / comuni < 3000"})
      stutpesc.AcceptChanges()

      Dim dttStutpeas As New DataTable()
      dttStutpeas.Columns.Add("cod", GetType(String))
      dttStutpeas.Columns.Add("val", GetType(String))
      dttStutpeas.Rows.Add(New Object() {"N", "No"})
      dttStutpeas.Rows.Add(New Object() {"1", "Più di dieci modelli"})
      dttStutpeas.Rows.Add(New Object() {"2", "Ricavi < 20% amm.compl."})
      dttStutpeas.AcceptChanges()

      Dim dttStutpinp As New DataTable()
      dttStutpinp.Columns.Add("cod", GetType(String))
      dttStutpinp.Columns.Add("val", GetType(String))
      dttStutpinp.Rows.Add(New Object() {"S", "Sintetica"})
      dttStutpinp.Rows.Add(New Object() {"A", "Analitica"})
      dttStutpinp.AcceptChanges()

      grvDice.NTSSetParam(oMenu, "DATI AGGIUNTIVI CONTABILI/STUDI PER ESERCIZI")
      ae_numesco.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129006947913261205, "Esercizio"), tabesco)
      ae_stutpass.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129006948523745583, "Assogettamento"), dttStutpass, "val", "cod")
      ae_stutpesc.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129006948098578449, "Esclusione per assog. E"), stutpesc, "val", "cod")
      ae_stutpeas.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128644966337343750, "Esclusione per assog. S"), dttStutpeas, "val", "cod")
      ae_stutpinp.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128644966337500000, "Modalità richiesta"), dttStutpinp, "val", "cod")

      ae_numesco.NTSForzaVisZoom = False

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
  Public Overridable Sub FRMCFDICE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      dsDice = oCleAnaz.dsShared
      dcDice.DataSource = dsDice.Tables("ANADITACE")
      dsDice.Tables("ANADITACE").AcceptChanges()

      grDice.DataSource = dcDice

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      'devo bloccare/sbloccare i campi chiave se necessario
      grvDice_NTSFocusedRowChanged(grvDice, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMCFDICE_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub FRMCFDICE_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcDice.Dispose()
      dsDice.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvDice.NTSNuovo()

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
      If Not grvDice.NTSDeleteRigaCorrente(dcDice, True) Then Return
      oCleAnaz.DiceSalva(True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvDice.NTSRipristinaRigaCorrenteBefore(dcDice, True) Then Return
      oCleAnaz.DiceRipristina(dcDice.Position, dcDice.Filter)
      grvDice.NTSRipristinaRigaCorrenteAfter()
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

  Public Overridable Sub grvDice_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvDice.NTSBeforeRowUpdate
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

  Public Overridable Sub grvDice_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvDice.NTSFocusedRowChanged
    Try
      If NTSCInt(grvDice.GetFocusedRowCellValue("ae_numesco")) <> 0 Then
        ae_numesco.Enabled = False
      Else
        GctlSetVisEnab(ae_numesco, False)
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
      dRes = grvDice.NTSSalvaRigaCorrente(dcDice, oCleAnaz.DiceRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleAnaz.DiceSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleAnaz.DiceRipristina(dcDice.Position, dcDice.Filter)
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
