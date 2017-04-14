Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGTERM
  Public oCallParams As CLE__CLDP
  Public oCleLsar As CLEMGLSAR

  Public bInLoad As Boolean = False
  Public bOnLoading As Boolean = False

#Region "Variabili"
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents ckExecute As NTSInformatica.NTSCheckBox
  Public WithEvents lbCommand As NTSInformatica.NTSLabel
  Public WithEvents cmdCommand As NTSInformatica.NTSButton
  Public WithEvents pnPulsanti As NTSInformatica.NTSPanel
  Public WithEvents frmTipofile As NTSInformatica.NTSGroupBox
  Public WithEvents lbDecSep As NTSInformatica.NTSLabel
  Public WithEvents edSepField As NTSInformatica.NTSTextBoxStr
  Public WithEvents opDelim As NTSInformatica.NTSRadioButton
  Public WithEvents opFixed As NTSInformatica.NTSRadioButton
  Public WithEvents lbNomeFile As NTSInformatica.NTSLabel
  Public WithEvents edNomeFile As NTSInformatica.NTSTextBoxStr
  Public WithEvents cmdNomeFile As NTSInformatica.NTSButton
  Public WithEvents edDecSep As NTSInformatica.NTSTextBoxStr
  Public WithEvents frmCampi As NTSInformatica.NTSGroupBox
  Public WithEvents ckShowLog As NTSInformatica.NTSCheckBox
  Public WithEvents ckIgnore1rk As NTSInformatica.NTSCheckBox
  Public WithEvents ckDelFile As NTSInformatica.NTSCheckBox
  Public WithEvents ckSilent As NTSInformatica.NTSCheckBox
  Public WithEvents opBarcode As NTSInformatica.NTSRadioButton
  Public WithEvents opCodart As NTSInformatica.NTSRadioButton
  Public WithEvents lbPosLotto As NTSInformatica.NTSLabel
  Public WithEvents lbPosFase As NTSInformatica.NTSLabel
  Public WithEvents lbPosArt As NTSInformatica.NTSLabel
  Public WithEvents edPosArt As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPosUbic As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPosLotto As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPosFase As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckUbic As NTSInformatica.NTSCheckBox
  Public WithEvents ckLotto As NTSInformatica.NTSCheckBox
  Public WithEvents ckFase As NTSInformatica.NTSCheckBox
  Public WithEvents lbCinque As NTSInformatica.NTSLabel
  Public WithEvents lbQuattro As NTSInformatica.NTSLabel
  Public WithEvents lbDue As NTSInformatica.NTSLabel
  Public WithEvents lbUno As NTSInformatica.NTSLabel
  Public WithEvents lbUbicA As NTSInformatica.NTSLabel
  Public WithEvents lbUbicDa As NTSInformatica.NTSLabel
  Public WithEvents edUbicA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edUbicDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbLottoA As NTSInformatica.NTSLabel
  Public WithEvents lbLottoDa As NTSInformatica.NTSLabel
  Public WithEvents edLottoA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edLottoDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbFaseA As NTSInformatica.NTSLabel
  Public WithEvents lbFaseDa As NTSInformatica.NTSLabel
  Public WithEvents edFaseA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edFaseDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbArtA As NTSInformatica.NTSLabel
  Public WithEvents lbArtDa As NTSInformatica.NTSLabel
  Public WithEvents edArtA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edArtDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbPosUbic As NTSInformatica.NTSLabel
  Public WithEvents lbEsisA As NTSInformatica.NTSLabel
  Public WithEvents lbEsisDa As NTSInformatica.NTSLabel
  Public WithEvents edEsisA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edEsisDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbPosEsis As NTSInformatica.NTSLabel
  Public WithEvents edPosEsis As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckEsis As NTSInformatica.NTSCheckBox
  Public WithEvents lbSette As NTSInformatica.NTSLabel
  Public WithEvents lbMatrA As NTSInformatica.NTSLabel
  Public WithEvents lbMatrDa As NTSInformatica.NTSLabel
  Public WithEvents edMatrA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edMatrDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbPosMatr As NTSInformatica.NTSLabel
  Public WithEvents edPosMatr As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckMatr As NTSInformatica.NTSCheckBox
  Public WithEvents lbSei As NTSInformatica.NTSLabel
  Public WithEvents lbComA As NTSInformatica.NTSLabel
  Public WithEvents lbComDa As NTSInformatica.NTSLabel
  Public WithEvents edComA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edComDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbPosCom As NTSInformatica.NTSLabel
  Public WithEvents edPosCom As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckCom As NTSInformatica.NTSCheckBox
  Public WithEvents lbTre As NTSInformatica.NTSLabel
  Public WithEvents opCodartBarcodeCodarfo As NTSInformatica.NTSRadioButton
  Public WithEvents pnControlli As NTSInformatica.NTSPanel
  Public WithEvents lbDivEsis As NTSInformatica.NTSLabel
  Public WithEvents edDivEsis As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbImpostaFile As NTSInformatica.NTSLabel
  Public WithEvents cbImpostaFile As NTSInformatica.NTSComboBox
  Public WithEvents cmdAggiungi As NTSInformatica.NTSButton
  Public WithEvents cmdCancella As NTSInformatica.NTSButton
  Public WithEvents cmdAggiorna As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
