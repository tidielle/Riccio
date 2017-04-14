Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__DITM
  Public oCleAnaz As CLE__ANAZ
  Public oCallParams As CLE__CLDP
  Public dsDitm As DataSet
  Public dcDitm As BindingSource = New BindingSource()

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
  Public WithEvents tlbDettaglioProgrammi As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbAggiungiModuli As NTSInformatica.NTSBarButtonItem
  Public WithEvents grDitm As NTSInformatica.NTSGrid
  Public WithEvents grvDitm As NTSInformatica.NTSGridView
  Public WithEvents opdi_modulo As NTSInformatica.NTSGridColumn
  Public WithEvents xx_modulo As NTSInformatica.NTSGridColumn
  Public WithEvents opdi_abilit As NTSInformatica.NTSGridColumn

  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__DITM))
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
    Me.grDitm = New NTSInformatica.NTSGrid
    Me.grvDitm = New NTSInformatica.NTSGridView
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
    'NtsBarManager1
    '
    Me.NtsBarManager1.AllowCustomization = False
    Me.NtsBarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.tlbMain})
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlTop)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlBottom)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlLeft)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlRight)
    Me.NtsBarManager1.Form = Me
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci, Me.tlbNuovo, Me.tlbCancella, Me.tlbAggiungiModuli, Me.tlbDettaglioProgrammi})
    Me.NtsBarManager1.MaxItemId = 21
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
    Me.tlbAggiungiModuli.Name = "tlbAggiungiModuli"
    Me.tlbAggiungiModuli.Visible = True
    '
    'tlbDettaglioProgrammi
    '
    Me.tlbDettaglioProgrammi.Caption = "Dettaglio programmi"
    Me.tlbDettaglioProgrammi.Glyph = CType(resources.GetObject("tlbDettaglioProgrammi.Glyph"), System.Drawing.Image)
    Me.tlbDettaglioProgrammi.Id = 20
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
    'grDitm
    '
    Me.grDitm.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grDitm.EmbeddedNavigator.Name = ""
    Me.grDitm.Location = New System.Drawing.Point(0, 30)
    Me.grDitm.MainView = Me.grvDitm
    Me.grDitm.Name = "grDitm"
    Me.grDitm.Size = New System.Drawing.Size(648, 412)
    Me.grDitm.TabIndex = 5
    Me.grDitm.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDitm})
    '
    'grvDitm
    '
    Me.grvDitm.ActiveFilterEnabled = False
    Me.grvDitm.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.opdi_modulo, Me.xx_modulo, Me.opdi_abilit})
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
    Me.grvDitm.OptionsView.EnableAppearanceEvenRow = True
    Me.grvDitm.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvDitm.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvDitm.OptionsView.ShowGroupPanel = False
    Me.grvDitm.RowHeight = 16
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
    Me.xx_modulo.NTSRepositoryItemText = Nothing
    Me.xx_modulo.OptionsColumn.AllowEdit = False
    Me.xx_modulo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_modulo.OptionsColumn.ReadOnly = True
    Me.xx_modulo.OptionsFilter.AllowFilter = False
    Me.xx_modulo.Visible = True
    Me.xx_modulo.VisibleIndex = 1
    Me.xx_modulo.Width = 70
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
    'FRM__DITM
    '
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.grDitm)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRM__DITM"
    Me.NTSLastControlFocussed = Me.grDitm
    Me.Text = "DETTAGLIO MODULI PER OPERATORE"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grDitm, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvDitm, System.ComponentModel.ISupportInitialize).EndInit()
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

  Public Overridable Sub InitEntity(ByRef cleAnaz As CLE__ANAZ, ByRef ds As DataSet, ByVal strUser As String)
    oCleAnaz = cleAnaz
    AddHandler oCleAnaz.RemoteEvent, AddressOf GestisciEventiEntity

    '-------------------------------------------------
    'leggo dal database i dati e collego il NTSBinding
    oCleAnaz.strUserAperto = strUser
    dsDitm = ds
    oCleAnaz.DitmSetDataTable(DittaCorrente, dsDitm.Tables("ACCDITM"))
    dcDitm.DataSource = dsDitm.Tables("ACCDITM")
    dsDitm.AcceptChanges()
    grDitm.DataSource = dcDitm
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
        tlbDettaglioProgrammi.GlyphPath = (oApp.ChildImageDir & "\open2.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvDitm.NTSSetParam(oMenu, "ACCESSO MODULI PER OPERATORE")
      opdi_modulo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129006945417416063, "Modulo"), "0", 3)
      xx_modulo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129006948791408685, "Descrizione"), 0, True)
      opdi_abilit.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129006945541012977, "Abilitato"), "S", "N")

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
  Public Overridable Sub FRM__DITM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      Me.Text = oApp.Tr(Me, 129006948902661533, "DETTAGLIO MODULI PER OPERATORE ") & oCleAnaz.strUserAperto

      If dsDitm.Tables("ACCDITM").Rows.Count = 0 Then oCleAnaz.DitmCaricaModuli()

      'devo bloccare/sbloccare i campi chiave se necessario
      grvDitm_NTSFocusedRowChanged(grvDitm, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__DITM_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
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
      oCleAnaz.DitmSalva(True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvDitm.NTSRipristinaRigaCorrenteBefore(dcDitm, True) Then Return
      oCleAnaz.DitmRipristina(dcDitm.Position, dcDitm.Filter)
      grvDitm.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbAggiungiModuli_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAggiungiModuli.ItemClick
    Try
      If Not Salva() Then Return

      oCleAnaz.DitmCaricaModuli()

      oApp.MsgBoxInfo(oApp.Tr(Me, 129006949020477049, "Operazione completata"))

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDettaglioProgrammi_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDettaglioProgrammi.ItemClick
    Dim ds As New DataSet
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim frmDitt As FRM__DITT = Nothing
    Try
      frmDitt = CType(NTSNewFormModal("FRM__DITT"), FRM__DITT)

      If Not Salva() Then Return

      If grvDitm.NTSGetCurrentDataRow Is Nothing Then Return

      Me.Cursor = Cursors.WaitCursor
      '-------------------------------
      'clono la tabella perchè devo far vedere solo un subset di righe
      ds.Tables.Clear()
      ds.Tables.Add(oCleAnaz.dsShared.Tables("ACCDITT").Clone())
      ds.Tables(0).TableName = "ACCDITT"
      dtrT = oCleAnaz.dsShared.Tables("ACCDITT").Select("opdi_opnome = " & CStrSQL(grvDitm.NTSGetCurrentDataRow!opdi_opnome.ToString) & _
                                                   " AND xx_modulo = " & NTSCInt(grvDitm.NTSGetCurrentDataRow!opdi_modulo), "opdi_nomprog")
      For i = 0 To dtrT.Length - 1
        ds.Tables("ACCDITT").ImportRow(dtrT(i))
        dtrT(i).Delete()
      Next
      oCleAnaz.dsShared.Tables("ACCDITT").AcceptChanges()

      frmDitt.Init(oMenu, Nothing, DittaCorrente)
      frmDitt.InitEntity(oCleAnaz, ds, grvDitm.NTSGetCurrentDataRow!opdi_opnome.ToString, NTSCInt(grvDitm.NTSGetCurrentDataRow!opdi_modulo))
      frmDitt.ShowDialog()

      '-------------------------------
      'riacquisisco ACCDITT
      For i = 0 To ds.Tables("ACCDITT").Rows.Count - 1
        If ds.Tables("ACCDITT").Rows(i).RowState <> DataRowState.Deleted Then
          'se lo stesso programma/voce di menu è condivisa con altri moduli, devo rimuovere i
          'record delle altre voci, diversamente al salva avrei chiave duplicata, 
          'visto che l'eseguibilità del programma non è per modulo, 
          'ma per l'operatore o è eseguibile oppure no, indipendentemente dal modulo
          For Each dtrT1 As DataRow In oCleAnaz.dsShared.Tables("ACCDITT").Select("codditt = " & CStrSQL(DittaCorrente) & _
                                                                               " AND opdi_opnome = " & CStrSQL(ds.Tables("ACCDITT").Rows(i)!opdi_opnome) & _
                                                                               " AND opdi_nomprog = " & CStrSQL(ds.Tables("ACCDITT").Rows(i)!opdi_nomprog))
            dtrT1.Delete()
          Next
          oCleAnaz.dsShared.Tables("ACCDITT").ImportRow(ds.Tables("ACCDITT").Rows(i))
        End If
      Next
      ds.Tables.Clear()
      oCleAnaz.dsShared.Tables("ACCDITT").AcceptChanges()
      oCleAnaz.bHasChanges = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmDitt Is Nothing Then frmDitt.Dispose()
      frmDitt = Nothing
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
      If NTSCInt(grvDitm.GetFocusedRowCellValue("opdi_modulo")) <> 0 Then
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

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvDitm.NTSSalvaRigaCorrente(dcDitm, oCleAnaz.DitmRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleAnaz.DitmSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleAnaz.DitmRipristina(dcDitm.Position, dcDitm.Filter)
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
