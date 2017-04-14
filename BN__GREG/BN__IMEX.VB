Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__IMEX

#Region "Variabili"
  Public oCleGreg As CLE__GREG
  Public oCallParams As CLE__CLDP
  Private components As System.ComponentModel.IContainer

  Public WithEvents pnImex As NTSInformatica.NTSPanel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOK As NTSInformatica.NTSButton
  Public WithEvents lbPath As NTSInformatica.NTSLabel
  Public WithEvents lbNomPath As NTSInformatica.NTSLabel
  Public WithEvents fmLivello As NTSInformatica.NTSGroupBox
  Public WithEvents opExport1 As NTSInformatica.NTSRadioButton
  Public WithEvents opExport0 As NTSInformatica.NTSRadioButton
  Public WithEvents fmCosa As NTSInformatica.NTSGroupBox
  Public WithEvents ckImEx6 As NTSInformatica.NTSCheckBox
  Public WithEvents ckImEx4 As NTSInformatica.NTSCheckBox
  Public WithEvents ckImEx3 As NTSInformatica.NTSCheckBox
  Public WithEvents ckImEx2 As NTSInformatica.NTSCheckBox
  Public WithEvents ckImEx7 As NTSInformatica.NTSCheckBox
  Public WithEvents ckImEx8 As NTSInformatica.NTSCheckBox
  Public WithEvents ckImEx9 As NTSInformatica.NTSCheckBox
  Public WithEvents ckImEx5 As NTSInformatica.NTSCheckBox
  Public WithEvents ckImEx1 As NTSInformatica.NTSCheckBox
  Public WithEvents ckImEx0 As NTSInformatica.NTSCheckBox
  Public WithEvents fmExport As NTSInformatica.NTSGroupBox
  Public WithEvents ckDipAzienda1 As NTSInformatica.NTSCheckBox
  Public WithEvents edAziendaExp As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAziendaExport As NTSInformatica.NTSLabel
  Public WithEvents ckDipAzienda0 As NTSInformatica.NTSCheckBox
  Public WithEvents ckDipOperatore1 As NTSInformatica.NTSCheckBox
  Public WithEvents edUtenteExp As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbUtenteExport As NTSInformatica.NTSLabel
  Public WithEvents ckDipOperatore0 As NTSInformatica.NTSCheckBox
  Public WithEvents edNomeFile As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbNome As NTSInformatica.NTSLabel
  Public WithEvents fmImport As NTSInformatica.NTSGroupBox
  Public WithEvents ckHow As NTSInformatica.NTSCheckBox
  Public WithEvents edUtenteImp As NTSInformatica.NTSTextBoxStr
  Public WithEvents opFiltroImp1 As NTSInformatica.NTSRadioButton
  Public WithEvents opFiltroImp0 As NTSInformatica.NTSRadioButton
  Public WithEvents OpenFileDialog1 As NTSOpenFileDialog
  Public WithEvents SaveFileDialog1 As NTSSaveFileDialog
