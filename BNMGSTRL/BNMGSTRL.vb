Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGSTRL
  Private Moduli_P As Integer = bsModMG
  Private ModuliExt_P As Integer = bsModExtMGE
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

  Public oCleStrl As CLEMGSTRL
  Public oCallParams As CLE__CLDP
  Public bInBloccaControlli As Boolean = False
  Public strWhereArtico As String = "."

  Private components As System.ComponentModel.IContainer


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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGSTRL))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbElabMultiMagaz = New NTSInformatica.NTSBarMenuItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaGriglia = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.edMagaz = New NTSInformatica.NTSTextBoxNum
    Me.lbMagaz = New NTSInformatica.NTSLabel
    Me.lbTipoElab = New NTSInformatica.NTSLabel
    Me.cbTipoElab = New NTSInformatica.NTSComboBox
    Me.edDtelab = New NTSInformatica.NTSTextBoxData
    Me.ckInvFinale = New NTSInformatica.NTSCheckBox
    Me.fmListini = New NTSInformatica.NTSGroupBox
    Me.edSalvaListino = New NTSInformatica.NTSTextBoxNum
    Me.ckSalvaCostiZero = New NTSInformatica.NTSCheckBox
    Me.lbSalvaListData = New NTSInformatica.NTSLabel
    Me.edSalvaListData = New NTSInformatica.NTSTextBoxData
    Me.lbSalvaListino = New NTSInformatica.NTSLabel
    Me.ckSalvaListini = New NTSInformatica.NTSCheckBox
    Me.lbGiacenze = New NTSInformatica.NTSLabel
    Me.cbGiacenze = New NTSInformatica.NTSComboBox
    Me.ckUsacostiglob = New NTSInformatica.NTSCheckBox
    Me.opLifo = New NTSInformatica.NTSRadioButton
    Me.opMedio = New NTSInformatica.NTSRadioButton
    Me.opMedioGlobale = New NTSInformatica.NTSRadioButton
    Me.opUltimoCosto = New NTSInformatica.NTSRadioButton
    Me.opListino = New NTSInformatica.NTSRadioButton
    Me.opFifo = New NTSInformatica.NTSRadioButton
    Me.opUltimoCostoacc = New NTSInformatica.NTSRadioButton
    Me.opInventarioFinale = New NTSInformatica.NTSRadioButton
    Me.ckLifoAnniPrec = New NTSInformatica.NTSCheckBox
    Me.opMagazUno = New NTSInformatica.NTSRadioButton
    Me.opMagazTutti = New NTSInformatica.NTSRadioButton
    Me.opMagazAltrui = New NTSInformatica.NTSRadioButton
    Me.opMagMerceProp = New NTSInformatica.NTSRadioButton
    Me.fmValorizzazione = New NTSInformatica.NTSGroupBox
    Me.ckUsaListForn = New NTSInformatica.NTSCheckBox
    Me.ckSoloPrezziListino = New NTSInformatica.NTSCheckBox
    Me.edListino = New NTSInformatica.NTSTextBoxNum
    Me.fmMagazzini = New NTSInformatica.NTSGroupBox
    Me.lbNegozio = New NTSInformatica.NTSLabel
    Me.edNegozio = New NTSInformatica.NTSTextBoxNum
    Me.opMagazNegozio = New NTSInformatica.NTSRadioButton
    Me.ckDettaglioTCO = New NTSInformatica.NTSCheckBox
    Me.cmdSelArt = New NTSInformatica.NTSButton
    Me.lbStatus = New NTSInformatica.NTSLabel
    Me.ckEscludiArticoliNonMov = New NTSInformatica.NTSCheckBox
    Me.fmGeneraListaSelezionata = New NTSInformatica.NTSGroupBox
    Me.ckQta0 = New NTSInformatica.NTSCheckBox
    Me.edCodlsar = New NTSInformatica.NTSTextBoxNum
    Me.edDeslsar = New NTSInformatica.NTSTextBoxStr
    Me.ckGeneraListaSelezionata = New NTSInformatica.NTSCheckBox
    Me.ckTcoEsitTaglia = New NTSInformatica.NTSCheckBox
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edMagaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipoElab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDtelab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckInvFinale.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmListini, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmListini.SuspendLayout()
    CType(Me.edSalvaListino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSalvaCostiZero.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSalvaListData.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSalvaListini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbGiacenze.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckUsacostiglob.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opLifo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opMedio.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opMedioGlobale.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opUltimoCosto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opListino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opFifo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opUltimoCostoacc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opInventarioFinale.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckLifoAnniPrec.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opMagazUno.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opMagazTutti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opMagazAltrui.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opMagMerceProp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmValorizzazione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmValorizzazione.SuspendLayout()
    CType(Me.ckUsaListForn.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSoloPrezziListino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edListino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmMagazzini, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmMagazzini.SuspendLayout()
    CType(Me.edNegozio.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opMagazNegozio.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDettaglioTCO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckEscludiArticoliNonMov.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmGeneraListaSelezionata, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmGeneraListaSelezionata.SuspendLayout()
    CType(Me.ckQta0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodlsar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDeslsar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckGeneraListaSelezionata.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTcoEsitTaglia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbStampaGriglia, Me.tlbElabMultiMagaz})
    Me.NtsBarManager1.MaxItemId = 19
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaGriglia), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbZoom.GlyphPath = ""
    Me.tlbZoom.Id = 13
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 15
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbElabMultiMagaz)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.GlyphPath = ""
    Me.tlbImpostaStampante.Id = 16
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbElabMultiMagaz
    '
    Me.tlbElabMultiMagaz.Caption = "Elaborazione multi magazzino"
    Me.tlbElabMultiMagaz.GlyphPath = ""
    Me.tlbElabMultiMagaz.Id = 18
    Me.tlbElabMultiMagaz.Name = "tlbElabMultiMagaz"
    Me.tlbElabMultiMagaz.NTSIsCheckBox = False
    Me.tlbElabMultiMagaz.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.GlyphPath = ""
    Me.tlbStampa.Id = 4
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.GlyphPath = ""
    Me.tlbStampaVideo.Id = 5
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbStampaGriglia
    '
    Me.tlbStampaGriglia.Caption = "Stampa su griglia"
    Me.tlbStampaGriglia.Glyph = CType(resources.GetObject("tlbStampaGriglia.Glyph"), System.Drawing.Image)
    Me.tlbStampaGriglia.GlyphPath = ""
    Me.tlbStampaGriglia.Id = 17
    Me.tlbStampaGriglia.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F11)
    Me.tlbStampaGriglia.Name = "tlbStampaGriglia"
    Me.tlbStampaGriglia.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 11
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 12
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'edMagaz
    '
    Me.edMagaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMagaz.EditValue = "0"
    Me.edMagaz.Location = New System.Drawing.Point(117, 95)
    Me.edMagaz.Name = "edMagaz"
    Me.edMagaz.NTSDbField = ""
    Me.edMagaz.NTSFormat = "0"
    Me.edMagaz.NTSForzaVisZoom = False
    Me.edMagaz.NTSOldValue = ""
    Me.edMagaz.Properties.Appearance.Options.UseTextOptions = True
    Me.edMagaz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMagaz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMagaz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMagaz.Properties.AutoHeight = False
    Me.edMagaz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMagaz.Properties.MaxLength = 65536
    Me.edMagaz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMagaz.Size = New System.Drawing.Size(56, 20)
    Me.edMagaz.TabIndex = 6
    '
    'lbMagaz
    '
    Me.lbMagaz.BackColor = System.Drawing.Color.Transparent
    Me.lbMagaz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbMagaz.Location = New System.Drawing.Point(179, 95)
    Me.lbMagaz.Name = "lbMagaz"
    Me.lbMagaz.NTSDbField = ""
    Me.lbMagaz.Size = New System.Drawing.Size(204, 20)
    Me.lbMagaz.TabIndex = 7
    Me.lbMagaz.Tooltip = ""
    Me.lbMagaz.UseMnemonic = False
    '
    'lbTipoElab
    '
    Me.lbTipoElab.AutoSize = True
    Me.lbTipoElab.BackColor = System.Drawing.Color.Transparent
    Me.lbTipoElab.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbTipoElab.Location = New System.Drawing.Point(12, 46)
    Me.lbTipoElab.Name = "lbTipoElab"
    Me.lbTipoElab.NTSDbField = ""
    Me.lbTipoElab.Size = New System.Drawing.Size(107, 13)
    Me.lbTipoElab.TabIndex = 8
    Me.lbTipoElab.Text = "Tipo elaborazione"
    Me.lbTipoElab.Tooltip = ""
    Me.lbTipoElab.UseMnemonic = False
    '
    'cbTipoElab
    '
    Me.cbTipoElab.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipoElab.DataSource = Nothing
    Me.cbTipoElab.DisplayMember = ""
    Me.cbTipoElab.Location = New System.Drawing.Point(124, 43)
    Me.cbTipoElab.Name = "cbTipoElab"
    Me.cbTipoElab.NTSDbField = ""
    Me.cbTipoElab.Properties.AutoHeight = False
    Me.cbTipoElab.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipoElab.Properties.DropDownRows = 30
    Me.cbTipoElab.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipoElab.SelectedValue = ""
    Me.cbTipoElab.Size = New System.Drawing.Size(151, 20)
    Me.cbTipoElab.TabIndex = 9
    Me.cbTipoElab.ValueMember = ""
    '
    'edDtelab
    '
    Me.edDtelab.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDtelab.EditValue = "01/01/1900"
    Me.edDtelab.Location = New System.Drawing.Point(290, 43)
    Me.edDtelab.Name = "edDtelab"
    Me.edDtelab.NTSDbField = ""
    Me.edDtelab.NTSForzaVisZoom = False
    Me.edDtelab.NTSOldValue = ""
    Me.edDtelab.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDtelab.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDtelab.Properties.AutoHeight = False
    Me.edDtelab.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDtelab.Properties.MaxLength = 65536
    Me.edDtelab.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDtelab.Size = New System.Drawing.Size(81, 20)
    Me.edDtelab.TabIndex = 10
    '
    'ckInvFinale
    '
    Me.ckInvFinale.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckInvFinale.Location = New System.Drawing.Point(466, 43)
    Me.ckInvFinale.Name = "ckInvFinale"
    Me.ckInvFinale.NTSCheckValue = "S"
    Me.ckInvFinale.NTSUnCheckValue = "N"
    Me.ckInvFinale.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckInvFinale.Properties.Appearance.Options.UseBackColor = True
    Me.ckInvFinale.Properties.AutoHeight = False
    Me.ckInvFinale.Properties.Caption = "In&ventario finale"
    Me.ckInvFinale.Size = New System.Drawing.Size(108, 19)
    Me.ckInvFinale.TabIndex = 11
    '
    'fmListini
    '
    Me.fmListini.AllowDrop = True
    Me.fmListini.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmListini.Appearance.Options.UseBackColor = True
    Me.fmListini.Controls.Add(Me.edSalvaListino)
    Me.fmListini.Controls.Add(Me.ckSalvaCostiZero)
    Me.fmListini.Controls.Add(Me.lbSalvaListData)
    Me.fmListini.Controls.Add(Me.edSalvaListData)
    Me.fmListini.Controls.Add(Me.lbSalvaListino)
    Me.fmListini.Controls.Add(Me.ckSalvaListini)
    Me.fmListini.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmListini.Location = New System.Drawing.Point(405, 285)
    Me.fmListini.Name = "fmListini"
    Me.fmListini.Size = New System.Drawing.Size(210, 135)
    Me.fmListini.TabIndex = 12
    '
    'edSalvaListino
    '
    Me.edSalvaListino.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSalvaListino.EditValue = "0"
    Me.edSalvaListino.Location = New System.Drawing.Point(104, 26)
    Me.edSalvaListino.Name = "edSalvaListino"
    Me.edSalvaListino.NTSDbField = ""
    Me.edSalvaListino.NTSFormat = "0"
    Me.edSalvaListino.NTSForzaVisZoom = False
    Me.edSalvaListino.NTSOldValue = ""
    Me.edSalvaListino.Properties.Appearance.Options.UseTextOptions = True
    Me.edSalvaListino.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSalvaListino.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSalvaListino.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSalvaListino.Properties.AutoHeight = False
    Me.edSalvaListino.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSalvaListino.Properties.MaxLength = 65536
    Me.edSalvaListino.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSalvaListino.Size = New System.Drawing.Size(84, 20)
    Me.edSalvaListino.TabIndex = 17
    '
    'ckSalvaCostiZero
    '
    Me.ckSalvaCostiZero.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSalvaCostiZero.Location = New System.Drawing.Point(5, 81)
    Me.ckSalvaCostiZero.Name = "ckSalvaCostiZero"
    Me.ckSalvaCostiZero.NTSCheckValue = "S"
    Me.ckSalvaCostiZero.NTSUnCheckValue = "N"
    Me.ckSalvaCostiZero.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSalvaCostiZero.Properties.Appearance.Options.UseBackColor = True
    Me.ckSalvaCostiZero.Properties.AutoHeight = False
    Me.ckSalvaCostiZero.Properties.Caption = "Salva anche costi pari a 0"
    Me.ckSalvaCostiZero.Size = New System.Drawing.Size(153, 19)
    Me.ckSalvaCostiZero.TabIndex = 16
    '
    'lbSalvaListData
    '
    Me.lbSalvaListData.AutoSize = True
    Me.lbSalvaListData.BackColor = System.Drawing.Color.Transparent
    Me.lbSalvaListData.Location = New System.Drawing.Point(5, 55)
    Me.lbSalvaListData.Name = "lbSalvaListData"
    Me.lbSalvaListData.NTSDbField = ""
    Me.lbSalvaListData.Size = New System.Drawing.Size(93, 13)
    Me.lbSalvaListData.TabIndex = 15
    Me.lbSalvaListData.Text = "Data inizio validità"
    Me.lbSalvaListData.Tooltip = ""
    Me.lbSalvaListData.UseMnemonic = False
    '
    'edSalvaListData
    '
    Me.edSalvaListData.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSalvaListData.EditValue = "01/01/1900"
    Me.edSalvaListData.Location = New System.Drawing.Point(104, 52)
    Me.edSalvaListData.Name = "edSalvaListData"
    Me.edSalvaListData.NTSDbField = ""
    Me.edSalvaListData.NTSForzaVisZoom = False
    Me.edSalvaListData.NTSOldValue = ""
    Me.edSalvaListData.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSalvaListData.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSalvaListData.Properties.AutoHeight = False
    Me.edSalvaListData.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSalvaListData.Properties.MaxLength = 65536
    Me.edSalvaListData.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSalvaListData.Size = New System.Drawing.Size(84, 20)
    Me.edSalvaListData.TabIndex = 14
    '
    'lbSalvaListino
    '
    Me.lbSalvaListino.AutoSize = True
    Me.lbSalvaListino.BackColor = System.Drawing.Color.Transparent
    Me.lbSalvaListino.Location = New System.Drawing.Point(5, 29)
    Me.lbSalvaListino.Name = "lbSalvaListino"
    Me.lbSalvaListino.NTSDbField = ""
    Me.lbSalvaListino.Size = New System.Drawing.Size(37, 13)
    Me.lbSalvaListino.TabIndex = 13
    Me.lbSalvaListino.Text = "Listino"
    Me.lbSalvaListino.Tooltip = ""
    Me.lbSalvaListino.UseMnemonic = False
    '
    'ckSalvaListini
    '
    Me.ckSalvaListini.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSalvaListini.Location = New System.Drawing.Point(5, 0)
    Me.ckSalvaListini.Name = "ckSalvaListini"
    Me.ckSalvaListini.NTSCheckValue = "S"
    Me.ckSalvaListini.NTSUnCheckValue = "N"
    Me.ckSalvaListini.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSalvaListini.Properties.Appearance.Options.UseBackColor = True
    Me.ckSalvaListini.Properties.AutoHeight = False
    Me.ckSalvaListini.Properties.Caption = "Salva costi come listino"
    Me.ckSalvaListini.Size = New System.Drawing.Size(133, 19)
    Me.ckSalvaListini.TabIndex = 12
    '
    'lbGiacenze
    '
    Me.lbGiacenze.AutoSize = True
    Me.lbGiacenze.BackColor = System.Drawing.Color.Transparent
    Me.lbGiacenze.Location = New System.Drawing.Point(12, 72)
    Me.lbGiacenze.Name = "lbGiacenze"
    Me.lbGiacenze.NTSDbField = ""
    Me.lbGiacenze.Size = New System.Drawing.Size(100, 13)
    Me.lbGiacenze.TabIndex = 13
    Me.lbGiacenze.Text = "Considera giacenze"
    Me.lbGiacenze.Tooltip = ""
    Me.lbGiacenze.UseMnemonic = False
    '
    'cbGiacenze
    '
    Me.cbGiacenze.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbGiacenze.DataSource = Nothing
    Me.cbGiacenze.DisplayMember = ""
    Me.cbGiacenze.Location = New System.Drawing.Point(124, 69)
    Me.cbGiacenze.Name = "cbGiacenze"
    Me.cbGiacenze.NTSDbField = ""
    Me.cbGiacenze.Properties.AutoHeight = False
    Me.cbGiacenze.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbGiacenze.Properties.DropDownRows = 30
    Me.cbGiacenze.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbGiacenze.SelectedValue = ""
    Me.cbGiacenze.Size = New System.Drawing.Size(151, 20)
    Me.cbGiacenze.TabIndex = 14
    Me.cbGiacenze.ValueMember = ""
    '
    'ckUsacostiglob
    '
    Me.ckUsacostiglob.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckUsacostiglob.Location = New System.Drawing.Point(115, 115)
    Me.ckUsacostiglob.Name = "ckUsacostiglob"
    Me.ckUsacostiglob.NTSCheckValue = "S"
    Me.ckUsacostiglob.NTSUnCheckValue = "N"
    Me.ckUsacostiglob.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckUsacostiglob.Properties.Appearance.Options.UseBackColor = True
    Me.ckUsacostiglob.Properties.AutoHeight = False
    Me.ckUsacostiglob.Properties.Caption = "Usa Costi ricavati dai movimenti di tutti i magazzini"
    Me.ckUsacostiglob.Size = New System.Drawing.Size(266, 19)
    Me.ckUsacostiglob.TabIndex = 15
    '
    'opLifo
    '
    Me.opLifo.Cursor = System.Windows.Forms.Cursors.Default
    Me.opLifo.Location = New System.Drawing.Point(5, 28)
    Me.opLifo.Name = "opLifo"
    Me.opLifo.NTSCheckValue = "S"
    Me.opLifo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opLifo.Properties.Appearance.Options.UseBackColor = True
    Me.opLifo.Properties.AutoHeight = False
    Me.opLifo.Properties.Caption = "&Lifo"
    Me.opLifo.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opLifo.Size = New System.Drawing.Size(45, 19)
    Me.opLifo.TabIndex = 17
    '
    'opMedio
    '
    Me.opMedio.Cursor = System.Windows.Forms.Cursors.Default
    Me.opMedio.EditValue = True
    Me.opMedio.Location = New System.Drawing.Point(5, 46)
    Me.opMedio.Name = "opMedio"
    Me.opMedio.NTSCheckValue = "S"
    Me.opMedio.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opMedio.Properties.Appearance.Options.UseBackColor = True
    Me.opMedio.Properties.AutoHeight = False
    Me.opMedio.Properties.Caption = "&Costo medio dell'anno"
    Me.opMedio.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opMedio.Size = New System.Drawing.Size(130, 19)
    Me.opMedio.TabIndex = 18
    '
    'opMedioGlobale
    '
    Me.opMedioGlobale.Cursor = System.Windows.Forms.Cursors.Default
    Me.opMedioGlobale.Location = New System.Drawing.Point(5, 64)
    Me.opMedioGlobale.Name = "opMedioGlobale"
    Me.opMedioGlobale.NTSCheckValue = "S"
    Me.opMedioGlobale.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opMedioGlobale.Properties.Appearance.Options.UseBackColor = True
    Me.opMedioGlobale.Properties.AutoHeight = False
    Me.opMedioGlobale.Properties.Caption = "Costo medio &globale"
    Me.opMedioGlobale.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opMedioGlobale.Size = New System.Drawing.Size(121, 19)
    Me.opMedioGlobale.TabIndex = 19
    '
    'opUltimoCosto
    '
    Me.opUltimoCosto.Cursor = System.Windows.Forms.Cursors.Default
    Me.opUltimoCosto.Location = New System.Drawing.Point(5, 81)
    Me.opUltimoCosto.Name = "opUltimoCosto"
    Me.opUltimoCosto.NTSCheckValue = "S"
    Me.opUltimoCosto.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opUltimoCosto.Properties.Appearance.Options.UseBackColor = True
    Me.opUltimoCosto.Properties.AutoHeight = False
    Me.opUltimoCosto.Properties.Caption = "Ultimo c&osto"
    Me.opUltimoCosto.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opUltimoCosto.Size = New System.Drawing.Size(84, 19)
    Me.opUltimoCosto.TabIndex = 20
    '
    'opListino
    '
    Me.opListino.Cursor = System.Windows.Forms.Cursors.Default
    Me.opListino.Location = New System.Drawing.Point(5, 100)
    Me.opListino.Name = "opListino"
    Me.opListino.NTSCheckValue = "S"
    Me.opListino.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opListino.Properties.Appearance.Options.UseBackColor = True
    Me.opListino.Properties.AutoHeight = False
    Me.opListino.Properties.Caption = "Li&stino"
    Me.opListino.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opListino.Size = New System.Drawing.Size(69, 19)
    Me.opListino.TabIndex = 21
    '
    'opFifo
    '
    Me.opFifo.Cursor = System.Windows.Forms.Cursors.Default
    Me.opFifo.Location = New System.Drawing.Point(5, 117)
    Me.opFifo.Name = "opFifo"
    Me.opFifo.NTSCheckValue = "S"
    Me.opFifo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opFifo.Properties.Appearance.Options.UseBackColor = True
    Me.opFifo.Properties.AutoHeight = False
    Me.opFifo.Properties.Caption = "&Fifo"
    Me.opFifo.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opFifo.Size = New System.Drawing.Size(45, 19)
    Me.opFifo.TabIndex = 22
    '
    'opUltimoCostoacc
    '
    Me.opUltimoCostoacc.Cursor = System.Windows.Forms.Cursors.Default
    Me.opUltimoCostoacc.Location = New System.Drawing.Point(5, 136)
    Me.opUltimoCostoacc.Name = "opUltimoCostoacc"
    Me.opUltimoCostoacc.NTSCheckValue = "S"
    Me.opUltimoCostoacc.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opUltimoCostoacc.Properties.Appearance.Options.UseBackColor = True
    Me.opUltimoCostoacc.Properties.AutoHeight = False
    Me.opUltimoCostoacc.Properties.Caption = "&Ultimo costo compreso di oneri accessori"
    Me.opUltimoCostoacc.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opUltimoCostoacc.Size = New System.Drawing.Size(218, 19)
    Me.opUltimoCostoacc.TabIndex = 23
    '
    'opInventarioFinale
    '
    Me.opInventarioFinale.Cursor = System.Windows.Forms.Cursors.Default
    Me.opInventarioFinale.Location = New System.Drawing.Point(5, 155)
    Me.opInventarioFinale.Name = "opInventarioFinale"
    Me.opInventarioFinale.NTSCheckValue = "S"
    Me.opInventarioFinale.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opInventarioFinale.Properties.Appearance.Options.UseBackColor = True
    Me.opInventarioFinale.Properties.AutoHeight = False
    Me.opInventarioFinale.Properties.Caption = "Come da inventario finale"
    Me.opInventarioFinale.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opInventarioFinale.Size = New System.Drawing.Size(145, 19)
    Me.opInventarioFinale.TabIndex = 24
    '
    'ckLifoAnniPrec
    '
    Me.ckLifoAnniPrec.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckLifoAnniPrec.Location = New System.Drawing.Point(202, 28)
    Me.ckLifoAnniPrec.Name = "ckLifoAnniPrec"
    Me.ckLifoAnniPrec.NTSCheckValue = "S"
    Me.ckLifoAnniPrec.NTSUnCheckValue = "N"
    Me.ckLifoAnniPrec.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckLifoAnniPrec.Properties.Appearance.Options.UseBackColor = True
    Me.ckLifoAnniPrec.Properties.AutoHeight = False
    Me.ckLifoAnniPrec.Properties.Caption = "Stam&pa dati anni precedenti"
    Me.ckLifoAnniPrec.Size = New System.Drawing.Size(162, 19)
    Me.ckLifoAnniPrec.TabIndex = 25
    '
    'opMagazUno
    '
    Me.opMagazUno.Cursor = System.Windows.Forms.Cursors.Default
    Me.opMagazUno.Location = New System.Drawing.Point(8, 96)
    Me.opMagazUno.Name = "opMagazUno"
    Me.opMagazUno.NTSCheckValue = "S"
    Me.opMagazUno.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opMagazUno.Properties.Appearance.Options.UseBackColor = True
    Me.opMagazUno.Properties.AutoHeight = False
    Me.opMagazUno.Properties.Caption = "&Un magazzino"
    Me.opMagazUno.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opMagazUno.Size = New System.Drawing.Size(97, 19)
    Me.opMagazUno.TabIndex = 29
    '
    'opMagazTutti
    '
    Me.opMagazTutti.Cursor = System.Windows.Forms.Cursors.Default
    Me.opMagazTutti.Location = New System.Drawing.Point(8, 55)
    Me.opMagazTutti.Name = "opMagazTutti"
    Me.opMagazTutti.NTSCheckValue = "S"
    Me.opMagazTutti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opMagazTutti.Properties.Appearance.Options.UseBackColor = True
    Me.opMagazTutti.Properties.AutoHeight = False
    Me.opMagazTutti.Properties.Caption = "&Tutti i magazzini"
    Me.opMagazTutti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opMagazTutti.Size = New System.Drawing.Size(107, 19)
    Me.opMagazTutti.TabIndex = 28
    '
    'opMagazAltrui
    '
    Me.opMagazAltrui.Cursor = System.Windows.Forms.Cursors.Default
    Me.opMagazAltrui.Location = New System.Drawing.Point(8, 38)
    Me.opMagazAltrui.Name = "opMagazAltrui"
    Me.opMagazAltrui.NTSCheckValue = "S"
    Me.opMagazAltrui.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opMagazAltrui.Properties.Appearance.Options.UseBackColor = True
    Me.opMagazAltrui.Properties.AutoHeight = False
    Me.opMagazAltrui.Properties.Caption = "M&erce altrui"
    Me.opMagazAltrui.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opMagazAltrui.Size = New System.Drawing.Size(84, 19)
    Me.opMagazAltrui.TabIndex = 27
    '
    'opMagMerceProp
    '
    Me.opMagMerceProp.Cursor = System.Windows.Forms.Cursors.Default
    Me.opMagMerceProp.EditValue = True
    Me.opMagMerceProp.Location = New System.Drawing.Point(8, 20)
    Me.opMagMerceProp.Name = "opMagMerceProp"
    Me.opMagMerceProp.NTSCheckValue = "S"
    Me.opMagMerceProp.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opMagMerceProp.Properties.Appearance.Options.UseBackColor = True
    Me.opMagMerceProp.Properties.AutoHeight = False
    Me.opMagMerceProp.Properties.Caption = "&Merce propria"
    Me.opMagMerceProp.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opMagMerceProp.Size = New System.Drawing.Size(97, 19)
    Me.opMagMerceProp.TabIndex = 26
    '
    'fmValorizzazione
    '
    Me.fmValorizzazione.AllowDrop = True
    Me.fmValorizzazione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmValorizzazione.Appearance.Options.UseBackColor = True
    Me.fmValorizzazione.Controls.Add(Me.ckUsaListForn)
    Me.fmValorizzazione.Controls.Add(Me.ckSoloPrezziListino)
    Me.fmValorizzazione.Controls.Add(Me.edListino)
    Me.fmValorizzazione.Controls.Add(Me.opLifo)
    Me.fmValorizzazione.Controls.Add(Me.opMedio)
    Me.fmValorizzazione.Controls.Add(Me.ckLifoAnniPrec)
    Me.fmValorizzazione.Controls.Add(Me.opMedioGlobale)
    Me.fmValorizzazione.Controls.Add(Me.opUltimoCosto)
    Me.fmValorizzazione.Controls.Add(Me.opListino)
    Me.fmValorizzazione.Controls.Add(Me.opFifo)
    Me.fmValorizzazione.Controls.Add(Me.opInventarioFinale)
    Me.fmValorizzazione.Controls.Add(Me.opUltimoCostoacc)
    Me.fmValorizzazione.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmValorizzazione.Location = New System.Drawing.Point(7, 95)
    Me.fmValorizzazione.Name = "fmValorizzazione"
    Me.fmValorizzazione.Size = New System.Drawing.Size(608, 183)
    Me.fmValorizzazione.TabIndex = 30
    Me.fmValorizzazione.Text = "Tipo valorizzazione"
    '
    'ckUsaListForn
    '
    Me.ckUsaListForn.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckUsaListForn.Location = New System.Drawing.Point(403, 100)
    Me.ckUsaListForn.Name = "ckUsaListForn"
    Me.ckUsaListForn.NTSCheckValue = "S"
    Me.ckUsaListForn.NTSUnCheckValue = "N"
    Me.ckUsaListForn.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckUsaListForn.Properties.Appearance.Options.UseBackColor = True
    Me.ckUsaListForn.Properties.AutoHeight = False
    Me.ckUsaListForn.Properties.Caption = "Usa listino Fornitore, se presente"
    Me.ckUsaListForn.Size = New System.Drawing.Size(189, 19)
    Me.ckUsaListForn.TabIndex = 28
    '
    'ckSoloPrezziListino
    '
    Me.ckSoloPrezziListino.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSoloPrezziListino.Location = New System.Drawing.Point(202, 100)
    Me.ckSoloPrezziListino.Name = "ckSoloPrezziListino"
    Me.ckSoloPrezziListino.NTSCheckValue = "S"
    Me.ckSoloPrezziListino.NTSUnCheckValue = "N"
    Me.ckSoloPrezziListino.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSoloPrezziListino.Properties.Appearance.Options.UseBackColor = True
    Me.ckSoloPrezziListino.Properties.AutoHeight = False
    Me.ckSoloPrezziListino.Properties.Caption = "U&sa solo prezzi da listino"
    Me.ckSoloPrezziListino.Size = New System.Drawing.Size(147, 19)
    Me.ckSoloPrezziListino.TabIndex = 27
    '
    'edListino
    '
    Me.edListino.Cursor = System.Windows.Forms.Cursors.Default
    Me.edListino.EditValue = "0"
    Me.edListino.Location = New System.Drawing.Point(117, 99)
    Me.edListino.Name = "edListino"
    Me.edListino.NTSDbField = ""
    Me.edListino.NTSFormat = "0"
    Me.edListino.NTSForzaVisZoom = False
    Me.edListino.NTSOldValue = ""
    Me.edListino.Properties.Appearance.Options.UseTextOptions = True
    Me.edListino.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edListino.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edListino.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edListino.Properties.AutoHeight = False
    Me.edListino.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edListino.Properties.MaxLength = 65536
    Me.edListino.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edListino.Size = New System.Drawing.Size(56, 20)
    Me.edListino.TabIndex = 26
    '
    'fmMagazzini
    '
    Me.fmMagazzini.AllowDrop = True
    Me.fmMagazzini.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmMagazzini.Appearance.Options.UseBackColor = True
    Me.fmMagazzini.Controls.Add(Me.lbNegozio)
    Me.fmMagazzini.Controls.Add(Me.edNegozio)
    Me.fmMagazzini.Controls.Add(Me.opMagazNegozio)
    Me.fmMagazzini.Controls.Add(Me.opMagMerceProp)
    Me.fmMagazzini.Controls.Add(Me.opMagazAltrui)
    Me.fmMagazzini.Controls.Add(Me.opMagazUno)
    Me.fmMagazzini.Controls.Add(Me.ckUsacostiglob)
    Me.fmMagazzini.Controls.Add(Me.opMagazTutti)
    Me.fmMagazzini.Controls.Add(Me.edMagaz)
    Me.fmMagazzini.Controls.Add(Me.lbMagaz)
    Me.fmMagazzini.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmMagazzini.Location = New System.Drawing.Point(7, 284)
    Me.fmMagazzini.Name = "fmMagazzini"
    Me.fmMagazzini.Size = New System.Drawing.Size(392, 136)
    Me.fmMagazzini.TabIndex = 31
    Me.fmMagazzini.Text = "Magazzini da elaborare"
    '
    'lbNegozio
    '
    Me.lbNegozio.BackColor = System.Drawing.Color.Transparent
    Me.lbNegozio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbNegozio.Location = New System.Drawing.Point(179, 73)
    Me.lbNegozio.Name = "lbNegozio"
    Me.lbNegozio.NTSDbField = ""
    Me.lbNegozio.Size = New System.Drawing.Size(204, 20)
    Me.lbNegozio.TabIndex = 32
    Me.lbNegozio.Tooltip = ""
    Me.lbNegozio.UseMnemonic = False
    '
    'edNegozio
    '
    Me.edNegozio.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNegozio.EditValue = "0"
    Me.edNegozio.Location = New System.Drawing.Point(117, 73)
    Me.edNegozio.Name = "edNegozio"
    Me.edNegozio.NTSDbField = ""
    Me.edNegozio.NTSFormat = "0"
    Me.edNegozio.NTSForzaVisZoom = False
    Me.edNegozio.NTSOldValue = ""
    Me.edNegozio.Properties.Appearance.Options.UseTextOptions = True
    Me.edNegozio.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNegozio.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNegozio.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNegozio.Properties.AutoHeight = False
    Me.edNegozio.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNegozio.Properties.MaxLength = 65536
    Me.edNegozio.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNegozio.Size = New System.Drawing.Size(56, 20)
    Me.edNegozio.TabIndex = 31
    '
    'opMagazNegozio
    '
    Me.opMagazNegozio.Cursor = System.Windows.Forms.Cursors.Default
    Me.opMagazNegozio.Location = New System.Drawing.Point(8, 74)
    Me.opMagazNegozio.Name = "opMagazNegozio"
    Me.opMagazNegozio.NTSCheckValue = "S"
    Me.opMagazNegozio.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opMagazNegozio.Properties.Appearance.Options.UseBackColor = True
    Me.opMagazNegozio.Properties.AutoHeight = False
    Me.opMagazNegozio.Properties.Caption = "Uno stabil/fil/neg."
    Me.opMagazNegozio.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opMagazNegozio.Size = New System.Drawing.Size(107, 19)
    Me.opMagazNegozio.TabIndex = 30
    '
    'ckDettaglioTCO
    '
    Me.ckDettaglioTCO.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDettaglioTCO.Location = New System.Drawing.Point(7, 493)
    Me.ckDettaglioTCO.Name = "ckDettaglioTCO"
    Me.ckDettaglioTCO.NTSCheckValue = "S"
    Me.ckDettaglioTCO.NTSUnCheckValue = "N"
    Me.ckDettaglioTCO.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDettaglioTCO.Properties.Appearance.Options.UseBackColor = True
    Me.ckDettaglioTCO.Properties.AutoHeight = False
    Me.ckDettaglioTCO.Properties.Caption = "Stampa dettaglio quantità per taglie"
    Me.ckDettaglioTCO.Size = New System.Drawing.Size(205, 19)
    Me.ckDettaglioTCO.TabIndex = 32
    '
    'cmdSelArt
    '
    Me.cmdSelArt.ImagePath = ""
    Me.cmdSelArt.ImageText = ""
    Me.cmdSelArt.Location = New System.Drawing.Point(453, 493)
    Me.cmdSelArt.Name = "cmdSelArt"
    Me.cmdSelArt.NTSContextMenu = Nothing
    Me.cmdSelArt.Size = New System.Drawing.Size(162, 27)
    Me.cmdSelArt.TabIndex = 33
    Me.cmdSelArt.Text = "Seleziona articoli"
    '
    'lbStatus
    '
    Me.lbStatus.AutoSize = True
    Me.lbStatus.BackColor = System.Drawing.Color.Transparent
    Me.lbStatus.Location = New System.Drawing.Point(14, 524)
    Me.lbStatus.Name = "lbStatus"
    Me.lbStatus.NTSDbField = ""
    Me.lbStatus.Size = New System.Drawing.Size(43, 13)
    Me.lbStatus.TabIndex = 34
    Me.lbStatus.Text = "Pronto."
    Me.lbStatus.Tooltip = ""
    Me.lbStatus.UseMnemonic = False
    '
    'ckEscludiArticoliNonMov
    '
    Me.ckEscludiArticoliNonMov.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.ckEscludiArticoliNonMov.Location = New System.Drawing.Point(290, 70)
    Me.ckEscludiArticoliNonMov.Name = "ckEscludiArticoliNonMov"
    Me.ckEscludiArticoliNonMov.NTSCheckValue = "S"
    Me.ckEscludiArticoliNonMov.NTSUnCheckValue = "N"
    Me.ckEscludiArticoliNonMov.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckEscludiArticoliNonMov.Properties.Appearance.Options.UseBackColor = True
    Me.ckEscludiArticoliNonMov.Properties.AutoHeight = False
    Me.ckEscludiArticoliNonMov.Properties.Caption = "Escludi articoli mai movimentati"
    Me.ckEscludiArticoliNonMov.Size = New System.Drawing.Size(175, 19)
    Me.ckEscludiArticoliNonMov.TabIndex = 35
    '
    'fmGeneraListaSelezionata
    '
    Me.fmGeneraListaSelezionata.AllowDrop = True
    Me.fmGeneraListaSelezionata.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmGeneraListaSelezionata.Appearance.Options.UseBackColor = True
    Me.fmGeneraListaSelezionata.Controls.Add(Me.ckQta0)
    Me.fmGeneraListaSelezionata.Controls.Add(Me.edCodlsar)
    Me.fmGeneraListaSelezionata.Controls.Add(Me.edDeslsar)
    Me.fmGeneraListaSelezionata.Controls.Add(Me.ckGeneraListaSelezionata)
    Me.fmGeneraListaSelezionata.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmGeneraListaSelezionata.Location = New System.Drawing.Point(7, 426)
    Me.fmGeneraListaSelezionata.Name = "fmGeneraListaSelezionata"
    Me.fmGeneraListaSelezionata.Size = New System.Drawing.Size(608, 52)
    Me.fmGeneraListaSelezionata.TabIndex = 36
    '
    'ckQta0
    '
    Me.ckQta0.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckQta0.Enabled = False
    Me.ckQta0.Location = New System.Drawing.Point(478, 25)
    Me.ckQta0.Name = "ckQta0"
    Me.ckQta0.NTSCheckValue = "S"
    Me.ckQta0.NTSUnCheckValue = "N"
    Me.ckQta0.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckQta0.Properties.Appearance.Options.UseBackColor = True
    Me.ckQta0.Properties.AutoHeight = False
    Me.ckQta0.Properties.Caption = "Forza quantità a 0"
    Me.ckQta0.Size = New System.Drawing.Size(125, 20)
    Me.ckQta0.TabIndex = 60
    '
    'edCodlsar
    '
    Me.edCodlsar.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodlsar.EditValue = "0"
    Me.edCodlsar.Enabled = False
    Me.edCodlsar.Location = New System.Drawing.Point(36, 25)
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
    Me.edCodlsar.Size = New System.Drawing.Size(73, 20)
    Me.edCodlsar.TabIndex = 58
    '
    'edDeslsar
    '
    Me.edDeslsar.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDeslsar.Enabled = False
    Me.edDeslsar.Location = New System.Drawing.Point(115, 25)
    Me.edDeslsar.Name = "edDeslsar"
    Me.edDeslsar.NTSDbField = ""
    Me.edDeslsar.NTSForzaVisZoom = False
    Me.edDeslsar.NTSOldValue = ""
    Me.edDeslsar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDeslsar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDeslsar.Properties.AutoHeight = False
    Me.edDeslsar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDeslsar.Properties.MaxLength = 65536
    Me.edDeslsar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDeslsar.Size = New System.Drawing.Size(357, 20)
    Me.edDeslsar.TabIndex = 59
    '
    'ckGeneraListaSelezionata
    '
    Me.ckGeneraListaSelezionata.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckGeneraListaSelezionata.Location = New System.Drawing.Point(5, 0)
    Me.ckGeneraListaSelezionata.Name = "ckGeneraListaSelezionata"
    Me.ckGeneraListaSelezionata.NTSCheckValue = "S"
    Me.ckGeneraListaSelezionata.NTSUnCheckValue = "N"
    Me.ckGeneraListaSelezionata.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckGeneraListaSelezionata.Properties.Appearance.Options.UseBackColor = True
    Me.ckGeneraListaSelezionata.Properties.AutoHeight = False
    Me.ckGeneraListaSelezionata.Properties.Caption = "Genera Lista Selezionata Articoli"
    Me.ckGeneraListaSelezionata.Size = New System.Drawing.Size(178, 19)
    Me.ckGeneraListaSelezionata.TabIndex = 12
    '
    'ckTcoEsitTaglia
    '
    Me.ckTcoEsitTaglia.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.ckTcoEsitTaglia.Location = New System.Drawing.Point(466, 70)
    Me.ckTcoEsitTaglia.Name = "ckTcoEsitTaglia"
    Me.ckTcoEsitTaglia.NTSCheckValue = "S"
    Me.ckTcoEsitTaglia.NTSUnCheckValue = "N"
    Me.ckTcoEsitTaglia.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTcoEsitTaglia.Properties.Appearance.Options.UseBackColor = True
    Me.ckTcoEsitTaglia.Properties.AutoHeight = False
    Me.ckTcoEsitTaglia.Properties.Caption = "TCO usa giacenza x taglia"
    Me.ckTcoEsitTaglia.Size = New System.Drawing.Size(149, 19)
    Me.ckTcoEsitTaglia.TabIndex = 37
    '
    'FRMMGSTRL
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(620, 546)
    Me.Controls.Add(Me.ckTcoEsitTaglia)
    Me.Controls.Add(Me.fmGeneraListaSelezionata)
    Me.Controls.Add(Me.ckEscludiArticoliNonMov)
    Me.Controls.Add(Me.lbStatus)
    Me.Controls.Add(Me.cmdSelArt)
    Me.Controls.Add(Me.ckDettaglioTCO)
    Me.Controls.Add(Me.fmMagazzini)
    Me.Controls.Add(Me.fmValorizzazione)
    Me.Controls.Add(Me.cbGiacenze)
    Me.Controls.Add(Me.lbGiacenze)
    Me.Controls.Add(Me.fmListini)
    Me.Controls.Add(Me.ckInvFinale)
    Me.Controls.Add(Me.edDtelab)
    Me.Controls.Add(Me.cbTipoElab)
    Me.Controls.Add(Me.lbTipoElab)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRMMGSTRL"
    Me.Text = "STAMPA INVENTARIO DI MAGAZZINO"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edMagaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipoElab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDtelab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckInvFinale.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmListini, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmListini.ResumeLayout(False)
    Me.fmListini.PerformLayout()
    CType(Me.edSalvaListino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSalvaCostiZero.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSalvaListData.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSalvaListini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbGiacenze.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckUsacostiglob.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opLifo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opMedio.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opMedioGlobale.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opUltimoCosto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opListino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opFifo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opUltimoCostoacc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opInventarioFinale.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckLifoAnniPrec.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opMagazUno.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opMagazTutti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opMagazAltrui.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opMagMerceProp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmValorizzazione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmValorizzazione.ResumeLayout(False)
    Me.fmValorizzazione.PerformLayout()
    CType(Me.ckUsaListForn.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSoloPrezziListino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edListino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmMagazzini, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmMagazzini.ResumeLayout(False)
    Me.fmMagazzini.PerformLayout()
    CType(Me.edNegozio.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opMagazNegozio.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDettaglioTCO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckEscludiArticoliNonMov.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmGeneraListaSelezionata, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmGeneraListaSelezionata.ResumeLayout(False)
    Me.fmGeneraListaSelezionata.PerformLayout()
    CType(Me.ckQta0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodlsar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDeslsar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckGeneraListaSelezionata.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTcoEsitTaglia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGSTRL", "BEMGSTRL", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128496233436616000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleStrl = CType(oTmp, CLEMGSTRL)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGSTRL", strRemoteServer, strRemotePort)
    AddHandler oCleStrl.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleStrl.Init(oApp, oScript, oMenu.oCleComm, "TABLING", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

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
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      Dim dttTipo As New DataTable()
      dttTipo.Columns.Add("cod", GetType(Short))
      dttTipo.Columns.Add("val", GetType(String))
      dttTipo.Rows.Add(New Object() {0, "Situazione attuale"})
      dttTipo.Rows.Add(New Object() {1, "A data"})
      dttTipo.Rows.Add(New Object() {2, oApp.Tr(Me, 129065470877703928, "A data ultimo aggiornamento |" & NTSCDate(oCleStrl.dttTabanaz.Rows(0)!tb_dtulap).ToShortDateString & "|")})
      dttTipo.AcceptChanges()
      cbTipoElab.DataSource = dttTipo
      cbTipoElab.ValueMember = "cod"
      cbTipoElab.DisplayMember = "val"

      Dim dttGiacenze As New DataTable()
      dttGiacenze.Columns.Add("cod", GetType(Short))
      dttGiacenze.Columns.Add("val", GetType(String))
      dttGiacenze.Rows.Add(New Object() {0, "Solo giacenze > 0"})
      dttGiacenze.Rows.Add(New Object() {1, "Solo giacenze < 0"})
      dttGiacenze.Rows.Add(New Object() {2, "Solo giacenze <> 0"})
      dttGiacenze.Rows.Add(New Object() {3, "Qualsiasi giacenza"})
      dttGiacenze.Rows.Add(New Object() {4, "Solo movimentati con giacenza = 0"})
      dttGiacenze.AcceptChanges()
      cbGiacenze.DataSource = dttGiacenze
      cbGiacenze.ValueMember = "cod"
      cbGiacenze.DisplayMember = "val"

      ckEscludiArticoliNonMov.NTSSetParam(oMenu, oApp.Tr(Me, 129054241056105050, "Escludi articoli mai movimentati"), "S", "N")
      ckTcoEsitTaglia.NTSSetParam(oMenu, oApp.Tr(Me, 129054241056105051, "TCO usa giacenza per taglia"), "S", "N")
      ckDettaglioTCO.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952394000, "Stampa dettaglio quantità per taglie"), "S", "N")
      opMagMerceProp.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952404000, "Merce propria"), "P")
      opMagazAltrui.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952414000, "Merce altrui"), "A")
      opMagazTutti.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952444000, "Tutti i magazzini"), "T")
      opMagazNegozio.NTSSetParam(oMenu, oApp.Tr(Me, 129865740043591089, "Uno Stafilimento/filiale/negozio"), "N")
      opMagazUno.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952424000, "Un magazzino"), "U")
      ckUsacostiglob.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952434000, "Usa Costi ricavati dai movimenti di tutti i magazzini"), "S", "N")
      edMagaz.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128497224952454000, "Magazzino"), tabmaga)
      edNegozio.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129865743083464434, "Stabilimento/filiale/negozio"), tabstab)
      ckUsaListForn.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952464000, "Usa listino Fornitore, se presente"), "S", "N")
      ckSoloPrezziListino.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952474000, "Usa solo prezzi da listino"), "S", "N")
      edListino.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128497224952484000, "Listino"), tablist)
      opLifo.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952494000, "Lifo"), "L")
      opMedio.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952504000, "Costo medio dell'anno"), "A")
      ckLifoAnniPrec.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952514000, "Stampa dati anni precedenti"), "S", "N")
      opMedioGlobale.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952524000, "Costo medio globale"), "G")
      opUltimoCosto.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952534000, "Ultimo costo"), "U")
      opListino.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952544000, "Listino"), "L")
      opFifo.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952554000, "Fifo"), "F")
      opInventarioFinale.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952564000, "Come da inventario finale"), "X")
      opUltimoCostoacc.NTSSetParam(oMenu, oApp.Tr(Me, 128831306803155815, "Ultimo costo compreso di oneri accessori"), "Y")
      cbGiacenze.NTSSetParam(oApp.Tr(Me, 128497224952574000, "Giacenze"))
      edSalvaListino.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128497224952594000, "Listino da salvare"), tablist)
      ckSalvaCostiZero.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952604000, "Salva anche costi pari a 0"), "S", "N")
      edSalvaListData.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952624000, "Data listino"), False)
      ckSalvaListini.NTSSetParam(oMenu, oApp.Tr(Me, 128831306916437790, "Salva costi come listino"), "S", "N")
      ckInvFinale.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952644000, "Inventario finale"), "S", "N")
      edDtelab.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952654000, "Data elaborazione"), False)
      cbTipoElab.NTSSetParam(oApp.Tr(Me, 128497224952664000, "Tipo elaborazione"))
      ckGeneraListaSelezionata.NTSSetParam(oMenu, oApp.Tr(Me, 129055363015271185, "Genera Lista Selezionata Articoli"), "S", "N")
      'edCodlsar.NTSSetParam(oMenu, oApp.Tr(Me, 129055364670917170, "Codice Lista Selezionata"), "0", 4, 0, 9999)
      edCodlsar.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129055364670917170, "Codice Lista Selezionata"), tablsar)
      edDeslsar.NTSSetParam(oMenu, oApp.Tr(Me, 129055365653617945, "Descrizione Lista Selezionata"), 50)

      'edCodlsar.NTSSetParamZoom("")
      edDeslsar.NTSSetParamZoom("")

      cbTipoElab.NTSSetRichiesto()

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

