Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__WZRD
  Public oCallParams As CLE__CLDP
  Public oCleGope As CLE__GOPE
  Private components As System.ComponentModel.IComponent

  Public bAnnullato As Boolean = True
  Public bCrmmod As Boolean = False
  Public bAmm As Boolean = False
  Public bCodcage As Boolean = False
  Public bSostituisce As Boolean = False

  Public nCodcage As Integer = 0
  Public nAltriAccessi As Integer = 0
  Public nOldAgente As Integer = 0

  Public strDescodcage As String = ""
  Public strCodruaz As String = ""
  Public strDescodruaz As String = ""
  Public strEmail As String = ""
  Public strCell As String = ""
  Public strOldOperatore As String = ""
  Public strDesOldAgente As String = ""

  Public strTmp As String = ""

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

  Private Sub InitializeComponent()
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdProcedi = New NTSInformatica.NTSButton
    Me.fmControlli = New NTSInformatica.NTSGroupBox
    Me.fmSostituisce = New NTSInformatica.NTSGroupBox
    Me.lbOldAgente = New NTSInformatica.NTSLabel
    Me.lbDesOldAgente = New NTSInformatica.NTSLabel
    Me.edOldAgente = New NTSInformatica.NTSTextBoxNum
    Me.edOldOperatore = New NTSInformatica.NTSTextBoxStr
    Me.lbOldOperatore = New NTSInformatica.NTSLabel
    Me.ckSostituisce = New NTSInformatica.NTSCheckBox
    Me.fmAltriAccessi = New NTSInformatica.NTSGroupBox
    Me.lbAltriAccessi = New NTSInformatica.NTSLabel
    Me.opAltriAccessi3 = New NTSInformatica.NTSRadioButton
    Me.opAltriAccessi2 = New NTSInformatica.NTSRadioButton
    Me.opAltriAccessi1 = New NTSInformatica.NTSRadioButton
    Me.fmOrganizzazioneDitta = New NTSInformatica.NTSGroupBox
    Me.lbCell = New NTSInformatica.NTSLabel
    Me.edCell = New NTSInformatica.NTSTextBoxStr
    Me.lbEmail = New NTSInformatica.NTSLabel
    Me.edEmail = New NTSInformatica.NTSTextBoxStr
    Me.lbDescodruaz = New NTSInformatica.NTSLabel
    Me.edCodruaz = New NTSInformatica.NTSTextBoxStr
    Me.lbCodruaz = New NTSInformatica.NTSLabel
    Me.fmAccessiDitta = New NTSInformatica.NTSGroupBox
    Me.lbDescodcage = New NTSInformatica.NTSLabel
    Me.edCodcage = New NTSInformatica.NTSTextBoxNum
    Me.ckCodcage = New NTSInformatica.NTSCheckBox
    Me.ckAmm = New NTSInformatica.NTSCheckBox
    Me.ckCrmmod = New NTSInformatica.NTSCheckBox
    Me.fmPulsanti = New NTSInformatica.NTSGroupBox
    CType(Me.fmControlli, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmControlli.SuspendLayout()
    CType(Me.fmSostituisce, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmSostituisce.SuspendLayout()
    CType(Me.edOldAgente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOldOperatore.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSostituisce.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmAltriAccessi, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmAltriAccessi.SuspendLayout()
    CType(Me.opAltriAccessi3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opAltriAccessi2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opAltriAccessi1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmOrganizzazioneDitta, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmOrganizzazioneDitta.SuspendLayout()
    CType(Me.edCell.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEmail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodruaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmAccessiDitta, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmAccessiDitta.SuspendLayout()
    CType(Me.edCodcage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckCodcage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAmm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckCrmmod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmPulsanti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmPulsanti.SuspendLayout()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'DevXDefaultLookAndFeel
    '
    
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Location = New System.Drawing.Point(3, 25)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(168, 23)
    Me.cmdAnnulla.TabIndex = 5
    Me.cmdAnnulla.Text = "Annulla"
    '
    'cmdProcedi
    '
    Me.cmdProcedi.Location = New System.Drawing.Point(3, 0)
    Me.cmdProcedi.Name = "cmdProcedi"
    Me.cmdProcedi.Size = New System.Drawing.Size(168, 23)
    Me.cmdProcedi.TabIndex = 4
    Me.cmdProcedi.Text = "Procedi"
    '
    'fmControlli
    '
    Me.fmControlli.AllowDrop = True
    Me.fmControlli.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmControlli.Appearance.Options.UseBackColor = True
    Me.fmControlli.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.fmControlli.Controls.Add(Me.fmSostituisce)
    Me.fmControlli.Controls.Add(Me.fmAltriAccessi)
    Me.fmControlli.Controls.Add(Me.fmOrganizzazioneDitta)
    Me.fmControlli.Controls.Add(Me.fmAccessiDitta)
    Me.fmControlli.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmControlli.Location = New System.Drawing.Point(2, 2)
    Me.fmControlli.Name = "fmControlli"
    Me.fmControlli.ShowCaption = False
    Me.fmControlli.Size = New System.Drawing.Size(672, 467)
    Me.fmControlli.TabIndex = 659
    '
    'fmSostituisce
    '
    Me.fmSostituisce.AllowDrop = True
    Me.fmSostituisce.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmSostituisce.Appearance.Options.UseBackColor = True
    Me.fmSostituisce.Controls.Add(Me.lbOldAgente)
    Me.fmSostituisce.Controls.Add(Me.lbDesOldAgente)
    Me.fmSostituisce.Controls.Add(Me.edOldAgente)
    Me.fmSostituisce.Controls.Add(Me.edOldOperatore)
    Me.fmSostituisce.Controls.Add(Me.lbOldOperatore)
    Me.fmSostituisce.Controls.Add(Me.ckSostituisce)
    Me.fmSostituisce.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmSostituisce.Location = New System.Drawing.Point(10, 356)
    Me.fmSostituisce.Name = "fmSostituisce"
    Me.fmSostituisce.Size = New System.Drawing.Size(653, 100)
    Me.fmSostituisce.TabIndex = 662
    Me.fmSostituisce.Text = "Sostituisce altro Utente/Agente"
    '
    'lbOldAgente
    '
    Me.lbOldAgente.AutoSize = True
    Me.lbOldAgente.BackColor = System.Drawing.Color.Transparent
    Me.lbOldAgente.Enabled = False
    Me.lbOldAgente.Location = New System.Drawing.Point(15, 71)
    Me.lbOldAgente.Name = "lbOldAgente"
    Me.lbOldAgente.NTSDbField = ""
    Me.lbOldAgente.Size = New System.Drawing.Size(81, 13)
    Me.lbOldAgente.TabIndex = 527
    Me.lbOldAgente.Text = "Vecchio Agente"
    Me.lbOldAgente.UseMnemonic = False
    '
    'lbDesOldAgente
    '
    Me.lbDesOldAgente.BackColor = System.Drawing.Color.Transparent
    Me.lbDesOldAgente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesOldAgente.Location = New System.Drawing.Point(188, 68)
    Me.lbDesOldAgente.Name = "lbDesOldAgente"
    Me.lbDesOldAgente.NTSDbField = ""
    Me.lbDesOldAgente.Size = New System.Drawing.Size(450, 20)
    Me.lbDesOldAgente.TabIndex = 526
    Me.lbDesOldAgente.UseMnemonic = False
    '
    'edOldAgente
    '
    Me.edOldAgente.Cursor = System.Windows.Forms.Cursors.Default
    Me.edOldAgente.EditValue = "0"
    Me.edOldAgente.Enabled = False
    Me.edOldAgente.Location = New System.Drawing.Point(119, 68)
    Me.edOldAgente.Name = "edOldAgente"
    Me.edOldAgente.NTSDbField = ""
    Me.edOldAgente.NTSFormat = "0"
    Me.edOldAgente.NTSForzaVisZoom = False
    Me.edOldAgente.NTSOldValue = ""
    Me.edOldAgente.Properties.Appearance.Options.UseTextOptions = True
    Me.edOldAgente.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edOldAgente.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOldAgente.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOldAgente.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOldAgente.Properties.MaxLength = 65536
    Me.edOldAgente.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOldAgente.Size = New System.Drawing.Size(64, 20)
    Me.edOldAgente.TabIndex = 525
    '
    'edOldOperatore
    '
    Me.edOldOperatore.Cursor = System.Windows.Forms.Cursors.Default
    Me.edOldOperatore.Enabled = False
    Me.edOldOperatore.Location = New System.Drawing.Point(119, 45)
    Me.edOldOperatore.Name = "edOldOperatore"
    Me.edOldOperatore.NTSDbField = ""
    Me.edOldOperatore.NTSForzaVisZoom = False
    Me.edOldOperatore.NTSOldValue = ""
    Me.edOldOperatore.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOldOperatore.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOldOperatore.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOldOperatore.Properties.MaxLength = 65536
    Me.edOldOperatore.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOldOperatore.Size = New System.Drawing.Size(236, 20)
    Me.edOldOperatore.TabIndex = 102
    '
    'lbOldOperatore
    '
    Me.lbOldOperatore.AutoSize = True
    Me.lbOldOperatore.BackColor = System.Drawing.Color.Transparent
    Me.lbOldOperatore.Enabled = False
    Me.lbOldOperatore.Location = New System.Drawing.Point(15, 48)
    Me.lbOldOperatore.Name = "lbOldOperatore"
    Me.lbOldOperatore.NTSDbField = ""
    Me.lbOldOperatore.Size = New System.Drawing.Size(96, 13)
    Me.lbOldOperatore.TabIndex = 101
    Me.lbOldOperatore.Text = "Vecchio Operatore"
    Me.lbOldOperatore.UseMnemonic = False
    '
    'ckSostituisce
    '
    Me.ckSostituisce.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSostituisce.Location = New System.Drawing.Point(18, 25)
    Me.ckSostituisce.Name = "ckSostituisce"
    Me.ckSostituisce.NTSCheckValue = "S"
    Me.ckSostituisce.NTSUnCheckValue = "N"
    Me.ckSostituisce.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSostituisce.Properties.Appearance.Options.UseBackColor = True
    Me.ckSostituisce.Properties.Caption = "Il nuovo Utente sostituisce, nell'Organizzazione, un precedente Operatore/Agente"
    Me.ckSostituisce.Size = New System.Drawing.Size(423, 18)
    Me.ckSostituisce.TabIndex = 24
    '
    'fmAltriAccessi
    '
    Me.fmAltriAccessi.AllowDrop = True
    Me.fmAltriAccessi.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmAltriAccessi.Appearance.Options.UseBackColor = True
    Me.fmAltriAccessi.Controls.Add(Me.lbAltriAccessi)
    Me.fmAltriAccessi.Controls.Add(Me.opAltriAccessi3)
    Me.fmAltriAccessi.Controls.Add(Me.opAltriAccessi2)
    Me.fmAltriAccessi.Controls.Add(Me.opAltriAccessi1)
    Me.fmAltriAccessi.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmAltriAccessi.Location = New System.Drawing.Point(10, 220)
    Me.fmAltriAccessi.Name = "fmAltriAccessi"
    Me.fmAltriAccessi.Size = New System.Drawing.Size(653, 130)
    Me.fmAltriAccessi.TabIndex = 661
    Me.fmAltriAccessi.Text = "Altri Accessi"
    '
    'lbAltriAccessi
    '
    Me.lbAltriAccessi.AutoSize = True
    Me.lbAltriAccessi.BackColor = System.Drawing.Color.Transparent
    Me.lbAltriAccessi.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbAltriAccessi.Location = New System.Drawing.Point(15, 95)
    Me.lbAltriAccessi.Name = "lbAltriAccessi"
    Me.lbAltriAccessi.NTSDbField = ""
    Me.lbAltriAccessi.Size = New System.Drawing.Size(623, 26)
    Me.lbAltriAccessi.TabIndex = 4
    Me.lbAltriAccessi.Text = "N.B.: se l'Utente ha accesso ai dati di alcuni altri Operatori CRM," & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "provvedere a" & _
        "d impostare tali accessi utilizzando l'apposito programma 'Gestione Accessi CRM " & _
        "per Operatore'"
    Me.lbAltriAccessi.UseMnemonic = False
    '
    'opAltriAccessi3
    '
    Me.opAltriAccessi3.Cursor = System.Windows.Forms.Cursors.Default
    Me.opAltriAccessi3.Location = New System.Drawing.Point(18, 65)
    Me.opAltriAccessi3.Name = "opAltriAccessi3"
    Me.opAltriAccessi3.NTSCheckValue = "S"
    Me.opAltriAccessi3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opAltriAccessi3.Properties.Appearance.Options.UseBackColor = True
    Me.opAltriAccessi3.Properties.Caption = "L'Operatore ha accesso ai suoi dati, in lettura/scrittura ed ai dati di tutti gli" & _
        " Operatori CRM in lettura e scrittura"
    Me.opAltriAccessi3.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opAltriAccessi3.Size = New System.Drawing.Size(551, 18)
    Me.opAltriAccessi3.TabIndex = 3
    '
    'opAltriAccessi2
    '
    Me.opAltriAccessi2.Cursor = System.Windows.Forms.Cursors.Default
    Me.opAltriAccessi2.Location = New System.Drawing.Point(18, 45)
    Me.opAltriAccessi2.Name = "opAltriAccessi2"
    Me.opAltriAccessi2.NTSCheckValue = "S"
    Me.opAltriAccessi2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opAltriAccessi2.Properties.Appearance.Options.UseBackColor = True
    Me.opAltriAccessi2.Properties.Caption = "L'Operatore ha accesso ai suoi dati, in lettura/scrittura ed ai dati di tutti gli" & _
        " Operatori CRM in sola lettura"
    Me.opAltriAccessi2.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opAltriAccessi2.Size = New System.Drawing.Size(521, 18)
    Me.opAltriAccessi2.TabIndex = 2
    '
    'opAltriAccessi1
    '
    Me.opAltriAccessi1.Cursor = System.Windows.Forms.Cursors.Default
    Me.opAltriAccessi1.EditValue = True
    Me.opAltriAccessi1.Location = New System.Drawing.Point(18, 25)
    Me.opAltriAccessi1.Name = "opAltriAccessi1"
    Me.opAltriAccessi1.NTSCheckValue = "S"
    Me.opAltriAccessi1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opAltriAccessi1.Properties.Appearance.Options.UseBackColor = True
    Me.opAltriAccessi1.Properties.Caption = "L'Operatore ha accesso solo ai suoi dati"
    Me.opAltriAccessi1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opAltriAccessi1.Size = New System.Drawing.Size(211, 18)
    Me.opAltriAccessi1.TabIndex = 1
    '
    'fmOrganizzazioneDitta
    '
    Me.fmOrganizzazioneDitta.AllowDrop = True
    Me.fmOrganizzazioneDitta.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmOrganizzazioneDitta.Appearance.Options.UseBackColor = True
    Me.fmOrganizzazioneDitta.Controls.Add(Me.lbCell)
    Me.fmOrganizzazioneDitta.Controls.Add(Me.edCell)
    Me.fmOrganizzazioneDitta.Controls.Add(Me.lbEmail)
    Me.fmOrganizzazioneDitta.Controls.Add(Me.edEmail)
    Me.fmOrganizzazioneDitta.Controls.Add(Me.lbDescodruaz)
    Me.fmOrganizzazioneDitta.Controls.Add(Me.edCodruaz)
    Me.fmOrganizzazioneDitta.Controls.Add(Me.lbCodruaz)
    Me.fmOrganizzazioneDitta.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmOrganizzazioneDitta.Location = New System.Drawing.Point(10, 112)
    Me.fmOrganizzazioneDitta.Name = "fmOrganizzazioneDitta"
    Me.fmOrganizzazioneDitta.Size = New System.Drawing.Size(653, 102)
    Me.fmOrganizzazioneDitta.TabIndex = 660
    Me.fmOrganizzazioneDitta.Text = "Organizzazione Ditta"
    '
    'lbCell
    '
    Me.lbCell.AutoSize = True
    Me.lbCell.BackColor = System.Drawing.Color.Transparent
    Me.lbCell.Location = New System.Drawing.Point(15, 74)
    Me.lbCell.Name = "lbCell"
    Me.lbCell.NTSDbField = ""
    Me.lbCell.Size = New System.Drawing.Size(63, 13)
    Me.lbCell.TabIndex = 526
    Me.lbCell.Text = "N° Cellulare"
    Me.lbCell.UseMnemonic = False
    '
    'edCell
    '
    Me.edCell.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCell.Location = New System.Drawing.Point(119, 71)
    Me.edCell.Name = "edCell"
    Me.edCell.NTSDbField = ""
    Me.edCell.NTSForzaVisZoom = False
    Me.edCell.NTSOldValue = ""
    Me.edCell.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCell.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCell.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCell.Properties.MaxLength = 65536
    Me.edCell.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCell.Size = New System.Drawing.Size(286, 20)
    Me.edCell.TabIndex = 525
    '
    'lbEmail
    '
    Me.lbEmail.AutoSize = True
    Me.lbEmail.BackColor = System.Drawing.Color.Transparent
    Me.lbEmail.Location = New System.Drawing.Point(15, 51)
    Me.lbEmail.Name = "lbEmail"
    Me.lbEmail.NTSDbField = ""
    Me.lbEmail.Size = New System.Drawing.Size(78, 13)
    Me.lbEmail.TabIndex = 524
    Me.lbEmail.Text = "Indirizzo E-mail"
    Me.lbEmail.UseMnemonic = False
    '
    'edEmail
    '
    Me.edEmail.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEmail.Location = New System.Drawing.Point(119, 48)
    Me.edEmail.Name = "edEmail"
    Me.edEmail.NTSDbField = ""
    Me.edEmail.NTSForzaVisZoom = False
    Me.edEmail.NTSOldValue = ""
    Me.edEmail.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEmail.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEmail.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEmail.Properties.MaxLength = 65536
    Me.edEmail.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEmail.Size = New System.Drawing.Size(519, 20)
    Me.edEmail.TabIndex = 523
    '
    'lbDescodruaz
    '
    Me.lbDescodruaz.BackColor = System.Drawing.Color.Transparent
    Me.lbDescodruaz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescodruaz.Location = New System.Drawing.Point(188, 25)
    Me.lbDescodruaz.Name = "lbDescodruaz"
    Me.lbDescodruaz.NTSDbField = ""
    Me.lbDescodruaz.Size = New System.Drawing.Size(450, 20)
    Me.lbDescodruaz.TabIndex = 522
    Me.lbDescodruaz.UseMnemonic = False
    '
    'edCodruaz
    '
    Me.edCodruaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodruaz.Location = New System.Drawing.Point(119, 25)
    Me.edCodruaz.Name = "edCodruaz"
    Me.edCodruaz.NTSDbField = ""
    Me.edCodruaz.NTSForzaVisZoom = False
    Me.edCodruaz.NTSOldValue = ""
    Me.edCodruaz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodruaz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodruaz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodruaz.Properties.MaxLength = 65536
    Me.edCodruaz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodruaz.Size = New System.Drawing.Size(63, 20)
    Me.edCodruaz.TabIndex = 100
    '
    'lbCodruaz
    '
    Me.lbCodruaz.AutoSize = True
    Me.lbCodruaz.BackColor = System.Drawing.Color.Transparent
    Me.lbCodruaz.Location = New System.Drawing.Point(15, 28)
    Me.lbCodruaz.Name = "lbCodruaz"
    Me.lbCodruaz.NTSDbField = ""
    Me.lbCodruaz.Size = New System.Drawing.Size(83, 13)
    Me.lbCodruaz.TabIndex = 2
    Me.lbCodruaz.Text = "Ruolo Aziendale"
    Me.lbCodruaz.UseMnemonic = False
    '
    'fmAccessiDitta
    '
    Me.fmAccessiDitta.AllowDrop = True
    Me.fmAccessiDitta.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmAccessiDitta.Appearance.Options.UseBackColor = True
    Me.fmAccessiDitta.Controls.Add(Me.lbDescodcage)
    Me.fmAccessiDitta.Controls.Add(Me.edCodcage)
    Me.fmAccessiDitta.Controls.Add(Me.ckCodcage)
    Me.fmAccessiDitta.Controls.Add(Me.ckAmm)
    Me.fmAccessiDitta.Controls.Add(Me.ckCrmmod)
    Me.fmAccessiDitta.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmAccessiDitta.Location = New System.Drawing.Point(10, 11)
    Me.fmAccessiDitta.Name = "fmAccessiDitta"
    Me.fmAccessiDitta.Size = New System.Drawing.Size(653, 95)
    Me.fmAccessiDitta.TabIndex = 659
    Me.fmAccessiDitta.Text = "Accessi Ditta per Operatore"
    '
    'lbDescodcage
    '
    Me.lbDescodcage.BackColor = System.Drawing.Color.Transparent
    Me.lbDescodcage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescodcage.Location = New System.Drawing.Point(259, 65)
    Me.lbDescodcage.Name = "lbDescodcage"
    Me.lbDescodcage.NTSDbField = ""
    Me.lbDescodcage.Size = New System.Drawing.Size(379, 20)
    Me.lbDescodcage.TabIndex = 521
    Me.lbDescodcage.UseMnemonic = False
    '
    'edCodcage
    '
    Me.edCodcage.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodcage.EditValue = "0"
    Me.edCodcage.Enabled = False
    Me.edCodcage.Location = New System.Drawing.Point(188, 65)
    Me.edCodcage.Name = "edCodcage"
    Me.edCodcage.NTSDbField = ""
    Me.edCodcage.NTSFormat = "0"
    Me.edCodcage.NTSForzaVisZoom = False
    Me.edCodcage.NTSOldValue = ""
    Me.edCodcage.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodcage.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodcage.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodcage.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodcage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodcage.Properties.MaxLength = 65536
    Me.edCodcage.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodcage.Size = New System.Drawing.Size(64, 20)
    Me.edCodcage.TabIndex = 520
    '
    'ckCodcage
    '
    Me.ckCodcage.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckCodcage.Location = New System.Drawing.Point(18, 65)
    Me.ckCodcage.Name = "ckCodcage"
    Me.ckCodcage.NTSCheckValue = "S"
    Me.ckCodcage.NTSUnCheckValue = "N"
    Me.ckCodcage.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckCodcage.Properties.Appearance.Options.UseBackColor = True
    Me.ckCodcage.Properties.Caption = "Il nuovo Utente è un Agente"
    Me.ckCodcage.Size = New System.Drawing.Size(164, 18)
    Me.ckCodcage.TabIndex = 24
    '
    'ckAmm
    '
    Me.ckAmm.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAmm.Location = New System.Drawing.Point(18, 45)
    Me.ckAmm.Name = "ckAmm"
    Me.ckAmm.NTSCheckValue = "S"
    Me.ckAmm.NTSUnCheckValue = "N"
    Me.ckAmm.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAmm.Properties.Appearance.Options.UseBackColor = True
    Me.ckAmm.Properties.Caption = "Il nuovo Utente ha la possibilità di vedere le Anagrafiche Fornitori e gli elench" & _
        "i dei Fornitori"
    Me.ckAmm.Size = New System.Drawing.Size(461, 18)
    Me.ckAmm.TabIndex = 23
    '
    'ckCrmmod
    '
    Me.ckCrmmod.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckCrmmod.EditValue = True
    Me.ckCrmmod.Location = New System.Drawing.Point(18, 25)
    Me.ckCrmmod.Name = "ckCrmmod"
    Me.ckCrmmod.NTSCheckValue = "S"
    Me.ckCrmmod.NTSUnCheckValue = "N"
    Me.ckCrmmod.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckCrmmod.Properties.Appearance.Options.UseBackColor = True
    Me.ckCrmmod.Properties.Caption = "Il nuovo Utente oltre alla possibilità di vedere i dati della ditta corrente, può" & _
        " anche modificare/inserirne di nuovi"
    Me.ckCrmmod.Size = New System.Drawing.Size(561, 18)
    Me.ckCrmmod.TabIndex = 22
    '
    'fmPulsanti
    '
    Me.fmPulsanti.AllowDrop = True
    Me.fmPulsanti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmPulsanti.Appearance.Options.UseBackColor = True
    Me.fmPulsanti.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.fmPulsanti.Controls.Add(Me.cmdProcedi)
    Me.fmPulsanti.Controls.Add(Me.cmdAnnulla)
    Me.fmPulsanti.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmPulsanti.Location = New System.Drawing.Point(680, 13)
    Me.fmPulsanti.Name = "fmPulsanti"
    Me.fmPulsanti.ShowCaption = False
    Me.fmPulsanti.Size = New System.Drawing.Size(176, 53)
    Me.fmPulsanti.TabIndex = 660
    '
    'FRM__WZRD
    '
    Me.ClientSize = New System.Drawing.Size(863, 471)
    Me.Controls.Add(Me.fmPulsanti)
    Me.Controls.Add(Me.fmControlli)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__WZRD"
    Me.Text = "WIZARD IMPOSTAZIONE UTENTE CRM"
    CType(Me.fmControlli, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmControlli.ResumeLayout(False)
    CType(Me.fmSostituisce, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmSostituisce.ResumeLayout(False)
    Me.fmSostituisce.PerformLayout()
    CType(Me.edOldAgente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOldOperatore.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSostituisce.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmAltriAccessi, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmAltriAccessi.ResumeLayout(False)
    Me.fmAltriAccessi.PerformLayout()
    CType(Me.opAltriAccessi3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opAltriAccessi2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opAltriAccessi1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmOrganizzazioneDitta, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmOrganizzazioneDitta.ResumeLayout(False)
    Me.fmOrganizzazioneDitta.PerformLayout()
    CType(Me.edCell.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEmail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodruaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmAccessiDitta, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmAccessiDitta.ResumeLayout(False)
    Me.fmAccessiDitta.PerformLayout()
    CType(Me.edCodcage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckCodcage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAmm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckCrmmod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmPulsanti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmPulsanti.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Accessi Ditta per Operatore
      '--------------------------------------------------------------------------------------------------------------
      ckCrmmod.NTSSetParam(oMenu, oApp.Tr(Me, 128982823894606772, "Il nuovo Utente oltre alla possibilità di vedere i dati della ditta corrente, può anche modificare/inserirne di nuovi"), "S", "N")
      ckAmm.NTSSetParam(oMenu, oApp.Tr(Me, 128982824177731772, "Il nuovo Utente ha la possibilità di vedere le Anagrafiche Fornitori e gli elenchi dei Fornitori"), "S", "N")
      ckCodcage.NTSSetParam(oMenu, oApp.Tr(Me, 128982824328044272, "Il nuovo Utente è un Agente"), "S", "N")
      edCodcage.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426228906250, "Agente"), tabcage)
      '--------------------------------------------------------------------------------------------------------------
      '--- Organizzazione Ditta
      '--------------------------------------------------------------------------------------------------------------
      edCodruaz.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128982826409138022, "Ruolo Aziendale"), tabruaz, True)
      edEmail.NTSSetParam(oMenu, oApp.Tr(Me, 128982828093874097, "Indirizzo E-mail"), 50)
      edCell.NTSSetParam(oMenu, oApp.Tr(Me, 128982828601374097, "N° cellulare"), 18)
      '--------------------------------------------------------------------------------------------------------------
      '--- Altri Accessi
      '--------------------------------------------------------------------------------------------------------------
      opAltriAccessi1.NTSSetParam(oMenu, oApp.Tr(Me, 128982829456374097, "L'Operatore ha accesso solo ai suoi dati"), "S")
      opAltriAccessi2.NTSSetParam(oMenu, oApp.Tr(Me, 128982829578249097, "L'Operatore ha accesso ai suoi dati, in lettura/scrittura ed ai dati di tutti gli Operatori CRM in sola lettura"), "S")
      opAltriAccessi3.NTSSetParam(oMenu, oApp.Tr(Me, 128982829597624097, "L'Operatore ha accesso ai suoi dati, in lettura/scrittura ed ai dati di tutti gli Operatori CRM in lettura e scrittura"), "S")
      '--------------------------------------------------------------------------------------------------------------
      '--- Sostituisce altro Utente/Agente
      '--------------------------------------------------------------------------------------------------------------
      ckSostituisce.NTSSetParam(oMenu, oApp.Tr(Me, 128982830454655347, "Il nuovo Utente sostituisce, nell'Organizzazione, un precedente Operatore/Agente"), "S", "N")
      edOldOperatore.NTSSetParam(oMenu, oApp.Tr(Me, 128983359086122170, "Operatore"), 20)
      edOldAgente.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128983359207215920, "Vecchio Agente"), tabcage)
      '--------------------------------------------------------------------------------------------------------------
      edOldOperatore.NTSSetParamZoom("ZOOMOPERAT")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Public Overridable Sub InitEntity(ByRef cleGope As CLE__GOPE)
    oCleGope = cleGope
    AddHandler oCleGope.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

#Region "FORM"
  Public Overridable Sub FRM__SEOP_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "TEXTBOX"
  Public Overridable Sub edCodcage_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodcage.Validated
    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleGope Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleGope.edCodcage_Validated(NTSCInt(edCodcage.Text), strTmp) Then
        edCodcage.Text = NTSCStr(edCodcage.OldEditValue)
      Else
        lbDescodcage.Text = strTmp
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edCodruaz_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodruaz.Validated
    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleGope Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleGope.edCodruaz_Validated(edCodruaz.Text, strTmp) Then
        edCodruaz.Text = NTSCStr(edCodruaz.OldEditValue)
      Else
        lbDescodruaz.Text = strTmp
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edOldAgente_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edOldAgente.Validated
    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleGope Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleGope.edOldAgente_Validated(NTSCInt(edOldAgente.Text), strTmp) Then
        edOldAgente.Text = NTSCStr(edOldAgente.OldEditValue)
      Else
        lbDesOldAgente.Text = strTmp
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edOldOperatore_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edOldOperatore.Validated
    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleGope Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleGope.edOldOperatore_Validated(edOldOperatore.Text) Then edOldOperatore.Text = NTSCStr(edOldOperatore.OldEditValue)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "CHECKBOX"
  Public Overridable Sub ckCodcage_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckCodcage.CheckedChanged
    Try
      If ckCodcage.Checked = True Then
        GctlSetVisEnab(edCodcage, False)
        GctlSetVisEnab(lbDescodcage, False)
        edCodcage.Focus()
      Else
        edCodcage.Text = "0"
        lbDescodcage.Text = ""
        edCodcage.Enabled = False
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub ckSostituisce_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSostituisce.CheckedChanged
    Try
      If ckSostituisce.Checked = True Then
        GctlSetVisEnab(lbOldOperatore, False)
        GctlSetVisEnab(edOldOperatore, False)
        GctlSetVisEnab(lbOldAgente, False)
        GctlSetVisEnab(edOldAgente, False)
        edOldOperatore.Focus()
      Else
        edOldOperatore.Text = ""
        edOldAgente.Text = "0"
        lbDesOldAgente.Text = ""
        lbOldOperatore.Enabled = False
        edOldOperatore.Enabled = False
        lbOldAgente.Enabled = False
        edOldAgente.Enabled = False
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "PULSANTI"
  Public Overridable Sub cmdProcedi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProcedi.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      If ckCodcage.Checked = True Then
        If NTSCInt(edCodcage.Text) = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128983656993498668, "Attenzione!" & vbCrLf & _
            "Se è stata selezionata l'opzione 'Il nuovo Utente è un Agente'" & vbCrLf & _
            "il codice Agente è obbligatorio, altrimenti deselezionare la scelta."))
          ckCodcage.Focus()
          Return
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If edCodruaz.Text.Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128983658347062763, "Attenzione!" & vbCrLf & _
          "Indicare un Ruolo Aziendale valido."))
        edCodruaz.Focus()
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If ckSostituisce.Checked = True Then
        If edOldOperatore.Text.Trim = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128985934345854110, "Attenzione!" & vbCrLf & _
            "Se è stato selezionato 'Il nuovo Utente sostituisce, nell'Organizzazione, un precedente Operatore/Agente'" & vbCrLf & _
            "L'operatore è obbligatorio."))
          edOldOperatore.Focus()
          Return
        End If
        If (NTSCInt(edOldAgente.Text) <> 0) And (NTSCInt(edCodcage.Text) = 0) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128986245489787005, "Attenzione!" & vbCrLf & _
            "Se è stato selezionato 'Il nuovo Utente sostituisce, nell'Organizzazione, un precedente Operatore/Agente'" & vbCrLf & _
            "ed è stato indicato il Vecchio Agente, il codice Agente del nuovo utente è obbligatorio."))
          If ckCodcage.Checked = True Then edCodcage.Focus() Else ckCodcage.Focus()
          Return
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Accessi Ditta per Operatore
      '--------------------------------------------------------------------------------------------------------------
      bCrmmod = ckCrmmod.Checked
      bAmm = ckAmm.Checked
      bCodcage = ckCodcage.Checked
      nCodcage = NTSCInt(edCodcage.Text)
      strDescodcage = lbDescodcage.Text
      '--------------------------------------------------------------------------------------------------------------
      '--- Organizzazione Ditta
      '--------------------------------------------------------------------------------------------------------------
      strCodruaz = edCodruaz.Text
      strDescodruaz = lbDescodruaz.Text
      strEmail = edEmail.Text
      strCell = edCell.Text
      '--------------------------------------------------------------------------------------------------------------
      '--- Altri Accessi
      '--------------------------------------------------------------------------------------------------------------
      If opAltriAccessi1.Checked = True Then nAltriAccessi = 1
      If opAltriAccessi2.Checked = True Then nAltriAccessi = 2
      If opAltriAccessi3.Checked = True Then nAltriAccessi = 3
      '--------------------------------------------------------------------------------------------------------------
      '--- Sostituisce altro Utente/Agente
      '--------------------------------------------------------------------------------------------------------------
      bSostituisce = ckSostituisce.Checked
      strOldOperatore = edOldOperatore.Text
      nOldAgente = NTSCInt(edOldAgente.Text)
      strDesOldAgente = lbDesOldAgente.Text
      '--------------------------------------------------------------------------------------------------------------
      bAnnullato = False
      '--------------------------------------------------------------------------------------------------------------
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      bAnnullato = True
      '--------------------------------------------------------------------------------------------------------------
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

End Class