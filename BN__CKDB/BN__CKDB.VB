Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__CKDB
  Private Moduli_P As Integer = CLN__STD.bsModAll
  Private ModuliExt_P As Integer = CLN__STD.bsModExtAll
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

#Region "Variabili"
  Public oCleCkdb As CLE__CKDB
  Public oCallParams As CLE__CLDP
  Public dsCkdb As DataSet
  Public dcCkdb As BindingSource = New BindingSource

  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbElabora As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents ckMostraQuery As NTSInformatica.NTSCheckBox
  Public WithEvents grCkdb As NTSInformatica.NTSGrid
  Public WithEvents grvCkdb As NTSInformatica.NTSGridView
  Public WithEvents xx_seleziona As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descrizione As NTSInformatica.NTSGridColumn
  Public WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
  Public WithEvents NtsPanel1 As NTSInformatica.NTSPanel
  Public WithEvents pnCkdbGriglia As NTSInformatica.NTSPanel

  Private components As System.ComponentModel.IContainer
#End Region

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__CKDB))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbElabora = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.ckMostraQuery = New NTSInformatica.NTSCheckBox
    Me.grCkdb = New NTSInformatica.NTSGrid
    Me.grvCkdb = New NTSInformatica.NTSGridView
    Me.xx_seleziona = New NTSInformatica.NTSGridColumn
    Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Me.xx_descrizione = New NTSInformatica.NTSGridColumn
    Me.NtsPanel1 = New NTSInformatica.NTSPanel
    Me.pnCkdbGriglia = New NTSInformatica.NTSPanel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckMostraQuery.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grCkdb, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvCkdb, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel1.SuspendLayout()
    CType(Me.pnCkdbGriglia, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCkdbGriglia.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbElabora, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 3
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbElabora), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbElabora
    '
    Me.tlbElabora.Caption = "Elabora"
    Me.tlbElabora.Glyph = CType(resources.GetObject("tlbElabora.Glyph"), System.Drawing.Image)
    Me.tlbElabora.GlyphPath = ""
    Me.tlbElabora.Id = 0
    Me.tlbElabora.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbElabora.Name = "tlbElabora"
    Me.tlbElabora.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 1
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 2
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'ckMostraQuery
    '
    Me.ckMostraQuery.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckMostraQuery.Location = New System.Drawing.Point(12, 6)
    Me.ckMostraQuery.Name = "ckMostraQuery"
    Me.ckMostraQuery.NTSCheckValue = "S"
    Me.ckMostraQuery.NTSUnCheckValue = "N"
    Me.ckMostraQuery.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckMostraQuery.Properties.Appearance.Options.UseBackColor = True
    Me.ckMostraQuery.Properties.AutoHeight = False
    Me.ckMostraQuery.Properties.Caption = "Mostra query eseguite nel file di LOG"
    Me.ckMostraQuery.Size = New System.Drawing.Size(203, 19)
    Me.ckMostraQuery.TabIndex = 4
    '
    'grCkdb
    '
    Me.grCkdb.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grCkdb.EmbeddedNavigator.Name = ""
    Me.grCkdb.Location = New System.Drawing.Point(0, 0)
    Me.grCkdb.MainView = Me.grvCkdb
    Me.grCkdb.Name = "grCkdb"
    Me.grCkdb.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
    Me.grCkdb.Size = New System.Drawing.Size(782, 404)
    Me.grCkdb.TabIndex = 5
    Me.grCkdb.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvCkdb})
    '
    'grvCkdb
    '
    Me.grvCkdb.ActiveFilterEnabled = False
    Me.grvCkdb.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_seleziona, Me.xx_descrizione})
    Me.grvCkdb.CustomizationFormBounds = New System.Drawing.Rectangle(592, 326, 208, 170)
    Me.grvCkdb.Enabled = True
    Me.grvCkdb.GridControl = Me.grCkdb
    Me.grvCkdb.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvCkdb.MinRowHeight = 14
    Me.grvCkdb.Name = "grvCkdb"
    Me.grvCkdb.NTSAllowDelete = True
    Me.grvCkdb.NTSAllowInsert = True
    Me.grvCkdb.NTSAllowUpdate = True
    Me.grvCkdb.NTSMenuContext = Nothing
    Me.grvCkdb.OptionsCustomization.AllowRowSizing = True
    Me.grvCkdb.OptionsFilter.AllowFilterEditor = False
    Me.grvCkdb.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvCkdb.OptionsNavigation.UseTabKey = False
    Me.grvCkdb.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvCkdb.OptionsView.ColumnAutoWidth = False
    Me.grvCkdb.OptionsView.EnableAppearanceEvenRow = True
    Me.grvCkdb.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvCkdb.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvCkdb.OptionsView.ShowGroupPanel = False
    Me.grvCkdb.RowHeight = 14
    '
    'xx_seleziona
    '
    Me.xx_seleziona.AppearanceCell.Options.UseBackColor = True
    Me.xx_seleziona.AppearanceCell.Options.UseTextOptions = True
    Me.xx_seleziona.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_seleziona.Caption = "Seleziona"
    Me.xx_seleziona.ColumnEdit = Me.RepositoryItemCheckEdit1
    Me.xx_seleziona.Enabled = True
    Me.xx_seleziona.FieldName = "xx_seleziona"
    Me.xx_seleziona.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_seleziona.Name = "xx_seleziona"
    Me.xx_seleziona.NTSRepositoryComboBox = Nothing
    Me.xx_seleziona.NTSRepositoryItemCheck = Nothing
    Me.xx_seleziona.NTSRepositoryItemMemo = Nothing
    Me.xx_seleziona.NTSRepositoryItemText = Nothing
    Me.xx_seleziona.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_seleziona.OptionsFilter.AllowFilter = False
    Me.xx_seleziona.Visible = True
    Me.xx_seleziona.VisibleIndex = 0
    Me.xx_seleziona.Width = 58
    '
    'RepositoryItemCheckEdit1
    '
    Me.RepositoryItemCheckEdit1.AutoHeight = False
    Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
    '
    'xx_descrizione
    '
    Me.xx_descrizione.AppearanceCell.Options.UseBackColor = True
    Me.xx_descrizione.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descrizione.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descrizione.Caption = "Descrizione"
    Me.xx_descrizione.Enabled = True
    Me.xx_descrizione.FieldName = "xx_descrizione"
    Me.xx_descrizione.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descrizione.Name = "xx_descrizione"
    Me.xx_descrizione.NTSRepositoryComboBox = Nothing
    Me.xx_descrizione.NTSRepositoryItemCheck = Nothing
    Me.xx_descrizione.NTSRepositoryItemMemo = Nothing
    Me.xx_descrizione.NTSRepositoryItemText = Nothing
    Me.xx_descrizione.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descrizione.OptionsFilter.AllowFilter = False
    Me.xx_descrizione.Visible = True
    Me.xx_descrizione.VisibleIndex = 1
    Me.xx_descrizione.Width = 686
    '
    'NtsPanel1
    '
    Me.NtsPanel1.AllowDrop = True
    Me.NtsPanel1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsPanel1.Appearance.Options.UseBackColor = True
    Me.NtsPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel1.Controls.Add(Me.ckMostraQuery)
    Me.NtsPanel1.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsPanel1.Dock = System.Windows.Forms.DockStyle.Top
    Me.NtsPanel1.Location = New System.Drawing.Point(0, 30)
    Me.NtsPanel1.Name = "NtsPanel1"
    Me.NtsPanel1.NTSActiveTrasparency = True
    Me.NtsPanel1.Size = New System.Drawing.Size(782, 32)
    Me.NtsPanel1.TabIndex = 6
    Me.NtsPanel1.Text = "NtsPanel1"
    '
    'pnCkdbGriglia
    '
    Me.pnCkdbGriglia.AllowDrop = True
    Me.pnCkdbGriglia.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCkdbGriglia.Appearance.Options.UseBackColor = True
    Me.pnCkdbGriglia.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCkdbGriglia.Controls.Add(Me.grCkdb)
    Me.pnCkdbGriglia.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCkdbGriglia.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnCkdbGriglia.Location = New System.Drawing.Point(0, 62)
    Me.pnCkdbGriglia.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnCkdbGriglia.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnCkdbGriglia.Name = "pnCkdbGriglia"
    Me.pnCkdbGriglia.NTSActiveTrasparency = True
    Me.pnCkdbGriglia.Size = New System.Drawing.Size(782, 404)
    Me.pnCkdbGriglia.TabIndex = 7
    Me.pnCkdbGriglia.Text = "NtsPanel2"
    '
    'FRM__CKDB
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(782, 466)
    Me.Controls.Add(Me.pnCkdbGriglia)
    Me.Controls.Add(Me.NtsPanel1)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRM__CKDB"
    Me.NTSLastControlFocussed = Me.grCkdb
    Me.Text = "CONTROLLO COERENZA DATI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckMostraQuery.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grCkdb, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvCkdb, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel1.ResumeLayout(False)
    Me.NtsPanel1.PerformLayout()
    CType(Me.pnCkdbGriglia, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCkdbGriglia.ResumeLayout(False)
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

    '------------------------------------------------
    'creo e attivo l'entity e inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__CKDB", "BE__CKDB", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128269970690085455, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleCkdb = CType(oTmp, CLE__CKDB)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__CKDB", strRemoteServer, strRemotePort)
    AddHandler oCleCkdb.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleCkdb.Init(oApp, NTSScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    '------------------------------------------------

    Return True
  End Function
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbElabora.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()
      '-------------------------------------------------

      grvCkdb.NTSSetParam(oMenu, oApp.Tr(Me, 128381201849656359, "Controllo coerenza dati"))
      xx_seleziona.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129086457182070312, "Seleziona"), "S", "N")
      xx_descrizione.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128381275577890770, "Descrizione"), 0, True)
      xx_descrizione.Width = 686
      ckMostraQuery.NTSSetParam(oMenu, oApp.Tr(Me, 128381201860274083, "Mostra query eseguite nel file di LOG"), "S", "N")

      ''non faccio lanciare lo zoom dal controllo visto che non devo cambiare il contenuto del campo ma riposizionare il dataset!!!

      xx_descrizione.Enabled = False
      grvCkdb.NTSAllowDelete = False
      grvCkdb.NTSAllowUpdate = False
      grvCkdb.NTSAllowInsert = False

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

