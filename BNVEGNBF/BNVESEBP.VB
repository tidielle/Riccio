Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVESEBP

#Region "Variabili"
  Public oCallParams As CLE__CLDP
  Public bOk As Boolean
  Public oCleGnbf As CLEVEGNBF

  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents pnLeft As NTSInformatica.NTSPanel
  Public WithEvents frData As NTSInformatica.NTSGroupBox
  Public WithEvents ckTutto As NTSInformatica.NTSCheckBox
  Public WithEvents frClienti As NTSInformatica.NTSGroupBox
  Public WithEvents lbAlcliente As NTSInformatica.NTSLabel
  Public WithEvents opAlcuniClienti As NTSInformatica.NTSRadioButton
  Public WithEvents opTuttiClienti As NTSInformatica.NTSRadioButton
  Public WithEvents ckVistate As NTSInformatica.NTSCheckBox
  Public WithEvents ckConssoloasa As NTSInformatica.NTSCheckBox
  Public WithEvents edAlcliente As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDalcliente As NTSInformatica.NTSTextBoxNum
  Public WithEvents frAgente As NTSInformatica.NTSGroupBox
  Public WithEvents edCodcage As NTSInformatica.NTSTextBoxNum
  Public WithEvents opAlcuniAgenti As NTSInformatica.NTSRadioButton
  Public WithEvents opTuttiAgenti As NTSInformatica.NTSRadioButton
  Public WithEvents edAlladata As NTSInformatica.NTSTextBoxData
  Public WithEvents edDalladata As NTSInformatica.NTSTextBoxData
  Public WithEvents ckAnchealtre As NTSInformatica.NTSCheckBox
  Public WithEvents lbAlladata As NTSInformatica.NTSLabel
  Public WithEvents opAlcuneDate As NTSInformatica.NTSRadioButton
  Public WithEvents opTutteDate As NTSInformatica.NTSRadioButton
  Public WithEvents frTipoBolla As NTSInformatica.NTSGroupBox
  Public WithEvents lbXx_Tipobf As NTSInformatica.NTSLabel
  Public WithEvents edTipobf As NTSInformatica.NTSTextBoxNum
  Public WithEvents opAlcuneBolle As NTSInformatica.NTSRadioButton
  Public WithEvents opTutteBolle As NTSInformatica.NTSRadioButton
  Public WithEvents frZona As NTSInformatica.NTSGroupBox
  Public WithEvents lbXx_Codzona As NTSInformatica.NTSLabel
  Public WithEvents edCodzona As NTSInformatica.NTSTextBoxNum
  Public WithEvents opAlcuneZone As NTSInformatica.NTSRadioButton
  Public WithEvents opTutteZone As NTSInformatica.NTSRadioButton
  Public WithEvents edNumpar As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAnnoBolle As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAnnoBolle As NTSInformatica.NTSLabel
  Public WithEvents lbNumpar As NTSInformatica.NTSLabel
  Public WithEvents fmVettore As NTSInformatica.NTSGroupBox
  Public WithEvents lbVettore2 As NTSInformatica.NTSLabel
  Public WithEvents edVet2A As NTSInformatica.NTSTextBoxNum
  Public WithEvents edVet1A As NTSInformatica.NTSTextBoxNum
  Public WithEvents edVet2Da As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbVettore2A As NTSInformatica.NTSLabel
  Public WithEvents edVet1Da As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbVettore2Da As NTSInformatica.NTSLabel
  Public WithEvents lbVettore1A As NTSInformatica.NTSLabel
  Public WithEvents lbVettore1Da As NTSInformatica.NTSLabel
  Public WithEvents lbVettore1 As NTSInformatica.NTSLabel
  Public WithEvents lbXx_Codcage As NTSInformatica.NTSLabel
