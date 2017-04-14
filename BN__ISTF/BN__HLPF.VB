Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__HLPF
  Public oCleIstf As CLE__ISTF
  Public oCallParams As CLE__CLDP

  Public dsHlpf As New DataSet
  Public dcHlpf As BindingSource = New BindingSource()

  Private components As System.ComponentModel.IContainer

#Region "Inizializzazione"
  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    '----------------------------------------------------------------------------------------------------------------
    oMenu = Menu
    oApp = oMenu.App
    '----------------------------------------------------------------------------------------------------------------
    oCallParams = Param
    '----------------------------------------------------------------------------------------------------------------
    If Ditta <> "" Then DittaCorrente = Ditta Else DittaCorrente = oApp.Ditta
    Me.GctlTipoDoc = ""
    '----------------------------------------------------------------------------------------------------------------
    InitializeComponent()
    Me.MinimumSize = Me.Size
    '----------------------------------------------------------------------------------------------------------------
    Return True
  End Function

  Private Sub InitializeComponent()
    Me.pnAction = New NTSInformatica.NTSPanel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.grHlpf = New NTSInformatica.NTSGrid
    Me.grvHlpf = New NTSInformatica.NTSGridView
    Me.pf_codform = New NTSInformatica.NTSGridColumn
    Me.pf_titstam = New NTSInformatica.NTSGridColumn
    CType(Me.pnAction, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAction.SuspendLayout()
    CType(Me.grHlpf, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvHlpf, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'pnAction
    '
    Me.pnAction.AllowDrop = True
    Me.pnAction.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAction.Appearance.Options.UseBackColor = True
    Me.pnAction.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAction.Controls.Add(Me.cmdAnnulla)
    Me.pnAction.Controls.Add(Me.cmdSeleziona)
    Me.pnAction.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnAction.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnAction.Location = New System.Drawing.Point(598, 0)
    Me.pnAction.Name = "pnAction"
    Me.pnAction.Size = New System.Drawing.Size(157, 313)
    Me.pnAction.TabIndex = 3
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(10, 35)
    Me.cmdAnnulla.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(138, 22)
    Me.cmdAnnulla.TabIndex = 3
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(10, 10)
    Me.cmdSeleziona.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.Size = New System.Drawing.Size(138, 22)
    Me.cmdSeleziona.TabIndex = 1
    Me.cmdSeleziona.Text = "&Seleziona"
    '
    'grHlpf
    '
    Me.grHlpf.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grHlpf.EmbeddedNavigator.Name = ""
    Me.grHlpf.Location = New System.Drawing.Point(0, 0)
    Me.grHlpf.MainView = Me.grvHlpf
    Me.grHlpf.Name = "grHlpf"
    Me.grHlpf.Size = New System.Drawing.Size(598, 313)
    Me.grHlpf.TabIndex = 4
    Me.grHlpf.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvHlpf})
    '
    'grvHlpf
    '
    Me.grvHlpf.ActiveFilterEnabled = False
    Me.grvHlpf.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.pf_codform, Me.pf_titstam})
    Me.grvHlpf.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvHlpf.Enabled = True
    Me.grvHlpf.GridControl = Me.grHlpf
    Me.grvHlpf.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvHlpf.Name = "grvHlpf"
    Me.grvHlpf.NTSAllowDelete = True
    Me.grvHlpf.NTSAllowInsert = True
    Me.grvHlpf.NTSAllowUpdate = True
    Me.grvHlpf.NTSMenuContext = Nothing
    Me.grvHlpf.OptionsCustomization.AllowRowSizing = True
    Me.grvHlpf.OptionsFilter.AllowFilterEditor = False
    Me.grvHlpf.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvHlpf.OptionsNavigation.UseTabKey = False
    Me.grvHlpf.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvHlpf.OptionsView.ColumnAutoWidth = False
    Me.grvHlpf.OptionsView.EnableAppearanceEvenRow = True
    Me.grvHlpf.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvHlpf.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvHlpf.OptionsView.ShowGroupPanel = False
    Me.grvHlpf.RowHeight = 16
    '
    'pf_codform
    '
    Me.pf_codform.AppearanceCell.Options.UseBackColor = True
    Me.pf_codform.AppearanceCell.Options.UseTextOptions = True
    Me.pf_codform.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pf_codform.Caption = "Formato di stampa"
    Me.pf_codform.Enabled = True
    Me.pf_codform.FieldName = "pf_codform"
    Me.pf_codform.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pf_codform.Name = "pf_codform"
    Me.pf_codform.NTSRepositoryComboBox = Nothing
    Me.pf_codform.NTSRepositoryItemCheck = Nothing
    Me.pf_codform.NTSRepositoryItemMemo = Nothing
    Me.pf_codform.NTSRepositoryItemText = Nothing
    Me.pf_codform.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pf_codform.OptionsFilter.AllowFilter = False
    Me.pf_codform.Visible = True
    Me.pf_codform.VisibleIndex = 0
    Me.pf_codform.Width = 66
    '
    'pf_titstam
    '
    Me.pf_titstam.AppearanceCell.Options.UseBackColor = True
    Me.pf_titstam.AppearanceCell.Options.UseTextOptions = True
    Me.pf_titstam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pf_titstam.Caption = "Titolo"
    Me.pf_titstam.Enabled = True
    Me.pf_titstam.FieldName = "pf_titstam"
    Me.pf_titstam.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pf_titstam.Name = "pf_titstam"
    Me.pf_titstam.NTSRepositoryComboBox = Nothing
    Me.pf_titstam.NTSRepositoryItemCheck = Nothing
    Me.pf_titstam.NTSRepositoryItemMemo = Nothing
    Me.pf_titstam.NTSRepositoryItemText = Nothing
    Me.pf_titstam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pf_titstam.OptionsFilter.AllowFilter = False
    Me.pf_titstam.Visible = True
    Me.pf_titstam.VisibleIndex = 1
    Me.pf_titstam.Width = 70
    '
    'FRM__HLPF
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(755, 313)
    Me.Controls.Add(Me.grHlpf)
    Me.Controls.Add(Me.pnAction)
    Me.MinimizeBox = False
    Me.Name = "FRM__HLPF"
    Me.Text = "ZOOM FORMATI DI STAMPA"
    CType(Me.pnAction, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAction.ResumeLayout(False)
    CType(Me.grHlpf, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvHlpf, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, False)
    '----------------------------------------------------------------------------------------------------------------
    Try
      '--------------------------------------------------------------------------------------------------------------
      grvHlpf.NTSSetParam(oMenu, oApp.Tr(Me, 128230023411980610, "Griglia Zoom Formato di Stampa"))
      '--------------------------------------------------------------------------------------------------------------
      pf_codform.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128595426229062500, "Formato di stampa"), "0", 9)
      pf_titstam.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129810216574253642, "Titolo"), 0, True)
      '--------------------------------------------------------------------------------------------------------------
      grvHlpf.NTSAllowDelete = False
      grvHlpf.NTSAllowUpdate = False
      grvHlpf.NTSAllowInsert = False
      grvHlpf.Enabled = False
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

  Public Overridable Sub InitEntity(ByVal cleIstf As CLE__ISTF)
    oCleIstf = cleIstf
    AddHandler oCleIstf.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRM__HLPF_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      '--------------------------------------------------------------------------------------------------------------
      oCleIstf.ApriZoom(dsHlpf)
      dcHlpf.DataSource = dsHlpf.Tables("PARSTAF")
      dsHlpf.AcceptChanges()
      grHlpf.DataSource = dcHlpf
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRM__HLPF_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      oCleIstf.bHlpfAnnullato = True
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
      GctlApplicaDefaultValue()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRM__HLPF_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Try
      '--------------------------------------------------------------------------------------------------------------
      If e.KeyCode = Keys.Enter Then
        If grvHlpf.Focused And grvHlpf.RowCount > 0 Then
          e.Handled = True
          cmdSeleziona_Click(Me, Nothing)
        End If
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRM__HLPF_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      '--------------------------------------------------------------------------------------------------------------
      dcHlpf.Dispose()
      If Not dsHlpf Is Nothing Then dsHlpf.Dispose()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi Pulsanti"
  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not grvHlpf.NTSGetCurrentDataRow() Is Nothing Then
        oCleIstf.lHlpfCodform = NTSCInt(grvHlpf.NTSGetCurrentDataRow!pf_codform)
        oCleIstf.bHlpfAnnullato = False
      End If
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

#Region "Eventi di Griglia"
  Public Overridable Sub grgrHlpf_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grHlpf.MouseDoubleClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      cmdSeleziona_Click(Me, Nothing)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

End Class