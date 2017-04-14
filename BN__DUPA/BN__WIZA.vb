#Region "Imports"
Imports System.Data
Imports NTSInformatica.CLN__STD
#End Region

Public Class FRM__WIZA

#Region "Moduli"
  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = 0
  Private ModuliSup_P As Integer = 0
  Private ModuliSupExt_P As Integer = 0
  Private ModuliPtn_P As Integer = 0
  Private ModuliPtnExt_P As Integer = 0

  Public ReadOnly Property Moduli() As Integer
    Get
      Return Moduli_P
    End Get
  End Property
  Public ReadOnly Property ModuliExt() As Integer
    Get
      Return ModuliExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliSup() As Integer
    Get
      Return ModuliSup_P
    End Get
  End Property
  Public ReadOnly Property ModuliSupExt() As Integer
    Get
      Return ModuliSupExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtn() As Integer
    Get
      Return ModuliPtn_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtnExt() As Integer
    Get
      Return ModuliPtnExt_P
    End Get
  End Property
#End Region

#Region "Variabili"
  Public oCleDupa As CLE__DUPA
  Public oCallParams As CLE__CLDP
  Public dsWiza As DataSet
  Public dcWiza As BindingSource = New BindingSource()
  Public strTabella As String = "AZIENDE"
  Public strDefaultDir As String = ""
#End Region

