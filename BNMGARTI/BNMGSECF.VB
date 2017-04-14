Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGSECF

#Region "Variabili"
  Public oCleArti As CLEMGARTI
  Public oCallParams As CLE__CLDP
  Private components As System.ComponentModel.IContainer

  Public strSecfCodarfo As String
  Public bSecfAnnullato As Boolean

  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents pnSecf As NTSInformatica.NTSPanel
  Public WithEvents edCodarfo As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbCodarfo As NTSInformatica.NTSLabel

#End Region

  Public Overridable Sub InitializeComponent()
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.pnSecf = New NTSInformatica.NTSPanel
    Me.edCodarfo = New NTSInformatica.NTSTextBoxStr
    Me.lbCodarfo = New NTSInformatica.NTSLabel
    CType(Me.pnSecf, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSecf.SuspendLayout()
    CType(Me.edCodarfo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.cmdAnnulla.Location = New System.Drawing.Point(403, 38)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(95, 26)
    Me.cmdAnnulla.TabIndex = 14
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(403, 6)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(95, 26)
    Me.cmdOk.TabIndex = 12
    Me.cmdOk.Text = "&Ok"
    '
    'pnSecf
    '
    Me.pnSecf.AllowDrop = True
    Me.pnSecf.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSecf.Appearance.Options.UseBackColor = True
    Me.pnSecf.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSecf.Controls.Add(Me.edCodarfo)
    Me.pnSecf.Controls.Add(Me.lbCodarfo)
    Me.pnSecf.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.pnSecf.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnSecf.Location = New System.Drawing.Point(0, 0)
    Me.pnSecf.Name = "pnSecf"
    Me.pnSecf.Size = New System.Drawing.Size(397, 71)
    Me.pnSecf.TabIndex = 22
    Me.pnSecf.Text = "NtsPanel1"
    '
    'edCodarfo
    '
    Me.edCodarfo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodarfo.Location = New System.Drawing.Point(175, 12)
    Me.edCodarfo.Name = "edCodarfo"
    Me.edCodarfo.NTSDbField = ""
    Me.edCodarfo.NTSForzaVisZoom = False
    Me.edCodarfo.NTSOldValue = ""
    Me.edCodarfo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodarfo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodarfo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodarfo.Properties.MaxLength = 65536
    Me.edCodarfo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodarfo.Size = New System.Drawing.Size(216, 20)
    Me.edCodarfo.TabIndex = 24
    '
    'lbCodarfo
    '
    Me.lbCodarfo.AutoSize = True
    Me.lbCodarfo.BackColor = System.Drawing.Color.Transparent
    Me.lbCodarfo.Location = New System.Drawing.Point(7, 15)
    Me.lbCodarfo.Name = "lbCodarfo"
    Me.lbCodarfo.NTSDbField = ""
    Me.lbCodarfo.Size = New System.Drawing.Size(162, 13)
    Me.lbCodarfo.TabIndex = 23
    Me.lbCodarfo.Text = "Codice Articolo Cliente/Fornitore"
    Me.lbCodarfo.Tooltip = ""
    Me.lbCodarfo.UseMnemonic = False
    '
    'FRMMGSECF
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(508, 71)
    Me.Controls.Add(Me.pnSecf)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.Name = "FRMMGSECF"
    Me.Text = "SELEZIONE ARTICOLO CLIENTE/FORNITORE"
    CType(Me.pnSecf, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSecf.ResumeLayout(False)
    Me.pnSecf.PerformLayout()
    CType(Me.edCodarfo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      edCodarfo.NTSSetParam(oMenu, oApp.Tr(Me, 128553204030161560, "Codice Articolo Cliente/Fornitore"), 50, True)

      edCodarfo.NTSSetRichiesto()

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
  Public Overridable Sub InitEntity(ByRef cleArti As CLEMGARTI)
    oCleArti = cleArti
    AddHandler oCleArti.RemoteEvent, AddressOf GestisciEventiEntity
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
      '-----------------------------------------------------------------------------------------
      '--- Controlla la validità dei dati
      '-----------------------------------------------------------------------------------------
      If Trim(edCodarfo.Text) = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128553084833948568, "Indicare il Codice Articolo Cliente/Fornitore"))
        edCodarfo.Focus()
        Exit Sub
      End If
      '-----------------------------------------------------------------------------------------
      '--- Setta la variabili di ritorno
      '-----------------------------------------------------------------------------------------
      bSecfAnnullato = False
      strSecfCodarfo = UCase(edCodarfo.Text)
      '-----------------------------------------------------------------------------------------
      Me.Close()
      '-----------------------------------------------------------------------------------------
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    bSecfAnnullato = True
    Me.Close()
  End Sub
  Public Overridable Sub FRMMGSECF_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      bSecfAnnullato = True

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

  Public Overridable Sub edCodarfo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCodarfo.Validated
    Try
      If Trim(edCodarfo.Text) <> "" Then edCodarfo.Text = UCase(edCodarfo.Text)
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
End Class