#Region "Eventi di FORM"
  Public Overridable Sub FRMMGSTRL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try

      oCleStrl.bModTCO = CBool(oCleStrl.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCO)

      If Not oCleStrl.LeggiDatiDitta(DittaCorrente) Then
        Me.Close()
        Return
      End If

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      oCleStrl.bCheckArtlif = CBool(oMenu.GetSettingBus("BSMGSTRL", "OPZIONI", ".", "CheckArtlif", "0", " ", "0"))

      edListino.Text = "1"
      edSalvaListino.Text = "1"
      edDtelab.Text = NTSCDate(oCleStrl.dttTabanaz.Rows(0)!tb_dtulap).AddDays(1).ToShortDateString
      edSalvaListData.Text = DateTime.Now.ToShortDateString
      cbGiacenze.SelectedValue = "2"
      lbStatus.Text = oApp.Tr(Me, 128831307018313442, "Pronto.")

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      GctlApplicaDefaultValue()

      BloccaSbloccaControlli()

      If oCleStrl.bModTCO = False Then
        ckDettaglioTCO.Checked = False
        ckDettaglioTCO.Visible = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGSTRL_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    Try
      oMenu.ResetTblInstId("TTINVENT", False, oCleStrl.lIITTinvent)
      oMenu.ResetTblInstId("TTINVENT", False, oCleStrl.lIITTinvent * -1)
      oMenu.ResetTblInstId("TTINVEN2", False, oCleStrl.lIITTinvent2)
      oMenu.ResetTblInstId("TTINVEN2", False, oCleStrl.lIITTinvent2 * -1)
      If oCleStrl.bModTCO Then
        oMenu.ResetTblInstId("TTINVENTTC", False, oCleStrl.lIITTinventTC)
        oMenu.ResetTblInstId("TTINVENTTC", False, oCleStrl.lIITTinventTC * -1)
      End If
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
      'zoom standard
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      '------------------------------------
      'zoom standard di textbox e griglia
      NTSCallStandardZoom()
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

  Public Overridable Sub tlbStampaGriglia_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaGriglia.ItemClick
    Dim frmGri As FRMMGGRIN = Nothing
    Dim dsTmp As New DataSet
    Dim dValore As Decimal = 0
    Dim strDtElab As String = DateTime.Now.ToShortDateString
    Dim strlValorizz As String = ""
    Dim strMagazzini As String = ""
    Dim i As Integer = 0

    Try
      '--------------------------------------------------
      Select Case NTSCInt(cbTipoElab.SelectedValue)
        Case 0 : strDtElab = DateTime.Now.ToShortDateString
        Case 1 : strDtElab = edDtelab.Text
        Case 2 : strDtElab = NTSCDate(oCleStrl.dttTabanaz.Rows(0)!tb_dtulap).ToShortDateString()
      End Select

      If opMagMerceProp.Checked Then strMagazzini = opMagMerceProp.Text
      If opMagazAltrui.Checked Then strMagazzini = opMagazAltrui.Text
      If opMagazTutti.Checked Then strMagazzini = opMagazTutti.Text
      If opMagazNegozio.Checked Then strMagazzini = opMagazNegozio.Text & ": " & edNegozio.Text & " - " & lbNegozio.Text
      If opMagazUno.Checked Then strMagazzini = opMagazUno.Text & ": " & edMagaz.Text & " - " & lbMagaz.Text

      If opUltimoCosto.Checked Then strlValorizz = opUltimoCosto.Text
      If opLifo.Checked Then strlValorizz = opLifo.Text
      If opMedio.Checked Then strlValorizz = opMedio.Text
      If opMedioGlobale.Checked Then strlValorizz = opMedioGlobale.Text
      If opFifo.Checked Then strlValorizz = opFifo.Text
      If opUltimoCostoacc.Checked Then strlValorizz = opUltimoCostoacc.Text
      If opInventarioFinale.Checked Then strlValorizz = opInventarioFinale.Text
      If opListino.Checked Then strlValorizz = opListino.Text & " " & edListino.Text

      strMagazzini = strMagazzini.Replace("&", "")
      strlValorizz = strlValorizz.Replace("&", "")

      If Not Elabora(0) Then Return
      dValore = 0
      If Not oCleStrl.CountTtinvent(i, dValore) Then Return
      If i > 1500000 And CLN__STD.IsWin64Bit = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130715756625767914, "Attenzione: la stampa su griglia di più di 1.500.000 articoli è consentita solo con la versione 64 Bit"))
        Return
      End If
      If Not oCleStrl.CaricaTtinvent(dsTmp) Then Return
      'For i = 0 To dsTmp.Tables("TTINVENT").Rows.Count - 1
      '  dValore += NTSCDec(dsTmp.Tables("TTINVENT").Rows(i)!in_vesist)
      'Next

      lbStatus.Text = oApp.Tr(Me, 128831307047688630, "Pronto.")
      Me.Cursor = Cursors.WaitCursor

      frmGri = CType(NTSNewFormModal("FRMMGGRIN"), FRMMGGRIN)
      frmGri.Init(oMenu, Nothing, DittaCorrente, Nothing)
      frmGri.Initentity(oCleStrl)
      frmGri.lbTipoelab.Text = cbTipoElab.Text & "    " & strDtElab
      frmGri.lbTipomagaz.Text = strMagazzini
      frmGri.lbTipomerce.Text = cbGiacenze.Text
      frmGri.lbTipval.Text = strlValorizz
      frmGri.lbValore.Text = dValore.ToString(oApp.FormatImporti)
      frmGri.dsGrin = dsTmp
      Me.Cursor = Cursors.Default
      frmGri.ShowDialog()
      frmGri.Dispose()
      frmGri = Nothing
      dsTmp.Clear()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
      lbStatus.Text = oApp.Tr(Me, 128498073391304728, "Pronto.")
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

  Public Sub tlbElabMultiMagaz_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbElabMultiMagaz.ItemClick
    If CheckCondition() Then
      'Apro form per elaborazione multi magazzino
      PrintMultiWarehouse()
    End If
  End Sub
