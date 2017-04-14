Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__NUMG

  Public oCleNumg As CLE__NUMG
  Public oCallParams As CLE__CLDP
  Public dsNumg As DataSet
  Public dcNumg As BindingSource = New BindingSource()
  Public strTipoSelected As String = ""

  Private Moduli_P As Integer = CLN__STD.bsModAll
  Private ModuliExt_P As Integer = CLN__STD.bsModExtAll
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


#Region "Definizione Controlli"
  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents grDati As NTSInformatica.NTSGrid
  Public WithEvents grvDati As NTSInformatica.NTSGridView
  Public WithEvents tb_numserieg As NTSInformatica.NTSGridColumn
  Public WithEvents xx_numserieg As NTSInformatica.NTSGridColumn
  Public WithEvents tb_numcodlg As NTSInformatica.NTSGridColumn
  Public WithEvents xx_numcodlg As NTSInformatica.NTSGridColumn
  Public WithEvents tb_numprogg As NTSInformatica.NTSGridColumn
  Public WithEvents liTipo As NTSInformatica.NTSListBox
  Public WithEvents fmTipo As NTSInformatica.NTSGroupBox
  Public WithEvents fmNumerazioni As NTSInformatica.NTSGroupBox
  Public WithEvents tb_note As NTSInformatica.NTSGridColumn
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
#End Region


  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__NUMG))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grDati = New NTSInformatica.NTSGrid
    Me.grvDati = New NTSInformatica.NTSGridView
    Me.tb_numserieg = New NTSInformatica.NTSGridColumn
    Me.xx_numserieg = New NTSInformatica.NTSGridColumn
    Me.tb_numcodlg = New NTSInformatica.NTSGridColumn
    Me.xx_numcodlg = New NTSInformatica.NTSGridColumn
    Me.tb_numprogg = New NTSInformatica.NTSGridColumn
    Me.tb_note = New NTSInformatica.NTSGridColumn
    Me.liTipo = New NTSInformatica.NTSListBox
    Me.fmTipo = New NTSInformatica.NTSGroupBox
    Me.fmNumerazioni = New NTSInformatica.NTSGroupBox
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grDati, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvDati, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.liTipo, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmTipo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmTipo.SuspendLayout()
    CType(Me.fmNumerazioni, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmNumerazioni.SuspendLayout()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    Me.frmPopup.ClientSize = New System.Drawing.Size(77, 21)
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 11
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbRipristina.Id = 3
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.Id = 2
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 4
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
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
    'grDati
    '
    Me.grDati.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grDati.EmbeddedNavigator.Name = ""
    Me.grDati.Location = New System.Drawing.Point(2, 20)
    Me.grDati.MainView = Me.grvDati
    Me.grDati.Name = "grDati"
    Me.grDati.Size = New System.Drawing.Size(496, 318)
    Me.grDati.TabIndex = 4
    Me.grDati.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDati})
    '
    'grvDati
    '
    Me.grvDati.ActiveFilterEnabled = False
    Me.grvDati.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tb_numserieg, Me.xx_numserieg, Me.tb_numcodlg, Me.xx_numcodlg, Me.tb_numprogg, Me.tb_note})
    Me.grvDati.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvDati.Enabled = True
    Me.grvDati.GridControl = Me.grDati
    Me.grvDati.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvDati.MinRowHeight = 14
    Me.grvDati.Name = "grvDati"
    Me.grvDati.NTSAllowDelete = True
    Me.grvDati.NTSAllowInsert = True
    Me.grvDati.NTSAllowUpdate = True
    Me.grvDati.NTSMenuContext = Nothing
    Me.grvDati.OptionsCustomization.AllowRowSizing = True
    Me.grvDati.OptionsFilter.AllowFilterEditor = False
    Me.grvDati.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvDati.OptionsNavigation.UseTabKey = False
    Me.grvDati.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvDati.OptionsView.ColumnAutoWidth = False
    Me.grvDati.OptionsView.EnableAppearanceEvenRow = True
    Me.grvDati.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvDati.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvDati.OptionsView.ShowGroupPanel = False
    Me.grvDati.RowHeight = 16
    '
    'tb_numserieg
    '
    Me.tb_numserieg.AppearanceCell.Options.UseBackColor = True
    Me.tb_numserieg.AppearanceCell.Options.UseTextOptions = True
    Me.tb_numserieg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_numserieg.Caption = "Codice Piano dei Conti"
    Me.tb_numserieg.Enabled = True
    Me.tb_numserieg.FieldName = "tb_numserieg"
    Me.tb_numserieg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_numserieg.Name = "tb_numserieg"
    Me.tb_numserieg.NTSRepositoryComboBox = Nothing
    Me.tb_numserieg.NTSRepositoryItemCheck = Nothing
    Me.tb_numserieg.NTSRepositoryItemMemo = Nothing
    Me.tb_numserieg.NTSRepositoryItemText = Nothing
    Me.tb_numserieg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_numserieg.OptionsFilter.AllowFilter = False
    Me.tb_numserieg.Visible = True
    Me.tb_numserieg.VisibleIndex = 0
    Me.tb_numserieg.Width = 61
    '
    'xx_numserieg
    '
    Me.xx_numserieg.AppearanceCell.Options.UseBackColor = True
    Me.xx_numserieg.AppearanceCell.Options.UseTextOptions = True
    Me.xx_numserieg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_numserieg.Caption = "Descrizione Piano dei Conti"
    Me.xx_numserieg.Enabled = True
    Me.xx_numserieg.FieldName = "xx_numserieg"
    Me.xx_numserieg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_numserieg.Name = "xx_numserieg"
    Me.xx_numserieg.NTSRepositoryComboBox = Nothing
    Me.xx_numserieg.NTSRepositoryItemCheck = Nothing
    Me.xx_numserieg.NTSRepositoryItemMemo = Nothing
    Me.xx_numserieg.NTSRepositoryItemText = Nothing
    Me.xx_numserieg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_numserieg.OptionsFilter.AllowFilter = False
    Me.xx_numserieg.Visible = True
    Me.xx_numserieg.VisibleIndex = 1
    Me.xx_numserieg.Width = 180
    '
    'tb_numcodlg
    '
    Me.tb_numcodlg.AppearanceCell.Options.UseBackColor = True
    Me.tb_numcodlg.AppearanceCell.Options.UseTextOptions = True
    Me.tb_numcodlg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_numcodlg.Caption = "Mastro"
    Me.tb_numcodlg.Enabled = True
    Me.tb_numcodlg.FieldName = "tb_numcodlg"
    Me.tb_numcodlg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_numcodlg.Name = "tb_numcodlg"
    Me.tb_numcodlg.NTSRepositoryComboBox = Nothing
    Me.tb_numcodlg.NTSRepositoryItemCheck = Nothing
    Me.tb_numcodlg.NTSRepositoryItemMemo = Nothing
    Me.tb_numcodlg.NTSRepositoryItemText = Nothing
    Me.tb_numcodlg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_numcodlg.OptionsFilter.AllowFilter = False
    Me.tb_numcodlg.Visible = True
    Me.tb_numcodlg.VisibleIndex = 2
    Me.tb_numcodlg.Width = 52
    '
    'xx_numcodlg
    '
    Me.xx_numcodlg.AppearanceCell.Options.UseBackColor = True
    Me.xx_numcodlg.AppearanceCell.Options.UseTextOptions = True
    Me.xx_numcodlg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_numcodlg.Caption = "Descrizione Mastro"
    Me.xx_numcodlg.Enabled = True
    Me.xx_numcodlg.FieldName = "xx_numcodlg"
    Me.xx_numcodlg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_numcodlg.Name = "xx_numcodlg"
    Me.xx_numcodlg.NTSRepositoryComboBox = Nothing
    Me.xx_numcodlg.NTSRepositoryItemCheck = Nothing
    Me.xx_numcodlg.NTSRepositoryItemMemo = Nothing
    Me.xx_numcodlg.NTSRepositoryItemText = Nothing
    Me.xx_numcodlg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_numcodlg.OptionsFilter.AllowFilter = False
    Me.xx_numcodlg.Visible = True
    Me.xx_numcodlg.VisibleIndex = 3
    Me.xx_numcodlg.Width = 151
    '
    'tb_numprogg
    '
    Me.tb_numprogg.AppearanceCell.Options.UseBackColor = True
    Me.tb_numprogg.AppearanceCell.Options.UseTextOptions = True
    Me.tb_numprogg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_numprogg.Caption = "N° Progressivo"
    Me.tb_numprogg.Enabled = True
    Me.tb_numprogg.FieldName = "tb_numprogg"
    Me.tb_numprogg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_numprogg.Name = "tb_numprogg"
    Me.tb_numprogg.NTSRepositoryComboBox = Nothing
    Me.tb_numprogg.NTSRepositoryItemCheck = Nothing
    Me.tb_numprogg.NTSRepositoryItemMemo = Nothing
    Me.tb_numprogg.NTSRepositoryItemText = Nothing
    Me.tb_numprogg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_numprogg.OptionsFilter.AllowFilter = False
    Me.tb_numprogg.Visible = True
    Me.tb_numprogg.VisibleIndex = 4
    Me.tb_numprogg.Width = 82
    '
    'tb_note
    '
    Me.tb_note.AppearanceCell.Options.UseBackColor = True
    Me.tb_note.AppearanceCell.Options.UseTextOptions = True
    Me.tb_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_note.Caption = "Note"
    Me.tb_note.Enabled = True
    Me.tb_note.FieldName = "tb_note"
    Me.tb_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_note.Name = "tb_note"
    Me.tb_note.NTSRepositoryComboBox = Nothing
    Me.tb_note.NTSRepositoryItemCheck = Nothing
    Me.tb_note.NTSRepositoryItemMemo = Nothing
    Me.tb_note.NTSRepositoryItemText = Nothing
    Me.tb_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_note.OptionsFilter.AllowFilter = False
    '
    'liTipo
    '
    Me.liTipo.Cursor = System.Windows.Forms.Cursors.Default
    Me.liTipo.Dock = System.Windows.Forms.DockStyle.Fill
    Me.liTipo.ItemHeight = 14
    Me.liTipo.Location = New System.Drawing.Point(2, 20)
    Me.liTipo.Name = "liTipo"
    Me.liTipo.NTSDbField = ""
    Me.liTipo.Size = New System.Drawing.Size(163, 318)
    Me.liTipo.TabIndex = 6
    '
    'fmTipo
    '
    Me.fmTipo.AllowDrop = True
    Me.fmTipo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmTipo.Appearance.Options.UseBackColor = True
    Me.fmTipo.Controls.Add(Me.liTipo)
    Me.fmTipo.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmTipo.Dock = System.Windows.Forms.DockStyle.Left
    Me.fmTipo.Location = New System.Drawing.Point(0, 26)
    Me.fmTipo.Name = "fmTipo"
    Me.fmTipo.Size = New System.Drawing.Size(167, 340)
    Me.fmTipo.TabIndex = 6
    Me.fmTipo.Text = "Tipo numerazione"
    '
    'fmNumerazioni
    '
    Me.fmNumerazioni.AllowDrop = True
    Me.fmNumerazioni.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmNumerazioni.Appearance.Options.UseBackColor = True
    Me.fmNumerazioni.Controls.Add(Me.grDati)
    Me.fmNumerazioni.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmNumerazioni.Dock = System.Windows.Forms.DockStyle.Fill
    Me.fmNumerazioni.Location = New System.Drawing.Point(167, 26)
    Me.fmNumerazioni.Name = "fmNumerazioni"
    Me.fmNumerazioni.Size = New System.Drawing.Size(500, 340)
    Me.fmNumerazioni.TabIndex = 7
    Me.fmNumerazioni.Text = "Numeratori"
    '
    'FRM__NUMG
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(667, 366)
    Me.Controls.Add(Me.fmNumerazioni)
    Me.Controls.Add(Me.fmTipo)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.HelpButton = True
    Me.Name = "FRM__NUMG"
    Me.NTSLastControlFocussed = Me.grDati
    Me.Text = "NUMERAZIONI COMUNI GLOBALI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grDati, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvDati, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.liTipo, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmTipo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmTipo.ResumeLayout(False)
    CType(Me.fmNumerazioni, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmNumerazioni.ResumeLayout(False)
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__NUMG", "BE__NUMG", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128299096216093750, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleNumg = CType(oTmp, CLE__NUMG)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__NUMG", strRemoteServer, strRemotePort)
    AddHandler oCleNumg.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleNumg.Init(oApp, oScript, oMenu.oCleComm, "TABNUMG", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvDati.NTSSetParam(oMenu, oApp.Tr(Me, 128299096469375000, "Griglia Numerazioni Sottoconti"))
      tb_numserieg.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128299096546250000, "Codice Piano dei Conti"), CLN__STD.tabpcon, False)
      xx_numserieg.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128299096597031250, "Descrizione Piano dei Conti"), 30, True)
      tb_numcodlg.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128299096627968750, "Mastro"), CLN__STD.tabmast, False)
      xx_numcodlg.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128299096650781250, "Descrizione Mastro"), 30, True)
      tb_numprogg.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128299096674531250, "N° Progressivo"), "0", 9, 0, 999999999)
      tb_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130511135036040605, "Note"), 255, True)
      fmTipo.Text = oApp.Tr(Me, 128299933851093750, "Tipo numerazione")
      fmNumerazioni.Text = oApp.Tr(Me, 129004313355990621, "Numeratori")
      liTipo.NTSSetParam(oApp.Tr(Me, 128396814064478000, "Lista numerazioni"))

      tb_numserieg.NTSSetRichiesto()
      tb_numcodlg.NTSSetRichiesto()
      xx_numserieg.Enabled = False
      xx_numcodlg.Enabled = False

      Dim ddtTipo As New DataTable()
      ddtTipo.Columns.Add("cod", GetType(String))
      ddtTipo.Columns.Add("val", GetType(String))
      If CLN__STD.FRIENDLY = False Then
        ddtTipo.Rows.Add(New Object() {"AG", oApp.Tr(Me, 128299934478437500, "Anagrafiche Generali")})
      End If
      If CLN__STD.FRIENDLY = False Then
        ddtTipo.Rows.Add(New Object() {"SX", oApp.Tr(Me, 129818028005191945, "Sottoconti CA DC")})
      End If
      ddtTipo.AcceptChanges()
      liTipo.DisplayMember = "val"
      liTipo.ValueMember = "cod"
      liTipo.DataSource = ddtTipo
      liTipo.SelectedIndex = -1
      ' abilito solo ora il gestore eventi
      AddHandler liTipo.SelectedIndexChanged, AddressOf Me.liTipo_SelectedIndexChanged

      ' disabilito il grid
      Me.grvDati.NTSAllowDelete = False
      Me.grvDati.NTSAllowInsert = False
      Me.grvDati.NTSAllowUpdate = False

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


