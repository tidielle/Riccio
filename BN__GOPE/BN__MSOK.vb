Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__MSOK
  Public oCallParams As CLE__CLDP
  Public oCleGope As CLE__GOPE
  Private components As System.ComponentModel.IComponent

  Public bAnnullato As Boolean = True
  Public bIndietro As Boolean = True
  Public bCrmmod As Boolean = False
  Public bAmm As Boolean = False
  Public bCodcage As Boolean = False
  Public bSostituisce As Boolean = False

  Public nCodcage As Integer = 0
  Public nAltriAccessi As Integer = 0
  Public nOldAgente As Integer = 0

  Public strDescodcage As String = ""
  Public strCodruaz As String = ""
  Public strDescodruaz As String = ""
  Public strEmail As String = ""
  Public strCell As String = ""
  Public strOldOperatore As String = ""
  Public strDesOldAgente As String = ""
  Public strCognome As String = ""
  Public strNome As String = ""

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

  Private Sub InitializeComponent()
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdProcedi = New NTSInformatica.NTSButton
    Me.edMsg = New NTSInformatica.NTSMemoBox
    CType(Me.edMsg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'DevXDefaultLookAndFeel
    '
    
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Location = New System.Drawing.Point(766, 41)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(168, 23)
    Me.cmdAnnulla.TabIndex = 7
    Me.cmdAnnulla.Text = "Annulla"
    '
    'cmdProcedi
    '
    Me.cmdProcedi.Location = New System.Drawing.Point(766, 12)
    Me.cmdProcedi.Name = "cmdProcedi"
    Me.cmdProcedi.Size = New System.Drawing.Size(168, 23)
    Me.cmdProcedi.TabIndex = 6
    Me.cmdProcedi.Text = "Procedi"
    '
    'edMsg
    '
    Me.edMsg.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMsg.Location = New System.Drawing.Point(12, 12)
    Me.edMsg.Name = "edMsg"
    Me.edMsg.NTSDbField = ""
    Me.edMsg.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMsg.Size = New System.Drawing.Size(737, 233)
    Me.edMsg.TabIndex = 34
    Me.edMsg.TabStop = False
    '
    'FRM__MSOK
    '
    Me.ClientSize = New System.Drawing.Size(958, 261)
    Me.Controls.Add(Me.edMsg)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdProcedi)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__MSOK"
    Me.Text = "RICHIESTA CONFERMA"
    CType(Me.edMsg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      edMsg.NTSSetParam(oMenu, oApp.Tr(Me, 128986054994864160, "Messaggio"), 0, True)
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Public Overridable Sub InitEntity(ByRef cleGope As CLE__GOPE)
    oCleGope = cleGope
    AddHandler oCleGope.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

#Region "FORM"
  Public Overridable Sub FRM__SEOP_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      edMsg.Text = "AVVIO DEL WIZARD PER L'IMPOSTAZIONE DEL NUOVO UTENTE -CRM- CON I SEGUENTI PARAMETRI:" & vbCrLf & vbCrLf
      edMsg.Text += " --> Cognome: " & strCognome & vbCrLf
      edMsg.Text += " --> Nome: " & strNome & vbCrLf
      If bCrmmod = True Then edMsg.Text += " --> Il nuovo Utente oltre alla possibilità di vedere i dati della ditta corrente, può anche modificare/inserirne di nuovi" & vbCrLf
      If bAmm = True Then edMsg.Text += " --> Il nuovo Utente ha la possibilità di vedere le Anagrafiche Fornitori e gli elenchi dei Fornitori" & vbCrLf
      If bCodcage = True Then edMsg.Text += " --> Il nuovo Utente è un Agente: " & nCodcage.ToString & " - " & strDescodcage & vbCrLf
      edMsg.Text += " --> Ruolo Aziendale: " & strCodruaz & " - " & strDescodruaz & vbCrLf
      If strEmail.Trim <> "" Then edMsg.Text += " --> E-mail: " & strEmail & vbCrLf
      If strCell.Trim <> "" Then edMsg.Text += " --> N° Cellulare: " & strCell & vbCrLf
      Select Case nAltriAccessi
        Case 1 : edMsg.Text += " --> L'Operatore ha accesso solo ai suoi dati" & vbCrLf
        Case 2 : edMsg.Text += " --> L'Operatore ha accesso ai suoi dati, in lettura/scrittura ed ai dati di tutti gli Operatori CRM in sola lettura" & vbCrLf
        Case 3 : edMsg.Text += " --> L'Operatore ha accesso ai suoi dati, in lettura/scrittura ed ai dati di tutti gli Operatori CRM in lettura e scrittura" & vbCrLf
      End Select
      If bSostituisce = True Then
        edMsg.Text += " --> Il nuovo Utente sostituisce, nell'Organizzazione, un precedente Operatore/Agente:" & vbCrLf & _
          "     --> Vecchio Operatore: " & strOldOperatore & vbCrLf
        If nOldAgente <> 0 Then edMsg.Text += "     --> Vecchio Agente: " & nOldAgente.ToString & " - " & strDesOldAgente
      End If
      '--------------------------------------------------------------------------------------------------------------
      edMsg.Properties.ReadOnly = True
      '--------------------------------------------------------------------------------------------------------------
      cmdProcedi.Focus()
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "PULSANTI"
  Public Overridable Sub cmdProcedi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProcedi.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      bAnnullato = False
      bIndietro = False
      '--------------------------------------------------------------------------------------------------------------
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      bAnnullato = True
      bIndietro = False
      '--------------------------------------------------------------------------------------------------------------
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

End Class