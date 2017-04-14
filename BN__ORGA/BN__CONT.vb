Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__CONT
  Public oCleOrga As CLE__ORGA
  Public oCallParams As CLE__CLDP
  Public dtrOrga As DataRow

  Private components As System.ComponentModel.IContainer

  Public Overridable Sub InitializeComponent()
    Me.opInterno = New NTSInformatica.NTSRadioButton
    Me.opConto = New NTSInformatica.NTSRadioButton
    Me.opLead = New NTSInformatica.NTSRadioButton
    Me.edConto = New NTSInformatica.NTSTextBoxNum
    Me.edLead = New NTSInformatica.NTSTextBoxNum
    Me.lbDesConto = New NTSInformatica.NTSLabel
    Me.lbDesLead = New NTSInformatica.NTSLabel
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdCreaAnagrafica = New NTSInformatica.NTSButton
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opInterno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opConto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opLead.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edLead.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'opInterno
    '
    Me.opInterno.EditValue = True
    Me.opInterno.Location = New System.Drawing.Point(11, 13)
    Me.opInterno.Name = "opInterno"
    Me.opInterno.NTSCheckValue = "S"
    Me.opInterno.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opInterno.Properties.Appearance.Options.UseBackColor = True
    Me.opInterno.Properties.AutoHeight = False
    Me.opInterno.Properties.Caption = "Organizzazione interna"
    Me.opInterno.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opInterno.Size = New System.Drawing.Size(142, 19)
    Me.opInterno.TabIndex = 4
    '
    'opConto
    '
    Me.opConto.Location = New System.Drawing.Point(11, 38)
    Me.opConto.Name = "opConto"
    Me.opConto.NTSCheckValue = "S"
    Me.opConto.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opConto.Properties.Appearance.Options.UseBackColor = True
    Me.opConto.Properties.AutoHeight = False
    Me.opConto.Properties.Caption = "Cliente\Fornitore"
    Me.opConto.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opConto.Size = New System.Drawing.Size(111, 19)
    Me.opConto.TabIndex = 4
    '
    'opLead
    '
    Me.opLead.Location = New System.Drawing.Point(11, 63)
    Me.opLead.Name = "opLead"
    Me.opLead.NTSCheckValue = "S"
    Me.opLead.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opLead.Properties.Appearance.Options.UseBackColor = True
    Me.opLead.Properties.AutoHeight = False
    Me.opLead.Properties.Caption = "Lead"
    Me.opLead.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opLead.Size = New System.Drawing.Size(55, 19)
    Me.opLead.TabIndex = 4
    '
    'edConto
    '
    Me.edConto.EditValue = "0"
    Me.edConto.Enabled = False
    Me.edConto.Location = New System.Drawing.Point(119, 38)
    Me.edConto.Name = "edConto"
    Me.edConto.NTSDbField = ""
    Me.edConto.NTSFormat = "0"
    Me.edConto.NTSForzaVisZoom = False
    Me.edConto.NTSOldValue = ""
    Me.edConto.Properties.Appearance.Options.UseTextOptions = True
    Me.edConto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edConto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edConto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edConto.Properties.AutoHeight = False
    Me.edConto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edConto.Properties.MaxLength = 65536
    Me.edConto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edConto.Size = New System.Drawing.Size(90, 20)
    Me.edConto.TabIndex = 5
    '
    'edLead
    '
    Me.edLead.EditValue = "0"
    Me.edLead.Enabled = False
    Me.edLead.Location = New System.Drawing.Point(152, 62)
    Me.edLead.Name = "edLead"
    Me.edLead.NTSDbField = ""
    Me.edLead.NTSFormat = "0"
    Me.edLead.NTSForzaVisZoom = False
    Me.edLead.NTSOldValue = ""
    Me.edLead.Properties.Appearance.Options.UseTextOptions = True
    Me.edLead.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edLead.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edLead.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edLead.Properties.AutoHeight = False
    Me.edLead.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edLead.Properties.MaxLength = 65536
    Me.edLead.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edLead.Size = New System.Drawing.Size(57, 20)
    Me.edLead.TabIndex = 5
    '
    'lbDesConto
    '
    Me.lbDesConto.BackColor = System.Drawing.Color.Transparent
    Me.lbDesConto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesConto.Location = New System.Drawing.Point(211, 38)
    Me.lbDesConto.Name = "lbDesConto"
    Me.lbDesConto.NTSDbField = ""
    Me.lbDesConto.Size = New System.Drawing.Size(201, 20)
    Me.lbDesConto.TabIndex = 6
    Me.lbDesConto.Tooltip = ""
    Me.lbDesConto.UseMnemonic = False
    '
    'lbDesLead
    '
    Me.lbDesLead.BackColor = System.Drawing.Color.Transparent
    Me.lbDesLead.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesLead.Location = New System.Drawing.Point(211, 62)
    Me.lbDesLead.Name = "lbDesLead"
    Me.lbDesLead.NTSDbField = ""
    Me.lbDesLead.Size = New System.Drawing.Size(201, 20)
    Me.lbDesLead.TabIndex = 6
    Me.lbDesLead.Tooltip = ""
    Me.lbDesLead.UseMnemonic = False
    '
    'cmdConferma
    '
    Me.cmdConferma.ImagePath = ""
    Me.cmdConferma.ImageText = ""
    Me.cmdConferma.Location = New System.Drawing.Point(419, 9)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.NTSContextMenu = Nothing
    Me.cmdConferma.Size = New System.Drawing.Size(75, 26)
    Me.cmdConferma.TabIndex = 7
    Me.cmdConferma.Text = "Conferma"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(419, 38)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(75, 26)
    Me.cmdAnnulla.TabIndex = 7
    Me.cmdAnnulla.Text = "Annulla"
    '
    'cmdCreaAnagrafica
    '
    Me.cmdCreaAnagrafica.Enabled = False
    Me.cmdCreaAnagrafica.ImagePath = ""
    Me.cmdCreaAnagrafica.ImageText = ""
    Me.cmdCreaAnagrafica.Location = New System.Drawing.Point(11, 88)
    Me.cmdCreaAnagrafica.Name = "cmdCreaAnagrafica"
    Me.cmdCreaAnagrafica.NTSContextMenu = Nothing
    Me.cmdCreaAnagrafica.Size = New System.Drawing.Size(198, 26)
    Me.cmdCreaAnagrafica.TabIndex = 8
    Me.cmdCreaAnagrafica.Text = "Crea l'anagrafica dalla persona"
    '
    'FRM__CONT
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(499, 122)
    Me.Controls.Add(Me.cmdCreaAnagrafica)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdConferma)
    Me.Controls.Add(Me.lbDesLead)
    Me.Controls.Add(Me.lbDesConto)
    Me.Controls.Add(Me.edLead)
    Me.Controls.Add(Me.edConto)
    Me.Controls.Add(Me.opLead)
    Me.Controls.Add(Me.opConto)
    Me.Controls.Add(Me.opInterno)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__CONT"
    Me.Text = "COMPLETAMENTO DATI PERSONA"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opInterno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opConto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opLead.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edLead.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

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

  Public Overridable Sub InitEntiry(ByVal CleOrga As CLE__ORGA)
    Try
      oCleOrga = CleOrga
      AddHandler oCleOrga.RemoteEvent, AddressOf GestisciEventiEntity
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      edConto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130675193521669852, "Conto"), tabanagrac)
      edLead.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130675193746893899, "Lead"), tableads)

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

