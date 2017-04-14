Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORCHIU
  Public oCleChiu As CLEORCHIU
  Public oCallParams As CLE__CLDP
  Public strCodagedaOld As String = "0"
  Public strDescrdaOld As String = ""
  Public strCodageaOld As String = "0"
  Public strDescraOld As String = ""

#Region "Moduli"
  Private Moduli_P As Integer = bsModOR
  Private ModuliExt_P As Integer = bsModExtORE
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

#Region "Dichiarazione Controlli"
  Private components As System.ComponentModel.IContainer
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
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
  Public WithEvents lbDadord As NTSInformatica.NTSLabel
  Public WithEvents lbDadcte As NTSInformatica.NTSLabel
  Public WithEvents lbDacomm As NTSInformatica.NTSLabel
  Public WithEvents lbAnno As NTSInformatica.NTSLabel
  Public WithEvents edcommecaFin As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAdatcons As NTSInformatica.NTSTextBoxData
  Public WithEvents edAdatord As NTSInformatica.NTSTextBoxData
  Public WithEvents edAnumord As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAnord As NTSInformatica.NTSLabel
  Public WithEvents lbAdord As NTSInformatica.NTSLabel
  Public WithEvents lbAdcte As NTSInformatica.NTSLabel
  Public WithEvents lbAcomme As NTSInformatica.NTSLabel
  Public WithEvents edCommecaIni As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDadatcons As NTSInformatica.NTSTextBoxData
  Public WithEvents edDadatord As NTSInformatica.NTSTextBoxData
  Public WithEvents edDanumord As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAnno As NTSInformatica.NTSTextBoxNum
  Public WithEvents fmOrchiu As NTSInformatica.NTSGroupBox
  Public WithEvents opOrdfor As NTSInformatica.NTSRadioButton
  Public WithEvents opOrdfoa As NTSInformatica.NTSRadioButton
  Public WithEvents opNotpre As NTSInformatica.NTSRadioButton
  Public WithEvents opImptra As NTSInformatica.NTSRadioButton
  Public WithEvents opImpcla As NTSInformatica.NTSRadioButton
  Public WithEvents opImpcli As NTSInformatica.NTSRadioButton
  Public WithEvents opOrdpro As NTSInformatica.NTSRadioButton
  Public WithEvents lbOrdine As NTSInformatica.NTSLabel
  Public WithEvents fmEvasioneOrdini As NTSInformatica.NTSGroupBox
  Public WithEvents optDaEvadere As NTSInformatica.NTSRadioButton
  Public WithEvents optParzEvasi As NTSInformatica.NTSRadioButton
  Public WithEvents edCodagen As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCodagen As NTSInformatica.NTSLabel
  Public WithEvents lbDescodagen As NTSInformatica.NTSLabel
#End Region

