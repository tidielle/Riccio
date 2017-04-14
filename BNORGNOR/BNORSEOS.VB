Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORSEOS
  Public oCallParams As CLE__CLDP
  Public bOk As Boolean
  Public oCleGnor As CLEORGNOR
  Public bDisabilitaCheckAnnoData As Boolean = False

  Public WithEvents edNumero As NTSInformatica.NTSTextBoxNum
  Public WithEvents edSerie As NTSInformatica.NTSTextBoxStr
  Public WithEvents cbTipo As NTSInformatica.NTSComboBox
  Public WithEvents edAnno As NTSInformatica.NTSTextBoxNum
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents lbNumero As NTSInformatica.NTSLabel
  Public WithEvents lbSerie As NTSInformatica.NTSLabel
  Public WithEvents lbAnno As NTSInformatica.NTSLabel
  Public WithEvents lbTipo As NTSInformatica.NTSLabel
  Public WithEvents lbAnnoTcLabel As NTSInformatica.NTSLabel
  Public WithEvents edAnnoTc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbStagioneLabel As NTSInformatica.NTSLabel
  Public WithEvents edStagione As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbStagione As NTSInformatica.NTSLabel
  Public WithEvents edDatdoc As NTSInformatica.NTSTextBoxData
  Public WithEvents lbDatdoc As NTSInformatica.NTSLabel
  Public WithEvents ckOpInterni As NTSInformatica.NTSCheckBox
  Public WithEvents lbTipobf As NTSInformatica.NTSLabel
  Public WithEvents lbTipobfLabel As NTSInformatica.NTSLabel
  Public WithEvents edTipobf As NTSInformatica.NTSTextBoxNum
  Public WithEvents pnConfermaFocus As NTSInformatica.NTSPanel
  Public WithEvents ckPrivilegiaTipoBF_CF As NTSInformatica.NTSCheckBox
  Public WithEvents pnOrdine As NTSInformatica.NTSPanel


  Public Overridable Sub InitializeComponent()
    Me.edNumero = New NTSInformatica.NTSTextBoxNum
    Me.edSerie = New NTSInformatica.NTSTextBoxStr
    Me.cbTipo = New NTSInformatica.NTSComboBox
    Me.edAnno = New NTSInformatica.NTSTextBoxNum
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.lbNumero = New NTSInformatica.NTSLabel
    Me.lbSerie = New NTSInformatica.NTSLabel
    Me.lbAnno = New NTSInformatica.NTSLabel
    Me.lbTipo = New NTSInformatica.NTSLabel
    Me.pnOrdine = New NTSInformatica.NTSPanel
    Me.ckPrivilegiaTipoBF_CF = New NTSInformatica.NTSCheckBox
    Me.lbTipobf = New NTSInformatica.NTSLabel
    Me.lbStagione = New NTSInformatica.NTSLabel
    Me.lbTipobfLabel = New NTSInformatica.NTSLabel
    Me.lbStagioneLabel = New NTSInformatica.NTSLabel
    Me.edTipobf = New NTSInformatica.NTSTextBoxNum
    Me.edStagione = New NTSInformatica.NTSTextBoxNum
    Me.ckOpInterni = New NTSInformatica.NTSCheckBox
    Me.lbAnnoTcLabel = New NTSInformatica.NTSLabel
    Me.edAnnoTc = New NTSInformatica.NTSTextBoxNum
    Me.edDatdoc = New NTSInformatica.NTSTextBoxData
    Me.lbDatdoc = New NTSInformatica.NTSLabel
    Me.pnConfermaFocus = New NTSInformatica.NTSPanel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumero.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnOrdine, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnOrdine.SuspendLayout()
    CType(Me.ckPrivilegiaTipoBF_CF.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTipobf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edStagione.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckOpInterni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnnoTc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatdoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnConfermaFocus, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnConfermaFocus.SuspendLayout()
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
    'edNumero
    '
    Me.edNumero.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumero.EditValue = "0"
    Me.edNumero.Location = New System.Drawing.Point(74, 48)
    Me.edNumero.Name = "edNumero"
    Me.edNumero.NTSDbField = ""
    Me.edNumero.NTSFormat = "0"
    Me.edNumero.NTSForzaVisZoom = False
    Me.edNumero.NTSOldValue = ""
    Me.edNumero.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edNumero.Properties.Appearance.Options.UseBackColor = True
    Me.edNumero.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumero.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumero.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNumero.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNumero.Properties.AutoHeight = False
    Me.edNumero.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumero.Properties.MaxLength = 9
    Me.edNumero.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumero.Size = New System.Drawing.Size(97, 20)
    Me.edNumero.TabIndex = 8
    '
    'edSerie
    '
    Me.edSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSerie.EditValue = " "
    Me.edSerie.Location = New System.Drawing.Point(245, 27)
    Me.edSerie.Name = "edSerie"
    Me.edSerie.NTSDbField = ""
    Me.edSerie.NTSForzaVisZoom = False
    Me.edSerie.NTSOldValue = " "
    Me.edSerie.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edSerie.Properties.Appearance.Options.UseBackColor = True
    Me.edSerie.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSerie.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSerie.Properties.AutoHeight = False
    Me.edSerie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSerie.Properties.MaxLength = 1
    Me.edSerie.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSerie.Size = New System.Drawing.Size(45, 20)
    Me.edSerie.TabIndex = 0
    '
    'cbTipo
    '
    Me.cbTipo.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipo.DataSource = Nothing
    Me.cbTipo.DisplayMember = ""
    Me.cbTipo.Location = New System.Drawing.Point(74, 4)
    Me.cbTipo.Name = "cbTipo"
    Me.cbTipo.NTSDbField = ""
    Me.cbTipo.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTipo.Properties.Appearance.Options.UseBackColor = True
    Me.cbTipo.Properties.AutoHeight = False
    Me.cbTipo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipo.Properties.DropDownRows = 30
    Me.cbTipo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipo.SelectedValue = ""
    Me.cbTipo.Size = New System.Drawing.Size(222, 20)
    Me.cbTipo.TabIndex = 0
    Me.cbTipo.ValueMember = ""
    '
    'edAnno
    '
    Me.edAnno.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnno.EditValue = "2008"
    Me.edAnno.Location = New System.Drawing.Point(74, 26)
    Me.edAnno.Name = "edAnno"
    Me.edAnno.NTSDbField = ""
    Me.edAnno.NTSFormat = "0"
    Me.edAnno.NTSForzaVisZoom = False
    Me.edAnno.NTSOldValue = "2008"
    Me.edAnno.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAnno.Properties.Appearance.Options.UseBackColor = True
    Me.edAnno.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnno.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnno.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnno.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnno.Properties.AutoHeight = False
    Me.edAnno.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnno.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnno.Size = New System.Drawing.Size(53, 20)
    Me.edAnno.TabIndex = 0
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(3, 54)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(116, 22)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdOk.ImagePath = ""
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(3, 25)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.NTSContextMenu = Nothing
    Me.cmdOk.Size = New System.Drawing.Size(116, 23)
    Me.cmdOk.TabIndex = 2
    Me.cmdOk.Text = "&Conferma"
    '
    'lbNumero
    '
    Me.lbNumero.AutoSize = True
    Me.lbNumero.BackColor = System.Drawing.Color.Transparent
    Me.lbNumero.Location = New System.Drawing.Point(10, 51)
    Me.lbNumero.Name = "lbNumero"
    Me.lbNumero.NTSDbField = ""
    Me.lbNumero.Size = New System.Drawing.Size(44, 13)
    Me.lbNumero.TabIndex = 3
    Me.lbNumero.Text = "Numero"
    Me.lbNumero.Tooltip = ""
    Me.lbNumero.UseMnemonic = False
    '
    'lbSerie
    '
    Me.lbSerie.AutoSize = True
    Me.lbSerie.BackColor = System.Drawing.Color.Transparent
    Me.lbSerie.Location = New System.Drawing.Point(203, 30)
    Me.lbSerie.Name = "lbSerie"
    Me.lbSerie.NTSDbField = ""
    Me.lbSerie.Size = New System.Drawing.Size(31, 13)
    Me.lbSerie.TabIndex = 4
    Me.lbSerie.Text = "Serie"
    Me.lbSerie.Tooltip = ""
    Me.lbSerie.UseMnemonic = False
    '
    'lbAnno
    '
    Me.lbAnno.AutoSize = True
    Me.lbAnno.BackColor = System.Drawing.Color.Transparent
    Me.lbAnno.Location = New System.Drawing.Point(10, 29)
    Me.lbAnno.Name = "lbAnno"
    Me.lbAnno.NTSDbField = ""
    Me.lbAnno.Size = New System.Drawing.Size(32, 13)
    Me.lbAnno.TabIndex = 5
    Me.lbAnno.Text = "Anno"
    Me.lbAnno.Tooltip = ""
    Me.lbAnno.UseMnemonic = False
    '
    'lbTipo
    '
    Me.lbTipo.AutoSize = True
    Me.lbTipo.BackColor = System.Drawing.Color.Transparent
    Me.lbTipo.Location = New System.Drawing.Point(10, 7)
    Me.lbTipo.Name = "lbTipo"
    Me.lbTipo.NTSDbField = ""
    Me.lbTipo.Size = New System.Drawing.Size(27, 13)
    Me.lbTipo.TabIndex = 6
    Me.lbTipo.Text = "Tipo"
    Me.lbTipo.Tooltip = ""
    Me.lbTipo.UseMnemonic = False
    '
    'pnOrdine
    '
    Me.pnOrdine.AllowDrop = True
    Me.pnOrdine.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnOrdine.Appearance.Options.UseBackColor = True
    Me.pnOrdine.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnOrdine.Controls.Add(Me.ckPrivilegiaTipoBF_CF)
    Me.pnOrdine.Controls.Add(Me.lbTipobf)
    Me.pnOrdine.Controls.Add(Me.lbStagione)
    Me.pnOrdine.Controls.Add(Me.lbTipobfLabel)
    Me.pnOrdine.Controls.Add(Me.lbStagioneLabel)
    Me.pnOrdine.Controls.Add(Me.edTipobf)
    Me.pnOrdine.Controls.Add(Me.edStagione)
    Me.pnOrdine.Controls.Add(Me.ckOpInterni)
    Me.pnOrdine.Controls.Add(Me.lbAnnoTcLabel)
    Me.pnOrdine.Controls.Add(Me.edAnnoTc)
    Me.pnOrdine.Controls.Add(Me.edDatdoc)
    Me.pnOrdine.Controls.Add(Me.lbDatdoc)
    Me.pnOrdine.Controls.Add(Me.lbTipo)
    Me.pnOrdine.Controls.Add(Me.edAnno)
    Me.pnOrdine.Controls.Add(Me.lbAnno)
    Me.pnOrdine.Controls.Add(Me.edSerie)
    Me.pnOrdine.Controls.Add(Me.lbSerie)
    Me.pnOrdine.Controls.Add(Me.cbTipo)
    Me.pnOrdine.Controls.Add(Me.lbNumero)
    Me.pnOrdine.Controls.Add(Me.edNumero)
    Me.pnOrdine.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnOrdine.Location = New System.Drawing.Point(2, 8)
    Me.pnOrdine.Name = "pnOrdine"
    Me.pnOrdine.NTSActiveTrasparency = True
    Me.pnOrdine.Size = New System.Drawing.Size(472, 193)
    Me.pnOrdine.TabIndex = 9
    Me.pnOrdine.Text = "NtsPanel1"
    '
    'ckPrivilegiaTipoBF_CF
    '
    Me.ckPrivilegiaTipoBF_CF.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckPrivilegiaTipoBF_CF.Location = New System.Drawing.Point(13, 114)
    Me.ckPrivilegiaTipoBF_CF.Name = "ckPrivilegiaTipoBF_CF"
    Me.ckPrivilegiaTipoBF_CF.NTSCheckValue = "S"
    Me.ckPrivilegiaTipoBF_CF.NTSUnCheckValue = "N"
    Me.ckPrivilegiaTipoBF_CF.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPrivilegiaTipoBF_CF.Properties.Appearance.Options.UseBackColor = True
    Me.ckPrivilegiaTipoBF_CF.Properties.AutoHeight = False
    Me.ckPrivilegiaTipoBF_CF.Properties.Caption = "Privilegia tipo bolla/fattura indicato in Anagrafica Clienti/Fornitori"
    Me.ckPrivilegiaTipoBF_CF.Size = New System.Drawing.Size(352, 19)
    Me.ckPrivilegiaTipoBF_CF.TabIndex = 56
    '
    'lbTipobf
    '
    Me.lbTipobf.BackColor = System.Drawing.Color.Transparent
    Me.lbTipobf.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbTipobf.Location = New System.Drawing.Point(133, 92)
    Me.lbTipobf.Name = "lbTipobf"
    Me.lbTipobf.NTSDbField = ""
    Me.lbTipobf.Size = New System.Drawing.Size(336, 20)
    Me.lbTipobf.TabIndex = 54
    Me.lbTipobf.Tooltip = ""
    Me.lbTipobf.UseMnemonic = False
    '
    'lbStagione
    '
    Me.lbStagione.BackColor = System.Drawing.Color.Transparent
    Me.lbStagione.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbStagione.Location = New System.Drawing.Point(133, 161)
    Me.lbStagione.Name = "lbStagione"
    Me.lbStagione.NTSDbField = ""
    Me.lbStagione.Size = New System.Drawing.Size(336, 20)
    Me.lbStagione.TabIndex = 50
    Me.lbStagione.Tooltip = ""
    Me.lbStagione.UseMnemonic = False
    '
    'lbTipobfLabel
    '
    Me.lbTipobfLabel.AutoSize = True
    Me.lbTipobfLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbTipobfLabel.Location = New System.Drawing.Point(10, 95)
    Me.lbTipobfLabel.Name = "lbTipobfLabel"
    Me.lbTipobfLabel.NTSDbField = ""
    Me.lbTipobfLabel.Size = New System.Drawing.Size(40, 13)
    Me.lbTipobfLabel.TabIndex = 52
    Me.lbTipobfLabel.Text = "Tipo Bf"
    Me.lbTipobfLabel.Tooltip = ""
    Me.lbTipobfLabel.UseMnemonic = False
    '
    'lbStagioneLabel
    '
    Me.lbStagioneLabel.AutoSize = True
    Me.lbStagioneLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbStagioneLabel.Location = New System.Drawing.Point(10, 164)
    Me.lbStagioneLabel.Name = "lbStagioneLabel"
    Me.lbStagioneLabel.NTSDbField = ""
    Me.lbStagioneLabel.Size = New System.Drawing.Size(49, 13)
    Me.lbStagioneLabel.TabIndex = 12
    Me.lbStagioneLabel.Text = "Stagione"
    Me.lbStagioneLabel.Tooltip = ""
    Me.lbStagioneLabel.UseMnemonic = False
    '
    'edTipobf
    '
    Me.edTipobf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTipobf.EditValue = "0"
    Me.edTipobf.Location = New System.Drawing.Point(74, 92)
    Me.edTipobf.Name = "edTipobf"
    Me.edTipobf.NTSDbField = ""
    Me.edTipobf.NTSFormat = "0"
    Me.edTipobf.NTSForzaVisZoom = False
    Me.edTipobf.NTSOldValue = ""
    Me.edTipobf.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTipobf.Properties.Appearance.Options.UseBackColor = True
    Me.edTipobf.Properties.Appearance.Options.UseTextOptions = True
    Me.edTipobf.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTipobf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTipobf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTipobf.Properties.AutoHeight = False
    Me.edTipobf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTipobf.Properties.MaxLength = 9
    Me.edTipobf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTipobf.Size = New System.Drawing.Size(53, 20)
    Me.edTipobf.TabIndex = 53
    '
    'edStagione
    '
    Me.edStagione.Cursor = System.Windows.Forms.Cursors.Default
    Me.edStagione.EditValue = "0"
    Me.edStagione.Location = New System.Drawing.Point(74, 161)
    Me.edStagione.Name = "edStagione"
    Me.edStagione.NTSDbField = ""
    Me.edStagione.NTSFormat = "0"
    Me.edStagione.NTSForzaVisZoom = False
    Me.edStagione.NTSOldValue = ""
    Me.edStagione.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edStagione.Properties.Appearance.Options.UseBackColor = True
    Me.edStagione.Properties.Appearance.Options.UseTextOptions = True
    Me.edStagione.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edStagione.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edStagione.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edStagione.Properties.AutoHeight = False
    Me.edStagione.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edStagione.Properties.MaxLength = 9
    Me.edStagione.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edStagione.Size = New System.Drawing.Size(53, 20)
    Me.edStagione.TabIndex = 13
    '
    'ckOpInterni
    '
    Me.ckOpInterni.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckOpInterni.Location = New System.Drawing.Point(188, 70)
    Me.ckOpInterni.Name = "ckOpInterni"
    Me.ckOpInterni.NTSCheckValue = "S"
    Me.ckOpInterni.NTSUnCheckValue = "N"
    Me.ckOpInterni.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckOpInterni.Properties.Appearance.Options.UseBackColor = True
    Me.ckOpInterni.Properties.AutoHeight = False
    Me.ckOpInterni.Properties.Caption = "Ordini &interni di produzione"
    Me.ckOpInterni.Size = New System.Drawing.Size(156, 19)
    Me.ckOpInterni.TabIndex = 11
    '
    'lbAnnoTcLabel
    '
    Me.lbAnnoTcLabel.AutoSize = True
    Me.lbAnnoTcLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbAnnoTcLabel.Location = New System.Drawing.Point(10, 142)
    Me.lbAnnoTcLabel.Name = "lbAnnoTcLabel"
    Me.lbAnnoTcLabel.NTSDbField = ""
    Me.lbAnnoTcLabel.Size = New System.Drawing.Size(56, 13)
    Me.lbAnnoTcLabel.TabIndex = 10
    Me.lbAnnoTcLabel.Text = "Anno (TC)"
    Me.lbAnnoTcLabel.Tooltip = ""
    Me.lbAnnoTcLabel.UseMnemonic = False
    '
    'edAnnoTc
    '
    Me.edAnnoTc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnnoTc.EditValue = "2008"
    Me.edAnnoTc.Location = New System.Drawing.Point(74, 139)
    Me.edAnnoTc.Name = "edAnnoTc"
    Me.edAnnoTc.NTSDbField = ""
    Me.edAnnoTc.NTSFormat = "0"
    Me.edAnnoTc.NTSForzaVisZoom = False
    Me.edAnnoTc.NTSOldValue = "2008"
    Me.edAnnoTc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAnnoTc.Properties.Appearance.Options.UseBackColor = True
    Me.edAnnoTc.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnnoTc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnnoTc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnnoTc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnnoTc.Properties.AutoHeight = False
    Me.edAnnoTc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnnoTc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnnoTc.Size = New System.Drawing.Size(53, 20)
    Me.edAnnoTc.TabIndex = 11
    '
    'edDatdoc
    '
    Me.edDatdoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatdoc.EditValue = "01/01/1900"
    Me.edDatdoc.Location = New System.Drawing.Point(74, 70)
    Me.edDatdoc.Name = "edDatdoc"
    Me.edDatdoc.NTSDbField = ""
    Me.edDatdoc.NTSForzaVisZoom = False
    Me.edDatdoc.NTSOldValue = ""
    Me.edDatdoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatdoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatdoc.Properties.AutoHeight = False
    Me.edDatdoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatdoc.Properties.MaxLength = 65536
    Me.edDatdoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatdoc.Size = New System.Drawing.Size(97, 20)
    Me.edDatdoc.TabIndex = 10
    '
    'lbDatdoc
    '
    Me.lbDatdoc.AutoSize = True
    Me.lbDatdoc.BackColor = System.Drawing.Color.Transparent
    Me.lbDatdoc.Location = New System.Drawing.Point(10, 73)
    Me.lbDatdoc.Name = "lbDatdoc"
    Me.lbDatdoc.NTSDbField = ""
    Me.lbDatdoc.Size = New System.Drawing.Size(30, 13)
    Me.lbDatdoc.TabIndex = 9
    Me.lbDatdoc.Text = "Data"
    Me.lbDatdoc.Tooltip = ""
    Me.lbDatdoc.UseMnemonic = False
    '
    'pnConfermaFocus
    '
    Me.pnConfermaFocus.AllowDrop = True
    Me.pnConfermaFocus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pnConfermaFocus.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnConfermaFocus.Appearance.Options.UseBackColor = True
    Me.pnConfermaFocus.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnConfermaFocus.Controls.Add(Me.cmdOk)
    Me.pnConfermaFocus.Controls.Add(Me.cmdAnnulla)
    Me.pnConfermaFocus.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnConfermaFocus.Location = New System.Drawing.Point(478, 122)
    Me.pnConfermaFocus.Name = "pnConfermaFocus"
    Me.pnConfermaFocus.NTSActiveTrasparency = True
    Me.pnConfermaFocus.Size = New System.Drawing.Size(122, 79)
    Me.pnConfermaFocus.TabIndex = 10
    Me.pnConfermaFocus.Text = "NtsPanel1"
    '
    'FRMORSEOS
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(605, 204)
    Me.Controls.Add(Me.pnConfermaFocus)
    Me.Controls.Add(Me.pnOrdine)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMORSEOS"
    Me.Text = "NUOVO ORDINE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNumero.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnOrdine, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnOrdine.ResumeLayout(False)
    Me.pnOrdine.PerformLayout()
    CType(Me.ckPrivilegiaTipoBF_CF.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTipobf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edStagione.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckOpInterni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnnoTc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatdoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnConfermaFocus, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnConfermaFocus.ResumeLayout(False)
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

  Public Overridable Sub InitEntity(ByRef cleGnor As CLEORGNOR)
    oCleGnor = cleGnor
    AddHandler oCleGnor.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      Dim dttTipoRk As New DataTable()

      dttTipoRk.Columns.Add("cod", GetType(String))
      dttTipoRk.Columns.Add("val", GetType(String))
      'dttTipoRk.Rows.Add(New Object() {"R", "Impegno cliente"})
      dttTipoRk.Rows.Add(New Object() {"O", "Ordine fornitore"})
      dttTipoRk.Rows.Add(New Object() {"H", "Ordine di produzione"})
      dttTipoRk.Rows.Add(New Object() {"X", "Impegno Trasferimento"})
      'dttTipoRk.Rows.Add(New Object() {"Q", "Preventivo"})
      'dttTipoRk.Rows.Add(New Object() {"#", "Impegno di commessa"})
      'dttTipoRk.Rows.Add(New Object() {"V", "Impegno cliente aperto"})
      'dttTipoRk.Rows.Add(New Object() {"$", "Ordine fornitore aperto"})
      'dttTipoRk.Rows.Add(New Object() {"Y", "Impegno di produzione"})
      cbTipo.DataSource = dttTipoRk
      cbTipo.ValueMember = "cod"
      cbTipo.DisplayMember = "val"

      edAnno.NTSSetParam(oMenu, oApp.Tr(Me, 128230023057311727, "Anno"), "0", 4, 1900, 2099)
      edSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128230023057467900, "Serie"), CLN__STD.SerieMaxLen, False)
      cbTipo.NTSSetParam(oApp.Tr(Me, 128230023057624073, "Tipo documento"))
      edNumero.NTSSetParam(oMenu, oApp.Tr(Me, 128230023057780246, "Numero"), "0", 9, 0, 999999999)
      edAnnoTc.NTSSetParam(oMenu, oApp.Tr(Me, 128602577224531250, "Anno TCO"), "0", 4, 1900, 2099)
      edTipobf.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128602577538750000, "Tipo bolla/fattura"), tabtpbf)
      edStagione.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128607804532656250, "Stagione"), tabstag)
      ckOpInterni.NTSSetParam(oMenu, oApp.Tr(Me, 128607823731718750, "Ordini interni di prod."), "S", "N")
      edDatdoc.NTSSetParam(oMenu, oApp.Tr(Me, 128607823732187500, "Data documento"), False)
      ckPrivilegiaTipoBF_CF.NTSSetParam(oMenu, oApp.Tr(Me, 130433193399001928, "Privilegia tipo bolla/fattura indicato in Anagrafica Clienti/Fornitori"), "S", "N")

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

  Public Overridable Sub FRMORSEOS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      edAnno.Text = DateTime.Now.Year.ToString
      edAnnoTc.Text = edAnno.Text
      edDatdoc.Text = DateTime.Now.ToShortDateString

      edAnno.Text = oMenu.GetSettingBus("Bsorgsor", "Recent", ".", "AnnoOrdineNuovo", DateTime.Now.Year.ToString, " ", DateTime.Now.Year.ToString)
      edSerie.Text = oMenu.GetSettingBus("Bsorgsor", "Recent", ".", "SerieOrdineNuovo", " ", " ", " ")
      bDisabilitaCheckAnnoData = CBool(oMenu.GetSettingBus("BSORGNOR", "Opzioni", ".", "DisabilitaCheckAnnoData", "0", " ", "0")) 'se impostata a -1 non fa il controllo se la data è inclusa nell'anno
      cbTipo.SelectedValue = oCleGnor.strOrdlistTipork
      ckOpInterni.Checked = oCleGnor.bOpInterni

      If Not CBool((oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCO)) Then
        Me.MinimumSize = New Size(Me.Width, 0)
        Me.Height = 174
        edAnnoTc.Enabled = False
        edStagione.Enabled = False
      Else
        edAnnoTc.Text = oMenu.GetSettingBus("Bsorgsor", "Recent", ".", "AnnoTCOrdineNuovo", DateTime.Now.Year.ToString, " ", DateTime.Now.Year.ToString)
        edStagione.Text = oMenu.GetSettingBus("Bsorgsor", "Recent", ".", "StagioneOrdineNuovo", "0", " ", "0")
      End If

      '--------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl

      GctlSetRoules()
      GctlApplicaDefaultValue()

      cbTipo_SelectedValueChanged(cbTipo, Nothing)

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub


  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Dim strSuff As String = ""
    Try
      '---------------------------
      'Controlla la congruità dell'anno della data di emssione con l'anno indicati
      If bDisabilitaCheckAnnoData = False Then
        If Year(NTSCDate(edDatdoc.Text)) <> NTSCInt(edAnno.Text) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128607818533437500, "Attenzione!" & vbCrLf & vbCrLf & _
            "L'anno della 'Data ordine' (|" & edDatdoc.Text & "|)" & vbCrLf & _
            "non corrisponde all'anno indicato (|" & edAnno.Text & "|)." & vbCrLf & vbCrLf & _
            "Generazione ordini da proposte d'ordine non possibile."))
          Return
        End If
      End If

      If NTSCInt(edTipobf.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128607819056875000, "Tipo bolla/fattura obbligatorio." & vbCrLf & vbCrLf & _
            "Generazione ordini da proposte d'ordine non possibile."))
        Return
      End If

      If NTSCInt(edStagione.Text) = 0 And CBool((oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCO)) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128607820231093750, "Stagione obbligatoria." & vbCrLf & vbCrLf & _
            "Generazione ordini da proposte d'ordine non possibile."))
        Return
      End If

      If NTSCInt(edNumero.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128607820246562500, "Il numero documento deve essere diverso da 0." & vbCrLf & vbCrLf & _
            "Generazione ordini da proposte d'ordine non possibile."))
        Return
      End If

      '------------------------------
      Select Case cbTipo.SelectedValue
        Case "O"
          strSuff = "OF"
        Case "H"
          If ckOpInterni.Checked Then
            strSuff = "OP"
          Else
            strSuff = "OT" ' ordini terzisti
          End If
        Case "X"
          strSuff = "IT"
      End Select

      oMenu.SaveSettingBus("Bsorgsor", "Recent", ".", "AnnoOrdineNuovo", edAnno.Text, " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("Bsorgsor", "Recent", ".", "SerieOrdineNuovo", edSerie.Text, " ", "NS.", "...", "...")
      If strSuff <> "" Then
        oMenu.SaveSettingBus("Bsorgnor", "Recent", ".", "TipoBFNuovo" & strSuff, edTipobf.Text, " ", "NS.", "...", "...")
      End If
      If CBool((oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCO)) Then
        oMenu.SaveSettingBus("Bsorgsor", "Recent", ".", "AnnoTcOrdineNuovo", edAnnoTc.Text, " ", "NS.", "...", "...")
        oMenu.SaveSettingBus("Bsorgsor", "Recent", ".", "StagioneOrdineNuovo", edStagione.Text, " ", "NS.", "...", "...")
      End If

      oCleGnor.bPrivilegiaTipoBF_CF = ckPrivilegiaTipoBF_CF.Checked

      bOk = True
      Me.Close()
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


  Public Overridable Sub cbTipo_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTipo.SelectedValueChanged
    Dim strSoloSerie As String = ""
    Dim strSuff As String = ""
    Try
      strSoloSerie = oMenu.GetSettingBus("Bsorgsor", "OpzioniDocUt", ".", "SoloSerie", "?", cbTipo.SelectedValue.ToString(), "?")
      If strSoloSerie = "" Then strSoloSerie = " "
      If strSoloSerie <> "?" Then
        edSerie.Text = strSoloSerie
        edSerie.Enabled = False
      Else
        GctlSetVisEnab(edSerie, False)
      End If

      Select Case cbTipo.SelectedValue
        Case "O"
          strSuff = "OF"
        Case "H"
          If ckOpInterni.Checked Then
            strSuff = "OP"
          Else
            strSuff = "OT" ' ordini terzisti
          End If
        Case "X"
          strSuff = "IT"
      End Select
      If strSuff = "" Then
        edTipobf.Text = "1"
      Else
        edTipobf.Text = oMenu.GetSettingBus("BSORGNOR", "Recent", ".", "TipoBFNuovo" & strSuff, "2", " ", "2")
      End If
      NTSGetDataAutocompletamentoTipork = cbTipo.SelectedValue 'per filtrare solo i tipibf utilizzabili per questo tipork

      ProponiNumdoc()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckOpInterni_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckOpInterni.CheckedChanged
    Dim strSuff As String = ""
    Try
      Select Case cbTipo.SelectedValue
        Case "O"
          strSuff = "OF"
        Case "H"
          If ckOpInterni.Checked Then
            strSuff = "OP"
          Else
            strSuff = "OT" ' ordini terzisti
          End If
        Case "X"
          strSuff = "IT"
      End Select
      If strSuff = "" Then
        edTipobf.Text = "1"
      Else
        edTipobf.Text = oMenu.GetSettingBus("Bsorgnor", "Recent", ".", "TipoBFNuovo" & strSuff, "2", " ", "2")
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


  Public Overridable Sub edAnno_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAnno.ValidatedAndChanged
    Try
      ProponiNumdoc()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edSerie_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSerie.ValidatedAndChanged
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edSerie.Text, False)
      If strTmp <> edSerie.Text Then edSerie.Text = strTmp

      If edSerie.Text <> edSerie.Text.ToUpper Then edSerie.Text = edSerie.Text.ToUpper
      ProponiNumdoc()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edStagione_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edStagione.ValidatedAndChanged
    Dim strTmp As String = ""
    Try
      If oCleGnor Is Nothing Then Return
      If Not oCleGnor.edStagione_Validated(NTSCInt(edStagione.Text), strTmp) Then
        edStagione.Text = NTSCStr(edStagione.OldEditValue)
      Else
        lbStagione.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edTipoBf_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTipobf.ValidatedAndChanged
    Dim strTmp As String = ""
    Try
      If oCleGnor Is Nothing Then Return
      If Not oCleGnor.edTipoBf_Validated(NTSCInt(edTipobf.Text), strTmp) Then
        edTipobf.Text = NTSCStr(edTipobf.OldEditValue)
      Else
        lbTipobf.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function ProponiNumdoc() As Boolean
    'al cambio di tipo, anno, serie documento propongo il num documento
    Dim lNewProgr As Integer = 0
    Try
      If cbTipo.SelectedValue <> "" Then
        lNewProgr = oCleGnor.LegNuma(IIf(cbTipo.SelectedValue.ToString() = "V", "VV", cbTipo.SelectedValue.ToString()).ToString, edSerie.Text, NTSCInt(edAnno.Text))
      End If
      edNumero.Text = lNewProgr.ToString

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

End Class
