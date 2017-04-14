Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__LSEL
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

#Region "Variabili"
  Public oCleLsel As CLE__LSEL
  Public oCallParams As CLE__CLDP
  Public dsLsel As DataSet
  Public dcLsel As BindingSource = New BindingSource()

  Public bClose As Boolean

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDettagli As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents grLsel As NTSInformatica.NTSGrid
  Public WithEvents grvLsel As NTSInformatica.NTSGridView
  Public WithEvents codditt As NTSInformatica.NTSGridColumn
  Public WithEvents tb_codlsel As NTSInformatica.NTSGridColumn
  Public WithEvents tb_deslsel As NTSInformatica.NTSGridColumn
  Public WithEvents tb_lselnote As NTSInformatica.NTSGridColumn
  Public WithEvents tb_dtcomp As NTSInformatica.NTSGridColumn
  Public WithEvents tb_tipolv As NTSInformatica.NTSGridColumn
  Public WithEvents tb_codragg As NTSInformatica.NTSGridColumn
  Public WithEvents tb_tipocl As NTSInformatica.NTSGridColumn
  Public WithEvents tb_opinc As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codragg As NTSInformatica.NTSGridColumn
  Public WithEvents xx_opinc As NTSInformatica.NTSGridColumn
#End Region

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__LSEL))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbDettagli = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grLsel = New NTSInformatica.NTSGrid
    Me.grvLsel = New NTSInformatica.NTSGridView
    Me.tb_codlsel = New NTSInformatica.NTSGridColumn
    Me.tb_deslsel = New NTSInformatica.NTSGridColumn
    Me.tb_lselnote = New NTSInformatica.NTSGridColumn
    Me.tb_dtcomp = New NTSInformatica.NTSGridColumn
    Me.tb_tipolv = New NTSInformatica.NTSGridColumn
    Me.tb_codragg = New NTSInformatica.NTSGridColumn
    Me.xx_codragg = New NTSInformatica.NTSGridColumn
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.tb_tipocl = New NTSInformatica.NTSGridColumn
    Me.tb_opinc = New NTSInformatica.NTSGridColumn
    Me.xx_opinc = New NTSInformatica.NTSGridColumn
    Me.tb_opnomeinc = New NTSInformatica.NTSGridColumn
    Me.tlbDuplica = New NTSInformatica.NTSBarButtonItem
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grLsel, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvLsel, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbDettagli, Me.tlbDuplica})
    Me.NtsBarManager1.MaxItemId = 20
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDuplica), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDettagli, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 15
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.Id = 16
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 4
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 5
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbDettagli
    '
    Me.tlbDettagli.Caption = "Dettagli"
    Me.tlbDettagli.Glyph = CType(resources.GetObject("tlbDettagli.Glyph"), System.Drawing.Image)
    Me.tlbDettagli.Id = 18
    Me.tlbDettagli.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D))
    Me.tlbDettagli.Name = "tlbDettagli"
    Me.tlbDettagli.Visible = True
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
    'grLsel
    '
    Me.grLsel.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grLsel.EmbeddedNavigator.Name = ""
    Me.grLsel.Location = New System.Drawing.Point(0, 30)
    Me.grLsel.MainView = Me.grvLsel
    Me.grLsel.Name = "grLsel"
    Me.grLsel.Size = New System.Drawing.Size(648, 412)
    Me.grLsel.TabIndex = 5
    Me.grLsel.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvLsel})
    '
    'grvLsel
    '
    Me.grvLsel.ActiveFilterEnabled = False
    Me.grvLsel.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tb_codlsel, Me.tb_deslsel, Me.tb_lselnote, Me.tb_dtcomp, Me.tb_tipolv, Me.tb_codragg, Me.xx_codragg, Me.codditt, Me.tb_tipocl, Me.tb_opinc, Me.xx_opinc, Me.tb_opnomeinc})
    Me.grvLsel.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvLsel.Enabled = True
    Me.grvLsel.GridControl = Me.grLsel
    Me.grvLsel.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvLsel.MinRowHeight = 14
    Me.grvLsel.Name = "grvLsel"
    Me.grvLsel.NTSAllowDelete = True
    Me.grvLsel.NTSAllowInsert = True
    Me.grvLsel.NTSAllowUpdate = True
    Me.grvLsel.NTSMenuContext = Nothing
    Me.grvLsel.OptionsCustomization.AllowRowSizing = True
    Me.grvLsel.OptionsFilter.AllowFilterEditor = False
    Me.grvLsel.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvLsel.OptionsNavigation.UseTabKey = False
    Me.grvLsel.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvLsel.OptionsView.ColumnAutoWidth = False
    Me.grvLsel.OptionsView.EnableAppearanceEvenRow = True
    Me.grvLsel.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvLsel.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvLsel.OptionsView.ShowGroupPanel = False
    Me.grvLsel.RowHeight = 16
    '
    'tb_codlsel
    '
    Me.tb_codlsel.AppearanceCell.Options.UseBackColor = True
    Me.tb_codlsel.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codlsel.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codlsel.Caption = "Codice"
    Me.tb_codlsel.Enabled = True
    Me.tb_codlsel.FieldName = "tb_codlsel"
    Me.tb_codlsel.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codlsel.Name = "tb_codlsel"
    Me.tb_codlsel.NTSRepositoryComboBox = Nothing
    Me.tb_codlsel.NTSRepositoryItemCheck = Nothing
    Me.tb_codlsel.NTSRepositoryItemMemo = Nothing
    Me.tb_codlsel.NTSRepositoryItemText = Nothing
    Me.tb_codlsel.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codlsel.OptionsFilter.AllowFilter = False
    Me.tb_codlsel.Visible = True
    Me.tb_codlsel.VisibleIndex = 0
    Me.tb_codlsel.Width = 70
    '
    'tb_deslsel
    '
    Me.tb_deslsel.AppearanceCell.Options.UseBackColor = True
    Me.tb_deslsel.AppearanceCell.Options.UseTextOptions = True
    Me.tb_deslsel.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_deslsel.Caption = "Descrizione"
    Me.tb_deslsel.Enabled = True
    Me.tb_deslsel.FieldName = "tb_deslsel"
    Me.tb_deslsel.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_deslsel.Name = "tb_deslsel"
    Me.tb_deslsel.NTSRepositoryComboBox = Nothing
    Me.tb_deslsel.NTSRepositoryItemCheck = Nothing
    Me.tb_deslsel.NTSRepositoryItemMemo = Nothing
    Me.tb_deslsel.NTSRepositoryItemText = Nothing
    Me.tb_deslsel.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_deslsel.OptionsFilter.AllowFilter = False
    Me.tb_deslsel.Visible = True
    Me.tb_deslsel.VisibleIndex = 1
    Me.tb_deslsel.Width = 70
    '
    'tb_lselnote
    '
    Me.tb_lselnote.AppearanceCell.Options.UseBackColor = True
    Me.tb_lselnote.AppearanceCell.Options.UseTextOptions = True
    Me.tb_lselnote.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_lselnote.Caption = "Note"
    Me.tb_lselnote.Enabled = True
    Me.tb_lselnote.FieldName = "tb_lselnote"
    Me.tb_lselnote.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_lselnote.Name = "tb_lselnote"
    Me.tb_lselnote.NTSRepositoryComboBox = Nothing
    Me.tb_lselnote.NTSRepositoryItemCheck = Nothing
    Me.tb_lselnote.NTSRepositoryItemMemo = Nothing
    Me.tb_lselnote.NTSRepositoryItemText = Nothing
    Me.tb_lselnote.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_lselnote.OptionsFilter.AllowFilter = False
    Me.tb_lselnote.Visible = True
    Me.tb_lselnote.VisibleIndex = 2
    Me.tb_lselnote.Width = 70
    '
    'tb_dtcomp
    '
    Me.tb_dtcomp.AppearanceCell.Options.UseBackColor = True
    Me.tb_dtcomp.AppearanceCell.Options.UseTextOptions = True
    Me.tb_dtcomp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_dtcomp.Caption = "Data compilazione"
    Me.tb_dtcomp.Enabled = True
    Me.tb_dtcomp.FieldName = "tb_dtcomp"
    Me.tb_dtcomp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_dtcomp.Name = "tb_dtcomp"
    Me.tb_dtcomp.NTSRepositoryComboBox = Nothing
    Me.tb_dtcomp.NTSRepositoryItemCheck = Nothing
    Me.tb_dtcomp.NTSRepositoryItemMemo = Nothing
    Me.tb_dtcomp.NTSRepositoryItemText = Nothing
    Me.tb_dtcomp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_dtcomp.OptionsFilter.AllowFilter = False
    Me.tb_dtcomp.Visible = True
    Me.tb_dtcomp.VisibleIndex = 3
    Me.tb_dtcomp.Width = 70
    '
    'tb_tipolv
    '
    Me.tb_tipolv.AppearanceCell.Options.UseBackColor = True
    Me.tb_tipolv.AppearanceCell.Options.UseTextOptions = True
    Me.tb_tipolv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_tipolv.Caption = "Tipo"
    Me.tb_tipolv.Enabled = True
    Me.tb_tipolv.FieldName = "tb_tipolv"
    Me.tb_tipolv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_tipolv.Name = "tb_tipolv"
    Me.tb_tipolv.NTSRepositoryComboBox = Nothing
    Me.tb_tipolv.NTSRepositoryItemCheck = Nothing
    Me.tb_tipolv.NTSRepositoryItemMemo = Nothing
    Me.tb_tipolv.NTSRepositoryItemText = Nothing
    Me.tb_tipolv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_tipolv.OptionsFilter.AllowFilter = False
    Me.tb_tipolv.Visible = True
    Me.tb_tipolv.VisibleIndex = 4
    Me.tb_tipolv.Width = 70
    '
    'tb_codragg
    '
    Me.tb_codragg.AppearanceCell.Options.UseBackColor = True
    Me.tb_codragg.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codragg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codragg.Caption = "C. raggr."
    Me.tb_codragg.Enabled = True
    Me.tb_codragg.FieldName = "tb_codragg"
    Me.tb_codragg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codragg.Name = "tb_codragg"
    Me.tb_codragg.NTSRepositoryComboBox = Nothing
    Me.tb_codragg.NTSRepositoryItemCheck = Nothing
    Me.tb_codragg.NTSRepositoryItemMemo = Nothing
    Me.tb_codragg.NTSRepositoryItemText = Nothing
    Me.tb_codragg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codragg.OptionsFilter.AllowFilter = False
    Me.tb_codragg.Visible = True
    Me.tb_codragg.VisibleIndex = 5
    Me.tb_codragg.Width = 70
    '
    'xx_codragg
    '
    Me.xx_codragg.AppearanceCell.Options.UseBackColor = True
    Me.xx_codragg.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codragg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codragg.Caption = "Des. raggruppamento"
    Me.xx_codragg.Enabled = False
    Me.xx_codragg.FieldName = "xx_codragg"
    Me.xx_codragg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codragg.Name = "xx_codragg"
    Me.xx_codragg.NTSRepositoryComboBox = Nothing
    Me.xx_codragg.NTSRepositoryItemCheck = Nothing
    Me.xx_codragg.NTSRepositoryItemMemo = Nothing
    Me.xx_codragg.NTSRepositoryItemText = Nothing
    Me.xx_codragg.OptionsColumn.AllowEdit = False
    Me.xx_codragg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codragg.OptionsColumn.ReadOnly = True
    Me.xx_codragg.OptionsFilter.AllowFilter = False
    Me.xx_codragg.Visible = True
    Me.xx_codragg.VisibleIndex = 6
    Me.xx_codragg.Width = 70
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
    Me.codditt.Width = 70
    '
    'tb_tipocl
    '
    Me.tb_tipocl.AppearanceCell.Options.UseBackColor = True
    Me.tb_tipocl.AppearanceCell.Options.UseTextOptions = True
    Me.tb_tipocl.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_tipocl.Caption = "Tipo"
    Me.tb_tipocl.Enabled = True
    Me.tb_tipocl.FieldName = "tb_tipocl"
    Me.tb_tipocl.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_tipocl.Name = "tb_tipocl"
    Me.tb_tipocl.NTSRepositoryComboBox = Nothing
    Me.tb_tipocl.NTSRepositoryItemCheck = Nothing
    Me.tb_tipocl.NTSRepositoryItemMemo = Nothing
    Me.tb_tipocl.NTSRepositoryItemText = Nothing
    Me.tb_tipocl.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_tipocl.OptionsFilter.AllowFilter = False
    Me.tb_tipocl.Visible = True
    Me.tb_tipocl.VisibleIndex = 7
    Me.tb_tipocl.Width = 70
    '
    'tb_opinc
    '
    Me.tb_opinc.AppearanceCell.Options.UseBackColor = True
    Me.tb_opinc.AppearanceCell.Options.UseTextOptions = True
    Me.tb_opinc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_opinc.Caption = "Op. incaricato"
    Me.tb_opinc.Enabled = True
    Me.tb_opinc.FieldName = "tb_opinc"
    Me.tb_opinc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_opinc.Name = "tb_opinc"
    Me.tb_opinc.NTSRepositoryComboBox = Nothing
    Me.tb_opinc.NTSRepositoryItemCheck = Nothing
    Me.tb_opinc.NTSRepositoryItemMemo = Nothing
    Me.tb_opinc.NTSRepositoryItemText = Nothing
    Me.tb_opinc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_opinc.OptionsFilter.AllowFilter = False
    Me.tb_opinc.Width = 70
    '
    'xx_opinc
    '
    Me.xx_opinc.AppearanceCell.Options.UseBackColor = True
    Me.xx_opinc.AppearanceCell.Options.UseTextOptions = True
    Me.xx_opinc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_opinc.Caption = "Des. op. incaricato"
    Me.xx_opinc.Enabled = False
    Me.xx_opinc.FieldName = "xx_opinc"
    Me.xx_opinc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_opinc.Name = "xx_opinc"
    Me.xx_opinc.NTSRepositoryComboBox = Nothing
    Me.xx_opinc.NTSRepositoryItemCheck = Nothing
    Me.xx_opinc.NTSRepositoryItemMemo = Nothing
    Me.xx_opinc.NTSRepositoryItemText = Nothing
    Me.xx_opinc.OptionsColumn.AllowEdit = False
    Me.xx_opinc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_opinc.OptionsColumn.ReadOnly = True
    Me.xx_opinc.OptionsFilter.AllowFilter = False
    Me.xx_opinc.Width = 70
    '
    'tb_opnomeinc
    '
    Me.tb_opnomeinc.AppearanceCell.Options.UseBackColor = True
    Me.tb_opnomeinc.AppearanceCell.Options.UseTextOptions = True
    Me.tb_opnomeinc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_opnomeinc.Caption = "Nome operatore"
    Me.tb_opnomeinc.Enabled = True
    Me.tb_opnomeinc.FieldName = "tb_opnomeinc"
    Me.tb_opnomeinc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_opnomeinc.Name = "tb_opnomeinc"
    Me.tb_opnomeinc.NTSRepositoryComboBox = Nothing
    Me.tb_opnomeinc.NTSRepositoryItemCheck = Nothing
    Me.tb_opnomeinc.NTSRepositoryItemMemo = Nothing
    Me.tb_opnomeinc.NTSRepositoryItemText = Nothing
    Me.tb_opnomeinc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_opnomeinc.OptionsFilter.AllowFilter = False
    Me.tb_opnomeinc.Visible = True
    Me.tb_opnomeinc.VisibleIndex = 8
    '
    'tlbDuplica
    '
    Me.tlbDuplica.Caption = "Duplica"
    Me.tlbDuplica.Glyph = CType(resources.GetObject("tlbDuplica.Glyph"), System.Drawing.Image)
    Me.tlbDuplica.Id = 19
    Me.tlbDuplica.Name = "tlbDuplica"
    Me.tlbDuplica.Visible = True
    '
    'FRM__LSEL
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.grLsel)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRM__LSEL"
    Me.NTSLastControlFocussed = Me.grLsel
    Me.Text = "LISTA SELEZIONATA CLIENTI/FORNITORI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grLsel, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvLsel, System.ComponentModel.ISupportInitialize).EndInit()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__LSEL", "BE__LSEL", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128520494605418990, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleLsel = CType(oTmp, CLE__LSEL)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__LSEL", strRemoteServer, strRemotePort)
    AddHandler oCleLsel.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleLsel.Init(oApp, oScript, oMenu.oCleComm, "TABLSEL", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttTb_tipolv As New DataTable()
    Dim dttTb_tipocl As New DataTable()
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbDuplica.GlyphPath = (oApp.ChildImageDir & "\duplica.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbDettagli.GlyphPath = (oApp.ChildImageDir & "\prngrid.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      dttTb_tipolv.Columns.Add("cod", GetType(String))
      dttTb_tipolv.Columns.Add("val", GetType(String))
      dttTb_tipolv.Rows.Add(New Object() {"L", "Lista selezionata"})
      dttTb_tipolv.Rows.Add(New Object() {"V", "Valori raggruppamenti"})
      dttTb_tipolv.AcceptChanges()

      dttTb_tipocl.Columns.Add("cod", GetType(String))
      dttTb_tipocl.Columns.Add("val", GetType(String))
      dttTb_tipocl.Rows.Add(New Object() {"C", "Clienti/Fornitori"})
      dttTb_tipocl.Rows.Add(New Object() {"L", "Leads"})
      dttTb_tipocl.AcceptChanges()

      grvLsel.NTSSetParam(oMenu, "LISTA SELEZIONATA CLIENTI/FORNITORI")
      codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128520494601668942, "Cod. ditta"), 12, True)
      tb_codlsel.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128520494601825194, "Codice"), tablsel)
      tb_deslsel.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128520494601981446, "Descrizione"), 50, True)
      tb_lselnote.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128520494602137698, "Note"), 0, True)
      tb_dtcomp.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128520494602293950, "Data compilazione"), True, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      tb_tipolv.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128520494604481478, "Tipo"), dttTb_tipolv, "val", "cod")
      tb_codragg.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128520494604637730, "C. raggr."), tabragg)
      tb_tipocl.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128520494604793982, "Tipo"), dttTb_tipocl, "val", "cod")
      tb_opinc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128520494604950234, "Op. incaricato"), "0", 9, 0, 999999999)
      xx_codragg.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128520494605106486, "Des. raggruppamento"), 0, True)
      xx_opinc.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128521326011676110, "Des. op. incaricato"), 0, True)

      tb_codlsel.NTSSetParamZoom("")
      tb_codlsel.NTSSetRichiesto()

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

  Public Overloads Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    Dim strTmp() As String
    Dim i As Integer = 0

    If Not IsMyThrowRemoteEvent() Then Return
    MyBase.GestisciEventiEntity(sender, e)

    Try
      '--------------------------------------------------------------------------------------------------------------
      If e.TipoEvento.Trim.Length < 10 Then Return
      '--------------------------------------------------------------------------------------------------------------
      strTmp = e.TipoEvento.Split(CType("|", Char))
      '--------------------------------------------------------------------------------------------------------------
      For i = 0 To (strTmp.Length - 1)
        Select Case strTmp(i).Substring(0, 10)
          Case "CHKOPERAT:"
            If oMenu.ValidaOpnomeInc(DittaCorrente, oApp.User.Nome, e.Message, 0) Then
              e.RetValue = CLN__STD.ThMsg.RETVALUE_OK
            Else
              e.RetValue = CLN__STD.ThMsg.RETVALUE_NO
            End If
        End Select
      Next
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

