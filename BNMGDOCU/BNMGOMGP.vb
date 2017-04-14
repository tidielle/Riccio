Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGOMGP
  Public oCallParams As CLE__CLDP

  Public dttOmgp As DataTable
  Public dcOmgp As New BindingSource
  Public strBlocchiOmaggio As String

  Public WithEvents pnAll As NTSInformatica.NTSPanel

  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGOMGP))
    Me.pnAll = New NTSInformatica.NTSPanel
    Me.pnBottomNoEdit = New NTSInformatica.NTSPanel
    Me.pnGiacenza = New NTSInformatica.NTSPanel
    Me.grOmgp = New NTSInformatica.NTSGrid
    Me.grvOmgp = New NTSInformatica.NTSGridView
    Me.xx_sel = New NTSInformatica.NTSGridColumn
    Me.ar_codart = New NTSInformatica.NTSGridColumn
    Me.ar_descr = New NTSInformatica.NTSGridColumn
    Me.ar_unmis = New NTSInformatica.NTSGridColumn
    Me.xx_quant = New NTSInformatica.NTSGridColumn
    Me.xx_maxquant = New NTSInformatica.NTSGridColumn
    Me.xx_codrepr = New NTSInformatica.NTSGridColumn
    Me.xx_desrepr = New NTSInformatica.NTSGridColumn
    Me.pnBottoni = New NTSInformatica.NTSPanel
    Me.cmdDx = New NTSInformatica.NTSButton
    Me.cmdGiu = New NTSInformatica.NTSButton
    Me.cmdSu = New NTSInformatica.NTSButton
    Me.cmdSx = New NTSInformatica.NTSButton
    Me.cmdEsci = New NTSInformatica.NTSButton
    Me.cmdConferma = New NTSInformatica.NTSButton
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnAll, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAll.SuspendLayout()
    CType(Me.pnBottomNoEdit, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBottomNoEdit.SuspendLayout()
    CType(Me.pnGiacenza, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGiacenza.SuspendLayout()
    CType(Me.grOmgp, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvOmgp, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnBottoni, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBottoni.SuspendLayout()
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
    Me.frmAuto.Appearance.BackColor = System.Drawing.SystemColors.GradientActiveCaption
    Me.frmAuto.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmAuto.Appearance.Options.UseBackColor = True
    Me.frmAuto.Appearance.Options.UseImage = True
    '
    'pnAll
    '
    Me.pnAll.AllowDrop = True
    Me.pnAll.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAll.Appearance.Options.UseBackColor = True
    Me.pnAll.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAll.Controls.Add(Me.pnBottomNoEdit)
    Me.pnAll.Controls.Add(Me.pnBottoni)
    Me.pnAll.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnAll.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnAll.Location = New System.Drawing.Point(0, 0)
    Me.pnAll.Name = "pnAll"
    Me.pnAll.NTSActiveTrasparency = True
    Me.pnAll.Size = New System.Drawing.Size(692, 395)
    Me.pnAll.TabIndex = 9
    Me.pnAll.Text = "NtsPanel1"
    '
    'pnBottomNoEdit
    '
    Me.pnBottomNoEdit.AllowDrop = True
    Me.pnBottomNoEdit.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBottomNoEdit.Appearance.Options.UseBackColor = True
    Me.pnBottomNoEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBottomNoEdit.Controls.Add(Me.pnGiacenza)
    Me.pnBottomNoEdit.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBottomNoEdit.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnBottomNoEdit.Location = New System.Drawing.Point(0, 0)
    Me.pnBottomNoEdit.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnBottomNoEdit.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnBottomNoEdit.Name = "pnBottomNoEdit"
    Me.pnBottomNoEdit.NTSActiveTrasparency = True
    Me.pnBottomNoEdit.Size = New System.Drawing.Size(692, 345)
    Me.pnBottomNoEdit.TabIndex = 24
    Me.pnBottomNoEdit.Text = "NtsPanel1"
    '
    'pnGiacenza
    '
    Me.pnGiacenza.AllowDrop = True
    Me.pnGiacenza.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGiacenza.Appearance.Options.UseBackColor = True
    Me.pnGiacenza.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGiacenza.Controls.Add(Me.grOmgp)
    Me.pnGiacenza.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGiacenza.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGiacenza.Location = New System.Drawing.Point(0, 0)
    Me.pnGiacenza.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGiacenza.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGiacenza.Name = "pnGiacenza"
    Me.pnGiacenza.NTSActiveTrasparency = True
    Me.pnGiacenza.Size = New System.Drawing.Size(692, 345)
    Me.pnGiacenza.TabIndex = 22
    Me.pnGiacenza.Text = "NtsPanel1"
    '
    'grOmgp
    '
    Me.grOmgp.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grOmgp.EmbeddedNavigator.Name = ""
    Me.grOmgp.Location = New System.Drawing.Point(0, 0)
    Me.grOmgp.MainView = Me.grvOmgp
    Me.grOmgp.Name = "grOmgp"
    Me.grOmgp.Size = New System.Drawing.Size(692, 345)
    Me.grOmgp.TabIndex = 1
    Me.grOmgp.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvOmgp})
    '
    'grvOmgp
    '
    Me.grvOmgp.ActiveFilterEnabled = False
    Me.grvOmgp.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_sel, Me.ar_codart, Me.ar_descr, Me.ar_unmis, Me.xx_quant, Me.xx_maxquant, Me.xx_codrepr, Me.xx_desrepr})
    Me.grvOmgp.Enabled = True
    Me.grvOmgp.GridControl = Me.grOmgp
    Me.grvOmgp.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvOmgp.MinRowHeight = 14
    Me.grvOmgp.Name = "grvOmgp"
    Me.grvOmgp.NTSAllowDelete = True
    Me.grvOmgp.NTSAllowInsert = True
    Me.grvOmgp.NTSAllowUpdate = True
    Me.grvOmgp.NTSMenuContext = Nothing
    Me.grvOmgp.OptionsCustomization.AllowRowSizing = True
    Me.grvOmgp.OptionsFilter.AllowFilterEditor = False
    Me.grvOmgp.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvOmgp.OptionsNavigation.UseTabKey = False
    Me.grvOmgp.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvOmgp.OptionsView.ColumnAutoWidth = False
    Me.grvOmgp.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvOmgp.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvOmgp.OptionsView.ShowGroupPanel = False
    Me.grvOmgp.RowHeight = 30
    '
    'xx_sel
    '
    Me.xx_sel.AppearanceCell.Options.UseBackColor = True
    Me.xx_sel.AppearanceCell.Options.UseTextOptions = True
    Me.xx_sel.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_sel.Caption = "Sel."
    Me.xx_sel.Enabled = True
    Me.xx_sel.FieldName = "xx_sel"
    Me.xx_sel.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_sel.Name = "xx_sel"
    Me.xx_sel.NTSRepositoryComboBox = Nothing
    Me.xx_sel.NTSRepositoryItemCheck = Nothing
    Me.xx_sel.NTSRepositoryItemMemo = Nothing
    Me.xx_sel.NTSRepositoryItemText = Nothing
    Me.xx_sel.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_sel.OptionsFilter.AllowFilter = False
    Me.xx_sel.Visible = True
    Me.xx_sel.VisibleIndex = 0
    '
    'ar_codart
    '
    Me.ar_codart.AppearanceCell.Options.UseBackColor = True
    Me.ar_codart.AppearanceCell.Options.UseTextOptions = True
    Me.ar_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_codart.Caption = "Articolo"
    Me.ar_codart.Enabled = False
    Me.ar_codart.FieldName = "ar_codart"
    Me.ar_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_codart.Name = "ar_codart"
    Me.ar_codart.NTSRepositoryComboBox = Nothing
    Me.ar_codart.NTSRepositoryItemCheck = Nothing
    Me.ar_codart.NTSRepositoryItemMemo = Nothing
    Me.ar_codart.NTSRepositoryItemText = Nothing
    Me.ar_codart.OptionsColumn.AllowEdit = False
    Me.ar_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_codart.OptionsColumn.ReadOnly = True
    Me.ar_codart.OptionsFilter.AllowFilter = False
    Me.ar_codart.Visible = True
    Me.ar_codart.VisibleIndex = 1
    '
    'ar_descr
    '
    Me.ar_descr.AppearanceCell.Options.UseBackColor = True
    Me.ar_descr.AppearanceCell.Options.UseTextOptions = True
    Me.ar_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_descr.Caption = "Descr."
    Me.ar_descr.Enabled = False
    Me.ar_descr.FieldName = "ar_descr"
    Me.ar_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_descr.Name = "ar_descr"
    Me.ar_descr.NTSRepositoryComboBox = Nothing
    Me.ar_descr.NTSRepositoryItemCheck = Nothing
    Me.ar_descr.NTSRepositoryItemMemo = Nothing
    Me.ar_descr.NTSRepositoryItemText = Nothing
    Me.ar_descr.OptionsColumn.AllowEdit = False
    Me.ar_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_descr.OptionsColumn.ReadOnly = True
    Me.ar_descr.OptionsFilter.AllowFilter = False
    Me.ar_descr.Visible = True
    Me.ar_descr.VisibleIndex = 2
    '
    'ar_unmis
    '
    Me.ar_unmis.AppearanceCell.Options.UseBackColor = True
    Me.ar_unmis.AppearanceCell.Options.UseTextOptions = True
    Me.ar_unmis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_unmis.Caption = "Un.mis."
    Me.ar_unmis.Enabled = False
    Me.ar_unmis.FieldName = "ar_unmis"
    Me.ar_unmis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_unmis.Name = "ar_unmis"
    Me.ar_unmis.NTSRepositoryComboBox = Nothing
    Me.ar_unmis.NTSRepositoryItemCheck = Nothing
    Me.ar_unmis.NTSRepositoryItemMemo = Nothing
    Me.ar_unmis.NTSRepositoryItemText = Nothing
    Me.ar_unmis.OptionsColumn.AllowEdit = False
    Me.ar_unmis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_unmis.OptionsColumn.ReadOnly = True
    Me.ar_unmis.OptionsFilter.AllowFilter = False
    Me.ar_unmis.Visible = True
    Me.ar_unmis.VisibleIndex = 3
    '
    'xx_quant
    '
    Me.xx_quant.AppearanceCell.Options.UseBackColor = True
    Me.xx_quant.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quant.Caption = "Quant."
    Me.xx_quant.Enabled = True
    Me.xx_quant.FieldName = "xx_quant"
    Me.xx_quant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quant.Name = "xx_quant"
    Me.xx_quant.NTSRepositoryComboBox = Nothing
    Me.xx_quant.NTSRepositoryItemCheck = Nothing
    Me.xx_quant.NTSRepositoryItemMemo = Nothing
    Me.xx_quant.NTSRepositoryItemText = Nothing
    Me.xx_quant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quant.OptionsFilter.AllowFilter = False
    Me.xx_quant.Visible = True
    Me.xx_quant.VisibleIndex = 4
    '
    'xx_maxquant
    '
    Me.xx_maxquant.AppearanceCell.Options.UseBackColor = True
    Me.xx_maxquant.AppearanceCell.Options.UseTextOptions = True
    Me.xx_maxquant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_maxquant.Caption = "Max Quant. "
    Me.xx_maxquant.Enabled = False
    Me.xx_maxquant.FieldName = "xx_maxquant"
    Me.xx_maxquant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_maxquant.Name = "xx_maxquant"
    Me.xx_maxquant.NTSRepositoryComboBox = Nothing
    Me.xx_maxquant.NTSRepositoryItemCheck = Nothing
    Me.xx_maxquant.NTSRepositoryItemMemo = Nothing
    Me.xx_maxquant.NTSRepositoryItemText = Nothing
    Me.xx_maxquant.OptionsColumn.AllowEdit = False
    Me.xx_maxquant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_maxquant.OptionsColumn.ReadOnly = True
    Me.xx_maxquant.OptionsFilter.AllowFilter = False
    Me.xx_maxquant.Visible = True
    Me.xx_maxquant.VisibleIndex = 5
    '
    'xx_codrepr
    '
    Me.xx_codrepr.AppearanceCell.Options.UseBackColor = True
    Me.xx_codrepr.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codrepr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codrepr.Caption = "Promozione"
    Me.xx_codrepr.Enabled = False
    Me.xx_codrepr.FieldName = "xx_codrepr"
    Me.xx_codrepr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codrepr.Name = "xx_codrepr"
    Me.xx_codrepr.NTSRepositoryComboBox = Nothing
    Me.xx_codrepr.NTSRepositoryItemCheck = Nothing
    Me.xx_codrepr.NTSRepositoryItemMemo = Nothing
    Me.xx_codrepr.NTSRepositoryItemText = Nothing
    Me.xx_codrepr.OptionsColumn.AllowEdit = False
    Me.xx_codrepr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codrepr.OptionsColumn.ReadOnly = True
    Me.xx_codrepr.OptionsFilter.AllowFilter = False
    Me.xx_codrepr.Visible = True
    Me.xx_codrepr.VisibleIndex = 6
    '
    'xx_desrepr
    '
    Me.xx_desrepr.AppearanceCell.Options.UseBackColor = True
    Me.xx_desrepr.AppearanceCell.Options.UseTextOptions = True
    Me.xx_desrepr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_desrepr.Caption = "Descr. promo."
    Me.xx_desrepr.Enabled = False
    Me.xx_desrepr.FieldName = "xx_desrepr"
    Me.xx_desrepr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_desrepr.Name = "xx_desrepr"
    Me.xx_desrepr.NTSRepositoryComboBox = Nothing
    Me.xx_desrepr.NTSRepositoryItemCheck = Nothing
    Me.xx_desrepr.NTSRepositoryItemMemo = Nothing
    Me.xx_desrepr.NTSRepositoryItemText = Nothing
    Me.xx_desrepr.OptionsColumn.AllowEdit = False
    Me.xx_desrepr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_desrepr.OptionsColumn.ReadOnly = True
    Me.xx_desrepr.OptionsFilter.AllowFilter = False
    Me.xx_desrepr.Visible = True
    Me.xx_desrepr.VisibleIndex = 7
    '
    'pnBottoni
    '
    Me.pnBottoni.AllowDrop = True
    Me.pnBottoni.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBottoni.Appearance.Options.UseBackColor = True
    Me.pnBottoni.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBottoni.Controls.Add(Me.cmdDx)
    Me.pnBottoni.Controls.Add(Me.cmdGiu)
    Me.pnBottoni.Controls.Add(Me.cmdSu)
    Me.pnBottoni.Controls.Add(Me.cmdSx)
    Me.pnBottoni.Controls.Add(Me.cmdEsci)
    Me.pnBottoni.Controls.Add(Me.cmdConferma)
    Me.pnBottoni.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBottoni.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnBottoni.Location = New System.Drawing.Point(0, 345)
    Me.pnBottoni.Name = "pnBottoni"
    Me.pnBottoni.NTSActiveTrasparency = True
    Me.pnBottoni.Size = New System.Drawing.Size(692, 50)
    Me.pnBottoni.TabIndex = 21
    Me.pnBottoni.Text = "NtsPanel1"
    '
    'cmdDx
    '
    Me.cmdDx.AllowFocus = False
    Me.cmdDx.Anchor = System.Windows.Forms.AnchorStyles.Top
    Me.cmdDx.Image = CType(resources.GetObject("cmdDx.Image"), System.Drawing.Image)
    Me.cmdDx.ImageText = ""
    Me.cmdDx.Location = New System.Drawing.Point(425, 3)
    Me.cmdDx.Name = "cmdDx"
    Me.cmdDx.NTSContextMenu = Nothing
    Me.cmdDx.Size = New System.Drawing.Size(55, 45)
    Me.cmdDx.TabIndex = 121
    Me.cmdDx.TabStop = False
    Me.cmdDx.Visible = False
    '
    'cmdGiu
    '
    Me.cmdGiu.AllowFocus = False
    Me.cmdGiu.Anchor = System.Windows.Forms.AnchorStyles.Top
    Me.cmdGiu.Image = CType(resources.GetObject("cmdGiu.Image"), System.Drawing.Image)
    Me.cmdGiu.ImageText = ""
    Me.cmdGiu.Location = New System.Drawing.Point(349, 3)
    Me.cmdGiu.Name = "cmdGiu"
    Me.cmdGiu.NTSContextMenu = Nothing
    Me.cmdGiu.Size = New System.Drawing.Size(55, 45)
    Me.cmdGiu.TabIndex = 120
    Me.cmdGiu.TabStop = False
    Me.cmdGiu.Visible = False
    '
    'cmdSu
    '
    Me.cmdSu.AllowFocus = False
    Me.cmdSu.Anchor = System.Windows.Forms.AnchorStyles.Top
    Me.cmdSu.Image = CType(resources.GetObject("cmdSu.Image"), System.Drawing.Image)
    Me.cmdSu.ImageText = ""
    Me.cmdSu.Location = New System.Drawing.Point(197, 3)
    Me.cmdSu.Name = "cmdSu"
    Me.cmdSu.NTSContextMenu = Nothing
    Me.cmdSu.Size = New System.Drawing.Size(55, 45)
    Me.cmdSu.TabIndex = 119
    Me.cmdSu.TabStop = False
    Me.cmdSu.Visible = False
    '
    'cmdSx
    '
    Me.cmdSx.AllowFocus = False
    Me.cmdSx.Anchor = System.Windows.Forms.AnchorStyles.Top
    Me.cmdSx.Image = CType(resources.GetObject("cmdSx.Image"), System.Drawing.Image)
    Me.cmdSx.ImageText = ""
    Me.cmdSx.Location = New System.Drawing.Point(273, 3)
    Me.cmdSx.Name = "cmdSx"
    Me.cmdSx.NTSContextMenu = Nothing
    Me.cmdSx.Size = New System.Drawing.Size(55, 45)
    Me.cmdSx.TabIndex = 118
    Me.cmdSx.TabStop = False
    Me.cmdSx.Visible = False
    '
    'cmdEsci
    '
    Me.cmdEsci.Appearance.Options.UseTextOptions = True
    Me.cmdEsci.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.cmdEsci.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Bottom
    Me.cmdEsci.Image = CType(resources.GetObject("cmdEsci.Image"), System.Drawing.Image)
    Me.cmdEsci.ImageAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.cmdEsci.ImageText = "Indietro"
    Me.cmdEsci.Location = New System.Drawing.Point(3, 3)
    Me.cmdEsci.Name = "cmdEsci"
    Me.cmdEsci.NTSContextMenu = Nothing
    Me.cmdEsci.Size = New System.Drawing.Size(60, 45)
    Me.cmdEsci.TabIndex = 116
    Me.cmdEsci.Text = "Indietro"
    '
    'cmdConferma
    '
    Me.cmdConferma.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdConferma.Appearance.Options.UseTextOptions = True
    Me.cmdConferma.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.cmdConferma.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Bottom
    Me.cmdConferma.Image = CType(resources.GetObject("cmdConferma.Image"), System.Drawing.Image)
    Me.cmdConferma.ImageAlignment = DevExpress.Utils.HorzAlignment.Center
    Me.cmdConferma.ImageText = "Conferma"
    Me.cmdConferma.Location = New System.Drawing.Point(629, 3)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.NTSContextMenu = Nothing
    Me.cmdConferma.Size = New System.Drawing.Size(60, 45)
    Me.cmdConferma.TabIndex = 115
    Me.cmdConferma.Text = "Conferma"
    '
    'FRMMGOMGP
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(692, 395)
    Me.Controls.Add(Me.pnAll)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMMGOMGP"
    Me.Text = "OMAGGI ARTICOLO"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnAll, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAll.ResumeLayout(False)
    CType(Me.pnBottomNoEdit, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBottomNoEdit.ResumeLayout(False)
    CType(Me.pnGiacenza, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGiacenza.ResumeLayout(False)
    CType(Me.grOmgp, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvOmgp, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnBottoni, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBottoni.ResumeLayout(False)
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

    InitializeComponent()
    Me.MinimumSize = Me.Size

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      Try
        cmdSx.Image = Bitmap.FromFile(oApp.ChildImageDir & "\arrow_left.gif")
        cmdSu.Image = Bitmap.FromFile(oApp.ChildImageDir & "\arrow_up.gif")
        cmdGiu.Image = Bitmap.FromFile(oApp.ChildImageDir & "\arrow_down.gif")
        cmdDx.Image = Bitmap.FromFile(oApp.ChildImageDir & "\arrow_right.gif")
        cmdConferma.Image = Bitmap.FromFile(oApp.ChildImageDir & "\recagg.gif")
        cmdEsci.Image = Bitmap.FromFile(oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try

      grvOmgp.NTSSetParam(oMenu, oApp.Tr(Me, 129442264503129212, "Omaggi"))
      xx_sel.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129442264503285439, "Sel."), "S", "N")
      ar_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129442264503441666, "Articolo"), 10, True)
      ar_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129442264503597893, "Descr."), 0, True)
      ar_unmis.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129442264503754120, "Un.mis."), 2, True)
      xx_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129442264503910347, "Quant."), oApp.FormatQta, 6)
      xx_maxquant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130003003506175486, "Max Quant."), oApp.FormatQta, 6)
      xx_codrepr.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129442264504066574, "Promozione"), "0", 9, 0, 999999999)
      xx_desrepr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129442264504222801, "Descr. promo."), 0, True)

      grvOmgp.NTSAllowDelete = False
      grvOmgp.NTSAllowInsert = False

      '--------------------------
      'chiamo lo script per inizializzare i controlli caricati con source ext
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