#End Region

#Region "Eventi per abilita/disabilita controlli"
  Public Overridable Sub cbTipoElab_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTipoElab.SelectedValueChanged
    Try
      If bInBloccaControlli = False Then BloccaSbloccaControlli()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cbGiacenze_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbGiacenze.SelectedValueChanged
    Try
      Select Case cbGiacenze.SelectedValue
        Case "3"
          GctlSetVisEnab(ckEscludiArticoliNonMov, False)
          ckTcoEsitTaglia.Checked = False
          ckTcoEsitTaglia.Enabled = False
        Case "4"
          'non ha senso, stamperebbe sempre tutto, visto che basta che una taglia non sia mai stata movimentata verrebbe soddisfatta la condizione
          ckTcoEsitTaglia.Checked = False
          ckTcoEsitTaglia.Enabled = False
        Case Else
          ckEscludiArticoliNonMov.Checked = False
          ckEscludiArticoliNonMov.Enabled = False
          GctlSetVisEnab(ckTcoEsitTaglia, False)
      End Select
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub opLifo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opLifo.CheckedChanged
    Try
      If opLifo.Checked And bInBloccaControlli = False Then BloccaSbloccaControlli()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub opMedio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opMedio.CheckedChanged
    Try
      If opMedio.Checked And bInBloccaControlli = False Then BloccaSbloccaControlli()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub opMedioGlobale_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opMedioGlobale.CheckedChanged
    Try
      If opMedioGlobale.Checked And bInBloccaControlli = False Then BloccaSbloccaControlli()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub opUltimoCosto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opUltimoCosto.CheckedChanged
    Try
      If opUltimoCosto.Checked And bInBloccaControlli = False Then BloccaSbloccaControlli()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub opListino_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opListino.CheckedChanged
    Try
      If opListino.Checked And bInBloccaControlli = False Then BloccaSbloccaControlli()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub opFifo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opFifo.CheckedChanged
    Try
      If opFifo.Checked And bInBloccaControlli = False Then BloccaSbloccaControlli()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub opUltimoCostoacc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opUltimoCostoacc.CheckedChanged
    Try
      If opUltimoCostoacc.Checked And bInBloccaControlli = False Then BloccaSbloccaControlli()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub opInventarioFinale_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opInventarioFinale.CheckedChanged
    Try
      If opInventarioFinale.Checked And bInBloccaControlli = False Then BloccaSbloccaControlli()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub opMagMerceProp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opMagMerceProp.CheckedChanged
    Try
      If opMagMerceProp.Checked And bInBloccaControlli = False Then BloccaSbloccaControlli()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub opMagazAltrui_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opMagazAltrui.CheckedChanged
    Try
      If opMagazAltrui.Checked And bInBloccaControlli = False Then BloccaSbloccaControlli()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub opMagazTutti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opMagazTutti.CheckedChanged
    Try
      If opMagazTutti.Checked And bInBloccaControlli = False Then BloccaSbloccaControlli()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub opMagazNegozio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opMagazNegozio.CheckedChanged
    Try
      If opMagazNegozio.Checked And bInBloccaControlli = False Then BloccaSbloccaControlli()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub opMagazUno_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opMagazUno.CheckedChanged
    Try
      If opMagazUno.Checked And bInBloccaControlli = False Then BloccaSbloccaControlli()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckSalvaListini_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSalvaListini.CheckedChanged
    Try
      If bInBloccaControlli = False Then BloccaSbloccaControlli()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckInvFinale_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckInvFinale.CheckedChanged
    Try
      If bInBloccaControlli = False Then BloccaSbloccaControlli()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckGeneraListaSelezionata_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckGeneraListaSelezionata.CheckedChanged
    Try
      If ckGeneraListaSelezionata.Checked = True Then
        GctlSetVisEnab(edCodlsar, False)
        GctlSetVisEnab(edDeslsar, False)
        GctlSetVisEnab(ckQta0, False)
        edCodlsar.Text = oCleStrl.DeterminaCodiceListaSelezionata.ToString
        edCodlsar.Focus()
      Else
        edCodlsar.Text = "0"
        edDeslsar.Text = ""
        edCodlsar.Enabled = False
        edDeslsar.Enabled = False
        ckQta0.Enabled = False
      End If
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

