Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVEBOTR

#Region "Moduli"
  Private Moduli_P As Integer = CLN__STD.bsModVE + CLN__STD.bsModMG
  Private ModuliExt_P As Integer = CLN__STD.bsModExtMGE + CLN__STD.bsModExtPAR
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
#End Region

#Region "Variabili"
  Public oCleBotr As CLEVEBOTR
  Public dsBotr As DataSet
  Public dcBotr As BindingSource = New BindingSource
  Public oCallParams As CLE__CLDP

  Public bOnLoading As Boolean = False

  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents pnBotr As NTSInformatica.NTSPanel
  Public WithEvents edTb_botrita As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_botrita As NTSInformatica.NTSLabel
  Public WithEvents edTb_botrest As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_botrest As NTSInformatica.NTSLabel
  Public WithEvents edTb_minimo As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_minimo As NTSInformatica.NTSLabel
  Public WithEvents edTb_arrot As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_arrot As NTSInformatica.NTSLabel
  Public WithEvents edTb_impftes As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_impftes As NTSInformatica.NTSLabel
  Public WithEvents edTb_impboes As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_impboes As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codesb_1 As NTSInformatica.NTSLabel
  Public WithEvents edTb_codesb_1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_codesb_1 As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codesb_2 As NTSInformatica.NTSLabel
  Public WithEvents edTb_codesb_2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_codesb_2 As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codesb_3 As NTSInformatica.NTSLabel
  Public WithEvents edTb_codesb_3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_codesb_3 As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codesb_4 As NTSInformatica.NTSLabel
  Public WithEvents edTb_codesb_4 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_codesb_4 As NTSInformatica.NTSLabel
  Public WithEvents fmFattureEsenti As NTSInformatica.NTSGroupBox
  Public WithEvents fmTratte As NTSInformatica.NTSGroupBox
  Public WithEvents lbXx_codesb_8 As NTSInformatica.NTSLabel
  Public WithEvents edTb_codesb_8 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_codesb_8 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_codesb_5 As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codesb_7 As NTSInformatica.NTSLabel
  Public WithEvents edTb_codesb_5 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_codesb_7 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_codesb_5 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_codesb_7 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_codesb_6 As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codesb_6 As NTSInformatica.NTSLabel
  Public WithEvents edTb_codesb_6 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbInfo As NTSInformatica.NTSLabel
  Public WithEvents ckCalcolaBolli As NTSInformatica.NTSCheckBox
  Public WithEvents lbTipoCalcoloBolli As NTSInformatica.NTSLabel
  Public WithEvents cbTipoCalcoloBolli As NTSInformatica.NTSComboBox
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
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
    Dim objTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNVEBOTR", "BEVEBOTR", objTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128050437330625000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleBotr = CType(objTmp, CLEVEBOTR)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNVEBOTR", strRemoteServer, strRemotePort)
    AddHandler oCleBotr.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleBotr.Init(oApp, NTSScript, oMenu.oCleComm, "TABBOTR", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMVEBOTR))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnBotr = New NTSInformatica.NTSPanel
    Me.fmFattureEsenti = New NTSInformatica.NTSGroupBox
    Me.lbTipoCalcoloBolli = New NTSInformatica.NTSLabel
    Me.cbTipoCalcoloBolli = New NTSInformatica.NTSComboBox
    Me.lbInfo = New NTSInformatica.NTSLabel
    Me.ckCalcolaBolli = New NTSInformatica.NTSCheckBox
    Me.lbXx_codesb_8 = New NTSInformatica.NTSLabel
    Me.edTb_codesb_8 = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_codesb_8 = New NTSInformatica.NTSLabel
    Me.lbTb_codesb_5 = New NTSInformatica.NTSLabel
    Me.lbXx_codesb_7 = New NTSInformatica.NTSLabel
    Me.edTb_codesb_5 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_codesb_7 = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_codesb_5 = New NTSInformatica.NTSLabel
    Me.lbTb_codesb_7 = New NTSInformatica.NTSLabel
    Me.lbTb_codesb_6 = New NTSInformatica.NTSLabel
    Me.lbXx_codesb_6 = New NTSInformatica.NTSLabel
    Me.edTb_codesb_6 = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_impftes = New NTSInformatica.NTSLabel
    Me.edTb_impftes = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_codesb_4 = New NTSInformatica.NTSLabel
    Me.lbTb_impboes = New NTSInformatica.NTSLabel
    Me.edTb_codesb_4 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_impboes = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_codesb_4 = New NTSInformatica.NTSLabel
    Me.lbTb_codesb_1 = New NTSInformatica.NTSLabel
    Me.lbXx_codesb_3 = New NTSInformatica.NTSLabel
    Me.edTb_codesb_1 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_codesb_3 = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_codesb_1 = New NTSInformatica.NTSLabel
    Me.lbTb_codesb_3 = New NTSInformatica.NTSLabel
    Me.lbTb_codesb_2 = New NTSInformatica.NTSLabel
    Me.lbXx_codesb_2 = New NTSInformatica.NTSLabel
    Me.edTb_codesb_2 = New NTSInformatica.NTSTextBoxNum
    Me.fmTratte = New NTSInformatica.NTSGroupBox
    Me.lbTb_botrita = New NTSInformatica.NTSLabel
    Me.edTb_botrita = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_botrest = New NTSInformatica.NTSLabel
    Me.edTb_botrest = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_minimo = New NTSInformatica.NTSLabel
    Me.edTb_minimo = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_arrot = New NTSInformatica.NTSLabel
    Me.edTb_arrot = New NTSInformatica.NTSTextBoxNum
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnBotr, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBotr.SuspendLayout()
    CType(Me.fmFattureEsenti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmFattureEsenti.SuspendLayout()
    CType(Me.cbTipoCalcoloBolli.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckCalcolaBolli.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codesb_8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codesb_5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codesb_7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codesb_6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_impftes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codesb_4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_impboes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codesb_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codesb_3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codesb_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmTratte, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmTratte.SuspendLayout()
    CType(Me.edTb_botrita.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_botrest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_minimo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_arrot.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
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
    'NtsBarManager1
    '
    Me.NtsBarManager1.AllowCustomization = False
    Me.NtsBarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.tlbMain})
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlTop)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlBottom)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlLeft)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlRight)
    Me.NtsBarManager1.Form = Me
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbRipristina, Me.tlbZoom, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 5
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.GlyphPath = ""
    Me.tlbSalva.Id = 0
    Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.GlyphPath = ""
    Me.tlbRipristina.Id = 1
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.GlyphPath = ""
    Me.tlbZoom.Id = 2
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 3
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 4
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnBotr
    '
    Me.pnBotr.AllowDrop = True
    Me.pnBotr.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBotr.Appearance.Options.UseBackColor = True
    Me.pnBotr.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBotr.Controls.Add(Me.fmFattureEsenti)
    Me.pnBotr.Controls.Add(Me.fmTratte)
    Me.pnBotr.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBotr.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnBotr.Location = New System.Drawing.Point(0, 26)
    Me.pnBotr.Name = "pnBotr"
    Me.pnBotr.NTSActiveTrasparency = True
    Me.pnBotr.Size = New System.Drawing.Size(678, 351)
    Me.pnBotr.TabIndex = 0
    Me.pnBotr.Text = "NtsPanel1"
    '
    'fmFattureEsenti
    '
    Me.fmFattureEsenti.AllowDrop = True
    Me.fmFattureEsenti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmFattureEsenti.Appearance.Options.UseBackColor = True
    Me.fmFattureEsenti.Controls.Add(Me.lbTipoCalcoloBolli)
    Me.fmFattureEsenti.Controls.Add(Me.cbTipoCalcoloBolli)
    Me.fmFattureEsenti.Controls.Add(Me.lbInfo)
    Me.fmFattureEsenti.Controls.Add(Me.ckCalcolaBolli)
    Me.fmFattureEsenti.Controls.Add(Me.lbXx_codesb_8)
    Me.fmFattureEsenti.Controls.Add(Me.edTb_codesb_8)
    Me.fmFattureEsenti.Controls.Add(Me.lbTb_codesb_8)
    Me.fmFattureEsenti.Controls.Add(Me.lbTb_codesb_5)
    Me.fmFattureEsenti.Controls.Add(Me.lbXx_codesb_7)
    Me.fmFattureEsenti.Controls.Add(Me.edTb_codesb_5)
    Me.fmFattureEsenti.Controls.Add(Me.edTb_codesb_7)
    Me.fmFattureEsenti.Controls.Add(Me.lbXx_codesb_5)
    Me.fmFattureEsenti.Controls.Add(Me.lbTb_codesb_7)
    Me.fmFattureEsenti.Controls.Add(Me.lbTb_codesb_6)
    Me.fmFattureEsenti.Controls.Add(Me.lbXx_codesb_6)
    Me.fmFattureEsenti.Controls.Add(Me.edTb_codesb_6)
    Me.fmFattureEsenti.Controls.Add(Me.lbTb_impftes)
    Me.fmFattureEsenti.Controls.Add(Me.edTb_impftes)
    Me.fmFattureEsenti.Controls.Add(Me.lbXx_codesb_4)
    Me.fmFattureEsenti.Controls.Add(Me.lbTb_impboes)
    Me.fmFattureEsenti.Controls.Add(Me.edTb_codesb_4)
    Me.fmFattureEsenti.Controls.Add(Me.edTb_impboes)
    Me.fmFattureEsenti.Controls.Add(Me.lbTb_codesb_4)
    Me.fmFattureEsenti.Controls.Add(Me.lbTb_codesb_1)
    Me.fmFattureEsenti.Controls.Add(Me.lbXx_codesb_3)
    Me.fmFattureEsenti.Controls.Add(Me.edTb_codesb_1)
    Me.fmFattureEsenti.Controls.Add(Me.edTb_codesb_3)
    Me.fmFattureEsenti.Controls.Add(Me.lbXx_codesb_1)
    Me.fmFattureEsenti.Controls.Add(Me.lbTb_codesb_3)
    Me.fmFattureEsenti.Controls.Add(Me.lbTb_codesb_2)
    Me.fmFattureEsenti.Controls.Add(Me.lbXx_codesb_2)
    Me.fmFattureEsenti.Controls.Add(Me.edTb_codesb_2)
    Me.fmFattureEsenti.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmFattureEsenti.Location = New System.Drawing.Point(226, 3)
    Me.fmFattureEsenti.Name = "fmFattureEsenti"
    Me.fmFattureEsenti.Size = New System.Drawing.Size(442, 340)
    Me.fmFattureEsenti.TabIndex = 121
    Me.fmFattureEsenti.Text = "Fatture Esenti:"
    '
    'lbTipoCalcoloBolli
    '
    Me.lbTipoCalcoloBolli.AutoSize = True
    Me.lbTipoCalcoloBolli.BackColor = System.Drawing.Color.Transparent
    Me.lbTipoCalcoloBolli.Location = New System.Drawing.Point(5, 264)
    Me.lbTipoCalcoloBolli.Name = "lbTipoCalcoloBolli"
    Me.lbTipoCalcoloBolli.NTSDbField = ""
    Me.lbTipoCalcoloBolli.Size = New System.Drawing.Size(381, 13)
    Me.lbTipoCalcoloBolli.TabIndex = 514
    Me.lbTipoCalcoloBolli.Text = "Come operare il calcolo bolli in Offerte/Ordini/Documenti/Fatturazione Differite"
    Me.lbTipoCalcoloBolli.Tooltip = ""
    Me.lbTipoCalcoloBolli.UseMnemonic = False
    '
    'cbTipoCalcoloBolli
    '
    Me.cbTipoCalcoloBolli.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipoCalcoloBolli.DataSource = Nothing
    Me.cbTipoCalcoloBolli.DisplayMember = ""
    Me.cbTipoCalcoloBolli.Location = New System.Drawing.Point(8, 280)
    Me.cbTipoCalcoloBolli.Name = "cbTipoCalcoloBolli"
    Me.cbTipoCalcoloBolli.NTSDbField = ""
    Me.cbTipoCalcoloBolli.Properties.AutoHeight = False
    Me.cbTipoCalcoloBolli.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipoCalcoloBolli.Properties.DropDownRows = 30
    Me.cbTipoCalcoloBolli.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipoCalcoloBolli.SelectedValue = ""
    Me.cbTipoCalcoloBolli.Size = New System.Drawing.Size(416, 20)
    Me.cbTipoCalcoloBolli.TabIndex = 513
    Me.cbTipoCalcoloBolli.ValueMember = ""
    '
    'lbInfo
    '
    Me.lbInfo.AutoSize = True
    Me.lbInfo.BackColor = System.Drawing.Color.Transparent
    Me.lbInfo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbInfo.Location = New System.Drawing.Point(5, 309)
    Me.lbInfo.Name = "lbInfo"
    Me.lbInfo.NTSDbField = ""
    Me.lbInfo.Size = New System.Drawing.Size(321, 26)
    Me.lbInfo.TabIndex = 511
    Me.lbInfo.Text = "ATTENZIONE!" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Le due impostazioni suddette, valgono per tutte le ditte"
    Me.lbInfo.Tooltip = ""
    Me.lbInfo.UseMnemonic = False
    '
    'ckCalcolaBolli
    '
    Me.ckCalcolaBolli.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckCalcolaBolli.Location = New System.Drawing.Point(8, 242)
    Me.ckCalcolaBolli.Name = "ckCalcolaBolli"
    Me.ckCalcolaBolli.NTSCheckValue = "S"
    Me.ckCalcolaBolli.NTSUnCheckValue = "N"
    Me.ckCalcolaBolli.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckCalcolaBolli.Properties.Appearance.Options.UseBackColor = True
    Me.ckCalcolaBolli.Properties.AutoHeight = False
    Me.ckCalcolaBolli.Properties.Caption = "Calcola i bolli ignorando il codice esenzione indicato in testata documento"
    Me.ckCalcolaBolli.Size = New System.Drawing.Size(380, 19)
    Me.ckCalcolaBolli.TabIndex = 510
    '
    'lbXx_codesb_8
    '
    Me.lbXx_codesb_8.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codesb_8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codesb_8.Location = New System.Drawing.Point(216, 219)
    Me.lbXx_codesb_8.Name = "lbXx_codesb_8"
    Me.lbXx_codesb_8.NTSDbField = ""
    Me.lbXx_codesb_8.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_codesb_8.TabIndex = 132
    Me.lbXx_codesb_8.Tooltip = ""
    Me.lbXx_codesb_8.UseMnemonic = False
    '
    'edTb_codesb_8
    '
    Me.edTb_codesb_8.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codesb_8.Location = New System.Drawing.Point(122, 219)
    Me.edTb_codesb_8.Name = "edTb_codesb_8"
    Me.edTb_codesb_8.NTSDbField = ""
    Me.edTb_codesb_8.NTSFormat = "0"
    Me.edTb_codesb_8.NTSForzaVisZoom = False
    Me.edTb_codesb_8.NTSOldValue = ""
    Me.edTb_codesb_8.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_codesb_8.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_codesb_8.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codesb_8.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codesb_8.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codesb_8.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codesb_8.Properties.AutoHeight = False
    Me.edTb_codesb_8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codesb_8.Properties.MaxLength = 65536
    Me.edTb_codesb_8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codesb_8.Size = New System.Drawing.Size(88, 20)
    Me.edTb_codesb_8.TabIndex = 124
    '
    'lbTb_codesb_8
    '
    Me.lbTb_codesb_8.AutoSize = True
    Me.lbTb_codesb_8.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_codesb_8.Location = New System.Drawing.Point(5, 222)
    Me.lbTb_codesb_8.Name = "lbTb_codesb_8"
    Me.lbTb_codesb_8.NTSDbField = ""
    Me.lbTb_codesb_8.Size = New System.Drawing.Size(111, 13)
    Me.lbTb_codesb_8.TabIndex = 131
    Me.lbTb_codesb_8.Text = "Codice Iva esente (8)"
    Me.lbTb_codesb_8.Tooltip = ""
    Me.lbTb_codesb_8.UseMnemonic = False
    '
    'lbTb_codesb_5
    '
    Me.lbTb_codesb_5.AutoSize = True
    Me.lbTb_codesb_5.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_codesb_5.Location = New System.Drawing.Point(5, 156)
    Me.lbTb_codesb_5.Name = "lbTb_codesb_5"
    Me.lbTb_codesb_5.NTSDbField = ""
    Me.lbTb_codesb_5.Size = New System.Drawing.Size(111, 13)
    Me.lbTb_codesb_5.TabIndex = 125
    Me.lbTb_codesb_5.Text = "Codice Iva esente (5)"
    Me.lbTb_codesb_5.Tooltip = ""
    Me.lbTb_codesb_5.UseMnemonic = False
    '
    'lbXx_codesb_7
    '
    Me.lbXx_codesb_7.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codesb_7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codesb_7.Location = New System.Drawing.Point(216, 197)
    Me.lbXx_codesb_7.Name = "lbXx_codesb_7"
    Me.lbXx_codesb_7.NTSDbField = ""
    Me.lbXx_codesb_7.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_codesb_7.TabIndex = 130
    Me.lbXx_codesb_7.Tooltip = ""
    Me.lbXx_codesb_7.UseMnemonic = False
    '
    'edTb_codesb_5
    '
    Me.edTb_codesb_5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codesb_5.Location = New System.Drawing.Point(122, 153)
    Me.edTb_codesb_5.Name = "edTb_codesb_5"
    Me.edTb_codesb_5.NTSDbField = ""
    Me.edTb_codesb_5.NTSFormat = "0"
    Me.edTb_codesb_5.NTSForzaVisZoom = False
    Me.edTb_codesb_5.NTSOldValue = ""
    Me.edTb_codesb_5.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_codesb_5.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_codesb_5.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codesb_5.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codesb_5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codesb_5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codesb_5.Properties.AutoHeight = False
    Me.edTb_codesb_5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codesb_5.Properties.MaxLength = 65536
    Me.edTb_codesb_5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codesb_5.Size = New System.Drawing.Size(88, 20)
    Me.edTb_codesb_5.TabIndex = 123
    '
    'edTb_codesb_7
    '
    Me.edTb_codesb_7.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codesb_7.Location = New System.Drawing.Point(122, 197)
    Me.edTb_codesb_7.Name = "edTb_codesb_7"
    Me.edTb_codesb_7.NTSDbField = ""
    Me.edTb_codesb_7.NTSFormat = "0"
    Me.edTb_codesb_7.NTSForzaVisZoom = False
    Me.edTb_codesb_7.NTSOldValue = ""
    Me.edTb_codesb_7.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_codesb_7.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_codesb_7.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codesb_7.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codesb_7.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codesb_7.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codesb_7.Properties.AutoHeight = False
    Me.edTb_codesb_7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codesb_7.Properties.MaxLength = 65536
    Me.edTb_codesb_7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codesb_7.Size = New System.Drawing.Size(88, 20)
    Me.edTb_codesb_7.TabIndex = 122
    '
    'lbXx_codesb_5
    '
    Me.lbXx_codesb_5.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codesb_5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codesb_5.Location = New System.Drawing.Point(216, 153)
    Me.lbXx_codesb_5.Name = "lbXx_codesb_5"
    Me.lbXx_codesb_5.NTSDbField = ""
    Me.lbXx_codesb_5.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_codesb_5.TabIndex = 126
    Me.lbXx_codesb_5.Tooltip = ""
    Me.lbXx_codesb_5.UseMnemonic = False
    '
    'lbTb_codesb_7
    '
    Me.lbTb_codesb_7.AutoSize = True
    Me.lbTb_codesb_7.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_codesb_7.Location = New System.Drawing.Point(5, 200)
    Me.lbTb_codesb_7.Name = "lbTb_codesb_7"
    Me.lbTb_codesb_7.NTSDbField = ""
    Me.lbTb_codesb_7.Size = New System.Drawing.Size(111, 13)
    Me.lbTb_codesb_7.TabIndex = 129
    Me.lbTb_codesb_7.Text = "Codice Iva esente (7)"
    Me.lbTb_codesb_7.Tooltip = ""
    Me.lbTb_codesb_7.UseMnemonic = False
    '
    'lbTb_codesb_6
    '
    Me.lbTb_codesb_6.AutoSize = True
    Me.lbTb_codesb_6.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_codesb_6.Location = New System.Drawing.Point(5, 178)
    Me.lbTb_codesb_6.Name = "lbTb_codesb_6"
    Me.lbTb_codesb_6.NTSDbField = ""
    Me.lbTb_codesb_6.Size = New System.Drawing.Size(111, 13)
    Me.lbTb_codesb_6.TabIndex = 127
    Me.lbTb_codesb_6.Text = "Codice Iva esente (6)"
    Me.lbTb_codesb_6.Tooltip = ""
    Me.lbTb_codesb_6.UseMnemonic = False
    '
    'lbXx_codesb_6
    '
    Me.lbXx_codesb_6.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codesb_6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codesb_6.Location = New System.Drawing.Point(216, 175)
    Me.lbXx_codesb_6.Name = "lbXx_codesb_6"
    Me.lbXx_codesb_6.NTSDbField = ""
    Me.lbXx_codesb_6.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_codesb_6.TabIndex = 128
    Me.lbXx_codesb_6.Tooltip = ""
    Me.lbXx_codesb_6.UseMnemonic = False
    '
    'edTb_codesb_6
    '
    Me.edTb_codesb_6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codesb_6.Location = New System.Drawing.Point(122, 175)
    Me.edTb_codesb_6.Name = "edTb_codesb_6"
    Me.edTb_codesb_6.NTSDbField = ""
    Me.edTb_codesb_6.NTSFormat = "0"
    Me.edTb_codesb_6.NTSForzaVisZoom = False
    Me.edTb_codesb_6.NTSOldValue = ""
    Me.edTb_codesb_6.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_codesb_6.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_codesb_6.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codesb_6.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codesb_6.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codesb_6.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codesb_6.Properties.AutoHeight = False
    Me.edTb_codesb_6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codesb_6.Properties.MaxLength = 65536
    Me.edTb_codesb_6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codesb_6.Size = New System.Drawing.Size(88, 20)
    Me.edTb_codesb_6.TabIndex = 121
    '
    'lbTb_impftes
    '
    Me.lbTb_impftes.AutoSize = True
    Me.lbTb_impftes.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_impftes.Location = New System.Drawing.Point(5, 24)
    Me.lbTb_impftes.Name = "lbTb_impftes"
    Me.lbTb_impftes.NTSDbField = ""
    Me.lbTb_impftes.Size = New System.Drawing.Size(80, 13)
    Me.lbTb_impftes.TabIndex = 104
    Me.lbTb_impftes.Text = "Importo minimo"
    Me.lbTb_impftes.Tooltip = ""
    Me.lbTb_impftes.UseMnemonic = False
    '
    'edTb_impftes
    '
    Me.edTb_impftes.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_impftes.Location = New System.Drawing.Point(122, 21)
    Me.edTb_impftes.Name = "edTb_impftes"
    Me.edTb_impftes.NTSDbField = ""
    Me.edTb_impftes.NTSFormat = "0"
    Me.edTb_impftes.NTSForzaVisZoom = False
    Me.edTb_impftes.NTSOldValue = ""
    Me.edTb_impftes.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_impftes.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_impftes.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_impftes.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_impftes.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_impftes.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_impftes.Properties.AutoHeight = False
    Me.edTb_impftes.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_impftes.Properties.MaxLength = 65536
    Me.edTb_impftes.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_impftes.Size = New System.Drawing.Size(88, 20)
    Me.edTb_impftes.TabIndex = 1
    '
    'lbXx_codesb_4
    '
    Me.lbXx_codesb_4.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codesb_4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codesb_4.Location = New System.Drawing.Point(216, 131)
    Me.lbXx_codesb_4.Name = "lbXx_codesb_4"
    Me.lbXx_codesb_4.NTSDbField = ""
    Me.lbXx_codesb_4.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_codesb_4.TabIndex = 120
    Me.lbXx_codesb_4.Tooltip = ""
    Me.lbXx_codesb_4.UseMnemonic = False
    '
    'lbTb_impboes
    '
    Me.lbTb_impboes.AutoSize = True
    Me.lbTb_impboes.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_impboes.Location = New System.Drawing.Point(5, 46)
    Me.lbTb_impboes.Name = "lbTb_impboes"
    Me.lbTb_impboes.NTSDbField = ""
    Me.lbTb_impboes.Size = New System.Drawing.Size(63, 13)
    Me.lbTb_impboes.TabIndex = 107
    Me.lbTb_impboes.Text = "Bollo di EUR"
    Me.lbTb_impboes.Tooltip = ""
    Me.lbTb_impboes.UseMnemonic = False
    '
    'edTb_codesb_4
    '
    Me.edTb_codesb_4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codesb_4.Location = New System.Drawing.Point(122, 131)
    Me.edTb_codesb_4.Name = "edTb_codesb_4"
    Me.edTb_codesb_4.NTSDbField = ""
    Me.edTb_codesb_4.NTSFormat = "0"
    Me.edTb_codesb_4.NTSForzaVisZoom = False
    Me.edTb_codesb_4.NTSOldValue = ""
    Me.edTb_codesb_4.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_codesb_4.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_codesb_4.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codesb_4.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codesb_4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codesb_4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codesb_4.Properties.AutoHeight = False
    Me.edTb_codesb_4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codesb_4.Properties.MaxLength = 65536
    Me.edTb_codesb_4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codesb_4.Size = New System.Drawing.Size(88, 20)
    Me.edTb_codesb_4.TabIndex = 20
    '
    'edTb_impboes
    '
    Me.edTb_impboes.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_impboes.Location = New System.Drawing.Point(122, 43)
    Me.edTb_impboes.Name = "edTb_impboes"
    Me.edTb_impboes.NTSDbField = ""
    Me.edTb_impboes.NTSFormat = "0"
    Me.edTb_impboes.NTSForzaVisZoom = False
    Me.edTb_impboes.NTSOldValue = ""
    Me.edTb_impboes.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_impboes.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_impboes.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_impboes.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_impboes.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_impboes.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_impboes.Properties.AutoHeight = False
    Me.edTb_impboes.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_impboes.Properties.MaxLength = 65536
    Me.edTb_impboes.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_impboes.Size = New System.Drawing.Size(88, 20)
    Me.edTb_impboes.TabIndex = 6
    '
    'lbTb_codesb_4
    '
    Me.lbTb_codesb_4.AutoSize = True
    Me.lbTb_codesb_4.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_codesb_4.Location = New System.Drawing.Point(5, 134)
    Me.lbTb_codesb_4.Name = "lbTb_codesb_4"
    Me.lbTb_codesb_4.NTSDbField = ""
    Me.lbTb_codesb_4.Size = New System.Drawing.Size(111, 13)
    Me.lbTb_codesb_4.TabIndex = 119
    Me.lbTb_codesb_4.Text = "Codice Iva esente (4)"
    Me.lbTb_codesb_4.Tooltip = ""
    Me.lbTb_codesb_4.UseMnemonic = False
    '
    'lbTb_codesb_1
    '
    Me.lbTb_codesb_1.AutoSize = True
    Me.lbTb_codesb_1.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_codesb_1.Location = New System.Drawing.Point(5, 68)
    Me.lbTb_codesb_1.Name = "lbTb_codesb_1"
    Me.lbTb_codesb_1.NTSDbField = ""
    Me.lbTb_codesb_1.Size = New System.Drawing.Size(111, 13)
    Me.lbTb_codesb_1.TabIndex = 110
    Me.lbTb_codesb_1.Text = "Codice Iva esente (1)"
    Me.lbTb_codesb_1.Tooltip = ""
    Me.lbTb_codesb_1.UseMnemonic = False
    '
    'lbXx_codesb_3
    '
    Me.lbXx_codesb_3.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codesb_3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codesb_3.Location = New System.Drawing.Point(216, 109)
    Me.lbXx_codesb_3.Name = "lbXx_codesb_3"
    Me.lbXx_codesb_3.NTSDbField = ""
    Me.lbXx_codesb_3.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_codesb_3.TabIndex = 117
    Me.lbXx_codesb_3.Tooltip = ""
    Me.lbXx_codesb_3.UseMnemonic = False
    '
    'edTb_codesb_1
    '
    Me.edTb_codesb_1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codesb_1.Location = New System.Drawing.Point(122, 65)
    Me.edTb_codesb_1.Name = "edTb_codesb_1"
    Me.edTb_codesb_1.NTSDbField = ""
    Me.edTb_codesb_1.NTSFormat = "0"
    Me.edTb_codesb_1.NTSForzaVisZoom = False
    Me.edTb_codesb_1.NTSOldValue = ""
    Me.edTb_codesb_1.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_codesb_1.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_codesb_1.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codesb_1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codesb_1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codesb_1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codesb_1.Properties.AutoHeight = False
    Me.edTb_codesb_1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codesb_1.Properties.MaxLength = 65536
    Me.edTb_codesb_1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codesb_1.Size = New System.Drawing.Size(88, 20)
    Me.edTb_codesb_1.TabIndex = 17
    '
    'edTb_codesb_3
    '
    Me.edTb_codesb_3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codesb_3.Location = New System.Drawing.Point(122, 109)
    Me.edTb_codesb_3.Name = "edTb_codesb_3"
    Me.edTb_codesb_3.NTSDbField = ""
    Me.edTb_codesb_3.NTSFormat = "0"
    Me.edTb_codesb_3.NTSForzaVisZoom = False
    Me.edTb_codesb_3.NTSOldValue = ""
    Me.edTb_codesb_3.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_codesb_3.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_codesb_3.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codesb_3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codesb_3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codesb_3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codesb_3.Properties.AutoHeight = False
    Me.edTb_codesb_3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codesb_3.Properties.MaxLength = 65536
    Me.edTb_codesb_3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codesb_3.Size = New System.Drawing.Size(88, 20)
    Me.edTb_codesb_3.TabIndex = 9
    '
    'lbXx_codesb_1
    '
    Me.lbXx_codesb_1.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codesb_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codesb_1.Location = New System.Drawing.Point(216, 65)
    Me.lbXx_codesb_1.Name = "lbXx_codesb_1"
    Me.lbXx_codesb_1.NTSDbField = ""
    Me.lbXx_codesb_1.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_codesb_1.TabIndex = 111
    Me.lbXx_codesb_1.Tooltip = ""
    Me.lbXx_codesb_1.UseMnemonic = False
    '
    'lbTb_codesb_3
    '
    Me.lbTb_codesb_3.AutoSize = True
    Me.lbTb_codesb_3.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_codesb_3.Location = New System.Drawing.Point(5, 112)
    Me.lbTb_codesb_3.Name = "lbTb_codesb_3"
    Me.lbTb_codesb_3.NTSDbField = ""
    Me.lbTb_codesb_3.Size = New System.Drawing.Size(111, 13)
    Me.lbTb_codesb_3.TabIndex = 116
    Me.lbTb_codesb_3.Text = "Codice Iva esente (3)"
    Me.lbTb_codesb_3.Tooltip = ""
    Me.lbTb_codesb_3.UseMnemonic = False
    '
    'lbTb_codesb_2
    '
    Me.lbTb_codesb_2.AutoSize = True
    Me.lbTb_codesb_2.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_codesb_2.Location = New System.Drawing.Point(5, 90)
    Me.lbTb_codesb_2.Name = "lbTb_codesb_2"
    Me.lbTb_codesb_2.NTSDbField = ""
    Me.lbTb_codesb_2.Size = New System.Drawing.Size(111, 13)
    Me.lbTb_codesb_2.TabIndex = 113
    Me.lbTb_codesb_2.Text = "Codice Iva esente (2)"
    Me.lbTb_codesb_2.Tooltip = ""
    Me.lbTb_codesb_2.UseMnemonic = False
    '
    'lbXx_codesb_2
    '
    Me.lbXx_codesb_2.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codesb_2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codesb_2.Location = New System.Drawing.Point(216, 87)
    Me.lbXx_codesb_2.Name = "lbXx_codesb_2"
    Me.lbXx_codesb_2.NTSDbField = ""
    Me.lbXx_codesb_2.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_codesb_2.TabIndex = 114
    Me.lbXx_codesb_2.Tooltip = ""
    Me.lbXx_codesb_2.UseMnemonic = False
    '
    'edTb_codesb_2
    '
    Me.edTb_codesb_2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codesb_2.Location = New System.Drawing.Point(122, 87)
    Me.edTb_codesb_2.Name = "edTb_codesb_2"
    Me.edTb_codesb_2.NTSDbField = ""
    Me.edTb_codesb_2.NTSFormat = "0"
    Me.edTb_codesb_2.NTSForzaVisZoom = False
    Me.edTb_codesb_2.NTSOldValue = ""
    Me.edTb_codesb_2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_codesb_2.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_codesb_2.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codesb_2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codesb_2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codesb_2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codesb_2.Properties.AutoHeight = False
    Me.edTb_codesb_2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codesb_2.Properties.MaxLength = 65536
    Me.edTb_codesb_2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codesb_2.Size = New System.Drawing.Size(88, 20)
    Me.edTb_codesb_2.TabIndex = 1
    '
    'fmTratte
    '
    Me.fmTratte.AllowDrop = True
    Me.fmTratte.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmTratte.Appearance.Options.UseBackColor = True
    Me.fmTratte.Controls.Add(Me.lbTb_botrita)
    Me.fmTratte.Controls.Add(Me.edTb_botrita)
    Me.fmTratte.Controls.Add(Me.lbTb_botrest)
    Me.fmTratte.Controls.Add(Me.edTb_botrest)
    Me.fmTratte.Controls.Add(Me.lbTb_minimo)
    Me.fmTratte.Controls.Add(Me.edTb_minimo)
    Me.fmTratte.Controls.Add(Me.lbTb_arrot)
    Me.fmTratte.Controls.Add(Me.edTb_arrot)
    Me.fmTratte.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmTratte.Location = New System.Drawing.Point(12, 3)
    Me.fmTratte.Name = "fmTratte"
    Me.fmTratte.Size = New System.Drawing.Size(208, 146)
    Me.fmTratte.TabIndex = 121
    Me.fmTratte.Text = "Tratte:"
    '
    'lbTb_botrita
    '
    Me.lbTb_botrita.AutoSize = True
    Me.lbTb_botrita.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_botrita.Location = New System.Drawing.Point(5, 31)
    Me.lbTb_botrita.Name = "lbTb_botrita"
    Me.lbTb_botrita.NTSDbField = ""
    Me.lbTb_botrita.Size = New System.Drawing.Size(70, 13)
    Me.lbTb_botrita.TabIndex = 92
    Me.lbTb_botrita.Text = "Bollo % Italia"
    Me.lbTb_botrita.Tooltip = ""
    Me.lbTb_botrita.UseMnemonic = False
    '
    'edTb_botrita
    '
    Me.edTb_botrita.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_botrita.Location = New System.Drawing.Point(96, 28)
    Me.edTb_botrita.Name = "edTb_botrita"
    Me.edTb_botrita.NTSDbField = ""
    Me.edTb_botrita.NTSFormat = "0"
    Me.edTb_botrita.NTSForzaVisZoom = False
    Me.edTb_botrita.NTSOldValue = ""
    Me.edTb_botrita.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_botrita.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_botrita.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_botrita.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_botrita.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_botrita.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_botrita.Properties.AutoHeight = False
    Me.edTb_botrita.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_botrita.Properties.MaxLength = 65536
    Me.edTb_botrita.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_botrita.Size = New System.Drawing.Size(88, 20)
    Me.edTb_botrita.TabIndex = 11
    '
    'lbTb_botrest
    '
    Me.lbTb_botrest.AutoSize = True
    Me.lbTb_botrest.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_botrest.Location = New System.Drawing.Point(5, 57)
    Me.lbTb_botrest.Name = "lbTb_botrest"
    Me.lbTb_botrest.NTSDbField = ""
    Me.lbTb_botrest.Size = New System.Drawing.Size(77, 13)
    Me.lbTb_botrest.TabIndex = 95
    Me.lbTb_botrest.Text = "Bollo % Estero"
    Me.lbTb_botrest.Tooltip = ""
    Me.lbTb_botrest.UseMnemonic = False
    '
    'edTb_botrest
    '
    Me.edTb_botrest.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_botrest.Location = New System.Drawing.Point(96, 54)
    Me.edTb_botrest.Name = "edTb_botrest"
    Me.edTb_botrest.NTSDbField = ""
    Me.edTb_botrest.NTSFormat = "0"
    Me.edTb_botrest.NTSForzaVisZoom = False
    Me.edTb_botrest.NTSOldValue = ""
    Me.edTb_botrest.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_botrest.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_botrest.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_botrest.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_botrest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_botrest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_botrest.Properties.AutoHeight = False
    Me.edTb_botrest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_botrest.Properties.MaxLength = 65536
    Me.edTb_botrest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_botrest.Size = New System.Drawing.Size(88, 20)
    Me.edTb_botrest.TabIndex = 12
    '
    'lbTb_minimo
    '
    Me.lbTb_minimo.AutoSize = True
    Me.lbTb_minimo.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_minimo.Location = New System.Drawing.Point(5, 83)
    Me.lbTb_minimo.Name = "lbTb_minimo"
    Me.lbTb_minimo.NTSDbField = ""
    Me.lbTb_minimo.Size = New System.Drawing.Size(64, 13)
    Me.lbTb_minimo.TabIndex = 98
    Me.lbTb_minimo.Text = "Minimo Bollo"
    Me.lbTb_minimo.Tooltip = ""
    Me.lbTb_minimo.UseMnemonic = False
    '
    'edTb_minimo
    '
    Me.edTb_minimo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_minimo.Location = New System.Drawing.Point(96, 80)
    Me.edTb_minimo.Name = "edTb_minimo"
    Me.edTb_minimo.NTSDbField = ""
    Me.edTb_minimo.NTSFormat = "0"
    Me.edTb_minimo.NTSForzaVisZoom = False
    Me.edTb_minimo.NTSOldValue = ""
    Me.edTb_minimo.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_minimo.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_minimo.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_minimo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_minimo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_minimo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_minimo.Properties.AutoHeight = False
    Me.edTb_minimo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_minimo.Properties.MaxLength = 65536
    Me.edTb_minimo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_minimo.Size = New System.Drawing.Size(88, 20)
    Me.edTb_minimo.TabIndex = 13
    '
    'lbTb_arrot
    '
    Me.lbTb_arrot.AutoSize = True
    Me.lbTb_arrot.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_arrot.Location = New System.Drawing.Point(5, 109)
    Me.lbTb_arrot.Name = "lbTb_arrot"
    Me.lbTb_arrot.NTSDbField = ""
    Me.lbTb_arrot.Size = New System.Drawing.Size(86, 13)
    Me.lbTb_arrot.TabIndex = 101
    Me.lbTb_arrot.Text = "Arrotondamento"
    Me.lbTb_arrot.Tooltip = ""
    Me.lbTb_arrot.UseMnemonic = False
    '
    'edTb_arrot
    '
    Me.edTb_arrot.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_arrot.Location = New System.Drawing.Point(96, 106)
    Me.edTb_arrot.Name = "edTb_arrot"
    Me.edTb_arrot.NTSDbField = ""
    Me.edTb_arrot.NTSFormat = "0"
    Me.edTb_arrot.NTSForzaVisZoom = False
    Me.edTb_arrot.NTSOldValue = ""
    Me.edTb_arrot.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_arrot.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_arrot.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_arrot.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_arrot.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_arrot.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_arrot.Properties.AutoHeight = False
    Me.edTb_arrot.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_arrot.Properties.MaxLength = 65536
    Me.edTb_arrot.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_arrot.Size = New System.Drawing.Size(88, 20)
    Me.edTb_arrot.TabIndex = 14
    '
    'FRMVEBOTR
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(678, 377)
    Me.Controls.Add(Me.pnBotr)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.KeyPreview = False
    Me.MaximizeBox = False
    Me.Name = "FRMVEBOTR"
    Me.Text = "TABELLA BOLLI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnBotr, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBotr.ResumeLayout(False)
    CType(Me.fmFattureEsenti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmFattureEsenti.ResumeLayout(False)
    Me.fmFattureEsenti.PerformLayout()
    CType(Me.cbTipoCalcoloBolli.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckCalcolaBolli.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codesb_8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codesb_5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codesb_7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codesb_6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_impftes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codesb_4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_impboes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codesb_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codesb_3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codesb_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmTratte, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmTratte.ResumeLayout(False)
    Me.fmTratte.PerformLayout()
    CType(Me.edTb_botrita.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_botrest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_minimo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_arrot.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub LoadImage()
    Try
      tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
    Catch ex As Exception
      'non gestisco l'errore: se non c'è una immagine prendo quella standard
    End Try
    Try
      tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
    Catch ex As Exception
      'non gestisco l'errore: se non c'è una immagine prendo quella standard
    End Try
    Try
      tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
    Catch ex As Exception
      'non gestisco l'errore: se non c'è una immagine prendo quella standard
    End Try
    Try
      tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
    Catch ex As Exception
      'non gestisco l'errore: se non c'è una immagine prendo quella standard
    End Try
    Try
      tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
    Catch ex As Exception
      'non gestisco l'errore: se non c'è una immagine prendo quella standard
    End Try
  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttValprop As New DataTable()
    Try
      LoadImage()

      tlbMain.NTSSetToolTip()

      If oCleBotr.RitornaValProp("OPZIONI", ".", ".", "TipoCalcoloBolli", dttValprop) = True Then
        cbTipoCalcoloBolli.DataSource = dttValprop
        cbTipoCalcoloBolli.ValueMember = "cod"
        cbTipoCalcoloBolli.DisplayMember = "val"
      End If

      edTb_impftes.NTSSetParam(oMenu, oApp.Tr(Me, 128485132437500000, "Importo minimo"), oApp.FormatImporti, 14, 0, 99999999999)
      edTb_impboes.NTSSetParam(oMenu, oApp.Tr(Me, 128485132438125000, "Bollo di EUR"), oApp.FormatImporti, 14, 0, 99999999999)
      edTb_codesb_1.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128485132438750000, "Codice Iva Esente (1)"), tabciva)
      edTb_codesb_2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128485132439687500, "Codice Iva Esente (2)"), tabciva)
      edTb_codesb_3.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128485132438906250, "Codice Iva Esente (3)"), tabciva)
      edTb_codesb_4.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128485132437968750, "Codice Iva Esente (4)"), tabciva)
      edTb_codesb_5.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130233706248658596, "Codice Iva Esente (5)"), tabciva)
      edTb_codesb_6.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130233706267386060, "Codice Iva Esente (6)"), tabciva)
      edTb_codesb_7.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130233706286982936, "Codice Iva Esente (7)"), tabciva)
      edTb_codesb_8.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130233706300502413, "Codice Iva Esente (8)"), tabciva)
      edTb_botrita.NTSSetParam(oMenu, oApp.Tr(Me, 128485132439843750, "Bollo % Italia"), oApp.FormatImporti, 10, 0, 1000000)
      edTb_botrest.NTSSetParam(oMenu, oApp.Tr(Me, 128485132440156250, "Bollo % Estero"), oApp.FormatImporti, 10, 0, 1000000)
      edTb_minimo.NTSSetParam(oMenu, oApp.Tr(Me, 128485132440468750, "Minimo bollo"), oApp.FormatImporti, 10, 0, 1000000)
      edTb_arrot.NTSSetParam(oMenu, oApp.Tr(Me, 128485132440781250, "Arrotondamento"), oApp.FormatImporti, 10, 0, 1000000)
      ckCalcolaBolli.NTSSetParam(oMenu, oApp.Tr(Me, 130693261400297566, "Calcola i bolli ignorando il codice esenzione indicato in testata documento"), "S", "N")
      cbTipoCalcoloBolli.NTSSetParam(oApp.Tr(Me, 130713198636892317, "Tipo Calcolo Bolli"))

      edTb_codesb_1.NTSForzaVisZoom = True
      edTb_codesb_2.NTSForzaVisZoom = True
      edTb_codesb_3.NTSForzaVisZoom = True
      edTb_codesb_4.NTSForzaVisZoom = True
      edTb_codesb_5.NTSForzaVisZoom = True
      edTb_codesb_6.NTSForzaVisZoom = True
      edTb_codesb_7.NTSForzaVisZoom = True
      edTb_codesb_8.NTSForzaVisZoom = True

      edTb_codesb_1.NTSSetParamZoom("ZOOMTABCIVA")
      edTb_codesb_2.NTSSetParamZoom("ZOOMTABCIVA")
      edTb_codesb_3.NTSSetParamZoom("ZOOMTABCIVA")
      edTb_codesb_4.NTSSetParamZoom("ZOOMTABCIVA")
      edTb_codesb_5.NTSSetParamZoom("ZOOMTABCIVA")
      edTb_codesb_6.NTSSetParamZoom("ZOOMTABCIVA")
      edTb_codesb_7.NTSSetParamZoom("ZOOMTABCIVA")
      edTb_codesb_8.NTSSetParamZoom("ZOOMTABCIVA")

      '-------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '-------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano già stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)
      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      '-------------------------------------------------
      edTb_botrita.NTSDbField = "tabbotr.tb_botrita"
      edTb_botrest.NTSDbField = "tabbotr.tb_botrest"
      edTb_minimo.NTSDbField = "tabbotr.tb_minimo"
      edTb_arrot.NTSDbField = "tabbotr.tb_arrot"
      edTb_impftes.NTSDbField = "tabbotr.tb_impftes"
      edTb_impboes.NTSDbField = "tabbotr.tb_impboes"
      edTb_codesb_1.NTSDbField = "tabbotr.tb_codesb_1"
      lbXx_codesb_1.NTSDbField = "tabbotr.xx_codesb_1"
      edTb_codesb_2.NTSDbField = "tabbotr.tb_codesb_2"
      lbXx_codesb_2.NTSDbField = "tabbotr.xx_codesb_2"
      edTb_codesb_3.NTSDbField = "tabbotr.tb_codesb_3"
      lbXx_codesb_3.NTSDbField = "tabbotr.xx_codesb_3"
      edTb_codesb_4.NTSDbField = "tabbotr.tb_codesb_4"
      lbXx_codesb_4.NTSDbField = "tabbotr.xx_codesb_4"
      edTb_codesb_5.NTSDbField = "tabbotr.tb_codesb_5"
      lbXx_codesb_5.NTSDbField = "tabbotr.xx_codesb_5"
      edTb_codesb_6.NTSDbField = "tabbotr.tb_codesb_6"
      lbXx_codesb_6.NTSDbField = "tabbotr.xx_codesb_6"
      edTb_codesb_7.NTSDbField = "tabbotr.tb_codesb_7"
      lbXx_codesb_7.NTSDbField = "tabbotr.xx_codesb_7"
      edTb_codesb_8.NTSDbField = "tabbotr.tb_codesb_8"
      lbXx_codesb_8.NTSDbField = "tabbotr.xx_codesb_8"
      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcBotr, Me)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRMVEBOTR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim bDbMultiDitta As Boolean = False

    Try

      InitControls()

      If oCleBotr.File_Apri(DittaCorrente, dsBotr) Then
        dcBotr.DataSource = dsBotr.Tables("TABBOTR")
        dsBotr.AcceptChanges()

        Bindcontrols()

        GctlSetRoules()

        If dsBotr.Tables("TABBOTR").Rows.Count = 0 Then
          oCleBotr.RecordNuovo()
          dcBotr.MoveLast()
          Me.GctlApplicaDefaultValue()
        End If

        dcBotr.ResetBindings(False)
        'dcBotr.MoveFirst()

        tlbZoom.Enabled = False
      Else
        Me.Close()
      End If
      '--------------------------------------------------------------------------------------------------------------
      oCleBotr.OpzioniPerDitta()
      '--------------------------------------------------------------------------------------------------------------
      If (oCleBotr.bOpzioniPerDitta = False) And (oCleBotr.SetIsMultiditta = True) Then
        lbInfo.Visible = True
      Else
        lbInfo.Visible = False
      End If
      bOnLoading = True
      oCleBotr.bCalcolaBolli = CBool(oMenu.GetSettingBusDitt(DittaCorrente, "Opzioni", ".", ".", _
        "DeterminaBolliSuOperazEsenti", "0", " ", "0"))
      ckCalcolaBolli.Checked = oCleBotr.bCalcolaBolli
      If oCleBotr.CheckEsistenzaOpzioni("DeterminaBolliSuOperazEsenti") > 1 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130713216895022321, "Attenzione!" & vbCrLf & _
          "L'opzione di registro:" & vbCrLf & _
          " . OPZIONI\'DeterminaBolliSuOperazEsenti'" & vbCrLf & _
          "è presente, erroneamente, sia per ditta che globalmente." & vbCrLf & _
          "Eventuali aggiornamenti avranno effetto 'globalmente', per tutte le ditte."))
      End If
      oCleBotr.strTipoCalcoloBolli = oMenu.GetSettingBusDitt(DittaCorrente, "Opzioni", ".", ".", _
        "TipoCalcoloBolli", "X", " ", "X")
      cbTipoCalcoloBolli.SelectedValue = oCleBotr.strTipoCalcoloBolli
      If oCleBotr.CheckEsistenzaOpzioni("TipoCalcoloBolli") > 1 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130713216909025331, "Attenzione!" & vbCrLf & _
          "L'opzione di registro:" & vbCrLf & _
          " . OPZIONI\'TipoCalcoloBolli'" & vbCrLf & _
          "è presente, erroneamente, sia per ditta che globalmente." & vbCrLf & _
          "Eventuali aggiornamenti avranno effetto 'globalmente', per tutte le ditte."))
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      bOnLoading = False
    End Try
  End Sub
  Public Overridable Sub FRMVEBOTR_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMVEBOTR_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcBotr.Dispose()
      If Not dsBotr Is Nothing Then dsBotr.Dispose()
    Catch
    End Try
  End Sub

  Public Overridable Sub edTb_codesb_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTb_codesb_1.Leave, edTb_codesb_2.Leave, edTb_codesb_3.Leave, edTb_codesb_4.Leave, edTb_codesb_5.Leave, edTb_codesb_6.Leave, edTb_codesb_7.Leave, edTb_codesb_8.Leave
    tlbZoom.Enabled = False
  End Sub
  Public Overridable Sub edTb_codesb_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTb_codesb_1.Enter, edTb_codesb_2.Enter, edTb_codesb_3.Enter, edTb_codesb_4.Enter, edTb_codesb_5.Enter, edTb_codesb_6.Enter, edTb_codesb_7.Enter, edTb_codesb_8.Enter
    tlbZoom.Enabled = True
  End Sub

