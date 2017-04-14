Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__DUAR
#Region "Variabili"
  Public oCallParams As CLE__CLDP
  Public oCleCaum As CLEMGCAUM
  Public bOk As Boolean
  Public strCodDupl As String = ""
#End Region

#Region "Controlli"
  Public WithEvents pnNuovo As NTSInformatica.NTSPanel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents lbCod As NTSInformatica.NTSLabel
  Public WithEvents edCod As NTSInformatica.NTSTextBoxNum
  Public WithEvents cmdOk As NTSInformatica.NTSButton
#End Region

#Region "Init"
  Public Overridable Sub InitializeComponent()
    Me.pnNuovo = New NTSInformatica.NTSPanel
    Me.edCod = New NTSInformatica.NTSTextBoxNum
    Me.lbCod = New NTSInformatica.NTSLabel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnNuovo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnNuovo.SuspendLayout()
    CType(Me.edCod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'pnNuovo
    '
    Me.pnNuovo.AllowDrop = True
    Me.pnNuovo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnNuovo.Appearance.Options.UseBackColor = True
    Me.pnNuovo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnNuovo.Controls.Add(Me.edCod)
    Me.pnNuovo.Controls.Add(Me.lbCod)
    Me.pnNuovo.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnNuovo.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnNuovo.Location = New System.Drawing.Point(0, 0)
    Me.pnNuovo.Name = "pnNuovo"
    Me.pnNuovo.NTSActiveTrasparency = True
    Me.pnNuovo.Size = New System.Drawing.Size(285, 84)
    Me.pnNuovo.TabIndex = 12
    Me.pnNuovo.Text = "NtsPanel1"
    '
    'edCod
    '
    Me.edCod.Location = New System.Drawing.Point(130, 11)
    Me.edCod.Name = "edCod"
    Me.edCod.NTSDbField = ""
    Me.edCod.NTSFormat = "0"
    Me.edCod.NTSForzaVisZoom = False
    Me.edCod.NTSOldValue = ""
    Me.edCod.Properties.Appearance.Options.UseTextOptions = True
    Me.edCod.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCod.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCod.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCod.Properties.AutoHeight = False
    Me.edCod.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCod.Properties.MaxLength = 65536
    Me.edCod.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCod.Size = New System.Drawing.Size(70, 20)
    Me.edCod.TabIndex = 583
    '
    'lbCod
    '
    Me.lbCod.AutoSize = True
    Me.lbCod.BackColor = System.Drawing.Color.Transparent
    Me.lbCod.Location = New System.Drawing.Point(13, 14)
    Me.lbCod.Name = "lbCod"
    Me.lbCod.NTSDbField = ""
    Me.lbCod.Size = New System.Drawing.Size(111, 13)
    Me.lbCod.TabIndex = 582
    Me.lbCod.Text = "Codice causale nuovo"
    Me.lbCod.Tooltip = ""
    Me.lbCod.UseMnemonic = False
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(291, 39)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(136, 26)
    Me.cmdAnnulla.TabIndex = 10
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImagePath = ""
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(291, 11)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.NTSContextMenu = Nothing
    Me.cmdOk.Size = New System.Drawing.Size(136, 26)
    Me.cmdOk.TabIndex = 11
    Me.cmdOk.Text = "&Conferma"
    '
    'FRM__DUAR
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(436, 84)
    Me.Controls.Add(Me.pnNuovo)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.HelpContext = ""
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__DUAR"
    Me.Text = "DUPLICAZIONE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnNuovo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnNuovo.ResumeLayout(False)
    Me.pnNuovo.PerformLayout()
    CType(Me.edCod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

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

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      edCod.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130722698207846286, "Codice causale"), tabcaum)

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
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRM__DUAR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      AddHandler oCleCaum.RemoteEvent, AddressOf GestisciEventiEntity

      edCod.Text = "0"

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '--------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
      GctlApplicaDefaultValue()

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRM__DUAR_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Dim ctrlTmp As Control = Nothing
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

#Region "Zoom / ALT+F3 / ALT+F3"
  Public Overridable Sub Zoom()
    Dim ctlLastControl As Control
    Dim ctrlTmp As Control = Nothing
    Dim oParam As New CLE__PATB

    Try
      ctlLastControl = NTSFindControlFocused(Me)
      If ctlLastControl Is Nothing Then Return

      '------------------------------------
      'zoom standard di textbox e griglia
      'SendKeys.SendWait("{F5}") 'se faccio questa riga va in un loop infinito....
      NTSCallStandardZoom()

      '------------------------------------
      'riporto il focus sul controllo che ha chiamato lo zoom
      ctlLastControl.Focus()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi Controlii"
  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      If Not oCleCaum.CheckCod(edCod.Text) Then
        Exit Sub
      Else
        strCodDupl = edCod.Text
        bOk = True
        Me.Close()
      End If

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      bOk = False
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

End Class