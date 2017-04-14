Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGDUAR
#Region "Variabili"
  Public oCallParams As CLE__CLDP
  Public oCleArti As CLEMGARTI
  Public bOk As Boolean = False
  Public bNew As Boolean = False

  Public lProgr As Integer
  'Public nLungRoot As Integer
  'Public nLungExt As Integer
  'Public strGeneraCodice As String
  Public gbDuarArticoloGenerato As Boolean
  Public glDuarProgr As Integer
  Public gstrDuarRoot As String

  Public dPerqtaOrigine As Decimal = 0
  Public bOnLoading As Boolean = False
#End Region

#Region "Controlli"
  Public WithEvents pnNuovo As NTSInformatica.NTSPanel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents lbCodart As NTSInformatica.NTSLabel
  Public WithEvents ckArtval As NTSInformatica.NTSCheckBox
  Public WithEvents ckProvvigioni As NTSInformatica.NTSCheckBox
  Public WithEvents ckSconti As NTSInformatica.NTSCheckBox
  Public WithEvents ckListini As NTSInformatica.NTSCheckBox
  Public WithEvents ckKit As NTSInformatica.NTSCheckBox
  Public WithEvents ckConai As NTSInformatica.NTSCheckBox
  Public WithEvents fmCheck As NTSInformatica.NTSGroupBox
  Public WithEvents ckArtfasi As NTSInformatica.NTSCheckBox
  Public WithEvents edCodart As NTSInformatica.NTSTextBoxStr
  Public WithEvents fmUnitamisura As NTSInformatica.NTSGroupBox
  Public WithEvents edAr_unmis As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAr_unmis As NTSInformatica.NTSLabel
  Public WithEvents lbAr_unmis2 As NTSInformatica.NTSLabel
  Public WithEvents edAr_confez2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAr_unmis2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAr_confez2 As NTSInformatica.NTSLabel
  Public WithEvents fmNuovoArt As NTSInformatica.NTSGroupBox
  Public WithEvents ckAr_gesubic As NTSInformatica.NTSCheckBox
  Public WithEvents ckAr_gesfasi As NTSInformatica.NTSCheckBox
  Public WithEvents ckAr_geslotti As NTSInformatica.NTSCheckBox
  Public WithEvents ckAr_gestmatr As NTSInformatica.NTSCheckBox
  Public WithEvents lbAr_gescomm As NTSInformatica.NTSLabel
  Public WithEvents cbAr_gescomm As NTSInformatica.NTSComboBox
  Public WithEvents lbAr_perqta As NTSInformatica.NTSLabel
  Public WithEvents edAr_perqta As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbAr_codtipa As NTSInformatica.NTSComboBox
  Public WithEvents lbAr_codtipa As NTSInformatica.NTSLabel
  Public WithEvents lbAr_um4 As NTSInformatica.NTSLabel
  Public WithEvents edAr_um4 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAr_formula As NTSInformatica.NTSLabel
  Public WithEvents edAr_formula As NTSInformatica.NTSTextBoxStr
  Public WithEvents ckValoriAttributi As NTSInformatica.NTSCheckBox
  Public WithEvents ckArtmaga As NTSInformatica.NTSCheckBox
  Public WithEvents cmdOk As NTSInformatica.NTSButton
#End Region

