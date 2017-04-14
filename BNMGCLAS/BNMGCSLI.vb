Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGCSLI
#Region "Variabili"
  Public oCleClas As CLEMGCLAS
  Public oCallParams As CLE__CLDP
  Public dttCsli As DataTable = Nothing
  Public dcCsli As BindingSource = New BindingSource()
#End Region

#Region "Controls"
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
  Public WithEvents grCsli As NTSInformatica.NTSGrid
  Public WithEvents grvCsli As NTSInformatica.NTSGridView
  Public WithEvents acx_codvalu As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codvalu As NTSInformatica.NTSGridColumn
  Public WithEvents acx_descla As NTSInformatica.NTSGridColumn
#End Region

#Region "Init"
  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGCSLI))
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
    Me.grCsli = New NTSInformatica.NTSGrid
    Me.grvCsli = New NTSInformatica.NTSGridView
    Me.acx_codvalu = New NTSInformatica.NTSGridColumn
    Me.xx_codvalu = New NTSInformatica.NTSGridColumn
    Me.acx_descla = New NTSInformatica.NTSGridColumn
    Me.acx_note = New NTSInformatica.NTSGridColumn
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grCsli, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvCsli, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.tlbCancella.Id = 3
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.GlyphPath = ""
    Me.tlbZoom.Id = 13
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
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
    'grCsli
    '
    Me.grCsli.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grCsli.EmbeddedNavigator.Name = ""
    Me.grCsli.Location = New System.Drawing.Point(0, 30)
    Me.grCsli.MainView = Me.grvCsli
    Me.grCsli.Name = "grCsli"
    Me.grCsli.Size = New System.Drawing.Size(648, 412)
    Me.grCsli.TabIndex = 5
    Me.grCsli.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvCsli})
    '
    'grvCsli
    '
    Me.grvCsli.ActiveFilterEnabled = False
    Me.grvCsli.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.acx_codvalu, Me.xx_codvalu, Me.acx_descla, Me.acx_note})
    Me.grvCsli.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvCsli.Enabled = True
    Me.grvCsli.GridControl = Me.grCsli
    Me.grvCsli.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvCsli.MinRowHeight = 14
    Me.grvCsli.Name = "grvCsli"
    Me.grvCsli.NTSAllowDelete = True
    Me.grvCsli.NTSAllowInsert = True
    Me.grvCsli.NTSAllowUpdate = True
    Me.grvCsli.NTSMenuContext = Nothing
    Me.grvCsli.OptionsCustomization.AllowRowSizing = True
    Me.grvCsli.OptionsFilter.AllowFilterEditor = False
    Me.grvCsli.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvCsli.OptionsNavigation.UseTabKey = False
    Me.grvCsli.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvCsli.OptionsView.ColumnAutoWidth = False
    Me.grvCsli.OptionsView.EnableAppearanceEvenRow = True
    Me.grvCsli.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvCsli.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvCsli.OptionsView.ShowGroupPanel = False
    Me.grvCsli.RowHeight = 16
    '
    'acx_codvalu
    '
    Me.acx_codvalu.AppearanceCell.Options.UseBackColor = True
    Me.acx_codvalu.AppearanceCell.Options.UseTextOptions = True
    Me.acx_codvalu.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.acx_codvalu.Caption = "Cod. lingua"
    Me.acx_codvalu.Enabled = True
    Me.acx_codvalu.FieldName = "acx_codvalu"
    Me.acx_codvalu.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.acx_codvalu.Name = "acx_codvalu"
    Me.acx_codvalu.NTSRepositoryComboBox = Nothing
    Me.acx_codvalu.NTSRepositoryItemCheck = Nothing
    Me.acx_codvalu.NTSRepositoryItemMemo = Nothing
    Me.acx_codvalu.NTSRepositoryItemText = Nothing
    Me.acx_codvalu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.acx_codvalu.OptionsFilter.AllowFilter = False
    Me.acx_codvalu.Visible = True
    Me.acx_codvalu.VisibleIndex = 0
    Me.acx_codvalu.Width = 70
    '
    'xx_codvalu
    '
    Me.xx_codvalu.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
    Me.xx_codvalu.AppearanceCell.Options.UseBackColor = True
    Me.xx_codvalu.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codvalu.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codvalu.Caption = "Desc. lingua"
    Me.xx_codvalu.Enabled = False
    Me.xx_codvalu.FieldName = "xx_codvalu"
    Me.xx_codvalu.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codvalu.Name = "xx_codvalu"
    Me.xx_codvalu.NTSRepositoryComboBox = Nothing
    Me.xx_codvalu.NTSRepositoryItemCheck = Nothing
    Me.xx_codvalu.NTSRepositoryItemMemo = Nothing
    Me.xx_codvalu.NTSRepositoryItemText = Nothing
    Me.xx_codvalu.OptionsColumn.AllowEdit = False
    Me.xx_codvalu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codvalu.OptionsColumn.ReadOnly = True
    Me.xx_codvalu.OptionsFilter.AllowFilter = False
    Me.xx_codvalu.Visible = True
    Me.xx_codvalu.VisibleIndex = 1
    Me.xx_codvalu.Width = 78
    '
    'acx_descla
    '
    Me.acx_descla.AppearanceCell.Options.UseBackColor = True
    Me.acx_descla.AppearanceCell.Options.UseTextOptions = True
    Me.acx_descla.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.acx_descla.Caption = "Descr. classificazione"
    Me.acx_descla.Enabled = True
    Me.acx_descla.FieldName = "acx_descla"
    Me.acx_descla.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.acx_descla.Name = "acx_descla"
    Me.acx_descla.NTSRepositoryComboBox = Nothing
    Me.acx_descla.NTSRepositoryItemCheck = Nothing
    Me.acx_descla.NTSRepositoryItemMemo = Nothing
    Me.acx_descla.NTSRepositoryItemText = Nothing
    Me.acx_descla.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.acx_descla.OptionsFilter.AllowFilter = False
    Me.acx_descla.Visible = True
    Me.acx_descla.VisibleIndex = 2
    Me.acx_descla.Width = 177
    '
    'acx_note
    '
    Me.acx_note.AppearanceCell.Options.UseBackColor = True
    Me.acx_note.AppearanceCell.Options.UseTextOptions = True
    Me.acx_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.acx_note.Caption = "Note"
    Me.acx_note.Enabled = True
    Me.acx_note.FieldName = "acx_note"
    Me.acx_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.acx_note.Name = "acx_note"
    Me.acx_note.NTSRepositoryComboBox = Nothing
    Me.acx_note.NTSRepositoryItemCheck = Nothing
    Me.acx_note.NTSRepositoryItemMemo = Nothing
    Me.acx_note.NTSRepositoryItemText = Nothing
    Me.acx_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.acx_note.OptionsFilter.AllowFilter = False
    Me.acx_note.Visible = True
    Me.acx_note.VisibleIndex = 3
    Me.acx_note.Width = 260
    '
    'FRMMGCSLI
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.grCsli)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMMGCSLI"
    Me.NTSLastControlFocussed = Me.grCsli
    Me.Text = "DESCRIZIONI IN LINGUA"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grCsli, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvCsli, System.ComponentModel.ISupportInitialize).EndInit()
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

  Public Overridable Sub InitEntity(ByRef oCleClas As CLEMGCLAS)
    Me.oCleClas = oCleClas
    AddHandler oCleClas.RemoteEvent, AddressOf GestisciEventiEntity
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

      grvCsli.NTSSetParam(oMenu, "DESCRIZIONI IN LINGUA")

      acx_codvalu.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128399361441014000, "Cod. lingua"), tabling)
      xx_codvalu.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128399366052182000, "Descrizione lingua"), 0)
      acx_descla.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128399366425646000, "Descrizione classificazione in lingua"), 50, False)
      acx_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128399366425646001, "Note in lingua"), 0, True, True)

      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub
