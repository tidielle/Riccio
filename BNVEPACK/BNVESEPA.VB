Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVESEPA

#Region "Dichiarazione variabili"
  Public oClePack As CLEVEPACK
  Public strTipoForm As String = "N"
  Public bOk As Boolean = False
  Public lAnno As Integer
  Public strSerie As String
  Public lNumero As Integer
  Public oCallParams As CLE__CLDP
  Private components As System.ComponentModel.IComponent

  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents lbAnno As NTSInformatica.NTSLabel
  Public WithEvents edAnno As NTSInformatica.NTSTextBoxNum
  Public WithEvents cmdConferma As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents edNumero As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbNumero As NTSInformatica.NTSLabel
  Public WithEvents lbSerie As NTSInformatica.NTSLabel
  Public WithEvents edSerie As NTSInformatica.NTSTextBoxStr
  Public WithEvents pnSepa As NTSInformatica.NTSPanel
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
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.lbAnno = New NTSInformatica.NTSLabel
    Me.edAnno = New NTSInformatica.NTSTextBoxNum
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.edNumero = New NTSInformatica.NTSTextBoxNum
    Me.lbNumero = New NTSInformatica.NTSLabel
    Me.lbSerie = New NTSInformatica.NTSLabel
    Me.edSerie = New NTSInformatica.NTSTextBoxStr
    Me.pnSepa = New NTSInformatica.NTSPanel
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumero.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnSepa, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSepa.SuspendLayout()
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
    Me.lbAnno.Location = New System.Drawing.Point(11, 19)
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
    Me.edAnno.Location = New System.Drawing.Point(3, 3)
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
    Me.edAnno.TabIndex = 1
    '
    'cmdConferma
    '
    Me.cmdConferma.ImageText = ""
    Me.cmdConferma.Location = New System.Drawing.Point(189, 12)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.Size = New System.Drawing.Size(96, 23)
    Me.cmdConferma.TabIndex = 4
    Me.cmdConferma.Text = "Conferma"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(189, 40)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(96, 23)
    Me.cmdAnnulla.TabIndex = 5
    Me.cmdAnnulla.Text = "Annulla"
    '
    'edNumero
    '
    Me.edNumero.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumero.EditValue = "0"
    Me.edNumero.Location = New System.Drawing.Point(3, 55)
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
    Me.edNumero.TabIndex = 3
    '
    'lbNumero
    '
    Me.lbNumero.AutoSize = True
    Me.lbNumero.BackColor = System.Drawing.Color.Transparent
    Me.lbNumero.Location = New System.Drawing.Point(12, 72)
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
    Me.lbSerie.Location = New System.Drawing.Point(12, 46)
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
    Me.edSerie.Location = New System.Drawing.Point(3, 29)
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
    Me.edSerie.TabIndex = 2
    '
    'pnSepa
    '
    Me.pnSepa.AllowDrop = True
    Me.pnSepa.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSepa.Appearance.Options.UseBackColor = True
    Me.pnSepa.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSepa.Controls.Add(Me.edAnno)
    Me.pnSepa.Controls.Add(Me.edSerie)
    Me.pnSepa.Controls.Add(Me.edNumero)
    Me.pnSepa.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnSepa.Location = New System.Drawing.Point(66, 12)
    Me.pnSepa.Name = "pnSepa"
    Me.pnSepa.NTSActiveTrasparency = True
    Me.pnSepa.Size = New System.Drawing.Size(117, 80)
    Me.pnSepa.TabIndex = 21
    Me.pnSepa.Text = "NtsPanel1"
    '
    'FRMVESEPA
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(297, 97)
    Me.Controls.Add(Me.pnSepa)
    Me.Controls.Add(Me.lbSerie)
    Me.Controls.Add(Me.lbNumero)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdConferma)
    Me.Controls.Add(Me.lbAnno)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMVESEPA"
    Me.Text = ""
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNumero.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnSepa, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSepa.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      edAnno.NTSSetParam(oMenu, oApp.Tr(Me, 128696566203426336, "Anno"), "0", 4, 1900, 2099)
      edSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128696565255366581, "Serie"), CLN__STD.SerieMaxLen)
      edNumero.NTSSetParam(oMenu, oApp.Tr(Me, 128696565255679111, "Numero"), "0", 9, 0, 999999999)
      edNumero.NTSForzaVisZoom = True


      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRMVESEPA_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Dim strAnnoCompe As String
    Try
      InitControls()

      strAnnoCompe = oClePack.AnnoCompetenza()

      If strTipoForm = "A" Or strTipoForm = "D" Then
        Me.Text = oApp.Tr(Me, 129082847413335922, "Apri documento")

        edAnno.Text = oMenu.GetSettingBus("Bsvepack", ".", ".", "AnnoDocAperto", strAnnoCompe, " ", strAnnoCompe)
        edSerie.Text = oMenu.GetSettingBus("Bsvepack", ".", ".", "SerieDocAperto", " ", " ", " ")

        If edSerie.Text = "" Then edSerie.Text = " "
      ElseIf strTipoForm = "N" Then
        Me.Text = oApp.Tr(Me, 129082847600846722, "Nuovo documento")

        edAnno.Text = oMenu.GetSettingBus("Bsvepack", ".", ".", "AnnoDocNuovo", strAnnoCompe, " ", strAnnoCompe)
        edSerie.Text = oMenu.GetSettingBus("Bsvepack", ".", ".", "SerieDocNuovo", " ", " ", " ")

        If edSerie.Text = "" Then edSerie.Text = " "

        oClePack.lNumNuovo = oClePack.LeggiNumerazione(UCase(edSerie.Text), NTSCInt(edAnno.Text))
        edNumero.Text = NTSCStr(oClePack.lNumNuovo)
      End If

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

  Public Overridable Sub FRMVESEPA_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case strTipoForm
        Case "N" : cmdConferma.Focus()
        Case Else : edNumero.Focus()
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMVESEPA_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Dim oPar As New CLE__CLDP
    Dim frmSep1 As FRMVESEP1 = Nothing
    Try
      If e.KeyCode = Keys.F5 And edNumero.Focused Then
        frmSep1 = CType(NTSNewFormModal("FRMVESEP1"), FRMVESEP1)
        oPar = New CLE__CLDP
        oPar.strPar2 = edSerie.Text
        oPar.strPar3 = edAnno.Text
        frmSep1.Init(oMenu, oPar, DittaCorrente)
        frmSep1.InitEntity(oClePack)
        frmSep1.ShowDialog()

        If oPar.bPar1 = False Then Return

        edSerie.Text = oPar.strPar2
        edNumero.Text = oPar.strPar4
        cmdConferma_Click(Me, Nothing)
      End If
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    Finally
      If Not frmSep1 Is Nothing Then frmSep1.Dispose()
      frmSep1 = Nothing
    End Try
  End Sub
