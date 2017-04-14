Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGHLAP

#Region "Variabili"
  Public oCleArmd As CLEMGARMD
  Public oCallParams As CLE__CLDP
  Public dsHlap As New DataSet
  Public dcHlap As New BindingSource()
  Public dttHlap As DataTable = Nothing
  Public dtrHlap() As DataRow = Nothing
  Private components As System.ComponentModel.IContainer

  Public WithEvents grHlap As NTSInformatica.NTSGrid
  Public WithEvents grvHlap As NTSInformatica.NTSGridView
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem

  Public WithEvents codditt As NTSInformatica.NTSGridColumn
  Public WithEvents ap_codart As NTSInformatica.NTSGridColumn
  Public WithEvents ap_magaz As NTSInformatica.NTSGridColumn
  Public WithEvents xx_magaz As NTSInformatica.NTSGridColumn
  Public WithEvents ap_esist As NTSInformatica.NTSGridColumn
  Public WithEvents ap_ordin As NTSInformatica.NTSGridColumn
  Public WithEvents ap_impeg As NTSInformatica.NTSGridColumn
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents ap_prenot As NTSInformatica.NTSGridColumn
  Public WithEvents tlbMovimenti As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbOrdini As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbTaglie As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbNavigazione As NTSInformatica.NTSBarButtonItem
  Public WithEvents xx_dispnet As NTSInformatica.NTSGridColumn
  Public WithEvents ap_giaini As NTSInformatica.NTSGridColumn
  Public WithEvents ap_carfor As NTSInformatica.NTSGridColumn
  Public WithEvents ap_carpro As NTSInformatica.NTSGridColumn
  Public WithEvents ap_carvar As NTSInformatica.NTSGridColumn
  Public WithEvents ap_rescli As NTSInformatica.NTSGridColumn
  Public WithEvents xx_totcarichi As NTSInformatica.NTSGridColumn
  Public WithEvents ap_scacli As NTSInformatica.NTSGridColumn
  Public WithEvents ap_scapro As NTSInformatica.NTSGridColumn
  Public WithEvents ap_scavar As NTSInformatica.NTSGridColumn
  Public WithEvents ap_resfor As NTSInformatica.NTSGridColumn
  Public WithEvents xx_totscarichi As NTSInformatica.NTSGridColumn
  Public WithEvents xx_ordes As NTSInformatica.NTSGridColumn
  Public WithEvents xx_ordap As NTSInformatica.NTSGridColumn
  Public WithEvents xx_impeges As NTSInformatica.NTSGridColumn
  Public WithEvents xx_impegap As NTSInformatica.NTSGridColumn
  Public WithEvents xx_dispones As NTSInformatica.NTSGridColumn
  Public WithEvents tlbSaldoMatricoleAperte As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDettaglioProgressivi As NTSInformatica.NTSBarButtonItem
  Public WithEvents xx_disp As NTSInformatica.NTSGridColumn
