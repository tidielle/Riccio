Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__GEOU

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
  Public dsGeou As DataSet
  Public dsGeouPro As DataSet
  Public dcGeou As BindingSource = New BindingSource()
  Public strNomeProg As String = ""

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbAbilita As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDisabilita As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbAggiungiProgrammi As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents grGeou As NTSInformatica.NTSGrid
  Public WithEvents grvGeou As NTSInformatica.NTSGridView

  Public WithEvents mnou_nomprog As NTSInformatica.NTSGridColumn
  Public WithEvents mndescr As NTSInformatica.NTSGridColumn
  Public WithEvents mnou_abilit As NTSInformatica.NTSGridColumn

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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__GEOU))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbAggiungiProgrammi = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbAbilita = New NTSInformatica.NTSBarButtonItem
    Me.tlbDisabilita = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grGeou = New NTSInformatica.NTSGrid
    Me.grvGeou = New NTSInformatica.NTSGridView
    Me.mnou_nomprog = New NTSInformatica.NTSGridColumn
    Me.mndescr = New NTSInformatica.NTSGridColumn
    Me.mnou_abilit = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grGeou, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvGeou, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci, Me.tlbNuovo, Me.tlbCancella, Me.tlbAggiungiProgrammi, Me.tlbStrumenti, Me.tlbAbilita, Me.tlbDisabilita})
    Me.NtsBarManager1.MaxItemId = 35
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAggiungiProgrammi, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbAggiungiProgrammi
    '
    Me.tlbAggiungiProgrammi.Caption = "Aggiungi programmi"
    Me.tlbAggiungiProgrammi.Glyph = CType(resources.GetObject("tlbAggiungiProgrammi.Glyph"), System.Drawing.Image)
    Me.tlbAggiungiProgrammi.Id = 19
    Me.tlbAggiungiProgrammi.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F7))
    Me.tlbAggiungiProgrammi.Name = "tlbAggiungiProgrammi"
    Me.tlbAggiungiProgrammi.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 32
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAbilita), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDisabilita)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbAbilita
    '
    Me.tlbAbilita.Caption = "Abilita tutto"
    Me.tlbAbilita.Id = 33
    Me.tlbAbilita.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
    Me.tlbAbilita.Name = "tlbAbilita"
    Me.tlbAbilita.Visible = True
    '
    'tlbDisabilita
    '
    Me.tlbDisabilita.Caption = "Disabilita tutto"
    Me.tlbDisabilita.Id = 34
    Me.tlbDisabilita.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D))
    Me.tlbDisabilita.Name = "tlbDisabilita"
    Me.tlbDisabilita.Visible = True
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
    'grGeou
    '
    Me.grGeou.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '
    '
    '
    Me.grGeou.EmbeddedNavigator.Name = ""
    Me.grGeou.Location = New System.Drawing.Point(0, 30)
    Me.grGeou.MainView = Me.grvGeou
    Me.grGeou.Name = "grGeou"
    Me.grGeou.Size = New System.Drawing.Size(463, 328)
    Me.grGeou.TabIndex = 5
    Me.grGeou.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvGeou})
    '
    'grvGeou
    '
    Me.grvGeou.ActiveFilterEnabled = False
    Me.grvGeou.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.mnou_nomprog, Me.mndescr, Me.mnou_abilit})
    Me.grvGeou.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvGeou.Enabled = True
    Me.grvGeou.GridControl = Me.grGeou
    Me.grvGeou.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvGeou.Name = "grvGeou"
    Me.grvGeou.NTSAllowDelete = True
    Me.grvGeou.NTSAllowInsert = True
    Me.grvGeou.NTSAllowUpdate = True
    Me.grvGeou.NTSMenuContext = Nothing
    Me.grvGeou.OptionsCustomization.AllowRowSizing = True
    Me.grvGeou.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvGeou.OptionsNavigation.UseTabKey = False
    Me.grvGeou.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvGeou.OptionsView.ColumnAutoWidth = False
    Me.grvGeou.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvGeou.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvGeou.OptionsView.ShowGroupPanel = False
    Me.grvGeou.RowHeight = 16
    '
    'mnou_nomprog
    '
    Me.mnou_nomprog.AppearanceCell.Options.UseBackColor = True
    Me.mnou_nomprog.AppearanceCell.Options.UseTextOptions = True
    Me.mnou_nomprog.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mnou_nomprog.Caption = "Programma"
    Me.mnou_nomprog.Enabled = True
    Me.mnou_nomprog.FieldName = "mnou_nomprog"
    Me.mnou_nomprog.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mnou_nomprog.Name = "mnou_nomprog"
    Me.mnou_nomprog.NTSRepositoryComboBox = Nothing
    Me.mnou_nomprog.NTSRepositoryItemCheck = Nothing
    Me.mnou_nomprog.NTSRepositoryItemMemo = Nothing
    Me.mnou_nomprog.NTSRepositoryItemText = Nothing
    Me.mnou_nomprog.OptionsColumn.AllowEdit = False
    Me.mnou_nomprog.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mnou_nomprog.OptionsFilter.AllowFilter = False
    Me.mnou_nomprog.Visible = True
    Me.mnou_nomprog.VisibleIndex = 0
    Me.mnou_nomprog.Width = 83
    '
    'mndescr
    '
    Me.mndescr.AppearanceCell.Options.UseBackColor = True
    Me.mndescr.AppearanceCell.Options.UseTextOptions = True
    Me.mndescr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mndescr.Caption = "Descrizione"
    Me.mndescr.Enabled = False
    Me.mndescr.FieldName = "mndescr"
    Me.mndescr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mndescr.Name = "mndescr"
    Me.mndescr.NTSRepositoryComboBox = Nothing
    Me.mndescr.NTSRepositoryItemCheck = Nothing
    Me.mndescr.NTSRepositoryItemMemo = Nothing
    Me.mndescr.NTSRepositoryItemText = Nothing
    Me.mndescr.OptionsColumn.AllowEdit = False
    Me.mndescr.OptionsColumn.AllowFocus = False
    Me.mndescr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mndescr.OptionsColumn.ReadOnly = True
    Me.mndescr.OptionsFilter.AllowFilter = False
    Me.mndescr.Visible = True
    Me.mndescr.VisibleIndex = 1
    Me.mndescr.Width = 83
    '
    'mnou_abilit
    '
    Me.mnou_abilit.AppearanceCell.Options.UseBackColor = True
    Me.mnou_abilit.AppearanceCell.Options.UseTextOptions = True
    Me.mnou_abilit.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mnou_abilit.Caption = "Abilitato"
    Me.mnou_abilit.Enabled = True
    Me.mnou_abilit.FieldName = "mnou_abilit"
    Me.mnou_abilit.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mnou_abilit.Name = "mnou_abilit"
    Me.mnou_abilit.NTSRepositoryComboBox = Nothing
    Me.mnou_abilit.NTSRepositoryItemCheck = Nothing
    Me.mnou_abilit.NTSRepositoryItemMemo = Nothing
    Me.mnou_abilit.NTSRepositoryItemText = Nothing
    Me.mnou_abilit.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mnou_abilit.OptionsFilter.AllowFilter = False
    Me.mnou_abilit.Visible = True
    Me.mnou_abilit.VisibleIndex = 2
    Me.mnou_abilit.Width = 70
    '
    'FRM__GEOU
    '
    Me.ClientSize = New System.Drawing.Size(463, 358)
    Me.Controls.Add(Me.grGeou)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.MinimizeBox = False
    Me.Name = "FRM__GEOU"
    Me.NTSLastControlFocussed = Me.grGeou
    Me.Text = "GESTIONE ACCESSI OPERATORE"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grGeou, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvGeou, System.ComponentModel.ISupportInitialize).EndInit()
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
        tlbAggiungiProgrammi.GlyphPath = (oApp.ChildImageDir & "\doc_2.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")

      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvGeou.NTSSetParam(oMenu, "ACCESSO PROGRAMMI PER OPERATORE/MODULO")
      mnou_nomprog.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128644966337031250, "Programma"), 0, False)
      mndescr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128649316377968750, "Descrizione"), 0, True)
      mnou_abilit.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128644966337187500, "Abilitato"), "S", "N")


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

  Public Overridable Sub FRM__GEOU_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      Me.Text = oApp.Tr(Me, 129024298842603678, "Gestione accessi - Operatore - ") & oCleGope.strNomeOp

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleGope.GeouApri(DittaCorrente, dsGeou) Then Me.Close()
      dcGeou.DataSource = dsGeou.Tables("ACCOPEU")
      dsGeou.AcceptChanges()

      grGeou.DataSource = dcGeou

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__GEOU_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__GEOU_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcGeou.Dispose()
      dsGeou.Dispose()
    Catch
    End Try
  End Sub

