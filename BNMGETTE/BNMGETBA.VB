Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGETBA
#Region "Variabili"
  Public oCleEtte As CLEMGETTE
  Public dsEtba As DataSet = Nothing
  Public dcEtba As BindingSource = New BindingSource()
  Public oCallParams As CLE__CLDP
  Public bControlloEsistenza As Boolean = False
  Public strCodart As String
  Public lIIstMatrTmp As Integer
  Public bOk As Boolean = True
  Private components As System.ComponentModel.IContainer

  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents grEtba As NTSInformatica.NTSGrid
  Public WithEvents grvEtba As NTSInformatica.NTSGridView
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tt_matric As NTSInformatica.NTSGridColumn
  Public WithEvents tt_riferim As NTSInformatica.NTSGridColumn
  Public WithEvents tt_qtacar As NTSInformatica.NTSGridColumn
  Public WithEvents tt_quant As NTSInformatica.NTSGridColumn
  Public WithEvents codditt As NTSInformatica.NTSGridColumn
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
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

    InitializeComponent()
    Me.MinimumSize = Me.Size

    Return True
  End Function

  Public Overridable Sub InitEntity(ByRef cleEtte As CLEMGETTE)
    oCleEtte = cleEtte
    AddHandler oCleEtte.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGETBA))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grEtba = New NTSInformatica.NTSGrid
    Me.grvEtba = New NTSInformatica.NTSGridView
    Me.tt_matric = New NTSInformatica.NTSGridColumn
    Me.tt_riferim = New NTSInformatica.NTSGridColumn
    Me.tt_qtacar = New NTSInformatica.NTSGridColumn
    Me.tt_quant = New NTSInformatica.NTSGridColumn
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grEtba, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvEtba, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbRipristina, Me.tlbCancella, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 34
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbSalva.Id = 31
    Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.Id = 32
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.Id = 33
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 10
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 26
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'grEtba
    '
    Me.grEtba.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '
    '
    '
    Me.grEtba.EmbeddedNavigator.Name = ""
    Me.grEtba.Location = New System.Drawing.Point(0, 36)
    Me.grEtba.MainView = Me.grvEtba
    Me.grEtba.Name = "grEtba"
    Me.grEtba.Size = New System.Drawing.Size(444, 203)
    Me.grEtba.TabIndex = 4
    Me.grEtba.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvEtba})
    '
    'grvEtba
    '
    Me.grvEtba.ActiveFilterEnabled = False
    Me.grvEtba.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tt_matric, Me.tt_riferim, Me.tt_qtacar, Me.tt_quant, Me.codditt})
    Me.grvEtba.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvEtba.Enabled = True
    Me.grvEtba.GridControl = Me.grEtba
    Me.grvEtba.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvEtba.GroupPanelText = "Per raggruppare: Tasto DX sulla colonna -> Group by this column *** Per totali pa" & _
        "rziali/generali: sul piede di gruppo della colonna tasto DX -> SUM "
    Me.grvEtba.Name = "grvEtba"
    Me.grvEtba.NTSAllowDelete = True
    Me.grvEtba.NTSAllowInsert = True
    Me.grvEtba.NTSAllowUpdate = True
    Me.grvEtba.NTSMenuContext = Nothing
    Me.grvEtba.OptionsCustomization.AllowRowSizing = True
    Me.grvEtba.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvEtba.OptionsNavigation.UseTabKey = False
    Me.grvEtba.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvEtba.OptionsView.ColumnAutoWidth = False
    Me.grvEtba.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvEtba.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvEtba.OptionsView.ShowGroupPanel = False
    Me.grvEtba.RowHeight = 14
    '
    'tt_matric
    '
    Me.tt_matric.AppearanceCell.Options.UseBackColor = True
    Me.tt_matric.AppearanceCell.Options.UseTextOptions = True
    Me.tt_matric.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_matric.AppearanceHeader.Options.UseTextOptions = True
    Me.tt_matric.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tt_matric.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tt_matric.Caption = "Barcode"
    Me.tt_matric.Enabled = False
    Me.tt_matric.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_matric.Name = "tt_matric"
    Me.tt_matric.NTSRepositoryComboBox = Nothing
    Me.tt_matric.NTSRepositoryItemCheck = Nothing
    Me.tt_matric.NTSRepositoryItemText = Nothing
    Me.tt_matric.OptionsColumn.AllowEdit = False
    Me.tt_matric.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_matric.OptionsColumn.ReadOnly = True
    Me.tt_matric.OptionsFilter.AllowFilter = False
    Me.tt_matric.Visible = True
    Me.tt_matric.VisibleIndex = 0
    Me.tt_matric.Width = 83
    '
    'tt_riferim
    '
    Me.tt_riferim.AppearanceCell.Options.UseBackColor = True
    Me.tt_riferim.AppearanceCell.Options.UseTextOptions = True
    Me.tt_riferim.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_riferim.AppearanceHeader.Options.UseTextOptions = True
    Me.tt_riferim.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tt_riferim.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tt_riferim.Caption = "Unità Mis."
    Me.tt_riferim.Enabled = False
    Me.tt_riferim.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_riferim.Name = "tt_riferim"
    Me.tt_riferim.NTSRepositoryComboBox = Nothing
    Me.tt_riferim.NTSRepositoryItemCheck = Nothing
    Me.tt_riferim.NTSRepositoryItemText = Nothing
    Me.tt_riferim.OptionsColumn.AllowEdit = False
    Me.tt_riferim.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_riferim.OptionsColumn.ReadOnly = True
    Me.tt_riferim.OptionsFilter.AllowFilter = False
    Me.tt_riferim.Visible = True
    Me.tt_riferim.VisibleIndex = 1
    Me.tt_riferim.Width = 71
    '
    'tt_qtacar
    '
    Me.tt_qtacar.AppearanceCell.Options.UseBackColor = True
    Me.tt_qtacar.AppearanceCell.Options.UseTextOptions = True
    Me.tt_qtacar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_qtacar.AppearanceHeader.Options.UseTextOptions = True
    Me.tt_qtacar.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tt_qtacar.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tt_qtacar.Caption = "Quantità"
    Me.tt_qtacar.Enabled = False
    Me.tt_qtacar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_qtacar.Name = "tt_qtacar"
    Me.tt_qtacar.NTSRepositoryComboBox = Nothing
    Me.tt_qtacar.NTSRepositoryItemCheck = Nothing
    Me.tt_qtacar.NTSRepositoryItemText = Nothing
    Me.tt_qtacar.OptionsColumn.AllowEdit = False
    Me.tt_qtacar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_qtacar.OptionsColumn.ReadOnly = True
    Me.tt_qtacar.OptionsFilter.AllowFilter = False
    Me.tt_qtacar.Visible = True
    Me.tt_qtacar.VisibleIndex = 2
    Me.tt_qtacar.Width = 83
    '
    'tt_quant
    '
    Me.tt_quant.AppearanceCell.Options.UseBackColor = True
    Me.tt_quant.AppearanceCell.Options.UseTextOptions = True
    Me.tt_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_quant.AppearanceHeader.Options.UseTextOptions = True
    Me.tt_quant.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tt_quant.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tt_quant.Caption = "Numero Etichette"
    Me.tt_quant.Enabled = True
    Me.tt_quant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_quant.Name = "tt_quant"
    Me.tt_quant.NTSRepositoryComboBox = Nothing
    Me.tt_quant.NTSRepositoryItemCheck = Nothing
    Me.tt_quant.NTSRepositoryItemText = Nothing
    Me.tt_quant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_quant.OptionsFilter.AllowFilter = False
    Me.tt_quant.Visible = True
    Me.tt_quant.VisibleIndex = 3
    Me.tt_quant.Width = 97
    '
    'codditt
    '
    Me.codditt.AppearanceCell.Options.UseBackColor = True
    Me.codditt.AppearanceCell.Options.UseTextOptions = True
    Me.codditt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.codditt.AppearanceHeader.Options.UseTextOptions = True
    Me.codditt.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.codditt.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.codditt.Caption = "codditt"
    Me.codditt.Enabled = False
    Me.codditt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.codditt.Name = "codditt"
    Me.codditt.NTSRepositoryComboBox = Nothing
    Me.codditt.NTSRepositoryItemCheck = Nothing
    Me.codditt.NTSRepositoryItemText = Nothing
    Me.codditt.OptionsColumn.AllowEdit = False
    Me.codditt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.codditt.OptionsColumn.ReadOnly = True
    Me.codditt.OptionsFilter.AllowFilter = False
    Me.codditt.Width = 83
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAnnulla.Location = New System.Drawing.Point(334, 245)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(98, 23)
    Me.cmdAnnulla.TabIndex = 5
    Me.cmdAnnulla.Text = "Annulla Stampa"
    '
    'FRMMGETBA
    '
    Me.ClientSize = New System.Drawing.Size(444, 280)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.grEtba)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMMGETBA"
    Me.Text = "Codici a barre"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grEtba, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvEtba, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      'Carico le immagini della Toolbar
      Try
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'Non gestisco l'errore: 
        'se non c'è una immagine prendo quella a design
      End Try

      grvEtba.NTSSetParam(oMenu, oApp.Tr(Me, 128674095538409079, "Codici a Barre"))
      tt_matric.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128674095538565329, "Barcode"), 0, True)
      tt_riferim.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128674095538721579, "Unità Mis."), 0, True)
      tt_qtacar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128674095538877829, "Quantità"), "#,##0.00", 15)
      tt_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128674095539034079, "Numero Etichette"), "#,##0.00", 15)
      codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128674095539190329, "codditt"), 0, True)

      grvEtba.NTSAllowInsert = False

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

