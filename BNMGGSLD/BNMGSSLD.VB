Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGSSLD
  Public oCleGsld As CLEMGGSLD
  Public oCallParams As CLE__CLDP
  Public bAnnullato As Boolean = True

#Region "VARIABILI"
  Private components As System.ComponentModel.IContainer
  Public WithEvents pnPanel As NTSInformatica.NTSPanel
  Public WithEvents lbLd_magaz As NTSInformatica.NTSLabel
  Public WithEvents lbLd_codart As NTSInformatica.NTSLabel
  Public WithEvents edLd_magaz As NTSInformatica.NTSTextBoxNum
  Public WithEvents edLd_codart As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbXx_codart As NTSInformatica.NTSLabel
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

  Public Overridable Sub InitEntity(ByRef cleGsld As CLEMGGSLD)
    oCleGsld = cleGsld
    AddHandler oCleGsld.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub InitializeComponent()
    Me.pnPanel = New NTSInformatica.NTSPanel
    Me.lbXx_magaz = New NTSInformatica.NTSLabel
    Me.lbXx_codart = New NTSInformatica.NTSLabel
    Me.edLd_magaz = New NTSInformatica.NTSTextBoxNum
    Me.edLd_codart = New NTSInformatica.NTSTextBoxStr
    Me.lbLd_magaz = New NTSInformatica.NTSLabel
    Me.lbLd_codart = New NTSInformatica.NTSLabel
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    CType(Me.pnPanel, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPanel.SuspendLayout()
    CType(Me.edLd_magaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edLd_codart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.pnPanel.Controls.Add(Me.lbXx_codart)
    Me.pnPanel.Controls.Add(Me.edLd_magaz)
    Me.pnPanel.Controls.Add(Me.edLd_codart)
    Me.pnPanel.Controls.Add(Me.lbLd_magaz)
    Me.pnPanel.Controls.Add(Me.lbLd_codart)
    Me.pnPanel.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPanel.Location = New System.Drawing.Point(12, 12)
    Me.pnPanel.Name = "pnPanel"
    Me.pnPanel.Size = New System.Drawing.Size(542, 74)
    Me.pnPanel.TabIndex = 0
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
    '
    'edLd_magaz
    '
    Me.edLd_magaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edLd_magaz.Location = New System.Drawing.Point(75, 40)
    Me.edLd_magaz.Name = "edLd_magaz"
    Me.edLd_magaz.NTSDbField = ""
    Me.edLd_magaz.NTSFormat = "0"
    Me.edLd_magaz.NTSForzaVisZoom = False
    Me.edLd_magaz.NTSOldValue = ""
    Me.edLd_magaz.Properties.Appearance.Options.UseTextOptions = True
    Me.edLd_magaz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edLd_magaz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edLd_magaz.Properties.MaxLength = 65536
    Me.edLd_magaz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edLd_magaz.Size = New System.Drawing.Size(62, 20)
    Me.edLd_magaz.TabIndex = 3
    '
    'edLd_codart
    '
    Me.edLd_codart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edLd_codart.Location = New System.Drawing.Point(75, 15)
    Me.edLd_codart.Name = "edLd_codart"
    Me.edLd_codart.NTSDbField = ""
    Me.edLd_codart.NTSForzaVisZoom = False
    Me.edLd_codart.NTSOldValue = ""
    Me.edLd_codart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edLd_codart.Properties.MaxLength = 65536
    Me.edLd_codart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edLd_codart.Size = New System.Drawing.Size(166, 20)
    Me.edLd_codart.TabIndex = 2
    '
    'lbLd_magaz
    '
    Me.lbLd_magaz.AutoSize = True
    Me.lbLd_magaz.BackColor = System.Drawing.Color.Transparent
    Me.lbLd_magaz.Location = New System.Drawing.Point(12, 43)
    Me.lbLd_magaz.Name = "lbLd_magaz"
    Me.lbLd_magaz.NTSDbField = ""
    Me.lbLd_magaz.Size = New System.Drawing.Size(57, 13)
    Me.lbLd_magaz.TabIndex = 1
    Me.lbLd_magaz.Text = "Magazzino"
    '
    'lbLd_codart
    '
    Me.lbLd_codart.AutoSize = True
    Me.lbLd_codart.BackColor = System.Drawing.Color.Transparent
    Me.lbLd_codart.Location = New System.Drawing.Point(12, 18)
    Me.lbLd_codart.Name = "lbLd_codart"
    Me.lbLd_codart.NTSDbField = ""
    Me.lbLd_codart.Size = New System.Drawing.Size(43, 13)
    Me.lbLd_codart.TabIndex = 0
    Me.lbLd_codart.Text = "Articolo"
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
    'FRMMGSSLD
    '
    Me.ClientSize = New System.Drawing.Size(695, 105)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.Controls.Add(Me.pnPanel)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMMGSSLD"
    Me.Text = ""
    CType(Me.pnPanel, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPanel.ResumeLayout(False)
    Me.pnPanel.PerformLayout()
    CType(Me.edLd_magaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edLd_codart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      edLd_codart.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128565452733324222, "Articolo"), tabartico, True)
      edLd_magaz.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128565452836492246, "Magazzino"), tabmaga)

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
  Public Overridable Sub FRMMGSSLD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      edLd_magaz.Text = "0"

      GctlSetRoules()

      edLd_codart.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGSSLD_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
  'Public Overridable Sub edLd_magaz_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs)
  '  Try
  '    e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo
  '  Catch ex As Exception
  '    '-------------------------------------------------
  '    Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
  '    '-------------------------------------------------
  '  End Try
  'End Sub

  Public Overridable Sub edLd_codart_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edLd_codart.Validated
    Dim bEsiste As Boolean = False
    Try
      '----------------------------------------------------------------------------------
      edLd_codart.Text = edLd_codart.Text.ToUpper
      '----------------------------------------------------------------------------------
      If (edLd_codart.Text.Trim = "") Or (edLd_codart.Text.Trim = ".") Then
        lbXx_codart.Text = ""
        Return
      End If
      '----------------------------------------------------------------------------------
      oCleGsld.DesCodart(DittaCorrente, edLd_codart.Text, lbXx_codart.Text, bEsiste)
      If bEsiste = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128565454982453554, "Codice articolo inesistente."))
        edLd_codart.Text = ""
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

  Public Overridable Sub edLd_magaz_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edLd_magaz.Validated
    Dim bEsiste As Boolean = False
    Try
      oCleGsld.DesMagaz(DittaCorrente, NTSCInt(edLd_magaz.Text), lbXx_magaz.Text, bEsiste)
      If bEsiste = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128565458030158472, "Codice magazzino inesistente."))
        edLd_magaz.Text = "0"
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
    Dim strMsg As String = ""
    If (edLd_codart.Text.Trim = "") Or (edLd_codart.Text.Trim = ".") Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128565458759766566, "Indicare un codice articolo valido."))
      edLd_codart.Focus()
      Return
    End If
    If NTSCInt(edLd_magaz.Text) = 0 Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128565459240165872, "Indicare un magazzino valido."))
      edLd_magaz.Focus()
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