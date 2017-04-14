Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__VAAN
  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = bsModExtAll
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

#Region "Variaribli"
  Public oCleVaan As CLE__VAAN
  Public oCallParams As CLE__CLDP
  Public strInfoMess As String = ""
  Public strTabella As String = "" 'tabella per effettuare lo zoom

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbElabora As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbDataelab As NTSInformatica.NTSLabel
  Public WithEvents fmElaborazione As NTSInformatica.NTSGroupBox
  Public WithEvents cbNuovoValore As NTSInformatica.NTSComboBox
  Public WithEvents cbValore As NTSInformatica.NTSComboBox
  Public WithEvents cbCampi As NTSInformatica.NTSComboBox
  Public WithEvents edDataelab As NTSInformatica.NTSTextBoxData
  Public WithEvents ckSeleziona As NTSInformatica.NTSCheckBox
  Public WithEvents lbNuovoValore As NTSInformatica.NTSLabel
  Public WithEvents lbCampi As NTSInformatica.NTSLabel
  Public WithEvents ckElaborazione As NTSInformatica.NTSCheckBox
  Public WithEvents lbInfo As NTSInformatica.NTSLabel
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarMenuItem
  Public WithEvents edNuovoValore As NTSInformatica.NTSTextBoxStr
  Public WithEvents edValore As NTSInformatica.NTSTextBoxStr
  Public WithEvents edDummy As NTSInformatica.NTSTextBoxStr
  Public WithEvents ckElabGri As NTSInformatica.NTSCheckBox
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
          Case "SELEZIONA:"
            cmdSeleziona_Click(Me, Nothing)
            Me.Refresh()
          Case "REFREINFO:"
            lbInfo.Text = oApp.Tr(Me, 129034453834708355, "Aggiornamento |" & Mid(cbCampi.Text, (InStr(1, cbCampi.Text, "- ") + 2)) & "| in corso...")
            Me.Refresh()
          Case "SALTACONT:"
            Salta()
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__VAAN))
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
    Me.lbDataelab = New NTSInformatica.NTSLabel
    Me.edDataelab = New NTSInformatica.NTSTextBoxData
    Me.cbCampi = New NTSInformatica.NTSComboBox
    Me.cbValore = New NTSInformatica.NTSComboBox
    Me.cbNuovoValore = New NTSInformatica.NTSComboBox
    Me.fmElaborazione = New NTSInformatica.NTSGroupBox
    Me.ckElabGri = New NTSInformatica.NTSCheckBox
    Me.ckElaborazione = New NTSInformatica.NTSCheckBox
    Me.lbCampi = New NTSInformatica.NTSLabel
    Me.lbNuovoValore = New NTSInformatica.NTSLabel
    Me.ckSeleziona = New NTSInformatica.NTSCheckBox
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.lbInfo = New NTSInformatica.NTSLabel
    Me.edValore = New NTSInformatica.NTSTextBoxStr
    Me.edNuovoValore = New NTSInformatica.NTSTextBoxStr
    Me.edDummy = New NTSInformatica.NTSTextBoxStr
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataelab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbCampi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbValore.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbNuovoValore.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmElaborazione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmElaborazione.SuspendLayout()
    CType(Me.ckElabGri.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckElaborazione.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSeleziona.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edValore.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNuovoValore.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDummy.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbElabora, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbStrumenti, Me.tlbImpostaStampante})
    Me.NtsBarManager1.MaxItemId = 31
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbElabora, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbElabora.Id = 4
    Me.tlbElabora.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbElabora.Name = "tlbElabora"
    Me.tlbElabora.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 27
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "StampaVideo"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 28
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 26
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 29
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.Id = 30
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.NTSIsCheckBox = False
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 18
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 19
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'lbDataelab
    '
    Me.lbDataelab.AutoSize = True
    Me.lbDataelab.BackColor = System.Drawing.Color.Transparent
    Me.lbDataelab.Location = New System.Drawing.Point(12, 53)
    Me.lbDataelab.Name = "lbDataelab"
    Me.lbDataelab.NTSDbField = ""
    Me.lbDataelab.Size = New System.Drawing.Size(94, 13)
    Me.lbDataelab.TabIndex = 98
    Me.lbDataelab.Text = "Data elaborazione"
    Me.lbDataelab.Tooltip = ""
    Me.lbDataelab.UseMnemonic = False
    '
    'edDataelab
    '
    Me.edDataelab.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataelab.Location = New System.Drawing.Point(183, 50)
    Me.edDataelab.Name = "edDataelab"
    Me.edDataelab.NTSDbField = ""
    Me.edDataelab.NTSForzaVisZoom = False
    Me.edDataelab.NTSOldValue = ""
    Me.edDataelab.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataelab.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataelab.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataelab.Properties.MaxLength = 65536
    Me.edDataelab.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataelab.Size = New System.Drawing.Size(95, 20)
    Me.edDataelab.TabIndex = 103
    '
    'cbCampi
    '
    Me.cbCampi.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbCampi.DataSource = Nothing
    Me.cbCampi.DisplayMember = ""
    Me.cbCampi.Location = New System.Drawing.Point(183, 76)
    Me.cbCampi.Name = "cbCampi"
    Me.cbCampi.NTSDbField = ""
    Me.cbCampi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbCampi.Properties.DropDownRows = 30
    Me.cbCampi.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbCampi.SelectedValue = ""
    Me.cbCampi.Size = New System.Drawing.Size(301, 20)
    Me.cbCampi.TabIndex = 104
    Me.cbCampi.ValueMember = ""
    '
    'cbValore
    '
    Me.cbValore.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbValore.DataSource = Nothing
    Me.cbValore.DisplayMember = ""
    Me.cbValore.Location = New System.Drawing.Point(183, 102)
    Me.cbValore.Name = "cbValore"
    Me.cbValore.NTSDbField = ""
    Me.cbValore.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbValore.Properties.DropDownRows = 30
    Me.cbValore.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbValore.SelectedValue = ""
    Me.cbValore.Size = New System.Drawing.Size(141, 20)
    Me.cbValore.TabIndex = 105
    Me.cbValore.ValueMember = ""
    '
    'cbNuovoValore
    '
    Me.cbNuovoValore.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbNuovoValore.DataSource = Nothing
    Me.cbNuovoValore.DisplayMember = ""
    Me.cbNuovoValore.Location = New System.Drawing.Point(183, 128)
    Me.cbNuovoValore.Name = "cbNuovoValore"
    Me.cbNuovoValore.NTSDbField = ""
    Me.cbNuovoValore.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbNuovoValore.Properties.DropDownRows = 30
    Me.cbNuovoValore.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbNuovoValore.SelectedValue = ""
    Me.cbNuovoValore.Size = New System.Drawing.Size(141, 20)
    Me.cbNuovoValore.TabIndex = 106
    Me.cbNuovoValore.ValueMember = ""
    '
    'fmElaborazione
    '
    Me.fmElaborazione.AllowDrop = True
    Me.fmElaborazione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmElaborazione.Appearance.Options.UseBackColor = True
    Me.fmElaborazione.Controls.Add(Me.ckElabGri)
    Me.fmElaborazione.Controls.Add(Me.ckElaborazione)
    Me.fmElaborazione.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmElaborazione.Location = New System.Drawing.Point(15, 160)
    Me.fmElaborazione.Name = "fmElaborazione"
    Me.fmElaborazione.Size = New System.Drawing.Size(273, 50)
    Me.fmElaborazione.TabIndex = 107
    '
    'ckElabGri
    '
    Me.ckElabGri.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckElabGri.Location = New System.Drawing.Point(143, 22)
    Me.ckElabGri.Name = "ckElabGri"
    Me.ckElabGri.NTSCheckValue = "S"
    Me.ckElabGri.NTSUnCheckValue = "N"
    Me.ckElabGri.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckElabGri.Properties.Appearance.Options.UseBackColor = True
    Me.ckElabGri.Properties.Caption = "Elaborazione in griglia"
    Me.ckElabGri.Size = New System.Drawing.Size(125, 19)
    Me.ckElabGri.TabIndex = 1
    '
    'ckElaborazione
    '
    Me.ckElaborazione.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckElaborazione.Location = New System.Drawing.Point(5, 23)
    Me.ckElaborazione.Name = "ckElaborazione"
    Me.ckElaborazione.NTSCheckValue = "S"
    Me.ckElaborazione.NTSUnCheckValue = "N"
    Me.ckElaborazione.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckElaborazione.Properties.Appearance.Options.UseBackColor = True
    Me.ckElaborazione.Properties.Caption = "Elaborazione manuale"
    Me.ckElaborazione.Size = New System.Drawing.Size(132, 19)
    Me.ckElaborazione.TabIndex = 0
    '
    'lbCampi
    '
    Me.lbCampi.AutoSize = True
    Me.lbCampi.BackColor = System.Drawing.Color.Transparent
    Me.lbCampi.Location = New System.Drawing.Point(12, 79)
    Me.lbCampi.Name = "lbCampi"
    Me.lbCampi.NTSDbField = ""
    Me.lbCampi.Size = New System.Drawing.Size(92, 13)
    Me.lbCampi.TabIndex = 108
    Me.lbCampi.Text = "Campo da variare"
    Me.lbCampi.Tooltip = ""
    Me.lbCampi.UseMnemonic = False
    '
    'lbNuovoValore
    '
    Me.lbNuovoValore.AutoSize = True
    Me.lbNuovoValore.BackColor = System.Drawing.Color.Transparent
    Me.lbNuovoValore.Location = New System.Drawing.Point(12, 131)
    Me.lbNuovoValore.Name = "lbNuovoValore"
    Me.lbNuovoValore.NTSDbField = ""
    Me.lbNuovoValore.Size = New System.Drawing.Size(124, 13)
    Me.lbNuovoValore.TabIndex = 110
    Me.lbNuovoValore.Text = "Nuovo valore da inserire"
    Me.lbNuovoValore.Tooltip = ""
    Me.lbNuovoValore.UseMnemonic = False
    '
    'ckSeleziona
    '
    Me.ckSeleziona.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSeleziona.Location = New System.Drawing.Point(15, 103)
    Me.ckSeleziona.Name = "ckSeleziona"
    Me.ckSeleziona.NTSCheckValue = "S"
    Me.ckSeleziona.NTSUnCheckValue = "N"
    Me.ckSeleziona.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSeleziona.Properties.Appearance.Options.UseBackColor = True
    Me.ckSeleziona.Properties.Caption = "Seleziona &valore da sostituire"
    Me.ckSeleziona.Size = New System.Drawing.Size(164, 19)
    Me.ckSeleziona.TabIndex = 111
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(367, 46)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.Size = New System.Drawing.Size(117, 24)
    Me.cmdSeleziona.TabIndex = 112
    Me.cmdSeleziona.Text = "&Seleziona Conti"
    '
    'lbInfo
    '
    Me.lbInfo.BackColor = System.Drawing.Color.Transparent
    Me.lbInfo.Location = New System.Drawing.Point(12, 213)
    Me.lbInfo.Name = "lbInfo"
    Me.lbInfo.NTSDbField = ""
    Me.lbInfo.Size = New System.Drawing.Size(276, 22)
    Me.lbInfo.TabIndex = 113
    Me.lbInfo.Tooltip = ""
    Me.lbInfo.UseMnemonic = False
    '
    'edValore
    '
    Me.edValore.Cursor = System.Windows.Forms.Cursors.Default
    Me.edValore.Location = New System.Drawing.Point(183, 102)
    Me.edValore.Name = "edValore"
    Me.edValore.NTSDbField = ""
    Me.edValore.NTSForzaVisZoom = False
    Me.edValore.NTSOldValue = ""
    Me.edValore.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edValore.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edValore.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edValore.Properties.MaxLength = 65536
    Me.edValore.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edValore.Size = New System.Drawing.Size(40, 20)
    Me.edValore.TabIndex = 114
    '
    'edNuovoValore
    '
    Me.edNuovoValore.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNuovoValore.Location = New System.Drawing.Point(183, 128)
    Me.edNuovoValore.Name = "edNuovoValore"
    Me.edNuovoValore.NTSDbField = ""
    Me.edNuovoValore.NTSForzaVisZoom = False
    Me.edNuovoValore.NTSOldValue = ""
    Me.edNuovoValore.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNuovoValore.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNuovoValore.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNuovoValore.Properties.MaxLength = 65536
    Me.edNuovoValore.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNuovoValore.Size = New System.Drawing.Size(40, 20)
    Me.edNuovoValore.TabIndex = 115
    '
    'edDummy
    '
    Me.edDummy.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDummy.Enabled = False
    Me.edDummy.Location = New System.Drawing.Point(284, 50)
    Me.edDummy.Name = "edDummy"
    Me.edDummy.NTSDbField = ""
    Me.edDummy.NTSForzaVisZoom = False
    Me.edDummy.NTSOldValue = ""
    Me.edDummy.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDummy.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDummy.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDummy.Properties.MaxLength = 65536
    Me.edDummy.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDummy.Size = New System.Drawing.Size(40, 20)
    Me.edDummy.TabIndex = 116
    Me.edDummy.TabStop = False
    Me.edDummy.Visible = False
    '
    'FRM__VAAN
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(499, 257)
    Me.Controls.Add(Me.edDummy)
    Me.Controls.Add(Me.edNuovoValore)
    Me.Controls.Add(Me.edValore)
    Me.Controls.Add(Me.lbInfo)
    Me.Controls.Add(Me.cmdSeleziona)
    Me.Controls.Add(Me.ckSeleziona)
    Me.Controls.Add(Me.lbNuovoValore)
    Me.Controls.Add(Me.lbCampi)
    Me.Controls.Add(Me.fmElaborazione)
    Me.Controls.Add(Me.cbNuovoValore)
    Me.Controls.Add(Me.cbValore)
    Me.Controls.Add(Me.cbCampi)
    Me.Controls.Add(Me.edDataelab)
    Me.Controls.Add(Me.lbDataelab)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRM__VAAN"
    Me.Text = "VARIAZIONE CAMPI ANAGRAFICA CLIENTI/FORNITORI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataelab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbCampi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbValore.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbNuovoValore.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmElaborazione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmElaborazione.ResumeLayout(False)
    Me.fmElaborazione.PerformLayout()
    CType(Me.ckElabGri.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckElaborazione.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSeleziona.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edValore.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNuovoValore.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDummy.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    'creo e attivo l'entity e inizializzo la funzione che dovrÃ  rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__VAAN", "BE__VAAN", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128498740238262677, "ERRORE in fase di creazione Entity:" & vbCrLf) & strErr)
      Return False
    End If
    oCleVaan = CType(oTmp, CLE__VAAN)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__VAAN", strRemoteServer, strRemotePort)
    AddHandler oCleVaan.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleVaan.Init(oApp, oScript, oMenu.oCleComm, "MOVMAG", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbElabora.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'Ã¨ una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      '-------------------------------------------------
      'completo le informazioni dei controlli

      ckSeleziona.NTSSetParam(oMenu, oApp.Tr(Me, 128503018095189186, "Seleziona &valore da sostituire"), "S", "N")
      ckElaborazione.NTSSetParam(oMenu, oApp.Tr(Me, 128503018095659616, "Elaborazione manuale"), "S", "N")
      cbNuovoValore.NTSSetParam(oApp.Tr(Me, 128503018095816426, "Nuovo valore da inserire"))
      cbValore.NTSSetParam(oApp.Tr(Me, 128503018095973236, "Valore da sostituire"))
      cbCampi.NTSSetParam(oApp.Tr(Me, 128503018096130046, "Campo da variare"))
      edDataelab.NTSSetParam(oMenu, oApp.Tr(Me, 128503018096286856, "Data elaborazione"), False)
      edNuovoValore.NTSSetParam(oMenu, oApp.Tr(Me, 128503058793743166, "Nuovo valore da inserire"), 0)
      edValore.NTSSetParam(oMenu, oApp.Tr(Me, 128503058793899071, "Valore da sostituire"), 0)
      edDummy.NTSSetParam(oMenu, oApp.Tr(Me, 129418299449451935, ""), 0)
      ckElabGri.NTSSetParam(oMenu, oApp.Tr(Me, 129832896977295651, "Elaborazione griglia"), "S", "N")

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

  Public Overridable Sub CaricaCombo()
    Dim dttCampi As New DataTable()
    Try
      dttCampi.Columns.Add("cod", GetType(String))
      dttCampi.Columns.Add("val", GetType(String))
      dttCampi.Rows.Add(New Object() {"A", "an_rating - Affidabilità"})
      dttCampi.Rows.Add(New Object() {"B", "an_agente - Agente 1"})
      dttCampi.Rows.Add(New Object() {"C", "an_agente2 - Agente 2"})
      dttCampi.Rows.Add(New Object() {"D", "an_agcontrop - Aggiunta a contropartita articolo"})
      If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupCAE) Then
        dttCampi.Rows.Add(New Object() {"}", "an_coddica - Aggregazione budget"})
      End If
      dttCampi.Rows.Add(New Object() {"E", "an_bolli - Bolli"})
      dttCampi.Rows.Add(New Object() {"!", "an_cap - Cap"})
      dttCampi.Rows.Add(New Object() {"F", "an_categ - Categoria"})
      dttCampi.Rows.Add(New Object() {"G", "an_clascon - Classe di sconto"})
      dttCampi.Rows.Add(New Object() {"H", "an_claprov - Classe provvigioni"})
      If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupGPV) Then
        dttCampi.Rows.Add(New Object() {"#", "an_privato - Cliente privato"})
      End If
      If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupCEC) Then
        dttCampi.Rows.Add(New Object() {"10", "an_webvis - Cliente/Fornitore visibile dall'applicazione esterna"})
      End If
      dttCampi.Rows.Add(New Object() {"I", "an_codcana - Codice canale"})
      dttCampi.Rows.Add(New Object() {"J", "an_codese - Codice esenzione IVA"})
      dttCampi.Rows.Add(New Object() {"K", "an_codbanc - Codice nostra banca"})
      dttCampi.Rows.Add(New Object() {"L", "an_codpag - Codice pagamento"})
      If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupTEN) Then
        dttCampi.Rows.Add(New Object() {"]", "an_codvfde - Codice voce finanziaria"})
      End If
      'dttCampi.Rows.Add(New Object() {"M", "an_controp - Contropartita abituale"})
      dttCampi.Rows.Add(New Object() {"°", "an_datdic - Data (Dichiarazione d'intenti)"})
      dttCampi.Rows.Add(New Object() {"N", "an_datdicp - Data (Dichiarazione d'intenti protocollo)"})
      dttCampi.Rows.Add(New Object() {"O", "an_scaddic - Data di scadenza (Dichiarazione d'intenti)"})
      dttCampi.Rows.Add(New Object() {"P", "an_giofiss - Giorno fisso di pagamento"})
      dttCampi.Rows.Add(New Object() {"Q", "an_gcons - Giorno di consegna"})
      dttCampi.Rows.Add(New Object() {"R", "an_maxdic - Importo massimo (Dichiarazione d'intenti)"})
      'dttCampi.Rows.Add(New Object() {"S", "an_codling - Lingua"})
      dttCampi.Rows.Add(New Object() {"T", "an_listino - Listino"})
      dttCampi.Rows.Add(New Object() {"U", "an_mesees1 - Mese escluso 1"})
      dttCampi.Rows.Add(New Object() {"W", "an_mesees2 - Mese escluso 2"})
      'dttCampi.Rows.Add(New Object() {"X", "an_usaem - Modalità di corrispondenza"})
      dttCampi.Rows.Add(New Object() {"Y", "an_codntra - Natura transazione"})
      dttCampi.Rows.Add(New Object() {"§", "an_numdic - Numero (Dichiarazione d'intenti)"})
      dttCampi.Rows.Add(New Object() {"Z", "an_numdicp - Numero (Dichiarazione d'intenti protocollo)"})
      dttCampi.Rows.Add(New Object() {"0", "an_perfatt - Periodo di fatturazione"})
      dttCampi.Rows.Add(New Object() {"1", "an_porto - Porto"})
      dttCampi.Rows.Add(New Object() {"X", "an_prov - Provincia"})
      If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupTEN) Then
        dttCampi.Rows.Add(New Object() {"[", "an_trating - Rating tesoreria"})
      End If
      dttCampi.Rows.Add(New Object() {"2", "an_kpccee2 - Saldo AVERE (Bil.CEE)"})
      dttCampi.Rows.Add(New Object() {"3", "an_kpccee - Saldo DARE (Bil.CEE)"})
      dttCampi.Rows.Add(New Object() {"4", "an_rifrica - Saldo AVERE (Rif.Bil.Riclass)"})
      dttCampi.Rows.Add(New Object() {"5", "an_rifricd - Saldo DARE (Rif.Bil.Riclass.)"})
      dttCampi.Rows.Add(New Object() {"6", "an_spinc - Spese incasso"})
      dttCampi.Rows.Add(New Object() {"7", "an_status - Status"})
      dttCampi.Rows.Add(New Object() {"8", "an_codtpbf - Tipo bolla/fattura"})
      dttCampi.Rows.Add(New Object() {"9", "an_fatt - Tipo fatturazione"})
      If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupCAE) Then
        dttCampi.Rows.Add(New Object() {"{", "an_codtcdc - Tipologia entità"})
      End If
      If oCleVaan.bModuloANG = False Then
        dttCampi.Rows.Add(New Object() {"M", "an_tpsogiva - Tipo soggetto Iva"})
      End If
      If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupCAE) Then
        dttCampi.Rows.Add(New Object() {"¶", "an_coddicv - Valore aggregazione budget"})
      End If
      dttCampi.Rows.Add(New Object() {"£", "an_valuta - Valuta"})
      dttCampi.Rows.Add(New Object() {"$", "an_vett - Vettore 1"})
      dttCampi.Rows.Add(New Object() {"%", "an_vett2 - Vettore 2"})
      dttCampi.Rows.Add(New Object() {"&", "an_zona - Zona"})
      dttCampi.AcceptChanges()
      cbCampi.DataSource = dttCampi
      cbCampi.ValueMember = "cod"
      cbCampi.DisplayMember = "val"

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRM__VAAN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try

      If CBool(oApp.ActKey.ModuliExtAcquistati And bsModExtANG) Then
        oCleVaan.bModuloANG = True
      Else
        oCleVaan.bModuloANG = False
      End If

      '-------------------------------------------------
      'carico i combobox
      CaricaCombo()
      strInfoMess = oApp.Tr(Me, 130421101892479167, "Nessuna operazione in corso...")
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      oCleVaan.bIsCrmUser = oMenu.IsCrmUser(DittaCorrente)

      '------------------------------------------------------------
      oCleVaan.bSeleziona = False
      oCleVaan.strCampo = ""

      oCleVaan.lIITTInvent = oMenu.GetTblInstId("TTINVENT", False)
      edDataelab.Text = NTSCStr(Now)
      '------------------------------------------------------------
      ckSeleziona.Checked = False
      edValore.Enabled = False
      cbValore.Enabled = False
      edValore.Visible = False
      cbValore.Visible = False
      ckElaborazione.Checked = False
      lbInfo.Text = strInfoMess
      InizializzaControlli(edNuovoValore, cbNuovoValore)
      '------------------------------------------------------------

      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__VAAN_ActivatedFirst(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ActivatedFirst
    Try
      If CBool(oApp.ActKey.ModuliExtAcquistati And bsModExtCRM) Or CBool(oApp.ActKey.ModuliSupAcquistati And bsModSupWCR) Then
        oCleVaan.bModuloCRM = True
      Else
        oCleVaan.bModuloCRM = False
      End If
      If CBool(oApp.ActKey.ModuliAcquistati And bsModAS) Then
        oCleVaan.bModuloAS = True
      Else
        oCleVaan.bModuloAS = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__VAAN_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If oCleVaan.bElabInCorso Then e.Cancel = True
  End Sub

  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Dim oPar As CLE__PATB = Nothing
    Try
      oPar = New CLE__PATB
      oPar.bTipoProposto = True
      oPar.strTipo = "C"
      oPar.bVisGriglia = False
      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oPar)
      oCleVaan.strWhereFian = oPar.strOut.Trim

      'estrae dalla query il tipo di conto su sui è stata fatta la ricerca
      'controlla che nella query ci sia la stringa anaext
      If oCleVaan.strWhereFian = "" Then
        oCleVaan.bSeleziona = False
        oCleVaan.strTipo = ""
        oCleVaan.bFxaxSelFiltri = False
      Else
        oCleVaan.bSeleziona = True
        If oCleVaan.strWhereFian.ToLower.IndexOf("an_tipo = '") > -1 Then
          oCleVaan.strTipo = Mid(oCleVaan.strWhereFian, oCleVaan.strWhereFian.ToLower.IndexOf("an_tipo = '") + 12, 1)
        Else
          oCleVaan.strTipo = ""
        End If
        If oCleVaan.strWhereFian.ToLower.IndexOf("anaext") > -1 Then
          oCleVaan.bFxaxSelFiltri = True
        Else
          oCleVaan.bFxaxSelFiltri = False
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckSeleziona_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSeleziona.CheckedChanged
    Try
      If ckSeleziona.Checked = True Then
        InizializzaControlli(edValore, cbValore)
      Else
        AzzeraControlli(edValore, cbValore)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cbCampi_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCampi.SelectedIndexChanged
    Try
      InizializzaControlli(edNuovoValore, cbNuovoValore)
      If ckSeleziona.Checked = True Then InizializzaControlli(edValore, cbValore)

      Select Case cbCampi.SelectedValue
        Case "}" : oApp.MsgBoxInfo(oApp.Tr(Me, 129321449997161344, "Promemoria:" & vbCrLf & _
                  "Durante il cambio dell'Aggregazione Budget verranno eliminati i Valori di Aggregazione Budget non associati all'Aggregazione Budget selezionata."))
        Case "¶" : oApp.MsgBoxInfo(oApp.Tr(Me, 129321450012329913, "Promemoria:" & vbCrLf & _
                  "Verranno modificati solo i clienti con l'Aggregazione Budget compatibile con il Valore di Aggregazione Budget selezionato."))
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"

  Public Overridable Sub tlbElabora_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbElabora.ItemClick
    Try
      Me.ValidaLastControl()
      If Not Elabora(0) Then Return
    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      Me.ValidaLastControl()
      If Not Elabora(1) Then Return
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      Me.ValidaLastControl()
      If Not Elabora(2) Then Return
      Stampa(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim dttTmp As New DataTable

    Try
      'per eventuali altri controlli caricati al volo
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      If edValore.Focused Then
        Select Case strTabella.ToUpper
          Case "ANAGRA"
            '--------------------------------------------
            'zoom anagra
            SetFastZoom(edValore.Text, oParam)
            oParam.strTipo = "S"
            oParam.bTipoProposto = True
            NTSZOOM.strIn = edValore.Text
            NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
            If NTSZOOM.strIn <> edValore.Text Then edValore.NTSTextDB = NTSZOOM.strIn
          Case "TCDC"
            '--------------------------------------------
            'zoom tabtcdc
            SetFastZoom(edNuovoValore.Text, oParam)
            oParam.strTipo = "A"
            NTSZOOM.strIn = edNuovoValore.Text
            NTSZOOM.ZoomStrIn("ZOOMTABTCDC", DittaCorrente, oParam)
            If NTSZOOM.strIn <> edNuovoValore.Text Then edNuovoValore.NTSTextDB = NTSZOOM.strIn
          Case "DICA"
            '--------------------------------------------
            'zoom TABDICA
            SetFastZoom(edNuovoValore.Text, oParam)
            oParam.strTipo = "A"
            NTSZOOM.strIn = edNuovoValore.Text
            NTSZOOM.ZoomStrIn("ZOOMTABDICA", DittaCorrente, oParam)
            If NTSZOOM.strIn <> edNuovoValore.Text Then edNuovoValore.NTSTextDB = NTSZOOM.strIn
          Case "COMUNI"
            edDummy.Text = ""
            NTSZOOM.strIn = NTSCStr(edDummy.Text)
            NTSZOOM.ZoomStrIn("ZOOMCOMUNI", DittaCorrente, oParam)
            If NTSZOOM.strIn <> edDummy.Text Then
              edDummy.Text = NTSZOOM.strIn
              If oMenu.ValCodiceDb(edDummy.Text, DittaCorrente, "COMUNI", "S", "", dttTmp) = True Then
                edValore.Text = NTSCStr(dttTmp.Rows(0)!co_cap)
              End If
            End If
          Case Else
            '------------------------------------
            'zoom standard di textbox e griglia
            NTSCallStandardZoom()
        End Select
      ElseIf edNuovoValore.Focused Then
        Select Case strTabella.ToUpper
          Case "ANAGRA"
            '--------------------------------------------
            'zoom anagra
            SetFastZoom(edNuovoValore.Text, oParam)
            oParam.strTipo = "S"
            oParam.bTipoProposto = True
            NTSZOOM.strIn = edNuovoValore.Text
            NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
            If NTSZOOM.strIn <> edNuovoValore.Text Then edNuovoValore.NTSTextDB = NTSZOOM.strIn
          Case "TCDC"
            '--------------------------------------------
            'zoom tabtcdc
            SetFastZoom(edNuovoValore.Text, oParam)
            oParam.strTipo = "K"
            NTSZOOM.strIn = edNuovoValore.Text
            NTSZOOM.ZoomStrIn("ZOOMTABTCDC", DittaCorrente, oParam)
            If NTSZOOM.strIn <> edNuovoValore.Text Then edNuovoValore.NTSTextDB = NTSZOOM.strIn
          Case "DICA"
            '--------------------------------------------
            'zoom TABDICA
            SetFastZoom(edNuovoValore.Text, oParam)
            oParam.strTipo = "K"
            NTSZOOM.strIn = edNuovoValore.Text
            NTSZOOM.ZoomStrIn("ZOOMTABDICA", DittaCorrente, oParam)
            If NTSZOOM.strIn <> edNuovoValore.Text Then edNuovoValore.NTSTextDB = NTSZOOM.strIn
          Case "COMUNI"
            edDummy.Text = ""
            NTSZOOM.strIn = NTSCStr(edDummy.Text)
            NTSZOOM.ZoomStrIn("ZOOMCOMUNI", DittaCorrente, oParam)
            If NTSZOOM.strIn <> edDummy.Text Then
              edDummy.Text = NTSZOOM.strIn
              If oMenu.ValCodiceDb(edDummy.Text, DittaCorrente, "COMUNI", "S", "", dttTmp) = True Then
                edNuovoValore.Text = NTSCStr(dttTmp.Rows(0)!co_cap)
              End If
            End If
          Case Else
            '------------------------------------
            'zoom standard di textbox e griglia
            NTSCallStandardZoom()
        End Select
      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        NTSCallStandardZoom()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
  End Sub
  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub

#End Region

#Region "Validazione campi"
  Public Overridable Sub edNuovoValore_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
    Dim strTmp As String = ""
    Try
      If oCleVaan Is Nothing Then Return
      If Not CheckValore(edNuovoValore) Then Exit Sub

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edValore_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
    Dim strTmp As String = ""
    Try
      If oCleVaan Is Nothing Then Return
      If Not CheckValore(edValore) Then Exit Sub

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub InizializzaControlli(ByVal edText As NTSTextBoxStr, ByVal cbText As NTSComboBox)
    Dim dttTmp As New DataTable
    Try
      '--------------------------------------------------------------------------
      ' Resetta gli EditBox e i ComboBox
      '--------------------------------------------------------------------------
      AzzeraControlli(edText, cbText)
      '--------------------------------------------------------------------------
      ' Prende il nome del campo di ANAGRA selezionato
      '--------------------------------------------------------------------------
      oCleVaan.strCampo = Mid(cbCampi.Text, 1, (InStr(1, cbCampi.Text, " -") - 1))
      oCleVaan.strDescr = Microsoft.VisualBasic.Right(cbCampi.Text, Len(cbCampi.Text) - InStr(1, cbCampi.Text, " -") - 2)
      '--------------------------------------------------------------------------
      Select Case oCleVaan.strCampo
        '--- String
        Case "an_prov"
          edText.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
          edText.Properties.MaxLength = 2
          edText.Width = 50
          GctlSetVisEnab(edText, False)
          GctlSetVisEnab(edText, True)
        Case "an_porto"
          edText.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
          edText.Properties.MaxLength = 3
          edText.Width = 50
          GctlSetVisEnab(edText, False)
          GctlSetVisEnab(edText, True)
        Case "an_rating"
          edText.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
          edText.Properties.MaxLength = 3
          edText.Width = 100
          GctlSetVisEnab(edText, False)
          GctlSetVisEnab(edText, True)
        Case "an_codvfde"
          edText.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
          edText.Properties.MaxLength = 4
          edText.Width = 100
          GctlSetVisEnab(edText, False)
          GctlSetVisEnab(edText, True)
        Case "an_numdic", "an_numdicp"
          edText.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
          edText.Properties.MaxLength = 7
          edText.Width = 200
          GctlSetVisEnab(edText, False)
          GctlSetVisEnab(edText, True)
        Case "an_kpccee", "an_kpccee2", "an_rifricd", "an_rifrica"
          edText.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
          edText.Properties.MaxLength = 8
          edText.Width = 200
          GctlSetVisEnab(edText, False)
          GctlSetVisEnab(edText, True)
        Case "an_datdic", "an_datdicp", "an_scaddic"
          edText.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
          edText.Properties.MaxLength = 10
          edText.Width = 200
          GctlSetVisEnab(edText, False)
          GctlSetVisEnab(edText, True)
        Case "an_cap"
          edText.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
          edText.Properties.MaxLength = 9
          edText.Width = 200
          GctlSetVisEnab(edText, False)
          GctlSetVisEnab(edText, True)
        Case "an_coddicv", "an_coddica"
          edText.Properties.MaxLength = 12
          edText.Width = 220
          GctlSetVisEnab(edText, False)
          GctlSetVisEnab(edText, True)
        Case "an_privato"
          edText.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
          edText.Properties.MaxLength = 1
          edText.Width = 50
          GctlSetVisEnab(edText, False)
          GctlSetVisEnab(edText, True)
          edText.Text = "N"
          '--- Integer
        Case "an_zona", "an_categ", "an_codese", "an_codpag", "an_agente", _
            "an_listino", "an_valuta", "an_claprov", "an_clascon", "an_vett", "an_agente2", _
            "an_codling", "an_codbanc", "an_agcontrop", "an_codntra", "an_codcana", _
            "an_codtpbf", "an_vett2", "an_codtcdc"
          edText.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
          edText.Properties.MaxLength = 4
          edText.Width = 100
          GctlSetVisEnab(edText, False)
          GctlSetVisEnab(edText, True)
          edText.Text = "0"
          '--- Altri Integer
        Case "an_mesees1", "an_mesees2", "an_giofiss"
          edText.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
          edText.Properties.MaxLength = 2
          edText.Width = 100
          GctlSetVisEnab(edText, False)
          GctlSetVisEnab(edText, True)
          edText.Text = "0"
          '--- Long
        Case "an_controp"
          edText.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
          edText.Width = 200
          edText.Properties.MaxLength = 9
          GctlSetVisEnab(edText, False)
          GctlSetVisEnab(edText, True)
          edText.Text = "0"
          '--- Valuta
        Case "an_maxdic"
          edText.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
          edText.Width = 300
          edText.Properties.MaxLength = 12
          GctlSetVisEnab(edText, False)
          GctlSetVisEnab(edText, True)
          edText.Text = "0"
          '--- Combo (con valori Sì-No)
        Case "an_spinc", "an_bolli", "an_webvis"
          dttTmp.Columns.Add("cod", GetType(String))
          dttTmp.Columns.Add("val", GetType(String))
          dttTmp.Rows.Add(New Object() {"", "Sì"})
          dttTmp.Rows.Add(New Object() {"", "No"})
          dttTmp.AcceptChanges()
          cbText.DataSource = dttTmp
          cbText.ValueMember = "cod"
          cbText.DisplayMember = "val"
          GctlSetVisEnab(cbText, False)
          GctlSetVisEnab(cbText, True)
          '--- Altri Combo...
        Case "an_fatt"
          dttTmp.Columns.Add("cod", GetType(String))
          dttTmp.Columns.Add("val", GetType(String))
          dttTmp.Rows.Add(New Object() {"", "Accompagnatoria"})
          dttTmp.Rows.Add(New Object() {"", "Per bolla"})
          dttTmp.Rows.Add(New Object() {"", "Riep. per Destin."})
          dttTmp.Rows.Add(New Object() {"", "Riepilogativa"})
          dttTmp.AcceptChanges()
          cbText.DataSource = dttTmp
          cbText.ValueMember = "cod"
          cbText.DisplayMember = "val"
          GctlSetVisEnab(cbText, False)
          GctlSetVisEnab(cbText, True)
        Case "an_gcons"
          dttTmp.Columns.Add("cod", GetType(String))
          dttTmp.Columns.Add("val", GetType(String))
          dttTmp.Rows.Add(New Object() {"", "(Qualsiasi)"})
          dttTmp.Rows.Add(New Object() {"", "Lunedì"})
          dttTmp.Rows.Add(New Object() {"", "Martedì"})
          dttTmp.Rows.Add(New Object() {"", "Mercoledì"})
          dttTmp.Rows.Add(New Object() {"", "Giovedì"})
          dttTmp.Rows.Add(New Object() {"", "Venerdì"})
          dttTmp.Rows.Add(New Object() {"", "Sabato"})
          dttTmp.Rows.Add(New Object() {"", "Domenica"})
          dttTmp.AcceptChanges()
          cbText.DataSource = dttTmp
          cbText.ValueMember = "cod"
          cbText.DisplayMember = "val"
          GctlSetVisEnab(cbText, False)
          GctlSetVisEnab(cbText, True)
        Case "an_usaem"
          dttTmp.Columns.Add("cod", GetType(String))
          dttTmp.Columns.Add("val", GetType(String))
          dttTmp.Rows.Add(New Object() {"", "Microsoft Fax"})
          dttTmp.Rows.Add(New Object() {"", "E-mail Internet"})
          dttTmp.Rows.Add(New Object() {"", "WinFax PRO Symantec"})
          dttTmp.AcceptChanges()
          cbText.DataSource = dttTmp
          cbText.ValueMember = "cod"
          cbText.DisplayMember = "val"
          GctlSetVisEnab(cbText, False)
          GctlSetVisEnab(cbText, True)
        Case "an_status"
          dttTmp.Columns.Add("cod", GetType(String))
          dttTmp.Columns.Add("val", GetType(String))
          dttTmp.Rows.Add(New Object() {"", "Attivo"})
          dttTmp.Rows.Add(New Object() {"", "Inattivo"})
          dttTmp.Rows.Add(New Object() {"", "Potenziale"})
          dttTmp.AcceptChanges()
          cbText.DataSource = dttTmp
          cbText.ValueMember = "cod"
          cbText.DisplayMember = "val"
          GctlSetVisEnab(cbText, False)
          GctlSetVisEnab(cbText, True)
        Case "an_perfatt"
          dttTmp.Columns.Add("cod", GetType(String))
          dttTmp.Columns.Add("val", GetType(String))
          dttTmp.Rows.Add(New Object() {"", "(Qualsiasi)"})
          dttTmp.Rows.Add(New Object() {"", "Giornaliera"})
          dttTmp.Rows.Add(New Object() {"", "Mensile"})
          dttTmp.Rows.Add(New Object() {"", "Quattordicinale"})
          dttTmp.Rows.Add(New Object() {"", "Settimanale"})
          dttTmp.AcceptChanges()
          cbText.DataSource = dttTmp
          cbText.ValueMember = "cod"
          cbText.DisplayMember = "val"
          GctlSetVisEnab(cbText, False)
          GctlSetVisEnab(cbText, True)
        Case "an_tpsogiva"
          dttTmp.Columns.Add("cod", GetType(String))
          dttTmp.Columns.Add("val", GetType(String))
          dttTmp.Rows.Add(New Object() {"", "Normale"})
          dttTmp.Rows.Add(New Object() {"", "Escluso da Iva 11"})
          dttTmp.Rows.Add(New Object() {"", "Intracee"})
          dttTmp.Rows.Add(New Object() {"", "Extracee"})
          dttTmp.Rows.Add(New Object() {"", "R.S.M."})
          dttTmp.Rows.Add(New Object() {"", "Dogana"})
          dttTmp.AcceptChanges()
          cbText.DataSource = dttTmp
          cbText.ValueMember = "cod"
          cbText.DisplayMember = "val"
          GctlSetVisEnab(cbText, False)
          GctlSetVisEnab(cbText, True)
        Case "an_trating"
          dttTmp.Columns.Add("cod", GetType(String))
          dttTmp.Columns.Add("val", GetType(String))
          dttTmp.Rows.Add(New Object() {"", "Inderogabile"})
          dttTmp.Rows.Add(New Object() {"", "Certo"})
          dttTmp.Rows.Add(New Object() {"", "Incerto"})
          dttTmp.Rows.Add(New Object() {"", "Inattendibile"})
          dttTmp.Rows.Add(New Object() {"", "Non gestito"})
          dttTmp.AcceptChanges()
          cbText.DataSource = dttTmp
          cbText.ValueMember = "cod"
          cbText.DisplayMember = "val"
          GctlSetVisEnab(cbText, False)
          GctlSetVisEnab(cbText, True)
      End Select

      SettaTabellaZoom()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Function CheckValore(ByVal edText As NTSTextBoxStr) As Boolean
    Dim strLabel As String
    Try
      CheckValore = False
      If edText.Name = "edValore" Then strLabel = "Valore da sostituire" Else strLabel = "Nuovo valore da inserire"
      Select Case oCleVaan.strCampo
        Case "an_listino"
          If Not ValNum(edText, strLabel, -2, 9999) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128830657805699813, "Il valore del campo da cambiare deve avere un valore compreso tra -2 e 9999"))
            edText.Text = "0"
            edText.Focus()
            Exit Function
          End If
          If NTSCInt(edText.Text) > 0 Then
            If Not oCleVaan.CheckInTable(oCleVaan.strCampo, NTSCStr(edText.Text)) Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128830657839919001, "Il valore del campo da cambiare deve essere presente nella tabella collegata"))
              edText.Text = "0"
              edText.Focus()
              Exit Function
            End If
          End If
        Case "an_zona", "an_categ", "an_codese", "an_codpag", "an_agente", _
            "an_valuta", "an_claprov", "an_clascon", "an_vett", "an_agente2", _
            "an_codling", "an_codbanc", "an_codntra", "an_agcontrop", "an_codcana", _
            "an_codtpbf", "an_vett2", "an_codtcdc"
          If Not ValNum(edText, strLabel, 0, 9999) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128830657877888237, "Il valore del campo da cambiare deve avere un valore compreso tra 0 e 9999"))
            edText.Text = "0"
            edText.Focus()
            Exit Function
          End If
          If Val(edText.Text) > 0 Then
            If Not oCleVaan.CheckInTable(oCleVaan.strCampo, NTSCStr(edText.Text)) Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128830657909294889, "Il valore del campo da cambiare deve essere presente nella tabella collegata"))
              edText.Text = "0"
              edText.Focus()
              Exit Function
            End If
          End If
        Case "an_mesees1", "an_mesees2"
          If Not ValNum(edText, strLabel, 0, 12) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128830657945232849, "Il valore del campo da cambiare deve avere un valore compreso tra 0 e 12"))
            edText.Text = "0"
            edText.Focus()
            Exit Function
          End If
        Case "an_giofiss"
          If Not ValNum(edText, strLabel, 0, 31) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128830657981952069, "Il valore del campo da cambiare deve avere un valore compreso tra 0 e 31"))
            edText.Text = "0"
            edText.Focus()
            Exit Function
          End If
        Case "an_maxdic"
          If Not ValNum(edText, strLabel, 0, 999999999999) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128830658018983793, "Il valore del campo da cambiare deve avere un valore compreso tra 0 e 999999999999"))
            edText.Text = "0"
            edText.Focus()
            Exit Function
          End If
        Case "an_controp"
          If Not ValNum(edText, strLabel, 0, 999999999) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128830658056484273, "Il valore del campo da cambiare deve avere un valore compreso tra 0 e 999999999"))
            edText.Text = "0"
            edText.Focus()
            Exit Function
          End If
          If Val(edText.Text) > 0 Then
            If Not oCleVaan.CheckInTable(oCleVaan.strCampo, NTSCStr(edText.Text)) Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128830658095078517, "Il valore del campo da cambiare deve essere presente nella tabella collegata"))
              edText.Text = "0"
              edText.Focus()
              Exit Function
            End If
          End If
        Case "an_porto", "an_coddica", "an_coddicv", "an_codvfde"
          If edText.Text <> "" Then
            If Not oCleVaan.CheckInTable(oCleVaan.strCampo, NTSCStr(edText.Text)) Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128830658134454021, "Il valore del campo da cambiare deve essere presente nella tabella collegata"))
              edText.Text = ""
              edText.Focus()
              Exit Function
            End If
          End If
        Case "an_datdic", "an_datdicp", "an_scaddic"
          If Not ValData(edText, strLabel, False) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128830658167266941, "Il valore del campo da cambiare deve essere una data valida"))
            edText.Text = ""
            edText.Focus()
            Exit Function
          End If
        Case "an_privato"
          edText.Text = edText.Text.ToUpper
          If edText.Text <> "S" AndAlso edText.Text <> "N" Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129664342947963190, "Il valore del campo da cambiare deve essere 'S' oppure 'N'"))
            edText.Text = "N"
            edText.Focus()
            Exit Function
          End If

      End Select
      CheckValore = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub AzzeraControlli(ByVal edText As NTSTextBoxStr, ByVal cbText As NTSComboBox)
    edText.Text = ""
    edText.Enabled = False
    cbText.Enabled = False
    edText.Visible = False
    cbText.Visible = False
  End Sub

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer

    Try
      '--------------------------------------------------
      'preparo il motore di stampa
      strCrpe = "{TTINVENT.instid} = " & oCleVaan.lIITTInvent & _
                " And {TTINVENT.codditt} = '" & DittaCorrente & "'"
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BS--VAAN", "Reports1", " ", 0, nDestin, "BS--VAAN.RPT", False, "Variazione Campi Anagrafica Clienti/Fornitori/Sottoconti", False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce già il multireport)
      For i = 1 To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function ValNum(ByVal edCampo As NTSTextBoxStr, ByVal strNomCampo As String, ByVal nMin As Decimal, ByVal nMax As Decimal) As Boolean
    ' valida un numer specificando anche quanti decimali
    Dim dComodo As Decimal
    Try

      ValNum = True
      If Not IsNumeric(edCampo.Text) Then
        Return False
      End If
      ' passa per un double
      dComodo = NTSCDec(edCampo.Text)
      If Not (NTSCDec(edCampo.Text) >= nMin And NTSCDec(edCampo.Text) <= nMax) Then Return False

      Exit Function

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function ValData(ByVal edDt As NTSTextBoxStr, ByVal strNomCampo As String, ByVal nObbl As Boolean) As Boolean
    Dim strDataOut As String, strDataTmp As String
    Dim IntTrovaSepDate As String
    ValData = True
    Try
      IntTrovaSepDate = "/"

      If edDt.Text = "" And nObbl = False Then
        Return True
      End If

      Select Case Len(edDt.Text)
        Case 6
          If Not IsNumeric(edDt.Text) Then
            Return False
          Else
            'Se viene passata una data con 2 cifre x l'anno la converte in 4
            'solo x il controllo della IsDate
            strDataOut = Mid(edDt.Text, 1, 2) & IntTrovaSepDate & Mid(edDt.Text, 3, 2) & IntTrovaSepDate
            strDataTmp = strDataOut
            strDataOut = strDataOut & Mid(edDt.Text, 5)
            If NTSCInt(Mid(edDt.Text, 5)) <= 29 Then
              strDataTmp = strDataTmp & "20" & Mid(edDt.Text, 5)
            Else
              strDataTmp = strDataTmp & "19" & Mid(edDt.Text, 5)
            End If
            If Not IsDate(strDataTmp) Then
              Return False
            End If
            edDt.Text = strDataOut
          End If
        Case 8
          If IsNumeric(edDt.Text) Then
            strDataOut = Mid(edDt.Text, 1, 2) & IntTrovaSepDate & Mid(edDt.Text, 3, 2) & IntTrovaSepDate & Mid(edDt.Text, 5)
            edDt.Text = strDataOut
          Else
            If Not ((IsNumeric(Mid(edDt.Text, 1, 2))) And (Mid(edDt.Text, 3, 1) = IntTrovaSepDate) And (IsNumeric(Mid(edDt.Text, 4, 2))) And (Mid(edDt.Text, 6, 1) = IntTrovaSepDate) And (IsNumeric(Mid(edDt.Text, 7, 2)))) Then
              Return False
            End If
            'Data con anno a 2 cifre
            'Appurato che ci sono le barre al posto giusto, con anno a 2 cifre
            strDataOut = edDt.Text
            If NTSCInt(Microsoft.VisualBasic.Right(strDataOut, 2)) <= 29 Then
              strDataTmp = Microsoft.VisualBasic.Left(strDataOut, 6) & "20" & Microsoft.VisualBasic.Right(strDataOut, 2)
            Else
              strDataTmp = Microsoft.VisualBasic.Left(strDataOut, 6) & "19" & Microsoft.VisualBasic.Right(strDataOut, 2)
            End If
            If Not IsDate(strDataTmp) Then
              Return False
            End If
          End If
        Case 10
          If Not ((IsNumeric(Mid(edDt.Text, 1, 2))) And (Mid(edDt.Text, 3, 1) = IntTrovaSepDate) And (IsNumeric(Mid(edDt.Text, 4, 2))) And (Mid(edDt.Text, 6, 1) = IntTrovaSepDate) And (IsNumeric(Mid(edDt.Text, 7, 4)))) Then
            Return False
          End If
        Case Else
          Return False
      End Select

      If Not IsDate(edDt.Text) Then
        Return False
      Else
        If NTSCInt(Mid(edDt.Text, 4, 2)) > 12 Then
          Return False
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function Elabora(ByVal nTipoElaborazione As Integer) As Boolean
    Dim lRecords As Integer
    Dim oParam As New CLE__CLDP
    Dim frmAutm As FRM__AUTM = Nothing
    Try

      Me.Cursor = Cursors.WaitCursor

      If ckElaborazione.Checked And ckElabGri.Checked Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129828391773887574, "Non selezionare entrambe le spunte 'Elaborazione manuale' e 'Elaborazione in griglia'"))
        Me.Cursor = Cursors.Default
        Exit Function
      End If

      If edNuovoValore.Visible Then
        If Not CheckValore(edNuovoValore) Then
          Me.Cursor = Cursors.Default
          Exit Function
        End If

      End If
      If edValore.Visible Then
        If Not CheckValore(edValore) Then
          Me.Cursor = Cursors.Default
          Exit Function
        End If
      End If

      If ckElabGri.Checked = False Then
        If oCleVaan.Elabora(lRecords, nTipoElaborazione, ckSeleziona.Checked, cbValore.Visible, cbValore.Text, cbNuovoValore.Text, edValore.Text, edNuovoValore.Text, cbNuovoValore.SelectedIndex, edValore.Visible, cbValore.SelectedIndex, edDataelab.Text, cbCampi.Text, cbNuovoValore.Visible, ckElaborazione.Checked) Then
          If lRecords > 0 Then
            If lRecords = 1 Then
              oApp.MsgBoxInfo(oApp.Tr(Me, 128503235626143857, "Elaborazione terminata." & vbCrLf & "Aggiornato n° 1 conto."))
            Else
              oApp.MsgBoxInfo(oApp.Tr(Me, 128503235667758477, "Elaborazione terminata." & vbCrLf & "Aggiornati n° |" & lRecords & "| conti."))
            End If
          End If
          lbInfo.Text = strInfoMess
          Me.Cursor = Cursors.Default
          Return True
        Else
          lbInfo.Text = strInfoMess
          Me.Cursor = Cursors.Default
          Return False
        End If
      Else
        oCleVaan.bAutmcbValoreVis = cbValore.Visible
        oCleVaan.bAutmckSeleziona = ckSeleziona.Checked
        oCleVaan.nAutmcbNuovoValore = cbNuovoValore.SelectedIndex
        oCleVaan.strAutmedNuovoValore = edNuovoValore.Text
        oCleVaan.strAutmedValore = edValore.Text
        oCleVaan.bAutmedValoreVis = edValore.Visible
        oCleVaan.nAutmcbValore = cbValore.SelectedIndex
        oCleVaan.strAutmcbValore = cbValore.Text
        oCleVaan.strAutmcbNuovoValore = cbNuovoValore.Text

        If Not oCleVaan.TestPreElabora(ckSeleziona.Checked, cbValore.Visible, cbValore.Text, cbNuovoValore.Text, edValore.Text, edNuovoValore.Text) Then
          Me.Cursor = Cursors.Default
          Return False
        End If

        frmAutm = CType(NTSNewFormModal("FRM__AUTM"), FRM__AUTM)
        frmAutm.Init(oMenu, oParam, DittaCorrente)
        frmAutm.InitEntity(oCleVaan)
        frmAutm.ShowDialog()
      End If

      Me.Cursor = Cursors.Default
      Return True

    Catch ex As Exception
      lbInfo.Text = strInfoMess
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmAutm Is Nothing Then frmAutm.Dispose()
      frmAutm = Nothing
    End Try
  End Function

  Public Overridable Function Salta() As Boolean
    Dim oParam As New CLE__CLDP
    Dim frmInfm As FRM__INFM = Nothing
    Try

      frmInfm = CType(NTSNewFormModal("FRM__INFM"), FRM__INFM)
      frmInfm.nInfmRichiesta = 0
      frmInfm.strInfmConto = oCleVaan.strConto
      frmInfm.strInfmDescr1 = oCleVaan.strDescr1
      frmInfm.Init(oMenu, oParam, DittaCorrente)
      frmInfm.InitEntity(oCleVaan)
      frmInfm.ShowDialog()

      oCleVaan.nRichiesta = frmInfm.nInfmRichiesta

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmInfm Is Nothing Then frmInfm.Dispose()
      frmInfm = Nothing
    End Try
  End Function

  Public Overridable Function SettaTabellaZoom() As Boolean
    Try
      edValore.NTSForzaVisZoom = False
      edNuovoValore.NTSForzaVisZoom = False

      Select Case oCleVaan.strCampo
        Case "an_cap"
          strTabella = "COMUNI"
          edValore.NTSForzaVisZoom = True
          edNuovoValore.NTSForzaVisZoom = True
        Case "an_controp"
          strTabella = "ANAGRA"
          edValore.NTSForzaVisZoom = True
          edNuovoValore.NTSForzaVisZoom = True
        Case "an_zona"
          strTabella = "Zone"
          edValore.NTSSetParamZoom("ZOOMTABZONE")
          edNuovoValore.NTSSetParamZoom("ZOOMTABZONE")
        Case "an_categ"
          strTabella = "Cate"
          edValore.NTSSetParamZoom("ZOOMTABCATE")
          edNuovoValore.NTSSetParamZoom("ZOOMTABCATE")
        Case "an_codese"
          strTabella = "Civa"
          edValore.NTSSetParamZoom("ZOOMTABCIVA")
          edNuovoValore.NTSSetParamZoom("ZOOMTABCIVA")
        Case "an_codpag"
          strTabella = "Paga"
          edValore.NTSSetParamZoom("ZOOMTABPAGA")
          edNuovoValore.NTSSetParamZoom("ZOOMTABPAGA")
        Case "an_agente", "an_agente2"
          strTabella = "Cage"
          edValore.NTSSetParamZoom("ZOOMTABCAGE")
          edNuovoValore.NTSSetParamZoom("ZOOMTABCAGE")
        Case "an_valuta"
          strTabella = "Valu"
          edValore.NTSSetParamZoom("ZOOMTABVALU")
          edNuovoValore.NTSSetParamZoom("ZOOMTABVALU")
        Case "an_claprov"
          strTabella = "Cpcl"
          edValore.NTSSetParamZoom("ZOOMTABCPCL")
          edNuovoValore.NTSSetParamZoom("ZOOMTABCPCL")
        Case "an_clascon"
          strTabella = "Cscl"
          edValore.NTSSetParamZoom("ZOOMTABCSCL")
          edNuovoValore.NTSSetParamZoom("ZOOMTABCSCL")
        Case "an_porto"
          strTabella = "Port"
          edValore.NTSSetParamZoom("ZOOMTABPORT")
          edNuovoValore.NTSSetParamZoom("ZOOMTABPORT")
        Case "an_vett", "an_vett2"
          strTabella = "Vett"
          edValore.NTSSetParamZoom("ZOOMTABVETT")
          edNuovoValore.NTSSetParamZoom("ZOOMTABVETT")
        Case "an_codling"
          strTabella = "Ling"
          edValore.NTSSetParamZoom("ZOOMTABLING")
          edNuovoValore.NTSSetParamZoom("ZOOMTABLING")
        Case "an_codbanc"
          strTabella = "Banc"
          edValore.NTSSetParamZoom("ZOOMTABBANC")
          edNuovoValore.NTSSetParamZoom("ZOOMTABBANC")
        Case "an_codntra"
          strTabella = "Ntra"
          edValore.NTSSetParamZoom("ZOOMTABNTRA")
          edNuovoValore.NTSSetParamZoom("ZOOMTABNTRA")
        Case "an_codcana"
          strTabella = "Cana"
          edValore.NTSSetParamZoom("ZOOMTABCANA")
          edNuovoValore.NTSSetParamZoom("ZOOMTABCANA")
        Case "an_codtpbf"
          strTabella = "Tpbf"
          edValore.NTSSetParamZoom("ZOOMTABTPBF")
          edNuovoValore.NTSSetParamZoom("ZOOMTABTPBF")
        Case "an_codtcdc"
          strTabella = "Tcdc"
          edValore.NTSSetParamZoom("ZOOMTABTCDC")
          edNuovoValore.NTSSetParamZoom("ZOOMTABTCDC")
        Case "an_coddica"
          strTabella = "Dica"
          edValore.NTSSetParamZoom("ZOOMTABDICA")
          edNuovoValore.NTSSetParamZoom("ZOOMTABDICA")
        Case "an_codvfde"
          strTabella = "Vfde"
          edValore.NTSSetParamZoom("ZOOMTABVFDE")
          edNuovoValore.NTSSetParamZoom("ZOOMTABVFDE")
        Case Else
          strTabella = ""
          edValore.NTSSetParamZoom("")
          edNuovoValore.NTSSetParamZoom("")
          Exit Function
      End Select

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

End Class

