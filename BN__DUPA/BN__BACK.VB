Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__BACK

  Public oCleDupa As CLE__DUPA
  Public oCallParams As CLE__CLDP
  Private components As System.ComponentModel.IContainer
  Public WithEvents pnImpo As NTSInformatica.NTSPanel
  Public WithEvents ceBackup As NTSInformatica.BN__SQLB

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Me.pnImpo = New NTSInformatica.NTSPanel
    Me.ceBackup = New NTSInformatica.BN__SQLB
    CType(Me.pnImpo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnImpo.SuspendLayout()
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
    'pnImpo
    '
    Me.pnImpo.AllowDrop = True
    Me.pnImpo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnImpo.Appearance.Options.UseBackColor = True
    Me.pnImpo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnImpo.Controls.Add(Me.ceBackup)
    Me.pnImpo.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnImpo.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnImpo.Location = New System.Drawing.Point(0, 0)
    Me.pnImpo.Name = "pnImpo"
    Me.pnImpo.Size = New System.Drawing.Size(533, 464)
    Me.pnImpo.TabIndex = 0
    Me.pnImpo.Text = "NtsPanel1"
    '
    'ceBackup
    '
    Me.ceBackup.Location = New System.Drawing.Point(3, 3)
    Me.ceBackup.MinimumSize = New System.Drawing.Size(527, 462)
    Me.ceBackup.Name = "ceBackup"
    Me.ceBackup.Size = New System.Drawing.Size(527, 462)
    Me.ceBackup.TabIndex = 0
    '
    'FRM__BACK
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(533, 464)
    Me.Controls.Add(Me.pnImpo)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.HelpContext = "bs--impo.htm"
    Me.LookAndFeel.SkinName = "Money Twins"
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__BACK"
    Me.Text = "GESTIONE BACKUP DATABASE"
    CType(Me.pnImpo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnImpo.ResumeLayout(False)
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
  Public Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
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

  Public Overridable Sub FRM__BACK_ActivatedFirst(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ActivatedFirst
    Try
      Me.Text = UCase(oApp.Tr(Me, 129024303086239914, "GESTIONE BACKUP DATABASE"))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub FRM__BACK_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      InitControls()
      GctlSetRoules()

      'parametri passati da DUPA
      If Not oCallParams Is Nothing Then
        ceBackup.edServer.Text = oCallParams.strPar1
        If oCallParams.bPar1 Then
          ceBackup.opTrusted.Checked = True
        Else
          ceBackup.opSQL.Checked = True
        End If
        ceBackup.bFriendly = FRIENDLY
        ceBackup.strAscDir = oApp.AscDir
        ceBackup.edUser.Text = oCallParams.strPar2
        ceBackup.edPwd.Text = oCallParams.strPar3
        ceBackup.cmdConnetti_Click(ceBackup.cmdConnetti, Nothing)
        ceBackup.cbDB.Text = oCallParams.strPar4
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class