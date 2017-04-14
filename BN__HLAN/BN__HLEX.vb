Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__HLEX
  Public oCleHlan As CLE__HLAN
  Public dsMaga As DataSet
  Public oCallParams As CLE__CLDP
  Public dcMaga As BindingSource = New BindingSource()
  Public strTipoConto As String      'tipo di estenzioni da trattare (C=Clienti, F=Fornitori)
  Public bOttimistico As Boolean ' Se nella maschera principale è presente la spunta di ottimistico

  Public strProgrChiamante As String = ""
  Public strFiltriDaEsterno As String = ""
  Public dttAnex As New DataTable

  Private components As System.ComponentModel.IContainer

  Public Overridable Sub InitializeComponent()
    Me.tsHlex = New NTSInformatica.NTSTabControl
    Me.pnTab1 = New NTSInformatica.NTSTabPage
    Me.pn1 = New NTSInformatica.NTSPanel
    Me.lbDesEx3 = New NTSInformatica.NTSLabel
    Me.lbDesEx2 = New NTSInformatica.NTSLabel
    Me.edDesex3 = New NTSInformatica.NTSMemoBox
    Me.edDesex2 = New NTSInformatica.NTSMemoBox
    Me.edDesex1 = New NTSInformatica.NTSMemoBox
    Me.lbDesEx1 = New NTSInformatica.NTSLabel
    Me.edTipo3 = New NTSInformatica.NTSTextBoxStr
    Me.edTipo2 = New NTSInformatica.NTSTextBoxStr
    Me.edTipo1 = New NTSInformatica.NTSTextBoxStr
    Me.lbHelpTipo3 = New NTSInformatica.NTSLabel
    Me.lbHelpTipo2 = New NTSInformatica.NTSLabel
    Me.lbHelpTipo1 = New NTSInformatica.NTSLabel
    Me.lbTipo2 = New NTSInformatica.NTSLabel
    Me.lbTipo3 = New NTSInformatica.NTSLabel
    Me.lbTipo1 = New NTSInformatica.NTSLabel
    Me.pnTab2 = New NTSInformatica.NTSTabPage
    Me.pn2 = New NTSInformatica.NTSPanel
    Me.ckData5 = New NTSInformatica.NTSCheckBox
    Me.ckData4 = New NTSInformatica.NTSCheckBox
    Me.ckData3 = New NTSInformatica.NTSCheckBox
    Me.ckData2 = New NTSInformatica.NTSCheckBox
    Me.NtsLabel11 = New NTSInformatica.NTSLabel
    Me.NtsLabel10 = New NTSInformatica.NTSLabel
    Me.NtsLabel9 = New NTSInformatica.NTSLabel
    Me.NtsLabel8 = New NTSInformatica.NTSLabel
    Me.NtsLabel7 = New NTSInformatica.NTSLabel
    Me.NtsLabel6 = New NTSInformatica.NTSLabel
    Me.NtsLabel5 = New NTSInformatica.NTSLabel
    Me.NtsLabel4 = New NTSInformatica.NTSLabel
    Me.edData5a = New NTSInformatica.NTSTextBoxData
    Me.edData4a = New NTSInformatica.NTSTextBoxData
    Me.edData3a = New NTSInformatica.NTSTextBoxData
    Me.edData2a = New NTSInformatica.NTSTextBoxData
    Me.edData5da = New NTSInformatica.NTSTextBoxData
    Me.edData4da = New NTSInformatica.NTSTextBoxData
    Me.edData3da = New NTSInformatica.NTSTextBoxData
    Me.edData2da = New NTSInformatica.NTSTextBoxData
    Me.edData1a = New NTSInformatica.NTSTextBoxData
    Me.edData1da = New NTSInformatica.NTSTextBoxData
    Me.NtsLabel3 = New NTSInformatica.NTSLabel
    Me.NtsLabel2 = New NTSInformatica.NTSLabel
    Me.ckData1 = New NTSInformatica.NTSCheckBox
    Me.lbDescr10 = New NTSInformatica.NTSLabel
    Me.lbDescr9 = New NTSInformatica.NTSLabel
    Me.lbDescr8 = New NTSInformatica.NTSLabel
    Me.lbDescr7 = New NTSInformatica.NTSLabel
    Me.lbDescr6 = New NTSInformatica.NTSLabel
    Me.lbDescr5 = New NTSInformatica.NTSLabel
    Me.lbDescr4 = New NTSInformatica.NTSLabel
    Me.lbDescr3 = New NTSInformatica.NTSLabel
    Me.lbDescr2 = New NTSInformatica.NTSLabel
    Me.edDescr10 = New NTSInformatica.NTSTextBoxStr
    Me.edDescr9 = New NTSInformatica.NTSTextBoxStr
    Me.edDescr8 = New NTSInformatica.NTSTextBoxStr
    Me.edDescr7 = New NTSInformatica.NTSTextBoxStr
    Me.edDescr6 = New NTSInformatica.NTSTextBoxStr
    Me.edDescr5 = New NTSInformatica.NTSTextBoxStr
    Me.edDescr4 = New NTSInformatica.NTSTextBoxStr
    Me.edDescr3 = New NTSInformatica.NTSTextBoxStr
    Me.edDescr2 = New NTSInformatica.NTSTextBoxStr
    Me.edDescr1 = New NTSInformatica.NTSTextBoxStr
    Me.lbDescr1 = New NTSInformatica.NTSLabel
    Me.pnTab3 = New NTSInformatica.NTSTabPage
    Me.pn3 = New NTSInformatica.NTSPanel
    Me.edMemo1 = New NTSInformatica.NTSMemoBox
    Me.lbMemo1 = New NTSInformatica.NTSLabel
    Me.pnTab4 = New NTSInformatica.NTSTabPage
    Me.pn4 = New NTSInformatica.NTSPanel
    Me.edMemo2 = New NTSInformatica.NTSMemoBox
    Me.lbMemo2 = New NTSInformatica.NTSLabel
    Me.pnTab5 = New NTSInformatica.NTSTabPage
    Me.pn5 = New NTSInformatica.NTSPanel
    Me.edNum10a = New NTSInformatica.NTSTextBoxNum
    Me.edNum9a = New NTSInformatica.NTSTextBoxNum
    Me.edNum8a = New NTSInformatica.NTSTextBoxNum
    Me.edNum7a = New NTSInformatica.NTSTextBoxNum
    Me.edNum6a = New NTSInformatica.NTSTextBoxNum
    Me.edNum5a = New NTSInformatica.NTSTextBoxNum
    Me.edNum4a = New NTSInformatica.NTSTextBoxNum
    Me.edNum3a = New NTSInformatica.NTSTextBoxNum
    Me.edNum2a = New NTSInformatica.NTSTextBoxNum
    Me.edNum1a = New NTSInformatica.NTSTextBoxNum
    Me.edNum10da = New NTSInformatica.NTSTextBoxNum
    Me.edNum9da = New NTSInformatica.NTSTextBoxNum
    Me.edNum8da = New NTSInformatica.NTSTextBoxNum
    Me.edNum7da = New NTSInformatica.NTSTextBoxNum
    Me.edNum6da = New NTSInformatica.NTSTextBoxNum
    Me.edNum5da = New NTSInformatica.NTSTextBoxNum
    Me.edNum4da = New NTSInformatica.NTSTextBoxNum
    Me.edNum3da = New NTSInformatica.NTSTextBoxNum
    Me.edNum2da = New NTSInformatica.NTSTextBoxNum
    Me.edNum1da = New NTSInformatica.NTSTextBoxNum
    Me.ckNum10 = New NTSInformatica.NTSCheckBox
    Me.ckNum9 = New NTSInformatica.NTSCheckBox
    Me.ckNum8 = New NTSInformatica.NTSCheckBox
    Me.ckNum7 = New NTSInformatica.NTSCheckBox
    Me.NtsLabel22 = New NTSInformatica.NTSLabel
    Me.NtsLabel23 = New NTSInformatica.NTSLabel
    Me.NtsLabel24 = New NTSInformatica.NTSLabel
    Me.NtsLabel25 = New NTSInformatica.NTSLabel
    Me.NtsLabel26 = New NTSInformatica.NTSLabel
    Me.NtsLabel27 = New NTSInformatica.NTSLabel
    Me.NtsLabel28 = New NTSInformatica.NTSLabel
    Me.NtsLabel29 = New NTSInformatica.NTSLabel
    Me.NtsLabel30 = New NTSInformatica.NTSLabel
    Me.NtsLabel31 = New NTSInformatica.NTSLabel
    Me.ckNum6 = New NTSInformatica.NTSCheckBox
    Me.ckNum5 = New NTSInformatica.NTSCheckBox
    Me.ckNum4 = New NTSInformatica.NTSCheckBox
    Me.ckNum3 = New NTSInformatica.NTSCheckBox
    Me.ckNum2 = New NTSInformatica.NTSCheckBox
    Me.NtsLabel12 = New NTSInformatica.NTSLabel
    Me.NtsLabel13 = New NTSInformatica.NTSLabel
    Me.NtsLabel14 = New NTSInformatica.NTSLabel
    Me.NtsLabel15 = New NTSInformatica.NTSLabel
    Me.NtsLabel16 = New NTSInformatica.NTSLabel
    Me.NtsLabel17 = New NTSInformatica.NTSLabel
    Me.NtsLabel18 = New NTSInformatica.NTSLabel
    Me.NtsLabel19 = New NTSInformatica.NTSLabel
    Me.NtsLabel20 = New NTSInformatica.NTSLabel
    Me.NtsLabel21 = New NTSInformatica.NTSLabel
    Me.ckNum1 = New NTSInformatica.NTSCheckBox
    Me.pnTab6 = New NTSInformatica.NTSTabPage
    Me.pn6 = New NTSInformatica.NTSPanel
    Me.lbCombo3 = New NTSInformatica.NTSLabel
    Me.lbCombo2 = New NTSInformatica.NTSLabel
    Me.liList3 = New NTSInformatica.NTSListBox
    Me.lbCombo1 = New NTSInformatica.NTSLabel
    Me.liList2 = New NTSInformatica.NTSListBox
    Me.liList1 = New NTSInformatica.NTSListBox
    Me.lbCheck10 = New NTSInformatica.NTSLabel
    Me.lbCheck8 = New NTSInformatica.NTSLabel
    Me.liCheck10 = New NTSInformatica.NTSListBox
    Me.lbCheck6 = New NTSInformatica.NTSLabel
    Me.liCheck8 = New NTSInformatica.NTSListBox
    Me.lbCheck4 = New NTSInformatica.NTSLabel
    Me.liCheck6 = New NTSInformatica.NTSListBox
    Me.liCheck4 = New NTSInformatica.NTSListBox
    Me.lbCheck2 = New NTSInformatica.NTSLabel
    Me.liCheck2 = New NTSInformatica.NTSListBox
    Me.lbCheck9 = New NTSInformatica.NTSLabel
    Me.lbCheck7 = New NTSInformatica.NTSLabel
    Me.liCheck9 = New NTSInformatica.NTSListBox
    Me.lbCheck5 = New NTSInformatica.NTSLabel
    Me.liCheck7 = New NTSInformatica.NTSListBox
    Me.lbCheck3 = New NTSInformatica.NTSLabel
    Me.liCheck5 = New NTSInformatica.NTSListBox
    Me.liCheck3 = New NTSInformatica.NTSListBox
    Me.lbCheck1 = New NTSInformatica.NTSLabel
    Me.liCheck1 = New NTSInformatica.NTSListBox
    Me.pnCommand = New NTSInformatica.NTSPanel
    Me.NtsLabel1 = New NTSInformatica.NTSLabel
    Me.cmdCancel = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.pnFilter = New NTSInformatica.NTSPanel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tsHlex, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsHlex.SuspendLayout()
    Me.pnTab1.SuspendLayout()
    CType(Me.pn1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pn1.SuspendLayout()
    CType(Me.edDesex3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDesex2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDesex1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTipo3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTipo2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTipo1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTab2.SuspendLayout()
    CType(Me.pn2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pn2.SuspendLayout()
    CType(Me.ckData5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckData4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckData3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckData2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edData5a.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edData4a.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edData3a.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edData2a.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edData5da.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edData4da.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edData3da.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edData2da.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edData1a.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edData1da.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckData1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDescr10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDescr9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDescr8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDescr7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDescr6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDescr5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDescr4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDescr3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDescr2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDescr1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTab3.SuspendLayout()
    CType(Me.pn3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pn3.SuspendLayout()
    CType(Me.edMemo1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTab4.SuspendLayout()
    CType(Me.pn4, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pn4.SuspendLayout()
    CType(Me.edMemo2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTab5.SuspendLayout()
    CType(Me.pn5, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pn5.SuspendLayout()
    CType(Me.edNum10a.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNum9a.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNum8a.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNum7a.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNum6a.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNum5a.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNum4a.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNum3a.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNum2a.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNum1a.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNum10da.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNum9da.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNum8da.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNum7da.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNum6da.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNum5da.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNum4da.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNum3da.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNum2da.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNum1da.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNum10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNum9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNum8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNum7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNum6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNum5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNum4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNum3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNum2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNum1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTab6.SuspendLayout()
    CType(Me.pn6, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pn6.SuspendLayout()
    CType(Me.liList3, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.liList2, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.liList1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.liCheck10, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.liCheck8, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.liCheck6, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.liCheck4, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.liCheck2, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.liCheck9, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.liCheck7, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.liCheck5, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.liCheck3, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.liCheck1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnCommand, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCommand.SuspendLayout()
    CType(Me.pnFilter, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFilter.SuspendLayout()
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
    'tsHlex
    '
    Me.tsHlex.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tsHlex.Location = New System.Drawing.Point(0, 0)
    Me.tsHlex.Name = "tsHlex"
    Me.tsHlex.SelectedTabPage = Me.pnTab1
    Me.tsHlex.Size = New System.Drawing.Size(678, 443)
    Me.tsHlex.TabIndex = 1
    Me.tsHlex.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.pnTab1, Me.pnTab2, Me.pnTab3, Me.pnTab4, Me.pnTab5, Me.pnTab6})
    '
    'pnTab1
    '
    Me.pnTab1.AllowDrop = True
    Me.pnTab1.Appearance.Header.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
    Me.pnTab1.Appearance.Header.Options.UseFont = True
    Me.pnTab1.Controls.Add(Me.pn1)
    Me.pnTab1.Enable = True
    Me.pnTab1.Name = "pnTab1"
    Me.pnTab1.Size = New System.Drawing.Size(669, 413)
    Me.pnTab1.Text = "*TAB1"
    '
    'pn1
    '
    Me.pn1.AllowDrop = True
    Me.pn1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pn1.Appearance.Options.UseBackColor = True
    Me.pn1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pn1.Controls.Add(Me.lbDesEx3)
    Me.pn1.Controls.Add(Me.lbDesEx2)
    Me.pn1.Controls.Add(Me.edDesex3)
    Me.pn1.Controls.Add(Me.edDesex2)
    Me.pn1.Controls.Add(Me.edDesex1)
    Me.pn1.Controls.Add(Me.lbDesEx1)
    Me.pn1.Controls.Add(Me.edTipo3)
    Me.pn1.Controls.Add(Me.edTipo2)
    Me.pn1.Controls.Add(Me.edTipo1)
    Me.pn1.Controls.Add(Me.lbHelpTipo3)
    Me.pn1.Controls.Add(Me.lbHelpTipo2)
    Me.pn1.Controls.Add(Me.lbHelpTipo1)
    Me.pn1.Controls.Add(Me.lbTipo2)
    Me.pn1.Controls.Add(Me.lbTipo3)
    Me.pn1.Controls.Add(Me.lbTipo1)
    Me.pn1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pn1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pn1.Location = New System.Drawing.Point(0, 0)
    Me.pn1.Name = "pn1"
    Me.pn1.NTSActiveTrasparency = True
    Me.pn1.Size = New System.Drawing.Size(669, 413)
    Me.pn1.TabIndex = 51
    '
    'lbDesEx3
    '
    Me.lbDesEx3.AutoSize = True
    Me.lbDesEx3.BackColor = System.Drawing.Color.Transparent
    Me.lbDesEx3.Location = New System.Drawing.Point(6, 327)
    Me.lbDesEx3.Name = "lbDesEx3"
    Me.lbDesEx3.NTSDbField = ""
    Me.lbDesEx3.Size = New System.Drawing.Size(57, 13)
    Me.lbDesEx3.TabIndex = 65
    Me.lbDesEx3.Text = "*lbDesEx3"
    Me.lbDesEx3.Tooltip = ""
    Me.lbDesEx3.UseMnemonic = False
    '
    'lbDesEx2
    '
    Me.lbDesEx2.AutoSize = True
    Me.lbDesEx2.BackColor = System.Drawing.Color.Transparent
    Me.lbDesEx2.Location = New System.Drawing.Point(6, 247)
    Me.lbDesEx2.Name = "lbDesEx2"
    Me.lbDesEx2.NTSDbField = ""
    Me.lbDesEx2.Size = New System.Drawing.Size(57, 13)
    Me.lbDesEx2.TabIndex = 64
    Me.lbDesEx2.Text = "*lbDesEx2"
    Me.lbDesEx2.Tooltip = ""
    Me.lbDesEx2.UseMnemonic = False
    '
    'edDesex3
    '
    Me.edDesex3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDesex3.EditValue = "*"
    Me.edDesex3.Location = New System.Drawing.Point(182, 324)
    Me.edDesex3.Name = "edDesex3"
    Me.edDesex3.NTSDbField = ""
    Me.edDesex3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDesex3.Size = New System.Drawing.Size(466, 64)
    Me.edDesex3.TabIndex = 63
    '
    'edDesex2
    '
    Me.edDesex2.EditValue = "*"
    Me.edDesex2.Location = New System.Drawing.Point(182, 244)
    Me.edDesex2.Name = "edDesex2"
    Me.edDesex2.NTSDbField = ""
    Me.edDesex2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDesex2.Size = New System.Drawing.Size(466, 64)
    Me.edDesex2.TabIndex = 62
    '
    'edDesex1
    '
    Me.edDesex1.EditValue = "*"
    Me.edDesex1.Location = New System.Drawing.Point(182, 163)
    Me.edDesex1.Name = "edDesex1"
    Me.edDesex1.NTSDbField = ""
    Me.edDesex1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDesex1.Size = New System.Drawing.Size(466, 64)
    Me.edDesex1.TabIndex = 61
    '
    'lbDesEx1
    '
    Me.lbDesEx1.AutoSize = True
    Me.lbDesEx1.BackColor = System.Drawing.Color.Transparent
    Me.lbDesEx1.Location = New System.Drawing.Point(6, 166)
    Me.lbDesEx1.Name = "lbDesEx1"
    Me.lbDesEx1.NTSDbField = ""
    Me.lbDesEx1.Size = New System.Drawing.Size(57, 13)
    Me.lbDesEx1.TabIndex = 60
    Me.lbDesEx1.Text = "*lbDesEx1"
    Me.lbDesEx1.Tooltip = ""
    Me.lbDesEx1.UseMnemonic = False
    '
    'edTipo3
    '
    Me.edTipo3.EditValue = "*"
    Me.edTipo3.Location = New System.Drawing.Point(181, 112)
    Me.edTipo3.Name = "edTipo3"
    Me.edTipo3.NTSDbField = ""
    Me.edTipo3.NTSForzaVisZoom = False
    Me.edTipo3.NTSOldValue = "*"
    Me.edTipo3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTipo3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTipo3.Properties.AutoHeight = False
    Me.edTipo3.Properties.MaxLength = 65536
    Me.edTipo3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTipo3.Size = New System.Drawing.Size(23, 20)
    Me.edTipo3.TabIndex = 59
    '
    'edTipo2
    '
    Me.edTipo2.EditValue = "*"
    Me.edTipo2.Location = New System.Drawing.Point(181, 64)
    Me.edTipo2.Name = "edTipo2"
    Me.edTipo2.NTSDbField = ""
    Me.edTipo2.NTSForzaVisZoom = False
    Me.edTipo2.NTSOldValue = "*"
    Me.edTipo2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTipo2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTipo2.Properties.AutoHeight = False
    Me.edTipo2.Properties.MaxLength = 65536
    Me.edTipo2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTipo2.Size = New System.Drawing.Size(23, 20)
    Me.edTipo2.TabIndex = 58
    '
    'edTipo1
    '
    Me.edTipo1.EditValue = "*"
    Me.edTipo1.Location = New System.Drawing.Point(181, 13)
    Me.edTipo1.Name = "edTipo1"
    Me.edTipo1.NTSDbField = ""
    Me.edTipo1.NTSForzaVisZoom = False
    Me.edTipo1.NTSOldValue = "*"
    Me.edTipo1.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window
    Me.edTipo1.Properties.Appearance.Options.UseBackColor = True
    Me.edTipo1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTipo1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTipo1.Properties.AutoHeight = False
    Me.edTipo1.Properties.MaxLength = 65536
    Me.edTipo1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTipo1.Size = New System.Drawing.Size(23, 20)
    Me.edTipo1.TabIndex = 57
    '
    'lbHelpTipo3
    '
    Me.lbHelpTipo3.AutoSize = True
    Me.lbHelpTipo3.BackColor = System.Drawing.Color.Transparent
    Me.lbHelpTipo3.Location = New System.Drawing.Point(225, 115)
    Me.lbHelpTipo3.Name = "lbHelpTipo3"
    Me.lbHelpTipo3.NTSDbField = ""
    Me.lbHelpTipo3.Size = New System.Drawing.Size(68, 13)
    Me.lbHelpTipo3.TabIndex = 56
    Me.lbHelpTipo3.Text = "*lbHelpTipo3"
    Me.lbHelpTipo3.Tooltip = ""
    Me.lbHelpTipo3.UseMnemonic = False
    '
    'lbHelpTipo2
    '
    Me.lbHelpTipo2.AutoSize = True
    Me.lbHelpTipo2.BackColor = System.Drawing.Color.Transparent
    Me.lbHelpTipo2.Location = New System.Drawing.Point(225, 67)
    Me.lbHelpTipo2.Name = "lbHelpTipo2"
    Me.lbHelpTipo2.NTSDbField = ""
    Me.lbHelpTipo2.Size = New System.Drawing.Size(68, 13)
    Me.lbHelpTipo2.TabIndex = 55
    Me.lbHelpTipo2.Text = "*lbHelpTipo2"
    Me.lbHelpTipo2.Tooltip = ""
    Me.lbHelpTipo2.UseMnemonic = False
    '
    'lbHelpTipo1
    '
    Me.lbHelpTipo1.AutoSize = True
    Me.lbHelpTipo1.BackColor = System.Drawing.Color.Transparent
    Me.lbHelpTipo1.Location = New System.Drawing.Point(225, 16)
    Me.lbHelpTipo1.Name = "lbHelpTipo1"
    Me.lbHelpTipo1.NTSDbField = ""
    Me.lbHelpTipo1.Size = New System.Drawing.Size(68, 13)
    Me.lbHelpTipo1.TabIndex = 54
    Me.lbHelpTipo1.Text = "*lbHelpTipo1"
    Me.lbHelpTipo1.Tooltip = ""
    Me.lbHelpTipo1.UseMnemonic = False
    '
    'lbTipo2
    '
    Me.lbTipo2.AutoSize = True
    Me.lbTipo2.BackColor = System.Drawing.Color.Transparent
    Me.lbTipo2.Location = New System.Drawing.Point(5, 67)
    Me.lbTipo2.Name = "lbTipo2"
    Me.lbTipo2.NTSDbField = ""
    Me.lbTipo2.Size = New System.Drawing.Size(47, 13)
    Me.lbTipo2.TabIndex = 53
    Me.lbTipo2.Text = "*lbTipo2"
    Me.lbTipo2.Tooltip = ""
    Me.lbTipo2.UseMnemonic = False
    '
    'lbTipo3
    '
    Me.lbTipo3.AutoSize = True
    Me.lbTipo3.BackColor = System.Drawing.Color.Transparent
    Me.lbTipo3.Location = New System.Drawing.Point(5, 115)
    Me.lbTipo3.Name = "lbTipo3"
    Me.lbTipo3.NTSDbField = ""
    Me.lbTipo3.Size = New System.Drawing.Size(47, 13)
    Me.lbTipo3.TabIndex = 52
    Me.lbTipo3.Text = "*lbTipo3"
    Me.lbTipo3.Tooltip = ""
    Me.lbTipo3.UseMnemonic = False
    '
    'lbTipo1
    '
    Me.lbTipo1.AutoSize = True
    Me.lbTipo1.BackColor = System.Drawing.Color.Transparent
    Me.lbTipo1.Location = New System.Drawing.Point(5, 16)
    Me.lbTipo1.Name = "lbTipo1"
    Me.lbTipo1.NTSDbField = ""
    Me.lbTipo1.Size = New System.Drawing.Size(47, 13)
    Me.lbTipo1.TabIndex = 51
    Me.lbTipo1.Text = "*lbTipo1"
    Me.lbTipo1.Tooltip = ""
    Me.lbTipo1.UseMnemonic = False
    '
    'pnTab2
    '
    Me.pnTab2.AllowDrop = True
    Me.pnTab2.Controls.Add(Me.pn2)
    Me.pnTab2.Enable = True
    Me.pnTab2.Name = "pnTab2"
    Me.pnTab2.Size = New System.Drawing.Size(669, 413)
    Me.pnTab2.Text = "*TAB2"
    '
    'pn2
    '
    Me.pn2.AllowDrop = True
    Me.pn2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pn2.Appearance.Options.UseBackColor = True
    Me.pn2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pn2.Controls.Add(Me.ckData5)
    Me.pn2.Controls.Add(Me.ckData4)
    Me.pn2.Controls.Add(Me.ckData3)
    Me.pn2.Controls.Add(Me.ckData2)
    Me.pn2.Controls.Add(Me.NtsLabel11)
    Me.pn2.Controls.Add(Me.NtsLabel10)
    Me.pn2.Controls.Add(Me.NtsLabel9)
    Me.pn2.Controls.Add(Me.NtsLabel8)
    Me.pn2.Controls.Add(Me.NtsLabel7)
    Me.pn2.Controls.Add(Me.NtsLabel6)
    Me.pn2.Controls.Add(Me.NtsLabel5)
    Me.pn2.Controls.Add(Me.NtsLabel4)
    Me.pn2.Controls.Add(Me.edData5a)
    Me.pn2.Controls.Add(Me.edData4a)
    Me.pn2.Controls.Add(Me.edData3a)
    Me.pn2.Controls.Add(Me.edData2a)
    Me.pn2.Controls.Add(Me.edData5da)
    Me.pn2.Controls.Add(Me.edData4da)
    Me.pn2.Controls.Add(Me.edData3da)
    Me.pn2.Controls.Add(Me.edData2da)
    Me.pn2.Controls.Add(Me.edData1a)
    Me.pn2.Controls.Add(Me.edData1da)
    Me.pn2.Controls.Add(Me.NtsLabel3)
    Me.pn2.Controls.Add(Me.NtsLabel2)
    Me.pn2.Controls.Add(Me.ckData1)
    Me.pn2.Controls.Add(Me.lbDescr10)
    Me.pn2.Controls.Add(Me.lbDescr9)
    Me.pn2.Controls.Add(Me.lbDescr8)
    Me.pn2.Controls.Add(Me.lbDescr7)
    Me.pn2.Controls.Add(Me.lbDescr6)
    Me.pn2.Controls.Add(Me.lbDescr5)
    Me.pn2.Controls.Add(Me.lbDescr4)
    Me.pn2.Controls.Add(Me.lbDescr3)
    Me.pn2.Controls.Add(Me.lbDescr2)
    Me.pn2.Controls.Add(Me.edDescr10)
    Me.pn2.Controls.Add(Me.edDescr9)
    Me.pn2.Controls.Add(Me.edDescr8)
    Me.pn2.Controls.Add(Me.edDescr7)
    Me.pn2.Controls.Add(Me.edDescr6)
    Me.pn2.Controls.Add(Me.edDescr5)
    Me.pn2.Controls.Add(Me.edDescr4)
    Me.pn2.Controls.Add(Me.edDescr3)
    Me.pn2.Controls.Add(Me.edDescr2)
    Me.pn2.Controls.Add(Me.edDescr1)
    Me.pn2.Controls.Add(Me.lbDescr1)
    Me.pn2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pn2.Location = New System.Drawing.Point(0, 0)
    Me.pn2.Name = "pn2"
    Me.pn2.NTSActiveTrasparency = True
    Me.pn2.Size = New System.Drawing.Size(669, 413)
    Me.pn2.TabIndex = 0
    '
    'ckData5
    '
    Me.ckData5.Location = New System.Drawing.Point(12, 383)
    Me.ckData5.Name = "ckData5"
    Me.ckData5.NTSCheckValue = "S"
    Me.ckData5.NTSUnCheckValue = "N"
    Me.ckData5.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckData5.Properties.Appearance.Options.UseBackColor = True
    Me.ckData5.Properties.AutoHeight = False
    Me.ckData5.Properties.Caption = "*ckData5"
    Me.ckData5.Size = New System.Drawing.Size(170, 19)
    Me.ckData5.TabIndex = 121
    '
    'ckData4
    '
    Me.ckData4.Location = New System.Drawing.Point(12, 357)
    Me.ckData4.Name = "ckData4"
    Me.ckData4.NTSCheckValue = "S"
    Me.ckData4.NTSUnCheckValue = "N"
    Me.ckData4.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckData4.Properties.Appearance.Options.UseBackColor = True
    Me.ckData4.Properties.AutoHeight = False
    Me.ckData4.Properties.Caption = "*ckData4"
    Me.ckData4.Size = New System.Drawing.Size(170, 19)
    Me.ckData4.TabIndex = 120
    '
    'ckData3
    '
    Me.ckData3.Location = New System.Drawing.Point(12, 331)
    Me.ckData3.Name = "ckData3"
    Me.ckData3.NTSCheckValue = "S"
    Me.ckData3.NTSUnCheckValue = "N"
    Me.ckData3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckData3.Properties.Appearance.Options.UseBackColor = True
    Me.ckData3.Properties.AutoHeight = False
    Me.ckData3.Properties.Caption = "*ckData3"
    Me.ckData3.Size = New System.Drawing.Size(170, 19)
    Me.ckData3.TabIndex = 119
    '
    'ckData2
    '
    Me.ckData2.Location = New System.Drawing.Point(12, 305)
    Me.ckData2.Name = "ckData2"
    Me.ckData2.NTSCheckValue = "S"
    Me.ckData2.NTSUnCheckValue = "N"
    Me.ckData2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckData2.Properties.Appearance.Options.UseBackColor = True
    Me.ckData2.Properties.AutoHeight = False
    Me.ckData2.Properties.Caption = "*ckData2"
    Me.ckData2.Size = New System.Drawing.Size(170, 19)
    Me.ckData2.TabIndex = 118
    '
    'NtsLabel11
    '
    Me.NtsLabel11.AutoSize = True
    Me.NtsLabel11.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel11.Location = New System.Drawing.Point(188, 384)
    Me.NtsLabel11.Name = "NtsLabel11"
    Me.NtsLabel11.NTSDbField = ""
    Me.NtsLabel11.Size = New System.Drawing.Size(20, 13)
    Me.NtsLabel11.TabIndex = 117
    Me.NtsLabel11.Text = "Da"
    Me.NtsLabel11.Tooltip = ""
    Me.NtsLabel11.UseMnemonic = False
    '
    'NtsLabel10
    '
    Me.NtsLabel10.AutoSize = True
    Me.NtsLabel10.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel10.Location = New System.Drawing.Point(188, 358)
    Me.NtsLabel10.Name = "NtsLabel10"
    Me.NtsLabel10.NTSDbField = ""
    Me.NtsLabel10.Size = New System.Drawing.Size(20, 13)
    Me.NtsLabel10.TabIndex = 116
    Me.NtsLabel10.Text = "Da"
    Me.NtsLabel10.Tooltip = ""
    Me.NtsLabel10.UseMnemonic = False
    '
    'NtsLabel9
    '
    Me.NtsLabel9.AutoSize = True
    Me.NtsLabel9.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel9.Location = New System.Drawing.Point(188, 332)
    Me.NtsLabel9.Name = "NtsLabel9"
    Me.NtsLabel9.NTSDbField = ""
    Me.NtsLabel9.Size = New System.Drawing.Size(20, 13)
    Me.NtsLabel9.TabIndex = 115
    Me.NtsLabel9.Text = "Da"
    Me.NtsLabel9.Tooltip = ""
    Me.NtsLabel9.UseMnemonic = False
    '
    'NtsLabel8
    '
    Me.NtsLabel8.AutoSize = True
    Me.NtsLabel8.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel8.Location = New System.Drawing.Point(188, 306)
    Me.NtsLabel8.Name = "NtsLabel8"
    Me.NtsLabel8.NTSDbField = ""
    Me.NtsLabel8.Size = New System.Drawing.Size(20, 13)
    Me.NtsLabel8.TabIndex = 114
    Me.NtsLabel8.Text = "Da"
    Me.NtsLabel8.Tooltip = ""
    Me.NtsLabel8.UseMnemonic = False
    '
    'NtsLabel7
    '
    Me.NtsLabel7.AutoSize = True
    Me.NtsLabel7.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel7.Location = New System.Drawing.Point(375, 384)
    Me.NtsLabel7.Name = "NtsLabel7"
    Me.NtsLabel7.NTSDbField = ""
    Me.NtsLabel7.Size = New System.Drawing.Size(14, 13)
    Me.NtsLabel7.TabIndex = 113
    Me.NtsLabel7.Text = "A"
    Me.NtsLabel7.Tooltip = ""
    Me.NtsLabel7.UseMnemonic = False
    '
    'NtsLabel6
    '
    Me.NtsLabel6.AutoSize = True
    Me.NtsLabel6.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel6.Location = New System.Drawing.Point(375, 358)
    Me.NtsLabel6.Name = "NtsLabel6"
    Me.NtsLabel6.NTSDbField = ""
    Me.NtsLabel6.Size = New System.Drawing.Size(14, 13)
    Me.NtsLabel6.TabIndex = 112
    Me.NtsLabel6.Text = "A"
    Me.NtsLabel6.Tooltip = ""
    Me.NtsLabel6.UseMnemonic = False
    '
    'NtsLabel5
    '
    Me.NtsLabel5.AutoSize = True
    Me.NtsLabel5.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel5.Location = New System.Drawing.Point(375, 332)
    Me.NtsLabel5.Name = "NtsLabel5"
    Me.NtsLabel5.NTSDbField = ""
    Me.NtsLabel5.Size = New System.Drawing.Size(14, 13)
    Me.NtsLabel5.TabIndex = 111
    Me.NtsLabel5.Text = "A"
    Me.NtsLabel5.Tooltip = ""
    Me.NtsLabel5.UseMnemonic = False
    '
    'NtsLabel4
    '
    Me.NtsLabel4.AutoSize = True
    Me.NtsLabel4.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel4.Location = New System.Drawing.Point(375, 306)
    Me.NtsLabel4.Name = "NtsLabel4"
    Me.NtsLabel4.NTSDbField = ""
    Me.NtsLabel4.Size = New System.Drawing.Size(14, 13)
    Me.NtsLabel4.TabIndex = 110
    Me.NtsLabel4.Text = "A"
    Me.NtsLabel4.Tooltip = ""
    Me.NtsLabel4.UseMnemonic = False
    '
    'edData5a
    '
    Me.edData5a.EditValue = "01/01/2005"
    Me.edData5a.Enabled = False
    Me.edData5a.Location = New System.Drawing.Point(391, 381)
    Me.edData5a.Name = "edData5a"
    Me.edData5a.NTSDbField = ""
    Me.edData5a.NTSForzaVisZoom = False
    Me.edData5a.NTSOldValue = ""
    Me.edData5a.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edData5a.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edData5a.Properties.AutoHeight = False
    Me.edData5a.Properties.MaxLength = 65536
    Me.edData5a.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edData5a.Size = New System.Drawing.Size(101, 20)
    Me.edData5a.TabIndex = 109
    '
    'edData4a
    '
    Me.edData4a.EditValue = "01/01/2005"
    Me.edData4a.Enabled = False
    Me.edData4a.Location = New System.Drawing.Point(391, 355)
    Me.edData4a.Name = "edData4a"
    Me.edData4a.NTSDbField = ""
    Me.edData4a.NTSForzaVisZoom = False
    Me.edData4a.NTSOldValue = ""
    Me.edData4a.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edData4a.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edData4a.Properties.AutoHeight = False
    Me.edData4a.Properties.MaxLength = 65536
    Me.edData4a.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edData4a.Size = New System.Drawing.Size(101, 20)
    Me.edData4a.TabIndex = 108
    '
    'edData3a
    '
    Me.edData3a.EditValue = "01/01/2005"
    Me.edData3a.Enabled = False
    Me.edData3a.Location = New System.Drawing.Point(391, 329)
    Me.edData3a.Name = "edData3a"
    Me.edData3a.NTSDbField = ""
    Me.edData3a.NTSForzaVisZoom = False
    Me.edData3a.NTSOldValue = ""
    Me.edData3a.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edData3a.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edData3a.Properties.AutoHeight = False
    Me.edData3a.Properties.MaxLength = 65536
    Me.edData3a.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edData3a.Size = New System.Drawing.Size(101, 20)
    Me.edData3a.TabIndex = 107
    '
    'edData2a
    '
    Me.edData2a.EditValue = "01/01/2005"
    Me.edData2a.Enabled = False
    Me.edData2a.Location = New System.Drawing.Point(391, 303)
    Me.edData2a.Name = "edData2a"
    Me.edData2a.NTSDbField = ""
    Me.edData2a.NTSForzaVisZoom = False
    Me.edData2a.NTSOldValue = ""
    Me.edData2a.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edData2a.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edData2a.Properties.AutoHeight = False
    Me.edData2a.Properties.MaxLength = 65536
    Me.edData2a.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edData2a.Size = New System.Drawing.Size(101, 20)
    Me.edData2a.TabIndex = 106
    '
    'edData5da
    '
    Me.edData5da.EditValue = "01/01/2005"
    Me.edData5da.Enabled = False
    Me.edData5da.Location = New System.Drawing.Point(211, 381)
    Me.edData5da.Name = "edData5da"
    Me.edData5da.NTSDbField = ""
    Me.edData5da.NTSForzaVisZoom = False
    Me.edData5da.NTSOldValue = ""
    Me.edData5da.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edData5da.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edData5da.Properties.AutoHeight = False
    Me.edData5da.Properties.MaxLength = 65536
    Me.edData5da.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edData5da.Size = New System.Drawing.Size(101, 20)
    Me.edData5da.TabIndex = 105
    '
    'edData4da
    '
    Me.edData4da.EditValue = "01/01/2005"
    Me.edData4da.Enabled = False
    Me.edData4da.Location = New System.Drawing.Point(211, 355)
    Me.edData4da.Name = "edData4da"
    Me.edData4da.NTSDbField = ""
    Me.edData4da.NTSForzaVisZoom = False
    Me.edData4da.NTSOldValue = ""
    Me.edData4da.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edData4da.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edData4da.Properties.AutoHeight = False
    Me.edData4da.Properties.MaxLength = 65536
    Me.edData4da.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edData4da.Size = New System.Drawing.Size(101, 20)
    Me.edData4da.TabIndex = 104
    '
    'edData3da
    '
    Me.edData3da.EditValue = "01/01/2005"
    Me.edData3da.Enabled = False
    Me.edData3da.Location = New System.Drawing.Point(211, 329)
    Me.edData3da.Name = "edData3da"
    Me.edData3da.NTSDbField = ""
    Me.edData3da.NTSForzaVisZoom = False
    Me.edData3da.NTSOldValue = ""
    Me.edData3da.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edData3da.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edData3da.Properties.AutoHeight = False
    Me.edData3da.Properties.MaxLength = 65536
    Me.edData3da.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edData3da.Size = New System.Drawing.Size(101, 20)
    Me.edData3da.TabIndex = 103
    '
    'edData2da
    '
    Me.edData2da.EditValue = "01/01/2005"
    Me.edData2da.Enabled = False
    Me.edData2da.Location = New System.Drawing.Point(211, 303)
    Me.edData2da.Name = "edData2da"
    Me.edData2da.NTSDbField = ""
    Me.edData2da.NTSForzaVisZoom = False
    Me.edData2da.NTSOldValue = ""
    Me.edData2da.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edData2da.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edData2da.Properties.AutoHeight = False
    Me.edData2da.Properties.MaxLength = 65536
    Me.edData2da.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edData2da.Size = New System.Drawing.Size(101, 20)
    Me.edData2da.TabIndex = 102
    '
    'edData1a
    '
    Me.edData1a.EditValue = "01/01/2005"
    Me.edData1a.Enabled = False
    Me.edData1a.Location = New System.Drawing.Point(391, 277)
    Me.edData1a.Name = "edData1a"
    Me.edData1a.NTSDbField = ""
    Me.edData1a.NTSForzaVisZoom = False
    Me.edData1a.NTSOldValue = ""
    Me.edData1a.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edData1a.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edData1a.Properties.AutoHeight = False
    Me.edData1a.Properties.MaxLength = 65536
    Me.edData1a.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edData1a.Size = New System.Drawing.Size(101, 20)
    Me.edData1a.TabIndex = 101
    '
    'edData1da
    '
    Me.edData1da.EditValue = "01/01/2005"
    Me.edData1da.Enabled = False
    Me.edData1da.Location = New System.Drawing.Point(211, 277)
    Me.edData1da.Name = "edData1da"
    Me.edData1da.NTSDbField = ""
    Me.edData1da.NTSForzaVisZoom = False
    Me.edData1da.NTSOldValue = ""
    Me.edData1da.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edData1da.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edData1da.Properties.AutoHeight = False
    Me.edData1da.Properties.MaxLength = 65536
    Me.edData1da.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edData1da.Size = New System.Drawing.Size(101, 20)
    Me.edData1da.TabIndex = 100
    '
    'NtsLabel3
    '
    Me.NtsLabel3.AutoSize = True
    Me.NtsLabel3.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel3.Location = New System.Drawing.Point(375, 282)
    Me.NtsLabel3.Name = "NtsLabel3"
    Me.NtsLabel3.NTSDbField = ""
    Me.NtsLabel3.Size = New System.Drawing.Size(14, 13)
    Me.NtsLabel3.TabIndex = 99
    Me.NtsLabel3.Text = "A"
    Me.NtsLabel3.Tooltip = ""
    Me.NtsLabel3.UseMnemonic = False
    '
    'NtsLabel2
    '
    Me.NtsLabel2.AutoSize = True
    Me.NtsLabel2.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel2.Location = New System.Drawing.Point(188, 282)
    Me.NtsLabel2.Name = "NtsLabel2"
    Me.NtsLabel2.NTSDbField = ""
    Me.NtsLabel2.Size = New System.Drawing.Size(20, 13)
    Me.NtsLabel2.TabIndex = 98
    Me.NtsLabel2.Text = "Da"
    Me.NtsLabel2.Tooltip = ""
    Me.NtsLabel2.UseMnemonic = False
    '
    'ckData1
    '
    Me.ckData1.Location = New System.Drawing.Point(12, 279)
    Me.ckData1.Name = "ckData1"
    Me.ckData1.NTSCheckValue = "S"
    Me.ckData1.NTSUnCheckValue = "N"
    Me.ckData1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckData1.Properties.Appearance.Options.UseBackColor = True
    Me.ckData1.Properties.AutoHeight = False
    Me.ckData1.Properties.Caption = "*ckData1"
    Me.ckData1.Size = New System.Drawing.Size(170, 19)
    Me.ckData1.TabIndex = 97
    '
    'lbDescr10
    '
    Me.lbDescr10.AutoSize = True
    Me.lbDescr10.BackColor = System.Drawing.Color.Transparent
    Me.lbDescr10.Location = New System.Drawing.Point(11, 246)
    Me.lbDescr10.Name = "lbDescr10"
    Me.lbDescr10.NTSDbField = ""
    Me.lbDescr10.Size = New System.Drawing.Size(60, 13)
    Me.lbDescr10.TabIndex = 96
    Me.lbDescr10.Text = "*lbDescr10"
    Me.lbDescr10.Tooltip = ""
    Me.lbDescr10.UseMnemonic = False
    '
    'lbDescr9
    '
    Me.lbDescr9.AutoSize = True
    Me.lbDescr9.BackColor = System.Drawing.Color.Transparent
    Me.lbDescr9.Location = New System.Drawing.Point(11, 220)
    Me.lbDescr9.Name = "lbDescr9"
    Me.lbDescr9.NTSDbField = ""
    Me.lbDescr9.Size = New System.Drawing.Size(54, 13)
    Me.lbDescr9.TabIndex = 95
    Me.lbDescr9.Text = "*lbDescr9"
    Me.lbDescr9.Tooltip = ""
    Me.lbDescr9.UseMnemonic = False
    '
    'lbDescr8
    '
    Me.lbDescr8.AutoSize = True
    Me.lbDescr8.BackColor = System.Drawing.Color.Transparent
    Me.lbDescr8.Location = New System.Drawing.Point(11, 194)
    Me.lbDescr8.Name = "lbDescr8"
    Me.lbDescr8.NTSDbField = ""
    Me.lbDescr8.Size = New System.Drawing.Size(54, 13)
    Me.lbDescr8.TabIndex = 94
    Me.lbDescr8.Text = "*lbDescr8"
    Me.lbDescr8.Tooltip = ""
    Me.lbDescr8.UseMnemonic = False
    '
    'lbDescr7
    '
    Me.lbDescr7.AutoSize = True
    Me.lbDescr7.BackColor = System.Drawing.Color.Transparent
    Me.lbDescr7.Location = New System.Drawing.Point(11, 168)
    Me.lbDescr7.Name = "lbDescr7"
    Me.lbDescr7.NTSDbField = ""
    Me.lbDescr7.Size = New System.Drawing.Size(54, 13)
    Me.lbDescr7.TabIndex = 93
    Me.lbDescr7.Text = "*lbDescr7"
    Me.lbDescr7.Tooltip = ""
    Me.lbDescr7.UseMnemonic = False
    '
    'lbDescr6
    '
    Me.lbDescr6.AutoSize = True
    Me.lbDescr6.BackColor = System.Drawing.Color.Transparent
    Me.lbDescr6.Location = New System.Drawing.Point(11, 142)
    Me.lbDescr6.Name = "lbDescr6"
    Me.lbDescr6.NTSDbField = ""
    Me.lbDescr6.Size = New System.Drawing.Size(54, 13)
    Me.lbDescr6.TabIndex = 92
    Me.lbDescr6.Text = "*lbDescr6"
    Me.lbDescr6.Tooltip = ""
    Me.lbDescr6.UseMnemonic = False
    '
    'lbDescr5
    '
    Me.lbDescr5.AutoSize = True
    Me.lbDescr5.BackColor = System.Drawing.Color.Transparent
    Me.lbDescr5.Location = New System.Drawing.Point(11, 116)
    Me.lbDescr5.Name = "lbDescr5"
    Me.lbDescr5.NTSDbField = ""
    Me.lbDescr5.Size = New System.Drawing.Size(54, 13)
    Me.lbDescr5.TabIndex = 91
    Me.lbDescr5.Text = "*lbDescr5"
    Me.lbDescr5.Tooltip = ""
    Me.lbDescr5.UseMnemonic = False
    '
    'lbDescr4
    '
    Me.lbDescr4.AutoSize = True
    Me.lbDescr4.BackColor = System.Drawing.Color.Transparent
    Me.lbDescr4.Location = New System.Drawing.Point(11, 90)
    Me.lbDescr4.Name = "lbDescr4"
    Me.lbDescr4.NTSDbField = ""
    Me.lbDescr4.Size = New System.Drawing.Size(54, 13)
    Me.lbDescr4.TabIndex = 90
    Me.lbDescr4.Text = "*lbDescr4"
    Me.lbDescr4.Tooltip = ""
    Me.lbDescr4.UseMnemonic = False
    '
    'lbDescr3
    '
    Me.lbDescr3.AutoSize = True
    Me.lbDescr3.BackColor = System.Drawing.Color.Transparent
    Me.lbDescr3.Location = New System.Drawing.Point(11, 64)
    Me.lbDescr3.Name = "lbDescr3"
    Me.lbDescr3.NTSDbField = ""
    Me.lbDescr3.Size = New System.Drawing.Size(54, 13)
    Me.lbDescr3.TabIndex = 89
    Me.lbDescr3.Text = "*lbDescr3"
    Me.lbDescr3.Tooltip = ""
    Me.lbDescr3.UseMnemonic = False
    '
    'lbDescr2
    '
    Me.lbDescr2.AutoSize = True
    Me.lbDescr2.BackColor = System.Drawing.Color.Transparent
    Me.lbDescr2.Location = New System.Drawing.Point(11, 38)
    Me.lbDescr2.Name = "lbDescr2"
    Me.lbDescr2.NTSDbField = ""
    Me.lbDescr2.Size = New System.Drawing.Size(54, 13)
    Me.lbDescr2.TabIndex = 88
    Me.lbDescr2.Text = "*lbDescr2"
    Me.lbDescr2.Tooltip = ""
    Me.lbDescr2.UseMnemonic = False
    '
    'edDescr10
    '
    Me.edDescr10.EditValue = "*"
    Me.edDescr10.Location = New System.Drawing.Point(187, 243)
    Me.edDescr10.Name = "edDescr10"
    Me.edDescr10.NTSDbField = ""
    Me.edDescr10.NTSForzaVisZoom = False
    Me.edDescr10.NTSOldValue = "*"
    Me.edDescr10.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDescr10.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDescr10.Properties.AutoHeight = False
    Me.edDescr10.Properties.MaxLength = 65536
    Me.edDescr10.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDescr10.Size = New System.Drawing.Size(466, 20)
    Me.edDescr10.TabIndex = 87
    '
    'edDescr9
    '
    Me.edDescr9.EditValue = "*"
    Me.edDescr9.Location = New System.Drawing.Point(187, 217)
    Me.edDescr9.Name = "edDescr9"
    Me.edDescr9.NTSDbField = ""
    Me.edDescr9.NTSForzaVisZoom = False
    Me.edDescr9.NTSOldValue = "*"
    Me.edDescr9.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDescr9.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDescr9.Properties.AutoHeight = False
    Me.edDescr9.Properties.MaxLength = 65536
    Me.edDescr9.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDescr9.Size = New System.Drawing.Size(466, 20)
    Me.edDescr9.TabIndex = 86
    '
    'edDescr8
    '
    Me.edDescr8.EditValue = "*"
    Me.edDescr8.Location = New System.Drawing.Point(187, 191)
    Me.edDescr8.Name = "edDescr8"
    Me.edDescr8.NTSDbField = ""
    Me.edDescr8.NTSForzaVisZoom = False
    Me.edDescr8.NTSOldValue = "*"
    Me.edDescr8.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDescr8.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDescr8.Properties.AutoHeight = False
    Me.edDescr8.Properties.MaxLength = 65536
    Me.edDescr8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDescr8.Size = New System.Drawing.Size(466, 20)
    Me.edDescr8.TabIndex = 85
    '
    'edDescr7
    '
    Me.edDescr7.EditValue = "*"
    Me.edDescr7.Location = New System.Drawing.Point(187, 165)
    Me.edDescr7.Name = "edDescr7"
    Me.edDescr7.NTSDbField = ""
    Me.edDescr7.NTSForzaVisZoom = False
    Me.edDescr7.NTSOldValue = "*"
    Me.edDescr7.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDescr7.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDescr7.Properties.AutoHeight = False
    Me.edDescr7.Properties.MaxLength = 65536
    Me.edDescr7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDescr7.Size = New System.Drawing.Size(466, 20)
    Me.edDescr7.TabIndex = 84
    '
    'edDescr6
    '
    Me.edDescr6.EditValue = "*"
    Me.edDescr6.Location = New System.Drawing.Point(187, 139)
    Me.edDescr6.Name = "edDescr6"
    Me.edDescr6.NTSDbField = ""
    Me.edDescr6.NTSForzaVisZoom = False
    Me.edDescr6.NTSOldValue = "*"
    Me.edDescr6.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDescr6.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDescr6.Properties.AutoHeight = False
    Me.edDescr6.Properties.MaxLength = 65536
    Me.edDescr6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDescr6.Size = New System.Drawing.Size(466, 20)
    Me.edDescr6.TabIndex = 83
    '
    'edDescr5
    '
    Me.edDescr5.EditValue = "*"
    Me.edDescr5.Location = New System.Drawing.Point(187, 113)
    Me.edDescr5.Name = "edDescr5"
    Me.edDescr5.NTSDbField = ""
    Me.edDescr5.NTSForzaVisZoom = False
    Me.edDescr5.NTSOldValue = "*"
    Me.edDescr5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDescr5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDescr5.Properties.AutoHeight = False
    Me.edDescr5.Properties.MaxLength = 65536
    Me.edDescr5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDescr5.Size = New System.Drawing.Size(466, 20)
    Me.edDescr5.TabIndex = 82
    '
    'edDescr4
    '
    Me.edDescr4.EditValue = "*"
    Me.edDescr4.Location = New System.Drawing.Point(187, 87)
    Me.edDescr4.Name = "edDescr4"
    Me.edDescr4.NTSDbField = ""
    Me.edDescr4.NTSForzaVisZoom = False
    Me.edDescr4.NTSOldValue = "*"
    Me.edDescr4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDescr4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDescr4.Properties.AutoHeight = False
    Me.edDescr4.Properties.MaxLength = 65536
    Me.edDescr4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDescr4.Size = New System.Drawing.Size(466, 20)
    Me.edDescr4.TabIndex = 81
    '
    'edDescr3
    '
    Me.edDescr3.EditValue = "*"
    Me.edDescr3.Location = New System.Drawing.Point(187, 61)
    Me.edDescr3.Name = "edDescr3"
    Me.edDescr3.NTSDbField = ""
    Me.edDescr3.NTSForzaVisZoom = False
    Me.edDescr3.NTSOldValue = "*"
    Me.edDescr3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDescr3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDescr3.Properties.AutoHeight = False
    Me.edDescr3.Properties.MaxLength = 65536
    Me.edDescr3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDescr3.Size = New System.Drawing.Size(466, 20)
    Me.edDescr3.TabIndex = 80
    '
    'edDescr2
    '
    Me.edDescr2.EditValue = "*"
    Me.edDescr2.Location = New System.Drawing.Point(187, 35)
    Me.edDescr2.Name = "edDescr2"
    Me.edDescr2.NTSDbField = ""
    Me.edDescr2.NTSForzaVisZoom = False
    Me.edDescr2.NTSOldValue = "*"
    Me.edDescr2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDescr2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDescr2.Properties.AutoHeight = False
    Me.edDescr2.Properties.MaxLength = 65536
    Me.edDescr2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDescr2.Size = New System.Drawing.Size(466, 20)
    Me.edDescr2.TabIndex = 79
    '
    'edDescr1
    '
    Me.edDescr1.EditValue = "*"
    Me.edDescr1.Location = New System.Drawing.Point(187, 9)
    Me.edDescr1.Name = "edDescr1"
    Me.edDescr1.NTSDbField = ""
    Me.edDescr1.NTSForzaVisZoom = False
    Me.edDescr1.NTSOldValue = "*"
    Me.edDescr1.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow
    Me.edDescr1.Properties.Appearance.Options.UseBackColor = True
    Me.edDescr1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDescr1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDescr1.Properties.AutoHeight = False
    Me.edDescr1.Properties.MaxLength = 65536
    Me.edDescr1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDescr1.Size = New System.Drawing.Size(466, 20)
    Me.edDescr1.TabIndex = 78
    '
    'lbDescr1
    '
    Me.lbDescr1.AutoSize = True
    Me.lbDescr1.BackColor = System.Drawing.Color.Transparent
    Me.lbDescr1.Location = New System.Drawing.Point(11, 12)
    Me.lbDescr1.Name = "lbDescr1"
    Me.lbDescr1.NTSDbField = ""
    Me.lbDescr1.Size = New System.Drawing.Size(54, 13)
    Me.lbDescr1.TabIndex = 77
    Me.lbDescr1.Text = "*lbDescr1"
    Me.lbDescr1.Tooltip = ""
    Me.lbDescr1.UseMnemonic = False
    '
    'pnTab3
    '
    Me.pnTab3.AllowDrop = True
    Me.pnTab3.Controls.Add(Me.pn3)
    Me.pnTab3.Enable = True
    Me.pnTab3.Name = "pnTab3"
    Me.pnTab3.Size = New System.Drawing.Size(669, 413)
    Me.pnTab3.Text = "*TAB3"
    '
    'pn3
    '
    Me.pn3.AllowDrop = True
    Me.pn3.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pn3.Appearance.Options.UseBackColor = True
    Me.pn3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pn3.Controls.Add(Me.edMemo1)
    Me.pn3.Controls.Add(Me.lbMemo1)
    Me.pn3.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pn3.Location = New System.Drawing.Point(0, 0)
    Me.pn3.Name = "pn3"
    Me.pn3.NTSActiveTrasparency = True
    Me.pn3.Size = New System.Drawing.Size(669, 413)
    Me.pn3.TabIndex = 0
    '
    'edMemo1
    '
    Me.edMemo1.EditValue = "*"
    Me.edMemo1.Location = New System.Drawing.Point(3, 21)
    Me.edMemo1.Name = "edMemo1"
    Me.edMemo1.NTSDbField = ""
    Me.edMemo1.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow
    Me.edMemo1.Properties.Appearance.Options.UseBackColor = True
    Me.edMemo1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMemo1.Size = New System.Drawing.Size(659, 385)
    Me.edMemo1.TabIndex = 45
    '
    'lbMemo1
    '
    Me.lbMemo1.AutoSize = True
    Me.lbMemo1.BackColor = System.Drawing.Color.Transparent
    Me.lbMemo1.Location = New System.Drawing.Point(4, 5)
    Me.lbMemo1.Name = "lbMemo1"
    Me.lbMemo1.NTSDbField = ""
    Me.lbMemo1.Size = New System.Drawing.Size(55, 13)
    Me.lbMemo1.TabIndex = 44
    Me.lbMemo1.Text = "*lbMemo1"
    Me.lbMemo1.Tooltip = ""
    Me.lbMemo1.UseMnemonic = False
    '
    'pnTab4
    '
    Me.pnTab4.AllowDrop = True
    Me.pnTab4.Controls.Add(Me.pn4)
    Me.pnTab4.Enable = True
    Me.pnTab4.Name = "pnTab4"
    Me.pnTab4.Size = New System.Drawing.Size(669, 413)
    Me.pnTab4.Text = "*TAB4"
    '
    'pn4
    '
    Me.pn4.AllowDrop = True
    Me.pn4.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pn4.Appearance.Options.UseBackColor = True
    Me.pn4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pn4.Controls.Add(Me.edMemo2)
    Me.pn4.Controls.Add(Me.lbMemo2)
    Me.pn4.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pn4.Location = New System.Drawing.Point(0, 0)
    Me.pn4.Name = "pn4"
    Me.pn4.NTSActiveTrasparency = True
    Me.pn4.Size = New System.Drawing.Size(669, 413)
    Me.pn4.TabIndex = 0
    '
    'edMemo2
    '
    Me.edMemo2.EditValue = "*"
    Me.edMemo2.Location = New System.Drawing.Point(3, 21)
    Me.edMemo2.Name = "edMemo2"
    Me.edMemo2.NTSDbField = ""
    Me.edMemo2.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow
    Me.edMemo2.Properties.Appearance.Options.UseBackColor = True
    Me.edMemo2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMemo2.Size = New System.Drawing.Size(659, 385)
    Me.edMemo2.TabIndex = 46
    '
    'lbMemo2
    '
    Me.lbMemo2.AutoSize = True
    Me.lbMemo2.BackColor = System.Drawing.Color.Transparent
    Me.lbMemo2.Location = New System.Drawing.Point(5, 5)
    Me.lbMemo2.Name = "lbMemo2"
    Me.lbMemo2.NTSDbField = ""
    Me.lbMemo2.Size = New System.Drawing.Size(55, 13)
    Me.lbMemo2.TabIndex = 45
    Me.lbMemo2.Text = "*lbMemo2"
    Me.lbMemo2.Tooltip = ""
    Me.lbMemo2.UseMnemonic = False
    '
    'pnTab5
    '
    Me.pnTab5.AllowDrop = True
    Me.pnTab5.Controls.Add(Me.pn5)
    Me.pnTab5.Enable = True
    Me.pnTab5.Name = "pnTab5"
    Me.pnTab5.Size = New System.Drawing.Size(669, 413)
    Me.pnTab5.Text = "TAB5"
    '
    'pn5
    '
    Me.pn5.AllowDrop = True
    Me.pn5.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pn5.Appearance.Options.UseBackColor = True
    Me.pn5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pn5.Controls.Add(Me.edNum10a)
    Me.pn5.Controls.Add(Me.edNum9a)
    Me.pn5.Controls.Add(Me.edNum8a)
    Me.pn5.Controls.Add(Me.edNum7a)
    Me.pn5.Controls.Add(Me.edNum6a)
    Me.pn5.Controls.Add(Me.edNum5a)
    Me.pn5.Controls.Add(Me.edNum4a)
    Me.pn5.Controls.Add(Me.edNum3a)
    Me.pn5.Controls.Add(Me.edNum2a)
    Me.pn5.Controls.Add(Me.edNum1a)
    Me.pn5.Controls.Add(Me.edNum10da)
    Me.pn5.Controls.Add(Me.edNum9da)
    Me.pn5.Controls.Add(Me.edNum8da)
    Me.pn5.Controls.Add(Me.edNum7da)
    Me.pn5.Controls.Add(Me.edNum6da)
    Me.pn5.Controls.Add(Me.edNum5da)
    Me.pn5.Controls.Add(Me.edNum4da)
    Me.pn5.Controls.Add(Me.edNum3da)
    Me.pn5.Controls.Add(Me.edNum2da)
    Me.pn5.Controls.Add(Me.edNum1da)
    Me.pn5.Controls.Add(Me.ckNum10)
    Me.pn5.Controls.Add(Me.ckNum9)
    Me.pn5.Controls.Add(Me.ckNum8)
    Me.pn5.Controls.Add(Me.ckNum7)
    Me.pn5.Controls.Add(Me.NtsLabel22)
    Me.pn5.Controls.Add(Me.NtsLabel23)
    Me.pn5.Controls.Add(Me.NtsLabel24)
    Me.pn5.Controls.Add(Me.NtsLabel25)
    Me.pn5.Controls.Add(Me.NtsLabel26)
    Me.pn5.Controls.Add(Me.NtsLabel27)
    Me.pn5.Controls.Add(Me.NtsLabel28)
    Me.pn5.Controls.Add(Me.NtsLabel29)
    Me.pn5.Controls.Add(Me.NtsLabel30)
    Me.pn5.Controls.Add(Me.NtsLabel31)
    Me.pn5.Controls.Add(Me.ckNum6)
    Me.pn5.Controls.Add(Me.ckNum5)
    Me.pn5.Controls.Add(Me.ckNum4)
    Me.pn5.Controls.Add(Me.ckNum3)
    Me.pn5.Controls.Add(Me.ckNum2)
    Me.pn5.Controls.Add(Me.NtsLabel12)
    Me.pn5.Controls.Add(Me.NtsLabel13)
    Me.pn5.Controls.Add(Me.NtsLabel14)
    Me.pn5.Controls.Add(Me.NtsLabel15)
    Me.pn5.Controls.Add(Me.NtsLabel16)
    Me.pn5.Controls.Add(Me.NtsLabel17)
    Me.pn5.Controls.Add(Me.NtsLabel18)
    Me.pn5.Controls.Add(Me.NtsLabel19)
    Me.pn5.Controls.Add(Me.NtsLabel20)
    Me.pn5.Controls.Add(Me.NtsLabel21)
    Me.pn5.Controls.Add(Me.ckNum1)
    Me.pn5.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pn5.Location = New System.Drawing.Point(0, 0)
    Me.pn5.Name = "pn5"
    Me.pn5.NTSActiveTrasparency = True
    Me.pn5.Size = New System.Drawing.Size(669, 413)
    Me.pn5.TabIndex = 0
    '
    'edNum10a
    '
    Me.edNum10a.EditValue = "0"
    Me.edNum10a.Enabled = False
    Me.edNum10a.Location = New System.Drawing.Point(384, 246)
    Me.edNum10a.Name = "edNum10a"
    Me.edNum10a.NTSDbField = ""
    Me.edNum10a.NTSFormat = "0"
    Me.edNum10a.NTSForzaVisZoom = False
    Me.edNum10a.NTSOldValue = ""
    Me.edNum10a.Properties.Appearance.Options.UseTextOptions = True
    Me.edNum10a.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNum10a.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNum10a.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNum10a.Properties.AutoHeight = False
    Me.edNum10a.Properties.MaxLength = 65536
    Me.edNum10a.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNum10a.Size = New System.Drawing.Size(143, 20)
    Me.edNum10a.TabIndex = 176
    '
    'edNum9a
    '
    Me.edNum9a.EditValue = "0"
    Me.edNum9a.Enabled = False
    Me.edNum9a.Location = New System.Drawing.Point(384, 220)
    Me.edNum9a.Name = "edNum9a"
    Me.edNum9a.NTSDbField = ""
    Me.edNum9a.NTSFormat = "0"
    Me.edNum9a.NTSForzaVisZoom = False
    Me.edNum9a.NTSOldValue = ""
    Me.edNum9a.Properties.Appearance.Options.UseTextOptions = True
    Me.edNum9a.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNum9a.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNum9a.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNum9a.Properties.AutoHeight = False
    Me.edNum9a.Properties.MaxLength = 65536
    Me.edNum9a.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNum9a.Size = New System.Drawing.Size(143, 20)
    Me.edNum9a.TabIndex = 175
    '
    'edNum8a
    '
    Me.edNum8a.EditValue = "0"
    Me.edNum8a.Enabled = False
    Me.edNum8a.Location = New System.Drawing.Point(384, 194)
    Me.edNum8a.Name = "edNum8a"
    Me.edNum8a.NTSDbField = ""
    Me.edNum8a.NTSFormat = "0"
    Me.edNum8a.NTSForzaVisZoom = False
    Me.edNum8a.NTSOldValue = ""
    Me.edNum8a.Properties.Appearance.Options.UseTextOptions = True
    Me.edNum8a.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNum8a.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNum8a.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNum8a.Properties.AutoHeight = False
    Me.edNum8a.Properties.MaxLength = 65536
    Me.edNum8a.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNum8a.Size = New System.Drawing.Size(143, 20)
    Me.edNum8a.TabIndex = 174
    '
    'edNum7a
    '
    Me.edNum7a.EditValue = "0"
    Me.edNum7a.Enabled = False
    Me.edNum7a.Location = New System.Drawing.Point(384, 168)
    Me.edNum7a.Name = "edNum7a"
    Me.edNum7a.NTSDbField = ""
    Me.edNum7a.NTSFormat = "0"
    Me.edNum7a.NTSForzaVisZoom = False
    Me.edNum7a.NTSOldValue = ""
    Me.edNum7a.Properties.Appearance.Options.UseTextOptions = True
    Me.edNum7a.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNum7a.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNum7a.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNum7a.Properties.AutoHeight = False
    Me.edNum7a.Properties.MaxLength = 65536
    Me.edNum7a.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNum7a.Size = New System.Drawing.Size(143, 20)
    Me.edNum7a.TabIndex = 173
    '
    'edNum6a
    '
    Me.edNum6a.EditValue = "0"
    Me.edNum6a.Enabled = False
    Me.edNum6a.Location = New System.Drawing.Point(384, 144)
    Me.edNum6a.Name = "edNum6a"
    Me.edNum6a.NTSDbField = ""
    Me.edNum6a.NTSFormat = "0"
    Me.edNum6a.NTSForzaVisZoom = False
    Me.edNum6a.NTSOldValue = ""
    Me.edNum6a.Properties.Appearance.Options.UseTextOptions = True
    Me.edNum6a.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNum6a.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNum6a.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNum6a.Properties.AutoHeight = False
    Me.edNum6a.Properties.MaxLength = 65536
    Me.edNum6a.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNum6a.Size = New System.Drawing.Size(143, 20)
    Me.edNum6a.TabIndex = 172
    '
    'edNum5a
    '
    Me.edNum5a.EditValue = "0"
    Me.edNum5a.Enabled = False
    Me.edNum5a.Location = New System.Drawing.Point(384, 119)
    Me.edNum5a.Name = "edNum5a"
    Me.edNum5a.NTSDbField = ""
    Me.edNum5a.NTSFormat = "0"
    Me.edNum5a.NTSForzaVisZoom = False
    Me.edNum5a.NTSOldValue = ""
    Me.edNum5a.Properties.Appearance.Options.UseTextOptions = True
    Me.edNum5a.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNum5a.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNum5a.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNum5a.Properties.AutoHeight = False
    Me.edNum5a.Properties.MaxLength = 65536
    Me.edNum5a.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNum5a.Size = New System.Drawing.Size(143, 20)
    Me.edNum5a.TabIndex = 171
    '
    'edNum4a
    '
    Me.edNum4a.EditValue = "0"
    Me.edNum4a.Enabled = False
    Me.edNum4a.Location = New System.Drawing.Point(384, 93)
    Me.edNum4a.Name = "edNum4a"
    Me.edNum4a.NTSDbField = ""
    Me.edNum4a.NTSFormat = "0"
    Me.edNum4a.NTSForzaVisZoom = False
    Me.edNum4a.NTSOldValue = ""
    Me.edNum4a.Properties.Appearance.Options.UseTextOptions = True
    Me.edNum4a.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNum4a.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNum4a.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNum4a.Properties.AutoHeight = False
    Me.edNum4a.Properties.MaxLength = 65536
    Me.edNum4a.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNum4a.Size = New System.Drawing.Size(143, 20)
    Me.edNum4a.TabIndex = 170
    '
    'edNum3a
    '
    Me.edNum3a.EditValue = "0"
    Me.edNum3a.Enabled = False
    Me.edNum3a.Location = New System.Drawing.Point(384, 67)
    Me.edNum3a.Name = "edNum3a"
    Me.edNum3a.NTSDbField = ""
    Me.edNum3a.NTSFormat = "0"
    Me.edNum3a.NTSForzaVisZoom = False
    Me.edNum3a.NTSOldValue = ""
    Me.edNum3a.Properties.Appearance.Options.UseTextOptions = True
    Me.edNum3a.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNum3a.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNum3a.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNum3a.Properties.AutoHeight = False
    Me.edNum3a.Properties.MaxLength = 65536
    Me.edNum3a.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNum3a.Size = New System.Drawing.Size(143, 20)
    Me.edNum3a.TabIndex = 169
    '
    'edNum2a
    '
    Me.edNum2a.EditValue = "0"
    Me.edNum2a.Enabled = False
    Me.edNum2a.Location = New System.Drawing.Point(384, 41)
    Me.edNum2a.Name = "edNum2a"
    Me.edNum2a.NTSDbField = ""
    Me.edNum2a.NTSFormat = "0"
    Me.edNum2a.NTSForzaVisZoom = False
    Me.edNum2a.NTSOldValue = ""
    Me.edNum2a.Properties.Appearance.Options.UseTextOptions = True
    Me.edNum2a.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNum2a.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNum2a.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNum2a.Properties.AutoHeight = False
    Me.edNum2a.Properties.MaxLength = 65536
    Me.edNum2a.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNum2a.Size = New System.Drawing.Size(143, 20)
    Me.edNum2a.TabIndex = 168
    '
    'edNum1a
    '
    Me.edNum1a.EditValue = "0"
    Me.edNum1a.Enabled = False
    Me.edNum1a.Location = New System.Drawing.Point(384, 17)
    Me.edNum1a.Name = "edNum1a"
    Me.edNum1a.NTSDbField = ""
    Me.edNum1a.NTSFormat = "0"
    Me.edNum1a.NTSForzaVisZoom = False
    Me.edNum1a.NTSOldValue = ""
    Me.edNum1a.Properties.Appearance.Options.UseTextOptions = True
    Me.edNum1a.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNum1a.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNum1a.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNum1a.Properties.AutoHeight = False
    Me.edNum1a.Properties.MaxLength = 65536
    Me.edNum1a.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNum1a.Size = New System.Drawing.Size(143, 20)
    Me.edNum1a.TabIndex = 167
    '
    'edNum10da
    '
    Me.edNum10da.EditValue = "0"
    Me.edNum10da.Enabled = False
    Me.edNum10da.Location = New System.Drawing.Point(204, 246)
    Me.edNum10da.Name = "edNum10da"
    Me.edNum10da.NTSDbField = ""
    Me.edNum10da.NTSFormat = "0"
    Me.edNum10da.NTSForzaVisZoom = False
    Me.edNum10da.NTSOldValue = ""
    Me.edNum10da.Properties.Appearance.Options.UseTextOptions = True
    Me.edNum10da.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNum10da.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNum10da.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNum10da.Properties.AutoHeight = False
    Me.edNum10da.Properties.MaxLength = 65536
    Me.edNum10da.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNum10da.Size = New System.Drawing.Size(143, 20)
    Me.edNum10da.TabIndex = 166
    '
    'edNum9da
    '
    Me.edNum9da.EditValue = "0"
    Me.edNum9da.Enabled = False
    Me.edNum9da.Location = New System.Drawing.Point(204, 220)
    Me.edNum9da.Name = "edNum9da"
    Me.edNum9da.NTSDbField = ""
    Me.edNum9da.NTSFormat = "0"
    Me.edNum9da.NTSForzaVisZoom = False
    Me.edNum9da.NTSOldValue = ""
    Me.edNum9da.Properties.Appearance.Options.UseTextOptions = True
    Me.edNum9da.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNum9da.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNum9da.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNum9da.Properties.AutoHeight = False
    Me.edNum9da.Properties.MaxLength = 65536
    Me.edNum9da.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNum9da.Size = New System.Drawing.Size(143, 20)
    Me.edNum9da.TabIndex = 165
    '
    'edNum8da
    '
    Me.edNum8da.EditValue = "0"
    Me.edNum8da.Enabled = False
    Me.edNum8da.Location = New System.Drawing.Point(204, 194)
    Me.edNum8da.Name = "edNum8da"
    Me.edNum8da.NTSDbField = ""
    Me.edNum8da.NTSFormat = "0"
    Me.edNum8da.NTSForzaVisZoom = False
    Me.edNum8da.NTSOldValue = ""
    Me.edNum8da.Properties.Appearance.Options.UseTextOptions = True
    Me.edNum8da.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNum8da.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNum8da.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNum8da.Properties.AutoHeight = False
    Me.edNum8da.Properties.MaxLength = 65536
    Me.edNum8da.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNum8da.Size = New System.Drawing.Size(143, 20)
    Me.edNum8da.TabIndex = 164
    '
    'edNum7da
    '
    Me.edNum7da.EditValue = "0"
    Me.edNum7da.Enabled = False
    Me.edNum7da.Location = New System.Drawing.Point(204, 168)
    Me.edNum7da.Name = "edNum7da"
    Me.edNum7da.NTSDbField = ""
    Me.edNum7da.NTSFormat = "0"
    Me.edNum7da.NTSForzaVisZoom = False
    Me.edNum7da.NTSOldValue = ""
    Me.edNum7da.Properties.Appearance.Options.UseTextOptions = True
    Me.edNum7da.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNum7da.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNum7da.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNum7da.Properties.AutoHeight = False
    Me.edNum7da.Properties.MaxLength = 65536
    Me.edNum7da.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNum7da.Size = New System.Drawing.Size(143, 20)
    Me.edNum7da.TabIndex = 163
    '
    'edNum6da
    '
    Me.edNum6da.EditValue = "0"
    Me.edNum6da.Enabled = False
    Me.edNum6da.Location = New System.Drawing.Point(204, 144)
    Me.edNum6da.Name = "edNum6da"
    Me.edNum6da.NTSDbField = ""
    Me.edNum6da.NTSFormat = "0"
    Me.edNum6da.NTSForzaVisZoom = False
    Me.edNum6da.NTSOldValue = ""
    Me.edNum6da.Properties.Appearance.Options.UseTextOptions = True
    Me.edNum6da.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNum6da.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNum6da.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNum6da.Properties.AutoHeight = False
    Me.edNum6da.Properties.MaxLength = 65536
    Me.edNum6da.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNum6da.Size = New System.Drawing.Size(143, 20)
    Me.edNum6da.TabIndex = 162
    '
    'edNum5da
    '
    Me.edNum5da.EditValue = "0"
    Me.edNum5da.Enabled = False
    Me.edNum5da.Location = New System.Drawing.Point(204, 119)
    Me.edNum5da.Name = "edNum5da"
    Me.edNum5da.NTSDbField = ""
    Me.edNum5da.NTSFormat = "0"
    Me.edNum5da.NTSForzaVisZoom = False
    Me.edNum5da.NTSOldValue = ""
    Me.edNum5da.Properties.Appearance.Options.UseTextOptions = True
    Me.edNum5da.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNum5da.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNum5da.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNum5da.Properties.AutoHeight = False
    Me.edNum5da.Properties.MaxLength = 65536
    Me.edNum5da.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNum5da.Size = New System.Drawing.Size(143, 20)
    Me.edNum5da.TabIndex = 161
    '
    'edNum4da
    '
    Me.edNum4da.EditValue = "0"
    Me.edNum4da.Enabled = False
    Me.edNum4da.Location = New System.Drawing.Point(204, 93)
    Me.edNum4da.Name = "edNum4da"
    Me.edNum4da.NTSDbField = ""
    Me.edNum4da.NTSFormat = "0"
    Me.edNum4da.NTSForzaVisZoom = False
    Me.edNum4da.NTSOldValue = ""
    Me.edNum4da.Properties.Appearance.Options.UseTextOptions = True
    Me.edNum4da.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNum4da.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNum4da.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNum4da.Properties.AutoHeight = False
    Me.edNum4da.Properties.MaxLength = 65536
    Me.edNum4da.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNum4da.Size = New System.Drawing.Size(143, 20)
    Me.edNum4da.TabIndex = 160
    '
    'edNum3da
    '
    Me.edNum3da.EditValue = "0"
    Me.edNum3da.Enabled = False
    Me.edNum3da.Location = New System.Drawing.Point(204, 67)
    Me.edNum3da.Name = "edNum3da"
    Me.edNum3da.NTSDbField = ""
    Me.edNum3da.NTSFormat = "0"
    Me.edNum3da.NTSForzaVisZoom = False
    Me.edNum3da.NTSOldValue = ""
    Me.edNum3da.Properties.Appearance.Options.UseTextOptions = True
    Me.edNum3da.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNum3da.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNum3da.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNum3da.Properties.AutoHeight = False
    Me.edNum3da.Properties.MaxLength = 65536
    Me.edNum3da.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNum3da.Size = New System.Drawing.Size(143, 20)
    Me.edNum3da.TabIndex = 159
    '
    'edNum2da
    '
    Me.edNum2da.EditValue = "0"
    Me.edNum2da.Enabled = False
    Me.edNum2da.Location = New System.Drawing.Point(204, 41)
    Me.edNum2da.Name = "edNum2da"
    Me.edNum2da.NTSDbField = ""
    Me.edNum2da.NTSFormat = "0"
    Me.edNum2da.NTSForzaVisZoom = False
    Me.edNum2da.NTSOldValue = ""
    Me.edNum2da.Properties.Appearance.Options.UseTextOptions = True
    Me.edNum2da.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNum2da.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNum2da.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNum2da.Properties.AutoHeight = False
    Me.edNum2da.Properties.MaxLength = 65536
    Me.edNum2da.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNum2da.Size = New System.Drawing.Size(143, 20)
    Me.edNum2da.TabIndex = 158
    '
    'edNum1da
    '
    Me.edNum1da.EditValue = "0"
    Me.edNum1da.Enabled = False
    Me.edNum1da.Location = New System.Drawing.Point(204, 17)
    Me.edNum1da.Name = "edNum1da"
    Me.edNum1da.NTSDbField = ""
    Me.edNum1da.NTSFormat = "0"
    Me.edNum1da.NTSForzaVisZoom = False
    Me.edNum1da.NTSOldValue = ""
    Me.edNum1da.Properties.Appearance.Options.UseTextOptions = True
    Me.edNum1da.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNum1da.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNum1da.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNum1da.Properties.AutoHeight = False
    Me.edNum1da.Properties.MaxLength = 65536
    Me.edNum1da.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNum1da.Size = New System.Drawing.Size(143, 20)
    Me.edNum1da.TabIndex = 157
    '
    'ckNum10
    '
    Me.ckNum10.Location = New System.Drawing.Point(5, 248)
    Me.ckNum10.Name = "ckNum10"
    Me.ckNum10.NTSCheckValue = "S"
    Me.ckNum10.NTSUnCheckValue = "N"
    Me.ckNum10.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNum10.Properties.Appearance.Options.UseBackColor = True
    Me.ckNum10.Properties.AutoHeight = False
    Me.ckNum10.Properties.Caption = "*ckNum10"
    Me.ckNum10.Size = New System.Drawing.Size(170, 19)
    Me.ckNum10.TabIndex = 156
    '
    'ckNum9
    '
    Me.ckNum9.Location = New System.Drawing.Point(5, 222)
    Me.ckNum9.Name = "ckNum9"
    Me.ckNum9.NTSCheckValue = "S"
    Me.ckNum9.NTSUnCheckValue = "N"
    Me.ckNum9.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNum9.Properties.Appearance.Options.UseBackColor = True
    Me.ckNum9.Properties.AutoHeight = False
    Me.ckNum9.Properties.Caption = "*ckNum9"
    Me.ckNum9.Size = New System.Drawing.Size(170, 19)
    Me.ckNum9.TabIndex = 155
    '
    'ckNum8
    '
    Me.ckNum8.Location = New System.Drawing.Point(5, 196)
    Me.ckNum8.Name = "ckNum8"
    Me.ckNum8.NTSCheckValue = "S"
    Me.ckNum8.NTSUnCheckValue = "N"
    Me.ckNum8.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNum8.Properties.Appearance.Options.UseBackColor = True
    Me.ckNum8.Properties.AutoHeight = False
    Me.ckNum8.Properties.Caption = "*ckNum8"
    Me.ckNum8.Size = New System.Drawing.Size(170, 19)
    Me.ckNum8.TabIndex = 154
    '
    'ckNum7
    '
    Me.ckNum7.Location = New System.Drawing.Point(5, 170)
    Me.ckNum7.Name = "ckNum7"
    Me.ckNum7.NTSCheckValue = "S"
    Me.ckNum7.NTSUnCheckValue = "N"
    Me.ckNum7.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNum7.Properties.Appearance.Options.UseBackColor = True
    Me.ckNum7.Properties.AutoHeight = False
    Me.ckNum7.Properties.Caption = "*ckNum7"
    Me.ckNum7.Size = New System.Drawing.Size(170, 19)
    Me.ckNum7.TabIndex = 153
    '
    'NtsLabel22
    '
    Me.NtsLabel22.AutoSize = True
    Me.NtsLabel22.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel22.Location = New System.Drawing.Point(181, 249)
    Me.NtsLabel22.Name = "NtsLabel22"
    Me.NtsLabel22.NTSDbField = ""
    Me.NtsLabel22.Size = New System.Drawing.Size(20, 13)
    Me.NtsLabel22.TabIndex = 152
    Me.NtsLabel22.Text = "Da"
    Me.NtsLabel22.Tooltip = ""
    Me.NtsLabel22.UseMnemonic = False
    '
    'NtsLabel23
    '
    Me.NtsLabel23.AutoSize = True
    Me.NtsLabel23.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel23.Location = New System.Drawing.Point(181, 223)
    Me.NtsLabel23.Name = "NtsLabel23"
    Me.NtsLabel23.NTSDbField = ""
    Me.NtsLabel23.Size = New System.Drawing.Size(20, 13)
    Me.NtsLabel23.TabIndex = 151
    Me.NtsLabel23.Text = "Da"
    Me.NtsLabel23.Tooltip = ""
    Me.NtsLabel23.UseMnemonic = False
    '
    'NtsLabel24
    '
    Me.NtsLabel24.AutoSize = True
    Me.NtsLabel24.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel24.Location = New System.Drawing.Point(181, 197)
    Me.NtsLabel24.Name = "NtsLabel24"
    Me.NtsLabel24.NTSDbField = ""
    Me.NtsLabel24.Size = New System.Drawing.Size(20, 13)
    Me.NtsLabel24.TabIndex = 150
    Me.NtsLabel24.Text = "Da"
    Me.NtsLabel24.Tooltip = ""
    Me.NtsLabel24.UseMnemonic = False
    '
    'NtsLabel25
    '
    Me.NtsLabel25.AutoSize = True
    Me.NtsLabel25.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel25.Location = New System.Drawing.Point(181, 171)
    Me.NtsLabel25.Name = "NtsLabel25"
    Me.NtsLabel25.NTSDbField = ""
    Me.NtsLabel25.Size = New System.Drawing.Size(20, 13)
    Me.NtsLabel25.TabIndex = 149
    Me.NtsLabel25.Text = "Da"
    Me.NtsLabel25.Tooltip = ""
    Me.NtsLabel25.UseMnemonic = False
    '
    'NtsLabel26
    '
    Me.NtsLabel26.AutoSize = True
    Me.NtsLabel26.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel26.Location = New System.Drawing.Point(368, 249)
    Me.NtsLabel26.Name = "NtsLabel26"
    Me.NtsLabel26.NTSDbField = ""
    Me.NtsLabel26.Size = New System.Drawing.Size(14, 13)
    Me.NtsLabel26.TabIndex = 148
    Me.NtsLabel26.Text = "A"
    Me.NtsLabel26.Tooltip = ""
    Me.NtsLabel26.UseMnemonic = False
    '
    'NtsLabel27
    '
    Me.NtsLabel27.AutoSize = True
    Me.NtsLabel27.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel27.Location = New System.Drawing.Point(368, 223)
    Me.NtsLabel27.Name = "NtsLabel27"
    Me.NtsLabel27.NTSDbField = ""
    Me.NtsLabel27.Size = New System.Drawing.Size(14, 13)
    Me.NtsLabel27.TabIndex = 147
    Me.NtsLabel27.Text = "A"
    Me.NtsLabel27.Tooltip = ""
    Me.NtsLabel27.UseMnemonic = False
    '
    'NtsLabel28
    '
    Me.NtsLabel28.AutoSize = True
    Me.NtsLabel28.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel28.Location = New System.Drawing.Point(368, 197)
    Me.NtsLabel28.Name = "NtsLabel28"
    Me.NtsLabel28.NTSDbField = ""
    Me.NtsLabel28.Size = New System.Drawing.Size(14, 13)
    Me.NtsLabel28.TabIndex = 146
    Me.NtsLabel28.Text = "A"
    Me.NtsLabel28.Tooltip = ""
    Me.NtsLabel28.UseMnemonic = False
    '
    'NtsLabel29
    '
    Me.NtsLabel29.AutoSize = True
    Me.NtsLabel29.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel29.Location = New System.Drawing.Point(368, 171)
    Me.NtsLabel29.Name = "NtsLabel29"
    Me.NtsLabel29.NTSDbField = ""
    Me.NtsLabel29.Size = New System.Drawing.Size(14, 13)
    Me.NtsLabel29.TabIndex = 145
    Me.NtsLabel29.Text = "A"
    Me.NtsLabel29.Tooltip = ""
    Me.NtsLabel29.UseMnemonic = False
    '
    'NtsLabel30
    '
    Me.NtsLabel30.AutoSize = True
    Me.NtsLabel30.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel30.Location = New System.Drawing.Point(368, 147)
    Me.NtsLabel30.Name = "NtsLabel30"
    Me.NtsLabel30.NTSDbField = ""
    Me.NtsLabel30.Size = New System.Drawing.Size(14, 13)
    Me.NtsLabel30.TabIndex = 144
    Me.NtsLabel30.Text = "A"
    Me.NtsLabel30.Tooltip = ""
    Me.NtsLabel30.UseMnemonic = False
    '
    'NtsLabel31
    '
    Me.NtsLabel31.AutoSize = True
    Me.NtsLabel31.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel31.Location = New System.Drawing.Point(181, 147)
    Me.NtsLabel31.Name = "NtsLabel31"
    Me.NtsLabel31.NTSDbField = ""
    Me.NtsLabel31.Size = New System.Drawing.Size(20, 13)
    Me.NtsLabel31.TabIndex = 143
    Me.NtsLabel31.Text = "Da"
    Me.NtsLabel31.Tooltip = ""
    Me.NtsLabel31.UseMnemonic = False
    '
    'ckNum6
    '
    Me.ckNum6.Location = New System.Drawing.Point(5, 144)
    Me.ckNum6.Name = "ckNum6"
    Me.ckNum6.NTSCheckValue = "S"
    Me.ckNum6.NTSUnCheckValue = "N"
    Me.ckNum6.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNum6.Properties.Appearance.Options.UseBackColor = True
    Me.ckNum6.Properties.AutoHeight = False
    Me.ckNum6.Properties.Caption = "*ckNum6"
    Me.ckNum6.Size = New System.Drawing.Size(170, 19)
    Me.ckNum6.TabIndex = 142
    '
    'ckNum5
    '
    Me.ckNum5.Location = New System.Drawing.Point(5, 121)
    Me.ckNum5.Name = "ckNum5"
    Me.ckNum5.NTSCheckValue = "S"
    Me.ckNum5.NTSUnCheckValue = "N"
    Me.ckNum5.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNum5.Properties.Appearance.Options.UseBackColor = True
    Me.ckNum5.Properties.AutoHeight = False
    Me.ckNum5.Properties.Caption = "*ckNum5"
    Me.ckNum5.Size = New System.Drawing.Size(170, 19)
    Me.ckNum5.TabIndex = 141
    '
    'ckNum4
    '
    Me.ckNum4.Location = New System.Drawing.Point(5, 95)
    Me.ckNum4.Name = "ckNum4"
    Me.ckNum4.NTSCheckValue = "S"
    Me.ckNum4.NTSUnCheckValue = "N"
    Me.ckNum4.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNum4.Properties.Appearance.Options.UseBackColor = True
    Me.ckNum4.Properties.AutoHeight = False
    Me.ckNum4.Properties.Caption = "*ckNum4"
    Me.ckNum4.Size = New System.Drawing.Size(170, 19)
    Me.ckNum4.TabIndex = 140
    '
    'ckNum3
    '
    Me.ckNum3.Location = New System.Drawing.Point(5, 69)
    Me.ckNum3.Name = "ckNum3"
    Me.ckNum3.NTSCheckValue = "S"
    Me.ckNum3.NTSUnCheckValue = "N"
    Me.ckNum3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNum3.Properties.Appearance.Options.UseBackColor = True
    Me.ckNum3.Properties.AutoHeight = False
    Me.ckNum3.Properties.Caption = "*ckNum3"
    Me.ckNum3.Size = New System.Drawing.Size(170, 19)
    Me.ckNum3.TabIndex = 139
    '
    'ckNum2
    '
    Me.ckNum2.Location = New System.Drawing.Point(5, 43)
    Me.ckNum2.Name = "ckNum2"
    Me.ckNum2.NTSCheckValue = "S"
    Me.ckNum2.NTSUnCheckValue = "N"
    Me.ckNum2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNum2.Properties.Appearance.Options.UseBackColor = True
    Me.ckNum2.Properties.AutoHeight = False
    Me.ckNum2.Properties.Caption = "*ckNum2"
    Me.ckNum2.Size = New System.Drawing.Size(170, 19)
    Me.ckNum2.TabIndex = 138
    '
    'NtsLabel12
    '
    Me.NtsLabel12.AutoSize = True
    Me.NtsLabel12.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel12.Location = New System.Drawing.Point(181, 122)
    Me.NtsLabel12.Name = "NtsLabel12"
    Me.NtsLabel12.NTSDbField = ""
    Me.NtsLabel12.Size = New System.Drawing.Size(20, 13)
    Me.NtsLabel12.TabIndex = 137
    Me.NtsLabel12.Text = "Da"
    Me.NtsLabel12.Tooltip = ""
    Me.NtsLabel12.UseMnemonic = False
    '
    'NtsLabel13
    '
    Me.NtsLabel13.AutoSize = True
    Me.NtsLabel13.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel13.Location = New System.Drawing.Point(181, 96)
    Me.NtsLabel13.Name = "NtsLabel13"
    Me.NtsLabel13.NTSDbField = ""
    Me.NtsLabel13.Size = New System.Drawing.Size(20, 13)
    Me.NtsLabel13.TabIndex = 136
    Me.NtsLabel13.Text = "Da"
    Me.NtsLabel13.Tooltip = ""
    Me.NtsLabel13.UseMnemonic = False
    '
    'NtsLabel14
    '
    Me.NtsLabel14.AutoSize = True
    Me.NtsLabel14.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel14.Location = New System.Drawing.Point(181, 70)
    Me.NtsLabel14.Name = "NtsLabel14"
    Me.NtsLabel14.NTSDbField = ""
    Me.NtsLabel14.Size = New System.Drawing.Size(20, 13)
    Me.NtsLabel14.TabIndex = 135
    Me.NtsLabel14.Text = "Da"
    Me.NtsLabel14.Tooltip = ""
    Me.NtsLabel14.UseMnemonic = False
    '
    'NtsLabel15
    '
    Me.NtsLabel15.AutoSize = True
    Me.NtsLabel15.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel15.Location = New System.Drawing.Point(181, 44)
    Me.NtsLabel15.Name = "NtsLabel15"
    Me.NtsLabel15.NTSDbField = ""
    Me.NtsLabel15.Size = New System.Drawing.Size(20, 13)
    Me.NtsLabel15.TabIndex = 134
    Me.NtsLabel15.Text = "Da"
    Me.NtsLabel15.Tooltip = ""
    Me.NtsLabel15.UseMnemonic = False
    '
    'NtsLabel16
    '
    Me.NtsLabel16.AutoSize = True
    Me.NtsLabel16.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel16.Location = New System.Drawing.Point(368, 122)
    Me.NtsLabel16.Name = "NtsLabel16"
    Me.NtsLabel16.NTSDbField = ""
    Me.NtsLabel16.Size = New System.Drawing.Size(14, 13)
    Me.NtsLabel16.TabIndex = 133
    Me.NtsLabel16.Text = "A"
    Me.NtsLabel16.Tooltip = ""
    Me.NtsLabel16.UseMnemonic = False
    '
    'NtsLabel17
    '
    Me.NtsLabel17.AutoSize = True
    Me.NtsLabel17.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel17.Location = New System.Drawing.Point(368, 96)
    Me.NtsLabel17.Name = "NtsLabel17"
    Me.NtsLabel17.NTSDbField = ""
    Me.NtsLabel17.Size = New System.Drawing.Size(14, 13)
    Me.NtsLabel17.TabIndex = 132
    Me.NtsLabel17.Text = "A"
    Me.NtsLabel17.Tooltip = ""
    Me.NtsLabel17.UseMnemonic = False
    '
    'NtsLabel18
    '
    Me.NtsLabel18.AutoSize = True
    Me.NtsLabel18.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel18.Location = New System.Drawing.Point(368, 70)
    Me.NtsLabel18.Name = "NtsLabel18"
    Me.NtsLabel18.NTSDbField = ""
    Me.NtsLabel18.Size = New System.Drawing.Size(14, 13)
    Me.NtsLabel18.TabIndex = 131
    Me.NtsLabel18.Text = "A"
    Me.NtsLabel18.Tooltip = ""
    Me.NtsLabel18.UseMnemonic = False
    '
    'NtsLabel19
    '
    Me.NtsLabel19.AutoSize = True
    Me.NtsLabel19.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel19.Location = New System.Drawing.Point(368, 44)
    Me.NtsLabel19.Name = "NtsLabel19"
    Me.NtsLabel19.NTSDbField = ""
    Me.NtsLabel19.Size = New System.Drawing.Size(14, 13)
    Me.NtsLabel19.TabIndex = 130
    Me.NtsLabel19.Text = "A"
    Me.NtsLabel19.Tooltip = ""
    Me.NtsLabel19.UseMnemonic = False
    '
    'NtsLabel20
    '
    Me.NtsLabel20.AutoSize = True
    Me.NtsLabel20.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel20.Location = New System.Drawing.Point(368, 20)
    Me.NtsLabel20.Name = "NtsLabel20"
    Me.NtsLabel20.NTSDbField = ""
    Me.NtsLabel20.Size = New System.Drawing.Size(14, 13)
    Me.NtsLabel20.TabIndex = 129
    Me.NtsLabel20.Text = "A"
    Me.NtsLabel20.Tooltip = ""
    Me.NtsLabel20.UseMnemonic = False
    '
    'NtsLabel21
    '
    Me.NtsLabel21.AutoSize = True
    Me.NtsLabel21.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel21.Location = New System.Drawing.Point(181, 20)
    Me.NtsLabel21.Name = "NtsLabel21"
    Me.NtsLabel21.NTSDbField = ""
    Me.NtsLabel21.Size = New System.Drawing.Size(20, 13)
    Me.NtsLabel21.TabIndex = 128
    Me.NtsLabel21.Text = "Da"
    Me.NtsLabel21.Tooltip = ""
    Me.NtsLabel21.UseMnemonic = False
    '
    'ckNum1
    '
    Me.ckNum1.Location = New System.Drawing.Point(5, 17)
    Me.ckNum1.Name = "ckNum1"
    Me.ckNum1.NTSCheckValue = "S"
    Me.ckNum1.NTSUnCheckValue = "N"
    Me.ckNum1.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow
    Me.ckNum1.Properties.Appearance.Options.UseBackColor = True
    Me.ckNum1.Properties.AutoHeight = False
    Me.ckNum1.Properties.Caption = "*ckNum1"
    Me.ckNum1.Size = New System.Drawing.Size(170, 19)
    Me.ckNum1.TabIndex = 127
    '
    'pnTab6
    '
    Me.pnTab6.AllowDrop = True
    Me.pnTab6.Controls.Add(Me.pn6)
    Me.pnTab6.Enable = True
    Me.pnTab6.Name = "pnTab6"
    Me.pnTab6.Size = New System.Drawing.Size(669, 413)
    Me.pnTab6.Text = "*TAB6"
    '
    'pn6
    '
    Me.pn6.AllowDrop = True
    Me.pn6.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pn6.Appearance.Options.UseBackColor = True
    Me.pn6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pn6.Controls.Add(Me.lbCombo3)
    Me.pn6.Controls.Add(Me.lbCombo2)
    Me.pn6.Controls.Add(Me.liList3)
    Me.pn6.Controls.Add(Me.lbCombo1)
    Me.pn6.Controls.Add(Me.liList2)
    Me.pn6.Controls.Add(Me.liList1)
    Me.pn6.Controls.Add(Me.lbCheck10)
    Me.pn6.Controls.Add(Me.lbCheck8)
    Me.pn6.Controls.Add(Me.liCheck10)
    Me.pn6.Controls.Add(Me.lbCheck6)
    Me.pn6.Controls.Add(Me.liCheck8)
    Me.pn6.Controls.Add(Me.lbCheck4)
    Me.pn6.Controls.Add(Me.liCheck6)
    Me.pn6.Controls.Add(Me.liCheck4)
    Me.pn6.Controls.Add(Me.lbCheck2)
    Me.pn6.Controls.Add(Me.liCheck2)
    Me.pn6.Controls.Add(Me.lbCheck9)
    Me.pn6.Controls.Add(Me.lbCheck7)
    Me.pn6.Controls.Add(Me.liCheck9)
    Me.pn6.Controls.Add(Me.lbCheck5)
    Me.pn6.Controls.Add(Me.liCheck7)
    Me.pn6.Controls.Add(Me.lbCheck3)
    Me.pn6.Controls.Add(Me.liCheck5)
    Me.pn6.Controls.Add(Me.liCheck3)
    Me.pn6.Controls.Add(Me.lbCheck1)
    Me.pn6.Controls.Add(Me.liCheck1)
    Me.pn6.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pn6.Location = New System.Drawing.Point(0, 0)
    Me.pn6.Name = "pn6"
    Me.pn6.NTSActiveTrasparency = True
    Me.pn6.Size = New System.Drawing.Size(669, 413)
    Me.pn6.TabIndex = 0
    '
    'lbCombo3
    '
    Me.lbCombo3.AutoSize = True
    Me.lbCombo3.BackColor = System.Drawing.Color.Transparent
    Me.lbCombo3.Location = New System.Drawing.Point(8, 324)
    Me.lbCombo3.Name = "lbCombo3"
    Me.lbCombo3.NTSDbField = ""
    Me.lbCombo3.Size = New System.Drawing.Size(60, 13)
    Me.lbCombo3.TabIndex = 114
    Me.lbCombo3.Text = "*lbCombo3"
    Me.lbCombo3.Tooltip = ""
    Me.lbCombo3.UseMnemonic = False
    '
    'lbCombo2
    '
    Me.lbCombo2.AutoSize = True
    Me.lbCombo2.BackColor = System.Drawing.Color.Transparent
    Me.lbCombo2.Location = New System.Drawing.Point(8, 262)
    Me.lbCombo2.Name = "lbCombo2"
    Me.lbCombo2.NTSDbField = ""
    Me.lbCombo2.Size = New System.Drawing.Size(60, 13)
    Me.lbCombo2.TabIndex = 112
    Me.lbCombo2.Text = "*lbCombo2"
    Me.lbCombo2.Tooltip = ""
    Me.lbCombo2.UseMnemonic = False
    '
    'liList3
    '
    Me.liList3.ItemHeight = 14
    Me.liList3.Location = New System.Drawing.Point(210, 324)
    Me.liList3.Name = "liList3"
    Me.liList3.NTSDbField = ""
    Me.liList3.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
    Me.liList3.Size = New System.Drawing.Size(202, 82)
    Me.liList3.TabIndex = 113
    '
    'lbCombo1
    '
    Me.lbCombo1.AutoSize = True
    Me.lbCombo1.BackColor = System.Drawing.Color.Transparent
    Me.lbCombo1.Location = New System.Drawing.Point(8, 200)
    Me.lbCombo1.Name = "lbCombo1"
    Me.lbCombo1.NTSDbField = ""
    Me.lbCombo1.Size = New System.Drawing.Size(60, 13)
    Me.lbCombo1.TabIndex = 111
    Me.lbCombo1.Text = "*lbCombo1"
    Me.lbCombo1.Tooltip = ""
    Me.lbCombo1.UseMnemonic = False
    '
    'liList2
    '
    Me.liList2.ItemHeight = 14
    Me.liList2.Location = New System.Drawing.Point(210, 262)
    Me.liList2.Name = "liList2"
    Me.liList2.NTSDbField = ""
    Me.liList2.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
    Me.liList2.Size = New System.Drawing.Size(202, 56)
    Me.liList2.TabIndex = 110
    '
    'liList1
    '
    Me.liList1.ItemHeight = 14
    Me.liList1.Location = New System.Drawing.Point(210, 200)
    Me.liList1.Name = "liList1"
    Me.liList1.NTSDbField = ""
    Me.liList1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
    Me.liList1.Size = New System.Drawing.Size(202, 56)
    Me.liList1.TabIndex = 109
    '
    'lbCheck10
    '
    Me.lbCheck10.AutoSize = True
    Me.lbCheck10.BackColor = System.Drawing.Color.Transparent
    Me.lbCheck10.Location = New System.Drawing.Point(355, 148)
    Me.lbCheck10.Name = "lbCheck10"
    Me.lbCheck10.NTSDbField = ""
    Me.lbCheck10.Size = New System.Drawing.Size(62, 13)
    Me.lbCheck10.TabIndex = 108
    Me.lbCheck10.Text = "*lbCheck10"
    Me.lbCheck10.Tooltip = ""
    Me.lbCheck10.UseMnemonic = False
    '
    'lbCheck8
    '
    Me.lbCheck8.AutoSize = True
    Me.lbCheck8.BackColor = System.Drawing.Color.Transparent
    Me.lbCheck8.Location = New System.Drawing.Point(355, 112)
    Me.lbCheck8.Name = "lbCheck8"
    Me.lbCheck8.NTSDbField = ""
    Me.lbCheck8.Size = New System.Drawing.Size(56, 13)
    Me.lbCheck8.TabIndex = 106
    Me.lbCheck8.Text = "*lbCheck8"
    Me.lbCheck8.Tooltip = ""
    Me.lbCheck8.UseMnemonic = False
    '
    'liCheck10
    '
    Me.liCheck10.ItemHeight = 14
    Me.liCheck10.Location = New System.Drawing.Point(561, 148)
    Me.liCheck10.Name = "liCheck10"
    Me.liCheck10.NTSDbField = ""
    Me.liCheck10.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
    Me.liCheck10.Size = New System.Drawing.Size(96, 30)
    Me.liCheck10.TabIndex = 107
    '
    'lbCheck6
    '
    Me.lbCheck6.AutoSize = True
    Me.lbCheck6.BackColor = System.Drawing.Color.Transparent
    Me.lbCheck6.Location = New System.Drawing.Point(355, 76)
    Me.lbCheck6.Name = "lbCheck6"
    Me.lbCheck6.NTSDbField = ""
    Me.lbCheck6.Size = New System.Drawing.Size(56, 13)
    Me.lbCheck6.TabIndex = 105
    Me.lbCheck6.Text = "*lbCheck6"
    Me.lbCheck6.Tooltip = ""
    Me.lbCheck6.UseMnemonic = False
    '
    'liCheck8
    '
    Me.liCheck8.ItemHeight = 14
    Me.liCheck8.Location = New System.Drawing.Point(561, 112)
    Me.liCheck8.Name = "liCheck8"
    Me.liCheck8.NTSDbField = ""
    Me.liCheck8.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
    Me.liCheck8.Size = New System.Drawing.Size(96, 30)
    Me.liCheck8.TabIndex = 104
    '
    'lbCheck4
    '
    Me.lbCheck4.AutoSize = True
    Me.lbCheck4.BackColor = System.Drawing.Color.Transparent
    Me.lbCheck4.Location = New System.Drawing.Point(355, 40)
    Me.lbCheck4.Name = "lbCheck4"
    Me.lbCheck4.NTSDbField = ""
    Me.lbCheck4.Size = New System.Drawing.Size(56, 13)
    Me.lbCheck4.TabIndex = 103
    Me.lbCheck4.Text = "*lbCheck4"
    Me.lbCheck4.Tooltip = ""
    Me.lbCheck4.UseMnemonic = False
    '
    'liCheck6
    '
    Me.liCheck6.ItemHeight = 14
    Me.liCheck6.Location = New System.Drawing.Point(561, 76)
    Me.liCheck6.Name = "liCheck6"
    Me.liCheck6.NTSDbField = ""
    Me.liCheck6.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
    Me.liCheck6.Size = New System.Drawing.Size(96, 30)
    Me.liCheck6.TabIndex = 102
    '
    'liCheck4
    '
    Me.liCheck4.ItemHeight = 14
    Me.liCheck4.Location = New System.Drawing.Point(561, 40)
    Me.liCheck4.Name = "liCheck4"
    Me.liCheck4.NTSDbField = ""
    Me.liCheck4.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
    Me.liCheck4.Size = New System.Drawing.Size(96, 30)
    Me.liCheck4.TabIndex = 101
    '
    'lbCheck2
    '
    Me.lbCheck2.AutoSize = True
    Me.lbCheck2.BackColor = System.Drawing.Color.Transparent
    Me.lbCheck2.Location = New System.Drawing.Point(355, 4)
    Me.lbCheck2.Name = "lbCheck2"
    Me.lbCheck2.NTSDbField = ""
    Me.lbCheck2.Size = New System.Drawing.Size(56, 13)
    Me.lbCheck2.TabIndex = 100
    Me.lbCheck2.Text = "*lbCheck2"
    Me.lbCheck2.Tooltip = ""
    Me.lbCheck2.UseMnemonic = False
    '
    'liCheck2
    '
    Me.liCheck2.ItemHeight = 14
    Me.liCheck2.Location = New System.Drawing.Point(561, 4)
    Me.liCheck2.Name = "liCheck2"
    Me.liCheck2.NTSDbField = ""
    Me.liCheck2.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
    Me.liCheck2.Size = New System.Drawing.Size(96, 30)
    Me.liCheck2.TabIndex = 99
    '
    'lbCheck9
    '
    Me.lbCheck9.AutoSize = True
    Me.lbCheck9.BackColor = System.Drawing.Color.Transparent
    Me.lbCheck9.Location = New System.Drawing.Point(8, 148)
    Me.lbCheck9.Name = "lbCheck9"
    Me.lbCheck9.NTSDbField = ""
    Me.lbCheck9.Size = New System.Drawing.Size(56, 13)
    Me.lbCheck9.TabIndex = 98
    Me.lbCheck9.Text = "*lbCheck9"
    Me.lbCheck9.Tooltip = ""
    Me.lbCheck9.UseMnemonic = False
    '
    'lbCheck7
    '
    Me.lbCheck7.AutoSize = True
    Me.lbCheck7.BackColor = System.Drawing.Color.Transparent
    Me.lbCheck7.Location = New System.Drawing.Point(8, 112)
    Me.lbCheck7.Name = "lbCheck7"
    Me.lbCheck7.NTSDbField = ""
    Me.lbCheck7.Size = New System.Drawing.Size(56, 13)
    Me.lbCheck7.TabIndex = 96
    Me.lbCheck7.Text = "*lbCheck7"
    Me.lbCheck7.Tooltip = ""
    Me.lbCheck7.UseMnemonic = False
    '
    'liCheck9
    '
    Me.liCheck9.ItemHeight = 14
    Me.liCheck9.Location = New System.Drawing.Point(210, 148)
    Me.liCheck9.Name = "liCheck9"
    Me.liCheck9.NTSDbField = ""
    Me.liCheck9.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
    Me.liCheck9.Size = New System.Drawing.Size(96, 30)
    Me.liCheck9.TabIndex = 97
    '
    'lbCheck5
    '
    Me.lbCheck5.AutoSize = True
    Me.lbCheck5.BackColor = System.Drawing.Color.Transparent
    Me.lbCheck5.Location = New System.Drawing.Point(8, 76)
    Me.lbCheck5.Name = "lbCheck5"
    Me.lbCheck5.NTSDbField = ""
    Me.lbCheck5.Size = New System.Drawing.Size(56, 13)
    Me.lbCheck5.TabIndex = 95
    Me.lbCheck5.Text = "*lbCheck5"
    Me.lbCheck5.Tooltip = ""
    Me.lbCheck5.UseMnemonic = False
    '
    'liCheck7
    '
    Me.liCheck7.ItemHeight = 14
    Me.liCheck7.Location = New System.Drawing.Point(210, 112)
    Me.liCheck7.Name = "liCheck7"
    Me.liCheck7.NTSDbField = ""
    Me.liCheck7.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
    Me.liCheck7.Size = New System.Drawing.Size(96, 30)
    Me.liCheck7.TabIndex = 94
    '
    'lbCheck3
    '
    Me.lbCheck3.AutoSize = True
    Me.lbCheck3.BackColor = System.Drawing.Color.Transparent
    Me.lbCheck3.Location = New System.Drawing.Point(8, 40)
    Me.lbCheck3.Name = "lbCheck3"
    Me.lbCheck3.NTSDbField = ""
    Me.lbCheck3.Size = New System.Drawing.Size(56, 13)
    Me.lbCheck3.TabIndex = 93
    Me.lbCheck3.Text = "*lbCheck3"
    Me.lbCheck3.Tooltip = ""
    Me.lbCheck3.UseMnemonic = False
    '
    'liCheck5
    '
    Me.liCheck5.ItemHeight = 14
    Me.liCheck5.Location = New System.Drawing.Point(210, 76)
    Me.liCheck5.Name = "liCheck5"
    Me.liCheck5.NTSDbField = ""
    Me.liCheck5.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
    Me.liCheck5.Size = New System.Drawing.Size(96, 30)
    Me.liCheck5.TabIndex = 92
    '
    'liCheck3
    '
    Me.liCheck3.ItemHeight = 14
    Me.liCheck3.Location = New System.Drawing.Point(210, 40)
    Me.liCheck3.Name = "liCheck3"
    Me.liCheck3.NTSDbField = ""
    Me.liCheck3.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
    Me.liCheck3.Size = New System.Drawing.Size(96, 30)
    Me.liCheck3.TabIndex = 91
    '
    'lbCheck1
    '
    Me.lbCheck1.AutoSize = True
    Me.lbCheck1.BackColor = System.Drawing.Color.Transparent
    Me.lbCheck1.Location = New System.Drawing.Point(8, 4)
    Me.lbCheck1.Name = "lbCheck1"
    Me.lbCheck1.NTSDbField = ""
    Me.lbCheck1.Size = New System.Drawing.Size(56, 13)
    Me.lbCheck1.TabIndex = 90
    Me.lbCheck1.Text = "*lbCheck1"
    Me.lbCheck1.Tooltip = ""
    Me.lbCheck1.UseMnemonic = False
    '
    'liCheck1
    '
    Me.liCheck1.ItemHeight = 14
    Me.liCheck1.Location = New System.Drawing.Point(210, 4)
    Me.liCheck1.Name = "liCheck1"
    Me.liCheck1.NTSDbField = ""
    Me.liCheck1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
    Me.liCheck1.Size = New System.Drawing.Size(96, 30)
    Me.liCheck1.TabIndex = 89
    '
    'pnCommand
    '
    Me.pnCommand.AllowDrop = True
    Me.pnCommand.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCommand.Appearance.Options.UseBackColor = True
    Me.pnCommand.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCommand.Controls.Add(Me.NtsLabel1)
    Me.pnCommand.Controls.Add(Me.cmdCancel)
    Me.pnCommand.Controls.Add(Me.cmdOk)
    Me.pnCommand.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCommand.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnCommand.Location = New System.Drawing.Point(0, 443)
    Me.pnCommand.Name = "pnCommand"
    Me.pnCommand.NTSActiveTrasparency = True
    Me.pnCommand.Size = New System.Drawing.Size(678, 29)
    Me.pnCommand.TabIndex = 2
    '
    'NtsLabel1
    '
    Me.NtsLabel1.AutoSize = True
    Me.NtsLabel1.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel1.Location = New System.Drawing.Point(8, 9)
    Me.NtsLabel1.Name = "NtsLabel1"
    Me.NtsLabel1.NTSDbField = ""
    Me.NtsLabel1.Size = New System.Drawing.Size(335, 13)
    Me.NtsLabel1.TabIndex = 2
    Me.NtsLabel1.Text = "NB: per non eseguire filtri sui campi di testo impostare nei campi un *"
    Me.NtsLabel1.Tooltip = ""
    Me.NtsLabel1.UseMnemonic = False
    '
    'cmdCancel
    '
    Me.cmdCancel.ImagePath = ""
    Me.cmdCancel.ImageText = ""
    Me.cmdCancel.Location = New System.Drawing.Point(552, 3)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.NTSContextMenu = Nothing
    Me.cmdCancel.Size = New System.Drawing.Size(107, 24)
    Me.cmdCancel.TabIndex = 1
    Me.cmdCancel.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImagePath = ""
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(424, 3)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.NTSContextMenu = Nothing
    Me.cmdOk.Size = New System.Drawing.Size(107, 24)
    Me.cmdOk.TabIndex = 0
    Me.cmdOk.Text = "&Conferma"
    '
    'pnFilter
    '
    Me.pnFilter.AllowDrop = True
    Me.pnFilter.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnFilter.Appearance.Options.UseBackColor = True
    Me.pnFilter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnFilter.Controls.Add(Me.tsHlex)
    Me.pnFilter.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnFilter.Location = New System.Drawing.Point(0, 0)
    Me.pnFilter.Name = "pnFilter"
    Me.pnFilter.NTSActiveTrasparency = True
    Me.pnFilter.Size = New System.Drawing.Size(678, 443)
    Me.pnFilter.TabIndex = 3
    '
    'FRM__HLEX
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(678, 472)
    Me.Controls.Add(Me.pnFilter)
    Me.Controls.Add(Me.pnCommand)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.Name = "FRM__HLEX"
    Me.NTSLastControlFocussed = Me.cmdOk
    Me.Text = "ZOOM ESTENSIONI ANAGRAFICHE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tsHlex, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsHlex.ResumeLayout(False)
    Me.pnTab1.ResumeLayout(False)
    CType(Me.pn1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pn1.ResumeLayout(False)
    Me.pn1.PerformLayout()
    CType(Me.edDesex3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDesex2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDesex1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTipo3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTipo2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTipo1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTab2.ResumeLayout(False)
    CType(Me.pn2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pn2.ResumeLayout(False)
    Me.pn2.PerformLayout()
    CType(Me.ckData5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckData4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckData3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckData2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edData5a.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edData4a.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edData3a.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edData2a.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edData5da.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edData4da.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edData3da.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edData2da.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edData1a.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edData1da.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckData1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDescr10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDescr9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDescr8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDescr7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDescr6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDescr5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDescr4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDescr3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDescr2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDescr1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTab3.ResumeLayout(False)
    CType(Me.pn3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pn3.ResumeLayout(False)
    Me.pn3.PerformLayout()
    CType(Me.edMemo1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTab4.ResumeLayout(False)
    CType(Me.pn4, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pn4.ResumeLayout(False)
    Me.pn4.PerformLayout()
    CType(Me.edMemo2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTab5.ResumeLayout(False)
    CType(Me.pn5, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pn5.ResumeLayout(False)
    Me.pn5.PerformLayout()
    CType(Me.edNum10a.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNum9a.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNum8a.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNum7a.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNum6a.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNum5a.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNum4a.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNum3a.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNum2a.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNum1a.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNum10da.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNum9da.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNum8da.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNum7da.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNum6da.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNum5da.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNum4da.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNum3da.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNum2da.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNum1da.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNum10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNum9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNum8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNum7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNum6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNum5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNum4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNum3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNum2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNum1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTab6.ResumeLayout(False)
    CType(Me.pn6, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pn6.ResumeLayout(False)
    Me.pn6.PerformLayout()
    CType(Me.liList3, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.liList2, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.liList1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.liCheck10, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.liCheck8, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.liCheck6, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.liCheck4, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.liCheck2, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.liCheck9, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.liCheck7, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.liCheck5, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.liCheck3, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.liCheck1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnCommand, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCommand.ResumeLayout(False)
    Me.pnCommand.PerformLayout()
    CType(Me.pnFilter, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFilter.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    oMenu = Menu
    oApp = oMenu.App
    oCallParams = Param

    oCallParams.bPar1 = False
    strTipoConto = oCallParams.strPar1
    oCallParams.strPar1 = ""

    If Ditta <> "" Then
      DittaCorrente = Ditta
    Else
      DittaCorrente = oApp.Ditta
    End If
    Me.GctlTipoDoc = ""

    InitializeComponent()
    Me.MinimumSize = Me.Size

    Select Case strTipoConto
      Case "C" : Me.Text = Me.Text & " - Clienti"
      Case "F" : Me.Text = Me.Text & " - Fornitori"
      Case "D" : Me.Text = Me.Text & " - Destinazioni diverse clienti"
      Case "E" : Me.Text = Me.Text & " - Destinazioni diverse fornitori"
      Case "L" : Me.Text = Me.Text & " - Leads"
    End Select

    Return True
  End Function

  Public Overridable Sub InitEntity(ByRef cleHlan As CLE__HLAN)
    oCleHlan = cleHlan
    AddHandler oCleHlan.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      edDescr10.NTSSetParam(oMenu, lbDescr10.Text, 30, True)
      edDescr9.NTSSetParam(oMenu, lbDescr9.Text, 30, True)
      edDescr8.NTSSetParam(oMenu, lbDescr8.Text, 30, True)
      edDescr7.NTSSetParam(oMenu, lbDescr7.Text, 30, True)
      edDescr6.NTSSetParam(oMenu, lbDescr6.Text, 30, True)
      edDescr5.NTSSetParam(oMenu, lbDescr5.Text, 30, True)
      edDescr4.NTSSetParam(oMenu, lbDescr4.Text, 30, True)
      edDescr3.NTSSetParam(oMenu, lbDescr3.Text, 30, True)
      edDescr2.NTSSetParam(oMenu, lbDescr2.Text, 30, True)
      edDescr1.NTSSetParam(oMenu, lbDescr1.Text, 30, True)
      edMemo1.NTSSetParam(oMenu, lbMemo1.Text, 0, True)
      edMemo2.NTSSetParam(oMenu, lbMemo2.Text, 0, True)
      edNum10a.NTSSetParam(oMenu, oApp.Tr(Me, 128230023448056573, "A ") & ckNum10.Text, "#,##0.00", 15, -999999999999D, 999999999999D)
      edNum9a.NTSSetParam(oMenu, oApp.Tr(Me, 128230023448212746, "A ") & ckNum9.Text, "#,##0.00", 15, -999999999999D, 999999999999D)
      edNum8a.NTSSetParam(oMenu, oApp.Tr(Me, 128230023448368919, "A ") & ckNum8.Text, "#,##0.00", 15, -999999999999D, 999999999999D)
      edNum7a.NTSSetParam(oMenu, oApp.Tr(Me, 128230023448525092, "A ") & ckNum7.Text, "#,##0.00", 15, -999999999999D, 999999999999D)
      edNum6a.NTSSetParam(oMenu, oApp.Tr(Me, 128230023448681265, "A ") & ckNum6.Text, "#,##0.00", 15, -999999999999D, 999999999999D)
      edNum5a.NTSSetParam(oMenu, oApp.Tr(Me, 128230023448837438, "A ") & ckNum5.Text, "#,##0.00", 15, -999999999999D, 999999999999D)
      edNum4a.NTSSetParam(oMenu, oApp.Tr(Me, 128230023448993611, "A ") & ckNum4.Text, "#,##0.00", 15, -999999999999D, 999999999999D)
      edNum3a.NTSSetParam(oMenu, oApp.Tr(Me, 128230023449149784, "A ") & ckNum3.Text, "#,##0.00", 15, -999999999999D, 999999999999D)
      edNum2a.NTSSetParam(oMenu, oApp.Tr(Me, 128230023449305957, "A ") & ckNum2.Text, "#,##0.00", 15, -999999999999D, 999999999999D)
      edNum1a.NTSSetParam(oMenu, oApp.Tr(Me, 128230023449462130, "A ") & ckNum1.Text, "#,##0.00", 15, -999999999999D, 999999999999D)
      edNum10da.NTSSetParam(oMenu, oApp.Tr(Me, 128230023449618303, "DA ") & ckNum10.Text, "#,##0.00", 15, -999999999999D, 999999999999D)
      edNum9da.NTSSetParam(oMenu, oApp.Tr(Me, 128230023449774476, "DA ") & ckNum9.Text, "#,##0.00", 15, -999999999999D, 999999999999D)
      edNum8da.NTSSetParam(oMenu, oApp.Tr(Me, 128230023449930649, "DA ") & ckNum8.Text, "#,##0.00", 15, -999999999999D, 999999999999D)
      edNum7da.NTSSetParam(oMenu, oApp.Tr(Me, 128230023450086822, "DA ") & ckNum7.Text, "#,##0.00", 15, -999999999999D, 999999999999D)
      edNum6da.NTSSetParam(oMenu, oApp.Tr(Me, 128230023450242995, "DA ") & ckNum6.Text, "#,##0.00", 15, -999999999999D, 999999999999D)
      edNum5da.NTSSetParam(oMenu, oApp.Tr(Me, 128230023450399168, "DA ") & ckNum5.Text, "#,##0.00", 15, -999999999999D, 999999999999D)
      edNum4da.NTSSetParam(oMenu, oApp.Tr(Me, 128230023450555341, "DA ") & ckNum4.Text, "#,##0.00", 15, -999999999999D, 999999999999D)
      edNum3da.NTSSetParam(oMenu, oApp.Tr(Me, 128230023450711514, "DA ") & ckNum3.Text, "#,##0.00", 15, -999999999999D, 999999999999D)
      edNum2da.NTSSetParam(oMenu, oApp.Tr(Me, 128230023450867687, "DA ") & ckNum2.Text, "#,##0.00", 15, -999999999999D, 999999999999D)
      edNum1da.NTSSetParam(oMenu, oApp.Tr(Me, 128230023451023860, "DA ") & ckNum1.Text, "#,##0.00", 15, -999999999999D, 999999999999D)
      ckNum10.NTSSetParam(oMenu, ckNum10.Text, oApp.Tr(Me, 128230023451180033, "S"), "N")
      ckNum9.NTSSetParam(oMenu, ckNum9.Text, oApp.Tr(Me, 128230023451336206, "S"), "N")
      ckNum8.NTSSetParam(oMenu, ckNum8.Text, oApp.Tr(Me, 128230023451492379, "S"), "N")
      ckNum7.NTSSetParam(oMenu, ckNum7.Text, oApp.Tr(Me, 128230023451648552, "S"), "N")
      ckNum6.NTSSetParam(oMenu, ckNum6.Text, oApp.Tr(Me, 128230023451804725, "S"), "N")
      ckNum5.NTSSetParam(oMenu, ckNum5.Text, oApp.Tr(Me, 128230023451960898, "S"), "N")
      ckNum4.NTSSetParam(oMenu, ckNum4.Text, oApp.Tr(Me, 128230023452117071, "S"), "N")
      ckNum3.NTSSetParam(oMenu, ckNum3.Text, oApp.Tr(Me, 128230023452273244, "S"), "N")
      ckNum2.NTSSetParam(oMenu, ckNum2.Text, oApp.Tr(Me, 128230023452429417, "S"), "N")
      ckNum1.NTSSetParam(oMenu, ckNum1.Text, oApp.Tr(Me, 128230023452585590, "S"), "N")
      liList3.NTSSetParam(lbCombo3.Text)
      liList2.NTSSetParam(lbCombo2.Text)
      liList1.NTSSetParam(lbCombo1.Text)
      liCheck10.NTSSetParam(liCheck10.Text)
      liCheck9.NTSSetParam(liCheck9.Text)
      liCheck8.NTSSetParam(liCheck8.Text)
      liCheck7.NTSSetParam(liCheck7.Text)
      liCheck6.NTSSetParam(liCheck6.Text)
      liCheck5.NTSSetParam(liCheck5.Text)
      liCheck4.NTSSetParam(liCheck4.Text)
      liCheck3.NTSSetParam(liCheck3.Text)
      liCheck2.NTSSetParam(liCheck2.Text)
      liCheck1.NTSSetParam(liCheck1.Text)

      edDesex3.NTSSetParam(oMenu, lbDesEx3.Text, 255, True)
      edDesex2.NTSSetParam(oMenu, lbDesEx2.Text, 255, True)
      edDesex1.NTSSetParam(oMenu, lbDesEx1.Text, 255, True)
      edTipo3.NTSSetParam(oMenu, lbTipo3.Text, 1)
      edTipo2.NTSSetParam(oMenu, lbTipo2.Text, 1)
      edTipo1.NTSSetParam(oMenu, lbTipo1.Text, 1)
      edData5a.NTSSetParam(oMenu, ckData5.Text, False)
      edData4a.NTSSetParam(oMenu, ckData4.Text, False)
      edData3a.NTSSetParam(oMenu, ckData3.Text, False)
      edData2a.NTSSetParam(oMenu, ckData2.Text, False)
      edData5da.NTSSetParam(oMenu, ckData5.Text, False)
      edData4da.NTSSetParam(oMenu, ckData4.Text, False)
      edData3da.NTSSetParam(oMenu, ckData3.Text, False)
      edData2da.NTSSetParam(oMenu, ckData2.Text, False)
      edData1a.NTSSetParam(oMenu, ckData1.Text, False)
      edData1da.NTSSetParam(oMenu, ckData1.Text, False)
      ckData1.NTSSetParam(oMenu, ckData1.Text, oApp.Tr(Me, 128230023457270780, "S"), "N")
      ckData2.NTSSetParam(oMenu, ckData2.Text, oApp.Tr(Me, 128230023457426953, "S"), "N")
      ckData3.NTSSetParam(oMenu, ckData3.Text, oApp.Tr(Me, 128230023457583126, "S"), "N")
      ckData4.NTSSetParam(oMenu, ckData4.Text, oApp.Tr(Me, 128230023457739299, "S"), "N")
      ckData5.NTSSetParam(oMenu, ckData5.Text, oApp.Tr(Me, 128230023457895472, "S"), "N")

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

  Public Overridable Sub FRMHLEX_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim ds As New DataSet
    Try
      '-------------------------------------------------
      'imposto le label come da TABAEXT e nascondo i controlli non gestiti
      If Not oCleHlan.GetTabAext(DittaCorrente, strTipoConto, ds) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222143750000, "Per la tipologia '|" & IIf(strTipoConto = "C", "Clienti", "Fornitori").ToString & "|' non sono previste estensioni anagrafiche"))
        Me.Close()
        Exit Sub
      End If
      ImpostaControlli(ds.Tables("TABAEXT").Rows(0))

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()


      '-------------------------------------------------
      'Carico i combobox
      CaricaComboBox(ds.Tables("TABAEXT").Rows(0))

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
      GctlApplicaDefaultValue()

      '-------------------------------------------------
      edData1da.Text = "01/01/1900"
      edData2da.Text = "01/01/1900"
      edData3da.Text = "01/01/1900"
      edData4da.Text = "01/01/1900"
      edData5da.Text = "01/01/1900"
      edData1a.Text = "31/12/2099"
      edData2a.Text = "31/12/2099"
      edData3a.Text = "31/12/2099"
      edData4a.Text = "31/12/2099"
      edData5a.Text = "31/12/2099"

      edNum1da.Text = "-999999999999" : edNum1a.Text = "999999999999"
      edNum2da.Text = "-999999999999" : edNum2a.Text = "999999999999"
      edNum3da.Text = "-999999999999" : edNum3a.Text = "999999999999"
      edNum4da.Text = "-999999999999" : edNum4a.Text = "999999999999"
      edNum5da.Text = "-999999999999" : edNum5a.Text = "999999999999"
      edNum6da.Text = "-999999999999" : edNum6a.Text = "999999999999"
      edNum7da.Text = "-999999999999" : edNum7a.Text = "999999999999"
      edNum8da.Text = "-999999999999" : edNum8a.Text = "999999999999"
      edNum9da.Text = "-999999999999" : edNum9a.Text = "999999999999"
      edNum10da.Text = "-999999999999" : edNum10a.Text = "999999999999"
      '--------------------------------------------------------------------------------------------------------------
      InizializzaCampiDaEsterno()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
    dttAnex = Nothing
    Me.Close()
  End Sub

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Dim strQuery As String = ""
    Dim strRpt As String = ""
    Dim i As Integer
    Try
      '--------------------------------------------------------------------------------------------------------------
      CreaDataTable()
      '------------------------
      'primo tab
      If edTipo1.Text <> "*" Then
        strQuery += "anaext.ax_tipo1 LIKE " & CStrSQL(edTipo1.Text) & "§"
        strRpt += "{anaext.ax_tipo1} LIKE " & ConvStrRpt(edTipo1.Text) & "§"
      End If
      If edTipo2.Text <> "*" Then
        strQuery += "anaext.ax_tipo2 LIKE " & CStrSQL(edTipo2.Text) & "§"
        strRpt += "{anaext.ax_tipo2} LIKE " & ConvStrRpt(edTipo2.Text) & "§"
      End If
      If edTipo3.Text <> "*" Then
        strQuery += "anaext.ax_tipo3 LIKE " & CStrSQL(edTipo3.Text) & "§"
        strRpt += "{anaext.ax_tipo3} LIKE " & ConvStrRpt(edTipo3.Text) & "§"
      End If
      If edDesex1.Text <> "*" Then
        strQuery += "anaext.ax_desext1 LIKE " & CampoTesto(edDesex1.Text) & "§"
        strRpt += "{anaext.ax_desext1} LIKE " & CampoTestoRpt(edDesex1.Text) & "§"
      End If
      If edDesex2.Text <> "*" Then
        strQuery += "anaext.ax_desext2 LIKE " & CampoTesto(edDesex2.Text) & "§"
        strRpt += "{anaext.ax_desext2} LIKE " & CampoTestoRpt(edDesex2.Text) & "§"
      End If
      If edDesex3.Text <> "*" Then
        strQuery += "anaext.ax_desext3 LIKE " & CampoTesto(edDesex3.Text) & "§"
        strRpt += "{anaext.ax_desext3} LIKE " & CampoTestoRpt(edDesex3.Text) & "§"
      End If
      '------------------------
      'secondo tab
      If edDescr1.Text <> "*" Then
        strQuery += "anaext.ax_descr1 LIKE " & CampoTesto(edDescr1.Text) & "§"
        strRpt += "{anaext.ax_descr1} LIKE " & CampoTestoRpt(edDescr1.Text) & "§"
      End If
      If edDescr2.Text <> "*" Then
        strQuery += "anaext.ax_descr2 LIKE " & CampoTesto(edDescr2.Text) & "§"
        strRpt += "{anaext.ax_descr2} LIKE " & CampoTestoRpt(edDescr2.Text) & "§"
      End If
      If edDescr3.Text <> "*" Then
        strQuery += "anaext.ax_descr3 LIKE " & CampoTesto(edDescr3.Text) & "§"
        strRpt += "{anaext.ax_descr3} LIKE " & CampoTestoRpt(edDescr3.Text) & "§"
      End If
      If edDescr4.Text <> "*" Then
        strQuery += "anaext.ax_descr4 LIKE " & CampoTesto(edDescr4.Text) & "§"
        strRpt += "{anaext.ax_descr4} LIKE " & CampoTestoRpt(edDescr4.Text) & "§"
      End If
      If edDescr5.Text <> "*" Then
        strQuery += "anaext.ax_descr5 LIKE " & CampoTesto(edDescr5.Text) & "§"
        strRpt += "{anaext.ax_descr5} LIKE " & CampoTestoRpt(edDescr5.Text) & "§"
      End If
      If edDescr6.Text <> "*" Then
        strQuery += "anaext.ax_descr6 LIKE " & CampoTesto(edDescr6.Text) & "§"
        strRpt += "{anaext.ax_descr6} LIKE " & CampoTestoRpt(edDescr6.Text) & "§"
      End If
      If edDescr7.Text <> "*" Then
        strQuery += "anaext.ax_descr7 LIKE " & CampoTesto(edDescr7.Text) & "§"
        strRpt += "{anaext.ax_descr7} LIKE " & CampoTestoRpt(edDescr7.Text) & "§"
      End If
      If edDescr8.Text <> "*" Then
        strQuery += "anaext.ax_descr8 LIKE " & CampoTesto(edDescr8.Text) & "§"
        strRpt += "{anaext.ax_descr8} LIKE " & CampoTestoRpt(edDescr8.Text) & "§"
      End If
      If edDescr9.Text <> "*" Then
        strQuery += "anaext.ax_descr9 LIKE " & CampoTesto(edDescr9.Text) & "§"
        strRpt += "{anaext.ax_descr9} LIKE " & CampoTestoRpt(edDescr9.Text) & "§"
      End If
      If edDescr10.Text <> "*" Then
        strQuery += "anaext.ax_descr10 LIKE " & CampoTesto(edDescr10.Text) & "§"
        strRpt += "{anaext.ax_descr10} LIKE " & CampoTestoRpt(edDescr10.Text) & "§"
      End If
      If ckData1.Checked Then
        strQuery += "anaext.ax_data1 >=" & CDataSQL(edData1da.Text) & "§" & "anaext.ax_data1 <=" & CDataSQL(edData1a.Text) & "§"
        strRpt += "{anaext.ax_data1} >=" & ConvDataRpt(edData1da.Text) & "§" & "{anaext.ax_data1} <=" & ConvDataRpt(edData1a.Text) & "§"
      End If
      If ckData2.Checked Then
        strQuery += "anaext.ax_data2 >=" & CDataSQL(edData2da.Text) & "§" & "anaext.ax_data2 <=" & CDataSQL(edData2a.Text) & "§"
        strRpt += "{anaext.ax_data2} >=" & ConvDataRpt(edData2da.Text) & "§" & "{anaext.ax_data2} <=" & ConvDataRpt(edData2a.Text) & "§"
      End If
      If ckData3.Checked Then
        strQuery += "anaext.ax_data3 >=" & CDataSQL(edData3da.Text) & "§" & "anaext.ax_data3 <=" & CDataSQL(edData3a.Text) & "§"
        strRpt += "{anaext.ax_data3} >=" & ConvDataRpt(edData3da.Text) & "§" & "{anaext.ax_data3} <=" & ConvDataRpt(edData3a.Text) & "§"
      End If
      If ckData4.Checked Then
        strQuery += "anaext.ax_data4 >=" & CDataSQL(edData4da.Text) & "§" & "anaext.ax_data4 <=" & CDataSQL(edData4a.Text) & "§"
        strRpt += "{anaext.ax_data4} >=" & ConvDataRpt(edData4da.Text) & "§" & "{anaext.ax_data4} <=" & ConvDataRpt(edData4a.Text) & "§"
      End If
      If ckData5.Checked Then
        strQuery += "anaext.ax_data5 >=" & CDataSQL(edData5da.Text) & "§" & "anaext.ax_data5 <=" & CDataSQL(edData5a.Text) & "§"
        strRpt += "{anaext.ax_data5} >=" & ConvDataRpt(edData5da.Text) & "§" & "{anaext.ax_data5} <=" & ConvDataRpt(edData5a.Text) & "§"
      End If
      '------------------------
      'terzo e quarto tab
      If edMemo1.Text <> "*" Then
        strQuery += "anaext.ax_memo1 LIKE " & CampoTesto(edMemo1.Text) & "§"
        strRpt += "{anaext.ax_memo1} LIKE " & CampoTestoRpt(edMemo1.Text) & "§"
      End If
      If edMemo2.Text <> "*" Then
        strQuery += "anaext.ax_memo2 LIKE " & CampoTesto(edMemo2.Text) & "§"
        strRpt += "{anaext.ax_memo2} LIKE " & CampoTestoRpt(edMemo2.Text) & "§"
      End If
      '------------------------
      'quinto tab
      If ckNum1.Checked Then
        strQuery += "anaext.ax_num1 >=" & CDblSQL(edNum1da.Text) & "§" & "anaext.ax_num1 <=" & CDblSQL(edNum1a.Text) & "§"
        strRpt += "{anaext.ax_num1} >=" & CDblSQL(edNum1da.Text) & "§" & "{anaext.ax_num1} <=" & CDblSQL(edNum1a.Text) & "§"
      End If
      If ckNum2.Checked Then
        strQuery += "anaext.ax_num2 >=" & CDblSQL(edNum2da.Text) & "§" & "anaext.ax_num2 <=" & CDblSQL(edNum2a.Text) & "§"
        strRpt += "{anaext.ax_num2} >=" & CDblSQL(edNum2da.Text) & "§" & "{anaext.ax_num2} <=" & CDblSQL(edNum2a.Text) & "§"
      End If
      If ckNum3.Checked Then
        strQuery += "anaext.ax_num3 >=" & CDblSQL(edNum3da.Text) & "§" & "anaext.ax_num3 <=" & CDblSQL(edNum3a.Text) & "§"
        strRpt += "{anaext.ax_num3} >=" & CDblSQL(edNum3da.Text) & "§" & "{anaext.ax_num3} <=" & CDblSQL(edNum3a.Text) & "§"
      End If
      If ckNum4.Checked Then
        strQuery += "anaext.ax_num4 >=" & CDblSQL(edNum4da.Text) & "§" & "anaext.ax_num4 <=" & CDblSQL(edNum4a.Text) & "§"
        strRpt += "{anaext.ax_num4} >=" & CDblSQL(edNum4da.Text) & "§" & "{anaext.ax_num4} <=" & CDblSQL(edNum4a.Text) & "§"
      End If
      If ckNum5.Checked Then
        strQuery += "anaext.ax_num5 >=" & CDblSQL(edNum5da.Text) & "§" & "anaext.ax_num5 <=" & CDblSQL(edNum5a.Text) & "§"
        strRpt += "{anaext.ax_num5} >=" & CDblSQL(edNum5da.Text) & "§" & "{anaext.ax_num5} <=" & CDblSQL(edNum5a.Text) & "§"
      End If
      If ckNum6.Checked Then
        strQuery += "anaext.ax_num6 >=" & CDblSQL(edNum6da.Text) & "§" & "anaext.ax_num6 <=" & CDblSQL(edNum6a.Text) & "§"
        strRpt += "{anaext.ax_num6} >=" & CDblSQL(edNum6da.Text) & "§" & "{anaext.ax_num6} <=" & CDblSQL(edNum6a.Text) & "§"
      End If
      If ckNum7.Checked Then
        strQuery += "anaext.ax_num7 >=" & CDblSQL(edNum7da.Text) & "§" & "anaext.ax_num7 <=" & CDblSQL(edNum7a.Text) & "§"
        strRpt += "{anaext.ax_num7} >=" & CDblSQL(edNum7da.Text) & "§" & "{anaext.ax_num7} <=" & CDblSQL(edNum7a.Text) & "§"
      End If
      If ckNum8.Checked Then
        strQuery += "anaext.ax_num8 >=" & CDblSQL(edNum8da.Text) & "§" & "anaext.ax_num8 <=" & CDblSQL(edNum8a.Text) & "§"
        strRpt += "{anaext.ax_num8} >=" & CDblSQL(edNum8da.Text) & "§" & "{anaext.ax_num8} <=" & CDblSQL(edNum8a.Text) & "§"
      End If
      If ckNum9.Checked Then
        strQuery += "anaext.ax_num9 >=" & CDblSQL(edNum9da.Text) & "§" & "anaext.ax_num9 <=" & CDblSQL(edNum9a.Text) & "§"
        strRpt += "{anaext.ax_num9} >=" & CDblSQL(edNum9da.Text) & "§" & "{anaext.ax_num9} <=" & CDblSQL(edNum9a.Text) & "§"
      End If
      If ckNum10.Checked Then
        strQuery += "anaext.ax_num10 >=" & CDblSQL(edNum10da.Text) & "§" & "anaext.ax_num10 <=" & CDblSQL(edNum10a.Text) & "§"
        strRpt += "{anaext.ax_num10} >=" & CDblSQL(edNum10da.Text) & "§" & "{anaext.ax_num10} <=" & CDblSQL(edNum10a.Text) & "§"
      End If
      '------------------------
      'sesto tab
      If liCheck1.SelectedIndices.Count > 0 And liCheck1.SelectedIndices.Count < liCheck1.ItemCount Then
        strQuery += "anaext.ax_check1|"
        strRpt += "{anaext.ax_check1}|"
        For i = 0 To liCheck1.SelectedIndices.Count - 1
          strQuery += "=" & CStrSQL(liCheck1.GetItemValue(liCheck1.SelectedIndices(i)).ToString) & "|"
          strRpt += "=" & ConvStrRpt(liCheck1.GetItemValue(liCheck1.SelectedIndices(i)).ToString) & "|"
        Next
        strQuery = strQuery.Substring(0, strQuery.Length - 1) & "§"
        strRpt = strRpt.Substring(0, strRpt.Length - 1) & "§"
      End If

      If liCheck2.SelectedIndices.Count > 0 And liCheck2.SelectedIndices.Count < liCheck2.ItemCount Then
        strQuery += "anaext.ax_check2|"
        strRpt += "{anaext.ax_check2}|"
        For i = 0 To liCheck2.SelectedIndices.Count - 1
          strQuery += "=" & CStrSQL(liCheck2.GetItemValue(liCheck2.SelectedIndices(i)).ToString) & "|"
          strRpt += "=" & ConvStrRpt(liCheck2.GetItemValue(liCheck2.SelectedIndices(i)).ToString) & "|"
        Next
        strQuery = strQuery.Substring(0, strQuery.Length - 1) & "§"
        strRpt = strRpt.Substring(0, strRpt.Length - 1) & "§"
      End If

      If liCheck3.SelectedIndices.Count > 0 And liCheck3.SelectedIndices.Count < liCheck3.ItemCount Then
        strQuery += "anaext.ax_check3|"
        strRpt += "{anaext.ax_check3}|"
        For i = 0 To liCheck3.SelectedIndices.Count - 1
          strQuery += "=" & CStrSQL(liCheck3.GetItemValue(liCheck3.SelectedIndices(i)).ToString) & "|"
          strRpt += "=" & ConvStrRpt(liCheck3.GetItemValue(liCheck3.SelectedIndices(i)).ToString) & "|"
        Next
        strQuery = strQuery.Substring(0, strQuery.Length - 1) & "§"
        strRpt = strRpt.Substring(0, strRpt.Length - 1) & "§"
      End If

      If liCheck4.SelectedIndices.Count > 0 And liCheck4.SelectedIndices.Count < liCheck4.ItemCount Then
        strQuery += "anaext.ax_check4|"
        strRpt += "{anaext.ax_check4}|"
        For i = 0 To liCheck4.SelectedIndices.Count - 1
          strQuery += "=" & CStrSQL(liCheck4.GetItemValue(liCheck4.SelectedIndices(i)).ToString) & "|"
          strRpt += "=" & ConvStrRpt(liCheck4.GetItemValue(liCheck4.SelectedIndices(i)).ToString) & "|"
        Next
        strQuery = strQuery.Substring(0, strQuery.Length - 1) & "§"
        strRpt = strRpt.Substring(0, strRpt.Length - 1) & "§"
      End If

      If liCheck5.SelectedIndices.Count > 0 And liCheck5.SelectedIndices.Count < liCheck5.ItemCount Then
        strQuery += "anaext.ax_check5|"
        strRpt += "{anaext.ax_check5}|"
        For i = 0 To liCheck5.SelectedIndices.Count - 1
          strQuery += "=" & CStrSQL(liCheck5.GetItemValue(liCheck5.SelectedIndices(i)).ToString) & "|"
          strRpt += "=" & ConvStrRpt(liCheck5.GetItemValue(liCheck5.SelectedIndices(i)).ToString) & "|"
        Next
        strQuery = strQuery.Substring(0, strQuery.Length - 1) & "§"
        strRpt = strRpt.Substring(0, strRpt.Length - 1) & "§"
      End If

      If liCheck6.SelectedIndices.Count > 0 And liCheck6.SelectedIndices.Count < liCheck6.ItemCount Then
        strQuery += "anaext.ax_check6|"
        strRpt += "{anaext.ax_check6}|"
        For i = 0 To liCheck6.SelectedIndices.Count - 1
          strQuery += "=" & CStrSQL(liCheck6.GetItemValue(liCheck6.SelectedIndices(i)).ToString) & "|"
          strRpt += "=" & ConvStrRpt(liCheck6.GetItemValue(liCheck6.SelectedIndices(i)).ToString) & "|"
        Next
        strQuery = strQuery.Substring(0, strQuery.Length - 1) & "§"
        strRpt = strRpt.Substring(0, strRpt.Length - 1) & "§"
      End If

      If liCheck7.SelectedIndices.Count > 0 And liCheck7.SelectedIndices.Count < liCheck7.ItemCount Then
        strQuery += "anaext.ax_check7|"
        strRpt += "{anaext.ax_check7}|"
        For i = 0 To liCheck7.SelectedIndices.Count - 1
          strQuery += "=" & CStrSQL(liCheck7.GetItemValue(liCheck7.SelectedIndices(i)).ToString) & "|"
          strRpt += "=" & ConvStrRpt(liCheck7.GetItemValue(liCheck7.SelectedIndices(i)).ToString) & "|"
        Next
        strQuery = strQuery.Substring(0, strQuery.Length - 1) & "§"
        strRpt = strRpt.Substring(0, strRpt.Length - 1) & "§"
      End If

      If liCheck8.SelectedIndices.Count > 0 And liCheck8.SelectedIndices.Count < liCheck8.ItemCount Then
        strQuery += "anaext.ax_check8|"
        strRpt += "{anaext.ax_check8}|"
        For i = 0 To liCheck8.SelectedIndices.Count - 1
          strQuery += "=" & CStrSQL(liCheck8.GetItemValue(liCheck8.SelectedIndices(i)).ToString) & "|"
          strRpt += "=" & ConvStrRpt(liCheck8.GetItemValue(liCheck8.SelectedIndices(i)).ToString) & "|"
        Next
        strQuery = strQuery.Substring(0, strQuery.Length - 1) & "§"
        strRpt = strRpt.Substring(0, strRpt.Length - 1) & "§"
      End If

      If liCheck9.SelectedIndices.Count > 0 And liCheck9.SelectedIndices.Count < liCheck9.ItemCount Then
        strQuery += "anaext.ax_check9|"
        strRpt += "{anaext.ax_check9}|"
        For i = 0 To liCheck9.SelectedIndices.Count - 1
          strQuery += "=" & CStrSQL(liCheck9.GetItemValue(liCheck9.SelectedIndices(i)).ToString) & "|"
          strRpt += "=" & ConvStrRpt(liCheck9.GetItemValue(liCheck9.SelectedIndices(i)).ToString) & "|"
        Next
        strQuery = strQuery.Substring(0, strQuery.Length - 1) & "§"
        strRpt = strRpt.Substring(0, strRpt.Length - 1) & "§"
      End If

      If liCheck10.SelectedIndices.Count > 0 And liCheck10.SelectedIndices.Count < liCheck10.ItemCount Then
        strQuery += "anaext.ax_check10|"
        strRpt += "{anaext.ax_check10}|"
        For i = 0 To liCheck10.SelectedIndices.Count - 1
          strQuery += "=" & CStrSQL(liCheck10.GetItemValue(liCheck10.SelectedIndices(i)).ToString) & "|"
          strRpt += "=" & ConvStrRpt(liCheck10.GetItemValue(liCheck10.SelectedIndices(i)).ToString) & "|"
        Next
        strQuery = strQuery.Substring(0, strQuery.Length - 1) & "§"
        strRpt = strRpt.Substring(0, strRpt.Length - 1) & "§"
      End If


      If liList1.SelectedIndices.Count > 0 And liList1.SelectedIndices.Count < liList1.ItemCount Then
        strQuery += "anaext.ax_combo1|"
        strRpt += "{anaext.ax_combo1}|"
        For i = 0 To liList1.SelectedIndices.Count - 1
          strQuery += "=" & CStrSQL(liList1.GetItemValue(liList1.SelectedIndices(i)).ToString) & "|"
          strRpt += "=" & ConvStrRpt(liList1.GetItemValue(liList1.SelectedIndices(i)).ToString) & "|"
        Next
        strQuery = strQuery.Substring(0, strQuery.Length - 1) & "§"
        strRpt = strRpt.Substring(0, strRpt.Length - 1) & "§"
      End If

      If liList2.SelectedIndices.Count > 0 And liList2.SelectedIndices.Count < liList2.ItemCount Then
        strQuery += "anaext.ax_combo2|"
        strRpt += "{anaext.ax_combo2}|"
        For i = 0 To liList2.SelectedIndices.Count - 1
          strQuery += "=" & CStrSQL(liList2.GetItemValue(liList2.SelectedIndices(i)).ToString) & "|"
          strRpt += "=" & ConvStrRpt(liList2.GetItemValue(liList2.SelectedIndices(i)).ToString) & "|"
        Next
        strQuery = strQuery.Substring(0, strQuery.Length - 1) & "§"
        strRpt = strRpt.Substring(0, strRpt.Length - 1) & "§"
      End If

      If liList3.SelectedIndices.Count > 0 And liList3.SelectedIndices.Count < liList3.ItemCount Then
        strQuery += "anaext.ax_combo3|"
        strRpt += "{anaext.ax_combo3}|"
        For i = 0 To liList3.SelectedIndices.Count - 1
          strQuery += "=" & CStrSQL(liList3.GetItemValue(liList3.SelectedIndices(i)).ToString) & "|"
          strRpt += "=" & ConvStrRpt(liList3.GetItemValue(liList3.SelectedIndices(i)).ToString) & "|"
        Next
        strQuery = strQuery.Substring(0, strQuery.Length - 1) & "§"
        strRpt = strRpt.Substring(0, strRpt.Length - 1) & "§"
      End If

      oCallParams.strPar1 = strQuery
      oCallParams.strPar2 = strRpt
      oCallParams.bPar1 = True
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Function CampoTesto(ByVal strTesto As String) As String
    Dim strOut As String = ""
    Dim bFil As String = ""
    Try
      If strTesto.Length > 1 Then
        If strTesto.Substring(strTesto.Length - 1, 1) = "*" Then
          strTesto = strTesto.Substring(0, strTesto.Length - 1)
        End If
        If strTesto.Substring(0, 1) = "*" Then
          strTesto = "%" + strTesto.Substring(1)
        ElseIf bOttimistico Then
          strTesto = "%" + strTesto
        End If
      End If
      strOut = CStrSQL(strTesto & "%")

      Return strOut

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return "''"
    End Try
  End Function
  Public Overridable Function CampoTestoRpt(ByVal strTesto As String) As String
    Dim strOut As String = ""
    Try
      strOut = strOut.Replace("_", "?").Replace("%", "*")

      Return ConvStrRpt(strOut)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return "''"
    End Try
  End Function

  Public Overridable Sub CaricaComboBox(ByVal dtrTmp As DataRow)

    Dim dttCheck1 As New DataTable()
    Dim dttCheck2 As New DataTable()
    Dim dttCheck3 As New DataTable()
    Dim dttCheck4 As New DataTable()
    Dim dttCheck5 As New DataTable()
    Dim dttCheck6 As New DataTable()
    Dim dttCheck7 As New DataTable()
    Dim dttCheck8 As New DataTable()
    Dim dttCheck9 As New DataTable()
    Dim dttCheck10 As New DataTable()
    Dim dttCombo1 As New DataTable()
    Dim dttCombo2 As New DataTable()
    Dim dttCombo3 As New DataTable()
    Dim i As Integer

    Try

      dttCheck1.Columns.Add("cod", GetType(String)) : dttCheck1.Columns.Add("val", GetType(String)) : dttCheck1.Rows.Add(New Object() {"S", "Si"}) : dttCheck1.Rows.Add(New Object() {"N", "No"})
      dttCheck2.Columns.Add("cod", GetType(String)) : dttCheck2.Columns.Add("val", GetType(String)) : dttCheck2.Rows.Add(New Object() {"S", "Si"}) : dttCheck2.Rows.Add(New Object() {"N", "No"})
      dttCheck3.Columns.Add("cod", GetType(String)) : dttCheck3.Columns.Add("val", GetType(String)) : dttCheck3.Rows.Add(New Object() {"S", "Si"}) : dttCheck3.Rows.Add(New Object() {"N", "No"})
      dttCheck4.Columns.Add("cod", GetType(String)) : dttCheck4.Columns.Add("val", GetType(String)) : dttCheck4.Rows.Add(New Object() {"S", "Si"}) : dttCheck4.Rows.Add(New Object() {"N", "No"})
      dttCheck5.Columns.Add("cod", GetType(String)) : dttCheck5.Columns.Add("val", GetType(String)) : dttCheck5.Rows.Add(New Object() {"S", "Si"}) : dttCheck5.Rows.Add(New Object() {"N", "No"})
      dttCheck6.Columns.Add("cod", GetType(String)) : dttCheck6.Columns.Add("val", GetType(String)) : dttCheck6.Rows.Add(New Object() {"S", "Si"}) : dttCheck6.Rows.Add(New Object() {"N", "No"})
      dttCheck7.Columns.Add("cod", GetType(String)) : dttCheck7.Columns.Add("val", GetType(String)) : dttCheck7.Rows.Add(New Object() {"S", "Si"}) : dttCheck7.Rows.Add(New Object() {"N", "No"})
      dttCheck8.Columns.Add("cod", GetType(String)) : dttCheck8.Columns.Add("val", GetType(String)) : dttCheck8.Rows.Add(New Object() {"S", "Si"}) : dttCheck8.Rows.Add(New Object() {"N", "No"})
      dttCheck9.Columns.Add("cod", GetType(String)) : dttCheck9.Columns.Add("val", GetType(String)) : dttCheck9.Rows.Add(New Object() {"S", "Si"}) : dttCheck9.Rows.Add(New Object() {"N", "No"})
      dttCheck10.Columns.Add("cod", GetType(String)) : dttCheck10.Columns.Add("val", GetType(String)) : dttCheck10.Rows.Add(New Object() {"S", "Si"}) : dttCheck10.Rows.Add(New Object() {"N", "No"})

      dttCombo1.Columns.Add("cod", GetType(String)) : dttCombo1.Columns.Add("val", GetType(String))
      dttCombo2.Columns.Add("cod", GetType(String)) : dttCombo2.Columns.Add("val", GetType(String))
      dttCombo3.Columns.Add("cod", GetType(String)) : dttCombo3.Columns.Add("val", GetType(String))

      liCheck1.DataSource = dttCheck1 : liCheck1.DisplayMember = "val" : liCheck1.ValueMember = "cod"
      For i = 0 To liCheck1.ItemCount - 1
        liCheck1.SetSelected(i, True)
      Next
      liCheck2.DataSource = dttCheck2 : liCheck2.DisplayMember = "val" : liCheck2.ValueMember = "cod"
      For i = 0 To liCheck2.ItemCount - 1
        liCheck2.SetSelected(i, True)
      Next
      liCheck3.DataSource = dttCheck3 : liCheck3.DisplayMember = "val" : liCheck3.ValueMember = "cod"
      For i = 0 To liCheck3.ItemCount - 1
        liCheck3.SetSelected(i, True)
      Next
      liCheck4.DataSource = dttCheck4 : liCheck4.DisplayMember = "val" : liCheck4.ValueMember = "cod"
      For i = 0 To liCheck4.ItemCount - 1
        liCheck4.SetSelected(i, True)
      Next
      liCheck5.DataSource = dttCheck5 : liCheck5.DisplayMember = "val" : liCheck5.ValueMember = "cod"
      For i = 0 To liCheck5.ItemCount - 1
        liCheck5.SetSelected(i, True)
      Next
      liCheck6.DataSource = dttCheck6 : liCheck6.DisplayMember = "val" : liCheck6.ValueMember = "cod"
      For i = 0 To liCheck6.ItemCount - 1
        liCheck6.SetSelected(i, True)
      Next
      liCheck7.DataSource = dttCheck7 : liCheck7.DisplayMember = "val" : liCheck7.ValueMember = "cod"
      For i = 0 To liCheck7.ItemCount - 1
        liCheck7.SetSelected(i, True)
      Next
      liCheck8.DataSource = dttCheck8 : liCheck8.DisplayMember = "val" : liCheck8.ValueMember = "cod"
      For i = 0 To liCheck8.ItemCount - 1
        liCheck8.SetSelected(i, True)
      Next
      liCheck9.DataSource = dttCheck9 : liCheck9.DisplayMember = "val" : liCheck9.ValueMember = "cod"
      For i = 0 To liCheck9.ItemCount - 1
        liCheck9.SetSelected(i, True)
      Next
      liCheck10.DataSource = dttCheck10 : liCheck10.DisplayMember = "val" : liCheck10.ValueMember = "cod"
      For i = 0 To liCheck10.ItemCount - 1
        liCheck10.SetSelected(i, True)
      Next

      '-----------------------------------
      'ora carico i combobox
      If dtrTmp!tb_helpcom1_A.ToString.Trim <> "" Then dttCombo1.Rows.Add(New Object() {"A", dtrTmp!tb_helpcom1_A.ToString.Trim})
      If dtrTmp!tb_helpcom1_B.ToString.Trim <> "" Then dttCombo1.Rows.Add(New Object() {"B", dtrTmp!tb_helpcom1_B.ToString.Trim})
      If dtrTmp!tb_helpcom1_C.ToString.Trim <> "" Then dttCombo1.Rows.Add(New Object() {"C", dtrTmp!tb_helpcom1_C.ToString.Trim})
      If dtrTmp!tb_helpcom1_D.ToString.Trim <> "" Then dttCombo1.Rows.Add(New Object() {"D", dtrTmp!tb_helpcom1_D.ToString.Trim})
      If dtrTmp!tb_helpcom1_E.ToString.Trim <> "" Then dttCombo1.Rows.Add(New Object() {"E", dtrTmp!tb_helpcom1_E.ToString.Trim})
      If dtrTmp!tb_helpcom1_F.ToString.Trim <> "" Then dttCombo1.Rows.Add(New Object() {"F", dtrTmp!tb_helpcom1_F.ToString.Trim})
      If dtrTmp!tb_helpcom1_G.ToString.Trim <> "" Then dttCombo1.Rows.Add(New Object() {"G", dtrTmp!tb_helpcom1_G.ToString.Trim})
      If dtrTmp!tb_helpcom1_H.ToString.Trim <> "" Then dttCombo1.Rows.Add(New Object() {"H", dtrTmp!tb_helpcom1_H.ToString.Trim})
      If dtrTmp!tb_helpcom1_I.ToString.Trim <> "" Then dttCombo1.Rows.Add(New Object() {"I", dtrTmp!tb_helpcom1_I.ToString.Trim})
      If dtrTmp!tb_helpcom1_L.ToString.Trim <> "" Then dttCombo1.Rows.Add(New Object() {"L", dtrTmp!tb_helpcom1_L.ToString.Trim})
      liList1.DataSource = dttCombo1 : liList1.DisplayMember = "val" : liList1.ValueMember = "cod"
      For i = 0 To liList1.ItemCount - 1
        liList1.SetSelected(i, True)
      Next

      If dtrTmp!tb_helpcom2_A.ToString.Trim <> "" Then dttCombo2.Rows.Add(New Object() {"A", dtrTmp!tb_helpcom2_A.ToString.Trim})
      If dtrTmp!tb_helpcom2_B.ToString.Trim <> "" Then dttCombo2.Rows.Add(New Object() {"B", dtrTmp!tb_helpcom2_B.ToString.Trim})
      If dtrTmp!tb_helpcom2_C.ToString.Trim <> "" Then dttCombo2.Rows.Add(New Object() {"C", dtrTmp!tb_helpcom2_C.ToString.Trim})
      If dtrTmp!tb_helpcom2_D.ToString.Trim <> "" Then dttCombo2.Rows.Add(New Object() {"D", dtrTmp!tb_helpcom2_D.ToString.Trim})
      If dtrTmp!tb_helpcom2_E.ToString.Trim <> "" Then dttCombo2.Rows.Add(New Object() {"E", dtrTmp!tb_helpcom2_E.ToString.Trim})
      liList2.DataSource = dttCombo2 : liList2.DisplayMember = "val" : liList2.ValueMember = "cod"
      For i = 0 To liList2.ItemCount - 1
        liList2.SetSelected(i, True)
      Next

      If dtrTmp!tb_helpcom3_A.ToString.Trim <> "" Then dttCombo3.Rows.Add(New Object() {"A", dtrTmp!tb_helpcom3_A.ToString.Trim})
      If dtrTmp!tb_helpcom3_B.ToString.Trim <> "" Then dttCombo3.Rows.Add(New Object() {"B", dtrTmp!tb_helpcom3_B.ToString.Trim})
      If dtrTmp!tb_helpcom3_C.ToString.Trim <> "" Then dttCombo3.Rows.Add(New Object() {"C", dtrTmp!tb_helpcom3_C.ToString.Trim})
      If dtrTmp!tb_helpcom3_D.ToString.Trim <> "" Then dttCombo3.Rows.Add(New Object() {"D", dtrTmp!tb_helpcom3_D.ToString.Trim})
      If dtrTmp!tb_helpcom3_E.ToString.Trim <> "" Then dttCombo3.Rows.Add(New Object() {"E", dtrTmp!tb_helpcom3_E.ToString.Trim})
      If dtrTmp!tb_helpcom3_F.ToString.Trim <> "" Then dttCombo3.Rows.Add(New Object() {"F", dtrTmp!tb_helpcom3_F.ToString.Trim})
      If dtrTmp!tb_helpcom3_G.ToString.Trim <> "" Then dttCombo3.Rows.Add(New Object() {"G", dtrTmp!tb_helpcom3_G.ToString.Trim})
      If dtrTmp!tb_helpcom3_H.ToString.Trim <> "" Then dttCombo3.Rows.Add(New Object() {"H", dtrTmp!tb_helpcom3_H.ToString.Trim})
      If dtrTmp!tb_helpcom3_I.ToString.Trim <> "" Then dttCombo3.Rows.Add(New Object() {"I", dtrTmp!tb_helpcom3_I.ToString.Trim})
      If dtrTmp!tb_helpcom3_L.ToString.Trim <> "" Then dttCombo3.Rows.Add(New Object() {"L", dtrTmp!tb_helpcom3_L.ToString.Trim})
      If dtrTmp!tb_helpcom3_M.ToString.Trim <> "" Then dttCombo3.Rows.Add(New Object() {"M", dtrTmp!tb_helpcom3_M.ToString.Trim})
      If dtrTmp!tb_helpcom3_N.ToString.Trim <> "" Then dttCombo3.Rows.Add(New Object() {"N", dtrTmp!tb_helpcom3_N.ToString.Trim})
      If dtrTmp!tb_helpcom3_O.ToString.Trim <> "" Then dttCombo3.Rows.Add(New Object() {"O", dtrTmp!tb_helpcom3_O.ToString.Trim})
      If dtrTmp!tb_helpcom3_P.ToString.Trim <> "" Then dttCombo3.Rows.Add(New Object() {"P", dtrTmp!tb_helpcom3_P.ToString.Trim})
      If dtrTmp!tb_helpcom3_Q.ToString.Trim <> "" Then dttCombo3.Rows.Add(New Object() {"Q", dtrTmp!tb_helpcom3_Q.ToString.Trim})
      If dtrTmp!tb_helpcom3_R.ToString.Trim <> "" Then dttCombo3.Rows.Add(New Object() {"R", dtrTmp!tb_helpcom3_R.ToString.Trim})
      If dtrTmp!tb_helpcom3_S.ToString.Trim <> "" Then dttCombo3.Rows.Add(New Object() {"S", dtrTmp!tb_helpcom3_S.ToString.Trim})
      If dtrTmp!tb_helpcom3_T.ToString.Trim <> "" Then dttCombo3.Rows.Add(New Object() {"T", dtrTmp!tb_helpcom3_T.ToString.Trim})
      If dtrTmp!tb_helpcom3_U.ToString.Trim <> "" Then dttCombo3.Rows.Add(New Object() {"U", dtrTmp!tb_helpcom3_U.ToString.Trim})
      If dtrTmp!tb_helpcom3_V.ToString.Trim <> "" Then dttCombo3.Rows.Add(New Object() {"V", dtrTmp!tb_helpcom3_V.ToString.Trim})
      liList3.DataSource = dttCombo3 : liList3.DisplayMember = "val" : liList3.ValueMember = "cod"
      For i = 0 To liList3.ItemCount - 1
        liList3.SetSelected(i, True)
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ImpostaControlli(ByVal dtrTmp As DataRow)
    Try
      '-------------------------------------------------
      'gestione dei tab
      If dtrTmp!tb_destab1.ToString.Trim <> "" Then pnTab1.Text = "1 - " & dtrTmp!tb_destab1.ToString.Trim Else pnTab1.Visible = False
      If dtrTmp!tb_destab2.ToString.Trim <> "" Then pnTab2.Text = "2 - " & dtrTmp!tb_destab2.ToString.Trim Else pnTab2.Visible = False
      If dtrTmp!tb_destab3.ToString.Trim <> "" Then pnTab3.Text = "3 - " & dtrTmp!tb_destab3.ToString.Trim Else pnTab3.Visible = False
      If dtrTmp!tb_destab4.ToString.Trim <> "" Then pnTab4.Text = "4 - " & dtrTmp!tb_destab4.ToString.Trim Else pnTab4.Visible = False
      If dtrTmp!tb_destab5.ToString.Trim <> "" Then pnTab5.Text = "5 - " & dtrTmp!tb_destab5.ToString.Trim Else pnTab5.Visible = False
      If dtrTmp!tb_destab6.ToString.Trim <> "" Then pnTab6.Text = "6 - " & dtrTmp!tb_destab6.ToString.Trim Else pnTab6.Visible = False

      '-------------------------------------------------
      'gestione delle label e visibilit dei campi
      'tab 1
      If dtrTmp!tb_tipo1.ToString.Trim <> "" Then
        lbTipo1.Text = dtrTmp!tb_tipo1.ToString.Trim
        lbHelpTipo1.Text = dtrTmp!tb_helptipo1.ToString.Trim
      Else
        lbTipo1.Visible = False
        edTipo1.Visible = False
        lbHelpTipo1.Visible = False
      End If
      If dtrTmp!tb_tipo2.ToString.Trim <> "" Then
        lbTipo2.Text = dtrTmp!tb_tipo2.ToString.Trim
        lbHelpTipo2.Text = dtrTmp!tb_helptipo2.ToString.Trim
      Else
        lbTipo2.Visible = False
        edTipo2.Visible = False
        lbHelpTipo2.Visible = False
      End If
      If dtrTmp!tb_tipo3.ToString.Trim <> "" Then
        lbTipo3.Text = dtrTmp!tb_tipo3.ToString.Trim
        lbHelpTipo3.Text = dtrTmp!tb_helptipo3.ToString.Trim
      Else
        lbTipo3.Visible = False
        edTipo3.Visible = False
        lbHelpTipo3.Visible = False
      End If
      '-------------------------------------------
      If dtrTmp!tb_desext1.ToString.Trim <> "" Then
        lbDesEx1.Text = dtrTmp!tb_desext1.ToString.Trim
      Else
        lbDesEx1.Visible = False : edDesex1.Visible = False
      End If
      If dtrTmp!tb_desext2.ToString.Trim <> "" Then
        lbDesEx2.Text = dtrTmp!tb_desext2.ToString.Trim
      Else
        lbDesEx2.Visible = False : edDesex2.Visible = False
      End If
      If dtrTmp!tb_desext3.ToString.Trim <> "" Then
        lbDesEx3.Text = dtrTmp!tb_desext3.ToString.Trim
      Else
        lbDesEx3.Visible = False : edDesex3.Visible = False
      End If
      '-------------------------------------------------
      'tab 2
      If dtrTmp!tb_descr1.ToString.Trim <> "" Then
        lbDescr1.Text = dtrTmp!tb_descr1.ToString.Trim
      Else
        lbDescr1.Visible = False : edDescr1.Visible = False
      End If
      If dtrTmp!tb_descr2.ToString.Trim <> "" Then
        lbDescr2.Text = dtrTmp!tb_descr2.ToString.Trim
      Else
        lbDescr2.Visible = False : edDescr2.Visible = False
      End If
      If dtrTmp!tb_descr3.ToString.Trim <> "" Then
        lbDescr3.Text = dtrTmp!tb_descr3.ToString.Trim
      Else
        lbDescr3.Visible = False : edDescr3.Visible = False
      End If
      If dtrTmp!tb_descr4.ToString.Trim <> "" Then
        lbDescr4.Text = dtrTmp!tb_descr4.ToString.Trim
      Else
        lbDescr4.Visible = False : edDescr4.Visible = False
      End If
      If dtrTmp!tb_descr5.ToString.Trim <> "" Then
        lbDescr5.Text = dtrTmp!tb_descr5.ToString.Trim
      Else
        lbDescr5.Visible = False : edDescr5.Visible = False
      End If
      If dtrTmp!tb_descr6.ToString.Trim <> "" Then
        lbDescr6.Text = dtrTmp!tb_descr6.ToString.Trim
      Else
        lbDescr6.Visible = False : edDescr6.Visible = False
      End If
      If dtrTmp!tb_descr7.ToString.Trim <> "" Then
        lbDescr7.Text = dtrTmp!tb_descr7.ToString.Trim
      Else
        lbDescr7.Visible = False : edDescr7.Visible = False
      End If
      If dtrTmp!tb_descr8.ToString.Trim <> "" Then
        lbDescr8.Text = dtrTmp!tb_descr8.ToString.Trim
      Else
        lbDescr8.Visible = False : edDescr8.Visible = False
      End If
      If dtrTmp!tb_descr9.ToString.Trim <> "" Then
        lbDescr9.Text = dtrTmp!tb_descr9.ToString.Trim
      Else
        lbDescr9.Visible = False : edDescr9.Visible = False
      End If
      If dtrTmp!tb_descr10.ToString.Trim <> "" Then
        lbDescr10.Text = dtrTmp!tb_descr10.ToString.Trim
      Else
        lbDescr10.Visible = False : edDescr10.Visible = False
      End If
      '---------------------------------
      If dtrTmp!tb_data1.ToString.Trim <> "" Then
        ckData1.Text = dtrTmp!tb_data1.ToString.Trim
      Else
        ckData1.Visible = False : edData1da.Visible = False : edData1a.Visible = False : NtsLabel2.Visible = False : NtsLabel3.Visible = False
      End If
      If dtrTmp!tb_data2.ToString.Trim <> "" Then
        ckData2.Text = dtrTmp!tb_data2.ToString.Trim
      Else
        ckData2.Visible = False : edData2da.Visible = False : edData2a.Visible = False : NtsLabel8.Visible = False : NtsLabel4.Visible = False
      End If
      If dtrTmp!tb_data3.ToString.Trim <> "" Then
        ckData3.Text = dtrTmp!tb_data3.ToString.Trim
      Else
        ckData3.Visible = False : edData3da.Visible = False : edData3a.Visible = False : NtsLabel9.Visible = False : NtsLabel5.Visible = False
      End If
      If dtrTmp!tb_data4.ToString.Trim <> "" Then
        ckData4.Text = dtrTmp!tb_data4.ToString.Trim
      Else
        ckData4.Visible = False : edData4da.Visible = False : edData4a.Visible = False : NtsLabel10.Visible = False : NtsLabel6.Visible = False
      End If
      If dtrTmp!tb_data5.ToString.Trim <> "" Then
        ckData5.Text = dtrTmp!tb_data5.ToString.Trim
      Else
        ckData5.Visible = False : edData5da.Visible = False : edData5a.Visible = False : NtsLabel11.Visible = False : NtsLabel7.Visible = False
      End If
      '-------------------------------------------------
      'tab 3
      If dtrTmp!tb_memo1.ToString.Trim <> "" Then
        lbMemo1.Text = dtrTmp!tb_memo1.ToString.Trim
      Else
        lbMemo1.Visible = False : edMemo1.Visible = False
      End If
      '-------------------------------------------------
      'tab 4
      If dtrTmp!tb_memo2.ToString.Trim <> "" Then
        lbMemo2.Text = dtrTmp!tb_memo2.ToString.Trim
      Else
        lbMemo2.Visible = False : edMemo2.Visible = False
      End If
      '-------------------------------------------------
      'tab 5
      If dtrTmp!tb_num1.ToString.Trim <> "" Then
        ckNum1.Text = dtrTmp!tb_num1.ToString.Trim
      Else
        ckNum1.Visible = False : edNum1da.Visible = False : edNum1a.Visible = False : NtsLabel21.Visible = False : NtsLabel20.Visible = False
      End If
      If dtrTmp!tb_num2.ToString.Trim <> "" Then
        ckNum2.Text = dtrTmp!tb_num2.ToString.Trim
      Else
        ckNum2.Visible = False : edNum2da.Visible = False : edNum2a.Visible = False : NtsLabel15.Visible = False : NtsLabel19.Visible = False
      End If
      If dtrTmp!tb_num3.ToString.Trim <> "" Then
        ckNum3.Text = dtrTmp!tb_num3.ToString.Trim
      Else
        ckNum3.Visible = False : edNum3da.Visible = False : edNum3a.Visible = False : NtsLabel14.Visible = False : NtsLabel18.Visible = False
      End If
      If dtrTmp!tb_num4.ToString.Trim <> "" Then
        ckNum4.Text = dtrTmp!tb_num4.ToString.Trim
      Else
        ckNum4.Visible = False : edNum4da.Visible = False : edNum4a.Visible = False : NtsLabel13.Visible = False : NtsLabel17.Visible = False
      End If
      If dtrTmp!tb_num5.ToString.Trim <> "" Then
        ckNum5.Text = dtrTmp!tb_num5.ToString.Trim
      Else
        ckNum5.Visible = False : edNum5da.Visible = False : edNum5a.Visible = False : NtsLabel12.Visible = False : NtsLabel16.Visible = False
      End If
      If dtrTmp!tb_num6.ToString.Trim <> "" Then
        ckNum6.Text = dtrTmp!tb_num6.ToString.Trim
      Else
        ckNum6.Visible = False : edNum6da.Visible = False : edNum6a.Visible = False : NtsLabel31.Visible = False : NtsLabel30.Visible = False
      End If
      If dtrTmp!tb_num7.ToString.Trim <> "" Then
        ckNum7.Text = dtrTmp!tb_num7.ToString.Trim
      Else
        ckNum7.Visible = False : edNum7da.Visible = False : edNum7a.Visible = False : NtsLabel25.Visible = False : NtsLabel29.Visible = False
      End If
      If dtrTmp!tb_num8.ToString.Trim <> "" Then
        ckNum8.Text = dtrTmp!tb_num8.ToString.Trim
      Else
        ckNum8.Visible = False : edNum8da.Visible = False : edNum8a.Visible = False : NtsLabel24.Visible = False : NtsLabel28.Visible = False
      End If
      If dtrTmp!tb_num9.ToString.Trim <> "" Then
        ckNum9.Text = dtrTmp!tb_num9.ToString.Trim
      Else
        ckNum9.Visible = False : edNum9da.Visible = False : edNum9a.Visible = False : NtsLabel23.Visible = False : NtsLabel27.Visible = False
      End If
      If dtrTmp!tb_num10.ToString.Trim <> "" Then
        ckNum10.Text = dtrTmp!tb_num10.ToString.Trim
      Else
        ckNum10.Visible = False : edNum10da.Visible = False : edNum10a.Visible = False : NtsLabel22.Visible = False : NtsLabel26.Visible = False
      End If
      '-------------------------------------------------
      'tab 6
      If dtrTmp!tb_check1.ToString.Trim <> "" Then
        lbCheck1.Text = dtrTmp!tb_check1.ToString.Trim
      Else
        lbCheck1.Visible = False : liCheck1.Visible = False
      End If
      If dtrTmp!tb_check2.ToString.Trim <> "" Then
        lbCheck2.Text = dtrTmp!tb_check2.ToString.Trim
      Else
        lbCheck2.Visible = False : liCheck2.Visible = False
      End If
      If dtrTmp!tb_check3.ToString.Trim <> "" Then
        lbCheck3.Text = dtrTmp!tb_check3.ToString.Trim
      Else
        lbCheck3.Visible = False : liCheck3.Visible = False
      End If
      If dtrTmp!tb_check4.ToString.Trim <> "" Then
        lbCheck4.Text = dtrTmp!tb_check4.ToString.Trim
      Else
        lbCheck4.Visible = False : liCheck4.Visible = False
      End If
      If dtrTmp!tb_check5.ToString.Trim <> "" Then
        lbCheck5.Text = dtrTmp!tb_check5.ToString.Trim
      Else
        lbCheck5.Visible = False : liCheck5.Visible = False
      End If
      If dtrTmp!tb_check6.ToString.Trim <> "" Then
        lbCheck6.Text = dtrTmp!tb_check6.ToString.Trim
      Else
        lbCheck6.Visible = False : liCheck6.Visible = False
      End If
      If dtrTmp!tb_check7.ToString.Trim <> "" Then
        lbCheck7.Text = dtrTmp!tb_check7.ToString.Trim
      Else
        lbCheck7.Visible = False : liCheck7.Visible = False
      End If
      If dtrTmp!tb_check8.ToString.Trim <> "" Then
        lbCheck8.Text = dtrTmp!tb_check8.ToString.Trim
      Else
        lbCheck8.Visible = False : liCheck8.Visible = False
      End If
      If dtrTmp!tb_check9.ToString.Trim <> "" Then
        lbCheck9.Text = dtrTmp!tb_check9.ToString.Trim
      Else
        lbCheck9.Visible = False : liCheck9.Visible = False
      End If
      If dtrTmp!tb_check10.ToString.Trim <> "" Then
        lbCheck10.Text = dtrTmp!tb_check10.ToString.Trim
      Else
        lbCheck10.Visible = False : liCheck10.Visible = False
      End If
      '-----------------------------
      If dtrTmp!tb_combo1.ToString.Trim <> "" Then
        lbCombo1.Text = dtrTmp!tb_combo1.ToString.Trim
      Else
        lbCombo1.Visible = False : liList1.Visible = False
      End If
      If dtrTmp!tb_combo2.ToString.Trim <> "" Then
        lbCombo2.Text = dtrTmp!tb_combo2.ToString.Trim
      Else
        lbCombo2.Visible = False : liList2.Visible = False
      End If
      If dtrTmp!tb_combo3.ToString.Trim <> "" Then
        lbCombo3.Text = dtrTmp!tb_combo3.ToString.Trim
      Else
        lbCombo3.Visible = False : liList3.Visible = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub CreaDataTable()
    Dim i As Integer = 0
    Dim n As Integer = 0
    Dim strTmp(11) As String
    Dim strCombo(21) As String

    Try
      '--------------------------------------------------------------------------------------------------------------
      For i = 1 To 3
        dttAnex.Columns.Add("Tipo" & i.ToString, GetType(String))
      Next
      For i = 1 To 10
        dttAnex.Columns.Add("Descr" & i.ToString, GetType(String))
      Next
      For i = 1 To 3
        dttAnex.Columns.Add("Desext" & i.ToString, GetType(String))
      Next
      For i = 1 To 5
        dttAnex.Columns.Add("SelData" & i.ToString, GetType(String))
      Next
      For i = 1 To 5
        dttAnex.Columns.Add("DataI" & i.ToString, GetType(String))
      Next
      For i = 1 To 5
        dttAnex.Columns.Add("DataF" & i.ToString, GetType(String))
      Next
      For i = 1 To 10
        dttAnex.Columns.Add("SelNum" & i.ToString, GetType(String))
      Next
      For i = 1 To 10
        dttAnex.Columns.Add("NumI" & i.ToString, GetType(Decimal))
      Next
      For i = 1 To 10
        dttAnex.Columns.Add("NumF" & i.ToString, GetType(Decimal))
      Next
      For i = 1 To 10
        dttAnex.Columns.Add("Check" & i.ToString, GetType(String))
      Next
      For i = 65 To 76
        If (i <> 74) And (i <> 75) Then dttAnex.Columns.Add("Combo1" & Chr(i).ToString, GetType(String))
      Next
      For i = 65 To 69
        dttAnex.Columns.Add("Combo2" & Chr(i).ToString, GetType(String))
      Next
      For i = 65 To 86
        If (i <> 74) And (i <> 75) Then dttAnex.Columns.Add("Combo3" & Chr(i).ToString, GetType(String))
      Next
      '--------------------------------------------------------------------------------------------------------------
      dttAnex.Rows.Add()
      '--------------------------------------------------------------------------------------------------------------
      dttAnex.Rows(0)!Tipo1 = IIf(edTipo1.Text = "", "*", edTipo1.Text).ToString
      dttAnex.Rows(0)!Tipo2 = IIf(edTipo2.Text = "", "*", edTipo2.Text).ToString
      dttAnex.Rows(0)!Tipo3 = IIf(edTipo3.Text = "", "*", edTipo3.Text).ToString
      '--------------------------------------------------------------------------------------------------------------
      dttAnex.Rows(0)!Descr1 = IIf(edDescr1.Text = "", "*", edDescr1.Text).ToString
      dttAnex.Rows(0)!Descr2 = IIf(edDescr2.Text = "", " * ", edDescr2.Text).ToString
      dttAnex.Rows(0)!Descr3 = IIf(edDescr3.Text = "", " * ", edDescr3.Text).ToString
      dttAnex.Rows(0)!Descr4 = IIf(edDescr4.Text = "", " * ", edDescr4.Text).ToString
      dttAnex.Rows(0)!Descr5 = IIf(edDescr5.Text = "", " * ", edDescr5.Text).ToString
      dttAnex.Rows(0)!Descr6 = IIf(edDescr6.Text = "", " * ", edDescr6.Text).ToString
      dttAnex.Rows(0)!Descr7 = IIf(edDescr7.Text = "", " * ", edDescr7.Text).ToString
      dttAnex.Rows(0)!Descr8 = IIf(edDescr8.Text = "", " * ", edDescr8.Text).ToString
      dttAnex.Rows(0)!Descr9 = IIf(edDescr9.Text = "", " * ", edDescr9.Text).ToString
      dttAnex.Rows(0)!Descr10 = IIf(edDescr10.Text = "", " * ", edDescr10.Text).ToString
      '--------------------------------------------------------------------------------------------------------------
      dttAnex.Rows(0)!Desext1 = IIf(edDesex1.Text = "", " * ", edDesex1.Text).ToString
      dttAnex.Rows(0)!Desext2 = IIf(edDesex2.Text = "", " * ", edDesex2.Text).ToString
      dttAnex.Rows(0)!Desext3 = IIf(edDesex3.Text = "", " * ", edDesex3.Text).ToString
      '--------------------------------------------------------------------------------------------------------------
      dttAnex.Rows(0)!SelData1 = IIf(ckData1.Checked = True, "S", "N").ToString
      dttAnex.Rows(0)!SelData2 = IIf(ckData2.Checked = True, "S", "N").ToString
      dttAnex.Rows(0)!SelData3 = IIf(ckData3.Checked = True, "S", "N").ToString
      dttAnex.Rows(0)!SelData4 = IIf(ckData4.Checked = True, "S", "N").ToString
      dttAnex.Rows(0)!SelData5 = IIf(ckData5.Checked = True, "S", "N").ToString
      '--------------------------------------------------------------------------------------------------------------
      dttAnex.Rows(0)!DataI1 = edData1da.Text
      dttAnex.Rows(0)!DataI2 = edData2da.Text
      dttAnex.Rows(0)!DataI3 = edData3da.Text
      dttAnex.Rows(0)!DataI4 = edData4da.Text
      dttAnex.Rows(0)!DataI5 = edData5da.Text
      dttAnex.Rows(0)!DataF1 = edData1a.Text
      dttAnex.Rows(0)!DataF2 = edData2a.Text
      dttAnex.Rows(0)!DataF3 = edData3a.Text
      dttAnex.Rows(0)!DataF4 = edData4a.Text
      dttAnex.Rows(0)!DataF5 = edData5a.Text
      '--------------------------------------------------------------------------------------------------------------                  
      dttAnex.Rows(0)!SelNum1 = IIf(ckNum1.Checked = True, "S", "N").ToString
      dttAnex.Rows(0)!SelNum2 = IIf(ckNum2.Checked = True, "S", "N").ToString
      dttAnex.Rows(0)!SelNum3 = IIf(ckNum3.Checked = True, "S", "N").ToString
      dttAnex.Rows(0)!SelNum4 = IIf(ckNum4.Checked = True, "S", "N").ToString
      dttAnex.Rows(0)!SelNum5 = IIf(ckNum5.Checked = True, "S", "N").ToString
      dttAnex.Rows(0)!SelNum6 = IIf(ckNum6.Checked = True, "S", "N").ToString
      dttAnex.Rows(0)!SelNum7 = IIf(ckNum7.Checked = True, "S", "N").ToString
      dttAnex.Rows(0)!SelNum8 = IIf(ckNum8.Checked = True, "S", "N").ToString
      dttAnex.Rows(0)!SelNum9 = IIf(ckNum9.Checked = True, "S", "N").ToString
      dttAnex.Rows(0)!SelNum10 = IIf(ckNum10.Checked = True, "S", "N").ToString
      '--------------------------------------------------------------------------------------------------------------
      dttAnex.Rows(0)!NumI1 = IIf(ckNum1.Checked = True, NTSCDec(edNum1da.Text), NTSCDec("-999999999999")).ToString
      dttAnex.Rows(0)!NumI2 = IIf(ckNum2.Checked = True, NTSCDec(edNum2da.Text), NTSCDec("-999999999999")).ToString
      dttAnex.Rows(0)!NumI3 = IIf(ckNum3.Checked = True, NTSCDec(edNum3da.Text), NTSCDec("-999999999999")).ToString
      dttAnex.Rows(0)!NumI4 = IIf(ckNum4.Checked = True, NTSCDec(edNum4da.Text), NTSCDec("-999999999999")).ToString
      dttAnex.Rows(0)!NumI5 = IIf(ckNum5.Checked = True, NTSCDec(edNum5da.Text), NTSCDec("-999999999999")).ToString
      dttAnex.Rows(0)!NumI6 = IIf(ckNum6.Checked = True, NTSCDec(edNum6da.Text), NTSCDec("-999999999999")).ToString
      dttAnex.Rows(0)!NumI7 = IIf(ckNum7.Checked = True, NTSCDec(edNum7da.Text), NTSCDec("-999999999999")).ToString
      dttAnex.Rows(0)!NumI8 = IIf(ckNum8.Checked = True, NTSCDec(edNum8da.Text), NTSCDec("-999999999999")).ToString
      dttAnex.Rows(0)!NumI9 = IIf(ckNum9.Checked = True, NTSCDec(edNum9da.Text), NTSCDec("-999999999999")).ToString
      dttAnex.Rows(0)!NumI10 = IIf(ckNum10.Checked = True, NTSCDec(edNum10da.Text), NTSCDec("-999999999999")).ToString
      '--------------------------------------------------------------------------------------------------------------
      dttAnex.Rows(0)!NumF1 = IIf(ckNum1.Checked = True, NTSCDec(edNum1a.Text), NTSCDec("999999999999")).ToString
      dttAnex.Rows(0)!NumF2 = IIf(ckNum2.Checked = True, NTSCDec(edNum2a.Text), NTSCDec("999999999999")).ToString
      dttAnex.Rows(0)!NumF3 = IIf(ckNum3.Checked = True, NTSCDec(edNum3a.Text), NTSCDec("999999999999")).ToString
      dttAnex.Rows(0)!NumF4 = IIf(ckNum4.Checked = True, NTSCDec(edNum4a.Text), NTSCDec("999999999999")).ToString
      dttAnex.Rows(0)!NumF5 = IIf(ckNum5.Checked = True, NTSCDec(edNum5a.Text), NTSCDec("999999999999")).ToString
      dttAnex.Rows(0)!NumF6 = IIf(ckNum6.Checked = True, NTSCDec(edNum6a.Text), NTSCDec("999999999999")).ToString
      dttAnex.Rows(0)!NumF7 = IIf(ckNum7.Checked = True, NTSCDec(edNum7a.Text), NTSCDec("999999999999")).ToString
      dttAnex.Rows(0)!NumF8 = IIf(ckNum8.Checked = True, NTSCDec(edNum8a.Text), NTSCDec("999999999999")).ToString
      dttAnex.Rows(0)!NumF9 = IIf(ckNum9.Checked = True, NTSCDec(edNum9a.Text), NTSCDec("999999999999")).ToString
      dttAnex.Rows(0)!NumF10 = IIf(ckNum10.Checked = True, NTSCDec(edNum10a.Text), NTSCDec("999999999999")).ToString
      '--------------------------------------------------------------------------------------------------------------
      For i = 1 To 10
        strTmp(i) = "E"
      Next
      If (liCheck1.SelectedIndices.Count > 0) And (liCheck1.SelectedIndices.Count < liCheck1.ItemCount) Then
        For i = 0 To (liCheck1.SelectedIndices.Count - 1)
          strTmp(1) = liCheck1.GetItemValue(liCheck1.SelectedIndices(i)).ToString
        Next
      End If
      If (liCheck2.SelectedIndices.Count > 0) And (liCheck2.SelectedIndices.Count < liCheck2.ItemCount) Then
        For i = 0 To (liCheck2.SelectedIndices.Count - 1)
          strTmp(2) = liCheck2.GetItemValue(liCheck2.SelectedIndices(i)).ToString
        Next
      End If
      If (liCheck3.SelectedIndices.Count > 0) And (liCheck3.SelectedIndices.Count < liCheck3.ItemCount) Then
        For i = 0 To (liCheck3.SelectedIndices.Count - 1)
          strTmp(3) = liCheck3.GetItemValue(liCheck3.SelectedIndices(i)).ToString
        Next
      End If
      If (liCheck4.SelectedIndices.Count > 0) And (liCheck4.SelectedIndices.Count < liCheck4.ItemCount) Then
        For i = 0 To (liCheck4.SelectedIndices.Count - 1)
          strTmp(4) = liCheck4.GetItemValue(liCheck4.SelectedIndices(i)).ToString
        Next
      End If
      If (liCheck5.SelectedIndices.Count > 0) And (liCheck5.SelectedIndices.Count < liCheck5.ItemCount) Then
        For i = 0 To (liCheck5.SelectedIndices.Count - 1)
          strTmp(5) = liCheck5.GetItemValue(liCheck5.SelectedIndices(i)).ToString
        Next
      End If
      If (liCheck6.SelectedIndices.Count > 0) And (liCheck6.SelectedIndices.Count < liCheck6.ItemCount) Then
        For i = 0 To (liCheck6.SelectedIndices.Count - 1)
          strTmp(6) = liCheck6.GetItemValue(liCheck6.SelectedIndices(i)).ToString
        Next
      End If
      If (liCheck7.SelectedIndices.Count > 0) And (liCheck7.SelectedIndices.Count < liCheck7.ItemCount) Then
        For i = 0 To (liCheck7.SelectedIndices.Count - 1)
          strTmp(7) = liCheck7.GetItemValue(liCheck7.SelectedIndices(i)).ToString
        Next
      End If
      If (liCheck8.SelectedIndices.Count > 0) And (liCheck8.SelectedIndices.Count < liCheck8.ItemCount) Then
        For i = 0 To (liCheck8.SelectedIndices.Count - 1)
          strTmp(8) = liCheck8.GetItemValue(liCheck8.SelectedIndices(i)).ToString
        Next
      End If
      If (liCheck9.SelectedIndices.Count > 0) And (liCheck9.SelectedIndices.Count < liCheck9.ItemCount) Then
        For i = 0 To (liCheck9.SelectedIndices.Count - 1)
          strTmp(9) = liCheck9.GetItemValue(liCheck9.SelectedIndices(i)).ToString
        Next
      End If
      If (liCheck10.SelectedIndices.Count > 0) And (liCheck10.SelectedIndices.Count < liCheck10.ItemCount) Then
        For i = 0 To (liCheck10.SelectedIndices.Count - 1)
          strTmp(10) = liCheck10.GetItemValue(liCheck10.SelectedIndices(i)).ToString
        Next
      End If
      dttAnex.Rows(0)!Check1 = strTmp(1)
      dttAnex.Rows(0)!Check2 = strTmp(2)
      dttAnex.Rows(0)!Check3 = strTmp(3)
      dttAnex.Rows(0)!Check4 = strTmp(4)
      dttAnex.Rows(0)!Check5 = strTmp(5)
      dttAnex.Rows(0)!Check6 = strTmp(6)
      dttAnex.Rows(0)!Check7 = strTmp(7)
      dttAnex.Rows(0)!Check8 = strTmp(8)
      dttAnex.Rows(0)!Check9 = strTmp(9)
      dttAnex.Rows(0)!Check10 = strTmp(10)
      '--------------------------------------------------------------------------------------------------------------
      For i = 1 To 20
        strCombo(i) = "N"
      Next
      For i = 0 To (liList1.SelectedIndices.Count - 1)
        strCombo(Asc(liList1.GetItemValue(liList1.SelectedIndices(i)).ToString) - 64) = "S"
      Next
      dttAnex.Rows(0)!Combo1A = strCombo(1)
      dttAnex.Rows(0)!Combo1B = strCombo(2)
      dttAnex.Rows(0)!Combo1C = strCombo(3)
      dttAnex.Rows(0)!Combo1D = strCombo(4)
      dttAnex.Rows(0)!Combo1E = strCombo(5)
      dttAnex.Rows(0)!Combo1F = strCombo(6)
      dttAnex.Rows(0)!Combo1G = strCombo(7)
      dttAnex.Rows(0)!Combo1H = strCombo(8)
      dttAnex.Rows(0)!Combo1I = strCombo(9)
      dttAnex.Rows(0)!Combo1L = strCombo(10)
      '--------------------------------------------------------------------------------------------------------------
      For i = 1 To 20
        strCombo(i) = "N"
      Next
      For i = 0 To (liList2.SelectedIndices.Count - 1)
        strCombo(Asc(liList2.GetItemValue(liList2.SelectedIndices(i)).ToString) - 64) = "S"
      Next
      dttAnex.Rows(0)!Combo2A = strCombo(1)
      dttAnex.Rows(0)!Combo2B = strCombo(2)
      dttAnex.Rows(0)!Combo2C = strCombo(3)
      dttAnex.Rows(0)!Combo2D = strCombo(4)
      dttAnex.Rows(0)!Combo2E = strCombo(5)
      '--------------------------------------------------------------------------------------------------------------
      For i = 1 To 20
        strCombo(i) = "N"
      Next
      For i = 0 To (liList3.SelectedIndices.Count - 1)
        n = NTSCInt(IIf(Asc(liList3.GetItemValue(liList3.SelectedIndices(i)).ToString) <= 73, 64, 66))
        strCombo(Asc(liList3.GetItemValue(liList3.SelectedIndices(i)).ToString) - n) = "S"
      Next
      dttAnex.Rows(0)!Combo3A = strCombo(1)
      dttAnex.Rows(0)!Combo3B = strCombo(2)
      dttAnex.Rows(0)!Combo3C = strCombo(3)
      dttAnex.Rows(0)!Combo3D = strCombo(4)
      dttAnex.Rows(0)!Combo3E = strCombo(5)
      dttAnex.Rows(0)!Combo3F = strCombo(6)
      dttAnex.Rows(0)!Combo3G = strCombo(7)
      dttAnex.Rows(0)!Combo3H = strCombo(8)
      dttAnex.Rows(0)!Combo3I = strCombo(9)
      dttAnex.Rows(0)!Combo3L = strCombo(10)
      dttAnex.Rows(0)!Combo3M = strCombo(11)
      dttAnex.Rows(0)!Combo3N = strCombo(12)
      dttAnex.Rows(0)!Combo3O = strCombo(13)
      dttAnex.Rows(0)!Combo3P = strCombo(14)
      dttAnex.Rows(0)!Combo3Q = strCombo(15)
      dttAnex.Rows(0)!Combo3R = strCombo(16)
      dttAnex.Rows(0)!Combo3S = strCombo(17)
      dttAnex.Rows(0)!Combo3T = strCombo(18)
      dttAnex.Rows(0)!Combo3U = strCombo(19)
      dttAnex.Rows(0)!Combo3V = strCombo(20)
      '--------------------------------------------------------------------------------------------------------------      
      dttAnex.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub InizializzaCampiDaEsterno()
    Dim i As Integer = 0
    Dim strTmp() As String

    Try
      '--------------------------------------------------------------------------------------------------------------
      If strProgrChiamante <> "BN__ISTF" Then Return
      '--------------------------------------------------------------------------------------------------------------
      If strFiltriDaEsterno Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      strTmp = strFiltriDaEsterno.Split("§"c)
      '--------------------------------------------------------------------------------------------------------------
      edTipo1.Text = NTSCStr(strTmp(0))
      edTipo2.Text = NTSCStr(strTmp(1))
      edTipo3.Text = NTSCStr(strTmp(2))
      '--------------------------------------------------------------------------------------------------------------
      edDescr1.Text = NTSCStr(strTmp(3))
      edDescr2.Text = NTSCStr(strTmp(4))
      edDescr3.Text = NTSCStr(strTmp(5))
      edDescr4.Text = NTSCStr(strTmp(6))
      edDescr5.Text = NTSCStr(strTmp(7))
      edDescr6.Text = NTSCStr(strTmp(8))
      edDescr7.Text = NTSCStr(strTmp(9))
      edDescr8.Text = NTSCStr(strTmp(10))
      edDescr9.Text = NTSCStr(strTmp(11))
      edDescr10.Text = NTSCStr(strTmp(12))
      '--------------------------------------------------------------------------------------------------------------
      edDesex1.Text = NTSCStr(strTmp(13))
      edDesex2.Text = NTSCStr(strTmp(14))
      edDesex3.Text = NTSCStr(strTmp(15))
      '--------------------------------------------------------------------------------------------------------------
      ckData1.Checked = CBool(IIf(NTSCStr(strTmp(16)) = "S", True, False))
      ckData2.Checked = CBool(IIf(NTSCStr(strTmp(17)) = "S", True, False))
      ckData3.Checked = CBool(IIf(NTSCStr(strTmp(18)) = "S", True, False))
      ckData4.Checked = CBool(IIf(NTSCStr(strTmp(19)) = "S", True, False))
      ckData5.Checked = CBool(IIf(NTSCStr(strTmp(20)) = "S", True, False))
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(strTmp(21)) <> "" Then edData1da.Text = NTSCStr(strTmp(21))
      If NTSCStr(strTmp(22)) <> "" Then edData2da.Text = NTSCStr(strTmp(22))
      If NTSCStr(strTmp(23)) <> "" Then edData3da.Text = NTSCStr(strTmp(23))
      If NTSCStr(strTmp(24)) <> "" Then edData4da.Text = NTSCStr(strTmp(24))
      If NTSCStr(strTmp(25)) <> "" Then edData5da.Text = NTSCStr(strTmp(25))
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(strTmp(26)) <> "" Then edData1a.Text = NTSCStr(strTmp(26))
      If NTSCStr(strTmp(27)) <> "" Then edData2a.Text = NTSCStr(strTmp(27))
      If NTSCStr(strTmp(28)) <> "" Then edData3a.Text = NTSCStr(strTmp(28))
      If NTSCStr(strTmp(29)) <> "" Then edData4a.Text = NTSCStr(strTmp(29))
      If NTSCStr(strTmp(30)) <> "" Then edData5a.Text = NTSCStr(strTmp(30))
      '--------------------------------------------------------------------------------------------------------------
      ckNum1.Checked = CBool(IIf(NTSCStr(strTmp(31)) = "S", True, False))
      ckNum2.Checked = CBool(IIf(NTSCStr(strTmp(32)) = "S", True, False))
      ckNum3.Checked = CBool(IIf(NTSCStr(strTmp(33)) = "S", True, False))
      ckNum4.Checked = CBool(IIf(NTSCStr(strTmp(34)) = "S", True, False))
      ckNum5.Checked = CBool(IIf(NTSCStr(strTmp(35)) = "S", True, False))
      ckNum6.Checked = CBool(IIf(NTSCStr(strTmp(36)) = "S", True, False))
      ckNum7.Checked = CBool(IIf(NTSCStr(strTmp(37)) = "S", True, False))
      ckNum8.Checked = CBool(IIf(NTSCStr(strTmp(38)) = "S", True, False))
      ckNum9.Checked = CBool(IIf(NTSCStr(strTmp(39)) = "S", True, False))
      ckNum10.Checked = CBool(IIf(NTSCStr(strTmp(40)) = "S", True, False))
      '--------------------------------------------------------------------------------------------------------------
      edNum1da.Text = NTSCStr(strTmp(41))
      edNum2da.Text = NTSCStr(strTmp(42))
      edNum3da.Text = NTSCStr(strTmp(43))
      edNum4da.Text = NTSCStr(strTmp(44))
      edNum5da.Text = NTSCStr(strTmp(45))
      edNum6da.Text = NTSCStr(strTmp(46))
      edNum7da.Text = NTSCStr(strTmp(47))
      edNum8da.Text = NTSCStr(strTmp(48))
      edNum9da.Text = NTSCStr(strTmp(49))
      edNum10da.Text = NTSCStr(strTmp(50))
      '--------------------------------------------------------------------------------------------------------------
      edNum1a.Text = NTSCStr(strTmp(51))
      edNum2a.Text = NTSCStr(strTmp(52))
      edNum3a.Text = NTSCStr(strTmp(53))
      edNum4a.Text = NTSCStr(strTmp(54))
      edNum5a.Text = NTSCStr(strTmp(55))
      edNum6a.Text = NTSCStr(strTmp(56))
      edNum7a.Text = NTSCStr(strTmp(57))
      edNum8a.Text = NTSCStr(strTmp(58))
      edNum9a.Text = NTSCStr(strTmp(59))
      edNum10a.Text = NTSCStr(strTmp(60))
      '--------------------------------------------------------------------------------------------------------------
      tsHlex.SelectedTabPageIndex = 6
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(strTmp(61)) <> "E" Then
        For i = 0 To (liCheck1.ItemCount - 1)
          liCheck1.SetSelected(i, False)
        Next
        Select Case NTSCStr(strTmp(61))
          Case "S" : liCheck1.SetSelected(0, True)
          Case "N" : liCheck1.SetSelected(1, True)
        End Select
      End If
      If NTSCStr(strTmp(62)) <> "E" Then
        For i = 0 To (liCheck2.ItemCount - 1)
          liCheck2.SetSelected(i, False)
        Next
        Select Case NTSCStr(strTmp(62))
          Case "S" : liCheck2.SetSelected(0, True)
          Case "N" : liCheck2.SetSelected(1, True)
        End Select
      End If
      If NTSCStr(strTmp(63)) <> "E" Then
        For i = 0 To (liCheck3.ItemCount - 1)
          liCheck3.SetSelected(i, False)
        Next
        Select Case NTSCStr(strTmp(63))
          Case "S" : liCheck3.SetSelected(0, True)
          Case "N" : liCheck3.SetSelected(1, True)
        End Select
      End If
      If NTSCStr(strTmp(64)) <> "E" Then
        For i = 0 To (liCheck4.ItemCount - 1)
          liCheck4.SetSelected(i, False)
        Next
        Select Case NTSCStr(strTmp(64))
          Case "S" : liCheck4.SetSelected(0, True)
          Case "N" : liCheck4.SetSelected(1, True)
        End Select
      End If
      If NTSCStr(strTmp(65)) <> "E" Then
        For i = 0 To (liCheck5.ItemCount - 1)
          liCheck5.SetSelected(i, False)
        Next
        Select Case NTSCStr(strTmp(65))
          Case "S" : liCheck5.SetSelected(0, True)
          Case "N" : liCheck5.SetSelected(1, True)
        End Select
      End If
      If NTSCStr(strTmp(66)) <> "E" Then
        For i = 0 To (liCheck6.ItemCount - 1)
          liCheck6.SetSelected(i, False)
        Next
        Select Case NTSCStr(strTmp(66))
          Case "S" : liCheck6.SetSelected(0, True)
          Case "N" : liCheck6.SetSelected(1, True)
        End Select
      End If
      If NTSCStr(strTmp(67)) <> "E" Then
        For i = 0 To (liCheck7.ItemCount - 1)
          liCheck7.SetSelected(i, False)
        Next
        Select Case NTSCStr(strTmp(67))
          Case "S" : liCheck7.SetSelected(0, True)
          Case "N" : liCheck7.SetSelected(1, True)
        End Select
      End If
      If NTSCStr(strTmp(68)) <> "E" Then
        For i = 0 To (liCheck8.ItemCount - 1)
          liCheck8.SetSelected(i, False)
        Next
        Select Case NTSCStr(strTmp(68))
          Case "S" : liCheck8.SetSelected(0, True)
          Case "N" : liCheck8.SetSelected(1, True)
        End Select
      End If
      If NTSCStr(strTmp(69)) <> "E" Then
        For i = 0 To (liCheck9.ItemCount - 1)
          liCheck9.SetSelected(i, False)
        Next
        Select Case NTSCStr(strTmp(69))
          Case "S" : liCheck9.SetSelected(0, True)
          Case "N" : liCheck9.SetSelected(1, True)
        End Select
      End If
      If NTSCStr(strTmp(70)) <> "E" Then
        For i = 0 To (liCheck10.ItemCount - 1)
          liCheck10.SetSelected(i, False)
        Next
        Select Case NTSCStr(strTmp(70))
          Case "S" : liCheck10.SetSelected(0, True)
          Case "N" : liCheck10.SetSelected(1, True)
        End Select
      End If
      '--------------------------------------------------------------------------------------------------------------
      For i = 0 To (liList1.ItemCount - 1)
        liList1.SetSelected(i, False)
      Next
      For i = 71 To 80
        If NTSCStr(strTmp(i)) = "S" Then
          liList1.SetSelected(i - 71, True)
        End If
      Next
      For i = 81 To 85
        If NTSCStr(strTmp(i)) = "S" Then
          liList2.SetSelected(i - 81, True)
        End If
      Next
      For i = 86 To 105
        If NTSCStr(strTmp(i)) = "S" Then
          liList3.SetSelected(i - 86, True)
        End If
      Next
      '--------------------------------------------------------------------------------------------------------------
      tsHlex.SelectedTabPageIndex = 0
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

#Region "BloccoSbloccoDateNumeri"
  Public Overridable Sub ckData1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckData1.CheckedChanged
    If ckData1.Checked Then
      GctlSetVisEnab(edData1da, False)
      GctlSetVisEnab(edData1a, False)
    Else
      edData1da.Enabled = False
      edData1a.Enabled = False
    End If
  End Sub

  Public Overridable Sub ckData2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckData2.CheckedChanged
    If ckData2.Checked Then
      GctlSetVisEnab(edData2da, False)
      GctlSetVisEnab(edData2a, False)
    Else
      edData2da.Enabled = False
      edData2a.Enabled = False
    End If
  End Sub

  Public Overridable Sub ckData3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckData3.CheckedChanged
    If ckData3.Checked Then
      GctlSetVisEnab(edData3da, False)
      GctlSetVisEnab(edData3a, False)
    Else
      edData3da.Enabled = False
      edData3a.Enabled = False
    End If
  End Sub

  Public Overridable Sub ckData4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckData4.CheckedChanged
    If ckData4.Checked Then
      GctlSetVisEnab(edData4da, False)
      GctlSetVisEnab(edData4a, False)
    Else
      edData4da.Enabled = False
      edData4a.Enabled = False
    End If
  End Sub

  Public Overridable Sub ckData5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckData5.CheckedChanged
    If ckData5.Checked Then
      GctlSetVisEnab(edData5da, False)
      GctlSetVisEnab(edData5a, False)
    Else
      edData5da.Enabled = False
      edData5a.Enabled = False
    End If
  End Sub

  Public Overridable Sub ckNum1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckNum1.CheckedChanged
    If ckNum1.Checked Then
      GctlSetVisEnab(edNum1da, False)
      GctlSetVisEnab(edNum1a, False)
    Else
      edNum1da.Enabled = False
      edNum1a.Enabled = False
    End If
  End Sub

  Public Overridable Sub ckNum2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckNum2.CheckedChanged
    If ckNum2.Checked Then
      GctlSetVisEnab(edNum2da, False)
      GctlSetVisEnab(edNum2a, False)
    Else
      edNum2da.Enabled = False
      edNum2a.Enabled = False
    End If
  End Sub

  Public Overridable Sub ckNum3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckNum3.CheckedChanged
    If ckNum3.Checked Then
      GctlSetVisEnab(edNum3da, False)
      GctlSetVisEnab(edNum3a, False)
    Else
      edNum3da.Enabled = False
      edNum3a.Enabled = False
    End If
  End Sub

  Public Overridable Sub ckNum4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckNum4.CheckedChanged
    If ckNum4.Checked Then
      GctlSetVisEnab(edNum4da, False)
      GctlSetVisEnab(edNum4a, False)
    Else
      edNum4da.Enabled = False
      edNum4a.Enabled = False
    End If
  End Sub

  Public Overridable Sub ckNum5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckNum5.CheckedChanged
    If ckNum5.Checked Then
      GctlSetVisEnab(edNum5da, False)
      GctlSetVisEnab(edNum5a, False)
    Else
      edNum5da.Enabled = False
      edNum5a.Enabled = False
    End If
  End Sub

  Public Overridable Sub ckNum6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckNum6.CheckedChanged
    If ckNum6.Checked Then
      GctlSetVisEnab(edNum6da, False)
      GctlSetVisEnab(edNum6a, False)
    Else
      edNum6da.Enabled = False
      edNum6a.Enabled = False
    End If
  End Sub

  Public Overridable Sub ckNum7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckNum7.CheckedChanged
    If ckNum7.Checked Then
      GctlSetVisEnab(edNum7da, False)
      GctlSetVisEnab(edNum7a, False)
    Else
      edNum7da.Enabled = False
      edNum7a.Enabled = False
    End If
  End Sub

  Public Overridable Sub ckNum8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckNum8.CheckedChanged
    If ckNum8.Checked Then
      GctlSetVisEnab(edNum8da, False)
      GctlSetVisEnab(edNum8a, False)
    Else
      edNum8da.Enabled = False
      edNum8a.Enabled = False
    End If
  End Sub

  Public Overridable Sub ckNum9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckNum9.CheckedChanged
    If ckNum9.Checked Then
      GctlSetVisEnab(edNum9da, False)
      GctlSetVisEnab(edNum9a, False)
    Else
      edNum9da.Enabled = False
      edNum9a.Enabled = False
    End If
  End Sub

  Public Overridable Sub ckNum10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckNum10.CheckedChanged
    If ckNum10.Checked Then
      GctlSetVisEnab(edNum10da, False)
      GctlSetVisEnab(edNum10a, False)
    Else
      edNum10da.Enabled = False
      edNum10a.Enabled = False
    End If
  End Sub
#End Region

End Class
