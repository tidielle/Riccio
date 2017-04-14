#Region "Imports"
Imports System.Data
Imports NTSInformatica.CLN__STD
#End Region

Public Class FRMMGCOAE

#Region "Moduli"
  Private Moduli_P As Integer = bsModMG
  Private ModuliExt_P As Integer = 0
  Private ModuliSup_P As Integer = 0
  Private ModuliSupExt_P As Integer = 0
  Private ModuliPtn_P As Integer = 0
  Private ModuliPtnExt_P As Integer = 0

  Public ReadOnly Property Moduli() As Integer
    Get
      Return Moduli_P
    End Get
  End Property
  Public ReadOnly Property ModuliExt() As Integer
    Get
      Return ModuliExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliSup() As Integer
    Get
      Return ModuliSup_P
    End Get
  End Property
  Public ReadOnly Property ModuliSupExt() As Integer
    Get
      Return ModuliSupExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtn() As Integer
    Get
      Return ModuliPtn_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtnExt() As Integer
    Get
      Return ModuliPtnExt_P
    End Get
  End Property
#End Region

#Region "Variabili"
  Public oCallParam As CLE__CLDP       'parametri passati dal child che mi ha chiamato: sempre tramite questa classe gli restituisceo il valore
  Public oCleCoae As CLEMGCTFO
  Public strTabella As String = "ARTEST"

  Private components As System.ComponentModel.IContainer
  Public WithEvents lbAe_codartf As NTSInformatica.NTSLabel
  Public WithEvents edAe_codartf As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAe_forn As NTSInformatica.NTSLabel
  Public WithEvents edAe_forn As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAe_codmarc As NTSInformatica.NTSLabel
  Public WithEvents edAe_codmarc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDescrForn As NTSInformatica.NTSLabel
  Public WithEvents lbDescrMarc As NTSInformatica.NTSLabel
  Public WithEvents pnCoae1 As NTSInformatica.NTSPanel
