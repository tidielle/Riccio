#Region "Import"

Imports System.Data
Imports NTSInformatica.CLN__STD

#End Region

Public Class FRM__AEMD

#Region "Variabili"

  Public WithEvents cmdConferma As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents pnApce As NTSInformatica.NTSPanel

  Public oCallParams As CLE__CLDP
  Public oCleAext As CLE__AEXT

  Public WithEvents NtsGroupBox1 As NTSInformatica.NTSGroupBox
  Public WithEvents NtsRadioButton1 As NTSInformatica.NTSRadioButton
  Public WithEvents NtsRadioButton6 As NTSInformatica.NTSRadioButton
  Public WithEvents NtsRadioButton5 As NTSInformatica.NTSRadioButton
  Public WithEvents NtsRadioButton4 As NTSInformatica.NTSRadioButton
  Public WithEvents NtsRadioButton3 As NTSInformatica.NTSRadioButton
  Public WithEvents NtsRadioButton2 As NTSInformatica.NTSRadioButton

  Public charTipork As Char

  Public bConfermato As Boolean = False
  Public WithEvents NtsRadioButton7 As NTSInformatica.NTSRadioButton
  Public WithEvents NtsRadioButton8 As NTSInformatica.NTSRadioButton

  Public bModalitaNuovo As Boolean = False

#End Region

