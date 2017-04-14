Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__SABI

#Region "Variabili"
  Public oCleAbic As CLE__ABIC
  Public oCallParams As CLE__CLDP
  Private components As System.ComponentModel.IContainer

  Public bSabiAnnullato As Boolean

  Public lAbicAbi As Integer
  Public lAbicCab As Integer
  Public strAbicComune As String

  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents cmdZoom As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents pnSabi As NTSInformatica.NTSPanel
  Public WithEvents edAbcCab As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAbcabi As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_Abcabi As NTSInformatica.NTSLabel
  Public WithEvents lbAbcCab As NTSInformatica.NTSLabel
  Public WithEvents lbAbcComune As NTSInformatica.NTSLabel
  Public WithEvents edAbcComune As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAbcabi As NTSInformatica.NTSLabel

#End Region

  Private Sub InitializeComponent()
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.cmdZoom = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.pnSabi = New NTSInformatica.NTSPanel
    Me.lbAbcComune = New NTSInformatica.NTSLabel
    Me.edAbcComune = New NTSInformatica.NTSTextBoxStr
    Me.edAbcCab = New NTSInformatica.NTSTextBoxNum
    Me.edAbcabi = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_Abcabi = New NTSInformatica.NTSLabel
    Me.lbAbcCab = New NTSInformatica.NTSLabel
    Me.lbAbcabi = New NTSInformatica.NTSLabel
    CType(Me.pnSabi, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSabi.SuspendLayout()
    CType(Me.edAbcComune.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAbcCab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAbcabi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(420, 3)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(128, 24)
    Me.cmdOk.TabIndex = 9
    Me.cmdOk.Text = "&Ok"
    '
    'cmdZoom
    '
    Me.cmdZoom.ImageText = ""
    Me.cmdZoom.Location = New System.Drawing.Point(420, 28)
    Me.cmdZoom.Name = "cmdZoom"
    Me.cmdZoom.Size = New System.Drawing.Size(128, 24)
    Me.cmdZoom.TabIndex = 10
    Me.cmdZoom.Text = "&Zoom"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(420, 57)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(128, 24)
    Me.cmdAnnulla.TabIndex = 11
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'pnSabi
    '
    Me.pnSabi.AllowDrop = True
    Me.pnSabi.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSabi.Appearance.Options.UseBackColor = True
    Me.pnSabi.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSabi.Controls.Add(Me.lbAbcComune)
    Me.pnSabi.Controls.Add(Me.edAbcComune)
    Me.pnSabi.Controls.Add(Me.edAbcCab)
    Me.pnSabi.Controls.Add(Me.edAbcabi)
    Me.pnSabi.Controls.Add(Me.lbXx_Abcabi)
    Me.pnSabi.Controls.Add(Me.lbAbcCab)
    Me.pnSabi.Controls.Add(Me.lbAbcabi)
    Me.pnSabi.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnSabi.Location = New System.Drawing.Point(12, 3)
    Me.pnSabi.Name = "pnSabi"
    Me.pnSabi.NTSActiveTrasparency = True
    Me.pnSabi.Size = New System.Drawing.Size(393, 76)
    Me.pnSabi.TabIndex = 12
    Me.pnSabi.Text = "NtsPanel1"
    '
    'lbAbcComune
    '
    Me.lbAbcComune.AutoSize = True
    Me.lbAbcComune.BackColor = System.Drawing.Color.Transparent
    Me.lbAbcComune.Location = New System.Drawing.Point(3, 54)
    Me.lbAbcComune.Name = "lbAbcComune"
    Me.lbAbcComune.NTSDbField = ""
    Me.lbAbcComune.Size = New System.Drawing.Size(46, 13)
    Me.lbAbcComune.TabIndex = 17
    Me.lbAbcComune.Text = "Comune"
    Me.lbAbcComune.Tooltip = ""
    Me.lbAbcComune.UseMnemonic = False
    '
    'edAbcComune
    '
    Me.edAbcComune.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAbcComune.Location = New System.Drawing.Point(73, 51)
    Me.edAbcComune.Name = "edAbcComune"
    Me.edAbcComune.NTSDbField = ""
    Me.edAbcComune.NTSForzaVisZoom = False
    Me.edAbcComune.NTSOldValue = ""
    Me.edAbcComune.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAbcComune.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAbcComune.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAbcComune.Properties.MaxLength = 65536
    Me.edAbcComune.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAbcComune.Size = New System.Drawing.Size(308, 20)
    Me.edAbcComune.TabIndex = 16
    '
    'edAbcCab
    '
    Me.edAbcCab.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAbcCab.Location = New System.Drawing.Point(74, 25)
    Me.edAbcCab.Name = "edAbcCab"
    Me.edAbcCab.NTSDbField = ""
    Me.edAbcCab.NTSFormat = "0"
    Me.edAbcCab.NTSForzaVisZoom = False
    Me.edAbcCab.NTSOldValue = ""
    Me.edAbcCab.Properties.Appearance.Options.UseTextOptions = True
    Me.edAbcCab.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAbcCab.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAbcCab.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAbcCab.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAbcCab.Properties.MaxLength = 65536
    Me.edAbcCab.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAbcCab.Size = New System.Drawing.Size(83, 20)
    Me.edAbcCab.TabIndex = 15
    '
    'edAbcabi
    '
    Me.edAbcabi.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAbcabi.Location = New System.Drawing.Point(74, 0)
    Me.edAbcabi.Name = "edAbcabi"
    Me.edAbcabi.NTSDbField = ""
    Me.edAbcabi.NTSFormat = "0"
    Me.edAbcabi.NTSForzaVisZoom = False
    Me.edAbcabi.NTSOldValue = ""
    Me.edAbcabi.Properties.Appearance.Options.UseTextOptions = True
    Me.edAbcabi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAbcabi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAbcabi.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAbcabi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAbcabi.Properties.MaxLength = 65536
    Me.edAbcabi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAbcabi.Size = New System.Drawing.Size(83, 20)
    Me.edAbcabi.TabIndex = 14
    '
    'lbXx_Abcabi
    '
    Me.lbXx_Abcabi.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Abcabi.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Abcabi.Location = New System.Drawing.Point(163, 0)
    Me.lbXx_Abcabi.Name = "lbXx_Abcabi"
    Me.lbXx_Abcabi.NTSDbField = ""
    Me.lbXx_Abcabi.Size = New System.Drawing.Size(218, 20)
    Me.lbXx_Abcabi.TabIndex = 13
    Me.lbXx_Abcabi.Tooltip = ""
    Me.lbXx_Abcabi.UseMnemonic = False
    '
    'lbAbcCab
    '
    Me.lbAbcCab.AutoSize = True
    Me.lbAbcCab.BackColor = System.Drawing.Color.Transparent
    Me.lbAbcCab.Location = New System.Drawing.Point(2, 28)
    Me.lbAbcCab.Name = "lbAbcCab"
    Me.lbAbcCab.NTSDbField = ""
    Me.lbAbcCab.Size = New System.Drawing.Size(61, 13)
    Me.lbAbcCab.TabIndex = 10
    Me.lbAbcCab.Text = "Codice Cab"
    Me.lbAbcCab.Tooltip = ""
    Me.lbAbcCab.UseMnemonic = False
    '
    'lbAbcabi
    '
    Me.lbAbcabi.AutoSize = True
    Me.lbAbcabi.BackColor = System.Drawing.Color.Transparent
    Me.lbAbcabi.Location = New System.Drawing.Point(2, 3)
    Me.lbAbcabi.Name = "lbAbcabi"
    Me.lbAbcabi.NTSDbField = ""
    Me.lbAbcabi.Size = New System.Drawing.Size(57, 13)
    Me.lbAbcabi.TabIndex = 9
    Me.lbAbcabi.Text = "Codice Abi"
    Me.lbAbcabi.Tooltip = ""
    Me.lbAbcabi.UseMnemonic = False
    '
    'FRM__SABI
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(559, 84)
    Me.Controls.Add(Me.pnSabi)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdZoom)
    Me.Controls.Add(Me.cmdOk)
    Me.Name = "FRM__SABI"
    Me.Text = ""
    CType(Me.pnSabi, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSabi.ResumeLayout(False)
    Me.pnSabi.PerformLayout()
    CType(Me.edAbcComune.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAbcCab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAbcabi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      edAbcComune.NTSSetParam(oMenu, oApp.Tr(Me, 128867052101421401, "Comune"), 50)
      edAbcCab.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128867052125062466, "Codice Cab"), tababicab)
      edAbcabi.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128867052142551581, "Codice Abi"), tababi)

      edAbcCab.NTSSetRichiesto()
      edAbcabi.NTSSetRichiesto()

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
  Public Overridable Sub InitEntity(ByRef cleAbic As CLE__ABIC)
    oCleAbic = cleAbic
    AddHandler oCleAbic.RemoteEvent, AddressOf GestisciEventiEntity
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

    '------------------------------
    'collego il datatable agli altri oggetti di form

    Return True
  End Function

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Dim dsAbiCab As DataSet = Nothing
    Dim dsAbiCabNuovo As DataSet = Nothing
    Dim dsAbi As DataSet = Nothing
    Try
      '-----------------------------------------------------------------------------------------
      '--- Controlla la validità dei dati
      '-----------------------------------------------------------------------------------------
      If oCleAbic.bAbicApri = True Then
        If (NTSCInt(edAbcabi.Text) = 0) And ((edAbcComune.Text = "")) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128867052169805696, "Digitare almeno il Codice abi o il Comune."))
          Exit Sub
        End If
        oCleAbic.DesAbi(edAbcabi.Text, lbXx_Abcabi.Text)

        oCleAbic.CheckAbiCab(NTSCInt(edAbcabi.Text), NTSCInt(edAbcCab.Text), NTSCStr(edAbcComune.Text), dsAbiCab)

        If dsAbiCab.Tables("ABICAB").Rows.Count = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128867052195184931, "Non esistono dati con queste caratteristiche."))
          Exit Sub
        End If
      End If
      '---------------------------------------------------------------------
      If oCleAbic.bAbicNuovo = True Then
        If NTSCInt(edAbcabi.Text) = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128867052221794556, "Il codice abi non può essere nullo."))
          Exit Sub
        End If
        If NTSCInt(edAbcCab.Text) = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128867052246910136, "Il codice cab non può essere nullo."))
          Exit Sub
        End If

        oCleAbic.CheckAbiCabNuovo(NTSCInt(edAbcabi.Text), NTSCInt(edAbcCab.Text), dsAbiCabNuovo)

        If dsAbiCabNuovo.Tables("ABICAB").Rows.Count > 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128867052271869476, "Esistono già dati con queste caratterisctiche."))
          Exit Sub
        End If

        oCleAbic.CheckAbi(NTSCInt(edAbcabi.Text), dsAbi)

        If dsAbi.Tables("TABABI").Rows.Count = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128867052294612161, "Codice Abi inesistente."))
          Exit Sub
        End If
      End If

      '-----------------------------------------------------------------------------------------
      '--- Setta la variabili di ritorno
      '-----------------------------------------------------------------------------------------
      bSabiAnnullato = False
      lAbicAbi = NTSCInt(edAbcabi.Text)
      lAbicCab = NTSCInt(edAbcCab.Text)
      strAbicComune = NTSCStr(edAbcComune.Text)
      '-----------------------------------------------------------------------------------------
      Me.Close()
      '-----------------------------------------------------------------------------------------
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdZoom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdZoom.Click
    Zoom()
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    bSabiAnnullato = True
    Me.Close()
  End Sub

  Public Overridable Sub FRM__SABI_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      bSabiAnnullato = True

      lAbicAbi = 0
      lAbicCab = 0
      strAbicComune = ""
      edAbcabi.Text = "0"
      edAbcCab.Text = "0"
      edAbcComune.Text = ""
      If oCleAbic.bAbicNuovo = True Then
        lbAbcComune.Visible = False
        edAbcComune.Visible = False
        Me.Text = oApp.Tr(Me, 130420502568193148, "Nuovo Abi/Cab")
      End If

      If oCleAbic.bAbicApri = True Then
        Me.Text = oApp.Tr(Me, 130420502648506162, "Seleziona Abi/Cab")
      End If

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      Me.Cursor = Cursors.Default

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRM__SABI_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
    Try
      '--------------------------------------------
      'gestione dello zoom:
      'eseguo la Zoom, tanto se per il controllo non deve venir eseguito uno zoom particolare, la routine non fa nulla e viene processato lo zoom standard del controllo, settato con la NTSSetParamZoom
      If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
        Zoom()
        e.Handled = True    'altrimenti anche il controllo riceve l'F5 e la routine ZOOM viene eseguita 2 volte!!!
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub Zoom()
    'per eventuali altri controlli caricati al volo
    Dim ctrlTmp As Control = NTSFindControlForZoom()
    If ctrlTmp Is Nothing Then Return
    Dim oParam As New CLE__PATB

    Try
      If edAbcCab Is NTSLastControlFocussed() Or edAbcCab.Focused Then
        '  '--------------------------------------------
        '  'zoom tababicab
        SetFastZoom(edAbcCab.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edAbcCab.Text
        oParam.strDescr = NTSCStr(edAbcabi.Text)
        NTSZOOM.ZoomStrIn("ZOOMABICAB", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edAbcCab.Text Then edAbcCab.NTSTextDB = NTSZOOM.strIn
      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        NTSCallStandardZoom()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAbcabi_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edAbcabi.Validated
    Try
      oCleAbic.DesAbi(edAbcabi.Text, lbXx_Abcabi.Text)
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
End Class