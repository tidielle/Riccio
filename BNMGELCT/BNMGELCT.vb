#Region "Imports"
Imports System.Data
Imports NTSInformatica.CLN__STD
#End Region

Public Class FRMMGELCT

#Region "Moduli"
  Private Moduli_P As Integer = bsModMG
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

#Region "Variaribli"
  Public oCleElct As CLEMGELCT
  Public oCallParams As CLE__CLDP

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbElabora As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarMenuItem
  Public WithEvents lbConto As NTSInformatica.NTSLabel
  Public WithEvents edConto As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDesconto As NTSInformatica.NTSLabel
  Public WithEvents edCodmarc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDescodmarc As NTSInformatica.NTSLabel
  Public WithEvents lbDatagg As NTSInformatica.NTSLabel
  Public WithEvents edDataAgg As NTSInformatica.NTSTextBoxData
  Public WithEvents fmTipoAggiornamenti As NTSInformatica.NTSGroupBox
  Public WithEvents ckDescrizione As NTSInformatica.NTSCheckBox
  Public WithEvents ckBarcode As NTSInformatica.NTSCheckBox
  Public WithEvents ckPrezzi As NTSInformatica.NTSCheckBox
  Public WithEvents ckClasseSconto As NTSInformatica.NTSCheckBox
  Public WithEvents ckGruppo As NTSInformatica.NTSCheckBox
  Public WithEvents ckStatus As NTSInformatica.NTSCheckBox
  Public WithEvents ckFamiglia As NTSInformatica.NTSCheckBox
  Public WithEvents ckNomenclatura As NTSInformatica.NTSCheckBox
  Public WithEvents ckRrfence As NTSInformatica.NTSCheckBox
  Public WithEvents ckNote As NTSInformatica.NTSCheckBox
  Public WithEvents ckPesi As NTSInformatica.NTSCheckBox
  Public WithEvents ckLotto As NTSInformatica.NTSCheckBox
  Public WithEvents fmTipoElaborazione As NTSInformatica.NTSGroupBox
  Public WithEvents ckElaborazione As NTSInformatica.NTSCheckBox
  Public WithEvents pnElct As NTSInformatica.NTSPanel
  Public WithEvents lbStatus As NTSInformatica.NTSLabel
