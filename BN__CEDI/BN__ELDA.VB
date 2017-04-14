Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__ELDA

#Region "Variabili"
  Public oCleCedi As CLE__CEDI
  Public oCallParams As CLE__CLDP
  Private components As System.ComponentModel.IContainer

  Public WithEvents frDatiEliminare As NTSInformatica.NTSGroupBox
  Public WithEvents cmdSelAll As NTSInformatica.NTSButton
  Public WithEvents cmdDesAll As NTSInformatica.NTSButton
  Public WithEvents pnElda As NTSInformatica.NTSPanel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOK As NTSInformatica.NTSButton
  Public WithEvents ckCRM As NTSInformatica.NTSCheckBox
  Public WithEvents ckPM_2 As NTSInformatica.NTSCheckBox
  Public WithEvents ckPM_1 As NTSInformatica.NTSCheckBox
  Public WithEvents ckPR As NTSInformatica.NTSCheckBox
  Public WithEvents ckBUD As NTSInformatica.NTSCheckBox
  Public WithEvents ckOR As NTSInformatica.NTSCheckBox
  Public WithEvents ckPRB As NTSInformatica.NTSCheckBox
  Public WithEvents ckMG As NTSInformatica.NTSCheckBox
  Public WithEvents ckCG_2 As NTSInformatica.NTSCheckBox
  Public WithEvents ckCG_1 As NTSInformatica.NTSCheckBox
  Public WithEvents ckMantieni As NTSInformatica.NTSCheckBox
  Public WithEvents ckTT As NTSInformatica.NTSCheckBox
  Public WithEvents ckAS_2 As NTSInformatica.NTSCheckBox
  Public WithEvents ckAs_1 As NTSInformatica.NTSCheckBox
  Public WithEvents ckDWH As NTSInformatica.NTSCheckBox
