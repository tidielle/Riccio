Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGSSLE
  Public oCleGsle As CLEMGGSLE
  Public oCallParams As CLE__CLDP
  Public bAnnullato As Boolean = True

#Region "VARIABILI"
  Private components As System.ComponentModel.IContainer
  Public WithEvents pnPanel As NTSInformatica.NTSPanel
  Public WithEvents lbLp_magaz As NTSInformatica.NTSLabel
  Public WithEvents lbLp_codart As NTSInformatica.NTSLabel
  Public WithEvents edLp_codart As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbXx_codart As NTSInformatica.NTSLabel
  Public WithEvents edLp_magaz As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_magaz As NTSInformatica.NTSLabel
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
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

  Public Overridable Sub InitEntity(ByRef cleGsle As CLEMGGSLE)
    oCleGsle = cleGsle
    AddHandler oCleGsle.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub InitializeComponent()
    Me.pnPanel = New NTSInformatica.NTSPanel
    Me.lbLp_codart = New NTSInformatica.NTSLabel
    Me.lbLp_magaz = New NTSInformatica.NTSLabel
    Me.edLp_codart = New NTSInformatica.NTSTextBoxStr
    Me.lbXx_codart = New NTSInformatica.NTSLabel
    Me.edLp_magaz = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_magaz = New NTSInformatica.NTSLabel
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    CType(Me.pnPanel, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPanel.SuspendLayout()
    CType(Me.edLp_codart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edLp_magaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'pnPanel
    '
    Me.pnPanel.AllowDrop = True
    Me.pnPanel.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPanel.Appearance.Options.UseBackColor = True
    Me.pnPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPanel.Controls.Add(Me.lbXx_magaz)
    Me.pnPanel.Controls.Add(Me.edLp_magaz)
    Me.pnPanel.Controls.Add(Me.lbXx_codart)
    Me.pnPanel.Controls.Add(Me.edLp_codart)
    Me.pnPanel.Controls.Add(Me.lbLp_magaz)
    Me.pnPanel.Controls.Add(Me.lbLp_codart)
    Me.pnPanel.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPanel.Location = New System.Drawing.Point(12, 12)
    Me.pnPanel.Name = "pnPanel"
    Me.pnPanel.Size = New System.Drawing.Size(542, 74)
    Me.pnPanel.TabIndex = 0
    Me.pnPanel.Text = "NtsPanel1"
    '
    'lbLp_codart
    '
    Me.lbLp_codart.AutoSize = True
    Me.lbLp_codart.BackColor = System.Drawing.Color.Transparent
    Me.lbLp_codart.Location = New System.Drawing.Point(12, 18)
    Me.lbLp_codart.Name = "lbLp_codart"
    Me.lbLp_codart.NTSDbField = ""
    Me.lbLp_codart.Size = New System.Drawing.Size(43, 13)
    Me.lbLp_codart.TabIndex = 0
    Me.lbLp_codart.Text = "Articolo"
    '
    'lbLp_magaz
    '
    Me.lbLp_magaz.AutoSize = True
    Me.lbLp_magaz.BackColor = System.Drawing.Color.Transparent
    Me.lbLp_magaz.Location = New System.Drawing.Point(12, 43)
    Me.lbLp_magaz.Name = "lbLp_magaz"
    Me.lbLp_magaz.NTSDbField = ""
    Me.lbLp_magaz.Size = New System.Drawing.Size(57, 13)
    Me.lbLp_magaz.TabIndex = 1
    Me.lbLp_magaz.Text = "Magazzino"
    '
    'edLp_codart
    '
    Me.edLp_codart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edLp_codart.Location = New System.Drawing.Point(75, 15)
    Me.edLp_codart.Name = "edLp_codart"
    Me.edLp_codart.NTSDbField = ""
    Me.edLp_codart.NTSForzaVisZoom = False
    Me.edLp_codart.NTSOldValue = ""
    Me.edLp_codart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edLp_codart.Properties.MaxLength = 65536
    Me.edLp_codart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edLp_codart.Size = New System.Drawing.Size(166, 20)
    Me.edLp_codart.TabIndex = 2
    '
    'lbXx_codart
    '
    Me.lbXx_codart.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codart.Location = New System.Drawing.Point(247, 15)
    Me.lbXx_codart.Name = "lbXx_codart"
    Me.lbXx_codart.NTSDbField = ""
    Me.lbXx_codart.Size = New System.Drawing.Size(277, 20)
    Me.lbXx_codart.TabIndex = 3
    '
    'edLp_magaz
    '
    Me.edLp_magaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edLp_magaz.Location = New System.Drawing.Point(75, 40)
    Me.edLp_magaz.Name = "edLp_magaz"
    Me.edLp_magaz.NTSDbField = ""
    Me.edLp_magaz.NTSFormat = "0"
    Me.edLp_magaz.NTSForzaVisZoom = False
    Me.edLp_magaz.NTSOldValue = ""
    Me.edLp_magaz.Properties.Appearance.Options.UseTextOptions = True
    Me.edLp_magaz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edLp_magaz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edLp_magaz.Properties.MaxLength = 65536
    Me.edLp_magaz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edLp_magaz.Size = New System.Drawing.Size(62, 20)
    Me.edLp_magaz.TabIndex = 4
    '
    'lbXx_magaz
    '
    Me.lbXx_magaz.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_magaz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_magaz.Location = New System.Drawing.Point(143, 40)
    Me.lbXx_magaz.Name = "lbXx_magaz"
    Me.lbXx_magaz.NTSDbField = ""
    Me.lbXx_magaz.Size = New System.Drawing.Size(381, 20)
    Me.lbXx_magaz.TabIndex = 5
    '
    'cmdOk
    '
    Me.cmdOk.Location = New System.Drawing.Point(560, 24)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(116, 23)
    Me.cmdOk.TabIndex = 1
    Me.cmdOk.Text = "Conferma"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Location = New System.Drawing.Point(560, 49)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(116, 23)
    Me.cmdAnnulla.TabIndex = 2
    Me.cmdAnnulla.Text = "Annulla"
    '
    'FRMMGSSLE
    '
    Me.ClientSize = New System.Drawing.Size(695, 105)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.Controls.Add(Me.pnPanel)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMMGSSLE"
    Me.Text = ""
    CType(Me.pnPanel, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPanel.ResumeLayout(False)
    Me.pnPanel.PerformLayout()
    CType(Me.edLp_codart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edLp_magaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      edLp_codart.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128566377065681696, "Articolo"), tabartico, True)
      edLp_magaz.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128566377131619618, "Magazzino"), tabmaga)

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

