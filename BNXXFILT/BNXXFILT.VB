Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class NTSXXFILT
  Public WithEvents oParent As FRM__CHIL = Nothing
  Public oCallParams As CLE__CLDP
  Public oCleFilt As CLEXXFILT
  Public dttFilt As DataTable
  Public dcFilt As BindingSource
  Public bFiltriSbloccati As Boolean = True

#Region "Interfaccia"
  Public WithEvents pnAll As NTSInformatica.NTSPanel
  Public WithEvents grFilt As NTSInformatica.NTSGrid
  Public WithEvents grvFilt As NTSInformatica.NTSGridView
  Public WithEvents xx_nome As NTSInformatica.NTSGridColumn
  Public WithEvents xx_tipo As NTSInformatica.NTSGridColumn
  Public WithEvents xx_valore As NTSInformatica.NTSGridColumn
  Public WithEvents pnBottom As NTSInformatica.NTSPanel
  Public WithEvents lbInfo As NTSInformatica.NTSLabel
  Public WithEvents cmdBloccoFiltri As NTSInformatica.NTSButton
  Public WithEvents cmdSalvaFiltri As NTSInformatica.NTSButton
  Public WithEvents ckAncheValori As NTSInformatica.NTSCheckBox
  Public WithEvents xx_descampo As NTSInformatica.NTSGridColumn
  Public WithEvents xx_valorea As NTSInformatica.NTSGridColumn
