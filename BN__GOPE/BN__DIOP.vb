Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__DIOP

#Region "Moduli"
  Private Moduli_P As Integer = CLN__STD.bsModAll
  Private ModuliExt_P As Integer = 0
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
#End Region

#Region "Variabili"
  Public oCleGope As CLE__GOPE
  Public oCallParams As CLE__CLDP
  Public dsDiop As DataSet
  Public dsDiopPro As DataSet
  Public dcDiop As BindingSource = New BindingSource()
  Public bIs15 As Boolean = True
  Public strNomeProg As String = ""

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbAbilita As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDisabilita As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbAggiungiDitte As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDettaglioProgrammi As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents grDiop As NTSInformatica.NTSGrid
  Public WithEvents grvDiop As NTSInformatica.NTSGridView
  Public WithEvents codditt As NTSInformatica.NTSGridColumn
  Public WithEvents opdi_abilit As NTSInformatica.NTSGridColumn
  Public WithEvents opdi_opnome As NTSInformatica.NTSGridColumn
  Public WithEvents opdi_crmvis As NTSInformatica.NTSGridColumn
  Public WithEvents opdi_crmmod As NTSInformatica.NTSGridColumn
  Public WithEvents opdi_amm As NTSInformatica.NTSGridColumn
  Public WithEvents opdi_codcage As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codcage As NTSInformatica.NTSGridColumn

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

  Public Overridable Sub InitEntity(ByRef CleGope As CLE__GOPE)
    ocleGope = CleGope
    AddHandler ocleGope.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__DIOP))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbAggiungiDitte = New NTSInformatica.NTSBarButtonItem
    Me.tlbDettaglioProgrammi = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.tlbAbilita = New NTSInformatica.NTSBarButtonItem
    Me.tlbDisabilita = New NTSInformatica.NTSBarButtonItem
    Me.grDiop = New NTSInformatica.NTSGrid
    Me.grvDiop = New NTSInformatica.NTSGridView
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.opdi_abilit = New NTSInformatica.NTSGridColumn
    Me.opdi_opnome = New NTSInformatica.NTSGridColumn
    Me.opdi_crmvis = New NTSInformatica.NTSGridColumn
    Me.opdi_crmmod = New NTSInformatica.NTSGridColumn
    Me.opdi_amm = New NTSInformatica.NTSGridColumn
    Me.opdi_codcage = New NTSInformatica.NTSGridColumn
    Me.xx_codcage = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grDiop, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvDiop, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci, Me.tlbNuovo, Me.tlbCancella, Me.tlbAggiungiDitte, Me.tlbDettaglioProgrammi, Me.tlbAbilita, Me.tlbDisabilita, Me.tlbZoom})
    Me.NtsBarManager1.MaxItemId = 37
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAggiungiDitte, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDettaglioProgrammi), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbNuovo.Id = 17
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
    Me.tlbCancella.Id = 18
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 36
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbAggiungiDitte
    '
    Me.tlbAggiungiDitte.Caption = "Aggiungi Ditte"
    Me.tlbAggiungiDitte.Glyph = CType(resources.GetObject("tlbAggiungiDitte.Glyph"), System.Drawing.Image)
    Me.tlbAggiungiDitte.Id = 35
    Me.tlbAggiungiDitte.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G))
    Me.tlbAggiungiDitte.Name = "tlbAggiungiDitte"
    Me.tlbAggiungiDitte.Visible = True
    '
    'tlbDettaglioProgrammi
    '
    Me.tlbDettaglioProgrammi.Caption = "Dettaglio programmi"
    Me.tlbDettaglioProgrammi.Glyph = CType(resources.GetObject("tlbDettaglioProgrammi.Glyph"), System.Drawing.Image)
    Me.tlbDettaglioProgrammi.Id = 19
    Me.tlbDettaglioProgrammi.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D))
    Me.tlbDettaglioProgrammi.Name = "tlbDettaglioProgrammi"
    Me.tlbDettaglioProgrammi.Visible = True
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
    'tlbAbilita
    '
    Me.tlbAbilita.Caption = "Abilita tutto"
    Me.tlbAbilita.Id = 33
    Me.tlbAbilita.Name = "tlbAbilita"
    Me.tlbAbilita.Visible = True
    '
    'tlbDisabilita
    '
    Me.tlbDisabilita.Caption = "Disabilita tutto"
    Me.tlbDisabilita.Id = 34
    Me.tlbDisabilita.Name = "tlbDisabilita"
    Me.tlbDisabilita.Visible = True
    '
    'grDiop
    '
    Me.grDiop.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '
    '
    '
    Me.grDiop.EmbeddedNavigator.Name = ""
    Me.grDiop.Location = New System.Drawing.Point(0, 28)
    Me.grDiop.MainView = Me.grvDiop
    Me.grDiop.Name = "grDiop"
    Me.grDiop.Size = New System.Drawing.Size(688, 230)
    Me.grDiop.TabIndex = 5
    Me.grDiop.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDiop})
    '
    'grvDiop
    '
    Me.grvDiop.ActiveFilterEnabled = False
    Me.grvDiop.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.codditt, Me.opdi_abilit, Me.opdi_opnome, Me.opdi_crmvis, Me.opdi_crmmod, Me.opdi_amm, Me.opdi_codcage, Me.xx_codcage})
    Me.grvDiop.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvDiop.Enabled = True
    Me.grvDiop.GridControl = Me.grDiop
    Me.grvDiop.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvDiop.Name = "grvDiop"
    Me.grvDiop.NTSAllowDelete = True
    Me.grvDiop.NTSAllowInsert = True
    Me.grvDiop.NTSAllowUpdate = True
    Me.grvDiop.NTSMenuContext = Nothing
    Me.grvDiop.OptionsCustomization.AllowRowSizing = True
    Me.grvDiop.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvDiop.OptionsNavigation.UseTabKey = False
    Me.grvDiop.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvDiop.OptionsView.ColumnAutoWidth = False
    Me.grvDiop.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvDiop.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvDiop.OptionsView.ShowGroupPanel = False
    Me.grvDiop.RowHeight = 16
    '
    'codditt
    '
    Me.codditt.AppearanceCell.Options.UseBackColor = True
    Me.codditt.AppearanceCell.Options.UseTextOptions = True
    Me.codditt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.codditt.Caption = "Ditta"
    Me.codditt.Enabled = True
    Me.codditt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.codditt.Name = "codditt"
    Me.codditt.NTSRepositoryComboBox = Nothing
    Me.codditt.NTSRepositoryItemCheck = Nothing
    Me.codditt.NTSRepositoryItemMemo = Nothing
    Me.codditt.NTSRepositoryItemText = Nothing
    Me.codditt.OptionsColumn.AllowEdit = False
    Me.codditt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.codditt.OptionsFilter.AllowFilter = False
    Me.codditt.Visible = True
    Me.codditt.VisibleIndex = 0
    Me.codditt.Width = 136
    '
    'opdi_abilit
    '
    Me.opdi_abilit.AppearanceCell.Options.UseBackColor = True
    Me.opdi_abilit.AppearanceCell.Options.UseTextOptions = True
    Me.opdi_abilit.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.opdi_abilit.Caption = "Abilitato"
    Me.opdi_abilit.Enabled = True
    Me.opdi_abilit.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.opdi_abilit.Name = "opdi_abilit"
    Me.opdi_abilit.NTSRepositoryComboBox = Nothing
    Me.opdi_abilit.NTSRepositoryItemCheck = Nothing
    Me.opdi_abilit.NTSRepositoryItemMemo = Nothing
    Me.opdi_abilit.NTSRepositoryItemText = Nothing
    Me.opdi_abilit.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.opdi_abilit.OptionsFilter.AllowFilter = False
    Me.opdi_abilit.Visible = True
    Me.opdi_abilit.VisibleIndex = 1
    Me.opdi_abilit.Width = 53
    '
    'opdi_opnome
    '
    Me.opdi_opnome.AppearanceCell.Options.UseBackColor = True
    Me.opdi_opnome.AppearanceCell.Options.UseTextOptions = True
    Me.opdi_opnome.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.opdi_opnome.Caption = "Op Nome"
    Me.opdi_opnome.Enabled = False
    Me.opdi_opnome.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.opdi_opnome.Name = "opdi_opnome"
    Me.opdi_opnome.NTSRepositoryComboBox = Nothing
    Me.opdi_opnome.NTSRepositoryItemCheck = Nothing
    Me.opdi_opnome.NTSRepositoryItemMemo = Nothing
    Me.opdi_opnome.NTSRepositoryItemText = Nothing
    Me.opdi_opnome.OptionsColumn.AllowEdit = False
    Me.opdi_opnome.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.opdi_opnome.OptionsColumn.ReadOnly = True
    Me.opdi_opnome.OptionsFilter.AllowFilter = False
    Me.opdi_opnome.Width = 70
    '
    'opdi_crmvis
    '
    Me.opdi_crmvis.AppearanceCell.Options.UseBackColor = True
    Me.opdi_crmvis.AppearanceCell.Options.UseTextOptions = True
    Me.opdi_crmvis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.opdi_crmvis.Caption = "Abil. vis. dati CRM"
    Me.opdi_crmvis.Enabled = True
    Me.opdi_crmvis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.opdi_crmvis.Name = "opdi_crmvis"
    Me.opdi_crmvis.NTSRepositoryComboBox = Nothing
    Me.opdi_crmvis.NTSRepositoryItemCheck = Nothing
    Me.opdi_crmvis.NTSRepositoryItemMemo = Nothing
    Me.opdi_crmvis.NTSRepositoryItemText = Nothing
    Me.opdi_crmvis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.opdi_crmvis.OptionsFilter.AllowFilter = False
    Me.opdi_crmvis.Visible = True
    Me.opdi_crmvis.VisibleIndex = 2
    '
    'opdi_crmmod
    '
    Me.opdi_crmmod.AppearanceCell.Options.UseBackColor = True
    Me.opdi_crmmod.AppearanceCell.Options.UseTextOptions = True
    Me.opdi_crmmod.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.opdi_crmmod.Caption = "Abil. mod. dati CRM"
    Me.opdi_crmmod.Enabled = True
    Me.opdi_crmmod.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.opdi_crmmod.Name = "opdi_crmmod"
    Me.opdi_crmmod.NTSRepositoryComboBox = Nothing
    Me.opdi_crmmod.NTSRepositoryItemCheck = Nothing
    Me.opdi_crmmod.NTSRepositoryItemMemo = Nothing
    Me.opdi_crmmod.NTSRepositoryItemText = Nothing
    Me.opdi_crmmod.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.opdi_crmmod.OptionsFilter.AllowFilter = False
    Me.opdi_crmmod.Visible = True
    Me.opdi_crmmod.VisibleIndex = 3
    Me.opdi_crmmod.Width = 84
    '
    'opdi_amm
    '
    Me.opdi_amm.AppearanceCell.Options.UseBackColor = True
    Me.opdi_amm.AppearanceCell.Options.UseTextOptions = True
    Me.opdi_amm.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.opdi_amm.Caption = "Acc. fornitori"
    Me.opdi_amm.Enabled = True
    Me.opdi_amm.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.opdi_amm.Name = "opdi_amm"
    Me.opdi_amm.NTSRepositoryComboBox = Nothing
    Me.opdi_amm.NTSRepositoryItemCheck = Nothing
    Me.opdi_amm.NTSRepositoryItemMemo = Nothing
    Me.opdi_amm.NTSRepositoryItemText = Nothing
    Me.opdi_amm.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.opdi_amm.OptionsFilter.AllowFilter = False
    Me.opdi_amm.Visible = True
    Me.opdi_amm.VisibleIndex = 4
    '
    'opdi_codcage
    '
    Me.opdi_codcage.AppearanceCell.Options.UseBackColor = True
    Me.opdi_codcage.AppearanceCell.Options.UseTextOptions = True
    Me.opdi_codcage.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.opdi_codcage.Caption = "Agente"
    Me.opdi_codcage.Enabled = True
    Me.opdi_codcage.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.opdi_codcage.Name = "opdi_codcage"
    Me.opdi_codcage.NTSRepositoryComboBox = Nothing
    Me.opdi_codcage.NTSRepositoryItemCheck = Nothing
    Me.opdi_codcage.NTSRepositoryItemMemo = Nothing
    Me.opdi_codcage.NTSRepositoryItemText = Nothing
    Me.opdi_codcage.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.opdi_codcage.OptionsFilter.AllowFilter = False
    Me.opdi_codcage.Visible = True
    Me.opdi_codcage.VisibleIndex = 5
    Me.opdi_codcage.Width = 54
    '
    'xx_codcage
    '
    Me.xx_codcage.AppearanceCell.Options.UseBackColor = True
    Me.xx_codcage.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codcage.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codcage.Caption = "Descr. agente"
    Me.xx_codcage.Enabled = False
    Me.xx_codcage.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codcage.Name = "xx_codcage"
    Me.xx_codcage.NTSRepositoryComboBox = Nothing
    Me.xx_codcage.NTSRepositoryItemCheck = Nothing
    Me.xx_codcage.NTSRepositoryItemMemo = Nothing
    Me.xx_codcage.NTSRepositoryItemText = Nothing
    Me.xx_codcage.OptionsColumn.AllowEdit = False
    Me.xx_codcage.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codcage.OptionsColumn.ReadOnly = True
    Me.xx_codcage.OptionsFilter.AllowFilter = False
    Me.xx_codcage.Visible = True
    Me.xx_codcage.VisibleIndex = 6
    Me.xx_codcage.Width = 188
    '
    'FRM__DIOP
    '
    Me.ClientSize = New System.Drawing.Size(688, 258)
    Me.Controls.Add(Me.grDiop)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.MinimizeBox = False
    Me.Name = "FRM__DIOP"
    Me.NTSLastControlFocussed = Me.grDiop
    Me.Text = "ACCESSI DITTE"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grDiop, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvDiop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Carico le immagini della toolbar
      '--------------------------------------------------------------------------------------------------------------
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbAggiungiDitte.GlyphPath = (oApp.ChildImageDir & "\doc_2.gif")
        tlbDettaglioProgrammi.GlyphPath = (oApp.ChildImageDir & "\Open2.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      '--------------------------------------------------------------------------------------------------------------
      tlbMain.NTSSetToolTip()
      '--------------------------------------------------------------------------------------------------------------
      grvDiop.NTSSetParam(oMenu, "ACCESSO PROGRAMMI PER OPERATORE/MODULO")
      '--------------------------------------------------------------------------------------------------------------
      codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128681226301562500, "Ditta"), 0, False)
      opdi_abilit.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128681226343125000, "Abilitato"), "S", "N")
      opdi_opnome.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128690680876875000, "Nome Operatore"), 0, False)
      opdi_crmvis.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128868454422011619, "Abil. vis. dati CRM"), "S", "N")
      opdi_crmmod.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128868454575615069, "Abil. mod. dati CRM"), "S", "N")
      opdi_amm.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128868454650248964, "Acc. fornitori"), "S", "N")
      opdi_codcage.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128868455349296019, "Agente"), CLN__STD.tabcage)
      xx_codcage.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128868455660018319, "Descrizione agente"), 50)

      codditt.NTSSetParamZoom("ZOOMTABANAZ")

      '--------------------------------------------------------------------------------------------------------------
      '--- Chiamo lo script per inizializzare i controlli caricati con source ext
      '--------------------------------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

