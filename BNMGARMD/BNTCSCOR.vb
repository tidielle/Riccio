Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMTCSCOR
#Region "Variabili"
  Public oCleArmd As CLEMGARMD
  Public oCallParams As CLE__CLDP
  Public dsScor As New DataSet
  Public dcScor As BindingSource = New BindingSource()
  Public dsTmp As DataSet
  Public components As System.ComponentModel.IContainer

  Public bServer As Boolean

  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents grTco As NTSInformatica.NTSGrid
  Public WithEvents grvTco As NTSInformatica.NTSGridView
  Public WithEvents xx_quant01 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quant02 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quant03 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quant04 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quant05 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quant06 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quant07 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quant08 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quant09 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quant10 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quant11 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quant12 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quant13 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quant14 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quant15 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quant16 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quant17 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quant18 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quant19 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quant20 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quant21 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quant22 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quant23 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quant24 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_caption As NTSInformatica.NTSGridColumn
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

    oCleArmd.strScorChiamante = Param.strPar1
    oCleArmd.bScorRoot = Param.bPar1
    oCleArmd.strScorCodart = Param.strPar2
    oCleArmd.nScorCodtagl = Param.dPar1
    oCleArmd.nScorCodmaga = NTSCInt(Param.dPar2)
    oCleArmd.nScorFase = NTSCInt(Param.dPar3)

    Return True
  End Function
  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMTCSCOR))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grTco = New NTSInformatica.NTSGrid
    Me.grvTco = New NTSInformatica.NTSGridView
    Me.xx_caption = New NTSInformatica.NTSGridColumn
    Me.xx_quant01 = New NTSInformatica.NTSGridColumn
    Me.xx_quant02 = New NTSInformatica.NTSGridColumn
    Me.xx_quant03 = New NTSInformatica.NTSGridColumn
    Me.xx_quant04 = New NTSInformatica.NTSGridColumn
    Me.xx_quant05 = New NTSInformatica.NTSGridColumn
    Me.xx_quant06 = New NTSInformatica.NTSGridColumn
    Me.xx_quant07 = New NTSInformatica.NTSGridColumn
    Me.xx_quant08 = New NTSInformatica.NTSGridColumn
    Me.xx_quant09 = New NTSInformatica.NTSGridColumn
    Me.xx_quant10 = New NTSInformatica.NTSGridColumn
    Me.xx_quant11 = New NTSInformatica.NTSGridColumn
    Me.xx_quant12 = New NTSInformatica.NTSGridColumn
    Me.xx_quant13 = New NTSInformatica.NTSGridColumn
    Me.xx_quant14 = New NTSInformatica.NTSGridColumn
    Me.xx_quant15 = New NTSInformatica.NTSGridColumn
    Me.xx_quant16 = New NTSInformatica.NTSGridColumn
    Me.xx_quant17 = New NTSInformatica.NTSGridColumn
    Me.xx_quant18 = New NTSInformatica.NTSGridColumn
    Me.xx_quant19 = New NTSInformatica.NTSGridColumn
    Me.xx_quant20 = New NTSInformatica.NTSGridColumn
    Me.xx_quant21 = New NTSInformatica.NTSGridColumn
    Me.xx_quant22 = New NTSInformatica.NTSGridColumn
    Me.xx_quant23 = New NTSInformatica.NTSGridColumn
    Me.xx_quant24 = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grTco, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvTco, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbRipristina, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 17
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci, True)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
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
    Me.tlbRipristina.Id = 2
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 12
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'grTco
    '
    Me.grTco.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grTco.EmbeddedNavigator.Name = ""
    Me.grTco.Location = New System.Drawing.Point(0, 30)
    Me.grTco.MainView = Me.grvTco
    Me.grTco.Name = "grTco"
    Me.grTco.Size = New System.Drawing.Size(492, 134)
    Me.grTco.TabIndex = 4
    Me.grTco.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvTco})
    '
    'grvTco
    '
    Me.grvTco.ActiveFilterEnabled = False
    Me.grvTco.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_caption, Me.xx_quant01, Me.xx_quant02, Me.xx_quant03, Me.xx_quant04, Me.xx_quant05, Me.xx_quant06, Me.xx_quant07, Me.xx_quant08, Me.xx_quant09, Me.xx_quant10, Me.xx_quant11, Me.xx_quant12, Me.xx_quant13, Me.xx_quant14, Me.xx_quant15, Me.xx_quant16, Me.xx_quant17, Me.xx_quant18, Me.xx_quant19, Me.xx_quant20, Me.xx_quant21, Me.xx_quant22, Me.xx_quant23, Me.xx_quant24})
    Me.grvTco.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvTco.Enabled = True
    Me.grvTco.GridControl = Me.grTco
    Me.grvTco.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvTco.Name = "grvTco"
    Me.grvTco.NTSAllowDelete = True
    Me.grvTco.NTSAllowInsert = True
    Me.grvTco.NTSAllowUpdate = True
    Me.grvTco.NTSMenuContext = Nothing
    Me.grvTco.OptionsCustomization.AllowRowSizing = True
    Me.grvTco.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvTco.OptionsNavigation.UseTabKey = False
    Me.grvTco.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvTco.OptionsView.ColumnAutoWidth = False
    Me.grvTco.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvTco.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvTco.OptionsView.ShowGroupPanel = False
    Me.grvTco.RowHeight = 14
    '
    'xx_caption
    '
    Me.xx_caption.AppearanceCell.Options.UseBackColor = True
    Me.xx_caption.AppearanceCell.Options.UseTextOptions = True
    Me.xx_caption.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_caption.Caption = "    "
    Me.xx_caption.Enabled = False
    Me.xx_caption.FieldName = "xx_caption"
    Me.xx_caption.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_caption.Name = "xx_caption"
    Me.xx_caption.NTSRepositoryComboBox = Nothing
    Me.xx_caption.NTSRepositoryItemCheck = Nothing
    Me.xx_caption.NTSRepositoryItemText = Nothing
    Me.xx_caption.OptionsColumn.AllowEdit = False
    Me.xx_caption.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_caption.OptionsColumn.ReadOnly = True
    Me.xx_caption.OptionsFilter.AllowFilter = False
    Me.xx_caption.Visible = True
    Me.xx_caption.VisibleIndex = 0
    '
    'xx_quant01
    '
    Me.xx_quant01.AppearanceCell.Options.UseBackColor = True
    Me.xx_quant01.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quant01.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quant01.Caption = "QTA1"
    Me.xx_quant01.Enabled = True
    Me.xx_quant01.FieldName = "xx_quant01"
    Me.xx_quant01.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quant01.Name = "xx_quant01"
    Me.xx_quant01.NTSRepositoryComboBox = Nothing
    Me.xx_quant01.NTSRepositoryItemCheck = Nothing
    Me.xx_quant01.NTSRepositoryItemText = Nothing
    Me.xx_quant01.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quant01.OptionsFilter.AllowFilter = False
    Me.xx_quant01.Visible = True
    Me.xx_quant01.VisibleIndex = 1
    '
    'xx_quant02
    '
    Me.xx_quant02.AppearanceCell.Options.UseBackColor = True
    Me.xx_quant02.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quant02.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quant02.Caption = "QTA2"
    Me.xx_quant02.Enabled = True
    Me.xx_quant02.FieldName = "xx_quant02"
    Me.xx_quant02.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quant02.Name = "xx_quant02"
    Me.xx_quant02.NTSRepositoryComboBox = Nothing
    Me.xx_quant02.NTSRepositoryItemCheck = Nothing
    Me.xx_quant02.NTSRepositoryItemText = Nothing
    Me.xx_quant02.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quant02.OptionsFilter.AllowFilter = False
    Me.xx_quant02.Visible = True
    Me.xx_quant02.VisibleIndex = 2
    '
    'xx_quant03
    '
    Me.xx_quant03.AppearanceCell.Options.UseBackColor = True
    Me.xx_quant03.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quant03.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quant03.Caption = "QTA3"
    Me.xx_quant03.Enabled = True
    Me.xx_quant03.FieldName = "xx_quant03"
    Me.xx_quant03.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quant03.Name = "xx_quant03"
    Me.xx_quant03.NTSRepositoryComboBox = Nothing
    Me.xx_quant03.NTSRepositoryItemCheck = Nothing
    Me.xx_quant03.NTSRepositoryItemText = Nothing
    Me.xx_quant03.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quant03.OptionsFilter.AllowFilter = False
    Me.xx_quant03.Visible = True
    Me.xx_quant03.VisibleIndex = 3
    '
    'xx_quant04
    '
    Me.xx_quant04.AppearanceCell.Options.UseBackColor = True
    Me.xx_quant04.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quant04.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quant04.Caption = "QTA4"
    Me.xx_quant04.Enabled = True
    Me.xx_quant04.FieldName = "xx_quant04"
    Me.xx_quant04.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quant04.Name = "xx_quant04"
    Me.xx_quant04.NTSRepositoryComboBox = Nothing
    Me.xx_quant04.NTSRepositoryItemCheck = Nothing
    Me.xx_quant04.NTSRepositoryItemText = Nothing
    Me.xx_quant04.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quant04.OptionsFilter.AllowFilter = False
    Me.xx_quant04.Visible = True
    Me.xx_quant04.VisibleIndex = 4
    '
    'xx_quant05
    '
    Me.xx_quant05.AppearanceCell.Options.UseBackColor = True
    Me.xx_quant05.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quant05.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quant05.Caption = "QTA5"
    Me.xx_quant05.Enabled = True
    Me.xx_quant05.FieldName = "xx_quant05"
    Me.xx_quant05.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quant05.Name = "xx_quant05"
    Me.xx_quant05.NTSRepositoryComboBox = Nothing
    Me.xx_quant05.NTSRepositoryItemCheck = Nothing
    Me.xx_quant05.NTSRepositoryItemText = Nothing
    Me.xx_quant05.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quant05.OptionsFilter.AllowFilter = False
    Me.xx_quant05.Visible = True
    Me.xx_quant05.VisibleIndex = 5
    '
    'xx_quant06
    '
    Me.xx_quant06.AppearanceCell.Options.UseBackColor = True
    Me.xx_quant06.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quant06.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quant06.Caption = "QTA6"
    Me.xx_quant06.Enabled = True
    Me.xx_quant06.FieldName = "xx_quant06"
    Me.xx_quant06.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quant06.Name = "xx_quant06"
    Me.xx_quant06.NTSRepositoryComboBox = Nothing
    Me.xx_quant06.NTSRepositoryItemCheck = Nothing
    Me.xx_quant06.NTSRepositoryItemText = Nothing
    Me.xx_quant06.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quant06.OptionsFilter.AllowFilter = False
    Me.xx_quant06.Visible = True
    Me.xx_quant06.VisibleIndex = 6
    '
    'xx_quant07
    '
    Me.xx_quant07.AppearanceCell.Options.UseBackColor = True
    Me.xx_quant07.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quant07.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quant07.Caption = "QTA7"
    Me.xx_quant07.Enabled = True
    Me.xx_quant07.FieldName = "xx_quant07"
    Me.xx_quant07.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quant07.Name = "xx_quant07"
    Me.xx_quant07.NTSRepositoryComboBox = Nothing
    Me.xx_quant07.NTSRepositoryItemCheck = Nothing
    Me.xx_quant07.NTSRepositoryItemText = Nothing
    Me.xx_quant07.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quant07.OptionsFilter.AllowFilter = False
    Me.xx_quant07.Visible = True
    Me.xx_quant07.VisibleIndex = 7
    '
    'xx_quant08
    '
    Me.xx_quant08.AppearanceCell.Options.UseBackColor = True
    Me.xx_quant08.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quant08.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quant08.Caption = "QTA8"
    Me.xx_quant08.Enabled = True
    Me.xx_quant08.FieldName = "xx_quant08"
    Me.xx_quant08.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quant08.Name = "xx_quant08"
    Me.xx_quant08.NTSRepositoryComboBox = Nothing
    Me.xx_quant08.NTSRepositoryItemCheck = Nothing
    Me.xx_quant08.NTSRepositoryItemText = Nothing
    Me.xx_quant08.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quant08.OptionsFilter.AllowFilter = False
    Me.xx_quant08.Visible = True
    Me.xx_quant08.VisibleIndex = 8
    '
    'xx_quant09
    '
    Me.xx_quant09.AppearanceCell.Options.UseBackColor = True
    Me.xx_quant09.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quant09.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quant09.Caption = "QTA9"
    Me.xx_quant09.Enabled = True
    Me.xx_quant09.FieldName = "xx_quant09"
    Me.xx_quant09.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quant09.Name = "xx_quant09"
    Me.xx_quant09.NTSRepositoryComboBox = Nothing
    Me.xx_quant09.NTSRepositoryItemCheck = Nothing
    Me.xx_quant09.NTSRepositoryItemText = Nothing
    Me.xx_quant09.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quant09.OptionsFilter.AllowFilter = False
    Me.xx_quant09.Visible = True
    Me.xx_quant09.VisibleIndex = 9
    '
    'xx_quant10
    '
    Me.xx_quant10.AppearanceCell.Options.UseBackColor = True
    Me.xx_quant10.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quant10.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quant10.Caption = "QTA10"
    Me.xx_quant10.Enabled = True
    Me.xx_quant10.FieldName = "xx_quant10"
    Me.xx_quant10.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quant10.Name = "xx_quant10"
    Me.xx_quant10.NTSRepositoryComboBox = Nothing
    Me.xx_quant10.NTSRepositoryItemCheck = Nothing
    Me.xx_quant10.NTSRepositoryItemText = Nothing
    Me.xx_quant10.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quant10.OptionsFilter.AllowFilter = False
    Me.xx_quant10.Visible = True
    Me.xx_quant10.VisibleIndex = 10
    '
    'xx_quant11
    '
    Me.xx_quant11.AppearanceCell.Options.UseBackColor = True
    Me.xx_quant11.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quant11.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quant11.Caption = "QTA11"
    Me.xx_quant11.Enabled = True
    Me.xx_quant11.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quant11.Name = "xx_quant11"
    Me.xx_quant11.NTSRepositoryComboBox = Nothing
    Me.xx_quant11.NTSRepositoryItemCheck = Nothing
    Me.xx_quant11.NTSRepositoryItemText = Nothing
    Me.xx_quant11.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quant11.OptionsFilter.AllowFilter = False
    Me.xx_quant11.Visible = True
    Me.xx_quant11.VisibleIndex = 11
    '
    'xx_quant12
    '
    Me.xx_quant12.AppearanceCell.Options.UseBackColor = True
    Me.xx_quant12.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quant12.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quant12.Caption = "QTA12"
    Me.xx_quant12.Enabled = True
    Me.xx_quant12.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quant12.Name = "xx_quant12"
    Me.xx_quant12.NTSRepositoryComboBox = Nothing
    Me.xx_quant12.NTSRepositoryItemCheck = Nothing
    Me.xx_quant12.NTSRepositoryItemText = Nothing
    Me.xx_quant12.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quant12.OptionsFilter.AllowFilter = False
    Me.xx_quant12.Visible = True
    Me.xx_quant12.VisibleIndex = 12
    '
    'xx_quant13
    '
    Me.xx_quant13.AppearanceCell.Options.UseBackColor = True
    Me.xx_quant13.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quant13.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quant13.Caption = "QTA13"
    Me.xx_quant13.Enabled = True
    Me.xx_quant13.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quant13.Name = "xx_quant13"
    Me.xx_quant13.NTSRepositoryComboBox = Nothing
    Me.xx_quant13.NTSRepositoryItemCheck = Nothing
    Me.xx_quant13.NTSRepositoryItemText = Nothing
    Me.xx_quant13.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quant13.OptionsFilter.AllowFilter = False
    Me.xx_quant13.Visible = True
    Me.xx_quant13.VisibleIndex = 13
    '
    'xx_quant14
    '
    Me.xx_quant14.AppearanceCell.Options.UseBackColor = True
    Me.xx_quant14.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quant14.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quant14.Caption = "QTA14"
    Me.xx_quant14.Enabled = True
    Me.xx_quant14.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quant14.Name = "xx_quant14"
    Me.xx_quant14.NTSRepositoryComboBox = Nothing
    Me.xx_quant14.NTSRepositoryItemCheck = Nothing
    Me.xx_quant14.NTSRepositoryItemText = Nothing
    Me.xx_quant14.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quant14.OptionsFilter.AllowFilter = False
    Me.xx_quant14.Visible = True
    Me.xx_quant14.VisibleIndex = 14
    '
    'xx_quant15
    '
    Me.xx_quant15.AppearanceCell.Options.UseBackColor = True
    Me.xx_quant15.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quant15.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quant15.Caption = "QTA15"
    Me.xx_quant15.Enabled = True
    Me.xx_quant15.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quant15.Name = "xx_quant15"
    Me.xx_quant15.NTSRepositoryComboBox = Nothing
    Me.xx_quant15.NTSRepositoryItemCheck = Nothing
    Me.xx_quant15.NTSRepositoryItemText = Nothing
    Me.xx_quant15.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quant15.OptionsFilter.AllowFilter = False
    Me.xx_quant15.Visible = True
    Me.xx_quant15.VisibleIndex = 15
    '
    'xx_quant16
    '
    Me.xx_quant16.AppearanceCell.Options.UseBackColor = True
    Me.xx_quant16.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quant16.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quant16.Caption = "QTA16"
    Me.xx_quant16.Enabled = True
    Me.xx_quant16.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quant16.Name = "xx_quant16"
    Me.xx_quant16.NTSRepositoryComboBox = Nothing
    Me.xx_quant16.NTSRepositoryItemCheck = Nothing
    Me.xx_quant16.NTSRepositoryItemText = Nothing
    Me.xx_quant16.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quant16.OptionsFilter.AllowFilter = False
    Me.xx_quant16.Visible = True
    Me.xx_quant16.VisibleIndex = 16
    '
    'xx_quant17
    '
    Me.xx_quant17.AppearanceCell.Options.UseBackColor = True
    Me.xx_quant17.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quant17.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quant17.Caption = "QTA17"
    Me.xx_quant17.Enabled = True
    Me.xx_quant17.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quant17.Name = "xx_quant17"
    Me.xx_quant17.NTSRepositoryComboBox = Nothing
    Me.xx_quant17.NTSRepositoryItemCheck = Nothing
    Me.xx_quant17.NTSRepositoryItemText = Nothing
    Me.xx_quant17.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quant17.OptionsFilter.AllowFilter = False
    Me.xx_quant17.Visible = True
    Me.xx_quant17.VisibleIndex = 17
    '
    'xx_quant18
    '
    Me.xx_quant18.AppearanceCell.Options.UseBackColor = True
    Me.xx_quant18.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quant18.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quant18.Caption = "QTA18"
    Me.xx_quant18.Enabled = True
    Me.xx_quant18.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quant18.Name = "xx_quant18"
    Me.xx_quant18.NTSRepositoryComboBox = Nothing
    Me.xx_quant18.NTSRepositoryItemCheck = Nothing
    Me.xx_quant18.NTSRepositoryItemText = Nothing
    Me.xx_quant18.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quant18.OptionsFilter.AllowFilter = False
    Me.xx_quant18.Visible = True
    Me.xx_quant18.VisibleIndex = 18
    '
    'xx_quant19
    '
    Me.xx_quant19.AppearanceCell.Options.UseBackColor = True
    Me.xx_quant19.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quant19.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quant19.Caption = "QTA19"
    Me.xx_quant19.Enabled = True
    Me.xx_quant19.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quant19.Name = "xx_quant19"
    Me.xx_quant19.NTSRepositoryComboBox = Nothing
    Me.xx_quant19.NTSRepositoryItemCheck = Nothing
    Me.xx_quant19.NTSRepositoryItemText = Nothing
    Me.xx_quant19.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quant19.OptionsFilter.AllowFilter = False
    Me.xx_quant19.Visible = True
    Me.xx_quant19.VisibleIndex = 19
    '
    'xx_quant20
    '
    Me.xx_quant20.AppearanceCell.Options.UseBackColor = True
    Me.xx_quant20.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quant20.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quant20.Caption = "QTA20"
    Me.xx_quant20.Enabled = True
    Me.xx_quant20.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quant20.Name = "xx_quant20"
    Me.xx_quant20.NTSRepositoryComboBox = Nothing
    Me.xx_quant20.NTSRepositoryItemCheck = Nothing
    Me.xx_quant20.NTSRepositoryItemText = Nothing
    Me.xx_quant20.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quant20.OptionsFilter.AllowFilter = False
    Me.xx_quant20.Visible = True
    Me.xx_quant20.VisibleIndex = 20
    '
    'xx_quant21
    '
    Me.xx_quant21.AppearanceCell.Options.UseBackColor = True
    Me.xx_quant21.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quant21.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quant21.Caption = "QTA21"
    Me.xx_quant21.Enabled = True
    Me.xx_quant21.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quant21.Name = "xx_quant21"
    Me.xx_quant21.NTSRepositoryComboBox = Nothing
    Me.xx_quant21.NTSRepositoryItemCheck = Nothing
    Me.xx_quant21.NTSRepositoryItemText = Nothing
    Me.xx_quant21.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quant21.OptionsFilter.AllowFilter = False
    Me.xx_quant21.Visible = True
    Me.xx_quant21.VisibleIndex = 21
    '
    'xx_quant22
    '
    Me.xx_quant22.AppearanceCell.Options.UseBackColor = True
    Me.xx_quant22.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quant22.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quant22.Caption = "QTA22"
    Me.xx_quant22.Enabled = True
    Me.xx_quant22.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quant22.Name = "xx_quant22"
    Me.xx_quant22.NTSRepositoryComboBox = Nothing
    Me.xx_quant22.NTSRepositoryItemCheck = Nothing
    Me.xx_quant22.NTSRepositoryItemText = Nothing
    Me.xx_quant22.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quant22.OptionsFilter.AllowFilter = False
    Me.xx_quant22.Visible = True
    Me.xx_quant22.VisibleIndex = 22
    '
    'xx_quant23
    '
    Me.xx_quant23.AppearanceCell.Options.UseBackColor = True
    Me.xx_quant23.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quant23.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quant23.Caption = "QTA23"
    Me.xx_quant23.Enabled = True
    Me.xx_quant23.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quant23.Name = "xx_quant23"
    Me.xx_quant23.NTSRepositoryComboBox = Nothing
    Me.xx_quant23.NTSRepositoryItemCheck = Nothing
    Me.xx_quant23.NTSRepositoryItemText = Nothing
    Me.xx_quant23.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quant23.OptionsFilter.AllowFilter = False
    Me.xx_quant23.Visible = True
    Me.xx_quant23.VisibleIndex = 23
    '
    'xx_quant24
    '
    Me.xx_quant24.AppearanceCell.Options.UseBackColor = True
    Me.xx_quant24.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quant24.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quant24.Caption = "QTA24"
    Me.xx_quant24.Enabled = True
    Me.xx_quant24.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quant24.Name = "xx_quant24"
    Me.xx_quant24.NTSRepositoryComboBox = Nothing
    Me.xx_quant24.NTSRepositoryItemCheck = Nothing
    Me.xx_quant24.NTSRepositoryItemText = Nothing
    Me.xx_quant24.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quant24.OptionsFilter.AllowFilter = False
    Me.xx_quant24.Visible = True
    Me.xx_quant24.VisibleIndex = 24
    '
    'FRMTCSCOR
    '
    Me.ClientSize = New System.Drawing.Size(492, 164)
    Me.Controls.Add(Me.grTco)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.MaximizeBox = False
    Me.MaximumSize = New System.Drawing.Size(1500, 200)
    Me.MinimizeBox = False
    Me.Name = "FRMTCSCOR"
    Me.Text = "Scorte modello/variante"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grTco, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvTco, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)

    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvTco.NTSSetParam(oMenu, oApp.Tr(Me, 128517787751994000, "Griglia dettaglio taglie"))
      xx_caption.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128556540578040000, "Tipo riga"), 0, True)
      xx_quant01.NTSSetParamNUM(oMenu, "Quantità 1^ taglia", "#,##0", 6, -99999, 99999)
      xx_quant02.NTSSetParamNUM(oMenu, "Quantità 2^ taglia", "#,##0", 6, -99999, 99999)
      xx_quant03.NTSSetParamNUM(oMenu, "Quantità 3^ taglia", "#,##0", 6, -99999, 99999)
      xx_quant04.NTSSetParamNUM(oMenu, "Quantità 4^ taglia", "#,##0", 6, -99999, 99999)
      xx_quant05.NTSSetParamNUM(oMenu, "Quantità 5^ taglia", "#,##0", 6, -99999, 99999)
      xx_quant06.NTSSetParamNUM(oMenu, "Quantità 6^ taglia", "#,##0", 6, -99999, 99999)
      xx_quant07.NTSSetParamNUM(oMenu, "Quantità 7^ taglia", "#,##0", 6, -99999, 99999)
      xx_quant08.NTSSetParamNUM(oMenu, "Quantità 8^ taglia", "#,##0", 6, -99999, 99999)
      xx_quant09.NTSSetParamNUM(oMenu, "Quantità 9^ taglia", "#,##0", 6, -99999, 99999)
      xx_quant10.NTSSetParamNUM(oMenu, "Quantità 10^ taglia", "#,##0", 6, -99999, 99999)
      xx_quant11.NTSSetParamNUM(oMenu, "Quantità 11^ taglia", "#,##0", 6, -99999, 99999)
      xx_quant12.NTSSetParamNUM(oMenu, "Quantità 12^ taglia", "#,##0", 6, -99999, 99999)
      xx_quant13.NTSSetParamNUM(oMenu, "Quantità 13^ taglia", "#,##0", 6, -99999, 99999)
      xx_quant14.NTSSetParamNUM(oMenu, "Quantità 14^ taglia", "#,##0", 6, -99999, 99999)
      xx_quant15.NTSSetParamNUM(oMenu, "Quantità 15^ taglia", "#,##0", 6, -99999, 99999)
      xx_quant16.NTSSetParamNUM(oMenu, "Quantità 16^ taglia", "#,##0", 6, -99999, 99999)
      xx_quant17.NTSSetParamNUM(oMenu, "Quantità 17^ taglia", "#,##0", 6, -99999, 99999)
      xx_quant18.NTSSetParamNUM(oMenu, "Quantità 18^ taglia", "#,##0", 6, -99999, 99999)
      xx_quant19.NTSSetParamNUM(oMenu, "Quantità 19^ taglia", "#,##0", 6, -99999, 99999)
      xx_quant20.NTSSetParamNUM(oMenu, "Quantità 20^ taglia", "#,##0", 6, -99999, 99999)
      xx_quant21.NTSSetParamNUM(oMenu, "Quantità 21^ taglia", "#,##0", 6, -99999, 99999)
      xx_quant22.NTSSetParamNUM(oMenu, "Quantità 22^ taglia", "#,##0", 6, -99999, 99999)
      xx_quant23.NTSSetParamNUM(oMenu, "Quantità 23^ taglia", "#,##0", 6, -99999, 99999)
      xx_quant24.NTSSetParamNUM(oMenu, "Quantità 24^ taglia", "#,##0", 6, -99999, 99999)

      grvTco.NTSAllowDelete = False
      grvTco.NTSAllowUpdate = True
      grvTco.NTSAllowInsert = False

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

