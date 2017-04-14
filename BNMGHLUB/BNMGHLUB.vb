Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGHLUB

  Public oCleHlub As CLEMGHLUB
  Public oParam As CLE__PATB       'parametri passati dal child che mi ha chiamato: sempre tramite questa classe gli restituisceo il valore
  Public dsZoom As New DataSet
  Public dcZoom As BindingSource = New BindingSource()


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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGHLUB", "BEMGHLUB", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 127791222144843750, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleHlub = CType(oTmp, CLEMGHLUB)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGHLUB", strRemoteServer, strRemotePort)
    oCleHlub.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort)

    '---------------------------------
    'inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    AddHandler oCleHlub.RemoteEvent, AddressOf GestisciEventiEntity

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'completo le informazioni dei i controlli
      edCodart.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023157106274, "Articolo"), tabartico, True)
      ckAnaubic.NTSSetParam(oMenu, oApp.Tr(Me, 131014845085498807, "Mostra tutte le ubicazioni"), "S", "N")

      grvZoom.NTSSetParam(oMenu, oApp.Tr(Me, 128230023157574793, "Griglia Zoom Lotti"))
      grvZoom.NTSAllowDelete = False
      grvZoom.NTSAllowUpdate = False
      grvZoom.NTSAllowInsert = False
      tt_ubicaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128569610184062500, "Ubicazione"), 0)
      tt_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128571560075781250, "Esistenza"), oApp.FormatQta, 15)
      tt_qtacar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128571560062187500, "Disponibilità netta"), oApp.FormatQta, 15)


      grvZoom.Enabled = False

      grvZoom.AddColumnBackColor("backcolor_tt_ubicaz")

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
    Me.ckAnaubic = New NTSInformatica.NTSCheckBox
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.cmdRicerca = New NTSInformatica.NTSButton
    Me.pnDescr = New NTSInformatica.NTSPanel
    Me.lbArticolo = New NTSInformatica.NTSLabel
    Me.edCodart = New NTSInformatica.NTSTextBoxStr
    Me.lbArticoloLabel = New NTSInformatica.NTSLabel
    Me.grZoom = New NTSInformatica.NTSGrid
    Me.grvZoom = New NTSInformatica.NTSGridView
    Me.tt_ubicaz = New NTSInformatica.NTSGridColumn
    Me.tt_quant = New NTSInformatica.NTSGridColumn
    Me.tt_qtacar = New NTSInformatica.NTSGridColumn
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnAction, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAction.SuspendLayout()
    CType(Me.ckAnaubic.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnDescr, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDescr.SuspendLayout()
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.pnAction.Controls.Add(Me.ckAnaubic)
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
    'ckAnaubic
    '
    Me.ckAnaubic.Location = New System.Drawing.Point(3, 301)
    Me.ckAnaubic.Name = "ckAnaubic"
    Me.ckAnaubic.NTSCheckValue = "S"
    Me.ckAnaubic.NTSUnCheckValue = "N"
    Me.ckAnaubic.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAnaubic.Properties.Appearance.Options.UseBackColor = True
    Me.ckAnaubic.Properties.Appearance.Options.UseTextOptions = True
    Me.ckAnaubic.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ckAnaubic.Properties.AutoHeight = False
    Me.ckAnaubic.Properties.Caption = "Mostra ubicazioni codificate"
    Me.ckAnaubic.Size = New System.Drawing.Size(104, 35)
    Me.ckAnaubic.TabIndex = 4
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(10, 67)
    Me.cmdAnnulla.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(88, 22)
    Me.cmdAnnulla.TabIndex = 3
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.ImagePath = ""
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
    Me.cmdRicerca.ImagePath = ""
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
    Me.pnDescr.Controls.Add(Me.lbArticolo)
    Me.pnDescr.Controls.Add(Me.edCodart)
    Me.pnDescr.Controls.Add(Me.lbArticoloLabel)
    Me.pnDescr.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnDescr.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnDescr.Location = New System.Drawing.Point(0, 0)
    Me.pnDescr.Name = "pnDescr"
    Me.pnDescr.NTSActiveTrasparency = True
    Me.pnDescr.Size = New System.Drawing.Size(483, 43)
    Me.pnDescr.TabIndex = 1
    '
    'lbArticolo
    '
    Me.lbArticolo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbArticolo.BackColor = System.Drawing.Color.Transparent
    Me.lbArticolo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbArticolo.Location = New System.Drawing.Point(179, 12)
    Me.lbArticolo.Name = "lbArticolo"
    Me.lbArticolo.NTSDbField = ""
    Me.lbArticolo.Size = New System.Drawing.Size(304, 20)
    Me.lbArticolo.TabIndex = 5
    Me.lbArticolo.Tooltip = ""
    Me.lbArticolo.UseMnemonic = False
    '
    'edCodart
    '
    Me.edCodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodart.EditValue = ""
    Me.edCodart.Enabled = False
    Me.edCodart.Location = New System.Drawing.Point(84, 12)
    Me.edCodart.Name = "edCodart"
    Me.edCodart.NTSDbField = ""
    Me.edCodart.NTSForzaVisZoom = False
    Me.edCodart.NTSOldValue = ""
    Me.edCodart.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCodart.Properties.Appearance.Options.UseBackColor = True
    Me.edCodart.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodart.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodart.Properties.AutoHeight = False
    Me.edCodart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodart.Properties.MaxLength = 65536
    Me.edCodart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodart.Size = New System.Drawing.Size(89, 20)
    Me.edCodart.TabIndex = 3
    '
    'lbArticoloLabel
    '
    Me.lbArticoloLabel.AutoSize = True
    Me.lbArticoloLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbArticoloLabel.Location = New System.Drawing.Point(12, 15)
    Me.lbArticoloLabel.Name = "lbArticoloLabel"
    Me.lbArticoloLabel.NTSDbField = ""
    Me.lbArticoloLabel.Size = New System.Drawing.Size(43, 13)
    Me.lbArticoloLabel.TabIndex = 2
    Me.lbArticoloLabel.Text = "Articolo"
    Me.lbArticoloLabel.Tooltip = ""
    Me.lbArticoloLabel.UseMnemonic = False
    '
    'grZoom
    '
    Me.grZoom.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grZoom.EmbeddedNavigator.Name = ""
    Me.grZoom.Location = New System.Drawing.Point(0, 43)
    Me.grZoom.MainView = Me.grvZoom
    Me.grZoom.Name = "grZoom"
    Me.grZoom.Size = New System.Drawing.Size(483, 297)
    Me.grZoom.TabIndex = 6
    Me.grZoom.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvZoom})
    '
    'grvZoom
    '
    Me.grvZoom.ActiveFilterEnabled = False
    Me.grvZoom.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tt_ubicaz, Me.tt_quant, Me.tt_qtacar})
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
    'tt_ubicaz
    '
    Me.tt_ubicaz.AppearanceCell.Options.UseBackColor = True
    Me.tt_ubicaz.AppearanceCell.Options.UseTextOptions = True
    Me.tt_ubicaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_ubicaz.Caption = "Ubicazione"
    Me.tt_ubicaz.Enabled = True
    Me.tt_ubicaz.FieldName = "tt_ubicaz"
    Me.tt_ubicaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_ubicaz.Name = "tt_ubicaz"
    Me.tt_ubicaz.NTSRepositoryComboBox = Nothing
    Me.tt_ubicaz.NTSRepositoryItemCheck = Nothing
    Me.tt_ubicaz.NTSRepositoryItemMemo = Nothing
    Me.tt_ubicaz.NTSRepositoryItemText = Nothing
    Me.tt_ubicaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_ubicaz.OptionsFilter.AllowFilter = False
    Me.tt_ubicaz.Visible = True
    Me.tt_ubicaz.VisibleIndex = 0
    Me.tt_ubicaz.Width = 70
    '
    'tt_quant
    '
    Me.tt_quant.AppearanceCell.Options.UseBackColor = True
    Me.tt_quant.AppearanceCell.Options.UseTextOptions = True
    Me.tt_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_quant.Caption = "Esistenza"
    Me.tt_quant.Enabled = True
    Me.tt_quant.FieldName = "tt_quant"
    Me.tt_quant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_quant.Name = "tt_quant"
    Me.tt_quant.NTSRepositoryComboBox = Nothing
    Me.tt_quant.NTSRepositoryItemCheck = Nothing
    Me.tt_quant.NTSRepositoryItemMemo = Nothing
    Me.tt_quant.NTSRepositoryItemText = Nothing
    Me.tt_quant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_quant.OptionsFilter.AllowFilter = False
    Me.tt_quant.Visible = True
    Me.tt_quant.VisibleIndex = 1
    '
    'tt_qtacar
    '
    Me.tt_qtacar.AppearanceCell.Options.UseBackColor = True
    Me.tt_qtacar.AppearanceCell.Options.UseTextOptions = True
    Me.tt_qtacar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_qtacar.Caption = "Dispon. netta"
    Me.tt_qtacar.Enabled = True
    Me.tt_qtacar.FieldName = "tt_qtacar"
    Me.tt_qtacar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_qtacar.Name = "tt_qtacar"
    Me.tt_qtacar.NTSRepositoryComboBox = Nothing
    Me.tt_qtacar.NTSRepositoryItemCheck = Nothing
    Me.tt_qtacar.NTSRepositoryItemMemo = Nothing
    Me.tt_qtacar.NTSRepositoryItemText = Nothing
    Me.tt_qtacar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_qtacar.OptionsFilter.AllowFilter = False
    Me.tt_qtacar.Visible = True
    Me.tt_qtacar.VisibleIndex = 2
    '
    'FRMMGHLUB
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.CancelButton = Me.cmdAnnulla
    Me.ClientSize = New System.Drawing.Size(593, 340)
    Me.Controls.Add(Me.grZoom)
    Me.Controls.Add(Me.pnDescr)
    Me.Controls.Add(Me.pnAction)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMMGHLUB"
    Me.Text = "ZOOM UBICAZIONI APERTE SU MAGAZ."
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnAction, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAction.ResumeLayout(False)
    Me.pnAction.PerformLayout()
    CType(Me.ckAnaubic.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnDescr, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDescr.ResumeLayout(False)
    Me.pnDescr.PerformLayout()
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grZoom, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvZoom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  '-----------

  Public Overridable Sub FRMMGHLUB_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim strTmp As String = ""
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupRME) OrElse _
         CBool(oMenu.GetSettingBus("BSRMANUB", "OPZIONI", ".", "SbloccaAccessoProgramma", "0", ".", "0")) Then
        ckAnaubic.Checked = CBool(oMenu.GetSettingBus("BSMGHLUB", "RECENT", ".", "Anaubic", "0", ".", "0"))
      Else
        ckAnaubic.Visible = False
        ckAnaubic.Checked = False
      End If

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      'e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipodocumento, ecc ...
      'GctlTipoDoc = ""
      GctlSetRoules()
      GctlApplicaDefaultValue()

      Me.Text = oApp.Tr(Me, 128572300152968750, "Ubicazioni aperte su magazzino ") & oParam.nMagaz.ToString

      edCodart.Text = oParam.strTipo
      oMenu.ValCodiceDb(edCodart.Text, DittaCorrente, "ARTICO", "S", strTmp)
      lbArticolo.Text = strTmp
      If oParam.nAnno <> 0 Then
        'nAnno = fase articolo
        lbArticolo.Text += " - " & oApp.Tr(Me, 128571547441562500, "Fase ") & oParam.nAnno.ToString
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGHLUB_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      cmdRicerca_Click(Me, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGHLUB_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Try
      '--------------------------------------------
      'gestione dello zoom:
      'eseguo la Zoom, tanto se per il controllo non deve venir eseguito uno zoom particolare, la routine non fa nulla e viene processato lo zoom standard del controllo, settato con la NTSSetParamZoom
      'If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
      '  Zoom()
      '  e.Handled = True    'altrimenti anche il controllo riceve l'F5 e la routine ZOOM viene eseguita 2 volte!!!
      'End If

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

  Public Overridable Sub FRMMGHLUB_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If ckAnaubic.Visible Then oMenu.SaveSettingBus("BSMGHLUB", "RECENT", ".", "Anaubic", NTSCStr(IIf(ckAnaubic.Checked, "-1", "0")), ".", ".S.", ".S.", "...")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  '------------

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    'oParam.Ditta = DittaCorrente
    Me.Close()
  End Sub

  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Try
      If dsZoom.Tables.Count > 0 Then
        If dsZoom.Tables("UBICAZ").Rows.Count > 0 Then
          oParam.strOut = grvZoom.GetFocusedRowCellDisplayText("tt_ubicaz")
          oParam.dImporto = NTSCDec(grvZoom.GetFocusedRowCellDisplayText("tt_qtacar"))
        End If
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
      dsZoom.Clear()

      'oParam.nAnno = fase articolo passata dal programma chiamante
      oCleHlub.Apri(dsZoom, DittaCorrente, edCodart.Text, oParam.nAnno, oParam.nMagaz, oParam.strDatreg, ckAnaubic.Checked)
      dcZoom.DataSource = dsZoom.Tables("UBICAZ")
      dsZoom.AcceptChanges()
      grZoom.DataSource = dcZoom

      'mi sposto sulla griglia e faccio in modo che con enter venga selezionato il valore
      If grvZoom.RowCount > 0 Then grZoom.Focus()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  '------------

  Public Overridable Sub grZoom_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grZoom.MouseDoubleClick
    cmdSeleziona_Click(Me, Nothing)
  End Sub

  Public Overridable Sub ckAnaubic_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckAnaubic.CheckedChanged
    Try
      cmdRicerca_Click(Nothing, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
End Class
