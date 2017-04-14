Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGORSC
  Public oCleDocu As CLEMGDOCU
  Public oCallParams As CLE__CLDP
  Public dsOrdin As DataSet
  Public dcOrdin As BindingSource = New BindingSource()

  Private components As System.ComponentModel.IContainer

  Public Overridable Sub InitializeComponent()
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.ckVisAll = New NTSInformatica.NTSCheckBox
    Me.cmdEsci = New NTSInformatica.NTSButton
    Me.lbConto = New NTSInformatica.NTSLabel
    Me.lbContoLabel = New NTSInformatica.NTSLabel
    Me.pnGrid = New NTSInformatica.NTSPanel
    Me.grOrdin = New NTSInformatica.NTSGrid
    Me.grvOrdin = New NTSInformatica.NTSGridView
    Me.tt_tipork = New NTSInformatica.NTSGridColumn
    Me.tt_anno = New NTSInformatica.NTSGridColumn
    Me.tt_serie = New NTSInformatica.NTSGridColumn
    Me.tt_numero = New NTSInformatica.NTSGridColumn
    Me.tt_riga = New NTSInformatica.NTSGridColumn
    Me.tt_codart = New NTSInformatica.NTSGridColumn
    Me.tt_desart = New NTSInformatica.NTSGridColumn
    Me.tt_magaz = New NTSInformatica.NTSGridColumn
    Me.tt_desmaga = New NTSInformatica.NTSGridColumn
    Me.tt_datord = New NTSInformatica.NTSGridColumn
    Me.tt_datcons = New NTSInformatica.NTSGridColumn
    Me.tt_quant = New NTSInformatica.NTSGridColumn
    Me.tt_quapre = New NTSInformatica.NTSGridColumn
    Me.tt_quaeva = New NTSInformatica.NTSGridColumn
    Me.tt_annpar = New NTSInformatica.NTSGridColumn
    Me.tt_alfpar = New NTSInformatica.NTSGridColumn
    Me.tt_numpar = New NTSInformatica.NTSGridColumn
    Me.tt_riferim = New NTSInformatica.NTSGridColumn
    Me.tt_note = New NTSInformatica.NTSGridColumn
    Me.tt_prezzo = New NTSInformatica.NTSGridColumn
    Me.tt_scont1 = New NTSInformatica.NTSGridColumn
    Me.tt_scont2 = New NTSInformatica.NTSGridColumn
    Me.tt_scont3 = New NTSInformatica.NTSGridColumn
    Me.tt_valres = New NTSInformatica.NTSGridColumn
    Me.tt_qtares = New NTSInformatica.NTSGridColumn
    Me.tt_commeca = New NTSInformatica.NTSGridColumn
    Me.tt_subcommeca = New NTSInformatica.NTSGridColumn
    Me.tt_scont4 = New NTSInformatica.NTSGridColumn
    Me.tt_scont5 = New NTSInformatica.NTSGridColumn
    Me.tt_scont6 = New NTSInformatica.NTSGridColumn
    Me.tt_fase = New NTSInformatica.NTSGridColumn
    Me.tt_codappr = New NTSInformatica.NTSGridColumn
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.ckVisAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
    CType(Me.grOrdin, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvOrdin, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
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
    Me.pnTop.Controls.Add(Me.ckVisAll)
    Me.pnTop.Controls.Add(Me.cmdEsci)
    Me.pnTop.Controls.Add(Me.lbConto)
    Me.pnTop.Controls.Add(Me.lbContoLabel)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.Size = New System.Drawing.Size(799, 46)
    Me.pnTop.TabIndex = 6
    Me.pnTop.Text = "NtsPanel1"
    '
    'ckVisAll
    '
    Me.ckVisAll.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckVisAll.Location = New System.Drawing.Point(426, 12)
    Me.ckVisAll.Name = "ckVisAll"
    Me.ckVisAll.NTSCheckValue = "S"
    Me.ckVisAll.NTSUnCheckValue = "N"
    Me.ckVisAll.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckVisAll.Properties.Appearance.Options.UseBackColor = True
    Me.ckVisAll.Properties.Caption = "&Mostra tutti gli articoli"
    Me.ckVisAll.Size = New System.Drawing.Size(134, 19)
    Me.ckVisAll.TabIndex = 11
    '
    'cmdEsci
    '
    Me.cmdEsci.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdEsci.ImageText = ""
    Me.cmdEsci.Location = New System.Drawing.Point(704, 14)
    Me.cmdEsci.Name = "cmdEsci"
    Me.cmdEsci.Size = New System.Drawing.Size(83, 24)
    Me.cmdEsci.TabIndex = 10
    Me.cmdEsci.Text = "&Esci"
    '
    'lbConto
    '
    Me.lbConto.BackColor = System.Drawing.Color.Transparent
    Me.lbConto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbConto.Location = New System.Drawing.Point(70, 13)
    Me.lbConto.Name = "lbConto"
    Me.lbConto.NTSDbField = ""
    Me.lbConto.Size = New System.Drawing.Size(340, 20)
    Me.lbConto.TabIndex = 4
    Me.lbConto.Tooltip = ""
    Me.lbConto.UseMnemonic = False
    '
    'lbContoLabel
    '
    Me.lbContoLabel.AutoSize = True
    Me.lbContoLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbContoLabel.Location = New System.Drawing.Point(12, 14)
    Me.lbContoLabel.Name = "lbContoLabel"
    Me.lbContoLabel.NTSDbField = ""
    Me.lbContoLabel.Size = New System.Drawing.Size(36, 13)
    Me.lbContoLabel.TabIndex = 2
    Me.lbContoLabel.Text = "Conto"
    Me.lbContoLabel.Tooltip = ""
    Me.lbContoLabel.UseMnemonic = False
    '
    'pnGrid
    '
    Me.pnGrid.AllowDrop = True
    Me.pnGrid.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrid.Appearance.Options.UseBackColor = True
    Me.pnGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGrid.Controls.Add(Me.grOrdin)
    Me.pnGrid.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(0, 46)
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.Size = New System.Drawing.Size(799, 396)
    Me.pnGrid.TabIndex = 7
    Me.pnGrid.Text = "NtsPanel1"
    '
    'grOrdin
    '
    Me.grOrdin.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grOrdin.EmbeddedNavigator.Name = ""
    Me.grOrdin.Location = New System.Drawing.Point(0, 0)
    Me.grOrdin.MainView = Me.grvOrdin
    Me.grOrdin.Name = "grOrdin"
    Me.grOrdin.Size = New System.Drawing.Size(799, 396)
    Me.grOrdin.TabIndex = 6
    Me.grOrdin.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvOrdin})
    '
    'grvOrdin
    '
    Me.grvOrdin.ActiveFilterEnabled = False
    Me.grvOrdin.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tt_tipork, Me.tt_anno, Me.tt_serie, Me.tt_numero, Me.tt_riga, Me.tt_codart, Me.tt_desart, Me.tt_magaz, Me.tt_desmaga, Me.tt_datord, Me.tt_datcons, Me.tt_quant, Me.tt_quapre, Me.tt_quaeva, Me.tt_annpar, Me.tt_alfpar, Me.tt_numpar, Me.tt_riferim, Me.tt_note, Me.tt_prezzo, Me.tt_scont1, Me.tt_scont2, Me.tt_scont3, Me.tt_valres, Me.tt_qtares, Me.tt_commeca, Me.tt_subcommeca, Me.tt_scont4, Me.tt_scont5, Me.tt_scont6, Me.tt_fase, Me.tt_codappr})
    Me.grvOrdin.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvOrdin.Enabled = True
    Me.grvOrdin.GridControl = Me.grOrdin
    Me.grvOrdin.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvOrdin.Name = "grvOrdin"
    Me.grvOrdin.NTSAllowDelete = True
    Me.grvOrdin.NTSAllowInsert = True
    Me.grvOrdin.NTSAllowUpdate = True
    Me.grvOrdin.NTSMenuContext = Nothing
    Me.grvOrdin.OptionsCustomization.AllowRowSizing = True
    Me.grvOrdin.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvOrdin.OptionsNavigation.UseTabKey = False
    Me.grvOrdin.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvOrdin.OptionsView.ColumnAutoWidth = False
    Me.grvOrdin.OptionsView.EnableAppearanceEvenRow = True
    Me.grvOrdin.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvOrdin.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvOrdin.OptionsView.ShowGroupPanel = False
    Me.grvOrdin.RowHeight = 16
    '
    'tt_tipork
    '
    Me.tt_tipork.AppearanceCell.Options.UseBackColor = True
    Me.tt_tipork.AppearanceCell.Options.UseTextOptions = True
    Me.tt_tipork.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_tipork.Caption = "Tipo ord"
    Me.tt_tipork.Enabled = True
    Me.tt_tipork.FieldName = "tt_tipork"
    Me.tt_tipork.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_tipork.Name = "tt_tipork"
    Me.tt_tipork.NTSRepositoryComboBox = Nothing
    Me.tt_tipork.NTSRepositoryItemCheck = Nothing
    Me.tt_tipork.NTSRepositoryItemMemo = Nothing
    Me.tt_tipork.NTSRepositoryItemText = Nothing
    Me.tt_tipork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_tipork.OptionsFilter.AllowFilter = False
    Me.tt_tipork.Visible = True
    Me.tt_tipork.VisibleIndex = 0
    Me.tt_tipork.Width = 70
    '
    'tt_anno
    '
    Me.tt_anno.AppearanceCell.Options.UseBackColor = True
    Me.tt_anno.AppearanceCell.Options.UseTextOptions = True
    Me.tt_anno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_anno.Caption = "Anno ord"
    Me.tt_anno.Enabled = True
    Me.tt_anno.FieldName = "tt_anno"
    Me.tt_anno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_anno.Name = "tt_anno"
    Me.tt_anno.NTSRepositoryComboBox = Nothing
    Me.tt_anno.NTSRepositoryItemCheck = Nothing
    Me.tt_anno.NTSRepositoryItemMemo = Nothing
    Me.tt_anno.NTSRepositoryItemText = Nothing
    Me.tt_anno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_anno.OptionsFilter.AllowFilter = False
    Me.tt_anno.Visible = True
    Me.tt_anno.VisibleIndex = 1
    Me.tt_anno.Width = 70
    '
    'tt_serie
    '
    Me.tt_serie.AppearanceCell.Options.UseBackColor = True
    Me.tt_serie.AppearanceCell.Options.UseTextOptions = True
    Me.tt_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_serie.Caption = "Serie ord"
    Me.tt_serie.Enabled = True
    Me.tt_serie.FieldName = "tt_serie"
    Me.tt_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_serie.Name = "tt_serie"
    Me.tt_serie.NTSRepositoryComboBox = Nothing
    Me.tt_serie.NTSRepositoryItemCheck = Nothing
    Me.tt_serie.NTSRepositoryItemMemo = Nothing
    Me.tt_serie.NTSRepositoryItemText = Nothing
    Me.tt_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_serie.OptionsFilter.AllowFilter = False
    Me.tt_serie.Visible = True
    Me.tt_serie.VisibleIndex = 2
    Me.tt_serie.Width = 70
    '
    'tt_numero
    '
    Me.tt_numero.AppearanceCell.Options.UseBackColor = True
    Me.tt_numero.AppearanceCell.Options.UseTextOptions = True
    Me.tt_numero.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_numero.Caption = "Num. ord"
    Me.tt_numero.Enabled = True
    Me.tt_numero.FieldName = "tt_numero"
    Me.tt_numero.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_numero.Name = "tt_numero"
    Me.tt_numero.NTSRepositoryComboBox = Nothing
    Me.tt_numero.NTSRepositoryItemCheck = Nothing
    Me.tt_numero.NTSRepositoryItemMemo = Nothing
    Me.tt_numero.NTSRepositoryItemText = Nothing
    Me.tt_numero.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_numero.OptionsFilter.AllowFilter = False
    Me.tt_numero.Visible = True
    Me.tt_numero.VisibleIndex = 3
    Me.tt_numero.Width = 70
    '
    'tt_riga
    '
    Me.tt_riga.AppearanceCell.Options.UseBackColor = True
    Me.tt_riga.AppearanceCell.Options.UseTextOptions = True
    Me.tt_riga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_riga.Caption = "Riga ord"
    Me.tt_riga.Enabled = True
    Me.tt_riga.FieldName = "tt_riga"
    Me.tt_riga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_riga.Name = "tt_riga"
    Me.tt_riga.NTSRepositoryComboBox = Nothing
    Me.tt_riga.NTSRepositoryItemCheck = Nothing
    Me.tt_riga.NTSRepositoryItemMemo = Nothing
    Me.tt_riga.NTSRepositoryItemText = Nothing
    Me.tt_riga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_riga.OptionsFilter.AllowFilter = False
    Me.tt_riga.Visible = True
    Me.tt_riga.VisibleIndex = 4
    Me.tt_riga.Width = 70
    '
    'tt_codart
    '
    Me.tt_codart.AppearanceCell.Options.UseBackColor = True
    Me.tt_codart.AppearanceCell.Options.UseTextOptions = True
    Me.tt_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_codart.Caption = "Articolo"
    Me.tt_codart.Enabled = True
    Me.tt_codart.FieldName = "tt_codart"
    Me.tt_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_codart.Name = "tt_codart"
    Me.tt_codart.NTSRepositoryComboBox = Nothing
    Me.tt_codart.NTSRepositoryItemCheck = Nothing
    Me.tt_codart.NTSRepositoryItemMemo = Nothing
    Me.tt_codart.NTSRepositoryItemText = Nothing
    Me.tt_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_codart.OptionsFilter.AllowFilter = False
    Me.tt_codart.Visible = True
    Me.tt_codart.VisibleIndex = 5
    Me.tt_codart.Width = 70
    '
    'tt_desart
    '
    Me.tt_desart.AppearanceCell.Options.UseBackColor = True
    Me.tt_desart.AppearanceCell.Options.UseTextOptions = True
    Me.tt_desart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_desart.Caption = "Descr. art."
    Me.tt_desart.Enabled = True
    Me.tt_desart.FieldName = "tt_desart"
    Me.tt_desart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_desart.Name = "tt_desart"
    Me.tt_desart.NTSRepositoryComboBox = Nothing
    Me.tt_desart.NTSRepositoryItemCheck = Nothing
    Me.tt_desart.NTSRepositoryItemMemo = Nothing
    Me.tt_desart.NTSRepositoryItemText = Nothing
    Me.tt_desart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_desart.OptionsFilter.AllowFilter = False
    Me.tt_desart.Visible = True
    Me.tt_desart.VisibleIndex = 6
    Me.tt_desart.Width = 70
    '
    'tt_magaz
    '
    Me.tt_magaz.AppearanceCell.Options.UseBackColor = True
    Me.tt_magaz.AppearanceCell.Options.UseTextOptions = True
    Me.tt_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_magaz.Caption = "Magaz."
    Me.tt_magaz.Enabled = True
    Me.tt_magaz.FieldName = "tt_magaz"
    Me.tt_magaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_magaz.Name = "tt_magaz"
    Me.tt_magaz.NTSRepositoryComboBox = Nothing
    Me.tt_magaz.NTSRepositoryItemCheck = Nothing
    Me.tt_magaz.NTSRepositoryItemMemo = Nothing
    Me.tt_magaz.NTSRepositoryItemText = Nothing
    Me.tt_magaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_magaz.OptionsFilter.AllowFilter = False
    Me.tt_magaz.Visible = True
    Me.tt_magaz.VisibleIndex = 7
    Me.tt_magaz.Width = 70
    '
    'tt_desmaga
    '
    Me.tt_desmaga.AppearanceCell.Options.UseBackColor = True
    Me.tt_desmaga.AppearanceCell.Options.UseTextOptions = True
    Me.tt_desmaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_desmaga.Caption = "Descr. magaz."
    Me.tt_desmaga.Enabled = True
    Me.tt_desmaga.FieldName = "tt_desmaga"
    Me.tt_desmaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_desmaga.Name = "tt_desmaga"
    Me.tt_desmaga.NTSRepositoryComboBox = Nothing
    Me.tt_desmaga.NTSRepositoryItemCheck = Nothing
    Me.tt_desmaga.NTSRepositoryItemMemo = Nothing
    Me.tt_desmaga.NTSRepositoryItemText = Nothing
    Me.tt_desmaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_desmaga.OptionsFilter.AllowFilter = False
    Me.tt_desmaga.Visible = True
    Me.tt_desmaga.VisibleIndex = 8
    Me.tt_desmaga.Width = 70
    '
    'tt_datord
    '
    Me.tt_datord.AppearanceCell.Options.UseBackColor = True
    Me.tt_datord.AppearanceCell.Options.UseTextOptions = True
    Me.tt_datord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_datord.Caption = "Data ord."
    Me.tt_datord.Enabled = True
    Me.tt_datord.FieldName = "tt_datord"
    Me.tt_datord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_datord.Name = "tt_datord"
    Me.tt_datord.NTSRepositoryComboBox = Nothing
    Me.tt_datord.NTSRepositoryItemCheck = Nothing
    Me.tt_datord.NTSRepositoryItemMemo = Nothing
    Me.tt_datord.NTSRepositoryItemText = Nothing
    Me.tt_datord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_datord.OptionsFilter.AllowFilter = False
    Me.tt_datord.Visible = True
    Me.tt_datord.VisibleIndex = 9
    Me.tt_datord.Width = 70
    '
    'tt_datcons
    '
    Me.tt_datcons.AppearanceCell.Options.UseBackColor = True
    Me.tt_datcons.AppearanceCell.Options.UseTextOptions = True
    Me.tt_datcons.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_datcons.Caption = "Data cons."
    Me.tt_datcons.Enabled = True
    Me.tt_datcons.FieldName = "tt_datcons"
    Me.tt_datcons.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_datcons.Name = "tt_datcons"
    Me.tt_datcons.NTSRepositoryComboBox = Nothing
    Me.tt_datcons.NTSRepositoryItemCheck = Nothing
    Me.tt_datcons.NTSRepositoryItemMemo = Nothing
    Me.tt_datcons.NTSRepositoryItemText = Nothing
    Me.tt_datcons.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_datcons.OptionsFilter.AllowFilter = False
    Me.tt_datcons.Visible = True
    Me.tt_datcons.VisibleIndex = 10
    Me.tt_datcons.Width = 70
    '
    'tt_quant
    '
    Me.tt_quant.AppearanceCell.Options.UseBackColor = True
    Me.tt_quant.AppearanceCell.Options.UseTextOptions = True
    Me.tt_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_quant.Caption = "Quant."
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
    Me.tt_quant.VisibleIndex = 11
    Me.tt_quant.Width = 70
    '
    'tt_quapre
    '
    Me.tt_quapre.AppearanceCell.Options.UseBackColor = True
    Me.tt_quapre.AppearanceCell.Options.UseTextOptions = True
    Me.tt_quapre.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_quapre.Caption = "Qta prenot."
    Me.tt_quapre.Enabled = True
    Me.tt_quapre.FieldName = "tt_quapre"
    Me.tt_quapre.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_quapre.Name = "tt_quapre"
    Me.tt_quapre.NTSRepositoryComboBox = Nothing
    Me.tt_quapre.NTSRepositoryItemCheck = Nothing
    Me.tt_quapre.NTSRepositoryItemMemo = Nothing
    Me.tt_quapre.NTSRepositoryItemText = Nothing
    Me.tt_quapre.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_quapre.OptionsFilter.AllowFilter = False
    Me.tt_quapre.Visible = True
    Me.tt_quapre.VisibleIndex = 12
    Me.tt_quapre.Width = 70
    '
    'tt_quaeva
    '
    Me.tt_quaeva.AppearanceCell.Options.UseBackColor = True
    Me.tt_quaeva.AppearanceCell.Options.UseTextOptions = True
    Me.tt_quaeva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_quaeva.Caption = "Qta evasa"
    Me.tt_quaeva.Enabled = True
    Me.tt_quaeva.FieldName = "tt_quaeva"
    Me.tt_quaeva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_quaeva.Name = "tt_quaeva"
    Me.tt_quaeva.NTSRepositoryComboBox = Nothing
    Me.tt_quaeva.NTSRepositoryItemCheck = Nothing
    Me.tt_quaeva.NTSRepositoryItemMemo = Nothing
    Me.tt_quaeva.NTSRepositoryItemText = Nothing
    Me.tt_quaeva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_quaeva.OptionsFilter.AllowFilter = False
    Me.tt_quaeva.Visible = True
    Me.tt_quaeva.VisibleIndex = 13
    Me.tt_quaeva.Width = 70
    '
    'tt_annpar
    '
    Me.tt_annpar.AppearanceCell.Options.UseBackColor = True
    Me.tt_annpar.AppearanceCell.Options.UseTextOptions = True
    Me.tt_annpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_annpar.Caption = "Anno part."
    Me.tt_annpar.Enabled = True
    Me.tt_annpar.FieldName = "tt_annpar"
    Me.tt_annpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_annpar.Name = "tt_annpar"
    Me.tt_annpar.NTSRepositoryComboBox = Nothing
    Me.tt_annpar.NTSRepositoryItemCheck = Nothing
    Me.tt_annpar.NTSRepositoryItemMemo = Nothing
    Me.tt_annpar.NTSRepositoryItemText = Nothing
    Me.tt_annpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_annpar.OptionsFilter.AllowFilter = False
    Me.tt_annpar.Visible = True
    Me.tt_annpar.VisibleIndex = 14
    Me.tt_annpar.Width = 70
    '
    'tt_alfpar
    '
    Me.tt_alfpar.AppearanceCell.Options.UseBackColor = True
    Me.tt_alfpar.AppearanceCell.Options.UseTextOptions = True
    Me.tt_alfpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_alfpar.Caption = "erie part."
    Me.tt_alfpar.Enabled = True
    Me.tt_alfpar.FieldName = "tt_alfpar"
    Me.tt_alfpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_alfpar.Name = "tt_alfpar"
    Me.tt_alfpar.NTSRepositoryComboBox = Nothing
    Me.tt_alfpar.NTSRepositoryItemCheck = Nothing
    Me.tt_alfpar.NTSRepositoryItemMemo = Nothing
    Me.tt_alfpar.NTSRepositoryItemText = Nothing
    Me.tt_alfpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_alfpar.OptionsFilter.AllowFilter = False
    Me.tt_alfpar.Visible = True
    Me.tt_alfpar.VisibleIndex = 15
    Me.tt_alfpar.Width = 70
    '
    'tt_numpar
    '
    Me.tt_numpar.AppearanceCell.Options.UseBackColor = True
    Me.tt_numpar.AppearanceCell.Options.UseTextOptions = True
    Me.tt_numpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_numpar.Caption = "Num. part."
    Me.tt_numpar.Enabled = True
    Me.tt_numpar.FieldName = "tt_numpar"
    Me.tt_numpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_numpar.Name = "tt_numpar"
    Me.tt_numpar.NTSRepositoryComboBox = Nothing
    Me.tt_numpar.NTSRepositoryItemCheck = Nothing
    Me.tt_numpar.NTSRepositoryItemMemo = Nothing
    Me.tt_numpar.NTSRepositoryItemText = Nothing
    Me.tt_numpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_numpar.OptionsFilter.AllowFilter = False
    Me.tt_numpar.Visible = True
    Me.tt_numpar.VisibleIndex = 16
    Me.tt_numpar.Width = 70
    '
    'tt_riferim
    '
    Me.tt_riferim.AppearanceCell.Options.UseBackColor = True
    Me.tt_riferim.AppearanceCell.Options.UseTextOptions = True
    Me.tt_riferim.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_riferim.Caption = "Riferimenti"
    Me.tt_riferim.Enabled = True
    Me.tt_riferim.FieldName = "tt_riferim"
    Me.tt_riferim.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_riferim.Name = "tt_riferim"
    Me.tt_riferim.NTSRepositoryComboBox = Nothing
    Me.tt_riferim.NTSRepositoryItemCheck = Nothing
    Me.tt_riferim.NTSRepositoryItemMemo = Nothing
    Me.tt_riferim.NTSRepositoryItemText = Nothing
    Me.tt_riferim.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_riferim.OptionsFilter.AllowFilter = False
    Me.tt_riferim.Visible = True
    Me.tt_riferim.VisibleIndex = 17
    Me.tt_riferim.Width = 70
    '
    'tt_note
    '
    Me.tt_note.AppearanceCell.Options.UseBackColor = True
    Me.tt_note.AppearanceCell.Options.UseTextOptions = True
    Me.tt_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_note.Caption = "Note"
    Me.tt_note.Enabled = True
    Me.tt_note.FieldName = "tt_note"
    Me.tt_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_note.Name = "tt_note"
    Me.tt_note.NTSRepositoryComboBox = Nothing
    Me.tt_note.NTSRepositoryItemCheck = Nothing
    Me.tt_note.NTSRepositoryItemMemo = Nothing
    Me.tt_note.NTSRepositoryItemText = Nothing
    Me.tt_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_note.OptionsFilter.AllowFilter = False
    Me.tt_note.Visible = True
    Me.tt_note.VisibleIndex = 18
    Me.tt_note.Width = 70
    '
    'tt_prezzo
    '
    Me.tt_prezzo.AppearanceCell.Options.UseBackColor = True
    Me.tt_prezzo.AppearanceCell.Options.UseTextOptions = True
    Me.tt_prezzo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_prezzo.Caption = "Prezzo"
    Me.tt_prezzo.Enabled = True
    Me.tt_prezzo.FieldName = "tt_prezzo"
    Me.tt_prezzo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_prezzo.Name = "tt_prezzo"
    Me.tt_prezzo.NTSRepositoryComboBox = Nothing
    Me.tt_prezzo.NTSRepositoryItemCheck = Nothing
    Me.tt_prezzo.NTSRepositoryItemMemo = Nothing
    Me.tt_prezzo.NTSRepositoryItemText = Nothing
    Me.tt_prezzo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_prezzo.OptionsFilter.AllowFilter = False
    Me.tt_prezzo.Visible = True
    Me.tt_prezzo.VisibleIndex = 19
    Me.tt_prezzo.Width = 70
    '
    'tt_scont1
    '
    Me.tt_scont1.AppearanceCell.Options.UseBackColor = True
    Me.tt_scont1.AppearanceCell.Options.UseTextOptions = True
    Me.tt_scont1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_scont1.Caption = "Sconto 1"
    Me.tt_scont1.Enabled = True
    Me.tt_scont1.FieldName = "tt_scont1"
    Me.tt_scont1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_scont1.Name = "tt_scont1"
    Me.tt_scont1.NTSRepositoryComboBox = Nothing
    Me.tt_scont1.NTSRepositoryItemCheck = Nothing
    Me.tt_scont1.NTSRepositoryItemMemo = Nothing
    Me.tt_scont1.NTSRepositoryItemText = Nothing
    Me.tt_scont1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_scont1.OptionsFilter.AllowFilter = False
    Me.tt_scont1.Visible = True
    Me.tt_scont1.VisibleIndex = 20
    Me.tt_scont1.Width = 70
    '
    'tt_scont2
    '
    Me.tt_scont2.AppearanceCell.Options.UseBackColor = True
    Me.tt_scont2.AppearanceCell.Options.UseTextOptions = True
    Me.tt_scont2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_scont2.Caption = "Sconto 2"
    Me.tt_scont2.Enabled = True
    Me.tt_scont2.FieldName = "tt_scont2"
    Me.tt_scont2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_scont2.Name = "tt_scont2"
    Me.tt_scont2.NTSRepositoryComboBox = Nothing
    Me.tt_scont2.NTSRepositoryItemCheck = Nothing
    Me.tt_scont2.NTSRepositoryItemMemo = Nothing
    Me.tt_scont2.NTSRepositoryItemText = Nothing
    Me.tt_scont2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_scont2.OptionsFilter.AllowFilter = False
    Me.tt_scont2.Visible = True
    Me.tt_scont2.VisibleIndex = 21
    Me.tt_scont2.Width = 70
    '
    'tt_scont3
    '
    Me.tt_scont3.AppearanceCell.Options.UseBackColor = True
    Me.tt_scont3.AppearanceCell.Options.UseTextOptions = True
    Me.tt_scont3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_scont3.Caption = "Sconto 3"
    Me.tt_scont3.Enabled = True
    Me.tt_scont3.FieldName = "tt_scont3"
    Me.tt_scont3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_scont3.Name = "tt_scont3"
    Me.tt_scont3.NTSRepositoryComboBox = Nothing
    Me.tt_scont3.NTSRepositoryItemCheck = Nothing
    Me.tt_scont3.NTSRepositoryItemMemo = Nothing
    Me.tt_scont3.NTSRepositoryItemText = Nothing
    Me.tt_scont3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_scont3.OptionsFilter.AllowFilter = False
    Me.tt_scont3.Visible = True
    Me.tt_scont3.VisibleIndex = 22
    Me.tt_scont3.Width = 70
    '
    'tt_valres
    '
    Me.tt_valres.AppearanceCell.Options.UseBackColor = True
    Me.tt_valres.AppearanceCell.Options.UseTextOptions = True
    Me.tt_valres.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_valres.Caption = "Valore resid."
    Me.tt_valres.Enabled = True
    Me.tt_valres.FieldName = "tt_valres"
    Me.tt_valres.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_valres.Name = "tt_valres"
    Me.tt_valres.NTSRepositoryComboBox = Nothing
    Me.tt_valres.NTSRepositoryItemCheck = Nothing
    Me.tt_valres.NTSRepositoryItemMemo = Nothing
    Me.tt_valres.NTSRepositoryItemText = Nothing
    Me.tt_valres.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_valres.OptionsFilter.AllowFilter = False
    Me.tt_valres.Visible = True
    Me.tt_valres.VisibleIndex = 23
    Me.tt_valres.Width = 70
    '
    'tt_qtares
    '
    Me.tt_qtares.AppearanceCell.Options.UseBackColor = True
    Me.tt_qtares.AppearanceCell.Options.UseTextOptions = True
    Me.tt_qtares.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_qtares.Caption = "Qta resid."
    Me.tt_qtares.Enabled = True
    Me.tt_qtares.FieldName = "tt_qtares"
    Me.tt_qtares.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_qtares.Name = "tt_qtares"
    Me.tt_qtares.NTSRepositoryComboBox = Nothing
    Me.tt_qtares.NTSRepositoryItemCheck = Nothing
    Me.tt_qtares.NTSRepositoryItemMemo = Nothing
    Me.tt_qtares.NTSRepositoryItemText = Nothing
    Me.tt_qtares.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_qtares.OptionsFilter.AllowFilter = False
    Me.tt_qtares.Visible = True
    Me.tt_qtares.VisibleIndex = 24
    Me.tt_qtares.Width = 70
    '
    'tt_commeca
    '
    Me.tt_commeca.AppearanceCell.Options.UseBackColor = True
    Me.tt_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.tt_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_commeca.Caption = "Commessa"
    Me.tt_commeca.Enabled = True
    Me.tt_commeca.FieldName = "tt_commeca"
    Me.tt_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_commeca.Name = "tt_commeca"
    Me.tt_commeca.NTSRepositoryComboBox = Nothing
    Me.tt_commeca.NTSRepositoryItemCheck = Nothing
    Me.tt_commeca.NTSRepositoryItemMemo = Nothing
    Me.tt_commeca.NTSRepositoryItemText = Nothing
    Me.tt_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_commeca.OptionsFilter.AllowFilter = False
    Me.tt_commeca.Visible = True
    Me.tt_commeca.VisibleIndex = 25
    Me.tt_commeca.Width = 70
    '
    'tt_subcommeca
    '
    Me.tt_subcommeca.AppearanceCell.Options.UseBackColor = True
    Me.tt_subcommeca.AppearanceCell.Options.UseTextOptions = True
    Me.tt_subcommeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_subcommeca.Caption = "Sub comm."
    Me.tt_subcommeca.Enabled = True
    Me.tt_subcommeca.FieldName = "tt_subcommeca"
    Me.tt_subcommeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_subcommeca.Name = "tt_subcommeca"
    Me.tt_subcommeca.NTSRepositoryComboBox = Nothing
    Me.tt_subcommeca.NTSRepositoryItemCheck = Nothing
    Me.tt_subcommeca.NTSRepositoryItemMemo = Nothing
    Me.tt_subcommeca.NTSRepositoryItemText = Nothing
    Me.tt_subcommeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_subcommeca.OptionsFilter.AllowFilter = False
    Me.tt_subcommeca.Visible = True
    Me.tt_subcommeca.VisibleIndex = 26
    Me.tt_subcommeca.Width = 70
    '
    'tt_scont4
    '
    Me.tt_scont4.AppearanceCell.Options.UseBackColor = True
    Me.tt_scont4.AppearanceCell.Options.UseTextOptions = True
    Me.tt_scont4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_scont4.Caption = "Sconto 4"
    Me.tt_scont4.Enabled = True
    Me.tt_scont4.FieldName = "tt_scont4"
    Me.tt_scont4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_scont4.Name = "tt_scont4"
    Me.tt_scont4.NTSRepositoryComboBox = Nothing
    Me.tt_scont4.NTSRepositoryItemCheck = Nothing
    Me.tt_scont4.NTSRepositoryItemMemo = Nothing
    Me.tt_scont4.NTSRepositoryItemText = Nothing
    Me.tt_scont4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_scont4.OptionsFilter.AllowFilter = False
    Me.tt_scont4.Visible = True
    Me.tt_scont4.VisibleIndex = 27
    Me.tt_scont4.Width = 70
    '
    'tt_scont5
    '
    Me.tt_scont5.AppearanceCell.Options.UseBackColor = True
    Me.tt_scont5.AppearanceCell.Options.UseTextOptions = True
    Me.tt_scont5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_scont5.Caption = "Sconto 5"
    Me.tt_scont5.Enabled = True
    Me.tt_scont5.FieldName = "tt_scont5"
    Me.tt_scont5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_scont5.Name = "tt_scont5"
    Me.tt_scont5.NTSRepositoryComboBox = Nothing
    Me.tt_scont5.NTSRepositoryItemCheck = Nothing
    Me.tt_scont5.NTSRepositoryItemMemo = Nothing
    Me.tt_scont5.NTSRepositoryItemText = Nothing
    Me.tt_scont5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_scont5.OptionsFilter.AllowFilter = False
    Me.tt_scont5.Visible = True
    Me.tt_scont5.VisibleIndex = 28
    Me.tt_scont5.Width = 70
    '
    'tt_scont6
    '
    Me.tt_scont6.AppearanceCell.Options.UseBackColor = True
    Me.tt_scont6.AppearanceCell.Options.UseTextOptions = True
    Me.tt_scont6.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_scont6.Caption = "Sconto 6"
    Me.tt_scont6.Enabled = True
    Me.tt_scont6.FieldName = "tt_scont6"
    Me.tt_scont6.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_scont6.Name = "tt_scont6"
    Me.tt_scont6.NTSRepositoryComboBox = Nothing
    Me.tt_scont6.NTSRepositoryItemCheck = Nothing
    Me.tt_scont6.NTSRepositoryItemMemo = Nothing
    Me.tt_scont6.NTSRepositoryItemText = Nothing
    Me.tt_scont6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_scont6.OptionsFilter.AllowFilter = False
    Me.tt_scont6.Visible = True
    Me.tt_scont6.VisibleIndex = 29
    Me.tt_scont6.Width = 70
    '
    'tt_fase
    '
    Me.tt_fase.AppearanceCell.Options.UseBackColor = True
    Me.tt_fase.AppearanceCell.Options.UseTextOptions = True
    Me.tt_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_fase.Caption = "Fase"
    Me.tt_fase.Enabled = True
    Me.tt_fase.FieldName = "tt_fase"
    Me.tt_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_fase.Name = "tt_fase"
    Me.tt_fase.NTSRepositoryComboBox = Nothing
    Me.tt_fase.NTSRepositoryItemCheck = Nothing
    Me.tt_fase.NTSRepositoryItemMemo = Nothing
    Me.tt_fase.NTSRepositoryItemText = Nothing
    Me.tt_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_fase.OptionsFilter.AllowFilter = False
    Me.tt_fase.Visible = True
    Me.tt_fase.VisibleIndex = 30
    Me.tt_fase.Width = 70
    '
    'tt_codappr
    '
    Me.tt_codappr.AppearanceCell.Options.UseBackColor = True
    Me.tt_codappr.AppearanceCell.Options.UseTextOptions = True
    Me.tt_codappr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tt_codappr.Caption = "Approvv."
    Me.tt_codappr.Enabled = True
    Me.tt_codappr.FieldName = "tt_codappr"
    Me.tt_codappr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tt_codappr.Name = "tt_codappr"
    Me.tt_codappr.NTSRepositoryComboBox = Nothing
    Me.tt_codappr.NTSRepositoryItemCheck = Nothing
    Me.tt_codappr.NTSRepositoryItemMemo = Nothing
    Me.tt_codappr.NTSRepositoryItemText = Nothing
    Me.tt_codappr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tt_codappr.OptionsFilter.AllowFilter = False
    Me.tt_codappr.Visible = True
    Me.tt_codappr.VisibleIndex = 31
    Me.tt_codappr.Width = 70
    '
    'FRMMGORSC
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(799, 442)
    Me.Controls.Add(Me.pnGrid)
    Me.Controls.Add(Me.pnTop)
    Me.MinimizeBox = False
    Me.Name = "FRMMGORSC"
    Me.Text = "RIGHE ORDINI NON EVASE"
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.ckVisAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    CType(Me.grOrdin, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvOrdin, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

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
    'creo e attivo l'entity e inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGORSC", "BEMGDOCU", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 130086825750366925, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleDocu = CType(oTmp, CLEMGDOCU)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGGRSC", strRemoteServer, strRemotePort)
    AddHandler oCleDocu.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleDocu.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)

    Try
      Dim dttTipoRk As New DataTable()

      dttTipoRk.Columns.Add("cod", GetType(String))
      dttTipoRk.Columns.Add("val", GetType(String))
      dttTipoRk.Rows.Add(New Object() {"R", "Impegno cliente"})
      dttTipoRk.Rows.Add(New Object() {"O", "Ordine fornitore"})
      dttTipoRk.Rows.Add(New Object() {"H", "Ordine di produzione"})
      dttTipoRk.Rows.Add(New Object() {"X", "Impegno Trasferimento"})
      dttTipoRk.Rows.Add(New Object() {"Q", "Preventivo"})
      dttTipoRk.Rows.Add(New Object() {"#", "Impegno di commessa"})
      dttTipoRk.Rows.Add(New Object() {"V", "Impegno cliente aperto"})
      dttTipoRk.Rows.Add(New Object() {"$", "Ordine fornitore aperto"})
      dttTipoRk.Rows.Add(New Object() {"Y", "Impegno di produzione"})

      grvOrdin.NTSSetParam(oMenu, oApp.Tr(Me, 130086826294898175, "Ordini inevasi"))
      tt_tipork.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128570433763437500, "Tipo ord"), dttTipoRk, "val", "cod")
      tt_anno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128570433763593750, "Anno ord"), "0", 4, 0, 9999)
      tt_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128570433763750000, "Serie ord"), CLN__STD.SerieMaxLen, True)
      tt_numero.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128570433763906250, "Num. ord"), "0", 9, 0, 999999999)
      tt_riga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128570433764062500, "Riga ord"), "0", 9, 0, 999999999)
      tt_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128570433764218750, "Articolo"), 0, True)
      tt_desart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128570433764375000, "Descr. art."), 0, True)
      tt_magaz.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128570433764531250, "Magaz."), "0", 4, 0, 9999)
      tt_desmaga.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128570433764687500, "Descr. magaz."), 0, True)
      tt_datord.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128570433764843750, "Data ord."), True, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      tt_datcons.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128570433765000000, "Data cons."), True, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      tt_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128570433765156250, "Quant."), oApp.FormatQta, 6, -9999999999999, 9999999999999)
      tt_quapre.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128570433765312500, "Qta prenot."), oApp.FormatQta, 6, -9999999999999, 9999999999999)
      tt_quaeva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128570433765468750, "Qta evasa"), oApp.FormatQta, 6, -9999999999999, 9999999999999)
      tt_annpar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128570433765625000, "Anno part."), "0", 4, 0, 9999)
      tt_alfpar.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128570433765781250, "erie part."), CLN__STD.SerieMaxLen, True)
      tt_numpar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128570433765937500, "Num. part."), "0", 9, 0, 999999999)
      tt_riferim.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128570433766093750, "Riferimenti"), 0, True)
      tt_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128570433766250000, "Note"), 0, True)
      tt_prezzo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128570433766406250, "Prezzo"), oApp.FormatPrzUn, 20, -9999999999999, 9999999999999)
      tt_scont1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128570433766562500, "Sconto 1"), oApp.FormatSconti, 6, -100, 100)
      tt_scont2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128570433766718750, "Sconto 2"), oApp.FormatSconti, 6, -100, 100)
      tt_scont3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128570433766875000, "Sconto 3"), oApp.FormatSconti, 6, -100, 100)
      tt_valres.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128570433767031250, "Valore resid."), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      tt_qtares.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128570433767187500, "Qta resid."), oApp.FormatImporti, 6, -9999999999999, 9999999999999)
      tt_commeca.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128570433767343750, "Commessa"), "0", 9, 0, 999999999)
      tt_subcommeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128570433767500000, "Sub comm."), 0, True)
      tt_scont4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128570433767656250, "Sconto 4"), oApp.FormatSconti, 6, -100, 100)
      tt_scont5.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128570433767812500, "Sconto 5"), oApp.FormatSconti, 6, -100, 100)
      tt_scont6.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128570433767968750, "Sconto 6"), oApp.FormatSconti, 6, -100, 100)
      tt_fase.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128570433768125000, "Fase"), "0", 4, 0, 9999)
      tt_codappr.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128570433768281250, "Approvv."), "0", 4, 0, 9999)


      grvOrdin.NTSAllowDelete = False
      grvOrdin.NTSAllowInsert = False
      grvOrdin.Enabled = False

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

  Public Overridable Sub FRMMGORSC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      If oCallParams.strPar1 = "" Then
        'non è stato passato il cod articolo dal chiamante
        ckVisAll.Enabled = False
        If ckVisAll.Checked Then ckVisAll.Checked = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGORSC_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      ckVisAll_CheckedChanged(ckVisAll, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------

    End Try
  End Sub
  Public Overridable Sub FRMMGORSC_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcOrdin.Dispose()
      dsOrdin.Dispose()
    Catch
    End Try
  End Sub

  Public Overridable Sub cmdEsci_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEsci.Click
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckVisAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckVisAll.CheckedChanged
    Try
      '-----------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      'oCallParam.bPar1() 'Se l'opzione di registro è settata a True (-1) considera anche i preventivi (TESTORD.td_tipork = 'Q'), altrimenti li esclude come faceva prima
      If Not oCleDocu.GetPrecedentiOrdini(DittaCorrente, dsOrdin, IIf(ckVisAll.Checked, "", oCallParams.strPar1).ToString, _
                                          NTSCInt(oCallParams.dPar1), NTSCInt(oCallParams.dPar3)) Then
        Me.Close()
        Return
      End If

      Me.Cursor = Cursors.WaitCursor
      dcOrdin.DataSource = dsOrdin.Tables("ORDIN")
      dsOrdin.AcceptChanges()
      grOrdin.DataSource = dcOrdin

      If dsOrdin.Tables("ORDIN").Rows.Count > 0 Then
        With dsOrdin.Tables("ORDIN").Rows(0)
          lbConto.Text = !tt_conto.ToString & " - " & !tt_desconto.ToString
        End With
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
End Class