#Region "Eventi di Form"
  Public Overridable Sub FRM__LSEL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim i As Integer

    Try
      '--------------------------------------------------------------------------------------------------------------
      bClose = False
      '--------------------------------------------------------------------------------------------------------------
      If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And CLN__STD.bsModExtCRM) Or _
         CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And CLN__STD.bsModSupWCR) Then
        oCleLsel.bModuloCRM = True
      Else
        oCleLsel.bModuloCRM = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCleLsel.bModuloCRM = True Then
        oCleLsel.bIsCRMUser = oMenu.IsCrmUser(DittaCorrente, oCleLsel.bAmm, oCleLsel.strAccvis, oCleLsel.strAccmod, oCleLsel.strRegvis, oCleLsel.strRegmod)
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Predispongo i controlli
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      '--- Leggo dal database i dati e collego il NTSBindingNavigator
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleLsel.Apri(DittaCorrente, dsLsel) Then Me.Close()
      dcLsel.DataSource = dsLsel.Tables("TABLSEL")
      dsLsel.AcceptChanges()
      grLsel.DataSource = dcLsel
      '--------------------------------------------------------------------------------------------------------------
      '---- Sempre alla fine di questa funzione: applico le regole della gctl
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
      '--- Sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
      '--------------------------------------------------------------------------------------------------------------
      If Not oCallParams Is Nothing Then
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
          If grvLsel.NTSAllowInsert Then
            grvLsel.NTSNuovo()
          End If
        ElseIf Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) <> "" Then
          For i = 0 To dcLsel.List.Count - 1
            If CType(dcLsel.Item(i), DataRowView)!tb_codlsel.ToString = Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) Then
              dcLsel.Position = i
              Exit For
            End If
          Next
        End If
      End If  'If Not oCallParams Is Nothing Then
      '--------------------------------------------------------------------------------------------------------------
      AbilDisabControlli()
      '--------------------------------------------------------------------------------------------------------------
      If oCleLsel.bModuloCRM = False Then
        tb_opinc.Visible = False
        xx_opinc.Visible = False
      Else
        If oCleLsel.strAccvis = "P" Then tb_opinc.Enabled = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione Friendly nasconde, sempre, alcuni controlli
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = True Then
        tb_tipolv.Visible = False
        tb_codragg.Visible = False
        xx_codragg.Visible = False
        tb_tipocl.Visible = False
        tb_opinc.Visible = False
        xx_opinc.Visible = False
        tb_opnomeinc.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRM__LSEL_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      If bClose Then
        Me.Close()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__LSEL_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub FRM__LSEL_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcLsel.Dispose()
      dsLsel.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvLsel.NTSNuovo()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Dim strCodLsel As String = ""
    Try
      If grvLsel.NTSGetCurrentDataRow Is Nothing Then Return
      strCodLsel = NTSCStr(dsLsel.Tables("TABLSEL").Rows(dcLsel.Position)!tb_codlsel)
      If Not grvLsel.NTSDeleteRigaCorrente(dcLsel, True) Then Return
      If Not oCleLsel.DeleteListsel(strCodLsel) Then Return
      oCleLsel.Salva(True)
      AbilDisabControlli()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvLsel.NTSRipristinaRigaCorrenteBefore(dcLsel, True) Then Return
      oCleLsel.Ripristina(dcLsel.Position, dcLsel.Filter)
      grvLsel.NTSRipristinaRigaCorrenteAfter()
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

      If ctrlTmp.GetType.ToString.IndexOf("NTSGrid") > -1 Then
        SetFastZoom(NTSCStr(CType(CType(ctrlTmp, NTSGrid).DefaultView, NTSGridView).EditingValue), oParam)

        If grLsel.Focused And grvLsel.FocusedColumn.Name.ToLower = "tb_opinc" Then
          '---------------------------------------------------------------------------------
          '--- Non permette la selezione di un operatore incaricato se esiste il modulo CRM
          '--- e se è permessa la visualizzazione dei soli dati personali
          '---------------------------------------------------------------------------------
          If (oCleLsel.bModuloCRM = True) And (oCleLsel.strAccvis = "P") Then Exit Sub
          'zoom su organig
          SetFastZoom(NTSCStr(CType(CType(ctrlTmp, NTSGrid).DefaultView, NTSGridView).EditingValue), oParam)
          oParam.lContoCF = 0
          oParam.strTipo = "I"
          NTSZOOM.strIn = NTSCStr(grvLsel.EditingValue)
          NTSZOOM.ZoomStrIn("ZOOMORGANIG", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvLsel.EditingValue) Then grvLsel.SetFocusedRowCellValue(tb_opinc, NTSZOOM.strIn)
          Return
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
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      If Not Salva() Then Return
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      If Not Salva() Then Return
      Stampa(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDettagli_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDettagli.ItemClick
    Dim oParam As New CLE__CLDP
    Dim dtrT As DataRow = Nothing
    Dim dsListSel As DataSet = Nothing
    Dim frmElse As FRM__ELSE = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      Salva()
      '--------------------------------------------------------------------------------------------------------------
      If grvLsel.NTSGetCurrentDataRow() Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      oCleLsel.strCodLSel = NTSCStr(grvLsel.NTSGetCurrentDataRow()!tb_codlsel)
      If Trim(oCleLsel.strCodLSel) = "" Then Return
      oCleLsel.nElseCodragg = NTSCInt(grvLsel.NTSGetCurrentDataRow()!tb_codragg)
      oCleLsel.strElseTipocl = NTSCStr(grvLsel.NTSGetCurrentDataRow()!tb_tipocl)
      frmElse = CType(NTSNewFormModal("FRM__ELSE"), FRM__ELSE)
      frmElse.Init(oMenu, oParam, DittaCorrente)
      frmElse.InitEntity(oCleLsel)
      frmElse.bConsentiModifica = True
      If (oCleLsel.bModuloCRM = True) And (oCleLsel.bIsCRMUser = True) Then
        oMenu.CercaAccessiCrmDaTablsel(DittaCorrente, NTSCInt(grvLsel.NTSGetCurrentDataRow()!tb_codlsel), oCleLsel.bAccmod)
        frmElse.bConsentiModifica = oCleLsel.bAccmod
      End If
      frmElse.ShowDialog()
      '--------------------------------------------------------------------------------------------------------------
      '--- Al ritorno della modale relativa al dettaglio, se non esistono righe in LISTSEL
      '--- sblocca la colonna del tipo, altrimenti la blocca
      '--------------------------------------------------------------------------------------------------------------
      dtrT = grvLsel.NTSGetCurrentDataRow
      If NTSCInt(dtrT!tb_codlsel) <> 0 Then
        oCleLsel.GetListSel(NTSCStr(dtrT!tb_codlsel), dsListSel)
        If dsListSel.Tables("LISTSEL").Rows.Count > 0 Then tb_tipocl.Enabled = False Else tb_tipocl.Enabled = True
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      If Not frmElse Is Nothing Then frmElse.Dispose()
      frmElse = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub

#End Region

#Region "Eventi Griglia"
  Public Overridable Sub grvLsel_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvLsel.NTSBeforeRowUpdate
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
  Public Overridable Sub grvLsel_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvLsel.NTSFocusedRowChanged
    '----------------------------------------------------------------------------------------------------------------
    '--- Blocco le colonne non modificabili
    '----------------------------------------------------------------------------------------------------------------
    Dim dtrT As DataRow = Nothing
    Dim dsListSel As DataSet = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleLsel Is Nothing Then Return
      dtrT = grvLsel.NTSGetCurrentDataRow
      '--------------------------------------------------------------------------------------------------------------
      '--- Sono su una nuova riga
      '--------------------------------------------------------------------------------------------------------------
      If dtrT Is Nothing Then
        tb_codlsel.Enabled = True
        tb_tipolv.Enabled = True
        tb_tipocl.Enabled = True
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(dtrT!tb_codlsel) <> 0 Then
        oCleLsel.GetListSel(NTSCStr(dtrT!tb_codlsel), dsListSel)
        If dsListSel.Tables("LISTSEL").Rows.Count > 0 Then
          tb_tipocl.Enabled = False
        Else
          tb_tipocl.Enabled = True
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(dtrT!tb_codlsel) <> 0 Then
        tb_codlsel.Enabled = False
        tb_tipolv.Enabled = False
      Else
        tb_codlsel.Enabled = True
        tb_tipolv.Enabled = True
      End If
      '--------------------------------------------------------------------------------------------------------------      
      If (oCleLsel.bModuloCRM = True) And (oCleLsel.bIsCRMUser = True) Then
        grvLsel.NTSAllowDelete = True
        grvLsel.NTSAllowUpdate = True
        GctlSetVisEnab(tlbSalva, False)
        GctlSetVisEnab(tlbCancella, False)
        GctlSetVisEnab(tlbZoom, False)
        If Not (grvLsel.NTSGetCurrentDataRow Is Nothing) Then
          oMenu.CercaAccessiCrmDaTablsel(DittaCorrente, NTSCInt(dtrT!tb_codlsel), oCleLsel.bAccmod)
          If oCleLsel.bAccmod = False Then
            grvLsel.NTSAllowDelete = False
            grvLsel.NTSAllowUpdate = False
            tlbSalva.Enabled = False
            tlbCancella.Enabled = False
            tlbZoom.Enabled = False
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvLsel.NTSSalvaRigaCorrente(dcLsel, oCleLsel.RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleLsel.Salva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleLsel.Ripristina(dcLsel.Position, dcLsel.Filter)
        Case System.Windows.Forms.DialogResult.Cancel
          'non posso fare nulla
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          'la riga non ha subito modifiche
      End Select

      AbilDisabControlli()

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer

    Try
      '--------------------------------------------------
      'preparo il motore di stampa
      strCrpe = "{tablsel.codditt} = '" & DittaCorrente & "'"
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BS--LSEL", "Reports1", " ", 0, nDestin, "BS--LSEL.RPT", False, "LISTA SELEZIONATA CLIENTI/FORNITORI", False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce già il multireport)
      For i = LBound(CType(nPjob, Array), 2) To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub AbilDisabControlli()
    Try
      If dsLsel.Tables("TABLSEL").Rows.Count = 0 Then
        tlbDettagli.Visible = False
        tlbStampaVideo.Visible = False
        tlbStampa.Visible = False
        tlbImpostaStampante.Visible = False
      Else
        tlbDettagli.Visible = True
        tlbStampaVideo.Visible = True
        tlbStampa.Visible = True
        tlbImpostaStampante.Visible = True
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDuplica_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDuplica.ItemClick
    Dim strCodlistsel As String
    Dim strNewCodlistsel As String
    Try
      If grvLsel.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130354650523211037, "Posizionarsi su una lista esistente da duplicare."))
        Return
      End If
      strNewCodlistsel = oApp.InputBoxNew("NUOVO CODICE LISTA SELEZIONATA")

      If strNewCodlistsel <> "" Then
        If Not IsNumeric(strNewCodlistsel) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130354650452741385, "Inserire un codice numerico per la nuova lista selezionata."))
          Return
        End If
        If NTSCInt(strNewCodlistsel) <= 0 Or NTSCInt(strNewCodlistsel) > 9999 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130354650476179185, "Inserire un codice numerico all'interno del intervallo 1-9999"))
          Return
        End If
        If oMenu.ValCodiceDb(strNewCodlistsel, DittaCorrente, "TABLSEL", "N") Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130354650498835725, "Codice nuova lista selezionata già esistente."))
          Return
        End If

        strCodlistsel = NTSCStr(grvLsel.NTSGetCurrentDataRow!tb_codlsel)

        oCleLsel.Duplica(strCodlistsel, strNewCodlistsel)

        If Not oCleLsel.Apri(DittaCorrente, dsLsel) Then Me.Close()
        dcLsel.DataSource = dsLsel.Tables("TABLSEL")
        dsLsel.AcceptChanges()
        grLsel.DataSource = dcLsel
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class