#Region "Eventi Form"
  Public Overridable Sub FRM__NUMG_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      'e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipo documento, ecc ...
      '(Ex: CTRL+ALT+F4)
      'GctlTipoDoc = ""
      GctlSetRoules()

      '--------------------------------------------
      'sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
      If Not oCallParams Is Nothing Then
      End If  'If Not oCallParams Is Nothing Then

      liTipo.SelectedIndex = 0

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__NUMG_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcNumg.Dispose()
      dsNumg.Dispose()
    Catch
    End Try
  End Sub

  Public Overridable Sub FRM__NUMG_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region


#Region "Eventi toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grDati.Focus()
      grvDati.NTSNuovo()
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
    Try
      If (liTipo.SelectedValue.ToString() = "SS" OrElse liTipo.SelectedValue.ToString() = "SX") Then
        If Not grvDati.NTSDeleteRigaCorrente(dcNumg, True) Then Return
        oCleNumg.Salva(True)
      Else 'If (liTipo.SelectedValue.ToString() = "AG") Then
        If Not NTSCInt(dsNumg.Tables("tabnumg").Rows(0).Item("tb_numprogg")) = 0 Then
          If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128302636615000000, "Conferma cancellazione del record?")) = vbNo Then Return
          oCleNumg.CancellaAG()
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvDati.NTSRipristinaRigaCorrenteBefore(dcNumg, True) Then Return
      oCleNumg.Ripristina(dcNumg.Position, dcNumg.Filter)
      grvDati.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim oParam As New CLE__PATB
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing

    Try
      If grDati.ContainsFocus Then
        If grvDati.FocusedColumn.Name.ToLower = "tb_numcodlg" Then
          Select Case NTSCStr(liTipo.SelectedValue)
            Case "SS"
              '----------------------------------------------
              'zoom specifico per mastri di contabilità generale
              oParam.strCodPdc = grvDati.GetFocusedRowCellValue(tb_numserieg).ToString     'passo il piano dei conti
              SetFastZoom(NTSCStr(grvDati.EditingValue), oParam)
              NTSZOOM.strIn = NTSCStr(grvDati.EditingValue)
              NTSZOOM.ZoomStrIn("ZOOMTABMAST", DittaCorrente, oParam)
              If NTSZOOM.strIn <> NTSCStr(grvDati.EditingValue) Then grvDati.SetFocusedValue(NTSZOOM.strIn)

            Case "SX"
              '----------------------------------------------
              'zoom specifico per mastri di contabilità analitica DC
              oParam.strCodPdc = grvDati.GetFocusedRowCellValue(tb_numserieg).ToString     'passo il piano dei conti
              SetFastZoom(NTSCStr(grvDati.EditingValue), oParam)
              NTSZOOM.strIn = NTSCStr(grvDati.EditingValue)
              NTSZOOM.ZoomStrIn("ZOOMTABMAC2", DittaCorrente, oParam)
              If NTSZOOM.strIn <> NTSCStr(grvDati.EditingValue) Then grvDati.SetFocusedValue(NTSZOOM.strIn)
          End Select
        ElseIf grvDati.FocusedColumn.Name.ToLower = "tb_numserieg" Then
          Select Case NTSCStr(liTipo.SelectedValue)
            Case "SS"
              SetFastZoom(NTSCStr(grvDati.EditingValue), oParam)
              NTSZOOM.strIn = NTSCStr(grvDati.EditingValue)
              NTSZOOM.ZoomStrIn("ZOOMTABPCON", DittaCorrente, oParam)
              If NTSZOOM.strIn <> NTSCStr(grvDati.EditingValue) Then grvDati.SetFocusedValue(NTSZOOM.strIn)

            Case "SX"
              SetFastZoom(NTSCStr(grvDati.EditingValue), oParam)
              NTSZOOM.strIn = NTSCStr(grvDati.EditingValue)
              NTSZOOM.ZoomStrIn("ZOOMTABPCCA", DittaCorrente, oParam)
              If NTSZOOM.strIn <> NTSCStr(grvDati.EditingValue) Then grvDati.SetFocusedValue(NTSZOOM.strIn)

          End Select
        Else
          NTSCallStandardZoom()
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      strErr = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Try
      If Not Salva() Then Return
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region


