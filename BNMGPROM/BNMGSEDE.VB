#Region "Import"

Imports System.Data
Imports NTSInformatica.CLN__STD

#End Region

Public Class FRMMGSEDE

#Region "Variabili"

  Public WithEvents cmdConferma As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents pnSede As NTSInformatica.NTSPanel
  Public WithEvents lbTb_coddpro As NTSInformatica.NTSLabel
  Public WithEvents edTb_coddpro As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_desdpro As NTSInformatica.NTSLabel

  Public oCallParams As CLE__CLDP
  Public oCleSede As CLEMGPROM

  Public WithEvents ckAzzera As NTSInformatica.NTSCheckBox

  Public bOk As Boolean

#End Region

#Region "InizializeComponent"
  Public Overridable Sub InitializeComponent()
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.lbTb_coddpro = New NTSInformatica.NTSLabel
    Me.pnSede = New NTSInformatica.NTSPanel
    Me.ckAzzera = New NTSInformatica.NTSCheckBox
    Me.lbTb_desdpro = New NTSInformatica.NTSLabel
    Me.edTb_coddpro = New NTSInformatica.NTSTextBoxStr
    CType(Me.pnSede, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSede.SuspendLayout()
    CType(Me.ckAzzera.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_coddpro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.cmdAnnulla.Location = New System.Drawing.Point(377, 40)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(93, 22)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdConferma
    '
    Me.cmdConferma.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer))
    Me.cmdConferma.Appearance.Options.UseForeColor = True
    Me.cmdConferma.Location = New System.Drawing.Point(377, 12)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.Size = New System.Drawing.Size(93, 22)
    Me.cmdConferma.TabIndex = 2
    Me.cmdConferma.Text = "&Conferma"
    '
    'lbTb_coddpro
    '
    Me.lbTb_coddpro.AutoSize = True
    Me.lbTb_coddpro.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_coddpro.Location = New System.Drawing.Point(12, 17)
    Me.lbTb_coddpro.Name = "lbTb_coddpro"
    Me.lbTb_coddpro.NTSDbField = ""
    Me.lbTb_coddpro.Size = New System.Drawing.Size(108, 13)
    Me.lbTb_coddpro.TabIndex = 5
    Me.lbTb_coddpro.Text = "Dettaglio promozione"
    '
    'pnSede
    '
    Me.pnSede.AllowDrop = True
    Me.pnSede.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSede.Appearance.Options.UseBackColor = True
    Me.pnSede.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSede.Controls.Add(Me.ckAzzera)
    Me.pnSede.Controls.Add(Me.lbTb_desdpro)
    Me.pnSede.Controls.Add(Me.cmdAnnulla)
    Me.pnSede.Controls.Add(Me.edTb_coddpro)
    Me.pnSede.Controls.Add(Me.cmdConferma)
    Me.pnSede.Controls.Add(Me.lbTb_coddpro)
    Me.pnSede.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnSede.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnSede.Location = New System.Drawing.Point(0, 0)
    Me.pnSede.Name = "pnSede"
    Me.pnSede.Size = New System.Drawing.Size(482, 107)
    Me.pnSede.TabIndex = 9
    Me.pnSede.Text = "NtsPanel1"
    '
    'ckAzzera
    '
    Me.ckAzzera.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAzzera.Location = New System.Drawing.Point(15, 74)
    Me.ckAzzera.Name = "ckAzzera"
    Me.ckAzzera.NTSCheckValue = "S"
    Me.ckAzzera.NTSUnCheckValue = "N"
    Me.ckAzzera.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAzzera.Properties.Appearance.Options.UseBackColor = True
    Me.ckAzzera.Properties.Caption = "Azzera il codice dettaglio promozione sulle righe selezionate"
    Me.ckAzzera.Size = New System.Drawing.Size(317, 18)
    Me.ckAzzera.TabIndex = 8
    '
    'lbTb_desdpro
    '
    Me.lbTb_desdpro.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_desdpro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbTb_desdpro.Location = New System.Drawing.Point(15, 42)
    Me.lbTb_desdpro.Name = "lbTb_desdpro"
    Me.lbTb_desdpro.NTSDbField = ""
    Me.lbTb_desdpro.Size = New System.Drawing.Size(317, 20)
    Me.lbTb_desdpro.TabIndex = 7
    '
    'edTb_coddpro
    '
    Me.edTb_coddpro.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_coddpro.Location = New System.Drawing.Point(126, 14)
    Me.edTb_coddpro.Name = "edTb_coddpro"
    Me.edTb_coddpro.NTSDbField = ""
    Me.edTb_coddpro.NTSForzaVisZoom = False
    Me.edTb_coddpro.NTSOldValue = ""
    Me.edTb_coddpro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_coddpro.Properties.MaxLength = 65536
    Me.edTb_coddpro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_coddpro.Size = New System.Drawing.Size(206, 20)
    Me.edTb_coddpro.TabIndex = 6
    '
    'FRMMGSEDE
    '
    Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(254, Byte), Integer))
    Me.Appearance.Options.UseBackColor = True
    Me.ClientSize = New System.Drawing.Size(482, 107)
    Me.Controls.Add(Me.pnSede)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMMGSEDE"
    Me.Text = "SELEZIONA CODICE DETTAGLIO PROMOZIONE"
    CType(Me.pnSede, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSede.ResumeLayout(False)
    Me.pnSede.PerformLayout()
    CType(Me.ckAzzera.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_coddpro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
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

    Return True
  End Function

  Public Overridable Sub InitEntity(ByRef oClePuceRef As CLEMGPROM)
    Try
      oCleSede = oClePuceRef
      AddHandler oCleSede.RemoteEvent, AddressOf GestisciEventiEntity
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try

      edTb_coddpro.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128439089619218750, "Dettaglio promozione"), tabdpro, True)

      '-------------------------------------------------
      'Chiamo lo script per inizializzare i controlli caricati con source ext
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Public Overridable Sub Zoom()
    Try
      'per eventuali altri controlli caricati al volo
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      '------------------------------------
      'zoom standard di textbox e griglia
      NTSCallStandardZoom()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function CheckCoddpro() As Boolean
    Try
      If ckAzzera.Checked Then Return True
      If oCleSede.GetDesCoddpro(edTb_coddpro.Text, lbTb_desdpro.Text) Then
        Return True
      End If
      lbTb_desdpro.Text = ""
      edTb_coddpro.Text = ""
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

