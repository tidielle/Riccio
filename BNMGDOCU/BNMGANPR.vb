Public Class FRMMGANPR
  Public oCallParams As CLE__CLDP
  Public oCleDocu As CLEMGDOCU

  Private components As System.ComponentModel.IContainer
  Public WithEvents cmdOk As NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents lbDescr1 As NTSInformatica.NTSLabel
  Public WithEvents edDescr1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDescr2 As NTSInformatica.NTSLabel
  Public WithEvents lbIndir As NTSInformatica.NTSLabel
  Public WithEvents lbCitta As NTSInformatica.NTSLabel
  Public WithEvents lbCap As NTSInformatica.NTSLabel
  Public WithEvents lbProv As NTSInformatica.NTSLabel
  Public WithEvents lbCodfis As NTSInformatica.NTSLabel
  Public WithEvents NtsPanel1 As NTSInformatica.NTSPanel
  Public WithEvents edCodfis As NTSInformatica.NTSTextBoxStr
  Public WithEvents edIndir As NTSInformatica.NTSTextBoxStr
  Public WithEvents edCitta As NTSInformatica.NTSTextBoxStr
  Public WithEvents edCap As NTSInformatica.NTSTextBoxStr
  Public WithEvents edProv As NTSInformatica.NTSTextBoxStr
  Public WithEvents edDescr2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents cmdControlla As NTSInformatica.NTSButton

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

      oCallParams = Param

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub InitializeComponent()
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.lbDescr1 = New NTSInformatica.NTSLabel
    Me.edDescr1 = New NTSInformatica.NTSTextBoxStr
    Me.lbDescr2 = New NTSInformatica.NTSLabel
    Me.lbIndir = New NTSInformatica.NTSLabel
    Me.lbCitta = New NTSInformatica.NTSLabel
    Me.lbCap = New NTSInformatica.NTSLabel
    Me.lbProv = New NTSInformatica.NTSLabel
    Me.lbCodfis = New NTSInformatica.NTSLabel
    Me.NtsPanel1 = New NTSInformatica.NTSPanel
    Me.cmdControlla = New NTSInformatica.NTSButton
    Me.edCodfis = New NTSInformatica.NTSTextBoxStr
    Me.edIndir = New NTSInformatica.NTSTextBoxStr
    Me.edCitta = New NTSInformatica.NTSTextBoxStr
    Me.edCap = New NTSInformatica.NTSTextBoxStr
    Me.edProv = New NTSInformatica.NTSTextBoxStr
    Me.edDescr2 = New NTSInformatica.NTSTextBoxStr
    CType(Me.edDescr1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel1.SuspendLayout()
    CType(Me.edCodfis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edIndir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCitta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edProv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDescr2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'cmdOk
    '
    Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(359, 12)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(79, 24)
    Me.cmdOk.TabIndex = 2
    Me.cmdOk.Text = "OK"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(359, 38)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(79, 24)
    Me.cmdAnnulla.TabIndex = 4
    Me.cmdAnnulla.Text = "Annulla"
    '
    'lbDescr1
    '
    Me.lbDescr1.AutoSize = True
    Me.lbDescr1.BackColor = System.Drawing.Color.Transparent
    Me.lbDescr1.Location = New System.Drawing.Point(12, 15)
    Me.lbDescr1.Name = "lbDescr1"
    Me.lbDescr1.NTSDbField = ""
    Me.lbDescr1.Size = New System.Drawing.Size(72, 13)
    Me.lbDescr1.TabIndex = 5
    Me.lbDescr1.Text = "Nome cliente:"
    Me.lbDescr1.Tooltip = ""
    Me.lbDescr1.UseMnemonic = False
    '
    'edDescr1
    '
    Me.edDescr1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDescr1.Location = New System.Drawing.Point(100, 12)
    Me.edDescr1.Name = "edDescr1"
    Me.edDescr1.NTSDbField = ""
    Me.edDescr1.NTSForzaVisZoom = False
    Me.edDescr1.NTSOldValue = ""
    Me.edDescr1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDescr1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDescr1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDescr1.Properties.MaxLength = 65536
    Me.edDescr1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDescr1.Size = New System.Drawing.Size(242, 20)
    Me.edDescr1.TabIndex = 6
    '
    'lbDescr2
    '
    Me.lbDescr2.AutoSize = True
    Me.lbDescr2.BackColor = System.Drawing.Color.Transparent
    Me.lbDescr2.Location = New System.Drawing.Point(12, 41)
    Me.lbDescr2.Name = "lbDescr2"
    Me.lbDescr2.NTSDbField = ""
    Me.lbDescr2.Size = New System.Drawing.Size(74, 13)
    Me.lbDescr2.TabIndex = 7
    Me.lbDescr2.Text = "Descr. suppl.:"
    Me.lbDescr2.Tooltip = ""
    Me.lbDescr2.UseMnemonic = False
    '
    'lbIndir
    '
    Me.lbIndir.AutoSize = True
    Me.lbIndir.BackColor = System.Drawing.Color.Transparent
    Me.lbIndir.Location = New System.Drawing.Point(12, 67)
    Me.lbIndir.Name = "lbIndir"
    Me.lbIndir.NTSDbField = ""
    Me.lbIndir.Size = New System.Drawing.Size(51, 13)
    Me.lbIndir.TabIndex = 8
    Me.lbIndir.Text = "Indirizzo:"
    Me.lbIndir.Tooltip = ""
    Me.lbIndir.UseMnemonic = False
    '
    'lbCitta
    '
    Me.lbCitta.AutoSize = True
    Me.lbCitta.BackColor = System.Drawing.Color.Transparent
    Me.lbCitta.Location = New System.Drawing.Point(12, 93)
    Me.lbCitta.Name = "lbCitta"
    Me.lbCitta.NTSDbField = ""
    Me.lbCitta.Size = New System.Drawing.Size(34, 13)
    Me.lbCitta.TabIndex = 9
    Me.lbCitta.Text = "Città:"
    Me.lbCitta.Tooltip = ""
    Me.lbCitta.UseMnemonic = False
    '
    'lbCap
    '
    Me.lbCap.AutoSize = True
    Me.lbCap.BackColor = System.Drawing.Color.Transparent
    Me.lbCap.Location = New System.Drawing.Point(12, 119)
    Me.lbCap.Name = "lbCap"
    Me.lbCap.NTSDbField = ""
    Me.lbCap.Size = New System.Drawing.Size(31, 13)
    Me.lbCap.TabIndex = 10
    Me.lbCap.Text = "CAP:"
    Me.lbCap.Tooltip = ""
    Me.lbCap.UseMnemonic = False
    '
    'lbProv
    '
    Me.lbProv.AutoSize = True
    Me.lbProv.BackColor = System.Drawing.Color.Transparent
    Me.lbProv.Location = New System.Drawing.Point(196, 119)
    Me.lbProv.Name = "lbProv"
    Me.lbProv.NTSDbField = ""
    Me.lbProv.Size = New System.Drawing.Size(54, 13)
    Me.lbProv.TabIndex = 11
    Me.lbProv.Text = "Provincia:"
    Me.lbProv.Tooltip = ""
    Me.lbProv.UseMnemonic = False
    '
    'lbCodfis
    '
    Me.lbCodfis.AutoSize = True
    Me.lbCodfis.BackColor = System.Drawing.Color.Transparent
    Me.lbCodfis.Location = New System.Drawing.Point(12, 145)
    Me.lbCodfis.Name = "lbCodfis"
    Me.lbCodfis.NTSDbField = ""
    Me.lbCodfis.Size = New System.Drawing.Size(76, 13)
    Me.lbCodfis.TabIndex = 12
    Me.lbCodfis.Text = "Codice fiscale:"
    Me.lbCodfis.Tooltip = ""
    Me.lbCodfis.UseMnemonic = False
    '
    'NtsPanel1
    '
    Me.NtsPanel1.AllowDrop = True
    Me.NtsPanel1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsPanel1.Appearance.Options.UseBackColor = True
    Me.NtsPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel1.Controls.Add(Me.cmdControlla)
    Me.NtsPanel1.Controls.Add(Me.edCodfis)
    Me.NtsPanel1.Controls.Add(Me.lbCodfis)
    Me.NtsPanel1.Controls.Add(Me.edIndir)
    Me.NtsPanel1.Controls.Add(Me.lbProv)
    Me.NtsPanel1.Controls.Add(Me.edCitta)
    Me.NtsPanel1.Controls.Add(Me.lbCap)
    Me.NtsPanel1.Controls.Add(Me.edCap)
    Me.NtsPanel1.Controls.Add(Me.lbCitta)
    Me.NtsPanel1.Controls.Add(Me.edProv)
    Me.NtsPanel1.Controls.Add(Me.lbIndir)
    Me.NtsPanel1.Controls.Add(Me.edDescr2)
    Me.NtsPanel1.Controls.Add(Me.lbDescr2)
    Me.NtsPanel1.Controls.Add(Me.edDescr1)
    Me.NtsPanel1.Controls.Add(Me.lbDescr1)
    Me.NtsPanel1.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsPanel1.Dock = System.Windows.Forms.DockStyle.Left
    Me.NtsPanel1.Location = New System.Drawing.Point(0, 0)
    Me.NtsPanel1.Name = "NtsPanel1"
    Me.NtsPanel1.Size = New System.Drawing.Size(357, 175)
    Me.NtsPanel1.TabIndex = 13
    Me.NtsPanel1.Text = "NtsPanel1"
    '
    'cmdControlla
    '
    Me.cmdControlla.ImageText = ""
    Me.cmdControlla.Location = New System.Drawing.Point(256, 142)
    Me.cmdControlla.Name = "cmdControlla"
    Me.cmdControlla.Size = New System.Drawing.Size(86, 20)
    Me.cmdControlla.TabIndex = 14
    Me.cmdControlla.Text = "Controlla"
    '
    'edCodfis
    '
    Me.edCodfis.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodfis.Location = New System.Drawing.Point(100, 142)
    Me.edCodfis.Name = "edCodfis"
    Me.edCodfis.NTSDbField = ""
    Me.edCodfis.NTSForzaVisZoom = False
    Me.edCodfis.NTSOldValue = ""
    Me.edCodfis.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodfis.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodfis.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodfis.Properties.MaxLength = 65536
    Me.edCodfis.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodfis.Size = New System.Drawing.Size(150, 20)
    Me.edCodfis.TabIndex = 12
    '
    'edIndir
    '
    Me.edIndir.Cursor = System.Windows.Forms.Cursors.Default
    Me.edIndir.Location = New System.Drawing.Point(100, 64)
    Me.edIndir.Name = "edIndir"
    Me.edIndir.NTSDbField = ""
    Me.edIndir.NTSForzaVisZoom = False
    Me.edIndir.NTSOldValue = ""
    Me.edIndir.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edIndir.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edIndir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edIndir.Properties.MaxLength = 65536
    Me.edIndir.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edIndir.Size = New System.Drawing.Size(242, 20)
    Me.edIndir.TabIndex = 11
    '
    'edCitta
    '
    Me.edCitta.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCitta.Location = New System.Drawing.Point(100, 90)
    Me.edCitta.Name = "edCitta"
    Me.edCitta.NTSDbField = ""
    Me.edCitta.NTSForzaVisZoom = False
    Me.edCitta.NTSOldValue = ""
    Me.edCitta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCitta.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCitta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCitta.Properties.MaxLength = 65536
    Me.edCitta.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCitta.Size = New System.Drawing.Size(242, 20)
    Me.edCitta.TabIndex = 10
    '
    'edCap
    '
    Me.edCap.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCap.Location = New System.Drawing.Point(100, 116)
    Me.edCap.Name = "edCap"
    Me.edCap.NTSDbField = ""
    Me.edCap.NTSForzaVisZoom = False
    Me.edCap.NTSOldValue = ""
    Me.edCap.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCap.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCap.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCap.Properties.MaxLength = 65536
    Me.edCap.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCap.Size = New System.Drawing.Size(73, 20)
    Me.edCap.TabIndex = 9
    '
    'edProv
    '
    Me.edProv.Cursor = System.Windows.Forms.Cursors.Default
    Me.edProv.Location = New System.Drawing.Point(256, 116)
    Me.edProv.Name = "edProv"
    Me.edProv.NTSDbField = ""
    Me.edProv.NTSForzaVisZoom = False
    Me.edProv.NTSOldValue = ""
    Me.edProv.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edProv.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edProv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edProv.Properties.MaxLength = 65536
    Me.edProv.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edProv.Size = New System.Drawing.Size(86, 20)
    Me.edProv.TabIndex = 8
    '
    'edDescr2
    '
    Me.edDescr2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDescr2.Location = New System.Drawing.Point(100, 38)
    Me.edDescr2.Name = "edDescr2"
    Me.edDescr2.NTSDbField = ""
    Me.edDescr2.NTSForzaVisZoom = False
    Me.edDescr2.NTSOldValue = ""
    Me.edDescr2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDescr2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDescr2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDescr2.Properties.MaxLength = 65536
    Me.edDescr2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDescr2.Size = New System.Drawing.Size(242, 20)
    Me.edDescr2.TabIndex = 7
    '
    'FRMMGANPR
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(450, 175)
    Me.Controls.Add(Me.NtsPanel1)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMMGANPR"
    Me.Text = "ANAGRAFICA CLIENTE PRIVATO"
    CType(Me.edDescr1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel1.ResumeLayout(False)
    Me.NtsPanel1.PerformLayout()
    CType(Me.edCodfis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edIndir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCitta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edProv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDescr2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      edCodfis.NTSSetParam(oMenu, oApp.Tr(Me, 128662037962656250, "Codice fiscale"), 16, True)
      edIndir.NTSSetParam(oMenu, oApp.Tr(Me, 128662037962968750, "Indirizzo"), 70, True)
      edCitta.NTSSetParam(oMenu, oApp.Tr(Me, 128662037963281250, "Città"), 50, True)
      edCap.NTSSetParam(oMenu, oApp.Tr(Me, 128662037963593750, "Cap"), 5, True)
      edProv.NTSSetParam(oMenu, oApp.Tr(Me, 128662037963906250, "Provincia"), 2, True)
      edDescr2.NTSSetParam(oMenu, oApp.Tr(Me, 128662037964218750, "Ulteriore descrizione"), 30, True)
      edDescr1.NTSSetParam(oMenu, oApp.Tr(Me, 128662037964531250, "Nome cliente"), 30, False)
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

  Public Overridable Sub FRMMGANPR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '--------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      edDescr1.Text = IIf(oCallParams.strPar1 = "", " ", oCallParams.strPar1).ToString
      edDescr2.Text = oCallParams.strPar2
      edIndir.Text = oCallParams.strPar3
      edCitta.Text = oCallParams.strPar4
      edCap.Text = oCallParams.strPar5
      edProv.Text = oCallParams.strParam
      edCodfis.Text = oCallParams.strOpen

    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    If edDescr1.Text.Trim = "" Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128662024680000000, "Nome cliente obbligatorio."))
      Return
    End If

    oCallParams.strPar1 = edDescr1.Text
    oCallParams.strPar2 = edDescr2.Text
    oCallParams.strPar3 = edIndir.Text
    oCallParams.strPar4 = edCitta.Text
    oCallParams.strPar5 = edCap.Text
    oCallParams.strParam = edProv.Text
    oCallParams.strOpen = edCodfis.Text.ToUpper
    oCallParams.bPar1 = True
    Me.Close()
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    oCallParams.bPar1 = False
    Me.Close()
  End Sub

  Public Overridable Sub cmdControlla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdControlla.Click
    Try
      edCodfis.Text = edCodfis.Text.ToUpper
      If Not oApp.CheckCfpi(1, edCodfis.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128662027296718750, "Codice fiscale non corretto (deve essere scritto in maiuscolo)."))
      Else
        oApp.MsgBoxErr(oApp.Tr(Me, 128662027366093750, "Codice fiscale corretto."))
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

End Class
