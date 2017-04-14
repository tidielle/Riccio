Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__HSER

  Public oCleHser As CLE__HSER
  Public oParam As CLE__PATB       'parametri passati dal child che mi ha chiamato: sempre tramite questa classe gli restituisceo il valore
  Public dsHser As New DataSet
  Public dcHser As BindingSource = New BindingSource()
  Public bSelectIfOneRow As Boolean = False

  Private components As System.ComponentModel.IContainer
  Public WithEvents pnAction As NTSInformatica.NTSPanel
  Public WithEvents pnDescr As NTSInformatica.NTSPanel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents cmdRicerca As NTSInformatica.NTSButton
  Public WithEvents grZoom As NTSInformatica.NTSGrid
  Public WithEvents grvZoom As NTSInformatica.NTSGridView
  Public WithEvents tb_numserie As NTSInformatica.NTSGridColumn
  Public WithEvents tb_note As NTSInformatica.NTSGridColumn
  Public WithEvents edAnno As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTipork As NTSInformatica.NTSLabel
  Public WithEvents lbAnno As NTSInformatica.NTSLabel
  Public WithEvents cbTipo As NTSInformatica.NTSComboBox

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

    InitializeComponent()
    Me.MinimumSize = Me.Size

    Me.MinimizeBox = False

    '------------------------------------------------
    'creo e attivo l'entity
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__HSER", "BE__HSER", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 127791222144843750, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleHser = CType(oTmp, CLE__HSER)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__HSER", strRemoteServer, strRemotePort)
    oCleHser.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort)

    '---------------------------------
    'inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    AddHandler oCleHser.RemoteEvent, AddressOf GestisciEventiEntity

    bSelectIfOneRow = oParam.bFlag1
    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'completo le informazioni dei i controlli
      edAnno.NTSSetParam(oMenu, oApp.Tr(Me, 128230023157106274, "Anno"), "0")
      cbTipo.NTSSetParam(oApp.Tr(Me, 130512680172277293, "Tipo"))

      grvZoom.NTSSetParam(oMenu, oApp.Tr(Me, 130512680123056968, "Griglia Zoom Serie"))
      tb_numserie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130512680140401273, "Serie"), 0)
      tb_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130512680157276813, "Note"), 0)

      grvZoom.NTSAllowDelete = False
      grvZoom.NTSAllowUpdate = False
      grvZoom.NTSAllowInsert = False
      grvZoom.Enabled = False

      If oParam.strDescr <> "" Then cbTipo.SelectedValue = oParam.strDescr
      If oParam.lContoCF <> 0 Then edAnno.Text = NTSCStr(oParam.lContoCF)

      edAnno.Enabled = False
      cbTipo.Enabled = False

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

  Public Overridable Sub CaricaCombo()
    Dim dttTipo As New DataTable()
    Try
      dttTipo.Columns.Add("cod", GetType(String))
      dttTipo.Columns.Add("val", GetType(String))
      'veboll
      dttTipo.Rows.Add(New Object() {"A", "Fattura Imm. emessa"})
      dttTipo.Rows.Add(New Object() {"B", "DDT emesso"})
      dttTipo.Rows.Add(New Object() {"C", "Corrispettivo emesso"})
      dttTipo.Rows.Add(New Object() {"E", "Nota di Addeb. emessa"})
      dttTipo.Rows.Add(New Object() {"F", "Ric.Fiscale Emessa"})
      dttTipo.Rows.Add(New Object() {"I", "Riemissione Ric.Fiscali"})
      dttTipo.Rows.Add(New Object() {"J", "Nota Accr. ricevuta"})
      dttTipo.Rows.Add(New Object() {"L", "Fattura Imm. ricevuta"})
      dttTipo.Rows.Add(New Object() {"M", "DDT ricevuto"})
      dttTipo.Rows.Add(New Object() {"N", "Nota Accr. emessa"})
      dttTipo.Rows.Add(New Object() {"S", "Fatt.Ric.Fisc. Emessa"})
      dttTipo.Rows.Add(New Object() {"T", "Carico da produzione"})
      dttTipo.Rows.Add(New Object() {"W", "Nota di prelievo"})
      dttTipo.Rows.Add(New Object() {"Z", "Bolla di mov. interna"})
      'vefdin
      dttTipo.Rows.Add(New Object() {"D", "Fattura Differita Emessa"})
      dttTipo.Rows.Add(New Object() {"K", "Fattura Differita Ricevuta"})
      dttTipo.Rows.Add(New Object() {"P", "Fattura/Ricevuta Fiscale Differita"})
      dttTipo.Rows.Add(New Object() {"£", "Nota di Accredito Differita Emessa"})
      dttTipo.Rows.Add(New Object() {"(", "Nota di Accredito Differita Ricevuta"})
      'orgsor
      dttTipo.Rows.Add(New Object() {"R", "Impegno cliente"})
      dttTipo.Rows.Add(New Object() {"O", "Ordine fornitore"})
      dttTipo.Rows.Add(New Object() {"H", "Ordine di produzione"})
      dttTipo.Rows.Add(New Object() {"X", "Impegno Trasferimento"})
      dttTipo.Rows.Add(New Object() {"Q", "Preventivo"})
      dttTipo.Rows.Add(New Object() {"#", "Impegno di commessa"})
      dttTipo.Rows.Add(New Object() {"V", "Impegno cliente aperto"})
      dttTipo.Rows.Add(New Object() {"$", "Ordine fornitore aperto"})
      dttTipo.AcceptChanges()
      cbTipo.DataSource = dttTipo
      cbTipo.ValueMember = "cod"
      cbTipo.DisplayMember = "val"

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub InitializeComponent()
    Me.pnAction = New NTSInformatica.NTSPanel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.cmdRicerca = New NTSInformatica.NTSButton
    Me.pnDescr = New NTSInformatica.NTSPanel
    Me.lbTipork = New NTSInformatica.NTSLabel
    Me.edAnno = New NTSInformatica.NTSTextBoxNum
    Me.lbAnno = New NTSInformatica.NTSLabel
    Me.grZoom = New NTSInformatica.NTSGrid
    Me.grvZoom = New NTSInformatica.NTSGridView
    Me.tb_numserie = New NTSInformatica.NTSGridColumn
    Me.tb_note = New NTSInformatica.NTSGridColumn
    Me.cbTipo = New NTSInformatica.NTSComboBox
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnAction, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAction.SuspendLayout()
    CType(Me.pnDescr, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDescr.SuspendLayout()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grZoom, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvZoom, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.pnAction.Controls.Add(Me.cmdRicerca)
    Me.pnAction.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnAction.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnAction.Location = New System.Drawing.Point(483, 0)
    Me.pnAction.Name = "pnAction"
    Me.pnAction.NTSActiveTrasparency = True
    Me.pnAction.Size = New System.Drawing.Size(110, 340)
    Me.pnAction.TabIndex = 2
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(9, 63)
    Me.cmdAnnulla.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(88, 22)
    Me.cmdAnnulla.TabIndex = 3
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(9, 38)
    Me.cmdSeleziona.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.NTSContextMenu = Nothing
    Me.cmdSeleziona.Size = New System.Drawing.Size(88, 22)
    Me.cmdSeleziona.TabIndex = 1
    Me.cmdSeleziona.Text = "&Seleziona"
    '
    'cmdRicerca
    '
    Me.cmdRicerca.ImageText = ""
    Me.cmdRicerca.Location = New System.Drawing.Point(9, 12)
    Me.cmdRicerca.Margin = New System.Windows.Forms.Padding(3, 3, 3, 2)
    Me.cmdRicerca.Name = "cmdRicerca"
    Me.cmdRicerca.NTSContextMenu = Nothing
    Me.cmdRicerca.Size = New System.Drawing.Size(88, 22)
    Me.cmdRicerca.TabIndex = 0
    Me.cmdRicerca.Text = "&Ricerca"
    '
    'pnDescr
    '
    Me.pnDescr.AllowDrop = True
    Me.pnDescr.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnDescr.Appearance.Options.UseBackColor = True
    Me.pnDescr.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnDescr.Controls.Add(Me.cbTipo)
    Me.pnDescr.Controls.Add(Me.lbTipork)
    Me.pnDescr.Controls.Add(Me.edAnno)
    Me.pnDescr.Controls.Add(Me.lbAnno)
    Me.pnDescr.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnDescr.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnDescr.Location = New System.Drawing.Point(0, 0)
    Me.pnDescr.Name = "pnDescr"
    Me.pnDescr.NTSActiveTrasparency = True
    Me.pnDescr.Size = New System.Drawing.Size(483, 42)
    Me.pnDescr.TabIndex = 1
    '
    'lbTipork
    '
    Me.lbTipork.AutoSize = True
    Me.lbTipork.BackColor = System.Drawing.Color.Transparent
    Me.lbTipork.Location = New System.Drawing.Point(12, 12)
    Me.lbTipork.Name = "lbTipork"
    Me.lbTipork.NTSDbField = ""
    Me.lbTipork.Size = New System.Drawing.Size(27, 13)
    Me.lbTipork.TabIndex = 4
    Me.lbTipork.Text = "Tipo"
    Me.lbTipork.Tooltip = ""
    Me.lbTipork.UseMnemonic = False
    '
    'edAnno
    '
    Me.edAnno.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAnno.EditValue = "0"
    Me.edAnno.Location = New System.Drawing.Point(235, 9)
    Me.edAnno.Name = "edAnno"
    Me.edAnno.NTSDbField = ""
    Me.edAnno.NTSFormat = "0"
    Me.edAnno.NTSForzaVisZoom = False
    Me.edAnno.NTSOldValue = ""
    Me.edAnno.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAnno.Properties.Appearance.Options.UseBackColor = True
    Me.edAnno.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnno.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnno.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnno.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnno.Properties.AutoHeight = False
    Me.edAnno.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnno.Properties.MaxLength = 65536
    Me.edAnno.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnno.Size = New System.Drawing.Size(71, 20)
    Me.edAnno.TabIndex = 3
    Me.edAnno.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edAnno.TextInt = 0
    '
    'lbAnno
    '
    Me.lbAnno.AutoSize = True
    Me.lbAnno.BackColor = System.Drawing.Color.Transparent
    Me.lbAnno.Location = New System.Drawing.Point(197, 12)
    Me.lbAnno.Name = "lbAnno"
    Me.lbAnno.NTSDbField = ""
    Me.lbAnno.Size = New System.Drawing.Size(32, 13)
    Me.lbAnno.TabIndex = 2
    Me.lbAnno.Text = "Anno"
    Me.lbAnno.Tooltip = ""
    Me.lbAnno.UseMnemonic = False
    '
    'grZoom
    '
    Me.grZoom.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grZoom.EmbeddedNavigator.Name = ""
    Me.grZoom.Location = New System.Drawing.Point(0, 42)
    Me.grZoom.MainView = Me.grvZoom
    Me.grZoom.Name = "grZoom"
    Me.grZoom.Size = New System.Drawing.Size(483, 298)
    Me.grZoom.TabIndex = 3
    Me.grZoom.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvZoom})
    '
    'grvZoom
    '
    Me.grvZoom.ActiveFilterEnabled = False
    Me.grvZoom.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tb_numserie, Me.tb_note})
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
    'tb_numserie
    '
    Me.tb_numserie.AppearanceCell.Options.UseBackColor = True
    Me.tb_numserie.AppearanceCell.Options.UseTextOptions = True
    Me.tb_numserie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_numserie.Caption = "Serie"
    Me.tb_numserie.Enabled = True
    Me.tb_numserie.FieldName = "tb_numserie"
    Me.tb_numserie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_numserie.Name = "tb_numserie"
    Me.tb_numserie.NTSRepositoryComboBox = Nothing
    Me.tb_numserie.NTSRepositoryItemCheck = Nothing
    Me.tb_numserie.NTSRepositoryItemMemo = Nothing
    Me.tb_numserie.NTSRepositoryItemText = Nothing
    Me.tb_numserie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_numserie.OptionsFilter.AllowFilter = False
    Me.tb_numserie.Visible = True
    Me.tb_numserie.VisibleIndex = 0
    Me.tb_numserie.Width = 94
    '
    'tb_note
    '
    Me.tb_note.AppearanceCell.Options.UseBackColor = True
    Me.tb_note.AppearanceCell.Options.UseTextOptions = True
    Me.tb_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_note.Caption = "Note"
    Me.tb_note.Enabled = True
    Me.tb_note.FieldName = "tb_note"
    Me.tb_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_note.Name = "tb_note"
    Me.tb_note.NTSRepositoryComboBox = Nothing
    Me.tb_note.NTSRepositoryItemCheck = Nothing
    Me.tb_note.NTSRepositoryItemMemo = Nothing
    Me.tb_note.NTSRepositoryItemText = Nothing
    Me.tb_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_note.OptionsFilter.AllowFilter = False
    Me.tb_note.Visible = True
    Me.tb_note.VisibleIndex = 1
    '
    'cbTipo
    '
    Me.cbTipo.DataSource = Nothing
    Me.cbTipo.DisplayMember = ""
    Me.cbTipo.Location = New System.Drawing.Point(49, 9)
    Me.cbTipo.Name = "cbTipo"
    Me.cbTipo.NTSDbField = ""
    Me.cbTipo.Properties.AutoHeight = False
    Me.cbTipo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipo.Properties.DropDownRows = 30
    Me.cbTipo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipo.SelectedValue = ""
    Me.cbTipo.Size = New System.Drawing.Size(142, 20)
    Me.cbTipo.TabIndex = 5
    Me.cbTipo.ValueMember = ""
    '
    'FRM__HSER
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.CancelButton = Me.cmdAnnulla
    Me.ClientSize = New System.Drawing.Size(593, 340)
    Me.Controls.Add(Me.grZoom)
    Me.Controls.Add(Me.pnDescr)
    Me.Controls.Add(Me.pnAction)
    Me.Name = "FRM__HSER"
    Me.NTSLastControlFocussed = Me.grZoom
    Me.Text = "ZOOM SERIE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnAction, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAction.ResumeLayout(False)
    CType(Me.pnDescr, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDescr.ResumeLayout(False)
    Me.pnDescr.PerformLayout()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grZoom, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvZoom, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    'oParam.Ditta = DittaCorrente
    Me.Close()
  End Sub

  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Try
      If dsHser Is Nothing Then Return
      If dsHser.Tables("TABNUMA") Is Nothing Then Return
      If dsHser.Tables("TABNUMA").Rows.Count > 0 Then
        oParam.strOut = grvZoom.GetFocusedRowCellDisplayText("tb_numserie")
      End If
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdRicerca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRicerca.Click
    Dim strTipork As String = ""
    Try
      dsHser.Clear()

      strTipork = NTSCStr(cbTipo.SelectedValue)
      Select Case strTipork
        Case "A", "D", "E", "N", "£"
          strTipork = "A"
        Case "L", "K", "J", "("
          strTipork = "L"
        Case "F", "I"
          strTipork = "F"
      End Select

      oCleHser.Apri(dsHser, DittaCorrente, edAnno.Text, strTipork)
      dcHser.DataSource = dsHser.Tables("TABNUMA")
      dsHser.AcceptChanges()
      grZoom.DataSource = dcHser

      'mi sposto sulla griglia e faccio in modo che con enter venga selezionato il valore
      If grvZoom.RowCount > 0 Then grZoom.Focus()

      'se è impostato di selezionare subito l'unica riga restituita lo faccio
      If bSelectIfOneRow And grvZoom.RowCount = 1 Then
        oParam.strOut = grvZoom.GetFocusedRowCellDisplayText("tb_numserie")
        Me.Cursor = Cursors.Default
        Me.Close()
      End If

      bSelectIfOneRow = False     'dopo la prima ricerca lanciata dal clienti con la ',' finale faccio in modo che il flag 'ottimistico' torni a funzionare

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__HSER_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      CaricaCombo()
      InitControls()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      'e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipodocumento, ecc ...
      'GctlTipoDoc = ""
      GctlSetRoules()
      GctlApplicaDefaultValue()

      cmdRicerca_Click(Me, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__HSER_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Try
      '--------------------------------------------
      'gestione dello zoom:
      'eseguo la Zoom, tanto se per il controllo non deve venir eseguito uno zoom particolare, la routine non fa nulla e viene processato lo zoom standard del controllo, settato con la NTSSetParamZoom
      If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
        Zoom()
        e.Handled = True    'altrimenti anche il controllo riceve l'F5 e la routine ZOOM viene eseguita 2 volte!!!
      End If

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

  Public Overridable Sub grZoom_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grZoom.MouseDoubleClick
    cmdSeleziona_Click(Me, Nothing)
  End Sub

  Public Overridable Sub Zoom()
    Dim ctrlTmp As Control = Nothing
    Dim oParam As New CLE__PATB
    Try
      '------------------------------------
      'zoom standard
      NTSCallStandardZoom()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
End Class
