Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGSTRL_F
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGSTRL_F))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
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
    Me.opMedio = New NTSInformatica.NTSRadioButton
    Me.opMedioGlobale = New NTSInformatica.NTSRadioButton
    Me.opUltimoCosto = New NTSInformatica.NTSRadioButton
    Me.opListino = New NTSInformatica.NTSRadioButton
    Me.opUltimoCostoacc = New NTSInformatica.NTSRadioButton
    Me.opInventarioFinale = New NTSInformatica.NTSRadioButton
    Me.opMagazUno = New NTSInformatica.NTSRadioButton
    Me.opMagazTutti = New NTSInformatica.NTSRadioButton
    Me.opMagazAltrui = New NTSInformatica.NTSRadioButton
    Me.opMagMerceProp = New NTSInformatica.NTSRadioButton
    Me.fmValorizzazione = New NTSInformatica.NTSGroupBox
    Me.ckUsaListForn = New NTSInformatica.NTSCheckBox
    Me.ckSoloPrezziListino = New NTSInformatica.NTSCheckBox
    Me.edListino = New NTSInformatica.NTSTextBoxNum
    Me.fmMagazzini = New NTSInformatica.NTSGroupBox
    Me.cmdSelArt = New NTSInformatica.NTSButton
    Me.lbStatus = New NTSInformatica.NTSLabel
    Me.ckEscludiArticoliNonMov = New NTSInformatica.NTSCheckBox
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
    CType(Me.opMedio.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opMedioGlobale.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opUltimoCosto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opListino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opUltimoCostoacc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opInventarioFinale.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    CType(Me.ckEscludiArticoliNonMov.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbStampaGriglia})
    Me.NtsBarManager1.MaxItemId = 18
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
    'tlbStampaGriglia
    '
    Me.tlbStampaGriglia.Caption = "Stampa su griglia"
    Me.tlbStampaGriglia.Glyph = CType(resources.GetObject("tlbStampaGriglia.Glyph"), System.Drawing.Image)
    Me.tlbStampaGriglia.Id = 17
    Me.tlbStampaGriglia.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F11)
    Me.tlbStampaGriglia.Name = "tlbStampaGriglia"
    Me.tlbStampaGriglia.Visible = True
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
    'edMagaz
    '
    Me.edMagaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMagaz.EditValue = "0"
    Me.edMagaz.Location = New System.Drawing.Point(117, 76)
    Me.edMagaz.Name = "edMagaz"
    Me.edMagaz.NTSDbField = ""
    Me.edMagaz.NTSFormat = "0"
    Me.edMagaz.NTSForzaVisZoom = False
    Me.edMagaz.NTSOldValue = ""
    Me.edMagaz.Properties.Appearance.Options.UseTextOptions = True
    Me.edMagaz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMagaz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMagaz.Properties.AppearanceDisabled.Options.UseForeColor = True
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
    Me.lbMagaz.Location = New System.Drawing.Point(179, 76)
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
    Me.lbTipoElab.Location = New System.Drawing.Point(12, 39)
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
    Me.cbTipoElab.Location = New System.Drawing.Point(124, 36)
    Me.cbTipoElab.Name = "cbTipoElab"
    Me.cbTipoElab.NTSDbField = ""
    Me.cbTipoElab.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipoElab.Properties.DropDownRows = 30
    Me.cbTipoElab.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipoElab.SelectedValue = ""
    Me.cbTipoElab.Size = New System.Drawing.Size(211, 20)
    Me.cbTipoElab.TabIndex = 9
    Me.cbTipoElab.ValueMember = ""
    '
    'edDtelab
    '
    Me.edDtelab.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDtelab.EditValue = "01/01/1900"
    Me.edDtelab.Location = New System.Drawing.Point(344, 36)
    Me.edDtelab.Name = "edDtelab"
    Me.edDtelab.NTSDbField = ""
    Me.edDtelab.NTSForzaVisZoom = False
    Me.edDtelab.NTSOldValue = ""
    Me.edDtelab.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDtelab.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDtelab.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDtelab.Properties.MaxLength = 65536
    Me.edDtelab.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDtelab.Size = New System.Drawing.Size(81, 20)
    Me.edDtelab.TabIndex = 10
    '
    'ckInvFinale
    '
    Me.ckInvFinale.Cursor = System.Windows.Forms.Cursors.Hand
    Me.ckInvFinale.Location = New System.Drawing.Point(435, 37)
    Me.ckInvFinale.Name = "ckInvFinale"
    Me.ckInvFinale.NTSCheckValue = "S"
    Me.ckInvFinale.NTSUnCheckValue = "N"
    Me.ckInvFinale.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckInvFinale.Properties.Appearance.Options.UseBackColor = True
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
    Me.fmListini.Location = New System.Drawing.Point(405, 257)
    Me.fmListini.Name = "fmListini"
    Me.fmListini.Size = New System.Drawing.Size(210, 96)
    Me.fmListini.TabIndex = 12
    '
    'edSalvaListino
    '
    Me.edSalvaListino.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSalvaListino.EditValue = "0"
    Me.edSalvaListino.Location = New System.Drawing.Point(109, 23)
    Me.edSalvaListino.Name = "edSalvaListino"
    Me.edSalvaListino.NTSDbField = ""
    Me.edSalvaListino.NTSFormat = "0"
    Me.edSalvaListino.NTSForzaVisZoom = False
    Me.edSalvaListino.NTSOldValue = ""
    Me.edSalvaListino.Properties.Appearance.Options.UseTextOptions = True
    Me.edSalvaListino.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSalvaListino.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSalvaListino.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSalvaListino.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSalvaListino.Properties.MaxLength = 65536
    Me.edSalvaListino.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSalvaListino.Size = New System.Drawing.Size(84, 20)
    Me.edSalvaListino.TabIndex = 17
    '
    'ckSalvaCostiZero
    '
    Me.ckSalvaCostiZero.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSalvaCostiZero.Location = New System.Drawing.Point(5, 71)
    Me.ckSalvaCostiZero.Name = "ckSalvaCostiZero"
    Me.ckSalvaCostiZero.NTSCheckValue = "S"
    Me.ckSalvaCostiZero.NTSUnCheckValue = "N"
    Me.ckSalvaCostiZero.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSalvaCostiZero.Properties.Appearance.Options.UseBackColor = True
    Me.ckSalvaCostiZero.Properties.Caption = "Salva anche costi pari a 0"
    Me.ckSalvaCostiZero.Size = New System.Drawing.Size(153, 19)
    Me.ckSalvaCostiZero.TabIndex = 16
    '
    'lbSalvaListData
    '
    Me.lbSalvaListData.AutoSize = True
    Me.lbSalvaListData.BackColor = System.Drawing.Color.Transparent
    Me.lbSalvaListData.Location = New System.Drawing.Point(5, 48)
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
    Me.edSalvaListData.Location = New System.Drawing.Point(109, 45)
    Me.edSalvaListData.Name = "edSalvaListData"
    Me.edSalvaListData.NTSDbField = ""
    Me.edSalvaListData.NTSForzaVisZoom = False
    Me.edSalvaListData.NTSOldValue = ""
    Me.edSalvaListData.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSalvaListData.Properties.AppearanceDisabled.Options.UseForeColor = True
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
    Me.lbSalvaListino.Location = New System.Drawing.Point(5, 26)
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
    Me.ckSalvaListini.Properties.Caption = "Salva costi come listino"
    Me.ckSalvaListini.Size = New System.Drawing.Size(133, 19)
    Me.ckSalvaListini.TabIndex = 12
    '
    'lbGiacenze
    '
    Me.lbGiacenze.AutoSize = True
    Me.lbGiacenze.BackColor = System.Drawing.Color.Transparent
    Me.lbGiacenze.Location = New System.Drawing.Point(12, 61)
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
    Me.cbGiacenze.Location = New System.Drawing.Point(124, 58)
    Me.cbGiacenze.Name = "cbGiacenze"
    Me.cbGiacenze.NTSDbField = ""
    Me.cbGiacenze.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbGiacenze.Properties.DropDownRows = 30
    Me.cbGiacenze.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbGiacenze.SelectedValue = ""
    Me.cbGiacenze.Size = New System.Drawing.Size(211, 20)
    Me.cbGiacenze.TabIndex = 14
    Me.cbGiacenze.ValueMember = ""
    '
    'ckUsacostiglob
    '
    Me.ckUsacostiglob.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckUsacostiglob.Location = New System.Drawing.Point(117, 99)
    Me.ckUsacostiglob.Name = "ckUsacostiglob"
    Me.ckUsacostiglob.NTSCheckValue = "S"
    Me.ckUsacostiglob.NTSUnCheckValue = "N"
    Me.ckUsacostiglob.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckUsacostiglob.Properties.Appearance.Options.UseBackColor = True
    Me.ckUsacostiglob.Properties.Caption = "Usa Costi ricavati dai movimenti di tutti i magazzini"
    Me.ckUsacostiglob.Size = New System.Drawing.Size(266, 19)
    Me.ckUsacostiglob.TabIndex = 15
    '
    'opMedio
    '
    Me.opMedio.Cursor = System.Windows.Forms.Cursors.Default
    Me.opMedio.EditValue = True
    Me.opMedio.Location = New System.Drawing.Point(5, 23)
    Me.opMedio.Name = "opMedio"
    Me.opMedio.NTSCheckValue = "S"
    Me.opMedio.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opMedio.Properties.Appearance.Options.UseBackColor = True
    Me.opMedio.Properties.Caption = "&Costo medio dell'anno"
    Me.opMedio.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opMedio.Size = New System.Drawing.Size(130, 19)
    Me.opMedio.TabIndex = 18
    '
    'opMedioGlobale
    '
    Me.opMedioGlobale.Cursor = System.Windows.Forms.Cursors.Default
    Me.opMedioGlobale.Location = New System.Drawing.Point(5, 41)
    Me.opMedioGlobale.Name = "opMedioGlobale"
    Me.opMedioGlobale.NTSCheckValue = "S"
    Me.opMedioGlobale.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opMedioGlobale.Properties.Appearance.Options.UseBackColor = True
    Me.opMedioGlobale.Properties.Caption = "Costo medio &globale"
    Me.opMedioGlobale.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opMedioGlobale.Size = New System.Drawing.Size(121, 19)
    Me.opMedioGlobale.TabIndex = 19
    '
    'opUltimoCosto
    '
    Me.opUltimoCosto.Cursor = System.Windows.Forms.Cursors.Default
    Me.opUltimoCosto.Location = New System.Drawing.Point(5, 59)
    Me.opUltimoCosto.Name = "opUltimoCosto"
    Me.opUltimoCosto.NTSCheckValue = "S"
    Me.opUltimoCosto.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opUltimoCosto.Properties.Appearance.Options.UseBackColor = True
    Me.opUltimoCosto.Properties.Caption = "Ultimo c&osto"
    Me.opUltimoCosto.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opUltimoCosto.Size = New System.Drawing.Size(84, 19)
    Me.opUltimoCosto.TabIndex = 20
    '
    'opListino
    '
    Me.opListino.Cursor = System.Windows.Forms.Cursors.Default
    Me.opListino.Location = New System.Drawing.Point(5, 77)
    Me.opListino.Name = "opListino"
    Me.opListino.NTSCheckValue = "S"
    Me.opListino.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opListino.Properties.Appearance.Options.UseBackColor = True
    Me.opListino.Properties.Caption = "Li&stino"
    Me.opListino.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opListino.Size = New System.Drawing.Size(69, 19)
    Me.opListino.TabIndex = 21
    '
    'opUltimoCostoacc
    '
    Me.opUltimoCostoacc.Cursor = System.Windows.Forms.Cursors.Default
    Me.opUltimoCostoacc.Location = New System.Drawing.Point(5, 95)
    Me.opUltimoCostoacc.Name = "opUltimoCostoacc"
    Me.opUltimoCostoacc.NTSCheckValue = "S"
    Me.opUltimoCostoacc.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opUltimoCostoacc.Properties.Appearance.Options.UseBackColor = True
    Me.opUltimoCostoacc.Properties.Caption = "&Ultimo costo compreso di oneri accessori"
    Me.opUltimoCostoacc.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opUltimoCostoacc.Size = New System.Drawing.Size(218, 19)
    Me.opUltimoCostoacc.TabIndex = 23
    '
    'opInventarioFinale
    '
    Me.opInventarioFinale.Cursor = System.Windows.Forms.Cursors.Default
    Me.opInventarioFinale.Location = New System.Drawing.Point(5, 113)
    Me.opInventarioFinale.Name = "opInventarioFinale"
    Me.opInventarioFinale.NTSCheckValue = "S"
    Me.opInventarioFinale.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opInventarioFinale.Properties.Appearance.Options.UseBackColor = True
    Me.opInventarioFinale.Properties.Caption = "Come da inventario finale"
    Me.opInventarioFinale.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opInventarioFinale.Size = New System.Drawing.Size(145, 19)
    Me.opInventarioFinale.TabIndex = 24
    '
    'opMagazUno
    '
    Me.opMagazUno.Cursor = System.Windows.Forms.Cursors.Default
    Me.opMagazUno.Location = New System.Drawing.Point(8, 77)
    Me.opMagazUno.Name = "opMagazUno"
    Me.opMagazUno.NTSCheckValue = "S"
    Me.opMagazUno.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opMagazUno.Properties.Appearance.Options.UseBackColor = True
    Me.opMagazUno.Properties.Caption = "&Un magazzino"
    Me.opMagazUno.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opMagazUno.Size = New System.Drawing.Size(97, 19)
    Me.opMagazUno.TabIndex = 29
    '
    'opMagazTutti
    '
    Me.opMagazTutti.Cursor = System.Windows.Forms.Cursors.Default
    Me.opMagazTutti.Location = New System.Drawing.Point(8, 59)
    Me.opMagazTutti.Name = "opMagazTutti"
    Me.opMagazTutti.NTSCheckValue = "S"
    Me.opMagazTutti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opMagazTutti.Properties.Appearance.Options.UseBackColor = True
    Me.opMagazTutti.Properties.Caption = "&Tutti i magazzini"
    Me.opMagazTutti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opMagazTutti.Size = New System.Drawing.Size(107, 19)
    Me.opMagazTutti.TabIndex = 28
    '
    'opMagazAltrui
    '
    Me.opMagazAltrui.Cursor = System.Windows.Forms.Cursors.Default
    Me.opMagazAltrui.Location = New System.Drawing.Point(8, 41)
    Me.opMagazAltrui.Name = "opMagazAltrui"
    Me.opMagazAltrui.NTSCheckValue = "S"
    Me.opMagazAltrui.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opMagazAltrui.Properties.Appearance.Options.UseBackColor = True
    Me.opMagazAltrui.Properties.Caption = "M&erce altrui"
    Me.opMagazAltrui.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opMagazAltrui.Size = New System.Drawing.Size(84, 19)
    Me.opMagazAltrui.TabIndex = 27
    '
    'opMagMerceProp
    '
    Me.opMagMerceProp.Cursor = System.Windows.Forms.Cursors.Default
    Me.opMagMerceProp.EditValue = True
    Me.opMagMerceProp.Location = New System.Drawing.Point(8, 23)
    Me.opMagMerceProp.Name = "opMagMerceProp"
    Me.opMagMerceProp.NTSCheckValue = "S"
    Me.opMagMerceProp.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opMagMerceProp.Properties.Appearance.Options.UseBackColor = True
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
    Me.fmValorizzazione.Controls.Add(Me.opMedio)
    Me.fmValorizzazione.Controls.Add(Me.opMedioGlobale)
    Me.fmValorizzazione.Controls.Add(Me.opUltimoCosto)
    Me.fmValorizzazione.Controls.Add(Me.opListino)
    Me.fmValorizzazione.Controls.Add(Me.opInventarioFinale)
    Me.fmValorizzazione.Controls.Add(Me.opUltimoCostoacc)
    Me.fmValorizzazione.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmValorizzazione.Location = New System.Drawing.Point(7, 84)
    Me.fmValorizzazione.Name = "fmValorizzazione"
    Me.fmValorizzazione.Size = New System.Drawing.Size(608, 140)
    Me.fmValorizzazione.TabIndex = 30
    Me.fmValorizzazione.Text = "Tipo valorizzazione"
    '
    'ckUsaListForn
    '
    Me.ckUsaListForn.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckUsaListForn.Location = New System.Drawing.Point(376, 77)
    Me.ckUsaListForn.Name = "ckUsaListForn"
    Me.ckUsaListForn.NTSCheckValue = "S"
    Me.ckUsaListForn.NTSUnCheckValue = "N"
    Me.ckUsaListForn.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckUsaListForn.Properties.Appearance.Options.UseBackColor = True
    Me.ckUsaListForn.Properties.Caption = "Usa listino Fornitore, se presente"
    Me.ckUsaListForn.Size = New System.Drawing.Size(189, 19)
    Me.ckUsaListForn.TabIndex = 28
    '
    'ckSoloPrezziListino
    '
    Me.ckSoloPrezziListino.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSoloPrezziListino.Location = New System.Drawing.Point(197, 77)
    Me.ckSoloPrezziListino.Name = "ckSoloPrezziListino"
    Me.ckSoloPrezziListino.NTSCheckValue = "S"
    Me.ckSoloPrezziListino.NTSUnCheckValue = "N"
    Me.ckSoloPrezziListino.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSoloPrezziListino.Properties.Appearance.Options.UseBackColor = True
    Me.ckSoloPrezziListino.Properties.Caption = "U&sa solo prezzi da listino"
    Me.ckSoloPrezziListino.Size = New System.Drawing.Size(147, 19)
    Me.ckSoloPrezziListino.TabIndex = 27
    '
    'edListino
    '
    Me.edListino.Cursor = System.Windows.Forms.Cursors.Default
    Me.edListino.EditValue = "0"
    Me.edListino.Location = New System.Drawing.Point(117, 76)
    Me.edListino.Name = "edListino"
    Me.edListino.NTSDbField = ""
    Me.edListino.NTSFormat = "0"
    Me.edListino.NTSForzaVisZoom = False
    Me.edListino.NTSOldValue = ""
    Me.edListino.Properties.Appearance.Options.UseTextOptions = True
    Me.edListino.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edListino.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edListino.Properties.AppearanceDisabled.Options.UseForeColor = True
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
    Me.fmMagazzini.Controls.Add(Me.opMagMerceProp)
    Me.fmMagazzini.Controls.Add(Me.opMagazAltrui)
    Me.fmMagazzini.Controls.Add(Me.opMagazUno)
    Me.fmMagazzini.Controls.Add(Me.ckUsacostiglob)
    Me.fmMagazzini.Controls.Add(Me.opMagazTutti)
    Me.fmMagazzini.Controls.Add(Me.edMagaz)
    Me.fmMagazzini.Controls.Add(Me.lbMagaz)
    Me.fmMagazzini.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmMagazzini.Location = New System.Drawing.Point(7, 230)
    Me.fmMagazzini.Name = "fmMagazzini"
    Me.fmMagazzini.Size = New System.Drawing.Size(392, 123)
    Me.fmMagazzini.TabIndex = 31
    Me.fmMagazzini.Text = "Magazzini da elaborare"
    '
    'cmdSelArt
    '
    Me.cmdSelArt.ImageText = ""
    Me.cmdSelArt.Location = New System.Drawing.Point(453, 359)
    Me.cmdSelArt.Name = "cmdSelArt"
    Me.cmdSelArt.Size = New System.Drawing.Size(162, 27)
    Me.cmdSelArt.TabIndex = 33
    Me.cmdSelArt.Text = "Seleziona articoli"
    '
    'lbStatus
    '
    Me.lbStatus.AutoSize = True
    Me.lbStatus.BackColor = System.Drawing.Color.Transparent
    Me.lbStatus.Location = New System.Drawing.Point(9, 373)
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
    Me.ckEscludiArticoliNonMov.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckEscludiArticoliNonMov.Location = New System.Drawing.Point(435, 58)
    Me.ckEscludiArticoliNonMov.Name = "ckEscludiArticoliNonMov"
    Me.ckEscludiArticoliNonMov.NTSCheckValue = "S"
    Me.ckEscludiArticoliNonMov.NTSUnCheckValue = "N"
    Me.ckEscludiArticoliNonMov.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckEscludiArticoliNonMov.Properties.Appearance.Options.UseBackColor = True
    Me.ckEscludiArticoliNonMov.Properties.Caption = "Escludi articoli mai movimentati"
    Me.ckEscludiArticoliNonMov.Size = New System.Drawing.Size(175, 19)
    Me.ckEscludiArticoliNonMov.TabIndex = 35
    '
    'FRMMGSTRL_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(624, 403)
    Me.Controls.Add(Me.ckEscludiArticoliNonMov)
    Me.Controls.Add(Me.lbStatus)
    Me.Controls.Add(Me.cmdSelArt)
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
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRMMGSTRL_F"
    Me.Text = "STAMPA INVENTARIO DI MAGAZZINO"
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
    CType(Me.opMedio.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opMedioGlobale.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opUltimoCosto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opListino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opUltimoCostoacc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opInventarioFinale.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    CType(Me.ckEscludiArticoliNonMov.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
      opMagMerceProp.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952404000, "Merce propria"), "P")
      opMagazAltrui.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952414000, "Merce altrui"), "A")
      opMagazTutti.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952444000, "Tutti i magazzini"), "T")
      opMagazUno.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952424000, "Un magazzino"), "U")
      ckUsacostiglob.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952434000, "Usa Costi ricavati dai movimenti di tutti i magazzini"), "S", "N")
      edMagaz.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128497224952454000, "Magazzino"), tabmaga)
      ckUsaListForn.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952464000, "Usa listino Fornitore, se presente"), "S", "N")
      ckSoloPrezziListino.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952474000, "Usa solo prezzi da listino"), "S", "N")
      edListino.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128497224952484000, "Listino"), tablist)
      opMedio.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952504000, "Costo medio dell'anno"), "A")
      opMedioGlobale.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952524000, "Costo medio globale"), "G")
      opUltimoCosto.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952534000, "Ultimo costo"), "U")
      opListino.NTSSetParam(oMenu, oApp.Tr(Me, 128497224952544000, "Listino"), "L")
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
      If opMagazUno.Checked Then strMagazzini = opMagazUno.Text & ": " & edMagaz.Text & " - " & lbMagaz.Text

      If opUltimoCosto.Checked Then strlValorizz = opUltimoCosto.Text
      If opMedio.Checked Then strlValorizz = opMedio.Text
      If opMedioGlobale.Checked Then strlValorizz = opMedioGlobale.Text
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
        Case Else
          ckEscludiArticoliNonMov.Checked = False
          ckEscludiArticoliNonMov.Enabled = False
      End Select
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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
      GctlSetVisEnab(opMagazUno, False)

      If ckInvFinale.Checked Then
        opMagMerceProp.Checked = True
        opInventarioFinale.Enabled = False
        opMagazAltrui.Enabled = False
        opMagazTutti.Enabled = False
        opMagazUno.Enabled = False
      Else
        GctlSetVisEnab(opInventarioFinale, False)
        GctlSetVisEnab(opMagazAltrui, False)
        GctlSetVisEnab(opMagazTutti, False)
        GctlSetVisEnab(opMagazUno, False)
      End If

      '--------------------------------
      'opzioni di valorizzazione
      If opInventarioFinale.Checked Then
        cbTipoElab.SelectedValue = "2"
        cbTipoElab.Enabled = False
        opMagMerceProp.Checked = True
        opMagazAltrui.Enabled = False
        opMagazTutti.Enabled = False
        opMagazUno.Enabled = False
      Else
        If opListino.Checked Then
          GctlSetVisEnab(edListino, True)
          GctlSetVisEnab(ckSoloPrezziListino, True)
          GctlSetVisEnab(ckUsaListForn, True)
        End If
      End If

      '--------------------------------
      'magazzino da elaborare
      If opMagazUno.Checked Then
        GctlSetVisEnab(edMagaz, True)
        GctlSetVisEnab(lbMagaz, True)
        GctlSetVisEnab(ckUsacostiglob, True)
      Else
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
      If opListino.Checked And NTSCInt(edListino.Text) < 1 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128497829753282000, "Il numero di listino da utilizzare per la valorizzazione del magazzino deve essere maggiore di 0"))
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
      Me.Cursor = Cursors.WaitCursor
      '--------------------------------------------------------------------------------------------------------------
      '--- Eseguo l'elaborazione
      '--------------------------------------------------------------------------------------------------------------
      If opUltimoCosto.Checked Then nValoriz = 0
      If opMedio.Checked Then nValoriz = -2
      If opMedioGlobale.Checked Then nValoriz = -3
      If opUltimoCostoacc.Checked Then nValoriz = -5
      If opInventarioFinale.Checked Then nValoriz = -6
      If opListino.Checked Then nValoriz = NTSCInt(edListino.Text)
      If opMagMerceProp.Checked Then nMagaz = 0
      If opMagazAltrui.Checked Then nMagaz = -1
      If opMagazTutti.Checked Then nMagaz = -2
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
                              False, ckSoloPrezziListino.Checked, ckUsaListForn.Checked, _
                              ckSalvaListini.Checked, NTSCInt(edSalvaListino.Text), edSalvaListData.Text, _
                              ckSalvaCostiZero.Checked, False, strWhereArtico, ckEscludiArticoliNonMov.Checked, _
                              False, 0, "", dTotalemagazStoricoLifo, False) Then Return False
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
      If opMagazUno.Checked Then strMagazzini = opMagazUno.Text & ": " & edMagaz.Text & " - " & lbMagaz.Text

      If opUltimoCosto.Checked Then strlValorizz = opUltimoCosto.Text
      If opMedio.Checked Then strlValorizz = opMedio.Text
      If opMedioGlobale.Checked Then strlValorizz = opMedioGlobale.Text
      If opUltimoCostoacc.Checked Then strlValorizz = opUltimoCostoacc.Text
      If opInventarioFinale.Checked Then strlValorizz = opInventarioFinale.Text
      If opListino.Checked Then strlValorizz = opListino.Text & " " & edListino.Text

      strMagazzini = strMagazzini.Replace("&", "")
      strlValorizz = strlValorizz.Replace("&", "")

      Me.Cursor = Cursors.WaitCursor
      '--------------------------------------------------
      'preparo il motore di stampa
      strCrpe = "{TTINVENT.codditt} = '" & DittaCorrente & "' And {TTINVENT.instid} = " & oCleStrl.lIITTinvent
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGSTRL", "Reports1", " ", 0, nDestin, "BSMGSTRL.RPT", False, "STAMPA INVENTARIO DI MAGAZZINO", False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce già il multireport)
      For i = LBound(CType(nPjob, Array), 2) To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "VALORIZZ", ConvStrRpt(strlValorizz))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "ELABOR", ConvStrRpt(cbTipoElab.Text & "    " & strDtElab))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "SCELTAMAGAZZINI", ConvStrRpt(strMagazzini))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "GIACENZE", ConvStrRpt(cbGiacenze.Text))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "MAGAZZINI", ConvStrRpt(strMagazzini))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DTULAP", "'" & NTSCDate(oCleStrl.dttTabanaz.Rows(0)!tb_dtulap).ToShortDateString & "'")
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DTELABOR", "'" & strDtElab & "'")
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "CODMAGA", "" & edMagaz.Text & "")
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DESMAGA", ConvStrRpt(lbMagaz.Text))

        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next
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
      If oPar.CANCELZOOM = False Then strWhereArtico = oPar.strOut.Trim

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

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
      strDesogglog += " - Tipo valorizzazione................................................: " & _
        IIf(opMedio.Checked = True, "Costo medio dell'anno", "").ToString & _
        IIf(opMedioGlobale.Checked = True, "Costo medio globale", "").ToString & _
        IIf(opUltimoCosto.Checked = True, "Ultimo costo", "").ToString & _
        IIf(opListino.Checked = True, "Listino --> " & edListino.Text, "").ToString & _
        IIf(opUltimoCostoacc.Checked = True, "Ultimo costo compreso di oneri accessori", "").ToString & _
        IIf(opInventarioFinale.Checked = True, "Come da inventario finale", "").ToString & vbCrLf
      If opListino.Checked = True Then
        If ckSoloPrezziListino.Visible = True Then
          strDesogglog += " - Usa solo prezzi da listino.........................................: " & _
            IIf(ckSoloPrezziListino.Checked = True, "Sì", "No").ToString & vbCrLf
        End If
      End If
      strDesogglog += " - Magazzino da elaborare.............................................: " & _
        IIf(opMagMerceProp.Checked = True, "Merce propria", "").ToString & _
        IIf(opMagazAltrui.Checked = True, "Merce altrui", "").ToString & _
        IIf(opMagazTutti.Checked = True, "Tutti i magazzini", "").ToString
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
      oCleStrl.ScriviActLog(strDesogglog)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

End Class