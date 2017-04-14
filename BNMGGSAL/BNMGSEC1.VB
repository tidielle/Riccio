Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGSEC1
  Public oCallParams As CLE__CLDP
  Public bAnnullato As Boolean = True

#Region "VARIABILI"
  Private components As System.ComponentModel.IContainer
  Public WithEvents pbSecf As NTSInformatica.NTSPanel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOK As NTSInformatica.NTSButton
  Public WithEvents edCodarfo As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbCodarfo As NTSInformatica.NTSLabel
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

  Public Overridable Sub InitializeComponent()
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOK = New NTSInformatica.NTSButton
    Me.edCodarfo = New NTSInformatica.NTSTextBoxStr
    Me.lbCodarfo = New NTSInformatica.NTSLabel
    Me.pbSecf = New NTSInformatica.NTSPanel
    CType(Me.edCodarfo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pbSecf, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pbSecf.SuspendLayout()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdAnnulla.Location = New System.Drawing.Point(439, 49)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(129, 23)
    Me.cmdAnnulla.TabIndex = 11
    Me.cmdAnnulla.Text = "Annulla"
    '
    'cmdOK
    '
    Me.cmdOK.Location = New System.Drawing.Point(439, 20)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.Size = New System.Drawing.Size(129, 23)
    Me.cmdOK.TabIndex = 10
    Me.cmdOK.Text = "Conferma"
    '
    'edCodarfo
    '
    Me.edCodarfo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodarfo.Location = New System.Drawing.Point(178, 28)
    Me.edCodarfo.Name = "edCodarfo"
    Me.edCodarfo.NTSDbField = ""
    Me.edCodarfo.NTSForzaVisZoom = True
    Me.edCodarfo.NTSOldValue = ""
    Me.edCodarfo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodarfo.Properties.MaxLength = 65536
    Me.edCodarfo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodarfo.Size = New System.Drawing.Size(240, 20)
    Me.edCodarfo.TabIndex = 24
    '
    'lbCodarfo
    '
    Me.lbCodarfo.AutoSize = True
    Me.lbCodarfo.BackColor = System.Drawing.Color.Transparent
    Me.lbCodarfo.Location = New System.Drawing.Point(10, 31)
    Me.lbCodarfo.Name = "lbCodarfo"
    Me.lbCodarfo.NTSDbField = ""
    Me.lbCodarfo.Size = New System.Drawing.Size(162, 13)
    Me.lbCodarfo.TabIndex = 25
    Me.lbCodarfo.Text = "Codice Articolo Cliente/Fornitore"
    '
    'pbSecf
    '
    Me.pbSecf.AllowDrop = True
    Me.pbSecf.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pbSecf.Appearance.Options.UseBackColor = True
    Me.pbSecf.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pbSecf.Controls.Add(Me.edCodarfo)
    Me.pbSecf.Controls.Add(Me.lbCodarfo)
    Me.pbSecf.Cursor = System.Windows.Forms.Cursors.Default
    Me.pbSecf.Location = New System.Drawing.Point(12, 8)
    Me.pbSecf.Name = "pbSecf"
    Me.pbSecf.Size = New System.Drawing.Size(421, 64)
    Me.pbSecf.TabIndex = 26
    Me.pbSecf.Text = "NtsPanel1"
    '
    'FRMMGSEC1
    '
    Me.AcceptButton = Me.cmdOK
    Me.CancelButton = Me.cmdAnnulla
    Me.ClientSize = New System.Drawing.Size(590, 97)
    Me.Controls.Add(Me.pbSecf)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOK)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMMGSEC1"
    Me.Text = "Selezione Articolo Cliente/Fornitore"
    CType(Me.edCodarfo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pbSecf, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pbSecf.ResumeLayout(False)
    Me.pbSecf.PerformLayout()
    Me.ResumeLayout(False)

  End Sub

  Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      edCodarfo.NTSSetParam(oMenu, oApp.Tr(Me, 128559425120760252, "Codice Articolo Cliente/Fornitore"), 18)

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
  Public Overridable Sub FRMMGSECF_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      GctlSetRoules()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "CONTROLLI"
  Public Overridable Sub edCodarfo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCodarfo.Validated
    Try
      edCodarfo.Text = edCodarfo.Text.ToUpper
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "PULSANTI"
  Public Overridable Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
    If edCodarfo.Text.Trim = "" Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128559989400949300, "Indicare un codice articolo cliente/fornitore valido."))
      Return
    End If
    bAnnullato = False
    Me.Close()
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Me.Close()
  End Sub
#End Region
End Class