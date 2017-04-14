'Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO

Public Class FROMGARTI
    ''  Inherits FRMMGARTI
    'Dim dcBarcode As BindingSource = New BindingSource
    ''Public WithEvents xxpn10 As NTSInformatica.NTSPanel
    'Public WithEvents xxpn10 As NTSInformatica.NTSPanel
    'Public WithEvents codditt As NTSInformatica.NTSGridColumn
    'Public WithEvents grCatalogo As NTSInformatica.NTSGrid
    'Public WithEvents grvCatalogo As NTSInformatica.NTSGridView
    'Public WithEvents tb_artcamp As NTSInformatica.NTSGridColumn
    'Public WithEvents tb_descr As NTSInformatica.NTSGridColumn
    'Public WithEvents tb_conto As NTSInformatica.NTSGridColumn
    'Public WithEvents xx_forn As NTSInformatica.NTSGridColumn
    'Public WithEvents tb_qtaord As NTSInformatica.NTSGridColumn
    'Public WithEvents tb_qtauso As NTSInformatica.NTSGridColumn
    'Public WithEvents tb_codfor As NTSInformatica.NTSGridColumn
    'Public WithEvents tb_alt As NTSInformatica.NTSGridColumn
    'Public WithEvents tb_fin As NTSInformatica.NTSGridColumn
    'Public WithEvents tb_costol As NTSInformatica.NTSGridColumn
    'Public WithEvents tb_costoc As NTSInformatica.NTSGridColumn
    'Public WithEvents tb_perc As NTSInformatica.NTSGridColumn
    'Public Overrides Sub FRMMGARTI_Load(ByVal sender As Object, ByVal e As System.EventArgs)

    '    MyBase.FRMMGARTI_Load(sender, e)


    '    'grCatalogo = CType(NTSFindControlByName(Me, "grCatalogo"), NTSGrid)
    '    'grvCatalogo = CType(NTSFindControlByName(Me, "grvCatalogo"), NTSGridView)
    '    xxpn10 = CType(Me.NTSFindControlByName(Me, "pnCata"), NTSPanel)
    '    grCatalogo = New NTSGrid
    '    grvCatalogo = New NTSGridView
    '    codditt = New NTSGridColumn
    '    tb_artcamp = New NTSGridColumn
    '    tb_descr = New NTSGridColumn
    '    tb_conto = New NTSGridColumn
    '    xx_forn = New NTSGridColumn
    '    tb_qtaord = New NTSGridColumn
    '    tb_qtauso = New NTSGridColumn
    '    tb_codfor = New NTSGridColumn
    '    tb_alt = New NTSGridColumn
    '    tb_fin = New NTSGridColumn
    '    tb_costol = New NTSGridColumn
    '    tb_costoc = New NTSGridColumn
    '    tb_perc = New NTSGridColumn




    '    CType(Me.grCatalogo, System.ComponentModel.ISupportInitialize).BeginInit()
    '    CType(Me.grvCatalogo, System.ComponentModel.ISupportInitialize).BeginInit()
    '    '    'Me.SuspendLayout()
    '    Me.xxpn10.Controls.Add(Me.grCatalogo)
    '    '    '
    '    '    'grBarc
    '    '    '
    '    Me.grCatalogo.Dock = System.Windows.Forms.DockStyle.Fill
    '    Me.grCatalogo.EmbeddedNavigator.Name = ""
    '    Me.grCatalogo.Location = New System.Drawing.Point(0, 0)
    '    Me.grCatalogo.MainView = Me.grvCatalogo
    '    Me.grCatalogo.Name = "grCatalogo"
    '    Me.grCatalogo.Size = New System.Drawing.Size(790, 294)
    '    Me.grCatalogo.TabIndex = 1
    '    Me.grCatalogo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvCatalogo})
    '    '    '
    '    '    'grvBarc
    '    '    '
    '    Me.grvCatalogo.ActiveFilterEnabled = True
    '    'Me.grvBarc.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.codditt, Me.bc_code, Me.tb_descr, Me.tb_conto, Me.xx_forn, Me.tb_qtaord, Me.tb_qtauso, Me.tb_codfor, Me.tb_alt, Me.tb_fin, Me.tb_costol, Me.tb_costoc, Me.codditt})
    '    Me.grvCatalogo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.codditt, Me.tb_artcamp, Me.tb_descr, Me.tb_conto, xx_forn, tb_qtaord, tb_qtauso, tb_codfor, tb_alt, tb_fin, tb_costol, tb_costoc, tb_perc})
    '    Me.grvCatalogo.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    '    Me.grvCatalogo.Enabled = True
    '    Me.grvCatalogo.GridControl = Me.grCatalogo
    '    Me.grvCatalogo.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
    '    Me.grvCatalogo.Name = "grvCatalogo"
    '    Me.grvCatalogo.NTSAllowDelete = True
    '    Me.grvCatalogo.NTSAllowInsert = True
    '    Me.grvCatalogo.NTSAllowUpdate = True
    '    Me.grvCatalogo.NTSMenuContext = Nothing
    '    Me.grvCatalogo.OptionsCustomization.AllowRowSizing = True
    '    Me.grvCatalogo.OptionsNavigation.EnterMoveNextColumn = True
    '    Me.grvCatalogo.OptionsNavigation.UseTabKey = False
    '    Me.grvCatalogo.OptionsSelection.EnableAppearanceFocusedRow = True
    '    Me.grvCatalogo.OptionsView.ColumnAutoWidth = False
    '    Me.grvCatalogo.OptionsView.EnableAppearanceEvenRow = True
    '    Me.grvCatalogo.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    '    Me.grvCatalogo.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Default
    '    Me.grvCatalogo.OptionsView.ShowGroupPanel = False
    '    Me.grvCatalogo.RowHeight = 16
    '    'Me.grvCatalogo.OptionsView.
    '    '    '
    '    '    'codditt
    '    '    '
    '    Me.codditt.AppearanceCell.Options.UseBackColor = True
    '    Me.codditt.AppearanceCell.Options.UseTextOptions = True
    '    Me.codditt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    '    Me.codditt.Caption = "Cod. ditta"
    '    Me.codditt.Enabled = False
    '    Me.codditt.FieldName = "codditt"
    '    Me.codditt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    '    Me.codditt.Name = "codditt"
    '    Me.codditt.NTSRepositoryComboBox = Nothing
    '    Me.codditt.NTSRepositoryItemCheck = Nothing
    '    Me.codditt.NTSRepositoryItemMemo = Nothing
    '    Me.codditt.NTSRepositoryItemText = Nothing
    '    Me.codditt.OptionsColumn.AllowEdit = False
    '    Me.codditt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    '    Me.codditt.OptionsColumn.ReadOnly = True
    '    Me.codditt.OptionsFilter.AllowFilter = False
    '    '    '
    '    '    'bc_code
    '    '    '
    '    Me.tb_artcamp.AppearanceCell.Options.UseBackColor = True
    '    Me.tb_artcamp.AppearanceCell.Options.UseTextOptions = True
    '    Me.tb_artcamp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    '    Me.tb_artcamp.Caption = "Barcode"
    '    Me.tb_artcamp.Enabled = True
    '    Me.tb_artcamp.FieldName = "tb_artcamp"
    '    Me.tb_artcamp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    '    Me.tb_artcamp.Name = "tb_artcamp"
    '    Me.tb_artcamp.NTSRepositoryComboBox = Nothing
    '    Me.tb_artcamp.NTSRepositoryItemCheck = Nothing
    '    Me.tb_artcamp.NTSRepositoryItemMemo = Nothing
    '    Me.tb_artcamp.NTSRepositoryItemText = Nothing
    '    Me.tb_artcamp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    '    Me.tb_artcamp.OptionsFilter.AllowFilter = False
    '    Me.tb_artcamp.Visible = True
    '    Me.tb_artcamp.VisibleIndex = 0
    '    '    '
    '    '    'tb_descr
    '    '    '
    '    Me.tb_descr.AppearanceCell.Options.UseBackColor = True
    '    Me.tb_descr.AppearanceCell.Options.UseTextOptions = True
    '    Me.tb_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    '    Me.tb_descr.Caption = "Descrizione"
    '    Me.tb_descr.Enabled = True
    '    Me.tb_descr.FieldName = "tb_descr"
    '    Me.tb_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    '    Me.tb_descr.Name = "tb_descr"
    '    Me.tb_descr.NTSRepositoryComboBox = Nothing
    '    Me.tb_descr.NTSRepositoryItemCheck = Nothing
    '    Me.tb_descr.NTSRepositoryItemMemo = Nothing
    '    Me.tb_descr.NTSRepositoryItemText = Nothing
    '    Me.tb_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    '    Me.tb_descr.OptionsFilter.AllowFilter = False
    '    Me.tb_descr.Visible = True
    '    Me.tb_descr.VisibleIndex = 1
    '    '    ''
    '    '    ''tb_conto
    '    '    ''
    '    Me.tb_conto.AppearanceCell.Options.UseBackColor = True
    '    Me.tb_conto.AppearanceCell.Options.UseTextOptions = True
    '    Me.tb_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    '    Me.tb_conto.Caption = "Quantità"
    '    Me.tb_conto.Enabled = True
    '    Me.tb_conto.FieldName = "tb_conto"
    '    Me.tb_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    '    Me.tb_conto.Name = "tb_conto"
    '    Me.tb_conto.NTSRepositoryComboBox = Nothing
    '    Me.tb_conto.NTSRepositoryItemCheck = Nothing
    '    Me.tb_conto.NTSRepositoryItemMemo = Nothing
    '    Me.tb_conto.NTSRepositoryItemText = Nothing
    '    Me.tb_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    '    Me.tb_conto.OptionsFilter.AllowFilter = False
    '    Me.tb_conto.Visible = True
    '    Me.tb_conto.VisibleIndex = 2
    '    '    ''
    '    '    ''xx_forn
    '    '    ''
    '    Me.xx_forn.AppearanceCell.Options.UseBackColor = True
    '    Me.xx_forn.AppearanceCell.Options.UseTextOptions = True
    '    Me.xx_forn.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    '    Me.xx_forn.Caption = "Data ins."
    '    Me.xx_forn.Enabled = True
    '    Me.xx_forn.FieldName = "xx_forn"
    '    Me.xx_forn.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    '    Me.xx_forn.Name = "xx_forn"
    '    Me.xx_forn.NTSRepositoryComboBox = Nothing
    '    Me.xx_forn.NTSRepositoryItemCheck = Nothing
    '    Me.xx_forn.NTSRepositoryItemMemo = Nothing
    '    Me.xx_forn.NTSRepositoryItemText = Nothing
    '    Me.xx_forn.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    '    Me.xx_forn.OptionsFilter.AllowFilter = False
    '    Me.xx_forn.Visible = True
    '    Me.xx_forn.VisibleIndex = 3
    '    '    ''
    '    '    ''tb_qtaord
    '    '    ''
    '    Me.tb_qtaord.AppearanceCell.Options.UseBackColor = True
    '    Me.tb_qtaord.AppearanceCell.Options.UseTextOptions = True
    '    Me.tb_qtaord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    '    Me.tb_qtaord.Caption = "Ora Inserimento"
    '    Me.tb_qtaord.Enabled = True
    '    Me.tb_qtaord.FieldName = "tb_qtaord"
    '    Me.tb_qtaord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    '    Me.tb_qtaord.Name = "tb_qtaord"
    '    Me.tb_qtaord.NTSRepositoryComboBox = Nothing
    '    Me.tb_qtaord.NTSRepositoryItemCheck = Nothing
    '    Me.tb_qtaord.NTSRepositoryItemMemo = Nothing
    '    Me.tb_qtaord.NTSRepositoryItemText = Nothing
    '    Me.tb_qtaord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    '    Me.tb_qtaord.OptionsFilter.AllowFilter = False
    '    '    ''
    '    '    ''tb_qtauso
    '    '    ''
    '    Me.tb_qtauso.AppearanceCell.Options.UseBackColor = True
    '    Me.tb_qtauso.AppearanceCell.Options.UseTextOptions = True
    '    Me.tb_qtauso.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    '    Me.tb_qtauso.Caption = "Data Aggiornamento"
    '    Me.tb_qtauso.Enabled = True
    '    Me.tb_qtauso.FieldName = "tb_qtauso"
    '    Me.tb_qtauso.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    '    Me.tb_qtauso.Name = "tb_qtauso"
    '    Me.tb_qtauso.NTSRepositoryComboBox = Nothing
    '    Me.tb_qtauso.NTSRepositoryItemCheck = Nothing
    '    Me.tb_qtauso.NTSRepositoryItemMemo = Nothing
    '    Me.tb_qtauso.NTSRepositoryItemText = Nothing
    '    Me.tb_qtauso.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    '    Me.tb_qtauso.OptionsFilter.AllowFilter = False
    '    '    ''
    '    '    ''tb_codfor
    '    '    ''
    '    Me.tb_codfor.AppearanceCell.Options.UseBackColor = True
    '    Me.tb_codfor.AppearanceCell.Options.UseTextOptions = True
    '    Me.tb_codfor.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    '    Me.tb_codfor.Caption = "Ora Aggiornamento"
    '    Me.tb_codfor.Enabled = True
    '    Me.tb_codfor.FieldName = "tb_codfor"
    '    Me.tb_codfor.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    '    Me.tb_codfor.Name = "tb_codfor"
    '    Me.tb_codfor.NTSRepositoryComboBox = Nothing
    '    Me.tb_codfor.NTSRepositoryItemCheck = Nothing
    '    Me.tb_codfor.NTSRepositoryItemMemo = Nothing
    '    Me.tb_codfor.NTSRepositoryItemText = Nothing
    '    Me.tb_codfor.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    '    Me.tb_codfor.OptionsFilter.AllowFilter = False
    '    '    ''
    '    '    ''tb_alt
    '    '    ''
    '    Me.tb_alt.AppearanceCell.Options.UseBackColor = True
    '    Me.tb_alt.AppearanceCell.Options.UseTextOptions = True
    '    Me.tb_alt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    '    Me.tb_alt.Caption = "Codice Articolo"
    '    Me.tb_alt.Enabled = True
    '    Me.tb_alt.FieldName = "tb_alt"
    '    Me.tb_alt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    '    Me.tb_alt.Name = "tb_alt"
    '    Me.tb_alt.NTSRepositoryComboBox = Nothing
    '    Me.tb_alt.NTSRepositoryItemCheck = Nothing
    '    Me.tb_alt.NTSRepositoryItemMemo = Nothing
    '    Me.tb_alt.NTSRepositoryItemText = Nothing
    '    Me.tb_alt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    '    Me.tb_alt.OptionsFilter.AllowFilter = False
    '    '    ''
    '    '    ''tb_fin
    '    '    ''
    '    Me.tb_fin.AppearanceCell.Options.UseBackColor = True
    '    Me.tb_fin.AppearanceCell.Options.UseTextOptions = True
    '    Me.tb_fin.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    '    Me.tb_fin.Caption = "Tipo"
    '    Me.tb_fin.Enabled = True
    '    Me.tb_fin.FieldName = "tb_fin"
    '    Me.tb_fin.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    '    Me.tb_fin.Name = "tb_fin"
    '    Me.tb_fin.NTSRepositoryComboBox = Nothing
    '    Me.tb_fin.NTSRepositoryItemCheck = Nothing
    '    Me.tb_fin.NTSRepositoryItemMemo = Nothing
    '    Me.tb_fin.NTSRepositoryItemText = Nothing
    '    Me.tb_fin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    '    Me.tb_fin.OptionsFilter.AllowFilter = False
    '    Me.tb_fin.Visible = True
    '    Me.tb_fin.VisibleIndex = 4
    '    '    ''
    '    '    ''tb_costol
    '    '    ''
    '    Me.tb_costol.AppearanceCell.Options.UseBackColor = True
    '    Me.tb_costol.AppearanceCell.Options.UseTextOptions = True
    '    Me.tb_costol.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    '    Me.tb_costol.Caption = "Taglia"
    '    Me.tb_costol.Enabled = True
    '    Me.tb_costol.FieldName = "tb_costol"
    '    Me.tb_costol.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    '    Me.tb_costol.Name = "tb_costol"
    '    Me.tb_costol.NTSRepositoryComboBox = Nothing
    '    Me.tb_costol.NTSRepositoryItemCheck = Nothing
    '    Me.tb_costol.NTSRepositoryItemMemo = Nothing
    '    Me.tb_costol.NTSRepositoryItemText = Nothing
    '    Me.tb_costol.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    '    Me.tb_costol.OptionsFilter.AllowFilter = False
    '    Me.tb_costol.Visible = True
    '    Me.tb_costol.VisibleIndex = 5
    '    '    '
    '    '    'tb_costoc
    '    '    '
    '    Me.tb_costoc.AppearanceCell.Options.UseBackColor = True
    '    Me.tb_costoc.AppearanceCell.Options.UseTextOptions = True
    '    Me.tb_costoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    '    Me.tb_costoc.Caption = "Fase"
    '    Me.tb_costoc.Enabled = True
    '    Me.tb_costoc.FieldName = "tb_costoc"
    '    Me.tb_costoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    '    Me.tb_costoc.Name = "tb_costoc"
    '    Me.tb_costoc.NTSRepositoryComboBox = Nothing
    '    Me.tb_costoc.NTSRepositoryItemCheck = Nothing
    '    Me.tb_costoc.NTSRepositoryItemMemo = Nothing
    '    Me.tb_costoc.NTSRepositoryItemText = Nothing
    '    Me.tb_costoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    '    Me.tb_costoc.OptionsFilter.AllowFilter = False
    '    Me.tb_costoc.Visible = True
    '    Me.tb_costoc.VisibleIndex = 6


    '    Me.tb_perc.AppearanceCell.Options.UseBackColor = True
    '    Me.tb_perc.AppearanceCell.Options.UseTextOptions = True
    '    Me.tb_perc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    '    Me.tb_perc.Caption = "Fase"
    '    Me.tb_perc.Enabled = True
    '    Me.tb_perc.FieldName = "tb_costoc"
    '    Me.tb_perc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    '    Me.tb_perc.Name = "tb_costoc"
    '    Me.tb_perc.NTSRepositoryComboBox = Nothing
    '    Me.tb_perc.NTSRepositoryItemCheck = Nothing
    '    Me.tb_perc.NTSRepositoryItemMemo = Nothing
    '    Me.tb_perc.NTSRepositoryItemText = Nothing
    '    Me.tb_perc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    '    Me.tb_perc.OptionsFilter.AllowFilter = False
    '    Me.tb_perc.Visible = True
    '    Me.tb_perc.VisibleIndex = 6


    '    CType(Me.grCatalogo, System.ComponentModel.ISupportInitialize).EndInit()
    '    CType(Me.grvCatalogo, System.ComponentModel.ISupportInitialize).EndInit()

    '    tb_artcamp.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828969870567651, "Articolo fornitore"), 50, False)
    '    tb_artcamp.NTSSetParamZoom("ZOOMARTEST")

    '    grvCatalogo.NTSSetParam(oMenu, oApp.Tr(Me, 131334622415832731, "grvCatalogo"))
    '    codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131334622415842743, "Cod. ditta"), 0, False)
    '    tb_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131334622415862756, "Descrizione"), 0, True)
    '    tb_conto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131334622415872762, "Quantità"), 0, True)
    '    xx_forn.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131334622415882764, "Data ins."), 0, True)
    '    tb_qtaord.NTSSetParamNUM(oMenu, oApp.Tr(Me, 131334622415892771, "Ora Inserimento"), "#,##0.00", 15)
    '    tb_qtauso.NTSSetParamNUM(oMenu, oApp.Tr(Me, 131334622415902777, "Data Aggiornamento"), "#,##0.00", 15)
    '    tb_codfor.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131334622415912784, "Ora Aggiornamento"), 0, True)
    '    tb_alt.NTSSetParamNUM(oMenu, oApp.Tr(Me, 131334622415922790, "Codice Articolo"), "#,##0.00", 15)
    '    tb_fin.NTSSetParamSTR(oMenu, oApp.Tr(Me, 131334622415932797, "Tipo"), 0, True)
    '    tb_costol.NTSSetParamNUM(oMenu, oApp.Tr(Me, 131334622415942804, "Taglia"), "#,##0.00", 15)
    '    tb_costoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 131334622415952810, "Fase"), "#,##0.00", 15)
    '    tb_costoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 131334622415962817, "Fase"), "#,##0.00", 15)


    '    '    grvBarc.NTSSetParam(oMenu, oApp.Tr(Me, 128828969839942259, "Codici a barre"))
    '    '    codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828969856036215, "Cod. ditta"), 12, False)
    '    'codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828970087445427, "Descr. fase"), 12, False)
    '    'tb_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828969887286615, "Descrizione"), 18, False)
    '    'tb_conto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828969902911815, "Quantità"), "#0", 9, 0, 99999999)
    '    'xx_forn.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828969921818307, "Data ins."), 50, False)
    '    'tb_qtaord.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828969934787223, "Ora Inserimento"), "#,##0.000", 15)
    '    'tb_qtauso.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828969950256171, "Data Aggiornamento"), "#,##0.000", 15)
    '    'tb_codfor.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828969973850223, "Ora Aggiornamento"), "#0", 9, 0, 99999999)
    '    'tb_alt.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828969988225407, "Codice Articolo"), "#,##0.000", 15)
    '    'tb_fin.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828970004319363, "Tipo"), 18, True)
    '    'tb_costol.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828970020257067, "Taglia"), "#,##0.000", 15)
    '    'tb_costoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128828970069945203, "Fase"), "#,##0.000", 15)


    '    tb_artcamp.NTSSetRichiesto()

    'End Sub

    Dim dcTmp As New BindingSource
    Public Overrides Function Apri(ByVal bNew As Boolean, ByVal bDuplica As Boolean) As Boolean
        If MyBase.Apri(bNew, bDuplica) Then
            Try
                If Not NTSFindControlByName(Me, "grhhcata") Is Nothing Then
                    Dim grTmp As NTSGrid = CType(NTSFindControlByName(Me, "grhhcata"), NTSGrid)
                    grTmp.DataSource = Nothing
                    dcTmp.DataSource = dsArti.Tables("TABHHAR")
                    grTmp.DataSource = dcTmp
                    'GctlSetRoules()
                End If
            Catch ex As Exception
            End Try
            Return True
            End If
    End Function
End Class

