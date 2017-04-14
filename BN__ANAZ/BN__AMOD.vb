Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__AMOD
  Public oCleAnaz As CLE__ANAZ
  Public oCallParams As CLE__CLDP
  Public dsAmod As DataSet
  Public dcAmod As BindingSource = New BindingSource()

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
  Public WithEvents grAmod As NTSInformatica.NTSGrid
  Public WithEvents grvAmod As NTSInformatica.NTSGridView
  Public WithEvents am_modulo As NTSInformatica.NTSGridColumn
  Public WithEvents xx_desmodulo As NTSInformatica.NTSGridColumn
  Public WithEvents am_abilit As NTSInformatica.NTSGridColumn
  Public WithEvents xx_abinsg As NTSInformatica.NTSGridColumn
  Public WithEvents xx_abchiave As NTSInformatica.NTSGridColumn

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__AMOD))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grAmod = New NTSInformatica.NTSGrid
    Me.grvAmod = New NTSInformatica.NTSGridView
    Me.am_modulo = New NTSInformatica.NTSGridColumn
    Me.xx_desmodulo = New NTSInformatica.NTSGridColumn
    Me.am_abilit = New NTSInformatica.NTSGridColumn
    Me.xx_abinsg = New NTSInformatica.NTSGridColumn
    Me.xx_abchiave = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grAmod, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvAmod, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 17
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
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
    'grAmod
    '
    Me.grAmod.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grAmod.EmbeddedNavigator.Name = ""
    Me.grAmod.Location = New System.Drawing.Point(0, 26)
    Me.grAmod.MainView = Me.grvAmod
    Me.grAmod.Name = "grAmod"
    Me.grAmod.Size = New System.Drawing.Size(648, 416)
    Me.grAmod.TabIndex = 5
    Me.grAmod.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvAmod})
    '
    'grvAmod
    '
    Me.grvAmod.ActiveFilterEnabled = False
    Me.grvAmod.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.am_modulo, Me.xx_desmodulo, Me.am_abilit, Me.xx_abinsg, Me.xx_abchiave})
    Me.grvAmod.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvAmod.Enabled = True
    Me.grvAmod.GridControl = Me.grAmod
    Me.grvAmod.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvAmod.MinRowHeight = 14
    Me.grvAmod.Name = "grvAmod"
    Me.grvAmod.NTSAllowDelete = True
    Me.grvAmod.NTSAllowInsert = True
    Me.grvAmod.NTSAllowUpdate = True
    Me.grvAmod.NTSMenuContext = Nothing
    Me.grvAmod.OptionsCustomization.AllowRowSizing = True
    Me.grvAmod.OptionsFilter.AllowFilterEditor = False
    Me.grvAmod.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvAmod.OptionsNavigation.UseTabKey = False
    Me.grvAmod.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvAmod.OptionsView.ColumnAutoWidth = False
    Me.grvAmod.OptionsView.EnableAppearanceEvenRow = True
    Me.grvAmod.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvAmod.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvAmod.OptionsView.ShowGroupPanel = False
    Me.grvAmod.RowHeight = 16
    '
    'am_modulo
    '
    Me.am_modulo.AppearanceCell.Options.UseBackColor = True
    Me.am_modulo.AppearanceCell.Options.UseTextOptions = True
    Me.am_modulo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.am_modulo.Caption = "Modulo"
    Me.am_modulo.Enabled = False
    Me.am_modulo.FieldName = "am_modulo"
    Me.am_modulo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.am_modulo.Name = "am_modulo"
    Me.am_modulo.NTSRepositoryComboBox = Nothing
    Me.am_modulo.NTSRepositoryItemCheck = Nothing
    Me.am_modulo.NTSRepositoryItemMemo = Nothing
    Me.am_modulo.NTSRepositoryItemText = Nothing
    Me.am_modulo.OptionsColumn.AllowEdit = False
    Me.am_modulo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.am_modulo.OptionsColumn.ReadOnly = True
    Me.am_modulo.OptionsFilter.AllowFilter = False
    Me.am_modulo.Visible = True
    Me.am_modulo.VisibleIndex = 0
    Me.am_modulo.Width = 70
    '
    'xx_desmodulo
    '
    Me.xx_desmodulo.AppearanceCell.Options.UseBackColor = True
    Me.xx_desmodulo.AppearanceCell.Options.UseTextOptions = True
    Me.xx_desmodulo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_desmodulo.Caption = "Descr. modulo"
    Me.xx_desmodulo.Enabled = False
    Me.xx_desmodulo.FieldName = "xx_desmodulo"
    Me.xx_desmodulo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_desmodulo.Name = "xx_desmodulo"
    Me.xx_desmodulo.NTSRepositoryComboBox = Nothing
    Me.xx_desmodulo.NTSRepositoryItemCheck = Nothing
    Me.xx_desmodulo.NTSRepositoryItemMemo = Nothing
    Me.xx_desmodulo.NTSRepositoryItemText = Nothing
    Me.xx_desmodulo.OptionsColumn.AllowEdit = False
    Me.xx_desmodulo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_desmodulo.OptionsColumn.ReadOnly = True
    Me.xx_desmodulo.OptionsFilter.AllowFilter = False
    Me.xx_desmodulo.Visible = True
    Me.xx_desmodulo.VisibleIndex = 1
    Me.xx_desmodulo.Width = 110
    '
    'am_abilit
    '
    Me.am_abilit.AppearanceCell.Options.UseBackColor = True
    Me.am_abilit.AppearanceCell.Options.UseTextOptions = True
    Me.am_abilit.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.am_abilit.Caption = "Abilitato"
    Me.am_abilit.Enabled = True
    Me.am_abilit.FieldName = "am_abilit"
    Me.am_abilit.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.am_abilit.Name = "am_abilit"
    Me.am_abilit.NTSRepositoryComboBox = Nothing
    Me.am_abilit.NTSRepositoryItemCheck = Nothing
    Me.am_abilit.NTSRepositoryItemMemo = Nothing
    Me.am_abilit.NTSRepositoryItemText = Nothing
    Me.am_abilit.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.am_abilit.OptionsFilter.AllowFilter = False
    Me.am_abilit.Visible = True
    Me.am_abilit.VisibleIndex = 2
    Me.am_abilit.Width = 70
    '
    'xx_abinsg
    '
    Me.xx_abinsg.AppearanceCell.Options.UseBackColor = True
    Me.xx_abinsg.AppearanceCell.Options.UseTextOptions = True
    Me.xx_abinsg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_abinsg.Caption = "Abilit. in Iniz. globali"
    Me.xx_abinsg.Enabled = False
    Me.xx_abinsg.FieldName = "xx_abinsg"
    Me.xx_abinsg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_abinsg.Name = "xx_abinsg"
    Me.xx_abinsg.NTSRepositoryComboBox = Nothing
    Me.xx_abinsg.NTSRepositoryItemCheck = Nothing
    Me.xx_abinsg.NTSRepositoryItemMemo = Nothing
    Me.xx_abinsg.NTSRepositoryItemText = Nothing
    Me.xx_abinsg.OptionsColumn.AllowEdit = False
    Me.xx_abinsg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_abinsg.OptionsColumn.ReadOnly = True
    Me.xx_abinsg.OptionsFilter.AllowFilter = False
    Me.xx_abinsg.Visible = True
    Me.xx_abinsg.VisibleIndex = 3
    Me.xx_abinsg.Width = 110
    '
    'xx_abchiave
    '
    Me.xx_abchiave.AppearanceCell.Options.UseBackColor = True
    Me.xx_abchiave.AppearanceCell.Options.UseTextOptions = True
    Me.xx_abchiave.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_abchiave.Caption = "Abilit. in chiave"
    Me.xx_abchiave.Enabled = False
    Me.xx_abchiave.FieldName = "xx_abchiave"
    Me.xx_abchiave.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_abchiave.Name = "xx_abchiave"
    Me.xx_abchiave.NTSRepositoryComboBox = Nothing
    Me.xx_abchiave.NTSRepositoryItemCheck = Nothing
    Me.xx_abchiave.NTSRepositoryItemMemo = Nothing
    Me.xx_abchiave.NTSRepositoryItemText = Nothing
    Me.xx_abchiave.OptionsColumn.AllowEdit = False
    Me.xx_abchiave.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_abchiave.OptionsColumn.ReadOnly = True
    Me.xx_abchiave.OptionsFilter.AllowFilter = False
    Me.xx_abchiave.Visible = True
    Me.xx_abchiave.VisibleIndex = 4
    Me.xx_abchiave.Width = 110
    '
    'FRM__AMOD
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.grAmod)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRM__AMOD"
    Me.NTSLastControlFocussed = Me.grAmod
    Me.Text = "SERVIZI ABILITATI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grAmod, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvAmod, System.ComponentModel.ISupportInitialize).EndInit()
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
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvAmod.NTSSetParam(oMenu, "SERVIZI ABILITATI")
      am_modulo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129006949553771951, "Modulo"), "0", 3)
      xx_desmodulo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129006949697056869, "Descr. modulo"), 0, True)
      am_abilit.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129006949768152439, "Abilitato"), "S", "N")
      xx_abinsg.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128647292816406250, "Abilitato in iniz. globali"), "S", "N")
      xx_abchiave.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129042226932968750, "Abilit. in chiave"), "S", "N")

      grvAmod.NTSAllowInsert = False
      grvAmod.NTSAllowDelete = False

      grvAmod.AddColumnBackColor("backcolor_am_abilit") 'sempre nella InitControls
      grvAmod.AddColumnBackColor("backcolor_xx_abinsg") 'sempre nella InitControls

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
  Public Overridable Sub FRM__AMOD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      dsAmod = oCleAnaz.dsShared
      dcAmod.DataSource = dsAmod.Tables("ANAZMOD")
      dcAmod.Filter = "xx_desmodulo NOT LIKE '%.....................%'"

      For Each dtrT As DataRow In dsAmod.Tables("ANAZMOD").Rows
        AssegnaColoriGriglia(dtrT)
      Next
      dsAmod.Tables("ANAZMOD").AcceptChanges()

      grAmod.DataSource = dcAmod

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__AMOD_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvAmod.NTSRipristinaRigaCorrenteBefore(dcAmod, True) Then Return
      oCleAnaz.AmodRipristina(dcAmod.Position, dcAmod.Filter)
      grvAmod.NTSRipristinaRigaCorrenteAfter()
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

