Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__HLNA
  Public oCleHlan As CLE__HLAN
  Public oCallParams As CLE__CLDP

  Public lcodAnagen as integer
  Public strCodPcon As String
  Public strTipoConto As String
  Public strRicodificaCF As String
  Public nMaxLenConto As Integer = 4
  Public lProgrProposto as integer = 0

  Private components As System.ComponentModel.IContainer

  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    Try
      'oCallParams.strPar1 = tipo conto (C/F)
      'oCallParams.strPar2 = codice piano dei conti
      'oCallParams.dPar1 = deve restituire true se confermo la creazione del nuovo conto , se abortisco FALSE
      'oCallParams.dPar2 = conto di anagrafica generale selezionato in griglia di bn__hlan
      'oCallParams.dPar3 = mastro del nuovo cliente/fornitore
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
      strCodPcon = oCallParams.strPar2
      strTipoConto = oCallParams.strPar1
      lcodAnagen = NTSCInt(oCallParams.dPar2)

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub InitializeComponent()
    Me.NtsLabel1 = New NTSInformatica.NTSLabel
    Me.NtsLabel2 = New NTSInformatica.NTSLabel
    Me.NtsPanel1 = New NTSInformatica.NTSPanel
    Me.lbMastro = New NTSInformatica.NTSLabel
    Me.edConto = New NTSInformatica.NTSTextBoxNum
    Me.edMastro = New NTSInformatica.NTSTextBoxNum
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel1.SuspendLayout()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'frmAuto
    '
    Me.frmAuto.Appearance.BackColor = System.Drawing.Color.Black
    Me.frmAuto.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmAuto.Appearance.Options.UseBackColor = True
    Me.frmAuto.Appearance.Options.UseImage = True
    '
    'NtsLabel1
    '
    Me.NtsLabel1.AutoSize = True
    Me.NtsLabel1.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel1.Location = New System.Drawing.Point(12, 14)
    Me.NtsLabel1.Name = "NtsLabel1"
    Me.NtsLabel1.NTSDbField = ""
    Me.NtsLabel1.Size = New System.Drawing.Size(75, 13)
    Me.NtsLabel1.TabIndex = 0
    Me.NtsLabel1.Text = "Codice Mastro"
    Me.NtsLabel1.Tooltip = ""
    Me.NtsLabel1.UseMnemonic = False
    '
    'NtsLabel2
    '
    Me.NtsLabel2.AutoSize = True
    Me.NtsLabel2.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel2.Location = New System.Drawing.Point(12, 37)
    Me.NtsLabel2.Name = "NtsLabel2"
    Me.NtsLabel2.NTSDbField = ""
    Me.NtsLabel2.Size = New System.Drawing.Size(71, 13)
    Me.NtsLabel2.TabIndex = 1
    Me.NtsLabel2.Text = "Codice Conto"
    Me.NtsLabel2.Tooltip = ""
    Me.NtsLabel2.UseMnemonic = False
    '
    'NtsPanel1
    '
    Me.NtsPanel1.AllowDrop = True
    Me.NtsPanel1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsPanel1.Appearance.Options.UseBackColor = True
    Me.NtsPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel1.Controls.Add(Me.lbMastro)
    Me.NtsPanel1.Controls.Add(Me.edConto)
    Me.NtsPanel1.Controls.Add(Me.edMastro)
    Me.NtsPanel1.Controls.Add(Me.NtsLabel1)
    Me.NtsPanel1.Controls.Add(Me.NtsLabel2)
    Me.NtsPanel1.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsPanel1.Location = New System.Drawing.Point(1, 3)
    Me.NtsPanel1.Name = "NtsPanel1"
    Me.NtsPanel1.Size = New System.Drawing.Size(331, 72)
    Me.NtsPanel1.TabIndex = 2
    '
    'lbMastro
    '
    Me.lbMastro.BackColor = System.Drawing.Color.Transparent
    Me.lbMastro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbMastro.Location = New System.Drawing.Point(163, 11)
    Me.lbMastro.Name = "lbMastro"
    Me.lbMastro.NTSDbField = ""
    Me.lbMastro.Size = New System.Drawing.Size(165, 21)
    Me.lbMastro.TabIndex = 4
    Me.lbMastro.Tooltip = ""
    Me.lbMastro.UseMnemonic = False
    '
    'edConto
    '
    Me.edConto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edConto.EditValue = "0"
    Me.edConto.Location = New System.Drawing.Point(92, 34)
    Me.edConto.Name = "edConto"
    Me.edConto.NTSDbField = ""
    Me.edConto.NTSFormat = "0"
    Me.edConto.NTSForzaVisZoom = False
    Me.edConto.NTSOldValue = ""
    Me.edConto.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edConto.Properties.Appearance.Options.UseBackColor = True
    Me.edConto.Properties.Appearance.Options.UseTextOptions = True
    Me.edConto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edConto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edConto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edConto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edConto.Properties.MaxLength = 65536
    Me.edConto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edConto.Size = New System.Drawing.Size(65, 20)
    Me.edConto.TabIndex = 3
    '
    'edMastro
    '
    Me.edMastro.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMastro.EditValue = "0"
    Me.edMastro.Location = New System.Drawing.Point(92, 12)
    Me.edMastro.Name = "edMastro"
    Me.edMastro.NTSDbField = ""
    Me.edMastro.NTSFormat = "0"
    Me.edMastro.NTSForzaVisZoom = False
    Me.edMastro.NTSOldValue = ""
    Me.edMastro.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edMastro.Properties.Appearance.Options.UseBackColor = True
    Me.edMastro.Properties.Appearance.Options.UseTextOptions = True
    Me.edMastro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMastro.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMastro.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMastro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMastro.Properties.MaxLength = 65536
    Me.edMastro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMastro.Size = New System.Drawing.Size(65, 20)
    Me.edMastro.TabIndex = 2
    '
    'cmdConferma
    '
    Me.cmdConferma.ImageText = ""
    Me.cmdConferma.Location = New System.Drawing.Point(336, 5)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.Size = New System.Drawing.Size(79, 24)
    Me.cmdConferma.TabIndex = 3
    Me.cmdConferma.Text = "&Conferma"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(336, 31)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(79, 24)
    Me.cmdAnnulla.TabIndex = 5
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'FRM__HLNA
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(425, 82)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdConferma)
    Me.Controls.Add(Me.NtsPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
    Me.MinimizeBox = False
    Me.Name = "FRM__HLNA"
    Me.Text = "NUOVO CLIENTE/FORNITORIE"
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel1.ResumeLayout(False)
    Me.NtsPanel1.PerformLayout()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edMastro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitEntity(ByRef cleHlan As CLE__HLAN)
    oCleHlan = cleHlan
    AddHandler oCleHlan.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'completo le informazioni dei i controlli
      edMastro.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023461955970, "Codice mastro contabile"), CLN__STD.tabmast)
      edConto.NTSSetParam(oMenu, oApp.Tr(Me, 128230023462112143, "Codice conto"), "0", 5, 0, 99999)

      '-------------------------------------------------
      'chiamo lo script per inizializzare i controlli caricati con source ext
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      oCallParams.bPar1 = False
      oCallParams.dPar1 = 0
      oCallParams.dPar3 = 0
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    Dim lConto As Integer
    Try
      If NTSCInt(edMastro.Text) = 0 Or NTSCInt(edConto.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222143906250, "Il codice del mastro contabile ed il codice conto devono essere diversi da 0"))
        Return
      End If

      '---------------------------------------------------
      'compongo il codice
      lConto = NTSCInt(edMastro.Text & edConto.Text.PadLeft(nMaxLenConto, CType("0", Char)))

      '---------------------------------------------------
      'verifico che non esista gi un cliente/fornitore con le stesse caratteristiche
      If oCleHlan.EsitecontoCF(lConto, strTipoConto) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222144062500, "Codice cliente/fornitore già utilizzato"))
        Return
      End If

      '---------------------------------------------------
      'se tutto ok ritorno il codice del conto
      oCallParams.bPar1 = True
      oCallParams.dPar1 = lConto                  'codice del nuovo cliente/fornitore
      oCallParams.dPar3 = NTSCInt(edMastro.Text)  'mastro del nuovo cliente/fornitore

      '---------------------------------------------------
      'passo al chiamante se deve aggiornare tabnuma oppure no
      If strRicodificaCF = "S" And lProgrProposto = NTSCInt(edConto.Text) Then
        oCallParams.bPar2 = True
      Else
        oCallParams.bPar2 = False
      End If
      Me.Close()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdZoom_Click()
    Dim oParam As New CLE__PATB
    Try
      '----------------------------------------------
      'zoom specifico per mastri di contabilità
      If edMastro.ContainsFocus Then
      oParam.strCodPdc = strCodPcon       'passo il piano dei conti
        SetFastZoom(edMastro.Text, oParam)   'gestione dello zoom veloce
        NTSZOOM.strIn = edMastro.Text
        NTSZOOM.ZoomStrIn("ZOOMTABMAST", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edMastro.Text Then edMastro.Text = NTSZOOM.strIn
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

  Public Overridable Sub FRM__HLNA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      GctlSetRoules()
      GctlApplicaDefaultValue()

      '-------------------------------------------------
      'determino se il preogressivo del conto deve essere attribuito leggendo
      'dalle numerazioni o prendere lo stesso numero del codice di anagrafica generale
      'e determino come deve essere costruito il conto CF
      oCleHlan.InitHlna(strCodPcon, strRicodificaCF, nMaxLenConto)
      edConto.NTSSetParam(oMenu, oApp.Tr(Me, 128230023462268316, "Codice conto"), "0", nMaxLenConto, 0, NTSCDec("".PadLeft(nMaxLenConto, CType("9", Char))))
      edMastro.NTSForzaVisZoom = True

      '-------------------------------------------------
      'devo cercare il primo mastro del tipo cliente/fornitore da proporre 
      edMastro.Text = "0"
      edMastro.Text = oCleHlan.TrovaPrimoMastroCF(strCodPcon, strTipoConto).ToString
      If NTSCInt(edMastro.Text) <> 0 Then edMastro_Leave(Me, Nothing)

      '-------------------------------------------------
      'ora che ho il mastro devo proporre il progressivo 
      edConto.Text = "0"
      If strRicodificaCF = "S" Then
        edConto.Text = oCleHlan.TrovaProgressivoCF(strTipoConto, edMastro.Text).ToString
      Else
        If Len(lcodAnagen.ToString) > nMaxLenConto Then
          oApp.MsgBoxErr(oApp.Tr(Me, 127791222144375000, "Il conto selezionato ha un numero di cifre superiore" & vbCrLf & _
                "a quello consentito per il Piano dei Conti corrente." & vbCrLf & _
                "Pertanto sarà preso solo il numero di cifre utile."))
          edConto.Text = lcodAnagen.ToString.Substring(Len(lcodAnagen.ToString) - nMaxLenConto)
        Else
          edConto.Text = lcodAnagen.ToString
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edMastro_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edMastro.Leave
    Try
      If oCleHlan.TestMastro(NTSCInt(edMastro.Text), lbMastro.Text, strCodPcon, strTipoConto) = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222144531250, "Codice Mastro contabile non corretto o non di tipo Cliente/fornitore compatibile con quello richiesto"))
        edMastro.Text = "0"
      End If
      If strRicodificaCF = "S" And NTSCInt(edMastro.Text) <> 0 Then
        edConto.Text = oCleHlan.TrovaProgressivoCF(strTipoConto, edMastro.Text).ToString
        lProgrProposto = NTSCInt(edConto.Text)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edMastro_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles edMastro.NTSZoomGest
    Dim oCZoo As New CLE__CZOO
    Dim bNuovo As Boolean = True
    Dim oTmp As New Control
    Dim oPar As New CLE__CLDP
    Try
      Me.ValidaLastControl()
      If e.TipoEvento = "OPEN" Then bNuovo = False
      oTmp.Text = edMastro.Text

      oPar.strPar1 = strCodPcon     'passo il piano dei conti
      NTSZOOM.OpenChildGest(oTmp, "ZOOMTABMAST", DittaCorrente, bNuovo, oPar)

      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo
      edMastro.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__HLNA_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Try
      If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
        cmdZoom_Click()
        e.Handled = True
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
End Class
