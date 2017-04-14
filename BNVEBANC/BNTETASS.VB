Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMTETASS

#Region "Variabili"
  Public oCleBanc As CLEVEBANC
  Public oCallParams As CLE__CLDP
  Public dsTass As New DataSet
  Public dcTass As New BindingSource()
  Public dttTass As DataTable = Nothing
  Public dtrTass() As DataRow = Nothing
  Private components As System.ComponentModel.IContainer

  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents grTass As NTSInformatica.NTSGrid
  Public WithEvents grvTass As NTSInformatica.NTSGridView
  Public WithEvents tx_codbanc As NTSInformatica.NTSGridColumn
  Public WithEvents tx_dtvalid As NTSInformatica.NTSGridColumn
  Public WithEvents tx_percatt As NTSInformatica.NTSGridColumn
  Public WithEvents tx_percatt2 As NTSInformatica.NTSGridColumn
  Public WithEvents tx_percatt3 As NTSInformatica.NTSGridColumn
  Public WithEvents tx_percpas As NTSInformatica.NTSGridColumn
  Public WithEvents tx_percpas2 As NTSInformatica.NTSGridColumn
  Public WithEvents codditt As NTSInformatica.NTSGridColumn

#End Region

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
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttTipo As New DataTable()
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        '  'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvTass.NTSSetParam(oMenu, oApp.Tr(Me, 128401968455223643, "Tassi applicati"))
      tx_codbanc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128777131900496193, "Codice banca"), "0", 4, 0, 9999)
      tx_dtvalid.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128777131919095298, "Data validità"), False)
      tx_percatt.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128777131932536668, "T.att. 1°sc."), "#,##0.00", 15)
      tx_percatt2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128777131958169048, "T.att. 2°sc."), "#,##0.00", 15)
      tx_percatt3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128777131981613298, "T.att. 3°sc."), "#,##0.00", 15)
      tx_percpas.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128777132016935968, "T.pass. 1°sc."), "#,##0.00", 15)
      tx_percpas2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128777132035535073, "T.pass. 2°sc."), "#,##0.00", 15)
      codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128777132052571228, "CODDITT"), 12, False)

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
  Public Overridable Sub InitEntity(ByRef cleBanc As CLEVEBANC)
    oCleBanc = cleBanc
    AddHandler oCleBanc.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub
  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMTETASS))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrecedente = New NTSInformatica.NTSBarButtonItem
    Me.tlbSuccessivo = New NTSInformatica.NTSBarButtonItem
    Me.tlbUltimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grTass = New NTSInformatica.NTSGrid
    Me.grvTass = New NTSInformatica.NTSGridView
    Me.tx_dtvalid = New NTSInformatica.NTSGridColumn
    Me.tx_percatt = New NTSInformatica.NTSGridColumn
    Me.tx_percatt2 = New NTSInformatica.NTSGridColumn
    Me.tx_percatt3 = New NTSInformatica.NTSGridColumn
    Me.tx_percpas = New NTSInformatica.NTSGridColumn
    Me.tx_percpas2 = New NTSInformatica.NTSGridColumn
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.tx_codbanc = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grTass, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvTass, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbRipristina, Me.tlbCancella, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbUltimo, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 10
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbPrimo
    '
    Me.tlbPrimo.Caption = "Primo"
    Me.tlbPrimo.Glyph = CType(resources.GetObject("tlbPrimo.Glyph"), System.Drawing.Image)
    Me.tlbPrimo.Id = 4
    Me.tlbPrimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P))
    Me.tlbPrimo.Name = "tlbPrimo"
    Me.tlbPrimo.Visible = True
    '
    'tlbPrecedente
    '
    Me.tlbPrecedente.Caption = "Precedente"
    Me.tlbPrecedente.Glyph = CType(resources.GetObject("tlbPrecedente.Glyph"), System.Drawing.Image)
    Me.tlbPrecedente.Id = 5
    Me.tlbPrecedente.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
    Me.tlbPrecedente.Name = "tlbPrecedente"
    Me.tlbPrecedente.Visible = True
    '
    'tlbSuccessivo
    '
    Me.tlbSuccessivo.Caption = "Successivo"
    Me.tlbSuccessivo.Glyph = CType(resources.GetObject("tlbSuccessivo.Glyph"), System.Drawing.Image)
    Me.tlbSuccessivo.Id = 6
    Me.tlbSuccessivo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
    Me.tlbSuccessivo.Name = "tlbSuccessivo"
    Me.tlbSuccessivo.Visible = True
    '
    'tlbUltimo
    '
    Me.tlbUltimo.Caption = "Ultimo"
    Me.tlbUltimo.Glyph = CType(resources.GetObject("tlbUltimo.Glyph"), System.Drawing.Image)
    Me.tlbUltimo.Id = 7
    Me.tlbUltimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 8
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 9
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'grTass
    '
    Me.grTass.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grTass.EmbeddedNavigator.Name = ""
    Me.grTass.Location = New System.Drawing.Point(0, 30)
    Me.grTass.MainView = Me.grvTass
    Me.grTass.Name = "grTass"
    Me.grTass.Size = New System.Drawing.Size(553, 320)
    Me.grTass.TabIndex = 4
    Me.grTass.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvTass})
    '
    'grvTass
    '
    Me.grvTass.ActiveFilterEnabled = False
    '
    'tx_dtvalid
    '
    Me.tx_dtvalid.AppearanceCell.Options.UseBackColor = True
    Me.tx_dtvalid.AppearanceCell.Options.UseTextOptions = True
    Me.tx_dtvalid.Caption = "Data validità"
    Me.tx_dtvalid.Enabled = True
    Me.tx_dtvalid.FieldName = "tx_dtvalid"
    Me.tx_dtvalid.Name = "tx_dtvalid"
    Me.tx_dtvalid.NTSRepositoryComboBox = Nothing
    Me.tx_dtvalid.NTSRepositoryItemCheck = Nothing
    Me.tx_dtvalid.NTSRepositoryItemText = Nothing
    Me.tx_dtvalid.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tx_dtvalid.OptionsFilter.AllowFilter = False
    Me.tx_dtvalid.Visible = True
    Me.tx_dtvalid.VisibleIndex = 0
    '
    'tx_percatt
    '
    Me.tx_percatt.AppearanceCell.Options.UseBackColor = True
    Me.tx_percatt.AppearanceCell.Options.UseTextOptions = True
    Me.tx_percatt.Caption = "T.att. 1°sc."
    Me.tx_percatt.Enabled = True
    Me.tx_percatt.FieldName = "tx_percatt"
    Me.tx_percatt.Name = "tx_percatt"
    Me.tx_percatt.NTSRepositoryComboBox = Nothing
    Me.tx_percatt.NTSRepositoryItemCheck = Nothing
    Me.tx_percatt.NTSRepositoryItemText = Nothing
    Me.tx_percatt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tx_percatt.OptionsFilter.AllowFilter = False
    Me.tx_percatt.Visible = True
    Me.tx_percatt.VisibleIndex = 1
    '
    'tx_percatt2
    '
    Me.tx_percatt2.AppearanceCell.Options.UseBackColor = True
    Me.tx_percatt2.AppearanceCell.Options.UseTextOptions = True
    Me.tx_percatt2.Caption = "T.att. 2°sc."
    Me.tx_percatt2.Enabled = True
    Me.tx_percatt2.FieldName = "tx_percatt2"
    Me.tx_percatt2.Name = "tx_percatt2"
    Me.tx_percatt2.NTSRepositoryComboBox = Nothing
    Me.tx_percatt2.NTSRepositoryItemCheck = Nothing
    Me.tx_percatt2.NTSRepositoryItemText = Nothing
    Me.tx_percatt2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tx_percatt2.OptionsFilter.AllowFilter = False
    Me.tx_percatt2.Visible = True
    Me.tx_percatt2.VisibleIndex = 2
    '
    'tx_percatt3
    '
    Me.tx_percatt3.AppearanceCell.Options.UseBackColor = True
    Me.tx_percatt3.AppearanceCell.Options.UseTextOptions = True
    Me.tx_percatt3.Caption = "T.att. 3°sc."
    Me.tx_percatt3.Enabled = True
    Me.tx_percatt3.FieldName = "tx_percatt3"
    Me.tx_percatt3.Name = "tx_percatt3"
    Me.tx_percatt3.NTSRepositoryComboBox = Nothing
    Me.tx_percatt3.NTSRepositoryItemCheck = Nothing
    Me.tx_percatt3.NTSRepositoryItemText = Nothing
    Me.tx_percatt3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tx_percatt3.OptionsFilter.AllowFilter = False
    Me.tx_percatt3.Visible = True
    Me.tx_percatt3.VisibleIndex = 3
    '
    'tx_percpas
    '
    Me.tx_percpas.AppearanceCell.Options.UseBackColor = True
    Me.tx_percpas.AppearanceCell.Options.UseTextOptions = True
    Me.tx_percpas.Caption = "T.pass. 1°sc."
    Me.tx_percpas.Enabled = True
    Me.tx_percpas.FieldName = "tx_percpas"
    Me.tx_percpas.Name = "tx_percpas"
    Me.tx_percpas.NTSRepositoryComboBox = Nothing
    Me.tx_percpas.NTSRepositoryItemCheck = Nothing
    Me.tx_percpas.NTSRepositoryItemText = Nothing
    Me.tx_percpas.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tx_percpas.OptionsFilter.AllowFilter = False
    Me.tx_percpas.Visible = True
    Me.tx_percpas.VisibleIndex = 4
    '
    'tx_percpas2
    '
    Me.tx_percpas2.AppearanceCell.Options.UseBackColor = True
    Me.tx_percpas2.AppearanceCell.Options.UseTextOptions = True
    Me.tx_percpas2.Caption = "T.pass. 2°sc."
    Me.tx_percpas2.Enabled = True
    Me.tx_percpas2.FieldName = "tx_percpas2"
    Me.tx_percpas2.Name = "tx_percpas2"
    Me.tx_percpas2.NTSRepositoryComboBox = Nothing
    Me.tx_percpas2.NTSRepositoryItemCheck = Nothing
    Me.tx_percpas2.NTSRepositoryItemText = Nothing
    Me.tx_percpas2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tx_percpas2.OptionsFilter.AllowFilter = False
    Me.tx_percpas2.Visible = True
    Me.tx_percpas2.VisibleIndex = 5
    '
    'codditt
    '
    Me.codditt.AppearanceCell.Options.UseBackColor = True
    Me.codditt.AppearanceCell.Options.UseTextOptions = True
    Me.codditt.Caption = "CODDITT"
    Me.codditt.Enabled = False
    Me.codditt.FieldName = "codditt"
    Me.codditt.Name = "codditt"
    Me.codditt.NTSRepositoryComboBox = Nothing
    Me.codditt.NTSRepositoryItemCheck = Nothing
    Me.codditt.NTSRepositoryItemText = Nothing
    Me.codditt.OptionsColumn.AllowEdit = False
    Me.codditt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.codditt.OptionsColumn.ReadOnly = True
    Me.codditt.OptionsFilter.AllowFilter = False
    Me.grvTass.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tx_codbanc, Me.tx_dtvalid, Me.tx_percatt, Me.tx_percatt2, Me.tx_percatt3, Me.tx_percpas, Me.tx_percpas2, Me.codditt})
    Me.grvTass.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvTass.Enabled = True
    Me.grvTass.GridControl = Me.grTass
    Me.grvTass.Name = "grvTass"
    Me.grvTass.NTSAllowDelete = True
    Me.grvTass.NTSAllowInsert = True
    Me.grvTass.NTSAllowUpdate = True
    Me.grvTass.NTSMenuContext = Nothing
    Me.grvTass.OptionsCustomization.AllowRowSizing = True
    Me.grvTass.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvTass.OptionsNavigation.UseTabKey = False
    Me.grvTass.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvTass.OptionsView.ColumnAutoWidth = False
    Me.grvTass.OptionsView.EnableAppearanceEvenRow = True
    Me.grvTass.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvTass.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvTass.OptionsView.ShowGroupPanel = False
    Me.grvTass.RowHeight = 14
    '
    'tx_codbanc
    '
    Me.tx_codbanc.AppearanceCell.Options.UseBackColor = True
    Me.tx_codbanc.AppearanceCell.Options.UseTextOptions = True
    Me.tx_codbanc.Caption = "Codice banca"
    Me.tx_codbanc.Enabled = True
    Me.tx_codbanc.FieldName = "tx_codbanc"
    Me.tx_codbanc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tx_codbanc.Name = "tx_codbanc"
    Me.tx_codbanc.NTSRepositoryComboBox = Nothing
    Me.tx_codbanc.NTSRepositoryItemCheck = Nothing
    Me.tx_codbanc.NTSRepositoryItemText = Nothing
    Me.tx_codbanc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tx_codbanc.OptionsFilter.AllowFilter = False
    '
    'FRMTETASS
    '
    Me.ClientSize = New System.Drawing.Size(553, 350)
    Me.Controls.Add(Me.grTass)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.HelpContext = "bstetass.htm"
    Me.Name = "FRMTETASS"
    Me.NTSLastControlFocussed = Me.grTass
    Me.Text = "Tassi applicati"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grTass, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvTass, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvTass.NTSSalvaRigaCorrente(dcTass, oCleBanc.TassRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleBanc.TassSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleBanc.TassRipristina(dcTass.Position, dcTass.Filter)
        Case System.Windows.Forms.DialogResult.Cancel
          'non posso fare nulla
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          'la riga non ha subito modifiche
      End Select
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

#Region "EventiForm"
  Public Overridable Sub FRMTETASS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleBanc.TassApri(DittaCorrente, dsTass) Then Me.Close()

      dcTass.DataSource = dsTass.Tables("TASSI")
      grTass.DataSource = dcTass

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      Me.Cursor = Cursors.Default

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMTETASS_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcTass.Dispose()
      dsTass.Dispose()
    Catch
    End Try
  End Sub
  Public Overridable Sub FRMTETASS_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvTass.NTSNuovo()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Try
      If Not grvTass.NTSDeleteRigaCorrente(dcTass, True) Then Return
      oCleBanc.TassSalva(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvTass.NTSRipristinaRigaCorrenteBefore(dcTass, True) Then Return
      oCleBanc.TassRipristina(dcTass.Position, dcTass.Filter)
      grvTass.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    Try
      '-------------------------------------------------
      'vado sul primo record
      If Not Salva() Then Return
      dcTass.MoveFirst()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbPrecedente_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrecedente.ItemClick
    Try
      '-------------------------------------------------
      'vado sul record precedente
      If Not Salva() Then Return
      dcTass.MovePrevious()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbSuccessivo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSuccessivo.ItemClick
    Try
      '-------------------------------------------------
      'vado sul record successivo
      If Not Salva() Then Return
      dcTass.MoveNext()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbUltimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbUltimo.ItemClick
    Try
      '-------------------------------------------------
      'vado sull'ultimo record
      If Not Salva() Then Return
      dcTass.MoveLast()
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
  Public Overridable Sub grvTass_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvTass.NTSBeforeRowUpdate
    Try
      If Not Salva() Then
        'Rimango sulla riga su cui sono
        e.Allow = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub grvTass_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvTass.NTSFocusedRowChanged
    Try
      If grvTass.NTSGetCurrentDataRow Is Nothing Then
        tx_dtvalid.Enabled = True
      Else
        If grvTass.NTSGetCurrentDataRow.RowState = DataRowState.Added Then
          tx_dtvalid.Enabled = True
        Else
          tx_dtvalid.Enabled = False
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

End Class