#End Region

  Public Overridable Sub BloccaSbloccaControlli()
    Try
      bInBloccaControlli = True

      GctlSetVisEnab(cbTipoElab, False)
      edListino.Visible = False
      ckSoloPrezziListino.Visible = False
      ckUsaListForn.Visible = False
      GctlSetVisEnab(opMagazAltrui, False)
      GctlSetVisEnab(opMagazTutti, False)
      GctlSetVisEnab(opMagazNegozio, False)
      GctlSetVisEnab(opMagazUno, False)

      If ckInvFinale.Checked Then
        If opInventarioFinale.Checked Then opLifo.Checked = True
        opMagMerceProp.Checked = True
        opInventarioFinale.Enabled = False
        opMagazAltrui.Enabled = False
        opMagazTutti.Enabled = False
        opMagazNegozio.Enabled = False
        opMagazUno.Enabled = False
      Else
        GctlSetVisEnab(opInventarioFinale, False)
        GctlSetVisEnab(opMagazAltrui, False)
        GctlSetVisEnab(opMagazTutti, False)
        GctlSetVisEnab(opMagazNegozio, False)
        GctlSetVisEnab(opMagazUno, False)
      End If
      If opFifo.Checked Then opMagazNegozio.Enabled = False

      '--------------------------------
      'opzioni di valorizzazione
      If opLifo.Checked Then
        opMagMerceProp.Checked = True
        opMagazAltrui.Enabled = False
        opMagazTutti.Enabled = False
        opMagazNegozio.Enabled = False
        opMagazUno.Enabled = False
        GctlSetVisEnab(ckLifoAnniPrec, True)
      Else
        ckLifoAnniPrec.Visible = False

        If opInventarioFinale.Checked Then
          cbTipoElab.SelectedValue = "2"
          cbTipoElab.Enabled = False
          opMagMerceProp.Checked = True
          opMagazAltrui.Enabled = False
          opMagazTutti.Enabled = False
          opMagazNegozio.Enabled = False
          opMagazUno.Enabled = False
        Else
          If opListino.Checked Then
            GctlSetVisEnab(edListino, True)
            GctlSetVisEnab(ckSoloPrezziListino, True)
            GctlSetVisEnab(ckUsaListForn, True)
          End If
          If opFifo.Checked And opMagazNegozio.Checked Then
            opMagazNegozio.Enabled = False
            opMagMerceProp.Checked = True
          End If
        End If
      End If

      '--------------------------------
      'magazzino da elaborare
      If opMagazUno.Checked Then
        edNegozio.Visible = False
        lbNegozio.Visible = False
        GctlSetVisEnab(edMagaz, True)
        GctlSetVisEnab(lbMagaz, True)
        If opFifo.Checked Then
          ckUsacostiglob.Visible = False
        Else
          GctlSetVisEnab(ckUsacostiglob, True)
        End If
      ElseIf opMagazNegozio.Checked Then
        edMagaz.Visible = False
        lbMagaz.Visible = False
        ckUsacostiglob.Visible = False
        GctlSetVisEnab(edNegozio, True)
        GctlSetVisEnab(lbNegozio, True)
        If opFifo.Checked Or opLifo.Checked Then
          opUltimoCosto.Checked = True
        End If
      Else

        edNegozio.Visible = False
        lbNegozio.Visible = False
        edMagaz.Visible = False
        lbMagaz.Visible = False
        ckUsacostiglob.Visible = False
      End If

      '--------------------------------
      'tipo elaborazione
      Select Case NTSCInt(cbTipoElab.SelectedValue)
        Case 0  'attuale
          edDtelab.Visible = False
          ckInvFinale.Checked = False
          ckInvFinale.Visible = False
          GctlSetVisEnab(fmListini, False)
          GctlSetVisEnab(ckSalvaListini, False)
          ckUsacostiglob.Visible = False
        Case 1  'a data
          GctlSetVisEnab(edDtelab, True)
          ckInvFinale.Checked = False
          ckInvFinale.Visible = False
          GctlSetVisEnab(fmListini, False)
          GctlSetVisEnab(ckSalvaListini, False)
        Case 2  'a data ultimo aggiornamento
          edDtelab.Visible = False
          GctlSetVisEnab(ckInvFinale, True)
          fmListini.Enabled = False
          ckSalvaListini.Checked = False
          ckSalvaListini.Enabled = False
          ckUsacostiglob.Visible = False
      End Select

      '----------------------------
      If ckSalvaListini.Checked Then
        GctlSetVisEnab(edSalvaListData, False)
        GctlSetVisEnab(edSalvaListino, False)
        GctlSetVisEnab(ckSalvaCostiZero, False)
      Else
        edSalvaListData.Enabled = False
        edSalvaListino.Enabled = False
        ckSalvaCostiZero.Enabled = False
      End If

      '----------------------------
      If opInventarioFinale.Checked Then
        ckInvFinale.Checked = False
        ckInvFinale.Visible = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      bInBloccaControlli = False
    End Try
  End Sub

  Public Overridable Sub edNegozio_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edNegozio.Validated
    Dim strTmp As String = ""
    Try
      If oCleStrl Is Nothing Then Return

      If Not oCleStrl.edNegozio_Validated(NTSCInt(edNegozio.Text), strTmp) Then
        edNegozio.Text = NTSCStr(edNegozio.OldEditValue)
      Else
        lbNegozio.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edMagaz_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edMagaz.Validated
    Dim strTmp As String = ""
    Try
      If oCleStrl Is Nothing Then Return

      If Not oCleStrl.edMagaz_Validated(NTSCInt(edMagaz.Text), strTmp) Then
        edMagaz.Text = NTSCStr(edMagaz.OldEditValue)
      Else
        lbMagaz.Text = strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edCodlsar_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodlsar.Validated
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleStrl Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      If oCleStrl.edCodlsar_Validated(NTSCInt(edCodlsar.Text), strTmp) = True Then
        If edCodlsar.Text <> NTSCStr(edCodlsar.OldEditValue) Then edDeslsar.Text = strTmp
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Function Elabora(ByRef dTotalemagazStoricoLifo As Decimal) As Boolean
    Dim nValoriz As Integer = 0
    Dim nMagaz As Integer = 0

    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      '--- Seleziono gli articoli se non è stato fatto
      '--------------------------------------------------------------------------------------------------------------
      If strWhereArtico.Trim = "." Then
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128497284063318000, "Non sono stati selezionati gli articoli." & vbCrLf & "Selezionarli ora?")) = Windows.Forms.DialogResult.Yes Then
          cmdSelArt_Click(cmdSelArt, Nothing)
        End If
      End If
      If strWhereArtico.Trim = "." Then Return False
      '--------------------------------------------------------------------------------------------------------------
      '--- Esclude le ROOT
      '--------------------------------------------------------------------------------------------------------------
      strWhereArtico += "AND ((ar_gesvar = 'S' AND ar_codroot IS NOT NULL) OR (ar_gesvar <> 'S'))"
      '--------------------------------------------------------------------------------------------------------------
      If opListino.Checked And NTSCInt(edListino.Text) < 1 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128497829753282000, "Il numero di listino da utilizzare per la valorizzazione del magazzino deve essere maggiore di 0"))
        Return False
      End If
      If opMagazNegozio.Checked And NTSCInt(edNegozio.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129865744091070212, "Indicare lo stabilimento/filiale/negozio da inventariare"))
        Return False
      End If
      If opMagazUno.Checked And NTSCInt(edMagaz.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128497836314024000, "Indicare il magazzino da inventariare"))
        Return False
      End If
      If ckInvFinale.Checked Then
        If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128497830460118000, "Confermi l'elaborazione con l'aggiornamento nei 'progressivi definitivi articoli -> magazzini merce propria' di ULTIMO PREZZO e VALORE ESISTENZA per l'inventario finale?")) = Windows.Forms.DialogResult.No Then Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (ckGeneraListaSelezionata.Checked = True) Then
        If NTSCInt(edCodlsar.Text) = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129055417048310360, "Attenzione!" & vbCrLf & _
            "Indicare un codice Lista Selezionata Articoli valido altrimenti deselezionare la scelta."))
          Return False
        End If
        If oMenu.ValCodiceDb(edCodlsar.Text, DittaCorrente, "TABLSAR", "N") = True Then
          If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129058671169848020, "Attenzione!" & vbCrLf & _
            "La Lista Selezionata da generare è già esistente e sarà svuotata e ricreata con i nuovi dati presenti nell'inventario." & vbCrLf & _
            "Procedere?")) = Windows.Forms.DialogResult.No Then Return False
        End If
        If edDeslsar.Text.Trim = "" Then edDeslsar.Text = oCleStrl.ProponiDescrizioneListaSelezionata(NTSCInt(edCodlsar.Text))
        If edDeslsar.Text.Trim = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129055417893407690, "Attenzione!" & vbCrLf & _
            "Indicare una descrizione Lista Selezionata Articoli valida altrimenti deselezionare la scelta."))
          Return False
        End If
        If oCleStrl.IsTablsarDeletable(NTSCInt(edCodlsar.Text)) = False Then Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.WaitCursor
      '--------------------------------------------------------------------------------------------------------------
      '--- Eseguo l'elaborazione
      '--------------------------------------------------------------------------------------------------------------
      If opUltimoCosto.Checked Then nValoriz = 0
      If opLifo.Checked Then nValoriz = -1
      If opMedio.Checked Then nValoriz = -2
      If opMedioGlobale.Checked Then nValoriz = -3
      If opFifo.Checked Then nValoriz = -4
      If opUltimoCostoacc.Checked Then nValoriz = -5
      If opInventarioFinale.Checked Then nValoriz = -6
      If opListino.Checked Then nValoriz = NTSCInt(edListino.Text)
      If opMagMerceProp.Checked Then nMagaz = 0
      If opMagazAltrui.Checked Then nMagaz = -1
      If opMagazTutti.Checked Then nMagaz = -2
      If opMagazNegozio.Checked Then nMagaz = NTSCInt(edNegozio.Text) + 1000000
      If opMagazUno.Checked Then nMagaz = NTSCInt(edMagaz.Text)
      '--------------------------------------------------------------------------------------------------------------
      ScriviActlog()
      '--------------------------------------------------------------------------------------------------------------
      'nTipoElab: 0 = attuale, 1 = a data, 2 = a data ultimo aggiornamento
      'nValoriz:  0 = ultimo costo, -1 = lifo, -2 = costo medio dell'anno, -3 = costo medio globale, -4 = fifo, -5 = ultimo costo con oneri accessori, -6 = come da inventario finale, > 0 listino indicato
      'nGiacenze: 0 = > 0,     1 = < 0,    2 = <> 0,      3 = tutte, 4 = = 0
      'nMagazzino: 0 = merce propria, > 0 < 1000000 un magazzino (quelli indicato), > 1000000 un negozio, -1 = merce altrui, -2 = tutti i magazzini
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleStrl.Elabora(NTSCInt(cbTipoElab.SelectedValue), nValoriz, NTSCInt(cbGiacenze.SelectedValue), _
                              nMagaz, edDtelab.Text, ckInvFinale.Checked, ckUsacostiglob.Checked, _
                              ckLifoAnniPrec.Checked, ckSoloPrezziListino.Checked, ckUsaListForn.Checked, _
                              ckSalvaListini.Checked, NTSCInt(edSalvaListino.Text), edSalvaListData.Text, _
                              ckSalvaCostiZero.Checked, ckDettaglioTCO.Checked, strWhereArtico, _
                              ckEscludiArticoliNonMov.Checked, ckGeneraListaSelezionata.Checked, _
                              NTSCInt(edCodlsar.Text), edDeslsar.Text, dTotalemagazStoricoLifo, _
                              ckTcoEsitTaglia.Checked, ckQta0.Checked) Then Return False
      '--------------------------------------------------------------------------------------------------------------
      If oCleStrl.LogError = True Then
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129467196334375000, "Stampa inventario di magazzino terminata." & vbCrLf & _
                                       "Esistono messaggi nel file di LOG." & vbCrLf & _
                                       "Visualizzarlo?")) = Windows.Forms.DialogResult.Yes Then
          NTSProcessStart("notepad", oCleStrl.LogFileName)
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Function

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer
    Dim strMagazzini As String = ""
    Dim strlValorizz As String = ""

    Dim strDtElab As String = DateTime.Now.ToShortDateString
    Dim dTotalemagazStoricoLifo As Decimal = 0

    '@LM
    Dim strFileName As String = ""
    '@

    Try

      If Not Elabora(dTotalemagazStoricoLifo) Then Return

      '--------------------------------------------------
      Select Case NTSCInt(cbTipoElab.SelectedValue)
        Case 0 : strDtElab = DateTime.Now.ToShortDateString
        Case 1 : strDtElab = edDtelab.Text
        Case 2 : strDtElab = NTSCDate(oCleStrl.dttTabanaz.Rows(0)!tb_dtulap).ToShortDateString()
      End Select

      If opMagMerceProp.Checked Then strMagazzini = opMagMerceProp.Text
      If opMagazAltrui.Checked Then strMagazzini = opMagazAltrui.Text
      If opMagazTutti.Checked Then strMagazzini = opMagazTutti.Text
      If opMagazNegozio.Checked Then strMagazzini = opMagazNegozio.Text & ": " & edNegozio.Text & " - " & lbNegozio.Text
      If opMagazUno.Checked Then strMagazzini = opMagazUno.Text & ": " & edMagaz.Text & " - " & lbMagaz.Text

      If opUltimoCosto.Checked Then strlValorizz = opUltimoCosto.Text
      If opLifo.Checked Then strlValorizz = opLifo.Text
      If opMedio.Checked Then strlValorizz = opMedio.Text
      If opMedioGlobale.Checked Then strlValorizz = opMedioGlobale.Text
      If opFifo.Checked Then strlValorizz = opFifo.Text
      If opUltimoCostoacc.Checked Then strlValorizz = opUltimoCostoacc.Text
      If opInventarioFinale.Checked Then strlValorizz = opInventarioFinale.Text
      If opListino.Checked Then strlValorizz = opListino.Text & " " & edListino.Text

      strMagazzini = strMagazzini.Replace("&", "")
      strlValorizz = strlValorizz.Replace("&", "")

      Me.Cursor = Cursors.WaitCursor
      '--------------------------------------------------
      'preparo il motore di stampa
      If ckLifoAnniPrec.Checked And opLifo.Checked Then
        strCrpe = "{TTINVEN2.codditt} = '" & DittaCorrente & "'" & _
                  " And {TTINVEN2.instid} = " & oCleStrl.lIITTinvent2 & _
                  " And {TTINVENT.codditt} = '" & DittaCorrente & "'" & _
                  " And {TTINVENT.instid} = " & oCleStrl.lIITTinvent
        nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGSTRL", "Reports2", " ", 0, nDestin, "BSMGSTR1.RPT", False, "STAMPA INVENTARIO DI MAGAZZINO", False)
      Else
        strCrpe = "{TTINVENT.codditt} = '" & DittaCorrente & "' And {TTINVENT.instid} = " & oCleStrl.lIITTinvent
        nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGSTRL", "Reports1", " ", 0, nDestin, "BSMGSTRL.RPT", False, "STAMPA INVENTARIO DI MAGAZZINO", False)
      End If
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce già il multireport)
      For i = LBound(CType(nPjob, Array), 2) To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "VALORIZZ", ConvStrRpt(strlValorizz))
        If cbTipoElab.SelectedValue.ToString <> "2" Then
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "ELABOR", ConvStrRpt(cbTipoElab.Text & "    " & strDtElab))
        Else
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "ELABOR", ConvStrRpt(cbTipoElab.Text))
        End If
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "SCELTAMAGAZZINI", ConvStrRpt(strMagazzini))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "GIACENZE", ConvStrRpt(cbGiacenze.Text))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "MAGAZZINI", ConvStrRpt(strMagazzini))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DTULAP", "'" & NTSCDate(oCleStrl.dttTabanaz.Rows(0)!tb_dtulap).ToShortDateString & "'")
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DTELABOR", "'" & strDtElab & "'")
        If opMagazNegozio.Checked Then
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "CODMAGA", "" & edNegozio.Text & "")
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DESMAGA", ConvStrRpt(lbNegozio.Text))
        Else
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "CODMAGA", "" & edMagaz.Text & "")
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DESMAGA", ConvStrRpt(lbMagaz.Text))
        End If

        If ckLifoAnniPrec.Checked And opLifo.Checked Then
          nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "TOTALEMAGAZ", CDblSQL(dTotalemagazStoricoLifo))
        End If

        '@LM
        'nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
        'Determino il tipo di stampa
        If nDestin = 2 Then
          'Stampa PDF
          strFileName = oApp.Dir & "\Asc\" & "StampaInventario-" & edMagaz.Text & "-" & lbMagaz.Text & ".PDF"
          nRis = oMenu.ReportPDF(NTSCInt(CType(nPjob, Array).GetValue(0, i)), strFileName)
          nRis = oMenu.ReportPEVaiPDF(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
          oMenu.ReportPEClosePDF(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
        Else
          'Stampa a video e Stampa su carta
          nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
        End If
        '@
      Next

      '------------------------
      'DETTAGLIO TCO
      If ckDettaglioTCO.Checked And oCleStrl.bModTCO Then
        strCrpe = "{TTINVENTTC.codditt} = '" & DittaCorrente & "'" & _
                  " And {TTINVENTTC.instid} = " & oCleStrl.lIITTinventTC & _
                  " And {TTINVENT.codditt} = '" & DittaCorrente & "'" & _
                  " And {TTINVENT.instid} = " & oCleStrl.lIITTinvent
        nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGSTRL", "Reports3", " ", 0, nDestin, "BSMGSTR3.RPT", False, "STAMPA INVENTARIO DI MAGAZZINO", False)
        If nPjob Is Nothing Then Return
        '--------------------------------------------------
        'lancio tutti gli eventuali reports (gestisce già il multireport)
        For i = LBound(CType(nPjob, Array), 2) To UBound(CType(nPjob, Array), 2)
          nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))

          '@LM
          'nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
          If nDestin = 2 Then
            'Stampa PDF
            strFileName = oApp.Dir & "\Asc\" & "StampaInventario-" & edMagaz.Text & "-" & lbMagaz.Text & ".PDF"
            nRis = oMenu.ReportPDF(NTSCInt(CType(nPjob, Array).GetValue(0, i)), strFileName)
            nRis = oMenu.ReportPEVaiPDF(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
            oMenu.ReportPEClosePDF(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
          Else
            'Stampa video e Stampa su carta
            nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
          End If
          '@
        Next
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
      lbStatus.Text = oApp.Tr(Me, 128831307080345089, "Pronto.")
    End Try
  End Sub

  Public Overridable Sub cmdSelArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelArt.Click
    Dim oPar As CLE__PATB = Nothing
    Try
      oPar = New CLE__PATB
      oPar.bVisGriglia = False
      oPar.strTipoArticolo = "N"
      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oPar)
      If oPar.CANCELZOOM = False And oPar.strOut <> "" Then strWhereArtico = oPar.strOut.Trim

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

#Region "Elaborazione multi magazzino"
  ''' <summary>
  ''' Verifica se le condizioni necessarie e sufficienti per la stampa multi magazzino sono rispettate.
  ''' </summary>
  ''' <returns>True= condizioni rispettate; False= condizioni non rispettate</returns>
  Public Function CheckCondition() As Boolean
    Dim bOk As Boolean = True
    Dim strWarning As String = ""

    Try
      'Verifico se sono stati selezionati gli articoli
      If (strWhereArtico.Trim = ".") Then bOk = False

      If bOk Then
        'Verifico che "Salva costi come listino" non sia spuntato
        If (ckSalvaListini.Checked) Then bOk = False
      End If

      If bOk Then
        'Verifico che "Tipo elaborazione" sia diverso da
        If (NTSCInt(cbTipoElab.SelectedValue) = 2) Then bOk = False
      End If

      If bOk Then
        'Se devo generare la lista selezionata valuto anche la descrizione della lista
        If ckGeneraListaSelezionata.Checked AndAlso edDeslsar.Text.Trim = "" Then bOk = False
      End If

      If Not bOk Then
        'Messaggio per utente
        strWarning = oApp.Tr(Me, 130904329796403963, "Per utilizzare questa funzionalità, è necessario rispettare le seguenti condizioni:" & vbCrLf & _
                                                     "- Aver selezionato degli articoli" & vbCrLf & _
                                                     "- Aver selezionato un 'Tipo elaborazione' diverso da 'A data ultimo aggiornamento'" & vbCrLf & _
                                                     "- Non aver selezionato l'opzione 'Salva costi come listino'" & vbCrLf & _
                                                     "- Se si è scelto di generare la lista selezionata occorre indicare la descrizione")
        oApp.MsgBoxInfo(strWarning)
      Else
        'Forzo magazzino 1
        opMagazUno.Checked = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    Return bOk
  End Function

  ''' <summary>
  ''' Visualizza la form per la stampa di inventario multi magazzino
  ''' </summary>
  Public Overridable Sub PrintMultiWarehouse()
    Dim frmStmm As FRMMGSTMM = Nothing
    Dim arrstrWarehouseSelected() As String = Nothing
    Dim strPrinterType As String = ""
    Dim bExecutePrint As Boolean = False

    Try
      frmStmm = CType(NTSNewFormModal("FRMMGSTMM"), FRMMGSTMM)
      frmStmm.Init(oMenu, Nothing, DittaCorrente, Nothing)
      frmStmm.Initentity(oCleStrl)

      Me.Cursor = Cursors.Default
      frmStmm.ShowDialog()

      'Determino le condizioni di stampa
      If Not frmStmm.mbCancel Then
        If (frmStmm.mstrWarehouseSelected.Length > 0) Then
          arrstrWarehouseSelected = frmStmm.mstrWarehouseSelected.Split(CType("|", Char))
          strPrinterType = frmStmm.mstrPrinterType
          bExecutePrint = True
        End If
      End If

      'Distruggo la form
      frmStmm.Dispose()
      frmStmm = Nothing

      'Verifico se procedere con la stampa
      If bExecutePrint Then
        Dim lTipoStampa As Integer
        Select Case strPrinterType
          Case "V" 'Stampa a video
            lTipoStampa = 0
          Case "C" 'Stampa su carta
            lTipoStampa = 1
          Case "P" 'Stampa su PDF
            lTipoStampa = 2
        End Select

        For Each strMagaz As String In arrstrWarehouseSelected
          edMagaz.Text = strMagaz.Substring(0, strMagaz.IndexOf(CType("§", Char)))
          lbMagaz.Text = strMagaz.Substring(strMagaz.IndexOf(CType("§", Char)) + 1)
          Stampa(lTipoStampa)
          If ckGeneraListaSelezionata.Checked Then
            Dim strDescr As String = edDeslsar.Text
            While True 'Incrementa il numero della lista selezionata
              edCodlsar.TextInt += 1
              edDeslsar.Text = strDescr
              If Not oMenu.ValCodiceDb(edCodlsar.Text, DittaCorrente, "TABLSAR", "N") Then Exit While
            End While
          End If
        Next
        'Resetto il riferimento al magazzino
        edMagaz.Text = "0"
        lbMagaz.Text = ""
      End If

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Scrittura LOG"
  Public Overridable Sub ScriviActlog()
    Dim strDesogglog As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strDesogglog = "Stampa Inventario di Magazzino" & vbCrLf & vbCrLf & _
        " - Tipo elaborazione................................................: " & cbTipoElab.Text & _
        IIf(edDtelab.Visible = True, " " & edDtelab.Text, "").ToString & vbCrLf & _
        " - Inventario finale................................................: " & IIf(ckInvFinale.Checked = True, "Sì", "No").ToString & vbCrLf & _
        " - Considera giacenze...............................................: " & cbGiacenze.Text & vbCrLf
      If ckEscludiArticoliNonMov.Enabled = True Then
        strDesogglog += " - Escludi articoli mai movimentati.................................: " & _
          IIf(ckEscludiArticoliNonMov.Checked = True, "Sì", "No").ToString & vbCrLf
      End If
      If ckTcoEsitTaglia.Enabled = True Then
        strDesogglog += " - TCO usa giacenza x taglia........................................: " & _
          IIf(ckTcoEsitTaglia.Checked = True, "Sì", "No").ToString & vbCrLf
      End If
      strDesogglog += " - Tipo valorizzazione................................................: " & _
        IIf(opLifo.Checked = True, "Lifo", "").ToString & _
        IIf(opMedio.Checked = True, "Costo medio dell'anno", "").ToString & _
        IIf(opMedioGlobale.Checked = True, "Costo medio globale", "").ToString & _
        IIf(opUltimoCosto.Checked = True, "Ultimo costo", "").ToString & _
        IIf(opListino.Checked = True, "Listino --> " & edListino.Text, "").ToString & _
        IIf(opFifo.Checked = True, "Fifo", "").ToString & _
        IIf(opUltimoCostoacc.Checked = True, "Ultimo costo compreso di oneri accessori", "").ToString & _
        IIf(opInventarioFinale.Checked = True, "Come da inventario finale", "").ToString & vbCrLf
      If (opLifo.Checked = True) And (ckLifoAnniPrec.Visible = True) Then
        strDesogglog += " - Stampa dati anni precedenti........................................: " & _
          IIf(ckLifoAnniPrec.Checked = True, "Sì", "No").ToString & vbCrLf
      End If
      If opListino.Checked = True Then
        If ckSoloPrezziListino.Visible = True Then
          strDesogglog += " - Usa solo prezzi da listino.........................................: " & _
            IIf(ckSoloPrezziListino.Checked = True, "Sì", "No").ToString & vbCrLf
        End If
      End If
      strDesogglog += " - Magazzino da elaborare.............................................: " & _
        IIf(opMagMerceProp.Checked = True, "Merce propria", "").ToString & _
        IIf(opMagazAltrui.Checked = True, "Merce altrui", "").ToString & _
        IIf(opMagazTutti.Checked = True, "Tutti i magazzini", "").ToString & _
        IIf(opMagazNegozio.Checked = True, "Uno stabilimento/filiale/negozio", "").ToString & _
        IIf(opMagazUno.Checked = True, "Un magazzino", "").ToString & vbCrLf
      If (opMagazNegozio.Checked = True) And (edNegozio.Visible = True) Then
        strDesogglog += " - Stabilimento/Filiale/Negozio.......................................: " & _
          edNegozio.Text & IIf(lbNegozio.Text.Trim <> "", " - " & lbNegozio.Text, "").ToString & vbCrLf
      End If
      If (opMagazUno.Checked = True) And (edMagaz.Visible = True) Then
        strDesogglog += " - Magazzino..........................................................: " & _
          edMagaz.Text & IIf(lbMagaz.Text.Trim <> "", " - " & lbMagaz.Text, "").ToString & vbCrLf
      End If
      If ckUsacostiglob.Visible = True Then
        strDesogglog += " - Usa Costi ricavati dai movimenti di tutti i magazzini..............: " & _
          IIf(ckUsacostiglob.Checked = True, "Sì", "No").ToString & vbCrLf
      End If
      strDesogglog += " - Salva costi come listini...........................................: " & _
        IIf(ckSalvaListini.Checked = True, "Sì", "No").ToString & vbCrLf
      If ckSalvaListini.Checked = True Then
        strDesogglog += " - Listino............................................................: " & _
          edSalvaListino.Text & vbCrLf & _
          " - Data inizio validità...............................................: " & _
          edSalvaListData.Text & vbCrLf & _
          " - Salva anche costi pari a 0.........................................: " & _
          IIf(ckSalvaCostiZero.Checked = True, "Sì", "No").ToString & vbCrLf
      End If
      strDesogglog += " - Genera Lista Selezionata Articoli....................................: " & _
        IIf(ckGeneraListaSelezionata.Checked = True, "Sì", "No").ToString & vbCrLf
      If ckGeneraListaSelezionata.Checked = True Then
        strDesogglog += " - Lista Selezionata Articoli generata................................: " & _
          edCodlsar.Text & IIf(edDeslsar.Text.Trim <> "", " - " & edDeslsar.Text, "").ToString & vbCrLf
      End If
      strDesogglog += " - Stampa dettaglio quantità per taglie.................................: " & _
        IIf(ckDettaglioTCO.Checked = True, "Sì", "No").ToString & vbCrLf
      oCleStrl.ScriviActLog(strDesogglog)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region
End Class
