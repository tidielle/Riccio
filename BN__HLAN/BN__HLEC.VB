Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__HLEC
  Public oCleHlan As CLE__HLAN
  Public dsHlec As DataSet
  Public oCallParams As CLE__CLDP
  Public dcHlec As BindingSource = New BindingSource()
  Public strCodPcon As String = ""
  Public strTipoConto As String = ""

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents grHlec As NTSInformatica.NTSGrid
  Public WithEvents grvHlec As NTSInformatica.NTSGridView
  Public WithEvents es_codpco As NTSInformatica.NTSGridColumn
  Public WithEvents es_coddit As NTSInformatica.NTSGridColumn
  Public WithEvents xx_coddit As NTSInformatica.NTSGridColumn
  Public WithEvents es_conto As NTSInformatica.NTSGridColumn
  Public WithEvents xx_conto As NTSInformatica.NTSGridColumn


  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__HLEC))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grHlec = New NTSInformatica.NTSGrid
    Me.grvHlec = New NTSInformatica.NTSGridView
    Me.es_codpco = New NTSInformatica.NTSGridColumn
    Me.es_coddit = New NTSInformatica.NTSGridColumn
    Me.xx_coddit = New NTSInformatica.NTSGridColumn
    Me.es_conto = New NTSInformatica.NTSGridColumn
    Me.xx_conto = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grHlec, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvHlec, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbRipristina, Me.tlbSalva, Me.tlbCancella, Me.tlbZoom, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 7
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
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.Id = 1
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.Id = 2
    Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
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
    Me.tlbZoom.Id = 4
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 5
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 6
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'grHlec
    '
    Me.grHlec.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grHlec.EmbeddedNavigator.Name = ""
    Me.grHlec.Location = New System.Drawing.Point(0, 26)
    Me.grHlec.MainView = Me.grvHlec
    Me.grHlec.Name = "grHlec"
    Me.grHlec.Size = New System.Drawing.Size(603, 240)
    Me.grHlec.TabIndex = 42
    Me.grHlec.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvHlec})
    '
    'grvHlec
    '
    Me.grvHlec.ActiveFilterEnabled = False
    Me.grvHlec.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.es_codpco, Me.es_coddit, Me.xx_coddit, Me.es_conto, Me.xx_conto})
    Me.grvHlec.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvHlec.Enabled = True
    Me.grvHlec.GridControl = Me.grHlec
    Me.grvHlec.Name = "grvHlec"
    Me.grvHlec.NTSAllowDelete = True
    Me.grvHlec.NTSAllowInsert = True
    Me.grvHlec.NTSAllowUpdate = True
    Me.grvHlec.NTSMenuContext = Nothing
    Me.grvHlec.OptionsCustomization.AllowRowSizing = True
    Me.grvHlec.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvHlec.OptionsNavigation.UseTabKey = False
    Me.grvHlec.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvHlec.OptionsView.ColumnAutoWidth = False
    Me.grvHlec.OptionsView.EnableAppearanceEvenRow = True
    Me.grvHlec.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvHlec.OptionsView.ShowGroupPanel = False
    Me.grvHlec.RowHeight = 16
    '
    'es_codpco
    '
    Me.es_codpco.AppearanceCell.Options.UseBackColor = True
    Me.es_codpco.AppearanceCell.Options.UseTextOptions = True
    Me.es_codpco.Caption = "Piano dei conti"
    Me.es_codpco.Enabled = True
    Me.es_codpco.FieldName = "es_codpco"
    Me.es_codpco.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.es_codpco.Name = "es_codpco"
    Me.es_codpco.NTSRepositoryComboBox = Nothing
    Me.es_codpco.NTSRepositoryItemCheck = Nothing
    Me.es_codpco.NTSRepositoryItemText = Nothing
    Me.es_codpco.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.es_codpco.OptionsFilter.AllowFilter = False
    Me.es_codpco.Visible = True
    Me.es_codpco.VisibleIndex = 0
    Me.es_codpco.Width = 97
    '
    'es_coddit
    '
    Me.es_coddit.AppearanceCell.Options.UseBackColor = True
    Me.es_coddit.AppearanceCell.Options.UseTextOptions = True
    Me.es_coddit.Caption = "Codice ditta"
    Me.es_coddit.Enabled = True
    Me.es_coddit.FieldName = "es_coddit"
    Me.es_coddit.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.es_coddit.Name = "es_coddit"
    Me.es_coddit.NTSRepositoryComboBox = Nothing
    Me.es_coddit.NTSRepositoryItemCheck = Nothing
    Me.es_coddit.NTSRepositoryItemText = Nothing
    Me.es_coddit.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.es_coddit.OptionsFilter.AllowFilter = False
    Me.es_coddit.Visible = True
    Me.es_coddit.VisibleIndex = 1
    Me.es_coddit.Width = 100
    '
    'xx_coddit
    '
    Me.xx_coddit.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.xx_coddit.AppearanceCell.Options.UseBackColor = True
    Me.xx_coddit.AppearanceCell.Options.UseTextOptions = True
    Me.xx_coddit.Caption = "Descrizione ditta"
    Me.xx_coddit.Enabled = False
    Me.xx_coddit.FieldName = "xx_coddit"
    Me.xx_coddit.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_coddit.Name = "xx_coddit"
    Me.xx_coddit.NTSRepositoryComboBox = Nothing
    Me.xx_coddit.NTSRepositoryItemCheck = Nothing
    Me.xx_coddit.NTSRepositoryItemText = Nothing
    Me.xx_coddit.OptionsColumn.AllowEdit = False
    Me.xx_coddit.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_coddit.OptionsColumn.ReadOnly = True
    Me.xx_coddit.OptionsFilter.AllowFilter = False
    Me.xx_coddit.Visible = True
    Me.xx_coddit.VisibleIndex = 2
    Me.xx_coddit.Width = 149
    '
    'es_conto
    '
    Me.es_conto.AppearanceCell.Options.UseBackColor = True
    Me.es_conto.AppearanceCell.Options.UseTextOptions = True
    Me.es_conto.Caption = "Sottoconto"
    Me.es_conto.Enabled = True
    Me.es_conto.FieldName = "es_conto"
    Me.es_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.es_conto.Name = "es_conto"
    Me.es_conto.NTSRepositoryComboBox = Nothing
    Me.es_conto.NTSRepositoryItemCheck = Nothing
    Me.es_conto.NTSRepositoryItemText = Nothing
    Me.es_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.es_conto.OptionsFilter.AllowFilter = False
    Me.es_conto.Visible = True
    Me.es_conto.VisibleIndex = 3
    Me.es_conto.Width = 102
    '
    'xx_conto
    '
    Me.xx_conto.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.xx_conto.AppearanceCell.Options.UseBackColor = True
    Me.xx_conto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_conto.Caption = "Descr. sottoconto"
    Me.xx_conto.Enabled = False
    Me.xx_conto.FieldName = "xx_conto"
    Me.xx_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_conto.Name = "xx_conto"
    Me.xx_conto.NTSRepositoryComboBox = Nothing
    Me.xx_conto.NTSRepositoryItemCheck = Nothing
    Me.xx_conto.NTSRepositoryItemText = Nothing
    Me.xx_conto.OptionsColumn.AllowEdit = False
    Me.xx_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_conto.OptionsColumn.ReadOnly = True
    Me.xx_conto.OptionsFilter.AllowFilter = False
    Me.xx_conto.Visible = True
    Me.xx_conto.VisibleIndex = 4
    Me.xx_conto.Width = 164
    '
    'FRM__HLEC
    '
    Me.ClientSize = New System.Drawing.Size(603, 266)
    Me.Controls.Add(Me.grHlec)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRM__HLEC"
    Me.NTSLastControlFocussed = Me.grHlec
    Me.Text = "Escludi conti"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grHlec, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvHlec, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    oMenu = Menu
    oApp = oMenu.App
    oCallParams = Param
    strCodPcon = Param.strPar1    'ricevo il codice del piano dei conti da trattare
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

  Public Overridable Sub InitEntity(ByRef cleHlan As CLE__HLAN)
    oCleHlan = cleHlan
    AddHandler oCleHlan.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Sub InitControls()
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

      grvHlec.NTSSetParam(oMenu, oApp.Tr(Me, 128230023442278172, "Griglia conti esclusi da Zoom"))
      es_codpco.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128230023442434345, "Codice piano dei conti"), tabpcon, True)
      es_coddit.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128230023442590518, "Codice ditta"), tabanaz, True)
      xx_coddit.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023442746691, "Descrizione ditta"), 40, True)
      es_conto.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023442902864, "Sottoconto da escludere"), tabanagra)
      xx_conto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023443059037, "Descrizione sottoconto da escludere"), 30, True)

      es_conto.NTSSetRichiesto()

      'non devo lanciare lo zoom standard
      es_conto.NTSSetParamZoom("")
      es_conto.NTSForzaVisZoom = True

      '-------------------------------------------------
      'chiamo lo script per inizializzare i controlli caricati con source ext
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Public Overridable Sub FRM__HLEC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleHlan.HlecApri(DittaCorrente, strCodPcon, dsHlec) Then Me.Close()
      dcHlec.DataSource = dsHlec.Tables("ESCONTI")
      dsHlec.AcceptChanges()

      grHlec.DataSource = dcHlec

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__HLEC_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub FRM__HLEC_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    'salvo l'impostazione della griglia. devo farlo anche al cambio del tipo documento (ad esempio in bsveboll
    dcHlec.Dispose()
    dsHlec.Dispose()
  End Sub

  Public Overridable Sub grvHlec_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvHlec.NTSBeforeRowUpdate
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

  Public Overridable Sub grvHlec_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvHlec.NTSFocusedRowChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(grvHlec.GetFocusedRowCellValue(es_conto).ToString.Trim) <> 0 Then
        es_codpco.Enabled = False
        es_coddit.Enabled = False
        es_conto.Enabled = False
      Else
        GctlSetVisEnab(es_codpco, False)
        GctlSetVisEnab(es_coddit, False)
        GctlSetVisEnab(es_conto, False)
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvHlec.NTSSalvaRigaCorrente(dcHlec, oCleHlan.HlecRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleHlan.HlecSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleHlan.HlecRipristina(dcHlec.Position, dcHlec.Filter)
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


#Region "Eventi Toolbar"

  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvHlec.NTSNuovo("es_codpco")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvHlec.NTSRipristinaRigaCorrenteBefore(dcHlec, True) Then Return
      oCleHlan.HlecRipristina(dcHlec.Position, dcHlec.Filter)
      grvHlec.NTSRipristinaRigaCorrenteAfter()
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
      If Not grvHlec.NTSDeleteRigaCorrente(dcHlec, True) Then Return
      oCleHlan.HlecSalva(True)
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
      If ctrlTmp.GetType.ToString.IndexOf("NTSTextBox") > -1 Then
        SetFastZoom(CType(ctrlTmp, NTSTextBox).Text, oParam)    'abilito la gestione dello zoom veloce
        CType(ctrlTmp, NTSTextBox).Zoom()
      ElseIf ctrlTmp.GetType.ToString.IndexOf("NTSGrid") > -1 Then
        SetFastZoom(NTSCStr(CType(CType(ctrlTmp, NTSGrid).DefaultView, NTSGridView).EditingValue), oParam)

        If grvHlec.FocusedColumn.Name = "es_conto" Then
          'non posso lanciare lo zoom standard, visto che devo lanciare quello su anagra se è indicata la ditta,
          'quello su anagpc se è indicato il pdc, oppure dare un messaggio se non è indicato nessun dei due

          If NTSCStr(grvHlec.GetFocusedRowCellValue(es_codpco)).Trim = "" And _
             NTSCStr(grvHlec.GetFocusedRowCellValue(es_coddit)).Trim = "" Then
            oApp.MsgBoxErr(oApp.Tr(Me, 127791222143593750, "Attenzione: prima indicare o il codice ditta o il piano dei conti"))
            Return
          End If

          If NTSCStr(grvHlec.GetFocusedRowCellValue(es_coddit)).Trim <> "" Then
            'zoom su anagra
            oParam.bTipoProposto = True
            oParam.strTipo = IIf(strTipoConto.Trim = "", "C", strTipoConto).ToString
            'ricordarsi della riga per abilitare la gestione dello zoom veloce per gli zoom diversi da quello per cui basta fare solo F5
            SetFastZoom(NTSCStr(CType(CType(ctrlTmp, NTSGrid).DefaultView, NTSGridView).EditingValue), oParam)
            NTSZOOM.strIn = NTSCStr(grvHlec.EditingValue)
            NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
            If NTSZOOM.strIn <> NTSCStr(grvHlec.EditingValue) Then grvHlec.SetFocusedRowCellValue(es_conto, NTSZOOM.strIn)
            Return
          End If

          If NTSCStr(grvHlec.GetFocusedRowCellValue(es_codpco)).Trim <> "" Then
            'zoom su anagpc
            oParam.strCodPdc = grvHlec.GetFocusedRowCellValue(es_codpco).ToString
            SetFastZoom(NTSCStr(CType(CType(ctrlTmp, NTSGrid).DefaultView, NTSGridView).EditingValue), oParam)
            NTSZOOM.strIn = NTSCStr(grvHlec.EditingValue)
            NTSZOOM.ZoomStrIn("ZOOMANAGPC", DittaCorrente, oParam)
            If NTSZOOM.strIn <> NTSCStr(grvHlec.EditingValue) Then grvHlec.SetFocusedRowCellValue(es_conto, NTSZOOM.strIn)
            Return
          End If
        Else
          '------------------------------------
          'zoom standard di textbox e griglia
          NTSCallStandardZoom()
        End If
      End If

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


  Public Overridable Sub es_codpco_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles es_codpco.NTSZoomGest
    Dim oCZoo As New CLE__CZOO
    Dim bNuovo As Boolean = True
    Dim oTmp As New Control
    Dim oPar As New CLE__CLDP
    Try
      Me.ValidaLastControl()

      If e.TipoEvento = "OPEN" Then
        bNuovo = False
        oPar.strParam = "APRI;"
      Else
        oPar.strParam = "NUOV;"
      End If
      oPar.strParam += grvHlec.GetFocusedRowCellValue(es_codpco).ToString.PadRight(12) & ";" 'passo il piano dei conti
      oTmp.Text = NTSCStr(grvHlec.EditingValue)
      oPar.strParam += oTmp.Text

      NTSZOOM.OpenChildGest(oTmp, "ZOOMANAGPC", DittaCorrente, bNuovo, oPar)

      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo
      grHlec.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try

  End Sub

#End Region

End Class
