Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVEDTA1

#Region "Variabili"
  Public oCallParams As CLE__CLDP
  Public bOk As Boolean
  Public oCleGnnp As CLEORGNNP
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
  Public WithEvents ckEvadiRigheaZero As NTSInformatica.NTSCheckBox
  Public WithEvents frTrasporto As NTSInformatica.NTSGroupBox
  Public WithEvents frScelte As NTSInformatica.NTSGroupBox
  Public WithEvents ckUnaNotaPerImpegno As NTSInformatica.NTSCheckBox
  Public WithEvents ckPickinglistdistinte As NTSInformatica.NTSCheckBox
  Public WithEvents ckCreaPick As NTSInformatica.NTSCheckBox
  Public WithEvents ckPreesist As NTSInformatica.NTSCheckBox
  Public WithEvents lbOriniz As NTSInformatica.NTSLabel
  Public WithEvents lbDtiniz As NTSInformatica.NTSLabel
  Public WithEvents edOriniz As NTSInformatica.NTSTextBoxData
  Public WithEvents edDtiniz As NTSInformatica.NTSTextBoxData
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
    Me.ckEvadiRigheaZero = New NTSInformatica.NTSCheckBox
    Me.frTrasporto = New NTSInformatica.NTSGroupBox
    Me.lbOriniz = New NTSInformatica.NTSLabel
    Me.lbDtiniz = New NTSInformatica.NTSLabel
    Me.edOriniz = New NTSInformatica.NTSTextBoxData
    Me.edDtiniz = New NTSInformatica.NTSTextBoxData
    Me.frScelte = New NTSInformatica.NTSGroupBox
    Me.ckUnaNotaPerImpegno = New NTSInformatica.NTSCheckBox
    Me.ckPickinglistdistinte = New NTSInformatica.NTSCheckBox
    Me.ckCreaPick = New NTSInformatica.NTSCheckBox
    Me.ckPreesist = New NTSInformatica.NTSCheckBox
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.edVettor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTrasp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerfat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumfat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatfat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnnofat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipoRk.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckEvadiRigheaZero.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frTrasporto, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frTrasporto.SuspendLayout()
    CType(Me.edOriniz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDtiniz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frScelte, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frScelte.SuspendLayout()
    CType(Me.ckUnaNotaPerImpegno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckPickinglistdistinte.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckCreaPick.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckPreesist.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(448, 41)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(124, 22)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(448, 12)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(124, 22)
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
    Me.pnTop.Size = New System.Drawing.Size(394, 148)
    Me.pnTop.TabIndex = 10
    Me.pnTop.Text = "NtsPanel1"
    '
    'lbXx_Vettor
    '
    Me.lbXx_Vettor.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Vettor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Vettor.Location = New System.Drawing.Point(191, 122)
    Me.lbXx_Vettor.Name = "lbXx_Vettor"
    Me.lbXx_Vettor.NTSDbField = ""
    Me.lbXx_Vettor.Size = New System.Drawing.Size(188, 20)
    Me.lbXx_Vettor.TabIndex = 51
    Me.lbXx_Vettor.Tooltip = ""
    Me.lbXx_Vettor.UseMnemonic = False
    '
    'lbVettor
    '
    Me.lbVettor.AutoSize = True
    Me.lbVettor.BackColor = System.Drawing.Color.Transparent
    Me.lbVettor.Location = New System.Drawing.Point(15, 125)
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
    Me.edVettor.Location = New System.Drawing.Point(122, 122)
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
    Me.lbTrasp.Location = New System.Drawing.Point(14, 103)
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
    Me.cbTrasp.Location = New System.Drawing.Point(122, 100)
    Me.cbTrasp.Name = "cbTrasp"
    Me.cbTrasp.NTSDbField = ""
    Me.cbTrasp.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTrasp.Properties.Appearance.Options.UseBackColor = True
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
    Me.lbNumfat.Location = New System.Drawing.Point(168, 81)
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
    Me.edSerfat.Location = New System.Drawing.Point(122, 78)
    Me.edSerfat.Name = "edSerfat"
    Me.edSerfat.NTSDbField = ""
    Me.edSerfat.NTSForzaVisZoom = False
    Me.edSerfat.NTSOldValue = " "
    Me.edSerfat.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edSerfat.Properties.Appearance.Options.UseBackColor = True
    Me.edSerfat.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSerfat.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSerfat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSerfat.Properties.MaxLength = 1
    Me.edSerfat.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSerfat.Size = New System.Drawing.Size(40, 20)
    Me.edSerfat.TabIndex = 15
    '
    'lbSerfat
    '
    Me.lbSerfat.AutoSize = True
    Me.lbSerfat.BackColor = System.Drawing.Color.Transparent
    Me.lbSerfat.Location = New System.Drawing.Point(14, 81)
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
    Me.edNumfat.Location = New System.Drawing.Point(184, 78)
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
    Me.edDatfat.Location = New System.Drawing.Point(122, 56)
    Me.edDatfat.Name = "edDatfat"
    Me.edDatfat.NTSDbField = ""
    Me.edDatfat.NTSForzaVisZoom = False
    Me.edDatfat.NTSOldValue = ""
    Me.edDatfat.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatfat.Properties.AppearanceDisabled.Options.UseForeColor = True
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
    Me.lbDatfat.Location = New System.Drawing.Point(14, 59)
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
    Me.edAnnofat.Location = New System.Drawing.Point(122, 34)
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
    Me.lbAnnofat.Location = New System.Drawing.Point(14, 37)
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
    Me.lbTipoRk.Location = New System.Drawing.Point(14, 15)
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
    Me.cbTipoRk.Location = New System.Drawing.Point(122, 12)
    Me.cbTipoRk.Name = "cbTipoRk"
    Me.cbTipoRk.NTSDbField = ""
    Me.cbTipoRk.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTipoRk.Properties.Appearance.Options.UseBackColor = True
    Me.cbTipoRk.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipoRk.Properties.DropDownRows = 30
    Me.cbTipoRk.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipoRk.SelectedValue = ""
    Me.cbTipoRk.Size = New System.Drawing.Size(222, 20)
    Me.cbTipoRk.TabIndex = 7
    Me.cbTipoRk.ValueMember = ""
    '
    'ckEvadiRigheaZero
    '
    Me.ckEvadiRigheaZero.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckEvadiRigheaZero.Location = New System.Drawing.Point(16, 23)
    Me.ckEvadiRigheaZero.Name = "ckEvadiRigheaZero"
    Me.ckEvadiRigheaZero.NTSCheckValue = "S"
    Me.ckEvadiRigheaZero.NTSUnCheckValue = "N"
    Me.ckEvadiRigheaZero.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckEvadiRigheaZero.Properties.Appearance.Options.UseBackColor = True
    Me.ckEvadiRigheaZero.Properties.Caption = "&Scrivi sul documento anche righe con quantità evadibile = 0"
    Me.ckEvadiRigheaZero.Size = New System.Drawing.Size(309, 19)
    Me.ckEvadiRigheaZero.TabIndex = 12
    Me.ckEvadiRigheaZero.Visible = False
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
    Me.frTrasporto.Location = New System.Drawing.Point(12, 154)
    Me.frTrasporto.Name = "frTrasporto"
    Me.frTrasporto.Size = New System.Drawing.Size(167, 78)
    Me.frTrasporto.TabIndex = 13
    Me.frTrasporto.Text = "Inizio trasporto: "
    '
    'lbOriniz
    '
    Me.lbOriniz.AutoSize = True
    Me.lbOriniz.BackColor = System.Drawing.Color.Transparent
    Me.lbOriniz.Location = New System.Drawing.Point(15, 52)
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
    Me.lbDtiniz.Location = New System.Drawing.Point(15, 26)
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
    Me.edOriniz.Location = New System.Drawing.Point(65, 49)
    Me.edOriniz.Name = "edOriniz"
    Me.edOriniz.NTSDbField = ""
    Me.edOriniz.NTSForzaVisZoom = False
    Me.edOriniz.NTSOldValue = ""
    Me.edOriniz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOriniz.Properties.AppearanceDisabled.Options.UseForeColor = True
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
    Me.edDtiniz.Location = New System.Drawing.Point(65, 23)
    Me.edDtiniz.Name = "edDtiniz"
    Me.edDtiniz.NTSDbField = ""
    Me.edDtiniz.NTSForzaVisZoom = False
    Me.edDtiniz.NTSOldValue = ""
    Me.edDtiniz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDtiniz.Properties.AppearanceDisabled.Options.UseForeColor = True
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
    Me.frScelte.Controls.Add(Me.ckUnaNotaPerImpegno)
    Me.frScelte.Controls.Add(Me.ckPickinglistdistinte)
    Me.frScelte.Controls.Add(Me.ckCreaPick)
    Me.frScelte.Controls.Add(Me.ckPreesist)
    Me.frScelte.Controls.Add(Me.ckEvadiRigheaZero)
    Me.frScelte.Cursor = System.Windows.Forms.Cursors.Default
    Me.frScelte.Location = New System.Drawing.Point(190, 154)
    Me.frScelte.Name = "frScelte"
    Me.frScelte.ShowCaption = False
    Me.frScelte.Size = New System.Drawing.Size(387, 100)
    Me.frScelte.TabIndex = 14
    '
    'ckUnaNotaPerImpegno
    '
    Me.ckUnaNotaPerImpegno.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckUnaNotaPerImpegno.Location = New System.Drawing.Point(16, 77)
    Me.ckUnaNotaPerImpegno.Name = "ckUnaNotaPerImpegno"
    Me.ckUnaNotaPerImpegno.NTSCheckValue = "S"
    Me.ckUnaNotaPerImpegno.NTSUnCheckValue = "N"
    Me.ckUnaNotaPerImpegno.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckUnaNotaPerImpegno.Properties.Appearance.Options.UseBackColor = True
    Me.ckUnaNotaPerImpegno.Properties.Caption = "Genera una nota per ogni impegno"
    Me.ckUnaNotaPerImpegno.Size = New System.Drawing.Size(192, 19)
    Me.ckUnaNotaPerImpegno.TabIndex = 16
    '
    'ckPickinglistdistinte
    '
    Me.ckPickinglistdistinte.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckPickinglistdistinte.Location = New System.Drawing.Point(16, 59)
    Me.ckPickinglistdistinte.Name = "ckPickinglistdistinte"
    Me.ckPickinglistdistinte.NTSCheckValue = "S"
    Me.ckPickinglistdistinte.NTSUnCheckValue = "N"
    Me.ckPickinglistdistinte.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPickinglistdistinte.Properties.Appearance.Options.UseBackColor = True
    Me.ckPickinglistdistinte.Properties.Caption = "Genera una picking list per ogni nota di prelievo generata"
    Me.ckPickinglistdistinte.Size = New System.Drawing.Size(300, 19)
    Me.ckPickinglistdistinte.TabIndex = 15
    '
    'ckCreaPick
    '
    Me.ckCreaPick.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckCreaPick.Location = New System.Drawing.Point(16, 41)
    Me.ckCreaPick.Name = "ckCreaPick"
    Me.ckCreaPick.NTSCheckValue = "S"
    Me.ckCreaPick.NTSUnCheckValue = "N"
    Me.ckCreaPick.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckCreaPick.Properties.Appearance.Options.UseBackColor = True
    Me.ckCreaPick.Properties.Caption = "Crea Pic&king"
    Me.ckCreaPick.Size = New System.Drawing.Size(86, 19)
    Me.ckCreaPick.TabIndex = 14
    '
    'ckPreesist
    '
    Me.ckPreesist.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckPreesist.Location = New System.Drawing.Point(16, 5)
    Me.ckPreesist.Name = "ckPreesist"
    Me.ckPreesist.NTSCheckValue = "S"
    Me.ckPreesist.NTSUnCheckValue = "N"
    Me.ckPreesist.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPreesist.Properties.Appearance.Options.UseBackColor = True
    Me.ckPreesist.Properties.Caption = "&Usa i dati accompagnatori preesistenti sul documento di origine"
    Me.ckPreesist.Size = New System.Drawing.Size(325, 19)
    Me.ckPreesist.TabIndex = 13
    '
    'FRMVEDTA1
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(585, 320)
    Me.Controls.Add(Me.frScelte)
    Me.Controls.Add(Me.frTrasporto)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMVEDTA1"
    Me.Text = "GENERAZIONE DOCUMENTI: INFORMAZIONI AGGIUNTIVE"
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
    CType(Me.ckEvadiRigheaZero.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frTrasporto, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frTrasporto.ResumeLayout(False)
    Me.frTrasporto.PerformLayout()
    CType(Me.edOriniz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDtiniz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frScelte, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frScelte.ResumeLayout(False)
    Me.frScelte.PerformLayout()
    CType(Me.ckUnaNotaPerImpegno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckPickinglistdistinte.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckCreaPick.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckPreesist.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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

  Public Overridable Sub InitEntity(ByRef cleGnnp As CLEORGNNP)
    oCleGnnp = cleGnnp
    AddHandler oCleGnnp.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      ckUnaNotaPerImpegno.NTSSetParam(oMenu, oApp.Tr(Me, 128711543267334953, "Genera una nota per ogni impegno"), "S", "N")
      ckPickinglistdistinte.NTSSetParam(oMenu, oApp.Tr(Me, 128711543267491206, "Genera una picking list per ogni nota di prelievo generata"), "S", "N")
      ckCreaPick.NTSSetParam(oMenu, oApp.Tr(Me, 128711543267647459, "Crea Picking"), "S", "N")
      ckPreesist.NTSSetParam(oMenu, oApp.Tr(Me, 128711543267803712, "Usa i dati accompagnatori preesistenti sul documento di origine"), "S", "N")
      ckEvadiRigheaZero.NTSSetParam(oMenu, oApp.Tr(Me, 128711543267959965, "&Scrivi sul documento anche righe con quantità evadibile = 0"), "S", "N")
      edOriniz.NTSSetParam(oMenu, oApp.Tr(Me, 128711543268272471, "Ora"), False, "M", NTSCDate(IntSetDate("01/01/1900")), NTSCDate(IntSetDate("31/12/2099")))
      edDtiniz.NTSSetParam(oMenu, oApp.Tr(Me, 128711543268428724, "Data"), False)
      edVettor.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128711543268741230, "Vettore"), tabvett)
      cbTrasp.NTSSetParam(oApp.Tr(Me, 128711543269053736, "Trasporto a cura di"))
      edSerfat.NTSSetParam(oMenu, oApp.Tr(Me, 128711543269366242, "N. primo documento"), CLN__STD.SerieMaxLen, False)
      edNumfat.NTSSetParam(oMenu, oApp.Tr(Me, 128711543269678748, "N. primo documento"), "0", 9, 0, 999999999)
      edDatfat.NTSSetParam(oMenu, oApp.Tr(Me, 128711543269835001, "Data"), False)
      edAnnofat.NTSSetParam(oMenu, oApp.Tr(Me, 128711543270147507, "Anno"), "0", 4, 0, 9999)
      cbTipoRk.NTSSetParam(oApp.Tr(Me, 128711543270616266, "Tipo documento"))

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

  Public Overridable Sub FRMVEDTA1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim bCheck As Boolean
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      edNumfat.Text = "1"

      CaricaCombo()

      'Calcola l'esercizio di competenza
      oCleGnnp.nDtacEscomp = Year(Now)
      edAnnofat.Text = NTSCStr(oCleGnnp.nDtacEscomp)
      'Seleziona nuove bolle
      edDatfat.Text = NTSCDate(Now).ToShortDateString
      edAnnofat.Text = NTSCStr(oCleGnnp.nDtacEscomp)
      cbTipoRk.SelectedValue = "B"
      If Trim(oCleGnnp.strDtacTipork) <> "" Then
        cbTipoRk.SelectedValue = oCleGnnp.strDtacTipork
      End If
      If NTSCStr(cbTipoRk.SelectedValue) <> "W" Then
        ckCreaPick.Enabled = False
        ckPickinglistdistinte.Enabled = False
      End If
      edSerfat.Text = " "
      edNumfat.Text = NTSCStr(oCleGnnp.LegNuma(NTSCStr(cbTipoRk.SelectedValue), edSerfat.Text, NTSCInt(edAnnofat.Text)))
      edDtiniz.Text = NTSCDate(Now).ToShortDateString
      edVettor.Text = "0"
      edOriniz.Text = Microsoft.VisualBasic.Right("00" & NTSCStr(Hour(Now)), 2) & "," & Microsoft.VisualBasic.Right("00" & NTSCStr(Minute(Now)), 2)
      cbTrasp.SelectedValue = "M"
      If NTSCDec(oMenu.GetSettingBus("BSORGNNP", "RECENT", ".", "ScriviRighe0", "0", " ", "0")) <> 0 Then
        ckEvadiRigheaZero.Checked = True
      Else
        ckEvadiRigheaZero.Checked = False
      End If
      '----------------------------------------------------------------------------------------
      If NTSCStr(cbTipoRk.SelectedValue) = "W" Then
        If oCleGnnp.ControllaModuliPickingAnazmod() OrElse _
           CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtPIK) OrElse _
           CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupRME) Then
          bCheck = CBool(oMenu.GetSettingBus("BSORGNNP", "RECENT", ".", "CreaPicking", "0", " ", "0"))
          ckCreaPick.Checked = CBool(IIf(bCheck = True, True, False))
          GctlSetVisEnab(ckCreaPick, False)
          bCheck = CBool(oMenu.GetSettingBus("BSORGNNP", "RECENT", ".", "GeneraPickingDistinti", "0", " ", "0"))
          ckPickinglistdistinte.Checked = CBool(IIf(bCheck = True, True, False))
          If ckCreaPick.Checked = True Then GctlSetVisEnab(ckPickinglistdistinte, False)
        Else
          ckCreaPick.Enabled = False
          ckPickinglistdistinte.Enabled = False
        End If
      End If
      '----------------------------------------------------------------------------------------
      '--- Questa modale è condivisa fra BSORGNNP e BSVEGNBF
      '--- se gstrDtacTipork = "W", vuol dire che è stata chiamata da BSORGNNP
      '--- se gstrDtacTipork = "B", vuol dire che è stata chiamata da BSVEGNBF
      '--- (in questo caso deseleziona e disabilita il CheckBo relativo a:
      '--- 'Genera una nota per ogni impegno')
      '----------------------------------------------------------------------------------------
      If oCleGnnp.strDtacTipork = "W" Then
        GctlSetVisEnab(ckUnaNotaPerImpegno, False)
        bCheck = CBool(oMenu.GetSettingBus("BSORGNNP", "RECENT", ".", "UnaNotaPerOgniImpegno", "0", " ", "0"))
        ckUnaNotaPerImpegno.Checked = CBool(IIf(bCheck = True, True, False))
      Else
        ckUnaNotaPerImpegno.Checked = False
        ckUnaNotaPerImpegno.Enabled = False
      End If

      '--------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl

      GctlSetRoules()
      GctlApplicaDefaultValue()

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMVEDTA1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If ckEvadiRigheaZero.Checked = True Then
        oMenu.SaveSettingBus("BSORGNNP", "RECENT", ".", "ScriviRighe0", "-1", " ", "NS.", "...", "...")
      Else
        oMenu.SaveSettingBus("BSORGNNP", "RECENT", ".", "ScriviRighe0", "0", " ", "NS.", "...", "...")
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Dim dttTmp As New DataTable
    Dim bVecchioPick As Boolean = False
    Try
      Me.ValidaLastControl()

      If edNumfat.Text = "0" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129107028752173572, "N. primo documento deve essere diverso da 0"))
        Return
      End If

      'il vecchio picking non gestisce i lotti alfanumerici
      If ckCreaPick.Checked Then
        oMenu.ValCodiceDb(DittaCorrente, DittaCorrente, "ANADITAC", "S", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then
          If NTSCStr(dttTmp.Rows(0)!ac_lotti2) = "S" Then
            ' Se sono attivati entrambi i moduli (o nessuno dei 2) faccio decidere alla chiave di registro chi ha priorità
            If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupRME) And CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtPIK) Then
              If oMenu.GetSettingBusDitt(DittaCorrente, "OPZIONI", ".", ".", "UsaNuovoPicking", "N", "", "N") = "N" Then bVecchioPick = True
            ElseIf CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupRME) Then
              bVecchioPick = False
            ElseIf CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtPIK) Then
              bVecchioPick = True
            Else
              If oMenu.GetSettingBusDitt(DittaCorrente, "OPZIONI", ".", ".", "UsaNuovoPicking", "N", "", "N") = "N" Then bVecchioPick = True
            End If
            If bVecchioPick Then
              oApp.MsgBoxErr(oApp.Tr(Me, 129519262436296160, "ATTENZIONE: con la gestione dei lotti alfanumerici abilitata (anagrafica ditta -> dati aggiuntivi contabilità 'Gestione lotti alfanumerici')" & vbCrLf & _
                                                              "non è possibile generare il picking con il modulo non in Terminal Server (Qualora siano abilitati sia il modulo di Picking in terminal server" & vbCrLf & _
                                                              "che la versione installata direttamente sui palmari verificare l'opzione di registro BUSINESS/OPZIONI/OPZIONI/UsaNuovoPicking)"))
              Return
            End If
          End If
        End If
      End If    'If ckCreaPick.Checked Then

      If Not oCleGnnp.DtacCheckSelection(edAnnofat.Text, edDatfat.Text) Then Return

      If NTSCStr(cbTipoRk.SelectedValue) = "W" Then
        oMenu.SaveSettingBus("BSORGNNP", "RECENT", ".", "CreaPicking", NTSCStr(IIf(ckCreaPick.Checked = True, "-1", "0")), " ", "NS.", "...", "...")
        oMenu.SaveSettingBus("BSORGNNP", "RECENT", ".", "GeneraPickingDistinti", NTSCStr(IIf(ckPickinglistdistinte.Checked = True, "-1", "0")), " ", "NS.", "...", "...")
      End If
      '----------------------------------------------------------------------------------------
      '--- Se il CheckBox relativo a 'Genera una nota per ogni impegno' è abilitato
      '--- vuol dire che è la form è stata chiamata da BSORGNNP e quindi salva il valore
      '--- impostato nel RECENT
      '----------------------------------------------------------------------------------------
      If ckUnaNotaPerImpegno.Enabled = True Then
        oCleGnnp.bDtacUnaNotaPerImpegno = CBool(IIf(ckUnaNotaPerImpegno.Checked = True, True, False))
        oMenu.SaveSettingBus("BSORGNNP", "RECENT", ".", "UnaNotaPerOgniImpegno", NTSCStr(IIf(ckUnaNotaPerImpegno.Checked = True, "-1", "0")), " ", "NS.", "...", "...")
      Else
        oCleGnnp.bDtacUnaNotaPerImpegno = False
      End If
      '----------------------------------------------------------------------------------------
      oCleGnnp.lDtacNumdoc = NTSCInt(edNumfat.Text)
      oCleGnnp.strDtacSerie = NTSCStr(IIf(edSerfat.Text = "", " ", edSerfat.Text))
      oCleGnnp.strDtacTipork = NTSCStr(cbTipoRk.SelectedValue)
      oCleGnnp.nDtacAnno = NTSCInt(edAnnofat.Text)
      oCleGnnp.dDtacDatdoc = NTSCDate(edDatfat.Text)
      oCleGnnp.strDtacDtiniz = edDtiniz.Text
      oCleGnnp.dDtacOriniz = NTSCDec(edOriniz.Text.Replace(".", ",").Replace(":", ","))
      oCleGnnp.nDtacCodvett = NTSCInt(edVettor.Text)
      oCleGnnp.strDtacAcuradi = NTSCStr(cbTrasp.SelectedValue)
      If ckPreesist.Checked Then
        oCleGnnp.bDtacVariaAcc = False
      Else
        oCleGnnp.bDtacVariaAcc = True
      End If
      oCleGnnp.bDtacCreaPicking = CBool(IIf(ckCreaPick.Checked = True, True, False))
      oCleGnnp.bDtacPickingDistinti = CBool(IIf(ckPickinglistdistinte.Checked = True, True, False))

      Me.Close()

      bOk = True

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      oCleGnnp.lDtacNumdoc = 0
      oCleGnnp.strDtacSerie = " "
      oCleGnnp.strDtacTipork = " "
      oCleGnnp.nDtacAnno = 0
      oCleGnnp.dDtacDatdoc = NTSCDate("01/01/1900")
      oCleGnnp.strDtacDtiniz = ""
      oCleGnnp.dDtacOriniz = 0
      oCleGnnp.nDtacCodvett = 0
      oCleGnnp.strDtacAcuradi = " "
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
      If oCleGnnp Is Nothing Then Return
      If Not oCleGnnp.edVettor_Validated(NTSCInt(edVettor.Text), strTmp) Then
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
      If oCleGnnp Is Nothing Then Return
      edAnnofat.Text = NTSCStr(Year(NTSCDate("01/01/" & edAnnofat.Text)))
      edNumfat.Text = NTSCStr(oCleGnnp.LegNuma(NTSCStr(cbTipoRk.SelectedValue), edSerfat.Text, NTSCInt(edAnnofat.Text)))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edSerfat_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSerfat.Validated
    Dim strTmp As String = ""
    Try
      If oCleGnnp Is Nothing Then Return

      strTmp = TestSerieMaxLen(edSerfat.Text, False)
      If strTmp <> edSerfat.Text Then edSerfat.Text = strTmp

      edAnnofat.Text = NTSCStr(Year(NTSCDate("01/01/" & edAnnofat.Text)))
      edNumfat.Text = NTSCStr(oCleGnnp.LegNuma(NTSCStr(cbTipoRk.SelectedValue), edSerfat.Text, NTSCInt(edAnnofat.Text)))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckCreaPick_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckCreaPick.CheckedChanged
    Try
      If ckCreaPick.Checked = True Then
        GctlSetVisEnab(ckPickinglistdistinte, False)
      Else
        ckPickinglistdistinte.Enabled = False
      End If
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

  Public Overridable Sub cbTipoRk_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTipoRk.SelectedValueChanged
    Try
      If NTSCStr(cbTipoRk.SelectedValue) = "W" Then
        If ckCreaPick.Enabled = False Then
          GctlSetVisEnab(ckCreaPick, False)
          If ckCreaPick.Checked = True Then GctlSetVisEnab(ckPickinglistdistinte, False)
        End If
      Else
        If ckCreaPick.Enabled = True Or ckPickinglistdistinte.Enabled = True Then
          ckCreaPick.Checked = False
          ckCreaPick.Enabled = False
          ckPickinglistdistinte.Checked = False
          ckPickinglistdistinte.Enabled = False
        End If
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
      edNumfat.Text = NTSCStr(oCleGnnp.LegNuma(NTSCStr(cbTipoRk.SelectedValue), edSerfat.Text, NTSCInt(edAnnofat.Text)))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

End Class
