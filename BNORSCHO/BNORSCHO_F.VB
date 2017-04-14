Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORSCHO_F
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

#Region "Variabili"
  Public oCleScho As CLEORSCHO
  Public oCallParams As CLE__CLDP
  Public strProntoMess As String = ""
  Public dttDefault, dttPersForm As New DataTable 'Per la gestione dei filtri

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
  Public WithEvents tlbStampaGriglia As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbStatus As NTSInformatica.NTSLabel
  Public WithEvents ckSalto As NTSInformatica.NTSCheckBox
  Public WithEvents ckEvasi As NTSInformatica.NTSCheckBox
  Public WithEvents lbDadatord As NTSInformatica.NTSLabel
  Public WithEvents edDadatord As NTSInformatica.NTSTextBoxData
  Public WithEvents edAagente As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDaagente As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDaagente As NTSInformatica.NTSLabel
  Public WithEvents edAmagaz As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDamagaz As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDamagaz As NTSInformatica.NTSLabel
  Public WithEvents edAcodart As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDacodart As NTSInformatica.NTSLabel
  Public WithEvents edDacodart As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAconto As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDaconto As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDaconto As NTSInformatica.NTSLabel
  Public WithEvents edAdatcons As NTSInformatica.NTSTextBoxData
  Public WithEvents edDadatcons As NTSInformatica.NTSTextBoxData
  Public WithEvents edAdatord As NTSInformatica.NTSTextBoxData
  Public WithEvents edSottogr As NTSInformatica.NTSTextBoxNum
  Public WithEvents edGruppo As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbGruppo As NTSInformatica.NTSLabel
  Public WithEvents edSerie As NTSInformatica.NTSTextBoxStr
  Public WithEvents ckTipork As NTSInformatica.NTSCheckBox
  Public WithEvents cbTipork As NTSInformatica.NTSComboBox
  Public WithEvents lbCodcfam As NTSInformatica.NTSLabel
  Public WithEvents edCodcfam As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbXx_Codcfam As NTSInformatica.NTSLabel
  Public WithEvents opTipoStampa1 As NTSInformatica.NTSRadioButton
  Public WithEvents opTipoStampa0 As NTSInformatica.NTSRadioButton
  Public WithEvents cbOrdin As NTSInformatica.NTSComboBox
  Public WithEvents tlbStampaWord As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaWordRaggr As NTSInformatica.NTSBarMenuItem
  Public WithEvents lbDadatcons As NTSInformatica.NTSLabel
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMORSCHO_F))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaWordRaggr = New NTSInformatica.NTSBarMenuItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaGriglia = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaWord = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.lbStatus = New NTSInformatica.NTSLabel
    Me.ckEvasi = New NTSInformatica.NTSCheckBox
    Me.ckSalto = New NTSInformatica.NTSCheckBox
    Me.lbDamagaz = New NTSInformatica.NTSLabel
    Me.edDamagaz = New NTSInformatica.NTSTextBoxNum
    Me.edAmagaz = New NTSInformatica.NTSTextBoxNum
    Me.edDaagente = New NTSInformatica.NTSTextBoxNum
    Me.lbDaagente = New NTSInformatica.NTSLabel
    Me.edAagente = New NTSInformatica.NTSTextBoxNum
    Me.edDadatord = New NTSInformatica.NTSTextBoxData
    Me.lbDadatord = New NTSInformatica.NTSLabel
    Me.edAdatord = New NTSInformatica.NTSTextBoxData
    Me.edAdatcons = New NTSInformatica.NTSTextBoxData
    Me.edDadatcons = New NTSInformatica.NTSTextBoxData
    Me.edAconto = New NTSInformatica.NTSTextBoxNum
    Me.edDaconto = New NTSInformatica.NTSTextBoxNum
    Me.lbDaconto = New NTSInformatica.NTSLabel
    Me.edDacodart = New NTSInformatica.NTSTextBoxStr
    Me.lbDacodart = New NTSInformatica.NTSLabel
    Me.edAcodart = New NTSInformatica.NTSTextBoxStr
    Me.edGruppo = New NTSInformatica.NTSTextBoxNum
    Me.lbGruppo = New NTSInformatica.NTSLabel
    Me.edSottogr = New NTSInformatica.NTSTextBoxNum
    Me.edSerie = New NTSInformatica.NTSTextBoxStr
    Me.cbTipork = New NTSInformatica.NTSComboBox
    Me.ckTipork = New NTSInformatica.NTSCheckBox
    Me.lbCodcfam = New NTSInformatica.NTSLabel
    Me.edCodcfam = New NTSInformatica.NTSTextBoxStr
    Me.lbXx_Codcfam = New NTSInformatica.NTSLabel
    Me.cbOrdin = New NTSInformatica.NTSComboBox
    Me.opTipoStampa1 = New NTSInformatica.NTSRadioButton
    Me.opTipoStampa0 = New NTSInformatica.NTSRadioButton
    Me.lbDadatcons = New NTSInformatica.NTSLabel
    Me.lbOrdinamento = New NTSInformatica.NTSLabel
    Me.ckSerie = New NTSInformatica.NTSCheckBox
    Me.NtsGridColumn1 = New NTSInformatica.NTSGridColumn
    Me.NtsGridColumn2 = New NTSInformatica.NTSGridColumn
    Me.NtsGridColumn3 = New NTSInformatica.NTSGridColumn
    Me.pnTipoStampa = New NTSInformatica.NTSPanel
    Me.lbTipoStampa = New NTSInformatica.NTSLabel
    Me.fmOpzioni = New NTSInformatica.NTSGroupBox
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckEvasi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSalto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDamagaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAmagaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDaagente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAagente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDadatord.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdatord.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdatcons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDadatcons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDaconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDacodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAcodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edGruppo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSottogr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipork.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTipork.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodcfam.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbOrdin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opTipoStampa1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opTipoStampa0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTipoStampa, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTipoStampa.SuspendLayout()
    CType(Me.fmOpzioni, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmOpzioni.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbStampaGriglia, Me.tlbStampaWord, Me.tlbStampaWordRaggr})
    Me.NtsBarManager1.MaxItemId = 21
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaGriglia), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaWord), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaWordRaggr)})
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
    'tlbStampaWordRaggr
    '
    Me.tlbStampaWordRaggr.Caption = "Stampa Word raggruppata"
    Me.tlbStampaWordRaggr.Id = 20
    Me.tlbStampaWordRaggr.Name = "tlbStampaWordRaggr"
    Me.tlbStampaWordRaggr.NTSIsCheckBox = False
    Me.tlbStampaWordRaggr.Visible = True
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
    'tlbStampaGriglia
    '
    Me.tlbStampaGriglia.Caption = "Stampa su griglia"
    Me.tlbStampaGriglia.Glyph = CType(resources.GetObject("tlbStampaGriglia.Glyph"), System.Drawing.Image)
    Me.tlbStampaGriglia.Id = 17
    Me.tlbStampaGriglia.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F11)
    Me.tlbStampaGriglia.Name = "tlbStampaGriglia"
    Me.tlbStampaGriglia.Visible = True
    '
    'tlbStampaWord
    '
    Me.tlbStampaWord.Caption = "StampaWord"
    Me.tlbStampaWord.Glyph = CType(resources.GetObject("tlbStampaWord.Glyph"), System.Drawing.Image)
    Me.tlbStampaWord.Id = 18
    Me.tlbStampaWord.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F7))
    Me.tlbStampaWord.Name = "tlbStampaWord"
    Me.tlbStampaWord.Visible = True
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
    'lbStatus
    '
    Me.lbStatus.AutoSize = True
    Me.lbStatus.BackColor = System.Drawing.Color.Transparent
    Me.lbStatus.Location = New System.Drawing.Point(12, 372)
    Me.lbStatus.Name = "lbStatus"
    Me.lbStatus.NTSDbField = ""
    Me.lbStatus.Size = New System.Drawing.Size(43, 13)
    Me.lbStatus.TabIndex = 34
    Me.lbStatus.Text = "Pronto."
    Me.lbStatus.Tooltip = ""
    Me.lbStatus.UseMnemonic = False
    '
    'ckEvasi
    '
    Me.ckEvasi.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.ckEvasi.Location = New System.Drawing.Point(17, 5)
    Me.ckEvasi.Name = "ckEvasi"
    Me.ckEvasi.NTSCheckValue = "S"
    Me.ckEvasi.NTSUnCheckValue = "N"
    Me.ckEvasi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckEvasi.Properties.Appearance.Options.UseBackColor = True
    Me.ckEvasi.Properties.Caption = "Considera solo le righe non evase"
    Me.ckEvasi.Size = New System.Drawing.Size(194, 19)
    Me.ckEvasi.TabIndex = 35
    '
    'ckSalto
    '
    Me.ckSalto.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSalto.Location = New System.Drawing.Point(17, 25)
    Me.ckSalto.Name = "ckSalto"
    Me.ckSalto.NTSCheckValue = "S"
    Me.ckSalto.NTSUnCheckValue = "N"
    Me.ckSalto.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSalto.Properties.Appearance.Options.UseBackColor = True
    Me.ckSalto.Properties.Caption = "&Salto pagina per ogni articolo"
    Me.ckSalto.Size = New System.Drawing.Size(175, 19)
    Me.ckSalto.TabIndex = 36
    '
    'lbDamagaz
    '
    Me.lbDamagaz.AutoSize = True
    Me.lbDamagaz.BackColor = System.Drawing.Color.Transparent
    Me.lbDamagaz.Location = New System.Drawing.Point(12, 71)
    Me.lbDamagaz.Name = "lbDamagaz"
    Me.lbDamagaz.NTSDbField = ""
    Me.lbDamagaz.Size = New System.Drawing.Size(88, 13)
    Me.lbDamagaz.TabIndex = 37
    Me.lbDamagaz.Text = "Magazzino DA /A"
    Me.lbDamagaz.Tooltip = ""
    Me.lbDamagaz.UseMnemonic = False
    '
    'edDamagaz
    '
    Me.edDamagaz.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edDamagaz.EditValue = "0"
    Me.edDamagaz.Location = New System.Drawing.Point(138, 68)
    Me.edDamagaz.Name = "edDamagaz"
    Me.edDamagaz.NTSDbField = ""
    Me.edDamagaz.NTSFormat = "0"
    Me.edDamagaz.NTSForzaVisZoom = False
    Me.edDamagaz.NTSOldValue = ""
    Me.edDamagaz.Properties.Appearance.Options.UseTextOptions = True
    Me.edDamagaz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDamagaz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDamagaz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDamagaz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDamagaz.Properties.MaxLength = 65536
    Me.edDamagaz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDamagaz.Size = New System.Drawing.Size(85, 20)
    Me.edDamagaz.TabIndex = 38
    '
    'edAmagaz
    '
    Me.edAmagaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAmagaz.EditValue = "0"
    Me.edAmagaz.Location = New System.Drawing.Point(324, 68)
    Me.edAmagaz.Name = "edAmagaz"
    Me.edAmagaz.NTSDbField = ""
    Me.edAmagaz.NTSFormat = "0"
    Me.edAmagaz.NTSForzaVisZoom = False
    Me.edAmagaz.NTSOldValue = ""
    Me.edAmagaz.Properties.Appearance.Options.UseTextOptions = True
    Me.edAmagaz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAmagaz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAmagaz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAmagaz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAmagaz.Properties.MaxLength = 65536
    Me.edAmagaz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAmagaz.Size = New System.Drawing.Size(84, 20)
    Me.edAmagaz.TabIndex = 39
    '
    'edDaagente
    '
    Me.edDaagente.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edDaagente.EditValue = "0"
    Me.edDaagente.Location = New System.Drawing.Point(138, 90)
    Me.edDaagente.Name = "edDaagente"
    Me.edDaagente.NTSDbField = ""
    Me.edDaagente.NTSFormat = "0"
    Me.edDaagente.NTSForzaVisZoom = False
    Me.edDaagente.NTSOldValue = ""
    Me.edDaagente.Properties.Appearance.Options.UseTextOptions = True
    Me.edDaagente.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDaagente.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDaagente.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDaagente.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDaagente.Properties.MaxLength = 65536
    Me.edDaagente.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDaagente.Size = New System.Drawing.Size(85, 20)
    Me.edDaagente.TabIndex = 42
    '
    'lbDaagente
    '
    Me.lbDaagente.AutoSize = True
    Me.lbDaagente.BackColor = System.Drawing.Color.Transparent
    Me.lbDaagente.Location = New System.Drawing.Point(12, 93)
    Me.lbDaagente.Name = "lbDaagente"
    Me.lbDaagente.NTSDbField = ""
    Me.lbDaagente.Size = New System.Drawing.Size(76, 13)
    Me.lbDaagente.TabIndex = 41
    Me.lbDaagente.Text = "Agente DA / A"
    Me.lbDaagente.Tooltip = ""
    Me.lbDaagente.UseMnemonic = False
    '
    'edAagente
    '
    Me.edAagente.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAagente.EditValue = "0"
    Me.edAagente.Location = New System.Drawing.Point(324, 90)
    Me.edAagente.Name = "edAagente"
    Me.edAagente.NTSDbField = ""
    Me.edAagente.NTSFormat = "0"
    Me.edAagente.NTSForzaVisZoom = False
    Me.edAagente.NTSOldValue = ""
    Me.edAagente.Properties.Appearance.Options.UseTextOptions = True
    Me.edAagente.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAagente.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAagente.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAagente.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAagente.Properties.MaxLength = 65536
    Me.edAagente.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAagente.Size = New System.Drawing.Size(84, 20)
    Me.edAagente.TabIndex = 44
    '
    'edDadatord
    '
    Me.edDadatord.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDadatord.Location = New System.Drawing.Point(138, 112)
    Me.edDadatord.Name = "edDadatord"
    Me.edDadatord.NTSDbField = ""
    Me.edDadatord.NTSForzaVisZoom = False
    Me.edDadatord.NTSOldValue = ""
    Me.edDadatord.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDadatord.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDadatord.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDadatord.Properties.MaxLength = 65536
    Me.edDadatord.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDadatord.Size = New System.Drawing.Size(85, 20)
    Me.edDadatord.TabIndex = 45
    '
    'lbDadatord
    '
    Me.lbDadatord.AutoSize = True
    Me.lbDadatord.BackColor = System.Drawing.Color.Transparent
    Me.lbDadatord.Location = New System.Drawing.Point(12, 115)
    Me.lbDadatord.Name = "lbDadatord"
    Me.lbDadatord.NTSDbField = ""
    Me.lbDadatord.Size = New System.Drawing.Size(97, 13)
    Me.lbDadatord.TabIndex = 46
    Me.lbDadatord.Text = "Data ordine DA / A"
    Me.lbDadatord.Tooltip = ""
    Me.lbDadatord.UseMnemonic = False
    '
    'edAdatord
    '
    Me.edAdatord.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdatord.Location = New System.Drawing.Point(324, 112)
    Me.edAdatord.Name = "edAdatord"
    Me.edAdatord.NTSDbField = ""
    Me.edAdatord.NTSForzaVisZoom = False
    Me.edAdatord.NTSOldValue = ""
    Me.edAdatord.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdatord.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdatord.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAdatord.Properties.MaxLength = 65536
    Me.edAdatord.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdatord.Size = New System.Drawing.Size(84, 20)
    Me.edAdatord.TabIndex = 47
    '
    'edAdatcons
    '
    Me.edAdatcons.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAdatcons.Location = New System.Drawing.Point(324, 134)
    Me.edAdatcons.Name = "edAdatcons"
    Me.edAdatcons.NTSDbField = ""
    Me.edAdatcons.NTSForzaVisZoom = False
    Me.edAdatcons.NTSOldValue = ""
    Me.edAdatcons.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdatcons.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdatcons.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAdatcons.Properties.MaxLength = 65536
    Me.edAdatcons.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdatcons.Size = New System.Drawing.Size(84, 20)
    Me.edAdatcons.TabIndex = 51
    '
    'edDadatcons
    '
    Me.edDadatcons.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edDadatcons.Location = New System.Drawing.Point(138, 134)
    Me.edDadatcons.Name = "edDadatcons"
    Me.edDadatcons.NTSDbField = ""
    Me.edDadatcons.NTSForzaVisZoom = False
    Me.edDadatcons.NTSOldValue = ""
    Me.edDadatcons.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDadatcons.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDadatcons.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDadatcons.Properties.MaxLength = 65536
    Me.edDadatcons.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDadatcons.Size = New System.Drawing.Size(85, 20)
    Me.edDadatcons.TabIndex = 49
    '
    'edAconto
    '
    Me.edAconto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAconto.EditValue = "0"
    Me.edAconto.Location = New System.Drawing.Point(324, 156)
    Me.edAconto.Name = "edAconto"
    Me.edAconto.NTSDbField = ""
    Me.edAconto.NTSFormat = "0"
    Me.edAconto.NTSForzaVisZoom = False
    Me.edAconto.NTSOldValue = ""
    Me.edAconto.Properties.Appearance.Options.UseTextOptions = True
    Me.edAconto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAconto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAconto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAconto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAconto.Properties.MaxLength = 65536
    Me.edAconto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAconto.Size = New System.Drawing.Size(84, 20)
    Me.edAconto.TabIndex = 55
    '
    'edDaconto
    '
    Me.edDaconto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDaconto.EditValue = "0"
    Me.edDaconto.Location = New System.Drawing.Point(138, 156)
    Me.edDaconto.Name = "edDaconto"
    Me.edDaconto.NTSDbField = ""
    Me.edDaconto.NTSFormat = "0"
    Me.edDaconto.NTSForzaVisZoom = False
    Me.edDaconto.NTSOldValue = ""
    Me.edDaconto.Properties.Appearance.Options.UseTextOptions = True
    Me.edDaconto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDaconto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDaconto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDaconto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDaconto.Properties.MaxLength = 65536
    Me.edDaconto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDaconto.Size = New System.Drawing.Size(85, 20)
    Me.edDaconto.TabIndex = 54
    '
    'lbDaconto
    '
    Me.lbDaconto.AutoSize = True
    Me.lbDaconto.BackColor = System.Drawing.Color.Transparent
    Me.lbDaconto.Location = New System.Drawing.Point(12, 159)
    Me.lbDaconto.Name = "lbDaconto"
    Me.lbDaconto.NTSDbField = ""
    Me.lbDaconto.Size = New System.Drawing.Size(70, 13)
    Me.lbDaconto.TabIndex = 53
    Me.lbDaconto.Text = "Conto DA / A"
    Me.lbDaconto.Tooltip = ""
    Me.lbDaconto.UseMnemonic = False
    '
    'edDacodart
    '
    Me.edDacodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDacodart.Location = New System.Drawing.Point(138, 178)
    Me.edDacodart.Name = "edDacodart"
    Me.edDacodart.NTSDbField = ""
    Me.edDacodart.NTSForzaVisZoom = False
    Me.edDacodart.NTSOldValue = ""
    Me.edDacodart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDacodart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDacodart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDacodart.Properties.MaxLength = 65536
    Me.edDacodart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDacodart.Size = New System.Drawing.Size(168, 20)
    Me.edDacodart.TabIndex = 57
    '
    'lbDacodart
    '
    Me.lbDacodart.AutoSize = True
    Me.lbDacodart.BackColor = System.Drawing.Color.Transparent
    Me.lbDacodart.Location = New System.Drawing.Point(12, 181)
    Me.lbDacodart.Name = "lbDacodart"
    Me.lbDacodart.NTSDbField = ""
    Me.lbDacodart.Size = New System.Drawing.Size(111, 13)
    Me.lbDacodart.TabIndex = 58
    Me.lbDacodart.Text = "Codice articolo DA / A"
    Me.lbDacodart.Tooltip = ""
    Me.lbDacodart.UseMnemonic = False
    '
    'edAcodart
    '
    Me.edAcodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAcodart.Location = New System.Drawing.Point(324, 178)
    Me.edAcodart.Name = "edAcodart"
    Me.edAcodart.NTSDbField = ""
    Me.edAcodart.NTSForzaVisZoom = False
    Me.edAcodart.NTSOldValue = ""
    Me.edAcodart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAcodart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAcodart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAcodart.Properties.MaxLength = 65536
    Me.edAcodart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAcodart.Size = New System.Drawing.Size(168, 20)
    Me.edAcodart.TabIndex = 60
    '
    'edGruppo
    '
    Me.edGruppo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edGruppo.EditValue = "0"
    Me.edGruppo.Location = New System.Drawing.Point(138, 200)
    Me.edGruppo.Name = "edGruppo"
    Me.edGruppo.NTSDbField = ""
    Me.edGruppo.NTSFormat = "0"
    Me.edGruppo.NTSForzaVisZoom = False
    Me.edGruppo.NTSOldValue = ""
    Me.edGruppo.Properties.Appearance.Options.UseTextOptions = True
    Me.edGruppo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edGruppo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edGruppo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edGruppo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edGruppo.Properties.MaxLength = 65536
    Me.edGruppo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edGruppo.Size = New System.Drawing.Size(85, 20)
    Me.edGruppo.TabIndex = 70
    '
    'lbGruppo
    '
    Me.lbGruppo.AutoSize = True
    Me.lbGruppo.BackColor = System.Drawing.Color.Transparent
    Me.lbGruppo.Location = New System.Drawing.Point(12, 203)
    Me.lbGruppo.Name = "lbGruppo"
    Me.lbGruppo.NTSDbField = ""
    Me.lbGruppo.Size = New System.Drawing.Size(106, 13)
    Me.lbGruppo.TabIndex = 69
    Me.lbGruppo.Text = "Gruppo/Sottogruppo"
    Me.lbGruppo.Tooltip = ""
    Me.lbGruppo.UseMnemonic = False
    '
    'edSottogr
    '
    Me.edSottogr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSottogr.EditValue = "0"
    Me.edSottogr.Location = New System.Drawing.Point(324, 200)
    Me.edSottogr.Name = "edSottogr"
    Me.edSottogr.NTSDbField = ""
    Me.edSottogr.NTSFormat = "0"
    Me.edSottogr.NTSForzaVisZoom = False
    Me.edSottogr.NTSOldValue = ""
    Me.edSottogr.Properties.Appearance.Options.UseTextOptions = True
    Me.edSottogr.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSottogr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSottogr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSottogr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSottogr.Properties.MaxLength = 65536
    Me.edSottogr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSottogr.Size = New System.Drawing.Size(84, 20)
    Me.edSottogr.TabIndex = 71
    '
    'edSerie
    '
    Me.edSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSerie.Location = New System.Drawing.Point(375, 244)
    Me.edSerie.Name = "edSerie"
    Me.edSerie.NTSDbField = ""
    Me.edSerie.NTSForzaVisZoom = False
    Me.edSerie.NTSOldValue = ""
    Me.edSerie.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSerie.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSerie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSerie.Properties.MaxLength = 65536
    Me.edSerie.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSerie.Size = New System.Drawing.Size(33, 20)
    Me.edSerie.TabIndex = 73
    '
    'cbTipork
    '
    Me.cbTipork.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipork.DataSource = Nothing
    Me.cbTipork.DisplayMember = ""
    Me.cbTipork.Location = New System.Drawing.Point(138, 244)
    Me.cbTipork.Name = "cbTipork"
    Me.cbTipork.NTSDbField = ""
    Me.cbTipork.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipork.Properties.DropDownRows = 30
    Me.cbTipork.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipork.SelectedValue = ""
    Me.cbTipork.Size = New System.Drawing.Size(155, 20)
    Me.cbTipork.TabIndex = 77
    Me.cbTipork.ValueMember = ""
    '
    'ckTipork
    '
    Me.ckTipork.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTipork.Location = New System.Drawing.Point(12, 245)
    Me.ckTipork.Name = "ckTipork"
    Me.ckTipork.NTSCheckValue = "S"
    Me.ckTipork.NTSUnCheckValue = "N"
    Me.ckTipork.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTipork.Properties.Appearance.Options.UseBackColor = True
    Me.ckTipork.Properties.Caption = "Tipo &Documento"
    Me.ckTipork.Size = New System.Drawing.Size(119, 19)
    Me.ckTipork.TabIndex = 78
    '
    'lbCodcfam
    '
    Me.lbCodcfam.AutoSize = True
    Me.lbCodcfam.BackColor = System.Drawing.Color.Transparent
    Me.lbCodcfam.Location = New System.Drawing.Point(12, 225)
    Me.lbCodcfam.Name = "lbCodcfam"
    Me.lbCodcfam.NTSDbField = ""
    Me.lbCodcfam.Size = New System.Drawing.Size(110, 13)
    Me.lbCodcfam.TabIndex = 80
    Me.lbCodcfam.Text = "Linea/famiglia articolo"
    Me.lbCodcfam.Tooltip = ""
    Me.lbCodcfam.UseMnemonic = False
    '
    'edCodcfam
    '
    Me.edCodcfam.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodcfam.Location = New System.Drawing.Point(138, 222)
    Me.edCodcfam.Name = "edCodcfam"
    Me.edCodcfam.NTSDbField = ""
    Me.edCodcfam.NTSForzaVisZoom = False
    Me.edCodcfam.NTSOldValue = ""
    Me.edCodcfam.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodcfam.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodcfam.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodcfam.Properties.MaxLength = 65536
    Me.edCodcfam.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodcfam.Size = New System.Drawing.Size(85, 20)
    Me.edCodcfam.TabIndex = 79
    '
    'lbXx_Codcfam
    '
    Me.lbXx_Codcfam.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codcfam.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codcfam.Location = New System.Drawing.Point(229, 222)
    Me.lbXx_Codcfam.Name = "lbXx_Codcfam"
    Me.lbXx_Codcfam.NTSDbField = ""
    Me.lbXx_Codcfam.Size = New System.Drawing.Size(253, 20)
    Me.lbXx_Codcfam.TabIndex = 81
    Me.lbXx_Codcfam.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_Codcfam.Tooltip = ""
    Me.lbXx_Codcfam.UseMnemonic = False
    '
    'cbOrdin
    '
    Me.cbOrdin.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbOrdin.DataSource = Nothing
    Me.cbOrdin.DisplayMember = ""
    Me.cbOrdin.Location = New System.Drawing.Point(138, 266)
    Me.cbOrdin.Name = "cbOrdin"
    Me.cbOrdin.NTSDbField = ""
    Me.cbOrdin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbOrdin.Properties.DropDownRows = 30
    Me.cbOrdin.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbOrdin.SelectedValue = ""
    Me.cbOrdin.Size = New System.Drawing.Size(106, 20)
    Me.cbOrdin.TabIndex = 0
    Me.cbOrdin.ValueMember = ""
    '
    'opTipoStampa1
    '
    Me.opTipoStampa1.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTipoStampa1.Location = New System.Drawing.Point(87, 3)
    Me.opTipoStampa1.Name = "opTipoStampa1"
    Me.opTipoStampa1.NTSCheckValue = "S"
    Me.opTipoStampa1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTipoStampa1.Properties.Appearance.Options.UseBackColor = True
    Me.opTipoStampa1.Properties.Caption = "Completa"
    Me.opTipoStampa1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTipoStampa1.Size = New System.Drawing.Size(75, 19)
    Me.opTipoStampa1.TabIndex = 1
    '
    'opTipoStampa0
    '
    Me.opTipoStampa0.Cursor = System.Windows.Forms.Cursors.Default
    Me.opTipoStampa0.EditValue = True
    Me.opTipoStampa0.Location = New System.Drawing.Point(1, 3)
    Me.opTipoStampa0.Name = "opTipoStampa0"
    Me.opTipoStampa0.NTSCheckValue = "S"
    Me.opTipoStampa0.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTipoStampa0.Properties.Appearance.Options.UseBackColor = True
    Me.opTipoStampa0.Properties.Caption = "Ridotta"
    Me.opTipoStampa0.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTipoStampa0.Size = New System.Drawing.Size(65, 19)
    Me.opTipoStampa0.TabIndex = 0
    '
    'lbDadatcons
    '
    Me.lbDadatcons.AutoSize = True
    Me.lbDadatcons.BackColor = System.Drawing.Color.Transparent
    Me.lbDadatcons.Location = New System.Drawing.Point(12, 137)
    Me.lbDadatcons.Name = "lbDadatcons"
    Me.lbDadatcons.NTSDbField = ""
    Me.lbDadatcons.Size = New System.Drawing.Size(113, 13)
    Me.lbDadatcons.TabIndex = 87
    Me.lbDadatcons.Text = "Data consegna DA / A"
    Me.lbDadatcons.Tooltip = ""
    Me.lbDadatcons.UseMnemonic = False
    '
    'lbOrdinamento
    '
    Me.lbOrdinamento.AutoSize = True
    Me.lbOrdinamento.BackColor = System.Drawing.Color.Transparent
    Me.lbOrdinamento.Location = New System.Drawing.Point(12, 269)
    Me.lbOrdinamento.Name = "lbOrdinamento"
    Me.lbOrdinamento.NTSDbField = ""
    Me.lbOrdinamento.Size = New System.Drawing.Size(73, 13)
    Me.lbOrdinamento.TabIndex = 92
    Me.lbOrdinamento.Text = "Ordinamento:"
    Me.lbOrdinamento.Tooltip = ""
    Me.lbOrdinamento.UseMnemonic = False
    '
    'ckSerie
    '
    Me.ckSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSerie.Location = New System.Drawing.Point(324, 244)
    Me.ckSerie.Name = "ckSerie"
    Me.ckSerie.NTSCheckValue = "S"
    Me.ckSerie.NTSUnCheckValue = "N"
    Me.ckSerie.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSerie.Properties.Appearance.Options.UseBackColor = True
    Me.ckSerie.Properties.Caption = "Serie"
    Me.ckSerie.Size = New System.Drawing.Size(55, 19)
    Me.ckSerie.TabIndex = 89
    '
    'NtsGridColumn1
    '
    Me.NtsGridColumn1.AppearanceCell.Options.UseBackColor = True
    Me.NtsGridColumn1.AppearanceCell.Options.UseTextOptions = True
    Me.NtsGridColumn1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.NtsGridColumn1.Caption = "Filtro"
    Me.NtsGridColumn1.Enabled = False
    Me.NtsGridColumn1.FieldName = "xx_nome"
    Me.NtsGridColumn1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.NtsGridColumn1.Name = "NtsGridColumn1"
    Me.NtsGridColumn1.NTSRepositoryComboBox = Nothing
    Me.NtsGridColumn1.NTSRepositoryItemCheck = Nothing
    Me.NtsGridColumn1.NTSRepositoryItemMemo = Nothing
    Me.NtsGridColumn1.NTSRepositoryItemText = Nothing
    Me.NtsGridColumn1.OptionsColumn.AllowEdit = False
    Me.NtsGridColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.NtsGridColumn1.OptionsColumn.ReadOnly = True
    Me.NtsGridColumn1.OptionsFilter.AllowFilter = False
    Me.NtsGridColumn1.Visible = True
    Me.NtsGridColumn1.VisibleIndex = 0
    Me.NtsGridColumn1.Width = 178
    '
    'NtsGridColumn2
    '
    Me.NtsGridColumn2.AppearanceCell.Options.UseBackColor = True
    Me.NtsGridColumn2.AppearanceCell.Options.UseTextOptions = True
    Me.NtsGridColumn2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.NtsGridColumn2.Caption = "Valore DA"
    Me.NtsGridColumn2.Enabled = True
    Me.NtsGridColumn2.FieldName = "xx_valoreda"
    Me.NtsGridColumn2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.NtsGridColumn2.Name = "NtsGridColumn2"
    Me.NtsGridColumn2.NTSRepositoryComboBox = Nothing
    Me.NtsGridColumn2.NTSRepositoryItemCheck = Nothing
    Me.NtsGridColumn2.NTSRepositoryItemMemo = Nothing
    Me.NtsGridColumn2.NTSRepositoryItemText = Nothing
    Me.NtsGridColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.NtsGridColumn2.OptionsFilter.AllowFilter = False
    Me.NtsGridColumn2.Visible = True
    Me.NtsGridColumn2.VisibleIndex = 1
    Me.NtsGridColumn2.Width = 300
    '
    'NtsGridColumn3
    '
    Me.NtsGridColumn3.AppearanceCell.Options.UseBackColor = True
    Me.NtsGridColumn3.AppearanceCell.Options.UseTextOptions = True
    Me.NtsGridColumn3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.NtsGridColumn3.Caption = "Valore A"
    Me.NtsGridColumn3.Enabled = True
    Me.NtsGridColumn3.FieldName = "xx_valorea"
    Me.NtsGridColumn3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.NtsGridColumn3.Name = "NtsGridColumn3"
    Me.NtsGridColumn3.NTSRepositoryComboBox = Nothing
    Me.NtsGridColumn3.NTSRepositoryItemCheck = Nothing
    Me.NtsGridColumn3.NTSRepositoryItemMemo = Nothing
    Me.NtsGridColumn3.NTSRepositoryItemText = Nothing
    Me.NtsGridColumn3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.NtsGridColumn3.OptionsFilter.AllowFilter = False
    Me.NtsGridColumn3.Visible = True
    Me.NtsGridColumn3.VisibleIndex = 2
    Me.NtsGridColumn3.Width = 300
    '
    'pnTipoStampa
    '
    Me.pnTipoStampa.AllowDrop = True
    Me.pnTipoStampa.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTipoStampa.Appearance.Options.UseBackColor = True
    Me.pnTipoStampa.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTipoStampa.Controls.Add(Me.opTipoStampa1)
    Me.pnTipoStampa.Controls.Add(Me.opTipoStampa0)
    Me.pnTipoStampa.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTipoStampa.Location = New System.Drawing.Point(138, 36)
    Me.pnTipoStampa.Name = "pnTipoStampa"
    Me.pnTipoStampa.NTSActiveTrasparency = True
    Me.pnTipoStampa.Size = New System.Drawing.Size(167, 28)
    Me.pnTipoStampa.TabIndex = 93
    Me.pnTipoStampa.Text = "NtsPanel1"
    '
    'lbTipoStampa
    '
    Me.lbTipoStampa.AutoSize = True
    Me.lbTipoStampa.BackColor = System.Drawing.Color.Transparent
    Me.lbTipoStampa.Location = New System.Drawing.Point(12, 41)
    Me.lbTipoStampa.Name = "lbTipoStampa"
    Me.lbTipoStampa.NTSDbField = ""
    Me.lbTipoStampa.Size = New System.Drawing.Size(80, 13)
    Me.lbTipoStampa.TabIndex = 77
    Me.lbTipoStampa.Text = "Tipo di stampa:"
    Me.lbTipoStampa.Tooltip = ""
    Me.lbTipoStampa.UseMnemonic = False
    '
    'fmOpzioni
    '
    Me.fmOpzioni.AllowDrop = True
    Me.fmOpzioni.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmOpzioni.Appearance.Options.UseBackColor = True
    Me.fmOpzioni.Controls.Add(Me.ckEvasi)
    Me.fmOpzioni.Controls.Add(Me.ckSalto)
    Me.fmOpzioni.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmOpzioni.Location = New System.Drawing.Point(12, 292)
    Me.fmOpzioni.Name = "fmOpzioni"
    Me.fmOpzioni.ShowCaption = False
    Me.fmOpzioni.Size = New System.Drawing.Size(232, 49)
    Me.fmOpzioni.TabIndex = 94
    '
    'FRMORSCHO_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(517, 394)
    Me.Controls.Add(Me.fmOpzioni)
    Me.Controls.Add(Me.pnTipoStampa)
    Me.Controls.Add(Me.lbTipoStampa)
    Me.Controls.Add(Me.edSerie)
    Me.Controls.Add(Me.cbOrdin)
    Me.Controls.Add(Me.lbOrdinamento)
    Me.Controls.Add(Me.ckTipork)
    Me.Controls.Add(Me.ckSerie)
    Me.Controls.Add(Me.cbTipork)
    Me.Controls.Add(Me.lbDamagaz)
    Me.Controls.Add(Me.lbXx_Codcfam)
    Me.Controls.Add(Me.edDadatcons)
    Me.Controls.Add(Me.lbCodcfam)
    Me.Controls.Add(Me.edCodcfam)
    Me.Controls.Add(Me.lbDacodart)
    Me.Controls.Add(Me.edSottogr)
    Me.Controls.Add(Me.lbStatus)
    Me.Controls.Add(Me.edGruppo)
    Me.Controls.Add(Me.lbGruppo)
    Me.Controls.Add(Me.lbDadatcons)
    Me.Controls.Add(Me.edAconto)
    Me.Controls.Add(Me.edAmagaz)
    Me.Controls.Add(Me.edAdatcons)
    Me.Controls.Add(Me.edAcodart)
    Me.Controls.Add(Me.edDacodart)
    Me.Controls.Add(Me.lbDaagente)
    Me.Controls.Add(Me.edAdatord)
    Me.Controls.Add(Me.edDaconto)
    Me.Controls.Add(Me.edDaagente)
    Me.Controls.Add(Me.edDadatord)
    Me.Controls.Add(Me.lbDaconto)
    Me.Controls.Add(Me.edDamagaz)
    Me.Controls.Add(Me.lbDadatord)
    Me.Controls.Add(Me.edAagente)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRMORSCHO_F"
    Me.Text = "STAMPA / VISUALIZZAZIONE SCHEDE ORDINI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckEvasi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSalto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDamagaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAmagaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDaagente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAagente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDadatord.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdatord.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdatcons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDadatcons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDaconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDacodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAcodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edGruppo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSottogr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipork.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTipork.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodcfam.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbOrdin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opTipoStampa1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opTipoStampa0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTipoStampa, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTipoStampa.ResumeLayout(False)
    Me.pnTipoStampa.PerformLayout()
    CType(Me.fmOpzioni, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmOpzioni.ResumeLayout(False)
    Me.fmOpzioni.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

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
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNORSCHO", "BEORSCHO", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128496233436616000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleScho = CType(oTmp, CLEORSCHO)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNORSCHO", strRemoteServer, strRemotePort)
    AddHandler oCleScho.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleScho.Init(oApp, oScript, oMenu.oCleComm, "MOVMAG", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

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
        tlbStampaGriglia.GlyphPath = (oApp.ChildImageDir & "\prngrid.gif")
        tlbStampaWord.GlyphPath = (oApp.ChildImageDir & "\word.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      opTipoStampa1.NTSSetParam(oMenu, oApp.Tr(Me, 128595426229218750, "Completo"), "S")
      opTipoStampa0.NTSSetParam(oMenu, oApp.Tr(Me, 128595426229375000, "Ridotto"), "S")
      cbOrdin.NTSSetParam(oApp.Tr(Me, 128756214849590060, "Ordinamento"))
      edCodcfam.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426231250000, "Linea/famiglia articolo"), tabcfam, True)
      ckTipork.NTSSetParam(oMenu, oApp.Tr(Me, 128595426231406250, "Tipo &Documento"), "S", "N")
      cbTipork.NTSSetParam(oApp.Tr(Me, 128595426231562500, "Tipo &Documento"))
      edSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128595426232187500, "Serie ordini"), CLN__STD.SerieMaxLen, True)
      edSottogr.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426232500000, "Sottogruppo"), tabsgme)
      edGruppo.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426232656250, "Gruppo"), tabgmer)
      edAcodart.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426234218750, "a codice articolo"), tabartico, False)
      edDacodart.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426234687500, "Da codice articolo"), tabartico, True)
      edAconto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426235000000, "a conto"), tabanagrac)
      edDaconto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426235156250, "Da conto"), tabanagrac)
      edAdatcons.NTSSetParam(oMenu, oApp.Tr(Me, 128595426235625000, "alla data di consegna"), False)
      edDadatcons.NTSSetParam(oMenu, oApp.Tr(Me, 128595426235937500, "Dalla data di consegna"), False)
      edAdatord.NTSSetParam(oMenu, oApp.Tr(Me, 128595426236250000, "alla data ordine"), False)
      edDadatord.NTSSetParam(oMenu, oApp.Tr(Me, 128595426236562500, "Dalla data ordine"), False)
      edAagente.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426236718750, "ad agente"), tabcage)
      edDaagente.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426237031250, "Da agente"), tabcage)
      edAmagaz.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426237500000, "a magazzino"), tabmaga)
      edDamagaz.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128595426237656250, "Da Magazzino"), tabmaga)
      ckSalto.NTSSetParam(oMenu, oApp.Tr(Me, 128595426237968750, "&Salto pagina per ogni articolo"), "S", "N")
      ckEvasi.NTSSetParam(oMenu, oApp.Tr(Me, 128595426238125000, "C&onsidera solo le righe non evase"), "S", "N")
      ckSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128860056110311263, "Serie"), "S", "N")

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
    Try
      Dim dttTipork As New DataTable()
      Dim dttOrdin As New DataTable()

      dttTipork.Columns.Add("cod", GetType(String))
      dttTipork.Columns.Add("val", GetType(String))
      dttTipork.Rows.Add(New Object() {"O", "Ordine fornitore"})
      dttTipork.Rows.Add(New Object() {"Q", "Preventivo"})
      dttTipork.Rows.Add(New Object() {"R", "Impegno cliente"})
      dttTipork.AcceptChanges()
      cbTipork.DataSource = dttTipork
      cbTipork.ValueMember = "cod"
      cbTipork.DisplayMember = "val"

      dttOrdin.Columns.Add("cod", GetType(Integer))
      dttOrdin.Columns.Add("val", GetType(String))
      dttOrdin.Rows.Add(New Object() {1, "Per Articolo"})
      dttOrdin.Rows.Add(New Object() {2, "Per Conto"})
      dttOrdin.AcceptChanges()
      cbOrdin.DataSource = dttOrdin
      cbOrdin.ValueMember = "cod"
      cbOrdin.DisplayMember = "val"

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi di FORM"
  Public Overridable Sub FRMORSCHO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()
      strProntoMess = oApp.Tr(Me, 128756215105211788, "Pronto.")

      oCleScho.bModTCO = CBool(oApp.ActKey.ModuliExtAzienda And bsModExtTCO)
      oCleScho.bModAS = CBool(oApp.ActKey.ModuliAzienda And bsModAS)
      If CBool(oApp.ActKey.ModuliExtAzienda And bsModExtCRM) Or _
         CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And CLN__STD.bsModSupWCR) Then
        oCleScho.bModuloCRM = True
      Else
        oCleScho.bModuloCRM = False
      End If

      oCleScho.bModuloCRM = False

      CaricaCombo()

      If Not oCleScho.LeggiDatiDitta(DittaCorrente) Then
        Me.Close()
        Return
      End If

      If Not oCallParams Is Nothing Then
        If oCallParams.strParam <> "" Then
          oCleScho.bSchoDagest = True
          oCleScho.lSchoDaconto = NTSCInt(Mid(oCallParams.strParam, 1, 9))
          oCleScho.lSchoAconto = NTSCInt(Mid(oCallParams.strParam, 11, 9))
          oCleScho.strSchoDacodart = Trim(Mid(oCallParams.strParam, 21, CLN__STD.CodartMaxLen))
          oCleScho.strSchoAcodart = Trim(Mid(oCallParams.strParam, CLN__STD.CodartMaxLen + 22, CLN__STD.CodartMaxLen))
          oCleScho.strSchoTipork = Mid(oCallParams.strParam, (CLN__STD.CodartMaxLen * 2) + 23, 1)
          oCleScho.bSchoFiltri = CBool(Mid(oCallParams.strParam, (CLN__STD.CodartMaxLen * 2) + 25, 1))
          oCleScho.strSchoDatordini = Trim(Mid(oCallParams.strParam, (CLN__STD.CodartMaxLen * 2) + 27, 10))
          oCleScho.strSchoDatordfin = Trim(Mid(oCallParams.strParam, (CLN__STD.CodartMaxLen * 2) + 38, 10))
        Else
          oCleScho.bSchoDagest = False
        End If
      End If

      '------------------------------------------------
      'CRM: se l'operatore non è stato codificato e non ha un ruolo non può operare
      If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And CLN__STD.bsModExtCRM) Or _
         CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And CLN__STD.bsModSupWCR) Then oCleScho.bModuloCRM = True
      If oCleScho.bModuloCRM Then
        oCleScho.bIsCRMUser = oMenu.IsCrmUser(DittaCorrente, oCleScho.bAmm, oCleScho.strAccvis, oCleScho.strAccmod, oCleScho.strRegvis, oCleScho.strRegmod)
        If oCleScho.bIsCRMUser Then
          oCleScho.lCodorgaOperat = oMenu.RitornaCodorgaDaOpnome(DittaCorrente, oCleScho.nCodcageoperat)
          If oCleScho.lCodorgaOperat = 0 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 127791222142500000, "Attenzione!" & vbCrLf & "L'operatore '|" & oApp.User.Nome & _
                 "|' (CRM) non è associato all'organizzazione della ditta corrente '|" & DittaCorrente & "|'." & vbCrLf & _
                 "Impossibile continuare."))
            Me.Close()
            Return
          End If
        End If
      End If

      edDamagaz.Text = "0"
      edAmagaz.Text = "9999"
      edDaagente.Text = "0"
      edAagente.Text = "9999"
      edDadatord.Text = IntSetDate("01/01/1900")
      edAdatord.Text = IntSetDate("31/12/2099")
      If oCleScho.bSchoDagest = False Then
        edDaconto.Text = "0"
        edAconto.Text = "999999999"
        edDacodart.Text = "".PadLeft(CLN__STD.CodartMaxLen)
        edAcodart.Text = "".PadLeft(CLN__STD.CodartMaxLen, "z"c)
        cbTipork.Enabled = False
      Else
        edDaconto.Text = NTSCStr(oCleScho.lSchoDaconto)
        edAconto.Text = NTSCStr(oCleScho.lSchoAconto)
        edDacodart.Text = oCleScho.strSchoDacodart
        edAcodart.Text = oCleScho.strSchoAcodart
        If oCleScho.strSchoTipork = "§" Then
          cbTipork.SelectedValue = "§"
          cbTipork.Enabled = False
        Else
          ckTipork.Checked = True
          cbTipork.SelectedValue = oCleScho.strSchoTipork
        End If
        If oCleScho.strSchoDatordini <> "" Then edDadatord.Text = oCleScho.strSchoDatordini
        If oCleScho.strSchoDatordfin <> "" Then edAdatord.Text = oCleScho.strSchoDatordfin
      End If
      edDadatcons.Text = IntSetDate("01/01/1900")
      edAdatcons.Text = IntSetDate("31/12/2099")
      edGruppo.Text = "0"
      edSottogr.Text = "0"
      edCodcfam.Text = ""
      lbXx_Codcfam.Text = ""
      tlbStampaWord.Enabled = False
      tlbStampaWordRaggr.Enabled = False

      '-----------------------------------------------------------------------------------------
      '--- Legge opzioni
      '-----------------------------------------------------------------------------------------
      oCleScho.nMagazInternoTransito = NTSCInt(oMenu.GetSettingBus("BSDBEMRP", "OPZIONI", ".", "MagazInternoTransito", "1", " ", "1"))
      oCleScho.nAltezzaGif = NTSCInt(oMenu.GetSettingBus("BSORSCHO", "OPZIONI", ".", "AltezzaGif", "20", " ", "20"))

      If (oCleScho.bSchoDagest = True) And (oCleScho.bSchoFiltri = False) Then
        Me.Visible = False
        ReportSuGriglia()
      End If

      edSerie.Enabled = False
      ckSerie.Checked = False
      lbStatus.Text = strProntoMess

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      GctlApplicaDefaultValue()
      '---------------------------------------------------
      'Prende la struttura della tabella
      oCleScho.GetTableStructMovIfil(dttDefault)

      dttDefault.Columns.Add("xx_descr")
      dttDefault.Columns.Add("xx_info")
      dttDefault.Columns.Add("xx_tipo")

      ComponiDatatable(dttDefault, Me)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMORSCHO_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    Try
      oMenu.ResetTblInstId("TTSCHO", False, oCleScho.lIITTScho)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      NTSCallStandardZoom()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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

  Public Overridable Sub tlbStampaGriglia_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaGriglia.ItemClick
    Try
      ReportSuGriglia()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
      lbStatus.Text = oApp.Tr(Me, 128756214995369444, "Pronto.")
    End Try
  End Sub

  Public Overridable Sub tlbStampaWordRaggr_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaWordRaggr.ItemClick
    Dim oPar As CLE__CLDP = Nothing
    Dim strQueryWord As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim strSQLRipartisciTaglieRagg As String = ""
    Dim strSQLTaglieRagg As String = ""
    Try
      Me.ValidaLastControl()
      '-----------------------
      'faccio comporre la query al dl

      oCleScho.bStampaWordRaggruppata = True

      strQueryWord = oCleScho.GetQueryStampaWord(ckEvasi.Checked, edDamagaz.Text, _
                                  edAmagaz.Text, edDaagente.Text, edAagente.Text, edDadatord.Text, _
                                  edAdatord.Text, edDadatcons.Text, edAdatcons.Text, edDaconto.Text, _
                                  edAconto.Text, edDacodart.Text, edAcodart.Text, "0", "".PadLeft(9, "9"c), _
                                  "0", "".PadLeft(4, "9"c), edGruppo.Text, edSottogr.Text, edSerie.Text, _
                                  "E", ckTipork.Checked, edCodcfam.Text, False, "0", "0", cbTipork.SelectedValue, "")
      If strQueryWord = "" Then Return

      CaricaDataSetWord(dsTmp)

      '-----------------------
      'chiamo la stampa su word passandogli la query
      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strPar1 = "BNORSCHO"
      oPar.strPar2 = strQueryWord
      oPar.bPar2 = oCleScho.bStampaWordRaggruppata
      oPar.ctlPar1 = dsTmp
      oCleScho.RipartisciTaglieQuantitaRagg(ckEvasi.Checked, edDamagaz.Text, edAmagaz.Text, _
                                            edDaagente.Text, edAagente.Text, edDadatord.Text, edAdatord.Text, _
                                            edDadatcons.Text, edAdatcons.Text, edDaconto.Text, edAconto.Text, _
                                            edDacodart.Text, edAcodart.Text, "0", "".PadLeft(9, "9"c), _
                                            "0", "".PadLeft(4, "9"c), edGruppo.Text, edSottogr.Text, _
                                            edSerie.Text, "E", ckTipork.Checked, cbTipork.SelectedValue, _
                                            edCodcfam.Text, False, "0", "0", "")
      oCleScho.TaglieQuantitaRagg(ckEvasi.Checked, edDamagaz.Text, edAmagaz.Text, _
                                      edDaagente.Text, edAagente.Text, edDadatord.Text, edAdatord.Text, _
                                      edDadatcons.Text, edAdatcons.Text, edDaconto.Text, edAconto.Text, _
                                      edDacodart.Text, edAcodart.Text, "0", "".PadLeft(9, "9"c), _
                                      "0", "".PadLeft(4, "9"c), edGruppo.Text, edSottogr.Text, _
                                      edSerie.Text, "E", ckTipork.Checked, cbTipork.SelectedValue, _
                                      edCodcfam.Text, False, "0", "0", "")
      oPar.strPar3 = strSQLRipartisciTaglieRagg
      oPar.strPar4 = strSQLTaglieRagg
      oMenu.RunChild("NTSInformatica", "FRM__STW3", "", DittaCorrente, "", "BN__STWO", oPar, "", True, True)

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
      '-----------------------
      'faccio comporre la query al dl

      oCleScho.bStampaWordRaggruppata = False

      strQueryWord = oCleScho.GetQueryStampaWord(ckEvasi.Checked, edDamagaz.Text, _
                                  edAmagaz.Text, edDaagente.Text, edAagente.Text, edDadatord.Text, _
                                  edAdatord.Text, edDadatcons.Text, edAdatcons.Text, edDaconto.Text, _
                                  edAconto.Text, edDacodart.Text, edAcodart.Text, "0", "".PadLeft(9, "9"c), _
                                  "0", "".PadLeft(4, "9"c), edGruppo.Text, edSottogr.Text, IIf(ckSerie.Checked = True, edSerie.Text, "").ToString, _
                                  "E", ckTipork.Checked, edCodcfam.Text, False, "0", "0", cbTipork.SelectedValue, "")
      If strQueryWord = "" Then Return

      CaricaDataSetWord(dsTmp)

      '-----------------------
      'chiamo la stampa su word passandogli la query
      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strPar1 = "BNORSCHO"
      oPar.strPar2 = strQueryWord
      oPar.strPar3 = cbTipork.SelectedValue
      oPar.bPar2 = oCleScho.bStampaWordRaggruppata
      oPar.ctlPar1 = dsTmp
      oPar.strPar3 = ""
      oPar.strPar4 = ""
      oMenu.RunChild("NTSInformatica", "FRM__STW3", "", DittaCorrente, "", "BN__STWO", oPar, "", True, True)

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
#End Region

  Public Overridable Function Elabora() As Boolean
    Dim nValoriz As Integer = 0
    Dim nMagaz As Integer = 0

    Try
      Me.ValidaLastControl()

      If Not CheckIntervalli() Then Return False

      Me.Cursor = Cursors.WaitCursor

      '-------------------------
      'eseguo l'elaborazione
      oCleScho.Elabora(edDacodart.Text, edAcodart.Text, edDamagaz.Text, edAmagaz.Text, _
                      edDaagente.Text, edAagente.Text, edDadatord.Text, edAdatord.Text, _
                      edDadatcons.Text, edAdatcons.Text, edDaconto.Text, edAconto.Text, _
                      "0", "".PadLeft(9, "9"c), "0", "".PadLeft(4, "9"c), _
                      ckEvasi.Checked, edGruppo.Text, edSottogr.Text, edSerie.Text, _
                      cbTipork.Enabled, edSerie.Text, "E", edCodcfam.Text, _
                      False, "0", "0", False, False, False, False, False, False, False, False, False, "", "", "", "", "", "")

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
    Dim sectionCode As Integer
    Dim nVisible As Integer
    Dim nNewPageBefore As Integer
    Dim nNewPageAfter As Integer
    Dim nKeepTogether As Integer
    Dim nSuppressBlankSection As Integer
    Dim nResetPageNAfter As Integer
    Dim nPrintAtBottomOfPage As Integer
    Dim lBackgroudColor As Integer
    Dim nUnderlaySection As Integer
    Dim nShowArea As Integer
    Dim nFreeFormPlacement As Integer
    Try

      '--------------------------------------------------
      Select Case NTSCInt(cbOrdin.SelectedValue)
        Case 1
          If opTipoStampa0.Checked = True Then
            strNomRpt = "BSORSCH6.RPT" : strKey = "Reports6"
          Else
            strNomRpt = "BSORSCH1.RPT" : strKey = "Reports1"
          End If
        Case 2
          If opTipoStampa0.Checked = True Then
            strNomRpt = "BSORSCH7.RPT" : strKey = "Reports7"
          Else
            strNomRpt = "BSORSCH2.RPT" : strKey = "Reports2"
          End If
        Case 3 : strNomRpt = "BSORSCH4.RPT" : strKey = "Reports4"
        Case 4
          If Not Elabora() Then Exit Sub
          If Not oCleScho.CheckTmpTable() Then Exit Sub
          strNomRpt = "BSORSCH3.RPT" : strKey = "Reports3"
        Case 5 : strNomRpt = "BSORSCH5.RPT" : strKey = "Reports5"
      End Select

      Me.Cursor = Cursors.WaitCursor

      strCrpe = ComponiFormula()

      '--------------------------------------------------
      'preparo il motore di stampa
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSORSCHO", strKey, " ", 0, nDestin, strNomRpt, False, "Stampa/Visualizzazione schede ordini", False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce già il multireport)
      For i = 1 To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DADATCONS", "'" & edDadatcons.Text & "'")
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "ADATCONS", "'" & edAdatcons.Text & "'")
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DADATORD", "'" & edDadatord.Text & "'")
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "ADATORD", "'" & edDadatord.Text & "'")

        If i = 1 Then
          Select Case NTSCInt(cbOrdin.SelectedValue)
            Case 1, 3, 4
              If (ckSalto.Checked = True) Then
                sectionCode = oMenu.PE_GROUPHEADER

                nRis = oMenu.PEGetSectionFormat(NTSCInt(CType(nPjob, Array).GetValue(0, i)), sectionCode, nVisible, nNewPageBefore, nNewPageAfter, nKeepTogether, nSuppressBlankSection, nResetPageNAfter, nPrintAtBottomOfPage, lBackgroudColor, nUnderlaySection, nShowArea, nFreeFormPlacement)
                nNewPageBefore = 1
                nRis = oMenu.PESetSectionFormat(NTSCInt(CType(nPjob, Array).GetValue(0, i)), sectionCode, nVisible, nNewPageBefore, nNewPageAfter, nKeepTogether, nSuppressBlankSection, nResetPageNAfter, nPrintAtBottomOfPage, lBackgroudColor, nUnderlaySection, nShowArea, nFreeFormPlacement)
              End If
          End Select
        End If

        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

      lbStatus.Text = oApp.Tr(Me, 128756215030525244, "Pronto.")

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
      lbStatus.Text = oApp.Tr(Me, 128756215066618532, "Pronto.")
    End Try
  End Sub

  Public Overridable Function CheckIntervalli() As Boolean
    Try
      If NTSCInt(edDamagaz.Text) > NTSCInt(edAmagaz.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128595499815189918, "Il magazzino di partenza non può essere superiore a quello di arrivo."))
        edDamagaz.Text = "0"
        edAmagaz.Text = "9999"
        Return False
      End If
      If NTSCInt(edDaagente.Text) > NTSCInt(edAagente.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128595499785346168, "L'agente di partenza non può essere superiore a quello di arrivo."))
        edDaagente.Text = "0"
        edAagente.Text = "9999"
        Return False
      End If
      If NTSCDate(edDadatord.Text) > NTSCDate(edAdatord.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128595499747689918, "La data ordine di partenza non può essere superiore a quella di arrivo."))
        edDadatord.Text = IntSetDate("01/01/1900")
        edAdatord.Text = IntSetDate("31/12/2099")
        Return False
      End If

      If NTSCDate(edDadatcons.Text) > NTSCDate(edAdatcons.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128595499730189918, "La data consegna di partenza non può essere superiore a quella di arrivo."))
        edDadatcons.Text = IntSetDate("01/01/1900")
        edAdatcons.Text = IntSetDate("31/12/2099")
        Return False
      End If
      If NTSCInt(edDaconto.Text) > NTSCInt(edAconto.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128595499709721168, "Il conto di partenza non può essere superiore a quello di arrivo."))
        edDaconto.Text = "0"
        edAconto.Text = "999999999"
        Return False
      End If
      If UCase(edDacodart.Text) > UCase(edAcodart.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128595499598002418, "L'articolo di partenza non può essere superiore a quello di arrivo."))
        edDacodart.Text = "".PadLeft(CLN__STD.CodartMaxLen)
        edAcodart.Text = "".PadLeft(CLN__STD.CodartMaxLen, "z"c)
        Return False
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function ComponiFormula() As String
    Dim strC As String
    Dim strDacodart As String
    Dim strAcodart As String
    Dim strTipork As String
    Dim strAggwhereCRM As String
    Try
      '--------------------------------------------------------------------------------------------------------------
      If ckSerie.Checked = False Then edSerie.Text = ""
      '--------------------------------------------------------------------------------------------------------------
      ComponiFormula = ""
      '-----------------------------------------------------------------------------------------
      '--- Se crm, allora mette anche solo i clienti che sono nel potere di visibilità dell'utente...
      '-----------------------------------------------------------------------------------------
      '--- Se il tipo di stampa è 'Per Articolo Pianificazione', non serve filtrare
      '--- gli eventuali dati CRM dato che lo fa già riempiendo la tabella temporanea TTSCHO
      '-----------------------------------------------------------------------------------------
      strAggwhereCRM = ""
      If NTSCInt(cbOrdin.SelectedValue) <> 4 Then
        If (oCleScho.bModuloCRM = True) And (oCleScho.bIsCRMUser = True) Then
          strAggwhereCRM = " And ("
          If oCleScho.strAccvis <> "T" Then
            strAggwhereCRM = strAggwhereCRM & "({anagra.an_tipo} = 'C' And {leads.le_coddest} = 0"
            Select Case oCleScho.strAccvis
              Case "P" : strAggwhereCRM = strAggwhereCRM & " And {leads.le_opinc} = " & oCleScho.lCodorgaOperat
              Case "C" : strAggwhereCRM = strAggwhereCRM & " And {leads.le_opinc} In [" & oCleScho.strRegvis & "]"
            End Select
            strAggwhereCRM = strAggwhereCRM & ")"
          Else '--- Tutti i clienti
            strAggwhereCRM = strAggwhereCRM & "  {anagra.an_tipo} = 'C'"
          End If
          If oCleScho.bAmm = False Then
            strAggwhereCRM = strAggwhereCRM & " And {anagra.an_tipo} <> 'F'"
          Else
            strAggwhereCRM = strAggwhereCRM & " Or {anagra.an_tipo} <> 'C'"
          End If
          strAggwhereCRM = strAggwhereCRM & ")"
        End If
      End If
      '-----------------------------------------------------------------------------------------
      ComponiFormula = ""
      If NTSCInt(cbOrdin.SelectedValue) = 4 Then 'Articolo Pianificazione
        strC = "{TTSCHO.codditt} = '" & DittaCorrente & "'" & _
          " And {TTSCHO.instid} = " & oCleScho.lIITTScho
      Else
        If Not edDacodart.Text = "".PadLeft(CLN__STD.CodartMaxLen) Then strDacodart = UCase(edDacodart.Text) Else strDacodart = edDacodart.Text
        If Not edAcodart.Text = "".PadLeft(CLN__STD.CodartMaxLen, "z"c) Then strAcodart = UCase(edAcodart.Text) Else strAcodart = edAcodart.Text
        strC = "{keyord.codditt} = '" & DittaCorrente & "'" & _
          " and {keyord.ko_magaz} in " & edDamagaz.Text & " to " & edAmagaz.Text & _
          " and {testord.td_codagen} in " & edDaagente.Text & " to " & edAagente.Text & _
          " and {testord.td_datord} in " & ConvDataRpt(edDadatord.Text) & " to " & ConvDataRpt(edAdatord.Text) & _
          " and {keyord.ko_datcons} in " & ConvDataRpt(edDadatcons.Text) & " to " & ConvDataRpt(edAdatcons.Text) & _
          " and {keyord.ko_conto} in " & edDaconto.Text & " to " & edAconto.Text & _
          " and {keyord.ko_codart} in '" & strDacodart & "' to '" & strAcodart & "'"
        If ckEvasi.Checked = True Then strC = strC & " and {movord.mo_flevas} = 'C'"
        If NTSCInt(edGruppo.Text) <> 0 Then strC = strC & " and {artico.ar_gruppo} = " & edGruppo.Text
        If NTSCInt(edSottogr.Text) <> 0 Then strC = strC & " and {artico.ar_sotgru} = " & edSottogr.Text
        If edSerie.Text <> "" Then strC = strC & " and {keyord.ko_serie} = '" & edSerie.Text & "'"
        If cbTipork.Enabled Then
          strTipork = cbTipork.SelectedValue
          strC = strC & " and {keyord.ko_tipork} = '" & strTipork & "'"
        End If
      End If
      Select Case cbOrdin.SelectedValue
        Case "3" 'Articolo/magazzino/commessa
          strC = strC & " And {artico.ar_gescomm} = 'S'"
        Case "5" 'Commessa/articolo
          strC = strC & " And {artico.ar_gescomm} = 'S'"
      End Select
      If Trim(edCodcfam.Text) <> "" Then
        strC = strC & " And {artico.ar_famprod} = '" & edCodcfam.Text & "'"
      End If
      '-----------------------------------------------------------------------------------------
      '--- Aggiunge eventuali condizioni CRM
      '-----------------------------------------------------------------------------------------
      strC = strC & strAggwhereCRM
      '-----------------------------------------------------------------------------------------
      ComponiFormula = strC

    Catch ex As Exception
      ComponiFormula = ""
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub ReportSuGriglia()
    Dim oParam As New CLE__CLDP
    Dim frmGrso As FRMORGRSO = Nothing
    Dim frmGrs1 As FRMORGRS1 = Nothing
    Dim frmGrs2 As FRMORGRS2 = Nothing
    Try
      Me.ValidaLastControl()
      If Not CheckIntervalli() Then Exit Sub
      oCleScho.strAltriFiltri = ""

      lbStatus.Text = oApp.Tr(Me, 130421108422604400, "Selezione dati in corso...")
      oCleScho.nSchoDamagaz = NTSCInt(edDamagaz.Text)
      oCleScho.nSchoAmagaz = NTSCInt(edAmagaz.Text)
      oCleScho.nSchoDaagente = NTSCInt(edDaagente.Text)
      oCleScho.nSchoAagente = NTSCInt(edAagente.Text)
      oCleScho.strSchoDadatord = edDadatord.Text
      oCleScho.strSchoAdatord = edAdatord.Text
      oCleScho.strSchoDadatcons = edDadatcons.Text
      oCleScho.strSchoAdatcons = edAdatcons.Text
      oCleScho.lSchoDaconto = NTSCInt(edDaconto.Text)
      oCleScho.lSchoAconto = NTSCInt(edAconto.Text)
      If Not edDacodart.Text = "".PadLeft(CLN__STD.CodartMaxLen) Then
        oCleScho.strSchoDacodart = UCase$(edDacodart.Text)
      Else
        oCleScho.strSchoDacodart = edDacodart.Text
      End If
      If Not edAcodart.Text = "".PadLeft(CLN__STD.CodartMaxLen, "z"c) Then
        oCleScho.strSchoAcodart = UCase$(edAcodart.Text)
      Else
        oCleScho.strSchoAcodart = edAcodart.Text
      End If
      oCleScho.lSchoCommecaini = 0
      oCleScho.lSchoCommecafin = 999999999
      oCleScho.nSchoFaseini = 0
      oCleScho.nSchoFasefin = 9999
      oCleScho.nSchoGruppo = NTSCInt(edGruppo.Text)
      oCleScho.nSchoSottogr = NTSCInt(edSottogr.Text)
      oCleScho.strSchoSerie = Trim(edSerie.Text)
      If ckEvasi.Checked = True Then oCleScho.bSchoEvasi = True Else oCleScho.bSchoEvasi = False
      If cbTipork.Enabled Then
        oCleScho.strSchoTipork = cbTipork.SelectedValue
      Else
        oCleScho.strSchoTipork = ""
      End If
      oCleScho.strSchoRilasciato = "E"
      Select Case NTSCInt(cbOrdin.SelectedValue)
        Case 1 : oCleScho.strSchoOrdin = "A"
        Case 2 : oCleScho.strSchoOrdin = "C"
      End Select
      lbStatus.Text = strProntoMess
      oCleScho.strGrsoCodcfam = Trim(edCodcfam.Text)
      oCleScho.bGrsoModTCO = False
      oCleScho.nGrsoAnnotco = 0
      oCleScho.nGrsoCodstag = 0
      frmGrso = CType(NTSNewFormModal("FRMORGRSO"), FRMORGRSO)
      frmGrso.Init(oMenu, oParam, DittaCorrente)
      frmGrso.InitEntity(oCleScho)
      frmGrso.ShowDialog()
      If frmGrso.bClose = True Then oApp.MsgBoxErr(oApp.Tr(Me, 128756212996332532, "Non esistono dati con queste caratteristiche."))
      If (oCleScho.bSchoDagest = True) And (oCleScho.bSchoFiltri = False) Then
        Me.Visible = True
        Me.Close()
        Exit Sub
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmGrso Is Nothing Then frmGrso.Dispose()
      frmGrso = Nothing

      If Not frmGrs1 Is Nothing Then frmGrs1.Dispose()
      frmGrs1 = Nothing

      If Not frmGrs2 Is Nothing Then frmGrs2.Dispose()
      frmGrs2 = Nothing
    End Try
  End Sub

  Public Overridable Sub edGruppo_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edGruppo.Validated
    Dim strTmp As String = ""
    Try
      If oCleScho Is Nothing Then Return

      If Not oCleScho.edGruppo_Validated(NTSCInt(edGruppo.Text), strTmp) Then
        edGruppo.Text = NTSCStr(edGruppo.OldEditValue)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edSottogr_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSottogr.Validated
    Dim strTmp As String = ""
    Try
      If oCleScho Is Nothing Then Return

      If Not oCleScho.edSottogr_Validated(NTSCInt(edSottogr.Text), strTmp) Then
        edSottogr.Text = NTSCStr(edSottogr.OldEditValue)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edCodcfam_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodcfam.Validated
    Dim strTmp As String = ""
    Try
      If oCleScho Is Nothing Then Return

      If Not oCleScho.edCodcfam_Validated(NTSCStr(edCodcfam.Text), strTmp) Then
        edCodcfam.Text = NTSCStr(edCodcfam.OldEditValue)
      Else
        lbXx_Codcfam.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edDacodart_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDacodart.Validated
    Try
      If edDacodart.Text <> "".PadLeft(CLN__STD.CodartMaxLen) And edDacodart.Text <> "" Then
        edDacodart.Text = UCase(edDacodart.Text)
        edAcodart.Text = edDacodart.Text
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edDaconto_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDaconto.Validated
    Try
      If NTSCInt(edDaconto.Text) <> 0 Then edAconto.Text = edDaconto.Text

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edAcodart_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAcodart.Validated
    Try
      If Not edAcodart.Text = "".PadLeft(CLN__STD.CodartMaxLen, "z"c) Then edAcodart.Text = UCase(edAcodart.Text)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cbOrdin_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbOrdin.SelectedIndexChanged
    Try
      Select Case NTSCInt(cbOrdin.SelectedValue)
        Case 1
          If ckSalto.Visible = False Then
            GctlSetVisEnab(ckSalto, True)
          End If
          If ckTipork.Enabled = False Then GctlSetVisEnab(ckTipork, False)
          If ckEvasi.Enabled = False Then
            ckEvasi.Checked = False
            GctlSetVisEnab(ckEvasi, False)
          End If
          tlbStampaWord.Enabled = False
          tlbStampaWordRaggr.Enabled = False
          If pnTipoStampa.Enabled = False Then
            GctlSetVisEnab(pnTipoStampa, False)
            GctlSetVisEnab(opTipoStampa0, False)
            GctlSetVisEnab(opTipoStampa1, False)
          End If
        Case 2
          ckSalto.Checked = False
          ckSalto.TabStop = False
          ckSalto.Visible = False
          If ckTipork.Enabled = False Then GctlSetVisEnab(ckTipork, False)
          If ckEvasi.Enabled = False Then
            ckEvasi.Checked = False
            GctlSetVisEnab(ckEvasi, False)
          End If
          GctlSetVisEnab(tlbStampaWord, False)
          GctlSetVisEnab(tlbStampaWordRaggr, False)
          GctlSetVisEnab(ckTipork, False)
          If pnTipoStampa.Enabled = False Then
            GctlSetVisEnab(pnTipoStampa, False)
            GctlSetVisEnab(opTipoStampa0, False)
            GctlSetVisEnab(opTipoStampa1, False)
          End If
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTipork_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckTipork.CheckedChanged
    Try
      If ckTipork.Checked = True Then
        GctlSetVisEnab(cbTipork, False)
        cbTipork.SelectedIndex = 0
        cbTipork.Focus()
      Else
        cbTipork.SelectedValue = "$"
        cbTipork.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub CaricaDataSetWord(ByRef ds As DataSet)
    Try
      ds = New DataSet()
      ds.Tables.Add("WORD")
      ds.Tables("WORD").Columns.Add(New DataColumn("xx_dadatcons", GetType(Date)))
      ds.Tables("WORD").Columns.Add(New DataColumn("xx_adatcons", GetType(Date)))
      ds.Tables("WORD").Columns.Add(New DataColumn("xx_dadatord", GetType(Date)))
      ds.Tables("WORD").Columns.Add(New DataColumn("xx_adatord", GetType(Date)))
      ds.AcceptChanges()

      ds.Tables("WORD").Rows.Add(ds.Tables("WORD").NewRow)

      ds.Tables("WORD").Rows(0)!xx_dadatcons = NTSCStr(edDadatcons.Text)
      ds.Tables("WORD").Rows(0)!xx_adatcons = NTSCStr(edAdatcons.Text)
      ds.Tables("WORD").Rows(0)!xx_dadatord = NTSCStr(edDadatord.Text)
      ds.Tables("WORD").Rows(0)!xx_adatord = NTSCStr(edAdatord.Text)
      ds.AcceptChanges()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Filtri Estesi"
  Public Overridable Function CampoTesto(ByVal strTesto As String, Optional ByVal bApplicaPerc As Boolean = False) As String
    Dim strOut As String = ""
    Dim bFil As String = ""
    Try
      If bApplicaPerc Then bFil = "%"

      If strTesto.Length > 1 Then
        If strTesto.Substring(strTesto.Length - 1, 1) = "*" Then
          strTesto = strTesto.Substring(0, strTesto.Length - 1)
        End If
      End If
      strOut = CStrSQL(strTesto & bFil)

      Return strOut

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return "''"
    End Try
  End Function
