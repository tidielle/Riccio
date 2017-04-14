Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGCACL
  Public oCleDocu As CLEMGDOCU
  Public oCallParams As CLE__CLDP
  Public dsCacl As New DataSet
  Public dcCacl As New BindingSource()

  Public components As System.ComponentModel.IContainer

#Region "Inizializzazione"
  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, _
    Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    '----------------------------------------------------------------------------------------------------------------
    oMenu = Menu
    oApp = oMenu.App
    oCallParams = Param
    '----------------------------------------------------------------------------------------------------------------
    If Ditta <> "" Then DittaCorrente = Ditta Else DittaCorrente = oApp.Ditta
    '----------------------------------------------------------------------------------------------------------------
    Me.GctlTipoDoc = ""
    '----------------------------------------------------------------------------------------------------------------
    InitializeComponent()
    Me.MinimumSize = Me.Size
    '----------------------------------------------------------------------------------------------------------------
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGDOCU", "BEMGDOCU", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 130086828294429425, "ERRORE in fase di creazione Entity:" & vbCrLf & strErr))
      Return False
    End If
    oCleDocu = CType(oTmp, CLEMGDOCU)
    '----------------------------------------------------------------------------------------------------------------
    bRemoting = Menu.Remoting("BNMGDOCU", strRemoteServer, strRemotePort)
    AddHandler oCleDocu.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleDocu.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    '----------------------------------------------------------------------------------------------------------------
    Return True
  End Function

  Private Sub InitializeComponent()
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.lbInfo = New NTSInformatica.NTSLabel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.grCacl = New NTSInformatica.NTSGrid
    Me.grvCacl = New NTSInformatica.NTSGridView
    Me.ae_forn = New NTSInformatica.NTSGridColumn
    Me.xx_forn = New NTSInformatica.NTSGridColumn
    Me.ae_siglaforn = New NTSInformatica.NTSGridColumn
    Me.ae_codmarc = New NTSInformatica.NTSGridColumn
    Me.xx_codmarc = New NTSInformatica.NTSGridColumn
    Me.ae_status = New NTSInformatica.NTSGridColumn
    Me.ae_descr = New NTSInformatica.NTSGridColumn
    Me.ae_desint = New NTSInformatica.NTSGridColumn
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.grCacl, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvCacl, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.lbInfo)
    Me.pnTop.Controls.Add(Me.cmdAnnulla)
    Me.pnTop.Controls.Add(Me.cmdConferma)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(701, 71)
    Me.pnTop.TabIndex = 5
    Me.pnTop.Text = "NtsPanel1"
    '
    'lbInfo
    '
    Me.lbInfo.BackColor = System.Drawing.Color.Transparent
    Me.lbInfo.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbInfo.Location = New System.Drawing.Point(12, 9)
    Me.lbInfo.Name = "lbInfo"
    Me.lbInfo.NTSDbField = ""
    Me.lbInfo.Size = New System.Drawing.Size(558, 59)
    Me.lbInfo.TabIndex = 668
    Me.lbInfo.Text = "Attenzione!" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Sono stati rilevati i seguenti articoli dal Catalogo Fornitori" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "da i" & _
        "mportare in Anagrafica Articoli." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
    Me.lbInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    Me.lbInfo.Tooltip = ""
    Me.lbInfo.UseMnemonic = False
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(576, 38)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(113, 24)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "Annulla"
    '
    'cmdConferma
    '
    Me.cmdConferma.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdConferma.ImageText = ""
    Me.cmdConferma.Location = New System.Drawing.Point(576, 12)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.NTSContextMenu = Nothing
    Me.cmdConferma.Size = New System.Drawing.Size(113, 24)
    Me.cmdConferma.TabIndex = 0
    Me.cmdConferma.Text = "Conferma"
    '
    'grCacl
    '
    Me.grCacl.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grCacl.EmbeddedNavigator.Name = ""
    Me.grCacl.Location = New System.Drawing.Point(0, 71)
    Me.grCacl.MainView = Me.grvCacl
    Me.grCacl.Name = "grCacl"
    Me.grCacl.Size = New System.Drawing.Size(701, 300)
    Me.grCacl.TabIndex = 6
    Me.grCacl.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvCacl})
    '
    'grvCacl
    '
    Me.grvCacl.ActiveFilterEnabled = False
    Me.grvCacl.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ae_forn, Me.xx_forn, Me.ae_siglaforn, Me.ae_codmarc, Me.xx_codmarc, Me.ae_status, Me.ae_descr, Me.ae_desint})
    Me.grvCacl.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvCacl.Enabled = True
    Me.grvCacl.GridControl = Me.grCacl
    Me.grvCacl.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvCacl.MinRowHeight = 14
    Me.grvCacl.Name = "grvCacl"
    Me.grvCacl.NTSAllowDelete = True
    Me.grvCacl.NTSAllowInsert = True
    Me.grvCacl.NTSAllowUpdate = True
    Me.grvCacl.NTSMenuContext = Nothing
    Me.grvCacl.OptionsCustomization.AllowRowSizing = True
    Me.grvCacl.OptionsFilter.AllowFilterEditor = False
    Me.grvCacl.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvCacl.OptionsNavigation.UseTabKey = False
    Me.grvCacl.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvCacl.OptionsView.ColumnAutoWidth = False
    Me.grvCacl.OptionsView.EnableAppearanceEvenRow = True
    Me.grvCacl.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvCacl.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvCacl.OptionsView.ShowGroupPanel = False
    Me.grvCacl.RowHeight = 16
    '
    'ae_forn
    '
    Me.ae_forn.AppearanceCell.Options.UseBackColor = True
    Me.ae_forn.AppearanceCell.Options.UseTextOptions = True
    Me.ae_forn.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ae_forn.Caption = "Fornitore"
    Me.ae_forn.Enabled = False
    Me.ae_forn.FieldName = "ae_forn"
    Me.ae_forn.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ae_forn.Name = "ae_forn"
    Me.ae_forn.NTSRepositoryComboBox = Nothing
    Me.ae_forn.NTSRepositoryItemCheck = Nothing
    Me.ae_forn.NTSRepositoryItemMemo = Nothing
    Me.ae_forn.NTSRepositoryItemText = Nothing
    Me.ae_forn.OptionsColumn.AllowEdit = False
    Me.ae_forn.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ae_forn.OptionsColumn.ReadOnly = True
    Me.ae_forn.OptionsFilter.AllowFilter = False
    Me.ae_forn.Visible = True
    Me.ae_forn.VisibleIndex = 0
    '
    'xx_forn
    '
    Me.xx_forn.AppearanceCell.Options.UseBackColor = True
    Me.xx_forn.AppearanceCell.Options.UseTextOptions = True
    Me.xx_forn.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_forn.Caption = "Descr. fornitore"
    Me.xx_forn.Enabled = False
    Me.xx_forn.FieldName = "xx_forn"
    Me.xx_forn.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_forn.Name = "xx_forn"
    Me.xx_forn.NTSRepositoryComboBox = Nothing
    Me.xx_forn.NTSRepositoryItemCheck = Nothing
    Me.xx_forn.NTSRepositoryItemMemo = Nothing
    Me.xx_forn.NTSRepositoryItemText = Nothing
    Me.xx_forn.OptionsColumn.AllowEdit = False
    Me.xx_forn.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_forn.OptionsColumn.ReadOnly = True
    Me.xx_forn.OptionsFilter.AllowFilter = False
    Me.xx_forn.Visible = True
    Me.xx_forn.VisibleIndex = 1
    '
    'ae_siglaforn
    '
    Me.ae_siglaforn.AppearanceCell.Options.UseBackColor = True
    Me.ae_siglaforn.AppearanceCell.Options.UseTextOptions = True
    Me.ae_siglaforn.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ae_siglaforn.Caption = "Sigla fornitore"
    Me.ae_siglaforn.Enabled = False
    Me.ae_siglaforn.FieldName = "ae_siglaforn"
    Me.ae_siglaforn.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ae_siglaforn.Name = "ae_siglaforn"
    Me.ae_siglaforn.NTSRepositoryComboBox = Nothing
    Me.ae_siglaforn.NTSRepositoryItemCheck = Nothing
    Me.ae_siglaforn.NTSRepositoryItemMemo = Nothing
    Me.ae_siglaforn.NTSRepositoryItemText = Nothing
    Me.ae_siglaforn.OptionsColumn.AllowEdit = False
    Me.ae_siglaforn.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ae_siglaforn.OptionsColumn.ReadOnly = True
    Me.ae_siglaforn.OptionsFilter.AllowFilter = False
    Me.ae_siglaforn.Visible = True
    Me.ae_siglaforn.VisibleIndex = 2
    '
    'ae_codmarc
    '
    Me.ae_codmarc.AppearanceCell.Options.UseBackColor = True
    Me.ae_codmarc.AppearanceCell.Options.UseTextOptions = True
    Me.ae_codmarc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ae_codmarc.Caption = "Marca"
    Me.ae_codmarc.Enabled = False
    Me.ae_codmarc.FieldName = "ae_codmarc"
    Me.ae_codmarc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ae_codmarc.Name = "ae_codmarc"
    Me.ae_codmarc.NTSRepositoryComboBox = Nothing
    Me.ae_codmarc.NTSRepositoryItemCheck = Nothing
    Me.ae_codmarc.NTSRepositoryItemMemo = Nothing
    Me.ae_codmarc.NTSRepositoryItemText = Nothing
    Me.ae_codmarc.OptionsColumn.AllowEdit = False
    Me.ae_codmarc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ae_codmarc.OptionsColumn.ReadOnly = True
    Me.ae_codmarc.OptionsFilter.AllowFilter = False
    Me.ae_codmarc.Visible = True
    Me.ae_codmarc.VisibleIndex = 3
    '
    'xx_codmarc
    '
    Me.xx_codmarc.AppearanceCell.Options.UseBackColor = True
    Me.xx_codmarc.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codmarc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codmarc.Caption = "Descr. marca"
    Me.xx_codmarc.Enabled = False
    Me.xx_codmarc.FieldName = "xx_codmarc"
    Me.xx_codmarc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codmarc.Name = "xx_codmarc"
    Me.xx_codmarc.NTSRepositoryComboBox = Nothing
    Me.xx_codmarc.NTSRepositoryItemCheck = Nothing
    Me.xx_codmarc.NTSRepositoryItemMemo = Nothing
    Me.xx_codmarc.NTSRepositoryItemText = Nothing
    Me.xx_codmarc.OptionsColumn.AllowEdit = False
    Me.xx_codmarc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codmarc.OptionsColumn.ReadOnly = True
    Me.xx_codmarc.OptionsFilter.AllowFilter = False
    Me.xx_codmarc.Visible = True
    Me.xx_codmarc.VisibleIndex = 4
    '
    'ae_status
    '
    Me.ae_status.AppearanceCell.Options.UseBackColor = True
    Me.ae_status.AppearanceCell.Options.UseTextOptions = True
    Me.ae_status.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ae_status.Caption = "Status"
    Me.ae_status.Enabled = False
    Me.ae_status.FieldName = "ae_status"
    Me.ae_status.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ae_status.Name = "ae_status"
    Me.ae_status.NTSRepositoryComboBox = Nothing
    Me.ae_status.NTSRepositoryItemCheck = Nothing
    Me.ae_status.NTSRepositoryItemMemo = Nothing
    Me.ae_status.NTSRepositoryItemText = Nothing
    Me.ae_status.OptionsColumn.AllowEdit = False
    Me.ae_status.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ae_status.OptionsColumn.ReadOnly = True
    Me.ae_status.OptionsFilter.AllowFilter = False
    Me.ae_status.Visible = True
    Me.ae_status.VisibleIndex = 5
    '
    'ae_descr
    '
    Me.ae_descr.AppearanceCell.Options.UseBackColor = True
    Me.ae_descr.AppearanceCell.Options.UseTextOptions = True
    Me.ae_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ae_descr.Caption = "Descrizione 1"
    Me.ae_descr.Enabled = False
    Me.ae_descr.FieldName = "ae_descr"
    Me.ae_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ae_descr.Name = "ae_descr"
    Me.ae_descr.NTSRepositoryComboBox = Nothing
    Me.ae_descr.NTSRepositoryItemCheck = Nothing
    Me.ae_descr.NTSRepositoryItemMemo = Nothing
    Me.ae_descr.NTSRepositoryItemText = Nothing
    Me.ae_descr.OptionsColumn.AllowEdit = False
    Me.ae_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ae_descr.OptionsColumn.ReadOnly = True
    Me.ae_descr.OptionsFilter.AllowFilter = False
    Me.ae_descr.Visible = True
    Me.ae_descr.VisibleIndex = 6
    '
    'ae_desint
    '
    Me.ae_desint.AppearanceCell.Options.UseBackColor = True
    Me.ae_desint.AppearanceCell.Options.UseTextOptions = True
    Me.ae_desint.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ae_desint.Caption = "Descrizione 2"
    Me.ae_desint.Enabled = False
    Me.ae_desint.FieldName = "ae_desint"
    Me.ae_desint.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ae_desint.Name = "ae_desint"
    Me.ae_desint.NTSRepositoryComboBox = Nothing
    Me.ae_desint.NTSRepositoryItemCheck = Nothing
    Me.ae_desint.NTSRepositoryItemMemo = Nothing
    Me.ae_desint.NTSRepositoryItemText = Nothing
    Me.ae_desint.OptionsColumn.AllowEdit = False
    Me.ae_desint.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ae_desint.OptionsColumn.ReadOnly = True
    Me.ae_desint.OptionsFilter.AllowFilter = False
    Me.ae_desint.Visible = True
    Me.ae_desint.VisibleIndex = 7
    '
    'FRMMGCACL
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(701, 371)
    Me.Controls.Add(Me.grCacl)
    Me.Controls.Add(Me.pnTop)
    Me.MinimizeBox = False
    Me.Name = "FRMMGCACL"
    Me.Text = "ARTICOLI CATALOGO CLIENTI/FORNITORI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    CType(Me.grCacl, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvCacl, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, False)
    '----------------------------------------------------------------------------------------------------------------
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim dttStatus As New DataTable
      dttStatus.Columns.Add("cod", GetType(String))
      dttStatus.Columns.Add("val", GetType(String))
      dttStatus.Rows.Add(New Object() {" ", "Valido/Attivo"})
      dttStatus.Rows.Add(New Object() {"D", "Cancellato"})
      dttStatus.Rows.Add(New Object() {"E", "In esaurimento"})
      dttStatus.Rows.Add(New Object() {"S", "Sostitutivo"})
      dttStatus.Rows.Add(New Object() {"V", "Veccio codice"})
      dttStatus.Rows.Add(New Object() {"A", "Altro"})
      dttStatus.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      grvCacl.NTSSetParam(oMenu, oApp.Tr(Me, 130086828516148175, "Articoli Catalogo Fornitori"))
      ae_forn.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128883227377170241, "Fornitore"), tabanagraf)
      xx_forn.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128883227377013988, "Descrizione fornitore"), 0, True)
      ae_siglaforn.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128883227377795253, "Sigla fornitore"), 0, True)
      ae_codmarc.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 130580300527753239, "Marca"), tabmarc)
      xx_codmarc.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130580300828550077, "Descrizione marca"), 0, True)
      ae_status.NTSSetParamCMB(oMenu, oApp.Tr(Me, 130580301965117551, "Status"), dttStatus, "val", "cod")
      ae_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128883227376545229, "Descrizione 1"), 0, True)
      ae_desint.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130580302399672518, "Descrizione 2"), 0, True)
      '--------------------------------------------------------------------------------------------------------------
      grvCacl.NTSAllowInsert = False
      grvCacl.NTSAllowUpdate = False
      grvCacl.NTSAllowDelete = False
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

  Public Overridable Sub InitEntity(ByRef cleDocu As CLEMGDOCU)
    oCleDocu = cleDocu
    AddHandler oCleDocu.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRMMGCACL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim dttTmp As New DataTable
    Try
      '--------------------------------------------------------------------------------------------------------------
      oCallParams.bPar1 = True
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      Apri()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMMGCACL_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcCacl.Dispose()
      dsCacl.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Pulsanti"
  Public Overridable Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      If grvCacl.NTSGetCurrentDataRow() Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      oCallParams.dPar1 = NTSCInt(grvCacl.NTSGetCurrentDataRow()!ae_forn)
      oCallParams.dPar2 = NTSCInt(grvCacl.NTSGetCurrentDataRow()!ae_codmarc)
      '--------------------------------------------------------------------------------------------------------------
      oCallParams.bPar1 = False
      '--------------------------------------------------------------------------------------------------------------
      Me.Close()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.Close()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi di Griglia"
  Public Overridable Sub grCacl_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grCacl.MouseDoubleClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      cmdConferma_Click(Me, Nothing)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Sub Apri()
    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleDocu.CaclApri(DittaCorrente, oCallParams.strPar1, dsCacl) Then
        Me.Close()
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      dcCacl.DataSource = Nothing
      dcCacl.DataSource = dsCacl.Tables("ARTEST")
      grCacl.DataSource = dcCacl
      '--------------------------------------------------------------------------------------------------------------
      grvCacl.NTSAllowInsert = False
      grvCacl.NTSAllowUpdate = False
      grvCacl.NTSAllowDelete = False
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.Default
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class