#End Region

  Private Sub InitializeComponent()
    Me.pnAll = New NTSInformatica.NTSPanel
    Me.grFilt = New NTSInformatica.NTSGrid
    Me.grvFilt = New NTSInformatica.NTSGridView
    Me.xx_nome = New NTSInformatica.NTSGridColumn
    Me.xx_descampo = New NTSInformatica.NTSGridColumn
    Me.xx_tipo = New NTSInformatica.NTSGridColumn
    Me.xx_valore = New NTSInformatica.NTSGridColumn
    Me.xx_valorea = New NTSInformatica.NTSGridColumn
    Me.pnBottom = New NTSInformatica.NTSPanel
    Me.ckAncheValori = New NTSInformatica.NTSCheckBox
    Me.cmdSalvaFiltri = New NTSInformatica.NTSButton
    Me.cmdBloccoFiltri = New NTSInformatica.NTSButton
    Me.lbInfo = New NTSInformatica.NTSLabel
    CType(Me.pnAll, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAll.SuspendLayout()
    CType(Me.grFilt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvFilt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBottom.SuspendLayout()
    CType(Me.ckAncheValori.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'pnAll
    '
    Me.pnAll.AllowDrop = True
    Me.pnAll.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAll.Appearance.Options.UseBackColor = True
    Me.pnAll.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAll.Controls.Add(Me.grFilt)
    Me.pnAll.Controls.Add(Me.pnBottom)
    Me.pnAll.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnAll.Location = New System.Drawing.Point(0, 0)
    Me.pnAll.Name = "pnAll"
    Me.pnAll.NTSActiveTrasparency = True
    Me.pnAll.Size = New System.Drawing.Size(399, 193)
    Me.pnAll.TabIndex = 0
    Me.pnAll.Text = "NtsPanel1"
    '
    'grFilt
    '
    Me.grFilt.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grFilt.EmbeddedNavigator.Name = ""
    Me.grFilt.Location = New System.Drawing.Point(0, 0)
    Me.grFilt.MainView = Me.grvFilt
    Me.grFilt.Name = "grFilt"
    Me.grFilt.Size = New System.Drawing.Size(399, 140)
    Me.grFilt.TabIndex = 0
    Me.grFilt.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvFilt})
    '
    'grvFilt
    '
    Me.grvFilt.ActiveFilterEnabled = False
    Me.grvFilt.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_nome, Me.xx_descampo, Me.xx_tipo, Me.xx_valore, Me.xx_valorea})
    Me.grvFilt.Enabled = True
    Me.grvFilt.GridControl = Me.grFilt
    Me.grvFilt.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvFilt.MinRowHeight = 14
    Me.grvFilt.Name = "grvFilt"
    Me.grvFilt.NTSAllowDelete = True
    Me.grvFilt.NTSAllowInsert = True
    Me.grvFilt.NTSAllowUpdate = True
    Me.grvFilt.NTSMenuContext = Nothing
    Me.grvFilt.OptionsCustomization.AllowRowSizing = True
    Me.grvFilt.OptionsFilter.AllowFilterEditor = False
    Me.grvFilt.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvFilt.OptionsNavigation.UseTabKey = False
    Me.grvFilt.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvFilt.OptionsView.ColumnAutoWidth = False
    Me.grvFilt.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvFilt.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvFilt.OptionsView.ShowGroupPanel = False
    Me.grvFilt.RowHeight = 14
    '
    'xx_nome
    '
    Me.xx_nome.AppearanceCell.Options.UseBackColor = True
    Me.xx_nome.AppearanceCell.Options.UseTextOptions = True
    Me.xx_nome.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_nome.Caption = "Nome Campo"
    Me.xx_nome.Enabled = False
    Me.xx_nome.FieldName = "xx_nome"
    Me.xx_nome.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_nome.Name = "xx_nome"
    Me.xx_nome.NTSRepositoryComboBox = Nothing
    Me.xx_nome.NTSRepositoryItemCheck = Nothing
    Me.xx_nome.NTSRepositoryItemMemo = Nothing
    Me.xx_nome.NTSRepositoryItemText = Nothing
    Me.xx_nome.OptionsColumn.AllowEdit = False
    Me.xx_nome.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_nome.OptionsColumn.ReadOnly = True
    Me.xx_nome.OptionsFilter.AllowFilter = False
    '
    'xx_descampo
    '
    Me.xx_descampo.AppearanceCell.Options.UseBackColor = True
    Me.xx_descampo.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descampo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descampo.Caption = "Campo"
    Me.xx_descampo.Enabled = True
    Me.xx_descampo.FieldName = "xx_descampo"
    Me.xx_descampo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descampo.Name = "xx_descampo"
    Me.xx_descampo.NTSRepositoryComboBox = Nothing
    Me.xx_descampo.NTSRepositoryItemCheck = Nothing
    Me.xx_descampo.NTSRepositoryItemMemo = Nothing
    Me.xx_descampo.NTSRepositoryItemText = Nothing
    Me.xx_descampo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descampo.OptionsFilter.AllowFilter = False
    Me.xx_descampo.Visible = True
    Me.xx_descampo.VisibleIndex = 0
    '
    'xx_tipo
    '
    Me.xx_tipo.AppearanceCell.Options.UseBackColor = True
    Me.xx_tipo.AppearanceCell.Options.UseTextOptions = True
    Me.xx_tipo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_tipo.Caption = "Tipo Filtro"
    Me.xx_tipo.Enabled = True
    Me.xx_tipo.FieldName = "xx_tipo"
    Me.xx_tipo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_tipo.Name = "xx_tipo"
    Me.xx_tipo.NTSRepositoryComboBox = Nothing
    Me.xx_tipo.NTSRepositoryItemCheck = Nothing
    Me.xx_tipo.NTSRepositoryItemMemo = Nothing
    Me.xx_tipo.NTSRepositoryItemText = Nothing
    Me.xx_tipo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_tipo.OptionsFilter.AllowFilter = False
    Me.xx_tipo.Visible = True
    Me.xx_tipo.VisibleIndex = 1
    '
    'xx_valore
    '
    Me.xx_valore.AppearanceCell.Options.UseBackColor = True
    Me.xx_valore.AppearanceCell.Options.UseTextOptions = True
    Me.xx_valore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_valore.Caption = "Valore (Da)"
    Me.xx_valore.Enabled = True
    Me.xx_valore.FieldName = "xx_valore"
    Me.xx_valore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_valore.Name = "xx_valore"
    Me.xx_valore.NTSRepositoryComboBox = Nothing
    Me.xx_valore.NTSRepositoryItemCheck = Nothing
    Me.xx_valore.NTSRepositoryItemMemo = Nothing
    Me.xx_valore.NTSRepositoryItemText = Nothing
    Me.xx_valore.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_valore.OptionsFilter.AllowFilter = False
    Me.xx_valore.Visible = True
    Me.xx_valore.VisibleIndex = 2
    '
    'xx_valorea
    '
    Me.xx_valorea.AppearanceCell.Options.UseBackColor = True
    Me.xx_valorea.AppearanceCell.Options.UseTextOptions = True
    Me.xx_valorea.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_valorea.Caption = "A Valore"
    Me.xx_valorea.Enabled = True
    Me.xx_valorea.FieldName = "xx_valorea"
    Me.xx_valorea.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_valorea.Name = "xx_valorea"
    Me.xx_valorea.NTSRepositoryComboBox = Nothing
    Me.xx_valorea.NTSRepositoryItemCheck = Nothing
    Me.xx_valorea.NTSRepositoryItemMemo = Nothing
    Me.xx_valorea.NTSRepositoryItemText = Nothing
    Me.xx_valorea.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_valorea.OptionsFilter.AllowFilter = False
    Me.xx_valorea.Visible = True
    Me.xx_valorea.VisibleIndex = 3
    '
    'pnBottom
    '
    Me.pnBottom.AllowDrop = True
    Me.pnBottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBottom.Appearance.Options.UseBackColor = True
    Me.pnBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBottom.Controls.Add(Me.ckAncheValori)
    Me.pnBottom.Controls.Add(Me.cmdSalvaFiltri)
    Me.pnBottom.Controls.Add(Me.cmdBloccoFiltri)
    Me.pnBottom.Controls.Add(Me.lbInfo)
    Me.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnBottom.Location = New System.Drawing.Point(0, 140)
    Me.pnBottom.Name = "pnBottom"
    Me.pnBottom.NTSActiveTrasparency = True
    Me.pnBottom.Size = New System.Drawing.Size(399, 53)
    Me.pnBottom.TabIndex = 1
    Me.pnBottom.Text = "NtsPanel1"
    '
    'ckAncheValori
    '
    Me.ckAncheValori.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ckAncheValori.Location = New System.Drawing.Point(182, 32)
    Me.ckAncheValori.Name = "ckAncheValori"
    Me.ckAncheValori.NTSCheckValue = "S"
    Me.ckAncheValori.NTSUnCheckValue = "N"
    Me.ckAncheValori.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAncheValori.Properties.Appearance.Options.UseBackColor = True
    Me.ckAncheValori.Properties.AutoHeight = False
    Me.ckAncheValori.Properties.Caption = "Salva anche i valori"
    Me.ckAncheValori.Size = New System.Drawing.Size(123, 14)
    Me.ckAncheValori.TabIndex = 2
    '
    'cmdSalvaFiltri
    '
    Me.cmdSalvaFiltri.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdSalvaFiltri.ImageText = ""
    Me.cmdSalvaFiltri.Location = New System.Drawing.Point(306, 27)
    Me.cmdSalvaFiltri.Name = "cmdSalvaFiltri"
    Me.cmdSalvaFiltri.NTSContextMenu = Nothing
    Me.cmdSalvaFiltri.Size = New System.Drawing.Size(90, 24)
    Me.cmdSalvaFiltri.TabIndex = 1
    Me.cmdSalvaFiltri.Text = "Salva Filtri"
    '
    'cmdBloccoFiltri
    '
    Me.cmdBloccoFiltri.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdBloccoFiltri.ImageText = ""
    Me.cmdBloccoFiltri.Location = New System.Drawing.Point(306, 2)
    Me.cmdBloccoFiltri.Name = "cmdBloccoFiltri"
    Me.cmdBloccoFiltri.NTSContextMenu = Nothing
    Me.cmdBloccoFiltri.Size = New System.Drawing.Size(90, 24)
    Me.cmdBloccoFiltri.TabIndex = 1
    Me.cmdBloccoFiltri.Text = "Sblocca Filtri"
    Me.cmdBloccoFiltri.Visible = False
    '
    'lbInfo
    '
    Me.lbInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbInfo.BackColor = System.Drawing.Color.Transparent
    Me.lbInfo.Location = New System.Drawing.Point(-3, 0)
    Me.lbInfo.Name = "lbInfo"
    Me.lbInfo.NTSDbField = ""
    Me.lbInfo.Size = New System.Drawing.Size(308, 29)
    Me.lbInfo.TabIndex = 0
    Me.lbInfo.Text = "Selezionare una riga per ottenere più informazioni."
    Me.lbInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbInfo.Tooltip = ""
    Me.lbInfo.UseMnemonic = False
    '
    'NTSXXFILT
    '
    Me.Controls.Add(Me.pnAll)
    Me.Name = "NTSXXFILT"
    Me.Size = New System.Drawing.Size(399, 193)
    CType(Me.pnAll, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAll.ResumeLayout(False)
    CType(Me.grFilt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvFilt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBottom.ResumeLayout(False)
    Me.pnBottom.PerformLayout()
    CType(Me.ckAncheValori.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Sub New()
    InitializeComponent()
    Me.MinimumSize = Me.Size
  End Sub

  Public Overridable Sub InitControls()
    oParent.InitControlsBeginEndInit(Me, False)
    Dim dttTipo As New DataTable
    Try
      dttTipo.Columns.Add("cod")
      dttTipo.Columns.Add("val")
      dttTipo.Rows.Add(New Object() {"=", "è uguale a"})
      dttTipo.Rows.Add(New Object() {"<>", "è diverso a"})
      dttTipo.Rows.Add(New Object() {"IN", "è uno di"})
      dttTipo.Rows.Add(New Object() {"NOT IN", "non è uno di"})
      dttTipo.Rows.Add(New Object() {"<", "è inferiore a"})
      dttTipo.Rows.Add(New Object() {"<=", "è minore di o uguale a"})
      dttTipo.Rows.Add(New Object() {">", "è superiore a"})
      dttTipo.Rows.Add(New Object() {">=", "è maggiore di o uguale a"})
      dttTipo.Rows.Add(New Object() {"BET", "è compreso tra"})
      dttTipo.Rows.Add(New Object() {"NOT BET", "non è compreso tra"})
      dttTipo.Rows.Add(New Object() {"START", "inizia con"})
      dttTipo.Rows.Add(New Object() {"NOT START", "non inizia con"})
      dttTipo.Rows.Add(New Object() {"LIKE", "è simile a"})
      dttTipo.Rows.Add(New Object() {"NOT LIKE", "non è simile a"})
      dttTipo.AcceptChanges()

      oCleFilt.dttCampiTabelle.AcceptChanges()

      grvFilt.NTSSetParam(oMenu, oApp.Tr(Me, 130415891638643743, "Filtri Estesi"))
      xx_nome.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130415891638653725, "Nome Campo"), 0, True)
      xx_descampo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130420240669364507, "Campo"), 0, True)
      xx_tipo.NTSSetParamCMB(oMenu, oApp.Tr(Me, 130415891638663702, "Tipo Filtro"), dttTipo, "val", "cod")
      xx_valore.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130415891638673679, "Valore (Da)"), 0, True)
      xx_valorea.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130415891638683661, "A Valore"), 0, True)

      xx_descampo.NTSForzaVisZoom = True
      xx_valore.NTSForzaVisZoom = True
      xx_valorea.NTSForzaVisZoom = True

      grvFilt.NTSAllowDelete = False
      grvFilt.NTSAllowInsert = False
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    oParent.InitControlsBeginEndInit(Me, True)
  End Sub

  Public Overridable Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    Try
      oParent.GestisciEventiEntity(sender, e)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

