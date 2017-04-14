Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__DIT2

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
  Public dsDitm As DataSet
  Public dsDitmPro As DataSet
  Public dcDitm As BindingSource = New BindingSource()
  Public strNomeProg As String = ""

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbAbilita As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDisabilita As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbAggiungiModuli As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDettaglioProgrammi As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents grDitm As NTSInformatica.NTSGrid
  Public WithEvents grvDitm As NTSInformatica.NTSGridView

  Public WithEvents codditt As NTSInformatica.NTSGridColumn
  Public WithEvents opdi_opnome As NTSInformatica.NTSGridColumn
  Public WithEvents opdi_modulo As NTSInformatica.NTSGridColumn
  Public WithEvents opdi_abilit As NTSInformatica.NTSGridColumn
  Public WithEvents xx_modulo As NTSInformatica.NTSGridColumn
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
    oCleGope = CleGope
    AddHandler oCleGope.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__DIT2))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbAggiungiModuli = New NTSInformatica.NTSBarButtonItem
    Me.tlbDettaglioProgrammi = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.tlbAbilita = New NTSInformatica.NTSBarButtonItem
    Me.tlbDisabilita = New NTSInformatica.NTSBarButtonItem
    Me.grDitm = New NTSInformatica.NTSGrid
    Me.grvDitm = New NTSInformatica.NTSGridView
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.opdi_opnome = New NTSInformatica.NTSGridColumn
    Me.opdi_modulo = New NTSInformatica.NTSGridColumn
    Me.xx_modulo = New NTSInformatica.NTSGridColumn
    Me.opdi_abilit = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grDitm, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvDitm, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci, Me.tlbNuovo, Me.tlbCancella, Me.tlbAggiungiModuli, Me.tlbDettaglioProgrammi, Me.tlbAbilita, Me.tlbDisabilita})
    Me.NtsBarManager1.MaxItemId = 35
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAggiungiModuli, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDettaglioProgrammi), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbAggiungiModuli
    '
    Me.tlbAggiungiModuli.Caption = "Aggiungi moduli"
    Me.tlbAggiungiModuli.Glyph = CType(resources.GetObject("tlbAggiungiModuli.Glyph"), System.Drawing.Image)
    Me.tlbAggiungiModuli.Id = 19
    Me.tlbAggiungiModuli.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G))
    Me.tlbAggiungiModuli.Name = "tlbAggiungiModuli"
    Me.tlbAggiungiModuli.Visible = True
    '
    'tlbDettaglioProgrammi
    '
    Me.tlbDettaglioProgrammi.Caption = "Dettaglio Programmi"
    Me.tlbDettaglioProgrammi.Glyph = CType(resources.GetObject("tlbDettaglioProgrammi.Glyph"), System.Drawing.Image)
    Me.tlbDettaglioProgrammi.Id = 20
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
    'grDitm
    '
    Me.grDitm.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '
    '
    '
    Me.grDitm.EmbeddedNavigator.Name = ""
    Me.grDitm.Location = New System.Drawing.Point(0, 30)
    Me.grDitm.MainView = Me.grvDitm
    Me.grDitm.Name = "grDitm"
    Me.grDitm.Size = New System.Drawing.Size(395, 295)
    Me.grDitm.TabIndex = 5
    Me.grDitm.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDitm})
    '
    'grvDitm
    '
    Me.grvDitm.ActiveFilterEnabled = False
    Me.grvDitm.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.codditt, Me.opdi_opnome, Me.opdi_modulo, Me.xx_modulo, Me.opdi_abilit})
    Me.grvDitm.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvDitm.Enabled = True
    Me.grvDitm.GridControl = Me.grDitm
    Me.grvDitm.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvDitm.Name = "grvDitm"
    Me.grvDitm.NTSAllowDelete = True
    Me.grvDitm.NTSAllowInsert = True
    Me.grvDitm.NTSAllowUpdate = True
    Me.grvDitm.NTSMenuContext = Nothing
    Me.grvDitm.OptionsCustomization.AllowRowSizing = True
    Me.grvDitm.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvDitm.OptionsNavigation.UseTabKey = False
    Me.grvDitm.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvDitm.OptionsView.ColumnAutoWidth = False
    Me.grvDitm.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvDitm.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvDitm.OptionsView.ShowGroupPanel = False
    Me.grvDitm.RowHeight = 16
    '
    'codditt
    '
    Me.codditt.AppearanceCell.Options.UseBackColor = True
    Me.codditt.AppearanceCell.Options.UseTextOptions = True
    Me.codditt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.codditt.Caption = "codditt"
    Me.codditt.Enabled = False
    Me.codditt.FieldName = "codditt"
    Me.codditt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.codditt.Name = "codditt"
    Me.codditt.NTSRepositoryComboBox = Nothing
    Me.codditt.NTSRepositoryItemCheck = Nothing
    Me.codditt.NTSRepositoryItemMemo = Nothing
    Me.codditt.NTSRepositoryItemText = Nothing
    Me.codditt.OptionsColumn.AllowEdit = False
    Me.codditt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.codditt.OptionsColumn.ReadOnly = True
    Me.codditt.OptionsFilter.AllowFilter = False
    Me.codditt.Width = 83
    '
    'opdi_opnome
    '
    Me.opdi_opnome.AppearanceCell.Options.UseBackColor = True
    Me.opdi_opnome.AppearanceCell.Options.UseTextOptions = True
    Me.opdi_opnome.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.opdi_opnome.Caption = "Nome op."
    Me.opdi_opnome.Enabled = False
    Me.opdi_opnome.FieldName = "opdi_opnome"
    Me.opdi_opnome.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.opdi_opnome.Name = "opdi_opnome"
    Me.opdi_opnome.NTSRepositoryComboBox = Nothing
    Me.opdi_opnome.NTSRepositoryItemCheck = Nothing
    Me.opdi_opnome.NTSRepositoryItemMemo = Nothing
    Me.opdi_opnome.NTSRepositoryItemText = Nothing
    Me.opdi_opnome.OptionsColumn.AllowEdit = False
    Me.opdi_opnome.OptionsColumn.AllowFocus = False
    Me.opdi_opnome.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.opdi_opnome.OptionsColumn.ReadOnly = True
    Me.opdi_opnome.OptionsFilter.AllowFilter = False
    Me.opdi_opnome.Width = 169
    '
    'opdi_modulo
    '
    Me.opdi_modulo.AppearanceCell.Options.UseBackColor = True
    Me.opdi_modulo.AppearanceCell.Options.UseTextOptions = True
    Me.opdi_modulo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.opdi_modulo.Caption = "Modulo"
    Me.opdi_modulo.Enabled = True
    Me.opdi_modulo.FieldName = "opdi_modulo"
    Me.opdi_modulo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.opdi_modulo.Name = "opdi_modulo"
    Me.opdi_modulo.NTSRepositoryComboBox = Nothing
    Me.opdi_modulo.NTSRepositoryItemCheck = Nothing
    Me.opdi_modulo.NTSRepositoryItemMemo = Nothing
    Me.opdi_modulo.NTSRepositoryItemText = Nothing
    Me.opdi_modulo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.opdi_modulo.OptionsFilter.AllowFilter = False
    Me.opdi_modulo.Visible = True
    Me.opdi_modulo.VisibleIndex = 0
    Me.opdi_modulo.Width = 70
    '
    'xx_modulo
    '
    Me.xx_modulo.AppearanceCell.Options.UseBackColor = True
    Me.xx_modulo.AppearanceCell.Options.UseTextOptions = True
    Me.xx_modulo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_modulo.Caption = "Descrizione"
    Me.xx_modulo.Enabled = False
    Me.xx_modulo.FieldName = "xx_modulo"
    Me.xx_modulo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_modulo.Name = "xx_modulo"
    Me.xx_modulo.NTSRepositoryComboBox = Nothing
    Me.xx_modulo.NTSRepositoryItemCheck = Nothing
    Me.xx_modulo.NTSRepositoryItemMemo = Nothing
    Me.xx_modulo.NTSRepositoryItemText = Nothing
    Me.xx_modulo.OptionsColumn.AllowEdit = False
    Me.xx_modulo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_modulo.OptionsColumn.ReadOnly = True
    Me.xx_modulo.OptionsFilter.AllowFilter = False
    Me.xx_modulo.Visible = True
    Me.xx_modulo.VisibleIndex = 1
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
    Me.opdi_abilit.VisibleIndex = 2
    '
    'FRM__DIT2
    '
    Me.ClientSize = New System.Drawing.Size(395, 325)
    Me.Controls.Add(Me.grDitm)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.MinimizeBox = False
    Me.Name = "FRM__DIT2"
    Me.NTSLastControlFocussed = Me.grDitm
    Me.Text = "DETTAGLIO MODULI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grDitm, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvDitm, System.ComponentModel.ISupportInitialize).EndInit()
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
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbAggiungiModuli.GlyphPath = (oApp.ChildImageDir & "\doc_2.gif")
        tlbDettaglioProgrammi.GlyphPath = (oApp.ChildImageDir & "\Open2.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvDitm.NTSSetParam(oMenu, oApp.Tr(Me, 128704591144288021, "Dettaglio moduli"))
      codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128704608365903511, "codditt"), 12, False)
      opdi_opnome.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128704608366059758, "Nome op."), 20, False)
      opdi_modulo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128704608366216005, "Modulo"), "0", 4, 0, 9999)
      xx_modulo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128704608366372252, "Descrizione"), 0, True)
      opdi_abilit.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128704608366528499, "Abilitato"), "S", "N")

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

