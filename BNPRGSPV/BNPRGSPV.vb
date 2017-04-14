#Region "Imports"
Imports System.Data
Imports NTSInformatica.CLN__STD
#End Region

Public Class FRMPRGSPV

#Region "Moduli"
  Private Moduli_P As Integer = bsModPR
  Private ModuliExt_P As Integer = 0
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
  Public oCleGspv As CLEPRGSPV
  Public oCallParams As CLE__CLDP
  Public dsGspv As DataSet
  Public dcGspv As BindingSource = New BindingSource()
  Public strTabella As String = "PROVVIG"

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents pnGspv As NTSInformatica.NTSPanel
  Public WithEvents lbDataDocumentiA As NTSInformatica.NTSLabel
  Public WithEvents lbScadenzaDocumentiA As NTSInformatica.NTSLabel
  Public WithEvents lbScadenzaDocumentiDa As NTSInformatica.NTSLabel
  Public WithEvents lbDataDocumentiDa As NTSInformatica.NTSLabel
  Public WithEvents lbAgente As NTSInformatica.NTSLabel
  Public WithEvents lbDesDataDocumentiDa As NTSInformatica.NTSLabel
  Public WithEvents lbDesScadenzaDocumentiA As NTSInformatica.NTSLabel
  Public WithEvents lbDesScadenzaDocumentiDa As NTSInformatica.NTSLabel
  Public WithEvents lbDesDataDocumentiA As NTSInformatica.NTSLabel
  Public WithEvents lbDesAgente As NTSInformatica.NTSLabel
  Public WithEvents tlbRecordNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRecordAggiorna As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRecordCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRecordRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbApri As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents grGspv As NTSInformatica.NTSGrid
  Public WithEvents grvGspv As NTSInformatica.NTSGridView
  Public WithEvents pv_tipdoc As NTSInformatica.NTSGridColumn
  Public WithEvents pv_numdoc As NTSInformatica.NTSGridColumn
  Public WithEvents pv_serie As NTSInformatica.NTSGridColumn
  Public WithEvents pv_anno As NTSInformatica.NTSGridColumn
  Public WithEvents pv_datdoc As NTSInformatica.NTSGridColumn
  Public WithEvents pv_numrat As NTSInformatica.NTSGridColumn
  Public WithEvents pv_datscad As NTSInformatica.NTSGridColumn
  Public WithEvents pv_conto As NTSInformatica.NTSGridColumn
  Public WithEvents xx_desconto As NTSInformatica.NTSGridColumn
  Public WithEvents pv_annpart As NTSInformatica.NTSGridColumn
  Public WithEvents pv_alfpart As NTSInformatica.NTSGridColumn
  Public WithEvents pv_numpart As NTSInformatica.NTSGridColumn
  Public WithEvents pv_totfatt As NTSInformatica.NTSGridColumn
  Public WithEvents pv_totomag As NTSInformatica.NTSGridColumn
  Public WithEvents pv_impopv As NTSInformatica.NTSGridColumn
  Public WithEvents pv_provv As NTSInformatica.NTSGridColumn
  Public WithEvents pv_impvmat As NTSInformatica.NTSGridColumn
  Public WithEvents pv_impvpag As NTSInformatica.NTSGridColumn
  Public WithEvents pv_codpaga As NTSInformatica.NTSGridColumn
  Public WithEvents xx_despaga As NTSInformatica.NTSGridColumn
  Public WithEvents pv_tippaga As NTSInformatica.NTSGridColumn
  Public WithEvents pv_flag As NTSInformatica.NTSGridColumn
  Public WithEvents pv_origine As NTSInformatica.NTSGridColumn
  Public WithEvents pv_codage As NTSInformatica.NTSGridColumn
  Public WithEvents pv_tipotass As NTSInformatica.NTSGridColumn
  Public WithEvents pv_codvalu As NTSInformatica.NTSGridColumn
  Public WithEvents xx_desvalu As NTSInformatica.NTSGridColumn
  Public WithEvents pv_cambio As NTSInformatica.NTSGridColumn
  Public WithEvents pv_vtotfatt As NTSInformatica.NTSGridColumn
  Public WithEvents pv_totomagv As NTSInformatica.NTSGridColumn
  Public WithEvents pv_vimpopv As NTSInformatica.NTSGridColumn
  Public WithEvents pv_vprovv As NTSInformatica.NTSGridColumn
  Public WithEvents pv_vimpvmat As NTSInformatica.NTSGridColumn
  Public WithEvents pv_vimpvpag As NTSInformatica.NTSGridColumn