#Region "EventiForm"

  Public Overridable Sub FRMCEAPCE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'Predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'Inizializazione ambiente personale form
      bOk = False
      cmdConferma.Enabled = False

      '-------------------------------------------------
      'Sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMCEAPCE_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Try
      '--------------------------------------------
      'gestione dello zoom:
      'eseguo la Zoom, tanto se per il controllo non deve venir eseguito uno zoom particolare, la routine non fa nulla e viene processato lo zoom standard del controllo, settato con la NTSSetParamZoom
      If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
        Zoom()
        e.Handled = True    'altrimenti anche il controllo riceve l'F5 e la routine ZOOM viene eseguita 2 volte!!!
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

#Region "EventiControlli"

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    Try
      bOk = True
      If ckAzzera.Checked Then
        edTb_coddpro.Text = "0"
      End If
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edTb_codpuce_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTb_coddpro.Validated
    Try
      If edTb_coddpro.Text = "" Then Return
      If edTb_coddpro.Text = "0" Then ckAzzera.CheckState = CheckState.Checked
      If CheckCoddpro() Then
        cmdConferma.Enabled = True
        cmdConferma.Focus()
      Else
        cmdConferma.Enabled = False
      End If
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

  Public Overridable Sub ckAzzera_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckAzzera.CheckedChanged
    Try
      If ckAzzera.Checked Then
        cmdConferma.Enabled = True
        edTb_coddpro.Enabled = False
      Else
        cmdConferma.Enabled = False
        edTb_coddpro.Enabled = True
        If edTb_coddpro.Text = "0" Then edTb_coddpro.Text = ""
      End If
    Catch ex As Exception

    End Try
  End Sub
End Class