#End Region

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

    '------------------------------------------------
    'creo e attivo l'entity e inizializzo la funzione che dovr√† rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGARMD", "BEMGARMD", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128550728307822408, "ERRORE in fase di creazione Entity:" & vbCrLf & strErr))
      Return False
    End If
    oCleArmd = CType(oTmp, CLEMGARMD)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGARMD", strRemoteServer, strRemotePort)
    AddHandler oCleArmd.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleArmd.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    oCleArmd.strPrgParent = oCallParams.strPar1
    oCleArmd.strHlapCodart = oCallParams.strPar2
    oCleArmd.dHlapFase = oCallParams.dPar1

    Return True
  End Function
  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGHLAP))
    Me.grHlap = New NTSInformatica.NTSGrid
    Me.grvHlap = New NTSInformatica.NTSGridView
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.ap_codart = New NTSInformatica.NTSGridColumn
    Me.ap_magaz = New NTSInformatica.NTSGridColumn
    Me.xx_magaz = New NTSInformatica.NTSGridColumn
    Me.ap_esist = New NTSInformatica.NTSGridColumn
    Me.ap_ordin = New NTSInformatica.NTSGridColumn
    Me.ap_impeg = New NTSInformatica.NTSGridColumn
    Me.ap_prenot = New NTSInformatica.NTSGridColumn
    Me.xx_disp = New NTSInformatica.NTSGridColumn
    Me.xx_dispnet = New NTSInformatica.NTSGridColumn
    Me.ap_giaini = New NTSInformatica.NTSGridColumn
    Me.ap_carfor = New NTSInformatica.NTSGridColumn
    Me.ap_carpro = New NTSInformatica.NTSGridColumn
    Me.ap_carvar = New NTSInformatica.NTSGridColumn
    Me.ap_rescli = New NTSInformatica.NTSGridColumn
    Me.xx_totcarichi = New NTSInformatica.NTSGridColumn
    Me.ap_scacli = New NTSInformatica.NTSGridColumn
    Me.ap_scapro = New NTSInformatica.NTSGridColumn
    Me.ap_scavar = New NTSInformatica.NTSGridColumn
    Me.ap_resfor = New NTSInformatica.NTSGridColumn
    Me.xx_totscarichi = New NTSInformatica.NTSGridColumn
    Me.xx_ordes = New NTSInformatica.NTSGridColumn
    Me.xx_ordap = New NTSInformatica.NTSGridColumn
    Me.xx_impeges = New NTSInformatica.NTSGridColumn
    Me.xx_impegap = New NTSInformatica.NTSGridColumn
    Me.xx_dispones = New NTSInformatica.NTSGridColumn
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbMovimenti = New NTSInformatica.NTSBarButtonItem
    Me.tlbSaldoMatricoleAperte = New NTSInformatica.NTSBarButtonItem
    Me.tlbOrdini = New NTSInformatica.NTSBarButtonItem
    Me.tlbTaglie = New NTSInformatica.NTSBarButtonItem
    Me.tlbNavigazione = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbDettaglioProgressivi = New NTSInformatica.NTSBarButtonItem
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grHlap, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvHlap, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Image = CType(resources.GetObject("frmPopup.Appearance.Image"), System.Drawing.Image)
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    Me.frmPopup.ClientSize = New System.Drawing.Size(77, 21)
    '
    'frmAuto
    '
    Me.frmAuto.Appearance.BackColor = System.Drawing.Color.Black
    Me.frmAuto.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmAuto.Appearance.Options.UseBackColor = True
    Me.frmAuto.Appearance.Options.UseImage = True
    '
    'grHlap
    '
    Me.grHlap.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grHlap.EmbeddedNavigator.Name = ""
    Me.grHlap.Location = New System.Drawing.Point(0, 30)
    Me.grHlap.MainView = Me.grvHlap
    Me.grHlap.Name = "grHlap"
    Me.grHlap.Size = New System.Drawing.Size(667, 336)
    Me.grHlap.TabIndex = 0
    Me.grHlap.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvHlap})
    '
    'grvHlap
    '
    Me.grvHlap.ActiveFilterEnabled = False
    Me.grvHlap.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.codditt, Me.ap_codart, Me.ap_magaz, Me.xx_magaz, Me.ap_esist, Me.ap_ordin, Me.ap_impeg, Me.ap_prenot, Me.xx_disp, Me.xx_dispnet, Me.ap_giaini, Me.ap_carfor, Me.ap_carpro, Me.ap_carvar, Me.ap_rescli, Me.xx_totcarichi, Me.ap_scacli, Me.ap_scapro, Me.ap_scavar, Me.ap_resfor, Me.xx_totscarichi, Me.xx_ordes, Me.xx_ordap, Me.xx_impeges, Me.xx_impegap, Me.xx_dispones})
    Me.grvHlap.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvHlap.Enabled = True
    Me.grvHlap.GridControl = Me.grHlap
    Me.grvHlap.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvHlap.MinRowHeight = 14
    Me.grvHlap.Name = "grvHlap"
    Me.grvHlap.NTSAllowDelete = True
    Me.grvHlap.NTSAllowInsert = True
    Me.grvHlap.NTSAllowUpdate = True
    Me.grvHlap.NTSMenuContext = Nothing
    Me.grvHlap.OptionsCustomization.AllowRowSizing = True
    Me.grvHlap.OptionsFilter.AllowFilterEditor = False
    Me.grvHlap.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvHlap.OptionsNavigation.UseTabKey = False
    Me.grvHlap.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvHlap.OptionsView.ColumnAutoWidth = False
    Me.grvHlap.OptionsView.EnableAppearanceEvenRow = True
    Me.grvHlap.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvHlap.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvHlap.OptionsView.ShowGroupPanel = False
    Me.grvHlap.RowHeight = 16
    '
    'codditt
    '
    Me.codditt.AppearanceCell.Options.UseBackColor = True
    Me.codditt.AppearanceCell.Options.UseTextOptions = True
    Me.codditt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.codditt.Caption = "Cod. ditta"
    Me.codditt.Enabled = False
    Me.codditt.FieldName = "codditt"
    Me.codditt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.codditt.Name = "codditt"
    Me.codditt.NTSRepositoryComboBox = Nothing
    Me.codditt.NTSRepositoryItemCheck = Nothing
    Me.codditt.NTSRepositoryItemMemo = Nothing
    Me.codditt.NTSRepositoryItemText = Nothing
    Me.codditt.OptionsColumn.AllowEdit = False
    Me.codditt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.codditt.OptionsColumn.ReadOnly = True
    Me.codditt.OptionsFilter.AllowFilter = False
    '
    'ap_codart
    '
    Me.ap_codart.AppearanceCell.Options.UseBackColor = True
    Me.ap_codart.AppearanceCell.Options.UseTextOptions = True
    Me.ap_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ap_codart.Caption = "Codice articolo"
    Me.ap_codart.Enabled = False
    Me.ap_codart.FieldName = "ap_codart"
    Me.ap_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ap_codart.Name = "ap_codart"
    Me.ap_codart.NTSRepositoryComboBox = Nothing
    Me.ap_codart.NTSRepositoryItemCheck = Nothing
    Me.ap_codart.NTSRepositoryItemMemo = Nothing
    Me.ap_codart.NTSRepositoryItemText = Nothing
    Me.ap_codart.OptionsColumn.AllowEdit = False
    Me.ap_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ap_codart.OptionsColumn.ReadOnly = True
    Me.ap_codart.OptionsFilter.AllowFilter = False
    '
    'ap_magaz
    '
    Me.ap_magaz.AppearanceCell.Options.UseBackColor = True
    Me.ap_magaz.AppearanceCell.Options.UseTextOptions = True
    Me.ap_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ap_magaz.Caption = "Codice"
    Me.ap_magaz.Enabled = False
    Me.ap_magaz.FieldName = "ap_magaz"
    Me.ap_magaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ap_magaz.Name = "ap_magaz"
    Me.ap_magaz.NTSRepositoryComboBox = Nothing
    Me.ap_magaz.NTSRepositoryItemCheck = Nothing
    Me.ap_magaz.NTSRepositoryItemMemo = Nothing
    Me.ap_magaz.NTSRepositoryItemText = Nothing
    Me.ap_magaz.OptionsColumn.AllowEdit = False
    Me.ap_magaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ap_magaz.OptionsColumn.ReadOnly = True
    Me.ap_magaz.OptionsFilter.AllowFilter = False
    Me.ap_magaz.Visible = True
    Me.ap_magaz.VisibleIndex = 0
    '
    'xx_magaz
    '
    Me.xx_magaz.AppearanceCell.Options.UseBackColor = True
    Me.xx_magaz.AppearanceCell.Options.UseTextOptions = True
    Me.xx_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_magaz.Caption = "Magazzino"
    Me.xx_magaz.Enabled = False
    Me.xx_magaz.FieldName = "xx_magaz"
    Me.xx_magaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_magaz.Name = "xx_magaz"
    Me.xx_magaz.NTSRepositoryComboBox = Nothing
    Me.xx_magaz.NTSRepositoryItemCheck = Nothing
    Me.xx_magaz.NTSRepositoryItemMemo = Nothing
    Me.xx_magaz.NTSRepositoryItemText = Nothing
    Me.xx_magaz.OptionsColumn.AllowEdit = False
    Me.xx_magaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_magaz.OptionsColumn.ReadOnly = True
    Me.xx_magaz.OptionsFilter.AllowFilter = False
    Me.xx_magaz.Visible = True
    Me.xx_magaz.VisibleIndex = 1
    '
    'ap_esist
    '
    Me.ap_esist.AppearanceCell.Options.UseBackColor = True
    Me.ap_esist.AppearanceCell.Options.UseTextOptions = True
    Me.ap_esist.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ap_esist.Caption = "Esistenza"
    Me.ap_esist.Enabled = False
    Me.ap_esist.FieldName = "ap_esist"
    Me.ap_esist.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ap_esist.Name = "ap_esist"
    Me.ap_esist.NTSRepositoryComboBox = Nothing
    Me.ap_esist.NTSRepositoryItemCheck = Nothing
    Me.ap_esist.NTSRepositoryItemMemo = Nothing
    Me.ap_esist.NTSRepositoryItemText = Nothing
    Me.ap_esist.OptionsColumn.AllowEdit = False
    Me.ap_esist.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ap_esist.OptionsColumn.ReadOnly = True
    Me.ap_esist.OptionsFilter.AllowFilter = False
    Me.ap_esist.Visible = True
    Me.ap_esist.VisibleIndex = 2
    '
    'ap_ordin
    '
    Me.ap_ordin.AppearanceCell.Options.UseBackColor = True
    Me.ap_ordin.AppearanceCell.Options.UseTextOptions = True
    Me.ap_ordin.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ap_ordin.Caption = "Ordinato"
    Me.ap_ordin.Enabled = False
    Me.ap_ordin.FieldName = "ap_ordin"
    Me.ap_ordin.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ap_ordin.Name = "ap_ordin"
    Me.ap_ordin.NTSRepositoryComboBox = Nothing
    Me.ap_ordin.NTSRepositoryItemCheck = Nothing
    Me.ap_ordin.NTSRepositoryItemMemo = Nothing
    Me.ap_ordin.NTSRepositoryItemText = Nothing
    Me.ap_ordin.OptionsColumn.AllowEdit = False
    Me.ap_ordin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ap_ordin.OptionsColumn.ReadOnly = True
    Me.ap_ordin.OptionsFilter.AllowFilter = False
    Me.ap_ordin.Visible = True
    Me.ap_ordin.VisibleIndex = 3
    '
    'ap_impeg
    '
    Me.ap_impeg.AppearanceCell.Options.UseBackColor = True
    Me.ap_impeg.AppearanceCell.Options.UseTextOptions = True
    Me.ap_impeg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ap_impeg.Caption = "Impegnato"
    Me.ap_impeg.Enabled = False
    Me.ap_impeg.FieldName = "ap_impeg"
    Me.ap_impeg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ap_impeg.Name = "ap_impeg"
    Me.ap_impeg.NTSRepositoryComboBox = Nothing
    Me.ap_impeg.NTSRepositoryItemCheck = Nothing
    Me.ap_impeg.NTSRepositoryItemMemo = Nothing
    Me.ap_impeg.NTSRepositoryItemText = Nothing
    Me.ap_impeg.OptionsColumn.AllowEdit = False
    Me.ap_impeg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ap_impeg.OptionsColumn.ReadOnly = True
    Me.ap_impeg.OptionsFilter.AllowFilter = False
    Me.ap_impeg.Visible = True
    Me.ap_impeg.VisibleIndex = 4
    '
    'ap_prenot
    '
    Me.ap_prenot.AppearanceCell.Options.UseBackColor = True
    Me.ap_prenot.AppearanceCell.Options.UseTextOptions = True
    Me.ap_prenot.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ap_prenot.Caption = "Prenotato"
    Me.ap_prenot.Enabled = False
    Me.ap_prenot.FieldName = "ap_prenot"
    Me.ap_prenot.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ap_prenot.Name = "ap_prenot"
    Me.ap_prenot.NTSRepositoryComboBox = Nothing
    Me.ap_prenot.NTSRepositoryItemCheck = Nothing
    Me.ap_prenot.NTSRepositoryItemMemo = Nothing
    Me.ap_prenot.NTSRepositoryItemText = Nothing
    Me.ap_prenot.OptionsColumn.AllowEdit = False
    Me.ap_prenot.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ap_prenot.OptionsColumn.ReadOnly = True
    Me.ap_prenot.OptionsFilter.AllowFilter = False
    Me.ap_prenot.Visible = True
    Me.ap_prenot.VisibleIndex = 5
    '
    'xx_disp
    '
    Me.xx_disp.AppearanceCell.Options.UseBackColor = True
    Me.xx_disp.AppearanceCell.Options.UseTextOptions = True
    Me.xx_disp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_disp.Caption = "Disponibilit‡"
    Me.xx_disp.Enabled = False
    Me.xx_disp.FieldName = "xx_disp"
    Me.xx_disp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_disp.Name = "xx_disp"
    Me.xx_disp.NTSRepositoryComboBox = Nothing
    Me.xx_disp.NTSRepositoryItemCheck = Nothing
    Me.xx_disp.NTSRepositoryItemMemo = Nothing
    Me.xx_disp.NTSRepositoryItemText = Nothing
    Me.xx_disp.OptionsColumn.AllowEdit = False
    Me.xx_disp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_disp.OptionsColumn.ReadOnly = True
    Me.xx_disp.OptionsFilter.AllowFilter = False
    Me.xx_disp.Visible = True
    Me.xx_disp.VisibleIndex = 6
    '
    'xx_dispnet
    '
    Me.xx_dispnet.AppearanceCell.Options.UseBackColor = True
    Me.xx_dispnet.AppearanceCell.Options.UseTextOptions = True
    Me.xx_dispnet.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_dispnet.Caption = "Disponibilit‡ netta"
    Me.xx_dispnet.Enabled = False
    Me.xx_dispnet.FieldName = "xx_dispnet"
    Me.xx_dispnet.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_dispnet.Name = "xx_dispnet"
    Me.xx_dispnet.NTSRepositoryComboBox = Nothing
    Me.xx_dispnet.NTSRepositoryItemCheck = Nothing
    Me.xx_dispnet.NTSRepositoryItemMemo = Nothing
    Me.xx_dispnet.NTSRepositoryItemText = Nothing
    Me.xx_dispnet.OptionsColumn.AllowEdit = False
    Me.xx_dispnet.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_dispnet.OptionsColumn.ReadOnly = True
    Me.xx_dispnet.OptionsFilter.AllowFilter = False
    Me.xx_dispnet.Visible = True
    Me.xx_dispnet.VisibleIndex = 7
    '
    'ap_giaini
    '
    Me.ap_giaini.AppearanceCell.Options.UseBackColor = True
    Me.ap_giaini.AppearanceCell.Options.UseTextOptions = True
    Me.ap_giaini.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ap_giaini.Caption = "Giacenza iniziale"
    Me.ap_giaini.Enabled = False
    Me.ap_giaini.FieldName = "ap_giaini"
    Me.ap_giaini.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ap_giaini.Name = "ap_giaini"
    Me.ap_giaini.NTSRepositoryComboBox = Nothing
    Me.ap_giaini.NTSRepositoryItemCheck = Nothing
    Me.ap_giaini.NTSRepositoryItemMemo = Nothing
    Me.ap_giaini.NTSRepositoryItemText = Nothing
    Me.ap_giaini.OptionsColumn.AllowEdit = False
    Me.ap_giaini.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ap_giaini.OptionsColumn.ReadOnly = True
    Me.ap_giaini.OptionsFilter.AllowFilter = False
    Me.ap_giaini.Visible = True
    Me.ap_giaini.VisibleIndex = 8
    '
    'ap_carfor
    '
    Me.ap_carfor.AppearanceCell.Options.UseBackColor = True
    Me.ap_carfor.AppearanceCell.Options.UseTextOptions = True
    Me.ap_carfor.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ap_carfor.Caption = "Carichi fornitore"
    Me.ap_carfor.Enabled = False
    Me.ap_carfor.FieldName = "ap_carfor"
    Me.ap_carfor.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ap_carfor.Name = "ap_carfor"
    Me.ap_carfor.NTSRepositoryComboBox = Nothing
    Me.ap_carfor.NTSRepositoryItemCheck = Nothing
    Me.ap_carfor.NTSRepositoryItemMemo = Nothing
    Me.ap_carfor.NTSRepositoryItemText = Nothing
    Me.ap_carfor.OptionsColumn.AllowEdit = False
    Me.ap_carfor.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ap_carfor.OptionsColumn.ReadOnly = True
    Me.ap_carfor.OptionsFilter.AllowFilter = False
    Me.ap_carfor.Visible = True
    Me.ap_carfor.VisibleIndex = 9
    '
    'ap_carpro
    '
    Me.ap_carpro.AppearanceCell.Options.UseBackColor = True
    Me.ap_carpro.AppearanceCell.Options.UseTextOptions = True
    Me.ap_carpro.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ap_carpro.Caption = "Carichi produzione"
    Me.ap_carpro.Enabled = False
    Me.ap_carpro.FieldName = "ap_carpro"
    Me.ap_carpro.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ap_carpro.Name = "ap_carpro"
    Me.ap_carpro.NTSRepositoryComboBox = Nothing
    Me.ap_carpro.NTSRepositoryItemCheck = Nothing
    Me.ap_carpro.NTSRepositoryItemMemo = Nothing
    Me.ap_carpro.NTSRepositoryItemText = Nothing
    Me.ap_carpro.OptionsColumn.AllowEdit = False
    Me.ap_carpro.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ap_carpro.OptionsColumn.ReadOnly = True
    Me.ap_carpro.OptionsFilter.AllowFilter = False
    Me.ap_carpro.Visible = True
    Me.ap_carpro.VisibleIndex = 10
    '
    'ap_carvar
    '
    Me.ap_carvar.AppearanceCell.Options.UseBackColor = True
    Me.ap_carvar.AppearanceCell.Options.UseTextOptions = True
    Me.ap_carvar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ap_carvar.Caption = "Carichi vari"
    Me.ap_carvar.Enabled = False
    Me.ap_carvar.FieldName = "ap_carvar"
    Me.ap_carvar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ap_carvar.Name = "ap_carvar"
    Me.ap_carvar.NTSRepositoryComboBox = Nothing
    Me.ap_carvar.NTSRepositoryItemCheck = Nothing
    Me.ap_carvar.NTSRepositoryItemMemo = Nothing
    Me.ap_carvar.NTSRepositoryItemText = Nothing
    Me.ap_carvar.OptionsColumn.AllowEdit = False
    Me.ap_carvar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ap_carvar.OptionsColumn.ReadOnly = True
    Me.ap_carvar.OptionsFilter.AllowFilter = False
    Me.ap_carvar.Visible = True
    Me.ap_carvar.VisibleIndex = 11
    '
    'ap_rescli
    '
    Me.ap_rescli.AppearanceCell.Options.UseBackColor = True
    Me.ap_rescli.AppearanceCell.Options.UseTextOptions = True
    Me.ap_rescli.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ap_rescli.Caption = "Resi da clienti"
    Me.ap_rescli.Enabled = False
    Me.ap_rescli.FieldName = "ap_rescli"
    Me.ap_rescli.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ap_rescli.Name = "ap_rescli"
    Me.ap_rescli.NTSRepositoryComboBox = Nothing
    Me.ap_rescli.NTSRepositoryItemCheck = Nothing
    Me.ap_rescli.NTSRepositoryItemMemo = Nothing
    Me.ap_rescli.NTSRepositoryItemText = Nothing
    Me.ap_rescli.OptionsColumn.AllowEdit = False
    Me.ap_rescli.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ap_rescli.OptionsColumn.ReadOnly = True
    Me.ap_rescli.OptionsFilter.AllowFilter = False
    Me.ap_rescli.Visible = True
    Me.ap_rescli.VisibleIndex = 12
    '
    'xx_totcarichi
    '
    Me.xx_totcarichi.AppearanceCell.Options.UseBackColor = True
    Me.xx_totcarichi.AppearanceCell.Options.UseTextOptions = True
    Me.xx_totcarichi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_totcarichi.Caption = "Totale carichi"
    Me.xx_totcarichi.Enabled = False
    Me.xx_totcarichi.FieldName = "xx_totcarichi"
    Me.xx_totcarichi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_totcarichi.Name = "xx_totcarichi"
    Me.xx_totcarichi.NTSRepositoryComboBox = Nothing
    Me.xx_totcarichi.NTSRepositoryItemCheck = Nothing
    Me.xx_totcarichi.NTSRepositoryItemMemo = Nothing
    Me.xx_totcarichi.NTSRepositoryItemText = Nothing
    Me.xx_totcarichi.OptionsColumn.AllowEdit = False
    Me.xx_totcarichi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_totcarichi.OptionsColumn.ReadOnly = True
    Me.xx_totcarichi.OptionsFilter.AllowFilter = False
    Me.xx_totcarichi.Visible = True
    Me.xx_totcarichi.VisibleIndex = 13
    '
    'ap_scacli
    '
    Me.ap_scacli.AppearanceCell.Options.UseBackColor = True
    Me.ap_scacli.AppearanceCell.Options.UseTextOptions = True
    Me.ap_scacli.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ap_scacli.Caption = "Scarichi a clienti"
    Me.ap_scacli.Enabled = False
    Me.ap_scacli.FieldName = "ap_scacli"
    Me.ap_scacli.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ap_scacli.Name = "ap_scacli"
    Me.ap_scacli.NTSRepositoryComboBox = Nothing
    Me.ap_scacli.NTSRepositoryItemCheck = Nothing
    Me.ap_scacli.NTSRepositoryItemMemo = Nothing
    Me.ap_scacli.NTSRepositoryItemText = Nothing
    Me.ap_scacli.OptionsColumn.AllowEdit = False
    Me.ap_scacli.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ap_scacli.OptionsColumn.ReadOnly = True
    Me.ap_scacli.OptionsFilter.AllowFilter = False
    Me.ap_scacli.Visible = True
    Me.ap_scacli.VisibleIndex = 14
    '
    'ap_scapro
    '
    Me.ap_scapro.AppearanceCell.Options.UseBackColor = True
    Me.ap_scapro.AppearanceCell.Options.UseTextOptions = True
    Me.ap_scapro.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ap_scapro.Caption = "Scarichi produzione"
    Me.ap_scapro.Enabled = False
    Me.ap_scapro.FieldName = "ap_scapro"
    Me.ap_scapro.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ap_scapro.Name = "ap_scapro"
    Me.ap_scapro.NTSRepositoryComboBox = Nothing
    Me.ap_scapro.NTSRepositoryItemCheck = Nothing
    Me.ap_scapro.NTSRepositoryItemMemo = Nothing
    Me.ap_scapro.NTSRepositoryItemText = Nothing
    Me.ap_scapro.OptionsColumn.AllowEdit = False
    Me.ap_scapro.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ap_scapro.OptionsColumn.ReadOnly = True
    Me.ap_scapro.OptionsFilter.AllowFilter = False
    Me.ap_scapro.Visible = True
    Me.ap_scapro.VisibleIndex = 15
    '
    'ap_scavar
    '
    Me.ap_scavar.AppearanceCell.Options.UseBackColor = True
    Me.ap_scavar.AppearanceCell.Options.UseTextOptions = True
    Me.ap_scavar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ap_scavar.Caption = "Scarichi vari"
    Me.ap_scavar.Enabled = False
    Me.ap_scavar.FieldName = "ap_scavar"
    Me.ap_scavar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ap_scavar.Name = "ap_scavar"
    Me.ap_scavar.NTSRepositoryComboBox = Nothing
    Me.ap_scavar.NTSRepositoryItemCheck = Nothing
    Me.ap_scavar.NTSRepositoryItemMemo = Nothing
    Me.ap_scavar.NTSRepositoryItemText = Nothing
    Me.ap_scavar.OptionsColumn.AllowEdit = False
    Me.ap_scavar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ap_scavar.OptionsColumn.ReadOnly = True
    Me.ap_scavar.OptionsFilter.AllowFilter = False
    Me.ap_scavar.Visible = True
    Me.ap_scavar.VisibleIndex = 16
    '
    'ap_resfor
    '
    Me.ap_resfor.AppearanceCell.Options.UseBackColor = True
    Me.ap_resfor.AppearanceCell.Options.UseTextOptions = True
    Me.ap_resfor.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ap_resfor.Caption = "Resi a fornitori"
    Me.ap_resfor.Enabled = False
    Me.ap_resfor.FieldName = "ap_resfor"
    Me.ap_resfor.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ap_resfor.Name = "ap_resfor"
    Me.ap_resfor.NTSRepositoryComboBox = Nothing
    Me.ap_resfor.NTSRepositoryItemCheck = Nothing
    Me.ap_resfor.NTSRepositoryItemMemo = Nothing
    Me.ap_resfor.NTSRepositoryItemText = Nothing
    Me.ap_resfor.OptionsColumn.AllowEdit = False
    Me.ap_resfor.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ap_resfor.OptionsColumn.ReadOnly = True
    Me.ap_resfor.OptionsFilter.AllowFilter = False
    Me.ap_resfor.Visible = True
    Me.ap_resfor.VisibleIndex = 17
    '
    'xx_totscarichi
    '
    Me.xx_totscarichi.AppearanceCell.Options.UseBackColor = True
    Me.xx_totscarichi.AppearanceCell.Options.UseTextOptions = True
    Me.xx_totscarichi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_totscarichi.Caption = "Totale scarichi"
    Me.xx_totscarichi.Enabled = False
    Me.xx_totscarichi.FieldName = "xx_totscarichi"
    Me.xx_totscarichi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_totscarichi.Name = "xx_totscarichi"
    Me.xx_totscarichi.NTSRepositoryComboBox = Nothing
    Me.xx_totscarichi.NTSRepositoryItemCheck = Nothing
    Me.xx_totscarichi.NTSRepositoryItemMemo = Nothing
    Me.xx_totscarichi.NTSRepositoryItemText = Nothing
    Me.xx_totscarichi.OptionsColumn.AllowEdit = False
    Me.xx_totscarichi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_totscarichi.OptionsColumn.ReadOnly = True
    Me.xx_totscarichi.OptionsFilter.AllowFilter = False
    Me.xx_totscarichi.Visible = True
    Me.xx_totscarichi.VisibleIndex = 18
    '
    'xx_ordes
    '
    Me.xx_ordes.AppearanceCell.Options.UseBackColor = True
    Me.xx_ordes.AppearanceCell.Options.UseTextOptions = True
    Me.xx_ordes.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_ordes.Caption = "Ordinato esecutivo"
    Me.xx_ordes.Enabled = False
    Me.xx_ordes.FieldName = "xx_ordes"
    Me.xx_ordes.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_ordes.Name = "xx_ordes"
    Me.xx_ordes.NTSRepositoryComboBox = Nothing
    Me.xx_ordes.NTSRepositoryItemCheck = Nothing
    Me.xx_ordes.NTSRepositoryItemMemo = Nothing
    Me.xx_ordes.NTSRepositoryItemText = Nothing
    Me.xx_ordes.OptionsColumn.AllowEdit = False
    Me.xx_ordes.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_ordes.OptionsColumn.ReadOnly = True
    Me.xx_ordes.OptionsFilter.AllowFilter = False
    '
    'xx_ordap
    '
    Me.xx_ordap.AppearanceCell.Options.UseBackColor = True
    Me.xx_ordap.AppearanceCell.Options.UseTextOptions = True
    Me.xx_ordap.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_ordap.Caption = "Ordinato aperto"
    Me.xx_ordap.Enabled = False
    Me.xx_ordap.FieldName = "xx_ordap"
    Me.xx_ordap.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_ordap.Name = "xx_ordap"
    Me.xx_ordap.NTSRepositoryComboBox = Nothing
    Me.xx_ordap.NTSRepositoryItemCheck = Nothing
    Me.xx_ordap.NTSRepositoryItemMemo = Nothing
    Me.xx_ordap.NTSRepositoryItemText = Nothing
    Me.xx_ordap.OptionsColumn.AllowEdit = False
    Me.xx_ordap.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_ordap.OptionsColumn.ReadOnly = True
    Me.xx_ordap.OptionsFilter.AllowFilter = False
    '
    'xx_impeges
    '
    Me.xx_impeges.AppearanceCell.Options.UseBackColor = True
    Me.xx_impeges.AppearanceCell.Options.UseTextOptions = True
    Me.xx_impeges.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_impeges.Caption = "Impegnato esecutivo"
    Me.xx_impeges.Enabled = False
    Me.xx_impeges.FieldName = "xx_impeges"
    Me.xx_impeges.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_impeges.Name = "xx_impeges"
    Me.xx_impeges.NTSRepositoryComboBox = Nothing
    Me.xx_impeges.NTSRepositoryItemCheck = Nothing
    Me.xx_impeges.NTSRepositoryItemMemo = Nothing
    Me.xx_impeges.NTSRepositoryItemText = Nothing
    Me.xx_impeges.OptionsColumn.AllowEdit = False
    Me.xx_impeges.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_impeges.OptionsColumn.ReadOnly = True
    Me.xx_impeges.OptionsFilter.AllowFilter = False
    '
    'xx_impegap
    '
    Me.xx_impegap.AppearanceCell.Options.UseBackColor = True
    Me.xx_impegap.AppearanceCell.Options.UseTextOptions = True
    Me.xx_impegap.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_impegap.Caption = "Impegnato aperto"
    Me.xx_impegap.Enabled = False
    Me.xx_impegap.FieldName = "xx_impegap"
    Me.xx_impegap.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_impegap.Name = "xx_impegap"
    Me.xx_impegap.NTSRepositoryComboBox = Nothing
    Me.xx_impegap.NTSRepositoryItemCheck = Nothing
    Me.xx_impegap.NTSRepositoryItemMemo = Nothing
    Me.xx_impegap.NTSRepositoryItemText = Nothing
    Me.xx_impegap.OptionsColumn.AllowEdit = False
    Me.xx_impegap.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_impegap.OptionsColumn.ReadOnly = True
    Me.xx_impegap.OptionsFilter.AllowFilter = False
    '
    'xx_dispones
    '
    Me.xx_dispones.AppearanceCell.Options.UseBackColor = True
    Me.xx_dispones.AppearanceCell.Options.UseTextOptions = True
    Me.xx_dispones.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_dispones.Caption = "Disponibilit‡ esecutiva"
    Me.xx_dispones.Enabled = False
    Me.xx_dispones.FieldName = "xx_dispones"
    Me.xx_dispones.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_dispones.Name = "xx_dispones"
    Me.xx_dispones.NTSRepositoryComboBox = Nothing
    Me.xx_dispones.NTSRepositoryItemCheck = Nothing
    Me.xx_dispones.NTSRepositoryItemMemo = Nothing
    Me.xx_dispones.NTSRepositoryItemText = Nothing
    Me.xx_dispones.OptionsColumn.AllowEdit = False
    Me.xx_dispones.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_dispones.OptionsColumn.ReadOnly = True
    Me.xx_dispones.OptionsFilter.AllowFilter = False
    '
    'NtsBarManager1
    '
    Me.NtsBarManager1.AllowCustomization = False
    Me.NtsBarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.tlbMain})
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlTop)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlBottom)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlLeft)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlRight)
    Me.NtsBarManager1.Form = Me
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbGuida, Me.tlbEsci, Me.tlbImpostaStampante, Me.tlbMovimenti, Me.tlbOrdini, Me.tlbTaglie, Me.tlbNavigazione, Me.tlbSaldoMatricoleAperte, Me.tlbDettaglioProgressivi})
    Me.NtsBarManager1.MaxItemId = 22
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbMovimenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSaldoMatricoleAperte), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbOrdini), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDettaglioProgressivi), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbTaglie, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNavigazione, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbMovimenti
    '
    Me.tlbMovimenti.Caption = "Movimenti"
    Me.tlbMovimenti.Glyph = CType(resources.GetObject("tlbMovimenti.Glyph"), System.Drawing.Image)
    Me.tlbMovimenti.GlyphPath = ""
    Me.tlbMovimenti.Id = 16
    Me.tlbMovimenti.Name = "tlbMovimenti"
    Me.tlbMovimenti.Visible = True
    '
    'tlbSaldoMatricoleAperte
    '
    Me.tlbSaldoMatricoleAperte.Caption = "Saldo Matricole Aperte"
    Me.tlbSaldoMatricoleAperte.Glyph = CType(resources.GetObject("tlbSaldoMatricoleAperte.Glyph"), System.Drawing.Image)
    Me.tlbSaldoMatricoleAperte.GlyphPath = ""
    Me.tlbSaldoMatricoleAperte.Id = 20
    Me.tlbSaldoMatricoleAperte.Name = "tlbSaldoMatricoleAperte"
    Me.tlbSaldoMatricoleAperte.Visible = True
    '
    'tlbOrdini
    '
    Me.tlbOrdini.Caption = "Ordini"
    Me.tlbOrdini.Glyph = CType(resources.GetObject("tlbOrdini.Glyph"), System.Drawing.Image)
    Me.tlbOrdini.GlyphPath = ""
    Me.tlbOrdini.Id = 17
    Me.tlbOrdini.Name = "tlbOrdini"
    Me.tlbOrdini.Visible = True
    '
    'tlbTaglie
    '
    Me.tlbTaglie.Caption = "Taglie"
    Me.tlbTaglie.Glyph = CType(resources.GetObject("tlbTaglie.Glyph"), System.Drawing.Image)
    Me.tlbTaglie.GlyphPath = ""
    Me.tlbTaglie.Id = 18
    Me.tlbTaglie.Name = "tlbTaglie"
    Me.tlbTaglie.Visible = True
    '
    'tlbNavigazione
    '
    Me.tlbNavigazione.Caption = "Navigazione"
    Me.tlbNavigazione.Glyph = CType(resources.GetObject("tlbNavigazione.Glyph"), System.Drawing.Image)
    Me.tlbNavigazione.GlyphPath = ""
    Me.tlbNavigazione.Id = 19
    Me.tlbNavigazione.Name = "tlbNavigazione"
    Me.tlbNavigazione.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 9
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 10
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta stampante"
    Me.tlbImpostaStampante.GlyphPath = ""
    Me.tlbImpostaStampante.Id = 15
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbDettaglioProgressivi
    '
    Me.tlbDettaglioProgressivi.Caption = "Dettaglio Progressivi"
    Me.tlbDettaglioProgressivi.Glyph = CType(resources.GetObject("tlbDettaglioProgressivi.Glyph"), System.Drawing.Image)
    Me.tlbDettaglioProgressivi.GlyphPath = ""
    Me.tlbDettaglioProgressivi.Id = 21
    Me.tlbDettaglioProgressivi.Name = "tlbDettaglioProgressivi"
    Me.tlbDettaglioProgressivi.Visible = True
    '
    'FRMMGHLAP
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(667, 366)
    Me.Controls.Add(Me.grHlap)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.HelpContext = ""
    Me.Name = "FRMMGHLAP"
    Me.NTSLastControlFocussed = Me.grHlap
    Me.Text = "PROGRESSIVI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grHlap, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvHlap, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbMovimenti.GlyphPath = (oApp.ChildImageDir & "\doc.gif")
        tlbSaldoMatricoleAperte.GlyphPath = (oApp.ChildImageDir & "\doc_1.gif")
        tlbOrdini.GlyphPath = (oApp.ChildImageDir & "\ordini.gif")
        tlbDettaglioProgressivi.GlyphPath = (oApp.ChildImageDir & "\visualizza.gif")
        tlbTaglie.GlyphPath = (oApp.ChildImageDir & "\tc.gif")
        tlbNavigazione.GlyphPath = (oApp.ChildImageDir & "\movmrp.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        '  'non gestisco l'errore: se non c'Ë una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvHlap.NTSSetParam(oMenu, oApp.Tr(Me, 128828971526205441, "Progressivi"))
      codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828971576205761, "Cod. ditta"), 12, False)
      ap_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828971595580885, "Codice articolo"), CLN__STD.CodartMaxLen, False)
      ap_magaz.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828971611674738, "Codice"), "0", 4, 0, 9999)
      xx_magaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828971624799822, "Magazzino"), 0, True)
      ap_esist.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828971638706161, "Esistenza"), "#,##0.000", 15)
      ap_ordin.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828971656050022, "Ordinato"), "#,##0.000", 15)
      ap_impeg.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828971669643859, "Impegnato"), "#,##0.000", 15)
      ap_prenot.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828971686206465, "Prenotato"), "#,##0.000", 15)
      xx_disp.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828971700894059, "Disponibilit‡"), "#,##0.000", 15)
      xx_dispnet.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828971715581653, "Disponibilit‡ netta"), "#,##0.000", 15)
      ap_giaini.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828971730738000, "Giacenza iniziale"), "#,##0.000", 15)
      ap_carfor.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828971746519351, "Carichi fornitore"), "#,##0.000", 15)
      ap_carpro.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828971763863212, "Carichi produzione"), "#,##0.000", 15)
      ap_carvar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828971779332061, "Carichi vari"), "#,##0.000", 15)
      ap_rescli.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828971795582165, "Resi da clienti"), "#,##0.000", 15)
      xx_totcarichi.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828971808238496, "Totale carichi"), "#,##0.000", 15)
      ap_scacli.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828971825894859, "Scarichi a clienti"), "#,##0.000", 15)
      ap_scapro.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828971842144963, "Scarichi produzione"), "#,##0.000", 15)
      ap_scavar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828971869645139, "Scarichi vari"), "#,##0.000", 15)
      ap_resfor.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828971911520407, "Resi a fornitori"), "#,##0.000", 15)
      xx_totscarichi.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828971927926762, "Totale scarichi"), "#,##0.000", 15)
      xx_ordes.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828971946051878, "Ordinato esecutivo"), "#,##0.000", 15)
      xx_ordap.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828971961520727, "Ordinato aperto"), "#,##0.000", 15)
      xx_impeges.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828971976677074, "Impegnato esecutivo"), "#,##0.000", 15)
      xx_impegap.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828971993083429, "Impegnato aperto"), "#,##0.000", 15)
      xx_dispones.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828972008083525, "Disponibilit‡ esecutiva"), "#,##0.000", 15)


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

