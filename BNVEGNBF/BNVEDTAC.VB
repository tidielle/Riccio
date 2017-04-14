Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVEDTAC

#Region "Variabili"
  Public oCallParams As CLE__CLDP
  Public bOk As Boolean
  Public oCleGnbf As CLEVEGNBF
  Public bDisabilitaCheckAnnoData As Boolean = False

  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents edAnnofat As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAnnofat As NTSInformatica.NTSLabel
  Public WithEvents lbTipoRk As NTSInformatica.NTSLabel
  Public WithEvents lbXx_Vettor As NTSInformatica.NTSLabel
  Public WithEvents lbVettor As NTSInformatica.NTSLabel
  Public WithEvents edVettor As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTrasp As NTSInformatica.NTSLabel
  Public WithEvents cbTrasp As NTSInformatica.NTSComboBox
  Public WithEvents lbNumfat As NTSInformatica.NTSLabel
  Public WithEvents edSerfat As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbSerfat As NTSInformatica.NTSLabel
  Public WithEvents edNumfat As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDatfat As NTSInformatica.NTSTextBoxData
  Public WithEvents lbDatfat As NTSInformatica.NTSLabel
  Public WithEvents frTrasporto As NTSInformatica.NTSGroupBox
  Public WithEvents frScelte As NTSInformatica.NTSGroupBox
  Public WithEvents ckPreesist As NTSInformatica.NTSCheckBox
  Public WithEvents lbOriniz As NTSInformatica.NTSLabel
  Public WithEvents lbDtiniz As NTSInformatica.NTSLabel
  Public WithEvents edOriniz As NTSInformatica.NTSTextBoxData
  Public WithEvents edDtiniz As NTSInformatica.NTSTextBoxData
  Public WithEvents ckRaggruppa As NTSInformatica.NTSCheckBox
  Public WithEvents frValoreMinimo As NTSInformatica.NTSGroupBox
  Public WithEvents edValoreMinimo As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbValoreMinimo As NTSInformatica.NTSLabel
  Public WithEvents cbTipoRk As NTSInformatica.NTSComboBox
