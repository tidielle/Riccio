Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGMPRO
  Public oCallParams As CLE__CLDP
  Public oCleLsar As CLEMGLSAR

#Region "Inizializzazione"
  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    oMenu = Menu
    oApp = oMenu.App
    oCallParams = Param
    If Ditta <> "" Then DittaCorrente = Ditta Else DittaCorrente = oApp.Ditta
    Me.GctlTipoDoc = ""
    InitializeComponent()
    Me.MinimumSize = Me.Size
    Return True
  End Function

  Private Sub InitializeComponent()
    Me.pnPulsanti = New NTSInformatica.NTSPanel
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.pnControlli = New NTSInformatica.NTSPanel
    Me.ckEsistMinZero = New NTSInformatica.NTSCheckBox
    Me.ckEsplFasi = New NTSInformatica.NTSCheckBox
    Me.ckEsistMagZero = New NTSInformatica.NTSCheckBox
    Me.ckEsplComb = New NTSInformatica.NTSCheckBox
    Me.ckMovimentazione = New NTSInformatica.NTSCheckBox
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnPulsanti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPulsanti.SuspendLayout()
    CType(Me.pnControlli, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnControlli.SuspendLayout()
    CType(Me.ckEsistMinZero.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckEsplFasi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckEsistMagZero.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckEsplComb.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckMovimentazione.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'pnPulsanti
    '
    Me.pnPulsanti.AllowDrop = True
    Me.pnPulsanti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPulsanti.Appearance.Options.UseBackColor = True
    Me.pnPulsanti.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPulsanti.Controls.Add(Me.cmdOk)
    Me.pnPulsanti.Controls.Add(Me.cmdAnnulla)
    Me.pnPulsanti.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPulsanti.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnPulsanti.Location = New System.Drawing.Point(395, 0)
    Me.pnPulsanti.Name = "pnPulsanti"
    Me.pnPulsanti.NTSActiveTrasparency = True
    Me.pnPulsanti.Size = New System.Drawing.Size(137, 152)
    Me.pnPulsanti.TabIndex = 7
    Me.pnPulsanti.Text = "NtsPanel1"
    '
    'cmdOk
    '
    Me.cmdOk.ImagePath = ""
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(3, 7)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.NTSContextMenu = Nothing
    Me.cmdOk.Size = New System.Drawing.Size(130, 23)
    Me.cmdOk.TabIndex = 2
    Me.cmdOk.Text = "&Conferma"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(3, 32)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(130, 23)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'pnControlli
    '
    Me.pnControlli.AllowDrop = True
    Me.pnControlli.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnControlli.Appearance.Options.UseBackColor = True
    Me.pnControlli.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnControlli.Controls.Add(Me.ckEsistMinZero)
    Me.pnControlli.Controls.Add(Me.ckEsplFasi)
    Me.pnControlli.Controls.Add(Me.ckEsistMagZero)
    Me.pnControlli.Controls.Add(Me.ckEsplComb)
    Me.pnControlli.Controls.Add(Me.ckMovimentazione)
    Me.pnControlli.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnControlli.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnControlli.Location = New System.Drawing.Point(0, 0)
    Me.pnControlli.Name = "pnControlli"
    Me.pnControlli.NTSActiveTrasparency = True
    Me.pnControlli.Size = New System.Drawing.Size(395, 152)
    Me.pnControlli.TabIndex = 8
    Me.pnControlli.Text = "NtsPanel1"
    '
    'ckEsistMinZero
    '
    Me.ckEsistMinZero.Enabled = False
    Me.ckEsistMinZero.Location = New System.Drawing.Point(12, 87)
    Me.ckEsistMinZero.Name = "ckEsistMinZero"
    Me.ckEsistMinZero.NTSCheckValue = "S"
    Me.ckEsistMinZero.NTSUnCheckValue = "N"
    Me.ckEsistMinZero.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckEsistMinZero.Properties.Appearance.Options.UseBackColor = True
    Me.ckEsistMinZero.Properties.AutoHeight = False
    Me.ckEsistMinZero.Properties.Caption = "Articoli/combinazioni con giacenza minore di zero."
    Me.ckEsistMinZero.Size = New System.Drawing.Size(376, 20)
    Me.ckEsistMinZero.TabIndex = 4
    '
    'ckEsplFasi
    '
    Me.ckEsplFasi.Location = New System.Drawing.Point(12, 120)
    Me.ckEsplFasi.Name = "ckEsplFasi"
    Me.ckEsplFasi.NTSCheckValue = "S"
    Me.ckEsplFasi.NTSUnCheckValue = "N"
    Me.ckEsplFasi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckEsplFasi.Properties.Appearance.Options.UseBackColor = True
    Me.ckEsplFasi.Properties.AutoHeight = False
    Me.ckEsplFasi.Properties.Caption = "Esplodi fasi articolo."
    Me.ckEsplFasi.Size = New System.Drawing.Size(375, 20)
    Me.ckEsplFasi.TabIndex = 3
    '
    'ckEsistMagZero
    '
    Me.ckEsistMagZero.Enabled = False
    Me.ckEsistMagZero.Location = New System.Drawing.Point(12, 61)
    Me.ckEsistMagZero.Name = "ckEsistMagZero"
    Me.ckEsistMagZero.NTSCheckValue = "S"
    Me.ckEsistMagZero.NTSUnCheckValue = "N"
    Me.ckEsistMagZero.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckEsistMagZero.Properties.Appearance.Options.UseBackColor = True
    Me.ckEsistMagZero.Properties.AutoHeight = False
    Me.ckEsistMagZero.Properties.Caption = "Articoli/combinazioni con giacenza maggiore di zero."
    Me.ckEsistMagZero.Size = New System.Drawing.Size(376, 20)
    Me.ckEsistMagZero.TabIndex = 2
    '
    'ckEsplComb
    '
    Me.ckEsplComb.Enabled = False
    Me.ckEsplComb.Location = New System.Drawing.Point(12, 37)
    Me.ckEsplComb.Name = "ckEsplComb"
    Me.ckEsplComb.NTSCheckValue = "S"
    Me.ckEsplComb.NTSUnCheckValue = "N"
    Me.ckEsplComb.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckEsplComb.Properties.Appearance.Options.UseBackColor = True
    Me.ckEsplComb.Properties.AutoHeight = False
    Me.ckEsplComb.Properties.Caption = "Esplodi combinazioni fase/lotto/commessa/ubicazione per ciascun articolo."
    Me.ckEsplComb.Size = New System.Drawing.Size(376, 20)
    Me.ckEsplComb.TabIndex = 1
    '
    'ckMovimentazione
    '
    Me.ckMovimentazione.Location = New System.Drawing.Point(12, 12)
    Me.ckMovimentazione.Name = "ckMovimentazione"
    Me.ckMovimentazione.NTSCheckValue = "S"
    Me.ckMovimentazione.NTSUnCheckValue = "N"
    Me.ckMovimentazione.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckMovimentazione.Properties.Appearance.Options.UseBackColor = True
    Me.ckMovimentazione.Properties.AutoHeight = False
    Me.ckMovimentazione.Properties.Caption = "Solo articoli movimentati sul magazzino della Lista Selezionata Articoli."
    Me.ckMovimentazione.Size = New System.Drawing.Size(376, 20)
    Me.ckMovimentazione.TabIndex = 0
    '
    'FRMMGMPRO
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(532, 152)
    Me.Controls.Add(Me.pnControlli)
    Me.Controls.Add(Me.pnPulsanti)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMMGMPRO"
    Me.Text = "SELEZIONE MOVIMENTAZIONE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPulsanti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPulsanti.ResumeLayout(False)
    CType(Me.pnControlli, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnControlli.ResumeLayout(False)
    Me.pnControlli.PerformLayout()
    CType(Me.ckEsistMinZero.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckEsplFasi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckEsistMagZero.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckEsplComb.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckMovimentazione.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, False)
    '----------------------------------------------------------------------------------------------------------------
    Dim dttLayout As New DataTable()
    Dim dttTmp As New DataTable

    Try
      dttLayout.Columns.Add("cod", GetType(String))
      dttLayout.Columns.Add("val", GetType(String))
      dttLayout.Rows.Add(New Object() {"Terminale", "(Predefinito)"})
      If oCleLsar.GetImpostazioniFile(dttTmp) = True Then
        For i As Integer = 0 To (dttTmp.Rows.Count - 1)
          With dttTmp.Rows(i)
            dttLayout.Rows.Add(New Object() {NTSCStr(!rp_liv2), Mid(NTSCStr(!rp_liv2), 11)})
          End With
        Next
      End If
      dttLayout.AcceptChanges()
      oCleLsar.bMovimentaz = False
      oCleLsar.bEsplComb = False
      oCleLsar.bEsistMagZero = False
      oCleLsar.bEsistMinZero = False
      oCleLsar.bEsplFasi = False
      If NTSCInt(oCleLsar.nCodMagP) = 0 Then
        ckMovimentazione.Enabled = False
      End If

      ckEsplFasi.NTSSetParam(oMenu, oApp.Tr(Me, 130748756044473346, "Esplodi fasi articolo."), "S", "N")
      ckEsistMagZero.NTSSetParam(oMenu, oApp.Tr(Me, 130748756044483112, "Articoli/combinazioni con giacenza maggiore di zero."), "S", "N")
      ckEsistMinZero.NTSSetParam(oMenu, oApp.Tr(Me, 130754880220380013, "Articoli/combinazioni con giacenza minore di zero."), "S", "N")
      ckEsplComb.NTSSetParam(oMenu, oApp.Tr(Me, 130748756044492878, "Esplodi combinazioni fase/lotto/commessa/ubicazione per ciascun articolo."), "S", "N")
      ckMovimentazione.NTSSetParam(oMenu, oApp.Tr(Me, 130748756044502644, "Solo articoli movimentati sul magazzino della Lista Selezionata Articoli."), "S", "N")

      '--------------------------------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, True)
    '----------------------------------------------------------------------------------------------------------------
  End Sub

  Public Overridable Sub InitEntity(ByRef cleLsar As CLEMGLSAR)
    oCleLsar = cleLsar
    AddHandler oCleLsar.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRMMGMPRO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      oCleLsar.bLayoAnnullato = True
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi Pulsanti"
  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      oCleLsar.bMovimentaz = ckMovimentazione.Checked
      oCleLsar.bEsplComb = ckEsplComb.Checked
      oCleLsar.bEsistMagZero = ckEsistMagZero.Checked
      oCleLsar.bEsistMinZero = ckEsistMinZero.Checked
      oCleLsar.bEsplFasi = ckEsplFasi.Checked
      oCleLsar.bLayoAnnullato = False
      '--------------------------------------------------------------------------------------------------------------
      Me.Close()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.Close()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Sub ckMovimentazione_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckMovimentazione.CheckedChanged
    If ckMovimentazione.Checked Then
      ckEsplComb.Enabled = True
      ckEsistMagZero.Enabled = True
      ckEsistMinZero.Enabled = True
      ckEsplFasi.Enabled = False
    Else
      ckEsplComb.Enabled = False
      ckEsistMagZero.Enabled = False
      ckEsistMinZero.Enabled = False
      ckEsplFasi.Enabled = True
    End If
  End Sub
End Class