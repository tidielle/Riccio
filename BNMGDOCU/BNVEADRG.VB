Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVEADRG
  Public oCleDocu As CLEMGDOCU
  Public oCallParams As CLE__CLDP
  Public dsAdrg As New DataSet
  Public dcAdrg As BindingSource = New BindingSource()

  Private components As System.ComponentModel.IContainer

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
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents grAdrg As NTSInformatica.NTSGrid
  Public WithEvents grvAdrg As NTSInformatica.NTSGridView
  Public WithEvents ak_riga As NTSInformatica.NTSGridColumn
  Public WithEvents ak_codart As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codart As NTSInformatica.NTSGridColumn
  Public WithEvents ak_unmis As NTSInformatica.NTSGridColumn
  Public WithEvents ak_colli As NTSInformatica.NTSGridColumn
  Public WithEvents ak_ump As NTSInformatica.NTSGridColumn
  Public WithEvents ak_quant As NTSInformatica.NTSGridColumn

  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMVEADRG))
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
    Me.grAdrg = New NTSInformatica.NTSGrid
    Me.grvAdrg = New NTSInformatica.NTSGridView
    Me.ak_riga = New NTSInformatica.NTSGridColumn
    Me.ak_codart = New NTSInformatica.NTSGridColumn
    Me.xx_codart = New NTSInformatica.NTSGridColumn
    Me.ak_unmis = New NTSInformatica.NTSGridColumn
    Me.ak_colli = New NTSInformatica.NTSGridColumn
    Me.ak_ump = New NTSInformatica.NTSGridColumn
    Me.ak_quant = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grAdrg, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvAdrg, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbRipristina, Me.tlbCancella, Me.tlbZoom, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 7
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
    Me.tlbZoom.Id = 4
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 5
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 6
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'grAdrg
    '
    Me.grAdrg.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grAdrg.EmbeddedNavigator.Name = ""
    Me.grAdrg.Location = New System.Drawing.Point(0, 30)
    Me.grAdrg.MainView = Me.grvAdrg
    Me.grAdrg.Name = "grAdrg"
    Me.grAdrg.Size = New System.Drawing.Size(682, 266)
    Me.grAdrg.TabIndex = 7
    Me.grAdrg.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvAdrg})
    '
    'grvAdrg
    '
    Me.grvAdrg.ActiveFilterEnabled = False
    Me.grvAdrg.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ak_riga, Me.ak_codart, Me.xx_codart, Me.ak_unmis, Me.ak_colli, Me.ak_ump, Me.ak_quant})
    Me.grvAdrg.CustomizationFormBounds = New System.Drawing.Rectangle(592, 326, 208, 170)
    Me.grvAdrg.Enabled = True
    Me.grvAdrg.GridControl = Me.grAdrg
    Me.grvAdrg.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvAdrg.Name = "grvAdrg"
    Me.grvAdrg.NTSAllowDelete = True
    Me.grvAdrg.NTSAllowInsert = True
    Me.grvAdrg.NTSAllowUpdate = True
    Me.grvAdrg.NTSMenuContext = Nothing
    Me.grvAdrg.OptionsCustomization.AllowRowSizing = True
    Me.grvAdrg.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvAdrg.OptionsNavigation.UseTabKey = False
    Me.grvAdrg.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvAdrg.OptionsView.ColumnAutoWidth = False
    Me.grvAdrg.OptionsView.EnableAppearanceEvenRow = True
    Me.grvAdrg.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvAdrg.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvAdrg.OptionsView.ShowGroupPanel = False
    Me.grvAdrg.RowHeight = 16
    '
    'ak_riga
    '
    Me.ak_riga.AppearanceCell.Options.UseBackColor = True
    Me.ak_riga.AppearanceCell.Options.UseTextOptions = True
    Me.ak_riga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ak_riga.Caption = "Riga"
    Me.ak_riga.Enabled = False
    Me.ak_riga.FieldName = "ak_riga"
    Me.ak_riga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ak_riga.Name = "ak_riga"
    Me.ak_riga.NTSRepositoryComboBox = Nothing
    Me.ak_riga.NTSRepositoryItemCheck = Nothing
    Me.ak_riga.NTSRepositoryItemText = Nothing
    Me.ak_riga.OptionsColumn.AllowEdit = False
    Me.ak_riga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ak_riga.OptionsColumn.ReadOnly = True
    Me.ak_riga.OptionsFilter.AllowFilter = False
    Me.ak_riga.Visible = True
    Me.ak_riga.VisibleIndex = 0
    Me.ak_riga.Width = 45
    '
    'ak_codart
    '
    Me.ak_codart.AppearanceCell.Options.UseBackColor = True
    Me.ak_codart.AppearanceCell.Options.UseTextOptions = True
    Me.ak_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ak_codart.Caption = "Articolo"
    Me.ak_codart.Enabled = True
    Me.ak_codart.FieldName = "ak_codart"
    Me.ak_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ak_codart.Name = "ak_codart"
    Me.ak_codart.NTSRepositoryComboBox = Nothing
    Me.ak_codart.NTSRepositoryItemCheck = Nothing
    Me.ak_codart.NTSRepositoryItemText = Nothing
    Me.ak_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ak_codart.OptionsFilter.AllowFilter = False
    Me.ak_codart.Visible = True
    Me.ak_codart.VisibleIndex = 1
    Me.ak_codart.Width = 66
    '
    'xx_codart
    '
    Me.xx_codart.AppearanceCell.Options.UseBackColor = True
    Me.xx_codart.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codart.Caption = "Descrizione"
    Me.xx_codart.Enabled = False
    Me.xx_codart.FieldName = "xx_codart"
    Me.xx_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codart.Name = "xx_codart"
    Me.xx_codart.NTSRepositoryComboBox = Nothing
    Me.xx_codart.NTSRepositoryItemCheck = Nothing
    Me.xx_codart.NTSRepositoryItemText = Nothing
    Me.xx_codart.OptionsColumn.AllowEdit = False
    Me.xx_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codart.OptionsColumn.ReadOnly = True
    Me.xx_codart.OptionsFilter.AllowFilter = False
    Me.xx_codart.Visible = True
    Me.xx_codart.VisibleIndex = 2
    Me.xx_codart.Width = 81
    '
    'ak_unmis
    '
    Me.ak_unmis.AppearanceCell.Options.UseBackColor = True
    Me.ak_unmis.AppearanceCell.Options.UseTextOptions = True
    Me.ak_unmis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ak_unmis.Caption = "U.M."
    Me.ak_unmis.Enabled = False
    Me.ak_unmis.FieldName = "ak_unmis"
    Me.ak_unmis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ak_unmis.Name = "ak_unmis"
    Me.ak_unmis.NTSRepositoryComboBox = Nothing
    Me.ak_unmis.NTSRepositoryItemCheck = Nothing
    Me.ak_unmis.NTSRepositoryItemText = Nothing
    Me.ak_unmis.OptionsColumn.AllowEdit = False
    Me.ak_unmis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ak_unmis.OptionsColumn.ReadOnly = True
    Me.ak_unmis.OptionsFilter.AllowFilter = False
    Me.ak_unmis.Visible = True
    Me.ak_unmis.VisibleIndex = 3
    Me.ak_unmis.Width = 37
    '
    'ak_colli
    '
    Me.ak_colli.AppearanceCell.Options.UseBackColor = True
    Me.ak_colli.AppearanceCell.Options.UseTextOptions = True
    Me.ak_colli.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ak_colli.Caption = "Colli"
    Me.ak_colli.Enabled = True
    Me.ak_colli.FieldName = "ak_colli"
    Me.ak_colli.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ak_colli.Name = "ak_colli"
    Me.ak_colli.NTSRepositoryComboBox = Nothing
    Me.ak_colli.NTSRepositoryItemCheck = Nothing
    Me.ak_colli.NTSRepositoryItemText = Nothing
    Me.ak_colli.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ak_colli.OptionsFilter.AllowFilter = False
    Me.ak_colli.Visible = True
    Me.ak_colli.VisibleIndex = 4
    Me.ak_colli.Width = 76
    '
    'ak_ump
    '
    Me.ak_ump.AppearanceCell.Options.UseBackColor = True
    Me.ak_ump.AppearanceCell.Options.UseTextOptions = True
    Me.ak_ump.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ak_ump.Caption = "UMP"
    Me.ak_ump.Enabled = False
    Me.ak_ump.FieldName = "ak_ump"
    Me.ak_ump.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ak_ump.Name = "ak_ump"
    Me.ak_ump.NTSRepositoryComboBox = Nothing
    Me.ak_ump.NTSRepositoryItemCheck = Nothing
    Me.ak_ump.NTSRepositoryItemText = Nothing
    Me.ak_ump.OptionsColumn.AllowEdit = False
    Me.ak_ump.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ak_ump.OptionsColumn.ReadOnly = True
    Me.ak_ump.OptionsFilter.AllowFilter = False
    Me.ak_ump.Visible = True
    Me.ak_ump.VisibleIndex = 5
    Me.ak_ump.Width = 36
    '
    'ak_quant
    '
    Me.ak_quant.AppearanceCell.Options.UseBackColor = True
    Me.ak_quant.AppearanceCell.Options.UseTextOptions = True
    Me.ak_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ak_quant.Caption = "Quantità"
    Me.ak_quant.Enabled = True
    Me.ak_quant.FieldName = "ak_quant"
    Me.ak_quant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ak_quant.Name = "ak_quant"
    Me.ak_quant.NTSRepositoryComboBox = Nothing
    Me.ak_quant.NTSRepositoryItemCheck = Nothing
    Me.ak_quant.NTSRepositoryItemText = Nothing
    Me.ak_quant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ak_quant.OptionsFilter.AllowFilter = False
    Me.ak_quant.Visible = True
    Me.ak_quant.VisibleIndex = 6
    Me.ak_quant.Width = 70
    '
    'FRMVEADRG
    '
    Me.ClientSize = New System.Drawing.Size(682, 296)
    Me.Controls.Add(Me.grAdrg)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMVEADRG"
    Me.Text = "INTEGRA RIGHE DOCUMENTO"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grAdrg, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvAdrg, System.ComponentModel.ISupportInitialize).EndInit()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNVEADRG", "BEMGDOCU", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128704611604674454, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleDocu = CType(oTmp, CLEMGDOCU)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNVEADRG", strRemoteServer, strRemotePort)
    AddHandler oCleDocu.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleDocu.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

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
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvAdrg.NTSSetParam(oMenu, oApp.Tr(Me, 128705238829375000, "Griglia integra righe"))
      ak_riga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128705238829531250, "Riga"), "0", 9, 0, 999999999)
      ak_codart.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128705238829687500, "Articolo"), tabartico, True)
      xx_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128705238829843750, "Descrizione"), 0, True)
      ak_unmis.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128705238830000000, "U.M."), 3, True)
      ak_colli.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128705238830156250, "Colli"), oApp.FormatQta, 15)
      ak_ump.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128705238830312500, "UMP"), 3, True)
      ak_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128705238830468750, "Quantità"), oApp.FormatQta, 15)

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

