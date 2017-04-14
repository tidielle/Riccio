Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGNARV

#Region "Variabili"
  Public oCleArtv As CLEMGARTV
  Public oCallParams As CLE__CLDP
  Private components As System.ComponentModel.IContainer

  Public bNarvAnnullato As Boolean

  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents edCodvari2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edCodvari1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbLungvar3 As NTSInformatica.NTSLabel
  Public WithEvents edLungvar3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbLungvar2 As NTSInformatica.NTSLabel
  Public WithEvents edLungvar2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbLungvar1 As NTSInformatica.NTSLabel
  Public WithEvents edLungvar1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbLungroot As NTSInformatica.NTSLabel
  Public WithEvents edLungroot As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbNumliv As NTSInformatica.NTSLabel
  Public WithEvents edNumliv As NTSInformatica.NTSTextBoxNum
  Public WithEvents fmPrezzi As NTSInformatica.NTSGroupBox
  Public WithEvents lbCodroot As NTSInformatica.NTSLabel
  Public WithEvents edCodroot As NTSInformatica.NTSTextBoxStr
  Public WithEvents edCodvari3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents opPrevar2 As NTSInformatica.NTSRadioButton
  Public WithEvents opPrevar1 As NTSInformatica.NTSRadioButton
  Public WithEvents opPrevar0 As NTSInformatica.NTSRadioButton
  Public WithEvents pnNarv As NTSInformatica.NTSPanel

