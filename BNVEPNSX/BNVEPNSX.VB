Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVEPNSX

#Region "Moduli"
  Private Moduli_P As Integer = bsModVE
  Private ModuliExt_P As Integer = 0
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
  Public oClePnsx As CLEVEPNSX
  Public oCallParams As CLE__CLDP

  Public bModuliAcquistati As Boolean = False

  Public nEscomp As Integer = 0
  Public nEscompp As Integer = 0
  Public nCodesco As Integer = 0

  Public strMsg As String = ""
  Public strTmp As String = ""
  Public strDesesco As String = ""
  Public strDtfieser As String = ""

  Public dttTmp As New DataTable

  Private components As System.ComponentModel.IContainer

  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbElabora As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbIntegr As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents edEscomp As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbEscomp As NTSInformatica.NTSLabel
  Public WithEvents lbXx_escomp As NTSInformatica.NTSLabel
  Public WithEvents lbDatreg As NTSInformatica.NTSLabel
  Public WithEvents edDatreg As NTSInformatica.NTSTextBoxData
  Public WithEvents fmDocumentiDacontabilizzare As NTSInformatica.NTSGroupBox
  Public WithEvents fmRegistrazioniDiStorno As NTSInformatica.NTSGroupBox
  Public WithEvents lbDatregst As NTSInformatica.NTSLabel
  Public WithEvents edDatregst As NTSInformatica.NTSTextBoxData
  Public WithEvents lbXx_escompst As NTSInformatica.NTSLabel
  Public WithEvents edEscompSt As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbEscompSt As NTSInformatica.NTSLabel
  Public WithEvents ckStorno As NTSInformatica.NTSCheckBox
  Public WithEvents fmSelezioneTipoDocumento As NTSInformatica.NTSGroupBox
  Public WithEvents ckTiporkB As NTSInformatica.NTSCheckBox
  Public WithEvents ckTiporkM As NTSInformatica.NTSCheckBox
  Public WithEvents ckTipoRkBResiforn As NTSInformatica.NTSCheckBox
  Public WithEvents ckTipoRkMResicli As NTSInformatica.NTSCheckBox
  Public WithEvents edAnno As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAnno As NTSInformatica.NTSLabel
  Public WithEvents lbDatini As NTSInformatica.NTSLabel
  Public WithEvents edDatini As NTSInformatica.NTSTextBoxData
  Public WithEvents lbDatfin As NTSInformatica.NTSLabel
  Public WithEvents edDatfin As NTSInformatica.NTSTextBoxData
  Public WithEvents lbSerieini As NTSInformatica.NTSLabel
  Public WithEvents edSerieini As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbBarraini As NTSInformatica.NTSLabel
  Public WithEvents edNumeroini As NTSInformatica.NTSTextBoxNum
  Public WithEvents edNumerofin As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbBarrafin As NTSInformatica.NTSLabel
  Public WithEvents lbSeriefin As NTSInformatica.NTSLabel
  Public WithEvents edSeriefin As NTSInformatica.NTSTextBoxStr
  Public WithEvents fmOpzioni As NTSInformatica.NTSGroupBox
  Public WithEvents ckAnalitica As NTSInformatica.NTSCheckBox
  Public WithEvents ckRielab As NTSInformatica.NTSCheckBox
  Public WithEvents lbXx_codtpbf As NTSInformatica.NTSLabel
  Public WithEvents edCodtpbf As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCodtpbf As NTSInformatica.NTSLabel
  Public WithEvents lbStatus As NTSInformatica.NTSLabel
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNVEPNSX", "BEVEPNSX", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128776126468568214, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oClePnsx = CType(oTmp, CLEVEPNSX)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNVEPNSX", strRemoteServer, strRemotePort)
    AddHandler oClePnsx.RemoteEvent, AddressOf GestisciEventiEntity
    If oClePnsx.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMVEPNSX))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbElabora = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbIntegr = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.edEscomp = New NTSInformatica.NTSTextBoxNum
    Me.lbEscomp = New NTSInformatica.NTSLabel
    Me.lbXx_escomp = New NTSInformatica.NTSLabel
    Me.edDatreg = New NTSInformatica.NTSTextBoxData
    Me.lbDatreg = New NTSInformatica.NTSLabel
    Me.fmDocumentiDacontabilizzare = New NTSInformatica.NTSGroupBox
    Me.lbXx_codtpbf = New NTSInformatica.NTSLabel
    Me.edCodtpbf = New NTSInformatica.NTSTextBoxNum
    Me.lbCodtpbf = New NTSInformatica.NTSLabel
    Me.fmOpzioni = New NTSInformatica.NTSGroupBox
    Me.ckAnalitica = New NTSInformatica.NTSCheckBox
    Me.ckRielab = New NTSInformatica.NTSCheckBox
    Me.edNumerofin = New NTSInformatica.NTSTextBoxNum
    Me.lbBarrafin = New NTSInformatica.NTSLabel
    Me.lbSeriefin = New NTSInformatica.NTSLabel
    Me.edSeriefin = New NTSInformatica.NTSTextBoxStr
    Me.edNumeroini = New NTSInformatica.NTSTextBoxNum
    Me.lbBarraini = New NTSInformatica.NTSLabel
    Me.lbSerieini = New NTSInformatica.NTSLabel
    Me.edSerieini = New NTSInformatica.NTSTextBoxStr
    Me.lbDatfin = New NTSInformatica.NTSLabel
    Me.edDatfin = New NTSInformatica.NTSTextBoxData
    Me.lbDatini = New NTSInformatica.NTSLabel
    Me.edDatini = New NTSInformatica.NTSTextBoxData
    Me.edAnno = New NTSInformatica.NTSTextBoxNum
    Me.lbAnno = New NTSInformatica.NTSLabel
    Me.fmSelezioneTipoDocumento = New NTSInformatica.NTSGroupBox
    Me.ckTipoRkMResicli = New NTSInformatica.NTSCheckBox
    Me.ckTipoRkBResiforn = New NTSInformatica.NTSCheckBox
    Me.ckTiporkM = New NTSInformatica.NTSCheckBox
    Me.ckTiporkB = New NTSInformatica.NTSCheckBox
    Me.ckStorno = New NTSInformatica.NTSCheckBox
    Me.lbEscompSt = New NTSInformatica.NTSLabel
    Me.edEscompSt = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_escompst = New NTSInformatica.NTSLabel
    Me.edDatregst = New NTSInformatica.NTSTextBoxData
    Me.lbDatregst = New NTSInformatica.NTSLabel
    Me.fmRegistrazioniDiStorno = New NTSInformatica.NTSGroupBox
    Me.lbStatus = New NTSInformatica.NTSLabel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEscomp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatreg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmDocumentiDacontabilizzare, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmDocumentiDacontabilizzare.SuspendLayout()
    CType(Me.edCodtpbf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmOpzioni, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmOpzioni.SuspendLayout()
    CType(Me.ckAnalitica.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckRielab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumerofin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSeriefin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumeroini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerieini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatfin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmSelezioneTipoDocumento, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmSelezioneTipoDocumento.SuspendLayout()
    CType(Me.ckTipoRkMResicli.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTipoRkBResiforn.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTiporkM.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTiporkB.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckStorno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEscompSt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatregst.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmRegistrazioniDiStorno, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmRegistrazioniDiStorno.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbElabora, Me.tlbZoom, Me.tlbIntegr, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 5
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbElabora), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbIntegr), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbElabora
    '
    Me.tlbElabora.Caption = "tlbElabora"
    Me.tlbElabora.Glyph = CType(resources.GetObject("tlbElabora.Glyph"), System.Drawing.Image)
    Me.tlbElabora.Id = 0
    Me.tlbElabora.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbElabora.Name = "tlbElabora"
    Me.tlbElabora.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "tlbZoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 1
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbIntegr
    '
    Me.tlbIntegr.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbIntegr.Caption = "tlbIntegr"
    Me.tlbIntegr.Glyph = CType(resources.GetObject("tlbIntegr.Glyph"), System.Drawing.Image)
    Me.tlbIntegr.Id = 2
    Me.tlbIntegr.Name = "tlbIntegr"
    Me.tlbIntegr.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "tlbGuida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 3
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "tlbEsci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 4
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'edEscomp
    '
    Me.edEscomp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEscomp.EditValue = ""
    Me.edEscomp.Location = New System.Drawing.Point(201, 36)
    Me.edEscomp.Name = "edEscomp"
    Me.edEscomp.NTSDbField = ""
    Me.edEscomp.NTSFormat = "0"
    Me.edEscomp.NTSForzaVisZoom = False
    Me.edEscomp.NTSOldValue = ""
    Me.edEscomp.Properties.Appearance.Options.UseTextOptions = True
    Me.edEscomp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEscomp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEscomp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEscomp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEscomp.Properties.MaxLength = 65536
    Me.edEscomp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEscomp.Size = New System.Drawing.Size(59, 20)
    Me.edEscomp.TabIndex = 9
    '
    'lbEscomp
    '
    Me.lbEscomp.AutoSize = True
    Me.lbEscomp.BackColor = System.Drawing.Color.Transparent
    Me.lbEscomp.Location = New System.Drawing.Point(16, 39)
    Me.lbEscomp.Name = "lbEscomp"
    Me.lbEscomp.NTSDbField = ""
    Me.lbEscomp.Size = New System.Drawing.Size(120, 13)
    Me.lbEscomp.TabIndex = 8
    Me.lbEscomp.Text = "Esercizio di competenza"
    Me.lbEscomp.Tooltip = ""
    Me.lbEscomp.UseMnemonic = False
    '
    'lbXx_escomp
    '
    Me.lbXx_escomp.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_escomp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_escomp.Location = New System.Drawing.Point(266, 36)
    Me.lbXx_escomp.Name = "lbXx_escomp"
    Me.lbXx_escomp.NTSDbField = ""
    Me.lbXx_escomp.Size = New System.Drawing.Size(365, 20)
    Me.lbXx_escomp.TabIndex = 520
    Me.lbXx_escomp.Tooltip = ""
    Me.lbXx_escomp.UseMnemonic = False
    '
    'edDatreg
    '
    Me.edDatreg.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatreg.EditValue = ""
    Me.edDatreg.Location = New System.Drawing.Point(201, 57)
    Me.edDatreg.Name = "edDatreg"
    Me.edDatreg.NTSDbField = ""
    Me.edDatreg.NTSForzaVisZoom = False
    Me.edDatreg.NTSOldValue = ""
    Me.edDatreg.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatreg.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatreg.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatreg.Properties.MaxLength = 65536
    Me.edDatreg.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatreg.Size = New System.Drawing.Size(89, 20)
    Me.edDatreg.TabIndex = 521
    '
    'lbDatreg
    '
    Me.lbDatreg.AutoSize = True
    Me.lbDatreg.BackColor = System.Drawing.Color.Transparent
    Me.lbDatreg.Location = New System.Drawing.Point(16, 60)
    Me.lbDatreg.Name = "lbDatreg"
    Me.lbDatreg.NTSDbField = ""
    Me.lbDatreg.Size = New System.Drawing.Size(106, 13)
    Me.lbDatreg.TabIndex = 522
    Me.lbDatreg.Text = "Data di registrazione"
    Me.lbDatreg.Tooltip = ""
    Me.lbDatreg.UseMnemonic = False
    '
    'fmDocumentiDacontabilizzare
    '
    Me.fmDocumentiDacontabilizzare.AllowDrop = True
    Me.fmDocumentiDacontabilizzare.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmDocumentiDacontabilizzare.Appearance.Options.UseBackColor = True
    Me.fmDocumentiDacontabilizzare.Controls.Add(Me.lbXx_codtpbf)
    Me.fmDocumentiDacontabilizzare.Controls.Add(Me.edCodtpbf)
    Me.fmDocumentiDacontabilizzare.Controls.Add(Me.lbCodtpbf)
    Me.fmDocumentiDacontabilizzare.Controls.Add(Me.fmOpzioni)
    Me.fmDocumentiDacontabilizzare.Controls.Add(Me.edNumerofin)
    Me.fmDocumentiDacontabilizzare.Controls.Add(Me.lbBarrafin)
    Me.fmDocumentiDacontabilizzare.Controls.Add(Me.lbSeriefin)
    Me.fmDocumentiDacontabilizzare.Controls.Add(Me.edSeriefin)
    Me.fmDocumentiDacontabilizzare.Controls.Add(Me.edNumeroini)
    Me.fmDocumentiDacontabilizzare.Controls.Add(Me.lbBarraini)
    Me.fmDocumentiDacontabilizzare.Controls.Add(Me.lbSerieini)
    Me.fmDocumentiDacontabilizzare.Controls.Add(Me.edSerieini)
    Me.fmDocumentiDacontabilizzare.Controls.Add(Me.lbDatfin)
    Me.fmDocumentiDacontabilizzare.Controls.Add(Me.edDatfin)
    Me.fmDocumentiDacontabilizzare.Controls.Add(Me.lbDatini)
    Me.fmDocumentiDacontabilizzare.Controls.Add(Me.edDatini)
    Me.fmDocumentiDacontabilizzare.Controls.Add(Me.edAnno)
    Me.fmDocumentiDacontabilizzare.Controls.Add(Me.lbAnno)
    Me.fmDocumentiDacontabilizzare.Controls.Add(Me.fmSelezioneTipoDocumento)
    Me.fmDocumentiDacontabilizzare.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmDocumentiDacontabilizzare.Location = New System.Drawing.Point(19, 160)
    Me.fmDocumentiDacontabilizzare.Name = "fmDocumentiDacontabilizzare"
    Me.fmDocumentiDacontabilizzare.Size = New System.Drawing.Size(670, 250)
    Me.fmDocumentiDacontabilizzare.TabIndex = 525
    Me.fmDocumentiDacontabilizzare.Text = "Selezione documenti da contabilizzare"
    '
    'lbXx_codtpbf
    '
    Me.lbXx_codtpbf.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codtpbf.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codtpbf.Location = New System.Drawing.Point(248, 170)
    Me.lbXx_codtpbf.Name = "lbXx_codtpbf"
    Me.lbXx_codtpbf.NTSDbField = ""
    Me.lbXx_codtpbf.Size = New System.Drawing.Size(407, 20)
    Me.lbXx_codtpbf.TabIndex = 544
    Me.lbXx_codtpbf.Tooltip = ""
    Me.lbXx_codtpbf.UseMnemonic = False
    '
    'edCodtpbf
    '
    Me.edCodtpbf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodtpbf.EditValue = ""
    Me.edCodtpbf.Location = New System.Drawing.Point(182, 170)
    Me.edCodtpbf.Name = "edCodtpbf"
    Me.edCodtpbf.NTSDbField = ""
    Me.edCodtpbf.NTSFormat = "0"
    Me.edCodtpbf.NTSForzaVisZoom = False
    Me.edCodtpbf.NTSOldValue = ""
    Me.edCodtpbf.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodtpbf.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodtpbf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodtpbf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodtpbf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodtpbf.Properties.MaxLength = 65536
    Me.edCodtpbf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodtpbf.Size = New System.Drawing.Size(59, 20)
    Me.edCodtpbf.TabIndex = 543
    '
    'lbCodtpbf
    '
    Me.lbCodtpbf.AutoSize = True
    Me.lbCodtpbf.BackColor = System.Drawing.Color.Transparent
    Me.lbCodtpbf.Location = New System.Drawing.Point(24, 173)
    Me.lbCodtpbf.Name = "lbCodtpbf"
    Me.lbCodtpbf.NTSDbField = ""
    Me.lbCodtpbf.Size = New System.Drawing.Size(141, 13)
    Me.lbCodtpbf.TabIndex = 542
    Me.lbCodtpbf.Text = "Tipo bolla/fattura (0 = tutti)"
    Me.lbCodtpbf.Tooltip = ""
    Me.lbCodtpbf.UseMnemonic = False
    '
    'fmOpzioni
    '
    Me.fmOpzioni.AllowDrop = True
    Me.fmOpzioni.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmOpzioni.Appearance.Options.UseBackColor = True
    Me.fmOpzioni.Controls.Add(Me.ckAnalitica)
    Me.fmOpzioni.Controls.Add(Me.ckRielab)
    Me.fmOpzioni.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmOpzioni.Location = New System.Drawing.Point(15, 192)
    Me.fmOpzioni.Name = "fmOpzioni"
    Me.fmOpzioni.ShowCaption = False
    Me.fmOpzioni.Size = New System.Drawing.Size(538, 48)
    Me.fmOpzioni.TabIndex = 541
    '
    'ckAnalitica
    '
    Me.ckAnalitica.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAnalitica.Location = New System.Drawing.Point(11, 23)
    Me.ckAnalitica.Name = "ckAnalitica"
    Me.ckAnalitica.NTSCheckValue = "S"
    Me.ckAnalitica.NTSUnCheckValue = "N"
    Me.ckAnalitica.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAnalitica.Properties.Appearance.Options.UseBackColor = True
    Me.ckAnalitica.Properties.Caption = "Genera movimenti di Contabilità Analitica associati"
    Me.ckAnalitica.Size = New System.Drawing.Size(269, 19)
    Me.ckAnalitica.TabIndex = 525
    '
    'ckRielab
    '
    Me.ckRielab.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckRielab.Location = New System.Drawing.Point(11, 5)
    Me.ckRielab.Name = "ckRielab"
    Me.ckRielab.NTSCheckValue = "S"
    Me.ckRielab.NTSUnCheckValue = "N"
    Me.ckRielab.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckRielab.Properties.Appearance.Options.UseBackColor = True
    Me.ckRielab.Properties.Caption = "Contabilizza anche documenti già contabilizzati in precedenza e cancella relative" & _
        " registrazioni"
    Me.ckRielab.Size = New System.Drawing.Size(470, 19)
    Me.ckRielab.TabIndex = 524
    '
    'edNumerofin
    '
    Me.edNumerofin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumerofin.EditValue = ""
    Me.edNumerofin.Location = New System.Drawing.Point(576, 149)
    Me.edNumerofin.Name = "edNumerofin"
    Me.edNumerofin.NTSDbField = ""
    Me.edNumerofin.NTSFormat = "0"
    Me.edNumerofin.NTSForzaVisZoom = False
    Me.edNumerofin.NTSOldValue = ""
    Me.edNumerofin.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumerofin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumerofin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNumerofin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNumerofin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumerofin.Properties.MaxLength = 65536
    Me.edNumerofin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumerofin.Size = New System.Drawing.Size(79, 20)
    Me.edNumerofin.TabIndex = 540
    '
    'lbBarrafin
    '
    Me.lbBarrafin.AutoSize = True
    Me.lbBarrafin.BackColor = System.Drawing.Color.Transparent
    Me.lbBarrafin.Location = New System.Drawing.Point(559, 152)
    Me.lbBarrafin.Name = "lbBarrafin"
    Me.lbBarrafin.NTSDbField = ""
    Me.lbBarrafin.Size = New System.Drawing.Size(11, 13)
    Me.lbBarrafin.TabIndex = 539
    Me.lbBarrafin.Text = "/"
    Me.lbBarrafin.Tooltip = ""
    Me.lbBarrafin.UseMnemonic = False
    '
    'lbSeriefin
    '
    Me.lbSeriefin.AutoSize = True
    Me.lbSeriefin.BackColor = System.Drawing.Color.Transparent
    Me.lbSeriefin.Location = New System.Drawing.Point(351, 152)
    Me.lbSeriefin.Name = "lbSeriefin"
    Me.lbSeriefin.NTSDbField = ""
    Me.lbSeriefin.Size = New System.Drawing.Size(135, 13)
    Me.lbSeriefin.TabIndex = 538
    Me.lbSeriefin.Text = "a serie/numero documento"
    Me.lbSeriefin.Tooltip = ""
    Me.lbSeriefin.UseMnemonic = False
    '
    'edSeriefin
    '
    Me.edSeriefin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSeriefin.Location = New System.Drawing.Point(510, 149)
    Me.edSeriefin.Name = "edSeriefin"
    Me.edSeriefin.NTSDbField = ""
    Me.edSeriefin.NTSForzaVisZoom = False
    Me.edSeriefin.NTSOldValue = ""
    Me.edSeriefin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSeriefin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSeriefin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSeriefin.Properties.MaxLength = 65536
    Me.edSeriefin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSeriefin.Size = New System.Drawing.Size(45, 20)
    Me.edSeriefin.TabIndex = 537
    '
    'edNumeroini
    '
    Me.edNumeroini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumeroini.EditValue = ""
    Me.edNumeroini.Location = New System.Drawing.Point(248, 149)
    Me.edNumeroini.Name = "edNumeroini"
    Me.edNumeroini.NTSDbField = ""
    Me.edNumeroini.NTSFormat = "0"
    Me.edNumeroini.NTSForzaVisZoom = False
    Me.edNumeroini.NTSOldValue = ""
    Me.edNumeroini.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumeroini.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumeroini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNumeroini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNumeroini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumeroini.Properties.MaxLength = 65536
    Me.edNumeroini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumeroini.Size = New System.Drawing.Size(79, 20)
    Me.edNumeroini.TabIndex = 536
    '
    'lbBarraini
    '
    Me.lbBarraini.AutoSize = True
    Me.lbBarraini.BackColor = System.Drawing.Color.Transparent
    Me.lbBarraini.Location = New System.Drawing.Point(231, 152)
    Me.lbBarraini.Name = "lbBarraini"
    Me.lbBarraini.NTSDbField = ""
    Me.lbBarraini.Size = New System.Drawing.Size(11, 13)
    Me.lbBarraini.TabIndex = 535
    Me.lbBarraini.Text = "/"
    Me.lbBarraini.Tooltip = ""
    Me.lbBarraini.UseMnemonic = False
    '
    'lbSerieini
    '
    Me.lbSerieini.AutoSize = True
    Me.lbSerieini.BackColor = System.Drawing.Color.Transparent
    Me.lbSerieini.Location = New System.Drawing.Point(23, 152)
    Me.lbSerieini.Name = "lbSerieini"
    Me.lbSerieini.NTSDbField = ""
    Me.lbSerieini.Size = New System.Drawing.Size(142, 13)
    Me.lbSerieini.TabIndex = 534
    Me.lbSerieini.Text = "Da serie/numero documento"
    Me.lbSerieini.Tooltip = ""
    Me.lbSerieini.UseMnemonic = False
    '
    'edSerieini
    '
    Me.edSerieini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSerieini.Location = New System.Drawing.Point(182, 149)
    Me.edSerieini.Name = "edSerieini"
    Me.edSerieini.NTSDbField = ""
    Me.edSerieini.NTSForzaVisZoom = False
    Me.edSerieini.NTSOldValue = ""
    Me.edSerieini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSerieini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSerieini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSerieini.Properties.MaxLength = 65536
    Me.edSerieini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSerieini.Size = New System.Drawing.Size(45, 20)
    Me.edSerieini.TabIndex = 533
    '
    'lbDatfin
    '
    Me.lbDatfin.AutoSize = True
    Me.lbDatfin.BackColor = System.Drawing.Color.Transparent
    Me.lbDatfin.Location = New System.Drawing.Point(351, 131)
    Me.lbDatfin.Name = "lbDatfin"
    Me.lbDatfin.NTSDbField = ""
    Me.lbDatfin.Size = New System.Drawing.Size(94, 13)
    Me.lbDatfin.TabIndex = 532
    Me.lbDatfin.Text = "a data documento"
    Me.lbDatfin.Tooltip = ""
    Me.lbDatfin.UseMnemonic = False
    '
    'edDatfin
    '
    Me.edDatfin.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edDatfin.EditValue = ""
    Me.edDatfin.Location = New System.Drawing.Point(510, 128)
    Me.edDatfin.Name = "edDatfin"
    Me.edDatfin.NTSDbField = ""
    Me.edDatfin.NTSForzaVisZoom = False
    Me.edDatfin.NTSOldValue = ""
    Me.edDatfin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatfin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatfin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatfin.Properties.MaxLength = 65536
    Me.edDatfin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatfin.Size = New System.Drawing.Size(89, 20)
    Me.edDatfin.TabIndex = 531
    '
    'lbDatini
    '
    Me.lbDatini.AutoSize = True
    Me.lbDatini.BackColor = System.Drawing.Color.Transparent
    Me.lbDatini.Location = New System.Drawing.Point(23, 131)
    Me.lbDatini.Name = "lbDatini"
    Me.lbDatini.NTSDbField = ""
    Me.lbDatini.Size = New System.Drawing.Size(101, 13)
    Me.lbDatini.TabIndex = 530
    Me.lbDatini.Text = "Da data documento"
    Me.lbDatini.Tooltip = ""
    Me.lbDatini.UseMnemonic = False
    '
    'edDatini
    '
    Me.edDatini.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edDatini.EditValue = ""
    Me.edDatini.Location = New System.Drawing.Point(182, 128)
    Me.edDatini.Name = "edDatini"
    Me.edDatini.NTSDbField = ""
    Me.edDatini.NTSForzaVisZoom = False
    Me.edDatini.NTSOldValue = ""
    Me.edDatini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatini.Properties.MaxLength = 65536
    Me.edDatini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatini.Size = New System.Drawing.Size(89, 20)
    Me.edDatini.TabIndex = 529
    '
    'edAnno
    '
    Me.edAnno.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnno.EditValue = ""
    Me.edAnno.Location = New System.Drawing.Point(182, 107)
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
    Me.edAnno.Size = New System.Drawing.Size(59, 20)
    Me.edAnno.TabIndex = 528
    '
    'lbAnno
    '
    Me.lbAnno.AutoSize = True
    Me.lbAnno.BackColor = System.Drawing.Color.Transparent
    Me.lbAnno.Location = New System.Drawing.Point(23, 110)
    Me.lbAnno.Name = "lbAnno"
    Me.lbAnno.NTSDbField = ""
    Me.lbAnno.Size = New System.Drawing.Size(139, 13)
    Me.lbAnno.TabIndex = 527
    Me.lbAnno.Text = "Anno documento (0 = tutti)"
    Me.lbAnno.Tooltip = ""
    Me.lbAnno.UseMnemonic = False
    '
    'fmSelezioneTipoDocumento
    '
    Me.fmSelezioneTipoDocumento.AllowDrop = True
    Me.fmSelezioneTipoDocumento.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmSelezioneTipoDocumento.Appearance.Options.UseBackColor = True
    Me.fmSelezioneTipoDocumento.Controls.Add(Me.ckTipoRkMResicli)
    Me.fmSelezioneTipoDocumento.Controls.Add(Me.ckTipoRkBResiforn)
    Me.fmSelezioneTipoDocumento.Controls.Add(Me.ckTiporkM)
    Me.fmSelezioneTipoDocumento.Controls.Add(Me.ckTiporkB)
    Me.fmSelezioneTipoDocumento.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmSelezioneTipoDocumento.Location = New System.Drawing.Point(15, 23)
    Me.fmSelezioneTipoDocumento.Name = "fmSelezioneTipoDocumento"
    Me.fmSelezioneTipoDocumento.ShowCaption = False
    Me.fmSelezioneTipoDocumento.Size = New System.Drawing.Size(329, 82)
    Me.fmSelezioneTipoDocumento.TabIndex = 526
    '
    'ckTipoRkMResicli
    '
    Me.ckTipoRkMResicli.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTipoRkMResicli.Location = New System.Drawing.Point(11, 59)
    Me.ckTipoRkMResicli.Name = "ckTipoRkMResicli"
    Me.ckTipoRkMResicli.NTSCheckValue = "S"
    Me.ckTipoRkMResicli.NTSUnCheckValue = "N"
    Me.ckTipoRkMResicli.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTipoRkMResicli.Properties.Appearance.Options.UseBackColor = True
    Me.ckTipoRkMResicli.Properties.Caption = "Resi Clienti - Note di accredito da emettere"
    Me.ckTipoRkMResicli.Size = New System.Drawing.Size(234, 19)
    Me.ckTipoRkMResicli.TabIndex = 527
    '
    'ckTipoRkBResiforn
    '
    Me.ckTipoRkBResiforn.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTipoRkBResiforn.Location = New System.Drawing.Point(11, 41)
    Me.ckTipoRkBResiforn.Name = "ckTipoRkBResiforn"
    Me.ckTipoRkBResiforn.NTSCheckValue = "S"
    Me.ckTipoRkBResiforn.NTSUnCheckValue = "N"
    Me.ckTipoRkBResiforn.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTipoRkBResiforn.Properties.Appearance.Options.UseBackColor = True
    Me.ckTipoRkBResiforn.Properties.Caption = "Resi Fornitori - Note di accredito da ricevere"
    Me.ckTipoRkBResiforn.Size = New System.Drawing.Size(245, 19)
    Me.ckTipoRkBResiforn.TabIndex = 526
    '
    'ckTiporkM
    '
    Me.ckTiporkM.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTiporkM.Location = New System.Drawing.Point(11, 23)
    Me.ckTiporkM.Name = "ckTiporkM"
    Me.ckTiporkM.NTSCheckValue = "S"
    Me.ckTiporkM.NTSUnCheckValue = "N"
    Me.ckTiporkM.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTiporkM.Properties.Appearance.Options.UseBackColor = True
    Me.ckTiporkM.Properties.Caption = "D.D.T. Ricevuti - Fatture da ricevere"
    Me.ckTiporkM.Size = New System.Drawing.Size(202, 19)
    Me.ckTiporkM.TabIndex = 525
    '
    'ckTiporkB
    '
    Me.ckTiporkB.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTiporkB.Location = New System.Drawing.Point(11, 5)
    Me.ckTiporkB.Name = "ckTiporkB"
    Me.ckTiporkB.NTSCheckValue = "S"
    Me.ckTiporkB.NTSUnCheckValue = "N"
    Me.ckTiporkB.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTiporkB.Properties.Appearance.Options.UseBackColor = True
    Me.ckTiporkB.Properties.Caption = "D.D.T. Emessi - Fatture da emettere"
    Me.ckTiporkB.Size = New System.Drawing.Size(202, 19)
    Me.ckTiporkB.TabIndex = 524
    '
    'ckStorno
    '
    Me.ckStorno.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckStorno.Location = New System.Drawing.Point(5, 0)
    Me.ckStorno.Name = "ckStorno"
    Me.ckStorno.NTSCheckValue = "S"
    Me.ckStorno.NTSUnCheckValue = "N"
    Me.ckStorno.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckStorno.Properties.Appearance.Options.UseBackColor = True
    Me.ckStorno.Properties.Caption = "Genera registrazioni di storno"
    Me.ckStorno.Size = New System.Drawing.Size(174, 19)
    Me.ckStorno.TabIndex = 523
    '
    'lbEscompSt
    '
    Me.lbEscompSt.AutoSize = True
    Me.lbEscompSt.BackColor = System.Drawing.Color.Transparent
    Me.lbEscompSt.Enabled = False
    Me.lbEscompSt.Location = New System.Drawing.Point(23, 26)
    Me.lbEscompSt.Name = "lbEscompSt"
    Me.lbEscompSt.NTSDbField = ""
    Me.lbEscompSt.Size = New System.Drawing.Size(120, 13)
    Me.lbEscompSt.TabIndex = 524
    Me.lbEscompSt.Text = "Esercizio di competenza"
    Me.lbEscompSt.Tooltip = ""
    Me.lbEscompSt.UseMnemonic = False
    '
    'edEscompSt
    '
    Me.edEscompSt.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEscompSt.EditValue = ""
    Me.edEscompSt.Enabled = False
    Me.edEscompSt.Location = New System.Drawing.Point(182, 23)
    Me.edEscompSt.Name = "edEscompSt"
    Me.edEscompSt.NTSDbField = ""
    Me.edEscompSt.NTSFormat = "0"
    Me.edEscompSt.NTSForzaVisZoom = False
    Me.edEscompSt.NTSOldValue = ""
    Me.edEscompSt.Properties.Appearance.Options.UseTextOptions = True
    Me.edEscompSt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEscompSt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEscompSt.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEscompSt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEscompSt.Properties.MaxLength = 65536
    Me.edEscompSt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEscompSt.Size = New System.Drawing.Size(59, 20)
    Me.edEscompSt.TabIndex = 525
    '
    'lbXx_escompst
    '
    Me.lbXx_escompst.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_escompst.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_escompst.Location = New System.Drawing.Point(247, 23)
    Me.lbXx_escompst.Name = "lbXx_escompst"
    Me.lbXx_escompst.NTSDbField = ""
    Me.lbXx_escompst.Size = New System.Drawing.Size(365, 20)
    Me.lbXx_escompst.TabIndex = 526
    Me.lbXx_escompst.Tooltip = ""
    Me.lbXx_escompst.UseMnemonic = False
    '
    'edDatregst
    '
    Me.edDatregst.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatregst.EditValue = ""
    Me.edDatregst.Enabled = False
    Me.edDatregst.Location = New System.Drawing.Point(182, 44)
    Me.edDatregst.Name = "edDatregst"
    Me.edDatregst.NTSDbField = ""
    Me.edDatregst.NTSForzaVisZoom = False
    Me.edDatregst.NTSOldValue = ""
    Me.edDatregst.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatregst.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatregst.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatregst.Properties.MaxLength = 65536
    Me.edDatregst.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatregst.Size = New System.Drawing.Size(89, 20)
    Me.edDatregst.TabIndex = 527
    '
    'lbDatregst
    '
    Me.lbDatregst.AutoSize = True
    Me.lbDatregst.BackColor = System.Drawing.Color.Transparent
    Me.lbDatregst.Enabled = False
    Me.lbDatregst.Location = New System.Drawing.Point(23, 47)
    Me.lbDatregst.Name = "lbDatregst"
    Me.lbDatregst.NTSDbField = ""
    Me.lbDatregst.Size = New System.Drawing.Size(106, 13)
    Me.lbDatregst.TabIndex = 528
    Me.lbDatregst.Text = "Data di registrazione"
    Me.lbDatregst.Tooltip = ""
    Me.lbDatregst.UseMnemonic = False
    '
    'fmRegistrazioniDiStorno
    '
    Me.fmRegistrazioniDiStorno.AllowDrop = True
    Me.fmRegistrazioniDiStorno.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmRegistrazioniDiStorno.Appearance.Options.UseBackColor = True
    Me.fmRegistrazioniDiStorno.Controls.Add(Me.lbDatregst)
    Me.fmRegistrazioniDiStorno.Controls.Add(Me.edDatregst)
    Me.fmRegistrazioniDiStorno.Controls.Add(Me.lbXx_escompst)
    Me.fmRegistrazioniDiStorno.Controls.Add(Me.edEscompSt)
    Me.fmRegistrazioniDiStorno.Controls.Add(Me.lbEscompSt)
    Me.fmRegistrazioniDiStorno.Controls.Add(Me.ckStorno)
    Me.fmRegistrazioniDiStorno.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmRegistrazioniDiStorno.Location = New System.Drawing.Point(19, 83)
    Me.fmRegistrazioniDiStorno.Name = "fmRegistrazioniDiStorno"
    Me.fmRegistrazioniDiStorno.Size = New System.Drawing.Size(670, 71)
    Me.fmRegistrazioniDiStorno.TabIndex = 524
    '
    'lbStatus
    '
    Me.lbStatus.BackColor = System.Drawing.Color.Transparent
    Me.lbStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbStatus.Location = New System.Drawing.Point(16, 421)
    Me.lbStatus.Name = "lbStatus"
    Me.lbStatus.NTSDbField = ""
    Me.lbStatus.Size = New System.Drawing.Size(668, 13)
    Me.lbStatus.TabIndex = 526
    Me.lbStatus.Text = "Pronto"
    Me.lbStatus.Tooltip = ""
    Me.lbStatus.UseMnemonic = False
    '
    'FRMVEPNSX
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(709, 447)
    Me.Controls.Add(Me.lbStatus)
    Me.Controls.Add(Me.fmDocumentiDacontabilizzare)
    Me.Controls.Add(Me.fmRegistrazioniDiStorno)
    Me.Controls.Add(Me.lbDatreg)
    Me.Controls.Add(Me.edDatreg)
    Me.Controls.Add(Me.lbXx_escomp)
    Me.Controls.Add(Me.edEscomp)
    Me.Controls.Add(Me.lbEscomp)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRMVEPNSX"
    Me.Text = "CONTABILIZZAZIONE STANZIAMENTI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEscomp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatreg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmDocumentiDacontabilizzare, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmDocumentiDacontabilizzare.ResumeLayout(False)
    Me.fmDocumentiDacontabilizzare.PerformLayout()
    CType(Me.edCodtpbf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmOpzioni, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmOpzioni.ResumeLayout(False)
    Me.fmOpzioni.PerformLayout()
    CType(Me.ckAnalitica.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckRielab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNumerofin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSeriefin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNumeroini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerieini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatfin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmSelezioneTipoDocumento, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmSelezioneTipoDocumento.ResumeLayout(False)
    Me.fmSelezioneTipoDocumento.PerformLayout()
    CType(Me.ckTipoRkMResicli.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTipoRkBResiforn.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTiporkM.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTiporkB.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckStorno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEscompSt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatregst.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmRegistrazioniDiStorno, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmRegistrazioniDiStorno.ResumeLayout(False)
    Me.fmRegistrazioniDiStorno.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbElabora.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbIntegr.GlyphPath = (oApp.ChildImageDir & "\int.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      '----------------------------------------------------------------------------------
      edEscomp.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128776150671634004, "Esercizio di competenza"), tabesco)
      edDatreg.NTSSetParam(oMenu, oApp.Tr(Me, 128776151322102754, "Data di registrazione"), False)
      ckStorno.NTSSetParam(oMenu, oApp.Tr(Me, 128776152629446504, "Genera registrazioni di storno"), "S", "N")
      edEscompSt.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128776153562571504, "Esercizio di competenza"), tabesco)
      edDatregst.NTSSetParam(oMenu, oApp.Tr(Me, 128776153780384004, "Data di registrazione"), True)
      ckTiporkB.NTSSetParam(oMenu, oApp.Tr(Me, 128776154038665254, "D.D.T. Emessi - Fatture da ricevere"), "S", "N")
      ckTiporkM.NTSSetParam(oMenu, oApp.Tr(Me, 128776154342884004, "D.D.T. Ricevuti - Fatture da ricevere"), "S", "N")
      ckTipoRkBResiforn.NTSSetParam(oMenu, oApp.Tr(Me, 128776154625696504, "Resi Fornitori - Note di accredito da ricevere"), "S", "N")
      ckTipoRkMResicli.NTSSetParam(oMenu, oApp.Tr(Me, 128776155180041859, "Resi Clienti - Note di accredito da emettere"), "S", "N")
      edAnno.NTSSetParam(oMenu, oApp.Tr(Me, 128776155913948109, "Anno documento (0 = tutti)"), "0", 4, 0, 2099)
      edDatini.NTSSetParam(oMenu, oApp.Tr(Me, 128776226468994324, "Da data documento"), True)
      edDatfin.NTSSetParam(oMenu, oApp.Tr(Me, 128776226718480309, "a data documento"), True)
      edSerieini.NTSSetParam(oMenu, oApp.Tr(Me, 128776226872562244, "Da serie documento"), CLN__STD.SerieMaxLen, False)
      edNumeroini.NTSSetParam(oMenu, oApp.Tr(Me, 128776228245130644, "Da numero documento"), "0", 9, 0, 999999999)
      edSeriefin.NTSSetParam(oMenu, oApp.Tr(Me, 128776228699154319, "a serie documento"), CLN__STD.SerieMaxLen, False)
      edNumerofin.NTSSetParam(oMenu, oApp.Tr(Me, 128776228868059524, "a numero documento"), "0", 9, 0, 999999999)
      edCodtpbf.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128776231714659724, "Tipo bolla/fattura (0 = tutti)"), tabtpbf)
      '----------------------------------------------------------------------------------
      edEscomp.NTSForzaVisZoom = True
      edEscompSt.NTSForzaVisZoom = True
      edCodtpbf.NTSForzaVisZoom = True
      '----------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRMVEPNSX_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '----------------------------------------------------------------------------------
      If oCallParams Is Nothing Then oClePnsx.bDaProgramma = False Else oClePnsx.bDaProgramma = True
      '----------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------        
      bModuliAcquistati = False
      If (CBool(oMenu.ModuliDittaDitt(DittaCorrente) And bsModCG) = True) Or _
         (CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtCGE) = True) Then
        bModuliAcquistati = True
      End If
      If bModuliAcquistati = False Then
        If oCallParams Is Nothing Then
          strMsg = "Attenzione!" & vbCrLf & _
            "Non è possibile contabilizzare documenti" & vbCrLf & _
            "in quanto il modulo 'Contabilità generale' non risulta essere tra i moduli acquistati." & vbCrLf & _
            "Avvio programma non possibile."
          oApp.MsgBoxErr(oApp.Tr(Me, 129290115466044922, strMsg))
        End If
        Me.Close()
        Return
      End If

      If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupCAE) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129290114456132813, "ATTENZIONE: non è possibile utilizzare questo programma con il modulo di 'Contabilità analitica duplice contabile' attivo"))
        Me.Close()
        Return
      End If
      '----------------------------------------------------------------------------------
      If LeggeRegistro() = False Then
        Me.Close()
        Return
      End If
      '----------------------------------------------------------------------------------
      '--- Propone l'esercizio di competenza (e decodifica) della ditta corrente
      '--- e il giorno e mese correnti con anno indicato in data inizio esercizio
      '--- relativo all'esercizio di competenza della ditta corrente
      '--- (giorno e mese di sistema + anno di TABESCO.tb_dtineser)
      '----------------------------------------------------------------------------------
      edEscomp.Text = nEscomp.ToString
      If nEscomp <> 0 Then
        oMenu.ValCodiceDb(nEscomp.ToString, DittaCorrente, "TABESCO", "N", "", dttTmp)
        If dttTmp.Rows.Count <> 0 Then
          lbXx_escomp.Text = dttTmp.Rows(0)!tb_desesco.ToString
          edDatreg.Text = IntSetDate(Microsoft.VisualBasic.Right("0" & DateTime.Now.Day.ToString, 2) & "/" & Microsoft.VisualBasic.Right("0" & DateTime.Now.Month.ToString, 2) & "/" & Year(CDate(dttTmp.Rows(0)!tb_dtineser)))
        End If
        dttTmp.Clear()
        dttTmp.Dispose()
      End If
      '----------------------------------------------------------------------------------
      lbEscompSt.Enabled = False : edEscompSt.Enabled = False
      lbDatregst.Enabled = False : edDatregst.Enabled = False
      '----------------------------------------------------------------------------------
      If oClePnsx.bStanziamentiStornoAutomaticoSubito = True Then
        ckStorno.Checked = True
      Else
        lbEscompSt.Enabled = False
        lbDatregst.Enabled = False
        edEscompSt.Text = "0" : lbXx_escompst.Text = ""
        edDatregst.Text = ""
      End If
      ckTiporkB.Checked = False
      ckTiporkM.Checked = False
      edAnno.Text = DateTime.Now.Year.ToString
      edDatini.Text = DateTime.Now.ToShortDateString
      edDatfin.Text = DateTime.Now.ToShortDateString
      edSerieini.Text = " "
      edSeriefin.Text = "z".PadRight(CLN__STD.SerieMaxLen, "z"c)
      edNumeroini.Text = "0"
      edNumerofin.Text = "".PadLeft(9, "9"c)
      edCodtpbf.Text = "0" : lbXx_codtpbf.Text = ""
      ckRielab.Checked = True
      If CBool(oMenu.ModuliDittaDitt(DittaCorrente) And bsModCI) = True Then
        ckAnalitica.Checked = True
        GctlSetVisEnab(ckAnalitica, True)
      Else
        ckAnalitica.Checked = False
        ckAnalitica.Enabled = False
      End If
      '----------------------------------------------------------------------------------
      If oCallParams Is Nothing Then
        oClePnsx.strOpnomeElab = oApp.User.Nome
      Else
        oClePnsx.strOpnomeElab = Trim(Mid(oCallParams.strParam, 38, 20))
        tlbIntegr.Down = True
        oClePnsx.strPnint = "S"
        edDatreg.Text = Trim(Mid(oCallParams.strParam, 27, 10))
        ckStorno.Checked = False
        ckTiporkB.Checked = True
        ckTiporkM.Checked = True
        ckTipoRkBResiforn.Checked = True
        ckTipoRkMResicli.Checked = True
        edDatini.Text = Trim(Mid(oCallParams.strParam, 16, 10))
        edDatfin.Text = Trim(Mid(oCallParams.strParam, 27, 10))
        ckRielab.Checked = False
        ckAnalitica.Checked = False
        If Trim(Mid(oCallParams.strParam, 59, 1)) = "1" Then Me.Visible = True
        EseguiEleborazione()
        Me.Close()
      End If
      '----------------------------------------------------------------------------------
      '--- Sempre alla fine di questa funzione: applico le regole della gctl
      '----------------------------------------------------------------------------------
      GctlSetRoules()
      '----------------------------------------------------------------------------------
      GctlApplicaDefaultValue()
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "TextBox"
  Public Overridable Sub edAnno_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAnno.Validated
    Try
      If NTSCInt(edAnno.Text) = 0 Then Return
      If (NTSCInt(edAnno.Text) < 1900) Or (NTSCInt(edAnno.Text) > 2099) Then
        strMsg = "Attenzione!" & vbCrLf & _
          "L'anno, se indicato, deve essere compreso fra 1900 e 2099."
        oApp.MsgBoxErr(oApp.Tr(Me, 128776936495006365, strMsg))
        edAnno.Text = NTSCStr(edAnno.OldEditValue)
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edCodtpbf_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodtpbf.Validated
    Try
      If oClePnsx Is Nothing Then Return
      If oClePnsx.edCodtpbf_Validated(NTSCInt(edCodtpbf.Text), strTmp) = False Then
        edCodtpbf.Text = NTSCStr(edCodtpbf.OldEditValue)
      Else
        lbXx_codtpbf.Text = strTmp
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edDatreg_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDatreg.Validated
    Try
      If (edDatreg.Text = edDatreg.OldEditValue.ToString) And (ckStorno.Checked = True) Then
        edDatregst.Text = NTSCStr(DateAdd("d", 1, NTSCDate(edDatreg.Text)))
        If oClePnsx Is Nothing Then Return
        If oClePnsx.LeggiTabesco(edDatregst.Text, nCodesco, strDesesco) = False Then
          strMsg = "Attenzione!" & vbCrLf & _
            "Non esiste un esercizio di competenza successivo a quello corrente." & vbCrLf & _
            "Indicare un esercizio di competenza e una data di registrazione di storno validi."
          oApp.MsgBoxErr(oApp.Tr(Me, 128776924587253155, strMsg))
          ckStorno.Checked = False
        Else
          edEscompSt.Text = nCodesco.ToString
          lbXx_escompst.Text = strDesesco
        End If
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edDatregst_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDatregst.Validated
    Try
      If ckStorno.Checked = False Then Return
      If oClePnsx Is Nothing Then Return
      If oClePnsx.CheckDatregst(NTSCInt(edEscompSt.Text), edDatregst.Text) = False Then
        strMsg = "Attenzione!" & vbCrLf & _
          "La data di registrazione indicata (registrazione di storno) non rientra nell'esercizio di competenza della ditta." & vbCrLf & _
          "Ripetere l'inserimento."
        oApp.MsgBoxErr(oApp.Tr(Me, 128776934397193865, strMsg))
        ckStorno.Checked = False
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edEscomp_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edEscomp.Validated
    Try
      If oClePnsx Is Nothing Then Return
      If Not oClePnsx.edEscomp_Validated(NTSCInt(edEscomp.Text), strTmp) Then
        edEscomp.Text = NTSCStr(edEscomp.OldEditValue)
      Else
        lbXx_escomp.Text = strTmp
        If (NTSCInt(edEscomp.Text) <> nEscomp) And (NTSCInt(edEscomp.Text) <> nEscompp) Then
          strMsg = "La ditta corrente '|" & DittaCorrente & "|'" & vbCrLf & _
            "ha come esercizio corrente: '|" & nEscomp.ToString & "|'" & vbCrLf & _
            "e come esercizio precedente: '|" & nEscompp.ToString & "|'" & vbCrLf & _
            "mentre l'esercizio di competenza indicato è '|" & edEscomp.Text & "|'." & vbCrLf & _
            "Ripetere l'inserimento."
          edEscomp.Text = NTSCStr(edEscomp.OldEditValue)
          oApp.MsgBoxErr(oApp.Tr(Me, 128776918643659405, strMsg))
        End If
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edEscompSt_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edEscompSt.Validated
    Try
      If ckStorno.Checked = False Then Return
      If oClePnsx Is Nothing Then Return
      If Not oClePnsx.edEscompst_Validated(NTSCInt(edEscompSt.Text), strTmp) Then
        edEscompSt.Text = NTSCStr(edEscompSt.OldEditValue)
      Else
        lbXx_escompst.Text = strTmp
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edSerieini_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSerieini.Validated
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edSerieini.Text, False)
      If strTmp <> edSerieini.Text Then edSerieini.Text = strTmp

    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edSeriefin_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSeriefin.Validated
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edSeriefin.Text, False)
      If strTmp <> edSeriefin.Text Then edSeriefin.Text = strTmp

    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub
