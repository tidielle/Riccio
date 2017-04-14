Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVESEB1

#Region "Dichiarazione variabili"
  Public oClePack As CLEVEPACK
  Public strTipo As String = ""
  Public lAnno As Integer = 0
  Public strSerie As String = ""
  Public lNumero As Integer = 0
  Public bOk As Boolean = False
  Public oCallParams As CLE__CLDP
  Private components As System.ComponentModel.IComponent

  Public WithEvents lbAnno As NTSInformatica.NTSLabel
  Public WithEvents edAnno As NTSInformatica.NTSTextBoxNum
  Public WithEvents cmdConferma As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents edNumero As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbNumero As NTSInformatica.NTSLabel
  Public WithEvents lbSerie As NTSInformatica.NTSLabel
  Public WithEvents edSerie As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTipo As NTSInformatica.NTSLabel
  Public WithEvents cbTipo As NTSInformatica.NTSComboBox
  Public WithEvents pnSebo As NTSInformatica.NTSPanel
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
    Me.lbAnno = New NTSInformatica.NTSLabel
    Me.edAnno = New NTSInformatica.NTSTextBoxNum
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.edNumero = New NTSInformatica.NTSTextBoxNum
    Me.lbNumero = New NTSInformatica.NTSLabel
    Me.lbSerie = New NTSInformatica.NTSLabel
    Me.edSerie = New NTSInformatica.NTSTextBoxStr
    Me.lbTipo = New NTSInformatica.NTSLabel
    Me.cbTipo = New NTSInformatica.NTSComboBox
    Me.pnSebo = New NTSInformatica.NTSPanel
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumero.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnSebo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSebo.SuspendLayout()
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
    'lbAnno
    '
    Me.lbAnno.AutoSize = True
    Me.lbAnno.BackColor = System.Drawing.Color.Transparent
    Me.lbAnno.Location = New System.Drawing.Point(12, 40)
    Me.lbAnno.Name = "lbAnno"
    Me.lbAnno.NTSDbField = ""
    Me.lbAnno.Size = New System.Drawing.Size(36, 13)
    Me.lbAnno.TabIndex = 4
    Me.lbAnno.Text = "Anno:"
    Me.lbAnno.Tooltip = ""
    Me.lbAnno.UseMnemonic = False
    '
    'edAnno
    '
    Me.edAnno.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnno.EditValue = "2008"
    Me.edAnno.Location = New System.Drawing.Point(7, 29)
    Me.edAnno.Name = "edAnno"
    Me.edAnno.NTSDbField = ""
    Me.edAnno.NTSFormat = "0"
    Me.edAnno.NTSForzaVisZoom = False
    Me.edAnno.NTSOldValue = "2008"
    Me.edAnno.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnno.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnno.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnno.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnno.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnno.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnno.Size = New System.Drawing.Size(52, 20)
    Me.edAnno.TabIndex = 2
    '
    'cmdConferma
    '
    Me.cmdConferma.ImageText = ""
    Me.cmdConferma.Location = New System.Drawing.Point(189, 8)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.Size = New System.Drawing.Size(96, 23)
    Me.cmdConferma.TabIndex = 5
    Me.cmdConferma.Text = "Conferma"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(189, 36)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(96, 23)
    Me.cmdAnnulla.TabIndex = 6
    Me.cmdAnnulla.Text = "Annulla"
    '
    'edNumero
    '
    Me.edNumero.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumero.EditValue = "0"
    Me.edNumero.Location = New System.Drawing.Point(7, 82)
    Me.edNumero.Name = "edNumero"
    Me.edNumero.NTSDbField = ""
    Me.edNumero.NTSFormat = "0"
    Me.edNumero.NTSForzaVisZoom = False
    Me.edNumero.NTSOldValue = ""
    Me.edNumero.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumero.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumero.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNumero.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNumero.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumero.Properties.MaxLength = 65536
    Me.edNumero.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumero.Size = New System.Drawing.Size(113, 20)
    Me.edNumero.TabIndex = 4
    '
    'lbNumero
    '
    Me.lbNumero.AutoSize = True
    Me.lbNumero.BackColor = System.Drawing.Color.Transparent
    Me.lbNumero.Location = New System.Drawing.Point(13, 93)
    Me.lbNumero.Name = "lbNumero"
    Me.lbNumero.NTSDbField = ""
    Me.lbNumero.Size = New System.Drawing.Size(48, 13)
    Me.lbNumero.TabIndex = 14
    Me.lbNumero.Text = "Numero:"
    Me.lbNumero.Tooltip = ""
    Me.lbNumero.UseMnemonic = False
    '
    'lbSerie
    '
    Me.lbSerie.AutoSize = True
    Me.lbSerie.BackColor = System.Drawing.Color.Transparent
    Me.lbSerie.Location = New System.Drawing.Point(13, 67)
    Me.lbSerie.Name = "lbSerie"
    Me.lbSerie.NTSDbField = ""
    Me.lbSerie.Size = New System.Drawing.Size(35, 13)
    Me.lbSerie.TabIndex = 16
    Me.lbSerie.Text = "Serie:"
    Me.lbSerie.Tooltip = ""
    Me.lbSerie.UseMnemonic = False
    '
    'edSerie
    '
    Me.edSerie.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edSerie.EditValue = ""
    Me.edSerie.Location = New System.Drawing.Point(7, 56)
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
    Me.edSerie.TabIndex = 3
    '
    'lbTipo
    '
    Me.lbTipo.AutoSize = True
    Me.lbTipo.BackColor = System.Drawing.Color.Transparent
    Me.lbTipo.Location = New System.Drawing.Point(12, 14)
    Me.lbTipo.Name = "lbTipo"
    Me.lbTipo.NTSDbField = ""
    Me.lbTipo.Size = New System.Drawing.Size(31, 13)
    Me.lbTipo.TabIndex = 22
    Me.lbTipo.Text = "Tipo:"
    Me.lbTipo.Tooltip = ""
    Me.lbTipo.UseMnemonic = False
    '
    'cbTipo
    '
    Me.cbTipo.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipo.DataSource = Nothing
    Me.cbTipo.DisplayMember = ""
    Me.cbTipo.Location = New System.Drawing.Point(7, 3)
    Me.cbTipo.Name = "cbTipo"
    Me.cbTipo.NTSDbField = ""
    Me.cbTipo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipo.Properties.DropDownRows = 30
    Me.cbTipo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipo.SelectedValue = ""
    Me.cbTipo.Size = New System.Drawing.Size(114, 20)
    Me.cbTipo.TabIndex = 1
    Me.cbTipo.ValueMember = ""
    '
    'pnSebo
    '
    Me.pnSebo.AllowDrop = True
    Me.pnSebo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSebo.Appearance.Options.UseBackColor = True
    Me.pnSebo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSebo.Controls.Add(Me.edNumero)
    Me.pnSebo.Controls.Add(Me.cbTipo)
    Me.pnSebo.Controls.Add(Me.edAnno)
    Me.pnSebo.Controls.Add(Me.edSerie)
    Me.pnSebo.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnSebo.Location = New System.Drawing.Point(59, 8)
    Me.pnSebo.Name = "pnSebo"
    Me.pnSebo.NTSActiveTrasparency = True
    Me.pnSebo.Size = New System.Drawing.Size(124, 108)
    Me.pnSebo.TabIndex = 27
    Me.pnSebo.Text = "NtsPanel1"
    '
    'FRMVESEB1
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(297, 115)
    Me.Controls.Add(Me.pnSebo)
    Me.Controls.Add(Me.lbTipo)
    Me.Controls.Add(Me.lbSerie)
    Me.Controls.Add(Me.lbNumero)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdConferma)
    Me.Controls.Add(Me.lbAnno)
    Me.Name = "FRMVESEB1"
    Me.Text = "APRI DA DOCUMENTO"
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNumero.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnSebo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSebo.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      edAnno.NTSSetParam(oMenu, oApp.Tr(Me, 129055327175451400, "Anno"), "0", 4, 1900, 2099)
      edSerie.NTSSetParam(oMenu, oApp.Tr(Me, 129055327202483515, "Serie"), CLN__STD.SerieMaxLen, False)
      edNumero.NTSSetParam(oMenu, oApp.Tr(Me, 129055327223577940, "Numero"), "0", 9, 0, 999999999)
      edNumero.NTSForzaVisZoom = True
      cbTipo.NTSSetParam(oApp.Tr(Me, 129055327244828620, "Tipo"))

      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRMVESEB1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      CaricaCombo()

      InitControls()

      If edSerie.Text = "" Then edSerie.Text = " "

      edAnno.Text = NTSCStr(Now.Year)
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

  Public Overridable Sub FRMVESEB1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Dim oParam As New CLE__PATB
    Try
      If e.KeyCode = Keys.F5 And edNumero.Focused Then
        oParam.strTipo = NTSCStr(cbTipo.SelectedValue)
        oParam.strAlfpar = edSerie.Text
        oParam.nAnno = NTSCInt(edAnno.Text)
        NTSZOOM.strIn = ""
        NTSZOOM.ZoomStrIn("ZOOMDOCUMENTI", DittaCorrente, oParam)

        If oParam.strOut <> "" Then
          edSerie.Text = oParam.strAlfpar
          edNumero.Text = oParam.lNumpar.ToString
          cmdConferma_Click(Me, e)
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub CaricaCombo()
    Dim dttTipo As New DataTable()
    Try
      dttTipo.Columns.Add("cod", GetType(String))
      dttTipo.Columns.Add("val", GetType(String))
      dttTipo.Rows.Add(New Object() {"A", "Fatture imm. emessa"})
      dttTipo.Rows.Add(New Object() {"B", "D.D.T. emesso"})
      dttTipo.Rows.Add(New Object() {"C", "Corrispettivo emesso"})
      'dttTipo.Rows.Add(New Object() {"D", "Fatture diff. emesse"})
      dttTipo.Rows.Add(New Object() {"E", "Note di addeb. emessa"})
      dttTipo.Rows.Add(New Object() {"F", "Ric. fiscale emessa"})
      dttTipo.Rows.Add(New Object() {"I", "Riemissione ric. fiscale"})
      dttTipo.Rows.Add(New Object() {"J", "Nota accr. ricevuta"})
      dttTipo.Rows.Add(New Object() {"L", "Fattura imm. ricevuta"})
      dttTipo.Rows.Add(New Object() {"M", "D.D.T. ricevuto"})
      dttTipo.Rows.Add(New Object() {"N", "Nota accr. emessa"})
      dttTipo.Rows.Add(New Object() {"S", "Fat. ric. fisc. emessa"})
      dttTipo.Rows.Add(New Object() {"T", "Carico di produzione"})
      dttTipo.Rows.Add(New Object() {"U", "Scarico di produzione"})
      dttTipo.Rows.Add(New Object() {"W", "Nota di prelievo"})
      dttTipo.Rows.Add(New Object() {"Z", "Bolla di mov. interna"})
      dttTipo.AcceptChanges()
      cbTipo.DataSource = dttTipo
      cbTipo.ValueMember = "cod"
      cbTipo.DisplayMember = "val"
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi"
  Public Overridable Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    Dim dttTmp As New DataTable
    Try
      '---------------------------------------------------------------------------------
      'Controlla presenza documento
      If Not oClePack.EsisteTestmag(cbTipo.SelectedValue, NTSCInt(edAnno.Text), edSerie.Text, NTSCInt(edNumero.Text), dttTmp) Then Return

      If dttTmp.Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128697492382224965, "Documento non presente in archivio."))
        Return
      End If

      strTipo = cbTipo.SelectedValue
      lAnno = NTSCInt(edAnno.Text)
      strSerie = edSerie.Text
      lNumero = NTSCInt(edNumero.Text)

      bOk = True
      Me.Close()
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      Me.Close()
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edSerie_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSerie.Validated
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edSerie.Text, False)
      If strTmp <> edSerie.Text Then edSerie.Text = strTmp

      edSerie.Text = UCase(edSerie.Text)
      If edSerie.Text = "" Then edSerie.Text = " "
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edNumero_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles edNumero.KeyDown
    Try
      If e.KeyCode = Keys.Enter Then cmdConferma_Click(Me, Nothing)
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cbTipo_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTipo.Validated
    Try
      If edSerie.Text = "" Then edSerie.Text = " "
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub
#End Region
End Class