#Region "Eventi Form"
  Public Overridable Sub FRM__CONT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      If Not oCleOrga.bIsUserCrm Then
        opLead.Visible = False
        edLead.Visible = False
        lbDesLead.Visible = False
      End If

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi"
  Public Overridable Sub edConto_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edConto.ValidatedAndChanged
    Try
      If edConto.TextInt = 0 Then edLead.TextInt = 0 : lbDesConto.Text = "" : Return

      Dim dttConto As New DataTable
      If oMenu.ValCodiceDb(edConto.Text, DittaCorrente, "ANAGRA", "N", , dttConto) Then
        Select Case NTSCStr(dttConto.Rows(0)!an_tipo)
          Case "S"
            oApp.MsgBoxErr(oApp.Tr(Me, 130675185452384356, "Conto non valido"))
            edConto.TextInt = 0
            Return
          Case "C"
            If oCleOrga.bModCRM Then 'Se è presente il modulo crm verifico se il cliente è un lead
              edLead.TextInt = oCleOrga.LeadCollegatoAConto(edConto.TextInt)
            End If
        End Select
        lbDesConto.Text = NTSCStr(dttConto.Rows(0)!an_descr1)
      Else
        oApp.MsgBoxErr(oApp.Tr(Me, 130675184450234691, "Conto non valido"))
        edConto.TextInt = 0
        Return
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edLead_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edLead.ValidatedAndChanged
    Try
      If edLead.TextInt = 0 Then edConto.TextInt = 0 : lbDesLead.Text = "" : Return

      Dim dttLead As New DataTable
      If oMenu.ValCodiceDb(edLead.Text, DittaCorrente, "LEADS", "N", , dttLead) Then
        lbDesLead.Text = NTSCStr(dttLead.Rows(0)!le_descr1)
        edConto.TextInt = NTSCInt(dttLead.Rows(0)!le_conto)
      Else
        oApp.MsgBoxErr(oApp.Tr(Me, 130675182722339044, "Lead non valido"))
        edLead.TextInt = 0
        Return
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub opConto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opConto.CheckedChanged
    Try
      edConto.Enabled = opConto.Checked
      edLead.TextInt = 0
      edConto.TextInt = 0
      cmdCreaAnagrafica.Enabled = opConto.Checked OrElse opLead.Checked
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opLead_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opLead.CheckedChanged
    Try
      edLead.Enabled = opLead.Checked
      edLead.TextInt = 0
      edConto.TextInt = 0
      cmdCreaAnagrafica.Enabled = opConto.Checked OrElse opLead.Checked
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      Me.DialogResult = Windows.Forms.DialogResult.Cancel
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    Try
      Me.DialogResult = Windows.Forms.DialogResult.OK
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

  Public Overridable Function ValoreRitorno() As String
    Try
      Select Case True
        Case opInterno.Checked : Return "0|0"
        Case opConto.Checked, opLead.Checked : Return edConto.TextInt & "|" & edLead.Text
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
    Return ""
  End Function

  Public Overridable Sub cmdCreaAnagrafica_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreaAnagrafica.Click
    Dim oPar As New CLE__CLDP
    Dim strClasse, strDll As String
    Try
      Select Case True
        Case opConto.Checked
          strClasse = "FRM__CLIE"
          strDll = "BN__CLIE"
        Case opLead.Checked
          strClasse = "FRMCRLEAD"
          strDll = "BNCRLEAD"
        Case Else : Return
      End Select

      oPar.ctlPar1 = dtrOrga

      oMenu.RunChild("NTSInformatica", strClasse, "", DittaCorrente, "", strDll, oPar, "", True, True)

      Select Case True
        Case opConto.Checked
          edConto.TextInt = NTSCInt(oPar.dPar1)
        Case opLead.Checked
          edLead.TextInt = NTSCInt(oPar.dPar1)
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
End Class

