Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORSEIC

#Region "Variabili"
  Public oCallParams As CLE__CLDP
  Public bOk As Boolean
  Public oCleGnnp As CLEORGNNP

  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents pnRight As NTSInformatica.NTSPanel
  Public WithEvents frData As NTSInformatica.NTSGroupBox
  Public WithEvents frClienti As NTSInformatica.NTSGroupBox
  Public WithEvents lbAlcliente As NTSInformatica.NTSLabel
  Public WithEvents opAlcuniClienti As NTSInformatica.NTSRadioButton
  Public WithEvents opTuttiClienti As NTSInformatica.NTSRadioButton
  Public WithEvents ckConssoloasa As NTSInformatica.NTSCheckBox
  Public WithEvents ckConf As NTSInformatica.NTSCheckBox
  Public WithEvents edAlcliente As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDalcliente As NTSInformatica.NTSTextBoxNum
  Public WithEvents frAgente As NTSInformatica.NTSGroupBox
  Public WithEvents edCodcage As NTSInformatica.NTSTextBoxNum
  Public WithEvents opAlcuniAgenti As NTSInformatica.NTSRadioButton
  Public WithEvents opTuttiAgenti As NTSInformatica.NTSRadioButton
  Public WithEvents edAlladata As NTSInformatica.NTSTextBoxData
  Public WithEvents edDalladata As NTSInformatica.NTSTextBoxData
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
  Public WithEvents NtsGroupBox1 As NTSInformatica.NTSGroupBox
  Public WithEvents lbCommecaini As NTSInformatica.NTSLabel
  Public WithEvents lbCommecafin As NTSInformatica.NTSLabel
  Public WithEvents cmdSelCodart As NTSInformatica.NTSButton
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents ckTutto As NTSInformatica.NTSCheckBox
  Public WithEvents lbInfo As NTSInformatica.NTSLabel
  Public WithEvents frPriorita As NTSInformatica.NTSGroupBox
  Public WithEvents opPriorAffidab As NTSInformatica.NTSRadioButton
  Public WithEvents opPriorNumero As NTSInformatica.NTSRadioButton
  Public WithEvents opPriorCliente As NTSInformatica.NTSRadioButton
  Public WithEvents opPriorData As NTSInformatica.NTSRadioButton
  Public WithEvents frImpegni As NTSInformatica.NTSGroupBox
  Public WithEvents opImp2 As NTSInformatica.NTSRadioButton
  Public WithEvents opImp1 As NTSInformatica.NTSRadioButton
  Public WithEvents opImp0 As NTSInformatica.NTSRadioButton
  Public WithEvents lbCodmaga As NTSInformatica.NTSLabel
  Public WithEvents edCodmaga As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbMaxnumord As NTSInformatica.NTSLabel
  Public WithEvents edMaxnumord As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbADataCons As NTSInformatica.NTSLabel
  Public WithEvents edADataCons As NTSInformatica.NTSTextBoxData
  Public WithEvents edSerie As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbSerie As NTSInformatica.NTSLabel
  Public WithEvents lbSerieLabel As NTSInformatica.NTSLabel
  Public WithEvents edCommecafin As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCommecaini As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_Codcage As NTSInformatica.NTSLabel
