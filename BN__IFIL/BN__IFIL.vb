Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__IFIL
#Region "Moduli"
  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = bsModExtAll
  Private ModuliSup_P As Integer = 0
  Private ModuliSupExt_P As Integer = 0
  Private ModuliPtn_P As Integer = 0
  Private ModuliPtnExt_P As Integer = 0

  Public ReadOnly Property Moduli() As Integer
    Get
      Return Moduli_P
    End Get
  End Property
  Public ReadOnly Property ModuliExt() As Integer
    Get
      Return ModuliExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliSup() As Integer
    Get
      Return ModuliSup_P
    End Get
  End Property
  Public ReadOnly Property ModuliSupExt() As Integer
    Get
      Return ModuliSupExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtn() As Integer
    Get
      Return ModuliPtn_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtnExt() As Integer
    Get
      Return ModuliPtnExt_P
    End Get
  End Property
#End Region

#Region "Variabili"
  Public oCleIfil As CLE__IFIL
  Public oCallParams As CLE__CLDP

  Public dsFiltri As New DataSet
  Public dcFiltri As New BindingSource
  Public dcTesta As New BindingSource

  Public dttValue As New DataTable

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents grFiltri As NTSInformatica.NTSGrid
  Public WithEvents grvFiltri As NTSInformatica.NTSGridView
  Public WithEvents mo_valore As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descr As NTSInformatica.NTSGridColumn
  Public WithEvents mo_locked As NTSInformatica.NTSGridColumn