#End Region

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
  Public Overridable Sub InitializeComponent()
    Me.pnImex = New NTSInformatica.NTSPanel
    Me.fmImport = New NTSInformatica.NTSGroupBox
    Me.ckHow = New NTSInformatica.NTSCheckBox
    Me.edUtenteImp = New NTSInformatica.NTSTextBoxStr
    Me.opFiltroImp1 = New NTSInformatica.NTSRadioButton
    Me.opFiltroImp0 = New NTSInformatica.NTSRadioButton
    Me.fmLivello = New NTSInformatica.NTSGroupBox
    Me.opExport1 = New NTSInformatica.NTSRadioButton
    Me.opExport0 = New NTSInformatica.NTSRadioButton
    Me.fmCosa = New NTSInformatica.NTSGroupBox
    Me.ckImEx6 = New NTSInformatica.NTSCheckBox
    Me.ckImEx4 = New NTSInformatica.NTSCheckBox
    Me.ckImEx3 = New NTSInformatica.NTSCheckBox
    Me.ckImEx2 = New NTSInformatica.NTSCheckBox
    Me.ckImEx7 = New NTSInformatica.NTSCheckBox
    Me.ckImEx8 = New NTSInformatica.NTSCheckBox
    Me.ckImEx9 = New NTSInformatica.NTSCheckBox
    Me.ckImEx5 = New NTSInformatica.NTSCheckBox
    Me.ckImEx1 = New NTSInformatica.NTSCheckBox
    Me.ckImEx0 = New NTSInformatica.NTSCheckBox
    Me.fmExport = New NTSInformatica.NTSGroupBox
    Me.ckDipAzienda1 = New NTSInformatica.NTSCheckBox
    Me.edAziendaExp = New NTSInformatica.NTSTextBoxStr
    Me.lbAziendaExport = New NTSInformatica.NTSLabel
    Me.ckDipAzienda0 = New NTSInformatica.NTSCheckBox
    Me.ckDipOperatore1 = New NTSInformatica.NTSCheckBox
    Me.edUtenteExp = New NTSInformatica.NTSTextBoxStr
    Me.lbUtenteExport = New NTSInformatica.NTSLabel
    Me.ckDipOperatore0 = New NTSInformatica.NTSCheckBox
    Me.edNomeFile = New NTSInformatica.NTSTextBoxStr
    Me.lbNome = New NTSInformatica.NTSLabel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOK = New NTSInformatica.NTSButton
    Me.lbPath = New NTSInformatica.NTSLabel
    Me.lbNomPath = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnImex, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnImex.SuspendLayout()
    CType(Me.fmImport, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmImport.SuspendLayout()
    CType(Me.ckHow.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edUtenteImp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opFiltroImp1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opFiltroImp0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmLivello, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmLivello.SuspendLayout()
    CType(Me.opExport1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opExport0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmCosa, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmCosa.SuspendLayout()
    CType(Me.ckImEx6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckImEx4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckImEx3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckImEx2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckImEx7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckImEx8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckImEx9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckImEx5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckImEx1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckImEx0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmExport, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmExport.SuspendLayout()
    CType(Me.ckDipAzienda1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAziendaExp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDipAzienda0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDipOperatore1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edUtenteExp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDipOperatore0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNomeFile.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(231, Byte), Integer))
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
    'pnImex
    '
    Me.pnImex.AllowDrop = True
    Me.pnImex.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnImex.Appearance.Options.UseBackColor = True
    Me.pnImex.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnImex.Controls.Add(Me.fmImport)
    Me.pnImex.Controls.Add(Me.fmLivello)
    Me.pnImex.Controls.Add(Me.fmCosa)
    Me.pnImex.Controls.Add(Me.fmExport)
    Me.pnImex.Controls.Add(Me.edNomeFile)
    Me.pnImex.Controls.Add(Me.lbNome)
    Me.pnImex.Controls.Add(Me.cmdAnnulla)
    Me.pnImex.Controls.Add(Me.cmdOK)
    Me.pnImex.Controls.Add(Me.lbPath)
    Me.pnImex.Controls.Add(Me.lbNomPath)
    Me.pnImex.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnImex.Location = New System.Drawing.Point(-1, -2)
    Me.pnImex.Name = "pnImex"
    Me.pnImex.NTSActiveTrasparency = True
    Me.pnImex.Size = New System.Drawing.Size(538, 311)
    Me.pnImex.TabIndex = 0
    Me.pnImex.Text = "NtsPanel1"
    '
    'fmImport
    '
    Me.fmImport.AllowDrop = True
    Me.fmImport.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmImport.Appearance.Options.UseBackColor = True
    Me.fmImport.Controls.Add(Me.ckHow)
    Me.fmImport.Controls.Add(Me.edUtenteImp)
    Me.fmImport.Controls.Add(Me.opFiltroImp1)
    Me.fmImport.Controls.Add(Me.opFiltroImp0)
    Me.fmImport.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmImport.Location = New System.Drawing.Point(16, 80)
    Me.fmImport.Name = "fmImport"
    Me.fmImport.Size = New System.Drawing.Size(243, 128)
    Me.fmImport.TabIndex = 9
    Me.fmImport.Text = "Tipo Importazione"
    Me.fmImport.Visible = False
    '
    'ckHow
    '
    Me.ckHow.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckHow.Location = New System.Drawing.Point(18, 102)
    Me.ckHow.Name = "ckHow"
    Me.ckHow.NTSCheckValue = "S"
    Me.ckHow.NTSUnCheckValue = "N"
    Me.ckHow.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckHow.Properties.Appearance.Options.UseBackColor = True
    Me.ckHow.Properties.AutoHeight = False
    Me.ckHow.Properties.Caption = "Sovrascrivi proprietà già esistenti"
    Me.ckHow.Size = New System.Drawing.Size(207, 19)
    Me.ckHow.TabIndex = 3
    '
    'edUtenteImp
    '
    Me.edUtenteImp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edUtenteImp.EditValue = ""
    Me.edUtenteImp.Location = New System.Drawing.Point(18, 70)
    Me.edUtenteImp.Name = "edUtenteImp"
    Me.edUtenteImp.NTSDbField = ""
    Me.edUtenteImp.NTSForzaVisZoom = False
    Me.edUtenteImp.NTSOldValue = ""
    Me.edUtenteImp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUtenteImp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUtenteImp.Properties.AutoHeight = False
    Me.edUtenteImp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUtenteImp.Properties.MaxLength = 65536
    Me.edUtenteImp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUtenteImp.Size = New System.Drawing.Size(183, 20)
    Me.edUtenteImp.TabIndex = 2
    Me.edUtenteImp.TextStr = ""
    '
    'opFiltroImp1
    '
    Me.opFiltroImp1.Cursor = System.Windows.Forms.Cursors.Default
    Me.opFiltroImp1.Location = New System.Drawing.Point(18, 45)
    Me.opFiltroImp1.Name = "opFiltroImp1"
    Me.opFiltroImp1.NTSCheckValue = "S"
    Me.opFiltroImp1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opFiltroImp1.Properties.Appearance.Options.UseBackColor = True
    Me.opFiltroImp1.Properties.AutoHeight = False
    Me.opFiltroImp1.Properties.Caption = "Importa sostituendo nome utente:"
    Me.opFiltroImp1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opFiltroImp1.Size = New System.Drawing.Size(207, 19)
    Me.opFiltroImp1.TabIndex = 1
    '
    'opFiltroImp0
    '
    Me.opFiltroImp0.Cursor = System.Windows.Forms.Cursors.Default
    Me.opFiltroImp0.EditValue = True
    Me.opFiltroImp0.Location = New System.Drawing.Point(18, 25)
    Me.opFiltroImp0.Name = "opFiltroImp0"
    Me.opFiltroImp0.NTSCheckValue = "S"
    Me.opFiltroImp0.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opFiltroImp0.Properties.Appearance.Options.UseBackColor = True
    Me.opFiltroImp0.Properties.AutoHeight = False
    Me.opFiltroImp0.Properties.Caption = "Importa con nome utente da file"
    Me.opFiltroImp0.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opFiltroImp0.Size = New System.Drawing.Size(207, 19)
    Me.opFiltroImp0.TabIndex = 0
    '
    'fmLivello
    '
    Me.fmLivello.AllowDrop = True
    Me.fmLivello.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmLivello.Appearance.Options.UseBackColor = True
    Me.fmLivello.Controls.Add(Me.opExport1)
    Me.fmLivello.Controls.Add(Me.opExport0)
    Me.fmLivello.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmLivello.Location = New System.Drawing.Point(280, 236)
    Me.fmLivello.Name = "fmLivello"
    Me.fmLivello.Size = New System.Drawing.Size(240, 64)
    Me.fmLivello.TabIndex = 8
    Me.fmLivello.Text = "Livello"
    '
    'opExport1
    '
    Me.opExport1.Cursor = System.Windows.Forms.Cursors.Default
    Me.opExport1.EditValue = True
    Me.opExport1.Location = New System.Drawing.Point(17, 40)
    Me.opExport1.Name = "opExport1"
    Me.opExport1.NTSCheckValue = "S"
    Me.opExport1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opExport1.Properties.Appearance.Options.UseBackColor = True
    Me.opExport1.Properties.AutoHeight = False
    Me.opExport1.Properties.Caption = "Esporta anche sottocartelle"
    Me.opExport1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opExport1.Size = New System.Drawing.Size(164, 19)
    Me.opExport1.TabIndex = 1
    '
    'opExport0
    '
    Me.opExport0.Cursor = System.Windows.Forms.Cursors.Default
    Me.opExport0.Location = New System.Drawing.Point(17, 23)
    Me.opExport0.Name = "opExport0"
    Me.opExport0.NTSCheckValue = "S"
    Me.opExport0.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opExport0.Properties.Appearance.Options.UseBackColor = True
    Me.opExport0.Properties.AutoHeight = False
    Me.opExport0.Properties.Caption = "Esporta ramo singolo"
    Me.opExport0.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opExport0.Size = New System.Drawing.Size(164, 19)
    Me.opExport0.TabIndex = 0
    '
    'fmCosa
    '
    Me.fmCosa.AllowDrop = True
    Me.fmCosa.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmCosa.Appearance.Options.UseBackColor = True
    Me.fmCosa.Controls.Add(Me.ckImEx6)
    Me.fmCosa.Controls.Add(Me.ckImEx4)
    Me.fmCosa.Controls.Add(Me.ckImEx3)
    Me.fmCosa.Controls.Add(Me.ckImEx2)
    Me.fmCosa.Controls.Add(Me.ckImEx7)
    Me.fmCosa.Controls.Add(Me.ckImEx8)
    Me.fmCosa.Controls.Add(Me.ckImEx9)
    Me.fmCosa.Controls.Add(Me.ckImEx5)
    Me.fmCosa.Controls.Add(Me.ckImEx1)
    Me.fmCosa.Controls.Add(Me.ckImEx0)
    Me.fmCosa.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmCosa.Location = New System.Drawing.Point(280, 80)
    Me.fmCosa.Name = "fmCosa"
    Me.fmCosa.Size = New System.Drawing.Size(240, 150)
    Me.fmCosa.TabIndex = 7
    Me.fmCosa.Text = "Importa/Esporta"
    '
    'ckImEx6
    '
    Me.ckImEx6.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckImEx6.Location = New System.Drawing.Point(202, 124)
    Me.ckImEx6.Name = "ckImEx6"
    Me.ckImEx6.NTSCheckValue = "S"
    Me.ckImEx6.NTSUnCheckValue = "N"
    Me.ckImEx6.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckImEx6.Properties.Appearance.Options.UseBackColor = True
    Me.ckImEx6.Properties.AutoHeight = False
    Me.ckImEx6.Properties.Caption = "&Sicurezza"
    Me.ckImEx6.Size = New System.Drawing.Size(75, 19)
    Me.ckImEx6.TabIndex = 9
    Me.ckImEx6.Visible = False
    '
    'ckImEx4
    '
    Me.ckImEx4.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckImEx4.Location = New System.Drawing.Point(202, 124)
    Me.ckImEx4.Name = "ckImEx4"
    Me.ckImEx4.NTSCheckValue = "S"
    Me.ckImEx4.NTSUnCheckValue = "N"
    Me.ckImEx4.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckImEx4.Properties.Appearance.Options.UseBackColor = True
    Me.ckImEx4.Properties.AutoHeight = False
    Me.ckImEx4.Properties.Caption = "&DCOM"
    Me.ckImEx4.Size = New System.Drawing.Size(75, 19)
    Me.ckImEx4.TabIndex = 8
    Me.ckImEx4.Visible = False
    '
    'ckImEx3
    '
    Me.ckImEx3.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckImEx3.Location = New System.Drawing.Point(17, 45)
    Me.ckImEx3.Name = "ckImEx3"
    Me.ckImEx3.NTSCheckValue = "S"
    Me.ckImEx3.NTSUnCheckValue = "N"
    Me.ckImEx3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckImEx3.Properties.Appearance.Options.UseBackColor = True
    Me.ckImEx3.Properties.AutoHeight = False
    Me.ckImEx3.Properties.Caption = "&Recent"
    Me.ckImEx3.Size = New System.Drawing.Size(75, 19)
    Me.ckImEx3.TabIndex = 7
    '
    'ckImEx2
    '
    Me.ckImEx2.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckImEx2.Location = New System.Drawing.Point(17, 25)
    Me.ckImEx2.Name = "ckImEx2"
    Me.ckImEx2.NTSCheckValue = "S"
    Me.ckImEx2.NTSUnCheckValue = "N"
    Me.ckImEx2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckImEx2.Properties.Appearance.Options.UseBackColor = True
    Me.ckImEx2.Properties.AutoHeight = False
    Me.ckImEx2.Properties.Caption = "&Opzioni"
    Me.ckImEx2.Size = New System.Drawing.Size(75, 19)
    Me.ckImEx2.TabIndex = 6
    '
    'ckImEx7
    '
    Me.ckImEx7.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckImEx7.Location = New System.Drawing.Point(202, 124)
    Me.ckImEx7.Name = "ckImEx7"
    Me.ckImEx7.NTSCheckValue = "S"
    Me.ckImEx7.NTSUnCheckValue = "N"
    Me.ckImEx7.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckImEx7.Properties.Appearance.Options.UseBackColor = True
    Me.ckImEx7.Properties.AutoHeight = False
    Me.ckImEx7.Properties.Caption = "Controlli Aggiuntivi (NEWCTRL)"
    Me.ckImEx7.Size = New System.Drawing.Size(178, 19)
    Me.ckImEx7.TabIndex = 5
    Me.ckImEx7.Visible = False
    '
    'ckImEx8
    '
    Me.ckImEx8.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckImEx8.Location = New System.Drawing.Point(202, 124)
    Me.ckImEx8.Name = "ckImEx8"
    Me.ckImEx8.NTSCheckValue = "S"
    Me.ckImEx8.NTSUnCheckValue = "N"
    Me.ckImEx8.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckImEx8.Properties.Appearance.Options.UseBackColor = True
    Me.ckImEx8.Properties.AutoHeight = False
    Me.ckImEx8.Properties.Caption = "Gest. Eventi (ALE)"
    Me.ckImEx8.Size = New System.Drawing.Size(116, 19)
    Me.ckImEx8.TabIndex = 4
    Me.ckImEx8.Visible = False
    '
    'ckImEx9
    '
    Me.ckImEx9.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckImEx9.Location = New System.Drawing.Point(202, 124)
    Me.ckImEx9.Name = "ckImEx9"
    Me.ckImEx9.NTSCheckValue = "S"
    Me.ckImEx9.NTSUnCheckValue = "N"
    Me.ckImEx9.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckImEx9.Properties.Appearance.Options.UseBackColor = True
    Me.ckImEx9.Properties.AutoHeight = False
    Me.ckImEx9.Properties.Caption = "Pers. Globali (ALP)"
    Me.ckImEx9.Size = New System.Drawing.Size(116, 19)
    Me.ckImEx9.TabIndex = 3
    Me.ckImEx9.Visible = False
    '
    'ckImEx5
    '
    Me.ckImEx5.Cursor = System.Windows.Forms.Cursors.Hand
    Me.ckImEx5.Location = New System.Drawing.Point(17, 65)
    Me.ckImEx5.Name = "ckImEx5"
    Me.ckImEx5.NTSCheckValue = "S"
    Me.ckImEx5.NTSUnCheckValue = "N"
    Me.ckImEx5.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckImEx5.Properties.Appearance.Options.UseBackColor = True
    Me.ckImEx5.Properties.AutoHeight = False
    Me.ckImEx5.Properties.Caption = "Re&port"
    Me.ckImEx5.Size = New System.Drawing.Size(116, 19)
    Me.ckImEx5.TabIndex = 2
    '
    'ckImEx1
    '
    Me.ckImEx1.Cursor = System.Windows.Forms.Cursors.Hand
    Me.ckImEx1.Location = New System.Drawing.Point(199, 124)
    Me.ckImEx1.Name = "ckImEx1"
    Me.ckImEx1.NTSCheckValue = "S"
    Me.ckImEx1.NTSUnCheckValue = "N"
    Me.ckImEx1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckImEx1.Properties.Appearance.Options.UseBackColor = True
    Me.ckImEx1.Properties.AutoHeight = False
    Me.ckImEx1.Properties.Caption = "Layout di &Form"
    Me.ckImEx1.Size = New System.Drawing.Size(119, 19)
    Me.ckImEx1.TabIndex = 1
    Me.ckImEx1.Visible = False
    '
    'ckImEx0
    '
    Me.ckImEx0.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckImEx0.Location = New System.Drawing.Point(202, 124)
    Me.ckImEx0.Name = "ckImEx0"
    Me.ckImEx0.NTSCheckValue = "S"
    Me.ckImEx0.NTSUnCheckValue = "N"
    Me.ckImEx0.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckImEx0.Properties.Appearance.Options.UseBackColor = True
    Me.ckImEx0.Properties.AutoHeight = False
    Me.ckImEx0.Properties.Caption = "Layout di &Griglia"
    Me.ckImEx0.Size = New System.Drawing.Size(116, 19)
    Me.ckImEx0.TabIndex = 0
    Me.ckImEx0.Visible = False
    '
    'fmExport
    '
    Me.fmExport.AllowDrop = True
    Me.fmExport.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmExport.Appearance.Options.UseBackColor = True
    Me.fmExport.Controls.Add(Me.ckDipAzienda1)
    Me.fmExport.Controls.Add(Me.edAziendaExp)
    Me.fmExport.Controls.Add(Me.lbAziendaExport)
    Me.fmExport.Controls.Add(Me.ckDipAzienda0)
    Me.fmExport.Controls.Add(Me.ckDipOperatore1)
    Me.fmExport.Controls.Add(Me.edUtenteExp)
    Me.fmExport.Controls.Add(Me.lbUtenteExport)
    Me.fmExport.Controls.Add(Me.ckDipOperatore0)
    Me.fmExport.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmExport.Location = New System.Drawing.Point(16, 80)
    Me.fmExport.Name = "fmExport"
    Me.fmExport.Size = New System.Drawing.Size(243, 220)
    Me.fmExport.TabIndex = 6
    Me.fmExport.Text = "Tipo esportazione"
    Me.fmExport.Visible = False
    '
    'ckDipAzienda1
    '
    Me.ckDipAzienda1.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDipAzienda1.Location = New System.Drawing.Point(18, 192)
    Me.ckDipAzienda1.Name = "ckDipAzienda1"
    Me.ckDipAzienda1.NTSCheckValue = "S"
    Me.ckDipAzienda1.NTSUnCheckValue = "N"
    Me.ckDipAzienda1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDipAzienda1.Properties.Appearance.Options.UseBackColor = True
    Me.ckDipAzienda1.Properties.AutoHeight = False
    Me.ckDipAzienda1.Properties.Caption = "N&on dipendente da azienda"
    Me.ckDipAzienda1.Size = New System.Drawing.Size(157, 19)
    Me.ckDipAzienda1.TabIndex = 7
    '
    'edAziendaExp
    '
    Me.edAziendaExp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAziendaExp.EditValue = ""
    Me.edAziendaExp.Enabled = False
    Me.edAziendaExp.Location = New System.Drawing.Point(18, 166)
    Me.edAziendaExp.Name = "edAziendaExp"
    Me.edAziendaExp.NTSDbField = ""
    Me.edAziendaExp.NTSForzaVisZoom = False
    Me.edAziendaExp.NTSOldValue = ""
    Me.edAziendaExp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAziendaExp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAziendaExp.Properties.AutoHeight = False
    Me.edAziendaExp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAziendaExp.Properties.MaxLength = 65536
    Me.edAziendaExp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAziendaExp.Size = New System.Drawing.Size(201, 20)
    Me.edAziendaExp.TabIndex = 6
    Me.edAziendaExp.TextStr = ""
    '
    'lbAziendaExport
    '
    Me.lbAziendaExport.AutoSize = True
    Me.lbAziendaExport.BackColor = System.Drawing.Color.Transparent
    Me.lbAziendaExport.Location = New System.Drawing.Point(15, 150)
    Me.lbAziendaExport.Name = "lbAziendaExport"
    Me.lbAziendaExport.NTSDbField = ""
    Me.lbAziendaExport.Size = New System.Drawing.Size(99, 13)
    Me.lbAziendaExport.TabIndex = 5
    Me.lbAziendaExport.Text = "Considera azienda:"
    Me.lbAziendaExport.Tooltip = ""
    Me.lbAziendaExport.UseMnemonic = False
    '
    'ckDipAzienda0
    '
    Me.ckDipAzienda0.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDipAzienda0.Location = New System.Drawing.Point(18, 127)
    Me.ckDipAzienda0.Name = "ckDipAzienda0"
    Me.ckDipAzienda0.NTSCheckValue = "S"
    Me.ckDipAzienda0.NTSUnCheckValue = "N"
    Me.ckDipAzienda0.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDipAzienda0.Properties.Appearance.Options.UseBackColor = True
    Me.ckDipAzienda0.Properties.AutoHeight = False
    Me.ckDipAzienda0.Properties.Caption = "Di&pendente da azienda"
    Me.ckDipAzienda0.Size = New System.Drawing.Size(138, 19)
    Me.ckDipAzienda0.TabIndex = 4
    '
    'ckDipOperatore1
    '
    Me.ckDipOperatore1.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDipOperatore1.Location = New System.Drawing.Point(18, 92)
    Me.ckDipOperatore1.Name = "ckDipOperatore1"
    Me.ckDipOperatore1.NTSCheckValue = "S"
    Me.ckDipOperatore1.NTSUnCheckValue = "N"
    Me.ckDipOperatore1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDipOperatore1.Properties.Appearance.Options.UseBackColor = True
    Me.ckDipOperatore1.Properties.AutoHeight = False
    Me.ckDipOperatore1.Properties.Caption = "&Non dipendente da operatore"
    Me.ckDipOperatore1.Size = New System.Drawing.Size(168, 19)
    Me.ckDipOperatore1.TabIndex = 3
    '
    'edUtenteExp
    '
    Me.edUtenteExp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edUtenteExp.EditValue = ""
    Me.edUtenteExp.Enabled = False
    Me.edUtenteExp.Location = New System.Drawing.Point(18, 66)
    Me.edUtenteExp.Name = "edUtenteExp"
    Me.edUtenteExp.NTSDbField = ""
    Me.edUtenteExp.NTSForzaVisZoom = False
    Me.edUtenteExp.NTSOldValue = ""
    Me.edUtenteExp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUtenteExp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUtenteExp.Properties.AutoHeight = False
    Me.edUtenteExp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUtenteExp.Properties.MaxLength = 65536
    Me.edUtenteExp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUtenteExp.Size = New System.Drawing.Size(201, 20)
    Me.edUtenteExp.TabIndex = 2
    Me.edUtenteExp.TextStr = ""
    '
    'lbUtenteExport
    '
    Me.lbUtenteExport.AutoSize = True
    Me.lbUtenteExport.BackColor = System.Drawing.Color.Transparent
    Me.lbUtenteExport.Location = New System.Drawing.Point(15, 51)
    Me.lbUtenteExport.Name = "lbUtenteExport"
    Me.lbUtenteExport.NTSDbField = ""
    Me.lbUtenteExport.Size = New System.Drawing.Size(123, 13)
    Me.lbUtenteExport.TabIndex = 1
    Me.lbUtenteExport.Text = "Considera nome utente:"
    Me.lbUtenteExport.Tooltip = ""
    Me.lbUtenteExport.UseMnemonic = False
    '
    'ckDipOperatore0
    '
    Me.ckDipOperatore0.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDipOperatore0.Location = New System.Drawing.Point(18, 25)
    Me.ckDipOperatore0.Name = "ckDipOperatore0"
    Me.ckDipOperatore0.NTSCheckValue = "S"
    Me.ckDipOperatore0.NTSUnCheckValue = "N"
    Me.ckDipOperatore0.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDipOperatore0.Properties.Appearance.Options.UseBackColor = True
    Me.ckDipOperatore0.Properties.AutoHeight = False
    Me.ckDipOperatore0.Properties.Caption = "D&ipendente da operatore"
    Me.ckDipOperatore0.Size = New System.Drawing.Size(145, 19)
    Me.ckDipOperatore0.TabIndex = 0
    '
    'edNomeFile
    '
    Me.edNomeFile.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNomeFile.EditValue = ""
    Me.edNomeFile.Location = New System.Drawing.Point(81, 47)
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
    Me.edNomeFile.Size = New System.Drawing.Size(351, 20)
    Me.edNomeFile.TabIndex = 5
    Me.edNomeFile.TextStr = ""
    '
    'lbNome
    '
    Me.lbNome.AutoSize = True
    Me.lbNome.BackColor = System.Drawing.Color.Transparent
    Me.lbNome.Location = New System.Drawing.Point(13, 50)
    Me.lbNome.Name = "lbNome"
    Me.lbNome.NTSDbField = ""
    Me.lbNome.Size = New System.Drawing.Size(55, 13)
    Me.lbNome.TabIndex = 4
    Me.lbNome.Text = "Nome file:"
    Me.lbNome.Tooltip = ""
    Me.lbNome.UseMnemonic = False
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(440, 47)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(80, 27)
    Me.cmdAnnulla.TabIndex = 3
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOK
    '
    Me.cmdOK.ImageText = ""
    Me.cmdOK.Location = New System.Drawing.Point(440, 14)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.NTSContextMenu = Nothing
    Me.cmdOK.Size = New System.Drawing.Size(80, 27)
    Me.cmdOK.TabIndex = 2
    Me.cmdOK.Text = "&OK"
    '
    'lbPath
    '
    Me.lbPath.BackColor = System.Drawing.Color.Transparent
    Me.lbPath.Location = New System.Drawing.Point(47, 14)
    Me.lbPath.Name = "lbPath"
    Me.lbPath.NTSDbField = ""
    Me.lbPath.Size = New System.Drawing.Size(385, 13)
    Me.lbPath.TabIndex = 1
    Me.lbPath.Tooltip = ""
    Me.lbPath.UseMnemonic = False
    '
    'lbNomPath
    '
    Me.lbNomPath.AutoSize = True
    Me.lbNomPath.BackColor = System.Drawing.Color.Transparent
    Me.lbNomPath.Location = New System.Drawing.Point(13, 14)
    Me.lbNomPath.Name = "lbNomPath"
    Me.lbNomPath.NTSDbField = ""
    Me.lbNomPath.Size = New System.Drawing.Size(33, 13)
    Me.lbNomPath.TabIndex = 0
    Me.lbNomPath.Text = "Path:"
    Me.lbNomPath.Tooltip = ""
    Me.lbNomPath.UseMnemonic = False
    '
    'FRM__IMEX
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(537, 308)
    Me.Controls.Add(Me.pnImex)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.Name = "FRM__IMEX"
    Me.Text = ""
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnImex, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnImex.ResumeLayout(False)
    Me.pnImex.PerformLayout()
    CType(Me.fmImport, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmImport.ResumeLayout(False)
    Me.fmImport.PerformLayout()
    CType(Me.ckHow.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edUtenteImp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opFiltroImp1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opFiltroImp0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmLivello, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmLivello.ResumeLayout(False)
    Me.fmLivello.PerformLayout()
    CType(Me.opExport1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opExport0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmCosa, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmCosa.ResumeLayout(False)
    Me.fmCosa.PerformLayout()
    CType(Me.ckImEx6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckImEx4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckImEx3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckImEx2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckImEx7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckImEx8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckImEx9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckImEx5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckImEx1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckImEx0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmExport, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmExport.ResumeLayout(False)
    Me.fmExport.PerformLayout()
    CType(Me.ckDipAzienda1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAziendaExp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDipAzienda0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDipOperatore1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edUtenteExp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDipOperatore0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNomeFile.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      ckHow.NTSSetParam(oMenu, oApp.Tr(Me, 128333967841698746, "Sovr. cartelle e proprietà già esistenti"), "S", "N")
      edUtenteImp.NTSSetParam(oMenu, oApp.Tr(Me, 128333967841854606, "Utente import"), 40)
      opFiltroImp1.NTSSetParam(oMenu, oApp.Tr(Me, 128333967842010466, "Importa sostituendo nome utente:"), "S")
      opFiltroImp0.NTSSetParam(oMenu, oApp.Tr(Me, 129024310725361548, "Importa con nome utente da file"), "S")
      opExport1.NTSSetParam(oMenu, oApp.Tr(Me, 129024310773331526, "Esporta anche sottocartelle"), "S")
      opExport0.NTSSetParam(oMenu, oApp.Tr(Me, 129024310833958078, "Esporta ramo singolo"), "S")
      ckImEx6.NTSSetParam(oMenu, oApp.Tr(Me, 129024310871927800, "&Sicurezza"), "S", "N")
      ckImEx4.NTSSetParam(oMenu, oApp.Tr(Me, 128333967842478046, "&DCOM"), "S", "N")
      ckImEx3.NTSSetParam(oMenu, oApp.Tr(Me, 128333967842633906, "&Recent"), "S", "N")
      ckImEx2.NTSSetParam(oMenu, oApp.Tr(Me, 128333967842789766, "&Opzioni"), "S", "N")
      ckImEx7.NTSSetParam(oMenu, oApp.Tr(Me, 128333967842945626, "Controlli Aggiuntivi (NEWCTRL)"), "S", "N")
      ckImEx8.NTSSetParam(oMenu, oApp.Tr(Me, 128333967843101486, "Gest. Eventi (ALE)"), "S", "N")
      ckImEx9.NTSSetParam(oMenu, oApp.Tr(Me, 128333967843257346, "Pers. Globali (ALP)"), "S", "N")
      ckImEx5.NTSSetParam(oMenu, oApp.Tr(Me, 128333967843413206, "Re&port"), "S", "N")
      ckImEx1.NTSSetParam(oMenu, oApp.Tr(Me, 128333967843569066, "Layout di &Form"), "S", "N")
      ckImEx0.NTSSetParam(oMenu, oApp.Tr(Me, 129024311022400402, "Layout di &Griglia"), "S", "N")
      ckDipAzienda1.NTSSetParam(oMenu, oApp.Tr(Me, 129024311080683144, "N&on dipendente da azienda"), "S", "N")
      edAziendaExp.NTSSetParam(oMenu, oApp.Tr(Me, 128333967843880786, "Azienda export"), 40)
      ckDipAzienda0.NTSSetParam(oMenu, oApp.Tr(Me, 128333967844192506, "Di&pendente da azienda"), "S", "N")
      ckDipOperatore1.NTSSetParam(oMenu, oApp.Tr(Me, 128333967844348366, "&Non dipendente da operatore"), "S", "N")
      edUtenteExp.NTSSetParam(oMenu, oApp.Tr(Me, 128333967844504226, "Utente export"), 40)
      ckDipOperatore0.NTSSetParam(oMenu, oApp.Tr(Me, 128333967844815946, "D&ipendente da operatore"), "S", "N")

      edUtenteImp.NTSSetParamZoom("ZOOMOPERAT")
      edAziendaExp.NTSSetParamZoom("ZOOMTABANAZ")
      edUtenteExp.NTSSetParamZoom("ZOOMOPERAT")

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
  Public Overridable Sub InitEntity(ByRef cleGreg As CLE__GREG)
    oCleGreg = cleGreg
    AddHandler oCleGreg.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

#Region "EventiForm"
  Public Overridable Sub FRM__IMEX_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      If oCleGreg.strLbPath = "" Then oCleGreg.strLbPath = "BUSINESS"
      lbPath.Text = oCleGreg.strLbPath
      If oCleGreg.strNomeUt = "*" Then
        oCleGreg.strNomeUt = oApp.User.Nome
      End If
      If oCleGreg.strNomeAz = "*" Then
        oCleGreg.strNomeAz = DittaCorrente
      End If
      If oCleGreg.bEsporta Then
        fmExport.Visible = True
        fmImport.Visible = False
        edUtenteExp.Text = oCleGreg.strNomeUt
        edAziendaExp.Text = oCleGreg.strNomeAz
      Else
        fmExport.Visible = False
        fmImport.Visible = True
        edUtenteImp.Enabled = False
        edUtenteImp.Text = oCleGreg.strNomeUt
      End If
      ckImEx0.Checked = True
      ckImEx1.Checked = True
      ckImEx2.Checked = True
      ckImEx3.Checked = True
      ckImEx4.Checked = True
      ckImEx5.Checked = True
      ckImEx6.Checked = True
      ckImEx7.Checked = True
      ckImEx8.Checked = True
      ckImEx9.Checked = True

      Me.Text = oCleGreg.strCaption

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

  Public Overridable Sub ckDipOperatore_Click()
    Try
      If ckDipOperatore0.Checked = True Then
        lbUtenteExport.Enabled = True
        edUtenteExp.Enabled = True
      Else
        lbUtenteExport.Enabled = False
        edUtenteExp.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckDipAzienda_Click()
    Try
      If ckDipAzienda0.Checked = True Then
        lbAziendaExport.Enabled = True
        edAziendaExp.Enabled = True
      Else
        lbAziendaExport.Enabled = False
        edAziendaExp.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub opFiltroImp_Click()
    Try
      If opFiltroImp0.Checked = True Then
        edUtenteImp.Enabled = False
      Else
        edUtenteImp.Enabled = True
        edUtenteImp.Focus()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckDipOperatore0_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckDipOperatore0.CheckedChanged
    Try
      ckDipOperatore_Click()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckDipOperatore1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckDipOperatore1.CheckedChanged
    Try
      ckDipOperatore_Click()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckDipAzienda0_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckDipAzienda0.CheckedChanged
    Try
      ckDipAzienda_Click()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckDipAzienda1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckDipAzienda1.CheckedChanged
    Try
      ckDipAzienda_Click()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub opFiltroImp0_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles opFiltroImp0.CheckedChanged
    Try
      opFiltroImp_Click()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub opFiltroImp1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles opFiltroImp1.CheckedChanged
    Try
      opFiltroImp_Click()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
    Dim nPos As Integer
    Try
      nPos = InStr(1, edNomeFile.Text, ".")
      If nPos = 0 Then
        edNomeFile.Text = edNomeFile.Text & ".rbs"
      Else
        If edNomeFile.Text.Substring(Len(edNomeFile.Text) - 4, 4) <> ".rbs" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128333928091733722, "Nome file non riconosciuto."))
          Exit Sub
        End If
      End If
      nPos = InStr(1, edNomeFile.Text, "*")
      If nPos > 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128333928408072682, "Nome file non riconosciuto."))
        Exit Sub
      End If
      oCleGreg.bNoSost = False
      If oCleGreg.bEsporta Then
        If ckDipOperatore0.Checked = False And ckDipOperatore1.Checked = False Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128333928732520261, "Selezionare almeno un tipo esportazione operatore."))
          Exit Sub
        ElseIf ckDipAzienda0.Checked = False And ckDipAzienda1.Checked = False Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128333928958657160, "Selezionare almeno un tipo esportazione azienda."))
          Exit Sub
        Else
          Esportazione()
        End If
      Else
        Importazione()
      End If
      If Not oCleGreg.bNoSost Then
        Me.Close()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edNomeFile_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles edNomeFile.ButtonClick
    Try
      If oCleGreg.bEsporta Then
        SaveFileDialog1 = New NTSSaveFileDialog
        SaveFileDialog1.CheckFileExists = False
        SaveFileDialog1.ShowHelp = False
        SaveFileDialog1.DefaultExt = "rbs"
        SaveFileDialog1.Title = "Salva file di registro di Business"
        SaveFileDialog1.Filter = "File di registro di Business (*.rbs)|*.rbs|Tutti i files (*.*)|*.*"
        SaveFileDialog1.InitialDirectory = oApp.Dir
        SaveFileDialog1.FileName = ""
        SaveFileDialog1.oMenu = oMenu
        SaveFileDialog1.ShowDialog()
        edNomeFile.Text = SaveFileDialog1.FileName
      Else
        OpenFileDialog1 = New NTSOpenFileDialog
        OpenFileDialog1.CheckFileExists = True
        OpenFileDialog1.ShowReadOnly = False
        OpenFileDialog1.ShowHelp = False
        OpenFileDialog1.DefaultExt = "rbs"
        OpenFileDialog1.Title = "File di registro di Business"
        OpenFileDialog1.Filter = "File di registro di Business (*.rbs)|*.rbs|Tutti i files (*.*)|*.*"
        OpenFileDialog1.InitialDirectory = oApp.Dir
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.oMenu = oMenu
        OpenFileDialog1.ShowDialog()
        edNomeFile.Text = OpenFileDialog1.FileName
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub Esportazione()
    Try
      oCleGreg.Esportazione(opExport0.Checked, ckDipOperatore0.Checked, ckDipOperatore1.Checked, _
          ckDipAzienda0.Checked, ckDipAzienda1.Checked, _
          False, False, ckImEx2.Checked, ckImEx3.Checked, False, _
          ckImEx5.Checked, False, False, False, False, _
          edUtenteExp.Text, edAziendaExp.Text, edNomeFile.Text)
      oApp.MsgBoxInfo(oApp.Tr(Me, 128333967841698778, "Esportazione terminata."))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub Importazione()
    Try
      oCleGreg.Importazione(opFiltroImp0.Checked, _
      False, False, ckImEx2.Checked, ckImEx3.Checked, False, _
      ckImEx5.Checked, False, False, False, False, _
      edUtenteImp.Text, edNomeFile.Text, ckHow.Checked)
      oApp.MsgBoxInfo(oApp.Tr(Me, 128333967841698779, "Importazione terminata."))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class