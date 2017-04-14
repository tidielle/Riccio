Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGCMMA

#Region "Dichiarazione variabili"
  Public oCleCmma As CLEMGCMMA
  Public oCallParams As CLE__CLDP
  Public strCodagedaOld As String = "0"
  Public strDescrdaOld As String = ""
  Public strCodageaOld As String = "0"
  Public strDescraOld As String = ""
  Private components As System.ComponentModel.IContainer

  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbEsegui As NTSInformatica.NTSBarButtonItem
  Public WithEvents fmAttenzione As NTSInformatica.NTSGroupBox
  Public WithEvents lbTesto As NTSInformatica.NTSLabel
  Public WithEvents edSerie As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDanord As NTSInformatica.NTSLabel
  Public WithEvents lbAnno As NTSInformatica.NTSLabel
  Public WithEvents edAnumord As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAnord As NTSInformatica.NTSLabel
  Public WithEvents edDanumord As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAnno As NTSInformatica.NTSTextBoxNum
  Public WithEvents fmDoccan As NTSInformatica.NTSGroupBox
  Public WithEvents lbOrdine As NTSInformatica.NTSLabel
  Public WithEvents ckDdteme As NTSInformatica.NTSCheckBox
  Public WithEvents ckCoreme As NTSInformatica.NTSCheckBox
  Public WithEvents ckFatdie As NTSInformatica.NTSCheckBox
  Public WithEvents ckFatimr As NTSInformatica.NTSCheckBox
  Public WithEvents ckDdtric As NTSInformatica.NTSCheckBox
  Public WithEvents ckNotace As NTSInformatica.NTSCheckBox
  Public WithEvents ckFatime As NTSInformatica.NTSCheckBox
#End Region