#Region "Eventi di form"
  Public Overridable Sub FRMVEADRG_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'collego i dati
      If Not oCleDocu.AdrgApri(dsAdrg) Then
        Me.Close()
      End If
      dcAdrg.DataSource = dsAdrg.Tables("ADRG")
      dsAdrg.AcceptChanges()
      grAdrg.DataSource = dcAdrg

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMVEADRG_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      oCallParams.ctlPar1 = dsAdrg.Tables("ADRG")
      dcAdrg.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvAdrg.NTSNuovo()

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
      If Not grvAdrg.NTSDeleteRigaCorrente(dcAdrg, True) Then Return
      oCleDocu.AdrgSalva(True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvAdrg.NTSRipristinaRigaCorrenteBefore(dcAdrg, True) Then Return
      oCleDocu.AdrgRipristina(dcAdrg.Position, dcAdrg.Filter)
      grvAdrg.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
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


  Public Overridable Sub grvAdrg_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvAdrg.NTSBeforeRowUpdate
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
  Public Overridable Sub grvAdrg_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvAdrg.NTSFocusedRowChanged
    Try
      '-------------------------------------------------
      'se il codice magazzino è diverso da 0 blocco la colonna, diversamente la rendo editabile
      If NTSCStr(grvAdrg.GetFocusedRowCellValue(ak_codart).ToString.Trim) = "" Then
        grvAdrg.FocusedColumn = ak_codart
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
      dRes = grvAdrg.NTSSalvaRigaCorrente(dcAdrg, oCleDocu.AdrgRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleDocu.AdrgSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleDocu.AdrgRipristina(dcAdrg.Position, dcAdrg.Filter)
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