#End Region

  Public Overloads Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    '---------------------------------
    'questa funzione riceve gli eventi dall'ENTITY: rimappata rispetto a quella standard di FRM__CHILD
    'prima eseguo quella standard
    Dim strTmp() As String
    Dim i As Integer = 0
    Dim frmInpc As FRMMGINPC = Nothing
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
          Case "MODALEMAN:"
            frmInpc = CType(NTSNewFormModal("FRMMGINPC"), FRMMGINPC)
            frmInpc.Init(oMenu, oCallParams)
            frmInpc.InitEntity(oCleElct)
            frmInpc.lbInfoBar.Text = lbDesconto.Text & " - " & lbDescodmarc.Text & " - " & edDataAgg.Text
            frmInpc.ShowDialog()
            e.RetValue = frmInpc.strFormRetValue
        End Select
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmInpc Is Nothing Then frmInpc.Dispose()
      frmInpc = Nothing
    End Try
  End Sub

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGELCT))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbElabora = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarMenuItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.lbConto = New NTSInformatica.NTSLabel
    Me.edConto = New NTSInformatica.NTSTextBoxNum
    Me.lbDesconto = New NTSInformatica.NTSLabel
    Me.edCodmarc = New NTSInformatica.NTSTextBoxNum
    Me.lbDescodmarc = New NTSInformatica.NTSLabel
    Me.lbDatagg = New NTSInformatica.NTSLabel
    Me.edDataAgg = New NTSInformatica.NTSTextBoxData
    Me.fmTipoAggiornamenti = New NTSInformatica.NTSGroupBox
    Me.ckLotto = New NTSInformatica.NTSCheckBox
    Me.ckPesi = New NTSInformatica.NTSCheckBox
    Me.ckRrfence = New NTSInformatica.NTSCheckBox
    Me.ckNote = New NTSInformatica.NTSCheckBox
    Me.ckNomenclatura = New NTSInformatica.NTSCheckBox
    Me.ckFamiglia = New NTSInformatica.NTSCheckBox
    Me.ckGruppo = New NTSInformatica.NTSCheckBox
    Me.ckStatus = New NTSInformatica.NTSCheckBox
    Me.ckClasseSconto = New NTSInformatica.NTSCheckBox
    Me.ckBarcode = New NTSInformatica.NTSCheckBox
    Me.ckPrezzi = New NTSInformatica.NTSCheckBox
    Me.ckDescrizione = New NTSInformatica.NTSCheckBox
    Me.fmTipoElaborazione = New NTSInformatica.NTSGroupBox
    Me.ckElaborazione = New NTSInformatica.NTSCheckBox
    Me.pnElct = New NTSInformatica.NTSPanel
    Me.ckCodmarc = New NTSInformatica.NTSCheckBox
    Me.lbStatus = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodmarc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataAgg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmTipoAggiornamenti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmTipoAggiornamenti.SuspendLayout()
    CType(Me.ckLotto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckPesi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckRrfence.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckNomenclatura.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckFamiglia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckGruppo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckStatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckClasseSconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckBarcode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckPrezzi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDescrizione.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmTipoElaborazione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmTipoElaborazione.SuspendLayout()
    CType(Me.ckElaborazione.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnElct, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnElct.SuspendLayout()
    CType(Me.ckCodmarc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbElabora, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbZoom, Me.tlbStrumenti, Me.tlbGuida, Me.tlbEsci, Me.tlbImpostaStampante})
    Me.NtsBarManager1.MaxItemId = 11
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbElabora), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbElabora.GlyphPath = ""
    Me.tlbElabora.Id = 0
    Me.tlbElabora.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbElabora.Name = "tlbElabora"
    Me.tlbElabora.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.GlyphPath = ""
    Me.tlbStampa.Id = 1
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "StampaVideo"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.GlyphPath = ""
    Me.tlbStampaVideo.Id = 2
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.GlyphPath = ""
    Me.tlbZoom.Id = 3
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 7
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.GlyphPath = ""
    Me.tlbImpostaStampante.Id = 10
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.NTSIsCheckBox = False
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 8
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 9
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'lbConto
    '
    Me.lbConto.AutoSize = True
    Me.lbConto.BackColor = System.Drawing.Color.Transparent
    Me.lbConto.Location = New System.Drawing.Point(13, 11)
    Me.lbConto.Name = "lbConto"
    Me.lbConto.NTSDbField = ""
    Me.lbConto.Size = New System.Drawing.Size(51, 13)
    Me.lbConto.TabIndex = 4
    Me.lbConto.Text = "Fornitore"
    Me.lbConto.Tooltip = ""
    Me.lbConto.UseMnemonic = False
    '
    'edConto
    '
    Me.edConto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edConto.Location = New System.Drawing.Point(152, 8)
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
    Me.edConto.Size = New System.Drawing.Size(100, 20)
    Me.edConto.TabIndex = 5
    '
    'lbDesconto
    '
    Me.lbDesconto.BackColor = System.Drawing.Color.Transparent
    Me.lbDesconto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesconto.Location = New System.Drawing.Point(258, 8)
    Me.lbDesconto.Name = "lbDesconto"
    Me.lbDesconto.NTSDbField = ""
    Me.lbDesconto.Size = New System.Drawing.Size(288, 20)
    Me.lbDesconto.TabIndex = 6
    Me.lbDesconto.Tooltip = ""
    Me.lbDesconto.UseMnemonic = False
    '
    'edCodmarc
    '
    Me.edCodmarc.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edCodmarc.Location = New System.Drawing.Point(152, 34)
    Me.edCodmarc.Name = "edCodmarc"
    Me.edCodmarc.NTSDbField = ""
    Me.edCodmarc.NTSFormat = "0"
    Me.edCodmarc.NTSForzaVisZoom = False
    Me.edCodmarc.NTSOldValue = ""
    Me.edCodmarc.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodmarc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodmarc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodmarc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodmarc.Properties.AutoHeight = False
    Me.edCodmarc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodmarc.Properties.MaxLength = 65536
    Me.edCodmarc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodmarc.Size = New System.Drawing.Size(100, 20)
    Me.edCodmarc.TabIndex = 8
    '
    'lbDescodmarc
    '
    Me.lbDescodmarc.BackColor = System.Drawing.Color.Transparent
    Me.lbDescodmarc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescodmarc.Location = New System.Drawing.Point(258, 34)
    Me.lbDescodmarc.Name = "lbDescodmarc"
    Me.lbDescodmarc.NTSDbField = ""
    Me.lbDescodmarc.Size = New System.Drawing.Size(288, 20)
    Me.lbDescodmarc.TabIndex = 9
    Me.lbDescodmarc.Tooltip = ""
    Me.lbDescodmarc.UseMnemonic = False
    '
    'lbDatagg
    '
    Me.lbDatagg.AutoSize = True
    Me.lbDatagg.BackColor = System.Drawing.Color.Transparent
    Me.lbDatagg.Location = New System.Drawing.Point(13, 63)
    Me.lbDatagg.Name = "lbDatagg"
    Me.lbDatagg.NTSDbField = ""
    Me.lbDatagg.Size = New System.Drawing.Size(120, 13)
    Me.lbDatagg.TabIndex = 10
    Me.lbDatagg.Text = "Da data aggiornamento"
    Me.lbDatagg.Tooltip = ""
    Me.lbDatagg.UseMnemonic = False
    '
    'edDataAgg
    '
    Me.edDataAgg.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataAgg.Location = New System.Drawing.Point(152, 60)
    Me.edDataAgg.Name = "edDataAgg"
    Me.edDataAgg.NTSDbField = ""
    Me.edDataAgg.NTSForzaVisZoom = False
    Me.edDataAgg.NTSOldValue = ""
    Me.edDataAgg.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataAgg.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataAgg.Properties.AutoHeight = False
    Me.edDataAgg.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataAgg.Properties.MaxLength = 65536
    Me.edDataAgg.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataAgg.Size = New System.Drawing.Size(100, 20)
    Me.edDataAgg.TabIndex = 11
    '
    'fmTipoAggiornamenti
    '
    Me.fmTipoAggiornamenti.AllowDrop = True
    Me.fmTipoAggiornamenti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmTipoAggiornamenti.Appearance.Options.UseBackColor = True
    Me.fmTipoAggiornamenti.Controls.Add(Me.ckLotto)
    Me.fmTipoAggiornamenti.Controls.Add(Me.ckPesi)
    Me.fmTipoAggiornamenti.Controls.Add(Me.ckRrfence)
    Me.fmTipoAggiornamenti.Controls.Add(Me.ckNote)
    Me.fmTipoAggiornamenti.Controls.Add(Me.ckNomenclatura)
    Me.fmTipoAggiornamenti.Controls.Add(Me.ckFamiglia)
    Me.fmTipoAggiornamenti.Controls.Add(Me.ckGruppo)
    Me.fmTipoAggiornamenti.Controls.Add(Me.ckStatus)
    Me.fmTipoAggiornamenti.Controls.Add(Me.ckClasseSconto)
    Me.fmTipoAggiornamenti.Controls.Add(Me.ckBarcode)
    Me.fmTipoAggiornamenti.Controls.Add(Me.ckPrezzi)
    Me.fmTipoAggiornamenti.Controls.Add(Me.ckDescrizione)
    Me.fmTipoAggiornamenti.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmTipoAggiornamenti.Location = New System.Drawing.Point(16, 96)
    Me.fmTipoAggiornamenti.Name = "fmTipoAggiornamenti"
    Me.fmTipoAggiornamenti.Size = New System.Drawing.Size(343, 192)
    Me.fmTipoAggiornamenti.TabIndex = 12
    Me.fmTipoAggiornamenti.Text = "Tipo di aggiornamento:"
    '
    'ckLotto
    '
    Me.ckLotto.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckLotto.Location = New System.Drawing.Point(161, 158)
    Me.ckLotto.Name = "ckLotto"
    Me.ckLotto.NTSCheckValue = "S"
    Me.ckLotto.NTSUnCheckValue = "N"
    Me.ckLotto.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckLotto.Properties.Appearance.Options.UseBackColor = True
    Me.ckLotto.Properties.AutoHeight = False
    Me.ckLotto.Properties.Caption = "&Lotto/sottolotto"
    Me.ckLotto.Size = New System.Drawing.Size(142, 18)
    Me.ckLotto.TabIndex = 11
    '
    'ckPesi
    '
    Me.ckPesi.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckPesi.Location = New System.Drawing.Point(161, 133)
    Me.ckPesi.Name = "ckPesi"
    Me.ckPesi.NTSCheckValue = "S"
    Me.ckPesi.NTSUnCheckValue = "N"
    Me.ckPesi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPesi.Properties.Appearance.Options.UseBackColor = True
    Me.ckPesi.Properties.AutoHeight = False
    Me.ckPesi.Properties.Caption = "&Pesi lordo/netto e volume"
    Me.ckPesi.Size = New System.Drawing.Size(157, 18)
    Me.ckPesi.TabIndex = 10
    '
    'ckRrfence
    '
    Me.ckRrfence.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckRrfence.Location = New System.Drawing.Point(161, 108)
    Me.ckRrfence.Name = "ckRrfence"
    Me.ckRrfence.NTSCheckValue = "S"
    Me.ckRrfence.NTSUnCheckValue = "N"
    Me.ckRrfence.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckRrfence.Properties.Appearance.Options.UseBackColor = True
    Me.ckRrfence.Properties.AutoHeight = False
    Me.ckRrfence.Properties.Caption = "&RR Fence"
    Me.ckRrfence.Size = New System.Drawing.Size(86, 18)
    Me.ckRrfence.TabIndex = 9
    '
    'ckNote
    '
    Me.ckNote.Cursor = System.Windows.Forms.Cursors.Hand
    Me.ckNote.Location = New System.Drawing.Point(161, 83)
    Me.ckNote.Name = "ckNote"
    Me.ckNote.NTSCheckValue = "S"
    Me.ckNote.NTSUnCheckValue = "N"
    Me.ckNote.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNote.Properties.Appearance.Options.UseBackColor = True
    Me.ckNote.Properties.AutoHeight = False
    Me.ckNote.Properties.Caption = "N&ote"
    Me.ckNote.Size = New System.Drawing.Size(86, 18)
    Me.ckNote.TabIndex = 8
    '
    'ckNomenclatura
    '
    Me.ckNomenclatura.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckNomenclatura.Location = New System.Drawing.Point(161, 58)
    Me.ckNomenclatura.Name = "ckNomenclatura"
    Me.ckNomenclatura.NTSCheckValue = "S"
    Me.ckNomenclatura.NTSUnCheckValue = "N"
    Me.ckNomenclatura.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckNomenclatura.Properties.Appearance.Options.UseBackColor = True
    Me.ckNomenclatura.Properties.AutoHeight = False
    Me.ckNomenclatura.Properties.Caption = "&Nomenclatura combinata"
    Me.ckNomenclatura.Size = New System.Drawing.Size(157, 18)
    Me.ckNomenclatura.TabIndex = 7
    '
    'ckFamiglia
    '
    Me.ckFamiglia.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckFamiglia.Location = New System.Drawing.Point(161, 33)
    Me.ckFamiglia.Name = "ckFamiglia"
    Me.ckFamiglia.NTSCheckValue = "S"
    Me.ckFamiglia.NTSUnCheckValue = "N"
    Me.ckFamiglia.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckFamiglia.Properties.Appearance.Options.UseBackColor = True
    Me.ckFamiglia.Properties.AutoHeight = False
    Me.ckFamiglia.Properties.Caption = "&Famiglia"
    Me.ckFamiglia.Size = New System.Drawing.Size(86, 18)
    Me.ckFamiglia.TabIndex = 6
    '
    'ckGruppo
    '
    Me.ckGruppo.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckGruppo.Location = New System.Drawing.Point(18, 158)
    Me.ckGruppo.Name = "ckGruppo"
    Me.ckGruppo.NTSCheckValue = "S"
    Me.ckGruppo.NTSUnCheckValue = "N"
    Me.ckGruppo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckGruppo.Properties.Appearance.Options.UseBackColor = True
    Me.ckGruppo.Properties.AutoHeight = False
    Me.ckGruppo.Properties.Caption = "&Gruppo/sottogruppo"
    Me.ckGruppo.Size = New System.Drawing.Size(133, 18)
    Me.ckGruppo.TabIndex = 5
    '
    'ckStatus
    '
    Me.ckStatus.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckStatus.Location = New System.Drawing.Point(18, 133)
    Me.ckStatus.Name = "ckStatus"
    Me.ckStatus.NTSCheckValue = "S"
    Me.ckStatus.NTSUnCheckValue = "N"
    Me.ckStatus.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckStatus.Properties.Appearance.Options.UseBackColor = True
    Me.ckStatus.Properties.AutoHeight = False
    Me.ckStatus.Properties.Caption = "&Status"
    Me.ckStatus.Size = New System.Drawing.Size(86, 18)
    Me.ckStatus.TabIndex = 4
    '
    'ckClasseSconto
    '
    Me.ckClasseSconto.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckClasseSconto.Location = New System.Drawing.Point(18, 108)
    Me.ckClasseSconto.Name = "ckClasseSconto"
    Me.ckClasseSconto.NTSCheckValue = "S"
    Me.ckClasseSconto.NTSUnCheckValue = "N"
    Me.ckClasseSconto.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckClasseSconto.Properties.Appearance.Options.UseBackColor = True
    Me.ckClasseSconto.Properties.AutoHeight = False
    Me.ckClasseSconto.Properties.Caption = "&Classe di sconto"
    Me.ckClasseSconto.Size = New System.Drawing.Size(120, 18)
    Me.ckClasseSconto.TabIndex = 3
    '
    'ckBarcode
    '
    Me.ckBarcode.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckBarcode.Location = New System.Drawing.Point(18, 83)
    Me.ckBarcode.Name = "ckBarcode"
    Me.ckBarcode.NTSCheckValue = "S"
    Me.ckBarcode.NTSUnCheckValue = "N"
    Me.ckBarcode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckBarcode.Properties.Appearance.Options.UseBackColor = True
    Me.ckBarcode.Properties.AutoHeight = False
    Me.ckBarcode.Properties.Caption = "&Barcode"
    Me.ckBarcode.Size = New System.Drawing.Size(86, 18)
    Me.ckBarcode.TabIndex = 2
    '
    'ckPrezzi
    '
    Me.ckPrezzi.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckPrezzi.Location = New System.Drawing.Point(18, 58)
    Me.ckPrezzi.Name = "ckPrezzi"
    Me.ckPrezzi.NTSCheckValue = "S"
    Me.ckPrezzi.NTSUnCheckValue = "N"
    Me.ckPrezzi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPrezzi.Properties.Appearance.Options.UseBackColor = True
    Me.ckPrezzi.Properties.AutoHeight = False
    Me.ckPrezzi.Properties.Caption = "&Prezzi"
    Me.ckPrezzi.Size = New System.Drawing.Size(86, 18)
    Me.ckPrezzi.TabIndex = 1
    '
    'ckDescrizione
    '
    Me.ckDescrizione.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDescrizione.Location = New System.Drawing.Point(18, 33)
    Me.ckDescrizione.Name = "ckDescrizione"
    Me.ckDescrizione.NTSCheckValue = "S"
    Me.ckDescrizione.NTSUnCheckValue = "N"
    Me.ckDescrizione.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDescrizione.Properties.Appearance.Options.UseBackColor = True
    Me.ckDescrizione.Properties.AutoHeight = False
    Me.ckDescrizione.Properties.Caption = "&Descrizione"
    Me.ckDescrizione.Size = New System.Drawing.Size(86, 18)
    Me.ckDescrizione.TabIndex = 0
    '
    'fmTipoElaborazione
    '
    Me.fmTipoElaborazione.AllowDrop = True
    Me.fmTipoElaborazione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmTipoElaborazione.Appearance.Options.UseBackColor = True
    Me.fmTipoElaborazione.Controls.Add(Me.ckElaborazione)
    Me.fmTipoElaborazione.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmTipoElaborazione.Location = New System.Drawing.Point(376, 238)
    Me.fmTipoElaborazione.Name = "fmTipoElaborazione"
    Me.fmTipoElaborazione.Size = New System.Drawing.Size(170, 50)
    Me.fmTipoElaborazione.TabIndex = 13
    Me.fmTipoElaborazione.Text = "Tipo di elaborazione:"
    '
    'ckElaborazione
    '
    Me.ckElaborazione.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckElaborazione.Location = New System.Drawing.Point(17, 23)
    Me.ckElaborazione.Name = "ckElaborazione"
    Me.ckElaborazione.NTSCheckValue = "S"
    Me.ckElaborazione.NTSUnCheckValue = "N"
    Me.ckElaborazione.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckElaborazione.Properties.Appearance.Options.UseBackColor = True
    Me.ckElaborazione.Properties.AutoHeight = False
    Me.ckElaborazione.Properties.Caption = "&Automatica"
    Me.ckElaborazione.Size = New System.Drawing.Size(86, 18)
    Me.ckElaborazione.TabIndex = 7
    '
    'pnElct
    '
    Me.pnElct.AllowDrop = True
    Me.pnElct.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnElct.Appearance.Options.UseBackColor = True
    Me.pnElct.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnElct.Controls.Add(Me.ckCodmarc)
    Me.pnElct.Controls.Add(Me.lbStatus)
    Me.pnElct.Controls.Add(Me.lbConto)
    Me.pnElct.Controls.Add(Me.fmTipoElaborazione)
    Me.pnElct.Controls.Add(Me.edConto)
    Me.pnElct.Controls.Add(Me.fmTipoAggiornamenti)
    Me.pnElct.Controls.Add(Me.lbDesconto)
    Me.pnElct.Controls.Add(Me.edDataAgg)
    Me.pnElct.Controls.Add(Me.lbDatagg)
    Me.pnElct.Controls.Add(Me.edCodmarc)
    Me.pnElct.Controls.Add(Me.lbDescodmarc)
    Me.pnElct.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnElct.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnElct.Location = New System.Drawing.Point(0, 30)
    Me.pnElct.Name = "pnElct"
    Me.pnElct.NTSActiveTrasparency = True
    Me.pnElct.Size = New System.Drawing.Size(558, 314)
    Me.pnElct.TabIndex = 14
    Me.pnElct.Text = "NtsPanel1"
    '
    'ckCodmarc
    '
    Me.ckCodmarc.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckCodmarc.EditValue = True
    Me.ckCodmarc.Location = New System.Drawing.Point(12, 35)
    Me.ckCodmarc.Name = "ckCodmarc"
    Me.ckCodmarc.NTSCheckValue = "S"
    Me.ckCodmarc.NTSUnCheckValue = "N"
    Me.ckCodmarc.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckCodmarc.Properties.Appearance.Options.UseBackColor = True
    Me.ckCodmarc.Properties.AutoHeight = False
    Me.ckCodmarc.Properties.Caption = "Marchio/Marca"
    Me.ckCodmarc.Size = New System.Drawing.Size(98, 18)
    Me.ckCodmarc.TabIndex = 36
    '
    'lbStatus
    '
    Me.lbStatus.AutoSize = True
    Me.lbStatus.BackColor = System.Drawing.Color.Transparent
    Me.lbStatus.Location = New System.Drawing.Point(13, 295)
    Me.lbStatus.Name = "lbStatus"
    Me.lbStatus.NTSDbField = ""
    Me.lbStatus.Size = New System.Drawing.Size(43, 13)
    Me.lbStatus.TabIndex = 35
    Me.lbStatus.Text = "Pronto."
    Me.lbStatus.Tooltip = ""
    Me.lbStatus.UseMnemonic = False
    '
    'FRMMGELCT
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(558, 344)
    Me.Controls.Add(Me.pnElct)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
    Me.MaximizeBox = False
    Me.Name = "FRMMGELCT"
    Me.Text = "AGGIORNAMENTO ARTICOLI DA CATALOGO FORNITORI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodmarc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataAgg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmTipoAggiornamenti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmTipoAggiornamenti.ResumeLayout(False)
    Me.fmTipoAggiornamenti.PerformLayout()
    CType(Me.ckLotto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckPesi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckRrfence.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckNomenclatura.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckFamiglia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckGruppo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckStatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckClasseSconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckBarcode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckPrezzi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDescrizione.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmTipoElaborazione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmTipoElaborazione.ResumeLayout(False)
    Me.fmTipoElaborazione.PerformLayout()
    CType(Me.ckElaborazione.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnElct, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnElct.ResumeLayout(False)
    Me.pnElct.PerformLayout()
    CType(Me.ckCodmarc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    'creo e attivo l'entity e inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGELCT", "BEMGELCT", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128666353134259345, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleElct = CType(oTmp, CLEMGELCT)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGELCT", strRemoteServer, strRemotePort)
    AddHandler oCleElct.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleElct.Init(oApp, oScript, oMenu.oCleComm, "ARTEST", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

#Region "Eventi Form"
  Public Overridable Sub FRMMGELCT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try

      edConto.Text = "0"
      edCodmarc.Text = "0"
      edDataAgg.Text = "01/01/1900"

      'leggo recent da registro
      ckDescrizione.Checked = CBool(IIf(oCleElct.GetSettingBusRecentCheck("Aggiorna descrizione") = "1", True, False))
      ckPrezzi.Checked = CBool(IIf(oCleElct.GetSettingBusRecentCheck("Aggiorna prezzi") = "1", True, False))
      ckBarcode.Checked = CBool(IIf(oCleElct.GetSettingBusRecentCheck("Aggiorna barcode") = "1", True, False))
      ckClasseSconto.Checked = CBool(IIf(oCleElct.GetSettingBusRecentCheck("Aggiorna classe di sconto") = "1", True, False))
      ckStatus.Checked = CBool(IIf(oCleElct.GetSettingBusRecentCheck("Aggiorna status") = "1", True, False))
      ckGruppo.Checked = CBool(IIf(oCleElct.GetSettingBusRecentCheck("Aggiorna gruppo/sottogruppo") = "1", True, False))
      ckFamiglia.Checked = CBool(IIf(oCleElct.GetSettingBusRecentCheck("Aggiorna famiglia") = "1", True, False))
      ckNomenclatura.Checked = CBool(IIf(oCleElct.GetSettingBusRecentCheck("Aggiorna nomenclatura combinata") = "1", True, False))
      ckNote.Checked = CBool(IIf(oCleElct.GetSettingBusRecentCheck("Aggiorna note") = "1", True, False))
      ckRrfence.Checked = CBool(IIf(oCleElct.GetSettingBusRecentCheck("Aggiorna rrfence") = "1", True, False))
      ckPesi.Checked = CBool(IIf(oCleElct.GetSettingBusRecentCheck("Aggiorna pesi lordo/netto e volume") = "1", True, False))
      ckLotto.Checked = CBool(IIf(oCleElct.GetSettingBusRecentCheck("Aggiorna lotto e sottolotto") = "1", True, False))

      '-------------------------------------------------------
      '--- Predispongo i controlli
      '-------------------------------------------------------
      InitControls()

      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGELCT_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      oCleElct.ResetTblInstId()

      'scrivo recent da registro
      oCleElct.SaveSettingBusRecentCheck("Aggiorna descrizione", ckDescrizione.Checked)
      oCleElct.SaveSettingBusRecentCheck("Aggiorna prezzi", ckPrezzi.Checked)
      oCleElct.SaveSettingBusRecentCheck("Aggiorna barcode", ckBarcode.Checked)
      oCleElct.SaveSettingBusRecentCheck("Aggiorna classe di sconto", ckClasseSconto.Checked)
      oCleElct.SaveSettingBusRecentCheck("Aggiorna status", ckStatus.Checked)
      oCleElct.SaveSettingBusRecentCheck("Aggiorna gruppo/sottogruppo", ckGruppo.Checked)
      oCleElct.SaveSettingBusRecentCheck("Aggiorna famiglia", ckFamiglia.Checked)
      oCleElct.SaveSettingBusRecentCheck("Aggiorna nomenclatura combinata", ckNomenclatura.Checked)
      oCleElct.SaveSettingBusRecentCheck("Aggiorna note", ckNote.Checked)
      oCleElct.SaveSettingBusRecentCheck("Aggiorna rrfence", ckRrfence.Checked)
      oCleElct.SaveSettingBusRecentCheck("Aggiorna pesi lordo/netto e volume", ckPesi.Checked)
      oCleElct.SaveSettingBusRecentCheck("Aggiorna lotto e sottolotto", ckLotto.Checked)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbElabora_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbElabora.ItemClick
    Elabora()
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

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim strTipork As String = ""

    Try
      'per eventuali altri controlli caricati al volo
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      'If edConto.Focused Then
      '  '----------------------------------------------
      '  'zoom specifico per mastri di contabilità
      '  SetFastZoom(edConto.Text, oParam) 'gestione dello zoom veloce
      '  NTSZOOM.strIn = edConto.Text
      '  oParam.bTipoProposto = False
      '  NTSZOOM.ZoomStrIn("ZOOMANAGRA" & cbTipo.SelectedValue.ToUpper, DittaCorrente, oParam)
      '  If NTSZOOM.strIn <> edConto.Text Then edConto.Text = NTSZOOM.strIn
      'Else
      If edConto.Focused Then
        SetFastZoom(edConto.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edConto.Text
        'oParam.bVisGriglia = True
        'oParam.strTipo = "F"
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRAF", DittaCorrente, oParam)
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

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub
#End Region

#Region "Eventi CheckBox"
  Public Overridable Sub ckCodmarc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckCodmarc.CheckedChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If ckCodmarc.Checked = True Then
        GctlSetVisEnab(edCodmarc, False)
        edCodmarc.Focus()
      Else
        edCodmarc.Enabled = False
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Sub LoadImage()
    '-------------------------------------------------
    'carico le immagini della toolbar
    Try
      tlbElabora.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
    Catch ex As Exception
    End Try
    Try
      tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
    Catch ex As Exception
    End Try
    Try
      tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
    Catch ex As Exception
    End Try
    Try
      tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
    Catch ex As Exception
    End Try
    Try
      tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
    Catch ex As Exception
    End Try
    Try
      tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
    Catch ex As Exception
    End Try
    Try
      tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
    Catch ex As Exception
      'non gestisco l'errore: se non c'Ã¨ una immagine prendo quella standard
    End Try
  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim i As Integer = 0
    Try
      LoadImage()

      tlbMain.NTSSetToolTip()

      '-------------------------------------------------
      'completo le informazioni dei controlli
      edConto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128535054430815080, "Fornitore"), tabanagra)
      edCodmarc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128671553009172611, "Marchio/Marca"), tabmarc)
      edDataAgg.NTSSetParam(oMenu, oApp.Tr(Me, 128671555072297611, "Da data aggiornamento"), False)
      edConto.NTSSetParamZoom("ZOOMANAGRAF")

      '-------------------------------------------------
      'chiamo lo script per inizializzare i controlli caricati con source ext
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim strCrpe As String
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim i As Integer
    Try
      'Test sui valori della form
      If Not Elabora() Then Return

      If Not oCleElct.VerificaPresenzaDatiDaStampare() Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128674334255937500, "Nessun aggiornamento effettuato, non esistono dati da stampare."))
        Return
      End If

      'Composizione formula per report
      strCrpe = "{TTARTICOX.instid} = " & oCleElct.nInstid & " And {TTARTICOX.codditt} = '" & DittaCorrente & "'"

      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGELCT", "Reports1", "", 0, nDestin, "BSMGELCT.RPT", False, "AGGIORNAMENTO ARTICOLI DA CATALOGO", False)

      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce già  il multireport)
      For i = LBound(CType(nPjob, Array), 2) To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Elabora() As Boolean
    Dim bRet As Boolean = True
    Try

      If Not TestPreElabora() Then Return False

      lbStatus.Text = oApp.Tr(Me, 128672331549062500, "Selezione articoli in corso...")
      If oCleElct.Apri(NTSCInt(edConto.Text), NTSCInt(edCodmarc.Text), NTSCDate(edDataAgg.Text), ckCodmarc.Checked) Then
        If Not oCleElct.Elabora(ckElaborazione.Checked, ckDescrizione.Checked, ckClasseSconto.Checked, _
                                ckStatus.Checked, ckGruppo.Checked, ckFamiglia.Checked, ckNomenclatura.Checked, _
                                ckNote.Checked, ckRrfence.Checked, ckPesi.Checked, ckLotto.Checked, _
                                ckPrezzi.Checked, ckBarcode.Checked) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128671794803235111, "Errore durante la fase di elaborazione."))
          bRet = False
        End If
      Else
        oApp.MsgBoxInfo(oApp.Tr(Me, 128671717935735111, "Non esistono dati con queste caratteristiche."))
        bRet = False
      End If
      lbStatus.Text = oApp.Tr(Me, 128729688072812500, "Pronto.")
      Return bRet

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      lbStatus.Text = oApp.Tr(Me, 128672331742656250, "Pronto.")
    End Try
  End Function
  Public Overridable Function TestPreElabora() As Boolean
    Try
      'controlla codfornitore obbligatorio
      If edConto.Text = "0" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128671713058547611, "Codice fornitore obbligatorio."))
        Return False
      End If

      'almeno un aggiornamento selezionato
      If Not (ckDescrizione.Checked Or ckPrezzi.Checked Or ckBarcode.Checked _
      Or ckClasseSconto.Checked Or ckStatus.Checked Or ckGruppo.Checked _
      Or ckFamiglia.Checked Or ckNomenclatura.Checked Or ckNote.Checked _
      Or ckRrfence.Checked Or ckPesi.Checked Or ckLotto.Checked) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128671716187297611, "Selezionare almeno un tipo di aggiornamento."))
        Return False
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub edConto_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edConto.Validated
    Dim strTmp As String = ""
    Try
      If edConto.Text <> "0" Then
        If Not oCleElct.ValidaConto(edConto.Text, strTmp) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128671660719172611, "Codice fornitore inesistente."))
          edConto.Text = "0"
        End If
      End If
      lbDesconto.Text = strTmp

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edCodmarc_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodmarc.Validated
    Dim strTmp As String = ""
    Try
      If edCodmarc.Text <> "0" Then
        If Not oCleElct.ValidaCodmarc(edCodmarc.Text, strTmp) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128671660959328861, "Codice marchio/marca inesistente."))
          edCodmarc.Text = "0"
        End If
      End If
      lbDescodmarc.Text = strTmp

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class
