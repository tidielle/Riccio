Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGHLAV
#Region "Variabili"
  Public oCleArmd As CLEMGARMD
  Public oCallParams As CLE__CLDP
  Public dsHlav As New DataSet
  Public dcHlav As New BindingSource()
  Public dttHlav As DataTable = Nothing
  Public dtrHlav() As DataRow = Nothing
  Public components As System.ComponentModel.IContainer

  Public bServer As Boolean

  Public WithEvents grHlav As NTSInformatica.NTSGrid
  Public WithEvents grvHlav As NTSInformatica.NTSGridView
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem

  Public WithEvents codditt As NTSInformatica.NTSGridColumn
  Public WithEvents ax_codvalu As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codvalu As NTSInformatica.NTSGridColumn
  Public WithEvents ax_desint As NTSInformatica.NTSGridColumn
  Public WithEvents ax_ultagg As NTSInformatica.NTSGridColumn
  Public WithEvents ax_codart As NTSInformatica.NTSGridColumn
  Public WithEvents ax_note As NTSInformatica.NTSGridColumn
  Public WithEvents ax_descr As NTSInformatica.NTSGridColumn
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

    '------------------------------------------------
    'creo e attivo l'entity e inizializzo la funzione che dovr√† rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGARMD", "BEMGARMD", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128550728307822408, "ERRORE in fase di creazione Entity:" & vbCrLf & strErr))
      Return False
    End If
    oCleArmd = CType(oTmp, CLEMGARMD)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGARMD", strRemoteServer, strRemotePort)
    AddHandler oCleArmd.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleArmd.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    oCleArmd.strPrgParent = oCallParams.strPar1
    oCleArmd.strHlavCodart = oCallParams.strPar2
    oCleArmd.strHlavCartellaColore = oCallParams.strPar3
    oCleArmd.bHlavIsRoot = oCallParams.bPar1

    Return True
  End Function
  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGHLAV))
    Me.grHlav = New NTSInformatica.NTSGrid
    Me.grvHlav = New NTSInformatica.NTSGridView
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.ax_codart = New NTSInformatica.NTSGridColumn
    Me.ax_codvalu = New NTSInformatica.NTSGridColumn
    Me.xx_codvalu = New NTSInformatica.NTSGridColumn
    Me.ax_descr = New NTSInformatica.NTSGridColumn
    Me.ax_desint = New NTSInformatica.NTSGridColumn
    Me.ax_ultagg = New NTSInformatica.NTSGridColumn
    Me.ax_note = New NTSInformatica.NTSGridColumn
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
    CType(Me.grHlav, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvHlav, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Image = CType(resources.GetObject("frmPopup.Appearance.Image"), System.Drawing.Image)
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    Me.frmPopup.ClientSize = New System.Drawing.Size(77, 21)
    '
    'grHlav
    '
    Me.grHlav.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grHlav.EmbeddedNavigator.Name = ""
    Me.grHlav.Location = New System.Drawing.Point(0, 30)
    Me.grHlav.MainView = Me.grvHlav
    Me.grHlav.Name = "grHlav"
    Me.grHlav.Size = New System.Drawing.Size(667, 336)
    Me.grHlav.TabIndex = 0
    Me.grHlav.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvHlav})
    '
    'grvHlav
    '
    Me.grvHlav.ActiveFilterEnabled = False
    Me.grvHlav.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.codditt, Me.ax_codart, Me.ax_codvalu, Me.xx_codvalu, Me.ax_descr, Me.ax_desint, Me.ax_ultagg, Me.ax_note})
    Me.grvHlav.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvHlav.Enabled = True
    Me.grvHlav.GridControl = Me.grHlav
    Me.grvHlav.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvHlav.Name = "grvHlav"
    Me.grvHlav.NTSAllowDelete = True
    Me.grvHlav.NTSAllowInsert = True
    Me.grvHlav.NTSAllowUpdate = True
    Me.grvHlav.NTSMenuContext = Nothing
    Me.grvHlav.OptionsCustomization.AllowRowSizing = True
    Me.grvHlav.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvHlav.OptionsNavigation.UseTabKey = False
    Me.grvHlav.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvHlav.OptionsView.ColumnAutoWidth = False
    Me.grvHlav.OptionsView.EnableAppearanceEvenRow = True
    Me.grvHlav.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvHlav.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvHlav.OptionsView.ShowGroupPanel = False
    Me.grvHlav.RowHeight = 16
    '
    'codditt
    '
    Me.codditt.AppearanceCell.Options.UseBackColor = True
    Me.codditt.AppearanceCell.Options.UseTextOptions = True
    Me.codditt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.codditt.Caption = "Cod. ditta"
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
    'ax_codart
    '
    Me.ax_codart.AppearanceCell.Options.UseBackColor = True
    Me.ax_codart.AppearanceCell.Options.UseTextOptions = True
    Me.ax_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ax_codart.Caption = "AX_CODART"
    Me.ax_codart.Enabled = True
    Me.ax_codart.FieldName = "ax_codart"
    Me.ax_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ax_codart.Name = "ax_codart"
    Me.ax_codart.NTSRepositoryComboBox = Nothing
    Me.ax_codart.NTSRepositoryItemCheck = Nothing
    Me.ax_codart.NTSRepositoryItemText = Nothing
    Me.ax_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ax_codart.OptionsFilter.AllowFilter = False
    '
    'ax_codvalu
    '
    Me.ax_codvalu.AppearanceCell.Options.UseBackColor = True
    Me.ax_codvalu.AppearanceCell.Options.UseTextOptions = True
    Me.ax_codvalu.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ax_codvalu.Caption = "Codice"
    Me.ax_codvalu.Enabled = True
    Me.ax_codvalu.FieldName = "ax_codvalu"
    Me.ax_codvalu.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ax_codvalu.Name = "ax_codvalu"
    Me.ax_codvalu.NTSRepositoryComboBox = Nothing
    Me.ax_codvalu.NTSRepositoryItemCheck = Nothing
    Me.ax_codvalu.NTSRepositoryItemText = Nothing
    Me.ax_codvalu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ax_codvalu.OptionsFilter.AllowFilter = False
    Me.ax_codvalu.Visible = True
    Me.ax_codvalu.VisibleIndex = 0
    '
    'xx_codvalu
    '
    Me.xx_codvalu.AppearanceCell.Options.UseBackColor = True
    Me.xx_codvalu.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codvalu.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codvalu.Caption = "Descrizione"
    Me.xx_codvalu.Enabled = False
    Me.xx_codvalu.FieldName = "xx_codvalu"
    Me.xx_codvalu.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codvalu.Name = "xx_codvalu"
    Me.xx_codvalu.NTSRepositoryComboBox = Nothing
    Me.xx_codvalu.NTSRepositoryItemCheck = Nothing
    Me.xx_codvalu.NTSRepositoryItemText = Nothing
    Me.xx_codvalu.OptionsColumn.AllowEdit = False
    Me.xx_codvalu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codvalu.OptionsColumn.ReadOnly = True
    Me.xx_codvalu.OptionsFilter.AllowFilter = False
    Me.xx_codvalu.Visible = True
    Me.xx_codvalu.VisibleIndex = 1
    '
    'ax_descr
    '
    Me.ax_descr.AppearanceCell.Options.UseBackColor = True
    Me.ax_descr.AppearanceCell.Options.UseTextOptions = True
    Me.ax_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ax_descr.Caption = "Descrizione Articolo"
    Me.ax_descr.Enabled = True
    Me.ax_descr.FieldName = "ax_descr"
    Me.ax_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ax_descr.Name = "ax_descr"
    Me.ax_descr.NTSRepositoryComboBox = Nothing
    Me.ax_descr.NTSRepositoryItemCheck = Nothing
    Me.ax_descr.NTSRepositoryItemText = Nothing
    Me.ax_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ax_descr.OptionsFilter.AllowFilter = False
    Me.ax_descr.Visible = True
    Me.ax_descr.VisibleIndex = 2
    '
    'ax_desint
    '
    Me.ax_desint.AppearanceCell.Options.UseBackColor = True
    Me.ax_desint.AppearanceCell.Options.UseTextOptions = True
    Me.ax_desint.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ax_desint.Caption = "Descrizione Interna"
    Me.ax_desint.Enabled = True
    Me.ax_desint.FieldName = "ax_desint"
    Me.ax_desint.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ax_desint.Name = "ax_desint"
    Me.ax_desint.NTSRepositoryComboBox = Nothing
    Me.ax_desint.NTSRepositoryItemCheck = Nothing
    Me.ax_desint.NTSRepositoryItemText = Nothing
    Me.ax_desint.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ax_desint.OptionsFilter.AllowFilter = False
    Me.ax_desint.Visible = True
    Me.ax_desint.VisibleIndex = 3
    '
    'ax_ultagg
    '
    Me.ax_ultagg.AppearanceCell.Options.UseBackColor = True
    Me.ax_ultagg.AppearanceCell.Options.UseTextOptions = True
    Me.ax_ultagg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ax_ultagg.Caption = "AX_ULTAGG"
    Me.ax_ultagg.Enabled = True
    Me.ax_ultagg.FieldName = "ax_ultagg"
    Me.ax_ultagg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ax_ultagg.Name = "ax_ultagg"
    Me.ax_ultagg.NTSRepositoryComboBox = Nothing
    Me.ax_ultagg.NTSRepositoryItemCheck = Nothing
    Me.ax_ultagg.NTSRepositoryItemText = Nothing
    Me.ax_ultagg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ax_ultagg.OptionsFilter.AllowFilter = False
    '
    'ax_note
    '
    Me.ax_note.AppearanceCell.Options.UseBackColor = True
    Me.ax_note.AppearanceCell.Options.UseTextOptions = True
    Me.ax_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ax_note.Caption = "Note"
    Me.ax_note.Enabled = True
    Me.ax_note.FieldName = "ax_note"
    Me.ax_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ax_note.Name = "ax_note"
    Me.ax_note.NTSRepositoryComboBox = Nothing
    Me.ax_note.NTSRepositoryItemCheck = Nothing
    Me.ax_note.NTSRepositoryItemText = Nothing
    Me.ax_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ax_note.OptionsFilter.AllowFilter = False
    Me.ax_note.Visible = True
    Me.ax_note.VisibleIndex = 4
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 16
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
    Me.tlbZoom.Id = 4
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 9
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 10
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'FRMMGHLAV
    '
    Me.ClientSize = New System.Drawing.Size(667, 366)
    Me.Controls.Add(Me.grHlav)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.HelpContext = ""
    Me.Name = "FRMMGHLAV"
    Me.NTSLastControlFocussed = Me.grHlav
    Me.Text = "Descrizione articolo in lingua straniera"
    CType(Me.grHlav, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvHlav, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

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
        '  'non gestisco l'errore: se non c'Ë una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvHlav.NTSSetParam(oMenu, oApp.Tr(Me, 128828972637775055, "Descrizione articolo in lingua straniera"))
      codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828972654025159, "Cod. ditta"), 12, False)
      ax_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828972676681554, "AX_CODART"), CLN__STD.CodartMaxLen, False)
      ax_codvalu.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128828972693087909, "Codice"), tabling)
      xx_codvalu.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828972706525495, "Descrizione"), 0, True)
      ax_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828972721213089, "Descrizione Articolo"), 40, True)
      ax_desint.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828972736525687, "Descrizione Interna"), 40, True)
      ax_ultagg.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128828972750432026, "AX_ULTAGG"), False)
      ax_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828972772619668, "Note"), 0, True)

      ax_codvalu.NTSSetRichiesto()

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

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvHlav.NTSSalvaRigaCorrente(dcHlav, oCleArmd.HlavRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleArmd.HlavSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleArmd.HlavRipristina(dcHlav.Position, dcHlav.Filter)
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
  Public Overridable Sub FRMMGHLAV_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleArmd.HlavApri(DittaCorrente, oCleArmd.strHlavCodart, dsHlav) Then Me.Close()

      dcHlav.DataSource = dsHlav.Tables("ARTVAL")
      grHlav.DataSource = dcHlav

      ax_codvalu.Enabled = False

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
  Public Overridable Sub FRMMGHLAV_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcHlav.Dispose()
      dsHlav.Dispose()
    Catch
    End Try
  End Sub
  Public Overridable Sub FRMMGHLAV_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvHlav.NTSNuovo()
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
      If Not grvHlav.NTSDeleteRigaCorrente(dcHlav, True) Then Return
      oCleArmd.HlavSalva(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvHlav.NTSRipristinaRigaCorrenteBefore(dcHlav, True) Then Return
      oCleArmd.HlavRipristina(dcHlav.Position, dcHlav.Filter)
      grvHlav.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim ctrlTmp As Control = Nothing
    Dim oParam As New CLE__PATB

    Try
      '------------------------------------------------
      'questa funzione viene sempre scatenata quando premo F5 o ALT+F2 o ALT+F3 sulla griglia
      'zoom standard
      ctrlTmp = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return

      '------------------------------------
      'zoom standard di textbox e griglia
      NTSCallStandardZoom()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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
  Public Overridable Sub grvHlav_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvHlav.NTSBeforeRowUpdate
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

  Public Overridable Sub grvHlav_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvHlav.NTSFocusedRowChanged
    'blocco le colonne non modificabili
    Dim dtrT As DataRow = Nothing
    Try
      If oCleArmd Is Nothing Then Return

      dtrT = grvHlav.NTSGetCurrentDataRow
      '------------------------------------
      'sono su una nuova riga
      If dtrT Is Nothing Then
        ax_codvalu.Enabled = True
        Return
      End If

      If NTSCInt(dtrT!ax_codvalu) <> 0 Then
        ax_codvalu.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

End Class