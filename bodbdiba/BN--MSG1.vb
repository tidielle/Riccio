Public Class FRM__MSG1
  Private components As System.ComponentModel.IContainer

  Public bOk As Boolean = False
  Public lConto As Integer = 0

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

      edMess.NTSSetParam(oMenu, oApp.Tr(Me, 128230023172255055, ""), 0)

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub InitializeComponent()
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.edMess = New NTSInformatica.NTSMemoBox
    Me.cmdZoom = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    CType(Me.edMess.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.frmAuto.Appearance.BackColor = System.Drawing.SystemColors.GradientActiveCaption
    Me.frmAuto.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmAuto.Appearance.Options.UseBackColor = True
    Me.frmAuto.Appearance.Options.UseImage = True
    '
    'cmdOk
    '
    Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdOk.Location = New System.Drawing.Point(335, 7)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(79, 24)
    Me.cmdOk.TabIndex = 2
    Me.cmdOk.Text = "OK"
    '
    'edMess
    '
    Me.edMess.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edMess.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMess.Location = New System.Drawing.Point(6, 7)
    Me.edMess.Name = "edMess"
    Me.edMess.NTSDbField = ""
    Me.edMess.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMess.Size = New System.Drawing.Size(323, 124)
    Me.edMess.TabIndex = 3
    '
    'cmdZoom
    '
    Me.cmdZoom.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdZoom.Location = New System.Drawing.Point(335, 37)
    Me.cmdZoom.Name = "cmdZoom"
    Me.cmdZoom.Size = New System.Drawing.Size(79, 24)
    Me.cmdZoom.TabIndex = 4
    Me.cmdZoom.Text = "Zoom"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAnnulla.Location = New System.Drawing.Point(335, 67)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(79, 24)
    Me.cmdAnnulla.TabIndex = 5
    Me.cmdAnnulla.Text = "Annulla"
    '
    'FRM__MSG1
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(422, 136)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdZoom)
    Me.Controls.Add(Me.cmdOk)
    Me.Controls.Add(Me.edMess)
    Me.MinimizeBox = False
    Me.Name = "FRM__MSG1"
    Me.Text = "MESSAGGIO DI BUSINESS"
    CType(Me.edMess.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      bOk = True
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdZoom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdZoom.Click
    Dim nSeparaNote As Integer = 0
    Dim oPar As New CLE__PATB

    Try
      nSeparaNote = CLN__STD.NTSCInt(Val(oMenu.GetSettingBusDitt(DittaCorrente, "BS--HLNT", "Opzioni", ".", "SeparaNote", "0", " ", "0")))  'Separa le note scelte in multiselezione da:  0=nessun carattere, 1=un cr+lf, 2=due cr+lf

      oPar.lContoCF = lConto
      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMNOTE", DittaCorrente, oPar)
      If NTSZOOM.strIn <> "" Then

        If edMess.Text = "" Then
          'Non c'è ancora testo nella textbox
          edMess.Text = NTSZOOM.strIn
        Else
          'C'è già del testo nella textbox delle note
          Select Case nSeparaNote
            Case 0
              edMess.Text = edMess.Text & NTSZOOM.strIn
            Case 1
              edMess.Text = edMess.Text & vbCrLf & NTSZOOM.strIn
            Case Else '(cioè 2)
              edMess.Text = edMess.Text & vbCrLf & vbCrLf & NTSZOOM.strIn
          End Select
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub FRM__MSG1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      edMess.NTSSetParam(oMenu, oApp.Tr(Me, 128689826417656250, "Note riga"), 0, True)
      NTSDisableEnterComeTab()
      NTSCorreggiTabIndex(Me)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub FRM__MSG1_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      If cmdZoom.Visible Then
        edMess.Focus()
      Else
        'edMess.Enabled = False     'la scroolbar viene bloccata !!!!
        edMess.TabStop = False
        cmdOk.Focus()
      End If
      edMess.SelectionStart = 0
      edMess.SelectionLength = 0
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
End Class
