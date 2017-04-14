Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__VLOG
  Private Moduli_P As Integer = CLN__STD.bsModAll
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

#Region "Variabili"
  Public dsVlog As DataSet
  Public oCleVlog As CLE__VLOG
  Public oCallParams As CLE__CLDP
  Public dcVlog As BindingSource = New BindingSource

  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbApri As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbSelezionaRighe As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDeselezionaRighe As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbOrdinaDataOra As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbOrdinaDescrizione As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbOrdinaOperatore As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbVisualizzaFile As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbVisualizzaFileRigheSel As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnVlog As NTSInformatica.NTSPanel
  Public WithEvents grVlog As NTSInformatica.NTSGrid
  Public WithEvents grvVlog As NTSInformatica.NTSGridView
  Public WithEvents al_opnome As NTSInformatica.NTSGridColumn
  Public WithEvents al_data As NTSInformatica.NTSGridColumn
  Public WithEvents al_desogglog As NTSInformatica.NTSGridColumn
  Public WithEvents al_nomprog As NTSInformatica.NTSGridColumn
  Public WithEvents codditt As NTSInformatica.NTSGridColumn
  Public WithEvents al_progr As NTSInformatica.NTSGridColumn
  Public WithEvents al_nomogglog As NTSInformatica.NTSGridColumn
  Public WithEvents al_nomtab As NTSInformatica.NTSGridColumn
  Public WithEvents al_tipoogglog As NTSInformatica.NTSGridColumn
  Public WithEvents al_tabprim As NTSInformatica.NTSGridColumn
  Public WithEvents al_tipoop As NTSInformatica.NTSGridColumn
  Public WithEvents al_key As NTSInformatica.NTSGridColumn
  Public WithEvents al_dati As NTSInformatica.NTSGridColumn
  Public WithEvents al_progrpadr As NTSInformatica.NTSGridColumn
  Public WithEvents xx_seleziona As NTSInformatica.NTSGridColumn
  Public WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit

  Private components As System.ComponentModel.IContainer