#Region "Init"
  Public Overridable Sub InitializeComponent()
    Me.pnNuovo = New NTSInformatica.NTSPanel
    Me.fmNuovoArt = New NTSInformatica.NTSGroupBox
    Me.edAr_formula = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_formula = New NTSInformatica.NTSLabel
    Me.lbAr_codtipa = New NTSInformatica.NTSLabel
    Me.cbAr_codtipa = New NTSInformatica.NTSComboBox
    Me.lbAr_perqta = New NTSInformatica.NTSLabel
    Me.edAr_perqta = New NTSInformatica.NTSTextBoxNum
    Me.ckAr_geslotti = New NTSInformatica.NTSCheckBox
    Me.ckAr_gestmatr = New NTSInformatica.NTSCheckBox
    Me.lbAr_gescomm = New NTSInformatica.NTSLabel
    Me.ckAr_gesubic = New NTSInformatica.NTSCheckBox
    Me.cbAr_gescomm = New NTSInformatica.NTSComboBox
    Me.ckAr_gesfasi = New NTSInformatica.NTSCheckBox
    Me.fmUnitamisura = New NTSInformatica.NTSGroupBox
    Me.lbAr_um4 = New NTSInformatica.NTSLabel
    Me.edAr_um4 = New NTSInformatica.NTSTextBoxStr
    Me.edAr_unmis = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_unmis = New NTSInformatica.NTSLabel
    Me.lbAr_unmis2 = New NTSInformatica.NTSLabel
    Me.edAr_confez2 = New NTSInformatica.NTSTextBoxStr
    Me.edAr_unmis2 = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_confez2 = New NTSInformatica.NTSLabel
    Me.edCodart = New NTSInformatica.NTSTextBoxStr
    Me.fmCheck = New NTSInformatica.NTSGroupBox
    Me.ckValoriAttributi = New NTSInformatica.NTSCheckBox
    Me.ckArtfasi = New NTSInformatica.NTSCheckBox
    Me.ckArtval = New NTSInformatica.NTSCheckBox
    Me.ckKit = New NTSInformatica.NTSCheckBox
    Me.ckListini = New NTSInformatica.NTSCheckBox
    Me.ckConai = New NTSInformatica.NTSCheckBox
    Me.ckSconti = New NTSInformatica.NTSCheckBox
    Me.ckProvvigioni = New NTSInformatica.NTSCheckBox
    Me.lbCodart = New NTSInformatica.NTSLabel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.ckArtmaga = New NTSInformatica.NTSCheckBox
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnNuovo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnNuovo.SuspendLayout()
    CType(Me.fmNuovoArt, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmNuovoArt.SuspendLayout()
    CType(Me.edAr_formula.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_codtipa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_perqta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_geslotti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_gestmatr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_gesubic.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_gescomm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_gesfasi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmUnitamisura, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmUnitamisura.SuspendLayout()
    CType(Me.edAr_um4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_unmis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_confez2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_unmis2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmCheck, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmCheck.SuspendLayout()
    CType(Me.ckValoriAttributi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckArtfasi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckArtval.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckKit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckListini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckConai.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSconti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckProvvigioni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckArtmaga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'pnNuovo
    '
    Me.pnNuovo.AllowDrop = True
    Me.pnNuovo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnNuovo.Appearance.Options.UseBackColor = True
    Me.pnNuovo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnNuovo.Controls.Add(Me.fmNuovoArt)
    Me.pnNuovo.Controls.Add(Me.fmUnitamisura)
    Me.pnNuovo.Controls.Add(Me.edCodart)
    Me.pnNuovo.Controls.Add(Me.fmCheck)
    Me.pnNuovo.Controls.Add(Me.lbCodart)
    Me.pnNuovo.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnNuovo.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnNuovo.Location = New System.Drawing.Point(0, 0)
    Me.pnNuovo.Name = "pnNuovo"
    Me.pnNuovo.NTSActiveTrasparency = True
    Me.pnNuovo.Size = New System.Drawing.Size(469, 475)
    Me.pnNuovo.TabIndex = 12
    Me.pnNuovo.Text = "NtsPanel1"
    '
    'fmNuovoArt
    '
    Me.fmNuovoArt.AllowDrop = True
    Me.fmNuovoArt.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmNuovoArt.Appearance.Options.UseBackColor = True
    Me.fmNuovoArt.Controls.Add(Me.edAr_formula)
    Me.fmNuovoArt.Controls.Add(Me.lbAr_formula)
    Me.fmNuovoArt.Controls.Add(Me.lbAr_codtipa)
    Me.fmNuovoArt.Controls.Add(Me.cbAr_codtipa)
    Me.fmNuovoArt.Controls.Add(Me.lbAr_perqta)
    Me.fmNuovoArt.Controls.Add(Me.edAr_perqta)
    Me.fmNuovoArt.Controls.Add(Me.ckAr_geslotti)
    Me.fmNuovoArt.Controls.Add(Me.ckAr_gestmatr)
    Me.fmNuovoArt.Controls.Add(Me.lbAr_gescomm)
    Me.fmNuovoArt.Controls.Add(Me.ckAr_gesubic)
    Me.fmNuovoArt.Controls.Add(Me.cbAr_gescomm)
    Me.fmNuovoArt.Controls.Add(Me.ckAr_gesfasi)
    Me.fmNuovoArt.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmNuovoArt.Location = New System.Drawing.Point(12, 219)
    Me.fmNuovoArt.Name = "fmNuovoArt"
    Me.fmNuovoArt.Size = New System.Drawing.Size(437, 244)
    Me.fmNuovoArt.TabIndex = 679
    Me.fmNuovoArt.Text = "DATI NUOVO ARTICOLO"
    '
    'edAr_formula
    '
    Me.edAr_formula.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_formula.Location = New System.Drawing.Point(12, 210)
    Me.edAr_formula.Name = "edAr_formula"
    Me.edAr_formula.NTSDbField = ""
    Me.edAr_formula.NTSForzaVisZoom = False
    Me.edAr_formula.NTSOldValue = ""
    Me.edAr_formula.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_formula.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_formula.Properties.AutoHeight = False
    Me.edAr_formula.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_formula.Properties.MaxLength = 65536
    Me.edAr_formula.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_formula.Size = New System.Drawing.Size(417, 20)
    Me.edAr_formula.TabIndex = 688
    '
    'lbAr_formula
    '
    Me.lbAr_formula.AutoSize = True
    Me.lbAr_formula.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_formula.Location = New System.Drawing.Point(11, 190)
    Me.lbAr_formula.Name = "lbAr_formula"
    Me.lbAr_formula.NTSDbField = ""
    Me.lbAr_formula.Size = New System.Drawing.Size(166, 13)
    Me.lbAr_formula.TabIndex = 685
    Me.lbAr_formula.Text = "Formula di trasformazione in UMP"
    Me.lbAr_formula.Tooltip = ""
    Me.lbAr_formula.UseMnemonic = False
    '
    'lbAr_codtipa
    '
    Me.lbAr_codtipa.AutoSize = True
    Me.lbAr_codtipa.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_codtipa.Location = New System.Drawing.Point(11, 164)
    Me.lbAr_codtipa.Name = "lbAr_codtipa"
    Me.lbAr_codtipa.NTSDbField = ""
    Me.lbAr_codtipa.Size = New System.Drawing.Size(87, 13)
    Me.lbAr_codtipa.TabIndex = 687
    Me.lbAr_codtipa.Text = "Tipologia articolo"
    Me.lbAr_codtipa.Tooltip = ""
    Me.lbAr_codtipa.UseMnemonic = False
    '
    'cbAr_codtipa
    '
    Me.cbAr_codtipa.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_codtipa.DataSource = Nothing
    Me.cbAr_codtipa.DisplayMember = ""
    Me.cbAr_codtipa.Location = New System.Drawing.Point(232, 161)
    Me.cbAr_codtipa.Name = "cbAr_codtipa"
    Me.cbAr_codtipa.NTSDbField = ""
    Me.cbAr_codtipa.Properties.AutoHeight = False
    Me.cbAr_codtipa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_codtipa.Properties.DropDownRows = 30
    Me.cbAr_codtipa.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_codtipa.SelectedValue = ""
    Me.cbAr_codtipa.Size = New System.Drawing.Size(197, 20)
    Me.cbAr_codtipa.TabIndex = 686
    Me.cbAr_codtipa.ValueMember = ""
    '
    'lbAr_perqta
    '
    Me.lbAr_perqta.AutoSize = True
    Me.lbAr_perqta.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_perqta.Location = New System.Drawing.Point(11, 114)
    Me.lbAr_perqta.Name = "lbAr_perqta"
    Me.lbAr_perqta.NTSDbField = ""
    Me.lbAr_perqta.Size = New System.Drawing.Size(149, 13)
    Me.lbAr_perqta.TabIndex = 684
    Me.lbAr_perqta.Text = "Moltiplicatore quantità/prezzo"
    Me.lbAr_perqta.Tooltip = ""
    Me.lbAr_perqta.UseMnemonic = False
    '
    'edAr_perqta
    '
    Me.edAr_perqta.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_perqta.EditValue = "0"
    Me.edAr_perqta.Location = New System.Drawing.Point(232, 111)
    Me.edAr_perqta.Name = "edAr_perqta"
    Me.edAr_perqta.NTSDbField = ""
    Me.edAr_perqta.NTSFormat = "0"
    Me.edAr_perqta.NTSForzaVisZoom = False
    Me.edAr_perqta.NTSOldValue = ""
    Me.edAr_perqta.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_perqta.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_perqta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_perqta.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_perqta.Properties.AutoHeight = False
    Me.edAr_perqta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_perqta.Properties.MaxLength = 65536
    Me.edAr_perqta.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_perqta.Size = New System.Drawing.Size(197, 20)
    Me.edAr_perqta.TabIndex = 685
    '
    'ckAr_geslotti
    '
    Me.ckAr_geslotti.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_geslotti.Location = New System.Drawing.Point(12, 66)
    Me.ckAr_geslotti.Name = "ckAr_geslotti"
    Me.ckAr_geslotti.NTSCheckValue = "S"
    Me.ckAr_geslotti.NTSUnCheckValue = "N"
    Me.ckAr_geslotti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_geslotti.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_geslotti.Properties.AutoHeight = False
    Me.ckAr_geslotti.Properties.Caption = "Gestione Lotti"
    Me.ckAr_geslotti.Size = New System.Drawing.Size(100, 19)
    Me.ckAr_geslotti.TabIndex = 680
    '
    'ckAr_gestmatr
    '
    Me.ckAr_gestmatr.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_gestmatr.Location = New System.Drawing.Point(12, 86)
    Me.ckAr_gestmatr.Name = "ckAr_gestmatr"
    Me.ckAr_gestmatr.NTSCheckValue = "S"
    Me.ckAr_gestmatr.NTSUnCheckValue = "N"
    Me.ckAr_gestmatr.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_gestmatr.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_gestmatr.Properties.AutoHeight = False
    Me.ckAr_gestmatr.Properties.Caption = "Gestione matricole"
    Me.ckAr_gestmatr.Size = New System.Drawing.Size(113, 19)
    Me.ckAr_gestmatr.TabIndex = 681
    '
    'lbAr_gescomm
    '
    Me.lbAr_gescomm.AutoSize = True
    Me.lbAr_gescomm.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_gescomm.Location = New System.Drawing.Point(11, 140)
    Me.lbAr_gescomm.Name = "lbAr_gescomm"
    Me.lbAr_gescomm.NTSDbField = ""
    Me.lbAr_gescomm.Size = New System.Drawing.Size(120, 13)
    Me.lbAr_gescomm.TabIndex = 682
    Me.lbAr_gescomm.Text = "Gestione per commessa"
    Me.lbAr_gescomm.Tooltip = ""
    Me.lbAr_gescomm.UseMnemonic = False
    '
    'ckAr_gesubic
    '
    Me.ckAr_gesubic.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_gesubic.Location = New System.Drawing.Point(12, 26)
    Me.ckAr_gesubic.Name = "ckAr_gesubic"
    Me.ckAr_gesubic.NTSCheckValue = "S"
    Me.ckAr_gesubic.NTSUnCheckValue = "N"
    Me.ckAr_gesubic.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_gesubic.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_gesubic.Properties.AutoHeight = False
    Me.ckAr_gesubic.Properties.Caption = "Gestione ubicazione"
    Me.ckAr_gesubic.Size = New System.Drawing.Size(121, 19)
    Me.ckAr_gesubic.TabIndex = 677
    '
    'cbAr_gescomm
    '
    Me.cbAr_gescomm.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_gescomm.DataSource = Nothing
    Me.cbAr_gescomm.DisplayMember = ""
    Me.cbAr_gescomm.Location = New System.Drawing.Point(232, 137)
    Me.cbAr_gescomm.Name = "cbAr_gescomm"
    Me.cbAr_gescomm.NTSDbField = ""
    Me.cbAr_gescomm.Properties.AutoHeight = False
    Me.cbAr_gescomm.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_gescomm.Properties.DropDownRows = 30
    Me.cbAr_gescomm.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_gescomm.SelectedValue = ""
    Me.cbAr_gescomm.Size = New System.Drawing.Size(197, 20)
    Me.cbAr_gescomm.TabIndex = 683
    Me.cbAr_gescomm.ValueMember = ""
    '
    'ckAr_gesfasi
    '
    Me.ckAr_gesfasi.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_gesfasi.Location = New System.Drawing.Point(12, 46)
    Me.ckAr_gesfasi.Name = "ckAr_gesfasi"
    Me.ckAr_gesfasi.NTSCheckValue = "S"
    Me.ckAr_gesfasi.NTSUnCheckValue = "N"
    Me.ckAr_gesfasi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_gesfasi.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_gesfasi.Properties.AutoHeight = False
    Me.ckAr_gesfasi.Properties.Caption = "Gestione fasi"
    Me.ckAr_gesfasi.Size = New System.Drawing.Size(93, 19)
    Me.ckAr_gesfasi.TabIndex = 678
    '
    'fmUnitamisura
    '
    Me.fmUnitamisura.AllowDrop = True
    Me.fmUnitamisura.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmUnitamisura.Appearance.Options.UseBackColor = True
    Me.fmUnitamisura.Controls.Add(Me.lbAr_um4)
    Me.fmUnitamisura.Controls.Add(Me.edAr_um4)
    Me.fmUnitamisura.Controls.Add(Me.edAr_unmis)
    Me.fmUnitamisura.Controls.Add(Me.lbAr_unmis)
    Me.fmUnitamisura.Controls.Add(Me.lbAr_unmis2)
    Me.fmUnitamisura.Controls.Add(Me.edAr_confez2)
    Me.fmUnitamisura.Controls.Add(Me.edAr_unmis2)
    Me.fmUnitamisura.Controls.Add(Me.lbAr_confez2)
    Me.fmUnitamisura.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmUnitamisura.Location = New System.Drawing.Point(274, 40)
    Me.fmUnitamisura.Name = "fmUnitamisura"
    Me.fmUnitamisura.Size = New System.Drawing.Size(175, 135)
    Me.fmUnitamisura.TabIndex = 676
    Me.fmUnitamisura.Text = "U.M. NUOVO ARTICOLO"
    '
    'lbAr_um4
    '
    Me.lbAr_um4.AutoSize = True
    Me.lbAr_um4.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_um4.Location = New System.Drawing.Point(14, 107)
    Me.lbAr_um4.Name = "lbAr_um4"
    Me.lbAr_um4.NTSDbField = ""
    Me.lbAr_um4.Size = New System.Drawing.Size(69, 13)
    Me.lbAr_um4.TabIndex = 509
    Me.lbAr_um4.Text = "U.M. formula"
    Me.lbAr_um4.Tooltip = ""
    Me.lbAr_um4.UseMnemonic = False
    '
    'edAr_um4
    '
    Me.edAr_um4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_um4.Location = New System.Drawing.Point(99, 104)
    Me.edAr_um4.Name = "edAr_um4"
    Me.edAr_um4.NTSDbField = ""
    Me.edAr_um4.NTSForzaVisZoom = False
    Me.edAr_um4.NTSOldValue = ""
    Me.edAr_um4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_um4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_um4.Properties.AutoHeight = False
    Me.edAr_um4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_um4.Properties.MaxLength = 65536
    Me.edAr_um4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_um4.Size = New System.Drawing.Size(57, 20)
    Me.edAr_um4.TabIndex = 510
    '
    'edAr_unmis
    '
    Me.edAr_unmis.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_unmis.Location = New System.Drawing.Point(99, 26)
    Me.edAr_unmis.Name = "edAr_unmis"
    Me.edAr_unmis.NTSDbField = ""
    Me.edAr_unmis.NTSForzaVisZoom = False
    Me.edAr_unmis.NTSOldValue = ""
    Me.edAr_unmis.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_unmis.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_unmis.Properties.AutoHeight = False
    Me.edAr_unmis.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_unmis.Properties.MaxLength = 65536
    Me.edAr_unmis.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_unmis.Size = New System.Drawing.Size(57, 20)
    Me.edAr_unmis.TabIndex = 505
    '
    'lbAr_unmis
    '
    Me.lbAr_unmis.AutoSize = True
    Me.lbAr_unmis.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_unmis.Location = New System.Drawing.Point(14, 29)
    Me.lbAr_unmis.Name = "lbAr_unmis"
    Me.lbAr_unmis.NTSDbField = ""
    Me.lbAr_unmis.Size = New System.Drawing.Size(52, 13)
    Me.lbAr_unmis.TabIndex = 15
    Me.lbAr_unmis.Text = "Principale"
    Me.lbAr_unmis.Tooltip = ""
    Me.lbAr_unmis.UseMnemonic = False
    '
    'lbAr_unmis2
    '
    Me.lbAr_unmis2.AutoSize = True
    Me.lbAr_unmis2.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_unmis2.Location = New System.Drawing.Point(14, 81)
    Me.lbAr_unmis2.Name = "lbAr_unmis2"
    Me.lbAr_unmis2.NTSDbField = ""
    Me.lbAr_unmis2.Size = New System.Drawing.Size(60, 13)
    Me.lbAr_unmis2.TabIndex = 16
    Me.lbAr_unmis2.Text = "Secondaria"
    Me.lbAr_unmis2.Tooltip = ""
    Me.lbAr_unmis2.UseMnemonic = False
    '
    'edAr_confez2
    '
    Me.edAr_confez2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_confez2.Location = New System.Drawing.Point(99, 52)
    Me.edAr_confez2.Name = "edAr_confez2"
    Me.edAr_confez2.NTSDbField = ""
    Me.edAr_confez2.NTSForzaVisZoom = False
    Me.edAr_confez2.NTSOldValue = ""
    Me.edAr_confez2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_confez2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_confez2.Properties.AutoHeight = False
    Me.edAr_confez2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_confez2.Properties.MaxLength = 65536
    Me.edAr_confez2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_confez2.Size = New System.Drawing.Size(57, 20)
    Me.edAr_confez2.TabIndex = 508
    '
    'edAr_unmis2
    '
    Me.edAr_unmis2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_unmis2.Location = New System.Drawing.Point(99, 78)
    Me.edAr_unmis2.Name = "edAr_unmis2"
    Me.edAr_unmis2.NTSDbField = ""
    Me.edAr_unmis2.NTSForzaVisZoom = False
    Me.edAr_unmis2.NTSOldValue = ""
    Me.edAr_unmis2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_unmis2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_unmis2.Properties.AutoHeight = False
    Me.edAr_unmis2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_unmis2.Properties.MaxLength = 65536
    Me.edAr_unmis2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_unmis2.Size = New System.Drawing.Size(57, 20)
    Me.edAr_unmis2.TabIndex = 506
    '
    'lbAr_confez2
    '
    Me.lbAr_confez2.AutoSize = True
    Me.lbAr_confez2.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_confez2.Location = New System.Drawing.Point(13, 55)
    Me.lbAr_confez2.Name = "lbAr_confez2"
    Me.lbAr_confez2.NTSDbField = ""
    Me.lbAr_confez2.Size = New System.Drawing.Size(61, 13)
    Me.lbAr_confez2.TabIndex = 18
    Me.lbAr_confez2.Text = "Confezione"
    Me.lbAr_confez2.Tooltip = ""
    Me.lbAr_confez2.UseMnemonic = False
    '
    'edCodart
    '
    Me.edCodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodart.Location = New System.Drawing.Point(149, 11)
    Me.edCodart.Name = "edCodart"
    Me.edCodart.NTSDbField = ""
    Me.edCodart.NTSForzaVisZoom = False
    Me.edCodart.NTSOldValue = ""
    Me.edCodart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodart.Properties.AutoHeight = False
    Me.edCodart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodart.Properties.MaxLength = 65536
    Me.edCodart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodart.Size = New System.Drawing.Size(254, 20)
    Me.edCodart.TabIndex = 13
    '
    'fmCheck
    '
    Me.fmCheck.AllowDrop = True
    Me.fmCheck.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmCheck.Appearance.Options.UseBackColor = True
    Me.fmCheck.Controls.Add(Me.ckArtmaga)
    Me.fmCheck.Controls.Add(Me.ckValoriAttributi)
    Me.fmCheck.Controls.Add(Me.ckArtfasi)
    Me.fmCheck.Controls.Add(Me.ckArtval)
    Me.fmCheck.Controls.Add(Me.ckKit)
    Me.fmCheck.Controls.Add(Me.ckListini)
    Me.fmCheck.Controls.Add(Me.ckConai)
    Me.fmCheck.Controls.Add(Me.ckSconti)
    Me.fmCheck.Controls.Add(Me.ckProvvigioni)
    Me.fmCheck.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmCheck.Location = New System.Drawing.Point(12, 40)
    Me.fmCheck.Name = "fmCheck"
    Me.fmCheck.Size = New System.Drawing.Size(244, 173)
    Me.fmCheck.TabIndex = 675
    Me.fmCheck.Text = "OPZIONI"
    '
    'ckValoriAttributi
    '
    Me.ckValoriAttributi.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckValoriAttributi.Location = New System.Drawing.Point(108, 26)
    Me.ckValoriAttributi.Name = "ckValoriAttributi"
    Me.ckValoriAttributi.NTSCheckValue = "S"
    Me.ckValoriAttributi.NTSUnCheckValue = "N"
    Me.ckValoriAttributi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckValoriAttributi.Properties.Appearance.Options.UseBackColor = True
    Me.ckValoriAttributi.Properties.AutoHeight = False
    Me.ckValoriAttributi.Properties.Caption = "Duplica valori attributi"
    Me.ckValoriAttributi.Size = New System.Drawing.Size(136, 19)
    Me.ckValoriAttributi.TabIndex = 674
    '
    'ckArtfasi
    '
    Me.ckArtfasi.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckArtfasi.Location = New System.Drawing.Point(12, 146)
    Me.ckArtfasi.Name = "ckArtfasi"
    Me.ckArtfasi.NTSCheckValue = "S"
    Me.ckArtfasi.NTSUnCheckValue = "N"
    Me.ckArtfasi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckArtfasi.Properties.Appearance.Options.UseBackColor = True
    Me.ckArtfasi.Properties.AutoHeight = False
    Me.ckArtfasi.Properties.Caption = "Duplica fa&si articolo"
    Me.ckArtfasi.Size = New System.Drawing.Size(114, 19)
    Me.ckArtfasi.TabIndex = 673
    '
    'ckArtval
    '
    Me.ckArtval.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckArtval.Location = New System.Drawing.Point(12, 86)
    Me.ckArtval.Name = "ckArtval"
    Me.ckArtval.NTSCheckValue = "S"
    Me.ckArtval.NTSUnCheckValue = "N"
    Me.ckArtval.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckArtval.Properties.Appearance.Options.UseBackColor = True
    Me.ckArtval.Properties.AutoHeight = False
    Me.ckArtval.Properties.Caption = "Duplica &descrizioni in lingua"
    Me.ckArtval.Size = New System.Drawing.Size(153, 19)
    Me.ckArtval.TabIndex = 667
    '
    'ckKit
    '
    Me.ckKit.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckKit.Location = New System.Drawing.Point(12, 106)
    Me.ckKit.Name = "ckKit"
    Me.ckKit.NTSCheckValue = "S"
    Me.ckKit.NTSUnCheckValue = "N"
    Me.ckKit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckKit.Properties.Appearance.Options.UseBackColor = True
    Me.ckKit.Properties.AutoHeight = False
    Me.ckKit.Properties.Caption = "Duplica figli del &KIT"
    Me.ckKit.Size = New System.Drawing.Size(114, 19)
    Me.ckKit.TabIndex = 668
    '
    'ckListini
    '
    Me.ckListini.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckListini.Location = New System.Drawing.Point(12, 26)
    Me.ckListini.Name = "ckListini"
    Me.ckListini.NTSCheckValue = "S"
    Me.ckListini.NTSUnCheckValue = "N"
    Me.ckListini.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckListini.Properties.Appearance.Options.UseBackColor = True
    Me.ckListini.Properties.AutoHeight = False
    Me.ckListini.Properties.Caption = "Duplica listini"
    Me.ckListini.Size = New System.Drawing.Size(90, 19)
    Me.ckListini.TabIndex = 670
    '
    'ckConai
    '
    Me.ckConai.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckConai.Location = New System.Drawing.Point(12, 126)
    Me.ckConai.Name = "ckConai"
    Me.ckConai.NTSCheckValue = "S"
    Me.ckConai.NTSUnCheckValue = "N"
    Me.ckConai.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckConai.Properties.Appearance.Options.UseBackColor = True
    Me.ckConai.Properties.AutoHeight = False
    Me.ckConai.Properties.Caption = "Duplica &figli del CONAI"
    Me.ckConai.Size = New System.Drawing.Size(129, 19)
    Me.ckConai.TabIndex = 669
    '
    'ckSconti
    '
    Me.ckSconti.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSconti.Location = New System.Drawing.Point(12, 46)
    Me.ckSconti.Name = "ckSconti"
    Me.ckSconti.NTSCheckValue = "S"
    Me.ckSconti.NTSUnCheckValue = "N"
    Me.ckSconti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSconti.Properties.Appearance.Options.UseBackColor = True
    Me.ckSconti.Properties.AutoHeight = False
    Me.ckSconti.Properties.Caption = "Duplica sconti"
    Me.ckSconti.Size = New System.Drawing.Size(90, 19)
    Me.ckSconti.TabIndex = 671
    '
    'ckProvvigioni
    '
    Me.ckProvvigioni.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckProvvigioni.Location = New System.Drawing.Point(12, 66)
    Me.ckProvvigioni.Name = "ckProvvigioni"
    Me.ckProvvigioni.NTSCheckValue = "S"
    Me.ckProvvigioni.NTSUnCheckValue = "N"
    Me.ckProvvigioni.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckProvvigioni.Properties.Appearance.Options.UseBackColor = True
    Me.ckProvvigioni.Properties.AutoHeight = False
    Me.ckProvvigioni.Properties.Caption = "Duplica provvigioni"
    Me.ckProvvigioni.Size = New System.Drawing.Size(114, 19)
    Me.ckProvvigioni.TabIndex = 672
    '
    'lbCodart
    '
    Me.lbCodart.AutoSize = True
    Me.lbCodart.BackColor = System.Drawing.Color.Transparent
    Me.lbCodart.Location = New System.Drawing.Point(13, 14)
    Me.lbCodart.Name = "lbCodart"
    Me.lbCodart.NTSDbField = ""
    Me.lbCodart.Size = New System.Drawing.Size(110, 13)
    Me.lbCodart.TabIndex = 582
    Me.lbCodart.Text = "Codice nuovo articolo"
    Me.lbCodart.Tooltip = ""
    Me.lbCodart.UseMnemonic = False
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(475, 39)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(136, 26)
    Me.cmdAnnulla.TabIndex = 10
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImagePath = ""
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(475, 11)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.NTSContextMenu = Nothing
    Me.cmdOk.Size = New System.Drawing.Size(136, 26)
    Me.cmdOk.TabIndex = 11
    Me.cmdOk.Text = "&Conferma"
    '
    'ckArtmaga
    '
    Me.ckArtmaga.Location = New System.Drawing.Point(108, 46)
    Me.ckArtmaga.Name = "ckArtmaga"
    Me.ckArtmaga.NTSCheckValue = "S"
    Me.ckArtmaga.NTSUnCheckValue = "N"
    Me.ckArtmaga.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckArtmaga.Properties.Appearance.Options.UseBackColor = True
    Me.ckArtmaga.Properties.AutoHeight = False
    Me.ckArtmaga.Properties.Caption = "Duplica articoli\magaz."
    Me.ckArtmaga.Size = New System.Drawing.Size(131, 19)
    Me.ckArtmaga.TabIndex = 675
    '
    'FRMMGDUAR
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(623, 475)
    Me.Controls.Add(Me.pnNuovo)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.HelpContext = ""
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMMGDUAR"
    Me.Text = "DUPLICAZIONE ARTICOLO"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnNuovo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnNuovo.ResumeLayout(False)
    Me.pnNuovo.PerformLayout()
    CType(Me.fmNuovoArt, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmNuovoArt.ResumeLayout(False)
    Me.fmNuovoArt.PerformLayout()
    CType(Me.edAr_formula.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_codtipa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_perqta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_geslotti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_gestmatr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_gesubic.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_gescomm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_gesfasi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmUnitamisura, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmUnitamisura.ResumeLayout(False)
    Me.fmUnitamisura.PerformLayout()
    CType(Me.edAr_um4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_unmis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_confez2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_unmis2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmCheck, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmCheck.ResumeLayout(False)
    Me.fmCheck.PerformLayout()
    CType(Me.ckValoriAttributi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckArtfasi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckArtval.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckKit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckListini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckConai.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSconti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckProvvigioni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckArtmaga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      ckArtfasi.NTSSetParam(oMenu, oApp.Tr(Me, 128560093887272501, "Duplica fa&si articolo"), "S", "N")
      ckArtval.NTSSetParam(oMenu, oApp.Tr(Me, 128560093887428691, "Duplica &descrizioni in lingua"), "S", "N")
      ckKit.NTSSetParam(oMenu, oApp.Tr(Me, 128560093887584881, "Duplica figli del &Kit"), "S", "N")
      ckListini.NTSSetParam(oMenu, oApp.Tr(Me, 128560093887741071, "Duplica listini"), "S", "N")
      ckConai.NTSSetParam(oMenu, oApp.Tr(Me, 128560093887897261, "Duplica &figli del Conai"), "S", "N")
      ckSconti.NTSSetParam(oMenu, oApp.Tr(Me, 128560093888053451, "Duplica sconti"), "S", "N")
      ckProvvigioni.NTSSetParam(oMenu, oApp.Tr(Me, 128560093888209641, "Duplica provvigioni"), "S", "N")
      ckArtmaga.NTSSetParam(oMenu, oApp.Tr(Me, 131012357955405998, "Duplica articoli\magazzino"), "S", "N")
      edCodart.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788390849006481, "Codice nuovo articolo"), tabartico, False)
      cbAr_codtipa.NTSSetParam(oApp.Tr(Me, 130385782165766653, "Tipologia art."))
      edAr_perqta.NTSSetParam(oMenu, oApp.Tr(Me, 130385780687172903, "Molt.qtà/prezzo:"), "0", 12, 0, 999999999999)
      ckAr_geslotti.NTSSetParam(oMenu, oApp.Tr(Me, 128849554806877432, "Gestione Lotti"), "S", "N")
      ckAr_gestmatr.NTSSetParam(oMenu, oApp.Tr(Me, 128849554807033710, "Gestione matricole"), "S", "N")
      ckAr_gesubic.NTSSetParam(oMenu, oApp.Tr(Me, 128849554807346266, "Gestione ubicazione"), "S", "N")
      cbAr_gescomm.NTSSetParam(oApp.Tr(Me, 130385780202485403, "Gestione per Comm."))
      ckAr_gesfasi.NTSSetParam(oMenu, oApp.Tr(Me, 128849554807658822, "Gestione fasi"), "S", "N")
      edAr_unmis.NTSSetParam(oMenu, oApp.Tr(Me, 130385777942641653, "Un. misura principale"), 3, True)
      edAr_confez2.NTSSetParam(oMenu, oApp.Tr(Me, 130385779638891653, "Un. misura confezione:"), 3, True)
      edAr_unmis2.NTSSetParam(oMenu, oApp.Tr(Me, 130385778877954153, "Un. misura secondaria:"), 3, True)
      edAr_um4.NTSSetParam(oMenu, oApp.Tr(Me, 129622100117138672, "U.M. formula"), 3, True)
      edAr_formula.NTSSetParam(oMenu, oApp.Tr(Me, 129622135833916016, "Formula di trasformazione in UMP"), 0, True)
      ckValoriAttributi.NTSSetParam(oMenu, oApp.Tr(Me, 130250878438850248, "Duplica valori attributi"), "S", "N")

      If oCleArti.bGestTabUnmis = True Then
        edAr_unmis.NTSSetParamZoom("ZOOMTABUMIS")
        edAr_confez2.NTSSetParamZoom("ZOOMTABUMIS")
        edAr_unmis2.NTSSetParamZoom("ZOOMTABUMIS")
        edAr_um4.NTSSetParamZoom("ZOOMTABUMIS")
      End If
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
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRMMGDUAR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim dttAr_codtipa As New DataTable()
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Try
      AddHandler oCleArti.RemoteEvent, AddressOf GestisciEventiEntity

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()
      CaricaRecent()

      bOk = False

      CaricaCombo()

      '----------------------------------------------------------------------------------------
      '--- Se non esiste almeno la tipologia 1 chiude
      '----------------------------------------------------------------------------------------
      If Not oCleArti.GetTabTipa1() Then Me.Close()
      '----------------------------------------------------------------------------------------
      oCleArti.GetTabTipa(dsTmp)

      dttAr_codtipa.Columns.Add("cod", GetType(String))
      dttAr_codtipa.Columns.Add("val", GetType(String))

      For i = 0 To dsTmp.Tables("TABTIPA").Rows.Count - 1
        dttAr_codtipa.Rows.Add(New Object() {NTSCStr(dsTmp.Tables("TABTIPA").Rows(i)!tb_codtipa), NTSCStr(dsTmp.Tables("TABTIPA").Rows(i)!tb_destipa)})
        dttAr_codtipa.AcceptChanges()
      Next

      cbAr_codtipa.DataSource = dttAr_codtipa
      cbAr_codtipa.ValueMember = "cod"
      cbAr_codtipa.DisplayMember = "val"

      '----------------------------------------------------
      oCleArti.strGeneraCodice = oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "GeneraCodice", "0", " ", "0")
      oCleArti.nLungRoot = NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "GeneraCodiceLungRoot", "0", " ", "0"))
      oCleArti.nLungExt = NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "GeneraCodiceLungExt", "0", " ", "0"))
      gbDuarArticoloGenerato = False
      lProgr = 0
      glDuarProgr = 0
      gstrDuarRoot = ""

      '--------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
      GctlApplicaDefaultValue()

      ckListini.Checked = oCleArti.bDuplicaListini OrElse (edCodart.Text.Trim <> "")
      ckSconti.Checked = oCleArti.bDuplicaSconti OrElse (edCodart.Text.Trim <> "")
      ckProvvigioni.Checked = oCleArti.bDuplicaProvvigioni OrElse (edCodart.Text.Trim <> "")
      ckArtval.Checked = oCleArti.bDuplicaDescrLingua
      ckKit.Checked = oCleArti.bDuplicaKit
      ckConai.Checked = oCleArti.bDuplicaConai
      ckArtfasi.Checked = oCleArti.bDuplicaFasi

      If edCodart.Text.Trim <> "" Then ckValoriAttributi.Checked = True

      oCleArti.GetUnitaMisDuar()
      If Not oCleArti.dtrTmpDuar Is Nothing Then
        edAr_unmis.Text = NTSCStr(oCleArti.dtrTmpDuar!ar_unmis)
        edAr_confez2.Text = NTSCStr(oCleArti.dtrTmpDuar!ar_confez2)
        edAr_unmis2.Text = NTSCStr(oCleArti.dtrTmpDuar!ar_unmis2)
        edAr_um4.Text = NTSCStr(oCleArti.dtrTmpDuar!ar_um4)
        edAr_formula.Text = NTSCStr(oCleArti.dtrTmpDuar!ar_formula)
        edAr_perqta.Text = NTSCStr(oCleArti.dtrTmpDuar!ar_perqta)
        cbAr_codtipa.SelectedValue = NTSCStr(oCleArti.dtrTmpDuar!ar_codtipa)
        ckAr_gesfasi.Checked = CBool(IIf(NTSCStr(oCleArti.dtrTmpDuar!ar_gesfasi) = "S", True, False))
        ckAr_gesubic.Checked = CBool(IIf(NTSCStr(oCleArti.dtrTmpDuar!ar_gesubic) = "S", True, False))
        ckAr_geslotti.Checked = CBool(IIf(NTSCStr(oCleArti.dtrTmpDuar!ar_geslotti) = "S", True, False))
        ckAr_gestmatr.Checked = CBool(IIf(NTSCStr(oCleArti.dtrTmpDuar!ar_gestmatr) = "S", True, False))
        cbAr_gescomm.SelectedValue = NTSCStr(oCleArti.dtrTmpDuar!ar_gescomm)

        'GIFT CARD
        If NTSCStr(oCleArti.dtrTmpDuar!ar_flgift) = "S" Then
          fmCheck.Enabled = False
          fmNuovoArt.Enabled = False
        End If
      End If

      If ckAr_gesfasi.Checked = True Then
        'se l'articolo di origine è a fasi è obbligatorio che anche quello di destinazione lo sia
        GctlSetVisEnab(ckAr_gesfasi, False)
      Else
        If oCleArti.bLogisticaEstesa = False Then
          'disabilito
          ckAr_gesfasi.Enabled = False
        Else
          'abilito (posso cioè creare un articolo a fase da uno NON a fasi)
          If ckAr_gesfasi.Enabled = False Then GctlSetVisEnab(ckAr_gesfasi, False)
        End If
      End If
      If oCleArti.bLogisticaEstesa Or oCleArti.bGestUbicSenzaLext Then
        'c'è il modulo LEXT o la opz. di sblocco, quindi il flag è accessbile
        If ckAr_gesubic.Enabled = False Then GctlSetVisEnab(ckAr_gesubic, False)
      Else
        'inibito
        ckAr_gesubic.Enabled = False
      End If

      '-----------------------------------------------------------------------------------------
      '--- Se è un articolo a fasi NON è duplicabile senza logistica estesa
      '-----------------------------------------------------------------------------------------
      If oCleArti.bLogisticaEstesa = False Then
        If ckAr_gesfasi.Checked = True Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128987722305774646, "Articolo a fasi non duplicabile in quanto non è abilitato il modulo Logistica estesa."))
          bOk = False
          Me.Close()
        End If
      End If
      '-----------------------------------------------------------------------------------------
      '--- Se è un articolo a ubicazioni NON è duplicabile senza logistica estesa (od opzione di sblocco GestUbicSenzaLext
      '-----------------------------------------------------------------------------------------
      If oCleArti.bLogisticaEstesa = False Then
        If oCleArti.bGestUbicSenzaLext = False Then
          If ckAr_gesubic.Checked = True Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128987722563261814, "Articolo a ubicazioni non duplicabile in quanto non è abilitato il modulo Logistica estesa."))
            bOk = False
            Me.Close()
          End If
        End If
      End If

      bOnLoading = True
      dPerqtaOrigine = NTSCDec(edAr_perqta.Text)
      If dPerqtaOrigine = 0 Then dPerqtaOrigine = 1
      If ckListini.Checked = True Then edAr_perqta.Enabled = False
      bOnLoading = False

      If edCodart.Text.Trim <> "" Then cmdOk_Click(Me, Nothing)
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMMGDUAR_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Dim ctrlTmp As Control = Nothing
    Try
      '--------------------------------------------
      'gestione dello zoom:
      'eseguo la Zoom, tanto se per il controllo non deve venir eseguito uno zoom particolare, la routine non fa nulla e viene processato lo zoom standard del controllo, settato con la NTSSetParamZoom
      If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
        Zoom()
        e.Handled = True    'altrimenti anche il controllo riceve l'F5 e la routine ZOOM viene eseguita 2 volte!!!
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGDUAR_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      SalvaRecent()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Zoom / ALT+F3 / ALT+F3"
  Public Overridable Sub Zoom()
    Dim ctlLastControl As Control
    Dim ctrlTmp As Control = Nothing
    Dim oParam As New CLE__PATB

    Try
      ctlLastControl = NTSFindControlFocused(Me)
      If ctlLastControl Is Nothing Then Return

      '------------------------------------
      'zoom standard di textbox e griglia
      'SendKeys.SendWait("{F5}") 'se faccio questa riga va in un loop infinito....
      NTSCallStandardZoom()

      '------------------------------------
      'riporto il focus sul controllo che ha chiamato lo zoom
      ctlLastControl.Focus()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Validazione articoli"
  Public Overridable Sub edCodart_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCodart.Validated
    Try
      If Not CheckCodartDuar() Then Exit Sub
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi Controlii"
  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      edCodart.Text = UCase(edCodart.Text).TrimEnd
      If Not CheckCodartDuar() Then Exit Sub
      If CheckFormula() = False Then Return
      If Not oCleArti.CheckArticoloDuar(edCodart.Text, edAr_unmis.Text, _
                                        edAr_confez2.Text, edAr_unmis2.Text, _
                                        edAr_perqta.Text, cbAr_codtipa.SelectedValue, _
                                        NTSCStr(IIf(ckAr_gesfasi.Checked, "S", "N")), NTSCStr(IIf(ckAr_gesubic.Checked, "S", "N")), _
                                        NTSCStr(IIf(ckAr_geslotti.Checked, "S", "N")), NTSCStr(IIf(ckAr_gestmatr.Checked, "S", "N")), _
                                        cbAr_gescomm.SelectedValue, edAr_um4.Text, edAr_formula.Text) Then
        Exit Sub
      Else
        oCleArti.strCodartDuar = edCodart.Text
        bOk = True
        Me.Close()
      End If

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      bOk = False
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub


  Public Overridable Sub edAr_unmis_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edAr_unmis.Validated
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleArti.bGestTabUnmis = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      If edAr_unmis.Text = "" Then Return
      '--------------------------------------------------------------------------------------------------------------
      If oMenu.ValCodiceDb(edAr_unmis.Text, DittaCorrente, "TABUMIS", "S", "", dttTmp) = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129428391125683594, "Attenzione!" & vbCrLf & _
          "Unità di misura Principale non valida."))
        edAr_unmis.Text = ""
        Return
      Else
        If NTSCStr(dttTmp.Rows(0)!tb_codumis) <> edAr_unmis.Text Then edAr_unmis.Text = NTSCStr(dttTmp.Rows(0)!tb_codumis)
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Sub

  Public Overridable Sub edAr_confez2_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edAr_confez2.Validated
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleArti.bGestTabUnmis = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      If edAr_confez2.Text = "" Then Return
      '--------------------------------------------------------------------------------------------------------------
      If oMenu.ValCodiceDb(edAr_confez2.Text, DittaCorrente, "TABUMIS", "S", "", dttTmp) = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129428395058203125, "Attenzione!" & vbCrLf & _
          "Unità di misura Confezione non valida."))
        edAr_confez2.Text = ""
        Return
      Else
        If NTSCStr(dttTmp.Rows(0)!tb_codumis) <> edAr_confez2.Text Then edAr_confez2.Text = NTSCStr(dttTmp.Rows(0)!tb_codumis)
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Sub

  Public Overridable Sub edAr_unmis2_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edAr_unmis2.Validated
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleArti.bGestTabUnmis = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      If edAr_unmis2.Text = "" Then Return
      '--------------------------------------------------------------------------------------------------------------
      If oMenu.ValCodiceDb(edAr_unmis2.Text, DittaCorrente, "TABUMIS", "S", "", dttTmp) = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129428395516464844, "Attenzione!" & vbCrLf & _
          "Unità di misura Secondaria non valida."))
        edAr_unmis2.Text = ""
        Return
      Else
        If NTSCStr(dttTmp.Rows(0)!tb_codumis) <> edAr_unmis2.Text Then edAr_unmis2.Text = NTSCStr(dttTmp.Rows(0)!tb_codumis)
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Sub

  Public Overridable Sub edAr_um4_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edAr_um4.Validated
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleArti.bGestTabUnmis = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      If edAr_um4.Text = "" Then
        edAr_formula.Text = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oMenu.ValCodiceDb(edAr_um4.Text, DittaCorrente, "TABUMIS", "S", "", dttTmp) = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129622113944404297, "Attenzione!" & vbCrLf & _
          "Unità di misura Formula non valida."))
        edAr_um4.Text = ""
        edAr_formula.Text = ""
        Return
      Else
        If NTSCStr(dttTmp.Rows(0)!tb_codumis) <> edAr_um4.Text Then edAr_um4.Text = NTSCStr(dttTmp.Rows(0)!tb_codumis)
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Sub

  Public Overridable Sub edAr_formula_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edAr_formula.Validated
    Try
      '--------------------------------------------------------------------------------------------------------------      
      If CheckFormula() = False Then Return
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub ckListini_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckListini.CheckedChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bOnLoading = True Then Return
      '--------------------------------------------------------------------------------------------------------------
      If ckListini.Checked = True Then
        edAr_perqta.Text = dPerqtaOrigine.ToString
        edAr_perqta.Enabled = False
      Else
        GctlSetVisEnab(edAr_perqta, False)
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Functions"
  Public Overridable Function CheckCodartDuar() As Boolean
    Try
      Return oCleArti.CheckCodartDuar(edCodart.Text)

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub CaricaCombo()
    Dim dttAr_gescomm As New DataTable()
    Try
      dttAr_gescomm.Columns.Add("cod", GetType(String))
      dttAr_gescomm.Columns.Add("val", GetType(String))
      dttAr_gescomm.Rows.Add(New Object() {"N", "No"})
      dttAr_gescomm.Rows.Add(New Object() {"O", "Ordini/Impegni"})
      dttAr_gescomm.Rows.Add(New Object() {"S", "Ordini/Imp./Magaz."})
      dttAr_gescomm.AcceptChanges()

      cbAr_gescomm.DataSource = dttAr_gescomm
      cbAr_gescomm.ValueMember = "cod"
      cbAr_gescomm.DisplayMember = "val"

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function CheckFormula() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------      
      If edAr_formula.Text <> "" Then
        If edAr_um4.Text = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129622138656933594, "Attenzione!" & vbCrLf & _
            "Se indicata la '|" & lbAr_formula.Text & "|'" & vbCrLf & _
            "la '|" & lbAr_um4.Text & "|' è obbligatoria."))
          edAr_formula.Text = ""
          Return False
        End If
      Else
        If edAr_um4.Text <> "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129622141539697266, "Attenzione!" & vbCrLf & _
            "Se indicata la '|" & lbAr_um4.Text & "|'" & vbCrLf & _
            "la '|" & lbAr_formula.Text & "|' è obbligatoria."))
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Sub SalvaRecent()
    Dim strChecked As String = "0"
    Try
      If ckValoriAttributi.Checked Then
        strChecked = "-1"
      End If
      oMenu.SaveSettingBus("BSMGARTI", "RECENT", ".", "DuplicaValoriAttributi", strChecked, " ", "NS.", "...", "...")
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub CaricaRecent()
    Try
      ckValoriAttributi.Checked = CBool(oMenu.GetSettingBus("BSMGARTI", "RECENT", ".", "DuplicaValoriAttributi", "0", " ", "0"))
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

#End Region
End Class