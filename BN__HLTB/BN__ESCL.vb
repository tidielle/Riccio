Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__ESCL
  Public oCleHltb As CLE__HLTB
  Public oCallParams As CLE__CLDP
  Public dsEscl As DataSet
  Public dcEscl As BindingSource = New BindingSource()

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
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents grEscl As NTSInformatica.NTSGrid
  Public WithEvents grvEscl As NTSInformatica.NTSGridView
  Public WithEvents es_ditta As NTSInformatica.NTSGridColumn
  Public WithEvents es_opnome As NTSInformatica.NTSGridColumn
  Public WithEvents es_ruolo As NTSInformatica.NTSGridColumn

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__ESCL))
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
    Me.grEscl = New NTSInformatica.NTSGrid
    Me.grvEscl = New NTSInformatica.NTSGridView
    Me.es_ditta = New NTSInformatica.NTSGridColumn
    Me.es_opnome = New NTSInformatica.NTSGridColumn
    Me.es_ruolo = New NTSInformatica.NTSGridColumn
    Me.es_escludi = New NTSInformatica.NTSGridColumn
    Me.es_cods = New NTSInformatica.NTSGridColumn
    Me.pnBottom = New NTSInformatica.NTSPanel
    Me.lbNota = New NTSInformatica.NTSLabel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grEscl, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvEscl, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBottom.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbEsci, Me.tlbZoom})
    Me.NtsBarManager1.MaxItemId = 17
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
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 12
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'grEscl
    '
    Me.grEscl.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grEscl.EmbeddedNavigator.Name = ""
    Me.grEscl.Location = New System.Drawing.Point(0, 30)
    Me.grEscl.MainView = Me.grvEscl
    Me.grEscl.Name = "grEscl"
    Me.grEscl.Size = New System.Drawing.Size(648, 389)
    Me.grEscl.TabIndex = 5
    Me.grEscl.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvEscl})
    '
    'grvEscl
    '
    Me.grvEscl.ActiveFilterEnabled = False
    Me.grvEscl.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.es_ditta, Me.es_opnome, Me.es_ruolo, Me.es_escludi, Me.es_cods})
    Me.grvEscl.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvEscl.Enabled = True
    Me.grvEscl.GridControl = Me.grEscl
    Me.grvEscl.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvEscl.Name = "grvEscl"
    Me.grvEscl.NTSAllowDelete = True
    Me.grvEscl.NTSAllowInsert = True
    Me.grvEscl.NTSAllowUpdate = True
    Me.grvEscl.NTSMenuContext = Nothing
    Me.grvEscl.OptionsCustomization.AllowRowSizing = True
    Me.grvEscl.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvEscl.OptionsNavigation.UseTabKey = False
    Me.grvEscl.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvEscl.OptionsView.ColumnAutoWidth = False
    Me.grvEscl.OptionsView.EnableAppearanceEvenRow = True
    Me.grvEscl.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvEscl.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvEscl.OptionsView.ShowGroupPanel = False
    Me.grvEscl.RowHeight = 16
    '
    'es_ditta
    '
    Me.es_ditta.AppearanceCell.Options.UseBackColor = True
    Me.es_ditta.AppearanceCell.Options.UseTextOptions = True
    Me.es_ditta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.es_ditta.Caption = "Ditta"
    Me.es_ditta.Enabled = True
    Me.es_ditta.FieldName = "es_ditta"
    Me.es_ditta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.es_ditta.Name = "es_ditta"
    Me.es_ditta.NTSRepositoryComboBox = Nothing
    Me.es_ditta.NTSRepositoryItemCheck = Nothing
    Me.es_ditta.NTSRepositoryItemMemo = Nothing
    Me.es_ditta.NTSRepositoryItemText = Nothing
    Me.es_ditta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.es_ditta.OptionsFilter.AllowFilter = False
    Me.es_ditta.Visible = True
    Me.es_ditta.VisibleIndex = 0
    Me.es_ditta.Width = 70
    '
    'es_opnome
    '
    Me.es_opnome.AppearanceCell.Options.UseBackColor = True
    Me.es_opnome.AppearanceCell.Options.UseTextOptions = True
    Me.es_opnome.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.es_opnome.Caption = "Operatore"
    Me.es_opnome.Enabled = True
    Me.es_opnome.FieldName = "es_opnome"
    Me.es_opnome.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.es_opnome.Name = "es_opnome"
    Me.es_opnome.NTSRepositoryComboBox = Nothing
    Me.es_opnome.NTSRepositoryItemCheck = Nothing
    Me.es_opnome.NTSRepositoryItemMemo = Nothing
    Me.es_opnome.NTSRepositoryItemText = Nothing
    Me.es_opnome.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.es_opnome.OptionsFilter.AllowFilter = False
    Me.es_opnome.Visible = True
    Me.es_opnome.VisibleIndex = 1
    Me.es_opnome.Width = 70
    '
    'es_ruolo
    '
    Me.es_ruolo.AppearanceCell.Options.UseBackColor = True
    Me.es_ruolo.AppearanceCell.Options.UseTextOptions = True
    Me.es_ruolo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.es_ruolo.Caption = "Ruolo / gruppo"
    Me.es_ruolo.Enabled = True
    Me.es_ruolo.FieldName = "es_ruolo"
    Me.es_ruolo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.es_ruolo.Name = "es_ruolo"
    Me.es_ruolo.NTSRepositoryComboBox = Nothing
    Me.es_ruolo.NTSRepositoryItemCheck = Nothing
    Me.es_ruolo.NTSRepositoryItemMemo = Nothing
    Me.es_ruolo.NTSRepositoryItemText = Nothing
    Me.es_ruolo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.es_ruolo.OptionsFilter.AllowFilter = False
    Me.es_ruolo.Visible = True
    Me.es_ruolo.VisibleIndex = 2
    Me.es_ruolo.Width = 70
    '
    'es_escludi
    '
    Me.es_escludi.AppearanceCell.Options.UseBackColor = True
    Me.es_escludi.AppearanceCell.Options.UseTextOptions = True
    Me.es_escludi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.es_escludi.Caption = "Tipo vincolo"
    Me.es_escludi.Enabled = True
    Me.es_escludi.FieldName = "es_escludi"
    Me.es_escludi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.es_escludi.Name = "es_escludi"
    Me.es_escludi.NTSRepositoryComboBox = Nothing
    Me.es_escludi.NTSRepositoryItemCheck = Nothing
    Me.es_escludi.NTSRepositoryItemMemo = Nothing
    Me.es_escludi.NTSRepositoryItemText = Nothing
    Me.es_escludi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.es_escludi.OptionsFilter.AllowFilter = False
    Me.es_escludi.Visible = True
    Me.es_escludi.VisibleIndex = 3
    '
    'es_cods
    '
    Me.es_cods.AppearanceCell.Options.UseBackColor = True
    Me.es_cods.AppearanceCell.Options.UseTextOptions = True
    Me.es_cods.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.es_cods.Caption = "Codici separati da ','"
    Me.es_cods.Enabled = True
    Me.es_cods.FieldName = "es_cods"
    Me.es_cods.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.es_cods.Name = "es_cods"
    Me.es_cods.NTSRepositoryComboBox = Nothing
    Me.es_cods.NTSRepositoryItemCheck = Nothing
    Me.es_cods.NTSRepositoryItemMemo = Nothing
    Me.es_cods.NTSRepositoryItemText = Nothing
    Me.es_cods.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.es_cods.OptionsFilter.AllowFilter = False
    Me.es_cods.Visible = True
    Me.es_cods.VisibleIndex = 4
    '
    'pnBottom
    '
    Me.pnBottom.AllowDrop = True
    Me.pnBottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBottom.Appearance.Options.UseBackColor = True
    Me.pnBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBottom.Controls.Add(Me.lbNota)
    Me.pnBottom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnBottom.Location = New System.Drawing.Point(0, 419)
    Me.pnBottom.Name = "pnBottom"
    Me.pnBottom.Size = New System.Drawing.Size(648, 23)
    Me.pnBottom.TabIndex = 6
    Me.pnBottom.Text = "NtsPanel1"
    '
    'lbNota
    '
    Me.lbNota.AutoSize = True
    Me.lbNota.BackColor = System.Drawing.Color.Transparent
    Me.lbNota.Location = New System.Drawing.Point(12, 4)
    Me.lbNota.Name = "lbNota"
    Me.lbNota.NTSDbField = ""
    Me.lbNota.Size = New System.Drawing.Size(197, 13)
    Me.lbNota.TabIndex = 0
    Me.lbNota.Text = "Filtri Ditta / Operatore / Ruolo: * = tutti"
    Me.lbNota.Tooltip = ""
    Me.lbNota.UseMnemonic = False
    '
    'FRM__ESCL
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.grEscl)
    Me.Controls.Add(Me.pnBottom)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRM__ESCL"
    Me.NTSLastControlFocussed = Me.grEscl
    Me.Text = "ESCLUSIONI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grEscl, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvEscl, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBottom.ResumeLayout(False)
    Me.pnBottom.PerformLayout()
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

  Public Overridable Sub InitEntity(ByRef cleHltb As CLE__HLTB)
    oCleHltb = cleHltb
    AddHandler oCleHltb.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttTipo As New DataTable
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

      dttTipo.Columns.Add("cod", GetType(String))
      dttTipo.Columns.Add("val", GetType(String))
      dttTipo.Rows.Add(New Object() {"N", "Visualizza solo"})
      dttTipo.Rows.Add(New Object() {"S", "Non visualizzare"})
      dttTipo.AcceptChanges()


      grvEscl.NTSSetParam(oMenu, "ESCLUSIONI")

      es_ditta.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128399361441014000, "Ditta"), 12, False)
      es_opnome.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128399366052182000, "Operatore"), 20, False)
      es_ruolo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129737852345858859, "Ruolo / gruppo operatore"), 20, False)
      es_escludi.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128399366425646000, "Tipo filtro"), dttTipo, "val", "cod")
      es_cods.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129737852741571560, "Filtri"), 0, False)

      es_ditta.NTSSetParamZoom("ZOOMTABANAZ")
      es_opnome.NTSSetParamZoom("ZOOMOPERAT")
      es_ruolo.NTSSetParamZoom("ZOOMRUOLI")


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
  Public Overridable Sub FRM__ESCL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleHltb.EsclApri(dsEscl) Then
        Me.Close()
        Return
      End If
      dcEscl.DataSource = dsEscl.Tables("ESZOOM")
      dsEscl.AcceptChanges()

      grEscl.DataSource = dcEscl

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__ESCL_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try

  End Sub

  Public Overridable Sub FRM__ESCL_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcEscl.Dispose()
      dsEscl.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvEscl.NTSNuovo()

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
      If Not grvEscl.NTSDeleteRigaCorrente(dcEscl, True) Then Return
      oCleHltb.EsclSalva(True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvEscl.NTSRipristinaRigaCorrenteBefore(dcEscl, True) Then Return
      oCleHltb.EsclRipristina(dcEscl.Position, dcEscl.Filter)
      grvEscl.NTSRipristinaRigaCorrenteAfter()
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

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub
#End Region

  Public Overridable Sub grvEscl_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvEscl.NTSBeforeRowUpdate
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


  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvEscl.NTSSalvaRigaCorrente(dcEscl, oCleHltb.EsclRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleHltb.EsclSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleHltb.EsclRipristina(dcEscl.Position, dcEscl.Filter)
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
