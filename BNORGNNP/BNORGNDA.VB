Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORGNDA
#Region "Variabili"
  Public oCleGnnp As CLEORGNNP
  Public oCallParams As CLE__CLDP
  Public dsGnda As New DataSet
  Public dcGnda As New BindingSource()
  Public components As System.ComponentModel.IContainer

  Public WithEvents grGnda As NTSInformatica.NTSGrid
  Public WithEvents grvGnda As NTSInformatica.NTSGridView
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents dn_codart As NTSInformatica.NTSGridColumn
  Public WithEvents dn_descr As NTSInformatica.NTSGridColumn
  Public WithEvents dn_dispnet As NTSInformatica.NTSGridColumn
  Public WithEvents dn_qtaass As NTSInformatica.NTSGridColumn
  Public WithEvents xx_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents xx_lottox As NTSInformatica.NTSGridColumn
  Public WithEvents xx_lotto As NTSInformatica.NTSGridColumn
  Public WithEvents cmdModifica As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents dn_fase As NTSInformatica.NTSGridColumn
  Public WithEvents dn_tcindtagl As NTSInformatica.NTSGridColumn
  Public WithEvents dn_tctaglia As NTSInformatica.NTSGridColumn
  Public WithEvents pnFill As NTSInformatica.NTSPanel
  Public WithEvents pnRight As NTSInformatica.NTSPanel
  Public WithEvents dn_commeca As NTSInformatica.NTSGridColumn
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


    Return True
  End Function
  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMORGNDA))
    Me.grGnda = New NTSInformatica.NTSGrid
    Me.grvGnda = New NTSInformatica.NTSGridView
    Me.dn_codart = New NTSInformatica.NTSGridColumn
    Me.dn_descr = New NTSInformatica.NTSGridColumn
    Me.dn_dispnet = New NTSInformatica.NTSGridColumn
    Me.dn_qtaass = New NTSInformatica.NTSGridColumn
    Me.dn_commeca = New NTSInformatica.NTSGridColumn
    Me.xx_commeca = New NTSInformatica.NTSGridColumn
    Me.xx_lottox = New NTSInformatica.NTSGridColumn
    Me.xx_lotto = New NTSInformatica.NTSGridColumn
    Me.dn_fase = New NTSInformatica.NTSGridColumn
    Me.dn_tcindtagl = New NTSInformatica.NTSGridColumn
    Me.dn_tctaglia = New NTSInformatica.NTSGridColumn
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.cmdModifica = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.pnRight = New NTSInformatica.NTSPanel
    Me.pnFill = New NTSInformatica.NTSPanel
    CType(Me.grGnda, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvGnda, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnRight.SuspendLayout()
    CType(Me.pnFill, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFill.SuspendLayout()
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
    'DevXDefaultLookAndFeel
    '

    '
    'grGnda
    '
    Me.grGnda.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grGnda.EmbeddedNavigator.Name = ""
    Me.grGnda.Location = New System.Drawing.Point(0, 0)
    Me.grGnda.MainView = Me.grvGnda
    Me.grGnda.Name = "grGnda"
    Me.grGnda.Size = New System.Drawing.Size(566, 340)
    Me.grGnda.TabIndex = 0
    Me.grGnda.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvGnda})
    '
    'grvGnda
    '
    Me.grvGnda.ActiveFilterEnabled = False
    Me.grvGnda.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.dn_codart, Me.dn_descr, Me.dn_dispnet, Me.dn_qtaass, Me.dn_commeca, Me.xx_commeca, Me.xx_lottox, Me.xx_lotto, Me.dn_fase, Me.dn_tcindtagl, Me.dn_tctaglia})
    Me.grvGnda.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvGnda.Enabled = True
    Me.grvGnda.GridControl = Me.grGnda
    Me.grvGnda.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvGnda.Name = "grvGnda"
    Me.grvGnda.NTSAllowDelete = True
    Me.grvGnda.NTSAllowInsert = True
    Me.grvGnda.NTSAllowUpdate = True
    Me.grvGnda.NTSMenuContext = Nothing
    Me.grvGnda.OptionsCustomization.AllowRowSizing = True
    Me.grvGnda.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvGnda.OptionsNavigation.UseTabKey = False
    Me.grvGnda.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvGnda.OptionsView.ColumnAutoWidth = False
    Me.grvGnda.OptionsView.EnableAppearanceEvenRow = True
    Me.grvGnda.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvGnda.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvGnda.OptionsView.ShowGroupPanel = False
    Me.grvGnda.RowHeight = 16
    '
    'dn_codart
    '
    Me.dn_codart.AppearanceCell.Options.UseBackColor = True
    Me.dn_codart.AppearanceCell.Options.UseTextOptions = True
    Me.dn_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.dn_codart.Caption = "Cod. articolo"
    Me.dn_codart.Enabled = True
    Me.dn_codart.FieldName = "dn_codart"
    Me.dn_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.dn_codart.Name = "dn_codart"
    Me.dn_codart.NTSRepositoryComboBox = Nothing
    Me.dn_codart.NTSRepositoryItemCheck = Nothing
    Me.dn_codart.NTSRepositoryItemText = Nothing
    Me.dn_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.dn_codart.OptionsFilter.AllowFilter = False
    Me.dn_codart.Visible = True
    Me.dn_codart.VisibleIndex = 0
    '
    'dn_descr
    '
    Me.dn_descr.AppearanceCell.Options.UseBackColor = True
    Me.dn_descr.AppearanceCell.Options.UseTextOptions = True
    Me.dn_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.dn_descr.Caption = "Descrizione"
    Me.dn_descr.Enabled = True
    Me.dn_descr.FieldName = "dn_descr"
    Me.dn_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.dn_descr.Name = "dn_descr"
    Me.dn_descr.NTSRepositoryComboBox = Nothing
    Me.dn_descr.NTSRepositoryItemCheck = Nothing
    Me.dn_descr.NTSRepositoryItemText = Nothing
    Me.dn_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.dn_descr.OptionsFilter.AllowFilter = False
    Me.dn_descr.Visible = True
    Me.dn_descr.VisibleIndex = 1
    '
    'dn_dispnet
    '
    Me.dn_dispnet.AppearanceCell.Options.UseBackColor = True
    Me.dn_dispnet.AppearanceCell.Options.UseTextOptions = True
    Me.dn_dispnet.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.dn_dispnet.Caption = "Disponibilità netta"
    Me.dn_dispnet.Enabled = True
    Me.dn_dispnet.FieldName = "dn_dispnet"
    Me.dn_dispnet.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.dn_dispnet.Name = "dn_dispnet"
    Me.dn_dispnet.NTSRepositoryComboBox = Nothing
    Me.dn_dispnet.NTSRepositoryItemCheck = Nothing
    Me.dn_dispnet.NTSRepositoryItemText = Nothing
    Me.dn_dispnet.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.dn_dispnet.OptionsFilter.AllowFilter = False
    Me.dn_dispnet.Visible = True
    Me.dn_dispnet.VisibleIndex = 2
    '
    'dn_qtaass
    '
    Me.dn_qtaass.AppearanceCell.Options.UseBackColor = True
    Me.dn_qtaass.AppearanceCell.Options.UseTextOptions = True
    Me.dn_qtaass.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.dn_qtaass.Caption = "Q.tà assegnata"
    Me.dn_qtaass.Enabled = True
    Me.dn_qtaass.FieldName = "dn_qtaass"
    Me.dn_qtaass.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.dn_qtaass.Name = "dn_qtaass"
    Me.dn_qtaass.NTSRepositoryComboBox = Nothing
    Me.dn_qtaass.NTSRepositoryItemCheck = Nothing
    Me.dn_qtaass.NTSRepositoryItemText = Nothing
    Me.dn_qtaass.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.dn_qtaass.OptionsFilter.AllowFilter = False
    Me.dn_qtaass.Visible = True
    Me.dn_qtaass.VisibleIndex = 3
    '
    'dn_commeca
    '
    Me.dn_commeca.AppearanceCell.Options.UseBackColor = True
    Me.dn_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.dn_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.dn_commeca.Caption = "Commessa"
    Me.dn_commeca.Enabled = True
    Me.dn_commeca.FieldName = "dn_commeca"
    Me.dn_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.dn_commeca.Name = "dn_commeca"
    Me.dn_commeca.NTSRepositoryComboBox = Nothing
    Me.dn_commeca.NTSRepositoryItemCheck = Nothing
    Me.dn_commeca.NTSRepositoryItemText = Nothing
    Me.dn_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.dn_commeca.OptionsFilter.AllowFilter = False
    Me.dn_commeca.Visible = True
    Me.dn_commeca.VisibleIndex = 4
    '
    'xx_commeca
    '
    Me.xx_commeca.AppearanceCell.Options.UseBackColor = True
    Me.xx_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.xx_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_commeca.Caption = "Descr. commessa"
    Me.xx_commeca.Enabled = True
    Me.xx_commeca.FieldName = "xx_commeca"
    Me.xx_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_commeca.Name = "xx_commeca"
    Me.xx_commeca.NTSRepositoryComboBox = Nothing
    Me.xx_commeca.NTSRepositoryItemCheck = Nothing
    Me.xx_commeca.NTSRepositoryItemText = Nothing
    Me.xx_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_commeca.OptionsFilter.AllowFilter = False
    Me.xx_commeca.Visible = True
    Me.xx_commeca.VisibleIndex = 5
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
    Me.xx_lottox.NTSRepositoryItemText = Nothing
    Me.xx_lottox.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_lottox.OptionsFilter.AllowFilter = False
    Me.xx_lottox.Visible = True
    Me.xx_lottox.VisibleIndex = 6
    '
    'xx_lotto
    '
    Me.xx_lotto.AppearanceCell.Options.UseBackColor = True
    Me.xx_lotto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_lotto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_lotto.Caption = "Descr. lotto"
    Me.xx_lotto.Enabled = True
    Me.xx_lotto.FieldName = "xx_lotto"
    Me.xx_lotto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_lotto.Name = "xx_lotto"
    Me.xx_lotto.NTSRepositoryComboBox = Nothing
    Me.xx_lotto.NTSRepositoryItemCheck = Nothing
    Me.xx_lotto.NTSRepositoryItemText = Nothing
    Me.xx_lotto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_lotto.OptionsFilter.AllowFilter = False
    Me.xx_lotto.Visible = True
    Me.xx_lotto.VisibleIndex = 7
    '
    'dn_fase
    '
    Me.dn_fase.AppearanceCell.Options.UseBackColor = True
    Me.dn_fase.AppearanceCell.Options.UseTextOptions = True
    Me.dn_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.dn_fase.Caption = "Fase"
    Me.dn_fase.Enabled = True
    Me.dn_fase.FieldName = "dn_fase"
    Me.dn_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.dn_fase.Name = "dn_fase"
    Me.dn_fase.NTSRepositoryComboBox = Nothing
    Me.dn_fase.NTSRepositoryItemCheck = Nothing
    Me.dn_fase.NTSRepositoryItemText = Nothing
    Me.dn_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.dn_fase.OptionsFilter.AllowFilter = False
    Me.dn_fase.Visible = True
    Me.dn_fase.VisibleIndex = 8
    '
    'dn_tcindtagl
    '
    Me.dn_tcindtagl.AppearanceCell.Options.UseBackColor = True
    Me.dn_tcindtagl.AppearanceCell.Options.UseTextOptions = True
    Me.dn_tcindtagl.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.dn_tcindtagl.Caption = "Taglia"
    Me.dn_tcindtagl.Enabled = True
    Me.dn_tcindtagl.FieldName = "dn_tcindtagl"
    Me.dn_tcindtagl.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.dn_tcindtagl.Name = "dn_tcindtagl"
    Me.dn_tcindtagl.NTSRepositoryComboBox = Nothing
    Me.dn_tcindtagl.NTSRepositoryItemCheck = Nothing
    Me.dn_tcindtagl.NTSRepositoryItemText = Nothing
    Me.dn_tcindtagl.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.dn_tcindtagl.OptionsFilter.AllowFilter = False
    Me.dn_tcindtagl.Visible = True
    Me.dn_tcindtagl.VisibleIndex = 9
    '
    'dn_tctaglia
    '
    Me.dn_tctaglia.AppearanceCell.Options.UseBackColor = True
    Me.dn_tctaglia.AppearanceCell.Options.UseTextOptions = True
    Me.dn_tctaglia.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.dn_tctaglia.Caption = "Descr. taglia"
    Me.dn_tctaglia.Enabled = True
    Me.dn_tctaglia.FieldName = "dn_tctaglia"
    Me.dn_tctaglia.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.dn_tctaglia.Name = "dn_tctaglia"
    Me.dn_tctaglia.NTSRepositoryComboBox = Nothing
    Me.dn_tctaglia.NTSRepositoryItemCheck = Nothing
    Me.dn_tctaglia.NTSRepositoryItemText = Nothing
    Me.dn_tctaglia.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.dn_tctaglia.OptionsFilter.AllowFilter = False
    Me.dn_tctaglia.Visible = True
    Me.dn_tctaglia.VisibleIndex = 10
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 16
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
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
    'cmdModifica
    '
    Me.cmdModifica.Location = New System.Drawing.Point(10, 16)
    Me.cmdModifica.Name = "cmdModifica"
    Me.cmdModifica.Size = New System.Drawing.Size(82, 22)
    Me.cmdModifica.TabIndex = 5
    Me.cmdModifica.Text = "&Modifica"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Location = New System.Drawing.Point(10, 45)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(82, 22)
    Me.cmdAnnulla.TabIndex = 4
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'pnRight
    '
    Me.pnRight.AllowDrop = True
    Me.pnRight.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnRight.Appearance.Options.UseBackColor = True
    Me.pnRight.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnRight.Controls.Add(Me.cmdModifica)
    Me.pnRight.Controls.Add(Me.cmdAnnulla)
    Me.pnRight.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnRight.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnRight.Location = New System.Drawing.Point(566, 26)
    Me.pnRight.Name = "pnRight"
    Me.pnRight.Size = New System.Drawing.Size(101, 340)
    Me.pnRight.TabIndex = 6
    Me.pnRight.Text = "NtsPanel1"
    '
    'pnFill
    '
    Me.pnFill.AllowDrop = True
    Me.pnFill.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnFill.Appearance.Options.UseBackColor = True
    Me.pnFill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnFill.Controls.Add(Me.grGnda)
    Me.pnFill.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnFill.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnFill.Location = New System.Drawing.Point(0, 26)
    Me.pnFill.Name = "pnFill"
    Me.pnFill.Size = New System.Drawing.Size(566, 340)
    Me.pnFill.TabIndex = 7
    Me.pnFill.Text = "NtsPanel1"
    '
    'FRMORGNDA
    '
    Me.ClientSize = New System.Drawing.Size(667, 366)
    Me.Controls.Add(Me.pnFill)
    Me.Controls.Add(Me.pnRight)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMORGNDA"
    Me.NTSLastControlFocussed = Me.grGnda
    Me.Text = "MODIFICA MANUALE EVASIONE"
    CType(Me.grGnda, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvGnda, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnRight.ResumeLayout(False)
    CType(Me.pnFill, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFill.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        '  'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvGnda.NTSSetParam(oMenu, oApp.Tr(Me, 128725969214494719, "Modifica manuale evasione"))
      dn_codart.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128726259753611717, "Cod. articolo"), tabartico, True)
      dn_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128726259753767969, "Descrizione"), 40, True)
      dn_dispnet.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128726259753924221, "Disponibilità netta"), "#,##0.00", 15)
      dn_qtaass.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128726259754080473, "Q.tà assegnata"), "#,##0.00", 15)
      dn_commeca.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128726259754236725, "Commessa"), tabcommess)
      xx_commeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128726793800511408, "Descr. commessa"), 0, True)
      xx_lottox.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128726259754705481, "Lotto"), "0", 9, 0, 999999999)
      xx_lotto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128726793800848914, "Descr. lotto"), 0, True)
      dn_fase.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128726259755017985, "Fase"), tabartfasi)
      dn_fase.ArtiPerFase = dn_codart
      dn_tcindtagl.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128726259755174237, "Taglia"), tabtagl)
      dn_tctaglia.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128726259755330489, "Descr. taglia"), 5, False)

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
  Public Overridable Sub InitEntity(ByRef cleGnnp As CLEORGNNP)
    oCleGnnp = cleGnnp
    AddHandler oCleGnnp.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

