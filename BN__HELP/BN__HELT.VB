Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__HELT

#Region "Variabili"

  Private components As System.ComponentModel.IContainer
  Public oPar As CLE__CLDP
  Public WithEvents cmdProcedi As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents edRighe As NTSInformatica.NTSTextBoxStr
  Public WithEvents edColonne As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbInsTabe As System.Windows.Forms.Label
  Public WithEvents lbColonne As System.Windows.Forms.Label
  Public WithEvents lbRighe As System.Windows.Forms.Label
  Public WithEvents ckConTitolo As NTSInformatica.NTSCheckBox
  Public WithEvents TrackBar1 As System.Windows.Forms.TrackBar
  Public WithEvents ckAutoSize As NTSInformatica.NTSCheckBox
  Public WithEvents edWidth As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbPerc As System.Windows.Forms.Label
  Public bControlBoxExit As Boolean

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
    Me.cmdProcedi = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.edRighe = New NTSInformatica.NTSTextBoxStr
    Me.edColonne = New NTSInformatica.NTSTextBoxStr
    Me.lbInsTabe = New System.Windows.Forms.Label
    Me.lbColonne = New System.Windows.Forms.Label
    Me.lbRighe = New System.Windows.Forms.Label
    Me.ckConTitolo = New NTSInformatica.NTSCheckBox
    Me.TrackBar1 = New System.Windows.Forms.TrackBar
    Me.ckAutoSize = New NTSInformatica.NTSCheckBox
    Me.edWidth = New NTSInformatica.NTSTextBoxStr
    Me.lbPerc = New System.Windows.Forms.Label
    CType(Me.edRighe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edColonne.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckConTitolo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAutoSize.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edWidth.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'cmdProcedi
    '
    Me.cmdProcedi.ImageText = ""
    Me.cmdProcedi.Location = New System.Drawing.Point(216, 12)
    Me.cmdProcedi.Name = "cmdProcedi"
    Me.cmdProcedi.Size = New System.Drawing.Size(91, 23)
    Me.cmdProcedi.TabIndex = 1
    Me.cmdProcedi.Text = "&Procedi"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(216, 37)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(91, 23)
    Me.cmdAnnulla.TabIndex = 2
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'edRighe
    '
    Me.edRighe.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edRighe.Cursor = System.Windows.Forms.Cursors.Default
    Me.edRighe.EditValue = "3"
    Me.edRighe.Location = New System.Drawing.Point(81, 29)
    Me.edRighe.Name = "edRighe"
    Me.edRighe.NTSDbField = ""
    Me.edRighe.NTSForzaVisZoom = False
    Me.edRighe.NTSOldValue = "3"
    Me.edRighe.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edRighe.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edRighe.Properties.MaxLength = 65536
    Me.edRighe.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edRighe.Size = New System.Drawing.Size(50, 20)
    Me.edRighe.TabIndex = 6
    '
    'edColonne
    '
    Me.edColonne.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edColonne.Cursor = System.Windows.Forms.Cursors.Default
    Me.edColonne.EditValue = "3"
    Me.edColonne.Location = New System.Drawing.Point(81, 55)
    Me.edColonne.Name = "edColonne"
    Me.edColonne.NTSDbField = ""
    Me.edColonne.NTSForzaVisZoom = False
    Me.edColonne.NTSOldValue = "3"
    Me.edColonne.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edColonne.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edColonne.Properties.MaxLength = 65536
    Me.edColonne.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edColonne.Size = New System.Drawing.Size(50, 20)
    Me.edColonne.TabIndex = 6
    '
    'lbInsTabe
    '
    Me.lbInsTabe.AutoSize = True
    Me.lbInsTabe.Location = New System.Drawing.Point(12, 9)
    Me.lbInsTabe.Name = "lbInsTabe"
    Me.lbInsTabe.Size = New System.Drawing.Size(86, 13)
    Me.lbInsTabe.TabIndex = 17
    Me.lbInsTabe.Text = "Tabella formato:"
    '
    'lbColonne
    '
    Me.lbColonne.AutoSize = True
    Me.lbColonne.Location = New System.Drawing.Point(12, 58)
    Me.lbColonne.Name = "lbColonne"
    Me.lbColonne.Size = New System.Drawing.Size(63, 13)
    Me.lbColonne.TabIndex = 17
    Me.lbColonne.Text = "per colonne"
    '
    'lbRighe
    '
    Me.lbRighe.AutoSize = True
    Me.lbRighe.Location = New System.Drawing.Point(12, 32)
    Me.lbRighe.Name = "lbRighe"
    Me.lbRighe.Size = New System.Drawing.Size(31, 13)
    Me.lbRighe.TabIndex = 17
    Me.lbRighe.Text = "righe"
    '
    'ckConTitolo
    '
    Me.ckConTitolo.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckConTitolo.EditValue = True
    Me.ckConTitolo.Location = New System.Drawing.Point(15, 151)
    Me.ckConTitolo.Name = "ckConTitolo"
    Me.ckConTitolo.NTSCheckValue = "S"
    Me.ckConTitolo.NTSUnCheckValue = "N"
    Me.ckConTitolo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckConTitolo.Properties.Appearance.Options.UseBackColor = True
    Me.ckConTitolo.Properties.Caption = "inserisci una riga con il titolo"
    Me.ckConTitolo.Size = New System.Drawing.Size(292, 19)
    Me.ckConTitolo.TabIndex = 18
    '
    'TrackBar1
    '
    Me.TrackBar1.Enabled = False
    Me.TrackBar1.Location = New System.Drawing.Point(104, 82)
    Me.TrackBar1.Maximum = 100
    Me.TrackBar1.Minimum = 10
    Me.TrackBar1.Name = "TrackBar1"
    Me.TrackBar1.Size = New System.Drawing.Size(104, 45)
    Me.TrackBar1.TabIndex = 19
    Me.TrackBar1.TickFrequency = 10
    Me.TrackBar1.Value = 50
    '
    'ckAutoSize
    '
    Me.ckAutoSize.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.ckAutoSize.EditValue = True
    Me.ckAutoSize.Location = New System.Drawing.Point(15, 87)
    Me.ckAutoSize.Name = "ckAutoSize"
    Me.ckAutoSize.NTSCheckValue = "S"
    Me.ckAutoSize.NTSUnCheckValue = "N"
    Me.ckAutoSize.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAutoSize.Properties.Appearance.Options.UseBackColor = True
    Me.ckAutoSize.Properties.Caption = "autosize"
    Me.ckAutoSize.Size = New System.Drawing.Size(83, 19)
    Me.ckAutoSize.TabIndex = 18
    '
    'edWidth
    '
    Me.edWidth.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edWidth.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edWidth.EditValue = "50"
    Me.edWidth.Enabled = False
    Me.edWidth.Location = New System.Drawing.Point(214, 86)
    Me.edWidth.Name = "edWidth"
    Me.edWidth.NTSDbField = ""
    Me.edWidth.NTSForzaVisZoom = False
    Me.edWidth.NTSOldValue = "50"
    Me.edWidth.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edWidth.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edWidth.Properties.MaxLength = 65536
    Me.edWidth.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edWidth.Size = New System.Drawing.Size(41, 20)
    Me.edWidth.TabIndex = 6
    '
    'lbPerc
    '
    Me.lbPerc.AutoSize = True
    Me.lbPerc.Location = New System.Drawing.Point(261, 89)
    Me.lbPerc.Name = "lbPerc"
    Me.lbPerc.Size = New System.Drawing.Size(18, 13)
    Me.lbPerc.TabIndex = 17
    Me.lbPerc.Text = "%"
    '
    'FRM__HELT
    '
    Me.AcceptButton = Me.cmdProcedi
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.CancelButton = Me.cmdAnnulla
    Me.ClientSize = New System.Drawing.Size(319, 184)
    Me.Controls.Add(Me.TrackBar1)
    Me.Controls.Add(Me.ckAutoSize)
    Me.Controls.Add(Me.ckConTitolo)
    Me.Controls.Add(Me.lbColonne)
    Me.Controls.Add(Me.lbPerc)
    Me.Controls.Add(Me.lbRighe)
    Me.Controls.Add(Me.lbInsTabe)
    Me.Controls.Add(Me.edWidth)
    Me.Controls.Add(Me.edColonne)
    Me.Controls.Add(Me.edRighe)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdProcedi)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__HELT"
    Me.Text = "INSERISCI TABELLA"
    CType(Me.edRighe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edColonne.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckConTitolo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAutoSize.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edWidth.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

#Region "Form"

  Public Overridable Sub FRM__SRPT_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    'Cerco di uscure dal Control-Box
    If bControlBoxExit Then
      e.Cancel = True
    End If
  End Sub

  Public Overridable Sub FRM__SRPT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    GctlSetRoules()
    GctlApplicaDefaultValue()
  End Sub

#End Region

  Public Overridable Sub cmdProcedi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdProcedi.Click
    oPar.bPar1 = True
    CostruisciTabella()
    Me.Close()
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    oPar.bPar1 = False
    Me.Close()
  End Sub

  Public Sub CostruisciTabella()
    Try
      If ckAutoSize.Checked Then
        oPar.strPar1 = "<TABLE class=auto>"
      Else
        oPar.strPar1 = "<TABLE class=auto width=""" & NTSCInt(edWidth.Text) & "%"">"
      End If
      If ckConTitolo.Checked Then oPar.strPar1 &= "<TR><TD colSpan=" & Format(NTSCInt(edColonne.Text), "0") & " align=middle>Titolo</TD></TR>"
      For i As Integer = 0 To NTSCInt(edRighe.Text) - 1
        oPar.strPar1 &= "<TR>"
        For j As Integer = 0 To NTSCInt(edColonne.Text) - 1
          oPar.strPar1 &= "<TD></TD>"
        Next
        oPar.strPar1 &= "</TR>"
      Next
      oPar.strPar1 &= "</TABLE>"

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Sub ckAutoSize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckAutoSize.CheckedChanged
    Try
      If ckAutoSize.Checked Then
        TrackBar1.Enabled = False
        edWidth.Enabled = False
      Else
        TrackBar1.Enabled = True
        edWidth.Enabled = True
      End If
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)

    End Try

  End Sub

  Public Overridable Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
    Try
      edWidth.Text = Format(TrackBar1.Value, "0")
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)

    End Try
  End Sub

  Public Overridable Sub edWidth_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edWidth.ValidatedAndChanged
    Try
      TrackBar1.Value = NTSCInt(edWidth.Text)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)

    End Try
  End Sub


  Public Overridable Sub edWidth_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles edWidth.Validating
    If NTSCInt(edWidth.Text) >= 10 And NTSCInt(edWidth.Text) <= 100 Then
      If NTSCInt(edWidth.Text) = NTSCInt(edWidth.Text) Then

      Else
        edWidth.Text = Format(NTSCInt(edWidth.Text), "0")
      End If
    Else
      edWidth.Text = edWidth.NTSOldValue
    End If
  End Sub
End Class