#Region "NTSSetparam\Caricamento Dati"
  Public Overridable Sub NTSSetParam(ByRef Menu As CLE__MENU, ByVal NomeCampo As String, ByVal ChildChiamante As String, ByRef Param As CLE__CLDP)
    Try
      '------------------------------------------------
      oMenu = Menu
      oApp = oMenu.oApp
      strNomeCampo = NomeCampo
      oCallParams = Param
      oParent = CType(Me.FindForm, FRM__CHIL)

      '------------------------------------------------
      'creo e attivo l'entity e inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNXXFILT", "BEXXFILT", oTmp, strErr, False, "", "") = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128271029889882656, "ERRORE in fase di creazione Entity:" & vbCrLf) & strErr)
        Return
      End If
      oCleFilt = CType(oTmp, CLEXXFILT)
      '------------------------------------------------
      oParent.bRemoting = Menu.Remoting("BNXXFILT", oParent.strRemoteServer, oParent.strRemotePort)
      AddHandler oCleFilt.RemoteEvent, AddressOf GestisciEventiEntity
      If oCleFilt.Init(oApp, oParent.NTSScript, oMenu.oCleComm, "", oParent.bRemoting, oParent.strRemoteServer, oParent.strRemotePort) = False Then Return

      oCleFilt.strProgrChiamante = ChildChiamante
      'Gestione del pulsante "Blocca\Sblocca Filtri"
      ImpostaBloccoFiltri(True)

      ckAncheValori.Checked = (oMenu.GetSettingBus(oCleFilt.strProgrChiamante, "RECENT", ".", "AncheValori", "N", ".", "N") = "S")

      xx_descampo.Width = 200
      xx_tipo.Width = 100
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function AggiungiTabella(ByVal strTabella As String) As Boolean
    Try
      Return oCleFilt.AggiungiTabella(strTabella)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function RimuoviTabella(ByVal strTabella As String) As Boolean
    Try
      Return oCleFilt.RimuoviTabella(strTabella)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function


  Public Overridable Function InizializzaFiltri() As Boolean
    Try
      'Vengono caricati i campi delle tabelle aggiunte tramite la chiamata al metodo "AggiungiTabella"
      oCleFilt.CaricaCampiTabelle()
      PreparaDatiIniziali()

      InitControls()

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function


  Public Overridable Function PreparaDatiIniziali() As Boolean
    Try
      dcFilt = New BindingSource

      If Not oCleFilt.PreparaDatiIniziali(dttFilt) Then Return False
      dcFilt.DataSource = dttFilt
      grFilt.DataSource = dcFilt

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
#End Region

