#Region "Imports"
Imports System.Data
Imports NTSInformatica.CLN__STD
#End Region

Public Class FRMMGINPC

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

#Region "Variaribli"
  Public oCleElct As CLEMGELCT
  Public oCallParams As CLE__CLDP
  'Valore di ritorno valori possibili "SALTA", "TERMINA", "AGGIORNA" e "AUTOMATICA"
  Public strFormRetValue As String

  Private components As System.ComponentModel.IContainer
  Public WithEvents lbLabelCodArtF As NTSInformatica.NTSLabel
  Public WithEvents lbDesCodArtF As NTSInformatica.NTSLabel
  Public WithEvents lbLabelDesCodArtF As NTSInformatica.NTSLabel
  Public WithEvents lbCodArtF As NTSInformatica.NTSLabel
  Public WithEvents lbInfoBar As NTSInformatica.NTSLabel
  Public WithEvents pnInpc As NTSInformatica.NTSPanel
  Public WithEvents cmdTermina As NTSInformatica.NTSButton
  Public WithEvents cmdSalta As NTSInformatica.NTSButton
  Public WithEvents cmdAggiorna As NTSInformatica.NTSButton
  Public WithEvents ckElaborazioneAutomatica As NTSInformatica.NTSCheckBox
#End Region

  Public Overridable Sub InitializeComponent()
    Me.lbLabelCodArtF = New NTSInformatica.NTSLabel
    Me.lbDesCodArtF = New NTSInformatica.NTSLabel
    Me.lbLabelDesCodArtF = New NTSInformatica.NTSLabel
    Me.lbCodArtF = New NTSInformatica.NTSLabel
    Me.lbInfoBar = New NTSInformatica.NTSLabel
    Me.pnInpc = New NTSInformatica.NTSPanel
    Me.cmdTermina = New NTSInformatica.NTSButton
    Me.cmdSalta = New NTSInformatica.NTSButton
    Me.cmdAggiorna = New NTSInformatica.NTSButton
    Me.ckElaborazioneAutomatica = New NTSInformatica.NTSCheckBox
    CType(Me.pnInpc, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnInpc.SuspendLayout()
    CType(Me.ckElaborazioneAutomatica.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'lbLabelCodArtF
    '
    Me.lbLabelCodArtF.AutoSize = True
    Me.lbLabelCodArtF.BackColor = System.Drawing.Color.Transparent
    Me.lbLabelCodArtF.Location = New System.Drawing.Point(12, 41)
    Me.lbLabelCodArtF.Name = "lbLabelCodArtF"
    Me.lbLabelCodArtF.NTSDbField = ""
    Me.lbLabelCodArtF.Size = New System.Drawing.Size(126, 13)
    Me.lbLabelCodArtF.TabIndex = 4
    Me.lbLabelCodArtF.Text = "Codice articolo fornitore:"
    '
    'lbDesCodArtF
    '
    Me.lbDesCodArtF.BackColor = System.Drawing.Color.Transparent
    Me.lbDesCodArtF.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesCodArtF.Location = New System.Drawing.Point(144, 64)
    Me.lbDesCodArtF.Name = "lbDesCodArtF"
    Me.lbDesCodArtF.NTSDbField = ""
    Me.lbDesCodArtF.Size = New System.Drawing.Size(288, 20)
    Me.lbDesCodArtF.TabIndex = 6
    '
    'lbLabelDesCodArtF
    '
    Me.lbLabelDesCodArtF.AutoSize = True
    Me.lbLabelDesCodArtF.BackColor = System.Drawing.Color.Transparent
    Me.lbLabelDesCodArtF.Location = New System.Drawing.Point(73, 67)
    Me.lbLabelDesCodArtF.Name = "lbLabelDesCodArtF"
    Me.lbLabelDesCodArtF.NTSDbField = ""
    Me.lbLabelDesCodArtF.Size = New System.Drawing.Size(65, 13)
    Me.lbLabelDesCodArtF.TabIndex = 7
    Me.lbLabelDesCodArtF.Text = "Descrizione:"
    '
    'lbCodArtF
    '
    Me.lbCodArtF.BackColor = System.Drawing.Color.Transparent
    Me.lbCodArtF.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbCodArtF.Location = New System.Drawing.Point(144, 38)
    Me.lbCodArtF.Name = "lbCodArtF"
    Me.lbCodArtF.NTSDbField = ""
    Me.lbCodArtF.Size = New System.Drawing.Size(288, 20)
    Me.lbCodArtF.TabIndex = 9
    '
    'lbInfoBar
    '
    Me.lbInfoBar.AutoSize = True
    Me.lbInfoBar.BackColor = System.Drawing.Color.Transparent
    Me.lbInfoBar.Location = New System.Drawing.Point(12, 15)
    Me.lbInfoBar.Name = "lbInfoBar"
    Me.lbInfoBar.NTSDbField = ""
    Me.lbInfoBar.Size = New System.Drawing.Size(0, 13)
    Me.lbInfoBar.TabIndex = 10
    '
    'pnInpc
    '
    Me.pnInpc.AllowDrop = True
    Me.pnInpc.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnInpc.Appearance.Options.UseBackColor = True
    Me.pnInpc.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnInpc.Controls.Add(Me.ckElaborazioneAutomatica)
    Me.pnInpc.Controls.Add(Me.cmdTermina)
    Me.pnInpc.Controls.Add(Me.cmdSalta)
    Me.pnInpc.Controls.Add(Me.cmdAggiorna)
    Me.pnInpc.Controls.Add(Me.lbLabelCodArtF)
    Me.pnInpc.Controls.Add(Me.lbDesCodArtF)
    Me.pnInpc.Controls.Add(Me.lbLabelDesCodArtF)
    Me.pnInpc.Controls.Add(Me.lbInfoBar)
    Me.pnInpc.Controls.Add(Me.lbCodArtF)
    Me.pnInpc.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnInpc.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnInpc.Location = New System.Drawing.Point(0, 0)
    Me.pnInpc.Name = "pnInpc"
    Me.pnInpc.Size = New System.Drawing.Size(444, 152)
    Me.pnInpc.TabIndex = 14
    Me.pnInpc.Text = "NtsPanel1"
    '
    'cmdTermina
    '
    Me.cmdTermina.Location = New System.Drawing.Point(311, 97)
    Me.cmdTermina.Name = "cmdTermina"
    Me.cmdTermina.Size = New System.Drawing.Size(121, 23)
    Me.cmdTermina.TabIndex = 11
    Me.cmdTermina.Text = "&Termina elaborazione"
    '
    'cmdSalta
    '
    Me.cmdSalta.Location = New System.Drawing.Point(96, 98)
    Me.cmdSalta.Name = "cmdSalta"
    Me.cmdSalta.Size = New System.Drawing.Size(75, 23)
    Me.cmdSalta.TabIndex = 11
    Me.cmdSalta.Text = "&Salta"
    '
    'cmdAggiorna
    '
    Me.cmdAggiorna.Location = New System.Drawing.Point(15, 98)
    Me.cmdAggiorna.Name = "cmdAggiorna"
    Me.cmdAggiorna.Size = New System.Drawing.Size(75, 23)
    Me.cmdAggiorna.TabIndex = 11
    Me.cmdAggiorna.Text = "&Aggiorna"
    '
    'ckElaborazioneAutomatica
    '
    Me.ckElaborazioneAutomatica.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckElaborazioneAutomatica.Location = New System.Drawing.Point(15, 127)
    Me.ckElaborazioneAutomatica.Name = "ckElaborazioneAutomatica"
    Me.ckElaborazioneAutomatica.NTSCheckValue = "S"
    Me.ckElaborazioneAutomatica.NTSUnCheckValue = "N"
    Me.ckElaborazioneAutomatica.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckElaborazioneAutomatica.Properties.Appearance.Options.UseBackColor = True
    Me.ckElaborazioneAutomatica.Properties.Caption = "Aggiorna e prosegui tramite elaborazione automatica per i record rimanenti."
    Me.ckElaborazioneAutomatica.Size = New System.Drawing.Size(417, 18)
    Me.ckElaborazioneAutomatica.TabIndex = 12
    '
    'FRMMGINPC
    '
    Me.ClientSize = New System.Drawing.Size(444, 152)
    Me.Controls.Add(Me.pnInpc)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMMGINPC"
    Me.Text = "ELABORAZIONE CATALOGO FORNITORI"
    CType(Me.pnInpc, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnInpc.ResumeLayout(False)
    Me.pnInpc.PerformLayout()
    CType(Me.ckElaborazioneAutomatica.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
  Public Overridable Sub InitEntity(ByRef oCleElct As CLEMGELCT)
    Try
      Me.oCleElct = oCleElct
      AddHandler Me.oCleElct.RemoteEvent, AddressOf GestisciEventiEntity
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
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

#Region "Eventi Form"
  Public Overridable Sub FRMMGINPC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try

      InitControls()

      strFormRetValue = "TERMINA"
      lbCodArtF.Text = oCleElct.strCodartf
      lbDesCodArtF.Text = oCleElct.strDescr
      cmdAggiorna.Focus()

      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub cmdAggiorna_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAggiorna.Click
    Try
      If ckElaborazioneAutomatica.Checked Then
        strFormRetValue = "AUTOMATICA"
      Else
        strFormRetValue = "AGGIORNA"
      End If
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdSalta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalta.Click
    Try
      strFormRetValue = "SALTA"
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdTermina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTermina.Click
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckElaborazioneAutomatica_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckElaborazioneAutomatica.CheckedChanged
    Try
      cmdSalta.Enabled = Not ckElaborazioneAutomatica.Checked
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class
