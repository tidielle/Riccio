Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__SVLO

#Region "Variabili"
  Public oCleVlog As CLE__VLOG
  Public oCallParams As CLE__CLDP
  Private components As System.ComponentModel.IContainer

  Public strNomogglog As String
  Public strDesogglog As String

  Public WithEvents pnSvlo As NTSInformatica.NTSPanel
  Public WithEvents pnLeft As NTSInformatica.NTSPanel
  Public WithEvents frTipoOperazione As NTSInformatica.NTSGroupBox
  Public WithEvents opTipoogglog2 As NTSInformatica.NTSRadioButton
  Public WithEvents opTipoogglog1 As NTSInformatica.NTSRadioButton
  Public WithEvents opTipoogglog0 As NTSInformatica.NTSRadioButton
  Public WithEvents frTipoModifica As NTSInformatica.NTSGroupBox
  Public WithEvents opTipoop2 As NTSInformatica.NTSRadioButton
  Public WithEvents opTipoop1 As NTSInformatica.NTSRadioButton
  Public WithEvents opTipoop0 As NTSInformatica.NTSRadioButton
  Public WithEvents PnLeftLeft As NTSInformatica.NTSPanel
  Public WithEvents frOrigineDati As NTSInformatica.NTSGroupBox
  Public WithEvents opOrigine1 As NTSInformatica.NTSRadioButton
  Public WithEvents opOrigine0 As NTSInformatica.NTSRadioButton
  Public WithEvents frFiltriPrincipali As NTSInformatica.NTSGroupBox
  Public WithEvents cbDesogglog As NTSInformatica.NTSComboBox
  Public WithEvents edCodditt As NTSInformatica.NTSTextBoxStr
  Public WithEvents ckSelCodditt As NTSInformatica.NTSCheckBox
  Public WithEvents ckSelDesogglog As NTSInformatica.NTSCheckBox
  Public WithEvents edOpnome As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbOpnome As NTSInformatica.NTSLabel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents edDesogglog As NTSInformatica.NTSMemoBox
  Public WithEvents edDatfin As NTSInformatica.NTSTextBoxData
  Public WithEvents edDatini As NTSInformatica.NTSTextBoxData
  Public WithEvents edDescrDesogglog As NTSInformatica.NTSLabel
  Public WithEvents lbDatfin As NTSInformatica.NTSLabel
  Public WithEvents lbDatini As NTSInformatica.NTSLabel

