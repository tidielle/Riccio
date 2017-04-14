Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGGRSC
  Public oCleDocu As CLEMGDOCU
  Public oCallParams As CLE__CLDP
  Public dsMovim As DataSet
  Public dcMovim As BindingSource = New BindingSource()

  'per accessi CRM
  Public bModuloCRM As Boolean = False
  Public bIsCRMUser As Boolean = False
  Public bAmm As Boolean = False
  Public strAccvis As String = ""
  Public strAccmod As String = ""
  Public strRegvis As String = ""
  Public strRegmod As String = ""
  Public lCodorgaOperat As Integer = 0
  Public nCodcageoperat As Integer = 0

  Private components As System.ComponentModel.IContainer

  Public Overridable Sub InitializeComponent()
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.cmdEsci = New NTSInformatica.NTSButton
    Me.lbFase = New NTSInformatica.NTSLabel
    Me.lbFaseLabel = New NTSInformatica.NTSLabel
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.lbMagaz = New NTSInformatica.NTSLabel
    Me.lbMagazLabel = New NTSInformatica.NTSLabel
    Me.lbConto = New NTSInformatica.NTSLabel
    Me.lbArticolo = New NTSInformatica.NTSLabel
    Me.lbContoLabel = New NTSInformatica.NTSLabel
    Me.lbArticoloLabel = New NTSInformatica.NTSLabel
    Me.pnGrid = New NTSInformatica.NTSPanel
    Me.grMovim = New NTSInformatica.NTSGrid
    Me.grvMovim = New NTSInformatica.NTSGridView
    Me.km_aammgg = New NTSInformatica.NTSGridColumn
    Me.km_tipork = New NTSInformatica.NTSGridColumn
    Me.km_serie = New NTSInformatica.NTSGridColumn
    Me.km_numdoc = New NTSInformatica.NTSGridColumn
    Me.km_causale = New NTSInformatica.NTSGridColumn
    Me.tb_descaum = New NTSInformatica.NTSGridColumn
    Me.tm_riferim = New NTSInformatica.NTSGridColumn
    Me.xx_scarichi = New NTSInformatica.NTSGridColumn
    Me.xx_carichi = New NTSInformatica.NTSGridColumn
    Me.xx_prezzo = New NTSInformatica.NTSGridColumn
    Me.mm_valore = New NTSInformatica.NTSGridColumn
    Me.mm_quant = New NTSInformatica.NTSGridColumn
    Me.mm_prelist = New NTSInformatica.NTSGridColumn
    Me.mm_prezzo = New NTSInformatica.NTSGridColumn
    Me.mm_preziva = New NTSInformatica.NTSGridColumn
    Me.mm_prezvalc = New NTSInformatica.NTSGridColumn
    Me.mm_scont1 = New NTSInformatica.NTSGridColumn
    Me.mm_scont2 = New NTSInformatica.NTSGridColumn
    Me.xx_lottox = New NTSInformatica.NTSGridColumn
    Me.km_magaz = New NTSInformatica.NTSGridColumn
    Me.km_subcommeca = New NTSInformatica.NTSGridColumn
    Me.km_ubicaz = New NTSInformatica.NTSGridColumn
    Me.mm_codcena = New NTSInformatica.NTSGridColumn
    Me.mm_codcfam = New NTSInformatica.NTSGridColumn
    Me.mm_codiva = New NTSInformatica.NTSGridColumn
    Me.mm_codnomc = New NTSInformatica.NTSGridColumn
    Me.mm_colli = New NTSInformatica.NTSGridColumn
    Me.mm_commeca = New NTSInformatica.NTSGridColumn
    Me.mm_controp = New NTSInformatica.NTSGridColumn
    Me.mm_misura1 = New NTSInformatica.NTSGridColumn
    Me.mm_misura2 = New NTSInformatica.NTSGridColumn
    Me.mm_misura3 = New NTSInformatica.NTSGridColumn
    Me.mm_ornum = New NTSInformatica.NTSGridColumn
    Me.mm_perqta = New NTSInformatica.NTSGridColumn
    Me.mm_provv = New NTSInformatica.NTSGridColumn
    Me.mm_provv2 = New NTSInformatica.NTSGridColumn
    Me.mm_scont3 = New NTSInformatica.NTSGridColumn
    Me.mm_scont4 = New NTSInformatica.NTSGridColumn
    Me.mm_scont5 = New NTSInformatica.NTSGridColumn
    Me.mm_scont6 = New NTSInformatica.NTSGridColumn
    Me.mm_vprovv = New NTSInformatica.NTSGridColumn
    Me.mm_vprovv2 = New NTSInformatica.NTSGridColumn
    Me.tm_valuta = New NTSInformatica.NTSGridColumn
    Me.km_conto = New NTSInformatica.NTSGridColumn
    Me.xx_conto = New NTSInformatica.NTSGridColumn
    Me.pnBottom = New NTSInformatica.NTSPanel
    Me.edTotCarichi = New NTSInformatica.NTSTextBoxNum
    Me.edTotScarichi = New NTSInformatica.NTSTextBoxNum
    Me.lbTotCarichi = New NTSInformatica.NTSLabel
    Me.lbTotscarichi = New NTSInformatica.NTSLabel
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
    CType(Me.grMovim, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvMovim, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBottom.SuspendLayout()
    CType(Me.edTotCarichi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTotScarichi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.pnTop.Controls.Add(Me.cmdEsci)
    Me.pnTop.Controls.Add(Me.lbFase)
    Me.pnTop.Controls.Add(Me.lbFaseLabel)
    Me.pnTop.Controls.Add(Me.cmdSeleziona)
    Me.pnTop.Controls.Add(Me.lbMagaz)
    Me.pnTop.Controls.Add(Me.lbMagazLabel)
    Me.pnTop.Controls.Add(Me.lbConto)
    Me.pnTop.Controls.Add(Me.lbArticolo)
    Me.pnTop.Controls.Add(Me.lbContoLabel)
    Me.pnTop.Controls.Add(Me.lbArticoloLabel)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.Size = New System.Drawing.Size(799, 72)
    Me.pnTop.TabIndex = 6
    Me.pnTop.Text = "NtsPanel1"
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
    'lbFase
    '
    Me.lbFase.BackColor = System.Drawing.Color.Transparent
    Me.lbFase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbFase.Location = New System.Drawing.Point(477, 15)
    Me.lbFase.Name = "lbFase"
    Me.lbFase.NTSDbField = ""
    Me.lbFase.Size = New System.Drawing.Size(39, 20)
    Me.lbFase.TabIndex = 9
    Me.lbFase.TextAlign = System.Drawing.ContentAlignment.TopRight
    Me.lbFase.Tooltip = ""
    Me.lbFase.UseMnemonic = False
    '
    'lbFaseLabel
    '
    Me.lbFaseLabel.AutoSize = True
    Me.lbFaseLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbFaseLabel.Location = New System.Drawing.Point(415, 14)
    Me.lbFaseLabel.Name = "lbFaseLabel"
    Me.lbFaseLabel.NTSDbField = ""
    Me.lbFaseLabel.Size = New System.Drawing.Size(30, 13)
    Me.lbFaseLabel.TabIndex = 8
    Me.lbFaseLabel.Text = "Fase"
    Me.lbFaseLabel.Tooltip = ""
    Me.lbFaseLabel.UseMnemonic = False
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(704, 39)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.Size = New System.Drawing.Size(83, 24)
    Me.cmdSeleziona.TabIndex = 7
    Me.cmdSeleziona.Text = "&Seleziona"
    '
    'lbMagaz
    '
    Me.lbMagaz.BackColor = System.Drawing.Color.Transparent
    Me.lbMagaz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbMagaz.Location = New System.Drawing.Point(478, 39)
    Me.lbMagaz.Name = "lbMagaz"
    Me.lbMagaz.NTSDbField = ""
    Me.lbMagaz.Size = New System.Drawing.Size(38, 20)
    Me.lbMagaz.TabIndex = 6
    Me.lbMagaz.TextAlign = System.Drawing.ContentAlignment.TopRight
    Me.lbMagaz.Tooltip = ""
    Me.lbMagaz.UseMnemonic = False
    '
    'lbMagazLabel
    '
    Me.lbMagazLabel.AutoSize = True
    Me.lbMagazLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbMagazLabel.Location = New System.Drawing.Point(415, 40)
    Me.lbMagazLabel.Name = "lbMagazLabel"
    Me.lbMagazLabel.NTSDbField = ""
    Me.lbMagazLabel.Size = New System.Drawing.Size(57, 13)
    Me.lbMagazLabel.TabIndex = 5
    Me.lbMagazLabel.Text = "Magazzino"
    Me.lbMagazLabel.Tooltip = ""
    Me.lbMagazLabel.UseMnemonic = False
    '
    'lbConto
    '
    Me.lbConto.BackColor = System.Drawing.Color.Transparent
    Me.lbConto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbConto.Location = New System.Drawing.Point(69, 39)
    Me.lbConto.Name = "lbConto"
    Me.lbConto.NTSDbField = ""
    Me.lbConto.Size = New System.Drawing.Size(340, 20)
    Me.lbConto.TabIndex = 4
    Me.lbConto.Tooltip = ""
    Me.lbConto.UseMnemonic = False
    '
    'lbArticolo
    '
    Me.lbArticolo.BackColor = System.Drawing.Color.Transparent
    Me.lbArticolo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbArticolo.Location = New System.Drawing.Point(69, 14)
    Me.lbArticolo.Name = "lbArticolo"
    Me.lbArticolo.NTSDbField = ""
    Me.lbArticolo.Size = New System.Drawing.Size(340, 20)
    Me.lbArticolo.TabIndex = 3
    Me.lbArticolo.Tooltip = ""
    Me.lbArticolo.UseMnemonic = False
    '
    'lbContoLabel
    '
    Me.lbContoLabel.AutoSize = True
    Me.lbContoLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbContoLabel.Location = New System.Drawing.Point(11, 40)
    Me.lbContoLabel.Name = "lbContoLabel"
    Me.lbContoLabel.NTSDbField = ""
    Me.lbContoLabel.Size = New System.Drawing.Size(36, 13)
    Me.lbContoLabel.TabIndex = 2
    Me.lbContoLabel.Text = "Conto"
    Me.lbContoLabel.Tooltip = ""
    Me.lbContoLabel.UseMnemonic = False
    '
    'lbArticoloLabel
    '
    Me.lbArticoloLabel.AutoSize = True
    Me.lbArticoloLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbArticoloLabel.Location = New System.Drawing.Point(11, 15)
    Me.lbArticoloLabel.Name = "lbArticoloLabel"
    Me.lbArticoloLabel.NTSDbField = ""
    Me.lbArticoloLabel.Size = New System.Drawing.Size(43, 13)
    Me.lbArticoloLabel.TabIndex = 1
    Me.lbArticoloLabel.Text = "Articolo"
    Me.lbArticoloLabel.Tooltip = ""
    Me.lbArticoloLabel.UseMnemonic = False
    '
    'pnGrid
    '
    Me.pnGrid.AllowDrop = True
    Me.pnGrid.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrid.Appearance.Options.UseBackColor = True
    Me.pnGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGrid.Controls.Add(Me.grMovim)
    Me.pnGrid.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(0, 72)
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.Size = New System.Drawing.Size(799, 326)
    Me.pnGrid.TabIndex = 7
    Me.pnGrid.Text = "NtsPanel1"
    '
    'grMovim
    '
    Me.grMovim.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grMovim.EmbeddedNavigator.Name = ""
    Me.grMovim.Location = New System.Drawing.Point(0, 0)
    Me.grMovim.MainView = Me.grvMovim
    Me.grMovim.Name = "grMovim"
    Me.grMovim.Size = New System.Drawing.Size(799, 326)
    Me.grMovim.TabIndex = 6
    Me.grMovim.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvMovim})
    '
    'grvMovim
    '
    Me.grvMovim.ActiveFilterEnabled = False
    Me.grvMovim.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.km_aammgg, Me.km_tipork, Me.km_serie, Me.km_numdoc, Me.km_causale, Me.tb_descaum, Me.tm_riferim, Me.xx_scarichi, Me.xx_carichi, Me.xx_prezzo, Me.mm_valore, Me.mm_quant, Me.mm_prelist, Me.mm_prezzo, Me.mm_preziva, Me.mm_prezvalc, Me.mm_scont1, Me.mm_scont2, Me.xx_lottox, Me.km_magaz, Me.km_subcommeca, Me.km_ubicaz, Me.mm_codcena, Me.mm_codcfam, Me.mm_codiva, Me.mm_codnomc, Me.mm_colli, Me.mm_commeca, Me.mm_controp, Me.mm_misura1, Me.mm_misura2, Me.mm_misura3, Me.mm_ornum, Me.mm_perqta, Me.mm_provv, Me.mm_provv2, Me.mm_scont3, Me.mm_scont4, Me.mm_scont5, Me.mm_scont6, Me.mm_vprovv, Me.mm_vprovv2, Me.tm_valuta, Me.km_conto, Me.xx_conto})
    Me.grvMovim.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvMovim.Enabled = True
    Me.grvMovim.GridControl = Me.grMovim
    Me.grvMovim.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvMovim.Name = "grvMovim"
    Me.grvMovim.NTSAllowDelete = True
    Me.grvMovim.NTSAllowInsert = True
    Me.grvMovim.NTSAllowUpdate = True
    Me.grvMovim.NTSMenuContext = Nothing
    Me.grvMovim.OptionsCustomization.AllowRowSizing = True
    Me.grvMovim.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvMovim.OptionsNavigation.UseTabKey = False
    Me.grvMovim.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvMovim.OptionsView.ColumnAutoWidth = False
    Me.grvMovim.OptionsView.EnableAppearanceEvenRow = True
    Me.grvMovim.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvMovim.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvMovim.OptionsView.ShowGroupPanel = False
    Me.grvMovim.RowHeight = 16
    '
    'km_aammgg
    '
    Me.km_aammgg.AppearanceCell.Options.UseBackColor = True
    Me.km_aammgg.AppearanceCell.Options.UseTextOptions = True
    Me.km_aammgg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_aammgg.Caption = "Data doc"
    Me.km_aammgg.Enabled = True
    Me.km_aammgg.FieldName = "km_aammgg"
    Me.km_aammgg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_aammgg.Name = "km_aammgg"
    Me.km_aammgg.NTSRepositoryComboBox = Nothing
    Me.km_aammgg.NTSRepositoryItemCheck = Nothing
    Me.km_aammgg.NTSRepositoryItemMemo = Nothing
    Me.km_aammgg.NTSRepositoryItemText = Nothing
    Me.km_aammgg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_aammgg.OptionsFilter.AllowFilter = False
    Me.km_aammgg.Visible = True
    Me.km_aammgg.VisibleIndex = 0
    Me.km_aammgg.Width = 70
    '
    'km_tipork
    '
    Me.km_tipork.AppearanceCell.Options.UseBackColor = True
    Me.km_tipork.AppearanceCell.Options.UseTextOptions = True
    Me.km_tipork.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_tipork.Caption = "Tipo doc"
    Me.km_tipork.Enabled = True
    Me.km_tipork.FieldName = "km_tipork"
    Me.km_tipork.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_tipork.Name = "km_tipork"
    Me.km_tipork.NTSRepositoryComboBox = Nothing
    Me.km_tipork.NTSRepositoryItemCheck = Nothing
    Me.km_tipork.NTSRepositoryItemMemo = Nothing
    Me.km_tipork.NTSRepositoryItemText = Nothing
    Me.km_tipork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_tipork.OptionsFilter.AllowFilter = False
    Me.km_tipork.Visible = True
    Me.km_tipork.VisibleIndex = 1
    Me.km_tipork.Width = 70
    '
    'km_serie
    '
    Me.km_serie.AppearanceCell.Options.UseBackColor = True
    Me.km_serie.AppearanceCell.Options.UseTextOptions = True
    Me.km_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_serie.Caption = "Serie doc"
    Me.km_serie.Enabled = True
    Me.km_serie.FieldName = "km_serie"
    Me.km_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_serie.Name = "km_serie"
    Me.km_serie.NTSRepositoryComboBox = Nothing
    Me.km_serie.NTSRepositoryItemCheck = Nothing
    Me.km_serie.NTSRepositoryItemMemo = Nothing
    Me.km_serie.NTSRepositoryItemText = Nothing
    Me.km_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_serie.OptionsFilter.AllowFilter = False
    Me.km_serie.Visible = True
    Me.km_serie.VisibleIndex = 2
    Me.km_serie.Width = 70
    '
    'km_numdoc
    '
    Me.km_numdoc.AppearanceCell.Options.UseBackColor = True
    Me.km_numdoc.AppearanceCell.Options.UseTextOptions = True
    Me.km_numdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_numdoc.Caption = "Num doc"
    Me.km_numdoc.Enabled = True
    Me.km_numdoc.FieldName = "km_numdoc"
    Me.km_numdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_numdoc.Name = "km_numdoc"
    Me.km_numdoc.NTSRepositoryComboBox = Nothing
    Me.km_numdoc.NTSRepositoryItemCheck = Nothing
    Me.km_numdoc.NTSRepositoryItemMemo = Nothing
    Me.km_numdoc.NTSRepositoryItemText = Nothing
    Me.km_numdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_numdoc.OptionsFilter.AllowFilter = False
    Me.km_numdoc.Visible = True
    Me.km_numdoc.VisibleIndex = 3
    Me.km_numdoc.Width = 70
    '
    'km_causale
    '
    Me.km_causale.AppearanceCell.Options.UseBackColor = True
    Me.km_causale.AppearanceCell.Options.UseTextOptions = True
    Me.km_causale.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_causale.Caption = "Causale"
    Me.km_causale.Enabled = True
    Me.km_causale.FieldName = "km_causale"
    Me.km_causale.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_causale.Name = "km_causale"
    Me.km_causale.NTSRepositoryComboBox = Nothing
    Me.km_causale.NTSRepositoryItemCheck = Nothing
    Me.km_causale.NTSRepositoryItemMemo = Nothing
    Me.km_causale.NTSRepositoryItemText = Nothing
    Me.km_causale.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_causale.OptionsFilter.AllowFilter = False
    Me.km_causale.Visible = True
    Me.km_causale.VisibleIndex = 4
    Me.km_causale.Width = 70
    '
    'tb_descaum
    '
    Me.tb_descaum.AppearanceCell.Options.UseBackColor = True
    Me.tb_descaum.AppearanceCell.Options.UseTextOptions = True
    Me.tb_descaum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_descaum.Caption = "Descr. causale"
    Me.tb_descaum.Enabled = True
    Me.tb_descaum.FieldName = "tb_descaum"
    Me.tb_descaum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_descaum.Name = "tb_descaum"
    Me.tb_descaum.NTSRepositoryComboBox = Nothing
    Me.tb_descaum.NTSRepositoryItemCheck = Nothing
    Me.tb_descaum.NTSRepositoryItemMemo = Nothing
    Me.tb_descaum.NTSRepositoryItemText = Nothing
    Me.tb_descaum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_descaum.OptionsFilter.AllowFilter = False
    Me.tb_descaum.Visible = True
    Me.tb_descaum.VisibleIndex = 5
    Me.tb_descaum.Width = 70
    '
    'tm_riferim
    '
    Me.tm_riferim.AppearanceCell.Options.UseBackColor = True
    Me.tm_riferim.AppearanceCell.Options.UseTextOptions = True
    Me.tm_riferim.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_riferim.Caption = "Riferimenti"
    Me.tm_riferim.Enabled = True
    Me.tm_riferim.FieldName = "tm_riferim"
    Me.tm_riferim.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_riferim.Name = "tm_riferim"
    Me.tm_riferim.NTSRepositoryComboBox = Nothing
    Me.tm_riferim.NTSRepositoryItemCheck = Nothing
    Me.tm_riferim.NTSRepositoryItemMemo = Nothing
    Me.tm_riferim.NTSRepositoryItemText = Nothing
    Me.tm_riferim.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_riferim.OptionsFilter.AllowFilter = False
    Me.tm_riferim.Visible = True
    Me.tm_riferim.VisibleIndex = 6
    Me.tm_riferim.Width = 70
    '
    'xx_scarichi
    '
    Me.xx_scarichi.AppearanceCell.Options.UseBackColor = True
    Me.xx_scarichi.AppearanceCell.Options.UseTextOptions = True
    Me.xx_scarichi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_scarichi.Caption = "Scarichi"
    Me.xx_scarichi.Enabled = True
    Me.xx_scarichi.FieldName = "xx_scarichi"
    Me.xx_scarichi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_scarichi.Name = "xx_scarichi"
    Me.xx_scarichi.NTSRepositoryComboBox = Nothing
    Me.xx_scarichi.NTSRepositoryItemCheck = Nothing
    Me.xx_scarichi.NTSRepositoryItemMemo = Nothing
    Me.xx_scarichi.NTSRepositoryItemText = Nothing
    Me.xx_scarichi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_scarichi.OptionsFilter.AllowFilter = False
    Me.xx_scarichi.Visible = True
    Me.xx_scarichi.VisibleIndex = 7
    Me.xx_scarichi.Width = 70
    '
    'xx_carichi
    '
    Me.xx_carichi.AppearanceCell.Options.UseBackColor = True
    Me.xx_carichi.AppearanceCell.Options.UseTextOptions = True
    Me.xx_carichi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_carichi.Caption = "Carichi"
    Me.xx_carichi.Enabled = True
    Me.xx_carichi.FieldName = "xx_carichi"
    Me.xx_carichi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_carichi.Name = "xx_carichi"
    Me.xx_carichi.NTSRepositoryComboBox = Nothing
    Me.xx_carichi.NTSRepositoryItemCheck = Nothing
    Me.xx_carichi.NTSRepositoryItemMemo = Nothing
    Me.xx_carichi.NTSRepositoryItemText = Nothing
    Me.xx_carichi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_carichi.OptionsFilter.AllowFilter = False
    Me.xx_carichi.Visible = True
    Me.xx_carichi.VisibleIndex = 8
    Me.xx_carichi.Width = 70
    '
    'xx_prezzo
    '
    Me.xx_prezzo.AppearanceCell.Options.UseBackColor = True
    Me.xx_prezzo.AppearanceCell.Options.UseTextOptions = True
    Me.xx_prezzo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_prezzo.Caption = "Prezzo netto"
    Me.xx_prezzo.Enabled = True
    Me.xx_prezzo.FieldName = "xx_prezzo"
    Me.xx_prezzo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_prezzo.Name = "xx_prezzo"
    Me.xx_prezzo.NTSRepositoryComboBox = Nothing
    Me.xx_prezzo.NTSRepositoryItemCheck = Nothing
    Me.xx_prezzo.NTSRepositoryItemMemo = Nothing
    Me.xx_prezzo.NTSRepositoryItemText = Nothing
    Me.xx_prezzo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_prezzo.OptionsFilter.AllowFilter = False
    Me.xx_prezzo.Visible = True
    Me.xx_prezzo.VisibleIndex = 9
    Me.xx_prezzo.Width = 70
    '
    'mm_valore
    '
    Me.mm_valore.AppearanceCell.Options.UseBackColor = True
    Me.mm_valore.AppearanceCell.Options.UseTextOptions = True
    Me.mm_valore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_valore.Caption = "Valore"
    Me.mm_valore.Enabled = True
    Me.mm_valore.FieldName = "mm_valore"
    Me.mm_valore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_valore.Name = "mm_valore"
    Me.mm_valore.NTSRepositoryComboBox = Nothing
    Me.mm_valore.NTSRepositoryItemCheck = Nothing
    Me.mm_valore.NTSRepositoryItemMemo = Nothing
    Me.mm_valore.NTSRepositoryItemText = Nothing
    Me.mm_valore.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_valore.OptionsFilter.AllowFilter = False
    Me.mm_valore.Visible = True
    Me.mm_valore.VisibleIndex = 10
    Me.mm_valore.Width = 70
    '
    'mm_quant
    '
    Me.mm_quant.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant.Caption = "Quant."
    Me.mm_quant.Enabled = True
    Me.mm_quant.FieldName = "mm_quant"
    Me.mm_quant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quant.Name = "mm_quant"
    Me.mm_quant.NTSRepositoryComboBox = Nothing
    Me.mm_quant.NTSRepositoryItemCheck = Nothing
    Me.mm_quant.NTSRepositoryItemMemo = Nothing
    Me.mm_quant.NTSRepositoryItemText = Nothing
    Me.mm_quant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quant.OptionsFilter.AllowFilter = False
    Me.mm_quant.Visible = True
    Me.mm_quant.VisibleIndex = 11
    Me.mm_quant.Width = 70
    '
    'mm_prelist
    '
    Me.mm_prelist.AppearanceCell.Options.UseBackColor = True
    Me.mm_prelist.AppearanceCell.Options.UseTextOptions = True
    Me.mm_prelist.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_prelist.Caption = "Prezzo list."
    Me.mm_prelist.Enabled = True
    Me.mm_prelist.FieldName = "mm_prelist"
    Me.mm_prelist.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_prelist.Name = "mm_prelist"
    Me.mm_prelist.NTSRepositoryComboBox = Nothing
    Me.mm_prelist.NTSRepositoryItemCheck = Nothing
    Me.mm_prelist.NTSRepositoryItemMemo = Nothing
    Me.mm_prelist.NTSRepositoryItemText = Nothing
    Me.mm_prelist.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_prelist.OptionsFilter.AllowFilter = False
    Me.mm_prelist.Visible = True
    Me.mm_prelist.VisibleIndex = 12
    Me.mm_prelist.Width = 70
    '
    'mm_prezzo
    '
    Me.mm_prezzo.AppearanceCell.Options.UseBackColor = True
    Me.mm_prezzo.AppearanceCell.Options.UseTextOptions = True
    Me.mm_prezzo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_prezzo.Caption = "Prezzo"
    Me.mm_prezzo.Enabled = True
    Me.mm_prezzo.FieldName = "mm_prezzo"
    Me.mm_prezzo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_prezzo.Name = "mm_prezzo"
    Me.mm_prezzo.NTSRepositoryComboBox = Nothing
    Me.mm_prezzo.NTSRepositoryItemCheck = Nothing
    Me.mm_prezzo.NTSRepositoryItemMemo = Nothing
    Me.mm_prezzo.NTSRepositoryItemText = Nothing
    Me.mm_prezzo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_prezzo.OptionsFilter.AllowFilter = False
    Me.mm_prezzo.Visible = True
    Me.mm_prezzo.VisibleIndex = 13
    Me.mm_prezzo.Width = 70
    '
    'mm_preziva
    '
    Me.mm_preziva.AppearanceCell.Options.UseBackColor = True
    Me.mm_preziva.AppearanceCell.Options.UseTextOptions = True
    Me.mm_preziva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_preziva.Caption = "Prezzo IVA comp."
    Me.mm_preziva.Enabled = True
    Me.mm_preziva.FieldName = "mm_preziva"
    Me.mm_preziva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_preziva.Name = "mm_preziva"
    Me.mm_preziva.NTSRepositoryComboBox = Nothing
    Me.mm_preziva.NTSRepositoryItemCheck = Nothing
    Me.mm_preziva.NTSRepositoryItemMemo = Nothing
    Me.mm_preziva.NTSRepositoryItemText = Nothing
    Me.mm_preziva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_preziva.OptionsFilter.AllowFilter = False
    Me.mm_preziva.Visible = True
    Me.mm_preziva.VisibleIndex = 14
    Me.mm_preziva.Width = 70
    '
    'mm_prezvalc
    '
    Me.mm_prezvalc.AppearanceCell.Options.UseBackColor = True
    Me.mm_prezvalc.AppearanceCell.Options.UseTextOptions = True
    Me.mm_prezvalc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_prezvalc.Caption = "Prezzo val."
    Me.mm_prezvalc.Enabled = True
    Me.mm_prezvalc.FieldName = "mm_prezvalc"
    Me.mm_prezvalc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_prezvalc.Name = "mm_prezvalc"
    Me.mm_prezvalc.NTSRepositoryComboBox = Nothing
    Me.mm_prezvalc.NTSRepositoryItemCheck = Nothing
    Me.mm_prezvalc.NTSRepositoryItemMemo = Nothing
    Me.mm_prezvalc.NTSRepositoryItemText = Nothing
    Me.mm_prezvalc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_prezvalc.OptionsFilter.AllowFilter = False
    Me.mm_prezvalc.Visible = True
    Me.mm_prezvalc.VisibleIndex = 15
    Me.mm_prezvalc.Width = 70
    '
    'mm_scont1
    '
    Me.mm_scont1.AppearanceCell.Options.UseBackColor = True
    Me.mm_scont1.AppearanceCell.Options.UseTextOptions = True
    Me.mm_scont1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_scont1.Caption = "Sconto 1"
    Me.mm_scont1.Enabled = True
    Me.mm_scont1.FieldName = "mm_scont1"
    Me.mm_scont1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_scont1.Name = "mm_scont1"
    Me.mm_scont1.NTSRepositoryComboBox = Nothing
    Me.mm_scont1.NTSRepositoryItemCheck = Nothing
    Me.mm_scont1.NTSRepositoryItemMemo = Nothing
    Me.mm_scont1.NTSRepositoryItemText = Nothing
    Me.mm_scont1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_scont1.OptionsFilter.AllowFilter = False
    Me.mm_scont1.Visible = True
    Me.mm_scont1.VisibleIndex = 16
    Me.mm_scont1.Width = 70
    '
    'mm_scont2
    '
    Me.mm_scont2.AppearanceCell.Options.UseBackColor = True
    Me.mm_scont2.AppearanceCell.Options.UseTextOptions = True
    Me.mm_scont2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_scont2.Caption = "Sconto 2"
    Me.mm_scont2.Enabled = True
    Me.mm_scont2.FieldName = "mm_scont2"
    Me.mm_scont2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_scont2.Name = "mm_scont2"
    Me.mm_scont2.NTSRepositoryComboBox = Nothing
    Me.mm_scont2.NTSRepositoryItemCheck = Nothing
    Me.mm_scont2.NTSRepositoryItemMemo = Nothing
    Me.mm_scont2.NTSRepositoryItemText = Nothing
    Me.mm_scont2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_scont2.OptionsFilter.AllowFilter = False
    Me.mm_scont2.Visible = True
    Me.mm_scont2.VisibleIndex = 17
    Me.mm_scont2.Width = 70
    '
    'xx_lottox
    '
    Me.xx_lottox.AppearanceCell.Options.UseBackColor = True
    Me.xx_lottox.AppearanceCell.Options.UseTextOptions = True
    Me.xx_lottox.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_lottox.Caption = "Lotto"
    Me.xx_lottox.Enabled = True
    Me.xx_lottox.FieldName = "xx_lottox"
    Me.xx_lottox.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_lottox.Name = "xx_lottox"
    Me.xx_lottox.NTSRepositoryComboBox = Nothing
    Me.xx_lottox.NTSRepositoryItemCheck = Nothing
    Me.xx_lottox.NTSRepositoryItemMemo = Nothing
    Me.xx_lottox.NTSRepositoryItemText = Nothing
    Me.xx_lottox.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_lottox.OptionsFilter.AllowFilter = False
    Me.xx_lottox.Visible = True
    Me.xx_lottox.VisibleIndex = 18
    Me.xx_lottox.Width = 70
    '
    'km_magaz
    '
    Me.km_magaz.AppearanceCell.Options.UseBackColor = True
    Me.km_magaz.AppearanceCell.Options.UseTextOptions = True
    Me.km_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_magaz.Caption = "Magaz"
    Me.km_magaz.Enabled = True
    Me.km_magaz.FieldName = "km_magaz"
    Me.km_magaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_magaz.Name = "km_magaz"
    Me.km_magaz.NTSRepositoryComboBox = Nothing
    Me.km_magaz.NTSRepositoryItemCheck = Nothing
    Me.km_magaz.NTSRepositoryItemMemo = Nothing
    Me.km_magaz.NTSRepositoryItemText = Nothing
    Me.km_magaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_magaz.OptionsFilter.AllowFilter = False
    Me.km_magaz.Visible = True
    Me.km_magaz.VisibleIndex = 19
    Me.km_magaz.Width = 70
    '
    'km_subcommeca
    '
    Me.km_subcommeca.AppearanceCell.Options.UseBackColor = True
    Me.km_subcommeca.AppearanceCell.Options.UseTextOptions = True
    Me.km_subcommeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_subcommeca.Caption = "SubCom"
    Me.km_subcommeca.Enabled = True
    Me.km_subcommeca.FieldName = "km_subcommeca"
    Me.km_subcommeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_subcommeca.Name = "km_subcommeca"
    Me.km_subcommeca.NTSRepositoryComboBox = Nothing
    Me.km_subcommeca.NTSRepositoryItemCheck = Nothing
    Me.km_subcommeca.NTSRepositoryItemMemo = Nothing
    Me.km_subcommeca.NTSRepositoryItemText = Nothing
    Me.km_subcommeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_subcommeca.OptionsFilter.AllowFilter = False
    Me.km_subcommeca.Visible = True
    Me.km_subcommeca.VisibleIndex = 20
    Me.km_subcommeca.Width = 70
    '
    'km_ubicaz
    '
    Me.km_ubicaz.AppearanceCell.Options.UseBackColor = True
    Me.km_ubicaz.AppearanceCell.Options.UseTextOptions = True
    Me.km_ubicaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_ubicaz.Caption = "Ubicaz."
    Me.km_ubicaz.Enabled = True
    Me.km_ubicaz.FieldName = "km_ubicaz"
    Me.km_ubicaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_ubicaz.Name = "km_ubicaz"
    Me.km_ubicaz.NTSRepositoryComboBox = Nothing
    Me.km_ubicaz.NTSRepositoryItemCheck = Nothing
    Me.km_ubicaz.NTSRepositoryItemMemo = Nothing
    Me.km_ubicaz.NTSRepositoryItemText = Nothing
    Me.km_ubicaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_ubicaz.OptionsFilter.AllowFilter = False
    Me.km_ubicaz.Visible = True
    Me.km_ubicaz.VisibleIndex = 21
    Me.km_ubicaz.Width = 70
    '
    'mm_codcena
    '
    Me.mm_codcena.AppearanceCell.Options.UseBackColor = True
    Me.mm_codcena.AppearanceCell.Options.UseTextOptions = True
    Me.mm_codcena.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_codcena.Caption = "Centro"
    Me.mm_codcena.Enabled = True
    Me.mm_codcena.FieldName = "mm_codcena"
    Me.mm_codcena.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_codcena.Name = "mm_codcena"
    Me.mm_codcena.NTSRepositoryComboBox = Nothing
    Me.mm_codcena.NTSRepositoryItemCheck = Nothing
    Me.mm_codcena.NTSRepositoryItemMemo = Nothing
    Me.mm_codcena.NTSRepositoryItemText = Nothing
    Me.mm_codcena.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_codcena.OptionsFilter.AllowFilter = False
    Me.mm_codcena.Visible = True
    Me.mm_codcena.VisibleIndex = 22
    Me.mm_codcena.Width = 70
    '
    'mm_codcfam
    '
    Me.mm_codcfam.AppearanceCell.Options.UseBackColor = True
    Me.mm_codcfam.AppearanceCell.Options.UseTextOptions = True
    Me.mm_codcfam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_codcfam.Caption = "Famiglia"
    Me.mm_codcfam.Enabled = True
    Me.mm_codcfam.FieldName = "mm_codcfam"
    Me.mm_codcfam.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_codcfam.Name = "mm_codcfam"
    Me.mm_codcfam.NTSRepositoryComboBox = Nothing
    Me.mm_codcfam.NTSRepositoryItemCheck = Nothing
    Me.mm_codcfam.NTSRepositoryItemMemo = Nothing
    Me.mm_codcfam.NTSRepositoryItemText = Nothing
    Me.mm_codcfam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_codcfam.OptionsFilter.AllowFilter = False
    Me.mm_codcfam.Visible = True
    Me.mm_codcfam.VisibleIndex = 23
    Me.mm_codcfam.Width = 70
    '
    'mm_codiva
    '
    Me.mm_codiva.AppearanceCell.Options.UseBackColor = True
    Me.mm_codiva.AppearanceCell.Options.UseTextOptions = True
    Me.mm_codiva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_codiva.Caption = "Cod. IVA"
    Me.mm_codiva.Enabled = True
    Me.mm_codiva.FieldName = "mm_codiva"
    Me.mm_codiva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_codiva.Name = "mm_codiva"
    Me.mm_codiva.NTSRepositoryComboBox = Nothing
    Me.mm_codiva.NTSRepositoryItemCheck = Nothing
    Me.mm_codiva.NTSRepositoryItemMemo = Nothing
    Me.mm_codiva.NTSRepositoryItemText = Nothing
    Me.mm_codiva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_codiva.OptionsFilter.AllowFilter = False
    Me.mm_codiva.Visible = True
    Me.mm_codiva.VisibleIndex = 24
    Me.mm_codiva.Width = 70
    '
    'mm_codnomc
    '
    Me.mm_codnomc.AppearanceCell.Options.UseBackColor = True
    Me.mm_codnomc.AppearanceCell.Options.UseTextOptions = True
    Me.mm_codnomc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_codnomc.Caption = "Cod. nom. comb."
    Me.mm_codnomc.Enabled = True
    Me.mm_codnomc.FieldName = "mm_codnomc"
    Me.mm_codnomc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_codnomc.Name = "mm_codnomc"
    Me.mm_codnomc.NTSRepositoryComboBox = Nothing
    Me.mm_codnomc.NTSRepositoryItemCheck = Nothing
    Me.mm_codnomc.NTSRepositoryItemMemo = Nothing
    Me.mm_codnomc.NTSRepositoryItemText = Nothing
    Me.mm_codnomc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_codnomc.OptionsFilter.AllowFilter = False
    Me.mm_codnomc.Visible = True
    Me.mm_codnomc.VisibleIndex = 25
    Me.mm_codnomc.Width = 70
    '
    'mm_colli
    '
    Me.mm_colli.AppearanceCell.Options.UseBackColor = True
    Me.mm_colli.AppearanceCell.Options.UseTextOptions = True
    Me.mm_colli.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_colli.Caption = "Colli"
    Me.mm_colli.Enabled = True
    Me.mm_colli.FieldName = "mm_colli"
    Me.mm_colli.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_colli.Name = "mm_colli"
    Me.mm_colli.NTSRepositoryComboBox = Nothing
    Me.mm_colli.NTSRepositoryItemCheck = Nothing
    Me.mm_colli.NTSRepositoryItemMemo = Nothing
    Me.mm_colli.NTSRepositoryItemText = Nothing
    Me.mm_colli.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_colli.OptionsFilter.AllowFilter = False
    Me.mm_colli.Visible = True
    Me.mm_colli.VisibleIndex = 26
    Me.mm_colli.Width = 70
    '
    'mm_commeca
    '
    Me.mm_commeca.AppearanceCell.Options.UseBackColor = True
    Me.mm_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.mm_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_commeca.Caption = "Commessa"
    Me.mm_commeca.Enabled = True
    Me.mm_commeca.FieldName = "mm_commeca"
    Me.mm_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_commeca.Name = "mm_commeca"
    Me.mm_commeca.NTSRepositoryComboBox = Nothing
    Me.mm_commeca.NTSRepositoryItemCheck = Nothing
    Me.mm_commeca.NTSRepositoryItemMemo = Nothing
    Me.mm_commeca.NTSRepositoryItemText = Nothing
    Me.mm_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_commeca.OptionsFilter.AllowFilter = False
    Me.mm_commeca.Visible = True
    Me.mm_commeca.VisibleIndex = 27
    Me.mm_commeca.Width = 70
    '
    'mm_controp
    '
    Me.mm_controp.AppearanceCell.Options.UseBackColor = True
    Me.mm_controp.AppearanceCell.Options.UseTextOptions = True
    Me.mm_controp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_controp.Caption = "Controp."
    Me.mm_controp.Enabled = True
    Me.mm_controp.FieldName = "mm_controp"
    Me.mm_controp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_controp.Name = "mm_controp"
    Me.mm_controp.NTSRepositoryComboBox = Nothing
    Me.mm_controp.NTSRepositoryItemCheck = Nothing
    Me.mm_controp.NTSRepositoryItemMemo = Nothing
    Me.mm_controp.NTSRepositoryItemText = Nothing
    Me.mm_controp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_controp.OptionsFilter.AllowFilter = False
    Me.mm_controp.Visible = True
    Me.mm_controp.VisibleIndex = 28
    Me.mm_controp.Width = 70
    '
    'mm_misura1
    '
    Me.mm_misura1.AppearanceCell.Options.UseBackColor = True
    Me.mm_misura1.AppearanceCell.Options.UseTextOptions = True
    Me.mm_misura1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_misura1.Caption = "Misura 1"
    Me.mm_misura1.Enabled = True
    Me.mm_misura1.FieldName = "mm_misura1"
    Me.mm_misura1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_misura1.Name = "mm_misura1"
    Me.mm_misura1.NTSRepositoryComboBox = Nothing
    Me.mm_misura1.NTSRepositoryItemCheck = Nothing
    Me.mm_misura1.NTSRepositoryItemMemo = Nothing
    Me.mm_misura1.NTSRepositoryItemText = Nothing
    Me.mm_misura1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_misura1.OptionsFilter.AllowFilter = False
    Me.mm_misura1.Visible = True
    Me.mm_misura1.VisibleIndex = 29
    Me.mm_misura1.Width = 70
    '
    'mm_misura2
    '
    Me.mm_misura2.AppearanceCell.Options.UseBackColor = True
    Me.mm_misura2.AppearanceCell.Options.UseTextOptions = True
    Me.mm_misura2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_misura2.Caption = "Misura 2"
    Me.mm_misura2.Enabled = True
    Me.mm_misura2.FieldName = "mm_misura2"
    Me.mm_misura2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_misura2.Name = "mm_misura2"
    Me.mm_misura2.NTSRepositoryComboBox = Nothing
    Me.mm_misura2.NTSRepositoryItemCheck = Nothing
    Me.mm_misura2.NTSRepositoryItemMemo = Nothing
    Me.mm_misura2.NTSRepositoryItemText = Nothing
    Me.mm_misura2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_misura2.OptionsFilter.AllowFilter = False
    Me.mm_misura2.Visible = True
    Me.mm_misura2.VisibleIndex = 30
    Me.mm_misura2.Width = 70
    '
    'mm_misura3
    '
    Me.mm_misura3.AppearanceCell.Options.UseBackColor = True
    Me.mm_misura3.AppearanceCell.Options.UseTextOptions = True
    Me.mm_misura3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_misura3.Caption = "Misura 3"
    Me.mm_misura3.Enabled = True
    Me.mm_misura3.FieldName = "mm_misura3"
    Me.mm_misura3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_misura3.Name = "mm_misura3"
    Me.mm_misura3.NTSRepositoryComboBox = Nothing
    Me.mm_misura3.NTSRepositoryItemCheck = Nothing
    Me.mm_misura3.NTSRepositoryItemMemo = Nothing
    Me.mm_misura3.NTSRepositoryItemText = Nothing
    Me.mm_misura3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_misura3.OptionsFilter.AllowFilter = False
    Me.mm_misura3.Visible = True
    Me.mm_misura3.VisibleIndex = 31
    Me.mm_misura3.Width = 70
    '
    'mm_ornum
    '
    Me.mm_ornum.AppearanceCell.Options.UseBackColor = True
    Me.mm_ornum.AppearanceCell.Options.UseTextOptions = True
    Me.mm_ornum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_ornum.Caption = "Num. ord"
    Me.mm_ornum.Enabled = True
    Me.mm_ornum.FieldName = "mm_ornum"
    Me.mm_ornum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_ornum.Name = "mm_ornum"
    Me.mm_ornum.NTSRepositoryComboBox = Nothing
    Me.mm_ornum.NTSRepositoryItemCheck = Nothing
    Me.mm_ornum.NTSRepositoryItemMemo = Nothing
    Me.mm_ornum.NTSRepositoryItemText = Nothing
    Me.mm_ornum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_ornum.OptionsFilter.AllowFilter = False
    Me.mm_ornum.Visible = True
    Me.mm_ornum.VisibleIndex = 32
    Me.mm_ornum.Width = 70
    '
    'mm_perqta
    '
    Me.mm_perqta.AppearanceCell.Options.UseBackColor = True
    Me.mm_perqta.AppearanceCell.Options.UseTextOptions = True
    Me.mm_perqta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_perqta.Caption = "Perqta"
    Me.mm_perqta.Enabled = True
    Me.mm_perqta.FieldName = "mm_perqta"
    Me.mm_perqta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_perqta.Name = "mm_perqta"
    Me.mm_perqta.NTSRepositoryComboBox = Nothing
    Me.mm_perqta.NTSRepositoryItemCheck = Nothing
    Me.mm_perqta.NTSRepositoryItemMemo = Nothing
    Me.mm_perqta.NTSRepositoryItemText = Nothing
    Me.mm_perqta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_perqta.OptionsFilter.AllowFilter = False
    Me.mm_perqta.Visible = True
    Me.mm_perqta.VisibleIndex = 33
    Me.mm_perqta.Width = 70
    '
    'mm_provv
    '
    Me.mm_provv.AppearanceCell.Options.UseBackColor = True
    Me.mm_provv.AppearanceCell.Options.UseTextOptions = True
    Me.mm_provv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_provv.Caption = "Provv."
    Me.mm_provv.Enabled = True
    Me.mm_provv.FieldName = "mm_provv"
    Me.mm_provv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_provv.Name = "mm_provv"
    Me.mm_provv.NTSRepositoryComboBox = Nothing
    Me.mm_provv.NTSRepositoryItemCheck = Nothing
    Me.mm_provv.NTSRepositoryItemMemo = Nothing
    Me.mm_provv.NTSRepositoryItemText = Nothing
    Me.mm_provv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_provv.OptionsFilter.AllowFilter = False
    Me.mm_provv.Visible = True
    Me.mm_provv.VisibleIndex = 34
    Me.mm_provv.Width = 70
    '
    'mm_provv2
    '
    Me.mm_provv2.AppearanceCell.Options.UseBackColor = True
    Me.mm_provv2.AppearanceCell.Options.UseTextOptions = True
    Me.mm_provv2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_provv2.Caption = "Provv. 2"
    Me.mm_provv2.Enabled = True
    Me.mm_provv2.FieldName = "mm_provv2"
    Me.mm_provv2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_provv2.Name = "mm_provv2"
    Me.mm_provv2.NTSRepositoryComboBox = Nothing
    Me.mm_provv2.NTSRepositoryItemCheck = Nothing
    Me.mm_provv2.NTSRepositoryItemMemo = Nothing
    Me.mm_provv2.NTSRepositoryItemText = Nothing
    Me.mm_provv2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_provv2.OptionsFilter.AllowFilter = False
    Me.mm_provv2.Visible = True
    Me.mm_provv2.VisibleIndex = 35
    Me.mm_provv2.Width = 70
    '
    'mm_scont3
    '
    Me.mm_scont3.AppearanceCell.Options.UseBackColor = True
    Me.mm_scont3.AppearanceCell.Options.UseTextOptions = True
    Me.mm_scont3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_scont3.Caption = "Sconto 3"
    Me.mm_scont3.Enabled = True
    Me.mm_scont3.FieldName = "mm_scont3"
    Me.mm_scont3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_scont3.Name = "mm_scont3"
    Me.mm_scont3.NTSRepositoryComboBox = Nothing
    Me.mm_scont3.NTSRepositoryItemCheck = Nothing
    Me.mm_scont3.NTSRepositoryItemMemo = Nothing
    Me.mm_scont3.NTSRepositoryItemText = Nothing
    Me.mm_scont3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_scont3.OptionsFilter.AllowFilter = False
    Me.mm_scont3.Visible = True
    Me.mm_scont3.VisibleIndex = 36
    Me.mm_scont3.Width = 70
    '
    'mm_scont4
    '
    Me.mm_scont4.AppearanceCell.Options.UseBackColor = True
    Me.mm_scont4.AppearanceCell.Options.UseTextOptions = True
    Me.mm_scont4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_scont4.Caption = "Sconto 4"
    Me.mm_scont4.Enabled = True
    Me.mm_scont4.FieldName = "mm_scont4"
    Me.mm_scont4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_scont4.Name = "mm_scont4"
    Me.mm_scont4.NTSRepositoryComboBox = Nothing
    Me.mm_scont4.NTSRepositoryItemCheck = Nothing
    Me.mm_scont4.NTSRepositoryItemMemo = Nothing
    Me.mm_scont4.NTSRepositoryItemText = Nothing
    Me.mm_scont4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_scont4.OptionsFilter.AllowFilter = False
    Me.mm_scont4.Visible = True
    Me.mm_scont4.VisibleIndex = 37
    Me.mm_scont4.Width = 70
    '
    'mm_scont5
    '
    Me.mm_scont5.AppearanceCell.Options.UseBackColor = True
    Me.mm_scont5.AppearanceCell.Options.UseTextOptions = True
    Me.mm_scont5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_scont5.Caption = "Sconto 5"
    Me.mm_scont5.Enabled = True
    Me.mm_scont5.FieldName = "mm_scont5"
    Me.mm_scont5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_scont5.Name = "mm_scont5"
    Me.mm_scont5.NTSRepositoryComboBox = Nothing
    Me.mm_scont5.NTSRepositoryItemCheck = Nothing
    Me.mm_scont5.NTSRepositoryItemMemo = Nothing
    Me.mm_scont5.NTSRepositoryItemText = Nothing
    Me.mm_scont5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_scont5.OptionsFilter.AllowFilter = False
    Me.mm_scont5.Visible = True
    Me.mm_scont5.VisibleIndex = 38
    Me.mm_scont5.Width = 70
    '
    'mm_scont6
    '
    Me.mm_scont6.AppearanceCell.Options.UseBackColor = True
    Me.mm_scont6.AppearanceCell.Options.UseTextOptions = True
    Me.mm_scont6.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_scont6.Caption = "Sconto 6"
    Me.mm_scont6.Enabled = True
    Me.mm_scont6.FieldName = "mm_scont6"
    Me.mm_scont6.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_scont6.Name = "mm_scont6"
    Me.mm_scont6.NTSRepositoryComboBox = Nothing
    Me.mm_scont6.NTSRepositoryItemCheck = Nothing
    Me.mm_scont6.NTSRepositoryItemMemo = Nothing
    Me.mm_scont6.NTSRepositoryItemText = Nothing
    Me.mm_scont6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_scont6.OptionsFilter.AllowFilter = False
    Me.mm_scont6.Visible = True
    Me.mm_scont6.VisibleIndex = 39
    Me.mm_scont6.Width = 70
    '
    'mm_vprovv
    '
    Me.mm_vprovv.AppearanceCell.Options.UseBackColor = True
    Me.mm_vprovv.AppearanceCell.Options.UseTextOptions = True
    Me.mm_vprovv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_vprovv.Caption = "Valore Provv"
    Me.mm_vprovv.Enabled = True
    Me.mm_vprovv.FieldName = "mm_vprovv"
    Me.mm_vprovv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_vprovv.Name = "mm_vprovv"
    Me.mm_vprovv.NTSRepositoryComboBox = Nothing
    Me.mm_vprovv.NTSRepositoryItemCheck = Nothing
    Me.mm_vprovv.NTSRepositoryItemMemo = Nothing
    Me.mm_vprovv.NTSRepositoryItemText = Nothing
    Me.mm_vprovv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_vprovv.OptionsFilter.AllowFilter = False
    Me.mm_vprovv.Visible = True
    Me.mm_vprovv.VisibleIndex = 40
    Me.mm_vprovv.Width = 70
    '
    'mm_vprovv2
    '
    Me.mm_vprovv2.AppearanceCell.Options.UseBackColor = True
    Me.mm_vprovv2.AppearanceCell.Options.UseTextOptions = True
    Me.mm_vprovv2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_vprovv2.Caption = "Valore Provv 2"
    Me.mm_vprovv2.Enabled = True
    Me.mm_vprovv2.FieldName = "mm_vprovv2"
    Me.mm_vprovv2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_vprovv2.Name = "mm_vprovv2"
    Me.mm_vprovv2.NTSRepositoryComboBox = Nothing
    Me.mm_vprovv2.NTSRepositoryItemCheck = Nothing
    Me.mm_vprovv2.NTSRepositoryItemMemo = Nothing
    Me.mm_vprovv2.NTSRepositoryItemText = Nothing
    Me.mm_vprovv2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_vprovv2.OptionsFilter.AllowFilter = False
    Me.mm_vprovv2.Visible = True
    Me.mm_vprovv2.VisibleIndex = 41
    Me.mm_vprovv2.Width = 70
    '
    'tm_valuta
    '
    Me.tm_valuta.AppearanceCell.Options.UseBackColor = True
    Me.tm_valuta.AppearanceCell.Options.UseTextOptions = True
    Me.tm_valuta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_valuta.Caption = "Valuta"
    Me.tm_valuta.Enabled = True
    Me.tm_valuta.FieldName = "tm_valuta"
    Me.tm_valuta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_valuta.Name = "tm_valuta"
    Me.tm_valuta.NTSRepositoryComboBox = Nothing
    Me.tm_valuta.NTSRepositoryItemCheck = Nothing
    Me.tm_valuta.NTSRepositoryItemMemo = Nothing
    Me.tm_valuta.NTSRepositoryItemText = Nothing
    Me.tm_valuta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_valuta.OptionsFilter.AllowFilter = False
    Me.tm_valuta.Visible = True
    Me.tm_valuta.VisibleIndex = 42
    Me.tm_valuta.Width = 70
    '
    'km_conto
    '
    Me.km_conto.AppearanceCell.Options.UseBackColor = True
    Me.km_conto.AppearanceCell.Options.UseTextOptions = True
    Me.km_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_conto.Caption = "Conto"
    Me.km_conto.Enabled = True
    Me.km_conto.FieldName = "km_conto"
    Me.km_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_conto.Name = "km_conto"
    Me.km_conto.NTSRepositoryComboBox = Nothing
    Me.km_conto.NTSRepositoryItemCheck = Nothing
    Me.km_conto.NTSRepositoryItemMemo = Nothing
    Me.km_conto.NTSRepositoryItemText = Nothing
    Me.km_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_conto.OptionsFilter.AllowFilter = False
    Me.km_conto.Visible = True
    Me.km_conto.VisibleIndex = 43
    '
    'xx_conto
    '
    Me.xx_conto.AppearanceCell.Options.UseBackColor = True
    Me.xx_conto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_conto.Caption = "Descr. conto"
    Me.xx_conto.Enabled = True
    Me.xx_conto.FieldName = "xx_conto"
    Me.xx_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_conto.Name = "xx_conto"
    Me.xx_conto.NTSRepositoryComboBox = Nothing
    Me.xx_conto.NTSRepositoryItemCheck = Nothing
    Me.xx_conto.NTSRepositoryItemMemo = Nothing
    Me.xx_conto.NTSRepositoryItemText = Nothing
    Me.xx_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_conto.OptionsFilter.AllowFilter = False
    Me.xx_conto.Visible = True
    Me.xx_conto.VisibleIndex = 44
    '
    'pnBottom
    '
    Me.pnBottom.AllowDrop = True
    Me.pnBottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBottom.Appearance.Options.UseBackColor = True
    Me.pnBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBottom.Controls.Add(Me.edTotCarichi)
    Me.pnBottom.Controls.Add(Me.edTotScarichi)
    Me.pnBottom.Controls.Add(Me.lbTotCarichi)
    Me.pnBottom.Controls.Add(Me.lbTotscarichi)
    Me.pnBottom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnBottom.Location = New System.Drawing.Point(0, 398)
    Me.pnBottom.Name = "pnBottom"
    Me.pnBottom.Size = New System.Drawing.Size(799, 44)
    Me.pnBottom.TabIndex = 8
    Me.pnBottom.Text = "NtsPanel1"
    '
    'edTotCarichi
    '
    Me.edTotCarichi.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTotCarichi.EditValue = "0"
    Me.edTotCarichi.Enabled = False
    Me.edTotCarichi.Location = New System.Drawing.Point(339, 16)
    Me.edTotCarichi.Name = "edTotCarichi"
    Me.edTotCarichi.NTSDbField = ""
    Me.edTotCarichi.NTSFormat = "0"
    Me.edTotCarichi.NTSForzaVisZoom = False
    Me.edTotCarichi.NTSOldValue = ""
    Me.edTotCarichi.Properties.Appearance.Options.UseTextOptions = True
    Me.edTotCarichi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTotCarichi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTotCarichi.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTotCarichi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTotCarichi.Properties.MaxLength = 65536
    Me.edTotCarichi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTotCarichi.Size = New System.Drawing.Size(155, 20)
    Me.edTotCarichi.TabIndex = 7
    '
    'edTotScarichi
    '
    Me.edTotScarichi.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTotScarichi.EditValue = "0"
    Me.edTotScarichi.Enabled = False
    Me.edTotScarichi.Location = New System.Drawing.Point(632, 16)
    Me.edTotScarichi.Name = "edTotScarichi"
    Me.edTotScarichi.NTSDbField = ""
    Me.edTotScarichi.NTSFormat = "0"
    Me.edTotScarichi.NTSForzaVisZoom = False
    Me.edTotScarichi.NTSOldValue = ""
    Me.edTotScarichi.Properties.Appearance.Options.UseTextOptions = True
    Me.edTotScarichi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTotScarichi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTotScarichi.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTotScarichi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTotScarichi.Properties.MaxLength = 65536
    Me.edTotScarichi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTotScarichi.Size = New System.Drawing.Size(155, 20)
    Me.edTotScarichi.TabIndex = 5
    '
    'lbTotCarichi
    '
    Me.lbTotCarichi.AutoSize = True
    Me.lbTotCarichi.BackColor = System.Drawing.Color.Transparent
    Me.lbTotCarichi.Location = New System.Drawing.Point(230, 19)
    Me.lbTotCarichi.Name = "lbTotCarichi"
    Me.lbTotCarichi.NTSDbField = ""
    Me.lbTotCarichi.Size = New System.Drawing.Size(70, 13)
    Me.lbTotCarichi.TabIndex = 2
    Me.lbTotCarichi.Text = "Totale carichi"
    Me.lbTotCarichi.Tooltip = ""
    Me.lbTotCarichi.UseMnemonic = False
    '
    'lbTotscarichi
    '
    Me.lbTotscarichi.AutoSize = True
    Me.lbTotscarichi.BackColor = System.Drawing.Color.Transparent
    Me.lbTotscarichi.Location = New System.Drawing.Point(506, 19)
    Me.lbTotscarichi.Name = "lbTotscarichi"
    Me.lbTotscarichi.NTSDbField = ""
    Me.lbTotscarichi.Size = New System.Drawing.Size(75, 13)
    Me.lbTotscarichi.TabIndex = 1
    Me.lbTotscarichi.Text = "Totale scarichi"
    Me.lbTotscarichi.Tooltip = ""
    Me.lbTotscarichi.UseMnemonic = False
    '
    'FRMMGGRSC
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(799, 442)
    Me.Controls.Add(Me.pnGrid)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.pnBottom)
    Me.MinimizeBox = False
    Me.Name = "FRMMGGRSC"
    Me.Text = "PRECEDENTI MOVIMENTI CONTO - ARTICOLO"
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    CType(Me.grMovim, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvMovim, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBottom.ResumeLayout(False)
    Me.pnBottom.PerformLayout()
    CType(Me.edTotCarichi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTotScarichi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGGRSC", "BEMGDOCU", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 130086825547085675, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
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
      dttTipoRk.Rows.Add(New Object() {"A", "Fatture Imm. emesse"})
      dttTipoRk.Rows.Add(New Object() {"B", "D.D.T. emessi"})
      dttTipoRk.Rows.Add(New Object() {"C", "Corrispettivi emessi"})
      dttTipoRk.Rows.Add(New Object() {"D", "Fatture Diff. emesse"})
      dttTipoRk.Rows.Add(New Object() {"E", "Note di Addebito emesse"})
      dttTipoRk.Rows.Add(New Object() {"F", "Ric.Fiscale Emessa"})
      dttTipoRk.Rows.Add(New Object() {"I", "Riemissione Ric.Fiscali"})
      dttTipoRk.Rows.Add(New Object() {"J", "Note Accr. ricevute"})
      dttTipoRk.Rows.Add(New Object() {"K", "Fatture Diff. ricevute"})
      dttTipoRk.Rows.Add(New Object() {"L", "Fatture Imm. ricevute"})
      dttTipoRk.Rows.Add(New Object() {"M", "D.D.T. ricevuti"})
      dttTipoRk.Rows.Add(New Object() {"N", "Note Accr. emesse"})
      dttTipoRk.Rows.Add(New Object() {"P", "Fatt.Ric.Fisc.Differita"})
      dttTipoRk.Rows.Add(New Object() {"S", "Fatt.Ric.Fisc. Emessa"})
      dttTipoRk.Rows.Add(New Object() {"T", "Carico da produz."})
      dttTipoRk.Rows.Add(New Object() {"U", "Scarico a produz."})
      dttTipoRk.Rows.Add(New Object() {"Z", "Bolle di mov. interna"})
      dttTipoRk.Rows.Add(New Object() {"£", "Note accred. diff. emesse"})
      dttTipoRk.Rows.Add(New Object() {"(", "Note accred. diff. ricevute"})

      edTotCarichi.NTSSetParam(oMenu, oApp.Tr(Me, 128570394397656250, "Totale carichi"), oApp.FormatQta)
      edTotScarichi.NTSSetParam(oMenu, oApp.Tr(Me, 128570394622656250, "Totale scarichi"), oApp.FormatQta)
      grvMovim.NTSSetParam(oMenu, oApp.Tr(Me, 128230023871441576, "Precedenti movimenti"))
      xx_carichi.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935088437500, "Carichi"), oApp.FormatQta, 9, 0, 999999999)
      km_aammgg.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128569935088750000, "Data doc"), True, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      km_causale.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935089062500, "Causale"), "0", 4, 0, 9999)
      xx_lottox.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935089687500, "Lotto"), "0", 9, 0, 999999999)
      km_magaz.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935089843750, "Magaz"), "0", 4, 0, 9999)
      km_numdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935090000000, "Num doc"), "0", 9, 0, 999999999)
      km_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128569935090312500, "Serie doc"), CLN__STD.SerieMaxLen, True)
      km_subcommeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128569935090468750, "SubCom"), 2, True)
      km_tipork.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128569935090625000, "Tipo doc"), dttTipoRk, "val", "cod")
      km_ubicaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128569935090781250, "Ubicaz."), 0, True)
      mm_codcena.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935090937500, "Centro"), "0", 9, 0, 999999999)
      mm_codcfam.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128569935091093750, "Famiglia"), 0, True)
      mm_codiva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935091250000, "Cod. IVA"), "0", 4, 0, 9999)
      mm_codnomc.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128569935091406250, "Cod. nom. comb."), 0, True)
      mm_colli.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935091562500, "Colli"), oApp.FormatQta, 20, -9999999999999, 9999999999999)
      mm_commeca.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935091718750, "Commessa"), "0", 9, 0, 999999999)
      mm_controp.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935091875000, "Controp."), "0", 4, 0, 9999)
      mm_misura1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935092031250, "Misura 1"), "0", 20, -9999999999999, 9999999999999)
      mm_misura2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935092187500, "Misura 2"), "0", 20, -9999999999999, 9999999999999)
      mm_misura3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935092343750, "Misura 3"), "0", 20, -9999999999999, 9999999999999)
      mm_ornum.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935092500000, "Num. ord"), "0", 9, 0, 999999999)
      mm_perqta.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935092656250, "Perqta"), "0", 6, 0, 1000000)
      mm_prelist.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935092812500, "Prezzo list."), oApp.FormatPrzUn, 20, -9999999999999, 9999999999999)
      mm_preziva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935092968750, "Prezzo IVA comp."), oApp.FormatPrzUn, 20, -9999999999999, 9999999999999)
      mm_prezvalc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935093125000, "Prezzo val."), oApp.FormatPrzUnVal, 20, -9999999999999, 9999999999999)
      mm_prezzo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935093281250, "Prezzo"), oApp.FormatPrzUn, 20, -9999999999999, 9999999999999)
      mm_provv.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935093437500, "Provv."), oApp.FormatSconti, 6, -100, 100)
      mm_provv2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935093593750, "Provv. 2"), oApp.FormatSconti, 6, -100, 100)
      mm_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935093750000, "Quant."), oApp.FormatQta, 20, -9999999999999, 9999999999999)
      mm_scont1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935093906250, "Sconto 1"), oApp.FormatSconti, 6, -100, 100)
      mm_scont2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935094062500, "Sconto 2"), oApp.FormatSconti, 6, -100, 100)
      mm_scont3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935094218750, "Sconto 3"), oApp.FormatSconti, 6, -100, 100)
      mm_scont4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935094375000, "Sconto 4"), oApp.FormatSconti, 6, -100, 100)
      mm_scont5.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935094531250, "Sconto 5"), oApp.FormatSconti, 6, -100, 100)
      mm_scont6.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935094687500, "Sconto 6"), oApp.FormatSconti, 6, -100, 100)
      mm_valore.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935094843750, "Valore"), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      mm_vprovv.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935095000000, "Valore Provv"), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      mm_vprovv2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935095156250, "Valore Provv 2"), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      xx_prezzo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935095312500, "Prezzo"), oApp.FormatPrzUn, 9, -9999999999999, 9999999999999)
      xx_scarichi.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935095468750, "Scarichi"), oApp.FormatQta, 9, -9999999999999, 9999999999999)
      tb_descaum.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128569935095625000, "Descr. causale"), 0, True)
      tm_riferim.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128569935095781250, "Riferimenti"), 0, True)
      tm_valuta.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128569935095937500, "Valuta"), "0", 4, 0, 9999)
      km_conto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128570713437031250, "Conto cliente/fornitore"), "0", 9, 0, 999999999)
      xx_conto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128570713833906250, "Descr. conto cliente/fornitore"), 0, True)

      grvMovim.NTSAllowDelete = False
      grvMovim.NTSAllowInsert = False
      grvMovim.Enabled = False

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

  Public Overridable Sub FRMMGGRSC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '------------------------------------------------
      'CRM: se l'operatore non è stato codificato e non ha un ruolo non può operare
      bModuloCRM = False
      If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And CLN__STD.bsModExtCRM) Or _
         CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And CLN__STD.bsModSupWCR) Then bModuloCRM = True
      If bModuloCRM Then
        bIsCRMUser = oMenu.IsCrmUser(DittaCorrente, bAmm, strAccvis, strAccmod, strRegvis, strRegmod)
        If bIsCRMUser Then
          lCodorgaOperat = oMenu.RitornaCodorgaDaOpnome(DittaCorrente, nCodcageoperat)
          If lCodorgaOperat = 0 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 127791222142500000, "Attenzione!" & vbCrLf & "L'operatore '|" & oApp.User.Nome & _
                 "|' (CRM) non è associato all'organizzazione della ditta corrente '|" & DittaCorrente & "|'." & vbCrLf & _
                 "Impossibile continuare."))
            Me.Close()
            Return
          End If
        End If
      End If

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGGRSC_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Dim i As Integer = 0
    Dim dTotC As Decimal = 0
    Dim dTotS As Decimal = 0
    Try
      '-----------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleDocu.GetPrecedentiMovimenti(DittaCorrente, dsMovim, oCallParams.strPar1, NTSCInt(oCallParams.dPar1), _
                                             NTSCInt(oCallParams.dPar2), NTSCInt(oCallParams.dPar3), bModuloCRM, _
                                             bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, bAmm) Then
        Me.Close()
        Return
      End If

      Me.Cursor = Cursors.WaitCursor
      dcMovim.DataSource = dsMovim.Tables("MOVIM")
      dsMovim.AcceptChanges()
      grMovim.DataSource = dcMovim

      If oCallParams.strNomProg = "BNMGLIST" Then cmdSeleziona.Visible = False

      If dsMovim.Tables("MOVIM").Rows.Count > 0 Then
        With dsMovim.Tables("MOVIM").Rows(0)
          If NTSCInt(oCallParams.dPar3) = 0 Then
            lbConto.Text = oApp.Tr(Me, 128570711819218750, "Tutti i conti cliente/fornitore")
          Else
            lbConto.Text = !km_conto.ToString & " - " & !xx_conto.ToString
          End If
          lbArticolo.Text = !km_codart.ToString & " - " & !mm_descr.ToString
          lbMagaz.Text = NTSCInt(oCallParams.dPar2).ToString
          lbFase.Text = !km_fase.ToString
        End With
        For i = 0 To dsMovim.Tables("MOVIM").Rows.Count - 1
          dTotC += NTSCDec(dsMovim.Tables("MOVIM").Rows(i)!xx_carichi)
          dTotS += NTSCDec(dsMovim.Tables("MOVIM").Rows(i)!xx_scarichi)
        Next
        edTotCarichi.Text = dTotC.ToString
        edTotScarichi.Text = dTotS.ToString
      Else
        oApp.MsgBoxInfo(oApp.Tr(Me, 128570359134843750, "Non sono stati trovati movimenti a parità di cliente / articolo / fase articolo / magazzino"))
        Me.Close()
        Return
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub FRMMGGRSC_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcMovim.Dispose()
      dsMovim.Dispose()
    Catch
    End Try
  End Sub

  Public Overridable Sub cmdEsci_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEsci.Click
    Try
      oCallParams.strPar2 = ""
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Try
      If dsMovim.Tables.Count > 0 Then
        If dsMovim.Tables("MOVIM").Rows.Count > 0 Then
          With grvMovim.NTSGetCurrentDataRow
            oCallParams.strPar2 = NTSCInt(!tm_valuta).ToString & ";" & _
                                  !tm_scorpo.ToString & ";" & _
                                  NTSCDec(!mm_prezvalc).ToString & ";" & _
                                  NTSCDec(!mm_preziva).ToString & ";" & _
                                  NTSCDec(!mm_prezzo).ToString & ";" & _
                                  NTSCDec(!mm_scont1).ToString & ";" & _
                                  NTSCDec(!mm_scont2).ToString & ";" & _
                                  NTSCDec(!mm_scont3).ToString & ";" & _
                                  NTSCDec(!mm_scont4).ToString & ";" & _
                                  NTSCDec(!mm_scont5).ToString & ";" & _
                                  NTSCDec(!mm_scont6).ToString & ";" & _
                                  NTSCDec(!mm_codiva).ToString
          End With

        End If
      End If
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grMovim_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grMovim.MouseDoubleClick
    cmdSeleziona_Click(Me, Nothing)
  End Sub
End Class