#Region "Eventi di Form"
  Public Overridable Sub FRMTCSCOR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim strDescr As String = ""
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      If Not Apri() Then
        Me.Close()
        Return
      End If

      '-------------------------------------------------
      'collego i dati alla griglia
      dsScor.Tables("TEMPTCO").AcceptChanges()
      dcScor.DataSource = dsScor.Tables("TEMPTCO")
      grTco.DataSource = dcScor

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      '-------------------------
      'carico l'intestazione delle colonne e nascondo quelle che non servono
      oMenu.ValCodiceDb(NTSCStr(oCleArmd.nScorCodtagl), DittaCorrente, "TABTAGL", "N", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then
        For i = 0 To grvTco.Columns.Count - 1
          l = NTSCInt(grvTco.Columns(i).Name.Substring(grvTco.Columns(i).Name.Length - 2))
          If l <> 0 Then
            grvTco.Columns(i).Caption = NTSCStr(dttTmp.Rows(0)("tb_dest" & l.ToString.PadLeft(2, "0"c)))
            If NTSCStr(grvTco.Columns(i).Caption).Trim = "" Then
              grvTco.Columns(i).Visible = False
            Else
              GctlSetVisEnab(grvTco.Columns(i), True)
            End If
          End If
        Next
      End If
      dttTmp.Clear()

      '-------------------------
      Select Case UCase(oCleArmd.strScorChiamante)
        Case "BNMGARMA"
          strDescr = "Variante: '" & oCleArmd.strScorCodart & "' / Mag.: " & NTSCStr(oCleArmd.nScorCodmaga) & "' / Fase: " & NTSCStr(oCleArmd.nScorFase)
        Case Else
          If oCleArmd.bScorRoot = True Then
            strDescr = "Scorte modello" & "Modello: '" & oCleArmd.strScorCodart & "'"
          Else
            strDescr = "Scorte variante" & "Variante: '" & oCleArmd.strScorCodart & "'"
          End If
      End Select

      Me.Text = oApp.Tr(Me, 128667431233395603, strDescr)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMTCSCOR_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub FRMTCSCOR_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcScor.Dispose()
      dsScor.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      If Not Salva() Then Return
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      dsScor.Tables("TEMPTCO").RejectChanges()
      grvTco.NTSMoveFirstColunn()
      grvTco.RefreshData()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Eventi di griglia"
  Public Overridable Sub grvTco_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvTco.NTSBeforeRowUpdate
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
#End Region

  Public Overridable Function Apri() As Boolean
    Dim i As Integer = 0
    Try
      '-------------------------------------------------
      'carico i dati nella griglia di analitico varianti
      If Not oCleArmd.ScorApri(DittaCorrente, dsTmp) Then Return False

      dsScor.Tables.Clear()
      dsScor.Tables.Add("TEMPTCO")

      '-------------------------------------------------
      'aggiungo la colonna dell'intestazione
      dsScor.Tables("TEMPTCO").Columns.Add("xx_caption", GetType(String))
      dsScor.Tables("TEMPTCO").Columns.Add("xx_tipo", GetType(String))

      'creo le colonne delle taglie
      For i = 1 To 24
        dsScor.Tables("TEMPTCO").Columns.Add("xx_quant" & i.ToString.PadLeft(2, "0"c), GetType(Integer))
      Next

      '-------------------------------------------------
      'aggiungo le righe 'Scorta minima', 'Scorta massima', 'Lotto standard'
      dsScor.Tables("TEMPTCO").Rows.Add(dsScor.Tables("TEMPTCO").NewRow())
      With dsScor.Tables("TEMPTCO").Rows(dsScor.Tables("TEMPTCO").Rows.Count - 1)
        !xx_tipo = "S" 'Scorta minima
        !xx_caption = "Scorta minima"
        For i = 1 To 24
          dsScor.Tables("TEMPTCO").Rows(dsScor.Tables("TEMPTCO").Rows.Count - 1)("xx_quant" & i.ToString.PadLeft(2, "0"c)) = NTSCDec(dsTmp.Tables("TEMPTCO").Rows(0)("xx_sm" & i.ToString.PadLeft(2, "0"c)))
        Next
      End With

      dsScor.Tables("TEMPTCO").Rows.Add(dsScor.Tables("TEMPTCO").NewRow())
      With dsScor.Tables("TEMPTCO").Rows(dsScor.Tables("TEMPTCO").Rows.Count - 1)
        !xx_tipo = "P" 'Scorta massima
        !xx_caption = "Scorta massima"
        For i = 1 To 24
          dsScor.Tables("TEMPTCO").Rows(dsScor.Tables("TEMPTCO").Rows.Count - 1)("xx_quant" & i.ToString.PadLeft(2, "0"c)) = NTSCDec(dsTmp.Tables("TEMPTCO").Rows(0)("xx_sx" & i.ToString.PadLeft(2, "0"c)))
        Next
      End With

      dsScor.Tables("TEMPTCO").Rows.Add(dsScor.Tables("TEMPTCO").NewRow())
      With dsScor.Tables("TEMPTCO").Rows(dsScor.Tables("TEMPTCO").Rows.Count - 1)
        !xx_tipo = "R" 'Lotto standard
        !xx_caption = "Lotto standard"
        For i = 1 To 24
          dsScor.Tables("TEMPTCO").Rows(dsScor.Tables("TEMPTCO").Rows.Count - 1)("xx_quant" & i.ToString.PadLeft(2, "0"c)) = NTSCDec(dsTmp.Tables("TEMPTCO").Rows(0)("xx_mo" & i.ToString.PadLeft(2, "0"c)))
        Next
      End With

      dsScor.Tables("TEMPTCO").AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Function Salva() As Boolean
    Dim i As Integer
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      With dsScor.Tables("TEMPTCO").Rows(0)
        For i = 1 To 24
          dsTmp.Tables("TEMPTCO").Rows(0)("xx_sm" & i.ToString.PadLeft(2, "0"c)) = NTSCDec(dsScor.Tables("TEMPTCO").Rows(0)("xx_quant" & i.ToString.PadLeft(2, "0"c)))
        Next
      End With

      With dsScor.Tables("TEMPTCO").Rows(1)
        For i = 1 To 24
          dsTmp.Tables("TEMPTCO").Rows(0)("xx_sx" & i.ToString.PadLeft(2, "0"c)) = NTSCDec(dsScor.Tables("TEMPTCO").Rows(1)("xx_quant" & i.ToString.PadLeft(2, "0"c)))
        Next
      End With

      With dsScor.Tables("TEMPTCO").Rows(2)
        For i = 1 To 24
          dsTmp.Tables("TEMPTCO").Rows(0)("xx_mo" & i.ToString.PadLeft(2, "0"c)) = NTSCDec(dsScor.Tables("TEMPTCO").Rows(2)("xx_quant" & i.ToString.PadLeft(2, "0"c)))
        Next
      End With

      If Not oCleArmd.ScorSalva(dsTmp) Then Return False

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

End Class
