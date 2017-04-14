Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__STPG
  Public oCleclie As CLE__CLIE
  Public oCallParams As CLE__CLDP
  Public dsStpg As DataSet
  Public dcStpg As BindingSource = New BindingSource()

  Private components As System.ComponentModel.IContainer

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__STPG))
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
    Me.grStpg = New NTSInformatica.NTSGrid
    Me.grvStpg = New NTSInformatica.NTSGridView
    Me.cts_codstpg = New NTSInformatica.NTSGridColumn
    Me.xx_desstpg = New NTSInformatica.NTSGridColumn
    Me.cts_ggritmed = New NTSInformatica.NTSGridColumn
    Me.pnStpg = New NTSInformatica.NTSPanel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grStpg, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvStpg, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnStpg, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnStpg.SuspendLayout()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'frmAuto
    '
    Me.frmAuto.Appearance.BackColor = System.Drawing.Color.Black
    Me.frmAuto.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmAuto.Appearance.Options.UseBackColor = True
    Me.frmAuto.Appearance.Options.UseImage = True
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom})
    Me.NtsBarManager1.MaxItemId = 17
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
    Me.tlbZoom.Id = 13
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
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
    'grStpg
    '
    Me.grStpg.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grStpg.EmbeddedNavigator.Name = ""
    Me.grStpg.Location = New System.Drawing.Point(0, 0)
    Me.grStpg.MainView = Me.grvStpg
    Me.grStpg.Name = "grStpg"
    Me.grStpg.Size = New System.Drawing.Size(648, 416)
    Me.grStpg.TabIndex = 5
    Me.grStpg.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvStpg})
    '
    'grvStpg
    '
    Me.grvStpg.ActiveFilterEnabled = False
    Me.grvStpg.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cts_codstpg, Me.xx_desstpg, Me.cts_ggritmed})
    Me.grvStpg.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvStpg.Enabled = True
    Me.grvStpg.GridControl = Me.grStpg
    Me.grvStpg.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvStpg.Name = "grvStpg"
    Me.grvStpg.NTSAllowDelete = True
    Me.grvStpg.NTSAllowInsert = True
    Me.grvStpg.NTSAllowUpdate = True
    Me.grvStpg.NTSMenuContext = Nothing
    Me.grvStpg.OptionsCustomization.AllowRowSizing = True
    Me.grvStpg.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvStpg.OptionsNavigation.UseTabKey = False
    Me.grvStpg.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvStpg.OptionsView.ColumnAutoWidth = False
    Me.grvStpg.OptionsView.EnableAppearanceEvenRow = True
    Me.grvStpg.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvStpg.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvStpg.OptionsView.ShowGroupPanel = False
    Me.grvStpg.RowHeight = 16
    '
    'cts_codstpg
    '
    Me.cts_codstpg.AppearanceCell.Options.UseBackColor = True
    Me.cts_codstpg.AppearanceCell.Options.UseTextOptions = True
    Me.cts_codstpg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.cts_codstpg.Caption = "Cod. sottotipo pag."
    Me.cts_codstpg.Enabled = True
    Me.cts_codstpg.FieldName = "cts_codstpg"
    Me.cts_codstpg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.cts_codstpg.Name = "cts_codstpg"
    Me.cts_codstpg.NTSRepositoryComboBox = Nothing
    Me.cts_codstpg.NTSRepositoryItemCheck = Nothing
    Me.cts_codstpg.NTSRepositoryItemMemo = Nothing
    Me.cts_codstpg.NTSRepositoryItemText = Nothing
    Me.cts_codstpg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.cts_codstpg.OptionsFilter.AllowFilter = False
    Me.cts_codstpg.Visible = True
    Me.cts_codstpg.VisibleIndex = 0
    Me.cts_codstpg.Width = 88
    '
    'xx_desstpg
    '
    Me.xx_desstpg.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
    Me.xx_desstpg.AppearanceCell.Options.UseBackColor = True
    Me.xx_desstpg.AppearanceCell.Options.UseTextOptions = True
    Me.xx_desstpg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_desstpg.Caption = "Descr. sottotipo pag."
    Me.xx_desstpg.Enabled = False
    Me.xx_desstpg.FieldName = "xx_desstpg"
    Me.xx_desstpg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_desstpg.Name = "xx_desstpg"
    Me.xx_desstpg.NTSRepositoryComboBox = Nothing
    Me.xx_desstpg.NTSRepositoryItemCheck = Nothing
    Me.xx_desstpg.NTSRepositoryItemMemo = Nothing
    Me.xx_desstpg.NTSRepositoryItemText = Nothing
    Me.xx_desstpg.OptionsColumn.AllowEdit = False
    Me.xx_desstpg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_desstpg.OptionsColumn.ReadOnly = True
    Me.xx_desstpg.OptionsFilter.AllowFilter = False
    Me.xx_desstpg.Visible = True
    Me.xx_desstpg.VisibleIndex = 1
    Me.xx_desstpg.Width = 240
    '
    'cts_ggritmed
    '
    Me.cts_ggritmed.AppearanceCell.Options.UseBackColor = True
    Me.cts_ggritmed.AppearanceCell.Options.UseTextOptions = True
    Me.cts_ggritmed.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.cts_ggritmed.Caption = "Tempo medio ritardo gg inc/pag"
    Me.cts_ggritmed.Enabled = True
    Me.cts_ggritmed.FieldName = "cts_ggritmed"
    Me.cts_ggritmed.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.cts_ggritmed.Name = "cts_ggritmed"
    Me.cts_ggritmed.NTSRepositoryComboBox = Nothing
    Me.cts_ggritmed.NTSRepositoryItemCheck = Nothing
    Me.cts_ggritmed.NTSRepositoryItemMemo = Nothing
    Me.cts_ggritmed.NTSRepositoryItemText = Nothing
    Me.cts_ggritmed.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.cts_ggritmed.OptionsFilter.AllowFilter = False
    Me.cts_ggritmed.Visible = True
    Me.cts_ggritmed.VisibleIndex = 2
    Me.cts_ggritmed.Width = 70
    '
    'pnStpg
    '
    Me.pnStpg.AllowDrop = True
    Me.pnStpg.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnStpg.Appearance.Options.UseBackColor = True
    Me.pnStpg.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnStpg.Controls.Add(Me.grStpg)
    Me.pnStpg.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnStpg.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnStpg.Location = New System.Drawing.Point(0, 26)
    Me.pnStpg.Name = "pnStpg"
    Me.pnStpg.Size = New System.Drawing.Size(648, 416)
    Me.pnStpg.TabIndex = 6
    Me.pnStpg.Text = "NtsPanel1"
    '
    'FRM__STPG
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.pnStpg)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRM__STPG"
    Me.NTSLastControlFocussed = Me.grStpg
    Me.Text = "SOTTOTIPI PAGAMENT CLI\FOR"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grStpg, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvStpg, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnStpg, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnStpg.ResumeLayout(False)
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

  Public Overridable Sub InitEntity(ByRef cleStpg As CLE__CLIE)
    oCleclie = cleStpg
    AddHandler oCleclie.RemoteEvent, AddressOf GestisciEventiEntity
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
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvStpg.NTSSetParam(oMenu, "Sottotipi pagamento cli\for")
      cts_codstpg.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128387232249038000, "Cod. sottotipo pag."), tabstpg)
      cts_ggritmed.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128387232249194000, "Tempo medio ritardo gg inc/pag"), "0", 999, 0, 999)
      xx_desstpg.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128387232249974000, "Descr. sottotipo pag."), 50, True)

      cts_codstpg.NTSSetRichiesto()

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
  Public Overridable Sub FRM__STPG_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      dsStpg = oCleclie.dsShared
      dcStpg.DataSource = dsStpg.Tables("CLISTPG")
      dsStpg.AcceptChanges()

      grStpg.DataSource = dcStpg

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlTipoDoc = oCleclie.strTipoConto
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__STPG_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub FRM__STPG_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcStpg.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvStpg.NTSNuovo()

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
      If Not grvStpg.NTSDeleteRigaCorrente(dcStpg, True) Then Return
      oCleclie.StpgSalva(True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvStpg.NTSRipristinaRigaCorrenteBefore(dcStpg, True) Then Return
      oCleclie.StpgRipristina(dcStpg.Position, dcStpg.Filter)
      grvStpg.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
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
    Me.Close()
  End Sub
#End Region

  Public Overridable Sub grvStpg_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvStpg.NTSBeforeRowUpdate
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
  Public Overridable Sub grvStpg_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvStpg.NTSFocusedRowChanged
    Try

      If grvStpg.FocusedRowHandle = -999999 Then Return

      If Not grvStpg.FocusedRowHandle = -999998 Then
        Select Case grvStpg.GetDataRow(grvStpg.FocusedRowHandle).RowState
          Case DataRowState.Added
            If Not cts_codstpg.Enabled Then GctlSetVisEnab(cts_codstpg, False)
          Case Else
            cts_codstpg.Enabled = False
        End Select
      Else
        If Not cts_codstpg.Enabled Then GctlSetVisEnab(cts_codstpg, False)
      End If

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub



  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvStpg.NTSSalvaRigaCorrente(dcStpg, oCleclie.RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleclie.StpgSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleclie.StpgRipristina(dcStpg.Position, dcStpg.Filter)
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
