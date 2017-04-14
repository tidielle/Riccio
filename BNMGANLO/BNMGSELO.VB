Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGSELO

#Region "Variabili"
  Public oCleAnlo As CLEMGANLO
  Public oCallParams As CLE__CLDP
  Public bAnnullato As Boolean = True
  Public bNew As Boolean = False
  Public bDaProgramma As Boolean = False

  Public lLotto As Integer = 0
  Public strLottox As String = ""

  Public strCodart As String = ""

  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents pnContenitoreControlli As NTSInformatica.NTSPanel
  Public WithEvents lbXx_codart As NTSInformatica.NTSLabel
  Public WithEvents edAlo_lotto As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAlo_codart As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAlo_lotto As NTSInformatica.NTSLabel
  Public WithEvents lbAlo_codart As NTSInformatica.NTSLabel

  Private components As System.ComponentModel.IContainer
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
  Public Overridable Sub InitEntity(ByRef cleAnlo As CLEMGANLO)
    oCleAnlo = cleAnlo
    AddHandler oCleAnlo.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub
  Private Sub InitializeComponent()
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.pnContenitoreControlli = New NTSInformatica.NTSPanel
    Me.edAlo_lottox = New NTSInformatica.NTSTextBoxStr
    Me.lbXx_codart = New NTSInformatica.NTSLabel
    Me.edAlo_lotto = New NTSInformatica.NTSTextBoxNum
    Me.edAlo_codart = New NTSInformatica.NTSTextBoxStr
    Me.lbAlo_lotto = New NTSInformatica.NTSLabel
    Me.lbAlo_codart = New NTSInformatica.NTSLabel
    CType(Me.pnContenitoreControlli, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnContenitoreControlli.SuspendLayout()
    CType(Me.edAlo_lottox.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAlo_lotto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAlo_codart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.cmdAnnulla.Location = New System.Drawing.Point(560, 49)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(116, 23)
    Me.cmdAnnulla.TabIndex = 5
    Me.cmdAnnulla.Text = "Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(560, 24)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(116, 23)
    Me.cmdOk.TabIndex = 4
    Me.cmdOk.Text = "Conferma"
    '
    'pnContenitoreControlli
    '
    Me.pnContenitoreControlli.AllowDrop = True
    Me.pnContenitoreControlli.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnContenitoreControlli.Appearance.Options.UseBackColor = True
    Me.pnContenitoreControlli.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnContenitoreControlli.Controls.Add(Me.edAlo_lottox)
    Me.pnContenitoreControlli.Controls.Add(Me.lbXx_codart)
    Me.pnContenitoreControlli.Controls.Add(Me.edAlo_lotto)
    Me.pnContenitoreControlli.Controls.Add(Me.edAlo_codart)
    Me.pnContenitoreControlli.Controls.Add(Me.lbAlo_lotto)
    Me.pnContenitoreControlli.Controls.Add(Me.lbAlo_codart)
    Me.pnContenitoreControlli.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnContenitoreControlli.Location = New System.Drawing.Point(12, 12)
    Me.pnContenitoreControlli.Name = "pnContenitoreControlli"
    Me.pnContenitoreControlli.Size = New System.Drawing.Size(542, 74)
    Me.pnContenitoreControlli.TabIndex = 3
    '
    'edAlo_lottox
    '
    Me.edAlo_lottox.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAlo_lottox.Location = New System.Drawing.Point(88, 40)
    Me.edAlo_lottox.Name = "edAlo_lottox"
    Me.edAlo_lottox.NTSDbField = ""
    Me.edAlo_lottox.NTSForzaVisZoom = False
    Me.edAlo_lottox.NTSOldValue = ""
    Me.edAlo_lottox.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAlo_lottox.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAlo_lottox.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAlo_lottox.Properties.MaxLength = 65536
    Me.edAlo_lottox.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAlo_lottox.Size = New System.Drawing.Size(153, 20)
    Me.edAlo_lottox.TabIndex = 5
    '
    'lbXx_codart
    '
    Me.lbXx_codart.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codart.Location = New System.Drawing.Point(247, 15)
    Me.lbXx_codart.Name = "lbXx_codart"
    Me.lbXx_codart.NTSDbField = ""
    Me.lbXx_codart.Size = New System.Drawing.Size(277, 20)
    Me.lbXx_codart.TabIndex = 4
    Me.lbXx_codart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_codart.Tooltip = ""
    Me.lbXx_codart.UseMnemonic = False
    '
    'edAlo_lotto
    '
    Me.edAlo_lotto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAlo_lotto.Location = New System.Drawing.Point(489, 40)
    Me.edAlo_lotto.Name = "edAlo_lotto"
    Me.edAlo_lotto.NTSDbField = ""
    Me.edAlo_lotto.NTSFormat = "0"
    Me.edAlo_lotto.NTSForzaVisZoom = False
    Me.edAlo_lotto.NTSOldValue = ""
    Me.edAlo_lotto.Properties.Appearance.Options.UseTextOptions = True
    Me.edAlo_lotto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAlo_lotto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAlo_lotto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAlo_lotto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAlo_lotto.Properties.MaxLength = 65536
    Me.edAlo_lotto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAlo_lotto.Size = New System.Drawing.Size(35, 20)
    Me.edAlo_lotto.TabIndex = 3
    Me.edAlo_lotto.Visible = False
    '
    'edAlo_codart
    '
    Me.edAlo_codart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAlo_codart.Location = New System.Drawing.Point(88, 15)
    Me.edAlo_codart.Name = "edAlo_codart"
    Me.edAlo_codart.NTSDbField = ""
    Me.edAlo_codart.NTSForzaVisZoom = False
    Me.edAlo_codart.NTSOldValue = ""
    Me.edAlo_codart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAlo_codart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAlo_codart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAlo_codart.Properties.MaxLength = 65536
    Me.edAlo_codart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAlo_codart.Size = New System.Drawing.Size(153, 20)
    Me.edAlo_codart.TabIndex = 2
    '
    'lbAlo_lotto
    '
    Me.lbAlo_lotto.AutoSize = True
    Me.lbAlo_lotto.BackColor = System.Drawing.Color.Transparent
    Me.lbAlo_lotto.Location = New System.Drawing.Point(12, 43)
    Me.lbAlo_lotto.Name = "lbAlo_lotto"
    Me.lbAlo_lotto.NTSDbField = ""
    Me.lbAlo_lotto.Size = New System.Drawing.Size(32, 13)
    Me.lbAlo_lotto.TabIndex = 1
    Me.lbAlo_lotto.Text = "Lotto"
    Me.lbAlo_lotto.Tooltip = ""
    Me.lbAlo_lotto.UseMnemonic = False
    '
    'lbAlo_codart
    '
    Me.lbAlo_codart.AutoSize = True
    Me.lbAlo_codart.BackColor = System.Drawing.Color.Transparent
    Me.lbAlo_codart.Location = New System.Drawing.Point(12, 18)
    Me.lbAlo_codart.Name = "lbAlo_codart"
    Me.lbAlo_codart.NTSDbField = ""
    Me.lbAlo_codart.Size = New System.Drawing.Size(43, 13)
    Me.lbAlo_codart.TabIndex = 0
    Me.lbAlo_codart.Text = "Articolo"
    Me.lbAlo_codart.Tooltip = ""
    Me.lbAlo_codart.UseMnemonic = False
    '
    'FRMMGSELO
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(689, 101)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.Controls.Add(Me.pnContenitoreControlli)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMMGSELO"
    Me.Text = ""
    CType(Me.pnContenitoreControlli, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnContenitoreControlli.ResumeLayout(False)
    Me.pnContenitoreControlli.PerformLayout()
    CType(Me.edAlo_lottox.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAlo_lotto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAlo_codart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '----------------------------------------------------------------------------------
      edAlo_codart.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129513008374889525, "Articolo"), tabartico, True)
      edAlo_lotto.NTSSetParam(oMenu, oApp.Tr(Me, 128715615645905308, "Lotto"), "0", 9, 0, 999999999)
      If oCleAnlo.bLottoNew = False Then
        'vecchia gestione solo lotti numerici di max 9 char
        edAlo_lottox.NTSSetParam(oMenu, oApp.Tr(Me, 129513008396880305, "Lotto"), 9, True)
      Else
        'nuova gestione lotto alfanumerico puro di 50 caratteri
        edAlo_lottox.NTSSetParam(oMenu, oApp.Tr(Me, 129513008422074005, "Lotto"), 50, True)
      End If
      '----------------------------------------------------------------------------------
      edAlo_codart.NTSForzaVisZoom = True
      If bNew = False Then edAlo_lottox.NTSForzaVisZoom = True
      '----------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '----------------------------------------------------------------------------------

    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

#Region "Form"
  Public Overridable Sub FRMMGSELO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '----------------------------------------------------------------------------------
      InitControls()
      '----------------------------------------------------------------------------------
      edAlo_lotto.Text = "0"
      edAlo_lottox.Text = ""
      If oCleAnlo.bLottoNew = False Then
        'vecchia gestione solo numerica: propongo il progressivo
        If oCleAnlo.strGenNumLotto = "U" And bNew Then
          edAlo_lottox.Text = oCleAnlo.LegNuma().ToString("000000000")
        End If
      End If

      If bNew Then
        lbAlo_lotto.Text = oApp.Tr(Me, 129513019204346570, "Lotto")
      Else
        lbAlo_lotto.Text = oApp.Tr(Me, 129513019059473030, "Lotto (facolt.)")
      End If

      '----------------------------------------------------------------------------------
      GctlSetRoules()
      '----------------------------------------------------------------------------------
      If bDaProgramma = True Then edAlo_codart.Text = strCodart
      edAlo_codart.Focus()
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMMGSELO_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Dim ctrlTmp As Control = Nothing
    Try
      '----------------------------------------------------------------------------------
      'gestione dello zoom:
      'eseguo la Zoom, tanto se per il controllo non deve venir eseguito uno zoom particolare, la routine non fa nulla e viene processato lo zoom standard del controllo, settato con la NTSSetParamZoom
      '----------------------------------------------------------------------------------
      If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
        Zoom()
        e.Handled = True    'altrimenti anche il controllo riceve l'F5 e la routine ZOOM viene eseguita 2 volte!!!
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Sub edAlo_codart_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAlo_codart.Validated
    Dim bEsiste As Boolean = False
    Try
      '----------------------------------------------------------------------------------
      edAlo_codart.Text = edAlo_codart.Text.ToUpper
      '----------------------------------------------------------------------------------
      If (edAlo_codart.Text.Trim = "") Or (edAlo_codart.Text.Trim = ".") Then
        lbXx_codart.Text = ""
        Return
      End If
      '----------------------------------------------------------------------------------
      oCleAnlo.DesCodart(DittaCorrente, edAlo_codart.Text, lbXx_codart.Text, bEsiste)
      If bEsiste = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128715621647286575, "Codice articolo inesistente."))
        edAlo_codart.Text = ""
        Return
      End If
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub edAlo_codart_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAlo_codart.ValidatedAndChanged
    Try
      '----------------------------------------------------------------------------------
      If (edAlo_codart.Text.Trim = "") Or (bNew = False) Or (oCleAnlo.strGenNumLotto <> "A") Then Return
      '---------------------------------------------------------- ------------------------
      If oCleAnlo.bLottoNew = False Then
        'vecchia gestione solo numerica: calcolo il progressivo lotto per articolo
        edAlo_lottox.Text = oCleAnlo.ProgressivoLottoPerArticolo(edAlo_codart.Text).ToString("000000000")
      End If

      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub edAlo_lottox_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAlo_lottox.ValidatedAndChanged
    Try
      If oCleAnlo.bLottoNew = False Then
        'vecchia gestione solo numerica
        'il numero di lotto è sempre di 9 caratteri con 0 per occupare gli spazi mancanti
        edAlo_lottox.Text = NTSCInt(edAlo_lottox.Text).ToString("000000000")
      End If

    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Me.Close()
  End Sub
  Public Overridable Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Dim strMsg As String = ""
    Dim i As Integer = 0
    Try
      '------------------------------------------------------------------------------------
      '--- Il codice articolo deve essere sempre indicato
      '------------------------------------------------------------------------------------
      If (edAlo_codart.Text.Trim = "") Or (edAlo_codart.Text.Trim = ".") Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128715623595725114, "Indicare un codice articolo valido."))
        edAlo_codart.Focus()
        Return
      End If
      '------------------------------------------------------------------------------------
      '--- Se si è in inserimento di una nuova Anagrafica non è stato indicato il lotto, 
      '--- a seconda dell'opzione di registro, lo determina adesso
      '------------------------------------------------------------------------------------
      If oCleAnlo.bLottoNew = False Then
        'vecchia gestione solo numerica
        If (bNew = True) And (NTSCInt(edAlo_lottox.Text) = 0) Then
          Select Case oCleAnlo.strGenNumLotto
            Case "U" : edAlo_lottox.Text = oCleAnlo.LegNuma().ToString("000000000")
            Case "A" : edAlo_lottox.Text = oCleAnlo.ProgressivoLottoPerArticolo(edAlo_codart.Text).ToString("000000000")
          End Select
        End If
      End If

      '------------------------------------------------------------------------------------
      '--- Se si è in inserimento e non è stato indicato il numero di lotto, avvisa
      '--- Se si è in modifica ed è stato indicato il numero di lotto, controlla
      '--- l'esistenza dei dati
      '------------------------------------------------------------------------------------
      Select Case bNew
        Case True
          If oCleAnlo.bLottoNew = False Then
            If NTSCInt(edAlo_lottox.Text) = 0 Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128715623912912614, "Indicare un NUMERO di lotto valido."))
              edAlo_lottox.Focus()
              Return
            End If
          Else
            If edAlo_lottox.Text.Trim = "" Then
              oApp.MsgBoxErr(oApp.Tr(Me, 129512988870349678, "Indicare un codice di lotto valido."))
              edAlo_lottox.Focus()
              Return
            End If
          End If

          If oCleAnlo.CheckEsistenzaDati(edAlo_codart.Text, NTSCInt(edAlo_lotto.Text), edAlo_lottox.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128722755026513012, "Esistono dati con queste caratteristiche."))
            Return
          End If
        Case False
          If Not oCleAnlo.CheckEsistenzaDati(edAlo_codart.Text, NTSCInt(edAlo_lotto.Text), edAlo_lottox.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128715860148947289, "Non esistono dati con queste caratteristiche."))
            Return
          End If
      End Select

      If oCleAnlo.bLottoNew = False Then
        'alo_lotto sarà sampre uguale a alo_lottox
        edAlo_lotto.Text = NTSCInt(edAlo_lottox.Text).ToString
      Else
        If bNew Then
          'determino il nuovo cod. lotto (in pratica è un ID nascosto)
          oCleAnlo.GetNewID(i)
          edAlo_lotto.Text = i.ToString
        Else
          'dato il cod. alfanumerico cerco il cod numerico
          oCleAnlo.GetIdFromLottox(edAlo_codart.Text, edAlo_lottox.Text, i)
          edAlo_lotto.Text = i.ToString
        End If
      End If    'If oCleAnlo.bLottoNew = False Then

      '------------------------------------------------------------------------------------
      oCleAnlo.strCodart = edAlo_codart.Text
      oCleAnlo.strDescodart = lbXx_codart.Text
      oCleAnlo.lLotto = NTSCInt(edAlo_lotto.Text)

      oCleAnlo.strLottox = edAlo_lottox.Text
       bAnnullato = False
      '------------------------------------------------------------------------------------
       Me.Close()
      '------------------------------------------------------------------------------------

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub Zoom()
    Dim ctlLastControl As Control
    Dim oParam As New CLE__PATB

    Try
      ctlLastControl = NTSFindControlFocused(Me)
      If ctlLastControl Is Nothing Then Return
      '----------------------------------------------------------------------------------
      If edAlo_lottox.Focused = True Then
        If edAlo_codart.Text.Trim = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128715626031029797, "Indicare un codice articolo valido prima di passare alla selezione dei lotti."))
          Return
        End If
        oParam.strTipo = edAlo_codart.Text
        NTSZOOM.strIn = edAlo_lottox.Text
        NTSZOOM.ZoomStrIn("ZOOMANALOTTI", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edAlo_lottox.Text Then edAlo_lottox.NTSTextDB = NTSZOOM.strIn
      Else
        'zoom standard di textbox e griglia
        'SendKeys.SendWait("{F5}") 'se faccio questa riga va in un loop infinito....
        NTSCallStandardZoom()
      End If
      '----------------------------------------------------------------------------------
      '--- riporto il focus sul controllo che ha chiamato lo zoom
      '----------------------------------------------------------------------------------
      ctlLastControl.Focus()
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class