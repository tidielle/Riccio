Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORGNDO
  Public oCleGnof As CLEORGNOF
  Public oCallParams As CLE__CLDP
  Public WithEvents pnGndo As NTSInformatica.NTSPanel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents edCodtpbf As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDatdoc As NTSInformatica.NTSTextBoxData
  Public WithEvents edNumord As NTSInformatica.NTSTextBoxNum
  Public WithEvents edSerie As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAnno As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCodtpbf As NTSInformatica.NTSLabel
  Public WithEvents lbDatdoc As NTSInformatica.NTSLabel
  Public WithEvents lbSerie As NTSInformatica.NTSLabel
  Public WithEvents lbAnno As NTSInformatica.NTSLabel
  Public WithEvents lbXxCodtpbf As NTSInformatica.NTSLabel
  Public WithEvents lbNumord As NTSInformatica.NTSLabel
  Private components As System.ComponentModel.IContainer

#Region "Inizializzazione"
  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    '----------------------------------------------------------------------------------------------------------------
    oMenu = Menu
    oApp = oMenu.App
    oCallParams = Param
    '----------------------------------------------------------------------------------------------------------------
    If Ditta <> "" Then
      DittaCorrente = Ditta
    Else
      DittaCorrente = oApp.Ditta
    End If
    '----------------------------------------------------------------------------------------------------------------
    Me.GctlTipoDoc = ""
    InitializeComponent()
    Me.MinimumSize = Me.Size
    '----------------------------------------------------------------------------------------------------------------
    Return True
    '----------------------------------------------------------------------------------------------------------------
  End Function

  Private Sub InitializeComponent()
    Me.pnGndo = New NTSInformatica.NTSPanel
    Me.lbNumord = New NTSInformatica.NTSLabel
    Me.lbCodtpbf = New NTSInformatica.NTSLabel
    Me.lbDatdoc = New NTSInformatica.NTSLabel
    Me.lbSerie = New NTSInformatica.NTSLabel
    Me.lbAnno = New NTSInformatica.NTSLabel
    Me.lbXxCodtpbf = New NTSInformatica.NTSLabel
    Me.edCodtpbf = New NTSInformatica.NTSTextBoxNum
    Me.edDatdoc = New NTSInformatica.NTSTextBoxData
    Me.edNumord = New NTSInformatica.NTSTextBoxNum
    Me.edSerie = New NTSInformatica.NTSTextBoxStr
    Me.edAnno = New NTSInformatica.NTSTextBoxNum
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    CType(Me.pnGndo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGndo.SuspendLayout()
    CType(Me.edCodtpbf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatdoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumord.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.frmAuto.Appearance.BackColor = System.Drawing.SystemColors.GradientActiveCaption
    Me.frmAuto.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmAuto.Appearance.Options.UseBackColor = True
    Me.frmAuto.Appearance.Options.UseImage = True
    '
    'pnGndo
    '
    Me.pnGndo.AllowDrop = True
    Me.pnGndo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGndo.Appearance.Options.UseBackColor = True
    Me.pnGndo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGndo.Controls.Add(Me.lbNumord)
    Me.pnGndo.Controls.Add(Me.lbCodtpbf)
    Me.pnGndo.Controls.Add(Me.lbDatdoc)
    Me.pnGndo.Controls.Add(Me.lbSerie)
    Me.pnGndo.Controls.Add(Me.lbAnno)
    Me.pnGndo.Controls.Add(Me.lbXxCodtpbf)
    Me.pnGndo.Controls.Add(Me.edCodtpbf)
    Me.pnGndo.Controls.Add(Me.edDatdoc)
    Me.pnGndo.Controls.Add(Me.edNumord)
    Me.pnGndo.Controls.Add(Me.edSerie)
    Me.pnGndo.Controls.Add(Me.edAnno)
    Me.pnGndo.Controls.Add(Me.cmdAnnulla)
    Me.pnGndo.Controls.Add(Me.cmdOk)
    Me.pnGndo.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGndo.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGndo.Location = New System.Drawing.Point(0, 0)
    Me.pnGndo.Name = "pnGndo"
    Me.pnGndo.NTSActiveTrasparency = True
    Me.pnGndo.Size = New System.Drawing.Size(508, 132)
    Me.pnGndo.TabIndex = 0
    Me.pnGndo.Text = "NtsPanel1"
    '
    'lbNumord
    '
    Me.lbNumord.AutoSize = True
    Me.lbNumord.BackColor = System.Drawing.Color.Transparent
    Me.lbNumord.Location = New System.Drawing.Point(149, 41)
    Me.lbNumord.Name = "lbNumord"
    Me.lbNumord.NTSDbField = ""
    Me.lbNumord.Size = New System.Drawing.Size(11, 13)
    Me.lbNumord.TabIndex = 24
    Me.lbNumord.Text = "/"
    Me.lbNumord.Tooltip = ""
    Me.lbNumord.UseMnemonic = False
    '
    'lbCodtpbf
    '
    Me.lbCodtpbf.AutoSize = True
    Me.lbCodtpbf.BackColor = System.Drawing.Color.Transparent
    Me.lbCodtpbf.Location = New System.Drawing.Point(12, 93)
    Me.lbCodtpbf.Name = "lbCodtpbf"
    Me.lbCodtpbf.NTSDbField = ""
    Me.lbCodtpbf.Size = New System.Drawing.Size(76, 13)
    Me.lbCodtpbf.TabIndex = 23
    Me.lbCodtpbf.Text = "Tipo Bolla/Fatt"
    Me.lbCodtpbf.Tooltip = ""
    Me.lbCodtpbf.UseMnemonic = False
    '
    'lbDatdoc
    '
    Me.lbDatdoc.AutoSize = True
    Me.lbDatdoc.BackColor = System.Drawing.Color.Transparent
    Me.lbDatdoc.Location = New System.Drawing.Point(12, 67)
    Me.lbDatdoc.Name = "lbDatdoc"
    Me.lbDatdoc.NTSDbField = ""
    Me.lbDatdoc.Size = New System.Drawing.Size(30, 13)
    Me.lbDatdoc.TabIndex = 22
    Me.lbDatdoc.Text = "Data"
    Me.lbDatdoc.Tooltip = ""
    Me.lbDatdoc.UseMnemonic = False
    '
    'lbSerie
    '
    Me.lbSerie.AutoSize = True
    Me.lbSerie.BackColor = System.Drawing.Color.Transparent
    Me.lbSerie.Location = New System.Drawing.Point(12, 41)
    Me.lbSerie.Name = "lbSerie"
    Me.lbSerie.NTSDbField = ""
    Me.lbSerie.Size = New System.Drawing.Size(78, 13)
    Me.lbSerie.TabIndex = 21
    Me.lbSerie.Text = "Serie / Numero"
    Me.lbSerie.Tooltip = ""
    Me.lbSerie.UseMnemonic = False
    '
    'lbAnno
    '
    Me.lbAnno.AutoSize = True
    Me.lbAnno.BackColor = System.Drawing.Color.Transparent
    Me.lbAnno.Location = New System.Drawing.Point(12, 15)
    Me.lbAnno.Name = "lbAnno"
    Me.lbAnno.NTSDbField = ""
    Me.lbAnno.Size = New System.Drawing.Size(32, 13)
    Me.lbAnno.TabIndex = 20
    Me.lbAnno.Text = "Anno"
    Me.lbAnno.Tooltip = ""
    Me.lbAnno.UseMnemonic = False
    '
    'lbXxCodtpbf
    '
    Me.lbXxCodtpbf.BackColor = System.Drawing.Color.Transparent
    Me.lbXxCodtpbf.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXxCodtpbf.Location = New System.Drawing.Point(170, 90)
    Me.lbXxCodtpbf.Name = "lbXxCodtpbf"
    Me.lbXxCodtpbf.NTSDbField = ""
    Me.lbXxCodtpbf.Size = New System.Drawing.Size(242, 20)
    Me.lbXxCodtpbf.TabIndex = 19
    Me.lbXxCodtpbf.Tooltip = ""
    Me.lbXxCodtpbf.UseMnemonic = False
    '
    'edCodtpbf
    '
    Me.edCodtpbf.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edCodtpbf.Location = New System.Drawing.Point(97, 90)
    Me.edCodtpbf.Name = "edCodtpbf"
    Me.edCodtpbf.NTSDbField = ""
    Me.edCodtpbf.NTSFormat = "0"
    Me.edCodtpbf.NTSForzaVisZoom = False
    Me.edCodtpbf.NTSOldValue = ""
    Me.edCodtpbf.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodtpbf.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodtpbf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodtpbf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodtpbf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodtpbf.Properties.MaxLength = 65536
    Me.edCodtpbf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodtpbf.Size = New System.Drawing.Size(67, 20)
    Me.edCodtpbf.TabIndex = 10
    '
    'edDatdoc
    '
    Me.edDatdoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatdoc.Location = New System.Drawing.Point(97, 64)
    Me.edDatdoc.Name = "edDatdoc"
    Me.edDatdoc.NTSDbField = ""
    Me.edDatdoc.NTSForzaVisZoom = False
    Me.edDatdoc.NTSOldValue = ""
    Me.edDatdoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatdoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatdoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatdoc.Properties.MaxLength = 65536
    Me.edDatdoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatdoc.Size = New System.Drawing.Size(100, 20)
    Me.edDatdoc.TabIndex = 9
    '
    'edNumord
    '
    Me.edNumord.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumord.Location = New System.Drawing.Point(168, 38)
    Me.edNumord.Name = "edNumord"
    Me.edNumord.NTSDbField = ""
    Me.edNumord.NTSFormat = "0"
    Me.edNumord.NTSForzaVisZoom = False
    Me.edNumord.NTSOldValue = ""
    Me.edNumord.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumord.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumord.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNumord.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNumord.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumord.Properties.MaxLength = 65536
    Me.edNumord.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumord.Size = New System.Drawing.Size(88, 20)
    Me.edNumord.TabIndex = 8
    '
    'edSerie
    '
    Me.edSerie.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edSerie.Location = New System.Drawing.Point(97, 38)
    Me.edSerie.Name = "edSerie"
    Me.edSerie.NTSDbField = ""
    Me.edSerie.NTSForzaVisZoom = False
    Me.edSerie.NTSOldValue = ""
    Me.edSerie.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSerie.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSerie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSerie.Properties.MaxLength = 65536
    Me.edSerie.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSerie.Size = New System.Drawing.Size(45, 20)
    Me.edSerie.TabIndex = 7
    '
    'edAnno
    '
    Me.edAnno.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnno.Location = New System.Drawing.Point(97, 12)
    Me.edAnno.Name = "edAnno"
    Me.edAnno.NTSDbField = ""
    Me.edAnno.NTSFormat = "0"
    Me.edAnno.NTSForzaVisZoom = False
    Me.edAnno.NTSOldValue = ""
    Me.edAnno.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnno.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnno.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnno.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnno.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnno.Properties.MaxLength = 65536
    Me.edAnno.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnno.Size = New System.Drawing.Size(67, 20)
    Me.edAnno.TabIndex = 6
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(421, 41)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(75, 23)
    Me.cmdAnnulla.TabIndex = 5
    Me.cmdAnnulla.Text = "Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(421, 12)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(75, 23)
    Me.cmdOk.TabIndex = 4
    Me.cmdOk.Text = "OK"
    '
    'FRMORGNDO
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(508, 132)
    Me.Controls.Add(Me.pnGndo)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMORGNDO"
    Me.Text = "GENERAZIONE DOCUMENTI: INFORMAZIONI AGGIUNTIVE"
    CType(Me.pnGndo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGndo.ResumeLayout(False)
    Me.pnGndo.PerformLayout()
    CType(Me.edCodtpbf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatdoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNumord.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, False)
    '----------------------------------------------------------------------------------------------------------------
    Try
      edCodtpbf.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129532256895954911, "Cod. tipo bolla/fattura"), tabtpbf)
      edDatdoc.NTSSetParam(oMenu, oApp.Tr(Me, 129532256896111762, "Data doc"), False)
      edNumord.NTSSetParam(oMenu, oApp.Tr(Me, 129532256896268613, "Numero ordine"), "0")
      edSerie.NTSSetParam(oMenu, oApp.Tr(Me, 129532256896425464, "Serie"), CLN__STD.SerieMaxLen)
      edAnno.NTSSetParam(oMenu, oApp.Tr(Me, 129532256896582315, "Anno"), "0")

      '--------------------------------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, True)
    '----------------------------------------------------------------------------------------------------------------
  End Sub

  Public Overridable Sub InitEntity(ByRef cleGnof As CLEORGNOF)
    '----------------------------------------------------------------------------------------------------------------
    oCleGnof = cleGnof
    '----------------------------------------------------------------------------------------------------------------
    AddHandler oCleGnof.RemoteEvent, AddressOf GestisciEventiEntity
    '----------------------------------------------------------------------------------------------------------------
  End Sub
#End Region

  Public Overridable Sub FRMORGNDO_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      InitControls()

      oCleGnof.bGndoAnnulla = True

      edAnno.Text = NTSCStr(Year(Now))
      edDatdoc.Text = NTSCDate(Now).ToShortDateString
      edSerie.Text = " "
      edNumord.TextInt = oCleGnof.LegNuma("O", edSerie.Text, edAnno.TextInt)
      edCodtpbf.Text = "0"
      NTSGetDataAutocompletamentoTipork = "O" 'per filtrare solo i tipibf utilizzabili per questo tipork

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      If NTSCInt(edNumord.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129532923992075546, "Numero ordine obbligatorio"))
        Return
      End If
      If NTSCInt(edCodtpbf.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129532979092622132, "Tipo bolla/fattura obbligatorio"))
        Return
      End If
      oCleGnof.nGndoAnno = NTSCInt(edAnno.Text)
      oCleGnof.strGndoSerie = edSerie.Text
      oCleGnof.lGndoNumord = NTSCInt(edNumord.Text)
      oCleGnof.strGndoDatord = edDatdoc.Text
      oCleGnof.nGndoCodtpbf = NTSCInt(edCodtpbf.Text)
      oCleGnof.bGndoAnnulla = False

      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      oCleGnof.bGndoAnnulla = True
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMORGNDO_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
    Try
      If e.KeyCode = Keys.F5 Then
        Zoom()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub Zoom()
    Try
      'per eventuali altri controlli caricati al volo
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      If edCodtpbf.Focused Then
        SetFastZoom(edCodtpbf.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edCodtpbf.Text
        NTSZOOM.ZoomStrIn("ZOOMTABTPBF", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edCodtpbf.Text Then edCodtpbf.NTSTextDB = NTSZOOM.strIn
      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        NTSCallStandardZoom()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edCodtpbf_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCodtpbf.Validated
    Dim strTmp As String = ""
    Try
      If oMenu.ValCodiceDb(edCodtpbf.Text, DittaCorrente, "TABTPBF", "N", strTmp) And NTSCInt(edCodtpbf.Text) <> 0 Then
        lbXxCodtpbf.Text = strTmp
      End If

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edSerie_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSerie.Validated
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edSerie.Text, False)
      If strTmp <> edSerie.Text Then edSerie.Text = strTmp

      edNumord.TextInt = oCleGnof.LegNuma("O", edSerie.Text, edAnno.TextInt)
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edAnno_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAnno.Validated
    Try
      edNumord.TextInt = oCleGnof.LegNuma("O", edSerie.Text, edAnno.TextInt)
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub
End Class