#End Region

#Region "Eventi Form"
  Public Overridable Sub FRMMGCSLI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      InitControls()

      If oCleClas.ApriGrigliaLingue(dttCsli) Then
        dcCsli.DataSource = dttCsli
        grCsli.DataSource = dcCsli
        GctlSetRoules()
      Else
        Me.Close()
      End If
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMMGCSLI_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub FRMMGCSLI_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcCsli.Dispose()
      dttCsli.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvCsli.NTSNuovo()
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

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Dim dtrCurrent As DataRow = Nothing
    Dim nLingua As Integer = 0
    Try
      dtrCurrent = grvCsli.NTSGetCurrentDataRow
      If dtrCurrent IsNot Nothing Then
        nLingua = NTSCInt(dtrCurrent!acx_codvalu)
        If grvCsli.NTSDeleteRigaCorrente(dcCsli, True) Then
          oCleClas.CancellaDescrizioneInLingua(nLingua)
        End If
      End If
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      Ripristina()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      NTSCallStandardZoom()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub
#End Region

#Region "Eventi Grid"
  Public Overridable Sub grvCsli_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvCsli.NTSBeforeRowUpdate
    Try
      If Not Salva() Then
        e.Allow = False
      End If
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Functions"
  Public Overridable Function Ripristina() As Boolean
    Dim dtrCurrent As DataRow = Nothing
    Try
      dtrCurrent = grvCsli.NTSGetCurrentDataRow
      If dtrCurrent IsNot Nothing Then
        If grvCsli.NTSRipristinaRigaCorrenteBefore(dcCsli, True) Then
          dtrCurrent.RejectChanges()
          grvCsli.NTSRipristinaRigaCorrenteAfter()
        End If
      End If
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function Salva() As Boolean
    Dim dRes As DialogResult = Nothing
    Dim dtrCurrent As DataRow = Nothing
    Dim strMess As String = ""
    Try
      dtrCurrent = grvCsli.NTSGetCurrentDataRow
      If dtrCurrent IsNot Nothing Then
        If dtrCurrent.RowState = DataRowState.Added OrElse dtrCurrent.RowState = DataRowState.Modified Then
          Me.ValidaLastControl()
          dRes = grvCsli.NTSSalvaRigaCorrente(dcCsli, True, False)
          Select Case dRes
            Case System.Windows.Forms.DialogResult.Yes
              If GctlControllaOutNotEqual() = False Then Return False

              If Not oCleClas.SalvaDescrizioneInLingua(dtrCurrent, strMess) Then
                If strMess.Length <> 0 Then
                  oApp.MsgBoxErr(strMess)
                End If
                Return False
              End If
            Case System.Windows.Forms.DialogResult.No
              'ripristino
              Ripristina()
            Case System.Windows.Forms.DialogResult.Cancel
              'non posso fare nulla
              Return False
            Case System.Windows.Forms.DialogResult.Abort
              'la riga non ha subito modifiche
          End Select
        End If
      End If

      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
#End Region
End Class
