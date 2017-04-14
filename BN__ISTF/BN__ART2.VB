Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__ART2
  Public oCleIstf As CLE__ISTF
  Public oCallParams As CLE__CLDP

  Public bAnnullato As Boolean = True

  Private components As System.ComponentModel.IContainer

  Public nTipoStampa As Integer = 0
  Public nScortaminima As Integer = 0
  Public nEsistenza As Integer = 0
  Public nUltimavendita As Integer = 0
  Public strData As String = ""
  Public nCodvalu As Integer = 0
  Public nMagazini As Integer = 0
  Public nMagazfin As Integer = 0
  Public nTipolistino As Integer = 0
  Public nListino As Integer = 0
  Public lContoini As Integer = 0
  Public lContofin As Integer = 0
  Public bCodtpro As Integer = 0
  Public nCodtpro As Integer = 0
  Public bPrimoLiv As Integer = 0

#Region "Inizializzazione"
  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    oMenu = Menu
    oApp = oMenu.App
    oCallParams = Param
    If Ditta <> "" Then DittaCorrente = Ditta Else DittaCorrente = oApp.Ditta
    Me.GctlTipoDoc = ""

    InitializeComponent()
    Me.MinimumSize = Me.Size

    Return True
  End Function

  Private Sub InitializeComponent()
    Me.pnButtons = New NTSInformatica.NTSPanel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.pnControlliTop = New NTSInformatica.NTSPanel
    Me.edMagazfin = New NTSInformatica.NTSTextBoxNum
    Me.lbMagazfin = New NTSInformatica.NTSLabel
    Me.edMagazini = New NTSInformatica.NTSTextBoxNum
    Me.lbMagazini = New NTSInformatica.NTSLabel
    Me.edData = New NTSInformatica.NTSTextBoxData
    Me.lbData = New NTSInformatica.NTSLabel
    Me.cbData = New NTSInformatica.NTSComboBox
    Me.lbTestData = New NTSInformatica.NTSLabel
    Me.edCodvalu = New NTSInformatica.NTSTextBoxNum
    Me.cbEsistenza = New NTSInformatica.NTSComboBox
    Me.lbEsistenza = New NTSInformatica.NTSLabel
    Me.lbDescodvalu = New NTSInformatica.NTSLabel
    Me.cbScorta = New NTSInformatica.NTSComboBox
    Me.lbScorta = New NTSInformatica.NTSLabel
    Me.lbCodvalu = New NTSInformatica.NTSLabel
    Me.pnControlliButtom = New NTSInformatica.NTSPanel
    Me.fmPrimoliv = New NTSInformatica.NTSGroupBox
    Me.ckPrimoliv = New NTSInformatica.NTSCheckBox
    Me.fmPromozione = New NTSInformatica.NTSGroupBox
    Me.edCodtpro = New NTSInformatica.NTSTextBoxNum
    Me.lbDescodtpro = New NTSInformatica.NTSLabel
    Me.lbCodtpro = New NTSInformatica.NTSLabel
    Me.ckPromozione = New NTSInformatica.NTSCheckBox
    Me.fmListino = New NTSInformatica.NTSGroupBox
    Me.cbTipolistini = New NTSInformatica.NTSComboBox
    Me.edContofin = New NTSInformatica.NTSTextBoxNum
    Me.lbContofin = New NTSInformatica.NTSLabel
    Me.edContoini = New NTSInformatica.NTSTextBoxNum
    Me.lbContoini = New NTSInformatica.NTSLabel
    Me.edListino = New NTSInformatica.NTSTextBoxNum
    Me.lbListino = New NTSInformatica.NTSLabel
    CType(Me.pnButtons, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnButtons.SuspendLayout()
    CType(Me.pnControlliTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnControlliTop.SuspendLayout()
    CType(Me.edMagazfin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edMagazini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edData.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbData.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodvalu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbEsistenza.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbScorta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnControlliButtom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnControlliButtom.SuspendLayout()
    CType(Me.fmPrimoliv, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmPrimoliv.SuspendLayout()
    CType(Me.ckPrimoliv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmPromozione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmPromozione.SuspendLayout()
    CType(Me.edCodtpro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckPromozione.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmListino, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmListino.SuspendLayout()
    CType(Me.cbTipolistini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edContofin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edContoini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edListino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'pnButtons
    '
    Me.pnButtons.AllowDrop = True
    Me.pnButtons.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnButtons.Appearance.Options.UseBackColor = True
    Me.pnButtons.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnButtons.Controls.Add(Me.cmdAnnulla)
    Me.pnButtons.Controls.Add(Me.cmdOk)
    Me.pnButtons.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnButtons.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnButtons.Location = New System.Drawing.Point(695, 0)
    Me.pnButtons.Name = "pnButtons"
    Me.pnButtons.Size = New System.Drawing.Size(140, 160)
    Me.pnButtons.TabIndex = 4
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(3, 48)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(131, 24)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(3, 23)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(131, 24)
    Me.cmdOk.TabIndex = 0
    Me.cmdOk.Text = "&Conferma"
    '
    'pnControlliTop
    '
    Me.pnControlliTop.AllowDrop = True
    Me.pnControlliTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnControlliTop.Appearance.Options.UseBackColor = True
    Me.pnControlliTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnControlliTop.Controls.Add(Me.edMagazfin)
    Me.pnControlliTop.Controls.Add(Me.lbMagazfin)
    Me.pnControlliTop.Controls.Add(Me.edMagazini)
    Me.pnControlliTop.Controls.Add(Me.lbMagazini)
    Me.pnControlliTop.Controls.Add(Me.edData)
    Me.pnControlliTop.Controls.Add(Me.lbData)
    Me.pnControlliTop.Controls.Add(Me.cbData)
    Me.pnControlliTop.Controls.Add(Me.lbTestData)
    Me.pnControlliTop.Controls.Add(Me.edCodvalu)
    Me.pnControlliTop.Controls.Add(Me.cbEsistenza)
    Me.pnControlliTop.Controls.Add(Me.lbEsistenza)
    Me.pnControlliTop.Controls.Add(Me.lbDescodvalu)
    Me.pnControlliTop.Controls.Add(Me.cbScorta)
    Me.pnControlliTop.Controls.Add(Me.lbScorta)
    Me.pnControlliTop.Controls.Add(Me.lbCodvalu)
    Me.pnControlliTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnControlliTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnControlliTop.Location = New System.Drawing.Point(0, 0)
    Me.pnControlliTop.Name = "pnControlliTop"
    Me.pnControlliTop.Size = New System.Drawing.Size(695, 162)
    Me.pnControlliTop.TabIndex = 5
    '
    'edMagazfin
    '
    Me.edMagazfin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMagazfin.EditValue = "0"
    Me.edMagazfin.Location = New System.Drawing.Point(176, 133)
    Me.edMagazfin.Name = "edMagazfin"
    Me.edMagazfin.NTSDbField = ""
    Me.edMagazfin.NTSFormat = "0"
    Me.edMagazfin.NTSForzaVisZoom = False
    Me.edMagazfin.NTSOldValue = ""
    Me.edMagazfin.Properties.Appearance.Options.UseTextOptions = True
    Me.edMagazfin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMagazfin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMagazfin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMagazfin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMagazfin.Properties.MaxLength = 65536
    Me.edMagazfin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMagazfin.Size = New System.Drawing.Size(51, 20)
    Me.edMagazfin.TabIndex = 173
    '
    'lbMagazfin
    '
    Me.lbMagazfin.AutoSize = True
    Me.lbMagazfin.BackColor = System.Drawing.Color.Transparent
    Me.lbMagazfin.Location = New System.Drawing.Point(24, 136)
    Me.lbMagazfin.Name = "lbMagazfin"
    Me.lbMagazfin.NTSDbField = ""
    Me.lbMagazfin.Size = New System.Drawing.Size(67, 13)
    Me.lbMagazfin.TabIndex = 172
    Me.lbMagazfin.Text = "A magazzino"
    Me.lbMagazfin.Tooltip = ""
    Me.lbMagazfin.UseMnemonic = False
    '
    'edMagazini
    '
    Me.edMagazini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMagazini.EditValue = "0"
    Me.edMagazini.Location = New System.Drawing.Point(176, 111)
    Me.edMagazini.Name = "edMagazini"
    Me.edMagazini.NTSDbField = ""
    Me.edMagazini.NTSFormat = "0"
    Me.edMagazini.NTSForzaVisZoom = False
    Me.edMagazini.NTSOldValue = ""
    Me.edMagazini.Properties.Appearance.Options.UseTextOptions = True
    Me.edMagazini.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMagazini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMagazini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMagazini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMagazini.Properties.MaxLength = 65536
    Me.edMagazini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMagazini.Size = New System.Drawing.Size(51, 20)
    Me.edMagazini.TabIndex = 171
    '
    'lbMagazini
    '
    Me.lbMagazini.AutoSize = True
    Me.lbMagazini.BackColor = System.Drawing.Color.Transparent
    Me.lbMagazini.Location = New System.Drawing.Point(24, 114)
    Me.lbMagazini.Name = "lbMagazini"
    Me.lbMagazini.NTSDbField = ""
    Me.lbMagazini.Size = New System.Drawing.Size(73, 13)
    Me.lbMagazini.TabIndex = 170
    Me.lbMagazini.Text = "Da magazzino"
    Me.lbMagazini.Tooltip = ""
    Me.lbMagazini.UseMnemonic = False
    '
    'edData
    '
    Me.edData.Cursor = System.Windows.Forms.Cursors.Default
    Me.edData.EditValue = "01/01/1900"
    Me.edData.Location = New System.Drawing.Point(525, 67)
    Me.edData.Name = "edData"
    Me.edData.NTSDbField = ""
    Me.edData.NTSForzaVisZoom = False
    Me.edData.NTSOldValue = ""
    Me.edData.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edData.Properties.Appearance.Options.UseBackColor = True
    Me.edData.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edData.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edData.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edData.Properties.MaxLength = 65536
    Me.edData.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edData.Size = New System.Drawing.Size(97, 20)
    Me.edData.TabIndex = 169
    '
    'lbData
    '
    Me.lbData.AutoSize = True
    Me.lbData.BackColor = System.Drawing.Color.Transparent
    Me.lbData.Location = New System.Drawing.Point(434, 70)
    Me.lbData.Name = "lbData"
    Me.lbData.NTSDbField = ""
    Me.lbData.Size = New System.Drawing.Size(85, 13)
    Me.lbData.TabIndex = 168
    Me.lbData.Text = "Data riferimento"
    Me.lbData.Tooltip = ""
    Me.lbData.UseMnemonic = False
    '
    'cbData
    '
    Me.cbData.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbData.DataSource = Nothing
    Me.cbData.DisplayMember = ""
    Me.cbData.Location = New System.Drawing.Point(176, 67)
    Me.cbData.Name = "cbData"
    Me.cbData.NTSDbField = ""
    Me.cbData.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbData.Properties.DropDownRows = 30
    Me.cbData.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbData.SelectedValue = ""
    Me.cbData.Size = New System.Drawing.Size(217, 20)
    Me.cbData.TabIndex = 167
    Me.cbData.ValueMember = ""
    '
    'lbTestData
    '
    Me.lbTestData.AutoSize = True
    Me.lbTestData.BackColor = System.Drawing.Color.Transparent
    Me.lbTestData.Location = New System.Drawing.Point(24, 70)
    Me.lbTestData.Name = "lbTestData"
    Me.lbTestData.NTSDbField = ""
    Me.lbTestData.Size = New System.Drawing.Size(137, 13)
    Me.lbTestData.TabIndex = 166
    Me.lbTestData.Text = "Test su data ultima vendita"
    Me.lbTestData.Tooltip = ""
    Me.lbTestData.UseMnemonic = False
    '
    'edCodvalu
    '
    Me.edCodvalu.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodvalu.EditValue = "0"
    Me.edCodvalu.Location = New System.Drawing.Point(176, 89)
    Me.edCodvalu.Name = "edCodvalu"
    Me.edCodvalu.NTSDbField = ""
    Me.edCodvalu.NTSFormat = "0"
    Me.edCodvalu.NTSForzaVisZoom = False
    Me.edCodvalu.NTSOldValue = ""
    Me.edCodvalu.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodvalu.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodvalu.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodvalu.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodvalu.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodvalu.Properties.MaxLength = 65536
    Me.edCodvalu.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodvalu.Size = New System.Drawing.Size(51, 20)
    Me.edCodvalu.TabIndex = 165
    '
    'cbEsistenza
    '
    Me.cbEsistenza.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbEsistenza.DataSource = Nothing
    Me.cbEsistenza.DisplayMember = ""
    Me.cbEsistenza.Location = New System.Drawing.Point(176, 45)
    Me.cbEsistenza.Name = "cbEsistenza"
    Me.cbEsistenza.NTSDbField = ""
    Me.cbEsistenza.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbEsistenza.Properties.DropDownRows = 30
    Me.cbEsistenza.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbEsistenza.SelectedValue = ""
    Me.cbEsistenza.Size = New System.Drawing.Size(217, 20)
    Me.cbEsistenza.TabIndex = 164
    Me.cbEsistenza.ValueMember = ""
    '
    'lbEsistenza
    '
    Me.lbEsistenza.AutoSize = True
    Me.lbEsistenza.BackColor = System.Drawing.Color.Transparent
    Me.lbEsistenza.Location = New System.Drawing.Point(24, 48)
    Me.lbEsistenza.Name = "lbEsistenza"
    Me.lbEsistenza.NTSDbField = ""
    Me.lbEsistenza.Size = New System.Drawing.Size(90, 13)
    Me.lbEsistenza.TabIndex = 163
    Me.lbEsistenza.Text = "Test su esistenza"
    Me.lbEsistenza.Tooltip = ""
    Me.lbEsistenza.UseMnemonic = False
    '
    'lbDescodvalu
    '
    Me.lbDescodvalu.BackColor = System.Drawing.Color.Transparent
    Me.lbDescodvalu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescodvalu.Location = New System.Drawing.Point(233, 88)
    Me.lbDescodvalu.Name = "lbDescodvalu"
    Me.lbDescodvalu.NTSDbField = ""
    Me.lbDescodvalu.Size = New System.Drawing.Size(286, 21)
    Me.lbDescodvalu.TabIndex = 162
    Me.lbDescodvalu.Tooltip = ""
    Me.lbDescodvalu.UseMnemonic = False
    '
    'cbScorta
    '
    Me.cbScorta.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbScorta.DataSource = Nothing
    Me.cbScorta.DisplayMember = ""
    Me.cbScorta.Location = New System.Drawing.Point(176, 23)
    Me.cbScorta.Name = "cbScorta"
    Me.cbScorta.NTSDbField = ""
    Me.cbScorta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbScorta.Properties.DropDownRows = 30
    Me.cbScorta.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbScorta.SelectedValue = ""
    Me.cbScorta.Size = New System.Drawing.Size(217, 20)
    Me.cbScorta.TabIndex = 161
    Me.cbScorta.ValueMember = ""
    '
    'lbScorta
    '
    Me.lbScorta.AutoSize = True
    Me.lbScorta.BackColor = System.Drawing.Color.Transparent
    Me.lbScorta.Location = New System.Drawing.Point(24, 26)
    Me.lbScorta.Name = "lbScorta"
    Me.lbScorta.NTSDbField = ""
    Me.lbScorta.Size = New System.Drawing.Size(110, 13)
    Me.lbScorta.TabIndex = 160
    Me.lbScorta.Text = "Test su scorta minima"
    Me.lbScorta.Tooltip = ""
    Me.lbScorta.UseMnemonic = False
    '
    'lbCodvalu
    '
    Me.lbCodvalu.AutoSize = True
    Me.lbCodvalu.BackColor = System.Drawing.Color.Transparent
    Me.lbCodvalu.Location = New System.Drawing.Point(24, 92)
    Me.lbCodvalu.Name = "lbCodvalu"
    Me.lbCodvalu.NTSDbField = ""
    Me.lbCodvalu.Size = New System.Drawing.Size(72, 13)
    Me.lbCodvalu.TabIndex = 158
    Me.lbCodvalu.Text = "Codice valuta"
    Me.lbCodvalu.Tooltip = ""
    Me.lbCodvalu.UseMnemonic = False
    '
    'pnControlliButtom
    '
    Me.pnControlliButtom.AllowDrop = True
    Me.pnControlliButtom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnControlliButtom.Appearance.Options.UseBackColor = True
    Me.pnControlliButtom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnControlliButtom.Controls.Add(Me.fmPrimoliv)
    Me.pnControlliButtom.Controls.Add(Me.fmPromozione)
    Me.pnControlliButtom.Controls.Add(Me.fmListino)
    Me.pnControlliButtom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnControlliButtom.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnControlliButtom.Location = New System.Drawing.Point(0, 162)
    Me.pnControlliButtom.Name = "pnControlliButtom"
    Me.pnControlliButtom.Size = New System.Drawing.Size(695, 0)
    Me.pnControlliButtom.TabIndex = 6
    '
    'fmPrimoliv
    '
    Me.fmPrimoliv.AllowDrop = True
    Me.fmPrimoliv.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmPrimoliv.Appearance.Options.UseBackColor = True
    Me.fmPrimoliv.Controls.Add(Me.ckPrimoliv)
    Me.fmPrimoliv.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmPrimoliv.Location = New System.Drawing.Point(258, 70)
    Me.fmPrimoliv.Name = "fmPrimoliv"
    Me.fmPrimoliv.ShowCaption = False
    Me.fmPrimoliv.Size = New System.Drawing.Size(418, 40)
    Me.fmPrimoliv.TabIndex = 3
    '
    'ckPrimoliv
    '
    Me.ckPrimoliv.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckPrimoliv.Location = New System.Drawing.Point(35, 9)
    Me.ckPrimoliv.Name = "ckPrimoliv"
    Me.ckPrimoliv.NTSCheckValue = "S"
    Me.ckPrimoliv.NTSUnCheckValue = "N"
    Me.ckPrimoliv.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPrimoliv.Properties.Appearance.Options.UseBackColor = True
    Me.ckPrimoliv.Properties.Caption = "Articoli a varianti con prezzi diversi al primo livello"
    Me.ckPrimoliv.Size = New System.Drawing.Size(274, 19)
    Me.ckPrimoliv.TabIndex = 6
    '
    'fmPromozione
    '
    Me.fmPromozione.AllowDrop = True
    Me.fmPromozione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmPromozione.Appearance.Options.UseBackColor = True
    Me.fmPromozione.Controls.Add(Me.edCodtpro)
    Me.fmPromozione.Controls.Add(Me.lbDescodtpro)
    Me.fmPromozione.Controls.Add(Me.lbCodtpro)
    Me.fmPromozione.Controls.Add(Me.ckPromozione)
    Me.fmPromozione.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmPromozione.Location = New System.Drawing.Point(258, 6)
    Me.fmPromozione.Name = "fmPromozione"
    Me.fmPromozione.Size = New System.Drawing.Size(418, 58)
    Me.fmPromozione.TabIndex = 2
    '
    'edCodtpro
    '
    Me.edCodtpro.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodtpro.EditValue = "0"
    Me.edCodtpro.Location = New System.Drawing.Point(117, 27)
    Me.edCodtpro.Name = "edCodtpro"
    Me.edCodtpro.NTSDbField = ""
    Me.edCodtpro.NTSFormat = "0"
    Me.edCodtpro.NTSForzaVisZoom = False
    Me.edCodtpro.NTSOldValue = ""
    Me.edCodtpro.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodtpro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodtpro.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodtpro.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodtpro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodtpro.Properties.MaxLength = 65536
    Me.edCodtpro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodtpro.Size = New System.Drawing.Size(51, 20)
    Me.edCodtpro.TabIndex = 168
    '
    'lbDescodtpro
    '
    Me.lbDescodtpro.BackColor = System.Drawing.Color.Transparent
    Me.lbDescodtpro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescodtpro.Location = New System.Drawing.Point(174, 26)
    Me.lbDescodtpro.Name = "lbDescodtpro"
    Me.lbDescodtpro.NTSDbField = ""
    Me.lbDescodtpro.Size = New System.Drawing.Size(223, 21)
    Me.lbDescodtpro.TabIndex = 167
    Me.lbDescodtpro.Tooltip = ""
    Me.lbDescodtpro.UseMnemonic = False
    '
    'lbCodtpro
    '
    Me.lbCodtpro.AutoSize = True
    Me.lbCodtpro.BackColor = System.Drawing.Color.Transparent
    Me.lbCodtpro.Location = New System.Drawing.Point(14, 30)
    Me.lbCodtpro.Name = "lbCodtpro"
    Me.lbCodtpro.NTSDbField = ""
    Me.lbCodtpro.Size = New System.Drawing.Size(97, 13)
    Me.lbCodtpro.TabIndex = 166
    Me.lbCodtpro.Text = "Codice promozione"
    Me.lbCodtpro.Tooltip = ""
    Me.lbCodtpro.UseMnemonic = False
    '
    'ckPromozione
    '
    Me.ckPromozione.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckPromozione.Location = New System.Drawing.Point(8, 0)
    Me.ckPromozione.Name = "ckPromozione"
    Me.ckPromozione.NTSCheckValue = "S"
    Me.ckPromozione.NTSUnCheckValue = "N"
    Me.ckPromozione.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPromozione.Properties.Appearance.Options.UseBackColor = True
    Me.ckPromozione.Properties.Caption = "Prezzi in promozione"
    Me.ckPromozione.Size = New System.Drawing.Size(127, 19)
    Me.ckPromozione.TabIndex = 5
    '
    'fmListino
    '
    Me.fmListino.AllowDrop = True
    Me.fmListino.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmListino.Appearance.Options.UseBackColor = True
    Me.fmListino.Controls.Add(Me.cbTipolistini)
    Me.fmListino.Controls.Add(Me.edContofin)
    Me.fmListino.Controls.Add(Me.lbContofin)
    Me.fmListino.Controls.Add(Me.edContoini)
    Me.fmListino.Controls.Add(Me.lbContoini)
    Me.fmListino.Controls.Add(Me.edListino)
    Me.fmListino.Controls.Add(Me.lbListino)
    Me.fmListino.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmListino.Location = New System.Drawing.Point(12, 6)
    Me.fmListino.Name = "fmListino"
    Me.fmListino.Size = New System.Drawing.Size(240, 104)
    Me.fmListino.TabIndex = 1
    '
    'cbTipolistini
    '
    Me.cbTipolistini.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipolistini.DataSource = Nothing
    Me.cbTipolistini.DisplayMember = ""
    Me.cbTipolistini.Location = New System.Drawing.Point(15, 0)
    Me.cbTipolistini.Name = "cbTipolistini"
    Me.cbTipolistini.NTSDbField = ""
    Me.cbTipolistini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipolistini.Properties.DropDownRows = 30
    Me.cbTipolistini.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipolistini.SelectedValue = ""
    Me.cbTipolistini.Size = New System.Drawing.Size(155, 20)
    Me.cbTipolistini.TabIndex = 163
    Me.cbTipolistini.ValueMember = ""
    '
    'edContofin
    '
    Me.edContofin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edContofin.EditValue = "0"
    Me.edContofin.Location = New System.Drawing.Point(119, 75)
    Me.edContofin.Name = "edContofin"
    Me.edContofin.NTSDbField = ""
    Me.edContofin.NTSFormat = "0"
    Me.edContofin.NTSForzaVisZoom = False
    Me.edContofin.NTSOldValue = ""
    Me.edContofin.Properties.Appearance.Options.UseTextOptions = True
    Me.edContofin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edContofin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edContofin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edContofin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edContofin.Properties.MaxLength = 65536
    Me.edContofin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edContofin.Size = New System.Drawing.Size(96, 20)
    Me.edContofin.TabIndex = 171
    '
    'lbContofin
    '
    Me.lbContofin.AutoSize = True
    Me.lbContofin.BackColor = System.Drawing.Color.Transparent
    Me.lbContofin.Location = New System.Drawing.Point(12, 78)
    Me.lbContofin.Name = "lbContofin"
    Me.lbContofin.NTSDbField = ""
    Me.lbContofin.Size = New System.Drawing.Size(58, 13)
    Me.lbContofin.TabIndex = 170
    Me.lbContofin.Text = "a fornitore"
    Me.lbContofin.Tooltip = ""
    Me.lbContofin.UseMnemonic = False
    '
    'edContoini
    '
    Me.edContoini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edContoini.EditValue = "0"
    Me.edContoini.Location = New System.Drawing.Point(119, 53)
    Me.edContoini.Name = "edContoini"
    Me.edContoini.NTSDbField = ""
    Me.edContoini.NTSFormat = "0"
    Me.edContoini.NTSForzaVisZoom = False
    Me.edContoini.NTSOldValue = ""
    Me.edContoini.Properties.Appearance.Options.UseTextOptions = True
    Me.edContoini.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edContoini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edContoini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edContoini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edContoini.Properties.MaxLength = 65536
    Me.edContoini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edContoini.Size = New System.Drawing.Size(96, 20)
    Me.edContoini.TabIndex = 169
    '
    'lbContoini
    '
    Me.lbContoini.AutoSize = True
    Me.lbContoini.BackColor = System.Drawing.Color.Transparent
    Me.lbContoini.Location = New System.Drawing.Point(12, 56)
    Me.lbContoini.Name = "lbContoini"
    Me.lbContoini.NTSDbField = ""
    Me.lbContoini.Size = New System.Drawing.Size(65, 13)
    Me.lbContoini.TabIndex = 168
    Me.lbContoini.Text = "Da fornitore"
    Me.lbContoini.Tooltip = ""
    Me.lbContoini.UseMnemonic = False
    '
    'edListino
    '
    Me.edListino.Cursor = System.Windows.Forms.Cursors.Default
    Me.edListino.EditValue = "0"
    Me.edListino.Location = New System.Drawing.Point(119, 31)
    Me.edListino.Name = "edListino"
    Me.edListino.NTSDbField = ""
    Me.edListino.NTSFormat = "0"
    Me.edListino.NTSForzaVisZoom = False
    Me.edListino.NTSOldValue = ""
    Me.edListino.Properties.Appearance.Options.UseTextOptions = True
    Me.edListino.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edListino.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edListino.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edListino.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edListino.Properties.MaxLength = 65536
    Me.edListino.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edListino.Size = New System.Drawing.Size(51, 20)
    Me.edListino.TabIndex = 167
    '
    'lbListino
    '
    Me.lbListino.AutoSize = True
    Me.lbListino.BackColor = System.Drawing.Color.Transparent
    Me.lbListino.Location = New System.Drawing.Point(12, 34)
    Me.lbListino.Name = "lbListino"
    Me.lbListino.NTSDbField = ""
    Me.lbListino.Size = New System.Drawing.Size(74, 13)
    Me.lbListino.TabIndex = 166
    Me.lbListino.Text = "Numero listino"
    Me.lbListino.Tooltip = ""
    Me.lbListino.UseMnemonic = False
    '
    'FRM__ART2
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(835, 160)
    Me.Controls.Add(Me.pnControlliButtom)
    Me.Controls.Add(Me.pnControlliTop)
    Me.Controls.Add(Me.pnButtons)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__ART2"
    Me.Text = "FILTRI SU DATI CONTABILI"
    CType(Me.pnButtons, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnButtons.ResumeLayout(False)
    CType(Me.pnControlliTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnControlliTop.ResumeLayout(False)
    Me.pnControlliTop.PerformLayout()
    CType(Me.edMagazfin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edMagazini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edData.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbData.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodvalu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbEsistenza.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbScorta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnControlliButtom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnControlliButtom.ResumeLayout(False)
    CType(Me.fmPrimoliv, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmPrimoliv.ResumeLayout(False)
    Me.fmPrimoliv.PerformLayout()
    CType(Me.ckPrimoliv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmPromozione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmPromozione.ResumeLayout(False)
    Me.fmPromozione.PerformLayout()
    CType(Me.edCodtpro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckPromozione.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmListino, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmListino.ResumeLayout(False)
    Me.fmListino.PerformLayout()
    CType(Me.cbTipolistini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edContofin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edContoini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edListino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, False)
    '----------------------------------------------------------------------------------------------------------------
    Try
      '--------------------------------------------------------------------------------------------------------------
      cbScorta.NTSSetParam(oApp.Tr(Me, 129718765882869679, "Test su scorta minima"))
      cbEsistenza.NTSSetParam(oApp.Tr(Me, 129718766044223531, "Test su esistenza"))
      cbData.NTSSetParam(oApp.Tr(Me, 129718766238430207, "Test su data ultima vendita"))
      edData.NTSSetParam(oMenu, oApp.Tr(Me, 129718766666845095, "Data riferimento"), True)
      edCodvalu.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129718766915467923, "Codice valuta"), tabvalu)
      edMagazini.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129718767183505559, "Da magazzino"), tabmaga)
      edMagazfin.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129718767342622997, "A magazzino"), tabmaga)
      cbTipolistini.NTSSetParam(oApp.Tr(Me, 129718767494093657, "tipo listino"))
      edContoini.NTSSetParam(oMenu, oApp.Tr(Me, 129718767689315997, "Da fornitore"), "0", 9, 0, 999999999)
      edContofin.NTSSetParam(oMenu, oApp.Tr(Me, 129718768296233833, "a fornitore"), "0", 9, 0, 999999999)
      ckPromozione.NTSSetParam(oMenu, oApp.Tr(Me, 129718768614693327, "Prezzi in promozione"), "S", "N")
      edCodtpro.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129718768861802425, "Codice promozione"), tabtpro)
      ckPrimoliv.NTSSetParam(oMenu, oApp.Tr(Me, 129718769126705175, "Articolo a varianti con prezzi diversi al primo livello"), "S", "N")
      edListino.NTSForzaVisZoom = False
      '--------------------------------------------------------------------------------------------------------------
      edContoini.NTSForzaVisZoom = True
      edContofin.NTSForzaVisZoom = True
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

  Public Overridable Sub InitEntity(ByVal cleIstf As CLE__ISTF)
    oCleIstf = cleIstf
    AddHandler oCleIstf.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRM__ART2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      CaricaCombo()
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
      GctlApplicaDefaultValue()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRM__ART2_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      cbScorta.SelectedValue = nScortaminima.ToString
      cbEsistenza.SelectedValue = nEsistenza.ToString
      cbData.SelectedValue = nUltimavendita.ToString
      edData.Text = strData
      edCodvalu.Text = nCodvalu.ToString
      If NTSCInt(edCodvalu.Text) <> 0 Then
        If oMenu.ValCodiceDb(NTSCStr(edCodvalu.Text), DittaCorrente, "TABVALU", "N", strTmp) = True Then
          lbDescodvalu.Text = strTmp
        End If
      End If
      edMagazini.Text = nMagazini.ToString
      edMagazfin.Text = nMagazfin.ToString
      cbTipolistini.SelectedValue = nTipolistino.ToString
      edListino.Text = nListino.ToString
      edContoini.Text = lContoini.ToString
      edContofin.Text = lContofin.ToString
      ckPromozione.Checked = CBool(IIf(bCodtpro = 1, True, False))
      edCodtpro.Text = nCodtpro.ToString
      If ckPromozione.Checked = False Then
        edCodtpro.Text = "0"
        lbDescodtpro.Text = ""
        lbCodtpro.Enabled = False
        edCodtpro.Enabled = False
      End If
      If NTSCInt(edCodtpro.Text) <> 0 Then
        If oMenu.ValCodiceDb(NTSCStr(edCodtpro.Text), DittaCorrente, "TABTPRO", "N", strTmp) = True Then
          lbDescodtpro.Text = strTmp
        End If
      End If
      ckPrimoliv.Checked = CBool(IIf(bPrimoLiv = 1, True, False))
      '--------------------------------------------------------------------------------------------------------------
      Select Case nTipoStampa
        Case 12, 14
          lbTestData.Enabled = False
          cbData.Enabled = False
          lbData.Enabled = False
          edData.Enabled = False
      End Select
      '--------------------------------------------------------------------------------------------------------------
      '--- Determina Caption e dimensioni Form
      '--------------------------------------------------------------------------------------------------------------
      If nTipoStampa = 19 Then
        Me.Text = "Filtri su listini"
        Me.Height = 314
        lbScorta.Enabled = False
        cbScorta.Enabled = False
        lbEsistenza.Enabled = False
        cbEsistenza.Enabled = False
        lbTestData.Enabled = False
        cbData.Enabled = False
        lbData.Enabled = False
        edData.Enabled = False
        lbMagazini.Enabled = False
        lbMagazfin.Enabled = False
        edMagazini.Enabled = False
        edMagazfin.Enabled = False
      Else
        Me.Text = "Filtri su dati contabili"
        lbCodvalu.Enabled = False
        edCodvalu.Enabled = False
        cbTipolistini.Enabled = False
        edListino.Enabled = False
        edContoini.Enabled = False
        edContofin.Enabled = False
        ckPromozione.Enabled = False
        edCodtpro.Enabled = False
        ckPrimoliv.Enabled = False
      End If
      '-------------------------------------------------------------------------------------------------------------      
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi TextBox"
  Public Overridable Sub edCodvalu_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodvalu.Validated
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edCodvalu.Text) = 0 Then
        lbDescodvalu.Text = ""
      Else
        If oMenu.ValCodiceDb(NTSCStr(edCodvalu.Text), DittaCorrente, "TABVALU", "N", strTmp) = True Then
          lbDescodvalu.Text = strTmp
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edCodtpro_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodtpro.Validated
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edCodtpro.Text) = 0 Then
        lbDescodtpro.Text = ""
      Else
        If oMenu.ValCodiceDb(NTSCStr(edCodtpro.Text), DittaCorrente, "TABTPRO", "N", strTmp) = True Then
          lbDescodtpro.Text = strTmp
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi ComboBox"
  Public Overridable Sub cbData_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbData.SelectedIndexChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCInt(cbData.SelectedValue)
        Case 0
          lbData.Enabled = False
          edData.Text = ""
          edData.Enabled = False
        Case Else
          GctlSetVisEnab(lbData, False)
          GctlSetVisEnab(edData, False)
          edData.Text = IIf(strData.Trim <> "", strData, Now.ToShortDateString).ToString
          edData.Focus()
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cbTipolistini_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTipolistini.SelectedIndexChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCInt(cbTipolistini.SelectedValue)
        Case 0
          lbContoini.Text = "Da conto"
          lbContofin.Text = "a conto"
          edContoini.NTSSetParamZoom("")
          edContofin.NTSSetParamZoom("")
        Case 1
          lbContoini.Text = "Da cliente"
          lbContofin.Text = "a cliente"
          edContoini.NTSSetParamZoom("ZOOMANAGRAC")
          edContofin.NTSSetParamZoom("ZOOMANAGRAC")
        Case 2
          lbContoini.Text = "Da fornitore"
          lbContofin.Text = "a fornitore"
          edContoini.NTSSetParamZoom("ZOOMANAGRAF")
          edContofin.NTSSetParamZoom("ZOOMANAGRAF")
      End Select
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCInt(cbTipolistini.SelectedValue)
        Case 0
          lbContoini.Enabled = False
          lbContofin.Enabled = False
          edContoini.Text = "0"
          edContofin.Text = "".PadLeft(9, "9"c)
          edContoini.Enabled = False
          edContofin.Enabled = False
          GctlSetVisEnab(lbListino, False)
          GctlSetVisEnab(edListino, False)
          edListino.Text = "1"
          edListino.Focus()
        Case Else
          edListino.Text = "0"
          lbListino.Enabled = False
          edListino.Enabled = False
          GctlSetVisEnab(lbContoini, False)
          GctlSetVisEnab(lbContofin, False)
          GctlSetVisEnab(edContoini, False)
          GctlSetVisEnab(edContofin, False)
          edContoini.Focus()
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi CheckBox"
  Public Overridable Sub ckPromozione_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckPromozione.CheckedChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If ckPromozione.Checked = True Then
        GctlSetVisEnab(lbCodtpro, False)
        GctlSetVisEnab(edCodtpro, False)
        edCodtpro.Focus()
      Else
        lbCodtpro.Enabled = False
        edCodtpro.Text = "0"
        edCodtpro.Enabled = False
      End If
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
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      If CheckSelection() = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      nScortaminima = NTSCInt(cbScorta.SelectedValue)
      nEsistenza = NTSCInt(cbEsistenza.SelectedValue)
      nUltimavendita = NTSCInt(cbData.SelectedValue)
      strData = edData.Text
      nCodvalu = NTSCInt(edCodvalu.Text)
      nMagazini = NTSCInt(edMagazini.Text)
      nMagazfin = NTSCInt(edMagazfin.Text)
      nTipolistino = NTSCInt(cbTipolistini.SelectedValue)
      nListino = NTSCInt(edListino.Text)
      bCodtpro = NTSCInt(IIf(ckPromozione.Checked = True, 1, 0))
      nCodtpro = NTSCInt(edCodtpro.Text)
      lContoini = NTSCInt(edContoini.Text)
      lContofin = NTSCInt(edContofin.Text)
      bPrimoLiv = NTSCInt(IIf(ckPrimoliv.Checked = True, 1, 0))
      '--------------------------------------------------------------------------------------------------------------
      bAnnullato = False
      '--------------------------------------------------------------------------------------------------------------
      Me.Close()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      bAnnullato = True
      '--------------------------------------------------------------------------------------------------------------
      Me.Close()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Sub CaricaCombo()
    Dim dttScorta As New DataTable()
    Dim dttEsistenza As New DataTable()
    Dim dttData As New DataTable()
    Dim dttTipolistini As New DataTable()

    Try
      '--------------------------------------------------------------------------------------------------------------
      dttScorta.Columns.Add("cod", GetType(Integer))
      dttScorta.Columns.Add("val", GetType(String))
      dttScorta.Rows.Add(New Object() {"0", "(Nessuno)"})
      dttScorta.Rows.Add(New Object() {"1", "Esistenza"})
      dttScorta.Rows.Add(New Object() {"2", "Disponibilità"})
      dttScorta.Rows.Add(New Object() {"3", "Esistenza - Impegnato"})
      dttScorta.Rows.Add(New Object() {"4", "Disponibilità netta"})
      dttScorta.AcceptChanges()
      cbScorta.DataSource = dttScorta
      cbScorta.ValueMember = "cod"
      cbScorta.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
      dttEsistenza.Columns.Add("cod", GetType(Integer))
      dttEsistenza.Columns.Add("val", GetType(String))
      dttEsistenza.Rows.Add(New Object() {"0", "(Nessuno)"})
      dttEsistenza.Rows.Add(New Object() {"1", "Solo esistenza > 0"})
      dttEsistenza.Rows.Add(New Object() {"2", "Solo esistenza < 0"})
      dttEsistenza.Rows.Add(New Object() {"3", "Solo esistenza = 0"})
      dttEsistenza.Rows.Add(New Object() {"4", "Solo esistenza <> 0"})
      dttEsistenza.Rows.Add(New Object() {"5", "Solo esistenza >= 0"})
      dttEsistenza.Rows.Add(New Object() {"6", "Solo esistenza <= 0"})
      dttEsistenza.AcceptChanges()
      cbEsistenza.DataSource = dttEsistenza
      cbEsistenza.ValueMember = "cod"
      cbEsistenza.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
      dttData.Columns.Add("cod", GetType(Integer))
      dttData.Columns.Add("val", GetType(String))
      dttData.Rows.Add(New Object() {"0", "(Nessuno)"})
      dttData.Rows.Add(New Object() {"1", "Anteriore"})
      dttData.Rows.Add(New Object() {"2", "Posteriore"})
      dttData.AcceptChanges()
      cbData.DataSource = dttData
      cbData.ValueMember = "cod"
      cbData.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
      dttTipolistini.Columns.Add("cod", GetType(Integer))
      dttTipolistini.Columns.Add("val", GetType(String))
      dttTipolistini.Rows.Add(New Object() {"0", "Listini generici"})
      dttTipolistini.Rows.Add(New Object() {"1", "Listini clienti"})
      dttTipolistini.Rows.Add(New Object() {"2", "Listini fornitori"})
      dttTipolistini.AcceptChanges()
      cbTipolistini.DataSource = dttTipolistini
      cbTipolistini.ValueMember = "cod"
      cbTipolistini.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Function CheckIntervalli() As Boolean
    Dim strConto As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edMagazini.Text) > NTSCInt(edMagazfin.Text) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 129718786838241348, "Attenzione!" & vbCrLf & _
          "Il magazzino iniziale non può essere superiore a quello di finale."))
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      strConto = IIf(NTSCInt(cbTipolistini.SelectedValue) = 2, "cliente", "fornitore").ToString
      If NTSCInt(edContoini.Text) > NTSCInt(edContofin.Text) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 129718784652101973, "Attenzione!" & vbCrLf & _
          "Il " & strConto & " iniziale non può essere superiore a quello di finale."))
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function CheckSelection() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      If (cbData.Enabled = True) And (NTSCInt(cbData.SelectedValue) <> 0) Then
        If edData.Text.Trim = "" Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 129718899805246175, "Attenzione!" & vbCrLf & _
            "Indicare una data di riferimento valida."))
          edData.Text = Now.ToShortDateString()
          edData.Focus()
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (cbTipolistini.Enabled = True) And (NTSCInt(cbTipolistini.SelectedValue) = 0) Then
        If NTSCInt(edListino.Text) = 0 Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 129718881060490367, "Attenzione!" & vbCrLf & _
            "Indicare un numero di listino valido."))
          edListino.Focus()
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (ckPromozione.Enabled = True) And (ckPromozione.Checked = True) Then
        If NTSCInt(edCodtpro.Text) = 0 Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 129718882173973787, "Attenzione!" & vbCrLf & _
            "Indicare un codice promozione valido" & vbCrLf & _
            "altrimenti deselezionare la scelta."))
          edCodtpro.Focus()
          Return False
        End If
      End If
      If CheckIntervalli() = False Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

End Class