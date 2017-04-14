Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORNUOV
  Public oCallParams As CLE__CLDP
  Public bOk As Boolean = False

  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents opIT As NTSInformatica.NTSRadioButton
  Public WithEvents opOP As NTSInformatica.NTSRadioButton
  Public WithEvents opOF As NTSInformatica.NTSRadioButton
  Public WithEvents pnMain As NTSInformatica.NTSPanel
  Public WithEvents cmdOk As NTSInformatica.NTSButton


  Public Overridable Sub InitializeComponent()
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.opIT = New NTSInformatica.NTSRadioButton
    Me.opOP = New NTSInformatica.NTSRadioButton
    Me.opOF = New NTSInformatica.NTSRadioButton
    Me.pnMain = New NTSInformatica.NTSPanel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opIT.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opOP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opOF.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnMain, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnMain.SuspendLayout()
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
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(161, 41)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(105, 26)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImagePath = ""
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(161, 12)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.NTSContextMenu = Nothing
    Me.cmdOk.Size = New System.Drawing.Size(105, 26)
    Me.cmdOk.TabIndex = 2
    Me.cmdOk.Text = "&Conferma"
    '
    'opIT
    '
    Me.opIT.Cursor = System.Windows.Forms.Cursors.Default
    Me.opIT.Location = New System.Drawing.Point(12, 62)
    Me.opIT.Name = "opIT"
    Me.opIT.NTSCheckValue = "S"
    Me.opIT.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opIT.Properties.Appearance.Options.UseBackColor = True
    Me.opIT.Properties.AutoHeight = False
    Me.opIT.Properties.Caption = "&Impegni di trasferimento"
    Me.opIT.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opIT.Size = New System.Drawing.Size(140, 19)
    Me.opIT.TabIndex = 5
    '
    'opOP
    '
    Me.opOP.Cursor = System.Windows.Forms.Cursors.Default
    Me.opOP.Location = New System.Drawing.Point(12, 37)
    Me.opOP.Name = "opOP"
    Me.opOP.NTSCheckValue = "S"
    Me.opOP.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opOP.Properties.Appearance.Options.UseBackColor = True
    Me.opOP.Properties.AutoHeight = False
    Me.opOP.Properties.Caption = "O&rdini di produzione"
    Me.opOP.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opOP.Size = New System.Drawing.Size(123, 19)
    Me.opOP.TabIndex = 4
    '
    'opOF
    '
    Me.opOF.Cursor = System.Windows.Forms.Cursors.Default
    Me.opOF.EditValue = True
    Me.opOF.Location = New System.Drawing.Point(12, 12)
    Me.opOF.Name = "opOF"
    Me.opOF.NTSCheckValue = "S"
    Me.opOF.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opOF.Properties.Appearance.Options.UseBackColor = True
    Me.opOF.Properties.AutoHeight = False
    Me.opOF.Properties.Caption = "&Ordini a fornitori"
    Me.opOF.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opOF.Size = New System.Drawing.Size(107, 19)
    Me.opOF.TabIndex = 3
    '
    'pnMain
    '
    Me.pnMain.AllowDrop = True
    Me.pnMain.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnMain.Appearance.Options.UseBackColor = True
    Me.pnMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnMain.Controls.Add(Me.opOF)
    Me.pnMain.Controls.Add(Me.opIT)
    Me.pnMain.Controls.Add(Me.opOP)
    Me.pnMain.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnMain.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnMain.Location = New System.Drawing.Point(0, 0)
    Me.pnMain.Name = "pnMain"
    Me.pnMain.NTSActiveTrasparency = True
    Me.pnMain.Size = New System.Drawing.Size(155, 87)
    Me.pnMain.TabIndex = 6
    Me.pnMain.Text = "NtsPanel1"
    '
    'FRMORNUOV
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(274, 87)
    Me.Controls.Add(Me.pnMain)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMORNUOV"
    Me.Text = "NUOVA PROPOSTA D'ORDINE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opIT.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opOP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opOF.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnMain, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnMain.ResumeLayout(False)
    Me.pnMain.PerformLayout()
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

      opOF.NTSSetParam(oMenu, oApp.Tr(Me, 128617952083437500, "Ordini fornitore"), "OF")
      opOP.NTSSetParam(oMenu, oApp.Tr(Me, 128617952307187500, "Ordini di produzione"), "OP")
      opIT.NTSSetParam(oMenu, oApp.Tr(Me, 128617952319062500, "Impegni di trasferimento"), "IT")

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

  Public Overridable Sub FRMORSEOR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      GctlSetRoules()
      GctlApplicaDefaultValue()

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      bOk = True
      Me.Close()
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
End Class