#Region "Eventi griglia"
  Public Overridable Sub grvDati_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvDati.NTSBeforeRowUpdate
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

  Public Overridable Sub grvDati_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvDati.NTSFocusedRowChanged
    Try

      If strTipoSelected = "SS" OrElse strTipoSelected = "SX" Then
        '-------------------------------------------------
        'se il pdc è diverso da "" blocco la colonna, diversamente la rendo editabile
        If grvDati.GetFocusedRowCellValue(tb_numserieg).ToString.Trim = "" Then
          GctlSetVisEnab(tb_numserieg, False)
          GctlSetVisEnab(tb_numcodlg, False)
        Else
          tb_numserieg.Enabled = False
          tb_numcodlg.Enabled = False
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region


#Region "Eventi lista"
  Public Overridable Sub liTipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles liTipo.SelectedIndexChanged
    Try

      ' mancando l'evento changing devo fare questi controlli
      If (strTipoSelected = liTipo.SelectedValue.ToString()) Then Return
      ' verifica se deve salvare
      If strTipoSelected <> "" Then
        If Not Salva() Then
          liTipo.SelectedValue = strTipoSelected
          grDati.Focus()
          Return
        End If
      End If

      ' salvo il tipo corrente
      strTipoSelected = liTipo.SelectedValue.ToString()
      ' imposta il titolo
      fmNumerazioni.Text = oApp.Tr(Me, 129004313397710439, "Numeratori") + " " + CType(liTipo.SelectedItem, DataRowView).Item("val").ToString()
      ' carica i nuovi dati
      If Not oCleNumg.Apri(DittaCorrente, liTipo.SelectedValue.ToString(), dsNumg) Then Me.Close()
      ' metto a nothing il dc prima di riassegnare il datasource perché
      ' altrimenti mantiene riferimenti al vecchio datasource e può generare errori
      grDati.DataSource = Nothing
      dcNumg.DataSource = Nothing
      dcNumg.DataSource = dsNumg.Tables("TABNUMG")
      dsNumg.AcceptChanges()
      grDati.DataSource = dcNumg

      ' abilito il grid
      If (liTipo.SelectedValue.ToString() = "SS" OrElse liTipo.SelectedValue.ToString() = "SX") Then
        grvDati.NTSAllowDelete = True
        grvDati.NTSAllowInsert = True
        grvDati.NTSAllowUpdate = True
        tlbNuovo.Enabled = True
        tb_numserieg.Visible = True
        xx_numserieg.Visible = True
        tb_numcodlg.Visible = True
        xx_numcodlg.Visible = True
      Else 'If (liTipo.SelectedValue.ToString() = "AG") Then
        grvDati.NTSAllowDelete = False
        grvDati.NTSAllowInsert = False
        grvDati.NTSAllowUpdate = False
        tlbNuovo.Enabled = False
        xx_numcodlg.Visible = False
        tb_numcodlg.Visible = False
        xx_numserieg.Visible = False
        tb_numserieg.Visible = False
        tb_numserieg.Enabled = False
        tb_numcodlg.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try

  End Sub
#End Region


  Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      If (strTipoSelected = "SS" OrElse strTipoSelected = "SX") Then
        Dim dRes As DialogResult
        dRes = grvDati.NTSSalvaRigaCorrente(dcNumg, oCleNumg.RecordIsChanged, False)
        Select Case dRes
          Case System.Windows.Forms.DialogResult.Yes
            'salvo
            '-------------------------------------------------
            'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
            If GctlControllaOutNotEqual() = False Then Return False

            If Not oCleNumg.Salva(False) Then
              Return False
            End If
          Case System.Windows.Forms.DialogResult.No
            'ripristino
            oCleNumg.Ripristina(dcNumg.Position, dcNumg.Filter)
          Case System.Windows.Forms.DialogResult.Cancel
            'non posso fare nulla
            Return False
          Case System.Windows.Forms.DialogResult.Abort
            'la riga non ha subito modifiche
        End Select
      Else 'If (strTipoSelected = "AG") Then
        If (oCleNumg.RecordIsChanged) Then
          If Not oCleNumg.Salva(False) Then
            Return False
          End If
        End If
      End If
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

End Class
