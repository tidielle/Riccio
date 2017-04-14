Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMCIALIN
  Public oCleSotc As CLE__SOTC
  Public oCallParams As CLE__CLDP
  Public dsAlin As DataSet
  Public dcAlin As BindingSource = New BindingSource()

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents grAlin As NTSInformatica.NTSGrid
  Public WithEvents grvAlin As NTSInformatica.NTSGridView
  Public WithEvents anl_acconto As NTSInformatica.NTSGridColumn
  Public WithEvents xxx_acconto As NTSInformatica.NTSGridColumn

  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMCIALIN))
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
    Me.grAlin = New NTSInformatica.NTSGrid
    Me.grvAlin = New NTSInformatica.NTSGridView
    Me.anl_acconto = New NTSInformatica.NTSGridColumn
    Me.xxx_acconto = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grAlin, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvAlin, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(231, Byte), Integer))
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom})
    Me.NtsBarManager1.MaxItemId = 17
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'grAlin
    '
    Me.grAlin.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grAlin.EmbeddedNavigator.Name = ""
    Me.grAlin.Location = New System.Drawing.Point(0, 26)
    Me.grAlin.MainView = Me.grvAlin
    Me.grAlin.Name = "grAlin"
    Me.grAlin.Size = New System.Drawing.Size(648, 416)
    Me.grAlin.TabIndex = 5
    Me.grAlin.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvAlin})
    '
    'grvAlin
    '
    Me.grvAlin.ActiveFilterEnabled = False
    Me.grvAlin.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.anl_acconto, Me.xxx_acconto})
    Me.grvAlin.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvAlin.Enabled = True
    Me.grvAlin.GridControl = Me.grAlin
    Me.grvAlin.Name = "grvAlin"
    Me.grvAlin.NTSAllowDelete = True
    Me.grvAlin.NTSAllowInsert = True
    Me.grvAlin.NTSAllowUpdate = True
    Me.grvAlin.NTSMenuContext = Nothing
    Me.grvAlin.OptionsCustomization.AllowRowSizing = True
    Me.grvAlin.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvAlin.OptionsNavigation.UseTabKey = False
    Me.grvAlin.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvAlin.OptionsView.ColumnAutoWidth = False
    Me.grvAlin.OptionsView.EnableAppearanceEvenRow = True
    Me.grvAlin.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvAlin.OptionsView.ShowGroupPanel = False
    Me.grvAlin.RowHeight = 16
    '
    'anl_acconto
    '
    Me.anl_acconto.AppearanceCell.Options.UseBackColor = True
    Me.anl_acconto.AppearanceCell.Options.UseTextOptions = True
    Me.anl_acconto.Caption = "Conto di CA"
    Me.anl_acconto.Enabled = True
    Me.anl_acconto.FieldName = "anl_acconto"
    Me.anl_acconto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.anl_acconto.Name = "anl_acconto"
    Me.anl_acconto.NTSRepositoryComboBox = Nothing
    Me.anl_acconto.NTSRepositoryItemCheck = Nothing
    Me.anl_acconto.NTSRepositoryItemText = Nothing
    Me.anl_acconto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.anl_acconto.OptionsFilter.AllowFilter = False
    Me.anl_acconto.Visible = True
    Me.anl_acconto.VisibleIndex = 0
    Me.anl_acconto.Width = 70
    '
    'xxx_acconto
    '
    Me.xxx_acconto.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
    Me.xxx_acconto.AppearanceCell.Options.UseBackColor = True
    Me.xxx_acconto.AppearanceCell.Options.UseTextOptions = True
    Me.xxx_acconto.Caption = "Desc. conto CA"
    Me.xxx_acconto.Enabled = False
    Me.xxx_acconto.FieldName = "xxx_acconto"
    Me.xxx_acconto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xxx_acconto.Name = "xxx_acconto"
    Me.xxx_acconto.NTSRepositoryComboBox = Nothing
    Me.xxx_acconto.NTSRepositoryItemCheck = Nothing
    Me.xxx_acconto.NTSRepositoryItemText = Nothing
    Me.xxx_acconto.OptionsColumn.AllowEdit = False
    Me.xxx_acconto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xxx_acconto.OptionsColumn.ReadOnly = True
    Me.xxx_acconto.OptionsFilter.AllowFilter = False
    Me.xxx_acconto.Visible = True
    Me.xxx_acconto.VisibleIndex = 1
    Me.xxx_acconto.Width = 70
    '
    'FRMCIALIN
    '
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.grAlin)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMCIALIN"
    Me.NTSLastControlFocussed = Me.grAlin
    Me.Text = "COLLEGAMENTI PDC DI CA"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grAlin, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvAlin, System.ComponentModel.ISupportInitialize).EndInit()
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

  Public overridable Sub InitEntity(ByRef cleSotc As CLE__SOTC)
    oCleSotc = cleSotc 
    AddHandler oCleSotc.RemoteEvent, AddressOf GestisciEventiEntity
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

      grvAlin.NTSSetParam(oMenu, "COLLEGAMENTI PDC DI CA")

      anl_acconto.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128327799621570000, "Conto di CA"), tabanagca)
      xxx_acconto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128327803086050000, "Descrizione conto CA"), 0)

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
  Public overridable Sub FRMCIALIN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleSotc.AlinApri(DittaCorrente, dsAlin) Then Me.Close()
      dcAlin.DataSource = dsAlin.Tables("ANALINK")
      dsAlin.AcceptChanges()

      grAlin.DataSource = dcAlin

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMCIALIN_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public overridable Sub FRMCIALIN_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcAlin.Dispose()
      dsAlin.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvAlin.NTSNuovo()

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
      If Not grvAlin.NTSDeleteRigaCorrente(dcAlin, True) Then Return
      oCleSotc.AlinSalva(True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvAlin.NTSRipristinaRigaCorrenteBefore(dcAlin, True) Then Return
      oCleSotc.AlinRipristina(dcAlin.Position, dcAlin.Filter)
      grvAlin.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      'zoom standard
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      '------------------------------------
      'zoom standard di textbox e griglia
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
    If Not Salva() Then Return
    Me.Close()
  End Sub

#End Region

  Public Overridable Sub grvAlin_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvAlin.NTSBeforeRowUpdate
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


  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvAlin.NTSSalvaRigaCorrente(dcAlin, oCleSotc.AlinRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleSotc.AlinSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleSotc.AlinRipristina(dcAlin.Position, dcAlin.Filter)
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
