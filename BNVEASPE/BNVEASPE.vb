Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVEASPE

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
  Public oCleAspe As CLEVEASPE
  Public dsAspe As DataSet
  Public dcAspe As BindingSource = New BindingSource()
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
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnCiva As NTSInformatica.NTSPanel
  Public WithEvents grAspe As NTSInformatica.NTSGrid
  Public WithEvents grvAspe As NTSInformatica.NTSGridView
  Public WithEvents tb_codaspe As NTSInformatica.NTSGridColumn
  Public WithEvents tb_desaspe As NTSInformatica.NTSGridColumn

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

    '------------------------------------------------
    'Creo e attivo l'entity, in più inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY stesso
    Dim strErr As String = ""
    Dim objTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNVEASPE", "BEVEASPE", objTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128182574024687500, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleAspe = CType(objTmp, CLEVEASPE)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNVEASPE", strRemoteServer, strRemotePort)
    AddHandler oCleAspe.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleAspe.Init(oApp, NTSScript, oMenu.oCleComm, "TABASPE", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
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
    Me.pnCiva = New NTSInformatica.NTSPanel
    Me.grAspe = New NTSInformatica.NTSGrid
    Me.grvAspe = New NTSInformatica.NTSGridView
    Me.tb_codaspe = New NTSInformatica.NTSGridColumn
    Me.tb_desaspe = New NTSInformatica.NTSGridColumn
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnCiva, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCiva.SuspendLayout()
    CType(Me.grAspe, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvAspe, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 16
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
    Me.tlbNuovo.GlyphPath = ""
    Me.tlbNuovo.Id = 0
    Me.tlbNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2)
    Me.tlbNuovo.Name = "tlbNuovo"
    Me.tlbNuovo.Visible = True
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.GlyphPath = ""
    Me.tlbSalva.Id = 1
    Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.GlyphPath = ""
    Me.tlbRipristina.Id = 3
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.GlyphPath = ""
    Me.tlbCancella.Id = 2
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.GlyphPath = ""
    Me.tlbZoom.Id = 4
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 9
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta stampante"
    Me.tlbImpostaStampante.GlyphPath = ""
    Me.tlbImpostaStampante.Id = 10
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 13
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 14
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnCiva
    '
    Me.pnCiva.AllowDrop = True
    Me.pnCiva.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCiva.Appearance.Options.UseBackColor = True
    Me.pnCiva.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCiva.Controls.Add(Me.grAspe)
    Me.pnCiva.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCiva.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnCiva.Location = New System.Drawing.Point(0, 24)
    Me.pnCiva.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnCiva.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnCiva.Name = "pnCiva"
    Me.pnCiva.NTSActiveTrasparency = True
    Me.pnCiva.Size = New System.Drawing.Size(688, 380)
    Me.pnCiva.TabIndex = 0
    Me.pnCiva.Text = "pnCiva"
    '
    'grAspe
    '
    Me.grAspe.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grAspe.EmbeddedNavigator.Name = ""
    Me.grAspe.Location = New System.Drawing.Point(0, 0)
    Me.grAspe.MainView = Me.grvAspe
    Me.grAspe.Name = "grAspe"
    Me.grAspe.Size = New System.Drawing.Size(688, 380)
    Me.grAspe.TabIndex = 1
    Me.grAspe.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvAspe})
    '
    'grvAspe
    '
    Me.grvAspe.ActiveFilterEnabled = False
    Me.grvAspe.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tb_codaspe, Me.tb_desaspe})
    Me.grvAspe.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvAspe.Enabled = True
    Me.grvAspe.GridControl = Me.grAspe
    Me.grvAspe.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvAspe.MinRowHeight = 14
    Me.grvAspe.Name = "grvAspe"
    Me.grvAspe.NTSAllowDelete = True
    Me.grvAspe.NTSAllowInsert = True
    Me.grvAspe.NTSAllowUpdate = True
    Me.grvAspe.NTSMenuContext = Nothing
    Me.grvAspe.OptionsCustomization.AllowRowSizing = True
    Me.grvAspe.OptionsFilter.AllowFilterEditor = False
    Me.grvAspe.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvAspe.OptionsNavigation.UseTabKey = False
    Me.grvAspe.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvAspe.OptionsView.ColumnAutoWidth = False
    Me.grvAspe.OptionsView.EnableAppearanceEvenRow = True
    Me.grvAspe.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvAspe.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvAspe.OptionsView.ShowGroupPanel = False
    Me.grvAspe.RowHeight = 16
    '
    'tb_codaspe
    '
    Me.tb_codaspe.AppearanceCell.Options.UseBackColor = True
    Me.tb_codaspe.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codaspe.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codaspe.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_codaspe.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_codaspe.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_codaspe.Caption = "Cod. Aspetto"
    Me.tb_codaspe.Enabled = True
    Me.tb_codaspe.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codaspe.Name = "tb_codaspe"
    Me.tb_codaspe.NTSRepositoryComboBox = Nothing
    Me.tb_codaspe.NTSRepositoryItemCheck = Nothing
    Me.tb_codaspe.NTSRepositoryItemMemo = Nothing
    Me.tb_codaspe.NTSRepositoryItemText = Nothing
    Me.tb_codaspe.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codaspe.OptionsFilter.AllowFilter = False
    Me.tb_codaspe.Visible = True
    Me.tb_codaspe.VisibleIndex = 0
    '
    'tb_desaspe
    '
    Me.tb_desaspe.AppearanceCell.Options.UseBackColor = True
    Me.tb_desaspe.AppearanceCell.Options.UseTextOptions = True
    Me.tb_desaspe.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_desaspe.AppearanceHeader.Options.UseTextOptions = True
    Me.tb_desaspe.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.tb_desaspe.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    Me.tb_desaspe.Caption = "Descr. Aspetto dei Beni"
    Me.tb_desaspe.Enabled = True
    Me.tb_desaspe.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_desaspe.Name = "tb_desaspe"
    Me.tb_desaspe.NTSRepositoryComboBox = Nothing
    Me.tb_desaspe.NTSRepositoryItemCheck = Nothing
    Me.tb_desaspe.NTSRepositoryItemMemo = Nothing
    Me.tb_desaspe.NTSRepositoryItemText = Nothing
    Me.tb_desaspe.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_desaspe.OptionsFilter.AllowFilter = False
    Me.tb_desaspe.Visible = True
    Me.tb_desaspe.VisibleIndex = 1
    Me.tb_desaspe.Width = 200
    '
    'FRMVEASPE
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(688, 404)
    Me.Controls.Add(Me.pnCiva)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.HelpButton = True
    Me.Name = "FRMVEASPE"
    Me.NTSLastControlFocussed = Me.grAspe
    Me.Text = "ASPETTO DEI BENI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnCiva, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCiva.ResumeLayout(False)
    CType(Me.grAspe, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvAspe, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

#Region "Form"

  Public Overridable Sub FRMVEASPE_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcAspe.Dispose()
      If Not dsAspe Is Nothing Then dsAspe.Dispose()
    Catch ex As Exception

    End Try
  End Sub

  Public Overridable Sub FRMVEASPE_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMVEASPE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try

      'Predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'Leggo dal database i dati e collego l'NTSBinding
      If oCleAspe.Apri(DittaCorrente, dsAspe) Then
        dcAspe.DataSource = dsAspe.Tables("TABASPE")
        dsAspe.AcceptChanges()

        grAspe.DataSource = dcAspe

        '-------------------------------------------------
        'Applico le impostazioni personalizzate della form (Ex: CTRL+ALT+F2)
        'questa funzione può essere chiamata quando si vuole,
        'ad esempio dopo aver impostato la ditta, cambiato il tipo documento, ecc ...
        '(Ex: CTRL+ALT+F4)
        'GctlTipoDoc = ""
        GctlSetRoules()

        '--------------------------------------------
        'sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
        If Not oCallParams Is Nothing Then
          If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
            If grvAspe.NTSAllowInsert Then
              grvAspe.NTSNuovo()
            End If
          ElseIf Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) <> "" Then
            For i As Integer = 0 To dcAspe.List.Count - 1
              If CType(dcAspe.Item(i), DataRowView)!tb_codaspe.ToString = Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) Then
                dcAspe.Position = i
                Exit For
              End If
            Next
          End If
        End If  'If Not oCallParams Is Nothing Then
      Else
        Me.Close()
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione Friendly nasconde, sempre, alcune colonne
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = True Then
        tb_codaspe.Visible = False
        tb_desaspe.Visible = False

      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

