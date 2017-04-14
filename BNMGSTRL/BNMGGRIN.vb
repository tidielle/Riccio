Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGGRIN
  Public oCleStrl As CLEMGSTRL
  Public oCallParams As CLE__CLDP
  Public dsGrin As DataSet
  Public dcGrin As BindingSource = New BindingSource()

  Private components As System.ComponentModel.IContainer


  Private Sub InitializeComponent()
    Me.grGrin = New NTSInformatica.NTSGrid
    Me.grvGrin = New NTSInformatica.NTSGridView
    Me.in_desint = New NTSInformatica.NTSGridColumn
    Me.in_codart = New NTSInformatica.NTSGridColumn
    Me.in_desart = New NTSInformatica.NTSGridColumn
    Me.in_unmis = New NTSInformatica.NTSGridColumn
    Me.in_giaini = New NTSInformatica.NTSGridColumn
    Me.in_vgiaini = New NTSInformatica.NTSGridColumn
    Me.in_incdec = New NTSInformatica.NTSGridColumn
    Me.in_costo = New NTSInformatica.NTSGridColumn
    Me.in_val = New NTSInformatica.NTSGridColumn
    Me.in_esist = New NTSInformatica.NTSGridColumn
    Me.in_vesist = New NTSInformatica.NTSGridColumn
    Me.in_perqta = New NTSInformatica.NTSGridColumn
    Me.in_fase = New NTSInformatica.NTSGridColumn
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.cmdStampaVideo = New NTSInformatica.NTSButton
    Me.lbValoreLabel = New NTSInformatica.NTSLabel
    Me.lbTipomerceLabel = New NTSInformatica.NTSLabel
    Me.lbTipomagazLabel = New NTSInformatica.NTSLabel
    Me.lbTipoelabLabel = New NTSInformatica.NTSLabel
    Me.lbValore = New NTSInformatica.NTSLabel
    Me.lbTipomerce = New NTSInformatica.NTSLabel
    Me.lbTipomagaz = New NTSInformatica.NTSLabel
    Me.lbTipoelab = New NTSInformatica.NTSLabel
    Me.cmdEsci = New NTSInformatica.NTSButton
    Me.lbTipval = New NTSInformatica.NTSLabel
    Me.lbTipvalLabel = New NTSInformatica.NTSLabel
    Me.pnGrid = New NTSInformatica.NTSPanel
    CType(Me.grGrin, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvGrin, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
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
    'grGrin
    '
    Me.grGrin.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grGrin.EmbeddedNavigator.Name = ""
    Me.grGrin.Location = New System.Drawing.Point(0, 0)
    Me.grGrin.MainView = Me.grvGrin
    Me.grGrin.Name = "grGrin"
    Me.grGrin.Size = New System.Drawing.Size(648, 340)
    Me.grGrin.TabIndex = 5
    Me.grGrin.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvGrin})
    '
    'grvGrin
    '
    Me.grvGrin.ActiveFilterEnabled = False
    '
    'in_desint
    '
    Me.in_desint.AppearanceCell.Options.UseBackColor = True
    Me.in_desint.AppearanceCell.Options.UseTextOptions = True
    Me.in_desint.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.in_desint.Caption = "2a descrizione"
    Me.in_desint.Enabled = True
    Me.in_desint.FieldName = "in_desint"
    Me.in_desint.Name = "in_desint"
    Me.in_desint.NTSRepositoryComboBox = Nothing
    Me.in_desint.NTSRepositoryItemCheck = Nothing
    Me.in_desint.NTSRepositoryItemMemo = Nothing
    Me.in_desint.NTSRepositoryItemText = Nothing
    Me.in_desint.Visible = True
    Me.in_desint.VisibleIndex = 12
    Me.grvGrin.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.in_codart, Me.in_desart, Me.in_unmis, Me.in_giaini, Me.in_vgiaini, Me.in_incdec, Me.in_costo, Me.in_val, Me.in_esist, Me.in_vesist, Me.in_perqta, Me.in_fase, Me.in_desint})
    Me.grvGrin.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvGrin.Enabled = True
    Me.grvGrin.GridControl = Me.grGrin
    Me.grvGrin.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvGrin.MinRowHeight = 14
    Me.grvGrin.Name = "grvGrin"
    Me.grvGrin.NTSAllowDelete = True
    Me.grvGrin.NTSAllowInsert = True
    Me.grvGrin.NTSAllowUpdate = True
    Me.grvGrin.NTSMenuContext = Nothing
    Me.grvGrin.OptionsCustomization.AllowRowSizing = True
    Me.grvGrin.OptionsFilter.AllowFilterEditor = False
    Me.grvGrin.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvGrin.OptionsNavigation.UseTabKey = False
    Me.grvGrin.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvGrin.OptionsView.ColumnAutoWidth = False
    Me.grvGrin.OptionsView.EnableAppearanceEvenRow = True
    Me.grvGrin.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvGrin.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvGrin.OptionsView.ShowGroupPanel = False
    Me.grvGrin.RowHeight = 16
    '
    'in_codart
    '
    Me.in_codart.AppearanceCell.Options.UseBackColor = True
    Me.in_codart.AppearanceCell.Options.UseTextOptions = True
    Me.in_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.in_codart.Caption = "Articolo"
    Me.in_codart.Enabled = True
    Me.in_codart.FieldName = "in_codart"
    Me.in_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.in_codart.Name = "in_codart"
    Me.in_codart.NTSRepositoryComboBox = Nothing
    Me.in_codart.NTSRepositoryItemCheck = Nothing
    Me.in_codart.NTSRepositoryItemMemo = Nothing
    Me.in_codart.NTSRepositoryItemText = Nothing
    Me.in_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.in_codart.OptionsFilter.AllowFilter = False
    Me.in_codart.Visible = True
    Me.in_codart.VisibleIndex = 0
    Me.in_codart.Width = 70
    '
    'in_desart
    '
    Me.in_desart.AppearanceCell.Options.UseBackColor = True
    Me.in_desart.AppearanceCell.Options.UseTextOptions = True
    Me.in_desart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.in_desart.Caption = "Descrizione"
    Me.in_desart.Enabled = True
    Me.in_desart.FieldName = "in_desart"
    Me.in_desart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.in_desart.Name = "in_desart"
    Me.in_desart.NTSRepositoryComboBox = Nothing
    Me.in_desart.NTSRepositoryItemCheck = Nothing
    Me.in_desart.NTSRepositoryItemMemo = Nothing
    Me.in_desart.NTSRepositoryItemText = Nothing
    Me.in_desart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.in_desart.OptionsFilter.AllowFilter = False
    Me.in_desart.Visible = True
    Me.in_desart.VisibleIndex = 1
    Me.in_desart.Width = 70
    '
    'in_unmis
    '
    Me.in_unmis.AppearanceCell.Options.UseBackColor = True
    Me.in_unmis.AppearanceCell.Options.UseTextOptions = True
    Me.in_unmis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.in_unmis.Caption = "UM"
    Me.in_unmis.Enabled = True
    Me.in_unmis.FieldName = "in_unmis"
    Me.in_unmis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.in_unmis.Name = "in_unmis"
    Me.in_unmis.NTSRepositoryComboBox = Nothing
    Me.in_unmis.NTSRepositoryItemCheck = Nothing
    Me.in_unmis.NTSRepositoryItemMemo = Nothing
    Me.in_unmis.NTSRepositoryItemText = Nothing
    Me.in_unmis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.in_unmis.OptionsFilter.AllowFilter = False
    Me.in_unmis.Visible = True
    Me.in_unmis.VisibleIndex = 2
    Me.in_unmis.Width = 70
    '
    'in_giaini
    '
    Me.in_giaini.AppearanceCell.Options.UseBackColor = True
    Me.in_giaini.AppearanceCell.Options.UseTextOptions = True
    Me.in_giaini.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.in_giaini.Caption = "Giac. iniz."
    Me.in_giaini.Enabled = True
    Me.in_giaini.FieldName = "in_giaini"
    Me.in_giaini.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.in_giaini.Name = "in_giaini"
    Me.in_giaini.NTSRepositoryComboBox = Nothing
    Me.in_giaini.NTSRepositoryItemCheck = Nothing
    Me.in_giaini.NTSRepositoryItemMemo = Nothing
    Me.in_giaini.NTSRepositoryItemText = Nothing
    Me.in_giaini.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.in_giaini.OptionsFilter.AllowFilter = False
    Me.in_giaini.Visible = True
    Me.in_giaini.VisibleIndex = 3
    Me.in_giaini.Width = 70
    '
    'in_vgiaini
    '
    Me.in_vgiaini.AppearanceCell.Options.UseBackColor = True
    Me.in_vgiaini.AppearanceCell.Options.UseTextOptions = True
    Me.in_vgiaini.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.in_vgiaini.Caption = "Val. giac. iniz."
    Me.in_vgiaini.Enabled = True
    Me.in_vgiaini.FieldName = "in_vgiaini"
    Me.in_vgiaini.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.in_vgiaini.Name = "in_vgiaini"
    Me.in_vgiaini.NTSRepositoryComboBox = Nothing
    Me.in_vgiaini.NTSRepositoryItemCheck = Nothing
    Me.in_vgiaini.NTSRepositoryItemMemo = Nothing
    Me.in_vgiaini.NTSRepositoryItemText = Nothing
    Me.in_vgiaini.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.in_vgiaini.OptionsFilter.AllowFilter = False
    Me.in_vgiaini.Visible = True
    Me.in_vgiaini.VisibleIndex = 4
    Me.in_vgiaini.Width = 70
    '
    'in_incdec
    '
    Me.in_incdec.AppearanceCell.Options.UseBackColor = True
    Me.in_incdec.AppearanceCell.Options.UseTextOptions = True
    Me.in_incdec.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.in_incdec.Caption = "Incr./decr."
    Me.in_incdec.Enabled = True
    Me.in_incdec.FieldName = "in_incdec"
    Me.in_incdec.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.in_incdec.Name = "in_incdec"
    Me.in_incdec.NTSRepositoryComboBox = Nothing
    Me.in_incdec.NTSRepositoryItemCheck = Nothing
    Me.in_incdec.NTSRepositoryItemMemo = Nothing
    Me.in_incdec.NTSRepositoryItemText = Nothing
    Me.in_incdec.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.in_incdec.OptionsFilter.AllowFilter = False
    Me.in_incdec.Visible = True
    Me.in_incdec.VisibleIndex = 5
    Me.in_incdec.Width = 70
    '
    'in_costo
    '
    Me.in_costo.AppearanceCell.Options.UseBackColor = True
    Me.in_costo.AppearanceCell.Options.UseTextOptions = True
    Me.in_costo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.in_costo.Caption = "Costo"
    Me.in_costo.Enabled = True
    Me.in_costo.FieldName = "in_costo"
    Me.in_costo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.in_costo.Name = "in_costo"
    Me.in_costo.NTSRepositoryComboBox = Nothing
    Me.in_costo.NTSRepositoryItemCheck = Nothing
    Me.in_costo.NTSRepositoryItemMemo = Nothing
    Me.in_costo.NTSRepositoryItemText = Nothing
    Me.in_costo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.in_costo.OptionsFilter.AllowFilter = False
    Me.in_costo.Visible = True
    Me.in_costo.VisibleIndex = 6
    Me.in_costo.Width = 70
    '
    'in_val
    '
    Me.in_val.AppearanceCell.Options.UseBackColor = True
    Me.in_val.AppearanceCell.Options.UseTextOptions = True
    Me.in_val.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.in_val.Caption = "Tipo Val."
    Me.in_val.Enabled = True
    Me.in_val.FieldName = "in_val"
    Me.in_val.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.in_val.Name = "in_val"
    Me.in_val.NTSRepositoryComboBox = Nothing
    Me.in_val.NTSRepositoryItemCheck = Nothing
    Me.in_val.NTSRepositoryItemMemo = Nothing
    Me.in_val.NTSRepositoryItemText = Nothing
    Me.in_val.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.in_val.OptionsFilter.AllowFilter = False
    Me.in_val.Visible = True
    Me.in_val.VisibleIndex = 7
    Me.in_val.Width = 70
    '
    'in_esist
    '
    Me.in_esist.AppearanceCell.Options.UseBackColor = True
    Me.in_esist.AppearanceCell.Options.UseTextOptions = True
    Me.in_esist.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.in_esist.Caption = "Giac. finale"
    Me.in_esist.Enabled = True
    Me.in_esist.FieldName = "in_esist"
    Me.in_esist.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.in_esist.Name = "in_esist"
    Me.in_esist.NTSRepositoryComboBox = Nothing
    Me.in_esist.NTSRepositoryItemCheck = Nothing
    Me.in_esist.NTSRepositoryItemMemo = Nothing
    Me.in_esist.NTSRepositoryItemText = Nothing
    Me.in_esist.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.in_esist.OptionsFilter.AllowFilter = False
    Me.in_esist.Visible = True
    Me.in_esist.VisibleIndex = 8
    Me.in_esist.Width = 70
    '
    'in_vesist
    '
    Me.in_vesist.AppearanceCell.Options.UseBackColor = True
    Me.in_vesist.AppearanceCell.Options.UseTextOptions = True
    Me.in_vesist.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.in_vesist.Caption = "Valore giac. fin."
    Me.in_vesist.Enabled = True
    Me.in_vesist.FieldName = "in_vesist"
    Me.in_vesist.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.in_vesist.Name = "in_vesist"
    Me.in_vesist.NTSRepositoryComboBox = Nothing
    Me.in_vesist.NTSRepositoryItemCheck = Nothing
    Me.in_vesist.NTSRepositoryItemMemo = Nothing
    Me.in_vesist.NTSRepositoryItemText = Nothing
    Me.in_vesist.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.in_vesist.OptionsFilter.AllowFilter = False
    Me.in_vesist.Visible = True
    Me.in_vesist.VisibleIndex = 9
    Me.in_vesist.Width = 70
    '
    'in_perqta
    '
    Me.in_perqta.AppearanceCell.Options.UseBackColor = True
    Me.in_perqta.AppearanceCell.Options.UseTextOptions = True
    Me.in_perqta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.in_perqta.Caption = "Molt. Qta/prz"
    Me.in_perqta.Enabled = True
    Me.in_perqta.FieldName = "in_perqta"
    Me.in_perqta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.in_perqta.Name = "in_perqta"
    Me.in_perqta.NTSRepositoryComboBox = Nothing
    Me.in_perqta.NTSRepositoryItemCheck = Nothing
    Me.in_perqta.NTSRepositoryItemMemo = Nothing
    Me.in_perqta.NTSRepositoryItemText = Nothing
    Me.in_perqta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.in_perqta.OptionsFilter.AllowFilter = False
    Me.in_perqta.Visible = True
    Me.in_perqta.VisibleIndex = 10
    Me.in_perqta.Width = 70
    '
    'in_fase
    '
    Me.in_fase.AppearanceCell.Options.UseBackColor = True
    Me.in_fase.AppearanceCell.Options.UseTextOptions = True
    Me.in_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.in_fase.Caption = "Fase"
    Me.in_fase.Enabled = True
    Me.in_fase.FieldName = "in_fase"
    Me.in_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.in_fase.Name = "in_fase"
    Me.in_fase.NTSRepositoryComboBox = Nothing
    Me.in_fase.NTSRepositoryItemCheck = Nothing
    Me.in_fase.NTSRepositoryItemMemo = Nothing
    Me.in_fase.NTSRepositoryItemText = Nothing
    Me.in_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.in_fase.OptionsFilter.AllowFilter = False
    Me.in_fase.Visible = True
    Me.in_fase.VisibleIndex = 11
    Me.in_fase.Width = 70
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.cmdStampaVideo)
    Me.pnTop.Controls.Add(Me.lbValoreLabel)
    Me.pnTop.Controls.Add(Me.lbTipomerceLabel)
    Me.pnTop.Controls.Add(Me.lbTipomagazLabel)
    Me.pnTop.Controls.Add(Me.lbTipoelabLabel)
    Me.pnTop.Controls.Add(Me.lbValore)
    Me.pnTop.Controls.Add(Me.lbTipomerce)
    Me.pnTop.Controls.Add(Me.lbTipomagaz)
    Me.pnTop.Controls.Add(Me.lbTipoelab)
    Me.pnTop.Controls.Add(Me.cmdEsci)
    Me.pnTop.Controls.Add(Me.lbTipval)
    Me.pnTop.Controls.Add(Me.lbTipvalLabel)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(648, 102)
    Me.pnTop.TabIndex = 6
    Me.pnTop.Text = "NtsPanel1"
    '
    'cmdStampaVideo
    '
    Me.cmdStampaVideo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdStampaVideo.ImageText = ""
    Me.cmdStampaVideo.Location = New System.Drawing.Point(454, 32)
    Me.cmdStampaVideo.Name = "cmdStampaVideo"
    Me.cmdStampaVideo.Size = New System.Drawing.Size(182, 23)
    Me.cmdStampaVideo.TabIndex = 12
    Me.cmdStampaVideo.Text = "Stampa anteprima a video"
    '
    'lbValoreLabel
    '
    Me.lbValoreLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbValoreLabel.AutoSize = True
    Me.lbValoreLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbValoreLabel.Location = New System.Drawing.Point(358, 72)
    Me.lbValoreLabel.Name = "lbValoreLabel"
    Me.lbValoreLabel.NTSDbField = ""
    Me.lbValoreLabel.Size = New System.Drawing.Size(90, 13)
    Me.lbValoreLabel.TabIndex = 10
    Me.lbValoreLabel.Text = "Valore magazzino"
    Me.lbValoreLabel.Tooltip = ""
    Me.lbValoreLabel.UseMnemonic = False
    '
    'lbTipomerceLabel
    '
    Me.lbTipomerceLabel.AutoSize = True
    Me.lbTipomerceLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbTipomerceLabel.Location = New System.Drawing.Point(12, 72)
    Me.lbTipomerceLabel.Name = "lbTipomerceLabel"
    Me.lbTipomerceLabel.NTSDbField = ""
    Me.lbTipomerceLabel.Size = New System.Drawing.Size(59, 13)
    Me.lbTipomerceLabel.TabIndex = 9
    Me.lbTipomerceLabel.Text = "Tipo merce"
    Me.lbTipomerceLabel.Tooltip = ""
    Me.lbTipomerceLabel.UseMnemonic = False
    '
    'lbTipomagazLabel
    '
    Me.lbTipomagazLabel.AutoSize = True
    Me.lbTipomagazLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbTipomagazLabel.Location = New System.Drawing.Point(12, 52)
    Me.lbTipomagazLabel.Name = "lbTipomagazLabel"
    Me.lbTipomagazLabel.NTSDbField = ""
    Me.lbTipomagazLabel.Size = New System.Drawing.Size(80, 13)
    Me.lbTipomagazLabel.TabIndex = 8
    Me.lbTipomagazLabel.Text = "Tipo magazzino"
    Me.lbTipomagazLabel.Tooltip = ""
    Me.lbTipomagazLabel.UseMnemonic = False
    '
    'lbTipoelabLabel
    '
    Me.lbTipoelabLabel.AutoSize = True
    Me.lbTipoelabLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbTipoelabLabel.Location = New System.Drawing.Point(12, 32)
    Me.lbTipoelabLabel.Name = "lbTipoelabLabel"
    Me.lbTipoelabLabel.NTSDbField = ""
    Me.lbTipoelabLabel.Size = New System.Drawing.Size(91, 13)
    Me.lbTipoelabLabel.TabIndex = 7
    Me.lbTipoelabLabel.Text = "Tipo elaborazione"
    Me.lbTipoelabLabel.Tooltip = ""
    Me.lbTipoelabLabel.UseMnemonic = False
    '
    'lbValore
    '
    Me.lbValore.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbValore.BackColor = System.Drawing.Color.Transparent
    Me.lbValore.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbValore.Location = New System.Drawing.Point(454, 71)
    Me.lbValore.Name = "lbValore"
    Me.lbValore.NTSDbField = ""
    Me.lbValore.Size = New System.Drawing.Size(182, 20)
    Me.lbValore.TabIndex = 6
    Me.lbValore.TextAlign = System.Drawing.ContentAlignment.TopRight
    Me.lbValore.Tooltip = ""
    Me.lbValore.UseMnemonic = False
    '
    'lbTipomerce
    '
    Me.lbTipomerce.BackColor = System.Drawing.Color.Transparent
    Me.lbTipomerce.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbTipomerce.Location = New System.Drawing.Point(115, 71)
    Me.lbTipomerce.Name = "lbTipomerce"
    Me.lbTipomerce.NTSDbField = ""
    Me.lbTipomerce.Size = New System.Drawing.Size(202, 20)
    Me.lbTipomerce.TabIndex = 5
    Me.lbTipomerce.Tooltip = ""
    Me.lbTipomerce.UseMnemonic = False
    '
    'lbTipomagaz
    '
    Me.lbTipomagaz.BackColor = System.Drawing.Color.Transparent
    Me.lbTipomagaz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbTipomagaz.Location = New System.Drawing.Point(115, 51)
    Me.lbTipomagaz.Name = "lbTipomagaz"
    Me.lbTipomagaz.NTSDbField = ""
    Me.lbTipomagaz.Size = New System.Drawing.Size(202, 20)
    Me.lbTipomagaz.TabIndex = 4
    Me.lbTipomagaz.Tooltip = ""
    Me.lbTipomagaz.UseMnemonic = False
    '
    'lbTipoelab
    '
    Me.lbTipoelab.BackColor = System.Drawing.Color.Transparent
    Me.lbTipoelab.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbTipoelab.Location = New System.Drawing.Point(115, 31)
    Me.lbTipoelab.Name = "lbTipoelab"
    Me.lbTipoelab.NTSDbField = ""
    Me.lbTipoelab.Size = New System.Drawing.Size(202, 20)
    Me.lbTipoelab.TabIndex = 3
    Me.lbTipoelab.Tooltip = ""
    Me.lbTipoelab.UseMnemonic = False
    '
    'cmdEsci
    '
    Me.cmdEsci.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdEsci.ImageText = ""
    Me.cmdEsci.Location = New System.Drawing.Point(454, 9)
    Me.cmdEsci.Name = "cmdEsci"
    Me.cmdEsci.Size = New System.Drawing.Size(182, 23)
    Me.cmdEsci.TabIndex = 2
    Me.cmdEsci.Text = "Chiudi"
    '
    'lbTipval
    '
    Me.lbTipval.BackColor = System.Drawing.Color.Transparent
    Me.lbTipval.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbTipval.Location = New System.Drawing.Point(115, 9)
    Me.lbTipval.Name = "lbTipval"
    Me.lbTipval.NTSDbField = ""
    Me.lbTipval.Size = New System.Drawing.Size(202, 20)
    Me.lbTipval.TabIndex = 1
    Me.lbTipval.Tooltip = ""
    Me.lbTipval.UseMnemonic = False
    '
    'lbTipvalLabel
    '
    Me.lbTipvalLabel.AutoSize = True
    Me.lbTipvalLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbTipvalLabel.Location = New System.Drawing.Point(12, 10)
    Me.lbTipvalLabel.Name = "lbTipvalLabel"
    Me.lbTipvalLabel.NTSDbField = ""
    Me.lbTipvalLabel.Size = New System.Drawing.Size(97, 13)
    Me.lbTipvalLabel.TabIndex = 0
    Me.lbTipvalLabel.Text = "Tipo valorizzazione"
    Me.lbTipvalLabel.Tooltip = ""
    Me.lbTipvalLabel.UseMnemonic = False
    '
    'pnGrid
    '
    Me.pnGrid.AllowDrop = True
    Me.pnGrid.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrid.Appearance.Options.UseBackColor = True
    Me.pnGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGrid.Controls.Add(Me.grGrin)
    Me.pnGrid.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(0, 102)
    Me.pnGrid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGrid.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.NTSActiveTrasparency = True
    Me.pnGrid.Size = New System.Drawing.Size(648, 340)
    Me.pnGrid.TabIndex = 7
    Me.pnGrid.Text = "NtsPanel1"
    '
    'FRMMGGRIN
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.pnGrid)
    Me.Controls.Add(Me.pnTop)
    Me.MinimizeBox = False
    Me.Name = "FRMMGGRIN"
    Me.NTSLastControlFocussed = Me.grGrin
    Me.Text = "STAMPA INVENTARIO"
    CType(Me.grGrin, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvGrin, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
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

    Return True
  End Function

  Public Overridable Sub Initentity(ByVal cleStrl As CLEMGSTRL)
    oCleStrl = cleStrl
    AddHandler oCleStrl.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub


  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      grvGrin.NTSSetParam(oMenu, "STAMPA INVENTARIO")

      in_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128504108557800000, "Articolo"), CLN__STD.CodartMaxLen, True)
      in_desart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128504108557956000, "Descrizione"), 40, True)
      in_unmis.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128504108558112000, "UM"), 3, True)
      in_giaini.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128504108558268000, "Giac. iniz."), oApp.FormatQta, 6, -100, 100)
      in_vgiaini.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128504108558424000, "Val. giac. iniz."), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      in_incdec.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128504108558580000, "Incr./decr."), oApp.FormatQta, 6, -100, 100)
      in_costo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128504108558736000, "Costo"), oApp.FormatPrzUn, 20, -9999999999999, 9999999999999)
      in_val.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128504108558892000, "Tipo Val."), 0)
      in_esist.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128504108559048000, "Giac. finale"), oApp.FormatQta, 6, -100, 100)
      in_vesist.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128504108559204000, "Valore giac. fin."), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      in_perqta.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128504108559360000, "Molt. Qta/prz"), "0", 6, -100, 100)
      in_fase.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128504108559516000, "Fase"), "0", 4, 0, 9999)
      in_desint.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130118096822760857, "2a descrizione"), 40, True)
      grvGrin.Enabled = False
      grvGrin.NTSAllowInsert = False

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
  Public Overridable Sub FRMMGGRIN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleStrl.CaricaTtinvent(dsGrin) Then
        Return
        Me.Close()
      End If
      dcGrin.DataSource = dsGrin.Tables("TTINVENT")
      dsGrin.AcceptChanges()

      grGrin.DataSource = dcGrin

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione Friendly nasconde, sempre, alcune colonne
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = True Then
        in_fase.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub


  Public Overridable Sub FRMMGGRIN_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcGrin.Dispose()
      dsGrin.Dispose()
    Catch
    End Try
  End Sub
#End Region

  Public Overridable Sub cmdEsci_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEsci.Click
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdStampaVideo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStampaVideo.Click
    Dim strHeader As String = ""
    Dim strFooter As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strHeader = lbTipvalLabel.Text & "".PadLeft(1) & lbTipval.Text & "".PadLeft(5) & _
        lbTipoelabLabel.Text & "".PadLeft(1) & lbTipoelab.Text & "".PadLeft(5) & _
        lbTipomagazLabel.Text & "".PadLeft(1) & lbTipomagaz.Text & "".PadLeft(5) & _
        lbTipomerceLabel.Text & "".PadLeft(1) & lbTipomerce.Text
      strFooter = lbValoreLabel.Text & "".PadLeft(1) & lbValore.Text
      '--------------------------------------------------------------------------------------------------------------      
      grvGrin.NTSPrintPreview(strHeader, strFooter)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class
