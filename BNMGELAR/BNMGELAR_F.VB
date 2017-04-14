Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGELAR_F

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

#Region "Variabili"
  Public oCleElar As CLEMGELAR
  Public dsElar As DataSet = Nothing
  Public dcElar As BindingSource = New BindingSource
  Public oCallParams As CLE__CLDP
  Public strProntoMess As String = ""

  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbElabora As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents pnElar As NTSInformatica.NTSPanel
  Public WithEvents fmTipoElaborazione As NTSInformatica.NTSGroupBox
  Public WithEvents cmdArticoliDaDoc As NTSInformatica.NTSButton
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents edConto As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbConto As NTSInformatica.NTSLabel
  Public WithEvents lbXx_Conto As NTSInformatica.NTSLabel
  Public WithEvents lbDatagg As NTSInformatica.NTSLabel
  Public WithEvents edDatagg As NTSInformatica.NTSTextBoxData
  Public WithEvents fmListinoElabora As NTSInformatica.NTSGroupBox
  Public WithEvents fmListinoPartenza As NTSInformatica.NTSGroupBox
  Public WithEvents edListpar As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbListpar As NTSInformatica.NTSLabel
  Public WithEvents lbAppMis As NTSInformatica.NTSLabel
  Public WithEvents cbAppMis As NTSInformatica.NTSComboBox
  Public WithEvents edValore As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbValore As NTSInformatica.NTSLabel
  Public WithEvents edRicar1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbRicar1 As NTSInformatica.NTSLabel
  Public WithEvents edArrotond As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbArrotond As NTSInformatica.NTSLabel
  Public WithEvents lbRicar2 As NTSInformatica.NTSLabel
  Public WithEvents edRicar2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents fmElaborazione As NTSInformatica.NTSGroupBox
  Public WithEvents ckElaborazione As NTSInformatica.NTSCheckBox
  Public WithEvents ckAggiungiIVA As NTSInformatica.NTSCheckBox
  Public WithEvents edCambiopar As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCambiopar As NTSInformatica.NTSLabel
  Public WithEvents edValpar As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbValpar As NTSInformatica.NTSLabel
  Public WithEvents lbListpar3 As NTSInformatica.NTSLabel
  Public WithEvents lbListpar2 As NTSInformatica.NTSLabel
  Public WithEvents lbListpar1 As NTSInformatica.NTSLabel
  Public WithEvents edCambioelab As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCambioelab As NTSInformatica.NTSLabel
  Public WithEvents edValelab As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbValelab As NTSInformatica.NTSLabel
  Public WithEvents lbXx_Valelab As NTSInformatica.NTSLabel
  Public WithEvents edListelab As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbListelab As NTSInformatica.NTSLabel
  Public WithEvents lbScadprom As NTSInformatica.NTSLabel
  Public WithEvents edCodtpro As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCodtpro As NTSInformatica.NTSLabel
  Public WithEvents lbXx_Codtpro As NTSInformatica.NTSLabel
  Public WithEvents ckAggiornalistini As NTSInformatica.NTSCheckBox
  Public WithEvents lbStatus As NTSInformatica.NTSLabel
  Public WithEvents edScadprom As NTSInformatica.NTSTextBoxData
  Public WithEvents pnRight As NTSInformatica.NTSPanel
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents ckVisRisultati As NTSInformatica.NTSCheckBox
  Public WithEvents ckNetto As NTSInformatica.NTSCheckBox
  Public WithEvents fmPromo As NTSInformatica.NTSGroupBox
  Public WithEvents lbDesPromo As NTSInformatica.NTSLabel
  Public WithEvents edPromo As NTSInformatica.NTSTextBoxNum
  'Private components As System.ComponentModel.IContainer
  Public WithEvents lbXx_Valpar As NTSInformatica.NTSLabel