#End Region

  Public Overridable Sub ckSerie_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSerie.CheckedChanged
    Try
      If ckSerie.Checked = True Then
        GctlSetVisEnab(edSerie, False)
        If edSerie.Text = "" Then edSerie.Text = " "
        edSerie.Focus()
      Else
        edSerie.Enabled = False
        edSerie.Text = ""
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edSerie_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSerie.Validated
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edSerie.Text, False)
      If strTmp <> edSerie.Text Then edSerie.Text = strTmp

      If ckSerie.Checked And edSerie.Text = "" Then edSerie.Text = " "
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Gestione filtri"
  Public Overridable Function ComponiDatatable(ByRef dttCampiForm As DataTable, ByVal oControl As Control) As Boolean
    Try
      'Verifico a quale controllo corrisponde e lo aggiungo al datatable dei campi.
      Select Case oControl.GetType().ToString
        Case "NTSInformatica.NTSButton"
          If oControl.Name = "cmdApriFiltri" Then Return True ' è un componente per la gestione dei filtri

          dttCampiForm.Rows.Add()
          With dttCampiForm.Rows(dttCampiForm.Rows.Count - 1)
            !xx_descr = oControl.Text
            !mo_control = oControl.Name
            !mo_valore = ""
            !mo_locked = IIf(oControl.Enabled, "N", "S")
            !xx_tipo = "NTSButton"
            !mo_ordin = dttCampiForm.Rows.Count
          End With
          Return True
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
          If oControl.Name = "cbFiltro" Then Return True ' è un componente per la gestione dei filtri

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
        If Not oCleScho.LeggiFiltro(lCod, "BNORSCHO", "FRMORSCHO", dttPersForm) Then Return False
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
            If cmdControl.Enabled Or bDoEnable Then ' Se il controllo è abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              cmdControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSTextBoxNum"
            Dim edControl As NTSTextBoxNum = CType(oControl, NTSTextBoxNum)
            edControl.Text = NTSCInt(dttControl.Rows(z)!mo_valore).ToString
            If edControl.Enabled Or bDoEnable Then ' Se il controllo è abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              edControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSTextBoxStr"
            Dim edControl As NTSTextBoxStr = CType(oControl, NTSTextBoxStr)
            edControl.Text = NTSCStr(dttControl.Rows(z)!mo_valore)
            If edControl.Enabled Or bDoEnable Then ' Se il controllo è abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              edControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSTextBoxData"
            Dim edControl As NTSTextBoxData = CType(oControl, NTSTextBoxData)
            edControl.Text = ConvertiInData(NTSCStr(dttControl.Rows(z)!mo_valore))
            If edControl.Enabled Or bDoEnable Then ' Se il controllo è abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              edControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSMemoBox"
            Dim edControl As NTSMemoBox = CType(oControl, NTSMemoBox)
            edControl.Text = NTSCStr(dttControl.Rows(z)!mo_valore)
            If edControl.Enabled Or bDoEnable Then ' Se il controllo è abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              edControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSCheckBox"
            Dim ckControl As NTSCheckBox = CType(oControl, NTSCheckBox)
            ckControl.Checked = CBool(dttControl.Rows(z)!mo_valore)
            If ckControl.Enabled Or bDoEnable Then ' Se il controllo è abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              ckControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSComboBox"
            Dim cbControl As NTSComboBox = CType(oControl, NTSComboBox)
            cbControl.SelectedValue = NTSCStr(dttControl.Rows(z)!mo_valore)
            If cbControl.Enabled Or bDoEnable Then ' Se il controllo è abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              cbControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
          Case "NTSInformatica.NTSRadioButton"
            Dim opControl As NTSRadioButton = CType(oControl, NTSRadioButton)
            If CBool(dttControl.Rows(z)!mo_valore) Then opControl.Checked = True
            If opControl.Enabled Or bDoEnable Then ' Se il controllo è abilitato vuol dire che posso disabilitarlo (altrimenti faccio vincere la GCTL)
              opControl.Enabled = CBool(IIf(NTSCStr(dttControl.Rows(z)!mo_locked) = "N", True, False))
            End If
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

      'Data odierna o oggi è quella del giorno di stampa
      If strData.ToLower = "data odierna" Or strData.ToLower = "oggi" Or _
         strData.ToLower = "today" Or strData.ToLower = "now" Or _
         strData.ToLower = oApp.Tr(Me, 129223786674775598, "data odierna").ToLower Or _
         strData.ToLower = oApp.Tr(Me, 129223786568213098, "oggi").ToLower Then Return Now.ToShortDateString

      'Non è di tipo "+1 Giorno", ecc... tutti con uno spazio in mezzo, quindi è una data classica
      strPart = strData.Split(" "c)

      If strPart.Length = 1 Then Return strData

      'dovrebbero essere supportati solo i nomi italiani, ma aggiungerne anche altri non costa niente.
      Select Case strPart(1).ToLower
        Case "giorni", "giorno", "g", "day", "days", "d", oApp.Tr(Me, 129223783990556848, "giorni").ToLower, oApp.Tr(Me, 129223784107275598, "giorno").ToLower
          Return Now.AddDays(NTSCInt(strPart(0))).ToShortDateString
        Case "mese", "mesi", "month", "months", "m", oApp.Tr(Me, 129223784378681848, "mese").ToLower, oApp.Tr(Me, 129223784395244348, "mesi").ToLower
          Return Now.AddMonths(NTSCInt(strPart(0))).ToShortDateString
        Case "anno", "anni", "a", "year", "years", "y", oApp.Tr(Me, 129223784387119348, "anno").ToLower, oApp.Tr(Me, 129223784402744348, "anni").ToLower
          Return Now.AddYears(NTSCInt(strPart(0))).ToShortDateString
      End Select

      'Se non riesce a codificarla in nulla ritorna la data di oggi e avvisa
      oApp.MsgBoxErr(oApp.Tr(Me, 129223785065556848, "Non è stato possibile tradurre la data '|" & strData & "|'"))
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