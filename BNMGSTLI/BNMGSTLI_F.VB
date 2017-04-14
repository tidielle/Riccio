Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGSTLI_F
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
  Public oCleStli As CLEMGSTLI
  Public oCallParams As CLE__CLDP
  Public dttDefault, dttPersForm As New DataTable

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaWord As NTSInformatica.NTSBarButtonItem
#End Region

  Public Overloads Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    '---------------------------------
    'questa funzione riceve gli eventi dall'ENTITY: rimappata rispetto a quella standard di FRM__CHILD
    'prima eseguo quella standard
    Dim strTmp() As String
    Dim i As Integer = 0

    If Not IsMyThrowRemoteEvent() Then Return 'il messaggio non Ë per questa form ...
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
        End Select
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGSTLI_F))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaWord = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaGriglia = New NTSInformatica.NTSBarButtonItem
    Me.tlbCaricaGriglia = New NTSInformatica.NTSBarButtonItem
    Me.tlbImportExcel = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.edDatagg = New NTSInformatica.NTSTextBoxData
    Me.lbDatagg = New NTSInformatica.NTSLabel
    Me.edConto = New NTSInformatica.NTSTextBoxNum
    Me.lbCodling = New NTSInformatica.NTSLabel
    Me.edCodling = New NTSInformatica.NTSTextBoxNum
    Me.ckSeleziona = New NTSInformatica.NTSCheckBox
    Me.ckNoSoloSconti = New NTSInformatica.NTSCheckBox
    Me.cbTipo1 = New NTSInformatica.NTSComboBox
    Me.lbXx_Conto = New NTSInformatica.NTSLabel
    Me.lbXx_Codling = New NTSInformatica.NTSLabel
    Me.edListino1 = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_Codvalu1 = New NTSInformatica.NTSLabel
    Me.edCodvalu1 = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_Codpromo1 = New NTSInformatica.NTSLabel
    Me.edCodpromo1 = New NTSInformatica.NTSTextBoxNum
    Me.edQuant1 = New NTSInformatica.NTSTextBoxNum
    Me.edQuant2 = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_Codpromo2 = New NTSInformatica.NTSLabel
    Me.edCodpromo2 = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_Codvalu2 = New NTSInformatica.NTSLabel
    Me.edCodvalu2 = New NTSInformatica.NTSTextBoxNum
    Me.edListino2 = New NTSInformatica.NTSTextBoxNum
    Me.cbTipo2 = New NTSInformatica.NTSComboBox
    Me.ckSecondoPrezzo = New NTSInformatica.NTSCheckBox
    Me.edQuant3 = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_Codpromo3 = New NTSInformatica.NTSLabel
    Me.edCodpromo3 = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_Codvalu3 = New NTSInformatica.NTSLabel
    Me.edCodvalu3 = New NTSInformatica.NTSTextBoxNum
    Me.edListino3 = New NTSInformatica.NTSTextBoxNum
    Me.cbTipo3 = New NTSInformatica.NTSComboBox
    Me.ckTerzoPrezzo = New NTSInformatica.NTSCheckBox
    Me.edQuant4 = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_Codpromo4 = New NTSInformatica.NTSLabel
    Me.edCodpromo4 = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_Codvalu4 = New NTSInformatica.NTSLabel
    Me.edCodvalu4 = New NTSInformatica.NTSTextBoxNum
    Me.edListino4 = New NTSInformatica.NTSTextBoxNum
    Me.cbTipo4 = New NTSInformatica.NTSComboBox
    Me.ckQuartoPrezzo = New NTSInformatica.NTSCheckBox
    Me.edQuantScont1 = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_Codtpro1 = New NTSInformatica.NTSLabel
    Me.edCodtpro1 = New NTSInformatica.NTSTextBoxNum
    Me.cbTipoSconto1 = New NTSInformatica.NTSComboBox
    Me.lbListino = New NTSInformatica.NTSLabel
    Me.lbCodvalu = New NTSInformatica.NTSLabel
    Me.lbCodpromo = New NTSInformatica.NTSLabel
    Me.lbTipo = New NTSInformatica.NTSLabel
    Me.lbQuant = New NTSInformatica.NTSLabel
    Me.ckSconti1 = New NTSInformatica.NTSCheckBox
    Me.fmSelezionaArt = New NTSInformatica.NTSGroupBox
    Me.cbOrdinaPer = New NTSInformatica.NTSComboBox
    Me.lbOrdinaPer = New NTSInformatica.NTSLabel
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.cbSelezione = New NTSInformatica.NTSComboBox
    Me.lbXx_Codlsar = New NTSInformatica.NTSLabel
    Me.edCodlsar = New NTSInformatica.NTSTextBoxNum
    Me.lbStatus = New NTSInformatica.NTSLabel
    Me.edQuant5 = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_Codpromo5 = New NTSInformatica.NTSLabel
    Me.edCodpromo5 = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_Codvalu5 = New NTSInformatica.NTSLabel
    Me.edCodvalu5 = New NTSInformatica.NTSTextBoxNum
    Me.edListino5 = New NTSInformatica.NTSTextBoxNum
    Me.cbTipo5 = New NTSInformatica.NTSComboBox
    Me.ckQuintoPrezzo = New NTSInformatica.NTSCheckBox
    Me.fmPrezzi = New NTSInformatica.NTSGroupBox
    Me.pnCheckPrezzi = New NTSInformatica.NTSPanel
    Me.lbPrezzi1 = New NTSInformatica.NTSLabel
    Me.fmSconti = New NTSInformatica.NTSGroupBox
    Me.pnCheckSconti = New NTSInformatica.NTSPanel
    Me.ckSconti2 = New NTSInformatica.NTSCheckBox
    Me.cbTipoSconto2 = New NTSInformatica.NTSComboBox
    Me.edCodtpro2 = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_Codtpro2 = New NTSInformatica.NTSLabel
    Me.edQuantScont2 = New NTSInformatica.NTSTextBoxNum
    Me.fmCliFor = New NTSInformatica.NTSGroupBox
    Me.cbSelClie = New NTSInformatica.NTSComboBox
    Me.cmdSelCliFor = New NTSInformatica.NTSButton
    Me.NtsPanel1 = New NTSInformatica.NTSPanel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatagg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodling.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSeleziona.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNoSoloSconti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipo1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edListino1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodvalu1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodpromo1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edQuant1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edQuant2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodpromo2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodvalu2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edListino2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipo2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSecondoPrezzo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edQuant3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodpromo3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodvalu3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edListino3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipo3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTerzoPrezzo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edQuant4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodpromo4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodvalu4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edListino4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipo4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckQuartoPrezzo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edQuantScont1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodtpro1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipoSconto1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSconti1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmSelezionaArt, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmSelezionaArt.SuspendLayout()
    CType(Me.cbOrdinaPer.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbSelezione.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodlsar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edQuant5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodpromo5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodvalu5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edListino5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipo5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckQuintoPrezzo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmPrezzi, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmPrezzi.SuspendLayout()
    CType(Me.pnCheckPrezzi, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCheckPrezzi.SuspendLayout()
    CType(Me.fmSconti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmSconti.SuspendLayout()
    CType(Me.pnCheckSconti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCheckSconti.SuspendLayout()
    CType(Me.ckSconti2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipoSconto2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodtpro2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edQuantScont2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmCliFor, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmCliFor.SuspendLayout()
    CType(Me.cbSelClie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel1.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbStampaWord, Me.tlbStampaGriglia, Me.tlbCaricaGriglia, Me.tlbImportExcel})
    Me.NtsBarManager1.MaxItemId = 24
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaWord), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaGriglia, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCaricaGriglia), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, False, Me.tlbImportExcel, False), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 13
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 15
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.Id = 16
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 4
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 5
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbStampaWord
    '
    Me.tlbStampaWord.Caption = "Stampa Word"
    Me.tlbStampaWord.Glyph = CType(resources.GetObject("tlbStampaWord.Glyph"), System.Drawing.Image)
    Me.tlbStampaWord.Id = 18
    Me.tlbStampaWord.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F7))
    Me.tlbStampaWord.Name = "tlbStampaWord"
    Me.tlbStampaWord.Visible = True
    '
    'tlbStampaGriglia
    '
    Me.tlbStampaGriglia.Caption = "Stampa su griglia"
    Me.tlbStampaGriglia.Glyph = CType(resources.GetObject("tlbStampaGriglia.Glyph"), System.Drawing.Image)
    Me.tlbStampaGriglia.Id = 21
    Me.tlbStampaGriglia.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F11)
    Me.tlbStampaGriglia.Name = "tlbStampaGriglia"
    Me.tlbStampaGriglia.Visible = True
    '
    'tlbCaricaGriglia
    '
    Me.tlbCaricaGriglia.Caption = "Carica griglia"
    Me.tlbCaricaGriglia.Glyph = CType(resources.GetObject("tlbCaricaGriglia.Glyph"), System.Drawing.Image)
    Me.tlbCaricaGriglia.Id = 22
    Me.tlbCaricaGriglia.Name = "tlbCaricaGriglia"
    Me.tlbCaricaGriglia.Visible = True
    '
    'tlbImportExcel
    '
    Me.tlbImportExcel.Caption = "Importa da Excel"
    Me.tlbImportExcel.Enabled = False
    Me.tlbImportExcel.Glyph = CType(resources.GetObject("tlbImportExcel.Glyph"), System.Drawing.Image)
    Me.tlbImportExcel.Id = 23
    Me.tlbImportExcel.Name = "tlbImportExcel"
    Me.tlbImportExcel.Visible = False
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
    'edDatagg
    '
    Me.edDatagg.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edDatagg.Location = New System.Drawing.Point(105, 10)
    Me.edDatagg.Name = "edDatagg"
    Me.edDatagg.NTSDbField = ""
    Me.edDatagg.NTSForzaVisZoom = False
    Me.edDatagg.NTSOldValue = ""
    Me.edDatagg.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatagg.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatagg.Properties.AutoHeight = False
    Me.edDatagg.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatagg.Properties.MaxLength = 65536
    Me.edDatagg.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatagg.Size = New System.Drawing.Size(84, 20)
    Me.edDatagg.TabIndex = 90
    Me.edDatagg.TextData = New Date(1900, 1, 1, 0, 0, 0, 0)
    '
    'lbDatagg
    '
    Me.lbDatagg.AutoSize = True
    Me.lbDatagg.BackColor = System.Drawing.Color.Transparent
    Me.lbDatagg.Location = New System.Drawing.Point(12, 13)
    Me.lbDatagg.Name = "lbDatagg"
    Me.lbDatagg.NTSDbField = ""
    Me.lbDatagg.Size = New System.Drawing.Size(78, 13)
    Me.lbDatagg.TabIndex = 91
    Me.lbDatagg.Text = "Data di validit‡"
    Me.lbDatagg.Tooltip = ""
    Me.lbDatagg.UseMnemonic = False
    '
    'edConto
    '
    Me.edConto.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edConto.EditValue = "0"
    Me.edConto.Location = New System.Drawing.Point(127, 22)
    Me.edConto.Name = "edConto"
    Me.edConto.NTSDbField = ""
    Me.edConto.NTSFormat = "0"
    Me.edConto.NTSForzaVisZoom = False
    Me.edConto.NTSOldValue = ""
    Me.edConto.Properties.Appearance.Options.UseTextOptions = True
    Me.edConto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edConto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edConto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edConto.Properties.AutoHeight = False
    Me.edConto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edConto.Properties.MaxLength = 65536
    Me.edConto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edConto.Size = New System.Drawing.Size(83, 20)
    Me.edConto.TabIndex = 93
    Me.edConto.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edConto.TextInt = 0
    '
    'lbCodling
    '
    Me.lbCodling.AutoSize = True
    Me.lbCodling.BackColor = System.Drawing.Color.Transparent
    Me.lbCodling.Location = New System.Drawing.Point(223, 13)
    Me.lbCodling.Name = "lbCodling"
    Me.lbCodling.NTSDbField = ""
    Me.lbCodling.Size = New System.Drawing.Size(70, 13)
    Me.lbCodling.TabIndex = 94
    Me.lbCodling.Text = "Codice lingua"
    Me.lbCodling.Tooltip = ""
    Me.lbCodling.UseMnemonic = False
    '
    'edCodling
    '
    Me.edCodling.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodling.EditValue = "0"
    Me.edCodling.Location = New System.Drawing.Point(299, 10)
    Me.edCodling.Name = "edCodling"
    Me.edCodling.NTSDbField = ""
    Me.edCodling.NTSFormat = "0"
    Me.edCodling.NTSForzaVisZoom = False
    Me.edCodling.NTSOldValue = ""
    Me.edCodling.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodling.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodling.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodling.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodling.Properties.AutoHeight = False
    Me.edCodling.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodling.Properties.MaxLength = 65536
    Me.edCodling.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodling.Size = New System.Drawing.Size(49, 20)
    Me.edCodling.TabIndex = 95
    Me.edCodling.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCodling.TextInt = 0
    '
    'ckSeleziona
    '
    Me.ckSeleziona.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSeleziona.Location = New System.Drawing.Point(550, 24)
    Me.ckSeleziona.Name = "ckSeleziona"
    Me.ckSeleziona.NTSCheckValue = "S"
    Me.ckSeleziona.NTSUnCheckValue = "N"
    Me.ckSeleziona.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSeleziona.Properties.Appearance.Options.UseBackColor = True
    Me.ckSeleziona.Properties.AutoHeight = False
    Me.ckSeleziona.Properties.Caption = "Non stampare articoli con dati a &zero"
    Me.ckSeleziona.Size = New System.Drawing.Size(203, 19)
    Me.ckSeleziona.TabIndex = 46
    '
    'ckNoSoloSconti
    '
    Me.ckNoSoloSconti.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckNoSoloSconti.Location = New System.Drawing.Point(516, 1)
    Me.ckNoSoloSconti.Name = "ckNoSoloSconti"
    Me.ckNoSoloSconti.NTSCheckValue = "S"
    Me.ckNoSoloSconti.NTSUnCheckValue = "N"
    Me.ckNoSoloSconti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNoSoloSconti.Properties.Appearance.Options.UseBackColor = True
    Me.ckNoSoloSconti.Properties.AutoHeight = False
    Me.ckNoSoloSconti.Properties.Caption = "Non stampare articoli in presenza di soli sconti"
    Me.ckNoSoloSconti.Size = New System.Drawing.Size(242, 19)
    Me.ckNoSoloSconti.TabIndex = 46
    '
    'cbTipo1
    '
    Me.cbTipo1.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipo1.DataSource = Nothing
    Me.cbTipo1.DisplayMember = ""
    Me.cbTipo1.Location = New System.Drawing.Point(536, 20)
    Me.cbTipo1.Name = "cbTipo1"
    Me.cbTipo1.NTSDbField = ""
    Me.cbTipo1.Properties.AutoHeight = False
    Me.cbTipo1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipo1.Properties.DropDownRows = 30
    Me.cbTipo1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipo1.SelectedValue = ""
    Me.cbTipo1.Size = New System.Drawing.Size(113, 20)
    Me.cbTipo1.TabIndex = 99
    Me.cbTipo1.ValueMember = ""
    '
    'lbXx_Conto
    '
    Me.lbXx_Conto.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Conto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Conto.Location = New System.Drawing.Point(216, 22)
    Me.lbXx_Conto.Name = "lbXx_Conto"
    Me.lbXx_Conto.NTSDbField = ""
    Me.lbXx_Conto.Size = New System.Drawing.Size(314, 20)
    Me.lbXx_Conto.TabIndex = 100
    Me.lbXx_Conto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_Conto.Tooltip = ""
    Me.lbXx_Conto.UseMnemonic = False
    '
    'lbXx_Codling
    '
    Me.lbXx_Codling.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codling.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codling.Location = New System.Drawing.Point(354, 10)
    Me.lbXx_Codling.Name = "lbXx_Codling"
    Me.lbXx_Codling.NTSDbField = ""
    Me.lbXx_Codling.Size = New System.Drawing.Size(409, 20)
    Me.lbXx_Codling.TabIndex = 102
    Me.lbXx_Codling.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_Codling.Tooltip = ""
    Me.lbXx_Codling.UseMnemonic = False
    '
    'edListino1
    '
    Me.edListino1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edListino1.EditValue = "0"
    Me.edListino1.Location = New System.Drawing.Point(53, 20)
    Me.edListino1.Name = "edListino1"
    Me.edListino1.NTSDbField = ""
    Me.edListino1.NTSFormat = "0"
    Me.edListino1.NTSForzaVisZoom = False
    Me.edListino1.NTSOldValue = ""
    Me.edListino1.Properties.Appearance.Options.UseTextOptions = True
    Me.edListino1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edListino1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edListino1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edListino1.Properties.AutoHeight = False
    Me.edListino1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edListino1.Properties.MaxLength = 65536
    Me.edListino1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edListino1.Size = New System.Drawing.Size(50, 20)
    Me.edListino1.TabIndex = 103
    Me.edListino1.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edListino1.TextInt = 0
    '
    'lbXx_Codvalu1
    '
    Me.lbXx_Codvalu1.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codvalu1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codvalu1.Location = New System.Drawing.Point(167, 19)
    Me.lbXx_Codvalu1.Name = "lbXx_Codvalu1"
    Me.lbXx_Codvalu1.NTSDbField = ""
    Me.lbXx_Codvalu1.Size = New System.Drawing.Size(93, 20)
    Me.lbXx_Codvalu1.TabIndex = 111
    Me.lbXx_Codvalu1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_Codvalu1.Tooltip = ""
    Me.lbXx_Codvalu1.UseMnemonic = False
    '
    'edCodvalu1
    '
    Me.edCodvalu1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodvalu1.EditValue = "0"
    Me.edCodvalu1.Location = New System.Drawing.Point(111, 20)
    Me.edCodvalu1.Name = "edCodvalu1"
    Me.edCodvalu1.NTSDbField = ""
    Me.edCodvalu1.NTSFormat = "0"
    Me.edCodvalu1.NTSForzaVisZoom = False
    Me.edCodvalu1.NTSOldValue = ""
    Me.edCodvalu1.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodvalu1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodvalu1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodvalu1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodvalu1.Properties.AutoHeight = False
    Me.edCodvalu1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodvalu1.Properties.MaxLength = 65536
    Me.edCodvalu1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodvalu1.Size = New System.Drawing.Size(50, 20)
    Me.edCodvalu1.TabIndex = 110
    Me.edCodvalu1.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCodvalu1.TextInt = 0
    '
    'lbXx_Codpromo1
    '
    Me.lbXx_Codpromo1.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codpromo1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codpromo1.Location = New System.Drawing.Point(322, 19)
    Me.lbXx_Codpromo1.Name = "lbXx_Codpromo1"
    Me.lbXx_Codpromo1.NTSDbField = ""
    Me.lbXx_Codpromo1.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_Codpromo1.TabIndex = 113
    Me.lbXx_Codpromo1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_Codpromo1.Tooltip = ""
    Me.lbXx_Codpromo1.UseMnemonic = False
    '
    'edCodpromo1
    '
    Me.edCodpromo1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodpromo1.EditValue = "0"
    Me.edCodpromo1.Location = New System.Drawing.Point(266, 19)
    Me.edCodpromo1.Name = "edCodpromo1"
    Me.edCodpromo1.NTSDbField = ""
    Me.edCodpromo1.NTSFormat = "0"
    Me.edCodpromo1.NTSForzaVisZoom = False
    Me.edCodpromo1.NTSOldValue = ""
    Me.edCodpromo1.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodpromo1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodpromo1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodpromo1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodpromo1.Properties.AutoHeight = False
    Me.edCodpromo1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodpromo1.Properties.MaxLength = 65536
    Me.edCodpromo1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodpromo1.Size = New System.Drawing.Size(50, 20)
    Me.edCodpromo1.TabIndex = 112
    Me.edCodpromo1.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCodpromo1.TextInt = 0
    '
    'edQuant1
    '
    Me.edQuant1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edQuant1.EditValue = "1"
    Me.edQuant1.Location = New System.Drawing.Point(655, 20)
    Me.edQuant1.Name = "edQuant1"
    Me.edQuant1.NTSDbField = ""
    Me.edQuant1.NTSFormat = "0"
    Me.edQuant1.NTSForzaVisZoom = False
    Me.edQuant1.NTSOldValue = "1"
    Me.edQuant1.Properties.Appearance.Options.UseTextOptions = True
    Me.edQuant1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edQuant1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edQuant1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edQuant1.Properties.AutoHeight = False
    Me.edQuant1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edQuant1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edQuant1.Size = New System.Drawing.Size(98, 20)
    Me.edQuant1.TabIndex = 114
    Me.edQuant1.TextDec = New Decimal(New Integer() {1, 0, 0, 0})
    Me.edQuant1.TextInt = 1
    '
    'edQuant2
    '
    Me.edQuant2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edQuant2.EditValue = "1"
    Me.edQuant2.Location = New System.Drawing.Point(655, 43)
    Me.edQuant2.Name = "edQuant2"
    Me.edQuant2.NTSDbField = ""
    Me.edQuant2.NTSFormat = "0"
    Me.edQuant2.NTSForzaVisZoom = False
    Me.edQuant2.NTSOldValue = "1"
    Me.edQuant2.Properties.Appearance.Options.UseTextOptions = True
    Me.edQuant2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edQuant2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edQuant2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edQuant2.Properties.AutoHeight = False
    Me.edQuant2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edQuant2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edQuant2.Size = New System.Drawing.Size(98, 20)
    Me.edQuant2.TabIndex = 125
    Me.edQuant2.TextDec = New Decimal(New Integer() {1, 0, 0, 0})
    Me.edQuant2.TextInt = 1
    '
    'lbXx_Codpromo2
    '
    Me.lbXx_Codpromo2.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codpromo2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codpromo2.Location = New System.Drawing.Point(322, 42)
    Me.lbXx_Codpromo2.Name = "lbXx_Codpromo2"
    Me.lbXx_Codpromo2.NTSDbField = ""
    Me.lbXx_Codpromo2.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_Codpromo2.TabIndex = 124
    Me.lbXx_Codpromo2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_Codpromo2.Tooltip = ""
    Me.lbXx_Codpromo2.UseMnemonic = False
    '
    'edCodpromo2
    '
    Me.edCodpromo2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodpromo2.EditValue = "0"
    Me.edCodpromo2.Location = New System.Drawing.Point(266, 42)
    Me.edCodpromo2.Name = "edCodpromo2"
    Me.edCodpromo2.NTSDbField = ""
    Me.edCodpromo2.NTSFormat = "0"
    Me.edCodpromo2.NTSForzaVisZoom = False
    Me.edCodpromo2.NTSOldValue = ""
    Me.edCodpromo2.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodpromo2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodpromo2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodpromo2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodpromo2.Properties.AutoHeight = False
    Me.edCodpromo2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodpromo2.Properties.MaxLength = 65536
    Me.edCodpromo2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodpromo2.Size = New System.Drawing.Size(50, 20)
    Me.edCodpromo2.TabIndex = 123
    Me.edCodpromo2.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCodpromo2.TextInt = 0
    '
    'lbXx_Codvalu2
    '
    Me.lbXx_Codvalu2.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codvalu2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codvalu2.Location = New System.Drawing.Point(167, 42)
    Me.lbXx_Codvalu2.Name = "lbXx_Codvalu2"
    Me.lbXx_Codvalu2.NTSDbField = ""
    Me.lbXx_Codvalu2.Size = New System.Drawing.Size(93, 20)
    Me.lbXx_Codvalu2.TabIndex = 122
    Me.lbXx_Codvalu2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_Codvalu2.Tooltip = ""
    Me.lbXx_Codvalu2.UseMnemonic = False
    '
    'edCodvalu2
    '
    Me.edCodvalu2.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edCodvalu2.EditValue = "0"
    Me.edCodvalu2.Location = New System.Drawing.Point(111, 42)
    Me.edCodvalu2.Name = "edCodvalu2"
    Me.edCodvalu2.NTSDbField = ""
    Me.edCodvalu2.NTSFormat = "0"
    Me.edCodvalu2.NTSForzaVisZoom = False
    Me.edCodvalu2.NTSOldValue = ""
    Me.edCodvalu2.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodvalu2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodvalu2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodvalu2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodvalu2.Properties.AutoHeight = False
    Me.edCodvalu2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodvalu2.Properties.MaxLength = 65536
    Me.edCodvalu2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodvalu2.Size = New System.Drawing.Size(50, 20)
    Me.edCodvalu2.TabIndex = 121
    Me.edCodvalu2.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCodvalu2.TextInt = 0
    '
    'edListino2
    '
    Me.edListino2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edListino2.EditValue = "0"
    Me.edListino2.Location = New System.Drawing.Point(53, 42)
    Me.edListino2.Name = "edListino2"
    Me.edListino2.NTSDbField = ""
    Me.edListino2.NTSFormat = "0"
    Me.edListino2.NTSForzaVisZoom = False
    Me.edListino2.NTSOldValue = ""
    Me.edListino2.Properties.Appearance.Options.UseTextOptions = True
    Me.edListino2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edListino2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edListino2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edListino2.Properties.AutoHeight = False
    Me.edListino2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edListino2.Properties.MaxLength = 65536
    Me.edListino2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edListino2.Size = New System.Drawing.Size(50, 20)
    Me.edListino2.TabIndex = 117
    Me.edListino2.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edListino2.TextInt = 0
    '
    'cbTipo2
    '
    Me.cbTipo2.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipo2.DataSource = Nothing
    Me.cbTipo2.DisplayMember = ""
    Me.cbTipo2.Location = New System.Drawing.Point(536, 43)
    Me.cbTipo2.Name = "cbTipo2"
    Me.cbTipo2.NTSDbField = ""
    Me.cbTipo2.Properties.AutoHeight = False
    Me.cbTipo2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipo2.Properties.DropDownRows = 30
    Me.cbTipo2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipo2.SelectedValue = ""
    Me.cbTipo2.Size = New System.Drawing.Size(113, 20)
    Me.cbTipo2.TabIndex = 116
    Me.cbTipo2.ValueMember = ""
    '
    'ckSecondoPrezzo
    '
    Me.ckSecondoPrezzo.Cursor = System.Windows.Forms.Cursors.Arrow
    Me.ckSecondoPrezzo.Location = New System.Drawing.Point(2, 23)
    Me.ckSecondoPrezzo.Name = "ckSecondoPrezzo"
    Me.ckSecondoPrezzo.NTSCheckValue = "S"
    Me.ckSecondoPrezzo.NTSUnCheckValue = "N"
    Me.ckSecondoPrezzo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSecondoPrezzo.Properties.Appearance.Options.UseBackColor = True
    Me.ckSecondoPrezzo.Properties.AutoHeight = False
    Me.ckSecondoPrezzo.Properties.Caption = "2∞"
    Me.ckSecondoPrezzo.Size = New System.Drawing.Size(34, 19)
    Me.ckSecondoPrezzo.TabIndex = 115
    '
    'edQuant3
    '
    Me.edQuant3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edQuant3.EditValue = "1"
    Me.edQuant3.Location = New System.Drawing.Point(655, 65)
    Me.edQuant3.Name = "edQuant3"
    Me.edQuant3.NTSDbField = ""
    Me.edQuant3.NTSFormat = "0"
    Me.edQuant3.NTSForzaVisZoom = False
    Me.edQuant3.NTSOldValue = "1"
    Me.edQuant3.Properties.Appearance.Options.UseTextOptions = True
    Me.edQuant3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edQuant3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edQuant3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edQuant3.Properties.AutoHeight = False
    Me.edQuant3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edQuant3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edQuant3.Size = New System.Drawing.Size(98, 20)
    Me.edQuant3.TabIndex = 136
    Me.edQuant3.TextDec = New Decimal(New Integer() {1, 0, 0, 0})
    Me.edQuant3.TextInt = 1
    '
    'lbXx_Codpromo3
    '
    Me.lbXx_Codpromo3.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codpromo3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codpromo3.Location = New System.Drawing.Point(322, 64)
    Me.lbXx_Codpromo3.Name = "lbXx_Codpromo3"
    Me.lbXx_Codpromo3.NTSDbField = ""
    Me.lbXx_Codpromo3.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_Codpromo3.TabIndex = 135
    Me.lbXx_Codpromo3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_Codpromo3.Tooltip = ""
    Me.lbXx_Codpromo3.UseMnemonic = False
    '
    'edCodpromo3
    '
    Me.edCodpromo3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodpromo3.EditValue = "0"
    Me.edCodpromo3.Location = New System.Drawing.Point(266, 64)
    Me.edCodpromo3.Name = "edCodpromo3"
    Me.edCodpromo3.NTSDbField = ""
    Me.edCodpromo3.NTSFormat = "0"
    Me.edCodpromo3.NTSForzaVisZoom = False
    Me.edCodpromo3.NTSOldValue = ""
    Me.edCodpromo3.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodpromo3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodpromo3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodpromo3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodpromo3.Properties.AutoHeight = False
    Me.edCodpromo3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodpromo3.Properties.MaxLength = 65536
    Me.edCodpromo3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodpromo3.Size = New System.Drawing.Size(50, 20)
    Me.edCodpromo3.TabIndex = 134
    Me.edCodpromo3.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCodpromo3.TextInt = 0
    '
    'lbXx_Codvalu3
    '
    Me.lbXx_Codvalu3.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codvalu3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codvalu3.Location = New System.Drawing.Point(167, 64)
    Me.lbXx_Codvalu3.Name = "lbXx_Codvalu3"
    Me.lbXx_Codvalu3.NTSDbField = ""
    Me.lbXx_Codvalu3.Size = New System.Drawing.Size(93, 20)
    Me.lbXx_Codvalu3.TabIndex = 133
    Me.lbXx_Codvalu3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_Codvalu3.Tooltip = ""
    Me.lbXx_Codvalu3.UseMnemonic = False
    '
    'edCodvalu3
    '
    Me.edCodvalu3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodvalu3.EditValue = "0"
    Me.edCodvalu3.Location = New System.Drawing.Point(111, 64)
    Me.edCodvalu3.Name = "edCodvalu3"
    Me.edCodvalu3.NTSDbField = ""
    Me.edCodvalu3.NTSFormat = "0"
    Me.edCodvalu3.NTSForzaVisZoom = False
    Me.edCodvalu3.NTSOldValue = ""
    Me.edCodvalu3.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodvalu3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodvalu3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodvalu3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodvalu3.Properties.AutoHeight = False
    Me.edCodvalu3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodvalu3.Properties.MaxLength = 65536
    Me.edCodvalu3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodvalu3.Size = New System.Drawing.Size(50, 20)
    Me.edCodvalu3.TabIndex = 132
    Me.edCodvalu3.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCodvalu3.TextInt = 0
    '
    'edListino3
    '
    Me.edListino3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edListino3.EditValue = "0"
    Me.edListino3.Location = New System.Drawing.Point(53, 64)
    Me.edListino3.Name = "edListino3"
    Me.edListino3.NTSDbField = ""
    Me.edListino3.NTSFormat = "0"
    Me.edListino3.NTSForzaVisZoom = False
    Me.edListino3.NTSOldValue = ""
    Me.edListino3.Properties.Appearance.Options.UseTextOptions = True
    Me.edListino3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edListino3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edListino3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edListino3.Properties.AutoHeight = False
    Me.edListino3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edListino3.Properties.MaxLength = 65536
    Me.edListino3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edListino3.Size = New System.Drawing.Size(50, 20)
    Me.edListino3.TabIndex = 128
    Me.edListino3.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edListino3.TextInt = 0
    '
    'cbTipo3
    '
    Me.cbTipo3.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipo3.DataSource = Nothing
    Me.cbTipo3.DisplayMember = ""
    Me.cbTipo3.Location = New System.Drawing.Point(536, 65)
    Me.cbTipo3.Name = "cbTipo3"
    Me.cbTipo3.NTSDbField = ""
    Me.cbTipo3.Properties.AutoHeight = False
    Me.cbTipo3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipo3.Properties.DropDownRows = 30
    Me.cbTipo3.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipo3.SelectedValue = ""
    Me.cbTipo3.Size = New System.Drawing.Size(113, 20)
    Me.cbTipo3.TabIndex = 127
    Me.cbTipo3.ValueMember = ""
    '
    'ckTerzoPrezzo
    '
    Me.ckTerzoPrezzo.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTerzoPrezzo.Location = New System.Drawing.Point(2, 46)
    Me.ckTerzoPrezzo.Name = "ckTerzoPrezzo"
    Me.ckTerzoPrezzo.NTSCheckValue = "S"
    Me.ckTerzoPrezzo.NTSUnCheckValue = "N"
    Me.ckTerzoPrezzo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTerzoPrezzo.Properties.Appearance.Options.UseBackColor = True
    Me.ckTerzoPrezzo.Properties.AutoHeight = False
    Me.ckTerzoPrezzo.Properties.Caption = "3∞"
    Me.ckTerzoPrezzo.Size = New System.Drawing.Size(34, 19)
    Me.ckTerzoPrezzo.TabIndex = 126
    '
    'edQuant4
    '
    Me.edQuant4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edQuant4.EditValue = "1"
    Me.edQuant4.Location = New System.Drawing.Point(655, 87)
    Me.edQuant4.Name = "edQuant4"
    Me.edQuant4.NTSDbField = ""
    Me.edQuant4.NTSFormat = "0"
    Me.edQuant4.NTSForzaVisZoom = False
    Me.edQuant4.NTSOldValue = "1"
    Me.edQuant4.Properties.Appearance.Options.UseTextOptions = True
    Me.edQuant4.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edQuant4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edQuant4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edQuant4.Properties.AutoHeight = False
    Me.edQuant4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edQuant4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edQuant4.Size = New System.Drawing.Size(98, 20)
    Me.edQuant4.TabIndex = 147
    Me.edQuant4.TextDec = New Decimal(New Integer() {1, 0, 0, 0})
    Me.edQuant4.TextInt = 1
    '
    'lbXx_Codpromo4
    '
    Me.lbXx_Codpromo4.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codpromo4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codpromo4.Location = New System.Drawing.Point(322, 86)
    Me.lbXx_Codpromo4.Name = "lbXx_Codpromo4"
    Me.lbXx_Codpromo4.NTSDbField = ""
    Me.lbXx_Codpromo4.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_Codpromo4.TabIndex = 146
    Me.lbXx_Codpromo4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_Codpromo4.Tooltip = ""
    Me.lbXx_Codpromo4.UseMnemonic = False
    '
    'edCodpromo4
    '
    Me.edCodpromo4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodpromo4.EditValue = "0"
    Me.edCodpromo4.Location = New System.Drawing.Point(266, 86)
    Me.edCodpromo4.Name = "edCodpromo4"
    Me.edCodpromo4.NTSDbField = ""
    Me.edCodpromo4.NTSFormat = "0"
    Me.edCodpromo4.NTSForzaVisZoom = False
    Me.edCodpromo4.NTSOldValue = ""
    Me.edCodpromo4.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodpromo4.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodpromo4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodpromo4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodpromo4.Properties.AutoHeight = False
    Me.edCodpromo4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodpromo4.Properties.MaxLength = 65536
    Me.edCodpromo4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodpromo4.Size = New System.Drawing.Size(50, 20)
    Me.edCodpromo4.TabIndex = 145
    Me.edCodpromo4.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCodpromo4.TextInt = 0
    '
    'lbXx_Codvalu4
    '
    Me.lbXx_Codvalu4.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codvalu4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codvalu4.Location = New System.Drawing.Point(167, 86)
    Me.lbXx_Codvalu4.Name = "lbXx_Codvalu4"
    Me.lbXx_Codvalu4.NTSDbField = ""
    Me.lbXx_Codvalu4.Size = New System.Drawing.Size(93, 20)
    Me.lbXx_Codvalu4.TabIndex = 144
    Me.lbXx_Codvalu4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_Codvalu4.Tooltip = ""
    Me.lbXx_Codvalu4.UseMnemonic = False
    '
    'edCodvalu4
    '
    Me.edCodvalu4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodvalu4.EditValue = "0"
    Me.edCodvalu4.Location = New System.Drawing.Point(111, 86)
    Me.edCodvalu4.Name = "edCodvalu4"
    Me.edCodvalu4.NTSDbField = ""
    Me.edCodvalu4.NTSFormat = "0"
    Me.edCodvalu4.NTSForzaVisZoom = False
    Me.edCodvalu4.NTSOldValue = ""
    Me.edCodvalu4.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodvalu4.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodvalu4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodvalu4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodvalu4.Properties.AutoHeight = False
    Me.edCodvalu4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodvalu4.Properties.MaxLength = 65536
    Me.edCodvalu4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodvalu4.Size = New System.Drawing.Size(50, 20)
    Me.edCodvalu4.TabIndex = 143
    Me.edCodvalu4.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCodvalu4.TextInt = 0
    '
    'edListino4
    '
    Me.edListino4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edListino4.EditValue = "0"
    Me.edListino4.Location = New System.Drawing.Point(53, 86)
    Me.edListino4.Name = "edListino4"
    Me.edListino4.NTSDbField = ""
    Me.edListino4.NTSFormat = "0"
    Me.edListino4.NTSForzaVisZoom = False
    Me.edListino4.NTSOldValue = ""
    Me.edListino4.Properties.Appearance.Options.UseTextOptions = True
    Me.edListino4.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edListino4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edListino4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edListino4.Properties.AutoHeight = False
    Me.edListino4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edListino4.Properties.MaxLength = 65536
    Me.edListino4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edListino4.Size = New System.Drawing.Size(50, 20)
    Me.edListino4.TabIndex = 139
    Me.edListino4.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edListino4.TextInt = 0
    '
    'cbTipo4
    '
    Me.cbTipo4.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipo4.DataSource = Nothing
    Me.cbTipo4.DisplayMember = ""
    Me.cbTipo4.Location = New System.Drawing.Point(536, 87)
    Me.cbTipo4.Name = "cbTipo4"
    Me.cbTipo4.NTSDbField = ""
    Me.cbTipo4.Properties.AutoHeight = False
    Me.cbTipo4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipo4.Properties.DropDownRows = 30
    Me.cbTipo4.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipo4.SelectedValue = ""
    Me.cbTipo4.Size = New System.Drawing.Size(113, 20)
    Me.cbTipo4.TabIndex = 138
    Me.cbTipo4.ValueMember = ""
    '
    'ckQuartoPrezzo
    '
    Me.ckQuartoPrezzo.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckQuartoPrezzo.Location = New System.Drawing.Point(2, 69)
    Me.ckQuartoPrezzo.Name = "ckQuartoPrezzo"
    Me.ckQuartoPrezzo.NTSCheckValue = "S"
    Me.ckQuartoPrezzo.NTSUnCheckValue = "N"
    Me.ckQuartoPrezzo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckQuartoPrezzo.Properties.Appearance.Options.UseBackColor = True
    Me.ckQuartoPrezzo.Properties.AutoHeight = False
    Me.ckQuartoPrezzo.Properties.Caption = "4∞"
    Me.ckQuartoPrezzo.Size = New System.Drawing.Size(34, 19)
    Me.ckQuartoPrezzo.TabIndex = 137
    '
    'edQuantScont1
    '
    Me.edQuantScont1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edQuantScont1.EditValue = "1"
    Me.edQuantScont1.Location = New System.Drawing.Point(655, 21)
    Me.edQuantScont1.Name = "edQuantScont1"
    Me.edQuantScont1.NTSDbField = ""
    Me.edQuantScont1.NTSFormat = "0"
    Me.edQuantScont1.NTSForzaVisZoom = False
    Me.edQuantScont1.NTSOldValue = "1"
    Me.edQuantScont1.Properties.Appearance.Options.UseTextOptions = True
    Me.edQuantScont1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edQuantScont1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edQuantScont1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edQuantScont1.Properties.AutoHeight = False
    Me.edQuantScont1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edQuantScont1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edQuantScont1.Size = New System.Drawing.Size(98, 20)
    Me.edQuantScont1.TabIndex = 158
    Me.edQuantScont1.TextDec = New Decimal(New Integer() {1, 0, 0, 0})
    Me.edQuantScont1.TextInt = 1
    '
    'lbXx_Codtpro1
    '
    Me.lbXx_Codtpro1.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codtpro1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codtpro1.Location = New System.Drawing.Point(322, 21)
    Me.lbXx_Codtpro1.Name = "lbXx_Codtpro1"
    Me.lbXx_Codtpro1.NTSDbField = ""
    Me.lbXx_Codtpro1.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_Codtpro1.TabIndex = 157
    Me.lbXx_Codtpro1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_Codtpro1.Tooltip = ""
    Me.lbXx_Codtpro1.UseMnemonic = False
    '
    'edCodtpro1
    '
    Me.edCodtpro1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodtpro1.EditValue = "0"
    Me.edCodtpro1.Location = New System.Drawing.Point(266, 20)
    Me.edCodtpro1.Name = "edCodtpro1"
    Me.edCodtpro1.NTSDbField = ""
    Me.edCodtpro1.NTSFormat = "0"
    Me.edCodtpro1.NTSForzaVisZoom = False
    Me.edCodtpro1.NTSOldValue = ""
    Me.edCodtpro1.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodtpro1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodtpro1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodtpro1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodtpro1.Properties.AutoHeight = False
    Me.edCodtpro1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodtpro1.Properties.MaxLength = 65536
    Me.edCodtpro1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodtpro1.Size = New System.Drawing.Size(50, 20)
    Me.edCodtpro1.TabIndex = 156
    Me.edCodtpro1.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCodtpro1.TextInt = 0
    '
    'cbTipoSconto1
    '
    Me.cbTipoSconto1.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipoSconto1.DataSource = Nothing
    Me.cbTipoSconto1.DisplayMember = ""
    Me.cbTipoSconto1.Location = New System.Drawing.Point(536, 21)
    Me.cbTipoSconto1.Name = "cbTipoSconto1"
    Me.cbTipoSconto1.NTSDbField = ""
    Me.cbTipoSconto1.Properties.AutoHeight = False
    Me.cbTipoSconto1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipoSconto1.Properties.DropDownRows = 30
    Me.cbTipoSconto1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipoSconto1.SelectedValue = ""
    Me.cbTipoSconto1.Size = New System.Drawing.Size(113, 20)
    Me.cbTipoSconto1.TabIndex = 149
    Me.cbTipoSconto1.ValueMember = ""
    '
    'lbListino
    '
    Me.lbListino.AutoSize = True
    Me.lbListino.BackColor = System.Drawing.Color.Transparent
    Me.lbListino.Location = New System.Drawing.Point(50, 3)
    Me.lbListino.Name = "lbListino"
    Me.lbListino.NTSDbField = ""
    Me.lbListino.Size = New System.Drawing.Size(32, 13)
    Me.lbListino.TabIndex = 160
    Me.lbListino.Text = "LIST."
    Me.lbListino.Tooltip = ""
    Me.lbListino.UseMnemonic = False
    '
    'lbCodvalu
    '
    Me.lbCodvalu.AutoSize = True
    Me.lbCodvalu.BackColor = System.Drawing.Color.Transparent
    Me.lbCodvalu.Location = New System.Drawing.Point(108, 3)
    Me.lbCodvalu.Name = "lbCodvalu"
    Me.lbCodvalu.NTSDbField = ""
    Me.lbCodvalu.Size = New System.Drawing.Size(45, 13)
    Me.lbCodvalu.TabIndex = 162
    Me.lbCodvalu.Text = "VALUTA"
    Me.lbCodvalu.Tooltip = ""
    Me.lbCodvalu.UseMnemonic = False
    '
    'lbCodpromo
    '
    Me.lbCodpromo.AutoSize = True
    Me.lbCodpromo.BackColor = System.Drawing.Color.Transparent
    Me.lbCodpromo.Location = New System.Drawing.Point(263, 3)
    Me.lbCodpromo.Name = "lbCodpromo"
    Me.lbCodpromo.NTSDbField = ""
    Me.lbCodpromo.Size = New System.Drawing.Size(75, 13)
    Me.lbCodpromo.TabIndex = 163
    Me.lbCodpromo.Text = "PROMOZIONE"
    Me.lbCodpromo.Tooltip = ""
    Me.lbCodpromo.UseMnemonic = False
    '
    'lbTipo
    '
    Me.lbTipo.AutoSize = True
    Me.lbTipo.BackColor = System.Drawing.Color.Transparent
    Me.lbTipo.Location = New System.Drawing.Point(533, 3)
    Me.lbTipo.Name = "lbTipo"
    Me.lbTipo.NTSDbField = ""
    Me.lbTipo.Size = New System.Drawing.Size(31, 13)
    Me.lbTipo.TabIndex = 164
    Me.lbTipo.Text = "TIPO"
    Me.lbTipo.Tooltip = ""
    Me.lbTipo.UseMnemonic = False
    '
    'lbQuant
    '
    Me.lbQuant.AutoSize = True
    Me.lbQuant.BackColor = System.Drawing.Color.Transparent
    Me.lbQuant.Location = New System.Drawing.Point(652, 3)
    Me.lbQuant.Name = "lbQuant"
    Me.lbQuant.NTSDbField = ""
    Me.lbQuant.Size = New System.Drawing.Size(61, 13)
    Me.lbQuant.TabIndex = 165
    Me.lbQuant.Text = "QUANTITA'"
    Me.lbQuant.Tooltip = ""
    Me.lbQuant.UseMnemonic = False
    '
    'ckSconti1
    '
    Me.ckSconti1.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSconti1.Location = New System.Drawing.Point(2, 1)
    Me.ckSconti1.Name = "ckSconti1"
    Me.ckSconti1.NTSCheckValue = "S"
    Me.ckSconti1.NTSUnCheckValue = "N"
    Me.ckSconti1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSconti1.Properties.Appearance.Options.UseBackColor = True
    Me.ckSconti1.Properties.AutoHeight = False
    Me.ckSconti1.Properties.Caption = "1∞"
    Me.ckSconti1.Size = New System.Drawing.Size(34, 19)
    Me.ckSconti1.TabIndex = 166
    '
    'fmSelezionaArt
    '
    Me.fmSelezionaArt.AllowDrop = True
    Me.fmSelezionaArt.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmSelezionaArt.Appearance.Options.UseBackColor = True
    Me.fmSelezionaArt.Controls.Add(Me.cbOrdinaPer)
    Me.fmSelezionaArt.Controls.Add(Me.lbOrdinaPer)
    Me.fmSelezionaArt.Controls.Add(Me.cmdSeleziona)
    Me.fmSelezionaArt.Controls.Add(Me.ckSeleziona)
    Me.fmSelezionaArt.Controls.Add(Me.cbSelezione)
    Me.fmSelezionaArt.Controls.Add(Me.lbXx_Codlsar)
    Me.fmSelezionaArt.Controls.Add(Me.edCodlsar)
    Me.fmSelezionaArt.Cursor = System.Windows.Forms.Cursors.Hand
    Me.fmSelezionaArt.Location = New System.Drawing.Point(10, 291)
    Me.fmSelezionaArt.Name = "fmSelezionaArt"
    Me.fmSelezionaArt.Size = New System.Drawing.Size(761, 52)
    Me.fmSelezionaArt.TabIndex = 167
    Me.fmSelezionaArt.Text = "Articoli"
    '
    'cbOrdinaPer
    '
    Me.cbOrdinaPer.Cursor = System.Windows.Forms.Cursors.Hand
    Me.cbOrdinaPer.DataSource = Nothing
    Me.cbOrdinaPer.DisplayMember = ""
    Me.cbOrdinaPer.Location = New System.Drawing.Point(379, 24)
    Me.cbOrdinaPer.Name = "cbOrdinaPer"
    Me.cbOrdinaPer.NTSDbField = ""
    Me.cbOrdinaPer.Properties.AutoHeight = False
    Me.cbOrdinaPer.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbOrdinaPer.Properties.DropDownRows = 30
    Me.cbOrdinaPer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbOrdinaPer.SelectedValue = ""
    Me.cbOrdinaPer.Size = New System.Drawing.Size(151, 20)
    Me.cbOrdinaPer.TabIndex = 185
    Me.cbOrdinaPer.ValueMember = ""
    '
    'lbOrdinaPer
    '
    Me.lbOrdinaPer.AutoSize = True
    Me.lbOrdinaPer.BackColor = System.Drawing.Color.Transparent
    Me.lbOrdinaPer.Location = New System.Drawing.Point(319, 27)
    Me.lbOrdinaPer.Name = "lbOrdinaPer"
    Me.lbOrdinaPer.NTSDbField = ""
    Me.lbOrdinaPer.Size = New System.Drawing.Size(58, 13)
    Me.lbOrdinaPer.TabIndex = 184
    Me.lbOrdinaPer.Text = "Ordina per"
    Me.lbOrdinaPer.Tooltip = ""
    Me.lbOrdinaPer.UseMnemonic = False
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(146, 23)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.NTSContextMenu = Nothing
    Me.cmdSeleziona.Size = New System.Drawing.Size(160, 24)
    Me.cmdSeleziona.TabIndex = 169
    Me.cmdSeleziona.Text = "&Seleziona articoli..."
    '
    'cbSelezione
    '
    Me.cbSelezione.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbSelezione.DataSource = Nothing
    Me.cbSelezione.DisplayMember = ""
    Me.cbSelezione.Location = New System.Drawing.Point(6, 24)
    Me.cbSelezione.Name = "cbSelezione"
    Me.cbSelezione.NTSDbField = ""
    Me.cbSelezione.Properties.AutoHeight = False
    Me.cbSelezione.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbSelezione.Properties.DropDownRows = 30
    Me.cbSelezione.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbSelezione.SelectedValue = ""
    Me.cbSelezione.Size = New System.Drawing.Size(134, 20)
    Me.cbSelezione.TabIndex = 183
    Me.cbSelezione.ValueMember = ""
    '
    'lbXx_Codlsar
    '
    Me.lbXx_Codlsar.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codlsar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codlsar.Location = New System.Drawing.Point(217, 24)
    Me.lbXx_Codlsar.Name = "lbXx_Codlsar"
    Me.lbXx_Codlsar.NTSDbField = ""
    Me.lbXx_Codlsar.Size = New System.Drawing.Size(327, 20)
    Me.lbXx_Codlsar.TabIndex = 171
    Me.lbXx_Codlsar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_Codlsar.Tooltip = ""
    Me.lbXx_Codlsar.UseMnemonic = False
    '
    'edCodlsar
    '
    Me.edCodlsar.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodlsar.EditValue = "0"
    Me.edCodlsar.Location = New System.Drawing.Point(146, 24)
    Me.edCodlsar.Name = "edCodlsar"
    Me.edCodlsar.NTSDbField = ""
    Me.edCodlsar.NTSFormat = "0"
    Me.edCodlsar.NTSForzaVisZoom = False
    Me.edCodlsar.NTSOldValue = ""
    Me.edCodlsar.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodlsar.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodlsar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodlsar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodlsar.Properties.AutoHeight = False
    Me.edCodlsar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodlsar.Properties.MaxLength = 65536
    Me.edCodlsar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodlsar.Size = New System.Drawing.Size(65, 20)
    Me.edCodlsar.TabIndex = 170
    Me.edCodlsar.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCodlsar.TextInt = 0
    '
    'lbStatus
    '
    Me.lbStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbStatus.AutoSize = True
    Me.lbStatus.BackColor = System.Drawing.Color.Transparent
    Me.lbStatus.Location = New System.Drawing.Point(7, 345)
    Me.lbStatus.Name = "lbStatus"
    Me.lbStatus.NTSDbField = ""
    Me.lbStatus.Size = New System.Drawing.Size(43, 13)
    Me.lbStatus.TabIndex = 168
    Me.lbStatus.Text = "Pronto."
    Me.lbStatus.Tooltip = ""
    Me.lbStatus.UseMnemonic = False
    '
    'edQuant5
    '
    Me.edQuant5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edQuant5.EditValue = "1"
    Me.edQuant5.Enabled = False
    Me.edQuant5.Location = New System.Drawing.Point(655, 109)
    Me.edQuant5.Name = "edQuant5"
    Me.edQuant5.NTSDbField = ""
    Me.edQuant5.NTSFormat = "0"
    Me.edQuant5.NTSForzaVisZoom = False
    Me.edQuant5.NTSOldValue = "1"
    Me.edQuant5.Properties.Appearance.Options.UseTextOptions = True
    Me.edQuant5.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edQuant5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edQuant5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edQuant5.Properties.AutoHeight = False
    Me.edQuant5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edQuant5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edQuant5.Size = New System.Drawing.Size(98, 20)
    Me.edQuant5.TabIndex = 178
    Me.edQuant5.TextDec = New Decimal(New Integer() {1, 0, 0, 0})
    Me.edQuant5.TextInt = 1
    '
    'lbXx_Codpromo5
    '
    Me.lbXx_Codpromo5.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codpromo5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codpromo5.Location = New System.Drawing.Point(322, 108)
    Me.lbXx_Codpromo5.Name = "lbXx_Codpromo5"
    Me.lbXx_Codpromo5.NTSDbField = ""
    Me.lbXx_Codpromo5.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_Codpromo5.TabIndex = 177
    Me.lbXx_Codpromo5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_Codpromo5.Tooltip = ""
    Me.lbXx_Codpromo5.UseMnemonic = False
    '
    'edCodpromo5
    '
    Me.edCodpromo5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodpromo5.EditValue = "0"
    Me.edCodpromo5.Enabled = False
    Me.edCodpromo5.Location = New System.Drawing.Point(266, 108)
    Me.edCodpromo5.Name = "edCodpromo5"
    Me.edCodpromo5.NTSDbField = ""
    Me.edCodpromo5.NTSFormat = "0"
    Me.edCodpromo5.NTSForzaVisZoom = False
    Me.edCodpromo5.NTSOldValue = ""
    Me.edCodpromo5.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodpromo5.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodpromo5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodpromo5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodpromo5.Properties.AutoHeight = False
    Me.edCodpromo5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodpromo5.Properties.MaxLength = 65536
    Me.edCodpromo5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodpromo5.Size = New System.Drawing.Size(50, 20)
    Me.edCodpromo5.TabIndex = 176
    Me.edCodpromo5.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCodpromo5.TextInt = 0
    '
    'lbXx_Codvalu5
    '
    Me.lbXx_Codvalu5.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codvalu5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codvalu5.Location = New System.Drawing.Point(167, 108)
    Me.lbXx_Codvalu5.Name = "lbXx_Codvalu5"
    Me.lbXx_Codvalu5.NTSDbField = ""
    Me.lbXx_Codvalu5.Size = New System.Drawing.Size(93, 20)
    Me.lbXx_Codvalu5.TabIndex = 175
    Me.lbXx_Codvalu5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_Codvalu5.Tooltip = ""
    Me.lbXx_Codvalu5.UseMnemonic = False
    '
    'edCodvalu5
    '
    Me.edCodvalu5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodvalu5.EditValue = "0"
    Me.edCodvalu5.Enabled = False
    Me.edCodvalu5.Location = New System.Drawing.Point(111, 108)
    Me.edCodvalu5.Name = "edCodvalu5"
    Me.edCodvalu5.NTSDbField = ""
    Me.edCodvalu5.NTSFormat = "0"
    Me.edCodvalu5.NTSForzaVisZoom = False
    Me.edCodvalu5.NTSOldValue = ""
    Me.edCodvalu5.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodvalu5.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodvalu5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodvalu5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodvalu5.Properties.AutoHeight = False
    Me.edCodvalu5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodvalu5.Properties.MaxLength = 65536
    Me.edCodvalu5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodvalu5.Size = New System.Drawing.Size(50, 20)
    Me.edCodvalu5.TabIndex = 174
    Me.edCodvalu5.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCodvalu5.TextInt = 0
    '
    'edListino5
    '
    Me.edListino5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edListino5.EditValue = "0"
    Me.edListino5.Enabled = False
    Me.edListino5.Location = New System.Drawing.Point(53, 108)
    Me.edListino5.Name = "edListino5"
    Me.edListino5.NTSDbField = ""
    Me.edListino5.NTSFormat = "0"
    Me.edListino5.NTSForzaVisZoom = False
    Me.edListino5.NTSOldValue = ""
    Me.edListino5.Properties.Appearance.Options.UseTextOptions = True
    Me.edListino5.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edListino5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edListino5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edListino5.Properties.AutoHeight = False
    Me.edListino5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edListino5.Properties.MaxLength = 65536
    Me.edListino5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edListino5.Size = New System.Drawing.Size(50, 20)
    Me.edListino5.TabIndex = 171
    Me.edListino5.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edListino5.TextInt = 0
    '
    'cbTipo5
    '
    Me.cbTipo5.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipo5.DataSource = Nothing
    Me.cbTipo5.DisplayMember = ""
    Me.cbTipo5.Enabled = False
    Me.cbTipo5.Location = New System.Drawing.Point(536, 109)
    Me.cbTipo5.Name = "cbTipo5"
    Me.cbTipo5.NTSDbField = ""
    Me.cbTipo5.Properties.AutoHeight = False
    Me.cbTipo5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipo5.Properties.DropDownRows = 30
    Me.cbTipo5.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipo5.SelectedValue = ""
    Me.cbTipo5.Size = New System.Drawing.Size(113, 20)
    Me.cbTipo5.TabIndex = 170
    Me.cbTipo5.ValueMember = ""
    '
    'ckQuintoPrezzo
    '
    Me.ckQuintoPrezzo.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckQuintoPrezzo.Location = New System.Drawing.Point(2, 92)
    Me.ckQuintoPrezzo.Name = "ckQuintoPrezzo"
    Me.ckQuintoPrezzo.NTSCheckValue = "S"
    Me.ckQuintoPrezzo.NTSUnCheckValue = "N"
    Me.ckQuintoPrezzo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckQuintoPrezzo.Properties.Appearance.Options.UseBackColor = True
    Me.ckQuintoPrezzo.Properties.AutoHeight = False
    Me.ckQuintoPrezzo.Properties.Caption = "5∞"
    Me.ckQuintoPrezzo.Size = New System.Drawing.Size(34, 19)
    Me.ckQuintoPrezzo.TabIndex = 169
    '
    'fmPrezzi
    '
    Me.fmPrezzi.AllowDrop = True
    Me.fmPrezzi.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmPrezzi.Appearance.Options.UseBackColor = True
    Me.fmPrezzi.Controls.Add(Me.pnCheckPrezzi)
    Me.fmPrezzi.Controls.Add(Me.edQuant5)
    Me.fmPrezzi.Controls.Add(Me.lbXx_Codpromo5)
    Me.fmPrezzi.Controls.Add(Me.edCodpromo5)
    Me.fmPrezzi.Controls.Add(Me.lbXx_Codvalu5)
    Me.fmPrezzi.Controls.Add(Me.edCodvalu5)
    Me.fmPrezzi.Controls.Add(Me.edListino5)
    Me.fmPrezzi.Controls.Add(Me.cbTipo5)
    Me.fmPrezzi.Controls.Add(Me.cbTipo2)
    Me.fmPrezzi.Controls.Add(Me.edListino2)
    Me.fmPrezzi.Controls.Add(Me.edCodvalu2)
    Me.fmPrezzi.Controls.Add(Me.lbXx_Codvalu2)
    Me.fmPrezzi.Controls.Add(Me.edCodpromo2)
    Me.fmPrezzi.Controls.Add(Me.lbXx_Codpromo2)
    Me.fmPrezzi.Controls.Add(Me.edQuant2)
    Me.fmPrezzi.Controls.Add(Me.cbTipo3)
    Me.fmPrezzi.Controls.Add(Me.edListino3)
    Me.fmPrezzi.Controls.Add(Me.edQuant4)
    Me.fmPrezzi.Controls.Add(Me.edCodvalu3)
    Me.fmPrezzi.Controls.Add(Me.lbXx_Codpromo4)
    Me.fmPrezzi.Controls.Add(Me.lbXx_Codvalu3)
    Me.fmPrezzi.Controls.Add(Me.edCodpromo4)
    Me.fmPrezzi.Controls.Add(Me.edCodpromo3)
    Me.fmPrezzi.Controls.Add(Me.lbXx_Codvalu4)
    Me.fmPrezzi.Controls.Add(Me.lbXx_Codpromo3)
    Me.fmPrezzi.Controls.Add(Me.edCodvalu4)
    Me.fmPrezzi.Controls.Add(Me.edQuant3)
    Me.fmPrezzi.Controls.Add(Me.cbTipo4)
    Me.fmPrezzi.Controls.Add(Me.edListino4)
    Me.fmPrezzi.Controls.Add(Me.cbTipo1)
    Me.fmPrezzi.Controls.Add(Me.edCodvalu1)
    Me.fmPrezzi.Controls.Add(Me.lbXx_Codvalu1)
    Me.fmPrezzi.Controls.Add(Me.edCodpromo1)
    Me.fmPrezzi.Controls.Add(Me.lbXx_Codpromo1)
    Me.fmPrezzi.Controls.Add(Me.edQuant1)
    Me.fmPrezzi.Controls.Add(Me.edListino1)
    Me.fmPrezzi.Controls.Add(Me.lbQuant)
    Me.fmPrezzi.Controls.Add(Me.lbTipo)
    Me.fmPrezzi.Controls.Add(Me.lbCodpromo)
    Me.fmPrezzi.Controls.Add(Me.lbCodvalu)
    Me.fmPrezzi.Controls.Add(Me.lbListino)
    Me.fmPrezzi.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmPrezzi.Location = New System.Drawing.Point(10, 84)
    Me.fmPrezzi.Name = "fmPrezzi"
    Me.fmPrezzi.Size = New System.Drawing.Size(761, 132)
    Me.fmPrezzi.TabIndex = 179
    Me.fmPrezzi.Text = "Prezzi"
    '
    'pnCheckPrezzi
    '
    Me.pnCheckPrezzi.AllowDrop = True
    Me.pnCheckPrezzi.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCheckPrezzi.Appearance.Options.UseBackColor = True
    Me.pnCheckPrezzi.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCheckPrezzi.Controls.Add(Me.ckSecondoPrezzo)
    Me.pnCheckPrezzi.Controls.Add(Me.ckQuartoPrezzo)
    Me.pnCheckPrezzi.Controls.Add(Me.ckTerzoPrezzo)
    Me.pnCheckPrezzi.Controls.Add(Me.ckQuintoPrezzo)
    Me.pnCheckPrezzi.Controls.Add(Me.lbPrezzi1)
    Me.pnCheckPrezzi.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCheckPrezzi.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnCheckPrezzi.Location = New System.Drawing.Point(2, 20)
    Me.pnCheckPrezzi.Name = "pnCheckPrezzi"
    Me.pnCheckPrezzi.NTSActiveTrasparency = True
    Me.pnCheckPrezzi.Size = New System.Drawing.Size(45, 110)
    Me.pnCheckPrezzi.TabIndex = 183
    Me.pnCheckPrezzi.Text = "NtsPanel2"
    '
    'lbPrezzi1
    '
    Me.lbPrezzi1.AutoSize = True
    Me.lbPrezzi1.BackColor = System.Drawing.Color.Transparent
    Me.lbPrezzi1.ForeColor = System.Drawing.Color.Black
    Me.lbPrezzi1.Location = New System.Drawing.Point(18, 3)
    Me.lbPrezzi1.Name = "lbPrezzi1"
    Me.lbPrezzi1.NTSDbField = ""
    Me.lbPrezzi1.Size = New System.Drawing.Size(18, 13)
    Me.lbPrezzi1.TabIndex = 104
    Me.lbPrezzi1.Text = "1∞"
    Me.lbPrezzi1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbPrezzi1.Tooltip = ""
    Me.lbPrezzi1.UseMnemonic = False
    '
    'fmSconti
    '
    Me.fmSconti.AllowDrop = True
    Me.fmSconti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmSconti.Appearance.Options.UseBackColor = True
    Me.fmSconti.Controls.Add(Me.pnCheckSconti)
    Me.fmSconti.Controls.Add(Me.cbTipoSconto2)
    Me.fmSconti.Controls.Add(Me.edCodtpro2)
    Me.fmSconti.Controls.Add(Me.lbXx_Codtpro2)
    Me.fmSconti.Controls.Add(Me.edQuantScont2)
    Me.fmSconti.Controls.Add(Me.cbTipoSconto1)
    Me.fmSconti.Controls.Add(Me.edCodtpro1)
    Me.fmSconti.Controls.Add(Me.lbXx_Codtpro1)
    Me.fmSconti.Controls.Add(Me.edQuantScont1)
    Me.fmSconti.Controls.Add(Me.ckNoSoloSconti)
    Me.fmSconti.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmSconti.Location = New System.Drawing.Point(10, 220)
    Me.fmSconti.Name = "fmSconti"
    Me.fmSconti.Size = New System.Drawing.Size(761, 67)
    Me.fmSconti.TabIndex = 180
    Me.fmSconti.Text = "Sconti"
    '
    'pnCheckSconti
    '
    Me.pnCheckSconti.AllowDrop = True
    Me.pnCheckSconti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCheckSconti.Appearance.Options.UseBackColor = True
    Me.pnCheckSconti.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCheckSconti.Controls.Add(Me.ckSconti1)
    Me.pnCheckSconti.Controls.Add(Me.ckSconti2)
    Me.pnCheckSconti.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCheckSconti.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnCheckSconti.Location = New System.Drawing.Point(2, 20)
    Me.pnCheckSconti.Name = "pnCheckSconti"
    Me.pnCheckSconti.NTSActiveTrasparency = True
    Me.pnCheckSconti.Size = New System.Drawing.Size(45, 45)
    Me.pnCheckSconti.TabIndex = 182
    Me.pnCheckSconti.Text = "NtsPanel2"
    '
    'ckSconti2
    '
    Me.ckSconti2.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSconti2.Location = New System.Drawing.Point(3, 23)
    Me.ckSconti2.Name = "ckSconti2"
    Me.ckSconti2.NTSCheckValue = "S"
    Me.ckSconti2.NTSUnCheckValue = "N"
    Me.ckSconti2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSconti2.Properties.Appearance.Options.UseBackColor = True
    Me.ckSconti2.Properties.AutoHeight = False
    Me.ckSconti2.Properties.Caption = "2∞"
    Me.ckSconti2.Size = New System.Drawing.Size(34, 19)
    Me.ckSconti2.TabIndex = 171
    '
    'cbTipoSconto2
    '
    Me.cbTipoSconto2.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipoSconto2.DataSource = Nothing
    Me.cbTipoSconto2.DisplayMember = ""
    Me.cbTipoSconto2.Enabled = False
    Me.cbTipoSconto2.Location = New System.Drawing.Point(536, 43)
    Me.cbTipoSconto2.Name = "cbTipoSconto2"
    Me.cbTipoSconto2.NTSDbField = ""
    Me.cbTipoSconto2.Properties.AutoHeight = False
    Me.cbTipoSconto2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipoSconto2.Properties.DropDownRows = 30
    Me.cbTipoSconto2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipoSconto2.SelectedValue = ""
    Me.cbTipoSconto2.Size = New System.Drawing.Size(113, 20)
    Me.cbTipoSconto2.TabIndex = 167
    Me.cbTipoSconto2.ValueMember = ""
    '
    'edCodtpro2
    '
    Me.edCodtpro2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodtpro2.EditValue = "0"
    Me.edCodtpro2.Enabled = False
    Me.edCodtpro2.Location = New System.Drawing.Point(266, 42)
    Me.edCodtpro2.Name = "edCodtpro2"
    Me.edCodtpro2.NTSDbField = ""
    Me.edCodtpro2.NTSFormat = "0"
    Me.edCodtpro2.NTSForzaVisZoom = False
    Me.edCodtpro2.NTSOldValue = ""
    Me.edCodtpro2.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodtpro2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodtpro2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodtpro2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodtpro2.Properties.AutoHeight = False
    Me.edCodtpro2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodtpro2.Properties.MaxLength = 65536
    Me.edCodtpro2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodtpro2.Size = New System.Drawing.Size(50, 20)
    Me.edCodtpro2.TabIndex = 168
    Me.edCodtpro2.TextDec = New Decimal(New Integer() {0, 0, 0, 0})
    Me.edCodtpro2.TextInt = 0
    '
    'lbXx_Codtpro2
    '
    Me.lbXx_Codtpro2.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codtpro2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codtpro2.Location = New System.Drawing.Point(322, 42)
    Me.lbXx_Codtpro2.Name = "lbXx_Codtpro2"
    Me.lbXx_Codtpro2.NTSDbField = ""
    Me.lbXx_Codtpro2.Size = New System.Drawing.Size(208, 20)
    Me.lbXx_Codtpro2.TabIndex = 169
    Me.lbXx_Codtpro2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_Codtpro2.Tooltip = ""
    Me.lbXx_Codtpro2.UseMnemonic = False
    '
    'edQuantScont2
    '
    Me.edQuantScont2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edQuantScont2.EditValue = "1"
    Me.edQuantScont2.Enabled = False
    Me.edQuantScont2.Location = New System.Drawing.Point(655, 43)
    Me.edQuantScont2.Name = "edQuantScont2"
    Me.edQuantScont2.NTSDbField = ""
    Me.edQuantScont2.NTSFormat = "0"
    Me.edQuantScont2.NTSForzaVisZoom = False
    Me.edQuantScont2.NTSOldValue = "1"
    Me.edQuantScont2.Properties.Appearance.Options.UseTextOptions = True
    Me.edQuantScont2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edQuantScont2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edQuantScont2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edQuantScont2.Properties.AutoHeight = False
    Me.edQuantScont2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edQuantScont2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edQuantScont2.Size = New System.Drawing.Size(98, 20)
    Me.edQuantScont2.TabIndex = 170
    Me.edQuantScont2.TextDec = New Decimal(New Integer() {1, 0, 0, 0})
    Me.edQuantScont2.TextInt = 1
    '
    'fmCliFor
    '
    Me.fmCliFor.AllowDrop = True
    Me.fmCliFor.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmCliFor.Appearance.Options.UseBackColor = True
    Me.fmCliFor.Controls.Add(Me.cbSelClie)
    Me.fmCliFor.Controls.Add(Me.cmdSelCliFor)
    Me.fmCliFor.Controls.Add(Me.lbXx_Conto)
    Me.fmCliFor.Controls.Add(Me.edConto)
    Me.fmCliFor.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmCliFor.Location = New System.Drawing.Point(10, 37)
    Me.fmCliFor.Name = "fmCliFor"
    Me.fmCliFor.Size = New System.Drawing.Size(544, 44)
    Me.fmCliFor.TabIndex = 181
    Me.fmCliFor.Text = "Cliente\Fornitore"
    '
    'cbSelClie
    '
    Me.cbSelClie.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbSelClie.DataSource = Nothing
    Me.cbSelClie.DisplayMember = ""
    Me.cbSelClie.Location = New System.Drawing.Point(9, 22)
    Me.cbSelClie.Name = "cbSelClie"
    Me.cbSelClie.NTSDbField = ""
    Me.cbSelClie.Properties.AutoHeight = False
    Me.cbSelClie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbSelClie.Properties.DropDownRows = 30
    Me.cbSelClie.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbSelClie.SelectedValue = ""
    Me.cbSelClie.Size = New System.Drawing.Size(113, 20)
    Me.cbSelClie.TabIndex = 182
    Me.cbSelClie.ValueMember = ""
    '
    'cmdSelCliFor
    '
    Me.cmdSelCliFor.ImageText = ""
    Me.cmdSelCliFor.Location = New System.Drawing.Point(128, 20)
    Me.cmdSelCliFor.Name = "cmdSelCliFor"
    Me.cmdSelCliFor.NTSContextMenu = Nothing
    Me.cmdSelCliFor.Size = New System.Drawing.Size(208, 23)
    Me.cmdSelCliFor.TabIndex = 172
    Me.cmdSelCliFor.Text = "Seleziona &Clienti\Fornitori..."
    '
    'NtsPanel1
    '
    Me.NtsPanel1.AllowDrop = True
    Me.NtsPanel1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsPanel1.Appearance.Options.UseBackColor = True
    Me.NtsPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel1.Controls.Add(Me.lbDatagg)
    Me.NtsPanel1.Controls.Add(Me.edDatagg)
    Me.NtsPanel1.Controls.Add(Me.fmCliFor)
    Me.NtsPanel1.Controls.Add(Me.edCodling)
    Me.NtsPanel1.Controls.Add(Me.fmSconti)
    Me.NtsPanel1.Controls.Add(Me.lbCodling)
    Me.NtsPanel1.Controls.Add(Me.fmPrezzi)
    Me.NtsPanel1.Controls.Add(Me.lbXx_Codling)
    Me.NtsPanel1.Controls.Add(Me.lbStatus)
    Me.NtsPanel1.Controls.Add(Me.fmSelezionaArt)
    Me.NtsPanel1.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsPanel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.NtsPanel1.Location = New System.Drawing.Point(0, 30)
    Me.NtsPanel1.Name = "NtsPanel1"
    Me.NtsPanel1.NTSActiveTrasparency = True
    Me.NtsPanel1.Size = New System.Drawing.Size(781, 369)
    Me.NtsPanel1.TabIndex = 182
    Me.NtsPanel1.Text = "NtsPanel1"
    '
    'FRMMGSTLI_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(781, 399)
    Me.Controls.Add(Me.NtsPanel1)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRMMGSTLI_F"
    Me.Text = "STAMPA LISTINI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatagg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodling.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSeleziona.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNoSoloSconti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipo1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edListino1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodvalu1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodpromo1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edQuant1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edQuant2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodpromo2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodvalu2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edListino2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipo2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSecondoPrezzo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edQuant3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodpromo3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodvalu3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edListino3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipo3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTerzoPrezzo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edQuant4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodpromo4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodvalu4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edListino4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipo4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckQuartoPrezzo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edQuantScont1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodtpro1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipoSconto1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSconti1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmSelezionaArt, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmSelezionaArt.ResumeLayout(False)
    Me.fmSelezionaArt.PerformLayout()
    CType(Me.cbOrdinaPer.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbSelezione.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodlsar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edQuant5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodpromo5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodvalu5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edListino5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipo5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckQuintoPrezzo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmPrezzi, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmPrezzi.ResumeLayout(False)
    Me.fmPrezzi.PerformLayout()
    CType(Me.pnCheckPrezzi, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCheckPrezzi.ResumeLayout(False)
    Me.pnCheckPrezzi.PerformLayout()
    CType(Me.fmSconti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmSconti.ResumeLayout(False)
    Me.fmSconti.PerformLayout()
    CType(Me.pnCheckSconti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCheckSconti.ResumeLayout(False)
    Me.pnCheckSconti.PerformLayout()
    CType(Me.ckSconti2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipoSconto2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodtpro2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edQuantScont2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmCliFor, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmCliFor.ResumeLayout(False)
    CType(Me.cbSelClie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel1.ResumeLayout(False)
    Me.NtsPanel1.PerformLayout()
    Me.ResumeLayout(False)

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
    'creo e attivo l'entity e inizializzo la funzione che dovr‡ rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGSTLI", "BEMGSTLI", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128496233436616000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleStli = CType(oTmp, CLEMGSTLI)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGSTLI", strRemoteServer, strRemotePort)
    AddHandler oCleStli.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleStli.Init(oApp, oScript, oMenu.oCleComm, "MOVMAG", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)

    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbStampaWord.GlyphPath = (oApp.ChildImageDir & "\word.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
        tlbStampaGriglia.GlyphPath = (oApp.ChildImageDir & "\prngrid.gif")
        tlbImportExcel.GlyphPath = (oApp.ChildImageDir & "\import.gif")
        tlbCaricaGriglia.GlyphPath = (oApp.ChildImageDir & "\ordini.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'Ë una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()
      cbSelClie.NTSSetParam(oApp.Tr(Me, 130149818802134159, "Cliente/Fornitore"))
      cbSelezione.NTSSetParam(oApp.Tr(Me, 130149819032353799, "Selezione Articoli"))
      edCodlsar.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128672462893697755, "Lista selezionata"), tablsar)
      ckSconti1.NTSSetParam(oMenu, oApp.Tr(Me, 128672462894166505, "S&conti"), "S", "N")
      edQuantScont1.NTSSetParam(oMenu, oApp.Tr(Me, 128672462895416505, "Quantit‡ sconti"), "0", 10, 1, 9999999999)
      edCodtpro1.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128672462895729005, "Promozione sconti"), tabtpro)
      cbTipoSconto1.NTSSetParam(oApp.Tr(Me, 128672462895885255, "Tipo sconto"))
      edQuant4.NTSSetParam(oMenu, oApp.Tr(Me, 128672462896041505, "Quantit‡4"), "0", 10, 1, 9999999999)
      edCodpromo4.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128672462896354005, "Promozione4"), tabtpro)
      edCodvalu4.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128672462896666505, "Valuta4"), tabvalu)
      edListino4.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128672462897291505, "Listino4"), tablist)
      cbTipo4.NTSSetParam(oApp.Tr(Me, 128672462897447755, "Tipo4"))
      ckQuartoPrezzo.NTSSetParam(oMenu, oApp.Tr(Me, 128672462897604005, ""), "S", "N")
      edQuant3.NTSSetParam(oMenu, oApp.Tr(Me, 128672462897760255, "Quantit‡3"), "0", 10, 1, 9999999999)
      edCodpromo3.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128672462898072755, "Promozione3"), tabtpro)
      edCodvalu3.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128672462898385255, "Valuta3"), tabvalu)
      edListino3.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128672462899010255, "Listino3"), tablist)
      cbTipo3.NTSSetParam(oApp.Tr(Me, 128672462899166505, "Tipo3"))
      ckTerzoPrezzo.NTSSetParam(oMenu, oApp.Tr(Me, 128672462899322755, ""), "S", "N")
      edQuant2.NTSSetParam(oMenu, oApp.Tr(Me, 128672462899479005, "Quantit‡2"), "0", 10, 1, 9999999999)
      edCodpromo2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128672462899791505, "Promozione2"), tabtpro)
      edCodvalu2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128672462900104005, "Valuta2"), tabvalu)
      edListino2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128672462900729005, "Listino2"), tablist)
      cbTipo2.NTSSetParam(oApp.Tr(Me, 128672462900885255, "Tipo2"))
      ckSecondoPrezzo.NTSSetParam(oMenu, oApp.Tr(Me, 128672462901041505, ""), "S", "N")
      edQuant1.NTSSetParam(oMenu, oApp.Tr(Me, 128672462901197755, "Quantit‡1"), "0", 10, 1, 9999999999)
      edCodpromo1.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128672462901510255, "Promozione1"), tabtpro)
      edCodvalu1.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128672462901822755, "Valuta1"), tabvalu)
      edListino1.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128672462902447755, "Listino1"), tablist)
      cbTipo1.NTSSetParam(oApp.Tr(Me, 128672462902916505, "Tipo1"))
      ckNoSoloSconti.NTSSetParam(oMenu, oApp.Tr(Me, 128672462903072755, "Non stampare articoli in presenza di soli sconti"), "S", "N")
      ckSeleziona.NTSSetParam(oMenu, oApp.Tr(Me, 128672462903229005, "Non stampare articoli con dati a &zero"), "S", "N")
      edCodling.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128672462903541505, "Codice lingua"), tabling)
      edConto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128672462903854005, "Cliente/Fornitore"), tabanagra)
      edDatagg.NTSSetParam(oMenu, oApp.Tr(Me, 128672462904010255, "Data di validit‡"), False)

      cbTipoSconto2.NTSSetParam(oApp.Tr(Me, 129211463442878460, "Tipo sconto 2"))
      edCodtpro2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129211463443034714, "Promozione sconti 2"), tabtpro)
      edQuantScont2.NTSSetParam(oMenu, oApp.Tr(Me, 129211463443347222, "Quantit‡ sconti 2"), "0", 10, 1, 9999999999)
      ckSconti2.NTSSetParam(oMenu, oApp.Tr(Me, 129211463443503476, "2∞"), "S", "N")
      edQuant5.NTSSetParam(oMenu, oApp.Tr(Me, 129211463445066016, "Quantit‡5"), "0", 10, 1, 9999999999)
      edCodpromo5.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129211463445691032, "Promozione5"), tabtpro)
      edCodvalu5.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129211463446316048, "Valuta5"), tabvalu)
      edListino5.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129211463447253572, "Listino5"), tablist)
      cbTipo5.NTSSetParam(oApp.Tr(Me, 129211463447566080, "Tipo5"))
      ckQuintoPrezzo.NTSSetParam(oMenu, oApp.Tr(Me, 129211463447878588, "5∞"), "S", "N")
      ckSecondoPrezzo.NTSSetParam(oMenu, oApp.Tr(Me, 129211463448191096, "2∞"), "S", "N")
      ckTerzoPrezzo.NTSSetParam(oMenu, oApp.Tr(Me, 129211463450691160, "3∞"), "S", "N")
      ckQuartoPrezzo.NTSSetParam(oMenu, oApp.Tr(Me, 129211463453191224, "4∞"), "S", "N")

      '-------------------------------------------------
      'chiamo lo script per inizializzare i controlli caricati con source ext
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Public Overridable Sub CaricaCombo()
    Dim dttSelClie As New DataTable()
    Dim dttTipo As New DataTable()
    Dim dttTipoSconto As New DataTable()
    Dim dttOrderBy As New DataTable
    Dim dttSelezione As New DataTable

    Try
      dttSelClie.Columns.Add("cod", GetType(String))
      dttSelClie.Columns.Add("val", GetType(String))
      dttSelClie.Rows.Add(New Object() {"N", "Nessuno"})
      dttSelClie.Rows.Add(New Object() {"S", "Solo"})
      dttSelClie.Rows.Add(New Object() {"M", "Da selezione..."})
      dttSelClie.AcceptChanges()
      cbSelClie.DataSource = dttSelClie
      cbSelClie.ValueMember = "cod"
      cbSelClie.DisplayMember = "val"

      dttTipo.Columns.Add("cod", GetType(String))
      dttTipo.Columns.Add("val", GetType(String))
      dttTipo.Rows.Add(New Object() {"G", "Generico"})
      dttTipo.Rows.Add(New Object() {"S", "Speciale"})
      dttTipo.Rows.Add(New Object() {"P", "Prevalente"})
      dttTipo.AcceptChanges()
      cbTipo1.DataSource = dttTipo
      cbTipo1.ValueMember = "cod"
      cbTipo1.DisplayMember = "val"

      cbTipo2.DataSource = dttTipo
      cbTipo2.ValueMember = "cod"
      cbTipo2.DisplayMember = "val"

      cbTipo3.DataSource = dttTipo
      cbTipo3.ValueMember = "cod"
      cbTipo3.DisplayMember = "val"

      cbTipo4.DataSource = dttTipo
      cbTipo4.ValueMember = "cod"
      cbTipo4.DisplayMember = "val"

      cbTipo5.DataSource = dttTipo
      cbTipo5.ValueMember = "cod"
      cbTipo5.DisplayMember = "val"


      dttTipoSconto.Columns.Add("cod", GetType(String))
      dttTipoSconto.Columns.Add("val", GetType(String))
      If CBool(oApp.oGvar.strTrattaScA.ToUpper = "S") Then dttTipoSconto.Rows.Add(New Object() {"A", "Speciale cli./art."})
      If CBool(oApp.oGvar.strTrattaScB.ToUpper = "S") Then dttTipoSconto.Rows.Add(New Object() {"B", "Art./classe cliente"})
      If CBool(oApp.oGvar.strTrattaScC.ToUpper = "S") Then dttTipoSconto.Rows.Add(New Object() {"C", "Cliente/classe art."})
      If CBool(oApp.oGvar.strTrattaScD.ToUpper = "S") Then dttTipoSconto.Rows.Add(New Object() {"D", "Classe Cl./classe art."})
      If CBool(oApp.oGvar.strTrattaScE.ToUpper = "S") Then dttTipoSconto.Rows.Add(New Object() {"E", "Generico articolo"})
      If CBool(oApp.oGvar.strTrattaScF.ToUpper = "S") Then dttTipoSconto.Rows.Add(New Object() {"F", "Generico Cliente"})
      dttTipoSconto.Rows.Add(New Object() {"P", "Prevalente"})
      dttTipoSconto.AcceptChanges()
      cbTipoSconto1.DataSource = dttTipoSconto
      cbTipoSconto1.ValueMember = "cod"
      cbTipoSconto1.DisplayMember = "val"

      cbTipoSconto2.DataSource = dttTipoSconto
      cbTipoSconto2.ValueMember = "cod"
      cbTipoSconto2.DisplayMember = "val"

      dttOrderBy.Columns.Add("cod", GetType(String))
      dttOrderBy.Columns.Add("val", GetType(String))
      dttOrderBy.Rows.Add(New Object() {"0", "Codice articolo"})
      dttOrderBy.Rows.Add(New Object() {"1", "Codice alternativo"})
      dttOrderBy.Rows.Add(New Object() {"2", "Descrizione articolo"})
      dttOrderBy.AcceptChanges()
      cbOrdinaPer.DataSource = dttOrderBy
      cbOrdinaPer.ValueMember = "cod"
      cbOrdinaPer.DisplayMember = "val"

      dttSelezione.Columns.Add("cod", GetType(String))
      dttSelezione.Columns.Add("val", GetType(String))
      dttSelezione.Rows.Add(New Object() {"S", "Da selezione..."})
      dttSelezione.Rows.Add(New Object() {"L", "Da Lista Selezionata N∞"})
      dttSelezione.AcceptChanges()
      cbSelezione.DataSource = dttSelezione
      cbSelezione.ValueMember = "cod"
      cbSelezione.DisplayMember = "val"
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi di FORM"
  Public Overridable Sub FRMMGSTLI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '--------------------------------------------------------------------------------------------------------------
      oCleStli.bModuloCRM = False
      If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And CLN__STD.bsModExtCRM) Then oCleStli.bModuloCRM = True
      '--------------------------------------------------------------------------------------------------------------

      edQuant1.Text = "1" : edQuant2.Text = "1" : edQuant3.Text = "1" : edQuant4.Text = "1" : edQuant5.Text = "1"
      edQuantScont1.Text = "1" : edQuantScont2.Text = "1"

      oCleStli.lIITTStli = oMenu.GetTblInstId("TTSTLI", False)
      oCleStli.lIITTListsar = oMenu.GetTblInstId("TTLISTSAR", False)

      CaricaCombo()

      If oApp.oGvar.strGestioneSconti.ToUpper = "N" Then fmSconti.Visible = False

      '-----------------------------------------------------------------------------------------
      '--- Legge opzioni
      '-----------------------------------------------------------------------------------------
      oCleStli.nAltezzaGif = NTSCInt(oMenu.GetSettingBus("BSMGSTLI", "OPZIONI", ".", "AltezzaGif", "20", " ", "20"))
      '-----------------------------------------------------------------------------------------
      edDatagg.Text = Now.ToString
      edConto.Text = "0" : lbXx_Conto.Text = ""
      edCodling.Text = "0" : lbXx_Codling.Text = ""
      edListino1.Text = "1" : edListino2.Text = "0" : edListino3.Text = "0" : edListino4.Text = "0" : edListino5.Text = "0"
      edCodvalu1.Text = "0" : edCodvalu2.Text = "0" : edCodvalu3.Text = "0" : edCodvalu4.Text = "0" : edCodvalu5.Text = "0"
      edCodpromo1.Text = "0" : edCodpromo2.Text = "0" : edCodpromo3.Text = "0" : edCodpromo4.Text = "0" : edCodpromo5.Text = "0"
      edCodtpro1.Text = "0" : edCodtpro2.Text = "0"
      cbTipo1.SelectedValue = "G"
      cbTipo2.SelectedValue = "G"
      cbTipo3.SelectedValue = "G"
      cbTipo4.SelectedValue = "G"
      cbTipo5.SelectedValue = "G"
      cbTipoSconto1.SelectedValue = "A"
      cbTipoSconto2.SelectedValue = "A"
      edListino2.Enabled = False : edListino3.Enabled = False : edListino4.Enabled = False : edListino5.Enabled = False
      edCodvalu2.Enabled = False : edCodvalu3.Enabled = False : edCodvalu4.Enabled = False : edCodvalu5.Enabled = False
      edCodpromo2.Enabled = False : edCodpromo3.Enabled = False : edCodpromo4.Enabled = False : edCodpromo5.Enabled = False
      cbTipo2.Enabled = False : cbTipo3.Enabled = False : cbTipo4.Enabled = False : cbTipo5.Enabled = False
      edQuant2.Enabled = False : edQuant3.Enabled = False : edQuant4.Enabled = False : edQuant5.Enabled = False
      edCodtpro1.Enabled = False : cbTipoSconto1.Enabled = False : edQuantScont1.Enabled = False
      edCodtpro2.Enabled = False : cbTipoSconto2.Enabled = False : edQuantScont2.Enabled = False
      edCodlsar.Enabled = False
      ckSeleziona.Checked = False
      lbStatus.Text = oApp.Tr(Me, 130421088925258002, "Nessuna operazione in corso")
      '-----------------------------------------------------------------------------------------

      cbSelClie_SelectedValueChanged(Me, Nothing)

      lbStatus.Text = oApp.Tr(Me, 128850371253574696, "Pronto.")

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      GctlApplicaDefaultValue()

      'Prende la struttura della tabella
      oCleStli.GetTableStructMovIfil(dttDefault)

      dttDefault.Columns.Add("xx_descr")
      dttDefault.Columns.Add("xx_info")
      dttDefault.Columns.Add("xx_tipo")

      ComponiDatatable(dttDefault, Me)

      '--------------------------------------------------------------------------------------------------------------
      oCleStli.bIsCRMUser = oMenu.IsCrmUser(DittaCorrente)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGSTLI_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    Try
      oMenu.ResetTblInstId("TTSTLI", False, oCleStli.lIITTStli)
      oMenu.ResetTblInstId("TTLISTSAR", False, oCleStli.lIITTListsar)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGSTLI_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    '-------------------------------------------------
    'salvo il recent
  End Sub

  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Dim oPar As CLE__PATB = Nothing
    Try
      oPar = New CLE__PATB
      oPar.bVisGriglia = False
      oPar.strTipoArticolo = "N"
      oPar.strTipo = "BNMGSTLI"
      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oPar)
      oCleStli.strWhereFiar = oPar.strOut.Trim

      If oCleStli.strWhereFiar = "" Then
        oCleStli.bSeleziona = False
      Else
        oCleStli.bSeleziona = True
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim strTmp As String = ""
    Try
      'zoom standard
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      If edConto.Focused Then
        SetFastZoom(edConto.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = NTSCStr(edConto.Text)
        oParam.strTipo = "C"
        NTSZOOM.ZoomStrIn("ZOOMANAGRAC", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edConto.Text Then edConto.NTSTextDB = NTSZOOM.strIn
      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        NTSCallStandardZoom()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      Stampa(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaWord_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaWord.ItemClick
    Dim oPar As CLE__CLDP = Nothing
    Dim strQueryWord As String = ""
    Dim dsTmp As DataSet = Nothing
    Try
      Me.ValidaLastControl()

      If Not Elabora(False) Then Exit Sub

      '-----------------------
      'faccio comporre la query al dl
      oCleStli.bStampaWordRaggruppata = False

      strQueryWord = oCleStli.GetQueryStampaWord()
      If strQueryWord = "" Then Return

      '-----------------------
      'chiamo la stampa su word passandogli la query
      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strPar1 = "BNMGSTLI"
      oPar.strPar2 = strQueryWord
      Select Case NTSCStr(cbSelClie.SelectedValue)
        Case "N"
          oPar.dPar1 = 0
          oPar.strPar3 = ""
        Case "S"
          oPar.dPar1 = NTSCDec(edConto.Text)
          oPar.strPar3 = NTSCStr(lbXx_Conto.Text)
        Case Else
          ''' Gestione multicliente
      End Select
      oMenu.RunChild("NTSInformatica", "FRM__STW1", "", DittaCorrente, "", "BN__STWO", oPar, "", True, True)

      lbStatus.Text = oApp.Tr(Me, 128674384345548213, "Pronto.")

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub

  Public Overridable Sub tlbStampaGriglia_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaGriglia.ItemClick
    Dim frmLise As FRMMGLISE = Nothing
    Dim oPar As New CLE__CLDP

    Try
      '--------------------------------------------------------------------------------------------------------------
      If ElaboraLISTSES(False) = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      '--- Avvia il programma della griglia
      '--------------------------------------------------------------------------------------------------------------
      frmLise = CType(NTSNewFormModal("FRMMGLISE"), FRMMGLISE)
      If Not frmLise.Init(oMenu, Nothing, DittaCorrente) Then Return
      frmLise.InitEntity(oCleStli)
      frmLise.ShowDialog()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      If Not frmLise Is Nothing Then frmLise.Dispose()
      frmLise = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbCaricaGriglia_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCaricaGriglia.ItemClick
    Dim dsTmp As New DataSet
    Dim frmLise As FRMMGLISE = Nothing
    Dim oPar As New CLE__CLDP
    Try
      frmLise = CType(NTSNewFormModal("FRMMGLISE"), FRMMGLISE)
      If Not frmLise.Init(oMenu, oPar, DittaCorrente) Then Return
      frmLise.InitEntity(oCleStli)
      frmLise.ShowDialog()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

  Public Overridable Function Elabora(ByVal bGriglia As Boolean) As Boolean
    Dim nValoriz As Integer = 0
    Dim nMagaz As Integer = 0
    Dim strError As String = ""

    Try
      Me.ValidaLastControl()

      If Not CheckIntervalli() Then Return False

      oCleStli.bNessunoClie = CBool(IIf(cbSelClie.SelectedValue = "N", True, False))
      oCleStli.bSoloClie = CBool(IIf(cbSelClie.SelectedValue = "S", True, False))
      oCleStli.bMultiClie = CBool(IIf(cbSelClie.SelectedValue = "M", True, False))

      Me.Cursor = Cursors.WaitCursor

      '-------------------------
      'eseguo l'elaborazione
      If Not oCleStli.Elabora(CBool(IIf(NTSCStr(cbSelezione.SelectedValue) = "S", True, False)), _
                       CBool(IIf(NTSCStr(cbSelezione.SelectedValue) = "L", True, False)), _
                       edCodlsar.Text, NTSCStr(IIf(NTSCStr(cbSelClie.SelectedValue) = "N", "0", edConto.Text)), _
                       edDatagg.Text, cbTipo1.SelectedValue, edCodpromo1.Text, _
                       "0", edListino1.Text, edCodvalu1.Text, edQuant1.Text, _
                       ckSecondoPrezzo.Checked, cbTipo2.SelectedValue, edCodpromo2.Text, "0", _
                       edListino2.Text, edCodvalu2.Text, edQuant2.Text, ckTerzoPrezzo.Checked, _
                       cbTipo3.SelectedValue, edCodpromo3.Text, "0", edListino3.Text, _
                       edCodvalu3.Text, edQuant3.Text, ckQuartoPrezzo.Checked, cbTipo4.SelectedValue, _
                       edCodpromo4.Text, "0", edListino4.Text, edCodvalu4.Text, _
                       edQuant4.Text, ckQuintoPrezzo.Checked, cbTipo5.SelectedValue, _
                       edCodpromo5.Text, "0", edListino5.Text, edCodvalu5.Text, _
                       edQuant5.Text, ckSconti1.Checked, cbTipoSconto1.SelectedValue, edCodtpro1.Text, _
                       edQuantScont1.Text, ckSconti2.Checked, cbTipoSconto2.SelectedValue, edCodtpro2.Text, _
                       edQuantScont2.Text, False, "A", "0", "0", False, "A", "0", "0", _
                       edCodling.Text, ckSeleziona.Checked, ckNoSoloSconti.Checked, bGriglia, strError) Then
        If strError <> "" Then oApp.MsgBoxErr(strError)
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Function

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer
    Dim strNomRpt As String = ""
    Dim strKey As String = ""
    Dim nListino1, nListino2, nListino3, nListino4, nListino5 As Integer
    Dim strDeslistino1, strDeslistino2, strDeslistino3, strDeslistino4, strDeslistino5 As String
    Dim strLavorazione1, strLavorazione2, strLavorazione3, strLavorazione4, strLavorazione5 As String
    Dim strValuta1, strValuta2, strValuta3, strValuta4, strValuta5 As String
    Dim strPromozione1, strPromozione2, strPromozione3, strPromozione4, strPromozione5 As String
    Dim strTipo1, strTipo2, strTipo3, strTipo4, strTipo5 As String
    Dim dQuantita1, dQuantita2, dQuantita3, dQuantita4, dQuantita5 As Decimal
    Dim strPrezzo2, strPrezzo3, strPrezzo4, strPrezzo5 As String
    Dim strSconti1, strSconti2, strSconti3, strSconti4 As String
    Try
      '-----------------------------------------------------------------------------------------
      If ElaboraLISTSES(True) = False Then Return
      '-----------------------------------------------------------------------------------------
      '--- Setta le variabili da passare alle formule
      '-----------------------------------------------------------------------------------------
      strDeslistino1 = "" : strDeslistino2 = "" : strDeslistino3 = "" : strDeslistino4 = "" : strDeslistino5 = ""
      nListino1 = NTSCInt(edListino1.Text)
      If nListino1 <> 0 Then
        oCleStli.edListino_Validated(NTSCInt(edListino1.Text), strDeslistino1)
      End If
      strLavorazione1 = ""
      If NTSCInt(edCodvalu1.Text) > 0 Then strValuta1 = lbXx_Codvalu1.Text Else strValuta1 = ""
      If NTSCInt(edCodpromo1.Text) > 0 Then strPromozione1 = lbXx_Codpromo1.Text Else strPromozione1 = ""
      strTipo1 = NTSCStr(cbTipo1.SelectedItem)
      dQuantita1 = NTSCDec(edQuant1.Text)
      If ckSecondoPrezzo.Checked = True Then
        nListino2 = NTSCInt(edListino2.Text)
        If nListino2 <> 0 Then
          oCleStli.edListino_Validated(NTSCInt(edListino2.Text), strDeslistino2)
        End If
        strLavorazione2 = ""
        If NTSCInt(edCodvalu2.Text) > 0 Then strValuta2 = lbXx_Codvalu2.Text Else strValuta2 = ""
        If NTSCInt(edCodpromo2.Text) > 0 Then strPromozione2 = lbXx_Codpromo2.Text Else strPromozione2 = ""
        strTipo2 = NTSCStr(cbTipo2.SelectedItem)
        dQuantita2 = NTSCDec(edQuant2.Text)
        strPrezzo2 = "S"
      Else
        nListino2 = 0 : dQuantita2 = 0
        strLavorazione2 = "" : strValuta2 = "" : strPromozione2 = "" : strTipo2 = ""
        strPrezzo2 = "N"
      End If
      If ckTerzoPrezzo.Checked = True Then
        nListino3 = NTSCInt(edListino3.Text)
        If nListino3 <> 0 Then
          oCleStli.edListino_Validated(NTSCInt(edListino3.Text), strDeslistino3)
        End If
        strLavorazione3 = ""
        If NTSCInt(edCodvalu3.Text) > 0 Then strValuta3 = lbXx_Codvalu3.Text Else strValuta3 = ""
        If NTSCInt(edCodpromo3.Text) > 0 Then strPromozione3 = lbXx_Codpromo3.Text Else strPromozione3 = ""
        strTipo3 = NTSCStr(cbTipo3.SelectedItem)
        dQuantita3 = NTSCDec(edQuant3.Text)
        strPrezzo3 = "S"
      Else
        nListino3 = 0 : dQuantita3 = 0
        strLavorazione3 = "" : strValuta3 = "" : strPromozione3 = "" : strTipo3 = ""
        strPrezzo3 = "N"
      End If
      If ckQuartoPrezzo.Checked = True Then
        nListino4 = CInt(edListino4.Text)
        If nListino4 <> 0 Then
          oCleStli.edListino_Validated(NTSCInt(edListino4.Text), strDeslistino4)
        End If
        strLavorazione4 = ""
        If NTSCInt(edCodvalu4.Text) > 0 Then strValuta4 = lbXx_Codvalu4.Text Else strValuta4 = ""
        If NTSCInt(edCodpromo4.Text) > 0 Then strPromozione4 = lbXx_Codpromo4.Text Else strPromozione4 = ""
        strTipo4 = NTSCStr(cbTipo4.SelectedItem)
        dQuantita4 = NTSCDec(edQuant4.Text)
        strPrezzo4 = "S"
      Else
        nListino4 = 0 : dQuantita4 = 0
        strLavorazione4 = "" : strValuta4 = "" : strPromozione4 = "" : strTipo4 = ""
        strPrezzo4 = "N"
      End If
      If ckQuintoPrezzo.Checked = True Then
        nListino5 = CInt(edListino5.Text)
        If nListino5 <> 0 Then
          oCleStli.edListino_Validated(NTSCInt(edListino5.Text), strDeslistino5)
        End If
        strLavorazione5 = ""
        If NTSCInt(edCodvalu5.Text) > 0 Then strValuta5 = lbXx_Codvalu5.Text Else strValuta5 = ""
        If NTSCInt(edCodpromo5.Text) > 0 Then strPromozione5 = lbXx_Codpromo5.Text Else strPromozione5 = ""
        strTipo5 = NTSCStr(cbTipo5.SelectedItem)
        dQuantita5 = NTSCDec(edQuant5.Text)
        strPrezzo5 = "S"
      Else
        nListino5 = 0 : dQuantita5 = 0
        strLavorazione5 = "" : strValuta5 = "" : strPromozione5 = "" : strTipo5 = ""
        strPrezzo5 = "N"
      End If

      strSconti1 = NTSCStr(IIf(ckSconti1.Checked, "S", "N"))
      strSconti2 = NTSCStr(IIf(ckSconti2.Checked, "S", "N"))
      strSconti3 = "N"
      strSconti4 = "N"

      Me.Cursor = Cursors.WaitCursor

      '-----------------------------------------------------------------------------------------
      '--- Setta il nome del report e la cartella
      '-----------------------------------------------------------------------------------------
      If (strPrezzo4 = "N" And strPrezzo5 = "N") And (strSconti1 = "N" And strSconti2 = "N" And strSconti3 = "N" And strSconti4 = "N") Then
        If cbSelClie.SelectedValue = "N" Then
          strNomRpt = "BSMGSTLI.RPT"
          strKey = "Reports1"
        Else
          strNomRpt = "BSMGSTLA.RPT"
          strKey = "Reports3"
        End If
      Else
        If cbSelClie.SelectedValue = "N" Then
          strNomRpt = "BSMGSTL1.RPT"
          strKey = "Reports2"
        Else
          strNomRpt = "BSMGSTLB.RPT"
          strKey = "Reports4"
        End If
      End If
      '-----------------------------------------------------------------------------------------
      '--- Setta la formula di selezione
      '-----------------------------------------------------------------------------------------
      strCrpe = "{TTSTLI.codditt} = '" & DittaCorrente & "'" & _
        " And {TTSTLI.instid} = " & oCleStli.lIITTStli & _
        IIf(strNomRpt <> "BSMGSTLA.RPT" And strNomRpt <> "BSMGSTLB.RPT", " And {TTLISTSAR.instid} = " & oCleStli.lIITTListsar, "").ToString
      '--------------------------------------------------
      'preparo il motore di stampa
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGSTLI", strKey, " ", 0, nDestin, strNomRpt, False, "Stampa Listini", False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce gi‡ il multireport)
      For i = 1 To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))

        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "LISTINO1", NTSCStr(nListino1))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "LISTINO2", NTSCStr(nListino2))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "LISTINO3", NTSCStr(nListino3))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "LISTINO4", NTSCStr(nListino4))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "LISTINO5", NTSCStr(nListino5))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DESLISTINO1", ConvStrRpt(strDeslistino1))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DESLISTINO2", ConvStrRpt(strDeslistino2))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DESLISTINO3", ConvStrRpt(strDeslistino3))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DESLISTINO4", ConvStrRpt(strDeslistino4))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DESLISTINO5", ConvStrRpt(strDeslistino5))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "LAVORAZIONE1", ConvStrRpt(strLavorazione1))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "LAVORAZIONE2", ConvStrRpt(strLavorazione2))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "LAVORAZIONE3", ConvStrRpt(strLavorazione3))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "LAVORAZIONE4", ConvStrRpt(strLavorazione4))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "LAVORAZIONE5", ConvStrRpt(strLavorazione5))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "VALUTA1", ConvStrRpt(strValuta1))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "VALUTA2", ConvStrRpt(strValuta2))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "VALUTA3", ConvStrRpt(strValuta3))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "VALUTA4", ConvStrRpt(strValuta4))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "VALUTA5", ConvStrRpt(strValuta5))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "PROMOZIONE1", ConvStrRpt(strPromozione1))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "PROMOZIONE2", ConvStrRpt(strPromozione2))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "PROMOZIONE3", ConvStrRpt(strPromozione3))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "PROMOZIONE4", ConvStrRpt(strPromozione4))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "PROMOZIONE5", ConvStrRpt(strPromozione5))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "TIPO1", ConvStrRpt(strTipo1))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "TIPO2", ConvStrRpt(strTipo2))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "TIPO3", ConvStrRpt(strTipo3))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "TIPO4", ConvStrRpt(strTipo4))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "TIPO5", ConvStrRpt(strTipo5))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "QUANTITA1", NTSCStr(dQuantita1))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "QUANTITA2", NTSCStr(dQuantita2))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "QUANTITA3", NTSCStr(dQuantita3))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "QUANTITA4", NTSCStr(dQuantita4))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "QUANTITA5", NTSCStr(dQuantita5))
        If NTSCInt(edConto.Text) = 0 Then
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "CONTO", "''")
        Else
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "CONTO", ConvStrRpt(NTSCStr(edConto.Text) & " - " & NTSCStr(lbXx_Conto.Text)))
        End If
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "PREZZO2", ConvStrRpt(strPrezzo2))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "PREZZO3", ConvStrRpt(strPrezzo3))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "PREZZO4", ConvStrRpt(strPrezzo4))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "PREZZO5", ConvStrRpt(strPrezzo5))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "SCONTI1", ConvStrRpt(strSconti1))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "SCONTI2", ConvStrRpt(strSconti2))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "SCONTI3", ConvStrRpt(strSconti3))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "SCONTI4", ConvStrRpt(strSconti4))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DATAGG", ConvStrRpt(edDatagg.Text))

        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

      lbStatus.Text = oApp.Tr(Me, 128850371216565724, "Pronto.")

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
      lbStatus.Text = oApp.Tr(Me, 128498073391304728, "Pronto.")
    End Try
  End Sub

  Public Overridable Function CheckIntervalli() As Boolean
    Dim dsTmp As DataSet = Nothing
    Dim dRes As DialogResult
    Try
      '----------------------------------------------------------------------
      '--- Tipo selezione articoli
      If oCleStli.strWhereClie = "" And NTSCStr(cbSelClie.SelectedValue) = "M" Then
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129224699766849083, "Non sono stati selezionati i clienti\fornitori." & vbCrLf & "Selezionarli ora?")) = Windows.Forms.DialogResult.Yes Then
          cmdSelCliFor_Click(Me, Nothing)
          If oCleStli.strWhereClie = "" Then Return False
        Else
          Return False
        End If
      End If

      If (NTSCStr(cbSelezione.SelectedValue) = "S") And (oCleStli.bSeleziona = False) Then
        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128674290993239085, "Non sono stati selezionati gli articoli." & vbCrLf & "Selezionarli ora?"))
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          cmdSeleziona_Click(Me, Nothing)
          If oCleStli.bSeleziona = False Then
            Return False
          End If
        Else
          Return False
        End If
      End If
      If NTSCStr(cbSelezione.SelectedValue) = "L" Then
        If NTSCInt(edCodlsar.Text) = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128850371348361388, "Il codice lista selezionata deve essere compreso fra 1 e 999."))
          Return False
        Else
          oCleStli.GetListsar(edCodlsar.Text, dsTmp)

          If dsTmp.Tables("LISTSAR").Rows.Count = 0 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128674289071488519, "Non esistono articoli nella selezione prescelta con codice '|" & edCodlsar.Text & "|'." & vbCrLf & "Impossibile continuare."))
            Return False
          End If
        End If
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub ckQuartoPrezzo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckQuartoPrezzo.CheckedChanged
    Try
      edListino4.Text = "0"
      edCodvalu4.Text = "0" : lbXx_Codvalu4.Text = ""
      edCodpromo4.Text = "0" : lbXx_Codpromo4.Text = ""
      cbTipo4.SelectedValue = "G"
      edQuant4.Text = "1"

      If ckQuartoPrezzo.Checked = True Then
        GctlSetVisEnab(edListino4, False)
        GctlSetVisEnab(edCodvalu4, False)
        GctlSetVisEnab(edCodpromo4, False)
        GctlSetVisEnab(cbTipo4, False)
        GctlSetVisEnab(edQuant4, False)
      Else
        edListino4.Enabled = False
        edCodvalu4.Enabled = False
        edCodpromo4.Enabled = False
        cbTipo4.Enabled = False
        edQuant4.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckSecondoPrezzo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSecondoPrezzo.CheckedChanged
    Try
      edListino2.Text = "0"
      edCodvalu2.Text = "0" : lbXx_Codvalu2.Text = ""
      edCodpromo2.Text = "0" : lbXx_Codpromo2.Text = ""
      cbTipo2.SelectedValue = "G"
      edQuant2.Text = "1"

      If ckSecondoPrezzo.Checked = True Then
        GctlSetVisEnab(edListino2, False)
        GctlSetVisEnab(edCodvalu2, False)
        GctlSetVisEnab(edCodpromo2, False)
        GctlSetVisEnab(cbTipo2, False)
        GctlSetVisEnab(edQuant2, False)
      Else
        edListino2.Enabled = False
        edCodvalu2.Enabled = False
        edCodpromo2.Enabled = False
        cbTipo2.Enabled = False
        edQuant2.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTerzoPrezzo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckTerzoPrezzo.CheckedChanged
    Try
      edListino3.Text = "0"
      edCodvalu3.Text = "0" : lbXx_Codvalu3.Text = ""
      edCodpromo3.Text = "0" : lbXx_Codpromo3.Text = ""
      cbTipo3.SelectedValue = "G"
      edQuant3.Text = "1"

      If ckTerzoPrezzo.Checked = True Then
        GctlSetVisEnab(edListino3, False)
        GctlSetVisEnab(edCodvalu3, False)
        GctlSetVisEnab(edCodpromo3, False)
        GctlSetVisEnab(cbTipo3, False)
        GctlSetVisEnab(edQuant3, False)
      Else
        edListino3.Enabled = False
        edCodvalu3.Enabled = False
        edCodpromo3.Enabled = False
        cbTipo3.Enabled = False
        edQuant3.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckQuintoPrezzo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckQuintoPrezzo.CheckedChanged
    Try
      edListino5.Text = "0"
      edCodvalu5.Text = "0" : lbXx_Codvalu5.Text = ""
      edCodpromo5.Text = "0" : lbXx_Codpromo5.Text = ""
      cbTipo5.SelectedValue = "G"
      edQuant5.Text = "1"

      If ckQuintoPrezzo.Checked = True Then
        GctlSetVisEnab(edListino5, False)
        GctlSetVisEnab(edCodvalu5, False)
        GctlSetVisEnab(edCodpromo5, False)
        GctlSetVisEnab(cbTipo5, False)
        GctlSetVisEnab(edQuant5, False)
      Else
        edListino5.Enabled = False
        edCodvalu5.Enabled = False
        edCodpromo5.Enabled = False
        cbTipo5.Enabled = False
        edQuant5.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckSconti1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSconti1.CheckedChanged
    Try
      edCodtpro1.Text = "0" : lbXx_Codtpro1.Text = ""
      cbTipoSconto1.SelectedValue = "A"
      edQuantScont1.Text = "1"

      If ckSconti1.Checked = True Then
        GctlSetVisEnab(edCodtpro1, False)
        GctlSetVisEnab(cbTipoSconto1, False)
        GctlSetVisEnab(edQuantScont1, False)
      Else
        edCodtpro1.Enabled = False
        cbTipoSconto1.Enabled = False
        edQuantScont1.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckSconti2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSconti2.CheckedChanged
    Try
      edCodtpro2.Text = "0" : lbXx_Codtpro2.Text = ""
      cbTipoSconto2.SelectedValue = "A"
      edQuantScont2.Text = "1"

      If ckSconti2.Checked = True Then
        GctlSetVisEnab(edCodtpro2, False)
        GctlSetVisEnab(cbTipoSconto2, False)
        GctlSetVisEnab(edQuantScont2, False)
      Else
        edCodtpro2.Enabled = False
        cbTipoSconto2.Enabled = False
        edQuantScont2.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edCodling_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodling.Validated
    Try
      oCleStli.edCodling_Validated(NTSCInt(edCodling.Text), lbXx_Codling.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edCodlsar_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodlsar.Validated
    Try
      oCleStli.edCodlsar_Validated(NTSCInt(edCodlsar.Text), lbXx_Codlsar.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edCodpromo1_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodpromo1.Validated
    Try
      oCleStli.edCodpromo_Validated(NTSCInt(edCodpromo1.Text), lbXx_Codpromo1.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edCodpromo2_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodpromo2.Validated
    Try
      oCleStli.edCodpromo_Validated(NTSCInt(edCodpromo2.Text), lbXx_Codpromo2.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edCodpromo3_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodpromo3.Validated
    Try
      oCleStli.edCodpromo_Validated(NTSCInt(edCodpromo3.Text), lbXx_Codpromo3.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edCodpromo4_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodpromo4.Validated
    Try
      oCleStli.edCodpromo_Validated(NTSCInt(edCodpromo4.Text), lbXx_Codpromo4.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edCodpromo5_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodpromo5.Validated
    Try
      oCleStli.edCodpromo_Validated(NTSCInt(edCodpromo5.Text), lbXx_Codpromo5.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edCodtpro1_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodtpro1.Validated
    Try
      oCleStli.edCodpromo_Validated(NTSCInt(edCodtpro1.Text), lbXx_Codtpro1.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edCodtpro2_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodtpro2.Validated
    Try
      oCleStli.edCodpromo_Validated(NTSCInt(edCodtpro2.Text), lbXx_Codtpro2.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edCodvalu1_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodvalu1.Validated
    Try
      oCleStli.edCodvalu_Validated(NTSCInt(edCodvalu1.Text), lbXx_Codvalu1.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edCodvalu2_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodvalu2.Validated
    Try
      oCleStli.edCodvalu_Validated(NTSCInt(edCodvalu2.Text), lbXx_Codvalu2.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edCodvalu3_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodvalu3.Validated
    Try
      oCleStli.edCodvalu_Validated(NTSCInt(edCodvalu3.Text), lbXx_Codvalu3.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edCodvalu4_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodvalu4.Validated
    Try
      oCleStli.edCodvalu_Validated(NTSCInt(edCodvalu4.Text), lbXx_Codvalu4.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edCodvalu5_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodvalu4.Validated
    Try
      oCleStli.edCodvalu_Validated(NTSCInt(edCodvalu5.Text), lbXx_Codvalu5.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edConto_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edConto.Validated
    Try
      oCleStli.edConto_Validated(NTSCInt(edConto.Text), lbXx_Conto.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


  Public Overridable Sub cbSelClie_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSelClie.SelectedValueChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCStr(cbSelClie.SelectedValue)
        Case "N"
          oCleStli.strWhereClie = ""
          edConto.Text = "0"
          edConto.Enabled = False
          edConto.Visible = False
          lbXx_Conto.Visible = False
          cmdSelCliFor.Enabled = False
          cmdSelCliFor.Visible = False
          GctlSetVisEnab(tlbStampa, False)
          GctlSetVisEnab(tlbStampaVideo, False)
          GctlSetVisEnab(tlbStampaWord, False)
        Case "S"
          oCleStli.strWhereClie = ""
          cmdSelCliFor.Enabled = False
          cmdSelCliFor.Visible = False
          oCleStli.strWhereClie = ""
          GctlSetVisEnab(edConto, False)
          GctlSetVisEnab(edConto, True)
          GctlSetVisEnab(lbXx_Conto, True)
          edConto.Text = "0"
          edConto.Focus()
          GctlSetVisEnab(tlbStampa, False)
          GctlSetVisEnab(tlbStampaVideo, False)
          GctlSetVisEnab(tlbStampaWord, False)
        Case "M"
          oCleStli.strWhereClie = ""
          edConto.Text = "0"
          edConto.Enabled = False
          edConto.Visible = False
          lbXx_Conto.Visible = False
          GctlSetVisEnab(cmdSelCliFor, False)
          GctlSetVisEnab(cmdSelCliFor, True)
          tlbStampaWord.Enabled = False
          cmdSelCliFor.Focus()
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cbSelezione_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSelezione.SelectedValueChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCStr(cbSelezione.SelectedValue)
        Case "S"
          edCodlsar.Text = "0"
          edCodlsar.Enabled = False
          edCodlsar.Visible = False
          lbXx_Codlsar.Text = ""
          lbXx_Codlsar.Visible = False
          GctlSetVisEnab(cmdSeleziona, False)
          GctlSetVisEnab(cmdSeleziona, True)
          GctlSetVisEnab(lbOrdinaPer, True)
          GctlSetVisEnab(cbOrdinaPer, False)
          GctlSetVisEnab(cbOrdinaPer, True)
          cmdSeleziona.Focus()
        Case "L"
          cmdSeleziona.Enabled = False
          cmdSeleziona.Visible = False
          lbOrdinaPer.Visible = False
          cbOrdinaPer.Enabled = False
          cbOrdinaPer.Visible = False
          GctlSetVisEnab(edCodlsar, False)
          GctlSetVisEnab(edCodlsar, True)
          GctlSetVisEnab(lbXx_Codlsar, True)
          edCodlsar.Text = "0"
          edCodlsar.Focus()
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdSelCliFor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelCliFor.Click
    Dim oParam As New CLE__PATB
    Try
      edConto.Text = "0"
      oParam.bTipoProposto = True
      oParam.bVisGriglia = False
      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMANAGRAC", DittaCorrente, oParam)

      If NTSZOOM.strIn <> "" Then
        oCleStli.strWhereClie = NTSZOOM.strIn
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edConto_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles edConto.NTSZoomGest
    Dim bNuovo As Boolean = True
    Dim strTipo As String = "C"
    Dim oCZoo As New CLE__CZOO
    Dim oTmp As New Control
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo
      '--------------------------------------------------------------------------------------------------------------
      '--- Se il conto indicato √® esistente, ne preleva il tipo
      '--------------------------------------------------------------------------------------------------------------
      If (NTSCInt(edConto.Text) <> 0) And (NTSCInt(edConto.Text) <> 999999999) Then
        If oMenu.ValCodiceDb(edConto.Text, DittaCorrente, "ANAGRA", "N", "", dttTmp) = True Then
          strTipo = NTSCStr(dttTmp.Rows(0)!an_tipo).ToUpper
        End If
        dttTmp.Clear()
        dttTmp.Dispose()
      End If
      '--------------------------------------------------------------------------------------------------------------
      If e.TipoEvento = "OPEN" Then bNuovo = False
      '--------------------------------------------------------------------------------------------------------------
      oTmp.Text = edConto.Text
      NTSZOOM.OpenChildGest(oTmp, "ZOOMANAGRA" & strTipo, DittaCorrente, bNuovo)
      '--------------------------------------------------------------------------------------------------------------
      edConto.Focus()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Sub

  Public Overridable Sub cbOrdinaPer_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbOrdinaPer.SelectedValueChanged
    Try
      Select Case cbOrdinaPer.SelectedValue
        Case "0"
          oCleStli.strOrderBy = " ORDER BY artico.ar_codart"
        Case "1"
          oCleStli.strOrderBy = " ORDER BY artico.ar_codalt"
        Case "2"
          oCleStli.strOrderBy = " ORDER BY artico.ar_descr"
      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function ElaboraLISTSES(ByVal bReport As Boolean) As Boolean
    Dim dsTmp As New DataSet

    Try
      '--------------------------------------------------------------------------------------------------------------
      If (bReport = True) And (cbSelClie.SelectedValue = "N") Then GoTo VaiElaborazione
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla che non ci sia gi√† una sessione aperta
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleStli.GetListSes(dsTmp) Then Return False
      '--------------------------------------------------------------------------------------------------------------
      '--- Se c'Ë, chiede se la si vuole cancellare
      '--------------------------------------------------------------------------------------------------------------
      If dsTmp.Tables("TTSTLI").Rows.Count <> 0 Then
        If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 129211559138551057, "Attenzione!" & vbCrLf & _
          "Esiste gi‡† una sessione aperta." & vbCrLf & _
          "Continuando andr‡† persa." & vbCrLf & _
          "Procedere con la nuova elaborazione?")) = Windows.Forms.DialogResult.No Then Return False
        '------------------------------------------------------------------------------------------------------------
        '--- La cancella
        '------------------------------------------------------------------------------------------------------------
        If Not oCleStli.CancellaListSes() Then Return False
        '------------------------------------------------------------------------------------------------------------
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Procede con l'elaborazione specifica per la stampa su griglia
      '--------------------------------------------------------------------------------------------------------------
VaiElaborazione:
      '--------------------------------------------------------------------------------------------------------------
      If (bReport = True) And (cbSelClie.SelectedValue = "N") Then
        If Elabora(False) = False Then Return False
      Else
        If Elabora(True) = False Then Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      lbStatus.Text = oApp.Tr(Me, 129212570932656250, "Elaborazione completata.")
      Me.Refresh()
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dsTmp.Clear()
      dsTmp.Dispose()
    End Try
  End Function

#Region "Gestione filtri"
  Public Overridable Function ComponiDatatable(ByRef dttCampiForm As DataTable, ByVal oControl As Control) As Boolean
    Try
      'Verifico a quale controllo corrisponde e lo aggiungo al datatable dei campi.
      Select Case oControl.GetType().ToString
        Case "NTSInformatica.NTSTextBoxNum"
          dttCampiForm.Rows.Add()
          With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
            !xx_descr = CType(oControl, NTSTextBoxNum).strNomeCampo
            !xx_info = CType(oControl, NTSTextBoxNum).nMaxLen
            !mo_control = oControl.Name
            !mo_valore = oControl.Text
            !mo_locked = IIf(oControl.Enabled, "N", "S")
            !xx_tipo = "NTSTextBoxNum"
            !mo_ordin = dttCampiForm.Rows.Count
          End With
          Return True
        Case "NTSInformatica.NTSTextBoxStr"
          dttCampiForm.Rows.Add()
          With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
            !xx_descr = CType(oControl, NTSTextBoxStr).strNomeCampo
            !xx_info = CType(oControl, NTSTextBoxStr).nMaxLen
            !mo_control = oControl.Name
            !mo_valore = oControl.Text
            !mo_locked = IIf(oControl.Enabled, "N", "S")
            !xx_tipo = "NTSTextBoxStr"
            !mo_ordin = dttCampiForm.Rows.Count
          End With
          Return True
        Case "NTSInformatica.NTSTextBoxData"
          dttCampiForm.Rows.Add()
          With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
            !xx_descr = CType(oControl, NTSTextBoxData).strNomeCampo
            !mo_control = oControl.Name
            !mo_valore = oControl.Text
            !mo_locked = IIf(oControl.Enabled, "N", "S")
            !xx_tipo = "NTSTextBoxData"
            !mo_ordin = dttCampiForm.Rows.Count
          End With
          Return True
        Case "NTSInformatica.NTSMemoBox"
          dttCampiForm.Rows.Add()
          With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
            !xx_descr = CType(oControl, NTSMemoBox).strNomeCampo
            !mo_control = oControl.Name
            !mo_valore = oControl.Text
            !mo_locked = IIf(oControl.Enabled, "N", "S")
            !xx_tipo = "NTSMemoBox"
            !mo_ordin = dttCampiForm.Rows.Count
          End With
          Return True
        Case "NTSInformatica.NTSCheckBox"
          dttCampiForm.Rows.Add()
          With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
            !xx_descr = CType(oControl, NTSCheckBox).strNomeCampo
            !mo_control = oControl.Name
            !mo_valore = IIf(CType(oControl, NTSCheckBox).Checked, -1, 0)
            !mo_locked = IIf(oControl.Enabled, "N", "S")
            !xx_tipo = "NTSCheckBox"
            !mo_ordin = dttCampiForm.Rows.Count
          End With
          Return True
        Case "NTSInformatica.NTSComboBox"
          If oControl.Name = "cbFiltro" Then Return True ' Ë un componente per la gestione dei filtri

          dttCampiForm.Rows.Add()
          With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
            !xx_descr = CType(oControl, NTSComboBox).strNomeCampo
            !mo_control = oControl.Name
            !mo_valore = CType(oControl, NTSComboBox).SelectedValue
            !mo_locked = IIf(oControl.Enabled, "N", "S")
            !xx_tipo = "NTSComboBox"
            !mo_ordin = dttCampiForm.Rows.Count
          End With
          Return True
        Case "NTSInformatica.NTSRadioButton"
          dttCampiForm.Rows.Add()
          With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
            !xx_descr = CType(oControl, NTSRadioButton).strNomeCampo
            !xx_info = CType(oControl, NTSRadioButton).Parent.Name
            !mo_control = oControl.Name
            !mo_valore = IIf(CType(oControl, NTSRadioButton).Checked, -1, 0)
            !mo_locked = IIf(oControl.Enabled, "N", "S")
            !xx_tipo = "NTSRadioButton"
            !mo_ordin = dttCampiForm.Rows.Count
          End With
          Return True
      End Select

      'Ricorsivamente scorro tutti i controlli
      For z As Integer = 0 To oControl.Controls.Count - 1
        If Not ComponiDatatable(dttCampiForm, oControl.Controls(z)) Then Return False
      Next

      Return True
    Catch ex As Exception
      '---------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '---------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ApplicaFiltro(ByVal lCod As Integer) As Boolean
    Try
      If Me.NTSActiveFirstOccured = False Then Return True
      'Ripristina lo stato di default dei controlli
      GctlSetRoules()
      AggiornaForm(dttDefault, True)

      If lCod <> 0 Then
        'Carica il filtro e lo applica (la gctlSetRoules vince sempre)
        If Not oCleStli.LeggiFiltro(lCod, "BNMGSTLI", "FRMMGSTLI", dttPersForm) Then Return False
        AggiornaForm(dttPersForm, False)
      End If

      Return True
    Catch ex As Exception
      '---------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '---------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggiornaForm(ByVal dttControl As DataTable, ByVal bDoEnable As Boolean) As Boolean
    Dim oControl As New Object
    Try
      For z As Integer = 0 To dttControl.Rows.Count - 1
        oControl = NTSFindControlByName(Me, NTSCStr(dttControl.Rows(z)!mo_control))
        If oControl Is Nothing Then Continue For

        Select Case oControl.GetType().ToString
          Case "NTSInformatica.NTSButton"
            Dim cmdControl As NTSButton = CType(oControl, NTSButton)
            If cmdControl.Enabled Or bDoEnable Then ' Se il controllo Ë abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              cmdControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSTextBoxNum"
            Dim edControl As NTSTextBoxNum = CType(oControl, NTSTextBoxNum)
            edControl.Text = NTSCInt(dttControl.Rows(z)!mo_valore).ToString
            If edControl.Enabled Or bDoEnable Then ' Se il controllo Ë abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              edControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSTextBoxStr"
            Dim edControl As NTSTextBoxStr = CType(oControl, NTSTextBoxStr)
            edControl.Text = NTSCStr(dttControl.Rows(z)!mo_valore)
            If edControl.Enabled Or bDoEnable Then ' Se il controllo Ë abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              edControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSTextBoxData"
            Dim edControl As NTSTextBoxData = CType(oControl, NTSTextBoxData)
            edControl.Text = ConvertiInData(NTSCStr(dttControl.Rows(z)!mo_valore))
            If edControl.Enabled Or bDoEnable Then ' Se il controllo Ë abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              edControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSMemoBox"
            Dim edControl As NTSMemoBox = CType(oControl, NTSMemoBox)
            edControl.Text = NTSCStr(dttControl.Rows(z)!mo_valore)
            If edControl.Enabled Or bDoEnable Then ' Se il controllo Ë abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              edControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSCheckBox"
            Dim ckControl As NTSCheckBox = CType(oControl, NTSCheckBox)
            ckControl.Checked = CBool(dttControl.Rows(z)!mo_valore)
            If ckControl.Enabled Or bDoEnable Then ' Se il controllo Ë abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              ckControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSComboBox"
            Dim cbControl As NTSComboBox = CType(oControl, NTSComboBox)
            cbControl.SelectedValue = NTSCStr(dttControl.Rows(z)!mo_valore)
            If cbControl.Enabled Or bDoEnable Then ' Se il controllo Ë abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              cbControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSRadioButton"
            Dim opControl As NTSRadioButton = CType(oControl, NTSRadioButton)
            If CBool(dttControl.Rows(z)!mo_valore) Then opControl.Checked = True
            If opControl.Enabled Or bDoEnable Then ' Se il controllo Ë abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              opControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSGrid"
        End Select
      Next

      Return True
    Catch ex As Exception
      '---------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '---------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ConvertiInData(ByVal strData As String) As String
    Dim strPart() As String
    Try
      strData = strData.Trim
      strPart = strData.Split(" "c)

      'Non Ë di tipo "Data Odierna", "+1 Giorno", ecc... tutti con uno spazio in mezzo
      If strPart.Length = 1 Then Return strData

      'Data odierna Ë quella del giorno di stampa
      If strData.ToLower = "data odierna" Then Return Now.ToShortDateString

      'dovrebbero essere supportati solo i nomi italiani, ma aggiungerne anche altri non costa niente.
      Select Case strPart(1).ToLower
        Case "giorni", "giorno", "g", "day", "days", "d"
          Return Now.AddDays(NTSCInt(strPart(0))).ToShortDateString
        Case "mese", "mesi", "month", "months", "m"
          Return Now.AddMonths(NTSCInt(strPart(0))).ToShortDateString
        Case "anno", "anni", "a", "year", "years", "y"
          Return Now.AddYears(NTSCInt(strPart(0))).ToShortDateString
      End Select

      'Se non riesce a codificarla in nulla ritorna la data di oggi
      Return Now.ToShortDateString
    Catch ex As Exception
      '---------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '---------------------------------------------------------------
      Return ""
    End Try
  End Function
#End Region
End Class