#End Region

#Region "CheckBox"
  Public Overridable Sub ckStorno_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckStorno.CheckedChanged
    Try
      If ckStorno.Checked = True Then
        GctlSetVisEnab(lbEscompSt, False)
        GctlSetVisEnab(lbDatregst, False)
        GctlSetVisEnab(edEscompSt, False)
        GctlSetVisEnab(edDatregst, False)
        If oClePnsx Is Nothing Then Return
        strTmp = NTSCStr(DateAdd("d", 1, NTSCDate(edDatreg.Text)))
        If oClePnsx.LeggiTabesco(strTmp, nCodesco, strDesesco) = False Then
          strMsg = "Attenzione!" & vbCrLf & _
            "Non esiste un esercizio di competenza successivo a quello corrente." & vbCrLf & _
            "Indicare un esercizio di competenza e una data di registrazione di storno validi."
          oApp.MsgBoxErr(oApp.Tr(Me, 128776916006471905, strMsg))
          ckStorno.Checked = False
        Else
          edEscompSt.Text = nCodesco.ToString
          lbXx_escompst.Text = strDesesco
        End If
        edDatregst.Text = NTSCStr(DateAdd("d", 1, NTSCDate(edDatreg.Text)))
      Else
        lbEscompSt.Enabled = False
        lbDatregst.Enabled = False
        edEscompSt.Text = "0" : lbXx_escompst.Text = ""
        edEscompSt.Enabled = False
        edDatregst.Text = ""
        edDatregst.Enabled = False
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbElabora_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbElabora.ItemClick
    Try
      EseguiEleborazione()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbIntegr_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbIntegr.ItemClick
    Try
      If tlbIntegr.Down Then
        oClePnsx.strPnint = "S"
        ckStorno.Checked = True
        ckStorno.Enabled = False
      Else
        oClePnsx.strPnint = "N"
        ckStorno.Checked = False
        GctlSetVisEnab(ckStorno, False)
        If oClePnsx.bStanziamentiStornoAutomaticoSubito = True Then
          ckStorno.Checked = True
        Else
          ckStorno.Checked = False
        End If
      End If
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB
      NTSCallStandardZoom()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub
