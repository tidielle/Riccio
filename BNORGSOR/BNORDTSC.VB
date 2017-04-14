Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORDTSC
  Public oCallParams As CLE__CLDP
  Public ocleDocu As CLEMGDOCU

#Region "Inizializzazione"
  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, _
    Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    '----------------------------------------------------------------------------------------------------------------
    oMenu = Menu
    oApp = oMenu.App
    oCallParams = Param
    If Ditta <> "" Then DittaCorrente = Ditta Else DittaCorrente = oApp.Ditta
    '----------------------------------------------------------------------------------------------------------------
    InitializeComponent()
    Me.MinimumSize = Me.Size
    '----------------------------------------------------------------------------------------------------------------
    Return True
  End Function

  Private Sub InitializeComponent()
    Me.pnSpese = New NTSInformatica.NTSPanel
    Me.cbEt_tippaga_8 = New NTSInformatica.NTSComboBox
    Me.cbEt_tippaga_7 = New NTSInformatica.NTSComboBox
    Me.cbEt_tippaga_6 = New NTSInformatica.NTSComboBox
    Me.cbEt_tippaga_5 = New NTSInformatica.NTSComboBox
    Me.cbEt_tippaga_4 = New NTSInformatica.NTSComboBox
    Me.cbEt_tippaga_3 = New NTSInformatica.NTSComboBox
    Me.cbEt_tippaga_2 = New NTSInformatica.NTSComboBox
    Me.cbEt_tippaga_1 = New NTSInformatica.NTSComboBox
    Me.edEt_datsca_8 = New NTSInformatica.NTSTextBoxData
    Me.edEt_datsca_7 = New NTSInformatica.NTSTextBoxData
    Me.edEt_datsca_6 = New NTSInformatica.NTSTextBoxData
    Me.edEt_datsca_5 = New NTSInformatica.NTSTextBoxData
    Me.edEt_datsca_4 = New NTSInformatica.NTSTextBoxData
    Me.edEt_datsca_3 = New NTSInformatica.NTSTextBoxData
    Me.edEt_datsca_2 = New NTSInformatica.NTSTextBoxData
    Me.edEt_datsca_1 = New NTSInformatica.NTSTextBoxData
    Me.edEt_impscav_8 = New NTSInformatica.NTSTextBoxNum
    Me.edEt_impscav_7 = New NTSInformatica.NTSTextBoxNum
    Me.edEt_impscav_6 = New NTSInformatica.NTSTextBoxNum
    Me.edEt_impscav_5 = New NTSInformatica.NTSTextBoxNum
    Me.edEt_impscav_4 = New NTSInformatica.NTSTextBoxNum
    Me.edEt_impscav_3 = New NTSInformatica.NTSTextBoxNum
    Me.edEt_impscav_2 = New NTSInformatica.NTSTextBoxNum
    Me.edEt_impscav_1 = New NTSInformatica.NTSTextBoxNum
    Me.lbImportoValuta = New NTSInformatica.NTSLabel
    Me.lbImporto = New NTSInformatica.NTSLabel
    Me.lbScadenza = New NTSInformatica.NTSLabel
    Me.lbTipo = New NTSInformatica.NTSLabel
    Me.lb8 = New NTSInformatica.NTSLabel
    Me.edEt_impsca_8 = New NTSInformatica.NTSTextBoxNum
    Me.lb7 = New NTSInformatica.NTSLabel
    Me.edEt_impsca_7 = New NTSInformatica.NTSTextBoxNum
    Me.lb6 = New NTSInformatica.NTSLabel
    Me.edEt_impsca_6 = New NTSInformatica.NTSTextBoxNum
    Me.lb5 = New NTSInformatica.NTSLabel
    Me.lb4 = New NTSInformatica.NTSLabel
    Me.lb3 = New NTSInformatica.NTSLabel
    Me.lb2 = New NTSInformatica.NTSLabel
    Me.edEt_impsca_5 = New NTSInformatica.NTSTextBoxNum
    Me.edEt_impsca_4 = New NTSInformatica.NTSTextBoxNum
    Me.edEt_impsca_3 = New NTSInformatica.NTSTextBoxNum
    Me.edEt_impsca_2 = New NTSInformatica.NTSTextBoxNum
    Me.lb1 = New NTSInformatica.NTSLabel
    Me.edEt_impsca_1 = New NTSInformatica.NTSTextBoxNum
    Me.cmdOk = New NTSInformatica.NTSButton
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnSpese, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSpese.SuspendLayout()
    CType(Me.cbEt_tippaga_8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbEt_tippaga_7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbEt_tippaga_6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbEt_tippaga_5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbEt_tippaga_4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbEt_tippaga_3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbEt_tippaga_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbEt_tippaga_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_datsca_8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_datsca_7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_datsca_6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_datsca_5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_datsca_4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_datsca_3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_datsca_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_datsca_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_impscav_8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_impscav_7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_impscav_6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_impscav_5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_impscav_4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_impscav_3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_impscav_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_impscav_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_impsca_8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_impsca_7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_impsca_6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_impsca_5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_impsca_4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_impsca_3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_impsca_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEt_impsca_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'pnSpese
    '
    Me.pnSpese.AllowDrop = True
    Me.pnSpese.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSpese.Appearance.Options.UseBackColor = True
    Me.pnSpese.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSpese.Controls.Add(Me.cbEt_tippaga_8)
    Me.pnSpese.Controls.Add(Me.cbEt_tippaga_7)
    Me.pnSpese.Controls.Add(Me.cbEt_tippaga_6)
    Me.pnSpese.Controls.Add(Me.cbEt_tippaga_5)
    Me.pnSpese.Controls.Add(Me.cbEt_tippaga_4)
    Me.pnSpese.Controls.Add(Me.cbEt_tippaga_3)
    Me.pnSpese.Controls.Add(Me.cbEt_tippaga_2)
    Me.pnSpese.Controls.Add(Me.cbEt_tippaga_1)
    Me.pnSpese.Controls.Add(Me.edEt_datsca_8)
    Me.pnSpese.Controls.Add(Me.edEt_datsca_7)
    Me.pnSpese.Controls.Add(Me.edEt_datsca_6)
    Me.pnSpese.Controls.Add(Me.edEt_datsca_5)
    Me.pnSpese.Controls.Add(Me.edEt_datsca_4)
    Me.pnSpese.Controls.Add(Me.edEt_datsca_3)
    Me.pnSpese.Controls.Add(Me.edEt_datsca_2)
    Me.pnSpese.Controls.Add(Me.edEt_datsca_1)
    Me.pnSpese.Controls.Add(Me.edEt_impscav_8)
    Me.pnSpese.Controls.Add(Me.edEt_impscav_7)
    Me.pnSpese.Controls.Add(Me.edEt_impscav_6)
    Me.pnSpese.Controls.Add(Me.edEt_impscav_5)
    Me.pnSpese.Controls.Add(Me.edEt_impscav_4)
    Me.pnSpese.Controls.Add(Me.edEt_impscav_3)
    Me.pnSpese.Controls.Add(Me.edEt_impscav_2)
    Me.pnSpese.Controls.Add(Me.edEt_impscav_1)
    Me.pnSpese.Controls.Add(Me.lbImportoValuta)
    Me.pnSpese.Controls.Add(Me.lbImporto)
    Me.pnSpese.Controls.Add(Me.lbScadenza)
    Me.pnSpese.Controls.Add(Me.lbTipo)
    Me.pnSpese.Controls.Add(Me.lb8)
    Me.pnSpese.Controls.Add(Me.edEt_impsca_8)
    Me.pnSpese.Controls.Add(Me.lb7)
    Me.pnSpese.Controls.Add(Me.edEt_impsca_7)
    Me.pnSpese.Controls.Add(Me.lb6)
    Me.pnSpese.Controls.Add(Me.edEt_impsca_6)
    Me.pnSpese.Controls.Add(Me.lb5)
    Me.pnSpese.Controls.Add(Me.lb4)
    Me.pnSpese.Controls.Add(Me.lb3)
    Me.pnSpese.Controls.Add(Me.lb2)
    Me.pnSpese.Controls.Add(Me.edEt_impsca_5)
    Me.pnSpese.Controls.Add(Me.edEt_impsca_4)
    Me.pnSpese.Controls.Add(Me.edEt_impsca_3)
    Me.pnSpese.Controls.Add(Me.edEt_impsca_2)
    Me.pnSpese.Controls.Add(Me.lb1)
    Me.pnSpese.Controls.Add(Me.edEt_impsca_1)
    Me.pnSpese.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnSpese.Location = New System.Drawing.Point(2, 8)
    Me.pnSpese.Name = "pnSpese"
    Me.pnSpese.NTSActiveTrasparency = True
    Me.pnSpese.Size = New System.Drawing.Size(465, 233)
    Me.pnSpese.TabIndex = 10
    Me.pnSpese.Text = "NtsPanel1"
    '
    'cbEt_tippaga_8
    '
    Me.cbEt_tippaga_8.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbEt_tippaga_8.DataSource = Nothing
    Me.cbEt_tippaga_8.DisplayMember = ""
    Me.cbEt_tippaga_8.Enabled = False
    Me.cbEt_tippaga_8.Location = New System.Drawing.Point(26, 206)
    Me.cbEt_tippaga_8.Name = "cbEt_tippaga_8"
    Me.cbEt_tippaga_8.NTSDbField = ""
    Me.cbEt_tippaga_8.Properties.AutoHeight = False
    Me.cbEt_tippaga_8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbEt_tippaga_8.Properties.DropDownRows = 30
    Me.cbEt_tippaga_8.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbEt_tippaga_8.SelectedValue = ""
    Me.cbEt_tippaga_8.Size = New System.Drawing.Size(117, 20)
    Me.cbEt_tippaga_8.TabIndex = 50
    Me.cbEt_tippaga_8.ValueMember = ""
    '
    'cbEt_tippaga_7
    '
    Me.cbEt_tippaga_7.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbEt_tippaga_7.DataSource = Nothing
    Me.cbEt_tippaga_7.DisplayMember = ""
    Me.cbEt_tippaga_7.Enabled = False
    Me.cbEt_tippaga_7.Location = New System.Drawing.Point(26, 180)
    Me.cbEt_tippaga_7.Name = "cbEt_tippaga_7"
    Me.cbEt_tippaga_7.NTSDbField = ""
    Me.cbEt_tippaga_7.Properties.AutoHeight = False
    Me.cbEt_tippaga_7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbEt_tippaga_7.Properties.DropDownRows = 30
    Me.cbEt_tippaga_7.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbEt_tippaga_7.SelectedValue = ""
    Me.cbEt_tippaga_7.Size = New System.Drawing.Size(117, 20)
    Me.cbEt_tippaga_7.TabIndex = 49
    Me.cbEt_tippaga_7.ValueMember = ""
    '
    'cbEt_tippaga_6
    '
    Me.cbEt_tippaga_6.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbEt_tippaga_6.DataSource = Nothing
    Me.cbEt_tippaga_6.DisplayMember = ""
    Me.cbEt_tippaga_6.Enabled = False
    Me.cbEt_tippaga_6.Location = New System.Drawing.Point(26, 154)
    Me.cbEt_tippaga_6.Name = "cbEt_tippaga_6"
    Me.cbEt_tippaga_6.NTSDbField = ""
    Me.cbEt_tippaga_6.Properties.AutoHeight = False
    Me.cbEt_tippaga_6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbEt_tippaga_6.Properties.DropDownRows = 30
    Me.cbEt_tippaga_6.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbEt_tippaga_6.SelectedValue = ""
    Me.cbEt_tippaga_6.Size = New System.Drawing.Size(117, 20)
    Me.cbEt_tippaga_6.TabIndex = 48
    Me.cbEt_tippaga_6.ValueMember = ""
    '
    'cbEt_tippaga_5
    '
    Me.cbEt_tippaga_5.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbEt_tippaga_5.DataSource = Nothing
    Me.cbEt_tippaga_5.DisplayMember = ""
    Me.cbEt_tippaga_5.Enabled = False
    Me.cbEt_tippaga_5.Location = New System.Drawing.Point(26, 128)
    Me.cbEt_tippaga_5.Name = "cbEt_tippaga_5"
    Me.cbEt_tippaga_5.NTSDbField = ""
    Me.cbEt_tippaga_5.Properties.AutoHeight = False
    Me.cbEt_tippaga_5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbEt_tippaga_5.Properties.DropDownRows = 30
    Me.cbEt_tippaga_5.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbEt_tippaga_5.SelectedValue = ""
    Me.cbEt_tippaga_5.Size = New System.Drawing.Size(117, 20)
    Me.cbEt_tippaga_5.TabIndex = 47
    Me.cbEt_tippaga_5.ValueMember = ""
    '
    'cbEt_tippaga_4
    '
    Me.cbEt_tippaga_4.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbEt_tippaga_4.DataSource = Nothing
    Me.cbEt_tippaga_4.DisplayMember = ""
    Me.cbEt_tippaga_4.Enabled = False
    Me.cbEt_tippaga_4.Location = New System.Drawing.Point(26, 102)
    Me.cbEt_tippaga_4.Name = "cbEt_tippaga_4"
    Me.cbEt_tippaga_4.NTSDbField = ""
    Me.cbEt_tippaga_4.Properties.AutoHeight = False
    Me.cbEt_tippaga_4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbEt_tippaga_4.Properties.DropDownRows = 30
    Me.cbEt_tippaga_4.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbEt_tippaga_4.SelectedValue = ""
    Me.cbEt_tippaga_4.Size = New System.Drawing.Size(117, 20)
    Me.cbEt_tippaga_4.TabIndex = 46
    Me.cbEt_tippaga_4.ValueMember = ""
    '
    'cbEt_tippaga_3
    '
    Me.cbEt_tippaga_3.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbEt_tippaga_3.DataSource = Nothing
    Me.cbEt_tippaga_3.DisplayMember = ""
    Me.cbEt_tippaga_3.Enabled = False
    Me.cbEt_tippaga_3.Location = New System.Drawing.Point(26, 76)
    Me.cbEt_tippaga_3.Name = "cbEt_tippaga_3"
    Me.cbEt_tippaga_3.NTSDbField = ""
    Me.cbEt_tippaga_3.Properties.AutoHeight = False
    Me.cbEt_tippaga_3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbEt_tippaga_3.Properties.DropDownRows = 30
    Me.cbEt_tippaga_3.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbEt_tippaga_3.SelectedValue = ""
    Me.cbEt_tippaga_3.Size = New System.Drawing.Size(117, 20)
    Me.cbEt_tippaga_3.TabIndex = 45
    Me.cbEt_tippaga_3.ValueMember = ""
    '
    'cbEt_tippaga_2
    '
    Me.cbEt_tippaga_2.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbEt_tippaga_2.DataSource = Nothing
    Me.cbEt_tippaga_2.DisplayMember = ""
    Me.cbEt_tippaga_2.Enabled = False
    Me.cbEt_tippaga_2.Location = New System.Drawing.Point(26, 50)
    Me.cbEt_tippaga_2.Name = "cbEt_tippaga_2"
    Me.cbEt_tippaga_2.NTSDbField = ""
    Me.cbEt_tippaga_2.Properties.AutoHeight = False
    Me.cbEt_tippaga_2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbEt_tippaga_2.Properties.DropDownRows = 30
    Me.cbEt_tippaga_2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbEt_tippaga_2.SelectedValue = ""
    Me.cbEt_tippaga_2.Size = New System.Drawing.Size(117, 20)
    Me.cbEt_tippaga_2.TabIndex = 44
    Me.cbEt_tippaga_2.ValueMember = ""
    '
    'cbEt_tippaga_1
    '
    Me.cbEt_tippaga_1.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbEt_tippaga_1.DataSource = Nothing
    Me.cbEt_tippaga_1.DisplayMember = ""
    Me.cbEt_tippaga_1.Enabled = False
    Me.cbEt_tippaga_1.Location = New System.Drawing.Point(26, 24)
    Me.cbEt_tippaga_1.Name = "cbEt_tippaga_1"
    Me.cbEt_tippaga_1.NTSDbField = ""
    Me.cbEt_tippaga_1.Properties.AutoHeight = False
    Me.cbEt_tippaga_1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbEt_tippaga_1.Properties.DropDownRows = 30
    Me.cbEt_tippaga_1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbEt_tippaga_1.SelectedValue = ""
    Me.cbEt_tippaga_1.Size = New System.Drawing.Size(117, 20)
    Me.cbEt_tippaga_1.TabIndex = 43
    Me.cbEt_tippaga_1.ValueMember = ""
    '
    'edEt_datsca_8
    '
    Me.edEt_datsca_8.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_datsca_8.Enabled = False
    Me.edEt_datsca_8.Location = New System.Drawing.Point(149, 206)
    Me.edEt_datsca_8.Name = "edEt_datsca_8"
    Me.edEt_datsca_8.NTSDbField = ""
    Me.edEt_datsca_8.NTSForzaVisZoom = False
    Me.edEt_datsca_8.NTSOldValue = ""
    Me.edEt_datsca_8.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_datsca_8.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_datsca_8.Properties.AutoHeight = False
    Me.edEt_datsca_8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_datsca_8.Properties.MaxLength = 65536
    Me.edEt_datsca_8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_datsca_8.Size = New System.Drawing.Size(100, 20)
    Me.edEt_datsca_8.TabIndex = 42
    '
    'edEt_datsca_7
    '
    Me.edEt_datsca_7.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_datsca_7.Enabled = False
    Me.edEt_datsca_7.Location = New System.Drawing.Point(149, 180)
    Me.edEt_datsca_7.Name = "edEt_datsca_7"
    Me.edEt_datsca_7.NTSDbField = ""
    Me.edEt_datsca_7.NTSForzaVisZoom = False
    Me.edEt_datsca_7.NTSOldValue = ""
    Me.edEt_datsca_7.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_datsca_7.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_datsca_7.Properties.AutoHeight = False
    Me.edEt_datsca_7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_datsca_7.Properties.MaxLength = 65536
    Me.edEt_datsca_7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_datsca_7.Size = New System.Drawing.Size(100, 20)
    Me.edEt_datsca_7.TabIndex = 41
    '
    'edEt_datsca_6
    '
    Me.edEt_datsca_6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_datsca_6.Enabled = False
    Me.edEt_datsca_6.Location = New System.Drawing.Point(149, 154)
    Me.edEt_datsca_6.Name = "edEt_datsca_6"
    Me.edEt_datsca_6.NTSDbField = ""
    Me.edEt_datsca_6.NTSForzaVisZoom = False
    Me.edEt_datsca_6.NTSOldValue = ""
    Me.edEt_datsca_6.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_datsca_6.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_datsca_6.Properties.AutoHeight = False
    Me.edEt_datsca_6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_datsca_6.Properties.MaxLength = 65536
    Me.edEt_datsca_6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_datsca_6.Size = New System.Drawing.Size(100, 20)
    Me.edEt_datsca_6.TabIndex = 40
    '
    'edEt_datsca_5
    '
    Me.edEt_datsca_5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_datsca_5.Enabled = False
    Me.edEt_datsca_5.Location = New System.Drawing.Point(149, 128)
    Me.edEt_datsca_5.Name = "edEt_datsca_5"
    Me.edEt_datsca_5.NTSDbField = ""
    Me.edEt_datsca_5.NTSForzaVisZoom = False
    Me.edEt_datsca_5.NTSOldValue = ""
    Me.edEt_datsca_5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_datsca_5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_datsca_5.Properties.AutoHeight = False
    Me.edEt_datsca_5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_datsca_5.Properties.MaxLength = 65536
    Me.edEt_datsca_5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_datsca_5.Size = New System.Drawing.Size(100, 20)
    Me.edEt_datsca_5.TabIndex = 39
    '
    'edEt_datsca_4
    '
    Me.edEt_datsca_4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_datsca_4.Enabled = False
    Me.edEt_datsca_4.Location = New System.Drawing.Point(149, 102)
    Me.edEt_datsca_4.Name = "edEt_datsca_4"
    Me.edEt_datsca_4.NTSDbField = ""
    Me.edEt_datsca_4.NTSForzaVisZoom = False
    Me.edEt_datsca_4.NTSOldValue = ""
    Me.edEt_datsca_4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_datsca_4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_datsca_4.Properties.AutoHeight = False
    Me.edEt_datsca_4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_datsca_4.Properties.MaxLength = 65536
    Me.edEt_datsca_4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_datsca_4.Size = New System.Drawing.Size(100, 20)
    Me.edEt_datsca_4.TabIndex = 38
    '
    'edEt_datsca_3
    '
    Me.edEt_datsca_3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_datsca_3.Enabled = False
    Me.edEt_datsca_3.Location = New System.Drawing.Point(149, 76)
    Me.edEt_datsca_3.Name = "edEt_datsca_3"
    Me.edEt_datsca_3.NTSDbField = ""
    Me.edEt_datsca_3.NTSForzaVisZoom = False
    Me.edEt_datsca_3.NTSOldValue = ""
    Me.edEt_datsca_3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_datsca_3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_datsca_3.Properties.AutoHeight = False
    Me.edEt_datsca_3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_datsca_3.Properties.MaxLength = 65536
    Me.edEt_datsca_3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_datsca_3.Size = New System.Drawing.Size(100, 20)
    Me.edEt_datsca_3.TabIndex = 37
    '
    'edEt_datsca_2
    '
    Me.edEt_datsca_2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_datsca_2.Enabled = False
    Me.edEt_datsca_2.Location = New System.Drawing.Point(149, 50)
    Me.edEt_datsca_2.Name = "edEt_datsca_2"
    Me.edEt_datsca_2.NTSDbField = ""
    Me.edEt_datsca_2.NTSForzaVisZoom = False
    Me.edEt_datsca_2.NTSOldValue = ""
    Me.edEt_datsca_2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_datsca_2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_datsca_2.Properties.AutoHeight = False
    Me.edEt_datsca_2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_datsca_2.Properties.MaxLength = 65536
    Me.edEt_datsca_2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_datsca_2.Size = New System.Drawing.Size(100, 20)
    Me.edEt_datsca_2.TabIndex = 36
    '
    'edEt_datsca_1
    '
    Me.edEt_datsca_1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_datsca_1.Enabled = False
    Me.edEt_datsca_1.Location = New System.Drawing.Point(149, 24)
    Me.edEt_datsca_1.Name = "edEt_datsca_1"
    Me.edEt_datsca_1.NTSDbField = ""
    Me.edEt_datsca_1.NTSForzaVisZoom = False
    Me.edEt_datsca_1.NTSOldValue = ""
    Me.edEt_datsca_1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_datsca_1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_datsca_1.Properties.AutoHeight = False
    Me.edEt_datsca_1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_datsca_1.Properties.MaxLength = 65536
    Me.edEt_datsca_1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_datsca_1.Size = New System.Drawing.Size(100, 20)
    Me.edEt_datsca_1.TabIndex = 35
    '
    'edEt_impscav_8
    '
    Me.edEt_impscav_8.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_impscav_8.EditValue = "0"
    Me.edEt_impscav_8.Enabled = False
    Me.edEt_impscav_8.Location = New System.Drawing.Point(358, 206)
    Me.edEt_impscav_8.Name = "edEt_impscav_8"
    Me.edEt_impscav_8.NTSDbField = ""
    Me.edEt_impscav_8.NTSFormat = "0"
    Me.edEt_impscav_8.NTSForzaVisZoom = False
    Me.edEt_impscav_8.NTSOldValue = ""
    Me.edEt_impscav_8.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edEt_impscav_8.Properties.Appearance.Options.UseBackColor = True
    Me.edEt_impscav_8.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_impscav_8.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_impscav_8.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_impscav_8.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_impscav_8.Properties.AutoHeight = False
    Me.edEt_impscav_8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_impscav_8.Properties.MaxLength = 9
    Me.edEt_impscav_8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_impscav_8.Size = New System.Drawing.Size(97, 20)
    Me.edEt_impscav_8.TabIndex = 34
    '
    'edEt_impscav_7
    '
    Me.edEt_impscav_7.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_impscav_7.EditValue = "0"
    Me.edEt_impscav_7.Enabled = False
    Me.edEt_impscav_7.Location = New System.Drawing.Point(358, 180)
    Me.edEt_impscav_7.Name = "edEt_impscav_7"
    Me.edEt_impscav_7.NTSDbField = ""
    Me.edEt_impscav_7.NTSFormat = "0"
    Me.edEt_impscav_7.NTSForzaVisZoom = False
    Me.edEt_impscav_7.NTSOldValue = ""
    Me.edEt_impscav_7.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edEt_impscav_7.Properties.Appearance.Options.UseBackColor = True
    Me.edEt_impscav_7.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_impscav_7.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_impscav_7.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_impscav_7.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_impscav_7.Properties.AutoHeight = False
    Me.edEt_impscav_7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_impscav_7.Properties.MaxLength = 9
    Me.edEt_impscav_7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_impscav_7.Size = New System.Drawing.Size(97, 20)
    Me.edEt_impscav_7.TabIndex = 33
    '
    'edEt_impscav_6
    '
    Me.edEt_impscav_6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_impscav_6.EditValue = "0"
    Me.edEt_impscav_6.Enabled = False
    Me.edEt_impscav_6.Location = New System.Drawing.Point(358, 154)
    Me.edEt_impscav_6.Name = "edEt_impscav_6"
    Me.edEt_impscav_6.NTSDbField = ""
    Me.edEt_impscav_6.NTSFormat = "0"
    Me.edEt_impscav_6.NTSForzaVisZoom = False
    Me.edEt_impscav_6.NTSOldValue = ""
    Me.edEt_impscav_6.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edEt_impscav_6.Properties.Appearance.Options.UseBackColor = True
    Me.edEt_impscav_6.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_impscav_6.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_impscav_6.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_impscav_6.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_impscav_6.Properties.AutoHeight = False
    Me.edEt_impscav_6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_impscav_6.Properties.MaxLength = 9
    Me.edEt_impscav_6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_impscav_6.Size = New System.Drawing.Size(97, 20)
    Me.edEt_impscav_6.TabIndex = 32
    '
    'edEt_impscav_5
    '
    Me.edEt_impscav_5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_impscav_5.EditValue = "0"
    Me.edEt_impscav_5.Enabled = False
    Me.edEt_impscav_5.Location = New System.Drawing.Point(358, 128)
    Me.edEt_impscav_5.Name = "edEt_impscav_5"
    Me.edEt_impscav_5.NTSDbField = ""
    Me.edEt_impscav_5.NTSFormat = "0"
    Me.edEt_impscav_5.NTSForzaVisZoom = False
    Me.edEt_impscav_5.NTSOldValue = ""
    Me.edEt_impscav_5.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edEt_impscav_5.Properties.Appearance.Options.UseBackColor = True
    Me.edEt_impscav_5.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_impscav_5.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_impscav_5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_impscav_5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_impscav_5.Properties.AutoHeight = False
    Me.edEt_impscav_5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_impscav_5.Properties.MaxLength = 9
    Me.edEt_impscav_5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_impscav_5.Size = New System.Drawing.Size(97, 20)
    Me.edEt_impscav_5.TabIndex = 31
    '
    'edEt_impscav_4
    '
    Me.edEt_impscav_4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_impscav_4.EditValue = "0"
    Me.edEt_impscav_4.Enabled = False
    Me.edEt_impscav_4.Location = New System.Drawing.Point(358, 102)
    Me.edEt_impscav_4.Name = "edEt_impscav_4"
    Me.edEt_impscav_4.NTSDbField = ""
    Me.edEt_impscav_4.NTSFormat = "0"
    Me.edEt_impscav_4.NTSForzaVisZoom = False
    Me.edEt_impscav_4.NTSOldValue = ""
    Me.edEt_impscav_4.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edEt_impscav_4.Properties.Appearance.Options.UseBackColor = True
    Me.edEt_impscav_4.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_impscav_4.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_impscav_4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_impscav_4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_impscav_4.Properties.AutoHeight = False
    Me.edEt_impscav_4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_impscav_4.Properties.MaxLength = 9
    Me.edEt_impscav_4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_impscav_4.Size = New System.Drawing.Size(97, 20)
    Me.edEt_impscav_4.TabIndex = 30
    '
    'edEt_impscav_3
    '
    Me.edEt_impscav_3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_impscav_3.EditValue = "0"
    Me.edEt_impscav_3.Enabled = False
    Me.edEt_impscav_3.Location = New System.Drawing.Point(358, 76)
    Me.edEt_impscav_3.Name = "edEt_impscav_3"
    Me.edEt_impscav_3.NTSDbField = ""
    Me.edEt_impscav_3.NTSFormat = "0"
    Me.edEt_impscav_3.NTSForzaVisZoom = False
    Me.edEt_impscav_3.NTSOldValue = ""
    Me.edEt_impscav_3.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edEt_impscav_3.Properties.Appearance.Options.UseBackColor = True
    Me.edEt_impscav_3.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_impscav_3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_impscav_3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_impscav_3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_impscav_3.Properties.AutoHeight = False
    Me.edEt_impscav_3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_impscav_3.Properties.MaxLength = 9
    Me.edEt_impscav_3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_impscav_3.Size = New System.Drawing.Size(97, 20)
    Me.edEt_impscav_3.TabIndex = 29
    '
    'edEt_impscav_2
    '
    Me.edEt_impscav_2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_impscav_2.EditValue = "0"
    Me.edEt_impscav_2.Enabled = False
    Me.edEt_impscav_2.Location = New System.Drawing.Point(358, 50)
    Me.edEt_impscav_2.Name = "edEt_impscav_2"
    Me.edEt_impscav_2.NTSDbField = ""
    Me.edEt_impscav_2.NTSFormat = "0"
    Me.edEt_impscav_2.NTSForzaVisZoom = False
    Me.edEt_impscav_2.NTSOldValue = ""
    Me.edEt_impscav_2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edEt_impscav_2.Properties.Appearance.Options.UseBackColor = True
    Me.edEt_impscav_2.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_impscav_2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_impscav_2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_impscav_2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_impscav_2.Properties.AutoHeight = False
    Me.edEt_impscav_2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_impscav_2.Properties.MaxLength = 9
    Me.edEt_impscav_2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_impscav_2.Size = New System.Drawing.Size(97, 20)
    Me.edEt_impscav_2.TabIndex = 28
    '
    'edEt_impscav_1
    '
    Me.edEt_impscav_1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_impscav_1.EditValue = "0"
    Me.edEt_impscav_1.Enabled = False
    Me.edEt_impscav_1.Location = New System.Drawing.Point(358, 24)
    Me.edEt_impscav_1.Name = "edEt_impscav_1"
    Me.edEt_impscav_1.NTSDbField = ""
    Me.edEt_impscav_1.NTSFormat = "0"
    Me.edEt_impscav_1.NTSForzaVisZoom = False
    Me.edEt_impscav_1.NTSOldValue = ""
    Me.edEt_impscav_1.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edEt_impscav_1.Properties.Appearance.Options.UseBackColor = True
    Me.edEt_impscav_1.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_impscav_1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_impscav_1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_impscav_1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_impscav_1.Properties.AutoHeight = False
    Me.edEt_impscav_1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_impscav_1.Properties.MaxLength = 9
    Me.edEt_impscav_1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_impscav_1.Size = New System.Drawing.Size(97, 20)
    Me.edEt_impscav_1.TabIndex = 27
    '
    'lbImportoValuta
    '
    Me.lbImportoValuta.AutoSize = True
    Me.lbImportoValuta.BackColor = System.Drawing.Color.Transparent
    Me.lbImportoValuta.Location = New System.Drawing.Point(366, 8)
    Me.lbImportoValuta.Name = "lbImportoValuta"
    Me.lbImportoValuta.NTSDbField = ""
    Me.lbImportoValuta.Size = New System.Drawing.Size(78, 13)
    Me.lbImportoValuta.TabIndex = 26
    Me.lbImportoValuta.Text = "Importo valuta"
    Me.lbImportoValuta.Tooltip = ""
    Me.lbImportoValuta.UseMnemonic = False
    '
    'lbImporto
    '
    Me.lbImporto.AutoSize = True
    Me.lbImporto.BackColor = System.Drawing.Color.Transparent
    Me.lbImporto.Location = New System.Drawing.Point(276, 8)
    Me.lbImporto.Name = "lbImporto"
    Me.lbImporto.NTSDbField = ""
    Me.lbImporto.Size = New System.Drawing.Size(45, 13)
    Me.lbImporto.TabIndex = 25
    Me.lbImporto.Text = "Importo"
    Me.lbImporto.Tooltip = ""
    Me.lbImporto.UseMnemonic = False
    '
    'lbScadenza
    '
    Me.lbScadenza.AutoSize = True
    Me.lbScadenza.BackColor = System.Drawing.Color.Transparent
    Me.lbScadenza.Location = New System.Drawing.Point(165, 8)
    Me.lbScadenza.Name = "lbScadenza"
    Me.lbScadenza.NTSDbField = ""
    Me.lbScadenza.Size = New System.Drawing.Size(53, 13)
    Me.lbScadenza.TabIndex = 24
    Me.lbScadenza.Text = "Scadenza"
    Me.lbScadenza.Tooltip = ""
    Me.lbScadenza.UseMnemonic = False
    '
    'lbTipo
    '
    Me.lbTipo.AutoSize = True
    Me.lbTipo.BackColor = System.Drawing.Color.Transparent
    Me.lbTipo.Location = New System.Drawing.Point(65, 8)
    Me.lbTipo.Name = "lbTipo"
    Me.lbTipo.NTSDbField = ""
    Me.lbTipo.Size = New System.Drawing.Size(27, 13)
    Me.lbTipo.TabIndex = 23
    Me.lbTipo.Text = "Tipo"
    Me.lbTipo.Tooltip = ""
    Me.lbTipo.UseMnemonic = False
    '
    'lb8
    '
    Me.lb8.AutoSize = True
    Me.lb8.BackColor = System.Drawing.Color.Transparent
    Me.lb8.Location = New System.Drawing.Point(3, 209)
    Me.lb8.Name = "lb8"
    Me.lb8.NTSDbField = ""
    Me.lb8.Size = New System.Drawing.Size(17, 13)
    Me.lb8.TabIndex = 22
    Me.lb8.Text = "8:"
    Me.lb8.Tooltip = ""
    Me.lb8.UseMnemonic = False
    '
    'edEt_impsca_8
    '
    Me.edEt_impsca_8.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_impsca_8.EditValue = "0"
    Me.edEt_impsca_8.Enabled = False
    Me.edEt_impsca_8.Location = New System.Drawing.Point(255, 206)
    Me.edEt_impsca_8.Name = "edEt_impsca_8"
    Me.edEt_impsca_8.NTSDbField = ""
    Me.edEt_impsca_8.NTSFormat = "0"
    Me.edEt_impsca_8.NTSForzaVisZoom = False
    Me.edEt_impsca_8.NTSOldValue = ""
    Me.edEt_impsca_8.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edEt_impsca_8.Properties.Appearance.Options.UseBackColor = True
    Me.edEt_impsca_8.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_impsca_8.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_impsca_8.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_impsca_8.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_impsca_8.Properties.AutoHeight = False
    Me.edEt_impsca_8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_impsca_8.Properties.MaxLength = 9
    Me.edEt_impsca_8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_impsca_8.Size = New System.Drawing.Size(97, 20)
    Me.edEt_impsca_8.TabIndex = 21
    '
    'lb7
    '
    Me.lb7.AutoSize = True
    Me.lb7.BackColor = System.Drawing.Color.Transparent
    Me.lb7.Location = New System.Drawing.Point(3, 183)
    Me.lb7.Name = "lb7"
    Me.lb7.NTSDbField = ""
    Me.lb7.Size = New System.Drawing.Size(17, 13)
    Me.lb7.TabIndex = 20
    Me.lb7.Text = "7:"
    Me.lb7.Tooltip = ""
    Me.lb7.UseMnemonic = False
    '
    'edEt_impsca_7
    '
    Me.edEt_impsca_7.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_impsca_7.EditValue = "0"
    Me.edEt_impsca_7.Enabled = False
    Me.edEt_impsca_7.Location = New System.Drawing.Point(255, 180)
    Me.edEt_impsca_7.Name = "edEt_impsca_7"
    Me.edEt_impsca_7.NTSDbField = ""
    Me.edEt_impsca_7.NTSFormat = "0"
    Me.edEt_impsca_7.NTSForzaVisZoom = False
    Me.edEt_impsca_7.NTSOldValue = ""
    Me.edEt_impsca_7.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edEt_impsca_7.Properties.Appearance.Options.UseBackColor = True
    Me.edEt_impsca_7.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_impsca_7.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_impsca_7.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_impsca_7.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_impsca_7.Properties.AutoHeight = False
    Me.edEt_impsca_7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_impsca_7.Properties.MaxLength = 9
    Me.edEt_impsca_7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_impsca_7.Size = New System.Drawing.Size(97, 20)
    Me.edEt_impsca_7.TabIndex = 19
    '
    'lb6
    '
    Me.lb6.AutoSize = True
    Me.lb6.BackColor = System.Drawing.Color.Transparent
    Me.lb6.Location = New System.Drawing.Point(3, 157)
    Me.lb6.Name = "lb6"
    Me.lb6.NTSDbField = ""
    Me.lb6.Size = New System.Drawing.Size(17, 13)
    Me.lb6.TabIndex = 18
    Me.lb6.Text = "6:"
    Me.lb6.Tooltip = ""
    Me.lb6.UseMnemonic = False
    '
    'edEt_impsca_6
    '
    Me.edEt_impsca_6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_impsca_6.EditValue = "0"
    Me.edEt_impsca_6.Enabled = False
    Me.edEt_impsca_6.Location = New System.Drawing.Point(255, 154)
    Me.edEt_impsca_6.Name = "edEt_impsca_6"
    Me.edEt_impsca_6.NTSDbField = ""
    Me.edEt_impsca_6.NTSFormat = "0"
    Me.edEt_impsca_6.NTSForzaVisZoom = False
    Me.edEt_impsca_6.NTSOldValue = ""
    Me.edEt_impsca_6.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edEt_impsca_6.Properties.Appearance.Options.UseBackColor = True
    Me.edEt_impsca_6.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_impsca_6.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_impsca_6.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_impsca_6.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_impsca_6.Properties.AutoHeight = False
    Me.edEt_impsca_6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_impsca_6.Properties.MaxLength = 9
    Me.edEt_impsca_6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_impsca_6.Size = New System.Drawing.Size(97, 20)
    Me.edEt_impsca_6.TabIndex = 17
    '
    'lb5
    '
    Me.lb5.AutoSize = True
    Me.lb5.BackColor = System.Drawing.Color.Transparent
    Me.lb5.Location = New System.Drawing.Point(3, 131)
    Me.lb5.Name = "lb5"
    Me.lb5.NTSDbField = ""
    Me.lb5.Size = New System.Drawing.Size(17, 13)
    Me.lb5.TabIndex = 16
    Me.lb5.Text = "5:"
    Me.lb5.Tooltip = ""
    Me.lb5.UseMnemonic = False
    '
    'lb4
    '
    Me.lb4.AutoSize = True
    Me.lb4.BackColor = System.Drawing.Color.Transparent
    Me.lb4.Location = New System.Drawing.Point(3, 105)
    Me.lb4.Name = "lb4"
    Me.lb4.NTSDbField = ""
    Me.lb4.Size = New System.Drawing.Size(17, 13)
    Me.lb4.TabIndex = 15
    Me.lb4.Text = "4:"
    Me.lb4.Tooltip = ""
    Me.lb4.UseMnemonic = False
    '
    'lb3
    '
    Me.lb3.AutoSize = True
    Me.lb3.BackColor = System.Drawing.Color.Transparent
    Me.lb3.Location = New System.Drawing.Point(3, 79)
    Me.lb3.Name = "lb3"
    Me.lb3.NTSDbField = ""
    Me.lb3.Size = New System.Drawing.Size(17, 13)
    Me.lb3.TabIndex = 14
    Me.lb3.Text = "3:"
    Me.lb3.Tooltip = ""
    Me.lb3.UseMnemonic = False
    '
    'lb2
    '
    Me.lb2.AutoSize = True
    Me.lb2.BackColor = System.Drawing.Color.Transparent
    Me.lb2.Location = New System.Drawing.Point(3, 53)
    Me.lb2.Name = "lb2"
    Me.lb2.NTSDbField = ""
    Me.lb2.Size = New System.Drawing.Size(17, 13)
    Me.lb2.TabIndex = 13
    Me.lb2.Text = "2:"
    Me.lb2.Tooltip = ""
    Me.lb2.UseMnemonic = False
    '
    'edEt_impsca_5
    '
    Me.edEt_impsca_5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_impsca_5.EditValue = "0"
    Me.edEt_impsca_5.Enabled = False
    Me.edEt_impsca_5.Location = New System.Drawing.Point(255, 128)
    Me.edEt_impsca_5.Name = "edEt_impsca_5"
    Me.edEt_impsca_5.NTSDbField = ""
    Me.edEt_impsca_5.NTSFormat = "0"
    Me.edEt_impsca_5.NTSForzaVisZoom = False
    Me.edEt_impsca_5.NTSOldValue = ""
    Me.edEt_impsca_5.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edEt_impsca_5.Properties.Appearance.Options.UseBackColor = True
    Me.edEt_impsca_5.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_impsca_5.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_impsca_5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_impsca_5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_impsca_5.Properties.AutoHeight = False
    Me.edEt_impsca_5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_impsca_5.Properties.MaxLength = 9
    Me.edEt_impsca_5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_impsca_5.Size = New System.Drawing.Size(97, 20)
    Me.edEt_impsca_5.TabIndex = 12
    '
    'edEt_impsca_4
    '
    Me.edEt_impsca_4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_impsca_4.EditValue = "0"
    Me.edEt_impsca_4.Enabled = False
    Me.edEt_impsca_4.Location = New System.Drawing.Point(255, 102)
    Me.edEt_impsca_4.Name = "edEt_impsca_4"
    Me.edEt_impsca_4.NTSDbField = ""
    Me.edEt_impsca_4.NTSFormat = "0"
    Me.edEt_impsca_4.NTSForzaVisZoom = False
    Me.edEt_impsca_4.NTSOldValue = ""
    Me.edEt_impsca_4.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edEt_impsca_4.Properties.Appearance.Options.UseBackColor = True
    Me.edEt_impsca_4.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_impsca_4.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_impsca_4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_impsca_4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_impsca_4.Properties.AutoHeight = False
    Me.edEt_impsca_4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_impsca_4.Properties.MaxLength = 9
    Me.edEt_impsca_4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_impsca_4.Size = New System.Drawing.Size(97, 20)
    Me.edEt_impsca_4.TabIndex = 11
    '
    'edEt_impsca_3
    '
    Me.edEt_impsca_3.AllowDrop = True
    Me.edEt_impsca_3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_impsca_3.EditValue = "0"
    Me.edEt_impsca_3.Enabled = False
    Me.edEt_impsca_3.Location = New System.Drawing.Point(255, 76)
    Me.edEt_impsca_3.Name = "edEt_impsca_3"
    Me.edEt_impsca_3.NTSDbField = ""
    Me.edEt_impsca_3.NTSFormat = "0"
    Me.edEt_impsca_3.NTSForzaVisZoom = False
    Me.edEt_impsca_3.NTSOldValue = ""
    Me.edEt_impsca_3.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edEt_impsca_3.Properties.Appearance.Options.UseBackColor = True
    Me.edEt_impsca_3.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_impsca_3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_impsca_3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_impsca_3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_impsca_3.Properties.AutoHeight = False
    Me.edEt_impsca_3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_impsca_3.Properties.MaxLength = 9
    Me.edEt_impsca_3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_impsca_3.Size = New System.Drawing.Size(97, 20)
    Me.edEt_impsca_3.TabIndex = 10
    '
    'edEt_impsca_2
    '
    Me.edEt_impsca_2.AllowDrop = True
    Me.edEt_impsca_2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_impsca_2.EditValue = "0"
    Me.edEt_impsca_2.Enabled = False
    Me.edEt_impsca_2.Location = New System.Drawing.Point(255, 50)
    Me.edEt_impsca_2.Name = "edEt_impsca_2"
    Me.edEt_impsca_2.NTSDbField = ""
    Me.edEt_impsca_2.NTSFormat = "0"
    Me.edEt_impsca_2.NTSForzaVisZoom = False
    Me.edEt_impsca_2.NTSOldValue = ""
    Me.edEt_impsca_2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edEt_impsca_2.Properties.Appearance.Options.UseBackColor = True
    Me.edEt_impsca_2.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_impsca_2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_impsca_2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_impsca_2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_impsca_2.Properties.AutoHeight = False
    Me.edEt_impsca_2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_impsca_2.Properties.MaxLength = 9
    Me.edEt_impsca_2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_impsca_2.Size = New System.Drawing.Size(97, 20)
    Me.edEt_impsca_2.TabIndex = 9
    '
    'lb1
    '
    Me.lb1.AutoSize = True
    Me.lb1.BackColor = System.Drawing.Color.Transparent
    Me.lb1.Location = New System.Drawing.Point(3, 28)
    Me.lb1.Name = "lb1"
    Me.lb1.NTSDbField = ""
    Me.lb1.Size = New System.Drawing.Size(17, 13)
    Me.lb1.TabIndex = 3
    Me.lb1.Text = "1:"
    Me.lb1.Tooltip = ""
    Me.lb1.UseMnemonic = False
    '
    'edEt_impsca_1
    '
    Me.edEt_impsca_1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEt_impsca_1.EditValue = "0"
    Me.edEt_impsca_1.Enabled = False
    Me.edEt_impsca_1.Location = New System.Drawing.Point(255, 24)
    Me.edEt_impsca_1.Name = "edEt_impsca_1"
    Me.edEt_impsca_1.NTSDbField = ""
    Me.edEt_impsca_1.NTSFormat = "0"
    Me.edEt_impsca_1.NTSForzaVisZoom = False
    Me.edEt_impsca_1.NTSOldValue = ""
    Me.edEt_impsca_1.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edEt_impsca_1.Properties.Appearance.Options.UseBackColor = True
    Me.edEt_impsca_1.Properties.Appearance.Options.UseTextOptions = True
    Me.edEt_impsca_1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEt_impsca_1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEt_impsca_1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEt_impsca_1.Properties.AutoHeight = False
    Me.edEt_impsca_1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEt_impsca_1.Properties.MaxLength = 9
    Me.edEt_impsca_1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEt_impsca_1.Size = New System.Drawing.Size(97, 20)
    Me.edEt_impsca_1.TabIndex = 8
    '
    'cmdOk
    '
    Me.cmdOk.ImagePath = ""
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(473, 7)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.NTSContextMenu = Nothing
    Me.cmdOk.Size = New System.Drawing.Size(105, 22)
    Me.cmdOk.TabIndex = 11
    Me.cmdOk.Text = "&Esci"
    '
    'FRMORDTSC
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(586, 242)
    Me.Controls.Add(Me.cmdOk)
    Me.Controls.Add(Me.pnSpese)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMORDTSC"
    Me.Text = "VISUALIZZAZIONE RATE SCADENZE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnSpese, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSpese.ResumeLayout(False)
    Me.pnSpese.PerformLayout()
    CType(Me.cbEt_tippaga_8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbEt_tippaga_7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbEt_tippaga_6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbEt_tippaga_5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbEt_tippaga_4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbEt_tippaga_3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbEt_tippaga_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbEt_tippaga_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_datsca_8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_datsca_7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_datsca_6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_datsca_5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_datsca_4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_datsca_3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_datsca_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_datsca_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_impscav_8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_impscav_7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_impscav_6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_impscav_5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_impscav_4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_impscav_3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_impscav_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_impscav_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_impsca_8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_impsca_7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_impsca_6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_impsca_5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_impsca_4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_impsca_3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_impsca_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEt_impsca_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, False)
    '----------------------------------------------------------------------------------------------------------------
    Dim dttTipPag As New DataTable()
    '----------------------------------------------------------------------------------------------------------------

    Try
      '--------------------------------------------------------------------------------------------------------------
      dttTipPag.Columns.Add("cod", GetType(Integer))
      dttTipPag.Columns.Add("val", GetType(String))
      dttTipPag.Rows.Add(New Object() {0, "(Nessuno)"})
      dttTipPag.Rows.Add(New Object() {1, "Tratta"})
      dttTipPag.Rows.Add(New Object() {2, "R.B. o RIBA"})
      dttTipPag.Rows.Add(New Object() {3, "Rim.Diretta"})
      dttTipPag.Rows.Add(New Object() {4, "Contanti"})
      dttTipPag.Rows.Add(New Object() {5, "Accr.Bancario"})
      dttTipPag.AcceptChanges()
      cbEt_tippaga_1.DataSource = dttTipPag : cbEt_tippaga_1.ValueMember = "cod" : cbEt_tippaga_1.DisplayMember = "val"
      cbEt_tippaga_2.DataSource = dttTipPag : cbEt_tippaga_2.ValueMember = "cod" : cbEt_tippaga_2.DisplayMember = "val"
      cbEt_tippaga_3.DataSource = dttTipPag : cbEt_tippaga_3.ValueMember = "cod" : cbEt_tippaga_3.DisplayMember = "val"
      cbEt_tippaga_4.DataSource = dttTipPag : cbEt_tippaga_4.ValueMember = "cod" : cbEt_tippaga_4.DisplayMember = "val"
      cbEt_tippaga_5.DataSource = dttTipPag : cbEt_tippaga_5.ValueMember = "cod" : cbEt_tippaga_5.DisplayMember = "val"
      cbEt_tippaga_6.DataSource = dttTipPag : cbEt_tippaga_6.ValueMember = "cod" : cbEt_tippaga_6.DisplayMember = "val"
      cbEt_tippaga_7.DataSource = dttTipPag : cbEt_tippaga_7.ValueMember = "cod" : cbEt_tippaga_7.DisplayMember = "val"
      cbEt_tippaga_8.DataSource = dttTipPag : cbEt_tippaga_8.ValueMember = "cod" : cbEt_tippaga_8.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
      cbEt_tippaga_8.NTSSetParam(oApp.Tr(Me, 128686243031712500, "Tipo scadenza 8"))
      cbEt_tippaga_7.NTSSetParam(oApp.Tr(Me, 128686243031868750, "Tipo scadenza 7"))
      cbEt_tippaga_6.NTSSetParam(oApp.Tr(Me, 128686243032025000, "Tipo scadenza 6"))
      cbEt_tippaga_5.NTSSetParam(oApp.Tr(Me, 128686243032181250, "Tipo scadenza 5"))
      cbEt_tippaga_4.NTSSetParam(oApp.Tr(Me, 128686243032337500, "Tipo scadenza 4"))
      cbEt_tippaga_3.NTSSetParam(oApp.Tr(Me, 128686243032493750, "Tipo scadenza 3"))
      cbEt_tippaga_2.NTSSetParam(oApp.Tr(Me, 128686243032650000, "Tipo scadenza 2"))
      cbEt_tippaga_1.NTSSetParam(oApp.Tr(Me, 128686243032806250, "Tipo scadenza 1"))
      edEt_datsca_8.NTSSetParam(oMenu, oApp.Tr(Me, 128686243032962500, "Data scadenza 8"), True)
      edEt_datsca_7.NTSSetParam(oMenu, oApp.Tr(Me, 128686243033118750, "Data scadenza 7"), True)
      edEt_datsca_6.NTSSetParam(oMenu, oApp.Tr(Me, 128686243033275000, "Data scadenza 6"), True)
      edEt_datsca_5.NTSSetParam(oMenu, oApp.Tr(Me, 128686243033431250, "Data scadenza 5"), True)
      edEt_datsca_4.NTSSetParam(oMenu, oApp.Tr(Me, 128686243033587500, "Data scadenza 4"), True)
      edEt_datsca_3.NTSSetParam(oMenu, oApp.Tr(Me, 128686243033743750, "Data scadenza 3"), True)
      edEt_datsca_2.NTSSetParam(oMenu, oApp.Tr(Me, 128686243033900000, "Data scadenza 2"), True)
      edEt_datsca_1.NTSSetParam(oMenu, oApp.Tr(Me, 128686243034056250, "Data scadenza 1"), True)
      edEt_impscav_8.NTSSetParam(oMenu, oApp.Tr(Me, 128686243034212500, "Importo valuta 8"), oApp.FormatImpVal)
      edEt_impscav_7.NTSSetParam(oMenu, oApp.Tr(Me, 128686243034368750, "Importo valuta 7"), oApp.FormatImpVal)
      edEt_impscav_6.NTSSetParam(oMenu, oApp.Tr(Me, 128686243034525000, "Importo valuta 6"), oApp.FormatImpVal)
      edEt_impscav_5.NTSSetParam(oMenu, oApp.Tr(Me, 128686243034681250, "Importo valuta 5"), oApp.FormatImpVal)
      edEt_impscav_4.NTSSetParam(oMenu, oApp.Tr(Me, 128686243034837500, "Importo valuta 4"), oApp.FormatImpVal)
      edEt_impscav_3.NTSSetParam(oMenu, oApp.Tr(Me, 128686243034993750, "Importo valuta 3"), oApp.FormatImpVal)
      edEt_impscav_2.NTSSetParam(oMenu, oApp.Tr(Me, 128686243035150000, "Importo valuta 2"), oApp.FormatImpVal)
      edEt_impscav_1.NTSSetParam(oMenu, oApp.Tr(Me, 128686243035306250, "Importo valuta 1"), oApp.FormatImpVal)
      edEt_impsca_8.NTSSetParam(oMenu, oApp.Tr(Me, 128686243036243750, "Importo 8"), oApp.FormatImporti)
      edEt_impsca_7.NTSSetParam(oMenu, oApp.Tr(Me, 128686243036556250, "Importo 7"), oApp.FormatImporti)
      edEt_impsca_6.NTSSetParam(oMenu, oApp.Tr(Me, 128686243036868750, "Importo 6"), oApp.FormatImporti)
      edEt_impsca_5.NTSSetParam(oMenu, oApp.Tr(Me, 128686243037650000, "Importo 5"), oApp.FormatImporti)
      edEt_impsca_4.NTSSetParam(oMenu, oApp.Tr(Me, 128686243037806250, "Importo 4"), oApp.FormatImporti)
      edEt_impsca_3.NTSSetParam(oMenu, oApp.Tr(Me, 128686243037962500, "Importo 3"), oApp.FormatImporti)
      edEt_impsca_2.NTSSetParam(oMenu, oApp.Tr(Me, 128686243038118750, "Importo 2"), oApp.FormatImporti)
      edEt_impsca_1.NTSSetParam(oMenu, oApp.Tr(Me, 128686243038431250, "Importo 1"), oApp.FormatImporti)
      '--------------------------------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, True)
    '----------------------------------------------------------------------------------------------------------------
  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRMVEDTSC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      GctlTipoDoc = ocleDocu.dttET.Rows(0)!et_tipork.ToString
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = 1 To 8
        CType(pnSpese.Controls("cbEt_tippaga_" & i.ToString), NTSComboBox).SelectedValue = NTSCInt(ocleDocu.dttET.Rows(0)("et_tippaga_" & i.ToString)).ToString
        If ocleDocu.dttET.Rows(0)("et_datsca_" & i.ToString).Equals(DBNull.Value) Then
          CType(pnSpese.Controls("edEt_datsca_" & i.ToString), NTSTextBoxData).Text = ""
        Else
          CType(pnSpese.Controls("edEt_datsca_" & i.ToString), NTSTextBoxData).Text = NTSCDate(ocleDocu.dttET.Rows(0)("et_datsca_" & i.ToString)).ToShortDateString
        End If
        CType(pnSpese.Controls("edEt_impsca_" & i.ToString), NTSTextBoxNum).Text = NTSCDec(ocleDocu.dttET.Rows(0)("et_impsca_" & i.ToString)).ToString
        CType(pnSpese.Controls("edEt_impscav_" & i.ToString), NTSTextBoxNum).Text = NTSCDec(ocleDocu.dttET.Rows(0)("et_impscav_" & i.ToString)).ToString
      Next i
      '--------------------------------------------------------------------------------------------------------------
      SetStato()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi Pulsanti"
  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.Close()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Function SetStato() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = 1 To 8
        If NTSCInt(CType(pnSpese.Controls("cbEt_tippaga_" & i.ToString), NTSComboBox).SelectedValue) <> 0 Then
          If CType(pnSpese.Controls("edEt_datsca_" & i.ToString), NTSTextBoxData).Text = "" Then
            CType(pnSpese.Controls("edEt_datsca_" & i.ToString), NTSTextBoxData).Text = NTSCDate(ocleDocu.dttET.Rows(0)!et_datdoc).ToShortDateString
          End If
          If NTSCInt(ocleDocu.dttET.Rows(0)!et_valuta) = 0 Then
            CType(pnSpese.Controls("edEt_impscav_" & i.ToString), NTSTextBoxNum).Text = "0"
          End If
        Else
          CType(pnSpese.Controls("edEt_datsca_" & i.ToString), NTSTextBoxData).Text = ""
          CType(pnSpese.Controls("edEt_impsca_" & i.ToString), NTSTextBoxNum).Text = "0"
          CType(pnSpese.Controls("edEt_impscav_" & i.ToString), NTSTextBoxNum).Text = "0"
        End If
      Next i
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

End Class