#End Region

  Public Overridable Sub InitializeComponent()
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.pnNarv = New NTSInformatica.NTSPanel
    Me.fmPrezzi = New NTSInformatica.NTSGroupBox
    Me.opPrevar2 = New NTSInformatica.NTSRadioButton
    Me.opPrevar1 = New NTSInformatica.NTSRadioButton
    Me.opPrevar0 = New NTSInformatica.NTSRadioButton
    Me.lbCodroot = New NTSInformatica.NTSLabel
    Me.edCodroot = New NTSInformatica.NTSTextBoxStr
    Me.edCodvari3 = New NTSInformatica.NTSTextBoxStr
    Me.lbLungvar3 = New NTSInformatica.NTSLabel
    Me.edLungvar3 = New NTSInformatica.NTSTextBoxNum
    Me.lbLungvar2 = New NTSInformatica.NTSLabel
    Me.edLungvar2 = New NTSInformatica.NTSTextBoxNum
    Me.lbLungvar1 = New NTSInformatica.NTSLabel
    Me.edLungvar1 = New NTSInformatica.NTSTextBoxNum
    Me.lbLungroot = New NTSInformatica.NTSLabel
    Me.edLungroot = New NTSInformatica.NTSTextBoxNum
    Me.lbNumliv = New NTSInformatica.NTSLabel
    Me.edNumliv = New NTSInformatica.NTSTextBoxNum
    Me.edCodvari2 = New NTSInformatica.NTSTextBoxStr
    Me.edCodvari1 = New NTSInformatica.NTSTextBoxStr
    CType(Me.pnNarv, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnNarv.SuspendLayout()
    CType(Me.fmPrezzi, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmPrezzi.SuspendLayout()
    CType(Me.opPrevar2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opPrevar1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opPrevar0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodroot.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodvari3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edLungvar3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edLungvar2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edLungvar1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edLungroot.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumliv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodvari2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodvari1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.cmdAnnulla.Location = New System.Drawing.Point(431, 44)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(103, 26)
    Me.cmdAnnulla.TabIndex = 14
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(431, 12)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(103, 26)
    Me.cmdOk.TabIndex = 12
    Me.cmdOk.Text = "&Ok"
    '
    'pnNarv
    '
    Me.pnNarv.AllowDrop = True
    Me.pnNarv.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnNarv.Appearance.Options.UseBackColor = True
    Me.pnNarv.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnNarv.Controls.Add(Me.fmPrezzi)
    Me.pnNarv.Controls.Add(Me.lbCodroot)
    Me.pnNarv.Controls.Add(Me.edCodroot)
    Me.pnNarv.Controls.Add(Me.edCodvari3)
    Me.pnNarv.Controls.Add(Me.lbLungvar3)
    Me.pnNarv.Controls.Add(Me.edLungvar3)
    Me.pnNarv.Controls.Add(Me.lbLungvar2)
    Me.pnNarv.Controls.Add(Me.edLungvar2)
    Me.pnNarv.Controls.Add(Me.lbLungvar1)
    Me.pnNarv.Controls.Add(Me.edLungvar1)
    Me.pnNarv.Controls.Add(Me.lbLungroot)
    Me.pnNarv.Controls.Add(Me.edLungroot)
    Me.pnNarv.Controls.Add(Me.lbNumliv)
    Me.pnNarv.Controls.Add(Me.edNumliv)
    Me.pnNarv.Controls.Add(Me.edCodvari2)
    Me.pnNarv.Controls.Add(Me.edCodvari1)
    Me.pnNarv.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnNarv.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnNarv.Location = New System.Drawing.Point(0, 0)
    Me.pnNarv.Name = "pnNarv"
    Me.pnNarv.Size = New System.Drawing.Size(425, 275)
    Me.pnNarv.TabIndex = 22
    Me.pnNarv.Text = "NtsPanel1"
    '
    'fmPrezzi
    '
    Me.fmPrezzi.AllowDrop = True
    Me.fmPrezzi.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmPrezzi.Appearance.Options.UseBackColor = True
    Me.fmPrezzi.Controls.Add(Me.opPrevar2)
    Me.fmPrezzi.Controls.Add(Me.opPrevar1)
    Me.fmPrezzi.Controls.Add(Me.opPrevar0)
    Me.fmPrezzi.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmPrezzi.Location = New System.Drawing.Point(15, 179)
    Me.fmPrezzi.Name = "fmPrezzi"
    Me.fmPrezzi.Size = New System.Drawing.Size(207, 84)
    Me.fmPrezzi.TabIndex = 17
    '
    'opPrevar2
    '
    Me.opPrevar2.Cursor = System.Windows.Forms.Cursors.Default
    Me.opPrevar2.Location = New System.Drawing.Point(5, 61)
    Me.opPrevar2.Name = "opPrevar2"
    Me.opPrevar2.NTSCheckValue = "S"
    Me.opPrevar2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opPrevar2.Properties.Appearance.Options.UseBackColor = True
    Me.opPrevar2.Properties.Caption = "Pr&ezzi diversi 3 varianti"
    Me.opPrevar2.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opPrevar2.Size = New System.Drawing.Size(133, 19)
    Me.opPrevar2.TabIndex = 2
    '
    'opPrevar1
    '
    Me.opPrevar1.Cursor = System.Windows.Forms.Cursors.Default
    Me.opPrevar1.Location = New System.Drawing.Point(5, 42)
    Me.opPrevar1.Name = "opPrevar1"
    Me.opPrevar1.NTSCheckValue = "S"
    Me.opPrevar1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opPrevar1.Properties.Appearance.Options.UseBackColor = True
    Me.opPrevar1.Properties.Caption = "P&rezzi diversi 1a variante"
    Me.opPrevar1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opPrevar1.Size = New System.Drawing.Size(145, 19)
    Me.opPrevar1.TabIndex = 1
    '
    'opPrevar0
    '
    Me.opPrevar0.Cursor = System.Windows.Forms.Cursors.Default
    Me.opPrevar0.EditValue = True
    Me.opPrevar0.Location = New System.Drawing.Point(5, 23)
    Me.opPrevar0.Name = "opPrevar0"
    Me.opPrevar0.NTSCheckValue = "S"
    Me.opPrevar0.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opPrevar0.Properties.Appearance.Options.UseBackColor = True
    Me.opPrevar0.Properties.Caption = "&Prezzi comuni"
    Me.opPrevar0.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opPrevar0.Size = New System.Drawing.Size(94, 19)
    Me.opPrevar0.TabIndex = 0
    '
    'lbCodroot
    '
    Me.lbCodroot.AutoSize = True
    Me.lbCodroot.BackColor = System.Drawing.Color.Transparent
    Me.lbCodroot.Location = New System.Drawing.Point(12, 145)
    Me.lbCodroot.Name = "lbCodroot"
    Me.lbCodroot.NTSDbField = ""
    Me.lbCodroot.Size = New System.Drawing.Size(77, 13)
    Me.lbCodroot.TabIndex = 16
    Me.lbCodroot.Text = "Codice articolo"
    Me.lbCodroot.Tooltip = ""
    Me.lbCodroot.UseMnemonic = False
    '
    'edCodroot
    '
    Me.edCodroot.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodroot.Location = New System.Drawing.Point(174, 142)
    Me.edCodroot.Name = "edCodroot"
    Me.edCodroot.NTSDbField = ""
    Me.edCodroot.NTSForzaVisZoom = False
    Me.edCodroot.NTSOldValue = ""
    Me.edCodroot.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodroot.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodroot.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodroot.Properties.MaxLength = 65536
    Me.edCodroot.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodroot.Size = New System.Drawing.Size(217, 20)
    Me.edCodroot.TabIndex = 15
    '
    'edCodvari3
    '
    Me.edCodvari3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodvari3.Location = New System.Drawing.Point(243, 116)
    Me.edCodvari3.Name = "edCodvari3"
    Me.edCodvari3.NTSDbField = ""
    Me.edCodvari3.NTSForzaVisZoom = False
    Me.edCodvari3.NTSOldValue = ""
    Me.edCodvari3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodvari3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodvari3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodvari3.Properties.MaxLength = 65536
    Me.edCodvari3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodvari3.Size = New System.Drawing.Size(93, 20)
    Me.edCodvari3.TabIndex = 14
    '
    'lbLungvar3
    '
    Me.lbLungvar3.AutoSize = True
    Me.lbLungvar3.BackColor = System.Drawing.Color.Transparent
    Me.lbLungvar3.Location = New System.Drawing.Point(12, 119)
    Me.lbLungvar3.Name = "lbLungvar3"
    Me.lbLungvar3.NTSDbField = ""
    Me.lbLungvar3.Size = New System.Drawing.Size(149, 13)
    Me.lbLungvar3.TabIndex = 13
    Me.lbLungvar3.Text = "Lunghezza 3a variante (1 - 8)"
    Me.lbLungvar3.Tooltip = ""
    Me.lbLungvar3.UseMnemonic = False
    '
    'edLungvar3
    '
    Me.edLungvar3.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edLungvar3.Location = New System.Drawing.Point(174, 116)
    Me.edLungvar3.Name = "edLungvar3"
    Me.edLungvar3.NTSDbField = ""
    Me.edLungvar3.NTSFormat = "0"
    Me.edLungvar3.NTSForzaVisZoom = False
    Me.edLungvar3.NTSOldValue = ""
    Me.edLungvar3.Properties.Appearance.Options.UseTextOptions = True
    Me.edLungvar3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edLungvar3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edLungvar3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edLungvar3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edLungvar3.Properties.MaxLength = 65536
    Me.edLungvar3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edLungvar3.Size = New System.Drawing.Size(63, 20)
    Me.edLungvar3.TabIndex = 12
    '
    'lbLungvar2
    '
    Me.lbLungvar2.AutoSize = True
    Me.lbLungvar2.BackColor = System.Drawing.Color.Transparent
    Me.lbLungvar2.Location = New System.Drawing.Point(12, 93)
    Me.lbLungvar2.Name = "lbLungvar2"
    Me.lbLungvar2.NTSDbField = ""
    Me.lbLungvar2.Size = New System.Drawing.Size(149, 13)
    Me.lbLungvar2.TabIndex = 11
    Me.lbLungvar2.Text = "Lunghezza 2a variante (1 - 8)"
    Me.lbLungvar2.Tooltip = ""
    Me.lbLungvar2.UseMnemonic = False
    '
    'edLungvar2
    '
    Me.edLungvar2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edLungvar2.Location = New System.Drawing.Point(174, 90)
    Me.edLungvar2.Name = "edLungvar2"
    Me.edLungvar2.NTSDbField = ""
    Me.edLungvar2.NTSFormat = "0"
    Me.edLungvar2.NTSForzaVisZoom = False
    Me.edLungvar2.NTSOldValue = ""
    Me.edLungvar2.Properties.Appearance.Options.UseTextOptions = True
    Me.edLungvar2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edLungvar2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edLungvar2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edLungvar2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edLungvar2.Properties.MaxLength = 65536
    Me.edLungvar2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edLungvar2.Size = New System.Drawing.Size(63, 20)
    Me.edLungvar2.TabIndex = 10
    '
    'lbLungvar1
    '
    Me.lbLungvar1.AutoSize = True
    Me.lbLungvar1.BackColor = System.Drawing.Color.Transparent
    Me.lbLungvar1.Location = New System.Drawing.Point(12, 67)
    Me.lbLungvar1.Name = "lbLungvar1"
    Me.lbLungvar1.NTSDbField = ""
    Me.lbLungvar1.Size = New System.Drawing.Size(149, 13)
    Me.lbLungvar1.TabIndex = 9
    Me.lbLungvar1.Text = "Lunghezza 1a variante (1 - 8)"
    Me.lbLungvar1.Tooltip = ""
    Me.lbLungvar1.UseMnemonic = False
    '
    'edLungvar1
    '
    Me.edLungvar1.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edLungvar1.Location = New System.Drawing.Point(174, 64)
    Me.edLungvar1.Name = "edLungvar1"
    Me.edLungvar1.NTSDbField = ""
    Me.edLungvar1.NTSFormat = "0"
    Me.edLungvar1.NTSForzaVisZoom = False
    Me.edLungvar1.NTSOldValue = ""
    Me.edLungvar1.Properties.Appearance.Options.UseTextOptions = True
    Me.edLungvar1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edLungvar1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edLungvar1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edLungvar1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edLungvar1.Properties.MaxLength = 65536
    Me.edLungvar1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edLungvar1.Size = New System.Drawing.Size(63, 20)
    Me.edLungvar1.TabIndex = 8
    '
    'lbLungroot
    '
    Me.lbLungroot.AutoSize = True
    Me.lbLungroot.BackColor = System.Drawing.Color.Transparent
    Me.lbLungroot.Location = New System.Drawing.Point(12, 41)
    Me.lbLungroot.Name = "lbLungroot"
    Me.lbLungroot.NTSDbField = ""
    Me.lbLungroot.Size = New System.Drawing.Size(153, 13)
    Me.lbLungroot.TabIndex = 7
    Me.lbLungroot.Text = "Lunghezza codice root (1 - 12)"
    Me.lbLungroot.Tooltip = ""
    Me.lbLungroot.UseMnemonic = False
    '
    'edLungroot
    '
    Me.edLungroot.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edLungroot.Location = New System.Drawing.Point(174, 38)
    Me.edLungroot.Name = "edLungroot"
    Me.edLungroot.NTSDbField = ""
    Me.edLungroot.NTSFormat = "0"
    Me.edLungroot.NTSForzaVisZoom = False
    Me.edLungroot.NTSOldValue = ""
    Me.edLungroot.Properties.Appearance.Options.UseTextOptions = True
    Me.edLungroot.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edLungroot.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edLungroot.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edLungroot.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edLungroot.Properties.MaxLength = 65536
    Me.edLungroot.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edLungroot.Size = New System.Drawing.Size(63, 20)
    Me.edLungroot.TabIndex = 6
    '
    'lbNumliv
    '
    Me.lbNumliv.AutoSize = True
    Me.lbNumliv.BackColor = System.Drawing.Color.Transparent
    Me.lbNumliv.Location = New System.Drawing.Point(12, 15)
    Me.lbNumliv.Name = "lbNumliv"
    Me.lbNumliv.NTSDbField = ""
    Me.lbNumliv.Size = New System.Drawing.Size(116, 13)
    Me.lbNumliv.TabIndex = 5
    Me.lbNumliv.Text = "N° livelli varianti (1 - 3)"
    Me.lbNumliv.Tooltip = ""
    Me.lbNumliv.UseMnemonic = False
    '
    'edNumliv
    '
    Me.edNumliv.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumliv.Location = New System.Drawing.Point(174, 12)
    Me.edNumliv.Name = "edNumliv"
    Me.edNumliv.NTSDbField = ""
    Me.edNumliv.NTSFormat = "0"
    Me.edNumliv.NTSForzaVisZoom = False
    Me.edNumliv.NTSOldValue = ""
    Me.edNumliv.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumliv.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumliv.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNumliv.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNumliv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumliv.Properties.MaxLength = 65536
    Me.edNumliv.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumliv.Size = New System.Drawing.Size(63, 20)
    Me.edNumliv.TabIndex = 4
    '
    'edCodvari2
    '
    Me.edCodvari2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodvari2.Location = New System.Drawing.Point(243, 90)
    Me.edCodvari2.Name = "edCodvari2"
    Me.edCodvari2.NTSDbField = ""
    Me.edCodvari2.NTSForzaVisZoom = False
    Me.edCodvari2.NTSOldValue = ""
    Me.edCodvari2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodvari2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodvari2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodvari2.Properties.MaxLength = 65536
    Me.edCodvari2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodvari2.Size = New System.Drawing.Size(93, 20)
    Me.edCodvari2.TabIndex = 3
    '
    'edCodvari1
    '
    Me.edCodvari1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodvari1.Location = New System.Drawing.Point(243, 64)
    Me.edCodvari1.Name = "edCodvari1"
    Me.edCodvari1.NTSDbField = ""
    Me.edCodvari1.NTSForzaVisZoom = False
    Me.edCodvari1.NTSOldValue = ""
    Me.edCodvari1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodvari1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodvari1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodvari1.Properties.MaxLength = 65536
    Me.edCodvari1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodvari1.Size = New System.Drawing.Size(93, 20)
    Me.edCodvari1.TabIndex = 1
    '
    'FRMMGNARV
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(543, 275)
    Me.Controls.Add(Me.pnNarv)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.Name = "FRMMGNARV"
    Me.Text = "NUOVO ARTICOLO"
    CType(Me.pnNarv, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnNarv.ResumeLayout(False)
    Me.pnNarv.PerformLayout()
    CType(Me.fmPrezzi, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmPrezzi.ResumeLayout(False)
    Me.fmPrezzi.PerformLayout()
    CType(Me.opPrevar2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opPrevar1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opPrevar0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodroot.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodvari3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edLungvar3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edLungvar2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edLungvar1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edLungroot.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNumliv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodvari2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodvari1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      opPrevar2.NTSSetParam(oMenu, oApp.Tr(Me, 128586844957118148, "Pr&ezzi diversi 3 varianti"), "S")
      opPrevar1.NTSSetParam(oMenu, oApp.Tr(Me, 128586844957274396, "P&rezzi diversi 1a variante"), "S")
      opPrevar0.NTSSetParam(oMenu, oApp.Tr(Me, 128586844957430644, "&Prezzi comuni"), "S")
      edCodroot.NTSSetParam(oMenu, oApp.Tr(Me, 128586844957586892, "Codice articolo"), NTSCInt(IIf(CLN__STD.CodartMaxLen = 18, 12, (CLN__STD.CodartMaxLen - 1))))
      edCodvari3.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128586844957743140, "Lunghezza 3a variante (1 - 8)"), tabvari, True)
      edLungvar3.NTSSetParam(oMenu, oApp.Tr(Me, 128586844958055636, "Lunghezza 3a variante (1 - 8)"), "0", 1, 0, 8)
      edLungvar2.NTSSetParam(oMenu, oApp.Tr(Me, 128586844958368132, "Lunghezza 2a variante (1 - 8)"), "0", 2, 0, 8)
      edLungvar1.NTSSetParam(oMenu, oApp.Tr(Me, 128586844958680628, "Lunghezza 1a variante (1 - 8)"), "0", 2, 0, 8)
      edLungroot.NTSSetParam(oMenu, oApp.Tr(Me, 128586844958993124, "Lunghezza codice root (1 - 12)"), "0", 2, 0, NTSCInt(IIf(CLN__STD.CodartMaxLen = 18, 12, (CLN__STD.CodartMaxLen - 1))))
      edNumliv.NTSSetParam(oMenu, oApp.Tr(Me, 128586844959305620, "N° livelli varianti (1 - 3)"), "0", 1, 0, 3)
      edCodvari2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128586844959461868, "Lunghezza 2a variante (1 - 8)"), tabvari, True)
      edCodvari1.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128586844959618116, "Lunghezza 1a variante (1 - 8)"), tabvari, True)

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
  Public Overridable Sub InitEntity(ByRef cleArtv As CLEMGARTV)
    oCleArtv = cleArtv
    AddHandler oCleArtv.RemoteEvent, AddressOf GestisciEventiEntity
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

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      '-----------------------------------------------------------------------------------------
      '--- Controlla la validità dei dati
      '-----------------------------------------------------------------------------------------
      Me.ValidaLastControl()

      If NTSCInt(edNumliv.Text) = 0 Then Exit Sub
      If NTSCInt(edNumliv.Text) >= 1 Then If Not CheckLungvar1() Then Exit Sub
      If NTSCInt(edNumliv.Text) >= 2 Then If Not CheckLungvar2() Then Exit Sub
      If NTSCInt(edNumliv.Text) = 3 Then If Not CheckLungvar3() Then Exit Sub
      If Not NarvCheckCodroot() Then Exit Sub
      If Not CheckSelection() Then Exit Sub
      If Trim(edCodroot.Text) = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128586911254125499, "Digitare il codice."))
        Exit Sub
      End If

      'setto varibili di ritorno
      oCleArtv.strArtvCodroot = UCase(edCodroot.Text)
      oCleArtv.nArtvNumliv = NTSCInt(edNumliv.Text)
      oCleArtv.nArtvLungroot = NTSCInt(edLungroot.Text)
      oCleArtv.nArtvLungvar1 = NTSCInt(edLungvar1.Text)
      oCleArtv.nArtvLungvar2 = NTSCInt(edLungvar2.Text)
      oCleArtv.nArtvLungvar3 = NTSCInt(edLungvar3.Text)
      If opPrevar0.Checked Then
        oCleArtv.strArtvPrevar = "N"
      Else
        If opPrevar1.Checked Then oCleArtv.strArtvPrevar = "1" Else oCleArtv.strArtvPrevar = "S"
      End If
      If edCodvari1.Text <> "" Then oCleArtv.strNarvCodvari1 = edCodvari1.Text Else oCleArtv.strNarvCodvari1 = ""
      If edCodvari2.Text <> "" Then oCleArtv.strNarvCodvari2 = edCodvari2.Text Else oCleArtv.strNarvCodvari2 = ""
      If edCodvari3.Text <> "" Then oCleArtv.strNarvCodvari3 = edCodvari3.Text Else oCleArtv.strNarvCodvari3 = ""

      oCleArtv.NarvGeneraCodice(edCodroot.Text, edLungroot.Text)

      bNarvAnnullato = False
      Me.Close()

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    bNarvAnnullato = True
    Me.Close()
  End Sub

  Public Overridable Sub FRMMGNARV_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
    Try
      '--------------------------------------------
      'gestione dello zoom:
      'eseguo la Zoom, tanto se per il controllo non deve venir eseguito uno zoom particolare, la routine non fa nulla e viene processato lo zoom standard del controllo, settato con la NTSSetParamZoom
      If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
        Zoom()
        e.Handled = True    'altrimenti anche il controllo riceve l'F5 e la routine ZOOM viene eseguita 2 volte!!!
      End If

      If edCodvari1.Focused And e.KeyCode = Keys.Return Then
        cmdOk.Focus()
        cmdOk_Click(sender, e)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGNARV_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      lbLungroot.Text = oApp.Tr(Me, 130420500517867526, "Lunghezza codice root (1 - |" & (NTSCInt(IIf(CLN__STD.CodartMaxLen = 18, 12, (CLN__STD.CodartMaxLen - 1)))).ToString & "|)")

      bNarvAnnullato = True

      oCleArtv.strGeneraCodice = oMenu.GetSettingBus("BSMGARTV", "OPZIONI", ".", "GeneraCodice", "0", " ", "0")
      oCleArtv.nLungPreRoot = NTSCInt(oMenu.GetSettingBus("BSMGARTV", "OPZIONI", ".", "GeneraCodiceLungPreRoot", "0", " ", "0"))
      oCleArtv.bArticoloGenerato = False
      oCleArtv.lProgressivo = 0
      oCleArtv.strRadice = ""

      edNumliv.Text = NTSCStr(oCleArtv.nArtvNumliv)
      edLungroot.Text = NTSCStr(oCleArtv.nArtvLungroot)
      edLungvar1.Text = NTSCStr(oCleArtv.nArtvLungvar1)
      edLungvar2.Text = NTSCStr(oCleArtv.nArtvLungvar2)
      edLungvar3.Text = NTSCStr(oCleArtv.nArtvLungvar3)
      edCodroot.Text = NTSCStr(oCleArtv.strArtvCodroot)
      lbLungvar2.Enabled = False
      lbLungvar3.Enabled = False
      edLungvar1.Text = "1"
      edLungvar2.Text = "0"
      edLungvar3.Text = "0"
      edLungvar2.Enabled = False
      edLungvar3.Enabled = False
      edLungroot.Text = NTSCStr(oMenu.GetSettingBus("BSMGARTV", "RECENT", ".", "LunghezzaRoot", "5", " ", "5"))

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
  Public Overridable Sub Zoom()
    Try
      'per eventuali altri controlli caricati al volo
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      '------------------------------------
      'zoom standard di textbox e griglia
      NTSCallStandardZoom()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edCodroot_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCodroot.Validated
    Try
      If Not NarvCheckCodroot() Then Exit Sub
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function NarvCheckCodroot() As Boolean
    Try
      Return oCleArtv.NarvCheckCodroot(edCodroot.Text, edLungroot.Text)

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckSelection() As Boolean
    Try
      Select Case edNumliv.Text
        Case "1"
          If (NTSCInt(edLungroot.Text) + NTSCInt(edLungvar1.Text)) > CLN__STD.CodartMaxLen Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128586906075902705, "Max. totale |" & CLN__STD.CodartMaxLen.ToString & "| caratteri fra codice articolo e 1° livello."))
            Return False
          End If
        Case "2"
          If (NTSCInt(edLungroot.Text) + NTSCInt(edLungvar1.Text) + NTSCInt(edLungvar2.Text)) > CLN__STD.CodartMaxLen Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128586906283275217, "Max. totale |" & CLN__STD.CodartMaxLen.ToString & "| caratteri fra codice articolo, 1° e 2° livello."))
            Return False
          End If
        Case "3"
          If (NTSCInt(edLungroot.Text) + NTSCInt(edLungvar1.Text) + NTSCInt(edLungvar2.Text) + NTSCInt(edLungvar3.Text)) > CLN__STD.CodartMaxLen Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128586906520941605, "Max. totale |" & CLN__STD.CodartMaxLen.ToString & "| caratteri fra codice articolo, 1°, 2° e 3° livello."))
            Return False
          End If
      End Select

      Return True

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckLungvar1() As Boolean
    Try
      If edLungvar1.Text = "0" Then Return False

      Return True

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function CheckLungvar2() As Boolean
    Try
      If edLungvar2.Text = "0" Then Return False

      Return True

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function CheckLungvar3() As Boolean
    Try
      If edLungvar3.Text = "0" Then Return False

      Return True

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub edCodvari1_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCodvari1.Validated
    Dim strError As String = ""
    Dim strLung As String = ""
    Try
      If oCleArtv Is Nothing Then Return
      If Not oCleArtv.edCodvari1_Validated(NTSCStr(edCodvari1.Text), strLung, strError) Then
        oApp.MsgBoxErr(strError)
        edCodvari1.Text = ""
        GctlSetVisEnab(edLungvar1, False)
      Else
        If NTSCInt(strLung) > 0 Then 'se trovato codice variante propone la lunghezza del codice della variante
          edLungvar1.Text = strLung
          edLungvar1.Enabled = False
        Else
          GctlSetVisEnab(edLungvar1, False)
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edCodvari2_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCodvari2.Validated
    Dim strError As String = ""
    Dim strLung As String = ""
    Try
      If oCleArtv Is Nothing Then Return
      If Not oCleArtv.edCodvari2_Validated(NTSCStr(edCodvari2.Text), strLung, strError) Then
        oApp.MsgBoxErr(strError)
        edCodvari2.Text = ""
        GctlSetVisEnab(edLungvar2, False)
      Else
        If NTSCInt(strLung) > 0 Then 'se trovato codice variante propone la lunghezza del codice della variante
          edLungvar2.Text = strLung
          edLungvar2.Enabled = False
        Else
          GctlSetVisEnab(edLungvar2, False)
        End If

      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edCodvari3_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edCodvari3.Validated
    Dim strError As String = ""
    Dim strLung As String = ""
    Try
      If oCleArtv Is Nothing Then Return
      If Not oCleArtv.edCodvari3_Validated(NTSCStr(edCodvari3.Text), strLung, strError) Then
        oApp.MsgBoxErr(strError)
        edCodvari3.Text = ""
        GctlSetVisEnab(edLungvar3, False)
      Else
        If NTSCInt(strLung) > 0 Then 'se trovato codice variante propone la lunghezza del codice della variante
          edLungvar3.Text = strLung
          edLungvar3.Enabled = False
        Else
          GctlSetVisEnab(edLungvar3, False)
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edNumliv_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edNumliv.Validated
    Dim strError As String = ""
    Try
      If edNumliv.Text = "0" Then Exit Sub

      Select Case edNumliv.Text
        Case "1"
          lbLungvar2.Enabled = False
          lbLungvar3.Enabled = False
          edLungvar1.Text = "1"
          edLungvar2.Text = "0"
          edLungvar3.Text = "0"
          edCodvari1.Text = ""
          edCodvari2.Text = ""
          edCodvari3.Text = ""
          GctlSetVisEnab(edLungvar1, False)
          edLungvar2.Enabled = False
          edLungvar3.Enabled = False
          edCodvari2.Enabled = False
          edCodvari3.Enabled = False
        Case "2"
          GctlSetVisEnab(lbLungvar2, False)
          lbLungvar3.Enabled = False
          edLungvar1.Text = "1"
          edLungvar2.Text = "1"
          edLungvar3.Text = "0"
          edCodvari1.Text = ""
          edCodvari2.Text = ""
          edCodvari3.Text = ""
          GctlSetVisEnab(edLungvar1, False)
          GctlSetVisEnab(edLungvar2, False)
          edLungvar3.Enabled = False
          GctlSetVisEnab(edCodvari2, False)
          edCodvari3.Enabled = False
        Case "3"
          GctlSetVisEnab(lbLungvar2, False)
          GctlSetVisEnab(lbLungvar3, False)
          edLungvar1.Text = "1"
          edLungvar2.Text = "1"
          edLungvar3.Text = "1"
          edCodvari1.Text = ""
          edCodvari2.Text = ""
          edCodvari3.Text = ""
          GctlSetVisEnab(edLungvar1, False)
          GctlSetVisEnab(edLungvar2, False)
          GctlSetVisEnab(edLungvar3, False)
          GctlSetVisEnab(edCodvari2, False)
          GctlSetVisEnab(edCodvari3, False)
      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class