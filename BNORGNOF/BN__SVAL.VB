Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__SVAL
  Private components As System.ComponentModel.IContainer

  Public oPar As CLE__CLDP
  Public nOptionOut As Integer = 0

  Public WithEvents lbTesto1 As NTSInformatica.NTSLabel
  Public WithEvents ckDeroga As NTSInformatica.NTSCheckBox
  Public WithEvents cmdConferma As NTSInformatica.NTSButton


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

      InitControls()

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try

      ckDeroga.NTSSetParam(oMenu, oApp.Tr(Me, 128607644513125000, "In deroga verrà considerato il cambio più vicino a tale data"), "S", "N")
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

  Public Overridable Sub InitializeComponent()
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.lbTesto1 = New NTSInformatica.NTSLabel
    Me.ckDeroga = New NTSInformatica.NTSCheckBox
    CType(Me.ckDeroga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'cmdConferma
    '
    Me.cmdConferma.ImageText = ""
    Me.cmdConferma.Location = New System.Drawing.Point(109, 93)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.Size = New System.Drawing.Size(119, 22)
    Me.cmdConferma.TabIndex = 20
    Me.cmdConferma.Text = "&Conferma"
    '
    'lbTesto1
    '
    Me.lbTesto1.BackColor = System.Drawing.Color.Transparent
    Me.lbTesto1.Location = New System.Drawing.Point(12, 9)
    Me.lbTesto1.Name = "lbTesto1"
    Me.lbTesto1.NTSDbField = ""
    Me.lbTesto1.Size = New System.Drawing.Size(332, 26)
    Me.lbTesto1.TabIndex = 21
    Me.lbTesto1.Text = "Eventuali ordini in valuta avranno un cambio determinato alla data di elaborazion" & _
        "e"
    Me.lbTesto1.Tooltip = ""
    Me.lbTesto1.UseMnemonic = False
    '
    'ckDeroga
    '
    Me.ckDeroga.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDeroga.EditValue = True
    Me.ckDeroga.Location = New System.Drawing.Point(12, 53)
    Me.ckDeroga.Name = "ckDeroga"
    Me.ckDeroga.NTSCheckValue = "S"
    Me.ckDeroga.NTSUnCheckValue = "N"
    Me.ckDeroga.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDeroga.Properties.Appearance.Options.UseBackColor = True
    Me.ckDeroga.Properties.Caption = "In deroga verrà considerato il cambio più vicino a tale data."
    Me.ckDeroga.Size = New System.Drawing.Size(313, 19)
    Me.ckDeroga.TabIndex = 26
    '
    'FRM__SVAL
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(343, 127)
    Me.Controls.Add(Me.cmdConferma)
    Me.Controls.Add(Me.ckDeroga)
    Me.Controls.Add(Me.lbTesto1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__SVAL"
    Me.Text = "IMPOSTA CAMBIO"
    CType(Me.ckDeroga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Public Overridable Sub FRM__SVAL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim strSoloSerie As String = ""
    Try
      nOptionOut = 0

      '-------------------------------
      'applico le impostazioni da GCTL
      GctlSetRoules()
      GctlApplicaDefaultValue()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    Try
 
      If ckDeroga.Checked Then
        nOptionOut = 3
      Else
        nOptionOut = 2
      End If

      Me.Close()

    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
End Class
