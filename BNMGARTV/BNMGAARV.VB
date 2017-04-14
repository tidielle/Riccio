Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGAARV

#Region "Variabili"
  Public oCleArtv As CLEMGARTV
  Public oCallParams As CLE__CLDP
  Private components As System.ComponentModel.IContainer

  Public bAarvAnnullato As Boolean

  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents edCodbar As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbCodbar As NTSInformatica.NTSLabel
  Public WithEvents edCodroot As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbCodroot As NTSInformatica.NTSLabel
  Public WithEvents pnAarv As NTSInformatica.NTSPanel

#End Region

  Public Overridable Sub InitializeComponent()
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.pnAarv = New NTSInformatica.NTSPanel
    Me.edCodbar = New NTSInformatica.NTSTextBoxStr
    Me.lbCodbar = New NTSInformatica.NTSLabel
    Me.edCodroot = New NTSInformatica.NTSTextBoxStr
    Me.lbCodroot = New NTSInformatica.NTSLabel
    CType(Me.pnAarv, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAarv.SuspendLayout()
    CType(Me.edCodbar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodroot.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.cmdAnnulla.Location = New System.Drawing.Point(337, 38)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(100, 26)
    Me.cmdAnnulla.TabIndex = 14
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(337, 6)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(100, 26)
    Me.cmdOk.TabIndex = 12
    Me.cmdOk.Text = "&Ok"
    '
    'pnAarv
    '
    Me.pnAarv.AllowDrop = True
    Me.pnAarv.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAarv.Appearance.Options.UseBackColor = True
    Me.pnAarv.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAarv.Controls.Add(Me.edCodbar)
    Me.pnAarv.Controls.Add(Me.lbCodbar)
    Me.pnAarv.Controls.Add(Me.edCodroot)
    Me.pnAarv.Controls.Add(Me.lbCodroot)
    Me.pnAarv.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnAarv.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnAarv.Location = New System.Drawing.Point(0, 0)
    Me.pnAarv.Name = "pnAarv"
    Me.pnAarv.Size = New System.Drawing.Size(331, 72)
    Me.pnAarv.TabIndex = 22
    Me.pnAarv.Text = "NtsPanel1"
    '
    'edCodbar
    '
    Me.edCodbar.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodbar.Location = New System.Drawing.Point(94, 41)
    Me.edCodbar.Name = "edCodbar"
    Me.edCodbar.NTSDbField = ""
    Me.edCodbar.NTSForzaVisZoom = False
    Me.edCodbar.NTSOldValue = ""
    Me.edCodbar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodbar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodbar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodbar.Properties.MaxLength = 65536
    Me.edCodbar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodbar.Size = New System.Drawing.Size(216, 20)
    Me.edCodbar.TabIndex = 3
    '
    'lbCodbar
    '
    Me.lbCodbar.AutoSize = True
    Me.lbCodbar.BackColor = System.Drawing.Color.Transparent
    Me.lbCodbar.Location = New System.Drawing.Point(11, 44)
    Me.lbCodbar.Name = "lbCodbar"
    Me.lbCodbar.NTSDbField = ""
    Me.lbCodbar.Size = New System.Drawing.Size(77, 13)
    Me.lbCodbar.TabIndex = 2
    Me.lbCodbar.Text = "Codice a barre"
    Me.lbCodbar.Tooltip = ""
    Me.lbCodbar.UseMnemonic = False
    '
    'edCodroot
    '
    Me.edCodroot.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodroot.Location = New System.Drawing.Point(94, 12)
    Me.edCodroot.Name = "edCodroot"
    Me.edCodroot.NTSDbField = ""
    Me.edCodroot.NTSForzaVisZoom = False
    Me.edCodroot.NTSOldValue = ""
    Me.edCodroot.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodroot.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodroot.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodroot.Properties.MaxLength = 65536
    Me.edCodroot.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodroot.Size = New System.Drawing.Size(216, 20)
    Me.edCodroot.TabIndex = 1
    '
    'lbCodroot
    '
    Me.lbCodroot.AutoSize = True
    Me.lbCodroot.BackColor = System.Drawing.Color.Transparent
    Me.lbCodroot.Location = New System.Drawing.Point(11, 15)
    Me.lbCodroot.Name = "lbCodroot"
    Me.lbCodroot.NTSDbField = ""
    Me.lbCodroot.Size = New System.Drawing.Size(62, 13)
    Me.lbCodroot.TabIndex = 0
    Me.lbCodroot.Text = "Codice root"
    Me.lbCodroot.Tooltip = ""
    Me.lbCodroot.UseMnemonic = False
    '
    'FRMMGAARV
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(447, 72)
    Me.Controls.Add(Me.pnAarv)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.Name = "FRMMGAARV"
    Me.Text = "APRI SCHEDA ARTICOLO"
    CType(Me.pnAarv, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAarv.ResumeLayout(False)
    Me.pnAarv.PerformLayout()
    CType(Me.edCodbar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodroot.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      edCodbar.NTSSetParam(oMenu, oApp.Tr(Me, 128553203421564263, "Codice a barre"), 255, True)
      edCodroot.NTSSetParam(oMenu, oApp.Tr(Me, 128553203421875805, "Codice root"), CLN__STD.CodartMaxLen, True)

      edCodroot.NTSSetParamZoom("ZOOMARTICO")

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
  Public Overridable Sub InitEntity(ByRef cleArtv As CLEMGARTV)
    oCleArtv = cleArtv
    AddHandler oCleArtv.RemoteEvent, AddressOf GestisciEventiEntity
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

    '------------------------------
    'collego il datatable agli altri oggetti di form

    Return True
  End Function

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Dim strMsg As String
    Try
      '-----------------------------------------------------------------------------------------
      '--- Controlla la validità dei dati
      '-----------------------------------------------------------------------------------------
      If edCodroot.Text = "" And edCodbar.Text = "" Then
        strMsg = "Indicare il codice root o il codice a barre."
        oApp.MsgBoxErr(oApp.Tr(Me, 128802030468521441, strMsg))
        edCodroot.Text = ""
        edCodbar.Text = ""
        Exit Sub
      End If
      If edCodroot.Text <> "" And edCodbar.Text <> "" Then
        strMsg = "Non possono essere indicati contemporaneamente codice root e codice a barre." & vbCrLf & _
          "Ripetere l'inserimento."
        oApp.MsgBoxErr(oApp.Tr(Me, 128553132286662259, strMsg))
        edCodroot.Text = ""
        edCodbar.Text = ""
        Exit Sub
      End If

      If Not AarvCheckCodroot() Then
        Exit Sub
      Else
        oCleArtv.strArtvCodroot = UCase(edCodroot.Text)
      End If
      If Not AarvCheckCodbar() Then Exit Sub
      If Not ArticoloTC() Then Exit Sub

      bAarvAnnullato = False
      Me.Close()

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    bAarvAnnullato = True
    Me.Close()
  End Sub

  Public Overridable Sub FRMMGAARV_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
    Try
      If edCodroot.Focused And e.KeyCode = Keys.Return Then
        If Not Me.frmAuto.bSelezionato Then
          cmdOk.Focus()
          cmdOk_Click(sender, e)
        Else
          Me.frmAuto.bSelezionato = False
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGAARV_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

  Public Overridable Sub FRMMGAARV_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      bAarvAnnullato = True

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      Me.Cursor = Cursors.Default

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub Zoom()
    Dim strCodart As String = ""
    Dim strCodroot As String = ""
    Try
      'per eventuali altri controlli caricati al volo
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      If edCodroot Is NTSLastControlFocussed() Or edCodroot.Focused Then
        SetFastZoom(edCodroot.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edCodroot.Text
        oParam.bVisGriglia = True
        oParam.strTipoArticolo = "N"
        oParam.strCodPdc = "V"
        NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edCodroot.Text Then strCodart = NTSZOOM.strIn

        If strCodart <> "" Then
          If Not oCleArtv.AarvCheckVarianti(strCodart, strCodroot) Then Exit Sub
          edCodroot.NTSTextDB = strCodroot
          If oCleArtv.bSelCodiceNoApri = False Then cmdOk_Click(Me, Nothing)
          If bAarvAnnullato = True Then Exit Sub
        Else
          Exit Sub
        End If

        bAarvAnnullato = False
        Me.Close()
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

  Public Overridable Function AarvCheckCodroot() As Boolean
    Try
      Return oCleArtv.AarvCheckCodroot(edCodroot.Text)

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function AarvCheckCodbar() As Boolean
    Try
      Return oCleArtv.AarvCheckCodbar(edCodbar.Text)

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function ArticoloTC() As Boolean
    Try
      Return oCleArtv.CheckArticoloTC()

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

End Class