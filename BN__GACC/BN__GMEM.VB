#Region "Imports"
Imports System.Data
Imports NTSInformatica.CLN__STD
#End Region

Public Class FRM__GMEM

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

  Public oCleGacc As CLE__GACC

  Public dsGmem As DataSet
  Public dcGmem As BindingSource = New BindingSource()

  Public oCallParams As CLE__CLDP
  Public strNomeProgramma, strTabellaModale As String

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
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnGmen As NTSInformatica.NTSPanel
  Public WithEvents grGmem As NTSInformatica.NTSGrid
  Public WithEvents grvGmem As NTSInformatica.NTSGridView
  Public WithEvents Colonna0 As NTSInformatica.NTSGridColumn
  Public WithEvents Colonna1 As NTSInformatica.NTSGridColumn
  Public WithEvents Colonna2 As NTSInformatica.NTSGridColumn

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
  Public Overridable Sub InitEntity(ByRef CleGacc As CLE__GACC, ByVal strNomeProgramma As String, ByVal strTabellaModale As String)
    Try
      Me.oCleGacc = CleGacc
      Me.dsGmem = oCleGacc.dsShared

      Me.strNomeProgramma = strNomeProgramma
      Me.strTabellaModale = strTabellaModale
      AddHandler Me.oCleGacc.RemoteEvent, AddressOf GestisciEventiEntity
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
      grvGmem.NTSSetParam(oMenu, oApp.Tr(Me, 128538720238281250, "GRIGLIA |" & Me.Text & "|"))

      Select Case strTabellaModale
        Case "ACCOPEU"
          Colonna1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128538720022187500, "Nome utente"), 20, True)
          Colonna1.NTSSetParamZoom("ZOOMOPERAT")
        Case "ACCOPER"
          Colonna1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128533454371875000, "Gruppo Operatori"), "0", 3, -1, 999)
          Colonna1.NTSSetParamZoom("ZOOMORUOLIPERAT")
      End Select
      Colonna0.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128538719806406250, "Nome Programma"), 30, False)
      Colonna2.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128533454403906250, "Accesso consentito"), "S", "N")
      Colonna1.NTSSetRichiesto()


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
      tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
    Catch ex As Exception
      'Non gestisco l'errore: 
      'se non c'è una immagine prendo quella a design
    End Try
    Try
      tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
    Catch ex As Exception
      'Non gestisco l'errore: 
      'se non c'è una immagine prendo quella a design
    End Try
  End Sub


  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__GMEM))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnGmen = New NTSInformatica.NTSPanel
    Me.grGmem = New NTSInformatica.NTSGrid
    Me.grvGmem = New NTSInformatica.NTSGridView
    Me.Colonna0 = New NTSInformatica.NTSGridColumn
    Me.Colonna1 = New NTSInformatica.NTSGridColumn
    Me.Colonna2 = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnGmen, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGmen.SuspendLayout()
    CType(Me.grGmem, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvGmem, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbRipristina, Me.tlbCancella, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 20
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 13
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 14
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnGmen
    '
    Me.pnGmen.AllowDrop = True
    Me.pnGmen.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGmen.Appearance.Options.UseBackColor = True
    Me.pnGmen.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGmen.Controls.Add(Me.grGmem)
    Me.pnGmen.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGmen.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGmen.Location = New System.Drawing.Point(0, 30)
    Me.pnGmen.Name = "pnGmen"
    Me.pnGmen.Size = New System.Drawing.Size(428, 297)
    Me.pnGmen.TabIndex = 0
    Me.pnGmen.Text = "pnGmem"
    '
    'grGmem
    '
    Me.grGmem.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grGmem.EmbeddedNavigator.Name = ""
    Me.grGmem.Location = New System.Drawing.Point(0, 0)
    Me.grGmem.MainView = Me.grvGmem
    Me.grGmem.Name = "grGmem"
    Me.grGmem.Size = New System.Drawing.Size(428, 297)
    Me.grGmem.TabIndex = 14
    Me.grGmem.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvGmem})
    '
    'grvGmem
    '
    Me.grvGmem.ActiveFilterEnabled = False
    Me.grvGmem.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.Colonna0, Me.Colonna1, Me.Colonna2})
    Me.grvGmem.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvGmem.Enabled = True
    Me.grvGmem.GridControl = Me.grGmem
    Me.grvGmem.Name = "grvGmem"
    Me.grvGmem.NTSAllowDelete = True
    Me.grvGmem.NTSAllowInsert = True
    Me.grvGmem.NTSAllowUpdate = True
    Me.grvGmem.NTSMenuContext = Nothing
    Me.grvGmem.OptionsCustomization.AllowRowSizing = True
    Me.grvGmem.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvGmem.OptionsNavigation.UseTabKey = False
    Me.grvGmem.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvGmem.OptionsView.ColumnAutoWidth = False
    Me.grvGmem.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvGmem.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvGmem.OptionsView.ShowGroupPanel = False
    Me.grvGmem.RowHeight = 14
    '
    'Colonna0
    '
    Me.Colonna0.AppearanceCell.Options.UseBackColor = True
    Me.Colonna0.AppearanceCell.Options.UseTextOptions = True
    Me.Colonna0.Caption = "Colonna0"
    Me.Colonna0.Enabled = False
    Me.Colonna0.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.Colonna0.Name = "Colonna0"
    Me.Colonna0.NTSRepositoryComboBox = Nothing
    Me.Colonna0.NTSRepositoryItemCheck = Nothing
    Me.Colonna0.NTSRepositoryItemText = Nothing
    Me.Colonna0.OptionsColumn.AllowEdit = False
    Me.Colonna0.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.Colonna0.OptionsColumn.ReadOnly = True
    Me.Colonna0.OptionsFilter.AllowFilter = False
    '
    'Colonna1
    '
    Me.Colonna1.AppearanceCell.Options.UseBackColor = True
    Me.Colonna1.AppearanceCell.Options.UseTextOptions = True
    Me.Colonna1.Caption = "Colonna1"
    Me.Colonna1.Enabled = True
    Me.Colonna1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.Colonna1.Name = "Colonna1"
    Me.Colonna1.NTSRepositoryComboBox = Nothing
    Me.Colonna1.NTSRepositoryItemCheck = Nothing
    Me.Colonna1.NTSRepositoryItemText = Nothing
    Me.Colonna1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.Colonna1.OptionsFilter.AllowFilter = False
    Me.Colonna1.Visible = True
    Me.Colonna1.VisibleIndex = 0
    '
    'Colonna2
    '
    Me.Colonna2.AppearanceCell.Options.UseBackColor = True
    Me.Colonna2.AppearanceCell.Options.UseTextOptions = True
    Me.Colonna2.Caption = "Colonna2"
    Me.Colonna2.Enabled = True
    Me.Colonna2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.Colonna2.Name = "Colonna2"
    Me.Colonna2.NTSRepositoryComboBox = Nothing
    Me.Colonna2.NTSRepositoryItemCheck = Nothing
    Me.Colonna2.NTSRepositoryItemText = Nothing
    Me.Colonna2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.Colonna2.OptionsFilter.AllowFilter = False
    Me.Colonna2.Visible = True
    Me.Colonna2.VisibleIndex = 1
    '
    'FRM__GMEM
    '
    Me.ClientSize = New System.Drawing.Size(428, 327)
    Me.Controls.Add(Me.pnGmen)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.HelpButton = True
    Me.Name = "FRM__GMEM"
    Me.NTSLastControlFocussed = Me.grGmem
    Me.Text = "GESTIONE"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnGmen, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGmen.ResumeLayout(False)
    CType(Me.grGmem, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvGmem, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

#Region "Form"

  Public Overridable Sub FRM__GMEN_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcGmem.Dispose()
      If Not dsGmem Is Nothing Then dsGmem.Dispose()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__GMEN_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva(True) Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__GMEN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try

      grvGmem.Name = "grv" & strTabellaModale

      Select Case strTabellaModale
        Case "ACCOPEU"
          Me.Text = oApp.Tr(Me, 128533556201406250, "GESTIONE ACCESSO PER OPERATORE")

          Me.Colonna0.Caption = "Nome Programma"
          Me.Colonna0.Name = "mnou_nomprog"

          Me.Colonna1.Caption = "Nome utente"
          Me.Colonna1.Name = "mnou_opnome"

          Me.Colonna2.Caption = "Accesso consentito"
          Me.Colonna2.Name = "mnou_abilit"

          GctlTipoDoc = "P"

        Case "ACCOPER"
          Me.Text = oApp.Tr(Me, 128533556261562500, "GESTIONE ACCESSO PER GRUPPO OPERATORI / RUOLI")

          Me.Colonna0.Caption = "Nome Programma"
          Me.Colonna0.Name = "mnop_nomprog"

          Me.Colonna1.Caption = "Gruppo Operatori"
          Me.Colonna1.Name = "mnop_gruppo"

          Me.Colonna2.Caption = "Accesso consentito"
          Me.Colonna2.Name = "mnop_abilit"

          GctlTipoDoc = "R"

      End Select


      'Predispongo i controlli
      InitControls()

      If oCleGacc.ApriModale(dsGmem, strTabellaModale, strNomeProgramma) Then
        dcGmem.DataSource = dsGmem.Tables(strTabellaModale)
        dsGmem.AcceptChanges()

        grGmem.DataSource = dcGmem

      Else
        Me.Close()
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
      grvGmem.NTSNuovo()

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
    Try

      If Not grvGmem.NTSGetCurrentDataRow Is Nothing Then
        If grvGmem.NTSDeleteRigaCorrente(dcGmem, True) Then
          oCleGacc.SalvaModale(True, strTabellaModale)
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
      If grvGmem.NTSRipristinaRigaCorrenteBefore(dcGmem, CBool(IIf(sender Is Nothing, False, True))) Then
        oCleGacc.RipristinaModale(dcGmem.Position, dcGmem.Filter, strTabellaModale)
        grvGmem.NTSRipristinaRigaCorrenteAfter()
      End If
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
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

#Region "Eventi Griglia"
  Public Overridable Sub grvGmenm_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvGmem.NTSFocusedRowChanged
    Try
      GrigliaAbilitaDisabilitaColonne(grvGmem, Colonna1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub GrigliaAbilitaDisabilitaColonne(ByRef grvRef As NTSGridView, ByRef gcRef As NTSGridColumn)
    Try
      If grvRef.FocusedRowHandle = -999999 Then Return

      If Not grvRef.FocusedRowHandle = -999998 Then
        Select Case grvRef.GetDataRow(grvRef.FocusedRowHandle).RowState
          Case DataRowState.Added
            If Not gcRef.Enabled Then GctlSetVisEnab(gcRef, False)
          Case Else
            gcRef.Enabled = False
        End Select
      Else
        If Not gcRef.Enabled Then GctlSetVisEnab(gcRef, False)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grv_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvGmem.NTSBeforeRowUpdate
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
    Dim dRes As DialogResult
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      dRes = grvGmem.NTSSalvaRigaCorrente(dcGmem, oCleGacc.RecordIsChangedModale, False)

      If bEsci Then
        If dRes = Windows.Forms.DialogResult.Yes Then
          If grvGmem.NTSGetCurrentDataRow.RowState = DataRowState.Added Then
            dRes = oApp.MsgBoxInfoYesNoCancel_DefYes(oApp.Tr(Me, 128511037688750000, "Salvare il nuovo record inserito prima di uscire?"))
          Else
            dRes = oApp.MsgBoxInfoYesNoCancel_DefYes(oApp.Tr(Me, 128511039167656250, "Salvare le modifiche apportate al record prima di uscire?"))
          End If

        End If
      End If

      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleGacc.SalvaModale(False, strTabellaModale) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          tlbRipristina_ItemClick(Nothing, Nothing)
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
