Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGFASI
#Region "Variabili"
  Public oCleArmd As CLEMGARMD
  Public oCallParams As CLE__CLDP
  Public dsFasi As New DataSet
  Public dcFasi As New BindingSource()
  Public dttFasi As DataTable = Nothing
  Public dtrFasi() As DataRow = Nothing
  Private components As System.ComponentModel.IContainer

  Public bServer As Boolean

  Public WithEvents grFasi As NTSInformatica.NTSGrid
  Public WithEvents grvFasi As NTSInformatica.NTSGridView
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem

  Public WithEvents codditt As NTSInformatica.NTSGridColumn
  Public WithEvents af_codart As NTSInformatica.NTSGridColumn
  Public WithEvents af_fase As NTSInformatica.NTSGridColumn
  Public WithEvents af_codlavo As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codlavo As NTSInformatica.NTSGridColumn
  Public WithEvents af_ultagg As NTSInformatica.NTSGridColumn
  Public WithEvents af_opnome As NTSInformatica.NTSGridColumn
  Public WithEvents af_descr As NTSInformatica.NTSGridColumn
  Public WithEvents af_livmindb As NTSInformatica.NTSGridColumn
  Public WithEvents af_rrfence As NTSInformatica.NTSGridColumn
  Public WithEvents af_fcorrlt As NTSInformatica.NTSGridColumn
  Public WithEvents af_makebuy As NTSInformatica.NTSGridColumn
  Public WithEvents tlbProgressiviTotali As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbQualità As NTSInformatica.NTSBarButtonItem
  Public WithEvents af_codtcdc As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codtcdc As NTSInformatica.NTSGridColumn
  Public WithEvents af_coddica As NTSInformatica.NTSGridColumn
  Public WithEvents xx_coddica As NTSInformatica.NTSGridColumn
  Public WithEvents af_coddicv As NTSInformatica.NTSGridColumn
  Public WithEvents xx_coddicv As NTSInformatica.NTSGridColumn
  Public WithEvents tlbProgressivi As NTSInformatica.NTSBarButtonItem
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
    'creo e attivo l'entity e inizializzo la funzione che dovrÃ  rilevare gli eventi dall'ENTITY
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
    oCleArmd.dFasiPerqta = oCallParams.dPar1
    oCleArmd.strFasiCodart = oCallParams.strPar2
    oCleArmd.strFasiColore = oCallParams.strPar3

    Return True
  End Function
  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGFASI))
    Me.grFasi = New NTSInformatica.NTSGrid
    Me.grvFasi = New NTSInformatica.NTSGridView
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.af_codart = New NTSInformatica.NTSGridColumn
    Me.af_fase = New NTSInformatica.NTSGridColumn
    Me.af_descr = New NTSInformatica.NTSGridColumn
    Me.af_codlavo = New NTSInformatica.NTSGridColumn
    Me.xx_codlavo = New NTSInformatica.NTSGridColumn
    Me.af_ultagg = New NTSInformatica.NTSGridColumn
    Me.af_opnome = New NTSInformatica.NTSGridColumn
    Me.af_livmindb = New NTSInformatica.NTSGridColumn
    Me.af_rrfence = New NTSInformatica.NTSGridColumn
    Me.af_fcorrlt = New NTSInformatica.NTSGridColumn
    Me.af_makebuy = New NTSInformatica.NTSGridColumn
    Me.af_codtcdc = New NTSInformatica.NTSGridColumn
    Me.xx_codtcdc = New NTSInformatica.NTSGridColumn
    Me.af_coddica = New NTSInformatica.NTSGridColumn
    Me.xx_coddica = New NTSInformatica.NTSGridColumn
    Me.af_coddicv = New NTSInformatica.NTSGridColumn
    Me.xx_coddicv = New NTSInformatica.NTSGridColumn
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbQualità = New NTSInformatica.NTSBarButtonItem
    Me.tlbProgressivi = New NTSInformatica.NTSBarButtonItem
    Me.tlbProgressiviTotali = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    CType(Me.grFasi, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvFasi, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'grFasi
    '
    Me.grFasi.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grFasi.EmbeddedNavigator.Name = ""
    Me.grFasi.Location = New System.Drawing.Point(0, 30)
    Me.grFasi.MainView = Me.grvFasi
    Me.grFasi.Name = "grFasi"
    Me.grFasi.Size = New System.Drawing.Size(667, 336)
    Me.grFasi.TabIndex = 0
    Me.grFasi.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvFasi})
    '
    'grvFasi
    '
    Me.grvFasi.ActiveFilterEnabled = False
    Me.grvFasi.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.codditt, Me.af_codart, Me.af_fase, Me.af_descr, Me.af_codlavo, Me.xx_codlavo, Me.af_ultagg, Me.af_opnome, Me.af_livmindb, Me.af_rrfence, Me.af_fcorrlt, Me.af_makebuy, Me.af_codtcdc, Me.xx_codtcdc, Me.af_coddica, Me.xx_coddica, Me.af_coddicv, Me.xx_coddicv})
    Me.grvFasi.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvFasi.Enabled = True
    Me.grvFasi.GridControl = Me.grFasi
    Me.grvFasi.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvFasi.Name = "grvFasi"
    Me.grvFasi.NTSAllowDelete = True
    Me.grvFasi.NTSAllowInsert = True
    Me.grvFasi.NTSAllowUpdate = True
    Me.grvFasi.NTSMenuContext = Nothing
    Me.grvFasi.OptionsCustomization.AllowRowSizing = True
    Me.grvFasi.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvFasi.OptionsNavigation.UseTabKey = False
    Me.grvFasi.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvFasi.OptionsView.ColumnAutoWidth = False
    Me.grvFasi.OptionsView.EnableAppearanceEvenRow = True
    Me.grvFasi.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvFasi.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvFasi.OptionsView.ShowGroupPanel = False
    Me.grvFasi.RowHeight = 16
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
    'af_codart
    '
    Me.af_codart.AppearanceCell.Options.UseBackColor = True
    Me.af_codart.AppearanceCell.Options.UseTextOptions = True
    Me.af_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.af_codart.Caption = "Codice articolo"
    Me.af_codart.Enabled = False
    Me.af_codart.FieldName = "af_codart"
    Me.af_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.af_codart.Name = "af_codart"
    Me.af_codart.NTSRepositoryComboBox = Nothing
    Me.af_codart.NTSRepositoryItemCheck = Nothing
    Me.af_codart.NTSRepositoryItemMemo = Nothing
    Me.af_codart.NTSRepositoryItemText = Nothing
    Me.af_codart.OptionsColumn.AllowEdit = False
    Me.af_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.af_codart.OptionsColumn.ReadOnly = True
    Me.af_codart.OptionsFilter.AllowFilter = False
    '
    'af_fase
    '
    Me.af_fase.AppearanceCell.Options.UseBackColor = True
    Me.af_fase.AppearanceCell.Options.UseTextOptions = True
    Me.af_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.af_fase.Caption = "Fase"
    Me.af_fase.Enabled = True
    Me.af_fase.FieldName = "af_fase"
    Me.af_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.af_fase.Name = "af_fase"
    Me.af_fase.NTSRepositoryComboBox = Nothing
    Me.af_fase.NTSRepositoryItemCheck = Nothing
    Me.af_fase.NTSRepositoryItemMemo = Nothing
    Me.af_fase.NTSRepositoryItemText = Nothing
    Me.af_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.af_fase.OptionsFilter.AllowFilter = False
    Me.af_fase.Visible = True
    Me.af_fase.VisibleIndex = 0
    '
    'af_descr
    '
    Me.af_descr.AppearanceCell.Options.UseBackColor = True
    Me.af_descr.AppearanceCell.Options.UseTextOptions = True
    Me.af_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.af_descr.Caption = "Descrizione Fase"
    Me.af_descr.Enabled = True
    Me.af_descr.FieldName = "af_descr"
    Me.af_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.af_descr.Name = "af_descr"
    Me.af_descr.NTSRepositoryComboBox = Nothing
    Me.af_descr.NTSRepositoryItemCheck = Nothing
    Me.af_descr.NTSRepositoryItemMemo = Nothing
    Me.af_descr.NTSRepositoryItemText = Nothing
    Me.af_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.af_descr.OptionsFilter.AllowFilter = False
    Me.af_descr.Visible = True
    Me.af_descr.VisibleIndex = 1
    '
    'af_codlavo
    '
    Me.af_codlavo.AppearanceCell.Options.UseBackColor = True
    Me.af_codlavo.AppearanceCell.Options.UseTextOptions = True
    Me.af_codlavo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.af_codlavo.Caption = "Cod. lavorazione"
    Me.af_codlavo.Enabled = True
    Me.af_codlavo.FieldName = "af_codlavo"
    Me.af_codlavo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.af_codlavo.Name = "af_codlavo"
    Me.af_codlavo.NTSRepositoryComboBox = Nothing
    Me.af_codlavo.NTSRepositoryItemCheck = Nothing
    Me.af_codlavo.NTSRepositoryItemMemo = Nothing
    Me.af_codlavo.NTSRepositoryItemText = Nothing
    Me.af_codlavo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.af_codlavo.OptionsFilter.AllowFilter = False
    Me.af_codlavo.Visible = True
    Me.af_codlavo.VisibleIndex = 2
    '
    'xx_codlavo
    '
    Me.xx_codlavo.AppearanceCell.Options.UseBackColor = True
    Me.xx_codlavo.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codlavo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codlavo.Caption = "Descr. lavorazione"
    Me.xx_codlavo.Enabled = False
    Me.xx_codlavo.FieldName = "xx_codlavo"
    Me.xx_codlavo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codlavo.Name = "xx_codlavo"
    Me.xx_codlavo.NTSRepositoryComboBox = Nothing
    Me.xx_codlavo.NTSRepositoryItemCheck = Nothing
    Me.xx_codlavo.NTSRepositoryItemMemo = Nothing
    Me.xx_codlavo.NTSRepositoryItemText = Nothing
    Me.xx_codlavo.OptionsColumn.AllowEdit = False
    Me.xx_codlavo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codlavo.OptionsColumn.ReadOnly = True
    Me.xx_codlavo.OptionsFilter.AllowFilter = False
    Me.xx_codlavo.Visible = True
    Me.xx_codlavo.VisibleIndex = 3
    '
    'af_ultagg
    '
    Me.af_ultagg.AppearanceCell.Options.UseBackColor = True
    Me.af_ultagg.AppearanceCell.Options.UseTextOptions = True
    Me.af_ultagg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.af_ultagg.Caption = "Ultimo agg."
    Me.af_ultagg.Enabled = False
    Me.af_ultagg.FieldName = "af_ultagg"
    Me.af_ultagg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.af_ultagg.Name = "af_ultagg"
    Me.af_ultagg.NTSRepositoryComboBox = Nothing
    Me.af_ultagg.NTSRepositoryItemCheck = Nothing
    Me.af_ultagg.NTSRepositoryItemMemo = Nothing
    Me.af_ultagg.NTSRepositoryItemText = Nothing
    Me.af_ultagg.OptionsColumn.AllowEdit = False
    Me.af_ultagg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.af_ultagg.OptionsColumn.ReadOnly = True
    Me.af_ultagg.OptionsFilter.AllowFilter = False
    '
    'af_opnome
    '
    Me.af_opnome.AppearanceCell.Options.UseBackColor = True
    Me.af_opnome.AppearanceCell.Options.UseTextOptions = True
    Me.af_opnome.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.af_opnome.Caption = "Nome operatore"
    Me.af_opnome.Enabled = False
    Me.af_opnome.FieldName = "af_opnome"
    Me.af_opnome.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.af_opnome.Name = "af_opnome"
    Me.af_opnome.NTSRepositoryComboBox = Nothing
    Me.af_opnome.NTSRepositoryItemCheck = Nothing
    Me.af_opnome.NTSRepositoryItemMemo = Nothing
    Me.af_opnome.NTSRepositoryItemText = Nothing
    Me.af_opnome.OptionsColumn.AllowEdit = False
    Me.af_opnome.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.af_opnome.OptionsColumn.ReadOnly = True
    Me.af_opnome.OptionsFilter.AllowFilter = False
    '
    'af_livmindb
    '
    Me.af_livmindb.AppearanceCell.Options.UseBackColor = True
    Me.af_livmindb.AppearanceCell.Options.UseTextOptions = True
    Me.af_livmindb.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.af_livmindb.Caption = "Liv.Min.DB"
    Me.af_livmindb.Enabled = True
    Me.af_livmindb.FieldName = "af_livmindb"
    Me.af_livmindb.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.af_livmindb.Name = "af_livmindb"
    Me.af_livmindb.NTSRepositoryComboBox = Nothing
    Me.af_livmindb.NTSRepositoryItemCheck = Nothing
    Me.af_livmindb.NTSRepositoryItemMemo = Nothing
    Me.af_livmindb.NTSRepositoryItemText = Nothing
    Me.af_livmindb.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.af_livmindb.OptionsFilter.AllowFilter = False
    Me.af_livmindb.Visible = True
    Me.af_livmindb.VisibleIndex = 4
    '
    'af_rrfence
    '
    Me.af_rrfence.AppearanceCell.Options.UseBackColor = True
    Me.af_rrfence.AppearanceCell.Options.UseTextOptions = True
    Me.af_rrfence.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.af_rrfence.Caption = "RR  Fence"
    Me.af_rrfence.Enabled = True
    Me.af_rrfence.FieldName = "af_rrfence"
    Me.af_rrfence.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.af_rrfence.Name = "af_rrfence"
    Me.af_rrfence.NTSRepositoryComboBox = Nothing
    Me.af_rrfence.NTSRepositoryItemCheck = Nothing
    Me.af_rrfence.NTSRepositoryItemMemo = Nothing
    Me.af_rrfence.NTSRepositoryItemText = Nothing
    Me.af_rrfence.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.af_rrfence.OptionsFilter.AllowFilter = False
    Me.af_rrfence.Visible = True
    Me.af_rrfence.VisibleIndex = 5
    '
    'af_fcorrlt
    '
    Me.af_fcorrlt.AppearanceCell.Options.UseBackColor = True
    Me.af_fcorrlt.AppearanceCell.Options.UseTextOptions = True
    Me.af_fcorrlt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.af_fcorrlt.Caption = "Fattore Correz. L.T."
    Me.af_fcorrlt.Enabled = True
    Me.af_fcorrlt.FieldName = "af_fcorrlt"
    Me.af_fcorrlt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.af_fcorrlt.Name = "af_fcorrlt"
    Me.af_fcorrlt.NTSRepositoryComboBox = Nothing
    Me.af_fcorrlt.NTSRepositoryItemCheck = Nothing
    Me.af_fcorrlt.NTSRepositoryItemMemo = Nothing
    Me.af_fcorrlt.NTSRepositoryItemText = Nothing
    Me.af_fcorrlt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.af_fcorrlt.OptionsFilter.AllowFilter = False
    Me.af_fcorrlt.Visible = True
    Me.af_fcorrlt.VisibleIndex = 6
    '
    'af_makebuy
    '
    Me.af_makebuy.AppearanceCell.Options.UseBackColor = True
    Me.af_makebuy.AppearanceCell.Options.UseTextOptions = True
    Me.af_makebuy.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.af_makebuy.Caption = "Make or Buy"
    Me.af_makebuy.Enabled = True
    Me.af_makebuy.FieldName = "af_makebuy"
    Me.af_makebuy.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.af_makebuy.Name = "af_makebuy"
    Me.af_makebuy.NTSRepositoryComboBox = Nothing
    Me.af_makebuy.NTSRepositoryItemCheck = Nothing
    Me.af_makebuy.NTSRepositoryItemMemo = Nothing
    Me.af_makebuy.NTSRepositoryItemText = Nothing
    Me.af_makebuy.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.af_makebuy.OptionsFilter.AllowFilter = False
    Me.af_makebuy.Visible = True
    Me.af_makebuy.VisibleIndex = 7
    '
    'af_codtcdc
    '
    Me.af_codtcdc.AppearanceCell.Options.UseBackColor = True
    Me.af_codtcdc.AppearanceCell.Options.UseTextOptions = True
    Me.af_codtcdc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.af_codtcdc.Caption = "Tipologia entità"
    Me.af_codtcdc.Enabled = True
    Me.af_codtcdc.FieldName = "af_codtcdc"
    Me.af_codtcdc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.af_codtcdc.Name = "af_codtcdc"
    Me.af_codtcdc.NTSRepositoryComboBox = Nothing
    Me.af_codtcdc.NTSRepositoryItemCheck = Nothing
    Me.af_codtcdc.NTSRepositoryItemMemo = Nothing
    Me.af_codtcdc.NTSRepositoryItemText = Nothing
    Me.af_codtcdc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.af_codtcdc.OptionsFilter.AllowFilter = False
    Me.af_codtcdc.Visible = True
    Me.af_codtcdc.VisibleIndex = 8
    '
    'xx_codtcdc
    '
    Me.xx_codtcdc.AppearanceCell.Options.UseBackColor = True
    Me.xx_codtcdc.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codtcdc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codtcdc.Caption = "Descr. Tipo. Entità"
    Me.xx_codtcdc.Enabled = False
    Me.xx_codtcdc.FieldName = "xx_codtcdc"
    Me.xx_codtcdc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codtcdc.Name = "xx_codtcdc"
    Me.xx_codtcdc.NTSRepositoryComboBox = Nothing
    Me.xx_codtcdc.NTSRepositoryItemCheck = Nothing
    Me.xx_codtcdc.NTSRepositoryItemMemo = Nothing
    Me.xx_codtcdc.NTSRepositoryItemText = Nothing
    Me.xx_codtcdc.OptionsColumn.AllowEdit = False
    Me.xx_codtcdc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codtcdc.OptionsColumn.ReadOnly = True
    Me.xx_codtcdc.OptionsFilter.AllowFilter = False
    Me.xx_codtcdc.Visible = True
    Me.xx_codtcdc.VisibleIndex = 9
    '
    'af_coddica
    '
    Me.af_coddica.AppearanceCell.Options.UseBackColor = True
    Me.af_coddica.AppearanceCell.Options.UseTextOptions = True
    Me.af_coddica.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.af_coddica.Caption = "Agg. Budget"
    Me.af_coddica.Enabled = True
    Me.af_coddica.FieldName = "af_coddica"
    Me.af_coddica.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.af_coddica.Name = "af_coddica"
    Me.af_coddica.NTSRepositoryComboBox = Nothing
    Me.af_coddica.NTSRepositoryItemCheck = Nothing
    Me.af_coddica.NTSRepositoryItemMemo = Nothing
    Me.af_coddica.NTSRepositoryItemText = Nothing
    Me.af_coddica.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.af_coddica.OptionsFilter.AllowFilter = False
    Me.af_coddica.Visible = True
    Me.af_coddica.VisibleIndex = 10
    '
    'xx_coddica
    '
    Me.xx_coddica.AppearanceCell.Options.UseBackColor = True
    Me.xx_coddica.AppearanceCell.Options.UseTextOptions = True
    Me.xx_coddica.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_coddica.Caption = "Descr. Agg. Budget"
    Me.xx_coddica.Enabled = False
    Me.xx_coddica.FieldName = "xx_coddica"
    Me.xx_coddica.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_coddica.Name = "xx_coddica"
    Me.xx_coddica.NTSRepositoryComboBox = Nothing
    Me.xx_coddica.NTSRepositoryItemCheck = Nothing
    Me.xx_coddica.NTSRepositoryItemMemo = Nothing
    Me.xx_coddica.NTSRepositoryItemText = Nothing
    Me.xx_coddica.OptionsColumn.AllowEdit = False
    Me.xx_coddica.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_coddica.OptionsColumn.ReadOnly = True
    Me.xx_coddica.OptionsFilter.AllowFilter = False
    Me.xx_coddica.Visible = True
    Me.xx_coddica.VisibleIndex = 11
    '
    'af_coddicv
    '
    Me.af_coddicv.AppearanceCell.Options.UseBackColor = True
    Me.af_coddicv.AppearanceCell.Options.UseTextOptions = True
    Me.af_coddicv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.af_coddicv.Caption = "Val. Agg. Budget"
    Me.af_coddicv.Enabled = True
    Me.af_coddicv.FieldName = "af_coddicv"
    Me.af_coddicv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.af_coddicv.Name = "af_coddicv"
    Me.af_coddicv.NTSRepositoryComboBox = Nothing
    Me.af_coddicv.NTSRepositoryItemCheck = Nothing
    Me.af_coddicv.NTSRepositoryItemMemo = Nothing
    Me.af_coddicv.NTSRepositoryItemText = Nothing
    Me.af_coddicv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.af_coddicv.OptionsFilter.AllowFilter = False
    Me.af_coddicv.Visible = True
    Me.af_coddicv.VisibleIndex = 12
    '
    'xx_coddicv
    '
    Me.xx_coddicv.AppearanceCell.Options.UseBackColor = True
    Me.xx_coddicv.AppearanceCell.Options.UseTextOptions = True
    Me.xx_coddicv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_coddicv.Caption = "Descr. Val. Agg. Budget"
    Me.xx_coddicv.Enabled = False
    Me.xx_coddicv.FieldName = "xx_coddicv"
    Me.xx_coddicv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_coddicv.Name = "xx_coddicv"
    Me.xx_coddicv.NTSRepositoryComboBox = Nothing
    Me.xx_coddicv.NTSRepositoryItemCheck = Nothing
    Me.xx_coddicv.NTSRepositoryItemMemo = Nothing
    Me.xx_coddicv.NTSRepositoryItemText = Nothing
    Me.xx_coddicv.OptionsColumn.AllowEdit = False
    Me.xx_coddicv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_coddicv.OptionsColumn.ReadOnly = True
    Me.xx_coddicv.OptionsFilter.AllowFilter = False
    Me.xx_coddicv.Visible = True
    Me.xx_coddicv.VisibleIndex = 13
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbGuida, Me.tlbEsci, Me.tlbProgressivi, Me.tlbProgressiviTotali, Me.tlbStrumenti, Me.tlbQualità})
    Me.NtsBarManager1.MaxItemId = 20
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbProgressivi, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbProgressiviTotali), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbNuovo
    '
    Me.tlbNuovo.Caption = "Nuovo"
    Me.tlbNuovo.Glyph = CType(resources.GetObject("tlbNuovo.Glyph"), System.Drawing.Image)
    Me.tlbNuovo.Id = 0
    Me.tlbNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2)
    Me.tlbNuovo.Name = "tlbNuovo"
    Me.tlbNuovo.Visible = True
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.Id = 1
    Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.Id = 3
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.Id = 2
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 4
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 18
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbQualità)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbQualità
    '
    Me.tlbQualità.Caption = "Controllo Qualità"
    Me.tlbQualità.Id = 19
    Me.tlbQualità.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q))
    Me.tlbQualità.Name = "tlbQualità"
    Me.tlbQualità.Visible = True
    '
    'tlbProgressivi
    '
    Me.tlbProgressivi.Caption = "Progressivi"
    Me.tlbProgressivi.Glyph = CType(resources.GetObject("tlbProgressivi.Glyph"), System.Drawing.Image)
    Me.tlbProgressivi.Id = 11
    Me.tlbProgressivi.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F7))
    Me.tlbProgressivi.Name = "tlbProgressivi"
    Me.tlbProgressivi.Visible = True
    '
    'tlbProgressiviTotali
    '
    Me.tlbProgressiviTotali.Caption = "Progressivi totali"
    Me.tlbProgressiviTotali.Glyph = CType(resources.GetObject("tlbProgressiviTotali.Glyph"), System.Drawing.Image)
    Me.tlbProgressiviTotali.Id = 16
    Me.tlbProgressiviTotali.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F8))
    Me.tlbProgressiviTotali.Name = "tlbProgressiviTotali"
    Me.tlbProgressiviTotali.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 9
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 10
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'FRMMGFASI
    '
    Me.ClientSize = New System.Drawing.Size(667, 366)
    Me.Controls.Add(Me.grFasi)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.HelpContext = ""
    Me.Name = "FRMMGFASI"
    Me.NTSLastControlFocussed = Me.grFasi
    Me.Text = "FASI ARTICOLO"
    CType(Me.grFasi, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvFasi, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttAf_makebuy As New DataTable()
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbProgressivi.GlyphPath = (oApp.ChildImageDir & "\ordini.gif")
        tlbProgressiviTotali.GlyphPath = (oApp.ChildImageDir & "\ordini_2.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        '  'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      dttAf_makebuy.Columns.Add("cod", GetType(String))
      dttAf_makebuy.Columns.Add("val", GetType(String))
      dttAf_makebuy.Rows.Add(New Object() {" ", "Come da distinta (Make)"})
      dttAf_makebuy.Rows.Add(New Object() {"B", "Buy"})
      dttAf_makebuy.AcceptChanges()

      grvFasi.NTSSetParam(oMenu, oApp.Tr(Me, 128828971049639891, "Fasi articolo"))
      codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828971071515031, "Cod. ditta"), 12, False)
      af_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828971087140131, "Codice articolo"), CLN__STD.CodartMaxLen, False)
      af_fase.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128828971103390235, "Fase"), tabartfasi)
      af_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828971119171586, "Descrizione Fase"), 40, True)
      af_codlavo.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128828971132921674, "Cod. lavorazione"), tablavo)
      xx_codlavo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828971149640531, "Descr. lavorazione"), 0, True)
      af_ultagg.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128828971161984360, "Ultimo agg."), False)
      af_opnome.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828971176046950, "Nome operatore"), 20, False)
      af_livmindb.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828971191047046, "Liv.Min.DB"), "0", 3, 0, 999)
      af_rrfence.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828971206515895, "RR  Fence"), "0", 3, 0, 999)
      af_fcorrlt.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828971221203489, "Fattore Correz. L.T."), "#,##0.00000", 10, 0, NTSCDec(9999.99999))
      af_makebuy.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128828971235266079, "Make or Buy"), dttAf_makebuy, "val", "cod")

      af_codtcdc.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129321201074263592, "Tipologia entità"), tabtcdc)
      xx_codtcdc.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129321201074419816, "Descr. Tipo. Entità"), 0, True)
      af_coddica.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 129321201074576040, "Agg. Budget"), tabdica, False)
      xx_coddica.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129321201074732264, "Descr. Agg. Budget"), 0, True)
      af_coddicv.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 129321201074888488, "Val. Agg. Budget"), tabdicv, False)
      xx_coddicv.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129321201075044712, "Descr. Val. Agg. Budget"), 0, True)


      af_fase.NTSSetRichiesto()

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

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvFasi.NTSSalvaRigaCorrente(dcFasi, oCleArmd.FasiRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleArmd.FasiSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleArmd.FasiRipristina(dcFasi.Position, dcFasi.Filter)
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

#Region "EventiForm"
  Public Overridable Sub FRMMGFASI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim dttPeca As New DataTable
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleArmd.FasiApri(DittaCorrente, oCleArmd.strFasiCodart, dsFasi) Then Me.Close()

      dcFasi.DataSource = dsFasi.Tables("ARTFASI")
      grFasi.DataSource = dcFasi

      af_fase.Enabled = False

      If oCleArmd.strPrgParent = "BNTCARTV" And oCleArmd.strFasiColore = "" Then
        tlbProgressivi.Enabled = False
        tlbProgressiviTotali.Enabled = False
      End If

      '---------------------------------------------------------------------
      If Not CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupCAE) Then
        af_coddica.Visible = False
        af_coddicv.Visible = False
        af_codtcdc.Visible = False
        af_coddica.Enabled = False
        af_coddicv.Enabled = False
        af_codtcdc.Enabled = False
        xx_coddica.Visible = False
        xx_coddicv.Visible = False
        xx_codtcdc.Visible = False
        xx_coddica.Enabled = False
        xx_coddicv.Enabled = False
        xx_codtcdc.Enabled = False
      End If

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      Me.Cursor = Cursors.Default

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGFASI_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcFasi.Dispose()
      dsFasi.Dispose()
    Catch
    End Try
  End Sub
  Public Overridable Sub FRMMGFASI_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvFasi.NTSNuovo()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Try
      If grvFasi.NTSGetCurrentDataRow() Is Nothing Then Return
      If Not oCleArmd.TestPreCancella(grvFasi.NTSGetCurrentDataRow()) Then Return
      If Not grvFasi.NTSDeleteRigaCorrente(dcFasi, True) Then Return
      oCleArmd.FasiSalva(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvFasi.NTSRipristinaRigaCorrenteBefore(dcFasi, True) Then Return
      oCleArmd.FasiRipristina(dcFasi.Position, dcFasi.Filter)
      grvFasi.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      'zoom standard
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      If ctrlTmp.GetType.ToString.IndexOf("NTSGrid") > -1 Then
        SetFastZoom(NTSCStr(CType(CType(ctrlTmp, NTSGrid).DefaultView, NTSGridView).EditingValue), oParam)

        If grFasi.Focused And grvFasi.FocusedColumn.Name.ToLower = "af_fase" Then
          'zoom su artfasi
          If grvFasi.NTSGetCurrentDataRow() Is Nothing Then Return
          If Trim(NTSCStr(grvFasi.NTSGetCurrentDataRow()!af_codart)) = "" Then
            oApp.MsgBoxInfo(oApp.Tr(Me, 128576675171932897, "Indicare l'articolo prima di inserire la fase."))
            Return
          End If

          SetFastZoom(NTSCStr(CType(CType(ctrlTmp, NTSGrid).DefaultView, NTSGridView).EditingValue), oParam)
          oParam.strTipo = NTSCStr(grvFasi.NTSGetCurrentDataRow()!af_codart)
          NTSZOOM.strIn = NTSCStr(grvFasi.EditingValue)
          NTSZOOM.ZoomStrIn("ZOOMARTFASI", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvFasi.EditingValue) Then grvFasi.SetFocusedRowCellValue(af_fase, NTSZOOM.strIn)
          Return
        ElseIf grFasi.Focused And grvFasi.FocusedColumn.Name.ToLower = "af_codtcdc" Then
          SetFastZoom(NTSCStr(grvFasi.EditingValue), oParam)
          NTSZOOM.strIn = NTSCStr(grvFasi.EditingValue)
          oParam.strTipo = "A"
          NTSZOOM.ZoomStrIn("ZOOMTABTCDC", "", oParam)
          If NTSZOOM.strIn <> NTSCStr(grvFasi.EditingValue) Then grvFasi.EditingValue = NTSZOOM.strIn
        ElseIf grFasi.Focused And grvFasi.FocusedColumn.Name.ToLower = "af_coddica" Then
          SetFastZoom(NTSCStr(grvFasi.EditingValue), oParam)
          NTSZOOM.strIn = NTSCStr(grvFasi.EditingValue)
          oParam.strTipo = "A"
          NTSZOOM.ZoomStrIn("ZOOMTABDICA", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvFasi.EditingValue) Then grvFasi.EditingValue = NTSZOOM.strIn
        ElseIf grFasi.Focused And grvFasi.FocusedColumn.Name.ToLower = "af_coddicv" Then
          If NTSCStr(grvFasi.NTSGetCurrentDataRow()!af_coddica).Trim = "" Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129309316650792127, "Prima specificare il codice di Aggregazione Budget"))
            Return
          End If

          SetFastZoom(NTSCStr(grvFasi.EditingValue), oParam)
          oParam.strCodice = NTSCStr(grvFasi.NTSGetCurrentDataRow()!af_coddica)
          NTSZOOM.strIn = NTSCStr(grvFasi.EditingValue)
          NTSZOOM.ZoomStrIn("ZOOMTABDICV", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvFasi.EditingValue) Then grvFasi.EditingValue = NTSZOOM.strIn
        Else
          '------------------------------------
          'zoom standard di textbox e griglia
          NTSCallStandardZoom()
        End If
      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        NTSCallStandardZoom()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbProgressivi_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbProgressivi.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      If Not Salva() Then Exit Sub

      If grvFasi.NTSGetCurrentDataRow() Is Nothing Then Exit Sub

      oPar.strPar1 = oCleArmd.strPrgParent
      oPar.strPar2 = oCleArmd.strFasiCodart & oCleArmd.strFasiColore
      oPar.dPar1 = NTSCInt(dsFasi.Tables("ARTFASI").Rows(dcFasi.Position)!af_fase)

      oMenu.RunChild("NTSInformatica", "FRMMGHLAP", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbProgressiviTotali_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbProgressiviTotali.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      If Not Salva() Then Exit Sub

      If grvFasi.NTSGetCurrentDataRow() Is Nothing Then Exit Sub

      oPar.strPar1 = oCleArmd.strPrgParent
      oPar.dPar1 = oCleArmd.dFasiPerqta
      oPar.strPar2 = oCleArmd.strFasiCodart & oCleArmd.strFasiColore
      oPar.dPar2 = NTSCInt(dsFasi.Tables("ARTFASI").Rows(dcFasi.Position)!af_fase)

      oMenu.RunChild("NTSInformatica", "FRMMGHLAT", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbQualità_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbQualità.ItemClick
    Dim strParam As String
    Try
      If CBool(oMenu.ModuliDittaDitt(DittaCorrente) And bsModSQ) Then
        If Not Salva() Then Exit Sub
        If grvFasi.NTSGetCurrentDataRow() Is Nothing Then Exit Sub
        strParam = "APRI;0;0;" & oCleArmd.strFasiCodart & ";0;0;" & NTSCStr(grvFasi.NTSGetCurrentDataRow()!af_fase) & ";0;" & Now.ToShortDateString & ";B;"
        oMenu.RunChild("NTSInformatica", "FRMSQCOQU", oApp.Tr(Me, 128580982589945330, "Controlli di Qualità"), DittaCorrente, "", "BNSQCOQU", Nothing, strParam, True, True)
      Else
        oApp.MsgBoxInfo(oApp.Tr(Me, 128580980054173938, "Modulo Sistema Qualità non abilitato." & vbCrLf & "Impossibile continuare."))
        Exit Sub
      End If

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
    If Not Salva() Then Return
    Me.Close()
  End Sub
#End Region

#Region "EventiGriglia"
  Public Overridable Sub grvFasi_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvFasi.NTSBeforeRowUpdate
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

  Public Overridable Sub grvFasi_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvFasi.NTSFocusedRowChanged
    'blocco le colonne non modificabili
    Dim dtrT As DataRow = Nothing
    Try
      If oCleArmd Is Nothing Then Return

      dtrT = grvFasi.NTSGetCurrentDataRow
      '------------------------------------
      'sono su una nuova riga
      If dtrT Is Nothing Then
        af_fase.Enabled = True
        Return
      End If

      If NTSCInt(dtrT!af_fase) <> 0 Then
        af_fase.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

End Class