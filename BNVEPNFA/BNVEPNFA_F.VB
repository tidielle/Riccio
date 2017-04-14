Imports System.Data
Imports NTSInformatica.CLN__STD

'ATTENZIONE: sia BNVEPNFA che BNREPNCO devono compilare il file BATCH con gli stessi parametri

Public Class FRMVEPNFA_F
  Private Moduli_P As Integer = bsModMG + bsModVE + bsModCG
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

  Public oClePnfa As CLEVEPNFA
  Public oCallParams As CLE__CLDP

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager


  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMVEPNFA_F))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbElabora = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbIntegr = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbVislog = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampavideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.lbEscompLabel = New NTSInformatica.NTSLabel
    Me.lbDatreg = New NTSInformatica.NTSLabel
    Me.lbNoteData = New NTSInformatica.NTSLabel
    Me.edEscomp = New NTSInformatica.NTSTextBoxNum
    Me.edDatreg = New NTSInformatica.NTSTextBoxData
    Me.lbEscomp = New NTSInformatica.NTSLabel
    Me.fmSelezione = New NTSInformatica.NTSGroupBox
    Me.pnSelexDx = New NTSInformatica.NTSPanel
    Me.ckContabAncheConScadSald = New NTSInformatica.NTSCheckBox
    Me.ckDelEffettiNoPres = New NTSInformatica.NTSCheckBox
    Me.lbNumdocA = New NTSInformatica.NTSLabel
    Me.lbDatada = New NTSInformatica.NTSLabel
    Me.ckTiporkJDiff = New NTSInformatica.NTSCheckBox
    Me.lbBarra2 = New NTSInformatica.NTSLabel
    Me.ckTiporkJ = New NTSInformatica.NTSCheckBox
    Me.ckSerie = New NTSInformatica.NTSCheckBox
    Me.lbTipobf = New NTSInformatica.NTSLabel
    Me.edTipobf = New NTSInformatica.NTSTextBoxNum
    Me.edNumdocA = New NTSInformatica.NTSTextBoxNum
    Me.edNumdocDa = New NTSInformatica.NTSTextBoxNum
    Me.ckTiporkP = New NTSInformatica.NTSCheckBox
    Me.edSeriedoc = New NTSInformatica.NTSTextBoxStr
    Me.ckTiporkK = New NTSInformatica.NTSCheckBox
    Me.ckTiporkS = New NTSInformatica.NTSCheckBox
    Me.edDatdocA = New NTSInformatica.NTSTextBoxData
    Me.ckTiporkL = New NTSInformatica.NTSCheckBox
    Me.edDatdocDa = New NTSInformatica.NTSTextBoxData
    Me.edAnnodoc = New NTSInformatica.NTSTextBoxNum
    Me.lbTipobfLabel = New NTSInformatica.NTSLabel
    Me.lbNumdocDaa = New NTSInformatica.NTSLabel
    Me.lbDatadaa = New NTSInformatica.NTSLabel
    Me.lbAnnodoc = New NTSInformatica.NTSLabel
    Me.ckRielab = New NTSInformatica.NTSCheckBox
    Me.ckContinc = New NTSInformatica.NTSCheckBox
    Me.pnSelexSx = New NTSInformatica.NTSPanel
    Me.ckTiporkNDiff = New NTSInformatica.NTSCheckBox
    Me.ckTiporkE = New NTSInformatica.NTSCheckBox
    Me.ckContCompensNoteAcc = New NTSInformatica.NTSCheckBox
    Me.lbContCompensDDT = New NTSInformatica.NTSLabel
    Me.ckContCompensDDT = New NTSInformatica.NTSCheckBox
    Me.ckTiporkB = New NTSInformatica.NTSCheckBox
    Me.ckContIncDDT = New NTSInformatica.NTSCheckBox
    Me.lbBarra = New NTSInformatica.NTSLabel
    Me.ckTiporkA = New NTSInformatica.NTSCheckBox
    Me.ckTiporkD = New NTSInformatica.NTSCheckBox
    Me.ckTiporkC = New NTSInformatica.NTSCheckBox
    Me.ckTiporkN = New NTSInformatica.NTSCheckBox
    Me.ckTiporkI = New NTSInformatica.NTSCheckBox
    Me.ckTiporkF = New NTSInformatica.NTSCheckBox
    Me.lbStatus = New NTSInformatica.NTSLabel
    Me.ckCheckProtocolli = New NTSInformatica.NTSCheckBox
    Me.ckGenEffetti = New NTSInformatica.NTSCheckBox
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEscomp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatreg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmSelezione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmSelezione.SuspendLayout()
    CType(Me.pnSelexDx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSelexDx.SuspendLayout()
    CType(Me.ckContabAncheConScadSald.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDelEffettiNoPres.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTiporkJDiff.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTiporkJ.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTipobf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumdocA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumdocDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTiporkP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSeriedoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTiporkK.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTiporkS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatdocA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTiporkL.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatdocDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnnodoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckRielab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckContinc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnSelexSx, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSelexSx.SuspendLayout()
    CType(Me.ckTiporkNDiff.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTiporkE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckContCompensNoteAcc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckContCompensDDT.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTiporkB.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckContIncDDT.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTiporkA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTiporkD.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTiporkC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTiporkN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTiporkI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTiporkF.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckCheckProtocolli.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckGenEffetti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbElabora, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti, Me.tlbVislog, Me.tlbIntegr, Me.tlbStampavideo})
    Me.NtsBarManager1.MaxItemId = 19
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbElabora), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbIntegr, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbZoom.Id = 13
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbIntegr
    '
    Me.tlbIntegr.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbIntegr.Caption = "Registrazione integrativa"
    Me.tlbIntegr.Glyph = CType(resources.GetObject("tlbIntegr.Glyph"), System.Drawing.Image)
    Me.tlbIntegr.Id = 17
    Me.tlbIntegr.Name = "tlbIntegr"
    Me.tlbIntegr.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 15
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbVislog), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampavideo)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbVislog
    '
    Me.tlbVislog.Caption = "Visualizza log ultima elaborazione"
    Me.tlbVislog.Id = 16
    Me.tlbVislog.Name = "tlbVislog"
    Me.tlbVislog.Visible = True
    '
    'tlbStampavideo
    '
    Me.tlbStampavideo.Caption = "Stampa registrazioni di prima nota"
    Me.tlbStampavideo.Id = 18
    Me.tlbStampavideo.Name = "tlbStampavideo"
    Me.tlbStampavideo.Visible = True
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
    'lbEscompLabel
    '
    Me.lbEscompLabel.AutoSize = True
    Me.lbEscompLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbEscompLabel.Location = New System.Drawing.Point(9, 35)
    Me.lbEscompLabel.Name = "lbEscompLabel"
    Me.lbEscompLabel.NTSDbField = ""
    Me.lbEscompLabel.Size = New System.Drawing.Size(120, 13)
    Me.lbEscompLabel.TabIndex = 4
    Me.lbEscompLabel.Text = "Esercizio di competenza"
    Me.lbEscompLabel.Tooltip = ""
    Me.lbEscompLabel.UseMnemonic = False
    '
    'lbDatreg
    '
    Me.lbDatreg.AutoSize = True
    Me.lbDatreg.BackColor = System.Drawing.Color.Transparent
    Me.lbDatreg.Location = New System.Drawing.Point(9, 57)
    Me.lbDatreg.Name = "lbDatreg"
    Me.lbDatreg.NTSDbField = ""
    Me.lbDatreg.Size = New System.Drawing.Size(95, 13)
    Me.lbDatreg.TabIndex = 5
    Me.lbDatreg.Text = "Data registrazione"
    Me.lbDatreg.Tooltip = ""
    Me.lbDatreg.UseMnemonic = False
    '
    'lbNoteData
    '
    Me.lbNoteData.AutoSize = True
    Me.lbNoteData.BackColor = System.Drawing.Color.Transparent
    Me.lbNoteData.Location = New System.Drawing.Point(239, 57)
    Me.lbNoteData.Name = "lbNoteData"
    Me.lbNoteData.NTSDbField = ""
    Me.lbNoteData.Size = New System.Drawing.Size(301, 13)
    Me.lbNoteData.TabIndex = 6
    Me.lbNoteData.Text = "(se non indicata  allora data registrazione = data documento)"
    Me.lbNoteData.Tooltip = ""
    Me.lbNoteData.UseMnemonic = False
    '
    'edEscomp
    '
    Me.edEscomp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEscomp.EditValue = "0"
    Me.edEscomp.Location = New System.Drawing.Point(143, 32)
    Me.edEscomp.Name = "edEscomp"
    Me.edEscomp.NTSDbField = ""
    Me.edEscomp.NTSFormat = "0"
    Me.edEscomp.NTSForzaVisZoom = False
    Me.edEscomp.NTSOldValue = ""
    Me.edEscomp.Properties.Appearance.Options.UseTextOptions = True
    Me.edEscomp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEscomp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEscomp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEscomp.Properties.AutoHeight = False
    Me.edEscomp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEscomp.Properties.MaxLength = 65536
    Me.edEscomp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEscomp.Size = New System.Drawing.Size(65, 20)
    Me.edEscomp.TabIndex = 7
    '
    'edDatreg
    '
    Me.edDatreg.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatreg.Location = New System.Drawing.Point(143, 54)
    Me.edDatreg.Name = "edDatreg"
    Me.edDatreg.NTSDbField = ""
    Me.edDatreg.NTSForzaVisZoom = False
    Me.edDatreg.NTSOldValue = ""
    Me.edDatreg.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatreg.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatreg.Properties.AutoHeight = False
    Me.edDatreg.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatreg.Properties.MaxLength = 65536
    Me.edDatreg.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatreg.Size = New System.Drawing.Size(85, 20)
    Me.edDatreg.TabIndex = 8
    '
    'lbEscomp
    '
    Me.lbEscomp.BackColor = System.Drawing.Color.Transparent
    Me.lbEscomp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbEscomp.Location = New System.Drawing.Point(213, 32)
    Me.lbEscomp.Name = "lbEscomp"
    Me.lbEscomp.NTSDbField = ""
    Me.lbEscomp.Size = New System.Drawing.Size(339, 20)
    Me.lbEscomp.TabIndex = 9
    Me.lbEscomp.Tooltip = ""
    Me.lbEscomp.UseMnemonic = False
    '
    'fmSelezione
    '
    Me.fmSelezione.AllowDrop = True
    Me.fmSelezione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmSelezione.Appearance.Options.UseBackColor = True
    Me.fmSelezione.Controls.Add(Me.pnSelexDx)
    Me.fmSelezione.Controls.Add(Me.pnSelexSx)
    Me.fmSelezione.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmSelezione.Location = New System.Drawing.Point(12, 98)
    Me.fmSelezione.Name = "fmSelezione"
    Me.fmSelezione.Size = New System.Drawing.Size(724, 319)
    Me.fmSelezione.TabIndex = 10
    Me.fmSelezione.Text = "Selezione documenti da contabilizzare"
    '
    'pnSelexDx
    '
    Me.pnSelexDx.AllowDrop = True
    Me.pnSelexDx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSelexDx.Appearance.Options.UseBackColor = True
    Me.pnSelexDx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSelexDx.Controls.Add(Me.ckGenEffetti)
    Me.pnSelexDx.Controls.Add(Me.ckContabAncheConScadSald)
    Me.pnSelexDx.Controls.Add(Me.ckDelEffettiNoPres)
    Me.pnSelexDx.Controls.Add(Me.lbNumdocA)
    Me.pnSelexDx.Controls.Add(Me.lbDatada)
    Me.pnSelexDx.Controls.Add(Me.ckTiporkJDiff)
    Me.pnSelexDx.Controls.Add(Me.lbBarra2)
    Me.pnSelexDx.Controls.Add(Me.ckTiporkJ)
    Me.pnSelexDx.Controls.Add(Me.ckSerie)
    Me.pnSelexDx.Controls.Add(Me.lbTipobf)
    Me.pnSelexDx.Controls.Add(Me.edTipobf)
    Me.pnSelexDx.Controls.Add(Me.edNumdocA)
    Me.pnSelexDx.Controls.Add(Me.edNumdocDa)
    Me.pnSelexDx.Controls.Add(Me.ckTiporkP)
    Me.pnSelexDx.Controls.Add(Me.edSeriedoc)
    Me.pnSelexDx.Controls.Add(Me.ckTiporkK)
    Me.pnSelexDx.Controls.Add(Me.ckTiporkS)
    Me.pnSelexDx.Controls.Add(Me.edDatdocA)
    Me.pnSelexDx.Controls.Add(Me.ckTiporkL)
    Me.pnSelexDx.Controls.Add(Me.edDatdocDa)
    Me.pnSelexDx.Controls.Add(Me.edAnnodoc)
    Me.pnSelexDx.Controls.Add(Me.lbTipobfLabel)
    Me.pnSelexDx.Controls.Add(Me.lbNumdocDaa)
    Me.pnSelexDx.Controls.Add(Me.lbDatadaa)
    Me.pnSelexDx.Controls.Add(Me.lbAnnodoc)
    Me.pnSelexDx.Controls.Add(Me.ckRielab)
    Me.pnSelexDx.Controls.Add(Me.ckContinc)
    Me.pnSelexDx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnSelexDx.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnSelexDx.Location = New System.Drawing.Point(336, 20)
    Me.pnSelexDx.Name = "pnSelexDx"
    Me.pnSelexDx.NTSActiveTrasparency = True
    Me.pnSelexDx.Size = New System.Drawing.Size(386, 297)
    Me.pnSelexDx.TabIndex = 23
    Me.pnSelexDx.Text = "NtsPanel2"
    '
    'ckContabAncheConScadSald
    '
    Me.ckContabAncheConScadSald.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckContabAncheConScadSald.Location = New System.Drawing.Point(22, 237)
    Me.ckContabAncheConScadSald.Name = "ckContabAncheConScadSald"
    Me.ckContabAncheConScadSald.NTSCheckValue = "S"
    Me.ckContabAncheConScadSald.NTSUnCheckValue = "N"
    Me.ckContabAncheConScadSald.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckContabAncheConScadSald.Properties.Appearance.Options.UseBackColor = True
    Me.ckContabAncheConScadSald.Properties.AutoHeight = False
    Me.ckContabAncheConScadSald.Properties.Caption = "Contab. anche documenti con scadenze saldate"
    Me.ckContabAncheConScadSald.Size = New System.Drawing.Size(259, 19)
    Me.ckContabAncheConScadSald.TabIndex = 49
    '
    'ckDelEffettiNoPres
    '
    Me.ckDelEffettiNoPres.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDelEffettiNoPres.Location = New System.Drawing.Point(22, 221)
    Me.ckDelEffettiNoPres.Name = "ckDelEffettiNoPres"
    Me.ckDelEffettiNoPres.NTSCheckValue = "S"
    Me.ckDelEffettiNoPres.NTSUnCheckValue = "N"
    Me.ckDelEffettiNoPres.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDelEffettiNoPres.Properties.Appearance.Options.UseBackColor = True
    Me.ckDelEffettiNoPres.Properties.AutoHeight = False
    Me.ckDelEffettiNoPres.Properties.Caption = "Cancella emiss. effetti collegati a fattura NON pres. banca"
    Me.ckDelEffettiNoPres.Size = New System.Drawing.Size(314, 19)
    Me.ckDelEffettiNoPres.TabIndex = 48
    '
    'lbNumdocA
    '
    Me.lbNumdocA.AutoSize = True
    Me.lbNumdocA.BackColor = System.Drawing.Color.Transparent
    Me.lbNumdocA.Location = New System.Drawing.Point(213, 165)
    Me.lbNumdocA.Name = "lbNumdocA"
    Me.lbNumdocA.NTSDbField = ""
    Me.lbNumdocA.Size = New System.Drawing.Size(13, 13)
    Me.lbNumdocA.TabIndex = 47
    Me.lbNumdocA.Text = "a"
    Me.lbNumdocA.Tooltip = ""
    Me.lbNumdocA.UseMnemonic = False
    '
    'lbDatada
    '
    Me.lbDatada.AutoSize = True
    Me.lbDatada.BackColor = System.Drawing.Color.Transparent
    Me.lbDatada.Location = New System.Drawing.Point(213, 143)
    Me.lbDatada.Name = "lbDatada"
    Me.lbDatada.NTSDbField = ""
    Me.lbDatada.Size = New System.Drawing.Size(13, 13)
    Me.lbDatada.TabIndex = 46
    Me.lbDatada.Text = "a"
    Me.lbDatada.Tooltip = ""
    Me.lbDatada.UseMnemonic = False
    '
    'ckTiporkJDiff
    '
    Me.ckTiporkJDiff.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTiporkJDiff.Location = New System.Drawing.Point(9, 88)
    Me.ckTiporkJDiff.Name = "ckTiporkJDiff"
    Me.ckTiporkJDiff.NTSCheckValue = "S"
    Me.ckTiporkJDiff.NTSUnCheckValue = "N"
    Me.ckTiporkJDiff.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTiporkJDiff.Properties.Appearance.Options.UseBackColor = True
    Me.ckTiporkJDiff.Properties.AutoHeight = False
    Me.ckTiporkJDiff.Properties.Caption = "Note di accredito differite ricevute"
    Me.ckTiporkJDiff.Size = New System.Drawing.Size(189, 19)
    Me.ckTiporkJDiff.TabIndex = 45
    '
    'lbBarra2
    '
    Me.lbBarra2.BackColor = System.Drawing.Color.Transparent
    Me.lbBarra2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbBarra2.Location = New System.Drawing.Point(3, 109)
    Me.lbBarra2.Name = "lbBarra2"
    Me.lbBarra2.NTSDbField = ""
    Me.lbBarra2.Size = New System.Drawing.Size(370, 2)
    Me.lbBarra2.TabIndex = 40
    Me.lbBarra2.Tooltip = ""
    Me.lbBarra2.UseMnemonic = False
    '
    'ckTiporkJ
    '
    Me.ckTiporkJ.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTiporkJ.Location = New System.Drawing.Point(9, 71)
    Me.ckTiporkJ.Name = "ckTiporkJ"
    Me.ckTiporkJ.NTSCheckValue = "S"
    Me.ckTiporkJ.NTSUnCheckValue = "N"
    Me.ckTiporkJ.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTiporkJ.Properties.Appearance.Options.UseBackColor = True
    Me.ckTiporkJ.Properties.AutoHeight = False
    Me.ckTiporkJ.Properties.Caption = "Nota accredito ricevute"
    Me.ckTiporkJ.Size = New System.Drawing.Size(145, 19)
    Me.ckTiporkJ.TabIndex = 39
    '
    'ckSerie
    '
    Me.ckSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSerie.Location = New System.Drawing.Point(223, 118)
    Me.ckSerie.Name = "ckSerie"
    Me.ckSerie.NTSCheckValue = "S"
    Me.ckSerie.NTSUnCheckValue = "N"
    Me.ckSerie.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSerie.Properties.Appearance.Options.UseBackColor = True
    Me.ckSerie.Properties.AutoHeight = False
    Me.ckSerie.Properties.Caption = "Serie"
    Me.ckSerie.Size = New System.Drawing.Size(49, 19)
    Me.ckSerie.TabIndex = 38
    '
    'lbTipobf
    '
    Me.lbTipobf.BackColor = System.Drawing.Color.Transparent
    Me.lbTipobf.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbTipobf.Location = New System.Drawing.Point(171, 184)
    Me.lbTipobf.Name = "lbTipobf"
    Me.lbTipobf.NTSDbField = ""
    Me.lbTipobf.Size = New System.Drawing.Size(212, 20)
    Me.lbTipobf.TabIndex = 37
    Me.lbTipobf.Tooltip = ""
    Me.lbTipobf.UseMnemonic = False
    '
    'edTipobf
    '
    Me.edTipobf.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTipobf.EditValue = "0"
    Me.edTipobf.Location = New System.Drawing.Point(114, 184)
    Me.edTipobf.Name = "edTipobf"
    Me.edTipobf.NTSDbField = ""
    Me.edTipobf.NTSFormat = "0"
    Me.edTipobf.NTSForzaVisZoom = False
    Me.edTipobf.NTSOldValue = ""
    Me.edTipobf.Properties.Appearance.Options.UseTextOptions = True
    Me.edTipobf.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTipobf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTipobf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTipobf.Properties.AutoHeight = False
    Me.edTipobf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTipobf.Properties.MaxLength = 65536
    Me.edTipobf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTipobf.Size = New System.Drawing.Size(51, 20)
    Me.edTipobf.TabIndex = 36
    '
    'edNumdocA
    '
    Me.edNumdocA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumdocA.EditValue = "999999999"
    Me.edNumdocA.Location = New System.Drawing.Point(232, 162)
    Me.edNumdocA.Name = "edNumdocA"
    Me.edNumdocA.NTSDbField = ""
    Me.edNumdocA.NTSFormat = "0"
    Me.edNumdocA.NTSForzaVisZoom = False
    Me.edNumdocA.NTSOldValue = "999999999"
    Me.edNumdocA.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumdocA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumdocA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNumdocA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNumdocA.Properties.AutoHeight = False
    Me.edNumdocA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumdocA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumdocA.Size = New System.Drawing.Size(84, 20)
    Me.edNumdocA.TabIndex = 35
    '
    'edNumdocDa
    '
    Me.edNumdocDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumdocDa.EditValue = "0"
    Me.edNumdocDa.Location = New System.Drawing.Point(114, 162)
    Me.edNumdocDa.Name = "edNumdocDa"
    Me.edNumdocDa.NTSDbField = ""
    Me.edNumdocDa.NTSFormat = "0"
    Me.edNumdocDa.NTSForzaVisZoom = False
    Me.edNumdocDa.NTSOldValue = ""
    Me.edNumdocDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumdocDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumdocDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNumdocDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNumdocDa.Properties.AutoHeight = False
    Me.edNumdocDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumdocDa.Properties.MaxLength = 65536
    Me.edNumdocDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumdocDa.Size = New System.Drawing.Size(84, 20)
    Me.edNumdocDa.TabIndex = 34
    '
    'ckTiporkP
    '
    Me.ckTiporkP.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTiporkP.Location = New System.Drawing.Point(9, 20)
    Me.ckTiporkP.Name = "ckTiporkP"
    Me.ckTiporkP.NTSCheckValue = "S"
    Me.ckTiporkP.NTSUnCheckValue = "N"
    Me.ckTiporkP.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTiporkP.Properties.Appearance.Options.UseBackColor = True
    Me.ckTiporkP.Properties.AutoHeight = False
    Me.ckTiporkP.Properties.Caption = "Fatture/Ricevute Fiscali differite"
    Me.ckTiporkP.Size = New System.Drawing.Size(189, 19)
    Me.ckTiporkP.TabIndex = 21
    '
    'edSeriedoc
    '
    Me.edSeriedoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSeriedoc.EditValue = " "
    Me.edSeriedoc.Location = New System.Drawing.Point(278, 118)
    Me.edSeriedoc.Name = "edSeriedoc"
    Me.edSeriedoc.NTSDbField = ""
    Me.edSeriedoc.NTSForzaVisZoom = False
    Me.edSeriedoc.NTSOldValue = " "
    Me.edSeriedoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSeriedoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSeriedoc.Properties.AutoHeight = False
    Me.edSeriedoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSeriedoc.Properties.MaxLength = 65536
    Me.edSeriedoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSeriedoc.Size = New System.Drawing.Size(38, 20)
    Me.edSeriedoc.TabIndex = 32
    '
    'ckTiporkK
    '
    Me.ckTiporkK.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTiporkK.Location = New System.Drawing.Point(9, 54)
    Me.ckTiporkK.Name = "ckTiporkK"
    Me.ckTiporkK.NTSCheckValue = "S"
    Me.ckTiporkK.NTSUnCheckValue = "N"
    Me.ckTiporkK.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTiporkK.Properties.Appearance.Options.UseBackColor = True
    Me.ckTiporkK.Properties.AutoHeight = False
    Me.ckTiporkK.Properties.Caption = "Fatture differite ricevute"
    Me.ckTiporkK.Size = New System.Drawing.Size(145, 19)
    Me.ckTiporkK.TabIndex = 31
    '
    'ckTiporkS
    '
    Me.ckTiporkS.Cursor = System.Windows.Forms.Cursors.Hand
    Me.ckTiporkS.Location = New System.Drawing.Point(9, 3)
    Me.ckTiporkS.Name = "ckTiporkS"
    Me.ckTiporkS.NTSCheckValue = "S"
    Me.ckTiporkS.NTSUnCheckValue = "N"
    Me.ckTiporkS.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTiporkS.Properties.Appearance.Options.UseBackColor = True
    Me.ckTiporkS.Properties.AutoHeight = False
    Me.ckTiporkS.Properties.Caption = "Fatture/Ricevute Fiscali emesse"
    Me.ckTiporkS.Size = New System.Drawing.Size(183, 19)
    Me.ckTiporkS.TabIndex = 20
    '
    'edDatdocA
    '
    Me.edDatdocA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatdocA.EditValue = "31/12/2099"
    Me.edDatdocA.Location = New System.Drawing.Point(232, 140)
    Me.edDatdocA.Name = "edDatdocA"
    Me.edDatdocA.NTSDbField = ""
    Me.edDatdocA.NTSForzaVisZoom = False
    Me.edDatdocA.NTSOldValue = ""
    Me.edDatdocA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatdocA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatdocA.Properties.AutoHeight = False
    Me.edDatdocA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatdocA.Properties.MaxLength = 65536
    Me.edDatdocA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatdocA.Size = New System.Drawing.Size(84, 20)
    Me.edDatdocA.TabIndex = 31
    '
    'ckTiporkL
    '
    Me.ckTiporkL.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTiporkL.Location = New System.Drawing.Point(9, 37)
    Me.ckTiporkL.Name = "ckTiporkL"
    Me.ckTiporkL.NTSCheckValue = "S"
    Me.ckTiporkL.NTSUnCheckValue = "N"
    Me.ckTiporkL.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTiporkL.Properties.Appearance.Options.UseBackColor = True
    Me.ckTiporkL.Properties.AutoHeight = False
    Me.ckTiporkL.Properties.Caption = "Fatture immediate ricevute"
    Me.ckTiporkL.Size = New System.Drawing.Size(156, 19)
    Me.ckTiporkL.TabIndex = 30
    '
    'edDatdocDa
    '
    Me.edDatdocDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatdocDa.EditValue = "01/01/1900"
    Me.edDatdocDa.Location = New System.Drawing.Point(114, 140)
    Me.edDatdocDa.Name = "edDatdocDa"
    Me.edDatdocDa.NTSDbField = ""
    Me.edDatdocDa.NTSForzaVisZoom = False
    Me.edDatdocDa.NTSOldValue = ""
    Me.edDatdocDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatdocDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatdocDa.Properties.AutoHeight = False
    Me.edDatdocDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatdocDa.Properties.MaxLength = 65536
    Me.edDatdocDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatdocDa.Size = New System.Drawing.Size(84, 20)
    Me.edDatdocDa.TabIndex = 30
    '
    'edAnnodoc
    '
    Me.edAnnodoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnnodoc.EditValue = "2009"
    Me.edAnnodoc.Location = New System.Drawing.Point(114, 118)
    Me.edAnnodoc.Name = "edAnnodoc"
    Me.edAnnodoc.NTSDbField = ""
    Me.edAnnodoc.NTSFormat = "0"
    Me.edAnnodoc.NTSForzaVisZoom = False
    Me.edAnnodoc.NTSOldValue = "2009"
    Me.edAnnodoc.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnnodoc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnnodoc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnnodoc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnnodoc.Properties.AutoHeight = False
    Me.edAnnodoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnnodoc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnnodoc.Size = New System.Drawing.Size(51, 20)
    Me.edAnnodoc.TabIndex = 29
    '
    'lbTipobfLabel
    '
    Me.lbTipobfLabel.AutoSize = True
    Me.lbTipobfLabel.BackColor = System.Drawing.Color.Transparent
    Me.lbTipobfLabel.Location = New System.Drawing.Point(7, 187)
    Me.lbTipobfLabel.Name = "lbTipobfLabel"
    Me.lbTipobfLabel.NTSDbField = ""
    Me.lbTipobfLabel.Size = New System.Drawing.Size(90, 13)
    Me.lbTipobfLabel.TabIndex = 28
    Me.lbTipobfLabel.Text = "Tipo bolla/fattura"
    Me.lbTipobfLabel.Tooltip = ""
    Me.lbTipobfLabel.UseMnemonic = False
    '
    'lbNumdocDaa
    '
    Me.lbNumdocDaa.AutoSize = True
    Me.lbNumdocDaa.BackColor = System.Drawing.Color.Transparent
    Me.lbNumdocDaa.Location = New System.Drawing.Point(7, 165)
    Me.lbNumdocDaa.Name = "lbNumdocDaa"
    Me.lbNumdocDaa.NTSDbField = ""
    Me.lbNumdocDaa.Size = New System.Drawing.Size(100, 13)
    Me.lbNumdocDaa.TabIndex = 27
    Me.lbNumdocDaa.Text = "Numero documento"
    Me.lbNumdocDaa.Tooltip = ""
    Me.lbNumdocDaa.UseMnemonic = False
    '
    'lbDatadaa
    '
    Me.lbDatadaa.AutoSize = True
    Me.lbDatadaa.BackColor = System.Drawing.Color.Transparent
    Me.lbDatadaa.Location = New System.Drawing.Point(7, 143)
    Me.lbDatadaa.Name = "lbDatadaa"
    Me.lbDatadaa.NTSDbField = ""
    Me.lbDatadaa.Size = New System.Drawing.Size(101, 13)
    Me.lbDatadaa.TabIndex = 26
    Me.lbDatadaa.Text = "Da data documento"
    Me.lbDatadaa.Tooltip = ""
    Me.lbDatadaa.UseMnemonic = False
    '
    'lbAnnodoc
    '
    Me.lbAnnodoc.AutoSize = True
    Me.lbAnnodoc.BackColor = System.Drawing.Color.Transparent
    Me.lbAnnodoc.Location = New System.Drawing.Point(7, 121)
    Me.lbAnnodoc.Name = "lbAnnodoc"
    Me.lbAnnodoc.NTSDbField = ""
    Me.lbAnnodoc.Size = New System.Drawing.Size(88, 13)
    Me.lbAnnodoc.TabIndex = 25
    Me.lbAnnodoc.Text = "Anno documento"
    Me.lbAnnodoc.Tooltip = ""
    Me.lbAnnodoc.UseMnemonic = False
    '
    'ckRielab
    '
    Me.ckRielab.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckRielab.Location = New System.Drawing.Point(4, 205)
    Me.ckRielab.Name = "ckRielab"
    Me.ckRielab.NTSCheckValue = "S"
    Me.ckRielab.NTSUnCheckValue = "N"
    Me.ckRielab.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckRielab.Properties.Appearance.Options.UseBackColor = True
    Me.ckRielab.Properties.AutoHeight = False
    Me.ckRielab.Properties.Caption = "Contabilizza anche documenti gia' contabilizzati in &precedenza"
    Me.ckRielab.Size = New System.Drawing.Size(319, 19)
    Me.ckRielab.TabIndex = 23
    '
    'ckContinc
    '
    Me.ckContinc.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckContinc.Location = New System.Drawing.Point(4, 254)
    Me.ckContinc.Name = "ckContinc"
    Me.ckContinc.NTSCheckValue = "S"
    Me.ckContinc.NTSUnCheckValue = "N"
    Me.ckContinc.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckContinc.Properties.Appearance.Options.UseBackColor = True
    Me.ckContinc.Properties.AutoHeight = False
    Me.ckContinc.Properties.Caption = "Contabilizza anche incassi e pagamenti &associati"
    Me.ckContinc.Size = New System.Drawing.Size(253, 19)
    Me.ckContinc.TabIndex = 22
    '
    'pnSelexSx
    '
    Me.pnSelexSx.AllowDrop = True
    Me.pnSelexSx.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSelexSx.Appearance.Options.UseBackColor = True
    Me.pnSelexSx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSelexSx.Controls.Add(Me.ckTiporkNDiff)
    Me.pnSelexSx.Controls.Add(Me.ckTiporkE)
    Me.pnSelexSx.Controls.Add(Me.ckContCompensNoteAcc)
    Me.pnSelexSx.Controls.Add(Me.lbContCompensDDT)
    Me.pnSelexSx.Controls.Add(Me.ckContCompensDDT)
    Me.pnSelexSx.Controls.Add(Me.ckTiporkB)
    Me.pnSelexSx.Controls.Add(Me.ckContIncDDT)
    Me.pnSelexSx.Controls.Add(Me.lbBarra)
    Me.pnSelexSx.Controls.Add(Me.ckTiporkA)
    Me.pnSelexSx.Controls.Add(Me.ckTiporkD)
    Me.pnSelexSx.Controls.Add(Me.ckTiporkC)
    Me.pnSelexSx.Controls.Add(Me.ckTiporkN)
    Me.pnSelexSx.Controls.Add(Me.ckTiporkI)
    Me.pnSelexSx.Controls.Add(Me.ckTiporkF)
    Me.pnSelexSx.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnSelexSx.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnSelexSx.Location = New System.Drawing.Point(2, 20)
    Me.pnSelexSx.Name = "pnSelexSx"
    Me.pnSelexSx.NTSActiveTrasparency = True
    Me.pnSelexSx.Size = New System.Drawing.Size(334, 297)
    Me.pnSelexSx.TabIndex = 22
    Me.pnSelexSx.Text = "NtsPanel1"
    '
    'ckTiporkNDiff
    '
    Me.ckTiporkNDiff.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTiporkNDiff.Location = New System.Drawing.Point(5, 153)
    Me.ckTiporkNDiff.Name = "ckTiporkNDiff"
    Me.ckTiporkNDiff.NTSCheckValue = "S"
    Me.ckTiporkNDiff.NTSUnCheckValue = "N"
    Me.ckTiporkNDiff.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTiporkNDiff.Properties.Appearance.Options.UseBackColor = True
    Me.ckTiporkNDiff.Properties.AutoHeight = False
    Me.ckTiporkNDiff.Properties.Caption = "Note di accredito differite emesse"
    Me.ckTiporkNDiff.Size = New System.Drawing.Size(189, 19)
    Me.ckTiporkNDiff.TabIndex = 44
    '
    'ckTiporkE
    '
    Me.ckTiporkE.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTiporkE.Location = New System.Drawing.Point(5, 170)
    Me.ckTiporkE.Name = "ckTiporkE"
    Me.ckTiporkE.NTSCheckValue = "S"
    Me.ckTiporkE.NTSUnCheckValue = "N"
    Me.ckTiporkE.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTiporkE.Properties.Appearance.Options.UseBackColor = True
    Me.ckTiporkE.Properties.AutoHeight = False
    Me.ckTiporkE.Properties.Caption = "Nota addebito emesse"
    Me.ckTiporkE.Size = New System.Drawing.Size(133, 19)
    Me.ckTiporkE.TabIndex = 43
    '
    'ckContCompensNoteAcc
    '
    Me.ckContCompensNoteAcc.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckContCompensNoteAcc.Location = New System.Drawing.Point(24, 136)
    Me.ckContCompensNoteAcc.Name = "ckContCompensNoteAcc"
    Me.ckContCompensNoteAcc.NTSCheckValue = "S"
    Me.ckContCompensNoteAcc.NTSUnCheckValue = "N"
    Me.ckContCompensNoteAcc.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckContCompensNoteAcc.Properties.Appearance.Options.UseBackColor = True
    Me.ckContCompensNoteAcc.Properties.AutoHeight = False
    Me.ckContCompensNoteAcc.Properties.Caption = "Genera registrazioni di compensazione con fatture emesse"
    Me.ckContCompensNoteAcc.Size = New System.Drawing.Size(304, 19)
    Me.ckContCompensNoteAcc.TabIndex = 40
    '
    'lbContCompensDDT
    '
    Me.lbContCompensDDT.AutoSize = True
    Me.lbContCompensDDT.BackColor = System.Drawing.Color.Transparent
    Me.lbContCompensDDT.Location = New System.Drawing.Point(45, 88)
    Me.lbContCompensDDT.Name = "lbContCompensDDT"
    Me.lbContCompensDDT.NTSDbField = ""
    Me.lbContCompensDDT.Size = New System.Drawing.Size(122, 13)
    Me.lbContCompensDDT.TabIndex = 42
    Me.lbContCompensDDT.Text = "anticipati su DDT emessi"
    Me.lbContCompensDDT.Tooltip = ""
    Me.lbContCompensDDT.UseMnemonic = False
    '
    'ckContCompensDDT
    '
    Me.ckContCompensDDT.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckContCompensDDT.Location = New System.Drawing.Point(24, 71)
    Me.ckContCompensDDT.Name = "ckContCompensDDT"
    Me.ckContCompensDDT.NTSCheckValue = "S"
    Me.ckContCompensDDT.NTSUnCheckValue = "N"
    Me.ckContCompensDDT.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckContCompensDDT.Properties.Appearance.Options.UseBackColor = True
    Me.ckContCompensDDT.Properties.AutoHeight = False
    Me.ckContCompensDDT.Properties.Caption = "Genera registrazioni di compensazione incassi"
    Me.ckContCompensDDT.Size = New System.Drawing.Size(250, 19)
    Me.ckContCompensDDT.TabIndex = 41
    '
    'ckTiporkB
    '
    Me.ckTiporkB.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTiporkB.Location = New System.Drawing.Point(5, 3)
    Me.ckTiporkB.Name = "ckTiporkB"
    Me.ckTiporkB.NTSCheckValue = "S"
    Me.ckTiporkB.NTSUnCheckValue = "N"
    Me.ckTiporkB.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTiporkB.Properties.Appearance.Options.UseBackColor = True
    Me.ckTiporkB.Properties.AutoHeight = False
    Me.ckTiporkB.Properties.Caption = "DDT emessi"
    Me.ckTiporkB.Size = New System.Drawing.Size(92, 19)
    Me.ckTiporkB.TabIndex = 34
    '
    'ckContIncDDT
    '
    Me.ckContIncDDT.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckContIncDDT.Location = New System.Drawing.Point(24, 20)
    Me.ckContIncDDT.Name = "ckContIncDDT"
    Me.ckContIncDDT.NTSCheckValue = "S"
    Me.ckContIncDDT.NTSUnCheckValue = "N"
    Me.ckContIncDDT.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckContIncDDT.Properties.Appearance.Options.UseBackColor = True
    Me.ckContIncDDT.Properties.AutoHeight = False
    Me.ckContIncDDT.Properties.Caption = "Contabilizza solo gli incassi anticipati"
    Me.ckContIncDDT.Size = New System.Drawing.Size(205, 19)
    Me.ckContIncDDT.TabIndex = 39
    '
    'lbBarra
    '
    Me.lbBarra.BackColor = System.Drawing.Color.Transparent
    Me.lbBarra.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbBarra.Dock = System.Windows.Forms.DockStyle.Right
    Me.lbBarra.Location = New System.Drawing.Point(332, 0)
    Me.lbBarra.Name = "lbBarra"
    Me.lbBarra.NTSDbField = ""
    Me.lbBarra.Size = New System.Drawing.Size(2, 297)
    Me.lbBarra.TabIndex = 29
    Me.lbBarra.Tooltip = ""
    Me.lbBarra.UseMnemonic = False
    '
    'ckTiporkA
    '
    Me.ckTiporkA.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTiporkA.Location = New System.Drawing.Point(5, 37)
    Me.ckTiporkA.Name = "ckTiporkA"
    Me.ckTiporkA.NTSCheckValue = "S"
    Me.ckTiporkA.NTSUnCheckValue = "N"
    Me.ckTiporkA.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTiporkA.Properties.Appearance.Options.UseBackColor = True
    Me.ckTiporkA.Properties.AutoHeight = False
    Me.ckTiporkA.Properties.Caption = "Fatture immediate emesse"
    Me.ckTiporkA.Size = New System.Drawing.Size(156, 19)
    Me.ckTiporkA.TabIndex = 14
    '
    'ckTiporkD
    '
    Me.ckTiporkD.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTiporkD.Location = New System.Drawing.Point(5, 54)
    Me.ckTiporkD.Name = "ckTiporkD"
    Me.ckTiporkD.NTSCheckValue = "S"
    Me.ckTiporkD.NTSUnCheckValue = "N"
    Me.ckTiporkD.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTiporkD.Properties.Appearance.Options.UseBackColor = True
    Me.ckTiporkD.Properties.AutoHeight = False
    Me.ckTiporkD.Properties.Caption = "Fatture differite emesse"
    Me.ckTiporkD.Size = New System.Drawing.Size(145, 19)
    Me.ckTiporkD.TabIndex = 15
    '
    'ckTiporkC
    '
    Me.ckTiporkC.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.ckTiporkC.Location = New System.Drawing.Point(5, 102)
    Me.ckTiporkC.Name = "ckTiporkC"
    Me.ckTiporkC.NTSCheckValue = "S"
    Me.ckTiporkC.NTSUnCheckValue = "N"
    Me.ckTiporkC.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTiporkC.Properties.Appearance.Options.UseBackColor = True
    Me.ckTiporkC.Properties.AutoHeight = False
    Me.ckTiporkC.Properties.Caption = "Corrispettivi emessi"
    Me.ckTiporkC.Size = New System.Drawing.Size(119, 19)
    Me.ckTiporkC.TabIndex = 16
    '
    'ckTiporkN
    '
    Me.ckTiporkN.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTiporkN.Location = New System.Drawing.Point(5, 119)
    Me.ckTiporkN.Name = "ckTiporkN"
    Me.ckTiporkN.NTSCheckValue = "S"
    Me.ckTiporkN.NTSUnCheckValue = "N"
    Me.ckTiporkN.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTiporkN.Properties.Appearance.Options.UseBackColor = True
    Me.ckTiporkN.Properties.AutoHeight = False
    Me.ckTiporkN.Properties.Caption = "Note di accredito emesse"
    Me.ckTiporkN.Size = New System.Drawing.Size(145, 19)
    Me.ckTiporkN.TabIndex = 17
    '
    'ckTiporkI
    '
    Me.ckTiporkI.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTiporkI.Location = New System.Drawing.Point(5, 204)
    Me.ckTiporkI.Name = "ckTiporkI"
    Me.ckTiporkI.NTSCheckValue = "S"
    Me.ckTiporkI.NTSUnCheckValue = "N"
    Me.ckTiporkI.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTiporkI.Properties.Appearance.Options.UseBackColor = True
    Me.ckTiporkI.Properties.AutoHeight = False
    Me.ckTiporkI.Properties.Caption = "Riemissione Ricevuta Fiscale"
    Me.ckTiporkI.Size = New System.Drawing.Size(162, 19)
    Me.ckTiporkI.TabIndex = 19
    '
    'ckTiporkF
    '
    Me.ckTiporkF.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTiporkF.Location = New System.Drawing.Point(5, 187)
    Me.ckTiporkF.Name = "ckTiporkF"
    Me.ckTiporkF.NTSCheckValue = "S"
    Me.ckTiporkF.NTSUnCheckValue = "N"
    Me.ckTiporkF.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTiporkF.Properties.Appearance.Options.UseBackColor = True
    Me.ckTiporkF.Properties.AutoHeight = False
    Me.ckTiporkF.Properties.Caption = "Ricevuta Fiscale emessa"
    Me.ckTiporkF.Size = New System.Drawing.Size(145, 19)
    Me.ckTiporkF.TabIndex = 18
    '
    'lbStatus
    '
    Me.lbStatus.AutoSize = True
    Me.lbStatus.BackColor = System.Drawing.Color.Transparent
    Me.lbStatus.Location = New System.Drawing.Point(9, 421)
    Me.lbStatus.Name = "lbStatus"
    Me.lbStatus.NTSDbField = ""
    Me.lbStatus.Size = New System.Drawing.Size(46, 13)
    Me.lbStatus.TabIndex = 14
    Me.lbStatus.Text = "lbStatus"
    Me.lbStatus.Tooltip = ""
    Me.lbStatus.UseMnemonic = False
    '
    'ckCheckProtocolli
    '
    Me.ckCheckProtocolli.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckCheckProtocolli.EditValue = True
    Me.ckCheckProtocolli.Location = New System.Drawing.Point(12, 76)
    Me.ckCheckProtocolli.Name = "ckCheckProtocolli"
    Me.ckCheckProtocolli.NTSCheckValue = "S"
    Me.ckCheckProtocolli.NTSUnCheckValue = "N"
    Me.ckCheckProtocolli.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckCheckProtocolli.Properties.Appearance.Options.UseBackColor = True
    Me.ckCheckProtocolli.Properties.AutoHeight = False
    Me.ckCheckProtocolli.Properties.Caption = "Verifica presenza di protocolli IVA doppi al termine dell'elaborazione"
    Me.ckCheckProtocolli.Size = New System.Drawing.Size(354, 19)
    Me.ckCheckProtocolli.TabIndex = 15
    '
    'ckGenEffetti
    '
    Me.ckGenEffetti.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckGenEffetti.Location = New System.Drawing.Point(4, 272)
    Me.ckGenEffetti.Name = "ckGenEffetti"
    Me.ckGenEffetti.NTSCheckValue = "S"
    Me.ckGenEffetti.NTSUnCheckValue = "N"
    Me.ckGenEffetti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckGenEffetti.Properties.Appearance.Options.UseBackColor = True
    Me.ckGenEffetti.Properties.AutoHeight = False
    Me.ckGenEffetti.Properties.Caption = "Genera effetti su scadenze RB di documenti emessi"
    Me.ckGenEffetti.Size = New System.Drawing.Size(276, 19)
    Me.ckGenEffetti.TabIndex = 50
    '
    'FRMVEPNFA_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(745, 440)
    Me.Controls.Add(Me.fmSelezione)
    Me.Controls.Add(Me.ckCheckProtocolli)
    Me.Controls.Add(Me.lbStatus)
    Me.Controls.Add(Me.lbEscomp)
    Me.Controls.Add(Me.edDatreg)
    Me.Controls.Add(Me.edEscomp)
    Me.Controls.Add(Me.lbNoteData)
    Me.Controls.Add(Me.lbDatreg)
    Me.Controls.Add(Me.lbEscompLabel)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRMVEPNFA_F"
    Me.Text = "CONTABILIZZAZIONE DOCUMENTI DI MAGAZZINO"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEscomp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatreg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmSelezione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmSelezione.ResumeLayout(False)
    CType(Me.pnSelexDx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSelexDx.ResumeLayout(False)
    Me.pnSelexDx.PerformLayout()
    CType(Me.ckContabAncheConScadSald.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDelEffettiNoPres.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTiporkJDiff.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTiporkJ.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTipobf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNumdocA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNumdocDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTiporkP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSeriedoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTiporkK.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTiporkS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatdocA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTiporkL.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatdocDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnnodoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckRielab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckContinc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnSelexSx, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSelexSx.ResumeLayout(False)
    Me.pnSelexSx.PerformLayout()
    CType(Me.ckTiporkNDiff.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTiporkE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckContCompensNoteAcc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckContCompensDDT.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTiporkB.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckContIncDDT.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTiporkA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTiporkD.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTiporkC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTiporkN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTiporkI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTiporkF.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckCheckProtocolli.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckGenEffetti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNVEPNFA", "BEVEPNFA", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128765662653302000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oClePnfa = CType(oTmp, CLEVEPNFA)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNVEPNFA", strRemoteServer, strRemotePort)
    AddHandler oClePnfa.RemoteEvent, AddressOf GestisciEventiEntity
    If oClePnfa.Init(oApp, oScript, oMenu.oCleComm, "TABZONE", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)

    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbElabora.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbIntegr.GlyphPath = (oApp.ChildImageDir & "\int.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      ckTiporkB.NTSSetParam(oMenu, oApp.Tr(Me, 129207135472187500, "DDT emessi"), "S", "N")
      ckTiporkE.NTSSetParam(oMenu, oApp.Tr(Me, 129207135648417968, "Nota addebito emesse"), "S", "N")
      ckTiporkJ.NTSSetParam(oMenu, oApp.Tr(Me, 129207135634980468, "Nota accredito ricevute"), "S", "N")
      ckTiporkJDiff.NTSSetParam(oMenu, oApp.Tr(Me, 129243451420742188, "Note di accredito differite ricevute"), "S", "N")
      ckTiporkK.NTSSetParam(oMenu, oApp.Tr(Me, 128765901298458000, "Fatture differite ricevute"), "S", "N")
      ckTiporkL.NTSSetParam(oMenu, oApp.Tr(Me, 128765901298614000, "Fatture immediate ricevute"), "S", "N")
      ckTiporkA.NTSSetParam(oMenu, oApp.Tr(Me, 128765901298926000, "Fatture immediate emesse"), "S", "N")
      ckTiporkD.NTSSetParam(oMenu, oApp.Tr(Me, 128765901299082000, "Fatture differite emesse"), "S", "N")
      ckTiporkP.NTSSetParam(oMenu, oApp.Tr(Me, 128765901299238000, "Fatture Ric. Fiscali differite"), "S", "N")
      ckTiporkC.NTSSetParam(oMenu, oApp.Tr(Me, 128765901299394000, "Corrispettivi emessi"), "S", "N")
      ckTiporkS.NTSSetParam(oMenu, oApp.Tr(Me, 128765901299550000, "Fatture Ric. Fiscali emesse"), "S", "N")
      ckTiporkN.NTSSetParam(oMenu, oApp.Tr(Me, 128765901299706000, "Note di accredito emesse"), "S", "N")
      ckTiporkNDiff.NTSSetParam(oMenu, oApp.Tr(Me, 129243451187626953, "Note di accredito differite emesse"), "S", "N")
      ckTiporkI.NTSSetParam(oMenu, oApp.Tr(Me, 128765901299862000, "Riemissione Ricevuta Fiscale"), "S", "N")
      ckTiporkF.NTSSetParam(oMenu, oApp.Tr(Me, 128765901300018000, "Ricevuta Fiscale emessa"), "S", "N")
      ckCheckProtocolli.NTSSetParam(oMenu, oApp.Tr(Me, 129768086942771166, "Controlla protocolli IVA doppi"), "S", "N")
      ckRielab.NTSSetParam(oMenu, oApp.Tr(Me, 128765901297990000, "Contabilizza anche documenti gia' contabilizzati in precedenza"), "S", "N")
      ckDelEffettiNoPres.NTSSetParam(oMenu, oApp.Tr(Me, 130373694223942047, "Cancella emiss. effetti collegati a fattura NON pres. banca"), "S", "N")
      ckContabAncheConScadSald.NTSSetParam(oMenu, oApp.Tr(Me, 130373694238388611, "Contabilizza anche documenti con scadenze saldate"), "S", "N")
      ckContinc.NTSSetParam(oMenu, oApp.Tr(Me, 128765910638686000, "Contabilizza anche incassi e pagamenti associati"), "S", "N")
      edTipobf.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128765901295962000, "Tipo bolla/fattura"), tabtpbf)
      edNumdocA.NTSSetParam(oMenu, oApp.Tr(Me, 128765901296118000, "A numero documento"), "0", 9, 0, 999999999)
      edNumdocDa.NTSSetParam(oMenu, oApp.Tr(Me, 128765901296274000, "Da numero documento"), "0", 9, 0, 999999999)
      edSeriedoc.NTSSetParam(oMenu, oApp.Tr(Me, 128765901296586000, "Da serie documento"), CLN__STD.SerieMaxLen, False)
      edDatdocA.NTSSetParam(oMenu, oApp.Tr(Me, 128765901296742000, "A data documento"), False)
      edDatdocDa.NTSSetParam(oMenu, oApp.Tr(Me, 128765901296898000, "Da data documento"), False)
      edAnnodoc.NTSSetParam(oMenu, oApp.Tr(Me, 128765901297054000, "Anno documenti da contabilizzare"), "0", 4, 1900, 2099)
      edDatreg.NTSSetParam(oMenu, oApp.Tr(Me, 128765901300174000, "Data registrazione"), True)
      edEscomp.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128765901300330000, "Esercizio di competenza"), tabesco)
      ckSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128934184376473945, "Solo serie"), "S", "N")
      ckContIncDDT.NTSSetParam(oMenu, oApp.Tr(Me, 129201156311298828, "DDT emessi contabilizza solo gli incassi anticipati"), "S", "N")
      ckContCompensDDT.NTSSetParam(oMenu, oApp.Tr(Me, 129201156325136719, "Fatt. diff. genera reg. di compens. incassi anticipati su DDT em"), "S", "N")
      ckContCompensNoteAcc.NTSSetParam(oMenu, oApp.Tr(Me, 129201156337216797, "Note accr. emesse genera reg. di compens. con fatture em."), "S", "N")
      ckGenEffetti.NTSSetParam(oMenu, oApp.Tr(Me, 130397962124569396, "Genera effetti su scadenze RB di documenti emessi"), "S", "N")

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

  Overloads Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    Try
      If Not IsMyThrowRemoteEvent() Then Return 'il messaggio non è per questa form ...
      MyBase.GestisciEventiEntity(sender, e)

      If e.TipoEvento.Trim.Length < 10 Then Return
      Select Case e.TipoEvento.Substring(0, 10)
        Case "AGGIOLABEL"
          lbStatus.Text = e.Message
          lbStatus.Refresh()
      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi di Form"
  Public Overridable Sub FRMVEPNFA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      ckDelEffettiNoPres.Checked = CBool(oMenu.GetSettingBus("BSVEPNFA", "RECENT", ".", "DelEffettiNoPres", "0", " ", "0"))
      ckContabAncheConScadSald.Checked = CBool(oMenu.GetSettingBus("BSVEPNFA", "RECENT", ".", "ContabAncheConScadSald", "0", " ", "0"))
      ckGenEffetti.Checked = CBool(oMenu.GetSettingBus("BSVEPNFA", "RECENT", ".", "GenEffetti", "0", " ", "0"))

      If oClePnfa.bModuliAcquistati = False Then
        ckGenEffetti.Checked = False
        ckGenEffetti.Enabled = False
      End If

      ckTiporkB_CheckedChanged(ckTiporkB, Nothing)
      ckTiporkN_CheckedChanged(ckTiporkN, Nothing)
      ckTiporkD_CheckedChanged(ckTiporkD, Nothing)
      lbStatus.Text = ""
      edAnnodoc.Text = DateTime.Now.Year.ToString
      edDatdocDa.Text = DateTime.Now.ToShortDateString
      edDatdocA.Text = DateTime.Now.ToShortDateString
      ckSerie_CheckedChanged(ckSerie, Nothing)
      ckRielab_CheckedChanged(ckRielab, Nothing)
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      GctlApplicaDefaultValue()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMVEPNFA_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Dim dttTmp As New DataTable
    Try
      oMenu.ValCodiceDb(DittaCorrente, DittaCorrente, "TABANAZ", "S", "", dttTmp)
      edEscomp.Text = NTSCInt(dttTmp.Rows(0)!tb_escomp).ToString
      ' legge se professionista esce
      If dttTmp.Rows(0)!tb_azprofes.ToString = "S" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128765915599634000, "Questo programma non può essere utilizzato su una ditta di tipo 'Professionista'! "))
        Me.Close()
        Return
      End If

      '-------------------------------------------------
      'se sono stato chiamato in modalità batch:
      'esempio di riga di comando:
      'mirto . pr14 business BNVEPNFA /B c:\bus14\asc\bnvepnfa.bub
      If oApp.Batch And oApp.AvvioProgrammaParametri.Trim <> "" Then
        'Me.Visible = False
        Me.Top = -10000
        Me.Left = -10000

        'lancio l'elaborazione. i parametri di avvio verranno presi dal file BNVEPNFA.BUB, poi esco
        Elabora()
        Me.Close()
        Return
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      dttTmp.Clear()
    End Try
  End Sub

  Public Overridable Sub FRMVEPNFA_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Dim ctrlTmp As Control = Nothing
    Dim strTipork1 As String = ""
    Try
      '--------------------------------------------
      'gestione dello zoom:
      'scrittura del file per la schedulazione BNVEPNFA.BUB
      'int=N
      'escomp = 2009
      'datreg=
      'tipork=A;D;N
      'ckDataIva = 0
      'dativa = 31/1/6
      'ckNoAutoriz = -1
      'annodoc = 2006
      'dadata = [#DATA ODIERNA-15]
      'adata = [#DATA ODIERNA-7]
      'daserie=
      'aserie = Z
      'danumero = 0
      'anumero = 999999999
      'tipobf = 0
      'ckRielab = -1
      'ckDelEffettiNoPres = -1
      'ckContabAncheConScadSald = -1
      'ckIncassi = 0
      'ckCa = -1
      'ckContIncDDT = 0
      'ckContCompensDDT = 0
      'ckContCompensNoteAcc = 0
      'bBnrepnco = 0
      'prgParent=BNVEPNFA
      'ckGenEffetti = -1
      'ckAggProvvig = -1

      If e.KeyCode = Keys.F5 And e.Control = True And e.Alt = True And e.Shift = False Then

        Me.ValidaLastControl()

        '-----------------------------------------------------------------------
        'Controlla se è stato selezionato qualcosa
        If ckTiporkA.Checked = False And ckTiporkC.Checked = False And ckTiporkD.Checked = False _
          And ckTiporkF.Checked = False And ckTiporkI.Checked = False And ckTiporkK.Checked = False _
          And ckTiporkL.Checked = False And ckTiporkN.Checked = False And ckTiporkP.Checked = False _
          And ckTiporkS.Checked = False And ckTiporkB.Checked = False And ckTiporkE.Checked = False _
          And ckTiporkJ.Checked = False And ckTiporkNDiff.Checked = False And ckTiporkJDiff.Checked = False Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128768358175030000, "Selezionare almeno un tipo di documento da elaborare"))
          Return
        End If

        If System.IO.File.Exists(oApp.AscDir & "\BNVEPNFA.BUB") Then
          If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128744957738192000, "Esiste già un file con nome |" & oApp.AscDir & "\BNVEPNFA.BUB" & "|: sovrascriverlo?")) = Windows.Forms.DialogResult.No Then Return
        End If
        Dim w1 As New System.IO.StreamWriter(oApp.AscDir & "\BNVEPNFA.BUB", False)

        If ckTiporkB.Checked Then strTipork1 += "B;"
        If ckTiporkA.Checked Then strTipork1 += "A;"
        If ckTiporkC.Checked Then strTipork1 += "C;"
        If ckTiporkD.Checked Then strTipork1 += "D;"
        If ckTiporkF.Checked Then strTipork1 += "F;"
        If ckTiporkI.Checked Then strTipork1 += "I;"
        If ckTiporkK.Checked Then strTipork1 += "K;"
        If ckTiporkL.Checked Then strTipork1 += "L;"
        If ckTiporkN.Checked Then strTipork1 += "N;"
        If ckTiporkE.Checked Then strTipork1 += "E;"
        If ckTiporkP.Checked Then strTipork1 += "P;"
        If ckTiporkS.Checked Then strTipork1 += "S;"
        If ckTiporkJ.Checked Then strTipork1 += "J;"
        If ckTiporkNDiff.Checked Then strTipork1 += "£;"
        If ckTiporkJDiff.Checked Then strTipork1 += "(;"
        If strTipork1.Length > 0 Then strTipork1 = strTipork1.Substring(0, strTipork1.Length - 1)

        w1.WriteLine("Int=" & IIf(tlbIntegr.Down, "S", "N").ToString)
        w1.WriteLine("escomp=" & edEscomp.Text)
        w1.WriteLine("datreg=" & edDatreg.Text)
        w1.WriteLine("tipork=" & strTipork1)
        w1.WriteLine("ckDataIva=0")
        w1.WriteLine("dativa=")
        w1.WriteLine("ckNoAutoriz=-1")
        w1.WriteLine("annodoc=" & edAnnodoc.Text)
        w1.WriteLine("dadata=" & edDatdocDa.Text)
        w1.WriteLine("adata=" & edDatdocA.Text)
        w1.WriteLine("daserie=" & IIf(ckSerie.Checked, edSeriedoc.Text, " ").ToString)
        w1.WriteLine("aserie=" & IIf(ckSerie.Checked, edSeriedoc.Text, "Z".PadLeft(SerieMaxLen, "Z"c)).ToString)
        w1.WriteLine("danumero=" & edNumdocDa.Text)
        w1.WriteLine("anumero=" & edNumdocA.Text)
        w1.WriteLine("tipobf=" & edTipobf.Text)
        w1.WriteLine("ckRielab=" & IIf(ckRielab.Checked, "-1", "0").ToString)
        w1.WriteLine("ckDelEffettiNoPres=" & IIf(ckDelEffettiNoPres.Checked, "-1", "0").ToString)
        w1.WriteLine("ckContabAncheConScadSald=" & IIf(ckContabAncheConScadSald.Checked, "-1", "0").ToString)
        w1.WriteLine("ckIncassi=" & IIf(ckContinc.Checked, "-1", "0").ToString)
        w1.WriteLine("ckCa=0")
        w1.WriteLine("ckContIncDDT=" & IIf(ckContIncDDT.Checked, "-1", "0").ToString)
        w1.WriteLine("ckContCompensDDT=" & IIf(ckContCompensDDT.Checked, "-1", "0").ToString)
        w1.WriteLine("ckContCompensNoteAcc=" & IIf(ckContCompensNoteAcc.Checked, "-1", "0").ToString)
        w1.WriteLine("ckCheckProtocolli=" & IIf(ckCheckProtocolli.Checked, "-1", "0").ToString)
        w1.WriteLine("bBnrepnco=0")
        w1.WriteLine("prgParent=BNVEPNFA")
        w1.WriteLine("ckGenEffetti=" & IIf(ckGenEffetti.Checked, "-1", "0").ToString)
        w1.WriteLine("ckAggProvvig=0")
        w1.Flush()
        w1.Close()
        oApp.MsgBoxInfo(oApp.Tr(Me, 128744371685129000, "Creato file |" & oApp.AscDir & "\BNVEPNFA.BUB" & "| correttamente"))
        e.Handled = True    'altrimenti anche il controllo riceve l'F5 e la routine ZOOM viene eseguita 2 volte!!!
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbElabora_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbElabora.ItemClick
    Dim strTipork1 As String = ""
    Try
      Me.ValidaLastControl()
      '-------------------------
      'passo le informazioni per l'elaborazione
      If ckTiporkB.Checked Then strTipork1 += "B;"
      If ckTiporkA.Checked Then strTipork1 += "A;"
      If ckTiporkC.Checked Then strTipork1 += "C;"
      If ckTiporkD.Checked Then strTipork1 += "D;"
      If ckTiporkF.Checked Then strTipork1 += "F;"
      If ckTiporkI.Checked Then strTipork1 += "I;"
      If ckTiporkK.Checked Then strTipork1 += "K;"
      If ckTiporkL.Checked Then strTipork1 += "L;"
      If ckTiporkN.Checked Then strTipork1 += "N;"
      If ckTiporkE.Checked Then strTipork1 += "E;"
      If ckTiporkP.Checked Then strTipork1 += "P;"
      If ckTiporkS.Checked Then strTipork1 += "S;"
      If ckTiporkJ.Checked Then strTipork1 += "J;"
      If ckTiporkNDiff.Checked Then strTipork1 += "£;"
      If ckTiporkJDiff.Checked Then strTipork1 += "(;"
      If strTipork1.Length > 0 Then strTipork1 = strTipork1.Substring(0, strTipork1.Length - 1)

      oClePnfa.bInt = tlbIntegr.Down
      oClePnfa.nEscomp = NTSCInt(edEscomp.Text)
      oClePnfa.strDatregForm = edDatreg.Text
      oClePnfa.strTiporks = strTipork1
      oClePnfa.bDerogaIva = False
      oClePnfa.strDtcomivaForm = ""
      oClePnfa.bAutorizzato = True
      oClePnfa.nAnnoDoc = NTSCInt(edAnnodoc.Text)
      oClePnfa.strDatdocDa = edDatdocDa.Text
      oClePnfa.strDatdocA = edDatdocA.Text
      oClePnfa.strSeriedocDa = IIf(ckSerie.Checked, edSeriedoc.Text, " ").ToString
      oClePnfa.strSeriedocA = IIf(ckSerie.Checked, edSeriedoc.Text, "Z".PadLeft(SerieMaxLen, "Z"c)).ToString
      oClePnfa.lNumdocDa = NTSCInt(edNumdocDa.Text)
      oClePnfa.lNumdocA = NTSCInt(edNumdocA.Text)
      oClePnfa.nTipobf = NTSCInt(edTipobf.Text)
      oClePnfa.bRielab = ckRielab.Checked
      oClePnfa.bDelEffettiNoPres = ckDelEffettiNoPres.Checked
      oClePnfa.bContabAncheConScadSald = ckContabAncheConScadSald.Checked
      oClePnfa.bIncassi = ckContinc.Checked
      oClePnfa.bCa = False
      oClePnfa.bContIncDDT = ckContIncDDT.Checked
      oClePnfa.bContCompensDDT = ckContCompensDDT.Checked
      oClePnfa.bContCompensNoteAcc = ckContCompensNoteAcc.Checked
      oClePnfa.bCheckProtocolli = ckCheckProtocolli.Checked
      oClePnfa.bBnrepnco = False
      oClePnfa.strPrgParent = "BNVEPNFA"
      oClePnfa.bGenEffetti = ckGenEffetti.Checked
      oClePnfa.bAggProvvig = False 'ckAggProvvig.Checked
      Elabora()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

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

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampavideo.ItemClick
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer

    Try
      oApp.MsgBoxInfo(oApp.Tr(Me, 129719796898831263, "Saranno stampate tutte le registrazioni inserite/modificate in data odierna dall'operatore '|" & oApp.User.Nome & "|' nell'esercizio contabile |" & edEscomp.Text & "|"))

      '--------------------------------------------------
      'preparo il motore di stampa
      strCrpe = "{prinot.codditt} = '" & oApp.Ditta & "' " & _
                " And {prinot.pn_escomp} = " & edEscomp.Text & _
                " And UpperCase({PRINOT.pn_opnome}) = '" & UCase(oApp.User.Nome) & "' " & _
                " And {PRINOT.pn_integr} = '" & IIf(tlbIntegr.Down, "S", "N").ToString & "' " & _
                " And {PRINOT.pn_ultagg} in Date(" & DateTime.Now.ToString("yyyy,MM,dd") & ")" & _
                " To Date(" & DateTime.Now.ToString("yyyy,MM,dd") & ")"

      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSCGSTPN", "Reports1", " ", 0, 0, "BSCGSTPN.RPT", False, "Stampa prima nota", False)
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

  Public Overridable Sub tlbVislog_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbVislog.ItemClick
    Try
      If System.IO.File.Exists(oClePnfa.LogFileName) Then
        NTSProcessStart("notepad", oClePnfa.LogFileName)
      Else
        oApp.MsgBoxErr(oApp.Tr(Me, 128765681106560000, "File di log '|" & oClePnfa.LogFileName & "| non trovato."))
      End If
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
#End Region

  Public Overridable Sub edEscomp_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edEscomp.Validated
    Dim strTmp As String = ""
    Try
      If oClePnfa Is Nothing Then Return
      If Not oClePnfa.edEscomp_Validated(NTSCInt(edEscomp.Text), strTmp) Then
        edEscomp.Text = NTSCStr(edEscomp.OldEditValue)
      Else
        lbEscomp.Text = strTmp
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edTipobf_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTipobf.Validated
    Dim strTmp As String = ""
    Try
      If oClePnfa Is Nothing Then Return
      If Not oClePnfa.edTipobf_Validated(NTSCInt(edTipobf.Text), strTmp) Then
        edTipobf.Text = NTSCStr(edTipobf.OldEditValue)
      Else
        lbTipobf.Text = strTmp
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckSerie_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSerie.CheckedChanged
    Try
      If ckSerie.Checked Then
        GctlSetVisEnab(edSeriedoc, False)
        edSeriedoc.Focus()
      Else
        edSeriedoc.Enabled = False
        edSeriedoc.Text = " "
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckRielab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckRielab.CheckedChanged
    Try
      ckDelEffettiNoPres.Enabled = ckRielab.Checked
      ckContabAncheConScadSald.Enabled = ckRielab.Checked

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function Elabora() As Boolean
    Try
      Me.Cursor = Cursors.WaitCursor

      oMenu.SaveSettingBus("BSVEPNFA", "RECENT", ".", "DelEffettiNoPres", IIf(ckDelEffettiNoPres.Checked, "-1", "0").ToString, " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("BSVEPNFA", "RECENT", ".", "ContabAncheConScadSald", IIf(ckContabAncheConScadSald.Checked, "-1", "0").ToString, " ", "NS.", "...", "...")
      oMenu.SaveSettingBus("BSVEPNFA", "RECENT", ".", "GenEffetti", IIf(ckGenEffetti.Checked, "-1", "0").ToString, " ", "NS.", "...", "...")

      oClePnfa.Elabora()

      'se serve visualizzo il file di log
      Me.Cursor = Cursors.Default
      oApp.MsgBoxInfo(oApp.Tr(Me, 127939835583750000, "Contabilizzazione documenti terminata."))

      If oClePnfa.LogError = True Then
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127940796626250000, "Esistono dei messaggi nel file di log del programma. Visualizzare il file?")) = Windows.Forms.DialogResult.Yes Then
          NTSProcessStart("notepad", oClePnfa.LogFileName)
        End If
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      lbStatus.Text = ""
      Me.Cursor = Cursors.Default
    End Try
  End Function


  Public Overridable Sub ckTiporkB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckTiporkB.CheckedChanged
    Try
      If ckTiporkB.Checked Then
        GctlSetVisEnab(ckContIncDDT, False)
      Else
        ckContIncDDT.Checked = False
        ckContIncDDT.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub ckTiporkD_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckTiporkD.CheckedChanged
    Try
      If ckTiporkD.Checked Then
        GctlSetVisEnab(ckContCompensDDT, False)
        GctlSetVisEnab(lbContCompensDDT, False)
      Else
        ckContCompensDDT.Checked = False
        ckContCompensDDT.Enabled = False
        lbContCompensDDT.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub ckTiporkN_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckTiporkN.CheckedChanged
    Try
      If ckTiporkN.Checked Then
        GctlSetVisEnab(ckContCompensNoteAcc, False)
      Else
        ckContCompensNoteAcc.Checked = False
        ckContCompensNoteAcc.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edSeriedoc_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSeriedoc.Validated
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edSeriedoc.Text, False)
      If strTmp <> edSeriedoc.Text Then edSeriedoc.Text = strTmp

    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub

End Class