#End Region

#Region "Funzioni"
  Public Overridable Function CheckDati() As Boolean
    Dim strDtullg As String = ""
    Try
      '----------------------------------------------------------------------------------
      '--- Test su Libro Giornale
      '----------------------------------------------------------------------------------
      strDtullg = oClePnsx.CalcolaDataFineStampaLG(NTSCInt(edEscomp.Text))
      If CDate(strDtullg) > CDate(edDatreg.Text) Then
        strMsg = "Attenzione!" & vbCrLf & _
            "Data di registrazione anteriore all'ultima stampa del Libro Giornale" & vbCrLf & _
            "Contabilizzazione documenti non possibile."
        oApp.MsgBoxErr(oApp.Tr(Me, 128777747173573310, strMsg))
        Return False
      End If
      '----------------------------------------------------------------------------------
      If Not (UCase(edSerieini.Text) = " " And UCase(edSeriefin.Text) = "Z") Then
        If Not (edSerieini.Text = edSeriefin.Text) Then
          strMsg = "Attenzione!" & vbCrLf & _
            "Per selezione serie documento occorre che le due serie siano uguali."
          oApp.MsgBoxErr(oApp.Tr(Me, 128776938811412615, strMsg))
          Return False
        End If
      End If
      '----------------------------------------------------------------------------------
      If edSerieini.Text > edSeriefin.Text Then
        strMsg = "Attenzione!" & vbCrLf & _
          "La serie iniziale non può essere superiore a quella finale."
        oApp.MsgBoxErr(oApp.Tr(Me, 128776939506725115, strMsg))
        edSerieini.Text = " "
        edSeriefin.Text = "z".PadRight(CLN__STD.SerieMaxLen, "z"c)
        If edSerieini.Enabled = True Then edSerieini.Focus()
        Return False
      End If
      '----------------------------------------------------------------------------------
      If CDate(edDatini.Text) > CDate(edDatfin.Text) Then
        strMsg = "Attenzione!" & vbCrLf & _
            "La data documento iniziale non può essere superiore a quella finale."
        oApp.MsgBoxErr(oApp.Tr(Me, 128776941486197445, strMsg))
        edDatini.Text = DateTime.Now.ToShortDateString
        edDatfin.Text = DateTime.Now.ToShortDateString
        If edDatini.Enabled = True Then edDatini.Focus()
        Return False
      End If
      '----------------------------------------------------------------------------------
      If (ckTiporkB.Checked = False) And (ckTiporkM.Checked = False) And _
         (ckTipoRkMResicli.Checked = False) And (ckTipoRkBResiforn.Checked = False) Then
        strMsg = "Attenzione!" & vbCrLf & _
            "Selezionare almeno un tipo di documento da elaborare."
        oApp.MsgBoxErr(oApp.Tr(Me, 128776950019166195, strMsg))
        Return False
      End If
      '----------------------------------------------------------------------------------
      '--- Chiede conferma dell'elaborazione
      '----------------------------------------------------------------------------------
      If oCallParams Is Nothing Then
        strMsg = "Confermare l'elaborazione degli stanziamenti con i dati impostati?"
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128776948924322445, strMsg)) = Windows.Forms.DialogResult.No Then Return False
      End If
      '----------------------------------------------------------------------------------
      '--- Se la data di registrazione NON è uguale alla data di fine esercizio relativa
      '--- all'esercizio di competenza e il pulsante di Prima Nota integrativa NON è selezionato
      '--- chiede ulteriore conferma dell'elaborazione
      '----------------------------------------------------------------------------------
      If tlbIntegr.Down = False Then
        strDtfieser = oClePnsx.RitornaDataFineEsercizio(NTSCInt(edEscomp.Text))
        If CDate(edDatreg.Text) <> CDate(strDtfieser) Then
          strMsg = "Attenzione!" & vbCrLf & _
            "Elaborazioni infra-esercizio dovrebbero essere eseguite" & vbCrLf & _
            "su registrazioni di Prima Nota Integrativa." & vbCrLf & _
            "Operare diversamente!" & vbCrLf & _
            "E' altamente sconsigliabile per la coerenza contabile dei dati gestiti." & vbCrLf & _
            "Confermare ugualmente l'elaborazione su Prima Nota NON Integrativa?"
          If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128783862293549720, strMsg)) = Windows.Forms.DialogResult.No Then Return False
        End If
      End If
      '----------------------------------------------------------------------------------
      Return True
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function CheckInfraEsercizio() As Boolean
    Try
      '----------------------------------------------------------------------------------
      '--- Se la data di registrazione NON è uguale alla data di fine esercizio relativa
      '--- all'esercizio di competenza e il pulsante di Prima Nota integrativa NON è selezionato
      '--- chiede ulteriore conferma dell'elaborazione
      '----------------------------------------------------------------------------------
      If tlbIntegr.Down = True Then Return True
      '----------------------------------------------------------------------------------
      oMenu.ValCodiceDb(edEscomp.Text, DittaCorrente, "TABESCO", "N", "", dttTmp)
      If dttTmp.Rows.Count = 0 Then
        If CDate(edDatreg.Text) <> CDate(dttTmp.Rows(0)!tb_dtfieser.ToString) Then
          strMsg = "Attenzione!" & vbCrLf & _
            "Elaborazioni infra-esercizio dovrebbero essere eseguite " & _
            "su registrazioni di Prima Nota Integrativa." & vbCrLf & _
            "Operare diversamente!" & vbCrLf & _
            "E' altamente sconsigliabile per la coerenza contabile dei dati gestiti." & vbCrLf & _
            "Confermare ugualmente l'elaborazione su Prima Nota NON Integrativa?"
          If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128776956760572445, strMsg)) = Windows.Forms.DialogResult.No Then Return False
        End If
      End If
      '----------------------------------------------------------------------------------
      Return True
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overloads Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    'questa funzione riceve gli eventi dall'ENTITY: rimappata rispetto a quella standard di FRM__CHILD
    'prima eseguo quella standard
    Dim strTmp() As String
    Dim i As Integer = 0

    If Not IsMyThrowRemoteEvent() Then Return 'il messaggio non è per questa form ...
    MyBase.GestisciEventiEntity(sender, e)

    Try
      'adesso gestisco le specifiche
      'devo inserire delle funzioni qui sotto per fare in modo che al variare di dati nell'entity delle informazioni 
      'legate all'interfaccia grafica (ui) vengano allineate a quanto richiesto dall'entity

      If e.TipoEvento.Length < 10 Then Return
      strTmp = e.TipoEvento.Split(CType("|", Char))
      For i = 0 To strTmp.Length - 1
        Select Case strTmp(i).Substring(0, 10)
          Case "STATUSBAR:"
            lbStatus.Text = e.Message
            Me.Refresh()
        End Select
      Next
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Function LeggeRegistro() As Boolean
    Try
      '----------------------------------------------------------------------------------
      If oClePnsx.GestStanziamenti(DittaCorrente) = False Then
        If oCallParams Is Nothing Then
          strMsg = "Attenzione!" & vbCrLf & _
            "Non è abilitata la gestione degli stanziamenti." & vbCrLf & _
            "Esecuzione programma non possibile."
          oApp.MsgBoxErr(oApp.Tr(Me, 128776266800664529, strMsg))
        End If
        Return False
      End If
      '----------------------------------------------------------------------------------
      If Not oClePnsx.LeggiTabpecg() Then
        If oCallParams Is Nothing Then
          strMsg = "Attenzione!" & vbCrLf & _
            "Non esistono dati validi in" & vbCrLf & _
            "'Personalizzazione Contabilità'." & vbCrLf & _
            "Esecuzione programma non possibile."
          oApp.MsgBoxErr(oApp.Tr(Me, 128776266837695779, strMsg))
        End If
        Return False
      Else
        '--------------------------------------------------------------------------------
        If oClePnsx.lContoFattureDaRicevere = 0 Then
          If oCallParams Is Nothing Then
            strMsg = "Attenzione!" & vbCrLf & _
              "Non è stato indicato un conto fatture da ricevere valido in" & vbCrLf & _
              "'Personalizzazione Contabilità'." & vbCrLf & _
              "Esecuzione programma non possibile."
            oApp.MsgBoxErr(oApp.Tr(Me, 128776266883633279, strMsg))
          End If
          Return False
        End If
        '--------------------------------------------------------------------------------
        If oClePnsx.lContoFattureDaEmettere = 0 Then
          If oCallParams Is Nothing Then
            strMsg = "Attenzione!" & vbCrLf & _
              "Non è stato indicato un conto fatture da emettere valido in" & vbCrLf & _
              "'Personalizzazione Contabilità'." & vbCrLf & _
              "Esecuzione programma non possibile."
            oApp.MsgBoxErr(oApp.Tr(Me, 128776266912070779, strMsg))
          End If
          Return False
        End If
        '--------------------------------------------------------------------------------
        oMenu.ValCodiceDb(oClePnsx.lContoFattureDaRicevere.ToString, DittaCorrente, _
          "ANAGRAS", "N", "", dttTmp)
        If dttTmp.Rows.Count = 0 Then
          dttTmp.Clear()
          dttTmp.Dispose()
          If oCallParams Is Nothing Then
            strMsg = "Attenzione!" & vbCrLf & _
              "Non è stato indicato un conto fatture da ricevere valido in" & vbCrLf & _
              "'Personalizzazione Contabilità'." & vbCrLf & _
              "Esecuzione programma non possibile."
            oApp.MsgBoxErr(oApp.Tr(Me, 128776266954727029, strMsg))
          End If
          Return False
        Else
          dttTmp.Clear()
          dttTmp.Dispose()
        End If
        '--------------------------------------------------------------------------------
        oMenu.ValCodiceDb(oClePnsx.lContoFattureDaEmettere.ToString, DittaCorrente, _
          "ANAGRAS", "N", "", dttTmp)
        If dttTmp.Rows.Count = 0 Then
          dttTmp.Clear()
          dttTmp.Dispose()
          If oCallParams Is Nothing Then
            strMsg = "Attenzione!" & vbCrLf & _
              "Non è stato indicato un conto fatture da emettere valido in" & vbCrLf & _
              "'Personalizzazione Contabilità'." & vbCrLf & _
              "Esecuzione programma non possibile."
            oApp.MsgBoxErr(oApp.Tr(Me, 128776266996758279, strMsg))
          End If
          Return False
        Else
          dttTmp.Clear()
          dttTmp.Dispose()
        End If
        '--------------------------------------------------------------------------------
        If oClePnsx.nCausaleStanziamenti = 0 Then
          If oCallParams Is Nothing Then
            strMsg = "Attenzione!" & vbCrLf & _
              "Non è stato indicata una causale stanziamenti valida in" & vbCrLf & _
              "'Personalizzazione Contabilità'." & vbCrLf & _
              "Esecuzione programma non possibile."
            oApp.MsgBoxErr(oApp.Tr(Me, 128776267040664529, strMsg))
          End If
          Return False
        End If
        If oClePnsx.nCausaleStornoStanziamenti = 0 Then
          If oCallParams Is Nothing Then
            strMsg = "Attenzione!" & vbCrLf & _
              "Non è stato indicata una causale storno stanziamenti valida in" & vbCrLf & _
              "'Personalizzazione Contabilità'." & vbCrLf & _
              "Esecuzione programma non possibile."
            oApp.MsgBoxErr(oApp.Tr(Me, 128776267070977029, strMsg))
          End If
          Return False
        End If
        '--------------------------------------------------------------------------------
        oMenu.ValCodiceDb(oClePnsx.nCausaleStanziamenti.ToString, DittaCorrente, _
          "TABCAUC", "N", "", dttTmp)
        If dttTmp.Rows.Count = 0 Then
          If oCallParams Is Nothing Then
            strMsg = "Attenzione!" & vbCrLf & _
              "Non è stato indicata una causale stanziamenti valida in" & vbCrLf & _
              "'Personalizzazione Contabilità'." & vbCrLf & _
              "Esecuzione programma non possibile."
            oApp.MsgBoxErr(oApp.Tr(Me, 128776267116602029, strMsg))
          End If
          Return False
        End If
        '--------------------------------------------------------------------------------
        oMenu.ValCodiceDb(oClePnsx.nCausaleStornoStanziamenti.ToString, DittaCorrente, _
          "TABCAUC", "N", "", dttTmp)
        If dttTmp.Rows.Count = 0 Then
          If oCallParams Is Nothing Then
            strMsg = "Attenzione!" & vbCrLf & _
              "Non è stato indicata una causale storno stanziamenti valida in" & vbCrLf & _
              "'Personalizzazione Contabilità'." & vbCrLf & _
              "Esecuzione programma non possibile."
            oApp.MsgBoxErr(oApp.Tr(Me, 128776267155508279, strMsg))
          End If
          Return False
        End If
        '--------------------------------------------------------------------------------
        oClePnsx.nTipoBfResiCli = 0
        oClePnsx.nTipoBfResiForn = 0
        oMenu.ValCodiceDb("1", DittaCorrente, "TABPEVE", "N", "", dttTmp)
        If dttTmp.Rows.Count <> 0 Then
          oClePnsx.nTipoBfResiCli = NTSCInt(dttTmp.Rows(0)!tb_tpbfrescli.ToString)
          oClePnsx.nTipoBfResiForn = NTSCInt(dttTmp.Rows(0)!tb_tpbfresfor.ToString)
        End If
        dttTmp.Clear()
        dttTmp.Dispose()
        '--------------------------------------------------------------------------------
        oMenu.ValCodiceDb(DittaCorrente, DittaCorrente, "TABANAZ", "S", "", dttTmp)
        If dttTmp.Rows.Count <> 0 Then
          nEscomp = NTSCInt(dttTmp.Rows(0)!tb_escomp.ToString)
          nEscompp = NTSCInt(dttTmp.Rows(0)!tb_escompp.ToString)
          oClePnsx.strDtulliqi = dttTmp.Rows(0)!tb_dtulliq.ToString
          oClePnsx.strDtullg = dttTmp.Rows(0)!tb_dtullg.ToString
          If dttTmp.Rows(0)!tb_azprofes.ToString = "S" Then
            dttTmp.Clear()
            dttTmp.Dispose()
            If oCallParams Is Nothing Then
              strMsg = "Attenzione!" & vbCrLf & _
                "Questo programma non può essere utilizzato su una ditta di tipo 'Professionista'!"
              oApp.MsgBoxErr(oApp.Tr(Me, 128776309460545949, strMsg))
            End If
            Return False
          End If
        End If
        dttTmp.Clear()
        dttTmp.Dispose()
        '--------------------------------------------------------------------------------
        '--- Legge le opzioni di registro 
        '--- Da BSVEPNFA se gestisco le date di competenza
        '--------------------------------------------------------------------------------
        oClePnsx.bGestDatecomp = CBool(oMenu.GetSettingBus("BSVEPNFA", "Opzioni", ".", _
          "GestDatecomp", "0", " ", "0"))
        '--------------------------------------------------------------------------------
        End If
        '----------------------------------------------------------------------------------
        Return True
        '----------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
