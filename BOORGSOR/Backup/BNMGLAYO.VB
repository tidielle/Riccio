Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGLAYO
  Public oCallParams As CLE__CLDP
  Public oCleGsor As CLEORGSOR

#Region "Inizializzazione"
  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    oMenu = Menu
    oApp = oMenu.App
    oCallParams = Param
    If Ditta <> "" Then DittaCorrente = Ditta Else DittaCorrente = oApp.Ditta
    Me.GctlTipoDoc = ""
    InitializeComponent()
    Me.MinimumSize = Me.Size
    Return True
  End Function

  Private Sub InitializeComponent()
    Me.pnPulsanti = New NTSInformatica.NTSPanel
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.pnControlli = New NTSInformatica.NTSPanel
    Me.cbLayout = New NTSInformatica.NTSComboBox
    Me.lbLayout = New NTSInformatica.NTSLabel
    CType(Me.pnPulsanti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPulsanti.SuspendLayout()
    CType(Me.pnControlli, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnControlli.SuspendLayout()
    CType(Me.cbLayout.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.pnPulsanti.Controls.Add(Me.cmdOk)
    Me.pnPulsanti.Controls.Add(Me.cmdAnnulla)
    Me.pnPulsanti.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPulsanti.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnPulsanti.Location = New System.Drawing.Point(376, 0)
    Me.pnPulsanti.Name = "pnPulsanti"
    Me.pnPulsanti.NTSActiveTrasparency = True
    Me.pnPulsanti.Size = New System.Drawing.Size(137, 60)
    Me.pnPulsanti.TabIndex = 7
    Me.pnPulsanti.Text = "NtsPanel1"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(3, 7)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.NTSContextMenu = Nothing
    Me.cmdOk.Size = New System.Drawing.Size(130, 23)
    Me.cmdOk.TabIndex = 2
    Me.cmdOk.Text = "&Conferma"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(3, 32)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(130, 23)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'pnControlli
    '
    Me.pnControlli.AllowDrop = True
    Me.pnControlli.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnControlli.Appearance.Options.UseBackColor = True
    Me.pnControlli.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnControlli.Controls.Add(Me.cbLayout)
    Me.pnControlli.Controls.Add(Me.lbLayout)
    Me.pnControlli.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnControlli.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnControlli.Location = New System.Drawing.Point(0, 0)
    Me.pnControlli.Name = "pnControlli"
    Me.pnControlli.NTSActiveTrasparency = True
    Me.pnControlli.Size = New System.Drawing.Size(376, 60)
    Me.pnControlli.TabIndex = 8
    Me.pnControlli.Text = "NtsPanel1"
    '
    'cbLayout
    '
    Me.cbLayout.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbLayout.DataSource = Nothing
    Me.cbLayout.DisplayMember = ""
    Me.cbLayout.Location = New System.Drawing.Point(130, 21)
    Me.cbLayout.Name = "cbLayout"
    Me.cbLayout.NTSDbField = ""
    Me.cbLayout.Properties.AutoHeight = False
    Me.cbLayout.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbLayout.Properties.DropDownRows = 30
    Me.cbLayout.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbLayout.SelectedValue = ""
    Me.cbLayout.Size = New System.Drawing.Size(222, 20)
    Me.cbLayout.TabIndex = 515
    Me.cbLayout.ValueMember = ""
    '
    'lbLayout
    '
    Me.lbLayout.AutoSize = True
    Me.lbLayout.BackColor = System.Drawing.Color.Transparent
    Me.lbLayout.Location = New System.Drawing.Point(14, 24)
    Me.lbLayout.Name = "lbLayout"
    Me.lbLayout.NTSDbField = ""
    Me.lbLayout.Size = New System.Drawing.Size(104, 13)
    Me.lbLayout.TabIndex = 514
    Me.lbLayout.Text = "Layout da importare"
    Me.lbLayout.Tooltip = ""
    Me.lbLayout.UseMnemonic = False
    '
    'FRMMGLAYO
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(513, 60)
    Me.Controls.Add(Me.pnControlli)
    Me.Controls.Add(Me.pnPulsanti)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMMGLAYO"
    Me.Text = "SELEZIONE LAYOUT"
    CType(Me.pnPulsanti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPulsanti.ResumeLayout(False)
    CType(Me.pnControlli, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnControlli.ResumeLayout(False)
    Me.pnControlli.PerformLayout()
    CType(Me.cbLayout.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, False)
    '----------------------------------------------------------------------------------------------------------------
    Dim dttLayout As New DataTable()
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      cbLayout.NTSSetParam(oApp.Tr(Me, 128759950861372722, "Layout da importare"))
      '--------------------------------------------------------------------------------------------------------------
      dttLayout.Columns.Add("cod", GetType(String))
      dttLayout.Columns.Add("val", GetType(String))
      dttLayout.Rows.Add(New Object() {"Terminale", "(Predefinito)"})
      If oCleGsor.GetImpostazioniFile("BSORGSOR", dttTmp) = True Then
        For i As Integer = 0 To (dttTmp.Rows.Count - 1)
          With dttTmp.Rows(i)
            dttLayout.Rows.Add(New Object() {NTSCStr(!rp_liv2), Mid(NTSCStr(!rp_liv2), 11)})
          End With
        Next
      End If
      dttLayout.AcceptChanges()
      cbLayout.DataSource = dttLayout
      cbLayout.ValueMember = "cod"
      cbLayout.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, True)
    '----------------------------------------------------------------------------------------------------------------
  End Sub

  Public Overridable Sub InitEntity(ByRef cleGsor As CLEORGSOR)
    oCleGsor = cleGsor
    AddHandler oCleGsor.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRMMGLAYO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      oCleGsor.bLayoAnnullato = True
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
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
      oCleGsor.strTermKey = cbLayout.SelectedValue
      oCleGsor.bLayoAnnullato = False
      '--------------------------------------------------------------------------------------------------------------
      Me.Close()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.Close()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

End Class