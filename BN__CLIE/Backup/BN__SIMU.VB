Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__SIMU
  Public oCallParams As CLE__CLDP
  Public oCleClie As CLE__CLIE

  Public lConto As Integer = 0
  Public nListino As Integer = 0
  Public nAnClascon As Integer = 0

  Public strDescr1 As String = ""
  Public strDescr2 As String = ""
  Public strIndir As String = ""
  Public strCap As String = ""
  Public strCitta As String = ""
  Public strProv As String = ""
  Public strStato As String = ""
  Public strFmPrezzo As String = ""
  Public strFmSconti As String = ""

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
    Me.pnControlli = New NTSInformatica.NTSPanel
    Me.fmEsist = New NTSInformatica.NTSGroupBox
    Me.lbDesdisponibilitanetta = New NTSInformatica.NTSLabel
    Me.NtsLabel10 = New NTSInformatica.NTSLabel
    Me.lbDesdisponibilita = New NTSInformatica.NTSLabel
    Me.NtsLabel8 = New NTSInformatica.NTSLabel
    Me.lbDesprenotato = New NTSInformatica.NTSLabel
    Me.NtsLabel6 = New NTSInformatica.NTSLabel
    Me.lbDesordinato = New NTSInformatica.NTSLabel
    Me.NtsLabel4 = New NTSInformatica.NTSLabel
    Me.lbDesimpegnato = New NTSInformatica.NTSLabel
    Me.NtsLabel2 = New NTSInformatica.NTSLabel
    Me.lbDesgiacenza = New NTSInformatica.NTSLabel
    Me.lbGiacenza = New NTSInformatica.NTSLabel
    Me.fmValori = New NTSInformatica.NTSGroupBox
    Me.lbDesvalore = New NTSInformatica.NTSLabel
    Me.lbValore = New NTSInformatica.NTSLabel
    Me.lbValoreUnitario = New NTSInformatica.NTSLabel
    Me.lbDesvaloreUnitario = New NTSInformatica.NTSLabel
    Me.fmSconti = New NTSInformatica.NTSGroupBox
    Me.lbDessconto1 = New NTSInformatica.NTSLabel
    Me.lbSconto1 = New NTSInformatica.NTSLabel
    Me.lbDessconto2 = New NTSInformatica.NTSLabel
    Me.lbDessconto3 = New NTSInformatica.NTSLabel
    Me.lbSconto2 = New NTSInformatica.NTSLabel
    Me.lbSconto3 = New NTSInformatica.NTSLabel
    Me.lbDessconto4 = New NTSInformatica.NTSLabel
    Me.lbDessconto5 = New NTSInformatica.NTSLabel
    Me.lbSconto6 = New NTSInformatica.NTSLabel
    Me.lbDessconto6 = New NTSInformatica.NTSLabel
    Me.lbSconto5 = New NTSInformatica.NTSLabel
    Me.lbSconto4 = New NTSInformatica.NTSLabel
    Me.fmPrezzo = New NTSInformatica.NTSGroupBox
    Me.lbDesprezzo = New NTSInformatica.NTSLabel
    Me.lbPrezzo = New NTSInformatica.NTSLabel
    Me.edCodmaga = New NTSInformatica.NTSTextBoxNum
    Me.lbDescodmaga = New NTSInformatica.NTSLabel
    Me.lbCodmaga = New NTSInformatica.NTSLabel
    Me.edQuant = New NTSInformatica.NTSTextBoxNum
    Me.lbQuant = New NTSInformatica.NTSLabel
    Me.lbDescodart = New NTSInformatica.NTSLabel
    Me.edCodart = New NTSInformatica.NTSTextBoxStr
    Me.lbCodart = New NTSInformatica.NTSLabel
    Me.edConto = New NTSInformatica.NTSTextBoxNum
    Me.lbConto = New NTSInformatica.NTSLabel
    Me.lbDeslistino = New NTSInformatica.NTSLabel
    Me.edListino = New NTSInformatica.NTSTextBoxNum
    Me.lbListino = New NTSInformatica.NTSLabel
    Me.lbDesconto = New NTSInformatica.NTSLabel
    Me.pnPulsanti = New NTSInformatica.NTSPanel
    Me.cmdSimula = New NTSInformatica.NTSButton
    Me.cmdEsci = New NTSInformatica.NTSButton
    Me.lbDescoddest = New NTSInformatica.NTSLabel
    Me.lbCoddest = New NTSInformatica.NTSLabel
    Me.edCoddest = New NTSInformatica.NTSTextBoxNum
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnControlli, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnControlli.SuspendLayout()
    CType(Me.fmEsist, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmEsist.SuspendLayout()
    CType(Me.fmValori, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmValori.SuspendLayout()
    CType(Me.fmSconti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmSconti.SuspendLayout()
    CType(Me.fmPrezzo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmPrezzo.SuspendLayout()
    CType(Me.edCodmaga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edQuant.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edListino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnPulsanti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPulsanti.SuspendLayout()
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
    'pnControlli
    '
    Me.pnControlli.AllowDrop = True
    Me.pnControlli.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnControlli.Appearance.Options.UseBackColor = True
    Me.pnControlli.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnControlli.Controls.Add(Me.lbDescoddest)
    Me.pnControlli.Controls.Add(Me.lbCoddest)
    Me.pnControlli.Controls.Add(Me.edCoddest)
    Me.pnControlli.Controls.Add(Me.fmEsist)
    Me.pnControlli.Controls.Add(Me.fmValori)
    Me.pnControlli.Controls.Add(Me.fmSconti)
    Me.pnControlli.Controls.Add(Me.fmPrezzo)
    Me.pnControlli.Controls.Add(Me.edCodmaga)
    Me.pnControlli.Controls.Add(Me.lbDescodmaga)
    Me.pnControlli.Controls.Add(Me.lbCodmaga)
    Me.pnControlli.Controls.Add(Me.edQuant)
    Me.pnControlli.Controls.Add(Me.lbQuant)
    Me.pnControlli.Controls.Add(Me.lbDescodart)
    Me.pnControlli.Controls.Add(Me.edCodart)
    Me.pnControlli.Controls.Add(Me.lbCodart)
    Me.pnControlli.Controls.Add(Me.edConto)
    Me.pnControlli.Controls.Add(Me.lbConto)
    Me.pnControlli.Controls.Add(Me.lbDeslistino)
    Me.pnControlli.Controls.Add(Me.edListino)
    Me.pnControlli.Controls.Add(Me.lbListino)
    Me.pnControlli.Controls.Add(Me.lbDesconto)
    Me.pnControlli.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnControlli.Location = New System.Drawing.Point(12, 12)
    Me.pnControlli.Name = "pnControlli"
    Me.pnControlli.NTSActiveTrasparency = True
    Me.pnControlli.Size = New System.Drawing.Size(639, 435)
    Me.pnControlli.TabIndex = 13
    Me.pnControlli.Text = "NtsPanel1"
    '
    'fmEsist
    '
    Me.fmEsist.AllowDrop = True
    Me.fmEsist.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmEsist.Appearance.Options.UseBackColor = True
    Me.fmEsist.Controls.Add(Me.lbDesdisponibilitanetta)
    Me.fmEsist.Controls.Add(Me.NtsLabel10)
    Me.fmEsist.Controls.Add(Me.lbDesdisponibilita)
    Me.fmEsist.Controls.Add(Me.NtsLabel8)
    Me.fmEsist.Controls.Add(Me.lbDesprenotato)
    Me.fmEsist.Controls.Add(Me.NtsLabel6)
    Me.fmEsist.Controls.Add(Me.lbDesordinato)
    Me.fmEsist.Controls.Add(Me.NtsLabel4)
    Me.fmEsist.Controls.Add(Me.lbDesimpegnato)
    Me.fmEsist.Controls.Add(Me.NtsLabel2)
    Me.fmEsist.Controls.Add(Me.lbDesgiacenza)
    Me.fmEsist.Controls.Add(Me.lbGiacenza)
    Me.fmEsist.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmEsist.Location = New System.Drawing.Point(309, 262)
    Me.fmEsist.Name = "fmEsist"
    Me.fmEsist.Size = New System.Drawing.Size(323, 167)
    Me.fmEsist.TabIndex = 621
    Me.fmEsist.Text = " Progressivi articolo"
    '
    'lbDesdisponibilitanetta
    '
    Me.lbDesdisponibilitanetta.BackColor = System.Drawing.Color.Transparent
    Me.lbDesdisponibilitanetta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesdisponibilitanetta.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbDesdisponibilitanetta.Location = New System.Drawing.Point(157, 135)
    Me.lbDesdisponibilitanetta.Name = "lbDesdisponibilitanetta"
    Me.lbDesdisponibilitanetta.NTSDbField = ""
    Me.lbDesdisponibilitanetta.Size = New System.Drawing.Size(83, 20)
    Me.lbDesdisponibilitanetta.TabIndex = 623
    Me.lbDesdisponibilitanetta.Text = "0"
    Me.lbDesdisponibilitanetta.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.lbDesdisponibilitanetta.Tooltip = ""
    Me.lbDesdisponibilitanetta.UseMnemonic = False
    '
    'NtsLabel10
    '
    Me.NtsLabel10.AutoSize = True
    Me.NtsLabel10.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.NtsLabel10.Location = New System.Drawing.Point(13, 139)
    Me.NtsLabel10.Name = "NtsLabel10"
    Me.NtsLabel10.NTSDbField = ""
    Me.NtsLabel10.Size = New System.Drawing.Size(110, 13)
    Me.NtsLabel10.TabIndex = 622
    Me.NtsLabel10.Text = "Disponibilità netta"
    Me.NtsLabel10.Tooltip = ""
    Me.NtsLabel10.UseMnemonic = False
    '
    'lbDesdisponibilita
    '
    Me.lbDesdisponibilita.BackColor = System.Drawing.Color.Transparent
    Me.lbDesdisponibilita.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesdisponibilita.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbDesdisponibilita.Location = New System.Drawing.Point(157, 113)
    Me.lbDesdisponibilita.Name = "lbDesdisponibilita"
    Me.lbDesdisponibilita.NTSDbField = ""
    Me.lbDesdisponibilita.Size = New System.Drawing.Size(83, 20)
    Me.lbDesdisponibilita.TabIndex = 621
    Me.lbDesdisponibilita.Text = "0"
    Me.lbDesdisponibilita.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.lbDesdisponibilita.Tooltip = ""
    Me.lbDesdisponibilita.UseMnemonic = False
    '
    'NtsLabel8
    '
    Me.NtsLabel8.AutoSize = True
    Me.NtsLabel8.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.NtsLabel8.Location = New System.Drawing.Point(13, 117)
    Me.NtsLabel8.Name = "NtsLabel8"
    Me.NtsLabel8.NTSDbField = ""
    Me.NtsLabel8.Size = New System.Drawing.Size(76, 13)
    Me.NtsLabel8.TabIndex = 620
    Me.NtsLabel8.Text = "Disponibilità"
    Me.NtsLabel8.Tooltip = ""
    Me.NtsLabel8.UseMnemonic = False
    '
    'lbDesprenotato
    '
    Me.lbDesprenotato.BackColor = System.Drawing.Color.Transparent
    Me.lbDesprenotato.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesprenotato.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbDesprenotato.Location = New System.Drawing.Point(157, 91)
    Me.lbDesprenotato.Name = "lbDesprenotato"
    Me.lbDesprenotato.NTSDbField = ""
    Me.lbDesprenotato.Size = New System.Drawing.Size(83, 20)
    Me.lbDesprenotato.TabIndex = 619
    Me.lbDesprenotato.Text = "0"
    Me.lbDesprenotato.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.lbDesprenotato.Tooltip = ""
    Me.lbDesprenotato.UseMnemonic = False
    '
    'NtsLabel6
    '
    Me.NtsLabel6.AutoSize = True
    Me.NtsLabel6.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.NtsLabel6.Location = New System.Drawing.Point(13, 95)
    Me.NtsLabel6.Name = "NtsLabel6"
    Me.NtsLabel6.NTSDbField = ""
    Me.NtsLabel6.Size = New System.Drawing.Size(64, 13)
    Me.NtsLabel6.TabIndex = 618
    Me.NtsLabel6.Text = "Prenotato"
    Me.NtsLabel6.Tooltip = ""
    Me.NtsLabel6.UseMnemonic = False
    '
    'lbDesordinato
    '
    Me.lbDesordinato.BackColor = System.Drawing.Color.Transparent
    Me.lbDesordinato.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesordinato.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbDesordinato.Location = New System.Drawing.Point(157, 69)
    Me.lbDesordinato.Name = "lbDesordinato"
    Me.lbDesordinato.NTSDbField = ""
    Me.lbDesordinato.Size = New System.Drawing.Size(83, 20)
    Me.lbDesordinato.TabIndex = 617
    Me.lbDesordinato.Text = "0"
    Me.lbDesordinato.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.lbDesordinato.Tooltip = ""
    Me.lbDesordinato.UseMnemonic = False
    '
    'NtsLabel4
    '
    Me.NtsLabel4.AutoSize = True
    Me.NtsLabel4.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.NtsLabel4.Location = New System.Drawing.Point(13, 73)
    Me.NtsLabel4.Name = "NtsLabel4"
    Me.NtsLabel4.NTSDbField = ""
    Me.NtsLabel4.Size = New System.Drawing.Size(56, 13)
    Me.NtsLabel4.TabIndex = 616
    Me.NtsLabel4.Text = "Ordinato"
    Me.NtsLabel4.Tooltip = ""
    Me.NtsLabel4.UseMnemonic = False
    '
    'lbDesimpegnato
    '
    Me.lbDesimpegnato.BackColor = System.Drawing.Color.Transparent
    Me.lbDesimpegnato.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesimpegnato.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbDesimpegnato.Location = New System.Drawing.Point(157, 47)
    Me.lbDesimpegnato.Name = "lbDesimpegnato"
    Me.lbDesimpegnato.NTSDbField = ""
    Me.lbDesimpegnato.Size = New System.Drawing.Size(83, 20)
    Me.lbDesimpegnato.TabIndex = 615
    Me.lbDesimpegnato.Text = "0"
    Me.lbDesimpegnato.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.lbDesimpegnato.Tooltip = ""
    Me.lbDesimpegnato.UseMnemonic = False
    '
    'NtsLabel2
    '
    Me.NtsLabel2.AutoSize = True
    Me.NtsLabel2.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.NtsLabel2.Location = New System.Drawing.Point(13, 51)
    Me.NtsLabel2.Name = "NtsLabel2"
    Me.NtsLabel2.NTSDbField = ""
    Me.NtsLabel2.Size = New System.Drawing.Size(70, 13)
    Me.NtsLabel2.TabIndex = 614
    Me.NtsLabel2.Text = "Impegnato"
    Me.NtsLabel2.Tooltip = ""
    Me.NtsLabel2.UseMnemonic = False
    '
    'lbDesgiacenza
    '
    Me.lbDesgiacenza.BackColor = System.Drawing.Color.Transparent
    Me.lbDesgiacenza.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesgiacenza.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbDesgiacenza.Location = New System.Drawing.Point(157, 25)
    Me.lbDesgiacenza.Name = "lbDesgiacenza"
    Me.lbDesgiacenza.NTSDbField = ""
    Me.lbDesgiacenza.Size = New System.Drawing.Size(83, 20)
    Me.lbDesgiacenza.TabIndex = 613
    Me.lbDesgiacenza.Text = "0"
    Me.lbDesgiacenza.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.lbDesgiacenza.Tooltip = ""
    Me.lbDesgiacenza.UseMnemonic = False
    '
    'lbGiacenza
    '
    Me.lbGiacenza.AutoSize = True
    Me.lbGiacenza.BackColor = System.Drawing.Color.Transparent
    Me.lbGiacenza.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbGiacenza.Location = New System.Drawing.Point(13, 29)
    Me.lbGiacenza.Name = "lbGiacenza"
    Me.lbGiacenza.NTSDbField = ""
    Me.lbGiacenza.Size = New System.Drawing.Size(58, 13)
    Me.lbGiacenza.TabIndex = 612
    Me.lbGiacenza.Text = "Giacenza"
    Me.lbGiacenza.Tooltip = ""
    Me.lbGiacenza.UseMnemonic = False
    '
    'fmValori
    '
    Me.fmValori.AllowDrop = True
    Me.fmValori.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmValori.Appearance.Options.UseBackColor = True
    Me.fmValori.Controls.Add(Me.lbDesvalore)
    Me.fmValori.Controls.Add(Me.lbValore)
    Me.fmValori.Controls.Add(Me.lbValoreUnitario)
    Me.fmValori.Controls.Add(Me.lbDesvaloreUnitario)
    Me.fmValori.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmValori.Location = New System.Drawing.Point(309, 179)
    Me.fmValori.Name = "fmValori"
    Me.fmValori.Size = New System.Drawing.Size(323, 80)
    Me.fmValori.TabIndex = 620
    Me.fmValori.Text = " Valori"
    '
    'lbDesvalore
    '
    Me.lbDesvalore.BackColor = System.Drawing.Color.Transparent
    Me.lbDesvalore.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesvalore.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbDesvalore.Location = New System.Drawing.Point(157, 25)
    Me.lbDesvalore.Name = "lbDesvalore"
    Me.lbDesvalore.NTSDbField = ""
    Me.lbDesvalore.Size = New System.Drawing.Size(83, 20)
    Me.lbDesvalore.TabIndex = 615
    Me.lbDesvalore.Text = "0"
    Me.lbDesvalore.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.lbDesvalore.Tooltip = ""
    Me.lbDesvalore.UseMnemonic = False
    '
    'lbValore
    '
    Me.lbValore.AutoSize = True
    Me.lbValore.BackColor = System.Drawing.Color.Transparent
    Me.lbValore.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbValore.Location = New System.Drawing.Point(13, 29)
    Me.lbValore.Name = "lbValore"
    Me.lbValore.NTSDbField = ""
    Me.lbValore.Size = New System.Drawing.Size(43, 13)
    Me.lbValore.TabIndex = 614
    Me.lbValore.Text = "Valore"
    Me.lbValore.Tooltip = ""
    Me.lbValore.UseMnemonic = False
    '
    'lbValoreUnitario
    '
    Me.lbValoreUnitario.AutoSize = True
    Me.lbValoreUnitario.BackColor = System.Drawing.Color.Transparent
    Me.lbValoreUnitario.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbValoreUnitario.Location = New System.Drawing.Point(13, 51)
    Me.lbValoreUnitario.Name = "lbValoreUnitario"
    Me.lbValoreUnitario.NTSDbField = ""
    Me.lbValoreUnitario.Size = New System.Drawing.Size(90, 13)
    Me.lbValoreUnitario.TabIndex = 616
    Me.lbValoreUnitario.Text = "Valore unitario"
    Me.lbValoreUnitario.Tooltip = ""
    Me.lbValoreUnitario.UseMnemonic = False
    '
    'lbDesvaloreUnitario
    '
    Me.lbDesvaloreUnitario.BackColor = System.Drawing.Color.Transparent
    Me.lbDesvaloreUnitario.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesvaloreUnitario.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbDesvaloreUnitario.Location = New System.Drawing.Point(157, 47)
    Me.lbDesvaloreUnitario.Name = "lbDesvaloreUnitario"
    Me.lbDesvaloreUnitario.NTSDbField = ""
    Me.lbDesvaloreUnitario.Size = New System.Drawing.Size(83, 20)
    Me.lbDesvaloreUnitario.TabIndex = 617
    Me.lbDesvaloreUnitario.Text = "0"
    Me.lbDesvaloreUnitario.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.lbDesvaloreUnitario.Tooltip = ""
    Me.lbDesvaloreUnitario.UseMnemonic = False
    '
    'fmSconti
    '
    Me.fmSconti.AllowDrop = True
    Me.fmSconti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmSconti.Appearance.Options.UseBackColor = True
    Me.fmSconti.Controls.Add(Me.lbDessconto1)
    Me.fmSconti.Controls.Add(Me.lbSconto1)
    Me.fmSconti.Controls.Add(Me.lbDessconto2)
    Me.fmSconti.Controls.Add(Me.lbDessconto3)
    Me.fmSconti.Controls.Add(Me.lbSconto2)
    Me.fmSconti.Controls.Add(Me.lbSconto3)
    Me.fmSconti.Controls.Add(Me.lbDessconto4)
    Me.fmSconti.Controls.Add(Me.lbDessconto5)
    Me.fmSconti.Controls.Add(Me.lbSconto6)
    Me.fmSconti.Controls.Add(Me.lbDessconto6)
    Me.fmSconti.Controls.Add(Me.lbSconto5)
    Me.fmSconti.Controls.Add(Me.lbSconto4)
    Me.fmSconti.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmSconti.Location = New System.Drawing.Point(3, 262)
    Me.fmSconti.Name = "fmSconti"
    Me.fmSconti.Size = New System.Drawing.Size(289, 167)
    Me.fmSconti.TabIndex = 619
    Me.fmSconti.Text = " Sconti"
    '
    'lbDessconto1
    '
    Me.lbDessconto1.BackColor = System.Drawing.Color.Transparent
    Me.lbDessconto1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDessconto1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbDessconto1.Location = New System.Drawing.Point(95, 25)
    Me.lbDessconto1.Name = "lbDessconto1"
    Me.lbDessconto1.NTSDbField = ""
    Me.lbDessconto1.Size = New System.Drawing.Size(83, 20)
    Me.lbDessconto1.TabIndex = 601
    Me.lbDessconto1.Text = "0"
    Me.lbDessconto1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.lbDessconto1.Tooltip = ""
    Me.lbDessconto1.UseMnemonic = False
    '
    'lbSconto1
    '
    Me.lbSconto1.AutoSize = True
    Me.lbSconto1.BackColor = System.Drawing.Color.Transparent
    Me.lbSconto1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbSconto1.Location = New System.Drawing.Point(12, 29)
    Me.lbSconto1.Name = "lbSconto1"
    Me.lbSconto1.NTSDbField = ""
    Me.lbSconto1.Size = New System.Drawing.Size(56, 13)
    Me.lbSconto1.TabIndex = 600
    Me.lbSconto1.Text = "Sconto 1"
    Me.lbSconto1.Tooltip = ""
    Me.lbSconto1.UseMnemonic = False
    '
    'lbDessconto2
    '
    Me.lbDessconto2.BackColor = System.Drawing.Color.Transparent
    Me.lbDessconto2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDessconto2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbDessconto2.Location = New System.Drawing.Point(95, 47)
    Me.lbDessconto2.Name = "lbDessconto2"
    Me.lbDessconto2.NTSDbField = ""
    Me.lbDessconto2.Size = New System.Drawing.Size(83, 20)
    Me.lbDessconto2.TabIndex = 602
    Me.lbDessconto2.Text = "0"
    Me.lbDessconto2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.lbDessconto2.Tooltip = ""
    Me.lbDessconto2.UseMnemonic = False
    '
    'lbDessconto3
    '
    Me.lbDessconto3.BackColor = System.Drawing.Color.Transparent
    Me.lbDessconto3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDessconto3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbDessconto3.Location = New System.Drawing.Point(95, 69)
    Me.lbDessconto3.Name = "lbDessconto3"
    Me.lbDessconto3.NTSDbField = ""
    Me.lbDessconto3.Size = New System.Drawing.Size(83, 20)
    Me.lbDessconto3.TabIndex = 603
    Me.lbDessconto3.Text = "0"
    Me.lbDessconto3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.lbDessconto3.Tooltip = ""
    Me.lbDessconto3.UseMnemonic = False
    '
    'lbSconto2
    '
    Me.lbSconto2.AutoSize = True
    Me.lbSconto2.BackColor = System.Drawing.Color.Transparent
    Me.lbSconto2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbSconto2.Location = New System.Drawing.Point(12, 51)
    Me.lbSconto2.Name = "lbSconto2"
    Me.lbSconto2.NTSDbField = ""
    Me.lbSconto2.Size = New System.Drawing.Size(56, 13)
    Me.lbSconto2.TabIndex = 607
    Me.lbSconto2.Text = "Sconto 2"
    Me.lbSconto2.Tooltip = ""
    Me.lbSconto2.UseMnemonic = False
    '
    'lbSconto3
    '
    Me.lbSconto3.AutoSize = True
    Me.lbSconto3.BackColor = System.Drawing.Color.Transparent
    Me.lbSconto3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbSconto3.Location = New System.Drawing.Point(12, 73)
    Me.lbSconto3.Name = "lbSconto3"
    Me.lbSconto3.NTSDbField = ""
    Me.lbSconto3.Size = New System.Drawing.Size(56, 13)
    Me.lbSconto3.TabIndex = 608
    Me.lbSconto3.Text = "Sconto 3"
    Me.lbSconto3.Tooltip = ""
    Me.lbSconto3.UseMnemonic = False
    '
    'lbDessconto4
    '
    Me.lbDessconto4.BackColor = System.Drawing.Color.Transparent
    Me.lbDessconto4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDessconto4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbDessconto4.Location = New System.Drawing.Point(95, 91)
    Me.lbDessconto4.Name = "lbDessconto4"
    Me.lbDessconto4.NTSDbField = ""
    Me.lbDessconto4.Size = New System.Drawing.Size(83, 20)
    Me.lbDessconto4.TabIndex = 604
    Me.lbDessconto4.Text = "0"
    Me.lbDessconto4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.lbDessconto4.Tooltip = ""
    Me.lbDessconto4.UseMnemonic = False
    '
    'lbDessconto5
    '
    Me.lbDessconto5.BackColor = System.Drawing.Color.Transparent
    Me.lbDessconto5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDessconto5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbDessconto5.Location = New System.Drawing.Point(95, 113)
    Me.lbDessconto5.Name = "lbDessconto5"
    Me.lbDessconto5.NTSDbField = ""
    Me.lbDessconto5.Size = New System.Drawing.Size(83, 20)
    Me.lbDessconto5.TabIndex = 605
    Me.lbDessconto5.Text = "0"
    Me.lbDessconto5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.lbDessconto5.Tooltip = ""
    Me.lbDessconto5.UseMnemonic = False
    '
    'lbSconto6
    '
    Me.lbSconto6.AutoSize = True
    Me.lbSconto6.BackColor = System.Drawing.Color.Transparent
    Me.lbSconto6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbSconto6.Location = New System.Drawing.Point(12, 139)
    Me.lbSconto6.Name = "lbSconto6"
    Me.lbSconto6.NTSDbField = ""
    Me.lbSconto6.Size = New System.Drawing.Size(56, 13)
    Me.lbSconto6.TabIndex = 611
    Me.lbSconto6.Text = "Sconto 6"
    Me.lbSconto6.Tooltip = ""
    Me.lbSconto6.UseMnemonic = False
    '
    'lbDessconto6
    '
    Me.lbDessconto6.BackColor = System.Drawing.Color.Transparent
    Me.lbDessconto6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDessconto6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbDessconto6.Location = New System.Drawing.Point(95, 135)
    Me.lbDessconto6.Name = "lbDessconto6"
    Me.lbDessconto6.NTSDbField = ""
    Me.lbDessconto6.Size = New System.Drawing.Size(83, 20)
    Me.lbDessconto6.TabIndex = 606
    Me.lbDessconto6.Text = "0"
    Me.lbDessconto6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.lbDessconto6.Tooltip = ""
    Me.lbDessconto6.UseMnemonic = False
    '
    'lbSconto5
    '
    Me.lbSconto5.AutoSize = True
    Me.lbSconto5.BackColor = System.Drawing.Color.Transparent
    Me.lbSconto5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbSconto5.Location = New System.Drawing.Point(12, 117)
    Me.lbSconto5.Name = "lbSconto5"
    Me.lbSconto5.NTSDbField = ""
    Me.lbSconto5.Size = New System.Drawing.Size(56, 13)
    Me.lbSconto5.TabIndex = 610
    Me.lbSconto5.Text = "Sconto 5"
    Me.lbSconto5.Tooltip = ""
    Me.lbSconto5.UseMnemonic = False
    '
    'lbSconto4
    '
    Me.lbSconto4.AutoSize = True
    Me.lbSconto4.BackColor = System.Drawing.Color.Transparent
    Me.lbSconto4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbSconto4.Location = New System.Drawing.Point(12, 95)
    Me.lbSconto4.Name = "lbSconto4"
    Me.lbSconto4.NTSDbField = ""
    Me.lbSconto4.Size = New System.Drawing.Size(56, 13)
    Me.lbSconto4.TabIndex = 609
    Me.lbSconto4.Text = "Sconto 4"
    Me.lbSconto4.Tooltip = ""
    Me.lbSconto4.UseMnemonic = False
    '
    'fmPrezzo
    '
    Me.fmPrezzo.AllowDrop = True
    Me.fmPrezzo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmPrezzo.Appearance.Options.UseBackColor = True
    Me.fmPrezzo.Controls.Add(Me.lbDesprezzo)
    Me.fmPrezzo.Controls.Add(Me.lbPrezzo)
    Me.fmPrezzo.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmPrezzo.Location = New System.Drawing.Point(3, 179)
    Me.fmPrezzo.Name = "fmPrezzo"
    Me.fmPrezzo.Size = New System.Drawing.Size(289, 80)
    Me.fmPrezzo.TabIndex = 618
    Me.fmPrezzo.Text = " Prezzo"
    '
    'lbDesprezzo
    '
    Me.lbDesprezzo.BackColor = System.Drawing.Color.Transparent
    Me.lbDesprezzo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesprezzo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbDesprezzo.Location = New System.Drawing.Point(95, 39)
    Me.lbDesprezzo.Name = "lbDesprezzo"
    Me.lbDesprezzo.NTSDbField = ""
    Me.lbDesprezzo.Size = New System.Drawing.Size(83, 20)
    Me.lbDesprezzo.TabIndex = 599
    Me.lbDesprezzo.Text = "0"
    Me.lbDesprezzo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.lbDesprezzo.Tooltip = ""
    Me.lbDesprezzo.UseMnemonic = False
    '
    'lbPrezzo
    '
    Me.lbPrezzo.AutoSize = True
    Me.lbPrezzo.BackColor = System.Drawing.Color.Transparent
    Me.lbPrezzo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbPrezzo.Location = New System.Drawing.Point(12, 43)
    Me.lbPrezzo.Name = "lbPrezzo"
    Me.lbPrezzo.NTSDbField = ""
    Me.lbPrezzo.Size = New System.Drawing.Size(45, 13)
    Me.lbPrezzo.TabIndex = 598
    Me.lbPrezzo.Text = "Prezzo"
    Me.lbPrezzo.Tooltip = ""
    Me.lbPrezzo.UseMnemonic = False
    '
    'edCodmaga
    '
    Me.edCodmaga.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodmaga.EditValue = "0"
    Me.edCodmaga.Location = New System.Drawing.Point(98, 149)
    Me.edCodmaga.Name = "edCodmaga"
    Me.edCodmaga.NTSDbField = ""
    Me.edCodmaga.NTSFormat = "0"
    Me.edCodmaga.NTSForzaVisZoom = False
    Me.edCodmaga.NTSOldValue = ""
    Me.edCodmaga.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodmaga.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodmaga.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodmaga.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodmaga.Properties.AutoHeight = False
    Me.edCodmaga.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodmaga.Properties.MaxLength = 65536
    Me.edCodmaga.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodmaga.Size = New System.Drawing.Size(83, 20)
    Me.edCodmaga.TabIndex = 597
    Me.edCodmaga.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCodmaga.TextInt = 0
    '
    'lbDescodmaga
    '
    Me.lbDescodmaga.BackColor = System.Drawing.Color.Transparent
    Me.lbDescodmaga.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescodmaga.Location = New System.Drawing.Point(187, 149)
    Me.lbDescodmaga.Name = "lbDescodmaga"
    Me.lbDescodmaga.NTSDbField = ""
    Me.lbDescodmaga.Size = New System.Drawing.Size(445, 20)
    Me.lbDescodmaga.TabIndex = 596
    Me.lbDescodmaga.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDescodmaga.Tooltip = ""
    Me.lbDescodmaga.UseMnemonic = False
    '
    'lbCodmaga
    '
    Me.lbCodmaga.AutoSize = True
    Me.lbCodmaga.BackColor = System.Drawing.Color.Transparent
    Me.lbCodmaga.Location = New System.Drawing.Point(3, 152)
    Me.lbCodmaga.Name = "lbCodmaga"
    Me.lbCodmaga.NTSDbField = ""
    Me.lbCodmaga.Size = New System.Drawing.Size(57, 13)
    Me.lbCodmaga.TabIndex = 595
    Me.lbCodmaga.Text = "Magazzino"
    Me.lbCodmaga.Tooltip = ""
    Me.lbCodmaga.UseMnemonic = False
    '
    'edQuant
    '
    Me.edQuant.Cursor = System.Windows.Forms.Cursors.Default
    Me.edQuant.EditValue = "0"
    Me.edQuant.Location = New System.Drawing.Point(98, 127)
    Me.edQuant.Name = "edQuant"
    Me.edQuant.NTSDbField = ""
    Me.edQuant.NTSFormat = "0"
    Me.edQuant.NTSForzaVisZoom = False
    Me.edQuant.NTSOldValue = ""
    Me.edQuant.Properties.Appearance.Options.UseTextOptions = True
    Me.edQuant.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edQuant.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edQuant.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edQuant.Properties.AutoHeight = False
    Me.edQuant.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edQuant.Properties.MaxLength = 65536
    Me.edQuant.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edQuant.Size = New System.Drawing.Size(83, 20)
    Me.edQuant.TabIndex = 594
    Me.edQuant.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edQuant.TextInt = 0
    '
    'lbQuant
    '
    Me.lbQuant.AutoSize = True
    Me.lbQuant.BackColor = System.Drawing.Color.Transparent
    Me.lbQuant.Location = New System.Drawing.Point(3, 130)
    Me.lbQuant.Name = "lbQuant"
    Me.lbQuant.NTSDbField = ""
    Me.lbQuant.Size = New System.Drawing.Size(49, 13)
    Me.lbQuant.TabIndex = 593
    Me.lbQuant.Text = "Quantità"
    Me.lbQuant.Tooltip = ""
    Me.lbQuant.UseMnemonic = False
    '
    'lbDescodart
    '
    Me.lbDescodart.BackColor = System.Drawing.Color.Transparent
    Me.lbDescodart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescodart.Location = New System.Drawing.Point(298, 105)
    Me.lbDescodart.Name = "lbDescodart"
    Me.lbDescodart.NTSDbField = ""
    Me.lbDescodart.Size = New System.Drawing.Size(334, 20)
    Me.lbDescodart.TabIndex = 592
    Me.lbDescodart.Tooltip = ""
    Me.lbDescodart.UseMnemonic = False
    '
    'edCodart
    '
    Me.edCodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodart.Location = New System.Drawing.Point(98, 105)
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
    Me.edCodart.Size = New System.Drawing.Size(194, 20)
    Me.edCodart.TabIndex = 590
    Me.edCodart.TextStr = ""
    '
    'lbCodart
    '
    Me.lbCodart.AutoSize = True
    Me.lbCodart.BackColor = System.Drawing.Color.Transparent
    Me.lbCodart.Location = New System.Drawing.Point(3, 108)
    Me.lbCodart.Name = "lbCodart"
    Me.lbCodart.NTSDbField = ""
    Me.lbCodart.Size = New System.Drawing.Size(43, 13)
    Me.lbCodart.TabIndex = 591
    Me.lbCodart.Text = "Articolo"
    Me.lbCodart.Tooltip = ""
    Me.lbCodart.UseMnemonic = False
    '
    'edConto
    '
    Me.edConto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edConto.EditValue = "0"
    Me.edConto.Enabled = False
    Me.edConto.Location = New System.Drawing.Point(98, 5)
    Me.edConto.Name = "edConto"
    Me.edConto.NTSDbField = ""
    Me.edConto.NTSFormat = "0"
    Me.edConto.NTSForzaVisZoom = False
    Me.edConto.NTSOldValue = "0"
    Me.edConto.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edConto.Properties.Appearance.Options.UseBackColor = True
    Me.edConto.Properties.Appearance.Options.UseTextOptions = True
    Me.edConto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edConto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edConto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edConto.Properties.AutoHeight = False
    Me.edConto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edConto.Properties.MaxLength = 65536
    Me.edConto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edConto.Size = New System.Drawing.Size(83, 20)
    Me.edConto.TabIndex = 589
    Me.edConto.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edConto.TextInt = 0
    '
    'lbConto
    '
    Me.lbConto.AutoSize = True
    Me.lbConto.BackColor = System.Drawing.Color.Transparent
    Me.lbConto.Location = New System.Drawing.Point(3, 8)
    Me.lbConto.Name = "lbConto"
    Me.lbConto.NTSDbField = ""
    Me.lbConto.Size = New System.Drawing.Size(36, 13)
    Me.lbConto.TabIndex = 588
    Me.lbConto.Text = "Conto"
    Me.lbConto.Tooltip = ""
    Me.lbConto.UseMnemonic = False
    '
    'lbDeslistino
    '
    Me.lbDeslistino.BackColor = System.Drawing.Color.Transparent
    Me.lbDeslistino.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDeslistino.Location = New System.Drawing.Point(187, 83)
    Me.lbDeslistino.Name = "lbDeslistino"
    Me.lbDeslistino.NTSDbField = ""
    Me.lbDeslistino.Size = New System.Drawing.Size(445, 20)
    Me.lbDeslistino.TabIndex = 587
    Me.lbDeslistino.Tooltip = ""
    Me.lbDeslistino.UseMnemonic = False
    '
    'edListino
    '
    Me.edListino.Cursor = System.Windows.Forms.Cursors.Default
    Me.edListino.EditValue = "0"
    Me.edListino.Location = New System.Drawing.Point(98, 83)
    Me.edListino.Name = "edListino"
    Me.edListino.NTSDbField = ""
    Me.edListino.NTSFormat = "0"
    Me.edListino.NTSForzaVisZoom = False
    Me.edListino.NTSOldValue = "0"
    Me.edListino.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edListino.Properties.Appearance.Options.UseBackColor = True
    Me.edListino.Properties.Appearance.Options.UseTextOptions = True
    Me.edListino.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edListino.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edListino.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edListino.Properties.AutoHeight = False
    Me.edListino.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edListino.Properties.MaxLength = 65536
    Me.edListino.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edListino.Size = New System.Drawing.Size(83, 20)
    Me.edListino.TabIndex = 586
    Me.edListino.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edListino.TextInt = 0
    '
    'lbListino
    '
    Me.lbListino.AutoSize = True
    Me.lbListino.BackColor = System.Drawing.Color.Transparent
    Me.lbListino.Location = New System.Drawing.Point(3, 86)
    Me.lbListino.Name = "lbListino"
    Me.lbListino.NTSDbField = ""
    Me.lbListino.Size = New System.Drawing.Size(37, 13)
    Me.lbListino.TabIndex = 585
    Me.lbListino.Text = "Listino"
    Me.lbListino.Tooltip = ""
    Me.lbListino.UseMnemonic = False
    '
    'lbDesconto
    '
    Me.lbDesconto.BackColor = System.Drawing.Color.Transparent
    Me.lbDesconto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesconto.Location = New System.Drawing.Point(187, 5)
    Me.lbDesconto.Name = "lbDesconto"
    Me.lbDesconto.NTSDbField = ""
    Me.lbDesconto.Size = New System.Drawing.Size(445, 52)
    Me.lbDesconto.TabIndex = 584
    Me.lbDesconto.Tooltip = ""
    Me.lbDesconto.UseMnemonic = False
    '
    'pnPulsanti
    '
    Me.pnPulsanti.AllowDrop = True
    Me.pnPulsanti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPulsanti.Appearance.Options.UseBackColor = True
    Me.pnPulsanti.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPulsanti.Controls.Add(Me.cmdSimula)
    Me.pnPulsanti.Controls.Add(Me.cmdEsci)
    Me.pnPulsanti.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPulsanti.Location = New System.Drawing.Point(657, 12)
    Me.pnPulsanti.Name = "pnPulsanti"
    Me.pnPulsanti.NTSActiveTrasparency = True
    Me.pnPulsanti.Size = New System.Drawing.Size(135, 435)
    Me.pnPulsanti.TabIndex = 14
    Me.pnPulsanti.Text = "NtsPanel1"
    '
    'cmdSimula
    '
    Me.cmdSimula.ImageText = ""
    Me.cmdSimula.Location = New System.Drawing.Point(3, 3)
    Me.cmdSimula.Name = "cmdSimula"
    Me.cmdSimula.NTSContextMenu = Nothing
    Me.cmdSimula.Size = New System.Drawing.Size(128, 26)
    Me.cmdSimula.TabIndex = 13
    Me.cmdSimula.Text = "&Simula"
    '
    'cmdEsci
    '
    Me.cmdEsci.ImageText = ""
    Me.cmdEsci.Location = New System.Drawing.Point(3, 31)
    Me.cmdEsci.Name = "cmdEsci"
    Me.cmdEsci.NTSContextMenu = Nothing
    Me.cmdEsci.Size = New System.Drawing.Size(128, 26)
    Me.cmdEsci.TabIndex = 12
    Me.cmdEsci.Text = "&Esci"
    '
    'lbDescoddest
    '
    Me.lbDescoddest.BackColor = System.Drawing.Color.Transparent
    Me.lbDescoddest.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescoddest.Location = New System.Drawing.Point(187, 60)
    Me.lbDescoddest.Name = "lbDescoddest"
    Me.lbDescoddest.NTSDbField = ""
    Me.lbDescoddest.Size = New System.Drawing.Size(445, 20)
    Me.lbDescoddest.TabIndex = 624
    Me.lbDescoddest.Tooltip = ""
    Me.lbDescoddest.UseMnemonic = False
    '
    'lbCoddest
    '
    Me.lbCoddest.AutoSize = True
    Me.lbCoddest.BackColor = System.Drawing.Color.Transparent
    Me.lbCoddest.Location = New System.Drawing.Point(3, 63)
    Me.lbCoddest.Name = "lbCoddest"
    Me.lbCoddest.NTSDbField = ""
    Me.lbCoddest.Size = New System.Drawing.Size(68, 13)
    Me.lbCoddest.TabIndex = 623
    Me.lbCoddest.Text = "Destinazione"
    Me.lbCoddest.Tooltip = ""
    Me.lbCoddest.UseMnemonic = False
    '
    'edCoddest
    '
    Me.edCoddest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCoddest.EditValue = "0"
    Me.edCoddest.Location = New System.Drawing.Point(98, 60)
    Me.edCoddest.Name = "edCoddest"
    Me.edCoddest.NTSDbField = ""
    Me.edCoddest.NTSFormat = "0"
    Me.edCoddest.NTSForzaVisZoom = False
    Me.edCoddest.NTSOldValue = ""
    Me.edCoddest.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCoddest.Properties.Appearance.Options.UseBackColor = True
    Me.edCoddest.Properties.Appearance.Options.UseTextOptions = True
    Me.edCoddest.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCoddest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCoddest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCoddest.Properties.AutoHeight = False
    Me.edCoddest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCoddest.Properties.MaxLength = 65536
    Me.edCoddest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCoddest.Size = New System.Drawing.Size(83, 20)
    Me.edCoddest.TabIndex = 622
    Me.edCoddest.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCoddest.TextInt = 0
    '
    'FRM__SIMU
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(797, 453)
    Me.Controls.Add(Me.pnPulsanti)
    Me.Controls.Add(Me.pnControlli)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__SIMU"
    Me.Text = "SIMULAZIONE VENDITA"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnControlli, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnControlli.ResumeLayout(False)
    Me.pnControlli.PerformLayout()
    CType(Me.fmEsist, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmEsist.ResumeLayout(False)
    Me.fmEsist.PerformLayout()
    CType(Me.fmValori, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmValori.ResumeLayout(False)
    Me.fmValori.PerformLayout()
    CType(Me.fmSconti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmSconti.ResumeLayout(False)
    Me.fmSconti.PerformLayout()
    CType(Me.fmPrezzo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmPrezzo.ResumeLayout(False)
    Me.fmPrezzo.PerformLayout()
    CType(Me.edCodmaga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edQuant.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edListino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPulsanti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPulsanti.ResumeLayout(False)
    CType(Me.edCoddest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, False)
    '----------------------------------------------------------------------------------------------------------------
    Try
      '--------------------------------------------------------------------------------------------------------------
      edConto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128377746572922000, "Conto"), tabanagra)
      edCoddest.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129048472787388675, "Destinazione diversa"), tabdestdiv)
      edCoddest.CliePerDestdiv = edConto
      edListino.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129833664275359698, "Listino"), tablist)
      edCodart.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129833664500003523, "Articolo"), tabartico, True)
      edQuant.NTSSetParam(oMenu, oApp.Tr(Me, 129833664816741063, "Quantità"), oApp.FormatQta, 13, 0, 9999999999999)
      edCodmaga.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129833665917570144, "Magazzino"), tabmaga)
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
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRM__SIMU_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim nTmp As Integer = 0

    Try
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      edConto.Text = lConto.ToString
      lbDesconto.Text = strDescr1.Trim & _
        IIf(strDescr2.Trim <> "", vbCrLf & strDescr2.Trim, "").ToString & _
        IIf(strIndir.Trim <> "", vbCrLf & strIndir, "").ToString
      If (strCap.Trim <> "") Or (strCitta.Trim <> "") Or (strProv.Trim <> "") Or (strStato.Trim <> "") Then
        lbDesconto.Text += vbCrLf & _
          IIf(strCap.Trim <> "", strCap.Trim & " ", "").ToString & _
          IIf(strCitta.Trim <> "", strCitta.Trim & " ", "").ToString & _
          IIf(strProv.Trim <> "", "(" & strProv.Trim & ") ", "").ToString & _
          IIf(strStato.Trim <> "", "(" & strStato.Trim & ")", "").ToString
      End If
      edListino.Text = nListino.ToString
      edQuant.Text = "1"
      nTmp = NTSCInt(oMenu.GetSettingBus("BS--SIMU", "RECENT", ".", "Magazzino", "0", " ", "0"))
      If nTmp = 0 Then nTmp = NTSCInt(oCleClie.PrimoMagazzino().ToString)
      edCodmaga.Text = nTmp.ToString
      '--------------------------------------------------------------------------------------------------------------
      strFmPrezzo = fmPrezzo.Text
      strFmSconti = fmSconti.Text
      '--------------------------------------------------------------------------------------------------------------
      AzzeraRisultati()
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      GctlApplicaDefaultValue()
      '--------------------------------------------------------------------------------------------------------------
      edCodart.Focus()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRM__SIMU_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Dim ctrlTmp As Control = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
        Zoom()
        e.Handled = True
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi TextBox"
  Public Overridable Sub edListino_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edListino.Validated
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleClie Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edListino.Text) = 0 Then
        lbDeslistino.Text = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oMenu.ValCodiceDb(edListino.Text, DittaCorrente, "TABLIST", "N", strTmp) = False Then
        lbDeslistino.Text = ""
      Else
        lbDeslistino.Text = strTmp
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edCodart_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodart.Validated
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleClie Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      If edCodart.Text.Trim = "" Then
        lbDescodart.Text = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oMenu.ValCodiceDb(edCodart.Text, DittaCorrente, "ARTICO", "S", strTmp) = False Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 129833720853759982, "Attenzione!" & vbCrLf & _
          "Articolo inesistente."))
        If Not edCodart.OldEditValue Is Nothing Then
          edCodart.Text = edCodart.OldEditValue.ToString
        Else
          edCodart.Text = ""
        End If
        Return
      Else
        lbDescodart.Text = strTmp
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edCodmaga_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodmaga.Validated
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleClie Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edCodmaga.Text) = 0 Then
        lbDescodmaga.Text = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oMenu.ValCodiceDb(edCodmaga.Text, DittaCorrente, "TABMAGA", "N", strTmp) = False Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 129833722294722442, "Attenzione!" & vbCrLf & _
          "Magazzino inesistente."))
        edCodmaga.Text = edCodmaga.OldEditValue.ToString
      Else
        lbDescodmaga.Text = strTmp
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edCoddest_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCoddest.Validated
    Dim strTmp As String = ""

    Try
      If oCleClie Is Nothing Then Return
      If NTSCInt(edCoddest.Text) = 0 Then
        lbDescoddest.Text = ""
        Return
      End If

      If Not oMenu.ValCodiceDb(edCoddest.Text, DittaCorrente, "DESTDIV", "N", strTmp, Nothing, edConto.Text) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130584436125707623, "Attenzione!" & vbCrLf & "Destinazione diversa inesistente."))
        edCoddest.Text = "0"
        lbDescoddest.Text = ""
      Else
        lbDescoddest.Text = strTmp
      End If

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi Pulsanti"
  Public Overridable Sub cmdSimula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSimula.Click
    Dim dPrezzo As Decimal = 0
    Dim dSconto1 As Decimal = 0
    Dim dSconto2 As Decimal = 0
    Dim dSconto3 As Decimal = 0
    Dim dSconto4 As Decimal = 0
    Dim dSconto5 As Decimal = 0
    Dim dSconto6 As Decimal = 0
    Dim dValore As Decimal = 0
    Dim dPerqta As Decimal = 0
    Dim strUmprz As String = ""
    Dim strTipovalListOut As String = ""
    Dim strTipovalScontOut As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If edCodart.Text.Trim = "" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 129833725934255122, "Attenzione!" & vbCrLf & _
          "Indicare un articolo valido."))
        edCodart.Focus()
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      AzzeraRisultati()
      '--------------------------------------------------------------------------------------------------------------
      If oCleClie.SimulaVendita(lConto, edCodart.Text, NTSCInt(edListino.Text), NTSCDec(edQuant.Text), _
        nAnClascon, dPrezzo, dSconto1, dSconto2, dSconto3, dSconto4, dSconto5, dSconto6, _
        strTipovalListOut, strTipovalScontOut, NTSCInt(edCoddest.Text)) = True Then
        lbDesprezzo.Text = Format(dPrezzo, oApp.FormatPrzUn)
        lbDessconto1.Text = Format(dSconto1, oApp.FormatSconti)
        lbDessconto2.Text = Format(dSconto2, oApp.FormatSconti)
        lbDessconto3.Text = Format(dSconto3, oApp.FormatSconti)
        lbDessconto4.Text = Format(dSconto4, oApp.FormatSconti)
        lbDessconto5.Text = Format(dSconto5, oApp.FormatSconti)
        lbDessconto6.Text = Format(dSconto6, oApp.FormatSconti)
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oMenu.ValCodiceDb(edCodart.Text, DittaCorrente, "ARTICO", "S", "", dttTmp) = True Then
        dPerqta = NTSCDec(dttTmp.Rows(0)!ar_perqta)
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      dValore = ArrDbl(dPrezzo * NTSCDec(edQuant.Text) / dPerqta * _
        (100 - dSconto1) / 100 * _
        (100 - dSconto2) / 100 * _
        (100 - dSconto3) / 100 * _
        (100 - dSconto4) / 100 * _
        (100 - dSconto5) / 100 * _
        (100 - dSconto6) / 100, oMenu.TrovaNdecSuPrzUn(0))
      lbDesvalore.Text = Format(dValore, oApp.FormatImporti)
      '--------------------------------------------------------------------------------------------------------------
      dValore = ArrDbl(dValore / NTSCDec(edQuant.Text) * dPerqta, oMenu.TrovaNdecSuPrzUn(0))
      lbDesvaloreUnitario.Text = Format(dValore, oApp.FormatImporti)
      '--------------------------------------------------------------------------------------------------------------
      Select Case strTipovalListOut
        Case "G" : fmPrezzo.Text += oApp.Tr(Me, 129833844035684103, " --> GENERICO")
        Case "S" : fmPrezzo.Text += oApp.Tr(Me, 129833844137552583, " --> SPECIALE")
      End Select
      '--------------------------------------------------------------------------------------------------------------
      Select Case strTipovalScontOut
        Case "A" : fmSconti.Text += oApp.Tr(Me, 129833845515950688, " --> Cliente/Articolo")
        Case "B" : fmSconti.Text += oApp.Tr(Me, 129833844606458118, " --> Articolo/Classe Cliente")
        Case "C" : fmSconti.Text += oApp.Tr(Me, 129833844620666193, " --> Cliente/Classe Articolo")
        Case "D" : fmSconti.Text += oApp.Tr(Me, 129833844635030508, " --> Classe Articolo/Classe Conto")
        Case "E" : fmSconti.Text += oApp.Tr(Me, 129833844648955398, " --> Generico Articolo")
        Case "F" : fmSconti.Text += oApp.Tr(Me, 129833844663641958, " --> Generico Cliente")
      End Select
      '--------------------------------------------------------------------------------------------------------------
      If oCleClie.ProgressiviMagazzino(edCodart.Text, NTSCInt(edCodmaga.Text), 0, dttTmp) Then
        With dttTmp.Rows(0)
          lbDesgiacenza.Text = Format(!ap_esist, oApp.FormatQta)
          lbDesimpegnato.Text = Format(!ap_impeg, oApp.FormatQta)
          lbDesordinato.Text = Format(!ap_ordin, oApp.FormatQta)
          lbDesprenotato.Text = Format(!ap_prenot, oApp.FormatQta)
          lbDesdisponibilita.Text = Format(NTSCDec(!ap_esist) - NTSCDec(!ap_prenot), oApp.FormatQta)
          lbDesdisponibilitanetta.Text = Format(NTSCDec(!ap_esist) + NTSCDec(!ap_ordin) - NTSCDec(!ap_impeg), oApp.FormatQta)
        End With
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Sub

  Public Overridable Sub cmdEsci_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEsci.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.Close()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Sub AzzeraRisultati()
    Try
      '--------------------------------------------------------------------------------------------------------------
      lbDesprezzo.Text = Format(0, oApp.FormatPrzUn)
      lbDessconto1.Text = Format(0, oApp.FormatSconti)
      lbDessconto2.Text = Format(0, oApp.FormatSconti)
      lbDessconto3.Text = Format(0, oApp.FormatSconti)
      lbDessconto4.Text = Format(0, oApp.FormatSconti)
      lbDessconto5.Text = Format(0, oApp.FormatSconti)
      lbDessconto6.Text = Format(0, oApp.FormatSconti)
      lbDesgiacenza.Text = Format(0, oApp.FormatQta)
      lbDesimpegnato.Text = Format(0, oApp.FormatQta)
      lbDesordinato.Text = Format(0, oApp.FormatQta)
      lbDesprenotato.Text = Format(0, oApp.FormatQta)
      lbDesdisponibilita.Text = Format(0, oApp.FormatQta)
      lbDesdisponibilitanetta.Text = Format(0, oApp.FormatQta)
      '--------------------------------------------------------------------------------------------------------------
      fmPrezzo.Text = strFmPrezzo
      fmSconti.Text = strFmSconti
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub Zoom()
    Dim oParam As New CLE__PATB
    Try
      If edCoddest.Focused Then
        SetFastZoom(edCoddest.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edCoddest.Text
        oParam.lContoCF = NTSCInt(edConto.Text)  'passo il conto cliente/fornitore
        NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edCoddest.Text Then edCoddest.NTSTextDB = NTSZOOM.strIn
      Else
        NTSCallStandardZoom()
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class