#End Region

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__IFIL))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbDuplica = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecRestore = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.grFiltri = New NTSInformatica.NTSGrid
    Me.grvFiltri = New NTSInformatica.NTSGridView
    Me.xx_descr = New NTSInformatica.NTSGridColumn
    Me.mo_valore = New NTSInformatica.NTSGridColumn
    Me.mo_locked = New NTSInformatica.NTSGridColumn
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.fmDitta = New NTSInformatica.NTSGroupBox
    Me.lbDitta = New NTSInformatica.NTSLabel
    Me.edTb_ditte = New NTSInformatica.NTSMemoBox
    Me.opTranneD = New NTSInformatica.NTSRadioButton
    Me.opSoloD = New NTSInformatica.NTSRadioButton
    Me.opTutteD = New NTSInformatica.NTSRadioButton
    Me.fmOperatori = New NTSInformatica.NTSGroupBox
    Me.lbOp = New NTSInformatica.NTSLabel
    Me.edTb_users = New NTSInformatica.NTSMemoBox
    Me.opTranneO = New NTSInformatica.NTSRadioButton
    Me.opSoloO = New NTSInformatica.NTSRadioButton
    Me.opTuttiO = New NTSInformatica.NTSRadioButton
    Me.lbCodice = New NTSInformatica.NTSLabel
    Me.lbDescr = New NTSInformatica.NTSLabel
    Me.edTb_desifil = New NTSInformatica.NTSTextBoxStr
    Me.edTb_codifil = New NTSInformatica.NTSTextBoxNum
    Me.pnBottom = New NTSInformatica.NTSPanel
    Me.lbInfo = New NTSInformatica.NTSLabel
    Me.cbData = New NTSInformatica.NTSComboBox
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grFiltri, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvFiltri, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.fmDitta, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmDitta.SuspendLayout()
    CType(Me.edTb_ditte.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opTranneD.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opSoloD.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opTutteD.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmOperatori, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmOperatori.SuspendLayout()
    CType(Me.edTb_users.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opTranneO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opSoloO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opTuttiO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_desifil.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codifil.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBottom.SuspendLayout()
    CType(Me.cbData.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbGuida, Me.tlbEsci, Me.tlbImpostaStampante, Me.tlbDuplica, Me.tlbSalva, Me.tlbRipristina, Me.tlbCancella, Me.tlbRecRestore})
    Me.NtsBarManager1.MaxItemId = 34
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDuplica), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecRestore, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbDuplica
    '
    Me.tlbDuplica.Caption = "Duplica"
    Me.tlbDuplica.Glyph = CType(resources.GetObject("tlbDuplica.Glyph"), System.Drawing.Image)
    Me.tlbDuplica.Id = 29
    Me.tlbDuplica.Name = "tlbDuplica"
    Me.tlbDuplica.Visible = True
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.Id = 30
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.Id = 31
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.Id = 32
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbRecRestore
    '
    Me.tlbRecRestore.Caption = "Ripristina riga"
    Me.tlbRecRestore.Glyph = CType(resources.GetObject("tlbRecRestore.Glyph"), System.Drawing.Image)
    Me.tlbRecRestore.Id = 33
    Me.tlbRecRestore.Name = "tlbRecRestore"
    Me.tlbRecRestore.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 18
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 19
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta stampante"
    Me.tlbImpostaStampante.Id = 25
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'grFiltri
    '
    Me.grFiltri.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grFiltri.EmbeddedNavigator.Name = ""
    Me.grFiltri.Location = New System.Drawing.Point(0, 169)
    Me.grFiltri.MainView = Me.grvFiltri
    Me.grFiltri.Name = "grFiltri"
    Me.grFiltri.Size = New System.Drawing.Size(687, 308)
    Me.grFiltri.TabIndex = 6
    Me.grFiltri.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvFiltri})
    '
    'grvFiltri
    '
    Me.grvFiltri.ActiveFilterEnabled = False
    Me.grvFiltri.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_descr, Me.mo_valore, Me.mo_locked})
    Me.grvFiltri.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvFiltri.Enabled = True
    Me.grvFiltri.GridControl = Me.grFiltri
    Me.grvFiltri.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvFiltri.Name = "grvFiltri"
    Me.grvFiltri.NTSAllowDelete = True
    Me.grvFiltri.NTSAllowInsert = True
    Me.grvFiltri.NTSAllowUpdate = True
    Me.grvFiltri.NTSMenuContext = Nothing
    Me.grvFiltri.OptionsCustomization.AllowRowSizing = True
    Me.grvFiltri.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvFiltri.OptionsNavigation.UseTabKey = False
    Me.grvFiltri.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvFiltri.OptionsView.ColumnAutoWidth = False
    Me.grvFiltri.OptionsView.EnableAppearanceEvenRow = True
    Me.grvFiltri.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvFiltri.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvFiltri.OptionsView.ShowGroupPanel = False
    Me.grvFiltri.RowHeight = 16
    '
    'xx_descr
    '
    Me.xx_descr.AppearanceCell.Options.UseBackColor = True
    Me.xx_descr.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descr.Caption = "Descrizione"
    Me.xx_descr.Enabled = False
    Me.xx_descr.FieldName = "xx_descr"
    Me.xx_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descr.Name = "xx_descr"
    Me.xx_descr.NTSRepositoryComboBox = Nothing
    Me.xx_descr.NTSRepositoryItemCheck = Nothing
    Me.xx_descr.NTSRepositoryItemMemo = Nothing
    Me.xx_descr.NTSRepositoryItemText = Nothing
    Me.xx_descr.OptionsColumn.AllowEdit = False
    Me.xx_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descr.OptionsColumn.ReadOnly = True
    Me.xx_descr.OptionsFilter.AllowFilter = False
    Me.xx_descr.Visible = True
    Me.xx_descr.VisibleIndex = 0
    Me.xx_descr.Width = 150
    '
    'mo_valore
    '
    Me.mo_valore.AppearanceCell.Options.UseBackColor = True
    Me.mo_valore.AppearanceCell.Options.UseTextOptions = True
    Me.mo_valore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_valore.Caption = "Valore"
    Me.mo_valore.Enabled = True
    Me.mo_valore.FieldName = "mo_valore"
    Me.mo_valore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_valore.Name = "mo_valore"
    Me.mo_valore.NTSRepositoryComboBox = Nothing
    Me.mo_valore.NTSRepositoryItemCheck = Nothing
    Me.mo_valore.NTSRepositoryItemMemo = Nothing
    Me.mo_valore.NTSRepositoryItemText = Nothing
    Me.mo_valore.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_valore.OptionsFilter.AllowFilter = False
    Me.mo_valore.Visible = True
    Me.mo_valore.VisibleIndex = 1
    Me.mo_valore.Width = 150
    '
    'mo_locked
    '
    Me.mo_locked.AppearanceCell.Options.UseBackColor = True
    Me.mo_locked.AppearanceCell.Options.UseTextOptions = True
    Me.mo_locked.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_locked.Caption = "Bloccato"
    Me.mo_locked.Enabled = True
    Me.mo_locked.FieldName = "mo_locked"
    Me.mo_locked.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_locked.Name = "mo_locked"
    Me.mo_locked.NTSRepositoryComboBox = Nothing
    Me.mo_locked.NTSRepositoryItemCheck = Nothing
    Me.mo_locked.NTSRepositoryItemMemo = Nothing
    Me.mo_locked.NTSRepositoryItemText = Nothing
    Me.mo_locked.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_locked.OptionsFilter.AllowFilter = False
    Me.mo_locked.Visible = True
    Me.mo_locked.VisibleIndex = 2
    Me.mo_locked.Width = 70
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.fmDitta)
    Me.pnTop.Controls.Add(Me.fmOperatori)
    Me.pnTop.Controls.Add(Me.lbCodice)
    Me.pnTop.Controls.Add(Me.lbDescr)
    Me.pnTop.Controls.Add(Me.edTb_desifil)
    Me.pnTop.Controls.Add(Me.edTb_codifil)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.Size = New System.Drawing.Size(687, 139)
    Me.pnTop.TabIndex = 7
    Me.pnTop.Text = "NtsPanel1"
    '
    'fmDitta
    '
    Me.fmDitta.AllowDrop = True
    Me.fmDitta.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.fmDitta.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmDitta.Appearance.Options.UseBackColor = True
    Me.fmDitta.Controls.Add(Me.lbDitta)
    Me.fmDitta.Controls.Add(Me.edTb_ditte)
    Me.fmDitta.Controls.Add(Me.opTranneD)
    Me.fmDitta.Controls.Add(Me.opSoloD)
    Me.fmDitta.Controls.Add(Me.opTutteD)
    Me.fmDitta.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmDitta.Location = New System.Drawing.Point(345, 32)
    Me.fmDitta.Name = "fmDitta"
    Me.fmDitta.Size = New System.Drawing.Size(330, 101)
    Me.fmDitta.TabIndex = 5
    Me.fmDitta.Text = "Ditta"
    '
    'lbDitta
    '
    Me.lbDitta.AutoSize = True
    Me.lbDitta.BackColor = System.Drawing.Color.Transparent
    Me.lbDitta.Location = New System.Drawing.Point(95, 26)
    Me.lbDitta.Name = "lbDitta"
    Me.lbDitta.NTSDbField = ""
    Me.lbDitta.Size = New System.Drawing.Size(127, 13)
    Me.lbDitta.TabIndex = 8
    Me.lbDitta.Text = "Nome ditte separate da ;"
    Me.lbDitta.UseMnemonic = False
    '
    'edTb_ditte
    '
    Me.edTb_ditte.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edTb_ditte.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_ditte.Location = New System.Drawing.Point(98, 47)
    Me.edTb_ditte.Name = "edTb_ditte"
    Me.edTb_ditte.NTSDbField = ""
    Me.edTb_ditte.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_ditte.Size = New System.Drawing.Size(227, 46)
    Me.edTb_ditte.TabIndex = 7
    '
    'opTranneD
    '
    Me.opTranneD.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTranneD.Location = New System.Drawing.Point(5, 74)
    Me.opTranneD.Name = "opTranneD"
    Me.opTranneD.NTSCheckValue = "S"
    Me.opTranneD.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTranneD.Properties.Appearance.Options.UseBackColor = True
    Me.opTranneD.Properties.Caption = "Tutte tranne:"
    Me.opTranneD.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTranneD.Size = New System.Drawing.Size(89, 19)
    Me.opTranneD.TabIndex = 3
    '
    'opSoloD
    '
    Me.opSoloD.Cursor = System.Windows.Forms.Cursors.Default
    Me.opSoloD.Location = New System.Drawing.Point(5, 49)
    Me.opSoloD.Name = "opSoloD"
    Me.opSoloD.NTSCheckValue = "S"
    Me.opSoloD.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opSoloD.Properties.Appearance.Options.UseBackColor = True
    Me.opSoloD.Properties.Caption = "Solo:"
    Me.opSoloD.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opSoloD.Size = New System.Drawing.Size(53, 19)
    Me.opSoloD.TabIndex = 2
    '
    'opTutteD
    '
    Me.opTutteD.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTutteD.EditValue = True
    Me.opTutteD.Location = New System.Drawing.Point(5, 24)
    Me.opTutteD.Name = "opTutteD"
    Me.opTutteD.NTSCheckValue = "S"
    Me.opTutteD.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTutteD.Properties.Appearance.Options.UseBackColor = True
    Me.opTutteD.Properties.Caption = "Tutte"
    Me.opTutteD.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTutteD.Size = New System.Drawing.Size(53, 19)
    Me.opTutteD.TabIndex = 1
    '
    'fmOperatori
    '
    Me.fmOperatori.AllowDrop = True
    Me.fmOperatori.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.fmOperatori.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmOperatori.Appearance.Options.UseBackColor = True
    Me.fmOperatori.Controls.Add(Me.lbOp)
    Me.fmOperatori.Controls.Add(Me.edTb_users)
    Me.fmOperatori.Controls.Add(Me.opTranneO)
    Me.fmOperatori.Controls.Add(Me.opSoloO)
    Me.fmOperatori.Controls.Add(Me.opTuttiO)
    Me.fmOperatori.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmOperatori.Location = New System.Drawing.Point(9, 32)
    Me.fmOperatori.Name = "fmOperatori"
    Me.fmOperatori.Size = New System.Drawing.Size(330, 101)
    Me.fmOperatori.TabIndex = 4
    Me.fmOperatori.Text = "Operatori"
    '
    'lbOp
    '
    Me.lbOp.AutoSize = True
    Me.lbOp.BackColor = System.Drawing.Color.Transparent
    Me.lbOp.Location = New System.Drawing.Point(94, 26)
    Me.lbOp.Name = "lbOp"
    Me.lbOp.NTSDbField = ""
    Me.lbOp.Size = New System.Drawing.Size(145, 13)
    Me.lbOp.TabIndex = 6
    Me.lbOp.Text = "Nome operatori separati da ;"
    Me.lbOp.UseMnemonic = False
    '
    'edTb_users
    '
    Me.edTb_users.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edTb_users.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_users.Location = New System.Drawing.Point(97, 47)
    Me.edTb_users.Name = "edTb_users"
    Me.edTb_users.NTSDbField = ""
    Me.edTb_users.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_users.Size = New System.Drawing.Size(228, 46)
    Me.edTb_users.TabIndex = 3
    '
    'opTranneO
    '
    Me.opTranneO.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTranneO.Location = New System.Drawing.Point(6, 75)
    Me.opTranneO.Name = "opTranneO"
    Me.opTranneO.NTSCheckValue = "S"
    Me.opTranneO.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTranneO.Properties.Appearance.Options.UseBackColor = True
    Me.opTranneO.Properties.Caption = "Tutti tranne:"
    Me.opTranneO.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTranneO.Size = New System.Drawing.Size(85, 19)
    Me.opTranneO.TabIndex = 2
    '
    'opSoloO
    '
    Me.opSoloO.Cursor = System.Windows.Forms.Cursors.Hand
    Me.opSoloO.Location = New System.Drawing.Point(6, 49)
    Me.opSoloO.Name = "opSoloO"
    Me.opSoloO.NTSCheckValue = "S"
    Me.opSoloO.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opSoloO.Properties.Appearance.Options.UseBackColor = True
    Me.opSoloO.Properties.Caption = "Solo:"
    Me.opSoloO.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opSoloO.Size = New System.Drawing.Size(48, 19)
    Me.opSoloO.TabIndex = 1
    '
    'opTuttiO
    '
    Me.opTuttiO.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTuttiO.EditValue = True
    Me.opTuttiO.Location = New System.Drawing.Point(6, 24)
    Me.opTuttiO.Name = "opTuttiO"
    Me.opTuttiO.NTSCheckValue = "S"
    Me.opTuttiO.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTuttiO.Properties.Appearance.Options.UseBackColor = True
    Me.opTuttiO.Properties.Caption = "Tutti"
    Me.opTuttiO.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTuttiO.Size = New System.Drawing.Size(48, 19)
    Me.opTuttiO.TabIndex = 0
    '
    'lbCodice
    '
    Me.lbCodice.AutoSize = True
    Me.lbCodice.BackColor = System.Drawing.Color.Transparent
    Me.lbCodice.Location = New System.Drawing.Point(15, 9)
    Me.lbCodice.Name = "lbCodice"
    Me.lbCodice.NTSDbField = ""
    Me.lbCodice.Size = New System.Drawing.Size(30, 13)
    Me.lbCodice.TabIndex = 3
    Me.lbCodice.Text = "Cod."
    Me.lbCodice.UseMnemonic = False
    '
    'lbDescr
    '
    Me.lbDescr.AutoSize = True
    Me.lbDescr.BackColor = System.Drawing.Color.Transparent
    Me.lbDescr.Location = New System.Drawing.Point(106, 9)
    Me.lbDescr.Name = "lbDescr"
    Me.lbDescr.NTSDbField = ""
    Me.lbDescr.Size = New System.Drawing.Size(61, 13)
    Me.lbDescr.TabIndex = 2
    Me.lbDescr.Text = "Descrizione"
    Me.lbDescr.UseMnemonic = False
    '
    'edTb_desifil
    '
    Me.edTb_desifil.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_desifil.Location = New System.Drawing.Point(173, 6)
    Me.edTb_desifil.Name = "edTb_desifil"
    Me.edTb_desifil.NTSDbField = ""
    Me.edTb_desifil.NTSForzaVisZoom = False
    Me.edTb_desifil.NTSOldValue = ""
    Me.edTb_desifil.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_desifil.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_desifil.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_desifil.Properties.MaxLength = 65536
    Me.edTb_desifil.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_desifil.Size = New System.Drawing.Size(266, 20)
    Me.edTb_desifil.TabIndex = 1
    '
    'edTb_codifil
    '
    Me.edTb_codifil.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codifil.Enabled = False
    Me.edTb_codifil.Location = New System.Drawing.Point(51, 6)
    Me.edTb_codifil.Name = "edTb_codifil"
    Me.edTb_codifil.NTSDbField = ""
    Me.edTb_codifil.NTSFormat = "0"
    Me.edTb_codifil.NTSForzaVisZoom = False
    Me.edTb_codifil.NTSOldValue = ""
    Me.edTb_codifil.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codifil.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codifil.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codifil.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codifil.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codifil.Properties.MaxLength = 65536
    Me.edTb_codifil.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codifil.Size = New System.Drawing.Size(49, 20)
    Me.edTb_codifil.TabIndex = 0
    '
    'pnBottom
    '
    Me.pnBottom.AllowDrop = True
    Me.pnBottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBottom.Appearance.Options.UseBackColor = True
    Me.pnBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBottom.Controls.Add(Me.lbInfo)
    Me.pnBottom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnBottom.Location = New System.Drawing.Point(0, 477)
    Me.pnBottom.Name = "pnBottom"
    Me.pnBottom.Size = New System.Drawing.Size(687, 23)
    Me.pnBottom.TabIndex = 8
    Me.pnBottom.Text = "NtsPanel1"
    '
    'lbInfo
    '
    Me.lbInfo.AutoSize = True
    Me.lbInfo.BackColor = System.Drawing.Color.Transparent
    Me.lbInfo.Location = New System.Drawing.Point(4, 3)
    Me.lbInfo.Name = "lbInfo"
    Me.lbInfo.NTSDbField = ""
    Me.lbInfo.Size = New System.Drawing.Size(31, 13)
    Me.lbInfo.TabIndex = 0
    Me.lbInfo.Text = "Info:"
    Me.lbInfo.UseMnemonic = False
    '
    'cbData
    '
    Me.cbData.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbData.DataSource = Nothing
    Me.cbData.DisplayMember = ""
    Me.cbData.Location = New System.Drawing.Point(550, 222)
    Me.cbData.Name = "cbData"
    Me.cbData.NTSDbField = ""
    Me.cbData.Properties.AutoComplete = False
    Me.cbData.Properties.AutoHeight = False
    Me.cbData.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbData.Properties.DropDownRows = 30
    Me.cbData.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbData.SelectedValue = ""
    Me.cbData.Size = New System.Drawing.Size(100, 20)
    Me.cbData.TabIndex = 9
    Me.cbData.ValueMember = ""
    Me.cbData.Visible = False
    '
    'FRM__IFIL
    '
    Me.ClientSize = New System.Drawing.Size(687, 500)
    Me.Controls.Add(Me.cbData)
    Me.Controls.Add(Me.grFiltri)
    Me.Controls.Add(Me.pnBottom)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRM__IFIL"
    Me.Text = "IMPOSTAZIONE FILTRI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grFiltri, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvFiltri, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.fmDitta, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmDitta.ResumeLayout(False)
    Me.fmDitta.PerformLayout()
    CType(Me.edTb_ditte.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opTranneD.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opSoloD.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opTutteD.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmOperatori, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmOperatori.ResumeLayout(False)
    Me.fmOperatori.PerformLayout()
    CType(Me.edTb_users.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opTranneO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opSoloO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opTuttiO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_desifil.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codifil.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBottom.ResumeLayout(False)
    Me.pnBottom.PerformLayout()
    CType(Me.cbData.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    'Me.MinimumSize = Me.Size

    '------------------------------------------------
    'creo e attivo l'entity e inizializzo la funzione che dovr√† rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__IFIL", "BE__IFIL", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128534975466065282, "ERRORE in fase di creazione Entity:" & vbCrLf) & strErr)
      Return False
    End If
    oCleIfil = CType(oTmp, CLE__IFIL)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BE__IFIL", strRemoteServer, strRemotePort)
    AddHandler oCleIfil.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleIfil.Init(oApp, oScript, oMenu.oCleComm, "TEIFILD", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim i As Integer = 0
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbDuplica.GlyphPath = (oApp.ChildImageDir & "\duplica.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRecRestore.GlyphPath = (oApp.ChildImageDir & "\recrestore.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'Ë una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      '-------------------------------------------------
      'completo le informazioni dei controlli
      grvFiltri.NTSSetParam(oMenu, oApp.Tr(Me, 129181480581132667, "Filtri"))
      xx_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129181480581288917, "Descrizione"), 0, True)
      mo_valore.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129181480581445167, "Valore"), 0, False)
      mo_locked.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129181480581601417, "Bloccato"), "S", "N")
      edTb_ditte.NTSSetParam(oMenu, oApp.Tr(Me, 129181480585351417, "Ditte"), 255)
      opTranneD.NTSSetParam(oMenu, oApp.Tr(Me, 129181480585820167, "Tutte tranne:"), "N")
      opSoloD.NTSSetParam(oMenu, oApp.Tr(Me, 129181480585976417, "Solo:"), "S")
      opTutteD.NTSSetParam(oMenu, oApp.Tr(Me, 129181480586132667, "Tutte"), " ")
      edTb_users.NTSSetParam(oMenu, oApp.Tr(Me, 129181480586288917, "Operatori"), 255)
      opTranneO.NTSSetParam(oMenu, oApp.Tr(Me, 129181480586757667, "Tutti tranne:"), "N")
      opSoloO.NTSSetParam(oMenu, oApp.Tr(Me, 129181480586913917, "Solo:"), "S")
      opTuttiO.NTSSetParam(oMenu, oApp.Tr(Me, 129181480587070167, "Tutti"), " ")
      edTb_desifil.NTSSetParam(oMenu, oApp.Tr(Me, 129181480587382667, "Descrizione"), 50)
      edTb_codifil.NTSSetParam(oMenu, oApp.Tr(Me, 129181480587538917, "Codice"), "0", 4, 0, 9999)
      cbData.NTSSetParam(oApp.Tr(Me, 129187579532187500, "Data"))
      cbData.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
      cbData.Properties.AutoComplete = False

      grvFiltri.NTSAllowDelete = False
      grvFiltri.NTSAllowInsert = False

      '-------------------------------------------------
      'chiamo lo script per inizializzare i controlli caricati con source ext
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub
  Public Overridable Sub CaricaCombo()
    Try
      '--------------------------------------------------------------------------------------------------------------
      dttValue.Columns.Add("cod", GetType(String))
      dttValue.Columns.Add("val", GetType(String))
      dttValue.Rows.Add(New Object() {"0", "Data odierna"})
      dttValue.Rows.Add(New Object() {"+1 G", "+1 giorno"})
      dttValue.Rows.Add(New Object() {"+2 G", "+2 giorni"})
      dttValue.Rows.Add(New Object() {"+3 G", "+3 giorni"})
      dttValue.Rows.Add(New Object() {"+4 G", "+4 giorni"})
      dttValue.Rows.Add(New Object() {"+5 G", "+5 giorni"})
      dttValue.Rows.Add(New Object() {"+6 G", "+6 giorni"})
      dttValue.Rows.Add(New Object() {"+7 G", "+7 giorni"})
      dttValue.Rows.Add(New Object() {"+14 G", "+14 giorni"})
      dttValue.Rows.Add(New Object() {"+21 G", "+21 giorni"})
      dttValue.Rows.Add(New Object() {"+1 M", "+1 mese"})
      dttValue.Rows.Add(New Object() {"+2 M", "+2 mesi"})
      dttValue.Rows.Add(New Object() {"+3 M", "+3 mesi"})
      dttValue.Rows.Add(New Object() {"+4 M", "+4 mesi"})
      dttValue.Rows.Add(New Object() {"+6 M", "+6 mesi"})
      dttValue.Rows.Add(New Object() {"+1 A", "+1 anno"})
      dttValue.Rows.Add(New Object() {"-1 G", "-1 giorno"})
      dttValue.Rows.Add(New Object() {"-2 G", "-2 giorni"})
      dttValue.Rows.Add(New Object() {"-3 G", "-3 giorni"})
      dttValue.Rows.Add(New Object() {"-4 G", "-4 giorni"})
      dttValue.Rows.Add(New Object() {"-5 G", "-5 giorni"})
      dttValue.Rows.Add(New Object() {"-6 G", "-6 giorni"})
      dttValue.Rows.Add(New Object() {"-7 G", "-7 giorni"})
      dttValue.Rows.Add(New Object() {"-14 G", "-14 giorni"})
      dttValue.Rows.Add(New Object() {"-21 G", "-21 giorni"})
      dttValue.Rows.Add(New Object() {"-1 M", "-1 mese"})
      dttValue.Rows.Add(New Object() {"-2 M", "-2 mesi"})
      dttValue.Rows.Add(New Object() {"-3 M", "-3 mesi"})
      dttValue.Rows.Add(New Object() {"-4 M", "-4 mesi"})
      dttValue.Rows.Add(New Object() {"-6 M", "-6 mesi"})
      dttValue.Rows.Add(New Object() {"-1 A", "-1 anno"})
      dttValue.AcceptChanges()

      cbData.DataSource = dttValue
      cbData.DisplayMember = "val"
      cbData.ValueMember = "cod"
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRM__IFIL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      If oCallParams Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129181261021718750, "Il programma non Ë stato chiamando passandogli i parametri corretti." & vbCrLf & "Impossibile continuare"))
        Me.Close()
        Return
      End If

      'In nuovo devo aver passato i parametri con la struttura dei campi altrimenti non apro
      If oCallParams.ctlPar1 Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129181419431020703, "I parametri non sono stati passati correttamente."))
        Me.Close()
        Return
      End If

      InitControls()

      CaricaCombo()

      'In ogni caso carico sempre i dati che sono stati passati.
      dsFiltri.Tables.Add(CType(oCallParams.ctlPar1, DataTable).Copy)
      dsFiltri.Tables(0).TableName = "MOVIFIL"

      dcFiltri.DataSource = dsFiltri.Tables("MOVIFIL")
      grFiltri.DataSource = dcFiltri

      If oCallParams.dPar1 = 0 Then ' 0 sono in nuovo, numero sono in apri (carica i dati di testata)
        If Not Nuovo() Then Return
      Else
        If Not Apri() Then Return
      End If

      dcTesta.DataSource = dsFiltri.Tables("TABIFIL")

      BindControls()
      '--------------------------------------------------------------------------------------------------------------
      '--- Sempre alla fine di questa funzione: applico le regole della gctl
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '---------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '---------------------------------------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub BindControls()
    Try
      '-------------------------------------------------
      'se i controlli erano gi‡† stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      edTb_ditte.NTSDbField = "TABIFIL.tb_ditte"
      opTranneD.NTSText.NTSDbField = "TABIFIL.tb_filtriditta"
      opSoloD.NTSText.NTSDbField = "TABIFIL.tb_filtriditta"
      opTutteD.NTSText.NTSDbField = "TABIFIL.tb_filtriditta"
      edTb_users.NTSDbField = "TABIFIL.tb_users"
      opTranneO.NTSText.NTSDbField = "TABIFIL.tb_filtriut"
      opSoloO.NTSText.NTSDbField = "TABIFIL.tb_filtriut"
      opTuttiO.NTSText.NTSDbField = "TABIFIL.tb_filtriut"
      edTb_desifil.NTSDbField = "TABIFIL.tb_desifil"
      edTb_codifil.NTSDbField = "TABIFIL.tb_codifil"

      '-------------------------------------------------
      'per aGiornianciare al dataset i vari controlli
      NTSFormAddDataBinding(dcTesta, Me)
    Catch ex As Exception
      '------------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '------------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Nuovo() As Boolean
    Try
      'Cerca un numero disponibile da assegnare e crea la struttura base
      If Not oCleIfil.NuovoFiltro(oCallParams.strPar1, dsFiltri) Then Return False

      For z As Integer = 0 To dsFiltri.Tables("MOVIFIL").Rows.Count - 1
        dsFiltri.Tables("MOVIFIL").Rows(z)!mo_codifil = dsFiltri.Tables("TABIFIL").Rows(0)!tb_codifil
      Next

      Return True
    Catch ex As Exception
      '------------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '------------------------------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function Apri() As Boolean
    Try
      'Carico i dati della testata
      oCleIfil.ApriFiltro(NTSCInt(oCallParams.dPar1), oCallParams.strPar1, dsFiltri)

      Return True
    Catch ex As Exception
      '------------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '------------------------------------------------------------------------------------
    End Try
  End Function

