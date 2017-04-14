Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORCAOR

#Region "Dichiarazione variabili"
  Public oCleCaor As CLEORCAOR
  Public oCallParams As CLE__CLDP
  Public strCodagedaOld As String = "0"
  Public strDescrdaOld As String = ""
  Public strCodageaOld As String = "0"
  Public strDescraOld As String = ""
  Private components As System.ComponentModel.IComponent

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
  Public WithEvents ckOrdfoa As NTSInformatica.NTSCheckBox
  Public WithEvents ckPreven As NTSInformatica.NTSCheckBox
  Public WithEvents ckImpcli As NTSInformatica.NTSCheckBox
  Public WithEvents ckImpcla As NTSInformatica.NTSCheckBox
  Public WithEvents ckImptra As NTSInformatica.NTSCheckBox
  Public WithEvents ckOrdpro As NTSInformatica.NTSCheckBox
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMORCAOR))
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
    Me.lbTesto = New NTSInformatica.NTSLabel
    Me.fmOrcaor = New NTSInformatica.NTSGroupBox
    Me.ckOrdfor = New NTSInformatica.NTSCheckBox
    Me.ckOrdfoa = New NTSInformatica.NTSCheckBox
    Me.ckPreven = New NTSInformatica.NTSCheckBox
    Me.ckImpcli = New NTSInformatica.NTSCheckBox
    Me.ckImpcla = New NTSInformatica.NTSCheckBox
    Me.ckImptra = New NTSInformatica.NTSCheckBox
    Me.ckOrdpro = New NTSInformatica.NTSCheckBox
    Me.lbOrdine = New NTSInformatica.NTSLabel
    Me.ckFattur = New NTSInformatica.NTSCheckBox
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
    CType(Me.ckOrdfoa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckPreven.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckImpcli.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckImpcla.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckImptra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckOrdpro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckFattur.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.fmAttenzione.Controls.Add(Me.lbTesto)
    Me.fmAttenzione.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmAttenzione.Location = New System.Drawing.Point(13, 37)
    Me.fmAttenzione.Name = "fmAttenzione"
    Me.fmAttenzione.Size = New System.Drawing.Size(461, 161)
    Me.fmAttenzione.TabIndex = 4
    Me.fmAttenzione.Text = "Attenzione!"
    '
    'lbDatords
    '
    Me.lbDatords.AutoSize = True
    Me.lbDatords.BackColor = System.Drawing.Color.Transparent
    Me.lbDatords.Location = New System.Drawing.Point(256, 51)
    Me.lbDatords.Name = "lbDatords"
    Me.lbDatords.NTSDbField = ""
    Me.lbDatords.Size = New System.Drawing.Size(57, 13)
    Me.lbDatords.TabIndex = 18
    Me.lbDatords.Text = "compresa."
    '
    'edAnumord
    '
    Me.edAnumord.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnumord.EditValue = "999999999"
    Me.edAnumord.Location = New System.Drawing.Point(344, 127)
    Me.edAnumord.Name = "edAnumord"
    Me.edAnumord.NTSDbField = ""
    Me.edAnumord.NTSFormat = "0"
    Me.edAnumord.NTSForzaVisZoom = False
    Me.edAnumord.NTSOldValue = "999999999"
    Me.edAnumord.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnumord.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnumord.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnumord.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnumord.Size = New System.Drawing.Size(100, 20)
    Me.edAnumord.TabIndex = 17
    '
    'lbAnord
    '
    Me.lbAnord.AutoSize = True
    Me.lbAnord.BackColor = System.Drawing.Color.Transparent
    Me.lbAnord.Location = New System.Drawing.Point(253, 130)
    Me.lbAnord.Name = "lbAnord"
    Me.lbAnord.NTSDbField = ""
    Me.lbAnord.Size = New System.Drawing.Size(85, 13)
    Me.lbAnord.TabIndex = 16
    Me.lbAnord.Text = "a numero ordine"
    '
    'edDatord
    '
    Me.edDatord.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatord.EditValue = "01/01/1900"
    Me.edDatord.Location = New System.Drawing.Point(146, 49)
    Me.edDatord.Name = "edDatord"
    Me.edDatord.NTSDbField = ""
    Me.edDatord.NTSForzaVisZoom = False
    Me.edDatord.NTSOldValue = ""
    Me.edDatord.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatord.Properties.MaxLength = 65536
    Me.edDatord.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatord.Size = New System.Drawing.Size(100, 20)
    Me.edDatord.TabIndex = 10
    '
    'edDanumord
    '
    Me.edDanumord.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDanumord.EditValue = "0"
    Me.edDanumord.Location = New System.Drawing.Point(147, 127)
    Me.edDanumord.Name = "edDanumord"
    Me.edDanumord.NTSDbField = ""
    Me.edDanumord.NTSFormat = "0"
    Me.edDanumord.NTSForzaVisZoom = False
    Me.edDanumord.NTSOldValue = ""
    Me.edDanumord.Properties.Appearance.Options.UseTextOptions = True
    Me.edDanumord.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDanumord.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDanumord.Properties.MaxLength = 65536
    Me.edDanumord.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDanumord.Size = New System.Drawing.Size(100, 20)
    Me.edDanumord.TabIndex = 9
    '
    'edAnno
    '
    Me.edAnno.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnno.Location = New System.Drawing.Point(147, 101)
    Me.edAnno.Name = "edAnno"
    Me.edAnno.NTSDbField = ""
    Me.edAnno.NTSFormat = "0"
    Me.edAnno.NTSForzaVisZoom = False
    Me.edAnno.NTSOldValue = ""
    Me.edAnno.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnno.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
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
    Me.lbDanord.Location = New System.Drawing.Point(10, 130)
    Me.lbDanord.Name = "lbDanord"
    Me.lbDanord.NTSDbField = ""
    Me.lbDanord.Size = New System.Drawing.Size(92, 13)
    Me.lbDanord.TabIndex = 7
    Me.lbDanord.Text = "Da numero ordine"
    '
    'lbDatord
    '
    Me.lbDatord.AutoSize = True
    Me.lbDatord.BackColor = System.Drawing.Color.Transparent
    Me.lbDatord.Location = New System.Drawing.Point(9, 52)
    Me.lbDatord.Name = "lbDatord"
    Me.lbDatord.NTSDbField = ""
    Me.lbDatord.Size = New System.Drawing.Size(125, 13)
    Me.lbDatord.TabIndex = 6
    Me.lbDatord.Text = "Data ordine anteriore al:"
    '
    'lbAnno
    '
    Me.lbAnno.AutoSize = True
    Me.lbAnno.BackColor = System.Drawing.Color.Transparent
    Me.lbAnno.Location = New System.Drawing.Point(10, 104)
    Me.lbAnno.Name = "lbAnno"
    Me.lbAnno.NTSDbField = ""
    Me.lbAnno.Size = New System.Drawing.Size(103, 13)
    Me.lbAnno.TabIndex = 3
    Me.lbAnno.Text = "Anno (0 = qualsiasi)"
    '
    'edSerie
    '
    Me.edSerie.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edSerie.Enabled = False
    Me.edSerie.Location = New System.Drawing.Point(147, 75)
    Me.edSerie.Name = "edSerie"
    Me.edSerie.NTSDbField = ""
    Me.edSerie.NTSForzaVisZoom = False
    Me.edSerie.NTSOldValue = ""
    Me.edSerie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSerie.Properties.MaxLength = 65536
    Me.edSerie.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSerie.Size = New System.Drawing.Size(34, 20)
    Me.edSerie.TabIndex = 2
    '
    'ckSerie
    '
    Me.ckSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSerie.Location = New System.Drawing.Point(10, 76)
    Me.ckSerie.Name = "ckSerie"
    Me.ckSerie.NTSCheckValue = "S"
    Me.ckSerie.NTSUnCheckValue = "N"
    Me.ckSerie.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSerie.Properties.Appearance.Options.UseBackColor = True
    Me.ckSerie.Properties.Caption = "Considera serie ordine"
    Me.ckSerie.Size = New System.Drawing.Size(132, 19)
    Me.ckSerie.TabIndex = 1
    '
    'lbTesto
    '
    Me.lbTesto.AutoSize = True
    Me.lbTesto.BackColor = System.Drawing.Color.Transparent
    Me.lbTesto.Location = New System.Drawing.Point(6, 24)
    Me.lbTesto.Name = "lbTesto"
    Me.lbTesto.NTSDbField = ""
    Me.lbTesto.Size = New System.Drawing.Size(342, 13)
    Me.lbTesto.TabIndex = 0
    Me.lbTesto.Text = "Il programma provvederà alla cancellazione di tutti gli ordini evasi con:"
    '
    'fmOrcaor
    '
    Me.fmOrcaor.AllowDrop = True
    Me.fmOrcaor.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmOrcaor.Appearance.Options.UseBackColor = True
    Me.fmOrcaor.Controls.Add(Me.ckOrdfor)
    Me.fmOrcaor.Controls.Add(Me.ckOrdfoa)
    Me.fmOrcaor.Controls.Add(Me.ckPreven)
    Me.fmOrcaor.Controls.Add(Me.ckImpcli)
    Me.fmOrcaor.Controls.Add(Me.ckImpcla)
    Me.fmOrcaor.Controls.Add(Me.ckImptra)
    Me.fmOrcaor.Controls.Add(Me.ckOrdpro)
    Me.fmOrcaor.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmOrcaor.Location = New System.Drawing.Point(13, 204)
    Me.fmOrcaor.Name = "fmOrcaor"
    Me.fmOrcaor.Size = New System.Drawing.Size(338, 123)
    Me.fmOrcaor.TabIndex = 5
    Me.fmOrcaor.Text = "Ordini da cancellare"
    '
    'ckOrdfor
    '
    Me.ckOrdfor.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckOrdfor.EditValue = True
    Me.ckOrdfor.Location = New System.Drawing.Point(5, 49)
    Me.ckOrdfor.Name = "ckOrdfor"
    Me.ckOrdfor.NTSCheckValue = "S"
    Me.ckOrdfor.NTSUnCheckValue = "N"
    Me.ckOrdfor.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckOrdfor.Properties.Appearance.Options.UseBackColor = True
    Me.ckOrdfor.Properties.Caption = "Ordine fornitore"
    Me.ckOrdfor.Size = New System.Drawing.Size(134, 19)
    Me.ckOrdfor.TabIndex = 6
    '
    'ckOrdfoa
    '
    Me.ckOrdfoa.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckOrdfoa.EditValue = True
    Me.ckOrdfoa.Location = New System.Drawing.Point(5, 74)
    Me.ckOrdfoa.Name = "ckOrdfoa"
    Me.ckOrdfoa.NTSCheckValue = "S"
    Me.ckOrdfoa.NTSUnCheckValue = "N"
    Me.ckOrdfoa.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckOrdfoa.Properties.Appearance.Options.UseBackColor = True
    Me.ckOrdfoa.Properties.Caption = "Ordine fornitore aperto"
    Me.ckOrdfoa.Size = New System.Drawing.Size(134, 19)
    Me.ckOrdfoa.TabIndex = 5
    '
    'ckPreven
    '
    Me.ckPreven.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckPreven.EditValue = True
    Me.ckPreven.Location = New System.Drawing.Point(5, 99)
    Me.ckPreven.Name = "ckPreven"
    Me.ckPreven.NTSCheckValue = "S"
    Me.ckPreven.NTSUnCheckValue = "N"
    Me.ckPreven.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPreven.Properties.Appearance.Options.UseBackColor = True
    Me.ckPreven.Properties.Caption = "Preventivo"
    Me.ckPreven.Size = New System.Drawing.Size(134, 19)
    Me.ckPreven.TabIndex = 4
    '
    'ckImpcli
    '
    Me.ckImpcli.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckImpcli.EditValue = True
    Me.ckImpcli.Location = New System.Drawing.Point(179, 24)
    Me.ckImpcli.Name = "ckImpcli"
    Me.ckImpcli.NTSCheckValue = "S"
    Me.ckImpcli.NTSUnCheckValue = "N"
    Me.ckImpcli.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckImpcli.Properties.Appearance.Options.UseBackColor = True
    Me.ckImpcli.Properties.Caption = "Impegno cliente"
    Me.ckImpcli.Size = New System.Drawing.Size(134, 19)
    Me.ckImpcli.TabIndex = 3
    '
    'ckImpcla
    '
    Me.ckImpcla.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckImpcla.EditValue = True
    Me.ckImpcla.Location = New System.Drawing.Point(179, 49)
    Me.ckImpcla.Name = "ckImpcla"
    Me.ckImpcla.NTSCheckValue = "S"
    Me.ckImpcla.NTSUnCheckValue = "N"
    Me.ckImpcla.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckImpcla.Properties.Appearance.Options.UseBackColor = True
    Me.ckImpcla.Properties.Caption = "Impegno cliente aperto"
    Me.ckImpcla.Size = New System.Drawing.Size(134, 19)
    Me.ckImpcla.TabIndex = 2
    '
    'ckImptra
    '
    Me.ckImptra.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckImptra.EditValue = True
    Me.ckImptra.Location = New System.Drawing.Point(179, 74)
    Me.ckImptra.Name = "ckImptra"
    Me.ckImptra.NTSCheckValue = "S"
    Me.ckImptra.NTSUnCheckValue = "N"
    Me.ckImptra.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckImptra.Properties.Appearance.Options.UseBackColor = True
    Me.ckImptra.Properties.Caption = "Impegno trasferimento"
    Me.ckImptra.Size = New System.Drawing.Size(134, 19)
    Me.ckImptra.TabIndex = 1
    '
    'ckOrdpro
    '
    Me.ckOrdpro.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckOrdpro.EditValue = True
    Me.ckOrdpro.Location = New System.Drawing.Point(5, 24)
    Me.ckOrdpro.Name = "ckOrdpro"
    Me.ckOrdpro.NTSCheckValue = "S"
    Me.ckOrdpro.NTSUnCheckValue = "N"
    Me.ckOrdpro.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckOrdpro.Properties.Appearance.Options.UseBackColor = True
    Me.ckOrdpro.Properties.Caption = "Ordine di produzione"
    Me.ckOrdpro.Size = New System.Drawing.Size(134, 19)
    Me.ckOrdpro.TabIndex = 0
    '
    'lbOrdine
    '
    Me.lbOrdine.BackColor = System.Drawing.Color.Transparent
    Me.lbOrdine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbOrdine.Location = New System.Drawing.Point(13, 356)
    Me.lbOrdine.Name = "lbOrdine"
    Me.lbOrdine.NTSDbField = ""
    Me.lbOrdine.Size = New System.Drawing.Size(461, 20)
    Me.lbOrdine.TabIndex = 6
    Me.lbOrdine.Text = "Nessuna Operazione in corso."
    '
    'ckFattur
    '
    Me.ckFattur.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckFattur.Location = New System.Drawing.Point(12, 333)
    Me.ckFattur.Name = "ckFattur"
    Me.ckFattur.NTSCheckValue = "S"
    Me.ckFattur.NTSUnCheckValue = "N"
    Me.ckFattur.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckFattur.Properties.Appearance.Options.UseBackColor = True
    Me.ckFattur.Properties.Caption = "Solo Impegni clienti, Ordini fornitori e Ordini di produzione riferiti a document" & _
        "i già fatturati"
    Me.ckFattur.Size = New System.Drawing.Size(461, 19)
    Me.ckFattur.TabIndex = 7
    '
    'FRMORCAOR
    '
    Me.ClientSize = New System.Drawing.Size(487, 387)
    Me.Controls.Add(Me.ckFattur)
    Me.Controls.Add(Me.lbOrdine)
    Me.Controls.Add(Me.fmOrcaor)
    Me.Controls.Add(Me.fmAttenzione)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMORCAOR"
    Me.Text = "Cancellazione ordini"
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
    CType(Me.ckOrdfoa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckPreven.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckImpcli.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckImpcla.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckImptra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckOrdpro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckFattur.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
      ckOrdfoa.NTSSetParam(oMenu, oApp.Tr(Me, 128624190584062500, "Ordine fornitore aperto"), "S", "N")
      ckPreven.NTSSetParam(oMenu, oApp.Tr(Me, 128624190584218750, "Preventivo"), "S", "N")
      ckImpcli.NTSSetParam(oMenu, oApp.Tr(Me, 128624190584375000, "Impegno cliente"), "S", "N")
      ckImpcla.NTSSetParam(oMenu, oApp.Tr(Me, 128624190584531250, "Impegno cliente aperto"), "S", "N")
      ckImptra.NTSSetParam(oMenu, oApp.Tr(Me, 128624190584687500, "Impegno trasferimento"), "S", "N")
      ckOrdpro.NTSSetParam(oMenu, oApp.Tr(Me, 128624190584843750, "Ordine di produzione"), "S", "N")


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
        ckOrdfoa.Enabled = False
        ckPreven.Enabled = False
        ckImpcla.Enabled = False
        ckImptra.Enabled = False
        ckOrdfoa.Checked = False
        ckPreven.Checked = False
        ckImpcla.Checked = False
        ckImptra.Checked = False
      Else
        ckOrdfoa.Enabled = True
        ckPreven.Enabled = True
        ckImpcla.Enabled = True
        ckImptra.Enabled = True
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

      If Not oCleCaor.ComponiTestoDaCk(ckOrdpro.Checked, ckOrdfor.Checked, ckOrdfoa.Checked, ckImpcli.Checked, _
                                       ckImpcla.Checked, ckImptra.Checked, ckPreven.Checked, strWhere) Then Return

      dlgProcedi = oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128624232824218750, _
                   "ATTENZIONE! Saranno cancellati tutti gli ordini evasi selezionati, procedere?"))

      If dlgProcedi = Windows.Forms.DialogResult.No Then
        Return
      End If

      tlbEsegui.Enabled = False
      '------ ELABORAZIONE -----------
      oCleCaor.elaboraserie(ckSerie.Checked, strSerie)

      If oCleCaor.Esegui(ckOrdpro.Checked, ckOrdfor.Checked, ckOrdfoa.Checked, ckImpcli.Checked, ckImpcla.Checked, _
                         ckImptra.Checked, ckPreven.Checked, ckFattur.Checked, NTSCInt(edAnno.Text), edDatord.Text, _
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