#Region "Griglia"

  Public Overridable Sub grvCiva_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvAspe.NTSFocusedRowChanged
    Try
      'Se il codice magazzino è diverso da 0 blocco la colonna, diversamente la rendo editabile
      If NTSCInt(grvAspe.GetFocusedRowCellValue(tb_codaspe).ToString.Trim) <> 0 Then
        tb_codaspe.Enabled = False
      Else
        GctlSetVisEnab(tb_codaspe, False)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvCiva_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvAspe.NTSBeforeRowUpdate
    Try
      If Not Salva() Then
        'Rimango sulla riga su cui sono
        e.Allow = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tb_codtpel_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs)
    Dim oCZoo As New CLE__CZOO
    Dim bNuovo As Boolean = True
    Dim oTmp As New Control
    Dim oPar As New CLE__CLDP
    Try
      Me.ValidaLastControl()

      If e.TipoEvento = "OPEN" Then
        bNuovo = False
        oPar.strParam = "APRI;"
      Else
        oPar.strParam = "NUOV;"
      End If
      oPar.strParam += NTSCStr(grvAspe.NTSGetCurrentDataRow!tb_tipel) & ";"
      oTmp.Text = NTSCStr(grvAspe.NTSGetCurrentDataRow!tb_codtpel)
      oPar.strParam += oTmp.Text

      NTSZOOM.OpenChildGest(oTmp, "ZOOMTABSMEL", DittaCorrente, bNuovo, oPar)

      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo
      grAspe.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try

  End Sub