#End Region

  Public Overridable Function Salva() As Boolean
    Try
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      If oCleBotr.RecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        Select Case oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128056616807812500, "Salvo la personalizzazione?"))
          Case System.Windows.Forms.DialogResult.Cancel
            Return False
          Case System.Windows.Forms.DialogResult.Yes
            If Not oCleBotr.File_Aggiorna(False) Then Return False
          Case System.Windows.Forms.DialogResult.No
            tlbRipristina_ItemClick(Nothing, Nothing)
        End Select

      End If
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

#Region "Eventi Toolbar"
  Public Overridable Sub tlb_Aggiorna_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Dim dRes As DialogResult
    Try
      If oCleBotr.bHasChanges Then

        '-------------------------------------------------
        'ripristino la forma di pagamento
        If sender Is Nothing Then
          dRes = Windows.Forms.DialogResult.Yes
        Else
          dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128039950429843750, "Ripristinare le modifiche apportate alla personalizzazione?"))
        End If

        If dRes = Windows.Forms.DialogResult.Yes Then oCleBotr.RecordRipristina(dcBotr.Position, dcBotr.Filter)

      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try

      NTSCallStandardZoom()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
  End Sub
  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub
#End Region

#Region "Eventi CheckBox"
  Public Overridable Sub ckCalcolaBolli_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckCalcolaBolli.CheckedChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bOnLoading = True Then Return
      '--------------------------------------------------------------------------------------------------------------
      oCleBotr.bHasChanges = True
      '--------------------------------------------------------------------------------------------------------------
      oCleBotr.bCalcolaBolli = ckCalcolaBolli.Checked
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi ComboBox"
  Public Overridable Sub cbOrdin_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTipoCalcoloBolli.SelectedIndexChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bOnLoading = True Then Return
      '--------------------------------------------------------------------------------------------------------------
      oCleBotr.bHasChanges = True
      '--------------------------------------------------------------------------------------------------------------
      oCleBotr.strTipoCalcoloBolli = cbTipoCalcoloBolli.SelectedValue
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

End Class
