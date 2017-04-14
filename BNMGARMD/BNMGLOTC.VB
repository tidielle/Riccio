Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGLOTC
  Public oCleArmd As CLEMGARMD
  Public oCallParams As CLE__CLDP

  Public bOnLoading As Boolean = False
  Public bGescomm As Boolean = False
  Public bGeslotti As Boolean = False
  Public bGesubic As Boolean = False

  Public dsLotc As New DataSet
  Public dcLotc As New BindingSource()

#Region "Inizializzazione"
  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, _
    Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    '----------------------------------------------------------------------------------------------------------------
    oMenu = Menu
    oApp = oMenu.App
    oCallParams = Param
    '----------------------------------------------------------------------------------------------------------------
    If Ditta <> "" Then DittaCorrente = Ditta Else DittaCorrente = oApp.Ditta
    '----------------------------------------------------------------------------------------------------------------
    Me.GctlTipoDoc = ""
    '----------------------------------------------------------------------------------------------------------------
    InitializeComponent()
    Me.MinimumSize = Me.Size
    '----------------------------------------------------------------------------------------------------------------
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGARMD", "BEMGARMD", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128550728307822408, "ERRORE in fase di creazione Entity:" & vbCrLf & strErr))
      Return False
    End If
    oCleArmd = CType(oTmp, CLEMGARMD)
    '----------------------------------------------------------------------------------------------------------------
    bRemoting = Menu.Remoting("BNMGARMD", strRemoteServer, strRemotePort)
    AddHandler oCleArmd.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleArmd.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    '----------------------------------------------------------------------------------------------------------------
    oCleArmd.strPrgParent = oCallParams.strPar1
    oCleArmd.strHlapCodart = oCallParams.strPar2
    oCleArmd.dHlapFase = oCallParams.dPar1
    '----------------------------------------------------------------------------------------------------------------
    Return True
  End Function

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGLOTC))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbCommessa = New NTSInformatica.NTSBarButtonItem
    Me.tlbUbicazioni = New NTSInformatica.NTSBarButtonItem
    Me.tlbLotti = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.lbXx_fase = New NTSInformatica.NTSLabel
    Me.lbXx_magaz = New NTSInformatica.NTSLabel
    Me.lbXx_codart = New NTSInformatica.NTSLabel
    Me.lbAr_fase = New NTSInformatica.NTSLabel
    Me.lbAr_magaz = New NTSInformatica.NTSLabel
    Me.lbAr_codart = New NTSInformatica.NTSLabel
    Me.lbFase = New NTSInformatica.NTSLabel
    Me.lbMagaz = New NTSInformatica.NTSLabel
    Me.lbCodart = New NTSInformatica.NTSLabel
    Me.grLotc = New NTSInformatica.NTSGrid
    Me.grvLotc = New NTSInformatica.NTSGridView
    Me.apt_impeg01 = New NTSInformatica.NTSGridColumn
    Me.apt_impeg02 = New NTSInformatica.NTSGridColumn
    Me.apt_impeg03 = New NTSInformatica.NTSGridColumn
    Me.apt_impeg04 = New NTSInformatica.NTSGridColumn
    Me.apt_impeg05 = New NTSInformatica.NTSGridColumn
    Me.apt_impeg06 = New NTSInformatica.NTSGridColumn
    Me.apt_impeg07 = New NTSInformatica.NTSGridColumn
    Me.apt_impeg08 = New NTSInformatica.NTSGridColumn
    Me.apt_impeg09 = New NTSInformatica.NTSGridColumn
    Me.apt_impeg10 = New NTSInformatica.NTSGridColumn
    Me.apt_impeg11 = New NTSInformatica.NTSGridColumn
    Me.apt_impeg12 = New NTSInformatica.NTSGridColumn
    Me.apt_impeg13 = New NTSInformatica.NTSGridColumn
    Me.apt_impeg14 = New NTSInformatica.NTSGridColumn
    Me.apt_impeg15 = New NTSInformatica.NTSGridColumn
    Me.apt_impeg16 = New NTSInformatica.NTSGridColumn
    Me.apt_impeg17 = New NTSInformatica.NTSGridColumn
    Me.apt_impeg18 = New NTSInformatica.NTSGridColumn
    Me.apt_impeg19 = New NTSInformatica.NTSGridColumn
    Me.apt_impeg20 = New NTSInformatica.NTSGridColumn
    Me.apt_impeg21 = New NTSInformatica.NTSGridColumn
    Me.apt_impeg22 = New NTSInformatica.NTSGridColumn
    Me.apt_impeg23 = New NTSInformatica.NTSGridColumn
    Me.apt_impeg24 = New NTSInformatica.NTSGridColumn
    Me.apt_commeca = New NTSInformatica.NTSGridColumn
    Me.xx_commeca = New NTSInformatica.NTSGridColumn
    Me.apt_lotto = New NTSInformatica.NTSGridColumn
    Me.xx_lotto = New NTSInformatica.NTSGridColumn
    Me.apt_ubicaz = New NTSInformatica.NTSGridColumn
    Me.apt_esist01 = New NTSInformatica.NTSGridColumn
    Me.apt_esist02 = New NTSInformatica.NTSGridColumn
    Me.apt_esist03 = New NTSInformatica.NTSGridColumn
    Me.apt_esist04 = New NTSInformatica.NTSGridColumn
    Me.apt_esist05 = New NTSInformatica.NTSGridColumn
    Me.apt_esist06 = New NTSInformatica.NTSGridColumn
    Me.apt_esist07 = New NTSInformatica.NTSGridColumn
    Me.apt_esist08 = New NTSInformatica.NTSGridColumn
    Me.apt_esist09 = New NTSInformatica.NTSGridColumn
    Me.apt_esist10 = New NTSInformatica.NTSGridColumn
    Me.apt_esist11 = New NTSInformatica.NTSGridColumn
    Me.apt_esist12 = New NTSInformatica.NTSGridColumn
    Me.apt_esist13 = New NTSInformatica.NTSGridColumn
    Me.apt_esist14 = New NTSInformatica.NTSGridColumn
    Me.apt_esist15 = New NTSInformatica.NTSGridColumn
    Me.apt_esist16 = New NTSInformatica.NTSGridColumn
    Me.apt_esist17 = New NTSInformatica.NTSGridColumn
    Me.apt_esist18 = New NTSInformatica.NTSGridColumn
    Me.apt_esist19 = New NTSInformatica.NTSGridColumn
    Me.apt_esist20 = New NTSInformatica.NTSGridColumn
    Me.apt_esist21 = New NTSInformatica.NTSGridColumn
    Me.apt_esist22 = New NTSInformatica.NTSGridColumn
    Me.apt_esist23 = New NTSInformatica.NTSGridColumn
    Me.apt_esist24 = New NTSInformatica.NTSGridColumn
    Me.apt_prenot01 = New NTSInformatica.NTSGridColumn
    Me.apt_prenot02 = New NTSInformatica.NTSGridColumn
    Me.apt_prenot03 = New NTSInformatica.NTSGridColumn
    Me.apt_prenot04 = New NTSInformatica.NTSGridColumn
    Me.apt_prenot05 = New NTSInformatica.NTSGridColumn
    Me.apt_prenot06 = New NTSInformatica.NTSGridColumn
    Me.apt_prenot07 = New NTSInformatica.NTSGridColumn
    Me.apt_prenot08 = New NTSInformatica.NTSGridColumn
    Me.apt_prenot09 = New NTSInformatica.NTSGridColumn
    Me.apt_prenot10 = New NTSInformatica.NTSGridColumn
    Me.apt_prenot11 = New NTSInformatica.NTSGridColumn
    Me.apt_prenot12 = New NTSInformatica.NTSGridColumn
    Me.apt_prenot13 = New NTSInformatica.NTSGridColumn
    Me.apt_prenot14 = New NTSInformatica.NTSGridColumn
    Me.apt_prenot15 = New NTSInformatica.NTSGridColumn
    Me.apt_prenot16 = New NTSInformatica.NTSGridColumn
    Me.apt_prenot17 = New NTSInformatica.NTSGridColumn
    Me.apt_prenot18 = New NTSInformatica.NTSGridColumn
    Me.apt_prenot19 = New NTSInformatica.NTSGridColumn
    Me.apt_prenot20 = New NTSInformatica.NTSGridColumn
    Me.apt_prenot21 = New NTSInformatica.NTSGridColumn
    Me.apt_prenot22 = New NTSInformatica.NTSGridColumn
    Me.apt_prenot23 = New NTSInformatica.NTSGridColumn
    Me.apt_prenot24 = New NTSInformatica.NTSGridColumn
    Me.apt_ordin01 = New NTSInformatica.NTSGridColumn
    Me.apt_ordin02 = New NTSInformatica.NTSGridColumn
    Me.apt_ordin03 = New NTSInformatica.NTSGridColumn
    Me.apt_ordin04 = New NTSInformatica.NTSGridColumn
    Me.apt_ordin05 = New NTSInformatica.NTSGridColumn
    Me.apt_ordin06 = New NTSInformatica.NTSGridColumn
    Me.apt_ordin07 = New NTSInformatica.NTSGridColumn
    Me.apt_ordin08 = New NTSInformatica.NTSGridColumn
    Me.apt_ordin09 = New NTSInformatica.NTSGridColumn
    Me.apt_ordin10 = New NTSInformatica.NTSGridColumn
    Me.apt_ordin11 = New NTSInformatica.NTSGridColumn
    Me.apt_ordin12 = New NTSInformatica.NTSGridColumn
    Me.apt_ordin13 = New NTSInformatica.NTSGridColumn
    Me.apt_ordin14 = New NTSInformatica.NTSGridColumn
    Me.apt_ordin15 = New NTSInformatica.NTSGridColumn
    Me.apt_ordin16 = New NTSInformatica.NTSGridColumn
    Me.apt_ordin17 = New NTSInformatica.NTSGridColumn
    Me.apt_ordin18 = New NTSInformatica.NTSGridColumn
    Me.apt_ordin19 = New NTSInformatica.NTSGridColumn
    Me.apt_ordin20 = New NTSInformatica.NTSGridColumn
    Me.apt_ordin21 = New NTSInformatica.NTSGridColumn
    Me.apt_ordin22 = New NTSInformatica.NTSGridColumn
    Me.apt_ordin23 = New NTSInformatica.NTSGridColumn
    Me.apt_ordin24 = New NTSInformatica.NTSGridColumn
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.grLotc, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvLotc, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbEsci, Me.tlbCommessa, Me.tlbUbicazioni, Me.tlbLotti})
    Me.NtsBarManager1.MaxItemId = 32
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCommessa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUbicazioni), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbLotti), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci, True)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbCommessa
    '
    Me.tlbCommessa.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbCommessa.Caption = "Commessa"
    Me.tlbCommessa.GlyphPath = ""
    Me.tlbCommessa.Id = 29
    Me.tlbCommessa.Name = "tlbCommessa"
    Me.tlbCommessa.Visible = True
    '
    'tlbUbicazioni
    '
    Me.tlbUbicazioni.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbUbicazioni.Caption = "Ubicazioni"
    Me.tlbUbicazioni.GlyphPath = ""
    Me.tlbUbicazioni.Id = 30
    Me.tlbUbicazioni.Name = "tlbUbicazioni"
    Me.tlbUbicazioni.Visible = True
    '
    'tlbLotti
    '
    Me.tlbLotti.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbLotti.Caption = "Lotti"
    Me.tlbLotti.GlyphPath = ""
    Me.tlbLotti.Id = 31
    Me.tlbLotti.Name = "tlbLotti"
    Me.tlbLotti.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 27
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.lbXx_fase)
    Me.pnTop.Controls.Add(Me.lbXx_magaz)
    Me.pnTop.Controls.Add(Me.lbXx_codart)
    Me.pnTop.Controls.Add(Me.lbAr_fase)
    Me.pnTop.Controls.Add(Me.lbAr_magaz)
    Me.pnTop.Controls.Add(Me.lbAr_codart)
    Me.pnTop.Controls.Add(Me.lbFase)
    Me.pnTop.Controls.Add(Me.lbMagaz)
    Me.pnTop.Controls.Add(Me.lbCodart)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(678, 74)
    Me.pnTop.TabIndex = 16
    '
    'lbXx_fase
    '
    Me.lbXx_fase.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_fase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_fase.Location = New System.Drawing.Point(143, 50)
    Me.lbXx_fase.Name = "lbXx_fase"
    Me.lbXx_fase.NTSDbField = ""
    Me.lbXx_fase.Size = New System.Drawing.Size(529, 20)
    Me.lbXx_fase.TabIndex = 10
    Me.lbXx_fase.Tooltip = ""
    Me.lbXx_fase.UseMnemonic = False
    '
    'lbXx_magaz
    '
    Me.lbXx_magaz.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_magaz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_magaz.Location = New System.Drawing.Point(143, 27)
    Me.lbXx_magaz.Name = "lbXx_magaz"
    Me.lbXx_magaz.NTSDbField = ""
    Me.lbXx_magaz.Size = New System.Drawing.Size(529, 20)
    Me.lbXx_magaz.TabIndex = 9
    Me.lbXx_magaz.Tooltip = ""
    Me.lbXx_magaz.UseMnemonic = False
    '
    'lbXx_codart
    '
    Me.lbXx_codart.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codart.Location = New System.Drawing.Point(266, 4)
    Me.lbXx_codart.Name = "lbXx_codart"
    Me.lbXx_codart.NTSDbField = ""
    Me.lbXx_codart.Size = New System.Drawing.Size(406, 20)
    Me.lbXx_codart.TabIndex = 8
    Me.lbXx_codart.Tooltip = ""
    Me.lbXx_codart.UseMnemonic = False
    '
    'lbAr_fase
    '
    Me.lbAr_fase.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_fase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbAr_fase.Location = New System.Drawing.Point(82, 49)
    Me.lbAr_fase.Name = "lbAr_fase"
    Me.lbAr_fase.NTSDbField = ""
    Me.lbAr_fase.Size = New System.Drawing.Size(55, 20)
    Me.lbAr_fase.TabIndex = 7
    Me.lbAr_fase.TextAlign = System.Drawing.ContentAlignment.TopRight
    Me.lbAr_fase.Tooltip = ""
    Me.lbAr_fase.UseMnemonic = False
    '
    'lbAr_magaz
    '
    Me.lbAr_magaz.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_magaz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbAr_magaz.Location = New System.Drawing.Point(82, 27)
    Me.lbAr_magaz.Name = "lbAr_magaz"
    Me.lbAr_magaz.NTSDbField = ""
    Me.lbAr_magaz.Size = New System.Drawing.Size(55, 20)
    Me.lbAr_magaz.TabIndex = 6
    Me.lbAr_magaz.TextAlign = System.Drawing.ContentAlignment.TopRight
    Me.lbAr_magaz.Tooltip = ""
    Me.lbAr_magaz.UseMnemonic = False
    '
    'lbAr_codart
    '
    Me.lbAr_codart.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_codart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbAr_codart.Location = New System.Drawing.Point(82, 5)
    Me.lbAr_codart.Name = "lbAr_codart"
    Me.lbAr_codart.NTSDbField = ""
    Me.lbAr_codart.Size = New System.Drawing.Size(178, 20)
    Me.lbAr_codart.TabIndex = 5
    Me.lbAr_codart.Tooltip = ""
    Me.lbAr_codart.UseMnemonic = False
    '
    'lbFase
    '
    Me.lbFase.AutoSize = True
    Me.lbFase.BackColor = System.Drawing.Color.Transparent
    Me.lbFase.Location = New System.Drawing.Point(12, 52)
    Me.lbFase.Name = "lbFase"
    Me.lbFase.NTSDbField = ""
    Me.lbFase.Size = New System.Drawing.Size(30, 13)
    Me.lbFase.TabIndex = 4
    Me.lbFase.Text = "Fase"
    Me.lbFase.Tooltip = ""
    Me.lbFase.UseMnemonic = False
    '
    'lbMagaz
    '
    Me.lbMagaz.AutoSize = True
    Me.lbMagaz.BackColor = System.Drawing.Color.Transparent
    Me.lbMagaz.Location = New System.Drawing.Point(12, 30)
    Me.lbMagaz.Name = "lbMagaz"
    Me.lbMagaz.NTSDbField = ""
    Me.lbMagaz.Size = New System.Drawing.Size(57, 13)
    Me.lbMagaz.TabIndex = 3
    Me.lbMagaz.Text = "Magazzino"
    Me.lbMagaz.Tooltip = ""
    Me.lbMagaz.UseMnemonic = False
    '
    'lbCodart
    '
    Me.lbCodart.AutoSize = True
    Me.lbCodart.BackColor = System.Drawing.Color.Transparent
    Me.lbCodart.Location = New System.Drawing.Point(12, 8)
    Me.lbCodart.Name = "lbCodart"
    Me.lbCodart.NTSDbField = ""
    Me.lbCodart.Size = New System.Drawing.Size(43, 13)
    Me.lbCodart.TabIndex = 2
    Me.lbCodart.Text = "Articolo"
    Me.lbCodart.Tooltip = ""
    Me.lbCodart.UseMnemonic = False
    '
    'grLotc
    '
    Me.grLotc.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grLotc.EmbeddedNavigator.Name = ""
    Me.grLotc.Location = New System.Drawing.Point(0, 104)
    Me.grLotc.MainView = Me.grvLotc
    Me.grLotc.Name = "grLotc"
    Me.grLotc.Size = New System.Drawing.Size(678, 275)
    Me.grLotc.TabIndex = 17
    Me.grLotc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvLotc})
    '
    'grvLotc
    '
    Me.grvLotc.ActiveFilterEnabled = False
    '
    'apt_impeg01
    '
    Me.apt_impeg01.AppearanceCell.Options.UseBackColor = True
    Me.apt_impeg01.AppearanceCell.Options.UseTextOptions = True
    Me.apt_impeg01.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_impeg01.Caption = "Impegnato 01"
    Me.apt_impeg01.Enabled = True
    Me.apt_impeg01.FieldName = "apt_impeg01"
    Me.apt_impeg01.Name = "apt_impeg01"
    Me.apt_impeg01.NTSRepositoryComboBox = Nothing
    Me.apt_impeg01.NTSRepositoryItemCheck = Nothing
    Me.apt_impeg01.NTSRepositoryItemMemo = Nothing
    Me.apt_impeg01.NTSRepositoryItemText = Nothing
    Me.apt_impeg01.Visible = True
    Me.apt_impeg01.VisibleIndex = 77
    '
    'apt_impeg02
    '
    Me.apt_impeg02.AppearanceCell.Options.UseBackColor = True
    Me.apt_impeg02.AppearanceCell.Options.UseTextOptions = True
    Me.apt_impeg02.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_impeg02.Caption = "Impegnato 02"
    Me.apt_impeg02.Enabled = True
    Me.apt_impeg02.FieldName = "apt_impeg02"
    Me.apt_impeg02.Name = "apt_impeg02"
    Me.apt_impeg02.NTSRepositoryComboBox = Nothing
    Me.apt_impeg02.NTSRepositoryItemCheck = Nothing
    Me.apt_impeg02.NTSRepositoryItemMemo = Nothing
    Me.apt_impeg02.NTSRepositoryItemText = Nothing
    Me.apt_impeg02.Visible = True
    Me.apt_impeg02.VisibleIndex = 78
    '
    'apt_impeg03
    '
    Me.apt_impeg03.AppearanceCell.Options.UseBackColor = True
    Me.apt_impeg03.AppearanceCell.Options.UseTextOptions = True
    Me.apt_impeg03.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_impeg03.Caption = "Impegnato 03"
    Me.apt_impeg03.Enabled = True
    Me.apt_impeg03.FieldName = "apt_impeg03"
    Me.apt_impeg03.Name = "apt_impeg03"
    Me.apt_impeg03.NTSRepositoryComboBox = Nothing
    Me.apt_impeg03.NTSRepositoryItemCheck = Nothing
    Me.apt_impeg03.NTSRepositoryItemMemo = Nothing
    Me.apt_impeg03.NTSRepositoryItemText = Nothing
    Me.apt_impeg03.Visible = True
    Me.apt_impeg03.VisibleIndex = 79
    '
    'apt_impeg04
    '
    Me.apt_impeg04.AppearanceCell.Options.UseBackColor = True
    Me.apt_impeg04.AppearanceCell.Options.UseTextOptions = True
    Me.apt_impeg04.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_impeg04.Caption = "Impegnato 04"
    Me.apt_impeg04.Enabled = True
    Me.apt_impeg04.FieldName = "apt_impeg04"
    Me.apt_impeg04.Name = "apt_impeg04"
    Me.apt_impeg04.NTSRepositoryComboBox = Nothing
    Me.apt_impeg04.NTSRepositoryItemCheck = Nothing
    Me.apt_impeg04.NTSRepositoryItemMemo = Nothing
    Me.apt_impeg04.NTSRepositoryItemText = Nothing
    Me.apt_impeg04.Visible = True
    Me.apt_impeg04.VisibleIndex = 80
    '
    'apt_impeg05
    '
    Me.apt_impeg05.AppearanceCell.Options.UseBackColor = True
    Me.apt_impeg05.AppearanceCell.Options.UseTextOptions = True
    Me.apt_impeg05.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_impeg05.Caption = "Impegnato 05"
    Me.apt_impeg05.Enabled = True
    Me.apt_impeg05.FieldName = "apt_impeg05"
    Me.apt_impeg05.Name = "apt_impeg05"
    Me.apt_impeg05.NTSRepositoryComboBox = Nothing
    Me.apt_impeg05.NTSRepositoryItemCheck = Nothing
    Me.apt_impeg05.NTSRepositoryItemMemo = Nothing
    Me.apt_impeg05.NTSRepositoryItemText = Nothing
    Me.apt_impeg05.Visible = True
    Me.apt_impeg05.VisibleIndex = 81
    '
    'apt_impeg06
    '
    Me.apt_impeg06.AppearanceCell.Options.UseBackColor = True
    Me.apt_impeg06.AppearanceCell.Options.UseTextOptions = True
    Me.apt_impeg06.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_impeg06.Caption = "Impegnato 06"
    Me.apt_impeg06.Enabled = True
    Me.apt_impeg06.FieldName = "apt_impeg06"
    Me.apt_impeg06.Name = "apt_impeg06"
    Me.apt_impeg06.NTSRepositoryComboBox = Nothing
    Me.apt_impeg06.NTSRepositoryItemCheck = Nothing
    Me.apt_impeg06.NTSRepositoryItemMemo = Nothing
    Me.apt_impeg06.NTSRepositoryItemText = Nothing
    Me.apt_impeg06.Visible = True
    Me.apt_impeg06.VisibleIndex = 82
    '
    'apt_impeg07
    '
    Me.apt_impeg07.AppearanceCell.Options.UseBackColor = True
    Me.apt_impeg07.AppearanceCell.Options.UseTextOptions = True
    Me.apt_impeg07.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_impeg07.Caption = "Impegnato 07"
    Me.apt_impeg07.Enabled = True
    Me.apt_impeg07.FieldName = "apt_impeg07"
    Me.apt_impeg07.Name = "apt_impeg07"
    Me.apt_impeg07.NTSRepositoryComboBox = Nothing
    Me.apt_impeg07.NTSRepositoryItemCheck = Nothing
    Me.apt_impeg07.NTSRepositoryItemMemo = Nothing
    Me.apt_impeg07.NTSRepositoryItemText = Nothing
    Me.apt_impeg07.Visible = True
    Me.apt_impeg07.VisibleIndex = 83
    '
    'apt_impeg08
    '
    Me.apt_impeg08.AppearanceCell.Options.UseBackColor = True
    Me.apt_impeg08.AppearanceCell.Options.UseTextOptions = True
    Me.apt_impeg08.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_impeg08.Caption = "Impegnato 08"
    Me.apt_impeg08.Enabled = True
    Me.apt_impeg08.FieldName = "apt_impeg08"
    Me.apt_impeg08.Name = "apt_impeg08"
    Me.apt_impeg08.NTSRepositoryComboBox = Nothing
    Me.apt_impeg08.NTSRepositoryItemCheck = Nothing
    Me.apt_impeg08.NTSRepositoryItemMemo = Nothing
    Me.apt_impeg08.NTSRepositoryItemText = Nothing
    Me.apt_impeg08.Visible = True
    Me.apt_impeg08.VisibleIndex = 84
    '
    'apt_impeg09
    '
    Me.apt_impeg09.AppearanceCell.Options.UseBackColor = True
    Me.apt_impeg09.AppearanceCell.Options.UseTextOptions = True
    Me.apt_impeg09.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_impeg09.Caption = "Impegnato 09"
    Me.apt_impeg09.Enabled = True
    Me.apt_impeg09.FieldName = "apt_impeg09"
    Me.apt_impeg09.Name = "apt_impeg09"
    Me.apt_impeg09.NTSRepositoryComboBox = Nothing
    Me.apt_impeg09.NTSRepositoryItemCheck = Nothing
    Me.apt_impeg09.NTSRepositoryItemMemo = Nothing
    Me.apt_impeg09.NTSRepositoryItemText = Nothing
    Me.apt_impeg09.Visible = True
    Me.apt_impeg09.VisibleIndex = 85
    '
    'apt_impeg10
    '
    Me.apt_impeg10.AppearanceCell.Options.UseBackColor = True
    Me.apt_impeg10.AppearanceCell.Options.UseTextOptions = True
    Me.apt_impeg10.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_impeg10.Caption = "Impegnato 10"
    Me.apt_impeg10.Enabled = True
    Me.apt_impeg10.FieldName = "apt_impeg10"
    Me.apt_impeg10.Name = "apt_impeg10"
    Me.apt_impeg10.NTSRepositoryComboBox = Nothing
    Me.apt_impeg10.NTSRepositoryItemCheck = Nothing
    Me.apt_impeg10.NTSRepositoryItemMemo = Nothing
    Me.apt_impeg10.NTSRepositoryItemText = Nothing
    Me.apt_impeg10.Visible = True
    Me.apt_impeg10.VisibleIndex = 86
    '
    'apt_impeg11
    '
    Me.apt_impeg11.AppearanceCell.Options.UseBackColor = True
    Me.apt_impeg11.AppearanceCell.Options.UseTextOptions = True
    Me.apt_impeg11.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_impeg11.Caption = "Impegnato 11"
    Me.apt_impeg11.Enabled = True
    Me.apt_impeg11.FieldName = "apt_impeg11"
    Me.apt_impeg11.Name = "apt_impeg11"
    Me.apt_impeg11.NTSRepositoryComboBox = Nothing
    Me.apt_impeg11.NTSRepositoryItemCheck = Nothing
    Me.apt_impeg11.NTSRepositoryItemMemo = Nothing
    Me.apt_impeg11.NTSRepositoryItemText = Nothing
    Me.apt_impeg11.Visible = True
    Me.apt_impeg11.VisibleIndex = 87
    '
    'apt_impeg12
    '
    Me.apt_impeg12.AppearanceCell.Options.UseBackColor = True
    Me.apt_impeg12.AppearanceCell.Options.UseTextOptions = True
    Me.apt_impeg12.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_impeg12.Caption = "Impegnato 12"
    Me.apt_impeg12.Enabled = True
    Me.apt_impeg12.FieldName = "apt_impeg12"
    Me.apt_impeg12.Name = "apt_impeg12"
    Me.apt_impeg12.NTSRepositoryComboBox = Nothing
    Me.apt_impeg12.NTSRepositoryItemCheck = Nothing
    Me.apt_impeg12.NTSRepositoryItemMemo = Nothing
    Me.apt_impeg12.NTSRepositoryItemText = Nothing
    Me.apt_impeg12.Visible = True
    Me.apt_impeg12.VisibleIndex = 88
    '
    'apt_impeg13
    '
    Me.apt_impeg13.AppearanceCell.Options.UseBackColor = True
    Me.apt_impeg13.AppearanceCell.Options.UseTextOptions = True
    Me.apt_impeg13.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_impeg13.Caption = "Impegnato 13"
    Me.apt_impeg13.Enabled = True
    Me.apt_impeg13.FieldName = "apt_impeg13"
    Me.apt_impeg13.Name = "apt_impeg13"
    Me.apt_impeg13.NTSRepositoryComboBox = Nothing
    Me.apt_impeg13.NTSRepositoryItemCheck = Nothing
    Me.apt_impeg13.NTSRepositoryItemMemo = Nothing
    Me.apt_impeg13.NTSRepositoryItemText = Nothing
    Me.apt_impeg13.Visible = True
    Me.apt_impeg13.VisibleIndex = 89
    '
    'apt_impeg14
    '
    Me.apt_impeg14.AppearanceCell.Options.UseBackColor = True
    Me.apt_impeg14.AppearanceCell.Options.UseTextOptions = True
    Me.apt_impeg14.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_impeg14.Caption = "Impegnato 14"
    Me.apt_impeg14.Enabled = True
    Me.apt_impeg14.FieldName = "apt_impeg14"
    Me.apt_impeg14.Name = "apt_impeg14"
    Me.apt_impeg14.NTSRepositoryComboBox = Nothing
    Me.apt_impeg14.NTSRepositoryItemCheck = Nothing
    Me.apt_impeg14.NTSRepositoryItemMemo = Nothing
    Me.apt_impeg14.NTSRepositoryItemText = Nothing
    Me.apt_impeg14.Visible = True
    Me.apt_impeg14.VisibleIndex = 90
    '
    'apt_impeg15
    '
    Me.apt_impeg15.AppearanceCell.Options.UseBackColor = True
    Me.apt_impeg15.AppearanceCell.Options.UseTextOptions = True
    Me.apt_impeg15.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_impeg15.Caption = "Impegnato 15"
    Me.apt_impeg15.Enabled = True
    Me.apt_impeg15.FieldName = "apt_impeg15"
    Me.apt_impeg15.Name = "apt_impeg15"
    Me.apt_impeg15.NTSRepositoryComboBox = Nothing
    Me.apt_impeg15.NTSRepositoryItemCheck = Nothing
    Me.apt_impeg15.NTSRepositoryItemMemo = Nothing
    Me.apt_impeg15.NTSRepositoryItemText = Nothing
    Me.apt_impeg15.Visible = True
    Me.apt_impeg15.VisibleIndex = 91
    '
    'apt_impeg16
    '
    Me.apt_impeg16.AppearanceCell.Options.UseBackColor = True
    Me.apt_impeg16.AppearanceCell.Options.UseTextOptions = True
    Me.apt_impeg16.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_impeg16.Caption = "Impegnato 16"
    Me.apt_impeg16.Enabled = True
    Me.apt_impeg16.FieldName = "apt_impeg16"
    Me.apt_impeg16.Name = "apt_impeg16"
    Me.apt_impeg16.NTSRepositoryComboBox = Nothing
    Me.apt_impeg16.NTSRepositoryItemCheck = Nothing
    Me.apt_impeg16.NTSRepositoryItemMemo = Nothing
    Me.apt_impeg16.NTSRepositoryItemText = Nothing
    Me.apt_impeg16.Visible = True
    Me.apt_impeg16.VisibleIndex = 92
    '
    'apt_impeg17
    '
    Me.apt_impeg17.AppearanceCell.Options.UseBackColor = True
    Me.apt_impeg17.AppearanceCell.Options.UseTextOptions = True
    Me.apt_impeg17.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_impeg17.Caption = "Impegnato 17"
    Me.apt_impeg17.Enabled = True
    Me.apt_impeg17.FieldName = "apt_impeg17"
    Me.apt_impeg17.Name = "apt_impeg17"
    Me.apt_impeg17.NTSRepositoryComboBox = Nothing
    Me.apt_impeg17.NTSRepositoryItemCheck = Nothing
    Me.apt_impeg17.NTSRepositoryItemMemo = Nothing
    Me.apt_impeg17.NTSRepositoryItemText = Nothing
    Me.apt_impeg17.Visible = True
    Me.apt_impeg17.VisibleIndex = 93
    '
    'apt_impeg18
    '
    Me.apt_impeg18.AppearanceCell.Options.UseBackColor = True
    Me.apt_impeg18.AppearanceCell.Options.UseTextOptions = True
    Me.apt_impeg18.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_impeg18.Caption = "Impegnato 18"
    Me.apt_impeg18.Enabled = True
    Me.apt_impeg18.FieldName = "apt_impeg18"
    Me.apt_impeg18.Name = "apt_impeg18"
    Me.apt_impeg18.NTSRepositoryComboBox = Nothing
    Me.apt_impeg18.NTSRepositoryItemCheck = Nothing
    Me.apt_impeg18.NTSRepositoryItemMemo = Nothing
    Me.apt_impeg18.NTSRepositoryItemText = Nothing
    Me.apt_impeg18.Visible = True
    Me.apt_impeg18.VisibleIndex = 94
    '
    'apt_impeg19
    '
    Me.apt_impeg19.AppearanceCell.Options.UseBackColor = True
    Me.apt_impeg19.AppearanceCell.Options.UseTextOptions = True
    Me.apt_impeg19.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_impeg19.Caption = "Impegnato 19"
    Me.apt_impeg19.Enabled = True
    Me.apt_impeg19.FieldName = "apt_impeg19"
    Me.apt_impeg19.Name = "apt_impeg19"
    Me.apt_impeg19.NTSRepositoryComboBox = Nothing
    Me.apt_impeg19.NTSRepositoryItemCheck = Nothing
    Me.apt_impeg19.NTSRepositoryItemMemo = Nothing
    Me.apt_impeg19.NTSRepositoryItemText = Nothing
    Me.apt_impeg19.Visible = True
    Me.apt_impeg19.VisibleIndex = 95
    '
    'apt_impeg20
    '
    Me.apt_impeg20.AppearanceCell.Options.UseBackColor = True
    Me.apt_impeg20.AppearanceCell.Options.UseTextOptions = True
    Me.apt_impeg20.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_impeg20.Caption = "Impegnato 20"
    Me.apt_impeg20.Enabled = True
    Me.apt_impeg20.FieldName = "apt_impeg20"
    Me.apt_impeg20.Name = "apt_impeg20"
    Me.apt_impeg20.NTSRepositoryComboBox = Nothing
    Me.apt_impeg20.NTSRepositoryItemCheck = Nothing
    Me.apt_impeg20.NTSRepositoryItemMemo = Nothing
    Me.apt_impeg20.NTSRepositoryItemText = Nothing
    Me.apt_impeg20.Visible = True
    Me.apt_impeg20.VisibleIndex = 96
    '
    'apt_impeg21
    '
    Me.apt_impeg21.AppearanceCell.Options.UseBackColor = True
    Me.apt_impeg21.AppearanceCell.Options.UseTextOptions = True
    Me.apt_impeg21.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_impeg21.Caption = "Impegnato 21"
    Me.apt_impeg21.Enabled = True
    Me.apt_impeg21.FieldName = "apt_impeg21"
    Me.apt_impeg21.Name = "apt_impeg21"
    Me.apt_impeg21.NTSRepositoryComboBox = Nothing
    Me.apt_impeg21.NTSRepositoryItemCheck = Nothing
    Me.apt_impeg21.NTSRepositoryItemMemo = Nothing
    Me.apt_impeg21.NTSRepositoryItemText = Nothing
    Me.apt_impeg21.Visible = True
    Me.apt_impeg21.VisibleIndex = 97
    '
    'apt_impeg22
    '
    Me.apt_impeg22.AppearanceCell.Options.UseBackColor = True
    Me.apt_impeg22.AppearanceCell.Options.UseTextOptions = True
    Me.apt_impeg22.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_impeg22.Caption = "Impegnato 22"
    Me.apt_impeg22.Enabled = True
    Me.apt_impeg22.FieldName = "apt_impeg22"
    Me.apt_impeg22.Name = "apt_impeg22"
    Me.apt_impeg22.NTSRepositoryComboBox = Nothing
    Me.apt_impeg22.NTSRepositoryItemCheck = Nothing
    Me.apt_impeg22.NTSRepositoryItemMemo = Nothing
    Me.apt_impeg22.NTSRepositoryItemText = Nothing
    Me.apt_impeg22.Visible = True
    Me.apt_impeg22.VisibleIndex = 98
    '
    'apt_impeg23
    '
    Me.apt_impeg23.AppearanceCell.Options.UseBackColor = True
    Me.apt_impeg23.AppearanceCell.Options.UseTextOptions = True
    Me.apt_impeg23.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_impeg23.Caption = "Impegnato 23"
    Me.apt_impeg23.Enabled = True
    Me.apt_impeg23.FieldName = "apt_impeg23"
    Me.apt_impeg23.Name = "apt_impeg23"
    Me.apt_impeg23.NTSRepositoryComboBox = Nothing
    Me.apt_impeg23.NTSRepositoryItemCheck = Nothing
    Me.apt_impeg23.NTSRepositoryItemMemo = Nothing
    Me.apt_impeg23.NTSRepositoryItemText = Nothing
    Me.apt_impeg23.Visible = True
    Me.apt_impeg23.VisibleIndex = 99
    '
    'apt_impeg24
    '
    Me.apt_impeg24.AppearanceCell.Options.UseBackColor = True
    Me.apt_impeg24.AppearanceCell.Options.UseTextOptions = True
    Me.apt_impeg24.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_impeg24.Caption = "Impegnato 24"
    Me.apt_impeg24.Enabled = True
    Me.apt_impeg24.FieldName = "apt_impeg24"
    Me.apt_impeg24.Name = "apt_impeg24"
    Me.apt_impeg24.NTSRepositoryComboBox = Nothing
    Me.apt_impeg24.NTSRepositoryItemCheck = Nothing
    Me.apt_impeg24.NTSRepositoryItemMemo = Nothing
    Me.apt_impeg24.NTSRepositoryItemText = Nothing
    Me.apt_impeg24.Visible = True
    Me.apt_impeg24.VisibleIndex = 100
    Me.grvLotc.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.apt_commeca, Me.xx_commeca, Me.apt_lotto, Me.xx_lotto, Me.apt_ubicaz, Me.apt_esist01, Me.apt_esist02, Me.apt_esist03, Me.apt_esist04, Me.apt_esist05, Me.apt_esist06, Me.apt_esist07, Me.apt_esist08, Me.apt_esist09, Me.apt_esist10, Me.apt_esist11, Me.apt_esist12, Me.apt_esist13, Me.apt_esist14, Me.apt_esist15, Me.apt_esist16, Me.apt_esist17, Me.apt_esist18, Me.apt_esist19, Me.apt_esist20, Me.apt_esist21, Me.apt_esist22, Me.apt_esist23, Me.apt_esist24, Me.apt_prenot01, Me.apt_prenot02, Me.apt_prenot03, Me.apt_prenot04, Me.apt_prenot05, Me.apt_prenot06, Me.apt_prenot07, Me.apt_prenot08, Me.apt_prenot09, Me.apt_prenot10, Me.apt_prenot11, Me.apt_prenot12, Me.apt_prenot13, Me.apt_prenot14, Me.apt_prenot15, Me.apt_prenot16, Me.apt_prenot17, Me.apt_prenot18, Me.apt_prenot19, Me.apt_prenot20, Me.apt_prenot21, Me.apt_prenot22, Me.apt_prenot23, Me.apt_prenot24, Me.apt_ordin01, Me.apt_ordin02, Me.apt_ordin03, Me.apt_ordin04, Me.apt_ordin05, Me.apt_ordin06, Me.apt_ordin07, Me.apt_ordin08, Me.apt_ordin09, Me.apt_ordin10, Me.apt_ordin11, Me.apt_ordin12, Me.apt_ordin13, Me.apt_ordin14, Me.apt_ordin15, Me.apt_ordin16, Me.apt_ordin17, Me.apt_ordin18, Me.apt_ordin19, Me.apt_ordin20, Me.apt_ordin21, Me.apt_ordin22, Me.apt_ordin23, Me.apt_ordin24, Me.apt_impeg01, Me.apt_impeg02, Me.apt_impeg03, Me.apt_impeg04, Me.apt_impeg05, Me.apt_impeg06, Me.apt_impeg07, Me.apt_impeg08, Me.apt_impeg09, Me.apt_impeg10, Me.apt_impeg11, Me.apt_impeg12, Me.apt_impeg13, Me.apt_impeg14, Me.apt_impeg15, Me.apt_impeg16, Me.apt_impeg17, Me.apt_impeg18, Me.apt_impeg19, Me.apt_impeg20, Me.apt_impeg21, Me.apt_impeg22, Me.apt_impeg23, Me.apt_impeg24})
    Me.grvLotc.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvLotc.Enabled = True
    Me.grvLotc.GridControl = Me.grLotc
    Me.grvLotc.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvLotc.MinRowHeight = 14
    Me.grvLotc.Name = "grvLotc"
    Me.grvLotc.NTSAllowDelete = True
    Me.grvLotc.NTSAllowInsert = True
    Me.grvLotc.NTSAllowUpdate = True
    Me.grvLotc.NTSMenuContext = Nothing
    Me.grvLotc.OptionsCustomization.AllowRowSizing = True
    Me.grvLotc.OptionsFilter.AllowFilterEditor = False
    Me.grvLotc.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvLotc.OptionsNavigation.UseTabKey = False
    Me.grvLotc.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvLotc.OptionsView.ColumnAutoWidth = False
    Me.grvLotc.OptionsView.EnableAppearanceEvenRow = True
    Me.grvLotc.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvLotc.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvLotc.OptionsView.ShowGroupPanel = False
    Me.grvLotc.RowHeight = 16
    '
    'apt_commeca
    '
    Me.apt_commeca.AppearanceCell.Options.UseBackColor = True
    Me.apt_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.apt_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_commeca.Caption = "Commessa"
    Me.apt_commeca.Enabled = True
    Me.apt_commeca.FieldName = "apt_commeca"
    Me.apt_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_commeca.Name = "apt_commeca"
    Me.apt_commeca.NTSRepositoryComboBox = Nothing
    Me.apt_commeca.NTSRepositoryItemCheck = Nothing
    Me.apt_commeca.NTSRepositoryItemMemo = Nothing
    Me.apt_commeca.NTSRepositoryItemText = Nothing
    Me.apt_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_commeca.OptionsFilter.AllowFilter = False
    Me.apt_commeca.Visible = True
    Me.apt_commeca.VisibleIndex = 0
    '
    'xx_commeca
    '
    Me.xx_commeca.AppearanceCell.Options.UseBackColor = True
    Me.xx_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.xx_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_commeca.Caption = "Descr. commessa"
    Me.xx_commeca.Enabled = True
    Me.xx_commeca.FieldName = "xx_commeca"
    Me.xx_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_commeca.Name = "xx_commeca"
    Me.xx_commeca.NTSRepositoryComboBox = Nothing
    Me.xx_commeca.NTSRepositoryItemCheck = Nothing
    Me.xx_commeca.NTSRepositoryItemMemo = Nothing
    Me.xx_commeca.NTSRepositoryItemText = Nothing
    Me.xx_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_commeca.OptionsFilter.AllowFilter = False
    Me.xx_commeca.Visible = True
    Me.xx_commeca.VisibleIndex = 1
    '
    'apt_lotto
    '
    Me.apt_lotto.AppearanceCell.Options.UseBackColor = True
    Me.apt_lotto.AppearanceCell.Options.UseTextOptions = True
    Me.apt_lotto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_lotto.Caption = "Lotto"
    Me.apt_lotto.Enabled = True
    Me.apt_lotto.FieldName = "apt_lotto"
    Me.apt_lotto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_lotto.Name = "apt_lotto"
    Me.apt_lotto.NTSRepositoryComboBox = Nothing
    Me.apt_lotto.NTSRepositoryItemCheck = Nothing
    Me.apt_lotto.NTSRepositoryItemMemo = Nothing
    Me.apt_lotto.NTSRepositoryItemText = Nothing
    Me.apt_lotto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_lotto.OptionsFilter.AllowFilter = False
    Me.apt_lotto.Visible = True
    Me.apt_lotto.VisibleIndex = 2
    '
    'xx_lotto
    '
    Me.xx_lotto.AppearanceCell.Options.UseBackColor = True
    Me.xx_lotto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_lotto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_lotto.Caption = "Descr. lotto"
    Me.xx_lotto.Enabled = True
    Me.xx_lotto.FieldName = "xx_lotto"
    Me.xx_lotto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_lotto.Name = "xx_lotto"
    Me.xx_lotto.NTSRepositoryComboBox = Nothing
    Me.xx_lotto.NTSRepositoryItemCheck = Nothing
    Me.xx_lotto.NTSRepositoryItemMemo = Nothing
    Me.xx_lotto.NTSRepositoryItemText = Nothing
    Me.xx_lotto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_lotto.OptionsFilter.AllowFilter = False
    Me.xx_lotto.Visible = True
    Me.xx_lotto.VisibleIndex = 3
    '
    'apt_ubicaz
    '
    Me.apt_ubicaz.AppearanceCell.Options.UseBackColor = True
    Me.apt_ubicaz.AppearanceCell.Options.UseTextOptions = True
    Me.apt_ubicaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_ubicaz.Caption = "Ubicazione"
    Me.apt_ubicaz.Enabled = True
    Me.apt_ubicaz.FieldName = "apt_ubicaz"
    Me.apt_ubicaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_ubicaz.Name = "apt_ubicaz"
    Me.apt_ubicaz.NTSRepositoryComboBox = Nothing
    Me.apt_ubicaz.NTSRepositoryItemCheck = Nothing
    Me.apt_ubicaz.NTSRepositoryItemMemo = Nothing
    Me.apt_ubicaz.NTSRepositoryItemText = Nothing
    Me.apt_ubicaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_ubicaz.OptionsFilter.AllowFilter = False
    Me.apt_ubicaz.Visible = True
    Me.apt_ubicaz.VisibleIndex = 4
    '
    'apt_esist01
    '
    Me.apt_esist01.AppearanceCell.Options.UseBackColor = True
    Me.apt_esist01.AppearanceCell.Options.UseTextOptions = True
    Me.apt_esist01.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_esist01.Caption = "Esistenza 01"
    Me.apt_esist01.Enabled = True
    Me.apt_esist01.FieldName = "apt_esist01"
    Me.apt_esist01.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_esist01.Name = "apt_esist01"
    Me.apt_esist01.NTSRepositoryComboBox = Nothing
    Me.apt_esist01.NTSRepositoryItemCheck = Nothing
    Me.apt_esist01.NTSRepositoryItemMemo = Nothing
    Me.apt_esist01.NTSRepositoryItemText = Nothing
    Me.apt_esist01.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_esist01.OptionsFilter.AllowFilter = False
    Me.apt_esist01.Visible = True
    Me.apt_esist01.VisibleIndex = 5
    '
    'apt_esist02
    '
    Me.apt_esist02.AppearanceCell.Options.UseBackColor = True
    Me.apt_esist02.AppearanceCell.Options.UseTextOptions = True
    Me.apt_esist02.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_esist02.Caption = "Esistenza 02"
    Me.apt_esist02.Enabled = True
    Me.apt_esist02.FieldName = "apt_esist02"
    Me.apt_esist02.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_esist02.Name = "apt_esist02"
    Me.apt_esist02.NTSRepositoryComboBox = Nothing
    Me.apt_esist02.NTSRepositoryItemCheck = Nothing
    Me.apt_esist02.NTSRepositoryItemMemo = Nothing
    Me.apt_esist02.NTSRepositoryItemText = Nothing
    Me.apt_esist02.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_esist02.OptionsFilter.AllowFilter = False
    Me.apt_esist02.Visible = True
    Me.apt_esist02.VisibleIndex = 6
    '
    'apt_esist03
    '
    Me.apt_esist03.AppearanceCell.Options.UseBackColor = True
    Me.apt_esist03.AppearanceCell.Options.UseTextOptions = True
    Me.apt_esist03.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_esist03.Caption = "Esistenza 03"
    Me.apt_esist03.Enabled = True
    Me.apt_esist03.FieldName = "apt_esist03"
    Me.apt_esist03.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_esist03.Name = "apt_esist03"
    Me.apt_esist03.NTSRepositoryComboBox = Nothing
    Me.apt_esist03.NTSRepositoryItemCheck = Nothing
    Me.apt_esist03.NTSRepositoryItemMemo = Nothing
    Me.apt_esist03.NTSRepositoryItemText = Nothing
    Me.apt_esist03.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_esist03.OptionsFilter.AllowFilter = False
    Me.apt_esist03.Visible = True
    Me.apt_esist03.VisibleIndex = 7
    '
    'apt_esist04
    '
    Me.apt_esist04.AppearanceCell.Options.UseBackColor = True
    Me.apt_esist04.AppearanceCell.Options.UseTextOptions = True
    Me.apt_esist04.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_esist04.Caption = "Esistenza 04"
    Me.apt_esist04.Enabled = True
    Me.apt_esist04.FieldName = "apt_esist04"
    Me.apt_esist04.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_esist04.Name = "apt_esist04"
    Me.apt_esist04.NTSRepositoryComboBox = Nothing
    Me.apt_esist04.NTSRepositoryItemCheck = Nothing
    Me.apt_esist04.NTSRepositoryItemMemo = Nothing
    Me.apt_esist04.NTSRepositoryItemText = Nothing
    Me.apt_esist04.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_esist04.OptionsFilter.AllowFilter = False
    Me.apt_esist04.Visible = True
    Me.apt_esist04.VisibleIndex = 8
    '
    'apt_esist05
    '
    Me.apt_esist05.AppearanceCell.Options.UseBackColor = True
    Me.apt_esist05.AppearanceCell.Options.UseTextOptions = True
    Me.apt_esist05.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_esist05.Caption = "Esistenza 05"
    Me.apt_esist05.Enabled = True
    Me.apt_esist05.FieldName = "apt_esist05"
    Me.apt_esist05.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_esist05.Name = "apt_esist05"
    Me.apt_esist05.NTSRepositoryComboBox = Nothing
    Me.apt_esist05.NTSRepositoryItemCheck = Nothing
    Me.apt_esist05.NTSRepositoryItemMemo = Nothing
    Me.apt_esist05.NTSRepositoryItemText = Nothing
    Me.apt_esist05.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_esist05.OptionsFilter.AllowFilter = False
    Me.apt_esist05.Visible = True
    Me.apt_esist05.VisibleIndex = 9
    '
    'apt_esist06
    '
    Me.apt_esist06.AppearanceCell.Options.UseBackColor = True
    Me.apt_esist06.AppearanceCell.Options.UseTextOptions = True
    Me.apt_esist06.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_esist06.Caption = "Esistenza 06"
    Me.apt_esist06.Enabled = True
    Me.apt_esist06.FieldName = "apt_esist06"
    Me.apt_esist06.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_esist06.Name = "apt_esist06"
    Me.apt_esist06.NTSRepositoryComboBox = Nothing
    Me.apt_esist06.NTSRepositoryItemCheck = Nothing
    Me.apt_esist06.NTSRepositoryItemMemo = Nothing
    Me.apt_esist06.NTSRepositoryItemText = Nothing
    Me.apt_esist06.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_esist06.OptionsFilter.AllowFilter = False
    Me.apt_esist06.Visible = True
    Me.apt_esist06.VisibleIndex = 10
    '
    'apt_esist07
    '
    Me.apt_esist07.AppearanceCell.Options.UseBackColor = True
    Me.apt_esist07.AppearanceCell.Options.UseTextOptions = True
    Me.apt_esist07.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_esist07.Caption = "Esistenza 07"
    Me.apt_esist07.Enabled = True
    Me.apt_esist07.FieldName = "apt_esist07"
    Me.apt_esist07.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_esist07.Name = "apt_esist07"
    Me.apt_esist07.NTSRepositoryComboBox = Nothing
    Me.apt_esist07.NTSRepositoryItemCheck = Nothing
    Me.apt_esist07.NTSRepositoryItemMemo = Nothing
    Me.apt_esist07.NTSRepositoryItemText = Nothing
    Me.apt_esist07.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_esist07.OptionsFilter.AllowFilter = False
    Me.apt_esist07.Visible = True
    Me.apt_esist07.VisibleIndex = 11
    '
    'apt_esist08
    '
    Me.apt_esist08.AppearanceCell.Options.UseBackColor = True
    Me.apt_esist08.AppearanceCell.Options.UseTextOptions = True
    Me.apt_esist08.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_esist08.Caption = "Esistenza 08"
    Me.apt_esist08.Enabled = True
    Me.apt_esist08.FieldName = "apt_esist08"
    Me.apt_esist08.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_esist08.Name = "apt_esist08"
    Me.apt_esist08.NTSRepositoryComboBox = Nothing
    Me.apt_esist08.NTSRepositoryItemCheck = Nothing
    Me.apt_esist08.NTSRepositoryItemMemo = Nothing
    Me.apt_esist08.NTSRepositoryItemText = Nothing
    Me.apt_esist08.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_esist08.OptionsFilter.AllowFilter = False
    Me.apt_esist08.Visible = True
    Me.apt_esist08.VisibleIndex = 12
    '
    'apt_esist09
    '
    Me.apt_esist09.AppearanceCell.Options.UseBackColor = True
    Me.apt_esist09.AppearanceCell.Options.UseTextOptions = True
    Me.apt_esist09.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_esist09.Caption = "Esistenza 09"
    Me.apt_esist09.Enabled = True
    Me.apt_esist09.FieldName = "apt_esist09"
    Me.apt_esist09.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_esist09.Name = "apt_esist09"
    Me.apt_esist09.NTSRepositoryComboBox = Nothing
    Me.apt_esist09.NTSRepositoryItemCheck = Nothing
    Me.apt_esist09.NTSRepositoryItemMemo = Nothing
    Me.apt_esist09.NTSRepositoryItemText = Nothing
    Me.apt_esist09.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_esist09.OptionsFilter.AllowFilter = False
    Me.apt_esist09.Visible = True
    Me.apt_esist09.VisibleIndex = 13
    '
    'apt_esist10
    '
    Me.apt_esist10.AppearanceCell.Options.UseBackColor = True
    Me.apt_esist10.AppearanceCell.Options.UseTextOptions = True
    Me.apt_esist10.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_esist10.Caption = "Esistenza 10"
    Me.apt_esist10.Enabled = True
    Me.apt_esist10.FieldName = "apt_esist10"
    Me.apt_esist10.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_esist10.Name = "apt_esist10"
    Me.apt_esist10.NTSRepositoryComboBox = Nothing
    Me.apt_esist10.NTSRepositoryItemCheck = Nothing
    Me.apt_esist10.NTSRepositoryItemMemo = Nothing
    Me.apt_esist10.NTSRepositoryItemText = Nothing
    Me.apt_esist10.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_esist10.OptionsFilter.AllowFilter = False
    Me.apt_esist10.Visible = True
    Me.apt_esist10.VisibleIndex = 14
    '
    'apt_esist11
    '
    Me.apt_esist11.AppearanceCell.Options.UseBackColor = True
    Me.apt_esist11.AppearanceCell.Options.UseTextOptions = True
    Me.apt_esist11.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_esist11.Caption = "Esistenza 11"
    Me.apt_esist11.Enabled = True
    Me.apt_esist11.FieldName = "apt_esist11"
    Me.apt_esist11.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_esist11.Name = "apt_esist11"
    Me.apt_esist11.NTSRepositoryComboBox = Nothing
    Me.apt_esist11.NTSRepositoryItemCheck = Nothing
    Me.apt_esist11.NTSRepositoryItemMemo = Nothing
    Me.apt_esist11.NTSRepositoryItemText = Nothing
    Me.apt_esist11.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_esist11.OptionsFilter.AllowFilter = False
    Me.apt_esist11.Visible = True
    Me.apt_esist11.VisibleIndex = 15
    '
    'apt_esist12
    '
    Me.apt_esist12.AppearanceCell.Options.UseBackColor = True
    Me.apt_esist12.AppearanceCell.Options.UseTextOptions = True
    Me.apt_esist12.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_esist12.Caption = "Esistenza 12"
    Me.apt_esist12.Enabled = True
    Me.apt_esist12.FieldName = "apt_esist12"
    Me.apt_esist12.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_esist12.Name = "apt_esist12"
    Me.apt_esist12.NTSRepositoryComboBox = Nothing
    Me.apt_esist12.NTSRepositoryItemCheck = Nothing
    Me.apt_esist12.NTSRepositoryItemMemo = Nothing
    Me.apt_esist12.NTSRepositoryItemText = Nothing
    Me.apt_esist12.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_esist12.OptionsFilter.AllowFilter = False
    Me.apt_esist12.Visible = True
    Me.apt_esist12.VisibleIndex = 16
    '
    'apt_esist13
    '
    Me.apt_esist13.AppearanceCell.Options.UseBackColor = True
    Me.apt_esist13.AppearanceCell.Options.UseTextOptions = True
    Me.apt_esist13.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_esist13.Caption = "Esistenza 13"
    Me.apt_esist13.Enabled = True
    Me.apt_esist13.FieldName = "apt_esist13"
    Me.apt_esist13.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_esist13.Name = "apt_esist13"
    Me.apt_esist13.NTSRepositoryComboBox = Nothing
    Me.apt_esist13.NTSRepositoryItemCheck = Nothing
    Me.apt_esist13.NTSRepositoryItemMemo = Nothing
    Me.apt_esist13.NTSRepositoryItemText = Nothing
    Me.apt_esist13.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_esist13.OptionsFilter.AllowFilter = False
    Me.apt_esist13.Visible = True
    Me.apt_esist13.VisibleIndex = 17
    '
    'apt_esist14
    '
    Me.apt_esist14.AppearanceCell.Options.UseBackColor = True
    Me.apt_esist14.AppearanceCell.Options.UseTextOptions = True
    Me.apt_esist14.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_esist14.Caption = "Esistenza 14"
    Me.apt_esist14.Enabled = True
    Me.apt_esist14.FieldName = "apt_esist14"
    Me.apt_esist14.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_esist14.Name = "apt_esist14"
    Me.apt_esist14.NTSRepositoryComboBox = Nothing
    Me.apt_esist14.NTSRepositoryItemCheck = Nothing
    Me.apt_esist14.NTSRepositoryItemMemo = Nothing
    Me.apt_esist14.NTSRepositoryItemText = Nothing
    Me.apt_esist14.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_esist14.OptionsFilter.AllowFilter = False
    Me.apt_esist14.Visible = True
    Me.apt_esist14.VisibleIndex = 18
    '
    'apt_esist15
    '
    Me.apt_esist15.AppearanceCell.Options.UseBackColor = True
    Me.apt_esist15.AppearanceCell.Options.UseTextOptions = True
    Me.apt_esist15.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_esist15.Caption = "Esistenza 15"
    Me.apt_esist15.Enabled = True
    Me.apt_esist15.FieldName = "apt_esist15"
    Me.apt_esist15.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_esist15.Name = "apt_esist15"
    Me.apt_esist15.NTSRepositoryComboBox = Nothing
    Me.apt_esist15.NTSRepositoryItemCheck = Nothing
    Me.apt_esist15.NTSRepositoryItemMemo = Nothing
    Me.apt_esist15.NTSRepositoryItemText = Nothing
    Me.apt_esist15.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_esist15.OptionsFilter.AllowFilter = False
    Me.apt_esist15.Visible = True
    Me.apt_esist15.VisibleIndex = 19
    '
    'apt_esist16
    '
    Me.apt_esist16.AppearanceCell.Options.UseBackColor = True
    Me.apt_esist16.AppearanceCell.Options.UseTextOptions = True
    Me.apt_esist16.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_esist16.Caption = "Esistenza 16"
    Me.apt_esist16.Enabled = True
    Me.apt_esist16.FieldName = "apt_esist16"
    Me.apt_esist16.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_esist16.Name = "apt_esist16"
    Me.apt_esist16.NTSRepositoryComboBox = Nothing
    Me.apt_esist16.NTSRepositoryItemCheck = Nothing
    Me.apt_esist16.NTSRepositoryItemMemo = Nothing
    Me.apt_esist16.NTSRepositoryItemText = Nothing
    Me.apt_esist16.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_esist16.OptionsFilter.AllowFilter = False
    Me.apt_esist16.Visible = True
    Me.apt_esist16.VisibleIndex = 20
    '
    'apt_esist17
    '
    Me.apt_esist17.AppearanceCell.Options.UseBackColor = True
    Me.apt_esist17.AppearanceCell.Options.UseTextOptions = True
    Me.apt_esist17.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_esist17.Caption = "Esistenza 17"
    Me.apt_esist17.Enabled = True
    Me.apt_esist17.FieldName = "apt_esist17"
    Me.apt_esist17.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_esist17.Name = "apt_esist17"
    Me.apt_esist17.NTSRepositoryComboBox = Nothing
    Me.apt_esist17.NTSRepositoryItemCheck = Nothing
    Me.apt_esist17.NTSRepositoryItemMemo = Nothing
    Me.apt_esist17.NTSRepositoryItemText = Nothing
    Me.apt_esist17.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_esist17.OptionsFilter.AllowFilter = False
    Me.apt_esist17.Visible = True
    Me.apt_esist17.VisibleIndex = 21
    '
    'apt_esist18
    '
    Me.apt_esist18.AppearanceCell.Options.UseBackColor = True
    Me.apt_esist18.AppearanceCell.Options.UseTextOptions = True
    Me.apt_esist18.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_esist18.Caption = "Esistenza 18"
    Me.apt_esist18.Enabled = True
    Me.apt_esist18.FieldName = "apt_esist18"
    Me.apt_esist18.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_esist18.Name = "apt_esist18"
    Me.apt_esist18.NTSRepositoryComboBox = Nothing
    Me.apt_esist18.NTSRepositoryItemCheck = Nothing
    Me.apt_esist18.NTSRepositoryItemMemo = Nothing
    Me.apt_esist18.NTSRepositoryItemText = Nothing
    Me.apt_esist18.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_esist18.OptionsFilter.AllowFilter = False
    Me.apt_esist18.Visible = True
    Me.apt_esist18.VisibleIndex = 22
    '
    'apt_esist19
    '
    Me.apt_esist19.AppearanceCell.Options.UseBackColor = True
    Me.apt_esist19.AppearanceCell.Options.UseTextOptions = True
    Me.apt_esist19.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_esist19.Caption = "Esistenza 19"
    Me.apt_esist19.Enabled = True
    Me.apt_esist19.FieldName = "apt_esist19"
    Me.apt_esist19.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_esist19.Name = "apt_esist19"
    Me.apt_esist19.NTSRepositoryComboBox = Nothing
    Me.apt_esist19.NTSRepositoryItemCheck = Nothing
    Me.apt_esist19.NTSRepositoryItemMemo = Nothing
    Me.apt_esist19.NTSRepositoryItemText = Nothing
    Me.apt_esist19.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_esist19.OptionsFilter.AllowFilter = False
    Me.apt_esist19.Visible = True
    Me.apt_esist19.VisibleIndex = 23
    '
    'apt_esist20
    '
    Me.apt_esist20.AppearanceCell.Options.UseBackColor = True
    Me.apt_esist20.AppearanceCell.Options.UseTextOptions = True
    Me.apt_esist20.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_esist20.Caption = "Esistenza 20"
    Me.apt_esist20.Enabled = True
    Me.apt_esist20.FieldName = "apt_esist20"
    Me.apt_esist20.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_esist20.Name = "apt_esist20"
    Me.apt_esist20.NTSRepositoryComboBox = Nothing
    Me.apt_esist20.NTSRepositoryItemCheck = Nothing
    Me.apt_esist20.NTSRepositoryItemMemo = Nothing
    Me.apt_esist20.NTSRepositoryItemText = Nothing
    Me.apt_esist20.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_esist20.OptionsFilter.AllowFilter = False
    Me.apt_esist20.Visible = True
    Me.apt_esist20.VisibleIndex = 24
    '
    'apt_esist21
    '
    Me.apt_esist21.AppearanceCell.Options.UseBackColor = True
    Me.apt_esist21.AppearanceCell.Options.UseTextOptions = True
    Me.apt_esist21.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_esist21.Caption = "Esistenza 21"
    Me.apt_esist21.Enabled = True
    Me.apt_esist21.FieldName = "apt_esist21"
    Me.apt_esist21.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_esist21.Name = "apt_esist21"
    Me.apt_esist21.NTSRepositoryComboBox = Nothing
    Me.apt_esist21.NTSRepositoryItemCheck = Nothing
    Me.apt_esist21.NTSRepositoryItemMemo = Nothing
    Me.apt_esist21.NTSRepositoryItemText = Nothing
    Me.apt_esist21.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_esist21.OptionsFilter.AllowFilter = False
    Me.apt_esist21.Visible = True
    Me.apt_esist21.VisibleIndex = 25
    '
    'apt_esist22
    '
    Me.apt_esist22.AppearanceCell.Options.UseBackColor = True
    Me.apt_esist22.AppearanceCell.Options.UseTextOptions = True
    Me.apt_esist22.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_esist22.Caption = "Esistenza 22"
    Me.apt_esist22.Enabled = True
    Me.apt_esist22.FieldName = "apt_esist22"
    Me.apt_esist22.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_esist22.Name = "apt_esist22"
    Me.apt_esist22.NTSRepositoryComboBox = Nothing
    Me.apt_esist22.NTSRepositoryItemCheck = Nothing
    Me.apt_esist22.NTSRepositoryItemMemo = Nothing
    Me.apt_esist22.NTSRepositoryItemText = Nothing
    Me.apt_esist22.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_esist22.OptionsFilter.AllowFilter = False
    Me.apt_esist22.Visible = True
    Me.apt_esist22.VisibleIndex = 26
    '
    'apt_esist23
    '
    Me.apt_esist23.AppearanceCell.Options.UseBackColor = True
    Me.apt_esist23.AppearanceCell.Options.UseTextOptions = True
    Me.apt_esist23.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_esist23.Caption = "Esistenza 23"
    Me.apt_esist23.Enabled = True
    Me.apt_esist23.FieldName = "apt_esist23"
    Me.apt_esist23.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_esist23.Name = "apt_esist23"
    Me.apt_esist23.NTSRepositoryComboBox = Nothing
    Me.apt_esist23.NTSRepositoryItemCheck = Nothing
    Me.apt_esist23.NTSRepositoryItemMemo = Nothing
    Me.apt_esist23.NTSRepositoryItemText = Nothing
    Me.apt_esist23.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_esist23.OptionsFilter.AllowFilter = False
    Me.apt_esist23.Visible = True
    Me.apt_esist23.VisibleIndex = 27
    '
    'apt_esist24
    '
    Me.apt_esist24.AppearanceCell.Options.UseBackColor = True
    Me.apt_esist24.AppearanceCell.Options.UseTextOptions = True
    Me.apt_esist24.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_esist24.Caption = "Esistenza 24"
    Me.apt_esist24.Enabled = True
    Me.apt_esist24.FieldName = "apt_esist24"
    Me.apt_esist24.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_esist24.Name = "apt_esist24"
    Me.apt_esist24.NTSRepositoryComboBox = Nothing
    Me.apt_esist24.NTSRepositoryItemCheck = Nothing
    Me.apt_esist24.NTSRepositoryItemMemo = Nothing
    Me.apt_esist24.NTSRepositoryItemText = Nothing
    Me.apt_esist24.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_esist24.OptionsFilter.AllowFilter = False
    Me.apt_esist24.Visible = True
    Me.apt_esist24.VisibleIndex = 28
    '
    'apt_prenot01
    '
    Me.apt_prenot01.AppearanceCell.Options.UseBackColor = True
    Me.apt_prenot01.AppearanceCell.Options.UseTextOptions = True
    Me.apt_prenot01.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_prenot01.Caption = "Prenotato 01"
    Me.apt_prenot01.Enabled = True
    Me.apt_prenot01.FieldName = "apt_prenot01"
    Me.apt_prenot01.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_prenot01.Name = "apt_prenot01"
    Me.apt_prenot01.NTSRepositoryComboBox = Nothing
    Me.apt_prenot01.NTSRepositoryItemCheck = Nothing
    Me.apt_prenot01.NTSRepositoryItemMemo = Nothing
    Me.apt_prenot01.NTSRepositoryItemText = Nothing
    Me.apt_prenot01.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_prenot01.OptionsFilter.AllowFilter = False
    Me.apt_prenot01.Visible = True
    Me.apt_prenot01.VisibleIndex = 29
    '
    'apt_prenot02
    '
    Me.apt_prenot02.AppearanceCell.Options.UseBackColor = True
    Me.apt_prenot02.AppearanceCell.Options.UseTextOptions = True
    Me.apt_prenot02.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_prenot02.Caption = "Prenotato 02"
    Me.apt_prenot02.Enabled = True
    Me.apt_prenot02.FieldName = "apt_prenot02"
    Me.apt_prenot02.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_prenot02.Name = "apt_prenot02"
    Me.apt_prenot02.NTSRepositoryComboBox = Nothing
    Me.apt_prenot02.NTSRepositoryItemCheck = Nothing
    Me.apt_prenot02.NTSRepositoryItemMemo = Nothing
    Me.apt_prenot02.NTSRepositoryItemText = Nothing
    Me.apt_prenot02.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_prenot02.OptionsFilter.AllowFilter = False
    Me.apt_prenot02.Visible = True
    Me.apt_prenot02.VisibleIndex = 30
    '
    'apt_prenot03
    '
    Me.apt_prenot03.AppearanceCell.Options.UseBackColor = True
    Me.apt_prenot03.AppearanceCell.Options.UseTextOptions = True
    Me.apt_prenot03.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_prenot03.Caption = "Prenotato 03"
    Me.apt_prenot03.Enabled = True
    Me.apt_prenot03.FieldName = "apt_prenot03"
    Me.apt_prenot03.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_prenot03.Name = "apt_prenot03"
    Me.apt_prenot03.NTSRepositoryComboBox = Nothing
    Me.apt_prenot03.NTSRepositoryItemCheck = Nothing
    Me.apt_prenot03.NTSRepositoryItemMemo = Nothing
    Me.apt_prenot03.NTSRepositoryItemText = Nothing
    Me.apt_prenot03.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_prenot03.OptionsFilter.AllowFilter = False
    Me.apt_prenot03.Visible = True
    Me.apt_prenot03.VisibleIndex = 31
    '
    'apt_prenot04
    '
    Me.apt_prenot04.AppearanceCell.Options.UseBackColor = True
    Me.apt_prenot04.AppearanceCell.Options.UseTextOptions = True
    Me.apt_prenot04.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_prenot04.Caption = "Prenotato 04"
    Me.apt_prenot04.Enabled = True
    Me.apt_prenot04.FieldName = "apt_prenot04"
    Me.apt_prenot04.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_prenot04.Name = "apt_prenot04"
    Me.apt_prenot04.NTSRepositoryComboBox = Nothing
    Me.apt_prenot04.NTSRepositoryItemCheck = Nothing
    Me.apt_prenot04.NTSRepositoryItemMemo = Nothing
    Me.apt_prenot04.NTSRepositoryItemText = Nothing
    Me.apt_prenot04.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_prenot04.OptionsFilter.AllowFilter = False
    Me.apt_prenot04.Visible = True
    Me.apt_prenot04.VisibleIndex = 32
    '
    'apt_prenot05
    '
    Me.apt_prenot05.AppearanceCell.Options.UseBackColor = True
    Me.apt_prenot05.AppearanceCell.Options.UseTextOptions = True
    Me.apt_prenot05.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_prenot05.Caption = "Prenotato 05"
    Me.apt_prenot05.Enabled = True
    Me.apt_prenot05.FieldName = "apt_prenot05"
    Me.apt_prenot05.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_prenot05.Name = "apt_prenot05"
    Me.apt_prenot05.NTSRepositoryComboBox = Nothing
    Me.apt_prenot05.NTSRepositoryItemCheck = Nothing
    Me.apt_prenot05.NTSRepositoryItemMemo = Nothing
    Me.apt_prenot05.NTSRepositoryItemText = Nothing
    Me.apt_prenot05.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_prenot05.OptionsFilter.AllowFilter = False
    Me.apt_prenot05.Visible = True
    Me.apt_prenot05.VisibleIndex = 33
    '
    'apt_prenot06
    '
    Me.apt_prenot06.AppearanceCell.Options.UseBackColor = True
    Me.apt_prenot06.AppearanceCell.Options.UseTextOptions = True
    Me.apt_prenot06.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_prenot06.Caption = "Prenotato 06"
    Me.apt_prenot06.Enabled = True
    Me.apt_prenot06.FieldName = "apt_prenot06"
    Me.apt_prenot06.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_prenot06.Name = "apt_prenot06"
    Me.apt_prenot06.NTSRepositoryComboBox = Nothing
    Me.apt_prenot06.NTSRepositoryItemCheck = Nothing
    Me.apt_prenot06.NTSRepositoryItemMemo = Nothing
    Me.apt_prenot06.NTSRepositoryItemText = Nothing
    Me.apt_prenot06.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_prenot06.OptionsFilter.AllowFilter = False
    Me.apt_prenot06.Visible = True
    Me.apt_prenot06.VisibleIndex = 34
    '
    'apt_prenot07
    '
    Me.apt_prenot07.AppearanceCell.Options.UseBackColor = True
    Me.apt_prenot07.AppearanceCell.Options.UseTextOptions = True
    Me.apt_prenot07.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_prenot07.Caption = "Prenotato 07"
    Me.apt_prenot07.Enabled = True
    Me.apt_prenot07.FieldName = "apt_prenot07"
    Me.apt_prenot07.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_prenot07.Name = "apt_prenot07"
    Me.apt_prenot07.NTSRepositoryComboBox = Nothing
    Me.apt_prenot07.NTSRepositoryItemCheck = Nothing
    Me.apt_prenot07.NTSRepositoryItemMemo = Nothing
    Me.apt_prenot07.NTSRepositoryItemText = Nothing
    Me.apt_prenot07.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_prenot07.OptionsFilter.AllowFilter = False
    Me.apt_prenot07.Visible = True
    Me.apt_prenot07.VisibleIndex = 35
    '
    'apt_prenot08
    '
    Me.apt_prenot08.AppearanceCell.Options.UseBackColor = True
    Me.apt_prenot08.AppearanceCell.Options.UseTextOptions = True
    Me.apt_prenot08.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_prenot08.Caption = "Prenotato 08"
    Me.apt_prenot08.Enabled = True
    Me.apt_prenot08.FieldName = "apt_prenot08"
    Me.apt_prenot08.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_prenot08.Name = "apt_prenot08"
    Me.apt_prenot08.NTSRepositoryComboBox = Nothing
    Me.apt_prenot08.NTSRepositoryItemCheck = Nothing
    Me.apt_prenot08.NTSRepositoryItemMemo = Nothing
    Me.apt_prenot08.NTSRepositoryItemText = Nothing
    Me.apt_prenot08.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_prenot08.OptionsFilter.AllowFilter = False
    Me.apt_prenot08.Visible = True
    Me.apt_prenot08.VisibleIndex = 36
    '
    'apt_prenot09
    '
    Me.apt_prenot09.AppearanceCell.Options.UseBackColor = True
    Me.apt_prenot09.AppearanceCell.Options.UseTextOptions = True
    Me.apt_prenot09.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_prenot09.Caption = "Prenotato 09"
    Me.apt_prenot09.Enabled = True
    Me.apt_prenot09.FieldName = "apt_prenot09"
    Me.apt_prenot09.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_prenot09.Name = "apt_prenot09"
    Me.apt_prenot09.NTSRepositoryComboBox = Nothing
    Me.apt_prenot09.NTSRepositoryItemCheck = Nothing
    Me.apt_prenot09.NTSRepositoryItemMemo = Nothing
    Me.apt_prenot09.NTSRepositoryItemText = Nothing
    Me.apt_prenot09.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_prenot09.OptionsFilter.AllowFilter = False
    Me.apt_prenot09.Visible = True
    Me.apt_prenot09.VisibleIndex = 37
    '
    'apt_prenot10
    '
    Me.apt_prenot10.AppearanceCell.Options.UseBackColor = True
    Me.apt_prenot10.AppearanceCell.Options.UseTextOptions = True
    Me.apt_prenot10.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_prenot10.Caption = "Prenotato 10"
    Me.apt_prenot10.Enabled = True
    Me.apt_prenot10.FieldName = "apt_prenot10"
    Me.apt_prenot10.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_prenot10.Name = "apt_prenot10"
    Me.apt_prenot10.NTSRepositoryComboBox = Nothing
    Me.apt_prenot10.NTSRepositoryItemCheck = Nothing
    Me.apt_prenot10.NTSRepositoryItemMemo = Nothing
    Me.apt_prenot10.NTSRepositoryItemText = Nothing
    Me.apt_prenot10.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_prenot10.OptionsFilter.AllowFilter = False
    Me.apt_prenot10.Visible = True
    Me.apt_prenot10.VisibleIndex = 38
    '
    'apt_prenot11
    '
    Me.apt_prenot11.AppearanceCell.Options.UseBackColor = True
    Me.apt_prenot11.AppearanceCell.Options.UseTextOptions = True
    Me.apt_prenot11.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_prenot11.Caption = "Prenotato 11"
    Me.apt_prenot11.Enabled = True
    Me.apt_prenot11.FieldName = "apt_prenot11"
    Me.apt_prenot11.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_prenot11.Name = "apt_prenot11"
    Me.apt_prenot11.NTSRepositoryComboBox = Nothing
    Me.apt_prenot11.NTSRepositoryItemCheck = Nothing
    Me.apt_prenot11.NTSRepositoryItemMemo = Nothing
    Me.apt_prenot11.NTSRepositoryItemText = Nothing
    Me.apt_prenot11.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_prenot11.OptionsFilter.AllowFilter = False
    Me.apt_prenot11.Visible = True
    Me.apt_prenot11.VisibleIndex = 39
    '
    'apt_prenot12
    '
    Me.apt_prenot12.AppearanceCell.Options.UseBackColor = True
    Me.apt_prenot12.AppearanceCell.Options.UseTextOptions = True
    Me.apt_prenot12.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_prenot12.Caption = "Prenotato 12"
    Me.apt_prenot12.Enabled = True
    Me.apt_prenot12.FieldName = "apt_prenot12"
    Me.apt_prenot12.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_prenot12.Name = "apt_prenot12"
    Me.apt_prenot12.NTSRepositoryComboBox = Nothing
    Me.apt_prenot12.NTSRepositoryItemCheck = Nothing
    Me.apt_prenot12.NTSRepositoryItemMemo = Nothing
    Me.apt_prenot12.NTSRepositoryItemText = Nothing
    Me.apt_prenot12.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_prenot12.OptionsFilter.AllowFilter = False
    Me.apt_prenot12.Visible = True
    Me.apt_prenot12.VisibleIndex = 40
    '
    'apt_prenot13
    '
    Me.apt_prenot13.AppearanceCell.Options.UseBackColor = True
    Me.apt_prenot13.AppearanceCell.Options.UseTextOptions = True
    Me.apt_prenot13.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_prenot13.Caption = "Prenotato 13"
    Me.apt_prenot13.Enabled = True
    Me.apt_prenot13.FieldName = "apt_prenot13"
    Me.apt_prenot13.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_prenot13.Name = "apt_prenot13"
    Me.apt_prenot13.NTSRepositoryComboBox = Nothing
    Me.apt_prenot13.NTSRepositoryItemCheck = Nothing
    Me.apt_prenot13.NTSRepositoryItemMemo = Nothing
    Me.apt_prenot13.NTSRepositoryItemText = Nothing
    Me.apt_prenot13.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_prenot13.OptionsFilter.AllowFilter = False
    Me.apt_prenot13.Visible = True
    Me.apt_prenot13.VisibleIndex = 41
    '
    'apt_prenot14
    '
    Me.apt_prenot14.AppearanceCell.Options.UseBackColor = True
    Me.apt_prenot14.AppearanceCell.Options.UseTextOptions = True
    Me.apt_prenot14.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_prenot14.Caption = "Prenotato 14"
    Me.apt_prenot14.Enabled = True
    Me.apt_prenot14.FieldName = "apt_prenot14"
    Me.apt_prenot14.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_prenot14.Name = "apt_prenot14"
    Me.apt_prenot14.NTSRepositoryComboBox = Nothing
    Me.apt_prenot14.NTSRepositoryItemCheck = Nothing
    Me.apt_prenot14.NTSRepositoryItemMemo = Nothing
    Me.apt_prenot14.NTSRepositoryItemText = Nothing
    Me.apt_prenot14.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_prenot14.OptionsFilter.AllowFilter = False
    Me.apt_prenot14.Visible = True
    Me.apt_prenot14.VisibleIndex = 42
    '
    'apt_prenot15
    '
    Me.apt_prenot15.AppearanceCell.Options.UseBackColor = True
    Me.apt_prenot15.AppearanceCell.Options.UseTextOptions = True
    Me.apt_prenot15.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_prenot15.Caption = "Prenotato 15"
    Me.apt_prenot15.Enabled = True
    Me.apt_prenot15.FieldName = "apt_prenot15"
    Me.apt_prenot15.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_prenot15.Name = "apt_prenot15"
    Me.apt_prenot15.NTSRepositoryComboBox = Nothing
    Me.apt_prenot15.NTSRepositoryItemCheck = Nothing
    Me.apt_prenot15.NTSRepositoryItemMemo = Nothing
    Me.apt_prenot15.NTSRepositoryItemText = Nothing
    Me.apt_prenot15.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_prenot15.OptionsFilter.AllowFilter = False
    Me.apt_prenot15.Visible = True
    Me.apt_prenot15.VisibleIndex = 43
    '
    'apt_prenot16
    '
    Me.apt_prenot16.AppearanceCell.Options.UseBackColor = True
    Me.apt_prenot16.AppearanceCell.Options.UseTextOptions = True
    Me.apt_prenot16.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_prenot16.Caption = "Prenotato 16"
    Me.apt_prenot16.Enabled = True
    Me.apt_prenot16.FieldName = "apt_prenot16"
    Me.apt_prenot16.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_prenot16.Name = "apt_prenot16"
    Me.apt_prenot16.NTSRepositoryComboBox = Nothing
    Me.apt_prenot16.NTSRepositoryItemCheck = Nothing
    Me.apt_prenot16.NTSRepositoryItemMemo = Nothing
    Me.apt_prenot16.NTSRepositoryItemText = Nothing
    Me.apt_prenot16.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_prenot16.OptionsFilter.AllowFilter = False
    Me.apt_prenot16.Visible = True
    Me.apt_prenot16.VisibleIndex = 44
    '
    'apt_prenot17
    '
    Me.apt_prenot17.AppearanceCell.Options.UseBackColor = True
    Me.apt_prenot17.AppearanceCell.Options.UseTextOptions = True
    Me.apt_prenot17.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_prenot17.Caption = "Prenotato 17"
    Me.apt_prenot17.Enabled = True
    Me.apt_prenot17.FieldName = "apt_prenot17"
    Me.apt_prenot17.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_prenot17.Name = "apt_prenot17"
    Me.apt_prenot17.NTSRepositoryComboBox = Nothing
    Me.apt_prenot17.NTSRepositoryItemCheck = Nothing
    Me.apt_prenot17.NTSRepositoryItemMemo = Nothing
    Me.apt_prenot17.NTSRepositoryItemText = Nothing
    Me.apt_prenot17.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_prenot17.OptionsFilter.AllowFilter = False
    Me.apt_prenot17.Visible = True
    Me.apt_prenot17.VisibleIndex = 45
    '
    'apt_prenot18
    '
    Me.apt_prenot18.AppearanceCell.Options.UseBackColor = True
    Me.apt_prenot18.AppearanceCell.Options.UseTextOptions = True
    Me.apt_prenot18.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_prenot18.Caption = "Prenotato 18"
    Me.apt_prenot18.Enabled = True
    Me.apt_prenot18.FieldName = "apt_prenot18"
    Me.apt_prenot18.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_prenot18.Name = "apt_prenot18"
    Me.apt_prenot18.NTSRepositoryComboBox = Nothing
    Me.apt_prenot18.NTSRepositoryItemCheck = Nothing
    Me.apt_prenot18.NTSRepositoryItemMemo = Nothing
    Me.apt_prenot18.NTSRepositoryItemText = Nothing
    Me.apt_prenot18.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_prenot18.OptionsFilter.AllowFilter = False
    Me.apt_prenot18.Visible = True
    Me.apt_prenot18.VisibleIndex = 46
    '
    'apt_prenot19
    '
    Me.apt_prenot19.AppearanceCell.Options.UseBackColor = True
    Me.apt_prenot19.AppearanceCell.Options.UseTextOptions = True
    Me.apt_prenot19.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_prenot19.Caption = "Prenotato 19"
    Me.apt_prenot19.Enabled = True
    Me.apt_prenot19.FieldName = "apt_prenot19"
    Me.apt_prenot19.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_prenot19.Name = "apt_prenot19"
    Me.apt_prenot19.NTSRepositoryComboBox = Nothing
    Me.apt_prenot19.NTSRepositoryItemCheck = Nothing
    Me.apt_prenot19.NTSRepositoryItemMemo = Nothing
    Me.apt_prenot19.NTSRepositoryItemText = Nothing
    Me.apt_prenot19.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_prenot19.OptionsFilter.AllowFilter = False
    Me.apt_prenot19.Visible = True
    Me.apt_prenot19.VisibleIndex = 47
    '
    'apt_prenot20
    '
    Me.apt_prenot20.AppearanceCell.Options.UseBackColor = True
    Me.apt_prenot20.AppearanceCell.Options.UseTextOptions = True
    Me.apt_prenot20.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_prenot20.Caption = "Prenotato 20"
    Me.apt_prenot20.Enabled = True
    Me.apt_prenot20.FieldName = "apt_prenot20"
    Me.apt_prenot20.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_prenot20.Name = "apt_prenot20"
    Me.apt_prenot20.NTSRepositoryComboBox = Nothing
    Me.apt_prenot20.NTSRepositoryItemCheck = Nothing
    Me.apt_prenot20.NTSRepositoryItemMemo = Nothing
    Me.apt_prenot20.NTSRepositoryItemText = Nothing
    Me.apt_prenot20.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_prenot20.OptionsFilter.AllowFilter = False
    Me.apt_prenot20.Visible = True
    Me.apt_prenot20.VisibleIndex = 48
    '
    'apt_prenot21
    '
    Me.apt_prenot21.AppearanceCell.Options.UseBackColor = True
    Me.apt_prenot21.AppearanceCell.Options.UseTextOptions = True
    Me.apt_prenot21.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_prenot21.Caption = "Prenotato 21"
    Me.apt_prenot21.Enabled = True
    Me.apt_prenot21.FieldName = "apt_prenot21"
    Me.apt_prenot21.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_prenot21.Name = "apt_prenot21"
    Me.apt_prenot21.NTSRepositoryComboBox = Nothing
    Me.apt_prenot21.NTSRepositoryItemCheck = Nothing
    Me.apt_prenot21.NTSRepositoryItemMemo = Nothing
    Me.apt_prenot21.NTSRepositoryItemText = Nothing
    Me.apt_prenot21.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_prenot21.OptionsFilter.AllowFilter = False
    Me.apt_prenot21.Visible = True
    Me.apt_prenot21.VisibleIndex = 49
    '
    'apt_prenot22
    '
    Me.apt_prenot22.AppearanceCell.Options.UseBackColor = True
    Me.apt_prenot22.AppearanceCell.Options.UseTextOptions = True
    Me.apt_prenot22.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_prenot22.Caption = "Prenotato 22"
    Me.apt_prenot22.Enabled = True
    Me.apt_prenot22.FieldName = "apt_prenot22"
    Me.apt_prenot22.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_prenot22.Name = "apt_prenot22"
    Me.apt_prenot22.NTSRepositoryComboBox = Nothing
    Me.apt_prenot22.NTSRepositoryItemCheck = Nothing
    Me.apt_prenot22.NTSRepositoryItemMemo = Nothing
    Me.apt_prenot22.NTSRepositoryItemText = Nothing
    Me.apt_prenot22.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_prenot22.OptionsFilter.AllowFilter = False
    Me.apt_prenot22.Visible = True
    Me.apt_prenot22.VisibleIndex = 50
    '
    'apt_prenot23
    '
    Me.apt_prenot23.AppearanceCell.Options.UseBackColor = True
    Me.apt_prenot23.AppearanceCell.Options.UseTextOptions = True
    Me.apt_prenot23.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_prenot23.Caption = "Prenotato 23"
    Me.apt_prenot23.Enabled = True
    Me.apt_prenot23.FieldName = "apt_prenot23"
    Me.apt_prenot23.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_prenot23.Name = "apt_prenot23"
    Me.apt_prenot23.NTSRepositoryComboBox = Nothing
    Me.apt_prenot23.NTSRepositoryItemCheck = Nothing
    Me.apt_prenot23.NTSRepositoryItemMemo = Nothing
    Me.apt_prenot23.NTSRepositoryItemText = Nothing
    Me.apt_prenot23.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_prenot23.OptionsFilter.AllowFilter = False
    Me.apt_prenot23.Visible = True
    Me.apt_prenot23.VisibleIndex = 51
    '
    'apt_prenot24
    '
    Me.apt_prenot24.AppearanceCell.Options.UseBackColor = True
    Me.apt_prenot24.AppearanceCell.Options.UseTextOptions = True
    Me.apt_prenot24.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_prenot24.Caption = "Prenotato 24"
    Me.apt_prenot24.Enabled = True
    Me.apt_prenot24.FieldName = "apt_prenot24"
    Me.apt_prenot24.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_prenot24.Name = "apt_prenot24"
    Me.apt_prenot24.NTSRepositoryComboBox = Nothing
    Me.apt_prenot24.NTSRepositoryItemCheck = Nothing
    Me.apt_prenot24.NTSRepositoryItemMemo = Nothing
    Me.apt_prenot24.NTSRepositoryItemText = Nothing
    Me.apt_prenot24.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_prenot24.OptionsFilter.AllowFilter = False
    Me.apt_prenot24.Visible = True
    Me.apt_prenot24.VisibleIndex = 52
    '
    'apt_ordin01
    '
    Me.apt_ordin01.AppearanceCell.Options.UseBackColor = True
    Me.apt_ordin01.AppearanceCell.Options.UseTextOptions = True
    Me.apt_ordin01.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_ordin01.Caption = "Ordinato 01"
    Me.apt_ordin01.Enabled = True
    Me.apt_ordin01.FieldName = "apt_ordin01"
    Me.apt_ordin01.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_ordin01.Name = "apt_ordin01"
    Me.apt_ordin01.NTSRepositoryComboBox = Nothing
    Me.apt_ordin01.NTSRepositoryItemCheck = Nothing
    Me.apt_ordin01.NTSRepositoryItemMemo = Nothing
    Me.apt_ordin01.NTSRepositoryItemText = Nothing
    Me.apt_ordin01.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_ordin01.OptionsFilter.AllowFilter = False
    Me.apt_ordin01.Visible = True
    Me.apt_ordin01.VisibleIndex = 53
    '
    'apt_ordin02
    '
    Me.apt_ordin02.AppearanceCell.Options.UseBackColor = True
    Me.apt_ordin02.AppearanceCell.Options.UseTextOptions = True
    Me.apt_ordin02.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_ordin02.Caption = "Ordinato 02"
    Me.apt_ordin02.Enabled = True
    Me.apt_ordin02.FieldName = "apt_ordin02"
    Me.apt_ordin02.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_ordin02.Name = "apt_ordin02"
    Me.apt_ordin02.NTSRepositoryComboBox = Nothing
    Me.apt_ordin02.NTSRepositoryItemCheck = Nothing
    Me.apt_ordin02.NTSRepositoryItemMemo = Nothing
    Me.apt_ordin02.NTSRepositoryItemText = Nothing
    Me.apt_ordin02.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_ordin02.OptionsFilter.AllowFilter = False
    Me.apt_ordin02.Visible = True
    Me.apt_ordin02.VisibleIndex = 54
    '
    'apt_ordin03
    '
    Me.apt_ordin03.AppearanceCell.Options.UseBackColor = True
    Me.apt_ordin03.AppearanceCell.Options.UseTextOptions = True
    Me.apt_ordin03.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_ordin03.Caption = "Ordinato 03"
    Me.apt_ordin03.Enabled = True
    Me.apt_ordin03.FieldName = "apt_ordin03"
    Me.apt_ordin03.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_ordin03.Name = "apt_ordin03"
    Me.apt_ordin03.NTSRepositoryComboBox = Nothing
    Me.apt_ordin03.NTSRepositoryItemCheck = Nothing
    Me.apt_ordin03.NTSRepositoryItemMemo = Nothing
    Me.apt_ordin03.NTSRepositoryItemText = Nothing
    Me.apt_ordin03.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_ordin03.OptionsFilter.AllowFilter = False
    Me.apt_ordin03.Visible = True
    Me.apt_ordin03.VisibleIndex = 55
    '
    'apt_ordin04
    '
    Me.apt_ordin04.AppearanceCell.Options.UseBackColor = True
    Me.apt_ordin04.AppearanceCell.Options.UseTextOptions = True
    Me.apt_ordin04.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_ordin04.Caption = "Ordinato 04"
    Me.apt_ordin04.Enabled = True
    Me.apt_ordin04.FieldName = "apt_ordin04"
    Me.apt_ordin04.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_ordin04.Name = "apt_ordin04"
    Me.apt_ordin04.NTSRepositoryComboBox = Nothing
    Me.apt_ordin04.NTSRepositoryItemCheck = Nothing
    Me.apt_ordin04.NTSRepositoryItemMemo = Nothing
    Me.apt_ordin04.NTSRepositoryItemText = Nothing
    Me.apt_ordin04.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_ordin04.OptionsFilter.AllowFilter = False
    Me.apt_ordin04.Visible = True
    Me.apt_ordin04.VisibleIndex = 56
    '
    'apt_ordin05
    '
    Me.apt_ordin05.AppearanceCell.Options.UseBackColor = True
    Me.apt_ordin05.AppearanceCell.Options.UseTextOptions = True
    Me.apt_ordin05.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_ordin05.Caption = "Ordinato 05"
    Me.apt_ordin05.Enabled = True
    Me.apt_ordin05.FieldName = "apt_ordin05"
    Me.apt_ordin05.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_ordin05.Name = "apt_ordin05"
    Me.apt_ordin05.NTSRepositoryComboBox = Nothing
    Me.apt_ordin05.NTSRepositoryItemCheck = Nothing
    Me.apt_ordin05.NTSRepositoryItemMemo = Nothing
    Me.apt_ordin05.NTSRepositoryItemText = Nothing
    Me.apt_ordin05.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_ordin05.OptionsFilter.AllowFilter = False
    Me.apt_ordin05.Visible = True
    Me.apt_ordin05.VisibleIndex = 57
    '
    'apt_ordin06
    '
    Me.apt_ordin06.AppearanceCell.Options.UseBackColor = True
    Me.apt_ordin06.AppearanceCell.Options.UseTextOptions = True
    Me.apt_ordin06.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_ordin06.Caption = "Ordinato 06"
    Me.apt_ordin06.Enabled = True
    Me.apt_ordin06.FieldName = "apt_ordin06"
    Me.apt_ordin06.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_ordin06.Name = "apt_ordin06"
    Me.apt_ordin06.NTSRepositoryComboBox = Nothing
    Me.apt_ordin06.NTSRepositoryItemCheck = Nothing
    Me.apt_ordin06.NTSRepositoryItemMemo = Nothing
    Me.apt_ordin06.NTSRepositoryItemText = Nothing
    Me.apt_ordin06.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_ordin06.OptionsFilter.AllowFilter = False
    Me.apt_ordin06.Visible = True
    Me.apt_ordin06.VisibleIndex = 58
    '
    'apt_ordin07
    '
    Me.apt_ordin07.AppearanceCell.Options.UseBackColor = True
    Me.apt_ordin07.AppearanceCell.Options.UseTextOptions = True
    Me.apt_ordin07.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_ordin07.Caption = "Ordinato 07"
    Me.apt_ordin07.Enabled = True
    Me.apt_ordin07.FieldName = "apt_ordin07"
    Me.apt_ordin07.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_ordin07.Name = "apt_ordin07"
    Me.apt_ordin07.NTSRepositoryComboBox = Nothing
    Me.apt_ordin07.NTSRepositoryItemCheck = Nothing
    Me.apt_ordin07.NTSRepositoryItemMemo = Nothing
    Me.apt_ordin07.NTSRepositoryItemText = Nothing
    Me.apt_ordin07.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_ordin07.OptionsFilter.AllowFilter = False
    Me.apt_ordin07.Visible = True
    Me.apt_ordin07.VisibleIndex = 59
    '
    'apt_ordin08
    '
    Me.apt_ordin08.AppearanceCell.Options.UseBackColor = True
    Me.apt_ordin08.AppearanceCell.Options.UseTextOptions = True
    Me.apt_ordin08.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_ordin08.Caption = "Ordinato 08"
    Me.apt_ordin08.Enabled = True
    Me.apt_ordin08.FieldName = "apt_ordin08"
    Me.apt_ordin08.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_ordin08.Name = "apt_ordin08"
    Me.apt_ordin08.NTSRepositoryComboBox = Nothing
    Me.apt_ordin08.NTSRepositoryItemCheck = Nothing
    Me.apt_ordin08.NTSRepositoryItemMemo = Nothing
    Me.apt_ordin08.NTSRepositoryItemText = Nothing
    Me.apt_ordin08.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_ordin08.OptionsFilter.AllowFilter = False
    Me.apt_ordin08.Visible = True
    Me.apt_ordin08.VisibleIndex = 60
    '
    'apt_ordin09
    '
    Me.apt_ordin09.AppearanceCell.Options.UseBackColor = True
    Me.apt_ordin09.AppearanceCell.Options.UseTextOptions = True
    Me.apt_ordin09.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_ordin09.Caption = "Ordinato 09"
    Me.apt_ordin09.Enabled = True
    Me.apt_ordin09.FieldName = "apt_ordin09"
    Me.apt_ordin09.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_ordin09.Name = "apt_ordin09"
    Me.apt_ordin09.NTSRepositoryComboBox = Nothing
    Me.apt_ordin09.NTSRepositoryItemCheck = Nothing
    Me.apt_ordin09.NTSRepositoryItemMemo = Nothing
    Me.apt_ordin09.NTSRepositoryItemText = Nothing
    Me.apt_ordin09.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_ordin09.OptionsFilter.AllowFilter = False
    Me.apt_ordin09.Visible = True
    Me.apt_ordin09.VisibleIndex = 61
    '
    'apt_ordin10
    '
    Me.apt_ordin10.AppearanceCell.Options.UseBackColor = True
    Me.apt_ordin10.AppearanceCell.Options.UseTextOptions = True
    Me.apt_ordin10.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_ordin10.Caption = "Ordinato 10"
    Me.apt_ordin10.Enabled = True
    Me.apt_ordin10.FieldName = "apt_ordin10"
    Me.apt_ordin10.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_ordin10.Name = "apt_ordin10"
    Me.apt_ordin10.NTSRepositoryComboBox = Nothing
    Me.apt_ordin10.NTSRepositoryItemCheck = Nothing
    Me.apt_ordin10.NTSRepositoryItemMemo = Nothing
    Me.apt_ordin10.NTSRepositoryItemText = Nothing
    Me.apt_ordin10.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_ordin10.OptionsFilter.AllowFilter = False
    Me.apt_ordin10.Visible = True
    Me.apt_ordin10.VisibleIndex = 62
    '
    'apt_ordin11
    '
    Me.apt_ordin11.AppearanceCell.Options.UseBackColor = True
    Me.apt_ordin11.AppearanceCell.Options.UseTextOptions = True
    Me.apt_ordin11.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_ordin11.Caption = "Ordinato 11"
    Me.apt_ordin11.Enabled = True
    Me.apt_ordin11.FieldName = "apt_ordin11"
    Me.apt_ordin11.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_ordin11.Name = "apt_ordin11"
    Me.apt_ordin11.NTSRepositoryComboBox = Nothing
    Me.apt_ordin11.NTSRepositoryItemCheck = Nothing
    Me.apt_ordin11.NTSRepositoryItemMemo = Nothing
    Me.apt_ordin11.NTSRepositoryItemText = Nothing
    Me.apt_ordin11.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_ordin11.OptionsFilter.AllowFilter = False
    Me.apt_ordin11.Visible = True
    Me.apt_ordin11.VisibleIndex = 63
    '
    'apt_ordin12
    '
    Me.apt_ordin12.AppearanceCell.Options.UseBackColor = True
    Me.apt_ordin12.AppearanceCell.Options.UseTextOptions = True
    Me.apt_ordin12.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_ordin12.Caption = "Ordinato 12"
    Me.apt_ordin12.Enabled = True
    Me.apt_ordin12.FieldName = "apt_ordin12"
    Me.apt_ordin12.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_ordin12.Name = "apt_ordin12"
    Me.apt_ordin12.NTSRepositoryComboBox = Nothing
    Me.apt_ordin12.NTSRepositoryItemCheck = Nothing
    Me.apt_ordin12.NTSRepositoryItemMemo = Nothing
    Me.apt_ordin12.NTSRepositoryItemText = Nothing
    Me.apt_ordin12.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_ordin12.OptionsFilter.AllowFilter = False
    Me.apt_ordin12.Visible = True
    Me.apt_ordin12.VisibleIndex = 64
    '
    'apt_ordin13
    '
    Me.apt_ordin13.AppearanceCell.Options.UseBackColor = True
    Me.apt_ordin13.AppearanceCell.Options.UseTextOptions = True
    Me.apt_ordin13.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_ordin13.Caption = "Ordinato 13"
    Me.apt_ordin13.Enabled = True
    Me.apt_ordin13.FieldName = "apt_ordin13"
    Me.apt_ordin13.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_ordin13.Name = "apt_ordin13"
    Me.apt_ordin13.NTSRepositoryComboBox = Nothing
    Me.apt_ordin13.NTSRepositoryItemCheck = Nothing
    Me.apt_ordin13.NTSRepositoryItemMemo = Nothing
    Me.apt_ordin13.NTSRepositoryItemText = Nothing
    Me.apt_ordin13.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_ordin13.OptionsFilter.AllowFilter = False
    Me.apt_ordin13.Visible = True
    Me.apt_ordin13.VisibleIndex = 65
    '
    'apt_ordin14
    '
    Me.apt_ordin14.AppearanceCell.Options.UseBackColor = True
    Me.apt_ordin14.AppearanceCell.Options.UseTextOptions = True
    Me.apt_ordin14.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_ordin14.Caption = "Ordinato 14"
    Me.apt_ordin14.Enabled = True
    Me.apt_ordin14.FieldName = "apt_ordin14"
    Me.apt_ordin14.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_ordin14.Name = "apt_ordin14"
    Me.apt_ordin14.NTSRepositoryComboBox = Nothing
    Me.apt_ordin14.NTSRepositoryItemCheck = Nothing
    Me.apt_ordin14.NTSRepositoryItemMemo = Nothing
    Me.apt_ordin14.NTSRepositoryItemText = Nothing
    Me.apt_ordin14.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_ordin14.OptionsFilter.AllowFilter = False
    Me.apt_ordin14.Visible = True
    Me.apt_ordin14.VisibleIndex = 66
    '
    'apt_ordin15
    '
    Me.apt_ordin15.AppearanceCell.Options.UseBackColor = True
    Me.apt_ordin15.AppearanceCell.Options.UseTextOptions = True
    Me.apt_ordin15.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_ordin15.Caption = "Ordinato 15"
    Me.apt_ordin15.Enabled = True
    Me.apt_ordin15.FieldName = "apt_ordin15"
    Me.apt_ordin15.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_ordin15.Name = "apt_ordin15"
    Me.apt_ordin15.NTSRepositoryComboBox = Nothing
    Me.apt_ordin15.NTSRepositoryItemCheck = Nothing
    Me.apt_ordin15.NTSRepositoryItemMemo = Nothing
    Me.apt_ordin15.NTSRepositoryItemText = Nothing
    Me.apt_ordin15.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_ordin15.OptionsFilter.AllowFilter = False
    Me.apt_ordin15.Visible = True
    Me.apt_ordin15.VisibleIndex = 67
    '
    'apt_ordin16
    '
    Me.apt_ordin16.AppearanceCell.Options.UseBackColor = True
    Me.apt_ordin16.AppearanceCell.Options.UseTextOptions = True
    Me.apt_ordin16.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_ordin16.Caption = "Ordinato 16"
    Me.apt_ordin16.Enabled = True
    Me.apt_ordin16.FieldName = "apt_ordin16"
    Me.apt_ordin16.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_ordin16.Name = "apt_ordin16"
    Me.apt_ordin16.NTSRepositoryComboBox = Nothing
    Me.apt_ordin16.NTSRepositoryItemCheck = Nothing
    Me.apt_ordin16.NTSRepositoryItemMemo = Nothing
    Me.apt_ordin16.NTSRepositoryItemText = Nothing
    Me.apt_ordin16.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_ordin16.OptionsFilter.AllowFilter = False
    Me.apt_ordin16.Visible = True
    Me.apt_ordin16.VisibleIndex = 68
    '
    'apt_ordin17
    '
    Me.apt_ordin17.AppearanceCell.Options.UseBackColor = True
    Me.apt_ordin17.AppearanceCell.Options.UseTextOptions = True
    Me.apt_ordin17.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_ordin17.Caption = "Ordinato 17"
    Me.apt_ordin17.Enabled = True
    Me.apt_ordin17.FieldName = "apt_ordin17"
    Me.apt_ordin17.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_ordin17.Name = "apt_ordin17"
    Me.apt_ordin17.NTSRepositoryComboBox = Nothing
    Me.apt_ordin17.NTSRepositoryItemCheck = Nothing
    Me.apt_ordin17.NTSRepositoryItemMemo = Nothing
    Me.apt_ordin17.NTSRepositoryItemText = Nothing
    Me.apt_ordin17.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_ordin17.OptionsFilter.AllowFilter = False
    Me.apt_ordin17.Visible = True
    Me.apt_ordin17.VisibleIndex = 69
    '
    'apt_ordin18
    '
    Me.apt_ordin18.AppearanceCell.Options.UseBackColor = True
    Me.apt_ordin18.AppearanceCell.Options.UseTextOptions = True
    Me.apt_ordin18.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_ordin18.Caption = "Ordinato 18"
    Me.apt_ordin18.Enabled = True
    Me.apt_ordin18.FieldName = "apt_ordin18"
    Me.apt_ordin18.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_ordin18.Name = "apt_ordin18"
    Me.apt_ordin18.NTSRepositoryComboBox = Nothing
    Me.apt_ordin18.NTSRepositoryItemCheck = Nothing
    Me.apt_ordin18.NTSRepositoryItemMemo = Nothing
    Me.apt_ordin18.NTSRepositoryItemText = Nothing
    Me.apt_ordin18.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_ordin18.OptionsFilter.AllowFilter = False
    Me.apt_ordin18.Visible = True
    Me.apt_ordin18.VisibleIndex = 70
    '
    'apt_ordin19
    '
    Me.apt_ordin19.AppearanceCell.Options.UseBackColor = True
    Me.apt_ordin19.AppearanceCell.Options.UseTextOptions = True
    Me.apt_ordin19.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_ordin19.Caption = "Ordinato 19"
    Me.apt_ordin19.Enabled = True
    Me.apt_ordin19.FieldName = "apt_ordin19"
    Me.apt_ordin19.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_ordin19.Name = "apt_ordin19"
    Me.apt_ordin19.NTSRepositoryComboBox = Nothing
    Me.apt_ordin19.NTSRepositoryItemCheck = Nothing
    Me.apt_ordin19.NTSRepositoryItemMemo = Nothing
    Me.apt_ordin19.NTSRepositoryItemText = Nothing
    Me.apt_ordin19.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_ordin19.OptionsFilter.AllowFilter = False
    Me.apt_ordin19.Visible = True
    Me.apt_ordin19.VisibleIndex = 71
    '
    'apt_ordin20
    '
    Me.apt_ordin20.AppearanceCell.Options.UseBackColor = True
    Me.apt_ordin20.AppearanceCell.Options.UseTextOptions = True
    Me.apt_ordin20.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_ordin20.Caption = "Ordinato 20"
    Me.apt_ordin20.Enabled = True
    Me.apt_ordin20.FieldName = "apt_ordin20"
    Me.apt_ordin20.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_ordin20.Name = "apt_ordin20"
    Me.apt_ordin20.NTSRepositoryComboBox = Nothing
    Me.apt_ordin20.NTSRepositoryItemCheck = Nothing
    Me.apt_ordin20.NTSRepositoryItemMemo = Nothing
    Me.apt_ordin20.NTSRepositoryItemText = Nothing
    Me.apt_ordin20.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_ordin20.OptionsFilter.AllowFilter = False
    Me.apt_ordin20.Visible = True
    Me.apt_ordin20.VisibleIndex = 72
    '
    'apt_ordin21
    '
    Me.apt_ordin21.AppearanceCell.Options.UseBackColor = True
    Me.apt_ordin21.AppearanceCell.Options.UseTextOptions = True
    Me.apt_ordin21.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_ordin21.Caption = "Ordinato 21"
    Me.apt_ordin21.Enabled = True
    Me.apt_ordin21.FieldName = "apt_ordin21"
    Me.apt_ordin21.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_ordin21.Name = "apt_ordin21"
    Me.apt_ordin21.NTSRepositoryComboBox = Nothing
    Me.apt_ordin21.NTSRepositoryItemCheck = Nothing
    Me.apt_ordin21.NTSRepositoryItemMemo = Nothing
    Me.apt_ordin21.NTSRepositoryItemText = Nothing
    Me.apt_ordin21.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_ordin21.OptionsFilter.AllowFilter = False
    Me.apt_ordin21.Visible = True
    Me.apt_ordin21.VisibleIndex = 73
    '
    'apt_ordin22
    '
    Me.apt_ordin22.AppearanceCell.Options.UseBackColor = True
    Me.apt_ordin22.AppearanceCell.Options.UseTextOptions = True
    Me.apt_ordin22.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_ordin22.Caption = "Ordinato 22"
    Me.apt_ordin22.Enabled = True
    Me.apt_ordin22.FieldName = "apt_ordin22"
    Me.apt_ordin22.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_ordin22.Name = "apt_ordin22"
    Me.apt_ordin22.NTSRepositoryComboBox = Nothing
    Me.apt_ordin22.NTSRepositoryItemCheck = Nothing
    Me.apt_ordin22.NTSRepositoryItemMemo = Nothing
    Me.apt_ordin22.NTSRepositoryItemText = Nothing
    Me.apt_ordin22.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_ordin22.OptionsFilter.AllowFilter = False
    Me.apt_ordin22.Visible = True
    Me.apt_ordin22.VisibleIndex = 74
    '
    'apt_ordin23
    '
    Me.apt_ordin23.AppearanceCell.Options.UseBackColor = True
    Me.apt_ordin23.AppearanceCell.Options.UseTextOptions = True
    Me.apt_ordin23.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_ordin23.Caption = "Ordinato 23"
    Me.apt_ordin23.Enabled = True
    Me.apt_ordin23.FieldName = "apt_ordin23"
    Me.apt_ordin23.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_ordin23.Name = "apt_ordin23"
    Me.apt_ordin23.NTSRepositoryComboBox = Nothing
    Me.apt_ordin23.NTSRepositoryItemCheck = Nothing
    Me.apt_ordin23.NTSRepositoryItemMemo = Nothing
    Me.apt_ordin23.NTSRepositoryItemText = Nothing
    Me.apt_ordin23.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_ordin23.OptionsFilter.AllowFilter = False
    Me.apt_ordin23.Visible = True
    Me.apt_ordin23.VisibleIndex = 75
    '
    'apt_ordin24
    '
    Me.apt_ordin24.AppearanceCell.Options.UseBackColor = True
    Me.apt_ordin24.AppearanceCell.Options.UseTextOptions = True
    Me.apt_ordin24.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.apt_ordin24.Caption = "Ordinato 24"
    Me.apt_ordin24.Enabled = True
    Me.apt_ordin24.FieldName = "apt_ordin24"
    Me.apt_ordin24.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.apt_ordin24.Name = "apt_ordin24"
    Me.apt_ordin24.NTSRepositoryComboBox = Nothing
    Me.apt_ordin24.NTSRepositoryItemCheck = Nothing
    Me.apt_ordin24.NTSRepositoryItemMemo = Nothing
    Me.apt_ordin24.NTSRepositoryItemText = Nothing
    Me.apt_ordin24.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.apt_ordin24.OptionsFilter.AllowFilter = False
    Me.apt_ordin24.Visible = True
    Me.apt_ordin24.VisibleIndex = 76
    '
    'FRMMGLOTC
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(678, 379)
    Me.Controls.Add(Me.grLotc)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.MinimizeBox = False
    Me.Name = "FRMMGLOTC"
    Me.Text = "DETTAGLIO PROGRESSIVI TAGLIE & COLORI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.grLotc, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvLotc, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, False)
    '----------------------------------------------------------------------------------------------------------------
    Try
      '--------------------------------------------------------------------------------------------------------------
      Try
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
      End Try
      tlbMain.NTSSetToolTip()
      '--------------------------------------------------------------------------------------------------------------
      grvLotc.NTSSetParam(oMenu, oApp.Tr(Me, 128828971526205441, "Griglia Dettaglio Progressivi Taglie & Colori"))
      apt_commeca.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128828971611674738, "Commessa"), tabcommess)
      xx_commeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128828971624799822, "Descrizione commessa"), 0, True)
      apt_lotto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130991381852735324, "Lotto"), "0", 9, 0, 999999999)
      xx_lotto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130991382169784452, "Descrizione lotto"), 0, True)
      apt_ubicaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130991382440534665, "Ubicazione"), 0, True)
      '--------------------------------------------------------------------------------------------------------------
      apt_esist01.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130993997558292023, "Esistenza 01"), oApp.FormatQta, 15)
      apt_esist02.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130993997574329437, "Esistenza 02"), oApp.FormatQta, 15)
      apt_esist03.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130993997589898035, "Esistenza 03"), oApp.FormatQta, 15)
      apt_esist04.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130993997607498169, "Esistenza 04"), oApp.FormatQta, 15)
      apt_esist05.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130993997625879663, "Esistenza 05"), oApp.FormatQta, 15)
      apt_esist06.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130993997642307757, "Esistenza 06"), oApp.FormatQta, 15)
      apt_esist07.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130993997658423307, "Esistenza 07"), oApp.FormatQta, 15)
      apt_esist08.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130993997674616993, "Esistenza 08"), oApp.FormatQta, 15)
      apt_esist09.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130993997707297375, "Esistenza 09"), oApp.FormatQta, 15)
      apt_esist10.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130993997727300191, "Esistenza 10"), oApp.FormatQta, 15)
      apt_esist11.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130993997745916093, "Esistenza 11"), oApp.FormatQta, 15)
      apt_esist12.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130993997770001515, "Esistenza 12"), oApp.FormatQta, 15)
      apt_esist13.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130993997803463257, "Esistenza 13"), oApp.FormatQta, 15)
      apt_esist14.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130993997819969487, "Esistenza 14"), oApp.FormatQta, 15)
      apt_esist15.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130993997834912997, "Esistenza 15"), oApp.FormatQta, 15)
      apt_esist16.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130993997850325323, "Esistenza 16"), oApp.FormatQta, 15)
      apt_esist17.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130993997867534777, "Esistenza 17"), oApp.FormatQta, 15)
      apt_esist18.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130993997882868967, "Esistenza 18"), oApp.FormatQta, 15)
      apt_esist19.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130993997897734341, "Esistenza 19"), oApp.FormatQta, 15)
      apt_esist20.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130993997915100067, "Esistenza 20"), oApp.FormatQta, 15)
      apt_esist21.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130993997931684433, "Esistenza 21"), oApp.FormatQta, 15)
      apt_esist22.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130993997947096759, "Esistenza 22"), oApp.FormatQta, 15)
      apt_esist23.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130993997962040269, "Esistenza 23"), oApp.FormatQta, 15)
      apt_esist24.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130993997976983779, "Esistenza 24"), oApp.FormatQta, 15)
      '--------------------------------------------------------------------------------------------------------------
      apt_prenot01.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994230259934545, "Prenotato 01"), oApp.FormatQta, 15)
      apt_prenot02.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994230298781775, "Prenotato 02"), oApp.FormatQta, 15)
      apt_prenot03.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994230315281354, "Prenotato 03"), oApp.FormatQta, 15)
      apt_prenot04.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994230334436485, "Prenotato 04"), oApp.FormatQta, 15)
      apt_prenot05.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994230351404691, "Prenotato 05"), oApp.FormatQta, 15)
      apt_prenot06.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994230366576493, "Prenotato 06"), oApp.FormatQta, 15)
      apt_prenot07.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994230382138818, "Prenotato 07"), oApp.FormatQta, 15)
      apt_prenot08.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994230399419441, "Prenotato 08"), oApp.FormatQta, 15)
      apt_prenot09.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994230415059872, "Prenotato 09"), oApp.FormatQta, 15)
      apt_prenot10.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994230434215004, "Prenotato 10"), oApp.FormatQta, 15)
      apt_prenot11.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994230470162604, "Prenotato 11"), oApp.FormatQta, 15)
      apt_prenot12.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994230485490617, "Prenotato 12"), oApp.FormatQta, 15)
      apt_prenot13.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994230502536929, "Prenotato 13"), oApp.FormatQta, 15)
      apt_prenot14.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994230523410360, "Prenotato 14"), oApp.FormatQta, 15)
      apt_prenot15.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994230539285103, "Prenotato 15"), oApp.FormatQta, 15)
      apt_prenot16.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994230555472264, "Prenotato 16"), oApp.FormatQta, 15)
      apt_prenot17.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994230581188174, "Prenotato 17"), oApp.FormatQta, 15)
      apt_prenot18.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994230596672395, "Prenotato 18"), oApp.FormatQta, 15)
      apt_prenot19.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994230617545826, "Prenotato 19"), oApp.FormatQta, 15)
      apt_prenot20.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994230635451286, "Prenotato 20"), oApp.FormatQta, 15)
      apt_prenot21.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994230651716553, "Prenotato 21"), oApp.FormatQta, 15)
      apt_prenot22.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994230669543910, "Prenotato 22"), oApp.FormatQta, 15)
      apt_prenot23.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994230686434011, "Prenotato 23"), oApp.FormatQta, 15)
      apt_prenot24.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994230703011695, "Prenotato 24"), oApp.FormatQta, 15)
      '--------------------------------------------------------------------------------------------------------------
      apt_prenot01.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994238735102884, "Ordinato 01"), oApp.FormatQta, 15)
      apt_prenot02.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994238781641652, "Ordinato 02"), oApp.FormatQta, 15)
      apt_prenot03.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994238797359589, "Ordinato 03"), oApp.FormatQta, 15)
      apt_prenot04.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994238813780441, "Ordinato 04"), oApp.FormatQta, 15)
      apt_prenot05.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994238833247254, "Ordinato 05"), oApp.FormatQta, 15)
      apt_prenot06.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994238849902413, "Ordinato 06"), oApp.FormatQta, 15)
      apt_prenot07.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994238866479468, "Ordinato 07"), oApp.FormatQta, 15)
      apt_prenot08.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994238883759435, "Ordinato 08"), oApp.FormatQta, 15)
      apt_prenot09.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994238907287522, "Ordinato 09"), oApp.FormatQta, 15)
      apt_prenot10.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994238922849256, "Ordinato 10"), oApp.FormatQta, 15)
      apt_prenot11.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994238941847456, "Ordinato 11"), oApp.FormatQta, 15)
      apt_prenot12.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994238989294139, "Ordinato 12"), oApp.FormatQta, 15)
      apt_prenot13.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994239005168279, "Ordinato 13"), oApp.FormatQta, 15)
      apt_prenot14.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994239021608665, "Ordinato 14"), oApp.FormatQta, 15)
      apt_prenot15.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994239037951414, "Ordinato 15"), oApp.FormatQta, 15)
      apt_prenot16.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994239383570192, "Ordinato 16"), oApp.FormatQta, 15)
      apt_prenot17.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994239078525166, "Ordinato 17"), oApp.FormatQta, 15)
      apt_prenot18.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994239094633612, "Ordinato 18"), oApp.FormatQta, 15)
      apt_prenot19.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994239114334727, "Ordinato 19"), oApp.FormatQta, 15)
      apt_prenot20.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994239413346406, "Ordinato 20"), oApp.FormatQta, 15)
      apt_prenot21.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994239428830041, "Ordinato 21"), oApp.FormatQta, 15)
      apt_prenot22.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994239445797601, "Ordinato 22"), oApp.FormatQta, 15)
      apt_prenot23.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994239461827944, "Ordinato 23"), oApp.FormatQta, 15)
      apt_prenot24.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994239486996164, "Ordinato 24"), oApp.FormatQta, 15)
      '--------------------------------------------------------------------------------------------------------------
      apt_impeg01.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994790022543641, "Impegnato 01"), oApp.FormatQta, 15)
      apt_impeg02.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994790063365964, "Impegnato 02"), oApp.FormatQta, 15)
      apt_impeg03.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994790088062399, "Impegnato 03"), oApp.FormatQta, 15)
      apt_impeg04.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994790103309761, "Impegnato 04"), oApp.FormatQta, 15)
      apt_impeg05.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994790119572314, "Impegnato 05"), oApp.FormatQta, 15)
      apt_impeg06.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994790135522501, "Impegnato 06"), oApp.FormatQta, 15)
      apt_impeg07.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994790151863144, "Impegnato 07"), oApp.FormatQta, 15)
      apt_impeg08.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994790168438063, "Impegnato 08"), oApp.FormatQta, 15)
      apt_impeg09.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994790185169160, "Impegnato 09"), oApp.FormatQta, 15)
      apt_impeg10.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994790203462091, "Impegnato 10"), oApp.FormatQta, 15)
      apt_impeg11.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994790220505558, "Impegnato 11"), oApp.FormatQta, 15)
      apt_impeg12.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994790238095663, "Impegnato 12"), oApp.FormatQta, 15)
      apt_impeg13.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994790253421118, "Impegnato 13"), oApp.FormatQta, 15)
      apt_impeg14.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994790270776952, "Impegnato 14"), oApp.FormatQta, 15)
      apt_impeg15.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994790287429961, "Impegnato 15"), oApp.FormatQta, 15)
      apt_impeg16.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994790303770604, "Impegnato 16"), oApp.FormatQta, 15)
      apt_impeg17.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994790320657889, "Impegnato 17"), oApp.FormatQta, 15)
      apt_impeg18.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994790336998532, "Impegnato 18"), oApp.FormatQta, 15)
      apt_impeg19.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994790359742678, "Impegnato 19"), oApp.FormatQta, 15)
      apt_impeg20.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994790375536682, "Impegnato 20"), oApp.FormatQta, 15)
      apt_impeg21.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994790392189691, "Impegnato 21"), oApp.FormatQta, 15)
      apt_impeg22.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994790409311249, "Impegnato 22"), oApp.FormatQta, 15)
      apt_impeg23.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994790425651892, "Impegnato 23"), oApp.FormatQta, 15)
      apt_impeg24.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130994790441836352, "Impegnato 24"), oApp.FormatQta, 15)
      '--------------------------------------------------------------------------------------------------------------
      grvLotc.NTSAllowInsert = False
      grvLotc.NTSAllowUpdate = False
      grvLotc.NTSAllowDelete = False
      grvLotc.Enabled = False
      '--------------------------------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, True)
    '----------------------------------------------------------------------------------------------------------------
  End Sub

  Public Overridable Sub InitEntity(ByVal cleArmd As CLEMGARMD)
    oCleArmd = cleArmd
    AddHandler oCleArmd.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRMMGLOTC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim bCommeca As Boolean = CBool(oMenu.GetSettingBus("BNMGLOTC", "RECENT", ".", "RaggrCommeca", "0", " ", "0"))
    Dim bLotto As Boolean = CBool(oMenu.GetSettingBus("BNMGLOTC", "RECENT", ".", "RaggrLotto", "0", " ", "0"))
    Dim bUbicaz As Boolean = CBool(oMenu.GetSettingBus("BNMGLOTC", "RECENT", ".", "RaggrUbicaz", "0", " ", "0"))
    Dim nTmp As Integer = 0
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      bOnLoading = True
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      lbAr_codart.Text = oCleArmd.strLotcCodart
      oMenu.ValCodiceDb(oCleArmd.strLotcCodart, DittaCorrente, "ARTICO", "S", "", dttTmp)
      lbXx_codart.Text = oCleArmd.strLotcDesCodart
      lbAr_magaz.Text = oCleArmd.strLotcMagaz
      lbXx_magaz.Text = oCleArmd.strLotcDesMagaz
      lbAr_fase.Text = oCleArmd.strLotcFase
      lbXx_fase.Text = oCleArmd.strLotcDesFase
      '--------------------------------------------------------------------------------------------------------------
      With dttTmp.Rows(0)
        bGescomm = CBool(IIf(NTSCStr(!ar_gescomm) <> "N", True, False))
        bGeslotti = CBool(IIf(NTSCStr(!ar_geslotti) <> "N", True, False))
        bGesubic = CBool(IIf(NTSCStr(!ar_gesubic) <> "N", True, False))
      End With
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dttTmp.Rows(0)!ar_gescomm) = "N" Then
        apt_commeca.Visible = False
        xx_commeca.Visible = False
        tlbCommessa.Down = False
        tlbCommessa.Enabled = False
      Else
        nTmp += 1
      End If
      If NTSCStr(dttTmp.Rows(0)!ar_geslotti) = "N" Then
        apt_lotto.Visible = False
        xx_lotto.Visible = False
        tlbLotti.Down = False
        tlbLotti.Enabled = False
      Else
        nTmp += 1
      End If
      If NTSCStr(dttTmp.Rows(0)!ar_gesubic) = "N" Then
        apt_ubicaz.Visible = False
        tlbUbicazioni.Down = False
        tlbUbicazioni.Enabled = False
      Else
        nTmp += 1
      End If
      '--------------------------------------------------------------------------------------------------------------
      If nTmp <= 1 Then
        tlbCommessa.Down = False
        tlbLotti.Down = False
        tlbUbicazioni.Down = False
        tlbCommessa.Enabled = False
        tlbLotti.Enabled = False
        tlbUbicazioni.Enabled = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If bCommeca = True And tlbCommessa.Enabled = True Then tlbCommessa.Down = True
      If bLotto = True And tlbLotti.Enabled = True Then tlbLotti.Down = True
      If bUbicaz = True And tlbUbicazioni.Enabled = True Then tlbUbicazioni.Down = True
      '--------------------------------------------------------------------------------------------------------------
      ApriDataSet()
      '---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      '--------------------------------------------------------------------------------------------------------------
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      bOnLoading = False
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGLOTC_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    Try
      '--------------------------------------------------------------------------------------------------------------
      oMenu.SaveSettingBus("BNMGLOTC", "RECENT", ".", "RaggrCommeca", IIf(tlbCommessa.Down = True, "-1", "0").ToString, " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("BNMGLOTC", "RECENT", ".", "RaggrLotto", IIf(tlbLotti.Down = True, "-1", "0").ToString, " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("BNMGLOTC", "RECENT", ".", "RaggrUbicaz", IIf(tlbUbicazioni.Down = True, "-1", "0").ToString, " ", "NS.", "...", "...")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMMGLOTC_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcLotc.Dispose()
      dsLotc.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbCommessa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCommessa.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bOnLoading = True Then Return
      '--------------------------------------------------------------------------------------------------------------
      ApriDataSet()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub tlbLotti_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbLotti.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bOnLoading = True Then Return
      '--------------------------------------------------------------------------------------------------------------
      ApriDataSet()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub tlbUbicazioni_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbUbicazioni.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bOnLoading = True Then Return
      '--------------------------------------------------------------------------------------------------------------
      ApriDataSet()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub
#End Region

  Public Overridable Sub ApriDataSet()
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not dsLotc.Tables("LOTCPROTC") Is Nothing Then dsLotc.Tables("LOTCPROTC").Clear()
      '--------------------------------------------------------------------------------------------------------------
      If oCleArmd.GetDataLotc(tlbCommessa.Down, tlbLotti.Down, tlbUbicazioni.Down, dsLotc) = False Then Me.Close()
      dcLotc.DataSource = dsLotc.Tables("LOTCPROTC")
      grLotc.DataSource = dcLotc
      '--------------------------------------------------------------------------------------------------------------
      If oCleArmd.RitornaTagliaArticolo(dttTmp) = True Then
        For i As Integer = 1 To 24
          If NTSCStr(dttTmp.Rows(0)("tb_dest" & Microsoft.VisualBasic.Right("0" & i.ToString, 2))).Trim = "" Then
            grvLotc.Columns("apt_esist" & Microsoft.VisualBasic.Right("0" & i.ToString, 2)).Visible = False
            grvLotc.Columns("apt_prenot" & Microsoft.VisualBasic.Right("0" & i.ToString, 2)).Visible = False
            grvLotc.Columns("apt_ordin" & Microsoft.VisualBasic.Right("0" & i.ToString, 2)).Visible = False
            grvLotc.Columns("apt_impeg" & Microsoft.VisualBasic.Right("0" & i.ToString, 2)).Visible = False
          Else
            grvLotc.Columns("apt_esist" & Microsoft.VisualBasic.Right("0" & i.ToString, 2)).Caption = "(Esist.) " & _
              NTSCStr(dttTmp.Rows(0)("tb_dest" & Microsoft.VisualBasic.Right("0" & i.ToString, 2))).Trim
            grvLotc.Columns("apt_prenot" & Microsoft.VisualBasic.Right("0" & i.ToString, 2)).Caption = "(Pren.) " & _
              NTSCStr(dttTmp.Rows(0)("tb_dest" & Microsoft.VisualBasic.Right("0" & i.ToString, 2))).Trim
            grvLotc.Columns("apt_ordin" & Microsoft.VisualBasic.Right("0" & i.ToString, 2)).Caption = "(Ordin.) " & _
              NTSCStr(dttTmp.Rows(0)("tb_dest" & Microsoft.VisualBasic.Right("0" & i.ToString, 2))).Trim
            grvLotc.Columns("apt_impeg" & Microsoft.VisualBasic.Right("0" & i.ToString, 2)).Caption = "(Impegn.) " & _
              NTSCStr(dttTmp.Rows(0)("tb_dest" & Microsoft.VisualBasic.Right("0" & i.ToString, 2))).Trim
          End If
        Next i
      End If
      '---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
      apt_commeca.Visible = True
      xx_commeca.Visible = True
      If (tlbCommessa.Down = False) And (tlbLotti.Down = True Or tlbUbicazioni.Down = True) Then
        apt_commeca.Visible = False
        xx_commeca.Visible = False
      End If
      apt_lotto.Visible = True
      xx_lotto.Visible = True
      If (tlbLotti.Down = False) And (tlbCommessa.Down = True Or tlbUbicazioni.Down = True) Then
        apt_lotto.Visible = False
        xx_lotto.Visible = False
      End If
      apt_ubicaz.Visible = True
      If (tlbUbicazioni.Down = False) And (tlbCommessa.Down = True Or tlbLotti.Down = True) Then
        apt_ubicaz.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If bGescomm = False Then
        apt_commeca.Visible = False
        xx_commeca.Visible = False
      End If
      If bGeslotti = False Then
        apt_lotto.Visible = False
        xx_lotto.Visible = False
      End If
      If bGesubic = False Then apt_ubicaz.Visible = False
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Sub

End Class