Imports System.Windows.Forms
Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__GCRE
  Public oCleGctl As CLE__GCTL
  Public dsRe As DataSet
  Public dtcRe As BindingSource = New BindingSource()

  Public Overloads Function Init(ByRef Menu As CLE__MENU) As Boolean
    Try
      oMenu = Menu
      oApp = oMenu.App
      DittaCorrente = oApp.Ditta

      InitializeComponent()
      Me.MinimumSize = Me.Size

      '---------------------------------
      'creo e attivo l'entity
      oCleGctl = New CLE__GCTL
      bRemoting = Menu.Remoting("BN__GCRE", strRemoteServer, strRemotePort)
      oCleGctl.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort)

      '---------------------------------
      'inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
      AddHandler oCleGctl.RemoteEvent, AddressOf GestisciEventiEntity

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'completo le informazioni dei i controlli
      grvPC.NTSSetParam(oMenu, oApp.Tr(Me, 128230023395426272, "Griglia Personalizzazioni"))
      pc_pcname.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023395582445, "Nome computer"), 50)
      pc_dll.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023395738618, "Nome child"), 12)
      pc_nomprop.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023395894791, "Nome proprietà"), 0)
      pc_valprop.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023396050964, "Valore proprietà"), 0)

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
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.NtsGroupBox1 = New NTSInformatica.NTSGroupBox
    Me.optPrior4 = New NTSInformatica.NTSRadioButton
    Me.optPrior2 = New NTSInformatica.NTSRadioButton
    Me.optPrior3 = New NTSInformatica.NTSRadioButton
    Me.optPrior1 = New NTSInformatica.NTSRadioButton
    Me.NtsGroupBox3 = New NTSInformatica.NTSGroupBox
    Me.grPC = New NTSInformatica.NTSGrid
    Me.grvPC = New NTSInformatica.NTSGridView
    Me.pc_pcname = New NTSInformatica.NTSGridColumn
    Me.pc_dll = New NTSInformatica.NTSGridColumn
    Me.pc_nomprop = New NTSInformatica.NTSGridColumn
    Me.pc_valprop = New NTSInformatica.NTSGridColumn
    Me.cmdCancRow = New NTSInformatica.NTSButton
    Me.NtsLabel2 = New NTSInformatica.NTSLabel
    Me.NtsLabel3 = New NTSInformatica.NTSLabel
    Me.NtsLabel4 = New NTSInformatica.NTSLabel
    Me.NtsLabel5 = New NTSInformatica.NTSLabel
    Me.NtsLabel6 = New NTSInformatica.NTSLabel
    Me.cmdRipristina = New NTSInformatica.NTSButton
    CType(Me.NtsGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsGroupBox1.SuspendLayout()
    CType(Me.optPrior4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optPrior2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optPrior3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optPrior1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsGroupBox3.SuspendLayout()
    CType(Me.grPC, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvPC, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'cmdConferma
    '
    Me.cmdConferma.Location = New System.Drawing.Point(458, 112)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.Size = New System.Drawing.Size(76, 20)
    Me.cmdConferma.TabIndex = 2
    Me.cmdConferma.Text = "&Conferma"
    '
    'NtsGroupBox1
    '
    Me.NtsGroupBox1.AllowDrop = True
    Me.NtsGroupBox1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsGroupBox1.Appearance.Options.UseBackColor = True
    Me.NtsGroupBox1.Controls.Add(Me.optPrior4)
    Me.NtsGroupBox1.Controls.Add(Me.optPrior2)
    Me.NtsGroupBox1.Controls.Add(Me.optPrior3)
    Me.NtsGroupBox1.Controls.Add(Me.optPrior1)
    Me.NtsGroupBox1.Location = New System.Drawing.Point(10, 12)
    Me.NtsGroupBox1.Name = "NtsGroupBox1"
    Me.NtsGroupBox1.Size = New System.Drawing.Size(187, 120)
    Me.NtsGroupBox1.TabIndex = 0
    Me.NtsGroupBox1.Text = "(1) Priorità "
    '
    'optPrior4
    '
    Me.optPrior4.Location = New System.Drawing.Point(15, 98)
    Me.optPrior4.Name = "optPrior4"
    Me.optPrior4.NTSCheckValue = "S"
    Me.optPrior4.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optPrior4.Properties.Appearance.Options.UseBackColor = True
    Me.optPrior4.Properties.Caption = "4 - Generale"
    Me.optPrior4.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optPrior4.Size = New System.Drawing.Size(83, 19)
    Me.optPrior4.TabIndex = 3
    '
    'optPrior2
    '
    Me.optPrior2.Cursor = System.Windows.Forms.Cursors.Default
    Me.optPrior2.Location = New System.Drawing.Point(15, 50)
    Me.optPrior2.Name = "optPrior2"
    Me.optPrior2.NTSCheckValue = "S"
    Me.optPrior2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optPrior2.Properties.Appearance.Options.UseBackColor = True
    Me.optPrior2.Properties.Caption = "2 - Computer"
    Me.optPrior2.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optPrior2.Size = New System.Drawing.Size(85, 19)
    Me.optPrior2.TabIndex = 1
    '
    'optPrior3
    '
    Me.optPrior3.Location = New System.Drawing.Point(15, 74)
    Me.optPrior3.Name = "optPrior3"
    Me.optPrior3.NTSCheckValue = "S"
    Me.optPrior3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optPrior3.Properties.Appearance.Options.UseBackColor = True
    Me.optPrior3.Properties.Caption = "3 - Programma"
    Me.optPrior3.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optPrior3.Size = New System.Drawing.Size(93, 19)
    Me.optPrior3.TabIndex = 2
    '
    'optPrior1
    '
    Me.optPrior1.Cursor = System.Windows.Forms.Cursors.Default
    Me.optPrior1.EditValue = True
    Me.optPrior1.Location = New System.Drawing.Point(15, 26)
    Me.optPrior1.Name = "optPrior1"
    Me.optPrior1.NTSCheckValue = "S"
    Me.optPrior1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optPrior1.Properties.Appearance.Options.UseBackColor = True
    Me.optPrior1.Properties.Caption = "1 - Computer / programma"
    Me.optPrior1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optPrior1.Size = New System.Drawing.Size(148, 19)
    Me.optPrior1.TabIndex = 0
    '
    'NtsGroupBox3
    '
    Me.NtsGroupBox3.AllowDrop = True
    Me.NtsGroupBox3.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsGroupBox3.Appearance.Options.UseBackColor = True
    Me.NtsGroupBox3.Controls.Add(Me.grPC)
    Me.NtsGroupBox3.Location = New System.Drawing.Point(10, 138)
    Me.NtsGroupBox3.Name = "NtsGroupBox3"
    Me.NtsGroupBox3.Size = New System.Drawing.Size(526, 230)
    Me.NtsGroupBox3.TabIndex = 1
    Me.NtsGroupBox3.Text = "(2) Valori impostati                        Le proprietà devono essere tutte indi" & _
        "cate (magari con priorità diversa)"
    '
    'grPC
    '
    Me.grPC.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grPC.EmbeddedNavigator.Name = ""
    Me.grPC.Location = New System.Drawing.Point(2, 20)
    Me.grPC.MainView = Me.grvPC
    Me.grPC.Name = "grPC"
    Me.grPC.Size = New System.Drawing.Size(522, 208)
    Me.grPC.TabIndex = 1
    Me.grPC.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvPC})
    '
    'grvPC
    '
    Me.grvPC.ActiveFilterEnabled = False
    Me.grvPC.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.pc_pcname, Me.pc_dll, Me.pc_nomprop, Me.pc_valprop})
    Me.grvPC.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvPC.Enabled = True
    Me.grvPC.GridControl = Me.grPC
    Me.grvPC.Name = "grvPC"
    Me.grvPC.NTSAllowDelete = True
    Me.grvPC.NTSAllowInsert = True
    Me.grvPC.NTSAllowUpdate = True
    Me.grvPC.NTSMenuContext = Nothing
    Me.grvPC.OptionsCustomization.AllowRowSizing = True
    Me.grvPC.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvPC.OptionsNavigation.UseTabKey = False
    Me.grvPC.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvPC.OptionsView.ColumnAutoWidth = False
    Me.grvPC.OptionsView.EnableAppearanceEvenRow = True
    Me.grvPC.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvPC.OptionsView.ShowGroupPanel = False
    Me.grvPC.RowHeight = 16
    '
    'pc_pcname
    '
    Me.pc_pcname.AppearanceCell.Options.UseBackColor = True
    Me.pc_pcname.AppearanceCell.Options.UseTextOptions = True
    Me.pc_pcname.Caption = "Computer"
    Me.pc_pcname.Enabled = True
    Me.pc_pcname.FieldName = "pc_pcname"
    Me.pc_pcname.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pc_pcname.Name = "pc_pcname"
    Me.pc_pcname.NTSRepositoryComboBox = Nothing
    Me.pc_pcname.NTSRepositoryItemCheck = Nothing
    Me.pc_pcname.NTSRepositoryItemText = Nothing
    Me.pc_pcname.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pc_pcname.OptionsFilter.AllowFilter = False
    Me.pc_pcname.Visible = True
    Me.pc_pcname.VisibleIndex = 0
    Me.pc_pcname.Width = 126
    '
    'pc_dll
    '
    Me.pc_dll.AppearanceCell.Options.UseBackColor = True
    Me.pc_dll.AppearanceCell.Options.UseTextOptions = True
    Me.pc_dll.Caption = "Programma"
    Me.pc_dll.Enabled = True
    Me.pc_dll.FieldName = "pc_dll"
    Me.pc_dll.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pc_dll.Name = "pc_dll"
    Me.pc_dll.NTSRepositoryComboBox = Nothing
    Me.pc_dll.NTSRepositoryItemCheck = Nothing
    Me.pc_dll.NTSRepositoryItemText = Nothing
    Me.pc_dll.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pc_dll.OptionsFilter.AllowFilter = False
    Me.pc_dll.Visible = True
    Me.pc_dll.VisibleIndex = 1
    Me.pc_dll.Width = 121
    '
    'pc_nomprop
    '
    Me.pc_nomprop.AppearanceCell.Options.UseBackColor = True
    Me.pc_nomprop.AppearanceCell.Options.UseTextOptions = True
    Me.pc_nomprop.Caption = "Proprietà"
    Me.pc_nomprop.Enabled = True
    Me.pc_nomprop.FieldName = "pc_nomprop"
    Me.pc_nomprop.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pc_nomprop.Name = "pc_nomprop"
    Me.pc_nomprop.NTSRepositoryComboBox = Nothing
    Me.pc_nomprop.NTSRepositoryItemCheck = Nothing
    Me.pc_nomprop.NTSRepositoryItemText = Nothing
    Me.pc_nomprop.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pc_nomprop.OptionsFilter.AllowFilter = False
    Me.pc_nomprop.Visible = True
    Me.pc_nomprop.VisibleIndex = 2
    Me.pc_nomprop.Width = 120
    '
    'pc_valprop
    '
    Me.pc_valprop.AppearanceCell.Options.UseBackColor = True
    Me.pc_valprop.AppearanceCell.Options.UseTextOptions = True
    Me.pc_valprop.Caption = "Valore"
    Me.pc_valprop.Enabled = True
    Me.pc_valprop.FieldName = "pc_valprop"
    Me.pc_valprop.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pc_valprop.Name = "pc_valprop"
    Me.pc_valprop.NTSRepositoryComboBox = Nothing
    Me.pc_valprop.NTSRepositoryItemCheck = Nothing
    Me.pc_valprop.NTSRepositoryItemText = Nothing
    Me.pc_valprop.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pc_valprop.OptionsFilter.AllowFilter = False
    Me.pc_valprop.Visible = True
    Me.pc_valprop.VisibleIndex = 3
    Me.pc_valprop.Width = 130
    '
    'cmdCancRow
    '
    Me.cmdCancRow.Location = New System.Drawing.Point(357, 112)
    Me.cmdCancRow.Name = "cmdCancRow"
    Me.cmdCancRow.Size = New System.Drawing.Size(76, 20)
    Me.cmdCancRow.TabIndex = 3
    Me.cmdCancRow.TabStop = False
    Me.cmdCancRow.Text = "Cancella riga"
    '
    'NtsLabel2
    '
    Me.NtsLabel2.AutoSize = True
    Me.NtsLabel2.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel2.Location = New System.Drawing.Point(215, 12)
    Me.NtsLabel2.Name = "NtsLabel2"
    Me.NtsLabel2.Size = New System.Drawing.Size(148, 13)
    Me.NtsLabel2.TabIndex = 10
    Me.NtsLabel2.Text = "Le proprietà da settare sono:"
    '
    'NtsLabel3
    '
    Me.NtsLabel3.AutoSize = True
    Me.NtsLabel3.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel3.Location = New System.Drawing.Point(215, 25)
    Me.NtsLabel3.Name = "NtsLabel3"
    Me.NtsLabel3.Size = New System.Drawing.Size(423, 13)
    Me.NtsLabel3.TabIndex = 11
    Me.NtsLabel3.Text = "---------------------------------------------------------------------------------" & _
        "-----------------------"
    '
    'NtsLabel4
    '
    Me.NtsLabel4.AutoSize = True
    Me.NtsLabel4.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel4.Location = New System.Drawing.Point(215, 38)
    Me.NtsLabel4.Name = "NtsLabel4"
    Me.NtsLabel4.Size = New System.Drawing.Size(234, 13)
    Me.NtsLabel4.TabIndex = 12
    Me.NtsLabel4.Text = "- Remoting         (0 = disabilitato /-1 = abilitato)"
    '
    'NtsLabel5
    '
    Me.NtsLabel5.AutoSize = True
    Me.NtsLabel5.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel5.Location = New System.Drawing.Point(215, 51)
    Me.NtsLabel5.Name = "NtsLabel5"
    Me.NtsLabel5.Size = New System.Drawing.Size(321, 13)
    Me.NtsLabel5.TabIndex = 13
    Me.NtsLabel5.Text = "- RemoteServer (Computer su cui eseguire il progr. - BNORGSOR)"
    '
    'NtsLabel6
    '
    Me.NtsLabel6.AutoSize = True
    Me.NtsLabel6.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel6.Location = New System.Drawing.Point(215, 64)
    Me.NtsLabel6.Name = "NtsLabel6"
    Me.NtsLabel6.Size = New System.Drawing.Size(327, 13)
    Me.NtsLabel6.TabIndex = 14
    Me.NtsLabel6.Text = "- RemotePort     (Porta abilitata per l'esecuzione del progr.  - 1973)"
    '
    'cmdRipristina
    '
    Me.cmdRipristina.Location = New System.Drawing.Point(262, 112)
    Me.cmdRipristina.Name = "cmdRipristina"
    Me.cmdRipristina.Size = New System.Drawing.Size(76, 20)
    Me.cmdRipristina.TabIndex = 15
    Me.cmdRipristina.Text = "Ripristina"
    '
    'FRM__GCRE
    '
    Me.ClientSize = New System.Drawing.Size(539, 368)
    Me.Controls.Add(Me.cmdRipristina)
    Me.Controls.Add(Me.NtsLabel6)
    Me.Controls.Add(Me.NtsLabel5)
    Me.Controls.Add(Me.NtsLabel4)
    Me.Controls.Add(Me.NtsLabel3)
    Me.Controls.Add(Me.NtsLabel2)
    Me.Controls.Add(Me.cmdCancRow)
    Me.Controls.Add(Me.NtsGroupBox3)
    Me.Controls.Add(Me.NtsGroupBox1)
    Me.Controls.Add(Me.cmdConferma)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
    Me.Name = "FRM__GCRE"
    Me.NTSLastControlFocussed = Me.grPC
    Me.Text = "Gestione remoting"
    CType(Me.NtsGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsGroupBox1.ResumeLayout(False)
    Me.NtsGroupBox1.PerformLayout()
    CType(Me.optPrior4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optPrior2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optPrior3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optPrior1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsGroupBox3.ResumeLayout(False)
    CType(Me.grPC, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvPC, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Public Overridable Sub FRM__GCRE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()


      optPrior1_CheckedChanged(optPrior1, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub


  Public Overridable Sub GestisciGriglia(ByVal nPrior As Integer)
    Try

      If oCleGctl.RecordIsChanged Then
        If Not Salva() Then Return
      End If

      '------------------------------------
      'prima riabilito tutto
      pc_pcname.Enabled = True
      pc_dll.Enabled = True

      '------------------------------------
      'adesso devo bloccare le colonne che non posso gestire
      'e far vedere solo le righe interessate dal vincolo

      If optPrior4.Checked = True And nPrior = 4 Then            'generale
        pc_pcname.Enabled = False
        pc_dll.Enabled = False
      ElseIf optPrior3.Checked = True And nPrior = 3 Then        'programma
        pc_pcname.Enabled = False
      ElseIf optPrior2.Checked = True And nPrior = 2 Then        'computer
        pc_dll.Enabled = False
      ElseIf optPrior1.Checked = True And nPrior = 1 Then        'computer/programma
      Else
        nPrior = 0
      End If

      If nPrior = 0 Then Return

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      grPC.DataSource = Nothing
      Me.Cursor = Cursors.WaitCursor
      If Not oCleGctl.ApriRemoting(dsRe, nPrior) Then Me.Close()
      dtcRe.DataSource = dsRe.Tables("PCCONF")
      dsRe.AcceptChanges()

      grPC.DataSource = dtcRe

      GctlSetRoules()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub


  Public Overridable Sub optPrior1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPrior1.CheckedChanged
    GestisciGriglia(1)
  End Sub

  Public Overridable Sub optPrior2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPrior2.CheckedChanged
    GestisciGriglia(2)
  End Sub

  Public Overridable Sub optPrior3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPrior3.CheckedChanged
    GestisciGriglia(3)
  End Sub

  Public Overridable Sub optPrior4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPrior4.CheckedChanged
    GestisciGriglia(4)
  End Sub

  Public Overridable Sub cmdCancRow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancRow.Click
    Try
      If Not grvPC.NTSDeleteRigaCorrente(dtcRe, True) Then Return
      oCleGctl.SalvaRemoting(True, 0)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__GCRE_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub grvPC_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvPC.NTSBeforeRowUpdate
    Try
      If Not Salva() Then
        'rimango sulla riga su cui sono
        e.Allow = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub


  Public Overridable Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    If Not Salva() Then Return
    Me.Close()
  End Sub

  Public Overridable Function Salva() As Boolean
    Dim dRes As DialogResult
    Dim nPriority As Integer = 0
    Try
      If optPrior1.Checked = True Then
        nPriority = 1
      ElseIf optPrior2.Checked = True Then
        nPriority = 2
      ElseIf optPrior3.Checked = True Then
        nPriority = 3
      ElseIf optPrior4.Checked = True Then
        nPriority = 4
      End If

      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      dRes = grvPC.NTSSalvaRigaCorrente(dtcRe, oCleGctl.RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          If Not oCleGctl.SalvaRemoting(False, nPriority) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleGctl.RipristinaRemoting(dtcRe.Position, dtcRe.Filter, "")
        Case System.Windows.Forms.DialogResult.Cancel
          'non posso fare nulla
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          'la riga non ha subito modifiche
      End Select
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub cmdRipristina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRipristina.Click
    Try
      If Not grvPC.NTSRipristinaRigaCorrenteBefore(dtcRe, True) Then Return
      oCleGctl.RipristinaRemoting(dtcRe.Position, dtcRe.Filter, "")
      grvPC.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__GCRE_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    'salvo l'impostazione della griglia. devo farlo anche al cambio del tipo documento (ad esempio in bsveboll
    Try
      dtcRe.Dispose()
      dsRe.Dispose()
    Catch
    End Try
  End Sub

End Class
