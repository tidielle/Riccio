Imports System
Imports System.Data
Imports NTSInformatica
Imports NTSInformatica.CLN__STD

Public Class FRM__GESF
  Inherits NTSInformatica.FRM__CHIL

  Public oCleGesf As CLE__BASN
  Public oCallParams As CLE__CLDP
  Public oCallParamsZoom As CLE__PATB
  Public dcFile As New System.Windows.Forms.BindingSource
  Public dsFile As New DataSet
  Public strCurDir As String = ""
  Public strFilter As String = "*.*"
  Public bOnlyDir As Boolean = False      'FolderBrowseerDialog
  Public bSaveFile As Boolean = False     'SaveFileDialog
  Public bModCRM As Boolean = False

  'per copia/incolla file
  Public strDirCopy As String = ""
  Public strFileCopy As String = ""

  Public WithEvents pnForm As NTSInformatica.NTSPanel
  Public WithEvents cmdEsci As NTSInformatica.NTSButton
  Public WithEvents pnCommand As NTSInformatica.NTSPanel
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents pnGrid As NTSInformatica.NTSPanel
  Private Shadows components As System.ComponentModel.IContainer
  Public WithEvents lbCurrentDir As NTSInformatica.NTSLabel
  Public WithEvents cmdCreaDir As NTSInformatica.NTSButton
  Public WithEvents cmdCancella As NTSInformatica.NTSButton
  Public WithEvents cmdFiltra As NTSInformatica.NTSButton
  Public WithEvents lbFiltro As NTSInformatica.NTSLabel
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents cmdGoSubdir As NTSInformatica.NTSButton
  Public WithEvents grFile As NTSInformatica.NTSGrid
  Public WithEvents grvFile As NTSInformatica.NTSGridView
  Public WithEvents xx_dir As NTSInformatica.NTSGridColumn
  Public WithEvents xx_name As NTSInformatica.NTSGridColumn
  Public WithEvents xx_size As NTSInformatica.NTSGridColumn
  Public WithEvents xx_datemod As NTSInformatica.NTSGridColumn
  Public WithEvents xx_readonly As NTSInformatica.NTSGridColumn
  Public WithEvents lbNomeFile As NTSInformatica.NTSLabel
  Public WithEvents edNomeFile As NTSInformatica.NTSTextBoxStr
  Public WithEvents NtsLabel1 As NTSInformatica.NTSLabel
  Public WithEvents cmdDownload As NTSInformatica.NTSButton
  Public WithEvents cmdPassaa As NTSInformatica.NTSButton


  Private Sub InitializeComponent()
    Me.pnForm = New NTSInformatica.NTSPanel
    Me.pnGrid = New NTSInformatica.NTSPanel
    Me.grFile = New NTSInformatica.NTSGrid
    Me.grvFile = New NTSInformatica.NTSGridView
    Me.xx_dir = New NTSInformatica.NTSGridColumn
    Me.xx_name = New NTSInformatica.NTSGridColumn
    Me.xx_size = New NTSInformatica.NTSGridColumn
    Me.xx_datemod = New NTSInformatica.NTSGridColumn
    Me.xx_readonly = New NTSInformatica.NTSGridColumn
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.lbTitolo = New NTSInformatica.NTSLabel
    Me.edNomeFile = New NTSInformatica.NTSTextBoxStr
    Me.lbNomeFile = New NTSInformatica.NTSLabel
    Me.lbFiltro = New NTSInformatica.NTSLabel
    Me.lbCurrentDir = New NTSInformatica.NTSLabel
    Me.pnCommand = New NTSInformatica.NTSPanel
    Me.ckAccessoCompleto = New NTSInformatica.NTSCheckBox
    Me.cmdUpload = New NTSInformatica.NTSButton
    Me.cmdDownload = New NTSInformatica.NTSButton
    Me.NtsLabel1 = New NTSInformatica.NTSLabel
    Me.cmdPassaa = New NTSInformatica.NTSButton
    Me.cmdGoSubdir = New NTSInformatica.NTSButton
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.cmdFiltra = New NTSInformatica.NTSButton
    Me.cmdCreaDir = New NTSInformatica.NTSButton
    Me.cmdCancella = New NTSInformatica.NTSButton
    Me.cmdEsci = New NTSInformatica.NTSButton
    Me.cmdCopia = New NTSInformatica.NTSButton
    Me.cmdIncolla = New NTSInformatica.NTSButton
    Me.cmdRinomina = New NTSInformatica.NTSButton
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnForm, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnForm.SuspendLayout()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
    CType(Me.grFile, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvFile, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.edNomeFile.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnCommand, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCommand.SuspendLayout()
    CType(Me.ckAccessoCompleto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'pnForm
    '
    Me.pnForm.AllowDrop = True
    Me.pnForm.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnForm.Appearance.Options.UseBackColor = True
    Me.pnForm.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnForm.Controls.Add(Me.pnGrid)
    Me.pnForm.Controls.Add(Me.pnTop)
    Me.pnForm.Controls.Add(Me.pnCommand)
    Me.pnForm.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnForm.Location = New System.Drawing.Point(0, 0)
    Me.pnForm.Name = "pnForm"
    Me.pnForm.NTSActiveTrasparency = True
    Me.pnForm.Size = New System.Drawing.Size(674, 511)
    Me.pnForm.TabIndex = 7
    '
    'pnGrid
    '
    Me.pnGrid.AllowDrop = True
    Me.pnGrid.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrid.Appearance.Options.UseBackColor = True
    Me.pnGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat
    Me.pnGrid.Controls.Add(Me.grFile)
    Me.pnGrid.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(0, 95)
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.NTSActiveTrasparency = True
    Me.pnGrid.Size = New System.Drawing.Size(568, 416)
    Me.pnGrid.TabIndex = 10
    Me.pnGrid.Text = "NtsPanel1"
    '
    'grFile
    '
    Me.grFile.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grFile.EmbeddedNavigator.Name = ""
    Me.grFile.Location = New System.Drawing.Point(2, 2)
    Me.grFile.MainView = Me.grvFile
    Me.grFile.Name = "grFile"
    Me.grFile.Size = New System.Drawing.Size(564, 412)
    Me.grFile.TabIndex = 6
    Me.grFile.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvFile})
    '
    'grvFile
    '
    Me.grvFile.ActiveFilterEnabled = False
    Me.grvFile.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_dir, Me.xx_name, Me.xx_size, Me.xx_datemod, Me.xx_readonly})
    Me.grvFile.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvFile.Enabled = True
    Me.grvFile.GridControl = Me.grFile
    Me.grvFile.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvFile.MinRowHeight = 14
    Me.grvFile.Name = "grvFile"
    Me.grvFile.NTSAllowDelete = True
    Me.grvFile.NTSAllowInsert = True
    Me.grvFile.NTSAllowUpdate = True
    Me.grvFile.NTSMenuContext = Nothing
    Me.grvFile.OptionsCustomization.AllowRowSizing = True
    Me.grvFile.OptionsFilter.AllowFilterEditor = False
    Me.grvFile.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvFile.OptionsNavigation.UseTabKey = False
    Me.grvFile.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvFile.OptionsView.ColumnAutoWidth = False
    Me.grvFile.OptionsView.EnableAppearanceEvenRow = True
    Me.grvFile.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvFile.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvFile.OptionsView.ShowGroupPanel = False
    Me.grvFile.RowHeight = 16
    '
    'xx_dir
    '
    Me.xx_dir.AppearanceCell.Options.UseBackColor = True
    Me.xx_dir.AppearanceCell.Options.UseTextOptions = True
    Me.xx_dir.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_dir.Caption = "Dir"
    Me.xx_dir.Enabled = True
    Me.xx_dir.FieldName = "xx_dir"
    Me.xx_dir.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_dir.Name = "xx_dir"
    Me.xx_dir.NTSRepositoryComboBox = Nothing
    Me.xx_dir.NTSRepositoryItemCheck = Nothing
    Me.xx_dir.NTSRepositoryItemMemo = Nothing
    Me.xx_dir.NTSRepositoryItemText = Nothing
    Me.xx_dir.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_dir.OptionsFilter.AllowFilter = False
    Me.xx_dir.Visible = True
    Me.xx_dir.VisibleIndex = 0
    Me.xx_dir.Width = 70
    '
    'xx_name
    '
    Me.xx_name.AppearanceCell.Options.UseBackColor = True
    Me.xx_name.AppearanceCell.Options.UseTextOptions = True
    Me.xx_name.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_name.Caption = "Nome"
    Me.xx_name.Enabled = True
    Me.xx_name.FieldName = "xx_name"
    Me.xx_name.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_name.Name = "xx_name"
    Me.xx_name.NTSRepositoryComboBox = Nothing
    Me.xx_name.NTSRepositoryItemCheck = Nothing
    Me.xx_name.NTSRepositoryItemMemo = Nothing
    Me.xx_name.NTSRepositoryItemText = Nothing
    Me.xx_name.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_name.OptionsFilter.AllowFilter = False
    Me.xx_name.Visible = True
    Me.xx_name.VisibleIndex = 1
    Me.xx_name.Width = 70
    '
    'xx_size
    '
    Me.xx_size.AppearanceCell.Options.UseBackColor = True
    Me.xx_size.AppearanceCell.Options.UseTextOptions = True
    Me.xx_size.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_size.Caption = "Dimensione"
    Me.xx_size.Enabled = True
    Me.xx_size.FieldName = "xx_size"
    Me.xx_size.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_size.Name = "xx_size"
    Me.xx_size.NTSRepositoryComboBox = Nothing
    Me.xx_size.NTSRepositoryItemCheck = Nothing
    Me.xx_size.NTSRepositoryItemMemo = Nothing
    Me.xx_size.NTSRepositoryItemText = Nothing
    Me.xx_size.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_size.OptionsFilter.AllowFilter = False
    Me.xx_size.Visible = True
    Me.xx_size.VisibleIndex = 2
    Me.xx_size.Width = 70
    '
    'xx_datemod
    '
    Me.xx_datemod.AppearanceCell.Options.UseBackColor = True
    Me.xx_datemod.AppearanceCell.Options.UseTextOptions = True
    Me.xx_datemod.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_datemod.Caption = "Data ultima modifica"
    Me.xx_datemod.Enabled = True
    Me.xx_datemod.FieldName = "xx_datemod"
    Me.xx_datemod.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_datemod.Name = "xx_datemod"
    Me.xx_datemod.NTSRepositoryComboBox = Nothing
    Me.xx_datemod.NTSRepositoryItemCheck = Nothing
    Me.xx_datemod.NTSRepositoryItemMemo = Nothing
    Me.xx_datemod.NTSRepositoryItemText = Nothing
    Me.xx_datemod.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_datemod.OptionsFilter.AllowFilter = False
    Me.xx_datemod.Visible = True
    Me.xx_datemod.VisibleIndex = 3
    '
    'xx_readonly
    '
    Me.xx_readonly.AppearanceCell.Options.UseBackColor = True
    Me.xx_readonly.AppearanceCell.Options.UseTextOptions = True
    Me.xx_readonly.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_readonly.Caption = "Sola lettura"
    Me.xx_readonly.Enabled = True
    Me.xx_readonly.FieldName = "xx_readonly"
    Me.xx_readonly.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_readonly.Name = "xx_readonly"
    Me.xx_readonly.NTSRepositoryComboBox = Nothing
    Me.xx_readonly.NTSRepositoryItemCheck = Nothing
    Me.xx_readonly.NTSRepositoryItemMemo = Nothing
    Me.xx_readonly.NTSRepositoryItemText = Nothing
    Me.xx_readonly.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_readonly.OptionsFilter.AllowFilter = False
    Me.xx_readonly.Visible = True
    Me.xx_readonly.VisibleIndex = 4
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.lbTitolo)
    Me.pnTop.Controls.Add(Me.edNomeFile)
    Me.pnTop.Controls.Add(Me.lbNomeFile)
    Me.pnTop.Controls.Add(Me.lbFiltro)
    Me.pnTop.Controls.Add(Me.lbCurrentDir)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(568, 95)
    Me.pnTop.TabIndex = 9
    Me.pnTop.Text = "NtsPanel2"
    '
    'lbTitolo
    '
    Me.lbTitolo.BackColor = System.Drawing.Color.Transparent
    Me.lbTitolo.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbTitolo.Location = New System.Drawing.Point(16, 7)
    Me.lbTitolo.Name = "lbTitolo"
    Me.lbTitolo.NTSDbField = ""
    Me.lbTitolo.Size = New System.Drawing.Size(160, 24)
    Me.lbTitolo.TabIndex = 7
    Me.lbTitolo.Text = "Gestione file"
    Me.lbTitolo.Tooltip = ""
    Me.lbTitolo.UseMnemonic = False
    '
    'edNomeFile
    '
    Me.edNomeFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edNomeFile.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNomeFile.EditValue = ""
    Me.edNomeFile.Location = New System.Drawing.Point(182, 59)
    Me.edNomeFile.Name = "edNomeFile"
    Me.edNomeFile.NTSDbField = ""
    Me.edNomeFile.NTSForzaVisZoom = False
    Me.edNomeFile.NTSOldValue = ""
    Me.edNomeFile.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNomeFile.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNomeFile.Properties.AutoHeight = False
    Me.edNomeFile.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNomeFile.Properties.MaxLength = 65536
    Me.edNomeFile.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNomeFile.Size = New System.Drawing.Size(380, 20)
    Me.edNomeFile.TabIndex = 6
    '
    'lbNomeFile
    '
    Me.lbNomeFile.BackColor = System.Drawing.Color.Transparent
    Me.lbNomeFile.Location = New System.Drawing.Point(16, 62)
    Me.lbNomeFile.Name = "lbNomeFile"
    Me.lbNomeFile.NTSDbField = ""
    Me.lbNomeFile.Size = New System.Drawing.Size(89, 26)
    Me.lbNomeFile.TabIndex = 5
    Me.lbNomeFile.Text = "Nome del file"
    Me.lbNomeFile.Tooltip = ""
    Me.lbNomeFile.UseMnemonic = False
    '
    'lbFiltro
    '
    Me.lbFiltro.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbFiltro.BackColor = System.Drawing.Color.Transparent
    Me.lbFiltro.Location = New System.Drawing.Point(182, 35)
    Me.lbFiltro.Name = "lbFiltro"
    Me.lbFiltro.NTSDbField = ""
    Me.lbFiltro.Size = New System.Drawing.Size(380, 29)
    Me.lbFiltro.TabIndex = 4
    Me.lbFiltro.Text = "Filtro"
    Me.lbFiltro.Tooltip = ""
    Me.lbFiltro.UseMnemonic = False
    '
    'lbCurrentDir
    '
    Me.lbCurrentDir.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbCurrentDir.BackColor = System.Drawing.Color.Transparent
    Me.lbCurrentDir.Location = New System.Drawing.Point(182, 7)
    Me.lbCurrentDir.Name = "lbCurrentDir"
    Me.lbCurrentDir.NTSDbField = ""
    Me.lbCurrentDir.Size = New System.Drawing.Size(380, 29)
    Me.lbCurrentDir.TabIndex = 4
    Me.lbCurrentDir.Text = "Cartella corrente"
    Me.lbCurrentDir.Tooltip = ""
    Me.lbCurrentDir.UseMnemonic = False
    '
    'pnCommand
    '
    Me.pnCommand.AllowDrop = True
    Me.pnCommand.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCommand.Appearance.Options.UseBackColor = True
    Me.pnCommand.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCommand.Controls.Add(Me.cmdRinomina)
    Me.pnCommand.Controls.Add(Me.cmdIncolla)
    Me.pnCommand.Controls.Add(Me.cmdCopia)
    Me.pnCommand.Controls.Add(Me.ckAccessoCompleto)
    Me.pnCommand.Controls.Add(Me.cmdUpload)
    Me.pnCommand.Controls.Add(Me.cmdDownload)
    Me.pnCommand.Controls.Add(Me.NtsLabel1)
    Me.pnCommand.Controls.Add(Me.cmdPassaa)
    Me.pnCommand.Controls.Add(Me.cmdGoSubdir)
    Me.pnCommand.Controls.Add(Me.cmdSeleziona)
    Me.pnCommand.Controls.Add(Me.cmdFiltra)
    Me.pnCommand.Controls.Add(Me.cmdCreaDir)
    Me.pnCommand.Controls.Add(Me.cmdCancella)
    Me.pnCommand.Controls.Add(Me.cmdEsci)
    Me.pnCommand.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCommand.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnCommand.Location = New System.Drawing.Point(568, 0)
    Me.pnCommand.Name = "pnCommand"
    Me.pnCommand.NTSActiveTrasparency = True
    Me.pnCommand.Size = New System.Drawing.Size(106, 511)
    Me.pnCommand.TabIndex = 8
    Me.pnCommand.Text = "NtsPanel2"
    '
    'ckAccessoCompleto
    '
    Me.ckAccessoCompleto.Location = New System.Drawing.Point(7, 485)
    Me.ckAccessoCompleto.Name = "ckAccessoCompleto"
    Me.ckAccessoCompleto.NTSCheckValue = "S"
    Me.ckAccessoCompleto.NTSUnCheckValue = "N"
    Me.ckAccessoCompleto.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAccessoCompleto.Properties.Appearance.Options.UseBackColor = True
    Me.ckAccessoCompleto.Properties.AutoHeight = False
    Me.ckAccessoCompleto.Properties.Caption = "Disk full access"
    Me.ckAccessoCompleto.Size = New System.Drawing.Size(96, 14)
    Me.ckAccessoCompleto.TabIndex = 9
    '
    'cmdUpload
    '
    Me.cmdUpload.ImagePath = ""
    Me.cmdUpload.ImageText = ""
    Me.cmdUpload.Location = New System.Drawing.Point(6, 352)
    Me.cmdUpload.Name = "cmdUpload"
    Me.cmdUpload.NTSContextMenu = Nothing
    Me.cmdUpload.Size = New System.Drawing.Size(89, 26)
    Me.cmdUpload.TabIndex = 8
    Me.cmdUpload.Text = "Upload"
    '
    'cmdDownload
    '
    Me.cmdDownload.ImagePath = ""
    Me.cmdDownload.ImageText = ""
    Me.cmdDownload.Location = New System.Drawing.Point(6, 378)
    Me.cmdDownload.Name = "cmdDownload"
    Me.cmdDownload.NTSContextMenu = Nothing
    Me.cmdDownload.Size = New System.Drawing.Size(89, 26)
    Me.cmdDownload.TabIndex = 7
    Me.cmdDownload.Text = "Download"
    '
    'NtsLabel1
    '
    Me.NtsLabel1.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel1.Location = New System.Drawing.Point(6, 407)
    Me.NtsLabel1.Name = "NtsLabel1"
    Me.NtsLabel1.NTSDbField = ""
    Me.NtsLabel1.Size = New System.Drawing.Size(89, 75)
    Me.NtsLabel1.TabIndex = 6
    Me.NtsLabel1.Text = "Per trasferire file" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "dal/al server" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "utilizzare app" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "come dropbox," & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "google drive, " & _
        "ecc"
    Me.NtsLabel1.Tooltip = ""
    Me.NtsLabel1.UseMnemonic = False
    '
    'cmdPassaa
    '
    Me.cmdPassaa.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPassaa.ImagePath = ""
    Me.cmdPassaa.ImageText = ""
    Me.cmdPassaa.Location = New System.Drawing.Point(6, 91)
    Me.cmdPassaa.Name = "cmdPassaa"
    Me.cmdPassaa.NTSContextMenu = Nothing
    Me.cmdPassaa.Size = New System.Drawing.Size(89, 48)
    Me.cmdPassaa.TabIndex = 4
    Me.cmdPassaa.Text = "Passa a cartella"
    '
    'cmdGoSubdir
    '
    Me.cmdGoSubdir.ImagePath = ""
    Me.cmdGoSubdir.ImageText = ""
    Me.cmdGoSubdir.Location = New System.Drawing.Point(6, 145)
    Me.cmdGoSubdir.Name = "cmdGoSubdir"
    Me.cmdGoSubdir.NTSContextMenu = Nothing
    Me.cmdGoSubdir.Size = New System.Drawing.Size(89, 26)
    Me.cmdGoSubdir.TabIndex = 3
    Me.cmdGoSubdir.Text = "Apri cartella"
    Me.cmdGoSubdir.Visible = False
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.ImagePath = ""
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(6, 42)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.NTSContextMenu = Nothing
    Me.cmdSeleziona.Size = New System.Drawing.Size(89, 26)
    Me.cmdSeleziona.TabIndex = 3
    Me.cmdSeleziona.Text = "Seleziona"
    Me.cmdSeleziona.Visible = False
    '
    'cmdFiltra
    '
    Me.cmdFiltra.ImagePath = ""
    Me.cmdFiltra.ImageText = ""
    Me.cmdFiltra.Location = New System.Drawing.Point(6, 176)
    Me.cmdFiltra.Name = "cmdFiltra"
    Me.cmdFiltra.NTSContextMenu = Nothing
    Me.cmdFiltra.Size = New System.Drawing.Size(89, 26)
    Me.cmdFiltra.TabIndex = 3
    Me.cmdFiltra.Text = "Filtra"
    '
    'cmdCreaDir
    '
    Me.cmdCreaDir.ImagePath = ""
    Me.cmdCreaDir.ImageText = ""
    Me.cmdCreaDir.Location = New System.Drawing.Point(6, 207)
    Me.cmdCreaDir.Name = "cmdCreaDir"
    Me.cmdCreaDir.NTSContextMenu = Nothing
    Me.cmdCreaDir.Size = New System.Drawing.Size(89, 26)
    Me.cmdCreaDir.TabIndex = 3
    Me.cmdCreaDir.Text = "Crea cartella"
    '
    'cmdCancella
    '
    Me.cmdCancella.ImagePath = ""
    Me.cmdCancella.ImageText = ""
    Me.cmdCancella.Location = New System.Drawing.Point(6, 234)
    Me.cmdCancella.Name = "cmdCancella"
    Me.cmdCancella.NTSContextMenu = Nothing
    Me.cmdCancella.Size = New System.Drawing.Size(89, 26)
    Me.cmdCancella.TabIndex = 3
    Me.cmdCancella.Text = "Cancella"
    '
    'cmdEsci
    '
    Me.cmdEsci.ImagePath = ""
    Me.cmdEsci.ImageText = ""
    Me.cmdEsci.Location = New System.Drawing.Point(6, 13)
    Me.cmdEsci.Name = "cmdEsci"
    Me.cmdEsci.NTSContextMenu = Nothing
    Me.cmdEsci.Size = New System.Drawing.Size(89, 26)
    Me.cmdEsci.TabIndex = 3
    Me.cmdEsci.Text = "Esci"
    '
    'cmdCopia
    '
    Me.cmdCopia.ImagePath = ""
    Me.cmdCopia.ImageText = ""
    Me.cmdCopia.Location = New System.Drawing.Point(7, 266)
    Me.cmdCopia.Name = "cmdCopia"
    Me.cmdCopia.NTSContextMenu = Nothing
    Me.cmdCopia.Size = New System.Drawing.Size(89, 26)
    Me.cmdCopia.TabIndex = 10
    Me.cmdCopia.Text = "Copia"
    '
    'cmdIncolla
    '
    Me.cmdIncolla.ImagePath = ""
    Me.cmdIncolla.ImageText = ""
    Me.cmdIncolla.Location = New System.Drawing.Point(7, 292)
    Me.cmdIncolla.Name = "cmdIncolla"
    Me.cmdIncolla.NTSContextMenu = Nothing
    Me.cmdIncolla.Size = New System.Drawing.Size(89, 26)
    Me.cmdIncolla.TabIndex = 11
    Me.cmdIncolla.Text = "Incolla"
    '
    'cmdRinomina
    '
    Me.cmdRinomina.ImagePath = ""
    Me.cmdRinomina.ImageText = ""
    Me.cmdRinomina.Location = New System.Drawing.Point(7, 319)
    Me.cmdRinomina.Name = "cmdRinomina"
    Me.cmdRinomina.NTSContextMenu = Nothing
    Me.cmdRinomina.Size = New System.Drawing.Size(89, 26)
    Me.cmdRinomina.TabIndex = 12
    Me.cmdRinomina.Text = "Rinomina"
    '
    'FRM__GESF
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(674, 511)
    Me.Controls.Add(Me.pnForm)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Location = New System.Drawing.Point(70, 46)
    Me.Name = "FRM__GESF"
    Me.Text = "GESTIONE FILE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnForm, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnForm.ResumeLayout(False)
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    CType(Me.grFile, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvFile, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    CType(Me.edNomeFile.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnCommand, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCommand.ResumeLayout(False)
    Me.pnCommand.PerformLayout()
    CType(Me.ckAccessoCompleto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    'init del child normale
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

    If bSaveFile Then
      'visualizzo il textbox x salvare il file
    Else
      pnTop.Height = 50
    End If

    '------------------------------------------------
    'creo e attivo l'entity e inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__GESF", "BE__BASN", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 127930271093451112, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleGesf = CType(oTmp, CLE__BASN)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__FILE", strRemoteServer, strRemotePort)
    AddHandler oCleGesf.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleGesf.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByVal NomeZoom As String, _
                               ByRef Param As CLE__PATB, Optional ByVal Ditta As String = "", _
                               Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    'chiamato dallo zoom

    '----------------
    'solo per non far dare il messaggio id funzione duplicata
    'If CLN__STD.CheckInvokeCustomFunction(

    oCallParamsZoom = Param
    'se devo restituire una directory, visualizzerò solo le dir
    bOnlyDir = Param.bFlag1
    bSaveFile = Param.bLiv2

    Return Init(Menu, Nothing, Ditta, Nothing)
    Return True
  End Function

  Public Overridable Sub InitControls()
    Try
      grvFile.NTSSetParam(oMenu, oApp.Tr(Me, 128995715082421875, "Griglia file"))
      xx_dir.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130255542060675639, "Directory S/N"), "-1", "0")
      xx_name.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130255542090188704, "Nome"), 0)
      xx_size.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130255542107948559, "Dimensione"), "#,##0", 0)
      xx_datemod.NTSSetParamDATA(oMenu, oApp.Tr(Me, 130255542124355557, "Data ultima modifica"), True, "X", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      xx_readonly.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130256162211231415, "Sola lettura S/N"), "-1", "0")
      edNomeFile.NTSSetParam(oMenu, oApp.Tr(Me, 130256326682440748, "Nome del file"), 0)
      ckAccessoCompleto.NTSSetParam(oMenu, oApp.Tr(Me, 130256326682440749, "Disk full access"), "S", "N")
      grvFile.Enabled = False
      grvFile.NTSAllowInsert = False

      '-------------------------------------------------
      'chiamo lo script per inizializzare i controlli caricati con source ext
      NTSScriptExec("InitControls", Me, Nothing)

    Catch ex As Exception 'non usare mai CLN__STD.GestError: la GestError che deve venir chiamata è quella di FRM__CHIL
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub


  Public Overridable Sub FRM__GESF_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Dim strTmp As String = ""
    Dim i As Integer = 0
    Dim strVis As String = "-1"
    Dim strEnab As String = "-1"
    Dim strDir As String = ""
    Dim bAmm As Boolean = True
    Dim strAccvis As String = "S"
    Dim strRegvis As String = "S"
    Dim strAccmod As String = "S"
    Dim strRegmod As String = "S"
    Try
      strDir = oApp.OfficeDir.ToUpper

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      'memorizzo se l'utente è un user CRM
      If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtCRM) Then bModCRM = True
      If bModCRM Then
        bModCRM = oMenu.IsCrmUser(DittaCorrente, bAmm, strAccvis, strAccmod, strRegvis, strRegmod)
      End If

      '--------------------------------------------
      'preparo il datatable
      dsFile.Tables.Add("FILES")
      dsFile.Tables("FILES").Columns.Add("xx_name", GetType(String))
      dsFile.Tables("FILES").Columns.Add("xx_size", GetType(Decimal))
      dsFile.Tables("FILES").Columns.Add("xx_datemod", GetType(DateTime))
      dsFile.Tables("FILES").Columns.Add("xx_dir", GetType(String))
      dsFile.Tables("FILES").Columns.Add("xx_readonly", GetType(String))

      dsFile.Tables("FILES").AcceptChanges()

      'se chiamato per selezionare un file mi predispongo per farlo
      'bnieimpo, in export, chiama per chiedere il nome del nuovo file...
      If Not oCallParamsZoom Is Nothing Then
        cmdSeleziona.Visible = True
        cmdDownload.Visible = False
        'cmdUpload.Visible = False  'se devo selezionare una immagine, forse devo prima caricarla da SBC client
        strFilter = oCallParamsZoom.strTipo
        If ckAccessoCompleto.Checked = False Then
          If oCallParamsZoom.strDescr.Trim <> "" Then
            'visto che posso vedere solo la dir OFFICE e le sue eventuali subdir, 
            'se la dir passata non è office o una sua derivata, visualizzo la office
            If oCallParamsZoom.strDescr.Trim.ToUpper.StartsWith(oApp.OfficeDir.Trim.ToUpper) Or _
               oCallParamsZoom.strDescr.Trim.ToUpper.StartsWith(oApp.ImgDir.Trim.ToUpper) Then
              strDir = oCallParamsZoom.strDescr.Trim
            Else
              '(per ora non gestita: potrei non avere i permessi per poter vedere quella subdir - limitazioni directory CRM!!!)
              strDir = oApp.OfficeDir.ToUpper
            End If
          End If
        End If

        oCallParamsZoom.strOut = ""     'per valori di ritorno
        oCallParamsZoom.strDescr = ""   'per valori di ritorno

        If oCallParamsZoom.bFlag1 Then
          lbTitolo.Text = oApp.Tr(Me, 130256332888420803, "SELEZIONA DIRECTORY")
          cmdEsci.Text = oApp.Tr(Me, 130766777737408598, "Annulla")
        ElseIf bSaveFile Then
          lbTitolo.Text = oApp.Tr(Me, 130256332901762823, "SALVA FILE")
          cmdEsci.Text = oApp.Tr(Me, 130766777880145947, "Annulla")
          cmdSeleziona.Text = oApp.Tr(Me, 130766743688917547, "OK")
          edNomeFile.Text = oCallParamsZoom.strIn
        Else
          lbTitolo.Text = oApp.Tr(Me, 130255541951714984, "SELEZIONA FILE")
          cmdEsci.Text = oApp.Tr(Me, 130766743857980742, "Annulla")
        End If
      Else
        cmdSeleziona.Visible = False
        strFilter = ""
        lbTitolo.Text = oApp.Tr(Me, 128998175961445313, "GESTIONE FILE")

        'If oApp.SO_TYPE <> "WIN" And oApp.SO_TYPE <> "WINTAB" Then
        'su ios/android non riesco a fare l'upload ...
        'cmdUpload.Visible = False ora si può
        'NtsLabel1.Visible = True
        'Else
        NtsLabel1.Visible = False
        'End If
      End If
      If strFilter = "" Then strFilter = "*.*"

      'carico la prima visualizzazione iniziale
      If Not CaricaLista(strDir, strFilter) Then
        Me.Close()
        Return
      End If

      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function CaricaLista(ByVal strDir As String, ByVal strFiltro As String) As Boolean
    Dim strT() As String = Nothing
    Dim strT1() As String = Nothing
    Dim i As Integer = 0
    Dim n As Integer = 0
    Dim fi As System.IO.FileInfo = Nothing
    Dim dttTmp As New System.Data.DataTable
    Dim bOk As Boolean = True
    Dim strDirTmp As String = ""
    Dim dttFile As New DataTable

    Try
      If Not oMenu.GetAccCrm(DittaCorrente, oApp.User.Nome, dttTmp) Then Return False

      dttFile = dsFile.Tables("FILES").Clone
      Dim pk(1) As System.Data.DataColumn
      pk(0) = dttFile.Columns("xx_name")
      dttFile.PrimaryKey = pk
      dttFile.AcceptChanges()

      If strDir = "*" Then
        'devo far vedere i dischi e le connessioni di rete
        grFile.DataSource = Nothing
        dcFile.DataSource = Nothing
        dsFile.Tables("FILES").Clear()
        lbCurrentDir.Text = ""
        strCurDir = ""
        For nDrive As Integer = 0 To System.IO.DriveInfo.GetDrives().Length - 1
          Try
            If NTSCStr(System.IO.DriveInfo.GetDrives()(nDrive).DriveFormat).ToUpper.StartsWith("NTFS") Or _
            NTSCStr(System.IO.DriveInfo.GetDrives()(nDrive).DriveFormat).ToUpper.StartsWith("FAT") Then
              fi = New System.IO.FileInfo(System.IO.DriveInfo.GetDrives()(nDrive).Name)
              dsFile.Tables("FILES").Rows.Add(New Object() {System.IO.DriveInfo.GetDrives()(nDrive).Name, 0, fi.LastWriteTime, "-1", IIf(fi.IsReadOnly, "-1", "0")})
            End If
          Catch ex As Exception
            'errore di disco non pronto (CD, DVD non montato): ignoro
          End Try
        Next
        If oApp.OfficeDir.Trim.ToUpper.StartsWith("\\") Then
          'aggiungo anche la officedir di rete, diversamente non potri risalire ai files di quella cartella
          fi = New System.IO.FileInfo(oApp.OfficeDir.Trim)
          dsFile.Tables("FILES").Rows.Add(New Object() {oApp.OfficeDir.Trim, 0, fi.LastWriteTime, "-1", IIf(fi.IsReadOnly, "-1", "0")})
          'per vedere altre cartelle in rete, connettere unità di rete ad explorer, quindi uscire e rientrare da busnet
        End If
        GoTo COLLEGA_GRIGLIA
      End If

      If strDir(strDir.Length - 1) = "\" Then
        strDir = strDir.Substring(0, strDir.Length - 1)
      End If
      If Not System.IO.Directory.Exists(strDir) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128995727296503906, "Directory '|" & strDir & "|' inesistente sul server"))
        Return False
      End If
      lbCurrentDir.Text = strDir
      If strFiltro.Trim <> "" Then strFilter = strFiltro
      lbFiltro.Text = oApp.Tr(Me, 128998080655380860, "Filtra: '" & strFilter & "'")
      If strFilter = "*.*" Or strFilter.Trim = "" Then lbFiltro.Text = ""
      strCurDir = strDir

      grFile.DataSource = Nothing
      dcFile.DataSource = Nothing

      dsFile.Tables("FILES").Clear()
      'prima carico le dir
      'se sono nella dir radice, posso vedere solo il contenuto della radice e le subdir di cui ho 
      'accesso in base alle autorizzazioni concesse in acccrm
      strT = System.IO.Directory.GetDirectories(strDir & "\")

      If (strDir.ToUpper = oApp.OfficeDir.ToUpper Or strDir.ToUpper = oApp.ImgDir.ToUpper) And bModCRM And ckAccessoCompleto.Checked = False Then
        'se c'è il modulo CRM la dir radice è la officedir: se viene passata uan dir diver sa dalla office dir posso sono vedere quella dir
      Else
        dsFile.Tables("FILES").Rows.Add(New Object() {"DIRECTORY SUPERIORE", -1, NTSCDate("01/01/1900"), "-1"})
      End If

      For i = 0 To strT.Length - 1
        bOk = False
        If strDir.ToUpper = oApp.OfficeDir.ToUpper And bModCRM And ckAccessoCompleto.Checked = False Then 'se il modulo CRM non è abilitato faccio vedere tutto
          'sono nella dir radice (officedir) non devo poter vedere le dir di utenti di cui non ho gli accessi
          strDirTmp = strT(i).Substring(strDir.Length + 1).ToUpper
          For Each dtrT As DataRow In dttTmp.Rows
            If strDirTmp = dtrT!opcr_alopnome.ToString.ToUpper Then
              bOk = True
              If dtrT!opcr_crmvis.ToString = "N" Then bOk = False
              Exit For
            End If
          Next
          'eccezioni: posso sempre andare nella mia dir o in quelle pubbliche
          If strDirTmp = oApp.User.Nome.ToUpper Then bOk = True
          If strDirTmp = "PUBBLICA" Then bOk = True
        Else
          bOk = True
        End If
        If bOk Then
          fi = New System.IO.FileInfo(strT(i))
          If (fi.Attributes And System.IO.FileAttributes.Hidden) <> System.IO.FileAttributes.Hidden Then
            dsFile.Tables("FILES").Rows.Add(New Object() {strT(i).Substring(strDir.Length + 1), 0, fi.LastWriteTime, "-1", IIf(fi.IsReadOnly, "-1", "0")})
          End If
        End If
      Next

      'ora carico i files
      'potrei aver passato il filtro *.doc;*.docx ....
      If bOnlyDir = False Then
        strT1 = strFilter.Split(";"c)
        For n = 0 To strT1.Length - 1
          strT = System.IO.Directory.GetFiles(strDir & "\", strT1(n))
          For i = 0 To strT.Length - 1
            If dttFile.Select("xx_name = " & CStrSQL(strT(i).Substring(strDir.Length + 1))).Length = 0 Then
              If xx_size.Visible = False And xx_readonly.Visible = False And xx_datemod.Visible = False Then
                dttFile.Rows.Add(New Object() {strT(i).Substring(strDir.Length + 1), 0, Nothing, "0", "0"})
              Else
                fi = New System.IO.FileInfo(strT(i))
                If (fi.Attributes And System.IO.FileAttributes.Hidden) <> System.IO.FileAttributes.Hidden Then
                  dttFile.Rows.Add(New Object() {strT(i).Substring(strDir.Length + 1), fi.Length, fi.LastWriteTime, "0", IIf(fi.IsReadOnly, "-1", "0")})
                End If
              End If
            End If
          Next
        Next
      End If    'If strOnlyDir = False Then
      For Each dtrT As DataRow In dttFile.Select("", "xx_name ASC")
        dsFile.Tables("FILES").ImportRow(dtrT)
      Next
      dttFile.Clear()

COLLEGA_GRIGLIA:
      grFile.DataSource = Nothing
      dsFile.Tables("FILES").AcceptChanges()
      dcFile.DataSource = dsFile.Tables("FILES")
      grFile.DataSource = dcFile

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function


  Public Overridable Sub cmdEsci_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEsci.Click
    Try
      If Not oCallParamsZoom Is Nothing Then
        'ho fatto annulla nella selezione di un file, di una dir o in salva file 
        oCallParamsZoom.strOut = "."  '. = annulla
      End If

      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDownload.click
    Try
      If grvFile.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128995776645986328, "Prima posizionarsi sul file da scaricare"))
        Return
      End If
      If NTSCInt(grvFile.NTSGetCurrentDataRow!xx_dir) < 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128995776633027343, "Non è possibile scaricare una directory"))
        Return
      End If

      'per poterlo scaricare prima devo copiarlo nella tmp (che è ina dir del sito), poi farlo scaricare, quindi cancellaro
      IS_ShowFileOnSbc(lbCurrentDir.Text & "\" & grvFile.NTSGetCurrentDataRow!xx_name.ToString, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpload.Click
    Dim strFiles As String = "" 'elenco dei file uplodati, separati da "?"
    Try
      'chiedo al client il file
      RemoveHandler Me.IS_UploadComplete, AddressOf FRM__GESF_IS_UploadComplete 'se faccio upload, poi annullo, non viene chiamata la FRM__GESF_IS_UploadComplete ed avrei la registrazionedell'handle IS_UploadComplete più volte
      AddHandler Me.IS_UploadComplete, AddressOf FRM__GESF_IS_UploadComplete
      IS_ShowUploadFromSbc(lbCurrentDir.Text)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub FRM__GESF_IS_UploadComplete(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Try
      RemoveHandler Me.IS_UploadComplete, AddressOf FRM__GESF_IS_UploadComplete
      oApp.MsgBoxInfo(oApp.Tr(Me, 130422140593326158, "Upload completato. File '|" & Me.IS_UpLoadFileName & "|' salvato correttamente"))

      CaricaLista(strCurDir, strFilter)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdFiltra_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFiltra.Click
    Dim strTmp As String = ""
    Try
      strTmp = oApp.InputBoxNew(oApp.Tr(Me, 128998081423496094, "Filtro per ricerca file (es '*.doc', '*ross*.*', per ricerche multiple '*.gif;*.jpg')" & vbCrLf & "(Per rivedere tutti i files, indicare *.*):"), "")
      If strTmp.Trim <> "" Then CaricaLista(strCurDir, strTmp.Trim)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Try
      If bSaveFile Then
        If edNomeFile.Text.Trim = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130256335935853092, "Impostare il nome del file"))
          Return
        End If
      Else
        If grvFile.NTSGetCurrentDataRow Is Nothing Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128998159258974610, "Prima posizionarsi sul file da selezionare"))
          Return
        End If
        If NTSCInt(grvFile.NTSGetCurrentDataRow!xx_dir) < 0 And bOnlyDir = False Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128998159274658204, "Non è possibile selezionare una directory"))
          Return
        End If

        If grvFile.NTSGetCurrentDataRow!xx_name.ToString = "DIRECTORY SUPERIORE" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130766773908894835, "Non è possibile selezionare 'DIRECTORY SUPERIORE'"))
          Return
        End If
      End If

      If oCallParamsZoom.bLiv2 Then
        'salvo un file 
        oCallParamsZoom.strOut = edNomeFile.Text
      Else
        'seleziono una dir o un file
        oCallParamsZoom.strOut = grvFile.NTSGetCurrentDataRow!xx_name.ToString
      End If

      oCallParamsZoom.strDescr = strCurDir
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdCancella_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancella.Click
    Try
      If grvFile.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128995734743105468, "Prima posizionarsi sul file/sulla directory da rimuovere"))
        Return
      End If

      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128995735384531250, "Confermi la cancellazione di '|" & grvFile.NTSGetCurrentDataRow!xx_name.ToString & "|'?")) = Windows.Forms.DialogResult.No Then Return

      If grvFile.NTSGetCurrentDataRow!xx_dir.ToString = "-1" Then
        'cancello una dir
        If NTSCInt(grvFile.NTSGetCurrentDataRow!xx_size) = -1 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130255541851516574, "Non è possibile cancellare la voce che permette di tornare indietro di una directory"))
          Return
        Else
          If System.IO.Directory.GetFiles(strCurDir & "\" & grvFile.NTSGetCurrentDataRow!xx_name.ToString).Length > 0 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128995736113408203, "Non è possibile cancellare la directory: contiene dei files"))
            Return
          Else
            Try
              System.IO.Directory.Delete(strCurDir & "\" & grvFile.NTSGetCurrentDataRow!xx_name.ToString, False)
            Catch ex As Exception
              oApp.MsgBoxErr(oApp.Tr(Me, 128995742244189453, "Errore in fase di cancellazione directory: ") & vbCrLf & ex.Message)
            End Try
          End If
        End If
      Else
        'cancello un file
        Try
          System.IO.File.Delete(strCurDir & "\" & grvFile.NTSGetCurrentDataRow!xx_name.ToString)
        Catch ex As Exception
          oApp.MsgBoxErr(oApp.Tr(Me, 128995741901396484, "Errore in fase di cancellazione file: ") & vbCrLf & ex.Message)
        End Try
      End If

      CaricaLista(strCurDir, strFilter)


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdCreaDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreaDir.Click
    Dim strTmp As String = ""
    Try
      strTmp = oApp.InputBoxNew("Nome nuova directory", "")
      If strTmp.Trim = "" Then Return
      If dsFile.Tables("FILES").Select("xx_name = " & CStrSQL(strTmp.Trim)).Length > 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128995732983291015, "Esite già una dir o un file con lo stesso nome della directory da creare"))
        Return
      End If
      System.IO.Directory.CreateDirectory(strCurDir & "\" & strTmp.Trim)
      CaricaLista(strCurDir, strFilter)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdGoSubdir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGoSubdir.Click
    Dim strTmp As String = ""
    Try
      If grvFile.NTSGetCurrentDataRow Is Nothing Then Return
      If grvFile.NTSGetCurrentDataRow!xx_dir.ToString = "-1" Then

        If NTSCInt(grvFile.NTSGetCurrentDataRow!xx_size) = -1 Then
          'vado alla dir padre
          If strCurDir.ToUpper = oApp.OfficeDir.ToUpper And bModCRM And ckAccessoCompleto.Checked = False Then
            'sotto alla dir 'OFFICE' non posso andare se l'utente è un utente crm
          Else
            strTmp = strCurDir
            While strTmp(strTmp.Length - 1) <> "\"
              strTmp = strTmp.Substring(0, strTmp.Length - 1)
              If strTmp = "" Then Exit While
            End While
            If strTmp = "" Then
              'tutti i dischi
              CaricaLista("*", strFilter)
            Else
              If strTmp.ToUpper.IndexOf(oApp.OfficeDir.ToUpper) = -1 And bModCRM And ckAccessoCompleto.Checked = False Then
                'se la dir su cui sono non è figlia di officedir non faccio navigare, diversamente potrei sfogliare tutto il disco ...
                oApp.MsgBoxErr(oApp.Tr(Me, 129157948910908203, "Non è possibile spostarsi tra le cartelle se la cartella su cui siamo non è contenuta in |'" & oApp.OfficeDir & "'|"))
                Return
              End If
            End If
          End If
          If System.IO.Directory.Exists(strTmp) Then
            CaricaLista(strTmp, strFilter)
          Else
            'elenco la dir corrente + i dischi fisici del pc
            CaricaLista("*", strFilter)
          End If
        Else
          'vado alla dir figlio
          If strCurDir = "" Then
            strTmp = grvFile.NTSGetCurrentDataRow!xx_name.ToString
          Else
            strTmp = strCurDir & "\" & grvFile.NTSGetCurrentDataRow!xx_name.ToString
          End If
          If strTmp.ToUpper.IndexOf(oApp.OfficeDir.ToUpper) = -1 And bModCRM And ckAccessoCompleto.Checked = False Then
            'se la dir su cui sono non è figlia di officedir non faccio navigare, diversamente potrei sfogliare tutto il disco ...
            oApp.MsgBoxErr(oApp.Tr(Me, 129157951981289062, "Non è possibile spostarsi tra le cartelle se la cartella su cui siamo non è contenuta in |'" & oApp.OfficeDir & "'|"))
            Return
          End If
          CaricaLista(strTmp, strFilter)
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdCopia_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopia.Click
    Try
      If grvFile.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 131040122456679224, "Prima posizionarsi sul file da copiare"))
        Return
      End If

      If grvFile.NTSGetCurrentDataRow!xx_dir.ToString = "-1" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 131040122720236633, "Prima posizionarsi sul file da copiare"))
        Return
      Else
        strDirCopy = lbCurrentDir.Text
        strFileCopy = NTSCStr(grvFile.NTSGetCurrentDataRow!xx_name)
        oApp.MsgBoxInfo(oApp.Tr(Me, 131040123387128618, "File pronto per la copia." & vbCrLf & _
                        "Selezionare la cartella dove copiare il file e selezionare quindi il comando 'Incolla file'"))
        Return
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdIncolla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdIncolla.Click
    Dim strTmp As String = ""
    Try
      If System.IO.File.Exists(lbCurrentDir.Text & "\" & strFileCopy) = False Then
        Try
          System.IO.File.Copy(strDirCopy & "\" & strFileCopy, lbCurrentDir.Text & "\" & strFileCopy)
          CaricaLista(strCurDir, strFilter)
        Catch ex As Exception
          oApp.MsgBoxErr(oApp.Tr(Me, 131040127098740330, "Errore in fase di copia file: ") & vbCrLf & ex.Message)
        End Try
      Else
Riprova:
        strTmp = oApp.InputBoxNew(oApp.Tr(Me, 131040135996287295, "Esiste già un file con lo stesso nome. Nuovo nome:"), strFileCopy)
        If strTmp.Trim = "" Or strTmp = strFileCopy Then
          oApp.MsgBoxErr(oApp.Tr(Me, 131040136419273618, "Copia non eseguita"))
          Return
        End If
        If System.IO.File.Exists(lbCurrentDir.Text & "\" & strTmp) Then
          GoTo Riprova
        End If

        System.IO.File.Copy(strDirCopy & "\" & strFileCopy, lbCurrentDir.Text & "\" & strTmp)
        CaricaLista(strCurDir, strFilter)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdRinomina_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdRinomina.Click
    Dim strTmp As String = ""
    Try
      If grvFile.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 131040129770270617, "Prima posizionarsi sul file da copiare"))
        Return
      End If

      If grvFile.NTSGetCurrentDataRow!xx_dir.ToString = "-1" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 131040129811036423, "Prima posizionarsi sul file da copiare"))
        Return
      Else
        strTmp = oApp.InputBoxNew(oApp.Tr(Me, 131040130162469615, "Nuovo nome:"), NTSCStr(grvFile.NTSGetCurrentDataRow!xx_name))
        If strTmp.Trim = "" Or strTmp = NTSCStr(grvFile.NTSGetCurrentDataRow!xx_name) Then Return

        If System.IO.File.Exists(lbCurrentDir.Text & "\" & strTmp) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 131040134836606064, "Esiste già un file con lo stesso nome. Rinomina non eseguita"))
          Return
        End If
        My.Computer.FileSystem.RenameFile(lbCurrentDir.Text & "\" & NTSCStr(grvFile.NTSGetCurrentDataRow!xx_name), strTmp)
        CaricaLista(strCurDir, strFilter)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub grFile_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grFile.MouseDoubleClick
    Dim strTmp As String = ""
    Try
      If grvFile.NTSGetCurrentDataRow Is Nothing Then Return
      If grvFile.NTSGetCurrentDataRow!xx_dir.ToString = "-1" Then
        cmdGoSubdir_Click(cmdGoSubdir, Nothing)
      Else
        'posso selezionare il file solo se sono aperto in modalità 'selezina file'
        If cmdSeleziona.Visible Then cmdSeleziona_Click(cmdSeleziona, Nothing)
      End If

    Catch ex As Exception 'non usare mai CLN__STD.GestError: la GestError che deve venir chiamata è quella di FRM__CHIL 'non usare mai CLN__STD.GestError: la GestError che deve venir chiamata è quella di FRM__CHIL
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub grvFile_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvFile.NTSFocusedRowChanged
    Try
      If Not grvFile.NTSGetCurrentDataRow Is Nothing Then
        If bSaveFile And grvFile.NTSGetCurrentDataRow!xx_dir.ToString <> "-1" Then
          edNomeFile.Text = grvFile.NTSGetCurrentDataRow!xx_name.ToString
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdPassaa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPassaa.Click
    Try
      grFile_MouseDoubleClick(grFile, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckAccessoCompleto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckAccessoCompleto.CheckedChanged
    Try
      If ckAccessoCompleto.Checked = False Then
        'se ero in una dir non derivata da officedir o omgdir, mi sposto su quella di partenza
        If strCurDir.ToUpper.StartsWith(oApp.OfficeDir.Trim.ToUpper) Or _
           strCurDir.ToUpper.StartsWith(oApp.ImgDir.Trim.ToUpper) Then
          CaricaLista(strCurDir, strFilter)
        Else
          CaricaLista(oApp.OfficeDir.Trim.ToUpper, strFilter)
        End If
      Else
        'aggiungo eventualmente la dir 'directory superiore'
        CaricaLista(strCurDir, strFilter)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

End Class