#End Region

  Public Overridable Sub InitializeComponent()
    Me.pnSvlo = New NTSInformatica.NTSPanel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.pnLeft = New NTSInformatica.NTSPanel
    Me.edDesogglog = New NTSInformatica.NTSMemoBox
    Me.edDatfin = New NTSInformatica.NTSTextBoxData
    Me.edDatini = New NTSInformatica.NTSTextBoxData
    Me.edDescrDesogglog = New NTSInformatica.NTSLabel
    Me.lbDatfin = New NTSInformatica.NTSLabel
    Me.lbDatini = New NTSInformatica.NTSLabel
    Me.PnLeftLeft = New NTSInformatica.NTSPanel
    Me.frOrigineDati = New NTSInformatica.NTSGroupBox
    Me.opOrigine1 = New NTSInformatica.NTSRadioButton
    Me.opOrigine0 = New NTSInformatica.NTSRadioButton
    Me.frFiltriPrincipali = New NTSInformatica.NTSGroupBox
    Me.cbDesogglog = New NTSInformatica.NTSComboBox
    Me.edCodditt = New NTSInformatica.NTSTextBoxStr
    Me.ckSelCodditt = New NTSInformatica.NTSCheckBox
    Me.ckSelDesogglog = New NTSInformatica.NTSCheckBox
    Me.edOpnome = New NTSInformatica.NTSTextBoxStr
    Me.lbOpnome = New NTSInformatica.NTSLabel
    Me.frTipoOperazione = New NTSInformatica.NTSGroupBox
    Me.opTipoogglog2 = New NTSInformatica.NTSRadioButton
    Me.opTipoogglog1 = New NTSInformatica.NTSRadioButton
    Me.opTipoogglog0 = New NTSInformatica.NTSRadioButton
    Me.frTipoModifica = New NTSInformatica.NTSGroupBox
    Me.opTipoop2 = New NTSInformatica.NTSRadioButton
    Me.opTipoop1 = New NTSInformatica.NTSRadioButton
    Me.opTipoop0 = New NTSInformatica.NTSRadioButton
    CType(Me.pnSvlo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSvlo.SuspendLayout()
    CType(Me.pnLeft, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnLeft.SuspendLayout()
    CType(Me.edDesogglog.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatfin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.PnLeftLeft, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.PnLeftLeft.SuspendLayout()
    CType(Me.frOrigineDati, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frOrigineDati.SuspendLayout()
    CType(Me.opOrigine1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opOrigine0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frFiltriPrincipali, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frFiltriPrincipali.SuspendLayout()
    CType(Me.cbDesogglog.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodditt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSelCodditt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSelDesogglog.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOpnome.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frTipoOperazione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frTipoOperazione.SuspendLayout()
    CType(Me.opTipoogglog2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opTipoogglog1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opTipoogglog0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frTipoModifica, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frTipoModifica.SuspendLayout()
    CType(Me.opTipoop2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opTipoop1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opTipoop0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(231, Byte), Integer))
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'DevXDefaultLookAndFeel
    '

    '
    'pnSvlo
    '
    Me.pnSvlo.AllowDrop = True
    Me.pnSvlo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSvlo.Appearance.Options.UseBackColor = True
    Me.pnSvlo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSvlo.Controls.Add(Me.cmdAnnulla)
    Me.pnSvlo.Controls.Add(Me.cmdOk)
    Me.pnSvlo.Controls.Add(Me.pnLeft)
    Me.pnSvlo.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnSvlo.Location = New System.Drawing.Point(0, -1)
    Me.pnSvlo.Name = "pnSvlo"
    Me.pnSvlo.Size = New System.Drawing.Size(639, 335)
    Me.pnSvlo.TabIndex = 0
    Me.pnSvlo.Text = "NtsPanel1"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Location = New System.Drawing.Point(526, 39)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(103, 23)
    Me.cmdAnnulla.TabIndex = 19
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.Location = New System.Drawing.Point(526, 11)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(103, 23)
    Me.cmdOk.TabIndex = 18
    Me.cmdOk.Text = "&Conferma"
    '
    'pnLeft
    '
    Me.pnLeft.AllowDrop = True
    Me.pnLeft.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnLeft.Appearance.Options.UseBackColor = True
    Me.pnLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnLeft.Controls.Add(Me.edDesogglog)
    Me.pnLeft.Controls.Add(Me.edDatfin)
    Me.pnLeft.Controls.Add(Me.edDatini)
    Me.pnLeft.Controls.Add(Me.edDescrDesogglog)
    Me.pnLeft.Controls.Add(Me.lbDatfin)
    Me.pnLeft.Controls.Add(Me.lbDatini)
    Me.pnLeft.Controls.Add(Me.PnLeftLeft)
    Me.pnLeft.Controls.Add(Me.frTipoOperazione)
    Me.pnLeft.Controls.Add(Me.frTipoModifica)
    Me.pnLeft.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnLeft.Location = New System.Drawing.Point(0, 0)
    Me.pnLeft.Name = "pnLeft"
    Me.pnLeft.Size = New System.Drawing.Size(520, 335)
    Me.pnLeft.TabIndex = 16
    Me.pnLeft.Text = "NtsPanel1"
    '
    'edDesogglog
    '
    Me.edDesogglog.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDesogglog.Location = New System.Drawing.Point(104, 255)
    Me.edDesogglog.Name = "edDesogglog"
    Me.edDesogglog.NTSDbField = ""
    Me.edDesogglog.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDesogglog.Size = New System.Drawing.Size(405, 70)
    Me.edDesogglog.TabIndex = 30
    '
    'edDatfin
    '
    Me.edDatfin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatfin.Location = New System.Drawing.Point(267, 224)
    Me.edDatfin.Name = "edDatfin"
    Me.edDatfin.NTSDbField = ""
    Me.edDatfin.NTSForzaVisZoom = False
    Me.edDatfin.NTSOldValue = ""
    Me.edDatfin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatfin.Properties.MaxLength = 65536
    Me.edDatfin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatfin.Size = New System.Drawing.Size(100, 20)
    Me.edDatfin.TabIndex = 29
    '
    'edDatini
    '
    Me.edDatini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatini.Location = New System.Drawing.Point(104, 224)
    Me.edDatini.Name = "edDatini"
    Me.edDatini.NTSDbField = ""
    Me.edDatini.NTSForzaVisZoom = False
    Me.edDatini.NTSOldValue = ""
    Me.edDatini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatini.Properties.MaxLength = 65536
    Me.edDatini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatini.Size = New System.Drawing.Size(100, 20)
    Me.edDatini.TabIndex = 28
    '
    'edDescrDesogglog
    '
    Me.edDescrDesogglog.AutoSize = True
    Me.edDescrDesogglog.BackColor = System.Drawing.Color.Transparent
    Me.edDescrDesogglog.Location = New System.Drawing.Point(5, 257)
    Me.edDescrDesogglog.Name = "edDescrDesogglog"
    Me.edDescrDesogglog.NTSDbField = ""
    Me.edDescrDesogglog.Size = New System.Drawing.Size(93, 13)
    Me.edDescrDesogglog.TabIndex = 27
    Me.edDescrDesogglog.Text = "Descrizione (LIKE)"
    '
    'lbDatfin
    '
    Me.lbDatfin.AutoSize = True
    Me.lbDatfin.BackColor = System.Drawing.Color.Transparent
    Me.lbDatfin.Location = New System.Drawing.Point(213, 227)
    Me.lbDatfin.Name = "lbDatfin"
    Me.lbDatfin.NTSDbField = ""
    Me.lbDatfin.Size = New System.Drawing.Size(48, 13)
    Me.lbDatfin.TabIndex = 26
    Me.lbDatfin.Text = "alla data"
    '
    'lbDatini
    '
    Me.lbDatini.AutoSize = True
    Me.lbDatini.BackColor = System.Drawing.Color.Transparent
    Me.lbDatini.Location = New System.Drawing.Point(7, 227)
    Me.lbDatini.Name = "lbDatini"
    Me.lbDatini.NTSDbField = ""
    Me.lbDatini.Size = New System.Drawing.Size(55, 13)
    Me.lbDatini.TabIndex = 25
    Me.lbDatini.Text = "Dalla data"
    '
    'PnLeftLeft
    '
    Me.PnLeftLeft.AllowDrop = True
    Me.PnLeftLeft.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.PnLeftLeft.Appearance.Options.UseBackColor = True
    Me.PnLeftLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.PnLeftLeft.Controls.Add(Me.frOrigineDati)
    Me.PnLeftLeft.Controls.Add(Me.frFiltriPrincipali)
    Me.PnLeftLeft.Cursor = System.Windows.Forms.Cursors.Default
    Me.PnLeftLeft.Location = New System.Drawing.Point(3, 3)
    Me.PnLeftLeft.Name = "PnLeftLeft"
    Me.PnLeftLeft.Size = New System.Drawing.Size(330, 215)
    Me.PnLeftLeft.TabIndex = 21
    Me.PnLeftLeft.Text = "NtsPanel1"
    '
    'frOrigineDati
    '
    Me.frOrigineDati.AllowDrop = True
    Me.frOrigineDati.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frOrigineDati.Appearance.Options.UseBackColor = True
    Me.frOrigineDati.Controls.Add(Me.opOrigine1)
    Me.frOrigineDati.Controls.Add(Me.opOrigine0)
    Me.frOrigineDati.Cursor = System.Windows.Forms.Cursors.Default
    Me.frOrigineDati.Location = New System.Drawing.Point(3, 8)
    Me.frOrigineDati.Name = "frOrigineDati"
    Me.frOrigineDati.Size = New System.Drawing.Size(322, 66)
    Me.frOrigineDati.TabIndex = 23
    Me.frOrigineDati.Text = "Origine dati da consultare: "
    '
    'opOrigine1
    '
    Me.opOrigine1.Cursor = System.Windows.Forms.Cursors.Default
    Me.opOrigine1.Location = New System.Drawing.Point(156, 27)
    Me.opOrigine1.Name = "opOrigine1"
    Me.opOrigine1.NTSCheckValue = "S"
    Me.opOrigine1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opOrigine1.Properties.Appearance.Options.UseBackColor = True
    Me.opOrigine1.Properties.Caption = "Da archivio &procedura"
    Me.opOrigine1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opOrigine1.Size = New System.Drawing.Size(145, 18)
    Me.opOrigine1.TabIndex = 1
    '
    'opOrigine0
    '
    Me.opOrigine0.Cursor = System.Windows.Forms.Cursors.Default
    Me.opOrigine0.Location = New System.Drawing.Point(5, 26)
    Me.opOrigine0.Name = "opOrigine0"
    Me.opOrigine0.NTSCheckValue = "S"
    Me.opOrigine0.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opOrigine0.Properties.Appearance.Options.UseBackColor = True
    Me.opOrigine0.Properties.Caption = "Da archivio a&zienda"
    Me.opOrigine0.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opOrigine0.Size = New System.Drawing.Size(145, 18)
    Me.opOrigine0.TabIndex = 0
    '
    'frFiltriPrincipali
    '
    Me.frFiltriPrincipali.AllowDrop = True
    Me.frFiltriPrincipali.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frFiltriPrincipali.Appearance.Options.UseBackColor = True
    Me.frFiltriPrincipali.Controls.Add(Me.cbDesogglog)
    Me.frFiltriPrincipali.Controls.Add(Me.edCodditt)
    Me.frFiltriPrincipali.Controls.Add(Me.ckSelCodditt)
    Me.frFiltriPrincipali.Controls.Add(Me.ckSelDesogglog)
    Me.frFiltriPrincipali.Controls.Add(Me.edOpnome)
    Me.frFiltriPrincipali.Controls.Add(Me.lbOpnome)
    Me.frFiltriPrincipali.Cursor = System.Windows.Forms.Cursors.Default
    Me.frFiltriPrincipali.Location = New System.Drawing.Point(3, 80)
    Me.frFiltriPrincipali.Name = "frFiltriPrincipali"
    Me.frFiltriPrincipali.Size = New System.Drawing.Size(321, 129)
    Me.frFiltriPrincipali.TabIndex = 22
    Me.frFiltriPrincipali.Text = "Filtri principali:"
    '
    'cbDesogglog
    '
    Me.cbDesogglog.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbDesogglog.DataSource = Nothing
    Me.cbDesogglog.DisplayMember = ""
    Me.cbDesogglog.Location = New System.Drawing.Point(151, 53)
    Me.cbDesogglog.Name = "cbDesogglog"
    Me.cbDesogglog.NTSDbField = ""
    Me.cbDesogglog.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbDesogglog.Properties.DropDownRows = 30
    Me.cbDesogglog.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbDesogglog.SelectedValue = ""
    Me.cbDesogglog.Size = New System.Drawing.Size(168, 20)
    Me.cbDesogglog.TabIndex = 9
    Me.cbDesogglog.ValueMember = ""
    '
    'edCodditt
    '
    Me.edCodditt.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodditt.Location = New System.Drawing.Point(151, 79)
    Me.edCodditt.Name = "edCodditt"
    Me.edCodditt.NTSDbField = ""
    Me.edCodditt.NTSForzaVisZoom = False
    Me.edCodditt.NTSOldValue = ""
    Me.edCodditt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodditt.Properties.MaxLength = 65536
    Me.edCodditt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodditt.Size = New System.Drawing.Size(168, 20)
    Me.edCodditt.TabIndex = 10
    '
    'ckSelCodditt
    '
    Me.ckSelCodditt.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSelCodditt.Location = New System.Drawing.Point(4, 80)
    Me.ckSelCodditt.Name = "ckSelCodditt"
    Me.ckSelCodditt.NTSCheckValue = "S"
    Me.ckSelCodditt.NTSUnCheckValue = "N"
    Me.ckSelCodditt.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSelCodditt.Properties.Appearance.Options.UseBackColor = True
    Me.ckSelCodditt.Properties.Caption = "Selezi&ona ditta: "
    Me.ckSelCodditt.Size = New System.Drawing.Size(115, 18)
    Me.ckSelCodditt.TabIndex = 7
    '
    'ckSelDesogglog
    '
    Me.ckSelDesogglog.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSelDesogglog.Location = New System.Drawing.Point(4, 54)
    Me.ckSelDesogglog.Name = "ckSelDesogglog"
    Me.ckSelDesogglog.NTSCheckValue = "S"
    Me.ckSelDesogglog.NTSUnCheckValue = "N"
    Me.ckSelDesogglog.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSelDesogglog.Properties.Appearance.Options.UseBackColor = True
    Me.ckSelDesogglog.Properties.Caption = "&Seleziona oggetto di LOG:"
    Me.ckSelDesogglog.Size = New System.Drawing.Size(147, 18)
    Me.ckSelDesogglog.TabIndex = 8
    '
    'edOpnome
    '
    Me.edOpnome.Cursor = System.Windows.Forms.Cursors.Default
    Me.edOpnome.Location = New System.Drawing.Point(151, 29)
    Me.edOpnome.Name = "edOpnome"
    Me.edOpnome.NTSDbField = ""
    Me.edOpnome.NTSForzaVisZoom = False
    Me.edOpnome.NTSOldValue = ""
    Me.edOpnome.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOpnome.Properties.MaxLength = 65536
    Me.edOpnome.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOpnome.Size = New System.Drawing.Size(168, 20)
    Me.edOpnome.TabIndex = 12
    '
    'lbOpnome
    '
    Me.lbOpnome.AutoSize = True
    Me.lbOpnome.BackColor = System.Drawing.Color.Transparent
    Me.lbOpnome.Location = New System.Drawing.Point(5, 32)
    Me.lbOpnome.Name = "lbOpnome"
    Me.lbOpnome.NTSDbField = ""
    Me.lbOpnome.Size = New System.Drawing.Size(85, 13)
    Me.lbOpnome.TabIndex = 11
    Me.lbOpnome.Text = "Nome operatore"
    '
    'frTipoOperazione
    '
    Me.frTipoOperazione.AllowDrop = True
    Me.frTipoOperazione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frTipoOperazione.Appearance.Options.UseBackColor = True
    Me.frTipoOperazione.Controls.Add(Me.opTipoogglog2)
    Me.frTipoOperazione.Controls.Add(Me.opTipoogglog1)
    Me.frTipoOperazione.Controls.Add(Me.opTipoogglog0)
    Me.frTipoOperazione.Cursor = System.Windows.Forms.Cursors.Default
    Me.frTipoOperazione.Location = New System.Drawing.Point(337, 115)
    Me.frTipoOperazione.Name = "frTipoOperazione"
    Me.frTipoOperazione.Size = New System.Drawing.Size(172, 97)
    Me.frTipoOperazione.TabIndex = 17
    Me.frTipoOperazione.Text = "Tipo operazione: "
    '
    'opTipoogglog2
    '
    Me.opTipoogglog2.Cursor = System.Windows.Forms.Cursors.Hand
    Me.opTipoogglog2.EditValue = True
    Me.opTipoogglog2.Location = New System.Drawing.Point(11, 73)
    Me.opTipoogglog2.Name = "opTipoogglog2"
    Me.opTipoogglog2.NTSCheckValue = "S"
    Me.opTipoogglog2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTipoogglog2.Properties.Appearance.Options.UseBackColor = True
    Me.opTipoogglog2.Properties.Caption = "Entra&mbi"
    Me.opTipoogglog2.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTipoogglog2.Size = New System.Drawing.Size(95, 18)
    Me.opTipoogglog2.TabIndex = 2
    '
    'opTipoogglog1
    '
    Me.opTipoogglog1.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTipoogglog1.Location = New System.Drawing.Point(11, 48)
    Me.opTipoogglog1.Name = "opTipoogglog1"
    Me.opTipoogglog1.NTSCheckValue = "S"
    Me.opTipoogglog1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTipoogglog1.Properties.Appearance.Options.UseBackColor = True
    Me.opTipoogglog1.Properties.Caption = "&Elaborazioni"
    Me.opTipoogglog1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTipoogglog1.Size = New System.Drawing.Size(95, 18)
    Me.opTipoogglog1.TabIndex = 1
    '
    'opTipoogglog0
    '
    Me.opTipoogglog0.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTipoogglog0.Location = New System.Drawing.Point(11, 23)
    Me.opTipoogglog0.Name = "opTipoogglog0"
    Me.opTipoogglog0.NTSCheckValue = "S"
    Me.opTipoogglog0.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTipoogglog0.Properties.Appearance.Options.UseBackColor = True
    Me.opTipoogglog0.Properties.Caption = "&Documenti"
    Me.opTipoogglog0.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTipoogglog0.Size = New System.Drawing.Size(95, 18)
    Me.opTipoogglog0.TabIndex = 0
    '
    'frTipoModifica
    '
    Me.frTipoModifica.AllowDrop = True
    Me.frTipoModifica.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frTipoModifica.Appearance.Options.UseBackColor = True
    Me.frTipoModifica.Controls.Add(Me.opTipoop2)
    Me.frTipoModifica.Controls.Add(Me.opTipoop1)
    Me.frTipoModifica.Controls.Add(Me.opTipoop0)
    Me.frTipoModifica.Cursor = System.Windows.Forms.Cursors.Default
    Me.frTipoModifica.Location = New System.Drawing.Point(337, 9)
    Me.frTipoModifica.Name = "frTipoModifica"
    Me.frTipoModifica.Size = New System.Drawing.Size(172, 100)
    Me.frTipoModifica.TabIndex = 16
    Me.frTipoModifica.Text = "Tipo modifica: "
    '
    'opTipoop2
    '
    Me.opTipoop2.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTipoop2.Location = New System.Drawing.Point(11, 72)
    Me.opTipoop2.Name = "opTipoop2"
    Me.opTipoop2.NTSCheckValue = "S"
    Me.opTipoop2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTipoop2.Properties.Appearance.Options.UseBackColor = True
    Me.opTipoop2.Properties.Caption = "Ca&ncellazione"
    Me.opTipoop2.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTipoop2.Size = New System.Drawing.Size(95, 18)
    Me.opTipoop2.TabIndex = 2
    '
    'opTipoop1
    '
    Me.opTipoop1.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTipoop1.Location = New System.Drawing.Point(11, 48)
    Me.opTipoop1.Name = "opTipoop1"
    Me.opTipoop1.NTSCheckValue = "S"
    Me.opTipoop1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTipoop1.Properties.Appearance.Options.UseBackColor = True
    Me.opTipoop1.Properties.Caption = "&Modifica"
    Me.opTipoop1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTipoop1.Size = New System.Drawing.Size(95, 18)
    Me.opTipoop1.TabIndex = 1
    '
    'opTipoop0
    '
    Me.opTipoop0.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTipoop0.EditValue = True
    Me.opTipoop0.Location = New System.Drawing.Point(11, 26)
    Me.opTipoop0.Name = "opTipoop0"
    Me.opTipoop0.NTSCheckValue = "S"
    Me.opTipoop0.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTipoop0.Properties.Appearance.Options.UseBackColor = True
    Me.opTipoop0.Properties.Caption = "&Tutte"
    Me.opTipoop0.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTipoop0.Size = New System.Drawing.Size(95, 18)
    Me.opTipoop0.TabIndex = 0
    '
    'FRM__SVLO
    '
    Me.ClientSize = New System.Drawing.Size(638, 334)
    Me.Controls.Add(Me.pnSvlo)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.Name = "FRM__SVLO"
    Me.Text = "Selezione dati LOG attività"
    CType(Me.pnSvlo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSvlo.ResumeLayout(False)
    CType(Me.pnLeft, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnLeft.ResumeLayout(False)
    Me.pnLeft.PerformLayout()
    CType(Me.edDesogglog.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatfin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.PnLeftLeft, System.ComponentModel.ISupportInitialize).EndInit()
    Me.PnLeftLeft.ResumeLayout(False)
    CType(Me.frOrigineDati, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frOrigineDati.ResumeLayout(False)
    Me.frOrigineDati.PerformLayout()
    CType(Me.opOrigine1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opOrigine0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frFiltriPrincipali, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frFiltriPrincipali.ResumeLayout(False)
    Me.frFiltriPrincipali.PerformLayout()
    CType(Me.cbDesogglog.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodditt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSelCodditt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSelDesogglog.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOpnome.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frTipoOperazione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frTipoOperazione.ResumeLayout(False)
    Me.frTipoOperazione.PerformLayout()
    CType(Me.opTipoogglog2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opTipoogglog1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opTipoogglog0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frTipoModifica, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frTipoModifica.ResumeLayout(False)
    Me.frTipoModifica.PerformLayout()
    CType(Me.opTipoop2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opTipoop1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opTipoop0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------

      opTipoogglog2.NTSSetParam(oMenu, oApp.Tr(Me, 128340579224175651, "Entra&mbi"), "S")
      opTipoogglog1.NTSSetParam(oMenu, oApp.Tr(Me, 128340579224331820, "&Elaborazioni"), "S")
      opTipoogglog0.NTSSetParam(oMenu, oApp.Tr(Me, 128735740643232000, "&Documenti"), "S")
      opTipoop2.NTSSetParam(oMenu, oApp.Tr(Me, 128340579224487989, "Ca&ncellazione"), "S")
      opTipoop1.NTSSetParam(oMenu, oApp.Tr(Me, 128340579224644158, "&Modifica"), "S")
      opTipoop0.NTSSetParam(oMenu, oApp.Tr(Me, 128340579224800327, "&Tutte"), "S")
      edDesogglog.NTSSetParam(oMenu, oApp.Tr(Me, 128340579225268834, "Descrizione (LIKE)"), 0)
      edDatfin.NTSSetParam(oMenu, oApp.Tr(Me, 128340579225737341, "alla data"), False)
      edDatini.NTSSetParam(oMenu, oApp.Tr(Me, 128340579225893510, "Dalla data"), False)
      edCodditt.NTSSetParam(oMenu, oApp.Tr(Me, 128340579226674355, "Ditta"), 12)
      ckSelCodditt.NTSSetParam(oMenu, oApp.Tr(Me, 128735740706412000, "Selezi&ona ditta: "), "S", "N")
      cbDesogglog.NTSSetParam(oApp.Tr(Me, 128340579226986693, "Oggetto di LOG"))
      ckSelDesogglog.NTSSetParam(oMenu, oApp.Tr(Me, 128735740771308000, "&Seleziona oggetto di LOG:"), "S", "N")
      edOpnome.NTSSetParam(oMenu, oApp.Tr(Me, 128340579227299031, "Nome operatore"), 20)
      opOrigine1.NTSSetParam(oMenu, oApp.Tr(Me, 128340579227611369, "Da archivio &procedura"), "S")
      opOrigine0.NTSSetParam(oMenu, oApp.Tr(Me, 128340579227767538, "Da archivio a&zienda"), "S")

      ''non faccio lanciare lo zoom dal controllo visto che non devo cambiare il contenuto del campo ma riposizionare il dataset!!!

      edCodditt.Enabled = False
      cbDesogglog.Enabled = False

      edCodditt.NTSSetParamZoom("ZOOMTABANAZ")

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
  Public Overridable Sub InitEntity(ByRef cleVlog As CLE__VLOG)
    oCleVlog = cleVlog
    AddHandler oCleVlog.RemoteEvent, AddressOf GestisciEventiEntity
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

  Public Overridable Sub FRM__SVLO_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      '-------------------------------------------------
      'carico i combobox
      CaricaCombo()

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-----------------------------------------------------------------------------------------
      '--- Inizializza i controlli
      '-----------------------------------------------------------------------------------------
      oCleVlog.GetOrigineDati()
      Select Case oCleVlog.nOrigine
        Case 0
          opOrigine0.Checked = True
        Case 1
          opOrigine1.Checked = True
      End Select
      '-----------------------------------------------------------------------------------------
      edDatini.Text = DateTime.Now.ToShortDateString
      edDatfin.Text = "31/12/2099"

      oCleVlog.bSvloAnnullato = True

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      'e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipodocumento, ecc ...
      'GctlTipoDoc = ""
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckSelCodditt_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckSelCodditt.CheckedChanged
    Try
      If ckSelCodditt.Checked = True Then
        edCodditt.Enabled = True
      Else
        edCodditt.Text = ""
        edCodditt.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckSelDesogglog_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckSelDesogglog.CheckedChanged
    Try
      If ckSelDesogglog.Checked = True Then
        cbDesogglog.Enabled = True
      Else
        cbDesogglog.SelectedIndex = 0
        cbDesogglog.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      '-----------------------------------------------------------------------------------------
      If Not CheckSelection() Then Exit Sub
      '-----------------------------------------------------------------------------------------
      '--- Salva nel RECENT l'ultima origine dati utilizzata
      '-----------------------------------------------------------------------------------------
      If opOrigine0.Checked = True Then oCleVlog.nOrigine = 0 Else oCleVlog.nOrigine = 1
      oCleVlog.SetOrigineDati()
      '-----------------------------------------------------------------------------------------
      oCleVlog.bSvloAnnullato = False
      If opOrigine0.Checked = True Then oCleVlog.nSvloOrigine = 0
      If opOrigine1.Checked = True Then oCleVlog.nSvloOrigine = 1
      oCleVlog.strSvloOpnome = Trim(edOpnome.Text)
      oCleVlog.bSvloSelDesogglog = CBool(IIf(ckSelDesogglog.Checked = True, True, False))
      oCleVlog.strSvloDesogglog = strNomogglog
      oCleVlog.bSvloSelCodditt = CBool(IIf(ckSelCodditt.Checked = True, True, False))
      oCleVlog.strSvloCodditt = Trim(edCodditt.Text)
      oCleVlog.strSvloDatini = edDatini.Text
      oCleVlog.strSvloDatfin = edDatfin.Text
      oCleVlog.strSvloDesogglogLIKE = Trim(edDesogglog.Text)
      If opTipoop0.Checked = True Then oCleVlog.nSvloTipoop = 0
      If opTipoop1.Checked = True Then oCleVlog.nSvloTipoop = 1
      If opTipoop2.Checked = True Then oCleVlog.nSvloTipoop = 2
      If opTipoogglog0.Checked = True Then oCleVlog.nSvloTipoogglog = 0
      If opTipoogglog1.Checked = True Then oCleVlog.nSvloTipoogglog = 1
      If opTipoogglog2.Checked = True Then oCleVlog.nSvloTipoogglog = 2
      oCleVlog.strSvloSQLQuery = CostruisciWhere()
      '-----------------------------------------------------------------------------------------
      Me.Close()
      '-----------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      oCleVlog.bSvloAnnullato = True
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edCodditt_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCodditt.LostFocus
    Try
      If Not CheckCodditt() Then Exit Sub
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edOpnome_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles edOpnome.LostFocus
    Try
      If Not CheckOpnome() Then Exit Sub
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub opOrigine0_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles opOrigine0.CheckedChanged
    Try
      opOrigine_Click(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub opOrigine1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles opOrigine1.CheckedChanged
    Try
      opOrigine_Click(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

  Public Overridable Sub CaricaCombo()
    Dim bInserito As Boolean = False
    Dim i As Integer = 0
    Dim dttDesogglog As New DataTable
    Dim dsStructLog As DataSet = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Riempie il ComboBox con le voci indicate in STRUCTLOG
      '--------------------------------------------------------------------------------------------------------------
      oCleVlog.GetStructLog(dsStructLog)
      dttDesogglog.Columns.Add("cod", GetType(String))
      dttDesogglog.Columns.Add("val", GetType(String))
      For i = 0 To dsStructLog.Tables("STRUCTLOG").Rows.Count - 1
        '------------------------------------------------------------------------------------------------------------
        '--- Federico: ho fatto in questa maniera per non intervanire sui record in STRUCTLOG in ARCPROC
        '------------------------------------------------------------------------------------------------------------
        If (NTSCStr(dsStructLog.Tables("STRUCTLOG").Rows(i)!cb_desogglog) > "Anagrafica matricole (parco)") And _
           (bInserito = False) Then
          dttDesogglog.Rows.Add(New Object() {"NNMATRICS", "Anagrafica matricole (parco)"})
          dttDesogglog.AcceptChanges()
          bInserito = True
        End If
        dttDesogglog.Rows.Add(New Object() {NTSCStr(dsStructLog.Tables("STRUCTLOG").Rows(i)!cb_desogglog), NTSCStr(dsStructLog.Tables("STRUCTLOG").Rows(i)!cb_desogglog)})
        dttDesogglog.AcceptChanges()
      Next
      '--------------------------------------------------------------------------------------------------------------
      cbDesogglog.DataSource = dttDesogglog
      cbDesogglog.ValueMember = "cod"
      cbDesogglog.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub opOrigine_Click(ByVal i As Integer)
    Try
      Select Case i
        Case 0
          ckSelCodditt.Enabled = True
        Case 1
          ckSelCodditt.Checked = False
          ckSelCodditt.Enabled = False
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function CheckSelection() As Boolean
    Dim bConn As Boolean
    Dim dsStructLog As DataSet = Nothing
    Dim dsActLog As DataSet = Nothing
    Try
      '-----------------------------------------------------------------------------------------
      CheckSelection = False
      '-----------------------------------------------------------------------------------------
      '--- Controlla la validità dei dati
      '-----------------------------------------------------------------------------------------
      If Not CheckOpnome() Then Exit Function
      If Not CheckCodditt() Then Exit Function
      If Not CheckIntervalli() Then Exit Function
      '-----------------------------------------------------------------------------------------
      If opOrigine0.Checked = True Then
        bConn = True
      Else
        bConn = True
      End If
      '-----------------------------------------------------------------------------------------
      If ckSelDesogglog.Checked = True Then
        oCleVlog.CheckSelectionStructLog(NTSCStr(cbDesogglog.SelectedValue), dsStructLog)
        If dsStructLog.Tables("STRUCTLOG").Rows.Count > 0 Then
          strNomogglog = NTSCStr(dsStructLog.Tables("STRUCTLOG").Rows(0)!cb_nomogglog)
        Else
          strNomogglog = NTSCStr(cbDesogglog.SelectedValue)
        End If
      End If
      '-----------------------------------------------------------------------------------------
      '--- Controlla l'esistenza dei dati
      '-----------------------------------------------------------------------------------------
      oCleVlog.CheckSelectionActLog(edOpnome.Text, strNomogglog, edCodditt.Text, _
      ckSelDesogglog.Checked, ckSelCodditt.Checked, edDesogglog.Text, opTipoop1.Checked, opTipoop2.Checked, _
      opTipoogglog0.Checked, opTipoogglog1.Checked, edDatini.Text, edDatfin.Text, bConn, _
      opOrigine0.Checked, dsActLog)
      If dsActLog.Tables("ACTLOG").Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128339941065602917, "Non esistono dati con queste caratteristiche."))
        Exit Function
      End If
      '-----------------------------------------------------------------------------------------
      CheckSelection = True
      '-----------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckCodditt() As Boolean
    Dim dsTabanaz As DataSet = Nothing
    Try
      CheckCodditt = False
      edCodditt.Text = (Trim(edCodditt.Text)).ToUpper
      If edCodditt.Text <> "" Then
        oCleVlog.CheckCoddittSelect(edCodditt.Text, dsTabanaz)

        If dsTabanaz.Tables("TABANAZ").Rows.Count = 0 Then
          edCodditt.Text = ""
          oApp.MsgBoxErr(oApp.Tr(Me, 128339965546066871, "Codice ditta inesistente."))
          edCodditt.Focus()
          Exit Function
        End If
      End If
      CheckCodditt = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function CheckIntervalli() As Boolean
    Try
      CheckIntervalli = False
      If NTSCDate(edDatini.Text) > NTSCDate(edDatfin.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128339965744021684, "La data iniziale non può essere superiore a quella finale."))
        edDatini.Text = "01/01/1900"
        edDatfin.Text = "31/12/2099"
        Exit Function
      End If
      CheckIntervalli = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function CheckOpnome() As Boolean
    Dim dsOperat As DataSet = Nothing
    Try
      CheckOpnome = False
      edOpnome.Text = Trim(edOpnome.Text)
      If edOpnome.Text <> "" Then
        oCleVlog.CheckOpnomeSelect(edOpnome.Text, dsOperat)

        If dsOperat.Tables("OPERAT").Rows.Count = 0 Then
          edOpnome.Text = ""
          oApp.MsgBoxErr(oApp.Tr(Me, 128339966102746428, "Nome operatore inesistente."))
          edOpnome.Focus()
          Exit Function
        End If
      End If
      CheckOpnome = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function CostruisciWhere() As String
    Dim bConn As Boolean
    Dim strDatfinTmp As String
    Dim dsStructLog As DataSet = Nothing
    CostruisciWhere = ""
    Try
      If ckSelDesogglog.Checked = True Then
        oCleVlog.CostruisciWhereStructLog(cbDesogglog.SelectedValue, dsStructLog)
        If dsStructLog.Tables("STRUCTLOG").Rows.Count > 0 Then
          strNomogglog = NTSCStr(dsStructLog.Tables("STRUCTLOG").Rows(0)!cb_nomogglog)
        End If
      End If
      '-----------------------------------------------------------------------------------------
      '--- Controlla l'esistenza dei dati
      '-----------------------------------------------------------------------------------------
      CostruisciWhere = "SELECT * FROM actlog" & _
        " WHERE al_tabprim = 'S'"
      If Trim(edOpnome.Text) <> "" Then
        CostruisciWhere = CostruisciWhere & " AND al_opnome = " & CStrSQL(edOpnome.Text)
      End If
      If ckSelDesogglog.Checked = True Then
        If strNomogglog.indexof("*") > -1 Then
          CostruisciWhere = CostruisciWhere & " AND al_nomogglog like " & CStrSQL(strNomogglog.replace("*", "%"))
        Else
          CostruisciWhere = CostruisciWhere & " AND al_nomogglog = " & CStrSQL(strNomogglog)
        End If
      End If
      If ckSelCodditt.Checked = True Then
        If edCodditt.Text <> "" Then
          CostruisciWhere = CostruisciWhere & " AND codditt = " & CStrSQL(edCodditt.Text)
        End If
      End If
      If Trim(edDesogglog.Text) <> "" Then
        strDesogglog = Trim(edDesogglog.Text)
        If strDesogglog.Substring(0, 1) = "*" Then strDesogglog = strDesogglog.Substring(1)
        If strDesogglog.Substring(strDesogglog.Length - 1, 1) = "*" Then
          strDesogglog = strDesogglog.Substring(0, strDesogglog.Length - 1)
        End If
        If strDesogglog.Substring(0, 1) <> "%" Then strDesogglog = "%" & strDesogglog
        If strDesogglog.Substring(Len(strDesogglog) - 1, 1) <> "%" Then strDesogglog = strDesogglog & "%"
        CostruisciWhere = CostruisciWhere & " AND al_desogglog LIKE " & CStrSQL(strDesogglog)
      End If
      If opTipoop1.Checked = True Then CostruisciWhere = CostruisciWhere & " AND al_tipoop = 'M'"
      If opTipoop2.Checked = True Then CostruisciWhere = CostruisciWhere & " AND al_tipoop = 'A'"
      If opTipoogglog0.Checked = True Then CostruisciWhere = CostruisciWhere & " AND al_tipoogglog = 'D'"
      If opTipoogglog1.Checked = True Then CostruisciWhere = CostruisciWhere & " AND al_tipoogglog = 'E'"
      '-----------------------------------------------------------------------------------------
      If (NTSCDate(edDatini.Text) <> NTSCDate("01/01/1900")) Or (NTSCDate(edDatfin.Text) <> NTSCDate("31/12/2099")) Then
        If NTSCDate(edDatfin.Text) <> NTSCDate("31/12/2099") Then
          strDatfinTmp = (NTSCDate(edDatfin.Text).AddDays(1)).ToShortDateString
        Else
          strDatfinTmp = edDatfin.Text
        End If
        CostruisciWhere = CostruisciWhere & " AND al_data BETWEEN " & CDataSQL(edDatini.Text) & " AND " & CDataSQL(strDatfinTmp)
        If opOrigine0.Checked = True Then
          bConn = True
        Else
          bConn = True
        End If
      End If
      '-----------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

End Class