#Region "Inizializzazione"
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNORCHIU", "BEORCHIU", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128589409211093750, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleChiu = CType(oTmp, CLEORCHIU)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNORCHIU", strRemoteServer, strRemotePort)
    AddHandler oCleChiu.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleChiu.Init(oApp, NTSScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMORCHIU))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbEsegui = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.fmAttenzione = New NTSInformatica.NTSGroupBox
    Me.lbDescodagen = New NTSInformatica.NTSLabel
    Me.edCodagen = New NTSInformatica.NTSTextBoxNum
    Me.lbCodagen = New NTSInformatica.NTSLabel
    Me.fmEvasioneOrdini = New NTSInformatica.NTSGroupBox
    Me.optDaEvadere = New NTSInformatica.NTSRadioButton
    Me.optParzEvasi = New NTSInformatica.NTSRadioButton
    Me.edcommecaFin = New NTSInformatica.NTSTextBoxNum
    Me.edAdatcons = New NTSInformatica.NTSTextBoxData
    Me.edAdatord = New NTSInformatica.NTSTextBoxData
    Me.edAnumord = New NTSInformatica.NTSTextBoxNum
    Me.lbAnord = New NTSInformatica.NTSLabel
    Me.lbAdord = New NTSInformatica.NTSLabel
    Me.lbAdcte = New NTSInformatica.NTSLabel
    Me.lbAcomme = New NTSInformatica.NTSLabel
    Me.edCommecaIni = New NTSInformatica.NTSTextBoxNum
    Me.edDadatcons = New NTSInformatica.NTSTextBoxData
    Me.edDadatord = New NTSInformatica.NTSTextBoxData
    Me.edDanumord = New NTSInformatica.NTSTextBoxNum
    Me.edAnno = New NTSInformatica.NTSTextBoxNum
    Me.lbDanord = New NTSInformatica.NTSLabel
    Me.lbDadord = New NTSInformatica.NTSLabel
    Me.lbDadcte = New NTSInformatica.NTSLabel
    Me.lbDacomm = New NTSInformatica.NTSLabel
    Me.lbAnno = New NTSInformatica.NTSLabel
    Me.edSerie = New NTSInformatica.NTSTextBoxStr
    Me.ckSerie = New NTSInformatica.NTSCheckBox
    Me.lbTesto = New NTSInformatica.NTSLabel
    Me.fmOrchiu = New NTSInformatica.NTSGroupBox
    Me.opOrdfor = New NTSInformatica.NTSRadioButton
    Me.opOrdfoa = New NTSInformatica.NTSRadioButton
    Me.opImpcla = New NTSInformatica.NTSRadioButton
    Me.opImptra = New NTSInformatica.NTSRadioButton
    Me.opNotpre = New NTSInformatica.NTSRadioButton
    Me.opImpcli = New NTSInformatica.NTSRadioButton
    Me.opOrdpro = New NTSInformatica.NTSRadioButton
    Me.lbOrdine = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmAttenzione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmAttenzione.SuspendLayout()
    CType(Me.edCodagen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmEvasioneOrdini, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmEvasioneOrdini.SuspendLayout()
    CType(Me.optDaEvadere.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optParzEvasi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edcommecaFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdatcons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdatord.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnumord.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCommecaIni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDadatcons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDadatord.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDanumord.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmOrchiu, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmOrchiu.SuspendLayout()
    CType(Me.opOrdfor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opOrdfoa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opImpcla.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opImptra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opNotpre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opImpcli.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opOrdpro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbZoom, Me.tlbGuida, Me.tlbEsci, Me.tlbEsegui})
    Me.NtsBarManager1.MaxItemId = 14
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsegui), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbEsegui.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbEsegui.Name = "tlbEsegui"
    Me.tlbEsegui.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 4
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
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
    Me.fmAttenzione.Controls.Add(Me.lbDescodagen)
    Me.fmAttenzione.Controls.Add(Me.edCodagen)
    Me.fmAttenzione.Controls.Add(Me.lbCodagen)
    Me.fmAttenzione.Controls.Add(Me.fmEvasioneOrdini)
    Me.fmAttenzione.Controls.Add(Me.edcommecaFin)
    Me.fmAttenzione.Controls.Add(Me.edAdatcons)
    Me.fmAttenzione.Controls.Add(Me.edAdatord)
    Me.fmAttenzione.Controls.Add(Me.edAnumord)
    Me.fmAttenzione.Controls.Add(Me.lbAnord)
    Me.fmAttenzione.Controls.Add(Me.lbAdord)
    Me.fmAttenzione.Controls.Add(Me.lbAdcte)
    Me.fmAttenzione.Controls.Add(Me.lbAcomme)
    Me.fmAttenzione.Controls.Add(Me.edCommecaIni)
    Me.fmAttenzione.Controls.Add(Me.edDadatcons)
    Me.fmAttenzione.Controls.Add(Me.edDadatord)
    Me.fmAttenzione.Controls.Add(Me.edDanumord)
    Me.fmAttenzione.Controls.Add(Me.edAnno)
    Me.fmAttenzione.Controls.Add(Me.lbDanord)
    Me.fmAttenzione.Controls.Add(Me.lbDadord)
    Me.fmAttenzione.Controls.Add(Me.lbDadcte)
    Me.fmAttenzione.Controls.Add(Me.lbDacomm)
    Me.fmAttenzione.Controls.Add(Me.lbAnno)
    Me.fmAttenzione.Controls.Add(Me.edSerie)
    Me.fmAttenzione.Controls.Add(Me.ckSerie)
    Me.fmAttenzione.Controls.Add(Me.lbTesto)
    Me.fmAttenzione.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmAttenzione.Location = New System.Drawing.Point(13, 34)
    Me.fmAttenzione.Name = "fmAttenzione"
    Me.fmAttenzione.Size = New System.Drawing.Size(602, 250)
    Me.fmAttenzione.TabIndex = 4
    Me.fmAttenzione.Text = "Attenzione!"
    '
    'lbDescodagen
    '
    Me.lbDescodagen.BackColor = System.Drawing.Color.Transparent
    Me.lbDescodagen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescodagen.Location = New System.Drawing.Point(242, 174)
    Me.lbDescodagen.Name = "lbDescodagen"
    Me.lbDescodagen.NTSDbField = ""
    Me.lbDescodagen.Size = New System.Drawing.Size(345, 20)
    Me.lbDescodagen.TabIndex = 33
    Me.lbDescodagen.Tooltip = ""
    Me.lbDescodagen.UseMnemonic = False
    '
    'edCodagen
    '
    Me.edCodagen.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodagen.EditValue = "0"
    Me.edCodagen.Location = New System.Drawing.Point(186, 174)
    Me.edCodagen.Name = "edCodagen"
    Me.edCodagen.NTSDbField = ""
    Me.edCodagen.NTSFormat = "0"
    Me.edCodagen.NTSForzaVisZoom = False
    Me.edCodagen.NTSOldValue = ""
    Me.edCodagen.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodagen.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodagen.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodagen.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodagen.Properties.AutoHeight = False
    Me.edCodagen.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodagen.Properties.MaxLength = 65536
    Me.edCodagen.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodagen.Size = New System.Drawing.Size(51, 20)
    Me.edCodagen.TabIndex = 22
    Me.edCodagen.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCodagen.TextInt = 0
    '
    'lbCodagen
    '
    Me.lbCodagen.AutoSize = True
    Me.lbCodagen.BackColor = System.Drawing.Color.Transparent
    Me.lbCodagen.Location = New System.Drawing.Point(9, 177)
    Me.lbCodagen.Name = "lbCodagen"
    Me.lbCodagen.NTSDbField = ""
    Me.lbCodagen.Size = New System.Drawing.Size(76, 13)
    Me.lbCodagen.TabIndex = 21
    Me.lbCodagen.Text = "Codice agente"
    Me.lbCodagen.Tooltip = ""
    Me.lbCodagen.UseMnemonic = False
    '
    'fmEvasioneOrdini
    '
    Me.fmEvasioneOrdini.AllowDrop = True
    Me.fmEvasioneOrdini.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmEvasioneOrdini.Appearance.Options.UseBackColor = True
    Me.fmEvasioneOrdini.Controls.Add(Me.optDaEvadere)
    Me.fmEvasioneOrdini.Controls.Add(Me.optParzEvasi)
    Me.fmEvasioneOrdini.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmEvasioneOrdini.Location = New System.Drawing.Point(9, 200)
    Me.fmEvasioneOrdini.Name = "fmEvasioneOrdini"
    Me.fmEvasioneOrdini.Size = New System.Drawing.Size(327, 43)
    Me.fmEvasioneOrdini.TabIndex = 7
    Me.fmEvasioneOrdini.Text = "Evasione ordini:"
    '
    'optDaEvadere
    '
    Me.optDaEvadere.Cursor = System.Windows.Forms.Cursors.Default
    Me.optDaEvadere.Location = New System.Drawing.Point(9, 20)
    Me.optDaEvadere.Name = "optDaEvadere"
    Me.optDaEvadere.NTSCheckValue = "S"
    Me.optDaEvadere.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optDaEvadere.Properties.Appearance.Options.UseBackColor = True
    Me.optDaEvadere.Properties.AutoHeight = False
    Me.optDaEvadere.Properties.Caption = "Da evadere"
    Me.optDaEvadere.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optDaEvadere.Size = New System.Drawing.Size(83, 19)
    Me.optDaEvadere.TabIndex = 2
    '
    'optParzEvasi
    '
    Me.optParzEvasi.Cursor = System.Windows.Forms.Cursors.Default
    Me.optParzEvasi.EditValue = True
    Me.optParzEvasi.Location = New System.Drawing.Point(160, 20)
    Me.optParzEvasi.Name = "optParzEvasi"
    Me.optParzEvasi.NTSCheckValue = "S"
    Me.optParzEvasi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optParzEvasi.Properties.Appearance.Options.UseBackColor = True
    Me.optParzEvasi.Properties.AutoHeight = False
    Me.optParzEvasi.Properties.Caption = "Parzialmente evasi"
    Me.optParzEvasi.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optParzEvasi.Size = New System.Drawing.Size(117, 19)
    Me.optParzEvasi.TabIndex = 1
    '
    'edcommecaFin
    '
    Me.edcommecaFin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edcommecaFin.EditValue = "999999999"
    Me.edcommecaFin.Location = New System.Drawing.Point(487, 152)
    Me.edcommecaFin.Name = "edcommecaFin"
    Me.edcommecaFin.NTSDbField = ""
    Me.edcommecaFin.NTSFormat = "0"
    Me.edcommecaFin.NTSForzaVisZoom = False
    Me.edcommecaFin.NTSOldValue = "999999999"
    Me.edcommecaFin.Properties.Appearance.Options.UseTextOptions = True
    Me.edcommecaFin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edcommecaFin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edcommecaFin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edcommecaFin.Properties.AutoHeight = False
    Me.edcommecaFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edcommecaFin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edcommecaFin.Size = New System.Drawing.Size(100, 20)
    Me.edcommecaFin.TabIndex = 20
    Me.edcommecaFin.TextDec = New Decimal(New Integer() {999999999, 0, 0, 0})
    Me.edcommecaFin.TextInt = 999999999
    '
    'edAdatcons
    '
    Me.edAdatcons.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdatcons.EditValue = "31/12/2099"
    Me.edAdatcons.Location = New System.Drawing.Point(487, 130)
    Me.edAdatcons.Name = "edAdatcons"
    Me.edAdatcons.NTSDbField = ""
    Me.edAdatcons.NTSForzaVisZoom = False
    Me.edAdatcons.NTSOldValue = ""
    Me.edAdatcons.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdatcons.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdatcons.Properties.AutoHeight = False
    Me.edAdatcons.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAdatcons.Properties.MaxLength = 65536
    Me.edAdatcons.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdatcons.Size = New System.Drawing.Size(100, 20)
    Me.edAdatcons.TabIndex = 19
    Me.edAdatcons.TextData = New Date(2099, 12, 31, 0, 0, 0, 0)
    '
    'edAdatord
    '
    Me.edAdatord.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdatord.EditValue = "31/12/2099"
    Me.edAdatord.Location = New System.Drawing.Point(487, 108)
    Me.edAdatord.Name = "edAdatord"
    Me.edAdatord.NTSDbField = ""
    Me.edAdatord.NTSForzaVisZoom = False
    Me.edAdatord.NTSOldValue = ""
    Me.edAdatord.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdatord.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdatord.Properties.AutoHeight = False
    Me.edAdatord.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAdatord.Properties.MaxLength = 65536
    Me.edAdatord.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdatord.Size = New System.Drawing.Size(100, 20)
    Me.edAdatord.TabIndex = 18
    Me.edAdatord.TextData = New Date(2099, 12, 31, 0, 0, 0, 0)
    '
    'edAnumord
    '
    Me.edAnumord.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnumord.EditValue = "999999999"
    Me.edAnumord.Location = New System.Drawing.Point(487, 86)
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
    Me.edAnumord.TextDec = New Decimal(New Integer() {999999999, 0, 0, 0})
    Me.edAnumord.TextInt = 999999999
    '
    'lbAnord
    '
    Me.lbAnord.AutoSize = True
    Me.lbAnord.BackColor = System.Drawing.Color.Transparent
    Me.lbAnord.Location = New System.Drawing.Point(310, 89)
    Me.lbAnord.Name = "lbAnord"
    Me.lbAnord.NTSDbField = ""
    Me.lbAnord.Size = New System.Drawing.Size(85, 13)
    Me.lbAnord.TabIndex = 16
    Me.lbAnord.Text = "a numero ordine"
    Me.lbAnord.Tooltip = ""
    Me.lbAnord.UseMnemonic = False
    '
    'lbAdord
    '
    Me.lbAdord.AutoSize = True
    Me.lbAdord.BackColor = System.Drawing.Color.Transparent
    Me.lbAdord.Location = New System.Drawing.Point(310, 111)
    Me.lbAdord.Name = "lbAdord"
    Me.lbAdord.NTSDbField = ""
    Me.lbAdord.Size = New System.Drawing.Size(71, 13)
    Me.lbAdord.TabIndex = 15
    Me.lbAdord.Text = "a data ordine"
    Me.lbAdord.Tooltip = ""
    Me.lbAdord.UseMnemonic = False
    '
    'lbAdcte
    '
    Me.lbAdcte.AutoSize = True
    Me.lbAdcte.BackColor = System.Drawing.Color.Transparent
    Me.lbAdcte.Location = New System.Drawing.Point(310, 133)
    Me.lbAdcte.Name = "lbAdcte"
    Me.lbAdcte.NTSDbField = ""
    Me.lbAdcte.Size = New System.Drawing.Size(136, 13)
    Me.lbAdcte.TabIndex = 14
    Me.lbAdcte.Text = "a data consegna di testata"
    Me.lbAdcte.Tooltip = ""
    Me.lbAdcte.UseMnemonic = False
    '
    'lbAcomme
    '
    Me.lbAcomme.AutoSize = True
    Me.lbAcomme.BackColor = System.Drawing.Color.Transparent
    Me.lbAcomme.Location = New System.Drawing.Point(310, 155)
    Me.lbAcomme.Name = "lbAcomme"
    Me.lbAcomme.NTSDbField = ""
    Me.lbAcomme.Size = New System.Drawing.Size(164, 13)
    Me.lbAcomme.TabIndex = 13
    Me.lbAcomme.Text = "a numero di commessa di testata"
    Me.lbAcomme.Tooltip = ""
    Me.lbAcomme.UseMnemonic = False
    '
    'edCommecaIni
    '
    Me.edCommecaIni.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCommecaIni.EditValue = "0"
    Me.edCommecaIni.Location = New System.Drawing.Point(186, 152)
    Me.edCommecaIni.Name = "edCommecaIni"
    Me.edCommecaIni.NTSDbField = ""
    Me.edCommecaIni.NTSFormat = "0"
    Me.edCommecaIni.NTSForzaVisZoom = False
    Me.edCommecaIni.NTSOldValue = ""
    Me.edCommecaIni.Properties.Appearance.Options.UseTextOptions = True
    Me.edCommecaIni.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCommecaIni.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCommecaIni.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCommecaIni.Properties.AutoHeight = False
    Me.edCommecaIni.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCommecaIni.Properties.MaxLength = 65536
    Me.edCommecaIni.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCommecaIni.Size = New System.Drawing.Size(100, 20)
    Me.edCommecaIni.TabIndex = 12
    Me.edCommecaIni.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCommecaIni.TextInt = 0
    '
    'edDadatcons
    '
    Me.edDadatcons.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDadatcons.EditValue = "01/01/1900"
    Me.edDadatcons.Location = New System.Drawing.Point(186, 130)
    Me.edDadatcons.Name = "edDadatcons"
    Me.edDadatcons.NTSDbField = ""
    Me.edDadatcons.NTSForzaVisZoom = False
    Me.edDadatcons.NTSOldValue = ""
    Me.edDadatcons.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDadatcons.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDadatcons.Properties.AutoHeight = False
    Me.edDadatcons.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDadatcons.Properties.MaxLength = 65536
    Me.edDadatcons.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDadatcons.Size = New System.Drawing.Size(100, 20)
    Me.edDadatcons.TabIndex = 11
    Me.edDadatcons.TextData = New Date(1900, 1, 1, 0, 0, 0, 0)
    '
    'edDadatord
    '
    Me.edDadatord.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDadatord.EditValue = "01/01/1900"
    Me.edDadatord.Location = New System.Drawing.Point(186, 108)
    Me.edDadatord.Name = "edDadatord"
    Me.edDadatord.NTSDbField = ""
    Me.edDadatord.NTSForzaVisZoom = False
    Me.edDadatord.NTSOldValue = ""
    Me.edDadatord.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDadatord.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDadatord.Properties.AutoHeight = False
    Me.edDadatord.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDadatord.Properties.MaxLength = 65536
    Me.edDadatord.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDadatord.Size = New System.Drawing.Size(100, 20)
    Me.edDadatord.TabIndex = 10
    Me.edDadatord.TextData = New Date(1900, 1, 1, 0, 0, 0, 0)
    '
    'edDanumord
    '
    Me.edDanumord.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDanumord.EditValue = "0"
    Me.edDanumord.Location = New System.Drawing.Point(186, 86)
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
    Me.edDanumord.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edDanumord.TextInt = 0
    '
    'edAnno
    '
    Me.edAnno.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnno.EditValue = "1900"
    Me.edAnno.Location = New System.Drawing.Point(186, 64)
    Me.edAnno.Name = "edAnno"
    Me.edAnno.NTSDbField = ""
    Me.edAnno.NTSFormat = "0"
    Me.edAnno.NTSForzaVisZoom = False
    Me.edAnno.NTSOldValue = "1900"
    Me.edAnno.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnno.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnno.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnno.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnno.Properties.AutoHeight = False
    Me.edAnno.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnno.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnno.Size = New System.Drawing.Size(51, 20)
    Me.edAnno.TabIndex = 8
    Me.edAnno.TextDec = New Decimal(New Integer() {1900, 0, 0, 0})
    Me.edAnno.TextInt = 1900
    '
    'lbDanord
    '
    Me.lbDanord.AutoSize = True
    Me.lbDanord.BackColor = System.Drawing.Color.Transparent
    Me.lbDanord.Location = New System.Drawing.Point(9, 89)
    Me.lbDanord.Name = "lbDanord"
    Me.lbDanord.NTSDbField = ""
    Me.lbDanord.Size = New System.Drawing.Size(92, 13)
    Me.lbDanord.TabIndex = 7
    Me.lbDanord.Text = "Da numero ordine"
    Me.lbDanord.Tooltip = ""
    Me.lbDanord.UseMnemonic = False
    '
    'lbDadord
    '
    Me.lbDadord.AutoSize = True
    Me.lbDadord.BackColor = System.Drawing.Color.Transparent
    Me.lbDadord.Location = New System.Drawing.Point(9, 111)
    Me.lbDadord.Name = "lbDadord"
    Me.lbDadord.NTSDbField = ""
    Me.lbDadord.Size = New System.Drawing.Size(78, 13)
    Me.lbDadord.TabIndex = 6
    Me.lbDadord.Text = "Da data ordine"
    Me.lbDadord.Tooltip = ""
    Me.lbDadord.UseMnemonic = False
    '
    'lbDadcte
    '
    Me.lbDadcte.AutoSize = True
    Me.lbDadcte.BackColor = System.Drawing.Color.Transparent
    Me.lbDadcte.Location = New System.Drawing.Point(9, 133)
    Me.lbDadcte.Name = "lbDadcte"
    Me.lbDadcte.NTSDbField = ""
    Me.lbDadcte.Size = New System.Drawing.Size(143, 13)
    Me.lbDadcte.TabIndex = 5
    Me.lbDadcte.Text = "Da data consegna di testata"
    Me.lbDadcte.Tooltip = ""
    Me.lbDadcte.UseMnemonic = False
    '
    'lbDacomm
    '
    Me.lbDacomm.AutoSize = True
    Me.lbDacomm.BackColor = System.Drawing.Color.Transparent
    Me.lbDacomm.Location = New System.Drawing.Point(9, 155)
    Me.lbDacomm.Name = "lbDacomm"
    Me.lbDacomm.NTSDbField = ""
    Me.lbDacomm.Size = New System.Drawing.Size(171, 13)
    Me.lbDacomm.TabIndex = 4
    Me.lbDacomm.Text = "Da numero di commessa di testata"
    Me.lbDacomm.Tooltip = ""
    Me.lbDacomm.UseMnemonic = False
    '
    'lbAnno
    '
    Me.lbAnno.AutoSize = True
    Me.lbAnno.BackColor = System.Drawing.Color.Transparent
    Me.lbAnno.Location = New System.Drawing.Point(9, 67)
    Me.lbAnno.Name = "lbAnno"
    Me.lbAnno.NTSDbField = ""
    Me.lbAnno.Size = New System.Drawing.Size(32, 13)
    Me.lbAnno.TabIndex = 3
    Me.lbAnno.Text = "Anno"
    Me.lbAnno.Tooltip = ""
    Me.lbAnno.UseMnemonic = False
    '
    'edSerie
    '
    Me.edSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSerie.EditValue = ""
    Me.edSerie.Enabled = False
    Me.edSerie.Location = New System.Drawing.Point(186, 42)
    Me.edSerie.Name = "edSerie"
    Me.edSerie.NTSDbField = ""
    Me.edSerie.NTSForzaVisZoom = False
    Me.edSerie.NTSOldValue = ""
    Me.edSerie.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSerie.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSerie.Properties.AutoHeight = False
    Me.edSerie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSerie.Properties.MaxLength = 65536
    Me.edSerie.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSerie.Size = New System.Drawing.Size(51, 20)
    Me.edSerie.TabIndex = 2
    Me.edSerie.TextStr = ""
    '
    'ckSerie
    '
    Me.ckSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSerie.Location = New System.Drawing.Point(12, 43)
    Me.ckSerie.Name = "ckSerie"
    Me.ckSerie.NTSCheckValue = "S"
    Me.ckSerie.NTSUnCheckValue = "N"
    Me.ckSerie.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSerie.Properties.Appearance.Options.UseBackColor = True
    Me.ckSerie.Properties.AutoHeight = False
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
    Me.lbTesto.Size = New System.Drawing.Size(322, 13)
    Me.lbTesto.TabIndex = 0
    Me.lbTesto.Text = "Il programma provvederà alla chiusura di tutti gli ordini aperti con:"
    Me.lbTesto.Tooltip = ""
    Me.lbTesto.UseMnemonic = False
    '
    'fmOrchiu
    '
    Me.fmOrchiu.AllowDrop = True
    Me.fmOrchiu.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmOrchiu.Appearance.Options.UseBackColor = True
    Me.fmOrchiu.Controls.Add(Me.opOrdfor)
    Me.fmOrchiu.Controls.Add(Me.opOrdfoa)
    Me.fmOrchiu.Controls.Add(Me.opImpcla)
    Me.fmOrchiu.Controls.Add(Me.opImptra)
    Me.fmOrchiu.Controls.Add(Me.opNotpre)
    Me.fmOrchiu.Controls.Add(Me.opImpcli)
    Me.fmOrchiu.Controls.Add(Me.opOrdpro)
    Me.fmOrchiu.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmOrchiu.Location = New System.Drawing.Point(13, 288)
    Me.fmOrchiu.Name = "fmOrchiu"
    Me.fmOrchiu.Size = New System.Drawing.Size(336, 100)
    Me.fmOrchiu.TabIndex = 5
    Me.fmOrchiu.Text = "Ordini da chiudere:"
    '
    'opOrdfor
    '
    Me.opOrdfor.Cursor = System.Windows.Forms.Cursors.Default
    Me.opOrdfor.Location = New System.Drawing.Point(18, 40)
    Me.opOrdfor.Name = "opOrdfor"
    Me.opOrdfor.NTSCheckValue = "S"
    Me.opOrdfor.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opOrdfor.Properties.Appearance.Options.UseBackColor = True
    Me.opOrdfor.Properties.AutoHeight = False
    Me.opOrdfor.Properties.Caption = "Ordini Fornitore"
    Me.opOrdfor.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opOrdfor.Size = New System.Drawing.Size(136, 19)
    Me.opOrdfor.TabIndex = 6
    '
    'opOrdfoa
    '
    Me.opOrdfoa.Cursor = System.Windows.Forms.Cursors.Default
    Me.opOrdfoa.Location = New System.Drawing.Point(18, 60)
    Me.opOrdfoa.Name = "opOrdfoa"
    Me.opOrdfoa.NTSCheckValue = "S"
    Me.opOrdfoa.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opOrdfoa.Properties.Appearance.Options.UseBackColor = True
    Me.opOrdfoa.Properties.AutoHeight = False
    Me.opOrdfoa.Properties.Caption = "Ordini Fornitore Aperti"
    Me.opOrdfoa.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opOrdfoa.Size = New System.Drawing.Size(136, 19)
    Me.opOrdfoa.TabIndex = 5
    '
    'opImpcla
    '
    Me.opImpcla.Cursor = System.Windows.Forms.Cursors.Default
    Me.opImpcla.Location = New System.Drawing.Point(169, 40)
    Me.opImpcla.Name = "opImpcla"
    Me.opImpcla.NTSCheckValue = "S"
    Me.opImpcla.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opImpcla.Properties.Appearance.Options.UseBackColor = True
    Me.opImpcla.Properties.AutoHeight = False
    Me.opImpcla.Properties.Caption = "Impegni Cliente Aperti"
    Me.opImpcla.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opImpcla.Size = New System.Drawing.Size(134, 19)
    Me.opImpcla.TabIndex = 2
    '
    'opImptra
    '
    Me.opImptra.Cursor = System.Windows.Forms.Cursors.Default
    Me.opImptra.Location = New System.Drawing.Point(169, 60)
    Me.opImptra.Name = "opImptra"
    Me.opImptra.NTSCheckValue = "S"
    Me.opImptra.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opImptra.Properties.Appearance.Options.UseBackColor = True
    Me.opImptra.Properties.AutoHeight = False
    Me.opImptra.Properties.Caption = "Impegni Trasferimento"
    Me.opImptra.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opImptra.Size = New System.Drawing.Size(134, 19)
    Me.opImptra.TabIndex = 3
    '
    'opNotpre
    '
    Me.opNotpre.Cursor = System.Windows.Forms.Cursors.Default
    Me.opNotpre.Location = New System.Drawing.Point(18, 80)
    Me.opNotpre.Name = "opNotpre"
    Me.opNotpre.NTSCheckValue = "S"
    Me.opNotpre.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opNotpre.Properties.Appearance.Options.UseBackColor = True
    Me.opNotpre.Properties.AutoHeight = False
    Me.opNotpre.Properties.Caption = "Note di Prelievo"
    Me.opNotpre.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opNotpre.Size = New System.Drawing.Size(136, 19)
    Me.opNotpre.TabIndex = 4
    '
    'opImpcli
    '
    Me.opImpcli.Cursor = System.Windows.Forms.Cursors.Default
    Me.opImpcli.Location = New System.Drawing.Point(169, 20)
    Me.opImpcli.Name = "opImpcli"
    Me.opImpcli.NTSCheckValue = "S"
    Me.opImpcli.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opImpcli.Properties.Appearance.Options.UseBackColor = True
    Me.opImpcli.Properties.AutoHeight = False
    Me.opImpcli.Properties.Caption = "Impegni Cliente"
    Me.opImpcli.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opImpcli.Size = New System.Drawing.Size(134, 19)
    Me.opImpcli.TabIndex = 1
    '
    'opOrdpro
    '
    Me.opOrdpro.Cursor = System.Windows.Forms.Cursors.Default
    Me.opOrdpro.EditValue = True
    Me.opOrdpro.Location = New System.Drawing.Point(18, 20)
    Me.opOrdpro.Name = "opOrdpro"
    Me.opOrdpro.NTSCheckValue = "S"
    Me.opOrdpro.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opOrdpro.Properties.Appearance.Options.UseBackColor = True
    Me.opOrdpro.Properties.AutoHeight = False
    Me.opOrdpro.Properties.Caption = "Ordini di Produzione"
    Me.opOrdpro.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opOrdpro.Size = New System.Drawing.Size(136, 19)
    Me.opOrdpro.TabIndex = 0
    '
    'lbOrdine
    '
    Me.lbOrdine.AutoSize = True
    Me.lbOrdine.BackColor = System.Drawing.Color.Transparent
    Me.lbOrdine.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbOrdine.Location = New System.Drawing.Point(12, 401)
    Me.lbOrdine.Name = "lbOrdine"
    Me.lbOrdine.NTSDbField = ""
    Me.lbOrdine.Size = New System.Drawing.Size(170, 13)
    Me.lbOrdine.TabIndex = 6
    Me.lbOrdine.Text = "Nessuna Operazione in atto..."
    Me.lbOrdine.Tooltip = ""
    Me.lbOrdine.UseMnemonic = False
    '
    'FRMORCHIU
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(628, 423)
    Me.Controls.Add(Me.lbOrdine)
    Me.Controls.Add(Me.fmOrchiu)
    Me.Controls.Add(Me.fmAttenzione)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMORCHIU"
    Me.Text = "CHIUSURA ORDINI E IMPEGNI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmAttenzione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmAttenzione.ResumeLayout(False)
    Me.fmAttenzione.PerformLayout()
    CType(Me.edCodagen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmEvasioneOrdini, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmEvasioneOrdini.ResumeLayout(False)
    Me.fmEvasioneOrdini.PerformLayout()
    CType(Me.optDaEvadere.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optParzEvasi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edcommecaFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdatcons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdatord.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnumord.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCommecaIni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDadatcons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDadatord.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDanumord.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmOrchiu, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmOrchiu.ResumeLayout(False)
    Me.fmOrchiu.PerformLayout()
    CType(Me.opOrdfor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opOrdfoa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opImpcla.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opImptra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opNotpre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opImpcli.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opOrdpro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      Try
        tlbEsegui.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try

      optDaEvadere.NTSSetParam(oMenu, oApp.Tr(Me, 130507736076548839, "Da evedere"), "N")
      optParzEvasi.NTSSetParam(oMenu, oApp.Tr(Me, 130507736194878142, "Parzialmente evasi"), "N")

      opOrdpro.NTSSetParam(oMenu, oApp.Tr(Me, 128871289454649000, "Ordini di Produzione"), "P")
      opOrdfor.NTSSetParam(oMenu, oApp.Tr(Me, 128613635679843750, "Ordini Fornitore"), "F")
      opOrdfoa.NTSSetParam(oMenu, oApp.Tr(Me, 128613635680000000, "Ordini Fornitore Aperti"), "G")
      opImpcli.NTSSetParam(oMenu, oApp.Tr(Me, 128613635680625000, "Impegni Clienti"), "C")
      opImpcla.NTSSetParam(oMenu, oApp.Tr(Me, 128613635680156250, "Impegni Clienti Aperti"), "D")
      opImptra.NTSSetParam(oMenu, oApp.Tr(Me, 128613635680312500, "Impegni di Trasferimento"), "T")
      opNotpre.NTSSetParam(oMenu, oApp.Tr(Me, 128613635680468750, "Note di Prelievo"), "N")

      edcommecaFin.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128613635680781250, "a numero commessa"), tabcommess)
      edAdatcons.NTSSetParam(oMenu, oApp.Tr(Me, 128613635680937500, "a data consegna"), False)
      edAdatord.NTSSetParam(oMenu, oApp.Tr(Me, 128613635681093750, "a data ordine"), False)
      edAnumord.NTSSetParam(oMenu, oApp.Tr(Me, 128613635681250000, "a numero ordine"), "0", 8, 0, 999999999)
      edCommecaIni.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128613635682031250, "da numero commessa"), tabcommess)
      edDadatcons.NTSSetParam(oMenu, oApp.Tr(Me, 128613635682187500, "da data consegna"), False)
      edDadatord.NTSSetParam(oMenu, oApp.Tr(Me, 128613635682343750, "da data ordine"), False)
      edDanumord.NTSSetParam(oMenu, oApp.Tr(Me, 128613635682500000, "da numero ordine"), "0", 8, 0, 999999999)
      edAnno.NTSSetParam(oMenu, oApp.Tr(Me, 128613635682656250, "anno"), "0", 4, 1900, 2099)
      edSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128613635683593750, "serie ordine"), CLN__STD.SerieMaxLen)
      ckSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128613635683750000, "Considera serie ordine"), "S", "N")
      edCodagen.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130507743438464637, "Codice agente"), tabcage)

      NTSScriptExec("InitControls", Me, Nothing)

    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRMORCHIU_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbEsegui_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsegui.ItemClick
    Try
      Esegui()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      '------------------------------------
      'zoom standard di textbox e griglia
      NTSCallStandardZoom()

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

