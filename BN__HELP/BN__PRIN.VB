Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__PRIN

#Region "Variabili"

  Private components As System.ComponentModel.IContainer
  Public oPar As CLE__CLDP
  Public WithEvents cmdStampa As NTSInformatica.NTSButton
  Public WithEvents pnPrint As NTSInformatica.NTSPanel
  Public WithEvents WebBrows As NTSInformatica.NTSWebBrowser
  Public WithEvents cmdImposta As NTSInformatica.NTSButton
  Public WithEvents cmdAnteprima As NTSInformatica.NTSButton
  Public WithEvents cmdChiudi As NTSInformatica.NTSButton

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
    Me.cmdStampa = New NTSInformatica.NTSButton
    Me.cmdChiudi = New NTSInformatica.NTSButton
    Me.pnPrint = New NTSInformatica.NTSPanel
    Me.WebBrows = New NTSInformatica.NTSWebBrowser
    Me.cmdImposta = New NTSInformatica.NTSButton
    Me.cmdAnteprima = New NTSInformatica.NTSButton
    CType(Me.pnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPrint.SuspendLayout()
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
    'cmdStampa
    '
    Me.cmdStampa.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdStampa.ImageText = ""
    Me.cmdStampa.Location = New System.Drawing.Point(252, 635)
    Me.cmdStampa.Name = "cmdStampa"
    Me.cmdStampa.Size = New System.Drawing.Size(91, 23)
    Me.cmdStampa.TabIndex = 1
    Me.cmdStampa.Text = "Stampa"
    '
    'cmdChiudi
    '
    Me.cmdChiudi.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdChiudi.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdChiudi.ImageText = ""
    Me.cmdChiudi.Location = New System.Drawing.Point(543, 635)
    Me.cmdChiudi.Name = "cmdChiudi"
    Me.cmdChiudi.Size = New System.Drawing.Size(91, 23)
    Me.cmdChiudi.TabIndex = 2
    Me.cmdChiudi.Text = "Chiudi"
    '
    'pnPrint
    '
    Me.pnPrint.AllowDrop = True
    Me.pnPrint.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPrint.Appearance.Options.UseBackColor = True
    Me.pnPrint.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPrint.Controls.Add(Me.WebBrows)
    Me.pnPrint.Controls.Add(Me.cmdImposta)
    Me.pnPrint.Controls.Add(Me.cmdAnteprima)
    Me.pnPrint.Controls.Add(Me.cmdStampa)
    Me.pnPrint.Controls.Add(Me.cmdChiudi)
    Me.pnPrint.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPrint.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPrint.Location = New System.Drawing.Point(0, 0)
    Me.pnPrint.Name = "pnPrint"
    Me.pnPrint.Size = New System.Drawing.Size(646, 670)
    Me.pnPrint.TabIndex = 3
    Me.pnPrint.Text = "NtsPanel1"
    '
    'WebBrows
    '
    Me.WebBrows.AllowNavigation = False
    Me.WebBrows.AllowWebBrowserDrop = False
    Me.WebBrows.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.WebBrows.CausesValidation = False
    Me.WebBrows.IsWebBrowserContextMenuEnabled = False
    Me.WebBrows.Location = New System.Drawing.Point(0, 0)
    Me.WebBrows.MinimumSize = New System.Drawing.Size(20, 20)
    Me.WebBrows.Name = "WebBrows"
    Me.WebBrows.ScriptErrorsSuppressed = True
    Me.WebBrows.Size = New System.Drawing.Size(646, 623)
    Me.WebBrows.TabIndex = 3
    Me.WebBrows.WebBrowserShortcutsEnabled = False
    '
    'cmdImposta
    '
    Me.cmdImposta.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdImposta.ImageText = ""
    Me.cmdImposta.Location = New System.Drawing.Point(446, 635)
    Me.cmdImposta.Name = "cmdImposta"
    Me.cmdImposta.Size = New System.Drawing.Size(91, 23)
    Me.cmdImposta.TabIndex = 1
    Me.cmdImposta.Text = "Imposta"
    '
    'cmdAnteprima
    '
    Me.cmdAnteprima.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAnteprima.ImageText = ""
    Me.cmdAnteprima.Location = New System.Drawing.Point(349, 635)
    Me.cmdAnteprima.Name = "cmdAnteprima"
    Me.cmdAnteprima.Size = New System.Drawing.Size(91, 23)
    Me.cmdAnteprima.TabIndex = 1
    Me.cmdAnteprima.Text = "Anteprima"
    '
    'FRM__PRIN
    '
    Me.AcceptButton = Me.cmdStampa
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.CancelButton = Me.cmdChiudi
    Me.ClientSize = New System.Drawing.Size(646, 670)
    Me.Controls.Add(Me.pnPrint)
    Me.MinimizeBox = False
    Me.Name = "FRM__PRIN"
    Me.Text = "STAMPA ARGOMENTO CORRENTE E SOTTO ARGOMENTI"
    CType(Me.pnPrint, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPrint.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

#Region "Form"

  Public Overridable Sub FRM__PRIN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      GctlSetRoules()
      GctlApplicaDefaultValue()
      WebBrows.Navigate(oApp.Dir & "\stampa_" & oApp.User.Nome & ".htm")
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

#End Region

  Public Overridable Sub cmdStampa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdStampa.Click
    Try
      WebBrows.ShowPrintDialog()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdAnteprima_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnteprima.Click
    Try
      WebBrows.ShowPrintPreviewDialog()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdImposta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImposta.Click
    Try
      WebBrows.ShowPageSetupDialog()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdChiudi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdChiudi.Click
    Try
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class
