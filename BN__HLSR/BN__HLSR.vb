Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__HLSR
  Public ocleHlsr As CLE__HLSR
  Public oParam As CLE__PATB       'parametri passati dal child che mi ha chiamato: sempre tramite questa classe gli restituisceo il valore

  Public dsHlsr As New DataSet
  Public dcHlsr As BindingSource = New BindingSource()

#Region "Dichiarazione Controlli"
  Public WithEvents pnAction As NTSInformatica.NTSPanel
  Public WithEvents pnDescr As NTSInformatica.NTSPanel
  Public WithEvents edDescr As NTSInformatica.NTSTextBoxStr
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents cmdRicerca As NTSInformatica.NTSButton
  Public WithEvents ckOttimistico As NTSInformatica.NTSCheckBox
  Public WithEvents grZoom As NTSInformatica.NTSGrid
  Public WithEvents grvZoom As NTSInformatica.NTSGridView

  Public WithEvents pn_conto As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descr1 As NTSInformatica.NTSGridColumn

  Public WithEvents edDatini As NTSInformatica.NTSTextBoxData
  Public WithEvents lbDescr As NTSInformatica.NTSLabel
#End Region

#Region "Inizializzazione"
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__HLSR", "BE__HLSR", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 127791222144843750, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    ocleHlsr = CType(oTmp, CLE__HLSR)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__HLSR", strRemoteServer, strRemotePort)
    ocleHlsr.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort)

    '---------------------------------
    'inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    AddHandler ocleHlsr.RemoteEvent, AddressOf GestisciEventiEntity

    If oParam.strDescr <> "" Then edDescr.Text = oParam.strDescr
    edDatini.Text = DateAdd("yyyy", -1, DateTime.Now).ToShortDateString
    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.pnAction = New NTSInformatica.NTSPanel
    Me.ckOttimistico = New NTSInformatica.NTSCheckBox
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.cmdRicerca = New NTSInformatica.NTSButton
    Me.pnDescr = New NTSInformatica.NTSPanel
    Me.lbDatini = New NTSInformatica.NTSLabel
    Me.lbDescr = New NTSInformatica.NTSLabel
    Me.edDatini = New NTSInformatica.NTSTextBoxData
    Me.edDescr = New NTSInformatica.NTSTextBoxStr
    Me.grZoom = New NTSInformatica.NTSGrid
    Me.grvZoom = New NTSInformatica.NTSGridView
    Me.pn_conto = New NTSInformatica.NTSGridColumn
    Me.xx_descr1 = New NTSInformatica.NTSGridColumn
    Me.lbConto = New NTSInformatica.NTSLabel
    CType(Me.pnAction, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAction.SuspendLayout()
    CType(Me.ckOttimistico.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnDescr, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDescr.SuspendLayout()
    CType(Me.edDatini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDescr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grZoom, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvZoom, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.pnAction.Controls.Add(Me.ckOttimistico)
    Me.pnAction.Controls.Add(Me.cmdAnnulla)
    Me.pnAction.Controls.Add(Me.cmdSeleziona)
    Me.pnAction.Controls.Add(Me.cmdRicerca)
    Me.pnAction.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnAction.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnAction.Location = New System.Drawing.Point(571, 0)
    Me.pnAction.Name = "pnAction"
    Me.pnAction.NTSActiveTrasparency = True
    Me.pnAction.Size = New System.Drawing.Size(138, 359)
    Me.pnAction.TabIndex = 2
    '
    'ckOttimistico
    '
    Me.ckOttimistico.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckOttimistico.Location = New System.Drawing.Point(8, 104)
    Me.ckOttimistico.Name = "ckOttimistico"
    Me.ckOttimistico.NTSCheckValue = "S"
    Me.ckOttimistico.NTSUnCheckValue = "N"
    Me.ckOttimistico.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckOttimistico.Properties.Appearance.Options.UseBackColor = True
    Me.ckOttimistico.Properties.AutoHeight = False
    Me.ckOttimistico.Properties.Caption = "&Ottimistico"
    Me.ckOttimistico.Size = New System.Drawing.Size(88, 19)
    Me.ckOttimistico.TabIndex = 4
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(8, 67)
    Me.cmdAnnulla.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(122, 22)
    Me.cmdAnnulla.TabIndex = 3
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(7, 38)
    Me.cmdSeleziona.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.NTSContextMenu = Nothing
    Me.cmdSeleziona.Size = New System.Drawing.Size(122, 22)
    Me.cmdSeleziona.TabIndex = 1
    Me.cmdSeleziona.Text = "&Seleziona"
    '
    'cmdRicerca
    '
    Me.cmdRicerca.ImageText = ""
    Me.cmdRicerca.Location = New System.Drawing.Point(7, 12)
    Me.cmdRicerca.Margin = New System.Windows.Forms.Padding(3, 3, 3, 2)
    Me.cmdRicerca.Name = "cmdRicerca"
    Me.cmdRicerca.NTSContextMenu = Nothing
    Me.cmdRicerca.Size = New System.Drawing.Size(122, 22)
    Me.cmdRicerca.TabIndex = 0
    Me.cmdRicerca.Text = "&Ricerca"
    '
    'pnDescr
    '
    Me.pnDescr.AllowDrop = True
    Me.pnDescr.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnDescr.Appearance.Options.UseBackColor = True
    Me.pnDescr.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnDescr.Controls.Add(Me.lbConto)
    Me.pnDescr.Controls.Add(Me.lbDatini)
    Me.pnDescr.Controls.Add(Me.lbDescr)
    Me.pnDescr.Controls.Add(Me.edDatini)
    Me.pnDescr.Controls.Add(Me.edDescr)
    Me.pnDescr.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnDescr.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnDescr.Location = New System.Drawing.Point(0, 0)
    Me.pnDescr.Name = "pnDescr"
    Me.pnDescr.NTSActiveTrasparency = True
    Me.pnDescr.Size = New System.Drawing.Size(571, 54)
    Me.pnDescr.TabIndex = 1
    '
    'lbDatini
    '
    Me.lbDatini.AutoSize = True
    Me.lbDatini.BackColor = System.Drawing.Color.Transparent
    Me.lbDatini.Location = New System.Drawing.Point(409, 31)
    Me.lbDatini.Name = "lbDatini"
    Me.lbDatini.NTSDbField = ""
    Me.lbDatini.Size = New System.Drawing.Size(55, 13)
    Me.lbDatini.TabIndex = 5
    Me.lbDatini.Text = "Dalla data"
    Me.lbDatini.Tooltip = ""
    Me.lbDatini.UseMnemonic = False
    '
    'lbDescr
    '
    Me.lbDescr.AutoSize = True
    Me.lbDescr.BackColor = System.Drawing.Color.Transparent
    Me.lbDescr.Location = New System.Drawing.Point(12, 31)
    Me.lbDescr.Name = "lbDescr"
    Me.lbDescr.NTSDbField = ""
    Me.lbDescr.Size = New System.Drawing.Size(61, 13)
    Me.lbDescr.TabIndex = 4
    Me.lbDescr.Text = "Descrizione"
    Me.lbDescr.Tooltip = ""
    Me.lbDescr.UseMnemonic = False
    '
    'edDatini
    '
    Me.edDatini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatini.EditValue = "09/07/2006"
    Me.edDatini.Location = New System.Drawing.Point(474, 28)
    Me.edDatini.Name = "edDatini"
    Me.edDatini.NTSDbField = ""
    Me.edDatini.NTSForzaVisZoom = False
    Me.edDatini.NTSOldValue = ""
    Me.edDatini.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDatini.Properties.Appearance.Options.UseBackColor = True
    Me.edDatini.Properties.Appearance.Options.UseTextOptions = True
    Me.edDatini.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDatini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatini.Properties.AutoHeight = False
    Me.edDatini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatini.Properties.MaxLength = 65536
    Me.edDatini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatini.Size = New System.Drawing.Size(89, 20)
    Me.edDatini.TabIndex = 3
    '
    'edDescr
    '
    Me.edDescr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDescr.Location = New System.Drawing.Point(104, 28)
    Me.edDescr.Name = "edDescr"
    Me.edDescr.NTSDbField = ""
    Me.edDescr.NTSForzaVisZoom = False
    Me.edDescr.NTSOldValue = ""
    Me.edDescr.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDescr.Properties.Appearance.Options.UseBackColor = True
    Me.edDescr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDescr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDescr.Properties.AutoHeight = False
    Me.edDescr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDescr.Properties.MaxLength = 65536
    Me.edDescr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDescr.Size = New System.Drawing.Size(299, 20)
    Me.edDescr.TabIndex = 1
    '
    'grZoom
    '
    Me.grZoom.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grZoom.EmbeddedNavigator.Name = ""
    Me.grZoom.Location = New System.Drawing.Point(0, 54)
    Me.grZoom.MainView = Me.grvZoom
    Me.grZoom.Name = "grZoom"
    Me.grZoom.Size = New System.Drawing.Size(571, 305)
    Me.grZoom.TabIndex = 3
    Me.grZoom.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvZoom})
    '
    'grvZoom
    '
    Me.grvZoom.ActiveFilterEnabled = False
    Me.grvZoom.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.pn_conto, Me.xx_descr1})
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
    'pn_conto
    '
    Me.pn_conto.AppearanceCell.Options.UseBackColor = True
    Me.pn_conto.AppearanceCell.Options.UseTextOptions = True
    Me.pn_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pn_conto.Caption = "Codice"
    Me.pn_conto.Enabled = True
    Me.pn_conto.FieldName = "pn_conto"
    Me.pn_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pn_conto.Name = "pn_conto"
    Me.pn_conto.NTSRepositoryComboBox = Nothing
    Me.pn_conto.NTSRepositoryItemCheck = Nothing
    Me.pn_conto.NTSRepositoryItemMemo = Nothing
    Me.pn_conto.NTSRepositoryItemText = Nothing
    Me.pn_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pn_conto.OptionsFilter.AllowFilter = False
    Me.pn_conto.Visible = True
    Me.pn_conto.VisibleIndex = 0
    Me.pn_conto.Width = 66
    '
    'xx_descr1
    '
    Me.xx_descr1.AppearanceCell.Options.UseBackColor = True
    Me.xx_descr1.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descr1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descr1.Caption = "Descrizione"
    Me.xx_descr1.Enabled = True
    Me.xx_descr1.FieldName = "xx_descr1"
    Me.xx_descr1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descr1.Name = "xx_descr1"
    Me.xx_descr1.NTSRepositoryComboBox = Nothing
    Me.xx_descr1.NTSRepositoryItemCheck = Nothing
    Me.xx_descr1.NTSRepositoryItemMemo = Nothing
    Me.xx_descr1.NTSRepositoryItemText = Nothing
    Me.xx_descr1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descr1.OptionsFilter.AllowFilter = False
    Me.xx_descr1.Visible = True
    Me.xx_descr1.VisibleIndex = 1
    Me.xx_descr1.Width = 70
    '
    'lbConto
    '
    Me.lbConto.AutoSize = True
    Me.lbConto.BackColor = System.Drawing.Color.Transparent
    Me.lbConto.Location = New System.Drawing.Point(12, 9)
    Me.lbConto.Name = "lbConto"
    Me.lbConto.NTSDbField = ""
    Me.lbConto.Size = New System.Drawing.Size(88, 13)
    Me.lbConto.TabIndex = 6
    Me.lbConto.Text = "Cliente/Fornitore"
    Me.lbConto.Tooltip = ""
    Me.lbConto.UseMnemonic = False
    '
    'FRM__HLSR
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.CancelButton = Me.cmdAnnulla
    Me.ClientSize = New System.Drawing.Size(709, 359)
    Me.Controls.Add(Me.grZoom)
    Me.Controls.Add(Me.pnDescr)
    Me.Controls.Add(Me.pnAction)
    Me.Name = "FRM__HLSR"
    Me.NTSLastControlFocussed = Me.grZoom
    Me.Text = "ZOOM SOTTOCONTI RECENTI"
    CType(Me.pnAction, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAction.ResumeLayout(False)
    Me.pnAction.PerformLayout()
    CType(Me.ckOttimistico.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnDescr, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDescr.ResumeLayout(False)
    Me.pnDescr.PerformLayout()
    CType(Me.edDatini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDescr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grZoom, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvZoom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'completo le informazioni dei i controlli
      edDatini.NTSSetParam(oMenu, oApp.Tr(Me, 128230023466953506, "Data inizio ricarca"), True)
      edDescr.NTSSetParam(oMenu, oApp.Tr(Me, 128230023467109679, "Descrizione conto"), 50, True)
      ckOttimistico.NTSSetParam(oMenu, oApp.Tr(Me, 128230023467265852, "Ottimistico"), "S", "N")

      grvZoom.NTSSetParam(oMenu, oApp.Tr(Me, 128230023467422025, "Griglia Zoom sottoconti recenti"))
      grvZoom.NTSAllowDelete = False
      grvZoom.NTSAllowUpdate = False
      grvZoom.NTSAllowInsert = False
      grvZoom.Enabled = False
      pn_conto.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128230023467578198, "Codice"), tabanagras)
      xx_descr1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023467734371, "Descrizione"), 50)
      pn_conto.NTSSetParamZoom("")

      '-------------------------------------------------
      'setto il recent
      ckOttimistico.Checked = CBool(oMenu.GetSettingBus("BN__HLSR", "RECENT", ".", "Ottimistico", "0", " ", "0"))

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
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRM__HLAB_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
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

  Public Overridable Sub FRM__HLSR_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      lbConto.Text = "(QUALSIASI CONTO)"
      '--------------------------------------------------------------------------------------------------------------
      '--- Sottoconti o Codici Iva utilizzati dal Conto di testata
      '--------------------------------------------------------------------------------------------------------------
      Select Case oParam.strTipo
        Case "A", "I"
          If oParam.lContoCF <> 0 Then
            If oMenu.ValCodiceDb(oParam.lContoCF.ToString, DittaCorrente, "ANAGRA", "N", "", dttTmp) = True Then
              If dttTmp.Rows.Count > 0 Then
                With dttTmp.Rows(0)
                  lbConto.Text = IIf(NTSCStr(!an_tipo) = "C", "CLIENTE: ", "FORNITORE: ").ToString & _
                    oParam.lContoCF.ToString & _
                    IIf(NTSCStr(!an_descr1).Trim <> "", " - ", "").ToString & NTSCStr(!an_descr1).Trim
                End With
              End If
            End If
          End If
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Sub

  Public Overridable Sub FRM__HLAB_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Try
      If e.KeyCode = Keys.Enter Then
        If grvZoom.Focused And grvZoom.RowCount > 0 Then
          e.Handled = True
          cmdSeleziona_Click(Me, Nothing)
        End If
        If edDescr.Focused Then
          cmdRicerca_Click(Me, Nothing)

          '---------------------------------------------------
          'faccio in modo che la pressione dell'enter non scateni l'emulazione del tasto TAB
          Me.NTSKeyDownEnterNotEmulateTabNow()
        End If

        Return
      End If

      If e.KeyValue > 40 And e.KeyValue < 127 And e.Alt = False And e.Control = False And e.Shift = False And grvZoom.Focused = True Then
        edDescr.Focus()
        edDescr.SelectAll()
        If e.KeyValue >= 96 And e.KeyValue <= 105 Then
          edDescr.Text = e.KeyCode.ToString.Substring(6)
        Else
          edDescr.Text = e.KeyCode.ToString
        End If
        edDescr.SelectionStart = edDescr.Text.Length
        e.Handled = True        'se non mettoquesta riga dopo aver compilato edComune intercetta nuovamente la KeyDown e fa partire subito la ricerca ...
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__HLAB_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    '-------------------------------------------------
    'salvo il recent
    If ckOttimistico.Checked Then
      oMenu.SaveSettingBus("BN__HLSR", "RECENT", ".", "Ottimistico", "-1", " ", "NS.", "...", "...")
    Else
      oMenu.SaveSettingBus("BN__HLSR", "RECENT", ".", "Ottimistico", "0", " ", "NS.", "...", "...")
    End If
  End Sub
