Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMPRCAPR

#Region "Dichiarazione variabili"
  Public oCleCapr As CLEPRCAPR
  Public oCallParams As CLE__CLDP
  Public strCodagedaOld As String = "0"
  Public strDescrdaOld As String = ""
  Public strCodageaOld As String = "0"
  Public strDescraOld As String = ""
  Private components As System.ComponentModel.IComponent

  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents lbDaagente As NTSInformatica.NTSLabel
  Public WithEvents edCodageda As NTSInformatica.NTSTextBoxNum
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbEsegui As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbAagente As NTSInformatica.NTSLabel
  Public WithEvents edCodagea As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDtelaborazione As NTSInformatica.NTSTextBoxData
  Public WithEvents lbAdata As NTSInformatica.NTSLabel
  Public WithEvents ckSaldate As NTSInformatica.NTSCheckBox
  Public WithEvents lbAgenteda As NTSInformatica.NTSLabel
  Public WithEvents lbAgentea As NTSInformatica.NTSLabel
#End Region

#Region "Moduli"
  Private Moduli_P As Integer = CLN__STD.bsModPR
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNPRCAPR", "BEPRCAPR", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128589409211093750, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleCapr = CType(oTmp, CLEPRCAPR)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNPRCAPR", strRemoteServer, strRemotePort)
    AddHandler oCleCapr.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleCapr.Init(oApp, NTSScript, oMenu.oCleComm, "TABCAPR", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    '------------------------------------------------
    'povra di una chamata CallByName: 
    'la funzione Pippo è contenuta nella BG__PAGA che eredita da BE__PAGA
    'CallByName(oCleProv, "Pippo", CallType.Method, "Pippo")
    '------------------------------------------------
    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMPRCAPR))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbEsegui = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.edCodageda = New NTSInformatica.NTSTextBoxNum
    Me.lbDaagente = New NTSInformatica.NTSLabel
    Me.lbAagente = New NTSInformatica.NTSLabel
    Me.edCodagea = New NTSInformatica.NTSTextBoxNum
    Me.lbAdata = New NTSInformatica.NTSLabel
    Me.edDtelaborazione = New NTSInformatica.NTSTextBoxData
    Me.lbAgentea = New NTSInformatica.NTSLabel
    Me.lbAgenteda = New NTSInformatica.NTSLabel
    Me.ckSaldate = New NTSInformatica.NTSCheckBox
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodageda.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodagea.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDtelaborazione.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSaldate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbZoom, Me.tlbGuida, Me.tlbEsci, Me.tlbEsegui})
    Me.NtsBarManager1.MaxItemId = 14
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsegui), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbEsegui
    '
    Me.tlbEsegui.Caption = "Esegui"
    Me.tlbEsegui.Glyph = CType(resources.GetObject("tlbEsegui.Glyph"), System.Drawing.Image)
    Me.tlbEsegui.Id = 13
    Me.tlbEsegui.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F7))
    Me.tlbEsegui.Name = "tlbEsegui"
    Me.tlbEsegui.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 4
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
    'edCodageda
    '
    Me.edCodageda.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodageda.EditValue = "0"
    Me.edCodageda.Location = New System.Drawing.Point(79, 97)
    Me.edCodageda.Name = "edCodageda"
    Me.edCodageda.NTSDbField = ""
    Me.edCodageda.NTSFormat = "0"
    Me.edCodageda.NTSForzaVisZoom = False
    Me.edCodageda.NTSOldValue = ""
    Me.edCodageda.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodageda.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodageda.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodageda.Properties.MaxLength = 65536
    Me.edCodageda.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodageda.Size = New System.Drawing.Size(63, 20)
    Me.edCodageda.TabIndex = 5
    '
    'lbDaagente
    '
    Me.lbDaagente.AutoSize = True
    Me.lbDaagente.BackColor = System.Drawing.Color.Transparent
    Me.lbDaagente.Location = New System.Drawing.Point(12, 100)
    Me.lbDaagente.Name = "lbDaagente"
    Me.lbDaagente.NTSDbField = ""
    Me.lbDaagente.Size = New System.Drawing.Size(61, 13)
    Me.lbDaagente.TabIndex = 7
    Me.lbDaagente.Text = "Da agente:"
    '
    'lbAagente
    '
    Me.lbAagente.AutoSize = True
    Me.lbAagente.BackColor = System.Drawing.Color.Transparent
    Me.lbAagente.Location = New System.Drawing.Point(12, 133)
    Me.lbAagente.Name = "lbAagente"
    Me.lbAagente.NTSDbField = ""
    Me.lbAagente.Size = New System.Drawing.Size(55, 13)
    Me.lbAagente.TabIndex = 9
    Me.lbAagente.Text = "A agente:"
    '
    'edCodagea
    '
    Me.edCodagea.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodagea.EditValue = "9999"
    Me.edCodagea.Location = New System.Drawing.Point(79, 130)
    Me.edCodagea.Name = "edCodagea"
    Me.edCodagea.NTSDbField = ""
    Me.edCodagea.NTSFormat = "0"
    Me.edCodagea.NTSForzaVisZoom = False
    Me.edCodagea.NTSOldValue = "9999"
    Me.edCodagea.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodagea.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodagea.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodagea.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodagea.Size = New System.Drawing.Size(63, 20)
    Me.edCodagea.TabIndex = 8
    '
    'lbAdata
    '
    Me.lbAdata.AutoSize = True
    Me.lbAdata.BackColor = System.Drawing.Color.Transparent
    Me.lbAdata.Location = New System.Drawing.Point(12, 53)
    Me.lbAdata.Name = "lbAdata"
    Me.lbAdata.NTSDbField = ""
    Me.lbAdata.Size = New System.Drawing.Size(43, 13)
    Me.lbAdata.TabIndex = 10
    Me.lbAdata.Text = "A data:"
    '
    'edDtelaborazione
    '
    Me.edDtelaborazione.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDtelaborazione.EditValue = "01/01/1900"
    Me.edDtelaborazione.Location = New System.Drawing.Point(79, 50)
    Me.edDtelaborazione.Name = "edDtelaborazione"
    Me.edDtelaborazione.NTSDbField = ""
    Me.edDtelaborazione.NTSForzaVisZoom = False
    Me.edDtelaborazione.NTSOldValue = ""
    Me.edDtelaborazione.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDtelaborazione.Properties.MaxLength = 65536
    Me.edDtelaborazione.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDtelaborazione.Size = New System.Drawing.Size(95, 20)
    Me.edDtelaborazione.TabIndex = 11
    '
    'lbAgentea
    '
    Me.lbAgentea.BackColor = System.Drawing.Color.Transparent
    Me.lbAgentea.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbAgentea.Location = New System.Drawing.Point(148, 130)
    Me.lbAgentea.Name = "lbAgentea"
    Me.lbAgentea.NTSDbField = ""
    Me.lbAgentea.Size = New System.Drawing.Size(163, 20)
    Me.lbAgentea.TabIndex = 12
    '
    'lbAgenteda
    '
    Me.lbAgenteda.BackColor = System.Drawing.Color.Transparent
    Me.lbAgenteda.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbAgenteda.Location = New System.Drawing.Point(148, 97)
    Me.lbAgenteda.Name = "lbAgenteda"
    Me.lbAgenteda.NTSDbField = ""
    Me.lbAgenteda.Size = New System.Drawing.Size(163, 20)
    Me.lbAgenteda.TabIndex = 13
    '
    'ckSaldate
    '
    Me.ckSaldate.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSaldate.EditValue = True
    Me.ckSaldate.Location = New System.Drawing.Point(15, 165)
    Me.ckSaldate.Name = "ckSaldate"
    Me.ckSaldate.NTSCheckValue = "S"
    Me.ckSaldate.NTSUnCheckValue = "N"
    Me.ckSaldate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSaldate.Properties.Appearance.Options.UseBackColor = True
    Me.ckSaldate.Properties.Caption = "Solo provvigioni saldate"
    Me.ckSaldate.Size = New System.Drawing.Size(159, 19)
    Me.ckSaldate.TabIndex = 14
    '
    'FRMPRCAPR
    '
    Me.ClientSize = New System.Drawing.Size(323, 193)
    Me.Controls.Add(Me.ckSaldate)
    Me.Controls.Add(Me.lbAgenteda)
    Me.Controls.Add(Me.lbAgentea)
    Me.Controls.Add(Me.edDtelaborazione)
    Me.Controls.Add(Me.lbAdata)
    Me.Controls.Add(Me.lbAagente)
    Me.Controls.Add(Me.edCodagea)
    Me.Controls.Add(Me.lbDaagente)
    Me.Controls.Add(Me.edCodageda)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMPRCAPR"
    Me.Text = "Cancellazione Provvigioni"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodageda.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodagea.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDtelaborazione.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSaldate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      Try
        tlbEsegui.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try

      edCodageda.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128612710472968750, "Codice Da agente"), tabcage)
      edCodagea.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128612710506718750, "Codice A agente"), tabcage)
      ckSaldate.NTSSetParam(oMenu, oApp.Tr(Me, 128612698605156250, "Solo provvigioni saldate"), "S", "N")
      edDtelaborazione.NTSSetParam(oMenu, oApp.Tr(Me, 128612698605625000, "Data"), False)

      NTSScriptExec("InitControls", Me, Nothing)

    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRMPRCAPR_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      InitControls()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      'e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipodocumento, ecc ...
      'GctlTipoDoc = ""
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Toolbar"
  Public Overridable Sub tlbEsegui_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsegui.ItemClick
    Dim dlgProcedi As DialogResult
    Dim lResult As Integer = 0

    Try
      Me.ValidaLastControl()

      dlgProcedi = oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128612821648437500, "ATTENZIONE! Saranno cancellate tutte le provvigioni in base ai dati impostati, procedere?"))

      If dlgProcedi = Windows.Forms.DialogResult.No Then Return

      Cursor = Cursors.WaitCursor
      oCleCapr.CancellaProvvigioni(edDtelaborazione.Text, edCodageda.Text, edCodagea.Text, ckSaldate.Checked, lResult)
      Cursor = Cursors.Default

      oApp.MsgBoxInfo(oApp.Tr(Me, 128612822032343750, "Elaborazione Completata. Cancellate |" & NTSCStr(lResult) & "| provvigioni"))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
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

  Public Overridable Sub edCodageda_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodageda.Validated
    Try
      If edCodageda.Text <> "0" Then
        If NTSCInt(edCodageda.Text) <= NTSCInt(edCodagea.Text) Then
          If Not oCleCapr.ValidaCodice(edCodageda.Text, lbAgenteda.Text) Then
            edCodageda.Text = strCodagedaOld
            lbAgenteda.Text = strDescrdaOld
          End If
        Else
          edCodageda.Text = strCodagedaOld
          oApp.MsgBoxErr("Il codice agente iniziale non può essere superiore a quello finale")
        End If
      Else
        lbAgenteda.Text = ""
      End If

      strCodagedaOld = edCodageda.Text
      strDescrdaOld = lbAgenteda.Text
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edCodagea_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodagea.Validated
    Try
      If edCodagea.Text <> "9999" Then
        If NTSCInt(edCodageda.Text) <= NTSCInt(edCodagea.Text) Then
          If Not oCleCapr.ValidaCodice(edCodagea.Text, lbAgentea.Text) Then
            edCodagea.Text = strCodageaOld
            lbAgentea.Text = strDescraOld
          End If
        Else
          edCodagea.Text = strCodageaOld
          oApp.MsgBoxErr("Il codice agente iniziale non può essere superiore a quello finale")
        End If
      Else
        lbAgentea.Text = ""
      End If

      strCodageaOld = edCodagea.Text
      strDescraOld = lbAgentea.Text
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub

End Class
