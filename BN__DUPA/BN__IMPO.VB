Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__IMPO

#Region "Variabili"
  Public oCleDupa As CLE__DUPA
  Public oCallParams As CLE__CLDP
  Private components As System.ComponentModel.IContainer
  Public strTabella As String = ""
  Public strMess1 As String = ""
  Public strMess2 As String = ""
  Public strMess3 As String = ""
  Public strMess4 As String = ""
#End Region

#Region "Variaribli Componenti Form e InitializeComponent"
  Public WithEvents pnImpo As NTSInformatica.NTSPanel
  Public WithEvents fmDatabaseDestinazione As NTSInformatica.NTSGroupBox
  Public WithEvents fmImporta As NTSInformatica.NTSGroupBox
  Public WithEvents opImportaAlcuniDati As NTSInformatica.NTSRadioButton
  Public WithEvents opImportaTuttiDati As NTSInformatica.NTSRadioButton
  Public WithEvents liTabelle As NTSInformatica.NTSListBox
  Public WithEvents fmOperazione As NTSInformatica.NTSGroupBox
  Public WithEvents cmdDesAll As NTSInformatica.NTSButton
  Public WithEvents cmdSelAll As NTSInformatica.NTSButton
  Public WithEvents opSovrascrivi As NTSInformatica.NTSRadioButton
  Public WithEvents opSostituisci As NTSInformatica.NTSRadioButton
  Public WithEvents opAccoda As NTSInformatica.NTSRadioButton
  Public WithEvents fmDatabaseOrigine As NTSInformatica.NTSGroupBox
  Public WithEvents cbDatabaseOrigine As NTSInformatica.NTSComboBox
  Public WithEvents lbDatabaseOrigine As NTSInformatica.NTSLabel
  Public WithEvents lbDatabaseOrigineDescr As NTSInformatica.NTSLabel
  Public WithEvents cbDatabaseDestinazione As NTSInformatica.NTSComboBox
  Public WithEvents lbDatabaseDestinazione As NTSInformatica.NTSLabel
  Public WithEvents lbDatabaseDestinazioneDescr As NTSInformatica.NTSLabel
  Public WithEvents pnAlcuniDati As NTSInformatica.NTSPanel
  Public WithEvents pbProgressBar As System.Windows.Forms.ProgressBar
  Public WithEvents lbOperazione As NTSInformatica.NTSLabel
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbImporta As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Me.pnImpo = New NTSInformatica.NTSPanel
    Me.pbProgressBar = New System.Windows.Forms.ProgressBar
    Me.lbOperazione = New NTSInformatica.NTSLabel
    Me.fmImporta = New NTSInformatica.NTSGroupBox
    Me.pnAlcuniDati = New NTSInformatica.NTSPanel
    Me.fmOperazione = New NTSInformatica.NTSGroupBox
    Me.opSovrascrivi = New NTSInformatica.NTSRadioButton
    Me.opSostituisci = New NTSInformatica.NTSRadioButton
    Me.opAccoda = New NTSInformatica.NTSRadioButton
    Me.cmdDesAll = New NTSInformatica.NTSButton
    Me.liTabelle = New NTSInformatica.NTSListBox
    Me.cmdSelAll = New NTSInformatica.NTSButton
    Me.opImportaAlcuniDati = New NTSInformatica.NTSRadioButton
    Me.opImportaTuttiDati = New NTSInformatica.NTSRadioButton
    Me.fmDatabaseOrigine = New NTSInformatica.NTSGroupBox
    Me.cbDatabaseOrigine = New NTSInformatica.NTSComboBox
    Me.lbDatabaseOrigine = New NTSInformatica.NTSLabel
    Me.lbDatabaseOrigineDescr = New NTSInformatica.NTSLabel
    Me.fmDatabaseDestinazione = New NTSInformatica.NTSGroupBox
    Me.cbDatabaseDestinazione = New NTSInformatica.NTSComboBox
    Me.lbDatabaseDestinazione = New NTSInformatica.NTSLabel
    Me.lbDatabaseDestinazioneDescr = New NTSInformatica.NTSLabel
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbImporta = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    CType(Me.pnImpo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnImpo.SuspendLayout()
    CType(Me.fmImporta, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmImporta.SuspendLayout()
    CType(Me.pnAlcuniDati, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAlcuniDati.SuspendLayout()
    CType(Me.fmOperazione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmOperazione.SuspendLayout()
    CType(Me.opSovrascrivi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opSostituisci.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opAccoda.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.liTabelle, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opImportaAlcuniDati.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opImportaTuttiDati.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmDatabaseOrigine, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmDatabaseOrigine.SuspendLayout()
    CType(Me.cbDatabaseOrigine.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmDatabaseDestinazione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmDatabaseDestinazione.SuspendLayout()
    CType(Me.cbDatabaseDestinazione.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'DevXDefaultLookAndFeel
    '
    
    '
    'pnImpo
    '
    Me.pnImpo.AllowDrop = True
    Me.pnImpo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnImpo.Appearance.Options.UseBackColor = True
    Me.pnImpo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnImpo.Controls.Add(Me.pbProgressBar)
    Me.pnImpo.Controls.Add(Me.lbOperazione)
    Me.pnImpo.Controls.Add(Me.fmImporta)
    Me.pnImpo.Controls.Add(Me.fmDatabaseOrigine)
    Me.pnImpo.Controls.Add(Me.fmDatabaseDestinazione)
    Me.pnImpo.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnImpo.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnImpo.Location = New System.Drawing.Point(0, 24)
    Me.pnImpo.Name = "pnImpo"
    Me.pnImpo.Size = New System.Drawing.Size(549, 418)
    Me.pnImpo.TabIndex = 0
    Me.pnImpo.Text = "NtsPanel1"
    '
    'pbProgressBar
    '
    Me.pbProgressBar.Location = New System.Drawing.Point(20, 376)
    Me.pbProgressBar.Name = "pbProgressBar"
    Me.pbProgressBar.Size = New System.Drawing.Size(511, 23)
    Me.pbProgressBar.TabIndex = 5
    '
    'lbOperazione
    '
    Me.lbOperazione.AutoSize = True
    Me.lbOperazione.BackColor = System.Drawing.Color.Transparent
    Me.lbOperazione.Location = New System.Drawing.Point(20, 345)
    Me.lbOperazione.Name = "lbOperazione"
    Me.lbOperazione.NTSDbField = ""
    Me.lbOperazione.Size = New System.Drawing.Size(148, 13)
    Me.lbOperazione.TabIndex = 3
    Me.lbOperazione.Text = "Nessuna operazione in corso."
    Me.lbOperazione.UseMnemonic = False
    '
    'fmImporta
    '
    Me.fmImporta.AllowDrop = True
    Me.fmImporta.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmImporta.Appearance.Options.UseBackColor = True
    Me.fmImporta.Controls.Add(Me.pnAlcuniDati)
    Me.fmImporta.Controls.Add(Me.opImportaAlcuniDati)
    Me.fmImporta.Controls.Add(Me.opImportaTuttiDati)
    Me.fmImporta.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmImporta.Location = New System.Drawing.Point(12, 12)
    Me.fmImporta.Name = "fmImporta"
    Me.fmImporta.Size = New System.Drawing.Size(246, 326)
    Me.fmImporta.TabIndex = 1
    Me.fmImporta.Text = "Importa: "
    '
    'pnAlcuniDati
    '
    Me.pnAlcuniDati.AllowDrop = True
    Me.pnAlcuniDati.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAlcuniDati.Appearance.Options.UseBackColor = True
    Me.pnAlcuniDati.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAlcuniDati.Controls.Add(Me.fmOperazione)
    Me.pnAlcuniDati.Controls.Add(Me.cmdDesAll)
    Me.pnAlcuniDati.Controls.Add(Me.liTabelle)
    Me.pnAlcuniDati.Controls.Add(Me.cmdSelAll)
    Me.pnAlcuniDati.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnAlcuniDati.Location = New System.Drawing.Point(0, 74)
    Me.pnAlcuniDati.Name = "pnAlcuniDati"
    Me.pnAlcuniDati.Size = New System.Drawing.Size(246, 252)
    Me.pnAlcuniDati.TabIndex = 7
    Me.pnAlcuniDati.Text = "NtsPanel1"
    '
    'fmOperazione
    '
    Me.fmOperazione.AllowDrop = True
    Me.fmOperazione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmOperazione.Appearance.Options.UseBackColor = True
    Me.fmOperazione.Controls.Add(Me.opSovrascrivi)
    Me.fmOperazione.Controls.Add(Me.opSostituisci)
    Me.fmOperazione.Controls.Add(Me.opAccoda)
    Me.fmOperazione.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmOperazione.Location = New System.Drawing.Point(14, 7)
    Me.fmOperazione.Name = "fmOperazione"
    Me.fmOperazione.Size = New System.Drawing.Size(216, 84)
    Me.fmOperazione.TabIndex = 2
    Me.fmOperazione.Text = "Operazione sui record:"
    '
    'opSovrascrivi
    '
    Me.opSovrascrivi.Cursor = System.Windows.Forms.Cursors.Default
    Me.opSovrascrivi.Location = New System.Drawing.Point(5, 61)
    Me.opSovrascrivi.Name = "opSovrascrivi"
    Me.opSovrascrivi.NTSCheckValue = "S"
    Me.opSovrascrivi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opSovrascrivi.Properties.Appearance.Options.UseBackColor = True
    Me.opSovrascrivi.Properties.Caption = "Sovrascrivi a &parità di codice"
    Me.opSovrascrivi.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opSovrascrivi.Size = New System.Drawing.Size(203, 18)
    Me.opSovrascrivi.TabIndex = 2
    '
    'opSostituisci
    '
    Me.opSostituisci.Cursor = System.Windows.Forms.Cursors.Default
    Me.opSostituisci.Location = New System.Drawing.Point(5, 42)
    Me.opSostituisci.Name = "opSostituisci"
    Me.opSostituisci.NTSCheckValue = "S"
    Me.opSostituisci.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opSostituisci.Properties.Appearance.Options.UseBackColor = True
    Me.opSostituisci.Properties.Caption = "&Sostituisci completamente"
    Me.opSostituisci.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opSostituisci.Size = New System.Drawing.Size(203, 18)
    Me.opSostituisci.TabIndex = 1
    '
    'opAccoda
    '
    Me.opAccoda.Cursor = System.Windows.Forms.Cursors.Default
    Me.opAccoda.EditValue = True
    Me.opAccoda.Location = New System.Drawing.Point(5, 23)
    Me.opAccoda.Name = "opAccoda"
    Me.opAccoda.NTSCheckValue = "S"
    Me.opAccoda.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opAccoda.Properties.Appearance.Options.UseBackColor = True
    Me.opAccoda.Properties.Caption = "Accoda &nuovi record"
    Me.opAccoda.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opAccoda.Size = New System.Drawing.Size(203, 18)
    Me.opAccoda.TabIndex = 0
    '
    'cmdDesAll
    '
    Me.cmdDesAll.Location = New System.Drawing.Point(129, 216)
    Me.cmdDesAll.Name = "cmdDesAll"
    Me.cmdDesAll.Size = New System.Drawing.Size(101, 25)
    Me.cmdDesAll.TabIndex = 5
    Me.cmdDesAll.Text = "Deseleziona t&utto"
    '
    'liTabelle
    '
    Me.liTabelle.Cursor = System.Windows.Forms.Cursors.Default
    Me.liTabelle.ItemHeight = 14
    Me.liTabelle.Location = New System.Drawing.Point(14, 97)
    Me.liTabelle.Name = "liTabelle"
    Me.liTabelle.NTSDbField = ""
    Me.liTabelle.Size = New System.Drawing.Size(216, 113)
    Me.liTabelle.TabIndex = 3
    '
    'cmdSelAll
    '
    Me.cmdSelAll.Location = New System.Drawing.Point(14, 216)
    Me.cmdSelAll.Name = "cmdSelAll"
    Me.cmdSelAll.Size = New System.Drawing.Size(101, 25)
    Me.cmdSelAll.TabIndex = 4
    Me.cmdSelAll.Text = "Se&leziona tutto"
    '
    'opImportaAlcuniDati
    '
    Me.opImportaAlcuniDati.Cursor = System.Windows.Forms.Cursors.Default
    Me.opImportaAlcuniDati.Location = New System.Drawing.Point(14, 50)
    Me.opImportaAlcuniDati.Name = "opImportaAlcuniDati"
    Me.opImportaAlcuniDati.NTSCheckValue = "S"
    Me.opImportaAlcuniDati.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opImportaAlcuniDati.Properties.Appearance.Options.UseBackColor = True
    Me.opImportaAlcuniDati.Properties.Caption = "&Alcuni dati"
    Me.opImportaAlcuniDati.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opImportaAlcuniDati.Size = New System.Drawing.Size(116, 18)
    Me.opImportaAlcuniDati.TabIndex = 1
    '
    'opImportaTuttiDati
    '
    Me.opImportaTuttiDati.Cursor = System.Windows.Forms.Cursors.Default
    Me.opImportaTuttiDati.EditValue = True
    Me.opImportaTuttiDati.Location = New System.Drawing.Point(14, 32)
    Me.opImportaTuttiDati.Name = "opImportaTuttiDati"
    Me.opImportaTuttiDati.NTSCheckValue = "S"
    Me.opImportaTuttiDati.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opImportaTuttiDati.Properties.Appearance.Options.UseBackColor = True
    Me.opImportaTuttiDati.Properties.Caption = "&Tutti i dati"
    Me.opImportaTuttiDati.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opImportaTuttiDati.Size = New System.Drawing.Size(116, 18)
    Me.opImportaTuttiDati.TabIndex = 0
    '
    'fmDatabaseOrigine
    '
    Me.fmDatabaseOrigine.AllowDrop = True
    Me.fmDatabaseOrigine.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmDatabaseOrigine.Appearance.Options.UseBackColor = True
    Me.fmDatabaseOrigine.Controls.Add(Me.cbDatabaseOrigine)
    Me.fmDatabaseOrigine.Controls.Add(Me.lbDatabaseOrigine)
    Me.fmDatabaseOrigine.Controls.Add(Me.lbDatabaseOrigineDescr)
    Me.fmDatabaseOrigine.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmDatabaseOrigine.Location = New System.Drawing.Point(264, 12)
    Me.fmDatabaseOrigine.Name = "fmDatabaseOrigine"
    Me.fmDatabaseOrigine.Size = New System.Drawing.Size(274, 160)
    Me.fmDatabaseOrigine.TabIndex = 2
    Me.fmDatabaseOrigine.Text = "Database di origine: "
    '
    'cbDatabaseOrigine
    '
    Me.cbDatabaseOrigine.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbDatabaseOrigine.DataSource = Nothing
    Me.cbDatabaseOrigine.DisplayMember = ""
    Me.cbDatabaseOrigine.Location = New System.Drawing.Point(68, 23)
    Me.cbDatabaseOrigine.Name = "cbDatabaseOrigine"
    Me.cbDatabaseOrigine.NTSDbField = ""
    Me.cbDatabaseOrigine.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbDatabaseOrigine.Properties.DropDownRows = 30
    Me.cbDatabaseOrigine.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbDatabaseOrigine.SelectedValue = ""
    Me.cbDatabaseOrigine.Size = New System.Drawing.Size(136, 20)
    Me.cbDatabaseOrigine.TabIndex = 525
    Me.cbDatabaseOrigine.ValueMember = ""
    '
    'lbDatabaseOrigine
    '
    Me.lbDatabaseOrigine.AutoSize = True
    Me.lbDatabaseOrigine.BackColor = System.Drawing.Color.Transparent
    Me.lbDatabaseOrigine.Location = New System.Drawing.Point(5, 26)
    Me.lbDatabaseOrigine.Name = "lbDatabaseOrigine"
    Me.lbDatabaseOrigine.NTSDbField = ""
    Me.lbDatabaseOrigine.Size = New System.Drawing.Size(57, 13)
    Me.lbDatabaseOrigine.TabIndex = 524
    Me.lbDatabaseOrigine.Text = "Database:"
    Me.lbDatabaseOrigine.UseMnemonic = False
    '
    'lbDatabaseOrigineDescr
    '
    Me.lbDatabaseOrigineDescr.AutoSize = True
    Me.lbDatabaseOrigineDescr.BackColor = System.Drawing.Color.Transparent
    Me.lbDatabaseOrigineDescr.Location = New System.Drawing.Point(5, 46)
    Me.lbDatabaseOrigineDescr.Name = "lbDatabaseOrigineDescr"
    Me.lbDatabaseOrigineDescr.NTSDbField = ""
    Me.lbDatabaseOrigineDescr.Size = New System.Drawing.Size(83, 13)
    Me.lbDatabaseOrigineDescr.TabIndex = 524
    Me.lbDatabaseOrigineDescr.Text = "Server\Istanza:"
    Me.lbDatabaseOrigineDescr.UseMnemonic = False
    '
    'fmDatabaseDestinazione
    '
    Me.fmDatabaseDestinazione.AllowDrop = True
    Me.fmDatabaseDestinazione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmDatabaseDestinazione.Appearance.Options.UseBackColor = True
    Me.fmDatabaseDestinazione.Controls.Add(Me.cbDatabaseDestinazione)
    Me.fmDatabaseDestinazione.Controls.Add(Me.lbDatabaseDestinazione)
    Me.fmDatabaseDestinazione.Controls.Add(Me.lbDatabaseDestinazioneDescr)
    Me.fmDatabaseDestinazione.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmDatabaseDestinazione.Location = New System.Drawing.Point(264, 178)
    Me.fmDatabaseDestinazione.Name = "fmDatabaseDestinazione"
    Me.fmDatabaseDestinazione.Size = New System.Drawing.Size(274, 160)
    Me.fmDatabaseDestinazione.TabIndex = 2
    Me.fmDatabaseDestinazione.Text = "Database di destinazione: "
    '
    'cbDatabaseDestinazione
    '
    Me.cbDatabaseDestinazione.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbDatabaseDestinazione.DataSource = Nothing
    Me.cbDatabaseDestinazione.DisplayMember = ""
    Me.cbDatabaseDestinazione.Location = New System.Drawing.Point(68, 23)
    Me.cbDatabaseDestinazione.Name = "cbDatabaseDestinazione"
    Me.cbDatabaseDestinazione.NTSDbField = ""
    Me.cbDatabaseDestinazione.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbDatabaseDestinazione.Properties.DropDownRows = 30
    Me.cbDatabaseDestinazione.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbDatabaseDestinazione.SelectedValue = ""
    Me.cbDatabaseDestinazione.Size = New System.Drawing.Size(136, 20)
    Me.cbDatabaseDestinazione.TabIndex = 525
    Me.cbDatabaseDestinazione.ValueMember = ""
    '
    'lbDatabaseDestinazione
    '
    Me.lbDatabaseDestinazione.AutoSize = True
    Me.lbDatabaseDestinazione.BackColor = System.Drawing.Color.Transparent
    Me.lbDatabaseDestinazione.Location = New System.Drawing.Point(5, 26)
    Me.lbDatabaseDestinazione.Name = "lbDatabaseDestinazione"
    Me.lbDatabaseDestinazione.NTSDbField = ""
    Me.lbDatabaseDestinazione.Size = New System.Drawing.Size(57, 13)
    Me.lbDatabaseDestinazione.TabIndex = 524
    Me.lbDatabaseDestinazione.Text = "Database:"
    Me.lbDatabaseDestinazione.UseMnemonic = False
    '
    'lbDatabaseDestinazioneDescr
    '
    Me.lbDatabaseDestinazioneDescr.AutoSize = True
    Me.lbDatabaseDestinazioneDescr.BackColor = System.Drawing.Color.Transparent
    Me.lbDatabaseDestinazioneDescr.Location = New System.Drawing.Point(5, 46)
    Me.lbDatabaseDestinazioneDescr.Name = "lbDatabaseDestinazioneDescr"
    Me.lbDatabaseDestinazioneDescr.NTSDbField = ""
    Me.lbDatabaseDestinazioneDescr.Size = New System.Drawing.Size(83, 13)
    Me.lbDatabaseDestinazioneDescr.TabIndex = 524
    Me.lbDatabaseDestinazioneDescr.Text = "Server\Istanza:"
    Me.lbDatabaseDestinazioneDescr.UseMnemonic = False
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbImporta, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 45
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImporta), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbImporta
    '
    Me.tlbImporta.Caption = "Importa"
    Me.tlbImporta.Id = 0
    Me.tlbImporta.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbImporta.Name = "tlbImporta"
    Me.tlbImporta.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Id = 18
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Id = 19
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'FRM__IMPO
    '
    Me.ClientSize = New System.Drawing.Size(549, 442)
    Me.Controls.Add(Me.pnImpo)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.HelpContext = "bs--impo.htm"
    Me.LookAndFeel.SkinName = "Money Twins"
    Me.Name = "FRM__IMPO"
    Me.Text = "IMPORTA DATI"
    CType(Me.pnImpo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnImpo.ResumeLayout(False)
    Me.pnImpo.PerformLayout()
    CType(Me.fmImporta, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmImporta.ResumeLayout(False)
    Me.fmImporta.PerformLayout()
    CType(Me.pnAlcuniDati, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAlcuniDati.ResumeLayout(False)
    CType(Me.fmOperazione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmOperazione.ResumeLayout(False)
    Me.fmOperazione.PerformLayout()
    CType(Me.opSovrascrivi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opSostituisci.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opAccoda.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.liTabelle, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opImportaAlcuniDati.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opImportaTuttiDati.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmDatabaseOrigine, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmDatabaseOrigine.ResumeLayout(False)
    Me.fmDatabaseOrigine.PerformLayout()
    CType(Me.cbDatabaseOrigine.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmDatabaseDestinazione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmDatabaseDestinazione.ResumeLayout(False)
    Me.fmDatabaseDestinazione.PerformLayout()
    CType(Me.cbDatabaseDestinazione.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
#End Region

  Public Overridable Sub InitEntity(ByRef cleCedi As CLE__DUPA)
    oCleDupa = cleCedi
    AddHandler oCleDupa.RemoteEvent, AddressOf GestisciEventiEntity
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
  Public Overridable Sub LoadImage()
    Try
      tlbImporta.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
    Catch ex As Exception
    End Try
    Try
      tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
    Catch ex As Exception
    End Try
    Try
      tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
    Catch ex As Exception
    End Try
  End Sub
  Public Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      LoadImage()
      tlbMain.NTSSetToolTip()

      liTabelle.NTSSetParam(oApp.Tr(Me, 128345191343148647, "Tabelle"))
      opSovrascrivi.NTSSetParam(oMenu, oApp.Tr(Me, 128345191343304897, "Sovrascrivi a &parità di codice"), "S")
      opSostituisci.NTSSetParam(oMenu, oApp.Tr(Me, 128345191343461147, "&Sostituisci completamente"), "S")
      opAccoda.NTSSetParam(oMenu, oApp.Tr(Me, 128345191342617397, "Accoda &nuovi record"), "S")
      opImportaAlcuniDati.NTSSetParam(oMenu, oApp.Tr(Me, 128345191343617397, "&Alcuni dati"), "S")
      opImportaTuttiDati.NTSSetParam(oMenu, oApp.Tr(Me, 128345191343773647, "&Tutti i dati"), "S")

      liTabelle.SelectionMode = SelectionMode.MultiSimple

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
    Dim dttTabelle As New DataTable()
    Dim dttDatabaseOrigine As New DataTable()
    Dim dttDatabaseDestinazione As New DataTable()
    Try
      dttTabelle.Columns.Add("cod", GetType(String))
      dttTabelle.Columns.Add("val", GetType(String))
      dttTabelle.Rows.Add(New Object() {"tabpcon", "Piani dei Conti (e riclass.)"})
      dttTabelle.Rows.Add(New Object() {"tabling", "Lingue Estere"})
      dttTabelle.Rows.Add(New Object() {"tabstat", "Stati Esteri"})
      dttTabelle.Rows.Add(New Object() {"comuni", "Comuni e Cap"})
      If CLN__STD.FRIENDLY = False Then dttTabelle.Rows.Add(New Object() {"anagen", "Anagrafiche Generali"})
      dttTabelle.Rows.Add(New Object() {"tabtacq", "Tipi Operazioni Iva"})
      dttTabelle.Rows.Add(New Object() {"tabciva", "Codici Iva"})
      If CLN__STD.FRIENDLY = False Then
        dttTabelle.Rows.Add(New Object() {"tabgrua", "Gruppi di Aziende"})
        dttTabelle.Rows.Add(New Object() {"tabstud", "Studi (in studi associati)"})
        dttTabelle.Rows.Add(New Object() {"tabruaz", "Ruoli Aziendali"})
      End If
      dttTabelle.Rows.Add(New Object() {"tabinsg", "Inizializzazione Azienda"})
      dttTabelle.Rows.Add(New Object() {"tabpaga", "Forme di Pagamento"})
      dttTabelle.Rows.Add(New Object() {"tabcovg", "Contropartite generali"})
      dttTabelle.Rows.Add(New Object() {"tabvalu", "Valute Estere"})
      dttTabelle.Rows.Add(New Object() {"tabcauc", "Causali di Contabilità"})
      dttTabelle.Rows.Add(New Object() {"tabpecg", "Personalizz. Contabilita Gener"})
      If CLN__STD.FRIENDLY = False Then
        dttTabelle.Rows.Add(New Object() {"tabcace", "Categ. Cespiti (e tab. minist)"})
        dttTabelle.Rows.Add(New Object() {"tabcamc", "Causali mov. Cespiti"})
        dttTabelle.Rows.Add(New Object() {"tabpece", "Personalizzazione Cespiti"})
        dttTabelle.Rows.Add(New Object() {"tabcoce", "Contabilizzazione Cespiti"})
        dttTabelle.Rows.Add(New Object() {"tablmup", "Limiti Uso promiscuo"})
        dttTabelle.Rows.Add(New Object() {"tabmpor", "Modalità di Trasporto (INTRA)"})
        dttTabelle.Rows.Add(New Object() {"tabntra", "Natura transazione (INTRA)"})
        dttTabelle.Rows.Add(New Object() {"tabrsta", "Regimi Statistici (INTRA)"})
      End If
      dttTabelle.Rows.Add(New Object() {"tabcaum", "Causali Magazzino"})
      dttTabelle.Rows.Add(New Object() {"tabbotr", "Bolli su Tratte"})
      If CLN__STD.FRIENDLY = False Then dttTabelle.Rows.Add(New Object() {"tabenas", "Tabella Enasarco"})
      dttTabelle.Rows.Add(New Object() {"tabpeve", "Personalizzazione Vendite"})
      dttTabelle.Rows.Add(New Object() {"tabpeac", "Personalizzazione Acquisti"})
      If CLN__STD.FRIENDLY = False Then
        dttTabelle.Rows.Add(New Object() {"tabpepa", "Personalizzazione Parcellaz."})
        dttTabelle.Rows.Add(New Object() {"tabrtac", "Tipo assogg. R.A."})
        dttTabelle.Rows.Add(New Object() {"tabcaus", "Causali ricorrenti ritenute"})
        dttTabelle.Rows.Add(New Object() {"tabtpco", "Tab. contabilizz. Ritenute"})
        dttTabelle.Rows.Add(New Object() {"tabpcca", "Piano dei Conti CA-DC"})
        dttTabelle.Rows.Add(New Object() {"tabcaca", "Causali CA"})
        dttTabelle.Rows.Add(New Object() {"tabdriv", "Drivers ribaltamenti"})
        dttTabelle.Rows.Add(New Object() {"tabscbg", "Voci e gruppi budget CA-DC"})
        dttTabelle.Rows.Add(New Object() {"tabtcdc", "Tipi entità CA-DC"})
        dttTabelle.Rows.Add(New Object() {"tabpeca", "Personalizzazione CA-DC"})
        dttTabelle.Rows.Add(New Object() {"caschebut", "Schemi Budget CA-DC"})
      End If
      dttTabelle.Rows.Add(New Object() {"altretabel", "Altre Tabelle comuni"})
      dttTabelle.AcceptChanges()
      liTabelle.DataSource = dttTabelle
      liTabelle.DisplayMember = "val"
      liTabelle.ValueMember = "cod"

      dttDatabaseOrigine.Columns.Add("cod", GetType(String))
      dttDatabaseOrigine.Columns.Add("val", GetType(String))
      For i As Integer = 0 To oCleDupa.dsShared.Tables(strTabella).Rows.Count - 1
        If NTSCStr(oCleDupa.dsShared.Tables(strTabella).Rows(i)!az_adoprovider) = "SQLOLEDB" Then dttDatabaseOrigine.Rows.Add(New Object() {oCleDupa.dsShared.Tables(strTabella).Rows(i)!AzCodaz, oCleDupa.dsShared.Tables(strTabella).Rows(i)!AzCodaz})
      Next
      dttDatabaseOrigine.AcceptChanges()
      cbDatabaseOrigine.DataSource = dttDatabaseOrigine
      cbDatabaseOrigine.DisplayMember = "val"
      cbDatabaseOrigine.ValueMember = "cod"

      dttDatabaseDestinazione.Columns.Add("cod", GetType(String))
      dttDatabaseDestinazione.Columns.Add("val", GetType(String))
      For i As Integer = 0 To oCleDupa.dsShared.Tables(strTabella).Rows.Count - 1
        If NTSCStr(oCleDupa.dsShared.Tables(strTabella).Rows(i)!az_adoprovider) = "SQLOLEDB" Then dttDatabaseDestinazione.Rows.Add(New Object() {oCleDupa.dsShared.Tables(strTabella).Rows(i)!AzCodaz, oCleDupa.dsShared.Tables(strTabella).Rows(i)!AzCodaz})
      Next
      dttDatabaseDestinazione.AcceptChanges()
      cbDatabaseDestinazione.DataSource = dttDatabaseDestinazione
      cbDatabaseDestinazione.DisplayMember = "val"
      cbDatabaseDestinazione.ValueMember = "cod"
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub ImpostaTradMess()
    Try
      strTabella = oApp.Tr(Me, 130421072680142802, "AZIENDE")
      strMess1 = oApp.Tr(Me, 130421061260573152, "Windows integrata")
      strMess2 = oApp.Tr(Me, 130421062292448152, "SQL Server tramite ID e password")
      strMess3 = oApp.Tr(Me, 130421063276359383, "Si")
      strMess4 = oApp.Tr(Me, 130421063298234523, "No")
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

#Region "EventiForm"
  Public Overridable Sub FRM__IMPD_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      '-------------------------------------------------
      ImpostaTradMess()
      CaricaCombo()
      InitControls()
      '-------------------------------------------------
      'default
      '-------------------------------------------------
      cbDatabaseOrigine.SelectedValue = UCase(oApp.Db.Nome)
      cbDatabaseDestinazione.SelectedValue = oCallParams.strPar1
      opImportaTuttiDati.Checked = True
      opImportaAlcuniDati_CheckedChanged(Nothing, Nothing)

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      '-------------------------------------------------
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRM__IMPO_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
    Try
      Me.Text = UCase(oApp.Tr(Me, 129024303599537453, "IMPORTA DATI"))
      opImportaTuttiDati.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function ImportaDati() As Boolean

    Dim dsOrderTbl As DataSet = Nothing
    Dim bImportError As Boolean = False
    Try
      'Controlla se è stata selezionata almeno una voce se scelta opzione importa alcuni dati
      If opImportaAlcuniDati.Checked And liTabelle.SelectedIndices.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128345812743546736, "Selezionare almeno una voce prima di passare all'importazione dei dati."))
        Return False
      End If

      'Verifica che i db di origine e di destinazione siano diversi
      If cbDatabaseOrigine.SelectedValue = cbDatabaseDestinazione.SelectedValue Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128345812743556836, "Impossibile importare i dati. L'azienda di origine e quella di destinazione coincidono. Operazione Annullata!"))
        Return False
      End If

      'Chiede conferma prima di procedere
      If opImportaTuttiDati.Checked Then
        If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128345812743546836, "Tutti i dati dell'azienda |" & cbDatabaseDestinazione.SelectedValue & "| andranno persi!" & vbCrLf & vbCrLf & "Continuare ?")) = Windows.Forms.DialogResult.No Then
          Return False
        End If
      End If

      'Verifica la connetivita ai database
      If Not oCleDupa.ImportaCheckConnection(oCleDupa.dsShared.Tables(strTabella).Select("AzCodaz = '" & cbDatabaseOrigine.SelectedValue & "'")(0), _
                                             oCleDupa.dsShared.Tables(strTabella).Select("AzCodaz = '" & cbDatabaseDestinazione.SelectedValue & "'")(0)) Then
        Return False
      End If


      '---------------------------------------------------------------------------------------
      '--- Apre un recordset su ORDERTBL selezionando le tabelle
      '--- Se non esistono dati avvisa ed esce
      '---------------------------------------------------------------------------------------
      If Not oCleDupa.ImportaGetOrderTbl(dsOrderTbl) Then Return False
      If Not dsOrderTbl.Tables("ORDERTBL").Rows.Count > 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128346017755211396, "Tabella 'OrderTbl' vuota. Impossibile stabilire l'ordine delle operazioni da eseguire." & vbCrLf & "Operazione Annullata."))
        Return False
      End If

      'Controllo versioni
      Dim GetReleaseDestinazione As String = ""
      Dim GetReleaseOrigine As String = ""
      Dim dttDest As New DataTable
      Dim dttOrig As New DataTable
      If Not oCleDupa.CheckRelease(dttDest, dttOrig) Then Return False
      If dttDest.Rows.Count = 0 Then
        GetReleaseDestinazione = oApp.Tr(Me, 128740869960625027, "informazioni sulla versione non trovate")
      Else
        GetReleaseDestinazione = NTSCStr(dttDest.Rows(0)!rel_maior) & "." & Format(NTSCInt(dttDest.Rows(0)!rel_minor), "00") & NTSCStr(dttDest.Rows(0)!rel_pers)
      End If
      If dttOrig.Rows.Count = 0 Then
        GetReleaseOrigine = oApp.Tr(Me, 128740869960625037, "informazioni sulla versione non trovate")
      Else
        GetReleaseOrigine = NTSCStr(dttOrig.Rows(0)!rel_maior) & "." & Format(NTSCInt(dttOrig.Rows(0)!rel_minor), "00") & NTSCStr(dttOrig.Rows(0)!rel_pers)
      End If

      If GetReleaseOrigine <> GetReleaseDestinazione Then
        If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128740869960625007, "Attenzione! Conflitto di versione database:" & vbCrLf & vbCrLf & _
                                 "Database origine: |" & GetReleaseOrigine & "|" & vbCrLf & _
                                 "Database destinazione: |" & GetReleaseDestinazione & "|" & vbCrLf & vbCrLf & _
                                 "Procedere ugualmente?")) = Windows.Forms.DialogResult.No Then Return False
      End If

      'Cotrollo valuta (se selezionato alcuni dati)
      If opImportaAlcuniDati.Checked Then
        Dim strValOri As String = oCleDupa.GetValutaContoOrig
        Dim strValDes As String = oCleDupa.GetValutaContoDest
        If strValOri <> strValDes Then
          If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128346017755711396, "Attenzione! Conflitto di valuta di conto aziende:" & vbCrLf & vbCrLf & "Azienda origine: |" & strValOri & "|" & vbCrLf & "Azienda destinazione: |" & strValDes & "|" & vbCrLf & vbCrLf & "Procedere ugualmente?")) = Windows.Forms.DialogResult.No Then
            Return False
          End If
        End If
      End If

      If opImportaTuttiDati.Checked Then
        'importa tutti i dati
        pbProgressBar.Minimum = 0
        pbProgressBar.Maximum = dsOrderTbl.Tables("ORDERTBL").Rows.Count
        pbProgressBar.Step = 1

        '---------------------------------------------------------------------------------------
        '--- Cancellazione di tutti i dati delle tabelle in ordine inverso
        '---------------------------------------------------------------------------------------
        pbProgressBar.Value = 0
        lbOperazione.Text = oApp.Tr(Me, 128715785989687500, "Cancellazione dei dati delle tabelle nel database di destinazione in corso...")
        For i As Integer = dsOrderTbl.Tables("ORDERTBL").Rows.Count - 1 To 0 Step -1
          pbProgressBar.PerformStep()
          Me.Refresh()
          '-------------------------------------------------------------------------------------
          '--- Passa alla cancellazione della tabella in esame
          '-------------------------------------------------------------------------------------
          oCleDupa.ImportaDeleteTabella(NTSCStr(dsOrderTbl.Tables("ORDERTBL").Rows(i)!ot_nometab))
        Next
        '---------------------------------------------------------------------------------------

        '---------------------------------------------------------------------------------------
        '--- Inserimento di tutti i dati delle tabelle 
        '---------------------------------------------------------------------------------------
        pbProgressBar.Value = 0
        lbOperazione.Text = oApp.Tr(Me, 128715785989687502, "Importazione dati nelle tabelle nel database di destinazione in corso...")
        For i As Integer = 0 To dsOrderTbl.Tables("ORDERTBL").Rows.Count - 1
          pbProgressBar.PerformStep()
          Me.Refresh()
          '-----------------------------------------------------------------------------------
          '--- Inserimento dati tabella
          '-----------------------------------------------------------------------------------
          If Not oCleDupa.ImportaDaAltroDb(NTSCStr(dsOrderTbl.Tables("ORDERTBL").Rows(i)!ot_nometab)) Then
            If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128357186709601517, "Attenzione! Nel trasferimento dati della tabella |" & NTSCStr(dsOrderTbl.Tables("ORDERTBL").Rows(i)!ot_nometab) & "| il trasferimento non è riuscito o non è riuscita completamente per ragioni, probabilmente,  di integrità referenziale. Proseguo ugualmente?")) = Windows.Forms.DialogResult.No Then Return False
            bImportError = True
          End If
        Next
        '---------------------------------------------------------------------------------------
      Else
        'alcuni dati
        Dim dttTabelleSelect As New DataTable
        dttTabelleSelect = liTabelle.DataSource.Clone
        For i As Integer = 0 To liTabelle.SelectedIndices.Count - 1
          dttTabelleSelect.ImportRow(liTabelle.DataSource.Rows(liTabelle.SelectedIndices(i)))
        Next

        pbProgressBar.Minimum = 0
        pbProgressBar.Maximum = dttTabelleSelect.Rows.Count
        pbProgressBar.Step = 1

        pbProgressBar.Value = 0
        lbOperazione.Text = oApp.Tr(Me, 128715785989687501, "Importazione dati nelle tabelle nel database di destinazione in corso...")
        For i As Integer = 0 To dttTabelleSelect.Rows.Count - 1
          pbProgressBar.PerformStep()
          Me.Refresh()
          If Not oCleDupa.ElaboraTabella(NTSCStr(dttTabelleSelect.Rows(i)!cod), opSostituisci.Checked, opSovrascrivi.Checked) Then
            If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128357186709161517, "Attenzione! Nel trasferimento dati della tabella |" & NTSCStr(dttTabelleSelect.Rows(0)!cod) & "| il trasferimento non è riuscito o non è riuscita completamente per ragioni, probabilmente,  di integrità referenziale. Proseguo ugualmente?")) = Windows.Forms.DialogResult.No Then Return False
            bImportError = True
          End If
        Next i
      End If

      If bImportError Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128357186709601518, "Attenzione verificare la coerenza dei dati."))
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub cmdSelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelAll.Click
    Dim i As Integer
    Try
      cmdDesAll_Click(sender, e)
      For i = 0 To (liTabelle.ItemCount - 1)
        liTabelle.SetSelected(i, True)
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdDesAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDesAll.Click
    Dim i As Integer
    Try
      For i = 0 To (liTabelle.ItemCount - 1)
        liTabelle.SetSelected(i, False)
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