#End Region

  Public Overridable Sub InitializeComponent()
    Me.lbAe_codartf = New NTSInformatica.NTSLabel
    Me.edAe_codartf = New NTSInformatica.NTSTextBoxStr
    Me.lbAe_forn = New NTSInformatica.NTSLabel
    Me.edAe_forn = New NTSInformatica.NTSTextBoxNum
    Me.lbAe_codmarc = New NTSInformatica.NTSLabel
    Me.edAe_codmarc = New NTSInformatica.NTSTextBoxNum
    Me.lbDescrForn = New NTSInformatica.NTSLabel
    Me.lbDescrMarc = New NTSInformatica.NTSLabel
    Me.pnCoae1 = New NTSInformatica.NTSPanel
    Me.cmdZoomMultiSelezione = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.pnCoae2 = New NTSInformatica.NTSPanel
    CType(Me.edAe_codartf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_forn.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAe_codmarc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnCoae1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCoae1.SuspendLayout()
    CType(Me.pnCoae2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCoae2.SuspendLayout()
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
    'lbAe_codartf
    '
    Me.lbAe_codartf.AutoSize = True
    Me.lbAe_codartf.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_codartf.Location = New System.Drawing.Point(12, 38)
    Me.lbAe_codartf.Name = "lbAe_codartf"
    Me.lbAe_codartf.NTSDbField = ""
    Me.lbAe_codartf.Size = New System.Drawing.Size(78, 13)
    Me.lbAe_codartf.TabIndex = 3
    Me.lbAe_codartf.Text = "Cod. art. forn:"
    '
    'edAe_codartf
    '
    Me.edAe_codartf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_codartf.Location = New System.Drawing.Point(96, 35)
    Me.edAe_codartf.Name = "edAe_codartf"
    Me.edAe_codartf.NTSDbField = ""
    Me.edAe_codartf.NTSForzaVisZoom = False
    Me.edAe_codartf.NTSOldValue = ""
    Me.edAe_codartf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_codartf.Properties.MaxLength = 65536
    Me.edAe_codartf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_codartf.Size = New System.Drawing.Size(373, 20)
    Me.edAe_codartf.TabIndex = 4
    '
    'lbAe_forn
    '
    Me.lbAe_forn.AutoSize = True
    Me.lbAe_forn.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_forn.Location = New System.Drawing.Point(12, 12)
    Me.lbAe_forn.Name = "lbAe_forn"
    Me.lbAe_forn.NTSDbField = ""
    Me.lbAe_forn.Size = New System.Drawing.Size(51, 13)
    Me.lbAe_forn.TabIndex = 0
    Me.lbAe_forn.Text = "Fornitore"
    '
    'edAe_forn
    '
    Me.edAe_forn.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_forn.EditValue = "0"
    Me.edAe_forn.Location = New System.Drawing.Point(96, 9)
    Me.edAe_forn.Name = "edAe_forn"
    Me.edAe_forn.NTSDbField = ""
    Me.edAe_forn.NTSFormat = "0"
    Me.edAe_forn.NTSForzaVisZoom = False
    Me.edAe_forn.NTSOldValue = ""
    Me.edAe_forn.Properties.Appearance.Options.UseTextOptions = True
    Me.edAe_forn.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAe_forn.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_forn.Properties.MaxLength = 65536
    Me.edAe_forn.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_forn.Size = New System.Drawing.Size(100, 20)
    Me.edAe_forn.TabIndex = 1
    '
    'lbAe_codmarc
    '
    Me.lbAe_codmarc.AutoSize = True
    Me.lbAe_codmarc.BackColor = System.Drawing.Color.Transparent
    Me.lbAe_codmarc.Location = New System.Drawing.Point(12, 64)
    Me.lbAe_codmarc.Name = "lbAe_codmarc"
    Me.lbAe_codmarc.NTSDbField = ""
    Me.lbAe_codmarc.Size = New System.Drawing.Size(36, 13)
    Me.lbAe_codmarc.TabIndex = 5
    Me.lbAe_codmarc.Text = "Marca"
    '
    'edAe_codmarc
    '
    Me.edAe_codmarc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAe_codmarc.EditValue = "0"
    Me.edAe_codmarc.Location = New System.Drawing.Point(96, 61)
    Me.edAe_codmarc.Name = "edAe_codmarc"
    Me.edAe_codmarc.NTSDbField = ""
    Me.edAe_codmarc.NTSFormat = "0"
    Me.edAe_codmarc.NTSForzaVisZoom = False
    Me.edAe_codmarc.NTSOldValue = ""
    Me.edAe_codmarc.Properties.Appearance.Options.UseTextOptions = True
    Me.edAe_codmarc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAe_codmarc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAe_codmarc.Properties.MaxLength = 65536
    Me.edAe_codmarc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAe_codmarc.Size = New System.Drawing.Size(68, 20)
    Me.edAe_codmarc.TabIndex = 6
    '
    'lbDescrForn
    '
    Me.lbDescrForn.BackColor = System.Drawing.Color.Transparent
    Me.lbDescrForn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescrForn.Location = New System.Drawing.Point(202, 9)
    Me.lbDescrForn.Name = "lbDescrForn"
    Me.lbDescrForn.NTSDbField = ""
    Me.lbDescrForn.Size = New System.Drawing.Size(267, 20)
    Me.lbDescrForn.TabIndex = 2
    '
    'lbDescrMarc
    '
    Me.lbDescrMarc.BackColor = System.Drawing.Color.Transparent
    Me.lbDescrMarc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescrMarc.Location = New System.Drawing.Point(170, 61)
    Me.lbDescrMarc.Name = "lbDescrMarc"
    Me.lbDescrMarc.NTSDbField = ""
    Me.lbDescrMarc.Size = New System.Drawing.Size(299, 20)
    Me.lbDescrMarc.TabIndex = 7
    '
    'pnCoae1
    '
    Me.pnCoae1.AllowDrop = True
    Me.pnCoae1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCoae1.Appearance.Options.UseBackColor = True
    Me.pnCoae1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCoae1.Controls.Add(Me.lbAe_codartf)
    Me.pnCoae1.Controls.Add(Me.lbDescrMarc)
    Me.pnCoae1.Controls.Add(Me.edAe_codartf)
    Me.pnCoae1.Controls.Add(Me.lbAe_forn)
    Me.pnCoae1.Controls.Add(Me.edAe_forn)
    Me.pnCoae1.Controls.Add(Me.lbDescrForn)
    Me.pnCoae1.Controls.Add(Me.lbAe_codmarc)
    Me.pnCoae1.Controls.Add(Me.edAe_codmarc)
    Me.pnCoae1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCoae1.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnCoae1.Location = New System.Drawing.Point(0, 0)
    Me.pnCoae1.Name = "pnCoae1"
    Me.pnCoae1.Size = New System.Drawing.Size(477, 93)
    Me.pnCoae1.TabIndex = 0
    Me.pnCoae1.Text = "NtsPanel1"
    '
    'cmdZoomMultiSelezione
    '
    Me.cmdZoomMultiSelezione.Location = New System.Drawing.Point(9, 61)
    Me.cmdZoomMultiSelezione.Name = "cmdZoomMultiSelezione"
    Me.cmdZoomMultiSelezione.Size = New System.Drawing.Size(118, 23)
    Me.cmdZoomMultiSelezione.TabIndex = 10
    Me.cmdZoomMultiSelezione.Text = "Zoom &multiselezione"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Location = New System.Drawing.Point(9, 35)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(118, 23)
    Me.cmdAnnulla.TabIndex = 9
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdConferma
    '
    Me.cmdConferma.Location = New System.Drawing.Point(9, 9)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.Size = New System.Drawing.Size(118, 23)
    Me.cmdConferma.TabIndex = 8
    Me.cmdConferma.Text = "&Conferma"
    '
    'pnCoae2
    '
    Me.pnCoae2.AllowDrop = True
    Me.pnCoae2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCoae2.Appearance.Options.UseBackColor = True
    Me.pnCoae2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCoae2.Controls.Add(Me.cmdZoomMultiSelezione)
    Me.pnCoae2.Controls.Add(Me.cmdConferma)
    Me.pnCoae2.Controls.Add(Me.cmdAnnulla)
    Me.pnCoae2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCoae2.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnCoae2.Location = New System.Drawing.Point(475, 0)
    Me.pnCoae2.Name = "pnCoae2"
    Me.pnCoae2.Size = New System.Drawing.Size(139, 93)
    Me.pnCoae2.TabIndex = 1
    Me.pnCoae2.Text = "NtsPanel1"
    '
    'FRMMGCOAE
    '
    Me.ClientSize = New System.Drawing.Size(614, 93)
    Me.Controls.Add(Me.pnCoae2)
    Me.Controls.Add(Me.pnCoae1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.Name = "FRMMGCOAE"
    Me.Text = "APRI SCHEDA ARTICOLO"
    CType(Me.edAe_codartf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_forn.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAe_codmarc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnCoae1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCoae1.ResumeLayout(False)
    Me.pnCoae1.PerformLayout()
    CType(Me.pnCoae2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCoae2.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef CallParam As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    oMenu = Menu
    oApp = oMenu.App
    oCallParam = CallParam
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
      '-------------------------------------------------
      'completo le informazioni dei controlli
      edAe_codartf.NTSSetParam(oMenu, oApp.Tr(Me, 128683607699521584, "Codice Articolo del fornitore"), 30, True)
      edAe_forn.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128683607746552834, "Codice Fornitore"), tabanagraf)
      edAe_codmarc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128683607791084084, "Marca/marchio"), tabmarc)

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

  Public Overridable Sub FRMMGCOAE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      If oCallParam.bAddNew Then
        'nuovo
        Me.Text = oApp.Tr(Me, 128683838882177834, "NUOVA SCHEDA ARTICOLO")
        cmdZoomMultiSelezione.Visible = False
      Else
        'apri
        Me.Text = oApp.Tr(Me, 128683839159677834, "APRI SCHEDA ARTICOLO")
      End If
      oCallParam.bPar1 = False
      '-------------------------------------------------
      'applico le regole della gctl
      GctlSetRoules()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdZoomMultiSelezione_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdZoomMultiSelezione.Click
    Dim oParam As New CLE__PATB
    Try
      oMenu.RunZoomNet("NTSInformatica", "FRMMGHLAE", "", "BNMGHLAE", "", DittaCorrente, CObj(oParam))
      If Not oParam.oParam Is Nothing Then
        oCallParam.bPar1 = True
        oCallParam.ctlPar1 = oParam.oParam
      End If
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    Dim dttArtest As New DataTable
    Try
      If Not TestPreElabora() Then Return
      If oCleCoae.VerificaArticolo(dttArtest, edAe_forn.Text, edAe_codartf.Text, edAe_codmarc.Text) Then
        'se gia' esistente entra qui'
        If oCallParam.bAddNew Then
          'dobbiamo creare un nuovo articolo, ma esiste gia',
          ' allora avviso e ritorno alla form
          oApp.MsgBoxErr(oApp.Tr(Me, 128683848258896584, "Codice fornitore |" & edAe_forn.Text & "| Codice articolo |" & edAe_codartf.Text & "| Codice marca |" & edAe_codmarc.Text & "| già esistente."))
          Return
        End If
      Else
        'se non esiste arriva qua'
        If Not oCallParam.bAddNew Then
          'dobbiamo aprire un articolo ma quello selezionato non esiste,
          ' allora avviso e ritorno alla form
          oApp.MsgBoxErr(oApp.Tr(Me, 128683848357646584, "Codice fornitore |" & edAe_forn.Text & "| Codice articolo |" & edAe_codartf.Text & "| Codice marca |" & edAe_codmarc.Text & "| inesistente."))
          Return
        Else
          dttArtest.Rows.Add(dttArtest.NewRow)
          With dttArtest.Rows(0)
            !ae_forn = edAe_forn.Text
            !ae_codartf = edAe_codartf.Text
            !ae_codmarc = edAe_codmarc.Text
          End With
          dttArtest.AcceptChanges()
        End If
      End If
      oCallParam.ctlPar1 = dttArtest
      oCallParam.bPar1 = True
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function TestPreElabora() As Boolean
    Try
      If edAe_forn.Text = "0" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128683843998115334, "Inserire il codice fornitore."))
        Return False
      End If
      If Trim(edAe_codartf.Text) = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128683844358896584, "Inserire il codice articolo fornitore."))
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      '--- Se si è in inserimento di un nuovo articolo, controlla che non ci siano spazi
      '--- in fondo, nel caso chiede di proseguire eliminandoli
      '-----------------------------------------------------------------------------------------
      If oCallParam.bAddNew Then

        If RTrim(edAe_codartf.Text).Length <> edAe_codartf.Text.Length Then
          If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128683885964677834, "Attenzione!" & vbCrLf & _
            "Il codice articolo indicato contiene degli spazi alla fine." & vbCrLf & _
            "Eliminarli e proseguire con l'inserimento?")) = Windows.Forms.DialogResult.Yes Then

            edAe_codartf.Text = RTrim(edAe_codartf.Text)

          Else
            Return False
          End If
        End If

      End If
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub edAe_forn_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAe_forn.Validated
    Dim strTmp As String = ""
    Try
      If edAe_forn.Text <> "0" Then
        If Not oCleCoae.VerificaFornitore(edAe_forn.Text, strTmp) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128671660719172611, "Codice fornitore inesistente."))
          edAe_forn.Text = "0"
        End If
      End If
      lbDescrForn.Text = strTmp

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edAe_codmarc_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAe_codmarc.Validated
    Dim strTmp As String = ""
    Try
      If edAe_codmarc.Text <> "0" Then
        If Not oCleCoae.VerificaMarca(edAe_codmarc.Text, strTmp) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128671660959328861, "Codice marchio/marca inesistente."))
          edAe_codmarc.Text = "0"
        End If
      End If
      lbDescrMarc.Text = strTmp

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class

