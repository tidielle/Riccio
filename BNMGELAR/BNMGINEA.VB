Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGINEA

#Region "Variabili"
  Public oCleElar As CLEMGELAR
  Public oCallParams As CLE__CLDP
  Private components As System.ComponentModel.IContainer

  Public FORMAT_CAMPO As String

  Public nIneaRichiesta As Integer
  Public strIneaCodart As String
  Public strIneaDescr As String

  Public WithEvents cmdFine As NTSInformatica.NTSButton
  Public WithEvents cmdSalta As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents lbXx_Conto As NTSInformatica.NTSLabel
  Public WithEvents edPrezzobase As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPrezzoattuale As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDaquant As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbUnmis As NTSInformatica.NTSLabel
  Public WithEvents edNuovovalore As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbNuovovalore As NTSInformatica.NTSLabel
  Public WithEvents lbDaquant As NTSInformatica.NTSLabel
  Public WithEvents lbPrezzoattuale As NTSInformatica.NTSLabel
  Public WithEvents lbPrezzobase As NTSInformatica.NTSLabel
  Public WithEvents lbAquant As NTSInformatica.NTSLabel
  Public WithEvents edAquant As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDatabase As NTSInformatica.NTSLabel
  Public WithEvents lbDataattuale As NTSInformatica.NTSLabel
  Public WithEvents frProgressivi As NTSInformatica.NTSGroupBox
  Public WithEvents edEsist As NTSInformatica.NTSTextBoxNum
  Public WithEvents cmdAltriMag As NTSInformatica.NTSButton
  Public WithEvents lbEsist As NTSInformatica.NTSLabel
  Public WithEvents lbDtulcar As NTSInformatica.NTSLabel
  Public WithEvents lbInesaur As NTSInformatica.NTSLabel
  Public WithEvents lbValuta As NTSInformatica.NTSLabel
  Public WithEvents ckNetto As NTSInformatica.NTSCheckBox
  Public WithEvents edConto As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_Codart As NTSInformatica.NTSLabel
  Public WithEvents lbCodart As NTSInformatica.NTSLabel
  Public WithEvents edFase As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_Fase As NTSInformatica.NTSLabel
  Public WithEvents lbFase As NTSInformatica.NTSLabel
  Public WithEvents edUnmis As NTSInformatica.NTSTextBoxStr
  Public WithEvents edCodart As NTSInformatica.NTSTextBoxStr
  Public WithEvents edDtulcar As NTSInformatica.NTSTextBoxData
  Public WithEvents edCoddest As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCoddest As NTSInformatica.NTSLabel
  Public WithEvents lbConto As NTSInformatica.NTSLabel

