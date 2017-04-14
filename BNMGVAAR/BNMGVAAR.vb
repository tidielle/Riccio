Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGVAAR
  Private Moduli_P As Integer = bsModMG + bsModVE + bsModPM
  Private ModuliExt_P As Integer = bsModExtMGE + bsModExtCRM
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
  Public oCleVaar As CLEMGVAAR
  Public oCallParams As CLE__CLDP

  Public strTabella As String 'tabella per effettuare lo zoom
  Public strInfoMess As String = ""

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
            lbInfo.Text = oApp.Tr(Me, 129036398931975834, "Aggiornamento |" & Mid(cbCampi.Text, (InStr(1, cbCampi.Text, "- ") + 2)) & "| in corso...")
            Me.Refresh()
          Case "SALTAARTI:"
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGVAAR))
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
    Me.fmElaborazione.Size = New System.Drawing.Size(284, 50)
    Me.fmElaborazione.TabIndex = 107
    '
    'ckElabGri
    '
    Me.ckElabGri.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckElabGri.Location = New System.Drawing.Point(145, 23)
    Me.ckElabGri.Name = "ckElabGri"
    Me.ckElabGri.NTSCheckValue = "S"
    Me.ckElabGri.NTSUnCheckValue = "N"
    Me.ckElabGri.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckElabGri.Properties.Appearance.Options.UseBackColor = True
    Me.ckElabGri.Properties.Caption = "Elaborazione in griglia"
    Me.ckElabGri.Size = New System.Drawing.Size(128, 19)
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
    Me.cmdSeleziona.Text = "Seleziona Articoli"
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
    'FRMMGVAAR
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(499, 253)
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
    Me.Name = "FRMMGVAAR"
    Me.Text = "VARIAZIONE CAMPI ANAGRAFICA ARTICOLI"
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGVAAR", "BEMGVAAR", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128498740238262677, "ERRORE in fase di creazione Entity:" & vbCrLf) & strErr)
      Return False
    End If
    oCleVaar = CType(oTmp, CLEMGVAAR)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGVAAR", strRemoteServer, strRemotePort)
    AddHandler oCleVaar.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleVaar.Init(oApp, oScript, oMenu.oCleComm, "MOVMAG", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbElabora.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
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
      ckElabGri.NTSSetParam(oMenu, oApp.Tr(Me, 129832897161561945, "Elaborazione griglia"), "S", "N")

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
    Dim dttCampiF As New DataTable()
    Try
      If CLN__STD.FRIENDLY = False Then
        dttCampi.Columns.Add("cod", GetType(String))
        dttCampi.Columns.Add("val", GetType(String))

        dttCampi.Rows.Add(New Object() {"1", "ar_codappr - Approvvigionatore"})
        If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupCEC) Then
          dttCampi.Rows.Add(New Object() {"57", "ar_webvis - Articolo visibile dall'applicazione esterna"})
          dttCampi.Rows.Add(New Object() {"58", "ar_webvend - Articolo vendibile da applicazione esterna"})
        End If
        dttCampi.Rows.Add(New Object() {"2", "ar_blocco - Blocco"})
        dttCampi.Rows.Add(New Object() {"3", "ar_claprov - Classe provvigione"})
        dttCampi.Rows.Add(New Object() {"4", "ar_clascon - Classe sconto"})
        dttCampi.Rows.Add(New Object() {"52", "ar_codcla1 - Codice classificazione 1"})
        dttCampi.Rows.Add(New Object() {"53", "ar_codcla2 - Codice classificazione 2"})
        dttCampi.Rows.Add(New Object() {"54", "ar_codcla3 - Codice classificazione 3"})
        dttCampi.Rows.Add(New Object() {"55", "ar_codcla4 - Codice classificazione 4"})
        dttCampi.Rows.Add(New Object() {"56", "ar_codcla5 - Codice classificazione 5"})
        dttCampi.Rows.Add(New Object() {"5", "ar_codiva - Codice IVA"})
        dttCampi.Rows.Add(New Object() {"6", "ar_codnomc - Codice nomenclatura combinata"})
        If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupCEC) Then
          dttCampi.Rows.Add(New Object() {"59", "ar_codseat - Codice Set di Attributi"})
        End If
        dttCampi.Rows.Add(New Object() {"7", "ar_controa - Contropartita acquisti"})
        dttCampi.Rows.Add(New Object() {"8", "ar_contros - Contropartita scarico produzione"})
        dttCampi.Rows.Add(New Object() {"9", "ar_controp - Contropartita vendite"})
        dttCampi.Rows.Add(New Object() {"10", "ar_famprod - Famiglia"})
        dttCampi.Rows.Add(New Object() {"11", "ar_fcorrlt - Fattore correzione L.T."})
        dttCampi.Rows.Add(New Object() {"12", "ar_forn - Fornitore 1"})
        dttCampi.Rows.Add(New Object() {"13", "ar_forn2 - Fornitore 2"})
        dttCampi.Rows.Add(New Object() {"14", "ar_fpfence - FP Fence"})
        dttCampi.Rows.Add(New Object() {"15", "ar_maxlotto - Gestione Lotti: lotto massimo"})
        dttCampi.Rows.Add(New Object() {"16", "ar_sublotto - Gestione Lotti: sublotto"})
        dttCampi.Rows.Add(New Object() {"17", "ar_ggrior - Giorni Lead Time"})
        dttCampi.Rows.Add(New Object() {"18", "ar_ggragg - Giorni raggruppamento"})
        dttCampi.Rows.Add(New Object() {"19", "ar_ggant - Giorni di Tolleranza Anticipo"})
        dttCampi.Rows.Add(New Object() {"20", "ar_ggpost - Giorni di Tolleranza Posticipo"})
        dttCampi.Rows.Add(New Object() {"21", "ar_gruppo - Gruppo merciologico"})
        dttCampi.Rows.Add(New Object() {"22", "ar_magprod - Magazzino produzione"})
        dttCampi.Rows.Add(New Object() {"23", "ar_magstock - Magazzino stoccaggio"})
        dttCampi.Rows.Add(New Object() {"24", "ar_codmarc - Marca"})
        dttCampi.Rows.Add(New Object() {"25", "ar_misura1 - Misura 1"})
        dttCampi.Rows.Add(New Object() {"26", "ar_misura2 - Misura 2"})
        dttCampi.Rows.Add(New Object() {"27", "ar_misura3 - Misura 3"})
        dttCampi.Rows.Add(New Object() {"28", "ar_pesoca - Non proporre le note art.sulle righe docum."})
        dttCampi.Rows.Add(New Object() {"29", "ar_perragg - Periodo raggruppamento"})
        dttCampi.Rows.Add(New Object() {"30", "ar_pesolor - Peso lordo"})
        dttCampi.Rows.Add(New Object() {"31", "ar_pesonet - Peso netto"})
        dttCampi.Rows.Add(New Object() {"32", "ar_polriord - Politica di Riordino"})
        dttCampi.Rows.Add(New Object() {"33", "ar_minord - Qta Lotto std pr/ac."})
        dttCampi.Rows.Add(New Object() {"34", "ar_codpdon - Relazione listini"})
        dttCampi.Rows.Add(New Object() {"35", "ar_reparto - Reparto (ECR)"})
        dttCampi.Rows.Add(New Object() {"36", "ar_ricar1 - Ricarico/Margine 1"})
        dttCampi.Rows.Add(New Object() {"37", "ar_ricar2 - Ricarico/Margine 2"})
        dttCampi.Rows.Add(New Object() {"38", "ar_rrfence - RR Fence"})
        dttCampi.Rows.Add(New Object() {"39", "ar_scomax - Scorta massima"})
        dttCampi.Rows.Add(New Object() {"40", "ar_scomin - Scorta minima"})
        dttCampi.Rows.Add(New Object() {"41", "ar_sotgru - Sottogruppo merciologico"})
        dttCampi.Rows.Add(New Object() {"42", "ar_stainv - Stampa articolo nell'inventario"})
        dttCampi.Rows.Add(New Object() {"43", "ar_tipo - Tipo"})
        dttCampi.Rows.Add(New Object() {"44", "ar_tipoopz - Tipo Opzione"})
        dttCampi.Rows.Add(New Object() {"45", "ar_umdapra - U.M. carichi"})
        dttCampi.Rows.Add(New Object() {"46", "ar_umpdapra - U.M. prezzo acquisto"})
        dttCampi.Rows.Add(New Object() {"47", "ar_umpdapr - U.M. prezzo vendita"})
        dttCampi.Rows.Add(New Object() {"48", "ar_umdapr - U.M. vendite"})
        dttCampi.Rows.Add(New Object() {"49", "ar_percvst - % Valore statistico"})
        dttCampi.Rows.Add(New Object() {"50", "ar_volume - Volume"})
        dttCampi.Rows.Add(New Object() {"51", "ar_ubicaz - Ubicazione"})
        dttCampi.Rows.Add(New Object() {"60", "ar_consmrp - Considera in MRP/Distinte Base"})
        dttCampi.Rows.Add(New Object() {"60", "ar_deterior - Articolo deteriorabile"})
        If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupCEC) Then
        End If
        If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupCAE) Then
          dttCampi.Rows.Add(New Object() {"{", "ar_codtcdc - Tipologia entità"})
          dttCampi.Rows.Add(New Object() {"}", "ar_coddica - Aggregazione budget"})
          dttCampi.Rows.Add(New Object() {"¶", "ar_coddicv - Valore aggregazione budget"})
        End If

        dttCampi.AcceptChanges()
        cbCampi.DataSource = dttCampi

      Else
        dttCampiF.Columns.Add("cod", GetType(String))
        dttCampiF.Columns.Add("val", GetType(String))

        dttCampiF.Rows.Add(New Object() {"1", "ar_claprov - Classe provvigione"})
        dttCampiF.Rows.Add(New Object() {"2", "ar_clascon - Classe sconto"})
        dttCampiF.Rows.Add(New Object() {"3", "ar_codiva - Codice IVA"})
        dttCampiF.Rows.Add(New Object() {"4", "ar_controa - Contropartita acquisti"})
        dttCampiF.Rows.Add(New Object() {"5", "ar_controp - Contropartita vendite"})
        dttCampiF.Rows.Add(New Object() {"6", "ar_pesoca - Non proporre le note art.sulle righe docum."})
        dttCampiF.Rows.Add(New Object() {"7", "ar_famprod - Famiglia"})
        dttCampiF.Rows.Add(New Object() {"8", "ar_forn - Fornitore 1"})
        dttCampiF.Rows.Add(New Object() {"9", "ar_forn2 - Fornitore 2"})
        dttCampiF.Rows.Add(New Object() {"10", "ar_gruppo - Gruppo merceologico"})
        dttCampiF.Rows.Add(New Object() {"11", "ar_pesolor - Peso lordo"})
        dttCampiF.Rows.Add(New Object() {"12", "ar_pesonet - Peso netto"})
        dttCampiF.Rows.Add(New Object() {"13", "ar_codpdon - Relazione listini"})
        dttCampiF.Rows.Add(New Object() {"14", "ar_reparto - Reparto (ECR)"})
        dttCampiF.Rows.Add(New Object() {"15", "ar_scomax - Scorta massima"})
        dttCampiF.Rows.Add(New Object() {"16", "ar_scomin - Scorta minima"})
        dttCampiF.Rows.Add(New Object() {"17", "ar_sotgru - Sottogruppo merciologico"})
        dttCampiF.Rows.Add(New Object() {"18", "ar_tipo - Tipo"})

        dttCampiF.AcceptChanges()
        cbCampi.DataSource = dttCampiF
      End If

      cbCampi.ValueMember = "cod"
      cbCampi.DisplayMember = "val"

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRMMGVAAR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'carico i combobox
      CaricaCombo()

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()
      strInfoMess = oApp.Tr(Me, 130421095154407246, "Nessuna operazione in corso...")
      '------------------------------------------------------------
      oCleVaar.bSeleziona = False
      oCleVaar.strCampo = ""

      oCleVaar.lIITTInvent = oMenu.GetTblInstId("TTINVENT", False)
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

  Public Overridable Sub FRMMGVAAR_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If oCleVaar.bElabInCorso Then e.Cancel = True
  End Sub

  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Dim oPar As CLE__PATB = Nothing
    Try
      oPar = New CLE__PATB
      oPar.bVisGriglia = False
      oPar.strTipoArticolo = "N"
      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oPar)
      oCleVaar.strWhereFiar = oPar.strOut.Trim

      If oCleVaar.strWhereFiar = "" Then
        oCleVaar.bSeleziona = False
      Else
        oCleVaar.bSeleziona = True
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
        Case "}" : oApp.MsgBoxInfo(oApp.Tr(Me, 129321440026382136, "Promemoria:" & vbCrLf & _
                  "Durante il cambio dell'Aggregazione Budget verranno eliminati i Valori di Aggregazione Budget non associati all'Aggregazione Budget selezionata."))
        Case "¶" : oApp.MsgBoxInfo(oApp.Tr(Me, 129321441614423162, "Promemoria:" & vbCrLf & _
                  "Verranno modificati solo gli articoli con l'Aggregazione Budget compatibile con il Valore di Aggregazione Budget selezionato."))
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
            oParam.strTipo = "F"
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
            oParam.strTipo = "F"
            oParam.bTipoProposto = True
            NTSZOOM.strIn = edNuovoValore.Text
            NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
            If NTSZOOM.strIn <> edNuovoValore.Text Then edNuovoValore.NTSTextDB = NTSZOOM.strIn
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
  Public Overridable Sub edNuovoValore_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edNuovoValore.Validated
    Dim strTmp As String = ""
    Try
      If oCleVaar Is Nothing Then Return
      If Not CheckValore(edNuovoValore) Then Exit Sub

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edValore_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edValore.Validated
    Dim strTmp As String = ""
    Try
      If oCleVaar Is Nothing Then Return
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
      ' Prende il nome del campo di ARTICO selezionato
      '--------------------------------------------------------------------------
      oCleVaar.strCampo = Mid(cbCampi.Text, 1, (InStr(1, cbCampi.Text, " -") - 1))
      oCleVaar.strDescr = Microsoft.VisualBasic.Right(cbCampi.Text, Len(cbCampi.Text) - InStr(1, cbCampi.Text, " -") - 2)
      '--------------------------------------------------------------------------
      Select Case oCleVaar.strCampo
        '--- String
        Case "ar_tipo", "ar_famprod", "ar_codnomc", "ar_ubicaz", "ar_coddica", "ar_coddicv", _
             "ar_codcla1", "ar_codcla2", "ar_codcla3", "ar_codcla4", "ar_codcla5"
          edText.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
          If oCleVaar.strCampo = "ar_tipo" Then
            edText.Properties.MaxLength = 1
            edText.Width = 50
          End If
          If oCleVaar.strCampo = "ar_codcla1" Or oCleVaar.strCampo = "ar_codcla2" Or oCleVaar.strCampo = "ar_codcla3" Or _
             oCleVaar.strCampo = "ar_codcla4" Or oCleVaar.strCampo = "ar_codcla5" Then
            edText.Properties.MaxLength = 5
            edText.Width = 100
          End If
          If oCleVaar.strCampo = "ar_famprod" Then
            edText.Properties.MaxLength = 4
            edText.Width = 100
          End If
          If oCleVaar.strCampo = "ar_codnomc" Then
            edText.Properties.MaxLength = 10
            edText.Width = 200
          End If
          If oCleVaar.strCampo = "ar_coddicv" Or oCleVaar.strCampo = "ar_coddica" Then
            edText.Properties.MaxLength = 12
            edText.Width = 220
          End If
          If oCleVaar.strCampo = "ar_ubicaz" Then
            edText.Properties.MaxLength = 18
            edText.Width = 300
          End If
          GctlSetVisEnab(edText, False)
          GctlSetVisEnab(edText, True)
          Select Case oCleVaar.strCampo
            Case "ar_famprod", "ar_coddica", "ar_coddicv" : edText.Text = " "
            Case Else : edText.Text = ""
          End Select
          oCleVaar.strTipo = "S"
          '--- Integer lonughezz = 4
        Case "ar_codiva", "ar_gruppo", "ar_sotgru", "ar_controp", "ar_claprov", "ar_clascon", _
          "ar_controa", "ar_reparto", "ar_codpdon", "ar_fpfence", "ar_rrfence", "ar_contros", _
          "ar_magstock", "ar_magprod", "ar_codappr", "ar_codmarc", "ar_ggrior", "ar_ggant", _
          "ar_ggpost", "ar_ggragg", "ar_codtcdc", "ar_codseat"
          edText.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
          If (oCleVaar.strCampo = "ar_ggant") Or (oCleVaar.strCampo = "ar_ggpost") Or (oCleVaar.strCampo = "ar_ggragg") Or (oCleVaar.strCampo = "ar_fpfence") Then
            edText.Properties.MaxLength = 3
          Else
            edText.Properties.MaxLength = 4
          End If
          edText.Width = 100
          GctlSetVisEnab(edText, False)
          GctlSetVisEnab(edText, True)
          edText.Text = "0"
          oCleVaar.strTipo = "N"
          '--- Long
        Case "ar_forn", "ar_forn2"
          edText.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
          edText.Width = 200
          If (oCleVaar.strCampo = "ar_forn") Or (oCleVaar.strCampo = "ar_forn2") Then
            edText.Properties.MaxLength = 9
          Else
            edText.Properties.MaxLength = 8
          End If
          GctlSetVisEnab(edText, False)
          GctlSetVisEnab(edText, True)
          edText.Text = "0"
          oCleVaar.strTipo = "N"
          '--- Double
        Case "ar_ricar1", "ar_ricar2", "ar_percvst", "ar_fcorrlt", "ar_misura1", "ar_misura2", _
             "ar_misura3", "ar_volume"
          edText.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
          edText.Width = 200
          If (oCleVaar.strCampo = "ar_ricar1") Or (oCleVaar.strCampo = "ar_ricar2") Then
            edText.Properties.MaxLength = 8
          End If
          If oCleVaar.strCampo = "ar_percvst" Then
            edText.Properties.MaxLength = 9
          End If
          If oCleVaar.strCampo = "ar_fcorrlt" Then
            edText.Properties.MaxLength = 10
          End If
          If (oCleVaar.strCampo = "ar_misura1") Or (oCleVaar.strCampo = "ar_misura2") Or (oCleVaar.strCampo = "ar_misura3") Or (oCleVaar.strCampo = "ar_volume") Then
            edText.Properties.MaxLength = 12
          End If
          GctlSetVisEnab(edText, False)
          GctlSetVisEnab(edText, True)
          edText.Text = "0"
          oCleVaar.strTipo = "N"
          '--- Altri Double
        Case "ar_scomin", "ar_scomax", "ar_minord", "ar_sublotto", "ar_maxlotto"
          edText.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
          edText.Width = 200
          edText.Properties.MaxLength = 13
          GctlSetVisEnab(edText, False)
          GctlSetVisEnab(edText, True)
          edText.Text = "0"
          oCleVaar.strTipo = "N"
        Case "ar_pesolor", "ar_pesonet"
          edText.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
          edText.Width = 200
          edText.Properties.MaxLength = 11
          GctlSetVisEnab(edText, False)
          GctlSetVisEnab(edText, True)
          edText.Text = "0"
          oCleVaar.strTipo = "N"
          '--- Combo (con valori Sì-No)
        Case "ar_stainv", "ar_blocco", "ar_pesoca", "ar_webvis", "ar_webvend", "ar_consmrp"
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
        Case "ar_tipoopz"
          dttTmp.Columns.Add("cod", GetType(String))
          dttTmp.Columns.Add("val", GetType(String))
          dttTmp.Rows.Add(New Object() {"", "MP/SL Norm.(Reale)"})
          dttTmp.Rows.Add(New Object() {"", "Fittizio (B)"})
          dttTmp.Rows.Add(New Object() {"", "SL/PF Indef.(Fitt.)"})
          dttTmp.Rows.Add(New Object() {"", "Gruppo PF (Fittizio)"})
          dttTmp.Rows.Add(New Object() {"", "Prod.Finito (Reale)"})
          dttTmp.Rows.Add(New Object() {"", "Fittizio (Q)"})
          dttTmp.AcceptChanges()
          cbText.DataSource = dttTmp
          cbText.ValueMember = "cod"
          cbText.DisplayMember = "val"
          GctlSetVisEnab(cbText, False)
          GctlSetVisEnab(cbText, True)
        Case "ar_polriord"
          dttTmp.Columns.Add("cod", GetType(String))
          dttTmp.Columns.Add("val", GetType(String))
          dttTmp.Rows.Add(New Object() {"", "Su fabbisogno con lotto"})
          dttTmp.Rows.Add(New Object() {"", "Su fabbisogno puro"})
          dttTmp.Rows.Add(New Object() {"", "A punto di riordino con lotto"})
          dttTmp.Rows.Add(New Object() {"", "A punto di riord.a ric.scorta"})
          dttTmp.Rows.Add(New Object() {"", "Su fabbisogno con lotto min."})
          dttTmp.AcceptChanges()
          cbText.DataSource = dttTmp
          cbText.ValueMember = "cod"
          cbText.DisplayMember = "val"
          GctlSetVisEnab(cbText, False)
          GctlSetVisEnab(cbText, True)
        Case "ar_umdapr", "ar_umdapra", "ar_umpdapr", "ar_umpdapra"
          dttTmp.Columns.Add("cod", GetType(String))
          dttTmp.Columns.Add("val", GetType(String))
          dttTmp.Rows.Add(New Object() {"", "Principale"})
          dttTmp.Rows.Add(New Object() {"", "Confezione"})
          dttTmp.Rows.Add(New Object() {"", "Secondaria"})
          dttTmp.Rows.Add(New Object() {"", "Formula"})
          dttTmp.AcceptChanges()
          cbText.DataSource = dttTmp
          cbText.ValueMember = "cod"
          cbText.DisplayMember = "val"
          GctlSetVisEnab(cbText, False)
          GctlSetVisEnab(cbText, True)
        Case "ar_perragg"
          dttTmp.Columns.Add("cod", GetType(String))
          dttTmp.Columns.Add("val", GetType(String))
          dttTmp.Rows.Add(New Object() {"", "Giorno"})
          dttTmp.Rows.Add(New Object() {"", "Settimana"})
          dttTmp.Rows.Add(New Object() {"", "Decade"})
          dttTmp.Rows.Add(New Object() {"", "Quindicina"})
          dttTmp.Rows.Add(New Object() {"", "Mese"})
          dttTmp.Rows.Add(New Object() {"", "Bimestre"})
          dttTmp.Rows.Add(New Object() {"", "Trimestre"})
          dttTmp.Rows.Add(New Object() {"", "Quadrimestre"})
          dttTmp.Rows.Add(New Object() {"", "Semestre"})
          dttTmp.Rows.Add(New Object() {"", "Anno"})
          dttTmp.AcceptChanges()
          cbText.DataSource = dttTmp
          cbText.ValueMember = "cod"
          cbText.DisplayMember = "val"
          GctlSetVisEnab(cbText, False)
          GctlSetVisEnab(cbText, True)
        Case "ar_deterior"
          dttTmp.Columns.Add("cod", GetType(String))
          dttTmp.Columns.Add("val", GetType(String))
          dttTmp.Rows.Add(New Object() {"S", "Prodotto alimentare deteriorabile"})
          dttTmp.Rows.Add(New Object() {"N", "Prodotto alimentare non deteriorabile"})
          dttTmp.Rows.Add(New Object() {" ", "Altro"})
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
      Select Case oCleVaar.strCampo
        '--- Campi stringa
        Case "ar_famprod", "ar_coddica", "ar_coddicv"
          If edText.Text.Trim = "" Then
            If edText.Text <> " " Then
              oApp.MsgBoxErr(oApp.Tr(Me, 130063702300847157, "Il valore del campo da cambiare, se non indicato, deve contenere un solo spazio."))
              edText.Text = " "
              edText.Focus()
              Exit Function
            End If
          End If
          If edText.Text.Trim <> "" Then
            If Not oCleVaar.CheckInTable(oCleVaar.strCampo, NTSCStr(edText.Text)) Then
              Exit Function
            End If
          End If
        Case "ar_codnomc"
          If edText.Name = "edNuovoValore" Then
            If edText.Text <> "" Then
              If Not oCleVaar.CheckInTable(oCleVaar.strCampo, NTSCStr(edText.Text)) Then
                Exit Function
              End If
            End If
          End If
          '--- Campi interi (da 0 a 99)
        Case "ar_gruppo"
          If Not ValNumEx(edText, strLabel, 0, 99, 0) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128619869304722639, "Il valore del campo da cambiare deve avere un valore compreso tra 0 e 99"))
            edText.Text = "0"
            edText.Focus()
            Exit Function
          End If
          If NTSCInt(edText.Text) > 0 Then
            If Not oCleVaar.CheckInTable(oCleVaar.strCampo, NTSCStr(edText.Text)) Then
              edText.Text = "0"
              edText.Focus()
              Exit Function
            End If
          End If
          '--- Campi interi (da 0 a 999)
        Case "ar_ggrior", "ar_reparto", "ar_numecr", "ar_codvuo", _
             "ar_codpdon", "ar_garacq", "ar_garven", "ar_livmindb", "ar_fpfence", "ar_rrfence", _
             "ar_codappr", "ar_ggant", "ar_ggpost", "ar_codimba", "ar_ggragg"
          If Not ValNumEx(edText, strLabel, 0, 999, 0) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128619869377939636, "Il valore del campo da cambiare deve avere un valore compreso tra 0 e 999"))
            edText.Text = "0"
            edText.Focus()
            Exit Function
          End If
          If Val(edText.Text) > 0 Then
            If Not oCleVaar.CheckInTable(oCleVaar.strCampo, NTSCStr(edText.Text)) Then
              edText.Text = "0"
              edText.Focus()
              Exit Function
            End If
          End If
          '--- Campi interi (da 0 a 9999)
        Case "ar_codiva", "ar_sotgru", "ar_controp", "ar_catlifo", "ar_controa", "ar_contros", _
             "ar_magstock", "ar_magprod", "ar_verdb", "ar_ultfase", "ar_codtagl", "ar_codstag", "ar_codtcdc", _
             "ar_claprov", "ar_clascon", "ar_codseat", "ar_codmarc"
          If Not ValNumEx(edText, strLabel, 0, 9999, 0) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128619869422119615, "Il valore del campo da cambiare deve avere un valore compreso tra 0 e 9999"))
            edText.Text = "0"
            edText.Focus()
            Exit Function
          End If
          If Val(edText.Text) > 0 Then
            If Not oCleVaar.CheckInTable(oCleVaar.strCampo, NTSCStr(edText.Text)) Then
              edText.Text = "0"
              edText.Focus()
              Exit Function
            End If
          End If
          '  '--- Campi long (da 0 a 99999999)
          'Case "ar_pesolor", "ar_pesonet"
          '  If Not ValNumEx(edText, strLabel, 0, 99999999, 0) Then
          '    oApp.MsgBoxErr(oApp.Tr(Me, 128619869467080159, "Il valore del campo da cambiare deve avere un valore compreso tra 0 e 99999999"))
          '    edText.Text = "0"
          '    edText.Focus()
          '    Exit Function
          '  End If
          '--- Campi long (da 0 a 999999999)
        Case "ar_forn", "ar_forn2", "ar_percvst"
          If Not ValNumEx(edText, strLabel, 0, 999999999, 0) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128619869496273290, "Il valore del campo da cambiare deve avere un valore compreso tra 0 e 999999999"))
            edText.Text = "0"
            edText.Focus()
            Exit Function
          End If
          If CLng(edText.Text) > 0 Then
            If Not oCleVaar.CheckInTable(oCleVaar.strCampo, NTSCStr(edText.Text)) Then
              edText.Text = "0"
              edText.Focus()
              Exit Function
            End If
          End If
          '--- Campi double (da 0 a 9999,99999)
        Case "ar_fcorrlt"
          If Not ValNumEx(edText, strLabel, 0, NTSCDec(9999.99999), 5) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128619869542326625, "Il valore del campo da cambiare deve avere un valore compreso tra 0 e 9999,99999"))
            edText.Text = "0"
            edText.Focus()
            Exit Function
          End If
          '--- Campi double (da 1 a 999999999999)
        Case "ar_perqta"
          If Not ValNumEx(edText, strLabel, 1, NTSCDec(999999999999.0), 0) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128619869566055801, "Il valore del campo da cambiare deve avere un valore compreso tra 1 e 999999999999,0"))
            edText.Text = "0"
            edText.Focus()
            Exit Function
          End If
          '--- Campi double (da 0,001 a 99999999)
        Case "ar_conver", "ar_qtacon2"
          If Not ValNumEx(edText, strLabel, NTSCDec(0.001), 99999999, 3) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128619869589316638, "Il valore del campo da cambiare deve avere un valore compreso tra 0,001 e 99999999"))
            edText.Text = "0"
            edText.Focus()
            Exit Function
          End If
          '--- Campi double (da 0 a 99) (2 decimali)
        Case "ar_ricar1", "ar_ricar2"
          If Not ValNumEx(edText, strLabel, 0, 999999999, 2) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128619869609142989, "Il valore del campo da cambiare deve avere un valore compreso tra 0 e 999999999"))
            edText.Text = "0"
            edText.Focus()
            Exit Function
          End If
          '--- Campi double (da 0 a 999999999) (3 decimali)
        Case "ar_scomin", "ar_scomax", "ar_sublotto", "ar_maxlotto"
          If Not ValNumEx(edText, strLabel, 0, 999999999, 3) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128619869637711668, "Il valore del campo da cambiare deve avere un valore compreso tra 0 e 999999999"))
            edText.Text = "0"
            edText.Focus()
            Exit Function
          End If
        Case "ar_minord"
          If Not ValNumEx(edText, strLabel, 0, 9999999999, 3) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129839822075253594, "Il valore del campo da cambiare deve avere un valore compreso tra 0 e 9999999999"))
            edText.Text = "0"
            edText.Focus()
            Exit Function
          End If
          '--- Campi double (da 0 a 999999) (3 decimali)
        Case "ar_pesolor", "ar_pesonet"
          If Not ValNumEx(edText, strLabel, 0, 999999, 3) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129367219907886059, "Il valore del campo da cambiare deve avere un valore compreso tra 0 e 999999"))
            edText.Text = "0"
            edText.Focus()
            Exit Function
          End If
          '--- Campi double (da 0 a 999999999999) (4 decimali)
        Case "ar_volume"
          If Not ValNumEx(edText, strLabel, 0, NTSCDec(999999999999.0), 4) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128619869663470313, "Il valore del campo da cambiare deve avere un valore compreso tra 0 e 999999999999,0"))
            edText.Text = "0"
            edText.Focus()
            Exit Function
          End If
          '--- Campi double (da 0 a 1000000000000) (9 decimali)
        Case "ar_misura1", "ar_misura2", "ar_misura3"
          If Not ValNumEx(edText, strLabel, 0, NTSCDec(1000000000000.0), 9) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128619869686106698, "Il valore del campo da cambiare deve avere un valore compreso tra 1 e 1000000000000,0"))
            edText.Text = "0"
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
    'ho tenuto traccia del tipo precedente
    'per poter azzerare il valore
    'quando si passa da un tipo campo da numerico a stringa
    'per non dare il msg "deve avere un valore compreso tra 0 e 999"
    If oCleVaar.strTipo = "S" Then
      edText.Text = " "
    ElseIf oCleVaar.strTipo = "N" Then
      edText.Text = "0"
    Else
      edText.Text = ""
    End If

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
      strCrpe = "{TTINVENT.instid} = " & oCleVaar.lIITTInvent & _
                " And {TTINVENT.codditt} = '" & DittaCorrente & "'"
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGVAAR", "Reports1", " ", 0, nDestin, "BSMGVAAR.RPT", False, "Variazione Campi Anagrafica Articoli", False)
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

  Public Overridable Function ValNumEx(ByVal edCampo As NTSTextBoxStr, ByVal strNomCampo As String, ByVal nMin As Decimal, ByVal nMax As Decimal, ByVal nDecMax As Integer) As Boolean
    ' valida un numer specificando anche quanti decimali
    Dim dComodo As Decimal
    Try
      ' se non indicato setta a 10 il numero max. di decimali
      If nDecMax = 0 Then
        nDecMax = 10
      End If

      ValNumEx = True
      If Not IsNumeric(edCampo.Text) Then
        Return False
      End If
      ' passa per un double
      dComodo = NTSCDec(edCampo.Text)
      If Not (NTSCDec(edCampo.Text) >= nMin And NTSCDec(edCampo.Text) <= nMax) Then Return False
      If Not (ArrDbl(dComodo, nDecMax) = dComodo) Then Return False

      Exit Function

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function Elabora(ByVal nTipoElaborazione As Integer) As Boolean
    Dim lRecords As Integer
    Dim oParam As New CLE__CLDP
    Dim frmAutm As FRM__AUT1 = Nothing
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
        If oCleVaar.Elabora(lRecords, nTipoElaborazione, ckSeleziona.Checked, cbValore.Visible, cbValore.Text, cbNuovoValore.Text, edValore.Text, edNuovoValore.Text, cbNuovoValore.SelectedIndex, edValore.Visible, cbValore.SelectedIndex, edDataelab.Text, cbCampi.Text, cbNuovoValore.Visible, ckElaborazione.Checked) Then
          If lRecords > 0 Then
            If lRecords = 1 Then
              oApp.MsgBoxInfo(oApp.Tr(Me, 128503235626143857, "Elaborazione terminata." & vbCrLf & "Aggiornato n° 1 articolo."))
            Else
              oApp.MsgBoxInfo(oApp.Tr(Me, 128503235667758477, "Elaborazione terminata." & vbCrLf & "Aggiornati n° |" & lRecords & "| articoli."))
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
        oCleVaar.bAutmcbValoreVis = cbValore.Visible
        oCleVaar.bAutmckSeleziona = ckSeleziona.Checked
        oCleVaar.nAutmcbNuovoValore = cbNuovoValore.SelectedIndex
        oCleVaar.strAutmedNuovoValore = edNuovoValore.Text
        oCleVaar.strAutmedValore = edValore.Text
        oCleVaar.bAutmedValoreVis = edValore.Visible
        oCleVaar.nAutmcbValore = cbValore.SelectedIndex
        oCleVaar.strAutmcbValore = cbValore.Text
        oCleVaar.strAutmcbNuovoValore = cbNuovoValore.Text

        If Not oCleVaar.TestPreElabora(ckSeleziona.Checked, cbValore.Visible, cbValore.Text, cbNuovoValore.Text, edValore.Text, edNuovoValore.Text) Then
          Me.Cursor = Cursors.Default
          Return False
        End If

        frmAutm = CType(NTSNewFormModal("FRM__AUT1"), FRM__AUT1)
        frmAutm.Init(oMenu, oParam, DittaCorrente)
        frmAutm.InitEntity(oCleVaar)
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
    Dim frmInmo As FRMMGINMO = Nothing
    Try

      frmInmo = CType(NTSNewFormModal("FRMMGINMO"), FRMMGINMO)
      frmInmo.nInmoRichiesta = 0
      frmInmo.strInmoCodart = oCleVaar.strCodart
      frmInmo.strInmoDescr = oCleVaar.strDescr
      frmInmo.Init(oMenu, oParam, DittaCorrente)
      frmInmo.InitEntity(oCleVaar)
      frmInmo.ShowDialog()

      oCleVaar.nRichiesta = frmInmo.nInmoRichiesta

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function SettaTabellaZoom() As Boolean
    Try
      edValore.NTSForzaVisZoom = False
      edNuovoValore.NTSForzaVisZoom = False

      Select Case oCleVaar.strCampo
        Case "ar_codiva"
          strTabella = "Civa"
          edValore.NTSSetParamZoom("ZOOMTABCIVA")
          edNuovoValore.NTSSetParamZoom("ZOOMTABCIVA")
        Case "ar_gruppo"
          strTabella = "Gmer"
          edValore.NTSSetParamZoom("ZOOMTABGMER")
          edNuovoValore.NTSSetParamZoom("ZOOMTABGMER")
        Case "ar_sotgru"
          strTabella = "Sgme"
          edValore.NTSSetParamZoom("ZOOMTABSGME")
          edNuovoValore.NTSSetParamZoom("ZOOMTABSGME")
        Case "ar_controp", "ar_controa", "ar_contros"
          strTabella = "Cove"
          edValore.NTSSetParamZoom("ZOOMTABCOVE")
          edNuovoValore.NTSSetParamZoom("ZOOMTABCOVE")
        Case "ar_claprov"
          strTabella = "Cpar"
          edValore.NTSSetParamZoom("ZOOMTABCPAR")
          edNuovoValore.NTSSetParamZoom("ZOOMTABCPAR")
        Case "ar_clascon"
          strTabella = "Csar"
          edValore.NTSSetParamZoom("ZOOMTABCSAR")
          edNuovoValore.NTSSetParamZoom("ZOOMTABCSAR")
        Case "ar_codpdon"
          strTabella = "Pdon"
          edValore.NTSSetParamZoom("ZOOMTABPDON")
          edNuovoValore.NTSSetParamZoom("ZOOMTABPDON")
        Case "ar_magstock"
          strTabella = "Maga"
          edValore.NTSSetParamZoom("ZOOMTABMAGA")
          edNuovoValore.NTSSetParamZoom("ZOOMTABMAGA")
        Case "ar_magprod"
          strTabella = "Maga"
          edValore.NTSSetParamZoom("ZOOMTABMAGA")
          edNuovoValore.NTSSetParamZoom("ZOOMTABMAGA")
        Case "ar_codappr"
          strTabella = "Appr"
          edValore.NTSSetParamZoom("ZOOMTABAPPR")
          edNuovoValore.NTSSetParamZoom("ZOOMTABAPPR")
        Case "ar_codmarc"
          strTabella = "Marc"
          edValore.NTSSetParamZoom("ZOOMTABMARC")
          edNuovoValore.NTSSetParamZoom("ZOOMTABMARC")
        Case "ar_forn"
          strTabella = "ANAGRA"
          edValore.NTSForzaVisZoom = True
          edNuovoValore.NTSForzaVisZoom = True
        Case "ar_forn2"
          strTabella = "ANAGRA"
          edValore.NTSForzaVisZoom = True
          edNuovoValore.NTSForzaVisZoom = True
        Case "ar_codnomc"
          strTabella = "TARIC"
          edValore.NTSSetParamZoom("ZOOMTARIC")
          edNuovoValore.NTSSetParamZoom("ZOOMTARIC")
        Case "ar_famprod"
          strTabella = "Cfam"
          edValore.NTSSetParamZoom("ZOOMTABCFAM")
          edNuovoValore.NTSSetParamZoom("ZOOMTABCFAM")
        Case "ar_codtcdc"
          strTabella = "Tcdc"
          edValore.NTSSetParamZoom("ZOOMTABTCDC")
          edNuovoValore.NTSSetParamZoom("ZOOMTABTCDC")
        Case "ar_coddica"
          strTabella = "Dica"
          edValore.NTSSetParamZoom("ZOOMTABDICA")
          edNuovoValore.NTSSetParamZoom("ZOOMTABDICA")
        Case "ar_codseat"
          strTabella = "Seat"
          edValore.NTSSetParamZoom("ZOOMTABSEAT")
          edNuovoValore.NTSSetParamZoom("ZOOMTABSEAT")
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

