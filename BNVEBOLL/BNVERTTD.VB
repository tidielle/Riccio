Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVERTTD
  Public oCallParams As CLE__CLDP
  Public bOk As Boolean
  Public oCleBoll As CLEVEBOLL
  Public strTiporkOrig As String = ""     'utilizzato solo in duplicadoc

  Public WithEvents edDiventi As NTSInformatica.NTSTextBoxNum
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents edDiminuiscaDi As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDiminuiscaDel As NTSInformatica.NTSTextBoxNum
  Public WithEvents opDiminuiscaDel As NTSInformatica.NTSRadioButton
  Public WithEvents opDiventi As NTSInformatica.NTSRadioButton
  Public WithEvents lbLabel1 As NTSInformatica.NTSLabel
  Public WithEvents lbPercento As NTSInformatica.NTSLabel
  Public WithEvents opDiminuiscaDi As NTSInformatica.NTSRadioButton
  Public WithEvents lbAttuale As NTSInformatica.NTSLabel
  Public WithEvents pnSpese As NTSInformatica.NTSPanel


  Public Overridable Sub InitializeComponent()
    Me.edDiventi = New NTSInformatica.NTSTextBoxNum
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.pnSpese = New NTSInformatica.NTSPanel
    Me.lbAttuale = New NTSInformatica.NTSLabel
    Me.lbPercento = New NTSInformatica.NTSLabel
    Me.opDiminuiscaDi = New NTSInformatica.NTSRadioButton
    Me.opDiminuiscaDel = New NTSInformatica.NTSRadioButton
    Me.opDiventi = New NTSInformatica.NTSRadioButton
    Me.lbLabel1 = New NTSInformatica.NTSLabel
    Me.edDiminuiscaDi = New NTSInformatica.NTSTextBoxNum
    Me.edDiminuiscaDel = New NTSInformatica.NTSTextBoxNum
    CType(Me.edDiventi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnSpese, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSpese.SuspendLayout()
    CType(Me.opDiminuiscaDi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opDiminuiscaDel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opDiventi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDiminuiscaDi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDiminuiscaDel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'edDiventi
    '
    Me.edDiventi.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edDiventi.EditValue = "0"
    Me.edDiventi.Location = New System.Drawing.Point(199, 51)
    Me.edDiventi.Name = "edDiventi"
    Me.edDiventi.NTSDbField = ""
    Me.edDiventi.NTSFormat = "0"
    Me.edDiventi.NTSForzaVisZoom = False
    Me.edDiventi.NTSOldValue = ""
    Me.edDiventi.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDiventi.Properties.Appearance.Options.UseBackColor = True
    Me.edDiventi.Properties.Appearance.Options.UseTextOptions = True
    Me.edDiventi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDiventi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDiventi.Properties.MaxLength = 9
    Me.edDiventi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDiventi.Size = New System.Drawing.Size(100, 20)
    Me.edDiventi.TabIndex = 8
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Location = New System.Drawing.Point(333, 36)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(105, 22)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.Location = New System.Drawing.Point(333, 7)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(105, 22)
    Me.cmdOk.TabIndex = 2
    Me.cmdOk.Text = "&Conferma"
    '
    'pnSpese
    '
    Me.pnSpese.AllowDrop = True
    Me.pnSpese.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSpese.Appearance.Options.UseBackColor = True
    Me.pnSpese.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSpese.Controls.Add(Me.lbAttuale)
    Me.pnSpese.Controls.Add(Me.lbPercento)
    Me.pnSpese.Controls.Add(Me.opDiminuiscaDi)
    Me.pnSpese.Controls.Add(Me.opDiminuiscaDel)
    Me.pnSpese.Controls.Add(Me.opDiventi)
    Me.pnSpese.Controls.Add(Me.lbLabel1)
    Me.pnSpese.Controls.Add(Me.edDiminuiscaDi)
    Me.pnSpese.Controls.Add(Me.edDiminuiscaDel)
    Me.pnSpese.Controls.Add(Me.edDiventi)
    Me.pnSpese.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnSpese.Location = New System.Drawing.Point(2, 8)
    Me.pnSpese.Name = "pnSpese"
    Me.pnSpese.Size = New System.Drawing.Size(325, 133)
    Me.pnSpese.TabIndex = 9
    Me.pnSpese.Text = "NtsPanel1"
    '
    'lbAttuale
    '
    Me.lbAttuale.AutoSize = True
    Me.lbAttuale.BackColor = System.Drawing.Color.Transparent
    Me.lbAttuale.Location = New System.Drawing.Point(3, 1)
    Me.lbAttuale.Name = "lbAttuale"
    Me.lbAttuale.NTSDbField = ""
    Me.lbAttuale.Size = New System.Drawing.Size(19, 13)
    Me.lbAttuale.TabIndex = 21
    Me.lbAttuale.Text = "XX"
    '
    'lbPercento
    '
    Me.lbPercento.AutoSize = True
    Me.lbPercento.BackColor = System.Drawing.Color.Transparent
    Me.lbPercento.Location = New System.Drawing.Point(281, 80)
    Me.lbPercento.Name = "lbPercento"
    Me.lbPercento.NTSDbField = ""
    Me.lbPercento.Size = New System.Drawing.Size(18, 13)
    Me.lbPercento.TabIndex = 20
    Me.lbPercento.Text = "%"
    '
    'opDiminuiscaDi
    '
    Me.opDiminuiscaDi.Cursor = System.Windows.Forms.Cursors.Default
    Me.opDiminuiscaDi.Location = New System.Drawing.Point(46, 104)
    Me.opDiminuiscaDi.Name = "opDiminuiscaDi"
    Me.opDiminuiscaDi.NTSCheckValue = "S"
    Me.opDiminuiscaDi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opDiminuiscaDi.Properties.Appearance.Options.UseBackColor = True
    Me.opDiminuiscaDi.Properties.Caption = "diminuisca circa di"
    Me.opDiminuiscaDi.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opDiminuiscaDi.Size = New System.Drawing.Size(115, 19)
    Me.opDiminuiscaDi.TabIndex = 19
    '
    'opDiminuiscaDel
    '
    Me.opDiminuiscaDel.Cursor = System.Windows.Forms.Cursors.Default
    Me.opDiminuiscaDel.Location = New System.Drawing.Point(46, 78)
    Me.opDiminuiscaDel.Name = "opDiminuiscaDel"
    Me.opDiminuiscaDel.NTSCheckValue = "S"
    Me.opDiminuiscaDel.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opDiminuiscaDel.Properties.Appearance.Options.UseBackColor = True
    Me.opDiminuiscaDel.Properties.Caption = "diminuisca circa del"
    Me.opDiminuiscaDel.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opDiminuiscaDel.Size = New System.Drawing.Size(115, 19)
    Me.opDiminuiscaDel.TabIndex = 18
    '
    'opDiventi
    '
    Me.opDiventi.Cursor = System.Windows.Forms.Cursors.Default
    Me.opDiventi.Location = New System.Drawing.Point(46, 51)
    Me.opDiventi.Name = "opDiventi"
    Me.opDiventi.NTSCheckValue = "S"
    Me.opDiventi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opDiventi.Properties.Appearance.Options.UseBackColor = True
    Me.opDiventi.Properties.Caption = "diventi circa"
    Me.opDiventi.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opDiventi.Size = New System.Drawing.Size(86, 19)
    Me.opDiventi.TabIndex = 17
    '
    'lbLabel1
    '
    Me.lbLabel1.AutoSize = True
    Me.lbLabel1.BackColor = System.Drawing.Color.Transparent
    Me.lbLabel1.Location = New System.Drawing.Point(3, 28)
    Me.lbLabel1.Name = "lbLabel1"
    Me.lbLabel1.NTSDbField = ""
    Me.lbLabel1.Size = New System.Drawing.Size(325, 13)
    Me.lbLabel1.TabIndex = 15
    Me.lbLabel1.Text = "Rettifica i prezzi di riga documento in modo che il totale documento"
    '
    'edDiminuiscaDi
    '
    Me.edDiminuiscaDi.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDiminuiscaDi.EditValue = "0"
    Me.edDiminuiscaDi.Location = New System.Drawing.Point(199, 103)
    Me.edDiminuiscaDi.Name = "edDiminuiscaDi"
    Me.edDiminuiscaDi.NTSDbField = ""
    Me.edDiminuiscaDi.NTSFormat = "0"
    Me.edDiminuiscaDi.NTSForzaVisZoom = False
    Me.edDiminuiscaDi.NTSOldValue = ""
    Me.edDiminuiscaDi.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDiminuiscaDi.Properties.Appearance.Options.UseBackColor = True
    Me.edDiminuiscaDi.Properties.Appearance.Options.UseTextOptions = True
    Me.edDiminuiscaDi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDiminuiscaDi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDiminuiscaDi.Properties.MaxLength = 9
    Me.edDiminuiscaDi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDiminuiscaDi.Size = New System.Drawing.Size(100, 20)
    Me.edDiminuiscaDi.TabIndex = 10
    '
    'edDiminuiscaDel
    '
    Me.edDiminuiscaDel.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDiminuiscaDel.EditValue = "0"
    Me.edDiminuiscaDel.Location = New System.Drawing.Point(199, 77)
    Me.edDiminuiscaDel.Name = "edDiminuiscaDel"
    Me.edDiminuiscaDel.NTSDbField = ""
    Me.edDiminuiscaDel.NTSFormat = "0"
    Me.edDiminuiscaDel.NTSForzaVisZoom = False
    Me.edDiminuiscaDel.NTSOldValue = ""
    Me.edDiminuiscaDel.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDiminuiscaDel.Properties.Appearance.Options.UseBackColor = True
    Me.edDiminuiscaDel.Properties.Appearance.Options.UseTextOptions = True
    Me.edDiminuiscaDel.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDiminuiscaDel.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDiminuiscaDel.Properties.MaxLength = 9
    Me.edDiminuiscaDel.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDiminuiscaDel.Size = New System.Drawing.Size(76, 20)
    Me.edDiminuiscaDel.TabIndex = 9
    '
    'FRMVERTTD
    '
    Me.ClientSize = New System.Drawing.Size(451, 142)
    Me.Controls.Add(Me.pnSpese)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMVERTTD"
    Me.Text = "Rettifica totale documento"
    CType(Me.edDiventi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnSpese, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSpese.ResumeLayout(False)
    Me.pnSpese.PerformLayout()
    CType(Me.opDiminuiscaDi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opDiminuiscaDel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opDiventi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDiminuiscaDi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDiminuiscaDel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
      opDiminuiscaDi.NTSSetParam(oMenu, oApp.Tr(Me, 128687246516562500, "diminuisca circa di"), "0")
      opDiminuiscaDel.NTSSetParam(oMenu, oApp.Tr(Me, 128687246516718750, "diminuisca circa del"), "1")
      opDiventi.NTSSetParam(oMenu, oApp.Tr(Me, 128687246516875000, "diventi circa"), "2")
      edDiminuiscaDi.NTSSetParam(oMenu, oApp.Tr(Me, 128687246517187500, "Diminuisca circa di"), oApp.FormatImporti, 13, -9999999999, 9999999999)
      edDiminuiscaDel.NTSSetParam(oMenu, oApp.Tr(Me, 128687246517343750, "Diminuisca circa del"), oApp.FormatSconti, 6, -100, 100)
      edDiventi.NTSSetParam(oMenu, oApp.Tr(Me, 128687246517500000, "Divendi circa di"), oApp.FormatImporti, 13, 0, 9999999999)

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

  Public Overridable Sub FRMVERTTD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim nUltimaScelta As Integer = 0
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      nUltimaScelta = NTSCInt(Val(oMenu.GetSettingBusDitt(DittaCorrente, "Bsveboll", "Recent", ".", "TipoRettificaDocumento", "0", ".", "0")))
      Select Case nUltimaScelta
        Case 0
          opDiventi.Checked = True
        Case 1
          opDiminuiscaDel.Checked = True
          edDiminuiscaDel.Text = NTSCDec(IntSetNum(oMenu.GetSettingBusDitt(DittaCorrente, "Bsveboll", "Recent", ".", "PercRettificaDocumento", "0", ".", "0"))).ToString
        Case 2
          opDiminuiscaDi.Checked = True
      End Select

      '--------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlTipoDoc = oCleBoll.dttET.Rows(0)!et_tipork.ToString
      GctlSetRoules()
      GctlApplicaDefaultValue()

      If opDiventi.Checked = False And opDiminuiscaDel.Checked = False And opDiminuiscaDi.Checked = False Then opDiventi.Checked = True

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Dim nUltimaScelta As Integer = 0
    Try
      If opDiventi.Checked Then nUltimaScelta = 0
      If opDiminuiscaDel.Checked Then nUltimaScelta = 1
      If opDiminuiscaDi.Checked Then nUltimaScelta = 2

      oMenu.SaveSettingBus("Bsveboll", "Recent", ".", "TipoRettificaDocumento", nUltimaScelta.ToString, " ", "NS.", "...", "...")
      If nUltimaScelta = 1 Then
        oMenu.SaveSettingBus("Bsveboll", "Recent", ".", "PercRettificaDocumento", edDiminuiscaDel.Text, " ", "NS.", "...", "...")
      End If

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


  Public Overridable Function SetStato() As Boolean
    Try
      If opDiventi.Checked Then
        GctlSetVisEnab(edDiventi, False)
      Else
        edDiventi.Enabled = False
        edDiventi.Text = "0"
      End If

      If opDiminuiscaDel.Checked Then
        GctlSetVisEnab(edDiminuiscaDel, False)
      Else
        edDiminuiscaDel.Enabled = False
        edDiminuiscaDel.Text = "0"
      End If

      If opDiminuiscaDi.Checked Then
        GctlSetVisEnab(edDiminuiscaDi, False)
      Else
        edDiminuiscaDi.Enabled = False
        edDiminuiscaDi.Text = "0"
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub opDiventi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opDiventi.CheckedChanged
    Try
      SetStato()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opDiminuiscaDel_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opDiminuiscaDel.CheckedChanged
    Try
      SetStato()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opDiminuiscaDi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opDiminuiscaDi.CheckedChanged
    Try
      SetStato()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
End Class