#Region "EventiForm"
  Public Overridable Sub FRMMGHLAP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim strTmp As String = oCleArmd.strPrgParent.ToUpper
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      If oMenu.ValCodiceDb(oCleArmd.strHlapCodart, DittaCorrente, "ARTICO", "S", Nothing, dttTmp) = True Then
        If dttTmp.Rows(0)!ar_codart.Equals(DBNull.Value) = False Then
          With dttTmp.Rows(0)
            If (NTSCStr(!ar_gescomm) = "N") And _
               (NTSCStr(!ar_geslotti) = "N") And _
               (NTSCStr(!ar_gesubic) = "N") Then tlbDettaglioProgressivi.Enabled = False
          End With
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Preleva l'eventuale elenco di magazzini da visualizzare in griglia indicato
      '--- nel registro per l'utente corrente
      '--------------------------------------------------------------------------------------------------------------
      If strTmp.Trim <> "" Then
        If Microsoft.VisualBasic.Left(strTmp, 2) = "BN" Then strTmp = "BS" & Microsoft.VisualBasic.Mid(strTmp, 3)
      End If
      oCleArmd.strElencoCodici = Trim(oMenu.GetSettingBus(strTmp, "OPZIONIUT", ".", "VisProgrMagazzini", "", " ", ""))
      If oCleArmd.strElencoCodici <> "" Then
        '--- Sostituisce i punto e virgola con virgole
        oCleArmd.strElencoCodici = Trim(Replace(oCleArmd.strElencoCodici, ";", ","))
        '--- Toglie l'eventuale virgola finale
        If Microsoft.VisualBasic.Right(oCleArmd.strElencoCodici, 1) = "," Then
          oCleArmd.strElencoCodici = Trim(Mid(oCleArmd.strElencoCodici, 1, (Len(oCleArmd.strElencoCodici) - 1)))
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleArmd.HlapApri(DittaCorrente, oCleArmd.strHlapCodart, dsHlap) Then Me.Close()
      '--------------------------------------------------------------------------------------------------------------
      dcHlap.DataSource = dsHlap.Tables("ARTPRO")
      grHlap.DataSource = dcHlap
      '--------------------------------------------------------------------------------------------------------------
      grvHlap.NTSAllowDelete = False
      grvHlap.NTSAllowInsert = False
      grvHlap.NTSAllowUpdate = False
      '--------------------------------------------------------------------------------------------------------------
      If oCleArmd.bHlapTCO = False Then tlbTaglie.Enabled = False
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.Default
      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione Friendly nasconde, sempre, alcuni controlli
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = True Then
        tlbTaglie.Visible = False
        tlbNavigazione.Visible = False
        ap_prenot.Visible = False
        xx_dispnet.Visible = False
        ap_carpro.Visible = False
        ap_scapro.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCleArmd.ArticoloGestitoAMatricole = False Then tlbSaldoMatricoleAperte.Enabled = False
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.Default
      '--------------------------------------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------------------------------------------------------------------------
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Sub
  Public Overridable Sub FRMMGHLAP_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcHlap.Dispose()
      dsHlap.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbMovimenti_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbMovimenti.ItemClick
    Dim strParam As String = ""
    Dim nAp_magaz As Integer
    Try
      If dsHlap.Tables("ARTPRO").Rows.Count > 0 Then
        nAp_magaz = NTSCInt(dsHlap.Tables("ARTPRO").Rows(dcHlap.Position)!ap_magaz)
      Else
        nAp_magaz = 0
      End If

      strParam = "APRI:" & NTSCStr(oCleArmd.strHlapCodart).ToString.PadRight(CLN__STD.CodartMaxLen, CChar(" ")) & ";" & _
        nAp_magaz.ToString.PadLeft(4, CChar("0")) & ";000000000;A;" & _
        NTSCInt(IIf(oCleArmd.dHlapFase >= 0, oCleArmd.dHlapFase, 0)).ToString.PadLeft(4, CChar("0")) & ";"

      oMenu.RunChild("BSMGSCHE", "CLSMGSCHE", " ", DittaCorrente, "", "", Nothing, strParam, True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbSaldoMatricoleAperte_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSaldoMatricoleAperte.ItemClick
    Dim nAp_magaz As Integer = 0
    Dim strParam As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If dsHlap.Tables("ARTPRO").Rows.Count > 0 Then
        nAp_magaz = NTSCInt(dsHlap.Tables("ARTPRO").Rows(dcHlap.Position)!ap_magaz)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strParam = "APRI:" & _
        NTSCStr(oCleArmd.strHlapCodart).ToString.PadRight(CLN__STD.CodartMaxLen, CChar(" ")) & ";" & _
        nAp_magaz.ToString.PadLeft(4, CChar("0")) & ";" & _
        "".PadLeft(9, "0"c) & ";" & _
        "K;" & _
        NTSCInt(IIf(oCleArmd.dHlapFase >= 0, oCleArmd.dHlapFase, 0)).ToString.PadLeft(4, CChar("0")) & ";" & _
        "-1"
      '--------------------------------------------------------------------------------------------------------------
      oMenu.RunChild("BSMGSCHE", "CLSMGSCHE", " ", DittaCorrente, "", "", Nothing, strParam, True, True)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub tlbOrdini_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbOrdini.ItemClick
    Dim strWhereHlmo As String
    Dim oPar As New CLE__PATB
    Dim nAp_magaz As Integer
    Try
      If dsHlap.Tables("ARTPRO").Rows.Count > 0 Then
        nAp_magaz = NTSCInt(dsHlap.Tables("ARTPRO").Rows(dcHlap.Position)!ap_magaz)
      Else
        nAp_magaz = 0
      End If

      strWhereHlmo = " (movord.mo_magaz = " & nAp_magaz & _
        " OR movord.mo_magaz2 = " & nAp_magaz & ")" & _
        " AND movord.mo_flevas = 'C'" & _
        " AND movord.mo_tipork <> 'Q'" & _
        " AND movord.codditt = " & CStrSQL(DittaCorrente)
      If NTSCInt(oCleArmd.dHlapFase) >= 0 Then strWhereHlmo += " AND mo_fase = " & NTSCInt(oCleArmd.dHlapFase)
      '------------------------
      'Chiama lo zoom ordini aperti
      oPar.strDescr = strWhereHlmo
      oPar.strTipo = " "
      oPar.lContoCF = 0
      oPar.strCodart = oCleArmd.strHlapCodart
      oPar.nFase = NTSCInt(oCleArmd.dHlapFase)
      oPar.dImporto = 0
      oPar.nTipologia = 0                     '0 solo visualizzaz, 2 = possibilit‡ di selez le righe
      oPar.oParam = Nothing                   'se chiamato da veboll qui occorrer‡ passare il datatable del corpo (oPar.oParam = oCleVeboll.dttEC)
      oPar.nMastro = NTSCInt(IIf(oCleArmd.strHlapCodart <> "", 10, 1))   'colonne di bsorhlmo da visualizzare (in vb6 lShowColumn)
      NTSZOOM.ZoomStrIn("ZOOMMOVORD", DittaCorrente, oPar)        'in vb6 la dohlmo
      If oPar.oParam Is Nothing Then Return
      'se non premuto 'annulla' in oPar.oParam viene restituito l'elenco delle righe della griglia da trattare!!!
      If CType(oPar.oParam, DataTable).Rows.Count = 0 Then Return

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbDettaglioProgressivi_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDettaglioProgressivi.ItemClick
    Dim oParam As New CLE__CLDP
    Dim frmDepr As FRMMGDEPR = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      If grvHlap.NTSGetCurrentDataRow() Is Nothing Then Return
      If NTSCStr(grvHlap.NTSGetCurrentDataRow!ap_codart).Trim = "" Then Return
      oCleArmd.strDeprCodart = NTSCStr(grvHlap.NTSGetCurrentDataRow!ap_codart)
      oCleArmd.dDeprMagaz = NTSCInt(grvHlap.NTSGetCurrentDataRow!ap_magaz)
      oCleArmd.strDeprDesMagaz = NTSCStr(grvHlap.NTSGetCurrentDataRow!xx_magaz)
      oCleArmd.dDeprFase = NTSCInt(grvHlap.NTSGetCurrentDataRow!ap_fase)
      '--------------------------------------------------------------------------------------------------------------
      frmDepr = CType(NTSNewFormModal("FRMMGDEPR"), FRMMGDEPR)
      frmDepr.Init(oMenu, oParam, DittaCorrente)
      frmDepr.InitEntity(oCleArmd)
      frmDepr.ShowDialog()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      If Not frmDepr Is Nothing Then
        frmDepr.Dispose()
        frmDepr = Nothing
      End If
    End Try
  End Sub
  Public Overridable Sub tlbTaglie_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbTaglie.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      If dsHlap.Tables("ARTPRO").Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128566340162716163, "Non esistono dati da visualizzare nel dettaglio delle quantit‡ per taglie."))
        Exit Sub
      End If

      If grvHlap.NTSGetCurrentDataRow() Is Nothing Then Exit Sub

      '------------------------------
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BS--HLAP"
      oPar.strParam = "".PadLeft(12) & "|" & _
               "".PadLeft(12, "z"c) & "|" & _
               NTSCStr(grvHlap.NTSGetCurrentDataRow!ap_codart) & "|" & _
               NTSCStr(grvHlap.NTSGetCurrentDataRow!ap_codart) & "|" & _
               NTSCInt(grvHlap.NTSGetCurrentDataRow!ap_magaz).ToString & "|" & _
               NTSCInt(grvHlap.NTSGetCurrentDataRow!ap_magaz).ToString & "|" & _
               "".PadLeft(18) & "|" & _
               "".PadLeft(18, "z"c) & "|" & _
               "0" & "|" & _
               "999999999" & "|" & _
               "0" & "|" & _
               "999999999" & "|" & _
               NTSCStr(IIf(oCleArmd.dHlapFase >= 0, oCleArmd.dHlapFase, "0")) & "|" & _
               NTSCStr(IIf(oCleArmd.dHlapFase >= 0, oCleArmd.dHlapFase, "0")) & "|" & _
               "ARTPROTC" & "|" & _
               "".PadLeft(6, "S"c) & "".PadLeft(14, "N"c) & "|" & _
               "0" & "|" & _
               "0"
      oMenu.RunChild("NTSInformatica", "FRMTCDIPT", "", DittaCorrente, "", "BNTCDIPT", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbNavigazione_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNavigazione.ItemClick
    Dim strParam As String = ""
    Dim nAp_magaz As Integer
    Try
      If dsHlap.Tables("ARTPRO").Rows.Count > 0 Then
        nAp_magaz = NTSCInt(dsHlap.Tables("ARTPRO").Rows(dcHlap.Position)!ap_magaz)
      Else
        nAp_magaz = 0
      End If

      strParam = "A;" & NTSCStr(oCleArmd.strHlapCodart).ToString.PadLeft(CLN__STD.CodartMaxLen, CChar(" ")) & _
        ";" & NTSCInt(IIf(oCleArmd.dHlapFase >= 0, oCleArmd.dHlapFase, 0)).ToString.PadLeft(4, CChar("0")) & _
        ";" & nAp_magaz.ToString.PadLeft(4, CChar("0"))

      oMenu.RunChild("BSDBNMRP", "CLSDBNMRP", " ", DittaCorrente, "", "", Nothing, strParam, True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub
  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub
#End Region

End Class