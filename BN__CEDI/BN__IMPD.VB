Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__IMPD

#Region "Variabili"
  Public oCleCedi As CLE__CEDI
  Public oCallParams As CLE__CLDP
  Private components As System.ComponentModel.IContainer

  Public strMsg As String

  Public dttTabelle As DataTable

  Public WithEvents pnImpd As NTSInformatica.NTSPanel
  Public WithEvents frDittaOrigine As NTSInformatica.NTSGroupBox
  Public WithEvents frImporta As NTSInformatica.NTSGroupBox
  Public WithEvents opImporta1 As NTSInformatica.NTSRadioButton
  Public WithEvents opImporta0 As NTSInformatica.NTSRadioButton
  Public WithEvents frDittaDestinazione As NTSInformatica.NTSGroupBox
  Public WithEvents lbDesdittDest As NTSInformatica.NTSLabel
  Public WithEvents lbCoddittDest As NTSInformatica.NTSLabel
  Public WithEvents liTabelle As NTSInformatica.NTSListBox
  Public WithEvents frOperazione As NTSInformatica.NTSGroupBox
  Public WithEvents opAltrodb As NTSInformatica.NTSRadioButton
  Public WithEvents opStessodb As NTSInformatica.NTSRadioButton
  Public WithEvents cmdDesAll As NTSInformatica.NTSButton
  Public WithEvents cmdSelAll As NTSInformatica.NTSButton
  Public WithEvents ckSvuotaPN As NTSInformatica.NTSCheckBox
  Public WithEvents ckDisabPS As NTSInformatica.NTSCheckBox
  Public WithEvents ckAbilPS As NTSInformatica.NTSCheckBox
  Public WithEvents ckScollegaAG As NTSInformatica.NTSCheckBox
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents lbDesdittOr As NTSInformatica.NTSLabel
  Public WithEvents edCoddittOr As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbCoddittOr As NTSInformatica.NTSLabel
  Public WithEvents opSovrascrivi As NTSInformatica.NTSRadioButton
  Public WithEvents opSostituisci As NTSInformatica.NTSRadioButton
  Public WithEvents opAccoda As NTSInformatica.NTSRadioButton
  Public WithEvents frSqlServer As NTSInformatica.NTSGroupBox
  Public WithEvents lbDatabase As NTSInformatica.NTSLabel
  Public WithEvents lbPwd As NTSInformatica.NTSLabel
  Public WithEvents lbUser As NTSInformatica.NTSLabel
  Public WithEvents lbServer As NTSInformatica.NTSLabel
  Public WithEvents edDatabase As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPwd As NTSInformatica.NTSTextBoxStr
  Public WithEvents edUser As NTSInformatica.NTSTextBoxStr
  Public WithEvents edServer As NTSInformatica.NTSTextBoxStr
