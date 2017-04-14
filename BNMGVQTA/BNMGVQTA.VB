Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGVQTA
  Private Moduli_P As Integer = bsModMG + bsModVE + bsModOR
  Private ModuliExt_P As Integer = bsModExtMGE + bsModExtORE
  Private ModuliSup_P As Integer = 0
  Private ModuliSupExt_P As Integer = 0
  Private ModuliPtn_P As Integer = 0
  Private ModuliPtnExt_P As Integer = 0

  Public ReadOnly Property Moduli() As Integer
    Get
      Return Moduli_P
    End Get
  End Property
  Public ReadOnly Property ModuliExt() As Integer
    Get
      Return ModuliExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliSup() As Integer
    Get
      Return ModuliSup_P
    End Get
  End Property
  Public ReadOnly Property ModuliSupExt() As Integer
    Get
      Return ModuliSupExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtn() As Integer
    Get
      Return ModuliPtn_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtnExt() As Integer
    Get
      Return ModuliPtnExt_P
    End Get
  End Property

#Region "VARIABILI"
  Public oCleVqta As CLEMGVQTA
  Public oCallParams As CLE__CLDP

  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbElabora As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents fmVqta As NTSInformatica.NTSGroupBox
  Public WithEvents opAr_perqta As NTSInformatica.NTSRadioButton
  Public WithEvents lbAr_perqta As NTSInformatica.NTSLabel
  Public WithEvents edAr_perqta As NTSInformatica.NTSTextBoxNum
  Public WithEvents opAr_gescomm As NTSInformatica.NTSRadioButton
  Public WithEvents cbAr_gescommIni As NTSInformatica.NTSComboBox
  Public WithEvents cbAr_gescommFin As NTSInformatica.NTSComboBox
  Public WithEvents lbAr_gescommFin As NTSInformatica.NTSLabel
  Public WithEvents lbAr_gescommIni As NTSInformatica.NTSLabel
  Public WithEvents opAr_geslotti As NTSInformatica.NTSRadioButton
  Public WithEvents cbAr_geslottiFin As NTSInformatica.NTSComboBox
  Public WithEvents lbAr_geslottiFin As NTSInformatica.NTSLabel
  Public WithEvents lbAr_geslottiIni As NTSInformatica.NTSLabel
  Public WithEvents cbAr_geslottiIni As NTSInformatica.NTSComboBox
  Public WithEvents cbAr_gestmatrFin As NTSInformatica.NTSComboBox
  Public WithEvents lbAr_gestmatrFin As NTSInformatica.NTSLabel
  Public WithEvents lbAr_gestmatrIni As NTSInformatica.NTSLabel
  Public WithEvents cbAr_gestmatrIni As NTSInformatica.NTSComboBox
  Public WithEvents opAr_gestmatr As NTSInformatica.NTSRadioButton
  Public WithEvents cbAr_gesubicFin As NTSInformatica.NTSComboBox
  Public WithEvents lbAr_gesubicFin As NTSInformatica.NTSLabel
  Public WithEvents lbAr_gesubicIni As NTSInformatica.NTSLabel
  Public WithEvents cbAr_gesubicIni As NTSInformatica.NTSComboBox
  Public WithEvents opAr_gesubic As NTSInformatica.NTSRadioButton
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents lbDescr As NTSInformatica.NTSLabel

  Private components As System.ComponentModel.IContainer
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

    '------------------------------------------------
    'creo e attivo l'entity e inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGVQTA", "BEMGVQTA", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128570465788163478, "ERRORE in fase di creazione Entity:" & vbCrLf) & strErr)
      Return False
    End If
    oCleVqta = CType(oTmp, CLEMGVQTA)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGVQTA", strRemoteServer, strRemotePort)
    AddHandler oCleVqta.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleVqta.Init(oApp, oScript, oMenu.oCleComm, "ARTICO", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGVQTA))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbElabora = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.fmVqta = New NTSInformatica.NTSGroupBox
    Me.cbAr_gesubicFin = New NTSInformatica.NTSComboBox
    Me.lbAr_gesubicFin = New NTSInformatica.NTSLabel
    Me.lbAr_gesubicIni = New NTSInformatica.NTSLabel
    Me.cbAr_gesubicIni = New NTSInformatica.NTSComboBox
    Me.opAr_gesubic = New NTSInformatica.NTSRadioButton
    Me.cbAr_gestmatrFin = New NTSInformatica.NTSComboBox
    Me.lbAr_gestmatrFin = New NTSInformatica.NTSLabel
    Me.lbAr_gestmatrIni = New NTSInformatica.NTSLabel
    Me.cbAr_gestmatrIni = New NTSInformatica.NTSComboBox
    Me.opAr_gestmatr = New NTSInformatica.NTSRadioButton
    Me.cbAr_geslottiFin = New NTSInformatica.NTSComboBox
    Me.lbAr_geslottiFin = New NTSInformatica.NTSLabel
    Me.lbAr_geslottiIni = New NTSInformatica.NTSLabel
    Me.cbAr_geslottiIni = New NTSInformatica.NTSComboBox
    Me.opAr_geslotti = New NTSInformatica.NTSRadioButton
    Me.cbAr_gescommFin = New NTSInformatica.NTSComboBox
    Me.lbAr_gescommFin = New NTSInformatica.NTSLabel
    Me.lbAr_gescommIni = New NTSInformatica.NTSLabel
    Me.cbAr_gescommIni = New NTSInformatica.NTSComboBox
    Me.opAr_gescomm = New NTSInformatica.NTSRadioButton
    Me.edAr_perqta = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_perqta = New NTSInformatica.NTSLabel
    Me.opAr_perqta = New NTSInformatica.NTSRadioButton
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.lbDescr = New NTSInformatica.NTSLabel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmVqta, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmVqta.SuspendLayout()
    CType(Me.cbAr_gesubicFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_gesubicIni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opAr_gesubic.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_gestmatrFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_gestmatrIni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opAr_gestmatr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_geslottiFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_geslottiIni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opAr_geslotti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_gescommFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_gescommIni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opAr_gescomm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_perqta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opAr_perqta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'NtsBarManager1
    '
    Me.NtsBarManager1.AllowCustomization = False
    Me.NtsBarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.tlbMain})
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlTop)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlBottom)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlLeft)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlRight)
    Me.NtsBarManager1.Form = Me
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbElabora, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 4
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbElabora), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbElabora
    '
    Me.tlbElabora.Caption = "Elabora"
    Me.tlbElabora.Glyph = CType(resources.GetObject("tlbElabora.Glyph"), System.Drawing.Image)
    Me.tlbElabora.Id = 0
    Me.tlbElabora.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbElabora.Name = "tlbElabora"
    Me.tlbElabora.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 1
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 3
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'fmVqta
    '
    Me.fmVqta.AllowDrop = True
    Me.fmVqta.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmVqta.Appearance.Options.UseBackColor = True
    Me.fmVqta.Controls.Add(Me.cbAr_gesubicFin)
    Me.fmVqta.Controls.Add(Me.lbAr_gesubicFin)
    Me.fmVqta.Controls.Add(Me.lbAr_gesubicIni)
    Me.fmVqta.Controls.Add(Me.cbAr_gesubicIni)
    Me.fmVqta.Controls.Add(Me.opAr_gesubic)
    Me.fmVqta.Controls.Add(Me.cbAr_gestmatrFin)
    Me.fmVqta.Controls.Add(Me.lbAr_gestmatrFin)
    Me.fmVqta.Controls.Add(Me.lbAr_gestmatrIni)
    Me.fmVqta.Controls.Add(Me.cbAr_gestmatrIni)
    Me.fmVqta.Controls.Add(Me.opAr_gestmatr)
    Me.fmVqta.Controls.Add(Me.cbAr_geslottiFin)
    Me.fmVqta.Controls.Add(Me.lbAr_geslottiFin)
    Me.fmVqta.Controls.Add(Me.lbAr_geslottiIni)
    Me.fmVqta.Controls.Add(Me.cbAr_geslottiIni)
    Me.fmVqta.Controls.Add(Me.opAr_geslotti)
    Me.fmVqta.Controls.Add(Me.cbAr_gescommFin)
    Me.fmVqta.Controls.Add(Me.lbAr_gescommFin)
    Me.fmVqta.Controls.Add(Me.lbAr_gescommIni)
    Me.fmVqta.Controls.Add(Me.cbAr_gescommIni)
    Me.fmVqta.Controls.Add(Me.opAr_gescomm)
    Me.fmVqta.Controls.Add(Me.edAr_perqta)
    Me.fmVqta.Controls.Add(Me.lbAr_perqta)
    Me.fmVqta.Controls.Add(Me.opAr_perqta)
    Me.fmVqta.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmVqta.Location = New System.Drawing.Point(12, 85)
    Me.fmVqta.Name = "fmVqta"
    Me.fmVqta.Size = New System.Drawing.Size(699, 170)
    Me.fmVqta.TabIndex = 4
    Me.fmVqta.Text = "Campi da modificare:"
    '
    'cbAr_gesubicFin
    '
    Me.cbAr_gesubicFin.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_gesubicFin.DataSource = Nothing
    Me.cbAr_gesubicFin.DisplayMember = ""
    Me.cbAr_gesubicFin.Enabled = False
    Me.cbAr_gesubicFin.Location = New System.Drawing.Point(509, 136)
    Me.cbAr_gesubicFin.Name = "cbAr_gesubicFin"
    Me.cbAr_gesubicFin.NTSDbField = ""
    Me.cbAr_gesubicFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_gesubicFin.Properties.DropDownRows = 30
    Me.cbAr_gesubicFin.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_gesubicFin.SelectedValue = ""
    Me.cbAr_gesubicFin.Size = New System.Drawing.Size(100, 20)
    Me.cbAr_gesubicFin.TabIndex = 124
    Me.cbAr_gesubicFin.ValueMember = ""
    '
    'lbAr_gesubicFin
    '
    Me.lbAr_gesubicFin.AutoSize = True
    Me.lbAr_gesubicFin.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_gesubicFin.Enabled = False
    Me.lbAr_gesubicFin.Location = New System.Drawing.Point(490, 139)
    Me.lbAr_gesubicFin.Name = "lbAr_gesubicFin"
    Me.lbAr_gesubicFin.NTSDbField = ""
    Me.lbAr_gesubicFin.Size = New System.Drawing.Size(13, 13)
    Me.lbAr_gesubicFin.TabIndex = 123
    Me.lbAr_gesubicFin.Text = "a"
    Me.lbAr_gesubicFin.UseMnemonic = False
    '
    'lbAr_gesubicIni
    '
    Me.lbAr_gesubicIni.AutoSize = True
    Me.lbAr_gesubicIni.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_gesubicIni.Enabled = False
    Me.lbAr_gesubicIni.Location = New System.Drawing.Point(204, 139)
    Me.lbAr_gesubicIni.Name = "lbAr_gesubicIni"
    Me.lbAr_gesubicIni.NTSDbField = ""
    Me.lbAr_gesubicIni.Size = New System.Drawing.Size(20, 13)
    Me.lbAr_gesubicIni.TabIndex = 122
    Me.lbAr_gesubicIni.Text = "Da"
    Me.lbAr_gesubicIni.UseMnemonic = False
    '
    'cbAr_gesubicIni
    '
    Me.cbAr_gesubicIni.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_gesubicIni.DataSource = Nothing
    Me.cbAr_gesubicIni.DisplayMember = ""
    Me.cbAr_gesubicIni.Enabled = False
    Me.cbAr_gesubicIni.Location = New System.Drawing.Point(283, 136)
    Me.cbAr_gesubicIni.Name = "cbAr_gesubicIni"
    Me.cbAr_gesubicIni.NTSDbField = ""
    Me.cbAr_gesubicIni.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_gesubicIni.Properties.DropDownRows = 30
    Me.cbAr_gesubicIni.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_gesubicIni.SelectedValue = ""
    Me.cbAr_gesubicIni.Size = New System.Drawing.Size(100, 20)
    Me.cbAr_gesubicIni.TabIndex = 121
    Me.cbAr_gesubicIni.ValueMember = ""
    '
    'opAr_gesubic
    '
    Me.opAr_gesubic.Cursor = System.Windows.Forms.Cursors.Default
    Me.opAr_gesubic.Location = New System.Drawing.Point(14, 137)
    Me.opAr_gesubic.Name = "opAr_gesubic"
    Me.opAr_gesubic.NTSCheckValue = "S"
    Me.opAr_gesubic.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opAr_gesubic.Properties.Appearance.Options.UseBackColor = True
    Me.opAr_gesubic.Properties.Caption = "Gestione Ubicazione"
    Me.opAr_gesubic.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opAr_gesubic.Size = New System.Drawing.Size(124, 19)
    Me.opAr_gesubic.TabIndex = 120
    '
    'cbAr_gestmatrFin
    '
    Me.cbAr_gestmatrFin.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_gestmatrFin.DataSource = Nothing
    Me.cbAr_gestmatrFin.DisplayMember = ""
    Me.cbAr_gestmatrFin.Enabled = False
    Me.cbAr_gestmatrFin.Location = New System.Drawing.Point(509, 110)
    Me.cbAr_gestmatrFin.Name = "cbAr_gestmatrFin"
    Me.cbAr_gestmatrFin.NTSDbField = ""
    Me.cbAr_gestmatrFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_gestmatrFin.Properties.DropDownRows = 30
    Me.cbAr_gestmatrFin.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_gestmatrFin.SelectedValue = ""
    Me.cbAr_gestmatrFin.Size = New System.Drawing.Size(100, 20)
    Me.cbAr_gestmatrFin.TabIndex = 119
    Me.cbAr_gestmatrFin.ValueMember = ""
    '
    'lbAr_gestmatrFin
    '
    Me.lbAr_gestmatrFin.AutoSize = True
    Me.lbAr_gestmatrFin.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_gestmatrFin.Enabled = False
    Me.lbAr_gestmatrFin.Location = New System.Drawing.Point(490, 113)
    Me.lbAr_gestmatrFin.Name = "lbAr_gestmatrFin"
    Me.lbAr_gestmatrFin.NTSDbField = ""
    Me.lbAr_gestmatrFin.Size = New System.Drawing.Size(13, 13)
    Me.lbAr_gestmatrFin.TabIndex = 118
    Me.lbAr_gestmatrFin.Text = "a"
    Me.lbAr_gestmatrFin.UseMnemonic = False
    '
    'lbAr_gestmatrIni
    '
    Me.lbAr_gestmatrIni.AutoSize = True
    Me.lbAr_gestmatrIni.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_gestmatrIni.Enabled = False
    Me.lbAr_gestmatrIni.Location = New System.Drawing.Point(204, 113)
    Me.lbAr_gestmatrIni.Name = "lbAr_gestmatrIni"
    Me.lbAr_gestmatrIni.NTSDbField = ""
    Me.lbAr_gestmatrIni.Size = New System.Drawing.Size(20, 13)
    Me.lbAr_gestmatrIni.TabIndex = 117
    Me.lbAr_gestmatrIni.Text = "Da"
    Me.lbAr_gestmatrIni.UseMnemonic = False
    '
    'cbAr_gestmatrIni
    '
    Me.cbAr_gestmatrIni.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_gestmatrIni.DataSource = Nothing
    Me.cbAr_gestmatrIni.DisplayMember = ""
    Me.cbAr_gestmatrIni.Enabled = False
    Me.cbAr_gestmatrIni.Location = New System.Drawing.Point(283, 110)
    Me.cbAr_gestmatrIni.Name = "cbAr_gestmatrIni"
    Me.cbAr_gestmatrIni.NTSDbField = ""
    Me.cbAr_gestmatrIni.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_gestmatrIni.Properties.DropDownRows = 30
    Me.cbAr_gestmatrIni.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_gestmatrIni.SelectedValue = ""
    Me.cbAr_gestmatrIni.Size = New System.Drawing.Size(100, 20)
    Me.cbAr_gestmatrIni.TabIndex = 116
    Me.cbAr_gestmatrIni.ValueMember = ""
    '
    'opAr_gestmatr
    '
    Me.opAr_gestmatr.Cursor = System.Windows.Forms.Cursors.Default
    Me.opAr_gestmatr.Location = New System.Drawing.Point(14, 111)
    Me.opAr_gestmatr.Name = "opAr_gestmatr"
    Me.opAr_gestmatr.NTSCheckValue = "S"
    Me.opAr_gestmatr.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opAr_gestmatr.Properties.Appearance.Options.UseBackColor = True
    Me.opAr_gestmatr.Properties.Caption = "Gestione Matricole"
    Me.opAr_gestmatr.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opAr_gestmatr.Size = New System.Drawing.Size(111, 19)
    Me.opAr_gestmatr.TabIndex = 115
    '
    'cbAr_geslottiFin
    '
    Me.cbAr_geslottiFin.Cursor = System.Windows.Forms.Cursors.Hand
    Me.cbAr_geslottiFin.DataSource = Nothing
    Me.cbAr_geslottiFin.DisplayMember = ""
    Me.cbAr_geslottiFin.Enabled = False
    Me.cbAr_geslottiFin.Location = New System.Drawing.Point(509, 84)
    Me.cbAr_geslottiFin.Name = "cbAr_geslottiFin"
    Me.cbAr_geslottiFin.NTSDbField = ""
    Me.cbAr_geslottiFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_geslottiFin.Properties.DropDownRows = 30
    Me.cbAr_geslottiFin.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_geslottiFin.SelectedValue = ""
    Me.cbAr_geslottiFin.Size = New System.Drawing.Size(100, 20)
    Me.cbAr_geslottiFin.TabIndex = 114
    Me.cbAr_geslottiFin.ValueMember = ""
    '
    'lbAr_geslottiFin
    '
    Me.lbAr_geslottiFin.AutoSize = True
    Me.lbAr_geslottiFin.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_geslottiFin.Enabled = False
    Me.lbAr_geslottiFin.Location = New System.Drawing.Point(490, 87)
    Me.lbAr_geslottiFin.Name = "lbAr_geslottiFin"
    Me.lbAr_geslottiFin.NTSDbField = ""
    Me.lbAr_geslottiFin.Size = New System.Drawing.Size(13, 13)
    Me.lbAr_geslottiFin.TabIndex = 113
    Me.lbAr_geslottiFin.Text = "a"
    Me.lbAr_geslottiFin.UseMnemonic = False
    '
    'lbAr_geslottiIni
    '
    Me.lbAr_geslottiIni.AutoSize = True
    Me.lbAr_geslottiIni.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_geslottiIni.Enabled = False
    Me.lbAr_geslottiIni.Location = New System.Drawing.Point(204, 87)
    Me.lbAr_geslottiIni.Name = "lbAr_geslottiIni"
    Me.lbAr_geslottiIni.NTSDbField = ""
    Me.lbAr_geslottiIni.Size = New System.Drawing.Size(20, 13)
    Me.lbAr_geslottiIni.TabIndex = 112
    Me.lbAr_geslottiIni.Text = "Da"
    Me.lbAr_geslottiIni.UseMnemonic = False
    '
    'cbAr_geslottiIni
    '
    Me.cbAr_geslottiIni.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_geslottiIni.DataSource = Nothing
    Me.cbAr_geslottiIni.DisplayMember = ""
    Me.cbAr_geslottiIni.Enabled = False
    Me.cbAr_geslottiIni.Location = New System.Drawing.Point(283, 84)
    Me.cbAr_geslottiIni.Name = "cbAr_geslottiIni"
    Me.cbAr_geslottiIni.NTSDbField = ""
    Me.cbAr_geslottiIni.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_geslottiIni.Properties.DropDownRows = 30
    Me.cbAr_geslottiIni.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_geslottiIni.SelectedValue = ""
    Me.cbAr_geslottiIni.Size = New System.Drawing.Size(100, 20)
    Me.cbAr_geslottiIni.TabIndex = 111
    Me.cbAr_geslottiIni.ValueMember = ""
    '
    'opAr_geslotti
    '
    Me.opAr_geslotti.Cursor = System.Windows.Forms.Cursors.Default
    Me.opAr_geslotti.Location = New System.Drawing.Point(14, 85)
    Me.opAr_geslotti.Name = "opAr_geslotti"
    Me.opAr_geslotti.NTSCheckValue = "S"
    Me.opAr_geslotti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opAr_geslotti.Properties.Appearance.Options.UseBackColor = True
    Me.opAr_geslotti.Properties.Caption = "Gestione Lotti"
    Me.opAr_geslotti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opAr_geslotti.Size = New System.Drawing.Size(92, 19)
    Me.opAr_geslotti.TabIndex = 110
    '
    'cbAr_gescommFin
    '
    Me.cbAr_gescommFin.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_gescommFin.DataSource = Nothing
    Me.cbAr_gescommFin.DisplayMember = ""
    Me.cbAr_gescommFin.Enabled = False
    Me.cbAr_gescommFin.Location = New System.Drawing.Point(509, 59)
    Me.cbAr_gescommFin.Name = "cbAr_gescommFin"
    Me.cbAr_gescommFin.NTSDbField = ""
    Me.cbAr_gescommFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_gescommFin.Properties.DropDownRows = 30
    Me.cbAr_gescommFin.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_gescommFin.SelectedValue = ""
    Me.cbAr_gescommFin.Size = New System.Drawing.Size(176, 20)
    Me.cbAr_gescommFin.TabIndex = 109
    Me.cbAr_gescommFin.ValueMember = ""
    '
    'lbAr_gescommFin
    '
    Me.lbAr_gescommFin.AutoSize = True
    Me.lbAr_gescommFin.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_gescommFin.Enabled = False
    Me.lbAr_gescommFin.Location = New System.Drawing.Point(490, 62)
    Me.lbAr_gescommFin.Name = "lbAr_gescommFin"
    Me.lbAr_gescommFin.NTSDbField = ""
    Me.lbAr_gescommFin.Size = New System.Drawing.Size(13, 13)
    Me.lbAr_gescommFin.TabIndex = 108
    Me.lbAr_gescommFin.Text = "a"
    Me.lbAr_gescommFin.UseMnemonic = False
    '
    'lbAr_gescommIni
    '
    Me.lbAr_gescommIni.AutoSize = True
    Me.lbAr_gescommIni.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_gescommIni.Enabled = False
    Me.lbAr_gescommIni.Location = New System.Drawing.Point(204, 62)
    Me.lbAr_gescommIni.Name = "lbAr_gescommIni"
    Me.lbAr_gescommIni.NTSDbField = ""
    Me.lbAr_gescommIni.Size = New System.Drawing.Size(20, 13)
    Me.lbAr_gescommIni.TabIndex = 107
    Me.lbAr_gescommIni.Text = "Da"
    Me.lbAr_gescommIni.UseMnemonic = False
    '
    'cbAr_gescommIni
    '
    Me.cbAr_gescommIni.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_gescommIni.DataSource = Nothing
    Me.cbAr_gescommIni.DisplayMember = ""
    Me.cbAr_gescommIni.Enabled = False
    Me.cbAr_gescommIni.Location = New System.Drawing.Point(283, 59)
    Me.cbAr_gescommIni.Name = "cbAr_gescommIni"
    Me.cbAr_gescommIni.NTSDbField = ""
    Me.cbAr_gescommIni.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_gescommIni.Properties.DropDownRows = 30
    Me.cbAr_gescommIni.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_gescommIni.SelectedValue = ""
    Me.cbAr_gescommIni.Size = New System.Drawing.Size(176, 20)
    Me.cbAr_gescommIni.TabIndex = 106
    Me.cbAr_gescommIni.ValueMember = ""
    '
    'opAr_gescomm
    '
    Me.opAr_gescomm.Cursor = System.Windows.Forms.Cursors.Default
    Me.opAr_gescomm.Location = New System.Drawing.Point(14, 60)
    Me.opAr_gescomm.Name = "opAr_gescomm"
    Me.opAr_gescomm.NTSCheckValue = "S"
    Me.opAr_gescomm.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opAr_gescomm.Properties.Appearance.Options.UseBackColor = True
    Me.opAr_gescomm.Properties.Caption = "Gestione per Commessa"
    Me.opAr_gescomm.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opAr_gescomm.Size = New System.Drawing.Size(145, 19)
    Me.opAr_gescomm.TabIndex = 3
    '
    'edAr_perqta
    '
    Me.edAr_perqta.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_perqta.Location = New System.Drawing.Point(283, 34)
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
    Me.edAr_perqta.Size = New System.Drawing.Size(100, 20)
    Me.edAr_perqta.TabIndex = 2
    '
    'lbAr_perqta
    '
    Me.lbAr_perqta.AutoSize = True
    Me.lbAr_perqta.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_perqta.Location = New System.Drawing.Point(204, 37)
    Me.lbAr_perqta.Name = "lbAr_perqta"
    Me.lbAr_perqta.NTSDbField = ""
    Me.lbAr_perqta.Size = New System.Drawing.Size(71, 13)
    Me.lbAr_perqta.TabIndex = 1
    Me.lbAr_perqta.Text = "Nuovo valore"
    Me.lbAr_perqta.UseMnemonic = False
    '
    'opAr_perqta
    '
    Me.opAr_perqta.Cursor = System.Windows.Forms.Cursors.Default
    Me.opAr_perqta.EditValue = True
    Me.opAr_perqta.Location = New System.Drawing.Point(14, 35)
    Me.opAr_perqta.Name = "opAr_perqta"
    Me.opAr_perqta.NTSCheckValue = "S"
    Me.opAr_perqta.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opAr_perqta.Properties.Appearance.Options.UseBackColor = True
    Me.opAr_perqta.Properties.Caption = "Moltiplicatore Quantità/Prezzo"
    Me.opAr_perqta.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opAr_perqta.Size = New System.Drawing.Size(171, 19)
    Me.opAr_perqta.TabIndex = 0
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.Location = New System.Drawing.Point(12, 45)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.Size = New System.Drawing.Size(150, 24)
    Me.cmdSeleziona.TabIndex = 113
    Me.cmdSeleziona.Text = "Seleziona Articoli"
    '
    'lbDescr
    '
    Me.lbDescr.BackColor = System.Drawing.Color.Transparent
    Me.lbDescr.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbDescr.Location = New System.Drawing.Point(23, 269)
    Me.lbDescr.Name = "lbDescr"
    Me.lbDescr.NTSDbField = ""
    Me.lbDescr.Size = New System.Drawing.Size(688, 22)
    Me.lbDescr.TabIndex = 114
    Me.lbDescr.Text = "Nessuna elaborazione in corso"
    Me.lbDescr.UseMnemonic = False
    '
    'FRMMGVQTA
    '
    Me.ClientSize = New System.Drawing.Size(723, 300)
    Me.Controls.Add(Me.lbDescr)
    Me.Controls.Add(Me.cmdSeleziona)
    Me.Controls.Add(Me.fmVqta)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRMMGVQTA"
    Me.Text = "VARIAZIONE CAMPI PARTICOLARI ARTICOLI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmVqta, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmVqta.ResumeLayout(False)
    Me.fmVqta.PerformLayout()
    CType(Me.cbAr_gesubicFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_gesubicIni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opAr_gesubic.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_gestmatrFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_gestmatrIni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opAr_gestmatr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_geslottiFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_geslottiIni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opAr_geslotti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_gescommFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_gescommIni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opAr_gescomm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_perqta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opAr_perqta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '----------------------------------------------------------------------------------
      Try
        tlbElabora.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'Ã¨ una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()
      '----------------------------------------------------------------------------------
      opAr_perqta.NTSSetParam(oMenu, oApp.Tr(Me, 128570565145367839, "Moltiplicatore Quantità/Prezzo"), "S")
      opAr_gescomm.NTSSetParam(oMenu, oApp.Tr(Me, 128570568434977562, "Gestione per Commessa"), "N")
      opAr_geslotti.NTSSetParam(oMenu, oApp.Tr(Me, 128570568903268766, "Gestione Lotti"), "N")
      opAr_gestmatr.NTSSetParam(oMenu, oApp.Tr(Me, 128570569123954694, "Gestione Matricole"), "N")
      opAr_gesubic.NTSSetParam(oMenu, oApp.Tr(Me, 128570569539491006, "Gestione Ubicazione"), "N")
      '----------------------------------------------------------------------------------
      edAr_perqta.NTSSetParam(oMenu, oApp.Tr(Me, 128570572541464201, "Nuovo valore Moltiplicatore Quantità/Prezzo"), "", 12, 1, 999999999999)
      '----------------------------------------------------------------------------------
      cbAr_gescommIni.NTSSetParam(oApp.Tr(Me, 128570572568991736, "Valore da sostituire"))
      cbAr_gescommFin.NTSSetParam(oApp.Tr(Me, 128570572912094776, "Nuovo valore da inserire"))
      cbAr_geslottiIni.NTSSetParam(oApp.Tr(Me, 128570573212212286, "Valore da sostituire"))
      cbAr_geslottiIni.NTSSetParam(oApp.Tr(Me, 128570573245579291, "Nuovo valore da inserire"))
      cbAr_gestmatrIni.NTSSetParam(oApp.Tr(Me, 128570573695560256, "Valore da sostituire"))
      cbAr_gestmatrIni.NTSSetParam(oApp.Tr(Me, 128570573716301116, "Nuovo valore da inserire"))
      cbAr_gesubicIni.NTSSetParam(oApp.Tr(Me, 128570573886221881, "Valore da sostituire"))
      cbAr_gesubicIni.NTSSetParam(oApp.Tr(Me, 128570573906904151, "Nuovo valore da inserire"))
      '----------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

#Region "FORM"
  Public Overridable Sub FRMMGVQTA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '----------------------------------------------------------------------------------
      CaricaCombo()
      '----------------------------------------------------------------------------------
      InitControls()
      '----------------------------------------------------------------------------------
      oCleVqta.bSeleziona = False
      '----------------------------------------------------------------------------------
      edAr_perqta.Text = "1"
      '----------------------------------------------------------------------------------
      GctlSetRoules()
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGVQTA_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If oCleVqta.bElabInCorso Then e.Cancel = True
  End Sub
#End Region

#Region "TOOLBAR"
  Public Overridable Sub tlbElabora_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbElabora.ItemClick
    Try
      '----------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '----------------------------------------------------------------------------------
      If Not Elabora() Then Return
      '----------------------------------------------------------------------------------
      If oCleVqta.nLogCount > 0 Then
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129356689615767552, "Attenzione!" & vbCrLf & _
          "Esistono dei messaggi nel file di log relativo a:" & vbCrLf & _
          " --> VARIAZIONE CAMPI PARTICOLARI ARTICOLI." & vbCrLf & _
          "Visualizzare il file?")) = Windows.Forms.DialogResult.No Then Return
        System.Diagnostics.Process.Start("notepad", oApp.Dir & "\" & "BSMGVQTA.log")
      End If
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '----------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub
#End Region

#Region "PULSANTI"
  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Dim oPar As CLE__PATB = Nothing
    Try
      '----------------------------------------------------------------------------------
      oPar = New CLE__PATB
      oPar.bVisGriglia = False
      oPar.strTipoArticolo = "N"
      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oPar)
      oCleVqta.strWhereFiar = oPar.strOut.Trim
      '----------------------------------------------------------------------------------
      If oCleVqta.strWhereFiar = "" Then
        oCleVqta.bSeleziona = False
      Else
        oCleVqta.bSeleziona = True
      End If
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub
#End Region

