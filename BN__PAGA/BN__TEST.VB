Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__TEST
  Public oCallParams As CLE__CLDP
  Public oClePaga As CLE__PAGA
  Public nCodpaga As Integer = 0
  Public dttTipscad As DataTable

  Public Overridable Sub InitializeComponent()
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.NtsPanel1 = New NTSInformatica.NTSPanel
    Me.lbCaptionRate = New NTSInformatica.NTSLabel
    Me.liRate = New NTSInformatica.NTSListBox
    Me.edRate = New NTSInformatica.NTSTextBoxNum
    Me.edSpese = New NTSInformatica.NTSTextBoxNum
    Me.edIva = New NTSInformatica.NTSTextBoxNum
    Me.edTotdoc = New NTSInformatica.NTSTextBoxNum
    Me.edData = New NTSInformatica.NTSTextBoxData
    Me.lbRate = New NTSInformatica.NTSLabel
    Me.lbSpese = New NTSInformatica.NTSLabel
    Me.lbIVA = New NTSInformatica.NTSLabel
    Me.lbTotdoc = New NTSInformatica.NTSLabel
    Me.lbData = New NTSInformatica.NTSLabel
    Me.edRateCalc = New NTSInformatica.NTSMemoBox
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel1.SuspendLayout()
    CType(Me.liRate, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edRate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSpese.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edIva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTotdoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edData.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edRateCalc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(339, 37)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(105, 26)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "&Esci"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(339, 8)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.NTSContextMenu = Nothing
    Me.cmdOk.Size = New System.Drawing.Size(105, 26)
    Me.cmdOk.TabIndex = 2
    Me.cmdOk.Text = "&Calcola"
    '
    'NtsPanel1
    '
    Me.NtsPanel1.AllowDrop = True
    Me.NtsPanel1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsPanel1.Appearance.Options.UseBackColor = True
    Me.NtsPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel1.Controls.Add(Me.edRateCalc)
    Me.NtsPanel1.Controls.Add(Me.lbCaptionRate)
    Me.NtsPanel1.Controls.Add(Me.edRate)
    Me.NtsPanel1.Controls.Add(Me.edSpese)
    Me.NtsPanel1.Controls.Add(Me.edIva)
    Me.NtsPanel1.Controls.Add(Me.edTotdoc)
    Me.NtsPanel1.Controls.Add(Me.edData)
    Me.NtsPanel1.Controls.Add(Me.lbRate)
    Me.NtsPanel1.Controls.Add(Me.lbSpese)
    Me.NtsPanel1.Controls.Add(Me.lbIVA)
    Me.NtsPanel1.Controls.Add(Me.lbTotdoc)
    Me.NtsPanel1.Controls.Add(Me.lbData)
    Me.NtsPanel1.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsPanel1.Location = New System.Drawing.Point(2, 2)
    Me.NtsPanel1.Name = "NtsPanel1"
    Me.NtsPanel1.NTSActiveTrasparency = True
    Me.NtsPanel1.Size = New System.Drawing.Size(331, 259)
    Me.NtsPanel1.TabIndex = 9
    Me.NtsPanel1.Text = "NtsPanel1"
    '
    'lbCaptionRate
    '
    Me.lbCaptionRate.AutoSize = True
    Me.lbCaptionRate.BackColor = System.Drawing.Color.Transparent
    Me.lbCaptionRate.Location = New System.Drawing.Point(10, 131)
    Me.lbCaptionRate.Name = "lbCaptionRate"
    Me.lbCaptionRate.NTSDbField = ""
    Me.lbCaptionRate.Size = New System.Drawing.Size(189, 13)
    Me.lbCaptionRate.TabIndex = 11
    Me.lbCaptionRate.Text = "Rata/scadenza/importo/tipo scadenza"
    Me.lbCaptionRate.Tooltip = ""
    Me.lbCaptionRate.UseMnemonic = False
    '
    'liRate
    '
    Me.liRate.Cursor = System.Windows.Forms.Cursors.Default
    Me.liRate.ItemHeight = 14
    Me.liRate.Location = New System.Drawing.Point(339, 237)
    Me.liRate.Name = "liRate"
    Me.liRate.NTSDbField = ""
    Me.liRate.Size = New System.Drawing.Size(24, 17)
    Me.liRate.TabIndex = 10
    Me.liRate.Visible = False
    '
    'edRate
    '
    Me.edRate.Cursor = System.Windows.Forms.Cursors.Default
    Me.edRate.EditValue = "0"
    Me.edRate.Enabled = False
    Me.edRate.Location = New System.Drawing.Point(228, 112)
    Me.edRate.Name = "edRate"
    Me.edRate.NTSDbField = ""
    Me.edRate.NTSFormat = "0"
    Me.edRate.NTSForzaVisZoom = False
    Me.edRate.NTSOldValue = ""
    Me.edRate.Properties.Appearance.Options.UseTextOptions = True
    Me.edRate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edRate.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edRate.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edRate.Properties.AutoHeight = False
    Me.edRate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edRate.Properties.MaxLength = 65536
    Me.edRate.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edRate.Size = New System.Drawing.Size(100, 20)
    Me.edRate.TabIndex = 9
    '
    'edSpese
    '
    Me.edSpese.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSpese.EditValue = "0"
    Me.edSpese.Location = New System.Drawing.Point(228, 86)
    Me.edSpese.Name = "edSpese"
    Me.edSpese.NTSDbField = ""
    Me.edSpese.NTSFormat = "0"
    Me.edSpese.NTSForzaVisZoom = False
    Me.edSpese.NTSOldValue = ""
    Me.edSpese.Properties.Appearance.Options.UseTextOptions = True
    Me.edSpese.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSpese.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSpese.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSpese.Properties.AutoHeight = False
    Me.edSpese.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSpese.Properties.MaxLength = 65536
    Me.edSpese.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSpese.Size = New System.Drawing.Size(100, 20)
    Me.edSpese.TabIndex = 8
    '
    'edIva
    '
    Me.edIva.Cursor = System.Windows.Forms.Cursors.Default
    Me.edIva.EditValue = "0"
    Me.edIva.Location = New System.Drawing.Point(228, 60)
    Me.edIva.Name = "edIva"
    Me.edIva.NTSDbField = ""
    Me.edIva.NTSFormat = "0"
    Me.edIva.NTSForzaVisZoom = False
    Me.edIva.NTSOldValue = ""
    Me.edIva.Properties.Appearance.Options.UseTextOptions = True
    Me.edIva.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edIva.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edIva.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edIva.Properties.AutoHeight = False
    Me.edIva.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edIva.Properties.MaxLength = 65536
    Me.edIva.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edIva.Size = New System.Drawing.Size(100, 20)
    Me.edIva.TabIndex = 7
    '
    'edTotdoc
    '
    Me.edTotdoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTotdoc.EditValue = "100"
    Me.edTotdoc.Location = New System.Drawing.Point(228, 34)
    Me.edTotdoc.Name = "edTotdoc"
    Me.edTotdoc.NTSDbField = ""
    Me.edTotdoc.NTSFormat = "0"
    Me.edTotdoc.NTSForzaVisZoom = False
    Me.edTotdoc.NTSOldValue = "100"
    Me.edTotdoc.Properties.Appearance.Options.UseTextOptions = True
    Me.edTotdoc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTotdoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTotdoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTotdoc.Properties.AutoHeight = False
    Me.edTotdoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTotdoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTotdoc.Size = New System.Drawing.Size(100, 20)
    Me.edTotdoc.TabIndex = 6
    '
    'edData
    '
    Me.edData.Cursor = System.Windows.Forms.Cursors.Default
    Me.edData.Location = New System.Drawing.Point(228, 9)
    Me.edData.Name = "edData"
    Me.edData.NTSDbField = ""
    Me.edData.NTSForzaVisZoom = False
    Me.edData.NTSOldValue = ""
    Me.edData.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edData.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edData.Properties.AutoHeight = False
    Me.edData.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edData.Properties.MaxLength = 65536
    Me.edData.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edData.Size = New System.Drawing.Size(100, 20)
    Me.edData.TabIndex = 5
    '
    'lbRate
    '
    Me.lbRate.AutoSize = True
    Me.lbRate.BackColor = System.Drawing.Color.Transparent
    Me.lbRate.Location = New System.Drawing.Point(10, 112)
    Me.lbRate.Name = "lbRate"
    Me.lbRate.NTSDbField = ""
    Me.lbRate.Size = New System.Drawing.Size(86, 13)
    Me.lbRate.TabIndex = 4
    Me.lbRate.Text = "Num. totale rate"
    Me.lbRate.Tooltip = ""
    Me.lbRate.UseMnemonic = False
    '
    'lbSpese
    '
    Me.lbSpese.AutoSize = True
    Me.lbSpese.BackColor = System.Drawing.Color.Transparent
    Me.lbSpese.Location = New System.Drawing.Point(10, 86)
    Me.lbSpese.Name = "lbSpese"
    Me.lbSpese.NTSDbField = ""
    Me.lbSpese.Size = New System.Drawing.Size(68, 13)
    Me.lbSpese.TabIndex = 3
    Me.lbSpese.Text = "Totale spese"
    Me.lbSpese.Tooltip = ""
    Me.lbSpese.UseMnemonic = False
    '
    'lbIVA
    '
    Me.lbIVA.AutoSize = True
    Me.lbIVA.BackColor = System.Drawing.Color.Transparent
    Me.lbIVA.Location = New System.Drawing.Point(10, 60)
    Me.lbIVA.Name = "lbIVA"
    Me.lbIVA.NTSDbField = ""
    Me.lbIVA.Size = New System.Drawing.Size(57, 13)
    Me.lbIVA.TabIndex = 2
    Me.lbIVA.Text = "Totale IVA"
    Me.lbIVA.Tooltip = ""
    Me.lbIVA.UseMnemonic = False
    '
    'lbTotdoc
    '
    Me.lbTotdoc.AutoSize = True
    Me.lbTotdoc.BackColor = System.Drawing.Color.Transparent
    Me.lbTotdoc.Location = New System.Drawing.Point(10, 34)
    Me.lbTotdoc.Name = "lbTotdoc"
    Me.lbTotdoc.NTSDbField = ""
    Me.lbTotdoc.Size = New System.Drawing.Size(93, 13)
    Me.lbTotdoc.TabIndex = 1
    Me.lbTotdoc.Text = "Totale documento"
    Me.lbTotdoc.Tooltip = ""
    Me.lbTotdoc.UseMnemonic = False
    '
    'lbData
    '
    Me.lbData.AutoSize = True
    Me.lbData.BackColor = System.Drawing.Color.Transparent
    Me.lbData.Location = New System.Drawing.Point(10, 9)
    Me.lbData.Name = "lbData"
    Me.lbData.NTSDbField = ""
    Me.lbData.Size = New System.Drawing.Size(85, 13)
    Me.lbData.TabIndex = 0
    Me.lbData.Text = "Data riferimento"
    Me.lbData.Tooltip = ""
    Me.lbData.UseMnemonic = False
    '
    'edRateCalc
    '
    Me.edRateCalc.Location = New System.Drawing.Point(13, 147)
    Me.edRateCalc.Name = "edRateCalc"
    Me.edRateCalc.NTSDbField = ""
    Me.edRateCalc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edRateCalc.Size = New System.Drawing.Size(315, 105)
    Me.edRateCalc.TabIndex = 10
    '
    'FRM__TEST
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(456, 266)
    Me.Controls.Add(Me.liRate)
    Me.Controls.Add(Me.NtsPanel1)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__TEST"
    Me.Text = "CALCOLA SCADENZE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel1.ResumeLayout(False)
    Me.NtsPanel1.PerformLayout()
    CType(Me.liRate, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edRate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSpese.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edIva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTotdoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edData.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edRateCalc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
      liRate.NTSSetParam(oApp.Tr(Me, 128243779257317230, "Rate"))
      edRate.NTSSetParam(oMenu, oApp.Tr(Me, 128243779257473475, "Numero totale rate"), "0")
      edSpese.NTSSetParam(oMenu, oApp.Tr(Me, 128243779257629720, "Totale spese"), oApp.FormatImporti)
      edIva.NTSSetParam(oMenu, oApp.Tr(Me, 128243779257785965, "Totale IVA"), oApp.FormatImporti)
      edTotdoc.NTSSetParam(oMenu, oApp.Tr(Me, 128243779257942210, "Totale Documento"), oApp.FormatImporti)
      edData.NTSSetParam(oMenu, oApp.Tr(Me, 128243779258098455, "Data di riferimento"), False)
      edRateCalc.NTSSetParam(oMenu, oApp.Tr(Me, 130469727778535321, "Rate"), 0)

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

  Public Overridable Sub FRM__TEST_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      edData.Text = DateTime.Now.ToShortDateString


      '--------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
      GctlApplicaDefaultValue()

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Dim i As Integer = 0
    Dim dttScad As New DataTable
    Dim strTmp As String = ""
    Dim dtrT() As DataRow
    Try
      edRateCalc.Text = ""
      Me.Cursor = Cursors.WaitCursor
      oClePaga.CalcolaScadenze(nCodpaga, edData.Text, NTSCDec(edTotdoc.Text), NTSCDec(edIva.Text), NTSCDec(edSpese.Text), dttScad)
      edRate.Text = dttScad.Rows.Count.ToString
      For i = 0 To dttScad.Rows.Count - 1
        strTmp = dttScad.Rows(i)!rata.ToString.PadLeft(2, CChar("0")) & " | " & _
                        NTSCDate(dttScad.Rows(i)!data).ToShortDateString & " | " & _
                        NTSCDec(dttScad.Rows(i)!importo).ToString(oApp.FormatImporti) & " | "
        dtrT = dttTipscad.Select("cod = " & dttScad.Rows(i)!tiprata.ToString)
        If dtrT.Length > 0 Then
          strTmp += dtrT(0)!val.ToString
        Else
          strTmp += dttScad.Rows(i)!tiprata.ToString
        End If
        edRateCalc.Text += strTmp & vbCrLf
      Next
      Me.Cursor = Cursors.Default

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class