#End Region

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttTipiva As New DataTable()
    Dim dttTipel As New DataTable()
    Dim dttTpaggpl As New DataTable()
    Dim dttArt73t As New DataTable()
    Dim dttAliqdetr As New DataTable()
    Dim dttEliva As New DataTable
    Dim dttRilanacq As New DataTable
    Dim dttRevcharge As New DataTable

    Try
      'Carico le immagini della Toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")

        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        'tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        'tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'Non gestisco l'errore: 
        'se non c'è una immagine prendo quella a design
      End Try
      tlbMain.NTSSetToolTip()

      'E' una sorta di CaricaCombo inserita qui...
      dttTipiva.Columns.Add("cod", GetType(Short))
      dttTipiva.Columns.Add("val", GetType(String))
      dttTipiva.Rows.Add(New Object() {1, "Op. imponibili"})
      dttTipiva.Rows.Add(New Object() {2, "Op. esenti/non imponib."})
      dttTipiva.Rows.Add(New Object() {3, "Acq. art. 8 2° comma"})
      dttTipiva.Rows.Add(New Object() {4, "Fuori campo IVA"})
      'dttTipiva.Rows.Add(New Object() {5, "********"})
      dttTipiva.Rows.Add(New Object() {6, "********"})
      dttTipiva.Rows.Add(New Object() {7, "********"})
      dttTipiva.Rows.Add(New Object() {8, "Da ventilare"})
      dttTipiva.AcceptChanges()

      dttTipel.Columns.Add("cod", GetType(String))
      dttTipel.Columns.Add("val", GetType(String))
      dttTipel.Rows.Add(New Object() {"A", "Acquisti"})
      dttTipel.Rows.Add(New Object() {"V", "Vendite"})
      dttTipel.AcceptChanges()

      dttTpaggpl.Columns.Add("cod", GetType(String))
      dttTpaggpl.Columns.Add("val", GetType(String))
      dttTpaggpl.Rows.Add(New Object() {"N", "No"})
      dttTpaggpl.Rows.Add(New Object() {"I", "Acq. interno"})
      dttTpaggpl.Rows.Add(New Object() {"E", "Acq. estero"})
      dttTpaggpl.Rows.Add(New Object() {"V", "Vendite"})
      dttTpaggpl.AcceptChanges()

      dttArt73t.Columns.Add("cod", GetType(String))
      dttArt73t.Columns.Add("val", GetType(String))
      dttArt73t.Rows.Add(New Object() {"N", "No"})
      dttArt73t.Rows.Add(New Object() {"1", "Acq./Vend. UE"})
      dttArt73t.Rows.Add(New Object() {"2", "Acq./Vend. no UE"})
      dttArt73t.Rows.Add(New Object() {"3", "Corr. viaggi misti UE/no UE"})
      dttArt73t.Rows.Add(New Object() {"4", "Corr. viaggi misti UE"})
      dttArt73t.Rows.Add(New Object() {"5", "Corr. viaggi misti no UE"})
      dttArt73t.AcceptChanges()

      dttAliqdetr.Columns.Add("cod", GetType(String))
      dttAliqdetr.Columns.Add("val", GetType(String))
      dttAliqdetr.Rows.Add(New Object() {"S", "Si"})
      dttAliqdetr.Rows.Add(New Object() {"N", "No"})
      dttAliqdetr.Rows.Add(New Object() {"P", "Parzialmente"})
      dttAliqdetr.AcceptChanges()


      dttEliva.Columns.Add("cod", GetType(String))
      dttEliva.Columns.Add("val", GetType(String))
      dttEliva.Rows.Add(New Object() {" ", "Vedi tipo Iva"})
      dttEliva.Rows.Add(New Object() {"1", "Operazioni imponibili"})
      dttEliva.Rows.Add(New Object() {"2", "Operazioni non imponibili"})
      dttEliva.Rows.Add(New Object() {"3", "Operazioni esenti"})
      dttEliva.Rows.Add(New Object() {"4", "Operazioni imponibili con Iva non esposta in fattura"})
      dttEliva.Rows.Add(New Object() {"5", "Imponibile con Iva a margine"})
      dttEliva.Rows.Add(New Object() {"6", "Escluso da Elenchi Iva"})
      dttEliva.AcceptChanges()

      dttRilanacq.Columns.Add("cod", GetType(String))
      dttRilanacq.Columns.Add("val", GetType(String))
      dttRilanacq.Rows.Add(New Object() {" ", "(Non definito)"})
      dttRilanacq.Rows.Add(New Object() {"M", "Merci"})
      dttRilanacq.Rows.Add(New Object() {"S", "Servizi"})
      dttRilanacq.Rows.Add(New Object() {"A", "Noleggio/Leasing Autovettura"})
      dttRilanacq.Rows.Add(New Object() {"B", "Noleggio/Leasing Caravan"})
      dttRilanacq.Rows.Add(New Object() {"C", "Noleggio/Leasing Altri veicoli"})
      dttRilanacq.Rows.Add(New Object() {"D", "Noleggio/Leasing Unità da diporto"})
      dttRilanacq.Rows.Add(New Object() {"E", "Noleggio/Leasing Aeromobili"})
      dttRilanacq.AcceptChanges()

      dttRevcharge.Columns.Add("cod", GetType(String))
      dttRevcharge.Columns.Add("val", GetType(String))
      dttRevcharge.Rows.Add(New Object() {"N", "No"})
      dttRevcharge.Rows.Add(New Object() {"A", "Acq. da non resid. Rapp.fisc."})
      dttRevcharge.Rows.Add(New Object() {"D", "Quote gas"})
      dttRevcharge.Rows.Add(New Object() {"E", "Certificati gas e energia"})
      dttRevcharge.Rows.Add(New Object() {"G", "Gas e energia da rivendita"})
      dttRevcharge.Rows.Add(New Object() {"F", "Fabbricati compravendita"})
      dttRevcharge.Rows.Add(New Object() {"H", "Hi-tech telefonia"})
      dttRevcharge.Rows.Add(New Object() {"I", "Hi-tech computer"})
      dttRevcharge.Rows.Add(New Object() {"M", "Manutenzione edifici"})
      dttRevcharge.Rows.Add(New Object() {"O", "Oro da investimento"})
      dttRevcharge.Rows.Add(New Object() {"Q", "Oro industriale"})
      dttRevcharge.Rows.Add(New Object() {"P", "Pallet"})
      dttRevcharge.Rows.Add(New Object() {"R", "Rottami ferrosi"})
      dttRevcharge.Rows.Add(New Object() {"S", "Subappalto"})
      dttRevcharge.Rows.Add(New Object() {"T", "Supermercati"})
      dttRevcharge.Rows.Add(New Object() {"V", "Prod. lapidei"})
      dttRevcharge.AcceptChanges()


      grvAspe.NTSSetParam(oMenu, oApp.Tr(Me, 128230023372468841, "Griglia Aspetto dei beni"))

      tb_codaspe.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023372625014, "Cod.Aspetto"), tabaspe)
      tb_desaspe.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023372781187, "Descr. Aspetto dei beni"), 20, False)

      'In questo modo non faccio apparire lo zoom a fianco del codice magazzino (lo zoom era stato abilitato dalla NTSSetParamTabe)
      tb_codaspe.NTSSetParamZoom("")

      tb_codaspe.NTSSetRichiesto()
      tb_desaspe.NTSSetRichiesto()


      'Chiamo lo script per inizializzare i controlli caricati con Source Extender
      NTSScriptExec("InitControls", Me, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Public Overridable Function Salva() As Boolean
    Try

      Me.ValidaLastControl()      'Valido l'ultimo controllo che ha il focus

      Select Case grvAspe.NTSSalvaRigaCorrente(dcAspe, oCleAspe.RecordIsChanged, False)
        Case System.Windows.Forms.DialogResult.Yes
          'Salvo
          '-------------------------------------------------
          'Controllo che i campi abbiano un valore diverso da quello 
          'impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleAspe.Salva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'Ripristino
          tlbRipristina_ItemClick(Nothing, Nothing)
        Case System.Windows.Forms.DialogResult.Cancel
          'Non posso fare nulla
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          'La riga non ha subito modifiche
      End Select

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim strNomeRpt As String = ""
    Dim strTitleRpt As String = ""
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim i As Integer

    Try

      If Not Salva() Then Return


      '--------------------------------------------------
      'Preparo il motore di stampa
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BS--CIVA", "Reports1", " ", 0, nDestin, strNomeRpt, False, strTitleRpt, False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'Lancio tutti gli eventuali reports (gestisce già il multireport)
      For i = 1 To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i))))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally

    End Try
  End Sub