#End Region

  Public Overridable Sub InitializeComponent()
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.pnLeft = New NTSInformatica.NTSPanel
    Me.frAgente = New NTSInformatica.NTSGroupBox
    Me.lbXx_Codcage = New NTSInformatica.NTSLabel
    Me.edCodcage = New NTSInformatica.NTSTextBoxNum
    Me.opAlcuniAgenti = New NTSInformatica.NTSRadioButton
    Me.opTuttiAgenti = New NTSInformatica.NTSRadioButton
    Me.frClienti = New NTSInformatica.NTSGroupBox
    Me.edAlcliente = New NTSInformatica.NTSTextBoxNum
    Me.edDalcliente = New NTSInformatica.NTSTextBoxNum
    Me.lbAlcliente = New NTSInformatica.NTSLabel
    Me.opAlcuniClienti = New NTSInformatica.NTSRadioButton
    Me.opTuttiClienti = New NTSInformatica.NTSRadioButton
    Me.frData = New NTSInformatica.NTSGroupBox
    Me.edAlladata = New NTSInformatica.NTSTextBoxData
    Me.edDalladata = New NTSInformatica.NTSTextBoxData
    Me.ckAnchealtre = New NTSInformatica.NTSCheckBox
    Me.lbAlladata = New NTSInformatica.NTSLabel
    Me.opAlcuneDate = New NTSInformatica.NTSRadioButton
    Me.opTutteDate = New NTSInformatica.NTSRadioButton
    Me.ckTutto = New NTSInformatica.NTSCheckBox
    Me.ckVistate = New NTSInformatica.NTSCheckBox
    Me.ckConssoloasa = New NTSInformatica.NTSCheckBox
    Me.frTipoBolla = New NTSInformatica.NTSGroupBox
    Me.lbXx_Tipobf = New NTSInformatica.NTSLabel
    Me.edTipobf = New NTSInformatica.NTSTextBoxNum
    Me.opAlcuneBolle = New NTSInformatica.NTSRadioButton
    Me.opTutteBolle = New NTSInformatica.NTSRadioButton
    Me.frZona = New NTSInformatica.NTSGroupBox
    Me.lbXx_Codzona = New NTSInformatica.NTSLabel
    Me.edCodzona = New NTSInformatica.NTSTextBoxNum
    Me.opAlcuneZone = New NTSInformatica.NTSRadioButton
    Me.opTutteZone = New NTSInformatica.NTSRadioButton
    Me.edNumpar = New NTSInformatica.NTSTextBoxNum
    Me.edAnnoBolle = New NTSInformatica.NTSTextBoxNum
    Me.lbAnnoBolle = New NTSInformatica.NTSLabel
    Me.lbNumpar = New NTSInformatica.NTSLabel
    Me.fmVettore = New NTSInformatica.NTSGroupBox
    Me.lbVettore2 = New NTSInformatica.NTSLabel
    Me.edVet2A = New NTSInformatica.NTSTextBoxNum
    Me.edVet1A = New NTSInformatica.NTSTextBoxNum
    Me.edVet2Da = New NTSInformatica.NTSTextBoxNum
    Me.lbVettore2A = New NTSInformatica.NTSLabel
    Me.edVet1Da = New NTSInformatica.NTSTextBoxNum
    Me.lbVettore2Da = New NTSInformatica.NTSLabel
    Me.lbVettore1A = New NTSInformatica.NTSLabel
    Me.lbVettore1Da = New NTSInformatica.NTSLabel
    Me.lbVettore1 = New NTSInformatica.NTSLabel
    CType(Me.pnLeft, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnLeft.SuspendLayout()
    CType(Me.frAgente, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frAgente.SuspendLayout()
    CType(Me.edCodcage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opAlcuniAgenti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opTuttiAgenti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frClienti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frClienti.SuspendLayout()
    CType(Me.edAlcliente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDalcliente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opAlcuniClienti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opTuttiClienti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frData, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frData.SuspendLayout()
    CType(Me.edAlladata.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDalladata.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAnchealtre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opAlcuneDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opTutteDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTutto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckVistate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckConssoloasa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frTipoBolla, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frTipoBolla.SuspendLayout()
    CType(Me.edTipobf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opAlcuneBolle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opTutteBolle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frZona, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frZona.SuspendLayout()
    CType(Me.edCodzona.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opAlcuneZone.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opTutteZone.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumpar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnnoBolle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmVettore, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmVettore.SuspendLayout()
    CType(Me.edVet2A.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edVet1A.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edVet2Da.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edVet1Da.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.cmdAnnulla.Location = New System.Drawing.Point(568, 34)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(105, 22)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(568, 8)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(105, 22)
    Me.cmdOk.TabIndex = 2
    Me.cmdOk.Text = "&Conferma"
    '
    'pnLeft
    '
    Me.pnLeft.AllowDrop = True
    Me.pnLeft.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnLeft.Appearance.Options.UseBackColor = True
    Me.pnLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnLeft.Controls.Add(Me.frAgente)
    Me.pnLeft.Controls.Add(Me.frClienti)
    Me.pnLeft.Controls.Add(Me.frData)
    Me.pnLeft.Controls.Add(Me.ckTutto)
    Me.pnLeft.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnLeft.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnLeft.Location = New System.Drawing.Point(0, 0)
    Me.pnLeft.Name = "pnLeft"
    Me.pnLeft.NTSActiveTrasparency = True
    Me.pnLeft.Size = New System.Drawing.Size(346, 300)
    Me.pnLeft.TabIndex = 10
    Me.pnLeft.Text = "NtsPanel1"
    '
    'frAgente
    '
    Me.frAgente.AllowDrop = True
    Me.frAgente.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frAgente.Appearance.Options.UseBackColor = True
    Me.frAgente.Controls.Add(Me.lbXx_Codcage)
    Me.frAgente.Controls.Add(Me.edCodcage)
    Me.frAgente.Controls.Add(Me.opAlcuniAgenti)
    Me.frAgente.Controls.Add(Me.opTuttiAgenti)
    Me.frAgente.Cursor = System.Windows.Forms.Cursors.Default
    Me.frAgente.Location = New System.Drawing.Point(12, 215)
    Me.frAgente.Name = "frAgente"
    Me.frAgente.Size = New System.Drawing.Size(321, 75)
    Me.frAgente.TabIndex = 15
    Me.frAgente.Text = "Agente"
    '
    'lbXx_Codcage
    '
    Me.lbXx_Codcage.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codcage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codcage.Location = New System.Drawing.Point(150, 48)
    Me.lbXx_Codcage.Name = "lbXx_Codcage"
    Me.lbXx_Codcage.NTSDbField = ""
    Me.lbXx_Codcage.Size = New System.Drawing.Size(165, 20)
    Me.lbXx_Codcage.TabIndex = 666
    Me.lbXx_Codcage.Tooltip = ""
    Me.lbXx_Codcage.UseMnemonic = False
    '
    'edCodcage
    '
    Me.edCodcage.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodcage.Enabled = False
    Me.edCodcage.Location = New System.Drawing.Point(88, 48)
    Me.edCodcage.Name = "edCodcage"
    Me.edCodcage.NTSDbField = ""
    Me.edCodcage.NTSFormat = "0"
    Me.edCodcage.NTSForzaVisZoom = False
    Me.edCodcage.NTSOldValue = ""
    Me.edCodcage.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodcage.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodcage.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodcage.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodcage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodcage.Properties.MaxLength = 65536
    Me.edCodcage.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodcage.Size = New System.Drawing.Size(56, 20)
    Me.edCodcage.TabIndex = 15
    '
    'opAlcuniAgenti
    '
    Me.opAlcuniAgenti.Cursor = System.Windows.Forms.Cursors.Default
    Me.opAlcuniAgenti.Location = New System.Drawing.Point(5, 49)
    Me.opAlcuniAgenti.Name = "opAlcuniAgenti"
    Me.opAlcuniAgenti.NTSCheckValue = "S"
    Me.opAlcuniAgenti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opAlcuniAgenti.Properties.Appearance.Options.UseBackColor = True
    Me.opAlcuniAgenti.Properties.Caption = "Agente"
    Me.opAlcuniAgenti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opAlcuniAgenti.Size = New System.Drawing.Size(77, 19)
    Me.opAlcuniAgenti.TabIndex = 57
    '
    'opTuttiAgenti
    '
    Me.opTuttiAgenti.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTuttiAgenti.EditValue = True
    Me.opTuttiAgenti.Location = New System.Drawing.Point(5, 25)
    Me.opTuttiAgenti.Name = "opTuttiAgenti"
    Me.opTuttiAgenti.NTSCheckValue = "S"
    Me.opTuttiAgenti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTuttiAgenti.Properties.Appearance.Options.UseBackColor = True
    Me.opTuttiAgenti.Properties.Caption = "Tutti"
    Me.opTuttiAgenti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTuttiAgenti.Size = New System.Drawing.Size(49, 19)
    Me.opTuttiAgenti.TabIndex = 56
    '
    'frClienti
    '
    Me.frClienti.AllowDrop = True
    Me.frClienti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frClienti.Appearance.Options.UseBackColor = True
    Me.frClienti.Controls.Add(Me.edAlcliente)
    Me.frClienti.Controls.Add(Me.edDalcliente)
    Me.frClienti.Controls.Add(Me.lbAlcliente)
    Me.frClienti.Controls.Add(Me.opAlcuniClienti)
    Me.frClienti.Controls.Add(Me.opTuttiClienti)
    Me.frClienti.Cursor = System.Windows.Forms.Cursors.Default
    Me.frClienti.Location = New System.Drawing.Point(12, 134)
    Me.frClienti.Name = "frClienti"
    Me.frClienti.Size = New System.Drawing.Size(321, 75)
    Me.frClienti.TabIndex = 14
    Me.frClienti.Text = "Clienti"
    '
    'edAlcliente
    '
    Me.edAlcliente.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAlcliente.Enabled = False
    Me.edAlcliente.Location = New System.Drawing.Point(232, 48)
    Me.edAlcliente.Name = "edAlcliente"
    Me.edAlcliente.NTSDbField = ""
    Me.edAlcliente.NTSFormat = "0"
    Me.edAlcliente.NTSForzaVisZoom = False
    Me.edAlcliente.NTSOldValue = ""
    Me.edAlcliente.Properties.Appearance.Options.UseTextOptions = True
    Me.edAlcliente.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAlcliente.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAlcliente.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAlcliente.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAlcliente.Properties.MaxLength = 65536
    Me.edAlcliente.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAlcliente.Size = New System.Drawing.Size(83, 20)
    Me.edAlcliente.TabIndex = 60
    '
    'edDalcliente
    '
    Me.edDalcliente.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDalcliente.Enabled = False
    Me.edDalcliente.Location = New System.Drawing.Point(88, 48)
    Me.edDalcliente.Name = "edDalcliente"
    Me.edDalcliente.NTSDbField = ""
    Me.edDalcliente.NTSFormat = "0"
    Me.edDalcliente.NTSForzaVisZoom = False
    Me.edDalcliente.NTSOldValue = ""
    Me.edDalcliente.Properties.Appearance.Options.UseTextOptions = True
    Me.edDalcliente.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDalcliente.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDalcliente.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDalcliente.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDalcliente.Properties.MaxLength = 65536
    Me.edDalcliente.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDalcliente.Size = New System.Drawing.Size(83, 20)
    Me.edDalcliente.TabIndex = 15
    '
    'lbAlcliente
    '
    Me.lbAlcliente.AutoSize = True
    Me.lbAlcliente.BackColor = System.Drawing.Color.Transparent
    Me.lbAlcliente.Enabled = False
    Me.lbAlcliente.Location = New System.Drawing.Point(177, 51)
    Me.lbAlcliente.Name = "lbAlcliente"
    Me.lbAlcliente.NTSDbField = ""
    Me.lbAlcliente.Size = New System.Drawing.Size(49, 13)
    Me.lbAlcliente.TabIndex = 58
    Me.lbAlcliente.Text = "al cliente"
    Me.lbAlcliente.Tooltip = ""
    Me.lbAlcliente.UseMnemonic = False
    '
    'opAlcuniClienti
    '
    Me.opAlcuniClienti.Cursor = System.Windows.Forms.Cursors.Default
    Me.opAlcuniClienti.Location = New System.Drawing.Point(5, 49)
    Me.opAlcuniClienti.Name = "opAlcuniClienti"
    Me.opAlcuniClienti.NTSCheckValue = "S"
    Me.opAlcuniClienti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opAlcuniClienti.Properties.Appearance.Options.UseBackColor = True
    Me.opAlcuniClienti.Properties.Caption = "Dal cliente"
    Me.opAlcuniClienti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opAlcuniClienti.Size = New System.Drawing.Size(77, 19)
    Me.opAlcuniClienti.TabIndex = 57
    '
    'opTuttiClienti
    '
    Me.opTuttiClienti.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTuttiClienti.EditValue = True
    Me.opTuttiClienti.Location = New System.Drawing.Point(5, 25)
    Me.opTuttiClienti.Name = "opTuttiClienti"
    Me.opTuttiClienti.NTSCheckValue = "S"
    Me.opTuttiClienti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTuttiClienti.Properties.Appearance.Options.UseBackColor = True
    Me.opTuttiClienti.Properties.Caption = "Tutti"
    Me.opTuttiClienti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTuttiClienti.Size = New System.Drawing.Size(49, 19)
    Me.opTuttiClienti.TabIndex = 56
    '
    'frData
    '
    Me.frData.AllowDrop = True
    Me.frData.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frData.Appearance.Options.UseBackColor = True
    Me.frData.Controls.Add(Me.edAlladata)
    Me.frData.Controls.Add(Me.edDalladata)
    Me.frData.Controls.Add(Me.ckAnchealtre)
    Me.frData.Controls.Add(Me.lbAlladata)
    Me.frData.Controls.Add(Me.opAlcuneDate)
    Me.frData.Controls.Add(Me.opTutteDate)
    Me.frData.Cursor = System.Windows.Forms.Cursors.Default
    Me.frData.Location = New System.Drawing.Point(12, 36)
    Me.frData.Name = "frData"
    Me.frData.Size = New System.Drawing.Size(321, 92)
    Me.frData.TabIndex = 13
    Me.frData.Text = "Data"
    '
    'edAlladata
    '
    Me.edAlladata.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAlladata.Enabled = False
    Me.edAlladata.Location = New System.Drawing.Point(232, 44)
    Me.edAlladata.Name = "edAlladata"
    Me.edAlladata.NTSDbField = ""
    Me.edAlladata.NTSForzaVisZoom = False
    Me.edAlladata.NTSOldValue = ""
    Me.edAlladata.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAlladata.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAlladata.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAlladata.Properties.MaxLength = 65536
    Me.edAlladata.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAlladata.Size = New System.Drawing.Size(83, 20)
    Me.edAlladata.TabIndex = 68
    '
    'edDalladata
    '
    Me.edDalladata.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDalladata.Enabled = False
    Me.edDalladata.Location = New System.Drawing.Point(88, 43)
    Me.edDalladata.Name = "edDalladata"
    Me.edDalladata.NTSDbField = ""
    Me.edDalladata.NTSForzaVisZoom = False
    Me.edDalladata.NTSOldValue = ""
    Me.edDalladata.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDalladata.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDalladata.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDalladata.Properties.MaxLength = 65536
    Me.edDalladata.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDalladata.Size = New System.Drawing.Size(83, 20)
    Me.edDalladata.TabIndex = 67
    '
    'ckAnchealtre
    '
    Me.ckAnchealtre.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAnchealtre.Enabled = False
    Me.ckAnchealtre.Location = New System.Drawing.Point(5, 69)
    Me.ckAnchealtre.Name = "ckAnchealtre"
    Me.ckAnchealtre.NTSCheckValue = "S"
    Me.ckAnchealtre.NTSUnCheckValue = "N"
    Me.ckAnchealtre.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAnchealtre.Properties.Appearance.Options.UseBackColor = True
    Me.ckAnchealtre.Properties.Caption = "Anche altre note sui clienti selezionati per data"
    Me.ckAnchealtre.Size = New System.Drawing.Size(247, 19)
    Me.ckAnchealtre.TabIndex = 66
    '
    'lbAlladata
    '
    Me.lbAlladata.AutoSize = True
    Me.lbAlladata.BackColor = System.Drawing.Color.Transparent
    Me.lbAlladata.Enabled = False
    Me.lbAlladata.Location = New System.Drawing.Point(177, 46)
    Me.lbAlladata.Name = "lbAlladata"
    Me.lbAlladata.NTSDbField = ""
    Me.lbAlladata.Size = New System.Drawing.Size(48, 13)
    Me.lbAlladata.TabIndex = 64
    Me.lbAlladata.Text = "alla data"
    Me.lbAlladata.Tooltip = ""
    Me.lbAlladata.UseMnemonic = False
    '
    'opAlcuneDate
    '
    Me.opAlcuneDate.Cursor = System.Windows.Forms.Cursors.Default
    Me.opAlcuneDate.Location = New System.Drawing.Point(5, 44)
    Me.opAlcuneDate.Name = "opAlcuneDate"
    Me.opAlcuneDate.NTSCheckValue = "S"
    Me.opAlcuneDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opAlcuneDate.Properties.Appearance.Options.UseBackColor = True
    Me.opAlcuneDate.Properties.Caption = "Dalla data"
    Me.opAlcuneDate.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opAlcuneDate.Size = New System.Drawing.Size(77, 19)
    Me.opAlcuneDate.TabIndex = 63
    '
    'opTutteDate
    '
    Me.opTutteDate.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTutteDate.EditValue = True
    Me.opTutteDate.Location = New System.Drawing.Point(5, 23)
    Me.opTutteDate.Name = "opTutteDate"
    Me.opTutteDate.NTSCheckValue = "S"
    Me.opTutteDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTutteDate.Properties.Appearance.Options.UseBackColor = True
    Me.opTutteDate.Properties.Caption = "Qualsiasi"
    Me.opTutteDate.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTutteDate.Size = New System.Drawing.Size(65, 19)
    Me.opTutteDate.TabIndex = 62
    '
    'ckTutto
    '
    Me.ckTutto.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTutto.Location = New System.Drawing.Point(12, 10)
    Me.ckTutto.Name = "ckTutto"
    Me.ckTutto.NTSCheckValue = "S"
    Me.ckTutto.NTSUnCheckValue = "N"
    Me.ckTutto.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTutto.Properties.Appearance.Options.UseBackColor = True
    Me.ckTutto.Properties.Caption = "Tutte le note esistenti"
    Me.ckTutto.Size = New System.Drawing.Size(126, 19)
    Me.ckTutto.TabIndex = 12
    '
    'ckVistate
    '
    Me.ckVistate.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckVistate.Location = New System.Drawing.Point(352, 12)
    Me.ckVistate.Name = "ckVistate"
    Me.ckVistate.NTSCheckValue = "S"
    Me.ckVistate.NTSUnCheckValue = "N"
    Me.ckVistate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckVistate.Properties.Appearance.Options.UseBackColor = True
    Me.ckVistate.Properties.Caption = "Solo Note di prelievo vistate"
    Me.ckVistate.Size = New System.Drawing.Size(157, 19)
    Me.ckVistate.TabIndex = 13
    '
    'ckConssoloasa
    '
    Me.ckConssoloasa.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckConssoloasa.Location = New System.Drawing.Point(352, 32)
    Me.ckConssoloasa.Name = "ckConssoloasa"
    Me.ckConssoloasa.NTSCheckValue = "S"
    Me.ckConssoloasa.NTSUnCheckValue = "N"
    Me.ckConssoloasa.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckConssoloasa.Properties.Appearance.Options.UseBackColor = True
    Me.ckConssoloasa.Properties.Caption = "Considera 'Solo a saldo' su ord."
    Me.ckConssoloasa.Size = New System.Drawing.Size(174, 19)
    Me.ckConssoloasa.TabIndex = 14
    '
    'frTipoBolla
    '
    Me.frTipoBolla.AllowDrop = True
    Me.frTipoBolla.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frTipoBolla.Appearance.Options.UseBackColor = True
    Me.frTipoBolla.Controls.Add(Me.lbXx_Tipobf)
    Me.frTipoBolla.Controls.Add(Me.edTipobf)
    Me.frTipoBolla.Controls.Add(Me.opAlcuneBolle)
    Me.frTipoBolla.Controls.Add(Me.opTutteBolle)
    Me.frTipoBolla.Cursor = System.Windows.Forms.Cursors.Default
    Me.frTipoBolla.Location = New System.Drawing.Point(352, 83)
    Me.frTipoBolla.Name = "frTipoBolla"
    Me.frTipoBolla.Size = New System.Drawing.Size(321, 65)
    Me.frTipoBolla.TabIndex = 16
    Me.frTipoBolla.Text = "Tipo bolla/fattura: "
    '
    'lbXx_Tipobf
    '
    Me.lbXx_Tipobf.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Tipobf.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Tipobf.Location = New System.Drawing.Point(153, 40)
    Me.lbXx_Tipobf.Name = "lbXx_Tipobf"
    Me.lbXx_Tipobf.NTSDbField = ""
    Me.lbXx_Tipobf.Size = New System.Drawing.Size(162, 20)
    Me.lbXx_Tipobf.TabIndex = 666
    Me.lbXx_Tipobf.Tooltip = ""
    Me.lbXx_Tipobf.UseMnemonic = False
    '
    'edTipobf
    '
    Me.edTipobf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTipobf.Enabled = False
    Me.edTipobf.Location = New System.Drawing.Point(91, 40)
    Me.edTipobf.Name = "edTipobf"
    Me.edTipobf.NTSDbField = ""
    Me.edTipobf.NTSFormat = "0"
    Me.edTipobf.NTSForzaVisZoom = False
    Me.edTipobf.NTSOldValue = ""
    Me.edTipobf.Properties.Appearance.Options.UseTextOptions = True
    Me.edTipobf.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTipobf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTipobf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTipobf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTipobf.Properties.MaxLength = 65536
    Me.edTipobf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTipobf.Size = New System.Drawing.Size(56, 20)
    Me.edTipobf.TabIndex = 15
    '
    'opAlcuneBolle
    '
    Me.opAlcuneBolle.Cursor = System.Windows.Forms.Cursors.Default
    Me.opAlcuneBolle.Location = New System.Drawing.Point(5, 40)
    Me.opAlcuneBolle.Name = "opAlcuneBolle"
    Me.opAlcuneBolle.NTSCheckValue = "S"
    Me.opAlcuneBolle.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opAlcuneBolle.Properties.Appearance.Options.UseBackColor = True
    Me.opAlcuneBolle.Properties.Caption = "Bolla/fattura"
    Me.opAlcuneBolle.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opAlcuneBolle.Size = New System.Drawing.Size(85, 19)
    Me.opAlcuneBolle.TabIndex = 57
    '
    'opTutteBolle
    '
    Me.opTutteBolle.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTutteBolle.EditValue = True
    Me.opTutteBolle.Location = New System.Drawing.Point(5, 21)
    Me.opTutteBolle.Name = "opTutteBolle"
    Me.opTutteBolle.NTSCheckValue = "S"
    Me.opTutteBolle.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTutteBolle.Properties.Appearance.Options.UseBackColor = True
    Me.opTutteBolle.Properties.Caption = "Tutte"
    Me.opTutteBolle.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTutteBolle.Size = New System.Drawing.Size(49, 19)
    Me.opTutteBolle.TabIndex = 56
    '
    'frZona
    '
    Me.frZona.AllowDrop = True
    Me.frZona.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frZona.Appearance.Options.UseBackColor = True
    Me.frZona.Controls.Add(Me.lbXx_Codzona)
    Me.frZona.Controls.Add(Me.edCodzona)
    Me.frZona.Controls.Add(Me.opAlcuneZone)
    Me.frZona.Controls.Add(Me.opTutteZone)
    Me.frZona.Cursor = System.Windows.Forms.Cursors.Default
    Me.frZona.Location = New System.Drawing.Point(352, 152)
    Me.frZona.Name = "frZona"
    Me.frZona.Size = New System.Drawing.Size(321, 65)
    Me.frZona.TabIndex = 17
    Me.frZona.Text = "Zona"
    '
    'lbXx_Codzona
    '
    Me.lbXx_Codzona.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codzona.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codzona.Location = New System.Drawing.Point(153, 40)
    Me.lbXx_Codzona.Name = "lbXx_Codzona"
    Me.lbXx_Codzona.NTSDbField = ""
    Me.lbXx_Codzona.Size = New System.Drawing.Size(162, 20)
    Me.lbXx_Codzona.TabIndex = 666
    Me.lbXx_Codzona.Tooltip = ""
    Me.lbXx_Codzona.UseMnemonic = False
    '
    'edCodzona
    '
    Me.edCodzona.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodzona.Enabled = False
    Me.edCodzona.Location = New System.Drawing.Point(91, 40)
    Me.edCodzona.Name = "edCodzona"
    Me.edCodzona.NTSDbField = ""
    Me.edCodzona.NTSFormat = "0"
    Me.edCodzona.NTSForzaVisZoom = False
    Me.edCodzona.NTSOldValue = ""
    Me.edCodzona.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodzona.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodzona.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodzona.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodzona.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodzona.Properties.MaxLength = 65536
    Me.edCodzona.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodzona.Size = New System.Drawing.Size(56, 20)
    Me.edCodzona.TabIndex = 15
    '
    'opAlcuneZone
    '
    Me.opAlcuneZone.Cursor = System.Windows.Forms.Cursors.Default
    Me.opAlcuneZone.Location = New System.Drawing.Point(5, 41)
    Me.opAlcuneZone.Name = "opAlcuneZone"
    Me.opAlcuneZone.NTSCheckValue = "S"
    Me.opAlcuneZone.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opAlcuneZone.Properties.Appearance.Options.UseBackColor = True
    Me.opAlcuneZone.Properties.Caption = "Zona"
    Me.opAlcuneZone.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opAlcuneZone.Size = New System.Drawing.Size(77, 19)
    Me.opAlcuneZone.TabIndex = 57
    '
    'opTutteZone
    '
    Me.opTutteZone.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTutteZone.EditValue = True
    Me.opTutteZone.Location = New System.Drawing.Point(5, 21)
    Me.opTutteZone.Name = "opTutteZone"
    Me.opTutteZone.NTSCheckValue = "S"
    Me.opTutteZone.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTutteZone.Properties.Appearance.Options.UseBackColor = True
    Me.opTutteZone.Properties.Caption = "Tutti"
    Me.opTutteZone.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTutteZone.Size = New System.Drawing.Size(49, 19)
    Me.opTutteZone.TabIndex = 56
    '
    'edNumpar
    '
    Me.edNumpar.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumpar.Location = New System.Drawing.Point(599, 60)
    Me.edNumpar.Name = "edNumpar"
    Me.edNumpar.NTSDbField = ""
    Me.edNumpar.NTSFormat = "0"
    Me.edNumpar.NTSForzaVisZoom = False
    Me.edNumpar.NTSOldValue = ""
    Me.edNumpar.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumpar.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumpar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNumpar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNumpar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumpar.Properties.MaxLength = 65536
    Me.edNumpar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumpar.Size = New System.Drawing.Size(74, 20)
    Me.edNumpar.TabIndex = 18
    '
    'edAnnoBolle
    '
    Me.edAnnoBolle.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnnoBolle.Location = New System.Drawing.Point(417, 60)
    Me.edAnnoBolle.Name = "edAnnoBolle"
    Me.edAnnoBolle.NTSDbField = ""
    Me.edAnnoBolle.NTSFormat = "0"
    Me.edAnnoBolle.NTSForzaVisZoom = False
    Me.edAnnoBolle.NTSOldValue = ""
    Me.edAnnoBolle.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnnoBolle.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnnoBolle.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnnoBolle.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnnoBolle.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnnoBolle.Properties.MaxLength = 65536
    Me.edAnnoBolle.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnnoBolle.Size = New System.Drawing.Size(65, 20)
    Me.edAnnoBolle.TabIndex = 19
    '
    'lbAnnoBolle
    '
    Me.lbAnnoBolle.AutoSize = True
    Me.lbAnnoBolle.BackColor = System.Drawing.Color.Transparent
    Me.lbAnnoBolle.Location = New System.Drawing.Point(354, 63)
    Me.lbAnnoBolle.Name = "lbAnnoBolle"
    Me.lbAnnoBolle.NTSDbField = ""
    Me.lbAnnoBolle.Size = New System.Drawing.Size(57, 13)
    Me.lbAnnoBolle.TabIndex = 65
    Me.lbAnnoBolle.Text = "Anno note"
    Me.lbAnnoBolle.Tooltip = ""
    Me.lbAnnoBolle.UseMnemonic = False
    '
    'lbNumpar
    '
    Me.lbNumpar.AutoSize = True
    Me.lbNumpar.BackColor = System.Drawing.Color.Transparent
    Me.lbNumpar.Location = New System.Drawing.Point(488, 63)
    Me.lbNumpar.Name = "lbNumpar"
    Me.lbNumpar.NTSDbField = ""
    Me.lbNumpar.Size = New System.Drawing.Size(105, 13)
    Me.lbNumpar.TabIndex = 66
    Me.lbNumpar.Text = "Num. partita/viaggio"
    Me.lbNumpar.Tooltip = ""
    Me.lbNumpar.UseMnemonic = False
    '
    'fmVettore
    '
    Me.fmVettore.AllowDrop = True
    Me.fmVettore.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmVettore.Appearance.Options.UseBackColor = True
    Me.fmVettore.Controls.Add(Me.lbVettore2)
    Me.fmVettore.Controls.Add(Me.edVet2A)
    Me.fmVettore.Controls.Add(Me.edVet1A)
    Me.fmVettore.Controls.Add(Me.edVet2Da)
    Me.fmVettore.Controls.Add(Me.lbVettore2A)
    Me.fmVettore.Controls.Add(Me.edVet1Da)
    Me.fmVettore.Controls.Add(Me.lbVettore2Da)
    Me.fmVettore.Controls.Add(Me.lbVettore1A)
    Me.fmVettore.Controls.Add(Me.lbVettore1Da)
    Me.fmVettore.Controls.Add(Me.lbVettore1)
    Me.fmVettore.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmVettore.Location = New System.Drawing.Point(352, 221)
    Me.fmVettore.Name = "fmVettore"
    Me.fmVettore.Size = New System.Drawing.Size(321, 69)
    Me.fmVettore.TabIndex = 67
    Me.fmVettore.Text = "Vettori"
    '
    'lbVettore2
    '
    Me.lbVettore2.AutoSize = True
    Me.lbVettore2.BackColor = System.Drawing.Color.Transparent
    Me.lbVettore2.Location = New System.Drawing.Point(7, 48)
    Me.lbVettore2.Name = "lbVettore2"
    Me.lbVettore2.NTSDbField = ""
    Me.lbVettore2.Size = New System.Drawing.Size(52, 13)
    Me.lbVettore2.TabIndex = 0
    Me.lbVettore2.Text = "Vettore 2"
    Me.lbVettore2.Tooltip = ""
    Me.lbVettore2.UseMnemonic = False
    '
    'edVet2A
    '
    Me.edVet2A.Cursor = System.Windows.Forms.Cursors.Default
    Me.edVet2A.EditValue = "9999"
    Me.edVet2A.Location = New System.Drawing.Point(186, 45)
    Me.edVet2A.Name = "edVet2A"
    Me.edVet2A.NTSDbField = ""
    Me.edVet2A.NTSFormat = "0"
    Me.edVet2A.NTSForzaVisZoom = False
    Me.edVet2A.NTSOldValue = "9999"
    Me.edVet2A.Properties.Appearance.Options.UseTextOptions = True
    Me.edVet2A.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edVet2A.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edVet2A.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edVet2A.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edVet2A.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edVet2A.Size = New System.Drawing.Size(66, 20)
    Me.edVet2A.TabIndex = 15
    '
    'edVet1A
    '
    Me.edVet1A.Cursor = System.Windows.Forms.Cursors.Default
    Me.edVet1A.EditValue = "9999"
    Me.edVet1A.Location = New System.Drawing.Point(186, 22)
    Me.edVet1A.Name = "edVet1A"
    Me.edVet1A.NTSDbField = ""
    Me.edVet1A.NTSFormat = "0"
    Me.edVet1A.NTSForzaVisZoom = False
    Me.edVet1A.NTSOldValue = "9999"
    Me.edVet1A.Properties.Appearance.Options.UseTextOptions = True
    Me.edVet1A.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edVet1A.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edVet1A.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edVet1A.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edVet1A.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edVet1A.Size = New System.Drawing.Size(66, 20)
    Me.edVet1A.TabIndex = 15
    '
    'edVet2Da
    '
    Me.edVet2Da.Cursor = System.Windows.Forms.Cursors.Default
    Me.edVet2Da.EditValue = "0"
    Me.edVet2Da.Location = New System.Drawing.Point(91, 45)
    Me.edVet2Da.Name = "edVet2Da"
    Me.edVet2Da.NTSDbField = ""
    Me.edVet2Da.NTSFormat = "0"
    Me.edVet2Da.NTSForzaVisZoom = False
    Me.edVet2Da.NTSOldValue = ""
    Me.edVet2Da.Properties.Appearance.Options.UseTextOptions = True
    Me.edVet2Da.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edVet2Da.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edVet2Da.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edVet2Da.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edVet2Da.Properties.MaxLength = 65536
    Me.edVet2Da.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edVet2Da.Size = New System.Drawing.Size(66, 20)
    Me.edVet2Da.TabIndex = 15
    '
    'lbVettore2A
    '
    Me.lbVettore2A.AutoSize = True
    Me.lbVettore2A.BackColor = System.Drawing.Color.Transparent
    Me.lbVettore2A.Location = New System.Drawing.Point(167, 48)
    Me.lbVettore2A.Name = "lbVettore2A"
    Me.lbVettore2A.NTSDbField = ""
    Me.lbVettore2A.Size = New System.Drawing.Size(13, 13)
    Me.lbVettore2A.TabIndex = 0
    Me.lbVettore2A.Text = "a"
    Me.lbVettore2A.Tooltip = ""
    Me.lbVettore2A.UseMnemonic = False
    '
    'edVet1Da
    '
    Me.edVet1Da.Cursor = System.Windows.Forms.Cursors.Default
    Me.edVet1Da.EditValue = "0"
    Me.edVet1Da.Location = New System.Drawing.Point(91, 22)
    Me.edVet1Da.Name = "edVet1Da"
    Me.edVet1Da.NTSDbField = ""
    Me.edVet1Da.NTSFormat = "0"
    Me.edVet1Da.NTSForzaVisZoom = False
    Me.edVet1Da.NTSOldValue = ""
    Me.edVet1Da.Properties.Appearance.Options.UseTextOptions = True
    Me.edVet1Da.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edVet1Da.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edVet1Da.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edVet1Da.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edVet1Da.Properties.MaxLength = 65536
    Me.edVet1Da.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edVet1Da.Size = New System.Drawing.Size(66, 20)
    Me.edVet1Da.TabIndex = 15
    '
    'lbVettore2Da
    '
    Me.lbVettore2Da.AutoSize = True
    Me.lbVettore2Da.BackColor = System.Drawing.Color.Transparent
    Me.lbVettore2Da.Location = New System.Drawing.Point(65, 48)
    Me.lbVettore2Da.Name = "lbVettore2Da"
    Me.lbVettore2Da.NTSDbField = ""
    Me.lbVettore2Da.Size = New System.Drawing.Size(19, 13)
    Me.lbVettore2Da.TabIndex = 0
    Me.lbVettore2Da.Text = "da"
    Me.lbVettore2Da.Tooltip = ""
    Me.lbVettore2Da.UseMnemonic = False
    '
    'lbVettore1A
    '
    Me.lbVettore1A.AutoSize = True
    Me.lbVettore1A.BackColor = System.Drawing.Color.Transparent
    Me.lbVettore1A.Location = New System.Drawing.Point(167, 25)
    Me.lbVettore1A.Name = "lbVettore1A"
    Me.lbVettore1A.NTSDbField = ""
    Me.lbVettore1A.Size = New System.Drawing.Size(13, 13)
    Me.lbVettore1A.TabIndex = 0
    Me.lbVettore1A.Text = "a"
    Me.lbVettore1A.Tooltip = ""
    Me.lbVettore1A.UseMnemonic = False
    '
    'lbVettore1Da
    '
    Me.lbVettore1Da.AutoSize = True
    Me.lbVettore1Da.BackColor = System.Drawing.Color.Transparent
    Me.lbVettore1Da.Location = New System.Drawing.Point(65, 25)
    Me.lbVettore1Da.Name = "lbVettore1Da"
    Me.lbVettore1Da.NTSDbField = ""
    Me.lbVettore1Da.Size = New System.Drawing.Size(19, 13)
    Me.lbVettore1Da.TabIndex = 0
    Me.lbVettore1Da.Text = "da"
    Me.lbVettore1Da.Tooltip = ""
    Me.lbVettore1Da.UseMnemonic = False
    '
    'lbVettore1
    '
    Me.lbVettore1.AutoSize = True
    Me.lbVettore1.BackColor = System.Drawing.Color.Transparent
    Me.lbVettore1.Location = New System.Drawing.Point(7, 25)
    Me.lbVettore1.Name = "lbVettore1"
    Me.lbVettore1.NTSDbField = ""
    Me.lbVettore1.Size = New System.Drawing.Size(52, 13)
    Me.lbVettore1.TabIndex = 0
    Me.lbVettore1.Text = "Vettore 1"
    Me.lbVettore1.Tooltip = ""
    Me.lbVettore1.UseMnemonic = False
    '
    'FRMVESEBP
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(682, 300)
    Me.Controls.Add(Me.fmVettore)
    Me.Controls.Add(Me.lbNumpar)
    Me.Controls.Add(Me.lbAnnoBolle)
    Me.Controls.Add(Me.edAnnoBolle)
    Me.Controls.Add(Me.edNumpar)
    Me.Controls.Add(Me.frZona)
    Me.Controls.Add(Me.frTipoBolla)
    Me.Controls.Add(Me.ckConssoloasa)
    Me.Controls.Add(Me.ckVistate)
    Me.Controls.Add(Me.pnLeft)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMVESEBP"
    Me.Text = "SELEZIONA NOTE DI PRELIEVO"
    CType(Me.pnLeft, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnLeft.ResumeLayout(False)
    Me.pnLeft.PerformLayout()
    CType(Me.frAgente, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frAgente.ResumeLayout(False)
    Me.frAgente.PerformLayout()
    CType(Me.edCodcage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opAlcuniAgenti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opTuttiAgenti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frClienti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frClienti.ResumeLayout(False)
    Me.frClienti.PerformLayout()
    CType(Me.edAlcliente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDalcliente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opAlcuniClienti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opTuttiClienti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frData, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frData.ResumeLayout(False)
    Me.frData.PerformLayout()
    CType(Me.edAlladata.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDalladata.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAnchealtre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opAlcuneDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opTutteDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTutto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckVistate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckConssoloasa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frTipoBolla, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frTipoBolla.ResumeLayout(False)
    Me.frTipoBolla.PerformLayout()
    CType(Me.edTipobf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opAlcuneBolle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opTutteBolle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frZona, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frZona.ResumeLayout(False)
    Me.frZona.PerformLayout()
    CType(Me.edCodzona.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opAlcuneZone.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opTutteZone.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNumpar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnnoBolle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmVettore, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmVettore.ResumeLayout(False)
    Me.fmVettore.PerformLayout()
    CType(Me.edVet2A.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edVet1A.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edVet2Da.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edVet1Da.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

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
      edAnnoBolle.NTSSetParam(oMenu, oApp.Tr(Me, 128711260081511461, "Anno note"), "0", 4, 0, 9999)
      edNumpar.NTSSetParam(oMenu, oApp.Tr(Me, 128711260081667711, "Num. partita/viaggio"), "0", 9, 0, 999999999)
      edCodzona.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128711260081980211, "Zona"), tabzone)
      opAlcuneZone.NTSSetParam(oMenu, oApp.Tr(Me, 128711260082136461, "Zona"), "S")
      opTutteZone.NTSSetParam(oMenu, oApp.Tr(Me, 128711260082292711, "Tutti"), "S")
      edTipobf.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128711260082448961, "Bolla/fattura"), tabtpbf)
      opAlcuneBolle.NTSSetParam(oMenu, oApp.Tr(Me, 128711260082605211, "Bolla/fattura"), "S")
      opTutteBolle.NTSSetParam(oMenu, oApp.Tr(Me, 128829896172095914, "Tutte"), "S")
      ckConssoloasa.NTSSetParam(oMenu, oApp.Tr(Me, 128829896211476962, "Considera 'Solo a saldo' su ord."), "S", "N")
      ckVistate.NTSSetParam(oMenu, oApp.Tr(Me, 128711260082917711, "Solo Note di prelievo vistate"), "S", "N")
      edCodcage.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128711260083230211, "Agente"), tabcage)
      opAlcuniAgenti.NTSSetParam(oMenu, oApp.Tr(Me, 128711260083386461, "Agente"), "S")
      opTuttiAgenti.NTSSetParam(oMenu, oApp.Tr(Me, 128829896317899556, "Tutti"), "S")
      edAlcliente.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128711260083542711, "al cliente"), tabanagrac)
      edDalcliente.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128711260083698961, "Dal cliente"), tabanagrac)
      opAlcuniClienti.NTSSetParam(oMenu, oApp.Tr(Me, 128711260084011461, "Dal cliente"), "S")
      opTuttiClienti.NTSSetParam(oMenu, oApp.Tr(Me, 128829896412757874, "Tutti"), "S")
      edAlladata.NTSSetParam(oMenu, oApp.Tr(Me, 128711260084167711, "alla data"), True)
      edDalladata.NTSSetParam(oMenu, oApp.Tr(Me, 128711260084323961, "Dalla data"), True)
      ckAnchealtre.NTSSetParam(oMenu, oApp.Tr(Me, 128711260084480211, "Anche altre note sui clienti selezionati per data"), "S", "N")
      opAlcuneDate.NTSSetParam(oMenu, oApp.Tr(Me, 128711260084792711, "Dalla data"), "S")
      opTutteDate.NTSSetParam(oMenu, oApp.Tr(Me, 128829896494489176, "Qualsiasi"), "S")
      ckTutto.NTSSetParam(oMenu, oApp.Tr(Me, 128711260084948961, "Tutte le note esistenti"), "S", "N")
      edVet2A.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130237987840580370, "Vettore 2"), tabvett)
      edVet1A.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130237987840590353, "Vettore 1"), tabvett)
      edVet2Da.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130237987840600378, "Vettore 2"), tabvett)
      edVet1Da.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130237987840620394, "Vettore 1"), tabvett)

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

  Public Overridable Sub FRMVESEBP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      bOk = False
      oCleGnbf.strSebpQuery = ""

      oCleGnbf.nSepbEscomp = Year(Now)
      edAnnoBolle.Text = NTSCStr(oCleGnbf.nSepbEscomp)
      edNumpar.Text = "0"

      ckAnchealtre.Checked = False
      ckAnchealtre.Enabled = False

      '--------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl

      GctlSetRoules()
      GctlApplicaDefaultValue()

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMVESEBP_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      Me.ValidaLastControl()

      If Not oCleGnbf.CheckSelection(edDalcliente.Text, edAlcliente.Text, edDalladata.Text, edAlladata.Text) Then Return

      oCleGnbf.ComponiQuerySebp(edAnnoBolle.Text, ckTutto.Checked, opTuttiClienti.Checked, edDalcliente.Text, _
                                edAlcliente.Text, opTutteBolle.Checked, edTipobf.Text, opTuttiAgenti.Checked, _
                                edCodcage.Text, opTutteZone.Checked, edCodzona.Text, ckVistate.Checked, _
                                edNumpar.Text, opTutteDate.Checked, ckAnchealtre.Checked, edDalladata.Text, _
                                edAlladata.Text, ckConssoloasa.Checked, NTSCInt(edVet1Da.Text), NTSCInt(edVet1A.Text), _
                                NTSCInt(edVet2Da.Text), NTSCInt(edVet2A.Text))

      bOk = True
      Me.Close()

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub Zoom()
    Dim ctlLastControl As Control
    Dim ctrlTmp As Control = Nothing
    Dim oParam As New CLE__PATB

    Try
      ctlLastControl = NTSFindControlFocused(Me)
      If ctlLastControl Is Nothing Then Return

      If edDalcliente.Focused Then
        '----------------------------------------------
        'zoom specifico per mastri di contabilità
        SetFastZoom(edDalcliente.Text, oParam) 'gestione dello zoom veloce
        oParam.bTipoProposto = False
        NTSZOOM.strIn = edDalcliente.Text
        NTSZOOM.ZoomStrIn("ZOOMANAGRAC", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edDalcliente.Text Then edDalcliente.Text = NTSZOOM.strIn
      ElseIf edAlcliente.Focused Then
        '----------------------------------------------
        'zoom specifico per mastri di contabilità
        SetFastZoom(edAlcliente.Text, oParam) 'gestione dello zoom veloce
        oParam.bTipoProposto = False
        NTSZOOM.strIn = edAlcliente.Text
        NTSZOOM.ZoomStrIn("ZOOMANAGRAC", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edAlcliente.Text Then edAlcliente.Text = NTSZOOM.strIn
      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        'SendKeys.SendWait("{F5}") 'se faccio questa riga va in un loop infinito....
        NTSCallStandardZoom()
      End If
      '------------------------------------
      'riporto il focus sul controllo che ha chiamato lo zoom
      ctlLastControl.Focus()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edCodcage_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodcage.ValidatedAndChanged
    Dim strTmp As String = ""
    Try
      If oCleGnbf Is Nothing Then Return
      If Not oCleGnbf.edCodcage_Validated(NTSCInt(edCodcage.Text), strTmp) Then
        edCodcage.Text = NTSCStr(edCodcage.OldEditValue)
      Else
        lbXx_Codcage.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edTipobf_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTipobf.ValidatedAndChanged
    Dim strTmp As String = ""
    Try
      If oCleGnbf Is Nothing Then Return
      If Not oCleGnbf.edTipoBf_Validated(NTSCInt(edTipobf.Text), strTmp) Then
        edTipobf.Text = NTSCStr(edTipobf.OldEditValue)
      Else
        lbXx_Tipobf.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edCodzona_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodzona.ValidatedAndChanged
    Dim strTmp As String = ""
    Try
      If oCleGnbf Is Nothing Then Return
      If Not oCleGnbf.edCodzona_Validated(NTSCInt(edCodzona.Text), strTmp) Then
        edCodzona.Text = NTSCStr(edCodzona.OldEditValue)
      Else
        lbXx_Codzona.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edDalcliente_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDalcliente.ValidatedAndChanged
    Try
      If NTSCInt(edDalcliente.Text) <> 0 Then edAlcliente.Text = edDalcliente.Text
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edVet1Da_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edVet1Da.ValidatedAndChanged
    Try
      If NTSCInt(edVet1Da.Text) <> 0 Then edVet1A.Text = edVet1Da.Text
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edVet2Da_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edVet2Da.ValidatedAndChanged
    Try
      If NTSCInt(edVet2Da.Text) <> 0 Then edVet2A.Text = edVet2Da.Text
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckTutto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckTutto.CheckedChanged
    Try
      If ckTutto.Checked = True Then
        edAnnoBolle.Text = NTSCStr(oCleGnbf.nSepbEscomp)
        edAnnoBolle.Enabled = False
        frData.Enabled = False
        frTipoBolla.Enabled = False
        frClienti.Enabled = False
        frAgente.Enabled = False
        frZona.Enabled = False
        fmVettore.Enabled = False
        opTuttiClienti.Checked = True
        opTutteBolle.Checked = True
        opTuttiAgenti.Checked = True
        opTutteZone.Checked = True
        opTutteDate.Checked = True
        opTuttiClienti.Enabled = False
        opTutteBolle.Enabled = False
        opTuttiAgenti.Enabled = False
        opTutteZone.Enabled = False
        opTutteDate.Enabled = False
        opAlcuniClienti.Enabled = False
        opAlcuneBolle.Enabled = False
        opAlcuniAgenti.Enabled = False
        opAlcuneZone.Enabled = False
        opAlcuneDate.Enabled = False
        lbAnnoBolle.Enabled = False
        lbAlladata.Enabled = False
        lbAlcliente.Enabled = False
        ckVistate.Enabled = False
        ckVistate.Checked = False
        edVet1A.Enabled = False
        edVet2A.Enabled = False
        edVet1Da.Enabled = False
        edVet2Da.Enabled = False
      Else
        edAnnoBolle.Enabled = True
        edAnnoBolle.Text = NTSCStr(oCleGnbf.nSepbEscomp)
        frData.Enabled = True
        frTipoBolla.Enabled = True
        frClienti.Enabled = True
        frAgente.Enabled = True
        frZona.Enabled = True
        fmVettore.Enabled = True
        opTuttiClienti.Enabled = True
        opTutteBolle.Enabled = True
        opTuttiAgenti.Enabled = True
        opTutteDate.Enabled = True
        opTutteZone.Enabled = True
        opAlcuniClienti.Enabled = True
        opAlcuneBolle.Enabled = True
        opAlcuniAgenti.Enabled = True
        opAlcuneZone.Enabled = True
        opAlcuneDate.Enabled = True
        lbAnnoBolle.Enabled = True
        lbAlladata.Enabled = True
        lbAlcliente.Enabled = True
        ckVistate.Enabled = True
        edVet1A.Enabled = True
        edVet2A.Enabled = True
        edVet1Da.Enabled = True
        edVet2Da.Enabled = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opAlcuneBolle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opAlcuneBolle.CheckedChanged
    Try
      If opAlcuneBolle.Checked = True Then
        edTipobf.Enabled = True
        edTipobf.Text = "0"
        lbXx_Tipobf.Text = ""
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opTutteBolle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opTutteBolle.CheckedChanged
    Try
      If opTutteBolle.Checked = True Then
        edTipobf.Text = "0"
        lbXx_Tipobf.Text = ""
        edTipobf.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opAlcuneDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opAlcuneDate.CheckedChanged
    Try
      If opAlcuneDate.Checked = True Then
        edDalladata.Enabled = True
        edAlladata.Enabled = True
        lbAlladata.Enabled = True
        edDalladata.Text = NTSCStr(NTSCDate(IntSetDate("01/" & Microsoft.VisualBasic.Right("00" & Month(Now), 2) & "/" & Year(Now))))
        edAlladata.Text = NTSCDate(Now).ToShortDateString
        ckAnchealtre.Enabled = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opTutteDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opTutteDate.CheckedChanged
    Try
      If opTutteDate.Checked = True Then
        edDalladata.Text = ""
        edAlladata.Text = ""
        edDalladata.Enabled = False
        edAlladata.Enabled = False
        lbAlladata.Enabled = False
        ckAnchealtre.Checked = False
        ckAnchealtre.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opAlcuneZone_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opAlcuneZone.CheckedChanged
    Try
      If opAlcuneZone.Checked = True Then
        edCodzona.Enabled = True
        edCodzona.Text = "0"
        lbXx_Codzona.Text = ""
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opTutteZone_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opTutteZone.CheckedChanged
    Try
      If opTutteZone.Checked = True Then
        edCodzona.Text = "0"
        lbXx_Codzona.Text = ""
        edCodzona.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opAlcuniAgenti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opAlcuniAgenti.CheckedChanged
    Try
      If opAlcuniAgenti.Checked = True Then
        edCodcage.Enabled = True
        edCodcage.Text = "0"
        lbXx_Codcage.Text = ""
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opTuttiAgenti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opTuttiAgenti.CheckedChanged
    Try
      If opTuttiAgenti.Checked = True Then
        edCodcage.Text = "0"
        lbXx_Codcage.Text = ""
        edCodcage.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opAlcuniClienti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opAlcuniClienti.CheckedChanged
    Try
      If opAlcuniClienti.Checked = True Then
        edDalcliente.Enabled = True
        edAlcliente.Enabled = True
        lbAlcliente.Enabled = True
        edDalcliente.Text = "0"
        edAlcliente.Text = "99999999"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opTuttiClienti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opTuttiClienti.CheckedChanged
    Try
      If opTuttiClienti.Checked = True Then
        edDalcliente.Text = "0"
        edAlcliente.Text = "99999999"
        edDalcliente.Enabled = False
        edAlcliente.Enabled = False
        lbAlcliente.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overrides Function NTSGetDataAutocompletamento(ByVal strTabName As String, ByVal strDescr As String, _
      ByVal IsCrmUser As Boolean, ByRef dsOut As DataSet) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      If edDalcliente.ContainsFocus Or edAlcliente.ContainsFocus Then strTabName = "ANAGRA_CLI"
      '--------------------------------------------------------------------------------------------------------------
      Return MyBase.NTSGetDataAutocompletamento(strTabName, strDescr, IsCrmUser, dsOut)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
End Class