#Region "Moduli"
  Private Moduli_P As Integer = bsModMG + bsModVE
  Private ModuliExt_P As Integer = bsModExtMGE
  Private ModuliSup_P As Integer = 0
  Private ModuliSupExt_P As Integer = 0
  Private ModuliPtn_P As Integer = 0
  Public WithEvents fmFiltri As NTSInformatica.NTSGroupBox
  Public WithEvents lbSerie As NTSInformatica.NTSLabel
  Public WithEvents ckFatrfd As NTSInformatica.NTSCheckBox
  Public WithEvents ckNade As NTSInformatica.NTSCheckBox
  Public WithEvents ckNadr As NTSInformatica.NTSCheckBox
  Public WithEvents ckRicfis As NTSInformatica.NTSCheckBox
  Public WithEvents ckRierif As NTSInformatica.NTSCheckBox
  Public WithEvents ckFatrif As NTSInformatica.NTSCheckBox
  Public WithEvents ckNotade As NTSInformatica.NTSCheckBox
  Public WithEvents ckNotacr As NTSInformatica.NTSCheckBox
  Public WithEvents ckFatdir As NTSInformatica.NTSCheckBox
  Public WithEvents ckCarpro As NTSInformatica.NTSCheckBox
  Public WithEvents ckScapro As NTSInformatica.NTSCheckBox
  Public WithEvents ckBolmoi As NTSInformatica.NTSCheckBox
  Public WithEvents fmDate As NTSInformatica.NTSGroupBox
  Public WithEvents lbUltcanc As NTSInformatica.NTSLabel
  Public WithEvents lbUltagg As NTSInformatica.NTSLabel
  Public WithEvents edDatrif As NTSInformatica.NTSTextBoxData
  Public WithEvents lbDatrif As NTSInformatica.NTSLabel
  Public WithEvents lbDatcan As NTSInformatica.NTSLabel
  Public WithEvents lbDatagg As NTSInformatica.NTSLabel
  Public WithEvents lbTesto2 As NTSInformatica.NTSLabel
  Public WithEvents cbTipo As NTSInformatica.NTSComboBox
  Public WithEvents tlbSelAll As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDesAll As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnCheck3 As NTSInformatica.NTSPanel
  Public WithEvents pnCheck2 As NTSInformatica.NTSPanel
  Public WithEvents pnCheck1 As NTSInformatica.NTSPanel
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGCMMA", "BEMGCMMA", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128589409211093750, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleCmma = CType(oTmp, CLEMGCMMA)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGCMMA", strRemoteServer, strRemotePort)
    AddHandler oCleCmma.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleCmma.Init(oApp, NTSScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGCMMA))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbEsegui = New NTSInformatica.NTSBarButtonItem
    Me.tlbSelAll = New NTSInformatica.NTSBarButtonItem
    Me.tlbDesAll = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.fmAttenzione = New NTSInformatica.NTSGroupBox
    Me.cbTipo = New NTSInformatica.NTSComboBox
    Me.lbTesto = New NTSInformatica.NTSLabel
    Me.edAnumord = New NTSInformatica.NTSTextBoxNum
    Me.lbAnord = New NTSInformatica.NTSLabel
    Me.edDanumord = New NTSInformatica.NTSTextBoxNum
    Me.edAnno = New NTSInformatica.NTSTextBoxNum
    Me.lbDanord = New NTSInformatica.NTSLabel
    Me.lbAnno = New NTSInformatica.NTSLabel
    Me.edSerie = New NTSInformatica.NTSTextBoxStr
    Me.fmDoccan = New NTSInformatica.NTSGroupBox
    Me.pnCheck3 = New NTSInformatica.NTSPanel
    Me.ckRicfis = New NTSInformatica.NTSCheckBox
    Me.ckFatrif = New NTSInformatica.NTSCheckBox
    Me.ckRierif = New NTSInformatica.NTSCheckBox
    Me.ckFatrfd = New NTSInformatica.NTSCheckBox
    Me.ckNadr = New NTSInformatica.NTSCheckBox
    Me.ckNade = New NTSInformatica.NTSCheckBox
    Me.pnCheck2 = New NTSInformatica.NTSPanel
    Me.ckFatimr = New NTSInformatica.NTSCheckBox
    Me.ckNotace = New NTSInformatica.NTSCheckBox
    Me.ckDdtric = New NTSInformatica.NTSCheckBox
    Me.ckBolmoi = New NTSInformatica.NTSCheckBox
    Me.ckScapro = New NTSInformatica.NTSCheckBox
    Me.ckCarpro = New NTSInformatica.NTSCheckBox
    Me.pnCheck1 = New NTSInformatica.NTSPanel
    Me.ckFatime = New NTSInformatica.NTSCheckBox
    Me.ckFatdie = New NTSInformatica.NTSCheckBox
    Me.ckCoreme = New NTSInformatica.NTSCheckBox
    Me.ckDdteme = New NTSInformatica.NTSCheckBox
    Me.ckFatdir = New NTSInformatica.NTSCheckBox
    Me.ckNotacr = New NTSInformatica.NTSCheckBox
    Me.ckNotade = New NTSInformatica.NTSCheckBox
    Me.lbOrdine = New NTSInformatica.NTSLabel
    Me.fmFiltri = New NTSInformatica.NTSGroupBox
    Me.lbTesto2 = New NTSInformatica.NTSLabel
    Me.lbSerie = New NTSInformatica.NTSLabel
    Me.fmDate = New NTSInformatica.NTSGroupBox
    Me.lbDatcan = New NTSInformatica.NTSLabel
    Me.lbDatagg = New NTSInformatica.NTSLabel
    Me.lbUltcanc = New NTSInformatica.NTSLabel
    Me.lbUltagg = New NTSInformatica.NTSLabel
    Me.edDatrif = New NTSInformatica.NTSTextBoxData
    Me.lbDatrif = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmAttenzione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmAttenzione.SuspendLayout()
    CType(Me.cbTipo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnumord.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDanumord.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmDoccan, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmDoccan.SuspendLayout()
    CType(Me.pnCheck3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCheck3.SuspendLayout()
    CType(Me.ckRicfis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckFatrif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckRierif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckFatrfd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNadr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNade.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnCheck2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCheck2.SuspendLayout()
    CType(Me.ckFatimr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNotace.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDdtric.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckBolmoi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckScapro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckCarpro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnCheck1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCheck1.SuspendLayout()
    CType(Me.ckFatime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckFatdie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckCoreme.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDdteme.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckFatdir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNotacr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNotade.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmFiltri, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmFiltri.SuspendLayout()
    CType(Me.fmDate, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmDate.SuspendLayout()
    CType(Me.edDatrif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'NtsBarManager1
    '
    Me.NtsBarManager1.AllowCustomization = False
    Me.NtsBarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.tlbMain})
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlTop)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlBottom)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlLeft)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlRight)
    Me.NtsBarManager1.Form = Me
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbGuida, Me.tlbEsci, Me.tlbEsegui, Me.tlbSelAll, Me.tlbDesAll})
    Me.NtsBarManager1.MaxItemId = 16
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsegui), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSelAll, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDesAll), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbEsegui
    '
    Me.tlbEsegui.Caption = "Esegui"
    Me.tlbEsegui.Glyph = CType(resources.GetObject("tlbEsegui.Glyph"), System.Drawing.Image)
    Me.tlbEsegui.GlyphPath = ""
    Me.tlbEsegui.Id = 13
    Me.tlbEsegui.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbEsegui.Name = "tlbEsegui"
    Me.tlbEsegui.Visible = True
    '
    'tlbSelAll
    '
    Me.tlbSelAll.Caption = "Seleziona tutto"
    Me.tlbSelAll.Glyph = CType(resources.GetObject("tlbSelAll.Glyph"), System.Drawing.Image)
    Me.tlbSelAll.GlyphPath = ""
    Me.tlbSelAll.Id = 14
    Me.tlbSelAll.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
    Me.tlbSelAll.Name = "tlbSelAll"
    Me.tlbSelAll.Visible = True
    '
    'tlbDesAll
    '
    Me.tlbDesAll.Caption = "Deseleziona tutto"
    Me.tlbDesAll.Glyph = CType(resources.GetObject("tlbDesAll.Glyph"), System.Drawing.Image)
    Me.tlbDesAll.GlyphPath = ""
    Me.tlbDesAll.Id = 15
    Me.tlbDesAll.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D))
    Me.tlbDesAll.Name = "tlbDesAll"
    Me.tlbDesAll.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 11
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 12
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'fmAttenzione
    '
    Me.fmAttenzione.AllowDrop = True
    Me.fmAttenzione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmAttenzione.Appearance.Options.UseBackColor = True
    Me.fmAttenzione.Controls.Add(Me.cbTipo)
    Me.fmAttenzione.Controls.Add(Me.lbTesto)
    Me.fmAttenzione.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmAttenzione.Location = New System.Drawing.Point(13, 37)
    Me.fmAttenzione.Name = "fmAttenzione"
    Me.fmAttenzione.Size = New System.Drawing.Size(589, 71)
    Me.fmAttenzione.TabIndex = 4
    Me.fmAttenzione.Text = "Attenzione!"
    '
    'cbTipo
    '
    Me.cbTipo.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipo.DataSource = Nothing
    Me.cbTipo.DisplayMember = ""
    Me.cbTipo.Location = New System.Drawing.Point(12, 41)
    Me.cbTipo.Name = "cbTipo"
    Me.cbTipo.NTSDbField = ""
    Me.cbTipo.Properties.AutoHeight = False
    Me.cbTipo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipo.Properties.DropDownRows = 30
    Me.cbTipo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipo.SelectedValue = ""
    Me.cbTipo.Size = New System.Drawing.Size(561, 20)
    Me.cbTipo.TabIndex = 1
    Me.cbTipo.ValueMember = ""
    '
    'lbTesto
    '
    Me.lbTesto.AutoSize = True
    Me.lbTesto.BackColor = System.Drawing.Color.Transparent
    Me.lbTesto.Location = New System.Drawing.Point(5, 25)
    Me.lbTesto.Name = "lbTesto"
    Me.lbTesto.NTSDbField = ""
    Me.lbTesto.Size = New System.Drawing.Size(167, 13)
    Me.lbTesto.TabIndex = 0
    Me.lbTesto.Text = "Si procederà alla cancellazione di:"
    Me.lbTesto.Tooltip = ""
    Me.lbTesto.UseMnemonic = False
    '
    'edAnumord
    '
    Me.edAnumord.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnumord.EditValue = "999999999"
    Me.edAnumord.Location = New System.Drawing.Point(311, 113)
    Me.edAnumord.Name = "edAnumord"
    Me.edAnumord.NTSDbField = ""
    Me.edAnumord.NTSFormat = "0"
    Me.edAnumord.NTSForzaVisZoom = False
    Me.edAnumord.NTSOldValue = "999999999"
    Me.edAnumord.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnumord.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnumord.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnumord.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnumord.Properties.AutoHeight = False
    Me.edAnumord.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnumord.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnumord.Size = New System.Drawing.Size(100, 20)
    Me.edAnumord.TabIndex = 17
    '
    'lbAnord
    '
    Me.lbAnord.AutoSize = True
    Me.lbAnord.BackColor = System.Drawing.Color.Transparent
    Me.lbAnord.Location = New System.Drawing.Point(220, 116)
    Me.lbAnord.Name = "lbAnord"
    Me.lbAnord.NTSDbField = ""
    Me.lbAnord.Size = New System.Drawing.Size(85, 13)
    Me.lbAnord.TabIndex = 16
    Me.lbAnord.Text = "a numero ordine"
    Me.lbAnord.Tooltip = ""
    Me.lbAnord.UseMnemonic = False
    '
    'edDanumord
    '
    Me.edDanumord.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edDanumord.EditValue = "0"
    Me.edDanumord.Location = New System.Drawing.Point(114, 113)
    Me.edDanumord.Name = "edDanumord"
    Me.edDanumord.NTSDbField = ""
    Me.edDanumord.NTSFormat = "0"
    Me.edDanumord.NTSForzaVisZoom = False
    Me.edDanumord.NTSOldValue = ""
    Me.edDanumord.Properties.Appearance.Options.UseTextOptions = True
    Me.edDanumord.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDanumord.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDanumord.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDanumord.Properties.AutoHeight = False
    Me.edDanumord.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDanumord.Properties.MaxLength = 65536
    Me.edDanumord.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDanumord.Size = New System.Drawing.Size(100, 20)
    Me.edDanumord.TabIndex = 9
    '
    'edAnno
    '
    Me.edAnno.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnno.Location = New System.Drawing.Point(114, 87)
    Me.edAnno.Name = "edAnno"
    Me.edAnno.NTSDbField = ""
    Me.edAnno.NTSFormat = "0"
    Me.edAnno.NTSForzaVisZoom = False
    Me.edAnno.NTSOldValue = ""
    Me.edAnno.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnno.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnno.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnno.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnno.Properties.AutoHeight = False
    Me.edAnno.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnno.Properties.MaxLength = 65536
    Me.edAnno.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnno.Size = New System.Drawing.Size(51, 20)
    Me.edAnno.TabIndex = 8
    '
    'lbDanord
    '
    Me.lbDanord.AutoSize = True
    Me.lbDanord.BackColor = System.Drawing.Color.Transparent
    Me.lbDanord.Location = New System.Drawing.Point(5, 116)
    Me.lbDanord.Name = "lbDanord"
    Me.lbDanord.NTSDbField = ""
    Me.lbDanord.Size = New System.Drawing.Size(92, 13)
    Me.lbDanord.TabIndex = 7
    Me.lbDanord.Text = "Da numero ordine"
    Me.lbDanord.Tooltip = ""
    Me.lbDanord.UseMnemonic = False
    '
    'lbAnno
    '
    Me.lbAnno.AutoSize = True
    Me.lbAnno.BackColor = System.Drawing.Color.Transparent
    Me.lbAnno.Location = New System.Drawing.Point(5, 90)
    Me.lbAnno.Name = "lbAnno"
    Me.lbAnno.NTSDbField = ""
    Me.lbAnno.Size = New System.Drawing.Size(103, 13)
    Me.lbAnno.TabIndex = 3
    Me.lbAnno.Text = "Anno (0 = qualsiasi)"
    Me.lbAnno.Tooltip = ""
    Me.lbAnno.UseMnemonic = False
    '
    'edSerie
    '
    Me.edSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSerie.EditValue = "*"
    Me.edSerie.Location = New System.Drawing.Point(114, 61)
    Me.edSerie.Name = "edSerie"
    Me.edSerie.NTSDbField = ""
    Me.edSerie.NTSForzaVisZoom = False
    Me.edSerie.NTSOldValue = "*"
    Me.edSerie.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSerie.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSerie.Properties.AutoHeight = False
    Me.edSerie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSerie.Properties.MaxLength = 65536
    Me.edSerie.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSerie.Size = New System.Drawing.Size(45, 20)
    Me.edSerie.TabIndex = 2
    '
    'fmDoccan
    '
    Me.fmDoccan.AllowDrop = True
    Me.fmDoccan.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmDoccan.Appearance.Options.UseBackColor = True
    Me.fmDoccan.Controls.Add(Me.pnCheck3)
    Me.fmDoccan.Controls.Add(Me.pnCheck2)
    Me.fmDoccan.Controls.Add(Me.pnCheck1)
    Me.fmDoccan.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmDoccan.Location = New System.Drawing.Point(13, 114)
    Me.fmDoccan.Name = "fmDoccan"
    Me.fmDoccan.Size = New System.Drawing.Size(589, 177)
    Me.fmDoccan.TabIndex = 5
    Me.fmDoccan.Text = "Documenti da cancellare"
    '
    'pnCheck3
    '
    Me.pnCheck3.AllowDrop = True
    Me.pnCheck3.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCheck3.Appearance.Options.UseBackColor = True
    Me.pnCheck3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCheck3.Controls.Add(Me.ckRicfis)
    Me.pnCheck3.Controls.Add(Me.ckFatrif)
    Me.pnCheck3.Controls.Add(Me.ckRierif)
    Me.pnCheck3.Controls.Add(Me.ckFatrfd)
    Me.pnCheck3.Controls.Add(Me.ckNadr)
    Me.pnCheck3.Controls.Add(Me.ckNade)
    Me.pnCheck3.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCheck3.Location = New System.Drawing.Point(374, 26)
    Me.pnCheck3.Name = "pnCheck3"
    Me.pnCheck3.NTSActiveTrasparency = True
    Me.pnCheck3.Size = New System.Drawing.Size(210, 150)
    Me.pnCheck3.TabIndex = 26
    Me.pnCheck3.Text = "NtsPanel1"
    '
    'ckRicfis
    '
    Me.ckRicfis.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckRicfis.EditValue = True
    Me.ckRicfis.Location = New System.Drawing.Point(3, 5)
    Me.ckRicfis.Name = "ckRicfis"
    Me.ckRicfis.NTSCheckValue = "S"
    Me.ckRicfis.NTSUnCheckValue = "N"
    Me.ckRicfis.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckRicfis.Properties.Appearance.Options.UseBackColor = True
    Me.ckRicfis.Properties.AutoHeight = False
    Me.ckRicfis.Properties.Caption = "Ricevute Fiscali"
    Me.ckRicfis.Size = New System.Drawing.Size(105, 19)
    Me.ckRicfis.TabIndex = 15
    '
    'ckFatrif
    '
    Me.ckFatrif.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckFatrif.EditValue = True
    Me.ckFatrif.Location = New System.Drawing.Point(3, 45)
    Me.ckFatrif.Name = "ckFatrif"
    Me.ckFatrif.NTSCheckValue = "S"
    Me.ckFatrif.NTSUnCheckValue = "N"
    Me.ckFatrif.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckFatrif.Properties.Appearance.Options.UseBackColor = True
    Me.ckFatrif.Properties.AutoHeight = False
    Me.ckFatrif.Properties.Caption = "Fatture/Ricevute Fiscali"
    Me.ckFatrif.Size = New System.Drawing.Size(145, 19)
    Me.ckFatrif.TabIndex = 13
    '
    'ckRierif
    '
    Me.ckRierif.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckRierif.EditValue = True
    Me.ckRierif.Location = New System.Drawing.Point(3, 25)
    Me.ckRierif.Name = "ckRierif"
    Me.ckRierif.NTSCheckValue = "S"
    Me.ckRierif.NTSUnCheckValue = "N"
    Me.ckRierif.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckRierif.Properties.Appearance.Options.UseBackColor = True
    Me.ckRierif.Properties.AutoHeight = False
    Me.ckRierif.Properties.Caption = "Riemissione Ricevute Fiscali"
    Me.ckRierif.Size = New System.Drawing.Size(163, 19)
    Me.ckRierif.TabIndex = 14
    '
    'ckFatrfd
    '
    Me.ckFatrfd.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckFatrfd.EditValue = True
    Me.ckFatrfd.Location = New System.Drawing.Point(3, 65)
    Me.ckFatrfd.Name = "ckFatrfd"
    Me.ckFatrfd.NTSCheckValue = "S"
    Me.ckFatrfd.NTSUnCheckValue = "N"
    Me.ckFatrfd.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckFatrfd.Properties.Appearance.Options.UseBackColor = True
    Me.ckFatrfd.Properties.AutoHeight = False
    Me.ckFatrfd.Properties.Caption = "Fatture/Ricevute Fiscali Differite"
    Me.ckFatrfd.Size = New System.Drawing.Size(184, 19)
    Me.ckFatrfd.TabIndex = 16
    '
    'ckNadr
    '
    Me.ckNadr.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckNadr.EditValue = True
    Me.ckNadr.Location = New System.Drawing.Point(3, 105)
    Me.ckNadr.Name = "ckNadr"
    Me.ckNadr.NTSCheckValue = "S"
    Me.ckNadr.NTSUnCheckValue = "N"
    Me.ckNadr.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNadr.Properties.Appearance.Options.UseBackColor = True
    Me.ckNadr.Properties.AutoHeight = False
    Me.ckNadr.Properties.Caption = "Note di Accredito Differite Ricevute"
    Me.ckNadr.Size = New System.Drawing.Size(196, 19)
    Me.ckNadr.TabIndex = 16
    '
    'ckNade
    '
    Me.ckNade.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckNade.EditValue = True
    Me.ckNade.Location = New System.Drawing.Point(3, 85)
    Me.ckNade.Name = "ckNade"
    Me.ckNade.NTSCheckValue = "S"
    Me.ckNade.NTSUnCheckValue = "N"
    Me.ckNade.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNade.Properties.Appearance.Options.UseBackColor = True
    Me.ckNade.Properties.AutoHeight = False
    Me.ckNade.Properties.Caption = "Note di Accredito Differite Emesse"
    Me.ckNade.Size = New System.Drawing.Size(196, 19)
    Me.ckNade.TabIndex = 16
    '
    'pnCheck2
    '
    Me.pnCheck2.AllowDrop = True
    Me.pnCheck2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCheck2.Appearance.Options.UseBackColor = True
    Me.pnCheck2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCheck2.Controls.Add(Me.ckFatimr)
    Me.pnCheck2.Controls.Add(Me.ckNotace)
    Me.pnCheck2.Controls.Add(Me.ckDdtric)
    Me.pnCheck2.Controls.Add(Me.ckBolmoi)
    Me.pnCheck2.Controls.Add(Me.ckScapro)
    Me.pnCheck2.Controls.Add(Me.ckCarpro)
    Me.pnCheck2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCheck2.Location = New System.Drawing.Point(182, 26)
    Me.pnCheck2.Name = "pnCheck2"
    Me.pnCheck2.NTSActiveTrasparency = True
    Me.pnCheck2.Size = New System.Drawing.Size(189, 150)
    Me.pnCheck2.TabIndex = 26
    Me.pnCheck2.Text = "NtsPanel1"
    '
    'ckFatimr
    '
    Me.ckFatimr.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckFatimr.EditValue = True
    Me.ckFatimr.Location = New System.Drawing.Point(3, 5)
    Me.ckFatimr.Name = "ckFatimr"
    Me.ckFatimr.NTSCheckValue = "S"
    Me.ckFatimr.NTSUnCheckValue = "N"
    Me.ckFatimr.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckFatimr.Properties.Appearance.Options.UseBackColor = True
    Me.ckFatimr.Properties.AutoHeight = False
    Me.ckFatimr.Properties.Caption = "Fatture Immediate Ricevute"
    Me.ckFatimr.Size = New System.Drawing.Size(163, 19)
    Me.ckFatimr.TabIndex = 3
    '
    'ckNotace
    '
    Me.ckNotace.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckNotace.EditValue = True
    Me.ckNotace.Location = New System.Drawing.Point(3, 45)
    Me.ckNotace.Name = "ckNotace"
    Me.ckNotace.NTSCheckValue = "S"
    Me.ckNotace.NTSUnCheckValue = "N"
    Me.ckNotace.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNotace.Properties.Appearance.Options.UseBackColor = True
    Me.ckNotace.Properties.AutoHeight = False
    Me.ckNotace.Properties.Caption = "Note di Accredito Emesse"
    Me.ckNotace.Size = New System.Drawing.Size(152, 19)
    Me.ckNotace.TabIndex = 1
    '
    'ckDdtric
    '
    Me.ckDdtric.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDdtric.EditValue = True
    Me.ckDdtric.Location = New System.Drawing.Point(3, 25)
    Me.ckDdtric.Name = "ckDdtric"
    Me.ckDdtric.NTSCheckValue = "S"
    Me.ckDdtric.NTSUnCheckValue = "N"
    Me.ckDdtric.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDdtric.Properties.Appearance.Options.UseBackColor = True
    Me.ckDdtric.Properties.AutoHeight = False
    Me.ckDdtric.Properties.Caption = "D.D.T. Ricevuti"
    Me.ckDdtric.Size = New System.Drawing.Size(103, 19)
    Me.ckDdtric.TabIndex = 2
    '
    'ckBolmoi
    '
    Me.ckBolmoi.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckBolmoi.EditValue = True
    Me.ckBolmoi.Location = New System.Drawing.Point(3, 105)
    Me.ckBolmoi.Name = "ckBolmoi"
    Me.ckBolmoi.NTSCheckValue = "S"
    Me.ckBolmoi.NTSUnCheckValue = "N"
    Me.ckBolmoi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckBolmoi.Properties.Appearance.Options.UseBackColor = True
    Me.ckBolmoi.Properties.AutoHeight = False
    Me.ckBolmoi.Properties.Caption = "Bolle di Movimentazione Interna"
    Me.ckBolmoi.Size = New System.Drawing.Size(183, 19)
    Me.ckBolmoi.TabIndex = 7
    '
    'ckScapro
    '
    Me.ckScapro.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckScapro.EditValue = True
    Me.ckScapro.Location = New System.Drawing.Point(3, 85)
    Me.ckScapro.Name = "ckScapro"
    Me.ckScapro.NTSCheckValue = "S"
    Me.ckScapro.NTSUnCheckValue = "N"
    Me.ckScapro.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckScapro.Properties.Appearance.Options.UseBackColor = True
    Me.ckScapro.Properties.AutoHeight = False
    Me.ckScapro.Properties.Caption = "Scarico a Produzione"
    Me.ckScapro.Size = New System.Drawing.Size(134, 19)
    Me.ckScapro.TabIndex = 8
    '
    'ckCarpro
    '
    Me.ckCarpro.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckCarpro.EditValue = True
    Me.ckCarpro.Location = New System.Drawing.Point(3, 65)
    Me.ckCarpro.Name = "ckCarpro"
    Me.ckCarpro.NTSCheckValue = "S"
    Me.ckCarpro.NTSUnCheckValue = "N"
    Me.ckCarpro.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckCarpro.Properties.Appearance.Options.UseBackColor = True
    Me.ckCarpro.Properties.AutoHeight = False
    Me.ckCarpro.Properties.Caption = "Carico da Produzione"
    Me.ckCarpro.Size = New System.Drawing.Size(134, 19)
    Me.ckCarpro.TabIndex = 9
    '
    'pnCheck1
    '
    Me.pnCheck1.AllowDrop = True
    Me.pnCheck1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCheck1.Appearance.Options.UseBackColor = True
    Me.pnCheck1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCheck1.Controls.Add(Me.ckFatime)
    Me.pnCheck1.Controls.Add(Me.ckFatdie)
    Me.pnCheck1.Controls.Add(Me.ckCoreme)
    Me.pnCheck1.Controls.Add(Me.ckDdteme)
    Me.pnCheck1.Controls.Add(Me.ckFatdir)
    Me.pnCheck1.Controls.Add(Me.ckNotacr)
    Me.pnCheck1.Controls.Add(Me.ckNotade)
    Me.pnCheck1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCheck1.Location = New System.Drawing.Point(5, 26)
    Me.pnCheck1.Name = "pnCheck1"
    Me.pnCheck1.NTSActiveTrasparency = True
    Me.pnCheck1.Size = New System.Drawing.Size(175, 150)
    Me.pnCheck1.TabIndex = 25
    Me.pnCheck1.Text = "NtsPanel1"
    '
    'ckFatime
    '
    Me.ckFatime.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckFatime.EditValue = True
    Me.ckFatime.Location = New System.Drawing.Point(3, 5)
    Me.ckFatime.Name = "ckFatime"
    Me.ckFatime.NTSCheckValue = "S"
    Me.ckFatime.NTSUnCheckValue = "N"
    Me.ckFatime.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckFatime.Properties.Appearance.Options.UseBackColor = True
    Me.ckFatime.Properties.AutoHeight = False
    Me.ckFatime.Properties.Caption = "Fatture Immediate Emesse"
    Me.ckFatime.Size = New System.Drawing.Size(157, 19)
    Me.ckFatime.TabIndex = 0
    '
    'ckFatdie
    '
    Me.ckFatdie.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckFatdie.EditValue = True
    Me.ckFatdie.Location = New System.Drawing.Point(3, 65)
    Me.ckFatdie.Name = "ckFatdie"
    Me.ckFatdie.NTSCheckValue = "S"
    Me.ckFatdie.NTSUnCheckValue = "N"
    Me.ckFatdie.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckFatdie.Properties.Appearance.Options.UseBackColor = True
    Me.ckFatdie.Properties.AutoHeight = False
    Me.ckFatdie.Properties.Caption = "Fatture Differite Emesse"
    Me.ckFatdie.Size = New System.Drawing.Size(151, 19)
    Me.ckFatdie.TabIndex = 4
    '
    'ckCoreme
    '
    Me.ckCoreme.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckCoreme.EditValue = True
    Me.ckCoreme.Location = New System.Drawing.Point(3, 45)
    Me.ckCoreme.Name = "ckCoreme"
    Me.ckCoreme.NTSCheckValue = "S"
    Me.ckCoreme.NTSUnCheckValue = "N"
    Me.ckCoreme.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckCoreme.Properties.Appearance.Options.UseBackColor = True
    Me.ckCoreme.Properties.AutoHeight = False
    Me.ckCoreme.Properties.Caption = "Corrispettivi Emessi"
    Me.ckCoreme.Size = New System.Drawing.Size(132, 19)
    Me.ckCoreme.TabIndex = 5
    '
    'ckDdteme
    '
    Me.ckDdteme.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDdteme.EditValue = True
    Me.ckDdteme.Location = New System.Drawing.Point(3, 25)
    Me.ckDdteme.Name = "ckDdteme"
    Me.ckDdteme.NTSCheckValue = "S"
    Me.ckDdteme.NTSUnCheckValue = "N"
    Me.ckDdteme.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDdteme.Properties.Appearance.Options.UseBackColor = True
    Me.ckDdteme.Properties.AutoHeight = False
    Me.ckDdteme.Properties.Caption = "D.D.T. Emessi"
    Me.ckDdteme.Size = New System.Drawing.Size(99, 19)
    Me.ckDdteme.TabIndex = 6
    '
    'ckFatdir
    '
    Me.ckFatdir.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckFatdir.EditValue = True
    Me.ckFatdir.Location = New System.Drawing.Point(3, 125)
    Me.ckFatdir.Name = "ckFatdir"
    Me.ckFatdir.NTSCheckValue = "S"
    Me.ckFatdir.NTSUnCheckValue = "N"
    Me.ckFatdir.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckFatdir.Properties.Appearance.Options.UseBackColor = True
    Me.ckFatdir.Properties.AutoHeight = False
    Me.ckFatdir.Properties.Caption = "Fatture Differite Ricevute"
    Me.ckFatdir.Size = New System.Drawing.Size(157, 19)
    Me.ckFatdir.TabIndex = 10
    '
    'ckNotacr
    '
    Me.ckNotacr.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckNotacr.EditValue = True
    Me.ckNotacr.Location = New System.Drawing.Point(3, 105)
    Me.ckNotacr.Name = "ckNotacr"
    Me.ckNotacr.NTSCheckValue = "S"
    Me.ckNotacr.NTSUnCheckValue = "N"
    Me.ckNotacr.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNotacr.Properties.Appearance.Options.UseBackColor = True
    Me.ckNotacr.Properties.AutoHeight = False
    Me.ckNotacr.Properties.Caption = "Note di Accredito Ricevute"
    Me.ckNotacr.Size = New System.Drawing.Size(157, 19)
    Me.ckNotacr.TabIndex = 11
    '
    'ckNotade
    '
    Me.ckNotade.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckNotade.EditValue = True
    Me.ckNotade.Location = New System.Drawing.Point(3, 85)
    Me.ckNotade.Name = "ckNotade"
    Me.ckNotade.NTSCheckValue = "S"
    Me.ckNotade.NTSUnCheckValue = "N"
    Me.ckNotade.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNotade.Properties.Appearance.Options.UseBackColor = True
    Me.ckNotade.Properties.AutoHeight = False
    Me.ckNotade.Properties.Caption = "Note di Addebito Emesse"
    Me.ckNotade.Size = New System.Drawing.Size(151, 19)
    Me.ckNotade.TabIndex = 12
    '
    'lbOrdine
    '
    Me.lbOrdine.AutoSize = True
    Me.lbOrdine.BackColor = System.Drawing.Color.Transparent
    Me.lbOrdine.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbOrdine.Location = New System.Drawing.Point(10, 405)
    Me.lbOrdine.Name = "lbOrdine"
    Me.lbOrdine.NTSDbField = ""
    Me.lbOrdine.Size = New System.Drawing.Size(168, 13)
    Me.lbOrdine.TabIndex = 6
    Me.lbOrdine.Text = "Nessuna Operazione in corso"
    Me.lbOrdine.Tooltip = ""
    Me.lbOrdine.UseMnemonic = False
    '
    'fmFiltri
    '
    Me.fmFiltri.AllowDrop = True
    Me.fmFiltri.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmFiltri.Appearance.Options.UseBackColor = True
    Me.fmFiltri.Controls.Add(Me.lbTesto2)
    Me.fmFiltri.Controls.Add(Me.lbSerie)
    Me.fmFiltri.Controls.Add(Me.edDanumord)
    Me.fmFiltri.Controls.Add(Me.edAnumord)
    Me.fmFiltri.Controls.Add(Me.edSerie)
    Me.fmFiltri.Controls.Add(Me.lbAnord)
    Me.fmFiltri.Controls.Add(Me.lbAnno)
    Me.fmFiltri.Controls.Add(Me.lbDanord)
    Me.fmFiltri.Controls.Add(Me.edAnno)
    Me.fmFiltri.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmFiltri.Location = New System.Drawing.Point(13, 114)
    Me.fmFiltri.Name = "fmFiltri"
    Me.fmFiltri.Size = New System.Drawing.Size(430, 177)
    Me.fmFiltri.TabIndex = 7
    Me.fmFiltri.Text = "Filtri"
    '
    'lbTesto2
    '
    Me.lbTesto2.AutoSize = True
    Me.lbTesto2.BackColor = System.Drawing.Color.Transparent
    Me.lbTesto2.Location = New System.Drawing.Point(5, 30)
    Me.lbTesto2.Name = "lbTesto2"
    Me.lbTesto2.NTSDbField = ""
    Me.lbTesto2.Size = New System.Drawing.Size(364, 13)
    Me.lbTesto2.TabIndex = 19
    Me.lbTesto2.Text = "Il programma provvederà alla cancellazione di tutte le note di prelievo con:"
    Me.lbTesto2.Tooltip = ""
    Me.lbTesto2.UseMnemonic = False
    '
    'lbSerie
    '
    Me.lbSerie.AutoSize = True
    Me.lbSerie.BackColor = System.Drawing.Color.Transparent
    Me.lbSerie.Location = New System.Drawing.Point(5, 64)
    Me.lbSerie.Name = "lbSerie"
    Me.lbSerie.NTSDbField = ""
    Me.lbSerie.Size = New System.Drawing.Size(86, 13)
    Me.lbSerie.TabIndex = 18
    Me.lbSerie.Text = "Serie (* = tutte)"
    Me.lbSerie.Tooltip = ""
    Me.lbSerie.UseMnemonic = False
    '
    'fmDate
    '
    Me.fmDate.AllowDrop = True
    Me.fmDate.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmDate.Appearance.Options.UseBackColor = True
    Me.fmDate.Controls.Add(Me.lbDatcan)
    Me.fmDate.Controls.Add(Me.lbDatagg)
    Me.fmDate.Controls.Add(Me.lbUltcanc)
    Me.fmDate.Controls.Add(Me.lbUltagg)
    Me.fmDate.Controls.Add(Me.edDatrif)
    Me.fmDate.Controls.Add(Me.lbDatrif)
    Me.fmDate.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmDate.Location = New System.Drawing.Point(12, 297)
    Me.fmDate.Name = "fmDate"
    Me.fmDate.ShowCaption = False
    Me.fmDate.Size = New System.Drawing.Size(588, 72)
    Me.fmDate.TabIndex = 8
    '
    'lbDatcan
    '
    Me.lbDatcan.BackColor = System.Drawing.Color.Transparent
    Me.lbDatcan.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDatcan.Location = New System.Drawing.Point(466, 38)
    Me.lbDatcan.Name = "lbDatcan"
    Me.lbDatcan.NTSDbField = ""
    Me.lbDatcan.Size = New System.Drawing.Size(105, 20)
    Me.lbDatcan.TabIndex = 21
    Me.lbDatcan.Tooltip = ""
    Me.lbDatcan.UseMnemonic = False
    '
    'lbDatagg
    '
    Me.lbDatagg.BackColor = System.Drawing.Color.Transparent
    Me.lbDatagg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDatagg.Location = New System.Drawing.Point(466, 12)
    Me.lbDatagg.Name = "lbDatagg"
    Me.lbDatagg.NTSDbField = ""
    Me.lbDatagg.Size = New System.Drawing.Size(105, 20)
    Me.lbDatagg.TabIndex = 20
    Me.lbDatagg.Tooltip = ""
    Me.lbDatagg.UseMnemonic = False
    '
    'lbUltcanc
    '
    Me.lbUltcanc.AutoSize = True
    Me.lbUltcanc.BackColor = System.Drawing.Color.Transparent
    Me.lbUltcanc.Location = New System.Drawing.Point(299, 39)
    Me.lbUltcanc.Name = "lbUltcanc"
    Me.lbUltcanc.NTSDbField = ""
    Me.lbUltcanc.Size = New System.Drawing.Size(127, 13)
    Me.lbUltcanc.TabIndex = 19
    Me.lbUltcanc.Text = "Data ultima cancellazione"
    Me.lbUltcanc.Tooltip = ""
    Me.lbUltcanc.UseMnemonic = False
    '
    'lbUltagg
    '
    Me.lbUltagg.AutoSize = True
    Me.lbUltagg.BackColor = System.Drawing.Color.Transparent
    Me.lbUltagg.Location = New System.Drawing.Point(299, 13)
    Me.lbUltagg.Name = "lbUltagg"
    Me.lbUltagg.NTSDbField = ""
    Me.lbUltagg.Size = New System.Drawing.Size(136, 13)
    Me.lbUltagg.TabIndex = 18
    Me.lbUltagg.Text = "Data ultimo aggiornamento"
    Me.lbUltagg.Tooltip = ""
    Me.lbUltagg.UseMnemonic = False
    '
    'edDatrif
    '
    Me.edDatrif.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatrif.EditValue = "01/01/1900"
    Me.edDatrif.Location = New System.Drawing.Point(114, 23)
    Me.edDatrif.Name = "edDatrif"
    Me.edDatrif.NTSDbField = ""
    Me.edDatrif.NTSForzaVisZoom = False
    Me.edDatrif.NTSOldValue = ""
    Me.edDatrif.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatrif.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatrif.Properties.AutoHeight = False
    Me.edDatrif.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatrif.Properties.MaxLength = 65536
    Me.edDatrif.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatrif.Size = New System.Drawing.Size(100, 20)
    Me.edDatrif.TabIndex = 10
    '
    'lbDatrif
    '
    Me.lbDatrif.AutoSize = True
    Me.lbDatrif.BackColor = System.Drawing.Color.Transparent
    Me.lbDatrif.Location = New System.Drawing.Point(6, 26)
    Me.lbDatrif.Name = "lbDatrif"
    Me.lbDatrif.NTSDbField = ""
    Me.lbDatrif.Size = New System.Drawing.Size(96, 13)
    Me.lbDatrif.TabIndex = 6
    Me.lbDatrif.Text = "Data di riferimento"
    Me.lbDatrif.Tooltip = ""
    Me.lbDatrif.UseMnemonic = False
    '
    'FRMMGCMMA
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(619, 427)
    Me.Controls.Add(Me.fmDate)
    Me.Controls.Add(Me.lbOrdine)
    Me.Controls.Add(Me.fmDoccan)
    Me.Controls.Add(Me.fmAttenzione)
    Me.Controls.Add(Me.fmFiltri)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRMMGCMMA"
    Me.Text = "CANCELLAZIONE MOVIMENTI DI MAGAZZINO"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmAttenzione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmAttenzione.ResumeLayout(False)
    Me.fmAttenzione.PerformLayout()
    CType(Me.cbTipo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnumord.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDanumord.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmDoccan, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmDoccan.ResumeLayout(False)
    CType(Me.pnCheck3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCheck3.ResumeLayout(False)
    Me.pnCheck3.PerformLayout()
    CType(Me.ckRicfis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckFatrif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckRierif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckFatrfd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNadr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNade.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnCheck2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCheck2.ResumeLayout(False)
    Me.pnCheck2.PerformLayout()
    CType(Me.ckFatimr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNotace.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDdtric.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckBolmoi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckScapro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckCarpro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnCheck1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCheck1.ResumeLayout(False)
    Me.pnCheck1.PerformLayout()
    CType(Me.ckFatime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckFatdie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckCoreme.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDdteme.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckFatdir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNotacr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNotade.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmFiltri, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmFiltri.ResumeLayout(False)
    Me.fmFiltri.PerformLayout()
    CType(Me.fmDate, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmDate.ResumeLayout(False)
    Me.fmDate.PerformLayout()
    CType(Me.edDatrif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      Try
        tlbSelAll.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\add_filter.gif")
        tlbDesAll.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\del_filter.gif")
        tlbEsegui.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try

      edDatrif.NTSSetParam(oMenu, oApp.Tr(Me, 128625871870625000, "Data riferimento"), False)
      edDanumord.NTSSetParam(oMenu, oApp.Tr(Me, 128625871871093750, "Da numero"), "0", 9, 0, 999999999)
      edAnumord.NTSSetParam(oMenu, oApp.Tr(Me, 128625871871250000, "A numero"), "0", 9, 0, 999999999)
      edSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128625871871406250, "Serie"), CLN__STD.SerieMaxLen)
      edAnno.NTSSetParam(oMenu, oApp.Tr(Me, 128625871872031250, "Anno"), "0", 4, 0, 2099)
      ckFatrfd.NTSSetParam(oMenu, oApp.Tr(Me, 128625871872187500, "Fatt./ricev. fisc. differ."), "S", "N")
      ckRicfis.NTSSetParam(oMenu, oApp.Tr(Me, 128625871872343750, "Ricevute fiscali"), "S", "N")
      ckRierif.NTSSetParam(oMenu, oApp.Tr(Me, 128625871872500000, "Riem. ricev. fiscali"), "S", "N")
      ckFatrif.NTSSetParam(oMenu, oApp.Tr(Me, 128625871872656250, "Fatt./ricev. fiscali"), "S", "N")
      ckNotade.NTSSetParam(oMenu, oApp.Tr(Me, 128625871872812500, "Note di addebito emesse"), "S", "N")
      ckNotacr.NTSSetParam(oMenu, oApp.Tr(Me, 128625871872968750, "Note Accr. ricevute"), "S", "N")
      ckFatdir.NTSSetParam(oMenu, oApp.Tr(Me, 128625871873125000, "Fatture Diff. ricevute"), "S", "N")
      ckCarpro.NTSSetParam(oMenu, oApp.Tr(Me, 128625871873281250, "Carico da produz."), "S", "N")
      ckScapro.NTSSetParam(oMenu, oApp.Tr(Me, 128625871873437500, "Scarico da produz."), "S", "N")
      ckBolmoi.NTSSetParam(oMenu, oApp.Tr(Me, 128625871873593750, "Bolle di mov. interna"), "S", "N")
      ckDdteme.NTSSetParam(oMenu, oApp.Tr(Me, 128625871873750000, "D.D.T. emessi"), "S", "N")
      ckCoreme.NTSSetParam(oMenu, oApp.Tr(Me, 128625871873906250, "Corrispettivi emessi"), "S", "N")
      ckFatdie.NTSSetParam(oMenu, oApp.Tr(Me, 128625871874062500, "Fatture Diff. emesse"), "S", "N")
      ckFatimr.NTSSetParam(oMenu, oApp.Tr(Me, 128625871874218750, "Fatture Imm. ricevute"), "S", "N")
      ckDdtric.NTSSetParam(oMenu, oApp.Tr(Me, 128625871874375000, "D.D.T. ricevuti"), "S", "N")
      ckNotace.NTSSetParam(oMenu, oApp.Tr(Me, 128625871874531250, "Note Accr. emesse"), "S", "N")
      ckFatime.NTSSetParam(oMenu, oApp.Tr(Me, 129022393071785531, "Fatture imm. emesse"), "S", "N")
      ckNade.NTSSetParam(oMenu, oApp.Tr(Me, 129243535099550781, "Note Accr. differite emesse"), "S", "N")
      ckNadr.NTSSetParam(oMenu, oApp.Tr(Me, 129243535082753906, "Note Accr. differite ricevute"), "S", "N")

      cbTipo.NTSSetParam(oApp.Tr(Me, 129022393096629281, "Documenti da cancellare"))


      NTSScriptExec("InitControls", Me, Nothing)

    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Public Overridable Sub CaricaCombo()
    Try
      Dim dttTipo As New DataTable()

      dttTipo.Columns.Add("cod", GetType(String))
      dttTipo.Columns.Add("val", GetType(String))
      dttTipo.Rows.Add(New Object() {"0", "Tutti i documenti di magazzino fino alla data indicata compresa."})
      dttTipo.Rows.Add(New Object() {"1", "Tutte le note di prelievo fino alla data indicata compresa."})
      dttTipo.AcceptChanges()

      cbTipo.DataSource = dttTipo
      cbTipo.ValueMember = "cod"
      cbTipo.DisplayMember = "val"
      cbTipo.SelectedValue = "0"
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub

  Public Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    If Not IsMyThrowRemoteEvent() Then Return 'il messaggio non è per questa form ...
    MyBase.GestisciEventiEntity(sender, e)
    '---------------------------------
    'questa funzione riceve gli eventi dall'ENTITY
    e.RetValue = ""
    If e.Message <> "" Then
      Select Case e.TipoEvento
        Case "LABEL"
          lbOrdine.Text = e.Message
          lbOrdine.Refresh()
          Me.Refresh()
      End Select
    End If
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRMMGCMMA_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      InitControls()

      edAnno.Text = NTSCStr(Today.Year)
      oCleCmma.LeggiDate(lbDatagg.Text, lbDatcan.Text)

      CaricaCombo()
      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      'e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipodocumento, ecc ...
      'GctlTipoDoc = ""
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGCMMA_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If tlbEsegui.Enabled = False Then
        e.Cancel = True
        oApp.MsgBoxErr(oApp.Tr(Me, 128625652938125000, "Attendere che l'esecuzione sia terminata"))
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Toolbar"
  Public Overridable Sub tlbSelAll_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSelAll.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      SelezionaDeselezionaCheck(True)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub tlbDesAll_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDesAll.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      SelezionaDeselezionaCheck(False)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbEsegui_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsegui.ItemClick
    Try
      Esegui()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Validated/Checked"
  Public Overridable Sub edDanumord_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDanumord.Validated
    Try
      If Not oCleCmma.ControllaMaggioreNum(NTSCInt(edDanumord.Text), NTSCInt(edAnumord.Text)) Then
        edDanumord.Text = "0"
      ElseIf edAnumord.Text = "999999999" Then
        edAnumord.Text = edDanumord.Text
      End If
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAnumord_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAnumord.Validated
    Try
      If oCleCmma Is Nothing Then Return
      If Not oCleCmma.ControllaMaggioreNum(NTSCInt(edDanumord.Text), NTSCInt(edAnumord.Text)) Then
        edAnumord.Text = "99999999"
      End If
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAnno_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAnno.Validated
    Try
      If oCleCmma Is Nothing Then Return
      If NTSCInt(edAnno.Text) > 0 And NTSCInt(edAnno.Text) < 1900 Then
        edAnno.Text = NTSCStr(Today.Year)
        oApp.MsgBoxErr(oApp.Tr(Me, 128624229570781250, "Il campo 'Anno' deve essere 0 (= qualsiasi) o compreso tra 1900 e 2099)"))
      End If
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cbTipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTipo.SelectedIndexChanged
    Try
      If cbTipo.SelectedValue = "0" Then
        fmDoccan.Visible = True
        fmDoccan.Enabled = True
        fmFiltri.Visible = False
        fmFiltri.Enabled = False
      Else
        fmDoccan.Visible = False
        fmDoccan.Enabled = False
        fmFiltri.Visible = True
        fmFiltri.Enabled = True
      End If
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckCarpro_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckCarpro.CheckedChanged
    Try
      ckScapro.Checked = ckCarpro.Checked
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckScapro_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckScapro.CheckedChanged
    Try
      ckCarpro.Checked = ckScapro.Checked
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edSerie_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSerie.Validated
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edSerie.Text, False)
      If strTmp <> edSerie.Text Then edSerie.Text = strTmp

    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub Esegui()
    Dim dlgProcedi As DialogResult
    Dim strWhere As String = ""
    Dim bAggiornaAnaz As Boolean = False

    Try
      Me.ValidaLastControl()

      If Not oCleCmma.ComponiTestoDaCk(ckFatime.Checked, ckDdteme.Checked, ckCoreme.Checked, ckFatdie.Checked, _
                                       ckNotade.Checked, ckNotacr.Checked, ckFatdir.Checked, ckFatimr.Checked, _
                                       ckDdtric.Checked, ckNotace.Checked, ckCarpro.Checked, ckScapro.Checked, _
                                       ckBolmoi.Checked, ckRicfis.Checked, ckRierif.Checked, ckFatrif.Checked, _
                                       ckFatrfd.Checked, strWhere, bAggiornaAnaz, ckNade.Checked, ckNadr.Checked) Then Return

      If cbTipo.SelectedValue = "0" Then
        If NTSCDate(edDatrif.Text) > NTSCDate(lbDatagg.Text) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128625936550468750, "La data fino alla quale devono essere cancellati i movimenti" & _
                                              vbCrLf & "deve essere minore o uguale della data di ultimo aggiornamento."))
          Return
        End If
      End If

      dlgProcedi = oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128625969448906250, _
                   "ATTENZIONE! Saranno cancellati i movimenti di magazzino selezionati, procedere?"))

      If dlgProcedi = Windows.Forms.DialogResult.No Then
        Return
      End If

      tlbEsegui.Enabled = False
      '------ ELABORAZIONE -----------
      If cbTipo.SelectedValue = "0" Then
        If oCleCmma.EseguiDoc(strWhere, edDatrif.Text, bAggiornaAnaz) Then oApp.MsgBoxInfo(oApp.Tr(Me, 129022393161785531, "Elaborazione Completata"))
      Else
        If CBool(bsModSupRME And oMenu.ModuliSupDittaDitt(DittaCorrente)) Then
          If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 129418260352392941, "Cancellare anche i picking collegati alle note di prelievo?")) = Windows.Forms.DialogResult.Yes Then
            oCleCmma.bCancellaPicking = True
          Else
            oCleCmma.bCancellaPicking = False
          End If
        End If

        If oCleCmma.EseguiNote(edSerie.Text, edDatrif.Text, NTSCInt(edAnno.Text), NTSCInt(edDanumord.Text), _
               NTSCInt(edAnumord.Text)) Then oApp.MsgBoxInfo(oApp.Tr(Me, 129022393178504281, "Elaborazione Completata"))
      End If
      '------ FINE ELABORA -----------

      tlbEsegui.Enabled = True
      lbOrdine.Text = oApp.Tr(Me, 129083656330471117, "Nessuna operazione in corso.")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      tlbEsegui.Enabled = True
    End Try
  End Sub

  Public Overridable Sub SelezionaDeselezionaCheck(ByVal bSeleziona As Boolean)
    Try
      '-------------------------------------------------------------------------------------------------------------
      ckFatime.Checked = bSeleziona
      ckDdteme.Checked = bSeleziona
      ckCoreme.Checked = bSeleziona
      ckFatdie.Checked = bSeleziona
      ckNotade.Checked = bSeleziona
      ckNotacr.Checked = bSeleziona
      ckFatdir.Checked = bSeleziona
      ckFatimr.Checked = bSeleziona
      ckDdtric.Checked = bSeleziona
      ckNotace.Checked = bSeleziona
      ckCarpro.Checked = bSeleziona
      ckScapro.Checked = bSeleziona
      ckBolmoi.Checked = bSeleziona
      ckRicfis.Checked = bSeleziona
      ckRierif.Checked = bSeleziona
      ckFatrif.Checked = bSeleziona
      ckFatrfd.Checked = bSeleziona
      ckNade.Checked = bSeleziona
      ckNadr.Checked = bSeleziona
      '-------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class