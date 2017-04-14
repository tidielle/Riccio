Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORGNOR
  Private Moduli_P As Integer = bsModOR
  Private ModuliExt_P As Integer = bsModExtORE
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

  Public oCleGnor As CLEORGNOR
  Public oCallParams As CLE__CLDP
  Public dsGnor As New DataSet
  Public dcGnor As BindingSource = New BindingSource()
  Public strTiporkDaProgramma As String = ""
  Public strElencoProgressivi As String = ""

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents grGnor As NTSInformatica.NTSGrid
  Public WithEvents grvGnor As NTSInformatica.NTSGridView
  Public WithEvents xx_seleziona As NTSInformatica.NTSGridColumn
  Public WithEvents td_anno As NTSInformatica.NTSGridColumn
  Public WithEvents td_numord As NTSInformatica.NTSGridColumn
  Public WithEvents td_serie As NTSInformatica.NTSGridColumn
  Public WithEvents td_datord As NTSInformatica.NTSGridColumn
  Public WithEvents td_totdoc As NTSInformatica.NTSGridColumn
  Public WithEvents td_conto As NTSInformatica.NTSGridColumn
  Public WithEvents xx_conto As NTSInformatica.NTSGridColumn

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMORGNOR))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grGnor = New NTSInformatica.NTSGrid
    Me.grvGnor = New NTSInformatica.NTSGridView
    Me.xx_seleziona = New NTSInformatica.NTSGridColumn
    Me.td_anno = New NTSInformatica.NTSGridColumn
    Me.td_numord = New NTSInformatica.NTSGridColumn
    Me.td_serie = New NTSInformatica.NTSGridColumn
    Me.td_datord = New NTSInformatica.NTSGridColumn
    Me.td_totdoc = New NTSInformatica.NTSGridColumn
    Me.td_conto = New NTSInformatica.NTSGridColumn
    Me.xx_conto = New NTSInformatica.NTSGridColumn
    Me.tlbGestioneOrdini = New NTSInformatica.NTSBarButtonItem
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grGnor, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvGnor, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbGestioneOrdini})
    Me.NtsBarManager1.MaxItemId = 18
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 15
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGestioneOrdini)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.Id = 16
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 4
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 5
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
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
    'grGnor
    '
    Me.grGnor.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grGnor.EmbeddedNavigator.Name = ""
    Me.grGnor.Location = New System.Drawing.Point(0, 30)
    Me.grGnor.MainView = Me.grvGnor
    Me.grGnor.Name = "grGnor"
    Me.grGnor.Size = New System.Drawing.Size(648, 412)
    Me.grGnor.TabIndex = 5
    Me.grGnor.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvGnor})
    '
    'grvGnor
    '
    Me.grvGnor.ActiveFilterEnabled = False
    Me.grvGnor.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_seleziona, Me.td_anno, Me.td_numord, Me.td_serie, Me.td_datord, Me.td_totdoc, Me.td_conto, Me.xx_conto})
    Me.grvGnor.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvGnor.Enabled = True
    Me.grvGnor.GridControl = Me.grGnor
    Me.grvGnor.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvGnor.Name = "grvGnor"
    Me.grvGnor.NTSAllowDelete = True
    Me.grvGnor.NTSAllowInsert = True
    Me.grvGnor.NTSAllowUpdate = True
    Me.grvGnor.NTSMenuContext = Nothing
    Me.grvGnor.OptionsCustomization.AllowRowSizing = True
    Me.grvGnor.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvGnor.OptionsNavigation.UseTabKey = False
    Me.grvGnor.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvGnor.OptionsView.ColumnAutoWidth = False
    Me.grvGnor.OptionsView.EnableAppearanceEvenRow = True
    Me.grvGnor.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvGnor.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvGnor.OptionsView.ShowGroupPanel = False
    Me.grvGnor.RowHeight = 16
    '
    'xx_seleziona
    '
    Me.xx_seleziona.AppearanceCell.Options.UseBackColor = True
    Me.xx_seleziona.AppearanceCell.Options.UseTextOptions = True
    Me.xx_seleziona.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_seleziona.Caption = "Seleziona"
    Me.xx_seleziona.Enabled = True
    Me.xx_seleziona.FieldName = "xx_seleziona"
    Me.xx_seleziona.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_seleziona.Name = "xx_seleziona"
    Me.xx_seleziona.NTSRepositoryComboBox = Nothing
    Me.xx_seleziona.NTSRepositoryItemCheck = Nothing
    Me.xx_seleziona.NTSRepositoryItemMemo = Nothing
    Me.xx_seleziona.NTSRepositoryItemText = Nothing
    Me.xx_seleziona.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_seleziona.OptionsFilter.AllowFilter = False
    Me.xx_seleziona.Visible = True
    Me.xx_seleziona.VisibleIndex = 0
    Me.xx_seleziona.Width = 70
    '
    'td_anno
    '
    Me.td_anno.AppearanceCell.Options.UseBackColor = True
    Me.td_anno.AppearanceCell.Options.UseTextOptions = True
    Me.td_anno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_anno.Caption = "Anno"
    Me.td_anno.Enabled = False
    Me.td_anno.FieldName = "td_anno"
    Me.td_anno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_anno.Name = "td_anno"
    Me.td_anno.NTSRepositoryComboBox = Nothing
    Me.td_anno.NTSRepositoryItemCheck = Nothing
    Me.td_anno.NTSRepositoryItemMemo = Nothing
    Me.td_anno.NTSRepositoryItemText = Nothing
    Me.td_anno.OptionsColumn.AllowEdit = False
    Me.td_anno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_anno.OptionsColumn.ReadOnly = True
    Me.td_anno.OptionsFilter.AllowFilter = False
    Me.td_anno.Visible = True
    Me.td_anno.VisibleIndex = 1
    Me.td_anno.Width = 70
    '
    'td_numord
    '
    Me.td_numord.AppearanceCell.Options.UseBackColor = True
    Me.td_numord.AppearanceCell.Options.UseTextOptions = True
    Me.td_numord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_numord.Caption = "Numero"
    Me.td_numord.Enabled = False
    Me.td_numord.FieldName = "td_numord"
    Me.td_numord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_numord.Name = "td_numord"
    Me.td_numord.NTSRepositoryComboBox = Nothing
    Me.td_numord.NTSRepositoryItemCheck = Nothing
    Me.td_numord.NTSRepositoryItemMemo = Nothing
    Me.td_numord.NTSRepositoryItemText = Nothing
    Me.td_numord.OptionsColumn.AllowEdit = False
    Me.td_numord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_numord.OptionsColumn.ReadOnly = True
    Me.td_numord.OptionsFilter.AllowFilter = False
    Me.td_numord.Visible = True
    Me.td_numord.VisibleIndex = 2
    Me.td_numord.Width = 70
    '
    'td_serie
    '
    Me.td_serie.AppearanceCell.Options.UseBackColor = True
    Me.td_serie.AppearanceCell.Options.UseTextOptions = True
    Me.td_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_serie.Caption = "Serie"
    Me.td_serie.Enabled = False
    Me.td_serie.FieldName = "td_serie"
    Me.td_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_serie.Name = "td_serie"
    Me.td_serie.NTSRepositoryComboBox = Nothing
    Me.td_serie.NTSRepositoryItemCheck = Nothing
    Me.td_serie.NTSRepositoryItemMemo = Nothing
    Me.td_serie.NTSRepositoryItemText = Nothing
    Me.td_serie.OptionsColumn.AllowEdit = False
    Me.td_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_serie.OptionsColumn.ReadOnly = True
    Me.td_serie.OptionsFilter.AllowFilter = False
    Me.td_serie.Visible = True
    Me.td_serie.VisibleIndex = 3
    Me.td_serie.Width = 70
    '
    'td_datord
    '
    Me.td_datord.AppearanceCell.Options.UseBackColor = True
    Me.td_datord.AppearanceCell.Options.UseTextOptions = True
    Me.td_datord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_datord.Caption = "Data"
    Me.td_datord.Enabled = False
    Me.td_datord.FieldName = "td_datord"
    Me.td_datord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_datord.Name = "td_datord"
    Me.td_datord.NTSRepositoryComboBox = Nothing
    Me.td_datord.NTSRepositoryItemCheck = Nothing
    Me.td_datord.NTSRepositoryItemMemo = Nothing
    Me.td_datord.NTSRepositoryItemText = Nothing
    Me.td_datord.OptionsColumn.AllowEdit = False
    Me.td_datord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_datord.OptionsColumn.ReadOnly = True
    Me.td_datord.OptionsFilter.AllowFilter = False
    Me.td_datord.Visible = True
    Me.td_datord.VisibleIndex = 4
    Me.td_datord.Width = 70
    '
    'td_totdoc
    '
    Me.td_totdoc.AppearanceCell.Options.UseBackColor = True
    Me.td_totdoc.AppearanceCell.Options.UseTextOptions = True
    Me.td_totdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_totdoc.Caption = "Totale doc."
    Me.td_totdoc.Enabled = False
    Me.td_totdoc.FieldName = "td_totdoc"
    Me.td_totdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_totdoc.Name = "td_totdoc"
    Me.td_totdoc.NTSRepositoryComboBox = Nothing
    Me.td_totdoc.NTSRepositoryItemCheck = Nothing
    Me.td_totdoc.NTSRepositoryItemMemo = Nothing
    Me.td_totdoc.NTSRepositoryItemText = Nothing
    Me.td_totdoc.OptionsColumn.AllowEdit = False
    Me.td_totdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_totdoc.OptionsColumn.ReadOnly = True
    Me.td_totdoc.OptionsFilter.AllowFilter = False
    Me.td_totdoc.Visible = True
    Me.td_totdoc.VisibleIndex = 5
    Me.td_totdoc.Width = 70
    '
    'td_conto
    '
    Me.td_conto.AppearanceCell.Options.UseBackColor = True
    Me.td_conto.AppearanceCell.Options.UseTextOptions = True
    Me.td_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_conto.Caption = "Fornitore"
    Me.td_conto.Enabled = False
    Me.td_conto.FieldName = "td_conto"
    Me.td_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_conto.Name = "td_conto"
    Me.td_conto.NTSRepositoryComboBox = Nothing
    Me.td_conto.NTSRepositoryItemCheck = Nothing
    Me.td_conto.NTSRepositoryItemMemo = Nothing
    Me.td_conto.NTSRepositoryItemText = Nothing
    Me.td_conto.OptionsColumn.AllowEdit = False
    Me.td_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_conto.OptionsColumn.ReadOnly = True
    Me.td_conto.OptionsFilter.AllowFilter = False
    Me.td_conto.Visible = True
    Me.td_conto.VisibleIndex = 6
    Me.td_conto.Width = 70
    '
    'xx_conto
    '
    Me.xx_conto.AppearanceCell.Options.UseBackColor = True
    Me.xx_conto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_conto.Caption = "Descr. forn."
    Me.xx_conto.Enabled = False
    Me.xx_conto.FieldName = "xx_conto"
    Me.xx_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_conto.Name = "xx_conto"
    Me.xx_conto.NTSRepositoryComboBox = Nothing
    Me.xx_conto.NTSRepositoryItemCheck = Nothing
    Me.xx_conto.NTSRepositoryItemMemo = Nothing
    Me.xx_conto.NTSRepositoryItemText = Nothing
    Me.xx_conto.OptionsColumn.AllowEdit = False
    Me.xx_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_conto.OptionsColumn.ReadOnly = True
    Me.xx_conto.OptionsFilter.AllowFilter = False
    Me.xx_conto.Visible = True
    Me.xx_conto.VisibleIndex = 7
    Me.xx_conto.Width = 70
    '
    'tlbGestioneOrdini
    '
    Me.tlbGestioneOrdini.Caption = "Gestione ordini"
    Me.tlbGestioneOrdini.Enabled = False
    Me.tlbGestioneOrdini.Id = 17
    Me.tlbGestioneOrdini.Name = "tlbGestioneOrdini"
    Me.tlbGestioneOrdini.Visible = True
    '
    'FRMORGNOR
    '
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.grGnor)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMORGNOR"
    Me.NTSLastControlFocussed = Me.grGnor
    Me.Text = "GENERA ORDINI DA PROP. D'ORDINE"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grGnor, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvGnor, System.ComponentModel.ISupportInitialize).EndInit()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNORGNOR", "BEORGNOR", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128607611686875000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleGnor = CType(oTmp, CLEORGNOR)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNORGNOR", strRemoteServer, strRemotePort)
    AddHandler oCleGnor.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleGnor.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function


  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)

    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvGnor.NTSSetParam(oMenu, "GENERA ORDINI DA PROP. D'ORDINE")
      xx_seleziona.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128607611685625000, "Seleziona"), "S", "N")
      td_anno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128607611685781250, "Anno"), "0", 4, 1900, 2099)
      td_numord.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128607611685937500, "Numero"), "0", 1, 0, 999999999)
      td_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128607611686093750, "Serie"), CLN__STD.SerieMaxLen, False)
      td_datord.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128607611686250000, "Data"), False, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      td_totdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128607611686406250, "Totale doc."), oApp.FormatImporti, 9, -999999999, 999999999)
      td_conto.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128607611686562500, "Fornitore"), tabanagra)
      xx_conto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128607611686718750, "Descr. forn."), 0, True)
      grvGnor.NTSAllowInsert = False
      grvGnor.NTSAllowDelete = False

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
  Public Overridable Sub FRMORGNOR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      grGnor.Visible = False

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      If Not oCallParams Is Nothing Then
        strTiporkDaProgramma = oCallParams.strParam.Substring(5, 1)
        strElencoProgressivi = oCallParams.strParam.Substring(7)
        strElencoProgressivi = strElencoProgressivi.Substring(0, strElencoProgressivi.Length - 1)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMORGNOR_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      '-------------------
      'verifico che tabpeve e tabpeac siano compilate
      If Not oCleGnor.InitExt() Then
        Me.Close()
        Return
      End If

      '-------------------
      oCleGnor.bRaggrRigheComm = CBool(oMenu.GetSettingBus("BSORGNOR", "OPZIONI", ".", "RaggruppaRigheCommDiv", "0", " ", "0"))
      oCleGnor.strRaggruppareH = (oMenu.GetSettingBus("BSORGNOR", "OPZIONI", ".", "RaggruppareH", "S", " ", "S"))
      oCleGnor.bOrdinaperprogrRDO = CBool(oMenu.GetSettingBus("BSORGNOR", "OPZIONI", ".", "OrdinaperprogrRDO", "0", " ", "0"))
      oCleGnor.fRicalcPrez = NTSCInt(oMenu.GetSettingBus("BSORGNOR", "OPZIONI", ".", "RicalcolaPrezzi", "0", " ", "0"))
      oCleGnor.fRicalcScon = NTSCInt(oMenu.GetSettingBus("BSORGNOR", "OPZIONI", ".", "RicalcolaSconti", "0", " ", "0"))
      oCleGnor.nCodStabilimento = NTSCInt(oMenu.GetSettingBus("BSDBEMRP", "OPZIONIUT", ".", "CodStabilimentoPianificazione", "0", " ", "0"))
      oCleGnor.nDefaultCodPaga = NTSCInt(oMenu.GetSettingBus("BSORGNOR", "OPZIONI", ".", "DefaultCodPaga", "1", " ", "1"))
      If oMenu.ValCodiceDb(NTSCStr(oCleGnor.nDefaultCodPaga), DittaCorrente, "TABPAGA", "N") = False Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 129635659526285099, "Attenzione!" & vbCrLf & _
          "Il codice pagamento predefinito '|" & oCleGnor.nDefaultCodPaga.ToString & "|'," & vbCrLf & _
          "indicato nell'opzione di registro 'BNORGNOR/OPZIONI/DefaultCodPaga'," & vbCrLf & _
          "non Ã¨ definito nella tabella dei Codici Pagamento."))
      End If
      '-------------------
      'se sono stato chiamato da altri (BNORGSOL) elaboro e chiudo
      If Not oCallParams Is Nothing Then
        oCleGnor.strOrdlistTipork = strTiporkDaProgramma
        oCleGnor.strOrdlistProgressiviDaElab = strElencoProgressivi    'memorizzo l'elenco dei progressivi di cui generare gli ordini
        tlbNuovo.Enabled = False
        tlbNuovo_ItemClick(tlbNuovo, Nothing)
      Else
        oCleGnor.strOrdlistProgressiviDaElab = ""
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMORGNOR_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcGnor.Dispose()
      dsGnor.Dispose()
      If Not oCallParams Is Nothing Then oCallParams.strParam = "N" 'valore di ritorno se chiamato da BSORGSOL
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Dim frmSeos As FRMORSEOS = Nothing
    Dim frmSval As FRM__SVAL = Nothing
    Dim oPar As CLE__CLDP = Nothing

    Try
      If Not oCallParams Is Nothing Then
        '---------------------------------
        'Chiede estremi da attribuire al primo dei nuovi ordini
        'Se chiamato da programma esterno (BSORGSOL) passa alla modale il tipo documento
        oCleGnor.lConto = 0
        oCleGnor.lCommeca = 0
        oCleGnor.nMagaz = 0
        oCleGnor.strDatconsini = NTSCDate(IntSetDate("01/01/1900")).ToShortDateString
        oCleGnor.strDatconsfin = NTSCDate(IntSetDate("31/12/2099")).ToShortDateString
        oCleGnor.strDatordini = NTSCDate(IntSetDate("01/01/1900")).ToShortDateString
        oCleGnor.strDatordfin = NTSCDate(IntSetDate("31/12/2099")).ToShortDateString
        oCleGnor.bGenerato = True
        oCleGnor.bConfermato = True
        oCleGnor.bEmRDA = True
        oCleGnor.bAppRDA = True
        oCleGnor.bEmRDO = True
        oCleGnor.bCongelato = True
        oCleGnor.bEmOrdine = True
      Else
        '-------------------------
        'visualizzo la form per la selezione delle proposte d'ordine
        'in oPar.strPar1 ricevo la stringa di selezione scadenze
        oPar = New CLE__CLDP
        oPar.strPar1 = ""
        oPar.strPar2 = "BNORGNOR:SELDOCU"
        oMenu.RunChild("NTSInformatica", "FRMORSEOL", "", DittaCorrente, "", "BNORSEOL", oPar, "", True, True)
        If oPar.strParam = "" Then Return 'ho annullato

        oCleGnor.strOrdlistTipork = oPar.strParam.Substring(0, 1)
        oCleGnor.strWhereArtico = oPar.strPar1
        oCleGnor.bOpInterni = oPar.bPar1
        oCleGnor.lConto = NTSCInt(oPar.dPar1)
        oCleGnor.lCommeca = NTSCInt(oPar.dPar2)
        oCleGnor.nMagaz = NTSCInt(oPar.dPar3)
        oCleGnor.strDatconsini = NTSCDate(oPar.strPar2).ToShortDateString
        oCleGnor.strDatconsfin = NTSCDate(oPar.strPar3).ToShortDateString
        oCleGnor.strDatordini = NTSCDate(oPar.strPar4).ToShortDateString
        oCleGnor.strDatordfin = NTSCDate(oPar.strPar5).ToShortDateString
        oCleGnor.bGenerato = CBool(IIf(oPar.strParam.Substring(1, 1) = "S", True, False))
        oCleGnor.bConfermato = CBool(IIf(oPar.strParam.Substring(2, 1) = "S", True, False))
        oCleGnor.bEmRDA = CBool(IIf(oPar.strParam.Substring(3, 1) = "S", True, False))
        oCleGnor.bAppRDA = CBool(IIf(oPar.strParam.Substring(4, 1) = "S", True, False))
        oCleGnor.bEmRDO = CBool(IIf(oPar.strParam.Substring(5, 1) = "S", True, False))
        oCleGnor.bCongelato = CBool(IIf(oPar.strParam.Substring(6, 1) = "S", True, False))
        oCleGnor.bEmOrdine = CBool(IIf(oPar.strParam.Substring(7, 1) = "S", True, False))
      End If    'If Not oCallParams Is Nothing Then
      oCleGnor.bModRA = False
      If CBool((oMenu.ModuliDittaDitt(DittaCorrente) And bsModRA)) Then
        Select Case oCleGnor.strOrdlistTipork
          Case "O" : oCleGnor.bModRA = True
          Case "H" : If oCleGnor.bOpInterni = False Then oCleGnor.bModRA = True
        End Select
      End If

