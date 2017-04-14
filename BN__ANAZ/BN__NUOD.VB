Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__NUOD
  Public oCallParams As CLE__CLDP
  Public oCleAnaz As CLE__ANAZ
  Public bOk As Boolean = False
  Public bNew As Boolean = False

  Public Overridable Sub InitializeComponent()
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.lbProgr = New NTSInformatica.NTSLabel
    Me.pnNuovo = New NTSInformatica.NTSPanel
    Me.edCodditt = New NTSInformatica.NTSTextBoxStr
    Me.lbAnagen = New NTSInformatica.NTSLabel
    Me.edAnagen = New NTSInformatica.NTSTextBoxNum
    Me.lbAnagenlabel = New NTSInformatica.NTSLabel
    CType(Me.pnNuovo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnNuovo.SuspendLayout()
    CType(Me.edCodditt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnagen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.lbProgr.Location = New System.Drawing.Point(3, 12)
    Me.lbProgr.Name = "lbProgr"
    Me.lbProgr.NTSDbField = ""
    Me.lbProgr.Size = New System.Drawing.Size(64, 13)
    Me.lbProgr.TabIndex = 5
    Me.lbProgr.Text = "Codice ditta"
    Me.lbProgr.UseMnemonic = False
    '
    'pnNuovo
    '
    Me.pnNuovo.AllowDrop = True
    Me.pnNuovo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnNuovo.Appearance.Options.UseBackColor = True
    Me.pnNuovo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnNuovo.Controls.Add(Me.edCodditt)
    Me.pnNuovo.Controls.Add(Me.lbAnagen)
    Me.pnNuovo.Controls.Add(Me.edAnagen)
    Me.pnNuovo.Controls.Add(Me.lbAnagenlabel)
    Me.pnNuovo.Controls.Add(Me.lbProgr)
    Me.pnNuovo.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnNuovo.Location = New System.Drawing.Point(2, 0)
    Me.pnNuovo.Name = "pnNuovo"
    Me.pnNuovo.Size = New System.Drawing.Size(447, 63)
    Me.pnNuovo.TabIndex = 9
    Me.pnNuovo.Text = "NtsPanel1"
    '
    'edCodditt
    '
    Me.edCodditt.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodditt.Location = New System.Drawing.Point(113, 9)
    Me.edCodditt.Name = "edCodditt"
    Me.edCodditt.NTSDbField = ""
    Me.edCodditt.NTSForzaVisZoom = False
    Me.edCodditt.NTSOldValue = ""
    Me.edCodditt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodditt.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodditt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodditt.Properties.MaxLength = 65536
    Me.edCodditt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodditt.Size = New System.Drawing.Size(147, 20)
    Me.edCodditt.TabIndex = 580
    '
    'lbAnagen
    '
    Me.lbAnagen.BackColor = System.Drawing.Color.Transparent
    Me.lbAnagen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbAnagen.Location = New System.Drawing.Point(202, 36)
    Me.lbAnagen.Name = "lbAnagen"
    Me.lbAnagen.NTSDbField = ""
    Me.lbAnagen.Size = New System.Drawing.Size(242, 20)
    Me.lbAnagen.TabIndex = 579
    Me.lbAnagen.Text = "lbAnagen"
    Me.lbAnagen.UseMnemonic = False
    '
    'edAnagen
    '
    Me.edAnagen.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnagen.EditValue = "0"
    Me.edAnagen.Location = New System.Drawing.Point(113, 36)
    Me.edAnagen.Name = "edAnagen"
    Me.edAnagen.NTSDbField = ""
    Me.edAnagen.NTSFormat = "0"
    Me.edAnagen.NTSForzaVisZoom = False
    Me.edAnagen.NTSOldValue = "0"
    Me.edAnagen.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAnagen.Properties.Appearance.Options.UseBackColor = True
    Me.edAnagen.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnagen.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnagen.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnagen.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnagen.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnagen.Properties.MaxLength = 65536
    Me.edAnagen.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnagen.Size = New System.Drawing.Size(83, 20)
    Me.edAnagen.TabIndex = 10
    '
    'lbAnagenlabel
    '
    Me.lbAnagenlabel.AutoSize = True
    Me.lbAnagenlabel.BackColor = System.Drawing.Color.Transparent
    Me.lbAnagenlabel.Location = New System.Drawing.Point(3, 39)
    Me.lbAnagenlabel.Name = "lbAnagenlabel"
    Me.lbAnagenlabel.NTSDbField = ""
    Me.lbAnagenlabel.Size = New System.Drawing.Size(106, 13)
    Me.lbAnagenlabel.TabIndex = 8
    Me.lbAnagenlabel.Text = "Crea da Anagr. gen."
    Me.lbAnagenlabel.UseMnemonic = False
    '
    'FRM__NUOD
    '
    Me.ClientSize = New System.Drawing.Size(572, 63)
    Me.Controls.Add(Me.pnNuovo)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.HelpContext = ""
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__NUOD"
    Me.Text = "NUOVA DITTA"
    CType(Me.pnNuovo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnNuovo.ResumeLayout(False)
    Me.pnNuovo.PerformLayout()
    CType(Me.edCodditt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnagen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    oMenu = Menu
    oApp = oMenu.App
    oCallParams = Param
    DittaCorrente = oApp.Ditta

    Me.GctlTipoDoc = ""

    InitializeComponent()
    Me.MinimumSize = Me.Size

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      edAnagen.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128377746572454000, "Anagrafica generale"), tabanagen)
      edCodditt.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128377746572610000, "Ditta"), tabanaz, True)
      edCodditt.NTSSetParamZoom("")

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
      AddHandler oCleAnaz.RemoteEvent, AddressOf GestisciEventiEntity

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      lbAnagen.Text = ""
      edCodditt.Text = ""
      bOk = False

      '--------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
      GctlApplicaDefaultValue()

      If oCleAnaz.bAnagen = False Then
        edAnagen.Text = "0"
        edAnagen.Visible = False
        lbAnagen.Visible = False
        lbAnagenlabel.Visible = False
      End If

      Me.Text = oApp.Tr(Me, 128642211441562500, "NUOVA DITTA")

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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

      Me.NTSCallStandardZoom()

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
#End Region

  Public Overridable Sub edAnagen_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAnagen.Leave
    Dim strTmp As String = ""
    Try

      If NTSCInt(edAnagen.Text) = 0 Then
        lbAnagen.Text = ""
        Return
      End If

      If oCleAnaz.TestAnagen(NTSCInt(edAnagen.Text), strTmp) = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128377707694012000, "Codice anagrafica generale inesistente"))
        edAnagen.Text = "0"
        lbAnagen.Text = ""
      Else
        lbAnagen.Text = strTmp
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub


  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Dim dttTmp As New DataTable
    Try
      edCodditt.Text = edCodditt.Text.Trim

      If edCodditt.Text.Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128272203434990000, "Indicare il codice da assegnare alla nuova DITTA"))
        Return
      End If

      If edCodditt.Text.Trim.IndexOf("'") > -1 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128765040429384000, "Il codice ditta indicato non può contenere apici semplici"))
        Return
      End If

      If edCodditt.Text.Trim.IndexOf(" ") > -1 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128642207812656250, "Il codice ditta indicato non può contenere spazi"))
        Return
      End If

      If NTSCInt(edAnagen.Text) = 0 And oCleAnaz.bAnagen Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128377741202782000, "Indicare il codice di anagrafica generale da associare alla nuova ditta"))
        Return
      End If

      oMenu.ValCodiceDb(edCodditt.Text, "", "TABANAZ", "S", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then
        dttTmp.Clear()
        oApp.MsgBoxErr(oApp.Tr(Me, 128642208750625000, "Codice ditta già esistente"))
        Return
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

End Class