Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__DTRI
  Public oCallParams As CLE__CLDP
  Public oCleTric As CLE__TRIC

#Region "Inizializzazione"
  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    '----------------------------------------------------------------------------------------------------------------
    oMenu = Menu
    oApp = oMenu.App
    oCallParams = Param
    '----------------------------------------------------------------------------------------------------------------
    If Ditta <> "" Then DittaCorrente = Ditta Else DittaCorrente = oApp.Ditta
    '----------------------------------------------------------------------------------------------------------------
    Me.GctlTipoDoc = ""
    '----------------------------------------------------------------------------------------------------------------
    InitializeComponent()
    Me.MinimumSize = Me.Size
    '----------------------------------------------------------------------------------------------------------------
    Return True
  End Function

  Private Sub InitializeComponent()
    Me.pnPulsanti = New NTSInformatica.NTSPanel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.lbDescr = New NTSInformatica.NTSLabel
    Me.edCodtric = New NTSInformatica.NTSTextBoxNum
    Me.lbCodtric = New NTSInformatica.NTSLabel
    Me.pnControlli = New NTSInformatica.NTSPanel
    Me.edDestric = New NTSInformatica.NTSTextBoxStr
    Me.edPdc = New NTSInformatica.NTSTextBoxStr
    Me.lbPdc = New NTSInformatica.NTSLabel
    Me.opCopia3 = New NTSInformatica.NTSRadioButton
    Me.opCopia2 = New NTSInformatica.NTSRadioButton
    Me.opCopia1 = New NTSInformatica.NTSRadioButton
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnPulsanti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPulsanti.SuspendLayout()
    CType(Me.edCodtric.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnControlli, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnControlli.SuspendLayout()
    CType(Me.edDestric.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPdc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opCopia3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opCopia2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opCopia1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'pnPulsanti
    '
    Me.pnPulsanti.AllowDrop = True
    Me.pnPulsanti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPulsanti.Appearance.Options.UseBackColor = True
    Me.pnPulsanti.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPulsanti.Controls.Add(Me.cmdAnnulla)
    Me.pnPulsanti.Controls.Add(Me.cmdOk)
    Me.pnPulsanti.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPulsanti.Location = New System.Drawing.Point(674, 1)
    Me.pnPulsanti.Name = "pnPulsanti"
    Me.pnPulsanti.NTSActiveTrasparency = True
    Me.pnPulsanti.Size = New System.Drawing.Size(150, 82)
    Me.pnPulsanti.TabIndex = 10
    Me.pnPulsanti.Text = "NtsPanel1"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(3, 43)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(139, 26)
    Me.cmdAnnulla.TabIndex = 3
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(3, 11)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.NTSContextMenu = Nothing
    Me.cmdOk.Size = New System.Drawing.Size(139, 26)
    Me.cmdOk.TabIndex = 4
    Me.cmdOk.Text = "&Conferma"
    '
    'lbDescr
    '
    Me.lbDescr.BackColor = System.Drawing.Color.Transparent
    Me.lbDescr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescr.Location = New System.Drawing.Point(12, 11)
    Me.lbDescr.Name = "lbDescr"
    Me.lbDescr.NTSDbField = ""
    Me.lbDescr.Size = New System.Drawing.Size(647, 20)
    Me.lbDescr.TabIndex = 52
    Me.lbDescr.Tooltip = ""
    Me.lbDescr.UseMnemonic = False
    '
    'edCodtric
    '
    Me.edCodtric.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodtric.EditValue = "0"
    Me.edCodtric.Location = New System.Drawing.Point(161, 40)
    Me.edCodtric.Name = "edCodtric"
    Me.edCodtric.NTSDbField = ""
    Me.edCodtric.NTSFormat = "0"
    Me.edCodtric.NTSForzaVisZoom = False
    Me.edCodtric.NTSOldValue = "0"
    Me.edCodtric.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCodtric.Properties.Appearance.Options.UseBackColor = True
    Me.edCodtric.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodtric.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodtric.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodtric.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodtric.Properties.AutoHeight = False
    Me.edCodtric.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodtric.Properties.MaxLength = 65536
    Me.edCodtric.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodtric.Size = New System.Drawing.Size(55, 20)
    Me.edCodtric.TabIndex = 53
    Me.edCodtric.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCodtric.TextInt = 0
    '
    'lbCodtric
    '
    Me.lbCodtric.AutoSize = True
    Me.lbCodtric.BackColor = System.Drawing.Color.Transparent
    Me.lbCodtric.Location = New System.Drawing.Point(9, 43)
    Me.lbCodtric.Name = "lbCodtric"
    Me.lbCodtric.NTSDbField = ""
    Me.lbCodtric.Size = New System.Drawing.Size(146, 13)
    Me.lbCodtric.TabIndex = 54
    Me.lbCodtric.Text = "Nuovo tipo di riclassificazione"
    Me.lbCodtric.Tooltip = ""
    Me.lbCodtric.UseMnemonic = False
    '
    'pnControlli
    '
    Me.pnControlli.AllowDrop = True
    Me.pnControlli.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnControlli.Appearance.Options.UseBackColor = True
    Me.pnControlli.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnControlli.Controls.Add(Me.edPdc)
    Me.pnControlli.Controls.Add(Me.lbPdc)
    Me.pnControlli.Controls.Add(Me.opCopia3)
    Me.pnControlli.Controls.Add(Me.opCopia2)
    Me.pnControlli.Controls.Add(Me.opCopia1)
    Me.pnControlli.Controls.Add(Me.edDestric)
    Me.pnControlli.Controls.Add(Me.lbDescr)
    Me.pnControlli.Controls.Add(Me.edCodtric)
    Me.pnControlli.Controls.Add(Me.lbCodtric)
    Me.pnControlli.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnControlli.Location = New System.Drawing.Point(0, 1)
    Me.pnControlli.Name = "pnControlli"
    Me.pnControlli.NTSActiveTrasparency = True
    Me.pnControlli.Size = New System.Drawing.Size(668, 168)
    Me.pnControlli.TabIndex = 55
    Me.pnControlli.Text = "NtsPanel1"
    '
    'edDestric
    '
    Me.edDestric.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDestric.EditValue = " "
    Me.edDestric.Location = New System.Drawing.Point(222, 40)
    Me.edDestric.Name = "edDestric"
    Me.edDestric.NTSDbField = ""
    Me.edDestric.NTSForzaVisZoom = False
    Me.edDestric.NTSOldValue = " "
    Me.edDestric.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDestric.Properties.Appearance.Options.UseBackColor = True
    Me.edDestric.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDestric.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDestric.Properties.AutoHeight = False
    Me.edDestric.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDestric.Properties.MaxLength = 1
    Me.edDestric.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDestric.Size = New System.Drawing.Size(437, 20)
    Me.edDestric.TabIndex = 55
    Me.edDestric.TextStr = " "
    '
    'edPdc
    '
    Me.edPdc.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edPdc.EditValue = ""
    Me.edPdc.Location = New System.Drawing.Point(160, 135)
    Me.edPdc.Name = "edPdc"
    Me.edPdc.NTSDbField = ""
    Me.edPdc.NTSForzaVisZoom = False
    Me.edPdc.NTSOldValue = ""
    Me.edPdc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPdc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPdc.Properties.AutoHeight = False
    Me.edPdc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPdc.Properties.MaxLength = 65536
    Me.edPdc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPdc.Size = New System.Drawing.Size(206, 20)
    Me.edPdc.TabIndex = 509
    Me.edPdc.TextStr = ""
    '
    'lbPdc
    '
    Me.lbPdc.AutoSize = True
    Me.lbPdc.BackColor = System.Drawing.Color.Transparent
    Me.lbPdc.Location = New System.Drawing.Point(11, 138)
    Me.lbPdc.Name = "lbPdc"
    Me.lbPdc.NTSDbField = ""
    Me.lbPdc.Size = New System.Drawing.Size(73, 13)
    Me.lbPdc.TabIndex = 508
    Me.lbPdc.Text = "PDC di origine"
    Me.lbPdc.Tooltip = ""
    Me.lbPdc.UseMnemonic = False
    '
    'opCopia3
    '
    Me.opCopia3.Location = New System.Drawing.Point(11, 111)
    Me.opCopia3.Name = "opCopia3"
    Me.opCopia3.NTSCheckValue = "S"
    Me.opCopia3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opCopia3.Properties.Appearance.Options.UseBackColor = True
    Me.opCopia3.Properties.AutoHeight = False
    Me.opCopia3.Properties.Caption = "Copia i riferimenti da quelli RICLASSIFICATI del PDC sotto specificato"
    Me.opCopia3.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opCopia3.Size = New System.Drawing.Size(366, 17)
    Me.opCopia3.TabIndex = 507
    '
    'opCopia2
    '
    Me.opCopia2.Location = New System.Drawing.Point(12, 88)
    Me.opCopia2.Name = "opCopia2"
    Me.opCopia2.NTSCheckValue = "S"
    Me.opCopia2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opCopia2.Properties.Appearance.Options.UseBackColor = True
    Me.opCopia2.Properties.AutoHeight = False
    Me.opCopia2.Properties.Caption = "Copia i riferimenti da quelli CEE del PDC sotto specificato"
    Me.opCopia2.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opCopia2.Size = New System.Drawing.Size(314, 17)
    Me.opCopia2.TabIndex = 506
    '
    'opCopia1
    '
    Me.opCopia1.EditValue = True
    Me.opCopia1.Location = New System.Drawing.Point(12, 65)
    Me.opCopia1.Name = "opCopia1"
    Me.opCopia1.NTSCheckValue = "S"
    Me.opCopia1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opCopia1.Properties.Appearance.Options.UseBackColor = True
    Me.opCopia1.Properties.AutoHeight = False
    Me.opCopia1.Properties.Caption = "Copia i riferimenti da quelli contenuti nel riclassificato di partenza"
    Me.opCopia1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opCopia1.Size = New System.Drawing.Size(341, 17)
    Me.opCopia1.TabIndex = 505
    '
    'FRM__DTRI
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(824, 170)
    Me.Controls.Add(Me.pnControlli)
    Me.Controls.Add(Me.pnPulsanti)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__DTRI"
    Me.Text = "NUOVO TIPO DI RICLASSIFICAZIONE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPulsanti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPulsanti.ResumeLayout(False)
    CType(Me.edCodtric.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnControlli, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnControlli.ResumeLayout(False)
    Me.pnControlli.PerformLayout()
    CType(Me.edDestric.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPdc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opCopia3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opCopia2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opCopia1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, False)
    '----------------------------------------------------------------------------------------------------------------
    Try
      '--------------------------------------------------------------------------------------------------------------
      edCodtric.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023057311727, "Nuovo tipo di riclassificazione"), tabtric)
      edDestric.NTSSetParam(oMenu, oApp.Tr(Me, 128230023057467900, "Descrizione"), 60, True)

      edPdc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130553318756744384, "Codice PDC"), tabpcon, True)
      opCopia1.NTSSetParam(oMenu, oApp.Tr(Me, 130553319937551411, "Copia i riferimenti da quelli contenuti nel riclassificato di partenza"), "1")
      opCopia2.NTSSetParam(oMenu, oApp.Tr(Me, 130553320145610018, "Copia i riferimenti da quelli CEE del PDC sotto specificato"), "2")
      opCopia3.NTSSetParam(oMenu, oApp.Tr(Me, 130553320169699816, "Copia i riferimenti da quelli RICLASSIFICATI del PDC sotto specificato"), "3")
      '--------------------------------------------------------------------------------------------------------------
      edCodtric.NTSSetParamZoom("")
      '--------------------------------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, True)
    '----------------------------------------------------------------------------------------------------------------
  End Sub

  Public Overridable Sub InitEntity(ByRef cleTric As CLE__TRIC)
    oCleTric = cleTric
    AddHandler oCleTric.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRM__DTRI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '--------------------------------------------------------------------------------------------------------------
      oCleTric.bAnnullato = True
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      lbDescr.Text = "Duplicazione tipo riclassificazione: '" & oCleTric.strOldDestric.ToString & " - " & oCleTric.strOldDestric & "'"
      AbilitaPdc()
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
      GctlApplicaDefaultValue()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi Pulsanti"
  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edCodtric.Text) = 0 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 129676580977513233, "Attenzione!" & vbCrLf & _
          "Indicare un codice riclassificazione valido."))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCleTric.bAllowOperation = False Then
        If (NTSCInt(edCodtric.Text) >= 1) And (NTSCInt(edCodtric.Text) <= 500) Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 129793929095486674, "Attenzione!" & vbCrLf & _
            "Codice Tipo riclassificazione non valido." & vbCrLf & _
            "E' consentito l'inserimento solo di quei codici NON compresi fra 1 e 500."))
          Return
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oMenu.ValCodiceDb(edCodtric.Text, DittaCorrente, "TABTRIC", "N") = True Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 129793930243421992, "Attenzione!" & vbCrLf & _
          "Codice Tipo riclassificazione già esistente."))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If edDestric.Text.Trim = "" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 129793932218022172, "Attenzione!" & vbCrLf & _
          "La descrizione è obbligatoria."))
        Return
      End If
      If opCopia1.Checked = False Then
        If edPdc.Text.Trim = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130553336196985555, "Indicare il codice PDC di riferimento"))
          Return
        End If
        If Not oMenu.ValCodiceDb(edPdc.Text, "", "TABPCON", "S", "") Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130553325052638802, "Codice PDC di riferimento inesistente"))
          Return
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127791958958281250, _
        "Confermare la creazione del nuovo tipo riclassificazione?")) = Windows.Forms.DialogResult.No Then Return
      '--------------------------------------------------------------------------------------------------------------
      oCleTric.nNewCodtric = NTSCInt(edCodtric.Text)
      oCleTric.strNewDestric = edDestric.Text
      oCleTric.bAnnullato = False
      '--------------------------------------------------------------------------------------------------------------
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Sub opCopia1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opCopia1.CheckedChanged
    Try
      AbilitaPdc()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opCopia2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opCopia2.CheckedChanged
    Try
      AbilitaPdc()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opCopia3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opCopia3.CheckedChanged
    Try
      AbilitaPdc()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function AbilitaPdc() As Boolean
    Try
      If opCopia1.Checked Then
        edPdc.Enabled = False
      Else
        GctlSetVisEnab(edPdc, False)
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

End Class