Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGLIST
  Public oCleDocu As CLEMGDOCU
  Public oCallParams As CLE__CLDP
  Public dsList As DataSet
  Public dcList, dcPromo As New BindingSource
  Public lForn1 As Integer = 0
  Public lForn2 As Integer = 0
  Public nFase As Integer = 0
  Public bOnLoading As Boolean = False
  Public bModCCC As Boolean = False
  Public strEscludiListini As String

  Private components As System.ComponentModel.IContainer

  Public Overridable Sub InitializeComponent()
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.edCodart = New NTSInformatica.NTSTextBoxStr
    Me.fmValuta = New NTSInformatica.NTSGroupBox
    Me.lbCodvalu = New NTSInformatica.NTSLabel
    Me.edCodvalu = New NTSInformatica.NTSTextBoxNum
    Me.opValAltro = New NTSInformatica.NTSRadioButton
    Me.opValEur = New NTSInformatica.NTSRadioButton
    Me.fmValidita = New NTSInformatica.NTSGroupBox
    Me.edDtval = New NTSInformatica.NTSTextBoxData
    Me.opValDay = New NTSInformatica.NTSRadioButton
    Me.opValTutti = New NTSInformatica.NTSRadioButton
    Me.cmdVismov = New NTSInformatica.NTSButton
    Me.cmdEsci = New NTSInformatica.NTSButton
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.lbCodart = New NTSInformatica.NTSLabel
    Me.lbArticoloLabel = New NTSInformatica.NTSLabel
    Me.pnGrid = New NTSInformatica.NTSPanel
    Me.grList = New NTSInformatica.NTSGrid
    Me.grvList = New NTSInformatica.NTSGridView
    Me.lc_listino = New NTSInformatica.NTSGridColumn
    Me.xx_listino = New NTSInformatica.NTSGridColumn
    Me.lc_prezzo = New NTSInformatica.NTSGridColumn
    Me.lc_daquant = New NTSInformatica.NTSGridColumn
    Me.lc_aquant = New NTSInformatica.NTSGridColumn
    Me.lc_datagg = New NTSInformatica.NTSGridColumn
    Me.lc_datscad = New NTSInformatica.NTSGridColumn
    Me.lc_note = New NTSInformatica.NTSGridColumn
    Me.lc_codlavo = New NTSInformatica.NTSGridColumn
    Me.xx_codlavo = New NTSInformatica.NTSGridColumn
    Me.lc_conto = New NTSInformatica.NTSGridColumn
    Me.xx_conto = New NTSInformatica.NTSGridColumn
    Me.lc_codvalu = New NTSInformatica.NTSGridColumn
    Me.xx_codvalu = New NTSInformatica.NTSGridColumn
    Me.lc_codtpro = New NTSInformatica.NTSGridColumn
    Me.xx_codtpro = New NTSInformatica.NTSGridColumn
    Me.lc_perqta = New NTSInformatica.NTSGridColumn
    Me.lc_unmis = New NTSInformatica.NTSGridColumn
    Me.lc_netto = New NTSInformatica.NTSGridColumn
    Me.lc_coddest = New NTSInformatica.NTSGridColumn
    Me.xx_coddest = New NTSInformatica.NTSGridColumn
    Me.so_scont1 = New NTSInformatica.NTSGridColumn
    Me.so_scont2 = New NTSInformatica.NTSGridColumn
    Me.so_scont3 = New NTSInformatica.NTSGridColumn
    Me.so_scont4 = New NTSInformatica.NTSGridColumn
    Me.so_scont5 = New NTSInformatica.NTSGridColumn
    Me.so_scont6 = New NTSInformatica.NTSGridColumn
    Me.xx_prznetto = New NTSInformatica.NTSGridColumn
    Me.fmPromo = New NTSInformatica.NTSGroupBox
    Me.grPromo = New NTSInformatica.NTSGrid
    Me.grvPromo = New NTSInformatica.NTSGridView
    Me.tb_codrepr = New NTSInformatica.NTSGridColumn
    Me.tb_desrepr = New NTSInformatica.NTSGridColumn
    Me.tb_tipopr = New NTSInformatica.NTSGridColumn
    Me.tb_datini = New NTSInformatica.NTSGridColumn
    Me.tb_datfin = New NTSInformatica.NTSGridColumn
    Me.km_aammgg = New NTSInformatica.NTSGridColumn
    Me.km_tipork = New NTSInformatica.NTSGridColumn
    Me.km_serie = New NTSInformatica.NTSGridColumn
    Me.km_numdoc = New NTSInformatica.NTSGridColumn
    Me.km_causale = New NTSInformatica.NTSGridColumn
    Me.tb_descaum = New NTSInformatica.NTSGridColumn
    Me.tm_riferim = New NTSInformatica.NTSGridColumn
    Me.xx_scarichi = New NTSInformatica.NTSGridColumn
    Me.xx_carichi = New NTSInformatica.NTSGridColumn
    Me.xx_prezzo = New NTSInformatica.NTSGridColumn
    Me.mm_valore = New NTSInformatica.NTSGridColumn
    Me.mm_quant = New NTSInformatica.NTSGridColumn
    Me.mm_prelist = New NTSInformatica.NTSGridColumn
    Me.mm_prezzo = New NTSInformatica.NTSGridColumn
    Me.mm_preziva = New NTSInformatica.NTSGridColumn
    Me.mm_prezvalc = New NTSInformatica.NTSGridColumn
    Me.mm_scont1 = New NTSInformatica.NTSGridColumn
    Me.mm_scont2 = New NTSInformatica.NTSGridColumn
    Me.xx_lottox = New NTSInformatica.NTSGridColumn
    Me.km_magaz = New NTSInformatica.NTSGridColumn
    Me.km_subcommeca = New NTSInformatica.NTSGridColumn
    Me.km_ubicaz = New NTSInformatica.NTSGridColumn
    Me.mm_codcena = New NTSInformatica.NTSGridColumn
    Me.mm_codcfam = New NTSInformatica.NTSGridColumn
    Me.mm_codiva = New NTSInformatica.NTSGridColumn
    Me.mm_codnomc = New NTSInformatica.NTSGridColumn
    Me.mm_colli = New NTSInformatica.NTSGridColumn
    Me.mm_commeca = New NTSInformatica.NTSGridColumn
    Me.mm_controp = New NTSInformatica.NTSGridColumn
    Me.mm_misura1 = New NTSInformatica.NTSGridColumn
    Me.mm_misura2 = New NTSInformatica.NTSGridColumn
    Me.mm_misura3 = New NTSInformatica.NTSGridColumn
    Me.mm_ornum = New NTSInformatica.NTSGridColumn
    Me.mm_perqta = New NTSInformatica.NTSGridColumn
    Me.mm_provv = New NTSInformatica.NTSGridColumn
    Me.mm_provv2 = New NTSInformatica.NTSGridColumn
    Me.mm_scont3 = New NTSInformatica.NTSGridColumn
    Me.mm_scont4 = New NTSInformatica.NTSGridColumn
    Me.mm_scont5 = New NTSInformatica.NTSGridColumn
    Me.mm_scont6 = New NTSInformatica.NTSGridColumn
    Me.mm_vprovv = New NTSInformatica.NTSGridColumn
    Me.mm_vprovv2 = New NTSInformatica.NTSGridColumn
    Me.tm_valuta = New NTSInformatica.NTSGridColumn
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmValuta, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmValuta.SuspendLayout()
    CType(Me.edCodvalu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opValAltro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opValEur.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmValidita, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmValidita.SuspendLayout()
    CType(Me.edDtval.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opValDay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opValTutti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
    CType(Me.grList, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvList, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmPromo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmPromo.SuspendLayout()
    CType(Me.grPromo, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvPromo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
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
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.edCodart)
    Me.pnTop.Controls.Add(Me.fmValuta)
    Me.pnTop.Controls.Add(Me.fmValidita)
    Me.pnTop.Controls.Add(Me.cmdVismov)
    Me.pnTop.Controls.Add(Me.cmdEsci)
    Me.pnTop.Controls.Add(Me.cmdSeleziona)
    Me.pnTop.Controls.Add(Me.lbCodart)
    Me.pnTop.Controls.Add(Me.lbArticoloLabel)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(774, 88)
    Me.pnTop.TabIndex = 6
    Me.pnTop.Text = "NtsPanel1"
    '
    'edCodart
    '
    Me.edCodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodart.EditValue = ""
    Me.edCodart.Location = New System.Drawing.Point(69, 10)
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
    Me.edCodart.Size = New System.Drawing.Size(220, 20)
    Me.edCodart.TabIndex = 14
    '
    'fmValuta
    '
    Me.fmValuta.AllowDrop = True
    Me.fmValuta.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmValuta.Appearance.Options.UseBackColor = True
    Me.fmValuta.Controls.Add(Me.lbCodvalu)
    Me.fmValuta.Controls.Add(Me.edCodvalu)
    Me.fmValuta.Controls.Add(Me.opValAltro)
    Me.fmValuta.Controls.Add(Me.opValEur)
    Me.fmValuta.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmValuta.Location = New System.Drawing.Point(306, 10)
    Me.fmValuta.Name = "fmValuta"
    Me.fmValuta.Size = New System.Drawing.Size(186, 69)
    Me.fmValuta.TabIndex = 13
    Me.fmValuta.Text = "Valuta"
    '
    'lbCodvalu
    '
    Me.lbCodvalu.BackColor = System.Drawing.Color.Transparent
    Me.lbCodvalu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbCodvalu.Location = New System.Drawing.Point(99, 44)
    Me.lbCodvalu.Name = "lbCodvalu"
    Me.lbCodvalu.NTSDbField = ""
    Me.lbCodvalu.Size = New System.Drawing.Size(80, 20)
    Me.lbCodvalu.TabIndex = 3
    Me.lbCodvalu.Tooltip = ""
    Me.lbCodvalu.UseMnemonic = False
    '
    'edCodvalu
    '
    Me.edCodvalu.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodvalu.EditValue = "0"
    Me.edCodvalu.Location = New System.Drawing.Point(58, 44)
    Me.edCodvalu.Name = "edCodvalu"
    Me.edCodvalu.NTSDbField = ""
    Me.edCodvalu.NTSFormat = "0"
    Me.edCodvalu.NTSForzaVisZoom = False
    Me.edCodvalu.NTSOldValue = ""
    Me.edCodvalu.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodvalu.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodvalu.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodvalu.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodvalu.Properties.AutoHeight = False
    Me.edCodvalu.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodvalu.Properties.MaxLength = 65536
    Me.edCodvalu.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodvalu.Size = New System.Drawing.Size(41, 20)
    Me.edCodvalu.TabIndex = 3
    '
    'opValAltro
    '
    Me.opValAltro.Cursor = System.Windows.Forms.Cursors.Default
    Me.opValAltro.EditValue = True
    Me.opValAltro.Location = New System.Drawing.Point(5, 45)
    Me.opValAltro.Name = "opValAltro"
    Me.opValAltro.NTSCheckValue = "S"
    Me.opValAltro.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opValAltro.Properties.Appearance.Options.UseBackColor = True
    Me.opValAltro.Properties.AutoHeight = False
    Me.opValAltro.Properties.Caption = "Altro"
    Me.opValAltro.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opValAltro.Size = New System.Drawing.Size(56, 19)
    Me.opValAltro.TabIndex = 2
    '
    'opValEur
    '
    Me.opValEur.Cursor = System.Windows.Forms.Cursors.Default
    Me.opValEur.Location = New System.Drawing.Point(5, 23)
    Me.opValEur.Name = "opValEur"
    Me.opValEur.NTSCheckValue = "S"
    Me.opValEur.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opValEur.Properties.Appearance.Options.UseBackColor = True
    Me.opValEur.Properties.AutoHeight = False
    Me.opValEur.Properties.Caption = "Euro"
    Me.opValEur.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opValEur.Size = New System.Drawing.Size(48, 19)
    Me.opValEur.TabIndex = 1
    '
    'fmValidita
    '
    Me.fmValidita.AllowDrop = True
    Me.fmValidita.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmValidita.Appearance.Options.UseBackColor = True
    Me.fmValidita.Controls.Add(Me.edDtval)
    Me.fmValidita.Controls.Add(Me.opValDay)
    Me.fmValidita.Controls.Add(Me.opValTutti)
    Me.fmValidita.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmValidita.Location = New System.Drawing.Point(503, 10)
    Me.fmValidita.Name = "fmValidita"
    Me.fmValidita.Size = New System.Drawing.Size(162, 70)
    Me.fmValidita.TabIndex = 12
    Me.fmValidita.Text = "Validità"
    '
    'edDtval
    '
    Me.edDtval.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDtval.EditValue = "15/01/2008"
    Me.edDtval.Location = New System.Drawing.Point(67, 46)
    Me.edDtval.Name = "edDtval"
    Me.edDtval.NTSDbField = ""
    Me.edDtval.NTSForzaVisZoom = False
    Me.edDtval.NTSOldValue = ""
    Me.edDtval.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDtval.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDtval.Properties.AutoHeight = False
    Me.edDtval.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDtval.Properties.MaxLength = 65536
    Me.edDtval.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDtval.Size = New System.Drawing.Size(89, 20)
    Me.edDtval.TabIndex = 2
    '
    'opValDay
    '
    Me.opValDay.Cursor = System.Windows.Forms.Cursors.Default
    Me.opValDay.EditValue = True
    Me.opValDay.Location = New System.Drawing.Point(5, 46)
    Me.opValDay.Name = "opValDay"
    Me.opValDay.NTSCheckValue = "S"
    Me.opValDay.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opValDay.Properties.Appearance.Options.UseBackColor = True
    Me.opValDay.Properties.AutoHeight = False
    Me.opValDay.Properties.Caption = "Valido il"
    Me.opValDay.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opValDay.Size = New System.Drawing.Size(56, 19)
    Me.opValDay.TabIndex = 1
    '
    'opValTutti
    '
    Me.opValTutti.Cursor = System.Windows.Forms.Cursors.Default
    Me.opValTutti.Location = New System.Drawing.Point(5, 23)
    Me.opValTutti.Name = "opValTutti"
    Me.opValTutti.NTSCheckValue = "S"
    Me.opValTutti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opValTutti.Properties.Appearance.Options.UseBackColor = True
    Me.opValTutti.Properties.AutoHeight = False
    Me.opValTutti.Properties.Caption = "Tutti"
    Me.opValTutti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opValTutti.Size = New System.Drawing.Size(48, 19)
    Me.opValTutti.TabIndex = 0
    '
    'cmdVismov
    '
    Me.cmdVismov.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdVismov.ImagePath = ""
    Me.cmdVismov.ImageText = ""
    Me.cmdVismov.Location = New System.Drawing.Point(679, 58)
    Me.cmdVismov.Name = "cmdVismov"
    Me.cmdVismov.NTSContextMenu = Nothing
    Me.cmdVismov.Size = New System.Drawing.Size(83, 24)
    Me.cmdVismov.TabIndex = 11
    Me.cmdVismov.Text = "&Vis. movimenti"
    '
    'cmdEsci
    '
    Me.cmdEsci.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdEsci.ImagePath = ""
    Me.cmdEsci.ImageText = ""
    Me.cmdEsci.Location = New System.Drawing.Point(679, 8)
    Me.cmdEsci.Name = "cmdEsci"
    Me.cmdEsci.NTSContextMenu = Nothing
    Me.cmdEsci.Size = New System.Drawing.Size(83, 24)
    Me.cmdEsci.TabIndex = 10
    Me.cmdEsci.Text = "&Esci"
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdSeleziona.ImagePath = ""
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(679, 33)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.NTSContextMenu = Nothing
    Me.cmdSeleziona.Size = New System.Drawing.Size(83, 24)
    Me.cmdSeleziona.TabIndex = 7
    Me.cmdSeleziona.Text = "&Seleziona"
    '
    'lbCodart
    '
    Me.lbCodart.BackColor = System.Drawing.Color.Transparent
    Me.lbCodart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbCodart.Location = New System.Drawing.Point(69, 35)
    Me.lbCodart.Name = "lbCodart"
    Me.lbCodart.NTSDbField = ""
    Me.lbCodart.Size = New System.Drawing.Size(220, 20)
    Me.lbCodart.TabIndex = 3
    Me.lbCodart.Tooltip = ""
    Me.lbCodart.UseMnemonic = False
    '
    'lbArticoloLabel
    '
    Me.lbArticoloLabel.AutoSize = True
    Me.lbArticoloLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbArticoloLabel.Location = New System.Drawing.Point(11, 11)
    Me.lbArticoloLabel.Name = "lbArticoloLabel"
    Me.lbArticoloLabel.NTSDbField = ""
    Me.lbArticoloLabel.Size = New System.Drawing.Size(43, 13)
    Me.lbArticoloLabel.TabIndex = 1
    Me.lbArticoloLabel.Text = "Articolo"
    Me.lbArticoloLabel.Tooltip = ""
    Me.lbArticoloLabel.UseMnemonic = False
    '
    'pnGrid
    '
    Me.pnGrid.AllowDrop = True
    Me.pnGrid.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrid.Appearance.Options.UseBackColor = True
    Me.pnGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGrid.Controls.Add(Me.grList)
    Me.pnGrid.Controls.Add(Me.fmPromo)
    Me.pnGrid.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(0, 88)
    Me.pnGrid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGrid.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.NTSActiveTrasparency = True
    Me.pnGrid.Size = New System.Drawing.Size(774, 423)
    Me.pnGrid.TabIndex = 7
    Me.pnGrid.Text = "NtsPanel1"
    '
    'grList
    '
    Me.grList.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grList.EmbeddedNavigator.Name = ""
    Me.grList.Location = New System.Drawing.Point(0, 0)
    Me.grList.MainView = Me.grvList
    Me.grList.Name = "grList"
    Me.grList.Size = New System.Drawing.Size(774, 201)
    Me.grList.TabIndex = 8
    Me.grList.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvList})
    '
    'grvList
    '
    Me.grvList.ActiveFilterEnabled = False
    Me.grvList.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.lc_listino, Me.xx_listino, Me.lc_prezzo, Me.lc_daquant, Me.lc_aquant, Me.lc_datagg, Me.lc_datscad, Me.lc_note, Me.lc_codlavo, Me.xx_codlavo, Me.lc_conto, Me.xx_conto, Me.lc_codvalu, Me.xx_codvalu, Me.lc_codtpro, Me.xx_codtpro, Me.lc_perqta, Me.lc_unmis, Me.lc_netto, Me.lc_coddest, Me.xx_coddest, Me.so_scont1, Me.so_scont2, Me.so_scont3, Me.so_scont4, Me.so_scont5, Me.so_scont6, Me.xx_prznetto})
    Me.grvList.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvList.Enabled = True
    Me.grvList.GridControl = Me.grList
    Me.grvList.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvList.MinRowHeight = 14
    Me.grvList.Name = "grvList"
    Me.grvList.NTSAllowDelete = True
    Me.grvList.NTSAllowInsert = True
    Me.grvList.NTSAllowUpdate = True
    Me.grvList.NTSMenuContext = Nothing
    Me.grvList.OptionsCustomization.AllowRowSizing = True
    Me.grvList.OptionsFilter.AllowFilterEditor = False
    Me.grvList.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvList.OptionsNavigation.UseTabKey = False
    Me.grvList.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvList.OptionsView.ColumnAutoWidth = False
    Me.grvList.OptionsView.EnableAppearanceEvenRow = True
    Me.grvList.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvList.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvList.OptionsView.ShowGroupPanel = False
    Me.grvList.RowHeight = 14
    '
    'lc_listino
    '
    Me.lc_listino.AppearanceCell.Options.UseBackColor = True
    Me.lc_listino.AppearanceCell.Options.UseTextOptions = True
    Me.lc_listino.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_listino.Caption = "Listino"
    Me.lc_listino.Enabled = True
    Me.lc_listino.FieldName = "lc_listino"
    Me.lc_listino.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_listino.Name = "lc_listino"
    Me.lc_listino.NTSRepositoryComboBox = Nothing
    Me.lc_listino.NTSRepositoryItemCheck = Nothing
    Me.lc_listino.NTSRepositoryItemMemo = Nothing
    Me.lc_listino.NTSRepositoryItemText = Nothing
    Me.lc_listino.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_listino.OptionsFilter.AllowFilter = False
    Me.lc_listino.Visible = True
    Me.lc_listino.VisibleIndex = 0
    '
    'xx_listino
    '
    Me.xx_listino.AppearanceCell.Options.UseBackColor = True
    Me.xx_listino.AppearanceCell.Options.UseTextOptions = True
    Me.xx_listino.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_listino.Caption = "Descr. listino"
    Me.xx_listino.Enabled = True
    Me.xx_listino.FieldName = "xx_listino"
    Me.xx_listino.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_listino.Name = "xx_listino"
    Me.xx_listino.NTSRepositoryComboBox = Nothing
    Me.xx_listino.NTSRepositoryItemCheck = Nothing
    Me.xx_listino.NTSRepositoryItemMemo = Nothing
    Me.xx_listino.NTSRepositoryItemText = Nothing
    Me.xx_listino.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_listino.OptionsFilter.AllowFilter = False
    Me.xx_listino.Visible = True
    Me.xx_listino.VisibleIndex = 1
    '
    'lc_prezzo
    '
    Me.lc_prezzo.AppearanceCell.Options.UseBackColor = True
    Me.lc_prezzo.AppearanceCell.Options.UseTextOptions = True
    Me.lc_prezzo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_prezzo.Caption = "Prezzo"
    Me.lc_prezzo.Enabled = True
    Me.lc_prezzo.FieldName = "lc_prezzo"
    Me.lc_prezzo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_prezzo.Name = "lc_prezzo"
    Me.lc_prezzo.NTSRepositoryComboBox = Nothing
    Me.lc_prezzo.NTSRepositoryItemCheck = Nothing
    Me.lc_prezzo.NTSRepositoryItemMemo = Nothing
    Me.lc_prezzo.NTSRepositoryItemText = Nothing
    Me.lc_prezzo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_prezzo.OptionsFilter.AllowFilter = False
    Me.lc_prezzo.Visible = True
    Me.lc_prezzo.VisibleIndex = 2
    '
    'lc_daquant
    '
    Me.lc_daquant.AppearanceCell.Options.UseBackColor = True
    Me.lc_daquant.AppearanceCell.Options.UseTextOptions = True
    Me.lc_daquant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_daquant.Caption = "Da quantità"
    Me.lc_daquant.Enabled = True
    Me.lc_daquant.FieldName = "lc_daquant"
    Me.lc_daquant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_daquant.Name = "lc_daquant"
    Me.lc_daquant.NTSRepositoryComboBox = Nothing
    Me.lc_daquant.NTSRepositoryItemCheck = Nothing
    Me.lc_daquant.NTSRepositoryItemMemo = Nothing
    Me.lc_daquant.NTSRepositoryItemText = Nothing
    Me.lc_daquant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_daquant.OptionsFilter.AllowFilter = False
    Me.lc_daquant.Visible = True
    Me.lc_daquant.VisibleIndex = 3
    '
    'lc_aquant
    '
    Me.lc_aquant.AppearanceCell.Options.UseBackColor = True
    Me.lc_aquant.AppearanceCell.Options.UseTextOptions = True
    Me.lc_aquant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_aquant.Caption = "A quantità"
    Me.lc_aquant.Enabled = True
    Me.lc_aquant.FieldName = "lc_aquant"
    Me.lc_aquant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_aquant.Name = "lc_aquant"
    Me.lc_aquant.NTSRepositoryComboBox = Nothing
    Me.lc_aquant.NTSRepositoryItemCheck = Nothing
    Me.lc_aquant.NTSRepositoryItemMemo = Nothing
    Me.lc_aquant.NTSRepositoryItemText = Nothing
    Me.lc_aquant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_aquant.OptionsFilter.AllowFilter = False
    Me.lc_aquant.Visible = True
    Me.lc_aquant.VisibleIndex = 4
    '
    'lc_datagg
    '
    Me.lc_datagg.AppearanceCell.Options.UseBackColor = True
    Me.lc_datagg.AppearanceCell.Options.UseTextOptions = True
    Me.lc_datagg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_datagg.Caption = "Data aggiorn."
    Me.lc_datagg.Enabled = True
    Me.lc_datagg.FieldName = "lc_datagg"
    Me.lc_datagg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_datagg.Name = "lc_datagg"
    Me.lc_datagg.NTSRepositoryComboBox = Nothing
    Me.lc_datagg.NTSRepositoryItemCheck = Nothing
    Me.lc_datagg.NTSRepositoryItemMemo = Nothing
    Me.lc_datagg.NTSRepositoryItemText = Nothing
    Me.lc_datagg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_datagg.OptionsFilter.AllowFilter = False
    Me.lc_datagg.Visible = True
    Me.lc_datagg.VisibleIndex = 5
    '
    'lc_datscad
    '
    Me.lc_datscad.AppearanceCell.Options.UseBackColor = True
    Me.lc_datscad.AppearanceCell.Options.UseTextOptions = True
    Me.lc_datscad.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_datscad.Caption = "Data scad."
    Me.lc_datscad.Enabled = True
    Me.lc_datscad.FieldName = "lc_datscad"
    Me.lc_datscad.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_datscad.Name = "lc_datscad"
    Me.lc_datscad.NTSRepositoryComboBox = Nothing
    Me.lc_datscad.NTSRepositoryItemCheck = Nothing
    Me.lc_datscad.NTSRepositoryItemMemo = Nothing
    Me.lc_datscad.NTSRepositoryItemText = Nothing
    Me.lc_datscad.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_datscad.OptionsFilter.AllowFilter = False
    Me.lc_datscad.Visible = True
    Me.lc_datscad.VisibleIndex = 6
    '
    'lc_note
    '
    Me.lc_note.AppearanceCell.Options.UseBackColor = True
    Me.lc_note.AppearanceCell.Options.UseTextOptions = True
    Me.lc_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_note.Caption = "Note"
    Me.lc_note.Enabled = True
    Me.lc_note.FieldName = "lc_note"
    Me.lc_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_note.Name = "lc_note"
    Me.lc_note.NTSRepositoryComboBox = Nothing
    Me.lc_note.NTSRepositoryItemCheck = Nothing
    Me.lc_note.NTSRepositoryItemMemo = Nothing
    Me.lc_note.NTSRepositoryItemText = Nothing
    Me.lc_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_note.OptionsFilter.AllowFilter = False
    Me.lc_note.Visible = True
    Me.lc_note.VisibleIndex = 7
    '
    'lc_codlavo
    '
    Me.lc_codlavo.AppearanceCell.Options.UseBackColor = True
    Me.lc_codlavo.AppearanceCell.Options.UseTextOptions = True
    Me.lc_codlavo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_codlavo.Caption = "Cod.lav."
    Me.lc_codlavo.Enabled = True
    Me.lc_codlavo.FieldName = "lc_codlavo"
    Me.lc_codlavo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_codlavo.Name = "lc_codlavo"
    Me.lc_codlavo.NTSRepositoryComboBox = Nothing
    Me.lc_codlavo.NTSRepositoryItemCheck = Nothing
    Me.lc_codlavo.NTSRepositoryItemMemo = Nothing
    Me.lc_codlavo.NTSRepositoryItemText = Nothing
    Me.lc_codlavo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_codlavo.OptionsFilter.AllowFilter = False
    Me.lc_codlavo.Visible = True
    Me.lc_codlavo.VisibleIndex = 8
    '
    'xx_codlavo
    '
    Me.xx_codlavo.AppearanceCell.Options.UseBackColor = True
    Me.xx_codlavo.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codlavo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codlavo.Caption = "Descr.lav."
    Me.xx_codlavo.Enabled = True
    Me.xx_codlavo.FieldName = "xx_codlavo"
    Me.xx_codlavo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codlavo.Name = "xx_codlavo"
    Me.xx_codlavo.NTSRepositoryComboBox = Nothing
    Me.xx_codlavo.NTSRepositoryItemCheck = Nothing
    Me.xx_codlavo.NTSRepositoryItemMemo = Nothing
    Me.xx_codlavo.NTSRepositoryItemText = Nothing
    Me.xx_codlavo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codlavo.OptionsFilter.AllowFilter = False
    Me.xx_codlavo.Visible = True
    Me.xx_codlavo.VisibleIndex = 9
    '
    'lc_conto
    '
    Me.lc_conto.AppearanceCell.Options.UseBackColor = True
    Me.lc_conto.AppearanceCell.Options.UseTextOptions = True
    Me.lc_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_conto.Caption = "Cliente/forn."
    Me.lc_conto.Enabled = True
    Me.lc_conto.FieldName = "lc_conto"
    Me.lc_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_conto.Name = "lc_conto"
    Me.lc_conto.NTSRepositoryComboBox = Nothing
    Me.lc_conto.NTSRepositoryItemCheck = Nothing
    Me.lc_conto.NTSRepositoryItemMemo = Nothing
    Me.lc_conto.NTSRepositoryItemText = Nothing
    Me.lc_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_conto.OptionsFilter.AllowFilter = False
    Me.lc_conto.Visible = True
    Me.lc_conto.VisibleIndex = 10
    '
    'xx_conto
    '
    Me.xx_conto.AppearanceCell.Options.UseBackColor = True
    Me.xx_conto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_conto.Caption = "Descrizione"
    Me.xx_conto.Enabled = True
    Me.xx_conto.FieldName = "xx_conto"
    Me.xx_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_conto.Name = "xx_conto"
    Me.xx_conto.NTSRepositoryComboBox = Nothing
    Me.xx_conto.NTSRepositoryItemCheck = Nothing
    Me.xx_conto.NTSRepositoryItemMemo = Nothing
    Me.xx_conto.NTSRepositoryItemText = Nothing
    Me.xx_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_conto.OptionsFilter.AllowFilter = False
    Me.xx_conto.Visible = True
    Me.xx_conto.VisibleIndex = 11
    '
    'lc_codvalu
    '
    Me.lc_codvalu.AppearanceCell.Options.UseBackColor = True
    Me.lc_codvalu.AppearanceCell.Options.UseTextOptions = True
    Me.lc_codvalu.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_codvalu.Caption = "Valuta"
    Me.lc_codvalu.Enabled = True
    Me.lc_codvalu.FieldName = "lc_codvalu"
    Me.lc_codvalu.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_codvalu.Name = "lc_codvalu"
    Me.lc_codvalu.NTSRepositoryComboBox = Nothing
    Me.lc_codvalu.NTSRepositoryItemCheck = Nothing
    Me.lc_codvalu.NTSRepositoryItemMemo = Nothing
    Me.lc_codvalu.NTSRepositoryItemText = Nothing
    Me.lc_codvalu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_codvalu.OptionsFilter.AllowFilter = False
    Me.lc_codvalu.Visible = True
    Me.lc_codvalu.VisibleIndex = 12
    '
    'xx_codvalu
    '
    Me.xx_codvalu.AppearanceCell.Options.UseBackColor = True
    Me.xx_codvalu.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codvalu.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codvalu.Caption = "Descr.valuta"
    Me.xx_codvalu.Enabled = True
    Me.xx_codvalu.FieldName = "xx_codvalu"
    Me.xx_codvalu.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codvalu.Name = "xx_codvalu"
    Me.xx_codvalu.NTSRepositoryComboBox = Nothing
    Me.xx_codvalu.NTSRepositoryItemCheck = Nothing
    Me.xx_codvalu.NTSRepositoryItemMemo = Nothing
    Me.xx_codvalu.NTSRepositoryItemText = Nothing
    Me.xx_codvalu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codvalu.OptionsFilter.AllowFilter = False
    Me.xx_codvalu.Visible = True
    Me.xx_codvalu.VisibleIndex = 13
    '
    'lc_codtpro
    '
    Me.lc_codtpro.AppearanceCell.Options.UseBackColor = True
    Me.lc_codtpro.AppearanceCell.Options.UseTextOptions = True
    Me.lc_codtpro.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_codtpro.Caption = "Cod. promoz."
    Me.lc_codtpro.Enabled = True
    Me.lc_codtpro.FieldName = "lc_codtpro"
    Me.lc_codtpro.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_codtpro.Name = "lc_codtpro"
    Me.lc_codtpro.NTSRepositoryComboBox = Nothing
    Me.lc_codtpro.NTSRepositoryItemCheck = Nothing
    Me.lc_codtpro.NTSRepositoryItemMemo = Nothing
    Me.lc_codtpro.NTSRepositoryItemText = Nothing
    Me.lc_codtpro.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_codtpro.OptionsFilter.AllowFilter = False
    Me.lc_codtpro.Visible = True
    Me.lc_codtpro.VisibleIndex = 14
    '
    'xx_codtpro
    '
    Me.xx_codtpro.AppearanceCell.Options.UseBackColor = True
    Me.xx_codtpro.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codtpro.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codtpro.Caption = "Descr. promoz."
    Me.xx_codtpro.Enabled = True
    Me.xx_codtpro.FieldName = "xx_codtpro"
    Me.xx_codtpro.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codtpro.Name = "xx_codtpro"
    Me.xx_codtpro.NTSRepositoryComboBox = Nothing
    Me.xx_codtpro.NTSRepositoryItemCheck = Nothing
    Me.xx_codtpro.NTSRepositoryItemMemo = Nothing
    Me.xx_codtpro.NTSRepositoryItemText = Nothing
    Me.xx_codtpro.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codtpro.OptionsFilter.AllowFilter = False
    Me.xx_codtpro.Visible = True
    Me.xx_codtpro.VisibleIndex = 15
    '
    'lc_perqta
    '
    Me.lc_perqta.AppearanceCell.Options.UseBackColor = True
    Me.lc_perqta.AppearanceCell.Options.UseTextOptions = True
    Me.lc_perqta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_perqta.Caption = "Q.tà prezzo"
    Me.lc_perqta.Enabled = True
    Me.lc_perqta.FieldName = "lc_perqta"
    Me.lc_perqta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_perqta.Name = "lc_perqta"
    Me.lc_perqta.NTSRepositoryComboBox = Nothing
    Me.lc_perqta.NTSRepositoryItemCheck = Nothing
    Me.lc_perqta.NTSRepositoryItemMemo = Nothing
    Me.lc_perqta.NTSRepositoryItemText = Nothing
    Me.lc_perqta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_perqta.OptionsFilter.AllowFilter = False
    Me.lc_perqta.Visible = True
    Me.lc_perqta.VisibleIndex = 16
    '
    'lc_unmis
    '
    Me.lc_unmis.AppearanceCell.Options.UseBackColor = True
    Me.lc_unmis.AppearanceCell.Options.UseTextOptions = True
    Me.lc_unmis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_unmis.Caption = "U.M."
    Me.lc_unmis.Enabled = True
    Me.lc_unmis.FieldName = "lc_unmis"
    Me.lc_unmis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_unmis.Name = "lc_unmis"
    Me.lc_unmis.NTSRepositoryComboBox = Nothing
    Me.lc_unmis.NTSRepositoryItemCheck = Nothing
    Me.lc_unmis.NTSRepositoryItemMemo = Nothing
    Me.lc_unmis.NTSRepositoryItemText = Nothing
    Me.lc_unmis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_unmis.OptionsFilter.AllowFilter = False
    Me.lc_unmis.Visible = True
    Me.lc_unmis.VisibleIndex = 17
    '
    'lc_netto
    '
    Me.lc_netto.AppearanceCell.Options.UseBackColor = True
    Me.lc_netto.AppearanceCell.Options.UseTextOptions = True
    Me.lc_netto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_netto.Caption = "Prezzo netto"
    Me.lc_netto.Enabled = True
    Me.lc_netto.FieldName = "lc_netto"
    Me.lc_netto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_netto.Name = "lc_netto"
    Me.lc_netto.NTSRepositoryComboBox = Nothing
    Me.lc_netto.NTSRepositoryItemCheck = Nothing
    Me.lc_netto.NTSRepositoryItemMemo = Nothing
    Me.lc_netto.NTSRepositoryItemText = Nothing
    Me.lc_netto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_netto.OptionsFilter.AllowFilter = False
    Me.lc_netto.Visible = True
    Me.lc_netto.VisibleIndex = 18
    '
    'lc_coddest
    '
    Me.lc_coddest.AppearanceCell.Options.UseBackColor = True
    Me.lc_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.lc_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.lc_coddest.Caption = "Destinaz. diversa"
    Me.lc_coddest.Enabled = True
    Me.lc_coddest.FieldName = "lc_coddest"
    Me.lc_coddest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.lc_coddest.Name = "lc_coddest"
    Me.lc_coddest.NTSRepositoryComboBox = Nothing
    Me.lc_coddest.NTSRepositoryItemCheck = Nothing
    Me.lc_coddest.NTSRepositoryItemMemo = Nothing
    Me.lc_coddest.NTSRepositoryItemText = Nothing
    Me.lc_coddest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.lc_coddest.OptionsFilter.AllowFilter = False
    Me.lc_coddest.Visible = True
    Me.lc_coddest.VisibleIndex = 19
    '
    'xx_coddest
    '
    Me.xx_coddest.AppearanceCell.Options.UseBackColor = True
    Me.xx_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.xx_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_coddest.Caption = "Descr. destinaz. diversa"
    Me.xx_coddest.Enabled = True
    Me.xx_coddest.FieldName = "xx_coddest"
    Me.xx_coddest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_coddest.Name = "xx_coddest"
    Me.xx_coddest.NTSRepositoryComboBox = Nothing
    Me.xx_coddest.NTSRepositoryItemCheck = Nothing
    Me.xx_coddest.NTSRepositoryItemMemo = Nothing
    Me.xx_coddest.NTSRepositoryItemText = Nothing
    Me.xx_coddest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_coddest.OptionsFilter.AllowFilter = False
    Me.xx_coddest.Visible = True
    Me.xx_coddest.VisibleIndex = 20
    '
    'so_scont1
    '
    Me.so_scont1.AppearanceCell.Options.UseBackColor = True
    Me.so_scont1.AppearanceCell.Options.UseTextOptions = True
    Me.so_scont1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.so_scont1.Caption = "Sconto 1"
    Me.so_scont1.Enabled = True
    Me.so_scont1.FieldName = "so_scont1"
    Me.so_scont1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.so_scont1.Name = "so_scont1"
    Me.so_scont1.NTSRepositoryComboBox = Nothing
    Me.so_scont1.NTSRepositoryItemCheck = Nothing
    Me.so_scont1.NTSRepositoryItemMemo = Nothing
    Me.so_scont1.NTSRepositoryItemText = Nothing
    Me.so_scont1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.so_scont1.OptionsFilter.AllowFilter = False
    '
    'so_scont2
    '
    Me.so_scont2.AppearanceCell.Options.UseBackColor = True
    Me.so_scont2.AppearanceCell.Options.UseTextOptions = True
    Me.so_scont2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.so_scont2.Caption = "Sconto 2"
    Me.so_scont2.Enabled = True
    Me.so_scont2.FieldName = "so_scont2"
    Me.so_scont2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.so_scont2.Name = "so_scont2"
    Me.so_scont2.NTSRepositoryComboBox = Nothing
    Me.so_scont2.NTSRepositoryItemCheck = Nothing
    Me.so_scont2.NTSRepositoryItemMemo = Nothing
    Me.so_scont2.NTSRepositoryItemText = Nothing
    Me.so_scont2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.so_scont2.OptionsFilter.AllowFilter = False
    '
    'so_scont3
    '
    Me.so_scont3.AppearanceCell.Options.UseBackColor = True
    Me.so_scont3.AppearanceCell.Options.UseTextOptions = True
    Me.so_scont3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.so_scont3.Caption = "Sconto 3"
    Me.so_scont3.Enabled = True
    Me.so_scont3.FieldName = "so_scont3"
    Me.so_scont3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.so_scont3.Name = "so_scont3"
    Me.so_scont3.NTSRepositoryComboBox = Nothing
    Me.so_scont3.NTSRepositoryItemCheck = Nothing
    Me.so_scont3.NTSRepositoryItemMemo = Nothing
    Me.so_scont3.NTSRepositoryItemText = Nothing
    Me.so_scont3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.so_scont3.OptionsFilter.AllowFilter = False
    '
    'so_scont4
    '
    Me.so_scont4.AppearanceCell.Options.UseBackColor = True
    Me.so_scont4.AppearanceCell.Options.UseTextOptions = True
    Me.so_scont4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.so_scont4.Caption = "Sconto 4"
    Me.so_scont4.Enabled = True
    Me.so_scont4.FieldName = "so_scont4"
    Me.so_scont4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.so_scont4.Name = "so_scont4"
    Me.so_scont4.NTSRepositoryComboBox = Nothing
    Me.so_scont4.NTSRepositoryItemCheck = Nothing
    Me.so_scont4.NTSRepositoryItemMemo = Nothing
    Me.so_scont4.NTSRepositoryItemText = Nothing
    Me.so_scont4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.so_scont4.OptionsFilter.AllowFilter = False
    '
    'so_scont5
    '
    Me.so_scont5.AppearanceCell.Options.UseBackColor = True
    Me.so_scont5.AppearanceCell.Options.UseTextOptions = True
    Me.so_scont5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.so_scont5.Caption = "Sconto 5"
    Me.so_scont5.Enabled = True
    Me.so_scont5.FieldName = "so_scont5"
    Me.so_scont5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.so_scont5.Name = "so_scont5"
    Me.so_scont5.NTSRepositoryComboBox = Nothing
    Me.so_scont5.NTSRepositoryItemCheck = Nothing
    Me.so_scont5.NTSRepositoryItemMemo = Nothing
    Me.so_scont5.NTSRepositoryItemText = Nothing
    Me.so_scont5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.so_scont5.OptionsFilter.AllowFilter = False
    '
    'so_scont6
    '
    Me.so_scont6.AppearanceCell.Options.UseBackColor = True
    Me.so_scont6.AppearanceCell.Options.UseTextOptions = True
    Me.so_scont6.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.so_scont6.Caption = "Sconto 6"
    Me.so_scont6.Enabled = True
    Me.so_scont6.FieldName = "so_scont6"
    Me.so_scont6.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.so_scont6.Name = "so_scont6"
    Me.so_scont6.NTSRepositoryComboBox = Nothing
    Me.so_scont6.NTSRepositoryItemCheck = Nothing
    Me.so_scont6.NTSRepositoryItemMemo = Nothing
    Me.so_scont6.NTSRepositoryItemText = Nothing
    Me.so_scont6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.so_scont6.OptionsFilter.AllowFilter = False
    '
    'xx_prznetto
    '
    Me.xx_prznetto.AppearanceCell.Options.UseBackColor = True
    Me.xx_prznetto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_prznetto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_prznetto.Caption = "Prezzo al netto di sconti"
    Me.xx_prznetto.Enabled = True
    Me.xx_prznetto.FieldName = "xx_prznetto"
    Me.xx_prznetto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_prznetto.Name = "xx_prznetto"
    Me.xx_prznetto.NTSRepositoryComboBox = Nothing
    Me.xx_prznetto.NTSRepositoryItemCheck = Nothing
    Me.xx_prznetto.NTSRepositoryItemMemo = Nothing
    Me.xx_prznetto.NTSRepositoryItemText = Nothing
    Me.xx_prznetto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_prznetto.OptionsFilter.AllowFilter = False
    '
    'fmPromo
    '
    Me.fmPromo.AllowDrop = True
    Me.fmPromo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmPromo.Appearance.Options.UseBackColor = True
    Me.fmPromo.Controls.Add(Me.grPromo)
    Me.fmPromo.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.fmPromo.Location = New System.Drawing.Point(0, 201)
    Me.fmPromo.Name = "fmPromo"
    Me.fmPromo.Size = New System.Drawing.Size(774, 222)
    Me.fmPromo.TabIndex = 9
    Me.fmPromo.Text = "Promozioni"
    '
    'grPromo
    '
    Me.grPromo.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grPromo.EmbeddedNavigator.Name = ""
    Me.grPromo.Location = New System.Drawing.Point(2, 20)
    Me.grPromo.MainView = Me.grvPromo
    Me.grPromo.Name = "grPromo"
    Me.grPromo.Size = New System.Drawing.Size(770, 200)
    Me.grPromo.TabIndex = 0
    Me.grPromo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvPromo})
    '
    'grvPromo
    '
    Me.grvPromo.ActiveFilterEnabled = False
    Me.grvPromo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tb_codrepr, Me.tb_desrepr, Me.tb_tipopr, Me.tb_datini, Me.tb_datfin})
    Me.grvPromo.Enabled = True
    Me.grvPromo.GridControl = Me.grPromo
    Me.grvPromo.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvPromo.MinRowHeight = 14
    Me.grvPromo.Name = "grvPromo"
    Me.grvPromo.NTSAllowDelete = True
    Me.grvPromo.NTSAllowInsert = True
    Me.grvPromo.NTSAllowUpdate = True
    Me.grvPromo.NTSMenuContext = Nothing
    Me.grvPromo.OptionsCustomization.AllowRowSizing = True
    Me.grvPromo.OptionsFilter.AllowFilterEditor = False
    Me.grvPromo.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvPromo.OptionsNavigation.UseTabKey = False
    Me.grvPromo.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvPromo.OptionsView.ColumnAutoWidth = False
    Me.grvPromo.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvPromo.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvPromo.OptionsView.ShowGroupPanel = False
    Me.grvPromo.RowHeight = 14
    '
    'tb_codrepr
    '
    Me.tb_codrepr.AppearanceCell.Options.UseBackColor = True
    Me.tb_codrepr.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codrepr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codrepr.Caption = "Cod."
    Me.tb_codrepr.Enabled = True
    Me.tb_codrepr.FieldName = "tb_codrepr"
    Me.tb_codrepr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codrepr.Name = "tb_codrepr"
    Me.tb_codrepr.NTSRepositoryComboBox = Nothing
    Me.tb_codrepr.NTSRepositoryItemCheck = Nothing
    Me.tb_codrepr.NTSRepositoryItemMemo = Nothing
    Me.tb_codrepr.NTSRepositoryItemText = Nothing
    Me.tb_codrepr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codrepr.OptionsFilter.AllowFilter = False
    Me.tb_codrepr.Visible = True
    Me.tb_codrepr.VisibleIndex = 0
    '
    'tb_desrepr
    '
    Me.tb_desrepr.AppearanceCell.Options.UseBackColor = True
    Me.tb_desrepr.AppearanceCell.Options.UseTextOptions = True
    Me.tb_desrepr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_desrepr.Caption = "Descr. Promozione"
    Me.tb_desrepr.Enabled = True
    Me.tb_desrepr.FieldName = "tb_desrepr"
    Me.tb_desrepr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_desrepr.Name = "tb_desrepr"
    Me.tb_desrepr.NTSRepositoryComboBox = Nothing
    Me.tb_desrepr.NTSRepositoryItemCheck = Nothing
    Me.tb_desrepr.NTSRepositoryItemMemo = Nothing
    Me.tb_desrepr.NTSRepositoryItemText = Nothing
    Me.tb_desrepr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_desrepr.OptionsFilter.AllowFilter = False
    Me.tb_desrepr.Visible = True
    Me.tb_desrepr.VisibleIndex = 1
    '
    'tb_tipopr
    '
    Me.tb_tipopr.AppearanceCell.Options.UseBackColor = True
    Me.tb_tipopr.AppearanceCell.Options.UseTextOptions = True
    Me.tb_tipopr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_tipopr.Caption = "Tipo"
    Me.tb_tipopr.Enabled = True
    Me.tb_tipopr.FieldName = "tb_tipopr"
    Me.tb_tipopr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_tipopr.Name = "tb_tipopr"
    Me.tb_tipopr.NTSRepositoryComboBox = Nothing
    Me.tb_tipopr.NTSRepositoryItemCheck = Nothing
    Me.tb_tipopr.NTSRepositoryItemMemo = Nothing
    Me.tb_tipopr.NTSRepositoryItemText = Nothing
    Me.tb_tipopr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_tipopr.OptionsFilter.AllowFilter = False
    Me.tb_tipopr.Visible = True
    Me.tb_tipopr.VisibleIndex = 2
    '
    'tb_datini
    '
    Me.tb_datini.AppearanceCell.Options.UseBackColor = True
    Me.tb_datini.AppearanceCell.Options.UseTextOptions = True
    Me.tb_datini.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_datini.Caption = "Data Inizio"
    Me.tb_datini.Enabled = True
    Me.tb_datini.FieldName = "tb_datini"
    Me.tb_datini.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_datini.Name = "tb_datini"
    Me.tb_datini.NTSRepositoryComboBox = Nothing
    Me.tb_datini.NTSRepositoryItemCheck = Nothing
    Me.tb_datini.NTSRepositoryItemMemo = Nothing
    Me.tb_datini.NTSRepositoryItemText = Nothing
    Me.tb_datini.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_datini.OptionsFilter.AllowFilter = False
    Me.tb_datini.Visible = True
    Me.tb_datini.VisibleIndex = 3
    '
    'tb_datfin
    '
    Me.tb_datfin.AppearanceCell.Options.UseBackColor = True
    Me.tb_datfin.AppearanceCell.Options.UseTextOptions = True
    Me.tb_datfin.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_datfin.Caption = "Data fine"
    Me.tb_datfin.Enabled = True
    Me.tb_datfin.FieldName = "tb_datfin"
    Me.tb_datfin.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_datfin.Name = "tb_datfin"
    Me.tb_datfin.NTSRepositoryComboBox = Nothing
    Me.tb_datfin.NTSRepositoryItemCheck = Nothing
    Me.tb_datfin.NTSRepositoryItemMemo = Nothing
    Me.tb_datfin.NTSRepositoryItemText = Nothing
    Me.tb_datfin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_datfin.OptionsFilter.AllowFilter = False
    Me.tb_datfin.Visible = True
    Me.tb_datfin.VisibleIndex = 4
    '
    'km_aammgg
    '
    Me.km_aammgg.AppearanceCell.Options.UseBackColor = True
    Me.km_aammgg.AppearanceCell.Options.UseTextOptions = True
    Me.km_aammgg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_aammgg.Caption = "Data doc"
    Me.km_aammgg.Enabled = True
    Me.km_aammgg.FieldName = "km_aammgg"
    Me.km_aammgg.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_aammgg.Name = "km_aammgg"
    Me.km_aammgg.NTSRepositoryComboBox = Nothing
    Me.km_aammgg.NTSRepositoryItemCheck = Nothing
    Me.km_aammgg.NTSRepositoryItemMemo = Nothing
    Me.km_aammgg.NTSRepositoryItemText = Nothing
    Me.km_aammgg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_aammgg.OptionsFilter.AllowFilter = False
    Me.km_aammgg.Visible = True
    Me.km_aammgg.VisibleIndex = 0
    Me.km_aammgg.Width = 70
    '
    'km_tipork
    '
    Me.km_tipork.AppearanceCell.Options.UseBackColor = True
    Me.km_tipork.AppearanceCell.Options.UseTextOptions = True
    Me.km_tipork.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_tipork.Caption = "Tipo doc"
    Me.km_tipork.Enabled = True
    Me.km_tipork.FieldName = "km_tipork"
    Me.km_tipork.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_tipork.Name = "km_tipork"
    Me.km_tipork.NTSRepositoryComboBox = Nothing
    Me.km_tipork.NTSRepositoryItemCheck = Nothing
    Me.km_tipork.NTSRepositoryItemMemo = Nothing
    Me.km_tipork.NTSRepositoryItemText = Nothing
    Me.km_tipork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_tipork.OptionsFilter.AllowFilter = False
    Me.km_tipork.Visible = True
    Me.km_tipork.VisibleIndex = 1
    Me.km_tipork.Width = 70
    '
    'km_serie
    '
    Me.km_serie.AppearanceCell.Options.UseBackColor = True
    Me.km_serie.AppearanceCell.Options.UseTextOptions = True
    Me.km_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_serie.Caption = "Serie doc"
    Me.km_serie.Enabled = True
    Me.km_serie.FieldName = "km_serie"
    Me.km_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_serie.Name = "km_serie"
    Me.km_serie.NTSRepositoryComboBox = Nothing
    Me.km_serie.NTSRepositoryItemCheck = Nothing
    Me.km_serie.NTSRepositoryItemMemo = Nothing
    Me.km_serie.NTSRepositoryItemText = Nothing
    Me.km_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_serie.OptionsFilter.AllowFilter = False
    Me.km_serie.Visible = True
    Me.km_serie.VisibleIndex = 2
    Me.km_serie.Width = 70
    '
    'km_numdoc
    '
    Me.km_numdoc.AppearanceCell.Options.UseBackColor = True
    Me.km_numdoc.AppearanceCell.Options.UseTextOptions = True
    Me.km_numdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_numdoc.Caption = "Num doc"
    Me.km_numdoc.Enabled = True
    Me.km_numdoc.FieldName = "km_numdoc"
    Me.km_numdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_numdoc.Name = "km_numdoc"
    Me.km_numdoc.NTSRepositoryComboBox = Nothing
    Me.km_numdoc.NTSRepositoryItemCheck = Nothing
    Me.km_numdoc.NTSRepositoryItemMemo = Nothing
    Me.km_numdoc.NTSRepositoryItemText = Nothing
    Me.km_numdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_numdoc.OptionsFilter.AllowFilter = False
    Me.km_numdoc.Visible = True
    Me.km_numdoc.VisibleIndex = 3
    Me.km_numdoc.Width = 70
    '
    'km_causale
    '
    Me.km_causale.AppearanceCell.Options.UseBackColor = True
    Me.km_causale.AppearanceCell.Options.UseTextOptions = True
    Me.km_causale.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_causale.Caption = "Causale"
    Me.km_causale.Enabled = True
    Me.km_causale.FieldName = "km_causale"
    Me.km_causale.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_causale.Name = "km_causale"
    Me.km_causale.NTSRepositoryComboBox = Nothing
    Me.km_causale.NTSRepositoryItemCheck = Nothing
    Me.km_causale.NTSRepositoryItemMemo = Nothing
    Me.km_causale.NTSRepositoryItemText = Nothing
    Me.km_causale.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_causale.OptionsFilter.AllowFilter = False
    Me.km_causale.Visible = True
    Me.km_causale.VisibleIndex = 4
    Me.km_causale.Width = 70
    '
    'tb_descaum
    '
    Me.tb_descaum.AppearanceCell.Options.UseBackColor = True
    Me.tb_descaum.AppearanceCell.Options.UseTextOptions = True
    Me.tb_descaum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_descaum.Caption = "Descr. causale"
    Me.tb_descaum.Enabled = True
    Me.tb_descaum.FieldName = "tb_descaum"
    Me.tb_descaum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_descaum.Name = "tb_descaum"
    Me.tb_descaum.NTSRepositoryComboBox = Nothing
    Me.tb_descaum.NTSRepositoryItemCheck = Nothing
    Me.tb_descaum.NTSRepositoryItemMemo = Nothing
    Me.tb_descaum.NTSRepositoryItemText = Nothing
    Me.tb_descaum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_descaum.OptionsFilter.AllowFilter = False
    Me.tb_descaum.Visible = True
    Me.tb_descaum.VisibleIndex = 5
    Me.tb_descaum.Width = 70
    '
    'tm_riferim
    '
    Me.tm_riferim.AppearanceCell.Options.UseBackColor = True
    Me.tm_riferim.AppearanceCell.Options.UseTextOptions = True
    Me.tm_riferim.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_riferim.Caption = "Riferimenti"
    Me.tm_riferim.Enabled = True
    Me.tm_riferim.FieldName = "tm_riferim"
    Me.tm_riferim.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_riferim.Name = "tm_riferim"
    Me.tm_riferim.NTSRepositoryComboBox = Nothing
    Me.tm_riferim.NTSRepositoryItemCheck = Nothing
    Me.tm_riferim.NTSRepositoryItemMemo = Nothing
    Me.tm_riferim.NTSRepositoryItemText = Nothing
    Me.tm_riferim.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_riferim.OptionsFilter.AllowFilter = False
    Me.tm_riferim.Visible = True
    Me.tm_riferim.VisibleIndex = 6
    Me.tm_riferim.Width = 70
    '
    'xx_scarichi
    '
    Me.xx_scarichi.AppearanceCell.Options.UseBackColor = True
    Me.xx_scarichi.AppearanceCell.Options.UseTextOptions = True
    Me.xx_scarichi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_scarichi.Caption = "Scarichi"
    Me.xx_scarichi.Enabled = True
    Me.xx_scarichi.FieldName = "xx_scarichi"
    Me.xx_scarichi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_scarichi.Name = "xx_scarichi"
    Me.xx_scarichi.NTSRepositoryComboBox = Nothing
    Me.xx_scarichi.NTSRepositoryItemCheck = Nothing
    Me.xx_scarichi.NTSRepositoryItemMemo = Nothing
    Me.xx_scarichi.NTSRepositoryItemText = Nothing
    Me.xx_scarichi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_scarichi.OptionsFilter.AllowFilter = False
    Me.xx_scarichi.Visible = True
    Me.xx_scarichi.VisibleIndex = 7
    Me.xx_scarichi.Width = 70
    '
    'xx_carichi
    '
    Me.xx_carichi.AppearanceCell.Options.UseBackColor = True
    Me.xx_carichi.AppearanceCell.Options.UseTextOptions = True
    Me.xx_carichi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_carichi.Caption = "Carichi"
    Me.xx_carichi.Enabled = True
    Me.xx_carichi.FieldName = "xx_carichi"
    Me.xx_carichi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_carichi.Name = "xx_carichi"
    Me.xx_carichi.NTSRepositoryComboBox = Nothing
    Me.xx_carichi.NTSRepositoryItemCheck = Nothing
    Me.xx_carichi.NTSRepositoryItemMemo = Nothing
    Me.xx_carichi.NTSRepositoryItemText = Nothing
    Me.xx_carichi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_carichi.OptionsFilter.AllowFilter = False
    Me.xx_carichi.Visible = True
    Me.xx_carichi.VisibleIndex = 8
    Me.xx_carichi.Width = 70
    '
    'xx_prezzo
    '
    Me.xx_prezzo.AppearanceCell.Options.UseBackColor = True
    Me.xx_prezzo.AppearanceCell.Options.UseTextOptions = True
    Me.xx_prezzo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_prezzo.Caption = "Prezzo netto"
    Me.xx_prezzo.Enabled = True
    Me.xx_prezzo.FieldName = "xx_prezzo"
    Me.xx_prezzo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_prezzo.Name = "xx_prezzo"
    Me.xx_prezzo.NTSRepositoryComboBox = Nothing
    Me.xx_prezzo.NTSRepositoryItemCheck = Nothing
    Me.xx_prezzo.NTSRepositoryItemMemo = Nothing
    Me.xx_prezzo.NTSRepositoryItemText = Nothing
    Me.xx_prezzo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_prezzo.OptionsFilter.AllowFilter = False
    Me.xx_prezzo.Visible = True
    Me.xx_prezzo.VisibleIndex = 9
    Me.xx_prezzo.Width = 70
    '
    'mm_valore
    '
    Me.mm_valore.AppearanceCell.Options.UseBackColor = True
    Me.mm_valore.AppearanceCell.Options.UseTextOptions = True
    Me.mm_valore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_valore.Caption = "Valore"
    Me.mm_valore.Enabled = True
    Me.mm_valore.FieldName = "mm_valore"
    Me.mm_valore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_valore.Name = "mm_valore"
    Me.mm_valore.NTSRepositoryComboBox = Nothing
    Me.mm_valore.NTSRepositoryItemCheck = Nothing
    Me.mm_valore.NTSRepositoryItemMemo = Nothing
    Me.mm_valore.NTSRepositoryItemText = Nothing
    Me.mm_valore.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_valore.OptionsFilter.AllowFilter = False
    Me.mm_valore.Visible = True
    Me.mm_valore.VisibleIndex = 10
    Me.mm_valore.Width = 70
    '
    'mm_quant
    '
    Me.mm_quant.AppearanceCell.Options.UseBackColor = True
    Me.mm_quant.AppearanceCell.Options.UseTextOptions = True
    Me.mm_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_quant.Caption = "Quant."
    Me.mm_quant.Enabled = True
    Me.mm_quant.FieldName = "mm_quant"
    Me.mm_quant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_quant.Name = "mm_quant"
    Me.mm_quant.NTSRepositoryComboBox = Nothing
    Me.mm_quant.NTSRepositoryItemCheck = Nothing
    Me.mm_quant.NTSRepositoryItemMemo = Nothing
    Me.mm_quant.NTSRepositoryItemText = Nothing
    Me.mm_quant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_quant.OptionsFilter.AllowFilter = False
    Me.mm_quant.Visible = True
    Me.mm_quant.VisibleIndex = 11
    Me.mm_quant.Width = 70
    '
    'mm_prelist
    '
    Me.mm_prelist.AppearanceCell.Options.UseBackColor = True
    Me.mm_prelist.AppearanceCell.Options.UseTextOptions = True
    Me.mm_prelist.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_prelist.Caption = "Prezzo list."
    Me.mm_prelist.Enabled = True
    Me.mm_prelist.FieldName = "mm_prelist"
    Me.mm_prelist.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_prelist.Name = "mm_prelist"
    Me.mm_prelist.NTSRepositoryComboBox = Nothing
    Me.mm_prelist.NTSRepositoryItemCheck = Nothing
    Me.mm_prelist.NTSRepositoryItemMemo = Nothing
    Me.mm_prelist.NTSRepositoryItemText = Nothing
    Me.mm_prelist.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_prelist.OptionsFilter.AllowFilter = False
    Me.mm_prelist.Visible = True
    Me.mm_prelist.VisibleIndex = 12
    Me.mm_prelist.Width = 70
    '
    'mm_prezzo
    '
    Me.mm_prezzo.AppearanceCell.Options.UseBackColor = True
    Me.mm_prezzo.AppearanceCell.Options.UseTextOptions = True
    Me.mm_prezzo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_prezzo.Caption = "Prezzo"
    Me.mm_prezzo.Enabled = True
    Me.mm_prezzo.FieldName = "mm_prezzo"
    Me.mm_prezzo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_prezzo.Name = "mm_prezzo"
    Me.mm_prezzo.NTSRepositoryComboBox = Nothing
    Me.mm_prezzo.NTSRepositoryItemCheck = Nothing
    Me.mm_prezzo.NTSRepositoryItemMemo = Nothing
    Me.mm_prezzo.NTSRepositoryItemText = Nothing
    Me.mm_prezzo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_prezzo.OptionsFilter.AllowFilter = False
    Me.mm_prezzo.Visible = True
    Me.mm_prezzo.VisibleIndex = 13
    Me.mm_prezzo.Width = 70
    '
    'mm_preziva
    '
    Me.mm_preziva.AppearanceCell.Options.UseBackColor = True
    Me.mm_preziva.AppearanceCell.Options.UseTextOptions = True
    Me.mm_preziva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_preziva.Caption = "Prezzo IVA comp."
    Me.mm_preziva.Enabled = True
    Me.mm_preziva.FieldName = "mm_preziva"
    Me.mm_preziva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_preziva.Name = "mm_preziva"
    Me.mm_preziva.NTSRepositoryComboBox = Nothing
    Me.mm_preziva.NTSRepositoryItemCheck = Nothing
    Me.mm_preziva.NTSRepositoryItemMemo = Nothing
    Me.mm_preziva.NTSRepositoryItemText = Nothing
    Me.mm_preziva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_preziva.OptionsFilter.AllowFilter = False
    Me.mm_preziva.Visible = True
    Me.mm_preziva.VisibleIndex = 14
    Me.mm_preziva.Width = 70
    '
    'mm_prezvalc
    '
    Me.mm_prezvalc.AppearanceCell.Options.UseBackColor = True
    Me.mm_prezvalc.AppearanceCell.Options.UseTextOptions = True
    Me.mm_prezvalc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_prezvalc.Caption = "Prezzo val."
    Me.mm_prezvalc.Enabled = True
    Me.mm_prezvalc.FieldName = "mm_prezvalc"
    Me.mm_prezvalc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_prezvalc.Name = "mm_prezvalc"
    Me.mm_prezvalc.NTSRepositoryComboBox = Nothing
    Me.mm_prezvalc.NTSRepositoryItemCheck = Nothing
    Me.mm_prezvalc.NTSRepositoryItemMemo = Nothing
    Me.mm_prezvalc.NTSRepositoryItemText = Nothing
    Me.mm_prezvalc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_prezvalc.OptionsFilter.AllowFilter = False
    Me.mm_prezvalc.Visible = True
    Me.mm_prezvalc.VisibleIndex = 15
    Me.mm_prezvalc.Width = 70
    '
    'mm_scont1
    '
    Me.mm_scont1.AppearanceCell.Options.UseBackColor = True
    Me.mm_scont1.AppearanceCell.Options.UseTextOptions = True
    Me.mm_scont1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_scont1.Caption = "Sconto 1"
    Me.mm_scont1.Enabled = True
    Me.mm_scont1.FieldName = "mm_scont1"
    Me.mm_scont1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_scont1.Name = "mm_scont1"
    Me.mm_scont1.NTSRepositoryComboBox = Nothing
    Me.mm_scont1.NTSRepositoryItemCheck = Nothing
    Me.mm_scont1.NTSRepositoryItemMemo = Nothing
    Me.mm_scont1.NTSRepositoryItemText = Nothing
    Me.mm_scont1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_scont1.OptionsFilter.AllowFilter = False
    Me.mm_scont1.Visible = True
    Me.mm_scont1.VisibleIndex = 16
    Me.mm_scont1.Width = 70
    '
    'mm_scont2
    '
    Me.mm_scont2.AppearanceCell.Options.UseBackColor = True
    Me.mm_scont2.AppearanceCell.Options.UseTextOptions = True
    Me.mm_scont2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_scont2.Caption = "Sconto 2"
    Me.mm_scont2.Enabled = True
    Me.mm_scont2.FieldName = "mm_scont2"
    Me.mm_scont2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_scont2.Name = "mm_scont2"
    Me.mm_scont2.NTSRepositoryComboBox = Nothing
    Me.mm_scont2.NTSRepositoryItemCheck = Nothing
    Me.mm_scont2.NTSRepositoryItemMemo = Nothing
    Me.mm_scont2.NTSRepositoryItemText = Nothing
    Me.mm_scont2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_scont2.OptionsFilter.AllowFilter = False
    Me.mm_scont2.Visible = True
    Me.mm_scont2.VisibleIndex = 17
    Me.mm_scont2.Width = 70
    '
    'xx_lottox
    '
    Me.xx_lottox.AppearanceCell.Options.UseBackColor = True
    Me.xx_lottox.AppearanceCell.Options.UseTextOptions = True
    Me.xx_lottox.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_lottox.Caption = "Lotto"
    Me.xx_lottox.Enabled = True
    Me.xx_lottox.FieldName = "xx_lottox"
    Me.xx_lottox.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_lottox.Name = "xx_lottox"
    Me.xx_lottox.NTSRepositoryComboBox = Nothing
    Me.xx_lottox.NTSRepositoryItemCheck = Nothing
    Me.xx_lottox.NTSRepositoryItemMemo = Nothing
    Me.xx_lottox.NTSRepositoryItemText = Nothing
    Me.xx_lottox.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_lottox.OptionsFilter.AllowFilter = False
    Me.xx_lottox.Visible = True
    Me.xx_lottox.VisibleIndex = 18
    Me.xx_lottox.Width = 70
    '
    'km_magaz
    '
    Me.km_magaz.AppearanceCell.Options.UseBackColor = True
    Me.km_magaz.AppearanceCell.Options.UseTextOptions = True
    Me.km_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_magaz.Caption = "Magaz"
    Me.km_magaz.Enabled = True
    Me.km_magaz.FieldName = "km_magaz"
    Me.km_magaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_magaz.Name = "km_magaz"
    Me.km_magaz.NTSRepositoryComboBox = Nothing
    Me.km_magaz.NTSRepositoryItemCheck = Nothing
    Me.km_magaz.NTSRepositoryItemMemo = Nothing
    Me.km_magaz.NTSRepositoryItemText = Nothing
    Me.km_magaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_magaz.OptionsFilter.AllowFilter = False
    Me.km_magaz.Visible = True
    Me.km_magaz.VisibleIndex = 19
    Me.km_magaz.Width = 70
    '
    'km_subcommeca
    '
    Me.km_subcommeca.AppearanceCell.Options.UseBackColor = True
    Me.km_subcommeca.AppearanceCell.Options.UseTextOptions = True
    Me.km_subcommeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_subcommeca.Caption = "SubCom"
    Me.km_subcommeca.Enabled = True
    Me.km_subcommeca.FieldName = "km_subcommeca"
    Me.km_subcommeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_subcommeca.Name = "km_subcommeca"
    Me.km_subcommeca.NTSRepositoryComboBox = Nothing
    Me.km_subcommeca.NTSRepositoryItemCheck = Nothing
    Me.km_subcommeca.NTSRepositoryItemMemo = Nothing
    Me.km_subcommeca.NTSRepositoryItemText = Nothing
    Me.km_subcommeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_subcommeca.OptionsFilter.AllowFilter = False
    Me.km_subcommeca.Visible = True
    Me.km_subcommeca.VisibleIndex = 20
    Me.km_subcommeca.Width = 70
    '
    'km_ubicaz
    '
    Me.km_ubicaz.AppearanceCell.Options.UseBackColor = True
    Me.km_ubicaz.AppearanceCell.Options.UseTextOptions = True
    Me.km_ubicaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.km_ubicaz.Caption = "Ubicaz."
    Me.km_ubicaz.Enabled = True
    Me.km_ubicaz.FieldName = "km_ubicaz"
    Me.km_ubicaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.km_ubicaz.Name = "km_ubicaz"
    Me.km_ubicaz.NTSRepositoryComboBox = Nothing
    Me.km_ubicaz.NTSRepositoryItemCheck = Nothing
    Me.km_ubicaz.NTSRepositoryItemMemo = Nothing
    Me.km_ubicaz.NTSRepositoryItemText = Nothing
    Me.km_ubicaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.km_ubicaz.OptionsFilter.AllowFilter = False
    Me.km_ubicaz.Visible = True
    Me.km_ubicaz.VisibleIndex = 21
    Me.km_ubicaz.Width = 70
    '
    'mm_codcena
    '
    Me.mm_codcena.AppearanceCell.Options.UseBackColor = True
    Me.mm_codcena.AppearanceCell.Options.UseTextOptions = True
    Me.mm_codcena.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_codcena.Caption = "Centro"
    Me.mm_codcena.Enabled = True
    Me.mm_codcena.FieldName = "mm_codcena"
    Me.mm_codcena.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_codcena.Name = "mm_codcena"
    Me.mm_codcena.NTSRepositoryComboBox = Nothing
    Me.mm_codcena.NTSRepositoryItemCheck = Nothing
    Me.mm_codcena.NTSRepositoryItemMemo = Nothing
    Me.mm_codcena.NTSRepositoryItemText = Nothing
    Me.mm_codcena.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_codcena.OptionsFilter.AllowFilter = False
    Me.mm_codcena.Visible = True
    Me.mm_codcena.VisibleIndex = 22
    Me.mm_codcena.Width = 70
    '
    'mm_codcfam
    '
    Me.mm_codcfam.AppearanceCell.Options.UseBackColor = True
    Me.mm_codcfam.AppearanceCell.Options.UseTextOptions = True
    Me.mm_codcfam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_codcfam.Caption = "Famiglia"
    Me.mm_codcfam.Enabled = True
    Me.mm_codcfam.FieldName = "mm_codcfam"
    Me.mm_codcfam.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_codcfam.Name = "mm_codcfam"
    Me.mm_codcfam.NTSRepositoryComboBox = Nothing
    Me.mm_codcfam.NTSRepositoryItemCheck = Nothing
    Me.mm_codcfam.NTSRepositoryItemMemo = Nothing
    Me.mm_codcfam.NTSRepositoryItemText = Nothing
    Me.mm_codcfam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_codcfam.OptionsFilter.AllowFilter = False
    Me.mm_codcfam.Visible = True
    Me.mm_codcfam.VisibleIndex = 23
    Me.mm_codcfam.Width = 70
    '
    'mm_codiva
    '
    Me.mm_codiva.AppearanceCell.Options.UseBackColor = True
    Me.mm_codiva.AppearanceCell.Options.UseTextOptions = True
    Me.mm_codiva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_codiva.Caption = "Cod. IVA"
    Me.mm_codiva.Enabled = True
    Me.mm_codiva.FieldName = "mm_codiva"
    Me.mm_codiva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_codiva.Name = "mm_codiva"
    Me.mm_codiva.NTSRepositoryComboBox = Nothing
    Me.mm_codiva.NTSRepositoryItemCheck = Nothing
    Me.mm_codiva.NTSRepositoryItemMemo = Nothing
    Me.mm_codiva.NTSRepositoryItemText = Nothing
    Me.mm_codiva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_codiva.OptionsFilter.AllowFilter = False
    Me.mm_codiva.Visible = True
    Me.mm_codiva.VisibleIndex = 24
    Me.mm_codiva.Width = 70
    '
    'mm_codnomc
    '
    Me.mm_codnomc.AppearanceCell.Options.UseBackColor = True
    Me.mm_codnomc.AppearanceCell.Options.UseTextOptions = True
    Me.mm_codnomc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_codnomc.Caption = "Cod. nom. comb."
    Me.mm_codnomc.Enabled = True
    Me.mm_codnomc.FieldName = "mm_codnomc"
    Me.mm_codnomc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_codnomc.Name = "mm_codnomc"
    Me.mm_codnomc.NTSRepositoryComboBox = Nothing
    Me.mm_codnomc.NTSRepositoryItemCheck = Nothing
    Me.mm_codnomc.NTSRepositoryItemMemo = Nothing
    Me.mm_codnomc.NTSRepositoryItemText = Nothing
    Me.mm_codnomc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_codnomc.OptionsFilter.AllowFilter = False
    Me.mm_codnomc.Visible = True
    Me.mm_codnomc.VisibleIndex = 25
    Me.mm_codnomc.Width = 70
    '
    'mm_colli
    '
    Me.mm_colli.AppearanceCell.Options.UseBackColor = True
    Me.mm_colli.AppearanceCell.Options.UseTextOptions = True
    Me.mm_colli.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_colli.Caption = "Colli"
    Me.mm_colli.Enabled = True
    Me.mm_colli.FieldName = "mm_colli"
    Me.mm_colli.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_colli.Name = "mm_colli"
    Me.mm_colli.NTSRepositoryComboBox = Nothing
    Me.mm_colli.NTSRepositoryItemCheck = Nothing
    Me.mm_colli.NTSRepositoryItemMemo = Nothing
    Me.mm_colli.NTSRepositoryItemText = Nothing
    Me.mm_colli.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_colli.OptionsFilter.AllowFilter = False
    Me.mm_colli.Visible = True
    Me.mm_colli.VisibleIndex = 26
    Me.mm_colli.Width = 70
    '
    'mm_commeca
    '
    Me.mm_commeca.AppearanceCell.Options.UseBackColor = True
    Me.mm_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.mm_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_commeca.Caption = "Commessa"
    Me.mm_commeca.Enabled = True
    Me.mm_commeca.FieldName = "mm_commeca"
    Me.mm_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_commeca.Name = "mm_commeca"
    Me.mm_commeca.NTSRepositoryComboBox = Nothing
    Me.mm_commeca.NTSRepositoryItemCheck = Nothing
    Me.mm_commeca.NTSRepositoryItemMemo = Nothing
    Me.mm_commeca.NTSRepositoryItemText = Nothing
    Me.mm_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_commeca.OptionsFilter.AllowFilter = False
    Me.mm_commeca.Visible = True
    Me.mm_commeca.VisibleIndex = 27
    Me.mm_commeca.Width = 70
    '
    'mm_controp
    '
    Me.mm_controp.AppearanceCell.Options.UseBackColor = True
    Me.mm_controp.AppearanceCell.Options.UseTextOptions = True
    Me.mm_controp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_controp.Caption = "Controp."
    Me.mm_controp.Enabled = True
    Me.mm_controp.FieldName = "mm_controp"
    Me.mm_controp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_controp.Name = "mm_controp"
    Me.mm_controp.NTSRepositoryComboBox = Nothing
    Me.mm_controp.NTSRepositoryItemCheck = Nothing
    Me.mm_controp.NTSRepositoryItemMemo = Nothing
    Me.mm_controp.NTSRepositoryItemText = Nothing
    Me.mm_controp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_controp.OptionsFilter.AllowFilter = False
    Me.mm_controp.Visible = True
    Me.mm_controp.VisibleIndex = 28
    Me.mm_controp.Width = 70
    '
    'mm_misura1
    '
    Me.mm_misura1.AppearanceCell.Options.UseBackColor = True
    Me.mm_misura1.AppearanceCell.Options.UseTextOptions = True
    Me.mm_misura1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_misura1.Caption = "Misura 1"
    Me.mm_misura1.Enabled = True
    Me.mm_misura1.FieldName = "mm_misura1"
    Me.mm_misura1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_misura1.Name = "mm_misura1"
    Me.mm_misura1.NTSRepositoryComboBox = Nothing
    Me.mm_misura1.NTSRepositoryItemCheck = Nothing
    Me.mm_misura1.NTSRepositoryItemMemo = Nothing
    Me.mm_misura1.NTSRepositoryItemText = Nothing
    Me.mm_misura1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_misura1.OptionsFilter.AllowFilter = False
    Me.mm_misura1.Visible = True
    Me.mm_misura1.VisibleIndex = 29
    Me.mm_misura1.Width = 70
    '
    'mm_misura2
    '
    Me.mm_misura2.AppearanceCell.Options.UseBackColor = True
    Me.mm_misura2.AppearanceCell.Options.UseTextOptions = True
    Me.mm_misura2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_misura2.Caption = "Misura 2"
    Me.mm_misura2.Enabled = True
    Me.mm_misura2.FieldName = "mm_misura2"
    Me.mm_misura2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_misura2.Name = "mm_misura2"
    Me.mm_misura2.NTSRepositoryComboBox = Nothing
    Me.mm_misura2.NTSRepositoryItemCheck = Nothing
    Me.mm_misura2.NTSRepositoryItemMemo = Nothing
    Me.mm_misura2.NTSRepositoryItemText = Nothing
    Me.mm_misura2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_misura2.OptionsFilter.AllowFilter = False
    Me.mm_misura2.Visible = True
    Me.mm_misura2.VisibleIndex = 30
    Me.mm_misura2.Width = 70
    '
    'mm_misura3
    '
    Me.mm_misura3.AppearanceCell.Options.UseBackColor = True
    Me.mm_misura3.AppearanceCell.Options.UseTextOptions = True
    Me.mm_misura3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_misura3.Caption = "Misura 3"
    Me.mm_misura3.Enabled = True
    Me.mm_misura3.FieldName = "mm_misura3"
    Me.mm_misura3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_misura3.Name = "mm_misura3"
    Me.mm_misura3.NTSRepositoryComboBox = Nothing
    Me.mm_misura3.NTSRepositoryItemCheck = Nothing
    Me.mm_misura3.NTSRepositoryItemMemo = Nothing
    Me.mm_misura3.NTSRepositoryItemText = Nothing
    Me.mm_misura3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_misura3.OptionsFilter.AllowFilter = False
    Me.mm_misura3.Visible = True
    Me.mm_misura3.VisibleIndex = 31
    Me.mm_misura3.Width = 70
    '
    'mm_ornum
    '
    Me.mm_ornum.AppearanceCell.Options.UseBackColor = True
    Me.mm_ornum.AppearanceCell.Options.UseTextOptions = True
    Me.mm_ornum.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_ornum.Caption = "Num. ord"
    Me.mm_ornum.Enabled = True
    Me.mm_ornum.FieldName = "mm_ornum"
    Me.mm_ornum.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_ornum.Name = "mm_ornum"
    Me.mm_ornum.NTSRepositoryComboBox = Nothing
    Me.mm_ornum.NTSRepositoryItemCheck = Nothing
    Me.mm_ornum.NTSRepositoryItemMemo = Nothing
    Me.mm_ornum.NTSRepositoryItemText = Nothing
    Me.mm_ornum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_ornum.OptionsFilter.AllowFilter = False
    Me.mm_ornum.Visible = True
    Me.mm_ornum.VisibleIndex = 32
    Me.mm_ornum.Width = 70
    '
    'mm_perqta
    '
    Me.mm_perqta.AppearanceCell.Options.UseBackColor = True
    Me.mm_perqta.AppearanceCell.Options.UseTextOptions = True
    Me.mm_perqta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_perqta.Caption = "Perqta"
    Me.mm_perqta.Enabled = True
    Me.mm_perqta.FieldName = "mm_perqta"
    Me.mm_perqta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_perqta.Name = "mm_perqta"
    Me.mm_perqta.NTSRepositoryComboBox = Nothing
    Me.mm_perqta.NTSRepositoryItemCheck = Nothing
    Me.mm_perqta.NTSRepositoryItemMemo = Nothing
    Me.mm_perqta.NTSRepositoryItemText = Nothing
    Me.mm_perqta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_perqta.OptionsFilter.AllowFilter = False
    Me.mm_perqta.Visible = True
    Me.mm_perqta.VisibleIndex = 33
    Me.mm_perqta.Width = 70
    '
    'mm_provv
    '
    Me.mm_provv.AppearanceCell.Options.UseBackColor = True
    Me.mm_provv.AppearanceCell.Options.UseTextOptions = True
    Me.mm_provv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_provv.Caption = "Provv."
    Me.mm_provv.Enabled = True
    Me.mm_provv.FieldName = "mm_provv"
    Me.mm_provv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_provv.Name = "mm_provv"
    Me.mm_provv.NTSRepositoryComboBox = Nothing
    Me.mm_provv.NTSRepositoryItemCheck = Nothing
    Me.mm_provv.NTSRepositoryItemMemo = Nothing
    Me.mm_provv.NTSRepositoryItemText = Nothing
    Me.mm_provv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_provv.OptionsFilter.AllowFilter = False
    Me.mm_provv.Visible = True
    Me.mm_provv.VisibleIndex = 34
    Me.mm_provv.Width = 70
    '
    'mm_provv2
    '
    Me.mm_provv2.AppearanceCell.Options.UseBackColor = True
    Me.mm_provv2.AppearanceCell.Options.UseTextOptions = True
    Me.mm_provv2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_provv2.Caption = "Provv. 2"
    Me.mm_provv2.Enabled = True
    Me.mm_provv2.FieldName = "mm_provv2"
    Me.mm_provv2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_provv2.Name = "mm_provv2"
    Me.mm_provv2.NTSRepositoryComboBox = Nothing
    Me.mm_provv2.NTSRepositoryItemCheck = Nothing
    Me.mm_provv2.NTSRepositoryItemMemo = Nothing
    Me.mm_provv2.NTSRepositoryItemText = Nothing
    Me.mm_provv2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_provv2.OptionsFilter.AllowFilter = False
    Me.mm_provv2.Visible = True
    Me.mm_provv2.VisibleIndex = 35
    Me.mm_provv2.Width = 70
    '
    'mm_scont3
    '
    Me.mm_scont3.AppearanceCell.Options.UseBackColor = True
    Me.mm_scont3.AppearanceCell.Options.UseTextOptions = True
    Me.mm_scont3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_scont3.Caption = "Sconto 3"
    Me.mm_scont3.Enabled = True
    Me.mm_scont3.FieldName = "mm_scont3"
    Me.mm_scont3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_scont3.Name = "mm_scont3"
    Me.mm_scont3.NTSRepositoryComboBox = Nothing
    Me.mm_scont3.NTSRepositoryItemCheck = Nothing
    Me.mm_scont3.NTSRepositoryItemMemo = Nothing
    Me.mm_scont3.NTSRepositoryItemText = Nothing
    Me.mm_scont3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_scont3.OptionsFilter.AllowFilter = False
    Me.mm_scont3.Visible = True
    Me.mm_scont3.VisibleIndex = 36
    Me.mm_scont3.Width = 70
    '
    'mm_scont4
    '
    Me.mm_scont4.AppearanceCell.Options.UseBackColor = True
    Me.mm_scont4.AppearanceCell.Options.UseTextOptions = True
    Me.mm_scont4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_scont4.Caption = "Sconto 4"
    Me.mm_scont4.Enabled = True
    Me.mm_scont4.FieldName = "mm_scont4"
    Me.mm_scont4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_scont4.Name = "mm_scont4"
    Me.mm_scont4.NTSRepositoryComboBox = Nothing
    Me.mm_scont4.NTSRepositoryItemCheck = Nothing
    Me.mm_scont4.NTSRepositoryItemMemo = Nothing
    Me.mm_scont4.NTSRepositoryItemText = Nothing
    Me.mm_scont4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_scont4.OptionsFilter.AllowFilter = False
    Me.mm_scont4.Visible = True
    Me.mm_scont4.VisibleIndex = 37
    Me.mm_scont4.Width = 70
    '
    'mm_scont5
    '
    Me.mm_scont5.AppearanceCell.Options.UseBackColor = True
    Me.mm_scont5.AppearanceCell.Options.UseTextOptions = True
    Me.mm_scont5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_scont5.Caption = "Sconto 5"
    Me.mm_scont5.Enabled = True
    Me.mm_scont5.FieldName = "mm_scont5"
    Me.mm_scont5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_scont5.Name = "mm_scont5"
    Me.mm_scont5.NTSRepositoryComboBox = Nothing
    Me.mm_scont5.NTSRepositoryItemCheck = Nothing
    Me.mm_scont5.NTSRepositoryItemMemo = Nothing
    Me.mm_scont5.NTSRepositoryItemText = Nothing
    Me.mm_scont5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_scont5.OptionsFilter.AllowFilter = False
    Me.mm_scont5.Visible = True
    Me.mm_scont5.VisibleIndex = 38
    Me.mm_scont5.Width = 70
    '
    'mm_scont6
    '
    Me.mm_scont6.AppearanceCell.Options.UseBackColor = True
    Me.mm_scont6.AppearanceCell.Options.UseTextOptions = True
    Me.mm_scont6.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_scont6.Caption = "Sconto 6"
    Me.mm_scont6.Enabled = True
    Me.mm_scont6.FieldName = "mm_scont6"
    Me.mm_scont6.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_scont6.Name = "mm_scont6"
    Me.mm_scont6.NTSRepositoryComboBox = Nothing
    Me.mm_scont6.NTSRepositoryItemCheck = Nothing
    Me.mm_scont6.NTSRepositoryItemMemo = Nothing
    Me.mm_scont6.NTSRepositoryItemText = Nothing
    Me.mm_scont6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_scont6.OptionsFilter.AllowFilter = False
    Me.mm_scont6.Visible = True
    Me.mm_scont6.VisibleIndex = 39
    Me.mm_scont6.Width = 70
    '
    'mm_vprovv
    '
    Me.mm_vprovv.AppearanceCell.Options.UseBackColor = True
    Me.mm_vprovv.AppearanceCell.Options.UseTextOptions = True
    Me.mm_vprovv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_vprovv.Caption = "Valore Provv"
    Me.mm_vprovv.Enabled = True
    Me.mm_vprovv.FieldName = "mm_vprovv"
    Me.mm_vprovv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_vprovv.Name = "mm_vprovv"
    Me.mm_vprovv.NTSRepositoryComboBox = Nothing
    Me.mm_vprovv.NTSRepositoryItemCheck = Nothing
    Me.mm_vprovv.NTSRepositoryItemMemo = Nothing
    Me.mm_vprovv.NTSRepositoryItemText = Nothing
    Me.mm_vprovv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_vprovv.OptionsFilter.AllowFilter = False
    Me.mm_vprovv.Visible = True
    Me.mm_vprovv.VisibleIndex = 40
    Me.mm_vprovv.Width = 70
    '
    'mm_vprovv2
    '
    Me.mm_vprovv2.AppearanceCell.Options.UseBackColor = True
    Me.mm_vprovv2.AppearanceCell.Options.UseTextOptions = True
    Me.mm_vprovv2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mm_vprovv2.Caption = "Valore Provv 2"
    Me.mm_vprovv2.Enabled = True
    Me.mm_vprovv2.FieldName = "mm_vprovv2"
    Me.mm_vprovv2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mm_vprovv2.Name = "mm_vprovv2"
    Me.mm_vprovv2.NTSRepositoryComboBox = Nothing
    Me.mm_vprovv2.NTSRepositoryItemCheck = Nothing
    Me.mm_vprovv2.NTSRepositoryItemMemo = Nothing
    Me.mm_vprovv2.NTSRepositoryItemText = Nothing
    Me.mm_vprovv2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mm_vprovv2.OptionsFilter.AllowFilter = False
    Me.mm_vprovv2.Visible = True
    Me.mm_vprovv2.VisibleIndex = 41
    Me.mm_vprovv2.Width = 70
    '
    'tm_valuta
    '
    Me.tm_valuta.AppearanceCell.Options.UseBackColor = True
    Me.tm_valuta.AppearanceCell.Options.UseTextOptions = True
    Me.tm_valuta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_valuta.Caption = "Valuta"
    Me.tm_valuta.Enabled = True
    Me.tm_valuta.FieldName = "tm_valuta"
    Me.tm_valuta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_valuta.Name = "tm_valuta"
    Me.tm_valuta.NTSRepositoryComboBox = Nothing
    Me.tm_valuta.NTSRepositoryItemCheck = Nothing
    Me.tm_valuta.NTSRepositoryItemMemo = Nothing
    Me.tm_valuta.NTSRepositoryItemText = Nothing
    Me.tm_valuta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_valuta.OptionsFilter.AllowFilter = False
    Me.tm_valuta.Visible = True
    Me.tm_valuta.VisibleIndex = 42
    Me.tm_valuta.Width = 70
    '
    'FRMMGLIST
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(774, 511)
    Me.Controls.Add(Me.pnGrid)
    Me.Controls.Add(Me.pnTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.MinimizeBox = False
    Me.Name = "FRMMGLIST"
    Me.Text = "ZOOM LISTINI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmValuta, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmValuta.ResumeLayout(False)
    Me.fmValuta.PerformLayout()
    CType(Me.edCodvalu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opValAltro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opValEur.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmValidita, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmValidita.ResumeLayout(False)
    Me.fmValidita.PerformLayout()
    CType(Me.edDtval.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opValDay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opValTutti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    CType(Me.grList, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvList, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmPromo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmPromo.ResumeLayout(False)
    CType(Me.grPromo, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvPromo, System.ComponentModel.ISupportInitialize).EndInit()
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

    '------------------------------------------------
    'creo e attivo l'entity e inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGGRSC", "BEMGDOCU", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 130086825685366925, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleDocu = CType(oTmp, CLEMGDOCU)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGGRSC", strRemoteServer, strRemotePort)
    AddHandler oCleDocu.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleDocu.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttPromo As New DataTable
    Try
      dttPromo.Columns.Add("cod", GetType(String))
      dttPromo.Columns.Add("val", GetType(String))
      dttPromo.Rows.Add(New Object() {"V", "Accumulo punti per valore"})
      dttPromo.Rows.Add(New Object() {"Q", "Accumulo punti per quantità"})
      dttPromo.Rows.Add(New Object() {"P", "Sconto di riga a percentuale"})
      dttPromo.Rows.Add(New Object() {"L", "Applica listino di riga"})
      dttPromo.Rows.Add(New Object() {"T", "Sconto di piede a percentuale"})
      dttPromo.Rows.Add(New Object() {"U", "Sconto di piede a valore"})
      dttPromo.Rows.Add(New Object() {"M", "MxN sullo stesso articolo"})
      dttPromo.Rows.Add(New Object() {"N", "MxN su articoli misti"})
      dttPromo.Rows.Add(New Object() {"F", "Articolo omaggio raggiunto un valore di spesa"})
      dttPromo.Rows.Add(New Object() {"A", "Articolo omaggio se acquistati determinati articoli"})
      dttPromo.Rows.Add(New Object() {"D", "Sconto percentuale differito"})
      dttPromo.Rows.Add(New Object() {"E", "Sconto a valore differito"})
      dttPromo.Rows.Add(New Object() {"O", "Articolo omaggio differito"})
      dttPromo.AcceptChanges()

      edCodart.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128570648697500000, "Codice articolo"), tabartico, False)
      edCodvalu.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128570648720937500, "Codice valuta"), tabvalu)
      grvList.NTSSetParam(oMenu, oApp.Tr(Me, 128448857602376000, "Griglia listini"))
      edCodvalu.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128448891664302000, "Valuta"), tabvalu)
      opValAltro.NTSSetParam(oMenu, oApp.Tr(Me, 128448891664458000, "Altro"), "A")
      opValEur.NTSSetParam(oMenu, oApp.Tr(Me, 128448891664614000, "Euro"), "E")
      edDtval.NTSSetParam(oMenu, oApp.Tr(Me, 128448891664770000, "Data validità listino"), False)
      opValDay.NTSSetParam(oMenu, oApp.Tr(Me, 128448891664926000, "Valido il"), "D")
      opValTutti.NTSSetParam(oMenu, oApp.Tr(Me, 128448891665082000, "Tutti"), "T")
      lc_codlavo.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128775260445636000, "Cod.lav."), tablavo)
      xx_codlavo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128775260509510000, "Descr.lav."), 0, True)
      lc_conto.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128775260867512000, "Cliente/forn."), tabanagra)
      xx_conto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128775260887012000, "Descrizione"), 0, True)
      lc_coddest.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129048472787388675, "Destinazione diversa"), tabdestdiv)
      lc_coddest.CliePerDestdiv = lc_conto
      xx_coddest.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130584635688954260, "Descrizione Destinazione diversa"), 0, True)
      lc_codvalu.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128775260904484000, "Valuta"), tabvalu)
      xx_codvalu.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128775261135756000, "Descr.valuta"), 0, True)
      lc_listino.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128775261150264000, "Listino"), tablist)
      lc_prezzo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128775261160872000, "Prezzo"), oApp.FormatPrzUn, 15)
      lc_daquant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128775261171636000, "Da quantità"), "#,##0", 15)
      lc_aquant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128775261182400000, "A quantità"), "#,##0", 15)
      lc_datagg.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128775261192384000, "Data aggiorn."), True, "D", NTSCDate(IntSetDate("01/01/1900")), NTSCDate(IntSetDate("31/12/2099")))
      lc_datscad.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128775261204708000, "Data scad."), True, "D", NTSCDate(IntSetDate("01/01/1900")), NTSCDate(IntSetDate("31/12/2099")))
      lc_codtpro.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128775261214224000, "Cod. promoz."), tabtpro)
      xx_codtpro.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128775261235128000, "Descr. promoz."), 0, True)
      lc_perqta.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128775261398262000, "Q.tà prezzo"), "#,##0", 15)
      lc_unmis.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128775261416514000, "U.M."), 3, False)
      lc_netto.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128775261427746000, "Prezzo netto"), "S", "N")
      lc_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128775261437574000, "Note"), 255, True)
      xx_listino.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128775261447246000, "Descr. listino"), 0, True)
      so_scont1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 131082051777963713, "Sconto 1"), oApp.FormatSconti, 15)
      so_scont2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 131082051778119970, "Sconto 2"), oApp.FormatSconti, 15)
      so_scont3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 131082051778276204, "Sconto 3"), oApp.FormatSconti, 15)
      so_scont4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 131082051778432457, "Sconto 4"), oApp.FormatSconti, 15)
      so_scont5.NTSSetParamNUM(oMenu, oApp.Tr(Me, 131082051778588719, "Sconto 5"), oApp.FormatSconti, 15)
      so_scont6.NTSSetParamNUM(oMenu, oApp.Tr(Me, 131082051778744977, "Sconto 6"), oApp.FormatSconti, 15)
      xx_prznetto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 131082051778901227, "Prezzo al netto di sconti"), oApp.FormatPrzUn, 15)

      grvList.NTSAllowDelete = False
      grvList.NTSAllowInsert = False
      grvList.Enabled = False

      grvPromo.NTSAllowDelete = False
      grvPromo.NTSAllowInsert = False
      grvPromo.NTSAllowUpdate = False
      grvPromo.Enabled = False

      grvPromo.NTSSetParam(oMenu, oApp.Tr(Me, 130657239143579004, "Promozioni"))
      tb_codrepr.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130657239143589029, "Cod."), "0", 9, 0, 999999999)
      tb_desrepr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130657239143599034, "Descr. Promozione"), 0, True)
      tb_tipopr.NTSSetParamCMB(oMenu, oApp.Tr(Me, 130657239143609047, "Tipo"), dttPromo, "val", "cod")
      tb_datini.NTSSetParamDATA(oMenu, oApp.Tr(Me, 130657239143619051, "Data Inizio"), True)
      tb_datfin.NTSSetParamDATA(oMenu, oApp.Tr(Me, 130657239143629056, "Data fine"), True)

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