#End Region

  Private Sub InitializeComponent()
    Me.frDatiEliminare = New NTSInformatica.NTSGroupBox
    Me.ckStatven = New NTSInformatica.NTSCheckBox
    Me.ckScadEx = New NTSInformatica.NTSCheckBox
    Me.ckEmail = New NTSInformatica.NTSCheckBox
    Me.ckMovc = New NTSInformatica.NTSCheckBox
    Me.ckAnac = New NTSInformatica.NTSCheckBox
    Me.ckMantieni = New NTSInformatica.NTSCheckBox
    Me.ckTT = New NTSInformatica.NTSCheckBox
    Me.ckAS_2 = New NTSInformatica.NTSCheckBox
    Me.ckAs_1 = New NTSInformatica.NTSCheckBox
    Me.ckDWH = New NTSInformatica.NTSCheckBox
    Me.ckCRM = New NTSInformatica.NTSCheckBox
    Me.ckPM_2 = New NTSInformatica.NTSCheckBox
    Me.ckPM_1 = New NTSInformatica.NTSCheckBox
    Me.ckPR = New NTSInformatica.NTSCheckBox
    Me.ckBUD = New NTSInformatica.NTSCheckBox
    Me.ckOR = New NTSInformatica.NTSCheckBox
    Me.ckPRB = New NTSInformatica.NTSCheckBox
    Me.ckMG = New NTSInformatica.NTSCheckBox
    Me.ckCG_2 = New NTSInformatica.NTSCheckBox
    Me.ckCG_1 = New NTSInformatica.NTSCheckBox
    Me.cmdSelAll = New NTSInformatica.NTSButton
    Me.cmdDesAll = New NTSInformatica.NTSButton
    Me.pnElda = New NTSInformatica.NTSPanel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOK = New NTSInformatica.NTSButton
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frDatiEliminare, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frDatiEliminare.SuspendLayout()
    CType(Me.ckStatven.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckScadEx.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckEmail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckMovc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAnac.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckMantieni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTT.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAS_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAs_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDWH.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckCRM.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckPM_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckPM_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckPR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckBUD.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckOR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckPRB.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckMG.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckCG_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckCG_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnElda, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnElda.SuspendLayout()
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
    'frDatiEliminare
    '
    Me.frDatiEliminare.AllowDrop = True
    Me.frDatiEliminare.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frDatiEliminare.Appearance.Options.UseBackColor = True
    Me.frDatiEliminare.Controls.Add(Me.ckStatven)
    Me.frDatiEliminare.Controls.Add(Me.ckScadEx)
    Me.frDatiEliminare.Controls.Add(Me.ckEmail)
    Me.frDatiEliminare.Controls.Add(Me.ckMovc)
    Me.frDatiEliminare.Controls.Add(Me.ckAnac)
    Me.frDatiEliminare.Controls.Add(Me.ckMantieni)
    Me.frDatiEliminare.Controls.Add(Me.ckTT)
    Me.frDatiEliminare.Controls.Add(Me.ckAS_2)
    Me.frDatiEliminare.Controls.Add(Me.ckAs_1)
    Me.frDatiEliminare.Controls.Add(Me.ckDWH)
    Me.frDatiEliminare.Controls.Add(Me.ckCRM)
    Me.frDatiEliminare.Controls.Add(Me.ckPM_2)
    Me.frDatiEliminare.Controls.Add(Me.ckPM_1)
    Me.frDatiEliminare.Controls.Add(Me.ckPR)
    Me.frDatiEliminare.Controls.Add(Me.ckBUD)
    Me.frDatiEliminare.Controls.Add(Me.ckOR)
    Me.frDatiEliminare.Controls.Add(Me.ckPRB)
    Me.frDatiEliminare.Controls.Add(Me.ckMG)
    Me.frDatiEliminare.Controls.Add(Me.ckCG_2)
    Me.frDatiEliminare.Controls.Add(Me.ckCG_1)
    Me.frDatiEliminare.Cursor = System.Windows.Forms.Cursors.Default
    Me.frDatiEliminare.Location = New System.Drawing.Point(12, 13)
    Me.frDatiEliminare.Name = "frDatiEliminare"
    Me.frDatiEliminare.Size = New System.Drawing.Size(553, 408)
    Me.frDatiEliminare.TabIndex = 0
    Me.frDatiEliminare.Text = "Dati da eliminare: "
    '
    'ckStatven
    '
    Me.ckStatven.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckStatven.Location = New System.Drawing.Point(17, 243)
    Me.ckStatven.Name = "ckStatven"
    Me.ckStatven.NTSCheckValue = "S"
    Me.ckStatven.NTSUnCheckValue = "N"
    Me.ckStatven.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckStatven.Properties.Appearance.Options.UseBackColor = True
    Me.ckStatven.Properties.AutoHeight = False
    Me.ckStatven.Properties.Caption = "Statistiche (vecchio modulo)"
    Me.ckStatven.Size = New System.Drawing.Size(169, 19)
    Me.ckStatven.TabIndex = 22
    '
    'ckScadEx
    '
    Me.ckScadEx.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckScadEx.Location = New System.Drawing.Point(17, 363)
    Me.ckScadEx.Name = "ckScadEx"
    Me.ckScadEx.NTSCheckValue = "S"
    Me.ckScadEx.NTSUnCheckValue = "N"
    Me.ckScadEx.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckScadEx.Properties.Appearance.Options.UseBackColor = True
    Me.ckScadEx.Properties.AutoHeight = False
    Me.ckScadEx.Properties.Caption = "Scadenziario extra-contabile"
    Me.ckScadEx.Size = New System.Drawing.Size(169, 19)
    Me.ckScadEx.TabIndex = 21
    '
    'ckEmail
    '
    Me.ckEmail.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckEmail.Location = New System.Drawing.Point(17, 343)
    Me.ckEmail.Name = "ckEmail"
    Me.ckEmail.NTSCheckValue = "S"
    Me.ckEmail.NTSUnCheckValue = "N"
    Me.ckEmail.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckEmail.Properties.Appearance.Options.UseBackColor = True
    Me.ckEmail.Properties.AutoHeight = False
    Me.ckEmail.Properties.Caption = "Gestione Integrata E-mail"
    Me.ckEmail.Size = New System.Drawing.Size(151, 19)
    Me.ckEmail.TabIndex = 19
    '
    'ckMovc
    '
    Me.ckMovc.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckMovc.Location = New System.Drawing.Point(17, 323)
    Me.ckMovc.Name = "ckMovc"
    Me.ckMovc.NTSCheckValue = "S"
    Me.ckMovc.NTSUnCheckValue = "N"
    Me.ckMovc.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckMovc.Properties.Appearance.Options.UseBackColor = True
    Me.ckMovc.Properties.AutoHeight = False
    Me.ckMovc.Properties.Caption = "Movimenti Cespiti"
    Me.ckMovc.Size = New System.Drawing.Size(110, 19)
    Me.ckMovc.TabIndex = 18
    '
    'ckAnac
    '
    Me.ckAnac.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAnac.Location = New System.Drawing.Point(17, 303)
    Me.ckAnac.Name = "ckAnac"
    Me.ckAnac.NTSCheckValue = "S"
    Me.ckAnac.NTSUnCheckValue = "N"
    Me.ckAnac.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAnac.Properties.Appearance.Options.UseBackColor = True
    Me.ckAnac.Properties.AutoHeight = False
    Me.ckAnac.Properties.Caption = "Anagrafiche Cespiti"
    Me.ckAnac.Size = New System.Drawing.Size(122, 19)
    Me.ckAnac.TabIndex = 17
    '
    'ckMantieni
    '
    Me.ckMantieni.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckMantieni.Location = New System.Drawing.Point(373, 63)
    Me.ckMantieni.Name = "ckMantieni"
    Me.ckMantieni.NTSCheckValue = "S"
    Me.ckMantieni.NTSUnCheckValue = "N"
    Me.ckMantieni.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckMantieni.Properties.Appearance.Options.UseBackColor = True
    Me.ckMantieni.Properties.AutoHeight = False
    Me.ckMantieni.Properties.Caption = "Mantieni &ultimi costi d'acquisto"
    Me.ckMantieni.Size = New System.Drawing.Size(175, 19)
    Me.ckMantieni.TabIndex = 14
    '
    'ckTT
    '
    Me.ckTT.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTT.Location = New System.Drawing.Point(17, 383)
    Me.ckTT.Name = "ckTT"
    Me.ckTT.NTSCheckValue = "S"
    Me.ckTT.NTSUnCheckValue = "N"
    Me.ckTT.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTT.Properties.Appearance.Options.UseBackColor = True
    Me.ckTT.Properties.AutoHeight = False
    Me.ckTT.Properties.Caption = "&Tabelle Temporanee"
    Me.ckTT.Size = New System.Drawing.Size(127, 19)
    Me.ckTT.TabIndex = 13
    '
    'ckAS_2
    '
    Me.ckAS_2.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAS_2.Location = New System.Drawing.Point(17, 283)
    Me.ckAS_2.Name = "ckAS_2"
    Me.ckAS_2.NTSCheckValue = "S"
    Me.ckAS_2.NTSUnCheckValue = "N"
    Me.ckAS_2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAS_2.Properties.Appearance.Options.UseBackColor = True
    Me.ckAS_2.Properties.AutoHeight = False
    Me.ckAS_2.Properties.Caption = "Customer Service (Chiamate/attività/ricambi)"
    Me.ckAS_2.Size = New System.Drawing.Size(244, 19)
    Me.ckAS_2.TabIndex = 12
    '
    'ckAs_1
    '
    Me.ckAs_1.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAs_1.Location = New System.Drawing.Point(17, 263)
    Me.ckAs_1.Name = "ckAs_1"
    Me.ckAs_1.NTSCheckValue = "S"
    Me.ckAs_1.NTSUnCheckValue = "N"
    Me.ckAs_1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAs_1.Properties.Appearance.Options.UseBackColor = True
    Me.ckAs_1.Properties.AutoHeight = False
    Me.ckAs_1.Properties.Caption = "Customer Service (Contratti/matricole/problemi-soluzioni)"
    Me.ckAs_1.Size = New System.Drawing.Size(306, 19)
    Me.ckAs_1.TabIndex = 11
    '
    'ckDWH
    '
    Me.ckDWH.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDWH.Location = New System.Drawing.Point(17, 223)
    Me.ckDWH.Name = "ckDWH"
    Me.ckDWH.NTSCheckValue = "S"
    Me.ckDWH.NTSUnCheckValue = "N"
    Me.ckDWH.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDWH.Properties.Appearance.Options.UseBackColor = True
    Me.ckDWH.Properties.AutoHeight = False
    Me.ckDWH.Properties.Caption = "Datawarehouse"
    Me.ckDWH.Size = New System.Drawing.Size(110, 19)
    Me.ckDWH.TabIndex = 10
    '
    'ckCRM
    '
    Me.ckCRM.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckCRM.Location = New System.Drawing.Point(17, 203)
    Me.ckCRM.Name = "ckCRM"
    Me.ckCRM.NTSCheckValue = "S"
    Me.ckCRM.NTSUnCheckValue = "N"
    Me.ckCRM.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckCRM.Properties.Appearance.Options.UseBackColor = True
    Me.ckCRM.Properties.AutoHeight = False
    Me.ckCRM.Properties.Caption = "Movimenti CRM"
    Me.ckCRM.Size = New System.Drawing.Size(110, 19)
    Me.ckCRM.TabIndex = 9
    '
    'ckPM_2
    '
    Me.ckPM_2.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckPM_2.Location = New System.Drawing.Point(17, 183)
    Me.ckPM_2.Name = "ckPM_2"
    Me.ckPM_2.NTSCheckValue = "S"
    Me.ckPM_2.NTSUnCheckValue = "N"
    Me.ckPM_2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPM_2.Properties.Appearance.Options.UseBackColor = True
    Me.ckPM_2.Properties.AutoHeight = False
    Me.ckPM_2.Properties.Caption = "Struttura commesse Project mana&gement"
    Me.ckPM_2.Size = New System.Drawing.Size(231, 19)
    Me.ckPM_2.TabIndex = 8
    '
    'ckPM_1
    '
    Me.ckPM_1.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckPM_1.Location = New System.Drawing.Point(17, 163)
    Me.ckPM_1.Name = "ckPM_1"
    Me.ckPM_1.NTSCheckValue = "S"
    Me.ckPM_1.NTSUnCheckValue = "N"
    Me.ckPM_1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPM_1.Properties.Appearance.Options.UseBackColor = True
    Me.ckPM_1.Properties.AutoHeight = False
    Me.ckPM_1.Properties.Caption = "Movimenti avanzamento Pro&ject management"
    Me.ckPM_1.Size = New System.Drawing.Size(244, 19)
    Me.ckPM_1.TabIndex = 7
    '
    'ckPR
    '
    Me.ckPR.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckPR.Location = New System.Drawing.Point(17, 143)
    Me.ckPR.Name = "ckPR"
    Me.ckPR.NTSCheckValue = "S"
    Me.ckPR.NTSUnCheckValue = "N"
    Me.ckPR.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPR.Properties.Appearance.Options.UseBackColor = True
    Me.ckPR.Properties.AutoHeight = False
    Me.ckPR.Properties.Caption = "&Provvigioni e progressivi agenti"
    Me.ckPR.Size = New System.Drawing.Size(178, 19)
    Me.ckPR.TabIndex = 6
    '
    'ckBUD
    '
    Me.ckBUD.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckBUD.Location = New System.Drawing.Point(17, 123)
    Me.ckBUD.Name = "ckBUD"
    Me.ckBUD.NTSCheckValue = "S"
    Me.ckBUD.NTSUnCheckValue = "N"
    Me.ckBUD.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckBUD.Properties.Appearance.Options.UseBackColor = True
    Me.ckBUD.Properties.AutoHeight = False
    Me.ckBUD.Properties.Caption = "&Budget di Contabilità Analitica e di Vendita"
    Me.ckBUD.Size = New System.Drawing.Size(231, 19)
    Me.ckBUD.TabIndex = 5
    '
    'ckOR
    '
    Me.ckOR.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckOR.Location = New System.Drawing.Point(17, 103)
    Me.ckOR.Name = "ckOR"
    Me.ckOR.NTSCheckValue = "S"
    Me.ckOR.NTSUnCheckValue = "N"
    Me.ckOR.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckOR.Properties.Appearance.Options.UseBackColor = True
    Me.ckOR.Properties.AutoHeight = False
    Me.ckOR.Properties.Caption = "&Ordini, Impegni, preventivi, proposte d'ordine e attività collegate, offerte CRM" & _
        ""
    Me.ckOR.Size = New System.Drawing.Size(420, 19)
    Me.ckOR.TabIndex = 4
    '
    'ckPRB
    '
    Me.ckPRB.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckPRB.Location = New System.Drawing.Point(17, 83)
    Me.ckPRB.Name = "ckPRB"
    Me.ckPRB.NTSCheckValue = "S"
    Me.ckPRB.NTSUnCheckValue = "N"
    Me.ckPRB.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPRB.Properties.Appearance.Options.UseBackColor = True
    Me.ckPRB.Properties.AutoHeight = False
    Me.ckPRB.Properties.Caption = "&Note di prelievo"
    Me.ckPRB.Size = New System.Drawing.Size(110, 19)
    Me.ckPRB.TabIndex = 3
    '
    'ckMG
    '
    Me.ckMG.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckMG.Location = New System.Drawing.Point(17, 63)
    Me.ckMG.Name = "ckMG"
    Me.ckMG.NTSCheckValue = "S"
    Me.ckMG.NTSUnCheckValue = "N"
    Me.ckMG.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckMG.Properties.Appearance.Options.UseBackColor = True
    Me.ckMG.Properties.AutoHeight = False
    Me.ckMG.Properties.Caption = "Mo&vimenti  e progressivi di magazzino, dati Lifo, Collaudi e RNC"
    Me.ckMG.Size = New System.Drawing.Size(332, 19)
    Me.ckMG.TabIndex = 2
    '
    'ckCG_2
    '
    Me.ckCG_2.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckCG_2.Location = New System.Drawing.Point(17, 43)
    Me.ckCG_2.Name = "ckCG_2"
    Me.ckCG_2.NTSCheckValue = "S"
    Me.ckCG_2.NTSUnCheckValue = "N"
    Me.ckCG_2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckCG_2.Properties.Appearance.Options.UseBackColor = True
    Me.ckCG_2.Properties.AutoHeight = False
    Me.ckCG_2.Properties.Caption = "Dati &Intra e Ritenute e Tesoreria"
    Me.ckCG_2.Size = New System.Drawing.Size(189, 19)
    Me.ckCG_2.TabIndex = 1
    '
    'ckCG_1
    '
    Me.ckCG_1.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckCG_1.Location = New System.Drawing.Point(17, 23)
    Me.ckCG_1.Name = "ckCG_1"
    Me.ckCG_1.NTSCheckValue = "S"
    Me.ckCG_1.NTSUnCheckValue = "N"
    Me.ckCG_1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckCG_1.Properties.Appearance.Options.UseBackColor = True
    Me.ckCG_1.Properties.AutoHeight = False
    Me.ckCG_1.Properties.Caption = "&Movimenti contabili, IVA, scadenziario, progressivi di contabilità, progressivi " & _
        "IVA e movimenti di C.A. "
    Me.ckCG_1.Size = New System.Drawing.Size(515, 19)
    Me.ckCG_1.TabIndex = 0
    '
    'cmdSelAll
    '
    Me.cmdSelAll.ImagePath = ""
    Me.cmdSelAll.ImageText = ""
    Me.cmdSelAll.Location = New System.Drawing.Point(12, 427)
    Me.cmdSelAll.Name = "cmdSelAll"
    Me.cmdSelAll.NTSContextMenu = Nothing
    Me.cmdSelAll.Size = New System.Drawing.Size(139, 23)
    Me.cmdSelAll.TabIndex = 1
    Me.cmdSelAll.Text = "&Seleziona tutto"
    '
    'cmdDesAll
    '
    Me.cmdDesAll.ImagePath = ""
    Me.cmdDesAll.ImageText = ""
    Me.cmdDesAll.Location = New System.Drawing.Point(157, 427)
    Me.cmdDesAll.Name = "cmdDesAll"
    Me.cmdDesAll.NTSContextMenu = Nothing
    Me.cmdDesAll.Size = New System.Drawing.Size(139, 23)
    Me.cmdDesAll.TabIndex = 2
    Me.cmdDesAll.Text = "&Deseleziona tutto"
    '
    'pnElda
    '
    Me.pnElda.AllowDrop = True
    Me.pnElda.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnElda.Appearance.Options.UseBackColor = True
    Me.pnElda.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnElda.Controls.Add(Me.cmdAnnulla)
    Me.pnElda.Controls.Add(Me.cmdOK)
    Me.pnElda.Controls.Add(Me.cmdDesAll)
    Me.pnElda.Controls.Add(Me.cmdSelAll)
    Me.pnElda.Controls.Add(Me.frDatiEliminare)
    Me.pnElda.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnElda.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnElda.Location = New System.Drawing.Point(0, 0)
    Me.pnElda.Name = "pnElda"
    Me.pnElda.NTSActiveTrasparency = True
    Me.pnElda.Size = New System.Drawing.Size(581, 458)
    Me.pnElda.TabIndex = 0
    Me.pnElda.Text = "NtsPanel1"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(471, 427)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(94, 23)
    Me.cmdAnnulla.TabIndex = 4
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOK
    '
    Me.cmdOK.ImagePath = ""
    Me.cmdOK.ImageText = ""
    Me.cmdOK.Location = New System.Drawing.Point(371, 427)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.NTSContextMenu = Nothing
    Me.cmdOK.Size = New System.Drawing.Size(94, 23)
    Me.cmdOK.TabIndex = 3
    Me.cmdOK.Text = "O&k"
    '
    'FRM__ELDA
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(581, 458)
    Me.Controls.Add(Me.pnElda)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__ELDA"
    Me.Text = "ELIMINAZIONE DATI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frDatiEliminare, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frDatiEliminare.ResumeLayout(False)
    Me.frDatiEliminare.PerformLayout()
    CType(Me.ckStatven.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckScadEx.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckEmail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckMovc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAnac.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckMantieni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTT.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAS_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAs_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDWH.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckCRM.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckPM_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckPM_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckPR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckBUD.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckOR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckPRB.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckMG.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckCG_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckCG_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnElda, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnElda.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------

      ckMantieni.NTSSetParam(oMenu, oApp.Tr(Me, 128345123272992397, "Mantieni &ultimi costi d'acquisto"), "S", "N")
      ckTT.NTSSetParam(oMenu, oApp.Tr(Me, 128345123273148647, "&Tabelle Temporanee"), "S", "N")
      ckAS_2.NTSSetParam(oMenu, oApp.Tr(Me, 128345123273304897, "Customer Service (Chiamate/attività/ricambi)"), "S", "N")
      ckAs_1.NTSSetParam(oMenu, oApp.Tr(Me, 128345123273461147, "Customer Service (Contratti/matricole/problemi-soluzioni)"), "S", "N")
      ckDWH.NTSSetParam(oMenu, oApp.Tr(Me, 128345123273617397, "Datawarehouse"), "S", "N")
      ckStatven.NTSSetParam(oMenu, oApp.Tr(Me, 131006977150285690, "Statistiche (vecchio modulo)"), "S", "N")
      ckCRM.NTSSetParam(oMenu, oApp.Tr(Me, 128345123273773647, "Movimenti CRM"), "S", "N")
      ckPM_2.NTSSetParam(oMenu, oApp.Tr(Me, 128345123273929897, "Struttura commesse Project mana&gement"), "S", "N")
      ckPM_1.NTSSetParam(oMenu, oApp.Tr(Me, 128345123274086147, "Movimenti avanzamento Pro&ject management"), "S", "N")
      ckPR.NTSSetParam(oMenu, oApp.Tr(Me, 128345123274242397, "&Provvigioni e progressivi agenti"), "S", "N")
      ckBUD.NTSSetParam(oMenu, oApp.Tr(Me, 128345123274398647, "&Budget di Contabilità Analitica e di Vendita"), "S", "N")
      ckOR.NTSSetParam(oMenu, oApp.Tr(Me, 128345123274554897, "&Ordini, Impegni, preventivi, proposte d'ordine e attività collegate, offerte CRM"), "S", "N")
      ckPRB.NTSSetParam(oMenu, oApp.Tr(Me, 128345123274711147, "&Note di prelievo"), "S", "N")
      ckMG.NTSSetParam(oMenu, oApp.Tr(Me, 128345123274867397, "Mo&vimenti  e progressivi di magazzino, dati Lifo, Collaudi e RNC"), "S", "N")
      ckCG_2.NTSSetParam(oMenu, oApp.Tr(Me, 128345123275023647, "Dati &Intra e Ritenute e Tesoreria"), "S", "N")
      ckCG_1.NTSSetParam(oMenu, oApp.Tr(Me, 128345123275179897, "&Movimenti contabili, IVA, scadenziario, progressivi di contabilità, progressivi IVA e movimenti di C.A. "), "S", "N")
      ckMovc.NTSSetParam(oMenu, oApp.Tr(Me, 128481733469950073, "Movimenti Cespiti"), "S", "N")
      ckAnac.NTSSetParam(oMenu, oApp.Tr(Me, 128481733470105990, "Anagrafiche Cespiti"), "S", "N")
      ckEmail.NTSSetParam(oMenu, oApp.Tr(Me, 128987042025507211, "Gestione Integrata E-mail"), "S", "N")
      ''non faccio lanciare lo zoom dal controllo visto che non devo cambiare il contenuto del campo ma riposizionare il dataset!!!

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
  Public Overridable Sub InitEntity(ByRef cleCedi As CLE__CEDI)
    oCleCedi = cleCedi
    AddHandler oCleCedi.RemoteEvent, AddressOf GestisciEventiEntity
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
  Public Overridable Sub FRM__ELDA_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------


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

  Public Overridable Sub cmdSelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelAll.Click
    Try
      ckCG_1.Checked = True
      ckCG_2.Checked = True
      ckMG.Checked = True
      ckMantieni.Checked = True
      ckPRB.Checked = True
      ckOR.Checked = True
      ckBUD.Checked = True
      ckPR.Checked = True
      ckPM_1.Checked = True
      ckPM_2.Checked = True
      ckCRM.Checked = True
      ckDWH.Checked = True
      ckStatven.Checked = True
      ckAs_1.Checked = True
      ckAS_2.Checked = True
      ckTT.Checked = True
      ckMovc.Checked = True
      ckAnac.Checked = True
      ckEmail.Checked = True
      ckScadEx.Checked = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdDesAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDesAll.Click
    Try
      ckCG_1.Checked = False
      ckCG_2.Checked = False
      ckMG.Checked = False
      ckMantieni.Checked = False
      ckPRB.Checked = False
      ckOR.Checked = False
      ckBUD.Checked = False
      ckPR.Checked = False
      ckPM_1.Checked = False
      ckPM_2.Checked = False
      ckCRM.Checked = False
      ckDWH.Checked = False
      ckStatven.Checked = False
      ckAs_1.Checked = False
      ckAS_2.Checked = False
      ckTT.Checked = False
      ckMovc.Checked = False
      ckAnac.Checked = False
      ckEmail.Checked = False
      ckScadEx.Checked = False
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
    Try
      '-----------------------------------------------------------------------------------------
      oCleCedi.bEldaCG_1 = CBool(ckCG_1.Checked)
      oCleCedi.bEldaCG_2 = CBool(ckCG_2.Checked)
      oCleCedi.bEldaMG = CBool(ckMG.Checked)
      oCleCedi.bEldaMantieni = CBool(ckMantieni.Checked)
      oCleCedi.bEldaPRB = CBool(ckPRB.Checked)
      oCleCedi.bEldaOR = CBool(ckOR.Checked)
      oCleCedi.bEldaBUD = CBool(ckBUD.Checked)
      oCleCedi.bEldaPR = CBool(ckPR.Checked)
      oCleCedi.bEldaPM_1 = CBool(ckPM_1.Checked)
      oCleCedi.bEldaPM_2 = CBool(ckPM_2.Checked)
      oCleCedi.bEldaCRM = CBool(ckCRM.Checked)
      oCleCedi.bEldaDWH = CBool(ckDWH.Checked)
      oCleCedi.bEldaStatistiche = CBool(ckStatven.Checked)
      oCleCedi.bEldaTT = CBool(ckTT.Checked)
      oCleCedi.bEldaAS_1 = CBool(ckAs_1.Checked)
      oCleCedi.bEldaAS_2 = CBool(ckAS_2.Checked)
      oCleCedi.bEldaMovc = CBool(ckMovc.Checked)
      oCleCedi.bEldaAnac = CBool(ckAnac.Checked)
      oCleCedi.bEldaEmail = CBool(ckEmail.Checked)
      oCleCedi.bEldaScadEx = CBool(ckScadEx.Checked)
      '-----------------------------------------------------------------------------------------
      '--- Se non è stato selezionato niente esce come se si fosse annullato
      '-----------------------------------------------------------------------------------------
      If (oCleCedi.bEldaCG_1 = False) And (oCleCedi.bEldaCG_2 = False) And (oCleCedi.bEldaMG = False) And _
         (oCleCedi.bEldaMantieni = False) And (oCleCedi.bEldaPRB = False) And (oCleCedi.bEldaOR = False) And _
         (oCleCedi.bEldaBUD = False) And (oCleCedi.bEldaPR = False) And (oCleCedi.bEldaPM_1 = False) And _
         (oCleCedi.bEldaPM_2 = False) And (oCleCedi.bEldaCRM = False) And (oCleCedi.bEldaDWH = False) And _
         (oCleCedi.bEldaTT = False) And (ckAs_1.Checked = False) And (ckAS_2.Checked = False) And _
         (oCleCedi.bEldaMovc = False) And (oCleCedi.bEldaAnac = False) And (oCleCedi.bEldaEmail = False) And _
         (oCleCedi.bEldaScadEx = False) And (oCleCedi.bEldaStatistiche = False) Then
        oCleCedi.bEldaAnnullato = True
      Else
        oCleCedi.bEldaAnnullato = False
      End If
      '-----------------------------------------------------------------------------------------
      oCleCedi.strEldaDesogglog = _
        "          . Movimenti contabili, Iva, scadenziario, progressivi, ecc...........: " & NTSCStr(IIf(ckCG_1.Checked = True, "'Sì'", "'No'")) & vbCrLf & _
        "          . Dati Intra e Ritenute e Tesoreria..................................: " & NTSCStr(IIf(ckCG_2.Checked = True, "'Sì'", "'No'")) & vbCrLf & _
        "          . Movimenti e progressivi di magazzino, dati Lifo, Collaudi e RNC....: " & NTSCStr(IIf(ckMG.Checked = True, "'Sì'", "'No'")) & vbCrLf & _
        "          . Mantieni ultimi costi d'acquisto...................................: " & NTSCStr(IIf(ckMantieni.Checked = True, "'Sì'", "'No'")) & vbCrLf & _
        "          . Note di prelievo...................................................: " & NTSCStr(IIf(ckPRB.Checked = True, "'Sì'", "'No'")) & vbCrLf & _
        "          . Ordini, Impegni, preventivi, proposte d'ordine e attività collegate: " & NTSCStr(IIf(ckOR.Checked = True, "'Sì'", "'No'")) & vbCrLf & _
        "          . Budget di Contabilità Analitica e di Vendita.......................: " & NTSCStr(IIf(ckBUD.Checked = True, "'Sì'", "'No'")) & vbCrLf & _
        "          . Provvigioni e progressivi agenti...................................: " & NTSCStr(IIf(ckPR.Checked = True, "'Sì'", "'No'")) & vbCrLf & _
        "          . Movimenti avanzamento Project management...........................: " & NTSCStr(IIf(ckPM_1.Checked = True, "'Sì'", "'No'")) & vbCrLf & _
        "          . Struttura commesse Project management..............................: " & NTSCStr(IIf(ckPM_2.Checked = True, "'Sì'", "'No'")) & vbCrLf & _
        "          . Movimenti CRM......................................................: " & NTSCStr(IIf(ckCRM.Checked = True, "'Sì'", "'No'")) & vbCrLf & _
        "          . Datawarehouse......................................................: " & NTSCStr(IIf(ckDWH.Checked = True, "'Sì'", "'No'")) & vbCrLf & _
        "          . Statistiche (vecchio modulo) ......................................: " & NTSCStr(IIf(ckStatven.Checked = True, "'Sì'", "'No'")) & vbCrLf & _
        "          . Customer Service (Contratti/matricole/problemi-soluzioni)..........: " & NTSCStr(IIf(ckAs_1.Checked = True, "'Sì'", "'No'")) & vbCrLf & _
        "          . Customer Service (Chiamate/attività/ricambi).......................: " & NTSCStr(IIf(ckAS_2.Checked = True, "'Sì'", "'No'")) & vbCrLf & _
        "          . Tabelle Temporanee.................................................: " & NTSCStr(IIf(ckTT.Checked = True, "'Sì'", "'No'")) & vbCrLf & _
        "          . Anagrafiche Cespiti................................................: " & NTSCStr(IIf(ckAnac.Checked = True, "'Sì'", "'No'")) & vbCrLf & _
        "          . Movimenti Cespiti..................................................: " & NTSCStr(IIf(ckMovc.Checked = True, "'Sì'", "'No'")) & vbCrLf & _
        "          . Scadenziario extra-contabile.......................................: " & NTSCStr(IIf(ckScadEx.Checked = True, "'Sì'", "'No'")) & vbCrLf
      '-----------------------------------------------------------------------------------------
      Me.Close()
      '-----------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      oCleCedi.bEldaAnnullato = True
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

End Class