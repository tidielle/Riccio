Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__HLCE

  Public oParam As CLE__PATB       'parametri passati dal child che mi ha chiamato: sempre tramite questa classe gli restituisceo il valore
  Public oCleHlce As CLE__HLCE
  Public dsHlce As New DataSet
  Public dcHlce As BindingSource = New BindingSource()

  Public strTipoBil As String = ""
  Public nCodTipoRicl As Integer = 0
  Public strTipoSezione As String = ""
  Public strTextForm As String = ""

  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByVal NomeZoom As String, _
                                 ByRef Param As CLE__PATB, Optional ByVal Ditta As String = "", _
                                 Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    oMenu = Menu
    oApp = oMenu.App
    If Ditta <> "" Then
      DittaCorrente = Ditta
    Else
      DittaCorrente = oApp.Ditta
    End If

    If Ditta <> "" Then DittaCorrente = Ditta

    oParam = Param
    strTipoBil = oParam.strTipoBil
    nCodTipoRicl = oParam.nCodTipoRicl
    strTipoSezione = oParam.strTipoSezione

    InitializeComponent()

    Me.MinimizeBox = False

    '------------------------------------------------
    'creo e attivo l'entity
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__HLCE", "BE__HLCE", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 127934577957343750, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleHlce = CType(oTmp, CLE__HLCE)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__HLCE", strRemoteServer, strRemotePort)
    oCleHlce.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort)

    '---------------------------------
    'inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    AddHandler oCleHlce.RemoteEvent, AddressOf GestisciEventiEntity

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'completo le informazioni dei i controlli
      optAttivo.NTSSetParam(oMenu, oApp.Tr(Me, 128230023463673873, "Attività"), "S")
      optPassivo.NTSSetParam(oMenu, oApp.Tr(Me, 128230023463830046, "Passività"), "S")
      optContoEc.NTSSetParam(oMenu, oApp.Tr(Me, 128230023463986219, "Conto economico"), "S")
      optContiDo.NTSSetParam(oMenu, oApp.Tr(Me, 128230023464142392, "Conti d'ordine"), "S")

      grvZoom.NTSSetParam(oMenu, oApp.Tr(Me, 128230023464298565, "Griglia Zoom"))
      grvZoom.NTSAllowDelete = False
      grvZoom.NTSAllowUpdate = False
      grvZoom.NTSAllowInsert = False
      F1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023464454738, "Riferimenti"), 50, True)
      f2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023464610911, "f1"), 50, True)
      f3.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023464767084, "f2"), 50, True)
      f4.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023464923257, "f3"), 50, True)
      f5.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023465079430, "f4"), 50, True)
      f6.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023465235603, "f5"), 50, True)
      grvZoom.Enabled = False
      edCerca.NTSSetParam(oMenu, oApp.Tr(Me, 130029701251927294, "Cerca"), 0)

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

  Public Overridable Sub InitializeComponent()
    Me.pnAction = New NTSInformatica.NTSPanel
    Me.fmSezione = New NTSInformatica.NTSGroupBox
    Me.optContiDo = New NTSInformatica.NTSRadioButton
    Me.optContoEc = New NTSInformatica.NTSRadioButton
    Me.optPassivo = New NTSInformatica.NTSRadioButton
    Me.optAttivo = New NTSInformatica.NTSRadioButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.grZoom = New NTSInformatica.NTSGrid
    Me.grvZoom = New NTSInformatica.NTSGridView
    Me.f1 = New NTSInformatica.NTSGridColumn
    Me.f2 = New NTSInformatica.NTSGridColumn
    Me.f3 = New NTSInformatica.NTSGridColumn
    Me.f4 = New NTSInformatica.NTSGridColumn
    Me.f5 = New NTSInformatica.NTSGridColumn
    Me.f6 = New NTSInformatica.NTSGridColumn
    Me.edCerca = New NTSInformatica.NTSTextBoxStr
    Me.cmdCerca = New NTSInformatica.NTSButton
    CType(Me.pnAction, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAction.SuspendLayout()
    CType(Me.fmSezione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmSezione.SuspendLayout()
    CType(Me.optContiDo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optContoEc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optPassivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optAttivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grZoom, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvZoom, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCerca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.pnAction.Controls.Add(Me.cmdCerca)
    Me.pnAction.Controls.Add(Me.edCerca)
    Me.pnAction.Controls.Add(Me.fmSezione)
    Me.pnAction.Controls.Add(Me.cmdAnnulla)
    Me.pnAction.Controls.Add(Me.cmdSeleziona)
    Me.pnAction.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnAction.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnAction.Location = New System.Drawing.Point(428, 0)
    Me.pnAction.Name = "pnAction"
    Me.pnAction.NTSActiveTrasparency = True
    Me.pnAction.Size = New System.Drawing.Size(140, 340)
    Me.pnAction.TabIndex = 2
    '
    'fmSezione
    '
    Me.fmSezione.AllowDrop = True
    Me.fmSezione.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.fmSezione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmSezione.Appearance.Options.UseBackColor = True
    Me.fmSezione.Controls.Add(Me.optContiDo)
    Me.fmSezione.Controls.Add(Me.optContoEc)
    Me.fmSezione.Controls.Add(Me.optPassivo)
    Me.fmSezione.Controls.Add(Me.optAttivo)
    Me.fmSezione.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmSezione.Location = New System.Drawing.Point(10, 199)
    Me.fmSezione.Name = "fmSezione"
    Me.fmSezione.Size = New System.Drawing.Size(118, 129)
    Me.fmSezione.TabIndex = 4
    Me.fmSezione.Text = "Sezione di bilancio"
    '
    'optContiDo
    '
    Me.optContiDo.Cursor = System.Windows.Forms.Cursors.Default
    Me.optContiDo.Location = New System.Drawing.Point(8, 98)
    Me.optContiDo.Name = "optContiDo"
    Me.optContiDo.NTSCheckValue = "S"
    Me.optContiDo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optContiDo.Properties.Appearance.Options.UseBackColor = True
    Me.optContiDo.Properties.Caption = "Conti &D'ordine"
    Me.optContiDo.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optContiDo.Size = New System.Drawing.Size(105, 19)
    Me.optContiDo.TabIndex = 3
    '
    'optContoEc
    '
    Me.optContoEc.Cursor = System.Windows.Forms.Cursors.Default
    Me.optContoEc.Location = New System.Drawing.Point(8, 73)
    Me.optContoEc.Name = "optContoEc"
    Me.optContoEc.NTSCheckValue = "S"
    Me.optContoEc.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optContoEc.Properties.Appearance.Options.UseBackColor = True
    Me.optContoEc.Properties.Caption = "Conto &Economico"
    Me.optContoEc.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optContoEc.Size = New System.Drawing.Size(105, 19)
    Me.optContoEc.TabIndex = 2
    '
    'optPassivo
    '
    Me.optPassivo.Cursor = System.Windows.Forms.Cursors.Default
    Me.optPassivo.Location = New System.Drawing.Point(8, 48)
    Me.optPassivo.Name = "optPassivo"
    Me.optPassivo.NTSCheckValue = "S"
    Me.optPassivo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optPassivo.Properties.Appearance.Options.UseBackColor = True
    Me.optPassivo.Properties.Caption = "&Passività"
    Me.optPassivo.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optPassivo.Size = New System.Drawing.Size(105, 19)
    Me.optPassivo.TabIndex = 1
    '
    'optAttivo
    '
    Me.optAttivo.Cursor = System.Windows.Forms.Cursors.Default
    Me.optAttivo.EditValue = True
    Me.optAttivo.Location = New System.Drawing.Point(8, 23)
    Me.optAttivo.Name = "optAttivo"
    Me.optAttivo.NTSCheckValue = "S"
    Me.optAttivo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optAttivo.Properties.Appearance.Options.UseBackColor = True
    Me.optAttivo.Properties.Caption = "&Attività"
    Me.optAttivo.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optAttivo.Size = New System.Drawing.Size(105, 19)
    Me.optAttivo.TabIndex = 0
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(10, 39)
    Me.cmdAnnulla.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(117, 26)
    Me.cmdAnnulla.TabIndex = 3
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(10, 10)
    Me.cmdSeleziona.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.Size = New System.Drawing.Size(117, 26)
    Me.cmdSeleziona.TabIndex = 1
    Me.cmdSeleziona.Text = "&Seleziona"
    '
    'grZoom
    '
    Me.grZoom.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grZoom.EmbeddedNavigator.Name = ""
    Me.grZoom.Location = New System.Drawing.Point(0, 0)
    Me.grZoom.MainView = Me.grvZoom
    Me.grZoom.Name = "grZoom"
    Me.grZoom.Size = New System.Drawing.Size(428, 340)
    Me.grZoom.TabIndex = 3
    Me.grZoom.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvZoom})
    '
    'grvZoom
    '
    Me.grvZoom.ActiveFilterEnabled = False
    Me.grvZoom.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.f1, Me.f2, Me.f3, Me.f4, Me.f5, Me.f6})
    Me.grvZoom.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvZoom.Enabled = True
    Me.grvZoom.GridControl = Me.grZoom
    Me.grvZoom.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvZoom.MinRowHeight = 14
    Me.grvZoom.Name = "grvZoom"
    Me.grvZoom.NTSAllowDelete = True
    Me.grvZoom.NTSAllowInsert = True
    Me.grvZoom.NTSAllowUpdate = True
    Me.grvZoom.NTSMenuContext = Nothing
    Me.grvZoom.OptionsCustomization.AllowRowSizing = True
    Me.grvZoom.OptionsFilter.AllowFilterEditor = False
    Me.grvZoom.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvZoom.OptionsNavigation.UseTabKey = False
    Me.grvZoom.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvZoom.OptionsView.ColumnAutoWidth = False
    Me.grvZoom.OptionsView.EnableAppearanceEvenRow = True
    Me.grvZoom.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvZoom.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvZoom.OptionsView.ShowGroupPanel = False
    Me.grvZoom.RowHeight = 16
    '
    'f1
    '
    Me.f1.AppearanceCell.Options.UseBackColor = True
    Me.f1.AppearanceCell.Options.UseTextOptions = True
    Me.f1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.f1.Caption = "Riferimenti"
    Me.f1.Enabled = True
    Me.f1.FieldName = "f1"
    Me.f1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.f1.Name = "f1"
    Me.f1.NTSRepositoryComboBox = Nothing
    Me.f1.NTSRepositoryItemCheck = Nothing
    Me.f1.NTSRepositoryItemMemo = Nothing
    Me.f1.NTSRepositoryItemText = Nothing
    Me.f1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.f1.OptionsFilter.AllowFilter = False
    Me.f1.Visible = True
    Me.f1.VisibleIndex = 0
    Me.f1.Width = 94
    '
    'f2
    '
    Me.f2.AppearanceCell.Options.UseBackColor = True
    Me.f2.AppearanceCell.Options.UseTextOptions = True
    Me.f2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.f2.Caption = "Livello 1"
    Me.f2.Enabled = True
    Me.f2.FieldName = "f2"
    Me.f2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.f2.Name = "f2"
    Me.f2.NTSRepositoryComboBox = Nothing
    Me.f2.NTSRepositoryItemCheck = Nothing
    Me.f2.NTSRepositoryItemMemo = Nothing
    Me.f2.NTSRepositoryItemText = Nothing
    Me.f2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.f2.OptionsFilter.AllowFilter = False
    Me.f2.Visible = True
    Me.f2.VisibleIndex = 1
    Me.f2.Width = 300
    '
    'f3
    '
    Me.f3.AppearanceCell.Options.UseBackColor = True
    Me.f3.AppearanceCell.Options.UseTextOptions = True
    Me.f3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.f3.Caption = "Livello 2"
    Me.f3.Enabled = True
    Me.f3.FieldName = "f3"
    Me.f3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.f3.Name = "f3"
    Me.f3.NTSRepositoryComboBox = Nothing
    Me.f3.NTSRepositoryItemCheck = Nothing
    Me.f3.NTSRepositoryItemMemo = Nothing
    Me.f3.NTSRepositoryItemText = Nothing
    Me.f3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.f3.OptionsFilter.AllowFilter = False
    Me.f3.Visible = True
    Me.f3.VisibleIndex = 2
    '
    'f4
    '
    Me.f4.AppearanceCell.Options.UseBackColor = True
    Me.f4.AppearanceCell.Options.UseTextOptions = True
    Me.f4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.f4.Caption = "Livello 3"
    Me.f4.Enabled = True
    Me.f4.FieldName = "f4"
    Me.f4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.f4.Name = "f4"
    Me.f4.NTSRepositoryComboBox = Nothing
    Me.f4.NTSRepositoryItemCheck = Nothing
    Me.f4.NTSRepositoryItemMemo = Nothing
    Me.f4.NTSRepositoryItemText = Nothing
    Me.f4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.f4.OptionsFilter.AllowFilter = False
    Me.f4.Visible = True
    Me.f4.VisibleIndex = 3
    '
    'f5
    '
    Me.f5.AppearanceCell.Options.UseBackColor = True
    Me.f5.AppearanceCell.Options.UseTextOptions = True
    Me.f5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.f5.Caption = "Livello 4"
    Me.f5.Enabled = True
    Me.f5.FieldName = "f5"
    Me.f5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.f5.Name = "f5"
    Me.f5.NTSRepositoryComboBox = Nothing
    Me.f5.NTSRepositoryItemCheck = Nothing
    Me.f5.NTSRepositoryItemMemo = Nothing
    Me.f5.NTSRepositoryItemText = Nothing
    Me.f5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.f5.OptionsFilter.AllowFilter = False
    Me.f5.Visible = True
    Me.f5.VisibleIndex = 4
    '
    'f6
    '
    Me.f6.AppearanceCell.Options.UseBackColor = True
    Me.f6.AppearanceCell.Options.UseTextOptions = True
    Me.f6.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.f6.Caption = "Livello 5"
    Me.f6.Enabled = True
    Me.f6.FieldName = "f6"
    Me.f6.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.f6.Name = "f6"
    Me.f6.NTSRepositoryComboBox = Nothing
    Me.f6.NTSRepositoryItemCheck = Nothing
    Me.f6.NTSRepositoryItemMemo = Nothing
    Me.f6.NTSRepositoryItemText = Nothing
    Me.f6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.f6.OptionsFilter.AllowFilter = False
    Me.f6.Visible = True
    Me.f6.VisibleIndex = 5
    '
    'edCerca
    '
    Me.edCerca.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCerca.Location = New System.Drawing.Point(10, 86)
    Me.edCerca.Name = "edCerca"
    Me.edCerca.NTSDbField = ""
    Me.edCerca.NTSForzaVisZoom = False
    Me.edCerca.NTSOldValue = ""
    Me.edCerca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCerca.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCerca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCerca.Properties.MaxLength = 65536
    Me.edCerca.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCerca.Size = New System.Drawing.Size(68, 20)
    Me.edCerca.TabIndex = 6
    '
    'cmdCerca
    '
    Me.cmdCerca.ImageText = ""
    Me.cmdCerca.Location = New System.Drawing.Point(79, 86)
    Me.cmdCerca.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
    Me.cmdCerca.Name = "cmdCerca"
    Me.cmdCerca.Size = New System.Drawing.Size(49, 20)
    Me.cmdCerca.TabIndex = 7
    Me.cmdCerca.Text = "Cerca"
    '
    'FRM__HLCE
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(568, 340)
    Me.Controls.Add(Me.grZoom)
    Me.Controls.Add(Me.pnAction)
    Me.Name = "FRM__HLCE"
    Me.NTSLastControlFocussed = Me.grZoom
    Me.Text = "ZOOM CONTI BILANCIO"
    CType(Me.pnAction, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAction.ResumeLayout(False)
    CType(Me.fmSezione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmSezione.ResumeLayout(False)
    Me.fmSezione.PerformLayout()
    CType(Me.optContiDo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optContoEc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optPassivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optAttivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grZoom, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvZoom, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCerca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Me.Close()
  End Sub

  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    If Not grvZoom.NTSGetCurrentDataRow() Is Nothing Then
      oParam.strOut = grvZoom.GetFocusedRowCellDisplayText("f1")
    End If
    Me.Close()
  End Sub

  Public Overridable Sub Ricerca()
    Dim bNoSez As Boolean = False
    Try

      If optAttivo.Checked Then strTipoSezione = "A"
      If optPassivo.Checked Then strTipoSezione = "B"
      If optContoEc.Checked Then strTipoSezione = "C"
      If optContiDo.Checked Then strTipoSezione = "D"

      dsHlce.Clear()

      oCleHlce.Apri(DittaCorrente, strTipoBil, nCodTipoRicl, strTipoSezione, dsHlce, bNoSez)
      dcHlce.DataSource = dsHlce.Tables("SHEET")
      dsHlce.AcceptChanges()
      grZoom.DataSource = dcHlce

      If strTextForm = "" Then
        strTextForm = Me.Text
      End If

      If bNoSez Then fmSezione.Visible = False

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__HLCE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      If strTipoBil = "R" Then
        'Disabilita nel riclassificato i conti d'ordine
        optContiDo.Enabled = False
      End If

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      'e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipodocumento, ecc ...
      'GctlTipoDoc = ""
      GctlSetRoules()
      GctlApplicaDefaultValue()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__HLCE_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    '-------------------------------------------------
    'carico la griglia
    Ricerca()
  End Sub

  Public Overridable Sub grZoom_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grZoom.MouseDoubleClick
    cmdSeleziona_Click(Me, Nothing)
  End Sub


  Public Overridable Sub optAttivo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optAttivo.CheckedChanged
    Try
      '-------------------------------------------------
      'carico la griglia
      If optAttivo.Checked Then Ricerca()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub optPassivo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPassivo.CheckedChanged
    Try
      '-------------------------------------------------
      'carico la griglia
      If optPassivo.Checked Then Ricerca()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub optContoEc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optContoEc.CheckedChanged
    Try
      '-------------------------------------------------
      'carico la griglia
      If optContoEc.Checked Then Ricerca()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub optContiDo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optContiDo.CheckedChanged
    Try
      '-------------------------------------------------
      'carico la griglia
      If optContiDo.Checked Then Ricerca()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__HLCE_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Try
      If e.KeyCode = Keys.Enter Then
        If grvZoom.Focused And grvZoom.RowCount > 0 Then
          e.Handled = True
          cmdSeleziona_Click(Me, Nothing)
        End If
        Return
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdCerca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCerca.Click
    Try
      If edCerca.Text.Trim = "" Then
        dcHlce.Filter = ""
      Else
        If dsHlce.Tables("SHEET").Columns.Contains("pindirap") Then
          'notax
          dcHlce.Filter = "f2 like " & CStrSQL("%" & edCerca.Text.Trim & "%")
        Else
          dcHlce.Filter = dsHlce.Tables("SHEET").Columns(0).ColumnName & " like " & CStrSQL("%" & edCerca.Text.Trim & "%")
          If dsHlce.Tables("SHEET").Columns.Count > 0 Then dcHlce.Filter += " OR " & dsHlce.Tables("SHEET").Columns(1).ColumnName & " like " & CStrSQL("%" & edCerca.Text.Trim & "%")
          If dsHlce.Tables("SHEET").Columns.Count > 1 Then dcHlce.Filter += " OR " & dsHlce.Tables("SHEET").Columns(2).ColumnName & " like " & CStrSQL("%" & edCerca.Text.Trim & "%")
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
End Class
