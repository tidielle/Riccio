#Region "Imports"
Imports System.Data
Imports NTSInformatica.CLN__STD
#End Region

Public Class FRMPRELMP

#Region "Moduli"
  Private Moduli_P As Integer = bsModPR
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

#Region "Variaribli"
  Public oCleElmp As CLEPRELMP
  Public strWhereFiar As String = ""
  Public oCallParams As CLE__CLDP

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbElabora As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbData As NTSInformatica.NTSLabel
  Public WithEvents edData As NTSInformatica.NTSTextBoxData
  Public WithEvents ckSblocca As NTSInformatica.NTSCheckBox
  Public WithEvents pnElmp As NTSInformatica.NTSPanel
  Public WithEvents edSblocca As NTSInformatica.NTSMemoBox
  Public WithEvents lbSblocca As NTSInformatica.NTSLabel
#End Region

  Public Overloads Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    '---------------------------------
    'questa funzione riceve gli eventi dall'ENTITY: rimappata rispetto a quella standard di FRM__CHILD
    'prima eseguo quella standard
    Dim strTmp() As String
    Dim i As Integer = 0

    If Not IsMyThrowRemoteEvent() Then Return 'il messaggio non è per questa form ...
    MyBase.GestisciEventiEntity(sender, e)

    Try
      '---------------------------------
      'adesso gestisco le specifiche
      'devo inserire delle funzioni qui sotto per fare in modo che al variare di dati nell'entity delle informazioni 
      'legate all'interfaccia grafica (ui) vengano allineate a quanto richiesto dall'entity

      If e.TipoEvento.Length < 10 Then Return
      strTmp = e.TipoEvento.Split(CType("|", Char))
      For i = 0 To strTmp.Length - 1
        Select Case strTmp(i).Substring(0, 10)
          Case "STATUSBAR:"
            edsblocca.Text = e.Message
            Me.Refresh()
        End Select
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMPRELMP))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbElabora = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.lbData = New NTSInformatica.NTSLabel
    Me.edData = New NTSInformatica.NTSTextBoxData
    Me.ckSblocca = New NTSInformatica.NTSCheckBox
    Me.pnElmp = New NTSInformatica.NTSPanel
    Me.edSblocca = New NTSInformatica.NTSMemoBox
    Me.lbSblocca = New NTSInformatica.NTSLabel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edData.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSblocca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnElmp, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnElmp.SuspendLayout()
    CType(Me.edSblocca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbElabora, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 11
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
    Me.tlbElabora.Id = 3
    Me.tlbElabora.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbElabora.Name = "tlbElabora"
    Me.tlbElabora.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 8
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 9
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'lbData
    '
    Me.lbData.AutoSize = True
    Me.lbData.BackColor = System.Drawing.Color.Transparent
    Me.lbData.Location = New System.Drawing.Point(118, 19)
    Me.lbData.Name = "lbData"
    Me.lbData.NTSDbField = ""
    Me.lbData.Size = New System.Drawing.Size(40, 13)
    Me.lbData.TabIndex = 10
    Me.lbData.Text = "fino al:"
    '
    'edData
    '
    Me.edData.Cursor = System.Windows.Forms.Cursors.Default
    Me.edData.Location = New System.Drawing.Point(164, 16)
    Me.edData.Name = "edData"
    Me.edData.NTSDbField = ""
    Me.edData.NTSForzaVisZoom = False
    Me.edData.NTSOldValue = ""
    Me.edData.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edData.Properties.MaxLength = 65536
    Me.edData.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edData.Size = New System.Drawing.Size(100, 20)
    Me.edData.TabIndex = 11
    '
    'ckSblocca
    '
    Me.ckSblocca.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSblocca.Location = New System.Drawing.Point(12, 42)
    Me.ckSblocca.Name = "ckSblocca"
    Me.ckSblocca.NTSCheckValue = "S"
    Me.ckSblocca.NTSUnCheckValue = "N"
    Me.ckSblocca.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSblocca.Properties.Appearance.Options.UseBackColor = True
    Me.ckSblocca.Properties.Caption = "Sblocca provvigioni sospese se scadenze saldate"
    Me.ckSblocca.Size = New System.Drawing.Size(423, 18)
    Me.ckSblocca.TabIndex = 7
    '
    'pnElmp
    '
    Me.pnElmp.AllowDrop = True
    Me.pnElmp.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnElmp.Appearance.Options.UseBackColor = True
    Me.pnElmp.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnElmp.Controls.Add(Me.ckSblocca)
    Me.pnElmp.Controls.Add(Me.lbData)
    Me.pnElmp.Controls.Add(Me.edSblocca)
    Me.pnElmp.Controls.Add(Me.edData)
    Me.pnElmp.Controls.Add(Me.lbSblocca)
    Me.pnElmp.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnElmp.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnElmp.Location = New System.Drawing.Point(0, 30)
    Me.pnElmp.Name = "pnElmp"
    Me.pnElmp.Size = New System.Drawing.Size(443, 377)
    Me.pnElmp.TabIndex = 14
    Me.pnElmp.Text = "NtsPanel1"
    '
    'edSblocca
    '
    Me.edSblocca.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSblocca.EditValue = ""
    Me.edSblocca.Location = New System.Drawing.Point(5, 101)
    Me.edSblocca.Name = "edSblocca"
    Me.edSblocca.NTSDbField = ""
    Me.edSblocca.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSblocca.Size = New System.Drawing.Size(435, 273)
    Me.edSblocca.TabIndex = 15
    '
    'lbSblocca
    '
    Me.lbSblocca.AutoSize = True
    Me.lbSblocca.BackColor = System.Drawing.Color.Transparent
    Me.lbSblocca.Location = New System.Drawing.Point(12, 85)
    Me.lbSblocca.Name = "lbSblocca"
    Me.lbSblocca.NTSDbField = ""
    Me.lbSblocca.Size = New System.Drawing.Size(145, 13)
    Me.lbSblocca.TabIndex = 35
    Me.lbSblocca.Text = "Elenco provvigioni sbloccate:"
    '
    'FRMPRELMP
    '
    Me.ClientSize = New System.Drawing.Size(443, 407)
    Me.Controls.Add(Me.pnElmp)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
    Me.MaximizeBox = False
    Me.Name = "FRMPRELMP"
    Me.Text = "ELABORAZIONE DELLE PROVVIGIONI MATURATE"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edData.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSblocca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnElmp, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnElmp.ResumeLayout(False)
    Me.pnElmp.PerformLayout()
    CType(Me.edSblocca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Me.Height = 143
    Me.MinimumSize = Me.Size

    '------------------------------------------------
    'creo e attivo l'entity e inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNPRELMP", "BEPRELMP", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128666353134259345, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleElmp = CType(oTmp, CLEPRELMP)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNPRELMP", strRemoteServer, strRemotePort)
    AddHandler oCleElmp.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleElmp.Init(oApp, oScript, oMenu.oCleComm, "provvig", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

#Region "Eventi Form"
  Public Overridable Sub FRMPRELMP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try

      edData.Text = Date.Now.ToString

      '-------------------------------------------------------
      '--- Predispongo i controlli
      '-------------------------------------------------------
      InitControls()

      oCleElmp.GetSettingBus()

      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      If oCleElmp.bProvvig2 Then
        ckSblocca.Checked = False
        ckSblocca.Enabled = False
        ckSblocca.Visible = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMPRELMP_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      'se chiamato da BNPRBNPV ...
      If Not oCallParams Is Nothing Then
        If oCallParams.strNomProg = "BNPRGNPV" Then
          edData.Text = oCallParams.strPar1
          edSblocca.Text = ""
          oCleElmp.Elabora(edData.Text, ckSblocca.Checked)
          Me.Close()
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"

  Public Overridable Sub tlbElabora_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbElabora.ItemClick
    Try

      Elabora()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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

  Public Overridable Sub LoadImage()
    '-------------------------------------------------
    'carico le immagini della toolbar
    Try
      tlbElabora.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
    Catch ex As Exception
    End Try
    Try
      tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
    Catch ex As Exception
    End Try
    Try
      tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
    Catch ex As Exception
      'non gestisco l'errore: se non c'Ã¨ una immagine prendo quella standard
    End Try
  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      LoadImage()
      tlbMain.NTSSetToolTip()

      '-------------------------------------------------
      'completo le informazioni dei controlli
      edData.NTSSetParam(oMenu, oApp.Tr(Me, 128671555072297611, "Data fino al"), False)

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

  Public Overridable Function Elabora() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      If OperazioneConsentita() = False Then Return False
      '--------------------------------------------------------------------------------------------------------------
      If Not TestPreElabora() Then Return False
      '--------------------------------------------------------------------------------------------------------------
      '--- Se impostata l'opzione di registro di controllo pre-elaborazione, segnala, in un file di testo
      '--- le eventuali righe di PROVVIG che, con una simulazione di elaborazione,
      '--- risultarebbero con il Maturato in negativo
      '--------------------------------------------------------------------------------------------------------------
      If oCleElmp.bEseguiTestPreElab = True Then
        If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 130906775736520182, "Attenzione!" & vbCrLf & _
          "Nel Registro di Business è attivo il controllo pre-elaborazione su maturato negativo:" & vbCrLf & _
          " . BSPRELMP\OPZIONI\EseguiTestPreElab" & vbCrLf & _
          "Il controllo potrebbe rallentare l'operazione." & vbCrLf & _
          "Procedere comunque con l'elaborazione?")) = Windows.Forms.DialogResult.No Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 130906778919701178, "Operazione annullata."))
          Return False
        Else
          If oCleElmp.TestPreElabora(edData.Text) = False Then
            If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 130906779804608596, "Attenzione!" & vbCrLf & _
              "Il controllo pre-elaborazione ha rilevato incongruenze su maturato." & vbCrLf & _
              "Proseguire comunque?")) = Windows.Forms.DialogResult.No Then
              If oCleElmp.bVisualizzaFilePreElab = True Then
                NTSProcessStart("notepad", oApp.Dir & "\" & "BSPRELMP.log")
              End If
              Return False
            End If
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      edSblocca.Text = ""
      Return oCleElmp.Elabora(edData.Text, ckSblocca.Checked)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function TestPreElabora() As Boolean
    Try

      If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128733747173593750, "Procedere con l'elaborazione?")) = Windows.Forms.DialogResult.Yes Then
        Return True
      Else
        Return False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub ckSblocca_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSblocca.CheckedChanged
    Try
      If ckSblocca.Checked Then
        Me.Height = 443
      Else
        Me.Height = 143
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function OperazioneConsentita() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      OperazioneConsentita = True
      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione "Friendly", scaduta e la data di registrazione è posteriore alla data di
      '--- scadenza della chiave, ritorna false
      '--------------------------------------------------------------------------------------------------------------
      If (CLN__STD.FRIENDLY = True) And _
         (NTSCDate(Now.ToShortDateString) > NTSCDate(oApp.ActKey.DataScad)) And _
         (NTSCDate(edData.Text) > NTSCDate(oApp.ActKey.DataScad)) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130161960628802790, "Attenzione!" & vbCrLf & _
          "Chiave di attivazione scaduta!" & vbCrLf & _
          "Operazione NON consentita su documenti con data posteriore al '|" & oApp.ActKey.DataScad & "|'."))
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

End Class
