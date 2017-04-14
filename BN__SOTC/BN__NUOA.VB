Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__NUOA
  Public oCallParams As CLE__CLDP
  Public oCleSotc As CLE__SOTC
  Public bOk As Boolean = False
  Public lContoOut As Integer = 0         'nuovo conto già testato e validato
  Public lContoPrec As Integer = 0

  Public Overridable Sub InitializeComponent()
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.pnMain = New NTSInformatica.NTSPanel
    Me.lbConto = New NTSInformatica.NTSLabel
    Me.lbCodMastro = New NTSInformatica.NTSLabel
    Me.lbMastro = New NTSInformatica.NTSLabel
    Me.edConto = New NTSInformatica.NTSTextBoxNum
    Me.edMastro = New NTSInformatica.NTSTextBoxNum
    Me.ckPdc = New NTSInformatica.NTSCheckBox
    CType(Me.pnMain, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnMain.SuspendLayout()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edMastro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckPdc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(231, Byte), Integer))
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
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(432, 40)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(79, 22)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(432, 12)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(79, 22)
    Me.cmdOk.TabIndex = 2
    Me.cmdOk.Text = "&Conferma"
    '
    'pnMain
    '
    Me.pnMain.AllowDrop = True
    Me.pnMain.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnMain.Appearance.Options.UseBackColor = True
    Me.pnMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnMain.Controls.Add(Me.ckPdc)
    Me.pnMain.Controls.Add(Me.lbConto)
    Me.pnMain.Controls.Add(Me.lbCodMastro)
    Me.pnMain.Controls.Add(Me.lbMastro)
    Me.pnMain.Controls.Add(Me.edConto)
    Me.pnMain.Controls.Add(Me.edMastro)
    Me.pnMain.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnMain.Location = New System.Drawing.Point(2, 8)
    Me.pnMain.Name = "pnMain"
    Me.pnMain.Size = New System.Drawing.Size(424, 79)
    Me.pnMain.TabIndex = 9
    Me.pnMain.Text = "pnMain"
    '
    'lbConto
    '
    Me.lbConto.AutoSize = True
    Me.lbConto.BackColor = System.Drawing.Color.Transparent
    Me.lbConto.Location = New System.Drawing.Point(10, 37)
    Me.lbConto.Name = "lbConto"
    Me.lbConto.NTSDbField = ""
    Me.lbConto.Size = New System.Drawing.Size(69, 13)
    Me.lbConto.TabIndex = 511
    Me.lbConto.Text = "Codice conto"
    Me.lbConto.Tooltip = ""
    Me.lbConto.UseMnemonic = False
    '
    'lbCodMastro
    '
    Me.lbCodMastro.AutoSize = True
    Me.lbCodMastro.BackColor = System.Drawing.Color.Transparent
    Me.lbCodMastro.Location = New System.Drawing.Point(10, 10)
    Me.lbCodMastro.Name = "lbCodMastro"
    Me.lbCodMastro.NTSDbField = ""
    Me.lbCodMastro.Size = New System.Drawing.Size(75, 13)
    Me.lbCodMastro.TabIndex = 510
    Me.lbCodMastro.Text = "Codice mastro"
    Me.lbCodMastro.Tooltip = ""
    Me.lbCodMastro.UseMnemonic = False
    '
    'lbMastro
    '
    Me.lbMastro.BackColor = System.Drawing.Color.Transparent
    Me.lbMastro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbMastro.Location = New System.Drawing.Point(171, 6)
    Me.lbMastro.Name = "lbMastro"
    Me.lbMastro.NTSDbField = ""
    Me.lbMastro.Size = New System.Drawing.Size(243, 20)
    Me.lbMastro.TabIndex = 509
    Me.lbMastro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbMastro.Tooltip = ""
    Me.lbMastro.UseMnemonic = False
    '
    'edConto
    '
    Me.edConto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edConto.EditValue = "0"
    Me.edConto.Location = New System.Drawing.Point(91, 34)
    Me.edConto.Name = "edConto"
    Me.edConto.NTSDbField = ""
    Me.edConto.NTSFormat = "0"
    Me.edConto.NTSForzaVisZoom = False
    Me.edConto.NTSOldValue = ""
    Me.edConto.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edConto.Properties.Appearance.Options.UseBackColor = True
    Me.edConto.Properties.Appearance.Options.UseTextOptions = True
    Me.edConto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edConto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edConto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edConto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edConto.Properties.MaxLength = 65536
    Me.edConto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edConto.Size = New System.Drawing.Size(74, 20)
    Me.edConto.TabIndex = 88
    '
    'edMastro
    '
    Me.edMastro.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMastro.EditValue = "0"
    Me.edMastro.Location = New System.Drawing.Point(91, 6)
    Me.edMastro.Name = "edMastro"
    Me.edMastro.NTSDbField = ""
    Me.edMastro.NTSFormat = "0"
    Me.edMastro.NTSForzaVisZoom = False
    Me.edMastro.NTSOldValue = ""
    Me.edMastro.Properties.Appearance.Options.UseTextOptions = True
    Me.edMastro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMastro.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMastro.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMastro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMastro.Properties.MaxLength = 65536
    Me.edMastro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMastro.Size = New System.Drawing.Size(74, 20)
    Me.edMastro.TabIndex = 87
    '
    'ckPdc
    '
    Me.ckPdc.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckPdc.Location = New System.Drawing.Point(13, 57)
    Me.ckPdc.Name = "ckPdc"
    Me.ckPdc.NTSCheckValue = "S"
    Me.ckPdc.NTSUnCheckValue = "N"
    Me.ckPdc.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPdc.Properties.Appearance.Options.UseBackColor = True
    Me.ckPdc.Properties.Caption = "Conto utilizzabile da tutte le ditte con PDC uguale a quello della ditta corrente" & _
        ""
    Me.ckPdc.Size = New System.Drawing.Size(408, 19)
    Me.ckPdc.TabIndex = 614
    '
    'FRM__NUOA
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(521, 89)
    Me.Controls.Add(Me.pnMain)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__NUOA"
    Me.Text = "NUOVO SOTTOCONTO"
    CType(Me.pnMain, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnMain.ResumeLayout(False)
    Me.pnMain.PerformLayout()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edMastro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckPdc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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


  Public Overridable Function InitEntity(ByVal cleSotc As CLE__SOTC) As Boolean
    Try

      oCleSotc = cleSotc
      AddHandler oCleSotc.RemoteEvent, AddressOf GestisciEventiEntity

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function


  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try

      edMastro.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128263751019052000, "Mastro contabile"), tabmast)
      edConto.NTSSetParam(oMenu, oApp.Tr(Me, 128309759299470000, "Progressivo sottoconto"), "0", 6, 0, 999999)
      edMastro.NTSSetParamZoom("ZOOMTABMAST")

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

  Public Overridable Sub FRM__NUOA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      edMastro.Text = NTSCInt(oMenu.GetSettingBusDitt(DittaCorrente, "BS--SOTC", "RECENT", ".", "UltimoMastro", "0", " ", "0")).ToString
      ckPdc.Checked = CBool(oMenu.GetSettingBusDitt(DittaCorrente, "BS--SOTC", "RECENT", ".", "FlPdc", "-1", " ", "-1"))

      '--------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
      GctlApplicaDefaultValue()
      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione Friendly nasconde, sempre, alcuni controlli
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = True Then
        ckPdc.Checked = False
        ckPdc.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRM__NUOA_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Dim ctrlTmp As Control = Nothing
    Try
      If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
        Zoom()
        e.Handled = True
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      If NTSCInt(edMastro.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128251683159926000, "Indicare un codice mastro, oppure selezionare 'Annulla'"))
        Return
      End If

      If NTSCInt(edConto.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128309774332080000, "Indicare un codice conto diverso da 0, oppure selezionare 'Annulla'"))
        Return
      End If

      '------------------------------
      'eseguo tutti i controlli di rito
      Me.Cursor = Cursors.WaitCursor
      If Not oCleSotc.FRM__NUOA_CheckConto(NTSCInt(edMastro.Text), NTSCInt(edConto.Text), lContoOut, ckPdc.Checked) Then Return

      oMenu.SaveSettingBusDitt(DittaCorrente, "BS--SOTC", "RECENT", ".", "UltimoMastro", edMastro.Text, " ", "NS.", "...", "...")
      oMenu.SaveSettingBusDitt(DittaCorrente, "BS--SOTC", "RECENT", ".", "FlPdc", IIf(ckPdc.Checked, "-1", "0").ToString, " ", "NS.", "...", "...")

      bOk = True
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      bOk = False
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub Zoom()
    Dim ctlLastControl As Control
    Dim ctrlTmp As Control = Nothing
    Dim oParam As New CLE__PATB

    Try
      'entro qui perchè nella FRM__HLAN_KeyDown ho inserito il seguente codice:
      'If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
      '  Zoom()
      '  e.Handled = True
      'End If

      ctlLastControl = NTSFindControlFocused(Me)
      If ctlLastControl Is Nothing Then Return

      If edMastro.Focused Then
        '----------------------------------------------
        'zoom specifico per mastri di contabilità
        oParam.strCodPdc = oCleSotc.strPDC     'passo il piano dei conti
        SetFastZoom(edMastro.Text, oParam) 'gestione dello zoom veloce
        NTSZOOM.strIn = edMastro.Text
        NTSZOOM.ZoomStrIn("ZOOMTABMAST", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edMastro.Text Then edMastro.Text = NTSZOOM.strIn
      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        'SendKeys.SendWait("{F5}") 'se faccio questa riga va in un loop infinito....
        NTSCallStandardZoom()
      End If
      '------------------------------------
      'riporto il focus sul controllo che ha chiamato lo zoom
      ctlLastControl.Focus()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edMastro_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles edMastro.NTSZoomGest
    Try
      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edMastro_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edMastro.Validated
    Dim strTmp As String = ""
    Dim lConto As Integer = 0
    Try
      If Not oCleSotc.FRM__NUOA_edMastro_Validated(NTSCInt(edMastro.Text), strTmp, lConto) Then
        edMastro.Text = "0"
        edConto.Text = "0"
      Else
        lbMastro.Text = strTmp
        edConto.Text = lConto.ToString
      End If
      oCleSotc.bNuovoContoProposto = True

      lContoPrec = NTSCInt(edConto.Text)

      RitornaProgressivo()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edConto_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edConto.Enter
    Dim strTmp As String = ""
    Try
      Select Case oCleSotc.strStrutturaPDC
        Case "A" : edConto.nMaxLen = 4
        Case "B" : edConto.nMaxLen = 6
        Case "C" : edConto.nMaxLen = 6
        Case "D" : edConto.nMaxLen = 5
        Case "S" : edConto.nMaxLen = 4
      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edConto_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edConto.ValidatedAndChanged
    Try
      If (lContoPrec <> NTSCInt(edConto.Text)) And (lContoPrec <> 0) Then oCleSotc.bNuovoContoProposto = False
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckPdc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckPdc.CheckedChanged
    Try
      '-------------------------------------------------------------------------------------------------------------
      RitornaProgressivo()
      '-------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub RitornaProgressivo()
    Try
      '-------------------------------------------------------------------------------------------------------------
      If NTSCInt(edMastro.Text) = 0 Then Return
      '-------------------------------------------------------------------------------------------------------------
      edConto.Text = oCleSotc.RitornaProgressivo(NTSCInt(edMastro.Text), ckPdc.Checked).ToString
      '-------------------------------------------------------------------------------------------------------------
      Select Case oCleSotc.strPDC.ToLower
        Case "standard-az", "standard-pr", "standard-cna", "standard-asm", "standard-psm", "standard-xxx"
          If oApp.User.Nome.ToUpper <> "NTS" Or oApp.User.Pwd.ToUpper <> "NTS" Then
            If NTSCInt(edConto.Text) <= 5000 Then edConto.Text = "5001"
          End If
      End Select
      '-------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class