#Region "Eventi TextBox"
  Public Overridable Sub edDanumord_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDanumord.Validated
    Try
      If Not oCleChiu.ControllaMaggioreNum(NTSCInt(edDanumord.Text), NTSCInt(edAnumord.Text), oApp.Tr(Me, 128613779083281250, "ordine")) Then
        edDanumord.Text = "0"
        Return
      End If
      If NTSCInt(edDanumord.Text) <> 0 Then edAnumord.Text = edDanumord.Text
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAnumord_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAnumord.Validated
    Try
      If oCleChiu Is Nothing Then Return
      If Not oCleChiu.ControllaMaggioreNum(NTSCInt(edDanumord.Text), NTSCInt(edAnumord.Text), oApp.Tr(Me, 128613779131718750, "ordine")) Then
        edAnumord.Text = "".PadLeft(9, "9"c)
      End If
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edDadatord_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDadatord.Validated
    Try
      If Not oCleChiu.ControllaMaggioreData(NTSCDate(edDadatord.Text), NTSCDate(edAdatord.Text), oApp.Tr(Me, 128613779181250000, "ordine")) Then
        edDadatord.Text = "01/01/1900"
      End If
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAdatord_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAdatord.Validated
    Try
      If Not oCleChiu.ControllaMaggioreData(NTSCDate(edDadatord.Text), NTSCDate(edAdatord.Text), oApp.Tr(Me, 128613779251875000, "ordine")) Then
        edAdatord.Text = "31/12/2099"
      End If
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edDadatcons_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDadatcons.Validated
    Try
      If Not oCleChiu.ControllaMaggioreData(NTSCDate(edDadatcons.Text), NTSCDate(edAdatcons.Text), oApp.Tr(Me, 128613779299062500, "commessa di testata")) Then
        edDadatcons.Text = "01/01/1900"
      End If
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAdatcons_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAdatcons.Validated
    Try
      If Not oCleChiu.ControllaMaggioreData(NTSCDate(edDadatcons.Text), NTSCDate(edAdatcons.Text), oApp.Tr(Me, 128613779354062500, "commessa di testata")) Then
        edAdatcons.Text = "31/12/2099"
      End If
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edCommecaIni_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCommecaIni.Validated
    Try
      If Not oCleChiu.ControllaMaggioreNum(NTSCInt(edCommecaIni.Text), NTSCInt(edcommecaFin.Text), oApp.Tr(Me, 128613779407500000, "commessa di testata")) Then
        edCommecaIni.Text = "0"
      End If
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edcommecaFin_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edcommecaFin.Validated
    Try
      If oCleChiu Is Nothing Then Return
      If Not oCleChiu.ControllaMaggioreNum(NTSCInt(edCommecaIni.Text), NTSCInt(edcommecaFin.Text), oApp.Tr(Me, 128613779454375000, "commessa di testata")) Then
        edcommecaFin.Text = "999999999"
      End If
    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
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

  Public Overridable Sub edCodagen_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodagen.Validated
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edCodagen.Text) = 0 Then
        lbDescodagen.Text = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oMenu.ValCodiceDb(edCodagen.Text, DittaCorrente, "TABCAGE", "N", strTmp) = False Then
        edCodagen.Text = "0"
        lbDescodagen.Text = ""
        oApp.MsgBoxInfo(oApp.Tr(Me, 130492041177076275, "Attenzione!" & vbCrLf & _
          "Codice agente inesistente."))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      lbDescodagen.Text = strTmp
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi CheckBox"
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
#End Region

