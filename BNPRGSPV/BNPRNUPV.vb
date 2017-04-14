#Region "Imports"
Imports System.Data
Imports NTSInformatica.CLN__STD
#End Region

Public Class FRMPRNUPV

#Region "Moduli"
  Private Moduli_P As Integer = bsModPR
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
  'parametri passati dal child che mi ha chiamato: sempre tramite questa classe
  'gli restituisceo il valore
  Public oCallParam As CLE__CLDP
  'oCallParam.dPar1 - restituisco il codice agente
  'oCallParam.strParam - descrizione dell'agente
  'oCallParam.bPar1 - se confermato vale True, altrimenti False

  Public oCleNupv As CLEPRGSPV

  Private components As System.ComponentModel.IContainer
  Public WithEvents lbCodage As NTSInformatica.NTSLabel
  Public WithEvents edCodage As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDesage As NTSInformatica.NTSLabel
  Public WithEvents pnNupv As NTSInformatica.NTSPanel
  Public WithEvents cmdConferma As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents pnButton As NTSInformatica.NTSPanel
#End Region

  Public Overridable Sub InitializeComponent()
    Me.lbCodage = New NTSInformatica.NTSLabel
    Me.edCodage = New NTSInformatica.NTSTextBoxNum
    Me.lbDesage = New NTSInformatica.NTSLabel
    Me.pnNupv = New NTSInformatica.NTSPanel
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.pnButton = New NTSInformatica.NTSPanel
    CType(Me.edCodage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnNupv, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnNupv.SuspendLayout()
    CType(Me.pnButton, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnButton.SuspendLayout()
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
    'lbCodage
    '
    Me.lbCodage.AutoSize = True
    Me.lbCodage.BackColor = System.Drawing.Color.Transparent
    Me.lbCodage.Location = New System.Drawing.Point(12, 32)
    Me.lbCodage.Name = "lbCodage"
    Me.lbCodage.NTSDbField = ""
    Me.lbCodage.Size = New System.Drawing.Size(77, 13)
    Me.lbCodage.TabIndex = 0
    Me.lbCodage.Text = "Codice Agente"
    '
    'edCodage
    '
    Me.edCodage.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodage.EditValue = "0"
    Me.edCodage.Location = New System.Drawing.Point(95, 29)
    Me.edCodage.Name = "edCodage"
    Me.edCodage.NTSDbField = ""
    Me.edCodage.NTSFormat = "0"
    Me.edCodage.NTSForzaVisZoom = False
    Me.edCodage.NTSOldValue = ""
    Me.edCodage.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodage.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodage.Properties.MaxLength = 65536
    Me.edCodage.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodage.Size = New System.Drawing.Size(65, 20)
    Me.edCodage.TabIndex = 1
    '
    'lbDesage
    '
    Me.lbDesage.BackColor = System.Drawing.Color.Transparent
    Me.lbDesage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesage.Location = New System.Drawing.Point(166, 29)
    Me.lbDesage.Name = "lbDesage"
    Me.lbDesage.NTSDbField = ""
    Me.lbDesage.Size = New System.Drawing.Size(247, 20)
    Me.lbDesage.TabIndex = 2
    '
    'pnNupv
    '
    Me.pnNupv.AllowDrop = True
    Me.pnNupv.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnNupv.Appearance.Options.UseBackColor = True
    Me.pnNupv.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnNupv.Controls.Add(Me.lbCodage)
    Me.pnNupv.Controls.Add(Me.edCodage)
    Me.pnNupv.Controls.Add(Me.lbDesage)
    Me.pnNupv.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnNupv.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnNupv.Location = New System.Drawing.Point(0, 0)
    Me.pnNupv.Name = "pnNupv"
    Me.pnNupv.Size = New System.Drawing.Size(419, 78)
    Me.pnNupv.TabIndex = 0
    Me.pnNupv.Text = "NtsPanel1"
    '
    'cmdConferma
    '
    Me.cmdConferma.Location = New System.Drawing.Point(9, 14)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.Size = New System.Drawing.Size(100, 23)
    Me.cmdConferma.TabIndex = 8
    Me.cmdConferma.Text = "&Conferma"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Location = New System.Drawing.Point(9, 43)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(100, 23)
    Me.cmdAnnulla.TabIndex = 9
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'pnButton
    '
    Me.pnButton.AllowDrop = True
    Me.pnButton.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnButton.Appearance.Options.UseBackColor = True
    Me.pnButton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnButton.Controls.Add(Me.cmdConferma)
    Me.pnButton.Controls.Add(Me.cmdAnnulla)
    Me.pnButton.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnButton.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnButton.Location = New System.Drawing.Point(419, 0)
    Me.pnButton.Name = "pnButton"
    Me.pnButton.Size = New System.Drawing.Size(123, 78)
    Me.pnButton.TabIndex = 1
    Me.pnButton.Text = "NtsPanel1"
    '
    'FRMPRNUPV
    '
    Me.ClientSize = New System.Drawing.Size(542, 78)
    Me.Controls.Add(Me.pnButton)
    Me.Controls.Add(Me.pnNupv)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.Name = "FRMPRNUPV"
    Me.Text = "NUOVE PROVVIGIONI AGENTE"
    CType(Me.edCodage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnNupv, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnNupv.ResumeLayout(False)
    Me.pnNupv.PerformLayout()
    CType(Me.pnButton, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnButton.ResumeLayout(False)
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
      edCodage.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128727951705312500, "Codice Agente"), tabcage)
      edCodage.NTSSetRichiesto()
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
      oCallParam.bPar1 = False

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'applico le regole della gctl
      GctlSetRoules()
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
    Try
      Me.ValidaLastControl()

      If NTSCInt(edCodage.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128683848258896584, "Codice agente obbligatorio."))
        Return
      End If

      oCallParam.dPar1 = NTSCInt(edCodage.Text)
      oCallParam.bPar1 = True

      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edCodage_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodage.Validated
    Try
      oCallParam.strParam = ""
      If edCodage.Text <> "0" Then
        If Not oCleNupv.VerificaAgente(edCodage.Text, oCallParam.strParam) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128671660719172611, "Codice agente inesistente."))
          edCodage.Text = "0"
        End If
      End If
      lbDesage.Text = oCallParam.strParam

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class

