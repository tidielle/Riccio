Imports NTSInformatica.CLN__STD
Imports System.Data

Public Class FRMVERECA
  Public oCallParams As CLE__CLDP
  Public oCleBoll As CLEVEBOLL

  Public bRecaAnnullato As Boolean = True

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

  Public Overridable Sub InitControls()
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, False)
    '----------------------------------------------------------------------------------------------------------------
    Try
      '--------------------------------------------------------------------------------------------------------------
      edCartella.NTSSetParam(oMenu, oApp.Tr(Me, 130156792204445788, "Cartella file dati"), 0, True)
      cbPorta.NTSSetParam(oApp.Tr(Me, 130156792455148774, "Porta di comunicazione"))
      edMaxCaratteriDescrArt.NTSSetParam(oMenu, oApp.Tr(Me, 130196634234162598, "N° max caratteri decrizione articolo in file"), "0", 2, 10, 40)
      edScriptTimeOut.NTSSetParam(oMenu, oApp.Tr(Me, 130196636182882940, "TimeOut esecuzione script (millisecondi)"), "0", 14, -2, 99999999999999)
      ckEsponiQtaPrezzo.NTSSetParam(oMenu, oApp.Tr(Me, 130196636884404018, "Aggiungi riga decrittiva in scontrino"), "S", "N")
      ckEsponiValoreNetto.NTSSetParam(oMenu, oApp.Tr(Me, 130196637142851442, "Esponi valore netto su ogni riga del file scontrino"), "S", "N")
      ckEsponiScontiAValore.NTSSetParam(oMenu, oApp.Tr(Me, 130196637656474680, "Esponi sconti, di testata/riga, a valore"), "S", "N")
      '--------------------------------------------------------------------------------------------------------------
      Dim dttPorta As New DataTable()
      dttPorta.Columns.Add("cod", GetType(String))
      dttPorta.Columns.Add("val", GetType(String))
      dttPorta.Rows.Add(New Object() {"SR_START.001", "COM1"})
      dttPorta.Rows.Add(New Object() {"SR_START.002", "COM2"})
      dttPorta.Rows.Add(New Object() {"SR_START.003", "COM3"})
      dttPorta.Rows.Add(New Object() {"SR_START.004", "COM4"})
      dttPorta.Rows.Add(New Object() {"SR_START.005", "COM5"})
      dttPorta.Rows.Add(New Object() {"SR_START.006", "COM6"})
      dttPorta.Rows.Add(New Object() {"SR_START.007", "COM7"})
      dttPorta.Rows.Add(New Object() {"SR_START.008", "COM8"})
      dttPorta.Rows.Add(New Object() {"SR_START.009", "COM9"})
      cbPorta.DataSource = dttPorta
      cbPorta.ValueMember = "cod"
      cbPorta.DisplayMember = "val"
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

  Public Overridable Sub InitializeComponent()
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.lbInfo = New NTSInformatica.NTSLabel
    Me.pnControlli = New NTSInformatica.NTSPanel
    Me.lbMaxCaratteriDescrArtDescr = New NTSInformatica.NTSLabel
    Me.lbScriptTimeOutDescr = New NTSInformatica.NTSLabel
    Me.edScriptTimeOut = New NTSInformatica.NTSTextBoxNum
    Me.lbScriptTimeOut = New NTSInformatica.NTSLabel
    Me.edMaxCaratteriDescrArt = New NTSInformatica.NTSTextBoxNum
    Me.lbMaxCaratteriDescrArt = New NTSInformatica.NTSLabel
    Me.fmOpzioni = New NTSInformatica.NTSGroupBox
    Me.ckEsponiScontiAValore = New NTSInformatica.NTSCheckBox
    Me.ckEsponiValoreNetto = New NTSInformatica.NTSCheckBox
    Me.ckEsponiQtaPrezzo = New NTSInformatica.NTSCheckBox
    Me.edCartella = New NTSInformatica.NTSTextBoxStr
    Me.cbPorta = New NTSInformatica.NTSComboBox
    Me.lbPorta = New NTSInformatica.NTSLabel
    Me.cmdCartella = New NTSInformatica.NTSButton
    Me.lbCartella = New NTSInformatica.NTSLabel
    Me.FolderBrowserDialog = New NTSFolderBrowserDialog
    CType(Me.pnControlli, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnControlli.SuspendLayout()
    CType(Me.edScriptTimeOut.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edMaxCaratteriDescrArt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmOpzioni, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmOpzioni.SuspendLayout()
    CType(Me.ckEsponiScontiAValore.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckEsponiValoreNetto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckEsponiQtaPrezzo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCartella.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPorta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.cmdAnnulla.Location = New System.Drawing.Point(610, 145)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(119, 23)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(484, 145)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(119, 23)
    Me.cmdOk.TabIndex = 2
    Me.cmdOk.Text = "&Conferma"
    '
    'lbInfo
    '
    Me.lbInfo.BackColor = System.Drawing.Color.Transparent
    Me.lbInfo.Dock = System.Windows.Forms.DockStyle.Top
    Me.lbInfo.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
    Me.lbInfo.Location = New System.Drawing.Point(0, 0)
    Me.lbInfo.Name = "lbInfo"
    Me.lbInfo.NTSDbField = ""
    Me.lbInfo.Size = New System.Drawing.Size(741, 32)
    Me.lbInfo.TabIndex = 6
    Me.lbInfo.Text = "Per utilizzare il registratore di Cassa è necessario aver acquistato e installato" & _
        " --> WPOS1"
    Me.lbInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    Me.lbInfo.Tooltip = ""
    Me.lbInfo.UseMnemonic = False
    '
    'pnControlli
    '
    Me.pnControlli.AllowDrop = True
    Me.pnControlli.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnControlli.Appearance.Options.UseBackColor = True
    Me.pnControlli.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnControlli.Controls.Add(Me.cmdOk)
    Me.pnControlli.Controls.Add(Me.cmdAnnulla)
    Me.pnControlli.Controls.Add(Me.lbMaxCaratteriDescrArtDescr)
    Me.pnControlli.Controls.Add(Me.lbScriptTimeOutDescr)
    Me.pnControlli.Controls.Add(Me.edScriptTimeOut)
    Me.pnControlli.Controls.Add(Me.lbScriptTimeOut)
    Me.pnControlli.Controls.Add(Me.edMaxCaratteriDescrArt)
    Me.pnControlli.Controls.Add(Me.lbMaxCaratteriDescrArt)
    Me.pnControlli.Controls.Add(Me.fmOpzioni)
    Me.pnControlli.Controls.Add(Me.edCartella)
    Me.pnControlli.Controls.Add(Me.cbPorta)
    Me.pnControlli.Controls.Add(Me.lbPorta)
    Me.pnControlli.Controls.Add(Me.cmdCartella)
    Me.pnControlli.Controls.Add(Me.lbCartella)
    Me.pnControlli.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnControlli.Location = New System.Drawing.Point(0, 32)
    Me.pnControlli.Name = "pnControlli"
    Me.pnControlli.NTSActiveTrasparency = True
    Me.pnControlli.Size = New System.Drawing.Size(741, 214)
    Me.pnControlli.TabIndex = 11
    Me.pnControlli.Text = "NtsPanel1"
    '
    'lbMaxCaratteriDescrArtDescr
    '
    Me.lbMaxCaratteriDescrArtDescr.AutoSize = True
    Me.lbMaxCaratteriDescrArtDescr.BackColor = System.Drawing.Color.Transparent
    Me.lbMaxCaratteriDescrArtDescr.Location = New System.Drawing.Point(313, 50)
    Me.lbMaxCaratteriDescrArtDescr.Name = "lbMaxCaratteriDescrArtDescr"
    Me.lbMaxCaratteriDescrArtDescr.NTSDbField = ""
    Me.lbMaxCaratteriDescrArtDescr.Size = New System.Drawing.Size(144, 13)
    Me.lbMaxCaratteriDescrArtDescr.TabIndex = 122
    Me.lbMaxCaratteriDescrArtDescr.Text = "(valori ammessi: fra 10 e 40)"
    Me.lbMaxCaratteriDescrArtDescr.Tooltip = ""
    Me.lbMaxCaratteriDescrArtDescr.UseMnemonic = False
    '
    'lbScriptTimeOutDescr
    '
    Me.lbScriptTimeOutDescr.AutoSize = True
    Me.lbScriptTimeOutDescr.BackColor = System.Drawing.Color.Transparent
    Me.lbScriptTimeOutDescr.Location = New System.Drawing.Point(313, 72)
    Me.lbScriptTimeOutDescr.Name = "lbScriptTimeOutDescr"
    Me.lbScriptTimeOutDescr.NTSDbField = ""
    Me.lbScriptTimeOutDescr.Size = New System.Drawing.Size(224, 13)
    Me.lbScriptTimeOutDescr.TabIndex = 121
    Me.lbScriptTimeOutDescr.Text = "(-2 --> non impostato, usa quello del motore)"
    Me.lbScriptTimeOutDescr.Tooltip = ""
    Me.lbScriptTimeOutDescr.UseMnemonic = False
    '
    'edScriptTimeOut
    '
    Me.edScriptTimeOut.Cursor = System.Windows.Forms.Cursors.Default
    Me.edScriptTimeOut.Location = New System.Drawing.Point(218, 69)
    Me.edScriptTimeOut.Name = "edScriptTimeOut"
    Me.edScriptTimeOut.NTSDbField = ""
    Me.edScriptTimeOut.NTSFormat = "0"
    Me.edScriptTimeOut.NTSForzaVisZoom = False
    Me.edScriptTimeOut.NTSOldValue = ""
    Me.edScriptTimeOut.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edScriptTimeOut.Properties.Appearance.Options.UseBackColor = True
    Me.edScriptTimeOut.Properties.Appearance.Options.UseTextOptions = True
    Me.edScriptTimeOut.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edScriptTimeOut.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edScriptTimeOut.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edScriptTimeOut.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edScriptTimeOut.Properties.MaxLength = 65536
    Me.edScriptTimeOut.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edScriptTimeOut.Size = New System.Drawing.Size(80, 20)
    Me.edScriptTimeOut.TabIndex = 120
    '
    'lbScriptTimeOut
    '
    Me.lbScriptTimeOut.AutoSize = True
    Me.lbScriptTimeOut.BackColor = System.Drawing.Color.Transparent
    Me.lbScriptTimeOut.Location = New System.Drawing.Point(9, 72)
    Me.lbScriptTimeOut.Name = "lbScriptTimeOut"
    Me.lbScriptTimeOut.NTSDbField = ""
    Me.lbScriptTimeOut.Size = New System.Drawing.Size(195, 13)
    Me.lbScriptTimeOut.TabIndex = 119
    Me.lbScriptTimeOut.Text = "TimeOut esecuzione script (millisecondi)"
    Me.lbScriptTimeOut.Tooltip = ""
    Me.lbScriptTimeOut.UseMnemonic = False
    '
    'edMaxCaratteriDescrArt
    '
    Me.edMaxCaratteriDescrArt.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMaxCaratteriDescrArt.Location = New System.Drawing.Point(218, 47)
    Me.edMaxCaratteriDescrArt.Name = "edMaxCaratteriDescrArt"
    Me.edMaxCaratteriDescrArt.NTSDbField = ""
    Me.edMaxCaratteriDescrArt.NTSFormat = "0"
    Me.edMaxCaratteriDescrArt.NTSForzaVisZoom = False
    Me.edMaxCaratteriDescrArt.NTSOldValue = ""
    Me.edMaxCaratteriDescrArt.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edMaxCaratteriDescrArt.Properties.Appearance.Options.UseBackColor = True
    Me.edMaxCaratteriDescrArt.Properties.Appearance.Options.UseTextOptions = True
    Me.edMaxCaratteriDescrArt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMaxCaratteriDescrArt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMaxCaratteriDescrArt.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMaxCaratteriDescrArt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMaxCaratteriDescrArt.Properties.MaxLength = 65536
    Me.edMaxCaratteriDescrArt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMaxCaratteriDescrArt.Size = New System.Drawing.Size(53, 20)
    Me.edMaxCaratteriDescrArt.TabIndex = 115
    '
    'lbMaxCaratteriDescrArt
    '
    Me.lbMaxCaratteriDescrArt.AutoSize = True
    Me.lbMaxCaratteriDescrArt.BackColor = System.Drawing.Color.Transparent
    Me.lbMaxCaratteriDescrArt.Location = New System.Drawing.Point(9, 50)
    Me.lbMaxCaratteriDescrArt.Name = "lbMaxCaratteriDescrArt"
    Me.lbMaxCaratteriDescrArt.NTSDbField = ""
    Me.lbMaxCaratteriDescrArt.Size = New System.Drawing.Size(203, 13)
    Me.lbMaxCaratteriDescrArt.TabIndex = 114
    Me.lbMaxCaratteriDescrArt.Text = "N° max caratteri decrizione articolo in file"
    Me.lbMaxCaratteriDescrArt.Tooltip = ""
    Me.lbMaxCaratteriDescrArt.UseMnemonic = False
    '
    'fmOpzioni
    '
    Me.fmOpzioni.AllowDrop = True
    Me.fmOpzioni.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmOpzioni.Appearance.Options.UseBackColor = True
    Me.fmOpzioni.Controls.Add(Me.ckEsponiScontiAValore)
    Me.fmOpzioni.Controls.Add(Me.ckEsponiValoreNetto)
    Me.fmOpzioni.Controls.Add(Me.ckEsponiQtaPrezzo)
    Me.fmOpzioni.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmOpzioni.Location = New System.Drawing.Point(12, 99)
    Me.fmOpzioni.Name = "fmOpzioni"
    Me.fmOpzioni.ShowCaption = False
    Me.fmOpzioni.Size = New System.Drawing.Size(304, 69)
    Me.fmOpzioni.TabIndex = 112
    '
    'ckEsponiScontiAValore
    '
    Me.ckEsponiScontiAValore.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckEsponiScontiAValore.Location = New System.Drawing.Point(15, 45)
    Me.ckEsponiScontiAValore.Name = "ckEsponiScontiAValore"
    Me.ckEsponiScontiAValore.NTSCheckValue = "S"
    Me.ckEsponiScontiAValore.NTSUnCheckValue = "N"
    Me.ckEsponiScontiAValore.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckEsponiScontiAValore.Properties.Appearance.Options.UseBackColor = True
    Me.ckEsponiScontiAValore.Properties.Caption = "Esponi sconti, di testata/riga, a valore"
    Me.ckEsponiScontiAValore.Size = New System.Drawing.Size(209, 19)
    Me.ckEsponiScontiAValore.TabIndex = 58
    '
    'ckEsponiValoreNetto
    '
    Me.ckEsponiValoreNetto.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckEsponiValoreNetto.Location = New System.Drawing.Point(15, 25)
    Me.ckEsponiValoreNetto.Name = "ckEsponiValoreNetto"
    Me.ckEsponiValoreNetto.NTSCheckValue = "S"
    Me.ckEsponiValoreNetto.NTSUnCheckValue = "N"
    Me.ckEsponiValoreNetto.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckEsponiValoreNetto.Properties.Appearance.Options.UseBackColor = True
    Me.ckEsponiValoreNetto.Properties.Caption = "Esponi valore netto su ogni riga del file scontrino"
    Me.ckEsponiValoreNetto.Size = New System.Drawing.Size(271, 19)
    Me.ckEsponiValoreNetto.TabIndex = 56
    '
    'ckEsponiQtaPrezzo
    '
    Me.ckEsponiQtaPrezzo.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckEsponiQtaPrezzo.Location = New System.Drawing.Point(15, 5)
    Me.ckEsponiQtaPrezzo.Name = "ckEsponiQtaPrezzo"
    Me.ckEsponiQtaPrezzo.NTSCheckValue = "S"
    Me.ckEsponiQtaPrezzo.NTSUnCheckValue = "N"
    Me.ckEsponiQtaPrezzo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckEsponiQtaPrezzo.Properties.Appearance.Options.UseBackColor = True
    Me.ckEsponiQtaPrezzo.Properties.Caption = "Aggiungi riga decrittiva in scontrino"
    Me.ckEsponiQtaPrezzo.Size = New System.Drawing.Size(194, 19)
    Me.ckEsponiQtaPrezzo.TabIndex = 55
    '
    'edCartella
    '
    Me.edCartella.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCartella.Location = New System.Drawing.Point(218, 3)
    Me.edCartella.Name = "edCartella"
    Me.edCartella.NTSDbField = ""
    Me.edCartella.NTSForzaVisZoom = False
    Me.edCartella.NTSOldValue = ""
    Me.edCartella.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCartella.Properties.Appearance.Options.UseBackColor = True
    Me.edCartella.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCartella.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCartella.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCartella.Properties.MaxLength = 65536
    Me.edCartella.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCartella.Size = New System.Drawing.Size(342, 20)
    Me.edCartella.TabIndex = 50
    '
    'cbPorta
    '
    Me.cbPorta.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPorta.DataSource = Nothing
    Me.cbPorta.DisplayMember = ""
    Me.cbPorta.Location = New System.Drawing.Point(218, 25)
    Me.cbPorta.Name = "cbPorta"
    Me.cbPorta.NTSDbField = ""
    Me.cbPorta.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbPorta.Properties.Appearance.Options.UseBackColor = True
    Me.cbPorta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPorta.Properties.DropDownRows = 30
    Me.cbPorta.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPorta.SelectedValue = ""
    Me.cbPorta.Size = New System.Drawing.Size(98, 20)
    Me.cbPorta.TabIndex = 10
    Me.cbPorta.ValueMember = ""
    '
    'lbPorta
    '
    Me.lbPorta.AutoSize = True
    Me.lbPorta.BackColor = System.Drawing.Color.Transparent
    Me.lbPorta.Location = New System.Drawing.Point(9, 28)
    Me.lbPorta.Name = "lbPorta"
    Me.lbPorta.NTSDbField = ""
    Me.lbPorta.Size = New System.Drawing.Size(116, 13)
    Me.lbPorta.TabIndex = 9
    Me.lbPorta.Text = "Porta di comunicazione"
    Me.lbPorta.Tooltip = ""
    Me.lbPorta.UseMnemonic = False
    '
    'cmdCartella
    '
    Me.cmdCartella.ImageText = ""
    Me.cmdCartella.Location = New System.Drawing.Point(566, 3)
    Me.cmdCartella.Name = "cmdCartella"
    Me.cmdCartella.Size = New System.Drawing.Size(23, 20)
    Me.cmdCartella.TabIndex = 8
    Me.cmdCartella.Text = "..."
    '
    'lbCartella
    '
    Me.lbCartella.AutoSize = True
    Me.lbCartella.BackColor = System.Drawing.Color.Transparent
    Me.lbCartella.Location = New System.Drawing.Point(9, 6)
    Me.lbCartella.Name = "lbCartella"
    Me.lbCartella.NTSDbField = ""
    Me.lbCartella.Size = New System.Drawing.Size(82, 13)
    Me.lbCartella.TabIndex = 6
    Me.lbCartella.Text = "Cartella file dati"
    Me.lbCartella.Tooltip = ""
    Me.lbCartella.UseMnemonic = False
    '
    'FRMVERECA
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(741, 212)
    Me.Controls.Add(Me.pnControlli)
    Me.Controls.Add(Me.lbInfo)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMVERECA"
    Me.Text = "NUOVO/APRI DOCUMENTO"
    CType(Me.pnControlli, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnControlli.ResumeLayout(False)
    Me.pnControlli.PerformLayout()
    CType(Me.edScriptTimeOut.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edMaxCaratteriDescrArt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmOpzioni, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmOpzioni.ResumeLayout(False)
    Me.fmOpzioni.PerformLayout()
    CType(Me.ckEsponiScontiAValore.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckEsponiValoreNetto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckEsponiQtaPrezzo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCartella.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPorta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRMVERECA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      bRecaAnnullato = True
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      strTmp = oMenu.GetSettingBusDitt(DittaCorrente, "BSVEBOLL", "OPZIONI", ".", "IEDirFile", "", " ", "").Trim
      If System.IO.Directory.Exists(strTmp) = False Then strTmp = ""
      If strTmp = "" Then
        strTmp = "C:\PROGRAMDATA\RTSWPOS1"
        If System.IO.Directory.Exists(strTmp) = True Then GoTo SettaCartella
        strTmp = "C:\PROGRAM FILES (x86)\RTS WPOS1"
        If System.IO.Directory.Exists(strTmp) = True Then GoTo SettaCartella
        strTmp = "C:\PROGRAM FILES\RTS WPOS1"
        If System.IO.Directory.Exists(strTmp) = True Then GoTo SettaCartella
        strTmp = "C:\PROGRAMMI\RTS WPOS1"
        If System.IO.Directory.Exists(strTmp) = True Then GoTo SettaCartella
        strTmp = "C:\PROGRAMS\RTS WPOS1"
        If System.IO.Directory.Exists(strTmp) = True Then GoTo SettaCartella
        strTmp = ""
      End If
SettaCartella:
      edCartella.Text = strTmp
      '--------------------------------------------------------------------------------------------------------------
      cbPorta.SelectedValue = oMenu.GetSettingBusDitt(DittaCorrente, "BSVEBOLL", "OPZIONI", ".", "IENomeFileSemaforoSarema", "SR_START.001", " ", "SR_START.001").Trim
      ckEsponiQtaPrezzo.Checked = CBool(oMenu.GetSettingBusDitt(DittaCorrente, "BSVEBOLL", "OPZIONI", ".", "IEEsponiQtaEPrezzoComeDescrizione", "0", " ", "0"))
      ckEsponiValoreNetto.Checked = CBool(oMenu.GetSettingBusDitt(DittaCorrente, "BSVEBOLL", "OPZIONI", ".", "IEEsponiValoreNetto", "0", " ", "0"))
      ckEsponiScontiAValore.Checked = CBool(oMenu.GetSettingBusDitt(DittaCorrente, "BSVEBOLL", "OPZIONI", ".", "IEScontiAValore", "0", " ", "0"))
      strTmp = oMenu.GetSettingBusDitt(DittaCorrente, "BSVEBOLL", "OPZIONI", ".", "IEMaxCaratteriDescrArt", "10", " ", "10")
      If Not IsNumeric(strTmp) Then
        strTmp = "10"
      Else
        If (NTSCInt(strTmp) < 10) Or (NTSCInt(strTmp) > 40) Then strTmp = "10"
      End If
      edMaxCaratteriDescrArt.Text = strTmp
      edScriptTimeOut.Text = oMenu.GetSettingBusDitt(DittaCorrente, "BSVEBOLL", "OPZIONI", ".", "IEScriptTimeOut", "-2", " ", "-2")
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      GctlApplicaDefaultValue()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMVERECA_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Try
      '--------------------------------------------------------------------------------------------------------------
      If (e.KeyCode = Keys.F5) And (e.Control = False) And (e.Alt = False) And (e.Shift = False) Then
        Zoom()
        e.Handled = True
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi Pulsanti"
  Public Overridable Sub cmdCartella_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCartella.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      With FolderBrowserDialog
        .Description = "Scegli cartella"
        .SelectedPath = IIf(edCartella.Text.Trim <> "", edCartella.Text.Trim, "C:\").ToString
        .oMenu = oMenu
        If .ShowDialog = Windows.Forms.DialogResult.OK Then edCartella.Text = FolderBrowserDialog.SelectedPath
      End With
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      If edCartella.Text.Trim = "" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130156804785114414, "Attenzione!" & vbCrLf & _
          "Indicare una cartella file dati esistente." & vbCrLf & _
          "Configurazione Registratore di Cassa annullata."))
        edCartella.Focus()
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 130156808191270596, _
        "Confermare le configurazioni selezionate per il Registratore di Cassa?")) = Windows.Forms.DialogResult.No Then Return
      '--------------------------------------------------------------------------------------------------------------
      oMenu.SaveSettingBus("BSVEBOLL", "OPZIONI", ".", "IEDirFile", edCartella.Text, " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("BSVEBOLL", "OPZIONI", ".", "IENomeFileSemaforoSarema", cbPorta.SelectedValue.ToString, " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("BSVEBOLL", "OPZIONI", ".", "IENomeFileSarema", "SR_DATI.00" & Microsoft.VisualBasic.Right(cbPorta.SelectedValue.ToString, 1), " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("BSVEBOLL", "OPZIONI", ".", "IEEsponiQtaEPrezzoComeDescrizione", IIf(ckEsponiQtaPrezzo.Checked = True, "-1", "0").ToString, " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("BSVEBOLL", "OPZIONI", ".", "IEEsponiValoreNetto", IIf(ckEsponiValoreNetto.Checked = True, "-1", "0").ToString, " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("BSVEBOLL", "OPZIONI", ".", "IEMaxCaratteriDescrArt", edMaxCaratteriDescrArt.Text, " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("BSVEBOLL", "OPZIONI", ".", "IEScontiAValore", IIf(ckEsponiScontiAValore.Checked = True, "-1", "0").ToString, " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("BSVEBOLL", "OPZIONI", ".", "IEScriptTimeOut", edScriptTimeOut.Text, " ", "NS.", "...", "...")
      '--------------------------------------------------------------------------------------------------------------
      bRecaAnnullato = False
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
      bRecaAnnullato = True
      '--------------------------------------------------------------------------------------------------------------
      Me.Close()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Sub Zoom()
    Try
      '--------------------------------------------------------------------------------------------------------------
      NTSCallStandardZoom()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class