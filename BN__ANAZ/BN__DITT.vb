Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__DITT
  Public oCleAnaz As CLE__ANAZ
  Public oCallParams As CLE__CLDP
  Public dsDitt As DataSet
  Public dcDitt As BindingSource = New BindingSource()

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
  Public WithEvents tlbAggiungiProgrammi As NTSInformatica.NTSBarButtonItem
  Public WithEvents grDitt As NTSInformatica.NTSGrid
  Public WithEvents grvDitt As NTSInformatica.NTSGridView
  Public WithEvents opdi_nomprog As NTSInformatica.NTSGridColumn
  Public WithEvents xx_nomprog As NTSInformatica.NTSGridColumn
  Public WithEvents opdi_abilit As NTSInformatica.NTSGridColumn

  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__DITT))
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
    Me.xx_nomprog = New NTSInformatica.NTSGridColumn
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
    Me.NtsBarManager1.MaxItemId = 21
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
    Me.grDitt.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grDitt.EmbeddedNavigator.Name = ""
    Me.grDitt.Location = New System.Drawing.Point(0, 30)
    Me.grDitt.MainView = Me.grvDitt
    Me.grDitt.Name = "grDitt"
    Me.grDitt.Size = New System.Drawing.Size(648, 412)
    Me.grDitt.TabIndex = 5
    Me.grDitt.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDitt})
    '
    'grvDitt
    '
    Me.grvDitt.ActiveFilterEnabled = False
    Me.grvDitt.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.opdi_nomprog, Me.xx_nomprog, Me.opdi_abilit})
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
    Me.grvDitt.OptionsView.EnableAppearanceEvenRow = True
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
    Me.opdi_nomprog.NTSRepositoryItemText = Nothing
    Me.opdi_nomprog.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.opdi_nomprog.OptionsFilter.AllowFilter = False
    Me.opdi_nomprog.Visible = True
    Me.opdi_nomprog.VisibleIndex = 0
    Me.opdi_nomprog.Width = 70
    '
    'xx_nomprog
    '
    Me.xx_nomprog.AppearanceCell.Options.UseBackColor = True
    Me.xx_nomprog.AppearanceCell.Options.UseTextOptions = True
    Me.xx_nomprog.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_nomprog.Caption = "Descrizione"
    Me.xx_nomprog.Enabled = False
    Me.xx_nomprog.FieldName = "xx_nomprog"
    Me.xx_nomprog.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_nomprog.Name = "xx_nomprog"
    Me.xx_nomprog.NTSRepositoryComboBox = Nothing
    Me.xx_nomprog.NTSRepositoryItemCheck = Nothing
    Me.xx_nomprog.NTSRepositoryItemText = Nothing
    Me.xx_nomprog.OptionsColumn.AllowEdit = False
    Me.xx_nomprog.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_nomprog.OptionsColumn.ReadOnly = True
    Me.xx_nomprog.OptionsFilter.AllowFilter = False
    Me.xx_nomprog.Visible = True
    Me.xx_nomprog.VisibleIndex = 1
    Me.xx_nomprog.Width = 70
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
    Me.opdi_abilit.NTSRepositoryItemText = Nothing
    Me.opdi_abilit.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.opdi_abilit.OptionsFilter.AllowFilter = False
    Me.opdi_abilit.Visible = True
    Me.opdi_abilit.VisibleIndex = 2
    Me.opdi_abilit.Width = 70
    '
    'FRM__DITT
    '
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.grDitt)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRM__DITT"
    Me.NTSLastControlFocussed = Me.grDitt
    Me.Text = "DETTAGLIO PROGRAMMI PER OPERATORE / MODULO"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grDitt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvDitt, System.ComponentModel.ISupportInitialize).EndInit()
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

  Public Overridable Sub InitEntity(ByRef cleAnaz As CLE__ANAZ, ByRef ds As DataSet, ByVal strUser As String, ByVal nModulo As Integer)
    oCleAnaz = cleAnaz
    AddHandler oCleAnaz.RemoteEvent, AddressOf GestisciEventiEntity

    '-------------------------------------------------
    'leggo dal database i dati e collego il NTSBinding
    oCleAnaz.strUserAperto = strUser
    oCleAnaz.nModuloAperto = nModulo
    dsDitt = ds
    oCleAnaz.DittSetDataTable(DittaCorrente, dsDitt.Tables("ACCDITT"))
    dcDitt.DataSource = dsDitt.Tables("ACCDITT")
    dsDitt.AcceptChanges()
    grDitt.DataSource = dcDitt
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

      grvDitt.NTSSetParam(oMenu, "ACCESSO PROGRAMMI PER OPERATORE/MODULO")
      opdi_nomprog.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129006946624165705, "Programma"), 0, False)
      xx_nomprog.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129006946761044209, "Descrizione"), 0, True)
      opdi_abilit.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129006946889641251, "Abilitato"), "S", "N")

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
  Public Overridable Sub FRM__DITT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      Me.Text = oApp.Tr(Me, 129006946993237653, "DETTAGLIO PROGRAMMI PER OPERATORE / MODULO ") & oCleAnaz.strUserAperto & " / " & oCleAnaz.nModuloAperto.ToString

      If dsDitt.Tables("ACCDITT").Rows.Count = 0 Then oCleAnaz.DittCaricaProgrammi()

      'devo bloccare/sbloccare i campi chiave se necessario
      grvDitt_NTSFocusedRowChanged(grvDitt, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__DITT_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
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
      oCleAnaz.DittSalva(True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvDitt.NTSRipristinaRigaCorrenteBefore(dcDitt, True) Then Return
      oCleAnaz.DittRipristina(dcDitt.Position, dcDitt.Filter)
      grvDitt.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbAggiungiProgrammi_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAggiungiProgrammi.ItemClick
    Try
      If Not Salva() Then Return

      oCleAnaz.DittCaricaProgrammi()

      oApp.MsgBoxInfo(oApp.Tr(Me, 129006947101052913, "Operazione completata"))

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
      If NTSCStr(grvDitt.GetFocusedRowCellValue("opdi_nomprog")).Trim <> "" Then
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

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvDitt.NTSSalvaRigaCorrente(dcDitt, oCleAnaz.DittRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleAnaz.DittSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleAnaz.DittRipristina(dcDitt.Position, dcDitt.Filter)
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
