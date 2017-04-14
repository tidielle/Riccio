Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVESEFD_F

#Region "Variabili"
  Public oCallParams As CLE__CLDP
  Public bOk As Boolean = False
  Public oCleFadi As CLEVEFADI
  Public bRielabora As Boolean = False
  Public strDtNewFatt As String = ""
  Public nRielabFirstNumdoc As Integer = 0
  Public strRielabTipork As String = ""
  'valori di ritorno
  Public strOutTestmag As String = ""
  Public strOutAnagra As String = ""
  Public strOutLista As String = ""
  Public lOutNumdocDa As Integer = 0
  Public lOutNumdocA As Integer = 0
  Public strOutSerieDa As String = ""
  Public strOutSerieA As String = ""
  'Nuova lista selezionata da generare
  Public nCodiceNuovaListaSel As Integer = 0
  Public strDescrNuovaListaSel As String = ""

  Public strTipobf As String = "2"
#End Region

#Region "Controlli"
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents pnFiltri As NTSInformatica.NTSPanel
  Public WithEvents fmDatiDocumento As NTSInformatica.NTSGroupBox
  Public WithEvents fmFiltri As NTSInformatica.NTSGroupBox
  Public WithEvents edDatadoc As NTSInformatica.NTSTextBoxData
  Public WithEvents edAnnodoc As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbTipodoc As NTSInformatica.NTSComboBox
  Public WithEvents lbNumdoc As NTSInformatica.NTSLabel
  Public WithEvents lbAnnoSeriedoc As NTSInformatica.NTSLabel
  Public WithEvents lbTipodoc As NTSInformatica.NTSLabel
  Public WithEvents edSeriedoc As NTSInformatica.NTSTextBoxStr
  Public WithEvents edNumdoc As NTSInformatica.NTSTextBoxNum
  Public WithEvents edNoteFattura As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbNoteFattura As NTSInformatica.NTSLabel
  Public WithEvents ckRiepilogatutto As NTSInformatica.NTSCheckBox
  Public WithEvents edClientea As NTSInformatica.NTSTextBoxNum
  Public WithEvents edClienteda As NTSInformatica.NTSTextBoxNum
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents lbPeriodofatt As NTSInformatica.NTSLabel
  Public WithEvents cbPeriodofatt As NTSInformatica.NTSComboBox
  Public WithEvents lbCheckrighe As NTSInformatica.NTSLabel
  Public WithEvents cbCheckrighe As NTSInformatica.NTSComboBox
  Public WithEvents edAnnoddt As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckNumdoc As NTSInformatica.NTSCheckBox
  Public WithEvents edSeriedocda As NTSInformatica.NTSTextBoxStr
  Public WithEvents edSeriedoca As NTSInformatica.NTSTextBoxStr
  Public WithEvents edNumdoca As NTSInformatica.NTSTextBoxNum
  Public WithEvents edNumdocda As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDatadoca As NTSInformatica.NTSTextBoxData
  Public WithEvents edDatadocda As NTSInformatica.NTSTextBoxData
  Public WithEvents ckProva As NTSInformatica.NTSCheckBox
  'Nuova lista selezionata da generare
  Public WithEvents ckDatdoc As NTSInformatica.NTSCheckBox
  Public WithEvents lbAnno As NTSInformatica.NTSLabel
  Public WithEvents cmdLoadRecent As NTSInformatica.NTSButton
  Public WithEvents ckVisDDT As NTSInformatica.NTSCheckBox