#Region "Eventi form"
  Public Overridable Sub FRMMGOMGP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      pnAll.Visible = False
      'Se sono con l'interfaccia normale, la tastiera non deve apparire.
      Me.bTouchModeManual = True

      dttOmgp = CType(oCallParams.ctlPar1, DataTable)
      strBlocchiOmaggio = oMenu.GetSettingBusDitt(DittaCorrente, "BSREGSRE", "OPZIONI", ".", "BloccoArticoliOmaggio", "N", ".", "N") 'N = nessun blocco, U = un unico omaggio selezionabile, P = come proposto

      dcOmgp.DataSource = dttOmgp
      grOmgp.DataSource = dcOmgp

      '--------------------------------------------
      'predispongo i controlli
      InitControls()

      ModificaMaschera()

      GctlSetRoules()
      pnAll.Visible = True

      'non è possibile modificare la selezione attuale
      If strBlocchiOmaggio = "P" Then
        xx_sel.Enabled = False
        dcOmgp.Filter = "xx_sel = 'S'"
      End If

      If CLN__STD.IsBis() OrElse oMenu.bForzaFormX Then
        'non c'è la sendkeys!!!!! 
        'non si riesce neanche a gestir, visto che non è detto che il testo debba essere accodato.
        'inoltre quando sono qui non so neanche qual'era il controllo con il focus!!!!
        cmdSx.Visible = False
        cmdDx.Visible = False
        cmdSu.Visible = False
        cmdGiu.Visible = False
      End If

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Function ModificaMaschera() As Boolean
    Dim dDimFont As Decimal
    Dim strFont As String
    Try
      dDimFont = NTSCDec(oMenu.GetSettingBus("BSREMENU", "OPZIONIUT", ".", "DimFont", "8,25", ".", "8,25"))
      strFont = oMenu.GetSettingBus("BSREMENU", "OPZIONIUT", ".", "Font", "Tahoma", ".", "Tahoma")

      If dDimFont <> 8.25 Or strFont <> "Tahoma" Then
        ImpostaFont(Me, strFont, dDimFont)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function ImpostaFont(ByVal oControl As Control, ByVal strFontName As String, ByVal dFont As Decimal) As Boolean
    Dim oStyle As New FontStyle
    Dim dDimFont As Single
    Try
      'Lo stile che dovrà essere usato nel font
      oStyle = FontStyle.Regular

      If oControl.Font.Bold Then
        oStyle = FontStyle.Bold
      ElseIf oControl.Font.Italic Then
        oStyle = FontStyle.Italic
      ElseIf oControl.Font.Strikeout Then
        oStyle = FontStyle.Strikeout
      ElseIf oControl.Font.Underline Then
        oStyle = FontStyle.Underline
      End If

      'La dimensione da usare
      If oControl.Font.Size = 8.25 Then
        dDimFont = dFont
      Else
        dDimFont = oControl.Font.Size
      End If

      'Viene cambiato il font e le dimensioni di solo alcuni controlli
      If oControl.GetType.ToString = "NTSInformatica.NTSComboBox" Or oControl.GetType.ToString = "NTSInformatica.NTSTextBoxNum" Or _
             oControl.GetType.ToString = "NTSInformatica.NTSTextBoxStr" Or oControl.GetType.ToString = "NTSInformatica.NTSTextBoxData" Or _
             oControl.GetType.ToString = "NTSInformatica.NTSMemoBox" Then
        oControl.Font = New Font(strFontName, dDimFont, oStyle)
      ElseIf oControl.GetType.ToString = "NTSInformatica.NTSGrid" Then
        CType(CType(oControl, NTSGrid).MainView, NTSGridView).Appearance.Row.Font = New Font(strFontName, dDimFont, oStyle)
        CType(CType(oControl, NTSGrid).MainView, NTSGridView).Appearance.HeaderPanel.Font = New Font(strFontName, dDimFont, oStyle)
      Else
        oControl.Font = New Font(strFontName, oControl.Font.Size, oStyle)
      End If

      For z As Integer = 0 To oControl.Controls.Count - 1
        ImpostaFont(oControl.Controls(z), strFontName, dFont)
      Next

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