#Region "Eventi di griglia"
  Public Overridable Sub grvAmod_CustomDrawCell(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles grvAmod.CustomDrawCell
    Try
      'obsoleta

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvAmod_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvAmod.NTSBeforeRowUpdate
    Try
      If Not Salva() Then
        'rimango sulla riga su cui sono
        e.Allow = False
      End If

      AssegnaColoriGriglia(grvAmod.NTSGetCurrentDataRow)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvAmod_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvAmod.NTSFocusedRowChanged
    Try

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvAmod_FocusedColumnChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles grvAmod.FocusedColumnChanged
    Try

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Function AssegnaColoriGriglia(ByRef dtrT As DataRow) As Boolean
    Dim dTempoPrevisto As Decimal = 0
    Dim dTempoTmp As Decimal = 0
    Try
      'quando viene cambiato il colore della cella backcolor_row o della backcolor_*, 
      'sbs invia al client la segnalazione di 'cella cambiata'
      'su SBC la cellace backcolor_ non viene mai disegnata, ma viene utilizzata per colorare la riga/cella
      'NB: la colonna backcolor_ deve essere inserita anche nella griglia (magari non visibile) altrimenti non viene passata a SBC!!!

      'dtrIn.Table.Columns.Add("backcolor_row", GetType(Integer))        'costante 'backcolor_row' utilizzato da SBC per colorare tutta la riga
      'dtrIn.Table.Columns.Add("backcolor_xx_status", GetType(Integer))  'prefisso 'backcolor_' utilizzato da SBC per colorare la singola cella

      If dtrT Is Nothing Then Return True

      'memorizzo lo stato prima della modifica del campo
      Dim oState As DataRowState = dtrT.RowState
      Dim bOk As Boolean = oCleAnaz.bHasChanges

      If dtrT.Table.Columns.Contains("backcolor_am_abilit") = False Then
        dtrT.Table.Columns.Add("backcolor_am_abilit", GetType(Integer))
        dtrT.Table.Columns.Add("backcolor_xx_abinsg", GetType(Integer))
      End If

      'per default non è
      dtrT!backcolor_am_abilit = -1
      dtrT!backcolor_xx_abinsg = -1

      'Controllo che i moduli abilitati siano presenti sia in xx_Abinsg e xx_abchiave
      If NTSCStr(dtrT!am_abilit) = "S" And (NTSCStr(dtrT!xx_abinsg) = "N" Or NTSCStr(dtrT!xx_abchiave) = "N") Then
        dtrT!backcolor_am_abilit = Color.Crimson.ToArgb
      End If
      If NTSCStr(dtrT!xx_abinsg) = "S" And NTSCStr(dtrT!xx_abchiave) = "N" Then
        If oApp.ActKey.MultiKey = "N" Then
          dtrT!backcolor_xx_abinsg = Color.IndianRed.ToArgb
        Else
          dtrT!backcolor_xx_abinsg = Color.LightGreen.ToArgb
        End If
      End If

      'reimposto lo stato a prima della modifica del campo
      If oState = DataRowState.Unchanged Then dtrT.AcceptChanges()
      oCleAnaz.bHasChanges = bOk

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvAmod.NTSSalvaRigaCorrente(dcAmod, oCleAnaz.AmodRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleAnaz.AmodSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleAnaz.AmodRipristina(dcAmod.Position, dcAmod.Filter)
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