#End Region

#Region "Eventi Toolbar"

  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvGeou.NTSNuovo()
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
      If Not grvGeou.NTSDeleteRigaCorrente(dcGeou, True) Then Return
      oCleGope.GeouSalva(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvGeou.NTSRipristinaRigaCorrenteBefore(dcGeou, True) Then Return
      oCleGope.GeouRipristina(dcGeou.Position, dcGeou.Filter)
      grvGeou.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbAggiungiProgrammi_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAggiungiProgrammi.ItemClick
    Try

      Dim dRes As DialogResult
      dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128679441225937500, "Aggiornare l'elenco dei programmi ?"))
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes

          grGeou.DataSource = Nothing
          oCleGope.GeouCaricaProgrammi(dsGeou)

          'leggo dal database i dati e collego il NTSBindingNavigator
          grGeou.DataSource = dcGeou

          '-------------------------------------------------
          'sempre alla fine di questa funzione: applico le regole della gctl
          GctlSetRoules()

          oApp.MsgBoxInfo(oApp.Tr(Me, 128679441311718750, "Operazione completata"))

      End Select
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

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvGeou.NTSSalvaRigaCorrente(dcGeou, oCleGope.GeouRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleGope.GeouSalva(False) Then
            Return False
          End If

        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleGope.GeouRipristina(dcGeou.Position, dcGeou.Filter)
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

  Public Overridable Sub grvGeou_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvGeou.NTSBeforeRowUpdate
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

  Public Overridable Sub grvGeou_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvGeou.NTSFocusedRowChanged
    Try
      If NTSCStr(grvGeou.GetFocusedRowCellValue(mnou_nomprog).ToString.Trim) <> "" Then
        mnou_nomprog.Enabled = False
      Else
        GctlSetVisEnab(mnou_nomprog, False)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbAbilita_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAbilita.ItemClick
    Dim i As Integer
    Try
      For i = 0 To dsGeou.Tables("ACCOPEU").Rows.Count - 1
        dsGeou.Tables("ACCOPEU").Rows(i)!mnou_abilit = "S"
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDisabilita_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDisabilita.ItemClick
    Dim i As Integer
    Try
      For i = 0 To dsGeou.Tables("ACCOPEU").Rows.Count - 1
        dsGeou.Tables("ACCOPEU").Rows(i)!mnou_abilit = "N"
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class
