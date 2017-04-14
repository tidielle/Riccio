Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__DIT3

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
  Public dsDitt As DataSet
  Public dsDittPro As DataSet
  Public dcDitt As BindingSource = New BindingSource()
  Public strNomeProg As String = ""

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbAggiungiProgrammi As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents grDitt As NTSInformatica.NTSGrid
  Public WithEvents grvDitt As NTSInformatica.NTSGridView

  Public WithEvents opdi_nomprog As NTSInformatica.NTSGridColumn
  Public WithEvents mndescr As NTSInformatica.NTSGridColumn
  Public WithEvents opdi_abilit As NTSInformatica.NTSGridColumn

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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__DIT3))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbAggiungiProgrammi = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grDitt = New NTSInformatica.NTSGrid
    Me.grvDitt = New NTSInformatica.NTSGridView
    Me.opdi_nomprog = New NTSInformatica.NTSGridColumn
    Me.mndescr = New NTSInformatica.NTSGridColumn
    Me.opdi_abilit = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grDitt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvDitt, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci, Me.tlbNuovo, Me.tlbCancella, Me.tlbAggiungiProgrammi})
    Me.NtsBarManager1.MaxItemId = 35
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAggiungiProgrammi, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbAggiungiProgrammi.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G))
    Me.tlbAggiungiProgrammi.Name = "tlbAggiungiProgrammi"
    Me.tlbAggiungiProgrammi.Visible = True
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
    'grDitt
    '
    Me.grDitt.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '
    '
    '
    Me.grDitt.EmbeddedNavigator.Name = ""
    Me.grDitt.Location = New System.Drawing.Point(0, 30)
    Me.grDitt.MainView = Me.grvDitt
    Me.grDitt.Name = "grDitt"
    Me.grDitt.Size = New System.Drawing.Size(463, 328)
    Me.grDitt.TabIndex = 5
    Me.grDitt.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDitt})
    '
    'grvDitt
    '
    Me.grvDitt.ActiveFilterEnabled = False
    Me.grvDitt.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.opdi_nomprog, Me.mndescr, Me.opdi_abilit})
    Me.grvDitt.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvDitt.Enabled = True
    Me.grvDitt.GridControl = Me.grDitt
    Me.grvDitt.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvDitt.Name = "grvDitt"
    Me.grvDitt.NTSAllowDelete = True
    Me.grvDitt.NTSAllowInsert = True
    Me.grvDitt.NTSAllowUpdate = True
    Me.grvDitt.NTSMenuContext = Nothing
    Me.grvDitt.OptionsCustomization.AllowRowSizing = True
    Me.grvDitt.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvDitt.OptionsNavigation.UseTabKey = False
    Me.grvDitt.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvDitt.OptionsView.ColumnAutoWidth = False
    Me.grvDitt.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvDitt.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvDitt.OptionsView.ShowGroupPanel = False
    Me.grvDitt.RowHeight = 16
    '
    'opdi_nomprog
    '
    Me.opdi_nomprog.AppearanceCell.Options.UseBackColor = True
    Me.opdi_nomprog.AppearanceCell.Options.UseTextOptions = True
    Me.opdi_nomprog.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.opdi_nomprog.Caption = "Programma"
    Me.opdi_nomprog.Enabled = True
    Me.opdi_nomprog.FieldName = "opdi_nomprog"
    Me.opdi_nomprog.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.opdi_nomprog.Name = "opdi_nomprog"
    Me.opdi_nomprog.NTSRepositoryComboBox = Nothing
    Me.opdi_nomprog.NTSRepositoryItemCheck = Nothing
    Me.opdi_nomprog.NTSRepositoryItemMemo = Nothing
    Me.opdi_nomprog.NTSRepositoryItemText = Nothing
    Me.opdi_nomprog.OptionsColumn.AllowEdit = False
    Me.opdi_nomprog.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.opdi_nomprog.OptionsFilter.AllowFilter = False
    Me.opdi_nomprog.Visible = True
    Me.opdi_nomprog.VisibleIndex = 0
    Me.opdi_nomprog.Width = 83
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
    Me.opdi_abilit.Width = 70
    '
    'FRM__DIT3
    '
    Me.ClientSize = New System.Drawing.Size(463, 358)
    Me.Controls.Add(Me.grDitt)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.MinimizeBox = False
    Me.Name = "FRM__DIT3"
    Me.NTSLastControlFocussed = Me.grDitt
    Me.Text = "DETTAGLIO PROGRAMMI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grDitt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvDitt, System.ComponentModel.ISupportInitialize).EndInit()
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
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")

      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvDitt.NTSSetParam(oMenu, "DETTAGLIO PROGRAMMI")
      opdi_nomprog.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128705405339721318, "Programma"), 30, False)
      mndescr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128705407462879830, "Descrizione"), 0, True)
      opdi_abilit.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128705405340190509, "Abilitato"), "S", "N")

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

  Public Overridable Sub FRM__DITT_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      Me.Text = oApp.Tr(Me, 129024298540553098, "Dettaglio programmi - ") & oCleGope.strNomeOp

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleGope.DittApri(DittaCorrente, dsDitt) Then Me.Close()
      dcDitt.DataSource = dsDitt.Tables("ACCDITT")
      dsDitt.AcceptChanges()

      grDitt.DataSource = dcDitt

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      tlbAggiungiProgrammi_ItemClick(Me, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__DITT_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__DITT_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcDitt.Dispose()
      dsDitt.Dispose()
    Catch
    End Try
  End Sub

#End Region

#Region "Eventi Toolbar"

  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvDitt.NTSNuovo()
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
      If Not grvDitt.NTSDeleteRigaCorrente(dcDitt, True) Then Return
      oCleGope.DittSalva(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvDitt.NTSRipristinaRigaCorrenteBefore(dcDitt, True) Then Return
      oCleGope.DittRipristina(dcDitt.Position, dcDitt.Filter)
      grvDitt.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbAggiungiProgrammi_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAggiungiProgrammi.ItemClick
    Try
      grDitt.DataSource = Nothing
      oCleGope.DittCaricaProgrammi(dsDitt)

      'leggo dal database i dati e collego il NTSBindingNavigator
      grDitt.DataSource = dcDitt

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

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
      dRes = grvDitt.NTSSalvaRigaCorrente(dcDitt, oCleGope.DittRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleGope.DittSalva(False) Then
            Return False
          End If

        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleGope.DittRipristina(dcDitt.Position, dcDitt.Filter)
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

  Public Overridable Sub grvDitt_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvDitt.NTSBeforeRowUpdate
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

  Public Overridable Sub grvDitt_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvDitt.NTSFocusedRowChanged
    Try
      If NTSCStr(grvDitt.GetFocusedRowCellValue(opdi_nomprog).ToString.Trim) <> "" Then
        opdi_nomprog.Enabled = False
      Else
        GctlSetVisEnab(opdi_nomprog, False)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class
