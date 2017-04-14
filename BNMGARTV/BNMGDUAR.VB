Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGDUAR
  Public oCallParams As CLE__CLDP
  Public oCleArtv As CLEMGARTV
  Public bOk As Boolean = False
  Public bNew As Boolean = False

  Public lProgr As Integer
  'Public nLungRoot As Integer
  'Public nLungExt As Integer
  'Public strGeneraCodice As String
  Public gbDuarArticoloGenerato As Boolean
  Public glDuarProgr As Integer
  Public gstrDuarRoot As String

  Public WithEvents pnNuovo As NTSInformatica.NTSPanel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents lbCodart As NTSInformatica.NTSLabel
  Public WithEvents ckProvvigioni As NTSInformatica.NTSCheckBox
  Public WithEvents ckSconti As NTSInformatica.NTSCheckBox
  Public WithEvents ckListini As NTSInformatica.NTSCheckBox
  Public WithEvents fmCheck As NTSInformatica.NTSGroupBox
  Public WithEvents ckArtfasi As NTSInformatica.NTSCheckBox
  Public WithEvents edCodart As NTSInformatica.NTSTextBoxStr
  Public WithEvents cmdOk As NTSInformatica.NTSButton

  Public Overridable Sub InitializeComponent()
    Me.pnNuovo = New NTSInformatica.NTSPanel
    Me.edCodart = New NTSInformatica.NTSTextBoxStr
    Me.fmCheck = New NTSInformatica.NTSGroupBox
    Me.ckArtfasi = New NTSInformatica.NTSCheckBox
    Me.ckListini = New NTSInformatica.NTSCheckBox
    Me.ckSconti = New NTSInformatica.NTSCheckBox
    Me.ckProvvigioni = New NTSInformatica.NTSCheckBox
    Me.lbCodart = New NTSInformatica.NTSLabel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.NtsPanel1 = New NTSInformatica.NTSPanel
    CType(Me.pnNuovo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnNuovo.SuspendLayout()
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmCheck, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmCheck.SuspendLayout()
    CType(Me.ckArtfasi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckListini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSconti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckProvvigioni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel1.SuspendLayout()
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
    'pnNuovo
    '
    Me.pnNuovo.AllowDrop = True
    Me.pnNuovo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnNuovo.Appearance.Options.UseBackColor = True
    Me.pnNuovo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnNuovo.Controls.Add(Me.edCodart)
    Me.pnNuovo.Controls.Add(Me.fmCheck)
    Me.pnNuovo.Controls.Add(Me.lbCodart)
    Me.pnNuovo.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnNuovo.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnNuovo.Location = New System.Drawing.Point(0, 0)
    Me.pnNuovo.Name = "pnNuovo"
    Me.pnNuovo.NTSActiveTrasparency = True
    Me.pnNuovo.Size = New System.Drawing.Size(296, 175)
    Me.pnNuovo.TabIndex = 12
    Me.pnNuovo.Text = "NtsPanel1"
    '
    'edCodart
    '
    Me.edCodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodart.Location = New System.Drawing.Point(119, 7)
    Me.edCodart.Name = "edCodart"
    Me.edCodart.NTSDbField = ""
    Me.edCodart.NTSForzaVisZoom = False
    Me.edCodart.NTSOldValue = ""
    Me.edCodart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodart.Properties.MaxLength = 65536
    Me.edCodart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodart.Size = New System.Drawing.Size(162, 20)
    Me.edCodart.TabIndex = 13
    '
    'fmCheck
    '
    Me.fmCheck.AllowDrop = True
    Me.fmCheck.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmCheck.Appearance.Options.UseBackColor = True
    Me.fmCheck.Controls.Add(Me.ckArtfasi)
    Me.fmCheck.Controls.Add(Me.ckListini)
    Me.fmCheck.Controls.Add(Me.ckSconti)
    Me.fmCheck.Controls.Add(Me.ckProvvigioni)
    Me.fmCheck.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmCheck.Location = New System.Drawing.Point(11, 36)
    Me.fmCheck.Name = "fmCheck"
    Me.fmCheck.Size = New System.Drawing.Size(210, 127)
    Me.fmCheck.TabIndex = 675
    '
    'ckArtfasi
    '
    Me.ckArtfasi.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckArtfasi.Location = New System.Drawing.Point(12, 98)
    Me.ckArtfasi.Name = "ckArtfasi"
    Me.ckArtfasi.NTSCheckValue = "S"
    Me.ckArtfasi.NTSUnCheckValue = "N"
    Me.ckArtfasi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckArtfasi.Properties.Appearance.Options.UseBackColor = True
    Me.ckArtfasi.Properties.Caption = "Duplica fa&si articolo"
    Me.ckArtfasi.Size = New System.Drawing.Size(114, 19)
    Me.ckArtfasi.TabIndex = 673
    '
    'ckListini
    '
    Me.ckListini.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckListini.Location = New System.Drawing.Point(12, 26)
    Me.ckListini.Name = "ckListini"
    Me.ckListini.NTSCheckValue = "S"
    Me.ckListini.NTSUnCheckValue = "N"
    Me.ckListini.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckListini.Properties.Appearance.Options.UseBackColor = True
    Me.ckListini.Properties.Caption = "Duplica listini"
    Me.ckListini.Size = New System.Drawing.Size(90, 19)
    Me.ckListini.TabIndex = 670
    '
    'ckSconti
    '
    Me.ckSconti.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSconti.Location = New System.Drawing.Point(12, 50)
    Me.ckSconti.Name = "ckSconti"
    Me.ckSconti.NTSCheckValue = "S"
    Me.ckSconti.NTSUnCheckValue = "N"
    Me.ckSconti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSconti.Properties.Appearance.Options.UseBackColor = True
    Me.ckSconti.Properties.Caption = "Duplica sconti"
    Me.ckSconti.Size = New System.Drawing.Size(90, 19)
    Me.ckSconti.TabIndex = 671
    '
    'ckProvvigioni
    '
    Me.ckProvvigioni.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckProvvigioni.Location = New System.Drawing.Point(12, 74)
    Me.ckProvvigioni.Name = "ckProvvigioni"
    Me.ckProvvigioni.NTSCheckValue = "S"
    Me.ckProvvigioni.NTSUnCheckValue = "N"
    Me.ckProvvigioni.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckProvvigioni.Properties.Appearance.Options.UseBackColor = True
    Me.ckProvvigioni.Properties.Caption = "Duplica provvigioni"
    Me.ckProvvigioni.Size = New System.Drawing.Size(114, 19)
    Me.ckProvvigioni.TabIndex = 672
    '
    'lbCodart
    '
    Me.lbCodart.AutoSize = True
    Me.lbCodart.BackColor = System.Drawing.Color.Transparent
    Me.lbCodart.Location = New System.Drawing.Point(3, 10)
    Me.lbCodart.Name = "lbCodart"
    Me.lbCodart.NTSDbField = ""
    Me.lbCodart.Size = New System.Drawing.Size(110, 13)
    Me.lbCodart.TabIndex = 582
    Me.lbCodart.Text = "Codice nuovo articolo"
    Me.lbCodart.Tooltip = ""
    Me.lbCodart.UseMnemonic = False
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(3, 31)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(105, 26)
    Me.cmdAnnulla.TabIndex = 10
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(3, 3)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(105, 26)
    Me.cmdOk.TabIndex = 11
    Me.cmdOk.Text = "&Conferma"
    '
    'NtsPanel1
    '
    Me.NtsPanel1.AllowDrop = True
    Me.NtsPanel1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsPanel1.Appearance.Options.UseBackColor = True
    Me.NtsPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel1.Controls.Add(Me.cmdOk)
    Me.NtsPanel1.Controls.Add(Me.cmdAnnulla)
    Me.NtsPanel1.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsPanel1.Dock = System.Windows.Forms.DockStyle.Right
    Me.NtsPanel1.Location = New System.Drawing.Point(308, 0)
    Me.NtsPanel1.Name = "NtsPanel1"
    Me.NtsPanel1.NTSActiveTrasparency = True
    Me.NtsPanel1.Size = New System.Drawing.Size(123, 175)
    Me.NtsPanel1.TabIndex = 13
    Me.NtsPanel1.Text = "NtsPanel1"
    '
    'FRMMGDUAR
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(431, 175)
    Me.Controls.Add(Me.NtsPanel1)
    Me.Controls.Add(Me.pnNuovo)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.HelpContext = ""
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMMGDUAR"
    Me.Text = "DUPLICAZIONE ARTICOLO"
    CType(Me.pnNuovo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnNuovo.ResumeLayout(False)
    Me.pnNuovo.PerformLayout()
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmCheck, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmCheck.ResumeLayout(False)
    Me.fmCheck.PerformLayout()
    CType(Me.ckArtfasi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckListini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSconti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckProvvigioni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel1.ResumeLayout(False)
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
      ckArtfasi.NTSSetParam(oMenu, oApp.Tr(Me, 128560093887272501, "Duplica fa&si articolo"), "S", "N")
      ckListini.NTSSetParam(oMenu, oApp.Tr(Me, 128560093887741071, "Duplica listini"), "S", "N")
      ckSconti.NTSSetParam(oMenu, oApp.Tr(Me, 128560093888053451, "Duplica sconti"), "S", "N")
      ckProvvigioni.NTSSetParam(oMenu, oApp.Tr(Me, 128560093888209641, "Duplica provvigioni"), "S", "N")
      edCodart.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128802043756293378, "Codice nuovo articolo"), tabartico, False)

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
  Public Overridable Sub FRMMGDUAR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      AddHandler oCleArtv.RemoteEvent, AddressOf GestisciEventiEntity

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      edCodart.Text = "."

      bOk = False

      '----------------------------------------------------
      oCleArtv.strGeneraCodice = oMenu.GetSettingBus("BSMGARTV", "OPZIONI", ".", "GeneraCodice", "0", " ", "0")
      oCleArtv.bDuarGeneraArticoli = CBool(oCleArtv.strGeneraCodice)
      oCleArtv.nLungRoot = NTSCInt(oMenu.GetSettingBus("BSMGARTV", "OPZIONI", ".", "GeneraCodiceLungRoot", "0", " ", "0"))
      oCleArtv.nLungExt = NTSCInt(oMenu.GetSettingBus("BSMGARTV", "OPZIONI", ".", "GeneraCodiceLungExt", "0", " ", "0"))
      gbDuarArticoloGenerato = False
      lProgr = 0
      glDuarProgr = 0
      gstrDuarRoot = ""

      '--------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
      GctlApplicaDefaultValue()

      If oCleArtv.bDuplicaListini = True Then ckListini.Checked = True Else ckListini.Checked = False
      If oCleArtv.bDuplicaSconti = True Then ckSconti.Checked = True Else ckSconti.Checked = False
      If oCleArtv.bDuplicaProvvigioni = True Then ckProvvigioni.Checked = True Else ckProvvigioni.Checked = False
      If oCleArtv.bDuplicaFasi = True Then ckArtfasi.Checked = True Else ckArtfasi.Checked = False

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMMGDUAR_ActivatedFirst(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ActivatedFirst
    Try
      '--------------------------------------------------------------------------------------------------------------
      edCodart.Focus()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMMGDUAR_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

#Region "Validazione articoli"
  Public Overridable Sub edCodart_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCodart.Validated
    Try
      If Not CheckCodartDuar() Then Exit Sub
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      edCodart.Text = UCase(edCodart.Text)
      If Not CheckCodartDuar() Then Exit Sub

      If Not oCleArtv.CheckArticoloDuar(edCodart.Text) Then
        Exit Sub
      Else
        oCleArtv.strCodartDuar = edCodart.Text
        bOk = True
        Me.Close()
      End If

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

  Public Overridable Function CheckCodartDuar() As Boolean
    Try
      Return oCleArtv.CheckCodartDuar(edCodart.Text)

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

End Class