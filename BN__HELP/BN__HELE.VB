Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__HELE

#Region "Variabili"

  Private components As System.ComponentModel.IContainer
  Public oPar As CLE__CLDP
  Public WithEvents cmdElencoPuntato As NTSInformatica.NTSButton
  Public WithEvents cmdElencoNum As NTSInformatica.NTSButton
  Public WithEvents lbElencoPuntato As System.Windows.Forms.Label
  Public WithEvents pnHele As NTSInformatica.NTSPanel
  Public WithEvents lbElencoNum As System.Windows.Forms.Label

#End Region

  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    Try
      oMenu = Menu
      oApp = oMenu.App
      If Ditta <> "" Then
        DittaCorrente = Ditta
      Else
        DittaCorrente = oApp.Ditta
      End If

      InitializeComponent()
      Me.MinimumSize = Me.Size

      oPar = Param

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub InitializeComponent()
    Me.cmdElencoPuntato = New NTSInformatica.NTSButton
    Me.cmdElencoNum = New NTSInformatica.NTSButton
    Me.lbElencoPuntato = New System.Windows.Forms.Label
    Me.pnHele = New NTSInformatica.NTSPanel
    Me.lbElencoNum = New System.Windows.Forms.Label
    CType(Me.pnHele, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnHele.SuspendLayout()
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
    'cmdElencoPuntato
    '
    Me.cmdElencoPuntato.ImageText = ""
    Me.cmdElencoPuntato.Location = New System.Drawing.Point(28, 49)
    Me.cmdElencoPuntato.Name = "cmdElencoPuntato"
    Me.cmdElencoPuntato.Size = New System.Drawing.Size(91, 77)
    Me.cmdElencoPuntato.TabIndex = 1
    Me.cmdElencoPuntato.Text = "   o  _____" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "   o  _____" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "   o  _____"
    '
    'cmdElencoNum
    '
    Me.cmdElencoNum.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdElencoNum.ImageText = ""
    Me.cmdElencoNum.Location = New System.Drawing.Point(160, 49)
    Me.cmdElencoNum.Name = "cmdElencoNum"
    Me.cmdElencoNum.Size = New System.Drawing.Size(91, 77)
    Me.cmdElencoNum.TabIndex = 2
    Me.cmdElencoNum.Text = "   1.  _____" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "   2.  _____" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "   3.  _____"
    '
    'lbElencoPuntato
    '
    Me.lbElencoPuntato.AutoSize = True
    Me.lbElencoPuntato.Location = New System.Drawing.Point(48, 24)
    Me.lbElencoPuntato.Name = "lbElencoPuntato"
    Me.lbElencoPuntato.Size = New System.Drawing.Size(45, 13)
    Me.lbElencoPuntato.TabIndex = 17
    Me.lbElencoPuntato.Text = "Puntato"
    '
    'pnHele
    '
    Me.pnHele.AllowDrop = True
    Me.pnHele.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnHele.Appearance.Options.UseBackColor = True
    Me.pnHele.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnHele.Controls.Add(Me.cmdElencoPuntato)
    Me.pnHele.Controls.Add(Me.lbElencoNum)
    Me.pnHele.Controls.Add(Me.lbElencoPuntato)
    Me.pnHele.Controls.Add(Me.cmdElencoNum)
    Me.pnHele.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnHele.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnHele.Location = New System.Drawing.Point(0, 0)
    Me.pnHele.Name = "pnHele"
    Me.pnHele.NTSActiveTrasparency = True
    Me.pnHele.Size = New System.Drawing.Size(282, 156)
    Me.pnHele.TabIndex = 18
    Me.pnHele.Text = "NtsPanel1"
    '
    'lbElencoNum
    '
    Me.lbElencoNum.AutoSize = True
    Me.lbElencoNum.Location = New System.Drawing.Point(173, 24)
    Me.lbElencoNum.Name = "lbElencoNum"
    Me.lbElencoNum.Size = New System.Drawing.Size(54, 13)
    Me.lbElencoNum.TabIndex = 17
    Me.lbElencoNum.Text = "Numerato"
    '
    'FRM__HELE
    '
    Me.AcceptButton = Me.cmdElencoPuntato
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.CancelButton = Me.cmdElencoNum
    Me.ClientSize = New System.Drawing.Size(282, 156)
    Me.Controls.Add(Me.pnHele)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__HELE"
    Me.Text = "INSERISCI ELENCO"
    CType(Me.pnHele, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnHele.ResumeLayout(False)
    Me.pnHele.PerformLayout()
    Me.ResumeLayout(False)

  End Sub


  Public Overridable Sub FRM__HELE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    GctlSetRoules()
    GctlApplicaDefaultValue()
  End Sub

  Public Overridable Sub cmdElencoPuntato_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdElencoPuntato.Click
    oPar.bPar1 = True
    oPar.bPar2 = True 'elenco puntato
    Me.Close()
  End Sub

  Public Overridable Sub cmdElencoNum_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdElencoNum.Click
    oPar.bPar1 = True
    oPar.bPar2 = False 'elenco numerato
    Me.Close()
  End Sub

End Class
