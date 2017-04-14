Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVESPEX
  Public oCallParams As CLE__CLDP
  Public bOk As Boolean
  Public oCleBoll As CLEVEBOLL
  Public strTiporkOrig As String = ""     'utilizzato solo in duplicadoc

  Public WithEvents edEt_cosdog As NTSInformatica.NTSTextBoxNum
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents lbEt_cosdog As NTSInformatica.NTSLabel
  Public WithEvents lbEt_cosvar3 As NTSInformatica.NTSLabel
  Public WithEvents lbEt_cosvar2 As NTSInformatica.NTSLabel
  Public WithEvents lbEt_cosvar1 As NTSInformatica.NTSLabel
  Public WithEvents lbEt_costrasp As NTSInformatica.NTSLabel
  Public WithEvents edEt_cosvar3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edEt_cosvar2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edEt_cosvar1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edEt_costrasp As NTSInformatica.NTSTextBoxNum
  Public WithEvents pnSpese As NTSInformatica.NTSPanel


  Public Overridable Sub InitializeComponent()
    Me.edEt_cosdog = New NTSInformatica.NTSTextBoxNum
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.lbEt_cosdog = New NTSInformatica.NTSLabel
    Me.pnSpese = New NTSInformatica.NTSPanel
    Me.edEt_costrasp = New NTSInformatica.NTSTextBoxNum
    Me.edEt_cosvar1 = New NTSInformatica.NTSTextBoxNum
    Me.edEt_cosvar2 = New NTSInformatica.NTSTextBoxNum
    Me.edEt_cosvar3 = New NTSInformatica.NTSTextBoxNum
    Me.lbEt_costrasp = New NTSInformatica.NTSLabel
    Me.lbEt_cosvar1 = New NTSInformatica.NTSLabel
    Me.lbEt_cosvar2 = New NTSInformatica.NTSLabel
    Me.lbEt_cosvar3 = New NTSInformatica.NTSLabel
    CType(Me.edEt_cosdog.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnSpese, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSpese.SuspendLayout()
    CType(Me.edEt_costrasp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_cosvar1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_cosvar2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_cosvar3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'edEt_cosdog
    '
    Me.edEt_cosdog.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_cosdog.EditValue = "0"
    Me.edEt_cosdog.Location = New System.Drawing.Point(113, 4)
    Me.edEt_cosdog.Name = "edEt_cosdog"
    Me.edEt_cosdog.NTSDbField = ""
    Me.edEt_cosdog.NTSFormat = "0"
    Me.edEt_cosdog.NTSForzaVisZoom = False
    Me.edEt_cosdog.NTSOldValue = ""
    Me.edEt_cosdog.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edEt_cosdog.Properties.Appearance.Options.UseBackColor = True
    Me.edEt_cosdog.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_cosdog.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_cosdog.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_cosdog.Properties.MaxLength = 9
    Me.edEt_cosdog.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_cosdog.Size = New System.Drawing.Size(114, 20)
    Me.edEt_cosdog.TabIndex = 8
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Location = New System.Drawing.Point(265, 36)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(105, 22)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.Location = New System.Drawing.Point(265, 7)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(105, 22)
    Me.cmdOk.TabIndex = 2
    Me.cmdOk.Text = "&Conferma"
    '
    'lbEt_cosdog
    '
    Me.lbEt_cosdog.AutoSize = True
    Me.lbEt_cosdog.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_cosdog.Location = New System.Drawing.Point(3, 8)
    Me.lbEt_cosdog.Name = "lbEt_cosdog"
    Me.lbEt_cosdog.NTSDbField = ""
    Me.lbEt_cosdog.Size = New System.Drawing.Size(110, 13)
    Me.lbEt_cosdog.TabIndex = 3
    Me.lbEt_cosdog.Text = "Spese sdoganamento"
    '
    'pnSpese
    '
    Me.pnSpese.AllowDrop = True
    Me.pnSpese.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSpese.Appearance.Options.UseBackColor = True
    Me.pnSpese.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSpese.Controls.Add(Me.lbEt_cosvar3)
    Me.pnSpese.Controls.Add(Me.lbEt_cosvar2)
    Me.pnSpese.Controls.Add(Me.lbEt_cosvar1)
    Me.pnSpese.Controls.Add(Me.lbEt_costrasp)
    Me.pnSpese.Controls.Add(Me.edEt_cosvar3)
    Me.pnSpese.Controls.Add(Me.edEt_cosvar2)
    Me.pnSpese.Controls.Add(Me.edEt_cosvar1)
    Me.pnSpese.Controls.Add(Me.edEt_costrasp)
    Me.pnSpese.Controls.Add(Me.lbEt_cosdog)
    Me.pnSpese.Controls.Add(Me.edEt_cosdog)
    Me.pnSpese.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnSpese.Location = New System.Drawing.Point(2, 8)
    Me.pnSpese.Name = "pnSpese"
    Me.pnSpese.Size = New System.Drawing.Size(230, 133)
    Me.pnSpese.TabIndex = 9
    Me.pnSpese.Text = "NtsPanel1"
    '
    'edEt_costrasp
    '
    Me.edEt_costrasp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_costrasp.EditValue = "0"
    Me.edEt_costrasp.Location = New System.Drawing.Point(113, 30)
    Me.edEt_costrasp.Name = "edEt_costrasp"
    Me.edEt_costrasp.NTSDbField = ""
    Me.edEt_costrasp.NTSFormat = "0"
    Me.edEt_costrasp.NTSForzaVisZoom = False
    Me.edEt_costrasp.NTSOldValue = ""
    Me.edEt_costrasp.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edEt_costrasp.Properties.Appearance.Options.UseBackColor = True
    Me.edEt_costrasp.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_costrasp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_costrasp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_costrasp.Properties.MaxLength = 9
    Me.edEt_costrasp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_costrasp.Size = New System.Drawing.Size(114, 20)
    Me.edEt_costrasp.TabIndex = 9
    '
    'edEt_cosvar1
    '
    Me.edEt_cosvar1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_cosvar1.EditValue = "0"
    Me.edEt_cosvar1.Location = New System.Drawing.Point(113, 56)
    Me.edEt_cosvar1.Name = "edEt_cosvar1"
    Me.edEt_cosvar1.NTSDbField = ""
    Me.edEt_cosvar1.NTSFormat = "0"
    Me.edEt_cosvar1.NTSForzaVisZoom = False
    Me.edEt_cosvar1.NTSOldValue = ""
    Me.edEt_cosvar1.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edEt_cosvar1.Properties.Appearance.Options.UseBackColor = True
    Me.edEt_cosvar1.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_cosvar1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_cosvar1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_cosvar1.Properties.MaxLength = 9
    Me.edEt_cosvar1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_cosvar1.Size = New System.Drawing.Size(114, 20)
    Me.edEt_cosvar1.TabIndex = 10
    '
    'edEt_cosvar2
    '
    Me.edEt_cosvar2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_cosvar2.EditValue = "0"
    Me.edEt_cosvar2.Location = New System.Drawing.Point(113, 82)
    Me.edEt_cosvar2.Name = "edEt_cosvar2"
    Me.edEt_cosvar2.NTSDbField = ""
    Me.edEt_cosvar2.NTSFormat = "0"
    Me.edEt_cosvar2.NTSForzaVisZoom = False
    Me.edEt_cosvar2.NTSOldValue = ""
    Me.edEt_cosvar2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edEt_cosvar2.Properties.Appearance.Options.UseBackColor = True
    Me.edEt_cosvar2.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_cosvar2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_cosvar2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_cosvar2.Properties.MaxLength = 9
    Me.edEt_cosvar2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_cosvar2.Size = New System.Drawing.Size(114, 20)
    Me.edEt_cosvar2.TabIndex = 11
    '
    'edEt_cosvar3
    '
    Me.edEt_cosvar3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_cosvar3.EditValue = "0"
    Me.edEt_cosvar3.Location = New System.Drawing.Point(113, 108)
    Me.edEt_cosvar3.Name = "edEt_cosvar3"
    Me.edEt_cosvar3.NTSDbField = ""
    Me.edEt_cosvar3.NTSFormat = "0"
    Me.edEt_cosvar3.NTSForzaVisZoom = False
    Me.edEt_cosvar3.NTSOldValue = ""
    Me.edEt_cosvar3.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edEt_cosvar3.Properties.Appearance.Options.UseBackColor = True
    Me.edEt_cosvar3.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_cosvar3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_cosvar3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_cosvar3.Properties.MaxLength = 9
    Me.edEt_cosvar3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_cosvar3.Size = New System.Drawing.Size(114, 20)
    Me.edEt_cosvar3.TabIndex = 12
    '
    'lbEt_costrasp
    '
    Me.lbEt_costrasp.AutoSize = True
    Me.lbEt_costrasp.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_costrasp.Location = New System.Drawing.Point(3, 33)
    Me.lbEt_costrasp.Name = "lbEt_costrasp"
    Me.lbEt_costrasp.NTSDbField = ""
    Me.lbEt_costrasp.Size = New System.Drawing.Size(84, 13)
    Me.lbEt_costrasp.TabIndex = 13
    Me.lbEt_costrasp.Text = "Spese trasporto"
    '
    'lbEt_cosvar1
    '
    Me.lbEt_cosvar1.AutoSize = True
    Me.lbEt_cosvar1.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_cosvar1.Location = New System.Drawing.Point(3, 59)
    Me.lbEt_cosvar1.Name = "lbEt_cosvar1"
    Me.lbEt_cosvar1.NTSDbField = ""
    Me.lbEt_cosvar1.Size = New System.Drawing.Size(84, 13)
    Me.lbEt_cosvar1.TabIndex = 14
    Me.lbEt_cosvar1.Text = "Ulteriori spese 1"
    '
    'lbEt_cosvar2
    '
    Me.lbEt_cosvar2.AutoSize = True
    Me.lbEt_cosvar2.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_cosvar2.Location = New System.Drawing.Point(3, 85)
    Me.lbEt_cosvar2.Name = "lbEt_cosvar2"
    Me.lbEt_cosvar2.NTSDbField = ""
    Me.lbEt_cosvar2.Size = New System.Drawing.Size(84, 13)
    Me.lbEt_cosvar2.TabIndex = 15
    Me.lbEt_cosvar2.Text = "Ulteriori spese 2"
    '
    'lbEt_cosvar3
    '
    Me.lbEt_cosvar3.AutoSize = True
    Me.lbEt_cosvar3.BackColor = System.Drawing.Color.Transparent
    Me.lbEt_cosvar3.Location = New System.Drawing.Point(3, 111)
    Me.lbEt_cosvar3.Name = "lbEt_cosvar3"
    Me.lbEt_cosvar3.NTSDbField = ""
    Me.lbEt_cosvar3.Size = New System.Drawing.Size(84, 13)
    Me.lbEt_cosvar3.TabIndex = 16
    Me.lbEt_cosvar3.Text = "Ulteriori spese 3"
    '
    'FRMVESPEX
    '
    Me.ClientSize = New System.Drawing.Size(382, 142)
    Me.Controls.Add(Me.pnSpese)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMVESPEX"
    Me.Text = "Spese ulteriori extra-documento"
    CType(Me.edEt_cosdog.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnSpese, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSpese.ResumeLayout(False)
    Me.pnSpese.PerformLayout()
    CType(Me.edEt_costrasp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_cosvar1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_cosvar2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_cosvar3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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

    InitializeComponent()
    Me.MinimumSize = Me.Size

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      edEt_cosvar3.NTSSetParam(oMenu, oApp.Tr(Me, 128686223506400000, "Ulteriori spese 3"), oApp.FormatImporti)
      edEt_cosvar2.NTSSetParam(oMenu, oApp.Tr(Me, 128686223506556250, "Ulteriori spese 2"), oApp.FormatImporti)
      edEt_cosvar1.NTSSetParam(oMenu, oApp.Tr(Me, 128686223506712500, "Ulteriori spese 1"), oApp.FormatImporti)
      edEt_costrasp.NTSSetParam(oMenu, oApp.Tr(Me, 128686223506868750, "Spese trasporto"), oApp.FormatImporti)
      edEt_cosdog.NTSSetParam(oMenu, oApp.Tr(Me, 128686223507181250, "Spese sdoganamento"), oApp.FormatImporti)

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

  Public Overridable Sub FRMVESPEX_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      edEt_cosdog.Text = NTSCDec(oCleBoll.dttET.Rows(0)!et_cosdog).ToString
      edEt_costrasp.Text = NTSCDec(oCleBoll.dttET.Rows(0)!et_costrasp).ToString
      edEt_cosvar1.Text = NTSCDec(oCleBoll.dttET.Rows(0)!et_cosvar1).ToString
      edEt_cosvar2.Text = NTSCDec(oCleBoll.dttET.Rows(0)!et_cosvar2).ToString
      edEt_cosvar3.Text = NTSCDec(oCleBoll.dttET.Rows(0)!et_cosvar3).ToString

      '--------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlTipoDoc = oCleBoll.dttET.Rows(0)!et_tipork.ToString
      GctlSetRoules()
      If oCleBoll.bNew Then GctlApplicaDefaultValue()

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      oCleBoll.dttET.Rows(0)!et_cosdog = NTSCDec(edEt_cosdog.Text)
      oCleBoll.dttET.Rows(0)!et_costrasp = NTSCDec(edEt_costrasp.Text)
      oCleBoll.dttET.Rows(0)!et_cosvar1 = NTSCDec(edEt_cosvar1.Text)
      oCleBoll.dttET.Rows(0)!et_cosvar2 = NTSCDec(edEt_cosvar2.Text)
      oCleBoll.dttET.Rows(0)!et_cosvar3 = NTSCDec(edEt_cosvar3.Text)

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
