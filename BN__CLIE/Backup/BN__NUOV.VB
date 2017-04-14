Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__NUOV
  Public oCallParams As CLE__CLDP
  Public oCleClie As CLE__CLIE
  Public bOk As Boolean = False
  Public bNew As Boolean = False
  Public strCodPcon As String = ""
  Public bNewCodProposto As Boolean = True
  Public bProgrtmp As Integer = 0

  Public Overridable Sub InitializeComponent()
    Me.edProgr = New NTSInformatica.NTSTextBoxNum
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.lbProgr = New NTSInformatica.NTSLabel
    Me.pnNuovo = New NTSInformatica.NTSPanel
    Me.lbMastro = New NTSInformatica.NTSLabel
    Me.lbLead = New NTSInformatica.NTSLabel
    Me.lbAnagen = New NTSInformatica.NTSLabel
    Me.lbLeadlabel = New NTSInformatica.NTSLabel
    Me.edAnagen = New NTSInformatica.NTSTextBoxNum
    Me.edLead = New NTSInformatica.NTSTextBoxNum
    Me.lbAnagenlabel = New NTSInformatica.NTSLabel
    Me.edMastro = New NTSInformatica.NTSTextBoxNum
    Me.lbMastrolabel = New NTSInformatica.NTSLabel
    CType(Me.edProgr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnNuovo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnNuovo.SuspendLayout()
    CType(Me.edAnagen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edLead.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edMastro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'DevXDefaultLookAndFeel
    '
    
    '
    'edProgr
    '
    Me.edProgr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edProgr.EditValue = "0"
    Me.edProgr.Location = New System.Drawing.Point(113, 32)
    Me.edProgr.Name = "edProgr"
    Me.edProgr.NTSDbField = ""
    Me.edProgr.NTSFormat = "0"
    Me.edProgr.NTSForzaVisZoom = False
    Me.edProgr.NTSOldValue = "0"
    Me.edProgr.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edProgr.Properties.Appearance.Options.UseBackColor = True
    Me.edProgr.Properties.Appearance.Options.UseTextOptions = True
    Me.edProgr.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edProgr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edProgr.Properties.MaxLength = 65536
    Me.edProgr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edProgr.Size = New System.Drawing.Size(83, 20)
    Me.edProgr.TabIndex = 0
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Location = New System.Drawing.Point(455, 34)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(105, 22)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.Location = New System.Drawing.Point(455, 6)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(105, 22)
    Me.cmdOk.TabIndex = 2
    Me.cmdOk.Text = "&Conferma"
    '
    'lbProgr
    '
    Me.lbProgr.AutoSize = True
    Me.lbProgr.BackColor = System.Drawing.Color.Transparent
    Me.lbProgr.Location = New System.Drawing.Point(3, 35)
    Me.lbProgr.Name = "lbProgr"
    Me.lbProgr.NTSDbField = ""
    Me.lbProgr.Size = New System.Drawing.Size(69, 13)
    Me.lbProgr.TabIndex = 5
    Me.lbProgr.Text = "Codice conto"
    '
    'pnNuovo
    '
    Me.pnNuovo.AllowDrop = True
    Me.pnNuovo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnNuovo.Appearance.Options.UseBackColor = True
    Me.pnNuovo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnNuovo.Controls.Add(Me.lbMastro)
    Me.pnNuovo.Controls.Add(Me.lbLead)
    Me.pnNuovo.Controls.Add(Me.lbAnagen)
    Me.pnNuovo.Controls.Add(Me.lbLeadlabel)
    Me.pnNuovo.Controls.Add(Me.edAnagen)
    Me.pnNuovo.Controls.Add(Me.edLead)
    Me.pnNuovo.Controls.Add(Me.lbAnagenlabel)
    Me.pnNuovo.Controls.Add(Me.edMastro)
    Me.pnNuovo.Controls.Add(Me.lbMastrolabel)
    Me.pnNuovo.Controls.Add(Me.edProgr)
    Me.pnNuovo.Controls.Add(Me.lbProgr)
    Me.pnNuovo.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnNuovo.Location = New System.Drawing.Point(2, 0)
    Me.pnNuovo.Name = "pnNuovo"
    Me.pnNuovo.Size = New System.Drawing.Size(447, 120)
    Me.pnNuovo.TabIndex = 9
    Me.pnNuovo.Text = "NtsPanel1"
    '
    'lbMastro
    '
    Me.lbMastro.BackColor = System.Drawing.Color.Transparent
    Me.lbMastro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbMastro.Location = New System.Drawing.Point(202, 6)
    Me.lbMastro.Name = "lbMastro"
    Me.lbMastro.NTSDbField = ""
    Me.lbMastro.Size = New System.Drawing.Size(242, 20)
    Me.lbMastro.TabIndex = 581
    Me.lbMastro.Text = "lbMastro"
    '
    'lbLead
    '
    Me.lbLead.BackColor = System.Drawing.Color.Transparent
    Me.lbLead.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbLead.Location = New System.Drawing.Point(202, 94)
    Me.lbLead.Name = "lbLead"
    Me.lbLead.NTSDbField = ""
    Me.lbLead.Size = New System.Drawing.Size(242, 20)
    Me.lbLead.TabIndex = 580
    Me.lbLead.Text = "lbLead"
    '
    'lbAnagen
    '
    Me.lbAnagen.BackColor = System.Drawing.Color.Transparent
    Me.lbAnagen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbAnagen.Location = New System.Drawing.Point(202, 68)
    Me.lbAnagen.Name = "lbAnagen"
    Me.lbAnagen.NTSDbField = ""
    Me.lbAnagen.Size = New System.Drawing.Size(242, 20)
    Me.lbAnagen.TabIndex = 579
    Me.lbAnagen.Text = "lbAnagen"
    '
    'lbLeadlabel
    '
    Me.lbLeadlabel.AutoSize = True
    Me.lbLeadlabel.BackColor = System.Drawing.Color.Transparent
    Me.lbLeadlabel.Location = New System.Drawing.Point(3, 97)
    Me.lbLeadlabel.Name = "lbLeadlabel"
    Me.lbLeadlabel.NTSDbField = ""
    Me.lbLeadlabel.Size = New System.Drawing.Size(68, 13)
    Me.lbLeadlabel.TabIndex = 11
    Me.lbLeadlabel.Text = "Crea da lead"
    '
    'edAnagen
    '
    Me.edAnagen.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnagen.EditValue = "0"
    Me.edAnagen.Location = New System.Drawing.Point(113, 68)
    Me.edAnagen.Name = "edAnagen"
    Me.edAnagen.NTSDbField = ""
    Me.edAnagen.NTSFormat = "0"
    Me.edAnagen.NTSForzaVisZoom = False
    Me.edAnagen.NTSOldValue = "0"
    Me.edAnagen.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAnagen.Properties.Appearance.Options.UseBackColor = True
    Me.edAnagen.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnagen.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnagen.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnagen.Properties.MaxLength = 65536
    Me.edAnagen.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnagen.Size = New System.Drawing.Size(83, 20)
    Me.edAnagen.TabIndex = 10
    '
    'edLead
    '
    Me.edLead.Cursor = System.Windows.Forms.Cursors.Default
    Me.edLead.EditValue = "0"
    Me.edLead.Location = New System.Drawing.Point(113, 94)
    Me.edLead.Name = "edLead"
    Me.edLead.NTSDbField = ""
    Me.edLead.NTSFormat = "0"
    Me.edLead.NTSForzaVisZoom = False
    Me.edLead.NTSOldValue = "0"
    Me.edLead.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edLead.Properties.Appearance.Options.UseBackColor = True
    Me.edLead.Properties.Appearance.Options.UseTextOptions = True
    Me.edLead.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edLead.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edLead.Properties.MaxLength = 65536
    Me.edLead.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edLead.Size = New System.Drawing.Size(83, 20)
    Me.edLead.TabIndex = 9
    '
    'lbAnagenlabel
    '
    Me.lbAnagenlabel.AutoSize = True
    Me.lbAnagenlabel.BackColor = System.Drawing.Color.Transparent
    Me.lbAnagenlabel.Location = New System.Drawing.Point(3, 71)
    Me.lbAnagenlabel.Name = "lbAnagenlabel"
    Me.lbAnagenlabel.NTSDbField = ""
    Me.lbAnagenlabel.Size = New System.Drawing.Size(106, 13)
    Me.lbAnagenlabel.TabIndex = 8
    Me.lbAnagenlabel.Text = "Crea da Anagr. gen."
    '
    'edMastro
    '
    Me.edMastro.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edMastro.EditValue = "0"
    Me.edMastro.Location = New System.Drawing.Point(113, 6)
    Me.edMastro.Name = "edMastro"
    Me.edMastro.NTSDbField = ""
    Me.edMastro.NTSFormat = "0"
    Me.edMastro.NTSForzaVisZoom = False
    Me.edMastro.NTSOldValue = "0"
    Me.edMastro.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edMastro.Properties.Appearance.Options.UseBackColor = True
    Me.edMastro.Properties.Appearance.Options.UseTextOptions = True
    Me.edMastro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMastro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMastro.Properties.MaxLength = 65536
    Me.edMastro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMastro.Size = New System.Drawing.Size(83, 20)
    Me.edMastro.TabIndex = 7
    '
    'lbMastrolabel
    '
    Me.lbMastrolabel.AutoSize = True
    Me.lbMastrolabel.BackColor = System.Drawing.Color.Transparent
    Me.lbMastrolabel.Location = New System.Drawing.Point(3, 9)
    Me.lbMastrolabel.Name = "lbMastrolabel"
    Me.lbMastrolabel.NTSDbField = ""
    Me.lbMastrolabel.Size = New System.Drawing.Size(75, 13)
    Me.lbMastrolabel.TabIndex = 6
    Me.lbMastrolabel.Text = "Codice mastro"
    '
    'FRM__NUOV
    '
    Me.ClientSize = New System.Drawing.Size(572, 118)
    Me.Controls.Add(Me.pnNuovo)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.HelpContext = ""
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__NUOV"
    Me.Text = "Nuova anagrafica"
    CType(Me.edProgr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnNuovo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnNuovo.ResumeLayout(False)
    Me.pnNuovo.PerformLayout()
    CType(Me.edAnagen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edLead.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edMastro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
      edAnagen.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128377746572454000, "Anagrafica generale"), tabanagen)
      edLead.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128377746572610000, "Lead"), tableads)
      edMastro.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128969754794195891, "Mastro"), tabmast)
      edProgr.NTSSetParam(oMenu, oApp.Tr(Me, 128969754962638781, "Progressivo"), "0", 6, 0, 999999)

      edMastro.NTSSetParamZoom("ZOOMTABMAST")

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