#Region "Form"
  Public Overridable Sub FRMMGETBA_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcEtba.Dispose()
      If Not dsEtba Is Nothing Then dsEtba.Dispose()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGETBA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim lTot As Decimal = 0
    Try
      'Predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'Leggo dal database i dati e collego l'NTSBinding
      If oCleEtte.ApriEtba(lIIstMatrTmp, strCodart, dsEtba) Then
        dcEtba.DataSource = dsEtba.Tables("TTSTMATR")
        dsEtba.AcceptChanges()

        grEtba.DataSource = dcEtba

        '-------------------------------------------------
        'Applico le impostazioni personalizzate della form (Ex: CTRL+ALT+F2)
        'questa funzione può essere chiamata quando si vuole,
        'ad esempio dopo aver impostato la ditta, cambiato il tipo documento, ecc ...
        '(Ex: CTRL+ALT+F4)
        'GctlTipoDoc = ""
        GctlSetRoules()

        grvEtba.FocusedColumn = tt_quant
      Else
        Me.Close()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGTEBA_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Salva() = False Then e.Cancel = True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Toolbar"
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
      If grvEtba.NTSRipristinaRigaCorrenteBefore(dcEtba, CType(IIf(sender Is Nothing, False, True), Boolean)) Then
        oCleEtte.Ripristina(dcEtba.Position, dcEtba.Filter)
        grvEtba.NTSRipristinaRigaCorrenteAfter()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Try
      If grvEtba.NTSGetCurrentDataRow Is Nothing Then Return

      If grvEtba.NTSDeleteRigaCorrente(dcEtba, True) Then oCleEtte.EtbaSalva(True, lIIstMatrTmp)

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
    Try
      If Salva() = False Then Return
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      bOk = False
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvEtba.NTSSalvaRigaCorrente(dcEtba, oCleEtte.RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleEtte.EtbaSalva(False, lIIstMatrTmp) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleEtte.Ripristina(dcEtba.Position, dcEtba.Filter)
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