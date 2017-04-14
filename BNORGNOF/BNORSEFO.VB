Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORSEFO
  Public oCleGnof As CLEORGNOF
  Public oCallParams As CLE__CLDP
  Public WithEvents pnSefo As NTSInformatica.NTSPanel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents edConto As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbConto As NTSInformatica.NTSLabel
  Public WithEvents lbXxConto As NTSInformatica.NTSLabel
  Private components As System.ComponentModel.IContainer

#Region "Inizializzazione"
  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    '----------------------------------------------------------------------------------------------------------------
    oMenu = Menu
    oApp = oMenu.App
    oCallParams = Param
    '----------------------------------------------------------------------------------------------------------------
    If Ditta <> "" Then
      DittaCorrente = Ditta
    Else
      DittaCorrente = oApp.Ditta
    End If
    '----------------------------------------------------------------------------------------------------------------
    Me.GctlTipoDoc = ""
    InitializeComponent()
    Me.MinimumSize = Me.Size
    '----------------------------------------------------------------------------------------------------------------
    Return True
    '----------------------------------------------------------------------------------------------------------------
  End Function

  Private Sub InitializeComponent()
    Me.pnSefo = New NTSInformatica.NTSPanel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.lbConto = New NTSInformatica.NTSLabel
    Me.edConto = New NTSInformatica.NTSTextBoxNum
    Me.lbXxConto = New NTSInformatica.NTSLabel
    CType(Me.pnSefo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSefo.SuspendLayout()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.frmAuto.Appearance.BackColor = System.Drawing.SystemColors.GradientActiveCaption
    Me.frmAuto.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmAuto.Appearance.Options.UseBackColor = True
    Me.frmAuto.Appearance.Options.UseImage = True
    '
    'pnSefo
    '
    Me.pnSefo.AllowDrop = True
    Me.pnSefo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSefo.Appearance.Options.UseBackColor = True
    Me.pnSefo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSefo.Controls.Add(Me.lbXxConto)
    Me.pnSefo.Controls.Add(Me.edConto)
    Me.pnSefo.Controls.Add(Me.lbConto)
    Me.pnSefo.Controls.Add(Me.cmdAnnulla)
    Me.pnSefo.Controls.Add(Me.cmdOk)
    Me.pnSefo.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnSefo.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnSefo.Location = New System.Drawing.Point(0, 0)
    Me.pnSefo.Name = "pnSefo"
    Me.pnSefo.Size = New System.Drawing.Size(508, 96)
    Me.pnSefo.TabIndex = 0
    Me.pnSefo.Text = "NtsPanel1"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(421, 41)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(75, 23)
    Me.cmdAnnulla.TabIndex = 5
    Me.cmdAnnulla.Text = "Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(421, 12)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(75, 23)
    Me.cmdOk.TabIndex = 4
    Me.cmdOk.Text = "OK"
    '
    'lbConto
    '
    Me.lbConto.AutoSize = True
    Me.lbConto.BackColor = System.Drawing.Color.Transparent
    Me.lbConto.Location = New System.Drawing.Point(12, 22)
    Me.lbConto.Name = "lbConto"
    Me.lbConto.NTSDbField = ""
    Me.lbConto.Size = New System.Drawing.Size(75, 13)
    Me.lbConto.TabIndex = 6
    Me.lbConto.Text = "Cod. fornitore"
    Me.lbConto.Tooltip = ""
    Me.lbConto.UseMnemonic = False
    '
    'edConto
    '
    Me.edConto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edConto.Location = New System.Drawing.Point(100, 19)
    Me.edConto.Name = "edConto"
    Me.edConto.NTSDbField = ""
    Me.edConto.NTSFormat = "0"
    Me.edConto.NTSForzaVisZoom = False
    Me.edConto.NTSOldValue = ""
    Me.edConto.Properties.Appearance.Options.UseTextOptions = True
    Me.edConto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edConto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edConto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edConto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edConto.Properties.MaxLength = 65536
    Me.edConto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edConto.Size = New System.Drawing.Size(100, 20)
    Me.edConto.TabIndex = 7
    '
    'lbXxConto
    '
    Me.lbXxConto.BackColor = System.Drawing.Color.Transparent
    Me.lbXxConto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXxConto.Location = New System.Drawing.Point(206, 19)
    Me.lbXxConto.Name = "lbXxConto"
    Me.lbXxConto.NTSDbField = ""
    Me.lbXxConto.Size = New System.Drawing.Size(201, 20)
    Me.lbXxConto.TabIndex = 19
    Me.lbXxConto.Tooltip = ""
    Me.lbXxConto.UseMnemonic = False
    '
    'FRMORSEFO
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(508, 96)
    Me.Controls.Add(Me.pnSefo)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMORSEFO"
    Me.Text = "SELEZIONA FORNITORE"
    CType(Me.pnSefo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSefo.ResumeLayout(False)
    Me.pnSefo.PerformLayout()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, False)
    '----------------------------------------------------------------------------------------------------------------
    Try
      edConto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129530486718138839, "Conto"), tabanagraf)
      '--------------------------------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, True)
    '----------------------------------------------------------------------------------------------------------------
  End Sub

  Public Overridable Sub InitEntity(ByRef cleGnof As CLEORGNOF)
    '----------------------------------------------------------------------------------------------------------------
    oCleGnof = cleGnof
    '----------------------------------------------------------------------------------------------------------------
    AddHandler oCleGnof.RemoteEvent, AddressOf GestisciEventiEntity
    '----------------------------------------------------------------------------------------------------------------
  End Sub
#End Region

  Public Overridable Sub FRMORSEFO_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      oCleGnof.bSefoAnnulla = True

      InitControls()

      edConto.Text = "0"

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub FRMORSEFO_ActivatedFirst(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ActivatedFirst
    Try
      edConto.Focus()

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      Me.ValidaLastControl()
      'Devo selezionare almeno un conto
      If NTSCInt(edConto.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129530476881190799, "Selezionare un fornitore prima di procedere."))
        Exit Sub
      Else
        oCleGnof.lSefoConto = NTSCInt(edConto.Text)
        oCleGnof.bSefoAnnulla = False
      End If

      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      oCleGnof.bSefoAnnulla = True
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMORSEFO_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
    Try
      If e.KeyCode = Keys.F5 Then
        Zoom()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub Zoom()
    Try
      'per eventuali altri controlli caricati al volo
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      If edConto.Focused Then
        SetFastZoom(edConto.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edConto.Text
        NTSZOOM.ZoomStrIn("ZOOMANAGRAF", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edConto.Text Then edConto.NTSTextDB = NTSZOOM.strIn
      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        NTSCallStandardZoom()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edConto_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edConto.Validated
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      If oMenu.ValCodiceDb(edConto.Text, DittaCorrente, "ANAGRA", "N", strTmp, dttTmp) And NTSCInt(edConto.Text) <> 0 Then
        If NTSCStr(dttTmp.Rows(0)!an_tipo) <> "F" Then
          edConto.Text = NTSCStr(edConto.OldEditValue)
          oApp.MsgBoxErr(oApp.Tr(Me, 130084423137648203, "Il conto indicato deve far parte dei Fornitori."))
        Else
          lbXxConto.Text = strTmp
        End If
      Else
        edConto.Text = NTSCStr(edConto.OldEditValue)
      End If

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class