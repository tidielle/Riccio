#Region "Imports"
Imports System.Data
Imports NTSInformatica.CLN__STD
#End Region

Public Class FRM__GACC

#Region "Moduli"
  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = 0 ' non trovato
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
#End Region ' non trovato  moduli ext

#Region "Variabili"

  Public oCleIstp As CLE__ISTP
  Public nCod As Integer = 0  'codice della stampa a cui si riferiscono gli accessi

  Public dsGacc As DataSet
  Public dcGacc As BindingSource = New BindingSource()

  Public oCallParams As CLE__CLDP

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnGacc As NTSInformatica.NTSPanel
  Public WithEvents grGacc As NTSInformatica.NTSGrid
  Public WithEvents grvGacc As NTSInformatica.NTSGridView
  Public WithEvents pza_ruolo As NTSInformatica.NTSGridColumn
  Public WithEvents pza_nome As NTSInformatica.NTSGridColumn

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
  Public Overridable Sub InitEntity(ByRef CleIstp As CLE__ISTP, ByVal nCodStampa As Integer)
    Try
      Me.oCleIstp = CleIstp
      nCod = nCodStampa
      Me.dsGacc = oCleIstp.dsShared
      AddHandler Me.oCleIstp.RemoteEvent, AddressOf GestisciEventiEntity
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub


  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      'Carico le immagini della Toolbar
      LoadImage()

      tlbMain.NTSSetToolTip()

      'griglia
      grvGacc.NTSSetParam(oMenu, oApp.Tr(Me, 128538720238281250, "GRIGLIA |" & Me.Text & "|"))

      pza_nome.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128538720022187500, "Utente"), 20, False)
      pza_ruolo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130465154063581510, "Ruolo"), 20, False)
      pza_nome.NTSSetParamZoom("ZOOMOPERAT")
      pza_ruolo.NTSSetParamZoom("ZOOMRUOLI")

      'Chiamo lo script per inizializzare i controlli caricati con Source Extender
      NTSScriptExec("InitControls", Me, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub
  Public Overridable Sub LoadImage()
    Try
      tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
    Catch ex As Exception
      'non gestisco l'errore: se non c'è una immagine prendo quella standard
    End Try
    Try
      tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
    Catch ex As Exception
      'non gestisco l'errore: se non c'è una immagine prendo quella standard
    End Try
    Try
      tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
    Catch ex As Exception
      'non gestisco l'errore: se non c'è una immagine prendo quella standard
    End Try
    Try
      tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
    Catch ex As Exception
      'non gestisco l'errore: se non c'è una immagine prendo quella standard
    End Try
    Try
      tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
    Catch ex As Exception
      'Non gestisco l'errore: 
      'se non c'è una immagine prendo quella a design
    End Try
  End Sub


  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__GACC))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnGacc = New NTSInformatica.NTSPanel
    Me.grGacc = New NTSInformatica.NTSGrid
    Me.grvGacc = New NTSInformatica.NTSGridView
    Me.pza_ruolo = New NTSInformatica.NTSGridColumn
    Me.pza_nome = New NTSInformatica.NTSGridColumn
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnGacc, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGacc.SuspendLayout()
    CType(Me.grGacc, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvGacc, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    Me.frmPopup.ClientSize = New System.Drawing.Size(30, 56)
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbRipristina, Me.tlbCancella, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 20
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbRipristina.Id = 3
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.Id = 2
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 14
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnGacc
    '
    Me.pnGacc.AllowDrop = True
    Me.pnGacc.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGacc.Appearance.Options.UseBackColor = True
    Me.pnGacc.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGacc.Controls.Add(Me.grGacc)
    Me.pnGacc.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGacc.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGacc.Location = New System.Drawing.Point(0, 30)
    Me.pnGacc.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGacc.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGacc.Name = "pnGacc"
    Me.pnGacc.NTSActiveTrasparency = True
    Me.pnGacc.Size = New System.Drawing.Size(428, 297)
    Me.pnGacc.TabIndex = 0
    Me.pnGacc.Text = "pnGacc"
    '
    'grGacc
    '
    Me.grGacc.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grGacc.EmbeddedNavigator.Name = ""
    Me.grGacc.Location = New System.Drawing.Point(0, 0)
    Me.grGacc.MainView = Me.grvGacc
    Me.grGacc.Name = "grGacc"
    Me.grGacc.Size = New System.Drawing.Size(428, 297)
    Me.grGacc.TabIndex = 14
    Me.grGacc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvGacc})
    '
    'grvGacc
    '
    Me.grvGacc.ActiveFilterEnabled = False
    '
    'pza_ruolo
    '
    Me.pza_ruolo.AppearanceCell.Options.UseBackColor = True
    Me.pza_ruolo.AppearanceCell.Options.UseTextOptions = True
    Me.pza_ruolo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pza_ruolo.Caption = "Gruppo"
    Me.pza_ruolo.Enabled = True
    Me.pza_ruolo.FieldName = "pza_ruolo"
    Me.pza_ruolo.Name = "pza_ruolo"
    Me.pza_ruolo.NTSRepositoryComboBox = Nothing
    Me.pza_ruolo.NTSRepositoryItemCheck = Nothing
    Me.pza_ruolo.NTSRepositoryItemMemo = Nothing
    Me.pza_ruolo.NTSRepositoryItemText = Nothing
    Me.pza_ruolo.Visible = True
    Me.pza_ruolo.VisibleIndex = 1
    Me.grvGacc.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.pza_nome, Me.pza_ruolo})
    Me.grvGacc.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvGacc.Enabled = True
    Me.grvGacc.GridControl = Me.grGacc
    Me.grvGacc.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvGacc.MinRowHeight = 14
    Me.grvGacc.Name = "grvGacc"
    Me.grvGacc.NTSAllowDelete = True
    Me.grvGacc.NTSAllowInsert = True
    Me.grvGacc.NTSAllowUpdate = True
    Me.grvGacc.NTSMenuContext = Nothing
    Me.grvGacc.OptionsCustomization.AllowRowSizing = True
    Me.grvGacc.OptionsFilter.AllowFilterEditor = False
    Me.grvGacc.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvGacc.OptionsNavigation.UseTabKey = False
    Me.grvGacc.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvGacc.OptionsView.ColumnAutoWidth = False
    Me.grvGacc.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvGacc.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvGacc.OptionsView.ShowGroupPanel = False
    Me.grvGacc.RowHeight = 14
    '
    'pza_nome
    '
    Me.pza_nome.AppearanceCell.Options.UseBackColor = True
    Me.pza_nome.AppearanceCell.Options.UseTextOptions = True
    Me.pza_nome.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pza_nome.Caption = "Utente"
    Me.pza_nome.Enabled = True
    Me.pza_nome.FieldName = "pza_nome"
    Me.pza_nome.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pza_nome.Name = "pza_nome"
    Me.pza_nome.NTSRepositoryComboBox = Nothing
    Me.pza_nome.NTSRepositoryItemCheck = Nothing
    Me.pza_nome.NTSRepositoryItemMemo = Nothing
    Me.pza_nome.NTSRepositoryItemText = Nothing
    Me.pza_nome.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pza_nome.OptionsFilter.AllowFilter = False
    Me.pza_nome.Visible = True
    Me.pza_nome.VisibleIndex = 0
    '
    'FRM__GACC
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(428, 327)
    Me.Controls.Add(Me.pnGacc)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.HelpButton = True
    Me.Name = "FRM__GACC"
    Me.NTSLastControlFocussed = Me.grGacc
    Me.Text = "GESTIONE ACCESSO PER OPERATORE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnGacc, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGacc.ResumeLayout(False)
    CType(Me.grGacc, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvGacc, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

#Region "Form"

  Public Overridable Sub FRM__GACC_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcGacc.Dispose()
      If Not dsGacc Is Nothing Then dsGacc.Dispose()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__GACC_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva(True) Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__GACC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      'Predispongo i controlli
      InitControls()

      If oCleIstp.GetAccessiOperatoreStampa(nCod, dsGacc) Then
        dcGacc.DataSource = dsGacc.Tables("PARSTAGACC")
        dsGacc.AcceptChanges()
        grGacc.DataSource = dcGacc
      Else
        Me.Close()
        Return
      End If

      '-------------------------------------------------
      'Sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

#Region "Toolbar"

  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvGacc.NTSNuovo()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva(False)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Dim strNome As String = ""
    Try
      If Not grvGacc.NTSGetCurrentDataRow Is Nothing Then
        If grvGacc.NTSDeleteRigaCorrente(dcGacc, True) Then
          oCleIstp.SaveAccessiOperatoreStampa(nCod, True)
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If grvGacc.NTSRipristinaRigaCorrenteBefore(dcGacc, CBool(IIf(sender Is Nothing, False, True))) Then
        dsGacc.RejectChanges()
        grvGacc.NTSRipristinaRigaCorrenteAfter()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

#Region "Eventi Griglia"
  Public Overridable Sub grvGacc_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvGacc.NTSFocusedRowChanged
    Try
      If grvGacc.FocusedRowHandle = -999999 Then Return

      If Not grvGacc.FocusedRowHandle = -999998 Then
        Select Case grvGacc.GetDataRow(grvGacc.FocusedRowHandle).RowState
          Case DataRowState.Added
            If Not pza_nome.Enabled Then GctlSetVisEnab(pza_nome, False)
            If Not pza_ruolo.Enabled Then GctlSetVisEnab(pza_ruolo, False)
          Case Else
            pza_nome.Enabled = False
            pza_ruolo.Enabled = False
        End Select
      Else
        If Not pza_nome.Enabled Then GctlSetVisEnab(pza_nome, False)
        If Not pza_ruolo.Enabled Then GctlSetVisEnab(pza_ruolo, False)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvGacc_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvGacc.NTSBeforeRowUpdate
    Try
      If Not Salva(False) Then
        'rimango sulla riga su cui sono
        e.Allow = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Function Salva(ByVal bEsci As Boolean) As Boolean
    Try
      Me.ValidaLastControl()

      If GctlControllaOutNotEqual() = False Then Return False
      If Not oCleIstp.SaveAccessiOperatoreStampa(nCod, False) Then
        Return False
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

End Class
