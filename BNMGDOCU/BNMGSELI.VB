Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGSELI
  Private components As System.ComponentModel.IContainer

  Public oPar As CLE__CLDP
  Public lMaga As Integer = 0
  Public lLista As Integer = 0
  Public dttLista As New DataTable
  Public dcLista As New BindingSource
  Public oCleDocu As CLEMGDOCU

  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    Try
      oMenu = Menu
      oApp = oMenu.App
      oPar = Param
      If Ditta <> "" Then
        DittaCorrente = Ditta
      Else
        DittaCorrente = oApp.Ditta
      End If

      InitializeComponent()
      Me.MinimumSize = Me.Size

      '------------------------------------------------
      'Creo e attivo l'entity, in più inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY stesso
      Dim strErr As String = ""
      Dim objTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGSELI", "BEMGDOCU", objTmp, strErr, False, "", "") = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130086828700835675, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
        Return False
      End If
      oCleDocu = CType(objTmp, CLEMGDOCU)
      '------------------------------------------------
      bRemoting = Menu.Remoting("BNMGSCTR", strRemoteServer, strRemotePort)
      AddHandler oCleDocu.RemoteEvent, AddressOf GestisciEventiEntity
      If oCleDocu.Init(oApp, NTSScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

      lMaga = NTSCInt(oPar.dPar1)

      InitControls()

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function


  Public Overridable Sub InitEntity(ByRef cleGsor As CLEMGDOCU)
    oCleDocu = cleGsor
    AddHandler oCleDocu.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      grvList.NTSSetParam(oMenu, oApp.Tr(Me, 129303108962592719, "Lista"))
      tb_codlsar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129303108962748944, "Codice"), "0", 4, 0, 9999)
      tb_deslsar.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129303108962905169, "Descrizione"), 0, True)
      tb_lsarnote.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129303108963061394, "Note"), 0, True)
      tb_opnomest.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129303108963217619, "Operatore"), 0, True)
      edOperatore.NTSSetParam(oMenu, oApp.Tr(Me, 129024305608039223, "Nome Operatore"), 20, True)
      edDescr.NTSSetParam(oMenu, oApp.Tr(Me, 129303108964623644, "Descrizione"), 0, True)
      ckCancella.NTSSetParam(oMenu, oApp.Tr(Me, 129303108965092319, "Cancella la lista dopo averla importata."), "S", "N")

      edOperatore.NTSSetParamZoom("ZOOMOPERAT")

      grvList.NTSAllowDelete = False
      grvList.NTSAllowUpdate = False
      grvList.NTSAllowInsert = False

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
    Me.pnCommandbutton = New NTSInformatica.NTSPanel
    Me.cmRicerca = New NTSInformatica.NTSButton
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.lbInfo = New NTSInformatica.NTSLabel
    Me.lbNome = New NTSInformatica.NTSLabel
    Me.lbDescr = New NTSInformatica.NTSLabel
    Me.edOperatore = New NTSInformatica.NTSTextBoxStr
    Me.edDescr = New NTSInformatica.NTSTextBoxStr
    Me.grList = New NTSInformatica.NTSGrid
    Me.grvList = New NTSInformatica.NTSGridView
    Me.tb_codlsar = New NTSInformatica.NTSGridColumn
    Me.tb_deslsar = New NTSInformatica.NTSGridColumn
    Me.tb_lsarnote = New NTSInformatica.NTSGridColumn
    Me.tb_opnomest = New NTSInformatica.NTSGridColumn
    Me.ckCancella = New NTSInformatica.NTSCheckBox
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnCommandbutton, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCommandbutton.SuspendLayout()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.edOperatore.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDescr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grList, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvList, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckCancella.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'pnCommandbutton
    '
    Me.pnCommandbutton.AllowDrop = True
    Me.pnCommandbutton.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCommandbutton.Appearance.Options.UseBackColor = True
    Me.pnCommandbutton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCommandbutton.Controls.Add(Me.cmRicerca)
    Me.pnCommandbutton.Controls.Add(Me.cmdConferma)
    Me.pnCommandbutton.Controls.Add(Me.cmdAnnulla)
    Me.pnCommandbutton.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCommandbutton.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnCommandbutton.Location = New System.Drawing.Point(503, 0)
    Me.pnCommandbutton.Name = "pnCommandbutton"
    Me.pnCommandbutton.NTSActiveTrasparency = True
    Me.pnCommandbutton.Size = New System.Drawing.Size(85, 304)
    Me.pnCommandbutton.TabIndex = 49
    '
    'cmRicerca
    '
    Me.cmRicerca.ImagePath = ""
    Me.cmRicerca.ImageText = ""
    Me.cmRicerca.Location = New System.Drawing.Point(3, 6)
    Me.cmRicerca.Name = "cmRicerca"
    Me.cmRicerca.NTSContextMenu = Nothing
    Me.cmRicerca.Size = New System.Drawing.Size(79, 24)
    Me.cmRicerca.TabIndex = 4
    Me.cmRicerca.Text = "&Ricerca"
    '
    'cmdConferma
    '
    Me.cmdConferma.ImagePath = ""
    Me.cmdConferma.ImageText = ""
    Me.cmdConferma.Location = New System.Drawing.Point(3, 34)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.NTSContextMenu = Nothing
    Me.cmdConferma.Size = New System.Drawing.Size(79, 24)
    Me.cmdConferma.TabIndex = 2
    Me.cmdConferma.Text = "&Conferma"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdAnnulla.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(3, 255)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(79, 24)
    Me.cmdAnnulla.TabIndex = 3
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.lbInfo)
    Me.pnTop.Controls.Add(Me.lbNome)
    Me.pnTop.Controls.Add(Me.lbDescr)
    Me.pnTop.Controls.Add(Me.edOperatore)
    Me.pnTop.Controls.Add(Me.edDescr)
    Me.pnTop.Controls.Add(Me.grList)
    Me.pnTop.Controls.Add(Me.ckCancella)
    Me.pnTop.Controls.Add(Me.pnCommandbutton)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTop.Location = New System.Drawing.Point(0, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(588, 304)
    Me.pnTop.TabIndex = 50
    '
    'lbInfo
    '
    Me.lbInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbInfo.AutoSize = True
    Me.lbInfo.BackColor = System.Drawing.Color.Transparent
    Me.lbInfo.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
    Me.lbInfo.Location = New System.Drawing.Point(21, 271)
    Me.lbInfo.Name = "lbInfo"
    Me.lbInfo.NTSDbField = ""
    Me.lbInfo.Size = New System.Drawing.Size(427, 13)
    Me.lbInfo.TabIndex = 57
    Me.lbInfo.Text = "N.B.: Vengono considerate solo le Liste Selezionate con status 'Completato'"
    Me.lbInfo.Tooltip = ""
    Me.lbInfo.UseMnemonic = False
    '
    'lbNome
    '
    Me.lbNome.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbNome.AutoSize = True
    Me.lbNome.BackColor = System.Drawing.Color.Transparent
    Me.lbNome.Location = New System.Drawing.Point(275, 11)
    Me.lbNome.Name = "lbNome"
    Me.lbNome.NTSDbField = ""
    Me.lbNome.Size = New System.Drawing.Size(57, 13)
    Me.lbNome.TabIndex = 56
    Me.lbNome.Text = "Operatore"
    Me.lbNome.Tooltip = ""
    Me.lbNome.UseMnemonic = False
    '
    'lbDescr
    '
    Me.lbDescr.AutoSize = True
    Me.lbDescr.BackColor = System.Drawing.Color.Transparent
    Me.lbDescr.Location = New System.Drawing.Point(12, 11)
    Me.lbDescr.Name = "lbDescr"
    Me.lbDescr.NTSDbField = ""
    Me.lbDescr.Size = New System.Drawing.Size(61, 13)
    Me.lbDescr.TabIndex = 55
    Me.lbDescr.Text = "Descrizione"
    Me.lbDescr.Tooltip = ""
    Me.lbDescr.UseMnemonic = False
    '
    'edOperatore
    '
    Me.edOperatore.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edOperatore.Cursor = System.Windows.Forms.Cursors.Default
    Me.edOperatore.Location = New System.Drawing.Point(336, 8)
    Me.edOperatore.Name = "edOperatore"
    Me.edOperatore.NTSDbField = ""
    Me.edOperatore.NTSForzaVisZoom = False
    Me.edOperatore.NTSOldValue = ""
    Me.edOperatore.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOperatore.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOperatore.Properties.AutoHeight = False
    Me.edOperatore.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOperatore.Properties.MaxLength = 65536
    Me.edOperatore.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOperatore.Size = New System.Drawing.Size(165, 20)
    Me.edOperatore.TabIndex = 54
    '
    'edDescr
    '
    Me.edDescr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDescr.Location = New System.Drawing.Point(79, 8)
    Me.edDescr.Name = "edDescr"
    Me.edDescr.NTSDbField = ""
    Me.edDescr.NTSForzaVisZoom = False
    Me.edDescr.NTSOldValue = ""
    Me.edDescr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDescr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDescr.Properties.AutoHeight = False
    Me.edDescr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDescr.Properties.MaxLength = 65536
    Me.edDescr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDescr.Size = New System.Drawing.Size(190, 20)
    Me.edDescr.TabIndex = 53
    '
    'grList
    '
    Me.grList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grList.EmbeddedNavigator.Name = ""
    Me.grList.Location = New System.Drawing.Point(3, 34)
    Me.grList.MainView = Me.grvList
    Me.grList.Name = "grList"
    Me.grList.Size = New System.Drawing.Size(499, 216)
    Me.grList.TabIndex = 52
    Me.grList.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvList})
    '
    'grvList
    '
    Me.grvList.ActiveFilterEnabled = False
    Me.grvList.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tb_codlsar, Me.tb_deslsar, Me.tb_lsarnote, Me.tb_opnomest})
    Me.grvList.Enabled = True
    Me.grvList.GridControl = Me.grList
    Me.grvList.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvList.MinRowHeight = 14
    Me.grvList.Name = "grvList"
    Me.grvList.NTSAllowDelete = True
    Me.grvList.NTSAllowInsert = True
    Me.grvList.NTSAllowUpdate = True
    Me.grvList.NTSMenuContext = Nothing
    Me.grvList.OptionsCustomization.AllowRowSizing = True
    Me.grvList.OptionsFilter.AllowFilterEditor = False
    Me.grvList.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvList.OptionsNavigation.UseTabKey = False
    Me.grvList.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvList.OptionsView.ColumnAutoWidth = False
    Me.grvList.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvList.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvList.OptionsView.ShowGroupPanel = False
    Me.grvList.RowHeight = 14
    '
    'tb_codlsar
    '
    Me.tb_codlsar.AppearanceCell.Options.UseBackColor = True
    Me.tb_codlsar.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codlsar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codlsar.Caption = "Codice"
    Me.tb_codlsar.Enabled = True
    Me.tb_codlsar.FieldName = "tb_codlsar"
    Me.tb_codlsar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codlsar.Name = "tb_codlsar"
    Me.tb_codlsar.NTSRepositoryComboBox = Nothing
    Me.tb_codlsar.NTSRepositoryItemCheck = Nothing
    Me.tb_codlsar.NTSRepositoryItemMemo = Nothing
    Me.tb_codlsar.NTSRepositoryItemText = Nothing
    Me.tb_codlsar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codlsar.OptionsFilter.AllowFilter = False
    Me.tb_codlsar.Visible = True
    Me.tb_codlsar.VisibleIndex = 0
    '
    'tb_deslsar
    '
    Me.tb_deslsar.AppearanceCell.Options.UseBackColor = True
    Me.tb_deslsar.AppearanceCell.Options.UseTextOptions = True
    Me.tb_deslsar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_deslsar.Caption = "Descrizione"
    Me.tb_deslsar.Enabled = True
    Me.tb_deslsar.FieldName = "tb_deslsar"
    Me.tb_deslsar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_deslsar.Name = "tb_deslsar"
    Me.tb_deslsar.NTSRepositoryComboBox = Nothing
    Me.tb_deslsar.NTSRepositoryItemCheck = Nothing
    Me.tb_deslsar.NTSRepositoryItemMemo = Nothing
    Me.tb_deslsar.NTSRepositoryItemText = Nothing
    Me.tb_deslsar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_deslsar.OptionsFilter.AllowFilter = False
    Me.tb_deslsar.Visible = True
    Me.tb_deslsar.VisibleIndex = 1
    '
    'tb_lsarnote
    '
    Me.tb_lsarnote.AppearanceCell.Options.UseBackColor = True
    Me.tb_lsarnote.AppearanceCell.Options.UseTextOptions = True
    Me.tb_lsarnote.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_lsarnote.Caption = "Note"
    Me.tb_lsarnote.Enabled = True
    Me.tb_lsarnote.FieldName = "tb_lsarnote"
    Me.tb_lsarnote.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_lsarnote.Name = "tb_lsarnote"
    Me.tb_lsarnote.NTSRepositoryComboBox = Nothing
    Me.tb_lsarnote.NTSRepositoryItemCheck = Nothing
    Me.tb_lsarnote.NTSRepositoryItemMemo = Nothing
    Me.tb_lsarnote.NTSRepositoryItemText = Nothing
    Me.tb_lsarnote.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_lsarnote.OptionsFilter.AllowFilter = False
    Me.tb_lsarnote.Visible = True
    Me.tb_lsarnote.VisibleIndex = 2
    '
    'tb_opnomest
    '
    Me.tb_opnomest.AppearanceCell.Options.UseBackColor = True
    Me.tb_opnomest.AppearanceCell.Options.UseTextOptions = True
    Me.tb_opnomest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_opnomest.Caption = "Operatore"
    Me.tb_opnomest.Enabled = True
    Me.tb_opnomest.FieldName = "tb_opnomest"
    Me.tb_opnomest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_opnomest.Name = "tb_opnomest"
    Me.tb_opnomest.NTSRepositoryComboBox = Nothing
    Me.tb_opnomest.NTSRepositoryItemCheck = Nothing
    Me.tb_opnomest.NTSRepositoryItemMemo = Nothing
    Me.tb_opnomest.NTSRepositoryItemText = Nothing
    Me.tb_opnomest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_opnomest.OptionsFilter.AllowFilter = False
    Me.tb_opnomest.Visible = True
    Me.tb_opnomest.VisibleIndex = 3
    '
    'ckCancella
    '
    Me.ckCancella.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.ckCancella.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckCancella.Location = New System.Drawing.Point(3, 251)
    Me.ckCancella.Name = "ckCancella"
    Me.ckCancella.NTSCheckValue = "S"
    Me.ckCancella.NTSUnCheckValue = "N"
    Me.ckCancella.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckCancella.Properties.Appearance.Options.UseBackColor = True
    Me.ckCancella.Properties.AutoHeight = False
    Me.ckCancella.Properties.Caption = "Cancella la lista dopo averla importata."
    Me.ckCancella.Size = New System.Drawing.Size(211, 19)
    Me.ckCancella.TabIndex = 51
    '
    'FRMMGSELI
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.CancelButton = Me.cmdAnnulla
    Me.ClientSize = New System.Drawing.Size(588, 304)
    Me.Controls.Add(Me.pnTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.MinimizeBox = False
    Me.Name = "FRMMGSELI"
    Me.Text = "SELEZIONE LISTA SELEZIONATA"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnCommandbutton, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCommandbutton.ResumeLayout(False)
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.edOperatore.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDescr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grList, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvList, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckCancella.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub


#Region "Eventi di form"
  Public Overridable Sub FRMMGSEDO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      grvList.Enabled = False
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      GctlApplicaDefaultValue()
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleDocu.CaricaListe(lMaga, edDescr.Text, edOperatore.Text, dttLista) Then Me.Close()
      '--------------------------------------------------------------------------------------------------------------
      dcLista.DataSource = dttLista
      grList.DataSource = dcLista
      '--------------------------------------------------------------------------------------------------------------
      ckCancella.Checked = CBool(oMenu.GetSettingBusDitt(DittaCorrente, "BSORGSOR", "Recent", ".", "CancellaLista", "0", " ", "0"))
      '--------------------------------------------------------------------------------------------------------------
      If lMaga <> 0 Then
        lbInfo.Text += vbCrLf & _
          oApp.Tr(Me, 130689190941277621, "       Relative al magazzino '|" & lMaga.ToString & "|'")
        If oMenu.ValCodiceDb(lMaga.ToString, DittaCorrente, "TABMAGA", "N", strTmp) = True Then
          If strTmp.Trim <> "" Then lbInfo.Text += " - " & strTmp
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region


#Region "Eventi"
  Public Overridable Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    Try
      If grvList.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129303091879213169, "Selezionare una riga per continuare."))
        Return
      End If

      oPar.dPar2 = NTSCInt(grvList.NTSGetCurrentDataRow!tb_codlsar)
      oPar.bPar1 = ckCancella.Checked

      oMenu.SaveSettingBus("BSORGSOR", "RECENT", ".", "CancellaLista", IIf(ckCancella.Checked, "-1", "0").ToString, " ", "NS.", "NS.", "NN.")

      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Me.Close()
  End Sub

  Public Overridable Sub cmRicerca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmRicerca.Click
    Try
      If Not oCleDocu.CaricaListe(lMaga, edDescr.Text, edOperatore.Text, dttLista) Then Me.Close()

      dcLista.DataSource = dttLista
      grList.DataSource = dcLista
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Griglia"
  Public Overridable Sub grvList_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grvList.DoubleClick
    Try
      cmdConferma_Click(Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region
End Class