#Region "OPTION-BUTTON"
  Public Overridable Sub opAr_perqta_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opAr_perqta.CheckedChanged
    Try
      SettaControlli()
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub opAr_gescomm_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opAr_gescomm.CheckedChanged
    Try
      SettaControlli()
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub opAr_lotti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opAr_geslotti.CheckedChanged
    Try
      SettaControlli()
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub opAr_gestmatr_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opAr_gestmatr.CheckedChanged
    Try
      SettaControlli()
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub opAr_gesubic_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opAr_gesubic.CheckedChanged
    Try
      SettaControlli()
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub
#End Region

#Region "COMBO-BOX"
  Public Overridable Sub cbAr_geslottiIni_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAr_geslottiIni.SelectedIndexChanged
    Try
      '----------------------------------------------------------------------------------
      Select Case cbAr_geslottiIni.SelectedValue
        Case "N" : cbAr_geslottiFin.SelectedValue = "S"
        Case "S" : cbAr_geslottiFin.SelectedValue = "N"
      End Select
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cbAr_gestmatrIni_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAr_gestmatrIni.SelectedIndexChanged
    Try
      '----------------------------------------------------------------------------------
      Select Case cbAr_gestmatrIni.SelectedValue
        Case "N" : cbAr_gestmatrFin.SelectedValue = "S"
        Case "S" : cbAr_gestmatrFin.SelectedValue = "N"
      End Select
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cbAr_gesubicIni_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAr_gesubicIni.SelectedIndexChanged
    Try
      '----------------------------------------------------------------------------------
      Select Case cbAr_gesubicIni.SelectedValue
        Case "N" : cbAr_gesubicFin.SelectedValue = "S"
        Case "S" : cbAr_gesubicFin.SelectedValue = "N"
      End Select
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub
#End Region

