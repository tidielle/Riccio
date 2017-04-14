Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__SVAL
  Private components As System.ComponentModel.IContainer

  Public oPar As CLE__CLDP
  Public nOptionOut As Integer = 0

  Public WithEvents lbTesto1 As NTSInformatica.NTSLabel
  Public WithEvents opValuta0 As NTSInformatica.NTSRadioButton
  Public WithEvents opValuta1 As NTSInformatica.NTSRadioButton
  Public WithEvents ckDeroga As NTSInformatica.NTSCheckBox
  Public WithEvents pnConfermaFocus As NTSInformatica.NTSPanel
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
      opValuta0.NTSSetParam(oMenu, oApp.Tr(Me, 128608400094843750, "Usa il cambio in testata documento"), "S")
      opValuta1.NTSSetParam(oMenu, oApp.Tr(Me, 128608400296718750, "Determina il cambio alla data di elaborazione"), "N")

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
    Me.opValuta0 = New NTSInformatica.NTSRadioButton
    Me.opValuta1 = New NTSInformatica.NTSRadioButton
    Me.ckDeroga = New NTSInformatica.NTSCheckBox
    Me.pnConfermaFocus = New NTSInformatica.NTSPanel
    CType(Me.opValuta0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opValuta1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDeroga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnConfermaFocus, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnConfermaFocus.SuspendLayout()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'cmdConferma
    '
    Me.cmdConferma.Location = New System.Drawing.Point(3, 143)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.Size = New System.Drawing.Size(119, 22)
    Me.cmdConferma.TabIndex = 20
    Me.cmdConferma.Text = "&Conferma"
    '
    'lbTesto1
    '
    Me.lbTesto1.AutoSize = True
    Me.lbTesto1.BackColor = System.Drawing.Color.Transparent
    Me.lbTesto1.Location = New System.Drawing.Point(12, 9)
    Me.lbTesto1.Name = "lbTesto1"
    Me.lbTesto1.NTSDbField = ""
    Me.lbTesto1.Size = New System.Drawing.Size(279, 26)
    Me.lbTesto1.TabIndex = 21
    Me.lbTesto1.Text = "Eventuali ordini o documenti in valuta avranno un cambio" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "specificato da una dell" & _
        "e seguenti scelte."
    Me.lbTesto1.UseMnemonic = False
    '
    'opValuta0
    '
    Me.opValuta0.Cursor = System.Windows.Forms.Cursors.Default
    Me.opValuta0.Location = New System.Drawing.Point(12, 51)
    Me.opValuta0.Name = "opValuta0"
    Me.opValuta0.NTSCheckValue = "S"
    Me.opValuta0.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opValuta0.Properties.Appearance.Options.UseBackColor = True
    Me.opValuta0.Properties.Caption = "Usa il cambio della proposta d'ordine"
    Me.opValuta0.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opValuta0.Size = New System.Drawing.Size(204, 19)
    Me.opValuta0.TabIndex = 22
    '
    'opValuta1
    '
    Me.opValuta1.Cursor = System.Windows.Forms.Cursors.Default
    Me.opValuta1.EditValue = True
    Me.opValuta1.Location = New System.Drawing.Point(12, 76)
    Me.opValuta1.Name = "opValuta1"
    Me.opValuta1.NTSCheckValue = "S"
    Me.opValuta1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opValuta1.Properties.Appearance.Options.UseBackColor = True
    Me.opValuta1.Properties.Caption = "Determina il cambio alla data di elaborazione."
    Me.opValuta1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opValuta1.Size = New System.Drawing.Size(242, 19)
    Me.opValuta1.TabIndex = 23
    '
    'ckDeroga
    '
    Me.ckDeroga.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDeroga.EditValue = True
    Me.ckDeroga.Location = New System.Drawing.Point(31, 101)
    Me.ckDeroga.Name = "ckDeroga"
    Me.ckDeroga.NTSCheckValue = "S"
    Me.ckDeroga.NTSUnCheckValue = "N"
    Me.ckDeroga.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDeroga.Properties.Appearance.Options.UseBackColor = True
    Me.ckDeroga.Properties.Caption = "In deroga verrà considerato il cambio più vicino a tale data."
    Me.ckDeroga.Size = New System.Drawing.Size(313, 19)
    Me.ckDeroga.TabIndex = 24
    '
    'pnConfermaFocus
    '
    Me.pnConfermaFocus.AllowDrop = True
    Me.pnConfermaFocus.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnConfermaFocus.Appearance.Options.UseBackColor = True
    Me.pnConfermaFocus.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnConfermaFocus.Controls.Add(Me.cmdConferma)
    Me.pnConfermaFocus.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnConfermaFocus.Location = New System.Drawing.Point(112, 4)
    Me.pnConfermaFocus.Name = "pnConfermaFocus"
    Me.pnConfermaFocus.Size = New System.Drawing.Size(125, 171)
    Me.pnConfermaFocus.TabIndex = 25
    Me.pnConfermaFocus.Text = "NtsPanel1"
    '
    'FRM__SVAL
    '
    Me.ClientSize = New System.Drawing.Size(343, 181)
    Me.Controls.Add(Me.ckDeroga)
    Me.Controls.Add(Me.opValuta1)
    Me.Controls.Add(Me.opValuta0)
    Me.Controls.Add(Me.lbTesto1)
    Me.Controls.Add(Me.pnConfermaFocus)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__SVAL"
    Me.Text = "IMPOSTA CAMBIO"
    CType(Me.opValuta0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opValuta1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDeroga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnConfermaFocus, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnConfermaFocus.ResumeLayout(False)
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
      Select Case True
        Case opValuta0.Checked
          nOptionOut = 1
        Case opValuta1.Checked
          If ckDeroga.Checked Then
            nOptionOut = 3
          Else
            nOptionOut = 2
          End If
      End Select
      Me.Close()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub opValuta1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opValuta1.CheckedChanged
    Try
      If opValuta1.Checked Then
        GctlSetVisEnab(ckDeroga, False)
      Else
        ckDeroga.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
End Class