#Region "Eventi Form"

  Public Overridable Sub FRM__DIOP_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    Try
      '--------------------------------------------------------------------------------------------------------------
      GctlSetVisEnab(opdi_crmvis, True)
      GctlSetVisEnab(opdi_crmmod, True)
      GctlSetVisEnab(opdi_amm, True)
      GctlSetVisEnab(opdi_codcage, True)
      GctlSetVisEnab(xx_codcage, True)
      GctlSetVisEnab(tlbZoom, True)
      '--------------------------------------------------------------------------------------------------------------
      '--- Predispongo i controlli
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      Me.Text = oApp.Tr(Me, 128681224304218750, "Accessi ditte - Operatore - ") & oCleGope.strNomeOp
      '--------------------------------------------------------------------------------------------------------------
      '--- Leggo dal database i dati e collego il NTSBindingNavigator
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleGope.DiopApri(DittaCorrente, dsDiop) Then Me.Close()
      dcDiop.DataSource = dsDiop.Tables("ACCDITO")
      dsDiop.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      grDiop.DataSource = dcDiop
      '--------------------------------------------------------------------------------------------------------------
      '--- Sempre alla fine di questa funzione: applico le regole della gctl
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRM__DIOP_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then Return
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__DIOP_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcDiop.Dispose()
      dsDiop.Dispose()
    Catch
    End Try
  End Sub

