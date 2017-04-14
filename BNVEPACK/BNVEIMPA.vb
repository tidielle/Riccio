Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVEIMPA

#Region "Dichiarazione variabili"
  Public oClePack As CLEVEPACK
  Public bOk As Boolean = False
  Public oCallParams As CLE__CLDP
  Public strPrefissoNumeroPacco As String = ""
  Public strCodicePacco As String = ""
  Public strDescPacco As String = ""
  Private components As System.ComponentModel.IComponent
  Public strIdPallet As String = ""

  Public WithEvents lbCodpac As NTSInformatica.NTSLabel
  Public WithEvents lbDescpac As NTSInformatica.NTSLabel
  Public WithEvents edCodpac As NTSInformatica.NTSTextBoxStr
  Public WithEvents edDescpac As NTSInformatica.NTSTextBoxStr
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdGenera As NTSInformatica.NTSButton
  Public WithEvents pnImpa As NTSInformatica.NTSPanel
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
  Public Overridable Sub InitEntity(ByRef clePack As CLEVEPACK)
    oClePack = clePack
    AddHandler oClePack.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub
  Public Sub InitializeComponent()
    Me.lbCodpac = New NTSInformatica.NTSLabel
    Me.lbDescpac = New NTSInformatica.NTSLabel
    Me.edCodpac = New NTSInformatica.NTSTextBoxStr
    Me.edDescpac = New NTSInformatica.NTSTextBoxStr
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdGenera = New NTSInformatica.NTSButton
    Me.pnImpa = New NTSInformatica.NTSPanel
    Me.edIdPallet = New NTSInformatica.NTSTextBoxStr
    Me.lbIdPallet = New NTSInformatica.NTSLabel
    CType(Me.edCodpac.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDescpac.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnImpa, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnImpa.SuspendLayout()
    CType(Me.edIdPallet.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'lbCodpac
    '
    Me.lbCodpac.AutoSize = True
    Me.lbCodpac.BackColor = System.Drawing.Color.Transparent
    Me.lbCodpac.Location = New System.Drawing.Point(12, 15)
    Me.lbCodpac.Name = "lbCodpac"
    Me.lbCodpac.NTSDbField = ""
    Me.lbCodpac.Size = New System.Drawing.Size(70, 13)
    Me.lbCodpac.TabIndex = 27
    Me.lbCodpac.Text = "Codice Pacco"
    Me.lbCodpac.Tooltip = ""
    Me.lbCodpac.UseMnemonic = False
    '
    'lbDescpac
    '
    Me.lbDescpac.AutoSize = True
    Me.lbDescpac.BackColor = System.Drawing.Color.Transparent
    Me.lbDescpac.Location = New System.Drawing.Point(12, 41)
    Me.lbDescpac.Name = "lbDescpac"
    Me.lbDescpac.NTSDbField = ""
    Me.lbDescpac.Size = New System.Drawing.Size(92, 13)
    Me.lbDescpac.TabIndex = 28
    Me.lbDescpac.Text = "Descrizione Pacco"
    Me.lbDescpac.Tooltip = ""
    Me.lbDescpac.UseMnemonic = False
    '
    'edCodpac
    '
    Me.edCodpac.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodpac.Location = New System.Drawing.Point(3, 3)
    Me.edCodpac.Name = "edCodpac"
    Me.edCodpac.NTSDbField = ""
    Me.edCodpac.NTSForzaVisZoom = False
    Me.edCodpac.NTSOldValue = ""
    Me.edCodpac.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodpac.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodpac.Properties.AutoHeight = False
    Me.edCodpac.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodpac.Properties.MaxLength = 65536
    Me.edCodpac.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodpac.Size = New System.Drawing.Size(184, 20)
    Me.edCodpac.TabIndex = 29
    '
    'edDescpac
    '
    Me.edDescpac.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDescpac.Location = New System.Drawing.Point(3, 29)
    Me.edDescpac.Name = "edDescpac"
    Me.edDescpac.NTSDbField = ""
    Me.edDescpac.NTSForzaVisZoom = False
    Me.edDescpac.NTSOldValue = ""
    Me.edDescpac.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDescpac.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDescpac.Properties.AutoHeight = False
    Me.edDescpac.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDescpac.Properties.MaxLength = 65536
    Me.edDescpac.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDescpac.Size = New System.Drawing.Size(184, 20)
    Me.edDescpac.TabIndex = 30
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(300, 9)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.NTSContextMenu = Nothing
    Me.cmdOk.Size = New System.Drawing.Size(103, 23)
    Me.cmdOk.TabIndex = 31
    Me.cmdOk.Text = "Ok"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(300, 38)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(103, 23)
    Me.cmdAnnulla.TabIndex = 32
    Me.cmdAnnulla.Text = "Annulla"
    '
    'cmdGenera
    '
    Me.cmdGenera.ImageText = ""
    Me.cmdGenera.Location = New System.Drawing.Point(300, 67)
    Me.cmdGenera.Name = "cmdGenera"
    Me.cmdGenera.NTSContextMenu = Nothing
    Me.cmdGenera.Size = New System.Drawing.Size(103, 23)
    Me.cmdGenera.TabIndex = 33
    Me.cmdGenera.Text = "Genera cod. pacco"
    '
    'pnImpa
    '
    Me.pnImpa.AllowDrop = True
    Me.pnImpa.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnImpa.Appearance.Options.UseBackColor = True
    Me.pnImpa.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnImpa.Controls.Add(Me.edIdPallet)
    Me.pnImpa.Controls.Add(Me.edCodpac)
    Me.pnImpa.Controls.Add(Me.edDescpac)
    Me.pnImpa.Cursor = System.Windows.Forms.Cursors.Hand
    Me.pnImpa.Location = New System.Drawing.Point(103, 9)
    Me.pnImpa.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnImpa.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnImpa.Name = "pnImpa"
    Me.pnImpa.NTSActiveTrasparency = True
    Me.pnImpa.Size = New System.Drawing.Size(191, 81)
    Me.pnImpa.TabIndex = 34
    Me.pnImpa.Text = "pnImpa"
    '
    'edIdPallet
    '
    Me.edIdPallet.Cursor = System.Windows.Forms.Cursors.Default
    Me.edIdPallet.Location = New System.Drawing.Point(3, 55)
    Me.edIdPallet.Name = "edIdPallet"
    Me.edIdPallet.NTSDbField = ""
    Me.edIdPallet.NTSForzaVisZoom = False
    Me.edIdPallet.NTSOldValue = ""
    Me.edIdPallet.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edIdPallet.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edIdPallet.Properties.AutoHeight = False
    Me.edIdPallet.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edIdPallet.Properties.MaxLength = 65536
    Me.edIdPallet.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edIdPallet.Size = New System.Drawing.Size(184, 20)
    Me.edIdPallet.TabIndex = 31
    '
    'lbIdPallet
    '
    Me.lbIdPallet.AutoSize = True
    Me.lbIdPallet.BackColor = System.Drawing.Color.Transparent
    Me.lbIdPallet.Location = New System.Drawing.Point(12, 67)
    Me.lbIdPallet.Name = "lbIdPallet"
    Me.lbIdPallet.NTSDbField = ""
    Me.lbIdPallet.Size = New System.Drawing.Size(46, 13)
    Me.lbIdPallet.TabIndex = 35
    Me.lbIdPallet.Text = "Id Pallet"
    Me.lbIdPallet.Tooltip = ""
    Me.lbIdPallet.UseMnemonic = False
    '
    'FRMVEIMPA
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(413, 95)
    Me.Controls.Add(Me.lbIdPallet)
    Me.Controls.Add(Me.pnImpa)
    Me.Controls.Add(Me.cmdGenera)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.Controls.Add(Me.lbDescpac)
    Me.Controls.Add(Me.lbCodpac)
    Me.Name = "FRMVEIMPA"
    Me.Text = "IMPOSTAZIONE CODICE/DESCRIZIONE PACCO"
    CType(Me.edCodpac.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDescpac.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnImpa, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnImpa.ResumeLayout(False)
    CType(Me.edIdPallet.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      edDescpac.NTSSetParam(oMenu, oApp.Tr(Me, 128714082461113975, "Descrizione pacco"), 40, True)
      edCodpac.NTSSetParam(oMenu, oApp.Tr(Me, 128714082461270226, "Codice pacco"), 20, True)
      edIdPallet.NTSSetParam(oMenu, oApp.Tr(Me, 130409185651543932, "Id Pallet"), 18)

      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRMVEIMPA_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      InitControls()

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

#Region "Eventi Click"
  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      If edCodpac.Text.Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128714067300835392, "Il 'Codice Pacco' non può essere vuoto."))
        Return
      End If

      bOk = True

      strCodicePacco = edCodpac.Text
      strDescPacco = edDescpac.Text
      strIdPallet = edIdPallet.Text

      Me.Close()
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

  Public Overridable Sub cmdGenera_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGenera.Click
    Dim lCod As Integer = 0
    Dim strTmp As String = ""
    Try
      If Not oClePack.NumaImpa(lCod) Then Return

      edCodpac.Text = strPrefissoNumeroPacco & NTSCStr(lCod).PadLeft(9).Replace(" ", "0")

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edDescpac_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles edDescpac.KeyDown
    Try
      If e.KeyCode = Keys.Enter Then cmdOk_Click(Me, Nothing)
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub
#End Region
End Class
