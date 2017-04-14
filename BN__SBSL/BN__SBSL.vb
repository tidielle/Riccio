Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__SBSL
  Private Moduli_P As Integer = bsModAll
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

  Public oCleSbsl As CLE__SBSL
  Public oCallParams As CLE__CLDP
  Public dsSbsl As DataSet
  Public dcSbsl As BindingSource = New BindingSource()

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbApri As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents grSbsl As NTSInformatica.NTSGrid
  Public WithEvents grvSbsl As NTSInformatica.NTSGridView
  Public WithEvents sb_device As NTSInformatica.NTSGridColumn
  Public WithEvents sb_devicename As NTSInformatica.NTSGridColumn
  Public WithEvents sb_profilo As NTSInformatica.NTSGridColumn
  Public WithEvents sb_db As NTSInformatica.NTSGridColumn
  Public WithEvents sb_ditta As NTSInformatica.NTSGridColumn
  Public WithEvents sb_user As NTSInformatica.NTSGridColumn
  Public WithEvents xx_evento As NTSInformatica.NTSGridColumn
  Public WithEvents sb_child As NTSInformatica.NTSGridColumn
  Public WithEvents xx_durata As NTSInformatica.NTSGridColumn


  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__SBSL))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbApri = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grSbsl = New NTSInformatica.NTSGrid
    Me.grvSbsl = New NTSInformatica.NTSGridView
    Me.sb_idsession = New NTSInformatica.NTSGridColumn
    Me.sb_device = New NTSInformatica.NTSGridColumn
    Me.sb_devicename = New NTSInformatica.NTSGridColumn
    Me.sb_profilo = New NTSInformatica.NTSGridColumn
    Me.sb_db = New NTSInformatica.NTSGridColumn
    Me.sb_ditta = New NTSInformatica.NTSGridColumn
    Me.sb_user = New NTSInformatica.NTSGridColumn
    Me.xx_data = New NTSInformatica.NTSGridColumn
    Me.xx_evento = New NTSInformatica.NTSGridColumn
    Me.sb_child = New NTSInformatica.NTSGridColumn
    Me.xx_durata = New NTSInformatica.NTSGridColumn
    Me.xx_disconn = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grSbsl, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvSbsl, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbApri, Me.tlbCancella, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 17
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApri), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbApri.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2)
    Me.tlbApri.Name = "tlbApri"
    Me.tlbApri.Visible = True
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
    'grSbsl
    '
    Me.grSbsl.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grSbsl.EmbeddedNavigator.Name = ""
    Me.grSbsl.Location = New System.Drawing.Point(0, 30)
    Me.grSbsl.MainView = Me.grvSbsl
    Me.grSbsl.Name = "grSbsl"
    Me.grSbsl.Size = New System.Drawing.Size(648, 412)
    Me.grSbsl.TabIndex = 5
    Me.grSbsl.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvSbsl})
    '
    'grvSbsl
    '
    Me.grvSbsl.ActiveFilterEnabled = False
    '
    'sb_idsession
    '
    Me.sb_idsession.AppearanceCell.Options.UseBackColor = True
    Me.sb_idsession.AppearanceCell.Options.UseTextOptions = True
    Me.sb_idsession.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sb_idsession.Caption = "Id sessione"
    Me.sb_idsession.Enabled = True
    Me.sb_idsession.FieldName = "sb_idsession"
    Me.sb_idsession.Name = "sb_idsession"
    Me.sb_idsession.NTSRepositoryComboBox = Nothing
    Me.sb_idsession.NTSRepositoryItemCheck = Nothing
    Me.sb_idsession.NTSRepositoryItemMemo = Nothing
    Me.sb_idsession.NTSRepositoryItemText = Nothing
    Me.sb_idsession.Visible = True
    Me.sb_idsession.VisibleIndex = 11
    Me.grvSbsl.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.sb_device, Me.sb_devicename, Me.sb_profilo, Me.sb_db, Me.sb_ditta, Me.sb_user, Me.xx_data, Me.xx_evento, Me.sb_child, Me.xx_durata, Me.xx_disconn, Me.sb_idsession})
    Me.grvSbsl.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvSbsl.Enabled = True
    Me.grvSbsl.GridControl = Me.grSbsl
    Me.grvSbsl.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvSbsl.MinRowHeight = 14
    Me.grvSbsl.Name = "grvSbsl"
    Me.grvSbsl.NTSAllowDelete = True
    Me.grvSbsl.NTSAllowInsert = True
    Me.grvSbsl.NTSAllowUpdate = True
    Me.grvSbsl.NTSMenuContext = Nothing
    Me.grvSbsl.OptionsCustomization.AllowRowSizing = True
    Me.grvSbsl.OptionsFilter.AllowFilterEditor = False
    Me.grvSbsl.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvSbsl.OptionsNavigation.UseTabKey = False
    Me.grvSbsl.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvSbsl.OptionsView.ColumnAutoWidth = False
    Me.grvSbsl.OptionsView.EnableAppearanceEvenRow = True
    Me.grvSbsl.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvSbsl.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvSbsl.OptionsView.ShowGroupPanel = False
    Me.grvSbsl.RowHeight = 16
    '
    'sb_device
    '
    Me.sb_device.AppearanceCell.Options.UseBackColor = True
    Me.sb_device.AppearanceCell.Options.UseTextOptions = True
    Me.sb_device.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sb_device.Caption = "Device"
    Me.sb_device.Enabled = True
    Me.sb_device.FieldName = "sb_device"
    Me.sb_device.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sb_device.Name = "sb_device"
    Me.sb_device.NTSRepositoryComboBox = Nothing
    Me.sb_device.NTSRepositoryItemCheck = Nothing
    Me.sb_device.NTSRepositoryItemMemo = Nothing
    Me.sb_device.NTSRepositoryItemText = Nothing
    Me.sb_device.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sb_device.OptionsFilter.AllowFilter = False
    Me.sb_device.Visible = True
    Me.sb_device.VisibleIndex = 0
    Me.sb_device.Width = 70
    '
    'sb_devicename
    '
    Me.sb_devicename.AppearanceCell.Options.UseBackColor = True
    Me.sb_devicename.AppearanceCell.Options.UseTextOptions = True
    Me.sb_devicename.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sb_devicename.Caption = "Device name"
    Me.sb_devicename.Enabled = True
    Me.sb_devicename.FieldName = "sb_devicename"
    Me.sb_devicename.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sb_devicename.Name = "sb_devicename"
    Me.sb_devicename.NTSRepositoryComboBox = Nothing
    Me.sb_devicename.NTSRepositoryItemCheck = Nothing
    Me.sb_devicename.NTSRepositoryItemMemo = Nothing
    Me.sb_devicename.NTSRepositoryItemText = Nothing
    Me.sb_devicename.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sb_devicename.OptionsFilter.AllowFilter = False
    Me.sb_devicename.Visible = True
    Me.sb_devicename.VisibleIndex = 1
    Me.sb_devicename.Width = 70
    '
    'sb_profilo
    '
    Me.sb_profilo.AppearanceCell.Options.UseBackColor = True
    Me.sb_profilo.AppearanceCell.Options.UseTextOptions = True
    Me.sb_profilo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sb_profilo.Caption = "Profilo"
    Me.sb_profilo.Enabled = True
    Me.sb_profilo.FieldName = "sb_profilo"
    Me.sb_profilo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sb_profilo.Name = "sb_profilo"
    Me.sb_profilo.NTSRepositoryComboBox = Nothing
    Me.sb_profilo.NTSRepositoryItemCheck = Nothing
    Me.sb_profilo.NTSRepositoryItemMemo = Nothing
    Me.sb_profilo.NTSRepositoryItemText = Nothing
    Me.sb_profilo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sb_profilo.OptionsFilter.AllowFilter = False
    Me.sb_profilo.Visible = True
    Me.sb_profilo.VisibleIndex = 2
    Me.sb_profilo.Width = 70
    '
    'sb_db
    '
    Me.sb_db.AppearanceCell.Options.UseBackColor = True
    Me.sb_db.AppearanceCell.Options.UseTextOptions = True
    Me.sb_db.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sb_db.Caption = "Database"
    Me.sb_db.Enabled = True
    Me.sb_db.FieldName = "sb_db"
    Me.sb_db.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sb_db.Name = "sb_db"
    Me.sb_db.NTSRepositoryComboBox = Nothing
    Me.sb_db.NTSRepositoryItemCheck = Nothing
    Me.sb_db.NTSRepositoryItemMemo = Nothing
    Me.sb_db.NTSRepositoryItemText = Nothing
    Me.sb_db.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sb_db.OptionsFilter.AllowFilter = False
    Me.sb_db.Visible = True
    Me.sb_db.VisibleIndex = 3
    Me.sb_db.Width = 70
    '
    'sb_ditta
    '
    Me.sb_ditta.AppearanceCell.Options.UseBackColor = True
    Me.sb_ditta.AppearanceCell.Options.UseTextOptions = True
    Me.sb_ditta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sb_ditta.Caption = "Ditta"
    Me.sb_ditta.Enabled = True
    Me.sb_ditta.FieldName = "sb_ditta"
    Me.sb_ditta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sb_ditta.Name = "sb_ditta"
    Me.sb_ditta.NTSRepositoryComboBox = Nothing
    Me.sb_ditta.NTSRepositoryItemCheck = Nothing
    Me.sb_ditta.NTSRepositoryItemMemo = Nothing
    Me.sb_ditta.NTSRepositoryItemText = Nothing
    Me.sb_ditta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sb_ditta.OptionsFilter.AllowFilter = False
    Me.sb_ditta.Visible = True
    Me.sb_ditta.VisibleIndex = 4
    Me.sb_ditta.Width = 70
    '
    'sb_user
    '
    Me.sb_user.AppearanceCell.Options.UseBackColor = True
    Me.sb_user.AppearanceCell.Options.UseTextOptions = True
    Me.sb_user.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sb_user.Caption = "Utente"
    Me.sb_user.Enabled = True
    Me.sb_user.FieldName = "sb_user"
    Me.sb_user.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sb_user.Name = "sb_user"
    Me.sb_user.NTSRepositoryComboBox = Nothing
    Me.sb_user.NTSRepositoryItemCheck = Nothing
    Me.sb_user.NTSRepositoryItemMemo = Nothing
    Me.sb_user.NTSRepositoryItemText = Nothing
    Me.sb_user.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sb_user.OptionsFilter.AllowFilter = False
    Me.sb_user.Visible = True
    Me.sb_user.VisibleIndex = 5
    Me.sb_user.Width = 70
    '
    'xx_data
    '
    Me.xx_data.AppearanceCell.Options.UseBackColor = True
    Me.xx_data.AppearanceCell.Options.UseTextOptions = True
    Me.xx_data.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_data.Caption = "Data"
    Me.xx_data.Enabled = True
    Me.xx_data.FieldName = "xx_data"
    Me.xx_data.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_data.Name = "xx_data"
    Me.xx_data.NTSRepositoryComboBox = Nothing
    Me.xx_data.NTSRepositoryItemCheck = Nothing
    Me.xx_data.NTSRepositoryItemMemo = Nothing
    Me.xx_data.NTSRepositoryItemText = Nothing
    Me.xx_data.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_data.OptionsFilter.AllowFilter = False
    Me.xx_data.Visible = True
    Me.xx_data.VisibleIndex = 6
    '
    'xx_evento
    '
    Me.xx_evento.AppearanceCell.Options.UseBackColor = True
    Me.xx_evento.AppearanceCell.Options.UseTextOptions = True
    Me.xx_evento.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_evento.Caption = "Evento"
    Me.xx_evento.Enabled = True
    Me.xx_evento.FieldName = "xx_evento"
    Me.xx_evento.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_evento.Name = "xx_evento"
    Me.xx_evento.NTSRepositoryComboBox = Nothing
    Me.xx_evento.NTSRepositoryItemCheck = Nothing
    Me.xx_evento.NTSRepositoryItemMemo = Nothing
    Me.xx_evento.NTSRepositoryItemText = Nothing
    Me.xx_evento.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_evento.OptionsFilter.AllowFilter = False
    Me.xx_evento.Visible = True
    Me.xx_evento.VisibleIndex = 7
    Me.xx_evento.Width = 70
    '
    'sb_child
    '
    Me.sb_child.AppearanceCell.Options.UseBackColor = True
    Me.sb_child.AppearanceCell.Options.UseTextOptions = True
    Me.sb_child.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.sb_child.Caption = "Programma"
    Me.sb_child.Enabled = True
    Me.sb_child.FieldName = "sb_child"
    Me.sb_child.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.sb_child.Name = "sb_child"
    Me.sb_child.NTSRepositoryComboBox = Nothing
    Me.sb_child.NTSRepositoryItemCheck = Nothing
    Me.sb_child.NTSRepositoryItemMemo = Nothing
    Me.sb_child.NTSRepositoryItemText = Nothing
    Me.sb_child.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.sb_child.OptionsFilter.AllowFilter = False
    Me.sb_child.Visible = True
    Me.sb_child.VisibleIndex = 8
    Me.sb_child.Width = 70
    '
    'xx_durata
    '
    Me.xx_durata.AppearanceCell.Options.UseBackColor = True
    Me.xx_durata.AppearanceCell.Options.UseTextOptions = True
    Me.xx_durata.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_durata.Caption = "Tempo"
    Me.xx_durata.Enabled = True
    Me.xx_durata.FieldName = "xx_durata"
    Me.xx_durata.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_durata.Name = "xx_durata"
    Me.xx_durata.NTSRepositoryComboBox = Nothing
    Me.xx_durata.NTSRepositoryItemCheck = Nothing
    Me.xx_durata.NTSRepositoryItemMemo = Nothing
    Me.xx_durata.NTSRepositoryItemText = Nothing
    Me.xx_durata.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_durata.OptionsFilter.AllowFilter = False
    Me.xx_durata.Visible = True
    Me.xx_durata.VisibleIndex = 9
    Me.xx_durata.Width = 70
    '
    'xx_disconn
    '
    Me.xx_disconn.AppearanceCell.Options.UseBackColor = True
    Me.xx_disconn.AppearanceCell.Options.UseTextOptions = True
    Me.xx_disconn.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_disconn.Caption = "Chiuso"
    Me.xx_disconn.Enabled = True
    Me.xx_disconn.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_disconn.Name = "xx_disconn"
    Me.xx_disconn.NTSRepositoryComboBox = Nothing
    Me.xx_disconn.NTSRepositoryItemCheck = Nothing
    Me.xx_disconn.NTSRepositoryItemMemo = Nothing
    Me.xx_disconn.NTSRepositoryItemText = Nothing
    Me.xx_disconn.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_disconn.OptionsFilter.AllowFilter = False
    Me.xx_disconn.Visible = True
    Me.xx_disconn.VisibleIndex = 10
    '
    'FRM__SBSL
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.grSbsl)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRM__SBSL"
    Me.NTSLastControlFocussed = Me.grSbsl
    Me.Text = "SBS LOG"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grSbsl, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvSbsl, System.ComponentModel.ISupportInitialize).EndInit()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__SBSL", "BE__SBSL", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 130449585263648747, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleSbsl = CType(oTmp, CLE__SBSL)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__SBSL", strRemoteServer, strRemotePort)
    AddHandler oCleSbsl.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleSbsl.Init(oApp, oScript, oMenu.oCleComm, "TABMAGA", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function


  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbApri.GlyphPath = (oApp.ChildImageDir & "\open.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvSbsl.NTSSetParam(oMenu, "SBS LOG")
      grvSbsl.NTSAllowInsert = False
      grvSbsl.Enabled = False
      sb_device.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130449621217795279, "Device"), 0, True)
      sb_devicename.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130449621217805050, "Device name"), 0, True)
      sb_profilo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130449621217814822, "Profilo"), 0, True)
      sb_db.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130449621217824598, "Database"), 0, True)
      sb_ditta.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130449621217834378, "Ditta"), 0, True)
      sb_user.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130449621217844154, "Utente"), 0, True)
      xx_data.NTSSetParamDATA(oMenu, oApp.Tr(Me, 130449622928681440, "Data"), False)
      xx_evento.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130449621217863689, "Evento"), 0, True)
      sb_child.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130449621217873452, "Programma"), 0, True)
      xx_durata.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130449585263590103, "Tempo HH,MMSS"), 0, False)
      xx_disconn.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130449620627145859, "Chiuso"), "S", "N")
      sb_idsession.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130449679379807002, "ID di sessione"), "0", 9)

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
  Public Overridable Sub FRM__SBSL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim strDtStart As String = DateTime.Now.ToShortDateString

    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      'per default faccio vedere da inizio mese fino ad oggi di tutte le device
      'se siamo nei primi 5 giorni del mese, dall'inizio del mese precedente
      'If DateTime.Now.Day < 5 Then
      '  strDtStart = NTSCDate(FineMese(DateTime.Now.ToShortDateString)).AddMonths(-2).ToShortDateString
      'Else
      '  strDtStart = NTSCDate(FineMese(DateTime.Now.ToShortDateString)).AddMonths(-1).ToShortDateString
      'End If
      'potrebbe restituire troppi RK e inchioderebbe tutto: prendo su solo i record di oggi
      strDtStart = DateTime.Now.ToShortDateString

      Me.Cursor = Cursors.WaitCursor

      If Not oCleSbsl.Apri(DittaCorrente, strDtStart, DateTime.Now.ToShortDateString, "", "", "T", dsSbsl, True) Then
        Me.Close()
        Return
      End If
      dcSbsl.DataSource = dsSbsl.Tables("SBSLOG")
      dsSbsl.AcceptChanges()

      'vito che ho aggiunt odei record nella 'apri' dell'entity, ora ordino i record in modo  corretto
      dcSbsl.Sort = "sb_device, sb_devicename, sb_profilo, sb_db, sb_ditta, sb_user, sb_ultagg"

      grSbsl.DataSource = dcSbsl

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub FRM__SBSL_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcSbsl.Dispose()
      dsSbsl.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbApri_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApri.ItemClick
    Dim frmSele As FRM__SELE = Nothing
    Try
      frmSele = CType(NTSNewFormModal("FRM__SELE"), FRM__SELE)
      frmSele.Init(oMenu, Nothing, DittaCorrente, Nothing)
      frmSele.ShowDialog()
      If frmSele.bOk = False Then Return

      Me.Cursor = Cursors.WaitCursor

      If Not oCleSbsl.Apri(DittaCorrente, _
                           frmSele.edDatScadDa.Text, _
                           frmSele.edDatScadA.Text, _
                           frmSele.edDeviceName.Text.Trim, _
                           frmSele.edUser.Text, _
                           frmSele.cbTipo.SelectedValue, dsSbsl, False) Then
        Return
      End If
      dcSbsl.DataSource = dsSbsl.Tables("SBSLOG")
      dsSbsl.AcceptChanges()

      'vito che ho aggiunt odei record nella 'apri' dell'entity, ora ordino i record in modo  corretto
      dcSbsl.Sort = "sb_device, sb_devicename, sb_profilo, sb_db, sb_ditta, sb_user, sb_ultagg"

      grSbsl.DataSource = dcSbsl

      If dsSbsl.Tables("SBSLOG").Rows.Count = 0 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130449693779685733, "Non è stato trovato nessun record per i filtri impostati."))
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
      If Not frmSele Is Nothing Then
        frmSele.Dispose()
        frmSele = Nothing
      End If
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Dim frmSele As FRM__SELE = Nothing
    Try
      frmSele = CType(NTSNewFormModal("FRM__SELE"), FRM__SELE)
      frmSele.Init(oMenu, Nothing, DittaCorrente, Nothing)
      frmSele.ShowDialog()
      If frmSele.bOk = False Then Return

      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 130449704075459844, "Confermi la cancellazione?")) = Windows.Forms.DialogResult.No Then Return

      Me.Cursor = Cursors.WaitCursor

      If Not oCleSbsl.Cancella(DittaCorrente, _
                           frmSele.edDatScadDa.Text, _
                           frmSele.edDatScadA.Text, _
                           frmSele.edDeviceName.Text.Trim, _
                           frmSele.edUser.Text, _
                           frmSele.cbTipo.SelectedValue) Then
        Me.Close()
        Return
      Else
        Me.Cursor = Cursors.Default
        oApp.MsgBoxInfo(oApp.Tr(Me, 130449692485463877, "Cancellazione avvenuta correttamente"))
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
      If Not frmSele Is Nothing Then
        frmSele.Dispose()
        frmSele = Nothing
      End If
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub

#End Region


End Class