#Region "InizializeComponent"
  Public Overridable Sub InitializeComponent()
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.pnApce = New NTSInformatica.NTSPanel
    Me.NtsGroupBox1 = New NTSInformatica.NTSGroupBox
    Me.NtsRadioButton7 = New NTSInformatica.NTSRadioButton
    Me.NtsRadioButton6 = New NTSInformatica.NTSRadioButton
    Me.NtsRadioButton5 = New NTSInformatica.NTSRadioButton
    Me.NtsRadioButton4 = New NTSInformatica.NTSRadioButton
    Me.NtsRadioButton3 = New NTSInformatica.NTSRadioButton
    Me.NtsRadioButton2 = New NTSInformatica.NTSRadioButton
    Me.NtsRadioButton1 = New NTSInformatica.NTSRadioButton
    Me.NtsRadioButton8 = New NTSInformatica.NTSRadioButton
    CType(Me.pnApce, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnApce.SuspendLayout()
    CType(Me.NtsGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsGroupBox1.SuspendLayout()
    CType(Me.NtsRadioButton7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsRadioButton6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsRadioButton5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsRadioButton4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsRadioButton3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsRadioButton2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsRadioButton1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsRadioButton8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.cmdAnnulla.Location = New System.Drawing.Point(269, 40)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(105, 22)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdConferma
    '
    Me.cmdConferma.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer))
    Me.cmdConferma.Appearance.Options.UseForeColor = True
    Me.cmdConferma.ImageText = ""
    Me.cmdConferma.Location = New System.Drawing.Point(269, 12)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.NTSContextMenu = Nothing
    Me.cmdConferma.Size = New System.Drawing.Size(105, 22)
    Me.cmdConferma.TabIndex = 2
    Me.cmdConferma.Text = "&Conferma"
    '
    'pnApce
    '
    Me.pnApce.AllowDrop = True
    Me.pnApce.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnApce.Appearance.Options.UseBackColor = True
    Me.pnApce.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnApce.Controls.Add(Me.NtsGroupBox1)
    Me.pnApce.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnApce.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnApce.Location = New System.Drawing.Point(0, 0)
    Me.pnApce.Name = "pnApce"
    Me.pnApce.NTSActiveTrasparency = True
    Me.pnApce.Size = New System.Drawing.Size(263, 210)
    Me.pnApce.TabIndex = 9
    Me.pnApce.Text = "NtsPanel1"
    '
    'NtsGroupBox1
    '
    Me.NtsGroupBox1.AllowDrop = True
    Me.NtsGroupBox1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsGroupBox1.Appearance.Options.UseBackColor = True
    Me.NtsGroupBox1.Controls.Add(Me.NtsRadioButton8)
    Me.NtsGroupBox1.Controls.Add(Me.NtsRadioButton7)
    Me.NtsGroupBox1.Controls.Add(Me.NtsRadioButton6)
    Me.NtsGroupBox1.Controls.Add(Me.NtsRadioButton5)
    Me.NtsGroupBox1.Controls.Add(Me.NtsRadioButton4)
    Me.NtsGroupBox1.Controls.Add(Me.NtsRadioButton3)
    Me.NtsGroupBox1.Controls.Add(Me.NtsRadioButton2)
    Me.NtsGroupBox1.Controls.Add(Me.NtsRadioButton1)
    Me.NtsGroupBox1.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsGroupBox1.Location = New System.Drawing.Point(12, 12)
    Me.NtsGroupBox1.Name = "NtsGroupBox1"
    Me.NtsGroupBox1.Size = New System.Drawing.Size(241, 186)
    Me.NtsGroupBox1.TabIndex = 0
    Me.NtsGroupBox1.Text = "Tipo di attributi:"
    '
    'NtsRadioButton7
    '
    Me.NtsRadioButton7.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsRadioButton7.Location = New System.Drawing.Point(20, 142)
    Me.NtsRadioButton7.Name = "NtsRadioButton7"
    Me.NtsRadioButton7.NTSCheckValue = "S"
    Me.NtsRadioButton7.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsRadioButton7.Properties.Appearance.Options.UseBackColor = True
    Me.NtsRadioButton7.Properties.AutoHeight = False
    Me.NtsRadioButton7.Properties.Caption = "Ubicazioni"
    Me.NtsRadioButton7.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.NtsRadioButton7.Size = New System.Drawing.Size(75, 18)
    Me.NtsRadioButton7.TabIndex = 1
    '
    'NtsRadioButton6
    '
    Me.NtsRadioButton6.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.NtsRadioButton6.Location = New System.Drawing.Point(20, 122)
    Me.NtsRadioButton6.Name = "NtsRadioButton6"
    Me.NtsRadioButton6.NTSCheckValue = "S"
    Me.NtsRadioButton6.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsRadioButton6.Properties.Appearance.Options.UseBackColor = True
    Me.NtsRadioButton6.Properties.AutoHeight = False
    Me.NtsRadioButton6.Properties.Caption = "Matricole"
    Me.NtsRadioButton6.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.NtsRadioButton6.Size = New System.Drawing.Size(75, 18)
    Me.NtsRadioButton6.TabIndex = 0
    '
    'NtsRadioButton5
    '
    Me.NtsRadioButton5.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsRadioButton5.Location = New System.Drawing.Point(20, 102)
    Me.NtsRadioButton5.Name = "NtsRadioButton5"
    Me.NtsRadioButton5.NTSCheckValue = "S"
    Me.NtsRadioButton5.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsRadioButton5.Properties.Appearance.Options.UseBackColor = True
    Me.NtsRadioButton5.Properties.AutoHeight = False
    Me.NtsRadioButton5.Properties.Caption = "Leads"
    Me.NtsRadioButton5.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.NtsRadioButton5.Size = New System.Drawing.Size(75, 18)
    Me.NtsRadioButton5.TabIndex = 0
    '
    'NtsRadioButton4
    '
    Me.NtsRadioButton4.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsRadioButton4.Location = New System.Drawing.Point(20, 82)
    Me.NtsRadioButton4.Name = "NtsRadioButton4"
    Me.NtsRadioButton4.NTSCheckValue = "S"
    Me.NtsRadioButton4.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsRadioButton4.Properties.Appearance.Options.UseBackColor = True
    Me.NtsRadioButton4.Properties.AutoHeight = False
    Me.NtsRadioButton4.Properties.Caption = "Destinazioni diverse fornitori"
    Me.NtsRadioButton4.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.NtsRadioButton4.Size = New System.Drawing.Size(176, 18)
    Me.NtsRadioButton4.TabIndex = 0
    '
    'NtsRadioButton3
    '
    Me.NtsRadioButton3.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsRadioButton3.Location = New System.Drawing.Point(20, 62)
    Me.NtsRadioButton3.Name = "NtsRadioButton3"
    Me.NtsRadioButton3.NTSCheckValue = "S"
    Me.NtsRadioButton3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsRadioButton3.Properties.Appearance.Options.UseBackColor = True
    Me.NtsRadioButton3.Properties.AutoHeight = False
    Me.NtsRadioButton3.Properties.Caption = "Destinazioni diverse clienti"
    Me.NtsRadioButton3.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.NtsRadioButton3.Size = New System.Drawing.Size(167, 18)
    Me.NtsRadioButton3.TabIndex = 0
    '
    'NtsRadioButton2
    '
    Me.NtsRadioButton2.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsRadioButton2.Location = New System.Drawing.Point(20, 42)
    Me.NtsRadioButton2.Name = "NtsRadioButton2"
    Me.NtsRadioButton2.NTSCheckValue = "S"
    Me.NtsRadioButton2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsRadioButton2.Properties.Appearance.Options.UseBackColor = True
    Me.NtsRadioButton2.Properties.AutoHeight = False
    Me.NtsRadioButton2.Properties.Caption = "Fornitori"
    Me.NtsRadioButton2.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.NtsRadioButton2.Size = New System.Drawing.Size(75, 18)
    Me.NtsRadioButton2.TabIndex = 0
    '
    'NtsRadioButton1
    '
    Me.NtsRadioButton1.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.NtsRadioButton1.EditValue = True
    Me.NtsRadioButton1.Location = New System.Drawing.Point(20, 22)
    Me.NtsRadioButton1.Name = "NtsRadioButton1"
    Me.NtsRadioButton1.NTSCheckValue = "S"
    Me.NtsRadioButton1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsRadioButton1.Properties.Appearance.Options.UseBackColor = True
    Me.NtsRadioButton1.Properties.AutoHeight = False
    Me.NtsRadioButton1.Properties.Caption = "Clienti"
    Me.NtsRadioButton1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.NtsRadioButton1.Size = New System.Drawing.Size(75, 18)
    Me.NtsRadioButton1.TabIndex = 0
    '
    'NtsRadioButton8
    '
    Me.NtsRadioButton8.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsRadioButton8.Location = New System.Drawing.Point(20, 162)
    Me.NtsRadioButton8.Name = "NtsRadioButton8"
    Me.NtsRadioButton8.NTSCheckValue = "S"
    Me.NtsRadioButton8.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsRadioButton8.Properties.Appearance.Options.UseBackColor = True
    Me.NtsRadioButton8.Properties.AutoHeight = False
    Me.NtsRadioButton8.Properties.Caption = "Articoli"
    Me.NtsRadioButton8.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.NtsRadioButton8.Size = New System.Drawing.Size(75, 18)
    Me.NtsRadioButton8.TabIndex = 2
    '
    'FRM__AEMD
    '
    Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(254, Byte), Integer))
    Me.Appearance.Options.UseBackColor = True
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(387, 210)
    Me.Controls.Add(Me.pnApce)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdConferma)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__AEMD"
    Me.Text = "NUOVA TIPOLOGIA ATTRIBUTI"
    CType(Me.pnApce, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnApce.ResumeLayout(False)
    CType(Me.NtsGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsGroupBox1.ResumeLayout(False)
    Me.NtsGroupBox1.PerformLayout()
    CType(Me.NtsRadioButton7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsRadioButton6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsRadioButton5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsRadioButton4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsRadioButton3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsRadioButton2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsRadioButton1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsRadioButton8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
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

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------------------------------------------
      'Chiamo lo script per inizializzare i controlli caricati con source ext
      '-------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Public Overridable Sub InitEnviroment(ByRef oCleAext As CLE__AEXT, ByVal bModalitaNuovo As Boolean)
    '-------------------------------------------------------------------------------------
    'Inizializazione ambiente personale form
    '-------------------------------------------------------------------------------------
    Me.oCleAext = oCleAext
    AddHandler oCleAext.RemoteEvent, AddressOf GestisciEventiEntity

    Me.bModalitaNuovo = bModalitaNuovo

    If bModalitaNuovo Then
      Me.Text = oApp.Tr(Me, 130420510232617200, "NUOVA TIPOLOGIA ATTRIBUTI")
    Else
      Me.Text = oApp.Tr(Me, 130420510335117856, "APRI TIPOLOGIA ATTRIBUTI")
    End If

    charTipork = "C"c
    bConfermato = False
  End Sub

#Region "EventiForm"

  Public Overridable Sub FRMCEAPCE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'Predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'Sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

#End Region

#Region "EventiControlli"

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    Try
      If NtsRadioButton1.Checked Then
        charTipork = "C"c
      ElseIf NtsRadioButton2.Checked Then
        charTipork = "F"c
      ElseIf NtsRadioButton3.Checked Then
        charTipork = "D"c
      ElseIf NtsRadioButton4.Checked Then
        charTipork = "E"c
      ElseIf NtsRadioButton5.Checked Then
        charTipork = "L"c
      ElseIf NtsRadioButton6.Checked Then
        charTipork = "M"c
      ElseIf NtsRadioButton7.Checked Then
        charTipork = "U"c
      ElseIf NtsRadioButton8.Checked Then
        charTipork = "A"c
      End If
      If oCleAext.CheckTipork(charTipork) = bModalitaNuovo Then
        If bModalitaNuovo Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128492652729843750, "Tipologia di attributi già esistente."))
        Else
          oApp.MsgBoxInfo(oApp.Tr(Me, 128492652789531250, "Tipologia di attributi non esistente."))
        End If
        Return
      End If
      bConfermato = True
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

#End Region

End Class
