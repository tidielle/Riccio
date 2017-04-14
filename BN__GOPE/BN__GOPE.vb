Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__GOPE

#Region "Moduli"
  Private Moduli_P As Integer = CLN__STD.bsModAll
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
  Public oCleGope As CLE__GOPE

  Public bIs15 As Boolean = True

  Public dsGope As DataSet
  Public dsRipr As DataSet
  Public dcGope As BindingSource = New BindingSource
  Public oCallParams As CLE__CLDP

  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbApri As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbProgrammi As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbAziende As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDitte As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbLogAcc As NTSInformatica.NTSLabel
  Public WithEvents lbAzienda As NTSInformatica.NTSLabel
  Public WithEvents lbRuoloOp As NTSInformatica.NTSLabel
  Public WithEvents lbGruppoOp As NTSInformatica.NTSLabel
  Public WithEvents lbPwdOp As NTSInformatica.NTSLabel
  Public WithEvents lbNomeOp As NTSInformatica.NTSLabel
  Public WithEvents lbNomePc As NTSInformatica.NTSLabel
  Public WithEvents lbPwdSQL As NTSInformatica.NTSLabel
  Public WithEvents lbLogSQL As NTSInformatica.NTSLabel
  Public WithEvents lbPwdAc As NTSInformatica.NTSLabel
  Public WithEvents lbpwdAcc As NTSInformatica.NTSLabel
  Public WithEvents lbultacc As NTSInformatica.NTSLabel
  Public WithEvents lbDataScad As NTSInformatica.NTSLabel
  Public WithEvents lbLingua As NTSInformatica.NTSLabel
  Public WithEvents lbConfPwd As NTSInformatica.NTSLabel
  Public WithEvents lbPW As NTSInformatica.NTSLabel
  Public WithEvents ckOpAbilcamb As NTSInformatica.NTSCheckBox
  Public WithEvents ckOpAbil As NTSInformatica.NTSCheckBox
  Public WithEvents edOpNome As NTSInformatica.NTSTextBoxStr
  Public WithEvents edOpGruppo As NTSInformatica.NTSTextBoxNum
  Public WithEvents edOpPin As NTSInformatica.NTSMemoBox
  Public WithEvents edOpDataScad As NTSInformatica.NTSTextBoxData
  Public WithEvents cbOpCodling As NTSInformatica.NTSComboBox
  Public WithEvents lbOpDatulac As NTSInformatica.NTSLabel
  Public WithEvents edOpPasswd As NTSInformatica.NTSTextBoxStr
  Public WithEvents edConfermaPwd As NTSInformatica.NTSTextBoxStr
  Public WithEvents edOpRuolo As NTSInformatica.NTSTextBoxStr
  Public WithEvents edOpAzienda As NTSInformatica.NTSTextBoxStr
  Public WithEvents edOpLoginaccess As NTSInformatica.NTSTextBoxStr
  Public WithEvents edOpDefprinter As NTSInformatica.NTSTextBoxStr
  Public WithEvents edOpPasssql As NTSInformatica.NTSTextBoxStr
  Public WithEvents edOpLoginsql As NTSInformatica.NTSTextBoxStr
  Public WithEvents edOpPassaccess As NTSInformatica.NTSTextBoxStr

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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__GOPE", "BE__GOPE", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128641434317968750, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleGope = CType(oTmp, CLE__GOPE)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__GOPE", strRemoteServer, strRemotePort)
    AddHandler oCleGope.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleGope.Init(oApp, NTSScript, oMenu.oCleComm, "OPERAT", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__GOPE))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbApri = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbProgrammi = New NTSInformatica.NTSBarButtonItem
    Me.tlbAziende = New NTSInformatica.NTSBarButtonItem
    Me.tlbDitte = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbCopiaConfig = New NTSInformatica.NTSBarMenuItem
    Me.tlbCopiaMenu = New NTSInformatica.NTSBarMenuItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.lbNomeOp = New NTSInformatica.NTSLabel
    Me.lbPwdOp = New NTSInformatica.NTSLabel
    Me.lbGruppoOp = New NTSInformatica.NTSLabel
    Me.lbRuoloOp = New NTSInformatica.NTSLabel
    Me.lbAzienda = New NTSInformatica.NTSLabel
    Me.lbLogAcc = New NTSInformatica.NTSLabel
    Me.lbpwdAcc = New NTSInformatica.NTSLabel
    Me.lbPwdAc = New NTSInformatica.NTSLabel
    Me.lbLogSQL = New NTSInformatica.NTSLabel
    Me.lbPwdSQL = New NTSInformatica.NTSLabel
    Me.lbNomePc = New NTSInformatica.NTSLabel
    Me.lbPW = New NTSInformatica.NTSLabel
    Me.lbConfPwd = New NTSInformatica.NTSLabel
    Me.lbLingua = New NTSInformatica.NTSLabel
    Me.lbDataScad = New NTSInformatica.NTSLabel
    Me.lbultacc = New NTSInformatica.NTSLabel
    Me.ckOpAbil = New NTSInformatica.NTSCheckBox
    Me.ckOpAbilcamb = New NTSInformatica.NTSCheckBox
    Me.edOpNome = New NTSInformatica.NTSTextBoxStr
    Me.edOpGruppo = New NTSInformatica.NTSTextBoxNum
    Me.edOpPin = New NTSInformatica.NTSMemoBox
    Me.cbOpCodling = New NTSInformatica.NTSComboBox
    Me.edOpDataScad = New NTSInformatica.NTSTextBoxData
    Me.lbOpDatulac = New NTSInformatica.NTSLabel
    Me.edOpPasswd = New NTSInformatica.NTSTextBoxStr
    Me.edConfermaPwd = New NTSInformatica.NTSTextBoxStr
    Me.edOpRuolo = New NTSInformatica.NTSTextBoxStr
    Me.edOpAzienda = New NTSInformatica.NTSTextBoxStr
    Me.edOpLoginaccess = New NTSInformatica.NTSTextBoxStr
    Me.edOpPassaccess = New NTSInformatica.NTSTextBoxStr
    Me.edOpLoginsql = New NTSInformatica.NTSTextBoxStr
    Me.edOpPasssql = New NTSInformatica.NTSTextBoxStr
    Me.edOpDefprinter = New NTSInformatica.NTSTextBoxStr
    Me.ckOpIscrmus = New NTSInformatica.NTSCheckBox
    Me.lbOpDescont = New NTSInformatica.NTSLabel
    Me.lbOpDescont2 = New NTSInformatica.NTSLabel
    Me.edOpDescont = New NTSInformatica.NTSTextBoxStr
    Me.edOpDescont2 = New NTSInformatica.NTSTextBoxStr
    Me.ckOpNetOnly = New NTSInformatica.NTSCheckBox
    Me.pnTutto = New NTSInformatica.NTSPanel
    Me.cmdCreaRelazioni = New NTSInformatica.NTSButton
    Me.fmSocial = New NTSInformatica.NTSGroupBox
    Me.lbOpSumail = New NTSInformatica.NTSLabel
    Me.edOpSumail = New NTSInformatica.NTSTextBoxStr
    Me.lbOpSutipouser = New NTSInformatica.NTSLabel
    Me.cbOpSutipouser = New NTSInformatica.NTSComboBox
    Me.fmGoogle = New NTSInformatica.NTSGroupBox
    Me.edOpGooglePwd = New NTSInformatica.NTSTextBoxStr
    Me.lbOpGooglePwd = New NTSInformatica.NTSLabel
    Me.lbOpGoogleUser = New NTSInformatica.NTSLabel
    Me.edOpGoogleUser = New NTSInformatica.NTSTextBoxStr
    Me.ckOpSulimiti = New NTSInformatica.NTSCheckBox
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckOpAbil.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckOpAbilcamb.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOpNome.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOpGruppo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOpPin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbOpCodling.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOpDataScad.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOpPasswd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edConfermaPwd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOpRuolo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOpAzienda.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOpLoginaccess.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOpPassaccess.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOpLoginsql.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOpPasssql.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOpDefprinter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckOpIscrmus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOpDescont.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOpDescont2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckOpNetOnly.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTutto, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTutto.SuspendLayout()
    CType(Me.fmSocial, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmSocial.SuspendLayout()
    CType(Me.edOpSumail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbOpSutipouser.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmGoogle, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmGoogle.SuspendLayout()
    CType(Me.edOpGooglePwd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOpGoogleUser.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckOpSulimiti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbApri, Me.tlbSalva, Me.tlbRipristina, Me.tlbCancella, Me.tlbZoom, Me.tlbProgrammi, Me.tlbAziende, Me.tlbDitte, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbCopiaConfig, Me.tlbCopiaMenu})
    Me.NtsBarManager1.MaxItemId = 16
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApri), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbProgrammi, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAziende), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDitte), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbNuovo
    '
    Me.tlbNuovo.Caption = "Nuovo"
    Me.tlbNuovo.Glyph = CType(resources.GetObject("tlbNuovo.Glyph"), System.Drawing.Image)
    Me.tlbNuovo.GlyphPath = ""
    Me.tlbNuovo.Id = 0
    Me.tlbNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2)
    Me.tlbNuovo.Name = "tlbNuovo"
    Me.tlbNuovo.Visible = True
    '
    'tlbApri
    '
    Me.tlbApri.Caption = "Apri"
    Me.tlbApri.Glyph = CType(resources.GetObject("tlbApri.Glyph"), System.Drawing.Image)
    Me.tlbApri.GlyphPath = ""
    Me.tlbApri.Id = 1
    Me.tlbApri.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3)
    Me.tlbApri.Name = "tlbApri"
    Me.tlbApri.Visible = True
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.GlyphPath = ""
    Me.tlbSalva.Id = 2
    Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.GlyphPath = ""
    Me.tlbRipristina.Id = 3
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.GlyphPath = ""
    Me.tlbCancella.Id = 4
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.GlyphPath = ""
    Me.tlbZoom.Id = 5
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbProgrammi
    '
    Me.tlbProgrammi.Caption = "Programmi"
    Me.tlbProgrammi.Glyph = CType(resources.GetObject("tlbProgrammi.Glyph"), System.Drawing.Image)
    Me.tlbProgrammi.GlyphPath = ""
    Me.tlbProgrammi.Id = 6
    Me.tlbProgrammi.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F7))
    Me.tlbProgrammi.Name = "tlbProgrammi"
    Me.tlbProgrammi.Visible = True
    '
    'tlbAziende
    '
    Me.tlbAziende.Caption = "Aziende"
    Me.tlbAziende.Glyph = CType(resources.GetObject("tlbAziende.Glyph"), System.Drawing.Image)
    Me.tlbAziende.GlyphPath = ""
    Me.tlbAziende.Id = 7
    Me.tlbAziende.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F8))
    Me.tlbAziende.Name = "tlbAziende"
    Me.tlbAziende.Visible = True
    '
    'tlbDitte
    '
    Me.tlbDitte.Caption = "Ditte"
    Me.tlbDitte.Glyph = CType(resources.GetObject("tlbDitte.Glyph"), System.Drawing.Image)
    Me.tlbDitte.GlyphPath = ""
    Me.tlbDitte.Id = 8
    Me.tlbDitte.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F9))
    Me.tlbDitte.Name = "tlbDitte"
    Me.tlbDitte.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 13
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, False, Me.tlbCopiaConfig, False), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCopiaMenu)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbCopiaConfig
    '
    Me.tlbCopiaConfig.Caption = "Copia config. da altro &operatore"
    Me.tlbCopiaConfig.GlyphPath = ""
    Me.tlbCopiaConfig.Id = 14
    Me.tlbCopiaConfig.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O))
    Me.tlbCopiaConfig.Name = "tlbCopiaConfig"
    Me.tlbCopiaConfig.NTSIsCheckBox = False
    Me.tlbCopiaConfig.Visible = False
    '
    'tlbCopiaMenu
    '
    Me.tlbCopiaMenu.Caption = "Copia Menu 'bloccato in alto/in basso' da altro utente"
    Me.tlbCopiaMenu.GlyphPath = ""
    Me.tlbCopiaMenu.Id = 15
    Me.tlbCopiaMenu.Name = "tlbCopiaMenu"
    Me.tlbCopiaMenu.NTSIsCheckBox = False
    Me.tlbCopiaMenu.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 9
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 10
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'lbNomeOp
    '
    Me.lbNomeOp.AutoSize = True
    Me.lbNomeOp.BackColor = System.Drawing.Color.Transparent
    Me.lbNomeOp.Location = New System.Drawing.Point(13, 12)
    Me.lbNomeOp.Name = "lbNomeOp"
    Me.lbNomeOp.NTSDbField = ""
    Me.lbNomeOp.Size = New System.Drawing.Size(87, 13)
    Me.lbNomeOp.TabIndex = 4
    Me.lbNomeOp.Text = "Nome Operatore"
    Me.lbNomeOp.Tooltip = ""
    Me.lbNomeOp.UseMnemonic = False
    '
    'lbPwdOp
    '
    Me.lbPwdOp.AutoSize = True
    Me.lbPwdOp.BackColor = System.Drawing.Color.Transparent
    Me.lbPwdOp.Location = New System.Drawing.Point(15, 38)
    Me.lbPwdOp.Name = "lbPwdOp"
    Me.lbPwdOp.NTSDbField = ""
    Me.lbPwdOp.Size = New System.Drawing.Size(104, 13)
    Me.lbPwdOp.TabIndex = 5
    Me.lbPwdOp.Text = "Password operatore"
    Me.lbPwdOp.Tooltip = ""
    Me.lbPwdOp.UseMnemonic = False
    '
    'lbGruppoOp
    '
    Me.lbGruppoOp.AutoSize = True
    Me.lbGruppoOp.BackColor = System.Drawing.Color.Transparent
    Me.lbGruppoOp.Location = New System.Drawing.Point(15, 64)
    Me.lbGruppoOp.Name = "lbGruppoOp"
    Me.lbGruppoOp.NTSDbField = ""
    Me.lbGruppoOp.Size = New System.Drawing.Size(93, 13)
    Me.lbGruppoOp.TabIndex = 6
    Me.lbGruppoOp.Text = "Gruppo operatore"
    Me.lbGruppoOp.Tooltip = ""
    Me.lbGruppoOp.UseMnemonic = False
    '
    'lbRuoloOp
    '
    Me.lbRuoloOp.AutoSize = True
    Me.lbRuoloOp.BackColor = System.Drawing.Color.Transparent
    Me.lbRuoloOp.Location = New System.Drawing.Point(15, 90)
    Me.lbRuoloOp.Name = "lbRuoloOp"
    Me.lbRuoloOp.NTSDbField = ""
    Me.lbRuoloOp.Size = New System.Drawing.Size(34, 13)
    Me.lbRuoloOp.TabIndex = 7
    Me.lbRuoloOp.Text = "Ruolo"
    Me.lbRuoloOp.Tooltip = ""
    Me.lbRuoloOp.UseMnemonic = False
    '
    'lbAzienda
    '
    Me.lbAzienda.AutoSize = True
    Me.lbAzienda.BackColor = System.Drawing.Color.Transparent
    Me.lbAzienda.Location = New System.Drawing.Point(15, 116)
    Me.lbAzienda.Name = "lbAzienda"
    Me.lbAzienda.NTSDbField = ""
    Me.lbAzienda.Size = New System.Drawing.Size(86, 13)
    Me.lbAzienda.TabIndex = 8
    Me.lbAzienda.Text = "Azienda abituale"
    Me.lbAzienda.Tooltip = ""
    Me.lbAzienda.UseMnemonic = False
    '
    'lbLogAcc
    '
    Me.lbLogAcc.AutoSize = True
    Me.lbLogAcc.BackColor = System.Drawing.Color.Transparent
    Me.lbLogAcc.Location = New System.Drawing.Point(27, 250)
    Me.lbLogAcc.Name = "lbLogAcc"
    Me.lbLogAcc.NTSDbField = ""
    Me.lbLogAcc.Size = New System.Drawing.Size(68, 13)
    Me.lbLogAcc.TabIndex = 9
    Me.lbLogAcc.Text = "Login Access"
    Me.lbLogAcc.Tooltip = ""
    Me.lbLogAcc.UseMnemonic = False
    Me.lbLogAcc.Visible = False
    '
    'lbpwdAcc
    '
    Me.lbpwdAcc.AutoSize = True
    Me.lbpwdAcc.BackColor = System.Drawing.Color.Transparent
    Me.lbpwdAcc.Location = New System.Drawing.Point(12, 174)
    Me.lbpwdAcc.Name = "lbpwdAcc"
    Me.lbpwdAcc.NTSDbField = ""
    Me.lbpwdAcc.Size = New System.Drawing.Size(0, 13)
    Me.lbpwdAcc.TabIndex = 10
    Me.lbpwdAcc.Tooltip = ""
    Me.lbpwdAcc.UseMnemonic = False
    '
    'lbPwdAc
    '
    Me.lbPwdAc.AutoSize = True
    Me.lbPwdAc.BackColor = System.Drawing.Color.Transparent
    Me.lbPwdAc.Location = New System.Drawing.Point(9, 250)
    Me.lbPwdAc.Name = "lbPwdAc"
    Me.lbPwdAc.NTSDbField = ""
    Me.lbPwdAc.Size = New System.Drawing.Size(89, 13)
    Me.lbPwdAc.TabIndex = 11
    Me.lbPwdAc.Text = "Password Access"
    Me.lbPwdAc.Tooltip = ""
    Me.lbPwdAc.UseMnemonic = False
    Me.lbPwdAc.Visible = False
    '
    'lbLogSQL
    '
    Me.lbLogSQL.AutoSize = True
    Me.lbLogSQL.BackColor = System.Drawing.Color.Transparent
    Me.lbLogSQL.Location = New System.Drawing.Point(33, 243)
    Me.lbLogSQL.Name = "lbLogSQL"
    Me.lbLogSQL.NTSDbField = ""
    Me.lbLogSQL.Size = New System.Drawing.Size(89, 13)
    Me.lbLogSQL.TabIndex = 12
    Me.lbLogSQL.Text = "Login SQL Server"
    Me.lbLogSQL.Tooltip = ""
    Me.lbLogSQL.UseMnemonic = False
    Me.lbLogSQL.Visible = False
    '
    'lbPwdSQL
    '
    Me.lbPwdSQL.AutoSize = True
    Me.lbPwdSQL.BackColor = System.Drawing.Color.Transparent
    Me.lbPwdSQL.Location = New System.Drawing.Point(36, 246)
    Me.lbPwdSQL.Name = "lbPwdSQL"
    Me.lbPwdSQL.NTSDbField = ""
    Me.lbPwdSQL.Size = New System.Drawing.Size(110, 13)
    Me.lbPwdSQL.TabIndex = 13
    Me.lbPwdSQL.Text = "Password SQL Server"
    Me.lbPwdSQL.Tooltip = ""
    Me.lbPwdSQL.UseMnemonic = False
    Me.lbPwdSQL.Visible = False
    '
    'lbNomePc
    '
    Me.lbNomePc.AutoSize = True
    Me.lbNomePc.BackColor = System.Drawing.Color.Transparent
    Me.lbNomePc.Location = New System.Drawing.Point(15, 146)
    Me.lbNomePc.Name = "lbNomePc"
    Me.lbNomePc.NTSDbField = ""
    Me.lbNomePc.Size = New System.Drawing.Size(91, 13)
    Me.lbNomePc.TabIndex = 14
    Me.lbNomePc.Text = "Nome PC abituale"
    Me.lbNomePc.Tooltip = ""
    Me.lbNomePc.UseMnemonic = False
    '
    'lbPW
    '
    Me.lbPW.AutoSize = True
    Me.lbPW.BackColor = System.Drawing.Color.Transparent
    Me.lbPW.Location = New System.Drawing.Point(15, 199)
    Me.lbPW.Name = "lbPW"
    Me.lbPW.NTSDbField = ""
    Me.lbPW.Size = New System.Drawing.Size(45, 13)
    Me.lbPW.TabIndex = 15
    Me.lbPW.Text = "Pin / WI"
    Me.lbPW.Tooltip = ""
    Me.lbPW.UseMnemonic = False
    '
    'lbConfPwd
    '
    Me.lbConfPwd.AutoSize = True
    Me.lbConfPwd.BackColor = System.Drawing.Color.Transparent
    Me.lbConfPwd.Location = New System.Drawing.Point(310, 38)
    Me.lbConfPwd.Name = "lbConfPwd"
    Me.lbConfPwd.NTSDbField = ""
    Me.lbConfPwd.Size = New System.Drawing.Size(136, 13)
    Me.lbConfPwd.TabIndex = 16
    Me.lbConfPwd.Text = "Conferma nuova password"
    Me.lbConfPwd.Tooltip = ""
    Me.lbConfPwd.UseMnemonic = False
    '
    'lbLingua
    '
    Me.lbLingua.AutoSize = True
    Me.lbLingua.BackColor = System.Drawing.Color.Transparent
    Me.lbLingua.Location = New System.Drawing.Point(15, 174)
    Me.lbLingua.Name = "lbLingua"
    Me.lbLingua.NTSDbField = ""
    Me.lbLingua.Size = New System.Drawing.Size(38, 13)
    Me.lbLingua.TabIndex = 17
    Me.lbLingua.Text = "Lingua"
    Me.lbLingua.Tooltip = ""
    Me.lbLingua.UseMnemonic = False
    '
    'lbDataScad
    '
    Me.lbDataScad.AutoSize = True
    Me.lbDataScad.BackColor = System.Drawing.Color.Transparent
    Me.lbDataScad.Location = New System.Drawing.Point(382, 146)
    Me.lbDataScad.Name = "lbDataScad"
    Me.lbDataScad.NTSDbField = ""
    Me.lbDataScad.Size = New System.Drawing.Size(138, 13)
    Me.lbDataScad.TabIndex = 18
    Me.lbDataScad.Text = "Data di scadenza password"
    Me.lbDataScad.Tooltip = ""
    Me.lbDataScad.UseMnemonic = False
    '
    'lbultacc
    '
    Me.lbultacc.AutoSize = True
    Me.lbultacc.BackColor = System.Drawing.Color.Transparent
    Me.lbultacc.Location = New System.Drawing.Point(382, 171)
    Me.lbultacc.Name = "lbultacc"
    Me.lbultacc.NTSDbField = ""
    Me.lbultacc.Size = New System.Drawing.Size(102, 13)
    Me.lbultacc.TabIndex = 19
    Me.lbultacc.Text = "Data ultimo accesso"
    Me.lbultacc.Tooltip = ""
    Me.lbultacc.UseMnemonic = False
    '
    'ckOpAbil
    '
    Me.ckOpAbil.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckOpAbil.Location = New System.Drawing.Point(385, 88)
    Me.ckOpAbil.Name = "ckOpAbil"
    Me.ckOpAbil.NTSCheckValue = "S"
    Me.ckOpAbil.NTSUnCheckValue = "N"
    Me.ckOpAbil.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckOpAbil.Properties.Appearance.Options.UseBackColor = True
    Me.ckOpAbil.Properties.AutoHeight = False
    Me.ckOpAbil.Properties.Caption = "Operatore abilitato"
    Me.ckOpAbil.Size = New System.Drawing.Size(125, 19)
    Me.ckOpAbil.TabIndex = 20
    '
    'ckOpAbilcamb
    '
    Me.ckOpAbilcamb.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckOpAbilcamb.Location = New System.Drawing.Point(385, 114)
    Me.ckOpAbilcamb.Name = "ckOpAbilcamb"
    Me.ckOpAbilcamb.NTSCheckValue = "S"
    Me.ckOpAbilcamb.NTSUnCheckValue = "N"
    Me.ckOpAbilcamb.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckOpAbilcamb.Properties.Appearance.Options.UseBackColor = True
    Me.ckOpAbilcamb.Properties.AutoHeight = False
    Me.ckOpAbilcamb.Properties.Caption = "Consenti modifica password"
    Me.ckOpAbilcamb.Size = New System.Drawing.Size(162, 19)
    Me.ckOpAbilcamb.TabIndex = 21
    '
    'edOpNome
    '
    Me.edOpNome.Cursor = System.Windows.Forms.Cursors.Default
    Me.edOpNome.EditValue = ""
    Me.edOpNome.Location = New System.Drawing.Point(144, 9)
    Me.edOpNome.Name = "edOpNome"
    Me.edOpNome.NTSDbField = ""
    Me.edOpNome.NTSForzaVisZoom = False
    Me.edOpNome.NTSOldValue = ""
    Me.edOpNome.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOpNome.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOpNome.Properties.AutoHeight = False
    Me.edOpNome.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOpNome.Properties.MaxLength = 65536
    Me.edOpNome.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOpNome.Size = New System.Drawing.Size(230, 20)
    Me.edOpNome.TabIndex = 22
    '
    'edOpGruppo
    '
    Me.edOpGruppo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edOpGruppo.EditValue = "0"
    Me.edOpGruppo.Location = New System.Drawing.Point(144, 61)
    Me.edOpGruppo.Name = "edOpGruppo"
    Me.edOpGruppo.NTSDbField = ""
    Me.edOpGruppo.NTSFormat = "0"
    Me.edOpGruppo.NTSForzaVisZoom = False
    Me.edOpGruppo.NTSOldValue = ""
    Me.edOpGruppo.Properties.Appearance.Options.UseTextOptions = True
    Me.edOpGruppo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edOpGruppo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOpGruppo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOpGruppo.Properties.AutoHeight = False
    Me.edOpGruppo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOpGruppo.Properties.MaxLength = 65536
    Me.edOpGruppo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOpGruppo.Size = New System.Drawing.Size(61, 20)
    Me.edOpGruppo.TabIndex = 25
    '
    'edOpPin
    '
    Me.edOpPin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edOpPin.Location = New System.Drawing.Point(144, 197)
    Me.edOpPin.Name = "edOpPin"
    Me.edOpPin.NTSDbField = ""
    Me.edOpPin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOpPin.Size = New System.Drawing.Size(474, 91)
    Me.edOpPin.TabIndex = 33
    '
    'cbOpCodling
    '
    Me.cbOpCodling.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbOpCodling.DataSource = Nothing
    Me.cbOpCodling.DisplayMember = ""
    Me.cbOpCodling.EditValue = ""
    Me.cbOpCodling.Location = New System.Drawing.Point(144, 171)
    Me.cbOpCodling.Name = "cbOpCodling"
    Me.cbOpCodling.NTSDbField = ""
    Me.cbOpCodling.Properties.AutoHeight = False
    Me.cbOpCodling.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbOpCodling.Properties.DropDownRows = 30
    Me.cbOpCodling.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbOpCodling.SelectedValue = ""
    Me.cbOpCodling.Size = New System.Drawing.Size(230, 20)
    Me.cbOpCodling.TabIndex = 35
    Me.cbOpCodling.ValueMember = ""
    '
    'edOpDataScad
    '
    Me.edOpDataScad.Cursor = System.Windows.Forms.Cursors.Default
    Me.edOpDataScad.EditValue = "01/01/1900"
    Me.edOpDataScad.Location = New System.Drawing.Point(535, 143)
    Me.edOpDataScad.Name = "edOpDataScad"
    Me.edOpDataScad.NTSDbField = ""
    Me.edOpDataScad.NTSForzaVisZoom = False
    Me.edOpDataScad.NTSOldValue = ""
    Me.edOpDataScad.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOpDataScad.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOpDataScad.Properties.AutoHeight = False
    Me.edOpDataScad.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOpDataScad.Properties.MaxLength = 65536
    Me.edOpDataScad.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOpDataScad.Size = New System.Drawing.Size(83, 20)
    Me.edOpDataScad.TabIndex = 36
    '
    'lbOpDatulac
    '
    Me.lbOpDatulac.BackColor = System.Drawing.Color.Transparent
    Me.lbOpDatulac.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbOpDatulac.Location = New System.Drawing.Point(535, 170)
    Me.lbOpDatulac.Name = "lbOpDatulac"
    Me.lbOpDatulac.NTSDbField = ""
    Me.lbOpDatulac.Size = New System.Drawing.Size(83, 20)
    Me.lbOpDatulac.TabIndex = 37
    Me.lbOpDatulac.Tooltip = ""
    Me.lbOpDatulac.UseMnemonic = False
    '
    'edOpPasswd
    '
    Me.edOpPasswd.Cursor = System.Windows.Forms.Cursors.Default
    Me.edOpPasswd.EditValue = ""
    Me.edOpPasswd.Location = New System.Drawing.Point(144, 35)
    Me.edOpPasswd.Name = "edOpPasswd"
    Me.edOpPasswd.NTSDbField = ""
    Me.edOpPasswd.NTSForzaVisZoom = False
    Me.edOpPasswd.NTSOldValue = ""
    Me.edOpPasswd.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOpPasswd.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOpPasswd.Properties.AutoHeight = False
    Me.edOpPasswd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOpPasswd.Properties.MaxLength = 65536
    Me.edOpPasswd.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
    Me.edOpPasswd.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOpPasswd.Size = New System.Drawing.Size(160, 20)
    Me.edOpPasswd.TabIndex = 38
    '
    'edConfermaPwd
    '
    Me.edConfermaPwd.Cursor = System.Windows.Forms.Cursors.Default
    Me.edConfermaPwd.EditValue = ""
    Me.edConfermaPwd.Location = New System.Drawing.Point(458, 35)
    Me.edConfermaPwd.Name = "edConfermaPwd"
    Me.edConfermaPwd.NTSDbField = ""
    Me.edConfermaPwd.NTSForzaVisZoom = False
    Me.edConfermaPwd.NTSOldValue = ""
    Me.edConfermaPwd.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edConfermaPwd.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edConfermaPwd.Properties.AutoHeight = False
    Me.edConfermaPwd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edConfermaPwd.Properties.MaxLength = 65536
    Me.edConfermaPwd.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
    Me.edConfermaPwd.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edConfermaPwd.Size = New System.Drawing.Size(160, 20)
    Me.edConfermaPwd.TabIndex = 39
    '
    'edOpRuolo
    '
    Me.edOpRuolo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edOpRuolo.EditValue = ""
    Me.edOpRuolo.Location = New System.Drawing.Point(144, 87)
    Me.edOpRuolo.Name = "edOpRuolo"
    Me.edOpRuolo.NTSDbField = ""
    Me.edOpRuolo.NTSForzaVisZoom = False
    Me.edOpRuolo.NTSOldValue = ""
    Me.edOpRuolo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOpRuolo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOpRuolo.Properties.AutoHeight = False
    Me.edOpRuolo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOpRuolo.Properties.MaxLength = 65536
    Me.edOpRuolo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOpRuolo.Size = New System.Drawing.Size(230, 20)
    Me.edOpRuolo.TabIndex = 40
    '
    'edOpAzienda
    '
    Me.edOpAzienda.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edOpAzienda.EditValue = ""
    Me.edOpAzienda.Location = New System.Drawing.Point(144, 113)
    Me.edOpAzienda.Name = "edOpAzienda"
    Me.edOpAzienda.NTSDbField = ""
    Me.edOpAzienda.NTSForzaVisZoom = False
    Me.edOpAzienda.NTSOldValue = ""
    Me.edOpAzienda.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOpAzienda.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOpAzienda.Properties.AutoHeight = False
    Me.edOpAzienda.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOpAzienda.Properties.MaxLength = 65536
    Me.edOpAzienda.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOpAzienda.Size = New System.Drawing.Size(230, 20)
    Me.edOpAzienda.TabIndex = 41
    '
    'edOpLoginaccess
    '
    Me.edOpLoginaccess.Cursor = System.Windows.Forms.Cursors.Default
    Me.edOpLoginaccess.EditValue = ""
    Me.edOpLoginaccess.Location = New System.Drawing.Point(101, 247)
    Me.edOpLoginaccess.Name = "edOpLoginaccess"
    Me.edOpLoginaccess.NTSDbField = ""
    Me.edOpLoginaccess.NTSForzaVisZoom = False
    Me.edOpLoginaccess.NTSOldValue = ""
    Me.edOpLoginaccess.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOpLoginaccess.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOpLoginaccess.Properties.AutoHeight = False
    Me.edOpLoginaccess.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOpLoginaccess.Properties.MaxLength = 65536
    Me.edOpLoginaccess.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOpLoginaccess.Size = New System.Drawing.Size(37, 20)
    Me.edOpLoginaccess.TabIndex = 42
    Me.edOpLoginaccess.Visible = False
    '
    'edOpPassaccess
    '
    Me.edOpPassaccess.Cursor = System.Windows.Forms.Cursors.Default
    Me.edOpPassaccess.EditValue = ""
    Me.edOpPassaccess.Location = New System.Drawing.Point(104, 247)
    Me.edOpPassaccess.Name = "edOpPassaccess"
    Me.edOpPassaccess.NTSDbField = ""
    Me.edOpPassaccess.NTSForzaVisZoom = False
    Me.edOpPassaccess.NTSOldValue = ""
    Me.edOpPassaccess.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOpPassaccess.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOpPassaccess.Properties.AutoHeight = False
    Me.edOpPassaccess.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOpPassaccess.Properties.MaxLength = 65536
    Me.edOpPassaccess.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
    Me.edOpPassaccess.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOpPassaccess.Size = New System.Drawing.Size(37, 20)
    Me.edOpPassaccess.TabIndex = 43
    Me.edOpPassaccess.Visible = False
    '
    'edOpLoginsql
    '
    Me.edOpLoginsql.Cursor = System.Windows.Forms.Cursors.Default
    Me.edOpLoginsql.EditValue = ""
    Me.edOpLoginsql.Location = New System.Drawing.Point(128, 240)
    Me.edOpLoginsql.Name = "edOpLoginsql"
    Me.edOpLoginsql.NTSDbField = ""
    Me.edOpLoginsql.NTSForzaVisZoom = False
    Me.edOpLoginsql.NTSOldValue = ""
    Me.edOpLoginsql.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOpLoginsql.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOpLoginsql.Properties.AutoHeight = False
    Me.edOpLoginsql.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOpLoginsql.Properties.MaxLength = 65536
    Me.edOpLoginsql.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOpLoginsql.Size = New System.Drawing.Size(37, 20)
    Me.edOpLoginsql.TabIndex = 44
    Me.edOpLoginsql.Visible = False
    '
    'edOpPasssql
    '
    Me.edOpPasssql.Cursor = System.Windows.Forms.Cursors.Default
    Me.edOpPasssql.EditValue = ""
    Me.edOpPasssql.Location = New System.Drawing.Point(135, 243)
    Me.edOpPasssql.Name = "edOpPasssql"
    Me.edOpPasssql.NTSDbField = ""
    Me.edOpPasssql.NTSForzaVisZoom = False
    Me.edOpPasssql.NTSOldValue = ""
    Me.edOpPasssql.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOpPasssql.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOpPasssql.Properties.AutoHeight = False
    Me.edOpPasssql.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOpPasssql.Properties.MaxLength = 65536
    Me.edOpPasssql.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
    Me.edOpPasssql.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOpPasssql.Size = New System.Drawing.Size(37, 20)
    Me.edOpPasssql.TabIndex = 45
    Me.edOpPasssql.Visible = False
    '
    'edOpDefprinter
    '
    Me.edOpDefprinter.Cursor = System.Windows.Forms.Cursors.Default
    Me.edOpDefprinter.EditValue = ""
    Me.edOpDefprinter.Location = New System.Drawing.Point(144, 143)
    Me.edOpDefprinter.Name = "edOpDefprinter"
    Me.edOpDefprinter.NTSDbField = ""
    Me.edOpDefprinter.NTSForzaVisZoom = False
    Me.edOpDefprinter.NTSOldValue = ""
    Me.edOpDefprinter.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOpDefprinter.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOpDefprinter.Properties.AutoHeight = False
    Me.edOpDefprinter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOpDefprinter.Properties.MaxLength = 65536
    Me.edOpDefprinter.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOpDefprinter.Size = New System.Drawing.Size(230, 20)
    Me.edOpDefprinter.TabIndex = 46
    '
    'ckOpIscrmus
    '
    Me.ckOpIscrmus.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckOpIscrmus.Location = New System.Drawing.Point(18, 324)
    Me.ckOpIscrmus.Name = "ckOpIscrmus"
    Me.ckOpIscrmus.NTSCheckValue = "S"
    Me.ckOpIscrmus.NTSUnCheckValue = "N"
    Me.ckOpIscrmus.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckOpIscrmus.Properties.Appearance.Options.UseBackColor = True
    Me.ckOpIscrmus.Properties.AutoHeight = False
    Me.ckOpIscrmus.Properties.Caption = "Utente CRM"
    Me.ckOpIscrmus.Size = New System.Drawing.Size(84, 19)
    Me.ckOpIscrmus.TabIndex = 21
    '
    'lbOpDescont
    '
    Me.lbOpDescont.AutoSize = True
    Me.lbOpDescont.BackColor = System.Drawing.Color.Transparent
    Me.lbOpDescont.Location = New System.Drawing.Point(15, 300)
    Me.lbOpDescont.Name = "lbOpDescont"
    Me.lbOpDescont.NTSDbField = ""
    Me.lbOpDescont.Size = New System.Drawing.Size(59, 13)
    Me.lbOpDescont.TabIndex = 650
    Me.lbOpDescont.Text = "Cognome /"
    Me.lbOpDescont.Tooltip = ""
    Me.lbOpDescont.UseMnemonic = False
    '
    'lbOpDescont2
    '
    Me.lbOpDescont2.AutoSize = True
    Me.lbOpDescont2.BackColor = System.Drawing.Color.Transparent
    Me.lbOpDescont2.Location = New System.Drawing.Point(74, 300)
    Me.lbOpDescont2.Name = "lbOpDescont2"
    Me.lbOpDescont2.NTSDbField = ""
    Me.lbOpDescont2.Size = New System.Drawing.Size(34, 13)
    Me.lbOpDescont2.TabIndex = 651
    Me.lbOpDescont2.Text = "Nome"
    Me.lbOpDescont2.Tooltip = ""
    Me.lbOpDescont2.UseMnemonic = False
    '
    'edOpDescont
    '
    Me.edOpDescont.Cursor = System.Windows.Forms.Cursors.Default
    Me.edOpDescont.EditValue = ""
    Me.edOpDescont.Location = New System.Drawing.Point(144, 297)
    Me.edOpDescont.Name = "edOpDescont"
    Me.edOpDescont.NTSDbField = ""
    Me.edOpDescont.NTSForzaVisZoom = False
    Me.edOpDescont.NTSOldValue = ""
    Me.edOpDescont.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOpDescont.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOpDescont.Properties.AutoHeight = False
    Me.edOpDescont.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOpDescont.Properties.MaxLength = 65536
    Me.edOpDescont.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOpDescont.Size = New System.Drawing.Size(230, 20)
    Me.edOpDescont.TabIndex = 652
    '
    'edOpDescont2
    '
    Me.edOpDescont2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edOpDescont2.EditValue = ""
    Me.edOpDescont2.Location = New System.Drawing.Point(385, 297)
    Me.edOpDescont2.Name = "edOpDescont2"
    Me.edOpDescont2.NTSDbField = ""
    Me.edOpDescont2.NTSForzaVisZoom = False
    Me.edOpDescont2.NTSOldValue = ""
    Me.edOpDescont2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOpDescont2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOpDescont2.Properties.AutoHeight = False
    Me.edOpDescont2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOpDescont2.Properties.MaxLength = 65536
    Me.edOpDescont2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOpDescont2.Size = New System.Drawing.Size(233, 20)
    Me.edOpDescont2.TabIndex = 653
    '
    'ckOpNetOnly
    '
    Me.ckOpNetOnly.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckOpNetOnly.Location = New System.Drawing.Point(70, 241)
    Me.ckOpNetOnly.Name = "ckOpNetOnly"
    Me.ckOpNetOnly.NTSCheckValue = "S"
    Me.ckOpNetOnly.NTSUnCheckValue = "N"
    Me.ckOpNetOnly.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckOpNetOnly.Properties.Appearance.Options.UseBackColor = True
    Me.ckOpNetOnly.Properties.AutoHeight = False
    Me.ckOpNetOnly.Properties.Caption = "Non avviare framework VB6"
    Me.ckOpNetOnly.Size = New System.Drawing.Size(68, 19)
    Me.ckOpNetOnly.TabIndex = 655
    Me.ckOpNetOnly.Visible = False
    '
    'pnTutto
    '
    Me.pnTutto.AllowDrop = True
    Me.pnTutto.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTutto.Appearance.Options.UseBackColor = True
    Me.pnTutto.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTutto.Controls.Add(Me.ckOpSulimiti)
    Me.pnTutto.Controls.Add(Me.cmdCreaRelazioni)
    Me.pnTutto.Controls.Add(Me.fmSocial)
    Me.pnTutto.Controls.Add(Me.ckOpIscrmus)
    Me.pnTutto.Controls.Add(Me.fmGoogle)
    Me.pnTutto.Controls.Add(Me.edOpNome)
    Me.pnTutto.Controls.Add(Me.lbNomeOp)
    Me.pnTutto.Controls.Add(Me.lbPwdOp)
    Me.pnTutto.Controls.Add(Me.edOpDescont2)
    Me.pnTutto.Controls.Add(Me.lbGruppoOp)
    Me.pnTutto.Controls.Add(Me.edOpDescont)
    Me.pnTutto.Controls.Add(Me.lbRuoloOp)
    Me.pnTutto.Controls.Add(Me.lbOpDescont2)
    Me.pnTutto.Controls.Add(Me.lbAzienda)
    Me.pnTutto.Controls.Add(Me.lbOpDescont)
    Me.pnTutto.Controls.Add(Me.edOpDefprinter)
    Me.pnTutto.Controls.Add(Me.lbNomePc)
    Me.pnTutto.Controls.Add(Me.lbPW)
    Me.pnTutto.Controls.Add(Me.edOpAzienda)
    Me.pnTutto.Controls.Add(Me.lbConfPwd)
    Me.pnTutto.Controls.Add(Me.edOpRuolo)
    Me.pnTutto.Controls.Add(Me.lbLingua)
    Me.pnTutto.Controls.Add(Me.edConfermaPwd)
    Me.pnTutto.Controls.Add(Me.lbDataScad)
    Me.pnTutto.Controls.Add(Me.edOpPasswd)
    Me.pnTutto.Controls.Add(Me.lbultacc)
    Me.pnTutto.Controls.Add(Me.lbOpDatulac)
    Me.pnTutto.Controls.Add(Me.ckOpAbil)
    Me.pnTutto.Controls.Add(Me.edOpDataScad)
    Me.pnTutto.Controls.Add(Me.ckOpAbilcamb)
    Me.pnTutto.Controls.Add(Me.cbOpCodling)
    Me.pnTutto.Controls.Add(Me.edOpGruppo)
    Me.pnTutto.Controls.Add(Me.edOpPin)
    Me.pnTutto.Controls.Add(Me.ckOpNetOnly)
    Me.pnTutto.Controls.Add(Me.lbLogAcc)
    Me.pnTutto.Controls.Add(Me.lbPwdAc)
    Me.pnTutto.Controls.Add(Me.edOpPasssql)
    Me.pnTutto.Controls.Add(Me.lbLogSQL)
    Me.pnTutto.Controls.Add(Me.edOpLoginsql)
    Me.pnTutto.Controls.Add(Me.lbPwdSQL)
    Me.pnTutto.Controls.Add(Me.edOpPassaccess)
    Me.pnTutto.Controls.Add(Me.edOpLoginaccess)
    Me.pnTutto.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTutto.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTutto.Location = New System.Drawing.Point(0, 30)
    Me.pnTutto.Name = "pnTutto"
    Me.pnTutto.NTSActiveTrasparency = True
    Me.pnTutto.Size = New System.Drawing.Size(632, 432)
    Me.pnTutto.TabIndex = 656
    '
    'cmdCreaRelazioni
    '
    Me.cmdCreaRelazioni.ImagePath = ""
    Me.cmdCreaRelazioni.ImageText = ""
    Me.cmdCreaRelazioni.Location = New System.Drawing.Point(144, 320)
    Me.cmdCreaRelazioni.Name = "cmdCreaRelazioni"
    Me.cmdCreaRelazioni.NTSContextMenu = Nothing
    Me.cmdCreaRelazioni.Size = New System.Drawing.Size(176, 23)
    Me.cmdCreaRelazioni.TabIndex = 660
    Me.cmdCreaRelazioni.Text = "Crea relazioni con i Leads guest"
    '
    'fmSocial
    '
    Me.fmSocial.AllowDrop = True
    Me.fmSocial.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmSocial.Appearance.Options.UseBackColor = True
    Me.fmSocial.Controls.Add(Me.lbOpSumail)
    Me.fmSocial.Controls.Add(Me.edOpSumail)
    Me.fmSocial.Controls.Add(Me.lbOpSutipouser)
    Me.fmSocial.Controls.Add(Me.cbOpSutipouser)
    Me.fmSocial.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmSocial.Location = New System.Drawing.Point(16, 349)
    Me.fmSocial.Name = "fmSocial"
    Me.fmSocial.Size = New System.Drawing.Size(282, 76)
    Me.fmSocial.TabIndex = 659
    Me.fmSocial.Text = "Utente social"
    '
    'lbOpSumail
    '
    Me.lbOpSumail.AutoSize = True
    Me.lbOpSumail.BackColor = System.Drawing.Color.Transparent
    Me.lbOpSumail.Location = New System.Drawing.Point(5, 52)
    Me.lbOpSumail.Name = "lbOpSumail"
    Me.lbOpSumail.NTSDbField = ""
    Me.lbOpSumail.Size = New System.Drawing.Size(35, 13)
    Me.lbOpSumail.TabIndex = 660
    Me.lbOpSumail.Text = "E-mail"
    Me.lbOpSumail.Tooltip = ""
    Me.lbOpSumail.UseMnemonic = False
    '
    'edOpSumail
    '
    Me.edOpSumail.Cursor = System.Windows.Forms.Cursors.Default
    Me.edOpSumail.EditValue = ""
    Me.edOpSumail.Location = New System.Drawing.Point(110, 49)
    Me.edOpSumail.Name = "edOpSumail"
    Me.edOpSumail.NTSDbField = ""
    Me.edOpSumail.NTSForzaVisZoom = False
    Me.edOpSumail.NTSOldValue = ""
    Me.edOpSumail.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOpSumail.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOpSumail.Properties.AutoHeight = False
    Me.edOpSumail.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOpSumail.Properties.MaxLength = 65536
    Me.edOpSumail.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOpSumail.Size = New System.Drawing.Size(166, 20)
    Me.edOpSumail.TabIndex = 659
    '
    'lbOpSutipouser
    '
    Me.lbOpSutipouser.AutoSize = True
    Me.lbOpSutipouser.BackColor = System.Drawing.Color.Transparent
    Me.lbOpSutipouser.Location = New System.Drawing.Point(5, 26)
    Me.lbOpSutipouser.Name = "lbOpSutipouser"
    Me.lbOpSutipouser.NTSDbField = ""
    Me.lbOpSutipouser.Size = New System.Drawing.Size(95, 13)
    Me.lbOpSutipouser.TabIndex = 657
    Me.lbOpSutipouser.Text = "Abilità menù Social"
    Me.lbOpSutipouser.Tooltip = ""
    Me.lbOpSutipouser.UseMnemonic = False
    '
    'cbOpSutipouser
    '
    Me.cbOpSutipouser.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbOpSutipouser.DataSource = Nothing
    Me.cbOpSutipouser.DisplayMember = ""
    Me.cbOpSutipouser.EditValue = ""
    Me.cbOpSutipouser.Location = New System.Drawing.Point(110, 23)
    Me.cbOpSutipouser.Name = "cbOpSutipouser"
    Me.cbOpSutipouser.NTSDbField = ""
    Me.cbOpSutipouser.Properties.AutoHeight = False
    Me.cbOpSutipouser.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbOpSutipouser.Properties.DropDownRows = 30
    Me.cbOpSutipouser.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbOpSutipouser.SelectedValue = ""
    Me.cbOpSutipouser.Size = New System.Drawing.Size(166, 20)
    Me.cbOpSutipouser.TabIndex = 658
    Me.cbOpSutipouser.ValueMember = ""
    '
    'fmGoogle
    '
    Me.fmGoogle.AllowDrop = True
    Me.fmGoogle.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmGoogle.Appearance.Options.UseBackColor = True
    Me.fmGoogle.Controls.Add(Me.edOpGooglePwd)
    Me.fmGoogle.Controls.Add(Me.lbOpGooglePwd)
    Me.fmGoogle.Controls.Add(Me.lbOpGoogleUser)
    Me.fmGoogle.Controls.Add(Me.edOpGoogleUser)
    Me.fmGoogle.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmGoogle.Location = New System.Drawing.Point(299, 349)
    Me.fmGoogle.Name = "fmGoogle"
    Me.fmGoogle.Size = New System.Drawing.Size(328, 76)
    Me.fmGoogle.TabIndex = 656
    Me.fmGoogle.Text = "Sincronizzazione con Google"
    Me.fmGoogle.Visible = False
    '
    'edOpGooglePwd
    '
    Me.edOpGooglePwd.Cursor = System.Windows.Forms.Cursors.Default
    Me.edOpGooglePwd.EditValue = ""
    Me.edOpGooglePwd.Location = New System.Drawing.Point(93, 49)
    Me.edOpGooglePwd.Name = "edOpGooglePwd"
    Me.edOpGooglePwd.NTSDbField = ""
    Me.edOpGooglePwd.NTSForzaVisZoom = False
    Me.edOpGooglePwd.NTSOldValue = ""
    Me.edOpGooglePwd.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOpGooglePwd.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOpGooglePwd.Properties.AutoHeight = False
    Me.edOpGooglePwd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOpGooglePwd.Properties.MaxLength = 65536
    Me.edOpGooglePwd.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
    Me.edOpGooglePwd.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOpGooglePwd.Size = New System.Drawing.Size(230, 20)
    Me.edOpGooglePwd.TabIndex = 658
    '
    'lbOpGooglePwd
    '
    Me.lbOpGooglePwd.AutoSize = True
    Me.lbOpGooglePwd.BackColor = System.Drawing.Color.Transparent
    Me.lbOpGooglePwd.Location = New System.Drawing.Point(5, 52)
    Me.lbOpGooglePwd.Name = "lbOpGooglePwd"
    Me.lbOpGooglePwd.NTSDbField = ""
    Me.lbOpGooglePwd.Size = New System.Drawing.Size(81, 13)
    Me.lbOpGooglePwd.TabIndex = 657
    Me.lbOpGooglePwd.Text = "Password GMail"
    Me.lbOpGooglePwd.Tooltip = ""
    Me.lbOpGooglePwd.UseMnemonic = False
    '
    'lbOpGoogleUser
    '
    Me.lbOpGoogleUser.AutoSize = True
    Me.lbOpGoogleUser.BackColor = System.Drawing.Color.Transparent
    Me.lbOpGoogleUser.Location = New System.Drawing.Point(5, 26)
    Me.lbOpGoogleUser.Name = "lbOpGoogleUser"
    Me.lbOpGoogleUser.NTSDbField = ""
    Me.lbOpGoogleUser.Size = New System.Drawing.Size(74, 13)
    Me.lbOpGoogleUser.TabIndex = 656
    Me.lbOpGoogleUser.Text = "Account GMail"
    Me.lbOpGoogleUser.Tooltip = ""
    Me.lbOpGoogleUser.UseMnemonic = False
    '
    'edOpGoogleUser
    '
    Me.edOpGoogleUser.Cursor = System.Windows.Forms.Cursors.Default
    Me.edOpGoogleUser.EditValue = ""
    Me.edOpGoogleUser.Location = New System.Drawing.Point(93, 23)
    Me.edOpGoogleUser.Name = "edOpGoogleUser"
    Me.edOpGoogleUser.NTSDbField = ""
    Me.edOpGoogleUser.NTSForzaVisZoom = False
    Me.edOpGoogleUser.NTSOldValue = ""
    Me.edOpGoogleUser.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOpGoogleUser.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOpGoogleUser.Properties.AutoHeight = False
    Me.edOpGoogleUser.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOpGoogleUser.Properties.MaxLength = 65536
    Me.edOpGoogleUser.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOpGoogleUser.Size = New System.Drawing.Size(230, 20)
    Me.edOpGoogleUser.TabIndex = 654
    '
    'ckOpSulimiti
    '
    Me.ckOpSulimiti.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckOpSulimiti.Location = New System.Drawing.Point(385, 324)
    Me.ckOpSulimiti.Name = "ckOpSulimiti"
    Me.ckOpSulimiti.NTSCheckValue = "S"
    Me.ckOpSulimiti.NTSUnCheckValue = "N"
    Me.ckOpSulimiti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckOpSulimiti.Properties.Appearance.Options.UseBackColor = True
    Me.ckOpSulimiti.Properties.AutoHeight = False
    Me.ckOpSulimiti.Properties.Caption = "Disabilita accesso SBC"
    Me.ckOpSulimiti.Size = New System.Drawing.Size(150, 19)
    Me.ckOpSulimiti.TabIndex = 661
    '
    'FRM__GOPE
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(632, 462)
    Me.Controls.Add(Me.pnTutto)
    Me.Controls.Add(Me.lbpwdAcc)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRM__GOPE"
    Me.Text = "GESTIONE OPERATORI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckOpAbil.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckOpAbilcamb.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOpNome.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOpGruppo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOpPin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbOpCodling.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOpDataScad.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOpPasswd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edConfermaPwd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOpRuolo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOpAzienda.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOpLoginaccess.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOpPassaccess.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOpLoginsql.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOpPasssql.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOpDefprinter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckOpIscrmus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOpDescont.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOpDescont2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckOpNetOnly.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTutto, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTutto.ResumeLayout(False)
    Me.pnTutto.PerformLayout()
    CType(Me.fmSocial, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmSocial.ResumeLayout(False)
    Me.fmSocial.PerformLayout()
    CType(Me.edOpSumail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbOpSutipouser.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmGoogle, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmGoogle.ResumeLayout(False)
    Me.fmGoogle.PerformLayout()
    CType(Me.edOpGooglePwd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOpGoogleUser.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckOpSulimiti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbApri.GlyphPath = (oApp.ChildImageDir & "\open.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbProgrammi.GlyphPath = (oApp.ChildImageDir & "\ordini.gif")
        tlbAziende.GlyphPath = (oApp.ChildImageDir & "\ordini_2.gif")
        tlbDitte.GlyphPath = (oApp.ChildImageDir & "\ordini_3.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      edOpDefprinter.NTSSetParam(oMenu, oApp.Tr(Me, 128641434597500000, "Pc Abituale"), 20)
      edOpPasssql.NTSSetParam(oMenu, oApp.Tr(Me, 128641434631718750, "Password SQL"), 20)
      edOpLoginsql.NTSSetParam(oMenu, oApp.Tr(Me, 128641434680937500, "Login SQL"), 20)
      edOpPassaccess.NTSSetParam(oMenu, oApp.Tr(Me, 128641434724843750, "Password Access"), 20)
      edOpLoginaccess.NTSSetParam(oMenu, oApp.Tr(Me, 128641434762031250, "Login Access"), 20)
      edOpAzienda.NTSSetParam(oMenu, oApp.Tr(Me, 128641434836875000, "Azienda Abituale"), 25)
      edOpRuolo.NTSSetParam(oMenu, oApp.Tr(Me, 128641434871093750, "Ruolo"), 20)
      edConfermaPwd.NTSSetParam(oMenu, oApp.Tr(Me, 128641434900468750, "Conferma Password"), 14)
      edOpPasswd.NTSSetParam(oMenu, oApp.Tr(Me, 128641434924687500, "Password"), 14)
      edOpDataScad.NTSSetParam(oMenu, oApp.Tr(Me, 128641434954062500, "Data Scadenza"), False)
      edOpPin.NTSSetParam(oMenu, oApp.Tr(Me, 128641434978437500, "Pin / WI"), 255)
      edOpGruppo.NTSSetParam(oMenu, oApp.Tr(Me, 128641434999843750, "Gruppo"), "0", 3, 0, 999)
      edOpNome.NTSSetParam(oMenu, oApp.Tr(Me, 128641435021875000, "Nome Operatore"), 20)
      cbOpCodling.NTSSetParam(oApp.Tr(Me, 128641435056875000, "Lingua"))
      ckOpAbilcamb.NTSSetParam(oMenu, oApp.Tr(Me, 128641435122812500, "Consenti modifica password"), "S", "N")
      ckOpAbil.NTSSetParam(oMenu, oApp.Tr(Me, 128641435145312500, "Operatore abilitato"), "S", "N")
      edOpDescont.NTSSetParam(oMenu, oApp.Tr(Me, 128865221245648248, "Cognome"), 30)
      edOpDescont2.NTSSetParam(oMenu, oApp.Tr(Me, 128865221452835748, "Nome"), 20)
      ckOpIscrmus.NTSSetParam(oMenu, oApp.Tr(Me, 128865221642991998, "Utente CRM"), "S", "N")
      edOpGoogleUser.NTSSetParam(oMenu, oApp.Tr(Me, 129654121527919922, "Account GMail"), 50, True)
      edOpGooglePwd.NTSSetParam(oMenu, oApp.Tr(Me, 129654121936035156, "Password GMail"), 50, True)
      edOpSumail.NTSSetParam(oMenu, oApp.Tr(Me, 129845831682426296, "E-mail utente social"), 255, True)
      ckOpSulimiti.NTSSetParam(oMenu, oApp.Tr(Me, 130710729044686385, "Operatore disabilitato su SBC"), "S", "N")

      edOpRuolo.NTSSetParamZoom("ZOOMRUOLI")
      edOpAzienda.NTSSetParamZoom("ZOOMAZIENDE")

      NTSScriptExec("InitControls", Me, Nothing)

    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRM__GOPE_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      '--------------------------------------------------------------------------------------------------------------
      oCleGope.bModuloCRM = CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And CLN__STD.bsModExtCRM)
      '--------------------------------------------------------------------------------------------------------------
      oCleGope.bGestScadPwd = CBool(oMenu.GetSettingBus("OPZIONI", ".", ".", "GestScadPwd", "0", " ", "0"))
      oCleGope.nGGScadPwd = NTSCInt(oMenu.GetSettingBus("OPZIONI", ".", ".", "GGScadPwd", "0", " ", "180"))
      oCleGope.nMinLungPwd = NTSCInt(oMenu.GetSettingBus("OPZIONI", ".", ".", "MinLungPwd", "0", " ", "8"))
      oCleGope.bPwdComplessa = CBool(oMenu.GetSettingBus("OPZIONI", ".", ".", "PwdComplessa", "0", " ", "0"))
      '--------------------------------------------------------------------------------------------------------------
      CaricaCombo()
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      ckOpIscrmus.NTSText.NTSDbField = "OPERAT.OpIscrmus"
      edOpDescont.NTSDbField = "OPERAT.OpDescont"
      edOpDescont2.NTSDbField = "OPERAT.OpDescont2"
      ckOpNetOnly.NTSText.NTSDbField = "OPERAT.OpNetOnly"
      '--------------------------------------------------------------------------------------------------------------
      oCleGope.VerificaTabling()
      '--------------------------------------------------------------------------------------------------------------
      SetStato(0)
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
      edOpPin.Height = 91
      Me.MinimumSize = Me.Size
      '--------------------------------------------------------------------------------------------------------------
      If Not CLN__STD.UserIsAdmin(oApp.User.Gruppo) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128965530351621094, "L'utente non è abilitato all'utilizzo di questro programma non essendo un amministratore"))
        Me.Close()
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRM__GOPE_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If oCleGope.nSetStato = 1 Then
        If Not Salva() Then e.Cancel = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__GOPE_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcGope.Dispose()
      dsGope.Dispose()
    Catch
    End Try
  End Sub
#End Region

  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano già stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      edOpDefprinter.NTSDbField = "operat.opdefprinter"
      edOpPasssql.NTSDbField = "operat.oppasssql"
      edOpLoginsql.NTSDbField = "operat.oploginsql"
      edOpPassaccess.NTSDbField = "operat.oppassaccess"
      edOpLoginaccess.NTSDbField = "operat.oploginaccess"
      edOpAzienda.NTSDbField = "operat.opazienda"
      edOpRuolo.NTSDbField = "operat.opruolo"
      edConfermaPwd.NTSDbField = "operat.confermapwd"
      edOpPasswd.NTSDbField = "operat.oppasswd"
      lbOpDatulac.NTSDbField = "operat.opdatulac"
      edOpDataScad.NTSDbField = "operat.opdatscad"
      cbOpCodling.NTSDbField = "operat.opcodling"
      edOpPin.NTSDbField = "operat.oppin"
      edOpGruppo.NTSDbField = "operat.opgruppo"
      edOpNome.NTSDbField = "operat.opnome"
      ckOpAbilcamb.NTSText.NTSDbField = "operat.opabilcamb"
      ckOpAbil.NTSText.NTSDbField = "operat.opabil"
      edConfermaPwd.NTSDbField = "operat.xxconfpass"
      edOpGoogleUser.NTSDbField = "operat.OpGoogleUser"
      edOpGooglePwd.NTSDbField = "operat.OpGooglePwd"
      cbOpSutipouser.NTSDbField = "operat.opsutipouser"
      edOpSumail.NTSDbField = "operat.opsumail"
      ckOpSulimiti.NTSText.NTSDbField = "operat.opsulimiti"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcGope, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Toolbar"

  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      Nuovo()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbApri_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApri.ItemClick
    Try
      Apri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      If Salva() Then SetStato(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Dim bRemovBinding As Boolean = False

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Se è attivo il modulo CRM e l'operatore è presente in ACCLEAD, avvisa ed esce
      '--------------------------------------------------------------------------------------------------------------
      If oCleGope.EsisteAcclead(edOpNome.Text) = True Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128986277773302100, "Attenzione!" & vbCrLf & _
          "Sostituire l'operatore che si vuole eliminare con un nuovo operatore CRM prima di procedere con l'eliminazione."))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Cancello l'operaio
      '--------------------------------------------------------------------------------------------------------------
      Dim dlgRes As DialogResult
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128641435921718750, "Confermi la cancellazione?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No : Return
        Case Windows.Forms.DialogResult.Yes
          If dsGope.Tables("OPERAT").Rows.Count = 1 Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If
          dcGope.RemoveAt(dcGope.Position)
          oCleGope.Salva(True)
          If bRemovBinding Then
            NTSFormAddDataBinding(dcGope, Me)
            bRemovBinding = False
            edOpNome.Enabled = True
          Else
            edOpNome.Enabled = False
          End If
          SetStato(0)
          Return
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcGope, Me)
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Dim bRemovBinding As Boolean = False
    Try
      Me.ValidaLastControl()          'se non valido il controllo su cui sono, quando modifico il controllo e, senza uscire, faccio 'ripristina' il controllo rimane sporco

      '-------------------------------------------------
      'ripristino
      Dim dlgRes As DialogResult

      If Not sender Is Nothing Then
        'chiamato facendo pressione sulla funzione 'ripristina'
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128648128437031250, "Ripristinare le modifiche apportate ?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes

          If dsGope.Tables("OPERAT").Rows.Count = 1 And dsGope.Tables("OPERAT").Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          oCleGope.Ripristina(dcGope.Position, dcGope.Filter)

          If bRemovBinding Then
            NTSFormAddDataBinding(dcGope, Me)
          End If

          SetStato(0)
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcGope, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      'per eventuali altri controlli caricati al volo
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      '------------------------------------
      'zoom standard di textbox e griglia
      NTSCallStandardZoom()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbProgrammi_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbProgrammi.ItemClick
    Dim frmGeou As FRM__GEOU = Nothing
    Try
      frmGeou = CType(NTSNewFormModal("FRM__GEOU"), FRM__GEOU)
      oCleGope.strNomeOp = edOpNome.Text
      If Not frmGeou.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return
      frmGeou.InitEntity(oCleGope)
      frmGeou.ShowDialog()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmGeou Is Nothing Then frmGeou.Dispose()
      frmGeou = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbAziende_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAziende.ItemClick
    Dim frmAcaz As FRM__ACAZ = Nothing
    Try
      frmAcaz = CType(NTSNewFormModal("FRM__ACAZ"), FRM__ACAZ)
      oCleGope.strNomeOp = edOpNome.Text
      If Not frmAcaz.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return
      frmAcaz.InitEntity(oCleGope)
      frmAcaz.ShowDialog()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmAcaz Is Nothing Then frmAcaz.Dispose()
      frmAcaz = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbDitte_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDitte.ItemClick
    Dim frmDiop As FRM__DIOP = Nothing
    Try
      frmDiop = CType(NTSNewFormModal("FRM__DIOP"), FRM__DIOP)
      oCleGope.strNomeOp = edOpNome.Text
      If Not frmDiop.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return
      frmDiop.InitEntity(oCleGope)
      frmDiop.ShowDialog()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmDiop Is Nothing Then frmDiop.Dispose()
      frmDiop = Nothing
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

  Public Overridable Sub tlbCopiaConfig_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCopiaConfig.ItemClick
    Dim frmSema As FRM__SEMA = Nothing
    Try
      frmSema = CType(NTSNewFormModal("FRM__SEMA"), FRM__SEMA)
      oCleGope.strNomeOp = edOpNome.Text
      If Not frmSema.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return
      frmSema.InitEntity(oCleGope)
      frmSema.ShowDialog()

      If Not frmSema.bOk = True Then Return

      If oCleGope.CopiaConfig() Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128780469987628619, "Finito"))
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmSema Is Nothing Then frmSema.Dispose()
      frmSema = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbCopiaMenu_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCopiaMenu.ItemClick
    Dim strNomeTmp As String
    Try
      strNomeTmp = oApp.InputBoxNew(oApp.Tr(Me, 128705477381628936, "Indicare il nome dell'operatore da cui prelevare il menu personalizzato"))
      If Trim(strNomeTmp) = "" Then Return

      If oCleGope.CopiaMenu(strNomeTmp) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128780470013253947, "Elaborazione completata regolarmente"))
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

  Public Overridable Sub CaricaCombo()
    Dim dttLing, dttSocial As New DataTable
    Try
      oCleGope.CaricaCombo(dsGope)

      dttLing.Columns.Add("cod", GetType(String))
      dttLing.Columns.Add("val", GetType(String))

      For Each dtrRow As DataRow In dsGope.Tables("TABLING").Rows
        dttLing.Rows.Add(New Object() {NTSCStr(dtrRow!tb_codling), NTSCStr(dtrRow!tb_desling)})
      Next
      dttLing.AcceptChanges()

      cbOpCodling.DataSource = dttLing
      cbOpCodling.ValueMember = "cod"
      cbOpCodling.DisplayMember = "val"

      dttSocial.Columns.Add("cod", GetType(String))
      dttSocial.Columns.Add("val", GetType(String))
      dttSocial.Rows.Add(New Object() {"N", "No"})
      dttSocial.Rows.Add(New Object() {"I", "Interno"})
      dttSocial.Rows.Add(New Object() {"E", "Esterno"})
      If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And CLN__STD.bsModSupBGE) Then dttSocial.Rows.Add(New Object() {"G", "Guest"})
      dttSocial.AcceptChanges()
      cbOpSutipouser.DataSource = dttSocial
      cbOpSutipouser.ValueMember = "cod"
      cbOpSutipouser.DisplayMember = "val"
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub Nuovo()
    Dim frmSeop As FRM__SEOP = Nothing
    Dim strDescr As String = ""
    Try
      frmSeop = CType(NTSNewFormModal("FRM__SEOP"), FRM__SEOP)
      If Not frmSeop.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return
      frmSeop.InitEntity(oCleGope)
      oCleGope.bNew = True
      frmSeop.ShowDialog()

      If frmSeop.bOk Then
        '-------------------------------------------------
        'leggo dal database i dati e collego il NTSBinding
        '-------------------------------------------------
        oCleGope.Nuovo(DittaCorrente, dsGope)

        dcGope.DataSource = dsGope.Tables("OPERAT")
        dcGope.MoveLast()

        '-------------------------------------------------
        'collego il BindingSource ai vari controlli 
        '-------------------------------------------------
        Bindcontrols()
        dcGope.ResetBindings(False)

        SetStato(1)

        If Not oCleGope.LeggiDescGruppo(NTSCInt(edOpGruppo.Text), strDescr) Then Return
        edOpRuolo.NTSTextDB = strDescr
        '-------------------------------------------------
        'imposto i valori di default come impostato nella GCTL
        Me.GctlApplicaDefaultValue()

        oCleGope.bNew = True
        oCleGope.bWizardCompletato = False
      End If

      AbilitaPulsanteCreaRelazioni()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmSeop Is Nothing Then frmSeop.Dispose()
      frmSeop = Nothing
    End Try
  End Sub

  Public Overridable Sub Apri()
    Dim frmSeop As FRM__SEOP = Nothing
    Try
      frmSeop = CType(NTSNewFormModal("FRM__SEOP"), FRM__SEOP)
      If Not frmSeop.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return
      frmSeop.InitEntity(oCleGope)
      oCleGope.bNew = False
      oCleGope.bWizardCompletato = False

      frmSeop.ShowDialog()

      If frmSeop.bOk Then
        '-------------------------------------------------
        'leggo dal database i dati e collego il NTSBinding
        '-------------------------------------------------
        If Not oCleGope.Apri(DittaCorrente, dsGope) Then Me.Close()

        dcGope.DataSource = dsGope.Tables("OPERAT")
        dsGope.AcceptChanges()

        '-------------------------------------------------
        'collego il BindingSource ai vari controlli 
        '-------------------------------------------------
        Bindcontrols()

        SetStato(1)

        edConfermaPwd.Enabled = False
      End If

      AbilitaPulsanteCreaRelazioni()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmSeop Is Nothing Then frmSeop.Dispose()
      frmSeop = Nothing
    End Try
  End Sub

  Public Overridable Function Salva() As Boolean
    Dim bWizard As Boolean = False
    Dim dRes As DialogResult

    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      If oCleGope.RecordIsChanged Then
        '------------------------------------------------------------------------------------------------------------
        If GctlControllaOutNotEqual() = False Then Return False
        '------------------------------------------------------------------------------------------------------------
        '--- Se si sta salvando un nuovo operatore, è attivo il modulo CRM e si è selezionato il CheckBox
        '--- operatore CRM', chiede di utilizzare il wizard per l'alimentazione delle tabelle necessarie
        '--- all'accesso CRM
        '------------------------------------------------------------------------------------------------------------
        If (Not oCleGope.bWizardCompletato) And (oCleGope.bNew = True) And (oCleGope.bModuloCRM = True) And (ckOpIscrmus.Checked = True) Then
          dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128982761446284102, "Attenzione!" & vbCrLf & _
            "Utilizzare il wizard per l'impostazione iniziale del nuovo utente CRM, sulla ditta corrente?" & vbCrLf & _
            "(N.B.: se il nuovo operatore è anche un agente, è assolutamente consigliabile, " & _
            "prima di procedere con il wizard," & vbCrLf & _
            "caricare la sua Anagrafica Fornitore e l'Anagrafica Agente nelle apposite tabelle della ditta corrente)." & vbCrLf & _
            "Procedere?"))
          If dRes = System.Windows.Forms.DialogResult.No Then
            dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129018874810524420, "Salvo l'operatore?"))
          Else
            bWizard = True
          End If
        Else
          dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128705485519741113, "Salvo l'operatore?"))
        End If
        '------------------------------------------------------------------------------------------------------------
        If bWizard = False Then
          If dRes = System.Windows.Forms.DialogResult.Yes Then
            If Not oCleGope.Salva(False) Then Return False
            If dsGope.Tables("OPERAT").Rows.Count > 0 Then edOpNome.Enabled = False
          End If
          If dRes = System.Windows.Forms.DialogResult.No Then tlbRipristina_ItemClick(Nothing, Nothing)
        Else
          '----------------------------------------------------------------------------------------------------------
          If Wizard() = False Then Return False
          If Not oCleGope.Salva(False) Then Return False
          If dsGope.Tables("OPERAT").Rows.Count > 0 Then edOpNome.Enabled = False
        End If
        '------------------------------------------------------------------------------------------------------------
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Sub SetStato(ByVal nSetStato As Integer)
    Try
      oCleGope.nSetStato = nSetStato

      If nSetStato = 0 Then
        oCleGope.bNew = False

        pnTutto.Visible = False

        tlbNuovo.Enabled = True
        tlbApri.Enabled = True
        tlbSalva.Enabled = False
        tlbRipristina.Enabled = False
        tlbCancella.Enabled = False
        tlbZoom.Enabled = False

        tlbProgrammi.Enabled = False
        tlbAziende.Enabled = False
        tlbDitte.Enabled = False

        tlbCopiaConfig.Enabled = False
        tlbCopiaMenu.Enabled = False

        edOpDataScad.Enabled = False
        edOpNome.Enabled = False
        ckOpAbil.Enabled = False
        ckOpAbilcamb.Enabled = False

        lbNomeOp.Visible = False
        lbGruppoOp.Visible = False
        lbRuoloOp.Visible = False
        lbAzienda.Visible = False
        lbLogAcc.Visible = False
        lbPwdOp.Visible = False
        lbPwdAc.Visible = False
        lbLogSQL.Visible = False
        lbPwdSQL.Visible = False
        lbNomePc.Visible = False
        lbPW.Visible = False
        lbConfPwd.Visible = False
        lbDataScad.Visible = False
        lbultacc.Visible = False
        lbLingua.Visible = False

        edOpNome.Visible = False
        edOpPasswd.Visible = False
        edOpGruppo.Visible = False
        edOpRuolo.Visible = False
        edOpAzienda.Visible = False
        edOpLoginaccess.Visible = False
        edOpPassaccess.Visible = False
        edOpLoginsql.Visible = False
        edOpPasssql.Visible = False
        edOpDefprinter.Visible = False
        edOpPin.Visible = False
        edConfermaPwd.Visible = False
        edOpDataScad.Visible = False

        lbOpDatulac.Visible = False
        cbOpCodling.Visible = False
        ckOpAbil.Visible = False
        ckOpAbilcamb.Visible = False

        ckOpIscrmus.Visible = False
        cmdCreaRelazioni.Visible = False
        lbOpDescont.Visible = False : edOpDescont.Visible = False
        lbOpDescont2.Visible = False : edOpDescont2.Visible = False

        ckOpNetOnly.Visible = False
        ckOpSulimiti.Visible = False
      Else
        tlbNuovo.Enabled = False
        tlbApri.Enabled = False

        pnTutto.Visible = True

        tlbSalva.Enabled = True
        tlbRipristina.Enabled = True
        tlbCancella.Enabled = False
        tlbZoom.Enabled = True

        tlbProgrammi.Enabled = False
        tlbAziende.Enabled = False
        tlbDitte.Enabled = False

        tlbCopiaConfig.Enabled = True
        tlbCopiaMenu.Enabled = True

        lbOpDatulac.Visible = True
        lbNomeOp.Visible = True
        lbGruppoOp.Visible = True
        lbRuoloOp.Visible = True
        lbAzienda.Visible = True
        lbPwdOp.Visible = True
        lbLogAcc.Visible = False
        lbPwdAc.Visible = False
        lbLogSQL.Visible = False
        lbPwdSQL.Visible = False
        lbNomePc.Visible = True
        lbPW.Visible = True
        lbConfPwd.Visible = True
        lbDataScad.Visible = True
        lbultacc.Visible = True
        lbLingua.Visible = True

        edOpGruppo.Enabled = False
        edOpRuolo.Enabled = True
        edOpDataScad.Enabled = False
        edOpNome.Enabled = False
        ckOpAbil.Enabled = False
        ckOpAbilcamb.Enabled = False

        edOpNome.Visible = True
        edOpPasswd.Visible = True
        edOpGruppo.Visible = True
        edOpRuolo.Visible = True
        edOpAzienda.Visible = True
        edOpLoginaccess.Visible = False
        edOpPassaccess.Visible = False
        edOpLoginsql.Visible = False
        edOpPasssql.Visible = False
        edOpDefprinter.Visible = True
        edOpPin.Visible = True
        edConfermaPwd.Visible = True
        edOpDataScad.Visible = True

        cbOpCodling.Visible = True

        ckOpAbil.Visible = True
        ckOpAbilcamb.Visible = True

        '-----------------------------------------------------------------------------------------
        '--- Se l'opzione di registro relativa alla gestione Password NON è attiva
        '--- disabilita i controlli relativi
        '-----------------------------------------------------------------------------------------
        If NTSCInt(oCleGope.bGestScadPwd) = 0 Then
          ckOpAbil.Enabled = False
          ckOpAbilcamb.Enabled = False
          edOpDataScad.Enabled = False
        Else
          ckOpAbil.Enabled = True
          ckOpAbilcamb.Enabled = True
          edOpDataScad.Enabled = True
        End If

        If oCleGope.bNew = False Then
          'edOpRuolo.Enabled = False
          tlbCancella.Enabled = True
          tlbProgrammi.Enabled = True
          tlbAziende.Enabled = True
          tlbDitte.Enabled = True
        End If

        GctlSetVisEnab(ckOpIscrmus, True)
        GctlSetVisEnab(cmdCreaRelazioni, True)
        GctlSetVisEnab(lbOpDescont, True)
        GctlSetVisEnab(edOpDescont, True)
        GctlSetVisEnab(lbOpDescont2, True)
        GctlSetVisEnab(edOpDescont2, True)
        'GctlSetVisEnab(ckOpNetOnly, True)
        GctlSetVisEnab(ckOpSulimiti, True)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Wizard() As Boolean
    Dim frmWzrd As FRM__WZRD = Nothing
    Dim frmMsok As FRM__MSOK = Nothing

    Try
      frmWzrd = CType(NTSNewFormModal("FRM__WZRD"), FRM__WZRD)
      frmMsok = CType(NTSNewFormModal("FRM__MSOK"), FRM__MSOK)
      '--------------------------------------------------------------------------------------------------------------
      If (edOpDescont.Text.Trim = "") Or (edOpDescont2.Text.Trim = "") Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128982771046755287, "Attenzione!" & vbCrLf & _
          "Se si è scelto di procedere con il wizard per l'impostazione del nuovo utente CRM" & vbCrLf & _
          "'Cognome' e 'Nome' sono obbligatori."))
        edOpDescont.Focus()
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not frmWzrd.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return False
      frmWzrd.InitEntity(oCleGope)
      frmWzrd.ShowDialog()
      '--------------------------------------------------------------------------------------------------------------
      '--- Se annullato, esce
      '--------------------------------------------------------------------------------------------------------------
      If frmWzrd.bAnnullato = True Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128985969507003680, "Impostazione nuovo utente CRM con wizard annullato." & vbCrLf & _
          "N.B.: Il nuovo operatore NON è ancora stato salvato."))
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not frmMsok.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return False
      frmMsok.InitEntity(oCleGope)
      frmMsok.strCognome = edOpDescont.Text
      frmMsok.strNome = edOpDescont2.Text
      frmMsok.bCrmmod = frmWzrd.bCrmmod
      frmMsok.bAmm = frmWzrd.bAmm
      frmMsok.bCodcage = frmWzrd.bCodcage
      frmMsok.nCodcage = frmWzrd.nCodcage
      frmMsok.strDescodcage = frmWzrd.strDescodcage
      frmMsok.strCodruaz = frmWzrd.strCodruaz
      frmMsok.strDescodruaz = frmWzrd.strDescodruaz
      frmMsok.strEmail = frmWzrd.strEmail
      frmMsok.strCell = frmWzrd.strCell
      frmMsok.nAltriAccessi = frmWzrd.nAltriAccessi
      frmMsok.bSostituisce = frmWzrd.bSostituisce
      frmMsok.strOldOperatore = frmWzrd.strOldOperatore
      frmMsok.nOldAgente = frmWzrd.nOldAgente
      frmMsok.strDesOldAgente = frmWzrd.strDesOldAgente
      frmMsok.ShowDialog()
      '--------------------------------------------------------------------------------------------------------------
      If frmMsok.bAnnullato Then
        If frmMsok.bIndietro = False Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128986068028066690, "Impostazione nuovo utente CRM con wizard annullato." & vbCrLf & _
            "N.B.: Il nuovo operatore NON è ancora stato salvato."))
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleGope.Wizard(edOpNome.Text, frmWzrd.bCrmmod, frmWzrd.bAmm, frmWzrd.bCodcage, frmWzrd.nCodcage, _
        frmWzrd.strCodruaz, frmWzrd.strEmail, frmWzrd.strCell, edOpDescont.Text, edOpDescont2.Text, _
        oCleGope.bModuloCRM, frmWzrd.nAltriAccessi, frmWzrd.bSostituisce, frmWzrd.strOldOperatore, _
        frmWzrd.nOldAgente) Then Return False
      '--------------------------------------------------------------------------------------------------------------
      oCleGope.bWizardCompletato = True
      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      If Not frmWzrd Is Nothing Then frmWzrd.Dispose()
      frmWzrd = Nothing
      If Not frmMsok Is Nothing Then frmMsok.Dispose()
      frmMsok = Nothing
    End Try
  End Function