#End Region

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMPRGSPV))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbApri = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecordNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecordAggiorna = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecordRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecordCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grGspv = New NTSInformatica.NTSGrid
    Me.grvGspv = New NTSInformatica.NTSGridView
    Me.pv_origine = New NTSInformatica.NTSGridColumn
    Me.pv_tipdoc = New NTSInformatica.NTSGridColumn
    Me.pv_numdoc = New NTSInformatica.NTSGridColumn
    Me.pv_serie = New NTSInformatica.NTSGridColumn
    Me.pv_anno = New NTSInformatica.NTSGridColumn
    Me.pv_numrat = New NTSInformatica.NTSGridColumn
    Me.pv_datdoc = New NTSInformatica.NTSGridColumn
    Me.pv_datscad = New NTSInformatica.NTSGridColumn
    Me.pv_datscadeff = New NTSInformatica.NTSGridColumn
    Me.pv_conto = New NTSInformatica.NTSGridColumn
    Me.xx_desconto = New NTSInformatica.NTSGridColumn
    Me.pv_annpart = New NTSInformatica.NTSGridColumn
    Me.pv_alfpart = New NTSInformatica.NTSGridColumn
    Me.pv_numpart = New NTSInformatica.NTSGridColumn
    Me.pv_segno = New NTSInformatica.NTSGridColumn
    Me.pv_totfatt = New NTSInformatica.NTSGridColumn
    Me.pv_totomag = New NTSInformatica.NTSGridColumn
    Me.pv_impopv = New NTSInformatica.NTSGridColumn
    Me.pv_provv = New NTSInformatica.NTSGridColumn
    Me.pv_impvmat = New NTSInformatica.NTSGridColumn
    Me.pv_datmatu = New NTSInformatica.NTSGridColumn
    Me.pv_impvpag = New NTSInformatica.NTSGridColumn
    Me.pv_datcorr = New NTSInformatica.NTSGridColumn
    Me.pv_codpaga = New NTSInformatica.NTSGridColumn
    Me.xx_despaga = New NTSInformatica.NTSGridColumn
    Me.pv_tippaga = New NTSInformatica.NTSGridColumn
    Me.pv_flag = New NTSInformatica.NTSGridColumn
    Me.pv_codage = New NTSInformatica.NTSGridColumn
    Me.pv_tipotass = New NTSInformatica.NTSGridColumn
    Me.pv_codvalu = New NTSInformatica.NTSGridColumn
    Me.xx_desvalu = New NTSInformatica.NTSGridColumn
    Me.pv_cambio = New NTSInformatica.NTSGridColumn
    Me.pv_vtotfatt = New NTSInformatica.NTSGridColumn
    Me.pv_totomagv = New NTSInformatica.NTSGridColumn
    Me.pv_vimpopv = New NTSInformatica.NTSGridColumn
    Me.pv_vprovv = New NTSInformatica.NTSGridColumn
    Me.pv_vimpvmat = New NTSInformatica.NTSGridColumn
    Me.pv_vimpvpag = New NTSInformatica.NTSGridColumn
    Me.pv_note = New NTSInformatica.NTSGridColumn
    Me.pv_scflsaldato = New NTSInformatica.NTSGridColumn
    Me.pv_dtsaldato = New NTSInformatica.NTSGridColumn
    Me.pv_impscad = New NTSInformatica.NTSGridColumn
    Me.pnGspv = New NTSInformatica.NTSPanel
    Me.cmdPaga = New NTSInformatica.NTSButton
    Me.lbDesDataDocumentiDa = New NTSInformatica.NTSLabel
    Me.lbDesScadenzaDocumentiA = New NTSInformatica.NTSLabel
    Me.lbDesScadenzaDocumentiDa = New NTSInformatica.NTSLabel
    Me.lbDesDataDocumentiA = New NTSInformatica.NTSLabel
    Me.lbDesAgente = New NTSInformatica.NTSLabel
    Me.lbDataDocumentiA = New NTSInformatica.NTSLabel
    Me.lbScadenzaDocumentiA = New NTSInformatica.NTSLabel
    Me.lbScadenzaDocumentiDa = New NTSInformatica.NTSLabel
    Me.lbDataDocumentiDa = New NTSInformatica.NTSLabel
    Me.lbAgente = New NTSInformatica.NTSLabel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grGspv, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvGspv, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnGspv, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGspv.SuspendLayout()
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
    'NtsBarManager1
    '
    Me.NtsBarManager1.AllowCustomization = False
    Me.NtsBarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.tlbMain})
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlTop)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlBottom)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlLeft)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlRight)
    Me.NtsBarManager1.Form = Me
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbApri, Me.tlbCancella, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbRecordNuovo, Me.tlbRecordAggiorna, Me.tlbRecordCancella, Me.tlbRecordRipristina})
    Me.NtsBarManager1.MaxItemId = 21
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApri), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordNuovo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordAggiorna), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbApri
    '
    Me.tlbApri.Caption = "Apri"
    Me.tlbApri.Glyph = CType(resources.GetObject("tlbApri.Glyph"), System.Drawing.Image)
    Me.tlbApri.Id = 1
    Me.tlbApri.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3)
    Me.tlbApri.Name = "tlbApri"
    Me.tlbApri.Visible = True
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
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.Id = 3
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 13
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbRecordNuovo
    '
    Me.tlbRecordNuovo.Caption = "Nuova riga"
    Me.tlbRecordNuovo.Glyph = CType(resources.GetObject("tlbRecordNuovo.Glyph"), System.Drawing.Image)
    Me.tlbRecordNuovo.Id = 17
    Me.tlbRecordNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F2))
    Me.tlbRecordNuovo.Name = "tlbRecordNuovo"
    Me.tlbRecordNuovo.Visible = True
    '
    'tlbRecordAggiorna
    '
    Me.tlbRecordAggiorna.Caption = "Aggiorna riga"
    Me.tlbRecordAggiorna.Glyph = CType(resources.GetObject("tlbRecordAggiorna.Glyph"), System.Drawing.Image)
    Me.tlbRecordAggiorna.Id = 18
    Me.tlbRecordAggiorna.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F9))
    Me.tlbRecordAggiorna.Name = "tlbRecordAggiorna"
    Me.tlbRecordAggiorna.Visible = True
    '
    'tlbRecordRipristina
    '
    Me.tlbRecordRipristina.Caption = "Ripristina riga"
    Me.tlbRecordRipristina.Glyph = CType(resources.GetObject("tlbRecordRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRecordRipristina.Id = 20
    Me.tlbRecordRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F8))
    Me.tlbRecordRipristina.Name = "tlbRecordRipristina"
    Me.tlbRecordRipristina.Visible = True
    '
    'tlbRecordCancella
    '
    Me.tlbRecordCancella.Caption = "Cancella riga"
    Me.tlbRecordCancella.Glyph = CType(resources.GetObject("tlbRecordCancella.Glyph"), System.Drawing.Image)
    Me.tlbRecordCancella.Id = 19
    Me.tlbRecordCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F4))
    Me.tlbRecordCancella.Name = "tlbRecordCancella"
    Me.tlbRecordCancella.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 11
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 12
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'grGspv
    '
    Me.grGspv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '
    '
    '
    Me.grGspv.EmbeddedNavigator.Name = ""
    Me.grGspv.Location = New System.Drawing.Point(0, 76)
    Me.grGspv.MainView = Me.grvGspv
    Me.grGspv.Name = "grGspv"
    Me.grGspv.Size = New System.Drawing.Size(648, 286)
    Me.grGspv.TabIndex = 5
    Me.grGspv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvGspv})
    '
    'grvGspv
    '
    Me.grvGspv.ActiveFilterEnabled = False
    Me.grvGspv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.pv_origine, Me.pv_tipdoc, Me.pv_numdoc, Me.pv_serie, Me.pv_anno, Me.pv_numrat, Me.pv_datdoc, Me.pv_datscad, Me.pv_datscadeff, Me.pv_conto, Me.xx_desconto, Me.pv_annpart, Me.pv_alfpart, Me.pv_numpart, Me.pv_segno, Me.pv_totfatt, Me.pv_totomag, Me.pv_impopv, Me.pv_provv, Me.pv_impvmat, Me.pv_datmatu, Me.pv_impvpag, Me.pv_datcorr, Me.pv_codpaga, Me.xx_despaga, Me.pv_tippaga, Me.pv_flag, Me.pv_codage, Me.pv_tipotass, Me.pv_codvalu, Me.xx_desvalu, Me.pv_cambio, Me.pv_vtotfatt, Me.pv_totomagv, Me.pv_vimpopv, Me.pv_vprovv, Me.pv_vimpvmat, Me.pv_vimpvpag, Me.pv_note, Me.pv_scflsaldato, Me.pv_dtsaldato, Me.pv_impscad})
    Me.grvGspv.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvGspv.Enabled = True
    Me.grvGspv.GridControl = Me.grGspv
    Me.grvGspv.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvGspv.MinRowHeight = 14
    Me.grvGspv.Name = "grvGspv"
    Me.grvGspv.NTSAllowDelete = True
    Me.grvGspv.NTSAllowInsert = True
    Me.grvGspv.NTSAllowUpdate = True
    Me.grvGspv.NTSMenuContext = Nothing
    Me.grvGspv.OptionsCustomization.AllowRowSizing = True
    Me.grvGspv.OptionsFilter.AllowFilterEditor = False
    Me.grvGspv.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvGspv.OptionsNavigation.UseTabKey = False
    Me.grvGspv.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvGspv.OptionsView.ColumnAutoWidth = False
    Me.grvGspv.OptionsView.EnableAppearanceEvenRow = True
    Me.grvGspv.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvGspv.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvGspv.OptionsView.ShowGroupPanel = False
    Me.grvGspv.RowHeight = 16
    '
    'pv_origine
    '
    Me.pv_origine.AppearanceCell.Options.UseBackColor = True
    Me.pv_origine.AppearanceCell.Options.UseTextOptions = True
    Me.pv_origine.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_origine.Caption = "Origine"
    Me.pv_origine.Enabled = True
    Me.pv_origine.FieldName = "pv_origine"
    Me.pv_origine.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_origine.Name = "pv_origine"
    Me.pv_origine.NTSRepositoryComboBox = Nothing
    Me.pv_origine.NTSRepositoryItemCheck = Nothing
    Me.pv_origine.NTSRepositoryItemMemo = Nothing
    Me.pv_origine.NTSRepositoryItemText = Nothing
    Me.pv_origine.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_origine.OptionsFilter.AllowFilter = False
    Me.pv_origine.Visible = True
    Me.pv_origine.VisibleIndex = 0
    Me.pv_origine.Width = 46
    '
    'pv_tipdoc
    '
    Me.pv_tipdoc.AppearanceCell.Options.UseBackColor = True
    Me.pv_tipdoc.AppearanceCell.Options.UseTextOptions = True
    Me.pv_tipdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_tipdoc.Caption = "Tipo doc."
    Me.pv_tipdoc.Enabled = True
    Me.pv_tipdoc.FieldName = "pv_tipdoc"
    Me.pv_tipdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_tipdoc.Name = "pv_tipdoc"
    Me.pv_tipdoc.NTSRepositoryComboBox = Nothing
    Me.pv_tipdoc.NTSRepositoryItemCheck = Nothing
    Me.pv_tipdoc.NTSRepositoryItemMemo = Nothing
    Me.pv_tipdoc.NTSRepositoryItemText = Nothing
    Me.pv_tipdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_tipdoc.OptionsFilter.AllowFilter = False
    Me.pv_tipdoc.Visible = True
    Me.pv_tipdoc.VisibleIndex = 1
    Me.pv_tipdoc.Width = 88
    '
    'pv_numdoc
    '
    Me.pv_numdoc.AppearanceCell.Options.UseBackColor = True
    Me.pv_numdoc.AppearanceCell.Options.UseTextOptions = True
    Me.pv_numdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_numdoc.Caption = "Numero doc."
    Me.pv_numdoc.Enabled = True
    Me.pv_numdoc.FieldName = "pv_numdoc"
    Me.pv_numdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_numdoc.Name = "pv_numdoc"
    Me.pv_numdoc.NTSRepositoryComboBox = Nothing
    Me.pv_numdoc.NTSRepositoryItemCheck = Nothing
    Me.pv_numdoc.NTSRepositoryItemMemo = Nothing
    Me.pv_numdoc.NTSRepositoryItemText = Nothing
    Me.pv_numdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_numdoc.OptionsFilter.AllowFilter = False
    Me.pv_numdoc.Visible = True
    Me.pv_numdoc.VisibleIndex = 2
    Me.pv_numdoc.Width = 105
    '
    'pv_serie
    '
    Me.pv_serie.AppearanceCell.Options.UseBackColor = True
    Me.pv_serie.AppearanceCell.Options.UseTextOptions = True
    Me.pv_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_serie.Caption = "Serie doc."
    Me.pv_serie.Enabled = True
    Me.pv_serie.FieldName = "pv_serie"
    Me.pv_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_serie.Name = "pv_serie"
    Me.pv_serie.NTSRepositoryComboBox = Nothing
    Me.pv_serie.NTSRepositoryItemCheck = Nothing
    Me.pv_serie.NTSRepositoryItemMemo = Nothing
    Me.pv_serie.NTSRepositoryItemText = Nothing
    Me.pv_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_serie.OptionsFilter.AllowFilter = False
    Me.pv_serie.Visible = True
    Me.pv_serie.VisibleIndex = 3
    Me.pv_serie.Width = 92
    '
    'pv_anno
    '
    Me.pv_anno.AppearanceCell.Options.UseBackColor = True
    Me.pv_anno.AppearanceCell.Options.UseTextOptions = True
    Me.pv_anno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_anno.Caption = "Anno doc."
    Me.pv_anno.Enabled = True
    Me.pv_anno.FieldName = "pv_anno"
    Me.pv_anno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_anno.Name = "pv_anno"
    Me.pv_anno.NTSRepositoryComboBox = Nothing
    Me.pv_anno.NTSRepositoryItemCheck = Nothing
    Me.pv_anno.NTSRepositoryItemMemo = Nothing
    Me.pv_anno.NTSRepositoryItemText = Nothing
    Me.pv_anno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_anno.OptionsFilter.AllowFilter = False
    Me.pv_anno.Visible = True
    Me.pv_anno.VisibleIndex = 4
    Me.pv_anno.Width = 93
    '
    'pv_numrat
    '
    Me.pv_numrat.AppearanceCell.Options.UseBackColor = True
    Me.pv_numrat.AppearanceCell.Options.UseTextOptions = True
    Me.pv_numrat.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_numrat.Caption = "Numero rata"
    Me.pv_numrat.Enabled = True
    Me.pv_numrat.FieldName = "pv_numrat"
    Me.pv_numrat.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_numrat.Name = "pv_numrat"
    Me.pv_numrat.NTSRepositoryComboBox = Nothing
    Me.pv_numrat.NTSRepositoryItemCheck = Nothing
    Me.pv_numrat.NTSRepositoryItemMemo = Nothing
    Me.pv_numrat.NTSRepositoryItemText = Nothing
    Me.pv_numrat.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_numrat.OptionsFilter.AllowFilter = False
    Me.pv_numrat.Visible = True
    Me.pv_numrat.VisibleIndex = 5
    Me.pv_numrat.Width = 72
    '
    'pv_datdoc
    '
    Me.pv_datdoc.AppearanceCell.Options.UseBackColor = True
    Me.pv_datdoc.AppearanceCell.Options.UseTextOptions = True
    Me.pv_datdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_datdoc.Caption = "Data doc."
    Me.pv_datdoc.Enabled = True
    Me.pv_datdoc.FieldName = "pv_datdoc"
    Me.pv_datdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_datdoc.Name = "pv_datdoc"
    Me.pv_datdoc.NTSRepositoryComboBox = Nothing
    Me.pv_datdoc.NTSRepositoryItemCheck = Nothing
    Me.pv_datdoc.NTSRepositoryItemMemo = Nothing
    Me.pv_datdoc.NTSRepositoryItemText = Nothing
    Me.pv_datdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_datdoc.OptionsFilter.AllowFilter = False
    Me.pv_datdoc.Visible = True
    Me.pv_datdoc.VisibleIndex = 6
    Me.pv_datdoc.Width = 72
    '
    'pv_datscad
    '
    Me.pv_datscad.AppearanceCell.Options.UseBackColor = True
    Me.pv_datscad.AppearanceCell.Options.UseTextOptions = True
    Me.pv_datscad.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_datscad.Caption = "Data scadenza"
    Me.pv_datscad.Enabled = True
    Me.pv_datscad.FieldName = "pv_datscad"
    Me.pv_datscad.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_datscad.Name = "pv_datscad"
    Me.pv_datscad.NTSRepositoryComboBox = Nothing
    Me.pv_datscad.NTSRepositoryItemCheck = Nothing
    Me.pv_datscad.NTSRepositoryItemMemo = Nothing
    Me.pv_datscad.NTSRepositoryItemText = Nothing
    Me.pv_datscad.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_datscad.OptionsFilter.AllowFilter = False
    Me.pv_datscad.Visible = True
    Me.pv_datscad.VisibleIndex = 7
    Me.pv_datscad.Width = 83
    '
    'pv_datscadeff
    '
    Me.pv_datscadeff.AppearanceCell.Options.UseBackColor = True
    Me.pv_datscadeff.AppearanceCell.Options.UseTextOptions = True
    Me.pv_datscadeff.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_datscadeff.Caption = "Data scad. effettiva"
    Me.pv_datscadeff.Enabled = True
    Me.pv_datscadeff.FieldName = "pv_datscadeff"
    Me.pv_datscadeff.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_datscadeff.Name = "pv_datscadeff"
    Me.pv_datscadeff.NTSRepositoryComboBox = Nothing
    Me.pv_datscadeff.NTSRepositoryItemCheck = Nothing
    Me.pv_datscadeff.NTSRepositoryItemMemo = Nothing
    Me.pv_datscadeff.NTSRepositoryItemText = Nothing
    Me.pv_datscadeff.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_datscadeff.OptionsFilter.AllowFilter = False
    Me.pv_datscadeff.Visible = True
    Me.pv_datscadeff.VisibleIndex = 8
    '
    'pv_conto
    '
    Me.pv_conto.AppearanceCell.Options.UseBackColor = True
    Me.pv_conto.AppearanceCell.Options.UseTextOptions = True
    Me.pv_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_conto.Caption = "Conto"
    Me.pv_conto.Enabled = True
    Me.pv_conto.FieldName = "pv_conto"
    Me.pv_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_conto.Name = "pv_conto"
    Me.pv_conto.NTSRepositoryComboBox = Nothing
    Me.pv_conto.NTSRepositoryItemCheck = Nothing
    Me.pv_conto.NTSRepositoryItemMemo = Nothing
    Me.pv_conto.NTSRepositoryItemText = Nothing
    Me.pv_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_conto.OptionsFilter.AllowFilter = False
    Me.pv_conto.Visible = True
    Me.pv_conto.VisibleIndex = 9
    Me.pv_conto.Width = 41
    '
    'xx_desconto
    '
    Me.xx_desconto.AppearanceCell.Options.UseBackColor = True
    Me.xx_desconto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_desconto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_desconto.Caption = "Descrizione conto"
    Me.xx_desconto.Enabled = False
    Me.xx_desconto.FieldName = "xx_desconto"
    Me.xx_desconto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_desconto.Name = "xx_desconto"
    Me.xx_desconto.NTSRepositoryComboBox = Nothing
    Me.xx_desconto.NTSRepositoryItemCheck = Nothing
    Me.xx_desconto.NTSRepositoryItemMemo = Nothing
    Me.xx_desconto.NTSRepositoryItemText = Nothing
    Me.xx_desconto.OptionsColumn.AllowEdit = False
    Me.xx_desconto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_desconto.OptionsColumn.ReadOnly = True
    Me.xx_desconto.OptionsFilter.AllowFilter = False
    Me.xx_desconto.Visible = True
    Me.xx_desconto.VisibleIndex = 10
    Me.xx_desconto.Width = 96
    '
    'pv_annpart
    '
    Me.pv_annpart.AppearanceCell.Options.UseBackColor = True
    Me.pv_annpart.AppearanceCell.Options.UseTextOptions = True
    Me.pv_annpart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_annpart.Caption = "Anno partita"
    Me.pv_annpart.Enabled = True
    Me.pv_annpart.FieldName = "pv_annpart"
    Me.pv_annpart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_annpart.Name = "pv_annpart"
    Me.pv_annpart.NTSRepositoryComboBox = Nothing
    Me.pv_annpart.NTSRepositoryItemCheck = Nothing
    Me.pv_annpart.NTSRepositoryItemMemo = Nothing
    Me.pv_annpart.NTSRepositoryItemText = Nothing
    Me.pv_annpart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_annpart.OptionsFilter.AllowFilter = False
    Me.pv_annpart.Visible = True
    Me.pv_annpart.VisibleIndex = 11
    Me.pv_annpart.Width = 72
    '
    'pv_alfpart
    '
    Me.pv_alfpart.AppearanceCell.Options.UseBackColor = True
    Me.pv_alfpart.AppearanceCell.Options.UseTextOptions = True
    Me.pv_alfpart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_alfpart.Caption = "Serie partita"
    Me.pv_alfpart.Enabled = True
    Me.pv_alfpart.FieldName = "pv_alfpart"
    Me.pv_alfpart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_alfpart.Name = "pv_alfpart"
    Me.pv_alfpart.NTSRepositoryComboBox = Nothing
    Me.pv_alfpart.NTSRepositoryItemCheck = Nothing
    Me.pv_alfpart.NTSRepositoryItemMemo = Nothing
    Me.pv_alfpart.NTSRepositoryItemText = Nothing
    Me.pv_alfpart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_alfpart.OptionsFilter.AllowFilter = False
    Me.pv_alfpart.Visible = True
    Me.pv_alfpart.VisibleIndex = 12
    Me.pv_alfpart.Width = 71
    '
    'pv_numpart
    '
    Me.pv_numpart.AppearanceCell.Options.UseBackColor = True
    Me.pv_numpart.AppearanceCell.Options.UseTextOptions = True
    Me.pv_numpart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_numpart.Caption = "Numero partita"
    Me.pv_numpart.Enabled = True
    Me.pv_numpart.FieldName = "pv_numpart"
    Me.pv_numpart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_numpart.Name = "pv_numpart"
    Me.pv_numpart.NTSRepositoryComboBox = Nothing
    Me.pv_numpart.NTSRepositoryItemCheck = Nothing
    Me.pv_numpart.NTSRepositoryItemMemo = Nothing
    Me.pv_numpart.NTSRepositoryItemText = Nothing
    Me.pv_numpart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_numpart.OptionsFilter.AllowFilter = False
    Me.pv_numpart.Visible = True
    Me.pv_numpart.VisibleIndex = 13
    Me.pv_numpart.Width = 84
    '
    'pv_segno
    '
    Me.pv_segno.AppearanceCell.Options.UseBackColor = True
    Me.pv_segno.AppearanceCell.Options.UseTextOptions = True
    Me.pv_segno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_segno.Caption = "Segno importo provv."
    Me.pv_segno.Enabled = True
    Me.pv_segno.FieldName = "pv_segno"
    Me.pv_segno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_segno.Name = "pv_segno"
    Me.pv_segno.NTSRepositoryComboBox = Nothing
    Me.pv_segno.NTSRepositoryItemCheck = Nothing
    Me.pv_segno.NTSRepositoryItemMemo = Nothing
    Me.pv_segno.NTSRepositoryItemText = Nothing
    Me.pv_segno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_segno.OptionsFilter.AllowFilter = False
    Me.pv_segno.Visible = True
    Me.pv_segno.VisibleIndex = 14
    '
    'pv_totfatt
    '
    Me.pv_totfatt.AppearanceCell.Options.UseBackColor = True
    Me.pv_totfatt.AppearanceCell.Options.UseTextOptions = True
    Me.pv_totfatt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_totfatt.Caption = "Totale fattura"
    Me.pv_totfatt.Enabled = True
    Me.pv_totfatt.FieldName = "pv_totfatt"
    Me.pv_totfatt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_totfatt.Name = "pv_totfatt"
    Me.pv_totfatt.NTSRepositoryComboBox = Nothing
    Me.pv_totfatt.NTSRepositoryItemCheck = Nothing
    Me.pv_totfatt.NTSRepositoryItemMemo = Nothing
    Me.pv_totfatt.NTSRepositoryItemText = Nothing
    Me.pv_totfatt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_totfatt.OptionsFilter.AllowFilter = False
    Me.pv_totfatt.Visible = True
    Me.pv_totfatt.VisibleIndex = 15
    Me.pv_totfatt.Width = 79
    '
    'pv_totomag
    '
    Me.pv_totomag.AppearanceCell.Options.UseBackColor = True
    Me.pv_totomag.AppearanceCell.Options.UseTextOptions = True
    Me.pv_totomag.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_totomag.Caption = "Totale omaggi"
    Me.pv_totomag.Enabled = True
    Me.pv_totomag.FieldName = "pv_totomag"
    Me.pv_totomag.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_totomag.Name = "pv_totomag"
    Me.pv_totomag.NTSRepositoryComboBox = Nothing
    Me.pv_totomag.NTSRepositoryItemCheck = Nothing
    Me.pv_totomag.NTSRepositoryItemMemo = Nothing
    Me.pv_totomag.NTSRepositoryItemText = Nothing
    Me.pv_totomag.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_totomag.OptionsFilter.AllowFilter = False
    Me.pv_totomag.Visible = True
    Me.pv_totomag.VisibleIndex = 16
    Me.pv_totomag.Width = 79
    '
    'pv_impopv
    '
    Me.pv_impopv.AppearanceCell.Options.UseBackColor = True
    Me.pv_impopv.AppearanceCell.Options.UseTextOptions = True
    Me.pv_impopv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_impopv.Caption = "Imponibile"
    Me.pv_impopv.Enabled = True
    Me.pv_impopv.FieldName = "pv_impopv"
    Me.pv_impopv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_impopv.Name = "pv_impopv"
    Me.pv_impopv.NTSRepositoryComboBox = Nothing
    Me.pv_impopv.NTSRepositoryItemCheck = Nothing
    Me.pv_impopv.NTSRepositoryItemMemo = Nothing
    Me.pv_impopv.NTSRepositoryItemText = Nothing
    Me.pv_impopv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_impopv.OptionsFilter.AllowFilter = False
    Me.pv_impopv.Visible = True
    Me.pv_impopv.VisibleIndex = 17
    Me.pv_impopv.Width = 60
    '
    'pv_provv
    '
    Me.pv_provv.AppearanceCell.Options.UseBackColor = True
    Me.pv_provv.AppearanceCell.Options.UseTextOptions = True
    Me.pv_provv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_provv.Caption = "Provvigione"
    Me.pv_provv.Enabled = True
    Me.pv_provv.FieldName = "pv_provv"
    Me.pv_provv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_provv.Name = "pv_provv"
    Me.pv_provv.NTSRepositoryComboBox = Nothing
    Me.pv_provv.NTSRepositoryItemCheck = Nothing
    Me.pv_provv.NTSRepositoryItemMemo = Nothing
    Me.pv_provv.NTSRepositoryItemText = Nothing
    Me.pv_provv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_provv.OptionsFilter.AllowFilter = False
    Me.pv_provv.Visible = True
    Me.pv_provv.VisibleIndex = 18
    Me.pv_provv.Width = 68
    '
    'pv_impvmat
    '
    Me.pv_impvmat.AppearanceCell.Options.UseBackColor = True
    Me.pv_impvmat.AppearanceCell.Options.UseTextOptions = True
    Me.pv_impvmat.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_impvmat.Caption = "Maturato"
    Me.pv_impvmat.Enabled = True
    Me.pv_impvmat.FieldName = "pv_impvmat"
    Me.pv_impvmat.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_impvmat.Name = "pv_impvmat"
    Me.pv_impvmat.NTSRepositoryComboBox = Nothing
    Me.pv_impvmat.NTSRepositoryItemCheck = Nothing
    Me.pv_impvmat.NTSRepositoryItemMemo = Nothing
    Me.pv_impvmat.NTSRepositoryItemText = Nothing
    Me.pv_impvmat.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_impvmat.OptionsFilter.AllowFilter = False
    Me.pv_impvmat.Visible = True
    Me.pv_impvmat.VisibleIndex = 19
    Me.pv_impvmat.Width = 56
    '
    'pv_datmatu
    '
    Me.pv_datmatu.AppearanceCell.Options.UseBackColor = True
    Me.pv_datmatu.AppearanceCell.Options.UseTextOptions = True
    Me.pv_datmatu.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_datmatu.Caption = "Data maturaz."
    Me.pv_datmatu.Enabled = True
    Me.pv_datmatu.FieldName = "pv_datmatu"
    Me.pv_datmatu.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_datmatu.Name = "pv_datmatu"
    Me.pv_datmatu.NTSRepositoryComboBox = Nothing
    Me.pv_datmatu.NTSRepositoryItemCheck = Nothing
    Me.pv_datmatu.NTSRepositoryItemMemo = Nothing
    Me.pv_datmatu.NTSRepositoryItemText = Nothing
    Me.pv_datmatu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_datmatu.OptionsFilter.AllowFilter = False
    Me.pv_datmatu.Visible = True
    Me.pv_datmatu.VisibleIndex = 20
    '
    'pv_impvpag
    '
    Me.pv_impvpag.AppearanceCell.Options.UseBackColor = True
    Me.pv_impvpag.AppearanceCell.Options.UseTextOptions = True
    Me.pv_impvpag.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_impvpag.Caption = "Pagato"
    Me.pv_impvpag.Enabled = True
    Me.pv_impvpag.FieldName = "pv_impvpag"
    Me.pv_impvpag.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_impvpag.Name = "pv_impvpag"
    Me.pv_impvpag.NTSRepositoryComboBox = Nothing
    Me.pv_impvpag.NTSRepositoryItemCheck = Nothing
    Me.pv_impvpag.NTSRepositoryItemMemo = Nothing
    Me.pv_impvpag.NTSRepositoryItemText = Nothing
    Me.pv_impvpag.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_impvpag.OptionsFilter.AllowFilter = False
    Me.pv_impvpag.Visible = True
    Me.pv_impvpag.VisibleIndex = 21
    Me.pv_impvpag.Width = 46
    '
    'pv_datcorr
    '
    Me.pv_datcorr.AppearanceCell.Options.UseBackColor = True
    Me.pv_datcorr.AppearanceCell.Options.UseTextOptions = True
    Me.pv_datcorr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_datcorr.Caption = "Data pagam."
    Me.pv_datcorr.Enabled = True
    Me.pv_datcorr.FieldName = "pv_datcorr"
    Me.pv_datcorr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_datcorr.Name = "pv_datcorr"
    Me.pv_datcorr.NTSRepositoryComboBox = Nothing
    Me.pv_datcorr.NTSRepositoryItemCheck = Nothing
    Me.pv_datcorr.NTSRepositoryItemMemo = Nothing
    Me.pv_datcorr.NTSRepositoryItemText = Nothing
    Me.pv_datcorr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_datcorr.OptionsFilter.AllowFilter = False
    Me.pv_datcorr.Visible = True
    Me.pv_datcorr.VisibleIndex = 22
    '
    'pv_codpaga
    '
    Me.pv_codpaga.AppearanceCell.Options.UseBackColor = True
    Me.pv_codpaga.AppearanceCell.Options.UseTextOptions = True
    Me.pv_codpaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_codpaga.Caption = "Codice pagamento"
    Me.pv_codpaga.Enabled = True
    Me.pv_codpaga.FieldName = "pv_codpaga"
    Me.pv_codpaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_codpaga.Name = "pv_codpaga"
    Me.pv_codpaga.NTSRepositoryComboBox = Nothing
    Me.pv_codpaga.NTSRepositoryItemCheck = Nothing
    Me.pv_codpaga.NTSRepositoryItemMemo = Nothing
    Me.pv_codpaga.NTSRepositoryItemText = Nothing
    Me.pv_codpaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_codpaga.OptionsFilter.AllowFilter = False
    Me.pv_codpaga.Visible = True
    Me.pv_codpaga.VisibleIndex = 23
    Me.pv_codpaga.Width = 101
    '
    'xx_despaga
    '
    Me.xx_despaga.AppearanceCell.Options.UseBackColor = True
    Me.xx_despaga.AppearanceCell.Options.UseTextOptions = True
    Me.xx_despaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_despaga.Caption = "Descrizione pagamento"
    Me.xx_despaga.Enabled = False
    Me.xx_despaga.FieldName = "xx_despaga"
    Me.xx_despaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_despaga.Name = "xx_despaga"
    Me.xx_despaga.NTSRepositoryComboBox = Nothing
    Me.xx_despaga.NTSRepositoryItemCheck = Nothing
    Me.xx_despaga.NTSRepositoryItemMemo = Nothing
    Me.xx_despaga.NTSRepositoryItemText = Nothing
    Me.xx_despaga.OptionsColumn.AllowEdit = False
    Me.xx_despaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_despaga.OptionsColumn.ReadOnly = True
    Me.xx_despaga.OptionsFilter.AllowFilter = False
    Me.xx_despaga.Visible = True
    Me.xx_despaga.VisibleIndex = 24
    Me.xx_despaga.Width = 123
    '
    'pv_tippaga
    '
    Me.pv_tippaga.AppearanceCell.Options.UseBackColor = True
    Me.pv_tippaga.AppearanceCell.Options.UseTextOptions = True
    Me.pv_tippaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_tippaga.Caption = "Tipo pagamento"
    Me.pv_tippaga.Enabled = True
    Me.pv_tippaga.FieldName = "pv_tippaga"
    Me.pv_tippaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_tippaga.Name = "pv_tippaga"
    Me.pv_tippaga.NTSRepositoryComboBox = Nothing
    Me.pv_tippaga.NTSRepositoryItemCheck = Nothing
    Me.pv_tippaga.NTSRepositoryItemMemo = Nothing
    Me.pv_tippaga.NTSRepositoryItemText = Nothing
    Me.pv_tippaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_tippaga.OptionsFilter.AllowFilter = False
    Me.pv_tippaga.Visible = True
    Me.pv_tippaga.VisibleIndex = 25
    Me.pv_tippaga.Width = 89
    '
    'pv_flag
    '
    Me.pv_flag.AppearanceCell.Options.UseBackColor = True
    Me.pv_flag.AppearanceCell.Options.UseTextOptions = True
    Me.pv_flag.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_flag.Caption = "Status"
    Me.pv_flag.Enabled = True
    Me.pv_flag.FieldName = "pv_flag"
    Me.pv_flag.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_flag.Name = "pv_flag"
    Me.pv_flag.NTSRepositoryComboBox = Nothing
    Me.pv_flag.NTSRepositoryItemCheck = Nothing
    Me.pv_flag.NTSRepositoryItemMemo = Nothing
    Me.pv_flag.NTSRepositoryItemText = Nothing
    Me.pv_flag.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_flag.OptionsFilter.AllowFilter = False
    Me.pv_flag.Visible = True
    Me.pv_flag.VisibleIndex = 26
    Me.pv_flag.Width = 43
    '
    'pv_codage
    '
    Me.pv_codage.AppearanceCell.Options.UseBackColor = True
    Me.pv_codage.AppearanceCell.Options.UseTextOptions = True
    Me.pv_codage.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_codage.Caption = "Codice agente"
    Me.pv_codage.Enabled = True
    Me.pv_codage.FieldName = "pv_codage"
    Me.pv_codage.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_codage.Name = "pv_codage"
    Me.pv_codage.NTSRepositoryComboBox = Nothing
    Me.pv_codage.NTSRepositoryItemCheck = Nothing
    Me.pv_codage.NTSRepositoryItemMemo = Nothing
    Me.pv_codage.NTSRepositoryItemText = Nothing
    Me.pv_codage.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_codage.OptionsFilter.AllowFilter = False
    Me.pv_codage.Width = 81
    '
    'pv_tipotass
    '
    Me.pv_tipotass.AppearanceCell.Options.UseBackColor = True
    Me.pv_tipotass.AppearanceCell.Options.UseTextOptions = True
    Me.pv_tipotass.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_tipotass.Caption = "Tipo tass."
    Me.pv_tipotass.Enabled = True
    Me.pv_tipotass.FieldName = "pv_tipotass"
    Me.pv_tipotass.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_tipotass.Name = "pv_tipotass"
    Me.pv_tipotass.NTSRepositoryComboBox = Nothing
    Me.pv_tipotass.NTSRepositoryItemCheck = Nothing
    Me.pv_tipotass.NTSRepositoryItemMemo = Nothing
    Me.pv_tipotass.NTSRepositoryItemText = Nothing
    Me.pv_tipotass.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_tipotass.OptionsFilter.AllowFilter = False
    Me.pv_tipotass.Visible = True
    Me.pv_tipotass.VisibleIndex = 27
    Me.pv_tipotass.Width = 59
    '
    'pv_codvalu
    '
    Me.pv_codvalu.AppearanceCell.Options.UseBackColor = True
    Me.pv_codvalu.AppearanceCell.Options.UseTextOptions = True
    Me.pv_codvalu.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_codvalu.Caption = "Codice valuta"
    Me.pv_codvalu.Enabled = False
    Me.pv_codvalu.FieldName = "pv_codvalu"
    Me.pv_codvalu.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_codvalu.Name = "pv_codvalu"
    Me.pv_codvalu.NTSRepositoryComboBox = Nothing
    Me.pv_codvalu.NTSRepositoryItemCheck = Nothing
    Me.pv_codvalu.NTSRepositoryItemMemo = Nothing
    Me.pv_codvalu.NTSRepositoryItemText = Nothing
    Me.pv_codvalu.OptionsColumn.AllowEdit = False
    Me.pv_codvalu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_codvalu.OptionsColumn.ReadOnly = True
    Me.pv_codvalu.OptionsFilter.AllowFilter = False
    Me.pv_codvalu.Width = 77
    '
    'xx_desvalu
    '
    Me.xx_desvalu.AppearanceCell.Options.UseBackColor = True
    Me.xx_desvalu.AppearanceCell.Options.UseTextOptions = True
    Me.xx_desvalu.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_desvalu.Caption = "Descrizione valuta"
    Me.xx_desvalu.Enabled = False
    Me.xx_desvalu.FieldName = "xx_desvalu"
    Me.xx_desvalu.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_desvalu.Name = "xx_desvalu"
    Me.xx_desvalu.NTSRepositoryComboBox = Nothing
    Me.xx_desvalu.NTSRepositoryItemCheck = Nothing
    Me.xx_desvalu.NTSRepositoryItemMemo = Nothing
    Me.xx_desvalu.NTSRepositoryItemText = Nothing
    Me.xx_desvalu.OptionsColumn.AllowEdit = False
    Me.xx_desvalu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_desvalu.OptionsColumn.ReadOnly = True
    Me.xx_desvalu.OptionsFilter.AllowFilter = False
    Me.xx_desvalu.Width = 99
    '
    'pv_cambio
    '
    Me.pv_cambio.AppearanceCell.Options.UseBackColor = True
    Me.pv_cambio.AppearanceCell.Options.UseTextOptions = True
    Me.pv_cambio.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_cambio.Caption = "Cambio"
    Me.pv_cambio.Enabled = False
    Me.pv_cambio.FieldName = "pv_cambio"
    Me.pv_cambio.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_cambio.Name = "pv_cambio"
    Me.pv_cambio.NTSRepositoryComboBox = Nothing
    Me.pv_cambio.NTSRepositoryItemCheck = Nothing
    Me.pv_cambio.NTSRepositoryItemMemo = Nothing
    Me.pv_cambio.NTSRepositoryItemText = Nothing
    Me.pv_cambio.OptionsColumn.AllowEdit = False
    Me.pv_cambio.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_cambio.OptionsColumn.ReadOnly = True
    Me.pv_cambio.OptionsFilter.AllowFilter = False
    Me.pv_cambio.Width = 47
    '
    'pv_vtotfatt
    '
    Me.pv_vtotfatt.AppearanceCell.Options.UseBackColor = True
    Me.pv_vtotfatt.AppearanceCell.Options.UseTextOptions = True
    Me.pv_vtotfatt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_vtotfatt.Caption = "Totale fattura in valuta"
    Me.pv_vtotfatt.Enabled = False
    Me.pv_vtotfatt.FieldName = "pv_vtotfatt"
    Me.pv_vtotfatt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_vtotfatt.Name = "pv_vtotfatt"
    Me.pv_vtotfatt.NTSRepositoryComboBox = Nothing
    Me.pv_vtotfatt.NTSRepositoryItemCheck = Nothing
    Me.pv_vtotfatt.NTSRepositoryItemMemo = Nothing
    Me.pv_vtotfatt.NTSRepositoryItemText = Nothing
    Me.pv_vtotfatt.OptionsColumn.AllowEdit = False
    Me.pv_vtotfatt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_vtotfatt.OptionsColumn.ReadOnly = True
    Me.pv_vtotfatt.OptionsFilter.AllowFilter = False
    Me.pv_vtotfatt.Width = 123
    '
    'pv_totomagv
    '
    Me.pv_totomagv.AppearanceCell.Options.UseBackColor = True
    Me.pv_totomagv.AppearanceCell.Options.UseTextOptions = True
    Me.pv_totomagv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_totomagv.Caption = "Totale omaggi in valuta"
    Me.pv_totomagv.Enabled = True
    Me.pv_totomagv.FieldName = "pv_totomagv"
    Me.pv_totomagv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_totomagv.Name = "pv_totomagv"
    Me.pv_totomagv.NTSRepositoryComboBox = Nothing
    Me.pv_totomagv.NTSRepositoryItemCheck = Nothing
    Me.pv_totomagv.NTSRepositoryItemMemo = Nothing
    Me.pv_totomagv.NTSRepositoryItemText = Nothing
    Me.pv_totomagv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_totomagv.OptionsFilter.AllowFilter = False
    Me.pv_totomagv.Width = 123
    '
    'pv_vimpopv
    '
    Me.pv_vimpopv.AppearanceCell.Options.UseBackColor = True
    Me.pv_vimpopv.AppearanceCell.Options.UseTextOptions = True
    Me.pv_vimpopv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_vimpopv.Caption = "Imponibile in valuta"
    Me.pv_vimpopv.Enabled = False
    Me.pv_vimpopv.FieldName = "pv_vimpopv"
    Me.pv_vimpopv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_vimpopv.Name = "pv_vimpopv"
    Me.pv_vimpopv.NTSRepositoryComboBox = Nothing
    Me.pv_vimpopv.NTSRepositoryItemCheck = Nothing
    Me.pv_vimpopv.NTSRepositoryItemMemo = Nothing
    Me.pv_vimpopv.NTSRepositoryItemText = Nothing
    Me.pv_vimpopv.OptionsColumn.AllowEdit = False
    Me.pv_vimpopv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_vimpopv.OptionsColumn.ReadOnly = True
    Me.pv_vimpopv.OptionsFilter.AllowFilter = False
    Me.pv_vimpopv.Width = 104
    '
    'pv_vprovv
    '
    Me.pv_vprovv.AppearanceCell.Options.UseBackColor = True
    Me.pv_vprovv.AppearanceCell.Options.UseTextOptions = True
    Me.pv_vprovv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_vprovv.Caption = "Provvigione in valuta"
    Me.pv_vprovv.Enabled = False
    Me.pv_vprovv.FieldName = "pv_vprovv"
    Me.pv_vprovv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_vprovv.Name = "pv_vprovv"
    Me.pv_vprovv.NTSRepositoryComboBox = Nothing
    Me.pv_vprovv.NTSRepositoryItemCheck = Nothing
    Me.pv_vprovv.NTSRepositoryItemMemo = Nothing
    Me.pv_vprovv.NTSRepositoryItemText = Nothing
    Me.pv_vprovv.OptionsColumn.AllowEdit = False
    Me.pv_vprovv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_vprovv.OptionsColumn.ReadOnly = True
    Me.pv_vprovv.OptionsFilter.AllowFilter = False
    Me.pv_vprovv.Width = 112
    '
    'pv_vimpvmat
    '
    Me.pv_vimpvmat.AppearanceCell.Options.UseBackColor = True
    Me.pv_vimpvmat.AppearanceCell.Options.UseTextOptions = True
    Me.pv_vimpvmat.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_vimpvmat.Caption = "Maturato in valuta"
    Me.pv_vimpvmat.Enabled = True
    Me.pv_vimpvmat.FieldName = "pv_vimpvmat"
    Me.pv_vimpvmat.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_vimpvmat.Name = "pv_vimpvmat"
    Me.pv_vimpvmat.NTSRepositoryComboBox = Nothing
    Me.pv_vimpvmat.NTSRepositoryItemCheck = Nothing
    Me.pv_vimpvmat.NTSRepositoryItemMemo = Nothing
    Me.pv_vimpvmat.NTSRepositoryItemText = Nothing
    Me.pv_vimpvmat.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_vimpvmat.OptionsFilter.AllowFilter = False
    Me.pv_vimpvmat.Width = 100
    '
    'pv_vimpvpag
    '
    Me.pv_vimpvpag.AppearanceCell.Options.UseBackColor = True
    Me.pv_vimpvpag.AppearanceCell.Options.UseTextOptions = True
    Me.pv_vimpvpag.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_vimpvpag.Caption = "Pagato in valuta"
    Me.pv_vimpvpag.Enabled = False
    Me.pv_vimpvpag.FieldName = "pv_vimpvpag"
    Me.pv_vimpvpag.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_vimpvpag.Name = "pv_vimpvpag"
    Me.pv_vimpvpag.NTSRepositoryComboBox = Nothing
    Me.pv_vimpvpag.NTSRepositoryItemCheck = Nothing
    Me.pv_vimpvpag.NTSRepositoryItemMemo = Nothing
    Me.pv_vimpvpag.NTSRepositoryItemText = Nothing
    Me.pv_vimpvpag.OptionsColumn.AllowEdit = False
    Me.pv_vimpvpag.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_vimpvpag.OptionsColumn.ReadOnly = True
    Me.pv_vimpvpag.OptionsFilter.AllowFilter = False
    Me.pv_vimpvpag.Width = 90
    '
    'pv_note
    '
    Me.pv_note.AppearanceCell.Options.UseBackColor = True
    Me.pv_note.AppearanceCell.Options.UseTextOptions = True
    Me.pv_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_note.Caption = "Note"
    Me.pv_note.Enabled = True
    Me.pv_note.FieldName = "pv_note"
    Me.pv_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_note.Name = "pv_note"
    Me.pv_note.NTSRepositoryComboBox = Nothing
    Me.pv_note.NTSRepositoryItemCheck = Nothing
    Me.pv_note.NTSRepositoryItemMemo = Nothing
    Me.pv_note.NTSRepositoryItemText = Nothing
    Me.pv_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_note.OptionsFilter.AllowFilter = False
    Me.pv_note.Visible = True
    Me.pv_note.VisibleIndex = 28
    '
    'pv_scflsaldato
    '
    Me.pv_scflsaldato.AppearanceCell.Options.UseBackColor = True
    Me.pv_scflsaldato.AppearanceCell.Options.UseTextOptions = True
    Me.pv_scflsaldato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_scflsaldato.Caption = "Scadenza saldata"
    Me.pv_scflsaldato.Enabled = True
    Me.pv_scflsaldato.FieldName = "pv_scflsaldato"
    Me.pv_scflsaldato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_scflsaldato.Name = "pv_scflsaldato"
    Me.pv_scflsaldato.NTSRepositoryComboBox = Nothing
    Me.pv_scflsaldato.NTSRepositoryItemCheck = Nothing
    Me.pv_scflsaldato.NTSRepositoryItemMemo = Nothing
    Me.pv_scflsaldato.NTSRepositoryItemText = Nothing
    Me.pv_scflsaldato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_scflsaldato.OptionsFilter.AllowFilter = False
    '
    'pv_dtsaldato
    '
    Me.pv_dtsaldato.AppearanceCell.Options.UseBackColor = True
    Me.pv_dtsaldato.AppearanceCell.Options.UseTextOptions = True
    Me.pv_dtsaldato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_dtsaldato.Caption = "Data saldo scadenza"
    Me.pv_dtsaldato.Enabled = True
    Me.pv_dtsaldato.FieldName = "pv_dtsaldato"
    Me.pv_dtsaldato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_dtsaldato.Name = "pv_dtsaldato"
    Me.pv_dtsaldato.NTSRepositoryComboBox = Nothing
    Me.pv_dtsaldato.NTSRepositoryItemCheck = Nothing
    Me.pv_dtsaldato.NTSRepositoryItemMemo = Nothing
    Me.pv_dtsaldato.NTSRepositoryItemText = Nothing
    Me.pv_dtsaldato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_dtsaldato.OptionsFilter.AllowFilter = False
    '
    'pv_impscad
    '
    Me.pv_impscad.AppearanceCell.Options.UseBackColor = True
    Me.pv_impscad.AppearanceCell.Options.UseTextOptions = True
    Me.pv_impscad.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.pv_impscad.Caption = "Importo scadenza"
    Me.pv_impscad.Enabled = False
    Me.pv_impscad.FieldName = "pv_impscad"
    Me.pv_impscad.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.pv_impscad.Name = "pv_impscad"
    Me.pv_impscad.NTSRepositoryComboBox = Nothing
    Me.pv_impscad.NTSRepositoryItemCheck = Nothing
    Me.pv_impscad.NTSRepositoryItemMemo = Nothing
    Me.pv_impscad.NTSRepositoryItemText = Nothing
    Me.pv_impscad.OptionsColumn.AllowEdit = False
    Me.pv_impscad.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.pv_impscad.OptionsColumn.ReadOnly = True
    Me.pv_impscad.OptionsFilter.AllowFilter = False
    Me.pv_impscad.Visible = True
    Me.pv_impscad.VisibleIndex = 29
    '
    'pnGspv
    '
    Me.pnGspv.AllowDrop = True
    Me.pnGspv.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGspv.Appearance.Options.UseBackColor = True
    Me.pnGspv.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGspv.Controls.Add(Me.cmdPaga)
    Me.pnGspv.Controls.Add(Me.lbDesDataDocumentiDa)
    Me.pnGspv.Controls.Add(Me.lbDesScadenzaDocumentiA)
    Me.pnGspv.Controls.Add(Me.lbDesScadenzaDocumentiDa)
    Me.pnGspv.Controls.Add(Me.lbDesDataDocumentiA)
    Me.pnGspv.Controls.Add(Me.lbDesAgente)
    Me.pnGspv.Controls.Add(Me.lbDataDocumentiA)
    Me.pnGspv.Controls.Add(Me.lbScadenzaDocumentiA)
    Me.pnGspv.Controls.Add(Me.lbScadenzaDocumentiDa)
    Me.pnGspv.Controls.Add(Me.lbDataDocumentiDa)
    Me.pnGspv.Controls.Add(Me.lbAgente)
    Me.pnGspv.Controls.Add(Me.grGspv)
    Me.pnGspv.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGspv.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGspv.Location = New System.Drawing.Point(0, 30)
    Me.pnGspv.Name = "pnGspv"
    Me.pnGspv.NTSActiveTrasparency = True
    Me.pnGspv.Size = New System.Drawing.Size(648, 362)
    Me.pnGspv.TabIndex = 6
    Me.pnGspv.Text = "NtsPanel1"
    '
    'cmdPaga
    '
    Me.cmdPaga.ImageText = ""
    Me.cmdPaga.Location = New System.Drawing.Point(454, 14)
    Me.cmdPaga.Name = "cmdPaga"
    Me.cmdPaga.Size = New System.Drawing.Size(179, 23)
    Me.cmdPaga.TabIndex = 51
    Me.cmdPaga.Text = "Paga tutte le provv. maturate"
    '
    'lbDesDataDocumentiDa
    '
    Me.lbDesDataDocumentiDa.BackColor = System.Drawing.Color.Transparent
    Me.lbDesDataDocumentiDa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesDataDocumentiDa.Location = New System.Drawing.Point(122, 47)
    Me.lbDesDataDocumentiDa.Name = "lbDesDataDocumentiDa"
    Me.lbDesDataDocumentiDa.NTSDbField = ""
    Me.lbDesDataDocumentiDa.Size = New System.Drawing.Size(75, 20)
    Me.lbDesDataDocumentiDa.TabIndex = 50
    Me.lbDesDataDocumentiDa.Text = "Descrizione fornitore"
    Me.lbDesDataDocumentiDa.Tooltip = ""
    Me.lbDesDataDocumentiDa.UseMnemonic = False
    '
    'lbDesScadenzaDocumentiA
    '
    Me.lbDesScadenzaDocumentiA.BackColor = System.Drawing.Color.Transparent
    Me.lbDesScadenzaDocumentiA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesScadenzaDocumentiA.Location = New System.Drawing.Point(558, 47)
    Me.lbDesScadenzaDocumentiA.Name = "lbDesScadenzaDocumentiA"
    Me.lbDesScadenzaDocumentiA.NTSDbField = ""
    Me.lbDesScadenzaDocumentiA.Size = New System.Drawing.Size(75, 20)
    Me.lbDesScadenzaDocumentiA.TabIndex = 50
    Me.lbDesScadenzaDocumentiA.Text = "Descrizione fornitore"
    Me.lbDesScadenzaDocumentiA.Tooltip = ""
    Me.lbDesScadenzaDocumentiA.UseMnemonic = False
    '
    'lbDesScadenzaDocumentiDa
    '
    Me.lbDesScadenzaDocumentiDa.BackColor = System.Drawing.Color.Transparent
    Me.lbDesScadenzaDocumentiDa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesScadenzaDocumentiDa.Location = New System.Drawing.Point(454, 47)
    Me.lbDesScadenzaDocumentiDa.Name = "lbDesScadenzaDocumentiDa"
    Me.lbDesScadenzaDocumentiDa.NTSDbField = ""
    Me.lbDesScadenzaDocumentiDa.Size = New System.Drawing.Size(75, 20)
    Me.lbDesScadenzaDocumentiDa.TabIndex = 50
    Me.lbDesScadenzaDocumentiDa.Text = "Descrizione fornitore"
    Me.lbDesScadenzaDocumentiDa.Tooltip = ""
    Me.lbDesScadenzaDocumentiDa.UseMnemonic = False
    '
    'lbDesDataDocumentiA
    '
    Me.lbDesDataDocumentiA.BackColor = System.Drawing.Color.Transparent
    Me.lbDesDataDocumentiA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesDataDocumentiA.Location = New System.Drawing.Point(226, 47)
    Me.lbDesDataDocumentiA.Name = "lbDesDataDocumentiA"
    Me.lbDesDataDocumentiA.NTSDbField = ""
    Me.lbDesDataDocumentiA.Size = New System.Drawing.Size(75, 20)
    Me.lbDesDataDocumentiA.TabIndex = 50
    Me.lbDesDataDocumentiA.Text = "Descrizione fornitore"
    Me.lbDesDataDocumentiA.Tooltip = ""
    Me.lbDesDataDocumentiA.UseMnemonic = False
    '
    'lbDesAgente
    '
    Me.lbDesAgente.BackColor = System.Drawing.Color.Transparent
    Me.lbDesAgente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesAgente.Location = New System.Drawing.Point(66, 14)
    Me.lbDesAgente.Name = "lbDesAgente"
    Me.lbDesAgente.NTSDbField = ""
    Me.lbDesAgente.Size = New System.Drawing.Size(266, 20)
    Me.lbDesAgente.TabIndex = 50
    Me.lbDesAgente.Text = "Descrizione fornitore"
    Me.lbDesAgente.Tooltip = ""
    Me.lbDesAgente.UseMnemonic = False
    '
    'lbDataDocumentiA
    '
    Me.lbDataDocumentiA.AutoSize = True
    Me.lbDataDocumentiA.BackColor = System.Drawing.Color.Transparent
    Me.lbDataDocumentiA.Location = New System.Drawing.Point(203, 48)
    Me.lbDataDocumentiA.Name = "lbDataDocumentiA"
    Me.lbDataDocumentiA.NTSDbField = ""
    Me.lbDataDocumentiA.Size = New System.Drawing.Size(17, 13)
    Me.lbDataDocumentiA.TabIndex = 6
    Me.lbDataDocumentiA.Text = "a:"
    Me.lbDataDocumentiA.Tooltip = ""
    Me.lbDataDocumentiA.UseMnemonic = False
    '
    'lbScadenzaDocumentiA
    '
    Me.lbScadenzaDocumentiA.AutoSize = True
    Me.lbScadenzaDocumentiA.BackColor = System.Drawing.Color.Transparent
    Me.lbScadenzaDocumentiA.Location = New System.Drawing.Point(535, 48)
    Me.lbScadenzaDocumentiA.Name = "lbScadenzaDocumentiA"
    Me.lbScadenzaDocumentiA.NTSDbField = ""
    Me.lbScadenzaDocumentiA.Size = New System.Drawing.Size(17, 13)
    Me.lbScadenzaDocumentiA.TabIndex = 6
    Me.lbScadenzaDocumentiA.Text = "a:"
    Me.lbScadenzaDocumentiA.Tooltip = ""
    Me.lbScadenzaDocumentiA.UseMnemonic = False
    '
    'lbScadenzaDocumentiDa
    '
    Me.lbScadenzaDocumentiDa.AutoSize = True
    Me.lbScadenzaDocumentiDa.BackColor = System.Drawing.Color.Transparent
    Me.lbScadenzaDocumentiDa.Location = New System.Drawing.Point(324, 48)
    Me.lbScadenzaDocumentiDa.Name = "lbScadenzaDocumentiDa"
    Me.lbScadenzaDocumentiDa.NTSDbField = ""
    Me.lbScadenzaDocumentiDa.Size = New System.Drawing.Size(124, 13)
    Me.lbScadenzaDocumentiDa.TabIndex = 6
    Me.lbScadenzaDocumentiDa.Text = "Scadenza documenti da:"
    Me.lbScadenzaDocumentiDa.Tooltip = ""
    Me.lbScadenzaDocumentiDa.UseMnemonic = False
    '
    'lbDataDocumentiDa
    '
    Me.lbDataDocumentiDa.AutoSize = True
    Me.lbDataDocumentiDa.BackColor = System.Drawing.Color.Transparent
    Me.lbDataDocumentiDa.Location = New System.Drawing.Point(14, 48)
    Me.lbDataDocumentiDa.Name = "lbDataDocumentiDa"
    Me.lbDataDocumentiDa.NTSDbField = ""
    Me.lbDataDocumentiDa.Size = New System.Drawing.Size(101, 13)
    Me.lbDataDocumentiDa.TabIndex = 6
    Me.lbDataDocumentiDa.Text = "Data documenti da:"
    Me.lbDataDocumentiDa.Tooltip = ""
    Me.lbDataDocumentiDa.UseMnemonic = False
    '
    'lbAgente
    '
    Me.lbAgente.AutoSize = True
    Me.lbAgente.BackColor = System.Drawing.Color.Transparent
    Me.lbAgente.Location = New System.Drawing.Point(14, 15)
    Me.lbAgente.Name = "lbAgente"
    Me.lbAgente.NTSDbField = ""
    Me.lbAgente.Size = New System.Drawing.Size(46, 13)
    Me.lbAgente.TabIndex = 6
    Me.lbAgente.Text = "Agente:"
    Me.lbAgente.Tooltip = ""
    Me.lbAgente.UseMnemonic = False
    '
    'FRMPRGSPV
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(648, 392)
    Me.Controls.Add(Me.pnGspv)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMPRGSPV"
    Me.NTSLastControlFocussed = Me.grGspv
    Me.Text = "GESTIONE PROVVIGIONI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grGspv, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvGspv, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnGspv, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGspv.ResumeLayout(False)
    Me.pnGspv.PerformLayout()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNPRGSPV", "BEPRGSPV", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128727749462343750, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleGspv = CType(oTmp, CLEPRGSPV)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNPRGSPV", strRemoteServer, strRemotePort)
    AddHandler oCleGspv.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleGspv.Init(oApp, oScript, oMenu.oCleComm, "PROVVIG", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub LoadImage()
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbApri.GlyphPath = (oApp.ChildImageDir & "\open.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")

        tlbRecordNuovo.GlyphPath = (oApp.ChildImageDir & "\recnew.gif")
        tlbRecordAggiorna.GlyphPath = (oApp.ChildImageDir & "\recagg.gif")
        tlbRecordRipristina.GlyphPath = (oApp.ChildImageDir & "\recrestore.gif")
        tlbRecordCancella.GlyphPath = (oApp.ChildImageDir & "\recdelete.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttTipDoc As New DataTable()
    Dim dttTipPag As New DataTable()
    Dim dttFlag As New DataTable()
    Dim dttOrigine As New DataTable()
    Dim dttTipoTass As New DataTable()
    Dim dttSegno As New DataTable()
    Try
      LoadImage()
      tlbMain.NTSSetToolTip()

      grvGspv.NTSSetParam(oMenu, "GESTIONE PROVVIGIONI")

      dttTipDoc.Columns.Add("cod", GetType(String))
      dttTipDoc.Columns.Add("val", GetType(String))
      dttTipDoc.Rows.Add(New Object() {"A", "A - Fatture Imm. emesse"})
      dttTipDoc.Rows.Add(New Object() {"B", "B - DDT emessi"})
      dttTipDoc.Rows.Add(New Object() {"C", "C - Corrispettivi emessi"})
      dttTipDoc.Rows.Add(New Object() {"D", "D - Fatture Diff. emesse"})
      dttTipDoc.Rows.Add(New Object() {"E", "E - Note di Addebito emesse"})
      dttTipDoc.Rows.Add(New Object() {"N", "N - Note Accr. emesse"})
      dttTipDoc.Rows.Add(New Object() {"£", "£ - Note Accr. Diff. emesse"})
      dttTipDoc.Rows.Add(New Object() {"F", "F - Ric.Fiscale Emessa"})
      dttTipDoc.Rows.Add(New Object() {"S", "S - Fatt.Ric.Fisc. Emessa"})
      dttTipDoc.Rows.Add(New Object() {"P", "P - Fatt.Ric.Fisc.Differita"})
      dttTipDoc.Rows.Add(New Object() {"R", "R - Impegni clienti"})
      dttTipDoc.Rows.Add(New Object() {"M", "M - DDT ricevuti"})
      dttTipDoc.Rows.Add(New Object() {" ", "Altro"})
      dttTipDoc.Rows.Add(New Object() {"@", "Premio da promozione differita"})
      dttTipDoc.AcceptChanges()

      dttSegno.Columns.Add("cod", GetType(Short))
      dttSegno.Columns.Add("val", GetType(String))
      dttSegno.Rows.Add(New Object() {1, "+"})
      dttSegno.Rows.Add(New Object() {-1, "-"})
      dttSegno.Rows.Add(New Object() {0, "solo inform./insoluto"})
      dttSegno.AcceptChanges()

      pv_tipdoc.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128727749457031250, "Tipo doc."), dttTipDoc, "val", "cod")
      pv_numdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128727749457187500, "Numero doc."), "0", 9, 0, 999999999)
      pv_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128727749457343750, "Serie doc."), CLN__STD.SerieMaxLen, False)
      pv_anno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128727749457500000, "Anno doc."), "0", 4, 1900, 2099)
      pv_datdoc.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128727749457656250, "Data doc."), False, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      pv_numrat.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128727749457812500, "Numero rata"), "0", 4, 0, 9999)
      pv_datscad.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128727749457968750, "Data scadenza"), False, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      pv_conto.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128727749458125000, "Conto"), tabanagrac)
      xx_desconto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128727749458281250, "Descrizione conto"), 50, True)
      pv_annpart.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128727749458437500, "Anno partita"), "0", 4, 0, 9999)
      pv_alfpart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128727749458593750, "Serie partita"), CLN__STD.SerieMaxLen, False)
      pv_numpart.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128727749458750000, "Numero partita"), "0", 9, 0, 999999999)
      pv_totfatt.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128727749458906250, "Totale fattura"), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      pv_totomag.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128727749459062500, "Totale omaggi"), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      pv_impopv.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128727749459218750, "Imponibile"), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      pv_provv.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128727749459375000, "Provvigione"), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      pv_impvmat.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128727749459531250, "Maturato"), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      pv_impvpag.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128727749459687500, "Pagato"), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      pv_codpaga.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128727749459843750, "Codice pagamento"), tabpaga)
      xx_despaga.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128727749460000000, "Descrizione pagamento"), 50, True)

      pv_segno.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129477037333720703, "Segno importo provv."), dttSegno, "val", "cod")
      pv_datscadeff.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129477901963554687, "Data scad. effettiva"), True)
      pv_datmatu.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129477901963759765, "Data maturaz."), True)
      pv_datcorr.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129477901963779297, "Data pagam."), True)
      pv_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129477901963945312, "Note"), 0, True)
      pv_scflsaldato.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129477901963955078, "Scadenza saldata"), "S", "N")
      pv_dtsaldato.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129477901963964843, "Data saldo scadenza"), True)

      dttTipPag.Columns.Add("cod", GetType(Integer))
      dttTipPag.Columns.Add("val", GetType(String))
      dttTipPag.Rows.Add(New Object() {1, "Tratta"})
      dttTipPag.Rows.Add(New Object() {2, "R.B. o RIBA"})
      dttTipPag.Rows.Add(New Object() {3, "Rim.Diretta"})
      dttTipPag.Rows.Add(New Object() {4, "Contanti"})
      dttTipPag.Rows.Add(New Object() {5, "Accr.Bancario"})
      dttTipPag.AcceptChanges()
      pv_tippaga.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128727749460156250, "Tipo pagamento"), dttTipPag, "val", "cod")

      dttFlag.Columns.Add("cod", GetType(String))
      dttFlag.Columns.Add("val", GetType(String))
      dttFlag.Rows.Add(New Object() {"N", "Non corrisposte"})
      dttFlag.Rows.Add(New Object() {"S", "Corrisposte"})
      dttFlag.Rows.Add(New Object() {"I", "Sospese"})
      dttFlag.AcceptChanges()
      pv_flag.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128727749460312500, "Status"), dttFlag, "val", "cod")

      dttOrigine.Columns.Add("cod", GetType(String))
      dttOrigine.Columns.Add("val", GetType(String))
      dttOrigine.Rows.Add(New Object() {"M", "(M) Da generaz. provv."})
      dttOrigine.Rows.Add(New Object() {"T", "(T) Da generaz. provv. nuovo incassato"})
      dttOrigine.Rows.Add(New Object() {"C", "(C) Integrazione o storno (gestione manuale)"})
      dttOrigine.Rows.Add(New Object() {"A", "(A) Acconto provvigioni"})
      dttOrigine.Rows.Add(New Object() {"S", "(S) Storno di provvig. già corrisposte"})
      dttOrigine.Rows.Add(New Object() {"B", "(B) Storno acconto provvigioni"})
      dttOrigine.Rows.Add(New Object() {"X", "(X) Simulazione"})
      dttOrigine.Rows.Add(New Object() {"Z", "(Z) Provvig. nuovo incassato non collegate a scadenze"})
      dttOrigine.AcceptChanges()
      pv_origine.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128727749460468750, "Origine"), dttOrigine, "val", "cod")

      pv_codage.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128727749460625000, "Codice agente"), tabcage)

      dttTipoTass.Columns.Add("cod", GetType(String))
      dttTipoTass.Columns.Add("val", GetType(String))
      dttTipoTass.Rows.Add(New Object() {"I", "Italia"})
      dttTipoTass.Rows.Add(New Object() {"E", "Unione Europea"})
      dttTipoTass.Rows.Add(New Object() {"X", "Extra Unione Europea"})
      dttTipoTass.AcceptChanges()
      pv_tipotass.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128727749460781250, "Tipo tass."), dttTipoTass, "val", "cod")

      pv_codvalu.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128727749460937500, "Codice valuta"), tabvalu)
      xx_desvalu.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128727749461093750, "Descrizione valuta"), 10, True)
      pv_cambio.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128727749461250000, "Cambio"), "#,##0.000000000", 20, 0, 99999999)
      pv_vtotfatt.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128727749461406250, "Totale fattura in valuta"), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      pv_totomagv.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128727749461562500, "Totale omaggi in valuta"), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      pv_vimpopv.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128727749461718750, "Imponibile in valuta"), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      pv_vprovv.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128727749461875000, "Provvigione in valuta"), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      pv_vimpvmat.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128727749462031250, "Maturato in valuta"), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      pv_vimpvpag.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128727749462187500, "Pagato in valuta"), oApp.FormatImporti, 20, -9999999999999, 9999999999999)
      pv_impscad.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129484743962529297, "Importo scadenza"), oApp.FormatImporti, 20, -9999999999999, 9999999999999)

      pv_tipdoc.NTSSetRichiesto()
      pv_numdoc.NTSSetRichiesto()
      pv_serie.NTSSetRichiesto()
      pv_anno.NTSSetRichiesto()
      pv_numrat.NTSSetRichiesto()
      pv_origine.NTSSetRichiesto()
      pv_codage.NTSSetRichiesto()

      grvGspv.AddColumnBackColor("backcolor_row") 'sempre nella InitControls

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
  Public Overridable Sub FRMPRGSPV_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      InitControls()
      SetForm()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMPRGSPV_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva(True) Then e.Cancel = True
  End Sub
  Public Overridable Sub FRMPRGSPV_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcGspv.Dispose()
      If Not dsGspv Is Nothing Then dsGspv.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick

    Dim frmNupv As FRMPRNUPV = Nothing
    frmNupv = CType(NTSNewFormModal("FRMPRNUPV"), FRMPRNUPV)
    Dim oCallParamTmp As New CLE__CLDP
    Try
      frmNupv.Init(oMenu, oCallParamTmp, DittaCorrente)
      frmNupv.oCleNupv = oCleGspv
      frmNupv.ShowDialog()
      If oCallParamTmp.bPar1 Then
        oCleGspv.nCodAge = NTSCInt(oCallParamTmp.dPar1)
        oCleGspv.strDesAge = oCallParamTmp.strParam
        oCleGspv.bApri = False
        Apri()
      End If
      tlbRecordNuovo_ItemClick(Nothing, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmNupv Is Nothing Then frmNupv.Dispose()
      frmNupv = Nothing
    End Try
  End Sub
  Public Overridable Sub tlbApri_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApri.ItemClick

    Dim frmSepv As FRMPRSEPV = Nothing
    frmSepv = CType(NTSNewFormModal("FRMPRSEPV"), FRMPRSEPV)
    Dim oCallParamTmp As New CLE__CLDP
    Try
      frmSepv.Init(oMenu, oCallParamTmp, DittaCorrente)
      frmSepv.oCleSepv = oCleGspv
      frmSepv.ShowDialog()
      If oCallParamTmp.bPar1 Then
        oCleGspv.nCodAge = NTSCInt(oCallParamTmp.dPar1)
        oCleGspv.strDesAge = oCallParamTmp.strParam
        oCleGspv.nCodConto = NTSCInt(oCallParamTmp.dPar2)
        oCleGspv.nCodPaga = NTSCInt(oCallParamTmp.dPar3)
        oCleGspv.nTipPaga = NTSCInt(oCallParamTmp.dPar4)
        oCleGspv.bApri = True
        oCleGspv.dateDataDocumentiDa = NTSCDate(oCallParamTmp.strPar1)
        oCleGspv.dateDataDocumentiA = NTSCDate(oCallParamTmp.strPar2)
        oCleGspv.dateScadenzaDocumentiDa = NTSCDate(oCallParamTmp.strPar3)
        oCleGspv.dateScadenzaDocumentiA = NTSCDate(oCallParamTmp.strPar4)
        oCleGspv.strStato = oCallParamTmp.strPar5
        Apri()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmSepv Is Nothing Then frmSepv.Dispose()
      frmSepv = Nothing
    End Try
  End Sub
  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not Salva(True) Then Return

      NTSFormClearDataBinding(Me)
      dsGspv.Tables.Clear()
      dsGspv.AcceptChanges()
      SetForm()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Try
      If dsGspv.Tables(strTabella).Rows.Count > 0 Then 'se non ci sono righe in griglia esco
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128729378705156250, "Scegliendo 'Sì' verranno eliminati tutti i dati visualizzati, procedere?")) = Windows.Forms.DialogResult.No Then
          Return
        End If

        pnGspv.Visible = False
        oCleGspv.Ripristina(dcGspv.Position, dcGspv.Filter)
        If dsGspv.Tables(strTabella).Rows.Count > 0 Then 'se dopo il ripristina non ci sono righe in griglia esco
          dcGspv.MoveFirst()
          For i As Integer = 0 To dsGspv.Tables(strTabella).Rows.Count - 1
            grvGspv.NTSDeleteRigaCorrente(dcGspv, False)
            dcGspv.MoveNext()
          Next
          If Not oCleGspv.Salva(True) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128729377991250000, "Errore durante l'eliminazione dei record, la cancellazione non è stata completata."))
          End If

        End If
      End If

      NTSFormClearDataBinding(Me)
      dsGspv.Tables.Clear()
      dsGspv.AcceptChanges()
      SetForm()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRecordNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordNuovo.ItemClick
    Try
      grvGspv.NTSNuovo()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRecordAggiorna_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordAggiorna.ItemClick
    Try
      Salva()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRecordCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordCancella.ItemClick
    Try
      If Not grvGspv.NTSDeleteRigaCorrente(dcGspv, True) Then Return
      oCleGspv.Salva(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRecordRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordRipristina.ItemClick
    Try
      If Not grvGspv.NTSRipristinaRigaCorrenteBefore(dcGspv, True) Then Return
      oCleGspv.Ripristina(dcGspv.Position, dcGspv.Filter)
      grvGspv.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      'zoom standard
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      '------------------------------------
      'zoom standard di textbox e griglia
      NTSCallStandardZoom()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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

  Public Overridable Sub grvGspv_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvGspv.NTSBeforeRowUpdate
    Try
      If Not Salva() Then e.Allow = False 'rimango sulla riga su cui sono
      AssegnaColoriGriglia(grvGspv.NTSGetCurrentDataRow)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub grvGspv_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvGspv.NTSFocusedRowChanged
    Try
      If Not pnGspv.Visible Then Return
      If grvGspv.FocusedRowHandle = -999999 Then Return

      If Not grvGspv.FocusedRowHandle = -999998 Then
        Select Case grvGspv.GetDataRow(grvGspv.FocusedRowHandle).RowState
          Case DataRowState.Added
            If Not pv_tipdoc.Enabled Then GctlSetVisEnab(pv_tipdoc, False)
            If Not pv_numdoc.Enabled Then GctlSetVisEnab(pv_numdoc, False)
            If Not pv_serie.Enabled Then GctlSetVisEnab(pv_serie, False)
            If Not pv_anno.Enabled Then GctlSetVisEnab(pv_anno, False)
            If Not pv_conto.Enabled Then GctlSetVisEnab(pv_conto, False)
            If Not pv_numrat.Enabled Then GctlSetVisEnab(pv_numrat, False)
            If Not pv_segno.Enabled Then GctlSetVisEnab(pv_segno, False)
            If Not pv_origine.Enabled Then GctlSetVisEnab(pv_origine, False)
          Case Else
            pv_tipdoc.Enabled = False
            pv_numdoc.Enabled = False
            pv_serie.Enabled = False
            pv_anno.Enabled = False
            If NTSCStr(grvGspv.NTSGetCurrentDataRow!pv_origine) = "T" Or _
               NTSCStr(grvGspv.NTSGetCurrentDataRow!pv_origine) = "Z" Then
              'il collegamento con scaden non deve essere modificato:
              pv_conto.Enabled = False
              pv_segno.Enabled = False
            End If
            pv_numrat.Enabled = False
            pv_origine.Enabled = False
        End Select
      Else
        If Not pv_tipdoc.Enabled Then GctlSetVisEnab(pv_tipdoc, False)
        If Not pv_numdoc.Enabled Then GctlSetVisEnab(pv_numdoc, False)
        If Not pv_serie.Enabled Then GctlSetVisEnab(pv_serie, False)
        If Not pv_anno.Enabled Then GctlSetVisEnab(pv_anno, False)
        If Not pv_numrat.Enabled Then GctlSetVisEnab(pv_numrat, False)
        If Not pv_origine.Enabled Then GctlSetVisEnab(pv_origine, False)
        grvGspv.NTSMoveFirstColunn()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Apri() As Boolean
    Try
      If Not oCleGspv.Apri(DittaCorrente, dsGspv) Then Return False

      dcGspv.DataSource = dsGspv.Tables(strTabella)
      For Each dtrT As DataRow In dsGspv.Tables(strTabella).Rows
        AssegnaColoriGriglia(dtrT)
      Next
      dsGspv.AcceptChanges()

      grGspv.DataSource = dcGspv

      SetForm()

      grvGspv_NTSFocusedRowChanged(Me, Nothing)

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function Salva(Optional ByVal bAsk As Boolean = False) As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvGspv.NTSSalvaRigaCorrente(dcGspv, oCleGspv.RecordIsChanged, False)

      If dRes = Windows.Forms.DialogResult.Yes AndAlso bAsk Then
        dRes = oApp.MsgBoxInfoYesNoCancel_DefYes(oApp.Tr(Me, 128562699674375000, "Vuoi salvare il record prima di procedere?"))
      End If

      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleGspv.Salva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleGspv.Ripristina(dcGspv.Position, dcGspv.Filter)
        Case System.Windows.Forms.DialogResult.Cancel
          'non posso fare nulla
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          'la riga non ha subito modifiche
      End Select
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function StatoNuovo() As Boolean
    Try
      If StatoVuoto() Then Return False
      If Not dsGspv.Tables(strTabella).Rows.Count > 0 Then Return False
      If dsGspv.Tables(strTabella).Rows(dcGspv.Position).RowState = DataRowState.Added Then Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function StatoVuoto() As Boolean
    Try
      If dsGspv Is Nothing Then Return True
      If dsGspv.Tables(strTabella) Is Nothing Then Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function StatoDataApri() As Boolean
    Try
      If Not StatoVuoto() Then
        If dsGspv.Tables(strTabella).Rows.Count > 0 Then Return True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function StatoDataLayout() As Boolean
    Try
      If Not StatoVuoto() Then
        If dsGspv.Tables(strTabella).Rows.Count = 0 Then Return True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub SetForm()
    Try
      SetToolBar()
      SetControls()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub SetToolBar()
    Try
      If StatoVuoto() Then
        tlbNuovo.Enabled = True
        tlbApri.Enabled = True
        tlbRipristina.Enabled = False
        tlbCancella.Enabled = False
        tlbZoom.Enabled = False
        tlbRecordNuovo.Enabled = False
        tlbRecordAggiorna.Enabled = False
        tlbRecordRipristina.Enabled = False
        tlbRecordCancella.Enabled = False
      Else
        tlbNuovo.Enabled = False
        tlbApri.Enabled = False
        tlbRipristina.Enabled = True
        tlbCancella.Enabled = True
        tlbZoom.Enabled = True
        tlbRecordNuovo.Enabled = True
        tlbRecordAggiorna.Enabled = True
        tlbRecordRipristina.Enabled = True
        tlbRecordCancella.Enabled = True
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub SetControls()
    Try
      If StatoVuoto() Then
        pnGspv.Visible = False
      Else
        lbDesAgente.Text = oCleGspv.strDesAge
        If oCleGspv.bApri Then
          lbDesDataDocumentiDa.Text = NTSCStr(NTSCDate(oCleGspv.dateDataDocumentiDa))
          lbDesDataDocumentiA.Text = NTSCStr(NTSCDate(oCleGspv.dateDataDocumentiA))
          lbDesScadenzaDocumentiDa.Text = NTSCStr(NTSCDate(oCleGspv.dateScadenzaDocumentiDa))
          lbDesScadenzaDocumentiA.Text = NTSCStr(NTSCDate(oCleGspv.dateScadenzaDocumentiA))
        Else
          lbDesDataDocumentiDa.Text = ""
          lbDesDataDocumentiA.Text = ""
          lbDesScadenzaDocumentiDa.Text = ""
          lbDesScadenzaDocumentiA.Text = ""
        End If
        pnGspv.Visible = True
        If oCleGspv.bApri Then
          grvGspv_NTSFocusedRowChanged(Nothing, Nothing)
        End If
        grvGspv.Focus()
        grvGspv.NTSMoveFirstColunn()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdPaga_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPaga.Click
    Dim strDtFineElab As String = ""
    Try
      strDtFineElab = oApp.InputBoxNew(oApp.Tr(Me, 130197474305984776, "Indicare la data fino alla quale pagare TUTTE le provvigioni maturate (anche quelle non visibili in griglia)"), DateTime.Now.ToShortDateString)
      If strDtFineElab.Trim = "" Then Return
      If Not IsDate(strDtFineElab) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130197475338724415, "La data inserita non è corretta. Elaborazione annullata"))
        Return
      End If

      Me.Cursor = Cursors.WaitCursor

      If oCleGspv.ElaboraPagato(strDtFineElab) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130197477030053132, "Elaborazione completata regolarmente." & vbCrLf & _
                         "Riaprire la griglia delle provvigioni per visualizzare i dati aggiornati"))
      Else
        oApp.MsgBoxErr(oApp.Tr(Me, 130197483213543102, "Si sono verificati degli errori durante l'elaborazione. Elaborazione annullata"))
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Function AssegnaColoriGriglia(ByRef dtrT As DataRow) As Boolean
    'coloro le celle
    Try
      'quando viene cambiato il colore della cella backcolor_row o della backcolor_*, 
      'sbs invia al client la segnalazione di 'cella cambiata'
      'su SBC la cellace backcolor_ non viene mai disegnata, ma viene utilizzata per colorare la riga/cella
      'NB: la colonna backcolor_ deve essere inserita anche nella griglia (magari non visibile) altrimenti non viene passata a SBC!!!

      'dtrIn.Table.Columns.Add("backcolor_row", GetType(Integer))        'costante 'backcolor_row' utilizzato da SBC per colorare tutta la riga
      'dtrIn.Table.Columns.Add("backcolor_xx_status", GetType(Integer))  'prefisso 'backcolor_' utilizzato da SBC per colorare la singola cella

      If dtrT Is Nothing Then Return True

      'memorizzo lo stato prima della modifica del campo
      Dim oState As DataRowState = dtrT.RowState
      Dim bOk As Boolean = oCleGspv.bHasChanges

      If dtrT.Table.Columns.Contains("backcolor_row") = False Then
        dtrT.Table.Columns.Add("backcolor_row", GetType(Integer))
      End If

      'per default non è
      dtrT("backcolor_row") = -1

      If NTSCStr(dtrT!pv_origine) = "Z" Then dtrT!backcolor_row = Color.FromArgb(255, 136, 136).ToArgb 'rosso

      'reimposto lo stato a prima della modifica del campo
      If oState = DataRowState.Unchanged Then dtrT.AcceptChanges()
      oCleGspv.bHasChanges = bOk

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
End Class
