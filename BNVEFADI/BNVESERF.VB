Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVESERF
  Public oCallParams As CLE__CLDP
  Public bOk As Boolean = False
  Public oCleFadi As CLEVEFADI

  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents pnFiltri As NTSInformatica.NTSPanel
  Public WithEvents fmFiltriDocumento As NTSInformatica.NTSGroupBox
  Public WithEvents fmFiltriCliente As NTSInformatica.NTSGroupBox
  Public WithEvents edDatadoca As NTSInformatica.NTSTextBoxData
  Public WithEvents edDatadocda As NTSInformatica.NTSTextBoxData
  Public WithEvents edAnnodoc As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbTipodoc As NTSInformatica.NTSComboBox
  Public WithEvents lbAnnodoc As NTSInformatica.NTSLabel
  Public WithEvents lbTipodoc As NTSInformatica.NTSLabel
  Public WithEvents ckNumdoc As NTSInformatica.NTSCheckBox
  Public WithEvents edSeriedocda As NTSInformatica.NTSTextBoxStr
  Public WithEvents edSeriedoca As NTSInformatica.NTSTextBoxStr
  Public WithEvents edNumdoca As NTSInformatica.NTSTextBoxNum
  Public WithEvents edNumdocda As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAgentedoca As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAgentedocda As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAgente As NTSInformatica.NTSLabel
  Public WithEvents edNoteFattura As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbNoteFattura As NTSInformatica.NTSLabel
  Public WithEvents opCliLista As NTSInformatica.NTSRadioButton
  Public WithEvents opCliDaa As NTSInformatica.NTSRadioButton
  Public WithEvents opCliTutti As NTSInformatica.NTSRadioButton
  Public WithEvents cbCorrispond As NTSInformatica.NTSComboBox
  Public WithEvents cbUguale As NTSInformatica.NTSComboBox
  Public WithEvents ckModalCorr As NTSInformatica.NTSCheckBox
  Public WithEvents lbLista As NTSInformatica.NTSLabel
  Public WithEvents edLista As NTSInformatica.NTSTextBoxNum
  Public WithEvents edZonaa As NTSInformatica.NTSTextBoxNum
  Public WithEvents edZonada As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbZona As NTSInformatica.NTSLabel
  Public WithEvents edCategoriaa As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCategoriada As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCategoria As NTSInformatica.NTSLabel
  Public WithEvents edClientea As NTSInformatica.NTSTextBoxNum
  Public WithEvents edClienteda As NTSInformatica.NTSTextBoxNum
  Public WithEvents cmdOk As NTSInformatica.NTSButton

  'valori di ritorno
  Public strOutTestmag As String = ""
  Public strOutAnagra As String = ""
  Public strOutLista As String = ""
  Public lOutNumdocDa As Integer = 0
  Public lOutNumdocA As Integer = 0
  Public strOutSerieDa As String = ""
  Public WithEvents ckDatdoc As NTSInformatica.NTSCheckBox
  Public WithEvents ckProva As NTSInformatica.NTSCheckBox
  Public WithEvents lbNotLista As NTSInformatica.NTSLabel
  Public WithEvents edNotLista As NTSInformatica.NTSTextBoxNum
  Public WithEvents opCliNotLista As NTSInformatica.NTSRadioButton
  Public WithEvents lbContab As NTSInformatica.NTSLabel
  Public WithEvents cbContab As NTSInformatica.NTSComboBox
  Public strOutSerieA As String = ""

  Public Overridable Sub InitializeComponent()
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.pnFiltri = New NTSInformatica.NTSPanel
    Me.edNoteFattura = New NTSInformatica.NTSTextBoxStr
    Me.lbNoteFattura = New NTSInformatica.NTSLabel
    Me.fmFiltriCliente = New NTSInformatica.NTSGroupBox
    Me.lbNotLista = New NTSInformatica.NTSLabel
    Me.edNotLista = New NTSInformatica.NTSTextBoxNum
    Me.opCliNotLista = New NTSInformatica.NTSRadioButton
    Me.edClientea = New NTSInformatica.NTSTextBoxNum
    Me.edClienteda = New NTSInformatica.NTSTextBoxNum
    Me.cbCorrispond = New NTSInformatica.NTSComboBox
    Me.cbUguale = New NTSInformatica.NTSComboBox
    Me.ckModalCorr = New NTSInformatica.NTSCheckBox
    Me.lbLista = New NTSInformatica.NTSLabel
    Me.edLista = New NTSInformatica.NTSTextBoxNum
    Me.edZonaa = New NTSInformatica.NTSTextBoxNum
    Me.edZonada = New NTSInformatica.NTSTextBoxNum
    Me.lbZona = New NTSInformatica.NTSLabel
    Me.edCategoriaa = New NTSInformatica.NTSTextBoxNum
    Me.edCategoriada = New NTSInformatica.NTSTextBoxNum
    Me.lbCategoria = New NTSInformatica.NTSLabel
    Me.opCliLista = New NTSInformatica.NTSRadioButton
    Me.opCliDaa = New NTSInformatica.NTSRadioButton
    Me.opCliTutti = New NTSInformatica.NTSRadioButton
    Me.fmFiltriDocumento = New NTSInformatica.NTSGroupBox
    Me.lbContab = New NTSInformatica.NTSLabel
    Me.cbContab = New NTSInformatica.NTSComboBox
    Me.ckProva = New NTSInformatica.NTSCheckBox
    Me.ckDatdoc = New NTSInformatica.NTSCheckBox
    Me.edAgentedoca = New NTSInformatica.NTSTextBoxNum
    Me.edAgentedocda = New NTSInformatica.NTSTextBoxNum
    Me.lbAgente = New NTSInformatica.NTSLabel
    Me.ckNumdoc = New NTSInformatica.NTSCheckBox
    Me.edSeriedocda = New NTSInformatica.NTSTextBoxStr
    Me.edSeriedoca = New NTSInformatica.NTSTextBoxStr
    Me.edNumdoca = New NTSInformatica.NTSTextBoxNum
    Me.edNumdocda = New NTSInformatica.NTSTextBoxNum
    Me.edDatadoca = New NTSInformatica.NTSTextBoxData
    Me.edDatadocda = New NTSInformatica.NTSTextBoxData
    Me.edAnnodoc = New NTSInformatica.NTSTextBoxNum
    Me.cbTipodoc = New NTSInformatica.NTSComboBox
    Me.lbAnnodoc = New NTSInformatica.NTSLabel
    Me.lbTipodoc = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnFiltri, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFiltri.SuspendLayout()
    CType(Me.edNoteFattura.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmFiltriCliente, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmFiltriCliente.SuspendLayout()
    CType(Me.edNotLista.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opCliNotLista.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edClientea.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edClienteda.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbCorrispond.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbUguale.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckModalCorr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edLista.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edZonaa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edZonada.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCategoriaa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCategoriada.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opCliLista.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opCliDaa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opCliTutti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmFiltriDocumento, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmFiltriDocumento.SuspendLayout()
    CType(Me.cbContab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckProva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDatdoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAgentedoca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAgentedocda.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNumdoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSeriedocda.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSeriedoca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumdoca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumdocda.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatadoca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatadocda.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnnodoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipodoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(473, 36)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(105, 26)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImagePath = ""
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(473, 7)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.NTSContextMenu = Nothing
    Me.cmdOk.Size = New System.Drawing.Size(105, 26)
    Me.cmdOk.TabIndex = 2
    Me.cmdOk.Text = "&Conferma"
    '
    'pnFiltri
    '
    Me.pnFiltri.AllowDrop = True
    Me.pnFiltri.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnFiltri.Appearance.Options.UseBackColor = True
    Me.pnFiltri.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnFiltri.Controls.Add(Me.edNoteFattura)
    Me.pnFiltri.Controls.Add(Me.lbNoteFattura)
    Me.pnFiltri.Controls.Add(Me.fmFiltriCliente)
    Me.pnFiltri.Controls.Add(Me.fmFiltriDocumento)
    Me.pnFiltri.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnFiltri.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnFiltri.Location = New System.Drawing.Point(0, 0)
    Me.pnFiltri.Name = "pnFiltri"
    Me.pnFiltri.NTSActiveTrasparency = True
    Me.pnFiltri.Size = New System.Drawing.Size(467, 429)
    Me.pnFiltri.TabIndex = 3
    Me.pnFiltri.Text = "NtsPanel1"
    '
    'edNoteFattura
    '
    Me.edNoteFattura.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edNoteFattura.Location = New System.Drawing.Point(87, 178)
    Me.edNoteFattura.Name = "edNoteFattura"
    Me.edNoteFattura.NTSDbField = ""
    Me.edNoteFattura.NTSForzaVisZoom = False
    Me.edNoteFattura.NTSOldValue = ""
    Me.edNoteFattura.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNoteFattura.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNoteFattura.Properties.AutoHeight = False
    Me.edNoteFattura.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNoteFattura.Properties.MaxLength = 65536
    Me.edNoteFattura.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNoteFattura.Size = New System.Drawing.Size(360, 20)
    Me.edNoteFattura.TabIndex = 3
    '
    'lbNoteFattura
    '
    Me.lbNoteFattura.AutoSize = True
    Me.lbNoteFattura.BackColor = System.Drawing.Color.Transparent
    Me.lbNoteFattura.Location = New System.Drawing.Point(9, 181)
    Me.lbNoteFattura.Name = "lbNoteFattura"
    Me.lbNoteFattura.NTSDbField = ""
    Me.lbNoteFattura.Size = New System.Drawing.Size(67, 13)
    Me.lbNoteFattura.TabIndex = 2
    Me.lbNoteFattura.Text = "Note fattura"
    Me.lbNoteFattura.Tooltip = ""
    Me.lbNoteFattura.UseMnemonic = False
    '
    'fmFiltriCliente
    '
    Me.fmFiltriCliente.AllowDrop = True
    Me.fmFiltriCliente.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmFiltriCliente.Appearance.Options.UseBackColor = True
    Me.fmFiltriCliente.Controls.Add(Me.lbNotLista)
    Me.fmFiltriCliente.Controls.Add(Me.edNotLista)
    Me.fmFiltriCliente.Controls.Add(Me.opCliNotLista)
    Me.fmFiltriCliente.Controls.Add(Me.edClientea)
    Me.fmFiltriCliente.Controls.Add(Me.edClienteda)
    Me.fmFiltriCliente.Controls.Add(Me.cbCorrispond)
    Me.fmFiltriCliente.Controls.Add(Me.cbUguale)
    Me.fmFiltriCliente.Controls.Add(Me.ckModalCorr)
    Me.fmFiltriCliente.Controls.Add(Me.lbLista)
    Me.fmFiltriCliente.Controls.Add(Me.edLista)
    Me.fmFiltriCliente.Controls.Add(Me.edZonaa)
    Me.fmFiltriCliente.Controls.Add(Me.edZonada)
    Me.fmFiltriCliente.Controls.Add(Me.lbZona)
    Me.fmFiltriCliente.Controls.Add(Me.edCategoriaa)
    Me.fmFiltriCliente.Controls.Add(Me.edCategoriada)
    Me.fmFiltriCliente.Controls.Add(Me.lbCategoria)
    Me.fmFiltriCliente.Controls.Add(Me.opCliLista)
    Me.fmFiltriCliente.Controls.Add(Me.opCliDaa)
    Me.fmFiltriCliente.Controls.Add(Me.opCliTutti)
    Me.fmFiltriCliente.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmFiltriCliente.Location = New System.Drawing.Point(3, 211)
    Me.fmFiltriCliente.Name = "fmFiltriCliente"
    Me.fmFiltriCliente.Size = New System.Drawing.Size(461, 213)
    Me.fmFiltriCliente.TabIndex = 1
    Me.fmFiltriCliente.Text = "Filtri cliente"
    '
    'lbNotLista
    '
    Me.lbNotLista.BackColor = System.Drawing.Color.Transparent
    Me.lbNotLista.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbNotLista.Location = New System.Drawing.Point(235, 99)
    Me.lbNotLista.Name = "lbNotLista"
    Me.lbNotLista.NTSDbField = ""
    Me.lbNotLista.Size = New System.Drawing.Size(209, 20)
    Me.lbNotLista.TabIndex = 30
    Me.lbNotLista.Tooltip = ""
    Me.lbNotLista.UseMnemonic = False
    '
    'edNotLista
    '
    Me.edNotLista.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNotLista.EditValue = "0"
    Me.edNotLista.Location = New System.Drawing.Point(182, 100)
    Me.edNotLista.Name = "edNotLista"
    Me.edNotLista.NTSDbField = ""
    Me.edNotLista.NTSFormat = "0"
    Me.edNotLista.NTSForzaVisZoom = False
    Me.edNotLista.NTSOldValue = ""
    Me.edNotLista.Properties.Appearance.Options.UseTextOptions = True
    Me.edNotLista.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNotLista.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNotLista.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNotLista.Properties.AutoHeight = False
    Me.edNotLista.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNotLista.Properties.MaxLength = 65536
    Me.edNotLista.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNotLista.Size = New System.Drawing.Size(49, 20)
    Me.edNotLista.TabIndex = 29
    '
    'opCliNotLista
    '
    Me.opCliNotLista.Cursor = System.Windows.Forms.Cursors.Default
    Me.opCliNotLista.Location = New System.Drawing.Point(13, 100)
    Me.opCliNotLista.Name = "opCliNotLista"
    Me.opCliNotLista.NTSCheckValue = "S"
    Me.opCliNotLista.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opCliNotLista.Properties.Appearance.Options.UseBackColor = True
    Me.opCliNotLista.Properties.AutoHeight = False
    Me.opCliNotLista.Properties.Caption = "Tutti tranne quelli di lista sel."
    Me.opCliNotLista.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opCliNotLista.Size = New System.Drawing.Size(167, 19)
    Me.opCliNotLista.TabIndex = 28
    '
    'edClientea
    '
    Me.edClientea.Cursor = System.Windows.Forms.Cursors.Default
    Me.edClientea.EditValue = "999999999"
    Me.edClientea.Location = New System.Drawing.Point(344, 48)
    Me.edClientea.Name = "edClientea"
    Me.edClientea.NTSDbField = ""
    Me.edClientea.NTSFormat = "0"
    Me.edClientea.NTSForzaVisZoom = False
    Me.edClientea.NTSOldValue = "999999999"
    Me.edClientea.Properties.Appearance.Options.UseTextOptions = True
    Me.edClientea.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edClientea.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edClientea.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edClientea.Properties.AutoHeight = False
    Me.edClientea.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edClientea.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edClientea.Size = New System.Drawing.Size(100, 20)
    Me.edClientea.TabIndex = 27
    '
    'edClienteda
    '
    Me.edClienteda.Cursor = System.Windows.Forms.Cursors.Default
    Me.edClienteda.EditValue = "0"
    Me.edClienteda.Location = New System.Drawing.Point(182, 48)
    Me.edClienteda.Name = "edClienteda"
    Me.edClienteda.NTSDbField = ""
    Me.edClienteda.NTSFormat = "0"
    Me.edClienteda.NTSForzaVisZoom = False
    Me.edClienteda.NTSOldValue = ""
    Me.edClienteda.Properties.Appearance.Options.UseTextOptions = True
    Me.edClienteda.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edClienteda.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edClienteda.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edClienteda.Properties.AutoHeight = False
    Me.edClienteda.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edClienteda.Properties.MaxLength = 65536
    Me.edClienteda.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edClienteda.Size = New System.Drawing.Size(100, 20)
    Me.edClienteda.TabIndex = 26
    '
    'cbCorrispond
    '
    Me.cbCorrispond.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbCorrispond.DataSource = Nothing
    Me.cbCorrispond.DisplayMember = ""
    Me.cbCorrispond.Location = New System.Drawing.Point(288, 181)
    Me.cbCorrispond.Name = "cbCorrispond"
    Me.cbCorrispond.NTSDbField = ""
    Me.cbCorrispond.Properties.AutoHeight = False
    Me.cbCorrispond.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbCorrispond.Properties.DropDownRows = 30
    Me.cbCorrispond.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbCorrispond.SelectedValue = ""
    Me.cbCorrispond.Size = New System.Drawing.Size(156, 20)
    Me.cbCorrispond.TabIndex = 25
    Me.cbCorrispond.ValueMember = ""
    '
    'cbUguale
    '
    Me.cbUguale.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbUguale.DataSource = Nothing
    Me.cbUguale.DisplayMember = ""
    Me.cbUguale.Location = New System.Drawing.Point(182, 181)
    Me.cbUguale.Name = "cbUguale"
    Me.cbUguale.NTSDbField = ""
    Me.cbUguale.Properties.AutoHeight = False
    Me.cbUguale.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbUguale.Properties.DropDownRows = 30
    Me.cbUguale.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbUguale.SelectedValue = ""
    Me.cbUguale.Size = New System.Drawing.Size(100, 20)
    Me.cbUguale.TabIndex = 24
    Me.cbUguale.ValueMember = ""
    '
    'ckModalCorr
    '
    Me.ckModalCorr.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckModalCorr.Location = New System.Drawing.Point(13, 181)
    Me.ckModalCorr.Name = "ckModalCorr"
    Me.ckModalCorr.NTSCheckValue = "S"
    Me.ckModalCorr.NTSUnCheckValue = "N"
    Me.ckModalCorr.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckModalCorr.Properties.Appearance.Options.UseBackColor = True
    Me.ckModalCorr.Properties.AutoHeight = False
    Me.ckModalCorr.Properties.Caption = "Filtra modalità corrispondenza"
    Me.ckModalCorr.Size = New System.Drawing.Size(167, 19)
    Me.ckModalCorr.TabIndex = 23
    '
    'lbLista
    '
    Me.lbLista.BackColor = System.Drawing.Color.Transparent
    Me.lbLista.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbLista.Location = New System.Drawing.Point(235, 73)
    Me.lbLista.Name = "lbLista"
    Me.lbLista.NTSDbField = ""
    Me.lbLista.Size = New System.Drawing.Size(209, 20)
    Me.lbLista.TabIndex = 22
    Me.lbLista.Tooltip = ""
    Me.lbLista.UseMnemonic = False
    '
    'edLista
    '
    Me.edLista.Cursor = System.Windows.Forms.Cursors.Default
    Me.edLista.EditValue = "0"
    Me.edLista.Location = New System.Drawing.Point(182, 74)
    Me.edLista.Name = "edLista"
    Me.edLista.NTSDbField = ""
    Me.edLista.NTSFormat = "0"
    Me.edLista.NTSForzaVisZoom = False
    Me.edLista.NTSOldValue = ""
    Me.edLista.Properties.Appearance.Options.UseTextOptions = True
    Me.edLista.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edLista.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edLista.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edLista.Properties.AutoHeight = False
    Me.edLista.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edLista.Properties.MaxLength = 65536
    Me.edLista.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edLista.Size = New System.Drawing.Size(49, 20)
    Me.edLista.TabIndex = 21
    '
    'edZonaa
    '
    Me.edZonaa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edZonaa.EditValue = "999"
    Me.edZonaa.Location = New System.Drawing.Point(344, 155)
    Me.edZonaa.Name = "edZonaa"
    Me.edZonaa.NTSDbField = ""
    Me.edZonaa.NTSFormat = "0"
    Me.edZonaa.NTSForzaVisZoom = False
    Me.edZonaa.NTSOldValue = "999"
    Me.edZonaa.Properties.Appearance.Options.UseTextOptions = True
    Me.edZonaa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edZonaa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edZonaa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edZonaa.Properties.AutoHeight = False
    Me.edZonaa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edZonaa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edZonaa.Size = New System.Drawing.Size(100, 20)
    Me.edZonaa.TabIndex = 20
    '
    'edZonada
    '
    Me.edZonada.Cursor = System.Windows.Forms.Cursors.Default
    Me.edZonada.EditValue = "0"
    Me.edZonada.Location = New System.Drawing.Point(182, 155)
    Me.edZonada.Name = "edZonada"
    Me.edZonada.NTSDbField = ""
    Me.edZonada.NTSFormat = "0"
    Me.edZonada.NTSForzaVisZoom = False
    Me.edZonada.NTSOldValue = ""
    Me.edZonada.Properties.Appearance.Options.UseTextOptions = True
    Me.edZonada.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edZonada.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edZonada.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edZonada.Properties.AutoHeight = False
    Me.edZonada.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edZonada.Properties.MaxLength = 65536
    Me.edZonada.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edZonada.Size = New System.Drawing.Size(100, 20)
    Me.edZonada.TabIndex = 19
    '
    'lbZona
    '
    Me.lbZona.AutoSize = True
    Me.lbZona.BackColor = System.Drawing.Color.Transparent
    Me.lbZona.Location = New System.Drawing.Point(10, 158)
    Me.lbZona.Name = "lbZona"
    Me.lbZona.NTSDbField = ""
    Me.lbZona.Size = New System.Drawing.Size(64, 13)
    Me.lbZona.TabIndex = 18
    Me.lbZona.Text = "Zona Da / A"
    Me.lbZona.Tooltip = ""
    Me.lbZona.UseMnemonic = False
    '
    'edCategoriaa
    '
    Me.edCategoriaa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCategoriaa.EditValue = "999"
    Me.edCategoriaa.Location = New System.Drawing.Point(344, 129)
    Me.edCategoriaa.Name = "edCategoriaa"
    Me.edCategoriaa.NTSDbField = ""
    Me.edCategoriaa.NTSFormat = "0"
    Me.edCategoriaa.NTSForzaVisZoom = False
    Me.edCategoriaa.NTSOldValue = "999"
    Me.edCategoriaa.Properties.Appearance.Options.UseTextOptions = True
    Me.edCategoriaa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCategoriaa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCategoriaa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCategoriaa.Properties.AutoHeight = False
    Me.edCategoriaa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCategoriaa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCategoriaa.Size = New System.Drawing.Size(100, 20)
    Me.edCategoriaa.TabIndex = 17
    '
    'edCategoriada
    '
    Me.edCategoriada.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edCategoriada.EditValue = "0"
    Me.edCategoriada.Location = New System.Drawing.Point(182, 129)
    Me.edCategoriada.Name = "edCategoriada"
    Me.edCategoriada.NTSDbField = ""
    Me.edCategoriada.NTSFormat = "0"
    Me.edCategoriada.NTSForzaVisZoom = False
    Me.edCategoriada.NTSOldValue = ""
    Me.edCategoriada.Properties.Appearance.Options.UseTextOptions = True
    Me.edCategoriada.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCategoriada.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCategoriada.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCategoriada.Properties.AutoHeight = False
    Me.edCategoriada.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCategoriada.Properties.MaxLength = 65536
    Me.edCategoriada.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCategoriada.Size = New System.Drawing.Size(100, 20)
    Me.edCategoriada.TabIndex = 16
    '
    'lbCategoria
    '
    Me.lbCategoria.AutoSize = True
    Me.lbCategoria.BackColor = System.Drawing.Color.Transparent
    Me.lbCategoria.Location = New System.Drawing.Point(10, 132)
    Me.lbCategoria.Name = "lbCategoria"
    Me.lbCategoria.NTSDbField = ""
    Me.lbCategoria.Size = New System.Drawing.Size(87, 13)
    Me.lbCategoria.TabIndex = 15
    Me.lbCategoria.Text = "Categoria Da / A"
    Me.lbCategoria.Tooltip = ""
    Me.lbCategoria.UseMnemonic = False
    '
    'opCliLista
    '
    Me.opCliLista.Cursor = System.Windows.Forms.Cursors.Default
    Me.opCliLista.Location = New System.Drawing.Point(13, 74)
    Me.opCliLista.Name = "opCliLista"
    Me.opCliLista.NTSCheckValue = "S"
    Me.opCliLista.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opCliLista.Properties.Appearance.Options.UseBackColor = True
    Me.opCliLista.Properties.AutoHeight = False
    Me.opCliLista.Properties.Caption = "Da lista selezionata"
    Me.opCliLista.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opCliLista.Size = New System.Drawing.Size(113, 19)
    Me.opCliLista.TabIndex = 2
    '
    'opCliDaa
    '
    Me.opCliDaa.Cursor = System.Windows.Forms.Cursors.Default
    Me.opCliDaa.Location = New System.Drawing.Point(13, 49)
    Me.opCliDaa.Name = "opCliDaa"
    Me.opCliDaa.NTSCheckValue = "S"
    Me.opCliDaa.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opCliDaa.Properties.Appearance.Options.UseBackColor = True
    Me.opCliDaa.Properties.AutoHeight = False
    Me.opCliDaa.Properties.Caption = "Dal / Al cliente"
    Me.opCliDaa.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opCliDaa.Size = New System.Drawing.Size(95, 19)
    Me.opCliDaa.TabIndex = 1
    '
    'opCliTutti
    '
    Me.opCliTutti.Cursor = System.Windows.Forms.Cursors.Default
    Me.opCliTutti.EditValue = True
    Me.opCliTutti.Location = New System.Drawing.Point(13, 24)
    Me.opCliTutti.Name = "opCliTutti"
    Me.opCliTutti.NTSCheckValue = "S"
    Me.opCliTutti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opCliTutti.Properties.Appearance.Options.UseBackColor = True
    Me.opCliTutti.Properties.AutoHeight = False
    Me.opCliTutti.Properties.Caption = "Tutti"
    Me.opCliTutti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opCliTutti.Size = New System.Drawing.Size(48, 19)
    Me.opCliTutti.TabIndex = 0
    '
    'fmFiltriDocumento
    '
    Me.fmFiltriDocumento.AllowDrop = True
    Me.fmFiltriDocumento.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmFiltriDocumento.Appearance.Options.UseBackColor = True
    Me.fmFiltriDocumento.Controls.Add(Me.lbContab)
    Me.fmFiltriDocumento.Controls.Add(Me.cbContab)
    Me.fmFiltriDocumento.Controls.Add(Me.ckProva)
    Me.fmFiltriDocumento.Controls.Add(Me.ckDatdoc)
    Me.fmFiltriDocumento.Controls.Add(Me.edAgentedoca)
    Me.fmFiltriDocumento.Controls.Add(Me.edAgentedocda)
    Me.fmFiltriDocumento.Controls.Add(Me.lbAgente)
    Me.fmFiltriDocumento.Controls.Add(Me.ckNumdoc)
    Me.fmFiltriDocumento.Controls.Add(Me.edSeriedocda)
    Me.fmFiltriDocumento.Controls.Add(Me.edSeriedoca)
    Me.fmFiltriDocumento.Controls.Add(Me.edNumdoca)
    Me.fmFiltriDocumento.Controls.Add(Me.edNumdocda)
    Me.fmFiltriDocumento.Controls.Add(Me.edDatadoca)
    Me.fmFiltriDocumento.Controls.Add(Me.edDatadocda)
    Me.fmFiltriDocumento.Controls.Add(Me.edAnnodoc)
    Me.fmFiltriDocumento.Controls.Add(Me.cbTipodoc)
    Me.fmFiltriDocumento.Controls.Add(Me.lbAnnodoc)
    Me.fmFiltriDocumento.Controls.Add(Me.lbTipodoc)
    Me.fmFiltriDocumento.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmFiltriDocumento.Location = New System.Drawing.Point(3, 7)
    Me.fmFiltriDocumento.Name = "fmFiltriDocumento"
    Me.fmFiltriDocumento.Size = New System.Drawing.Size(461, 161)
    Me.fmFiltriDocumento.TabIndex = 0
    Me.fmFiltriDocumento.Text = "Filtri documento"
    '
    'lbContab
    '
    Me.lbContab.AutoSize = True
    Me.lbContab.BackColor = System.Drawing.Color.Transparent
    Me.lbContab.Location = New System.Drawing.Point(10, 138)
    Me.lbContab.Name = "lbContab"
    Me.lbContab.NTSDbField = ""
    Me.lbContab.Size = New System.Drawing.Size(70, 13)
    Me.lbContab.TabIndex = 18
    Me.lbContab.Text = "Contabilizzati"
    Me.lbContab.Tooltip = ""
    Me.lbContab.UseMnemonic = False
    '
    'cbContab
    '
    Me.cbContab.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbContab.DataSource = Nothing
    Me.cbContab.DisplayMember = ""
    Me.cbContab.Location = New System.Drawing.Point(84, 135)
    Me.cbContab.Name = "cbContab"
    Me.cbContab.NTSDbField = ""
    Me.cbContab.Properties.AutoHeight = False
    Me.cbContab.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbContab.Properties.DropDownRows = 30
    Me.cbContab.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbContab.SelectedValue = ""
    Me.cbContab.Size = New System.Drawing.Size(226, 20)
    Me.cbContab.TabIndex = 17
    Me.cbContab.ValueMember = ""
    '
    'ckProva
    '
    Me.ckProva.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckProva.Location = New System.Drawing.Point(317, 137)
    Me.ckProva.Name = "ckProva"
    Me.ckProva.NTSCheckValue = "S"
    Me.ckProva.NTSUnCheckValue = "N"
    Me.ckProva.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckProva.Properties.Appearance.Options.UseBackColor = True
    Me.ckProva.Properties.AutoHeight = False
    Me.ckProva.Properties.Caption = "Elaborazioni di prova"
    Me.ckProva.Size = New System.Drawing.Size(127, 19)
    Me.ckProva.TabIndex = 16
    '
    'ckDatdoc
    '
    Me.ckDatdoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDatdoc.Location = New System.Drawing.Point(9, 58)
    Me.ckDatdoc.Name = "ckDatdoc"
    Me.ckDatdoc.NTSCheckValue = "S"
    Me.ckDatdoc.NTSUnCheckValue = "N"
    Me.ckDatdoc.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDatdoc.Properties.Appearance.Options.UseBackColor = True
    Me.ckDatdoc.Properties.AutoHeight = False
    Me.ckDatdoc.Properties.Caption = "Filtra data documento DA / A"
    Me.ckDatdoc.Size = New System.Drawing.Size(167, 19)
    Me.ckDatdoc.TabIndex = 15
    '
    'edAgentedoca
    '
    Me.edAgentedoca.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAgentedoca.EditValue = "9999"
    Me.edAgentedoca.Location = New System.Drawing.Point(316, 109)
    Me.edAgentedoca.Name = "edAgentedoca"
    Me.edAgentedoca.NTSDbField = ""
    Me.edAgentedoca.NTSFormat = "0"
    Me.edAgentedoca.NTSForzaVisZoom = False
    Me.edAgentedoca.NTSOldValue = "9999"
    Me.edAgentedoca.Properties.Appearance.Options.UseTextOptions = True
    Me.edAgentedoca.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAgentedoca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAgentedoca.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAgentedoca.Properties.AutoHeight = False
    Me.edAgentedoca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAgentedoca.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAgentedoca.Size = New System.Drawing.Size(128, 20)
    Me.edAgentedoca.TabIndex = 14
    '
    'edAgentedocda
    '
    Me.edAgentedocda.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAgentedocda.EditValue = "0"
    Me.edAgentedocda.Location = New System.Drawing.Point(182, 109)
    Me.edAgentedocda.Name = "edAgentedocda"
    Me.edAgentedocda.NTSDbField = ""
    Me.edAgentedocda.NTSFormat = "0"
    Me.edAgentedocda.NTSForzaVisZoom = False
    Me.edAgentedocda.NTSOldValue = ""
    Me.edAgentedocda.Properties.Appearance.Options.UseTextOptions = True
    Me.edAgentedocda.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAgentedocda.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAgentedocda.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAgentedocda.Properties.AutoHeight = False
    Me.edAgentedocda.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAgentedocda.Properties.MaxLength = 65536
    Me.edAgentedocda.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAgentedocda.Size = New System.Drawing.Size(128, 20)
    Me.edAgentedocda.TabIndex = 13
    '
    'lbAgente
    '
    Me.lbAgente.AutoSize = True
    Me.lbAgente.BackColor = System.Drawing.Color.Transparent
    Me.lbAgente.Location = New System.Drawing.Point(10, 112)
    Me.lbAgente.Name = "lbAgente"
    Me.lbAgente.NTSDbField = ""
    Me.lbAgente.Size = New System.Drawing.Size(84, 13)
    Me.lbAgente.TabIndex = 12
    Me.lbAgente.Text = "Agente 1 Da / A"
    Me.lbAgente.Tooltip = ""
    Me.lbAgente.UseMnemonic = False
    '
    'ckNumdoc
    '
    Me.ckNumdoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckNumdoc.Location = New System.Drawing.Point(9, 84)
    Me.ckNumdoc.Name = "ckNumdoc"
    Me.ckNumdoc.NTSCheckValue = "S"
    Me.ckNumdoc.NTSUnCheckValue = "N"
    Me.ckNumdoc.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNumdoc.Properties.Appearance.Options.UseBackColor = True
    Me.ckNumdoc.Properties.AutoHeight = False
    Me.ckNumdoc.Properties.Caption = "Filtra numero / serie docum."
    Me.ckNumdoc.Size = New System.Drawing.Size(167, 19)
    Me.ckNumdoc.TabIndex = 11
    '
    'edSeriedocda
    '
    Me.edSeriedocda.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSeriedocda.EditValue = " "
    Me.edSeriedocda.Location = New System.Drawing.Point(265, 83)
    Me.edSeriedocda.Name = "edSeriedocda"
    Me.edSeriedocda.NTSDbField = ""
    Me.edSeriedocda.NTSForzaVisZoom = False
    Me.edSeriedocda.NTSOldValue = " "
    Me.edSeriedocda.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSeriedocda.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSeriedocda.Properties.AutoHeight = False
    Me.edSeriedocda.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSeriedocda.Properties.MaxLength = 65536
    Me.edSeriedocda.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSeriedocda.Size = New System.Drawing.Size(45, 20)
    Me.edSeriedocda.TabIndex = 10
    '
    'edSeriedoca
    '
    Me.edSeriedoca.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSeriedoca.EditValue = "Z"
    Me.edSeriedoca.Location = New System.Drawing.Point(399, 83)
    Me.edSeriedoca.Name = "edSeriedoca"
    Me.edSeriedoca.NTSDbField = ""
    Me.edSeriedoca.NTSForzaVisZoom = False
    Me.edSeriedoca.NTSOldValue = "Z"
    Me.edSeriedoca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSeriedoca.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSeriedoca.Properties.AutoHeight = False
    Me.edSeriedoca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSeriedoca.Properties.MaxLength = 65536
    Me.edSeriedoca.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSeriedoca.Size = New System.Drawing.Size(45, 20)
    Me.edSeriedoca.TabIndex = 9
    '
    'edNumdoca
    '
    Me.edNumdoca.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumdoca.EditValue = "999999999"
    Me.edNumdoca.Location = New System.Drawing.Point(316, 83)
    Me.edNumdoca.Name = "edNumdoca"
    Me.edNumdoca.NTSDbField = ""
    Me.edNumdoca.NTSFormat = "0"
    Me.edNumdoca.NTSForzaVisZoom = False
    Me.edNumdoca.NTSOldValue = "999999999"
    Me.edNumdoca.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumdoca.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumdoca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNumdoca.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNumdoca.Properties.AutoHeight = False
    Me.edNumdoca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumdoca.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumdoca.Size = New System.Drawing.Size(80, 20)
    Me.edNumdoca.TabIndex = 8
    '
    'edNumdocda
    '
    Me.edNumdocda.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumdocda.EditValue = "0"
    Me.edNumdocda.Location = New System.Drawing.Point(182, 83)
    Me.edNumdocda.Name = "edNumdocda"
    Me.edNumdocda.NTSDbField = ""
    Me.edNumdocda.NTSFormat = "0"
    Me.edNumdocda.NTSForzaVisZoom = False
    Me.edNumdocda.NTSOldValue = ""
    Me.edNumdocda.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumdocda.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumdocda.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNumdocda.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNumdocda.Properties.AutoHeight = False
    Me.edNumdocda.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumdocda.Properties.MaxLength = 65536
    Me.edNumdocda.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumdocda.Size = New System.Drawing.Size(80, 20)
    Me.edNumdocda.TabIndex = 7
    '
    'edDatadoca
    '
    Me.edDatadoca.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatadoca.EditValue = "31/12/2099"
    Me.edDatadoca.Location = New System.Drawing.Point(316, 57)
    Me.edDatadoca.Name = "edDatadoca"
    Me.edDatadoca.NTSDbField = ""
    Me.edDatadoca.NTSForzaVisZoom = False
    Me.edDatadoca.NTSOldValue = ""
    Me.edDatadoca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatadoca.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatadoca.Properties.AutoHeight = False
    Me.edDatadoca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatadoca.Properties.MaxLength = 65536
    Me.edDatadoca.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatadoca.Size = New System.Drawing.Size(128, 20)
    Me.edDatadoca.TabIndex = 6
    '
    'edDatadocda
    '
    Me.edDatadocda.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatadocda.EditValue = "01/01/1900"
    Me.edDatadocda.Location = New System.Drawing.Point(182, 57)
    Me.edDatadocda.Name = "edDatadocda"
    Me.edDatadocda.NTSDbField = ""
    Me.edDatadocda.NTSForzaVisZoom = False
    Me.edDatadocda.NTSOldValue = ""
    Me.edDatadocda.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatadocda.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatadocda.Properties.AutoHeight = False
    Me.edDatadocda.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatadocda.Properties.MaxLength = 65536
    Me.edDatadocda.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatadocda.Size = New System.Drawing.Size(128, 20)
    Me.edDatadocda.TabIndex = 5
    '
    'edAnnodoc
    '
    Me.edAnnodoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnnodoc.EditValue = "2008"
    Me.edAnnodoc.Location = New System.Drawing.Point(390, 29)
    Me.edAnnodoc.Name = "edAnnodoc"
    Me.edAnnodoc.NTSDbField = ""
    Me.edAnnodoc.NTSFormat = "0"
    Me.edAnnodoc.NTSForzaVisZoom = False
    Me.edAnnodoc.NTSOldValue = "2008"
    Me.edAnnodoc.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnnodoc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnnodoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnnodoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnnodoc.Properties.AutoHeight = False
    Me.edAnnodoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnnodoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnnodoc.Size = New System.Drawing.Size(54, 20)
    Me.edAnnodoc.TabIndex = 4
    '
    'cbTipodoc
    '
    Me.cbTipodoc.Cursor = System.Windows.Forms.Cursors.Hand
    Me.cbTipodoc.DataSource = Nothing
    Me.cbTipodoc.DisplayMember = ""
    Me.cbTipodoc.Location = New System.Drawing.Point(84, 29)
    Me.cbTipodoc.Name = "cbTipodoc"
    Me.cbTipodoc.NTSDbField = ""
    Me.cbTipodoc.Properties.AutoHeight = False
    Me.cbTipodoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipodoc.Properties.DropDownRows = 30
    Me.cbTipodoc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipodoc.SelectedValue = ""
    Me.cbTipodoc.Size = New System.Drawing.Size(226, 20)
    Me.cbTipodoc.TabIndex = 3
    Me.cbTipodoc.ValueMember = ""
    '
    'lbAnnodoc
    '
    Me.lbAnnodoc.AutoSize = True
    Me.lbAnnodoc.BackColor = System.Drawing.Color.Transparent
    Me.lbAnnodoc.Location = New System.Drawing.Point(341, 32)
    Me.lbAnnodoc.Name = "lbAnnodoc"
    Me.lbAnnodoc.NTSDbField = ""
    Me.lbAnnodoc.Size = New System.Drawing.Size(32, 13)
    Me.lbAnnodoc.TabIndex = 1
    Me.lbAnnodoc.Text = "Anno"
    Me.lbAnnodoc.Tooltip = ""
    Me.lbAnnodoc.UseMnemonic = False
    '
    'lbTipodoc
    '
    Me.lbTipodoc.AutoSize = True
    Me.lbTipodoc.BackColor = System.Drawing.Color.Transparent
    Me.lbTipodoc.Location = New System.Drawing.Point(10, 32)
    Me.lbTipodoc.Name = "lbTipodoc"
    Me.lbTipodoc.NTSDbField = ""
    Me.lbTipodoc.Size = New System.Drawing.Size(27, 13)
    Me.lbTipodoc.TabIndex = 0
    Me.lbTipodoc.Text = "Tipo"
    Me.lbTipodoc.Tooltip = ""
    Me.lbTipodoc.UseMnemonic = False
    '
    'FRMVESERF
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(586, 429)
    Me.Controls.Add(Me.pnFiltri)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMVESERF"
    Me.Text = "APRI DOCUMENTI RIEPILOGATIVI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnFiltri, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFiltri.ResumeLayout(False)
    Me.pnFiltri.PerformLayout()
    CType(Me.edNoteFattura.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmFiltriCliente, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmFiltriCliente.ResumeLayout(False)
    Me.fmFiltriCliente.PerformLayout()
    CType(Me.edNotLista.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opCliNotLista.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edClientea.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edClienteda.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbCorrispond.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbUguale.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckModalCorr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edLista.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edZonaa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edZonada.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCategoriaa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCategoriada.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opCliLista.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opCliDaa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opCliTutti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmFiltriDocumento, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmFiltriDocumento.ResumeLayout(False)
    Me.fmFiltriDocumento.PerformLayout()
    CType(Me.cbContab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckProva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDatdoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAgentedoca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAgentedocda.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNumdoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSeriedocda.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSeriedoca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNumdoca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNumdocda.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatadoca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatadocda.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnnodoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipodoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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

    InitializeComponent()
    Me.MinimumSize = Me.Size

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttTipcorr As New DataTable()
    Dim dttUguale As New DataTable()
    Dim dttTipodoc As New DataTable()
    Dim dttContab As New DataTable()
    Try
      '--------------------------
      'carico i combobox
      dttTipcorr.Columns.Add("cod", GetType(String))
      dttTipcorr.Columns.Add("val", GetType(String))
      dttTipcorr.Rows.Add(New Object() {"S", "E-mail Internet"})
      dttTipcorr.Rows.Add(New Object() {"X", "Fax service Win XP/2003"})
      dttTipcorr.Rows.Add(New Object() {"Y", "Fax service Win 2000 (locale)"})
      dttTipcorr.Rows.Add(New Object() {"N", "Microsoft Fax (mapi)"})
      dttTipcorr.Rows.Add(New Object() {"Z", "Zetafax MAPI"})
      dttTipcorr.Rows.Add(New Object() {"H", "HylaFAX"})
      dttTipcorr.AcceptChanges()
      cbCorrispond.DataSource = dttTipcorr
      cbCorrispond.ValueMember = "cod"
      cbCorrispond.DisplayMember = "val"

      dttUguale.Columns.Add("cod", GetType(String))
      dttUguale.Columns.Add("val", GetType(String))
      dttUguale.Rows.Add(New Object() {"0", "Uguale a"})
      dttUguale.Rows.Add(New Object() {"1", "Diverso da"})
      dttUguale.AcceptChanges()
      cbUguale.DataSource = dttUguale
      cbUguale.ValueMember = "cod"
      cbUguale.DisplayMember = "val"

      dttTipodoc.Columns.Add("cod", GetType(String))
      dttTipodoc.Columns.Add("val", GetType(String))
      dttTipodoc.Rows.Add(New Object() {"D", "Fattura differita emessa"})
      dttTipodoc.Rows.Add(New Object() {"P", "Fattura/ricevuta fiscale differita"})
      dttTipodoc.Rows.Add(New Object() {"£", "Nota accredito differita emessa"})
      dttTipodoc.AcceptChanges()
      cbTipodoc.DataSource = dttTipodoc
      cbTipodoc.ValueMember = "cod"
      cbTipodoc.DisplayMember = "val"

      dttcontab.Columns.Add("cod", GetType(String))
      dttcontab.Columns.Add("val", GetType(String))
      dttContab.Rows.Add(New Object() {"T", "Tutti"})
      dttContab.Rows.Add(New Object() {"N", "Non contabilizzati"})
      dttContab.Rows.Add(New Object() {"S", "Solo contabilizzati"})
      dttcontab.AcceptChanges()
      cbContab.DataSource = dttcontab
      cbContab.ValueMember = "cod"
      cbContab.DisplayMember = "val"

      edNoteFattura.NTSSetParam(oMenu, oApp.Tr(Me, 129055311901349695, "Note fattura"), 80)
      edClientea.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055311926663005, "Cliente DA"), tabanagrac)
      edClienteda.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055311956038945, "Cliente A"), tabanagrac)
      cbCorrispond.NTSSetParam(oApp.Tr(Me, 129055311976664605, "Modalità di corrispondenza"))
      cbUguale.NTSSetParam(oApp.Tr(Me, 129055311998852815, "Uguale/diverso"))
      cbContab.NTSSetParam(oApp.Tr(Me, 130086765959983613, "Contabilizzati"))
      ckModalCorr.NTSSetParam(oMenu, oApp.Tr(Me, 129055312021978555, "Filtra modalità corrispondenza"), "S", "N")
      edLista.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055312062761110, "Lista selezionata"), tablsel)
      edNotLista.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129636790489277343, "Lista selezionata"), tablsel)
      edZonaa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055312092605815, "Zona A"), tabzone)
      edZonada.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055312111825180, "Zona DA"), tabzone)
      edCategoriaa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055312131982075, "Categoria A"), tabcate)
      edCategoriada.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055312154795305, "Categoria DA"), tabcate)
      opCliNotLista.NTSSetParam(oMenu, oApp.Tr(Me, 129636790757509765, "Tutti tranne quelli della lista selezionata"), "E")
      opCliLista.NTSSetParam(oMenu, oApp.Tr(Me, 129055312175889730, "Da lista selezionata"), "L")
      opCliDaa.NTSSetParam(oMenu, oApp.Tr(Me, 129055312207453240, "Dal / Al cliente"), "C")
      opCliTutti.NTSSetParam(oMenu, oApp.Tr(Me, 129055312231829020, "Tutti"), "T")
      edAgentedoca.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055312256673565, "Agente A"), tabcage)
      edAgentedocda.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055312276986715, "Agente DA"), tabcage)
      ckNumdoc.NTSSetParam(oMenu, oApp.Tr(Me, 129055312297456120, "Filtra numero / serie docum."), "S", "N")
      ckDatdoc.NTSSetParam(oMenu, oApp.Tr(Me, 129055312317613015, "Filtra data documento"), "S", "N")
      edSeriedocda.NTSSetParam(oMenu, oApp.Tr(Me, 129055312336676125, "Serie documento DA"), CLN__STD.SerieMaxLen, False)
      edSeriedoca.NTSSetParam(oMenu, oApp.Tr(Me, 129055312359645610, "Serie documento A"), CLN__STD.SerieMaxLen, False)
      edNumdoca.NTSSetParam(oMenu, oApp.Tr(Me, 129055312384958920, "Numero documento A"), "0", 9, 0, 999999999)
      edNumdocda.NTSSetParam(oMenu, oApp.Tr(Me, 129055312407303385, "Numero documento DA"), "0", 9, 0, 999999999)
      edDatadoca.NTSSetParam(oMenu, oApp.Tr(Me, 129055312429022830, "Data documento A"), False)
      edDatadocda.NTSSetParam(oMenu, oApp.Tr(Me, 129055312449961000, "Data documento DA"), False)
      edAnnodoc.NTSSetParam(oMenu, oApp.Tr(Me, 129055312471367935, "Anno documento"), "0", 4, 1900, 2099)
      cbTipodoc.NTSSetParam(oApp.Tr(Me, 129055312491993595, "Tipo documento"))
      ckProva.NTSSetParam(oMenu, oApp.Tr(Me, 129095931746660156, "Elaborazioni di prova"), "S", "N")

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
  Public Overridable Sub FRMVESERF_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim strTmp As String = ""
    Try
      '--------------------------------------------
      'predispongo i controlli
      InitControls()

      cbTipodoc.SelectedValue = "D"
      edAnnodoc.Text = DateTime.Now.Year.ToString
      edDatadocda.Text = IntSetDate("01/" & DateTime.Now.Month.ToString("00") & "/" & DateTime.Now.Year.ToString)
      edDatadoca.Text = DateTime.Now.ToShortDateString

      ckNumdoc_CheckedChanged(ckNumdoc, Nothing)
      ckDatdoc_CheckedChanged(ckDatdoc, Nothing)
      opCliTutti.Checked = True
      opCliTutti_CheckedChanged(opCliTutti, Nothing)
      ckModalCorr_CheckedChanged(ckModalCorr, Nothing)

      '--------------------------------------------
      'recent
      strTmp = oMenu.GetSettingBus("BSVEDERF", "RECENT", ".", "Segno", "0", " ", "0")
      If strTmp = "0" Or strTmp = "1" Then cbUguale.SelectedValue = strTmp

      strTmp = oMenu.GetSettingBus("BSVEDERF", "RECENT", ".", "Usaem", "S", " ", "S")
      cbCorrispond.SelectedValue = strTmp
      If cbCorrispond.SelectedIndex = -1 Then cbCorrispond.SelectedValue = "S"

      '--------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
      GctlApplicaDefaultValue()

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMVESERF_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Try
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
      If NTSCInt(edAgentedocda.Text) > NTSCInt(edAgentedoca.Text) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130692620262873202, "Attenzione!" & vbCrLf & _
          "L'Agente 1 iniziale non può essere superiore a quello finale."))
        edAgentedocda.Text = "0"
        edAgentedoca.Text = "".PadLeft(4, "9"c)
        edAgentedocda.Focus()
        Return
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
      If NTSCInt(edCategoriada.Text) > NTSCInt(edCategoriaa.Text) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130692622327647407, "Attenzione!" & vbCrLf & _
          "La categoria iniziale non può essere superiore a quella finale."))
        edCategoriada.Text = "0"
        edCategoriaa.Text = "".PadLeft(3, "9"c)
        edCategoriada.Focus()
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edZonada.Text) > NTSCInt(edZonaa.Text) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130692622525491363, "Attenzione!" & vbCrLf & _
          "La zona iniziale non può essere superiore a quella finale."))
        edZonada.Text = "0"
        edZonaa.Text = "".PadLeft(3, "9"c)
        edZonada.Focus()
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      'filtri su testmag
      strOutTestmag += "tm_tipork = " & CStrSQL(cbTipodoc.SelectedValue) & "§"
      strOutTestmag += "tm_anno = " & CDblSQL(edAnnodoc.Text) & "§"
      If NTSCInt(edAgentedocda.Text) <> 0 Or NTSCInt(edAgentedoca.Text) <> 9999 Then
        strOutTestmag += "tm_codagen >= " & CDblSQL(edAgentedocda.Text) & "§"
        strOutTestmag += "tm_codagen <= " & CDblSQL(edAgentedoca.Text) & "§"
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

      If ckDatdoc.Checked Then
        strOutTestmag += "tm_datdoc >= " & CDataSQL(edDatadocda.Text) & "§"
        strOutTestmag += "tm_datdoc <= " & CDataSQL(edDatadoca.Text) & "§"
      End If

      '---------------------------
      'filtri su anagra
      If NTSCInt(edCategoriada.Text) <> 0 Or NTSCInt(edCategoriaa.Text) <> 999 Then
        strOutAnagra += "an_categ >= " & CDblSQL(edCategoriada.Text) & "§"
        strOutAnagra += "an_categ <= " & CDblSQL(edCategoriaa.Text) & "§"
      End If
      If NTSCInt(edZonada.Text) <> 0 Or NTSCInt(edZonaa.Text) <> 999 Then
        strOutAnagra += "an_zona >= " & CDblSQL(edZonada.Text) & "§"
        strOutAnagra += "an_zona <= " & CDblSQL(edZonaa.Text) & "§"
      End If
      If opCliDaa.Checked Then
        strOutAnagra += "an_conto >= " & CDblSQL(edClienteda.Text) & "§"
        strOutAnagra += "an_conto <= " & CDblSQL(edClientea.Text) & "§"
      End If

      If ckModalCorr.Checked Then
        If cbUguale.SelectedValue.ToString = "0" Then
          strOutAnagra += "an_usaem = " & CStrSQL(cbCorrispond.SelectedValue) & "§"
        Else
          strOutAnagra += "an_usaem <> " & CStrSQL(cbCorrispond.SelectedValue) & "§"
        End If
      End If

      '---------------------------
      'lista selezionata
      If opCliLista.Checked Then
        If NTSCInt(edLista.Text) = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129055313215610500, "Indicare il numero della lista selezionata clienti"))
          Return
        End If
        strOutLista = edLista.Text
      End If

      If opCliNotLista.Checked Then
        If NTSCInt(edNotLista.Text) = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129636791089990234, "Indicare il numero della lista selezionata clienti daaescludere"))
          Return
        End If
        strOutLista = (NTSCInt(edNotLista.Text) * -1).ToString
      End If

      '---------------------------
      oMenu.SaveSettingBus("BSVEDERF", "RECENT", ".", "Segno", cbUguale.SelectedValue, " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("BSVEDERF", "RECENT", ".", "Usaem", cbCorrispond.SelectedValue, " ", "NS.", "...", "...")

      If strOutTestmag.Trim <> "" Then strOutTestmag = strOutTestmag.Substring(0, strOutTestmag.Length - 1)
      If strOutAnagra.Trim <> "" Then strOutAnagra = strOutAnagra.Substring(0, strOutAnagra.Length - 1)

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

  Public Overridable Sub ckDatdoc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckDatdoc.CheckedChanged
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
  Public Overridable Sub ckNumdoc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckNumdoc.CheckedChanged
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
  Public Overridable Sub opCliTutti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opCliTutti.CheckedChanged
    Try
      If opCliTutti.Checked Then
        edClienteda.Enabled = False
        edClientea.Enabled = False
        edLista.Enabled = False
        edNotLista.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opCliDaa_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opCliDaa.CheckedChanged
    Try
      If opCliDaa.Checked Then
        GctlSetVisEnab(edClienteda, False)
        GctlSetVisEnab(edClientea, False)
        edLista.Enabled = False
        edNotLista.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opCliLista_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opCliLista.CheckedChanged
    Try
      If opCliLista.Checked Then
        edClienteda.Enabled = False
        edClientea.Enabled = False
        GctlSetVisEnab(edLista, False)
        edNotLista.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opCliNotLista_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opCliNotLista.CheckedChanged
    Try
      If opCliNotLista.Checked Then
        edClienteda.Enabled = False
        edClientea.Enabled = False
        edLista.Enabled = False
        GctlSetVisEnab(edNotLista, False)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub ckModalCorr_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckModalCorr.CheckedChanged
    Try
      If ckModalCorr.Checked Then
        GctlSetVisEnab(cbUguale, False)
        GctlSetVisEnab(cbCorrispond, False)
      Else
        cbUguale.Enabled = False
        cbCorrispond.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edLista_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edLista.ValidatedAndChanged
    Dim strTmp As String = ""
    Try
      If oCleFadi Is Nothing Then Return
      If Not oCleFadi.SerfedLista_ValidatedAndChanged(NTSCInt(edLista.Text), strTmp) Then
        edLista.Text = NTSCStr(edLista.OldEditValue)
      Else
        lbLista.Text = strTmp
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edNotLista_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edNotLista.ValidatedAndChanged
    Dim strTmp As String = ""
    Try
      If oCleFadi Is Nothing Then Return
      If Not oCleFadi.SerfedLista_ValidatedAndChanged(NTSCInt(edNotLista.Text), strTmp) Then
        edNotLista.Text = NTSCStr(edNotLista.OldEditValue)
      Else
        lbNotLista.Text = strTmp
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edAgentedocda_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAgentedocda.ValidatedAndChanged
    Try
      edAgentedoca.Text = edAgentedocda.Text
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
  Public Overridable Sub edCategoriada_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCategoriada.ValidatedAndChanged
    Try
      edCategoriaa.Text = edCategoriada.Text
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edZonada_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edZonada.ValidatedAndChanged
    Try
      edZonaa.Text = edZonada.Text
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
      If edClienteda.ContainsFocus Or edClientea.ContainsFocus Then strTabName = "ANAGRA_CLI"
      '--------------------------------------------------------------------------------------------------------------
      Return MyBase.NTSGetDataAutocompletamento(strTabName, strDescr, IsCrmUser, dsOut)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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