#Region "Eventi di Form"
  Public Overridable Sub FRMMGLIST_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim bIsCRMUser As Boolean = False
    Dim bAmm As Boolean = False
    Dim strAccvis As String = ""
    Dim strAccmod As String = ""
    Dim strRegvis As String = ""
    Dim strRegmod As String = ""

    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      bModCCC = CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupCCC)

      If Not bModCCC Then fmPromo.Visible = False

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      'If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And CLN__STD.bsModExtCRM) Or _
      '   CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And CLN__STD.bsModSupWCR) Then
      ''disabilito il comando per vedere i movimenti se c'Ã¨ il modulo crm. querto perchÃ¨ in quella griglia non Ã¨ gestoto il CRM
      'cmdVismov.Enabled = False
      'End If

      bIsCRMUser = oMenu.IsCrmUser(DittaCorrente, bAmm, strAccvis, strAccmod, strRegvis, strRegmod)
      If bIsCRMUser = True Then cmdVismov.Visible = False : cmdVismov.Enabled = False

      GctlApplicaDefaultValue()

      If oApp.oGvar.strSconClCliDaList <> "S" Then
        so_scont1.Visible = False
        so_scont2.Visible = False
        so_scont3.Visible = False
        so_scont4.Visible = False
        so_scont5.Visible = False
        so_scont6.Visible = False
        xx_prznetto.Visible = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGLIST_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      bOnLoading = True
      nFase = NTSCInt(oCallParams.dPar1)
      edDtval.Text = oCallParams.strPar2
      strEscludiListini = oCallParams.strPar3.Replace(" ", "").Replace(";", ",").Trim(","c) 'tolgo spazi e virgole inutili
      opValEur.Checked = True
      If oCallParams.dPar5 <> 0 Then
        opValAltro.Checked = True
        edCodvalu.Text = oCallParams.dPar5.ToString
      End If
      oCallParams.dPar5 = 0 'poi dorvrò ritornare la valuta effettivamente selezionata
      edCodart.Text = oCallParams.strPar1
      edCodart_Validated(edCodart, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      bOnLoading = False
    End Try
  End Sub
  Public Overridable Sub FRMMGLIST_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcList.Dispose()
      dsList.Dispose()
    Catch
    End Try
  End Sub
#End Region

  Public Overridable Function ApriListini() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bOnLoading AndAlso edCodart.Text.Trim <> "" Then
        Dim dttTmp As New DataTable
        If oMenu.ValCodiceDb(edCodart.Text, DittaCorrente, "ARTICO", "S", "", dttTmp) = True Then
          lbCodart.Text = NTSCStr(dttTmp.Rows(0)!ar_descr)
          lForn1 = NTSCInt(dttTmp.Rows(0)!ar_forn)
          lForn2 = NTSCInt(dttTmp.Rows(0)!ar_forn2)
        End If
        dttTmp.Clear()
        dttTmp.Dispose()
      End If
      '--------------------------------------------------------------------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleDocu.ZoomListini(DittaCorrente, dsList, edCodart.Text, nFase, _
                                  NTSCInt(oCallParams.dPar3), opValEur.Checked, _
                                  NTSCInt(edCodvalu.Text), opValDay.Checked, _
                                  NTSCDate(edDtval.Text).ToShortDateString, lForn1, lForn2) Then
        Me.Close()
        Return False
      End If

      Me.Cursor = Cursors.WaitCursor
      dcList.DataSource = dsList.Tables("LISTINI")
      grList.DataSource = dcList

      If strEscludiListini <> "" Then
        For Each dtrRow As DataRow In dsList.Tables("LISTINI").Select("lc_listino IN (" & strEscludiListini & ")")
          dtrRow.Delete()
        Next
      End If
      dsList.AcceptChanges()

      'Carico i dati della griglia promozioni
      ApriPromozioni()

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Function


  Public Overridable Function ApriPromozioni() As Boolean
    Dim dttPromo As DataTable = Nothing
    Try
      'Se non è presente il modulo CCC non devo fare nulla
      If Not bModCCC Then Return True

      If Not oCleDocu.TrovaPromozioniPerArticoloCliente(DittaCorrente, edCodart.Text, NTSCInt(oCallParams.dPar3), opValDay.Checked, edDtval.TextData, _
                                                        dttPromo) Then Return False

      dcPromo = New BindingSource
      dcPromo.DataSource = dttPromo
      grPromo.DataSource = dcPromo

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function