#Region "Variaribli Componenti Form e InitializeComponent"
  Private components As System.ComponentModel.IContainer
  Public WithEvents als_mitt As NTSInformatica.NTSGridColumn
  Public WithEvents als_tipop As NTSInformatica.NTSGridColumn
  Public WithEvents als_tipom As NTSInformatica.NTSGridColumn
  Public WithEvents als_tipoc As NTSInformatica.NTSGridColumn
  Public WithEvents als_tipoe As NTSInformatica.NTSGridColumn
  Public WithEvents pnWiza As NTSInformatica.NTSPanel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdIndietro As NTSInformatica.NTSButton
  Public WithEvents CmdAvantiFine As NTSInformatica.NTSButton
  Public WithEvents lbExt As NTSInformatica.NTSLabel
  Public WithEvents lbDesaz As NTSInformatica.NTSLabel
  Public WithEvents lbCodaz As NTSInformatica.NTSLabel
  Public WithEvents edExt As NTSInformatica.NTSTextBoxStr
  Public WithEvents edCodaz As NTSInformatica.NTSTextBoxStr
  Public WithEvents edDesaz As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTitolo As NTSInformatica.NTSLabel
  Public WithEvents pnSql As NTSInformatica.NTSPanel
  Public WithEvents pnAzienda As NTSInformatica.NTSPanel
  Public WithEvents lbPasso As NTSInformatica.NTSLabel
  Public WithEvents opAutenticazioneWindows As NTSInformatica.NTSRadioButton
  Public WithEvents opAutenticazioneIDPassword As NTSInformatica.NTSRadioButton
  Public WithEvents lbPassword As NTSInformatica.NTSLabel
  Public WithEvents lbLogin As NTSInformatica.NTSLabel
  Public WithEvents lbServerIstanza As NTSInformatica.NTSLabel
  Public WithEvents edPassword As NTSInformatica.NTSTextBoxStr
  Public WithEvents edLogin As NTSInformatica.NTSTextBoxStr
  Public WithEvents edServerIstanza As NTSInformatica.NTSTextBoxStr
  Public WithEvents pnOperazione As NTSInformatica.NTSPanel
  Public WithEvents opNuovo As NTSInformatica.NTSRadioButton
  Public WithEvents opCollega As NTSInformatica.NTSRadioButton

  Private Sub InitializeComponent()
    Me.als_mitt = New NTSInformatica.NTSGridColumn
    Me.als_tipop = New NTSInformatica.NTSGridColumn
    Me.als_tipom = New NTSInformatica.NTSGridColumn
    Me.als_tipoc = New NTSInformatica.NTSGridColumn
    Me.als_tipoe = New NTSInformatica.NTSGridColumn
    Me.pnWiza = New NTSInformatica.NTSPanel
    Me.pnSql = New NTSInformatica.NTSPanel
    Me.opAutenticazioneWindows = New NTSInformatica.NTSRadioButton
    Me.opAutenticazioneIDPassword = New NTSInformatica.NTSRadioButton
    Me.lbPassword = New NTSInformatica.NTSLabel
    Me.lbLogin = New NTSInformatica.NTSLabel
    Me.lbServerIstanza = New NTSInformatica.NTSLabel
    Me.edPassword = New NTSInformatica.NTSTextBoxStr
    Me.edLogin = New NTSInformatica.NTSTextBoxStr
    Me.edServerIstanza = New NTSInformatica.NTSTextBoxStr
    Me.pnOperazione = New NTSInformatica.NTSPanel
    Me.ckUnicode = New NTSInformatica.NTSCheckBox
    Me.lbDirnota = New NTSInformatica.NTSLabel
    Me.edLdf = New NTSInformatica.NTSTextBoxStr
    Me.edMdf = New NTSInformatica.NTSTextBoxStr
    Me.lbLdf = New NTSInformatica.NTSLabel
    Me.lbMdf = New NTSInformatica.NTSLabel
    Me.opAttach = New NTSInformatica.NTSRadioButton
    Me.opNuovo = New NTSInformatica.NTSRadioButton
    Me.opNessuna = New NTSInformatica.NTSRadioButton
    Me.opCollega = New NTSInformatica.NTSRadioButton
    Me.pnAzienda = New NTSInformatica.NTSPanel
    Me.lbCodaz = New NTSInformatica.NTSLabel
    Me.lbExt = New NTSInformatica.NTSLabel
    Me.edDesaz = New NTSInformatica.NTSTextBoxStr
    Me.edCodaz = New NTSInformatica.NTSTextBoxStr
    Me.lbDesaz = New NTSInformatica.NTSLabel
    Me.edExt = New NTSInformatica.NTSTextBoxStr
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdIndietro = New NTSInformatica.NTSButton
    Me.lbTitolo = New NTSInformatica.NTSLabel
    Me.lbPasso = New NTSInformatica.NTSLabel
    Me.CmdAvantiFine = New NTSInformatica.NTSButton
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnWiza, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnWiza.SuspendLayout()
    CType(Me.pnSql, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSql.SuspendLayout()
    CType(Me.opAutenticazioneWindows.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opAutenticazioneIDPassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edLogin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edServerIstanza.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnOperazione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnOperazione.SuspendLayout()
    CType(Me.ckUnicode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edLdf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edMdf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opAttach.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opNuovo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opNessuna.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opCollega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnAzienda, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAzienda.SuspendLayout()
    CType(Me.edDesaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edExt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'als_mitt
    '
    Me.als_mitt.AppearanceCell.Options.UseBackColor = True
    Me.als_mitt.AppearanceCell.Options.UseTextOptions = True
    Me.als_mitt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.als_mitt.Caption = "Operatore"
    Me.als_mitt.Enabled = True
    Me.als_mitt.FieldName = "als_mitt"
    Me.als_mitt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.als_mitt.Name = "als_mitt"
    Me.als_mitt.NTSRepositoryComboBox = Nothing
    Me.als_mitt.NTSRepositoryItemCheck = Nothing
    Me.als_mitt.NTSRepositoryItemMemo = Nothing
    Me.als_mitt.NTSRepositoryItemText = Nothing
    Me.als_mitt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.als_mitt.OptionsFilter.AllowFilter = False
    Me.als_mitt.Visible = True
    Me.als_mitt.VisibleIndex = 0
    Me.als_mitt.Width = 62
    '
    'als_tipop
    '
    Me.als_tipop.AppearanceCell.Options.UseBackColor = True
    Me.als_tipop.AppearanceCell.Options.UseTextOptions = True
    Me.als_tipop.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.als_tipop.Caption = "Tipo alert popup"
    Me.als_tipop.Enabled = True
    Me.als_tipop.FieldName = "als_tipop"
    Me.als_tipop.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.als_tipop.Name = "als_tipop"
    Me.als_tipop.NTSRepositoryComboBox = Nothing
    Me.als_tipop.NTSRepositoryItemCheck = Nothing
    Me.als_tipop.NTSRepositoryItemMemo = Nothing
    Me.als_tipop.NTSRepositoryItemText = Nothing
    Me.als_tipop.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.als_tipop.OptionsFilter.AllowFilter = False
    Me.als_tipop.Visible = True
    Me.als_tipop.VisibleIndex = 1
    Me.als_tipop.Width = 90
    '
    'als_tipom
    '
    Me.als_tipom.AppearanceCell.Options.UseBackColor = True
    Me.als_tipom.AppearanceCell.Options.UseTextOptions = True
    Me.als_tipom.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.als_tipom.Caption = "Tipo alert e-mail"
    Me.als_tipom.Enabled = True
    Me.als_tipom.FieldName = "als_tipom"
    Me.als_tipom.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.als_tipom.Name = "als_tipom"
    Me.als_tipom.NTSRepositoryComboBox = Nothing
    Me.als_tipom.NTSRepositoryItemCheck = Nothing
    Me.als_tipom.NTSRepositoryItemMemo = Nothing
    Me.als_tipom.NTSRepositoryItemText = Nothing
    Me.als_tipom.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.als_tipom.OptionsFilter.AllowFilter = False
    Me.als_tipom.Visible = True
    Me.als_tipom.VisibleIndex = 2
    Me.als_tipom.Width = 88
    '
    'als_tipoc
    '
    Me.als_tipoc.AppearanceCell.Options.UseBackColor = True
    Me.als_tipoc.AppearanceCell.Options.UseTextOptions = True
    Me.als_tipoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.als_tipoc.Caption = "Tipo alert attività C.R.M."
    Me.als_tipoc.Enabled = True
    Me.als_tipoc.FieldName = "als_tipoc"
    Me.als_tipoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.als_tipoc.Name = "als_tipoc"
    Me.als_tipoc.NTSRepositoryComboBox = Nothing
    Me.als_tipoc.NTSRepositoryItemCheck = Nothing
    Me.als_tipoc.NTSRepositoryItemMemo = Nothing
    Me.als_tipoc.NTSRepositoryItemText = Nothing
    Me.als_tipoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.als_tipoc.OptionsFilter.AllowFilter = False
    Me.als_tipoc.Visible = True
    Me.als_tipoc.VisibleIndex = 3
    Me.als_tipoc.Width = 131
    '
    'als_tipoe
    '
    Me.als_tipoe.AppearanceCell.Options.UseBackColor = True
    Me.als_tipoe.AppearanceCell.Options.UseTextOptions = True
    Me.als_tipoe.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.als_tipoe.Caption = "Tipo alert procedura da eseguire"
    Me.als_tipoe.Enabled = True
    Me.als_tipoe.FieldName = "als_tipoe"
    Me.als_tipoe.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.als_tipoe.Name = "als_tipoe"
    Me.als_tipoe.NTSRepositoryComboBox = Nothing
    Me.als_tipoe.NTSRepositoryItemCheck = Nothing
    Me.als_tipoe.NTSRepositoryItemMemo = Nothing
    Me.als_tipoe.NTSRepositoryItemText = Nothing
    Me.als_tipoe.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.als_tipoe.OptionsFilter.AllowFilter = False
    Me.als_tipoe.Visible = True
    Me.als_tipoe.VisibleIndex = 4
    Me.als_tipoe.Width = 168
    '
    'pnWiza
    '
    Me.pnWiza.AllowDrop = True
    Me.pnWiza.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnWiza.Appearance.Options.UseBackColor = True
    Me.pnWiza.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnWiza.Controls.Add(Me.pnSql)
    Me.pnWiza.Controls.Add(Me.pnOperazione)
    Me.pnWiza.Controls.Add(Me.pnAzienda)
    Me.pnWiza.Controls.Add(Me.cmdAnnulla)
    Me.pnWiza.Controls.Add(Me.cmdIndietro)
    Me.pnWiza.Controls.Add(Me.lbTitolo)
    Me.pnWiza.Controls.Add(Me.lbPasso)
    Me.pnWiza.Controls.Add(Me.CmdAvantiFine)
    Me.pnWiza.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnWiza.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnWiza.Location = New System.Drawing.Point(0, 0)
    Me.pnWiza.Name = "pnWiza"
    Me.pnWiza.NTSActiveTrasparency = True
    Me.pnWiza.Size = New System.Drawing.Size(371, 269)
    Me.pnWiza.TabIndex = 6
    Me.pnWiza.Text = "NtsPanel1"
    '
    'pnSql
    '
    Me.pnSql.AllowDrop = True
    Me.pnSql.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSql.Appearance.Options.UseBackColor = True
    Me.pnSql.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSql.Controls.Add(Me.opAutenticazioneWindows)
    Me.pnSql.Controls.Add(Me.opAutenticazioneIDPassword)
    Me.pnSql.Controls.Add(Me.lbPassword)
    Me.pnSql.Controls.Add(Me.lbLogin)
    Me.pnSql.Controls.Add(Me.lbServerIstanza)
    Me.pnSql.Controls.Add(Me.edPassword)
    Me.pnSql.Controls.Add(Me.edLogin)
    Me.pnSql.Controls.Add(Me.edServerIstanza)
    Me.pnSql.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnSql.Location = New System.Drawing.Point(0, 67)
    Me.pnSql.Name = "pnSql"
    Me.pnSql.NTSActiveTrasparency = True
    Me.pnSql.Size = New System.Drawing.Size(371, 154)
    Me.pnSql.TabIndex = 527
    Me.pnSql.Text = "NtsPanel2"
    Me.pnSql.Visible = False
    '
    'opAutenticazioneWindows
    '
    Me.opAutenticazioneWindows.Cursor = System.Windows.Forms.Cursors.Default
    Me.opAutenticazioneWindows.Location = New System.Drawing.Point(30, 38)
    Me.opAutenticazioneWindows.Name = "opAutenticazioneWindows"
    Me.opAutenticazioneWindows.NTSCheckValue = "S"
    Me.opAutenticazioneWindows.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opAutenticazioneWindows.Properties.Appearance.Options.UseBackColor = True
    Me.opAutenticazioneWindows.Properties.AutoHeight = False
    Me.opAutenticazioneWindows.Properties.Caption = "Utilizza autenticazione &Windows integrata"
    Me.opAutenticazioneWindows.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opAutenticazioneWindows.Size = New System.Drawing.Size(328, 19)
    Me.opAutenticazioneWindows.TabIndex = 523
    '
    'opAutenticazioneIDPassword
    '
    Me.opAutenticazioneIDPassword.Cursor = System.Windows.Forms.Cursors.Default
    Me.opAutenticazioneIDPassword.EditValue = True
    Me.opAutenticazioneIDPassword.Location = New System.Drawing.Point(31, 62)
    Me.opAutenticazioneIDPassword.Name = "opAutenticazioneIDPassword"
    Me.opAutenticazioneIDPassword.NTSCheckValue = "S"
    Me.opAutenticazioneIDPassword.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opAutenticazioneIDPassword.Properties.Appearance.Options.UseBackColor = True
    Me.opAutenticazioneIDPassword.Properties.AutoHeight = False
    Me.opAutenticazioneIDPassword.Properties.Caption = "Utilizza &autenticazione SQL Server tramite ID e password"
    Me.opAutenticazioneIDPassword.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opAutenticazioneIDPassword.Size = New System.Drawing.Size(328, 19)
    Me.opAutenticazioneIDPassword.TabIndex = 523
    '
    'lbPassword
    '
    Me.lbPassword.AutoSize = True
    Me.lbPassword.BackColor = System.Drawing.Color.Transparent
    Me.lbPassword.Location = New System.Drawing.Point(27, 124)
    Me.lbPassword.Name = "lbPassword"
    Me.lbPassword.NTSDbField = ""
    Me.lbPassword.Size = New System.Drawing.Size(57, 13)
    Me.lbPassword.TabIndex = 519
    Me.lbPassword.Text = "Password:"
    Me.lbPassword.Tooltip = ""
    Me.lbPassword.UseMnemonic = False
    '
    'lbLogin
    '
    Me.lbLogin.AutoSize = True
    Me.lbLogin.BackColor = System.Drawing.Color.Transparent
    Me.lbLogin.Location = New System.Drawing.Point(27, 98)
    Me.lbLogin.Name = "lbLogin"
    Me.lbLogin.NTSDbField = ""
    Me.lbLogin.Size = New System.Drawing.Size(50, 13)
    Me.lbLogin.TabIndex = 519
    Me.lbLogin.Text = "Login ID:"
    Me.lbLogin.Tooltip = ""
    Me.lbLogin.UseMnemonic = False
    '
    'lbServerIstanza
    '
    Me.lbServerIstanza.AutoSize = True
    Me.lbServerIstanza.BackColor = System.Drawing.Color.Transparent
    Me.lbServerIstanza.Location = New System.Drawing.Point(27, 14)
    Me.lbServerIstanza.Name = "lbServerIstanza"
    Me.lbServerIstanza.NTSDbField = ""
    Me.lbServerIstanza.Size = New System.Drawing.Size(83, 13)
    Me.lbServerIstanza.TabIndex = 519
    Me.lbServerIstanza.Text = "Server\Istanza:"
    Me.lbServerIstanza.Tooltip = ""
    Me.lbServerIstanza.UseMnemonic = False
    '
    'edPassword
    '
    Me.edPassword.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPassword.EditValue = ""
    Me.edPassword.Location = New System.Drawing.Point(118, 121)
    Me.edPassword.Name = "edPassword"
    Me.edPassword.NTSDbField = ""
    Me.edPassword.NTSForzaVisZoom = False
    Me.edPassword.NTSOldValue = ""
    Me.edPassword.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPassword.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPassword.Properties.AutoHeight = False
    Me.edPassword.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPassword.Properties.MaxLength = 65536
    Me.edPassword.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
    Me.edPassword.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPassword.Size = New System.Drawing.Size(135, 20)
    Me.edPassword.TabIndex = 522
    Me.edPassword.TextStr = ""
    '
    'edLogin
    '
    Me.edLogin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edLogin.EditValue = ""
    Me.edLogin.Location = New System.Drawing.Point(118, 95)
    Me.edLogin.Name = "edLogin"
    Me.edLogin.NTSDbField = ""
    Me.edLogin.NTSForzaVisZoom = False
    Me.edLogin.NTSOldValue = ""
    Me.edLogin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edLogin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edLogin.Properties.AutoHeight = False
    Me.edLogin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edLogin.Properties.MaxLength = 65536
    Me.edLogin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edLogin.Size = New System.Drawing.Size(135, 20)
    Me.edLogin.TabIndex = 522
    Me.edLogin.TextStr = ""
    '
    'edServerIstanza
    '
    Me.edServerIstanza.Cursor = System.Windows.Forms.Cursors.Default
    Me.edServerIstanza.EditValue = ""
    Me.edServerIstanza.Location = New System.Drawing.Point(118, 11)
    Me.edServerIstanza.Name = "edServerIstanza"
    Me.edServerIstanza.NTSDbField = ""
    Me.edServerIstanza.NTSForzaVisZoom = False
    Me.edServerIstanza.NTSOldValue = ""
    Me.edServerIstanza.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edServerIstanza.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edServerIstanza.Properties.AutoHeight = False
    Me.edServerIstanza.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edServerIstanza.Properties.MaxLength = 65536
    Me.edServerIstanza.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edServerIstanza.Size = New System.Drawing.Size(135, 20)
    Me.edServerIstanza.TabIndex = 522
    Me.edServerIstanza.TextStr = ""
    '
    'pnOperazione
    '
    Me.pnOperazione.AllowDrop = True
    Me.pnOperazione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnOperazione.Appearance.Options.UseBackColor = True
    Me.pnOperazione.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnOperazione.Controls.Add(Me.ckUnicode)
    Me.pnOperazione.Controls.Add(Me.lbDirnota)
    Me.pnOperazione.Controls.Add(Me.edLdf)
    Me.pnOperazione.Controls.Add(Me.edMdf)
    Me.pnOperazione.Controls.Add(Me.lbLdf)
    Me.pnOperazione.Controls.Add(Me.lbMdf)
    Me.pnOperazione.Controls.Add(Me.opAttach)
    Me.pnOperazione.Controls.Add(Me.opNuovo)
    Me.pnOperazione.Controls.Add(Me.opNessuna)
    Me.pnOperazione.Controls.Add(Me.opCollega)
    Me.pnOperazione.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnOperazione.Location = New System.Drawing.Point(0, 67)
    Me.pnOperazione.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnOperazione.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnOperazione.Name = "pnOperazione"
    Me.pnOperazione.NTSActiveTrasparency = True
    Me.pnOperazione.Size = New System.Drawing.Size(371, 154)
    Me.pnOperazione.TabIndex = 528
    Me.pnOperazione.Text = "NtsPanel2"
    Me.pnOperazione.Visible = False
    '
    'ckUnicode
    '
    Me.ckUnicode.Location = New System.Drawing.Point(47, 24)
    Me.ckUnicode.Name = "ckUnicode"
    Me.ckUnicode.NTSCheckValue = "S"
    Me.ckUnicode.NTSUnCheckValue = "N"
    Me.ckUnicode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckUnicode.Properties.Appearance.Options.UseBackColor = True
    Me.ckUnicode.Properties.AutoHeight = False
    Me.ckUnicode.Properties.Caption = "Caratteri Unicode"
    Me.ckUnicode.Size = New System.Drawing.Size(114, 19)
    Me.ckUnicode.TabIndex = 530
    Me.ckUnicode.Visible = True
    '
    'lbDirnota
    '
    Me.lbDirnota.AutoSize = True
    Me.lbDirnota.BackColor = System.Drawing.Color.Transparent
    Me.lbDirnota.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbDirnota.Location = New System.Drawing.Point(86, 95)
    Me.lbDirnota.Name = "lbDirnota"
    Me.lbDirnota.NTSDbField = ""
    Me.lbDirnota.Size = New System.Drawing.Size(244, 11)
    Me.lbDirnota.TabIndex = 529
    Me.lbDirnota.Text = "Le directory si riferiscono al server SQL e non al PC locale"
    Me.lbDirnota.Tooltip = ""
    Me.lbDirnota.UseMnemonic = False
    '
    'edLdf
    '
    Me.edLdf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edLdf.EditValue = ""
    Me.edLdf.Location = New System.Drawing.Point(65, 130)
    Me.edLdf.Name = "edLdf"
    Me.edLdf.NTSDbField = ""
    Me.edLdf.NTSForzaVisZoom = False
    Me.edLdf.NTSOldValue = ""
    Me.edLdf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edLdf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edLdf.Properties.AutoHeight = False
    Me.edLdf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edLdf.Properties.MaxLength = 65536
    Me.edLdf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edLdf.Size = New System.Drawing.Size(294, 20)
    Me.edLdf.TabIndex = 528
    Me.edLdf.TextStr = ""
    '
    'edMdf
    '
    Me.edMdf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMdf.EditValue = ""
    Me.edMdf.Location = New System.Drawing.Point(65, 106)
    Me.edMdf.Name = "edMdf"
    Me.edMdf.NTSDbField = ""
    Me.edMdf.NTSForzaVisZoom = False
    Me.edMdf.NTSOldValue = ""
    Me.edMdf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMdf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMdf.Properties.AutoHeight = False
    Me.edMdf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMdf.Properties.MaxLength = 65536
    Me.edMdf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMdf.Size = New System.Drawing.Size(294, 20)
    Me.edMdf.TabIndex = 527
    Me.edMdf.TextStr = ""
    '
    'lbLdf
    '
    Me.lbLdf.AutoSize = True
    Me.lbLdf.BackColor = System.Drawing.Color.Transparent
    Me.lbLdf.Location = New System.Drawing.Point(9, 133)
    Me.lbLdf.Name = "lbLdf"
    Me.lbLdf.NTSDbField = ""
    Me.lbLdf.Size = New System.Drawing.Size(44, 13)
    Me.lbLdf.TabIndex = 526
    Me.lbLdf.Text = "File LDF"
    Me.lbLdf.Tooltip = ""
    Me.lbLdf.UseMnemonic = False
    '
    'lbMdf
    '
    Me.lbMdf.AutoSize = True
    Me.lbMdf.BackColor = System.Drawing.Color.Transparent
    Me.lbMdf.Location = New System.Drawing.Point(9, 109)
    Me.lbMdf.Name = "lbMdf"
    Me.lbMdf.NTSDbField = ""
    Me.lbMdf.Size = New System.Drawing.Size(47, 13)
    Me.lbMdf.TabIndex = 525
    Me.lbMdf.Text = "File MDF"
    Me.lbMdf.Tooltip = ""
    Me.lbMdf.UseMnemonic = False
    '
    'opAttach
    '
    Me.opAttach.Cursor = System.Windows.Forms.Cursors.Default
    Me.opAttach.Location = New System.Drawing.Point(30, 68)
    Me.opAttach.Name = "opAttach"
    Me.opAttach.NTSCheckValue = "S"
    Me.opAttach.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opAttach.Properties.Appearance.Options.UseBackColor = True
    Me.opAttach.Properties.AutoHeight = False
    Me.opAttach.Properties.Caption = "Collega il file .MDF/.LDF a Server SQL"
    Me.opAttach.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opAttach.Size = New System.Drawing.Size(212, 19)
    Me.opAttach.TabIndex = 524
    '
    'opNuovo
    '
    Me.opNuovo.Cursor = System.Windows.Forms.Cursors.Default
    Me.opNuovo.EditValue = True
    Me.opNuovo.Location = New System.Drawing.Point(30, 4)
    Me.opNuovo.Name = "opNuovo"
    Me.opNuovo.NTSCheckValue = "S"
    Me.opNuovo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opNuovo.Properties.Appearance.Options.UseBackColor = True
    Me.opNuovo.Properties.AutoHeight = False
    Me.opNuovo.Properties.Caption = "Crea &nuovo database"
    Me.opNuovo.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opNuovo.Size = New System.Drawing.Size(144, 19)
    Me.opNuovo.TabIndex = 523
    '
    'opNessuna
    '
    Me.opNessuna.Cursor = System.Windows.Forms.Cursors.Default
    Me.opNessuna.Location = New System.Drawing.Point(0, 87)
    Me.opNessuna.Name = "opNessuna"
    Me.opNessuna.NTSCheckValue = "S"
    Me.opNessuna.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opNessuna.Properties.Appearance.Options.UseBackColor = True
    Me.opNessuna.Properties.AutoHeight = False
    Me.opNessuna.Properties.Caption = "Nessuna"
    Me.opNessuna.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opNessuna.Size = New System.Drawing.Size(28, 19)
    Me.opNessuna.TabIndex = 523
    Me.opNessuna.Visible = False
    '
    'opCollega
    '
    Me.opCollega.Cursor = System.Windows.Forms.Cursors.Default
    Me.opCollega.Location = New System.Drawing.Point(30, 43)
    Me.opCollega.Name = "opCollega"
    Me.opCollega.NTSCheckValue = "S"
    Me.opCollega.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opCollega.Properties.Appearance.Options.UseBackColor = True
    Me.opCollega.Properties.AutoHeight = False
    Me.opCollega.Properties.Caption = "Crea da database esistente"
    Me.opCollega.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opCollega.Size = New System.Drawing.Size(165, 19)
    Me.opCollega.TabIndex = 523
    '
    'pnAzienda
    '
    Me.pnAzienda.AllowDrop = True
    Me.pnAzienda.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAzienda.Appearance.Options.UseBackColor = True
    Me.pnAzienda.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAzienda.Controls.Add(Me.lbCodaz)
    Me.pnAzienda.Controls.Add(Me.lbExt)
    Me.pnAzienda.Controls.Add(Me.edDesaz)
    Me.pnAzienda.Controls.Add(Me.edCodaz)
    Me.pnAzienda.Controls.Add(Me.lbDesaz)
    Me.pnAzienda.Controls.Add(Me.edExt)
    Me.pnAzienda.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnAzienda.Location = New System.Drawing.Point(0, 67)
    Me.pnAzienda.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnAzienda.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnAzienda.Name = "pnAzienda"
    Me.pnAzienda.NTSActiveTrasparency = True
    Me.pnAzienda.Size = New System.Drawing.Size(371, 154)
    Me.pnAzienda.TabIndex = 526
    Me.pnAzienda.Text = "NtsPanel2"
    '
    'lbCodaz
    '
    Me.lbCodaz.AutoSize = True
    Me.lbCodaz.BackColor = System.Drawing.Color.Transparent
    Me.lbCodaz.Location = New System.Drawing.Point(27, 14)
    Me.lbCodaz.Name = "lbCodaz"
    Me.lbCodaz.NTSDbField = ""
    Me.lbCodaz.Size = New System.Drawing.Size(79, 13)
    Me.lbCodaz.TabIndex = 519
    Me.lbCodaz.Text = "Codice azienda"
    Me.lbCodaz.Tooltip = ""
    Me.lbCodaz.UseMnemonic = False
    '
    'lbExt
    '
    Me.lbExt.AutoSize = True
    Me.lbExt.BackColor = System.Drawing.Color.Transparent
    Me.lbExt.Location = New System.Drawing.Point(27, 66)
    Me.lbExt.Name = "lbExt"
    Me.lbExt.NTSDbField = ""
    Me.lbExt.Size = New System.Drawing.Size(85, 13)
    Me.lbExt.TabIndex = 521
    Me.lbExt.Text = "Ditta predefinita"
    Me.lbExt.Tooltip = ""
    Me.lbExt.UseMnemonic = False
    '
    'edDesaz
    '
    Me.edDesaz.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edDesaz.EditValue = ""
    Me.edDesaz.Location = New System.Drawing.Point(118, 37)
    Me.edDesaz.Name = "edDesaz"
    Me.edDesaz.NTSDbField = ""
    Me.edDesaz.NTSForzaVisZoom = False
    Me.edDesaz.NTSOldValue = ""
    Me.edDesaz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDesaz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDesaz.Properties.AutoHeight = False
    Me.edDesaz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDesaz.Properties.MaxLength = 65536
    Me.edDesaz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDesaz.Size = New System.Drawing.Size(240, 20)
    Me.edDesaz.TabIndex = 523
    Me.edDesaz.TextStr = ""
    '
    'edCodaz
    '
    Me.edCodaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodaz.EditValue = ""
    Me.edCodaz.Location = New System.Drawing.Point(118, 11)
    Me.edCodaz.Name = "edCodaz"
    Me.edCodaz.NTSDbField = ""
    Me.edCodaz.NTSForzaVisZoom = False
    Me.edCodaz.NTSOldValue = ""
    Me.edCodaz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodaz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodaz.Properties.AutoHeight = False
    Me.edCodaz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodaz.Properties.MaxLength = 65536
    Me.edCodaz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodaz.Size = New System.Drawing.Size(135, 20)
    Me.edCodaz.TabIndex = 522
    Me.edCodaz.TextStr = ""
    '
    'lbDesaz
    '
    Me.lbDesaz.AutoSize = True
    Me.lbDesaz.BackColor = System.Drawing.Color.Transparent
    Me.lbDesaz.Location = New System.Drawing.Point(27, 40)
    Me.lbDesaz.Name = "lbDesaz"
    Me.lbDesaz.NTSDbField = ""
    Me.lbDesaz.Size = New System.Drawing.Size(81, 13)
    Me.lbDesaz.TabIndex = 520
    Me.lbDesaz.Text = "Ragione sociale"
    Me.lbDesaz.Tooltip = ""
    Me.lbDesaz.UseMnemonic = False
    '
    'edExt
    '
    Me.edExt.Cursor = System.Windows.Forms.Cursors.Default
    Me.edExt.EditValue = ""
    Me.edExt.Location = New System.Drawing.Point(118, 63)
    Me.edExt.Name = "edExt"
    Me.edExt.NTSDbField = ""
    Me.edExt.NTSForzaVisZoom = False
    Me.edExt.NTSOldValue = ""
    Me.edExt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edExt.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edExt.Properties.AutoHeight = False
    Me.edExt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edExt.Properties.MaxLength = 65536
    Me.edExt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edExt.Size = New System.Drawing.Size(135, 20)
    Me.edExt.TabIndex = 524
    Me.edExt.TextStr = ""
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(272, 236)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(86, 23)
    Me.cmdAnnulla.TabIndex = 525
    Me.cmdAnnulla.Text = "Annulla"
    '
    'cmdIndietro
    '
    Me.cmdIndietro.ImageText = ""
    Me.cmdIndietro.Location = New System.Drawing.Point(88, 236)
    Me.cmdIndietro.Name = "cmdIndietro"
    Me.cmdIndietro.NTSContextMenu = Nothing
    Me.cmdIndietro.Size = New System.Drawing.Size(86, 23)
    Me.cmdIndietro.TabIndex = 525
    Me.cmdIndietro.Text = "Indietro"
    Me.cmdIndietro.Visible = False
    '
    'lbTitolo
    '
    Me.lbTitolo.AutoSize = True
    Me.lbTitolo.BackColor = System.Drawing.Color.Transparent
    Me.lbTitolo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbTitolo.Location = New System.Drawing.Point(27, 9)
    Me.lbTitolo.Name = "lbTitolo"
    Me.lbTitolo.NTSDbField = ""
    Me.lbTitolo.Size = New System.Drawing.Size(215, 13)
    Me.lbTitolo.TabIndex = 519
    Me.lbTitolo.Text = "Creazione guidata azienda/database"
    Me.lbTitolo.Tooltip = ""
    Me.lbTitolo.UseMnemonic = False
    '
    'lbPasso
    '
    Me.lbPasso.AutoSize = True
    Me.lbPasso.BackColor = System.Drawing.Color.Transparent
    Me.lbPasso.Location = New System.Drawing.Point(27, 39)
    Me.lbPasso.Name = "lbPasso"
    Me.lbPasso.NTSDbField = ""
    Me.lbPasso.Size = New System.Drawing.Size(209, 13)
    Me.lbPasso.TabIndex = 519
    Me.lbPasso.Text = "Impostare i dati relativi alla nuova azienda"
    Me.lbPasso.Tooltip = ""
    Me.lbPasso.UseMnemonic = False
    '
    'CmdAvantiFine
    '
    Me.CmdAvantiFine.ImageText = ""
    Me.CmdAvantiFine.Location = New System.Drawing.Point(180, 236)
    Me.CmdAvantiFine.Name = "CmdAvantiFine"
    Me.CmdAvantiFine.NTSContextMenu = Nothing
    Me.CmdAvantiFine.Size = New System.Drawing.Size(86, 23)
    Me.CmdAvantiFine.TabIndex = 525
    Me.CmdAvantiFine.Text = "Avanti"
    '
    'FRM__WIZA
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(371, 269)
    Me.Controls.Add(Me.pnWiza)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.LookAndFeel.SkinName = "Money Twins"
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__WIZA"
    Me.Text = "CREAZIONE GUIDATA AZIENDA/DATABASE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnWiza, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnWiza.ResumeLayout(False)
    Me.pnWiza.PerformLayout()
    CType(Me.pnSql, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSql.ResumeLayout(False)
    Me.pnSql.PerformLayout()
    CType(Me.opAutenticazioneWindows.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opAutenticazioneIDPassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edLogin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edServerIstanza.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnOperazione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnOperazione.ResumeLayout(False)
    Me.pnOperazione.PerformLayout()
    CType(Me.ckUnicode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edLdf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edMdf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opAttach.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opNuovo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opNessuna.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opCollega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnAzienda, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAzienda.ResumeLayout(False)
    Me.pnAzienda.PerformLayout()
    CType(Me.edDesaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edExt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
#End Region

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
  Public Overridable Sub InitEntity(ByRef oCleDupa As CLE__DUPA)
    Try
      Me.oCleDupa = oCleDupa
      Me.dsWiza = oCleDupa.dsShared
      AddHandler Me.oCleDupa.RemoteEvent, AddressOf GestisciEventiEntity
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      edCodaz.NTSSetParam(oMenu, oApp.Tr(Me, 128230023372625010, "Codice azienda"), 25, True)
      edDesaz.NTSSetParam(oMenu, oApp.Tr(Me, 128230023372625011, "Descrizione azienda"), 50, True)
      edExt.NTSSetParam(oMenu, oApp.Tr(Me, 128230023372625012, "Ditta predefinita"), 12, True)

      opAttach.NTSSetParam(oMenu, oApp.Tr(Me, 129778498140593312, "Nuovo"), "N")
      opAttach.NTSSetParam(oMenu, oApp.Tr(Me, 129778498125873278, "Collega"), "C")
      opAttach.NTSSetParam(oMenu, oApp.Tr(Me, 129778497588962309, "Attacca i files MDF/LDF al server sql"), "A")
      opNessuna.NTSSetParam(oMenu, oApp.Tr(Me, 129778498108925240, "Nessuna"), "X")

      edMdf.NTSSetParam(oMenu, oApp.Tr(Me, 129778498542554020, "File .MDF"), 0, True)
      edLdf.NTSSetParam(oMenu, oApp.Tr(Me, 129778498555738049, "File .LDF"), 0, True)

      edServerIstanza.NTSSetParamZoom("")
      edPassword.NTSSetParamZoom("")
      edLogin.NTSSetParamZoom("")

      edMdf.NTSSetParamZoom(".")
      edLdf.NTSSetParamZoom(".")
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
  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano già  stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      edCodaz.NTSDbField = strTabella & ".AzCodaz"
      edDesaz.NTSDbField = strTabella & ".AzDescr"
      edExt.NTSDbField = strTabella & ".AzExt"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcWiza, Me)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi di Form"
  Public Overridable Sub FRM__ALTA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      dcWiza.DataSource = dsWiza.Tables(strTabella)


      Bindcontrols()
      dcWiza.MoveLast()


      edServerIstanza.Text = oCleDupa.EstraiParametroDaConnectionString(oApp.PrcConnect, "SERVER")

      edLogin.Text = "sa"

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRM__WIZA_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
    Try
      Me.Text = UCase(oApp.Tr(Me, 128345816743546736, "CREAZIONE GUIDATA AZIENDA/DATABASE"))
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Sub cmdIndietro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdIndietro.Click
    Try
      If pnSql.Visible Then
        SetControls(1)
      Else
        If pnOperazione.Visible Then
          SetControls(2)
        End If
      End If
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub CmdAvantiFine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdAvantiFine.Click
    Dim strTmp As String = ""
    Try
      If pnAzienda.Visible Then
        If Trim(edCodaz.Text) = "" Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128741762141406250, "Codice azienda obbligatorio"))
          Return
        End If
        If Trim(edDesaz.Text) = "" Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128741762141406251, "Descrizione azienda obbligatoria"))
          Return
        End If
        If Trim(edExt.Text) = "" Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128741762141406252, "Codice ditta obbligatorio"))
          Return
        End If
        SetControls(2)
        edServerIstanza.Focus()
      Else
        If pnSql.Visible Then
          opAttach_CheckedChanged(opAttach, Nothing)
          If opAutenticazioneWindows.Checked Then
            dsWiza.Tables(strTabella).Rows(dcWiza.Position)!az_adoconnect = "Server=" & edServerIstanza.Text & _
                                                                    ";Database=" & edCodaz.Text & _
                                                                    ";Trusted_Connection=Yes;LANGUAGE=us_english;APP=Business"
          Else
            dsWiza.Tables(strTabella).Rows(dcWiza.Position)!az_adoconnect = "Server=" & edServerIstanza.Text & _
                                                                    ";Database=" & edCodaz.Text & _
                                                                    ";UID=" & edLogin.Text & _
                                                                    ";pwd=" & edPassword.Text & _
                                                                    ";LANGUAGE=us_english;APP=Business"
          End If
          dsWiza.Tables(strTabella).Rows(dcWiza.Position)!azconnect = "ODBC;Driver={SQL Server};" & dsWiza.Tables(strTabella).Rows(dcWiza.Position)!az_adoconnect

          'verifico la connessione e ottengo la dir predefinita per i database
          Try
            oCleDupa.GetDefaultDataDir(dsWiza.Tables(strTabella).Rows(dcWiza.Position)!az_adoconnect.ToString, strDefaultDir)
            If opNuovo.Checked Then
              edMdf.Text = strDefaultDir
              edLdf.Text = strDefaultDir
            End If

          Catch ex As Exception
            oApp.MsgBoxErr(oApp.Tr(Me, 129793220830838897, "Impossibile stabilire la connessione con il server SQL"))
            Return
          End Try
          SetControls(3)
        Else

          If opAttach.Checked Then
            If Trim(edMdf.Text) = "" Then
              oApp.MsgBoxInfo(oApp.Tr(Me, 129778499085766987, "file .MDF obbligatorio"))
              Return
            Else
              If Not oCleDupa.CheckSQLServerDir(dsWiza.Tables(strTabella).Rows(dcWiza.Position)!az_adoconnect.ToString, "", edMdf.Text) Then
                oApp.MsgBoxInfo(oApp.Tr(Me, 129778499366671482, "file .MDF Inesistente"))
                Return
              End If
            End If
            If Trim(edLdf.Text) <> "" Then
              If Not oCleDupa.CheckSQLServerDir(dsWiza.Tables(strTabella).Rows(dcWiza.Position)!az_adoconnect.ToString, "", edLdf.Text) Then
                oApp.MsgBoxInfo(oApp.Tr(Me, 129778499591045878, "file .LDF Inesistente"))
                Return
              End If
            End If
          End If

          oCallParams.strNomProg = edServerIstanza.Text
          oCallParams.strPar1 = edCodaz.Text
          oCallParams.strPar2 = edLogin.Text
          oCallParams.strPar3 = edPassword.Text
          oCallParams.strPar4 = edMdf.Text
          oCallParams.strPar5 = edLdf.Text
          oCallParams.bPar1 = opAutenticazioneWindows.Checked
          oCallParams.strParam = " "
          If opNuovo.Checked Then

            If Trim(edMdf.Text) = "" Then
              oApp.MsgBoxInfo(oApp.Tr(Me, 129793213866674654, "Directory per file .MDF obbligatorio"))
              Return
            Else
              If Not oCleDupa.CheckSQLServerDir(dsWiza.Tables(strTabella).Rows(dcWiza.Position)!az_adoconnect.ToString, edMdf.Text, "") Then
                oApp.MsgBoxInfo(oApp.Tr(Me, 129793213855738633, "Directory per file .MDF Inesistente sul server SQL"))
                Return
              End If
            End If
            If Trim(edLdf.Text) = "" Then
              oApp.MsgBoxInfo(oApp.Tr(Me, 129793213842630608, "Directory per file .LDF obbligatorio"))
              Return
            Else
              If Not oCleDupa.CheckSQLServerDir(dsWiza.Tables(strTabella).Rows(dcWiza.Position)!az_adoconnect.ToString, edLdf.Text, "") Then
                oApp.MsgBoxInfo(oApp.Tr(Me, 129793213830014573, "Directory per file .LDF Inesistente sul server SQL"))
                Return
              End If
            End If
            oCallParams.strParam = "N"
          End If
          If opCollega.Checked Then oCallParams.strParam = "C"
          If opAttach.Checked Then oCallParams.strParam = "A"

          Me.Close()
        End If
      End If
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub SetControls(ByVal nStato As Integer)
    Try
      Select Case nStato
        Case 1
          lbPasso.Text = oApp.Tr(Me, 128741762143336250, "Impostare i dati relativi alla nuova azienda")
          pnAzienda.Visible = True
          pnSql.Visible = False
          pnOperazione.Visible = False
          CmdAvantiFine.Text = oApp.Tr(Me, 128741762143336251, "Avanti")
          cmdIndietro.Visible = False
        Case 2
          lbPasso.Text = oApp.Tr(Me, 128741762143336252, "Impostare i parametri di connessione")
          pnAzienda.Visible = False
          pnSql.Visible = True
          pnOperazione.Visible = False
          CmdAvantiFine.Text = oApp.Tr(Me, 128741762143336253, "Avanti")
          cmdIndietro.Visible = True
          edPassword.Focus()
        Case 3
          lbPasso.Text = oApp.Tr(Me, 128741762143336254, "Selezionare l'operazione da eseguire")
          pnAzienda.Visible = False
          pnSql.Visible = False
          pnOperazione.Visible = True
          CmdAvantiFine.Text = oApp.Tr(Me, 128741762143336255, "Fine")
          cmdIndietro.Visible = True
      End Select
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub opAutenticazioneIDPassword_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opAutenticazioneIDPassword.CheckedChanged
    Try
      If opAutenticazioneIDPassword.Checked Then
        edLogin.Enabled = True
        edPassword.Enabled = True
      Else
        edLogin.Enabled = False
        edPassword.Enabled = False
      End If
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub opAttach_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opAttach.CheckedChanged
    Try
      edMdf.Visible = Not opCollega.Checked
      edLdf.Visible = Not opCollega.Checked
      lbMdf.Visible = Not opCollega.Checked
      lbLdf.Visible = Not opCollega.Checked
      edMdf.Text = strDefaultDir
      edLdf.Text = strDefaultDir
      lbDirnota.Visible = edMdf.Visible

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opNuovo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opNuovo.CheckedChanged
    Try
      edMdf.Visible = Not opCollega.Checked
      edLdf.Visible = Not opCollega.Checked
      lbMdf.Visible = Not opCollega.Checked
      lbLdf.Visible = Not opCollega.Checked
      ckUnicode.Enabled = opNuovo.Checked
      edMdf.Text = strDefaultDir
      edLdf.Text = strDefaultDir
      lbDirnota.Visible = edMdf.Visible

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub FRM__WIZA_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


  Public Overridable Sub Zoom()
    Dim ctlLastControl As Control
    Dim ctrlTmp As Control = Nothing
    Dim oParam As New CLE__PATB

    Try
      'entro qui perchè nella FRM__HLAN_KeyDown ho inserito il seguente codice:
      'If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
      '  Zoom()
      '  e.Handled = True
      'End If

      ctlLastControl = NTSFindControlFocused(Me)
      If ctlLastControl Is Nothing Then Return

      If edMdf.Focused Or edLdf.Focused Then
        If opNuovo.Checked Then
          'chiedo dir
          Dim oDlg As New NTSFolderBrowserDialog
          oDlg.ShowNewFolderButton = True
          oDlg.SelectedPath = "C:\"
          If edMdf.Focused And edMdf.Text.Trim <> "" Then oDlg.SelectedPath = edMdf.Text
          If edLdf.Focused And edLdf.Text.Trim <> "" Then oDlg.SelectedPath = edLdf.Text
          oDlg.Description = "Directory per nuovo database"
          oDlg.oMenu = oMenu
          If oDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            If edMdf.Focused Then
              edMdf.Text = oDlg.SelectedPath
              If edLdf.Text.Trim = "" Then edLdf.Text = edMdf.Text
            Else
              edLdf.Text = oDlg.SelectedPath
            End If
          End If
        Else
          '----------------------------------------------
          'zoom specifico per browse file mdf/ldf
          Dim oDlg As New NTSOpenFileDialog
          oDlg.Multiselect = False
          oDlg.InitialDirectory = "C:\"
          If edMdf.Focused And edMdf.Text.Trim <> "" Then oDlg.InitialDirectory = edMdf.Text
          If edLdf.Focused And edLdf.Text.Trim <> "" Then oDlg.InitialDirectory = edLdf.Text
          oDlg.CheckFileExists = True
          'oDlg.ShowReadOnly = True
          oDlg.ShowHelp = False
          oDlg.ValidateNames = False
          If edMdf.Focused Then
            oDlg.Filter = "File MDF (*.MDF)|*.mdf"
            oDlg.DefaultExt = "*.MDF"
          Else
            Try
              oDlg.InitialDirectory = System.IO.Path.GetDirectoryName(edMdf.Text)
            Catch
            End Try
            oDlg.Filter = "File LDF (*.LDF)|*.ldf"
            oDlg.DefaultExt = "*.LDF"
          End If
          oDlg.oMenu = oMenu
          If oDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            If edMdf.Focused Then
              edMdf.Text = oDlg.FileName
            Else
              edLdf.Text = oDlg.FileName
            End If
          End If
        End If    'If opNuovo.Checked Then
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

End Class

