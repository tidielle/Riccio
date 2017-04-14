Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class NTSXXFLDO

  Public oCleFldo As CLEXXFLDO
  Public oCallParams As CLE__CLDP
  Public dsFldo As New DataSet
  Public dcFldo As New BindingSource
  Public oParent As FRM__CHIL = Nothing
  Public WithEvents VScrool As NTSInformatica.NTSVScrollBar
  Public WithEvents pnNodi As NTSInformatica.NTSPanel
  Public WithEvents HScrool As NTSInformatica.NTSHScrollBar
  Public WithEvents lbMemo As NTSInformatica.NTSLabel
  Public WithEvents pnFldo As NTSInformatica.NTSPanel
  Public arLinee As New ArrayList()
  Public dttLink As DataTable
  Public WithEvents pnSBS As NTSInformatica.NTSPanel
  Public WithEvents pnSbsTop As NTSInformatica.NTSPanel
  Public WithEvents cmdSbsApriDoc As NTSInformatica.NTSButton
  Public WithEvents cmdSbsRiparti As NTSInformatica.NTSButton
  Public WithEvents grFldo As NTSInformatica.NTSGrid
  Public WithEvents grvFldo As NTSInformatica.NTSGridView
  Public WithEvents xx_id As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descr As NTSInformatica.NTSGridColumn
  Public WithEvents xx_idsuc As NTSInformatica.NTSGridColumn
  Public WithEvents xx_idprec As NTSInformatica.NTSGridColumn
  Public WithEvents cmdSbsApriDocNoMod As NTSInformatica.NTSButton
  Public WithEvents cmdSbsStampa As NTSInformatica.NTSButton
  Public ptOrigin As Point


  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NTSXXFLDO))
    Me.pnFldo = New NTSInformatica.NTSPanel
    Me.pnSBS = New NTSInformatica.NTSPanel
    Me.grFldo = New NTSInformatica.NTSGrid
    Me.grvFldo = New NTSInformatica.NTSGridView
    Me.xx_id = New NTSInformatica.NTSGridColumn
    Me.xx_descr = New NTSInformatica.NTSGridColumn
    Me.xx_idprec = New NTSInformatica.NTSGridColumn
    Me.xx_idsuc = New NTSInformatica.NTSGridColumn
    Me.pnSbsTop = New NTSInformatica.NTSPanel
    Me.cmdSbsStampa = New NTSInformatica.NTSButton
    Me.cmdSbsApriDocNoMod = New NTSInformatica.NTSButton
    Me.cmdSbsApriDoc = New NTSInformatica.NTSButton
    Me.cmdSbsRiparti = New NTSInformatica.NTSButton
    Me.pnNodi = New NTSInformatica.NTSPanel
    Me.lbMemo = New NTSInformatica.NTSLabel
    Me.VScrool = New NTSInformatica.NTSVScrollBar
    Me.HScrool = New NTSInformatica.NTSHScrollBar
    CType(Me.pnFldo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFldo.SuspendLayout()
    CType(Me.pnSBS, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSBS.SuspendLayout()
    CType(Me.grFldo, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvFldo, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnSbsTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSbsTop.SuspendLayout()
    CType(Me.pnNodi, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnNodi.SuspendLayout()
    Me.SuspendLayout()
    '
    'pnFldo
    '
    Me.pnFldo.AllowDrop = True
    Me.pnFldo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnFldo.Appearance.Options.UseBackColor = True
    Me.pnFldo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnFldo.Controls.Add(Me.pnSBS)
    Me.pnFldo.Controls.Add(Me.pnNodi)
    Me.pnFldo.Controls.Add(Me.VScrool)
    Me.pnFldo.Controls.Add(Me.HScrool)
    Me.pnFldo.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnFldo.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnFldo.Location = New System.Drawing.Point(0, 0)
    Me.pnFldo.Name = "pnFldo"
    Me.pnFldo.NTSActiveTrasparency = True
    Me.pnFldo.Size = New System.Drawing.Size(400, 400)
    Me.pnFldo.TabIndex = 9
    Me.pnFldo.Text = "NtsPanel1"
    '
    'pnSBS
    '
    Me.pnSBS.AllowDrop = True
    Me.pnSBS.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSBS.Appearance.Options.UseBackColor = True
    Me.pnSBS.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSBS.Controls.Add(Me.grFldo)
    Me.pnSBS.Controls.Add(Me.pnSbsTop)
    Me.pnSBS.Location = New System.Drawing.Point(5, 96)
    Me.pnSBS.Name = "pnSBS"
    Me.pnSBS.NTSActiveTrasparency = True
    Me.pnSBS.Size = New System.Drawing.Size(362, 263)
    Me.pnSBS.TabIndex = 2
    Me.pnSBS.Text = "NtsPanel1"
    '
    'grFldo
    '
    Me.grFldo.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grFldo.EmbeddedNavigator.Name = ""
    Me.grFldo.Location = New System.Drawing.Point(0, 32)
    Me.grFldo.MainView = Me.grvFldo
    Me.grFldo.Name = "grFldo"
    Me.grFldo.Size = New System.Drawing.Size(362, 231)
    Me.grFldo.TabIndex = 6
    Me.grFldo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvFldo})
    '
    'grvFldo
    '
    Me.grvFldo.ActiveFilterEnabled = False
    Me.grvFldo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_id, Me.xx_descr, Me.xx_idprec, Me.xx_idsuc})
    Me.grvFldo.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvFldo.Enabled = True
    Me.grvFldo.GridControl = Me.grFldo
    Me.grvFldo.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvFldo.MinRowHeight = 14
    Me.grvFldo.Name = "grvFldo"
    Me.grvFldo.NTSAllowDelete = True
    Me.grvFldo.NTSAllowInsert = True
    Me.grvFldo.NTSAllowUpdate = True
    Me.grvFldo.NTSMenuContext = Nothing
    Me.grvFldo.OptionsCustomization.AllowRowSizing = True
    Me.grvFldo.OptionsFilter.AllowFilterEditor = False
    Me.grvFldo.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvFldo.OptionsNavigation.UseTabKey = False
    Me.grvFldo.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvFldo.OptionsView.ColumnAutoWidth = False
    Me.grvFldo.OptionsView.EnableAppearanceEvenRow = True
    Me.grvFldo.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvFldo.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvFldo.OptionsView.ShowGroupPanel = False
    Me.grvFldo.RowHeight = 16
    '
    'xx_id
    '
    Me.xx_id.AppearanceCell.Options.UseBackColor = True
    Me.xx_id.AppearanceCell.Options.UseTextOptions = True
    Me.xx_id.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_id.Caption = "ID"
    Me.xx_id.Enabled = True
    Me.xx_id.FieldName = "xx_id"
    Me.xx_id.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_id.Name = "xx_id"
    Me.xx_id.NTSRepositoryComboBox = Nothing
    Me.xx_id.NTSRepositoryItemCheck = Nothing
    Me.xx_id.NTSRepositoryItemMemo = Nothing
    Me.xx_id.NTSRepositoryItemText = Nothing
    Me.xx_id.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_id.OptionsFilter.AllowFilter = False
    Me.xx_id.Visible = True
    Me.xx_id.VisibleIndex = 0
    Me.xx_id.Width = 70
    '
    'xx_descr
    '
    Me.xx_descr.AppearanceCell.Options.UseBackColor = True
    Me.xx_descr.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descr.Caption = "Descrizione"
    Me.xx_descr.Enabled = True
    Me.xx_descr.FieldName = "xx_descr"
    Me.xx_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descr.Name = "xx_descr"
    Me.xx_descr.NTSRepositoryComboBox = Nothing
    Me.xx_descr.NTSRepositoryItemCheck = Nothing
    Me.xx_descr.NTSRepositoryItemMemo = Nothing
    Me.xx_descr.NTSRepositoryItemText = Nothing
    Me.xx_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descr.OptionsFilter.AllowFilter = False
    Me.xx_descr.Visible = True
    Me.xx_descr.VisibleIndex = 1
    Me.xx_descr.Width = 70
    '
    'xx_idprec
    '
    Me.xx_idprec.AppearanceCell.Options.UseBackColor = True
    Me.xx_idprec.AppearanceCell.Options.UseTextOptions = True
    Me.xx_idprec.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_idprec.Caption = "ID Precedente"
    Me.xx_idprec.Enabled = True
    Me.xx_idprec.FieldName = "xx_idprec"
    Me.xx_idprec.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_idprec.Name = "xx_idprec"
    Me.xx_idprec.NTSRepositoryComboBox = Nothing
    Me.xx_idprec.NTSRepositoryItemCheck = Nothing
    Me.xx_idprec.NTSRepositoryItemMemo = Nothing
    Me.xx_idprec.NTSRepositoryItemText = Nothing
    Me.xx_idprec.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_idprec.OptionsFilter.AllowFilter = False
    Me.xx_idprec.Visible = True
    Me.xx_idprec.VisibleIndex = 2
    '
    'xx_idsuc
    '
    Me.xx_idsuc.AppearanceCell.Options.UseBackColor = True
    Me.xx_idsuc.AppearanceCell.Options.UseTextOptions = True
    Me.xx_idsuc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_idsuc.Caption = "ID Successivo"
    Me.xx_idsuc.Enabled = True
    Me.xx_idsuc.FieldName = "xx_idsuc"
    Me.xx_idsuc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_idsuc.Name = "xx_idsuc"
    Me.xx_idsuc.NTSRepositoryComboBox = Nothing
    Me.xx_idsuc.NTSRepositoryItemCheck = Nothing
    Me.xx_idsuc.NTSRepositoryItemMemo = Nothing
    Me.xx_idsuc.NTSRepositoryItemText = Nothing
    Me.xx_idsuc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_idsuc.OptionsFilter.AllowFilter = False
    Me.xx_idsuc.Visible = True
    Me.xx_idsuc.VisibleIndex = 3
    Me.xx_idsuc.Width = 70
    '
    'pnSbsTop
    '
    Me.pnSbsTop.AllowDrop = True
    Me.pnSbsTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSbsTop.Appearance.Options.UseBackColor = True
    Me.pnSbsTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSbsTop.Controls.Add(Me.cmdSbsStampa)
    Me.pnSbsTop.Controls.Add(Me.cmdSbsApriDocNoMod)
    Me.pnSbsTop.Controls.Add(Me.cmdSbsApriDoc)
    Me.pnSbsTop.Controls.Add(Me.cmdSbsRiparti)
    Me.pnSbsTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnSbsTop.Location = New System.Drawing.Point(0, 0)
    Me.pnSbsTop.Name = "pnSbsTop"
    Me.pnSbsTop.NTSActiveTrasparency = True
    Me.pnSbsTop.Size = New System.Drawing.Size(362, 32)
    Me.pnSbsTop.TabIndex = 3
    Me.pnSbsTop.Text = "NtsPanel1"
    '
    'cmdSbsStampa
    '
    Me.cmdSbsStampa.ImagePath = ""
    Me.cmdSbsStampa.ImageText = ""
    Me.cmdSbsStampa.Location = New System.Drawing.Point(138, 3)
    Me.cmdSbsStampa.Name = "cmdSbsStampa"
    Me.cmdSbsStampa.NTSContextMenu = Nothing
    Me.cmdSbsStampa.Size = New System.Drawing.Size(129, 26)
    Me.cmdSbsStampa.TabIndex = 3
    Me.cmdSbsStampa.Text = "Stampa documento"
    '
    'cmdSbsApriDocNoMod
    '
    Me.cmdSbsApriDocNoMod.ImagePath = ""
    Me.cmdSbsApriDocNoMod.ImageText = ""
    Me.cmdSbsApriDocNoMod.Location = New System.Drawing.Point(416, 3)
    Me.cmdSbsApriDocNoMod.Name = "cmdSbsApriDocNoMod"
    Me.cmdSbsApriDocNoMod.NTSContextMenu = Nothing
    Me.cmdSbsApriDocNoMod.Size = New System.Drawing.Size(129, 26)
    Me.cmdSbsApriDocNoMod.TabIndex = 2
    Me.cmdSbsApriDocNoMod.Text = "Apri docum. non modale"
    '
    'cmdSbsApriDoc
    '
    Me.cmdSbsApriDoc.ImagePath = ""
    Me.cmdSbsApriDoc.ImageText = ""
    Me.cmdSbsApriDoc.Location = New System.Drawing.Point(281, 3)
    Me.cmdSbsApriDoc.Name = "cmdSbsApriDoc"
    Me.cmdSbsApriDoc.NTSContextMenu = Nothing
    Me.cmdSbsApriDoc.Size = New System.Drawing.Size(129, 26)
    Me.cmdSbsApriDoc.TabIndex = 1
    Me.cmdSbsApriDoc.Text = "Apri documento"
    '
    'cmdSbsRiparti
    '
    Me.cmdSbsRiparti.ImagePath = ""
    Me.cmdSbsRiparti.ImageText = ""
    Me.cmdSbsRiparti.Location = New System.Drawing.Point(3, 3)
    Me.cmdSbsRiparti.Name = "cmdSbsRiparti"
    Me.cmdSbsRiparti.NTSContextMenu = Nothing
    Me.cmdSbsRiparti.Size = New System.Drawing.Size(129, 26)
    Me.cmdSbsRiparti.TabIndex = 0
    Me.cmdSbsRiparti.Text = "Riparti da nodo"
    '
    'pnNodi
    '
    Me.pnNodi.AllowDrop = True
    Me.pnNodi.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnNodi.Appearance.Options.UseBackColor = True
    Me.pnNodi.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
    Me.pnNodi.Controls.Add(Me.lbMemo)
    Me.pnNodi.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnNodi.Location = New System.Drawing.Point(0, 0)
    Me.pnNodi.Name = "pnNodi"
    Me.pnNodi.NTSActiveTrasparency = True
    Me.pnNodi.Size = New System.Drawing.Size(367, 90)
    Me.pnNodi.TabIndex = 0
    Me.pnNodi.Text = "NtsPanel1"
    '
    'lbMemo
    '
    Me.lbMemo.AutoSize = True
    Me.lbMemo.BackColor = System.Drawing.Color.Transparent
    Me.lbMemo.Dock = System.Windows.Forms.DockStyle.Top
    Me.lbMemo.ForeColor = System.Drawing.Color.Gray
    Me.lbMemo.Location = New System.Drawing.Point(2, 2)
    Me.lbMemo.Name = "lbMemo"
    Me.lbMemo.NTSDbField = ""
    Me.lbMemo.Size = New System.Drawing.Size(507, 78)
    Me.lbMemo.TabIndex = 0
    Me.lbMemo.Text = resources.GetString("lbMemo.Text")
    Me.lbMemo.Tooltip = ""
    Me.lbMemo.UseMnemonic = False
    '
    'VScrool
    '
    Me.VScrool.Dock = System.Windows.Forms.DockStyle.Right
    Me.VScrool.LargeChange = 101
    Me.VScrool.Location = New System.Drawing.Point(383, 0)
    Me.VScrool.Name = "VScrool"
    Me.VScrool.Size = New System.Drawing.Size(17, 383)
    Me.VScrool.SmallChange = 50
    Me.VScrool.TabIndex = 0
    '
    'HScrool
    '
    Me.HScrool.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.HScrool.LargeChange = 101
    Me.HScrool.Location = New System.Drawing.Point(0, 383)
    Me.HScrool.Name = "HScrool"
    Me.HScrool.Size = New System.Drawing.Size(400, 17)
    Me.HScrool.SmallChange = 50
    Me.HScrool.TabIndex = 1
    '
    'NTSXXFLDO
    '
    Me.Controls.Add(Me.pnFldo)
    Me.Name = "NTSXXFLDO"
    Me.Size = New System.Drawing.Size(400, 400)
    CType(Me.pnFldo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFldo.ResumeLayout(False)
    CType(Me.pnSBS, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSBS.ResumeLayout(False)
    CType(Me.grFldo, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvFldo, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnSbsTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSbsTop.ResumeLayout(False)
    CType(Me.pnNodi, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnNodi.ResumeLayout(False)
    Me.pnNodi.PerformLayout()
    Me.ResumeLayout(False)

  End Sub

  Public Sub New()
    InitializeComponent()
    Me.MinimumSize = Me.Size
  End Sub

  Public Overloads Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    Dim oCtrl As Control = Nothing
    Try
      'per ora giro i messaggi al chiamante
      oParent.GestisciEventiEntity(sender, e)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Sub NTSSetParam(ByRef Menu As CLE__MENU, ByVal NomeCampo As String, ByVal ChildChiamante As String, ByRef Param As CLE__CLDP)
    Try
      '------------------------------------------------
      oMenu = Menu
      oApp = oMenu.oApp
      strNomeCampo = NomeCampo
      oCallParams = Param
      oParent = CType(Me.FindForm, FRM__CHIL)

      '------------------------------------------------
      'creo e attivo l'entity e inizializzo la funzione che dovrÃ  rilevare gli eventi dall'ENTITY
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNXXFLDO", "BEXXFLDO", oTmp, strErr, False, "", "") = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128271029889882656, "ERRORE in fase di creazione Entity:" & vbCrLf & strErr))
        Return
      End If
      oCleFldo = CType(oTmp, CLEXXFLDO)
      '------------------------------------------------
      oParent.bRemoting = Menu.Remoting("BNXXFLDO", oParent.strRemoteServer, oParent.strRemotePort)
      AddHandler oCleFldo.RemoteEvent, AddressOf GestisciEventiEntity
      If oCleFldo.Init(oApp, oParent.NTSScript, oMenu.oCleComm, "", oParent.bRemoting, oParent.strRemoteServer, oParent.strRemotePort) = False Then Return

      '------------------------------------------------
      'predispongo i controlli
      oCleFldo.strChildParent = ChildChiamante
      oCleFldo.strDittaCorrente = oParent.DittaCorrente

      grvFldo.NTSSetParam(oMenu, "")
      xx_id.NTSSetParamNUM(oMenu, "", "0", 9)
      xx_idprec.NTSSetParamSTR(oMenu, "", 0, True)
      xx_idsuc.NTSSetParamSTR(oMenu, "", 0, True)
      xx_descr.NTSSetParamSTR(oMenu, "", 0, True, True)
      grvFldo.NTSAllowInsert = False
      grvFldo.NTSAllowUpdate = False
      grvFldo.NTSAllowDelete = False
      grvFldo.Enabled = False
      If grvFldo.RowHeight < 50 Then grvFldo.RowHeight = 50

      PulisciFlusso()

      If CLN__STD.IsBis Or oMenu.bForzaFormX Then
        'NON UTILIZZO IL FLUSSO GRAFICO, ma uso la griglia
        pnNodi.Visible = False
        VScrool.Visible = False
        HScrool.Visible = False
        pnSBS.Visible = True

        pnSBS.Dock = DockStyle.Fill
        pnSBS.Visible = True
        'non faccio vedere 'apri non modale': già le risorse sono poche...
        cmdSbsApriDocNoMod.Visible = False
      Else
        pnSBS.Visible = False
      End If
      'per poter tradurre il testo del controllo bisogna passarlo alla funzione oapp.tr
      'in quanto il Designer di Visual Studio non assegna il testo
      'con le righe che vanno a capo direttamente alla proprietà Text
      'ma lo passa tramite funzione 
      'Me.lbMemo.Text = Resources.GetString("lbMemo.Text")
      lbMemo.Text = oApp.Tr(Me, 130858270917623716, _
                    "Doppio click sull'immagine per aprire il documento, CTRL+Doppio click per aprire il documento non modale" & vbCrLf & _
                    "Ctrl+click sull'immagine stampa il documento" & vbCrLf & _
                    "Click singolo sulla descrizione per aprire/chiudere informazioni aggiuntive sul nodo" & vbCrLf & _
                    "Alt+click sull'immagine  di un nodo per rieseguire il disegno del flusso partendo dal nodo su cui si è premuto" & vbCrLf & _
                    "Alt+click sullo sfondo stampa il flusso a video" & vbCrLf & _
                    "Click e tenere premuto sull'immagine per spostare un nodo")

      '------------------------------------------------
      'non riesco a fare meglio: alla chiusura della form svuoto i temporanei ...
      AddHandler pnNodi.FindForm.FormClosing, AddressOf FormParent_FormClosing

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub FormParent_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs)
    Try
      '-----------------------
      If oCleFldo.lIITTFldonod <> 0 Then oMenu.ResetTblInstId("TTFLDONOD", False, oCleFldo.lIITTFldonod)
      If oCleFldo.lIITTFldolin <> 0 Then oMenu.ResetTblInstId("TTFLDOLIN", False, oCleFldo.lIITTFldolin)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public ReadOnly Property NTSVisibleToUser() As Boolean
    'true se il controllo è visibile all'utente (potrebbe avere la proprietà visible = true 
    'ma essere in una pagina di tabstrip non visibile)
    Get
      Return True
    End Get
  End Property

  Public Overridable Function CalcolaFlusso(ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                                            ByVal lNumero As Integer, ByVal nVers As Integer, ByVal lConto As Integer, _
                                            ByVal strDatreg As String, ByVal lNumreg As Integer, ByVal nRigareg As Integer, _
                                            ByVal nAnnpar As Integer, ByVal strAlfpar As String, ByVal lNumpar As Integer, _
                                            ByVal nNumrata As Integer) As Boolean
    'dati gli estremi di un documento, ne calcolo il flusso
    'strTipork: "1" = CG, "2" = SC, altri tipi rk = come offerte, magazzino, ordini, ...
    Try
      Me.FindForm.Cursor = Cursors.WaitCursor

      PulisciFlusso()

      'passo i parametri di base
      oCleFldo.strTipork = strTipork
      oCleFldo.nAnno = nAnno
      oCleFldo.strSerie = strSerie
      oCleFldo.lNumero = lNumero
      oCleFldo.nVers = nVers
      oCleFldo.lConto = lConto
      oCleFldo.strDatreg = strDatreg
      oCleFldo.lNumreg = lNumreg
      oCleFldo.nRigareg = nRigareg
      oCleFldo.nAnnpar = nAnnpar
      oCleFldo.strAlfpar = strAlfpar
      oCleFldo.lNumpar = lNumpar
      oCleFldo.nNumrata = nNumrata

      If oCleFldo.CalcolaFlusso() Then
        If Not DisegnaFlusso() Then Return False
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.FindForm.Cursor = Cursors.Default
    End Try
  End Function
  Public Overridable Function PulisciFlusso() As Boolean
    Try
      lbMemo.Parent = pnFldo
      While pnNodi.Controls.Count > 0
        pnNodi.Controls(0).Dispose()
      End While
      arLinee.Clear()
      pnNodi.Refresh()
      lbMemo.Parent = pnNodi
      pnNodi.SendToBack()
      pnNodi.Left = 0
      pnNodi.Top = 0
      pnNodi.Height = 2000
      pnNodi.Width = 2000
      If pnNodi.Height - Me.Height < 0 Then
        pnNodi.Height = pnNodi.Width - (pnNodi.Height - Me.Height)
      End If
      If pnNodi.Width - Me.Width < 0 Then
        pnNodi.Width = pnNodi.Width - (pnNodi.Width - Me.Width)
      End If
      VScrool.Minimum = 0
      HScrool.Minimum = 0
      VScrool.Maximum = pnNodi.Height - Me.Height
      HScrool.Maximum = pnNodi.Width - Me.Width
      HScrool.Value = 0
      VScrool.Value = VScrool.Minimum

      HScrool.BringToFront()
      VScrool.BringToFront()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function DisegnaFlusso() As Boolean
    Dim dttNodi As New DataTable
    Dim i As Integer
    Dim lLeft As Integer
    Dim lTop As Integer
    Dim nTmpLevel As Integer
    Dim bFirst As Boolean
    Dim nNodo As Integer = 0
    Dim dtrN As DataRow = Nothing
    Dim ceNodo As NTSXXNODO = Nothing
    Dim nLivShift As Integer = 0
    Dim nNodoWidth As Integer = 170
    Dim lMaxTop As Integer = 0
    Dim strIdPrec As String = ""
    Dim strIdSuc As String = ""
    Try
      '--------------------
      'ottengo i nodi e i collegamenti
      If Not oCleFldo.GetFlusso(dttNodi, dttLink) Then Return False

      If CLN__STD.IsBis Or oMenu.bForzaFormX Then
        'uso la griglia per rappresentare il flusso, non il grafico
        grFldo.DataSource = Nothing
        If dsFldo.Tables.Count > 0 Then dsFldo.Tables.Clear()

        dsFldo.Tables.Add("FLDO")
        dsFldo.Tables("FLDO").Columns.Add("xx_id", GetType(Integer))
        dsFldo.Tables("FLDO").Columns.Add("xx_idprec", GetType(String))
        dsFldo.Tables("FLDO").Columns.Add("xx_idsuc", GetType(String))
        dsFldo.Tables("FLDO").Columns.Add("xx_descr", GetType(String))

        For Each dtrT As DataRow In dttNodi.Select("", "ttn_livello ASC, ttn_nodoid ASC")
          'calcolo nodo precedente
          strIdPrec = ""
          For Each dtrT1 As DataRow In dttLink.Select("ttl_nodoidr = " & dtrT!ttn_nodoid.ToString)
            strIdPrec += NTSCStr(dtrT1!ttl_nodoidl) & ", "
          Next
          If strIdPrec <> "" Then strIdPrec = strIdPrec.Substring(0, strIdPrec.Length - 2)

          'calcolo nodo successivo
          strIdSuc = ""
          For Each dtrT1 As DataRow In dttLink.Select("ttl_nodoidl = " & dtrT!ttn_nodoid.ToString)
            strIdSuc += NTSCStr(dtrT1!ttl_nodoidr) & ", "
          Next
          If strIdSuc <> "" Then strIdSuc = strIdSuc.Substring(0, strIdSuc.Length - 2)

          dsFldo.Tables("FLDO").Rows.Add(New Object() {dtrT!ttn_nodoid, strIdPrec, strIdSuc, DeterminaDescrizione(dtrT) & vbCrLf & NTSCStr(dtrT!ttn_note)})
        Next

        dsFldo.AcceptChanges()
        dcFldo.DataSource = dsFldo.Tables("FLDO")
        grFldo.DataSource = dcFldo

        Return True
      End If

      '--------------------
      'disegno i nodi
      lLeft = 10
      lTop = lbMemo.Height + 5
      lMaxTop = lTop
      nTmpLevel = -3
      bFirst = True
      For nNodo = 0 To dttNodi.Rows.Count - 1
        dtrN = dttNodi.Rows(nNodo)
        If nNodo = 0 Then nTmpLevel = NTSCInt(dtrN!ttn_livello)
        '----------------------
        i = NTSCInt(dtrN!ttn_nodoid)
        If nTmpLevel = NTSCInt(dtrN!ttn_livello) Then
          'sono sempre sullo stesso livello: non devo fare nulla
        Else
          'devo spostarmi di livello a dx e riposizionarmi in alto
          If bFirst = True Then
            nLivShift += 10
            lTop = lbMemo.Height + 5 + nLivShift
            If lMaxTop < lTop Then lMaxTop = lTop
            lLeft += nNodoWidth + 30      'il nodo 1 c'è sempre, visto 
            bFirst = False
          End If
        End If
        '--------------------
        'creo il nodo e lo posiziono: massimo 500 nodi!
        If i <= 500 Then
          ceNodo = New NTSXXNODO
          ceNodo.Name = "ceNodo_" & NTSCInt(dtrN!ttn_nodoid).ToString
          ceNodo.NTSSetParam(oMenu, "Nodo_" & NTSCInt(dtrN!ttn_nodoid).ToString, oCleFldo.strChildParent, Nothing)
          ceNodo.VisNote = False
          ceNodo.Parent = pnNodi
          nNodoWidth = ceNodo.Width
          ceNodo.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
          AddHandler ceNodo.pcImage.Click, AddressOf Nodo_pcImage_Click
          AddHandler ceNodo.pcImage.DoubleClick, AddressOf Nodo_pcImage_DoubleClick
          AddHandler ceNodo.pcImage.MouseMove, AddressOf Nodo_PcImage_MouseMove
          AddHandler ceNodo.pcImage.MouseDown, AddressOf Nodo_PcImage_MouseDown
          AddHandler ceNodo.pcImage.MouseUp, AddressOf Nodo_PcImage_MouseUp
          AddHandler ceNodo.lbTesto.Click, AddressOf Nodo_lbTesto_Click
          AddHandler ceNodo.lbNote.Click, AddressOf Nodo_lbTesto_Click
        Else
          dttNodi.Clear()
          Return True
        End If
        '--------------------
        ceNodo.Top = lTop
        If lMaxTop < lTop Then lMaxTop = lTop
        ceNodo.Left = lLeft
        ceNodo.IdNodo = i
        ceNodo.Picture = NTSCInt(dtrN!ttn_tipoogg).ToString
        ceNodo.Note = NTSCStr(dtrN!ttn_note)
        ceNodo.Caption = DeterminaDescrizione(dtrN)
        lTop += ceNodo.Height + 10
        If lMaxTop < lTop Then lMaxTop = lTop

        '----------------------
        nTmpLevel = NTSCInt(dtrN!ttn_livello)
        If nNodo + 1 < dttNodi.Rows.Count Then
          If nTmpLevel <> NTSCInt(dttNodi.Rows(nNodo + 1)!ttn_livello) Then bFirst = True
        End If
      Next

      RefreshLinee()

      lMaxTop += 200      'un po' di spazio in pù a fine pagina ...
      If lMaxTop < Me.Height Then lMaxTop = Me.Height
      pnNodi.Height = lMaxTop

      VScrool.Maximum = pnNodi.Height - Me.Height

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      dttNodi.Clear()
    End Try
  End Function
  Public Overridable Function DeterminaDescrizione(ByRef dtrT As DataRow) As String
    Dim strOut As String = ""
    Try
      '-------------------
      'A seconda dell'oggetto compongo la descrizione
      Select Case dtrT!ttn_tipoogg.ToString
        Case "1"   '--- Ordine/Impegno cliente
          Select Case dtrT!ttn_tipork.ToString
            Case "O" : strOut = oApp.Tr(Me, 128789954412089000, "Ord.Forn.")
            Case "H" : strOut = oApp.Tr(Me, 128789954833913000, "Ord.Prod.")
            Case "R" : strOut = oApp.Tr(Me, 128789954844209000, "Imp.Cli.")
            Case "V" : strOut = oApp.Tr(Me, 130056476569170614, "I.C. aperto")
            Case "$" : strOut = oApp.Tr(Me, 130056550464623151, "O.F. aperto")
          End Select
          strOut = strOut & " n. " & dtrT!ttn_numero.ToString & IIf(dtrT!ttn_serie.ToString <> " ", "/" & dtrT!ttn_serie.ToString, "").ToString
          If Not (CLN__STD.IsBis Or oMenu.bForzaFormX) Then strOut += vbCrLf
          strOut += " del " & NTSCDate(dtrT!ttn_datreg).ToShortDateString
        Case "2"   '--- D.D.T.
          Select Case dtrT!ttn_tipork.ToString
            Case "B" : strOut = oApp.Tr(Me, 128789954871353000, "DDT Em.")
            Case "M" : strOut = oApp.Tr(Me, 128789954879153000, "DDT Ric.")
            Case "T" : strOut = oApp.Tr(Me, 128789954890073000, "Car.Prod.")
          End Select
          strOut = strOut & " n. " & dtrT!ttn_numero.ToString & IIf(dtrT!ttn_serie.ToString <> " ", "/" & dtrT!ttn_serie.ToString, "").ToString
          If Not (CLN__STD.IsBis Or oMenu.bForzaFormX) Then strOut += vbCrLf
          strOut += " del " & NTSCDate(dtrT!ttn_datreg).ToShortDateString
        Case "3", "4"   '--- fatture
          Select Case dtrT!ttn_tipork.ToString
            Case "A" : strOut = oApp.Tr(Me, 128789954899589000, "Fatt.Imm.Em.")
            Case "C" : strOut = oApp.Tr(Me, 128789954918309000, "Corrisp.Em.")
            Case "D" : strOut = oApp.Tr(Me, 128789954929385000, "Fatt.Diff.Em.")
            Case "E" : strOut = oApp.Tr(Me, 128789954937809000, "Nota Add.Em.")
            Case "F" : strOut = oApp.Tr(Me, 128789954955125000, "Ric.Fisc.Em.")
            Case "J" : strOut = oApp.Tr(Me, 128789954968073000, "Nota Accr.Ric.")
            Case "(" : strOut = oApp.Tr(Me, 129243519262216797, "Nota Accr.Diff.Ric.")
            Case "K" : strOut = oApp.Tr(Me, 128789954976029000, "Fatt.Diff.Ric.")
            Case "L" : strOut = oApp.Tr(Me, 128789954983361000, "Fatt.Imm.Ric.")
            Case "N" : strOut = oApp.Tr(Me, 128789955019709000, "Nota Accr.Em.")
            Case "£" : strOut = oApp.Tr(Me, 129243519393496094, "Nota Accr.Diff.Em.")
            Case "S" : strOut = oApp.Tr(Me, 128789955026885000, "Fatt/Ric.fisc.Em.")
            Case "Z" : strOut = oApp.Tr(Me, 130626167983344605, "Bolla di Mov. Int.")
          End Select
          strOut = strOut & " n. " & dtrT!ttn_numero.ToString & IIf(dtrT!ttn_serie.ToString <> " ", "/" & dtrT!ttn_serie.ToString, "").ToString
          If Not (CLN__STD.IsBis Or oMenu.bForzaFormX) Then strOut += vbCrLf
          strOut += " del " & NTSCDate(dtrT!ttn_datreg).ToShortDateString
        Case "5"
          strOut = oApp.Tr(Me, 128789955035465000, "Reg. PN") & _
            " n." & dtrT!ttn_numreg.ToString
          If Not (CLN__STD.IsBis Or oMenu.bForzaFormX) Then strOut += vbCrLf
          strOut += " del " & NTSCDate(dtrT!ttn_datreg).ToShortDateString
          If InStr(1, NTSCStr(dtrT!ttn_note), "fat", vbTextCompare) > 0 Then strOut = strOut & " (FT)"
          If InStr(1, NTSCStr(dtrT!ttn_note), "not", vbTextCompare) > 0 Then strOut = strOut & " (NA)"
        Case "6"
          strOut = oApp.Tr(Me, 128789955045605000, "Scadenza") & _
            " n. " & dtrT!ttn_numpar.ToString & IIf(dtrT!ttn_alfpar.ToString <> " ", "/" & dtrT!ttn_alfpar.ToString, "").ToString
          If Not (CLN__STD.IsBis Or oMenu.bForzaFormX) Then strOut += vbCrLf
          strOut += " del " & dtrT!ttn_annpar.ToString & " Rata " & dtrT!ttn_numrata.ToString
          If Not (CLN__STD.IsBis Or oMenu.bForzaFormX) Then strOut += vbCrLf
          strOut += " scad. " & NTSCDate(dtrT!ttn_datreg).ToShortDateString
        Case "7"
          strOut = oApp.Tr(Me, 128789955064793000, "Off. N° ") & dtrT!ttn_numero.ToString & IIf(dtrT!ttn_serie.ToString <> " ", "/" & dtrT!ttn_serie.ToString, "").ToString & _
            " rev. " & dtrT!ttn_numreg.ToString
          If Not (CLN__STD.IsBis Or oMenu.bForzaFormX) Then strOut += vbCrLf
          strOut += "del " & dtrT!ttn_anno.ToString
        Case "8"   '--- Nota di prelievo
          strOut = oApp.Tr(Me, 128789955083201000, "Nota Prel.") & _
            " n. " & dtrT!ttn_numero.ToString & IIf(dtrT!ttn_serie.ToString <> " ", "/" & dtrT!ttn_serie.ToString, "").ToString
          If Not (CLN__STD.IsBis Or oMenu.bForzaFormX) Then strOut += vbCrLf
          strOut += " del " & NTSCDate(dtrT!ttn_datreg).ToShortDateString
      End Select
      strOut = strOut.Replace("/20", "/")     'tolgo il secolo all'anno

      Return strOut
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
      Return ""
    End Try
  End Function


  Public Overridable Sub VScrool_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles VScrool.Scroll
    Try
      pnNodi.Top = VScrool.Value * -1
      pnNodi.SendToBack()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub HScrool_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles HScrool.Scroll
    Try
      pnNodi.Left = HScrool.Value * -1
      pnNodi.SendToBack()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub NTSXXFLDO_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
    Try
      VScrool.Maximum = pnNodi.Height - Me.Height
      HScrool.Maximum = pnNodi.Width - Me.Width
      pnNodi.Top = VScrool.Value * -1
      pnNodi.Left = HScrool.Value * -1
      pnNodi.SendToBack()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub pnNodi_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnNodi.Paint
    If CLN__STD.IsBis Or oMenu.bForzaFormX Then Return

    e.Graphics.Clear(Color.White)
    Dim X1 As Integer, Y1 As Integer, X2 As Integer, Y2 As Integer
    Dim strT() As String
    Dim oColor As Color = Color.Blue
    For i As Integer = 0 To arLinee.Count - 1
      strT = arLinee.Item(i).ToString.Split(";"c)
      Y1 = NTSCInt(NTSCInt(strT(0)) * dMoltiplicatoreInterfaccia)
      X1 = NTSCInt(NTSCInt(strT(1)) * dMoltiplicatoreInterfaccia)
      Y2 = NTSCInt(NTSCInt(strT(2)) * dMoltiplicatoreInterfaccia)
      X2 = NTSCInt(NTSCInt(strT(3)) * dMoltiplicatoreInterfaccia)
      Select Case NTSCInt(strT(4))
        Case -3 : oColor = Color.Blue
        Case -2 : oColor = Color.Blue
        Case -1 : oColor = Color.Red
        Case 0 : oColor = Color.Green
        Case 1 : oColor = Color.Black
        Case 2 : oColor = Color.Violet
        Case 3 : oColor = Color.Brown
        Case 4 : oColor = Color.Blue
      End Select

      e.Graphics.DrawLine(New Pen(oColor), X1, Y1, X2, Y2)
    Next
  End Sub

  Public Overridable Sub Nodo_pcImage_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Dim dttTmp As New DataTable
    Dim strFldoTipork As String
    Dim strFldoSerie As String
    Dim strFldoDatreg As String
    Dim strFldoAlfpar As String
    Dim strFldoTipoogg As String
    Dim nFldoAnno As Integer
    Dim nFldoRigareg As Integer
    Dim nFldoAnnpar As Integer
    Dim nFldoNumrata As Integer
    Dim lFldoNumero As Integer
    Dim lFldoConto As Integer
    Dim lFldoNumreg As Integer
    Dim lFldoNumpar As Integer
    Dim strParam As String = ""
    Dim bChildNoModal As Boolean = False
    Try
      If (Control.ModifierKeys And Keys.Control) = Keys.Control Then bChildNoModal = True

      '-----------------------
      'apro il documento sottostante
      If CLN__STD.IsBis Or oMenu.bForzaFormX Then
        If grvFldo.NTSGetCurrentDataRow Is Nothing Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130360890856771278, "Posizionarsi su una riga di griglia"))
          Return
        End If
        If sender.Equals(cmdSbsApriDocNoMod) Then bChildNoModal = True
        If Not oCleFldo.GetRigaFlusso(NTSCInt(grvFldo.NTSGetCurrentDataRow!xx_id), dttTmp) Then Return
      Else
        If Not oCleFldo.GetRigaFlusso(CType(CType(CType(sender, NTSPictureBox).Parent, NTSPanel).Parent, NTSXXNODO).IdNodo, dttTmp) Then Return
      End If
      If dttTmp.Rows.Count = 0 Then Return
      Me.FindForm.Cursor = Cursors.WaitCursor
      With dttTmp.Rows(0)
        strFldoTipork = NTSCStr(!ttn_tipork)
        strFldoSerie = NTSCStr(!ttn_serie)
        strFldoDatreg = NTSCDate(!ttn_datreg).ToShortDateString
        strFldoAlfpar = NTSCStr(!ttn_alfpar)
        strFldoTipoogg = NTSCStr(!ttn_tipoogg)
        nFldoAnno = NTSCInt(!ttn_anno)
        nFldoRigareg = NTSCInt(!ttn_rigareg)
        nFldoAnnpar = NTSCInt(!ttn_annpar)
        nFldoNumrata = NTSCInt(!ttn_numrata)
        lFldoNumero = NTSCInt(!ttn_numero)
        lFldoConto = NTSCInt(!ttn_conto)
        lFldoNumreg = NTSCInt(!ttn_numreg)
        lFldoNumpar = NTSCInt(!ttn_numpar)
      End With

      Select Case strFldoTipoogg
        Case "5"
          'prima nota
          strParam = "APRI;" & _
                     strFldoDatreg & ";" & _
                     NTSCInt(lFldoNumreg).ToString("000000")
          oMenu.RunChild("BSCGPRIN", "CLSCGPRIN", oApp.Tr(Me, 128789319270472797, "Gestione prima nota"), CType(Me.FindForm, FRM__CHIL).DittaCorrente, "", "BNCGPRIN", Nothing, strParam, Not bChildNoModal, Not bChildNoModal)
        Case "6"
          'scadenze
          oApp.MsgBoxErr(oApp.Tr(Me, 128789318754268797, "Funzione attualmente non disponibile"))
        Case Else
          'documenti/note prel/ordini/offerte
          Select Case strFldoTipork
            Case "!"
              strParam = "APRI;" & nFldoAnno.ToString.PadLeft(4, "0"c) & ";" & _
                         strFldoSerie & ";" & _
                         lFldoNumero.ToString.PadLeft(9, "0"c) & ";" & _
                         lFldoNumreg.ToString.PadLeft(9, "0"c) & ";"
              oMenu.RunChild("BSCRGSOF", "CLSCRGSOF", oApp.Tr(Me, 128784095957782000, "Gestione offerte"), CType(Me.FindForm, FRM__CHIL).DittaCorrente, "", "", Nothing, strParam, Not bChildNoModal, Not bChildNoModal)
            Case "$", "H", "O", "Q", "R", "V", "X", "Y"
              strParam = "APRI;" & IIf(strFldoTipork = "Y", "H", strFldoTipork).ToString & ";" & _
                           nFldoAnno.ToString.PadLeft(4, "0"c) & ";" & _
                           strFldoSerie & ";" & _
                           lFldoNumero.ToString.PadLeft(9, "0"c) & ";"
              oMenu.RunChild("BSORGSOR", "CLSORGSOR", oApp.Tr(Me, 128784095937970000, "Gestione ordini/impegno"), CType(Me.FindForm, FRM__CHIL).DittaCorrente, "", "", Nothing, strParam, Not bChildNoModal, Not bChildNoModal)
            Case "D", "K", "P", "£", "("
              strParam = "APRI;" & strFldoTipork & ";" & _
                           nFldoAnno.ToString.PadLeft(4, "0"c) & ";" & _
                           strFldoSerie & ";" & _
                           lFldoNumero.ToString.PadLeft(9, "0"c) & ";"
              oMenu.RunChild("BSVEFDIN", "CLSVEFDIN", oApp.Tr(Me, 128784095921434000, "Fatturazione differita interattiva"), CType(Me.FindForm, FRM__CHIL).DittaCorrente, "", "", Nothing, strParam, Not bChildNoModal, Not bChildNoModal)
            Case Else
              strParam = "APRI;" & IIf(strFldoTipork = "U", "T", strFldoTipork).ToString & ";" & _
                           nFldoAnno.ToString.PadLeft(4, "0"c) & ";" & _
                           strFldoSerie & ";" & _
                           lFldoNumero.ToString.PadLeft(9, "0"c) & ";"


              If oCleFldo.IsDocRetail(strFldoTipork, nFldoAnno, strFldoSerie, lFldoNumero) Then
                oMenu.RunChild("BSVEBOLL", "CLSVEBOLL", oApp.Tr(Me, 130021109295484106, "Gestione documenti di magazzino"), CType(Me.FindForm, FRM__CHIL).DittaCorrente, "", "", Nothing, strParam, Not bChildNoModal, Not bChildNoModal)
              ElseIf oCleFldo.IsDocRetailNew(strFldoTipork, nFldoAnno, strFldoSerie, lFldoNumero) Then
                oMenu.RunChild("BSREGSRE", "CLSREGSRE", oApp.Tr(Me, 129588203437744141, "Gestione retail"), CType(Me.FindForm, FRM__CHIL).DittaCorrente, "", "", Nothing, strParam, Not bChildNoModal, Not bChildNoModal)
              Else
                oMenu.RunChild("BSVEBOLL", "CLSVEBOLL", oApp.Tr(Me, 128726153464687500, "Gestione documenti di magazzino"), CType(Me.FindForm, FRM__CHIL).DittaCorrente, "", "", Nothing, strParam, Not bChildNoModal, Not bChildNoModal)
              End If
          End Select

      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.FindForm.Cursor = Cursors.Default
      dttTmp.Clear()
    End Try
  End Sub
  Public Overridable Sub Nodo_pcImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Dim dttTmp As New DataTable
    Dim strFldoTipork As String
    Dim strFldoSerie As String
    Dim strFldoDatreg As String
    Dim strFldoAlfpar As String
    Dim strFldoTipoogg As String
    Dim nFldoAnno As Integer
    Dim nFldoRigareg As Integer
    Dim nFldoAnnpar As Integer
    Dim nFldoNumrata As Integer
    Dim lFldoNumero As Integer
    Dim lFldoConto As Integer
    Dim lFldoNumreg As Integer
    Dim lFldoNumpar As Integer

    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer
    Dim j As Integer
    Dim strKey2 As String = ""
    Dim strReportName As String = ""
    Dim nValuta As Integer = 0
    Dim bScorpo As Boolean = False
    Dim strPrzBoll As String = ""

    Dim nNodoStart As Integer = 0
    Try
      If sender.Equals(cmdSbsStampa) Then GoTo STAMPA

      '-----------------------
      'riparto da questo nodo, se non sono già il nodo di partenza ...
      If ((Control.ModifierKeys And Keys.Alt) = Keys.Alt And Not ((Control.ModifierKeys And Keys.Control) = Keys.Control)) Or _
          CLN__STD.IsBis Or oMenu.bForzaFormX Then

        If CLN__STD.IsBis Or oMenu.bForzaFormX Then
          If grvFldo.NTSGetCurrentDataRow Is Nothing Then
            oApp.MsgBoxErr(oApp.Tr(Me, 130360894701683960, "Posizionarsi su una riga di griglia"))
            Return
          End If
          nNodoStart = NTSCInt(grvFldo.NTSGetCurrentDataRow!xx_id)
        Else
          nNodoStart = CType(CType(CType(sender, NTSPictureBox).Parent, NTSPanel).Parent, NTSXXNODO).IdNodo
        End If

        If nNodoStart <> 1 Then
          If Not oCleFldo.GetRigaFlusso(nNodoStart, dttTmp) Then Return
          If dttTmp.Rows.Count = 0 Then Return
          Me.FindForm.Cursor = Cursors.WaitCursor
          With dttTmp.Rows(0)
            strFldoTipork = NTSCStr(!ttn_tipork)
            strFldoSerie = NTSCStr(!ttn_serie)
            strFldoDatreg = NTSCDate(!ttn_datreg).ToShortDateString
            strFldoAlfpar = NTSCStr(!ttn_alfpar)
            strFldoTipoogg = NTSCStr(!ttn_tipoogg)
            nFldoAnno = NTSCInt(!ttn_anno)
            nFldoRigareg = NTSCInt(!ttn_rigareg)
            nFldoAnnpar = NTSCInt(!ttn_annpar)
            nFldoNumrata = NTSCInt(!ttn_numrata)
            lFldoNumero = NTSCInt(!ttn_numero)
            lFldoConto = NTSCInt(!ttn_conto)
            lFldoNumreg = NTSCInt(!ttn_numreg)
            lFldoNumpar = NTSCInt(!ttn_numpar)
          End With

          Select Case strFldoTipoogg
            Case "5"
              'prima nota
              strFldoTipork = "1"
              If Not CalcolaFlusso(strFldoTipork, 0, "", 0, 0, lFldoConto, NTSCDate(strFldoDatreg).ToShortDateString, lFldoNumreg, nFldoRigareg, 0, "", 0, 0) Then
                Return
              End If
            Case "6"
              'scadenze
              strFldoTipork = "2"
              If Not CalcolaFlusso(strFldoTipork, 0, "", 0, 0, lFldoConto, "", 0, 0, nFldoAnnpar, strFldoAlfpar, lFldoNumpar, nFldoNumrata) Then
                Return
              End If
            Case Else
              'documenti
              If Not CalcolaFlusso(strFldoTipork, nFldoAnno, strFldoSerie, lFldoNumero, lFldoNumreg, 0, "", 0, 0, 0, "", 0, 0) Then
                Return
              End If
          End Select
        End If
        Return
      End If

      '-----------------------
      'stampo il documento: richiamo l'evenutale multireport
STAMPA:

      If (Not ((Control.ModifierKeys And Keys.Alt) = Keys.Alt) And (Control.ModifierKeys And Keys.Control) = Keys.Control) Or _
          CLN__STD.IsBis Or oMenu.bForzaFormX Then

        If CLN__STD.IsBis Or oMenu.bForzaFormX Then
          If grvFldo.NTSGetCurrentDataRow Is Nothing Then
            oApp.MsgBoxErr(oApp.Tr(Me, 130360902642821804, "Posizionarsi su una riga di griglia"))
            Return
          End If
          If Not oCleFldo.GetRigaFlusso(NTSCInt(grvFldo.NTSGetCurrentDataRow!xx_id), dttTmp) Then Return
        Else
          If Not oCleFldo.GetRigaFlusso(CType(CType(CType(sender, NTSPictureBox).Parent, NTSPanel).Parent, NTSXXNODO).IdNodo, dttTmp) Then Return
        End If

        If dttTmp.Rows.Count = 0 Then Return
        Me.FindForm.Cursor = Cursors.WaitCursor
        With dttTmp.Rows(0)
          strFldoTipork = NTSCStr(!ttn_tipork)
          strFldoSerie = NTSCStr(!ttn_serie)
          strFldoDatreg = NTSCDate(!ttn_datreg).ToShortDateString
          strFldoAlfpar = NTSCStr(!ttn_alfpar)
          strFldoTipoogg = NTSCStr(!ttn_tipoogg)
          nFldoAnno = NTSCInt(!ttn_anno)
          nFldoRigareg = NTSCInt(!ttn_rigareg)
          nFldoAnnpar = NTSCInt(!ttn_annpar)
          nFldoNumrata = NTSCInt(!ttn_numrata)
          lFldoNumero = NTSCInt(!ttn_numero)
          lFldoConto = NTSCInt(!ttn_conto)
          lFldoNumreg = NTSCInt(!ttn_numreg)
          lFldoNumpar = NTSCInt(!ttn_numpar)
        End With

        If strFldoTipoogg <> "5" And strFldoTipoogg <> "6" Then
          If Not oCleFldo.GetValutaScorpo(oCleFldo.strDittaCorrente, strFldoTipork, nFldoAnno, _
                                          strFldoSerie, lFldoNumero, 0, nValuta, bScorpo, strPrzBoll) Then Return
        End If

        Select Case strFldoTipoogg
          Case "7"  'offerte
            If nValuta <> 0 Then
              strKey2 = "Reports3"
            ElseIf bScorpo Then
              strKey2 = "Reports2"
            Else
              strKey2 = "Reports1"
            End If

            strReportName = "Bscrgsof.rpt"

            strCrpe = "{movoff.codditt} = '" & oCleFldo.strDittaCorrente & "'" & _
                     " And {movoff.mo_tipork} = '!'" & _
                     " And {movoff.mo_anno} = " & nFldoAnno & _
                     " AND {movoff.mo_serie} = '" & strFldoSerie & "'" & _
                     " AND {movoff.mo_numord} = " & lFldoNumero & _
                     " AND {movoff.mo_vers} = " & lFldoNumreg & _
                     " AND {movoff.mo_stasino} <> 'N'"

            nPjob = oMenu.ReportPEInit(oCleFldo.strDittaCorrente, CType(Me.FindForm, FRM__CHIL), "Bscrgsof", strKey2, strFldoTipork, 0, 0, strReportName, False, "Stampa offerte", False)
            If nPjob Is Nothing Then Return

            '--------------------------------------------------
            'lancio tutti gli eventuali reports (le righe che seguono gestiscono gi il multireport)
            For i = 1 To UBound(CType(nPjob, Array), 2)
              nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CType(Me.FindForm, FRM__CHIL).CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
              'le formule particolari calcolate da 'CrpeResolveFormula' (ci sono solo in bsveboll, bsorgsor e pochi altri programmi
              For j = 3 To 12
                If Trim(CStr(CType(nPjob, Array).GetValue(j, i))) <> "" Then
                  nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CStr(CType(nPjob, Array).GetValue(j, i)), CType(Me.FindForm, FRM__CHIL).CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(j + 10, i))))
                End If
              Next j
              nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
            Next


          Case "1"  'ordini/impegni
            If strFldoTipork = "Y" Then
              oApp.MsgBoxErr(oApp.Tr(Me, 127791222114843750, "Si possono stampare solo documenti :" & vbCrLf & " - Ordini di produzione;" & vbCrLf & " - Ordini fornitori;" & vbCrLf & " - Preventivi;" & vbCrLf & " - Impegni clienti;" & vbCrLf & " - Impegni di trasferimento;"))
              Return
            End If

            If nValuta <> 0 Then
              strKey2 = "Reports3"
            ElseIf bScorpo Then
              strKey2 = "Reports2"
            Else
              strKey2 = "Reports1"
            End If

            strReportName = "Bsorgsor.rpt"

            strCrpe = "{MOVORD.codditt} = '" & oCleFldo.strDittaCorrente & "'" & _
                      " And {MOVORD.mo_anno} = " & nFldoAnno & _
                      " AND {MOVORD.mo_numord} = " & lFldoNumero & _
                      " AND {MOVORD.mo_serie} = '" & strFldoSerie & "'" & _
                      " AND {MOVORD.mo_tipork} = '" & strFldoTipork & "'" & _
                      " AND {MOVORD.mo_stasino} <> 'N'" & _
                      " AND {TESTORD.td_magaz2} <> {KEYORD.ko_magaz}"
            nPjob = oMenu.ReportPEInit(oCleFldo.strDittaCorrente, CType(Me.FindForm, FRM__CHIL), "Bsorgsor", strKey2, strFldoTipork, 0, 0, strReportName, False, "Stampa Ordine", False)
            If nPjob Is Nothing Then Return

            '--------------------------------------------------
            'lancio tutti gli eventuali reports (le righe che seguono gestiscono gi il multireport)
            For i = 1 To UBound(CType(nPjob, Array), 2)
              nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CType(Me.FindForm, FRM__CHIL).CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
              'le formule particolari calcolate da 'CrpeResolveFormula' (ci sono solo in bsveboll, bsorgsor e pochi altri programmi
              For j = 3 To 12
                If Trim(CStr(CType(nPjob, Array).GetValue(j, i))) <> "" Then
                  nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CStr(CType(nPjob, Array).GetValue(j, i)), CType(Me.FindForm, FRM__CHIL).CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(j + 10, i))))
                End If
              Next j
              nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
            Next

          Case "2", "3", "8"  'documenti di magazzino /fatture immediate/nota di prelievo
            If strFldoTipork = "U" Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128659597701562500, "Non si possono stampare documenti di tipo 'Scarichi a produzione'"))
              Return
            End If

            If nValuta <> 0 Then
              strKey2 = "Reports3"
            ElseIf bScorpo Then
              strKey2 = "Reports2"
            Else
              strKey2 = "Reports1"
            End If

            Select Case strFldoTipork
              Case "A", "N", "E", "C", "J", "L"
                If nValuta <> 0 Then
                  strReportName = "BSVEFATV.RPT"
                ElseIf bScorpo Then
                  strReportName = "BSVEFATC.RPT"
                Else
                  strReportName = "BSVEFATI.RPT"
                End If
              Case "B", "M", "Z", "T"
                If nValuta <> 0 Then
                  strReportName = "BSVEBOLV.RPT"
                ElseIf bScorpo Then
                  strReportName = "BSVEBOLC.RPT"
                Else
                  strReportName = "BSVEBOLL.RPT"
                End If
              Case "W"
                If nValuta <> 0 Then
                  strReportName = "BSVEPRBV.RPT"
                ElseIf bScorpo Then
                  strReportName = "BSVEPRBC.RPT"
                Else
                  strReportName = "BSVEPRBN.RPT"
                End If
              Case "F", "I"
                If nValuta <> 0 Then
                  strReportName = "BSVERIFV.RPT"
                ElseIf bScorpo Then
                  strReportName = "BSVERIFC.RPT"
                Else
                  strReportName = "BSVERIFI.RPT"
                End If
              Case "S"
                If nValuta <> 0 Then
                  strReportName = "BSVEFRFV.RPT"
                ElseIf bScorpo Then
                  strReportName = "BSVEFRFC.RPT"
                Else
                  strReportName = "BSVEFRFI.RPT"
                End If
            End Select

            strCrpe = "{testmag.codditt} = '" & oCleFldo.strDittaCorrente & "'" & _
                    " And {testmag.tm_anno} = " & nFldoAnno & _
                    " AND {testmag.tm_numdoc} = " & lFldoNumero & _
                    " AND {testmag.tm_serie} = '" & strFldoSerie & "'" & _
                    " AND {testmag.tm_tipork} = '" & strFldoTipork & "'" & _
                    " AND {movmag.mm_stasino} <> 'N'"
            If CBool(Val(oMenu.GetSettingBus("Bsveboll", "Opzioni", ".", "UsaKeymag", "0", " ", "0"))) Then strCrpe = strCrpe & " AND {KEYMAG.km_magaz} = {MOVMAG.mm_magaz}"
            If strFldoTipork = "B" Then strCrpe = strCrpe & " AND {MOVMAG.mm_stasino}<>'D'"

            nPjob = oMenu.ReportPEInit(oCleFldo.strDittaCorrente, CType(Me.FindForm, FRM__CHIL), "BSVEBOLL", strKey2, strFldoTipork, 0, 0, strReportName, False, "Stampa documento", False)
            If nPjob Is Nothing Then Return

            '--------------------------------------------------
            'lancio tutti gli eventuali reports (le righe che seguono gestiscono gi il multireport)
            For i = 1 To UBound(CType(nPjob, Array), 2)
              If strFldoTipork = "B" Then
                nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "PREZZOINBOLLA", "'" & strPrzBoll & "'")
              End If


              nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CType(Me.FindForm, FRM__CHIL).CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
              'le formule particolari calcolate da 'CrpeResolveFormula' (ci sono solo in BSVEBOLL, BSVEBOLL e pochi altri programmi
              For j = 3 To 12
                If Trim(CStr(CType(nPjob, Array).GetValue(j, i))) <> "" Then
                  nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CStr(CType(nPjob, Array).GetValue(j, i)), CType(Me.FindForm, FRM__CHIL).CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(j + 10, i))))
                End If
              Next j
              nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
            Next


          Case "4"  'fatture differite
            strKey2 = "Reports1"
            strReportName = "BSVEFATD.rpt"
            If nValuta <> 0 Then
              strKey2 = "Reports3"
            ElseIf bScorpo Then
              strKey2 = "Reports2"
            Else
              strKey2 = "Reports1"
            End If
            Select Case strFldoTipork
              Case "D", "K", "£", "("
                If nValuta <> 0 Then
                  strReportName = "BSVEFADV.RPT"
                ElseIf bScorpo Then
                  strReportName = "BSVEFADC.RPT"
                Else
                  strReportName = "BSVEFATD.RPT"
                End If
              Case "P"
                If nValuta <> 0 Then
                  strReportName = "BSVEFRDV.RPT"
                ElseIf bScorpo Then
                  strReportName = "BSVEFRDC.RPT"
                Else
                  strReportName = "BSVEFRFD.RPT"
                End If
              Case Else
                oApp.MsgBoxErr(oApp.Tr(Me, 128727080808437500, "Tipo documento |" & strFldoTipork & "| non consentito."))
                Return
            End Select
            strCrpe = "{testmag.codditt} = '" & oCleFldo.strDittaCorrente & "'" & _
                      " And {testmag.tm_anno} = " & nFldoAnno & _
                      " AND {testmag.tm_numdoc} = " & lFldoNumero & _
                      " AND {testmag.tm_serie} = '" & strFldoSerie & "'" & _
                      " AND {testmag.tm_tipork} = '" & strFldoTipork & "'" & _
                      " AND {movmag.mm_stasino} <> 'N'" & _
                      " AND {movmag.mm_stasino} <> 'B'"
            If CBool(Val(oMenu.GetSettingBus("Bsvefdin", "Opzioni", ".", "UsaKeymag", "0", " ", "0"))) Then strCrpe = strCrpe & " AND {KEYMAG.km_magaz} = {MOVMAG.mm_magaz}"

            nPjob = oMenu.ReportPEInit(oCleFldo.strDittaCorrente, CType(Me.FindForm, FRM__CHIL), "BSVEFDIN", strKey2, strFldoTipork, 0, 0, strReportName, False, "Stampa documento", False)
            If nPjob Is Nothing Then Return

            '--------------------------------------------------
            'lancio tutti gli eventuali reports (le righe che seguono gestiscono gi il multireport)
            For i = 1 To UBound(CType(nPjob, Array), 2)
              nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CType(Me.FindForm, FRM__CHIL).CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
              'le formule particolari calcolate da 'CrpeResolveFormula' (ci sono solo in BSVEBOLL, BSVEBOLL e pochi altri programmi
              For j = 3 To 12
                If Trim(CStr(CType(nPjob, Array).GetValue(j, i))) <> "" Then
                  nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CStr(CType(nPjob, Array).GetValue(j, i)), CType(Me.FindForm, FRM__CHIL).CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(j + 10, i))))
                End If
              Next j
              nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
            Next

          Case "5"  'prima nota
            oApp.MsgBoxErr(oApp.Tr(Me, 129826886940192325, "Non è possibile stampare dei movimenti di prima nota"))
            Return

          Case "6"  'scadenze
            oApp.MsgBoxErr(oApp.Tr(Me, 129826886778020032, "Non è possibile stampare delle scadenze"))
            Return

        End Select
      End If


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.FindForm.Cursor = Cursors.Default
      dttTmp.Clear()
    End Try
  End Sub
  Public Overridable Sub Nodo_lbTesto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Dim dttTmp As New DataTable
    Dim oNodo As NTSXXNODO
    Try
      Me.FindForm.Cursor = Cursors.WaitCursor
      oNodo = CType(CType(CType(sender, NTSLabel).Parent, NTSPanel).Parent, NTSXXNODO)
      oNodo.VisNote = Not oNodo.VisNote

      oNodo.BringToFront()
      RefreshLinee()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.FindForm.Cursor = Cursors.Default
      dttTmp.Clear()
    End Try
  End Sub
  Public Overridable Sub pnNodi_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnNodi.MouseDown
    Try
      'alt + click sullo sfondo stampa il flusso ... da finire
      If (Control.ModifierKeys And Keys.Alt) = Keys.Alt Then
        Dim bmp As New Bitmap(pnNodi.Width, pnNodi.Height)
        pnFldo.DrawToBitmap(bmp, New Rectangle(0, 0, pnNodi.Width, pnNodi.Height))
        bmp.Save(oApp.AscDir & "\tmp.bmp")
        If CLN__STD.IsBis Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 130316543493825460, "Immagine |" & oApp.AscDir & "\tmp.bmp| creata correttamente"))
        Else
          CType(Me.FindForm(), FRM__CHIL).NTSProcessStart(oApp.AscDir & "\tmp.bmp", "")
        End If

        bmp.Dispose()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub Nodo_PcImage_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Try
      If e.Button = MouseButtons.Left Then
        Dim oNodo As NTSXXNODO = CType(CType(sender, NTSInformatica.NTSPictureBox).Parent.Parent, NTSXXNODO)
        oNodo.Top += e.Y - ptOrigin.Y
        oNodo.Left += e.X - ptOrigin.X
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub Nodo_PcImage_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Try
      If e.Button = MouseButtons.Left Then RefreshLinee()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub Nodo_PcImage_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Try
      If e.Button = MouseButtons.Left Then
        CType(CType(sender, NTSInformatica.NTSPictureBox).Parent.Parent, NTSXXNODO).BringToFront()
        ptOrigin = New Point(e.X, e.Y)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub RefreshLinee()
    Try
      If CLN__STD.IsBis Or oMenu.bForzaFormX Then Return

      Dim ctlNodoLeft As NTSXXNODO = Nothing
      Dim ctlNodoRight As NTSXXNODO = Nothing
      Dim X1 As Integer, Y1 As Integer, X2 As Integer, Y2 As Integer
      arLinee.Clear()
      pnNodi.Refresh()
      '--------------------
      'disegno i collegamenti
      For Each dtrN As DataRow In dttLink.Rows
        'cerco il nodo 1 e nodo 2
        ctlNodoLeft = CType(pnNodi.Controls("ceNodo_" & dtrN!ttl_nodoidl.ToString), NTSXXNODO)
        ctlNodoRight = CType(pnNodi.Controls("ceNodo_" & dtrN!ttl_nodoidr.ToString), NTSXXNODO)
        Y1 = ctlNodoLeft.Left + ctlNodoLeft.Width
        X1 = ctlNodoLeft.Top + NTSCInt(ctlNodoLeft.Height / 2)
        Y2 = ctlNodoRight.Left
        X2 = ctlNodoRight.Top + NTSCInt(ctlNodoRight.Height / 2)
        'creo la collezione di linee
        arLinee.Add(X1.ToString & ";" & Y1.ToString & ";" & X2.ToString & ";" & Y2.ToString & ";" & dtrN!ttl_attrib.ToString)
      Next
      pnFldo.Refresh()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub



  Public Overridable Sub cmdSbsRiparti_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSbsRiparti.Click
    Try
      Nodo_pcImage_Click(cmdSbsRiparti, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdSbsStampa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSbsStampa.Click
    Try
      Nodo_pcImage_Click(cmdSbsStampa, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdSbsApriDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSbsApriDoc.Click
    Try
      Nodo_pcImage_DoubleClick(cmdSbsApriDoc, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdSbsApriDocNoMod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSbsApriDocNoMod.Click
    Try
      Nodo_pcImage_DoubleClick(cmdSbsApriDocNoMod, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

End Class