#End Region

  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__VLOG))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbApri = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbSelezionaRighe = New NTSInformatica.NTSBarButtonItem
    Me.tlbDeselezionaRighe = New NTSInformatica.NTSBarButtonItem
    Me.tlbOrdinaDataOra = New NTSInformatica.NTSBarButtonItem
    Me.tlbOrdinaDescrizione = New NTSInformatica.NTSBarButtonItem
    Me.tlbOrdinaOperatore = New NTSInformatica.NTSBarButtonItem
    Me.tlbVisualizzaFile = New NTSInformatica.NTSBarButtonItem
    Me.tlbVisualizzaFileRigheSel = New NTSInformatica.NTSBarButtonItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnVlog = New NTSInformatica.NTSPanel
    Me.grVlog = New NTSInformatica.NTSGrid
    Me.grvVlog = New NTSInformatica.NTSGridView
    Me.al_opnome = New NTSInformatica.NTSGridColumn
    Me.al_data = New NTSInformatica.NTSGridColumn
    Me.al_desogglog = New NTSInformatica.NTSGridColumn
    Me.al_nomprog = New NTSInformatica.NTSGridColumn
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.al_progr = New NTSInformatica.NTSGridColumn
    Me.al_nomogglog = New NTSInformatica.NTSGridColumn
    Me.al_nomtab = New NTSInformatica.NTSGridColumn
    Me.al_tipoogglog = New NTSInformatica.NTSGridColumn
    Me.al_tabprim = New NTSInformatica.NTSGridColumn
    Me.al_tipoop = New NTSInformatica.NTSGridColumn
    Me.al_key = New NTSInformatica.NTSGridColumn
    Me.al_dati = New NTSInformatica.NTSGridColumn
    Me.al_progrpadr = New NTSInformatica.NTSGridColumn
    Me.xx_seleziona = New NTSInformatica.NTSGridColumn
    Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnVlog, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnVlog.SuspendLayout()
    CType(Me.grVlog, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvVlog, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(231, Byte), Integer))
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'DevXDefaultLookAndFeel
    '

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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbApri, Me.tlbCancella, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbSelezionaRighe, Me.tlbDeselezionaRighe, Me.tlbOrdinaDataOra, Me.tlbOrdinaDescrizione, Me.tlbOrdinaOperatore, Me.tlbVisualizzaFile, Me.tlbVisualizzaFileRigheSel, Me.tlbImpostaStampante})
    Me.NtsBarManager1.MaxItemId = 18
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApri), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbApri
    '
    Me.tlbApri.Caption = "Apri"
    Me.tlbApri.Glyph = CType(resources.GetObject("tlbApri.Glyph"), System.Drawing.Image)
    Me.tlbApri.Id = 0
    Me.tlbApri.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3)
    Me.tlbApri.Name = "tlbApri"
    Me.tlbApri.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.Id = 1
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 2
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "StampaVideo"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 3
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 7
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSelezionaRighe), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDeselezionaRighe), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbOrdinaDataOra), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbOrdinaDescrizione), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbOrdinaOperatore), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbVisualizzaFile), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbVisualizzaFileRigheSel), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbSelezionaRighe
    '
    Me.tlbSelezionaRighe.Caption = "Seleziona tutte le righe"
    Me.tlbSelezionaRighe.Id = 10
    Me.tlbSelezionaRighe.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.J))
    Me.tlbSelezionaRighe.Name = "tlbSelezionaRighe"
    Me.tlbSelezionaRighe.Visible = True
    '
    'tlbDeselezionaRighe
    '
    Me.tlbDeselezionaRighe.Caption = "Deseleziona tutte le righe"
    Me.tlbDeselezionaRighe.Id = 11
    Me.tlbDeselezionaRighe.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.K))
    Me.tlbDeselezionaRighe.Name = "tlbDeselezionaRighe"
    Me.tlbDeselezionaRighe.Visible = True
    '
    'tlbOrdinaDataOra
    '
    Me.tlbOrdinaDataOra.Caption = "Ordina per data/ora"
    Me.tlbOrdinaDataOra.Id = 12
    Me.tlbOrdinaDataOra.Name = "tlbOrdinaDataOra"
    Me.tlbOrdinaDataOra.Visible = True
    '
    'tlbOrdinaDescrizione
    '
    Me.tlbOrdinaDescrizione.Caption = "Ordina per descrizione"
    Me.tlbOrdinaDescrizione.Id = 13
    Me.tlbOrdinaDescrizione.Name = "tlbOrdinaDescrizione"
    Me.tlbOrdinaDescrizione.Visible = True
    '
    'tlbOrdinaOperatore
    '
    Me.tlbOrdinaOperatore.Caption = "Ordina per operatore"
    Me.tlbOrdinaOperatore.Id = 14
    Me.tlbOrdinaOperatore.Name = "tlbOrdinaOperatore"
    Me.tlbOrdinaOperatore.Visible = True
    '
    'tlbVisualizzaFile
    '
    Me.tlbVisualizzaFile.Caption = "Visualizza in file di testo"
    Me.tlbVisualizzaFile.Id = 15
    Me.tlbVisualizzaFile.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F))
    Me.tlbVisualizzaFile.Name = "tlbVisualizzaFile"
    Me.tlbVisualizzaFile.Visible = True
    '
    'tlbVisualizzaFileRigheSel
    '
    Me.tlbVisualizzaFileRigheSel.Caption = "Visualizza in file di testo le righe selezionate"
    Me.tlbVisualizzaFileRigheSel.Id = 16
    Me.tlbVisualizzaFileRigheSel.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G))
    Me.tlbVisualizzaFileRigheSel.Name = "tlbVisualizzaFileRigheSel"
    Me.tlbVisualizzaFileRigheSel.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta stampante"
    Me.tlbImpostaStampante.Id = 17
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 5
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 6
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnVlog
    '
    Me.pnVlog.AllowDrop = True
    Me.pnVlog.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnVlog.Appearance.Options.UseBackColor = True
    Me.pnVlog.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnVlog.Controls.Add(Me.grVlog)
    Me.pnVlog.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnVlog.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnVlog.Location = New System.Drawing.Point(0, 30)
    Me.pnVlog.Name = "pnVlog"
    Me.pnVlog.Size = New System.Drawing.Size(680, 392)
    Me.pnVlog.TabIndex = 5
    Me.pnVlog.Text = "NtsPanel1"
    '
    'grVlog
    '
    Me.grVlog.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grVlog.EmbeddedNavigator.Name = ""
    Me.grVlog.Location = New System.Drawing.Point(0, 0)
    Me.grVlog.MainView = Me.grvVlog
    Me.grVlog.Name = "grVlog"
    Me.grVlog.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
    Me.grVlog.Size = New System.Drawing.Size(680, 392)
    Me.grVlog.TabIndex = 5
    Me.grVlog.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvVlog})
    '
    'grvVlog
    '
    Me.grvVlog.ActiveFilterEnabled = False
    Me.grvVlog.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.al_opnome, Me.al_data, Me.al_desogglog, Me.al_nomprog, Me.codditt, Me.al_progr, Me.al_nomogglog, Me.al_nomtab, Me.al_tipoogglog, Me.al_tabprim, Me.al_tipoop, Me.al_key, Me.al_dati, Me.al_progrpadr, Me.xx_seleziona})
    Me.grvVlog.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvVlog.Enabled = True
    Me.grvVlog.GridControl = Me.grVlog
    Me.grvVlog.Name = "grvVlog"
    Me.grvVlog.NTSAllowDelete = True
    Me.grvVlog.NTSAllowInsert = True
    Me.grvVlog.NTSAllowUpdate = True
    Me.grvVlog.NTSMenuContext = Nothing
    Me.grvVlog.OptionsCustomization.AllowRowSizing = True
    Me.grvVlog.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvVlog.OptionsNavigation.UseTabKey = False
    Me.grvVlog.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvVlog.OptionsView.ColumnAutoWidth = False
    Me.grvVlog.OptionsView.EnableAppearanceEvenRow = True
    Me.grvVlog.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvVlog.OptionsView.ShowGroupPanel = False
    Me.grvVlog.RowHeight = 14
    '
    'al_opnome
    '
    Me.al_opnome.AppearanceCell.Options.UseBackColor = True
    Me.al_opnome.AppearanceCell.Options.UseTextOptions = True
    Me.al_opnome.Caption = "Nome operatore"
    Me.al_opnome.Enabled = True
    Me.al_opnome.FieldName = "al_opnome"
    Me.al_opnome.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.al_opnome.Name = "al_opnome"
    Me.al_opnome.NTSRepositoryComboBox = Nothing
    Me.al_opnome.NTSRepositoryItemCheck = Nothing
    Me.al_opnome.NTSRepositoryItemText = Nothing
    Me.al_opnome.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.al_opnome.OptionsFilter.AllowFilter = False
    Me.al_opnome.Visible = True
    Me.al_opnome.VisibleIndex = 0
    '
    'al_data
    '
    Me.al_data.AppearanceCell.Options.UseBackColor = True
    Me.al_data.AppearanceCell.Options.UseTextOptions = True
    Me.al_data.Caption = "Data/ora"
    Me.al_data.Enabled = True
    Me.al_data.FieldName = "al_data"
    Me.al_data.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.al_data.Name = "al_data"
    Me.al_data.NTSRepositoryComboBox = Nothing
    Me.al_data.NTSRepositoryItemCheck = Nothing
    Me.al_data.NTSRepositoryItemText = Nothing
    Me.al_data.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.al_data.OptionsFilter.AllowFilter = False
    Me.al_data.Visible = True
    Me.al_data.VisibleIndex = 1
    '
    'al_desogglog
    '
    Me.al_desogglog.AppearanceCell.Options.UseBackColor = True
    Me.al_desogglog.AppearanceCell.Options.UseTextOptions = True
    Me.al_desogglog.Caption = "Attività di LOG"
    Me.al_desogglog.Enabled = True
    Me.al_desogglog.FieldName = "al_desogglog"
    Me.al_desogglog.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.al_desogglog.Name = "al_desogglog"
    Me.al_desogglog.NTSRepositoryComboBox = Nothing
    Me.al_desogglog.NTSRepositoryItemCheck = Nothing
    Me.al_desogglog.NTSRepositoryItemText = Nothing
    Me.al_desogglog.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.al_desogglog.OptionsFilter.AllowFilter = False
    Me.al_desogglog.Visible = True
    Me.al_desogglog.VisibleIndex = 2
    '
    'al_nomprog
    '
    Me.al_nomprog.AppearanceCell.Options.UseBackColor = True
    Me.al_nomprog.AppearanceCell.Options.UseTextOptions = True
    Me.al_nomprog.Caption = "Nome programma"
    Me.al_nomprog.Enabled = True
    Me.al_nomprog.FieldName = "al_nomprog"
    Me.al_nomprog.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.al_nomprog.Name = "al_nomprog"
    Me.al_nomprog.NTSRepositoryComboBox = Nothing
    Me.al_nomprog.NTSRepositoryItemCheck = Nothing
    Me.al_nomprog.NTSRepositoryItemText = Nothing
    Me.al_nomprog.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.al_nomprog.OptionsFilter.AllowFilter = False
    Me.al_nomprog.Visible = True
    Me.al_nomprog.VisibleIndex = 3
    '
    'codditt
    '
    Me.codditt.AppearanceCell.Options.UseBackColor = True
    Me.codditt.AppearanceCell.Options.UseTextOptions = True
    Me.codditt.Caption = "Ditta"
    Me.codditt.Enabled = True
    Me.codditt.FieldName = "codditt"
    Me.codditt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.codditt.Name = "codditt"
    Me.codditt.NTSRepositoryComboBox = Nothing
    Me.codditt.NTSRepositoryItemCheck = Nothing
    Me.codditt.NTSRepositoryItemText = Nothing
    Me.codditt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.codditt.OptionsFilter.AllowFilter = False
    Me.codditt.Visible = True
    Me.codditt.VisibleIndex = 4
    '
    'al_progr
    '
    Me.al_progr.AppearanceCell.Options.UseBackColor = True
    Me.al_progr.AppearanceCell.Options.UseTextOptions = True
    Me.al_progr.Caption = "Progressivo"
    Me.al_progr.Enabled = True
    Me.al_progr.FieldName = "al_progr"
    Me.al_progr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.al_progr.Name = "al_progr"
    Me.al_progr.NTSRepositoryComboBox = Nothing
    Me.al_progr.NTSRepositoryItemCheck = Nothing
    Me.al_progr.NTSRepositoryItemText = Nothing
    Me.al_progr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.al_progr.OptionsFilter.AllowFilter = False
    Me.al_progr.Visible = True
    Me.al_progr.VisibleIndex = 5
    '
    'al_nomogglog
    '
    Me.al_nomogglog.AppearanceCell.Options.UseBackColor = True
    Me.al_nomogglog.AppearanceCell.Options.UseTextOptions = True
    Me.al_nomogglog.Caption = "al_nomogglog"
    Me.al_nomogglog.Enabled = True
    Me.al_nomogglog.FieldName = "al_nomogglog"
    Me.al_nomogglog.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.al_nomogglog.Name = "al_nomogglog"
    Me.al_nomogglog.NTSRepositoryComboBox = Nothing
    Me.al_nomogglog.NTSRepositoryItemCheck = Nothing
    Me.al_nomogglog.NTSRepositoryItemText = Nothing
    Me.al_nomogglog.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.al_nomogglog.OptionsFilter.AllowFilter = False
    Me.al_nomogglog.Visible = True
    Me.al_nomogglog.VisibleIndex = 6
    '
    'al_nomtab
    '
    Me.al_nomtab.AppearanceCell.Options.UseBackColor = True
    Me.al_nomtab.AppearanceCell.Options.UseTextOptions = True
    Me.al_nomtab.Caption = "al_nomtab"
    Me.al_nomtab.Enabled = True
    Me.al_nomtab.FieldName = "al_nomtab"
    Me.al_nomtab.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.al_nomtab.Name = "al_nomtab"
    Me.al_nomtab.NTSRepositoryComboBox = Nothing
    Me.al_nomtab.NTSRepositoryItemCheck = Nothing
    Me.al_nomtab.NTSRepositoryItemText = Nothing
    Me.al_nomtab.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.al_nomtab.OptionsFilter.AllowFilter = False
    Me.al_nomtab.Visible = True
    Me.al_nomtab.VisibleIndex = 7
    '
    'al_tipoogglog
    '
    Me.al_tipoogglog.AppearanceCell.Options.UseBackColor = True
    Me.al_tipoogglog.AppearanceCell.Options.UseTextOptions = True
    Me.al_tipoogglog.Caption = "Tipo oggetto log"
    Me.al_tipoogglog.Enabled = True
    Me.al_tipoogglog.FieldName = "al_tipoogglog"
    Me.al_tipoogglog.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.al_tipoogglog.Name = "al_tipoogglog"
    Me.al_tipoogglog.NTSRepositoryComboBox = Nothing
    Me.al_tipoogglog.NTSRepositoryItemCheck = Nothing
    Me.al_tipoogglog.NTSRepositoryItemText = Nothing
    Me.al_tipoogglog.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.al_tipoogglog.OptionsFilter.AllowFilter = False
    Me.al_tipoogglog.Visible = True
    Me.al_tipoogglog.VisibleIndex = 8
    '
    'al_tabprim
    '
    Me.al_tabprim.AppearanceCell.Options.UseBackColor = True
    Me.al_tabprim.AppearanceCell.Options.UseTextOptions = True
    Me.al_tabprim.Caption = "al_tabprim"
    Me.al_tabprim.Enabled = True
    Me.al_tabprim.FieldName = "al_tabprim"
    Me.al_tabprim.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.al_tabprim.Name = "al_tabprim"
    Me.al_tabprim.NTSRepositoryComboBox = Nothing
    Me.al_tabprim.NTSRepositoryItemCheck = Nothing
    Me.al_tabprim.NTSRepositoryItemText = Nothing
    Me.al_tabprim.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.al_tabprim.OptionsFilter.AllowFilter = False
    Me.al_tabprim.Visible = True
    Me.al_tabprim.VisibleIndex = 9
    '
    'al_tipoop
    '
    Me.al_tipoop.AppearanceCell.Options.UseBackColor = True
    Me.al_tipoop.AppearanceCell.Options.UseTextOptions = True
    Me.al_tipoop.Caption = "Tipo modifica documento/record"
    Me.al_tipoop.Enabled = True
    Me.al_tipoop.FieldName = "al_tipoop"
    Me.al_tipoop.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.al_tipoop.Name = "al_tipoop"
    Me.al_tipoop.NTSRepositoryComboBox = Nothing
    Me.al_tipoop.NTSRepositoryItemCheck = Nothing
    Me.al_tipoop.NTSRepositoryItemText = Nothing
    Me.al_tipoop.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.al_tipoop.OptionsFilter.AllowFilter = False
    Me.al_tipoop.Visible = True
    Me.al_tipoop.VisibleIndex = 10
    '
    'al_key
    '
    Me.al_key.AppearanceCell.Options.UseBackColor = True
    Me.al_key.AppearanceCell.Options.UseTextOptions = True
    Me.al_key.Caption = "al_key"
    Me.al_key.Enabled = True
    Me.al_key.FieldName = "al_key"
    Me.al_key.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.al_key.Name = "al_key"
    Me.al_key.NTSRepositoryComboBox = Nothing
    Me.al_key.NTSRepositoryItemCheck = Nothing
    Me.al_key.NTSRepositoryItemText = Nothing
    Me.al_key.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.al_key.OptionsFilter.AllowFilter = False
    Me.al_key.Visible = True
    Me.al_key.VisibleIndex = 11
    '
    'al_dati
    '
    Me.al_dati.AppearanceCell.Options.UseBackColor = True
    Me.al_dati.AppearanceCell.Options.UseTextOptions = True
    Me.al_dati.Caption = "al_dati"
    Me.al_dati.Enabled = True
    Me.al_dati.FieldName = "al_dati"
    Me.al_dati.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.al_dati.Name = "al_dati"
    Me.al_dati.NTSRepositoryComboBox = Nothing
    Me.al_dati.NTSRepositoryItemCheck = Nothing
    Me.al_dati.NTSRepositoryItemText = Nothing
    Me.al_dati.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.al_dati.OptionsFilter.AllowFilter = False
    Me.al_dati.Visible = True
    Me.al_dati.VisibleIndex = 12
    '
    'al_progrpadr
    '
    Me.al_progrpadr.AppearanceCell.Options.UseBackColor = True
    Me.al_progrpadr.AppearanceCell.Options.UseTextOptions = True
    Me.al_progrpadr.Caption = "al_progrpadr"
    Me.al_progrpadr.Enabled = True
    Me.al_progrpadr.FieldName = "al_progrpadr"
    Me.al_progrpadr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.al_progrpadr.Name = "al_progrpadr"
    Me.al_progrpadr.NTSRepositoryComboBox = Nothing
    Me.al_progrpadr.NTSRepositoryItemCheck = Nothing
    Me.al_progrpadr.NTSRepositoryItemText = Nothing
    Me.al_progrpadr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.al_progrpadr.OptionsFilter.AllowFilter = False
    Me.al_progrpadr.Visible = True
    Me.al_progrpadr.VisibleIndex = 13
    '
    'xx_seleziona
    '
    Me.xx_seleziona.AppearanceCell.Options.UseBackColor = True
    Me.xx_seleziona.AppearanceCell.Options.UseTextOptions = True
    Me.xx_seleziona.Caption = "Seleziona"
    Me.xx_seleziona.ColumnEdit = Me.RepositoryItemCheckEdit1
    Me.xx_seleziona.Enabled = True
    Me.xx_seleziona.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_seleziona.Name = "xx_seleziona"
    Me.xx_seleziona.NTSRepositoryComboBox = Nothing
    Me.xx_seleziona.NTSRepositoryItemCheck = Nothing
    Me.xx_seleziona.NTSRepositoryItemText = Nothing
    Me.xx_seleziona.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_seleziona.OptionsFilter.AllowFilter = False
    Me.xx_seleziona.Visible = True
    Me.xx_seleziona.VisibleIndex = 14
    '
    'RepositoryItemCheckEdit1
    '
    Me.RepositoryItemCheckEdit1.AutoHeight = False
    Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
    '
    'FRM__VLOG
    '
    Me.ClientSize = New System.Drawing.Size(680, 422)
    Me.Controls.Add(Me.pnVlog)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.HelpContext = "*"
    Me.Name = "FRM__VLOG"
    Me.NTSLastControlFocussed = Me.grVlog
    Me.Text = "Visualizzazione LOG attività"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnVlog, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnVlog.ResumeLayout(False)
    CType(Me.grVlog, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvVlog, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbApri.GlyphPath = (oApp.ChildImageDir() & "\open.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir() & "\delete.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir() & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir() & "\prnscreen.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir() & "\options.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir() & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir() & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()
      '-------------------------------------------------
      'decodifico le colonne 'tipo oggetto log' e 'tipo modifica'
      Dim dttTipoOggLog As New DataTable

      dttTipoOggLog.Columns.Add("cod", GetType(String))
      dttTipoOggLog.Columns.Add("val", GetType(String))
      dttTipoOggLog.Rows.Add(New Object() {"E", "Elaborazione"})
      dttTipoOggLog.Rows.Add(New Object() {"D", "Documento"})
      dttTipoOggLog.AcceptChanges()

      Dim dttTipoMod As New DataTable
      dttTipoMod.Columns.Add("cod", GetType(String))
      dttTipoMod.Columns.Add("val", GetType(String))
      dttTipoMod.Rows.Add(New Object() {"A", "Annulla"})
      dttTipoMod.Rows.Add(New Object() {"M", "Modifica"})
      dttTipoMod.AcceptChanges()
      '-----------------------------------------------------------------------------------------------------------------------------------------
      grvVlog.NTSSetParam(oMenu, oApp.Tr(Me, 128735740262752000, "LOG attività"))
      al_opnome.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128735740283812000, "Nome operatore"), 20, False)
      al_data.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128735740296916000, "Data/ora"), False, "X", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      al_desogglog.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128735740311892000, "Attività di LOG"), 0, True)
      al_nomprog.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128735740326712000, "Nome programma"), 20, False)
      codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128735740340596000, "Ditta"), 20, True)
      al_progr.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128735740356664000, "Progressivo"), "0", 9, 0, 999999999)
      al_nomogglog.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128735740371952000, "al_nomogglog"), 30, False)
      al_nomtab.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128735740385680000, "al_nomtab"), 20, False)
      al_tipoogglog.NTSSetParamCMB(oMenu, oApp.Tr(Me, 131098682872459340, "Tipo oggetto log"), dttTipoOggLog, "val", "cod")
      al_tabprim.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128735740422964000, "al_tabprim"), 1, False)
      al_tipoop.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128735740436224000, "Tipo modifica documento/record"), dttTipoMod, "val", "cod") 'A = annulla, M = modifica
      al_key.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128735740451044000, "al_key"), 0, True)
      al_dati.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128735740466956000, "al_dati"), 0, True)
      al_progrpadr.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128735740481152000, "al_progrpadr"), "0", 9, 0, 999999999)
      xx_seleziona.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128735740495972000, "Seleziona"), "S", "N")

      'non faccio lanciare lo zoom dal controllo visto che non devo cambiare il contenuto del campo ma riposizionare il dataset!!!

      grvVlog.NTSAllowInsert = False
      grvVlog.NTSAllowUpdate = False

      al_nomogglog.Visible = False
      al_nomtab.Visible = False
      al_tipoogglog.Visible = True
      al_tabprim.Visible = False
      al_tipoop.Visible = True
      al_key.Visible = False
      al_dati.Visible = False
      al_progrpadr.Visible = False

      'tutte le colonne disabilitate tranne xx_seleziona
      al_opnome.Enabled = False
      al_data.Enabled = False
      al_desogglog.Enabled = False
      al_nomprog.Enabled = False
      codditt.Enabled = False
      al_progr.Enabled = False
      al_nomogglog.Enabled = False
      al_nomtab.Enabled = False
      al_tipoogglog.Enabled = False
      al_tabprim.Enabled = False
      al_tipoop.Enabled = False
      al_key.Enabled = False
      al_dati.Enabled = False
      al_progrpadr.Enabled = False

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
  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano già stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 


      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcVlog, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__VLOG", "BE__VLOG", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128328672233059763, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleVlog = CType(oTmp, CLE__VLOG)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__VLOG", strRemoteServer, strRemotePort)
    AddHandler oCleVlog.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleVlog.Init(oApp, NTSScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    '------------------------------------------------

    Return True
  End Function

#Region "EventiForm"
  Public Overridable Sub FRM__VLOG_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      '--------------------------------------------------------------------------------------------------------------
      oCleVlog.InserisciAttivitaMancanti()
      '--------------------------------------------------------------------------------------------------------------
      'predispongo i controlli
      InitControls()

      SetStato(0)

      oCleVlog.GetOrdinamento()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      'e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipodocumento, ecc ...
      'GctlTipoDoc = ""
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRM__VLOG_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcVlog.Dispose()
      dsVlog.Dispose()
    Catch
    End Try
  End Sub
  Public Overridable Sub FRM__VLOG_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      oCleVlog.SetOrdinamento()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "EventiToolbar"

  Public Overridable Sub tlbApri_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApri.ItemClick
    Try
      FileApri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Try
      FileCancella()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      Stampa(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSelezionaRighe_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSelezionaRighe.ItemClick
    Dim i As Integer
    Try
      '-------------------------------------------------------------------------------------
      '--- N.B.: Non seleziona le righe di tipo 'Elaborazione'
      '-------------------------------------------------------------------------------------
      For i = 0 To dsVlog.Tables("ACTLOG").Rows.Count - 1
        If NTSCStr(dsVlog.Tables("ACTLOG").Rows(i)!al_tipoogglog) <> "E" Then
          dsVlog.Tables("ACTLOG").Rows(i)!xx_seleziona = "S"
        End If
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbDeselezionaRighe_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDeselezionaRighe.ItemClick
    Dim i As Integer
    Try
      For i = 0 To dsVlog.Tables("ACTLOG").Rows.Count - 1
        dsVlog.Tables("ACTLOG").Rows(i)!xx_seleziona = "N"
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbOrdinaDataOra_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbOrdinaDataOra.ItemClick
    Try
      oCleVlog.nOrdinamento = 0
      Apri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbOrdinaDescrizione_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbOrdinaDescrizione.ItemClick
    Try
      oCleVlog.nOrdinamento = 1
      Apri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbOrdinaOperatore_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbOrdinaOperatore.ItemClick
    Try
      oCleVlog.nOrdinamento = 2
      Apri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbVisualizzaFile_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbVisualizzaFile.ItemClick
    Try
      If dsVlog.Tables("ACTLOG").Rows.Count > 0 Then
        oCleVlog.StampaSuFile(dsVlog.Tables("ACTLOG").Rows(dcVlog.Position))
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbVisualizzaFileRigheSel_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbVisualizzaFileRigheSel.ItemClick
    Dim dtrT() As DataRow
    Try
      dtrT = dsVlog.Tables("ACTLOG").Select("xx_seleziona = 'S'")
      oCleVlog.ReportFileSel(dtrT)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    Try
      oMenu.ReportImposta(Me)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
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

#Region "EventiGriglia"

  Public Overridable Sub grVlog_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grVlog.DoubleClick
    Try
      If dsVlog.Tables("ACTLOG").Rows.Count > 0 Then
        oCleVlog.StampaSuFile(dsVlog.Tables("ACTLOG").Rows(dcVlog.Position))
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  'L'unica colonna editabile è la colonna -check box 'Seleziona' 
  'Bisogna immediatamente confermare la selezione/deselezione di check-box
  'altrimenti chiamando il Configuratore della griglia con Ctrl+Shift+Click
  'viene presentato il messaggio: 'Salvare o ripristinare la riga prima di entrare nella gestione della configurazione'
  'che appare solo se la riga ha uno stato diverso da Unchanged
  Public Overridable Sub grvLog_NTSCellValueChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvVlog.NTSCellValueChanged
    Try
      dsVlog.AcceptChanges()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

#End Region

  Public Overridable Sub FileApri()
    Dim oParam As New CLE__CLDP
    Dim frmSvlo As FRM__SVLO = Nothing
    Try
      '-----------------------------------------------------------------------------------------
      '--- Si posiziona in Stato0
      '-----------------------------------------------------------------------------------------
      SetStato(0)
      '-----------------------------------------------------------------------------------------
      '--- Chiama la modale per la selezione dei dati
      '-----------------------------------------------------------------------------------------
      frmSvlo = CType(NTSNewFormModal("FRM__SVLO"), FRM__SVLO)
      frmSvlo.Init(oMenu, oParam, DittaCorrente)
      frmSvlo.InitEntity(oCleVlog)
      frmSvlo.ShowDialog()
      '-----------------------------------------------------------------------------------------
      '--- Se annullato esce
      '-----------------------------------------------------------------------------------------
      If oCleVlog.bSvloAnnullato = True Then Exit Sub
      '-----------------------------------------------------------------------------------------
      '--- Apre il DataControl collegato alla griglia
      '-----------------------------------------------------------------------------------------
      Apri()
      SetStato(1)
      '-----------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmSvlo Is Nothing Then frmSvlo.Dispose()
      frmSvlo = Nothing
    End Try
  End Sub
  Public Overridable Sub SetStato(ByVal i As Integer)
    Try
      Select Case i
        Case 0
          pnVlog.Visible = False
          tlbCancella.Enabled = False
          tlbSelezionaRighe.Enabled = False
          tlbDeselezionaRighe.Enabled = False
          tlbOrdinaDataOra.Enabled = False
          tlbOrdinaDescrizione.Enabled = False
          tlbOrdinaOperatore.Enabled = False
          tlbStampa.Enabled = False
          tlbStampaVideo.Enabled = False
          tlbVisualizzaFile.Enabled = False
          tlbVisualizzaFileRigheSel.Enabled = False
          tlbImpostaStampante.Enabled = False
          oCleVlog.lStato = 0
        Case 1
          pnVlog.Visible = True
          tlbCancella.Enabled = True
          tlbSelezionaRighe.Enabled = True
          tlbDeselezionaRighe.Enabled = True
          tlbOrdinaDataOra.Enabled = True
          tlbOrdinaDescrizione.Enabled = True
          tlbOrdinaOperatore.Enabled = True
          tlbStampa.Enabled = True
          tlbStampaVideo.Enabled = True
          tlbVisualizzaFile.Enabled = True
          tlbVisualizzaFileRigheSel.Enabled = True
          tlbImpostaStampante.Enabled = True
          oCleVlog.lStato = 1
      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub Apri()
    Dim i As Integer
    Try
      oCleVlog.binapri = True
      Me.Cursor = Cursors.WaitCursor
      oCleVlog.Apri(dsVlog)

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      dcVlog.DataSource = dsVlog.Tables("ACTLOG")
      grVlog.DataSource = dcVlog

      dsVlog.Tables("ACTLOG").Columns.Add(New DataColumn("xx_seleziona", GetType(String)))
      dsVlog.Tables("ACTLOG").Columns("xx_seleziona").DefaultValue = "N"

      For i = 0 To dsVlog.Tables("ACTLOG").Rows.Count - 1
        dsVlog.Tables("ACTLOG").Rows(i)!xx_seleziona = "N"
      Next
      dsVlog.AcceptChanges()

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()

      '-----------------------------------------------------------------------------------------
      '--- Se l'origine è da archivio procedura, nasconde la colonna relativa alla ditta
      '-----------------------------------------------------------------------------------------
      Select Case oCleVlog.nSvloOrigine
        Case 0
          codditt.Visible = True
        Case 1
          codditt.Visible = False
      End Select
      '-----------------------------------------------------------------------------------------
      grVlog.Focus()
      '-----------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
      oCleVlog.binapri = False
    End Try
  End Sub
  Public Overridable Sub FileCancella()
    Dim strMsg As String
    Try
      '-----------------------------------------------------------------------------------------
      If oCleVlog.lStato = 0 Then Exit Sub
      '-----------------------------------------------------------------------------------------
      '--- Non dovrebbe succedere...
      '-----------------------------------------------------------------------------------------
      If dsVlog.Tables("ACTLOG").Rows.Count = 0 Then Exit Sub
      '-----------------------------------------------------------------------------------------
      '--- Chiede conferma della cancellazione
      '-----------------------------------------------------------------------------------------
      strMsg = "Premendo 'Sì' saranno eliminate tutte le righe VISUALIZZATE." & vbCrLf & _
        "Confermare la cancellazione?"
      If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128340657368960941, strMsg)) = Windows.Forms.DialogResult.No Then Exit Sub
      '-----------------------------------------------------------------------------------------
      oCleVlog.FileCancella()
      '-----------------------------------------------------------------------------------------
      '--- Si posiziona in Stato0
      '-----------------------------------------------------------------------------------------
      SetStato(0)
      '-----------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object = Nothing
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer

    Try
      '-----------------------------------------------------------------------------------------
      '--- Compone la formula di selezione dei dati
      '-----------------------------------------------------------------------------------------
      strCrpe = ComponiFormula()

      '--------------------------------------------------
      'preparo il motore di stampa

      Select Case oCleVlog.nSvloOrigine
        Case 0
          nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BS--VLOG", "Reports1", " ", 0, nDestin, "BS--VLOG.RPT", False, "Visualizzazione LOG attività", False)
        Case 1
          nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BS--VLOG", "Reports2", " ", 0, nDestin, "BS--VLO1.RPT", False, "Visualizzazione LOG attività", True)
      End Select

      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce già il multireport)
      For i = 1 To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Function ComponiFormula() As String
    Dim strSQL As String
    ComponiFormula = ""
    Try
      '-----------------------------------------------------------------------------------------
      strSQL = "{actlog.al_tabprim} = 'S'"
      If oCleVlog.strSvloOpnome <> "" Then
        strSQL = strSQL & " And {actlog.al_opnome} = " & ConvStrRpt(oCleVlog.strSvloOpnome)
      End If
      If oCleVlog.bSvloSelDesogglog = True Then
        strSQL = strSQL & " And {actlog.al_nomogglog} like " & ConvStrRpt(oCleVlog.strSvloDesogglog)
      End If
      If (oCleVlog.nSvloOrigine = 0) And (oCleVlog.bSvloSelCodditt = True) Then
        strSQL = strSQL & " And {actlog.codditt} = " & ConvStrRpt(oCleVlog.strSvloCodditt)
      End If
      If (NTSCDate(oCleVlog.strSvloDatini) <> NTSCDate(IntSetDate("01/01/1900"))) Or (NTSCDate(oCleVlog.strSvloDatfin) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strSQL = strSQL & " And {actlog.al_data} In " & ConvDataRpt(oCleVlog.strSvloDatini) & " To " & ConvDataRpt(oCleVlog.strSvloDatfin)
      End If
      Select Case oCleVlog.nSvloTipoop
        Case 1 : strSQL = strSQL & " And {actlog.al_tipoop} = 'M'"
        Case 2 : strSQL = strSQL & " And {actlog.al_tipoop} = 'A'"
      End Select
      Select Case oCleVlog.nSvloTipoogglog
        Case 0 : strSQL = strSQL & " And {actlog.al_tipoogglog} = 'D'"
        Case 1 : strSQL = strSQL & " And {actlog.al_tipoogglog} = 'E'"
      End Select
      '-----------------------------------------------------------------------------------------
      ComponiFormula = strSQL

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

End Class
