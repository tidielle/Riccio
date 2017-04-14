Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__ANA2
  Public oCleIstf As CLE__ISTF
  Public oCallParams As CLE__CLDP

  Public bAnnullato As Boolean = True
  Public nEscomp As Integer = 0
  Public nSaldo As Integer = 0
  Public nMovimentato As Integer = 0

  Private components As System.ComponentModel.IContainer

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
    Me.pnButtons = New NTSInformatica.NTSPanel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.pnControlli = New NTSInformatica.NTSPanel
    Me.lbEscomp = New NTSInformatica.NTSLabel
    Me.cbSaldo = New NTSInformatica.NTSComboBox
    Me.lbSaldo = New NTSInformatica.NTSLabel
    Me.lbDesescomp = New NTSInformatica.NTSLabel
    Me.cbMovimentato = New NTSInformatica.NTSComboBox
    Me.lbMovimentato = New NTSInformatica.NTSLabel
    Me.edEscomp = New NTSInformatica.NTSTextBoxNum
    CType(Me.pnButtons, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnButtons.SuspendLayout()
    CType(Me.pnControlli, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnControlli.SuspendLayout()
    CType(Me.cbSaldo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbMovimentato.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEscomp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'pnButtons
    '
    Me.pnButtons.AllowDrop = True
    Me.pnButtons.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnButtons.Appearance.Options.UseBackColor = True
    Me.pnButtons.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnButtons.Controls.Add(Me.cmdAnnulla)
    Me.pnButtons.Controls.Add(Me.cmdOk)
    Me.pnButtons.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnButtons.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnButtons.Location = New System.Drawing.Point(500, 0)
    Me.pnButtons.Name = "pnButtons"
    Me.pnButtons.Size = New System.Drawing.Size(140, 103)
    Me.pnButtons.TabIndex = 3
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(3, 48)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(131, 24)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(3, 23)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(131, 24)
    Me.cmdOk.TabIndex = 0
    Me.cmdOk.Text = "&Conferma"
    '
    'pnControlli
    '
    Me.pnControlli.AllowDrop = True
    Me.pnControlli.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnControlli.Appearance.Options.UseBackColor = True
    Me.pnControlli.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnControlli.Controls.Add(Me.edEscomp)
    Me.pnControlli.Controls.Add(Me.cbMovimentato)
    Me.pnControlli.Controls.Add(Me.lbMovimentato)
    Me.pnControlli.Controls.Add(Me.lbDesescomp)
    Me.pnControlli.Controls.Add(Me.cbSaldo)
    Me.pnControlli.Controls.Add(Me.lbSaldo)
    Me.pnControlli.Controls.Add(Me.lbEscomp)
    Me.pnControlli.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnControlli.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnControlli.Location = New System.Drawing.Point(0, 0)
    Me.pnControlli.Name = "pnControlli"
    Me.pnControlli.Size = New System.Drawing.Size(500, 103)
    Me.pnControlli.TabIndex = 4
    '
    'lbEscomp
    '
    Me.lbEscomp.AutoSize = True
    Me.lbEscomp.BackColor = System.Drawing.Color.Transparent
    Me.lbEscomp.Location = New System.Drawing.Point(12, 26)
    Me.lbEscomp.Name = "lbEscomp"
    Me.lbEscomp.NTSDbField = ""
    Me.lbEscomp.Size = New System.Drawing.Size(120, 13)
    Me.lbEscomp.TabIndex = 158
    Me.lbEscomp.Text = "Esercizio di competenza"
    Me.lbEscomp.Tooltip = ""
    Me.lbEscomp.UseMnemonic = False
    '
    'cbSaldo
    '
    Me.cbSaldo.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.cbSaldo.DataSource = Nothing
    Me.cbSaldo.DisplayMember = ""
    Me.cbSaldo.Location = New System.Drawing.Point(174, 45)
    Me.cbSaldo.Name = "cbSaldo"
    Me.cbSaldo.NTSDbField = ""
    Me.cbSaldo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbSaldo.Properties.DropDownRows = 30
    Me.cbSaldo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbSaldo.SelectedValue = ""
    Me.cbSaldo.Size = New System.Drawing.Size(217, 20)
    Me.cbSaldo.TabIndex = 161
    Me.cbSaldo.ValueMember = ""
    '
    'lbSaldo
    '
    Me.lbSaldo.AutoSize = True
    Me.lbSaldo.BackColor = System.Drawing.Color.Transparent
    Me.lbSaldo.Location = New System.Drawing.Point(12, 48)
    Me.lbSaldo.Name = "lbSaldo"
    Me.lbSaldo.NTSDbField = ""
    Me.lbSaldo.Size = New System.Drawing.Size(70, 13)
    Me.lbSaldo.TabIndex = 160
    Me.lbSaldo.Text = "Test su saldo"
    Me.lbSaldo.Tooltip = ""
    Me.lbSaldo.UseMnemonic = False
    '
    'lbDesescomp
    '
    Me.lbDesescomp.BackColor = System.Drawing.Color.Transparent
    Me.lbDesescomp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesescomp.Location = New System.Drawing.Point(231, 23)
    Me.lbDesescomp.Name = "lbDesescomp"
    Me.lbDesescomp.NTSDbField = ""
    Me.lbDesescomp.Size = New System.Drawing.Size(254, 21)
    Me.lbDesescomp.TabIndex = 162
    Me.lbDesescomp.Tooltip = ""
    Me.lbDesescomp.UseMnemonic = False
    '
    'cbMovimentato
    '
    Me.cbMovimentato.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.cbMovimentato.DataSource = Nothing
    Me.cbMovimentato.DisplayMember = ""
    Me.cbMovimentato.Location = New System.Drawing.Point(174, 67)
    Me.cbMovimentato.Name = "cbMovimentato"
    Me.cbMovimentato.NTSDbField = ""
    Me.cbMovimentato.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbMovimentato.Properties.DropDownRows = 30
    Me.cbMovimentato.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbMovimentato.SelectedValue = ""
    Me.cbMovimentato.Size = New System.Drawing.Size(217, 20)
    Me.cbMovimentato.TabIndex = 164
    Me.cbMovimentato.ValueMember = ""
    '
    'lbMovimentato
    '
    Me.lbMovimentato.AutoSize = True
    Me.lbMovimentato.BackColor = System.Drawing.Color.Transparent
    Me.lbMovimentato.Location = New System.Drawing.Point(12, 70)
    Me.lbMovimentato.Name = "lbMovimentato"
    Me.lbMovimentato.NTSDbField = ""
    Me.lbMovimentato.Size = New System.Drawing.Size(137, 13)
    Me.lbMovimentato.TabIndex = 163
    Me.lbMovimentato.Text = "Test su conto movimentato"
    Me.lbMovimentato.Tooltip = ""
    Me.lbMovimentato.UseMnemonic = False
    '
    'edEscomp
    '
    Me.edEscomp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEscomp.EditValue = "0"
    Me.edEscomp.Location = New System.Drawing.Point(174, 23)
    Me.edEscomp.Name = "edEscomp"
    Me.edEscomp.NTSDbField = ""
    Me.edEscomp.NTSFormat = "0"
    Me.edEscomp.NTSForzaVisZoom = False
    Me.edEscomp.NTSOldValue = ""
    Me.edEscomp.Properties.Appearance.Options.UseTextOptions = True
    Me.edEscomp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEscomp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEscomp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEscomp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEscomp.Properties.MaxLength = 65536
    Me.edEscomp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEscomp.Size = New System.Drawing.Size(51, 20)
    Me.edEscomp.TabIndex = 165
    '
    'FRM__ANA2
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(640, 103)
    Me.Controls.Add(Me.pnControlli)
    Me.Controls.Add(Me.pnButtons)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__ANA2"
    Me.Text = "FILTRI SU DATI CONTABILI"
    CType(Me.pnButtons, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnButtons.ResumeLayout(False)
    CType(Me.pnControlli, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnControlli.ResumeLayout(False)
    Me.pnControlli.PerformLayout()
    CType(Me.cbSaldo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbMovimentato.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEscomp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, False)
    '----------------------------------------------------------------------------------------------------------------
    Try
      '--------------------------------------------------------------------------------------------------------------
      edEscomp.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426229062500, "Esercizio di competenza"), tabesco)
      cbSaldo.NTSSetParam(oApp.Tr(Me, 128756214849590060, "Test su saldo"))
      cbMovimentato.NTSSetParam(oApp.Tr(Me, 129717860310274914, "Test conto movimentato"))
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

  Public Overridable Sub InitEntity(ByVal cleIstf As CLE__ISTF)
    oCleIstf = cleIstf
    AddHandler oCleIstf.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRM__ANA2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      CaricaCombo()
      '--------------------------------------------------------------------------------------------------------------
      edEscomp.Text = nEscomp.ToString
      lbDesescomp.Text = ""
      If oMenu.ValCodiceDb(NTSCStr(edEscomp.Text), DittaCorrente, "tabesco", "N", strTmp) = True Then
        lbDesescomp.Text = strTmp
      End If
      cbSaldo.SelectedValue = nSaldo.ToString
      cbMovimentato.SelectedValue = nMovimentato.ToString
      '--------------------------------------------------------------------------------------------------------------
      If oCleIstf.nPf_codquery = 3 Then
        cbSaldo.Enabled = False
        cbMovimentato.Enabled = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
      GctlApplicaDefaultValue()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi TextBox"
  Public Overridable Sub edEscomp_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edEscomp.Validated
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------      
      If oMenu.ValCodiceDb(NTSCStr(edEscomp.Text), DittaCorrente, "tabesco", "N", strTmp) = True Then
        lbDesescomp.Text = strTmp
      Else
        lbDesescomp.Text = ""
      End If
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
      nEscomp = NTSCInt(edEscomp.Text)
      nSaldo = NTSCInt(cbSaldo.SelectedValue)
      nMovimentato = NTSCInt(cbMovimentato.SelectedValue)
      '--------------------------------------------------------------------------------------------------------------
      bAnnullato = False
      '--------------------------------------------------------------------------------------------------------------
      Me.Close()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      bAnnullato = True
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Sub CaricaCombo()
    Dim dttSaldo As New DataTable()
    Dim dttMovimentato As New DataTable()

    Try
      '--------------------------------------------------------------------------------------------------------------
      dttSaldo.Columns.Add("cod", GetType(Integer))
      dttSaldo.Columns.Add("val", GetType(String))
      dttSaldo.Rows.Add(New Object() {"0", "(Nessuno)"})
      dttSaldo.Rows.Add(New Object() {"1", "Solo saldo dare"})
      dttSaldo.Rows.Add(New Object() {"2", "Solo saldo avere"})
      dttSaldo.Rows.Add(New Object() {"3", "Solo saldo = 0"})
      dttSaldo.Rows.Add(New Object() {"4", "Solo saldo <> 0"})
      dttSaldo.Rows.Add(New Object() {"5", "Solo saldo dare e = 0"})
      dttSaldo.Rows.Add(New Object() {"6", "Solo saldo avere e = 0"})
      dttSaldo.AcceptChanges()
      cbSaldo.DataSource = dttSaldo
      cbSaldo.ValueMember = "cod"
      cbSaldo.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
      dttMovimentato.Columns.Add("cod", GetType(Integer))
      dttMovimentato.Columns.Add("val", GetType(String))
      dttMovimentato.Rows.Add(New Object() {"0", "(Nessun test)"})
      dttMovimentato.Rows.Add(New Object() {"1", "Solo movimentato"})
      dttMovimentato.Rows.Add(New Object() {"2", "Solo non movimentato"})
      dttMovimentato.AcceptChanges()
      cbMovimentato.DataSource = dttMovimentato
      cbMovimentato.ValueMember = "cod"
      cbMovimentato.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class