#End Region

  Public Overridable Sub InitializeComponent()
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.pnFiltri = New NTSInformatica.NTSPanel
    Me.ckVisDDT = New NTSInformatica.NTSCheckBox
    Me.lbCheckrighe = New NTSInformatica.NTSLabel
    Me.edNoteFattura = New NTSInformatica.NTSTextBoxStr
    Me.cbCheckrighe = New NTSInformatica.NTSComboBox
    Me.lbNoteFattura = New NTSInformatica.NTSLabel
    Me.fmFiltri = New NTSInformatica.NTSGroupBox
    Me.lbClienteda = New NTSInformatica.NTSLabel
    Me.ckDatdoc = New NTSInformatica.NTSCheckBox
    Me.lbAnno = New NTSInformatica.NTSLabel
    Me.edAnnoddt = New NTSInformatica.NTSTextBoxNum
    Me.ckNumdoc = New NTSInformatica.NTSCheckBox
    Me.edSeriedocda = New NTSInformatica.NTSTextBoxStr
    Me.edSeriedoca = New NTSInformatica.NTSTextBoxStr
    Me.edNumdoca = New NTSInformatica.NTSTextBoxNum
    Me.edNumdocda = New NTSInformatica.NTSTextBoxNum
    Me.edDatadoca = New NTSInformatica.NTSTextBoxData
    Me.edDatadocda = New NTSInformatica.NTSTextBoxData
    Me.lbPeriodofatt = New NTSInformatica.NTSLabel
    Me.cbPeriodofatt = New NTSInformatica.NTSComboBox
    Me.edClientea = New NTSInformatica.NTSTextBoxNum
    Me.edClienteda = New NTSInformatica.NTSTextBoxNum
    Me.ckRiepilogatutto = New NTSInformatica.NTSCheckBox
    Me.fmDatiDocumento = New NTSInformatica.NTSGroupBox
    Me.edSeriedoc = New NTSInformatica.NTSTextBoxStr
    Me.edNumdoc = New NTSInformatica.NTSTextBoxNum
    Me.edDatadoc = New NTSInformatica.NTSTextBoxData
    Me.edAnnodoc = New NTSInformatica.NTSTextBoxNum
    Me.cbTipodoc = New NTSInformatica.NTSComboBox
    Me.lbNumdoc = New NTSInformatica.NTSLabel
    Me.lbAnnoSeriedoc = New NTSInformatica.NTSLabel
    Me.lbTipodoc = New NTSInformatica.NTSLabel
    Me.ckProva = New NTSInformatica.NTSCheckBox
    Me.cmdLoadRecent = New NTSInformatica.NTSButton
    CType(Me.pnFiltri, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFiltri.SuspendLayout()
    CType(Me.ckVisDDT.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNoteFattura.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbCheckrighe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmFiltri, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmFiltri.SuspendLayout()
    CType(Me.ckDatdoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnnoddt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNumdoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSeriedocda.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSeriedoca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumdoca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumdocda.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatadoca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatadocda.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPeriodofatt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edClientea.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edClienteda.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckRiepilogatutto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmDatiDocumento, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmDatiDocumento.SuspendLayout()
    CType(Me.edSeriedoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumdoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatadoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnnodoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipodoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckProva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.cmdAnnulla.Location = New System.Drawing.Point(465, 36)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(114, 26)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(465, 7)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(114, 26)
    Me.cmdOk.TabIndex = 2
    Me.cmdOk.Text = "&Conferma"
    '
    'pnFiltri
    '
    Me.pnFiltri.AllowDrop = True
    Me.pnFiltri.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnFiltri.Appearance.Options.UseBackColor = True
    Me.pnFiltri.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnFiltri.Controls.Add(Me.ckVisDDT)
    Me.pnFiltri.Controls.Add(Me.lbCheckrighe)
    Me.pnFiltri.Controls.Add(Me.edNoteFattura)
    Me.pnFiltri.Controls.Add(Me.cbCheckrighe)
    Me.pnFiltri.Controls.Add(Me.lbNoteFattura)
    Me.pnFiltri.Controls.Add(Me.fmFiltri)
    Me.pnFiltri.Controls.Add(Me.fmDatiDocumento)
    Me.pnFiltri.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnFiltri.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnFiltri.Location = New System.Drawing.Point(0, 0)
    Me.pnFiltri.Name = "pnFiltri"
    Me.pnFiltri.NTSActiveTrasparency = True
    Me.pnFiltri.Size = New System.Drawing.Size(459, 311)
    Me.pnFiltri.TabIndex = 3
    Me.pnFiltri.Text = "NtsPanel1"
    '
    'ckVisDDT
    '
    Me.ckVisDDT.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckVisDDT.Location = New System.Drawing.Point(9, 148)
    Me.ckVisDDT.Name = "ckVisDDT"
    Me.ckVisDDT.NTSCheckValue = "S"
    Me.ckVisDDT.NTSUnCheckValue = "N"
    Me.ckVisDDT.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckVisDDT.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.ckVisDDT.Properties.Appearance.Options.UseBackColor = True
    Me.ckVisDDT.Properties.Appearance.Options.UseFont = True
    Me.ckVisDDT.Properties.Caption = "Visualizza estremi DDT prima dell'elaborazione"
    Me.ckVisDDT.Size = New System.Drawing.Size(251, 19)
    Me.ckVisDDT.TabIndex = 40
    '
    'lbCheckrighe
    '
    Me.lbCheckrighe.AutoSize = True
    Me.lbCheckrighe.BackColor = System.Drawing.Color.Transparent
    Me.lbCheckrighe.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbCheckrighe.Location = New System.Drawing.Point(10, 129)
    Me.lbCheckrighe.Name = "lbCheckrighe"
    Me.lbCheckrighe.NTSDbField = ""
    Me.lbCheckrighe.Size = New System.Drawing.Size(161, 13)
    Me.lbCheckrighe.TabIndex = 39
    Me.lbCheckrighe.Text = "Controlla righe con prezzi a zero"
    Me.lbCheckrighe.Tooltip = ""
    Me.lbCheckrighe.UseMnemonic = False
    '
    'edNoteFattura
    '
    Me.edNoteFattura.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNoteFattura.Location = New System.Drawing.Point(186, 104)
    Me.edNoteFattura.Name = "edNoteFattura"
    Me.edNoteFattura.NTSDbField = ""
    Me.edNoteFattura.NTSForzaVisZoom = False
    Me.edNoteFattura.NTSOldValue = ""
    Me.edNoteFattura.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNoteFattura.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNoteFattura.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNoteFattura.Properties.MaxLength = 65536
    Me.edNoteFattura.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNoteFattura.Size = New System.Drawing.Size(262, 20)
    Me.edNoteFattura.TabIndex = 3
    '
    'cbCheckrighe
    '
    Me.cbCheckrighe.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbCheckrighe.DataSource = Nothing
    Me.cbCheckrighe.DisplayMember = ""
    Me.cbCheckrighe.Location = New System.Drawing.Point(186, 126)
    Me.cbCheckrighe.Name = "cbCheckrighe"
    Me.cbCheckrighe.NTSDbField = ""
    Me.cbCheckrighe.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbCheckrighe.Properties.DropDownRows = 30
    Me.cbCheckrighe.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbCheckrighe.SelectedValue = ""
    Me.cbCheckrighe.Size = New System.Drawing.Size(262, 20)
    Me.cbCheckrighe.TabIndex = 38
    Me.cbCheckrighe.ValueMember = ""
    '
    'lbNoteFattura
    '
    Me.lbNoteFattura.AutoSize = True
    Me.lbNoteFattura.BackColor = System.Drawing.Color.Transparent
    Me.lbNoteFattura.Location = New System.Drawing.Point(10, 107)
    Me.lbNoteFattura.Name = "lbNoteFattura"
    Me.lbNoteFattura.NTSDbField = ""
    Me.lbNoteFattura.Size = New System.Drawing.Size(67, 13)
    Me.lbNoteFattura.TabIndex = 2
    Me.lbNoteFattura.Text = "Note fattura"
    Me.lbNoteFattura.Tooltip = ""
    Me.lbNoteFattura.UseMnemonic = False
    '
    'fmFiltri
    '
    Me.fmFiltri.AllowDrop = True
    Me.fmFiltri.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmFiltri.Appearance.Options.UseBackColor = True
    Me.fmFiltri.Controls.Add(Me.lbClienteda)
    Me.fmFiltri.Controls.Add(Me.ckDatdoc)
    Me.fmFiltri.Controls.Add(Me.lbAnno)
    Me.fmFiltri.Controls.Add(Me.edAnnoddt)
    Me.fmFiltri.Controls.Add(Me.ckNumdoc)
    Me.fmFiltri.Controls.Add(Me.edSeriedocda)
    Me.fmFiltri.Controls.Add(Me.edSeriedoca)
    Me.fmFiltri.Controls.Add(Me.edNumdoca)
    Me.fmFiltri.Controls.Add(Me.edNumdocda)
    Me.fmFiltri.Controls.Add(Me.edDatadoca)
    Me.fmFiltri.Controls.Add(Me.edDatadocda)
    Me.fmFiltri.Controls.Add(Me.lbPeriodofatt)
    Me.fmFiltri.Controls.Add(Me.cbPeriodofatt)
    Me.fmFiltri.Controls.Add(Me.edClientea)
    Me.fmFiltri.Controls.Add(Me.edClienteda)
    Me.fmFiltri.Controls.Add(Me.ckRiepilogatutto)
    Me.fmFiltri.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmFiltri.Location = New System.Drawing.Point(3, 173)
    Me.fmFiltri.Name = "fmFiltri"
    Me.fmFiltri.Size = New System.Drawing.Size(453, 135)
    Me.fmFiltri.TabIndex = 1
    Me.fmFiltri.Text = "Filtri documenti da riepilogare"
    '
    'lbClienteda
    '
    Me.lbClienteda.AutoSize = True
    Me.lbClienteda.BackColor = System.Drawing.Color.Transparent
    Me.lbClienteda.Location = New System.Drawing.Point(7, 91)
    Me.lbClienteda.Name = "lbClienteda"
    Me.lbClienteda.NTSDbField = ""
    Me.lbClienteda.Size = New System.Drawing.Size(69, 13)
    Me.lbClienteda.TabIndex = 44
    Me.lbClienteda.Text = "Dal/Al cliente"
    Me.lbClienteda.Tooltip = ""
    Me.lbClienteda.UseMnemonic = False
    '
    'ckDatdoc
    '
    Me.ckDatdoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDatdoc.Location = New System.Drawing.Point(6, 45)
    Me.ckDatdoc.Name = "ckDatdoc"
    Me.ckDatdoc.NTSCheckValue = "S"
    Me.ckDatdoc.NTSUnCheckValue = "N"
    Me.ckDatdoc.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDatdoc.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.ckDatdoc.Properties.Appearance.Options.UseBackColor = True
    Me.ckDatdoc.Properties.Appearance.Options.UseFont = True
    Me.ckDatdoc.Properties.Caption = "Da/A data documento"
    Me.ckDatdoc.Size = New System.Drawing.Size(158, 19)
    Me.ckDatdoc.TabIndex = 43
    '
    'lbAnno
    '
    Me.lbAnno.AutoSize = True
    Me.lbAnno.BackColor = System.Drawing.Color.Transparent
    Me.lbAnno.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAnno.Location = New System.Drawing.Point(340, 25)
    Me.lbAnno.Name = "lbAnno"
    Me.lbAnno.NTSDbField = ""
    Me.lbAnno.Size = New System.Drawing.Size(36, 13)
    Me.lbAnno.TabIndex = 42
    Me.lbAnno.Text = "Anno"
    Me.lbAnno.Tooltip = ""
    Me.lbAnno.UseMnemonic = False
    '
    'edAnnoddt
    '
    Me.edAnnoddt.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAnnoddt.EditValue = "2008"
    Me.edAnnoddt.Location = New System.Drawing.Point(386, 22)
    Me.edAnnoddt.Name = "edAnnoddt"
    Me.edAnnoddt.NTSDbField = ""
    Me.edAnnoddt.NTSFormat = "0"
    Me.edAnnoddt.NTSForzaVisZoom = False
    Me.edAnnoddt.NTSOldValue = "2008"
    Me.edAnnoddt.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnnoddt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnnoddt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnnoddt.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnnoddt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnnoddt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnnoddt.Size = New System.Drawing.Size(57, 20)
    Me.edAnnoddt.TabIndex = 38
    '
    'ckNumdoc
    '
    Me.ckNumdoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckNumdoc.Location = New System.Drawing.Point(5, 67)
    Me.ckNumdoc.Name = "ckNumdoc"
    Me.ckNumdoc.NTSCheckValue = "S"
    Me.ckNumdoc.NTSUnCheckValue = "N"
    Me.ckNumdoc.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNumdoc.Properties.Appearance.Options.UseBackColor = True
    Me.ckNumdoc.Properties.Caption = "Filtra numero/serie documento"
    Me.ckNumdoc.Size = New System.Drawing.Size(175, 19)
    Me.ckNumdoc.TabIndex = 37
    '
    'edSeriedocda
    '
    Me.edSeriedocda.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSeriedocda.EditValue = " "
    Me.edSeriedocda.Location = New System.Drawing.Point(261, 66)
    Me.edSeriedocda.Name = "edSeriedocda"
    Me.edSeriedocda.NTSDbField = ""
    Me.edSeriedocda.NTSForzaVisZoom = False
    Me.edSeriedocda.NTSOldValue = " "
    Me.edSeriedocda.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSeriedocda.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSeriedocda.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSeriedocda.Properties.MaxLength = 65536
    Me.edSeriedocda.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSeriedocda.Size = New System.Drawing.Size(45, 20)
    Me.edSeriedocda.TabIndex = 36
    '
    'edSeriedoca
    '
    Me.edSeriedoca.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSeriedoca.EditValue = "Z"
    Me.edSeriedoca.Location = New System.Drawing.Point(398, 66)
    Me.edSeriedoca.Name = "edSeriedoca"
    Me.edSeriedoca.NTSDbField = ""
    Me.edSeriedoca.NTSForzaVisZoom = False
    Me.edSeriedoca.NTSOldValue = "Z"
    Me.edSeriedoca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSeriedoca.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSeriedoca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSeriedoca.Properties.MaxLength = 65536
    Me.edSeriedoca.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSeriedoca.Size = New System.Drawing.Size(45, 20)
    Me.edSeriedoca.TabIndex = 35
    '
    'edNumdoca
    '
    Me.edNumdoca.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumdoca.EditValue = "999999999"
    Me.edNumdoca.Location = New System.Drawing.Point(316, 66)
    Me.edNumdoca.Name = "edNumdoca"
    Me.edNumdoca.NTSDbField = ""
    Me.edNumdoca.NTSFormat = "0"
    Me.edNumdoca.NTSForzaVisZoom = False
    Me.edNumdoca.NTSOldValue = "999999999"
    Me.edNumdoca.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumdoca.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumdoca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNumdoca.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNumdoca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumdoca.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumdoca.Size = New System.Drawing.Size(82, 20)
    Me.edNumdoca.TabIndex = 34
    '
    'edNumdocda
    '
    Me.edNumdocda.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumdocda.EditValue = "0"
    Me.edNumdocda.Location = New System.Drawing.Point(182, 66)
    Me.edNumdocda.Name = "edNumdocda"
    Me.edNumdocda.NTSDbField = ""
    Me.edNumdocda.NTSFormat = "0"
    Me.edNumdocda.NTSForzaVisZoom = False
    Me.edNumdocda.NTSOldValue = ""
    Me.edNumdocda.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumdocda.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumdocda.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNumdocda.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNumdocda.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumdocda.Properties.MaxLength = 65536
    Me.edNumdocda.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumdocda.Size = New System.Drawing.Size(79, 20)
    Me.edNumdocda.TabIndex = 33
    '
    'edDatadoca
    '
    Me.edDatadoca.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatadoca.EditValue = "31/12/2099"
    Me.edDatadoca.Location = New System.Drawing.Point(343, 44)
    Me.edDatadoca.Name = "edDatadoca"
    Me.edDatadoca.NTSDbField = ""
    Me.edDatadoca.NTSForzaVisZoom = False
    Me.edDatadoca.NTSOldValue = ""
    Me.edDatadoca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatadoca.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatadoca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatadoca.Properties.MaxLength = 65536
    Me.edDatadoca.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatadoca.Size = New System.Drawing.Size(100, 20)
    Me.edDatadoca.TabIndex = 32
    '
    'edDatadocda
    '
    Me.edDatadocda.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatadocda.EditValue = "01/01/1900"
    Me.edDatadocda.Location = New System.Drawing.Point(182, 44)
    Me.edDatadocda.Name = "edDatadocda"
    Me.edDatadocda.NTSDbField = ""
    Me.edDatadocda.NTSForzaVisZoom = False
    Me.edDatadocda.NTSOldValue = ""
    Me.edDatadocda.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatadocda.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatadocda.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatadocda.Properties.MaxLength = 65536
    Me.edDatadocda.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatadocda.Size = New System.Drawing.Size(100, 20)
    Me.edDatadocda.TabIndex = 31
    '
    'lbPeriodofatt
    '
    Me.lbPeriodofatt.AutoSize = True
    Me.lbPeriodofatt.BackColor = System.Drawing.Color.Transparent
    Me.lbPeriodofatt.Location = New System.Drawing.Point(7, 113)
    Me.lbPeriodofatt.Name = "lbPeriodofatt"
    Me.lbPeriodofatt.NTSDbField = ""
    Me.lbPeriodofatt.Size = New System.Drawing.Size(116, 13)
    Me.lbPeriodofatt.TabIndex = 29
    Me.lbPeriodofatt.Text = "Periodo di fatturazione"
    Me.lbPeriodofatt.Tooltip = ""
    Me.lbPeriodofatt.UseMnemonic = False
    '
    'cbPeriodofatt
    '
    Me.cbPeriodofatt.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPeriodofatt.DataSource = Nothing
    Me.cbPeriodofatt.DisplayMember = ""
    Me.cbPeriodofatt.Location = New System.Drawing.Point(182, 110)
    Me.cbPeriodofatt.Name = "cbPeriodofatt"
    Me.cbPeriodofatt.NTSDbField = ""
    Me.cbPeriodofatt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPeriodofatt.Properties.DropDownRows = 30
    Me.cbPeriodofatt.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPeriodofatt.SelectedValue = ""
    Me.cbPeriodofatt.Size = New System.Drawing.Size(263, 20)
    Me.cbPeriodofatt.TabIndex = 28
    Me.cbPeriodofatt.ValueMember = ""
    '
    'edClientea
    '
    Me.edClientea.Cursor = System.Windows.Forms.Cursors.Default
    Me.edClientea.EditValue = "999999999"
    Me.edClientea.Location = New System.Drawing.Point(345, 88)
    Me.edClientea.Name = "edClientea"
    Me.edClientea.NTSDbField = ""
    Me.edClientea.NTSFormat = "0"
    Me.edClientea.NTSForzaVisZoom = False
    Me.edClientea.NTSOldValue = "999999999"
    Me.edClientea.Properties.Appearance.Options.UseTextOptions = True
    Me.edClientea.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edClientea.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edClientea.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edClientea.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edClientea.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edClientea.Size = New System.Drawing.Size(100, 20)
    Me.edClientea.TabIndex = 27
    '
    'edClienteda
    '
    Me.edClienteda.Cursor = System.Windows.Forms.Cursors.Default
    Me.edClienteda.EditValue = "0"
    Me.edClienteda.Location = New System.Drawing.Point(182, 88)
    Me.edClienteda.Name = "edClienteda"
    Me.edClienteda.NTSDbField = ""
    Me.edClienteda.NTSFormat = "0"
    Me.edClienteda.NTSForzaVisZoom = False
    Me.edClienteda.NTSOldValue = ""
    Me.edClienteda.Properties.Appearance.Options.UseTextOptions = True
    Me.edClienteda.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edClienteda.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edClienteda.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edClienteda.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edClienteda.Properties.MaxLength = 65536
    Me.edClienteda.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edClienteda.Size = New System.Drawing.Size(99, 20)
    Me.edClienteda.TabIndex = 26
    '
    'ckRiepilogatutto
    '
    Me.ckRiepilogatutto.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckRiepilogatutto.Location = New System.Drawing.Point(6, 23)
    Me.ckRiepilogatutto.Name = "ckRiepilogatutto"
    Me.ckRiepilogatutto.NTSCheckValue = "S"
    Me.ckRiepilogatutto.NTSUnCheckValue = "N"
    Me.ckRiepilogatutto.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckRiepilogatutto.Properties.Appearance.Options.UseBackColor = True
    Me.ckRiepilogatutto.Properties.Caption = "Tutti i documenti da riepilogare"
    Me.ckRiepilogatutto.Size = New System.Drawing.Size(174, 19)
    Me.ckRiepilogatutto.TabIndex = 23
    '
    'fmDatiDocumento
    '
    Me.fmDatiDocumento.AllowDrop = True
    Me.fmDatiDocumento.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmDatiDocumento.Appearance.Options.UseBackColor = True
    Me.fmDatiDocumento.Controls.Add(Me.edSeriedoc)
    Me.fmDatiDocumento.Controls.Add(Me.edNumdoc)
    Me.fmDatiDocumento.Controls.Add(Me.edDatadoc)
    Me.fmDatiDocumento.Controls.Add(Me.edAnnodoc)
    Me.fmDatiDocumento.Controls.Add(Me.cbTipodoc)
    Me.fmDatiDocumento.Controls.Add(Me.lbNumdoc)
    Me.fmDatiDocumento.Controls.Add(Me.lbAnnoSeriedoc)
    Me.fmDatiDocumento.Controls.Add(Me.lbTipodoc)
    Me.fmDatiDocumento.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmDatiDocumento.Location = New System.Drawing.Point(3, 7)
    Me.fmDatiDocumento.Name = "fmDatiDocumento"
    Me.fmDatiDocumento.Size = New System.Drawing.Size(453, 91)
    Me.fmDatiDocumento.TabIndex = 0
    Me.fmDatiDocumento.Text = "Documenti da generare"
    '
    'edSeriedoc
    '
    Me.edSeriedoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSeriedoc.EditValue = " "
    Me.edSeriedoc.Location = New System.Drawing.Point(251, 44)
    Me.edSeriedoc.Name = "edSeriedoc"
    Me.edSeriedoc.NTSDbField = ""
    Me.edSeriedoc.NTSForzaVisZoom = False
    Me.edSeriedoc.NTSOldValue = " "
    Me.edSeriedoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSeriedoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSeriedoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSeriedoc.Properties.MaxLength = 65536
    Me.edSeriedoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSeriedoc.Size = New System.Drawing.Size(45, 20)
    Me.edSeriedoc.TabIndex = 10
    '
    'edNumdoc
    '
    Me.edNumdoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumdoc.EditValue = "0"
    Me.edNumdoc.Location = New System.Drawing.Point(182, 66)
    Me.edNumdoc.Name = "edNumdoc"
    Me.edNumdoc.NTSDbField = ""
    Me.edNumdoc.NTSFormat = "0"
    Me.edNumdoc.NTSForzaVisZoom = False
    Me.edNumdoc.NTSOldValue = ""
    Me.edNumdoc.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumdoc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumdoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNumdoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNumdoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumdoc.Properties.MaxLength = 65536
    Me.edNumdoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumdoc.Size = New System.Drawing.Size(114, 20)
    Me.edNumdoc.TabIndex = 7
    '
    'edDatadoc
    '
    Me.edDatadoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatadoc.EditValue = "01/01/1900"
    Me.edDatadoc.Location = New System.Drawing.Point(341, 44)
    Me.edDatadoc.Name = "edDatadoc"
    Me.edDatadoc.NTSDbField = ""
    Me.edDatadoc.NTSForzaVisZoom = False
    Me.edDatadoc.NTSOldValue = ""
    Me.edDatadoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatadoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatadoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatadoc.Properties.MaxLength = 65536
    Me.edDatadoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatadoc.Size = New System.Drawing.Size(102, 20)
    Me.edDatadoc.TabIndex = 5
    '
    'edAnnodoc
    '
    Me.edAnnodoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnnodoc.EditValue = "2008"
    Me.edAnnodoc.Location = New System.Drawing.Point(182, 44)
    Me.edAnnodoc.Name = "edAnnodoc"
    Me.edAnnodoc.NTSDbField = ""
    Me.edAnnodoc.NTSFormat = "0"
    Me.edAnnodoc.NTSForzaVisZoom = False
    Me.edAnnodoc.NTSOldValue = "2008"
    Me.edAnnodoc.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnnodoc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnnodoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnnodoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnnodoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnnodoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnnodoc.Size = New System.Drawing.Size(63, 20)
    Me.edAnnodoc.TabIndex = 4
    '
    'cbTipodoc
    '
    Me.cbTipodoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipodoc.DataSource = Nothing
    Me.cbTipodoc.DisplayMember = ""
    Me.cbTipodoc.Location = New System.Drawing.Point(182, 22)
    Me.cbTipodoc.Name = "cbTipodoc"
    Me.cbTipodoc.NTSDbField = ""
    Me.cbTipodoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipodoc.Properties.DropDownRows = 30
    Me.cbTipodoc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipodoc.SelectedValue = ""
    Me.cbTipodoc.Size = New System.Drawing.Size(261, 20)
    Me.cbTipodoc.TabIndex = 3
    Me.cbTipodoc.ValueMember = ""
    '
    'lbNumdoc
    '
    Me.lbNumdoc.AutoSize = True
    Me.lbNumdoc.BackColor = System.Drawing.Color.Transparent
    Me.lbNumdoc.Location = New System.Drawing.Point(9, 69)
    Me.lbNumdoc.Name = "lbNumdoc"
    Me.lbNumdoc.NTSDbField = ""
    Me.lbNumdoc.Size = New System.Drawing.Size(78, 13)
    Me.lbNumdoc.TabIndex = 2
    Me.lbNumdoc.Text = "Numero iniziale"
    Me.lbNumdoc.Tooltip = ""
    Me.lbNumdoc.UseMnemonic = False
    '
    'lbAnnoSeriedoc
    '
    Me.lbAnnoSeriedoc.AutoSize = True
    Me.lbAnnoSeriedoc.BackColor = System.Drawing.Color.Transparent
    Me.lbAnnoSeriedoc.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAnnoSeriedoc.Location = New System.Drawing.Point(9, 47)
    Me.lbAnnoSeriedoc.Name = "lbAnnoSeriedoc"
    Me.lbAnnoSeriedoc.NTSDbField = ""
    Me.lbAnnoSeriedoc.Size = New System.Drawing.Size(102, 13)
    Me.lbAnnoSeriedoc.TabIndex = 1
    Me.lbAnnoSeriedoc.Text = "Anno/serie/data"
    Me.lbAnnoSeriedoc.Tooltip = ""
    Me.lbAnnoSeriedoc.UseMnemonic = False
    '
    'lbTipodoc
    '
    Me.lbTipodoc.AutoSize = True
    Me.lbTipodoc.BackColor = System.Drawing.Color.Transparent
    Me.lbTipodoc.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbTipodoc.Location = New System.Drawing.Point(7, 25)
    Me.lbTipodoc.Name = "lbTipodoc"
    Me.lbTipodoc.NTSDbField = ""
    Me.lbTipodoc.Size = New System.Drawing.Size(58, 13)
    Me.lbTipodoc.TabIndex = 0
    Me.lbTipodoc.Text = "Tipologia"
    Me.lbTipodoc.Tooltip = ""
    Me.lbTipodoc.UseMnemonic = False
    '
    'ckProva
    '
    Me.ckProva.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckProva.Location = New System.Drawing.Point(465, 63)
    Me.ckProva.Name = "ckProva"
    Me.ckProva.NTSCheckValue = "S"
    Me.ckProva.NTSUnCheckValue = "N"
    Me.ckProva.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckProva.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.ckProva.Properties.Appearance.Options.UseBackColor = True
    Me.ckProva.Properties.Appearance.Options.UseFont = True
    Me.ckProva.Properties.Caption = "Fatturaz. di prova"
    Me.ckProva.Size = New System.Drawing.Size(114, 19)
    Me.ckProva.TabIndex = 38
    '
    'cmdLoadRecent
    '
    Me.cmdLoadRecent.ImageText = ""
    Me.cmdLoadRecent.Location = New System.Drawing.Point(465, 105)
    Me.cmdLoadRecent.Name = "cmdLoadRecent"
    Me.cmdLoadRecent.Size = New System.Drawing.Size(114, 30)
    Me.cmdLoadRecent.TabIndex = 39
    Me.cmdLoadRecent.Text = "Conf. come da prova"
    '
    'FRMVESEFD_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(584, 311)
    Me.Controls.Add(Me.cmdLoadRecent)
    Me.Controls.Add(Me.ckProva)
    Me.Controls.Add(Me.pnFiltri)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMVESEFD_F"
    Me.Text = "NUOVI DOCUMENTI RIEPILOGATIVI"
    CType(Me.pnFiltri, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFiltri.ResumeLayout(False)
    Me.pnFiltri.PerformLayout()
    CType(Me.ckVisDDT.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNoteFattura.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbCheckrighe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmFiltri, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmFiltri.ResumeLayout(False)
    Me.fmFiltri.PerformLayout()
    CType(Me.ckDatdoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnnoddt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNumdoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSeriedocda.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSeriedoca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNumdoca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNumdocda.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatadoca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatadocda.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPeriodofatt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edClientea.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edClienteda.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckRiepilogatutto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmDatiDocumento, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmDatiDocumento.ResumeLayout(False)
    Me.fmDatiDocumento.PerformLayout()
    CType(Me.edSeriedoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNumdoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatadoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnnodoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipodoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckProva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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

    InitializeComponent()
    Me.MinimumSize = Me.Size

    Return True
  End Function

  Public Overridable Function CaricaCombo() As Boolean
    Dim dttPeriodo As New DataTable()
    Dim dttCheck As New DataTable()
    Dim dttTipodoc As New DataTable()
    Try
      '--------------------------
      'carico i combobox
      dttPeriodo.Columns.Add("cod", GetType(String))
      dttPeriodo.Columns.Add("val", GetType(String))
      dttPeriodo.Rows.Add(New Object() {" ", "(Qualsiasi)"})
      dttPeriodo.Rows.Add(New Object() {"M", "Mensile"})
      dttPeriodo.Rows.Add(New Object() {"Q", "Quattordicinale"})
      dttPeriodo.Rows.Add(New Object() {"S", "Settimanale"})
      dttPeriodo.Rows.Add(New Object() {"G", "Giornaliera"})
      dttPeriodo.AcceptChanges()
      cbPeriodofatt.DataSource = dttPeriodo
      cbPeriodofatt.ValueMember = "cod"
      cbPeriodofatt.DisplayMember = "val"

      dttCheck.Columns.Add("cod", GetType(String))
      dttCheck.Columns.Add("val", GetType(String))
      dttCheck.Rows.Add(New Object() {"0", "Nessuno"})
      dttCheck.Rows.Add(New Object() {"1", "Tutte tranne articolo D"})
      dttCheck.Rows.Add(New Object() {"2", "Tutte tranne articolo non a magazzino"})
      dttCheck.Rows.Add(New Object() {"3", "Tutte quelle con quantità diversa da 0"})
      dttCheck.AcceptChanges()
      cbCheckrighe.DataSource = dttCheck
      cbCheckrighe.ValueMember = "cod"
      cbCheckrighe.DisplayMember = "val"

      dttTipodoc.Columns.Add("cod", GetType(String))
      dttTipodoc.Columns.Add("val", GetType(String))
      dttTipodoc.Rows.Add(New Object() {"D", "Fattura differita emessa"})
      dttTipodoc.Rows.Add(New Object() {"P", "Fattura/ricevuta fiscale differita"})
      dttTipodoc.Rows.Add(New Object() {"£", "Nota accredito differita emessa"})
      dttTipodoc.AcceptChanges()
      cbTipodoc.DataSource = dttTipodoc
      cbTipodoc.ValueMember = "cod"
      cbTipodoc.DisplayMember = "val"

      Return True

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      edNoteFattura.NTSSetParam(oMenu, oApp.Tr(Me, 129055311003977230, "Note fattura"), 80)
      edClientea.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055311031634365, "Cliente DA"), tabanagrac)
      edClienteda.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055311058510225, "Cliente A"), tabanagrac)
      ckNumdoc.NTSSetParam(oMenu, oApp.Tr(Me, 129055311167107450, "Filtra numero / serie docum."), "S", "N")
      ckDatdoc.NTSSetParam(oMenu, oApp.Tr(Me, 129055311195077095, "Filtra data documento DA / A"), "S", "N")
      edSeriedocda.NTSSetParam(oMenu, oApp.Tr(Me, 129055311214921480, "Serie documento DA"), CLN__STD.SerieMaxLen, False)
      edSeriedoca.NTSSetParam(oMenu, oApp.Tr(Me, 129055311234297100, "Serie documento A"), CLN__STD.SerieMaxLen, False)
      edNumdoca.NTSSetParam(oMenu, oApp.Tr(Me, 129055311253672720, "Numero documento A"), "0", 9, 0, 999999999)
      edNumdocda.NTSSetParam(oMenu, oApp.Tr(Me, 129055311275860930, "Numero documento DA"), "0", 9, 0, 999999999)
      edDatadoca.NTSSetParam(oMenu, oApp.Tr(Me, 129055311294611530, "Data documento A"), False)
      edDatadocda.NTSSetParam(oMenu, oApp.Tr(Me, 129055311313674640, "Data documento DA"), False)
      edAnnodoc.NTSSetParam(oMenu, oApp.Tr(Me, 129055311340863010, "Anno documento"), "0", 4, 1900, 2099)
      cbTipodoc.NTSSetParam(oApp.Tr(Me, 129055311366488830, "Tipo documento"))
      cbCheckrighe.NTSSetParam(oApp.Tr(Me, 129055311386801980, "Controlla righe con prezzi a zero"))
      edAnnoddt.NTSSetParam(oMenu, oApp.Tr(Me, 129055311459304300, "Anno documenti da riepilogare"), "0", 4, 1900, 2099)
      cbPeriodofatt.NTSSetParam(oApp.Tr(Me, 129055311489930280, "Periodo di fatturazione"))
      ckRiepilogatutto.NTSSetParam(oMenu, oApp.Tr(Me, 129055311513212275, "Tutti i documenti da riepilogare"), "S", "N")
      ckProva.NTSSetParam(oMenu, oApp.Tr(Me, 129055311543369490, "Fatturazione di prova"), "S", "N")
      edSeriedoc.NTSSetParam(oMenu, oApp.Tr(Me, 129055311570245350, "Serie documento"), CLN__STD.SerieMaxLen, False)
      edNumdoc.NTSSetParam(oMenu, oApp.Tr(Me, 129055311592746070, "Numero primo documento"), "0", 9, 0, 999999999)
      edDatadoc.NTSSetParam(oMenu, oApp.Tr(Me, 129055311613996750, "Data documento"), False)
      ckVisDDT.NTSSetParam(oMenu, oApp.Tr(Me, 130003157396090311, "Visualizza estremi DDT prima dell'elaborazione"), "S", "N")

      edSeriedoca.Text = "Z".PadRight(CLN__STD.SerieMaxLen, "Z"c)
      '--------------------------
      'chiamo lo script per inizializzare i controlli caricati con source ext
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

#Region "Eventi di form"
  Public Overridable Sub FRMVESEFD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim strTmp As String = ""
    Try
      CaricaCombo()

      InitControls()

      If bRielabora Then
        GctlSetRoules()
        cbTipodoc.SelectedValue = strRielabTipork
        ckProva.Enabled = False
        ckRiepilogatutto.Enabled = False
        cbTipodoc.Enabled = False
        edSeriedoc.Enabled = False
        edNumdoc.Enabled = False
        edAnnodoc.Enabled = False
        edNumdoc.Text = nRielabFirstNumdoc.ToString
        ckRiepilogatutto_CheckedChanged(ckRiepilogatutto, Nothing)
        cbPeriodofatt.Enabled = False
        ckVisDDT.Enabled = False
        ckVisDDT.Checked = False
        cmdLoadRecent.Enabled = False
        Me.Text = oApp.Tr(Me, 128764202820459000, "RIELABORA DOCUMENTI")
      Else
        cbTipodoc.SelectedValue = "D"
        edDatadocda.Text = IntSetDate("01/" & DateTime.Now.Month.ToString.PadLeft(2, "0"c) & "/" & DateTime.Now.Year.ToString)
        edDatadoca.Text = DateTime.Now.ToShortDateString
        cbTipodoc_SelectedValueChanged(cbTipodoc, Nothing)      'propongo il numero documento
        ckRiepilogatutto_CheckedChanged(ckRiepilogatutto, Nothing)
        'ckGeneraLista.Checked = True
        'edCodNuoLista.Text = NTSCStr(oCleFadi.GetNumeroNuovaListaSel())
        cbTipodoc.Focus()
        '--------------------------------------------
        edAnnodoc.Text = DateTime.Now.Year.ToString
        If bRielabora = False And oCleFadi.bRiproponiDataDoc And NTSCDate(edDatadoc.Text).Year > 2000 Then
          'già impostata da BNVEFADI
        Else
          If strDtNewFatt = "" Then
            edDatadoc.Text = DateTime.Now.ToShortDateString
          Else
            edDatadoc.Text = strDtNewFatt
          End If
        End If
      End If
      ckNumdoc_CheckedChanged(ckNumdoc, Nothing)
      ckDatdoc_CheckedChanged(ckDatdoc, Nothing)
      edAnnoddt.Text = edAnnodoc.Text

      '--------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      If bRielabora = False Then
        GctlSetRoules()
        GctlApplicaDefaultValue()
      End If

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMVESEFD_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Try
      '--------------------------------------------
      'gestione dello zoom:
      'eseguo la Zoom, tanto se per il controllo non deve venir eseguito uno zoom particolare, la routine non fa nulla e viene processato lo zoom standard del controllo, settato con la NTSSetParamZoom
      If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
        Zoom()
        e.Handled = True    'altrimenti anche il controllo riceve l'F5 e la routine ZOOM viene eseguita 2 volte!!!
      End If

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

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

      NTSCallStandardZoom()

      '------------------------------------
      'riporto il focus sul controllo che ha chiamato lo zoom
      ctlLastControl.Focus()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try

  End Sub

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Dim lNumDocTmp As Integer
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strTipobf = "2"
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      If (edDatadocda.Enabled = True) Or (edDatadoca.Enabled = True) Then
        If NTSCDate(edDatadocda.Text) > NTSCDate(edDatadoca.Text) Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 130692612727932300, "Attenzione!" & vbCrLf & _
            "La data documento iniziale non può essere superiore a quella finale."))
          edDatadocda.Focus()
          Return
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (edSeriedocda.Enabled = True) Or (edSeriedoca.Enabled = True) Then
        If edSeriedocda.Text > edSeriedoca.Text Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 130692613925241938, "Attenzione!" & vbCrLf & _
            "La serie documento iniziale non può essere superiore a quella finale."))
          edSeriedocda.Focus()
          Return
        Else
          If (edSeriedocda.Text = edSeriedoca.Text) And (NTSCInt(edNumdocda.Text) > NTSCInt(edNumdoca.Text)) Then
            oApp.MsgBoxInfo(oApp.Tr(Me, 130692614687805431, "Attenzione!" & vbCrLf & _
              "Il numero documento iniziale non può essere superiore a quello finale."))
            edNumdocda.Focus()
            Return
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (edClienteda.Enabled = True) Or (edClientea.Enabled = True) Then
        If NTSCInt(edClienteda.Text) > NTSCInt(edClientea.Text) Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 130692615277095437, "Attenzione!" & vbCrLf & _
            "Il Cliente iniziale non può essere superiore a quello finale."))
          edClienteda.Text = "0"
          edClientea.Text = "".PadLeft(9, "9"c)
          edClienteda.Focus()
          Return
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If OperazioneConsentita() = False Then Return

      '--------------------------
      'un po' di controlli banali ...
      If NTSCInt(edAnnodoc.Text) <> NTSCDate(edDatadoc.Text).Year Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128758103935603000, "La data fatturazione non è inclusa nell'anno indicato"))
        Return
      End If

      If ckProva.Checked = False Then
        lNumDocTmp = oCleFadi.PrimoNumdoc(cbTipodoc.SelectedValue, NTSCInt(edAnnodoc.Text), edSeriedoc.Text)
        If lNumDocTmp = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129999473636164913, "Prima di creare un nuovo documento è necessario attivare la numerazione del documento"))
          Return
        End If
      End If

      If NTSCInt(edNumdoc.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129999707297037768, "N. primo documento obbligatorio"))
        edNumdoc.Focus()
        Exit Sub
      End If

      If ckRiepilogatutto.Checked Then
        If cbTipodoc.SelectedValue = "P" Then strTipobf = "1"
      Else
        If (cbTipodoc.SelectedValue <> "D") And (cbTipodoc.SelectedValue <> "£") Then strTipobf = "1"
      End If    'If ckRiepilogatutto.Checked Then

      If bRielabora And NTSCInt(edClienteda.Text) <> NTSCInt(edClientea.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128757918613414000, "E' possibile rielaborare fatture di un solo cliente alla volta."))
        Return
      End If

      '--------------------------------
      Select Case cbTipodoc.SelectedValue
        Case "D" : strOutTestmag = "testmag.tm_tipork|='B'|='F'§"
        Case "P" : strOutTestmag = "testmag.tm_tipork|='B'|='F'§"
        Case "£" : strOutTestmag = "testmag.tm_tipork='M'§an_tipo='C'§"   'solo clienti, altrimenti tirerei su tutti i ddt ricevuti !!!!
      End Select
      strOutTestmag += "tm_flfatt <> 'S'§"
      strOutTestmag += "tm_datdoc <= " & CDataSQL(edDatadoc.Text) & "§"
      '---------------------------
      'filtri su tipobf
      Select Case strTipobf
        Case "1" : strOutTestmag += "tabtpbf.tb_flresocl = 'F'§"    'Tutti i documenti 'segue fattura/ric. fiscale'
        Case "2" : strOutTestmag += "tabtpbf.tb_flresocl = 'S'§"    'Tutti i documenti 'segue fattura'
      End Select

      If ckRiepilogatutto.Checked Then
        'nulla da fare ...
        lOutNumdocDa = 0
        lOutNumdocA = 999999999
        strOutSerieDa = " "
        strOutSerieA = "Z"
      Else
        '---------------------------
        'filtri su testmag
        strOutTestmag += "testmag.tm_anno = " & CDblSQL(edAnnoddt.Text) & "§"

        If ckDatdoc.Checked Then
          strOutTestmag += "tm_datdoc >= " & CDataSQL(edDatadocda.Text) & "§"
          strOutTestmag += "tm_datdoc <= " & CDataSQL(edDatadoca.Text) & "§"
        End If

        If ckNumdoc.Checked Then
          lOutNumdocDa = NTSCInt(edNumdocda.Text)
          lOutNumdocA = NTSCInt(edNumdoca.Text)
          strOutSerieDa = edSeriedocda.Text.ToUpper
          strOutSerieA = edSeriedoca.Text.ToUpper
          'gstrFdQuery = gstrFdQuery & " AND ((tm_serie = '" & edSerieda & "' AND tm_numdoc >= " & edNumda & ") OR (tm_serie = '" & edSeriea & "' AND tm_numdoc <= " & edNuma & ") OR (tm_serie > '" & edSerieda & "' AND tm_serie < '" & edSeriea & "'))"
        Else
          lOutNumdocDa = 0
          lOutNumdocA = 999999999
          strOutSerieDa = " "
          strOutSerieA = "Z"
        End If

        '---------------------------
        'filtri su anagra
        strOutAnagra += "an_conto >= " & CDblSQL(edClienteda.Text) & "§"
        strOutAnagra += "an_conto <= " & CDblSQL(edClientea.Text) & "§"
        If cbPeriodofatt.SelectedValue <> " " Then
          strOutAnagra += "an_perfatt = " & CStrSQL(cbPeriodofatt.SelectedValue) & "§"
        End If
      End If    'If ckRiepilogatutto.Checked Then

      '---------------------------
      If bRielabora = False Then
        oMenu.SaveSettingBus("Bsvefadi", "RECENT", ".", "CheckRighe", cbCheckrighe.SelectedValue, " ", "NS.", "...", "...")
      End If

      If strOutTestmag.Trim <> "" Then strOutTestmag = strOutTestmag.Substring(0, strOutTestmag.Length - 1)
      If strOutAnagra.Trim <> "" Then strOutAnagra = strOutAnagra.Substring(0, strOutAnagra.Length - 1)

      If ckProva.Checked Then
        'memorizzo le impostazioni per poterle riutilizzare nell'elaborazione definitiva
        strTmp = "cbTipodoc:" & cbTipodoc.SelectedValue & "§" & _
                 "edAnnodoc:" & edAnnodoc.Text & "§" & _
                 "edSeriedoc:" & edSeriedoc.Text & "§" & _
                 "edDatadoc:" & edDatadoc.Text & "§" & _
                 "edNumdoc:" & edNumdoc.Text & "§" & _
                 "edNoteFattura:" & edNoteFattura.Text & "§" & _
                 "ckVisDDT:" & IIf(ckVisDDT.Checked, "-1", "0").ToString & "§" & _
                 "cbCheckrighe:" & cbCheckrighe.SelectedValue & "§" & _
                 "ckForzaDataDiversa:0§" & _
                 "edDataDiversa:" & IntSetDate(Now.ToShortDateString) & "§" & _
                 "ckRiepilogatutto:" & IIf(ckRiepilogatutto.Checked, "-1", "0").ToString & "§" & _
                 "edAnnoddt:" & edAnnoddt.Text & "§" & _
                 "ckDatdoc:" & IIf(ckDatdoc.Checked, "-1", "0").ToString & "§" & _
                 "edDatadocda:" & edDatadocda.Text & "§" & _
                 "edDatadoca:" & edDatadoca.Text & "§" & _
                 "ckNumdoc:" & IIf(ckNumdoc.Checked, "-1", "0").ToString & "§" & _
                 "edNumdocda:" & edNumdocda.Text & "§" & _
                 "edSeriedocda:" & edSeriedocda.Text & "§" & _
                 "edNumdoca:" & edNumdoca.Text & "§" & _
                 "edSeriedoca:" & edSeriedoca.Text & "§" & _
                 "cbTipobf:" & strTipobf & "§" & _
                 "edTipobf:0§" & _
                 "opCliDaa:-1§" & _
                 "edClienteda:" & edClienteda.Text & "§" & _
                 "edClientea:" & edClientea.Text & "§" & _
                 "opCliLista:0§" & _
                 "edLista:0§" & _
                 "cbPeriodofatt:" & cbPeriodofatt.SelectedValue & "§" & _
                 "ckGeneraLista:0§" & _
                 "edCodNuoLista:0§" & _
                 "edDescrNuoLista:"


        oMenu.SaveSettingBus("Bsvefadi", "RECENT", ".", "LastTest", strTmp, " ", "NS.", "...", "...")

      End If

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

  Public Overridable Sub cmdLoadRecent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoadRecent.Click
    Dim strTmp As String = ""
    Dim strT() As String = Nothing
    Dim i As Integer = 0
    Dim l As Integer = 0
    Try
      'reimposto la maschera come da ultima elaboraz. di prova
      strTmp = oMenu.GetSettingBus("Bsvefadi", "RECENT", ".", "LastTest", "", " ", "")
      If strTmp.Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130002175008119105, "Per utilizzare questa funzione occore aver lanciato almeno una volta la stampa in prova"))
        Return
      End If
      strT = strTmp.Split("§"c)
      For i = 0 To strT.Length - 1
        If strT(i).Trim <> "" Then
          l = strT(i).IndexOf(":")
          Select Case strT(i).Substring(0, l)
            Case "cbTipodoc" : cbTipodoc.SelectedValue = strT(i).Substring(l + 1)
            Case "edAnnodoc" : edAnnodoc.Text = strT(i).Substring(l + 1)
            Case "edSeriedoc" : edSeriedoc.Text = strT(i).Substring(l + 1)
            Case "edDatadoc" : edDatadoc.Text = strT(i).Substring(l + 1)
            Case "edNumdoc" : edNumdoc.Text = strT(i).Substring(l + 1)
            Case "edNoteFattura" : edNoteFattura.Text = strT(i).Substring(l + 1)
            Case "ckVisDDT" : ckVisDDT.Checked = CBool(strT(i).Substring(l + 1))
            Case "cbCheckrighe" : cbCheckrighe.SelectedValue = strT(i).Substring(l + 1)
            Case "ckRiepilogatutto" : ckRiepilogatutto.Checked = CBool(strT(i).Substring(l + 1))
            Case "edAnnoddt" : edAnnoddt.Text = strT(i).Substring(l + 1)
            Case "ckDatdoc" : ckDatdoc.Checked = CBool(strT(i).Substring(l + 1))
            Case "edDatadocda" : edDatadocda.Text = strT(i).Substring(l + 1)
            Case "edDatadoca" : edDatadoca.Text = strT(i).Substring(l + 1)
            Case "ckNumdoc" : ckNumdoc.Checked = CBool(strT(i).Substring(l + 1))
            Case "edNumdocda" : edNumdocda.Text = strT(i).Substring(l + 1)
            Case "edSeriedocda" : edSeriedocda.Text = strT(i).Substring(l + 1)
            Case "edNumdoca" : edNumdoca.Text = strT(i).Substring(l + 1)
            Case "edSeriedoca" : edSeriedoca.Text = strT(i).Substring(l + 1)
            Case "edClienteda" : edClienteda.Text = strT(i).Substring(l + 1)
            Case "edClientea" : edClientea.Text = strT(i).Substring(l + 1)
            Case "cbPeriodofatt" : cbPeriodofatt.SelectedValue = strT(i).Substring(l + 1)
          End Select
        End If
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckDatdoc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckDatdoc.CheckStateChanged
    Try
      If ckDatdoc.Checked Then
        GctlSetVisEnab(edDatadocda, False)
        GctlSetVisEnab(edDatadoca, False)
      Else
        edDatadocda.Enabled = False
        edDatadoca.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub ckNumdoc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckNumdoc.CheckStateChanged
    Try
      If ckNumdoc.Checked Then
        GctlSetVisEnab(edNumdocda, False)
        GctlSetVisEnab(edNumdoca, False)
        GctlSetVisEnab(edSeriedocda, False)
        GctlSetVisEnab(edSeriedoca, False)
      Else
        edNumdocda.Enabled = False
        edNumdoca.Enabled = False
        edSeriedocda.Enabled = False
        edSeriedoca.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckRiepilogatutto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckRiepilogatutto.CheckedChanged
    Try
      If ckRiepilogatutto.Checked = False Then
        GctlSetVisEnab(ckNumdoc, False)
        GctlSetVisEnab(edAnnoddt, False)
        GctlSetVisEnab(ckDatdoc, False)
        If bRielabora = False Then GctlSetVisEnab(cbPeriodofatt, False)
      Else
        ckNumdoc.Checked = False
        ckNumdoc.Enabled = False
        edClienteda.Enabled = False
        edClientea.Enabled = False
        edAnnoddt.Enabled = False
        cbPeriodofatt.Enabled = False
        ckDatdoc.Enabled = False
        ckDatdoc.Checked = False
        edDatadocda.Enabled = False
        edDatadoca.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edClienteda_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edClienteda.ValidatedAndChanged
    Try
      edClientea.Text = edClienteda.Text
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