#End Region

  Public Overridable Sub cbDatabaseOrigine_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDatabaseOrigine.SelectedIndexChanged
    Try
      lbDatabaseOrigineDescr.Text = oApp.Tr(Me, 130421060358385652, _
                             "Server\Istanza: |" & _
                             oCleDupa.EstraiParametroDaConnectionString(NTSCStr(oCleDupa.dsShared.Tables(strTabella).Select("AzCodaz = '" & cbDatabaseOrigine.SelectedValue & "'")(0)!AzConnect), "SERVER") & _
                             "|" & vbCrLf & _
                             "Autenticazione: |" & _
                             NTSCStr(IIf(InStr(NTSCStr(oCleDupa.dsShared.Tables(strTabella).Select("AzCodaz = '" & cbDatabaseOrigine.SelectedValue & "'")(0)!AzConnect), "Trusted_Connection") > 0, _
                             strMess1, strMess2)) & _
                             "|" & vbCrLf & _
                             "Ragione sociale: |" & _
                             NTSCStr(oCleDupa.dsShared.Tables(strTabella).Select("AzCodaz = '" & cbDatabaseOrigine.SelectedValue & "'")(0)!AzDescr) & _
                             "|" & vbCrLf & _
                             "Database azienda in uso da questa istanza" & vbCrLf & "di Business NET: |" & _
                             NTSCStr(IIf(NTSCStr(oCleDupa.dsShared.Tables(strTabella).Select("AzCodaz = '" & cbDatabaseOrigine.SelectedValue & "'")(0)!AzCodaz) = UCase(oApp.Db.Nome), _
                             strMess3, strMess4)) & "|")
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cbDatabaseDestinazione_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDatabaseDestinazione.SelectedIndexChanged
    Try
      lbDatabaseDestinazioneDescr.Text = oApp.Tr(Me, 130421064363866343, _
                                  "Server\Istanza: |" & _
                                  oCleDupa.EstraiParametroDaConnectionString(NTSCStr(oCleDupa.dsShared.Tables(strTabella).Select("AzCodaz = '" & cbDatabaseDestinazione.SelectedValue & "'")(0)!AzConnect), "SERVER") & _
                                  "|" & vbCrLf & _
                                  "Autenticazione: |" & _
                                  NTSCStr(IIf(InStr(NTSCStr(oCleDupa.dsShared.Tables(strTabella).Select("AzCodaz = '" & cbDatabaseDestinazione.SelectedValue & "'")(0)!AzConnect), "Trusted_Connection") > 0, _
                                  strMess1, strMess2)) & _
                                  "|" & vbCrLf & _
                                  "Ragione sociale: |" & _
                                  NTSCStr(oCleDupa.dsShared.Tables(strTabella).Select("AzCodaz = '" & cbDatabaseDestinazione.SelectedValue & "'")(0)!AzDescr) & _
                                  "|" & vbCrLf & _
                                  "Database azienda in uso da questa istanza" & vbCrLf & "di Business NET: |" & _
                                  NTSCStr(IIf(NTSCStr(oCleDupa.dsShared.Tables(strTabella).Select("AzCodaz = '" & cbDatabaseDestinazione.SelectedValue & "'")(0)!AzCodaz) = UCase(oApp.Db.Nome), _
                                  strMess3, strMess4)) & "|")
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub opImportaAlcuniDati_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opImportaAlcuniDati.CheckedChanged
    Try
      If opImportaAlcuniDati.Checked Then
        GctlSetVisEnab(pnAlcuniDati, True)
      Else
        pnAlcuniDati.Visible = False
        cmdDesAll_Click(Nothing, Nothing)
      End If
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

#Region "Eventi Toolbar"
  Public Overridable Sub tlbElabora_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImporta.ItemClick
    Dim strLbOperazione As String = lbOperazione.Text
    Try
      If ImportaDati() Then oApp.MsgBoxInfo(oApp.Tr(Me, 128345812743546837, "Operazione terminata."))
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      lbOperazione.Text = strLbOperazione
      pbProgressBar.Value = 0
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
  End Sub
  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub
#End Region

End Class