#Region "FORM"
  Public Overridable Sub FRMMGSSLE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      edLp_magaz.Text = "0"

      GctlSetRoules()

      edLp_codart.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGSSLE_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

#Region "CONTROLLI"
  Public Overridable Sub edLp_codart_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edLp_codart.Validated
    Dim bEsiste As Boolean = False
    Try
      '----------------------------------------------------------------------------------
      edLp_codart.Text = edLp_codart.Text.ToUpper
      '----------------------------------------------------------------------------------
      If (edLp_codart.Text.Trim = "") Or (edLp_codart.Text.Trim = ".") Then
        lbXx_codart.Text = ""
        Return
      End If
      '----------------------------------------------------------------------------------
      oCleGsle.DesCodart(DittaCorrente, edLp_codart.Text, lbXx_codart.Text, bEsiste)
      If bEsiste = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128566379065538245, "Codice articolo inesistente."))
        edLp_codart.Text = ""
        Return
      End If
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edLp_magaz_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edLp_magaz.Validated
    Dim bEsiste As Boolean = False
    Try
      oCleGsle.DesMagaz(DittaCorrente, NTSCInt(edLp_magaz.Text), lbXx_magaz.Text, bEsiste)
      If bEsiste = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128566379210226671, "Codice magazzino inesistente."))
        edLp_magaz.Text = "0"
        Return
      End If
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "PULSANTI"
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Me.Close()
  End Sub

  Public Overridable Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    If (edLp_codart.Text.Trim = "") Or (edLp_codart.Text.Trim = ".") Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128566379413977975, "Indicare un codice articolo valido."))
      edLp_codart.Focus()
      Return
    End If
    If NTSCInt(edLp_magaz.Text) = 0 Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128566379525228687, "Indicare un magazzino valido."))
      edLp_magaz.Focus()
      Return
    End If
    bAnnullato = False
    Me.Close()
  End Sub
#End Region

#Region "FUNZIONI"
  Public Overridable Sub Zoom()
    Dim ctlLastControl As Control

    Try
      ctlLastControl = NTSFindControlFocused(Me)
      If ctlLastControl Is Nothing Then Return
      '------------------------------------
      'zoom standard di textbox e griglia
      'SendKeys.SendWait("{F5}") 'se faccio questa riga va in un loop infinito....
      NTSCallStandardZoom()
      '------------------------------------
      'riporto il focus sul controllo che ha chiamato lo zoom
      ctlLastControl.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

End Class