#End Region

  Public Overridable Sub InitializeComponent()
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.pnRight = New NTSInformatica.NTSPanel
    Me.cmdSelCodart = New NTSInformatica.NTSButton
    Me.cmdSeleziona = New NTSInformatica.NTSButton
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
    Me.lbAlladata = New NTSInformatica.NTSLabel
    Me.opAlcuneDate = New NTSInformatica.NTSRadioButton
    Me.opTutteDate = New NTSInformatica.NTSRadioButton
    Me.ckConssoloasa = New NTSInformatica.NTSCheckBox
    Me.ckConf = New NTSInformatica.NTSCheckBox
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
    Me.NtsGroupBox1 = New NTSInformatica.NTSGroupBox
    Me.edCommecafin = New NTSInformatica.NTSTextBoxNum
    Me.edCommecaini = New NTSInformatica.NTSTextBoxNum
    Me.lbCommecaini = New NTSInformatica.NTSLabel
    Me.lbCommecafin = New NTSInformatica.NTSLabel
    Me.ckTutto = New NTSInformatica.NTSCheckBox
    Me.lbInfo = New NTSInformatica.NTSLabel
    Me.frPriorita = New NTSInformatica.NTSGroupBox
    Me.opPriorAffidab = New NTSInformatica.NTSRadioButton
    Me.opPriorNumero = New NTSInformatica.NTSRadioButton
    Me.opPriorCliente = New NTSInformatica.NTSRadioButton
    Me.opPriorData = New NTSInformatica.NTSRadioButton
    Me.frImpegni = New NTSInformatica.NTSGroupBox
    Me.opImp2 = New NTSInformatica.NTSRadioButton
    Me.opImp1 = New NTSInformatica.NTSRadioButton
    Me.opImp0 = New NTSInformatica.NTSRadioButton
    Me.lbCodmaga = New NTSInformatica.NTSLabel
    Me.edCodmaga = New NTSInformatica.NTSTextBoxNum
    Me.lbMaxnumord = New NTSInformatica.NTSLabel
    Me.edMaxnumord = New NTSInformatica.NTSTextBoxNum
    Me.lbADataCons = New NTSInformatica.NTSLabel
    Me.edADataCons = New NTSInformatica.NTSTextBoxData
    Me.edSerie = New NTSInformatica.NTSTextBoxStr
    Me.lbSerie = New NTSInformatica.NTSLabel
    Me.lbSerieLabel = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnRight.SuspendLayout()
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
    CType(Me.opAlcuneDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opTutteDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckConssoloasa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckConf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    CType(Me.NtsGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsGroupBox1.SuspendLayout()
    CType(Me.edCommecafin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCommecaini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTutto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frPriorita, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frPriorita.SuspendLayout()
    CType(Me.opPriorAffidab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opPriorNumero.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opPriorCliente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opPriorData.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frImpegni, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frImpegni.SuspendLayout()
    CType(Me.opImp2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opImp1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opImp0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodmaga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edMaxnumord.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edADataCons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.cmdAnnulla.Cursor = System.Windows.Forms.Cursors.Default
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(3, 32)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(150, 22)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
    Me.cmdOk.ImagePath = ""
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(3, 3)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.NTSContextMenu = Nothing
    Me.cmdOk.Size = New System.Drawing.Size(150, 22)
    Me.cmdOk.TabIndex = 2
    Me.cmdOk.Text = "&Conferma"
    '
    'pnRight
    '
    Me.pnRight.AllowDrop = True
    Me.pnRight.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnRight.Appearance.Options.UseBackColor = True
    Me.pnRight.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnRight.Controls.Add(Me.cmdSelCodart)
    Me.pnRight.Controls.Add(Me.cmdSeleziona)
    Me.pnRight.Controls.Add(Me.cmdOk)
    Me.pnRight.Controls.Add(Me.cmdAnnulla)
    Me.pnRight.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.pnRight.Location = New System.Drawing.Point(501, 12)
    Me.pnRight.Name = "pnRight"
    Me.pnRight.NTSActiveTrasparency = True
    Me.pnRight.Size = New System.Drawing.Size(159, 117)
    Me.pnRight.TabIndex = 10
    Me.pnRight.Text = "NtsPanel1"
    '
    'cmdSelCodart
    '
    Me.cmdSelCodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.cmdSelCodart.ImagePath = ""
    Me.cmdSelCodart.ImageText = ""
    Me.cmdSelCodart.Location = New System.Drawing.Point(3, 88)
    Me.cmdSelCodart.Name = "cmdSelCodart"
    Me.cmdSelCodart.NTSContextMenu = Nothing
    Me.cmdSelCodart.Size = New System.Drawing.Size(150, 22)
    Me.cmdSelCodart.TabIndex = 4
    Me.cmdSelCodart.Text = "S&el. per articolo e procedi"
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.Cursor = System.Windows.Forms.Cursors.Default
    Me.cmdSeleziona.ImagePath = ""
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(3, 60)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.NTSContextMenu = Nothing
    Me.cmdSeleziona.Size = New System.Drawing.Size(150, 22)
    Me.cmdSeleziona.TabIndex = 3
    Me.cmdSeleziona.Text = "Sel.per &N°ordine e procedi"
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
    Me.frAgente.Location = New System.Drawing.Point(339, 244)
    Me.frAgente.Name = "frAgente"
    Me.frAgente.Size = New System.Drawing.Size(321, 75)
    Me.frAgente.TabIndex = 15
    Me.frAgente.Text = "Agente"
    '
    'lbXx_Codcage
    '
    Me.lbXx_Codcage.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codcage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codcage.Location = New System.Drawing.Point(162, 48)
    Me.lbXx_Codcage.Name = "lbXx_Codcage"
    Me.lbXx_Codcage.NTSDbField = ""
    Me.lbXx_Codcage.Size = New System.Drawing.Size(153, 20)
    Me.lbXx_Codcage.TabIndex = 666
    Me.lbXx_Codcage.Tooltip = ""
    Me.lbXx_Codcage.UseMnemonic = False
    '
    'edCodcage
    '
    Me.edCodcage.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodcage.Enabled = False
    Me.edCodcage.Location = New System.Drawing.Point(100, 48)
    Me.edCodcage.Name = "edCodcage"
    Me.edCodcage.NTSDbField = ""
    Me.edCodcage.NTSFormat = "0"
    Me.edCodcage.NTSForzaVisZoom = False
    Me.edCodcage.NTSOldValue = ""
    Me.edCodcage.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodcage.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodcage.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodcage.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodcage.Properties.AutoHeight = False
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
    Me.opAlcuniAgenti.Properties.AutoHeight = False
    Me.opAlcuniAgenti.Properties.Caption = "Agente"
    Me.opAlcuniAgenti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opAlcuniAgenti.Size = New System.Drawing.Size(64, 19)
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
    Me.opTuttiAgenti.Properties.AutoHeight = False
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
    Me.frClienti.Location = New System.Drawing.Point(12, 163)
    Me.frClienti.Name = "frClienti"
    Me.frClienti.Size = New System.Drawing.Size(321, 75)
    Me.frClienti.TabIndex = 14
    Me.frClienti.Text = "Clienti/Fornitori"
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
    Me.edAlcliente.Properties.AutoHeight = False
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
    Me.edDalcliente.Properties.AutoHeight = False
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
    Me.opAlcuniClienti.Properties.AutoHeight = False
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
    Me.opTuttiClienti.Properties.AutoHeight = False
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
    Me.frData.Controls.Add(Me.lbAlladata)
    Me.frData.Controls.Add(Me.opAlcuneDate)
    Me.frData.Controls.Add(Me.opTutteDate)
    Me.frData.Cursor = System.Windows.Forms.Cursors.Default
    Me.frData.Location = New System.Drawing.Point(12, 244)
    Me.frData.Name = "frData"
    Me.frData.Size = New System.Drawing.Size(321, 75)
    Me.frData.TabIndex = 13
    Me.frData.Text = "Data impegno"
    '
    'edAlladata
    '
    Me.edAlladata.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAlladata.Enabled = False
    Me.edAlladata.Location = New System.Drawing.Point(232, 44)
    Me.edAlladata.Name = "edAlladata"
    Me.edAlladata.NTSDbField = ""
    Me.edAlladata.NTSForzaVisZoom = False
    Me.edAlladata.NTSOldValue = ""
    Me.edAlladata.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAlladata.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAlladata.Properties.AutoHeight = False
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
    Me.edDalladata.Properties.AutoHeight = False
    Me.edDalladata.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDalladata.Properties.MaxLength = 65536
    Me.edDalladata.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDalladata.Size = New System.Drawing.Size(83, 20)
    Me.edDalladata.TabIndex = 67
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
    Me.opAlcuneDate.Properties.AutoHeight = False
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
    Me.opTutteDate.Properties.AutoHeight = False
    Me.opTutteDate.Properties.Caption = "Qualsiasi"
    Me.opTutteDate.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTutteDate.Size = New System.Drawing.Size(65, 19)
    Me.opTutteDate.TabIndex = 62
    '
    'ckConssoloasa
    '
    Me.ckConssoloasa.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckConssoloasa.Location = New System.Drawing.Point(297, 111)
    Me.ckConssoloasa.Name = "ckConssoloasa"
    Me.ckConssoloasa.NTSCheckValue = "S"
    Me.ckConssoloasa.NTSUnCheckValue = "N"
    Me.ckConssoloasa.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckConssoloasa.Properties.Appearance.Options.UseBackColor = True
    Me.ckConssoloasa.Properties.AutoHeight = False
    Me.ckConssoloasa.Properties.Caption = "Considera solo a saldo"
    Me.ckConssoloasa.Size = New System.Drawing.Size(138, 19)
    Me.ckConssoloasa.TabIndex = 13
    '
    'ckConf
    '
    Me.ckConf.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckConf.Location = New System.Drawing.Point(297, 132)
    Me.ckConf.Name = "ckConf"
    Me.ckConf.NTSCheckValue = "S"
    Me.ckConf.NTSUnCheckValue = "N"
    Me.ckConf.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckConf.Properties.Appearance.Options.UseBackColor = True
    Me.ckConf.Properties.AutoHeight = False
    Me.ckConf.Properties.Caption = "&Solo impegni confermati"
    Me.ckConf.Size = New System.Drawing.Size(138, 19)
    Me.ckConf.TabIndex = 14
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
    Me.frTipoBolla.Location = New System.Drawing.Point(339, 163)
    Me.frTipoBolla.Name = "frTipoBolla"
    Me.frTipoBolla.Size = New System.Drawing.Size(321, 75)
    Me.frTipoBolla.TabIndex = 16
    Me.frTipoBolla.Text = "Tipo bolla/fattura: "
    '
    'lbXx_Tipobf
    '
    Me.lbXx_Tipobf.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Tipobf.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Tipobf.Location = New System.Drawing.Point(162, 48)
    Me.lbXx_Tipobf.Name = "lbXx_Tipobf"
    Me.lbXx_Tipobf.NTSDbField = ""
    Me.lbXx_Tipobf.Size = New System.Drawing.Size(153, 20)
    Me.lbXx_Tipobf.TabIndex = 666
    Me.lbXx_Tipobf.Tooltip = ""
    Me.lbXx_Tipobf.UseMnemonic = False
    '
    'edTipobf
    '
    Me.edTipobf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTipobf.Enabled = False
    Me.edTipobf.Location = New System.Drawing.Point(100, 48)
    Me.edTipobf.Name = "edTipobf"
    Me.edTipobf.NTSDbField = ""
    Me.edTipobf.NTSFormat = "0"
    Me.edTipobf.NTSForzaVisZoom = False
    Me.edTipobf.NTSOldValue = ""
    Me.edTipobf.Properties.Appearance.Options.UseTextOptions = True
    Me.edTipobf.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTipobf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTipobf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTipobf.Properties.AutoHeight = False
    Me.edTipobf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTipobf.Properties.MaxLength = 65536
    Me.edTipobf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTipobf.Size = New System.Drawing.Size(56, 20)
    Me.edTipobf.TabIndex = 15
    '
    'opAlcuneBolle
    '
    Me.opAlcuneBolle.Cursor = System.Windows.Forms.Cursors.Default
    Me.opAlcuneBolle.Location = New System.Drawing.Point(5, 49)
    Me.opAlcuneBolle.Name = "opAlcuneBolle"
    Me.opAlcuneBolle.NTSCheckValue = "S"
    Me.opAlcuneBolle.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opAlcuneBolle.Properties.Appearance.Options.UseBackColor = True
    Me.opAlcuneBolle.Properties.AutoHeight = False
    Me.opAlcuneBolle.Properties.Caption = "Bolla/fattura"
    Me.opAlcuneBolle.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opAlcuneBolle.Size = New System.Drawing.Size(89, 19)
    Me.opAlcuneBolle.TabIndex = 57
    '
    'opTutteBolle
    '
    Me.opTutteBolle.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTutteBolle.EditValue = True
    Me.opTutteBolle.Location = New System.Drawing.Point(5, 25)
    Me.opTutteBolle.Name = "opTutteBolle"
    Me.opTutteBolle.NTSCheckValue = "S"
    Me.opTutteBolle.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTutteBolle.Properties.Appearance.Options.UseBackColor = True
    Me.opTutteBolle.Properties.AutoHeight = False
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
    Me.frZona.Location = New System.Drawing.Point(339, 325)
    Me.frZona.Name = "frZona"
    Me.frZona.Size = New System.Drawing.Size(321, 75)
    Me.frZona.TabIndex = 17
    Me.frZona.Text = "Zona"
    '
    'lbXx_Codzona
    '
    Me.lbXx_Codzona.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codzona.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codzona.Location = New System.Drawing.Point(162, 48)
    Me.lbXx_Codzona.Name = "lbXx_Codzona"
    Me.lbXx_Codzona.NTSDbField = ""
    Me.lbXx_Codzona.Size = New System.Drawing.Size(153, 20)
    Me.lbXx_Codzona.TabIndex = 666
    Me.lbXx_Codzona.Tooltip = ""
    Me.lbXx_Codzona.UseMnemonic = False
    '
    'edCodzona
    '
    Me.edCodzona.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodzona.Enabled = False
    Me.edCodzona.Location = New System.Drawing.Point(100, 48)
    Me.edCodzona.Name = "edCodzona"
    Me.edCodzona.NTSDbField = ""
    Me.edCodzona.NTSFormat = "0"
    Me.edCodzona.NTSForzaVisZoom = False
    Me.edCodzona.NTSOldValue = ""
    Me.edCodzona.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodzona.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodzona.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodzona.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodzona.Properties.AutoHeight = False
    Me.edCodzona.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodzona.Properties.MaxLength = 65536
    Me.edCodzona.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodzona.Size = New System.Drawing.Size(56, 20)
    Me.edCodzona.TabIndex = 15
    '
    'opAlcuneZone
    '
    Me.opAlcuneZone.Cursor = System.Windows.Forms.Cursors.Default
    Me.opAlcuneZone.Location = New System.Drawing.Point(5, 49)
    Me.opAlcuneZone.Name = "opAlcuneZone"
    Me.opAlcuneZone.NTSCheckValue = "S"
    Me.opAlcuneZone.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opAlcuneZone.Properties.Appearance.Options.UseBackColor = True
    Me.opAlcuneZone.Properties.AutoHeight = False
    Me.opAlcuneZone.Properties.Caption = "Zona"
    Me.opAlcuneZone.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opAlcuneZone.Size = New System.Drawing.Size(49, 19)
    Me.opAlcuneZone.TabIndex = 57
    '
    'opTutteZone
    '
    Me.opTutteZone.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTutteZone.EditValue = True
    Me.opTutteZone.Location = New System.Drawing.Point(5, 25)
    Me.opTutteZone.Name = "opTutteZone"
    Me.opTutteZone.NTSCheckValue = "S"
    Me.opTutteZone.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTutteZone.Properties.Appearance.Options.UseBackColor = True
    Me.opTutteZone.Properties.AutoHeight = False
    Me.opTutteZone.Properties.Caption = "Tutti"
    Me.opTutteZone.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTutteZone.Size = New System.Drawing.Size(49, 19)
    Me.opTutteZone.TabIndex = 56
    '
    'NtsGroupBox1
    '
    Me.NtsGroupBox1.AllowDrop = True
    Me.NtsGroupBox1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsGroupBox1.Appearance.Options.UseBackColor = True
    Me.NtsGroupBox1.Controls.Add(Me.edCommecafin)
    Me.NtsGroupBox1.Controls.Add(Me.edCommecaini)
    Me.NtsGroupBox1.Controls.Add(Me.lbCommecaini)
    Me.NtsGroupBox1.Controls.Add(Me.lbCommecafin)
    Me.NtsGroupBox1.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsGroupBox1.Location = New System.Drawing.Point(12, 325)
    Me.NtsGroupBox1.Name = "NtsGroupBox1"
    Me.NtsGroupBox1.Size = New System.Drawing.Size(321, 75)
    Me.NtsGroupBox1.TabIndex = 15
    Me.NtsGroupBox1.Text = "Commessa"
    '
    'edCommecafin
    '
    Me.edCommecafin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCommecafin.Location = New System.Drawing.Point(233, 36)
    Me.edCommecafin.Name = "edCommecafin"
    Me.edCommecafin.NTSDbField = ""
    Me.edCommecafin.NTSFormat = "0"
    Me.edCommecafin.NTSForzaVisZoom = False
    Me.edCommecafin.NTSOldValue = ""
    Me.edCommecafin.Properties.Appearance.Options.UseTextOptions = True
    Me.edCommecafin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCommecafin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCommecafin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCommecafin.Properties.AutoHeight = False
    Me.edCommecafin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCommecafin.Properties.MaxLength = 65536
    Me.edCommecafin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCommecafin.Size = New System.Drawing.Size(83, 20)
    Me.edCommecafin.TabIndex = 71
    '
    'edCommecaini
    '
    Me.edCommecaini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCommecaini.Location = New System.Drawing.Point(77, 36)
    Me.edCommecaini.Name = "edCommecaini"
    Me.edCommecaini.NTSDbField = ""
    Me.edCommecaini.NTSFormat = "0"
    Me.edCommecaini.NTSForzaVisZoom = False
    Me.edCommecaini.NTSOldValue = ""
    Me.edCommecaini.Properties.Appearance.Options.UseTextOptions = True
    Me.edCommecaini.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCommecaini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCommecaini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCommecaini.Properties.AutoHeight = False
    Me.edCommecaini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCommecaini.Properties.MaxLength = 65536
    Me.edCommecaini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCommecaini.Size = New System.Drawing.Size(83, 20)
    Me.edCommecaini.TabIndex = 70
    '
    'lbCommecaini
    '
    Me.lbCommecaini.AutoSize = True
    Me.lbCommecaini.BackColor = System.Drawing.Color.Transparent
    Me.lbCommecaini.Location = New System.Drawing.Point(4, 39)
    Me.lbCommecaini.Name = "lbCommecaini"
    Me.lbCommecaini.NTSDbField = ""
    Me.lbCommecaini.Size = New System.Drawing.Size(72, 13)
    Me.lbCommecaini.TabIndex = 69
    Me.lbCommecaini.Text = "Da commessa"
    Me.lbCommecaini.Tooltip = ""
    Me.lbCommecaini.UseMnemonic = False
    '
    'lbCommecafin
    '
    Me.lbCommecafin.AutoSize = True
    Me.lbCommecafin.BackColor = System.Drawing.Color.Transparent
    Me.lbCommecafin.Location = New System.Drawing.Point(166, 39)
    Me.lbCommecafin.Name = "lbCommecafin"
    Me.lbCommecafin.NTSDbField = ""
    Me.lbCommecafin.Size = New System.Drawing.Size(65, 13)
    Me.lbCommecafin.TabIndex = 64
    Me.lbCommecafin.Text = "a commessa"
    Me.lbCommecafin.Tooltip = ""
    Me.lbCommecafin.UseMnemonic = False
    '
    'ckTutto
    '
    Me.ckTutto.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTutto.Location = New System.Drawing.Point(297, 91)
    Me.ckTutto.Name = "ckTutto"
    Me.ckTutto.NTSCheckValue = "S"
    Me.ckTutto.NTSUnCheckValue = "N"
    Me.ckTutto.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTutto.Properties.Appearance.Options.UseBackColor = True
    Me.ckTutto.Properties.AutoHeight = False
    Me.ckTutto.Properties.Caption = "&Tutti gli impegni inevasi"
    Me.ckTutto.Size = New System.Drawing.Size(138, 19)
    Me.ckTutto.TabIndex = 67
    '
    'lbInfo
    '
    Me.lbInfo.BackColor = System.Drawing.Color.Transparent
    Me.lbInfo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbInfo.Location = New System.Drawing.Point(12, 403)
    Me.lbInfo.Name = "lbInfo"
    Me.lbInfo.NTSDbField = ""
    Me.lbInfo.Size = New System.Drawing.Size(651, 32)
    Me.lbInfo.TabIndex = 68
    Me.lbInfo.Text = "Saranno trattati solo gli Impegni non bloccati e/o sospesi intestati a soggetti n" & _
        "on bloccati nelle rispettive anagrafiche per i quali è indicato il codice pagame" & _
        "nto."
    Me.lbInfo.Tooltip = ""
    Me.lbInfo.UseMnemonic = False
    '
    'frPriorita
    '
    Me.frPriorita.AllowDrop = True
    Me.frPriorita.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frPriorita.Appearance.Options.UseBackColor = True
    Me.frPriorita.Controls.Add(Me.opPriorAffidab)
    Me.frPriorita.Controls.Add(Me.opPriorNumero)
    Me.frPriorita.Controls.Add(Me.opPriorCliente)
    Me.frPriorita.Controls.Add(Me.opPriorData)
    Me.frPriorita.Cursor = System.Windows.Forms.Cursors.Default
    Me.frPriorita.Location = New System.Drawing.Point(6, 12)
    Me.frPriorita.Name = "frPriorita"
    Me.frPriorita.Size = New System.Drawing.Size(132, 107)
    Me.frPriorita.TabIndex = 69
    Me.frPriorita.Text = "Priorità di assegnamento"
    '
    'opPriorAffidab
    '
    Me.opPriorAffidab.Cursor = System.Windows.Forms.Cursors.Default
    Me.opPriorAffidab.Location = New System.Drawing.Point(5, 85)
    Me.opPriorAffidab.Name = "opPriorAffidab"
    Me.opPriorAffidab.NTSCheckValue = "S"
    Me.opPriorAffidab.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opPriorAffidab.Properties.Appearance.Options.UseBackColor = True
    Me.opPriorAffidab.Properties.AutoHeight = False
    Me.opPriorAffidab.Properties.Caption = "Affidabilità cliente"
    Me.opPriorAffidab.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opPriorAffidab.Size = New System.Drawing.Size(107, 19)
    Me.opPriorAffidab.TabIndex = 59
    '
    'opPriorNumero
    '
    Me.opPriorNumero.Cursor = System.Windows.Forms.Cursors.Default
    Me.opPriorNumero.Location = New System.Drawing.Point(5, 65)
    Me.opPriorNumero.Name = "opPriorNumero"
    Me.opPriorNumero.NTSCheckValue = "S"
    Me.opPriorNumero.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opPriorNumero.Properties.Appearance.Options.UseBackColor = True
    Me.opPriorNumero.Properties.AutoHeight = False
    Me.opPriorNumero.Properties.Caption = "Data Ordine "
    Me.opPriorNumero.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opPriorNumero.Size = New System.Drawing.Size(83, 19)
    Me.opPriorNumero.TabIndex = 58
    '
    'opPriorCliente
    '
    Me.opPriorCliente.Cursor = System.Windows.Forms.Cursors.Default
    Me.opPriorCliente.Location = New System.Drawing.Point(5, 45)
    Me.opPriorCliente.Name = "opPriorCliente"
    Me.opPriorCliente.NTSCheckValue = "S"
    Me.opPriorCliente.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opPriorCliente.Properties.Appearance.Options.UseBackColor = True
    Me.opPriorCliente.Properties.AutoHeight = False
    Me.opPriorCliente.Properties.Caption = "Codice Cliente "
    Me.opPriorCliente.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opPriorCliente.Size = New System.Drawing.Size(91, 19)
    Me.opPriorCliente.TabIndex = 57
    '
    'opPriorData
    '
    Me.opPriorData.Cursor = System.Windows.Forms.Cursors.Default
    Me.opPriorData.EditValue = True
    Me.opPriorData.Location = New System.Drawing.Point(5, 25)
    Me.opPriorData.Name = "opPriorData"
    Me.opPriorData.NTSCheckValue = "S"
    Me.opPriorData.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opPriorData.Properties.Appearance.Options.UseBackColor = True
    Me.opPriorData.Properties.AutoHeight = False
    Me.opPriorData.Properties.Caption = "Data di consegna"
    Me.opPriorData.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opPriorData.Size = New System.Drawing.Size(107, 19)
    Me.opPriorData.TabIndex = 56
    '
    'frImpegni
    '
    Me.frImpegni.AllowDrop = True
    Me.frImpegni.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frImpegni.Appearance.Options.UseBackColor = True
    Me.frImpegni.Controls.Add(Me.opImp2)
    Me.frImpegni.Controls.Add(Me.opImp1)
    Me.frImpegni.Controls.Add(Me.opImp0)
    Me.frImpegni.Cursor = System.Windows.Forms.Cursors.Default
    Me.frImpegni.Location = New System.Drawing.Point(144, 12)
    Me.frImpegni.Name = "frImpegni"
    Me.frImpegni.Size = New System.Drawing.Size(144, 107)
    Me.frImpegni.TabIndex = 70
    Me.frImpegni.Text = "Tipo impegni"
    '
    'opImp2
    '
    Me.opImp2.Cursor = System.Windows.Forms.Cursors.Default
    Me.opImp2.Location = New System.Drawing.Point(5, 65)
    Me.opImp2.Name = "opImp2"
    Me.opImp2.NTSCheckValue = "S"
    Me.opImp2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opImp2.Properties.Appearance.Options.UseBackColor = True
    Me.opImp2.Properties.AutoHeight = False
    Me.opImp2.Properties.Caption = "Impegni di produzione"
    Me.opImp2.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opImp2.Size = New System.Drawing.Size(134, 19)
    Me.opImp2.TabIndex = 58
    '
    'opImp1
    '
    Me.opImp1.Cursor = System.Windows.Forms.Cursors.Default
    Me.opImp1.Location = New System.Drawing.Point(5, 45)
    Me.opImp1.Name = "opImp1"
    Me.opImp1.NTSCheckValue = "S"
    Me.opImp1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opImp1.Properties.Appearance.Options.UseBackColor = True
    Me.opImp1.Properties.AutoHeight = False
    Me.opImp1.Properties.Caption = "Impegni trasferimento"
    Me.opImp1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opImp1.Size = New System.Drawing.Size(134, 19)
    Me.opImp1.TabIndex = 57
    '
    'opImp0
    '
    Me.opImp0.Cursor = System.Windows.Forms.Cursors.Default
    Me.opImp0.EditValue = True
    Me.opImp0.Location = New System.Drawing.Point(5, 25)
    Me.opImp0.Name = "opImp0"
    Me.opImp0.NTSCheckValue = "S"
    Me.opImp0.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opImp0.Properties.Appearance.Options.UseBackColor = True
    Me.opImp0.Properties.AutoHeight = False
    Me.opImp0.Properties.Caption = "Impegni clienti"
    Me.opImp0.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opImp0.Size = New System.Drawing.Size(94, 19)
    Me.opImp0.TabIndex = 56
    '
    'lbCodmaga
    '
    Me.lbCodmaga.AutoSize = True
    Me.lbCodmaga.BackColor = System.Drawing.Color.Transparent
    Me.lbCodmaga.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbCodmaga.Location = New System.Drawing.Point(294, 15)
    Me.lbCodmaga.Name = "lbCodmaga"
    Me.lbCodmaga.NTSDbField = ""
    Me.lbCodmaga.Size = New System.Drawing.Size(92, 13)
    Me.lbCodmaga.TabIndex = 72
    Me.lbCodmaga.Text = "Magazzino riga"
    Me.lbCodmaga.Tooltip = ""
    Me.lbCodmaga.UseMnemonic = False
    '
    'edCodmaga
    '
    Me.edCodmaga.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodmaga.Location = New System.Drawing.Point(412, 12)
    Me.edCodmaga.Name = "edCodmaga"
    Me.edCodmaga.NTSDbField = ""
    Me.edCodmaga.NTSFormat = "0"
    Me.edCodmaga.NTSForzaVisZoom = False
    Me.edCodmaga.NTSOldValue = ""
    Me.edCodmaga.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodmaga.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodmaga.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodmaga.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodmaga.Properties.AutoHeight = False
    Me.edCodmaga.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodmaga.Properties.MaxLength = 65536
    Me.edCodmaga.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodmaga.Size = New System.Drawing.Size(83, 20)
    Me.edCodmaga.TabIndex = 71
    '
    'lbMaxnumord
    '
    Me.lbMaxnumord.AutoSize = True
    Me.lbMaxnumord.BackColor = System.Drawing.Color.Transparent
    Me.lbMaxnumord.Location = New System.Drawing.Point(294, 68)
    Me.lbMaxnumord.Name = "lbMaxnumord"
    Me.lbMaxnumord.NTSDbField = ""
    Me.lbMaxnumord.Size = New System.Drawing.Size(117, 13)
    Me.lbMaxnumord.TabIndex = 74
    Me.lbMaxnumord.Text = "Consid. max num.ord.:"
    Me.lbMaxnumord.Tooltip = ""
    Me.lbMaxnumord.UseMnemonic = False
    '
    'edMaxnumord
    '
    Me.edMaxnumord.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMaxnumord.Location = New System.Drawing.Point(412, 65)
    Me.edMaxnumord.Name = "edMaxnumord"
    Me.edMaxnumord.NTSDbField = ""
    Me.edMaxnumord.NTSFormat = "0"
    Me.edMaxnumord.NTSForzaVisZoom = False
    Me.edMaxnumord.NTSOldValue = ""
    Me.edMaxnumord.Properties.Appearance.Options.UseTextOptions = True
    Me.edMaxnumord.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMaxnumord.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMaxnumord.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMaxnumord.Properties.AutoHeight = False
    Me.edMaxnumord.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMaxnumord.Properties.MaxLength = 65536
    Me.edMaxnumord.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMaxnumord.Size = New System.Drawing.Size(83, 20)
    Me.edMaxnumord.TabIndex = 73
    '
    'lbADataCons
    '
    Me.lbADataCons.AutoSize = True
    Me.lbADataCons.BackColor = System.Drawing.Color.Transparent
    Me.lbADataCons.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbADataCons.Location = New System.Drawing.Point(294, 41)
    Me.lbADataCons.Name = "lbADataCons"
    Me.lbADataCons.NTSDbField = ""
    Me.lbADataCons.Size = New System.Drawing.Size(101, 13)
    Me.lbADataCons.TabIndex = 75
    Me.lbADataCons.Text = "Fino a data cons."
    Me.lbADataCons.Tooltip = ""
    Me.lbADataCons.UseMnemonic = False
    '
    'edADataCons
    '
    Me.edADataCons.Cursor = System.Windows.Forms.Cursors.Default
    Me.edADataCons.Location = New System.Drawing.Point(412, 39)
    Me.edADataCons.Name = "edADataCons"
    Me.edADataCons.NTSDbField = ""
    Me.edADataCons.NTSForzaVisZoom = False
    Me.edADataCons.NTSOldValue = ""
    Me.edADataCons.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edADataCons.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edADataCons.Properties.AutoHeight = False
    Me.edADataCons.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edADataCons.Properties.MaxLength = 65536
    Me.edADataCons.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edADataCons.Size = New System.Drawing.Size(83, 20)
    Me.edADataCons.TabIndex = 76
    '
    'edSerie
    '
    Me.edSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSerie.Location = New System.Drawing.Point(94, 127)
    Me.edSerie.Name = "edSerie"
    Me.edSerie.NTSDbField = ""
    Me.edSerie.NTSForzaVisZoom = False
    Me.edSerie.NTSOldValue = ""
    Me.edSerie.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSerie.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSerie.Properties.AutoHeight = False
    Me.edSerie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSerie.Properties.MaxLength = 65536
    Me.edSerie.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSerie.Size = New System.Drawing.Size(45, 20)
    Me.edSerie.TabIndex = 77
    '
    'lbSerie
    '
    Me.lbSerie.AutoSize = True
    Me.lbSerie.BackColor = System.Drawing.Color.Transparent
    Me.lbSerie.Location = New System.Drawing.Point(14, 130)
    Me.lbSerie.Name = "lbSerie"
    Me.lbSerie.NTSDbField = ""
    Me.lbSerie.Size = New System.Drawing.Size(74, 13)
    Me.lbSerie.TabIndex = 78
    Me.lbSerie.Text = "Serie impegni:"
    Me.lbSerie.Tooltip = ""
    Me.lbSerie.UseMnemonic = False
    '
    'lbSerieLabel
    '
    Me.lbSerieLabel.AutoSize = True
    Me.lbSerieLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbSerieLabel.Location = New System.Drawing.Point(144, 130)
    Me.lbSerieLabel.Name = "lbSerieLabel"
    Me.lbSerieLabel.NTSDbField = ""
    Me.lbSerieLabel.Size = New System.Drawing.Size(55, 13)
    Me.lbSerieLabel.TabIndex = 79
    Me.lbSerieLabel.Text = "(* = tutti)"
    Me.lbSerieLabel.Tooltip = ""
    Me.lbSerieLabel.UseMnemonic = False
    '
    'FRMORSEIC
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(674, 433)
    Me.Controls.Add(Me.lbSerieLabel)
    Me.Controls.Add(Me.lbSerie)
    Me.Controls.Add(Me.edSerie)
    Me.Controls.Add(Me.edADataCons)
    Me.Controls.Add(Me.lbADataCons)
    Me.Controls.Add(Me.lbMaxnumord)
    Me.Controls.Add(Me.edMaxnumord)
    Me.Controls.Add(Me.lbCodmaga)
    Me.Controls.Add(Me.edCodmaga)
    Me.Controls.Add(Me.frImpegni)
    Me.Controls.Add(Me.frPriorita)
    Me.Controls.Add(Me.lbInfo)
    Me.Controls.Add(Me.NtsGroupBox1)
    Me.Controls.Add(Me.ckTutto)
    Me.Controls.Add(Me.frClienti)
    Me.Controls.Add(Me.frData)
    Me.Controls.Add(Me.frAgente)
    Me.Controls.Add(Me.frZona)
    Me.Controls.Add(Me.frTipoBolla)
    Me.Controls.Add(Me.ckConf)
    Me.Controls.Add(Me.ckConssoloasa)
    Me.Controls.Add(Me.pnRight)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMORSEIC"
    Me.Text = "SELEZIONA IMPEGNI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnRight.ResumeLayout(False)
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
    CType(Me.opAlcuneDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opTutteDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckConssoloasa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckConf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    CType(Me.NtsGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsGroupBox1.ResumeLayout(False)
    Me.NtsGroupBox1.PerformLayout()
    CType(Me.edCommecafin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCommecaini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTutto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frPriorita, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frPriorita.ResumeLayout(False)
    Me.frPriorita.PerformLayout()
    CType(Me.opPriorAffidab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opPriorNumero.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opPriorCliente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opPriorData.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frImpegni, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frImpegni.ResumeLayout(False)
    Me.frImpegni.PerformLayout()
    CType(Me.opImp2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opImp1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opImp0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodmaga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edMaxnumord.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edADataCons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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

  Public Overridable Sub InitEntity(ByRef cleGnnp As CLEORGNNP)
    oCleGnnp = cleGnnp
    AddHandler oCleGnnp.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      edSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128720205494034653, "Serie impegni:"), CLN__STD.SerieMaxLen)
      edADataCons.NTSSetParam(oMenu, oApp.Tr(Me, 128720205494190907, "Fino a data consegna:"), False)
      edMaxnumord.NTSSetParam(oMenu, oApp.Tr(Me, 128720205494659669, "Consid. max num.ord.:"), "0", 4, 1, 9999)
      edCodmaga.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128720205494972177, "Magazzino riga:"), tabmaga)
      opImp2.NTSSetParam(oMenu, oApp.Tr(Me, 128720205495128431, "Impegni di produzione"), "S")
      opImp1.NTSSetParam(oMenu, oApp.Tr(Me, 128720205495284685, "Impegni trasferimento"), "S")
      opImp0.NTSSetParam(oMenu, oApp.Tr(Me, 128733971439593595, "Impegni clienti"), "S")
      opPriorAffidab.NTSSetParam(oMenu, oApp.Tr(Me, 128720205495440939, "Affidabilità cliente"), "S")
      opPriorNumero.NTSSetParam(oMenu, oApp.Tr(Me, 128720205495597193, "Data Ordine "), "S")
      opPriorCliente.NTSSetParam(oMenu, oApp.Tr(Me, 128720205495753447, "Codice Cliente "), "S")
      opPriorData.NTSSetParam(oMenu, oApp.Tr(Me, 128720205495909701, "Data di consegna"), "S")
      edCommecafin.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128720205496222209, "a commessa"), tabcommess)
      edCommecaini.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128720205496378463, "Da commessa"), tabcommess)
      ckTutto.NTSSetParam(oMenu, oApp.Tr(Me, 128720205496534717, "Solo Note di prelievo vistate"), "S", "N")
      edAlcliente.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128720205496690971, "al cliente"), tabanagrac)
      edDalcliente.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128720205496847225, "dal cliente"), tabanagrac)
      opAlcuniClienti.NTSSetParam(oMenu, oApp.Tr(Me, 128720205497159733, "Dal cliente"), "S")
      opTuttiClienti.NTSSetParam(oMenu, oApp.Tr(Me, 128733971566787607, "Tutti"), "S")
      edAlladata.NTSSetParam(oMenu, oApp.Tr(Me, 128720205497315987, "alla data"), True)
      edDalladata.NTSSetParam(oMenu, oApp.Tr(Me, 128720205497472241, "dalla data"), True)
      opAlcuneDate.NTSSetParam(oMenu, oApp.Tr(Me, 128720205497784749, "Dalla data"), "S")
      opTutteDate.NTSSetParam(oMenu, oApp.Tr(Me, 128720205497941003, "Qualsiasi"), "S")
      edCodcage.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128720205498097257, "Agente"), tabcage)
      opAlcuniAgenti.NTSSetParam(oMenu, oApp.Tr(Me, 128720205498253511, "Agente"), "S")
      opTuttiAgenti.NTSSetParam(oMenu, oApp.Tr(Me, 128720205498409765, "Tutti"), "S")
      edCodzona.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128720205498566019, "Zona"), tabzone)
      opAlcuneZone.NTSSetParam(oMenu, oApp.Tr(Me, 128720205498722273, "Zona"), "S")
      opTutteZone.NTSSetParam(oMenu, oApp.Tr(Me, 128720205498878527, "Tutti"), "S")
      edTipobf.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128720205499034781, "tipo bolla/fattura"), tabtpbf)
      opAlcuneBolle.NTSSetParam(oMenu, oApp.Tr(Me, 128720205499191035, "Bolla/fattura"), "S")
      opTutteBolle.NTSSetParam(oMenu, oApp.Tr(Me, 128733971673824337, "Tutte"), "S")
      ckConf.NTSSetParam(oMenu, oApp.Tr(Me, 128720205499347289, "Considera 'Solo a saldo' su ord."), "S", "N")
      ckConssoloasa.NTSSetParam(oMenu, oApp.Tr(Me, 128720205499503543, "Solo Note di prelievo vistate"), "S", "N")

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

  Public Overridable Sub FRMORSEIC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      bOk = False
      oCleGnnp.strSeicQuery = ""
      oCleGnnp.nSeicPriorita = 0
      oCleGnnp.nSeicMagaz = 0

      oCleGnnp.strTipoConferma = oMenu.GetSettingBus("Bsorgnnp", "OPZIONI", ".", "TipoConferma", "R", " ", "R")

      'Calcola l'esercizio di competenza
      oCleGnnp.LeggiDatiDitta(DittaCorrente)
      edADataCons.Text = Now.ToShortDateString
      edCodmaga.Text = "1"
      edMaxnumord.Text = "9999"
      edCommecaini.Text = "0"
      edCommecafin.Text = "999999999"
      edSerie.Text = "*"
      oCleGnnp.bDaSelezioneArticoli = False
      oCleGnnp.lIITTOltomo = oMenu.GetTblInstId("TTOLTOMO", False)

      If CBool(oMenu.GetSettingBus("BSORGNNP", "OPZIONI", ".", "ElaboraClientiBloccati", "0", ".", "0")) Then
        lbInfo.Text = oApp.Tr(Me, 129490772835223667, "Saranno trattati solo gli Impegni non bloccati e/o sospesi intestati a soggetti per i quali è indicato il codice pagamento nelle rispettive anagrafiche .")
      End If

      '--------------------------------------------
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

      If Not oCleGnnp.CheckSelection(edDalcliente.Text, edAlcliente.Text, edDalladata.Text, edAlladata.Text, _
                                     edCommecaini.Text, edCommecafin.Text) Then Return

      oCleGnnp.ComponiQuerySeic(edADataCons.Text, _
                                edCodmaga.Text, edSerie.Text, ckTutto.Checked, _
                                opTuttiClienti.Checked, edDalcliente.Text, edAlcliente.Text, opTutteDate.Checked, _
                                edDalladata.Text, edAlladata.Text, opTutteBolle.Checked, edTipobf.Text, _
                                opTuttiAgenti.Checked, edCodcage.Text, opTutteZone.Checked, edCodzona.Text, _
                                ckConf.Checked, edCommecaini.Text, edCommecafin.Text, opImp0.Checked, _
                                opImp1.Checked, opImp2.Checked, ckConssoloasa.Checked, edMaxnumord.Text, _
                                opPriorData.Checked, opPriorCliente.Checked, opPriorNumero.Checked, opPriorAffidab.Checked)

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

  Public Overridable Sub edCodcage_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodcage.ValidatedAndChanged
    Dim strTmp As String = ""
    Try
      If oCleGnnp Is Nothing Then Return
      If Not oCleGnnp.edCodcage_Validated(NTSCInt(edCodcage.Text), strTmp) Then
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
      If oCleGnnp Is Nothing Then Return
      If Not oCleGnnp.edTipoBf_Validated(NTSCInt(edTipobf.Text), strTmp) Then
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
      If oCleGnnp Is Nothing Then Return
      If Not oCleGnnp.edCodzona_Validated(NTSCInt(edCodzona.Text), strTmp) Then
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
  Public Overridable Sub edSerie_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSerie.ValidatedAndChanged
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edSerie.Text, False)
      If strTmp <> edSerie.Text Then edSerie.Text = strTmp

      If edSerie.Text = "" Then
        edSerie.Text = "*"
      Else
        edSerie.Text = UCase(edSerie.Text)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edCommecaini_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCommecaini.ValidatedAndChanged
    Try
      If NTSCInt(edCommecaini.Text) <> 0 Then edCommecafin.Text = edCommecaini.Text
    Catch ex As Exception
      '-------------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edDalcliente_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDalcliente.ValidatedAndChanged
    Try
      If NTSCInt(edDalcliente.Text) <> 0 Then edAlcliente.Text = edDalcliente.Text
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub ckTutto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckTutto.CheckedChanged
    Try
      If ckTutto.Checked = True Then
        frData.Enabled = False
        frTipoBolla.Enabled = False
        frClienti.Enabled = False
        frAgente.Enabled = False
        frZona.Enabled = False
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
        lbADataCons.Enabled = False
        lbAlcliente.Enabled = False
        lbAlladata.Enabled = False
        ckConf.Checked = False
        ckConf.Enabled = False
        lbCommecaini.Enabled = False
        lbCommecafin.Enabled = False
        edCommecaini.Text = "0"
        edCommecafin.Text = "999999999"
        edCommecaini.Enabled = False
        edCommecafin.Enabled = False
      Else
        frData.Enabled = True
        frTipoBolla.Enabled = True
        frClienti.Enabled = True
        frAgente.Enabled = True
        frZona.Enabled = True
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
        lbADataCons.Enabled = True
        lbAlcliente.Enabled = True
        lbAlladata.Enabled = True
        ckConf.Checked = False
        ckConf.Enabled = True
        lbCommecaini.Enabled = True
        lbCommecafin.Enabled = True
        edCommecaini.Enabled = True
        edCommecafin.Enabled = True
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
        edAlcliente.Text = "999999999"
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
        edAlcliente.Text = "999999999"
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

  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Dim oParam As New CLE__CLDP
    Dim fmSenu As FRMORSENU = Nothing
    Try
      oMenu.ResetTblInstId("TTOLTOMO", False, oCleGnnp.lIITTOltomo)
      oCleGnnp.nSenuAnno = Year(Now)
      oCleGnnp.strSenuSerie = " "
      oCleGnnp.lSenuInstid = oCleGnnp.lIITTOltomo

      fmSenu = CType(NTSNewFormModal("FRMORSENU"), FRMORSENU)
      fmSenu.Init(oMenu, oParam, DittaCorrente)
      fmSenu.InitEntity(oCleGnnp)
      fmSenu.ShowDialog()

      If oCleGnnp.nSenuAnno = 0 Then Exit Sub

      cmdOk_Click(Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If Not fmSenu Is Nothing Then fmSenu.Dispose()
      fmSenu = Nothing
    End Try
  End Sub
  Public Overridable Sub cmdSelCodart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelCodart.Click
    Dim oPar As CLE__PATB = Nothing
    Try
      oCleGnnp.bDaSelezioneArticoli = False
      '-----------------------------------------------------------------------------------------
      '--- Chiama la modale sui filtri articolo
      '-----------------------------------------------------------------------------------------
      oPar = New CLE__PATB
      oPar.bVisGriglia = False
      oPar.strTipoArticolo = "N"
      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oPar)
      oCleGnnp.strWhereFiar = oPar.strOut.Trim
      '-----------------------------------------------------------------------------------------
      '--- Se annullato esce
      '-----------------------------------------------------------------------------------------
      If Trim(oCleGnnp.strWhereFiar) = "" Then Exit Sub
      '-----------------------------------------------------------------------------------------
      '--- Setta la variabile che indica la scelta del pulsante
      '-----------------------------------------------------------------------------------------
      oCleGnnp.bDaSelezioneArticoli = True

      cmdOk_Click(Me, Nothing)

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
