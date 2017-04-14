Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__TRIC
  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = bsModExtAll
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

  Public oCleTric As CLE__TRIC
  Public oCallParams As CLE__CLDP
  Public dsTric As DataSet
  Public dcTric As BindingSource = New BindingSource()

  Private components As System.ComponentModel.IContainer


  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__TRIC))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbDuplicaPdc = New NTSInformatica.NTSBarMenuItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grTric = New NTSInformatica.NTSGrid
    Me.grvTric = New NTSInformatica.NTSGridView
    Me.tb_codtric = New NTSInformatica.NTSGridColumn
    Me.tb_destric = New NTSInformatica.NTSGridColumn
    Me.tb_xslname = New NTSInformatica.NTSGridColumn
    Me.tlbDuplica = New NTSInformatica.NTSBarMenuItem
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grTric, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvTric, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbDuplicaPdc, Me.tlbDuplica})
    Me.NtsBarManager1.MaxItemId = 19
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 15
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDuplica), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDuplicaPdc), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbDuplicaPdc
    '
    Me.tlbDuplicaPdc.Caption = "Duplica su PDC"
    Me.tlbDuplicaPdc.Id = 17
    Me.tlbDuplicaPdc.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F2))
    Me.tlbDuplicaPdc.Name = "tlbDuplicaPdc"
    Me.tlbDuplicaPdc.NTSIsCheckBox = False
    Me.tlbDuplicaPdc.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.Id = 16
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 4
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 5
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
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
    'grTric
    '
    Me.grTric.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grTric.EmbeddedNavigator.Name = ""
    Me.grTric.Location = New System.Drawing.Point(0, 26)
    Me.grTric.MainView = Me.grvTric
    Me.grTric.Name = "grTric"
    Me.grTric.Size = New System.Drawing.Size(648, 416)
    Me.grTric.TabIndex = 5
    Me.grTric.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvTric})
    '
    'grvTric
    '
    Me.grvTric.ActiveFilterEnabled = False
    Me.grvTric.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tb_codtric, Me.tb_destric, Me.tb_xslname})
    Me.grvTric.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvTric.Enabled = True
    Me.grvTric.GridControl = Me.grTric
    Me.grvTric.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvTric.Name = "grvTric"
    Me.grvTric.NTSAllowDelete = True
    Me.grvTric.NTSAllowInsert = True
    Me.grvTric.NTSAllowUpdate = True
    Me.grvTric.NTSMenuContext = Nothing
    Me.grvTric.OptionsCustomization.AllowRowSizing = True
    Me.grvTric.OptionsFilter.AllowFilterEditor = False
    Me.grvTric.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvTric.OptionsNavigation.UseTabKey = False
    Me.grvTric.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvTric.OptionsView.ColumnAutoWidth = False
    Me.grvTric.OptionsView.EnableAppearanceEvenRow = True
    Me.grvTric.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvTric.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvTric.OptionsView.ShowGroupPanel = False
    Me.grvTric.RowHeight = 16
    '
    'tb_codtric
    '
    Me.tb_codtric.AppearanceCell.Options.UseBackColor = True
    Me.tb_codtric.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codtric.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codtric.Caption = "Codice"
    Me.tb_codtric.Enabled = True
    Me.tb_codtric.FieldName = "tb_codtric"
    Me.tb_codtric.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codtric.Name = "tb_codtric"
    Me.tb_codtric.NTSRepositoryComboBox = Nothing
    Me.tb_codtric.NTSRepositoryItemCheck = Nothing
    Me.tb_codtric.NTSRepositoryItemMemo = Nothing
    Me.tb_codtric.NTSRepositoryItemText = Nothing
    Me.tb_codtric.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codtric.OptionsFilter.AllowFilter = False
    Me.tb_codtric.Visible = True
    Me.tb_codtric.VisibleIndex = 0
    Me.tb_codtric.Width = 70
    '
    'tb_destric
    '
    Me.tb_destric.AppearanceCell.Options.UseBackColor = True
    Me.tb_destric.AppearanceCell.Options.UseTextOptions = True
    Me.tb_destric.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_destric.Caption = "Descrizione"
    Me.tb_destric.Enabled = True
    Me.tb_destric.FieldName = "tb_destric"
    Me.tb_destric.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_destric.Name = "tb_destric"
    Me.tb_destric.NTSRepositoryComboBox = Nothing
    Me.tb_destric.NTSRepositoryItemCheck = Nothing
    Me.tb_destric.NTSRepositoryItemMemo = Nothing
    Me.tb_destric.NTSRepositoryItemText = Nothing
    Me.tb_destric.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_destric.OptionsFilter.AllowFilter = False
    Me.tb_destric.Visible = True
    Me.tb_destric.VisibleIndex = 1
    Me.tb_destric.Width = 70
    '
    'tb_xslname
    '
    Me.tb_xslname.AppearanceCell.Options.UseBackColor = True
    Me.tb_xslname.AppearanceCell.Options.UseTextOptions = True
    Me.tb_xslname.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_xslname.Caption = "File Excel"
    Me.tb_xslname.Enabled = True
    Me.tb_xslname.FieldName = "tb_xslname"
    Me.tb_xslname.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_xslname.Name = "tb_xslname"
    Me.tb_xslname.NTSRepositoryComboBox = Nothing
    Me.tb_xslname.NTSRepositoryItemCheck = Nothing
    Me.tb_xslname.NTSRepositoryItemMemo = Nothing
    Me.tb_xslname.NTSRepositoryItemText = Nothing
    Me.tb_xslname.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_xslname.OptionsFilter.AllowFilter = False
    Me.tb_xslname.Visible = True
    Me.tb_xslname.VisibleIndex = 2
    Me.tb_xslname.Width = 70
    '
    'tlbDuplica
    '
    Me.tlbDuplica.Caption = "Duplica voce di riclassificazione"
    Me.tlbDuplica.Id = 18
    Me.tlbDuplica.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F2))
    Me.tlbDuplica.Name = "tlbDuplica"
    Me.tlbDuplica.NTSIsCheckBox = False
    Me.tlbDuplica.Visible = True
    '
    'FRM__TRIC
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.grTric)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.HelpButton = True
    Me.Name = "FRM__TRIC"
    Me.NTSLastControlFocussed = Me.grTric
    Me.Text = "TIPI RICLASSIFICAZIONE"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grTric, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvTric, System.ComponentModel.ISupportInitialize).EndInit()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__TRIC", "BE__TRIC", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128269891339862000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleTric = CType(oTmp, CLE__TRIC)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__TRIC", strRemoteServer, strRemotePort)
    AddHandler oCleTric.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleTric.Init(oApp, oScript, oMenu.oCleComm, "TABTRIC", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

      Return True
  End Function

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
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvTric.NTSSetParam(oMenu, "TIPI RICLASSIFICAZIONE")
      tb_codtric.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128269891339282000, "Codice"), tabtric)
      tb_destric.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128269891339822000, "Descrizione"), 60, False)
      tb_xslname.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128269891339832000, "File Excel"), 50, False)

      tb_codtric.NTSSetParamZoom("")     'in questo modo non faccio apparire lo zoom  (lo zoom è stato abilitato da NTSSetParamTabe)

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
  Public Overridable Sub FRM__TRIC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim i As Integer
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleTric.Apri(DittaCorrente, dsTric) Then Me.Close()
      dcTric.DataSource = dsTric.Tables("TABTRIC")
      dsTric.AcceptChanges()

      grTric.DataSource = dcTric

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      '--------------------------------------------
      'sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
      If Not oCallParams Is Nothing Then
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
          If grvTric.NTSAllowInsert Then
            grvTric.NTSNuovo()
          End If
        ElseIf Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) <> "" Then
          For i = 0 To dcTric.List.Count - 1
            If CType(dcTric.Item(i), DataRowView)!tb_codtric.ToString = Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) Then
              dcTric.Position = i
              Exit For
            End If
          Next
        End If
      End If  'If Not oCallParams Is Nothing Then

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__TRIC_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub FRM__TRIC_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcTric.Dispose()
      dsTric.Dispose()
    Catch
    End Try
  End Sub
