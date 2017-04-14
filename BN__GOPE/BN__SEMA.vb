Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__SEMA

#Region "Variabili"

  Public bOk As Boolean = False

  Public oCleGope As CLE__GOPE
  Public oCallParams As CLE__CLDP
  Private components As System.ComponentModel.IComponent

  Public WithEvents edOpNome As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbNomeOp As NTSInformatica.NTSLabel
  Public WithEvents cmdConferma As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton

#End Region

#Region "Eventi Form"
  Public Overridable Sub FRM__SEMA_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      InitControls()
      cmdConferma.Enabled = False
      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      'e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipodocumento, ecc ...
      'GctlTipoDoc = ""
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Private Sub InitializeComponent()
    Me.edOpNome = New NTSInformatica.NTSTextBoxStr
    Me.lbNomeOp = New NTSInformatica.NTSLabel
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    CType(Me.edOpNome.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'edOpNome
    '
    Me.edOpNome.Cursor = System.Windows.Forms.Cursors.Default
    Me.edOpNome.Location = New System.Drawing.Point(103, 15)
    Me.edOpNome.Name = "edOpNome"
    Me.edOpNome.NTSDbField = ""
    Me.edOpNome.NTSForzaVisZoom = False
    Me.edOpNome.NTSOldValue = ""
    Me.edOpNome.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOpNome.Properties.MaxLength = 65536
    Me.edOpNome.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOpNome.Size = New System.Drawing.Size(195, 20)
    Me.edOpNome.TabIndex = 0
    '
    'lbNomeOp
    '
    Me.lbNomeOp.AutoSize = True
    Me.lbNomeOp.BackColor = System.Drawing.Color.Transparent
    Me.lbNomeOp.Location = New System.Drawing.Point(12, 18)
    Me.lbNomeOp.Name = "lbNomeOp"
    Me.lbNomeOp.NTSDbField = ""
    Me.lbNomeOp.Size = New System.Drawing.Size(85, 13)
    Me.lbNomeOp.TabIndex = 1
    Me.lbNomeOp.Text = "Nome operatore"
    '
    'cmdConferma
    '
    Me.cmdConferma.Location = New System.Drawing.Point(330, 15)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.Size = New System.Drawing.Size(75, 23)
    Me.cmdConferma.TabIndex = 1
    Me.cmdConferma.Text = "Conferma"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Location = New System.Drawing.Point(330, 44)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(75, 23)
    Me.cmdAnnulla.TabIndex = 3
    Me.cmdAnnulla.Text = "Annulla"
    '
    'FRM__SEMA
    '
    Me.ClientSize = New System.Drawing.Size(417, 77)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdConferma)
    Me.Controls.Add(Me.lbNomeOp)
    Me.Controls.Add(Me.edOpNome)
    Me.MinimizeBox = False
    Me.Name = "FRM__SEMA"
    Me.Text = "SELEZIONA OPERATORE DA CUI COPIARE"
    CType(Me.edOpNome.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

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

  Public Overridable Sub InitEntity(ByRef cleGope As CLE__GOPE)
    oCleGope = cleGope
    AddHandler oCleGope.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      edOpNome.NTSSetParam(oMenu, oApp.Tr(Me, 129024299100745198, "Nome Operatore"), 20, True)

      edOpNome.NTSSetParamZoom("ZOOMOPERAT")
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

#Region "Eventi"
  Public Overridable Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    Try
      If oCleGope.SemaValidaNomeOp(edOpNome.Text) Then
        oCleGope.strMasterOpNome = edOpNome.Text
        bOk = True

        Me.Close()
      End If
      edOpNome.Text = ""

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
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edOpNome_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edOpNome.TextChanged
    Try
      If edOpNome.Text = "" Then
        cmdConferma.Enabled = False
      Else
        cmdConferma.Enabled = True
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edOpNome_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles edOpNome.KeyDown
    Try
      If e.KeyCode = Keys.Enter Then
        cmdConferma_Click(Me, Nothing)
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region
End Class