#End Region

  Public Overridable Sub InitializeComponent()
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.lbXx_Vettor = New NTSInformatica.NTSLabel
    Me.lbVettor = New NTSInformatica.NTSLabel
    Me.edVettor = New NTSInformatica.NTSTextBoxNum
    Me.lbTrasp = New NTSInformatica.NTSLabel
    Me.cbTrasp = New NTSInformatica.NTSComboBox
    Me.lbNumfat = New NTSInformatica.NTSLabel
    Me.edSerfat = New NTSInformatica.NTSTextBoxStr
    Me.lbSerfat = New NTSInformatica.NTSLabel
    Me.edNumfat = New NTSInformatica.NTSTextBoxNum
    Me.edDatfat = New NTSInformatica.NTSTextBoxData
    Me.lbDatfat = New NTSInformatica.NTSLabel
    Me.edAnnofat = New NTSInformatica.NTSTextBoxNum
    Me.lbAnnofat = New NTSInformatica.NTSLabel
    Me.lbTipoRk = New NTSInformatica.NTSLabel
    Me.cbTipoRk = New NTSInformatica.NTSComboBox
    Me.frTrasporto = New NTSInformatica.NTSGroupBox
    Me.lbOriniz = New NTSInformatica.NTSLabel
    Me.lbDtiniz = New NTSInformatica.NTSLabel
    Me.edOriniz = New NTSInformatica.NTSTextBoxData
    Me.edDtiniz = New NTSInformatica.NTSTextBoxData
    Me.frScelte = New NTSInformatica.NTSGroupBox
    Me.ckRaggruppa = New NTSInformatica.NTSCheckBox
    Me.ckPreesist = New NTSInformatica.NTSCheckBox
    Me.frValoreMinimo = New NTSInformatica.NTSGroupBox
    Me.edValoreMinimo = New NTSInformatica.NTSTextBoxNum
    Me.lbValoreMinimo = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.edVettor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTrasp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerfat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumfat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatfat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnnofat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipoRk.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frTrasporto, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frTrasporto.SuspendLayout()
    CType(Me.edOriniz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDtiniz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frScelte, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frScelte.SuspendLayout()
    CType(Me.ckRaggruppa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckPreesist.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frValoreMinimo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frValoreMinimo.SuspendLayout()
    CType(Me.edValoreMinimo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(467, 49)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(105, 22)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImagePath = ""
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(467, 24)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.NTSContextMenu = Nothing
    Me.cmdOk.Size = New System.Drawing.Size(105, 22)
    Me.cmdOk.TabIndex = 2
    Me.cmdOk.Text = "&Conferma"
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.lbXx_Vettor)
    Me.pnTop.Controls.Add(Me.lbVettor)
    Me.pnTop.Controls.Add(Me.edVettor)
    Me.pnTop.Controls.Add(Me.lbTrasp)
    Me.pnTop.Controls.Add(Me.cbTrasp)
    Me.pnTop.Controls.Add(Me.lbNumfat)
    Me.pnTop.Controls.Add(Me.edSerfat)
    Me.pnTop.Controls.Add(Me.lbSerfat)
    Me.pnTop.Controls.Add(Me.edNumfat)
    Me.pnTop.Controls.Add(Me.edDatfat)
    Me.pnTop.Controls.Add(Me.lbDatfat)
    Me.pnTop.Controls.Add(Me.edAnnofat)
    Me.pnTop.Controls.Add(Me.lbAnnofat)
    Me.pnTop.Controls.Add(Me.lbTipoRk)
    Me.pnTop.Controls.Add(Me.cbTipoRk)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Location = New System.Drawing.Point(-1, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(462, 159)
    Me.pnTop.TabIndex = 10
    Me.pnTop.Text = "NtsPanel1"
    '
    'lbXx_Vettor
    '
    Me.lbXx_Vettor.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Vettor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Vettor.Location = New System.Drawing.Point(190, 134)
    Me.lbXx_Vettor.Name = "lbXx_Vettor"
    Me.lbXx_Vettor.NTSDbField = ""
    Me.lbXx_Vettor.Size = New System.Drawing.Size(268, 20)
    Me.lbXx_Vettor.TabIndex = 51
    Me.lbXx_Vettor.Tooltip = ""
    Me.lbXx_Vettor.UseMnemonic = False
    '
    'lbVettor
    '
    Me.lbVettor.AutoSize = True
    Me.lbVettor.BackColor = System.Drawing.Color.Transparent
    Me.lbVettor.Location = New System.Drawing.Point(13, 137)
    Me.lbVettor.Name = "lbVettor"
    Me.lbVettor.NTSDbField = ""
    Me.lbVettor.Size = New System.Drawing.Size(43, 13)
    Me.lbVettor.TabIndex = 19
    Me.lbVettor.Text = "Vettore"
    Me.lbVettor.Tooltip = ""
    Me.lbVettor.UseMnemonic = False
    '
    'edVettor
    '
    Me.edVettor.Cursor = System.Windows.Forms.Cursors.Default
    Me.edVettor.EditValue = "0"
    Me.edVettor.Location = New System.Drawing.Point(121, 134)
    Me.edVettor.Name = "edVettor"
    Me.edVettor.NTSDbField = ""
    Me.edVettor.NTSFormat = "0"
    Me.edVettor.NTSForzaVisZoom = False
    Me.edVettor.NTSOldValue = ""
    Me.edVettor.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edVettor.Properties.Appearance.Options.UseBackColor = True
    Me.edVettor.Properties.Appearance.Options.UseTextOptions = True
    Me.edVettor.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edVettor.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edVettor.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edVettor.Properties.AutoHeight = False
    Me.edVettor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edVettor.Properties.MaxLength = 9
    Me.edVettor.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edVettor.Size = New System.Drawing.Size(63, 20)
    Me.edVettor.TabIndex = 20
    '
    'lbTrasp
    '
    Me.lbTrasp.AutoSize = True
    Me.lbTrasp.BackColor = System.Drawing.Color.Transparent
    Me.lbTrasp.Location = New System.Drawing.Point(13, 115)
    Me.lbTrasp.Name = "lbTrasp"
    Me.lbTrasp.NTSDbField = ""
    Me.lbTrasp.Size = New System.Drawing.Size(98, 13)
    Me.lbTrasp.TabIndex = 18
    Me.lbTrasp.Text = "Trasporto a cura di"
    Me.lbTrasp.Tooltip = ""
    Me.lbTrasp.UseMnemonic = False
    '
    'cbTrasp
    '
    Me.cbTrasp.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTrasp.DataSource = Nothing
    Me.cbTrasp.DisplayMember = ""
    Me.cbTrasp.Location = New System.Drawing.Point(121, 112)
    Me.cbTrasp.Name = "cbTrasp"
    Me.cbTrasp.NTSDbField = ""
    Me.cbTrasp.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTrasp.Properties.Appearance.Options.UseBackColor = True
    Me.cbTrasp.Properties.AutoHeight = False
    Me.cbTrasp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTrasp.Properties.DropDownRows = 30
    Me.cbTrasp.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTrasp.SelectedValue = ""
    Me.cbTrasp.Size = New System.Drawing.Size(222, 20)
    Me.cbTrasp.TabIndex = 17
    Me.cbTrasp.ValueMember = ""
    '
    'lbNumfat
    '
    Me.lbNumfat.AutoSize = True
    Me.lbNumfat.BackColor = System.Drawing.Color.Transparent
    Me.lbNumfat.Location = New System.Drawing.Point(190, 93)
    Me.lbNumfat.Name = "lbNumfat"
    Me.lbNumfat.NTSDbField = ""
    Me.lbNumfat.Size = New System.Drawing.Size(11, 13)
    Me.lbNumfat.TabIndex = 16
    Me.lbNumfat.Text = "/"
    Me.lbNumfat.Tooltip = ""
    Me.lbNumfat.UseMnemonic = False
    '
    'edSerfat
    '
    Me.edSerfat.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSerfat.EditValue = " "
    Me.edSerfat.Location = New System.Drawing.Point(121, 90)
    Me.edSerfat.Name = "edSerfat"
    Me.edSerfat.NTSDbField = ""
    Me.edSerfat.NTSForzaVisZoom = False
    Me.edSerfat.NTSOldValue = " "
    Me.edSerfat.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edSerfat.Properties.Appearance.Options.UseBackColor = True
    Me.edSerfat.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSerfat.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSerfat.Properties.AutoHeight = False
    Me.edSerfat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSerfat.Properties.MaxLength = 1
    Me.edSerfat.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSerfat.Size = New System.Drawing.Size(63, 20)
    Me.edSerfat.TabIndex = 15
    '
    'lbSerfat
    '
    Me.lbSerfat.AutoSize = True
    Me.lbSerfat.BackColor = System.Drawing.Color.Transparent
    Me.lbSerfat.Location = New System.Drawing.Point(13, 93)
    Me.lbSerfat.Name = "lbSerfat"
    Me.lbSerfat.NTSDbField = ""
    Me.lbSerfat.Size = New System.Drawing.Size(103, 13)
    Me.lbSerfat.TabIndex = 13
    Me.lbSerfat.Text = "N. primo documento"
    Me.lbSerfat.Tooltip = ""
    Me.lbSerfat.UseMnemonic = False
    '
    'edNumfat
    '
    Me.edNumfat.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumfat.EditValue = "0"
    Me.edNumfat.Location = New System.Drawing.Point(207, 90)
    Me.edNumfat.Name = "edNumfat"
    Me.edNumfat.NTSDbField = ""
    Me.edNumfat.NTSFormat = "0"
    Me.edNumfat.NTSForzaVisZoom = False
    Me.edNumfat.NTSOldValue = ""
    Me.edNumfat.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edNumfat.Properties.Appearance.Options.UseBackColor = True
    Me.edNumfat.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumfat.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumfat.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNumfat.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNumfat.Properties.AutoHeight = False
    Me.edNumfat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumfat.Properties.MaxLength = 9
    Me.edNumfat.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumfat.Size = New System.Drawing.Size(97, 20)
    Me.edNumfat.TabIndex = 14
    '
    'edDatfat
    '
    Me.edDatfat.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatfat.EditValue = ""
    Me.edDatfat.Location = New System.Drawing.Point(121, 68)
    Me.edDatfat.Name = "edDatfat"
    Me.edDatfat.NTSDbField = ""
    Me.edDatfat.NTSForzaVisZoom = False
    Me.edDatfat.NTSOldValue = ""
    Me.edDatfat.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatfat.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatfat.Properties.AutoHeight = False
    Me.edDatfat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatfat.Properties.MaxLength = 65536
    Me.edDatfat.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatfat.Size = New System.Drawing.Size(86, 20)
    Me.edDatfat.TabIndex = 12
    '
    'lbDatfat
    '
    Me.lbDatfat.AutoSize = True
    Me.lbDatfat.BackColor = System.Drawing.Color.Transparent
    Me.lbDatfat.Location = New System.Drawing.Point(13, 71)
    Me.lbDatfat.Name = "lbDatfat"
    Me.lbDatfat.NTSDbField = ""
    Me.lbDatfat.Size = New System.Drawing.Size(30, 13)
    Me.lbDatfat.TabIndex = 11
    Me.lbDatfat.Text = "Data"
    Me.lbDatfat.Tooltip = ""
    Me.lbDatfat.UseMnemonic = False
    '
    'edAnnofat
    '
    Me.edAnnofat.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnnofat.EditValue = ""
    Me.edAnnofat.Location = New System.Drawing.Point(121, 46)
    Me.edAnnofat.Name = "edAnnofat"
    Me.edAnnofat.NTSDbField = ""
    Me.edAnnofat.NTSFormat = "0"
    Me.edAnnofat.NTSForzaVisZoom = False
    Me.edAnnofat.NTSOldValue = ""
    Me.edAnnofat.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAnnofat.Properties.Appearance.Options.UseBackColor = True
    Me.edAnnofat.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnnofat.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnnofat.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnnofat.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnnofat.Properties.AutoHeight = False
    Me.edAnnofat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnnofat.Properties.MaxLength = 65536
    Me.edAnnofat.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnnofat.Size = New System.Drawing.Size(86, 20)
    Me.edAnnofat.TabIndex = 9
    '
    'lbAnnofat
    '
    Me.lbAnnofat.AutoSize = True
    Me.lbAnnofat.BackColor = System.Drawing.Color.Transparent
    Me.lbAnnofat.Location = New System.Drawing.Point(13, 49)
    Me.lbAnnofat.Name = "lbAnnofat"
    Me.lbAnnofat.NTSDbField = ""
    Me.lbAnnofat.Size = New System.Drawing.Size(32, 13)
    Me.lbAnnofat.TabIndex = 10
    Me.lbAnnofat.Text = "Anno"
    Me.lbAnnofat.Tooltip = ""
    Me.lbAnnofat.UseMnemonic = False
    '
    'lbTipoRk
    '
    Me.lbTipoRk.AutoSize = True
    Me.lbTipoRk.BackColor = System.Drawing.Color.Transparent
    Me.lbTipoRk.Location = New System.Drawing.Point(13, 27)
    Me.lbTipoRk.Name = "lbTipoRk"
    Me.lbTipoRk.NTSDbField = ""
    Me.lbTipoRk.Size = New System.Drawing.Size(83, 13)
    Me.lbTipoRk.TabIndex = 8
    Me.lbTipoRk.Text = "Tipo documento"
    Me.lbTipoRk.Tooltip = ""
    Me.lbTipoRk.UseMnemonic = False
    '
    'cbTipoRk
    '
    Me.cbTipoRk.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipoRk.DataSource = Nothing
    Me.cbTipoRk.DisplayMember = ""
    Me.cbTipoRk.Location = New System.Drawing.Point(121, 24)
    Me.cbTipoRk.Name = "cbTipoRk"
    Me.cbTipoRk.NTSDbField = ""
    Me.cbTipoRk.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTipoRk.Properties.Appearance.Options.UseBackColor = True
    Me.cbTipoRk.Properties.AutoHeight = False
    Me.cbTipoRk.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipoRk.Properties.DropDownRows = 30
    Me.cbTipoRk.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipoRk.SelectedValue = ""
    Me.cbTipoRk.Size = New System.Drawing.Size(222, 20)
    Me.cbTipoRk.TabIndex = 7
    Me.cbTipoRk.ValueMember = ""
    '
    'frTrasporto
    '
    Me.frTrasporto.AllowDrop = True
    Me.frTrasporto.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frTrasporto.Appearance.Options.UseBackColor = True
    Me.frTrasporto.Controls.Add(Me.lbOriniz)
    Me.frTrasporto.Controls.Add(Me.lbDtiniz)
    Me.frTrasporto.Controls.Add(Me.edOriniz)
    Me.frTrasporto.Controls.Add(Me.edDtiniz)
    Me.frTrasporto.Cursor = System.Windows.Forms.Cursors.Default
    Me.frTrasporto.Location = New System.Drawing.Point(12, 165)
    Me.frTrasporto.Name = "frTrasporto"
    Me.frTrasporto.Size = New System.Drawing.Size(167, 93)
    Me.frTrasporto.TabIndex = 13
    Me.frTrasporto.Text = "Inizio trasporto: "
    '
    'lbOriniz
    '
    Me.lbOriniz.AutoSize = True
    Me.lbOriniz.BackColor = System.Drawing.Color.Transparent
    Me.lbOriniz.Location = New System.Drawing.Point(14, 58)
    Me.lbOriniz.Name = "lbOriniz"
    Me.lbOriniz.NTSDbField = ""
    Me.lbOriniz.Size = New System.Drawing.Size(25, 13)
    Me.lbOriniz.TabIndex = 16
    Me.lbOriniz.Text = "Ora"
    Me.lbOriniz.Tooltip = ""
    Me.lbOriniz.UseMnemonic = False
    '
    'lbDtiniz
    '
    Me.lbDtiniz.AutoSize = True
    Me.lbDtiniz.BackColor = System.Drawing.Color.Transparent
    Me.lbDtiniz.Location = New System.Drawing.Point(14, 32)
    Me.lbDtiniz.Name = "lbDtiniz"
    Me.lbDtiniz.NTSDbField = ""
    Me.lbDtiniz.Size = New System.Drawing.Size(30, 13)
    Me.lbDtiniz.TabIndex = 15
    Me.lbDtiniz.Text = "Data"
    Me.lbDtiniz.Tooltip = ""
    Me.lbDtiniz.UseMnemonic = False
    '
    'edOriniz
    '
    Me.edOriniz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edOriniz.EditValue = ""
    Me.edOriniz.Location = New System.Drawing.Point(64, 55)
    Me.edOriniz.Name = "edOriniz"
    Me.edOriniz.NTSDbField = ""
    Me.edOriniz.NTSForzaVisZoom = False
    Me.edOriniz.NTSOldValue = ""
    Me.edOriniz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOriniz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOriniz.Properties.AutoHeight = False
    Me.edOriniz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOriniz.Properties.MaxLength = 65536
    Me.edOriniz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOriniz.Size = New System.Drawing.Size(86, 20)
    Me.edOriniz.TabIndex = 14
    '
    'edDtiniz
    '
    Me.edDtiniz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDtiniz.EditValue = ""
    Me.edDtiniz.Location = New System.Drawing.Point(64, 29)
    Me.edDtiniz.Name = "edDtiniz"
    Me.edDtiniz.NTSDbField = ""
    Me.edDtiniz.NTSForzaVisZoom = False
    Me.edDtiniz.NTSOldValue = ""
    Me.edDtiniz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDtiniz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDtiniz.Properties.AutoHeight = False
    Me.edDtiniz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDtiniz.Properties.MaxLength = 65536
    Me.edDtiniz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDtiniz.Size = New System.Drawing.Size(86, 20)
    Me.edDtiniz.TabIndex = 13
    '
    'frScelte
    '
    Me.frScelte.AllowDrop = True
    Me.frScelte.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frScelte.Appearance.Options.UseBackColor = True
    Me.frScelte.Controls.Add(Me.ckRaggruppa)
    Me.frScelte.Controls.Add(Me.ckPreesist)
    Me.frScelte.Cursor = System.Windows.Forms.Cursors.Default
    Me.frScelte.Location = New System.Drawing.Point(185, 165)
    Me.frScelte.Name = "frScelte"
    Me.frScelte.ShowCaption = False
    Me.frScelte.Size = New System.Drawing.Size(387, 49)
    Me.frScelte.TabIndex = 14
    '
    'ckRaggruppa
    '
    Me.ckRaggruppa.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckRaggruppa.Location = New System.Drawing.Point(21, 25)
    Me.ckRaggruppa.Name = "ckRaggruppa"
    Me.ckRaggruppa.NTSCheckValue = "S"
    Me.ckRaggruppa.NTSUnCheckValue = "N"
    Me.ckRaggruppa.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckRaggruppa.Properties.Appearance.Options.UseBackColor = True
    Me.ckRaggruppa.Properties.AutoHeight = False
    Me.ckRaggruppa.Properties.Caption = "Raggruppa Note di Prelievo"
    Me.ckRaggruppa.Size = New System.Drawing.Size(165, 19)
    Me.ckRaggruppa.TabIndex = 14
    '
    'ckPreesist
    '
    Me.ckPreesist.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckPreesist.Location = New System.Drawing.Point(21, 5)
    Me.ckPreesist.Name = "ckPreesist"
    Me.ckPreesist.NTSCheckValue = "S"
    Me.ckPreesist.NTSUnCheckValue = "N"
    Me.ckPreesist.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPreesist.Properties.Appearance.Options.UseBackColor = True
    Me.ckPreesist.Properties.AutoHeight = False
    Me.ckPreesist.Properties.Caption = "&Usa i dati accompagnatori preesistenti sul documento di origine"
    Me.ckPreesist.Size = New System.Drawing.Size(325, 19)
    Me.ckPreesist.TabIndex = 13
    '
    'frValoreMinimo
    '
    Me.frValoreMinimo.AllowDrop = True
    Me.frValoreMinimo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frValoreMinimo.Appearance.Options.UseBackColor = True
    Me.frValoreMinimo.Controls.Add(Me.edValoreMinimo)
    Me.frValoreMinimo.Controls.Add(Me.lbValoreMinimo)
    Me.frValoreMinimo.Cursor = System.Windows.Forms.Cursors.Default
    Me.frValoreMinimo.Location = New System.Drawing.Point(185, 220)
    Me.frValoreMinimo.Name = "frValoreMinimo"
    Me.frValoreMinimo.ShowCaption = False
    Me.frValoreMinimo.Size = New System.Drawing.Size(387, 38)
    Me.frValoreMinimo.TabIndex = 15
    '
    'edValoreMinimo
    '
    Me.edValoreMinimo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edValoreMinimo.EditValue = ""
    Me.edValoreMinimo.Location = New System.Drawing.Point(249, 9)
    Me.edValoreMinimo.Name = "edValoreMinimo"
    Me.edValoreMinimo.NTSDbField = ""
    Me.edValoreMinimo.NTSFormat = "0"
    Me.edValoreMinimo.NTSForzaVisZoom = False
    Me.edValoreMinimo.NTSOldValue = ""
    Me.edValoreMinimo.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edValoreMinimo.Properties.Appearance.Options.UseBackColor = True
    Me.edValoreMinimo.Properties.Appearance.Options.UseTextOptions = True
    Me.edValoreMinimo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edValoreMinimo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edValoreMinimo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edValoreMinimo.Properties.AutoHeight = False
    Me.edValoreMinimo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edValoreMinimo.Properties.MaxLength = 65536
    Me.edValoreMinimo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edValoreMinimo.Size = New System.Drawing.Size(122, 20)
    Me.edValoreMinimo.TabIndex = 11
    '
    'lbValoreMinimo
    '
    Me.lbValoreMinimo.AutoSize = True
    Me.lbValoreMinimo.BackColor = System.Drawing.Color.Transparent
    Me.lbValoreMinimo.Location = New System.Drawing.Point(18, 12)
    Me.lbValoreMinimo.Name = "lbValoreMinimo"
    Me.lbValoreMinimo.NTSDbField = ""
    Me.lbValoreMinimo.Size = New System.Drawing.Size(205, 13)
    Me.lbValoreMinimo.TabIndex = 12
    Me.lbValoreMinimo.Text = "Valore minimo per generazione documenti"
    Me.lbValoreMinimo.Tooltip = ""
    Me.lbValoreMinimo.UseMnemonic = False
    '
    'FRMVEDTAC
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(585, 270)
    Me.Controls.Add(Me.frValoreMinimo)
    Me.Controls.Add(Me.frScelte)
    Me.Controls.Add(Me.frTrasporto)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMVEDTAC"
    Me.Text = "GENERAZIONE DOCUMENTI: INFORMAZIONI AGGIUNTIVE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.edVettor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTrasp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerfat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNumfat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatfat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnnofat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipoRk.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frTrasporto, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frTrasporto.ResumeLayout(False)
    Me.frTrasporto.PerformLayout()
    CType(Me.edOriniz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDtiniz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frScelte, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frScelte.ResumeLayout(False)
    Me.frScelte.PerformLayout()
    CType(Me.ckRaggruppa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckPreesist.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frValoreMinimo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frValoreMinimo.ResumeLayout(False)
    Me.frValoreMinimo.PerformLayout()
    CType(Me.edValoreMinimo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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

  Public Overridable Sub InitEntity(ByRef cleGnbf As CLEVEGNBF)
    oCleGnbf = cleGnbf
    AddHandler oCleGnbf.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      ckPreesist.NTSSetParam(oMenu, oApp.Tr(Me, 128711543267803712, "Usa i dati accompagnatori preesistenti sul documento di origine"), "S", "N")
      edOriniz.NTSSetParam(oMenu, oApp.Tr(Me, 128711543268272471, "Ora"), False, "M", NTSCDate(IntSetDate("01/01/1900")), NTSCDate(IntSetDate("31/12/2099")))
      edDtiniz.NTSSetParam(oMenu, oApp.Tr(Me, 128711543268428724, "Data"), False)
      edVettor.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128711543268741230, "Vettore"), tabvett)
      cbTrasp.NTSSetParam(oApp.Tr(Me, 128711543269053736, "Trasporto a cura di"))
      edSerfat.NTSSetParam(oMenu, oApp.Tr(Me, 128711543269366242, "N. primo documento"), CLN__STD.SerieMaxLen, False)
      edNumfat.NTSSetParam(oMenu, oApp.Tr(Me, 128711543269678748, "N. primo documento"), "0", 9, 1, 999999999)
      edDatfat.NTSSetParam(oMenu, oApp.Tr(Me, 128711543269835001, "Data"), False)
      edAnnofat.NTSSetParam(oMenu, oApp.Tr(Me, 128711543270147507, "Anno"), "0", 4, 0, 9999)
      cbTipoRk.NTSSetParam(oApp.Tr(Me, 128711543270616266, "Tipo documento"))
      edValoreMinimo.NTSSetParam(oMenu, oApp.Tr(Me, 129648756451641738, "Valore minimo per generazione documenti"), oApp.FormatImporti, 0, 0, 999999999)

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

  Public Overridable Sub FRMVEDTAC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      edNumfat.Text = "1"

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      CaricaCombo()

      'Calcola l'esercizio di competenza
      oCleGnbf.nDtacEscomp = Year(Now)
      edAnnofat.Text = NTSCStr(oCleGnbf.nDtacEscomp)
      'Seleziona nuove bolle
      edDatfat.Text = NTSCDate(Now).ToShortDateString
      edAnnofat.Text = NTSCStr(oCleGnbf.nDtacEscomp)
      cbTipoRk.SelectedValue = "B"
      If Trim(oCleGnbf.strDtacTipork) <> "" Then
        cbTipoRk.SelectedValue = oCleGnbf.strDtacTipork
      End If
      edSerfat.Text = " "
      edNumfat.Text = NTSCStr(oCleGnbf.LegNuma(NTSCStr(cbTipoRk.SelectedValue), edSerfat.Text, NTSCInt(edAnnofat.Text)))
      edDtiniz.Text = NTSCDate(Now).ToShortDateString
      edVettor.Text = "0"
      edOriniz.Text = Microsoft.VisualBasic.Right("00" & NTSCStr(Hour(Now)), 2) & "," & Microsoft.VisualBasic.Right("00" & NTSCStr(Minute(Now)), 2)
      cbTrasp.SelectedValue = "M"
      '--------------------------------------------------------------------------------------------------------------
      edValoreMinimo.Text = NTSCStr(oMenu.GetSettingBus("BSVEGNBF", "OPZIONI", ".", "ValoreMinimo", "0", " ", "0"))
      '--------------------------------------------------------------------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl

      GctlSetRoules()
      GctlApplicaDefaultValue()

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      Me.ValidaLastControl()

      If Not oCleGnbf.DtacCheckSelection(edAnnofat.Text, edDatfat.Text) Then Return

      '----------------------------------------------------------------------------------------
      oCleGnbf.lDtacNumdoc = NTSCInt(edNumfat.Text)
      oCleGnbf.strDtacSerie = NTSCStr(IIf(edSerfat.Text = "", " ", edSerfat.Text))
      oCleGnbf.strDtacTipork = NTSCStr(cbTipoRk.SelectedValue)
      oCleGnbf.nDtacAnno = NTSCInt(edAnnofat.Text)
      oCleGnbf.dDtacDatdoc = NTSCDate(edDatfat.Text)
      oCleGnbf.strDtacDtiniz = edDtiniz.Text
      oCleGnbf.dDtacOriniz = NTSCDec(edOriniz.Text.Replace(".", ",").Replace(":", ","))
      oCleGnbf.nDtacCodvett = NTSCInt(edVettor.Text)
      oCleGnbf.strDtacAcuradi = NTSCStr(cbTrasp.SelectedValue)
      oCleGnbf.bPrimo = True
      If ckPreesist.Checked Then
        oCleGnbf.bDtacVariaAcc = False
      Else
        oCleGnbf.bDtacVariaAcc = True
      End If
      oCleGnbf.bDtacRaggruppa = ckRaggruppa.Checked
      oCleGnbf.dDtacValoreMinimo = NTSCDec(edValoreMinimo.Text)
      '----------------------------------------------------------------------------------------
      Me.Close()

      bOk = True
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      oCleGnbf.lDtacNumdoc = 0
      oCleGnbf.strDtacSerie = " "
      oCleGnbf.strDtacTipork = " "
      oCleGnbf.nDtacAnno = 0
      oCleGnbf.dDtacDatdoc = NTSCDate("01/01/1900")
      oCleGnbf.strDtacDtiniz = ""
      oCleGnbf.dDtacOriniz = 0
      oCleGnbf.nDtacCodvett = 0
      oCleGnbf.strDtacAcuradi = " "
      bOk = False
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub CaricaCombo()
    Dim dttCbTipoRk As New DataTable()
    Dim dttCbTrasp As New DataTable()
    Try
      dttCbTipoRk.Columns.Add("cod", GetType(String))
      dttCbTipoRk.Columns.Add("val", GetType(String))
      dttCbTipoRk.Rows.Add(New Object() {"A", "Fattura Imm. emessa"})
      dttCbTipoRk.Rows.Add(New Object() {"B", "D.D.T. emesso"})
      dttCbTipoRk.Rows.Add(New Object() {"C", "Corrispettivo emesso"})
      dttCbTipoRk.Rows.Add(New Object() {"E", "Nota di Addeb. emessa"})
      dttCbTipoRk.Rows.Add(New Object() {"F", "Ric.Fiscale Emessa"})
      dttCbTipoRk.Rows.Add(New Object() {"I", "Riemissione Ric.Fiscali"})
      dttCbTipoRk.Rows.Add(New Object() {"J", "Nota Accr. ricevuta"})
      dttCbTipoRk.Rows.Add(New Object() {"L", "Fattura Imm. ricevuta"})
      dttCbTipoRk.Rows.Add(New Object() {"M", "D.D.T. ricevuto"})
      dttCbTipoRk.Rows.Add(New Object() {"N", "Nota Accr. emessa"})
      dttCbTipoRk.Rows.Add(New Object() {"S", "Fatt.Ric.Fisc. Emessa"})
      dttCbTipoRk.Rows.Add(New Object() {"T", "Carico da produz."})
      dttCbTipoRk.Rows.Add(New Object() {"U", "Scarico a produz."})
      dttCbTipoRk.Rows.Add(New Object() {"W", "Nota di prelievo"})
      dttCbTipoRk.Rows.Add(New Object() {"Z", "Bolla di mov. interna"})
      dttCbTipoRk.AcceptChanges()

      cbTipoRk.DataSource = dttCbTipoRk
      cbTipoRk.ValueMember = "cod"
      cbTipoRk.DisplayMember = "val"

      dttCbTrasp.Columns.Add("cod", GetType(String))
      dttCbTrasp.Columns.Add("val", GetType(String))
      dttCbTrasp.Rows.Add(New Object() {" ", "(nessuno)"})
      dttCbTrasp.Rows.Add(New Object() {"D", "Destinatario"})
      dttCbTrasp.Rows.Add(New Object() {"M", "Mittente"})
      dttCbTrasp.Rows.Add(New Object() {"V", "Vettore"})
      dttCbTrasp.AcceptChanges()

      cbTrasp.DataSource = dttCbTrasp
      cbTrasp.ValueMember = "cod"
      cbTrasp.DisplayMember = "val"

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edVettor_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edVettor.Validated
    Dim strTmp As String = ""
    Try
      If oCleGnbf Is Nothing Then Return
      If Not oCleGnbf.edVettor_Validated(NTSCInt(edVettor.Text), strTmp) Then
        edVettor.Text = NTSCStr(edVettor.OldEditValue)
      Else
        lbXx_Vettor.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edAnnofat_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAnnofat.Validated
    Try
      If oCleGnbf Is Nothing Then Return
      edAnnofat.Text = NTSCStr(Year(NTSCDate("01/01/" & edAnnofat.Text)))
      edNumfat.Text = NTSCStr(oCleGnbf.LegNuma(NTSCStr(cbTipoRk.SelectedValue), edSerfat.Text, NTSCInt(edAnnofat.Text)))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edSerfat_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSerfat.Validated
    Try
      If oCleGnbf Is Nothing Then Return
      edAnnofat.Text = NTSCStr(Year(NTSCDate("01/01/" & edAnnofat.Text)))
      edNumfat.Text = NTSCStr(oCleGnbf.LegNuma(NTSCStr(cbTipoRk.SelectedValue), edSerfat.Text, NTSCInt(edAnnofat.Text)))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckPreesist_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckPreesist.CheckedChanged
    Try
      If ckPreesist.Checked = True Then
        edVettor.Text = "0"
        lbXx_Vettor.Text = ""
        edVettor.Enabled = False
        cbTrasp.Enabled = False
      Else
        edVettor.Text = "0"
        edVettor.Enabled = True
        cbTrasp.Enabled = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cbTipork_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTipoRk.Leave
    Try
      edAnnofat.Text = NTSCStr(Year(NTSCDate("01/01/" & edAnnofat.Text)))
      edNumfat.Text = NTSCStr(oCleGnbf.LegNuma(NTSCStr(cbTipoRk.SelectedValue), edSerfat.Text, NTSCInt(edAnnofat.Text)))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

End Class