#End Region

#Region "Routines"
  Public Overridable Sub EseguiEleborazione()
    Dim bOk As Boolean = False

    Try
      '----------------------------------------------------------------------------------
      If CheckDati() = False Then Return
      '----------------------------------------------------------------------------------
      If tlbIntegr.Down Then oClePnsx.strPnint = "S" Else oClePnsx.strPnint = "N"
      '----------------------------------------------------------------------------------
      bOk = oClePnsx.EseguiElaborazione(NTSCInt(edEscomp.Text), edDatreg.Text, _
        ckStorno.Checked, NTSCInt(edEscompSt.Text), edDatregst.Text, _
        ckTiporkB.Checked, ckTiporkM.Checked, _
        ckTipoRkBResiforn.Checked, ckTipoRkMResicli.Checked, _
        NTSCInt(edAnno.Text), edDatini.Text, edDatfin.Text, _
        edSerieini.Text, edSeriefin.Text, _
        NTSCInt(edNumeroini.Text), NTSCInt(edNumerofin.Text), NTSCInt(edCodtpbf.Text), _
        ckRielab.Checked, ckAnalitica.Checked)
      '----------------------------------------------------------------------------------
      lbStatus.Text = "Pronto"
      '----------------------------------------------------------------------------------
      If oClePnsx.bDaProgramma = False Then
        If bOk = True Then
          strMsg = "Elaborazione completata regolarmente." & vbCrLf
          Select Case oClePnsx.lRkfatt
            Case 0 : strMsg = strMsg & "Nessun documento trattato/registrato."
            Case 1 : strMsg = strMsg & "Trattato/registrato un documento."
            Case Else : strMsg = strMsg & "Trattati/registrati " & oClePnsx.lRkfatt.ToString & " documenti."
          End Select
          oApp.MsgBoxInfo(oApp.Tr(Me, 128783872388988825, strMsg))
        Else
          strMsg = "Elaborazione interrotta." & vbCrLf & _
            "Nessun documento elaborato."
          oApp.MsgBoxInfo(oApp.Tr(Me, 128783883033978190, strMsg))
        End If
      End If
      '----------------------------------------------------------------------------------
      Return
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

End Class
