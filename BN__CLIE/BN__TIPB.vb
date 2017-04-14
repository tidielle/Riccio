Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__TIPB
  Public oCleclie As CLE__CLIE
  Public oCallParams As CLE__CLDP
  Public dsTipb As DataSet
  Public dcTipb As BindingSource = New BindingSource()

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
  Public WithEvents grTipb As NTSInformatica.NTSGrid
  Public WithEvents grvTipb As NTSInformatica.NTSGridView
  Public WithEvents ctp_codtpbf As NTSInformatica.NTSGridColumn
  Public WithEvents ctp_codpaga As NTSInformatica.NTSGridColumn
  Public WithEvents ctp_mesees1 As NTSInformatica.NTSGridColumn
  Public WithEvents ctp_mesees2 As NTSInformatica.NTSGridColumn
  Public WithEvents ctp_giofiss As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codpaga As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codtpbf As NTSInformatica.NTSGridColumn

  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__TIPB))
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
    Me.grTipb = New NTSInformatica.NTSGrid
    Me.grvTipb = New NTSInformatica.NTSGridView
    Me.ctp_codtpbf = New NTSInformatica.NTSGridColumn
    Me.ctp_codpaga = New NTSInformatica.NTSGridColumn
    Me.ctp_mesees1 = New NTSInformatica.NTSGridColumn
    Me.ctp_mesees2 = New NTSInformatica.NTSGridColumn
    Me.ctp_giofiss = New NTSInformatica.NTSGridColumn
    Me.xx_codpaga = New NTSInformatica.NTSGridColumn
    Me.xx_codtpbf = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grTipb, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvTipb, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'grTipb
    '
    Me.grTipb.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grTipb.EmbeddedNavigator.Name = ""
    Me.grTipb.Location = New System.Drawing.Point(0, 26)
    Me.grTipb.MainView = Me.grvTipb
    Me.grTipb.Name = "grTipb"
    Me.grTipb.Size = New System.Drawing.Size(648, 416)
    Me.grTipb.TabIndex = 5
    Me.grTipb.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvTipb})
    '
    'grvTipb
    '
    Me.grvTipb.ActiveFilterEnabled = False
    Me.grvTipb.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ctp_codtpbf, Me.xx_codtpbf, Me.ctp_codpaga, Me.xx_codpaga, Me.ctp_mesees1, Me.ctp_mesees2, Me.ctp_giofiss})
    Me.grvTipb.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvTipb.Enabled = True
    Me.grvTipb.GridControl = Me.grTipb
    Me.grvTipb.Name = "grvTipb"
    Me.grvTipb.NTSAllowDelete = True
    Me.grvTipb.NTSAllowInsert = True
    Me.grvTipb.NTSAllowUpdate = True
    Me.grvTipb.NTSMenuContext = Nothing
    Me.grvTipb.OptionsCustomization.AllowRowSizing = True
    Me.grvTipb.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvTipb.OptionsNavigation.UseTabKey = False
    Me.grvTipb.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvTipb.OptionsView.ColumnAutoWidth = False
    Me.grvTipb.OptionsView.EnableAppearanceEvenRow = True
    Me.grvTipb.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvTipb.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvTipb.OptionsView.ShowGroupPanel = False
    Me.grvTipb.RowHeight = 16
    '
    'ctp_codtpbf
    '
    Me.ctp_codtpbf.AppearanceCell.Options.UseBackColor = True
    Me.ctp_codtpbf.AppearanceCell.Options.UseTextOptions = True
    Me.ctp_codtpbf.Caption = "Tipo B/F"
    Me.ctp_codtpbf.Enabled = True
    Me.ctp_codtpbf.FieldName = "ctp_codtpbf"
    Me.ctp_codtpbf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ctp_codtpbf.Name = "ctp_codtpbf"
    Me.ctp_codtpbf.NTSRepositoryComboBox = Nothing
    Me.ctp_codtpbf.NTSRepositoryItemCheck = Nothing
    Me.ctp_codtpbf.NTSRepositoryItemText = Nothing
    Me.ctp_codtpbf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ctp_codtpbf.OptionsFilter.AllowFilter = False
    Me.ctp_codtpbf.Visible = True
    Me.ctp_codtpbf.VisibleIndex = 0
    Me.ctp_codtpbf.Width = 70
    '
    'ctp_codpaga
    '
    Me.ctp_codpaga.AppearanceCell.Options.UseBackColor = True
    Me.ctp_codpaga.AppearanceCell.Options.UseTextOptions = True
    Me.ctp_codpaga.Caption = "Cod. pag."
    Me.ctp_codpaga.Enabled = True
    Me.ctp_codpaga.FieldName = "ctp_codpaga"
    Me.ctp_codpaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ctp_codpaga.Name = "ctp_codpaga"
    Me.ctp_codpaga.NTSRepositoryComboBox = Nothing
    Me.ctp_codpaga.NTSRepositoryItemCheck = Nothing
    Me.ctp_codpaga.NTSRepositoryItemText = Nothing
    Me.ctp_codpaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ctp_codpaga.OptionsFilter.AllowFilter = False
    Me.ctp_codpaga.Visible = True
    Me.ctp_codpaga.VisibleIndex = 2
    Me.ctp_codpaga.Width = 70
    '
    'ctp_mesees1
    '
    Me.ctp_mesees1.AppearanceCell.Options.UseBackColor = True
    Me.ctp_mesees1.AppearanceCell.Options.UseTextOptions = True
    Me.ctp_mesees1.Caption = "Mese es.1"
    Me.ctp_mesees1.Enabled = True
    Me.ctp_mesees1.FieldName = "ctp_mesees1"
    Me.ctp_mesees1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ctp_mesees1.Name = "ctp_mesees1"
    Me.ctp_mesees1.NTSRepositoryComboBox = Nothing
    Me.ctp_mesees1.NTSRepositoryItemCheck = Nothing
    Me.ctp_mesees1.NTSRepositoryItemText = Nothing
    Me.ctp_mesees1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ctp_mesees1.OptionsFilter.AllowFilter = False
    Me.ctp_mesees1.Visible = True
    Me.ctp_mesees1.VisibleIndex = 4
    Me.ctp_mesees1.Width = 70
    '
    'ctp_mesees2
    '
    Me.ctp_mesees2.AppearanceCell.Options.UseBackColor = True
    Me.ctp_mesees2.AppearanceCell.Options.UseTextOptions = True
    Me.ctp_mesees2.Caption = "Mese es.2"
    Me.ctp_mesees2.Enabled = True
    Me.ctp_mesees2.FieldName = "ctp_mesees2"
    Me.ctp_mesees2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ctp_mesees2.Name = "ctp_mesees2"
    Me.ctp_mesees2.NTSRepositoryComboBox = Nothing
    Me.ctp_mesees2.NTSRepositoryItemCheck = Nothing
    Me.ctp_mesees2.NTSRepositoryItemText = Nothing
    Me.ctp_mesees2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ctp_mesees2.OptionsFilter.AllowFilter = False
    Me.ctp_mesees2.Visible = True
    Me.ctp_mesees2.VisibleIndex = 5
    Me.ctp_mesees2.Width = 70
    '
    'ctp_giofiss
    '
    Me.ctp_giofiss.AppearanceCell.Options.UseBackColor = True
    Me.ctp_giofiss.AppearanceCell.Options.UseTextOptions = True
    Me.ctp_giofiss.Caption = "Giorno fisso"
    Me.ctp_giofiss.Enabled = True
    Me.ctp_giofiss.FieldName = "ctp_giofiss"
    Me.ctp_giofiss.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ctp_giofiss.Name = "ctp_giofiss"
    Me.ctp_giofiss.NTSRepositoryComboBox = Nothing
    Me.ctp_giofiss.NTSRepositoryItemCheck = Nothing
    Me.ctp_giofiss.NTSRepositoryItemText = Nothing
    Me.ctp_giofiss.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ctp_giofiss.OptionsFilter.AllowFilter = False
    Me.ctp_giofiss.Visible = True
    Me.ctp_giofiss.VisibleIndex = 6
    Me.ctp_giofiss.Width = 70
    '
    'xx_codpaga
    '
    Me.xx_codpaga.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
    Me.xx_codpaga.AppearanceCell.Options.UseBackColor = True
    Me.xx_codpaga.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codpaga.Caption = "Descr. pagamento"
    Me.xx_codpaga.Enabled = False
    Me.xx_codpaga.FieldName = "xx_codpaga"
    Me.xx_codpaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codpaga.Name = "xx_codpaga"
    Me.xx_codpaga.NTSRepositoryComboBox = Nothing
    Me.xx_codpaga.NTSRepositoryItemCheck = Nothing
    Me.xx_codpaga.NTSRepositoryItemText = Nothing
    Me.xx_codpaga.OptionsColumn.AllowEdit = False
    Me.xx_codpaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codpaga.OptionsColumn.ReadOnly = True
    Me.xx_codpaga.OptionsFilter.AllowFilter = False
    Me.xx_codpaga.Visible = True
    Me.xx_codpaga.VisibleIndex = 3
    Me.xx_codpaga.Width = 70
    '
    'xx_codtpbf
    '
    Me.xx_codtpbf.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
    Me.xx_codtpbf.AppearanceCell.Options.UseBackColor = True
    Me.xx_codtpbf.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codtpbf.Caption = "Descr. tipo B/F"
    Me.xx_codtpbf.Enabled = False
    Me.xx_codtpbf.FieldName = "xx_codtpbf"
    Me.xx_codtpbf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codtpbf.Name = "xx_codtpbf"
    Me.xx_codtpbf.NTSRepositoryComboBox = Nothing
    Me.xx_codtpbf.NTSRepositoryItemCheck = Nothing
    Me.xx_codtpbf.NTSRepositoryItemText = Nothing
    Me.xx_codtpbf.OptionsColumn.AllowEdit = False
    Me.xx_codtpbf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codtpbf.OptionsColumn.ReadOnly = True
    Me.xx_codtpbf.OptionsFilter.AllowFilter = False
    Me.xx_codtpbf.Visible = True
    Me.xx_codtpbf.VisibleIndex = 1
    Me.xx_codtpbf.Width = 70
    '
    'FRM__TIPB
    '
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.grTipb)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRM__TIPB"
    Me.NTSLastControlFocussed = Me.grTipb
    Me.Text = "CONDIZIONI PER TIPO BOLLA/FATTURA"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grTipb, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvTipb, System.ComponentModel.ISupportInitialize).EndInit()
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

  Public Overridable Sub InitEntity(ByRef cleTipb As CLE__CLIE)
    oCleclie = cleTipb
    AddHandler oCleclie.RemoteEvent, AddressOf GestisciEventiEntity
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

      grvTipb.NTSSetParam(oMenu, "CONDIZIONI PER TIPO BOLLA/FATTURA")

      ctp_codtpbf.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128387232249038000, "Tipo B/F"), tabtpbf)
      ctp_codpaga.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128387232249194000, "Cod. pag."), tabpaga)
      ctp_mesees1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128387232249350000, "Mese es.1"), "0", 2, 0, 12)
      ctp_mesees2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128387232249506000, "Mese es.2"), "0", 2, 0, 12)
      ctp_giofiss.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128387232249662000, "Giorno fisso"), "0", 2, 0, 31)
      xx_codpaga.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128387232249818000, "Descr. pagamento"), 0, True)
      xx_codtpbf.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128387232249974000, "Descr. tipo B/F"), 0, True)

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
  Public Overridable Sub FRM__TIPB_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      dsTipb = oCleclie.dsShared
      dcTipb.DataSource = dsTipb.Tables("CLITIPB")
      dsTipb.AcceptChanges()

      grTipb.DataSource = dcTipb

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlTipoDoc = oCleclie.strTipoConto
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__TIPB_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub FRM__TIPB_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcTipb.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvTipb.NTSNuovo()

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
      If Not grvTipb.NTSDeleteRigaCorrente(dcTipb, True) Then Return
      oCleclie.TipbSalva(True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvTipb.NTSRipristinaRigaCorrenteBefore(dcTipb, True) Then Return
      oCleclie.TipbRipristina(dcTipb.Position, dcTipb.Filter)
      grvTipb.NTSRipristinaRigaCorrenteAfter()
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

  Public Overridable Sub grvTipb_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvTipb.NTSBeforeRowUpdate
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
      dRes = grvTipb.NTSSalvaRigaCorrente(dcTipb, oCleclie.RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleclie.TipbSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleclie.TipbRipristina(dcTipb.Position, dcTipb.Filter)
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