#Region "Eventi di Form"
  Public Overridable Sub FRM__NUOV_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      AddHandler oCleClie.RemoteEvent, AddressOf GestisciEventiEntity

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      strCodPcon = oCleClie.dttAnaz.Rows(0)!tb_azcodpcon.ToString
      lbAnagen.Text = ""
      lbLead.Text = ""
      lbMastro.Text = ""
      bOk = False

      If oCleClie.bAnagen = False And oCleClie.bModuloCRM = False And oCleClie.bModuloAS = False Then
        Me.Height = 90
      End If

      '--------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlTipoDoc = oCleClie.strTipoConto
      GctlSetRoules()
      GctlApplicaDefaultValue()

      If oCleClie.bAnagen = False Then
        edAnagen.Text = "0"
        edAnagen.Visible = False
        lbAnagen.Visible = False
        lbAnagenlabel.Visible = False
      End If

      If (oCleClie.bModuloCRM = False And oCleClie.bModuloAS = False) Or oCleClie.strTipoConto <> "C" Then
        edLead.Text = "0"
        edLead.Visible = False
        lbLead.Visible = False
        lbLeadlabel.Visible = False
      End If


    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRM__NUOV_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      If NTSCInt(edMastro.Text) = 0 Then
        Select Case oCleClie.strTipoConto
          Case "C"
            edMastro.Text = NTSCInt(oMenu.GetSettingBusDitt(DittaCorrente, "BS--CLNV", "RECENT", ".", "MastroNuovoClie", "0", " ", "0")).ToString
          Case "F"
            edMastro.Text = NTSCInt(oMenu.GetSettingBusDitt(DittaCorrente, "BS--CLNV", "RECENT", ".", "MastroNuovoForn", "0", " ", "0")).ToString
        End Select
      End If
      If NTSCInt(edMastro.Text) <> 0 Then edMastro_Leave(edMastro, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub FRM__NUOV_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Dim ctrlTmp As Control = Nothing
    Try
      '--------------------------------------------
      'gestione dello zoom:
      'eseguo la Zoom, tanto se per il controllo non deve venir eseguito uno zoom particolare, la routine non fa nulla e viene processato lo zoom standard del controllo, settato con la NTSSetParamZoom
      If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
        Zoom()
        e.Handled = True    'altrimenti anche il controllo riceve l'F5 e la routine ZOOM viene eseguita 2 volte!!!
      End If

      If e.KeyCode = Keys.Enter And NTSCInt(edProgr.Text) <> 0 And edProgr.Text.Length > 6 Then cmdOk_Click(cmdOk, Nothing)

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
      'entro qui perchè nella FRM__HLAN_KeyDown ho inserito il seguente codice:
      'If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
      '  Zoom()
      '  e.Handled = True
      'End If

      ctlLastControl = NTSFindControlFocused(Me)
      If ctlLastControl Is Nothing Then Return

      If edMastro.Focused Then
        '----------------------------------------------
        'zoom specifico per mastri di contabilità
        oParam.strCodPdc = strCodPcon     'passo il piano dei conti
        SetFastZoom(edMastro.Text, oParam) 'gestione dello zoom veloce
        NTSZOOM.strIn = edMastro.Text
        NTSZOOM.ZoomStrIn("ZOOMTABMAST", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edMastro.Text Then edMastro.Text = NTSZOOM.strIn
      ElseIf edLead.Focused Then
        SetFastZoom(edLead.Text, oParam) 'gestione dello zoom veloce
        oParam.nAnno = -1               'devo visualizzare solo i lead non collegati a clienti 
        oParam.strIn = edLead.Text
        NTSZOOM.ZoomStrIn("ZOOMLEADS", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edLead.Text Then edLead.Text = NTSZOOM.strIn
      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        'SendKeys.SendWait("{F5}") 'se faccio questa riga va in un loop infinito....
        NTSCallStandardZoom()
      End If
      NTSSendKeys.SendWait(0, "{TAB}")
      '------------------------------------
      'riporto il focus sul controllo che ha chiamato lo zoom
      ctlLastControl.Focus()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAnagen_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles edAnagen.NTSZoomGest
    Dim oCZoo As New CLE__CZOO
    Dim bNuovo As Boolean = True
    Dim oTmp As New Control
    Dim oPar As New CLE__CLDP
    Try
      Me.ValidaLastControl()

      If e.TipoEvento = "OPEN" Then
        bNuovo = False
        oPar.strParam = NTSCInt(edAnagen.Text).ToString("000000000")
      Else
        oPar.strParam = "NUOV;0"
      End If

      oTmp.Text = NTSCInt(edAnagen.Text).ToString("000000000")
      NTSZOOM.OpenChildGest(oTmp, "ZOOMANAGEN", DittaCorrente, bNuovo, oPar)

      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo
      edAnagen.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edLead_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles edLead.NTSZoomGest
    Dim oCZoo As New CLE__CZOO
    Dim bNuovo As Boolean = True
    Dim oTmp As New Control
    Dim oPar As New CLE__CLDP
    Try
      Me.ValidaLastControl()
      If e.TipoEvento = "OPEN" Then
        bNuovo = False
        oPar.strParam = "APRI;LEAD;" & edLead.Text.PadLeft(9, CChar("0"))
      Else
        oPar.strParam = "NUOV;0"
      End If
      oTmp.Text = edLead.Text
      NTSZOOM.OpenChildGest(oTmp, "ZOOMLEADS", DittaCorrente, bNuovo, oPar)

      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo
      edLead.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edMastro_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles edMastro.NTSZoomGest
    Try
      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Validazione conti"
  Public Overridable Sub edAnagen_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAnagen.Leave
    Dim strTmp As String = ""
    Dim lProgr As Integer = 0
    Try
      If oCleClie.dttAnaz.Rows(0)!tb_flriccf.ToString <> "S" Then bNewCodProposto = True

      If NTSCInt(edAnagen.Text) = 0 Then
        lbAnagen.Text = ""
        If oCleClie.dttAnaz.Rows(0)!tb_flriccf.ToString <> "S" Then edProgr.Text = "0"
        Return
      End If

      If oCleClie.TestAnagen(NTSCInt(edAnagen.Text), strTmp, lProgr) = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128738065077280000, "Codice anagrafica generale inesistente"))
        edAnagen.Text = "0"
        lbAnagen.Text = ""
      Else
        lbAnagen.Text = strTmp
        If oCleClie.dttAnaz.Rows(0)!tb_flriccf.ToString <> "S" Then
          edProgr.Text = lProgr.ToString
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edLead_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edLead.Leave
    Dim strTmp As String = ""
    Dim lProgr As Integer = 0
    Try
      If NTSCInt(edLead.Text) = 0 Then
        lbLead.Text = ""
        Return
      End If

      If oCleClie.TestLead(NTSCInt(edLead.Text), strTmp) = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128377707694012000, "Codice lead inesistente (o già collegato ad un cliente)"))
        edLead.Text = "0"
        lbLead.Text = ""
      Else
        lbLead.Text = strTmp
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edMastro_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edMastro.Leave
    Dim strTmp As String = ""
    Dim lProgr As Integer = 0
    Try
      If oCleClie.dttAnaz.Rows(0)!tb_flriccf.ToString = "S" Then bNewCodProposto = True

      If NTSCInt(edMastro.Text) = 0 Then
        lbMastro.Text = ""
        If oCleClie.dttAnaz.Rows(0)!tb_flriccf.ToString = "S" Then edProgr.Text = "0"
        Return
      End If

      If oCleClie.TestMastro(NTSCInt(edMastro.Text), strTmp, strCodPcon, oCleClie.strTipoConto, lProgr) = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128969755729221932, "Codice Mastro contabile non corretto (inesistente o di tipo cliente/fornitore non compatibile)"))
        edMastro.Text = "0"
        lbMastro.Text = ""
        edProgr.Text = "0"
      Else
        lbMastro.Text = strTmp
        If oCleClie.dttAnaz.Rows(0)!tb_flriccf.ToString = "S" Then
          edProgr.Text = lProgr.ToString
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub


  Public Overridable Sub edProgr_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edProgr.Enter
    Try
      bProgrtmp = NTSCInt(edProgr.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edProgr_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edProgr.Leave
    Try
      If bProgrtmp <> NTSCInt(edProgr.Text) Then bNewCodProposto = False

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      If NTSCInt(edProgr.Text) <= 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128969755934695942, "Indicare il codice cliente/fornitore"))
        Return
      End If

      If NTSCInt(edProgr.Text) >= oCleClie.lContoProgrMoltip Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128969756177045896, "Progressivo cliente/fornitore di lunghezza superiore a quella consentita dal piano dei conti in uso (il numero deve essere inferiore a |" & oCleClie.lContoProgrMoltip.ToString & "|)"))
        Return
      End If


      If NTSCInt(edMastro.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128969756323768402, "Indicare il mastro a cui deve appartenere il nuovo cliente/fornitore"))
        Return
      End If

      If NTSCInt(edAnagen.Text) <> 0 And NTSCInt(edLead.Text) <> 0 Then
        If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 131066636647966724, "Attenzione: indicando sia un codice di anagrafica generale che un codice lead," & vbCrLf & _
                                  "i dati dell'anagrafica lead in comune con l'anagrafica generale e l'organizzazione del lead " & _
                                  "verranno persi e sostituiti con quello dell'anagrafica generale." & vbCrLf & _
                                  "proseguire?")) = Windows.Forms.DialogResult.No Then Return
        'oApp.MsgBoxErr(oApp.Tr(Me, 128377741202782000, "Il nuovo cliente/fornitore può ereditare o da Lead o da Anagrafica generale, non da entrambi"))
      End If

      Select Case oCleClie.strTipoConto
        Case "C"
          oMenu.SaveSettingBusDitt(DittaCorrente, "BS--CLNV", "RECENT", ".", "MastroNuovoClie", edMastro.Text, " ", ".S.", ".S.", ".N.")
        Case "F"
          oMenu.SaveSettingBusDitt(DittaCorrente, "BS--CLNV", "RECENT", ".", "MastroNuovoForn", edMastro.Text, " ", ".S.", ".S.", ".N.")
      End Select

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