#End Region
  
#Region "Eventi di Griglia"
  Public Overridable Sub grZoom_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grZoom.MouseDoubleClick
    Try
      cmdSeleziona_Click(Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Eventi Pulsanti"
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Me.Close()
  End Sub

  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Try
      If Not grvZoom.NTSGetCurrentDataRow() Is Nothing Then
        oParam.strOut = grvZoom.GetFocusedRowCellDisplayText("pn_conto")
      End If
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdRicerca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRicerca.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      dsHlsr.Clear()
      If ckOttimistico.Checked = True Then edDescr.Text = "*" & edDescr.Text & "*"
      If edDescr.Text = "**" Then edDescr.Text = ""
      edDescr.Text = edDescr.Text.Replace("**", "*")
      If edDescr.Text <> "" Then
        If edDescr.Text.Substring(edDescr.Text.Length - 1) <> "*" Then edDescr.Text += "*"
      End If
      '--------------------------------------------------------------------------------------------------------------
      ocleHlsr.Apri(dsHlsr, DittaCorrente, edDescr.Text.Replace("?", "_").Replace("*", "%"), _
        edDatini.Text, oParam.strTipo, oParam.lContoCF, oParam.lNumreg, oParam.strDatreg)
      dcHlsr.DataSource = dsHlsr.Tables("PRINOT")
      dsHlsr.AcceptChanges()
      grZoom.DataSource = dcHlsr
      '--------------------------------------------------------------------------------------------------------------
      '--- Mi sposto sulla griglia e faccio in modo che con enter venga selezionato il valore
      '--------------------------------------------------------------------------------------------------------------
      If grvZoom.RowCount > 0 Then grZoom.Focus()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

End Class