#End Region

  Public Overridable Sub grvTric_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvTric.NTSBeforeRowUpdate
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
      dRes = grvTric.NTSSalvaRigaCorrente(dcTric, oCleTric.RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleTric.Salva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleTric.Ripristina(dcTric.Position, dcTric.Filter)
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

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer

    Try
      '--------------------------------------------------
      'preparo il motore di stampa
      strCrpe = ""
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BS--TRIC", "Reports1", " ", 0, nDestin, "BS--TRIC.RPT", False, "TIPI RICLASSIFICAZIONE", False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce già il multireport)
      For i = LBound(CType(nPjob, Array), 2) To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvTric.NTSNuovo()

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
    Dim bEliminaTabelleCorrelate As Boolean = False
    Dim bAnatricAnptric As Boolean = False
    Dim bAsk As Boolean = True
    Dim nCodtric As Integer = 0

    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleTric.TestPreDelete(CType(dcTric.Current, DataRowView).Row) Then Return
      '--------------------------------------------------------------------------------------------------------------
      nCodtric = NTSCInt(grvTric.NTSGetCurrentDataRow!tb_codtric)
      '--------------------------------------------------------------------------------------------------------------
      If oCleTric.CheckEsistenzaTabelleCorrelate(NTSCInt(grvTric.NTSGetCurrentDataRow!tb_codtric), _
        bAnatricAnptric) = True Then
        If bAnatricAnptric = False Then
          If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 130476668621112985, "Attenzione!" & vbCrLf & _
            "Eliminando il Tipo Riclassificazione, saranno eliminati tutti i dati correlati." & vbCrLf & _
            "Proseguire?")) = Windows.Forms.DialogResult.No Then
            Return
          Else
            bAsk = False
          End If
        Else
          If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 130477219971599549, "Attenzione!" & vbCrLf & _
            "Eliminando il Tipo Riclassificazione, saranno eliminati tutti i dati correlati," & vbCrLf & _
            "compresi i collegamenti con il Piano dei Conti." & vbCrLf & _
            "Proseguire?")) = Windows.Forms.DialogResult.No Then
            Return
          Else
            bAsk = False
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not grvTric.NTSDeleteRigaCorrente(dcTric, bAsk) Then Return
      '--------------------------------------------------------------------------------------------------------------
      If oCleTric.Salva(True) = True Then oCleTric.SvuotaTabelleCorrelate(nCodtric)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvTric.NTSRipristinaRigaCorrenteBefore(dcTric, True) Then Return
      oCleTric.Ripristina(dcTric.Position, dcTric.Filter)
      grvTric.NTSRipristinaRigaCorrenteAfter()
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

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      If Not Salva() Then Return
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      If Not Salva() Then Return
      Stampa(0)
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

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub

  Public Overridable Sub tlbDuplica_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDuplica.ItemClick
    Dim frmDtri As FRM__DTRI = Nothing
    Dim nOrigine As Integer = 0
    Try
      '--------------------------------------------------------------------------------------------------------------
      If Salva() = False Then Return
      If dcTric.List.Count = 0 Then Return
      If grvTric.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130553331597319898, "Posizionarsi sul record da duplicare"))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      oCleTric.nOldCodtric = NTSCInt(grvTric.NTSGetCurrentDataRow!tb_codtric)
      oCleTric.strOldDestric = NTSCStr(grvTric.NTSGetCurrentDataRow!tb_destric)
      '--------------------------------------------------------------------------------------------------------------
      frmDtri = CType(NTSNewFormModal("FRM__DTRI"), FRM__DTRI)
      frmDtri.Init(oMenu, Nothing, DittaCorrente)
      frmDtri.InitEntity(oCleTric)
      frmDtri.ShowDialog()
      '--------------------------------------------------------------------------------------------------------------
      If oCleTric.bAnnullato = True Then Return
      '--------------------------------------------------------------------------------------------------------------
      'oCleTric.bCreaAnatricAnptric = False
      'If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129793981485687926, _
      '  "Creare anche i collegamenti con i Piani dei Conti?")) = Windows.Forms.DialogResult.Yes Then
      '  oCleTric.bCreaAnatricAnptric = True
      'End If
      oCleTric.bCreaAnatricAnptric = True 'se lo si imposta = false fa solo la creazione di TABTRIC ... che si puÃ² fare tranquillamente a mano
      '--------------------------------------------------------------------------------------------------------------
      If frmDtri.opCopia1.Checked Then nOrigine = 0
      If frmDtri.opCopia2.Checked Then nOrigine = 1
      If frmDtri.opCopia3.Checked Then nOrigine = 2
      If oCleTric.Duplica(nOrigine, frmDtri.edPdc.Text) = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleTric.Apri(DittaCorrente, dsTric) Then
        Me.Close()
        Return
      End If
      dcTric.DataSource = dsTric.Tables("TABTRIC")
      dsTric.AcceptChanges()
      grTric.DataSource = dcTric
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      If Not frmDtri Is Nothing Then frmDtri.Dispose()
      frmDtri = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbDuplicaPdc_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDuplicaPdc.ItemClick
    Dim strPdcModel As String = ""
    Dim strPdcNew As String = ""
    Try
      If grvTric.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130553331383162085, "Posizionarsi sul record da duplicare"))
        Return
      End If
      If oCleTric.bAllowOperation = False Then
        If NTSCInt(grvTric.NTSGetCurrentDataRow!tb_codtric) >= 1 And NTSCInt(grvTric.NTSGetCurrentDataRow!tb_codtric) <= 500 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128269945822692000, "E' consentita la duplicazione su PDC solo per quei codici NON compresi fra 1 e 500."))
          Return
        End If
      End If

      strPdcModel = oApp.InputBoxNew(oApp.Tr(Me, 128269948107832000, "Riclassificato '|" & grvTric.NTSGetCurrentDataRow!tb_codtric.ToString & "|': Indicare il codice PDC di partenza"))
      If strPdcModel.Trim = "" Then Return
      strPdcNew = oApp.InputBoxNew(oApp.Tr(Me, 129000749217007695, "Riclassificato '|" & grvTric.NTSGetCurrentDataRow!tb_codtric.ToString & "|': Indicare il codice PDC di destinazione"))
      If strPdcNew.Trim = "" Then Return

      Me.Cursor = Cursors.WaitCursor
      oCleTric.DuplicaPdc(NTSCInt(grvTric.NTSGetCurrentDataRow!tb_codtric), strPdcModel, strPdcNew)
      Me.Cursor = Cursors.Default
      oApp.MsgBoxInfo(oApp.Tr(Me, 128269963527432000, "Elaborazione completata"))

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
#End Region
End Class