#Region "EventiForm"

  Public Overridable Sub FRM__CKDB_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      Apri()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      'e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipodocumento, ecc ...
      'GctlTipoDoc = ""
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRM__CKDB_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcCkdb.Dispose()
      dsCkdb.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "EventiToolbar"

  Public Overridable Sub tlbElabora_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbElabora.ItemClick
    Dim strFile As String = ""
    Try
      Me.ValidaLastControl()

      strFile = "Bs--ckdb_" & Format(Year(Now), "0000") & Format(Month(Now), "00") & Format(DateAndTime.Day(Now), "00") & Format(Hour(Now), "00") & Format(Minute(Now), "00") & Format(Second(Now), "00")
      'Parte il log
      If Not oCleCkdb.LogStart(strFile, "Controllo coerenza dati") Then Exit Sub

      oCleCkdb.EseguiControlli(ckMostraQuery.Checked)
      oCleCkdb.LogStop()

      'Mostra il log
      NTSProcessStart("notepad", oCleCkdb.LogFileName)

    Catch ex As Exception
      oCleCkdb.LogStop()
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
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

  Public Overridable Function Apri() As Boolean
    Try
      dsCkdb = New DataSet

      oCleCkdb.Apri(DittaCorrente, dsCkdb)

      dcCkdb.DataSource = dsCkdb.Tables("TABELLA")
      grCkdb.DataSource = dcCkdb

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

End Class
