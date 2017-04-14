Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__CHGP
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
  Public oCleChgp As CLE__CHGP
  Public oCallParams As CLE__CLDP
  Public WithEvents pnChgp As NTSInformatica.NTSPanel
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbOpNome As NTSInformatica.NTSLabel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdCambia As NTSInformatica.NTSButton
  Public WithEvents edConfermaPwd As NTSInformatica.NTSTextBoxStr
  Public WithEvents edNuovaPwd As NTSInformatica.NTSTextBoxStr
  Public WithEvents edOpPasswd As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbConfermaPwd As NTSInformatica.NTSLabel
  Public WithEvents lbNuovaPwd As NTSInformatica.NTSLabel
  Public WithEvents lbOpPasswd As NTSInformatica.NTSLabel
  Private components As System.ComponentModel.IContainer
#End Region

  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__CHGP))
    Me.pnChgp = New NTSInformatica.NTSPanel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdCambia = New NTSInformatica.NTSButton
    Me.edConfermaPwd = New NTSInformatica.NTSTextBoxStr
    Me.edNuovaPwd = New NTSInformatica.NTSTextBoxStr
    Me.edOpPasswd = New NTSInformatica.NTSTextBoxStr
    Me.lbConfermaPwd = New NTSInformatica.NTSLabel
    Me.lbNuovaPwd = New NTSInformatica.NTSLabel
    Me.lbOpPasswd = New NTSInformatica.NTSLabel
    Me.lbOpNome = New NTSInformatica.NTSLabel
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    CType(Me.pnChgp, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnChgp.SuspendLayout()
    CType(Me.edConfermaPwd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNuovaPwd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOpPasswd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    Me.frmPopup.ClientSize = New System.Drawing.Size(77, 21)
    '
    'pnChgp
    '
    Me.pnChgp.AllowDrop = True
    Me.pnChgp.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnChgp.Appearance.Options.UseBackColor = True
    Me.pnChgp.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnChgp.Controls.Add(Me.cmdAnnulla)
    Me.pnChgp.Controls.Add(Me.cmdCambia)
    Me.pnChgp.Controls.Add(Me.edConfermaPwd)
    Me.pnChgp.Controls.Add(Me.edNuovaPwd)
    Me.pnChgp.Controls.Add(Me.edOpPasswd)
    Me.pnChgp.Controls.Add(Me.lbConfermaPwd)
    Me.pnChgp.Controls.Add(Me.lbNuovaPwd)
    Me.pnChgp.Controls.Add(Me.lbOpPasswd)
    Me.pnChgp.Controls.Add(Me.lbOpNome)
    Me.pnChgp.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnChgp.Location = New System.Drawing.Point(1, 29)
    Me.pnChgp.Name = "pnChgp"
    Me.pnChgp.Size = New System.Drawing.Size(499, 139)
    Me.pnChgp.TabIndex = 0
    Me.pnChgp.Text = "NtsPanel1"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Location = New System.Drawing.Point(371, 94)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(101, 20)
    Me.cmdAnnulla.TabIndex = 8
    Me.cmdAnnulla.Text = "&Annulla (F8)"
    '
    'cmdCambia
    '
    Me.cmdCambia.Location = New System.Drawing.Point(371, 53)
    Me.cmdCambia.Name = "cmdCambia"
    Me.cmdCambia.Size = New System.Drawing.Size(101, 21)
    Me.cmdCambia.TabIndex = 7
    Me.cmdCambia.Text = "&Cambia (F9)"
    '
    'edConfermaPwd
    '
    Me.edConfermaPwd.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edConfermaPwd.Location = New System.Drawing.Point(147, 98)
    Me.edConfermaPwd.Name = "edConfermaPwd"
    Me.edConfermaPwd.NTSDbField = ""
    Me.edConfermaPwd.NTSForzaVisZoom = False
    Me.edConfermaPwd.NTSOldValue = ""
    Me.edConfermaPwd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edConfermaPwd.Properties.MaxLength = 65536
    Me.edConfermaPwd.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edConfermaPwd.Size = New System.Drawing.Size(193, 20)
    Me.edConfermaPwd.TabIndex = 6
    '
    'edNuovaPwd
    '
    Me.edNuovaPwd.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edNuovaPwd.Location = New System.Drawing.Point(147, 76)
    Me.edNuovaPwd.Name = "edNuovaPwd"
    Me.edNuovaPwd.NTSDbField = ""
    Me.edNuovaPwd.NTSForzaVisZoom = False
    Me.edNuovaPwd.NTSOldValue = ""
    Me.edNuovaPwd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNuovaPwd.Properties.MaxLength = 65536
    Me.edNuovaPwd.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNuovaPwd.Size = New System.Drawing.Size(193, 20)
    Me.edNuovaPwd.TabIndex = 5
    '
    'edOpPasswd
    '
    Me.edOpPasswd.Cursor = System.Windows.Forms.Cursors.Default
    Me.edOpPasswd.Location = New System.Drawing.Point(147, 54)
    Me.edOpPasswd.Name = "edOpPasswd"
    Me.edOpPasswd.NTSDbField = ""
    Me.edOpPasswd.NTSForzaVisZoom = False
    Me.edOpPasswd.NTSOldValue = ""
    Me.edOpPasswd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOpPasswd.Properties.MaxLength = 65536
    Me.edOpPasswd.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOpPasswd.Size = New System.Drawing.Size(193, 20)
    Me.edOpPasswd.TabIndex = 4
    '
    'lbConfermaPwd
    '
    Me.lbConfermaPwd.AutoSize = True
    Me.lbConfermaPwd.BackColor = System.Drawing.Color.Transparent
    Me.lbConfermaPwd.Location = New System.Drawing.Point(16, 101)
    Me.lbConfermaPwd.Name = "lbConfermaPwd"
    Me.lbConfermaPwd.NTSDbField = ""
    Me.lbConfermaPwd.Size = New System.Drawing.Size(116, 13)
    Me.lbConfermaPwd.TabIndex = 3
    Me.lbConfermaPwd.Text = "Ripeti nuova Password"
    '
    'lbNuovaPwd
    '
    Me.lbNuovaPwd.AutoSize = True
    Me.lbNuovaPwd.BackColor = System.Drawing.Color.Transparent
    Me.lbNuovaPwd.Location = New System.Drawing.Point(16, 79)
    Me.lbNuovaPwd.Name = "lbNuovaPwd"
    Me.lbNuovaPwd.NTSDbField = ""
    Me.lbNuovaPwd.Size = New System.Drawing.Size(87, 13)
    Me.lbNuovaPwd.TabIndex = 2
    Me.lbNuovaPwd.Text = "Nuova Password"
    '
    'lbOpPasswd
    '
    Me.lbOpPasswd.AutoSize = True
    Me.lbOpPasswd.BackColor = System.Drawing.Color.Transparent
    Me.lbOpPasswd.Location = New System.Drawing.Point(16, 57)
    Me.lbOpPasswd.Name = "lbOpPasswd"
    Me.lbOpPasswd.NTSDbField = ""
    Me.lbOpPasswd.Size = New System.Drawing.Size(100, 13)
    Me.lbOpPasswd.TabIndex = 1
    Me.lbOpPasswd.Text = "Password esistente"
    '
    'lbOpNome
    '
    Me.lbOpNome.BackColor = System.Drawing.Color.Transparent
    Me.lbOpNome.Location = New System.Drawing.Point(17, 22)
    Me.lbOpNome.Name = "lbOpNome"
    Me.lbOpNome.NTSDbField = ""
    Me.lbOpNome.Size = New System.Drawing.Size(403, 19)
    Me.lbOpNome.TabIndex = 0
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbRipristina, Me.tlbSalva, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 8
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.Id = 5
    Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.Id = 4
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 6
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 7
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'FRM__CHGP
    '
    Me.ClientSize = New System.Drawing.Size(500, 168)
    Me.Controls.Add(Me.pnChgp)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.HelpContext = "*"
    Me.Name = "FRM__CHGP"
    Me.Text = "Cambia Password operatore"
    CType(Me.pnChgp, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnChgp.ResumeLayout(False)
    Me.pnChgp.PerformLayout()
    CType(Me.edConfermaPwd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNuovaPwd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOpPasswd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      edConfermaPwd.NTSSetParam(oMenu, oApp.Tr(Me, 128305405600342621, "Ripeti nuova Password"), 14, True)
      edNuovaPwd.NTSSetParam(oMenu, oApp.Tr(Me, 128305405600521625, "Nuova Password"), 14, True)
      edOpPasswd.NTSSetParam(oMenu, oApp.Tr(Me, 128305405600700629, "Password esistente"), 14, True)

      edConfermaPwd.Properties.PasswordChar = CChar("*")
      edNuovaPwd.Properties.PasswordChar = CChar("*")
      edOpPasswd.Properties.PasswordChar = CChar("*")

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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__CHGP", "BE__CHGP", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128304534306564055, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleChgp = CType(oTmp, CLE__CHGP)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__CHGP", strRemoteServer, strRemotePort)
    AddHandler oCleChgp.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleChgp.Init(oApp, NTSScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    '------------------------------------------------

    Return True
  End Function

#Region "EventiForm"
  Public Overridable Sub FRM__CHGP_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRM__CHGP_ActivatedFirst(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ActivatedFirst
    Dim bOut As Boolean
    Try
      bOut = Apri()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "EventiToolbar"
  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      FileCambia()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      Me.Close()
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
    Dim strlbOpNome As String = ""
    Dim bDisabilita As Boolean = False
    Try

      If Not oCleChgp.Apri(DittaCorrente, strlbOpNome, bDisabilita) Then
        Me.Close()
        Return False
      End If
      If bDisabilita = True Then
        lbOpPasswd.Enabled = False
        edOpPasswd.Enabled = False
      End If
      lbOpNome.Text = strlbOpNome

      cmdCambia.Enabled = False
      tlbSalva.Enabled = False

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Sub FileCambia()
    Dim lFocus As Integer = 0
    Try

      If oCleChgp.FileCambia(edOpPasswd.Text, edNuovaPwd.Text, edConfermaPwd.Text, lFocus) Then
        Me.Close()
      Else
        Select Case lFocus
          Case 1 : edOpPasswd.Focus()
          Case 2 : edNuovaPwd.Focus()
          Case 3 : edConfermaPwd.Focus()
        End Select
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdCambia_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCambia.Click
    Try
      FileCambia()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edConfermaPwd_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edConfermaPwd.TextChanged
    Try
      If (edNuovaPwd.Text <> "") And (edConfermaPwd.Text <> "") Then
        cmdCambia.Enabled = True
        tlbSalva.Enabled = True
      Else
        cmdCambia.Enabled = False
        tlbSalva.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edNuovaPwd_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles edNuovaPwd.TextChanged
    Try
      If (edNuovaPwd.Text <> "") And (edConfermaPwd.Text <> "") Then
        cmdCambia.Enabled = True
        tlbSalva.Enabled = True
      Else
        cmdCambia.Enabled = False
        tlbSalva.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class
