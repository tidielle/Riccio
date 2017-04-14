Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__DITO
  Public oCleAnaz As CLE__ANAZ
  Public oCallParams As CLE__CLDP
  Public dsDito As DataSet
  Public dcDito As BindingSource = New BindingSource()

  Public bIs15 As Boolean = True

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDettaglioModuli As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbAggiungiOperatori As NTSInformatica.NTSBarButtonItem
  Public WithEvents grDito As NTSInformatica.NTSGrid
  Public WithEvents grvDito As NTSInformatica.NTSGridView
  Public WithEvents opdi_opnome As NTSInformatica.NTSGridColumn
  Public WithEvents opdi_abilit As NTSInformatica.NTSGridColumn
  Public WithEvents opdi_crmvis As NTSInformatica.NTSGridColumn
  Public WithEvents opdi_crmmod As NTSInformatica.NTSGridColumn
  Public WithEvents opdi_amm As NTSInformatica.NTSGridColumn
  Public WithEvents opdi_codcage As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codcage As NTSInformatica.NTSGridColumn


  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__DITO))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbAggiungiOperatori = New NTSInformatica.NTSBarButtonItem
    Me.tlbDettaglioModuli = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grDito = New NTSInformatica.NTSGrid
    Me.grvDito = New NTSInformatica.NTSGridView
    Me.opdi_opnome = New NTSInformatica.NTSGridColumn
    Me.opdi_abilit = New NTSInformatica.NTSGridColumn
    Me.opdi_crmvis = New NTSInformatica.NTSGridColumn
    Me.opdi_crmmod = New NTSInformatica.NTSGridColumn
    Me.opdi_amm = New NTSInformatica.NTSGridColumn
    Me.opdi_codcage = New NTSInformatica.NTSGridColumn
    Me.xx_codcage = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grDito, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvDito, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'DevXDefaultLookAndFeel
    '
    
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci, Me.tlbNuovo, Me.tlbCancella, Me.tlbAggiungiOperatori, Me.tlbDettaglioModuli, Me.tlbZoom})
    Me.NtsBarManager1.MaxItemId = 22
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAggiungiOperatori, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDettaglioModuli), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbZoom.Id = 21
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbAggiungiOperatori
    '
    Me.tlbAggiungiOperatori.Caption = "Aggiungi operatori"
    Me.tlbAggiungiOperatori.Glyph = CType(resources.GetObject("tlbAggiungiOperatori.Glyph"), System.Drawing.Image)
    Me.tlbAggiungiOperatori.Id = 19
    Me.tlbAggiungiOperatori.Name = "tlbAggiungiOperatori"
    Me.tlbAggiungiOperatori.Visible = True
    '
    'tlbDettaglioModuli
    '
    Me.tlbDettaglioModuli.Caption = "Dettaglio moduli"
    Me.tlbDettaglioModuli.Glyph = CType(resources.GetObject("tlbDettaglioModuli.Glyph"), System.Drawing.Image)
    Me.tlbDettaglioModuli.Id = 20
    Me.tlbDettaglioModuli.Name = "tlbDettaglioModuli"
    Me.tlbDettaglioModuli.Visible = True
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
    'grDito
    '
    Me.grDito.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grDito.EmbeddedNavigator.Name = ""
    Me.grDito.Location = New System.Drawing.Point(0, 30)
    Me.grDito.MainView = Me.grvDito
    Me.grDito.Name = "grDito"
    Me.grDito.Size = New System.Drawing.Size(648, 412)
    Me.grDito.TabIndex = 5
    Me.grDito.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDito})
    '
    'grvDito
    '
    Me.grvDito.ActiveFilterEnabled = False
    Me.grvDito.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.opdi_opnome, Me.opdi_abilit, Me.opdi_crmvis, Me.opdi_crmmod, Me.opdi_amm, Me.opdi_codcage, Me.xx_codcage})
    Me.grvDito.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvDito.Enabled = True
    Me.grvDito.GridControl = Me.grDito
    Me.grvDito.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvDito.Name = "grvDito"
    Me.grvDito.NTSAllowDelete = True
    Me.grvDito.NTSAllowInsert = True
    Me.grvDito.NTSAllowUpdate = True
    Me.grvDito.NTSMenuContext = Nothing
    Me.grvDito.OptionsCustomization.AllowRowSizing = True
    Me.grvDito.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvDito.OptionsNavigation.UseTabKey = False
    Me.grvDito.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvDito.OptionsView.ColumnAutoWidth = False
    Me.grvDito.OptionsView.EnableAppearanceEvenRow = True
    Me.grvDito.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvDito.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvDito.OptionsView.ShowGroupPanel = False
    Me.grvDito.RowHeight = 16
    '
    'opdi_opnome
    '
    Me.opdi_opnome.AppearanceCell.Options.UseBackColor = True
    Me.opdi_opnome.AppearanceCell.Options.UseTextOptions = True
    Me.opdi_opnome.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.opdi_opnome.Caption = "Operatore"
    Me.opdi_opnome.Enabled = True
    Me.opdi_opnome.FieldName = "opdi_opnome"
    Me.opdi_opnome.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.opdi_opnome.Name = "opdi_opnome"
    Me.opdi_opnome.NTSRepositoryComboBox = Nothing
    Me.opdi_opnome.NTSRepositoryItemCheck = Nothing
    Me.opdi_opnome.NTSRepositoryItemMemo = Nothing
    Me.opdi_opnome.NTSRepositoryItemText = Nothing
    Me.opdi_opnome.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.opdi_opnome.OptionsFilter.AllowFilter = False
    Me.opdi_opnome.Visible = True
    Me.opdi_opnome.VisibleIndex = 0
    Me.opdi_opnome.Width = 70
    '
    'opdi_abilit
    '
    Me.opdi_abilit.AppearanceCell.Options.UseBackColor = True
    Me.opdi_abilit.AppearanceCell.Options.UseTextOptions = True
    Me.opdi_abilit.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.opdi_abilit.Caption = "Abilitato"
    Me.opdi_abilit.Enabled = True
    Me.opdi_abilit.FieldName = "opdi_abilit"
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
    Me.opdi_abilit.Width = 70
    '
    'opdi_crmvis
    '
    Me.opdi_crmvis.AppearanceCell.Options.UseBackColor = True
    Me.opdi_crmvis.AppearanceCell.Options.UseTextOptions = True
    Me.opdi_crmvis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.opdi_crmvis.Caption = "Abil. vis. CRM"
    Me.opdi_crmvis.Enabled = True
    Me.opdi_crmvis.FieldName = "opdi_crmvis"
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
    Me.opdi_crmmod.Caption = "Abil. mod. CRM"
    Me.opdi_crmmod.Enabled = True
    Me.opdi_crmmod.FieldName = "opdi_crmmod"
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
    '
    'opdi_amm
    '
    Me.opdi_amm.AppearanceCell.Options.UseBackColor = True
    Me.opdi_amm.AppearanceCell.Options.UseTextOptions = True
    Me.opdi_amm.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.opdi_amm.Caption = "Acc. forn."
    Me.opdi_amm.Enabled = True
    Me.opdi_amm.FieldName = "opdi_amm"
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
    Me.opdi_codcage.FieldName = "opdi_codcage"
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
    '
    'xx_codcage
    '
    Me.xx_codcage.AppearanceCell.Options.UseBackColor = True
    Me.xx_codcage.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codcage.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codcage.Caption = "Des. agente"
    Me.xx_codcage.Enabled = False
    Me.xx_codcage.FieldName = "xx_codcage"
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
    '
    'FRM__DITO
    '
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.grDito)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRM__DITO"
    Me.NTSLastControlFocussed = Me.grDito
    Me.Text = "ACCESSI CRM PER OPERATORE"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grDito, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvDito, System.ComponentModel.ISupportInitialize).EndInit()
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
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbAggiungiOperatori.GlyphPath = (oApp.ChildImageDir & "\doc_2.gif")
        tlbDettaglioModuli.GlyphPath = (oApp.ChildImageDir & "\open2.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvDito.NTSSetParam(oMenu, "ACCESSI PER OPERATORE")
      opdi_opnome.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129006945761487371, "Operatore"), 20, False)
      opdi_abilit.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129006945888209365, "Abilitato"), "S", "N")
      opdi_crmvis.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128865219634086778, "Abil. vis. CRM"), "S", "N")
      opdi_crmmod.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128865219811966018, "Abil. mod. CRM"), "S", "N")
      opdi_amm.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128865220086040273, "Accesso ai fornitori"), "S", "N")
      opdi_codcage.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128865225702835748, "Agente"), CLN__STD.tabcage)
      xx_codcage.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128865225728929498, "Descrizione agente"), 50)
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
  Public Overridable Sub FRM__DITO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Predispongo i controlli
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      dsDito = oCleAnaz.dsShared
      dcDito.DataSource = dsDito.Tables("ACCDITO")
      dsDito.Tables("ACCDITO").AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      grDito.DataSource = dcDito
      '--------------------------------------------------------------------------------------------------------------
      If dsDito.Tables("ACCDITO").Rows.Count = 0 Then oCleAnaz.DitoCaricaUsers()
      '--------------------------------------------------------------------------------------------------------------
      '--- Sempre alla fine di questa funzione: applico le regole della gctl
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
      '--- Devo bloccare/sbloccare i campi chiave se necessario
      '--------------------------------------------------------------------------------------------------------------
      grvDito_NTSFocusedRowChanged(grvDito, Nothing)
      '--------------------------------------------------------------------------------------------------------------
      '--- visualizza le colonne relative ai nuovi campi di ACCDITO
      '--------------------------------------------------------------------------------------------------------------
      GctlSetVisEnab(opdi_crmvis, True)
      GctlSetVisEnab(opdi_crmmod, True)
      GctlSetVisEnab(opdi_amm, True)
      GctlSetVisEnab(opdi_codcage, True)
      GctlSetVisEnab(xx_codcage, True)
      GctlSetVisEnab(tlbZoom, True)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRM__DITO_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvDito.NTSNuovo()

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
      If Not grvDito.NTSDeleteRigaCorrente(dcDito, True) Then Return
      oCleAnaz.ditoSalva(True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvDito.NTSRipristinaRigaCorrenteBefore(dcDito, True) Then Return
      oCleAnaz.DitoRipristina(dcDito.Position, dcDito.Filter)
      grvDito.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim strErr As String = ""
    Try
      '--- Zoom standard
      NTSCallStandardZoom()
    Catch ex As Exception
      strErr = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbAggiungiOperatori_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAggiungiOperatori.ItemClick
    Try
      If Not Salva() Then Return

      oCleAnaz.DitoCaricaUsers()

      oApp.MsgBoxInfo(oApp.Tr(Me, 129006946024931615, "Operazione completata"))

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDettaglioModuli_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDettaglioModuli.ItemClick
    Dim ds As New DataSet
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim frmDitm As FRM__DITM = Nothing
    Try
      frmDitm = CType(NTSNewFormModal("FRM__DITM"), FRM__DITM)

      If Not Salva() Then Return

      If grvDito.NTSGetCurrentDataRow Is Nothing Then Return

      Me.Cursor = Cursors.WaitCursor
      '-------------------------------
      'clono la tabella perchè devo far vedere solo un subset di righe
      ds.Tables.Clear()
      ds.Tables.Add(oCleAnaz.dsShared.Tables("ACCDITM").Clone())
      ds.Tables(0).TableName = "ACCDITM"
      dtrT = oCleAnaz.dsShared.Tables("ACCDITM").Select("opdi_opnome = " & CStrSQL(grvDito.NTSGetCurrentDataRow!opdi_opnome.ToString), "opdi_modulo")
      For i = 0 To dtrT.Length - 1
        ds.Tables("ACCDITM").ImportRow(dtrT(i))
        dtrT(i).Delete()
      Next
      oCleAnaz.dsShared.Tables("ACCDITM").AcceptChanges()

      frmDitm.Init(oMenu, Nothing, DittaCorrente)
      frmDitm.InitEntity(oCleAnaz, ds, grvDito.NTSGetCurrentDataRow!opdi_opnome.ToString)
      frmDitm.ShowDialog()

      '-------------------------------
      'riacquisisco ACCDITM
      For i = 0 To ds.Tables("ACCDITM").Rows.Count - 1
        If ds.Tables("ACCDITM").Rows(i).RowState <> DataRowState.Deleted Then
          oCleAnaz.dsShared.Tables("ACCDITM").ImportRow(ds.Tables("ACCDITM").Rows(i))
        End If
      Next
      ds.Tables.Clear()
      oCleAnaz.dsShared.Tables("ACCDITM").AcceptChanges()
      oCleAnaz.bHasChanges = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmDitm Is Nothing Then frmDitm.Dispose()
      frmDitm = Nothing
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub
#End Region

#Region "Eventi di griglia"

  Public Overridable Sub grvDito_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvDito.NTSBeforeRowUpdate
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

  Public Overridable Sub grvDito_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvDito.NTSFocusedRowChanged
    Try
      If NTSCStr(grvDito.GetFocusedRowCellValue("opdi_opnome")).Trim <> "" Then
        opdi_opnome.Enabled = False
      Else
        GctlSetVisEnab(opdi_opnome, False)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvDito.NTSSalvaRigaCorrente(dcDito, oCleAnaz.DitoRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleAnaz.DitoSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleAnaz.DitoRipristina(dcDito.Position, dcDito.Filter)
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
