Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__SRPT

#Region "Variabili"

  Private components As System.ComponentModel.IContainer
  Public oPar As CLE__CLDP
  Public WithEvents opRidotta As NTSInformatica.NTSRadioButton
  Public WithEvents cmdProcedi As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents opCompleta As NTSInformatica.NTSRadioButton
  Public WithEvents fmTipostampa As NTSInformatica.NTSGroupBox
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
    Me.fmTipostampa = New NTSInformatica.NTSGroupBox
    Me.opCompleta = New NTSInformatica.NTSRadioButton
    Me.opRidotta = New NTSInformatica.NTSRadioButton
    Me.cmdProcedi = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    CType(Me.fmTipostampa, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmTipostampa.SuspendLayout()
    CType(Me.opCompleta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opRidotta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'fmTipostampa
    '
    Me.fmTipostampa.AllowDrop = True
    Me.fmTipostampa.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmTipostampa.Appearance.Options.UseBackColor = True
    Me.fmTipostampa.Controls.Add(Me.opCompleta)
    Me.fmTipostampa.Controls.Add(Me.opRidotta)
    Me.fmTipostampa.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmTipostampa.Location = New System.Drawing.Point(6, 6)
    Me.fmTipostampa.Name = "fmTipostampa"
    Me.fmTipostampa.Size = New System.Drawing.Size(150, 74)
    Me.fmTipostampa.TabIndex = 0
    Me.fmTipostampa.Text = "Tipo di stampa"
    '
    'opCompleta
    '
    Me.opCompleta.Cursor = System.Windows.Forms.Cursors.Default
    Me.opCompleta.Location = New System.Drawing.Point(10, 48)
    Me.opCompleta.Name = "opCompleta"
    Me.opCompleta.NTSCheckValue = "S"
    Me.opCompleta.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opCompleta.Properties.Appearance.Options.UseBackColor = True
    Me.opCompleta.Properties.Caption = "&Completa"
    Me.opCompleta.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opCompleta.Size = New System.Drawing.Size(109, 19)
    Me.opCompleta.TabIndex = 1
    '
    'opRidotta
    '
    Me.opRidotta.Cursor = System.Windows.Forms.Cursors.Default
    Me.opRidotta.EditValue = True
    Me.opRidotta.Location = New System.Drawing.Point(10, 23)
    Me.opRidotta.Name = "opRidotta"
    Me.opRidotta.NTSCheckValue = "S"
    Me.opRidotta.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opRidotta.Properties.Appearance.Options.UseBackColor = True
    Me.opRidotta.Properties.Caption = "&Ridotta"
    Me.opRidotta.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opRidotta.Size = New System.Drawing.Size(110, 19)
    Me.opRidotta.TabIndex = 0
    '
    'cmdProcedi
    '
    Me.cmdProcedi.Location = New System.Drawing.Point(165, 12)
    Me.cmdProcedi.Name = "cmdProcedi"
    Me.cmdProcedi.Size = New System.Drawing.Size(91, 23)
    Me.cmdProcedi.TabIndex = 1
    Me.cmdProcedi.Text = "&Procedi"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Location = New System.Drawing.Point(165, 37)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(91, 23)
    Me.cmdAnnulla.TabIndex = 2
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'FRM__SRPT
    '
    Me.AcceptButton = Me.cmdProcedi
    Me.ClientSize = New System.Drawing.Size(263, 86)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdProcedi)
    Me.Controls.Add(Me.fmTipostampa)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__SRPT"
    Me.Text = "Seleziona Tipo di Stampa"
    CType(Me.fmTipostampa, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmTipostampa.ResumeLayout(False)
    Me.fmTipostampa.PerformLayout()
    CType(Me.opCompleta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opRidotta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

#Region "Form"

  Public Overridable Sub FRM__SRPT_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    'Cerco di uscure dal Control-Box
    If bControlBoxExit Then
      e.Cancel = True
    End If
  End Sub

  Public Overridable Sub FRM__SRPT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    'Applico le impostazioni da GCTL
    'GctlTipoDoc = " "
    GctlSetRoules()
    GctlApplicaDefaultValue()

    Me.NTSDisableEnterComeTab()

    bControlBoxExit = True

  End Sub

#End Region

  Public Overridable Sub cmdProcedi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdProcedi.Click
    oPar.bPar1 = False
    If opRidotta.Checked Then
      oPar.dPar1 = 1
    Else
      oPar.dPar1 = 2
    End If

    bControlBoxExit = False
    Me.Close()
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    oPar.bPar1 = True

    bControlBoxExit = False
    Me.Close()
  End Sub

End Class