#End Region

  Public Overloads Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    '---------------------------------
    'questa funzione riceve gli eventi dall'ENTITY: rimappata rispetto a quella standard di FRM__CHILD
    'prima eseguo quella standard
    Dim strTmp() As String
    Dim i As Integer = 0

    If Not IsMyThrowRemoteEvent() Then Return 'il messaggio non è per questa form ...
    MyBase.GestisciEventiEntity(sender, e)

    Try
      '---------------------------------
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
          Case "SALTALIST:"
            Salta()
            Me.Refresh()
          Case "VISRISULT:"
            visRisult()
        End Select
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGELAR", "BEMGELAR", objTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128050437330625000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleElar = CType(objTmp, CLEMGELAR)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGELAR", strRemoteServer, strRemotePort)
    AddHandler oCleElar.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleElar.Init(oApp, NTSScript, oMenu.oCleComm, "tabanaz", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGELAR_F))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbElabora = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnElar = New NTSInformatica.NTSPanel
    Me.fmTipoVariazione = New NTSInformatica.NTSGroupBox
    Me.opScorporaIva = New NTSInformatica.NTSRadioButton
    Me.opRicarico = New NTSInformatica.NTSRadioButton
    Me.opValore = New NTSInformatica.NTSRadioButton
    Me.opPercentuale = New NTSInformatica.NTSRadioButton
    Me.ckNetto = New NTSInformatica.NTSCheckBox
    Me.fmElaborazione = New NTSInformatica.NTSGroupBox
    Me.ckElaborazione = New NTSInformatica.NTSCheckBox
    Me.pnRight = New NTSInformatica.NTSPanel
    Me.ckVisRisultati = New NTSInformatica.NTSCheckBox
    Me.lbArrotond = New NTSInformatica.NTSLabel
    Me.edArrotond = New NTSInformatica.NTSTextBoxNum
    Me.ckAggiungiIVA = New NTSInformatica.NTSCheckBox
    Me.lbRicar1 = New NTSInformatica.NTSLabel
    Me.lbRicar2 = New NTSInformatica.NTSLabel
    Me.edRicar1 = New NTSInformatica.NTSTextBoxNum
    Me.edRicar2 = New NTSInformatica.NTSTextBoxNum
    Me.lbValore = New NTSInformatica.NTSLabel
    Me.lbAppMis = New NTSInformatica.NTSLabel
    Me.edValore = New NTSInformatica.NTSTextBoxNum
    Me.cbAppMis = New NTSInformatica.NTSComboBox
    Me.lbStatus = New NTSInformatica.NTSLabel
    Me.fmListinoElabora = New NTSInformatica.NTSGroupBox
    Me.edScadprom = New NTSInformatica.NTSTextBoxData
    Me.ckAggiornalistini = New NTSInformatica.NTSCheckBox
    Me.lbScadprom = New NTSInformatica.NTSLabel
    Me.edCodtpro = New NTSInformatica.NTSTextBoxNum
    Me.lbCodtpro = New NTSInformatica.NTSLabel
    Me.lbXx_Codtpro = New NTSInformatica.NTSLabel
    Me.edCambioelab = New NTSInformatica.NTSTextBoxNum
    Me.lbCambioelab = New NTSInformatica.NTSLabel
    Me.edValelab = New NTSInformatica.NTSTextBoxNum
    Me.lbValelab = New NTSInformatica.NTSLabel
    Me.lbXx_Valelab = New NTSInformatica.NTSLabel
    Me.edListelab = New NTSInformatica.NTSTextBoxNum
    Me.lbListelab = New NTSInformatica.NTSLabel
    Me.fmListinoPartenza = New NTSInformatica.NTSGroupBox
    Me.fmPromo = New NTSInformatica.NTSGroupBox
    Me.cbPromozioni = New NTSInformatica.NTSComboBox
    Me.lbDesPromo = New NTSInformatica.NTSLabel
    Me.edPromo = New NTSInformatica.NTSTextBoxNum
    Me.lbListpar3 = New NTSInformatica.NTSLabel
    Me.lbListpar2 = New NTSInformatica.NTSLabel
    Me.lbListpar1 = New NTSInformatica.NTSLabel
    Me.edCambiopar = New NTSInformatica.NTSTextBoxNum
    Me.lbCambiopar = New NTSInformatica.NTSLabel
    Me.edValpar = New NTSInformatica.NTSTextBoxNum
    Me.lbValpar = New NTSInformatica.NTSLabel
    Me.lbXx_Valpar = New NTSInformatica.NTSLabel
    Me.edListpar = New NTSInformatica.NTSTextBoxNum
    Me.lbListpar = New NTSInformatica.NTSLabel
    Me.lbDatagg = New NTSInformatica.NTSLabel
    Me.edDatagg = New NTSInformatica.NTSTextBoxData
    Me.cmdArticoliDaDoc = New NTSInformatica.NTSButton
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.edConto = New NTSInformatica.NTSTextBoxNum
    Me.lbConto = New NTSInformatica.NTSLabel
    Me.lbXx_Conto = New NTSInformatica.NTSLabel
    Me.fmTipoElaborazione = New NTSInformatica.NTSGroupBox
    Me.cbTipoElaborazione = New NTSInformatica.NTSComboBox
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnElar, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnElar.SuspendLayout()
    CType(Me.fmTipoVariazione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmTipoVariazione.SuspendLayout()
    CType(Me.opScorporaIva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opRicarico.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opValore.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opPercentuale.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNetto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmElaborazione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmElaborazione.SuspendLayout()
    CType(Me.ckElaborazione.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnRight.SuspendLayout()
    CType(Me.ckVisRisultati.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edArrotond.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAggiungiIVA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edRicar1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edRicar2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edValore.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAppMis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmListinoElabora, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmListinoElabora.SuspendLayout()
    CType(Me.edScadprom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAggiornalistini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodtpro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCambioelab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edValelab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edListelab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmListinoPartenza, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmListinoPartenza.SuspendLayout()
    CType(Me.fmPromo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmPromo.SuspendLayout()
    CType(Me.cbPromozioni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPromo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCambiopar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edValpar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edListpar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatagg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmTipoElaborazione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmTipoElaborazione.SuspendLayout()
    CType(Me.cbTipoElaborazione.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.frmAuto.Appearance.BackColor = System.Drawing.SystemColors.GradientActiveCaption
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbElabora, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom})
    Me.NtsBarManager1.MaxItemId = 6
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbElabora), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 5
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 3
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 4
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnElar
    '
    Me.pnElar.AllowDrop = True
    Me.pnElar.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnElar.Appearance.Options.UseBackColor = True
    Me.pnElar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnElar.Controls.Add(Me.fmTipoVariazione)
    Me.pnElar.Controls.Add(Me.ckNetto)
    Me.pnElar.Controls.Add(Me.fmElaborazione)
    Me.pnElar.Controls.Add(Me.pnRight)
    Me.pnElar.Controls.Add(Me.lbStatus)
    Me.pnElar.Controls.Add(Me.fmListinoElabora)
    Me.pnElar.Controls.Add(Me.fmListinoPartenza)
    Me.pnElar.Controls.Add(Me.lbDatagg)
    Me.pnElar.Controls.Add(Me.edDatagg)
    Me.pnElar.Controls.Add(Me.cmdArticoliDaDoc)
    Me.pnElar.Controls.Add(Me.cmdSeleziona)
    Me.pnElar.Controls.Add(Me.edConto)
    Me.pnElar.Controls.Add(Me.lbConto)
    Me.pnElar.Controls.Add(Me.lbXx_Conto)
    Me.pnElar.Controls.Add(Me.fmTipoElaborazione)
    Me.pnElar.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnElar.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnElar.Location = New System.Drawing.Point(0, 30)
    Me.pnElar.Name = "pnElar"
    Me.pnElar.NTSActiveTrasparency = True
    Me.pnElar.Size = New System.Drawing.Size(748, 367)
    Me.pnElar.TabIndex = 0
    Me.pnElar.Text = "NtsPanel1"
    '
    'fmTipoVariazione
    '
    Me.fmTipoVariazione.AllowDrop = True
    Me.fmTipoVariazione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmTipoVariazione.Appearance.Options.UseBackColor = True
    Me.fmTipoVariazione.Controls.Add(Me.opScorporaIva)
    Me.fmTipoVariazione.Controls.Add(Me.opRicarico)
    Me.fmTipoVariazione.Controls.Add(Me.opValore)
    Me.fmTipoVariazione.Controls.Add(Me.opPercentuale)
    Me.fmTipoVariazione.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmTipoVariazione.Location = New System.Drawing.Point(466, 54)
    Me.fmTipoVariazione.Name = "fmTipoVariazione"
    Me.fmTipoVariazione.Size = New System.Drawing.Size(275, 60)
    Me.fmTipoVariazione.TabIndex = 151
    Me.fmTipoVariazione.Text = "Tipo Variazione : "
    '
    'opScorporaIva
    '
    Me.opScorporaIva.Cursor = System.Windows.Forms.Cursors.Default
    Me.opScorporaIva.Location = New System.Drawing.Point(111, 38)
    Me.opScorporaIva.Name = "opScorporaIva"
    Me.opScorporaIva.NTSCheckValue = "S"
    Me.opScorporaIva.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opScorporaIva.Properties.Appearance.Options.UseBackColor = True
    Me.opScorporaIva.Properties.Caption = "Scorpora Iva"
    Me.opScorporaIva.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opScorporaIva.Size = New System.Drawing.Size(86, 19)
    Me.opScorporaIva.TabIndex = 7
    '
    'opRicarico
    '
    Me.opRicarico.Cursor = System.Windows.Forms.Cursors.Default
    Me.opRicarico.Location = New System.Drawing.Point(111, 20)
    Me.opRicarico.Name = "opRicarico"
    Me.opRicarico.NTSCheckValue = "S"
    Me.opRicarico.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opRicarico.Properties.Appearance.Options.UseBackColor = True
    Me.opRicarico.Properties.Caption = "Ri&carico/Margine"
    Me.opRicarico.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opRicarico.Size = New System.Drawing.Size(101, 19)
    Me.opRicarico.TabIndex = 6
    '
    'opValore
    '
    Me.opValore.Cursor = System.Windows.Forms.Cursors.Default
    Me.opValore.Location = New System.Drawing.Point(10, 38)
    Me.opValore.Name = "opValore"
    Me.opValore.NTSCheckValue = "S"
    Me.opValore.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opValore.Properties.Appearance.Options.UseBackColor = True
    Me.opValore.Properties.Caption = "&Valore"
    Me.opValore.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opValore.Size = New System.Drawing.Size(56, 19)
    Me.opValore.TabIndex = 5
    '
    'opPercentuale
    '
    Me.opPercentuale.Cursor = System.Windows.Forms.Cursors.Default
    Me.opPercentuale.EditValue = True
    Me.opPercentuale.Location = New System.Drawing.Point(10, 20)
    Me.opPercentuale.Name = "opPercentuale"
    Me.opPercentuale.NTSCheckValue = "S"
    Me.opPercentuale.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opPercentuale.Properties.Appearance.Options.UseBackColor = True
    Me.opPercentuale.Properties.Caption = "&Percentuale"
    Me.opPercentuale.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opPercentuale.Size = New System.Drawing.Size(86, 19)
    Me.opPercentuale.TabIndex = 4
    '
    'ckNetto
    '
    Me.ckNetto.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckNetto.Location = New System.Drawing.Point(229, 46)
    Me.ckNetto.Name = "ckNetto"
    Me.ckNetto.NTSCheckValue = "S"
    Me.ckNetto.NTSUnCheckValue = "N"
    Me.ckNetto.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNetto.Properties.Appearance.Options.UseBackColor = True
    Me.ckNetto.Properties.Caption = "Netto sconti specifico Clienti/Fornitori"
    Me.ckNetto.Size = New System.Drawing.Size(207, 19)
    Me.ckNetto.TabIndex = 150
    '
    'fmElaborazione
    '
    Me.fmElaborazione.AllowDrop = True
    Me.fmElaborazione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmElaborazione.Appearance.Options.UseBackColor = True
    Me.fmElaborazione.Controls.Add(Me.ckElaborazione)
    Me.fmElaborazione.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmElaborazione.Location = New System.Drawing.Point(564, 332)
    Me.fmElaborazione.Name = "fmElaborazione"
    Me.fmElaborazione.ShowCaption = False
    Me.fmElaborazione.Size = New System.Drawing.Size(177, 28)
    Me.fmElaborazione.TabIndex = 147
    '
    'ckElaborazione
    '
    Me.ckElaborazione.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckElaborazione.Location = New System.Drawing.Point(10, 5)
    Me.ckElaborazione.Name = "ckElaborazione"
    Me.ckElaborazione.NTSCheckValue = "S"
    Me.ckElaborazione.NTSUnCheckValue = "N"
    Me.ckElaborazione.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckElaborazione.Properties.Appearance.Options.UseBackColor = True
    Me.ckElaborazione.Properties.Caption = "&Elaborazione automatica"
    Me.ckElaborazione.Size = New System.Drawing.Size(143, 19)
    Me.ckElaborazione.TabIndex = 147
    '
    'pnRight
    '
    Me.pnRight.AllowDrop = True
    Me.pnRight.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnRight.Appearance.Options.UseBackColor = True
    Me.pnRight.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnRight.Controls.Add(Me.ckVisRisultati)
    Me.pnRight.Controls.Add(Me.lbArrotond)
    Me.pnRight.Controls.Add(Me.edArrotond)
    Me.pnRight.Controls.Add(Me.ckAggiungiIVA)
    Me.pnRight.Controls.Add(Me.lbRicar1)
    Me.pnRight.Controls.Add(Me.lbRicar2)
    Me.pnRight.Controls.Add(Me.edRicar1)
    Me.pnRight.Controls.Add(Me.edRicar2)
    Me.pnRight.Controls.Add(Me.lbValore)
    Me.pnRight.Controls.Add(Me.lbAppMis)
    Me.pnRight.Controls.Add(Me.edValore)
    Me.pnRight.Controls.Add(Me.cbAppMis)
    Me.pnRight.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnRight.Location = New System.Drawing.Point(466, 117)
    Me.pnRight.Name = "pnRight"
    Me.pnRight.NTSActiveTrasparency = True
    Me.pnRight.Size = New System.Drawing.Size(281, 132)
    Me.pnRight.TabIndex = 149
    Me.pnRight.Text = "NtsPanel1"
    '
    'ckVisRisultati
    '
    Me.ckVisRisultati.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckVisRisultati.Location = New System.Drawing.Point(8, 110)
    Me.ckVisRisultati.Name = "ckVisRisultati"
    Me.ckVisRisultati.NTSCheckValue = "S"
    Me.ckVisRisultati.NTSUnCheckValue = "N"
    Me.ckVisRisultati.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckVisRisultati.Properties.Appearance.Options.UseBackColor = True
    Me.ckVisRisultati.Properties.Caption = "Visualizza risultati"
    Me.ckVisRisultati.Size = New System.Drawing.Size(108, 19)
    Me.ckVisRisultati.TabIndex = 148
    '
    'lbArrotond
    '
    Me.lbArrotond.AutoSize = True
    Me.lbArrotond.BackColor = System.Drawing.Color.Transparent
    Me.lbArrotond.Location = New System.Drawing.Point(5, 4)
    Me.lbArrotond.Name = "lbArrotond"
    Me.lbArrotond.NTSDbField = ""
    Me.lbArrotond.Size = New System.Drawing.Size(134, 13)
    Me.lbArrotond.TabIndex = 136
    Me.lbArrotond.Text = "Arrotondamento di default"
    Me.lbArrotond.Tooltip = ""
    Me.lbArrotond.UseMnemonic = False
    '
    'edArrotond
    '
    Me.edArrotond.Cursor = System.Windows.Forms.Cursors.Default
    Me.edArrotond.Location = New System.Drawing.Point(144, 1)
    Me.edArrotond.Name = "edArrotond"
    Me.edArrotond.NTSDbField = ""
    Me.edArrotond.NTSFormat = "0"
    Me.edArrotond.NTSForzaVisZoom = False
    Me.edArrotond.NTSOldValue = ""
    Me.edArrotond.Properties.Appearance.Options.UseTextOptions = True
    Me.edArrotond.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edArrotond.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edArrotond.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edArrotond.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edArrotond.Properties.MaxLength = 65536
    Me.edArrotond.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edArrotond.Size = New System.Drawing.Size(131, 20)
    Me.edArrotond.TabIndex = 137
    '
    'ckAggiungiIVA
    '
    Me.ckAggiungiIVA.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAggiungiIVA.Location = New System.Drawing.Point(8, 88)
    Me.ckAggiungiIVA.Name = "ckAggiungiIVA"
    Me.ckAggiungiIVA.NTSCheckValue = "S"
    Me.ckAggiungiIVA.NTSUnCheckValue = "N"
    Me.ckAggiungiIVA.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAggiungiIVA.Properties.Appearance.Options.UseBackColor = True
    Me.ckAggiungiIVA.Properties.Caption = "Aggiungi l'IVA al valore del listino di partenza"
    Me.ckAggiungiIVA.Size = New System.Drawing.Size(242, 19)
    Me.ckAggiungiIVA.TabIndex = 146
    '
    'lbRicar1
    '
    Me.lbRicar1.AutoSize = True
    Me.lbRicar1.BackColor = System.Drawing.Color.Transparent
    Me.lbRicar1.Location = New System.Drawing.Point(5, 25)
    Me.lbRicar1.Name = "lbRicar1"
    Me.lbRicar1.NTSDbField = ""
    Me.lbRicar1.Size = New System.Drawing.Size(131, 13)
    Me.lbRicar1.TabIndex = 138
    Me.lbRicar1.Text = "Percentuale di incremento"
    Me.lbRicar1.Tooltip = ""
    Me.lbRicar1.UseMnemonic = False
    '
    'lbRicar2
    '
    Me.lbRicar2.AutoSize = True
    Me.lbRicar2.BackColor = System.Drawing.Color.Transparent
    Me.lbRicar2.Location = New System.Drawing.Point(204, 26)
    Me.lbRicar2.Name = "lbRicar2"
    Me.lbRicar2.NTSDbField = ""
    Me.lbRicar2.Size = New System.Drawing.Size(15, 13)
    Me.lbRicar2.TabIndex = 145
    Me.lbRicar2.Text = "+"
    Me.lbRicar2.Tooltip = ""
    Me.lbRicar2.UseMnemonic = False
    '
    'edRicar1
    '
    Me.edRicar1.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edRicar1.Location = New System.Drawing.Point(145, 22)
    Me.edRicar1.Name = "edRicar1"
    Me.edRicar1.NTSDbField = ""
    Me.edRicar1.NTSFormat = "0"
    Me.edRicar1.NTSForzaVisZoom = False
    Me.edRicar1.NTSOldValue = ""
    Me.edRicar1.Properties.Appearance.Options.UseTextOptions = True
    Me.edRicar1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edRicar1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edRicar1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edRicar1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edRicar1.Properties.MaxLength = 65536
    Me.edRicar1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edRicar1.Size = New System.Drawing.Size(55, 20)
    Me.edRicar1.TabIndex = 139
    '
    'edRicar2
    '
    Me.edRicar2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edRicar2.Location = New System.Drawing.Point(220, 22)
    Me.edRicar2.Name = "edRicar2"
    Me.edRicar2.NTSDbField = ""
    Me.edRicar2.NTSFormat = "0"
    Me.edRicar2.NTSForzaVisZoom = False
    Me.edRicar2.NTSOldValue = ""
    Me.edRicar2.Properties.Appearance.Options.UseTextOptions = True
    Me.edRicar2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edRicar2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edRicar2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edRicar2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edRicar2.Properties.MaxLength = 65536
    Me.edRicar2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edRicar2.Size = New System.Drawing.Size(55, 20)
    Me.edRicar2.TabIndex = 144
    '
    'lbValore
    '
    Me.lbValore.AutoSize = True
    Me.lbValore.BackColor = System.Drawing.Color.Transparent
    Me.lbValore.Location = New System.Drawing.Point(4, 47)
    Me.lbValore.Name = "lbValore"
    Me.lbValore.NTSDbField = ""
    Me.lbValore.Size = New System.Drawing.Size(104, 13)
    Me.lbValore.TabIndex = 140
    Me.lbValore.Text = "Valore di incremento"
    Me.lbValore.Tooltip = ""
    Me.lbValore.UseMnemonic = False
    '
    'lbAppMis
    '
    Me.lbAppMis.AutoSize = True
    Me.lbAppMis.BackColor = System.Drawing.Color.Transparent
    Me.lbAppMis.Location = New System.Drawing.Point(4, 69)
    Me.lbAppMis.Name = "lbAppMis"
    Me.lbAppMis.NTSDbField = ""
    Me.lbAppMis.Size = New System.Drawing.Size(86, 13)
    Me.lbAppMis.TabIndex = 143
    Me.lbAppMis.Text = "Applicata a U.M."
    Me.lbAppMis.Tooltip = ""
    Me.lbAppMis.UseMnemonic = False
    '
    'edValore
    '
    Me.edValore.Cursor = System.Windows.Forms.Cursors.Default
    Me.edValore.Location = New System.Drawing.Point(145, 44)
    Me.edValore.Name = "edValore"
    Me.edValore.NTSDbField = ""
    Me.edValore.NTSFormat = "0"
    Me.edValore.NTSForzaVisZoom = False
    Me.edValore.NTSOldValue = ""
    Me.edValore.Properties.Appearance.Options.UseTextOptions = True
    Me.edValore.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edValore.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edValore.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edValore.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edValore.Properties.MaxLength = 65536
    Me.edValore.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edValore.Size = New System.Drawing.Size(130, 20)
    Me.edValore.TabIndex = 141
    '
    'cbAppMis
    '
    Me.cbAppMis.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAppMis.DataSource = Nothing
    Me.cbAppMis.DisplayMember = ""
    Me.cbAppMis.Location = New System.Drawing.Point(145, 66)
    Me.cbAppMis.Name = "cbAppMis"
    Me.cbAppMis.NTSDbField = ""
    Me.cbAppMis.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAppMis.Properties.DropDownRows = 30
    Me.cbAppMis.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAppMis.SelectedValue = ""
    Me.cbAppMis.Size = New System.Drawing.Size(130, 20)
    Me.cbAppMis.TabIndex = 142
    Me.cbAppMis.ValueMember = ""
    '
    'lbStatus
    '
    Me.lbStatus.BackColor = System.Drawing.Color.Transparent
    Me.lbStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbStatus.Location = New System.Drawing.Point(9, 347)
    Me.lbStatus.Name = "lbStatus"
    Me.lbStatus.NTSDbField = ""
    Me.lbStatus.Size = New System.Drawing.Size(550, 13)
    Me.lbStatus.TabIndex = 148
    Me.lbStatus.Text = "Pronto."
    Me.lbStatus.Tooltip = ""
    Me.lbStatus.UseMnemonic = False
    '
    'fmListinoElabora
    '
    Me.fmListinoElabora.AllowDrop = True
    Me.fmListinoElabora.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmListinoElabora.Appearance.Options.UseBackColor = True
    Me.fmListinoElabora.Controls.Add(Me.edScadprom)
    Me.fmListinoElabora.Controls.Add(Me.ckAggiornalistini)
    Me.fmListinoElabora.Controls.Add(Me.lbScadprom)
    Me.fmListinoElabora.Controls.Add(Me.edCodtpro)
    Me.fmListinoElabora.Controls.Add(Me.lbCodtpro)
    Me.fmListinoElabora.Controls.Add(Me.lbXx_Codtpro)
    Me.fmListinoElabora.Controls.Add(Me.edCambioelab)
    Me.fmListinoElabora.Controls.Add(Me.lbCambioelab)
    Me.fmListinoElabora.Controls.Add(Me.edValelab)
    Me.fmListinoElabora.Controls.Add(Me.lbValelab)
    Me.fmListinoElabora.Controls.Add(Me.lbXx_Valelab)
    Me.fmListinoElabora.Controls.Add(Me.edListelab)
    Me.fmListinoElabora.Controls.Add(Me.lbListelab)
    Me.fmListinoElabora.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmListinoElabora.Location = New System.Drawing.Point(7, 252)
    Me.fmListinoElabora.Name = "fmListinoElabora"
    Me.fmListinoElabora.ShowCaption = False
    Me.fmListinoElabora.Size = New System.Drawing.Size(734, 76)
    Me.fmListinoElabora.TabIndex = 134
    '
    'edScadprom
    '
    Me.edScadprom.Cursor = System.Windows.Forms.Cursors.Default
    Me.edScadprom.Location = New System.Drawing.Point(589, 49)
    Me.edScadprom.Name = "edScadprom"
    Me.edScadprom.NTSDbField = ""
    Me.edScadprom.NTSForzaVisZoom = False
    Me.edScadprom.NTSOldValue = ""
    Me.edScadprom.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edScadprom.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edScadprom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edScadprom.Properties.MaxLength = 65536
    Me.edScadprom.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edScadprom.Size = New System.Drawing.Size(85, 20)
    Me.edScadprom.TabIndex = 160
    '
    'ckAggiornalistini
    '
    Me.ckAggiornalistini.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAggiornalistini.Location = New System.Drawing.Point(184, 6)
    Me.ckAggiornalistini.Name = "ckAggiornalistini"
    Me.ckAggiornalistini.NTSCheckValue = "S"
    Me.ckAggiornalistini.NTSUnCheckValue = "N"
    Me.ckAggiornalistini.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAggiornalistini.Properties.Appearance.Options.UseBackColor = True
    Me.ckAggiornalistini.Properties.Caption = "&Aggiorna listini dal 2 all' 8"
    Me.ckAggiornalistini.Size = New System.Drawing.Size(143, 19)
    Me.ckAggiornalistini.TabIndex = 159
    '
    'lbScadprom
    '
    Me.lbScadprom.AutoSize = True
    Me.lbScadprom.BackColor = System.Drawing.Color.Transparent
    Me.lbScadprom.Location = New System.Drawing.Point(447, 52)
    Me.lbScadprom.Name = "lbScadprom"
    Me.lbScadprom.NTSDbField = ""
    Me.lbScadprom.Size = New System.Drawing.Size(136, 13)
    Me.lbScadprom.TabIndex = 154
    Me.lbScadprom.Text = "Data scadenza promozione"
    Me.lbScadprom.Tooltip = ""
    Me.lbScadprom.UseMnemonic = False
    '
    'edCodtpro
    '
    Me.edCodtpro.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodtpro.Location = New System.Drawing.Point(111, 49)
    Me.edCodtpro.Name = "edCodtpro"
    Me.edCodtpro.NTSDbField = ""
    Me.edCodtpro.NTSFormat = "0"
    Me.edCodtpro.NTSForzaVisZoom = False
    Me.edCodtpro.NTSOldValue = ""
    Me.edCodtpro.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodtpro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodtpro.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodtpro.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodtpro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodtpro.Properties.MaxLength = 65536
    Me.edCodtpro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodtpro.Size = New System.Drawing.Size(67, 20)
    Me.edCodtpro.TabIndex = 152
    '
    'lbCodtpro
    '
    Me.lbCodtpro.AutoSize = True
    Me.lbCodtpro.BackColor = System.Drawing.Color.Transparent
    Me.lbCodtpro.Location = New System.Drawing.Point(8, 52)
    Me.lbCodtpro.Name = "lbCodtpro"
    Me.lbCodtpro.NTSDbField = ""
    Me.lbCodtpro.Size = New System.Drawing.Size(97, 13)
    Me.lbCodtpro.TabIndex = 151
    Me.lbCodtpro.Text = "Codice promozione"
    Me.lbCodtpro.Tooltip = ""
    Me.lbCodtpro.UseMnemonic = False
    '
    'lbXx_Codtpro
    '
    Me.lbXx_Codtpro.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codtpro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codtpro.Location = New System.Drawing.Point(183, 49)
    Me.lbXx_Codtpro.Name = "lbXx_Codtpro"
    Me.lbXx_Codtpro.NTSDbField = ""
    Me.lbXx_Codtpro.Size = New System.Drawing.Size(258, 20)
    Me.lbXx_Codtpro.TabIndex = 150
    Me.lbXx_Codtpro.Tooltip = ""
    Me.lbXx_Codtpro.UseMnemonic = False
    '
    'edCambioelab
    '
    Me.edCambioelab.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCambioelab.Location = New System.Drawing.Point(466, 27)
    Me.edCambioelab.Name = "edCambioelab"
    Me.edCambioelab.NTSDbField = ""
    Me.edCambioelab.NTSFormat = "0"
    Me.edCambioelab.NTSForzaVisZoom = False
    Me.edCambioelab.NTSOldValue = ""
    Me.edCambioelab.Properties.Appearance.Options.UseTextOptions = True
    Me.edCambioelab.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCambioelab.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCambioelab.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCambioelab.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCambioelab.Properties.MaxLength = 65536
    Me.edCambioelab.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCambioelab.Size = New System.Drawing.Size(208, 20)
    Me.edCambioelab.TabIndex = 149
    '
    'lbCambioelab
    '
    Me.lbCambioelab.AutoSize = True
    Me.lbCambioelab.BackColor = System.Drawing.Color.Transparent
    Me.lbCambioelab.Location = New System.Drawing.Point(447, 32)
    Me.lbCambioelab.Name = "lbCambioelab"
    Me.lbCambioelab.NTSDbField = ""
    Me.lbCambioelab.Size = New System.Drawing.Size(17, 13)
    Me.lbCambioelab.TabIndex = 148
    Me.lbCambioelab.Text = " / "
    Me.lbCambioelab.Tooltip = ""
    Me.lbCambioelab.UseMnemonic = False
    '
    'edValelab
    '
    Me.edValelab.Cursor = System.Windows.Forms.Cursors.Default
    Me.edValelab.Location = New System.Drawing.Point(111, 27)
    Me.edValelab.Name = "edValelab"
    Me.edValelab.NTSDbField = ""
    Me.edValelab.NTSFormat = "0"
    Me.edValelab.NTSForzaVisZoom = False
    Me.edValelab.NTSOldValue = ""
    Me.edValelab.Properties.Appearance.Options.UseTextOptions = True
    Me.edValelab.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edValelab.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edValelab.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edValelab.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edValelab.Properties.MaxLength = 65536
    Me.edValelab.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edValelab.Size = New System.Drawing.Size(67, 20)
    Me.edValelab.TabIndex = 146
    '
    'lbValelab
    '
    Me.lbValelab.AutoSize = True
    Me.lbValelab.BackColor = System.Drawing.Color.Transparent
    Me.lbValelab.Location = New System.Drawing.Point(8, 30)
    Me.lbValelab.Name = "lbValelab"
    Me.lbValelab.NTSDbField = ""
    Me.lbValelab.Size = New System.Drawing.Size(74, 13)
    Me.lbValelab.TabIndex = 145
    Me.lbValelab.Text = "Valuta/cambio"
    Me.lbValelab.Tooltip = ""
    Me.lbValelab.UseMnemonic = False
    '
    'lbXx_Valelab
    '
    Me.lbXx_Valelab.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Valelab.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Valelab.Location = New System.Drawing.Point(183, 27)
    Me.lbXx_Valelab.Name = "lbXx_Valelab"
    Me.lbXx_Valelab.NTSDbField = ""
    Me.lbXx_Valelab.Size = New System.Drawing.Size(258, 20)
    Me.lbXx_Valelab.TabIndex = 144
    Me.lbXx_Valelab.Tooltip = ""
    Me.lbXx_Valelab.UseMnemonic = False
    '
    'edListelab
    '
    Me.edListelab.Cursor = System.Windows.Forms.Cursors.Default
    Me.edListelab.Location = New System.Drawing.Point(111, 5)
    Me.edListelab.Name = "edListelab"
    Me.edListelab.NTSDbField = ""
    Me.edListelab.NTSFormat = "0"
    Me.edListelab.NTSForzaVisZoom = False
    Me.edListelab.NTSOldValue = ""
    Me.edListelab.Properties.Appearance.Options.UseTextOptions = True
    Me.edListelab.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edListelab.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edListelab.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edListelab.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edListelab.Properties.MaxLength = 65536
    Me.edListelab.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edListelab.Size = New System.Drawing.Size(67, 20)
    Me.edListelab.TabIndex = 143
    '
    'lbListelab
    '
    Me.lbListelab.AutoSize = True
    Me.lbListelab.BackColor = System.Drawing.Color.Transparent
    Me.lbListelab.Location = New System.Drawing.Point(8, 8)
    Me.lbListelab.Name = "lbListelab"
    Me.lbListelab.NTSDbField = ""
    Me.lbListelab.Size = New System.Drawing.Size(101, 13)
    Me.lbListelab.TabIndex = 142
    Me.lbListelab.Text = "Listino da elaborare"
    Me.lbListelab.Tooltip = ""
    Me.lbListelab.UseMnemonic = False
    '
    'fmListinoPartenza
    '
    Me.fmListinoPartenza.AllowDrop = True
    Me.fmListinoPartenza.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmListinoPartenza.Appearance.Options.UseBackColor = True
    Me.fmListinoPartenza.Controls.Add(Me.fmPromo)
    Me.fmListinoPartenza.Controls.Add(Me.lbListpar3)
    Me.fmListinoPartenza.Controls.Add(Me.lbListpar2)
    Me.fmListinoPartenza.Controls.Add(Me.lbListpar1)
    Me.fmListinoPartenza.Controls.Add(Me.edCambiopar)
    Me.fmListinoPartenza.Controls.Add(Me.lbCambiopar)
    Me.fmListinoPartenza.Controls.Add(Me.edValpar)
    Me.fmListinoPartenza.Controls.Add(Me.lbValpar)
    Me.fmListinoPartenza.Controls.Add(Me.lbXx_Valpar)
    Me.fmListinoPartenza.Controls.Add(Me.edListpar)
    Me.fmListinoPartenza.Controls.Add(Me.lbListpar)
    Me.fmListinoPartenza.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmListinoPartenza.Location = New System.Drawing.Point(7, 92)
    Me.fmListinoPartenza.Name = "fmListinoPartenza"
    Me.fmListinoPartenza.ShowCaption = False
    Me.fmListinoPartenza.Size = New System.Drawing.Size(453, 154)
    Me.fmListinoPartenza.TabIndex = 133
    '
    'fmPromo
    '
    Me.fmPromo.AllowDrop = True
    Me.fmPromo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmPromo.Appearance.Options.UseBackColor = True
    Me.fmPromo.Controls.Add(Me.cbPromozioni)
    Me.fmPromo.Controls.Add(Me.lbDesPromo)
    Me.fmPromo.Controls.Add(Me.edPromo)
    Me.fmPromo.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmPromo.Location = New System.Drawing.Point(5, 94)
    Me.fmPromo.Name = "fmPromo"
    Me.fmPromo.Size = New System.Drawing.Size(443, 50)
    Me.fmPromo.TabIndex = 150
    Me.fmPromo.Text = "Promozioni"
    '
    'cbPromozioni
    '
    Me.cbPromozioni.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPromozioni.DataSource = Nothing
    Me.cbPromozioni.DisplayMember = ""
    Me.cbPromozioni.Location = New System.Drawing.Point(6, 23)
    Me.cbPromozioni.Name = "cbPromozioni"
    Me.cbPromozioni.NTSDbField = ""
    Me.cbPromozioni.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPromozioni.Properties.DropDownRows = 30
    Me.cbPromozioni.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPromozioni.SelectedValue = ""
    Me.cbPromozioni.Size = New System.Drawing.Size(166, 20)
    Me.cbPromozioni.TabIndex = 162
    Me.cbPromozioni.ValueMember = ""
    '
    'lbDesPromo
    '
    Me.lbDesPromo.BackColor = System.Drawing.Color.Transparent
    Me.lbDesPromo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesPromo.Location = New System.Drawing.Point(242, 23)
    Me.lbDesPromo.Name = "lbDesPromo"
    Me.lbDesPromo.NTSDbField = ""
    Me.lbDesPromo.Size = New System.Drawing.Size(194, 20)
    Me.lbDesPromo.TabIndex = 4
    Me.lbDesPromo.Tooltip = ""
    Me.lbDesPromo.UseMnemonic = False
    '
    'edPromo
    '
    Me.edPromo.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edPromo.EditValue = "0"
    Me.edPromo.Enabled = False
    Me.edPromo.Location = New System.Drawing.Point(179, 23)
    Me.edPromo.Name = "edPromo"
    Me.edPromo.NTSDbField = ""
    Me.edPromo.NTSFormat = "0"
    Me.edPromo.NTSForzaVisZoom = False
    Me.edPromo.NTSOldValue = ""
    Me.edPromo.Properties.Appearance.Options.UseTextOptions = True
    Me.edPromo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPromo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPromo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPromo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPromo.Properties.MaxLength = 65536
    Me.edPromo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPromo.Size = New System.Drawing.Size(57, 20)
    Me.edPromo.TabIndex = 3
    '
    'lbListpar3
    '
    Me.lbListpar3.AutoSize = True
    Me.lbListpar3.BackColor = System.Drawing.Color.Transparent
    Me.lbListpar3.Location = New System.Drawing.Point(183, 28)
    Me.lbListpar3.Name = "lbListpar3"
    Me.lbListpar3.NTSDbField = ""
    Me.lbListpar3.Size = New System.Drawing.Size(98, 13)
    Me.lbListpar3.TabIndex = 146
    Me.lbListpar3.Text = "(-2 = Costo Medio)"
    Me.lbListpar3.Tooltip = ""
    Me.lbListpar3.UseMnemonic = False
    '
    'lbListpar2
    '
    Me.lbListpar2.AutoSize = True
    Me.lbListpar2.BackColor = System.Drawing.Color.Transparent
    Me.lbListpar2.Location = New System.Drawing.Point(183, 15)
    Me.lbListpar2.Name = "lbListpar2"
    Me.lbListpar2.NTSDbField = ""
    Me.lbListpar2.Size = New System.Drawing.Size(195, 13)
    Me.lbListpar2.TabIndex = 145
    Me.lbListpar2.Text = "(-1 = Ultimo Costo con Oneri accessori)"
    Me.lbListpar2.Tooltip = ""
    Me.lbListpar2.UseMnemonic = False
    '
    'lbListpar1
    '
    Me.lbListpar1.AutoSize = True
    Me.lbListpar1.BackColor = System.Drawing.Color.Transparent
    Me.lbListpar1.Location = New System.Drawing.Point(183, 2)
    Me.lbListpar1.Name = "lbListpar1"
    Me.lbListpar1.NTSDbField = ""
    Me.lbListpar1.Size = New System.Drawing.Size(143, 13)
    Me.lbListpar1.TabIndex = 144
    Me.lbListpar1.Text = "( 1 - 999) (0 = Ultimo Costo)"
    Me.lbListpar1.Tooltip = ""
    Me.lbListpar1.UseMnemonic = False
    '
    'edCambiopar
    '
    Me.edCambiopar.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCambiopar.Location = New System.Drawing.Point(110, 67)
    Me.edCambiopar.Name = "edCambiopar"
    Me.edCambiopar.NTSDbField = ""
    Me.edCambiopar.NTSFormat = "0"
    Me.edCambiopar.NTSForzaVisZoom = False
    Me.edCambiopar.NTSOldValue = ""
    Me.edCambiopar.Properties.Appearance.Options.UseTextOptions = True
    Me.edCambiopar.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCambiopar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCambiopar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCambiopar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCambiopar.Properties.MaxLength = 65536
    Me.edCambiopar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCambiopar.Size = New System.Drawing.Size(208, 20)
    Me.edCambiopar.TabIndex = 137
    '
    'lbCambiopar
    '
    Me.lbCambiopar.AutoSize = True
    Me.lbCambiopar.BackColor = System.Drawing.Color.Transparent
    Me.lbCambiopar.Location = New System.Drawing.Point(8, 70)
    Me.lbCambiopar.Name = "lbCambiopar"
    Me.lbCambiopar.NTSDbField = ""
    Me.lbCambiopar.Size = New System.Drawing.Size(42, 13)
    Me.lbCambiopar.TabIndex = 136
    Me.lbCambiopar.Text = "Cambio"
    Me.lbCambiopar.Tooltip = ""
    Me.lbCambiopar.UseMnemonic = False
    '
    'edValpar
    '
    Me.edValpar.Cursor = System.Windows.Forms.Cursors.Default
    Me.edValpar.Location = New System.Drawing.Point(110, 45)
    Me.edValpar.Name = "edValpar"
    Me.edValpar.NTSDbField = ""
    Me.edValpar.NTSFormat = "0"
    Me.edValpar.NTSForzaVisZoom = False
    Me.edValpar.NTSOldValue = ""
    Me.edValpar.Properties.Appearance.Options.UseTextOptions = True
    Me.edValpar.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edValpar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edValpar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edValpar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edValpar.Properties.MaxLength = 65536
    Me.edValpar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edValpar.Size = New System.Drawing.Size(67, 20)
    Me.edValpar.TabIndex = 134
    '
    'lbValpar
    '
    Me.lbValpar.AutoSize = True
    Me.lbValpar.BackColor = System.Drawing.Color.Transparent
    Me.lbValpar.Location = New System.Drawing.Point(8, 48)
    Me.lbValpar.Name = "lbValpar"
    Me.lbValpar.NTSDbField = ""
    Me.lbValpar.Size = New System.Drawing.Size(37, 13)
    Me.lbValpar.TabIndex = 133
    Me.lbValpar.Text = "Valuta"
    Me.lbValpar.Tooltip = ""
    Me.lbValpar.UseMnemonic = False
    '
    'lbXx_Valpar
    '
    Me.lbXx_Valpar.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Valpar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Valpar.Location = New System.Drawing.Point(183, 44)
    Me.lbXx_Valpar.Name = "lbXx_Valpar"
    Me.lbXx_Valpar.NTSDbField = ""
    Me.lbXx_Valpar.Size = New System.Drawing.Size(258, 20)
    Me.lbXx_Valpar.TabIndex = 132
    Me.lbXx_Valpar.Tooltip = ""
    Me.lbXx_Valpar.UseMnemonic = False
    '
    'edListpar
    '
    Me.edListpar.Cursor = System.Windows.Forms.Cursors.Default
    Me.edListpar.Location = New System.Drawing.Point(110, 12)
    Me.edListpar.Name = "edListpar"
    Me.edListpar.NTSDbField = ""
    Me.edListpar.NTSFormat = "0"
    Me.edListpar.NTSForzaVisZoom = False
    Me.edListpar.NTSOldValue = ""
    Me.edListpar.Properties.Appearance.Options.UseTextOptions = True
    Me.edListpar.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edListpar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edListpar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edListpar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edListpar.Properties.MaxLength = 65536
    Me.edListpar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edListpar.Size = New System.Drawing.Size(67, 20)
    Me.edListpar.TabIndex = 131
    '
    'lbListpar
    '
    Me.lbListpar.AutoSize = True
    Me.lbListpar.BackColor = System.Drawing.Color.Transparent
    Me.lbListpar.Location = New System.Drawing.Point(8, 15)
    Me.lbListpar.Name = "lbListpar"
    Me.lbListpar.NTSDbField = ""
    Me.lbListpar.Size = New System.Drawing.Size(94, 13)
    Me.lbListpar.TabIndex = 130
    Me.lbListpar.Text = "Listino di partenza"
    Me.lbListpar.Tooltip = ""
    Me.lbListpar.UseMnemonic = False
    '
    'lbDatagg
    '
    Me.lbDatagg.AutoSize = True
    Me.lbDatagg.BackColor = System.Drawing.Color.Transparent
    Me.lbDatagg.Location = New System.Drawing.Point(229, 23)
    Me.lbDatagg.Name = "lbDatagg"
    Me.lbDatagg.NTSDbField = ""
    Me.lbDatagg.Size = New System.Drawing.Size(93, 13)
    Me.lbDatagg.TabIndex = 132
    Me.lbDatagg.Text = "Data inizio validità"
    Me.lbDatagg.Tooltip = ""
    Me.lbDatagg.UseMnemonic = False
    '
    'edDatagg
    '
    Me.edDatagg.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatagg.Location = New System.Drawing.Point(375, 20)
    Me.edDatagg.Name = "edDatagg"
    Me.edDatagg.NTSDbField = ""
    Me.edDatagg.NTSForzaVisZoom = False
    Me.edDatagg.NTSOldValue = ""
    Me.edDatagg.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatagg.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatagg.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatagg.Properties.MaxLength = 65536
    Me.edDatagg.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatagg.Size = New System.Drawing.Size(85, 20)
    Me.edDatagg.TabIndex = 131
    '
    'cmdArticoliDaDoc
    '
    Me.cmdArticoliDaDoc.ImageText = ""
    Me.cmdArticoliDaDoc.Location = New System.Drawing.Point(466, 26)
    Me.cmdArticoliDaDoc.Name = "cmdArticoliDaDoc"
    Me.cmdArticoliDaDoc.Size = New System.Drawing.Size(275, 23)
    Me.cmdArticoliDaDoc.TabIndex = 130
    Me.cmdArticoliDaDoc.Text = "Seleziona articoli da documenti di magazzino..."
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(466, 3)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.Size = New System.Drawing.Size(275, 23)
    Me.cmdSeleziona.TabIndex = 129
    Me.cmdSeleziona.Text = "&Seleziona Articoli..."
    '
    'edConto
    '
    Me.edConto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edConto.Location = New System.Drawing.Point(117, 68)
    Me.edConto.Name = "edConto"
    Me.edConto.NTSDbField = ""
    Me.edConto.NTSFormat = "0"
    Me.edConto.NTSForzaVisZoom = False
    Me.edConto.NTSOldValue = ""
    Me.edConto.Properties.Appearance.Options.UseTextOptions = True
    Me.edConto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edConto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edConto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edConto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edConto.Properties.MaxLength = 65536
    Me.edConto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edConto.Size = New System.Drawing.Size(91, 20)
    Me.edConto.TabIndex = 128
    '
    'lbConto
    '
    Me.lbConto.AutoSize = True
    Me.lbConto.BackColor = System.Drawing.Color.Transparent
    Me.lbConto.Location = New System.Drawing.Point(15, 71)
    Me.lbConto.Name = "lbConto"
    Me.lbConto.NTSDbField = ""
    Me.lbConto.Size = New System.Drawing.Size(36, 13)
    Me.lbConto.TabIndex = 127
    Me.lbConto.Text = "Conto"
    Me.lbConto.Tooltip = ""
    Me.lbConto.UseMnemonic = False
    '
    'lbXx_Conto
    '
    Me.lbXx_Conto.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Conto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Conto.Location = New System.Drawing.Point(214, 68)
    Me.lbXx_Conto.Name = "lbXx_Conto"
    Me.lbXx_Conto.NTSDbField = ""
    Me.lbXx_Conto.Size = New System.Drawing.Size(246, 20)
    Me.lbXx_Conto.TabIndex = 126
    Me.lbXx_Conto.Tooltip = ""
    Me.lbXx_Conto.UseMnemonic = False
    '
    'fmTipoElaborazione
    '
    Me.fmTipoElaborazione.AllowDrop = True
    Me.fmTipoElaborazione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmTipoElaborazione.Appearance.Options.UseBackColor = True
    Me.fmTipoElaborazione.Controls.Add(Me.cbTipoElaborazione)
    Me.fmTipoElaborazione.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmTipoElaborazione.Location = New System.Drawing.Point(7, 5)
    Me.fmTipoElaborazione.Name = "fmTipoElaborazione"
    Me.fmTipoElaborazione.Size = New System.Drawing.Size(216, 60)
    Me.fmTipoElaborazione.TabIndex = 121
    Me.fmTipoElaborazione.Text = "Aggiornamento/creazione Listino di tipo"
    '
    'cbTipoElaborazione
    '
    Me.cbTipoElaborazione.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipoElaborazione.DataSource = Nothing
    Me.cbTipoElaborazione.DisplayMember = ""
    Me.cbTipoElaborazione.Location = New System.Drawing.Point(11, 27)
    Me.cbTipoElaborazione.Name = "cbTipoElaborazione"
    Me.cbTipoElaborazione.NTSDbField = ""
    Me.cbTipoElaborazione.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipoElaborazione.Properties.DropDownRows = 30
    Me.cbTipoElaborazione.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipoElaborazione.SelectedValue = ""
    Me.cbTipoElaborazione.Size = New System.Drawing.Size(190, 20)
    Me.cbTipoElaborazione.TabIndex = 161
    Me.cbTipoElaborazione.ValueMember = ""
    '
    'FRMMGELAR_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(748, 397)
    Me.Controls.Add(Me.pnElar)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.KeyPreview = False
    Me.MaximizeBox = False
    Me.Name = "FRMMGELAR_F"
    Me.Text = "VARIAZIONE PREZZI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnElar, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnElar.ResumeLayout(False)
    Me.pnElar.PerformLayout()
    CType(Me.fmTipoVariazione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmTipoVariazione.ResumeLayout(False)
    Me.fmTipoVariazione.PerformLayout()
    CType(Me.opScorporaIva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opRicarico.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opValore.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opPercentuale.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNetto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmElaborazione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmElaborazione.ResumeLayout(False)
    Me.fmElaborazione.PerformLayout()
    CType(Me.ckElaborazione.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnRight.ResumeLayout(False)
    Me.pnRight.PerformLayout()
    CType(Me.ckVisRisultati.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edArrotond.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAggiungiIVA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edRicar1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edRicar2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edValore.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAppMis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmListinoElabora, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmListinoElabora.ResumeLayout(False)
    Me.fmListinoElabora.PerformLayout()
    CType(Me.edScadprom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAggiornalistini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodtpro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCambioelab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edValelab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edListelab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmListinoPartenza, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmListinoPartenza.ResumeLayout(False)
    Me.fmListinoPartenza.PerformLayout()
    CType(Me.fmPromo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmPromo.ResumeLayout(False)
    CType(Me.cbPromozioni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPromo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCambiopar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edValpar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edListpar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatagg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmTipoElaborazione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmTipoElaborazione.ResumeLayout(False)
    CType(Me.cbTipoElaborazione.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      Try
        tlbElabora.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try

      tlbMain.NTSSetToolTip()
      cbTipoElaborazione.NTSSetParam(oApp.Tr(Me, 130149156428133111, "Tipo elaborazione"))
      cbPromozioni.NTSSetParam(oApp.Tr(Me, 130149185310078369, "Promozioni"))
      ckElaborazione.NTSSetParam(oMenu, oApp.Tr(Me, 128685557583999205, "&Elaborazione automatica"), "S", "N")
      ckAggiungiIVA.NTSSetParam(oMenu, oApp.Tr(Me, 128685557584155495, "Aggiungi l'IVA al valore del listino di partenza"), "S", "N")
      edRicar2.NTSSetParam(oMenu, oApp.Tr(Me, 128685557584468075, "+"), oApp.FormatImporti, 5, -999, 999)
      cbAppMis.NTSSetParam(oApp.Tr(Me, 128685557584780655, "Valore di incremento"))
      edValore.NTSSetParam(oMenu, oApp.Tr(Me, 128685557584936945, "Valore di incremento"), oCleElar.TrovaFmtPrz, 12, -999999999999, 999999999999)
      edRicar1.NTSSetParam(oMenu, oApp.Tr(Me, 128685557585249525, "Percentuale di incremento"), oApp.FormatImporti, 5, -999, 999)
      edArrotond.NTSSetParam(oMenu, oApp.Tr(Me, 128685557585562105, "Arrotondamento di default"), oApp.FormatTempi, 7, -1000000, 1000000)
      opScorporaIva.NTSSetParam(oMenu, oApp.Tr(Me, 128685557585874685, "Scorpora Iva"), "S")
      opRicarico.NTSSetParam(oMenu, oApp.Tr(Me, 128685557586030975, "Ri&carico/Margine"), "S")
      opValore.NTSSetParam(oMenu, oApp.Tr(Me, 128685557586187265, "&Valore"), "S")
      opPercentuale.NTSSetParam(oMenu, oApp.Tr(Me, 128685557586343555, "&Percentuale"), "S")
      ckAggiornalistini.NTSSetParam(oMenu, oApp.Tr(Me, 128770929105589315, "&Aggiorna listini dal 2 all' 8"), "S", "N")
      edScadprom.NTSSetParam(oMenu, oApp.Tr(Me, 128685557586968715, "Data scad. promoz."), False)
      edCodtpro.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128685557587281295, "Codice promozione"), tabtpro)
      edCambioelab.NTSSetParam(oMenu, oApp.Tr(Me, 128685557587750165, "Cambio"), "#,##0.000000000", 9, NTSCDec(0.000000001), 999999999)
      edValelab.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128685557588062745, "Valuta"), tabvalu)
      edListelab.NTSSetParam(oMenu, oApp.Tr(Me, 128685557588531615, "Listelab"), "0", 4, 1, 9999)
      edCambiopar.NTSSetParam(oMenu, oApp.Tr(Me, 128685557590250805, "Cambio"), "#,##0.000000000", 9, NTSCDec(0.000000001), 999999999)
      edValpar.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128685557590563385, "Valuta"), tabvalu)
      edListpar.NTSSetParam(oMenu, oApp.Tr(Me, 128685557591032255, "Listino di partenza"), "0", 4, -2, 9999)
      edDatagg.NTSSetParam(oMenu, oApp.Tr(Me, 128685557591344835, "Data inizio validità"), False)
      edConto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128685557591813705, "Conto"), tabanagra)
      ckNetto.NTSSetParam(oMenu, oApp.Tr(Me, 129441353801675778, "Netto sconti spec. clienti/forn."), "S", "N")
      ckVisRisultati.NTSSetParam(oMenu, oApp.Tr(Me, 129441353801831988, "Visualizza risultati"), "S", "N")
      edPromo.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129441353806830708, "Promozione"), tabtpro)

      edConto.NTSSetParamZoom("")
      edConto.NTSForzaVisZoom = True

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

  Public Overridable Sub CaricaCombo()
    Try
      Dim dttAppMis As New DataTable()
      dttAppMis.Columns.Add("cod", GetType(String))
      dttAppMis.Columns.Add("val", GetType(String))
      dttAppMis.Rows.Add(New Object() {"P", "Principale"})
      dttAppMis.Rows.Add(New Object() {"C", "Confezione"})
      dttAppMis.Rows.Add(New Object() {"S", "Secondaria"})
      dttAppMis.AcceptChanges()
      cbAppMis.DataSource = dttAppMis
      cbAppMis.ValueMember = "cod"
      cbAppMis.DisplayMember = "val"

      Dim dttTipoElaborazione As New DataTable()
      dttTipoElaborazione.Columns.Add("cod", GetType(Integer))
      dttTipoElaborazione.Columns.Add("val", GetType(String))
      dttTipoElaborazione.Rows.Add(New Object() {1, "Generico"})
      dttTipoElaborazione.Rows.Add(New Object() {2, "Speciale Clienti"})
      dttTipoElaborazione.Rows.Add(New Object() {3, "Speciale Fornitori"})
      dttTipoElaborazione.Rows.Add(New Object() {4, "Generico da Listino Clienti"})
      dttTipoElaborazione.Rows.Add(New Object() {5, "Generico da Listino Fornitori"})
      dttTipoElaborazione.AcceptChanges()
      cbTipoElaborazione.DataSource = dttTipoElaborazione
      cbTipoElaborazione.ValueMember = "cod"
      cbTipoElaborazione.DisplayMember = "val"

      Dim dttPromozioni As New DataTable()
      dttPromozioni.Columns.Add("cod", GetType(Integer))
      dttPromozioni.Columns.Add("val", GetType(String))
      dttPromozioni.Rows.Add(New Object() {1, "Listini non in promozione"})
      dttPromozioni.Rows.Add(New Object() {2, "Listini con codice in promozione"})
      dttPromozioni.Rows.Add(New Object() {3, "Tutti i listini in promozione"})
      dttPromozioni.AcceptChanges()
      cbPromozioni.DataSource = dttPromozioni
      cbPromozioni.ValueMember = "cod"
      cbPromozioni.DisplayMember = "val"

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRMMGELAR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim nDecPrx As Integer
    Try
      InitControls()

      CaricaCombo()
      strProntoMess = oApp.Tr(Me, 130421082882171012, "Pronto.")
      '------------------------------------------------------------------------------------
      oCleElar.bAggPrezzi0 = CBool(oMenu.GetSettingBus("BSMGELAR", "OPZIONI", ".", "AggiornaPrezzi0", "0", " ", "0"))
      oCleElar.strTipoArro = UCase(CStr(oMenu.GetSettingBus("BSMGELAR", "OPZIONI", ".", "TipoArrotondamento", "E", " ", "E")))
      '------------------------------------------------------------------------------------
      edConto.Text = "0"
      lbXx_Conto.Text = ""
      edDatagg.Text = NTSCDate(Now).ToShortDateString
      edListpar.Text = "1"
      edValpar.Text = "0"
      lbXx_Valpar.Text = ""
      edCambiopar.Text = "1"
      edCambiopar.Enabled = False
      edListelab.Text = "1"
      ckAggiornalistini.Checked = False
      edValelab.Text = "0"
      lbXx_Valelab.Text = ""
      edCambioelab.Text = "1"
      edCambioelab.Enabled = False
      edCodtpro.Text = "0"
      lbXx_Codtpro.Text = ""
      edScadprom.Text = IntSetDate("31/12/2099")
      edScadprom.Enabled = False

      'solo per azienda euro
      oCleElar.strArroPrx = "0,01"
      nDecPrx = oMenu.TrovaNdecSuPrzUn(0)
      Select Case nDecPrx
        Case 0
          oCleElar.strArroPrx = "1"
        Case 1
          oCleElar.strArroPrx = "0,1"
        Case 2
          oCleElar.strArroPrx = "0,01" 'default
        Case 3
          oCleElar.strArroPrx = "0,001"
        Case 4
          oCleElar.strArroPrx = "0,0001"
      End Select

      edArrotond.Text = NTSCStr(oCleElar.strArroPrx)
      edRicar1.Text = "0"
      edRicar2.Text = "0"
      edValore.Text = "0"
      edValore.Enabled = False
      edValore.TabStop = False
      lbValore.Enabled = False
      lbConto.Enabled = False
      lbAppMis.Enabled = False
      cbAppMis.Enabled = False

      cbAppMis.SelectedValue = "P"

      edConto.Enabled = False

      ckVisRisultati.Checked = CBool(oMenu.GetSettingBus("BSMGELAR", "RECENT", ".", "VisRisultati", "-1", " ", "-1"))

      ckNetto.Enabled = False
      ckNetto.Checked = False
      '------------------------------------------------------------------------------
      oCleElar.bSeleziona = False

      lbStatus.Text = strProntoMess

      GctlSetRoules()
      GctlApplicaDefaultValue()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGELAR_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If oCleElar.bElabInCorso Then e.Cancel = True
      oMenu.SaveSettingBus("BSMGELAR", "RECENT", ".", "VisRisultati", NTSCStr(IIf((ckVisRisultati.Checked), "-1", "0")), " ", "NS.", "...", "...")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Dim oPar As CLE__PATB = Nothing
    Try
      oPar = New CLE__PATB
      oPar.bVisGriglia = False
      oPar.strTipoArticolo = "N"
      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oPar)
      oCleElar.strWhereFiar = oPar.strOut.Trim
      oCleElar.strAdocQuery = ""

      If oCleElar.strWhereFiar = "" Then
        oCleElar.bSeleziona = False
      Else
        oCleElar.bSeleziona = True
      End If

      cbTipoElaborazione.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdArticoliDaDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdArticoliDaDoc.Click
    Dim oParam As New CLE__CLDP
    Dim fmAdoc As FRMMGADOC = Nothing
    Try
      oCleElar.strAdocQuery = ""

      fmAdoc = CType(NTSNewFormModal("FRMMGADOC"), FRMMGADOC)
      fmAdoc.Init(oMenu, oParam, DittaCorrente)
      fmAdoc.InitEntity(oCleElar)
      fmAdoc.ShowDialog()

      oCleElar.strWhereFiar = ""
      '-----------------------------------------------------------------------------------------
      If oCleElar.bAdocAnnullato = True Then
        oCleElar.bSeleziona = True
      Else
        oCleElar.bSeleziona = False
      End If
      '-----------------------------------------------------------------------------------------

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not fmAdoc Is Nothing Then fmAdoc.Dispose()
      fmAdoc = Nothing
    End Try
  End Sub

#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbElabora_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbElabora.ItemClick
    Dim dRes As DialogResult
    Dim bGenerico As Boolean = False
    Dim bClienti As Boolean = False
    Dim bFornitori As Boolean = False
    Dim bGenericoClienti As Boolean = False
    Dim bGenericoFornitori As Boolean = False

    Try
      Me.ValidaLastControl()

      If (oCleElar.bSeleziona = False) Then
        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128674290993239085, "Non sono stati selezionati gli articoli." & vbCrLf & "Selezionarli ora?"))
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          cmdSeleziona_Click(Me, Nothing)
          If oCleElar.bSeleziona = False Then
            Exit Sub
          End If
        Else
          Exit Sub
        End If
      End If

      Select Case NTSCInt(cbTipoElaborazione.SelectedValue)
        Case 1 : bGenerico = True
        Case 2 : bClienti = True
        Case 3 : bFornitori = True
        Case 4 : bGenericoClienti = True
        Case 5 : bGenericoFornitori = True
      End Select

      If oCleElar.Elabora(bGenerico, edConto.Text, bClienti, bGenericoClienti, bFornitori, bGenericoFornitori, _
                          "0", edValpar.Text, edDatagg.Text, "0", edListpar.Text, False, False, edListelab.Text, _
                          False, edValelab.Text, edCodtpro.Text, "0", edScadprom.Text, ckAggiornalistini.Checked, _
                          opValore.Checked, cbAppMis.SelectedValue, ckElaborazione.Checked, ckVisRisultati.Checked, _
                          ckAggiungiIVA.Checked, edValore.Text, edCambioelab.Text, edCambiopar.Text, _
                          edRicar1.Text, edRicar2.Text, opPercentuale.Checked, opRicarico.Checked, _
                          opScorporaIva.Checked, edArrotond.Text, dsElar, ckNetto.Checked) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128551610678125000, "Elaborazione completata regolarmente."))
      Else
        oApp.MsgBoxErr(oApp.Tr(Me, 128551610937343750, "Elaborazione non riuscita."))
      End If

      lbStatus.Text = strProntoMess
    Catch ex As Exception
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

      If edConto.Focused Then
        SetFastZoom(edConto.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = NTSCStr(edConto.Text)

        Select Case NTSCInt(cbTipoElaborazione.SelectedValue)
          Case 2, 4 : oParam.strTipo = "C"
          Case 3, 5 : oParam.strTipo = "F"
        End Select

        NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edConto.Text Then edConto.NTSTextDB = NTSZOOM.strIn

      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        NTSCallStandardZoom()
      End If

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

#Region "Validazioni"
  Public Overridable Sub edArrotond_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edArrotond.Validated
    Try
      If NTSCDec(edArrotond.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128695998129835058, "L'arrotondamento non può essere pari a zero."))
        edArrotond.Text = IntSetNum(oCleElar.strArroPrx)
        Exit Sub
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edCodtpro_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodtpro.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If Not oCleElar.edCodtpro_Validated(NTSCInt(edCodtpro.Text), strTmp, strError) Then
        oApp.MsgBoxErr(strError)
        edCodtpro.Text = "0"
        lbXx_Codtpro.Text = ""
        edScadprom.Text = IntSetDate("31/12/2099")
        edScadprom.Enabled = False
      Else
        lbXx_Codtpro.Text = strTmp
        If edCodtpro.Text = "0" Then
          edScadprom.Text = IntSetDate("31/12/2099")
          edScadprom.Enabled = False
        Else
          GctlSetVisEnab(edScadprom, False)
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edConto_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edConto.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Dim bClienti As Boolean = False
    Dim bFornitori As Boolean = False

    Try
      Select Case NTSCInt(cbTipoElaborazione.SelectedValue)
        Case 2 : bClienti = True
        Case 3 : bFornitori = True
      End Select
      If Not oCleElar.edConto_Validated(NTSCInt(edConto.Text), strTmp, strError, bFornitori, bClienti) Then
        oApp.MsgBoxErr(strError)
        edConto.Text = "0"
        lbXx_Conto.Text = ""
      Else
        lbXx_Conto.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edDatagg_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDatagg.Validated
    Try
      If NTSCInt(edValpar.Text) <> 0 Then
        oCleElar.CercaCambioDiOggi(NTSCInt(edValpar.Text), edDatagg.Text, edCambiopar.Text)
      End If
      If NTSCInt(edValelab.Text) <> 0 Then
        oCleElar.CercaCambioDiOggi(NTSCInt(edValelab.Text), edDatagg.Text, edCambioelab.Text)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edListelab_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edListelab.Validated
    Try
      If NTSCInt(edListelab.Text) = 1 Then
        If Not ckAggiornalistini.Checked Then
          GctlSetVisEnab(ckAggiornalistini, True)
        End If
      Else
        ckAggiornalistini.Checked = False
        ckAggiornalistini.Visible = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edValelab_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edValelab.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If Not edValelab.Text = "0" Then
        If Not oCleElar.edValelab_Validated(NTSCInt(edValelab.Text), strTmp, strError) Then
          oApp.MsgBoxErr(strError)
          edValelab.Text = "0"
          lbXx_Valelab.Text = ""
          edCambioelab.Text = "1"
          edCambioelab.Enabled = False
        Else
          GctlSetVisEnab(edCambioelab, False)
          oCleElar.CercaCambioDiOggi(NTSCInt(edValelab.Text), edDatagg.Text, edCambioelab.Text)
          lbXx_Valelab.Text = strTmp
        End If
      Else
        edValelab.Text = "0"
        lbXx_Valelab.Text = ""
        edCambioelab.Text = "1"
        edCambioelab.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edValpar_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edValpar.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If Not edValpar.Text = "0" Then
        If Not oCleElar.edValpar_Validated(NTSCInt(edValpar.Text), strTmp, strError) Then
          oApp.MsgBoxErr(strError)
          edValpar.Text = "0"
          lbXx_Valpar.Text = ""
          edCambiopar.Text = "1"
          edCambiopar.Enabled = False
        Else
          GctlSetVisEnab(edCambiopar, False)
          oCleElar.CercaCambioDiOggi(NTSCInt(edValpar.Text), edDatagg.Text, edCambiopar.Text)
          lbXx_Valpar.Text = strTmp
        End If
      Else
        edValpar.Text = "0"
        lbXx_Valpar.Text = ""
        edCambiopar.Text = "1"
        edCambiopar.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edPromo_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edPromo.Validated
    Dim strTmp As String = ""
    Dim strError As String = ""
    Try
      If Not oCleElar.edCodtpro_Validated(NTSCInt(edPromo.Text), strTmp, strError) Then
        oApp.MsgBoxErr(strError)
        edPromo.Text = "0"
        lbDesPromo.Text = ""
      Else
        lbDesPromo.Text = strTmp
      End If

      oCleElar.lPromo = NTSCInt(edPromo.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cbTipoElaborazione_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTipoElaborazione.SelectedIndexChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCInt(cbTipoElaborazione.SelectedValue)
        Case 1
          GctlSetVisEnab(lbListpar, False)
          GctlSetVisEnab(lbListelab, False)
          lbConto.Text = oApp.Tr(Me, 128686435412088589, "Conto")
          lbConto.Enabled = False
          GctlSetVisEnab(lbListpar1, False)
          GctlSetVisEnab(lbListpar2, False)
          GctlSetVisEnab(lbListpar3, False)
          GctlSetVisEnab(edListpar, False)
          GctlSetVisEnab(edListelab, False)
          edConto.Text = "0"
          edConto.Enabled = False
          lbXx_Conto.Text = ""
          GctlSetVisEnab(opRicarico, False)
          opPercentuale.Checked = True
          GctlSetVisEnab(ckAggiornalistini, True)
          ckNetto.Enabled = False
          ckNetto.Checked = False
        Case 2
          lbListpar.Enabled = False
          lbListelab.Enabled = False
          GctlSetVisEnab(lbConto, False)
          lbConto.Text = oApp.Tr(Me, 128450577437473574, "Cliente")
          lbListpar1.Enabled = False
          lbListpar2.Enabled = False
          lbListpar3.Enabled = False
          edListpar.Text = "1"
          edListpar.Enabled = False
          edListelab.Text = "1"
          edListelab.Enabled = False
          GctlSetVisEnab(edConto, False)
          If opRicarico.Checked = True Then opPercentuale.Checked = True
          opRicarico.Checked = False
          opRicarico.Enabled = False
          ckAggiornalistini.Checked = False
          ckAggiornalistini.Visible = False
          ckNetto.Enabled = False
          ckNetto.Checked = False
        Case 3
          lbListpar.Enabled = False
          lbListelab.Enabled = False
          GctlSetVisEnab(lbConto, False)
          lbConto.Text = oApp.Tr(Me, 128686435287556449, "Fornitore")
          lbListpar1.Enabled = False
          lbListpar2.Enabled = False
          lbListpar3.Enabled = False
          edListpar.Text = "1"
          edListpar.Enabled = False
          edListelab.Text = "1"
          edListelab.Enabled = False
          GctlSetVisEnab(edConto, False)
          If opRicarico.Checked = True Then opPercentuale.Checked = True
          opRicarico.Checked = False
          opRicarico.Enabled = False
          ckAggiornalistini.Checked = False
          ckAggiornalistini.Visible = False
          ckNetto.Enabled = False
          ckNetto.Checked = False
        Case 4, 5
          lbListpar.Enabled = False
          GctlSetVisEnab(lbConto, False)
          Select Case NTSCInt(cbTipoElaborazione.SelectedValue)
            Case 4 : lbConto.Text = "Cliente"
            Case 5 : lbConto.Text = "Fornitore"
          End Select
          lbListpar1.Enabled = False
          lbListpar2.Enabled = False
          lbListpar3.Enabled = False
          edListpar.Text = "1"
          edListpar.Enabled = False
          GctlSetVisEnab(edConto, False)
          GctlSetVisEnab(lbListelab, False)
          GctlSetVisEnab(edListelab, False)
          GctlSetVisEnab(ckAggiornalistini, True)
          GctlSetVisEnab(opRicarico, False)
          GctlSetVisEnab(ckNetto, False)
          ckNetto.Checked = False
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cbPromozioni_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPromozioni.SelectedIndexChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCInt(cbPromozioni.SelectedValue)
        Case 1
          edPromo.Text = "0"
          edPromo.Enabled = False
          oCleElar.strTipoPromo = "N"
          edCodtpro.Enabled = True
        Case 2
          edPromo.Enabled = True
          oCleElar.strTipoPromo = "S"
          edCodtpro.Enabled = True
          edPromo.Focus()
        Case 3
          edPromo.Text = "0"
          edPromo.Enabled = False
          oCleElar.strTipoPromo = "T"
          edCodtpro.Enabled = False
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub opPercentuale_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opPercentuale.CheckedChanged
    Try
      If opPercentuale.Checked = True Then
        GctlSetVisEnab(lbArrotond, False)
        GctlSetVisEnab(lbRicar1, False)
        GctlSetVisEnab(lbRicar2, False)
        lbValore.Enabled = False
        lbAppMis.Enabled = False
        GctlSetVisEnab(edArrotond, False)
        GctlSetVisEnab(edRicar1, False)
        GctlSetVisEnab(edRicar2, False)
        edValore.Text = "0"
        edValore.Enabled = False
        cbAppMis.Enabled = False
        cbAppMis.SelectedValue = "P"
        GctlSetVisEnab(ckAggiungiIVA, False)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub opRicarico_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opRicarico.CheckedChanged
    Try
      If opRicarico.Checked = True Then
        GctlSetVisEnab(lbArrotond, False)
        lbRicar1.Enabled = False
        lbRicar2.Enabled = False
        lbValore.Enabled = False
        lbAppMis.Enabled = False
        edArrotond.Text = IntSetNum(oCleElar.strArroPrx)
        GctlSetVisEnab(edArrotond, False)
        edRicar1.Text = "0"
        edRicar1.Enabled = False
        edRicar2.Text = "0"
        edRicar2.Enabled = False
        edValore.Text = "0"
        edValore.Enabled = False
        cbAppMis.Enabled = False
        cbAppMis.SelectedValue = "P"
        GctlSetVisEnab(ckAggiungiIVA, False)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub opScorporaIva_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opScorporaIva.CheckedChanged
    Try
      If opScorporaIva.Checked = True Then
        edRicar1.Text = "0"
        edRicar2.Text = "0"
        edValore.Text = "0"
        cbAppMis.SelectedValue = "P"
        lbRicar1.Enabled = False
        lbRicar2.Enabled = False
        lbValore.Enabled = False
        lbAppMis.Enabled = False
        edRicar1.Enabled = False
        edRicar2.Enabled = False
        edValore.Enabled = False
        cbAppMis.Enabled = False
        GctlSetVisEnab(lbArrotond, False)
        GctlSetVisEnab(edArrotond, False)
        edArrotond.Text = IntSetNum(oCleElar.strArroPrx)
        ckAggiungiIVA.Checked = False
        ckAggiungiIVA.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub opValore_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opValore.CheckedChanged
    Try
      If opValore.Checked = True Then
        lbArrotond.Enabled = False
        lbRicar1.Enabled = False
        lbRicar2.Enabled = False
        lbAppMis.Enabled = True
        GctlSetVisEnab(lbValore, False)
        edArrotond.Text = IntSetNum(oCleElar.strArroPrx)
        edArrotond.Enabled = False
        edRicar1.Text = "0"
        edRicar1.Enabled = False
        edRicar2.Text = "0"
        edRicar2.Enabled = False
        GctlSetVisEnab(edValore, False)
        GctlSetVisEnab(cbAppMis, False)
        cbAppMis.SelectedValue = "P"
        GctlSetVisEnab(ckAggiungiIVA, False)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Function Salta() As Boolean
    Dim oParam As New CLE__CLDP
    Dim frmInea As FRMMGINEA = Nothing
    Try

      frmInea = CType(NTSNewFormModal("FRMMGINEA"), FRMMGINEA)
      frmInea.nIneaRichiesta = 0
      frmInea.Init(oMenu, oParam, DittaCorrente)
      frmInea.InitEntity(oCleElar)
      frmInea.ShowDialog()

      oCleElar.nRichiesta = frmInea.nIneaRichiesta

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmInea Is Nothing Then frmInea.Dispose()
      frmInea = Nothing
    End Try
  End Function

  Public Overridable Function visRisult() As Boolean
    Dim oParam As New CLE__CLDP
    Dim frmVris As FRMMGVRIS = Nothing
    Try

      frmVris = CType(NTSNewFormModal("FRMMGVRIS"), FRMMGVRIS)
      frmVris.Init(oMenu, oParam, DittaCorrente)
      frmVris.InitEntity(oCleElar)
      frmVris.ShowDialog()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmVris Is Nothing Then frmVris.Dispose()
      frmVris = Nothing
    End Try
  End Function

End Class
