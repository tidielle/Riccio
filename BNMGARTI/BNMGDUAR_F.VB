Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGDUAR_F
  Public oCallParams As CLE__CLDP
  Public oCleArti As CLEMGARTI
  Public bOk As Boolean = False
  Public bNew As Boolean = False

  Public lProgr As Integer
  'Public nLungRoot As Integer
  'Public nLungExt As Integer
  'Public strGeneraCodice As String
  Public gbDuarArticoloGenerato As Boolean
  Public glDuarProgr As Integer
  Public gstrDuarRoot As String

  Public dPerqtaOrigine As Decimal = 0
  Public bOnLoading As Boolean = False

  Public WithEvents pnNuovo As NTSInformatica.NTSPanel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents lbCodart As NTSInformatica.NTSLabel
  Public WithEvents ckArtval As NTSInformatica.NTSCheckBox
  Public WithEvents ckProvvigioni As NTSInformatica.NTSCheckBox
  Public WithEvents ckSconti As NTSInformatica.NTSCheckBox
  Public WithEvents ckListini As NTSInformatica.NTSCheckBox
  Public WithEvents ckKit As NTSInformatica.NTSCheckBox
  Public WithEvents fmCheck As NTSInformatica.NTSGroupBox
  Public WithEvents edCodart As NTSInformatica.NTSTextBoxStr
  Public WithEvents fmUnitamisura As NTSInformatica.NTSGroupBox
  Public WithEvents edAr_unmis As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAr_unmis As NTSInformatica.NTSLabel
  Public WithEvents lbAr_unmis2 As NTSInformatica.NTSLabel
  Public WithEvents edAr_confez2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAr_unmis2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAr_confez2 As NTSInformatica.NTSLabel
  Public WithEvents fmNuovoArt As NTSInformatica.NTSGroupBox
  Public WithEvents lbAr_perqta As NTSInformatica.NTSLabel
  Public WithEvents edAr_perqta As NTSInformatica.NTSTextBoxNum
  Public WithEvents cmdOk As NTSInformatica.NTSButton

  Public Overridable Sub InitializeComponent()
    Me.pnNuovo = New NTSInformatica.NTSPanel
    Me.fmNuovoArt = New NTSInformatica.NTSGroupBox
    Me.lbAr_perqta = New NTSInformatica.NTSLabel
    Me.edAr_perqta = New NTSInformatica.NTSTextBoxNum
    Me.fmUnitamisura = New NTSInformatica.NTSGroupBox
    Me.edAr_unmis = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_unmis = New NTSInformatica.NTSLabel
    Me.lbAr_unmis2 = New NTSInformatica.NTSLabel
    Me.edAr_confez2 = New NTSInformatica.NTSTextBoxStr
    Me.edAr_unmis2 = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_confez2 = New NTSInformatica.NTSLabel
    Me.edCodart = New NTSInformatica.NTSTextBoxStr
    Me.fmCheck = New NTSInformatica.NTSGroupBox
    Me.ckArtval = New NTSInformatica.NTSCheckBox
    Me.ckKit = New NTSInformatica.NTSCheckBox
    Me.ckListini = New NTSInformatica.NTSCheckBox
    Me.ckSconti = New NTSInformatica.NTSCheckBox
    Me.ckProvvigioni = New NTSInformatica.NTSCheckBox
    Me.lbCodart = New NTSInformatica.NTSLabel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    CType(Me.pnNuovo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnNuovo.SuspendLayout()
    CType(Me.fmNuovoArt, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmNuovoArt.SuspendLayout()
    CType(Me.edAr_perqta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmUnitamisura, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmUnitamisura.SuspendLayout()
    CType(Me.edAr_unmis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_confez2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_unmis2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmCheck, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmCheck.SuspendLayout()
    CType(Me.ckArtval.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckKit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckListini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSconti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckProvvigioni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'pnNuovo
    '
    Me.pnNuovo.AllowDrop = True
    Me.pnNuovo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnNuovo.Appearance.Options.UseBackColor = True
    Me.pnNuovo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnNuovo.Controls.Add(Me.fmNuovoArt)
    Me.pnNuovo.Controls.Add(Me.fmUnitamisura)
    Me.pnNuovo.Controls.Add(Me.edCodart)
    Me.pnNuovo.Controls.Add(Me.fmCheck)
    Me.pnNuovo.Controls.Add(Me.lbCodart)
    Me.pnNuovo.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnNuovo.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnNuovo.Location = New System.Drawing.Point(0, 0)
    Me.pnNuovo.Name = "pnNuovo"
    Me.pnNuovo.NTSActiveTrasparency = True
    Me.pnNuovo.Size = New System.Drawing.Size(412, 229)
    Me.pnNuovo.TabIndex = 12
    Me.pnNuovo.Text = "NtsPanel1"
    '
    'fmNuovoArt
    '
    Me.fmNuovoArt.AllowDrop = True
    Me.fmNuovoArt.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmNuovoArt.Appearance.Options.UseBackColor = True
    Me.fmNuovoArt.Controls.Add(Me.lbAr_perqta)
    Me.fmNuovoArt.Controls.Add(Me.edAr_perqta)
    Me.fmNuovoArt.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmNuovoArt.Location = New System.Drawing.Point(12, 159)
    Me.fmNuovoArt.Name = "fmNuovoArt"
    Me.fmNuovoArt.Size = New System.Drawing.Size(393, 57)
    Me.fmNuovoArt.TabIndex = 679
    Me.fmNuovoArt.Text = "DATI NUOVO ARTICOLO"
    '
    'lbAr_perqta
    '
    Me.lbAr_perqta.AutoSize = True
    Me.lbAr_perqta.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_perqta.Location = New System.Drawing.Point(9, 28)
    Me.lbAr_perqta.Name = "lbAr_perqta"
    Me.lbAr_perqta.NTSDbField = ""
    Me.lbAr_perqta.Size = New System.Drawing.Size(149, 13)
    Me.lbAr_perqta.TabIndex = 684
    Me.lbAr_perqta.Text = "Moltiplicatore quantità/prezzo"
    Me.lbAr_perqta.Tooltip = ""
    Me.lbAr_perqta.UseMnemonic = False
    '
    'edAr_perqta
    '
    Me.edAr_perqta.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_perqta.EditValue = "0"
    Me.edAr_perqta.Location = New System.Drawing.Point(193, 25)
    Me.edAr_perqta.Name = "edAr_perqta"
    Me.edAr_perqta.NTSDbField = ""
    Me.edAr_perqta.NTSFormat = "0"
    Me.edAr_perqta.NTSForzaVisZoom = False
    Me.edAr_perqta.NTSOldValue = ""
    Me.edAr_perqta.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_perqta.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_perqta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_perqta.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_perqta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_perqta.Properties.MaxLength = 65536
    Me.edAr_perqta.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_perqta.Size = New System.Drawing.Size(181, 20)
    Me.edAr_perqta.TabIndex = 685
    '
    'fmUnitamisura
    '
    Me.fmUnitamisura.AllowDrop = True
    Me.fmUnitamisura.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmUnitamisura.Appearance.Options.UseBackColor = True
    Me.fmUnitamisura.Controls.Add(Me.edAr_unmis)
    Me.fmUnitamisura.Controls.Add(Me.lbAr_unmis)
    Me.fmUnitamisura.Controls.Add(Me.lbAr_unmis2)
    Me.fmUnitamisura.Controls.Add(Me.edAr_confez2)
    Me.fmUnitamisura.Controls.Add(Me.edAr_unmis2)
    Me.fmUnitamisura.Controls.Add(Me.lbAr_confez2)
    Me.fmUnitamisura.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmUnitamisura.Location = New System.Drawing.Point(230, 62)
    Me.fmUnitamisura.Name = "fmUnitamisura"
    Me.fmUnitamisura.Size = New System.Drawing.Size(175, 91)
    Me.fmUnitamisura.TabIndex = 676
    Me.fmUnitamisura.Text = "U.M. NUOVO ARTICOLO"
    '
    'edAr_unmis
    '
    Me.edAr_unmis.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_unmis.Location = New System.Drawing.Point(99, 22)
    Me.edAr_unmis.Name = "edAr_unmis"
    Me.edAr_unmis.NTSDbField = ""
    Me.edAr_unmis.NTSForzaVisZoom = False
    Me.edAr_unmis.NTSOldValue = ""
    Me.edAr_unmis.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_unmis.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_unmis.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_unmis.Properties.MaxLength = 65536
    Me.edAr_unmis.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_unmis.Size = New System.Drawing.Size(57, 20)
    Me.edAr_unmis.TabIndex = 505
    '
    'lbAr_unmis
    '
    Me.lbAr_unmis.AutoSize = True
    Me.lbAr_unmis.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_unmis.Location = New System.Drawing.Point(14, 25)
    Me.lbAr_unmis.Name = "lbAr_unmis"
    Me.lbAr_unmis.NTSDbField = ""
    Me.lbAr_unmis.Size = New System.Drawing.Size(52, 13)
    Me.lbAr_unmis.TabIndex = 15
    Me.lbAr_unmis.Text = "Principale"
    Me.lbAr_unmis.Tooltip = ""
    Me.lbAr_unmis.UseMnemonic = False
    '
    'lbAr_unmis2
    '
    Me.lbAr_unmis2.AutoSize = True
    Me.lbAr_unmis2.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_unmis2.Location = New System.Drawing.Point(14, 69)
    Me.lbAr_unmis2.Name = "lbAr_unmis2"
    Me.lbAr_unmis2.NTSDbField = ""
    Me.lbAr_unmis2.Size = New System.Drawing.Size(60, 13)
    Me.lbAr_unmis2.TabIndex = 16
    Me.lbAr_unmis2.Text = "Secondaria"
    Me.lbAr_unmis2.Tooltip = ""
    Me.lbAr_unmis2.UseMnemonic = False
    '
    'edAr_confez2
    '
    Me.edAr_confez2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_confez2.Location = New System.Drawing.Point(99, 44)
    Me.edAr_confez2.Name = "edAr_confez2"
    Me.edAr_confez2.NTSDbField = ""
    Me.edAr_confez2.NTSForzaVisZoom = False
    Me.edAr_confez2.NTSOldValue = ""
    Me.edAr_confez2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_confez2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_confez2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_confez2.Properties.MaxLength = 65536
    Me.edAr_confez2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_confez2.Size = New System.Drawing.Size(57, 20)
    Me.edAr_confez2.TabIndex = 508
    '
    'edAr_unmis2
    '
    Me.edAr_unmis2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_unmis2.Location = New System.Drawing.Point(99, 66)
    Me.edAr_unmis2.Name = "edAr_unmis2"
    Me.edAr_unmis2.NTSDbField = ""
    Me.edAr_unmis2.NTSForzaVisZoom = False
    Me.edAr_unmis2.NTSOldValue = ""
    Me.edAr_unmis2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_unmis2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_unmis2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_unmis2.Properties.MaxLength = 65536
    Me.edAr_unmis2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_unmis2.Size = New System.Drawing.Size(57, 20)
    Me.edAr_unmis2.TabIndex = 506
    '
    'lbAr_confez2
    '
    Me.lbAr_confez2.AutoSize = True
    Me.lbAr_confez2.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_confez2.Location = New System.Drawing.Point(14, 47)
    Me.lbAr_confez2.Name = "lbAr_confez2"
    Me.lbAr_confez2.NTSDbField = ""
    Me.lbAr_confez2.Size = New System.Drawing.Size(61, 13)
    Me.lbAr_confez2.TabIndex = 18
    Me.lbAr_confez2.Text = "Confezione"
    Me.lbAr_confez2.Tooltip = ""
    Me.lbAr_confez2.UseMnemonic = False
    '
    'edCodart
    '
    Me.edCodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodart.Location = New System.Drawing.Point(151, 5)
    Me.edCodart.Name = "edCodart"
    Me.edCodart.NTSDbField = ""
    Me.edCodart.NTSForzaVisZoom = False
    Me.edCodart.NTSOldValue = ""
    Me.edCodart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodart.Properties.MaxLength = 65536
    Me.edCodart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodart.Size = New System.Drawing.Size(254, 20)
    Me.edCodart.TabIndex = 13
    '
    'fmCheck
    '
    Me.fmCheck.AllowDrop = True
    Me.fmCheck.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmCheck.Appearance.Options.UseBackColor = True
    Me.fmCheck.Controls.Add(Me.ckArtval)
    Me.fmCheck.Controls.Add(Me.ckKit)
    Me.fmCheck.Controls.Add(Me.ckListini)
    Me.fmCheck.Controls.Add(Me.ckSconti)
    Me.fmCheck.Controls.Add(Me.ckProvvigioni)
    Me.fmCheck.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmCheck.Location = New System.Drawing.Point(12, 31)
    Me.fmCheck.Name = "fmCheck"
    Me.fmCheck.Size = New System.Drawing.Size(212, 122)
    Me.fmCheck.TabIndex = 675
    Me.fmCheck.Text = "OPZIONI"
    '
    'ckArtval
    '
    Me.ckArtval.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckArtval.Location = New System.Drawing.Point(12, 80)
    Me.ckArtval.Name = "ckArtval"
    Me.ckArtval.NTSCheckValue = "S"
    Me.ckArtval.NTSUnCheckValue = "N"
    Me.ckArtval.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckArtval.Properties.Appearance.Options.UseBackColor = True
    Me.ckArtval.Properties.Caption = "Duplica &descrizioni in lingua"
    Me.ckArtval.Size = New System.Drawing.Size(153, 19)
    Me.ckArtval.TabIndex = 667
    '
    'ckKit
    '
    Me.ckKit.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckKit.Location = New System.Drawing.Point(12, 100)
    Me.ckKit.Name = "ckKit"
    Me.ckKit.NTSCheckValue = "S"
    Me.ckKit.NTSUnCheckValue = "N"
    Me.ckKit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckKit.Properties.Appearance.Options.UseBackColor = True
    Me.ckKit.Properties.Caption = "Duplica figli del &KIT"
    Me.ckKit.Size = New System.Drawing.Size(114, 19)
    Me.ckKit.TabIndex = 668
    '
    'ckListini
    '
    Me.ckListini.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckListini.Location = New System.Drawing.Point(12, 20)
    Me.ckListini.Name = "ckListini"
    Me.ckListini.NTSCheckValue = "S"
    Me.ckListini.NTSUnCheckValue = "N"
    Me.ckListini.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckListini.Properties.Appearance.Options.UseBackColor = True
    Me.ckListini.Properties.Caption = "Duplica listini"
    Me.ckListini.Size = New System.Drawing.Size(90, 19)
    Me.ckListini.TabIndex = 670
    '
    'ckSconti
    '
    Me.ckSconti.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSconti.Location = New System.Drawing.Point(12, 40)
    Me.ckSconti.Name = "ckSconti"
    Me.ckSconti.NTSCheckValue = "S"
    Me.ckSconti.NTSUnCheckValue = "N"
    Me.ckSconti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSconti.Properties.Appearance.Options.UseBackColor = True
    Me.ckSconti.Properties.Caption = "Duplica sconti"
    Me.ckSconti.Size = New System.Drawing.Size(90, 19)
    Me.ckSconti.TabIndex = 671
    '
    'ckProvvigioni
    '
    Me.ckProvvigioni.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.ckProvvigioni.Location = New System.Drawing.Point(12, 60)
    Me.ckProvvigioni.Name = "ckProvvigioni"
    Me.ckProvvigioni.NTSCheckValue = "S"
    Me.ckProvvigioni.NTSUnCheckValue = "N"
    Me.ckProvvigioni.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckProvvigioni.Properties.Appearance.Options.UseBackColor = True
    Me.ckProvvigioni.Properties.Caption = "Duplica provvigioni"
    Me.ckProvvigioni.Size = New System.Drawing.Size(114, 19)
    Me.ckProvvigioni.TabIndex = 672
    '
    'lbCodart
    '
    Me.lbCodart.AutoSize = True
    Me.lbCodart.BackColor = System.Drawing.Color.Transparent
    Me.lbCodart.Location = New System.Drawing.Point(21, 8)
    Me.lbCodart.Name = "lbCodart"
    Me.lbCodart.NTSDbField = ""
    Me.lbCodart.Size = New System.Drawing.Size(110, 13)
    Me.lbCodart.TabIndex = 582
    Me.lbCodart.Text = "Codice nuovo articolo"
    Me.lbCodart.Tooltip = ""
    Me.lbCodart.UseMnemonic = False
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(418, 33)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(136, 26)
    Me.cmdAnnulla.TabIndex = 10
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(418, 5)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(136, 26)
    Me.cmdOk.TabIndex = 11
    Me.cmdOk.Text = "&Conferma"
    '
    'FRMMGDUAR_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(562, 229)
    Me.Controls.Add(Me.pnNuovo)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.HelpContext = ""
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMMGDUAR_F"
    Me.Text = "DUPLICAZIONE ARTICOLO"
    CType(Me.pnNuovo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnNuovo.ResumeLayout(False)
    Me.pnNuovo.PerformLayout()
    CType(Me.fmNuovoArt, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmNuovoArt.ResumeLayout(False)
    Me.fmNuovoArt.PerformLayout()
    CType(Me.edAr_perqta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmUnitamisura, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmUnitamisura.ResumeLayout(False)
    Me.fmUnitamisura.PerformLayout()
    CType(Me.edAr_unmis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_confez2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_unmis2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmCheck, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmCheck.ResumeLayout(False)
    Me.fmCheck.PerformLayout()
    CType(Me.ckArtval.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckKit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckListini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSconti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckProvvigioni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      ckArtval.NTSSetParam(oMenu, oApp.Tr(Me, 128560093887428691, "Duplica &descrizioni in lingua"), "S", "N")
      ckKit.NTSSetParam(oMenu, oApp.Tr(Me, 128560093887584881, "Duplica figli del &Kit"), "S", "N")
      ckListini.NTSSetParam(oMenu, oApp.Tr(Me, 128560093887741071, "Duplica listini"), "S", "N")
      ckSconti.NTSSetParam(oMenu, oApp.Tr(Me, 128560093888053451, "Duplica sconti"), "S", "N")
      ckProvvigioni.NTSSetParam(oMenu, oApp.Tr(Me, 128560093888209641, "Duplica provvigioni"), "S", "N")
      edCodart.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788390849006481, "Codice nuovo articolo"), tabartico, False)
      edAr_perqta.NTSSetParam(oMenu, oApp.Tr(Me, 130385780778266653, "Molt.qtà/prezzo:"), "0", 12, 0, 999999999999)
      edAr_unmis.NTSSetParam(oMenu, oApp.Tr(Me, 130385777995141653, "Un. misura principale"), 3, True)
      edAr_confez2.NTSSetParam(oMenu, oApp.Tr(Me, 130385779556547903, "Un. misura confezione:"), 3, True)
      edAr_unmis2.NTSSetParam(oMenu, oApp.Tr(Me, 130385778913422903, "Un. misura secondaria:"), 3, True)

      If oCleArti.bGestTabUnmis = True Then
        edAr_unmis.NTSSetParamZoom("ZOOMTABUMIS")
        edAr_confez2.NTSSetParamZoom("ZOOMTABUMIS")
        edAr_unmis2.NTSSetParamZoom("ZOOMTABUMIS")
      End If
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
  Public Overridable Sub FRMMGDUAR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim dsTmp As DataSet = Nothing
    Try
      AddHandler oCleArti.RemoteEvent, AddressOf GestisciEventiEntity

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      bOk = False
      '----------------------------------------------------------------------------------------
      '--- Se non esiste almeno la tipologia 1 chiude
      '----------------------------------------------------------------------------------------
      If Not oCleArti.GetTabTipa1() Then Me.Close()
      '----------------------------------------------------------------------------------------

      oCleArti.strGeneraCodice = oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "GeneraCodice", "0", " ", "0")
      oCleArti.nLungRoot = NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "GeneraCodiceLungRoot", "0", " ", "0"))
      oCleArti.nLungExt = NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "GeneraCodiceLungExt", "0", " ", "0"))
      gbDuarArticoloGenerato = False
      lProgr = 0
      glDuarProgr = 0
      gstrDuarRoot = ""

      '--------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
      GctlApplicaDefaultValue()

      ckListini.Checked = oCleArti.bDuplicaListini OrElse (edCodart.Text.Trim <> "")
      ckSconti.Checked = oCleArti.bDuplicaSconti OrElse (edCodart.Text.Trim <> "")
      ckProvvigioni.Checked = oCleArti.bDuplicaProvvigioni OrElse (edCodart.Text.Trim <> "")
      ckArtval.Checked = oCleArti.bDuplicaDescrLingua
      ckKit.Checked = oCleArti.bDuplicaKit

      oCleArti.GetUnitaMisDuar()
      If Not oCleArti.dtrTmpDuar Is Nothing Then
        edAr_unmis.Text = NTSCStr(oCleArti.dtrTmpDuar!ar_unmis)
        edAr_confez2.Text = NTSCStr(oCleArti.dtrTmpDuar!ar_confez2)
        edAr_unmis2.Text = NTSCStr(oCleArti.dtrTmpDuar!ar_unmis2)
        edAr_perqta.Text = NTSCStr(oCleArti.dtrTmpDuar!ar_perqta)
      End If

      bOnLoading = True
      dPerqtaOrigine = NTSCDec(edAr_perqta.Text)
      If dPerqtaOrigine = 0 Then dPerqtaOrigine = 1
      If ckListini.Checked = True Then edAr_perqta.Enabled = False
      bOnLoading = False

      If edCodart.Text.Trim <> "" Then cmdOk_Click(Me, Nothing)
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMMGDUAR_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Dim ctrlTmp As Control = Nothing
    Try
      '--------------------------------------------
      'gestione dello zoom:
      'eseguo la Zoom, tanto se per il controllo non deve venir eseguito uno zoom particolare, la routine non fa nulla e viene processato lo zoom standard del controllo, settato con la NTSSetParamZoom
      If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
        Zoom()
        e.Handled = True    'altrimenti anche il controllo riceve l'F5 e la routine ZOOM viene eseguita 2 volte!!!
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Zoom / ALT+F3 / ALT+F3"
  Public Overridable Sub Zoom()
    Dim ctlLastControl As Control
    Dim ctrlTmp As Control = Nothing
    Dim oParam As New CLE__PATB

    Try
      ctlLastControl = NTSFindControlFocused(Me)
      If ctlLastControl Is Nothing Then Return

      '------------------------------------
      'zoom standard di textbox e griglia
      'SendKeys.SendWait("{F5}") 'se faccio questa riga va in un loop infinito....
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
#End Region

#Region "Validazione articoli"
  Public Overridable Sub edCodart_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCodart.Validated
    Try
      If Not CheckCodartDuar() Then Exit Sub
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      edCodart.Text = UCase(edCodart.Text).TrimEnd
      If Not CheckCodartDuar() Then Exit Sub
      If Not oCleArti.CheckArticoloDuar(edCodart.Text, edAr_unmis.Text, edAr_confez2.Text, edAr_unmis2.Text, _
                                        edAr_perqta.Text, "1", "N", "N", "N", "N", "N", "", "") Then
        Exit Sub
      Else
        oCleArti.strCodartDuar = edCodart.Text
        bOk = True
        Me.Close()
      End If

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

  Public Overridable Function CheckCodartDuar() As Boolean
    Try
      Return oCleArti.CheckCodartDuar(edCodart.Text)

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub edAr_unmis_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edAr_unmis.Validated
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleArti.bGestTabUnmis = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      If edAr_unmis.Text = "" Then Return
      '--------------------------------------------------------------------------------------------------------------
      If oMenu.ValCodiceDb(edAr_unmis.Text, DittaCorrente, "TABUMIS", "S", "", dttTmp) = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129428391125683594, "Attenzione!" & vbCrLf & _
          "Unità di misura Principale non valida."))
        edAr_unmis.Text = ""
        Return
      Else
        If NTSCStr(dttTmp.Rows(0)!tb_codumis) <> edAr_unmis.Text Then edAr_unmis.Text = NTSCStr(dttTmp.Rows(0)!tb_codumis)
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Sub

  Public Overridable Sub edAr_confez2_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edAr_confez2.Validated
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleArti.bGestTabUnmis = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      If edAr_confez2.Text = "" Then Return
      '--------------------------------------------------------------------------------------------------------------
      If oMenu.ValCodiceDb(edAr_confez2.Text, DittaCorrente, "TABUMIS", "S", "", dttTmp) = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129428395058203125, "Attenzione!" & vbCrLf & _
          "Unità di misura Confezione non valida."))
        edAr_confez2.Text = ""
        Return
      Else
        If NTSCStr(dttTmp.Rows(0)!tb_codumis) <> edAr_confez2.Text Then edAr_confez2.Text = NTSCStr(dttTmp.Rows(0)!tb_codumis)
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Sub

  Public Overridable Sub edAr_unmis2_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edAr_unmis2.Validated
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleArti.bGestTabUnmis = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      If edAr_unmis2.Text = "" Then Return
      '--------------------------------------------------------------------------------------------------------------
      If oMenu.ValCodiceDb(edAr_unmis2.Text, DittaCorrente, "TABUMIS", "S", "", dttTmp) = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129428395516464844, "Attenzione!" & vbCrLf & _
          "Unità di misura Secondaria non valida."))
        edAr_unmis2.Text = ""
        Return
      Else
        If NTSCStr(dttTmp.Rows(0)!tb_codumis) <> edAr_unmis2.Text Then edAr_unmis2.Text = NTSCStr(dttTmp.Rows(0)!tb_codumis)
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Sub

  Public Overridable Sub ckListini_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckListini.CheckedChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bOnLoading = True Then Return
      '--------------------------------------------------------------------------------------------------------------
      If ckListini.Checked = True Then
        edAr_perqta.Text = dPerqtaOrigine.ToString
        edAr_perqta.Enabled = False
      Else
        GctlSetVisEnab(edAr_perqta, False)
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class