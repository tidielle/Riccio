Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORCAOR_F

#Region "Dichiarazione variabili"
  Public oCleCaor As CLEORCAOR
  Public oCallParams As CLE__CLDP
  Public strCodagedaOld As String = "0"
  Public strDescrdaOld As String = ""
  Public strCodageaOld As String = "0"
  Public strDescraOld As String = ""

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
  Public WithEvents ckSerie As NTSInformatica.NTSCheckBox
  Public WithEvents edSerie As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDanord As NTSInformatica.NTSLabel
  Public WithEvents lbDatord As NTSInformatica.NTSLabel
  Public WithEvents lbAnno As NTSInformatica.NTSLabel
  Public WithEvents edAnumord As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAnord As NTSInformatica.NTSLabel
  Public WithEvents edDatord As NTSInformatica.NTSTextBoxData
  Public WithEvents edDanumord As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAnno As NTSInformatica.NTSTextBoxNum
  Public WithEvents fmOrcaor As NTSInformatica.NTSGroupBox
  Public WithEvents lbOrdine As NTSInformatica.NTSLabel
#End Region

#Region "Moduli"
  Private Moduli_P As Integer = bsModOR
  Private ModuliExt_P As Integer = bsModExtORE
  Private ModuliSup_P As Integer = 0
  Private ModuliSupExt_P As Integer = 0
  Private ModuliPtn_P As Integer = 0
  Public WithEvents lbDatords As NTSInformatica.NTSLabel
  Public WithEvents ckFattur As NTSInformatica.NTSCheckBox
  Public WithEvents ckOrdfor As NTSInformatica.NTSCheckBox
  Public WithEvents ckPreven As NTSInformatica.NTSCheckBox
  Public WithEvents ckImpcli As NTSInformatica.NTSCheckBox
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNORCAOR", "BEORCAOR", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128589409211093750, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleCaor = CType(oTmp, CLEORCAOR)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNORCAOR", strRemoteServer, strRemotePort)
    AddHandler oCleCaor.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleCaor.Init(oApp, NTSScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMORCAOR_F))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbEsegui = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.fmAttenzione = New NTSInformatica.NTSGroupBox
    Me.lbDatords = New NTSInformatica.NTSLabel
    Me.edAnumord = New NTSInformatica.NTSTextBoxNum
    Me.lbAnord = New NTSInformatica.NTSLabel
    Me.edDatord = New NTSInformatica.NTSTextBoxData
    Me.edDanumord = New NTSInformatica.NTSTextBoxNum
    Me.edAnno = New NTSInformatica.NTSTextBoxNum
    Me.lbDanord = New NTSInformatica.NTSLabel
    Me.lbDatord = New NTSInformatica.NTSLabel
    Me.lbAnno = New NTSInformatica.NTSLabel
    Me.edSerie = New NTSInformatica.NTSTextBoxStr
    Me.ckSerie = New NTSInformatica.NTSCheckBox
    Me.fmOrcaor = New NTSInformatica.NTSGroupBox
    Me.ckOrdfor = New NTSInformatica.NTSCheckBox
    Me.ckPreven = New NTSInformatica.NTSCheckBox
    Me.ckImpcli = New NTSInformatica.NTSCheckBox
    Me.lbOrdine = New NTSInformatica.NTSLabel
    Me.ckFattur = New NTSInformatica.NTSCheckBox
    Me.fmFattur = New NTSInformatica.NTSGroupBox
    Me.NtsLabel1 = New NTSInformatica.NTSLabel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmAttenzione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmAttenzione.SuspendLayout()
    CType(Me.edAnumord.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatord.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDanumord.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmOrcaor, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmOrcaor.SuspendLayout()
    CType(Me.ckOrdfor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckPreven.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckImpcli.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckFattur.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmFattur, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmFattur.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbGuida, Me.tlbEsci, Me.tlbEsegui})
    Me.NtsBarManager1.MaxItemId = 14
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsegui), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbEsegui.Id = 13
    Me.tlbEsegui.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F7))
    Me.tlbEsegui.Name = "tlbEsegui"
    Me.tlbEsegui.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 11
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 12
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'fmAttenzione
    '
    Me.fmAttenzione.AllowDrop = True
    Me.fmAttenzione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmAttenzione.Appearance.Options.UseBackColor = True
    Me.fmAttenzione.Controls.Add(Me.lbDatords)
    Me.fmAttenzione.Controls.Add(Me.edAnumord)
    Me.fmAttenzione.Controls.Add(Me.lbAnord)
    Me.fmAttenzione.Controls.Add(Me.edDatord)
    Me.fmAttenzione.Controls.Add(Me.edDanumord)
    Me.fmAttenzione.Controls.Add(Me.edAnno)
    Me.fmAttenzione.Controls.Add(Me.lbDanord)
    Me.fmAttenzione.Controls.Add(Me.lbDatord)
    Me.fmAttenzione.Controls.Add(Me.lbAnno)
    Me.fmAttenzione.Controls.Add(Me.edSerie)
    Me.fmAttenzione.Controls.Add(Me.ckSerie)
    Me.fmAttenzione.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmAttenzione.Location = New System.Drawing.Point(12, 82)
    Me.fmAttenzione.Name = "fmAttenzione"
    Me.fmAttenzione.ShowCaption = False
    Me.fmAttenzione.Size = New System.Drawing.Size(529, 107)
    Me.fmAttenzione.TabIndex = 4
    '
    'lbDatords
    '
    Me.lbDatords.AutoSize = True
    Me.lbDatords.BackColor = System.Drawing.Color.Transparent
    Me.lbDatords.Location = New System.Drawing.Point(249, 12)
    Me.lbDatords.Name = "lbDatords"
    Me.lbDatords.NTSDbField = ""
    Me.lbDatords.Size = New System.Drawing.Size(53, 13)
    Me.lbDatords.TabIndex = 18
    Me.lbDatords.Text = "compreso"
    Me.lbDatords.Tooltip = ""
    Me.lbDatords.UseMnemonic = False
    '
    'edAnumord
    '
    Me.edAnumord.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnumord.EditValue = "999999999"
    Me.edAnumord.Location = New System.Drawing.Point(381, 78)
    Me.edAnumord.Name = "edAnumord"
    Me.edAnumord.NTSDbField = ""
    Me.edAnumord.NTSFormat = "0"
    Me.edAnumord.NTSForzaVisZoom = False
    Me.edAnumord.NTSOldValue = "999999999"
    Me.edAnumord.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnumord.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnumord.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnumord.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnumord.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnumord.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnumord.Size = New System.Drawing.Size(100, 20)
    Me.edAnumord.TabIndex = 17
    '
    'lbAnord
    '
    Me.lbAnord.AutoSize = True
    Me.lbAnord.BackColor = System.Drawing.Color.Transparent
    Me.lbAnord.Location = New System.Drawing.Point(287, 81)
    Me.lbAnord.Name = "lbAnord"
    Me.lbAnord.NTSDbField = ""
    Me.lbAnord.Size = New System.Drawing.Size(90, 13)
    Me.lbAnord.TabIndex = 16
    Me.lbAnord.Text = "al Numero Ordine"
    Me.lbAnord.Tooltip = ""
    Me.lbAnord.UseMnemonic = False
    '
    'edDatord
    '
    Me.edDatord.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatord.EditValue = "01/01/1900"
    Me.edDatord.Location = New System.Drawing.Point(161, 9)
    Me.edDatord.Name = "edDatord"
    Me.edDatord.NTSDbField = ""
    Me.edDatord.NTSForzaVisZoom = False
    Me.edDatord.NTSOldValue = ""
    Me.edDatord.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatord.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatord.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatord.Properties.MaxLength = 65536
    Me.edDatord.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatord.Size = New System.Drawing.Size(82, 20)
    Me.edDatord.TabIndex = 10
    '
    'edDanumord
    '
    Me.edDanumord.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDanumord.EditValue = "0"
    Me.edDanumord.Location = New System.Drawing.Point(161, 78)
    Me.edDanumord.Name = "edDanumord"
    Me.edDanumord.NTSDbField = ""
    Me.edDanumord.NTSFormat = "0"
    Me.edDanumord.NTSForzaVisZoom = False
    Me.edDanumord.NTSOldValue = ""
    Me.edDanumord.Properties.Appearance.Options.UseTextOptions = True
    Me.edDanumord.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDanumord.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDanumord.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDanumord.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDanumord.Properties.MaxLength = 65536
    Me.edDanumord.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDanumord.Size = New System.Drawing.Size(100, 20)
    Me.edDanumord.TabIndex = 9
    '
    'edAnno
    '
    Me.edAnno.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnno.Location = New System.Drawing.Point(161, 55)
    Me.edAnno.Name = "edAnno"
    Me.edAnno.NTSDbField = ""
    Me.edAnno.NTSFormat = "0"
    Me.edAnno.NTSForzaVisZoom = False
    Me.edAnno.NTSOldValue = ""
    Me.edAnno.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnno.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnno.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnno.Properties.AppearanceDisabled.Options.UseForeColor = True
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
    Me.lbDanord.Location = New System.Drawing.Point(10, 81)
    Me.lbDanord.Name = "lbDanord"
    Me.lbDanord.NTSDbField = ""
    Me.lbDanord.Size = New System.Drawing.Size(97, 13)
    Me.lbDanord.TabIndex = 7
    Me.lbDanord.Text = "Dal Numero Ordine"
    Me.lbDanord.Tooltip = ""
    Me.lbDanord.UseMnemonic = False
    '
    'lbDatord
    '
    Me.lbDatord.AutoSize = True
    Me.lbDatord.BackColor = System.Drawing.Color.Transparent
    Me.lbDatord.Location = New System.Drawing.Point(10, 12)
    Me.lbDatord.Name = "lbDatord"
    Me.lbDatord.NTSDbField = ""
    Me.lbDatord.Size = New System.Drawing.Size(121, 13)
    Me.lbDatord.TabIndex = 6
    Me.lbDatord.Text = "Data ordine anteriore al"
    Me.lbDatord.Tooltip = ""
    Me.lbDatord.UseMnemonic = False
    '
    'lbAnno
    '
    Me.lbAnno.AutoSize = True
    Me.lbAnno.BackColor = System.Drawing.Color.Transparent
    Me.lbAnno.Location = New System.Drawing.Point(10, 58)
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
    Me.edSerie.Enabled = False
    Me.edSerie.Location = New System.Drawing.Point(161, 32)
    Me.edSerie.Name = "edSerie"
    Me.edSerie.NTSDbField = ""
    Me.edSerie.NTSForzaVisZoom = False
    Me.edSerie.NTSOldValue = ""
    Me.edSerie.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSerie.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSerie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSerie.Properties.MaxLength = 65536
    Me.edSerie.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSerie.Size = New System.Drawing.Size(34, 20)
    Me.edSerie.TabIndex = 2
    '
    'ckSerie
    '
    Me.ckSerie.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.ckSerie.Location = New System.Drawing.Point(9, 33)
    Me.ckSerie.Name = "ckSerie"
    Me.ckSerie.NTSCheckValue = "S"
    Me.ckSerie.NTSUnCheckValue = "N"
    Me.ckSerie.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSerie.Properties.Appearance.Options.UseBackColor = True
    Me.ckSerie.Properties.Caption = "Considera serie ordine"
    Me.ckSerie.Size = New System.Drawing.Size(132, 19)
    Me.ckSerie.TabIndex = 1
    '
    'fmOrcaor
    '
    Me.fmOrcaor.AllowDrop = True
    Me.fmOrcaor.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmOrcaor.Appearance.Options.UseBackColor = True
    Me.fmOrcaor.Controls.Add(Me.ckOrdfor)
    Me.fmOrcaor.Controls.Add(Me.ckPreven)
    Me.fmOrcaor.Controls.Add(Me.ckImpcli)
    Me.fmOrcaor.Cursor = System.Windows.Forms.Cursors.Hand
    Me.fmOrcaor.Location = New System.Drawing.Point(12, 195)
    Me.fmOrcaor.Name = "fmOrcaor"
    Me.fmOrcaor.Size = New System.Drawing.Size(141, 92)
    Me.fmOrcaor.TabIndex = 5
    Me.fmOrcaor.Text = "Documenti da cancellare"
    '
    'ckOrdfor
    '
    Me.ckOrdfor.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckOrdfor.EditValue = True
    Me.ckOrdfor.Location = New System.Drawing.Point(13, 44)
    Me.ckOrdfor.Name = "ckOrdfor"
    Me.ckOrdfor.NTSCheckValue = "S"
    Me.ckOrdfor.NTSUnCheckValue = "N"
    Me.ckOrdfor.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckOrdfor.Properties.Appearance.Options.UseBackColor = True
    Me.ckOrdfor.Properties.Caption = "Ordine Fornitore"
    Me.ckOrdfor.Size = New System.Drawing.Size(107, 19)
    Me.ckOrdfor.TabIndex = 6
    '
    'ckPreven
    '
    Me.ckPreven.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckPreven.EditValue = True
    Me.ckPreven.Location = New System.Drawing.Point(13, 66)
    Me.ckPreven.Name = "ckPreven"
    Me.ckPreven.NTSCheckValue = "S"
    Me.ckPreven.NTSUnCheckValue = "N"
    Me.ckPreven.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPreven.Properties.Appearance.Options.UseBackColor = True
    Me.ckPreven.Properties.Caption = "Preventivo"
    Me.ckPreven.Size = New System.Drawing.Size(89, 19)
    Me.ckPreven.TabIndex = 4
    '
    'ckImpcli
    '
    Me.ckImpcli.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckImpcli.EditValue = True
    Me.ckImpcli.Location = New System.Drawing.Point(13, 22)
    Me.ckImpcli.Name = "ckImpcli"
    Me.ckImpcli.NTSCheckValue = "S"
    Me.ckImpcli.NTSUnCheckValue = "N"
    Me.ckImpcli.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckImpcli.Properties.Appearance.Options.UseBackColor = True
    Me.ckImpcli.Properties.Caption = "Impegno Cliente"
    Me.ckImpcli.Size = New System.Drawing.Size(107, 19)
    Me.ckImpcli.TabIndex = 3
    '
    'lbOrdine
    '
    Me.lbOrdine.AutoSize = True
    Me.lbOrdine.BackColor = System.Drawing.Color.Transparent
    Me.lbOrdine.Location = New System.Drawing.Point(9, 299)
    Me.lbOrdine.Name = "lbOrdine"
    Me.lbOrdine.NTSDbField = ""
    Me.lbOrdine.Size = New System.Drawing.Size(150, 13)
    Me.lbOrdine.TabIndex = 6
    Me.lbOrdine.Text = "Nessuna Operazione in corso."
    Me.lbOrdine.Tooltip = ""
    Me.lbOrdine.UseMnemonic = False
    '
    'ckFattur
    '
    Me.ckFattur.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckFattur.Location = New System.Drawing.Point(14, 10)
    Me.ckFattur.Name = "ckFattur"
    Me.ckFattur.NTSCheckValue = "S"
    Me.ckFattur.NTSUnCheckValue = "N"
    Me.ckFattur.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckFattur.Properties.Appearance.Options.UseBackColor = True
    Me.ckFattur.Properties.Caption = "Solo Impegni Clienti e Ordini Fornitori riferiti a documenti già fatturati"
    Me.ckFattur.Size = New System.Drawing.Size(360, 19)
    Me.ckFattur.TabIndex = 7
    '
    'fmFattur
    '
    Me.fmFattur.AllowDrop = True
    Me.fmFattur.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmFattur.Appearance.Options.UseBackColor = True
    Me.fmFattur.Controls.Add(Me.ckFattur)
    Me.fmFattur.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmFattur.Location = New System.Drawing.Point(159, 249)
    Me.fmFattur.Name = "fmFattur"
    Me.fmFattur.ShowCaption = False
    Me.fmFattur.Size = New System.Drawing.Size(382, 38)
    Me.fmFattur.TabIndex = 8
    '
    'NtsLabel1
    '
    Me.NtsLabel1.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel1.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Italic)
    Me.NtsLabel1.Location = New System.Drawing.Point(9, 33)
    Me.NtsLabel1.Name = "NtsLabel1"
    Me.NtsLabel1.NTSDbField = ""
    Me.NtsLabel1.Size = New System.Drawing.Size(532, 46)
    Me.NtsLabel1.TabIndex = 9
    Me.NtsLabel1.Text = "ATTENZIONE!" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Il programma provvederà alla cancellazione di tutti gli Ordini evasi" & _
        " con:"
    Me.NtsLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter
    Me.NtsLabel1.Tooltip = ""
    Me.NtsLabel1.UseMnemonic = False
    '
    'FRMORCAOR_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(558, 323)
    Me.Controls.Add(Me.NtsLabel1)
    Me.Controls.Add(Me.fmFattur)
    Me.Controls.Add(Me.lbOrdine)
    Me.Controls.Add(Me.fmOrcaor)
    Me.Controls.Add(Me.fmAttenzione)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMORCAOR_F"
    Me.Text = "CANCELLAZIONE ORDINI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmAttenzione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmAttenzione.ResumeLayout(False)
    Me.fmAttenzione.PerformLayout()
    CType(Me.edAnumord.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatord.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDanumord.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmOrcaor, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmOrcaor.ResumeLayout(False)
    Me.fmOrcaor.PerformLayout()
    CType(Me.ckOrdfor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckPreven.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckImpcli.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckFattur.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmFattur, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmFattur.ResumeLayout(False)
    Me.fmFattur.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      Try
        tlbEsegui.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try


      edAnumord.NTSSetParam(oMenu, oApp.Tr(Me, 128624196256250000, "a numero ordine"), "0", 9, 0, 999999999)
      edDatord.NTSSetParam(oMenu, oApp.Tr(Me, 128624196304843750, "Data ordine anteriore al:"), False)
      edDanumord.NTSSetParam(oMenu, oApp.Tr(Me, 128624196327031250, "da numero ordine"), "0", 9, 0, 999999999)
      edAnno.NTSSetParam(oMenu, oApp.Tr(Me, 128624196367343750, "anno"), "0", 4, 0, 2099)
      edSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128624196384531250, "serie ordine"), CLN__STD.SerieMaxLen)
      ckSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128624196410468750, "Considera serie ordine"), "S", "N")
      ckFattur.NTSSetParam(oMenu, oApp.Tr(Me, 128624190583593750, "Solo Impegni clienti, Ordini fornitori e Ordini di produzione riferiti a documenti già fatturati"), "S", "N")
      ckOrdfor.NTSSetParam(oMenu, oApp.Tr(Me, 128624190583906250, "Ordine fornitore"), "S", "N")
      ckPreven.NTSSetParam(oMenu, oApp.Tr(Me, 128624190584218750, "Preventivo"), "S", "N")
      ckImpcli.NTSSetParam(oMenu, oApp.Tr(Me, 128624190584375000, "Impegno cliente"), "S", "N")

      NTSScriptExec("InitControls", Me, Nothing)

    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Public Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    Dim i As Integer = 0
    Dim strT() As String = Nothing
    Dim strTmp As String = ""

    Try
      If Not IsMyThrowRemoteEvent() Then Return 'il messaggio non è per questa form ...
      MyBase.GestisciEventiEntity(sender, e)

      '---------------------------------
      'questa funzione riceve gli eventi dall'ENTITY
      e.RetValue = ""
      If e.Message <> "" Then
        Select Case e.TipoEvento
          Case "LABEL"
            lbOrdine.Text = e.Message
            Me.Refresh()
            Application.DoEvents()  'altrimenti non aggiorna la label
        End Select
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try

  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRMORCAOR_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      InitControls()

      edAnno.Text = NTSCStr(Today.Year)
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

  Public Overridable Sub FRMORCAOR_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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
      If Not oCleCaor.ControllaMaggioreNum(NTSCInt(edDanumord.Text), NTSCInt(edAnumord.Text)) Then
        edDanumord.Text = "0"
      End If
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAnumord_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAnumord.Validated
    Try
      If oCleCaor Is Nothing Then Return
      If Not oCleCaor.ControllaMaggioreNum(NTSCInt(edDanumord.Text), NTSCInt(edAnumord.Text)) Then
        edAnumord.Text = "999999999"
      End If
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAnno_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAnno.Validated
    Try
      If oCleCaor Is Nothing Then Return
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

  Public Overridable Sub ckSerie_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSerie.CheckedChanged
    Try
      If ckSerie.Checked Then
        edSerie.Enabled = True
        edSerie.Focus()
      Else
        edSerie.Text = ""
        edSerie.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckFattur_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckFattur.CheckedChanged
    Try
      If ckFattur.Checked Then
        ckPreven.Enabled = False
        ckPreven.Checked = False
      Else
        ckPreven.Enabled = True
      End If
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
    Dim strSerie As String = edSerie.Text

    Try
      Me.ValidaLastControl()

      If Not oCleCaor.ComponiTestoDaCk(False, ckOrdfor.Checked, False, ckImpcli.Checked, False, False, _
                                       ckPreven.Checked, strWhere) Then Return

      dlgProcedi = oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128624232824218750, _
                   "ATTENZIONE! Saranno cancellati tutti gli ordini evasi selezionati, procedere?"))

      If dlgProcedi = Windows.Forms.DialogResult.No Then
        Return
      End If

      tlbEsegui.Enabled = False
      '------ ELABORAZIONE -----------
      oCleCaor.ElaboraSerie(ckSerie.Checked, strSerie)

      If oCleCaor.Esegui(False, ckOrdfor.Checked, False, ckImpcli.Checked, False, False, ckPreven.Checked, _
                         ckFattur.Checked, NTSCInt(edAnno.Text), edDatord.Text, _
                         NTSCInt(edDanumord.Text), NTSCInt(edAnumord.Text), strWhere, strSerie) Then

        oApp.MsgBoxInfo(oApp.Tr(Me, 128625036835781250, "Elaborazione Completata"))
      End If
      '------ FINE ELABORA -----------

      tlbEsegui.Enabled = True

      lbOrdine.Text = oApp.Tr(Me, 129083620334643286, "Nessuna operazione in corso.")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      tlbEsegui.Enabled = True
    End Try
  End Sub

End Class