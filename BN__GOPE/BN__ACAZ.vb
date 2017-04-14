Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__ACAZ

#Region "Moduli"
  Private Moduli_P As Integer = CLN__STD.bsModAll
  Private ModuliExt_P As Integer = 0
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
#End Region

#Region "Variabili"
  Public oCleGope As CLE__GOPE
  Public dsAcaz As DataSet
  Public dcAcaz As BindingSource = New BindingSource
  Public oCallParams As CLE__CLDP
  Private components As System.ComponentModel.IComponent

  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar

  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl


  Public WithEvents grAcaz As NTSInformatica.NTSGrid
  Public WithEvents grvAcaz As NTSInformatica.NTSGridView

  Public WithEvents opaz_codazi As NTSInformatica.NTSGridColumn
  Public WithEvents xx_azdescr As NTSInformatica.NTSGridColumn
  Public WithEvents opaz_opnome As NTSInformatica.NTSGridColumn
  Public WithEvents opaz_gruppo As NTSInformatica.NTSGridColumn
  Public WithEvents opaz_abilit As NTSInformatica.NTSGridColumn

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
    Me.GctlTipoDoc = ""

    InitializeComponent()
    Me.MinimumSize = Me.Size

    Return True
  End Function

  Public Overridable Sub InitEntity(ByRef CleGope As CLE__GOPE)
    oCleGope = CleGope
    AddHandler oCleGope.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__ACAZ))
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
    Me.grAcaz = New NTSInformatica.NTSGrid
    Me.grvAcaz = New NTSInformatica.NTSGridView
    Me.opaz_codazi = New NTSInformatica.NTSGridColumn
    Me.xx_azdescr = New NTSInformatica.NTSGridColumn
    Me.opaz_opnome = New NTSInformatica.NTSGridColumn
    Me.opaz_gruppo = New NTSInformatica.NTSGridColumn
    Me.opaz_abilit = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grAcaz, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvAcaz, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbRipristina, Me.tlbCancella, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 13
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbSalva.Id = 2
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
    Me.tlbCancella.Id = 4
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
    Me.tlbEsci.Id = 11
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'grAcaz
    '
    Me.grAcaz.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grAcaz.EmbeddedNavigator.Name = ""
    Me.grAcaz.Location = New System.Drawing.Point(0, 30)
    Me.grAcaz.MainView = Me.grvAcaz
    Me.grAcaz.Name = "grAcaz"
    Me.grAcaz.Size = New System.Drawing.Size(493, 279)
    Me.grAcaz.TabIndex = 4
    Me.grAcaz.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvAcaz})
    '
    'grvAcaz
    '
    Me.grvAcaz.ActiveFilterEnabled = False
    Me.grvAcaz.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.opaz_codazi, Me.xx_azdescr, Me.opaz_opnome, Me.opaz_gruppo, Me.opaz_abilit})
    Me.grvAcaz.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvAcaz.Enabled = True
    Me.grvAcaz.GridControl = Me.grAcaz
    Me.grvAcaz.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvAcaz.Name = "grvAcaz"
    Me.grvAcaz.NTSAllowDelete = True
    Me.grvAcaz.NTSAllowInsert = True
    Me.grvAcaz.NTSAllowUpdate = True
    Me.grvAcaz.NTSMenuContext = Nothing
    Me.grvAcaz.OptionsCustomization.AllowRowSizing = True
    Me.grvAcaz.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvAcaz.OptionsNavigation.UseTabKey = False
    Me.grvAcaz.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvAcaz.OptionsView.ColumnAutoWidth = False
    Me.grvAcaz.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvAcaz.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvAcaz.OptionsView.ShowGroupPanel = False
    Me.grvAcaz.RowHeight = 14
    '
    'opaz_codazi
    '
    Me.opaz_codazi.AppearanceCell.Options.UseBackColor = True
    Me.opaz_codazi.AppearanceCell.Options.UseTextOptions = True
    Me.opaz_codazi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.opaz_codazi.Caption = "Codice azienda disabilitata"
    Me.opaz_codazi.Enabled = True
    Me.opaz_codazi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.opaz_codazi.Name = "opaz_codazi"
    Me.opaz_codazi.NTSRepositoryComboBox = Nothing
    Me.opaz_codazi.NTSRepositoryItemCheck = Nothing
    Me.opaz_codazi.NTSRepositoryItemMemo = Nothing
    Me.opaz_codazi.NTSRepositoryItemText = Nothing
    Me.opaz_codazi.OptionsColumn.AllowEdit = False
    Me.opaz_codazi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.opaz_codazi.OptionsFilter.AllowFilter = False
    Me.opaz_codazi.Visible = True
    Me.opaz_codazi.VisibleIndex = 0
    Me.opaz_codazi.Width = 139
    '
    'xx_azdescr
    '
    Me.xx_azdescr.AppearanceCell.Options.UseBackColor = True
    Me.xx_azdescr.AppearanceCell.Options.UseTextOptions = True
    Me.xx_azdescr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_azdescr.Caption = "Descrizione"
    Me.xx_azdescr.Enabled = False
    Me.xx_azdescr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_azdescr.Name = "xx_azdescr"
    Me.xx_azdescr.NTSRepositoryComboBox = Nothing
    Me.xx_azdescr.NTSRepositoryItemCheck = Nothing
    Me.xx_azdescr.NTSRepositoryItemMemo = Nothing
    Me.xx_azdescr.NTSRepositoryItemText = Nothing
    Me.xx_azdescr.OptionsColumn.AllowEdit = False
    Me.xx_azdescr.OptionsColumn.AllowFocus = False
    Me.xx_azdescr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_azdescr.OptionsColumn.ReadOnly = True
    Me.xx_azdescr.OptionsFilter.AllowFilter = False
    Me.xx_azdescr.Visible = True
    Me.xx_azdescr.VisibleIndex = 1
    Me.xx_azdescr.Width = 187
    '
    'opaz_opnome
    '
    Me.opaz_opnome.AppearanceCell.Options.UseBackColor = True
    Me.opaz_opnome.AppearanceCell.Options.UseTextOptions = True
    Me.opaz_opnome.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.opaz_opnome.Caption = "opaz_opnome"
    Me.opaz_opnome.Enabled = False
    Me.opaz_opnome.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.opaz_opnome.Name = "opaz_opnome"
    Me.opaz_opnome.NTSRepositoryComboBox = Nothing
    Me.opaz_opnome.NTSRepositoryItemCheck = Nothing
    Me.opaz_opnome.NTSRepositoryItemMemo = Nothing
    Me.opaz_opnome.NTSRepositoryItemText = Nothing
    Me.opaz_opnome.OptionsColumn.AllowEdit = False
    Me.opaz_opnome.OptionsColumn.AllowFocus = False
    Me.opaz_opnome.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.opaz_opnome.OptionsColumn.ReadOnly = True
    Me.opaz_opnome.OptionsFilter.AllowFilter = False
    Me.opaz_opnome.Width = 187
    '
    'opaz_gruppo
    '
    Me.opaz_gruppo.AppearanceCell.Options.UseBackColor = True
    Me.opaz_gruppo.AppearanceCell.Options.UseTextOptions = True
    Me.opaz_gruppo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.opaz_gruppo.Caption = "opaz_gruppo"
    Me.opaz_gruppo.Enabled = False
    Me.opaz_gruppo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.opaz_gruppo.Name = "opaz_gruppo"
    Me.opaz_gruppo.NTSRepositoryComboBox = Nothing
    Me.opaz_gruppo.NTSRepositoryItemCheck = Nothing
    Me.opaz_gruppo.NTSRepositoryItemMemo = Nothing
    Me.opaz_gruppo.NTSRepositoryItemText = Nothing
    Me.opaz_gruppo.OptionsColumn.AllowEdit = False
    Me.opaz_gruppo.OptionsColumn.AllowFocus = False
    Me.opaz_gruppo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.opaz_gruppo.OptionsColumn.ReadOnly = True
    Me.opaz_gruppo.OptionsFilter.AllowFilter = False
    Me.opaz_gruppo.Width = 187
    '
    'opaz_abilit
    '
    Me.opaz_abilit.AppearanceCell.Options.UseBackColor = True
    Me.opaz_abilit.AppearanceCell.Options.UseTextOptions = True
    Me.opaz_abilit.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.opaz_abilit.Caption = "opaz_abilit"
    Me.opaz_abilit.Enabled = False
    Me.opaz_abilit.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.opaz_abilit.Name = "opaz_abilit"
    Me.opaz_abilit.NTSRepositoryComboBox = Nothing
    Me.opaz_abilit.NTSRepositoryItemCheck = Nothing
    Me.opaz_abilit.NTSRepositoryItemMemo = Nothing
    Me.opaz_abilit.NTSRepositoryItemText = Nothing
    Me.opaz_abilit.OptionsColumn.AllowEdit = False
    Me.opaz_abilit.OptionsColumn.AllowFocus = False
    Me.opaz_abilit.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.opaz_abilit.OptionsColumn.ReadOnly = True
    Me.opaz_abilit.OptionsFilter.AllowFilter = False
    Me.opaz_abilit.Width = 187
    '
    'FRM__ACAZ
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(493, 309)
    Me.Controls.Add(Me.grAcaz)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.MinimizeBox = False
    Me.Name = "FRM__ACAZ"
    Me.Text = "AZIENDE DISABILITATE OPERATORE"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grAcaz, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvAcaz, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")

      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvAcaz.NTSSetParam(oMenu, oApp.Tr(Me, 128650933982812500, "Aziende disabilitate operatore"))
      opaz_codazi.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128780553531911567, "Codice azienda disabilitata"), 25, True)
      xx_azdescr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128650933983125000, "Descrizione"), 0, True)
      opaz_opnome.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128780553567748327, "opaz_opnome"), 12, True)
      opaz_gruppo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128780553553569435, "opaz_gruppo"), 12, True)
      opaz_abilit.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128780553588627135, "opaz_abilit"), 12, True)

      opaz_codazi.NTSSetParamZoom("ZOOMAZIENDE")

      NTSScriptExec("InitControls", Me, Nothing)

    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