#Region "Eventi Form"
  Public Overridable Sub FRM__Ditm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      Me.Text = oApp.Tr(Me, 128681224201406250, "Dettaglio moduli - Operatore - ") & oCleGope.strNomeOp

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleGope.DitmApri(DittaCorrente, dsDitm) Then Me.Close()
      dcDitm.DataSource = dsDitm.Tables("ACCDITM")
      dsDitm.AcceptChanges()

      grDitm.DataSource = dcDitm

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__Ditm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then Return
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__Ditm_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcDitm.Dispose()
      dsDitm.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"

  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvDitm.NTSNuovo()
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
      If Not grvDitm.NTSDeleteRigaCorrente(dcDitm, True) Then Return
      oCleGope.DitmSalva(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvDitm.NTSRipristinaRigaCorrenteBefore(dcDitm, True) Then Return
      oCleGope.DitmRipristina(dcDitm.Position, dcDitm.Filter)
      grvDitm.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbAggiungiModuli_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAggiungiModuli.ItemClick
    Try
      grDitm.DataSource = Nothing
      oCleGope.DitmAggiungiModuli(dsDitm)

      'leggo dal database i dati e collego il NTSBindingNavigator
      grDitm.DataSource = dcDitm

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
    Dim frmDitt As FRM__DIT3 = Nothing
    Try
      frmDitt = CType(NTSNewFormModal("FRM__DIT3"), FRM__DIT3)
      If Not Salva() Then Return
      If grvDitm.NTSGetCurrentDataRow() Is Nothing Then Return
      oCleGope.strDittModulo = NTSCStr(grvDitm.NTSGetCurrentDataRow()!opdi_modulo)
      If Not frmDitt.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return
      frmDitt.InitEntity(oCleGope)
      frmDitt.ShowDialog()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmDitt Is Nothing Then frmDitt.Dispose()
      frmDitt = Nothing
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
      dRes = grvDitm.NTSSalvaRigaCorrente(dcDitm, oCleGope.DitmRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleGope.DitmSalva(False) Then
            Return False
          End If

        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleGope.DitmRipristina(dcDitm.Position, dcDitm.Filter)
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

  Public Overridable Sub grvDitm_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvDitm.NTSBeforeRowUpdate
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

  Public Overridable Sub grvDitm_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvDitm.NTSFocusedRowChanged
    Try
      If NTSCInt(grvDitm.GetFocusedRowCellValue(opdi_modulo)) <> 0 Then
        opdi_modulo.Enabled = False
      Else
        GctlSetVisEnab(opdi_modulo, False)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class