prcFF:
      '--------------------------
      'visualizzo la form per la selezione degli estremi del documento da generare
      frmSeos = CType(NTSNewFormModal("FRMORSEOS"), FRMORSEOS)
      frmSeos.Init(oMenu, Nothing, DittaCorrente)
      frmSeos.InitEntity(oCleGnor)
      frmSeos.cbTipo.SelectedValue = oCleGnor.strOrdlistTipork
      frmSeos.ShowDialog()
      If frmSeos.bOk = False Then
        If Not oCallParams Is Nothing Then
          'se sono stato chiamato da bsorgsol esco
          Me.Close()
        End If
        Return
      End If

      '---------------------------
      'Se chiamato da programma esterno (BSORGSOL) il tipo documento selezionato
      'deve essere quello passato da BSORGSOL
      If Not oCallParams Is Nothing Then
        If oCleGnor.strOrdlistTipork <> strTiporkDaProgramma Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128607745044375000, "Il tipo documento selezionato deve essere dello stesso tipo delle proposte d'ordine di origine."))
          GoTo prcFF
        End If
      End If
      '----------------------------
      'Setta il tipo di documento
      If oCallParams Is Nothing And (Not (oCleGnor.strOrdlistTipork = frmSeos.cbTipo.SelectedValue)) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128607745685000000, "Indicare un tipo di documento coerente. "))
        GoTo prcFF
      End If

      oCleGnor.nAnnoNewDoc = NTSCInt(frmSeos.edAnno.Text)
      oCleGnor.strSerieNewDoc = frmSeos.edSerie.Text
      oCleGnor.lNumNewDoc = NTSCInt(frmSeos.edNumero.Text)
      oCleGnor.nTipoBfNewDoc = NTSCInt(frmSeos.edTipobf.Text)
      oCleGnor.nAnnoTcNewDoc = NTSCInt(frmSeos.edAnnoTc.Text)
      oCleGnor.nStagioneNewDoc = NTSCInt(frmSeos.edStagione.Text)
      oCleGnor.strDataNewDoc = frmSeos.edDatdoc.Text


      '----------------------
      'Chiede come operare con le valute
      If oCleGnor.CheckPropValuta(DittaCorrente, oCleGnor.strOrdlistTipork) Then
        'visualizzo la form della definizione del cambio solo se sono presenti righe in valuta 
        frmSval = CType(NTSNewFormModal("FRM__SVAL"), FRM__SVAL)
        frmSval.Init(oMenu, Nothing, DittaCorrente, Nothing)
        frmSval.ShowDialog()
        oCleGnor.nSvalOpzione = frmSval.nOptionOut
      Else
        oCleGnor.nSvalOpzione = 3
      End If

      If oCleGnor.nSvalOpzione = 0 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, oCleGnor.nSvalOpzione, "Operazione annullata"))
        Return
      End If
      'nSvalOpzione: 1 cambio da ordlist, 2 = cambio di tabvalu al cambio del documento, 3, cambio di tabvalu piÃ¹ vicina ala data del documento

      '--------------------------------
      'inizio l'elaborazione
      Me.Cursor = Cursors.WaitCursor
      If Not oCleGnor.LogStart("BNORGNOR", "GENERA ORDINI DA PROPOSTE D'ORDINE") Then Return
      If Not oCleGnor.Elabora(dsGnor) Then
        Me.Cursor = Cursors.Default
        oCleGnor.LogStop()
        If oCleGnor.LogError = True Then
          If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129151178583076172, "Esistono dei messaggi nel file di log. Visualizzare il file?")) = Windows.Forms.DialogResult.Yes Then
            NTSProcessStart("notepad", oCleGnor.LogFileName)
          End If
        End If
        Return
      End If
      Me.Cursor = Cursors.Default
      oCleGnor.LogStop()


      oApp.MsgBoxInfo(oApp.Tr(Me, 128608590069062500, "Elaborazione completata. Generati n. |" & oCleGnor.lGiafat.ToString & "| ordini/documenti."))
      If oCleGnor.lGiafat <> 0 Then GctlSetVisEnab(grGnor, True) : GctlSetVisEnab(tlbGestioneOrdini, False)

      If oCleGnor.LogError = True Then
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129151177987128906, "Esistono dei messaggi nel file di log. Visualizzare il file?")) = Windows.Forms.DialogResult.Yes Then
          NTSProcessStart("notepad", oCleGnor.LogFileName)
        End If
      End If

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      dcGnor.DataSource = dsGnor.Tables("GNOR")
      dsGnor.AcceptChanges()
      grGnor.DataSource = dcGnor

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmSeos Is Nothing Then frmSeos.Dispose()
      frmSeos = Nothing
      If Not frmSval Is Nothing Then frmSval.Dispose()
      frmSval = Nothing
      Me.Cursor = Cursors.Default
      oCleGnor.LogStop()
    End Try
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      If grvGnor.RowCount = 0 Then Return
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      If grvGnor.RowCount = 0 Then Return
      Stampa(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGestioneOrdini_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGestioneOrdini.ItemClick
    Dim strParam As String
    Try
      If grvGnor.NTSGetCurrentDataRow Is Nothing Then Return

      With grvGnor.NTSGetCurrentDataRow
        strParam = "APRI;" & oCleGnor.strOrdlistTipork & ";" & NTSCStr(!td_anno) & ";" & NTSCStr(!td_serie) & ";" & NTSCStr(!td_numord)
      End With

      oMenu.RunChild("BSORGSOR", "CLSORGSOR", oApp.Tr(Me, 129318613639333206, "Gestione ordini"), DittaCorrente, , , , strParam, True, False)
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

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub
#End Region

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object = Nothing
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer
    Dim dtrT() As DataRow = Nothing
    Dim nGiro As Integer = 0
    Dim strTmp As String = ""
    Dim lNumMin As Integer = 0
    Dim lNumMax As Integer = 0

    Try
      '--------------------------------------------------
      'test pre-stampa
      If dsGnor.Tables.Count = 0 Then Return
      If dsGnor.Tables("GNOR").Rows.Count = 0 Then Return
      dsGnor.Tables("GNOR").AcceptChanges()
      dtrT = dsGnor.Tables("GNOR").Select("xx_seleziona = 'S'")
      If dtrT.Length = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128611101258125000, "Non è stata selezionato nessun ordine."))
        Return
      End If

      lNumMin = NTSCInt(dsGnor.Tables("GNOR").Select("xx_seleziona = 'S'", "td_numord ASC")(0)!td_numord)
      lNumMax = NTSCInt(dsGnor.Tables("GNOR").Select("xx_seleziona = 'S'", "td_numord DESC")(0)!td_numord)

      For nGiro = 0 To 3
        'documento normale
        If nGiro = 0 Then strTmp = "xx_seleziona = 'S' AND td_valuta = 0"
        'documento in valuta
        If nGiro = 1 Then strTmp = "xx_seleziona = 'S' AND td_valuta > 0"
        'documento con scorporo
        If nGiro = 2 Then strTmp = "xx_seleziona = 'S' AND td_scorpo = 'S'"
        dtrT = dsGnor.Tables("GNOR").Select(strTmp)
        If dtrT.Length > 0 Then
          '--------------------------------------------------
          'preparo il motore di stampa
          strCrpe = "{testord.codditt} = '" & DittaCorrente & "'" & _
                    " And {testord.td_tipork} = '" & oCleGnor.strOrdlistTipork & "'" & _
                    " And {testord.td_anno} = " & oCleGnor.nAnnoNewDoc & _
                    " And {testord.td_serie} = '" & oCleGnor.strSerieNewDoc & "'" & _
                    " And {testord.td_numord} >= " & lNumMin.ToString & _
                    " And {testord.td_numord} <= " & lNumMax.ToString & _
                    " And {movord.mo_stasino} <> 'N'" & _
                    " And {TESTORD.td_magaz2} <> {KEYORD.ko_magaz} "
          'documento normale
          If nGiro = 0 Then strCrpe += " AND {testord.td_scorpo} = 'N' AND {testord.td_valuta} = 0"
          'documento in valuta
          If nGiro = 1 Then strCrpe += " AND {testord.td_scorpo} = 'N' AND {testord.td_valuta} > 0"
          'documento con scorporo
          If nGiro = 2 Then strTmp = " AND {testord.td_scorpo} = 'S' AND {testord.td_valuta} > 0"

          strCrpe += " AND ("
          For i = 0 To dtrT.Length - 1
            If i > 0 Then strCrpe += " OR "
            strCrpe += " {testord.td_numord} = " & dtrT(i)!td_numord.ToString
          Next
          strCrpe += ")"

          If nGiro = 0 Then nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSORGSOR", "Reports1", oCleGnor.strOrdlistTipork, 0, nDestin, "BSORGSOR.RPT", False, "Stampa Ordine", False)
          If nGiro = 1 Then nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSORGSOR", "Reports3", oCleGnor.strOrdlistTipork, 0, nDestin, "BSORGSOR.RPT", False, "Stampa Ordine", False)
          If nGiro = 2 Then nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSORGSOR", "Reports2", oCleGnor.strOrdlistTipork, 0, nDestin, "BSORGSOR.RPT", False, "Stampa Ordine", False)
          If nPjob Is Nothing Then Return

          '--------------------------------------------------
          'lancio tutti gli eventuali reports (gestisce già il multireport)
          For i = 1 To UBound(CType(nPjob, Array), 2)
            nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
            nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
          Next
        End If    'If dtrT.Length > 0 Then
      Next    'For nGiro = 0 To 3

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
End Class