#Region "Eventi Toolbar"
  Public Overridable Sub tlbDuplica_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDuplica.ItemClick
    Try
      If Not Salva() Then Return

      If Not oCleIfil.DuplicaFiltro(dsFiltri) Then Return
    Catch ex As Exception
      '------------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '------------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva()
    Catch ex As Exception
      '------------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '------------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not oCleIfil.bHasChanges Then Return

      If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129182100590766015, "Ripristinare le modifiche?")) = Windows.Forms.DialogResult.No Then Return

    Catch ex As Exception
      '------------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '------------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Try
      If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129182101135394845, "Cancellare le impostazioni?")) = Windows.Forms.DialogResult.No Then Return

      If oCleIfil.Cancella() Then Me.Close()
    Catch ex As Exception
      '------------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '------------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRecRestore_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecRestore.ItemClick
    Try

    Catch ex As Exception
      '------------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '------------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    Try
      SendKeys.SendWait("{F1}")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi"
  Public Overridable Sub opTuttiO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opTuttiO.CheckedChanged
    Try
      edTb_users.Enabled = Not opTuttiO.Checked
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub opTutteD_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opTutteD.CheckedChanged
    Try
      edTb_ditte.Enabled = Not opTutteD.Checked
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cbData_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbData.Leave
    Try
      grvFiltri.NTSGetCurrentDataRow()!mo_valore = cbData.Text
      cbData.Visible = False
    Catch ex As Exception
      '------------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '------------------------------------------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()

      If Not oCleIfil.bHasChanges Then Return True

      If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129182101768164467, "Salvare le modifiche?")) = Windows.Forms.DialogResult.No Then Return False

      If Not oCleIfil.SalvaFiltro() Then Return False

      Return True
    Catch ex As Exception
      '------------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '------------------------------------------------------------------------------------
    End Try
  End Function