#Region "Eventi"
  Public Overridable Sub edOpPasswd_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edOpPasswd.EditValueChanged
    Try
      If edOpPasswd.Text = "" And edConfermaPwd.Text = "" Then
        edConfermaPwd.Enabled = False
      Else
        edConfermaPwd.Enabled = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edConfermaPwd_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edConfermaPwd.TextChanged
    Try
      If edOpPasswd.Text = "" And edConfermaPwd.Text = "" Then
        edConfermaPwd.Enabled = False
      Else
        edConfermaPwd.Enabled = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edOpRuolo_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edOpRuolo.Validated
    Dim lOpGruppo As Integer
    Try
      If edOpRuolo.Text.Trim = "" Then Return
      If Not oCleGope.LeggiRuolo(edOpRuolo.Text, lOpGruppo) Then Return
      If lOpGruppo <> -1 Then
        If lOpGruppo <> NTSCInt(edOpGruppo.Text) Then edOpGruppo.NTSTextDB = NTSCStr(lOpGruppo)
      Else
        If Not oCleGope.LeggiGruppo(NTSCInt(edOpGruppo.Text)) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128647565380312500, "Il ruolo inserito è inesistente"))
          oCleGope.LeggiDescGruppo(NTSCInt(edOpGruppo.Text), edOpRuolo.Text)
        Else
          If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128780457726666532, "Ruolo inesistente: confermi la creazione?")) = Windows.Forms.DialogResult.Yes Then
            If Not oCleGope.AggiungiRuolo(edOpRuolo.Text, NTSCInt(edOpGruppo.Text)) Then Return
          End If
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cbOpSutipouser_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbOpSutipouser.SelectedValueChanged
    Try
      If Not dsGope.Tables.Contains("OPERAT") Then Return

      AbilitaPulsanteCreaRelazioni()

      If cbOpSutipouser.SelectedValue = "N" Then
        'dsGope.Tables("OPERAT").Rows(0)!OpSutipouser = "N" 'Serve, altrimenti ripristina il valore precedente quando andrà a cambiare l'e-mail
        'edOpSumail.NTSTextDB = ""
        edOpSumail.Enabled = False
      Else
        edOpSumail.Enabled = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckOpIscrmus_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckOpIscrmus.CheckedChanged
    Try
      AbilitaPulsanteCreaRelazioni()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdCreaRelazioni_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreaRelazioni.Click
    Try
      Salva()

      If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 130543786572565316, "Creare le relazioni tra l'utente corrente e i contatti dei suoi Leads che hanno un operatore Guest?")) = Windows.Forms.DialogResult.Yes Then
        oCleGope.CreaRelazioniConLeadGuest()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

  Public Overridable Sub AbilitaPulsanteCreaRelazioni()
    Try
      If ckOpIscrmus.Checked AndAlso cbOpSutipouser.SelectedValue <> "N" Then
        GctlSetVisible(cmdCreaRelazioni)
      Else
        cmdCreaRelazioni.Visible = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
End Class