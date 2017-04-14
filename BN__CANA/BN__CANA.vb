Imports NTSInformatica
Imports System.Data
Imports System.Windows.Forms
Imports System.Drawing
Imports NTSInformatica.CLN__STD

Public Class FRM__CANA
#Region "Variabili"
  Public oCleCana As CLE__CANA
  Public oCallParams As CLE__CLDP
  Public dsCana As DataSet
  Public dcCana As New Windows.Forms.BindingSource
#End Region

#Region "Controlli"
  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents grCana As NTSInformatica.NTSGrid
  Public WithEvents grvCana As NTSInformatica.NTSGridView
  Public WithEvents tb_codcana As NTSInformatica.NTSGridColumn
  Public WithEvents tb_descana As NTSInformatica.NTSGridColumn
#End Region

#Region "Inizializzazione"
  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__CANA))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grCana = New NTSInformatica.NTSGrid
    Me.grvCana = New NTSInformatica.NTSGridView
    Me.tb_codcana = New NTSInformatica.NTSGridColumn
    Me.tb_descana = New NTSInformatica.NTSGridColumn
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grCana, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvCana, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.frmAuto.Appearance.BackColor = System.Drawing.SystemColors.GradientActiveCaption
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbCancella, Me.tlbZoom})
    Me.NtsBarManager1.MaxItemId = 20
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbNuovo.GlyphPath = ""
    Me.tlbNuovo.Id = 0
    Me.tlbNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2)
    Me.tlbNuovo.Name = "tlbNuovo"
    Me.tlbNuovo.Visible = True
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.GlyphPath = ""
    Me.tlbSalva.Id = 1
    Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.GlyphPath = ""
    Me.tlbRipristina.Id = 2
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.GlyphPath = ""
    Me.tlbCancella.Id = 18
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.GlyphPath = ""
    Me.tlbZoom.Id = 19
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 15
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.GlyphPath = ""
    Me.tlbImpostaStampante.Id = 16
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 11
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 12
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'grCana
    '
    Me.grCana.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grCana.EmbeddedNavigator.Name = ""
    Me.grCana.Location = New System.Drawing.Point(0, 26)
    Me.grCana.MainView = Me.grvCana
    Me.grCana.Name = "grCana"
    Me.grCana.Size = New System.Drawing.Size(648, 416)
    Me.grCana.TabIndex = 5
    Me.grCana.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvCana})
    '
    'grvCana
    '
    Me.grvCana.ActiveFilterEnabled = False
    Me.grvCana.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tb_codcana, Me.tb_descana})
    Me.grvCana.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvCana.Enabled = True
    Me.grvCana.GridControl = Me.grCana
    Me.grvCana.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvCana.MinRowHeight = 14
    Me.grvCana.Name = "grvCana"
    Me.grvCana.NTSAllowDelete = True
    Me.grvCana.NTSAllowInsert = True
    Me.grvCana.NTSAllowUpdate = True
    Me.grvCana.NTSMenuContext = Nothing
    Me.grvCana.OptionsCustomization.AllowRowSizing = True
    Me.grvCana.OptionsFilter.AllowFilterEditor = False
    Me.grvCana.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvCana.OptionsNavigation.UseTabKey = False
    Me.grvCana.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvCana.OptionsView.ColumnAutoWidth = False
    Me.grvCana.OptionsView.EnableAppearanceEvenRow = True
    Me.grvCana.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvCana.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvCana.OptionsView.ShowGroupPanel = False
    Me.grvCana.RowHeight = 16
    '
    'tb_codcana
    '
    Me.tb_codcana.AppearanceCell.Options.UseBackColor = True
    Me.tb_codcana.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codcana.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codcana.Caption = "Codice"
    Me.tb_codcana.Enabled = True
    Me.tb_codcana.FieldName = "tb_codcana"
    Me.tb_codcana.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codcana.Name = "tb_codcana"
    Me.tb_codcana.NTSRepositoryComboBox = Nothing
    Me.tb_codcana.NTSRepositoryItemCheck = Nothing
    Me.tb_codcana.NTSRepositoryItemMemo = Nothing
    Me.tb_codcana.NTSRepositoryItemText = Nothing
    Me.tb_codcana.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codcana.OptionsFilter.AllowFilter = False
    Me.tb_codcana.Visible = True
    Me.tb_codcana.VisibleIndex = 0
    Me.tb_codcana.Width = 85
    '
    'tb_descana
    '
    Me.tb_descana.AppearanceCell.Options.UseBackColor = True
    Me.tb_descana.AppearanceCell.Options.UseTextOptions = True
    Me.tb_descana.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_descana.Caption = "Descrizione"
    Me.tb_descana.Enabled = True
    Me.tb_descana.FieldName = "tb_descana"
    Me.tb_descana.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_descana.Name = "tb_descana"
    Me.tb_descana.NTSRepositoryComboBox = Nothing
    Me.tb_descana.NTSRepositoryItemCheck = Nothing
    Me.tb_descana.NTSRepositoryItemMemo = Nothing
    Me.tb_descana.NTSRepositoryItemText = Nothing
    Me.tb_descana.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_descana.OptionsFilter.AllowFilter = False
    Me.tb_descana.Visible = True
    Me.tb_descana.VisibleIndex = 1
    Me.tb_descana.Width = 160
    '
    'FRM__CANA
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.grCana)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRM__CANA"
    Me.NTSLastControlFocussed = Me.grCana
    Me.RightToLeftLayout = True
    Me.Text = "CANALI DI VENDITA"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grCana, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvCana, System.ComponentModel.ISupportInitialize).EndInit()
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

    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__CANA", "BE__CANA", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128490898873912000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleCana = CType(oTmp, CLE__CANA)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__CANA", strRemoteServer, strRemotePort)
    AddHandler oCleCana.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleCana.Init(oApp, oScript, oMenu.oCleComm, "TABCANA", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      Try
        tlbNuovo.GlyphPath = oApp.ChildImageDir & "\new.gif"
        tlbSalva.GlyphPath = oApp.ChildImageDir & "\save.gif"
        tlbRipristina.GlyphPath = oApp.ChildImageDir & "\restore.gif"
        tlbCancella.GlyphPath = oApp.ChildImageDir & "\delete.gif"
        tlbZoom.GlyphPath = oApp.ChildImageDir & "\zoom.gif"
        tlbStrumenti.GlyphPath = oApp.ChildImageDir & "\options.gif"
        tlbGuida.GlyphPath = oApp.ChildImageDir & "\help.gif"
        tlbEsci.GlyphPath = oApp.ChildImageDir & "\exit.gif"
      Catch ex As Exception

      End Try
      tlbMain.NTSSetToolTip()

      grvCana.NTSSetParam(oMenu, oApp.Tr(Me, 129138343800717110, "Canali di Vendita"))

      tb_codcana.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128490898873444000, "Codice Canale di Vendita"), "0", 4, 0, 9999)
      tb_descana.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128490898873600000, "Descrizione Canale di Vendita"), 50, True)
      tb_codcana.NTSSetRichiesto()
      tb_descana.NTSSetRichiesto()

      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRM__CANA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      If oCleCana.Apri(DittaCorrente, dsCana) Then
        InitControls()
        dcCana.DataSource = dsCana.Tables("TABCANA")
        dsCana.AcceptChanges()

        grCana.DataSource = dcCana
        GctlSetRoules()
      Else
        Me.Close()
      End If
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRM__CANA_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvCana.NTSNuovo()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvCana.NTSRipristinaRigaCorrenteBefore(dcCana, True) Then Return
      oCleCana.Ripristina(dcCana.Position, dcCana.Filter)
      grvCana.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Try
      If Not grvCana.NTSDeleteRigaCorrente(dcCana, True) Then Return
      oCleCana.Salva(True)
    Catch ex As Exception
      CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      NTSCallStandardZoom()
    Catch ex As Exception
      CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    Try
      oMenu.ReportImposta(Me)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

#End Region

#Region "Eventi Griglia"
  Public Overridable Sub grvCana_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvCana.NTSBeforeRowUpdate
    Try
      If Not Salva() Then
        e.Allow = False
      End If
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub grvCana_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvCana.NTSFocusedRowChanged
    Try
      If NTSCInt(grvCana.GetFocusedRowCellValue(tb_codcana)) <> 0 Then
        tb_codcana.Enabled = False
      Else
        GctlSetVisEnab(tb_codcana, False)
      End If
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Funzioni"
  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()
      Dim dRes As System.Windows.Forms.DialogResult
      dRes = grvCana.NTSSalvaRigaCorrente(dcCana, oCleCana.RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          If GctlControllaOutNotEqual() = False Then
            Return False
          End If

          If Not oCleCana.Salva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          oCleCana.Ripristina(dcCana.Position, dcCana.Filter)
        Case System.Windows.Forms.DialogResult.Cancel
          Return False
      End Select
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
#End Region
End Class