#End Region

#Region "Eventi Toolbar"

  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvDiop.NTSNuovo()
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
      If Not grvDiop.NTSDeleteRigaCorrente(dcDiop, True) Then Return
      ocleGope.DiopSalva(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvDiop.NTSRipristinaRigaCorrenteBefore(dcDiop, True) Then Return
      ocleGope.DiopRipristina(dcDiop.Position, dcDiop.Filter)
      grvDiop.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim strErr As String = ""

    Try
      NTSCallStandardZoom()
    Catch ex As Exception
      strErr = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbAggiungiDitte_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAggiungiDitte.ItemClick
    Try
      grDiop.DataSource = Nothing
      oCleGope.DiopAggiungiDitte(dsDiop)

      'leggo dal database i dati e collego il NTSBindingNavigator
      grDiop.DataSource = dcDiop

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDettaglioProgrammi_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDettaglioProgrammi.ItemClick
    Dim frmDitm As FRM__DIT2 = Nothing
    Try
      frmDitm = CType(NTSNewFormModal("FRM__DIT2"), FRM__DIT2)
      If Not Salva() Then Return
      If grvDiop.NTSGetCurrentDataRow() Is Nothing Then Return
      oCleGope.strDitmDitta = NTSCStr(grvDiop.NTSGetCurrentDataRow()!codditt)
      If Not frmDitm.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return
      frmDitm.InitEntity(oCleGope)
      frmDitm.ShowDialog()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmDitm Is Nothing Then frmDitm.Dispose()
      frmDitm = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub

#End Region

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvDiop.NTSSalvaRigaCorrente(dcDiop, ocleGope.DiopRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not ocleGope.DiopSalva(False) Then
            Return False
          End If

        Case System.Windows.Forms.DialogResult.No
          'ripristino
          ocleGope.DiopRipristina(dcDiop.Position, dcDiop.Filter)
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

  Public Overridable Sub grvDiop_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvDiop.NTSBeforeRowUpdate
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

  Public Overridable Sub grvDiop_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvDiop.NTSFocusedRowChanged
    Try
      If NTSCStr(grvDiop.GetFocusedRowCellValue(codditt).ToString.Trim) <> "" Then
        codditt.Enabled = False
      Else
        GctlSetVisEnab(codditt, False)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class