#Region "FUNZIONI"
  Public Overridable Sub CaricaCombo()
    Try
      '----------------------------------------------------------------------------------
      Dim dttGescommIni As New DataTable()
      dttGescommIni.Columns.Add("cod", GetType(String))
      dttGescommIni.Columns.Add("val", GetType(String))
      dttGescommIni.Rows.Add(New Object() {"N", "No"})
      dttGescommIni.Rows.Add(New Object() {"O", "Ordini/Impegni"})
      dttGescommIni.Rows.Add(New Object() {"S", "Ordini/Impegni/Magazzino"})
      dttGescommIni.AcceptChanges()
      cbAr_gescommIni.DataSource = dttGescommIni
      cbAr_gescommIni.ValueMember = "cod"
      cbAr_gescommIni.DisplayMember = "val"
      cbAr_gescommIni.SelectedValue = "N"
      '----------------------------------------------------------------------------------
      Dim dttGescommFin As New DataTable()
      dttGescommFin.Columns.Add("cod", GetType(String))
      dttGescommFin.Columns.Add("val", GetType(String))
      dttGescommFin.Rows.Add(New Object() {"N", "No"})
      dttGescommFin.Rows.Add(New Object() {"O", "Ordini/Impegni"})
      dttGescommFin.Rows.Add(New Object() {"S", "Ordini/Impegni/Magazzino"})
      dttGescommFin.AcceptChanges()
      cbAr_gescommFin.DataSource = dttGescommFin
      cbAr_gescommFin.ValueMember = "cod"
      cbAr_gescommFin.DisplayMember = "val"
      cbAr_gescommFin.SelectedValue = "O"
      '----------------------------------------------------------------------------------
      Dim dttGeslottiIni As New DataTable()
      dttGeslottiIni.Columns.Add("cod", GetType(String))
      dttGeslottiIni.Columns.Add("val", GetType(String))
      dttGeslottiIni.Rows.Add(New Object() {"N", "No"})
      dttGeslottiIni.Rows.Add(New Object() {"S", "Sì"})
      dttGeslottiIni.AcceptChanges()
      cbAr_geslottiIni.DataSource = dttGeslottiIni
      cbAr_geslottiIni.ValueMember = "cod"
      cbAr_geslottiIni.DisplayMember = "val"
      cbAr_geslottiIni.SelectedValue = "N"
      '----------------------------------------------------------------------------------
      Dim dttGeslottiFin As New DataTable()
      dttGeslottiFin.Columns.Add("cod", GetType(String))
      dttGeslottiFin.Columns.Add("val", GetType(String))
      dttGeslottiFin.Rows.Add(New Object() {"N", "No"})
      dttGeslottiFin.Rows.Add(New Object() {"S", "Sì"})
      dttGeslottiFin.AcceptChanges()
      cbAr_geslottiFin.DataSource = dttGeslottiFin
      cbAr_geslottiFin.ValueMember = "cod"
      cbAr_geslottiFin.DisplayMember = "val"
      cbAr_geslottiFin.SelectedValue = "S"
      '----------------------------------------------------------------------------------
      Dim dttGestmatrIni As New DataTable()
      dttGestmatrIni.Columns.Add("cod", GetType(String))
      dttGestmatrIni.Columns.Add("val", GetType(String))
      dttGestmatrIni.Rows.Add(New Object() {"N", "No"})
      dttGestmatrIni.Rows.Add(New Object() {"S", "Sì"})
      dttGestmatrIni.AcceptChanges()
      cbAr_gestmatrIni.DataSource = dttGestmatrIni
      cbAr_gestmatrIni.ValueMember = "cod"
      cbAr_gestmatrIni.DisplayMember = "val"
      cbAr_gestmatrIni.SelectedValue = "N"
      '----------------------------------------------------------------------------------
      Dim dttGestmatrFin As New DataTable()
      dttGestmatrFin.Columns.Add("cod", GetType(String))
      dttGestmatrFin.Columns.Add("val", GetType(String))
      dttGestmatrFin.Rows.Add(New Object() {"N", "No"})
      dttGestmatrFin.Rows.Add(New Object() {"S", "Sì"})
      dttGestmatrFin.AcceptChanges()
      cbAr_gestmatrFin.DataSource = dttGestmatrFin
      cbAr_gestmatrFin.ValueMember = "cod"
      cbAr_gestmatrFin.DisplayMember = "val"
      cbAr_gestmatrFin.SelectedValue = "S"
      '----------------------------------------------------------------------------------
      Dim dttGesubicIni As New DataTable()
      dttGesubicIni.Columns.Add("cod", GetType(String))
      dttGesubicIni.Columns.Add("val", GetType(String))
      dttGesubicIni.Rows.Add(New Object() {"N", "No"})
      dttGesubicIni.Rows.Add(New Object() {"S", "Sì"})
      dttGesubicIni.AcceptChanges()
      cbAr_gesubicIni.DataSource = dttGesubicIni
      cbAr_gesubicIni.ValueMember = "cod"
      cbAr_gesubicIni.DisplayMember = "val"
      cbAr_gesubicIni.SelectedValue = "N"
      '----------------------------------------------------------------------------------
      Dim dttGesubicFin As New DataTable()
      dttGesubicFin.Columns.Add("cod", GetType(String))
      dttGesubicFin.Columns.Add("val", GetType(String))
      dttGesubicFin.Rows.Add(New Object() {"N", "No"})
      dttGesubicFin.Rows.Add(New Object() {"S", "Sì"})
      dttGesubicFin.AcceptChanges()
      cbAr_gesubicFin.DataSource = dttGesubicFin
      cbAr_gesubicFin.ValueMember = "cod"
      cbAr_gesubicFin.DisplayMember = "val"
      cbAr_gesubicFin.SelectedValue = "S"
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Elabora() As Boolean
    Dim lRecords As Integer = 0
    Dim strMsg As String = ""
    Try
      '----------------------------------------------------------------------------------
      '--- Controlla se sono stati selezionati gli articoli
      '----------------------------------------------------------------------------------
      If oCleVqta.bSeleziona = False Then
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128570649268559489, "Attenzione!" & vbCrLf & _
          "Non sono stati selezionati gli articoli da elaborare." & vbCrLf & _
          "Procedere adesso con la selezione degli articoli?")) = Windows.Forms.DialogResult.No Then
          Return False
        Else
          cmdSeleziona_Click(cmdSeleziona, Nothing)
          If oCleVqta.bSeleziona = False Then Return False
        End If
      End If
      '----------------------------------------------------------------------------------
      If opAr_perqta.Checked = True Then
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128570676015723889, "Attenzione!" & vbCrLf & _
          "Nuovo moltiplicatore quantità/prezzo: " & "|" & edAr_perqta.Text & "|" & "." & vbCrLf & _
          "Confermare modifica?")) = Windows.Forms.DialogResult.No Then
          Return False
        End If
      End If
      '----------------------------------------------------------------------------------
      '--- Controlla la validità dei dati impostati in maschera
      '----------------------------------------------------------------------------------
      If (opAr_gescomm.Checked = True) And _
         (cbAr_gescommIni.SelectedValue = cbAr_gescommFin.SelectedValue) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128570623140672046, "Attenzione!" & vbCrLf & _
          "Il valore da sostituire nel campo 'Gestione Commesse'" & vbCrLf & _
          "è uguale al nuovo valore da sostituire." & vbCrLf & _
          "Elaborazione non possibile."))
        Return False
      End If
      '----------------------------------------------------------------------------------
      strMsg = oApp.Tr(Me, 129060371399624831, "Modifica del campo ")
      If opAr_perqta.Checked = True Then strMsg += oApp.Tr(Me, 129060371427438577, "'Moltiplicatore Quantità/Prezzo'")
      If opAr_gescomm.Checked = True Then strMsg += oApp.Tr(Me, 129060371449627071, "'Gestione per Commessa'")
      If opAr_geslotti.Checked = True Then strMsg += oApp.Tr(Me, 129060371472596850, "'Gestione Lotti'")
      If opAr_gestmatr.Checked = True Then strMsg += oApp.Tr(Me, 129060371493222774, "'Gestione Matricole'")
      If opAr_gesubic.Checked = True Then strMsg += oApp.Tr(Me, 129060371516817581, "'Gestione Ubicazione'")
      strMsg += oApp.Tr(Me, 129082851447595382, " in corso...")
      lbDescr.Text = strMsg
      '----------------------------------------------------------------------------------
      '--- Chiama il metodo dell'Entity
      '----------------------------------------------------------------------------------
      If oCleVqta.Elabora(DittaCorrente, opAr_perqta.Checked, NTSCDec(edAr_perqta.Text), opAr_gescomm.Checked, cbAr_gescommIni.SelectedValue, cbAr_gescommFin.SelectedValue, opAr_geslotti.Checked, cbAr_geslottiIni.SelectedValue, cbAr_geslottiFin.SelectedValue, opAr_gestmatr.Checked, cbAr_gestmatrIni.SelectedValue, cbAr_gestmatrFin.SelectedValue, opAr_gesubic.Checked, cbAr_gesubicIni.SelectedValue, cbAr_gesubicFin.SelectedValue, lRecords) = True Then
        lbDescr.Text = oApp.Tr(Me, 129082851717294964, "Nessuna operazione in corso")
        Me.Cursor = Cursors.Default
        If lRecords > 0 Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128570634002314900, "Elaborazione terminata correttamente."))
        End If
        Return True
      Else
        lbDescr.Text = oApp.Tr(Me, 129082851740264743, "Nessuna operazione in corso")
        Me.Cursor = Cursors.Default
        Return False
      End If
      '----------------------------------------------------------------------------------
      Me.Cursor = Cursors.WaitCursor
      '----------------------------------------------------------------------------------
      Return True
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      lbDescr.Text = oApp.Tr(Me, 129082851842925592, "Nessuna operazione in corso")
      '----------------------------------------------------------------------------------
      Me.Cursor = Cursors.Default
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Function

  Public Overridable Sub SettaControlli()
    Try
      '----------------------------------------------------------------------------------
      If opAr_perqta.Checked = True Then
        If lbAr_perqta.Enabled = False Then GctlSetVisEnab(lbAr_perqta, False)
        If edAr_perqta.Enabled = False Then
          GctlSetVisEnab(edAr_perqta, False)
          edAr_perqta.Focus()
        End If
      Else
        If lbAr_perqta.Enabled = True Then lbAr_perqta.Enabled = False
        If edAr_perqta.Enabled = True Then
          edAr_perqta.Text = "1"
          edAr_perqta.Enabled = False
        End If
      End If
      '----------------------------------------------------------------------------------
      If opAr_gescomm.Checked = True Then
        If lbAr_gescommIni.Enabled = False Then GctlSetVisEnab(lbAr_gescommIni, False)
        If lbAr_gescommFin.Enabled = False Then GctlSetVisEnab(lbAr_gescommFin, False)
        If cbAr_gescommIni.Enabled = False Then GctlSetVisEnab(cbAr_gescommIni, False)
        If cbAr_gescommFin.Enabled = False Then
          GctlSetVisEnab(cbAr_gescommFin, False)
          cbAr_gescommIni.Focus()
        End If
      Else
        If lbAr_gescommIni.Enabled = True Then lbAr_gescommIni.Enabled = False
        If lbAr_gescommFin.Enabled = True Then lbAr_gescommFin.Enabled = False
        If cbAr_gescommIni.Enabled = True Then
          cbAr_gescommIni.SelectedValue = "N"
          cbAr_gescommIni.Enabled = False
        End If
        If cbAr_gescommFin.Enabled = True Then
          cbAr_gescommFin.SelectedValue = "O"
          cbAr_gescommFin.Enabled = False
        End If
      End If
      '----------------------------------------------------------------------------------
      If opAr_geslotti.Checked = True Then
        If lbAr_geslottiIni.Enabled = False Then GctlSetVisEnab(lbAr_geslottiIni, False)
        If lbAr_geslottiFin.Enabled = False Then GctlSetVisEnab(lbAr_geslottiFin, False)
        If cbAr_geslottiIni.Enabled = False Then
          GctlSetVisEnab(cbAr_geslottiIni, False)
          cbAr_geslottiIni.Focus()
        End If
      Else
        If lbAr_geslottiIni.Enabled = True Then lbAr_geslottiIni.Enabled = False
        If lbAr_geslottiFin.Enabled = True Then lbAr_geslottiFin.Enabled = False
        If cbAr_geslottiIni.Enabled = True Then
          cbAr_geslottiIni.SelectedValue = "N"
          cbAr_geslottiIni.Enabled = False
        End If
        If cbAr_geslottiFin.Enabled = True Then cbAr_geslottiFin.SelectedValue = "S"
      End If
      '----------------------------------------------------------------------------------
      If opAr_gestmatr.Checked = True Then
        If lbAr_gestmatrIni.Enabled = False Then GctlSetVisEnab(lbAr_gestmatrIni, False)
        If lbAr_gestmatrFin.Enabled = False Then GctlSetVisEnab(lbAr_gestmatrFin, False)
        If cbAr_gestmatrIni.Enabled = False Then
          GctlSetVisEnab(cbAr_gestmatrIni, False)
          cbAr_gestmatrIni.Focus()
        End If
      Else
        If lbAr_gestmatrIni.Enabled = True Then lbAr_gestmatrIni.Enabled = False
        If lbAr_gestmatrFin.Enabled = True Then lbAr_gestmatrFin.Enabled = False
        If cbAr_gestmatrIni.Enabled = True Then
          cbAr_gestmatrIni.SelectedValue = "N"
          cbAr_gestmatrIni.Enabled = False
        End If
        If cbAr_gestmatrFin.Enabled = True Then cbAr_gestmatrFin.SelectedValue = "S"
      End If
      '----------------------------------------------------------------------------------
      If opAr_gesubic.Checked = True Then
        If lbAr_gesubicIni.Enabled = False Then GctlSetVisEnab(lbAr_gesubicIni, False)
        If lbAr_gesubicFin.Enabled = False Then GctlSetVisEnab(lbAr_gesubicFin, False)
        If cbAr_gesubicIni.Enabled = False Then
          GctlSetVisEnab(cbAr_gesubicIni, False)
          cbAr_gesubicIni.Focus()
        End If
      Else
        If lbAr_gesubicIni.Enabled = True Then lbAr_gesubicIni.Enabled = False
        If lbAr_gesubicFin.Enabled = True Then lbAr_gesubicFin.Enabled = False
        If cbAr_gesubicIni.Enabled = True Then
          cbAr_gesubicIni.SelectedValue = "N"
          cbAr_gesubicIni.Enabled = False
        End If
        If cbAr_gesubicFin.Enabled = True Then cbAr_gesubicFin.SelectedValue = "S"
      End If
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub
#End Region

End Class
