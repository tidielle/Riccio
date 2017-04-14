Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__INFM

#Region "Variabili"
  Public oCleVaan As CLE__VAAN
  Public oCallParams As CLE__CLDP
  Private components As System.ComponentModel.IContainer

  Public nInfmRichiesta As Integer
  Public strInfmConto As String
  Public strInfmDescr1 As String

  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdSalta As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents lbConto As NTSInformatica.NTSLabel
  Public WithEvents lbDescrizione As NTSInformatica.NTSLabel
  Public WithEvents lbDesconto As NTSInformatica.NTSLabel
  Public WithEvents lbContoLabel As NTSInformatica.NTSLabel

#End Region

  Private Sub InitializeComponent()
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdSalta = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.lbConto = New NTSInformatica.NTSLabel
    Me.lbContoLabel = New NTSInformatica.NTSLabel
    Me.lbDescrizione = New NTSInformatica.NTSLabel
    Me.lbDesconto = New NTSInformatica.NTSLabel
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
    'cmdAnnulla
    '
    Me.cmdAnnulla.Location = New System.Drawing.Point(318, 95)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(92, 20)
    Me.cmdAnnulla.TabIndex = 14
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdSalta
    '
    Me.cmdSalta.Location = New System.Drawing.Point(112, 95)
    Me.cmdSalta.Name = "cmdSalta"
    Me.cmdSalta.Size = New System.Drawing.Size(92, 20)
    Me.cmdSalta.TabIndex = 13
    Me.cmdSalta.Text = "&Salta"
    '
    'cmdOk
    '
    Me.cmdOk.Location = New System.Drawing.Point(14, 95)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(92, 20)
    Me.cmdOk.TabIndex = 12
    Me.cmdOk.Text = "&Conferma"
    '
    'lbConto
    '
    Me.lbConto.BackColor = System.Drawing.Color.Transparent
    Me.lbConto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbConto.Location = New System.Drawing.Point(81, 25)
    Me.lbConto.Name = "lbConto"
    Me.lbConto.NTSDbField = ""
    Me.lbConto.Size = New System.Drawing.Size(268, 20)
    Me.lbConto.TabIndex = 20
    '
    'lbContoLabel
    '
    Me.lbContoLabel.AutoSize = True
    Me.lbContoLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbContoLabel.Location = New System.Drawing.Point(11, 26)
    Me.lbContoLabel.Name = "lbContoLabel"
    Me.lbContoLabel.NTSDbField = ""
    Me.lbContoLabel.Size = New System.Drawing.Size(36, 13)
    Me.lbContoLabel.TabIndex = 19
    Me.lbContoLabel.Text = "Conto"
    '
    'lbDescrizione
    '
    Me.lbDescrizione.AutoSize = True
    Me.lbDescrizione.BackColor = System.Drawing.Color.Transparent
    Me.lbDescrizione.Location = New System.Drawing.Point(11, 55)
    Me.lbDescrizione.Name = "lbDescrizione"
    Me.lbDescrizione.NTSDbField = ""
    Me.lbDescrizione.Size = New System.Drawing.Size(61, 13)
    Me.lbDescrizione.TabIndex = 21
    Me.lbDescrizione.Text = "Descrizione"
    '
    'lbDesconto
    '
    Me.lbDesconto.BackColor = System.Drawing.Color.Transparent
    Me.lbDesconto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesconto.Location = New System.Drawing.Point(81, 54)
    Me.lbDesconto.Name = "lbDesconto"
    Me.lbDesconto.NTSDbField = ""
    Me.lbDesconto.Size = New System.Drawing.Size(329, 20)
    Me.lbDesconto.TabIndex = 22
    '
    'FRM__INFM
    '
    Me.ClientSize = New System.Drawing.Size(423, 127)
    Me.Controls.Add(Me.lbDescrizione)
    Me.Controls.Add(Me.lbDesconto)
    Me.Controls.Add(Me.lbContoLabel)
    Me.Controls.Add(Me.lbConto)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdSalta)
    Me.Controls.Add(Me.cmdOk)
    Me.Name = "FRM__INFM"
    Me.Text = "Conferma modifica articolo"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Public Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------


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
  Public Overridable Sub InitEntity(ByRef cleVaan As CLE__VAAN)
    oCleVaan = cleVaan
    AddHandler oCleVaan.RemoteEvent, AddressOf GestisciEventiEntity
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
    Try
      nInfmRichiesta = 2
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdSalta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalta.Click
    Try
      nInfmRichiesta = 1
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      nInfmRichiesta = 0
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRM__INFM_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      lbConto.Text = strInfmConto
      lbDesconto.Text = strInfmDescr1

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

End Class