#End Region

#Region "Eventi"
  Public Overridable Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    Dim dttTmp As New DataTable
    Try
      If Not oClePack.EsistePacking(NTSCInt(edAnno.Text), edSerie.Text, NTSCInt(edNumero.Text), dttTmp) Then Return

      If dttTmp.Rows.Count <> 0 And strTipoForm = "N" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128697414742646348, "Documento già presente in archivio."))
        Return
      ElseIf dttTmp.Rows.Count = 0 And strTipoForm <> "N" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128697414374679953, "Documento non presente in archivio."))
        Return
      End If


      bOk = True

      lAnno = NTSCInt(edAnno.Text)
      strSerie = edSerie.Text
      lNumero = NTSCInt(edNumero.Text)

      If strTipoForm <> "N" Then
        oMenu.SaveSettingBus("Bsvepack", ".", ".", "AnnoDocAperto", edAnno.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus("Bsvepack", ".", ".", "SerieDocAperto", edSerie.Text, " ", "...", "...", "...")
      Else
        oMenu.SaveSettingBus("Bsvepack", ".", ".", "AnnoDocNuovo", edAnno.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus("Bsvepack", ".", ".", "SerieDocNuovo", edSerie.Text, " ", "...", "...", "...")
      End If

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

      If strTipoForm = "N" And Not oClePack Is Nothing Then
        edNumero.Text = NTSCStr(oClePack.LeggiNumerazione(UCase(edSerie.Text), NTSCInt(edAnno.Text)))
      End If
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAnno_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAnno.Validated
    Try
      If strTipoForm = "N" And Not oClePack Is Nothing Then
        edNumero.Text = NTSCStr(oClePack.LeggiNumerazione(UCase(edSerie.Text), NTSCInt(edAnno.Text)))
      End If
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
#End Region
End Class