#Region "Eventi Griglia"
  Public Overridable Sub grvFilt_NTSFocusedColumnChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles grvFilt.NTSFocusedColumnChanged
    Try
      FocusedRowColumnChanged()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub grvFilt_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvFilt.NTSFocusedRowChanged
    Try
      FocusedRowColumnChanged()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub FocusedRowColumnChanged()
    Try
      If grvFilt.NTSGetCurrentDataRow Is Nothing Then Return

      'Se non è stato selezionato nulla, allora blocca le colonne "Tipo e valore"
      If NTSCStr(grvFilt.NTSGetCurrentDataRow!xx_nome) = "-" Then
        xx_tipo.Enabled = False
        xx_valore.Enabled = False
        xx_valorea.Enabled = False
        lbInfo.Text = oApp.Tr(Me, 130415948572957879, "Sulla riga corrente non è stato selezionato il campo da filtrare.")
      Else
        oParent.GctlSetVisEnab(xx_tipo, False)
        oParent.GctlSetVisEnab(xx_valore, False)
        'Solo per between e not between abilita i dati "Valore A"
        Select Case NTSCStr(grvFilt.NTSGetCurrentDataRow!xx_tipo)
          Case "BET", "NOT BET" : oParent.GctlSetVisEnab(xx_valorea, False)
          Case Else : xx_valorea.Enabled = False
        End Select

        'Ho selezionato un campo memo, sono disponibili sono i LIKE
        If NTSCInt(grvFilt.NTSGetCurrentDataRow!xx_tipocampo) = 12 AndAlso _
             NTSCStr(grvFilt.NTSGetCurrentDataRow!xx_tipo) <> "LIKE" AndAlso NTSCStr(grvFilt.NTSGetCurrentDataRow!xx_tipo) <> "NOT LIKE" Then
          grvFilt.NTSGetCurrentDataRow!xx_tipo = "LIKE"
        End If

        'Determino il tipo di colonna
        Select Case NTSCInt(grvFilt.NTSGetCurrentDataRow!xx_tipocampo)
          Case 3 'Small Int
            lbInfo.Text = oApp.Tr(Me, 130415964318610539, "Campo numerico: sono accettati solo numeri interi con massimo 4 cifre.")
          Case 4 'Int 
            lbInfo.Text = oApp.Tr(Me, 130415964280046151, "Campo numerico: sono accettati solo numeri interi con massimo 9 cifre.")
          Case 5 ' Money
            lbInfo.Text = oApp.Tr(Me, 130420306046271355, "Campo importo: è accettato qualsiasi valore numerico.")
          Case 6, 7 ' Real e Float
            lbInfo.Text = oApp.Tr(Me, 130420304735271394, "Campo decimale: è accettato qualsiasi valore numerico.")
          Case 8 'Data
            lbInfo.Text = oApp.Tr(Me, 130415964259896419, "Campo data: sono accettate date comprese tra 01/01/1900 e 31/12/2099.")
          Case 10 'Stringa
            lbInfo.Text = oApp.Tr(Me, 130415964245499064, "Campo testo: accetta fino a |" & NTSCInt(grvFilt.NTSGetCurrentDataRow!xx_size) & "| caratteri.")
          Case 12 'Memo
            lbInfo.Text = oApp.Tr(Me, 130415949675623063, "Campo memo: è accettato qualsiasi tipo di testo. Sono possibili solo gli operatori 'è simili a' e 'non è simile a'.")
          Case Else
            lbInfo.Text = ""
        End Select
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub Zoom()
    Try
      If grFilt.ContainsFocus Then
        '------------------------------------
        'zoom su filtri1 di griglia
        Select Case grvFilt.FocusedColumn.Name
          Case "xx_valore", "xx_valorea"
            If NTSCStr(grvFilt.NTSGetCurrentDataRow!xx_nome) = "-" Then Return 'sono su una colonna senza filtri selezionati
            ApriZoomTabella(NTSCStr(grvFilt.NTSGetCurrentDataRow(grvFilt.FocusedColumn.Name)), NTSCStr(grvFilt.NTSGetCurrentDataRow!xx_nome))
          Case "xx_descampo"
            ZoomNomeCampi()
        End Select
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Eventi"
  Public Overridable Sub cmdBloccoFiltri_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBloccoFiltri.Click
    Try
      ImpostaBloccoFiltri(Not bFiltriSbloccati)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdSalvaFiltri_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalvaFiltri.Click
    Try
      If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 130416135235104743, "Salvare i filtri correnti?")) = DialogResult.Yes Then
        oCleFilt.SalvaFiltri(ckAncheValori.Checked)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

  Public Overridable Sub ImpostaBloccoFiltri(ByVal bSblocca As Boolean)
    Try
      bFiltriSbloccati = bSblocca

      If bFiltriSbloccati Then
        oParent.GctlSetVisEnab(xx_descampo, False)
        cmdBloccoFiltri.Text = oApp.Tr(Me, 130416057063749673, "Blocca Filtri")
      Else
        xx_descampo.Enabled = False
        cmdBloccoFiltri.Text = oApp.Tr(Me, 130416057233318294, "Sblocca Filtri")
      End If

      FocusedRowColumnChanged()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function GeneraQuerySQL() As String
    Try
      'Seleziona solo i filtri impostati e con un valore associato
      Return oCleFilt.GeneraQuerySQL(dttFilt.Select("xx_nome <> '-'"))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
    Return ""
  End Function
  Public Overridable Function GeneraQueryReport() As String
    Try
      'Seleziona solo i filtri impostati e con un valore associato
      Return oCleFilt.GeneraQueryReport(dttFilt.Select("xx_nome <> '-'"))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
    Return ""
  End Function

  Public Overridable Sub ApriZoomTabella(ByRef strIn As String, ByVal strCampo As String)
    'per eventuali altri controlli caricati al volo
    Dim strNomeZoom As String = ""
    Dim oParam As New CLE__PATB
    Try
      strNomeZoom = CType(oMenu.oCleComm, CLELBMENU).TrovaNomeZoomHlvl(strCampo)
      If strNomeZoom = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128586809070468750, "Zoom per campo |'" & strCampo & "'| non trovato (TrovaNomeZoomHlvl)"))
        Return
      End If

      If strNomeZoom.StartsWith("ZOOMANAGRA", StringComparison.CurrentCultureIgnoreCase) AndAlso _
        (oCleFilt.strProgrChiamante = "BSCGPART" OrElse oCleFilt.strProgrChiamante = "BSCGPART" OrElse _
         oCleFilt.strProgrChiamante = "BSCGSTPN" OrElse oCleFilt.strProgrChiamante = "BSCGSESC") Then strNomeZoom = "ZOOMANAGRAS"

      If strNomeZoom = "ZOOMHLVL" Then
        oParam.strTipo = strCampo.Split("."c)(0)
        oParam.strDescr = strCampo
      Else
        oParent.SetFastZoom(NTSCStr(grvFilt.EditingValue), oParam)    'abilito la gestione dello zoom veloce
      End If

      'Se sono in un campo "IN" non faccio la proposta del valore indicato, in quanto non sarebbe valido.
      If NTSCStr(grvFilt.NTSGetCurrentDataRow!xx_tipo) <> "IN" AndAlso NTSCStr(grvFilt.NTSGetCurrentDataRow!xx_tipo) <> "NOT IN" Then
        oParent.NTSZOOM.strIn = NTSCStr(grvFilt.EditingValue)
      End If
      oParent.NTSZOOM.ZoomStrIn(strNomeZoom, oParent.DittaCorrente, oParam)

      If oParent.NTSZOOM.strIn <> NTSCStr(grvFilt.EditingValue) Then
        If NTSCStr(grvFilt.NTSGetCurrentDataRow!xx_tipo) = "IN" OrElse NTSCStr(grvFilt.NTSGetCurrentDataRow!xx_tipo) = "NOT IN" Then
          If NTSCStr(grvFilt.EditingValue).Trim = "" Then
            grvFilt.SetFocusedValue(oParent.NTSZOOM.strIn)
          Else
            grvFilt.SetFocusedValue(NTSCStr(grvFilt.EditingValue) & ";" & oParent.NTSZOOM.strIn)
          End If
        Else
          grvFilt.SetFocusedValue(oParent.NTSZOOM.strIn)
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ZoomNomeCampi()
    Dim oPar As New CLE__CLDP
    Dim frmNomc As FRMXXNOMC
    Try
      oPar.ctlPar1 = oCleFilt.dttCampiTabelle

      frmNomc = CType(NTSNewFormModal("FRMXXNOMC"), FRMXXNOMC)
      If Not frmNomc.Init(oMenu, oPar, oParent.DittaCorrente) Then Return
      frmNomc.ShowDialog()
      If frmNomc.strNomeCampo <> "" Then
        grvFilt.NTSGetCurrentDataRow!xx_nome = frmNomc.strNomeCampo
        grvFilt.FocusedColumn = xx_valore
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
End Class