#End Region

  Public Overridable Sub InitializeComponent()
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.ckExecute = New NTSInformatica.NTSCheckBox
    Me.lbCommand = New NTSInformatica.NTSLabel
    Me.cmdCommand = New NTSInformatica.NTSButton
    Me.pnPulsanti = New NTSInformatica.NTSPanel
    Me.pnControlli = New NTSInformatica.NTSPanel
    Me.cmdCancella = New NTSInformatica.NTSButton
    Me.cmdAggiungi = New NTSInformatica.NTSButton
    Me.cbImpostaFile = New NTSInformatica.NTSComboBox
    Me.lbImpostaFile = New NTSInformatica.NTSLabel
    Me.frmTipofile = New NTSInformatica.NTSGroupBox
    Me.edDecSep = New NTSInformatica.NTSTextBoxStr
    Me.lbDecSep = New NTSInformatica.NTSLabel
    Me.edSepField = New NTSInformatica.NTSTextBoxStr
    Me.opDelim = New NTSInformatica.NTSRadioButton
    Me.opFixed = New NTSInformatica.NTSRadioButton
    Me.frmCampi = New NTSInformatica.NTSGroupBox
    Me.lbDivEsis = New NTSInformatica.NTSLabel
    Me.edDivEsis = New NTSInformatica.NTSTextBoxNum
    Me.opCodartBarcodeCodarfo = New NTSInformatica.NTSRadioButton
    Me.lbComA = New NTSInformatica.NTSLabel
    Me.lbComDa = New NTSInformatica.NTSLabel
    Me.edComA = New NTSInformatica.NTSTextBoxNum
    Me.edComDa = New NTSInformatica.NTSTextBoxNum
    Me.lbPosCom = New NTSInformatica.NTSLabel
    Me.edPosCom = New NTSInformatica.NTSTextBoxNum
    Me.ckCom = New NTSInformatica.NTSCheckBox
    Me.lbTre = New NTSInformatica.NTSLabel
    Me.lbEsisA = New NTSInformatica.NTSLabel
    Me.lbEsisDa = New NTSInformatica.NTSLabel
    Me.edEsisA = New NTSInformatica.NTSTextBoxNum
    Me.edEsisDa = New NTSInformatica.NTSTextBoxNum
    Me.lbPosEsis = New NTSInformatica.NTSLabel
    Me.edPosEsis = New NTSInformatica.NTSTextBoxNum
    Me.ckEsis = New NTSInformatica.NTSCheckBox
    Me.lbSette = New NTSInformatica.NTSLabel
    Me.lbMatrA = New NTSInformatica.NTSLabel
    Me.lbMatrDa = New NTSInformatica.NTSLabel
    Me.edMatrA = New NTSInformatica.NTSTextBoxNum
    Me.edMatrDa = New NTSInformatica.NTSTextBoxNum
    Me.lbPosMatr = New NTSInformatica.NTSLabel
    Me.edPosMatr = New NTSInformatica.NTSTextBoxNum
    Me.ckMatr = New NTSInformatica.NTSCheckBox
    Me.lbSei = New NTSInformatica.NTSLabel
    Me.lbUbicA = New NTSInformatica.NTSLabel
    Me.lbUbicDa = New NTSInformatica.NTSLabel
    Me.edUbicA = New NTSInformatica.NTSTextBoxNum
    Me.edUbicDa = New NTSInformatica.NTSTextBoxNum
    Me.lbLottoA = New NTSInformatica.NTSLabel
    Me.lbLottoDa = New NTSInformatica.NTSLabel
    Me.edLottoA = New NTSInformatica.NTSTextBoxNum
    Me.edLottoDa = New NTSInformatica.NTSTextBoxNum
    Me.lbFaseA = New NTSInformatica.NTSLabel
    Me.lbFaseDa = New NTSInformatica.NTSLabel
    Me.edFaseA = New NTSInformatica.NTSTextBoxNum
    Me.edFaseDa = New NTSInformatica.NTSTextBoxNum
    Me.lbArtA = New NTSInformatica.NTSLabel
    Me.lbArtDa = New NTSInformatica.NTSLabel
    Me.edArtA = New NTSInformatica.NTSTextBoxNum
    Me.edArtDa = New NTSInformatica.NTSTextBoxNum
    Me.lbPosUbic = New NTSInformatica.NTSLabel
    Me.lbPosLotto = New NTSInformatica.NTSLabel
    Me.lbPosFase = New NTSInformatica.NTSLabel
    Me.lbPosArt = New NTSInformatica.NTSLabel
    Me.edPosArt = New NTSInformatica.NTSTextBoxNum
    Me.edPosUbic = New NTSInformatica.NTSTextBoxNum
    Me.edPosLotto = New NTSInformatica.NTSTextBoxNum
    Me.edPosFase = New NTSInformatica.NTSTextBoxNum
    Me.ckUbic = New NTSInformatica.NTSCheckBox
    Me.ckLotto = New NTSInformatica.NTSCheckBox
    Me.ckFase = New NTSInformatica.NTSCheckBox
    Me.lbCinque = New NTSInformatica.NTSLabel
    Me.lbQuattro = New NTSInformatica.NTSLabel
    Me.lbDue = New NTSInformatica.NTSLabel
    Me.lbUno = New NTSInformatica.NTSLabel
    Me.opBarcode = New NTSInformatica.NTSRadioButton
    Me.opCodart = New NTSInformatica.NTSRadioButton
    Me.lbNomeFile = New NTSInformatica.NTSLabel
    Me.ckSilent = New NTSInformatica.NTSCheckBox
    Me.ckShowLog = New NTSInformatica.NTSCheckBox
    Me.edNomeFile = New NTSInformatica.NTSTextBoxStr
    Me.ckDelFile = New NTSInformatica.NTSCheckBox
    Me.ckIgnore1rk = New NTSInformatica.NTSCheckBox
    Me.cmdNomeFile = New NTSInformatica.NTSButton
    Me.cmdAggiorna = New NTSInformatica.NTSButton
    CType(Me.ckExecute.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnPulsanti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPulsanti.SuspendLayout()
    CType(Me.pnControlli, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnControlli.SuspendLayout()
    CType(Me.cbImpostaFile.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frmTipofile, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frmTipofile.SuspendLayout()
    CType(Me.edDecSep.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSepField.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opDelim.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opFixed.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frmCampi, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frmCampi.SuspendLayout()
    CType(Me.edDivEsis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opCodartBarcodeCodarfo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edComA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edComDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPosCom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckCom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEsisA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEsisDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPosEsis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckEsis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edMatrA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edMatrDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPosMatr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckMatr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edUbicA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edUbicDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edLottoA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edLottoDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFaseA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFaseDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edArtA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edArtDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPosArt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPosUbic.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPosLotto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPosFase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckUbic.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckLotto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckFase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opBarcode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opCodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSilent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckShowLog.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNomeFile.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDelFile.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckIgnore1rk.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.cmdAnnulla.Location = New System.Drawing.Point(3, 32)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(130, 23)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(3, 3)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.NTSContextMenu = Nothing
    Me.cmdOk.Size = New System.Drawing.Size(130, 23)
    Me.cmdOk.TabIndex = 2
    Me.cmdOk.Text = "&Conferma"
    '
    'ckExecute
    '
    Me.ckExecute.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckExecute.Location = New System.Drawing.Point(10, 36)
    Me.ckExecute.Name = "ckExecute"
    Me.ckExecute.NTSCheckValue = "S"
    Me.ckExecute.NTSUnCheckValue = "N"
    Me.ckExecute.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckExecute.Properties.Appearance.Options.UseBackColor = True
    Me.ckExecute.Properties.AutoHeight = False
    Me.ckExecute.Properties.Caption = "Esegui il comando"
    Me.ckExecute.Size = New System.Drawing.Size(112, 19)
    Me.ckExecute.TabIndex = 3
    '
    'lbCommand
    '
    Me.lbCommand.BackColor = System.Drawing.Color.Transparent
    Me.lbCommand.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbCommand.Location = New System.Drawing.Point(128, 35)
    Me.lbCommand.Name = "lbCommand"
    Me.lbCommand.NTSDbField = ""
    Me.lbCommand.Size = New System.Drawing.Size(378, 20)
    Me.lbCommand.TabIndex = 4
    Me.lbCommand.Tooltip = ""
    Me.lbCommand.UseMnemonic = False
    '
    'cmdCommand
    '
    Me.cmdCommand.ImageText = ""
    Me.cmdCommand.Location = New System.Drawing.Point(512, 35)
    Me.cmdCommand.Name = "cmdCommand"
    Me.cmdCommand.NTSContextMenu = Nothing
    Me.cmdCommand.Size = New System.Drawing.Size(25, 20)
    Me.cmdCommand.TabIndex = 5
    Me.cmdCommand.Text = "..."
    '
    'pnPulsanti
    '
    Me.pnPulsanti.AllowDrop = True
    Me.pnPulsanti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPulsanti.Appearance.Options.UseBackColor = True
    Me.pnPulsanti.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPulsanti.Controls.Add(Me.cmdOk)
    Me.pnPulsanti.Controls.Add(Me.cmdAnnulla)
    Me.pnPulsanti.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPulsanti.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnPulsanti.Location = New System.Drawing.Point(547, 0)
    Me.pnPulsanti.Name = "pnPulsanti"
    Me.pnPulsanti.NTSActiveTrasparency = True
    Me.pnPulsanti.Size = New System.Drawing.Size(137, 420)
    Me.pnPulsanti.TabIndex = 6
    Me.pnPulsanti.Text = "NtsPanel1"
    '
    'pnControlli
    '
    Me.pnControlli.AllowDrop = True
    Me.pnControlli.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnControlli.Appearance.Options.UseBackColor = True
    Me.pnControlli.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnControlli.Controls.Add(Me.cmdAggiorna)
    Me.pnControlli.Controls.Add(Me.cmdCancella)
    Me.pnControlli.Controls.Add(Me.cmdAggiungi)
    Me.pnControlli.Controls.Add(Me.cbImpostaFile)
    Me.pnControlli.Controls.Add(Me.lbImpostaFile)
    Me.pnControlli.Controls.Add(Me.ckExecute)
    Me.pnControlli.Controls.Add(Me.frmTipofile)
    Me.pnControlli.Controls.Add(Me.frmCampi)
    Me.pnControlli.Controls.Add(Me.lbNomeFile)
    Me.pnControlli.Controls.Add(Me.lbCommand)
    Me.pnControlli.Controls.Add(Me.ckSilent)
    Me.pnControlli.Controls.Add(Me.ckShowLog)
    Me.pnControlli.Controls.Add(Me.edNomeFile)
    Me.pnControlli.Controls.Add(Me.cmdCommand)
    Me.pnControlli.Controls.Add(Me.ckDelFile)
    Me.pnControlli.Controls.Add(Me.ckIgnore1rk)
    Me.pnControlli.Controls.Add(Me.cmdNomeFile)
    Me.pnControlli.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnControlli.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnControlli.Location = New System.Drawing.Point(0, 0)
    Me.pnControlli.Name = "pnControlli"
    Me.pnControlli.NTSActiveTrasparency = True
    Me.pnControlli.Size = New System.Drawing.Size(547, 420)
    Me.pnControlli.TabIndex = 15
    Me.pnControlli.Text = "NtsPanel1"
    '
    'cmdCancella
    '
    Me.cmdCancella.ImageText = ""
    Me.cmdCancella.Location = New System.Drawing.Point(463, 12)
    Me.cmdCancella.Name = "cmdCancella"
    Me.cmdCancella.NTSContextMenu = Nothing
    Me.cmdCancella.Size = New System.Drawing.Size(70, 23)
    Me.cmdCancella.TabIndex = 515
    Me.cmdCancella.Text = "Elimina"
    '
    'cmdAggiungi
    '
    Me.cmdAggiungi.ImageText = ""
    Me.cmdAggiungi.Location = New System.Drawing.Point(323, 12)
    Me.cmdAggiungi.Name = "cmdAggiungi"
    Me.cmdAggiungi.NTSContextMenu = Nothing
    Me.cmdAggiungi.Size = New System.Drawing.Size(70, 23)
    Me.cmdAggiungi.TabIndex = 514
    Me.cmdAggiungi.Text = "Aggiungi"
    '
    'cbImpostaFile
    '
    Me.cbImpostaFile.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbImpostaFile.DataSource = Nothing
    Me.cbImpostaFile.DisplayMember = ""
    Me.cbImpostaFile.Location = New System.Drawing.Point(128, 13)
    Me.cbImpostaFile.Name = "cbImpostaFile"
    Me.cbImpostaFile.NTSDbField = ""
    Me.cbImpostaFile.Properties.AutoHeight = False
    Me.cbImpostaFile.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbImpostaFile.Properties.DropDownRows = 30
    Me.cbImpostaFile.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbImpostaFile.SelectedValue = ""
    Me.cbImpostaFile.Size = New System.Drawing.Size(192, 20)
    Me.cbImpostaFile.TabIndex = 513
    Me.cbImpostaFile.ValueMember = ""
    '
    'lbImpostaFile
    '
    Me.lbImpostaFile.AutoSize = True
    Me.lbImpostaFile.BackColor = System.Drawing.Color.Transparent
    Me.lbImpostaFile.Location = New System.Drawing.Point(12, 16)
    Me.lbImpostaFile.Name = "lbImpostaFile"
    Me.lbImpostaFile.NTSDbField = ""
    Me.lbImpostaFile.Size = New System.Drawing.Size(90, 13)
    Me.lbImpostaFile.TabIndex = 18
    Me.lbImpostaFile.Text = "Impostazione File"
    Me.lbImpostaFile.Tooltip = ""
    Me.lbImpostaFile.UseMnemonic = False
    '
    'frmTipofile
    '
    Me.frmTipofile.AllowDrop = True
    Me.frmTipofile.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frmTipofile.Appearance.Options.UseBackColor = True
    Me.frmTipofile.Controls.Add(Me.edDecSep)
    Me.frmTipofile.Controls.Add(Me.lbDecSep)
    Me.frmTipofile.Controls.Add(Me.edSepField)
    Me.frmTipofile.Controls.Add(Me.opDelim)
    Me.frmTipofile.Controls.Add(Me.opFixed)
    Me.frmTipofile.Cursor = System.Windows.Forms.Cursors.Default
    Me.frmTipofile.Location = New System.Drawing.Point(10, 82)
    Me.frmTipofile.Name = "frmTipofile"
    Me.frmTipofile.Size = New System.Drawing.Size(190, 93)
    Me.frmTipofile.TabIndex = 9
    Me.frmTipofile.Text = "Tipo file"
    '
    'edDecSep
    '
    Me.edDecSep.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDecSep.EditValue = ","
    Me.edDecSep.Location = New System.Drawing.Point(149, 61)
    Me.edDecSep.Name = "edDecSep"
    Me.edDecSep.NTSDbField = ""
    Me.edDecSep.NTSForzaVisZoom = False
    Me.edDecSep.NTSOldValue = ","
    Me.edDecSep.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDecSep.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDecSep.Properties.AutoHeight = False
    Me.edDecSep.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDecSep.Properties.MaxLength = 65536
    Me.edDecSep.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDecSep.Size = New System.Drawing.Size(24, 20)
    Me.edDecSep.TabIndex = 10
    '
    'lbDecSep
    '
    Me.lbDecSep.AutoSize = True
    Me.lbDecSep.BackColor = System.Drawing.Color.Transparent
    Me.lbDecSep.Location = New System.Drawing.Point(5, 64)
    Me.lbDecSep.Name = "lbDecSep"
    Me.lbDecSep.NTSDbField = ""
    Me.lbDecSep.Size = New System.Drawing.Size(105, 13)
    Me.lbDecSep.TabIndex = 9
    Me.lbDecSep.Text = "Separatore decimale"
    Me.lbDecSep.Tooltip = ""
    Me.lbDecSep.UseMnemonic = False
    '
    'edSepField
    '
    Me.edSepField.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSepField.EditValue = ";"
    Me.edSepField.Enabled = False
    Me.edSepField.Location = New System.Drawing.Point(149, 39)
    Me.edSepField.Name = "edSepField"
    Me.edSepField.NTSDbField = ""
    Me.edSepField.NTSForzaVisZoom = False
    Me.edSepField.NTSOldValue = ";"
    Me.edSepField.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSepField.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSepField.Properties.AutoHeight = False
    Me.edSepField.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSepField.Properties.MaxLength = 65536
    Me.edSepField.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSepField.Size = New System.Drawing.Size(24, 20)
    Me.edSepField.TabIndex = 2
    '
    'opDelim
    '
    Me.opDelim.Cursor = System.Windows.Forms.Cursors.Default
    Me.opDelim.Location = New System.Drawing.Point(5, 40)
    Me.opDelim.Name = "opDelim"
    Me.opDelim.NTSCheckValue = "S"
    Me.opDelim.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opDelim.Properties.Appearance.Options.UseBackColor = True
    Me.opDelim.Properties.AutoHeight = False
    Me.opDelim.Properties.Caption = "Separatore di campo"
    Me.opDelim.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opDelim.Size = New System.Drawing.Size(129, 19)
    Me.opDelim.TabIndex = 1
    '
    'opFixed
    '
    Me.opFixed.Cursor = System.Windows.Forms.Cursors.Default
    Me.opFixed.EditValue = True
    Me.opFixed.Location = New System.Drawing.Point(5, 20)
    Me.opFixed.Name = "opFixed"
    Me.opFixed.NTSCheckValue = "S"
    Me.opFixed.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opFixed.Properties.Appearance.Options.UseBackColor = True
    Me.opFixed.Properties.AutoHeight = False
    Me.opFixed.Properties.Caption = "Posizioni fisse"
    Me.opFixed.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opFixed.Size = New System.Drawing.Size(99, 19)
    Me.opFixed.TabIndex = 0
    '
    'frmCampi
    '
    Me.frmCampi.AllowDrop = True
    Me.frmCampi.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frmCampi.Appearance.Options.UseBackColor = True
    Me.frmCampi.Controls.Add(Me.lbDivEsis)
    Me.frmCampi.Controls.Add(Me.edDivEsis)
    Me.frmCampi.Controls.Add(Me.opCodartBarcodeCodarfo)
    Me.frmCampi.Controls.Add(Me.lbComA)
    Me.frmCampi.Controls.Add(Me.lbComDa)
    Me.frmCampi.Controls.Add(Me.edComA)
    Me.frmCampi.Controls.Add(Me.edComDa)
    Me.frmCampi.Controls.Add(Me.lbPosCom)
    Me.frmCampi.Controls.Add(Me.edPosCom)
    Me.frmCampi.Controls.Add(Me.ckCom)
    Me.frmCampi.Controls.Add(Me.lbTre)
    Me.frmCampi.Controls.Add(Me.lbEsisA)
    Me.frmCampi.Controls.Add(Me.lbEsisDa)
    Me.frmCampi.Controls.Add(Me.edEsisA)
    Me.frmCampi.Controls.Add(Me.edEsisDa)
    Me.frmCampi.Controls.Add(Me.lbPosEsis)
    Me.frmCampi.Controls.Add(Me.edPosEsis)
    Me.frmCampi.Controls.Add(Me.ckEsis)
    Me.frmCampi.Controls.Add(Me.lbSette)
    Me.frmCampi.Controls.Add(Me.lbMatrA)
    Me.frmCampi.Controls.Add(Me.lbMatrDa)
    Me.frmCampi.Controls.Add(Me.edMatrA)
    Me.frmCampi.Controls.Add(Me.edMatrDa)
    Me.frmCampi.Controls.Add(Me.lbPosMatr)
    Me.frmCampi.Controls.Add(Me.edPosMatr)
    Me.frmCampi.Controls.Add(Me.ckMatr)
    Me.frmCampi.Controls.Add(Me.lbSei)
    Me.frmCampi.Controls.Add(Me.lbUbicA)
    Me.frmCampi.Controls.Add(Me.lbUbicDa)
    Me.frmCampi.Controls.Add(Me.edUbicA)
    Me.frmCampi.Controls.Add(Me.edUbicDa)
    Me.frmCampi.Controls.Add(Me.lbLottoA)
    Me.frmCampi.Controls.Add(Me.lbLottoDa)
    Me.frmCampi.Controls.Add(Me.edLottoA)
    Me.frmCampi.Controls.Add(Me.edLottoDa)
    Me.frmCampi.Controls.Add(Me.lbFaseA)
    Me.frmCampi.Controls.Add(Me.lbFaseDa)
    Me.frmCampi.Controls.Add(Me.edFaseA)
    Me.frmCampi.Controls.Add(Me.edFaseDa)
    Me.frmCampi.Controls.Add(Me.lbArtA)
    Me.frmCampi.Controls.Add(Me.lbArtDa)
    Me.frmCampi.Controls.Add(Me.edArtA)
    Me.frmCampi.Controls.Add(Me.edArtDa)
    Me.frmCampi.Controls.Add(Me.lbPosUbic)
    Me.frmCampi.Controls.Add(Me.lbPosLotto)
    Me.frmCampi.Controls.Add(Me.lbPosFase)
    Me.frmCampi.Controls.Add(Me.lbPosArt)
    Me.frmCampi.Controls.Add(Me.edPosArt)
    Me.frmCampi.Controls.Add(Me.edPosUbic)
    Me.frmCampi.Controls.Add(Me.edPosLotto)
    Me.frmCampi.Controls.Add(Me.edPosFase)
    Me.frmCampi.Controls.Add(Me.ckUbic)
    Me.frmCampi.Controls.Add(Me.ckLotto)
    Me.frmCampi.Controls.Add(Me.ckFase)
    Me.frmCampi.Controls.Add(Me.lbCinque)
    Me.frmCampi.Controls.Add(Me.lbQuattro)
    Me.frmCampi.Controls.Add(Me.lbDue)
    Me.frmCampi.Controls.Add(Me.lbUno)
    Me.frmCampi.Controls.Add(Me.opBarcode)
    Me.frmCampi.Controls.Add(Me.opCodart)
    Me.frmCampi.Cursor = System.Windows.Forms.Cursors.Default
    Me.frmCampi.Location = New System.Drawing.Point(10, 181)
    Me.frmCampi.Name = "frmCampi"
    Me.frmCampi.Size = New System.Drawing.Size(527, 228)
    Me.frmCampi.TabIndex = 14
    Me.frmCampi.Text = "Campi"
    '
    'lbDivEsis
    '
    Me.lbDivEsis.AutoSize = True
    Me.lbDivEsis.BackColor = System.Drawing.Color.Transparent
    Me.lbDivEsis.Location = New System.Drawing.Point(391, 204)
    Me.lbDivEsis.Name = "lbDivEsis"
    Me.lbDivEsis.NTSDbField = ""
    Me.lbDivEsis.Size = New System.Drawing.Size(51, 13)
    Me.lbDivEsis.TabIndex = 120
    Me.lbDivEsis.Text = "Dividi per"
    Me.lbDivEsis.Tooltip = ""
    Me.lbDivEsis.UseMnemonic = False
    '
    'edDivEsis
    '
    Me.edDivEsis.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDivEsis.EditValue = "1"
    Me.edDivEsis.Location = New System.Drawing.Point(444, 201)
    Me.edDivEsis.Name = "edDivEsis"
    Me.edDivEsis.NTSDbField = ""
    Me.edDivEsis.NTSFormat = "0"
    Me.edDivEsis.NTSForzaVisZoom = False
    Me.edDivEsis.NTSOldValue = "1"
    Me.edDivEsis.Properties.Appearance.Options.UseTextOptions = True
    Me.edDivEsis.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDivEsis.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDivEsis.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDivEsis.Properties.AutoHeight = False
    Me.edDivEsis.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDivEsis.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDivEsis.Size = New System.Drawing.Size(47, 20)
    Me.edDivEsis.TabIndex = 119
    '
    'opCodartBarcodeCodarfo
    '
    Me.opCodartBarcodeCodarfo.Cursor = System.Windows.Forms.Cursors.Default
    Me.opCodartBarcodeCodarfo.Location = New System.Drawing.Point(20, 25)
    Me.opCodartBarcodeCodarfo.Name = "opCodartBarcodeCodarfo"
    Me.opCodartBarcodeCodarfo.NTSCheckValue = "S"
    Me.opCodartBarcodeCodarfo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opCodartBarcodeCodarfo.Properties.Appearance.Options.UseBackColor = True
    Me.opCodartBarcodeCodarfo.Properties.AutoHeight = False
    Me.opCodartBarcodeCodarfo.Properties.Caption = "Codice Articolo o Barcode o Codice Articolo Cliente/Fornitore"
    Me.opCodartBarcodeCodarfo.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opCodartBarcodeCodarfo.Size = New System.Drawing.Size(320, 19)
    Me.opCodartBarcodeCodarfo.TabIndex = 118
    '
    'lbComA
    '
    Me.lbComA.AutoSize = True
    Me.lbComA.BackColor = System.Drawing.Color.Transparent
    Me.lbComA.Location = New System.Drawing.Point(326, 112)
    Me.lbComA.Name = "lbComA"
    Me.lbComA.NTSDbField = ""
    Me.lbComA.Size = New System.Drawing.Size(14, 13)
    Me.lbComA.TabIndex = 108
    Me.lbComA.Text = "A"
    Me.lbComA.Tooltip = ""
    Me.lbComA.UseMnemonic = False
    '
    'lbComDa
    '
    Me.lbComDa.AutoSize = True
    Me.lbComDa.BackColor = System.Drawing.Color.Transparent
    Me.lbComDa.Location = New System.Drawing.Point(253, 112)
    Me.lbComDa.Name = "lbComDa"
    Me.lbComDa.NTSDbField = ""
    Me.lbComDa.Size = New System.Drawing.Size(20, 13)
    Me.lbComDa.TabIndex = 107
    Me.lbComDa.Text = "Da"
    Me.lbComDa.Tooltip = ""
    Me.lbComDa.UseMnemonic = False
    '
    'edComA
    '
    Me.edComA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edComA.EditValue = "9999"
    Me.edComA.Location = New System.Drawing.Point(346, 109)
    Me.edComA.Name = "edComA"
    Me.edComA.NTSDbField = ""
    Me.edComA.NTSFormat = "0"
    Me.edComA.NTSForzaVisZoom = False
    Me.edComA.NTSOldValue = "9999"
    Me.edComA.Properties.Appearance.Options.UseTextOptions = True
    Me.edComA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edComA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edComA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edComA.Properties.AutoHeight = False
    Me.edComA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edComA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edComA.Size = New System.Drawing.Size(38, 20)
    Me.edComA.TabIndex = 106
    '
    'edComDa
    '
    Me.edComDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edComDa.EditValue = "1"
    Me.edComDa.Location = New System.Drawing.Point(278, 109)
    Me.edComDa.Name = "edComDa"
    Me.edComDa.NTSDbField = ""
    Me.edComDa.NTSFormat = "0"
    Me.edComDa.NTSForzaVisZoom = False
    Me.edComDa.NTSOldValue = "1"
    Me.edComDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edComDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edComDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edComDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edComDa.Properties.AutoHeight = False
    Me.edComDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edComDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edComDa.Size = New System.Drawing.Size(42, 20)
    Me.edComDa.TabIndex = 105
    '
    'lbPosCom
    '
    Me.lbPosCom.AutoSize = True
    Me.lbPosCom.BackColor = System.Drawing.Color.Transparent
    Me.lbPosCom.Location = New System.Drawing.Point(155, 112)
    Me.lbPosCom.Name = "lbPosCom"
    Me.lbPosCom.NTSDbField = ""
    Me.lbPosCom.Size = New System.Drawing.Size(51, 13)
    Me.lbPosCom.TabIndex = 104
    Me.lbPosCom.Text = "Posizione"
    Me.lbPosCom.Tooltip = ""
    Me.lbPosCom.UseMnemonic = False
    '
    'edPosCom
    '
    Me.edPosCom.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPosCom.EditValue = "1"
    Me.edPosCom.Enabled = False
    Me.edPosCom.Location = New System.Drawing.Point(210, 109)
    Me.edPosCom.Name = "edPosCom"
    Me.edPosCom.NTSDbField = ""
    Me.edPosCom.NTSFormat = "0"
    Me.edPosCom.NTSForzaVisZoom = False
    Me.edPosCom.NTSOldValue = "1"
    Me.edPosCom.Properties.Appearance.Options.UseTextOptions = True
    Me.edPosCom.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPosCom.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPosCom.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPosCom.Properties.AutoHeight = False
    Me.edPosCom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPosCom.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPosCom.Size = New System.Drawing.Size(37, 20)
    Me.edPosCom.TabIndex = 103
    '
    'ckCom
    '
    Me.ckCom.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckCom.Location = New System.Drawing.Point(20, 110)
    Me.ckCom.Name = "ckCom"
    Me.ckCom.NTSCheckValue = "S"
    Me.ckCom.NTSUnCheckValue = "N"
    Me.ckCom.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckCom.Properties.Appearance.Options.UseBackColor = True
    Me.ckCom.Properties.AutoHeight = False
    Me.ckCom.Properties.Caption = "Commessa"
    Me.ckCom.Size = New System.Drawing.Size(83, 19)
    Me.ckCom.TabIndex = 102
    '
    'lbTre
    '
    Me.lbTre.AutoSize = True
    Me.lbTre.BackColor = System.Drawing.Color.Transparent
    Me.lbTre.Location = New System.Drawing.Point(4, 112)
    Me.lbTre.Name = "lbTre"
    Me.lbTre.NTSDbField = ""
    Me.lbTre.Size = New System.Drawing.Size(17, 13)
    Me.lbTre.TabIndex = 101
    Me.lbTre.Text = "3."
    Me.lbTre.Tooltip = ""
    Me.lbTre.UseMnemonic = False
    '
    'lbEsisA
    '
    Me.lbEsisA.AutoSize = True
    Me.lbEsisA.BackColor = System.Drawing.Color.Transparent
    Me.lbEsisA.Location = New System.Drawing.Point(326, 203)
    Me.lbEsisA.Name = "lbEsisA"
    Me.lbEsisA.NTSDbField = ""
    Me.lbEsisA.Size = New System.Drawing.Size(14, 13)
    Me.lbEsisA.TabIndex = 58
    Me.lbEsisA.Text = "A"
    Me.lbEsisA.Tooltip = ""
    Me.lbEsisA.UseMnemonic = False
    '
    'lbEsisDa
    '
    Me.lbEsisDa.AutoSize = True
    Me.lbEsisDa.BackColor = System.Drawing.Color.Transparent
    Me.lbEsisDa.Location = New System.Drawing.Point(253, 203)
    Me.lbEsisDa.Name = "lbEsisDa"
    Me.lbEsisDa.NTSDbField = ""
    Me.lbEsisDa.Size = New System.Drawing.Size(20, 13)
    Me.lbEsisDa.TabIndex = 57
    Me.lbEsisDa.Text = "Da"
    Me.lbEsisDa.Tooltip = ""
    Me.lbEsisDa.UseMnemonic = False
    '
    'edEsisA
    '
    Me.edEsisA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEsisA.EditValue = "9999"
    Me.edEsisA.Enabled = False
    Me.edEsisA.Location = New System.Drawing.Point(346, 200)
    Me.edEsisA.Name = "edEsisA"
    Me.edEsisA.NTSDbField = ""
    Me.edEsisA.NTSFormat = "0"
    Me.edEsisA.NTSForzaVisZoom = False
    Me.edEsisA.NTSOldValue = "9999"
    Me.edEsisA.Properties.Appearance.Options.UseTextOptions = True
    Me.edEsisA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEsisA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEsisA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEsisA.Properties.AutoHeight = False
    Me.edEsisA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEsisA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEsisA.Size = New System.Drawing.Size(38, 20)
    Me.edEsisA.TabIndex = 56
    '
    'edEsisDa
    '
    Me.edEsisDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEsisDa.EditValue = "1"
    Me.edEsisDa.Enabled = False
    Me.edEsisDa.Location = New System.Drawing.Point(278, 200)
    Me.edEsisDa.Name = "edEsisDa"
    Me.edEsisDa.NTSDbField = ""
    Me.edEsisDa.NTSFormat = "0"
    Me.edEsisDa.NTSForzaVisZoom = False
    Me.edEsisDa.NTSOldValue = "1"
    Me.edEsisDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edEsisDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEsisDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEsisDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEsisDa.Properties.AutoHeight = False
    Me.edEsisDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEsisDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEsisDa.Size = New System.Drawing.Size(42, 20)
    Me.edEsisDa.TabIndex = 55
    '
    'lbPosEsis
    '
    Me.lbPosEsis.AutoSize = True
    Me.lbPosEsis.BackColor = System.Drawing.Color.Transparent
    Me.lbPosEsis.Location = New System.Drawing.Point(155, 203)
    Me.lbPosEsis.Name = "lbPosEsis"
    Me.lbPosEsis.NTSDbField = ""
    Me.lbPosEsis.Size = New System.Drawing.Size(51, 13)
    Me.lbPosEsis.TabIndex = 54
    Me.lbPosEsis.Text = "Posizione"
    Me.lbPosEsis.Tooltip = ""
    Me.lbPosEsis.UseMnemonic = False
    '
    'edPosEsis
    '
    Me.edPosEsis.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPosEsis.EditValue = "1"
    Me.edPosEsis.Enabled = False
    Me.edPosEsis.Location = New System.Drawing.Point(210, 200)
    Me.edPosEsis.Name = "edPosEsis"
    Me.edPosEsis.NTSDbField = ""
    Me.edPosEsis.NTSFormat = "0"
    Me.edPosEsis.NTSForzaVisZoom = False
    Me.edPosEsis.NTSOldValue = "1"
    Me.edPosEsis.Properties.Appearance.Options.UseTextOptions = True
    Me.edPosEsis.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPosEsis.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPosEsis.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPosEsis.Properties.AutoHeight = False
    Me.edPosEsis.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPosEsis.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPosEsis.Size = New System.Drawing.Size(37, 20)
    Me.edPosEsis.TabIndex = 53
    '
    'ckEsis
    '
    Me.ckEsis.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckEsis.Location = New System.Drawing.Point(20, 201)
    Me.ckEsis.Name = "ckEsis"
    Me.ckEsis.NTSCheckValue = "S"
    Me.ckEsis.NTSUnCheckValue = "N"
    Me.ckEsis.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckEsis.Properties.Appearance.Options.UseBackColor = True
    Me.ckEsis.Properties.AutoHeight = False
    Me.ckEsis.Properties.Caption = "Esistenza"
    Me.ckEsis.Size = New System.Drawing.Size(66, 19)
    Me.ckEsis.TabIndex = 52
    '
    'lbSette
    '
    Me.lbSette.AutoSize = True
    Me.lbSette.BackColor = System.Drawing.Color.Transparent
    Me.lbSette.Location = New System.Drawing.Point(4, 203)
    Me.lbSette.Name = "lbSette"
    Me.lbSette.NTSDbField = ""
    Me.lbSette.Size = New System.Drawing.Size(17, 13)
    Me.lbSette.TabIndex = 51
    Me.lbSette.Text = "7."
    Me.lbSette.Tooltip = ""
    Me.lbSette.UseMnemonic = False
    '
    'lbMatrA
    '
    Me.lbMatrA.AutoSize = True
    Me.lbMatrA.BackColor = System.Drawing.Color.Transparent
    Me.lbMatrA.Location = New System.Drawing.Point(326, 180)
    Me.lbMatrA.Name = "lbMatrA"
    Me.lbMatrA.NTSDbField = ""
    Me.lbMatrA.Size = New System.Drawing.Size(14, 13)
    Me.lbMatrA.TabIndex = 48
    Me.lbMatrA.Text = "A"
    Me.lbMatrA.Tooltip = ""
    Me.lbMatrA.UseMnemonic = False
    '
    'lbMatrDa
    '
    Me.lbMatrDa.AutoSize = True
    Me.lbMatrDa.BackColor = System.Drawing.Color.Transparent
    Me.lbMatrDa.Location = New System.Drawing.Point(253, 180)
    Me.lbMatrDa.Name = "lbMatrDa"
    Me.lbMatrDa.NTSDbField = ""
    Me.lbMatrDa.Size = New System.Drawing.Size(20, 13)
    Me.lbMatrDa.TabIndex = 47
    Me.lbMatrDa.Text = "Da"
    Me.lbMatrDa.Tooltip = ""
    Me.lbMatrDa.UseMnemonic = False
    '
    'edMatrA
    '
    Me.edMatrA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMatrA.EditValue = "9999"
    Me.edMatrA.Enabled = False
    Me.edMatrA.Location = New System.Drawing.Point(346, 177)
    Me.edMatrA.Name = "edMatrA"
    Me.edMatrA.NTSDbField = ""
    Me.edMatrA.NTSFormat = "0"
    Me.edMatrA.NTSForzaVisZoom = False
    Me.edMatrA.NTSOldValue = "9999"
    Me.edMatrA.Properties.Appearance.Options.UseTextOptions = True
    Me.edMatrA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMatrA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMatrA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMatrA.Properties.AutoHeight = False
    Me.edMatrA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMatrA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMatrA.Size = New System.Drawing.Size(38, 20)
    Me.edMatrA.TabIndex = 46
    '
    'edMatrDa
    '
    Me.edMatrDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMatrDa.EditValue = "1"
    Me.edMatrDa.Enabled = False
    Me.edMatrDa.Location = New System.Drawing.Point(278, 177)
    Me.edMatrDa.Name = "edMatrDa"
    Me.edMatrDa.NTSDbField = ""
    Me.edMatrDa.NTSFormat = "0"
    Me.edMatrDa.NTSForzaVisZoom = False
    Me.edMatrDa.NTSOldValue = "1"
    Me.edMatrDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edMatrDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMatrDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMatrDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMatrDa.Properties.AutoHeight = False
    Me.edMatrDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMatrDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMatrDa.Size = New System.Drawing.Size(42, 20)
    Me.edMatrDa.TabIndex = 45
    '
    'lbPosMatr
    '
    Me.lbPosMatr.AutoSize = True
    Me.lbPosMatr.BackColor = System.Drawing.Color.Transparent
    Me.lbPosMatr.Location = New System.Drawing.Point(155, 180)
    Me.lbPosMatr.Name = "lbPosMatr"
    Me.lbPosMatr.NTSDbField = ""
    Me.lbPosMatr.Size = New System.Drawing.Size(51, 13)
    Me.lbPosMatr.TabIndex = 44
    Me.lbPosMatr.Text = "Posizione"
    Me.lbPosMatr.Tooltip = ""
    Me.lbPosMatr.UseMnemonic = False
    '
    'edPosMatr
    '
    Me.edPosMatr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPosMatr.EditValue = "1"
    Me.edPosMatr.Enabled = False
    Me.edPosMatr.Location = New System.Drawing.Point(210, 177)
    Me.edPosMatr.Name = "edPosMatr"
    Me.edPosMatr.NTSDbField = ""
    Me.edPosMatr.NTSFormat = "0"
    Me.edPosMatr.NTSForzaVisZoom = False
    Me.edPosMatr.NTSOldValue = "1"
    Me.edPosMatr.Properties.Appearance.Options.UseTextOptions = True
    Me.edPosMatr.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPosMatr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPosMatr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPosMatr.Properties.AutoHeight = False
    Me.edPosMatr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPosMatr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPosMatr.Size = New System.Drawing.Size(37, 20)
    Me.edPosMatr.TabIndex = 43
    '
    'ckMatr
    '
    Me.ckMatr.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckMatr.Location = New System.Drawing.Point(20, 178)
    Me.ckMatr.Name = "ckMatr"
    Me.ckMatr.NTSCheckValue = "S"
    Me.ckMatr.NTSUnCheckValue = "N"
    Me.ckMatr.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckMatr.Properties.Appearance.Options.UseBackColor = True
    Me.ckMatr.Properties.AutoHeight = False
    Me.ckMatr.Properties.Caption = "Matricola"
    Me.ckMatr.Size = New System.Drawing.Size(66, 19)
    Me.ckMatr.TabIndex = 42
    '
    'lbSei
    '
    Me.lbSei.AutoSize = True
    Me.lbSei.BackColor = System.Drawing.Color.Transparent
    Me.lbSei.Location = New System.Drawing.Point(4, 180)
    Me.lbSei.Name = "lbSei"
    Me.lbSei.NTSDbField = ""
    Me.lbSei.Size = New System.Drawing.Size(17, 13)
    Me.lbSei.TabIndex = 41
    Me.lbSei.Text = "6."
    Me.lbSei.Tooltip = ""
    Me.lbSei.UseMnemonic = False
    '
    'lbUbicA
    '
    Me.lbUbicA.AutoSize = True
    Me.lbUbicA.BackColor = System.Drawing.Color.Transparent
    Me.lbUbicA.Location = New System.Drawing.Point(326, 157)
    Me.lbUbicA.Name = "lbUbicA"
    Me.lbUbicA.NTSDbField = ""
    Me.lbUbicA.Size = New System.Drawing.Size(14, 13)
    Me.lbUbicA.TabIndex = 36
    Me.lbUbicA.Text = "A"
    Me.lbUbicA.Tooltip = ""
    Me.lbUbicA.UseMnemonic = False
    '
    'lbUbicDa
    '
    Me.lbUbicDa.AutoSize = True
    Me.lbUbicDa.BackColor = System.Drawing.Color.Transparent
    Me.lbUbicDa.Location = New System.Drawing.Point(253, 157)
    Me.lbUbicDa.Name = "lbUbicDa"
    Me.lbUbicDa.NTSDbField = ""
    Me.lbUbicDa.Size = New System.Drawing.Size(20, 13)
    Me.lbUbicDa.TabIndex = 35
    Me.lbUbicDa.Text = "Da"
    Me.lbUbicDa.Tooltip = ""
    Me.lbUbicDa.UseMnemonic = False
    '
    'edUbicA
    '
    Me.edUbicA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edUbicA.EditValue = "9999"
    Me.edUbicA.Enabled = False
    Me.edUbicA.Location = New System.Drawing.Point(346, 154)
    Me.edUbicA.Name = "edUbicA"
    Me.edUbicA.NTSDbField = ""
    Me.edUbicA.NTSFormat = "0"
    Me.edUbicA.NTSForzaVisZoom = False
    Me.edUbicA.NTSOldValue = "9999"
    Me.edUbicA.Properties.Appearance.Options.UseTextOptions = True
    Me.edUbicA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edUbicA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUbicA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUbicA.Properties.AutoHeight = False
    Me.edUbicA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUbicA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUbicA.Size = New System.Drawing.Size(38, 20)
    Me.edUbicA.TabIndex = 34
    '
    'edUbicDa
    '
    Me.edUbicDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edUbicDa.EditValue = "1"
    Me.edUbicDa.Enabled = False
    Me.edUbicDa.Location = New System.Drawing.Point(278, 154)
    Me.edUbicDa.Name = "edUbicDa"
    Me.edUbicDa.NTSDbField = ""
    Me.edUbicDa.NTSFormat = "0"
    Me.edUbicDa.NTSForzaVisZoom = False
    Me.edUbicDa.NTSOldValue = "1"
    Me.edUbicDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edUbicDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edUbicDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUbicDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUbicDa.Properties.AutoHeight = False
    Me.edUbicDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUbicDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUbicDa.Size = New System.Drawing.Size(42, 20)
    Me.edUbicDa.TabIndex = 33
    '
    'lbLottoA
    '
    Me.lbLottoA.AutoSize = True
    Me.lbLottoA.BackColor = System.Drawing.Color.Transparent
    Me.lbLottoA.Location = New System.Drawing.Point(326, 134)
    Me.lbLottoA.Name = "lbLottoA"
    Me.lbLottoA.NTSDbField = ""
    Me.lbLottoA.Size = New System.Drawing.Size(14, 13)
    Me.lbLottoA.TabIndex = 32
    Me.lbLottoA.Text = "A"
    Me.lbLottoA.Tooltip = ""
    Me.lbLottoA.UseMnemonic = False
    '
    'lbLottoDa
    '
    Me.lbLottoDa.AutoSize = True
    Me.lbLottoDa.BackColor = System.Drawing.Color.Transparent
    Me.lbLottoDa.Location = New System.Drawing.Point(253, 134)
    Me.lbLottoDa.Name = "lbLottoDa"
    Me.lbLottoDa.NTSDbField = ""
    Me.lbLottoDa.Size = New System.Drawing.Size(20, 13)
    Me.lbLottoDa.TabIndex = 31
    Me.lbLottoDa.Text = "Da"
    Me.lbLottoDa.Tooltip = ""
    Me.lbLottoDa.UseMnemonic = False
    '
    'edLottoA
    '
    Me.edLottoA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edLottoA.EditValue = "9999"
    Me.edLottoA.Location = New System.Drawing.Point(346, 131)
    Me.edLottoA.Name = "edLottoA"
    Me.edLottoA.NTSDbField = ""
    Me.edLottoA.NTSFormat = "0"
    Me.edLottoA.NTSForzaVisZoom = False
    Me.edLottoA.NTSOldValue = "9999"
    Me.edLottoA.Properties.Appearance.Options.UseTextOptions = True
    Me.edLottoA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edLottoA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edLottoA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edLottoA.Properties.AutoHeight = False
    Me.edLottoA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edLottoA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edLottoA.Size = New System.Drawing.Size(38, 20)
    Me.edLottoA.TabIndex = 30
    '
    'edLottoDa
    '
    Me.edLottoDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edLottoDa.EditValue = "1"
    Me.edLottoDa.Location = New System.Drawing.Point(278, 131)
    Me.edLottoDa.Name = "edLottoDa"
    Me.edLottoDa.NTSDbField = ""
    Me.edLottoDa.NTSFormat = "0"
    Me.edLottoDa.NTSForzaVisZoom = False
    Me.edLottoDa.NTSOldValue = "1"
    Me.edLottoDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edLottoDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edLottoDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edLottoDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edLottoDa.Properties.AutoHeight = False
    Me.edLottoDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edLottoDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edLottoDa.Size = New System.Drawing.Size(42, 20)
    Me.edLottoDa.TabIndex = 29
    '
    'lbFaseA
    '
    Me.lbFaseA.AutoSize = True
    Me.lbFaseA.BackColor = System.Drawing.Color.Transparent
    Me.lbFaseA.Location = New System.Drawing.Point(326, 89)
    Me.lbFaseA.Name = "lbFaseA"
    Me.lbFaseA.NTSDbField = ""
    Me.lbFaseA.Size = New System.Drawing.Size(14, 13)
    Me.lbFaseA.TabIndex = 28
    Me.lbFaseA.Text = "A"
    Me.lbFaseA.Tooltip = ""
    Me.lbFaseA.UseMnemonic = False
    '
    'lbFaseDa
    '
    Me.lbFaseDa.AutoSize = True
    Me.lbFaseDa.BackColor = System.Drawing.Color.Transparent
    Me.lbFaseDa.Location = New System.Drawing.Point(253, 89)
    Me.lbFaseDa.Name = "lbFaseDa"
    Me.lbFaseDa.NTSDbField = ""
    Me.lbFaseDa.Size = New System.Drawing.Size(20, 13)
    Me.lbFaseDa.TabIndex = 27
    Me.lbFaseDa.Text = "Da"
    Me.lbFaseDa.Tooltip = ""
    Me.lbFaseDa.UseMnemonic = False
    '
    'edFaseA
    '
    Me.edFaseA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edFaseA.EditValue = "9999"
    Me.edFaseA.Location = New System.Drawing.Point(346, 86)
    Me.edFaseA.Name = "edFaseA"
    Me.edFaseA.NTSDbField = ""
    Me.edFaseA.NTSFormat = "0"
    Me.edFaseA.NTSForzaVisZoom = False
    Me.edFaseA.NTSOldValue = "9999"
    Me.edFaseA.Properties.Appearance.Options.UseTextOptions = True
    Me.edFaseA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edFaseA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edFaseA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edFaseA.Properties.AutoHeight = False
    Me.edFaseA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFaseA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFaseA.Size = New System.Drawing.Size(38, 20)
    Me.edFaseA.TabIndex = 26
    '
    'edFaseDa
    '
    Me.edFaseDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edFaseDa.EditValue = "1"
    Me.edFaseDa.Location = New System.Drawing.Point(278, 86)
    Me.edFaseDa.Name = "edFaseDa"
    Me.edFaseDa.NTSDbField = ""
    Me.edFaseDa.NTSFormat = "0"
    Me.edFaseDa.NTSForzaVisZoom = False
    Me.edFaseDa.NTSOldValue = "1"
    Me.edFaseDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edFaseDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edFaseDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edFaseDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edFaseDa.Properties.AutoHeight = False
    Me.edFaseDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFaseDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFaseDa.Size = New System.Drawing.Size(42, 20)
    Me.edFaseDa.TabIndex = 25
    '
    'lbArtA
    '
    Me.lbArtA.AutoSize = True
    Me.lbArtA.BackColor = System.Drawing.Color.Transparent
    Me.lbArtA.Location = New System.Drawing.Point(326, 67)
    Me.lbArtA.Name = "lbArtA"
    Me.lbArtA.NTSDbField = ""
    Me.lbArtA.Size = New System.Drawing.Size(14, 13)
    Me.lbArtA.TabIndex = 24
    Me.lbArtA.Text = "A"
    Me.lbArtA.Tooltip = ""
    Me.lbArtA.UseMnemonic = False
    '
    'lbArtDa
    '
    Me.lbArtDa.AutoSize = True
    Me.lbArtDa.BackColor = System.Drawing.Color.Transparent
    Me.lbArtDa.Location = New System.Drawing.Point(253, 67)
    Me.lbArtDa.Name = "lbArtDa"
    Me.lbArtDa.NTSDbField = ""
    Me.lbArtDa.Size = New System.Drawing.Size(20, 13)
    Me.lbArtDa.TabIndex = 23
    Me.lbArtDa.Text = "Da"
    Me.lbArtDa.Tooltip = ""
    Me.lbArtDa.UseMnemonic = False
    '
    'edArtA
    '
    Me.edArtA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edArtA.EditValue = "9999"
    Me.edArtA.Location = New System.Drawing.Point(346, 63)
    Me.edArtA.Name = "edArtA"
    Me.edArtA.NTSDbField = ""
    Me.edArtA.NTSFormat = "0"
    Me.edArtA.NTSForzaVisZoom = False
    Me.edArtA.NTSOldValue = "9999"
    Me.edArtA.Properties.Appearance.Options.UseTextOptions = True
    Me.edArtA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edArtA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edArtA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edArtA.Properties.AutoHeight = False
    Me.edArtA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edArtA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edArtA.Size = New System.Drawing.Size(38, 20)
    Me.edArtA.TabIndex = 22
    '
    'edArtDa
    '
    Me.edArtDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edArtDa.EditValue = "1"
    Me.edArtDa.Location = New System.Drawing.Point(278, 64)
    Me.edArtDa.Name = "edArtDa"
    Me.edArtDa.NTSDbField = ""
    Me.edArtDa.NTSFormat = "0"
    Me.edArtDa.NTSForzaVisZoom = False
    Me.edArtDa.NTSOldValue = "1"
    Me.edArtDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edArtDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edArtDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edArtDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edArtDa.Properties.AutoHeight = False
    Me.edArtDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edArtDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edArtDa.Size = New System.Drawing.Size(42, 20)
    Me.edArtDa.TabIndex = 21
    '
    'lbPosUbic
    '
    Me.lbPosUbic.AutoSize = True
    Me.lbPosUbic.BackColor = System.Drawing.Color.Transparent
    Me.lbPosUbic.Location = New System.Drawing.Point(155, 157)
    Me.lbPosUbic.Name = "lbPosUbic"
    Me.lbPosUbic.NTSDbField = ""
    Me.lbPosUbic.Size = New System.Drawing.Size(51, 13)
    Me.lbPosUbic.TabIndex = 20
    Me.lbPosUbic.Text = "Posizione"
    Me.lbPosUbic.Tooltip = ""
    Me.lbPosUbic.UseMnemonic = False
    '
    'lbPosLotto
    '
    Me.lbPosLotto.AutoSize = True
    Me.lbPosLotto.BackColor = System.Drawing.Color.Transparent
    Me.lbPosLotto.Location = New System.Drawing.Point(155, 134)
    Me.lbPosLotto.Name = "lbPosLotto"
    Me.lbPosLotto.NTSDbField = ""
    Me.lbPosLotto.Size = New System.Drawing.Size(51, 13)
    Me.lbPosLotto.TabIndex = 19
    Me.lbPosLotto.Text = "Posizione"
    Me.lbPosLotto.Tooltip = ""
    Me.lbPosLotto.UseMnemonic = False
    '
    'lbPosFase
    '
    Me.lbPosFase.AutoSize = True
    Me.lbPosFase.BackColor = System.Drawing.Color.Transparent
    Me.lbPosFase.Location = New System.Drawing.Point(155, 89)
    Me.lbPosFase.Name = "lbPosFase"
    Me.lbPosFase.NTSDbField = ""
    Me.lbPosFase.Size = New System.Drawing.Size(51, 13)
    Me.lbPosFase.TabIndex = 18
    Me.lbPosFase.Text = "Posizione"
    Me.lbPosFase.Tooltip = ""
    Me.lbPosFase.UseMnemonic = False
    '
    'lbPosArt
    '
    Me.lbPosArt.AutoSize = True
    Me.lbPosArt.BackColor = System.Drawing.Color.Transparent
    Me.lbPosArt.Location = New System.Drawing.Point(155, 67)
    Me.lbPosArt.Name = "lbPosArt"
    Me.lbPosArt.NTSDbField = ""
    Me.lbPosArt.Size = New System.Drawing.Size(51, 13)
    Me.lbPosArt.TabIndex = 17
    Me.lbPosArt.Text = "Posizione"
    Me.lbPosArt.Tooltip = ""
    Me.lbPosArt.UseMnemonic = False
    '
    'edPosArt
    '
    Me.edPosArt.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPosArt.EditValue = "1"
    Me.edPosArt.Enabled = False
    Me.edPosArt.Location = New System.Drawing.Point(210, 64)
    Me.edPosArt.Name = "edPosArt"
    Me.edPosArt.NTSDbField = ""
    Me.edPosArt.NTSFormat = "0"
    Me.edPosArt.NTSForzaVisZoom = False
    Me.edPosArt.NTSOldValue = "1"
    Me.edPosArt.Properties.Appearance.Options.UseTextOptions = True
    Me.edPosArt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPosArt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPosArt.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPosArt.Properties.AutoHeight = False
    Me.edPosArt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPosArt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPosArt.Size = New System.Drawing.Size(37, 20)
    Me.edPosArt.TabIndex = 16
    '
    'edPosUbic
    '
    Me.edPosUbic.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPosUbic.EditValue = "1"
    Me.edPosUbic.Enabled = False
    Me.edPosUbic.Location = New System.Drawing.Point(210, 154)
    Me.edPosUbic.Name = "edPosUbic"
    Me.edPosUbic.NTSDbField = ""
    Me.edPosUbic.NTSFormat = "0"
    Me.edPosUbic.NTSForzaVisZoom = False
    Me.edPosUbic.NTSOldValue = "1"
    Me.edPosUbic.Properties.Appearance.Options.UseTextOptions = True
    Me.edPosUbic.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPosUbic.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPosUbic.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPosUbic.Properties.AutoHeight = False
    Me.edPosUbic.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPosUbic.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPosUbic.Size = New System.Drawing.Size(37, 20)
    Me.edPosUbic.TabIndex = 15
    '
    'edPosLotto
    '
    Me.edPosLotto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPosLotto.EditValue = "1"
    Me.edPosLotto.Enabled = False
    Me.edPosLotto.Location = New System.Drawing.Point(210, 131)
    Me.edPosLotto.Name = "edPosLotto"
    Me.edPosLotto.NTSDbField = ""
    Me.edPosLotto.NTSFormat = "0"
    Me.edPosLotto.NTSForzaVisZoom = False
    Me.edPosLotto.NTSOldValue = "1"
    Me.edPosLotto.Properties.Appearance.Options.UseTextOptions = True
    Me.edPosLotto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPosLotto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPosLotto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPosLotto.Properties.AutoHeight = False
    Me.edPosLotto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPosLotto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPosLotto.Size = New System.Drawing.Size(37, 20)
    Me.edPosLotto.TabIndex = 14
    '
    'edPosFase
    '
    Me.edPosFase.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPosFase.EditValue = "1"
    Me.edPosFase.Enabled = False
    Me.edPosFase.Location = New System.Drawing.Point(210, 86)
    Me.edPosFase.Name = "edPosFase"
    Me.edPosFase.NTSDbField = ""
    Me.edPosFase.NTSFormat = "0"
    Me.edPosFase.NTSForzaVisZoom = False
    Me.edPosFase.NTSOldValue = "1"
    Me.edPosFase.Properties.Appearance.Options.UseTextOptions = True
    Me.edPosFase.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPosFase.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPosFase.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPosFase.Properties.AutoHeight = False
    Me.edPosFase.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPosFase.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPosFase.Size = New System.Drawing.Size(37, 20)
    Me.edPosFase.TabIndex = 13
    '
    'ckUbic
    '
    Me.ckUbic.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckUbic.Location = New System.Drawing.Point(20, 155)
    Me.ckUbic.Name = "ckUbic"
    Me.ckUbic.NTSCheckValue = "S"
    Me.ckUbic.NTSUnCheckValue = "N"
    Me.ckUbic.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckUbic.Properties.Appearance.Options.UseBackColor = True
    Me.ckUbic.Properties.AutoHeight = False
    Me.ckUbic.Properties.Caption = "Ubicazione"
    Me.ckUbic.Size = New System.Drawing.Size(83, 19)
    Me.ckUbic.TabIndex = 10
    '
    'ckLotto
    '
    Me.ckLotto.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckLotto.Location = New System.Drawing.Point(20, 132)
    Me.ckLotto.Name = "ckLotto"
    Me.ckLotto.NTSCheckValue = "S"
    Me.ckLotto.NTSUnCheckValue = "N"
    Me.ckLotto.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckLotto.Properties.Appearance.Options.UseBackColor = True
    Me.ckLotto.Properties.AutoHeight = False
    Me.ckLotto.Properties.Caption = "Lotto"
    Me.ckLotto.Size = New System.Drawing.Size(48, 19)
    Me.ckLotto.TabIndex = 9
    '
    'ckFase
    '
    Me.ckFase.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckFase.Location = New System.Drawing.Point(20, 87)
    Me.ckFase.Name = "ckFase"
    Me.ckFase.NTSCheckValue = "S"
    Me.ckFase.NTSUnCheckValue = "N"
    Me.ckFase.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckFase.Properties.Appearance.Options.UseBackColor = True
    Me.ckFase.Properties.AutoHeight = False
    Me.ckFase.Properties.Caption = "Fase"
    Me.ckFase.Size = New System.Drawing.Size(48, 19)
    Me.ckFase.TabIndex = 8
    '
    'lbCinque
    '
    Me.lbCinque.AutoSize = True
    Me.lbCinque.BackColor = System.Drawing.Color.Transparent
    Me.lbCinque.Location = New System.Drawing.Point(4, 157)
    Me.lbCinque.Name = "lbCinque"
    Me.lbCinque.NTSDbField = ""
    Me.lbCinque.Size = New System.Drawing.Size(17, 13)
    Me.lbCinque.TabIndex = 7
    Me.lbCinque.Text = "5."
    Me.lbCinque.Tooltip = ""
    Me.lbCinque.UseMnemonic = False
    '
    'lbQuattro
    '
    Me.lbQuattro.AutoSize = True
    Me.lbQuattro.BackColor = System.Drawing.Color.Transparent
    Me.lbQuattro.Location = New System.Drawing.Point(4, 134)
    Me.lbQuattro.Name = "lbQuattro"
    Me.lbQuattro.NTSDbField = ""
    Me.lbQuattro.Size = New System.Drawing.Size(17, 13)
    Me.lbQuattro.TabIndex = 6
    Me.lbQuattro.Text = "4."
    Me.lbQuattro.Tooltip = ""
    Me.lbQuattro.UseMnemonic = False
    '
    'lbDue
    '
    Me.lbDue.AutoSize = True
    Me.lbDue.BackColor = System.Drawing.Color.Transparent
    Me.lbDue.Location = New System.Drawing.Point(4, 89)
    Me.lbDue.Name = "lbDue"
    Me.lbDue.NTSDbField = ""
    Me.lbDue.Size = New System.Drawing.Size(17, 13)
    Me.lbDue.TabIndex = 5
    Me.lbDue.Text = "2."
    Me.lbDue.Tooltip = ""
    Me.lbDue.UseMnemonic = False
    '
    'lbUno
    '
    Me.lbUno.AutoSize = True
    Me.lbUno.BackColor = System.Drawing.Color.Transparent
    Me.lbUno.Location = New System.Drawing.Point(4, 47)
    Me.lbUno.Name = "lbUno"
    Me.lbUno.NTSDbField = ""
    Me.lbUno.Size = New System.Drawing.Size(17, 13)
    Me.lbUno.TabIndex = 4
    Me.lbUno.Text = "1."
    Me.lbUno.Tooltip = ""
    Me.lbUno.UseMnemonic = False
    '
    'opBarcode
    '
    Me.opBarcode.Cursor = System.Windows.Forms.Cursors.Default
    Me.opBarcode.Location = New System.Drawing.Point(20, 64)
    Me.opBarcode.Name = "opBarcode"
    Me.opBarcode.NTSCheckValue = "S"
    Me.opBarcode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opBarcode.Properties.Appearance.Options.UseBackColor = True
    Me.opBarcode.Properties.AutoHeight = False
    Me.opBarcode.Properties.Caption = "Barcode"
    Me.opBarcode.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opBarcode.Size = New System.Drawing.Size(66, 19)
    Me.opBarcode.TabIndex = 2
    '
    'opCodart
    '
    Me.opCodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.opCodart.EditValue = True
    Me.opCodart.Location = New System.Drawing.Point(20, 45)
    Me.opCodart.Name = "opCodart"
    Me.opCodart.NTSCheckValue = "S"
    Me.opCodart.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opCodart.Properties.Appearance.Options.UseBackColor = True
    Me.opCodart.Properties.AutoHeight = False
    Me.opCodart.Properties.Caption = "Cod. articolo"
    Me.opCodart.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opCodart.Size = New System.Drawing.Size(89, 19)
    Me.opCodart.TabIndex = 1
    '
    'lbNomeFile
    '
    Me.lbNomeFile.AutoSize = True
    Me.lbNomeFile.BackColor = System.Drawing.Color.Transparent
    Me.lbNomeFile.Location = New System.Drawing.Point(10, 60)
    Me.lbNomeFile.Name = "lbNomeFile"
    Me.lbNomeFile.NTSDbField = ""
    Me.lbNomeFile.Size = New System.Drawing.Size(104, 13)
    Me.lbNomeFile.TabIndex = 8
    Me.lbNomeFile.Text = "Percorso e nome file"
    Me.lbNomeFile.Tooltip = ""
    Me.lbNomeFile.UseMnemonic = False
    '
    'ckSilent
    '
    Me.ckSilent.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSilent.Location = New System.Drawing.Point(220, 90)
    Me.ckSilent.Name = "ckSilent"
    Me.ckSilent.NTSCheckValue = "S"
    Me.ckSilent.NTSUnCheckValue = "N"
    Me.ckSilent.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSilent.Properties.Appearance.Options.UseBackColor = True
    Me.ckSilent.Properties.AutoHeight = False
    Me.ckSilent.Properties.Caption = "Lancia il comando in modalità nascosta"
    Me.ckSilent.Size = New System.Drawing.Size(212, 19)
    Me.ckSilent.TabIndex = 10
    '
    'ckShowLog
    '
    Me.ckShowLog.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckShowLog.Location = New System.Drawing.Point(220, 150)
    Me.ckShowLog.Name = "ckShowLog"
    Me.ckShowLog.NTSCheckValue = "S"
    Me.ckShowLog.NTSUnCheckValue = "N"
    Me.ckShowLog.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckShowLog.Properties.Appearance.Options.UseBackColor = True
    Me.ckShowLog.Properties.AutoHeight = False
    Me.ckShowLog.Properties.Caption = "Visualizza file log con gli errori di importazione"
    Me.ckShowLog.Size = New System.Drawing.Size(247, 19)
    Me.ckShowLog.TabIndex = 13
    '
    'edNomeFile
    '
    Me.edNomeFile.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNomeFile.Location = New System.Drawing.Point(128, 57)
    Me.edNomeFile.Name = "edNomeFile"
    Me.edNomeFile.NTSDbField = ""
    Me.edNomeFile.NTSForzaVisZoom = False
    Me.edNomeFile.NTSOldValue = ""
    Me.edNomeFile.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNomeFile.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNomeFile.Properties.AutoHeight = False
    Me.edNomeFile.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNomeFile.Properties.MaxLength = 65536
    Me.edNomeFile.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNomeFile.Size = New System.Drawing.Size(378, 20)
    Me.edNomeFile.TabIndex = 7
    '
    'ckDelFile
    '
    Me.ckDelFile.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDelFile.Location = New System.Drawing.Point(220, 110)
    Me.ckDelFile.Name = "ckDelFile"
    Me.ckDelFile.NTSCheckValue = "S"
    Me.ckDelFile.NTSUnCheckValue = "N"
    Me.ckDelFile.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDelFile.Properties.Appearance.Options.UseBackColor = True
    Me.ckDelFile.Properties.AutoHeight = False
    Me.ckDelFile.Properties.Caption = "Elimina il file a fine acquisizione"
    Me.ckDelFile.Size = New System.Drawing.Size(173, 19)
    Me.ckDelFile.TabIndex = 11
    '
    'ckIgnore1rk
    '
    Me.ckIgnore1rk.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckIgnore1rk.Location = New System.Drawing.Point(220, 130)
    Me.ckIgnore1rk.Name = "ckIgnore1rk"
    Me.ckIgnore1rk.NTSCheckValue = "S"
    Me.ckIgnore1rk.NTSUnCheckValue = "N"
    Me.ckIgnore1rk.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckIgnore1rk.Properties.Appearance.Options.UseBackColor = True
    Me.ckIgnore1rk.Properties.AutoHeight = False
    Me.ckIgnore1rk.Properties.Caption = "Ignora primo record"
    Me.ckIgnore1rk.Size = New System.Drawing.Size(125, 19)
    Me.ckIgnore1rk.TabIndex = 12
    '
    'cmdNomeFile
    '
    Me.cmdNomeFile.ImageText = ""
    Me.cmdNomeFile.Location = New System.Drawing.Point(512, 57)
    Me.cmdNomeFile.Name = "cmdNomeFile"
    Me.cmdNomeFile.NTSContextMenu = Nothing
    Me.cmdNomeFile.Size = New System.Drawing.Size(25, 20)
    Me.cmdNomeFile.TabIndex = 6
    Me.cmdNomeFile.Text = "..."
    '
    'cmdAggiorna
    '
    Me.cmdAggiorna.ImageText = ""
    Me.cmdAggiorna.Location = New System.Drawing.Point(393, 12)
    Me.cmdAggiorna.Name = "cmdAggiorna"
    Me.cmdAggiorna.NTSContextMenu = Nothing
    Me.cmdAggiorna.Size = New System.Drawing.Size(70, 23)
    Me.cmdAggiorna.TabIndex = 516
    Me.cmdAggiorna.Text = "Aggiorna"
    '
    'FRMMGTERM
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(684, 420)
    Me.Controls.Add(Me.pnControlli)
    Me.Controls.Add(Me.pnPulsanti)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMMGTERM"
    Me.Text = "IMPOSTAZIONI FILE TERMINALE"
    CType(Me.ckExecute.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPulsanti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPulsanti.ResumeLayout(False)
    CType(Me.pnControlli, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnControlli.ResumeLayout(False)
    Me.pnControlli.PerformLayout()
    CType(Me.cbImpostaFile.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frmTipofile, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frmTipofile.ResumeLayout(False)
    Me.frmTipofile.PerformLayout()
    CType(Me.edDecSep.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSepField.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opDelim.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opFixed.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frmCampi, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frmCampi.ResumeLayout(False)
    Me.frmCampi.PerformLayout()
    CType(Me.edDivEsis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opCodartBarcodeCodarfo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edComA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edComDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPosCom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckCom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEsisA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEsisDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPosEsis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckEsis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edMatrA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edMatrDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPosMatr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckMatr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edUbicA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edUbicDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edLottoA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edLottoDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFaseA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFaseDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edArtA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edArtDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPosArt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPosUbic.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPosLotto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPosFase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckUbic.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckLotto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckFase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opBarcode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opCodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSilent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckShowLog.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNomeFile.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDelFile.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckIgnore1rk.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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

    '------------------------------
    'collego il datatable agli altri oggetti di form

    Return True
  End Function

  Public Overridable Sub InitEntity(ByRef cleLsar As CLEMGLSAR)
    oCleLsar = cleLsar
    AddHandler oCleLsar.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '--------------------------------------------------------------------------------------------------------------
      cbImpostaFile.NTSSetParam(oApp.Tr(Me, 128759950861372722, "Impostazione File"))
      '--------------------------------------------------------------------------------------------------------------
      edNomeFile.NTSSetParam(oMenu, oApp.Tr(Me, 128571233823125000, "Nome file"), 0, True)
      edDecSep.NTSSetParam(oMenu, oApp.Tr(Me, 128571233822343750, "Separatore decimale"), 1, True)
      edSepField.NTSSetParam(oMenu, oApp.Tr(Me, 128571233822656250, "Separatore di campo"), 1, True)
      '--------------------------------------------------------------------------------------------------------------
      opFixed.NTSSetParam(oMenu, oApp.Tr(Me, 128571233822968750, "Posizioni fisse"), "F")
      opDelim.NTSSetParam(oMenu, oApp.Tr(Me, 128571233822812500, "Separat. di campo"), "S")
      opCodartBarcodeCodarfo.NTSSetParam(oMenu, oApp.Tr(Me, 129161457518144922, "Cod. articolo/Barcode/Cod. art. C/F."), "X")
      opCodart.NTSSetParam(oMenu, oApp.Tr(Me, 128775259974828000, "Cod. articolo"), "C")
      opBarcode.NTSSetParam(oMenu, oApp.Tr(Me, 128571233821562500, "Barcode"), "B")
      '--------------------------------------------------------------------------------------------------------------
      ckShowLog.NTSSetParam(oMenu, oApp.Tr(Me, 128571233821718750, "Visualizza file log con gli errori di importazione"), "-1", "0")
      ckIgnore1rk.NTSSetParam(oMenu, oApp.Tr(Me, 128571233821875000, "Ignora primo record"), "-1", "0")
      ckDelFile.NTSSetParam(oMenu, oApp.Tr(Me, 128571233822031250, "Elimina il file a fine acquisizione"), "-1", "0")
      ckSilent.NTSSetParam(oMenu, oApp.Tr(Me, 128571233822187500, "Lancia il comando in modalità nascosta"), "-1", "0")
      ckExecute.NTSSetParam(oMenu, oApp.Tr(Me, 128571233823437500, "Esegui il comando"), "-1", "0")
      '--------------------------------------------------------------------------------------------------------------
      ckFase.NTSSetParam(oMenu, oApp.Tr(Me, 128571233820625000, "Fase"), "-1", "0")
      ckCom.NTSSetParam(oMenu, oApp.Tr(Me, 129037913420961597, "Commessa"), "-1", "0")
      ckLotto.NTSSetParam(oMenu, oApp.Tr(Me, 128571233820468750, "Lotto"), "-1", "0")
      ckUbic.NTSSetParam(oMenu, oApp.Tr(Me, 128571233820312500, "Ubicazione"), "-1", "0")
      ckMatr.NTSSetParam(oMenu, oApp.Tr(Me, 128775271769980000, "Matricola"), "S", "N")
      ckEsis.NTSSetParam(oMenu, oApp.Tr(Me, 128775271768420000, "Esistenza"), "S", "N")
      '--------------------------------------------------------------------------------------------------------------
      edPosArt.NTSSetParam(oMenu, oApp.Tr(Me, 128571233819375000, "Posizione articolo"), "0", 3, 0, 255)
      edPosFase.NTSSetParam(oMenu, oApp.Tr(Me, 128571233819843750, "Posizione fase"), "0", 3, 0, 255)
      edPosCom.NTSSetParam(oMenu, oApp.Tr(Me, 129037917377728802, "Posizione commessa"), "0", 3, 0, 255)
      edPosUbic.NTSSetParam(oMenu, oApp.Tr(Me, 128571233819531250, "Posizione lotto"), "0", 3, 0, 255)
      edPosLotto.NTSSetParam(oMenu, oApp.Tr(Me, 128571233819687500, "Posizione ubicazione"), "0", 3, 0, 255)
      edPosMatr.NTSSetParam(oMenu, oApp.Tr(Me, 128775271769824000, "Posizione matricola"), "0", 3, 0, 255)
      edPosEsis.NTSSetParam(oMenu, oApp.Tr(Me, 128775271768264000, "Posizione esistenza"), "0", 3, 0, 255)
      '--------------------------------------------------------------------------------------------------------------
      edArtDa.NTSSetParam(oMenu, oApp.Tr(Me, 128571233818593750, "Articoo DA"), "0", 4, 0, 9999)
      edFaseDa.NTSSetParam(oMenu, oApp.Tr(Me, 128571233817968750, "Fase DA"), "0", 4, 0, 9999)
      edComDa.NTSSetParam(oMenu, oApp.Tr(Me, 129037916537416302, "Commessa DA"), "0", 4, 0, 9999)
      edLottoDa.NTSSetParam(oMenu, oApp.Tr(Me, 128571233817343750, "Lotto DA"), "0", 4, 0, 9999)
      edUbicDa.NTSSetParam(oMenu, oApp.Tr(Me, 128571233816718750, "Ubicazione DA"), "0", 4, 0, 9999)
      edMatrDa.NTSSetParam(oMenu, oApp.Tr(Me, 128775271769512000, "Matricola DA"), "0", 4, 0, 9999)
      edEsisDa.NTSSetParam(oMenu, oApp.Tr(Me, 128775271767952000, "Esistenza DA"), "0", 4, 0, 9999)
      '--------------------------------------------------------------------------------------------------------------
      edArtA.NTSSetParam(oMenu, oApp.Tr(Me, 128571233818437500, "Articolo A"), "0", 4, 0, 9999)
      edFaseA.NTSSetParam(oMenu, oApp.Tr(Me, 128571233817812500, "Fase A"), "0", 4, 0, 9999)
      edComA.NTSSetParam(oMenu, oApp.Tr(Me, 129037920091791302, "Commessa A"), "0", 4, 0, 9999)
      edUbicA.NTSSetParam(oMenu, oApp.Tr(Me, 128571233816562500, "Lotto A"), "0", 4, 0, 9999)
      edLottoA.NTSSetParam(oMenu, oApp.Tr(Me, 128571233817187500, "Ubicazione A"), "0", 4, 0, 9999)
      edMatrA.NTSSetParam(oMenu, oApp.Tr(Me, 128775271769356000, "Matricola A"), "0", 4, 0, 9999)
      edEsisA.NTSSetParam(oMenu, oApp.Tr(Me, 128775271767796000, "Esistenza A"), "0", 4, 0, 9999)
      '--------------------------------------------------------------------------------------------------------------
      edDivEsis.NTSSetParam(oMenu, oApp.Tr(Me, 128571233815937500, "Dividi prezzo"), "0", 5, 1, 10000)

      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRMMGTERM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Predispongo i controlli
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      '--- Sempre alla fine di questa funzione: applico le regole della gctl
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub FRMMGTERM_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      '--------------------------------------------------------------------------------------------------------------
      bOnLoading = True
      '--------------------------------------------------------------------------------------------------------------
      CaricaCombo("")
      '--------------------------------------------------------------------------------------------------------------
      oCleLsar.strTermKey = cbImpostaFile.SelectedValue
      '--------------------------------------------------------------------------------------------------------------
      CaricaMaschera()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
      bOnLoading = False
    End Try
  End Sub
#End Region

#Region "Eventi"
  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      If Salva() = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      Me.Close()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdCommand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCommand.Click
    Try
      Dim oDlg As New NTSOpenFileDialog
      oDlg.Multiselect = False
      oDlg.ShowHelp = False
      oDlg.Title = oApp.Tr(Me, 129247728571380702, "File da eseguire")
      oDlg.DefaultExt = "EXE"
      oDlg.Filter = "Applicazioni (*.exe)|*.exe|Tutti i file (*.*)|*.*"
      oDlg.InitialDirectory = oApp.CmbDir
      oDlg.FileName = ""
      oDlg.oMenu = oMenu
      oDlg.ShowDialog()
      If oDlg.FileName <> "" Then lbCommand.Text = oDlg.FileName
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdNomeFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNomeFile.Click
    Try
      Dim oDlg As New NTSOpenFileDialog
      oDlg.Multiselect = False
      oDlg.ShowHelp = False
      oDlg.Title = oApp.Tr(Me, 129247728792619332, "File da acquisire")
      oDlg.DefaultExt = "TXT"
      oDlg.Filter = "File di testo (*.txt)|*.txt|Tutti i file (*.*)|*.*"
      oDlg.InitialDirectory = oApp.AscDir
      oDlg.FileName = ""
      oDlg.oMenu = oMenu
      oDlg.ShowDialog()
      If oDlg.FileName <> "" Then edNomeFile.Text = oDlg.FileName
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdAggiungi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAggiungi.Click
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If Salva() = False Then Return
      '--------------------------------------------------------------------------------------------------------------
Richiedi:
      '--------------------------------------------------------------------------------------------------------------
      strTmp = oApp.InputBoxNew(oApp.Tr(Me, 130438354805533977, _
        "Indicare il nome del nuovo layout (senza spazi)" & vbCrLf & _
        "(max 10 caratteri compresi fra A-Z, a-z, 0-9):" & vbCrLf))
      '--------------------------------------------------------------------------------------------------------------
      If strTmp.Trim = "" Then Return
      '--------------------------------------------------------------------------------------------------------------
      If strTmp.Length > 10 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130438357996758353, "Attenzione!" & vbCrLf & _
          "Il nome del nuovo layout non deve superare i 10 caratteri."))
        GoTo Richiedi
      End If
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = 1 To strTmp.Length
        If Asc(Mid(strTmp, i, 1)) = 32 Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 130438389974528425, "Attenzione!" & vbCrLf & _
            "Il nome del nuovo layout NON può contenere spazi."))
          GoTo Richiedi
        End If
      Next
      For i As Integer = 1 To strTmp.Length
        If (Not (Asc(Mid(strTmp, i, 1)) >= 65 And Asc(Mid(strTmp, i, 1)) <= 90)) And _
           (Not (Asc(Mid(strTmp, i, 1)) >= 97 And Asc(Mid(strTmp, i, 1)) <= 122)) And _
           (Not (Asc(Mid(strTmp, i, 1)) >= 48 And Asc(Mid(strTmp, i, 1)) <= 57)) Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 130438361590900688, "Attenzione!" & vbCrLf & _
            "Il nome del nuovo layout può contenere SOLO caratteri compresi fra:" & vbCrLf & _
            " . A - Z" & vbCrLf & _
            " . a - z" & vbCrLf & _
            " . 0 - 9"))
          GoTo Richiedi
        End If
      Next
      '--------------------------------------------------------------------------------------------------------------
      strTmp = "Terminale_" & strTmp
      '--------------------------------------------------------------------------------------------------------------
      If oCleLsar.ImpostazioneFileGiaEsistente(strTmp) = True Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130438366359074360, "Attenzione!" & vbCrLf & _
          "Esiste già un layout file con questo nome." & vbCrLf & _
          "Ripetere."))
        GoTo Richiedi
      End If
      '--------------------------------------------------------------------------------------------------------------
      bOnLoading = True
      oCleLsar.strTermKey = strTmp
      CaricaCombo(strTmp)
      CaricaMaschera()
      bOnLoading = False
      cbImpostaFile.SelectedValue = strTmp
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      bOnLoading = False
    End Try
  End Sub
  Public Overridable Sub cmdAggiorna_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAggiorna.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      Salva()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdCancella_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancella.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      If cbImpostaFile.SelectedValue.ToLower = "terminale" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130438375936652917, "Attenzione!" & vbCrLf & _
          "Il layout '(Predefinito)' non può essere eliminato."))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 130438378110868306, _
        "Confermare l'eliminazione del layout '|" & cbImpostaFile.SelectedItem.ToString & "|'?")) = Windows.Forms.DialogResult.No Then
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCleLsar.DeleteImpostazioniFile(cbImpostaFile.SelectedValue) = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      cbImpostaFile.SelectedValue = "Terminale"
      CaricaCombo("")
      CaricaMaschera()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi ComboBox"
  Public Overridable Sub cbImpostaFile_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbImpostaFile.SelectedIndexChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bOnLoading = True Then Return
      '--------------------------------------------------------------------------------------------------------------
      oCleLsar.strTermKey = cbImpostaFile.SelectedValue
      '--------------------------------------------------------------------------------------------------------------
      CaricaMaschera()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "per bloccare/sbloccare i controlli a video"
  Public Overridable Sub ckExecute_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckExecute.CheckedChanged
    Try
      ImpostaCampi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub opFixed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opFixed.CheckedChanged
    Try
      ImpostaCampi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub opCodart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opCodart.CheckedChanged
    Try
      If opCodart.Checked Then ImpostaCampi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opBarcode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opBarcode.CheckedChanged
    Try
      If opBarcode.Checked Then ImpostaCampi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckFase_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckFase.CheckedChanged
    Try
      ImpostaCampi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckCom_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckCom.CheckedChanged
    Try
      ImpostaCampi()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub ckLotto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckLotto.CheckedChanged
    Try
      ImpostaCampi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckUbic_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckUbic.CheckedChanged
    Try
      ImpostaCampi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckMatr_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckMatr.CheckedChanged
    Try
      ImpostaCampi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckEsis_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckEsis.CheckedChanged
    Try
      ImpostaCampi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function ImpostaCampi() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bInLoad Then Return True
      '--------------------------------------------------------------------------------------------------------------
      If ckExecute.Checked Then
        ckSilent.Enabled = True
        cmdCommand.Enabled = True
      Else
        ckSilent.Enabled = False
        cmdCommand.Enabled = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If opFixed.Checked Then
        edPosArt.Text = "0"
        edPosArt.Enabled = False
        edArtDa.Enabled = True
        edArtA.Enabled = True

        If ckFase.Checked Then
          edPosFase.Text = "0"
          edPosFase.Enabled = False
          edFaseDa.Enabled = True
          edFaseA.Enabled = True
        Else
          edPosFase.Text = "0"
          edPosFase.Enabled = False
          edFaseDa.Text = "0"
          edFaseDa.Enabled = False
          edFaseA.Text = "0"
          edFaseA.Enabled = False
        End If

        If ckCom.Checked Then
          edPosCom.Text = "0"
          edPosCom.Enabled = False
          edComDa.Enabled = True
          edComA.Enabled = True
        Else
          edPosCom.Text = "0"
          edPosCom.Enabled = False
          edComDa.Text = "0"
          edComDa.Enabled = False
          edComA.Text = "0"
          edComA.Enabled = False
        End If

        If ckLotto.Checked Then
          edPosLotto.Text = "0"
          edPosLotto.Enabled = False
          edLottoDa.Enabled = True
          edLottoA.Enabled = True
        Else
          edPosLotto.Text = "0"
          edPosLotto.Enabled = False
          edLottoDa.Text = "0"
          edLottoDa.Enabled = False
          edLottoA.Text = "0"
          edLottoA.Enabled = False
        End If

        If ckUbic.Checked Then
          edPosUbic.Text = "0"
          edPosUbic.Enabled = False
          edUbicDa.Enabled = True
          edUbicA.Enabled = True
        Else
          edPosUbic.Text = "0"
          edPosUbic.Enabled = False
          edUbicDa.Text = "0"
          edUbicDa.Enabled = False
          edUbicA.Text = "0"
          edUbicA.Enabled = False
        End If

        If ckMatr.Checked Then
          edPosMatr.Text = "0"
          edPosMatr.Enabled = False
          edMatrDa.Enabled = True
          edMatrA.Enabled = True
        Else
          edPosMatr.Text = "0"
          edPosMatr.Enabled = False
          edMatrDa.Text = "0"
          edMatrDa.Enabled = False
          edMatrA.Text = "0"
          edMatrA.Enabled = False
        End If

        If ckEsis.Checked Then
          edPosEsis.Text = "0"
          edPosEsis.Enabled = False
          edEsisDa.Enabled = True
          edEsisA.Enabled = True
          edDivEsis.Enabled = True
        Else
          edPosEsis.Text = "0"
          edPosEsis.Enabled = False
          edEsisDa.Text = "0"
          edEsisDa.Enabled = False
          edEsisA.Text = "0"
          edEsisA.Enabled = False
          edDivEsis.Enabled = False
        End If
        edSepField.Text = ";"
        edSepField.Enabled = False
      Else
        'codart
        edPosArt.Enabled = True
        edArtDa.Text = "0"
        edArtDa.Enabled = False
        edArtA.Text = "0"
        edArtA.Enabled = False

        If ckFase.Checked Then
          edPosFase.Enabled = True
          edFaseDa.Text = "0"
          edFaseDa.Enabled = False
          edFaseA.Text = "0"
          edFaseA.Enabled = False
        Else
          edPosFase.Text = "0"
          edPosFase.Enabled = False
          edFaseDa.Text = "0"
          edFaseDa.Enabled = False
          edFaseA.Text = "0"
          edFaseA.Enabled = False
        End If

        If ckCom.Checked Then
          edPosCom.Enabled = True
          edComDa.Text = "0"
          edComDa.Enabled = False
          edComA.Text = "0"
          edComA.Enabled = False
        Else
          edPosCom.Text = "0"
          edPosCom.Enabled = False
          edComDa.Text = "0"
          edComDa.Enabled = False
          edComA.Text = "0"
          edComA.Enabled = False
        End If

        If ckLotto.Checked Then
          edPosLotto.Enabled = True
          edLottoDa.Text = "0"
          edLottoDa.Enabled = False
          edLottoA.Text = "0"
          edLottoA.Enabled = False
        Else
          edPosLotto.Text = "0"
          edPosLotto.Enabled = False
          edLottoDa.Text = "0"
          edLottoDa.Enabled = False
          edLottoA.Text = "0"
          edLottoA.Enabled = False
        End If

        If ckUbic.Checked Then
          edPosUbic.Enabled = True
          edUbicDa.Text = "0"
          edUbicDa.Enabled = False
          edUbicA.Text = "0"
          edUbicA.Enabled = False
        Else
          edPosUbic.Text = "0"
          edPosUbic.Enabled = False
          edUbicDa.Text = "0"
          edUbicDa.Enabled = False
          edUbicA.Text = "0"
          edUbicA.Enabled = False
        End If

        If ckMatr.Checked Then
          edPosMatr.Enabled = True
          edMatrDa.Text = "0"
          edMatrDa.Enabled = False
          edMatrA.Text = "0"
          edMatrA.Enabled = False
        Else
          edPosMatr.Text = "0"
          edPosMatr.Enabled = False
          edMatrDa.Text = "0"
          edMatrDa.Enabled = False
          edMatrA.Text = "0"
          edMatrA.Enabled = False
        End If

        If ckEsis.Checked Then
          edPosEsis.Enabled = True
          edEsisDa.Text = "0"
          edEsisDa.Enabled = False
          edEsisA.Text = "0"
          edEsisA.Enabled = False
          edDivEsis.Enabled = True
        Else
          edPosEsis.Text = "0"
          edPosEsis.Enabled = False
          edEsisDa.Text = "0"
          edEsisDa.Enabled = False
          edEsisA.Text = "0"
          edEsisA.Enabled = False
          edDivEsis.Enabled = False
        End If
        edSepField.Enabled = True
      End If    'If opFixed.Checked Then

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
#End Region

  Public Overridable Sub CaricaCombo(ByVal strNew As String)
    Dim dttImpostaFile As New DataTable()
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      dttImpostaFile.Columns.Add("cod", GetType(String))
      dttImpostaFile.Columns.Add("val", GetType(String))
      dttImpostaFile.Rows.Add(New Object() {"Terminale", "(Predefinito)"})
      If oCleLsar.GetImpostazioniFile(dttTmp) = True Then
        For i As Integer = 0 To (dttTmp.Rows.Count - 1)
          With dttTmp.Rows(i)
            dttImpostaFile.Rows.Add(New Object() {NTSCStr(!rp_liv2), Mid(NTSCStr(!rp_liv2), 11)})
          End With
        Next
      End If
      If strNew.Trim <> "" Then dttImpostaFile.Rows.Add(New Object() {strNew, Mid(strNew, 11)})
      dttImpostaFile.AcceptChanges()
      cbImpostaFile.DataSource = dttImpostaFile
      cbImpostaFile.ValueMember = "cod"
      cbImpostaFile.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Sub
  Public Overridable Sub CaricaMaschera()
    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Imposta la finestra con le opzioni di registro come eventualmente indicato
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.WaitCursor
      If Not oCleLsar.TerminaleLeggiCheck(oCallParams.strNomProg) Then
        Me.Close()
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Ora imposta la maschera
      '--------------------------------------------------------------------------------------------------------------
      bInLoad = True
      If oCleLsar.bTermExecute Then
        ckExecute.Checked = True
        lbCommand.Text = oCleLsar.strTermCommand
        If oCleLsar.bTermSilent Then ckSilent.Checked = True Else ckSilent.Checked = False
      Else
        ckExecute.Checked = False
      End If
      edNomeFile.Text = oCleLsar.strTermFileName
      If oCleLsar.bTermDeleteAfter Then ckDelFile.Checked = True Else ckDelFile.Checked = False
      If oCleLsar.bTermIgnoreFirstRow Then ckIgnore1rk.Checked = True Else ckIgnore1rk.Checked = False
      If oCleLsar.bTermShowLogErr Then ckShowLog.Checked = True Else ckShowLog.Checked = False
      If oCleLsar.bTermFase Then ckFase.Checked = True Else ckFase.Checked = False
      If oCleLsar.bTermCom Then ckCom.Checked = True Else ckCom.Checked = False
      If oCleLsar.bTermLotto Then ckLotto.Checked = True Else ckLotto.Checked = False
      If oCleLsar.bTermUbic Then ckUbic.Checked = True Else ckUbic.Checked = False
      If oCleLsar.bTermMatr Then ckMatr.Checked = True Else ckMatr.Checked = False
      If oCleLsar.bTermEsis Then ckEsis.Checked = True Else ckEsis.Checked = False
      '--------------------------------------------------------------------------------------------------------------
      Select Case oCleLsar.strTermTipoArt
        Case "X"  'articolo o barcode
          opCodartBarcodeCodarfo.Checked = True
        Case "C" 'codart
          opCodart.Checked = True
        Case "B" 'barcode(default)
          opBarcode.Checked = True
      End Select
      edDecSep.Text = oCleLsar.strTermDecimalSep
      Select Case oCleLsar.strTermFileType
        Case "F" 'fixed(default)
          opFixed.Checked = True
        Case Else 'S=carattere separatore
          opDelim.Checked = True
          edSepField.Text = oCleLsar.strTermFieldSep
      End Select
      '--------------------------------------------------------------------------------------------------------------
      edPosArt.Text = oCleLsar.nTermCodartPos.ToString
      '--------------------------------------------------------------------------------------------------------------
      edPosFase.Text = oCleLsar.nTermFasePos.ToString
      edPosCom.Text = oCleLsar.nTermComPos.ToString
      edPosLotto.Text = oCleLsar.nTermLottoPos.ToString
      edPosUbic.Text = oCleLsar.nTermUbicPos.ToString
      edPosMatr.Text = oCleLsar.nTermMatrPos.ToString
      edPosEsis.Text = oCleLsar.nTermEsisPos.ToString
      '--------------------------------------------------------------------------------------------------------------
      edArtDa.Text = oCleLsar.nTermCodartStart.ToString
      edFaseDa.Text = oCleLsar.nTermFaseStart.ToString
      edComDa.Text = oCleLsar.nTermComStart.ToString
      edLottoDa.Text = oCleLsar.nTermLottoStart.ToString
      edUbicDa.Text = oCleLsar.nTermUbicStart.ToString
      edMatrDa.Text = oCleLsar.nTermMatrStart.ToString
      edEsisDa.Text = oCleLsar.nTermEsisStart.ToString
      '--------------------------------------------------------------------------------------------------------------
      edArtA.Text = oCleLsar.nTermCodartStop.ToString
      edFaseA.Text = oCleLsar.nTermFaseStop.ToString
      edComA.Text = oCleLsar.nTermComStop.ToString
      edLottoA.Text = oCleLsar.nTermLottoStop.ToString
      edUbicA.Text = oCleLsar.nTermUbicStop.ToString
      edMatrA.Text = oCleLsar.nTermMatrStop.ToString
      edEsisA.Text = oCleLsar.nTermEsisStop.ToString
      '--------------------------------------------------------------------------------------------------------------
      edDivEsis.Text = oCleLsar.nTermDivEsis.ToString
      '--------------------------------------------------------------------------------------------------------------
      bInLoad = False
      '--------------------------------------------------------------------------------------------------------------
      ImpostaCampi()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Function Salva() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlli preliminari
      '--------------------------------------------------------------------------------------------------------------
      If ckExecute.Checked Then
        If lbCommand.Text.Trim = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129247970939133080, "File terminale: Comando da eseguire obbligatorio."))
          Return False
        End If
      End If
      If edNomeFile.Text.Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129247970954133080, "File terminale: Nome di file obbligatorio."))
        Return False
      End If
      If opDelim.Checked Then
        If edSepField.Text = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129247970963195580, "File terminale: Separatore di campo obbligatorio."))
          Return False
        End If
      End If
      If edDecSep.Text.Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129247970984289330, "File terminale: Separatore decimale obbligatorio."))
        Return False
      End If
      If edDecSep.Text <> "," And edDecSep.Text <> "." Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129247970995383080, "File terminale: Il separatore decimale deve essere '.' (punto) o ',' (virgola)."))
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not opFixed.Checked Then
        '------------------------------------------------------------------------------------------------------------
        '--- Controlla validità numeri
        '--- Controlla congruenza
        '------------------------------------------------------------------------------------------------------------
        If ckFase.Checked Then
          If edPosArt.Text = edPosFase.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128571245200312500, "La posizione del campo fase non deve essere uguale alla posizione del campo lotto."))
            Return False
          End If
        End If
        If ckCom.Checked Then
          If edPosArt.Text = edPosCom.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037935620266712, "La posizione del campo commessa non deve essere uguale alla posizione del campo lotto."))
            Return False
          End If
        End If
        If ckLotto.Checked Then
          If edPosArt.Text = edPosLotto.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128571245342031250, "La posizione del campo articolo non deve essere uguale alla posizione del campo lotto."))
            Return False
          End If
        End If
        If ckUbic.Checked Then
          If edPosArt.Text = edPosUbic.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260054076000, "La posizione del campo articolo non deve essere uguale alla posizione del campo ubicazione."))
            Return False
          End If
        End If
        If ckMatr.Checked Then
          If edPosArt.Text = edPosMatr.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775253933208000, "La posizione del campo articolo non deve essere uguale alla posizione del campo matricola."))
            Return False
          End If
        End If
        If ckEsis.Checked Then
          If edPosArt.Text = edPosEsis.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775253920572000, "La posizione del campo articolo non deve essere uguale alla posizione del campo esistenza."))
            Return False
          End If
        End If
        If (ckFase.Checked) And (ckCom.Checked) Then
          If edPosFase.Text = edPosCom.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037936313860462, "La posizione del campo fase non deve essere uguale alla posizione del campo commessa."))
            Return False
          End If
        End If
        If (ckFase.Checked) And (ckLotto.Checked) Then
          If edPosFase.Text = edPosLotto.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254565320000, "La posizione del campo fase non deve essere uguale alla posizione del campo lotto."))
            Return False
          End If
        End If
        If (ckFase.Checked) And (ckUbic.Checked) Then
          If edPosFase.Text = edPosUbic.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254545508000, "La posizione del campo fase non deve essere uguale alla posizione del campo ubicazione."))
            Return False
          End If
        End If
        If (ckFase.Checked) And (ckMatr.Checked) Then
          If edPosFase.Text = edPosMatr.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254533340000, "La posizione del campo fase non deve essere uguale alla posizione del campo matricola."))
            Return False
          End If
        End If
        If (ckFase.Checked) And (ckEsis.Checked) Then
          If edPosFase.Text = edPosEsis.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254518052000, "La posizione del campo fase non deve essere uguale alla posizione del campo esistenza."))
            Return False
          End If
        End If
        If (ckLotto.Checked) And (ckCom.Checked) Then
          If edPosCom.Text = edPosLotto.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037937372922962, "La posizione del campo commessa non deve essere uguale alla posizione del campo lotto."))
            Return False
          End If
        End If
        If (ckUbic.Checked) And (ckCom.Checked) Then
          If edPosCom.Text = edPosUbic.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037937944641712, "La posizione del campo commessa non deve essere uguale alla posizione del campo ubicazione."))
            Return False
          End If
        End If
        If (ckMatr.Checked) And (ckCom.Checked) Then
          If edPosCom.Text = edPosMatr.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037938254641712, "La posizione del campo commessa non deve essere uguale alla posizione del campo matricola."))
            Return False
          End If
        End If
        If (ckEsis.Checked) And (ckCom.Checked) Then
          If edPosCom.Text = edPosEsis.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037938587766712, "La posizione del campo commessa non deve essere uguale alla posizione del campo esistenza."))
            Return False
          End If
        End If
        If (ckUbic.Checked) And (ckLotto.Checked) Then
          If edPosLotto.Text = edPosUbic.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254887304000, "La posizione del campo lotto non deve essere uguale alla posizione del campo ubicazione."))
            Return False
          End If
        End If
        If (ckMatr.Checked) And (ckLotto.Checked) Then
          If edPosLotto.Text = edPosMatr.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254871080000, "La posizione del campo lotto non deve essere uguale alla posizione del campo matricola."))
            Return False
          End If
        End If
        If (ckEsis.Checked) And (ckLotto.Checked) Then
          If edPosLotto.Text = edPosEsis.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254857352000, "La posizione del campo lotto non deve essere uguale alla posizione del campo esistenza."))
            Return False
          End If
        End If
        If (ckMatr.Checked) And (ckUbic.Checked) Then
          If edPosUbic.Text = edPosMatr.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775255545070000, "La posizione del campo ubicazione non deve essere uguale alla posizione del campo matricola."))
            Return False
          End If
        End If
        If (ckEsis.Checked) And (ckUbic.Checked) Then
          If edPosUbic.Text = edPosEsis.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775255530250000, "La posizione del campo ubicazione non deve essere uguale alla posizione del campo esistenza."))
            Return False
          End If
        End If
      Else
        'Controlla validità numeri
        If NTSCInt(edArtDa.Text) > NTSCInt(edArtA.Text) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128571247651250000, "La posizione iniziale del campo articolo non deve superare quella finale."))
          Return False
        End If
        If ckFase.Checked Then
          If NTSCInt(edFaseDa.Text) > NTSCInt(edFaseA.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128571247881875000, "La posizione iniziale del campo fase non deve superare quella finale."))
            Return False
          End If
        End If
        If ckCom.Checked Then
          If NTSCInt(edComDa.Text) > NTSCInt(edComA.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037940556204212, "La posizione iniziale del campo commessa non deve superare quella finale."))
            Return False
          End If
        End If
        If ckLotto.Checked Then
          If NTSCInt(edLottoDa.Text) > NTSCInt(edLottoA.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128571248041718750, "La posizione iniziale del campo lotto non deve superare quella finale."))
            Return False
          End If
        End If
        If ckUbic.Checked Then
          If NTSCInt(edUbicDa.Text) > NTSCInt(edUbicA.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128571248171093750, "La posizione iniziale del campo ubicazione non deve superare quella finale."))
            Return False
          End If
        End If
        If ckMatr.Checked Then
          If NTSCInt(edMatrDa.Text) > NTSCInt(edMatrA.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256369062000, "La posizione iniziale del campo matricola non deve superare quella finale."))
            Return False
          End If
        End If
        If ckEsis.Checked Then
          If NTSCInt(edEsisDa.Text) > NTSCInt(edEsisA.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256352994000, "La posizione iniziale del campo esistenza non deve superare quella finale."))
            Return False
          End If
        End If
        '------------------------------------------------------------------------------------------------------------
        '--- Controllo congruenza posizioni
        '------------------------------------------------------------------------------------------------------------
        If ckFase.Checked Then
          If (NTSCInt(edFaseDa.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edFaseDa.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edFaseA.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edFaseA.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edArtDa.Text) >= NTSCInt(edFaseDa.Text) And NTSCInt(edArtDa.Text) <= NTSCInt(edFaseA.Text)) Or (NTSCInt(edArtA.Text) >= NTSCInt(edFaseDa.Text) And NTSCInt(edArtA.Text) <= NTSCInt(edFaseA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256806486000, "La posizione del campo fase non deve essere inclusa nella posizione del campo articolo e viceversa."))
            Return False
          End If
        End If
        If ckCom.Checked Then
          If (NTSCInt(edComDa.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edComDa.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edComA.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edComA.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edArtDa.Text) >= NTSCInt(edComDa.Text) And NTSCInt(edArtDa.Text) <= NTSCInt(edComA.Text)) Or (NTSCInt(edArtA.Text) >= NTSCInt(edComDa.Text) And NTSCInt(edArtA.Text) <= NTSCInt(edComA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037942606086852, "La posizione del campo commessa non deve essere inclusa nella posizione del campo articolo e viceversa."))
            Return False
          End If
        End If
        If ckLotto.Checked Then
          If (NTSCInt(edLottoDa.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edLottoDa.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edLottoA.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edLottoA.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edArtDa.Text) >= NTSCInt(edLottoDa.Text) And NTSCInt(edArtDa.Text) <= NTSCInt(edLottoA.Text)) Or (NTSCInt(edArtA.Text) >= NTSCInt(edLottoDa.Text) And NTSCInt(edArtA.Text) <= NTSCInt(edLottoA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256779498000, "La posizione del campo lotto non deve essere inclusa nella posizione del campo articolo e viceversa."))
            Return False
          End If
        End If
        If ckUbic.Checked Then
          If (NTSCInt(edUbicDa.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edUbicDa.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edUbicA.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edUbicA.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edArtDa.Text) >= NTSCInt(edUbicDa.Text) And NTSCInt(edArtDa.Text) <= NTSCInt(edUbicA.Text)) Or (NTSCInt(edArtA.Text) >= NTSCInt(edUbicDa.Text) And NTSCInt(edArtA.Text) <= NTSCInt(edUbicA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256761714000, "La posizione del campo ubicazione non deve essere inclusa nella posizione del campo articolo e viceversa."))
            Return False
          End If
        End If
        If ckMatr.Checked Then
          If (NTSCInt(edMatrDa.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edMatrDa.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edMatrA.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edMatrA.Text) <= NTSCInt(edArtA)) Or (NTSCInt(edArtDa.Text) >= NTSCInt(edMatrDa) And NTSCInt(edArtDa.Text) <= NTSCInt(edMatrA)) Or (NTSCInt(edArtA.Text) >= NTSCInt(edMatrDa) And NTSCInt(edArtA.Text) <= NTSCInt(edMatrA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256748766000, "La posizione del campo matricola non deve essere inclusa nella posizione del campo articolo e viceversa."))
            Return False
          End If
        End If
        If ckEsis.Checked Then
          If (NTSCInt(edEsisDa.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edEsisDa.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edEsisA.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edEsisA.Text) <= NTSCInt(edArtA)) Or (NTSCInt(edArtDa.Text) >= NTSCInt(edEsisDa) And NTSCInt(edArtDa.Text) <= NTSCInt(edEsisA)) Or (NTSCInt(edArtA.Text) >= NTSCInt(edEsisDa) And NTSCInt(edArtA.Text) <= NTSCInt(edEsisA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256730514000, "La posizione del campo esistenza non deve essere inclusa nella posizione del campo articolo e viceversa."))
            Return False
          End If
        End If
        If (ckFase.Checked) And (ckCom.Checked) Then
          If (NTSCInt(edComDa.Text) >= NTSCInt(edFaseDa.Text) And NTSCInt(edComDa.Text) <= NTSCInt(edFaseA.Text)) Or (NTSCInt(edComA.Text) >= NTSCInt(edFaseDa.Text) And NTSCInt(edComA.Text) <= NTSCInt(edFaseA.Text)) Or (NTSCInt(edFaseDa.Text) >= NTSCInt(edComDa.Text) And NTSCInt(edFaseDa.Text) <= NTSCInt(edComA.Text)) Or (NTSCInt(edFaseA.Text) >= NTSCInt(edComDa.Text) And NTSCInt(edFaseA.Text) <= NTSCInt(edComA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037943977336852, "La posizione del campo fase non deve essere inclusa nella posizione del campo commessa e viceversa."))
            Return False
          End If
        End If
        If (ckFase.Checked) And (ckLotto.Checked) Then
          If (NTSCInt(edLottoDa.Text) >= NTSCInt(edFaseDa.Text) And NTSCInt(edLottoDa.Text) <= NTSCInt(edFaseA.Text)) Or (NTSCInt(edLottoA.Text) >= NTSCInt(edFaseDa.Text) And NTSCInt(edLottoA.Text) <= NTSCInt(edFaseA.Text)) Or (NTSCInt(edFaseDa.Text) >= NTSCInt(edLottoDa.Text) And NTSCInt(edFaseDa.Text) <= NTSCInt(edLottoA.Text)) Or (NTSCInt(edFaseA.Text) >= NTSCInt(edLottoDa.Text) And NTSCInt(edFaseA.Text) <= NTSCInt(edLottoA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128571248820156250, "La posizione del campo fase non deve essere inclusa nella posizione del campo lotto e viceversa."))
            Return False
          End If
        End If
        If (ckFase.Checked) And (ckUbic.Checked) Then
          If (NTSCInt(edUbicDa.Text) >= NTSCInt(edFaseDa.Text) And NTSCInt(edUbicDa.Text) <= NTSCInt(edFaseA.Text)) Or (NTSCInt(edUbicA.Text) >= NTSCInt(edFaseDa.Text) And NTSCInt(edUbicA.Text) <= NTSCInt(edFaseA.Text)) Or (NTSCInt(edFaseDa.Text) >= NTSCInt(edUbicDa.Text) And NTSCInt(edFaseDa.Text) <= NTSCInt(edUbicA.Text)) Or (NTSCInt(edFaseA.Text) >= NTSCInt(edUbicDa.Text) And NTSCInt(edFaseA.Text) <= NTSCInt(edUbicA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128571249002031250, "La posizione del campo fase non deve essere inclusa nella posizione del campo ubicazione e viceversa."))
            Return False
          End If
        End If
        If (ckFase.Checked) And (ckMatr.Checked) Then
          If (NTSCInt(edMatrDa.Text) >= NTSCInt(edFaseDa) And NTSCInt(edMatrDa.Text) <= NTSCInt(edFaseA)) Or (NTSCInt(edMatrA.Text) >= NTSCInt(edFaseDa) And NTSCInt(edMatrA.Text) <= NTSCInt(edFaseA)) Or (NTSCInt(edFaseDa.Text) >= NTSCInt(edMatrDa) And NTSCInt(edFaseDa.Text) <= NTSCInt(edMatrA)) Or (NTSCInt(edFaseA.Text) >= NTSCInt(edMatrDa) And NTSCInt(edFaseA.Text) <= NTSCInt(edMatrA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260075292000, "La posizione del campo fase non deve essere inclusa nella posizione del campo matricola e viceversa."))
            Return False
          End If
        End If
        If (ckFase.Checked) And (ckEsis.Checked) Then
          If (NTSCInt(edEsisDa.Text) >= NTSCInt(edFaseDa) And NTSCInt(edEsisDa.Text) <= NTSCInt(edFaseA)) Or (NTSCInt(edEsisA.Text) >= NTSCInt(edFaseDa) And NTSCInt(edEsisA.Text) <= NTSCInt(edFaseA)) Or (NTSCInt(edFaseDa.Text) >= NTSCInt(edEsisDa) And NTSCInt(edFaseDa.Text) <= NTSCInt(edEsisA)) Or (NTSCInt(edFaseA.Text) >= NTSCInt(edEsisDa) And NTSCInt(edFaseA.Text) <= NTSCInt(edEsisA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260085900000, "La posizione del campo fase non deve essere inclusa nella posizione del campo esistenza e viceversa."))
            Return False
          End If
        End If
        If (ckCom.Checked) And (ckLotto.Checked) Then
          If (NTSCInt(edLottoDa.Text) >= NTSCInt(edComDa.Text) And NTSCInt(edLottoDa.Text) <= NTSCInt(edComA.Text)) Or (NTSCInt(edLottoA.Text) >= NTSCInt(edComDa.Text) And NTSCInt(edLottoA.Text) <= NTSCInt(edComA.Text)) Or (NTSCInt(edComDa.Text) >= NTSCInt(edLottoDa.Text) And NTSCInt(edComDa.Text) <= NTSCInt(edLottoA.Text)) Or (NTSCInt(edComA.Text) >= NTSCInt(edLottoDa.Text) And NTSCInt(edComA.Text) <= NTSCInt(edLottoA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037946799524352, "La posizione del campo commessa non deve essere inclusa nella posizione del campo lotto e viceversa."))
            Return False
          End If
        End If
        If (ckCom.Checked) And (ckUbic.Checked) Then
          If (NTSCInt(edUbicDa.Text) >= NTSCInt(edComDa.Text) And NTSCInt(edUbicDa.Text) <= NTSCInt(edComA.Text)) Or (NTSCInt(edUbicA.Text) >= NTSCInt(edComDa.Text) And NTSCInt(edUbicA.Text) <= NTSCInt(edComA.Text)) Or (NTSCInt(edComDa.Text) >= NTSCInt(edUbicDa.Text) And NTSCInt(edComDa.Text) <= NTSCInt(edUbicA.Text)) Or (NTSCInt(edComA.Text) >= NTSCInt(edUbicDa.Text) And NTSCInt(edComA.Text) <= NTSCInt(edUbicA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037947459993102, "La posizione del campo commessa non deve essere inclusa nella posizione del campo ubicazione e viceversa."))
            Return False
          End If
        End If
        If (ckCom.Checked) And (ckMatr.Checked) Then
          If (NTSCInt(edMatrDa.Text) >= NTSCInt(edComDa) And NTSCInt(edMatrDa.Text) <= NTSCInt(edComA)) Or (NTSCInt(edMatrA.Text) >= NTSCInt(edComDa) And NTSCInt(edMatrA.Text) <= NTSCInt(edComA)) Or (NTSCInt(edComDa.Text) >= NTSCInt(edMatrDa) And NTSCInt(edComDa.Text) <= NTSCInt(edMatrA)) Or (NTSCInt(edComA.Text) >= NTSCInt(edMatrDa) And NTSCInt(edComA.Text) <= NTSCInt(edMatrA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037948317961852, "La posizione del campo commessa non deve essere inclusa nella posizione del campo matricola e viceversa."))
            Return False
          End If
        End If
        If (ckCom.Checked) And (ckEsis.Checked) Then
          If (NTSCInt(edEsisDa.Text) >= NTSCInt(edComDa) And NTSCInt(edEsisDa.Text) <= NTSCInt(edComA)) Or (NTSCInt(edEsisA.Text) >= NTSCInt(edComDa) And NTSCInt(edEsisA.Text) <= NTSCInt(edComA)) Or (NTSCInt(edComDa.Text) >= NTSCInt(edEsisDa) And NTSCInt(edComDa.Text) <= NTSCInt(edEsisA)) Or (NTSCInt(edComA.Text) >= NTSCInt(edEsisDa) And NTSCInt(edComA.Text) <= NTSCInt(edEsisA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129038023223806992, "La posizione del campo commessa non deve essere inclusa nella posizione del campo esistenza e viceversa."))
            Return False
          End If
        End If
        If (ckUbic.Checked) And (ckLotto.Checked) Then
          If (NTSCInt(edLottoDa.Text) >= NTSCInt(edUbicDa.Text) And NTSCInt(edLottoDa.Text) <= NTSCInt(edUbicA.Text)) Or (NTSCInt(edLottoA.Text) >= NTSCInt(edUbicDa.Text) And NTSCInt(edLottoA.Text) <= NTSCInt(edUbicA.Text)) Or (NTSCInt(edUbicDa.Text) >= NTSCInt(edLottoDa.Text) And NTSCInt(edUbicDa.Text) <= NTSCInt(edLottoA.Text)) Or (NTSCInt(edUbicA.Text) >= NTSCInt(edLottoDa.Text) And NTSCInt(edUbicA.Text) <= NTSCInt(edLottoA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128571249138750000, "La posizione del campo lotto non deve essere inclusa nella posizione del campo ubicazione e viceversa."))
            Return False
          End If
        End If
        If (ckMatr.Checked) And (ckLotto.Checked) Then
          If (NTSCInt(edLottoDa.Text) >= NTSCInt(edMatrDa) And NTSCInt(edLottoDa.Text) <= NTSCInt(edMatrA)) Or (NTSCInt(edLottoA.Text) >= NTSCInt(edMatrDa) And NTSCInt(edLottoA.Text) <= NTSCInt(edMatrA)) Or (NTSCInt(edMatrDa.Text) >= NTSCInt(edLottoDa) And NTSCInt(edMatrDa.Text) <= NTSCInt(edLottoA)) Or (NTSCInt(edMatrA.Text) >= NTSCInt(edLottoDa) And NTSCInt(edMatrA.Text) <= NTSCInt(edLottoA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260138628000, "La posizione del campo lotto non deve essere inclusa nella posizione del campo matricola e viceversa."))
            Return False
          End If
        End If
        If (ckEsis.Checked) And (ckLotto.Checked) Then
          If (NTSCInt(edLottoDa.Text) >= NTSCInt(edEsisDa) And NTSCInt(edLottoDa.Text) <= NTSCInt(edEsisA)) Or (NTSCInt(edLottoA.Text) >= NTSCInt(edEsisDa) And NTSCInt(edLottoA.Text) <= NTSCInt(edEsisA)) Or (NTSCInt(edEsisDa.Text) >= NTSCInt(edLottoDa) And NTSCInt(edEsisDa.Text) <= NTSCInt(edLottoA)) Or (NTSCInt(edEsisA.Text) >= NTSCInt(edLottoDa) And NTSCInt(edEsisA.Text) <= NTSCInt(edLottoA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260149392000, "La posizione del campo lotto non deve essere inclusa nella posizione del campo esistenza e viceversa."))
            Return False
          End If
        End If
        If (ckUbic.Checked) And (ckMatr.Checked) Then
          If (NTSCInt(edMatrDa.Text) >= NTSCInt(edUbicDa) And NTSCInt(edMatrDa.Text) <= NTSCInt(edUbicA)) Or (NTSCInt(edMatrA.Text) >= NTSCInt(edUbicDa) And NTSCInt(edMatrA.Text) <= NTSCInt(edUbicA)) Or (NTSCInt(edUbicDa.Text) >= NTSCInt(edMatrDa) And NTSCInt(edUbicDa.Text) <= NTSCInt(edMatrA)) Or (NTSCInt(edUbicA.Text) >= NTSCInt(edMatrDa) And NTSCInt(edUbicA.Text) <= NTSCInt(edMatrA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260192292000, "La posizione del campo matricola non deve essere inclusa nella posizione del campo ubicazione e viceversa."))
            Return False
          End If
        End If
        If (ckUbic.Checked) And (ckEsis.Checked) Then
          If (NTSCInt(edEsisDa.Text) >= NTSCInt(edUbicDa) And NTSCInt(edEsisDa.Text) <= NTSCInt(edUbicA)) Or (NTSCInt(edEsisA.Text) >= NTSCInt(edUbicDa) And NTSCInt(edEsisA.Text) <= NTSCInt(edUbicA)) Or (NTSCInt(edUbicDa.Text) >= NTSCInt(edEsisDa) And NTSCInt(edUbicDa.Text) <= NTSCInt(edEsisA)) Or (NTSCInt(edUbicA.Text) >= NTSCInt(edEsisDa) And NTSCInt(edUbicA.Text) <= NTSCInt(edEsisA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260201184000, "La posizione del campo esistenza non deve essere inclusa nella posizione del campo ubicazione e viceversa."))
            Return False
          End If
        End If
      End If    'If Not opFixed.Checked Then

      '--------------------------------------------------------------------------------------------------------------
      '--- Ora è il momento di salvare le informazioni nel registro
      '--- nessuna dipendenza operatore/azienda/tipodocumento
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.WaitCursor
      oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermExecute", IIf(ckExecute.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      If ckExecute.Checked Then
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermCommand", lbCommand.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermSilent", IIf(ckSilent.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      End If
      oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermFileName", edNomeFile.Text, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermDeleteAfter", IIf(ckDelFile.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermIgnoreFirstRow", IIf(ckIgnore1rk.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermShowLogErr", IIf(ckShowLog.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermFileType", IIf(opFixed.Checked, "F", "S").ToString, " ", "...", "...", "...")
      If opDelim.Checked Then
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermFieldSep", edSepField.Text, " ", "...", "...", "...")
      End If
      oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermDecimalSep", edDecSep.Text, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermTipoArt", IIf(opCodart.Checked, "C", IIf(opBarcode.Checked, "B", "A")).ToString, " ", "...", "...", "...")
      If opCodartBarcodeCodarfo.Checked Then
        'se ho selezionato sia articolo che 'articolo o barcode'
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermTipoArt", "X", " ", "...", "...", "...")
      End If
      oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermUbic", IIf(ckUbic.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermLotto", IIf(ckLotto.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermFase", IIf(ckFase.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermCom", IIf(ckCom.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermMatr", IIf(ckMatr.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermEsis", IIf(ckEsis.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      If Not opFixed.Checked Then
        'posizioni
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermCodartPos", edPosArt.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermFasePos", edPosFase.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermComPos", edPosCom.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermLottoPos", edPosLotto.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermUbicPos", edPosUbic.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermMatrPos", edPosMatr.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermEsisPos", edPosEsis.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermCodartStart", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermCodartStop", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermFaseStart", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermFaseStop", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermComStart", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermComStop", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermLottoStart", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermLottoStop", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermUbicStart", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermUbicStop", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermMatrStart", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermEsisStart", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermMatrStop", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermEsisStop", "0", " ", "...", "...", "...")
      Else
        'da, a 
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermCodartPos", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermFasePos", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermComPos", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermLottoPos", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermUbicPos", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermMatrPos", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermEsisPos", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermCodartStart", edArtDa.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermCodartStop", edArtA.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermFaseStart", edFaseDa.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermFaseStop", edFaseA.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermComStart", edComDa.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermComStop", edComA.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermLottoStart", edLottoDa.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermLottoStop", edLottoA.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermUbicStart", edUbicDa.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermUbicStop", edUbicA.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermMatrStart", edMatrDa.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermEsisStart", edEsisDa.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermMatrStop", edMatrA.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermEsisStop", edEsisA.Text, " ", "...", "...", "...")
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Scrive i divisori
      '--------------------------------------------------------------------------------------------------------------
      If ckEsis.Checked Then
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermDivEsis", edDivEsis.Text, " ", "...", "...", "...")
      Else
        oMenu.SaveSettingBus(oCleLsar.strTermPrgOwner, oCleLsar.strTermKey, ".", "TermDivEsis", "1", " ", "...", "...", "...")
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Function

End Class