#Region "Eventi Form"

  Public Overridable Sub FRM__ACAZ_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      Me.Text = oApp.Tr(Me, 128681224444687500, "Aziende disabilitate - Operatore - ") & oCleGope.strNomeOp

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleGope.AcazApri(DittaCorrente, dsAcaz) Then Me.Close()
      dcAcaz.DataSource = dsAcaz.Tables("ACCAZI")
      dsAcaz.AcceptChanges()

      grAcaz.DataSource = dcAcaz

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__ACAZ_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then Return
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__ACAZ_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcAcaz.Dispose()
      dsAcaz.Dispose()
    Catch
    End Try
  End Sub

#End Region

#Region "Toolbar"

  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvAcaz.NTSNuovo()
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
      If Not grvAcaz.NTSDeleteRigaCorrente(dcAcaz, True) Then Return
      oCleGope.AcazSalva(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvAcaz.NTSRipristinaRigaCorrenteBefore(dcAcaz, True) Then Return
      oCleGope.AcazRipristina(dcAcaz.Position, dcAcaz.Filter)
      grvAcaz.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub

#End Region

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvAcaz.NTSSalvaRigaCorrente(dcAcaz, oCleGope.AcazRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes

          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleGope.AcazSalva(False) Then
            Return False
          End If

        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleGope.AcazRipristina(dcAcaz.Position, dcAcaz.Filter)
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

  Public Overridable Sub grvAcaz_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvAcaz.NTSBeforeRowUpdate
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

  Public Overridable Sub grvAcaz_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvAcaz.NTSFocusedRowChanged
    Try
      If NTSCStr(grvAcaz.GetFocusedRowCellValue(opaz_codazi).ToString.Trim) <> "" Then
        opaz_codazi.Enabled = False
      Else
        GctlSetVisEnab(opaz_codazi, False)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class