#Region "Eventi Griglia"
  Public Overridable Sub grvFiltri_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvFiltri.NTSFocusedRowChanged
    Try
      'Al cambio di tipo riga faccio variare anche le indicazioni
      With grvFiltri.NTSGetCurrentDataRow()
        Select Case NTSCStr(!xx_tipo)
          Case "NTSButton"
            lbInfo.Text = oApp.Tr(Me, 129187297764102444, "Bottone, non Ë possibile impostare nessun valore")
          Case "NTSTextBoxNum"
            lbInfo.Text = oApp.Tr(Me, 129187297773633694, "Campo numerico, sono consentiti solo numeri, la lunghezza massima del campo Ë ") & NTSCInt(!xx_info)
          Case "NTSTextBoxStr"
            lbInfo.Text = oApp.Tr(Me, 129187297780821194, "Campo di testo, Ë possibile usare qualsiasi carattere, ")
            If NTSCInt(!xx_info) = 0 Then
              lbInfo.Text &= oApp.Tr(Me, 129187312568321194, "non vi sono limiti di dimensione")
            Else
              lbInfo.Text &= oApp.Tr(Me, 129187312585977444, "la lunghezza massima del campo Ë ") & NTSCInt(!xx_info)
            End If
          Case "NTSTextBoxData"
            lbInfo.Text = oApp.Tr(Me, 129187297789883694, "Campo data, accetta date fisse, altrimenti selezionare o scrivere una data relativa dalla data di stampa (es: +15 giorni, -3 mesi, +1 anno)")
          Case "NTSMemoBox"
            lbInfo.Text = oApp.Tr(Me, 129187297849727444, "Campo di testo, Ë possibile usare qualsiasi carattere, non vi sono limiti di dimensione")
          Case "NTSCheckBox"
            lbInfo.Text = oApp.Tr(Me, 129187297825196194, "Spunta, valori accettati: -1 = selezionato, 0 = non selezionato")
          Case "NTSComboBox"
            lbInfo.Text = oApp.Tr(Me, 129187297831602444, "Combo, si consiglia di selezionare il valore da definire nella maschera precedente e di non modificarlo manualmente dalla griglia")
          Case "NTSRadioButton"
            Dim strCampi As String = ""
            Dim dtrRow() As DataRow

            dtrRow = CType(CType(grFiltri.DataSource, BindingSource).DataSource, DataTable).Select("xx_info = '" & NTSCStr(!xx_info) & "'")
            For z As Integer = 0 To dtrRow.Length - 1
              If NTSCStr(dtrRow(z)!xx_descr) <> NTSCStr(!xx_descr) Then
                strCampi &= "'" & NTSCStr(dtrRow(z)!xx_descr) & "', "
              End If
            Next
            If strCampi.Length > 0 Then strCampi = strCampi.Substring(0, strCampi.Length - 2)

            lbInfo.Text = oApp.Tr(Me, 129187297863477444, "Spunta alternativa a: |" & strCampi & "|. Valori accettati: -1 = selezionato, 0 = non selezionato")
          Case "NTSGridColumn"
            lbInfo.Text = oApp.Tr(Me, 129187297877696194, "Colonna di griglia. ")
            Select Case NTSCInt(!xx_info)
              Case 3, 4, 5, 6, 7
                lbInfo.Text &= oApp.Tr(Me, 129187332715039944, "Accetta solo valori numerici")
              Case 8
                lbInfo.Text &= oApp.Tr(Me, 129187340419883694, "Accetta date fisse, altrimenti selezionare o scrivere una data relativa dalla data di stampa (es: +15 giorni, -3 mesi, +1 anno)")
              Case 10, 11, 12
                lbInfo.Text &= oApp.Tr(Me, 129187332039571194, "Accetta qualsiasi tipo di carattere")
            End Select
        End Select
      End With
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvFiltri_NTSCellEnter(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles grvFiltri.NTSCellEnter
    Dim lTop, lLeft As Integer
    Try
      'faccio apparire il combo box con le possibili date
      If (NTSCStr(grvFiltri.NTSGetCurrentDataRow!xx_tipo) = "NTSTextBoxData" Or _
          (NTSCStr(grvFiltri.NTSGetCurrentDataRow!xx_tipo) = "NTSGridColumn" And NTSCInt(grvFiltri.NTSGetCurrentDataRow!xx_info) = 8)) _
          And grvFiltri.FocusedColumn.Name = "mo_valore" Then
        cbData.Width = grvFiltri.FocusedColumn.Width + 1
        grvFiltri.CalcolaTopLeft(lTop, lLeft)
        cbData.Left = lLeft - cbData.Width + 1
        If grvFiltri.RowHeight < 20 Then
          lTop = (grvFiltri.FocusedRowHandle - grvFiltri.TopRowIndex) * 20
          cbData.Height = 21
        Else
          lTop = (grvFiltri.FocusedRowHandle - grvFiltri.TopRowIndex) * (grvFiltri.RowHeight + 1)
          cbData.Height = grvFiltri.RowHeight + 1
        End If
        If lTop < 0 Or lTop + 24 + cbData.Height > grFiltri.Height Then cbData.Visible = False : Return
        cbData.Top = lTop + grFiltri.Top + 24
        cbData.Text = NTSCStr(grvFiltri.NTSGetCurrentDataRow()!mo_valore)
        cbData.Visible = True
        cbData.Focus()

        If Not e Is Nothing Then e.Cancel = True
      End If
    Catch ex As Exception
      '------------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '------------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvFiltri_TopRowChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grvFiltri.TopRowChanged
    Try
      If cbData.Visible Then grvFiltri_NTSCellEnter(Me, Nothing)
    Catch ex As Exception
      '------------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '------------------------------------------------------------------------------------
    End Try
  End Sub
#End Region
End Class