#Region "EventiForm"
  Public Overridable Sub FRMORGNDA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleGnnp.GndaApri(DittaCorrente, dsGnda) Then Me.Close()

      dcGnda.DataSource = dsGnda.Tables("TTDISPNET")
      grGnda.DataSource = dcGnda

      grvGnda.NTSAllowInsert = False
      grvGnda.NTSAllowUpdate = False
      grvGnda.NTSAllowDelete = False
      grvGnda.Enabled = False

      If dsGnda.Tables("TTDISPNET").Rows.Count = 0 Then
        cmdModifica.Enabled = False
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
  Public Overridable Sub FRMORGNDA_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcGnda.Dispose()
      dsGnda.Dispose()
    Catch
    End Try
  End Sub

  Public Overridable Sub cmdModifica_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdModifica.Click
    Dim fmGndt As FRMORGNDT = Nothing
    Dim dsTmp As DataSet = Nothing
    Dim dQuantass As Decimal
    Try
      oCleGnnp.strGndtCodart = NTSCStr(dsGnda.Tables("TTDISPNET").Rows(dcGnda.Position)!dn_codart)
      oCleGnnp.dGndtDispon = NTSCDec(dsGnda.Tables("TTDISPNET").Rows(dcGnda.Position)!dn_dispnet)
      oCleGnnp.dGndtQtaass = NTSCDec(dsGnda.Tables("TTDISPNET").Rows(dcGnda.Position)!dn_qtaass)
      oCleGnnp.lGndtCommeca = NTSCInt(dsGnda.Tables("TTDISPNET").Rows(dcGnda.Position)!dn_commeca)
      oCleGnnp.lGndtLotto = NTSCInt(dsGnda.Tables("TTDISPNET").Rows(dcGnda.Position)!dn_lotto)
      oCleGnnp.nGndtFase = NTSCInt(dsGnda.Tables("TTDISPNET").Rows(dcGnda.Position)!dn_fase)
      oCleGnnp.nGndtTaglia = NTSCInt(dsGnda.Tables("TTDISPNET").Rows(dcGnda.Position)!dn_tcindtagl)

      fmGndt = CType(NTSNewFormModal("FRMORGNDT"), FRMORGNDT)
      fmGndt.Init(oMenu, Nothing, DittaCorrente)
      fmGndt.InitEntity(oCleGnnp)
      fmGndt.ShowDialog()

      With dsGnda.Tables("TTDISPNET").Rows(dcGnda.Position)
        oCleGnnp.GndaModifica(NTSCStr(!dn_codart), NTSCStr(!dn_commeca), _
                              NTSCStr(!dn_lotto), NTSCStr(!dn_fase), _
                              NTSCInt(!dn_tcindtagl), dsTmp)
      End With

      'Aggiusta la quantità assegnata
      If dsTmp.Tables("TTMOPERNP").Rows.Count = 0 Then
        dQuantass = 0
      Else
        dQuantass = NTSCDec(dsTmp.Tables("TTMOPERNP").Rows(0)!expr1)
      End If
      dsGnda.Tables("TTDISPNET").Rows(dcGnda.Position)!dn_qtaass = dQuantass
      oCleGnnp.GndaSalva(False)

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not fmGndt Is Nothing Then fmGndt.Dispose()
      fmGndt = Nothing
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      Me.Close()
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub
  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub
#End Region

End Class