#End Region

  Private Sub InitializeComponent()
    Me.pnImpd = New NTSInformatica.NTSPanel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.frDittaOrigine = New NTSInformatica.NTSGroupBox
    Me.frSqlServer = New NTSInformatica.NTSGroupBox
    Me.lbDatabase = New NTSInformatica.NTSLabel
    Me.lbPwd = New NTSInformatica.NTSLabel
    Me.lbUser = New NTSInformatica.NTSLabel
    Me.lbServer = New NTSInformatica.NTSLabel
    Me.edDatabase = New NTSInformatica.NTSTextBoxStr
    Me.edPwd = New NTSInformatica.NTSTextBoxStr
    Me.edUser = New NTSInformatica.NTSTextBoxStr
    Me.edServer = New NTSInformatica.NTSTextBoxStr
    Me.lbDesdittOr = New NTSInformatica.NTSLabel
    Me.edCoddittOr = New NTSInformatica.NTSTextBoxStr
    Me.lbCoddittOr = New NTSInformatica.NTSLabel
    Me.ckSvuotaPN = New NTSInformatica.NTSCheckBox
    Me.ckDisabPS = New NTSInformatica.NTSCheckBox
    Me.ckAbilPS = New NTSInformatica.NTSCheckBox
    Me.ckScollegaAG = New NTSInformatica.NTSCheckBox
    Me.opAltrodb = New NTSInformatica.NTSRadioButton
    Me.opStessodb = New NTSInformatica.NTSRadioButton
    Me.frImporta = New NTSInformatica.NTSGroupBox
    Me.cmdDesAll = New NTSInformatica.NTSButton
    Me.cmdSelAll = New NTSInformatica.NTSButton
    Me.liTabelle = New NTSInformatica.NTSListBox
    Me.frOperazione = New NTSInformatica.NTSGroupBox
    Me.opSovrascrivi = New NTSInformatica.NTSRadioButton
    Me.opSostituisci = New NTSInformatica.NTSRadioButton
    Me.opAccoda = New NTSInformatica.NTSRadioButton
    Me.opImporta1 = New NTSInformatica.NTSRadioButton
    Me.opImporta0 = New NTSInformatica.NTSRadioButton
    Me.frDittaDestinazione = New NTSInformatica.NTSGroupBox
    Me.lbDesdittDest = New NTSInformatica.NTSLabel
    Me.lbCoddittDest = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnImpd, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnImpd.SuspendLayout()
    CType(Me.frDittaOrigine, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frDittaOrigine.SuspendLayout()
    CType(Me.frSqlServer, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frSqlServer.SuspendLayout()
    CType(Me.edDatabase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPwd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edUser.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edServer.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCoddittOr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSvuotaPN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDisabPS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAbilPS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckScollegaAG.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opAltrodb.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opStessodb.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frImporta, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frImporta.SuspendLayout()
    CType(Me.liTabelle, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frOperazione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frOperazione.SuspendLayout()
    CType(Me.opSovrascrivi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opSostituisci.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opAccoda.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opImporta1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opImporta0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frDittaDestinazione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frDittaDestinazione.SuspendLayout()
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
    'pnImpd
    '
    Me.pnImpd.AllowDrop = True
    Me.pnImpd.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnImpd.Appearance.Options.UseBackColor = True
    Me.pnImpd.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnImpd.Controls.Add(Me.cmdAnnulla)
    Me.pnImpd.Controls.Add(Me.cmdOk)
    Me.pnImpd.Controls.Add(Me.frDittaOrigine)
    Me.pnImpd.Controls.Add(Me.frImporta)
    Me.pnImpd.Controls.Add(Me.frDittaDestinazione)
    Me.pnImpd.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnImpd.Location = New System.Drawing.Point(1, 1)
    Me.pnImpd.Name = "pnImpd"
    Me.pnImpd.NTSActiveTrasparency = True
    Me.pnImpd.Size = New System.Drawing.Size(721, 440)
    Me.pnImpd.TabIndex = 0
    Me.pnImpd.Text = "NtsPanel1"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(574, 38)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(140, 25)
    Me.cmdAnnulla.TabIndex = 6
    Me.cmdAnnulla.Text = "&Chiudi"
    '
    'cmdOk
    '
    Me.cmdOk.ImagePath = ""
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(574, 11)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.NTSContextMenu = Nothing
    Me.cmdOk.Size = New System.Drawing.Size(140, 25)
    Me.cmdOk.TabIndex = 4
    Me.cmdOk.Text = "&Importa"
    '
    'frDittaOrigine
    '
    Me.frDittaOrigine.AllowDrop = True
    Me.frDittaOrigine.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frDittaOrigine.Appearance.Options.UseBackColor = True
    Me.frDittaOrigine.Controls.Add(Me.frSqlServer)
    Me.frDittaOrigine.Controls.Add(Me.lbDesdittOr)
    Me.frDittaOrigine.Controls.Add(Me.edCoddittOr)
    Me.frDittaOrigine.Controls.Add(Me.lbCoddittOr)
    Me.frDittaOrigine.Controls.Add(Me.ckSvuotaPN)
    Me.frDittaOrigine.Controls.Add(Me.ckDisabPS)
    Me.frDittaOrigine.Controls.Add(Me.ckAbilPS)
    Me.frDittaOrigine.Controls.Add(Me.ckScollegaAG)
    Me.frDittaOrigine.Controls.Add(Me.opAltrodb)
    Me.frDittaOrigine.Controls.Add(Me.opStessodb)
    Me.frDittaOrigine.Cursor = System.Windows.Forms.Cursors.Default
    Me.frDittaOrigine.Location = New System.Drawing.Point(279, 80)
    Me.frDittaOrigine.Name = "frDittaOrigine"
    Me.frDittaOrigine.Size = New System.Drawing.Size(289, 350)
    Me.frDittaOrigine.TabIndex = 2
    Me.frDittaOrigine.Text = "Ditta di origine: "
    '
    'frSqlServer
    '
    Me.frSqlServer.AllowDrop = True
    Me.frSqlServer.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frSqlServer.Appearance.Options.UseBackColor = True
    Me.frSqlServer.Controls.Add(Me.lbDatabase)
    Me.frSqlServer.Controls.Add(Me.lbPwd)
    Me.frSqlServer.Controls.Add(Me.lbUser)
    Me.frSqlServer.Controls.Add(Me.lbServer)
    Me.frSqlServer.Controls.Add(Me.edDatabase)
    Me.frSqlServer.Controls.Add(Me.edPwd)
    Me.frSqlServer.Controls.Add(Me.edUser)
    Me.frSqlServer.Controls.Add(Me.edServer)
    Me.frSqlServer.Cursor = System.Windows.Forms.Cursors.Default
    Me.frSqlServer.Location = New System.Drawing.Point(48, 75)
    Me.frSqlServer.Name = "frSqlServer"
    Me.frSqlServer.Size = New System.Drawing.Size(221, 131)
    Me.frSqlServer.TabIndex = 16
    Me.frSqlServer.Text = "Sql Server"
    '
    'lbDatabase
    '
    Me.lbDatabase.AutoSize = True
    Me.lbDatabase.BackColor = System.Drawing.Color.Transparent
    Me.lbDatabase.Location = New System.Drawing.Point(15, 104)
    Me.lbDatabase.Name = "lbDatabase"
    Me.lbDatabase.NTSDbField = ""
    Me.lbDatabase.Size = New System.Drawing.Size(82, 13)
    Me.lbDatabase.TabIndex = 23
    Me.lbDatabase.Text = "Nome database"
    Me.lbDatabase.Tooltip = ""
    Me.lbDatabase.UseMnemonic = False
    '
    'lbPwd
    '
    Me.lbPwd.AutoSize = True
    Me.lbPwd.BackColor = System.Drawing.Color.Transparent
    Me.lbPwd.Location = New System.Drawing.Point(15, 78)
    Me.lbPwd.Name = "lbPwd"
    Me.lbPwd.NTSDbField = ""
    Me.lbPwd.Size = New System.Drawing.Size(27, 13)
    Me.lbPwd.TabIndex = 22
    Me.lbPwd.Text = "Pwd"
    Me.lbPwd.Tooltip = ""
    Me.lbPwd.UseMnemonic = False
    '
    'lbUser
    '
    Me.lbUser.AutoSize = True
    Me.lbUser.BackColor = System.Drawing.Color.Transparent
    Me.lbUser.Location = New System.Drawing.Point(15, 52)
    Me.lbUser.Name = "lbUser"
    Me.lbUser.NTSDbField = ""
    Me.lbUser.Size = New System.Drawing.Size(29, 13)
    Me.lbUser.TabIndex = 21
    Me.lbUser.Text = "User"
    Me.lbUser.Tooltip = ""
    Me.lbUser.UseMnemonic = False
    '
    'lbServer
    '
    Me.lbServer.AutoSize = True
    Me.lbServer.BackColor = System.Drawing.Color.Transparent
    Me.lbServer.Location = New System.Drawing.Point(15, 26)
    Me.lbServer.Name = "lbServer"
    Me.lbServer.NTSDbField = ""
    Me.lbServer.Size = New System.Drawing.Size(39, 13)
    Me.lbServer.TabIndex = 20
    Me.lbServer.Text = "Server"
    Me.lbServer.Tooltip = ""
    Me.lbServer.UseMnemonic = False
    '
    'edDatabase
    '
    Me.edDatabase.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatabase.Location = New System.Drawing.Point(103, 101)
    Me.edDatabase.Name = "edDatabase"
    Me.edDatabase.NTSDbField = ""
    Me.edDatabase.NTSForzaVisZoom = False
    Me.edDatabase.NTSOldValue = ""
    Me.edDatabase.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatabase.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatabase.Properties.AutoHeight = False
    Me.edDatabase.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatabase.Properties.MaxLength = 65536
    Me.edDatabase.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatabase.Size = New System.Drawing.Size(112, 20)
    Me.edDatabase.TabIndex = 19
    '
    'edPwd
    '
    Me.edPwd.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPwd.Location = New System.Drawing.Point(103, 75)
    Me.edPwd.Name = "edPwd"
    Me.edPwd.NTSDbField = ""
    Me.edPwd.NTSForzaVisZoom = False
    Me.edPwd.NTSOldValue = ""
    Me.edPwd.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPwd.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPwd.Properties.AutoHeight = False
    Me.edPwd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPwd.Properties.MaxLength = 65536
    Me.edPwd.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPwd.Size = New System.Drawing.Size(112, 20)
    Me.edPwd.TabIndex = 18
    '
    'edUser
    '
    Me.edUser.Cursor = System.Windows.Forms.Cursors.Default
    Me.edUser.Location = New System.Drawing.Point(103, 49)
    Me.edUser.Name = "edUser"
    Me.edUser.NTSDbField = ""
    Me.edUser.NTSForzaVisZoom = False
    Me.edUser.NTSOldValue = ""
    Me.edUser.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUser.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUser.Properties.AutoHeight = False
    Me.edUser.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUser.Properties.MaxLength = 65536
    Me.edUser.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUser.Size = New System.Drawing.Size(112, 20)
    Me.edUser.TabIndex = 17
    '
    'edServer
    '
    Me.edServer.Cursor = System.Windows.Forms.Cursors.Default
    Me.edServer.Location = New System.Drawing.Point(104, 23)
    Me.edServer.Name = "edServer"
    Me.edServer.NTSDbField = ""
    Me.edServer.NTSForzaVisZoom = False
    Me.edServer.NTSOldValue = ""
    Me.edServer.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edServer.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edServer.Properties.AutoHeight = False
    Me.edServer.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edServer.Properties.MaxLength = 65536
    Me.edServer.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edServer.Size = New System.Drawing.Size(113, 20)
    Me.edServer.TabIndex = 16
    '
    'lbDesdittOr
    '
    Me.lbDesdittOr.BackColor = System.Drawing.Color.Transparent
    Me.lbDesdittOr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesdittOr.Location = New System.Drawing.Point(17, 315)
    Me.lbDesdittOr.Name = "lbDesdittOr"
    Me.lbDesdittOr.NTSDbField = ""
    Me.lbDesdittOr.Size = New System.Drawing.Size(243, 23)
    Me.lbDesdittOr.TabIndex = 11
    Me.lbDesdittOr.Tooltip = ""
    Me.lbDesdittOr.UseMnemonic = False
    '
    'edCoddittOr
    '
    Me.edCoddittOr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCoddittOr.Location = New System.Drawing.Point(146, 292)
    Me.edCoddittOr.Name = "edCoddittOr"
    Me.edCoddittOr.NTSDbField = ""
    Me.edCoddittOr.NTSForzaVisZoom = False
    Me.edCoddittOr.NTSOldValue = ""
    Me.edCoddittOr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCoddittOr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCoddittOr.Properties.AutoHeight = False
    Me.edCoddittOr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCoddittOr.Properties.MaxLength = 65536
    Me.edCoddittOr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCoddittOr.Size = New System.Drawing.Size(114, 20)
    Me.edCoddittOr.TabIndex = 10
    '
    'lbCoddittOr
    '
    Me.lbCoddittOr.AutoSize = True
    Me.lbCoddittOr.BackColor = System.Drawing.Color.Transparent
    Me.lbCoddittOr.Location = New System.Drawing.Point(14, 295)
    Me.lbCoddittOr.Name = "lbCoddittOr"
    Me.lbCoddittOr.NTSDbField = ""
    Me.lbCoddittOr.Size = New System.Drawing.Size(95, 13)
    Me.lbCoddittOr.TabIndex = 9
    Me.lbCoddittOr.Text = "Cod. Ditta origine:"
    Me.lbCoddittOr.Tooltip = ""
    Me.lbCoddittOr.UseMnemonic = False
    '
    'ckSvuotaPN
    '
    Me.ckSvuotaPN.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSvuotaPN.Location = New System.Drawing.Point(48, 269)
    Me.ckSvuotaPN.Name = "ckSvuotaPN"
    Me.ckSvuotaPN.NTSCheckValue = "S"
    Me.ckSvuotaPN.NTSUnCheckValue = "N"
    Me.ckSvuotaPN.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSvuotaPN.Properties.Appearance.Options.UseBackColor = True
    Me.ckSvuotaPN.Properties.AutoHeight = False
    Me.ckSvuotaPN.Properties.Caption = "Svuota archivi p.nota e altri arch. contabili"
    Me.ckSvuotaPN.Size = New System.Drawing.Size(236, 19)
    Me.ckSvuotaPN.TabIndex = 8
    '
    'ckDisabPS
    '
    Me.ckDisabPS.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDisabPS.Location = New System.Drawing.Point(48, 250)
    Me.ckDisabPS.Name = "ckDisabPS"
    Me.ckDisabPS.NTSCheckValue = "S"
    Me.ckDisabPS.NTSUnCheckValue = "N"
    Me.ckDisabPS.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDisabPS.Properties.Appearance.Options.UseBackColor = True
    Me.ckDisabPS.Properties.AutoHeight = False
    Me.ckDisabPS.Properties.Caption = "Disabilita partite e scadenze su cli/forn."
    Me.ckDisabPS.Size = New System.Drawing.Size(236, 19)
    Me.ckDisabPS.TabIndex = 7
    '
    'ckAbilPS
    '
    Me.ckAbilPS.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAbilPS.Location = New System.Drawing.Point(48, 231)
    Me.ckAbilPS.Name = "ckAbilPS"
    Me.ckAbilPS.NTSCheckValue = "S"
    Me.ckAbilPS.NTSUnCheckValue = "N"
    Me.ckAbilPS.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAbilPS.Properties.Appearance.Options.UseBackColor = True
    Me.ckAbilPS.Properties.AutoHeight = False
    Me.ckAbilPS.Properties.Caption = "Abilita partite e scadenze su cli/forn."
    Me.ckAbilPS.Size = New System.Drawing.Size(236, 19)
    Me.ckAbilPS.TabIndex = 6
    '
    'ckScollegaAG
    '
    Me.ckScollegaAG.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.ckScollegaAG.Location = New System.Drawing.Point(48, 212)
    Me.ckScollegaAG.Name = "ckScollegaAG"
    Me.ckScollegaAG.NTSCheckValue = "S"
    Me.ckScollegaAG.NTSUnCheckValue = "N"
    Me.ckScollegaAG.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckScollegaAG.Properties.Appearance.Options.UseBackColor = True
    Me.ckScollegaAG.Properties.AutoHeight = False
    Me.ckScollegaAG.Properties.Caption = "Scollega da anagrafica  generale"
    Me.ckScollegaAG.Size = New System.Drawing.Size(236, 19)
    Me.ckScollegaAG.TabIndex = 5
    '
    'opAltrodb
    '
    Me.opAltrodb.Cursor = System.Windows.Forms.Cursors.Default
    Me.opAltrodb.Location = New System.Drawing.Point(16, 50)
    Me.opAltrodb.Name = "opAltrodb"
    Me.opAltrodb.NTSCheckValue = "S"
    Me.opAltrodb.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opAltrodb.Properties.Appearance.Options.UseBackColor = True
    Me.opAltrodb.Properties.AutoHeight = False
    Me.opAltrodb.Properties.Caption = "Altro Database :"
    Me.opAltrodb.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opAltrodb.Size = New System.Drawing.Size(116, 19)
    Me.opAltrodb.TabIndex = 3
    '
    'opStessodb
    '
    Me.opStessodb.Cursor = System.Windows.Forms.Cursors.Default
    Me.opStessodb.Location = New System.Drawing.Point(16, 32)
    Me.opStessodb.Name = "opStessodb"
    Me.opStessodb.NTSCheckValue = "S"
    Me.opStessodb.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opStessodb.Properties.Appearance.Options.UseBackColor = True
    Me.opStessodb.Properties.AutoHeight = False
    Me.opStessodb.Properties.Caption = "Database corrente"
    Me.opStessodb.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opStessodb.Size = New System.Drawing.Size(116, 19)
    Me.opStessodb.TabIndex = 2
    '
    'frImporta
    '
    Me.frImporta.AllowDrop = True
    Me.frImporta.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frImporta.Appearance.Options.UseBackColor = True
    Me.frImporta.Controls.Add(Me.cmdDesAll)
    Me.frImporta.Controls.Add(Me.cmdSelAll)
    Me.frImporta.Controls.Add(Me.liTabelle)
    Me.frImporta.Controls.Add(Me.frOperazione)
    Me.frImporta.Controls.Add(Me.opImporta1)
    Me.frImporta.Controls.Add(Me.opImporta0)
    Me.frImporta.Cursor = System.Windows.Forms.Cursors.Default
    Me.frImporta.Location = New System.Drawing.Point(11, 80)
    Me.frImporta.Name = "frImporta"
    Me.frImporta.Size = New System.Drawing.Size(262, 350)
    Me.frImporta.TabIndex = 1
    Me.frImporta.Text = "Importa: "
    '
    'cmdDesAll
    '
    Me.cmdDesAll.ImagePath = ""
    Me.cmdDesAll.ImageText = ""
    Me.cmdDesAll.Location = New System.Drawing.Point(137, 257)
    Me.cmdDesAll.Name = "cmdDesAll"
    Me.cmdDesAll.NTSContextMenu = Nothing
    Me.cmdDesAll.Size = New System.Drawing.Size(101, 25)
    Me.cmdDesAll.TabIndex = 5
    Me.cmdDesAll.Text = "Deseleziona t&utto"
    '
    'cmdSelAll
    '
    Me.cmdSelAll.ImagePath = ""
    Me.cmdSelAll.ImageText = ""
    Me.cmdSelAll.Location = New System.Drawing.Point(30, 257)
    Me.cmdSelAll.Name = "cmdSelAll"
    Me.cmdSelAll.NTSContextMenu = Nothing
    Me.cmdSelAll.Size = New System.Drawing.Size(101, 25)
    Me.cmdSelAll.TabIndex = 4
    Me.cmdSelAll.Text = "Se&leziona tutto"
    '
    'liTabelle
    '
    Me.liTabelle.Cursor = System.Windows.Forms.Cursors.Default
    Me.liTabelle.ItemHeight = 14
    Me.liTabelle.Location = New System.Drawing.Point(30, 161)
    Me.liTabelle.Name = "liTabelle"
    Me.liTabelle.NTSDbField = ""
    Me.liTabelle.Size = New System.Drawing.Size(216, 90)
    Me.liTabelle.TabIndex = 3
    '
    'frOperazione
    '
    Me.frOperazione.AllowDrop = True
    Me.frOperazione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frOperazione.Appearance.Options.UseBackColor = True
    Me.frOperazione.Controls.Add(Me.opSovrascrivi)
    Me.frOperazione.Controls.Add(Me.opSostituisci)
    Me.frOperazione.Controls.Add(Me.opAccoda)
    Me.frOperazione.Cursor = System.Windows.Forms.Cursors.Default
    Me.frOperazione.Location = New System.Drawing.Point(30, 75)
    Me.frOperazione.Name = "frOperazione"
    Me.frOperazione.Size = New System.Drawing.Size(216, 80)
    Me.frOperazione.TabIndex = 2
    '
    'opSovrascrivi
    '
    Me.opSovrascrivi.Cursor = System.Windows.Forms.Cursors.Default
    Me.opSovrascrivi.Location = New System.Drawing.Point(5, 61)
    Me.opSovrascrivi.Name = "opSovrascrivi"
    Me.opSovrascrivi.NTSCheckValue = "S"
    Me.opSovrascrivi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opSovrascrivi.Properties.Appearance.Options.UseBackColor = True
    Me.opSovrascrivi.Properties.AutoHeight = False
    Me.opSovrascrivi.Properties.Caption = "Sovrascrivi a &parità di codice"
    Me.opSovrascrivi.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opSovrascrivi.Size = New System.Drawing.Size(203, 19)
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
    Me.opSostituisci.Properties.AutoHeight = False
    Me.opSostituisci.Properties.Caption = "&Sostituisci completamente"
    Me.opSostituisci.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opSostituisci.Size = New System.Drawing.Size(203, 19)
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
    Me.opAccoda.Properties.AutoHeight = False
    Me.opAccoda.Properties.Caption = "Accoda &nuovi record"
    Me.opAccoda.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opAccoda.Size = New System.Drawing.Size(203, 19)
    Me.opAccoda.TabIndex = 0
    '
    'opImporta1
    '
    Me.opImporta1.Cursor = System.Windows.Forms.Cursors.Default
    Me.opImporta1.Location = New System.Drawing.Point(14, 50)
    Me.opImporta1.Name = "opImporta1"
    Me.opImporta1.NTSCheckValue = "S"
    Me.opImporta1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opImporta1.Properties.Appearance.Options.UseBackColor = True
    Me.opImporta1.Properties.AutoHeight = False
    Me.opImporta1.Properties.Caption = "&Alcuni dati"
    Me.opImporta1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opImporta1.Size = New System.Drawing.Size(116, 19)
    Me.opImporta1.TabIndex = 1
    '
    'opImporta0
    '
    Me.opImporta0.Cursor = System.Windows.Forms.Cursors.Default
    Me.opImporta0.Location = New System.Drawing.Point(14, 32)
    Me.opImporta0.Name = "opImporta0"
    Me.opImporta0.NTSCheckValue = "S"
    Me.opImporta0.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opImporta0.Properties.Appearance.Options.UseBackColor = True
    Me.opImporta0.Properties.AutoHeight = False
    Me.opImporta0.Properties.Caption = "&Tutti i dati"
    Me.opImporta0.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opImporta0.Size = New System.Drawing.Size(116, 19)
    Me.opImporta0.TabIndex = 0
    '
    'frDittaDestinazione
    '
    Me.frDittaDestinazione.AllowDrop = True
    Me.frDittaDestinazione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frDittaDestinazione.Appearance.Options.UseBackColor = True
    Me.frDittaDestinazione.Controls.Add(Me.lbDesdittDest)
    Me.frDittaDestinazione.Controls.Add(Me.lbCoddittDest)
    Me.frDittaDestinazione.Cursor = System.Windows.Forms.Cursors.Default
    Me.frDittaDestinazione.Location = New System.Drawing.Point(11, 11)
    Me.frDittaDestinazione.Name = "frDittaDestinazione"
    Me.frDittaDestinazione.Size = New System.Drawing.Size(557, 58)
    Me.frDittaDestinazione.TabIndex = 0
    Me.frDittaDestinazione.Text = "Ditta di destinazione: "
    '
    'lbDesdittDest
    '
    Me.lbDesdittDest.BackColor = System.Drawing.Color.Transparent
    Me.lbDesdittDest.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesdittDest.Location = New System.Drawing.Point(192, 27)
    Me.lbDesdittDest.Name = "lbDesdittDest"
    Me.lbDesdittDest.NTSDbField = ""
    Me.lbDesdittDest.Size = New System.Drawing.Size(360, 21)
    Me.lbDesdittDest.TabIndex = 1
    Me.lbDesdittDest.Tooltip = ""
    Me.lbDesdittDest.UseMnemonic = False
    '
    'lbCoddittDest
    '
    Me.lbCoddittDest.BackColor = System.Drawing.Color.Transparent
    Me.lbCoddittDest.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbCoddittDest.Location = New System.Drawing.Point(8, 27)
    Me.lbCoddittDest.Name = "lbCoddittDest"
    Me.lbCoddittDest.NTSDbField = ""
    Me.lbCoddittDest.Size = New System.Drawing.Size(178, 21)
    Me.lbCoddittDest.TabIndex = 0
    Me.lbCoddittDest.Tooltip = ""
    Me.lbCoddittDest.UseMnemonic = False
    '
    'FRM__IMPD
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(720, 440)
    Me.Controls.Add(Me.pnImpd)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.Name = "FRM__IMPD"
    Me.Text = "IMPORTA DATI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnImpd, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnImpd.ResumeLayout(False)
    CType(Me.frDittaOrigine, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frDittaOrigine.ResumeLayout(False)
    Me.frDittaOrigine.PerformLayout()
    CType(Me.frSqlServer, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frSqlServer.ResumeLayout(False)
    Me.frSqlServer.PerformLayout()
    CType(Me.edDatabase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPwd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edUser.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edServer.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCoddittOr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSvuotaPN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDisabPS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAbilPS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckScollegaAG.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opAltrodb.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opStessodb.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frImporta, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frImporta.ResumeLayout(False)
    Me.frImporta.PerformLayout()
    CType(Me.liTabelle, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frOperazione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frOperazione.ResumeLayout(False)
    Me.frOperazione.PerformLayout()
    CType(Me.opSovrascrivi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opSostituisci.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opAccoda.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opImporta1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opImporta0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frDittaDestinazione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frDittaDestinazione.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      edCoddittOr.NTSSetParam(oMenu, oApp.Tr(Me, 128345191341586147, "Cod. Ditta origine:"), 0)
      ckSvuotaPN.NTSSetParam(oMenu, oApp.Tr(Me, 128345191341898647, "Svuota archivi p.nota e altri arch. contabili"), "S", "N")
      ckDisabPS.NTSSetParam(oMenu, oApp.Tr(Me, 128345191342054897, "Disabilita partite e scadenze su cli/forn."), "S", "N")
      ckAbilPS.NTSSetParam(oMenu, oApp.Tr(Me, 128345191342211147, "Abilita partite e scadenze su cli/forn."), "S", "N")
      ckScollegaAG.NTSSetParam(oMenu, oApp.Tr(Me, 128345191342367397, "Scollega da anagrafica  generale"), "S", "N")
      edDatabase.NTSSetParam(oMenu, oApp.Tr(Me, 128347701068570301, "Database"), 0)
      edPwd.NTSSetParam(oMenu, oApp.Tr(Me, 128347701068726576, "Pwd"), 0)
      edUser.NTSSetParam(oMenu, oApp.Tr(Me, 128347701068882851, "User"), 0)
      edServer.NTSSetParam(oMenu, oApp.Tr(Me, 128347701069039126, "Server"), 0)
      opAltrodb.NTSSetParam(oMenu, oApp.Tr(Me, 128345191342679897, "Altro Database :"), "S")
      opStessodb.NTSSetParam(oMenu, oApp.Tr(Me, 128345191342836147, "Database corrente"), "S")
      liTabelle.NTSSetParam(oApp.Tr(Me, 128345191343148647, "Tabelle"))
      opSovrascrivi.NTSSetParam(oMenu, oApp.Tr(Me, 128345191343304897, "Sovrascrivi a &parità di codice"), "S")
      opSostituisci.NTSSetParam(oMenu, oApp.Tr(Me, 128345191343461147, "&Sostituisci completamente"), "S")
      opAccoda.NTSSetParam(oMenu, oApp.Tr(Me, 128345191343617397, "Accoda &nuovi record"), "S")
      opImporta1.NTSSetParam(oMenu, oApp.Tr(Me, 128873965176897158, "&Alcuni dati"), "S")
      opImporta0.NTSSetParam(oMenu, oApp.Tr(Me, 128345191343773647, "&Tutti i dati"), "S")

      ''non faccio lanciare lo zoom dal controllo visto che non devo cambiare il contenuto del campo ma riposizionare il dataset!!!

      opStessodb.Checked = True
      liTabelle.SelectionMode = SelectionMode.MultiSimple
      edPwd.Properties.PasswordChar = CChar("*")


      edCoddittOr.NTSForzaVisZoom = True
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
  Public Overridable Sub InitEntity(ByRef cleCedi As CLE__CEDI)
    oCleCedi = cleCedi
    AddHandler oCleCedi.RemoteEvent, AddressOf GestisciEventiEntity
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

    '------------------------------
    'collego il datatable agli altri oggetti di form

    Return True
  End Function

#Region "EventiForm"
  Public Overridable Sub FRM__IMPD_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Dim ds As DataSet = Nothing
    Try
      '-------------------------------------------------
      CaricaCombo()

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-----------------------------------------------------------------------------------------
      opImporta0.Checked = True
      '-----------------------------------------------------------------------------------------
      cmdOk.Enabled = False
      '
      frSqlServer.Enabled = False
      ckAbilPS.Enabled = False
      ckDisabPS.Enabled = False
      ckScollegaAG.Enabled = False
      ckSvuotaPN.Enabled = False
      '-----------------------------------------------------------------------------------------
      lbCoddittDest.Text = oCleCedi.strImpdCoddittDest
      lbDesdittDest.Text = oCleCedi.strImpdDescrDittDest
      '-----------------------------------------------------------------------------------------
      '--- Variabile di ritorno settata a 'True' solo se è stata scelta l'opzione 'Tutti i dati'
      '--- perché aggiorna tutti i campi di TABANAZ e al ritorno i campi esposti nella form
      '--- principale devono essere quelli aggiornati
      '-----------------------------------------------------------------------------------------
      oCleCedi.bImpdRefresh = False
      '-----------------------------------------------------------------------------------------

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      Me.Cursor = Cursors.Default

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRM__IMPD_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Try
      '--------------------------------------------------------------------------------------------------------------
      If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
        Zoom()
        e.Handled = True
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub CaricaCombo()
    dttTabelle = New DataTable()
    Dim ds As DataSet = Nothing
    Try
      dttTabelle.Columns.Add("cod", GetType(String))
      dttTabelle.Columns.Add("val", GetType(String))

      dttTabelle.Rows.Add(New Object() {"tabcate", "Categorie clienti/fornitori"})
      dttTabelle.Rows.Add(New Object() {"tabzone", "Zone geografiche"})
      dttTabelle.Rows.Add(New Object() {"tabgmer", "Gruppi Merceologici"})
      dttTabelle.Rows.Add(New Object() {"tabsgme", "Sottogruppi merceologici"})
      dttTabelle.Rows.Add(New Object() {"tabcscl", "Classi sconti clienti"})
      dttTabelle.Rows.Add(New Object() {"tabcpcl", "Classi provvigioni clienti"})
      dttTabelle.Rows.Add(New Object() {"tabcsar", "Classi sconti articoli"})
      dttTabelle.Rows.Add(New Object() {"tabcpar", "Classi provvigioni articoli"})
      dttTabelle.Rows.Add(New Object() {"anagraS", "Piano dei conti"})
      dttTabelle.Rows.Add(New Object() {"anagraC", "Clienti"})
      dttTabelle.Rows.Add(New Object() {"anagraF", "Fornitori"})
      dttTabelle.Rows.Add(New Object() {"tabbanc", "Banche"})
      dttTabelle.Rows.Add(New Object() {"tabcove", "Contropartite ditta"})
      dttTabelle.Rows.Add(New Object() {"tabmaga", "Magazzini/depositi/terzisti"})
      dttTabelle.Rows.Add(New Object() {"tabcage", "Agenti"})
      dttTabelle.Rows.Add(New Object() {"tabtpbf", "Tipi bolle/fatture"})
      dttTabelle.Rows.Add(New Object() {"artico", "Articoli magazzino"})
      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di atitivazione Friendly, non aggiunge alcune voci
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = False Then
        dttTabelle.Rows.Add(New Object() {"anagca", "Piano dei conti di CA"})
        dttTabelle.Rows.Add(New Object() {"analink", "Collegamenti CG/CA"})
        dttTabelle.Rows.Add(New Object() {"budget", "Budget CA"})
      End If
      '--------------------------------------------------------------------------------------------------------------

      dttTabelle.AcceptChanges()

      liTabelle.DataSource = dttTabelle
      liTabelle.DisplayMember = "val"
      liTabelle.ValueMember = "cod"

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Dim i As Integer
    Dim dttTabelleSelect As New DataTable
    Try
      '-----------------------------------------------------------------------------------------
      '--- Serve per un'eventuale scrittura in ACTLOG
      '-----------------------------------------------------------------------------------------
      oCleCedi.strImpdDesogglog = ""
      '-----------------------------------------------------------------------------------------
      '--- Controlla la validità della ditta di origine
      '-----------------------------------------------------------------------------------------
      If Not CheckSelection() Then Exit Sub
      '-----------------------------------------------------------------------------------------
      '--- Chiede conferma dell'elaborazione
      '-----------------------------------------------------------------------------------------
      If opImporta0.Checked = True Then
        strMsg = "Attenzione!" & vbCrLf & _
          "Tutti i dati della ditta '" & lbCoddittDest.Text & "'" & vbCrLf & _
          "dell'azienda di destinazione '" & oCleCedi.strImpdCoddittDest & "'" & vbCrLf & _
          "andranno persi." & vbCrLf & _
          "Continuare?"
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128345812743546836, strMsg)) = MsgBoxResult.No Then Exit Sub
      End If

      dttTabelleSelect = dttTabelle.Clone
      For i = 0 To liTabelle.SelectedIndices().Count - 1
        dttTabelleSelect.ImportRow(dttTabelle.Rows(liTabelle.SelectedIndices(i)))
      Next

      If oCleCedi.Importa(opImporta0.Checked, opImporta1.Checked, lbCoddittDest.Text, opAltrodb.Checked, _
        edCoddittOr.Text, ckScollegaAG.Checked, ckAbilPS.Checked, ckDisabPS.Checked, ckSvuotaPN.Checked, _
        opSostituisci.Checked, opSovrascrivi.Checked, opAccoda.Checked, opStessodb.Checked, lbDesdittDest.Text, _
        lbDesdittOr.Text, dttTabelleSelect, edServer.Text, edDatabase.Text, edUser.Text, edPwd.Text) Then
        cmdAnnulla.Focus()
        If opImporta1.Checked = True Then
          liTabelle.SelectedItem = 0
        End If
      End If

      Me.Cursor = Cursors.Default
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub Zoom()
    Dim oParam As New CLE__CLDP

    Try
      '--------------------------------------------------------------------------------------------------------------
      If opAltrodb.Checked = True Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128344926232523647, "Attenzione!" & vbCrLf & _
          "Non è abilitato lo zoom su ditte di altro database!"))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      oParam.strNomProg = "BN__ANAZ"
      oParam.dPar1 = 0
      oParam.dPar2 = 0
      oParam.dPar3 = 0
      oParam.dPar4 = 0
      oParam.dPar5 = 0
      oParam.strPar1 = "0"
      oMenu.RunZoomNet("NTSInformatica", "FRM__HLDI", "", "BN__HLDI", "Zoom ditte", DittaCorrente, CObj(oParam))
      edCoddittOr.Text = oParam.Ditta
      oParam = Nothing
      '--------------------------------------------------------------------------------------------------------------
      If opAltrodb.Checked = False Then CheckCoddittOr()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdSelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelAll.Click
    Dim i As Integer
    Try
      For i = 0 To (liTabelle.ItemCount - 1)
        liTabelle.SetSelected(i, True)
      Next
      liTabelle.SelectedIndex = 0
      If Trim(edCoddittOr.Text) <> "" Then cmdOk.Focus()
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
      If Trim(edCoddittOr.Text) <> "" Then cmdOk.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub opAltrodb_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles opAltrodb.CheckedChanged
    Try
      frSqlServer.Enabled = True
      If opImporta0.Checked = True Then
        If ckAbilPS.Enabled = False Then ckAbilPS.Enabled = True
        If ckDisabPS.Enabled = False Then ckDisabPS.Enabled = True
        If ckScollegaAG.Enabled = False Then ckScollegaAG.Enabled = True
        If ckSvuotaPN.Enabled = False Then ckSvuotaPN.Enabled = True
      Else
        ckAbilPS.Checked = False
        ckAbilPS.Enabled = False
        ckDisabPS.Checked = False
        ckDisabPS.Enabled = False
        ckScollegaAG.Checked = False
        ckScollegaAG.Enabled = False
        ckSvuotaPN.Checked = False
        ckSvuotaPN.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opImporta0_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles opImporta0.CheckedChanged
    Try
      opImporta_CheckedChanged()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opImporta1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles opImporta1.CheckedChanged
    Try
      opImporta_CheckedChanged()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opStessodb_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles opStessodb.CheckedChanged
    Try
      edServer.Text = ""
      edUser.Text = ""
      edPwd.Text = ""
      edDatabase.Text = ""
      frSqlServer.Enabled = False
      ckAbilPS.Checked = False
      ckAbilPS.Enabled = False
      ckDisabPS.Checked = False
      ckDisabPS.Enabled = False
      ckScollegaAG.Checked = False
      ckScollegaAG.Enabled = False
      ckSvuotaPN.Checked = False
      ckSvuotaPN.Enabled = False
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edCoddittOr_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCoddittOr.LostFocus
    Try
      If opAltrodb.Checked = False Then
        If Not CheckCoddittOr() Then Exit Sub
      End If
      If Trim(edCoddittOr.Text) = "" Then
        cmdOk.Enabled = False
      Else
        cmdOk.Enabled = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edCoddittOr_Modified(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCoddittOr.Modified
    Try
      If Trim(edCoddittOr.Text) = "" Then
        cmdOk.Enabled = False
      Else
        cmdOk.Enabled = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edCoddittOr_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCoddittOr.Validated
    Try
      If Trim(edCoddittOr.Text) = "" Then
        cmdOk.Enabled = False
      Else
        cmdOk.Enabled = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

#End Region

  Public Overridable Sub opImporta_CheckedChanged()
    Dim i As Integer
    Try
      If opImporta0.Checked = True Then
        If liTabelle.Visible = True Then
          For i = 0 To (liTabelle.ItemCount - 1)
            liTabelle.SetSelected(i, False)
          Next
          liTabelle.SelectedItem = 0
        End If
        liTabelle.Visible = False
        opAccoda.Visible = False
        opSovrascrivi.Visible = False
        opSostituisci.Visible = False
        cmdSelAll.Visible = False
        cmdDesAll.Visible = False
        frOperazione.Visible = False
        If opAltrodb.Checked = True Then
          If ckScollegaAG.Enabled = False Then ckScollegaAG.Enabled = True
          If ckAbilPS.Enabled = False Then ckAbilPS.Enabled = True
          If ckDisabPS.Enabled = False Then ckDisabPS.Enabled = True
          If ckSvuotaPN.Enabled = False Then ckSvuotaPN.Enabled = True
        End If
      Else
        liTabelle.Visible = True
        opAccoda.Visible = True
        opSovrascrivi.Visible = True
        opSostituisci.Visible = True
        cmdSelAll.Visible = True
        cmdDesAll.Visible = True
        frOperazione.Visible = True
        opAccoda.Checked = True
        ckScollegaAG.Checked = False
        ckScollegaAG.Enabled = False
        ckAbilPS.Checked = False
        ckAbilPS.Enabled = False
        ckDisabPS.Checked = False
        ckDisabPS.Enabled = False
        ckSvuotaPN.Checked = False
        ckSvuotaPN.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function CheckCoddittOr() As Boolean
    Dim dsTabAnaz As DataSet = Nothing
    Try
      '-----------------------------------------------------------------------------------------
      CheckCoddittOr = False
      '-----------------------------------------------------------------------------------------
      edCoddittOr.Text = (Trim(edCoddittOr.Text)) '.ToUpper
      '-----------------------------------------------------------------------------------------
      If edCoddittOr.Text <> "" Then
        If edCoddittOr.Text = lbCoddittDest.Text Then
          edCoddittOr.Text = ""
          lbDesdittOr.Text = ""
          strMsg = "La ditta di origine è la stessa di quella di destinazione." & vbCrLf & _
            "Ripetere la selezione."
          oApp.MsgBoxErr(oApp.Tr(Me, 128345711723120700, strMsg))
          Exit Function
        End If
      End If
      '-----------------------------------------------------------------------------------------
      If edCoddittOr.Text <> "" Then
        oCleCedi.CheckCoddittOr(edCoddittOr.Text, dsTabAnaz)
        If dsTabAnaz.Tables("TABANAZ").Rows.Count = 0 Then
          edCoddittOr.Text = ""
          lbDesdittOr.Text = ""
          oApp.MsgBoxErr(oApp.Tr(Me, 128345711843399500, "Ditta di origine inesistente."))
          edCoddittOr.Focus()
          Exit Function
        Else
          lbDesdittOr.Text = NTSCStr(dsTabAnaz.Tables("TABANAZ").Rows(0)!tb_azrags1)
        End If
      Else
        lbDesdittOr.Text = ""
      End If
      '-----------------------------------------------------------------------------------------
      CheckCoddittOr = True
      '-----------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function CheckCoddittOrAltroDb() As Boolean
    Dim dsAltrodb As DataSet = Nothing
    Try
      '-----------------------------------------------------------------------------------------
      CheckCoddittOrAltroDb = False
      '-----------------------------------------------------------------------------------------
      edCoddittOr.Text = (Trim(edCoddittOr.Text)) '.ToUpper
      '-----------------------------------------------------------------------------------------
      If edCoddittOr.Text <> "" Then
        oCleCedi.CheckCoddittOrAltroDb(edCoddittOr.Text, edServer.Text, edDatabase.Text, edUser.Text, edPwd.Text, dsAltrodb)
        If dsAltrodb.Tables("TABANAZ").Rows.Count = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128347689490964876, "Ditta di origine inesistente."))
          edCoddittOr.Text = ""
          lbDesdittOr.Text = ""
          edCoddittOr.Focus()
          Exit Function
        Else
          lbDesdittOr.Text = NTSCStr(dsAltrodb.Tables("TABANAZ").Rows(0)!tb_azrags1)
        End If
      Else
        lbDesdittOr.Text = ""
      End If
      '-----------------------------------------------------------------------------------------
      CheckCoddittOrAltroDb = True
      '-----------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function CheckDatabase(ByVal strName As String) As Boolean
    Dim dsAziende As DataSet = Nothing
    Try
      oCleCedi.CheckDatabase(strName, dsAziende)
      If NTSCInt(dsAziende.Tables("AZIENDE").Rows(0)!Totale) = 0 Then Return False

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function CheckSelection() As Boolean
    Try
      '-----------------------------------------------------------------------------------------
      CheckSelection = False
      '-----------------------------------------------------------------------------------------
      If Trim(edCoddittOr.Text) = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128345729038752036, "Indicare la ditta di origine."))
        edCoddittOr.Focus()
        Exit Function
      End If
      If opAltrodb.Checked = True Then
        If Trim(edServer.Text) = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128345729181559966, "Indicare il server di Sql Server."))
          edServer.Focus()
          Exit Function
        End If
        If Trim(edUser.Text) = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128347677983933626, "Indicare l'user di accesso di Sql Server."))
          edUser.Focus()
          Exit Function
        End If
        If Trim(edDatabase.Text) = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128347678060652376, "Indicare il nome del database di origine."))
          edDatabase.Focus()
          Exit Function
        End If
        If Not oCleCedi.CheckConnectionAltroDB(edServer.Text, edDatabase.Text, edUser.Text, edPwd.Text) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128351761709580096, "Connessione ad altro database non riuscita"))
          Exit Function
        End If
      End If
      'If opAltrodb.Checked = True Then 'Non ha senso verificare se il database esiste su arcproc!
      '  If Not CheckDatabase(edDatabase.Text) Then
      '    oApp.MsgBoxErr(oApp.Tr(Me, 128345729955128961, "Attenzione!" & vbCrLf & _
      '           "Il database di origine '" & edDatabase.Text & "' è inesistente."))
      '    edServer.Focus()
      '    Exit Function
      '  End If
      'End If
      If (Trim(edServer.Text) <> "" Or Trim(edUser.Text) <> "" Or Trim(edDatabase.Text) <> "") And opAltrodb.Checked = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128345730758853241, "Non indicare il server, user, password, database di origine, a meno che non si richieda una origine 'altro database'."))
        edServer.Focus()
        Exit Function
      End If
      '-----------------------------------------------------------------------------------------
      If opAltrodb.Checked = False Then
        If Not CheckCoddittOr() Then Exit Function
      Else
        If Not CheckCoddittOrAltroDb() Then Exit Function
      End If
      '-----------------------------------------------------------------------------------------
      If opAltrodb.Checked = True Then
        strMsg = "Attenzione!" & vbCrLf & _
          "Selezionando un altro Database come contenitore della ditta di partenza, " & vbCrLf & _
          "è necessario verificare prima che le tabelle comuni dei due Database siano le stesse" & vbCrLf & _
          "(idem per il Piano dei Conti delle due ditte)." & vbCrLf & _
          "In caso contrario potranno verificarsi degli errori in fase di importazione dati." & vbCrLf & _
          "Proseguire ugualmente?"
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128345733148489039, strMsg)) = MsgBoxResult.No Then Exit Function
      End If
      If ckAbilPS.Checked = True And ckDisabPS.Checked = True Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128345732056628015, "Attenzione!!! Scelta incoerente su abilitazione/disabilitazione partite/scadenze ! Solo uno dei due flag può essere attivato! "))
        Exit Function
      End If
      '-----------------------------------------------------------------------------------------
      CheckSelection = True
      '-----------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

End Class