#Region "Eventi"
  Public Overridable Sub cmdEsci_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEsci.Click
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    Try
      For z As Integer = 0 To dttOmgp.Rows.Count - 1
        If NTSCDec(dttOmgp.Rows(z)!xx_quant) > NTSCDec(dttOmgp.Rows(z)!xx_maxquant) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129941603414449401, "La quantità omaggio (|" & NTSCDec(dttOmgp.Rows(z)!xx_quant) & _
                                 "|) non può superare quella proposta dalla promozione (|" & NTSCDec(dttOmgp.Rows(z)!xx_maxquant) & "|)"))
          Return
        End If

        If NTSCStr(dttOmgp.Rows(z)!xx_sel) = "S" Then
          Dim dtrRow() As DataRow
          Select Case strBlocchiOmaggio
            Case "U"
              dtrRow = dttOmgp.Select("xx_sel = 'S' AND xx_tipo = " & CStrSQL(dttOmgp.Rows(z)!xx_tipo) & _
                                                 " AND xx_codrepr <> " & NTSCInt(dttOmgp.Rows(z)!xx_codrepr))
              If dtrRow.Length > 0 Then
                oApp.MsgBoxErr(oApp.Tr(Me, 130003106903303245, "Non è possibile selezionare omaggi della promozione |" & NTSCStr(dttOmgp.Rows(z)!xx_desrepr) & vbCrLf & _
                                                               "|con omaggi della promozione |" & NTSCStr(dtrRow(0)!xx_desrepr) & "|"))
                Return
              End If
            Case "S"
              dtrRow = dttOmgp.Select("xx_sel = 'S' AND xx_codrepr = " & NTSCInt(dttOmgp.Rows(z)!xx_codrepr))

              If dtrRow.Length > 1 Then
                oApp.MsgBoxErr(oApp.Tr(Me, 130403860370660136, "Non è possibile selezionare più omaggi per la promozione |" & NTSCStr(dttOmgp.Rows(z)!xx_desrepr) & "|"))
                Return
              End If
          End Select
        End If
      Next

      oCallParams.bPar1 = True
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdSx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSx.Click
    Try
      grvOmgp.NTSMovePreviousColunn()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdSu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSu.Click
    Try
      grvOmgp.MovePrev()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdGiu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGiu.Click
    Try
      grvOmgp.MoveNext()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdDx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDx.Click
    Try
      grvOmgp.NTSMoveNextColunn()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region
End Class