#Region "Per far ricaricare i listini al cambio di data validità/valuta/articolo/..."
  Public Overridable Sub opValDay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opValDay.CheckedChanged
    Try
      If opValDay.Checked Then
        GctlSetVisEnab(edDtval, False)
      Else
        edDtval.Enabled = False
      End If
      If oCleDocu Is Nothing Then Return
      If edCodart.Text.Trim <> "" Then ApriListini()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub opValAltro_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opValAltro.CheckedChanged
    Try
      If opValAltro.Checked Then
        GctlSetVisEnab(edCodvalu, False)
        GctlSetVisEnab(lc_codvalu, True)
        GctlSetVisEnab(xx_codvalu, True)
        lc_prezzo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128775261464094000, "Prezzo"), oApp.FormatPrzUnVal, 15)
      Else
        edCodvalu.Enabled = False
        lc_codvalu.Visible = False
        xx_codvalu.Visible = False
        lc_prezzo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128775261475326000, "Prezzo"), oApp.FormatPrzUn, 15)
      End If
      If oCleDocu Is Nothing Then Return
      If edCodart.Text.Trim <> "" Then ApriListini()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edDtval_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDtval.Validated
    Try
      If bOnLoading = True Then
        If edDtval.Text = edDtval.OldEditValue.ToString Then Return

        If oCleDocu Is Nothing Then Return
      End If

      If edCodart.Text.Trim <> "" Then ApriListini()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edCodvalu_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodvalu.Validated
    Dim strTmp As String = ""
    Try
      If bOnLoading = True Then
        If edCodvalu.Text = NTSCStr(edCodvalu.OldEditValue) Then Return
        If oCleDocu Is Nothing Then Return

        If Not oCleDocu.ZoomListini_edCodvalu_Validated(NTSCInt(edCodvalu.Text), strTmp) Then
          edCodvalu.Text = "0"
          lbCodvalu.Text = ""
          Return
        Else
          lbCodvalu.Text = strTmp
        End If
      End If

      If edCodart.Text.Trim <> "" Then ApriListini()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edCodart_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodart.Validated
    Dim strTmp As String = ""
    Try
      If bOnLoading = False Then
        If edCodart.Text = NTSCStr(edCodart.OldEditValue) Then Return
        If oCleDocu Is Nothing Then Return

        If Not oCleDocu.ZoomListini_edCodart_Validated(edCodart.Text, strTmp, nFase, lForn1, lForn2) Then
          edCodart.Text = "D"
          lbCodart.Text = ""
          nFase = 0
          lForn1 = 0
          lForn2 = 0
          Return
        Else
          lbCodart.Text = strTmp
          If nFase <> 0 Then lbCodart.Text += oApp.Tr(Me, 128570658929843750, "Fase: ") & nFase.ToString
        End If
      End If

      If edCodart.Text.Trim <> "" Then ApriListini()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

  Public Overridable Sub cmdEsci_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEsci.Click
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Try
      If dsList.Tables.Count > 0 Then
        If dsList.Tables("LISTINI").Rows.Count > 0 Then
          With grvList.NTSGetCurrentDataRow
            oCallParams.dPar4 = NTSCDec(grvList.NTSGetCurrentDataRow!lc_prezzo)
            oCallParams.dPar5 = NTSCDec(grvList.NTSGetCurrentDataRow!lc_codvalu)
            oCallParams.ctlPar1 = grvList.NTSGetCurrentDataRow
          End With
        End If
      End If
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdVismov_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdVismov.Click
    Dim oPar As CLE__CLDP = Nothing
    Try
      'strParam = "APRI;" & edCodart.Text.PadLeft(18) & ";0000;" & "".PadLeft(9, CChar("0")) & ";C"
      'oMenu.RunChild("BSMGSCHE", "CLSMGSCHE", "", DittaCorrente, "", "", Nothing, strParam, True, True)

      '----------------------
      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BNMGLIST"
      oPar.strPar1 = edCodart.Text
      oPar.dPar1 = nFase
      oPar.dPar2 = 0
      oPar.dPar3 = 0
      oPar.strPar2 = ""                   'VALORI DI RITORNO
      oMenu.RunChild("NTSInformatica", "FRMMGGRSC", "", DittaCorrente, "", "BNMGDOCU", oPar, "", True, True)

      grList.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub grList_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grList.MouseDoubleClick
    cmdSeleziona_Click(Me, Nothing)
  End Sub

End Class