#Region "Toolbar"

  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvAspe.NTSNuovo()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDuplica_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
    Dim strCodOld As String = ""
    Try
      If Not Salva() Then Return

      'test su riga valida
      If grvAspe.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130723482390952496, "Posizionarsi su un record valido"))
        Return
      Else
        strCodOld = NTSCStr(grvAspe.NTSGetCurrentDataRow()!tb_codaspe)
      End If

      dcAspe.MoveLast()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally

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
      If grvAspe.NTSGetCurrentDataRow Is Nothing Then Return

      If oCleAspe.TestPreCancella(grvAspe.NTSGetCurrentDataRow) Then
        If grvAspe.NTSDeleteRigaCorrente(dcAspe, True) Then oCleAspe.Salva(True)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If grvAspe.NTSRipristinaRigaCorrenteBefore(dcAspe, CType(IIf(sender Is Nothing, False, True), Boolean)) Then
        oCleAspe.Ripristina(dcAspe.Position, dcAspe.Filter)
        grvAspe.NTSRipristinaRigaCorrenteAfter()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim oParam As New CLE__PATB

    Try
      If grvAspe.FocusedColumn.Name = "tb_codtpel" Then
        '--------------------------------------------
        'zoom tabsmel
        SetFastZoom(NTSCStr(grvAspe.EditingValue), oParam)    'abilito la gestione dello zoom veloce
        oParam.bVisGriglia = True
        oParam.strTipo = NTSCStr(grvAspe.GetFocusedRowCellValue("tb_tipel"))
        NTSZOOM.strIn = NTSCStr(grvAspe.EditingValue)
        NTSZOOM.ZoomStrIn("ZOOMTABSMEL", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvAspe.EditingValue) Then grvAspe.SetFocusedValue(NTSZOOM.strIn)

      Else
        NTSCallStandardZoom()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
    Try
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
    Try
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
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    Try
      oMenu.ReportImposta(Me)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

End Class