#Region "Eventi optionButton"
  Public Overridable Sub opNotpre_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opNotpre.CheckedChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If opNotpre.Checked = True Then
        edDadatcons.Text = IntSetDate("01/01/1900")
        edAdatcons.Text = IntSetDate("31/12/2099")
        lbDadcte.Enabled = False
        lbAdcte.Enabled = False
        edDadatcons.Enabled = False
        edAdatcons.Enabled = False
      Else
        GctlSetVisEnab(edDadatcons, False)
        GctlSetVisEnab(edAdatcons, False)
        GctlSetVisEnab(lbDadcte, False)
        GctlSetVisEnab(lbAdcte, False)
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Sub Esegui()
    Dim strMsg As String = ""
    Dim strWhere As String = ""
    Dim strOpt As String = ""
    Dim frmGrid As FRMORGRID = Nothing
    Dim oPar As CLE__CLDP

    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla se i range min/max sono validi
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleChiu.ControllaMaggioreData(NTSCDate(edDadatord.Text), NTSCDate(edAdatord.Text), oApp.Tr(Me, 128613779835937500, "ordine")) Then Return
      If Not oCleChiu.ControllaMaggioreData(NTSCDate(edDadatcons.Text), NTSCDate(edAdatcons.Text), oApp.Tr(Me, 128613779812500000, "commessa di testata")) Then Return
      If Not oCleChiu.ControllaMaggioreNum(NTSCInt(edDanumord.Text), NTSCInt(edAnumord.Text), oApp.Tr(Me, 128613779788593750, "ordine")) Then Return
      If Not oCleChiu.ControllaMaggioreNum(NTSCInt(edCommecaIni.Text), NTSCInt(edcommecaFin.Text), oApp.Tr(Me, 128613779769531250, "consegna di testata")) Then Return
      '--------------------------------------------------------------------------------------------------------------
      '--- Compone il testo della where e della msgbox in base agli opt
      '--------------------------------------------------------------------------------------------------------------
      oCleChiu.ComponiTestoDaOpt(opOrdpro.Checked, opOrdfor.Checked, opOrdfoa.Checked, opImpcli.Checked, _
                                 opImpcla.Checked, opImptra.Checked, strMsg, strWhere, strOpt)
      '--------------------------------------------------------------------------------------------------------------
      '--- Mette lo spazio se il testo della serie è abilitato e vuoto
      '--------------------------------------------------------------------------------------------------------------
      oCleChiu.ElaboraSerie(ckSerie.Checked, edSerie.Text)
      '--------------------------------------------------------------------------------------------------------------
      oCleChiu.nCodagen = NTSCInt(edCodagen.Text)
      oCleChiu.bParzEvasi = optParzEvasi.Checked
      '--------------------------------------------------------------------------------------------------------------
      If oCleChiu.Esegui(edSerie.Text, NTSCInt(edAnno.Text), _
        NTSCInt(edDanumord.Text), NTSCInt(edAnumord.Text), _
        edDadatord.Text, edAdatord.Text, edDadatcons.Text, edAdatcons.Text, _
        NTSCInt(edCommecaIni.Text), NTSCInt(edcommecaFin.Text), strOpt, strWhere) = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      frmGrid = CType(NTSNewFormModal("FRMORGRID"), FRMORGRID)
      oPar = New CLE__CLDP
      frmGrid.Init(oMenu, oPar, DittaCorrente)
      frmGrid.InitEntity(oCleChiu)
      If opOrdpro.Checked = True Then frmGrid.lbDescr.Text = "Ordini di Produzione"
      If opOrdfor.Checked = True Then frmGrid.lbDescr.Text = "Ordini Fornitore"
      If opOrdfoa.Checked = True Then frmGrid.lbDescr.Text = "Ordini Fornitore Aperti"
      If opNotpre.Checked = True Then frmGrid.lbDescr.Text = "Note di Prelievo"
      If opImpcli.Checked = True Then frmGrid.lbDescr.Text = "Impegni Cliente"
      If opImpcla.Checked = True Then frmGrid.lbDescr.Text = "Impegni Cliente Aperti"
      If opImptra.Checked = True Then frmGrid.lbDescr.Text = "Impegni di Trasferimento"
      frmGrid.ShowDialog()
      '--------------------------------------------------------------------------------------------------------------
      If oCleChiu.dttTest.Rows.Count = 0 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130492217459886217, "Operazione annullata."))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCleChiu.LogStart("BNORCHIU", "Chiusura ordini e impegni") = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      Cursor = Cursors.WaitCursor
      '--------------------------------------------------------------------------------------------------------------
      oCleChiu.ChiudiOrdiniSelezionati(strOpt)
      '--------------------------------------------------------------------------------------------------------------
      Cursor = Cursors.Default
      oApp.MsgBoxInfo(oApp.Tr(Me, 128613779737812500, "Elaborazione Completata."))
      '--------------------------------------------------------------------------------------------------------------
      oCleChiu.LogStop()
      If oCleChiu.LogError = True Then
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128656228049375000, "Esistono dei messaggi nel file di log della Cancellazione Ordini e Impegni. Visualizzare il file?")) = Windows.Forms.DialogResult.Yes Then
          System.Diagnostics.Process.Start("notepad", oCleChiu.LogFileName)
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      lbOrdine.Text = oApp.Tr(Me, 128614693118437500, "Nessuna operazione in atto ...")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      oCleChiu.LogStop()
      Me.Cursor = Cursors.Default
      '--------------------------------------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------------------------------------------------------------------------
    Finally
      If Not frmGrid Is Nothing Then frmGrid.Dispose()
      frmGrid = Nothing
    End Try
  End Sub

  Public Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    Dim i As Integer = 0
    Dim strT() As String = Nothing
    Dim strTmp As String = ""

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
  End Sub

End Class