#Region "proposta numero documento"
  Public Overridable Sub cbTipodoc_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTipodoc.SelectedValueChanged
    Try
      NTSGetDataAutocompletamentoTipork = cbTipodoc.SelectedValue 'per filtrare solo i tipibf utilizzabili per questo tipork

      PrimoNumdoc()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edAnnodoc_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAnnodoc.ValidatedAndChanged
    Try
      PrimoNumdoc()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edSeriedoc_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSeriedoc.ValidatedAndChanged
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edSeriedoc.Text, False)
      If strTmp <> edSeriedoc.Text Then edSeriedoc.Text = strTmp

      If edSeriedoc.Text <> edSeriedoc.Text.ToUpper Then edSeriedoc.Text = edSeriedoc.Text.ToUpper
      PrimoNumdoc()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Function PrimoNumdoc() As Boolean
    Try
      '---------------------
      'propone il primo numero documento
      If oCleFadi Is Nothing Then Return True
      edNumdoc.Text = oCleFadi.PrimoNumdoc(cbTipodoc.SelectedValue, NTSCInt(edAnnodoc.Text), edSeriedoc.Text).ToString

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
#End Region

  Public Overrides Function NTSGetDataAutocompletamento(ByVal strTabName As String, ByVal strDescr As String, _
      ByVal IsCrmUser As Boolean, ByRef dsOut As DataSet) As Boolean
    Try
      '-------------------MO-------------------------------------------------------------------------------------------
      If edClienteda.ContainsFocus Or edClientea.ContainsFocus Then strTabName = "ANAGRA_CLI"
      '--------------------------------------------------------------------------------------------------------------
      Return MyBase.NTSGetDataAutocompletamento(strTabName, strDescr, IsCrmUser, dsOut)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function OperazioneConsentita() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      OperazioneConsentita = True
      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione "Friendly", scaduta e la data di registrazione è posteriore alla data di
      '--- scadenza della chiave, ritorna false
      '--------------------------------------------------------------------------------------------------------------
      If (CLN__STD.FRIENDLY = True) And _
         (NTSCDate(Now.ToShortDateString) > NTSCDate(oApp.ActKey.DataScad)) And _
         (NTSCDate(edDatadoc.Text) > NTSCDate(oApp.ActKey.DataScad)) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130161960628802790, "Attenzione!" & vbCrLf & _
          "Chiave di attivazione scaduta!" & vbCrLf & _
          "Operazione NON consentita con data nuovi documenti posteriore al '|" & oApp.ActKey.DataScad & "|'."))
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Sub edSeriedoca_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSeriedoca.Validated
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edSeriedoca.Text, False)
      If strTmp <> edSeriedoca.Text Then edSeriedoca.Text = strTmp

    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edSeriedocda_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSeriedocda.Validated
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edSeriedocda.Text, False)
      If strTmp <> edSeriedocda.Text Then edSeriedocda.Text = strTmp

    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub

End Class