#End Region

  Private Sub InitializeComponent()
    Me.cmdFine = New NTSInformatica.NTSButton
    Me.cmdSalta = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.lbConto = New NTSInformatica.NTSLabel
    Me.lbXx_Conto = New NTSInformatica.NTSLabel
    Me.edPrezzobase = New NTSInformatica.NTSTextBoxNum
    Me.edPrezzoattuale = New NTSInformatica.NTSTextBoxNum
    Me.edDaquant = New NTSInformatica.NTSTextBoxNum
    Me.lbUnmis = New NTSInformatica.NTSLabel
    Me.edNuovovalore = New NTSInformatica.NTSTextBoxNum
    Me.lbNuovovalore = New NTSInformatica.NTSLabel
    Me.lbDaquant = New NTSInformatica.NTSLabel
    Me.lbPrezzoattuale = New NTSInformatica.NTSLabel
    Me.lbPrezzobase = New NTSInformatica.NTSLabel
    Me.lbAquant = New NTSInformatica.NTSLabel
    Me.edAquant = New NTSInformatica.NTSTextBoxNum
    Me.lbDatabase = New NTSInformatica.NTSLabel
    Me.lbDataattuale = New NTSInformatica.NTSLabel
    Me.frProgressivi = New NTSInformatica.NTSGroupBox
    Me.edDtulcar = New NTSInformatica.NTSTextBoxData
    Me.cmdAltriMag = New NTSInformatica.NTSButton
    Me.lbEsist = New NTSInformatica.NTSLabel
    Me.lbDtulcar = New NTSInformatica.NTSLabel
    Me.edEsist = New NTSInformatica.NTSTextBoxNum
    Me.lbInesaur = New NTSInformatica.NTSLabel
    Me.lbValuta = New NTSInformatica.NTSLabel
    Me.ckNetto = New NTSInformatica.NTSCheckBox
    Me.edConto = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_Codart = New NTSInformatica.NTSLabel
    Me.lbCodart = New NTSInformatica.NTSLabel
    Me.edFase = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_Fase = New NTSInformatica.NTSLabel
    Me.lbFase = New NTSInformatica.NTSLabel
    Me.edUnmis = New NTSInformatica.NTSTextBoxStr
    Me.edCodart = New NTSInformatica.NTSTextBoxStr
    Me.edCoddest = New NTSInformatica.NTSTextBoxNum
    Me.lbCoddest = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPrezzobase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPrezzoattuale.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDaquant.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNuovovalore.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAquant.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frProgressivi, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frProgressivi.SuspendLayout()
    CType(Me.edDtulcar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEsist.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNetto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edUnmis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCoddest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'cmdFine
    '
    Me.cmdFine.ImageText = ""
    Me.cmdFine.Location = New System.Drawing.Point(578, 235)
    Me.cmdFine.Name = "cmdFine"
    Me.cmdFine.NTSContextMenu = Nothing
    Me.cmdFine.Size = New System.Drawing.Size(92, 26)
    Me.cmdFine.TabIndex = 14
    Me.cmdFine.Text = "&Fine"
    '
    'cmdSalta
    '
    Me.cmdSalta.ImageText = ""
    Me.cmdSalta.Location = New System.Drawing.Point(112, 235)
    Me.cmdSalta.Name = "cmdSalta"
    Me.cmdSalta.NTSContextMenu = Nothing
    Me.cmdSalta.Size = New System.Drawing.Size(92, 26)
    Me.cmdSalta.TabIndex = 13
    Me.cmdSalta.Text = "&Salta"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(14, 235)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.NTSContextMenu = Nothing
    Me.cmdOk.Size = New System.Drawing.Size(92, 26)
    Me.cmdOk.TabIndex = 12
    Me.cmdOk.Text = "&Conferma"
    '
    'lbConto
    '
    Me.lbConto.AutoSize = True
    Me.lbConto.BackColor = System.Drawing.Color.Transparent
    Me.lbConto.Location = New System.Drawing.Point(11, 10)
    Me.lbConto.Name = "lbConto"
    Me.lbConto.NTSDbField = ""
    Me.lbConto.Size = New System.Drawing.Size(73, 13)
    Me.lbConto.TabIndex = 19
    Me.lbConto.Text = "Conto/destin."
    Me.lbConto.Tooltip = ""
    Me.lbConto.UseMnemonic = False
    '
    'lbXx_Conto
    '
    Me.lbXx_Conto.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Conto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Conto.Location = New System.Drawing.Point(191, 9)
    Me.lbXx_Conto.Name = "lbXx_Conto"
    Me.lbXx_Conto.NTSDbField = ""
    Me.lbXx_Conto.Size = New System.Drawing.Size(231, 20)
    Me.lbXx_Conto.TabIndex = 22
    Me.lbXx_Conto.Tooltip = ""
    Me.lbXx_Conto.UseMnemonic = False
    '
    'edPrezzobase
    '
    Me.edPrezzobase.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPrezzobase.Location = New System.Drawing.Point(94, 87)
    Me.edPrezzobase.Name = "edPrezzobase"
    Me.edPrezzobase.NTSDbField = ""
    Me.edPrezzobase.NTSFormat = "0"
    Me.edPrezzobase.NTSForzaVisZoom = False
    Me.edPrezzobase.NTSOldValue = ""
    Me.edPrezzobase.Properties.Appearance.Options.UseTextOptions = True
    Me.edPrezzobase.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPrezzobase.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPrezzobase.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPrezzobase.Properties.AutoHeight = False
    Me.edPrezzobase.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPrezzobase.Properties.MaxLength = 65536
    Me.edPrezzobase.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPrezzobase.Size = New System.Drawing.Size(122, 20)
    Me.edPrezzobase.TabIndex = 23
    Me.edPrezzobase.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edPrezzobase.TextInt = 0
    '
    'edPrezzoattuale
    '
    Me.edPrezzoattuale.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPrezzoattuale.Location = New System.Drawing.Point(94, 113)
    Me.edPrezzoattuale.Name = "edPrezzoattuale"
    Me.edPrezzoattuale.NTSDbField = ""
    Me.edPrezzoattuale.NTSFormat = "0"
    Me.edPrezzoattuale.NTSForzaVisZoom = False
    Me.edPrezzoattuale.NTSOldValue = ""
    Me.edPrezzoattuale.Properties.Appearance.Options.UseTextOptions = True
    Me.edPrezzoattuale.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPrezzoattuale.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPrezzoattuale.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPrezzoattuale.Properties.AutoHeight = False
    Me.edPrezzoattuale.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPrezzoattuale.Properties.MaxLength = 65536
    Me.edPrezzoattuale.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPrezzoattuale.Size = New System.Drawing.Size(122, 20)
    Me.edPrezzoattuale.TabIndex = 30
    Me.edPrezzoattuale.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edPrezzoattuale.TextInt = 0
    '
    'edDaquant
    '
    Me.edDaquant.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDaquant.Location = New System.Drawing.Point(94, 139)
    Me.edDaquant.Name = "edDaquant"
    Me.edDaquant.NTSDbField = ""
    Me.edDaquant.NTSFormat = "0"
    Me.edDaquant.NTSForzaVisZoom = False
    Me.edDaquant.NTSOldValue = ""
    Me.edDaquant.Properties.Appearance.Options.UseTextOptions = True
    Me.edDaquant.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDaquant.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDaquant.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDaquant.Properties.AutoHeight = False
    Me.edDaquant.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDaquant.Properties.MaxLength = 65536
    Me.edDaquant.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDaquant.Size = New System.Drawing.Size(122, 20)
    Me.edDaquant.TabIndex = 31
    Me.edDaquant.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edDaquant.TextInt = 0
    '
    'lbUnmis
    '
    Me.lbUnmis.AutoSize = True
    Me.lbUnmis.BackColor = System.Drawing.Color.Transparent
    Me.lbUnmis.Location = New System.Drawing.Point(11, 168)
    Me.lbUnmis.Name = "lbUnmis"
    Me.lbUnmis.NTSDbField = ""
    Me.lbUnmis.Size = New System.Drawing.Size(77, 13)
    Me.lbUnmis.TabIndex = 32
    Me.lbUnmis.Text = "Unità di misura"
    Me.lbUnmis.Tooltip = ""
    Me.lbUnmis.UseMnemonic = False
    '
    'edNuovovalore
    '
    Me.edNuovovalore.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNuovovalore.Location = New System.Drawing.Point(94, 191)
    Me.edNuovovalore.Name = "edNuovovalore"
    Me.edNuovovalore.NTSDbField = ""
    Me.edNuovovalore.NTSFormat = "0"
    Me.edNuovovalore.NTSForzaVisZoom = False
    Me.edNuovovalore.NTSOldValue = ""
    Me.edNuovovalore.Properties.Appearance.Options.UseTextOptions = True
    Me.edNuovovalore.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNuovovalore.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNuovovalore.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNuovovalore.Properties.AutoHeight = False
    Me.edNuovovalore.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNuovovalore.Properties.MaxLength = 65536
    Me.edNuovovalore.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNuovovalore.Size = New System.Drawing.Size(122, 20)
    Me.edNuovovalore.TabIndex = 34
    Me.edNuovovalore.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edNuovovalore.TextInt = 0
    '
    'lbNuovovalore
    '
    Me.lbNuovovalore.AutoSize = True
    Me.lbNuovovalore.BackColor = System.Drawing.Color.Transparent
    Me.lbNuovovalore.Location = New System.Drawing.Point(10, 194)
    Me.lbNuovovalore.Name = "lbNuovovalore"
    Me.lbNuovovalore.NTSDbField = ""
    Me.lbNuovovalore.Size = New System.Drawing.Size(77, 13)
    Me.lbNuovovalore.TabIndex = 35
    Me.lbNuovovalore.Text = "Val. modificato"
    Me.lbNuovovalore.Tooltip = ""
    Me.lbNuovovalore.UseMnemonic = False
    '
    'lbDaquant
    '
    Me.lbDaquant.AutoSize = True
    Me.lbDaquant.BackColor = System.Drawing.Color.Transparent
    Me.lbDaquant.Location = New System.Drawing.Point(11, 142)
    Me.lbDaquant.Name = "lbDaquant"
    Me.lbDaquant.NTSDbField = ""
    Me.lbDaquant.Size = New System.Drawing.Size(63, 13)
    Me.lbDaquant.TabIndex = 36
    Me.lbDaquant.Text = "Da quantità"
    Me.lbDaquant.Tooltip = ""
    Me.lbDaquant.UseMnemonic = False
    '
    'lbPrezzoattuale
    '
    Me.lbPrezzoattuale.AutoSize = True
    Me.lbPrezzoattuale.BackColor = System.Drawing.Color.Transparent
    Me.lbPrezzoattuale.Location = New System.Drawing.Point(11, 116)
    Me.lbPrezzoattuale.Name = "lbPrezzoattuale"
    Me.lbPrezzoattuale.NTSDbField = ""
    Me.lbPrezzoattuale.Size = New System.Drawing.Size(74, 13)
    Me.lbPrezzoattuale.TabIndex = 37
    Me.lbPrezzoattuale.Text = "Valore attuale"
    Me.lbPrezzoattuale.Tooltip = ""
    Me.lbPrezzoattuale.UseMnemonic = False
    '
    'lbPrezzobase
    '
    Me.lbPrezzobase.AutoSize = True
    Me.lbPrezzobase.BackColor = System.Drawing.Color.Transparent
    Me.lbPrezzobase.Location = New System.Drawing.Point(11, 90)
    Me.lbPrezzobase.Name = "lbPrezzobase"
    Me.lbPrezzobase.NTSDbField = ""
    Me.lbPrezzobase.Size = New System.Drawing.Size(63, 13)
    Me.lbPrezzobase.TabIndex = 38
    Me.lbPrezzobase.Text = "Valore base"
    Me.lbPrezzobase.Tooltip = ""
    Me.lbPrezzobase.UseMnemonic = False
    '
    'lbAquant
    '
    Me.lbAquant.AutoSize = True
    Me.lbAquant.BackColor = System.Drawing.Color.Transparent
    Me.lbAquant.Location = New System.Drawing.Point(222, 142)
    Me.lbAquant.Name = "lbAquant"
    Me.lbAquant.NTSDbField = ""
    Me.lbAquant.Size = New System.Drawing.Size(56, 13)
    Me.lbAquant.TabIndex = 40
    Me.lbAquant.Text = "a quantità"
    Me.lbAquant.Tooltip = ""
    Me.lbAquant.UseMnemonic = False
    '
    'edAquant
    '
    Me.edAquant.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAquant.Location = New System.Drawing.Point(300, 139)
    Me.edAquant.Name = "edAquant"
    Me.edAquant.NTSDbField = ""
    Me.edAquant.NTSFormat = "0"
    Me.edAquant.NTSForzaVisZoom = False
    Me.edAquant.NTSOldValue = ""
    Me.edAquant.Properties.Appearance.Options.UseTextOptions = True
    Me.edAquant.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAquant.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAquant.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAquant.Properties.AutoHeight = False
    Me.edAquant.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAquant.Properties.MaxLength = 65536
    Me.edAquant.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAquant.Size = New System.Drawing.Size(122, 20)
    Me.edAquant.TabIndex = 39
    Me.edAquant.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edAquant.TextInt = 0
    '
    'lbDatabase
    '
    Me.lbDatabase.BackColor = System.Drawing.Color.Transparent
    Me.lbDatabase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDatabase.Location = New System.Drawing.Point(222, 87)
    Me.lbDatabase.Name = "lbDatabase"
    Me.lbDatabase.NTSDbField = ""
    Me.lbDatabase.Size = New System.Drawing.Size(448, 20)
    Me.lbDatabase.TabIndex = 41
    Me.lbDatabase.Tooltip = ""
    Me.lbDatabase.UseMnemonic = False
    '
    'lbDataattuale
    '
    Me.lbDataattuale.BackColor = System.Drawing.Color.Transparent
    Me.lbDataattuale.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDataattuale.Location = New System.Drawing.Point(222, 113)
    Me.lbDataattuale.Name = "lbDataattuale"
    Me.lbDataattuale.NTSDbField = ""
    Me.lbDataattuale.Size = New System.Drawing.Size(200, 20)
    Me.lbDataattuale.TabIndex = 42
    Me.lbDataattuale.Tooltip = ""
    Me.lbDataattuale.UseMnemonic = False
    '
    'frProgressivi
    '
    Me.frProgressivi.AllowDrop = True
    Me.frProgressivi.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frProgressivi.Appearance.Options.UseBackColor = True
    Me.frProgressivi.Controls.Add(Me.edDtulcar)
    Me.frProgressivi.Controls.Add(Me.cmdAltriMag)
    Me.frProgressivi.Controls.Add(Me.lbEsist)
    Me.frProgressivi.Controls.Add(Me.lbDtulcar)
    Me.frProgressivi.Controls.Add(Me.edEsist)
    Me.frProgressivi.Cursor = System.Windows.Forms.Cursors.Default
    Me.frProgressivi.Location = New System.Drawing.Point(428, 113)
    Me.frProgressivi.Name = "frProgressivi"
    Me.frProgressivi.Size = New System.Drawing.Size(242, 108)
    Me.frProgressivi.TabIndex = 43
    Me.frProgressivi.Text = "Progressivi"
    '
    'edDtulcar
    '
    Me.edDtulcar.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDtulcar.Location = New System.Drawing.Point(125, 52)
    Me.edDtulcar.Name = "edDtulcar"
    Me.edDtulcar.NTSDbField = ""
    Me.edDtulcar.NTSForzaVisZoom = False
    Me.edDtulcar.NTSOldValue = ""
    Me.edDtulcar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDtulcar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDtulcar.Properties.AutoHeight = False
    Me.edDtulcar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDtulcar.Properties.MaxLength = 65536
    Me.edDtulcar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDtulcar.Size = New System.Drawing.Size(109, 20)
    Me.edDtulcar.TabIndex = 41
    Me.edDtulcar.TextData = New Date(1900, 1, 1, 0, 0, 0, 0)
    '
    'cmdAltriMag
    '
    Me.cmdAltriMag.ImageText = ""
    Me.cmdAltriMag.Location = New System.Drawing.Point(142, 78)
    Me.cmdAltriMag.Name = "cmdAltriMag"
    Me.cmdAltriMag.NTSContextMenu = Nothing
    Me.cmdAltriMag.Size = New System.Drawing.Size(92, 26)
    Me.cmdAltriMag.TabIndex = 40
    Me.cmdAltriMag.Text = "Altri &magazzini"
    '
    'lbEsist
    '
    Me.lbEsist.AutoSize = True
    Me.lbEsist.BackColor = System.Drawing.Color.Transparent
    Me.lbEsist.Location = New System.Drawing.Point(5, 29)
    Me.lbEsist.Name = "lbEsist"
    Me.lbEsist.NTSDbField = ""
    Me.lbEsist.Size = New System.Drawing.Size(114, 13)
    Me.lbEsist.TabIndex = 39
    Me.lbEsist.Text = "Esistenza magazzino 1"
    Me.lbEsist.Tooltip = ""
    Me.lbEsist.UseMnemonic = False
    '
    'lbDtulcar
    '
    Me.lbDtulcar.AutoSize = True
    Me.lbDtulcar.BackColor = System.Drawing.Color.Transparent
    Me.lbDtulcar.Location = New System.Drawing.Point(5, 55)
    Me.lbDtulcar.Name = "lbDtulcar"
    Me.lbDtulcar.NTSDbField = ""
    Me.lbDtulcar.Size = New System.Drawing.Size(92, 13)
    Me.lbDtulcar.TabIndex = 38
    Me.lbDtulcar.Text = "Data ultimo carico"
    Me.lbDtulcar.Tooltip = ""
    Me.lbDtulcar.UseMnemonic = False
    '
    'edEsist
    '
    Me.edEsist.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEsist.Location = New System.Drawing.Point(125, 26)
    Me.edEsist.Name = "edEsist"
    Me.edEsist.NTSDbField = ""
    Me.edEsist.NTSFormat = "0"
    Me.edEsist.NTSForzaVisZoom = False
    Me.edEsist.NTSOldValue = ""
    Me.edEsist.Properties.Appearance.Options.UseTextOptions = True
    Me.edEsist.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEsist.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEsist.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEsist.Properties.AutoHeight = False
    Me.edEsist.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEsist.Properties.MaxLength = 65536
    Me.edEsist.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEsist.Size = New System.Drawing.Size(109, 20)
    Me.edEsist.TabIndex = 32
    Me.edEsist.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edEsist.TextInt = 0
    '
    'lbInesaur
    '
    Me.lbInesaur.BackColor = System.Drawing.Color.Transparent
    Me.lbInesaur.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbInesaur.Location = New System.Drawing.Point(210, 235)
    Me.lbInesaur.Name = "lbInesaur"
    Me.lbInesaur.NTSDbField = ""
    Me.lbInesaur.Size = New System.Drawing.Size(362, 20)
    Me.lbInesaur.TabIndex = 44
    Me.lbInesaur.Tooltip = ""
    Me.lbInesaur.UseMnemonic = False
    '
    'lbValuta
    '
    Me.lbValuta.BackColor = System.Drawing.Color.Transparent
    Me.lbValuta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbValuta.Location = New System.Drawing.Point(222, 191)
    Me.lbValuta.Name = "lbValuta"
    Me.lbValuta.NTSDbField = ""
    Me.lbValuta.Size = New System.Drawing.Size(200, 20)
    Me.lbValuta.TabIndex = 45
    Me.lbValuta.Tooltip = ""
    Me.lbValuta.UseMnemonic = False
    '
    'ckNetto
    '
    Me.ckNetto.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckNetto.Location = New System.Drawing.Point(222, 163)
    Me.ckNetto.Name = "ckNetto"
    Me.ckNetto.NTSCheckValue = "S"
    Me.ckNetto.NTSUnCheckValue = "N"
    Me.ckNetto.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNetto.Properties.Appearance.Options.UseBackColor = True
    Me.ckNetto.Properties.AutoHeight = False
    Me.ckNetto.Properties.Caption = "Prezzo netto"
    Me.ckNetto.Size = New System.Drawing.Size(82, 18)
    Me.ckNetto.TabIndex = 46
    '
    'edConto
    '
    Me.edConto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edConto.Location = New System.Drawing.Point(94, 9)
    Me.edConto.Name = "edConto"
    Me.edConto.NTSDbField = ""
    Me.edConto.NTSFormat = "0"
    Me.edConto.NTSForzaVisZoom = False
    Me.edConto.NTSOldValue = ""
    Me.edConto.Properties.Appearance.Options.UseTextOptions = True
    Me.edConto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edConto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edConto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edConto.Properties.AutoHeight = False
    Me.edConto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edConto.Properties.MaxLength = 65536
    Me.edConto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edConto.Size = New System.Drawing.Size(91, 20)
    Me.edConto.TabIndex = 47
    Me.edConto.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edConto.TextInt = 0
    '
    'lbXx_Codart
    '
    Me.lbXx_Codart.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codart.Location = New System.Drawing.Point(269, 35)
    Me.lbXx_Codart.Name = "lbXx_Codart"
    Me.lbXx_Codart.NTSDbField = ""
    Me.lbXx_Codart.Size = New System.Drawing.Size(401, 20)
    Me.lbXx_Codart.TabIndex = 49
    Me.lbXx_Codart.Tooltip = ""
    Me.lbXx_Codart.UseMnemonic = False
    '
    'lbCodart
    '
    Me.lbCodart.AutoSize = True
    Me.lbCodart.BackColor = System.Drawing.Color.Transparent
    Me.lbCodart.Location = New System.Drawing.Point(11, 36)
    Me.lbCodart.Name = "lbCodart"
    Me.lbCodart.NTSDbField = ""
    Me.lbCodart.Size = New System.Drawing.Size(43, 13)
    Me.lbCodart.TabIndex = 48
    Me.lbCodart.Text = "Articolo"
    Me.lbCodart.Tooltip = ""
    Me.lbCodart.UseMnemonic = False
    '
    'edFase
    '
    Me.edFase.Cursor = System.Windows.Forms.Cursors.Default
    Me.edFase.Location = New System.Drawing.Point(94, 61)
    Me.edFase.Name = "edFase"
    Me.edFase.NTSDbField = ""
    Me.edFase.NTSFormat = "0"
    Me.edFase.NTSForzaVisZoom = False
    Me.edFase.NTSOldValue = ""
    Me.edFase.Properties.Appearance.Options.UseTextOptions = True
    Me.edFase.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edFase.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edFase.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edFase.Properties.AutoHeight = False
    Me.edFase.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFase.Properties.MaxLength = 65536
    Me.edFase.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFase.Size = New System.Drawing.Size(91, 20)
    Me.edFase.TabIndex = 53
    Me.edFase.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edFase.TextInt = 0
    '
    'lbXx_Fase
    '
    Me.lbXx_Fase.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Fase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Fase.Location = New System.Drawing.Point(191, 61)
    Me.lbXx_Fase.Name = "lbXx_Fase"
    Me.lbXx_Fase.NTSDbField = ""
    Me.lbXx_Fase.Size = New System.Drawing.Size(479, 20)
    Me.lbXx_Fase.TabIndex = 52
    Me.lbXx_Fase.Tooltip = ""
    Me.lbXx_Fase.UseMnemonic = False
    '
    'lbFase
    '
    Me.lbFase.AutoSize = True
    Me.lbFase.BackColor = System.Drawing.Color.Transparent
    Me.lbFase.Location = New System.Drawing.Point(11, 62)
    Me.lbFase.Name = "lbFase"
    Me.lbFase.NTSDbField = ""
    Me.lbFase.Size = New System.Drawing.Size(30, 13)
    Me.lbFase.TabIndex = 51
    Me.lbFase.Text = "Fase"
    Me.lbFase.Tooltip = ""
    Me.lbFase.UseMnemonic = False
    '
    'edUnmis
    '
    Me.edUnmis.Cursor = System.Windows.Forms.Cursors.Default
    Me.edUnmis.Location = New System.Drawing.Point(94, 165)
    Me.edUnmis.Name = "edUnmis"
    Me.edUnmis.NTSDbField = ""
    Me.edUnmis.NTSForzaVisZoom = False
    Me.edUnmis.NTSOldValue = ""
    Me.edUnmis.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUnmis.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUnmis.Properties.AutoHeight = False
    Me.edUnmis.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUnmis.Properties.MaxLength = 65536
    Me.edUnmis.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUnmis.Size = New System.Drawing.Size(58, 20)
    Me.edUnmis.TabIndex = 54
    Me.edUnmis.TextStr = ""
    '
    'edCodart
    '
    Me.edCodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodart.Location = New System.Drawing.Point(94, 35)
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
    Me.edCodart.Size = New System.Drawing.Size(168, 20)
    Me.edCodart.TabIndex = 55
    Me.edCodart.TextStr = ""
    '
    'edCoddest
    '
    Me.edCoddest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCoddest.Location = New System.Drawing.Point(428, 9)
    Me.edCoddest.Name = "edCoddest"
    Me.edCoddest.NTSDbField = ""
    Me.edCoddest.NTSFormat = "0"
    Me.edCoddest.NTSForzaVisZoom = False
    Me.edCoddest.NTSOldValue = ""
    Me.edCoddest.Properties.Appearance.Options.UseTextOptions = True
    Me.edCoddest.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCoddest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCoddest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCoddest.Properties.AutoHeight = False
    Me.edCoddest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCoddest.Properties.MaxLength = 65536
    Me.edCoddest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCoddest.Size = New System.Drawing.Size(38, 20)
    Me.edCoddest.TabIndex = 56
    Me.edCoddest.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCoddest.TextInt = 0
    '
    'lbCoddest
    '
    Me.lbCoddest.BackColor = System.Drawing.Color.Transparent
    Me.lbCoddest.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbCoddest.Location = New System.Drawing.Point(472, 9)
    Me.lbCoddest.Name = "lbCoddest"
    Me.lbCoddest.NTSDbField = ""
    Me.lbCoddest.Size = New System.Drawing.Size(198, 20)
    Me.lbCoddest.TabIndex = 57
    Me.lbCoddest.Tooltip = ""
    Me.lbCoddest.UseMnemonic = False
    '
    'FRMMGINEA
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(679, 266)
    Me.Controls.Add(Me.lbCoddest)
    Me.Controls.Add(Me.edCoddest)
    Me.Controls.Add(Me.edCodart)
    Me.Controls.Add(Me.edUnmis)
    Me.Controls.Add(Me.edFase)
    Me.Controls.Add(Me.lbXx_Fase)
    Me.Controls.Add(Me.lbFase)
    Me.Controls.Add(Me.lbXx_Codart)
    Me.Controls.Add(Me.lbCodart)
    Me.Controls.Add(Me.edConto)
    Me.Controls.Add(Me.ckNetto)
    Me.Controls.Add(Me.lbValuta)
    Me.Controls.Add(Me.lbInesaur)
    Me.Controls.Add(Me.frProgressivi)
    Me.Controls.Add(Me.lbDataattuale)
    Me.Controls.Add(Me.lbDatabase)
    Me.Controls.Add(Me.lbAquant)
    Me.Controls.Add(Me.edAquant)
    Me.Controls.Add(Me.lbPrezzobase)
    Me.Controls.Add(Me.lbPrezzoattuale)
    Me.Controls.Add(Me.lbDaquant)
    Me.Controls.Add(Me.lbNuovovalore)
    Me.Controls.Add(Me.edNuovovalore)
    Me.Controls.Add(Me.lbUnmis)
    Me.Controls.Add(Me.edDaquant)
    Me.Controls.Add(Me.edPrezzoattuale)
    Me.Controls.Add(Me.edPrezzobase)
    Me.Controls.Add(Me.lbXx_Conto)
    Me.Controls.Add(Me.lbConto)
    Me.Controls.Add(Me.cmdFine)
    Me.Controls.Add(Me.cmdSalta)
    Me.Controls.Add(Me.cmdOk)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMMGINEA"
    Me.Text = "ELABORAZIONE MAGAZZINO ARTICOLI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPrezzobase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPrezzoattuale.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDaquant.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNuovovalore.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAquant.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frProgressivi, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frProgressivi.ResumeLayout(False)
    Me.frProgressivi.PerformLayout()
    CType(Me.edDtulcar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEsist.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNetto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edUnmis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCoddest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      FORMAT_CAMPO = "#,##0." & "".PadLeft(oMenu.TrovaNdecSuPrzUn(oCleElar.nIneaCodvalu), CChar("0"))
      '-------------------------------------------------
      edCodart.NTSSetParam(oMenu, oApp.Tr(Me, 128693346674903020, "Articolo"), 0)
      edUnmis.NTSSetParam(oMenu, oApp.Tr(Me, 128693346675059319, "Unità di misura"), 0)
      edFase.NTSSetParam(oMenu, oApp.Tr(Me, 128693346675215618, "Fase"), "0")
      edConto.NTSSetParam(oMenu, oApp.Tr(Me, 128693346675997113, "Conto"), "0")
      edCoddest.NTSSetParam(oMenu, oApp.Tr(Me, 129048472787388675, "Destinazione diversa"), "0", 9, -1, 999999999)
      edCoddest.CliePerDestdiv = edConto
      ckNetto.NTSSetParam(oMenu, oApp.Tr(Me, 128693346676153412, "Prezzo netto"), "S", "N")
      edDtulcar.NTSSetParam(oMenu, oApp.Tr(Me, 128693346677091206, "Data ultimo carico"), True)
      edEsist.NTSSetParam(oMenu, oApp.Tr(Me, 128693346677247505, "Esistenza magazzino 1"), oApp.FormatQta)
      edAquant.NTSSetParam(oMenu, oApp.Tr(Me, 128693346677716402, "a quantità"), oApp.FormatQta)
      edNuovovalore.NTSSetParam(oMenu, oApp.Tr(Me, 128693346678497897, "Val. modificato"), FORMAT_CAMPO, 12, 0, 999999999999)
      edDaquant.NTSSetParam(oMenu, oApp.Tr(Me, 128693346678810495, "Da quantità"), oApp.FormatQta)
      edPrezzoattuale.NTSSetParam(oMenu, oApp.Tr(Me, 128693346678966794, "Valore attuale"), FORMAT_CAMPO)
      edPrezzobase.NTSSetParam(oMenu, oApp.Tr(Me, 128693346679123093, "Valore base"), FORMAT_CAMPO)

      edCodart.Enabled = False
      edUnmis.Enabled = False
      edFase.Enabled = False
      edConto.Enabled = False
      edCoddest.Enabled = False
      ckNetto.Enabled = False
      edDtulcar.Enabled = False
      edEsist.Enabled = False
      edAquant.Enabled = False
      edDaquant.Enabled = False
      edPrezzoattuale.Enabled = False
      edPrezzobase.Enabled = False

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
  Public Overridable Sub InitEntity(ByRef cleElar As CLEMGELAR)
    oCleElar = cleElar
    AddHandler oCleElar.RemoteEvent, AddressOf GestisciEventiEntity
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

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      oCleElar.dIneaNuovovalore = NTSCDec(edNuovovalore.Text)
      nIneaRichiesta = 2
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdSalta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalta.Click
    Try
      nIneaRichiesta = 1
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFine.Click
    Try
      nIneaRichiesta = 0
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGINEA_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Dim dsTmp As DataSet = Nothing
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      edConto.Text = NTSCStr(oCleElar.lIneaConto)
      If Not (oCleElar.lIneaConto = 0) Then
        If Not oCleElar.edConto_Validated(NTSCInt(edConto.Text), strTmp) Then
          lbXx_Conto.Text = ""
        Else
          lbXx_Conto.Text = strTmp
        End If
      End If
      edCoddest.Text = NTSCStr(oCleElar.lIneaCoddest)
      If oCleElar.lIneaConto > 0 Then
        If Not oCleElar.edCoddest_Validated(NTSCInt(edCoddest.Text), oCleElar.lIneaConto, strTmp) Then
          lbCoddest.Text = ""
        Else
          lbCoddest.Text = strTmp
        End If
      End If
      If oCleElar.strIneaCodart <> oCleElar.strIneaCodartListini Then
        edCodart.Text = oCleElar.strIneaCodartListini
      Else
        edCodart.Text = oCleElar.strIneaCodart
      End If
      If Not (edCodart.Text = "") Then
        If Not oCleElar.edCodart_Validated(edCodart.Text, strTmp, dttTmp) Then
          lbXx_Codart.Text = ""
        Else
          lbXx_Codart.Text = strTmp
          If NTSCStr(dttTmp.Rows(0)!ar_inesaur) = "S" Then lbInesaur.Text = "ARTICOLO IN ESAURIMENTO!"
          oCleElar.GetEsistenza(edCodart.Text, lbEsist.Text, edEsist.Text, edDtulcar.Text)
        End If
      Else
        GctlSetVisEnab(cmdAltriMag, False)
      End If
      edFase.Text = NTSCStr(oCleElar.nIneaFase)
      lbXx_Fase.Text = ""
      If oCleElar.nIneaFase <> 0 Then
        If Not oCleElar.edFase_Validated(NTSCInt(edFase.Text), edCodart.Text, strTmp) Then
          lbXx_Fase.Text = ""
        Else
          lbXx_Fase.Text = strTmp
        End If
      End If
      If Not (oCleElar.nIneaCodvalu = 0) Then
        edPrezzobase.Text = Format(oCleElar.dIneaPrezzobase, FORMAT_CAMPO)
        edPrezzoattuale.Text = Format(oCleElar.dIneaPrezzoattuale, FORMAT_CAMPO)

        If Not oCleElar.Codvalu_Validated(oCleElar.nIneaCodvalu, strTmp) Then
          lbValuta.Text = ""
        Else
          lbValuta.Text = oApp.Tr(Me, 130421083566229134, "Elaborazione in : |" & strTmp & "|")
        End If
      Else
        edPrezzobase.Text = Format(oCleElar.dIneaPrezzobase, FORMAT_CAMPO)
        edPrezzoattuale.Text = Format(oCleElar.dIneaPrezzoattuale, FORMAT_CAMPO)
        lbValuta.Text = oApp.Tr(Me, 130421083779352770, "Elaborazione in : Euro")
      End If
      edNuovovalore.Text = Format(oCleElar.dIneaNuovovalore, FORMAT_CAMPO)
      lbDatabase.Text = oCleElar.strIneaDatabase
      lbDataattuale.Text = oCleElar.strIneaDataattuale
      edDaquant.Text = NTSCStr(oCleElar.dIneaDaquant)
      edAquant.Text = NTSCStr(oCleElar.dIneaAquant)
      edUnmis.Text = oCleElar.strIneaUnmis
      ckNetto.Checked = CBool(IIf(oCleElar.strIneaNetto = "S", True, False))

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
  Public Overridable Sub cmdAltriMag_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAltriMag.Click
    Dim oPar As New CLE__CLDP
    Try
      If (cmdAltriMag.Enabled = False) Or (Trim(oCleElar.strIneaCodart) = "") Then Exit Sub
      oPar.strPar1 = "BNMGELAR"
      oPar.strPar2 = edCodart.Text
      oPar.dPar1 = 0

      oMenu.RunChild("NTSInformatica", "FRMMGHLAP", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGINEA_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Dim ctrlTmp As Control = Nothing
    Try
      If e.KeyCode = Keys.Enter Then
        cmdOk_Click(Me, Nothing)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGINEA_ActivatedFirst(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ActivatedFirst
    Try
      edNuovovalore.Focus()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class