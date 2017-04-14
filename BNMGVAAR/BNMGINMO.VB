Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGINMO

#Region "Variabili"
  Public oCleVaar As CLEMGVAAR
  Public oCallParams As CLE__CLDP
  Private components As System.ComponentModel.IContainer

  Public nInmoRichiesta As Integer
  Public strInmoCodart As String
  Public strInmoDescr As String

  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdSalta As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents lbCodart As NTSInformatica.NTSLabel
  Public WithEvents lbDescrizione As NTSInformatica.NTSLabel
  Public WithEvents lbDescr As NTSInformatica.NTSLabel
  Public WithEvents lbArticolo As NTSInformatica.NTSLabel

#End Region

  Private Sub InitializeComponent()
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdSalta = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.lbCodart = New NTSInformatica.NTSLabel
    Me.lbArticolo = New NTSInformatica.NTSLabel
    Me.lbDescrizione = New NTSInformatica.NTSLabel
    Me.lbDescr = New NTSInformatica.NTSLabel
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
    'lbCodart
    '
    Me.lbCodart.BackColor = System.Drawing.Color.Transparent
    Me.lbCodart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbCodart.Location = New System.Drawing.Point(81, 25)
    Me.lbCodart.Name = "lbCodart"
    Me.lbCodart.NTSDbField = ""
    Me.lbCodart.Size = New System.Drawing.Size(268, 20)
    Me.lbCodart.TabIndex = 20
    '
    'lbArticolo
    '
    Me.lbArticolo.AutoSize = True
    Me.lbArticolo.BackColor = System.Drawing.Color.Transparent
    Me.lbArticolo.Location = New System.Drawing.Point(11, 26)
    Me.lbArticolo.Name = "lbArticolo"
    Me.lbArticolo.NTSDbField = ""
    Me.lbArticolo.Size = New System.Drawing.Size(43, 13)
    Me.lbArticolo.TabIndex = 19
    Me.lbArticolo.Text = "Articolo"
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
    'lbDescr
    '
    Me.lbDescr.BackColor = System.Drawing.Color.Transparent
    Me.lbDescr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescr.Location = New System.Drawing.Point(81, 54)
    Me.lbDescr.Name = "lbDescr"
    Me.lbDescr.NTSDbField = ""
    Me.lbDescr.Size = New System.Drawing.Size(329, 20)
    Me.lbDescr.TabIndex = 22
    '
    'FRMMGINMO
    '
    Me.ClientSize = New System.Drawing.Size(423, 127)
    Me.Controls.Add(Me.lbDescrizione)
    Me.Controls.Add(Me.lbDescr)
    Me.Controls.Add(Me.lbArticolo)
    Me.Controls.Add(Me.lbCodart)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdSalta)
    Me.Controls.Add(Me.cmdOk)
    Me.Name = "FRMMGINMO"
    Me.Text = "Conferma modifica articolo"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Public Overridable Sub InitControls()
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
  Public Overridable Sub InitEntity(ByRef cleVaar As CLEMGVAAR)
    oCleVaar = cleVaar
    AddHandler oCleVaar.RemoteEvent, AddressOf GestisciEventiEntity
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
      nInmoRichiesta = 2
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdSalta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalta.Click
    Try
      nInmoRichiesta = 1
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      nInmoRichiesta = 0
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGINMO_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      lbCodart.Text = strInmoCodart
      lbDescr.Text = strInmoDescr

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