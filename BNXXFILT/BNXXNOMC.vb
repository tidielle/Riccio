Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMXXNOMC
  Public oCallParams As CLE__CLDP
  Public dttCampi As DataTable
  Public dcCampi As New BindingSource
  Public strNomeCampo As String = ""
  
  Private components As System.ComponentModel.IContainer

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Me.grCampi = New NTSInformatica.NTSGrid
    Me.grvCampi = New NTSInformatica.NTSGridView
    Me.cb_destab = New NTSInformatica.NTSGridColumn
    Me.cb_descampo = New NTSInformatica.NTSGridColumn
    Me.cb_tipocampo = New NTSInformatica.NTSGridColumn
    Me.cb_nomcampo = New NTSInformatica.NTSGridColumn
    Me.pnGriglia = New NTSInformatica.NTSPanel
    Me.pnSeleziona = New NTSInformatica.NTSPanel
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.pnRicerca = New NTSInformatica.NTSPanel
    Me.edRicerca = New NTSInformatica.NTSTextBoxStr
    Me.lbRicerca = New NTSInformatica.NTSLabel
    CType(Me.grCampi, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvCampi, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnGriglia, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGriglia.SuspendLayout()
    CType(Me.pnSeleziona, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSeleziona.SuspendLayout()
    CType(Me.pnRicerca, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnRicerca.SuspendLayout()
    CType(Me.edRicerca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'grCampi
    '
    Me.grCampi.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grCampi.EmbeddedNavigator.Name = ""
    Me.grCampi.Location = New System.Drawing.Point(0, 0)
    Me.grCampi.MainView = Me.grvCampi
    Me.grCampi.Name = "grCampi"
    Me.grCampi.Size = New System.Drawing.Size(486, 379)
    Me.grCampi.TabIndex = 5
    Me.grCampi.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvCampi})
    '
    'grvCampi
    '
    Me.grvCampi.ActiveFilterEnabled = False
    Me.grvCampi.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cb_destab, Me.cb_descampo, Me.cb_tipocampo, Me.cb_nomcampo})
    Me.grvCampi.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvCampi.Enabled = True
    Me.grvCampi.GridControl = Me.grCampi
    Me.grvCampi.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvCampi.MinRowHeight = 14
    Me.grvCampi.Name = "grvCampi"
    Me.grvCampi.NTSAllowDelete = True
    Me.grvCampi.NTSAllowInsert = True
    Me.grvCampi.NTSAllowUpdate = True
    Me.grvCampi.NTSMenuContext = Nothing
    Me.grvCampi.OptionsCustomization.AllowRowSizing = True
    Me.grvCampi.OptionsFilter.AllowFilterEditor = False
    Me.grvCampi.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvCampi.OptionsNavigation.UseTabKey = False
    Me.grvCampi.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvCampi.OptionsView.ColumnAutoWidth = False
    Me.grvCampi.OptionsView.EnableAppearanceEvenRow = True
    Me.grvCampi.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvCampi.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvCampi.OptionsView.ShowGroupPanel = False
    Me.grvCampi.RowHeight = 16
    '
    'cb_destab
    '
    Me.cb_destab.AppearanceCell.Options.UseBackColor = True
    Me.cb_destab.AppearanceCell.Options.UseTextOptions = True
    Me.cb_destab.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.cb_destab.Caption = "Tabella"
    Me.cb_destab.Enabled = True
    Me.cb_destab.FieldName = "cb_destab"
    Me.cb_destab.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.cb_destab.Name = "cb_destab"
    Me.cb_destab.NTSRepositoryComboBox = Nothing
    Me.cb_destab.NTSRepositoryItemCheck = Nothing
    Me.cb_destab.NTSRepositoryItemMemo = Nothing
    Me.cb_destab.NTSRepositoryItemText = Nothing
    Me.cb_destab.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.cb_destab.OptionsFilter.AllowFilter = False
    Me.cb_destab.Visible = True
    Me.cb_destab.VisibleIndex = 0
    Me.cb_destab.Width = 70
    '
    'cb_descampo
    '
    Me.cb_descampo.AppearanceCell.Options.UseBackColor = True
    Me.cb_descampo.AppearanceCell.Options.UseTextOptions = True
    Me.cb_descampo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.cb_descampo.Caption = "Campo"
    Me.cb_descampo.Enabled = True
    Me.cb_descampo.FieldName = "cb_descampo"
    Me.cb_descampo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.cb_descampo.Name = "cb_descampo"
    Me.cb_descampo.NTSRepositoryComboBox = Nothing
    Me.cb_descampo.NTSRepositoryItemCheck = Nothing
    Me.cb_descampo.NTSRepositoryItemMemo = Nothing
    Me.cb_descampo.NTSRepositoryItemText = Nothing
    Me.cb_descampo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.cb_descampo.OptionsFilter.AllowFilter = False
    Me.cb_descampo.Visible = True
    Me.cb_descampo.VisibleIndex = 1
    Me.cb_descampo.Width = 70
    '
    'cb_tipocampo
    '
    Me.cb_tipocampo.AppearanceCell.Options.UseBackColor = True
    Me.cb_tipocampo.AppearanceCell.Options.UseTextOptions = True
    Me.cb_tipocampo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.cb_tipocampo.Caption = "Tipo Campo"
    Me.cb_tipocampo.Enabled = True
    Me.cb_tipocampo.FieldName = "cb_tipocampo"
    Me.cb_tipocampo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.cb_tipocampo.Name = "cb_tipocampo"
    Me.cb_tipocampo.NTSRepositoryComboBox = Nothing
    Me.cb_tipocampo.NTSRepositoryItemCheck = Nothing
    Me.cb_tipocampo.NTSRepositoryItemMemo = Nothing
    Me.cb_tipocampo.NTSRepositoryItemText = Nothing
    Me.cb_tipocampo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.cb_tipocampo.OptionsFilter.AllowFilter = False
    Me.cb_tipocampo.Width = 70
    '
    'cb_nomcampo
    '
    Me.cb_nomcampo.AppearanceCell.Options.UseBackColor = True
    Me.cb_nomcampo.AppearanceCell.Options.UseTextOptions = True
    Me.cb_nomcampo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.cb_nomcampo.Caption = "Nome Campo"
    Me.cb_nomcampo.Enabled = True
    Me.cb_nomcampo.FieldName = "cb_nomcampo"
    Me.cb_nomcampo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.cb_nomcampo.Name = "cb_nomcampo"
    Me.cb_nomcampo.NTSRepositoryComboBox = Nothing
    Me.cb_nomcampo.NTSRepositoryItemCheck = Nothing
    Me.cb_nomcampo.NTSRepositoryItemMemo = Nothing
    Me.cb_nomcampo.NTSRepositoryItemText = Nothing
    Me.cb_nomcampo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.cb_nomcampo.OptionsFilter.AllowFilter = False
    '
    'pnGriglia
    '
    Me.pnGriglia.AllowDrop = True
    Me.pnGriglia.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGriglia.Appearance.Options.UseBackColor = True
    Me.pnGriglia.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGriglia.Controls.Add(Me.grCampi)
    Me.pnGriglia.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGriglia.Location = New System.Drawing.Point(0, 31)
    Me.pnGriglia.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGriglia.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGriglia.Name = "pnGriglia"
    Me.pnGriglia.NTSActiveTrasparency = True
    Me.pnGriglia.Size = New System.Drawing.Size(486, 379)
    Me.pnGriglia.TabIndex = 9
    Me.pnGriglia.Text = "NtsPanel1"
    '
    'pnSeleziona
    '
    Me.pnSeleziona.AllowDrop = True
    Me.pnSeleziona.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSeleziona.Appearance.Options.UseBackColor = True
    Me.pnSeleziona.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSeleziona.Controls.Add(Me.cmdSeleziona)
    Me.pnSeleziona.Controls.Add(Me.cmdAnnulla)
    Me.pnSeleziona.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnSeleziona.Location = New System.Drawing.Point(0, 410)
    Me.pnSeleziona.Name = "pnSeleziona"
    Me.pnSeleziona.NTSActiveTrasparency = True
    Me.pnSeleziona.Size = New System.Drawing.Size(486, 32)
    Me.pnSeleziona.TabIndex = 9
    Me.pnSeleziona.Text = "NtsPanel1"
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(393, 3)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.NTSContextMenu = Nothing
    Me.cmdSeleziona.Size = New System.Drawing.Size(90, 26)
    Me.cmdSeleziona.TabIndex = 0
    Me.cmdSeleziona.Text = "Seleziona"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(3, 3)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(90, 26)
    Me.cmdAnnulla.TabIndex = 0
    Me.cmdAnnulla.Text = "Annulla"
    '
    'pnRicerca
    '
    Me.pnRicerca.AllowDrop = True
    Me.pnRicerca.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnRicerca.Appearance.Options.UseBackColor = True
    Me.pnRicerca.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnRicerca.Controls.Add(Me.edRicerca)
    Me.pnRicerca.Controls.Add(Me.lbRicerca)
    Me.pnRicerca.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnRicerca.Location = New System.Drawing.Point(0, 0)
    Me.pnRicerca.Name = "pnRicerca"
    Me.pnRicerca.NTSActiveTrasparency = True
    Me.pnRicerca.Size = New System.Drawing.Size(486, 31)
    Me.pnRicerca.TabIndex = 9
    Me.pnRicerca.Text = "NtsPanel1"
    '
    'edRicerca
    '
    Me.edRicerca.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edRicerca.Location = New System.Drawing.Point(41, 6)
    Me.edRicerca.Name = "edRicerca"
    Me.edRicerca.NTSDbField = ""
    Me.edRicerca.NTSForzaVisZoom = False
    Me.edRicerca.NTSOldValue = ""
    Me.edRicerca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edRicerca.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edRicerca.Properties.AutoHeight = False
    Me.edRicerca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edRicerca.Properties.MaxLength = 65536
    Me.edRicerca.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edRicerca.Size = New System.Drawing.Size(442, 20)
    Me.edRicerca.TabIndex = 1
    '
    'lbRicerca
    '
    Me.lbRicerca.AutoSize = True
    Me.lbRicerca.BackColor = System.Drawing.Color.Transparent
    Me.lbRicerca.Location = New System.Drawing.Point(4, 9)
    Me.lbRicerca.Name = "lbRicerca"
    Me.lbRicerca.NTSDbField = ""
    Me.lbRicerca.Size = New System.Drawing.Size(35, 13)
    Me.lbRicerca.TabIndex = 0
    Me.lbRicerca.Text = "Cerca"
    Me.lbRicerca.Tooltip = ""
    Me.lbRicerca.UseMnemonic = False
    '
    'FRMXXNOMC
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(486, 442)
    Me.Controls.Add(Me.pnGriglia)
    Me.Controls.Add(Me.pnSeleziona)
    Me.Controls.Add(Me.pnRicerca)
    Me.Name = "FRMXXNOMC"
    Me.NTSLastControlFocussed = Me.grCampi
    Me.Text = "SELEZIONE CAMPI"
    CType(Me.grCampi, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvCampi, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnGriglia, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGriglia.ResumeLayout(False)
    CType(Me.pnSeleziona, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSeleziona.ResumeLayout(False)
    CType(Me.pnRicerca, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnRicerca.ResumeLayout(False)
    Me.pnRicerca.PerformLayout()
    CType(Me.edRicerca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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

#Region "Eventi Form"
  Public Overridable Sub FRMXXNOMC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      If oCallParams Is Nothing OrElse oCallParams.ctlPar1 Is Nothing Then Me.Close() : Return
      dttCampi = CType(oCallParams.ctlPar1, DataTable)
      dcCampi.DataSource = dttCampi
      grCampi.DataSource = dcCampi

      dcCampi.Filter = ""

      If IsBis() Then pnRicerca.Visible = False ' SBS\SBC non supporta il BindingSource.Filter

      '-------------------------------------------------
      'applico le regole della gctl
      GctlSetRoules()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttTipo As New DataTable
    Try
      dttTipo.Columns.Add("cod")
      dttTipo.Columns.Add("val")
      dttTipo.Rows.Add(New Object() {"3", "Intero Corto"})
      dttTipo.Rows.Add(New Object() {"4", "Intero Lungo"})
      dttTipo.Rows.Add(New Object() {"5", "Importo"})
      dttTipo.Rows.Add(New Object() {"6", "Decimale"})
      dttTipo.Rows.Add(New Object() {"7", "Decimale"})
      dttTipo.Rows.Add(New Object() {"8", "Data"})
      dttTipo.Rows.Add(New Object() {"10", "Testo"})
      dttTipo.Rows.Add(New Object() {"12", "Memo"})
      dttTipo.AcceptChanges()

      grvCampi.NTSSetParam(oMenu, oApp.Tr(Me, 130420258083613084, "Campi"))
      cb_destab.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130420258083769291, "Tabella"), 0, True)
      cb_descampo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130420258083925539, "Campo"), 0, True)
      cb_tipocampo.NTSSetParamCMB(oMenu, oApp.Tr(Me, 130420258084081754, "Tipo Campo"), dttTipo, "val", "cod")
      cb_nomcampo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130420258084238002, "Nome Campo"), 0, True)

      edRicerca.NTSSetParam(oMenu, oApp.Tr(Me, 130420258086425499, "Ricerca"), 0, True)

      grvCampi.NTSAllowDelete = False
      grvCampi.NTSAllowInsert = False
      grvCampi.NTSAllowUpdate = False

      grvCampi.Enabled = False

      cb_destab.Width = 200
      cb_descampo.Width = 200
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

#Region "Eventi"
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Try
      If grvCampi.NTSGetCurrentDataRow Is Nothing Then Return

      strNomeCampo = NTSCStr(grvCampi.NTSGetCurrentDataRow!cb_nomcampo)
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub grvCampi_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grvCampi.DoubleClick
    Try
      cmdSeleziona_Click(Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edRicerca_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edRicerca.TextChanged
    Try
      If edRicerca.Text <> "" Then
        dcCampi.Filter = "cb_descampo LIKE " & CStrSQL("%" & edRicerca.Text & "%")
      Else
        dcCampi.Filter = ""
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edRicerca_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles edRicerca.KeyDown
    Try
      Select Case e.KeyCode
        Case Keys.Down
          grvCampi.MoveNext()
        Case Keys.Up
          grvCampi.MovePrev()
        Case Keys.Enter
          cmdSeleziona_Click(Nothing, Nothing)
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdCerca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Try
      edRicerca_TextChanged(Nothing, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region
End Class
