Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__GRCK

#Region "Variabili"
  Public oCleGreg As CLE__GREG
  Public oCallParams As CLE__CLDP
  Private components As System.ComponentModel.IContainer

  Public WithEvents pnGrck As NTSInformatica.NTSPanel
  Public WithEvents lbErr As NTSInformatica.NTSLabel
  Public WithEvents cmdCancel As NTSInformatica.NTSButton
  Public WithEvents cmdOK As NTSInformatica.NTSButton
  Public WithEvents fr1 As NTSInformatica.NTSGroupBox
  Public WithEvents lbCurr As NTSInformatica.NTSLabel
  Public WithEvents lb2 As NTSInformatica.NTSLabel
  Public WithEvents lb1 As NTSInformatica.NTSLabel
#End Region

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

    '------------------------------
    'collego il datatable agli altri oggetti di form

    Return True
  End Function
  Public Overridable Sub InitializeComponent()
    Me.pnGrck = New NTSInformatica.NTSPanel
    Me.lbErr = New NTSInformatica.NTSLabel
    Me.cmdCancel = New NTSInformatica.NTSButton
    Me.cmdOK = New NTSInformatica.NTSButton
    Me.fr1 = New NTSInformatica.NTSGroupBox
    Me.lbCurr = New NTSInformatica.NTSLabel
    Me.lb2 = New NTSInformatica.NTSLabel
    Me.lb1 = New NTSInformatica.NTSLabel
    CType(Me.pnGrck, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrck.SuspendLayout()
    CType(Me.fr1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fr1.SuspendLayout()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(231, Byte), Integer))
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'pnGrck
    '
    Me.pnGrck.AllowDrop = True
    Me.pnGrck.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrck.Appearance.Options.UseBackColor = True
    Me.pnGrck.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGrck.Controls.Add(Me.lbErr)
    Me.pnGrck.Controls.Add(Me.cmdCancel)
    Me.pnGrck.Controls.Add(Me.cmdOK)
    Me.pnGrck.Controls.Add(Me.fr1)
    Me.pnGrck.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGrck.Location = New System.Drawing.Point(0, -1)
    Me.pnGrck.Name = "pnGrck"
    Me.pnGrck.Size = New System.Drawing.Size(476, 205)
    Me.pnGrck.TabIndex = 1
    Me.pnGrck.Text = "NtsPanel1"
    '
    'lbErr
    '
    Me.lbErr.BackColor = System.Drawing.Color.Transparent
    Me.lbErr.Location = New System.Drawing.Point(392, 163)
    Me.lbErr.Name = "lbErr"
    Me.lbErr.NTSDbField = ""
    Me.lbErr.Size = New System.Drawing.Size(72, 13)
    Me.lbErr.TabIndex = 4
    '
    'cmdCancel
    '
    Me.cmdCancel.Location = New System.Drawing.Point(385, 47)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(80, 26)
    Me.cmdCancel.TabIndex = 3
    Me.cmdCancel.Text = "Chiudi"
    '
    'cmdOK
    '
    Me.cmdOK.Location = New System.Drawing.Point(384, 13)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(80, 26)
    Me.cmdOK.TabIndex = 2
    Me.cmdOK.Text = "Procedi"
    '
    'fr1
    '
    Me.fr1.AllowDrop = True
    Me.fr1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fr1.Appearance.Options.UseBackColor = True
    Me.fr1.Controls.Add(Me.lbCurr)
    Me.fr1.Controls.Add(Me.lb2)
    Me.fr1.Controls.Add(Me.lb1)
    Me.fr1.Cursor = System.Windows.Forms.Cursors.Default
    Me.fr1.Location = New System.Drawing.Point(12, 13)
    Me.fr1.Name = "fr1"
    Me.fr1.Size = New System.Drawing.Size(366, 180)
    Me.fr1.TabIndex = 1
    Me.fr1.Text = "Controllo"
    '
    'lbCurr
    '
    Me.lbCurr.BackColor = System.Drawing.Color.Transparent
    Me.lbCurr.Location = New System.Drawing.Point(14, 150)
    Me.lbCurr.Name = "lbCurr"
    Me.lbCurr.NTSDbField = ""
    Me.lbCurr.Size = New System.Drawing.Size(347, 24)
    Me.lbCurr.TabIndex = 2
    Me.lbCurr.Text = "Nessuna."
    '
    'lb2
    '
    Me.lb2.AutoSize = True
    Me.lb2.BackColor = System.Drawing.Color.Transparent
    Me.lb2.Location = New System.Drawing.Point(14, 115)
    Me.lb2.Name = "lb2"
    Me.lb2.NTSDbField = ""
    Me.lb2.Size = New System.Drawing.Size(102, 13)
    Me.lb2.TabIndex = 1
    Me.lb2.Text = "Operazione in corso"
    '
    'lb1
    '
    Me.lb1.BackColor = System.Drawing.Color.Transparent
    Me.lb1.Location = New System.Drawing.Point(14, 34)
    Me.lb1.Name = "lb1"
    Me.lb1.NTSDbField = ""
    Me.lb1.Size = New System.Drawing.Size(318, 64)
    Me.lb1.TabIndex = 0
    Me.lb1.Text = "Questa funzione effettua un controllo sull'integrità del Registro di Business, in" & _
        " particolare verifica la corretta indicazione delle dipendenze (ditta, operatore" & _
        " e documento) sulle proprietà."
    '
    'FRM__GRCK
    '
    Me.ClientSize = New System.Drawing.Size(476, 204)
    Me.Controls.Add(Me.pnGrck)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.Name = "FRM__GRCK"
    Me.Text = "Controllo integrità Registro di Business"
    CType(Me.pnGrck, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrck.ResumeLayout(False)
    CType(Me.fr1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fr1.ResumeLayout(False)
    Me.fr1.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try





      '-------------------------------------------------
      'chiamo lo script per inizializzare i controlli caricati con source ext
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub
  Public Overridable Sub InitEntity(ByRef cleGreg As CLE__GREG)
    oCleGreg = cleGreg
    AddHandler oCleGreg.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overloads Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    '---------------------------------
    'questa funzione riceve gli eventi dall'ENTITY: rimappata rispetto a quella standard di FRM__CHILD
    'prima eseguo quella standard
    Dim strTmp() As String
    Dim i As Integer = 0

    If Not IsMyThrowRemoteEvent() Then Return 'il messaggio non è per questa form ...
    MyBase.GestisciEventiEntity(sender, e)

    Try
      '---------------------------------
      'adesso gestisco le specifiche
      'devo inserire delle funzioni qui sotto per fare in modo che al variare di dati nell'entity delle informazioni 
      'legate all'interfaccia grafica (ui) vengano allineate a quanto richiesto dall'entity

      If e.TipoEvento.Length < 10 Then Return
      strTmp = e.TipoEvento.Split(CType("|", Char))
      For i = 0 To strTmp.Length - 1
        Select Case strTmp(i).Substring(0, 10)
          Case "NUMERRORI:"
            lbErr.Text = strTmp(i).Substring(10)
            Me.Refresh()
          Case "OPERAZCOR:"
            lbCurr.Text = strTmp(i).Substring(10)
            Me.Refresh()
        End Select
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "EventiForm"
  Public Overridable Sub FRM__GRCK_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      Me.Cursor = Cursors.Default

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
    Try
      oCleGreg.LogStart("BN__GREG", "Scansione Registro di Business")
      oCleGreg.ControllaRegistro()
      oCleGreg.LogStop()
      If oCleGreg.LogError = True Then
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127940796626250000, "Esistono dei messaggi nel file di log. Visualizzare il file?")) = Windows.Forms.DialogResult.Yes Then
          NTSProcessStart("notepad", oCleGreg.LogFileName)
        End If
      Else
        oApp.MsgBoxErr(oApp.Tr(Me, 128334535256579000, "Nessuna anomalia rilevata."))
      End If
    Catch ex As Exception
      oCleGreg.LogStop()
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

End Class