Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGCMMA_F

#Region "Dichiarazione variabili"
  Public oCleCmma As CLEMGCMMA
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
  Public WithEvents lbTesto As NTSInformatica.NTSLabel
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
  Public WithEvents ckFatrfd As NTSInformatica.NTSCheckBox
  Public WithEvents ckNade As NTSInformatica.NTSCheckBox
  Public WithEvents ckNadr As NTSInformatica.NTSCheckBox
  Public WithEvents ckRicfis As NTSInformatica.NTSCheckBox
  Public WithEvents ckRierif As NTSInformatica.NTSCheckBox
  Public WithEvents ckFatrif As NTSInformatica.NTSCheckBox
  Public WithEvents ckNotade As NTSInformatica.NTSCheckBox
  Public WithEvents ckNotacr As NTSInformatica.NTSCheckBox
  Public WithEvents ckFatdir As NTSInformatica.NTSCheckBox
  Public WithEvents ckBolmoi As NTSInformatica.NTSCheckBox
  Public WithEvents fmDate As NTSInformatica.NTSGroupBox
  Public WithEvents lbUltcanc As NTSInformatica.NTSLabel
  Public WithEvents lbUltagg As NTSInformatica.NTSLabel
  Public WithEvents edDatrif As NTSInformatica.NTSTextBoxData
  Public WithEvents lbDatrif As NTSInformatica.NTSLabel
  Public WithEvents lbDatcan As NTSInformatica.NTSLabel
  Public WithEvents lbDatagg As NTSInformatica.NTSLabel
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGCMMA_F))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbEsegui = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.lbTesto = New NTSInformatica.NTSLabel
    Me.fmDoccan = New NTSInformatica.NTSGroupBox
    Me.pnRight = New NTSInformatica.NTSPanel
    Me.ckNotacr = New NTSInformatica.NTSCheckBox
    Me.ckFatimr = New NTSInformatica.NTSCheckBox
    Me.ckNadr = New NTSInformatica.NTSCheckBox
    Me.ckDdtric = New NTSInformatica.NTSCheckBox
    Me.ckFatdir = New NTSInformatica.NTSCheckBox
    Me.pnMiddle = New NTSInformatica.NTSPanel
    Me.ckNade = New NTSInformatica.NTSCheckBox
    Me.ckFatrfd = New NTSInformatica.NTSCheckBox
    Me.ckFatrif = New NTSInformatica.NTSCheckBox
    Me.ckBolmoi = New NTSInformatica.NTSCheckBox
    Me.pnLeft = New NTSInformatica.NTSPanel
    Me.ckFatime = New NTSInformatica.NTSCheckBox
    Me.ckNotace = New NTSInformatica.NTSCheckBox
    Me.ckFatdie = New NTSInformatica.NTSCheckBox
    Me.ckRicfis = New NTSInformatica.NTSCheckBox
    Me.ckRierif = New NTSInformatica.NTSCheckBox
    Me.ckCoreme = New NTSInformatica.NTSCheckBox
    Me.ckDdteme = New NTSInformatica.NTSCheckBox
    Me.ckNotade = New NTSInformatica.NTSCheckBox
    Me.lbOrdine = New NTSInformatica.NTSLabel
    Me.fmDate = New NTSInformatica.NTSGroupBox
    Me.lbDatcan = New NTSInformatica.NTSLabel
    Me.lbDatagg = New NTSInformatica.NTSLabel
    Me.lbUltcanc = New NTSInformatica.NTSLabel
    Me.lbUltagg = New NTSInformatica.NTSLabel
    Me.edDatrif = New NTSInformatica.NTSTextBoxData
    Me.lbDatrif = New NTSInformatica.NTSLabel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmDoccan, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmDoccan.SuspendLayout()
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnRight.SuspendLayout()
    CType(Me.ckNotacr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckFatimr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNadr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDdtric.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckFatdir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnMiddle, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnMiddle.SuspendLayout()
    CType(Me.ckNade.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckFatrfd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckFatrif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckBolmoi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnLeft, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnLeft.SuspendLayout()
    CType(Me.ckFatime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNotace.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckFatdie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckRicfis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckRierif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckCoreme.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDdteme.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNotade.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'lbTesto
    '
    Me.lbTesto.BackColor = System.Drawing.Color.Transparent
    Me.lbTesto.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Italic)
    Me.lbTesto.Location = New System.Drawing.Point(15, 33)
    Me.lbTesto.Name = "lbTesto"
    Me.lbTesto.NTSDbField = ""
    Me.lbTesto.Size = New System.Drawing.Size(589, 51)
    Me.lbTesto.TabIndex = 0
    Me.lbTesto.Text = "ATTENZIONE!" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Si procederà alla cancellazione di:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "TUTTI I DOCUMENTI DI MAGAZZINO " & _
        "FINO ALLA DATA INDICATA COMPRESA" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
    Me.lbTesto.TextAlign = System.Drawing.ContentAlignment.TopCenter
    Me.lbTesto.Tooltip = ""
    Me.lbTesto.UseMnemonic = False
    '
    'fmDoccan
    '
    Me.fmDoccan.AllowDrop = True
    Me.fmDoccan.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmDoccan.Appearance.Options.UseBackColor = True
    Me.fmDoccan.Controls.Add(Me.pnRight)
    Me.fmDoccan.Controls.Add(Me.pnMiddle)
    Me.fmDoccan.Controls.Add(Me.pnLeft)
    Me.fmDoccan.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmDoccan.Location = New System.Drawing.Point(13, 87)
    Me.fmDoccan.Name = "fmDoccan"
    Me.fmDoccan.Size = New System.Drawing.Size(591, 204)
    Me.fmDoccan.TabIndex = 5
    Me.fmDoccan.Text = "Documenti da cancellare"
    '
    'pnRight
    '
    Me.pnRight.AllowDrop = True
    Me.pnRight.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnRight.Appearance.Options.UseBackColor = True
    Me.pnRight.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnRight.Controls.Add(Me.ckNotacr)
    Me.pnRight.Controls.Add(Me.ckFatimr)
    Me.pnRight.Controls.Add(Me.ckNadr)
    Me.pnRight.Controls.Add(Me.ckDdtric)
    Me.pnRight.Controls.Add(Me.ckFatdir)
    Me.pnRight.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnRight.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnRight.Location = New System.Drawing.Point(377, 20)
    Me.pnRight.Name = "pnRight"
    Me.pnRight.NTSActiveTrasparency = True
    Me.pnRight.Size = New System.Drawing.Size(212, 182)
    Me.pnRight.TabIndex = 95
    Me.pnRight.Text = "NtsPanel1"
    '
    'ckNotacr
    '
    Me.ckNotacr.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckNotacr.EditValue = True
    Me.ckNotacr.Location = New System.Drawing.Point(8, 90)
    Me.ckNotacr.Name = "ckNotacr"
    Me.ckNotacr.NTSCheckValue = "S"
    Me.ckNotacr.NTSUnCheckValue = "N"
    Me.ckNotacr.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNotacr.Properties.Appearance.Options.UseBackColor = True
    Me.ckNotacr.Properties.Caption = "Note di Accredito Ricevute"
    Me.ckNotacr.Size = New System.Drawing.Size(154, 19)
    Me.ckNotacr.TabIndex = 11
    '
    'ckFatimr
    '
    Me.ckFatimr.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckFatimr.EditValue = True
    Me.ckFatimr.Location = New System.Drawing.Point(8, 2)
    Me.ckFatimr.Name = "ckFatimr"
    Me.ckFatimr.NTSCheckValue = "S"
    Me.ckFatimr.NTSUnCheckValue = "N"
    Me.ckFatimr.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckFatimr.Properties.Appearance.Options.UseBackColor = True
    Me.ckFatimr.Properties.Caption = "Fatture Immediate Ricevute"
    Me.ckFatimr.Size = New System.Drawing.Size(165, 19)
    Me.ckFatimr.TabIndex = 3
    '
    'ckNadr
    '
    Me.ckNadr.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckNadr.EditValue = True
    Me.ckNadr.Location = New System.Drawing.Point(8, 68)
    Me.ckNadr.Name = "ckNadr"
    Me.ckNadr.NTSCheckValue = "S"
    Me.ckNadr.NTSUnCheckValue = "N"
    Me.ckNadr.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNadr.Properties.Appearance.Options.UseBackColor = True
    Me.ckNadr.Properties.Caption = "Note di Accredito Differite Ricevute"
    Me.ckNadr.Size = New System.Drawing.Size(199, 19)
    Me.ckNadr.TabIndex = 16
    '
    'ckDdtric
    '
    Me.ckDdtric.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDdtric.EditValue = True
    Me.ckDdtric.Location = New System.Drawing.Point(8, 24)
    Me.ckDdtric.Name = "ckDdtric"
    Me.ckDdtric.NTSCheckValue = "S"
    Me.ckDdtric.NTSUnCheckValue = "N"
    Me.ckDdtric.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDdtric.Properties.Appearance.Options.UseBackColor = True
    Me.ckDdtric.Properties.Caption = "D.D.T. Ricevuti"
    Me.ckDdtric.Size = New System.Drawing.Size(108, 19)
    Me.ckDdtric.TabIndex = 2
    '
    'ckFatdir
    '
    Me.ckFatdir.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckFatdir.EditValue = True
    Me.ckFatdir.Location = New System.Drawing.Point(8, 46)
    Me.ckFatdir.Name = "ckFatdir"
    Me.ckFatdir.NTSCheckValue = "S"
    Me.ckFatdir.NTSUnCheckValue = "N"
    Me.ckFatdir.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckFatdir.Properties.Appearance.Options.UseBackColor = True
    Me.ckFatdir.Properties.Caption = "Fatture Differite Ricevute"
    Me.ckFatdir.Size = New System.Drawing.Size(154, 19)
    Me.ckFatdir.TabIndex = 10
    '
    'pnMiddle
    '
    Me.pnMiddle.AllowDrop = True
    Me.pnMiddle.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnMiddle.Appearance.Options.UseBackColor = True
    Me.pnMiddle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnMiddle.Controls.Add(Me.ckNade)
    Me.pnMiddle.Controls.Add(Me.ckFatrfd)
    Me.pnMiddle.Controls.Add(Me.ckFatrif)
    Me.pnMiddle.Controls.Add(Me.ckBolmoi)
    Me.pnMiddle.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnMiddle.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnMiddle.Location = New System.Drawing.Point(175, 20)
    Me.pnMiddle.Name = "pnMiddle"
    Me.pnMiddle.NTSActiveTrasparency = True
    Me.pnMiddle.Size = New System.Drawing.Size(414, 182)
    Me.pnMiddle.TabIndex = 95
    Me.pnMiddle.Text = "NtsPanel1"
    '
    'ckNade
    '
    Me.ckNade.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckNade.EditValue = True
    Me.ckNade.Location = New System.Drawing.Point(8, 46)
    Me.ckNade.Name = "ckNade"
    Me.ckNade.NTSCheckValue = "S"
    Me.ckNade.NTSUnCheckValue = "N"
    Me.ckNade.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNade.Properties.Appearance.Options.UseBackColor = True
    Me.ckNade.Properties.Caption = "Note di Accredito Dfferite Emesse"
    Me.ckNade.Size = New System.Drawing.Size(196, 19)
    Me.ckNade.TabIndex = 16
    '
    'ckFatrfd
    '
    Me.ckFatrfd.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckFatrfd.EditValue = True
    Me.ckFatrfd.Location = New System.Drawing.Point(8, 24)
    Me.ckFatrfd.Name = "ckFatrfd"
    Me.ckFatrfd.NTSCheckValue = "S"
    Me.ckFatrfd.NTSUnCheckValue = "N"
    Me.ckFatrfd.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckFatrfd.Properties.Appearance.Options.UseBackColor = True
    Me.ckFatrfd.Properties.Caption = "Fatture/Ricevute Fiscali Differite"
    Me.ckFatrfd.Size = New System.Drawing.Size(180, 19)
    Me.ckFatrfd.TabIndex = 16
    '
    'ckFatrif
    '
    Me.ckFatrif.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckFatrif.EditValue = True
    Me.ckFatrif.Location = New System.Drawing.Point(8, 2)
    Me.ckFatrif.Name = "ckFatrif"
    Me.ckFatrif.NTSCheckValue = "S"
    Me.ckFatrif.NTSUnCheckValue = "N"
    Me.ckFatrif.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckFatrif.Properties.Appearance.Options.UseBackColor = True
    Me.ckFatrif.Properties.Caption = "Fatture/Ricevute Fiscali"
    Me.ckFatrif.Size = New System.Drawing.Size(146, 19)
    Me.ckFatrif.TabIndex = 13
    '
    'ckBolmoi
    '
    Me.ckBolmoi.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckBolmoi.EditValue = True
    Me.ckBolmoi.Location = New System.Drawing.Point(8, 68)
    Me.ckBolmoi.Name = "ckBolmoi"
    Me.ckBolmoi.NTSCheckValue = "S"
    Me.ckBolmoi.NTSUnCheckValue = "N"
    Me.ckBolmoi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckBolmoi.Properties.Appearance.Options.UseBackColor = True
    Me.ckBolmoi.Properties.Caption = "Bolle di Movimentazione Interna"
    Me.ckBolmoi.Size = New System.Drawing.Size(178, 19)
    Me.ckBolmoi.TabIndex = 7
    '
    'pnLeft
    '
    Me.pnLeft.AllowDrop = True
    Me.pnLeft.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnLeft.Appearance.Options.UseBackColor = True
    Me.pnLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnLeft.Controls.Add(Me.ckFatime)
    Me.pnLeft.Controls.Add(Me.ckNotace)
    Me.pnLeft.Controls.Add(Me.ckFatdie)
    Me.pnLeft.Controls.Add(Me.ckRicfis)
    Me.pnLeft.Controls.Add(Me.ckRierif)
    Me.pnLeft.Controls.Add(Me.ckCoreme)
    Me.pnLeft.Controls.Add(Me.ckDdteme)
    Me.pnLeft.Controls.Add(Me.ckNotade)
    Me.pnLeft.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnLeft.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnLeft.Location = New System.Drawing.Point(2, 20)
    Me.pnLeft.Name = "pnLeft"
    Me.pnLeft.NTSActiveTrasparency = True
    Me.pnLeft.Size = New System.Drawing.Size(173, 182)
    Me.pnLeft.TabIndex = 94
    Me.pnLeft.Text = "NtsPanel1"
    '
    'ckFatime
    '
    Me.ckFatime.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.ckFatime.EditValue = True
    Me.ckFatime.Location = New System.Drawing.Point(8, 2)
    Me.ckFatime.Name = "ckFatime"
    Me.ckFatime.NTSCheckValue = "S"
    Me.ckFatime.NTSUnCheckValue = "N"
    Me.ckFatime.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckFatime.Properties.Appearance.Options.UseBackColor = True
    Me.ckFatime.Properties.Caption = "Fatture Immediate Emesse"
    Me.ckFatime.Size = New System.Drawing.Size(160, 19)
    Me.ckFatime.TabIndex = 0
    '
    'ckNotace
    '
    Me.ckNotace.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckNotace.EditValue = True
    Me.ckNotace.Location = New System.Drawing.Point(8, 112)
    Me.ckNotace.Name = "ckNotace"
    Me.ckNotace.NTSCheckValue = "S"
    Me.ckNotace.NTSUnCheckValue = "N"
    Me.ckNotace.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNotace.Properties.Appearance.Options.UseBackColor = True
    Me.ckNotace.Properties.Caption = "Note di Accredito Emesse"
    Me.ckNotace.Size = New System.Drawing.Size(150, 19)
    Me.ckNotace.TabIndex = 1
    '
    'ckFatdie
    '
    Me.ckFatdie.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckFatdie.EditValue = True
    Me.ckFatdie.Location = New System.Drawing.Point(8, 68)
    Me.ckFatdie.Name = "ckFatdie"
    Me.ckFatdie.NTSCheckValue = "S"
    Me.ckFatdie.NTSUnCheckValue = "N"
    Me.ckFatdie.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckFatdie.Properties.Appearance.Options.UseBackColor = True
    Me.ckFatdie.Properties.Caption = "Fatture Differite Emesse"
    Me.ckFatdie.Size = New System.Drawing.Size(154, 19)
    Me.ckFatdie.TabIndex = 4
    '
    'ckRicfis
    '
    Me.ckRicfis.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckRicfis.EditValue = True
    Me.ckRicfis.Location = New System.Drawing.Point(8, 134)
    Me.ckRicfis.Name = "ckRicfis"
    Me.ckRicfis.NTSCheckValue = "S"
    Me.ckRicfis.NTSUnCheckValue = "N"
    Me.ckRicfis.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckRicfis.Properties.Appearance.Options.UseBackColor = True
    Me.ckRicfis.Properties.Caption = "Ricevute Fiscali"
    Me.ckRicfis.Size = New System.Drawing.Size(102, 19)
    Me.ckRicfis.TabIndex = 15
    '
    'ckRierif
    '
    Me.ckRierif.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckRierif.EditValue = True
    Me.ckRierif.Location = New System.Drawing.Point(8, 156)
    Me.ckRierif.Name = "ckRierif"
    Me.ckRierif.NTSCheckValue = "S"
    Me.ckRierif.NTSUnCheckValue = "N"
    Me.ckRierif.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckRierif.Properties.Appearance.Options.UseBackColor = True
    Me.ckRierif.Properties.Caption = "Riemissione Ricevute Fiscali"
    Me.ckRierif.Size = New System.Drawing.Size(160, 19)
    Me.ckRierif.TabIndex = 14
    '
    'ckCoreme
    '
    Me.ckCoreme.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckCoreme.EditValue = True
    Me.ckCoreme.Location = New System.Drawing.Point(8, 46)
    Me.ckCoreme.Name = "ckCoreme"
    Me.ckCoreme.NTSCheckValue = "S"
    Me.ckCoreme.NTSUnCheckValue = "N"
    Me.ckCoreme.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckCoreme.Properties.Appearance.Options.UseBackColor = True
    Me.ckCoreme.Properties.Caption = "Corrispettivi Emessi"
    Me.ckCoreme.Size = New System.Drawing.Size(121, 19)
    Me.ckCoreme.TabIndex = 5
    '
    'ckDdteme
    '
    Me.ckDdteme.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.ckDdteme.EditValue = True
    Me.ckDdteme.Location = New System.Drawing.Point(8, 24)
    Me.ckDdteme.Name = "ckDdteme"
    Me.ckDdteme.NTSCheckValue = "S"
    Me.ckDdteme.NTSUnCheckValue = "N"
    Me.ckDdteme.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDdteme.Properties.Appearance.Options.UseBackColor = True
    Me.ckDdteme.Properties.Caption = "D.D.T. Emessi"
    Me.ckDdteme.Size = New System.Drawing.Size(102, 19)
    Me.ckDdteme.TabIndex = 6
    '
    'ckNotade
    '
    Me.ckNotade.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckNotade.EditValue = True
    Me.ckNotade.Location = New System.Drawing.Point(8, 90)
    Me.ckNotade.Name = "ckNotade"
    Me.ckNotade.NTSCheckValue = "S"
    Me.ckNotade.NTSUnCheckValue = "N"
    Me.ckNotade.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNotade.Properties.Appearance.Options.UseBackColor = True
    Me.ckNotade.Properties.Caption = "Note di Addebito Emesse"
    Me.ckNotade.Size = New System.Drawing.Size(154, 19)
    Me.ckNotade.TabIndex = 12
    '
    'lbOrdine
    '
    Me.lbOrdine.AutoSize = True
    Me.lbOrdine.BackColor = System.Drawing.Color.Transparent
    Me.lbOrdine.Location = New System.Drawing.Point(12, 372)
    Me.lbOrdine.Name = "lbOrdine"
    Me.lbOrdine.NTSDbField = ""
    Me.lbOrdine.Size = New System.Drawing.Size(150, 13)
    Me.lbOrdine.TabIndex = 6
    Me.lbOrdine.Text = "Nessuna Operazione in corso."
    Me.lbOrdine.Tooltip = ""
    Me.lbOrdine.UseMnemonic = False
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
    Me.fmDate.Size = New System.Drawing.Size(592, 72)
    Me.fmDate.TabIndex = 8
    '
    'lbDatcan
    '
    Me.lbDatcan.BackColor = System.Drawing.Color.Transparent
    Me.lbDatcan.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDatcan.Location = New System.Drawing.Point(390, 45)
    Me.lbDatcan.Name = "lbDatcan"
    Me.lbDatcan.NTSDbField = ""
    Me.lbDatcan.Size = New System.Drawing.Size(80, 20)
    Me.lbDatcan.TabIndex = 21
    Me.lbDatcan.Tooltip = ""
    Me.lbDatcan.UseMnemonic = False
    '
    'lbDatagg
    '
    Me.lbDatagg.BackColor = System.Drawing.Color.Transparent
    Me.lbDatagg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDatagg.Location = New System.Drawing.Point(390, 23)
    Me.lbDatagg.Name = "lbDatagg"
    Me.lbDatagg.NTSDbField = ""
    Me.lbDatagg.Size = New System.Drawing.Size(80, 20)
    Me.lbDatagg.TabIndex = 20
    Me.lbDatagg.Tooltip = ""
    Me.lbDatagg.UseMnemonic = False
    '
    'lbUltcanc
    '
    Me.lbUltcanc.AutoSize = True
    Me.lbUltcanc.BackColor = System.Drawing.Color.Transparent
    Me.lbUltcanc.Location = New System.Drawing.Point(249, 46)
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
    Me.lbUltagg.Location = New System.Drawing.Point(248, 26)
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
    Me.edDatrif.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatrif.Properties.MaxLength = 65536
    Me.edDatrif.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatrif.Size = New System.Drawing.Size(80, 20)
    Me.edDatrif.TabIndex = 10
    '
    'lbDatrif
    '
    Me.lbDatrif.AutoSize = True
    Me.lbDatrif.BackColor = System.Drawing.Color.Transparent
    Me.lbDatrif.Location = New System.Drawing.Point(8, 26)
    Me.lbDatrif.Name = "lbDatrif"
    Me.lbDatrif.NTSDbField = ""
    Me.lbDatrif.Size = New System.Drawing.Size(96, 13)
    Me.lbDatrif.TabIndex = 6
    Me.lbDatrif.Text = "Data di riferimento"
    Me.lbDatrif.Tooltip = ""
    Me.lbDatrif.UseMnemonic = False
    '
    'FRMMGCMMA_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(619, 399)
    Me.Controls.Add(Me.lbTesto)
    Me.Controls.Add(Me.fmDate)
    Me.Controls.Add(Me.lbOrdine)
    Me.Controls.Add(Me.fmDoccan)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMMGCMMA_F"
    Me.Text = "CANCELLAZIONE MOVIMENTI DI MAGAZZINO"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmDoccan, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmDoccan.ResumeLayout(False)
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnRight.ResumeLayout(False)
    Me.pnRight.PerformLayout()
    CType(Me.ckNotacr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckFatimr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNadr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDdtric.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckFatdir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnMiddle, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnMiddle.ResumeLayout(False)
    Me.pnMiddle.PerformLayout()
    CType(Me.ckNade.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckFatrfd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckFatrif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckBolmoi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnLeft, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnLeft.ResumeLayout(False)
    Me.pnLeft.PerformLayout()
    CType(Me.ckFatime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNotace.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckFatdie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckRicfis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckRierif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckCoreme.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDdteme.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNotade.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
        tlbEsegui.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try

      edDatrif.NTSSetParam(oMenu, oApp.Tr(Me, 128625871870625000, "Data riferimento"), False)
      ckFatrfd.NTSSetParam(oMenu, oApp.Tr(Me, 128625871872187500, "Fatt./ricev. fisc. differ."), "S", "N")
      ckRicfis.NTSSetParam(oMenu, oApp.Tr(Me, 128625871872343750, "Ricevute fiscali"), "S", "N")
      ckRierif.NTSSetParam(oMenu, oApp.Tr(Me, 128625871872500000, "Riem. ricev. fiscali"), "S", "N")
      ckFatrif.NTSSetParam(oMenu, oApp.Tr(Me, 128625871872656250, "Fatt./ricev. fiscali"), "S", "N")
      ckNotade.NTSSetParam(oMenu, oApp.Tr(Me, 128625871872812500, "Note di addebito emesse"), "S", "N")
      ckNotacr.NTSSetParam(oMenu, oApp.Tr(Me, 128625871872968750, "Note Accr. ricevute"), "S", "N")
      ckFatdir.NTSSetParam(oMenu, oApp.Tr(Me, 128625871873125000, "Fatture Diff. ricevute"), "S", "N")
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

      NTSScriptExec("InitControls", Me, Nothing)

    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
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
          Me.Refresh()
      End Select
    End If
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRMMGCMMA_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      InitControls()

      oCleCmma.LeggiDate(lbDatagg.Text, lbDatcan.Text)

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

  Public Overridable Sub Esegui()
    Dim dlgProcedi As DialogResult
    Dim strWhere As String = ""
    Dim bAggiornaAnaz As Boolean = False

    Try
      Me.ValidaLastControl()

      If Not oCleCmma.ComponiTestoDaCk(ckFatime.Checked, ckDdteme.Checked, ckCoreme.Checked, ckFatdie.Checked, _
                                       ckNotade.Checked, ckNotacr.Checked, ckFatdir.Checked, ckFatimr.Checked, _
                                       ckDdtric.Checked, ckNotace.Checked, False, False, _
                                       ckBolmoi.Checked, ckRicfis.Checked, ckRierif.Checked, ckFatrif.Checked, _
                                       ckFatrfd.Checked, strWhere, bAggiornaAnaz, ckNade.Checked, ckNadr.Checked) Then Return

      If NTSCDate(edDatrif.Text) > NTSCDate(lbDatagg.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128625936550468750, "La data fino alla quale devono essere cancellati i movimenti" & _
                                            vbCrLf & "deve essere minore o uguale della data di ultimo aggiornamento."))
        Return
      End If

      dlgProcedi = oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128625969448906250, _
                   "ATTENZIONE! Saranno cancellati i movimenti di magazzino selezionati, procedere?"))

      If dlgProcedi = Windows.Forms.DialogResult.No Then
        Return
      End If

      tlbEsegui.Enabled = False
      '------ ELABORAZIONE -----------
      If oCleCmma.EseguiDoc(strWhere, edDatrif.Text, bAggiornaAnaz) Then oApp.MsgBoxInfo(oApp.Tr(Me, 129022393161785531, "Elaborazione Completata"))
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

End Class