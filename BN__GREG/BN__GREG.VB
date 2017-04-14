Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__GREG
  Private Moduli_P As Integer = CLN__STD.bsModAll
  Private ModuliExt_P As Integer = CLN__STD.bsModExtAll
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
  Public dsGreg As DataSet
  Public oCleGreg As CLE__GREG
  Public oCallParams As CLE__CLDP
  Public dcGreg As BindingSource = New BindingSource

  Public nodReg As TreeNode
  Public bLoadTree As Boolean
  Public strChild As String
  Public strCopyKey As String
  Public nLivDa As Integer
  Public dsCopia As DataSet
  Public strCopyNomOp As String
  Public strCopyCodAz As String
  Public strCopyTipDoc As String
  Public nTotRiga As Integer

  Const CAMPI As Integer = 4
  Public strCopy(0 To 100, 0 To CAMPI) As String

  Public dsCopiaCancRighe As DataSet

  Public bEsciDaZoom As Boolean = False

  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbRecordNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRecordSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRecordRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRecordCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbModificaRbs As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbControlloIntegrità As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbRinominaCartella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsportaRamo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbImportaRamo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCopiaCartella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbIncollaCartella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSelezionaCartella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCopiaRighe As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbIncollaRighe As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEliminaRighe As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnGreg As NTSInformatica.NTSPanel
  Public WithEvents pnLeft As NTSInformatica.NTSPanel
  Public WithEvents trGreg As NTSInformatica.NTSTreeView
  Public WithEvents pnFill As NTSInformatica.NTSPanel
  Public WithEvents grProp As NTSInformatica.NTSGrid
  Public WithEvents grvProp As NTSInformatica.NTSGridView
  Public WithEvents rp_nomprop As NTSInformatica.NTSGridColumn
  Public WithEvents rp_valprop As NTSInformatica.NTSGridColumn
  Public WithEvents rp_idut As NTSInformatica.NTSGridColumn
  Public WithEvents rp_idaz As NTSInformatica.NTSGridColumn
  Public WithEvents rp_idtdoc As NTSInformatica.NTSGridColumn
  Public WithEvents rp_liv1 As NTSInformatica.NTSGridColumn
  Public WithEvents rp_liv2 As NTSInformatica.NTSGridColumn
  Public WithEvents rp_liv3 As NTSInformatica.NTSGridColumn
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents lbPath As NTSInformatica.NTSLabel
  Public WithEvents cmdFiltri As NTSInformatica.NTSButton
  Public WithEvents fmFiltri As NTSInformatica.NTSGroupBox
  Public WithEvents edCode2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edCode1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edCode0 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDoc As NTSInformatica.NTSLabel
  Public WithEvents lbAzienda As NTSInformatica.NTSLabel
  Public WithEvents lbUtente As NTSInformatica.NTSLabel
  Public WithEvents pnFiltriLeft As NTSInformatica.NTSPanel
  Public WithEvents lbDip As NTSInformatica.NTSLabel
  Public WithEvents ckDip2 As NTSInformatica.NTSCheckBox
  Public WithEvents ckDip1 As NTSInformatica.NTSCheckBox
  Public WithEvents ckDip0 As NTSInformatica.NTSCheckBox
  Public WithEvents xx_seleziona As NTSInformatica.NTSGridColumn
  Public WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
  Public WithEvents ImageList1 As System.Windows.Forms.ImageList
  Public WithEvents tlbPropPrinter As NTSInformatica.NTSBarMenuItem

  Private components As System.ComponentModel.IContainer
#End Region

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__GREG))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecordNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecordSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecordRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecordCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbRinominaCartella = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsportaRamo = New NTSInformatica.NTSBarButtonItem
    Me.tlbImportaRamo = New NTSInformatica.NTSBarButtonItem
    Me.tlbCopiaCartella = New NTSInformatica.NTSBarButtonItem
    Me.tlbIncollaCartella = New NTSInformatica.NTSBarButtonItem
    Me.tlbSelezionaCartella = New NTSInformatica.NTSBarButtonItem
    Me.tlbCopiaRighe = New NTSInformatica.NTSBarButtonItem
    Me.tlbIncollaRighe = New NTSInformatica.NTSBarButtonItem
    Me.tlbEliminaRighe = New NTSInformatica.NTSBarButtonItem
    Me.tlbModificaRbs = New NTSInformatica.NTSBarButtonItem
    Me.tlbControlloIntegrità = New NTSInformatica.NTSBarButtonItem
    Me.tlbPropPrinter = New NTSInformatica.NTSBarMenuItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnGreg = New NTSInformatica.NTSPanel
    Me.pnFill = New NTSInformatica.NTSPanel
    Me.grProp = New NTSInformatica.NTSGrid
    Me.grvProp = New NTSInformatica.NTSGridView
    Me.rp_nomprop = New NTSInformatica.NTSGridColumn
    Me.rp_valprop = New NTSInformatica.NTSGridColumn
    Me.rp_idut = New NTSInformatica.NTSGridColumn
    Me.rp_idaz = New NTSInformatica.NTSGridColumn
    Me.rp_idtdoc = New NTSInformatica.NTSGridColumn
    Me.rp_liv1 = New NTSInformatica.NTSGridColumn
    Me.rp_liv2 = New NTSInformatica.NTSGridColumn
    Me.rp_liv3 = New NTSInformatica.NTSGridColumn
    Me.xx_seleziona = New NTSInformatica.NTSGridColumn
    Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.lbPath = New NTSInformatica.NTSLabel
    Me.cmdFiltri = New NTSInformatica.NTSButton
    Me.fmFiltri = New NTSInformatica.NTSGroupBox
    Me.pnFiltriLeft = New NTSInformatica.NTSPanel
    Me.lbDip = New NTSInformatica.NTSLabel
    Me.ckDip2 = New NTSInformatica.NTSCheckBox
    Me.ckDip1 = New NTSInformatica.NTSCheckBox
    Me.ckDip0 = New NTSInformatica.NTSCheckBox
    Me.edCode2 = New NTSInformatica.NTSTextBoxStr
    Me.edCode1 = New NTSInformatica.NTSTextBoxStr
    Me.edCode0 = New NTSInformatica.NTSTextBoxStr
    Me.lbDoc = New NTSInformatica.NTSLabel
    Me.lbAzienda = New NTSInformatica.NTSLabel
    Me.lbUtente = New NTSInformatica.NTSLabel
    Me.pnLeft = New NTSInformatica.NTSPanel
    Me.trGreg = New NTSInformatica.NTSTreeView
    Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnGreg, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGreg.SuspendLayout()
    CType(Me.pnFill, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFill.SuspendLayout()
    CType(Me.grProp, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvProp, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.fmFiltri, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmFiltri.SuspendLayout()
    CType(Me.pnFiltriLeft, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFiltriLeft.SuspendLayout()
    CType(Me.ckDip2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDip1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDip0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCode2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCode1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCode0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnLeft, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnLeft.SuspendLayout()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(231, Byte), Integer))
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbCancella, Me.tlbRecordNuovo, Me.tlbRecordSalva, Me.tlbRecordRipristina, Me.tlbRecordCancella, Me.tlbZoom, Me.tlbModificaRbs, Me.tlbControlloIntegrità, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbRinominaCartella, Me.tlbEsportaRamo, Me.tlbImportaRamo, Me.tlbCopiaCartella, Me.tlbIncollaCartella, Me.tlbSelezionaCartella, Me.tlbCopiaRighe, Me.tlbIncollaRighe, Me.tlbEliminaRighe, Me.tlbPropPrinter})
    Me.NtsBarManager1.MaxItemId = 23
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordNuovo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbNuovo
    '
    Me.tlbNuovo.Caption = "Nuovo"
    Me.tlbNuovo.Glyph = CType(resources.GetObject("tlbNuovo.Glyph"), System.Drawing.Image)
    Me.tlbNuovo.Id = 0
    Me.tlbNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2)
    Me.tlbNuovo.Name = "tlbNuovo"
    Me.tlbNuovo.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.Id = 1
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbRecordNuovo
    '
    Me.tlbRecordNuovo.Caption = "Record Nuovo"
    Me.tlbRecordNuovo.Glyph = CType(resources.GetObject("tlbRecordNuovo.Glyph"), System.Drawing.Image)
    Me.tlbRecordNuovo.Id = 2
    Me.tlbRecordNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F2))
    Me.tlbRecordNuovo.Name = "tlbRecordNuovo"
    Me.tlbRecordNuovo.Visible = True
    '
    'tlbRecordSalva
    '
    Me.tlbRecordSalva.Caption = "Record Salva"
    Me.tlbRecordSalva.Glyph = CType(resources.GetObject("tlbRecordSalva.Glyph"), System.Drawing.Image)
    Me.tlbRecordSalva.Id = 3
    Me.tlbRecordSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F9))
    Me.tlbRecordSalva.Name = "tlbRecordSalva"
    Me.tlbRecordSalva.Visible = True
    '
    'tlbRecordRipristina
    '
    Me.tlbRecordRipristina.Caption = "Record Ripristina"
    Me.tlbRecordRipristina.Glyph = CType(resources.GetObject("tlbRecordRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRecordRipristina.Id = 4
    Me.tlbRecordRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F8))
    Me.tlbRecordRipristina.Name = "tlbRecordRipristina"
    Me.tlbRecordRipristina.Visible = True
    '
    'tlbRecordCancella
    '
    Me.tlbRecordCancella.Caption = "Record Cancella"
    Me.tlbRecordCancella.Glyph = CType(resources.GetObject("tlbRecordCancella.Glyph"), System.Drawing.Image)
    Me.tlbRecordCancella.Id = 5
    Me.tlbRecordCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F4))
    Me.tlbRecordCancella.Name = "tlbRecordCancella"
    Me.tlbRecordCancella.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 6
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 12
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRinominaCartella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsportaRamo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImportaRamo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCopiaCartella, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbIncollaCartella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSelezionaCartella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCopiaRighe, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbIncollaRighe), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEliminaRighe), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbModificaRbs, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbControlloIntegrità), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPropPrinter, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbRinominaCartella
    '
    Me.tlbRinominaCartella.Caption = "Rinomina cartella"
    Me.tlbRinominaCartella.Id = 13
    Me.tlbRinominaCartella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbRinominaCartella.Name = "tlbRinominaCartella"
    Me.tlbRinominaCartella.Visible = True
    '
    'tlbEsportaRamo
    '
    Me.tlbEsportaRamo.Caption = "Esporta ramo"
    Me.tlbEsportaRamo.Id = 14
    Me.tlbEsportaRamo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F5))
    Me.tlbEsportaRamo.Name = "tlbEsportaRamo"
    Me.tlbEsportaRamo.Visible = True
    '
    'tlbImportaRamo
    '
    Me.tlbImportaRamo.Caption = "Importa ramo"
    Me.tlbImportaRamo.Id = 15
    Me.tlbImportaRamo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F6))
    Me.tlbImportaRamo.Name = "tlbImportaRamo"
    Me.tlbImportaRamo.Visible = True
    '
    'tlbCopiaCartella
    '
    Me.tlbCopiaCartella.Caption = "Copia cartella"
    Me.tlbCopiaCartella.Id = 16
    Me.tlbCopiaCartella.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
    Me.tlbCopiaCartella.Name = "tlbCopiaCartella"
    Me.tlbCopiaCartella.Visible = True
    '
    'tlbIncollaCartella
    '
    Me.tlbIncollaCartella.Caption = "Incolla cartella"
    Me.tlbIncollaCartella.Id = 17
    Me.tlbIncollaCartella.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L))
    Me.tlbIncollaCartella.Name = "tlbIncollaCartella"
    Me.tlbIncollaCartella.Visible = True
    '
    'tlbSelezionaCartella
    '
    Me.tlbSelezionaCartella.Caption = "Seleziona cartella"
    Me.tlbSelezionaCartella.Id = 18
    Me.tlbSelezionaCartella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3)
    Me.tlbSelezionaCartella.Name = "tlbSelezionaCartella"
    Me.tlbSelezionaCartella.Visible = True
    '
    'tlbCopiaRighe
    '
    Me.tlbCopiaRighe.Caption = "Copia righe"
    Me.tlbCopiaRighe.Id = 19
    Me.tlbCopiaRighe.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F1))
    Me.tlbCopiaRighe.Name = "tlbCopiaRighe"
    Me.tlbCopiaRighe.Visible = True
    '
    'tlbIncollaRighe
    '
    Me.tlbIncollaRighe.Caption = "Incolla righe"
    Me.tlbIncollaRighe.Id = 20
    Me.tlbIncollaRighe.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F2))
    Me.tlbIncollaRighe.Name = "tlbIncollaRighe"
    Me.tlbIncollaRighe.Visible = True
    '
    'tlbEliminaRighe
    '
    Me.tlbEliminaRighe.Caption = "Elimina righe"
    Me.tlbEliminaRighe.Id = 21
    Me.tlbEliminaRighe.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F3))
    Me.tlbEliminaRighe.Name = "tlbEliminaRighe"
    Me.tlbEliminaRighe.Visible = True
    '
    'tlbModificaRbs
    '
    Me.tlbModificaRbs.Caption = "Modifica Rbs"
    Me.tlbModificaRbs.Id = 7
    Me.tlbModificaRbs.Name = "tlbModificaRbs"
    Me.tlbModificaRbs.Visible = True
    '
    'tlbControlloIntegrità
    '
    Me.tlbControlloIntegrità.Caption = "Controllo Integrità"
    Me.tlbControlloIntegrità.Id = 8
    Me.tlbControlloIntegrità.Name = "tlbControlloIntegrità"
    Me.tlbControlloIntegrità.Visible = True
    '
    'tlbPropPrinter
    '
    Me.tlbPropPrinter.Caption = "Proprietà stampante"
    Me.tlbPropPrinter.Id = 22
    Me.tlbPropPrinter.Name = "tlbPropPrinter"
    Me.tlbPropPrinter.NTSIsCheckBox = False
    Me.tlbPropPrinter.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 10
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 11
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnGreg
    '
    Me.pnGreg.AllowDrop = True
    Me.pnGreg.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGreg.Appearance.Options.UseBackColor = True
    Me.pnGreg.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGreg.Controls.Add(Me.pnFill)
    Me.pnGreg.Controls.Add(Me.pnTop)
    Me.pnGreg.Controls.Add(Me.pnLeft)
    Me.pnGreg.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.pnGreg.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGreg.Location = New System.Drawing.Point(0, 30)
    Me.pnGreg.Name = "pnGreg"
    Me.pnGreg.Size = New System.Drawing.Size(701, 432)
    Me.pnGreg.TabIndex = 6
    Me.pnGreg.Text = "NtsPanel1"
    '
    'pnFill
    '
    Me.pnFill.AllowDrop = True
    Me.pnFill.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnFill.Appearance.Options.UseBackColor = True
    Me.pnFill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnFill.Controls.Add(Me.grProp)
    Me.pnFill.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnFill.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnFill.Location = New System.Drawing.Point(281, 162)
    Me.pnFill.Name = "pnFill"
    Me.pnFill.Size = New System.Drawing.Size(420, 270)
    Me.pnFill.TabIndex = 13
    Me.pnFill.Text = "NtsPanel3"
    '
    'grProp
    '
    Me.grProp.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grProp.EmbeddedNavigator.Name = ""
    Me.grProp.Location = New System.Drawing.Point(0, 0)
    Me.grProp.MainView = Me.grvProp
    Me.grProp.Name = "grProp"
    Me.grProp.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
    Me.grProp.Size = New System.Drawing.Size(420, 270)
    Me.grProp.TabIndex = 8
    Me.grProp.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvProp})
    '
    'grvProp
    '
    Me.grvProp.ActiveFilterEnabled = False
    Me.grvProp.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.rp_nomprop, Me.rp_valprop, Me.rp_idut, Me.rp_idaz, Me.rp_idtdoc, Me.rp_liv1, Me.rp_liv2, Me.rp_liv3, Me.xx_seleziona})
    Me.grvProp.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvProp.Enabled = True
    Me.grvProp.GridControl = Me.grProp
    Me.grvProp.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvProp.Name = "grvProp"
    Me.grvProp.NTSAllowDelete = True
    Me.grvProp.NTSAllowInsert = True
    Me.grvProp.NTSAllowUpdate = True
    Me.grvProp.NTSMenuContext = Nothing
    Me.grvProp.OptionsCustomization.AllowRowSizing = True
    Me.grvProp.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvProp.OptionsNavigation.UseTabKey = False
    Me.grvProp.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvProp.OptionsView.ColumnAutoWidth = False
    Me.grvProp.OptionsView.EnableAppearanceEvenRow = True
    Me.grvProp.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvProp.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvProp.OptionsView.ShowGroupPanel = False
    Me.grvProp.RowHeight = 14
    '
    'rp_nomprop
    '
    Me.rp_nomprop.AppearanceCell.Options.UseBackColor = True
    Me.rp_nomprop.AppearanceCell.Options.UseTextOptions = True
    Me.rp_nomprop.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.rp_nomprop.Caption = "Proprietà"
    Me.rp_nomprop.Enabled = True
    Me.rp_nomprop.FieldName = "rp_nomprop"
    Me.rp_nomprop.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.rp_nomprop.Name = "rp_nomprop"
    Me.rp_nomprop.NTSRepositoryComboBox = Nothing
    Me.rp_nomprop.NTSRepositoryItemCheck = Nothing
    Me.rp_nomprop.NTSRepositoryItemMemo = Nothing
    Me.rp_nomprop.NTSRepositoryItemText = Nothing
    Me.rp_nomprop.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.rp_nomprop.OptionsFilter.AllowFilter = False
    Me.rp_nomprop.Visible = True
    Me.rp_nomprop.VisibleIndex = 0
    '
    'rp_valprop
    '
    Me.rp_valprop.AppearanceCell.Options.UseBackColor = True
    Me.rp_valprop.AppearanceCell.Options.UseTextOptions = True
    Me.rp_valprop.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.rp_valprop.Caption = "Valore"
    Me.rp_valprop.Enabled = True
    Me.rp_valprop.FieldName = "rp_valprop"
    Me.rp_valprop.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.rp_valprop.Name = "rp_valprop"
    Me.rp_valprop.NTSRepositoryComboBox = Nothing
    Me.rp_valprop.NTSRepositoryItemCheck = Nothing
    Me.rp_valprop.NTSRepositoryItemMemo = Nothing
    Me.rp_valprop.NTSRepositoryItemText = Nothing
    Me.rp_valprop.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.rp_valprop.OptionsFilter.AllowFilter = False
    Me.rp_valprop.Visible = True
    Me.rp_valprop.VisibleIndex = 1
    '
    'rp_idut
    '
    Me.rp_idut.AppearanceCell.Options.UseBackColor = True
    Me.rp_idut.AppearanceCell.Options.UseTextOptions = True
    Me.rp_idut.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.rp_idut.Caption = "Ident. Utente"
    Me.rp_idut.Enabled = True
    Me.rp_idut.FieldName = "rp_idut"
    Me.rp_idut.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.rp_idut.Name = "rp_idut"
    Me.rp_idut.NTSRepositoryComboBox = Nothing
    Me.rp_idut.NTSRepositoryItemCheck = Nothing
    Me.rp_idut.NTSRepositoryItemMemo = Nothing
    Me.rp_idut.NTSRepositoryItemText = Nothing
    Me.rp_idut.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.rp_idut.OptionsFilter.AllowFilter = False
    Me.rp_idut.Visible = True
    Me.rp_idut.VisibleIndex = 2
    '
    'rp_idaz
    '
    Me.rp_idaz.AppearanceCell.Options.UseBackColor = True
    Me.rp_idaz.AppearanceCell.Options.UseTextOptions = True
    Me.rp_idaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.rp_idaz.Caption = "Ident. Ditta"
    Me.rp_idaz.Enabled = True
    Me.rp_idaz.FieldName = "rp_idaz"
    Me.rp_idaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.rp_idaz.Name = "rp_idaz"
    Me.rp_idaz.NTSRepositoryComboBox = Nothing
    Me.rp_idaz.NTSRepositoryItemCheck = Nothing
    Me.rp_idaz.NTSRepositoryItemMemo = Nothing
    Me.rp_idaz.NTSRepositoryItemText = Nothing
    Me.rp_idaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.rp_idaz.OptionsFilter.AllowFilter = False
    Me.rp_idaz.Visible = True
    Me.rp_idaz.VisibleIndex = 3
    '
    'rp_idtdoc
    '
    Me.rp_idtdoc.AppearanceCell.Options.UseBackColor = True
    Me.rp_idtdoc.AppearanceCell.Options.UseTextOptions = True
    Me.rp_idtdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.rp_idtdoc.Caption = "Ident. Doc."
    Me.rp_idtdoc.Enabled = True
    Me.rp_idtdoc.FieldName = "rp_idtdoc"
    Me.rp_idtdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.rp_idtdoc.Name = "rp_idtdoc"
    Me.rp_idtdoc.NTSRepositoryComboBox = Nothing
    Me.rp_idtdoc.NTSRepositoryItemCheck = Nothing
    Me.rp_idtdoc.NTSRepositoryItemMemo = Nothing
    Me.rp_idtdoc.NTSRepositoryItemText = Nothing
    Me.rp_idtdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.rp_idtdoc.OptionsFilter.AllowFilter = False
    Me.rp_idtdoc.Visible = True
    Me.rp_idtdoc.VisibleIndex = 4
    '
    'rp_liv1
    '
    Me.rp_liv1.AppearanceCell.Options.UseBackColor = True
    Me.rp_liv1.AppearanceCell.Options.UseTextOptions = True
    Me.rp_liv1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.rp_liv1.Caption = "rp_liv1"
    Me.rp_liv1.Enabled = True
    Me.rp_liv1.FieldName = "rp_liv1"
    Me.rp_liv1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.rp_liv1.Name = "rp_liv1"
    Me.rp_liv1.NTSRepositoryComboBox = Nothing
    Me.rp_liv1.NTSRepositoryItemCheck = Nothing
    Me.rp_liv1.NTSRepositoryItemMemo = Nothing
    Me.rp_liv1.NTSRepositoryItemText = Nothing
    Me.rp_liv1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.rp_liv1.OptionsFilter.AllowFilter = False
    Me.rp_liv1.Visible = True
    Me.rp_liv1.VisibleIndex = 5
    '
    'rp_liv2
    '
    Me.rp_liv2.AppearanceCell.Options.UseBackColor = True
    Me.rp_liv2.AppearanceCell.Options.UseTextOptions = True
    Me.rp_liv2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.rp_liv2.Caption = "rp_liv2"
    Me.rp_liv2.Enabled = True
    Me.rp_liv2.FieldName = "rp_liv2"
    Me.rp_liv2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.rp_liv2.Name = "rp_liv2"
    Me.rp_liv2.NTSRepositoryComboBox = Nothing
    Me.rp_liv2.NTSRepositoryItemCheck = Nothing
    Me.rp_liv2.NTSRepositoryItemMemo = Nothing
    Me.rp_liv2.NTSRepositoryItemText = Nothing
    Me.rp_liv2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.rp_liv2.OptionsFilter.AllowFilter = False
    Me.rp_liv2.Visible = True
    Me.rp_liv2.VisibleIndex = 6
    '
    'rp_liv3
    '
    Me.rp_liv3.AppearanceCell.Options.UseBackColor = True
    Me.rp_liv3.AppearanceCell.Options.UseTextOptions = True
    Me.rp_liv3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.rp_liv3.Caption = "rp_liv3"
    Me.rp_liv3.Enabled = True
    Me.rp_liv3.FieldName = "rp_liv3"
    Me.rp_liv3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.rp_liv3.Name = "rp_liv3"
    Me.rp_liv3.NTSRepositoryComboBox = Nothing
    Me.rp_liv3.NTSRepositoryItemCheck = Nothing
    Me.rp_liv3.NTSRepositoryItemMemo = Nothing
    Me.rp_liv3.NTSRepositoryItemText = Nothing
    Me.rp_liv3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.rp_liv3.OptionsFilter.AllowFilter = False
    Me.rp_liv3.Visible = True
    Me.rp_liv3.VisibleIndex = 7
    '
    'xx_seleziona
    '
    Me.xx_seleziona.AppearanceCell.Options.UseBackColor = True
    Me.xx_seleziona.AppearanceCell.Options.UseTextOptions = True
    Me.xx_seleziona.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_seleziona.Caption = "Seleziona"
    Me.xx_seleziona.ColumnEdit = Me.RepositoryItemCheckEdit1
    Me.xx_seleziona.Enabled = True
    Me.xx_seleziona.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_seleziona.Name = "xx_seleziona"
    Me.xx_seleziona.NTSRepositoryComboBox = Nothing
    Me.xx_seleziona.NTSRepositoryItemCheck = Nothing
    Me.xx_seleziona.NTSRepositoryItemMemo = Nothing
    Me.xx_seleziona.NTSRepositoryItemText = Nothing
    Me.xx_seleziona.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_seleziona.OptionsFilter.AllowFilter = False
    Me.xx_seleziona.Visible = True
    Me.xx_seleziona.VisibleIndex = 8
    '
    'RepositoryItemCheckEdit1
    '
    Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.lbPath)
    Me.pnTop.Controls.Add(Me.cmdFiltri)
    Me.pnTop.Controls.Add(Me.fmFiltri)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(281, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.Size = New System.Drawing.Size(420, 162)
    Me.pnTop.TabIndex = 12
    Me.pnTop.Text = "NtsPanel2"
    '
    'lbPath
    '
    Me.lbPath.BackColor = System.Drawing.Color.Transparent
    Me.lbPath.Location = New System.Drawing.Point(12, 12)
    Me.lbPath.Name = "lbPath"
    Me.lbPath.NTSDbField = ""
    Me.lbPath.Size = New System.Drawing.Size(297, 18)
    Me.lbPath.TabIndex = 13
    Me.lbPath.Tooltip = ""
    Me.lbPath.UseMnemonic = False
    '
    'cmdFiltri
    '
    Me.cmdFiltri.ImageText = ""
    Me.cmdFiltri.Location = New System.Drawing.Point(315, 6)
    Me.cmdFiltri.Name = "cmdFiltri"
    Me.cmdFiltri.Size = New System.Drawing.Size(80, 24)
    Me.cmdFiltri.TabIndex = 12
    Me.cmdFiltri.Text = "Cambia Filtri"
    '
    'fmFiltri
    '
    Me.fmFiltri.AllowDrop = True
    Me.fmFiltri.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmFiltri.Appearance.Options.UseBackColor = True
    Me.fmFiltri.Controls.Add(Me.pnFiltriLeft)
    Me.fmFiltri.Controls.Add(Me.edCode2)
    Me.fmFiltri.Controls.Add(Me.edCode1)
    Me.fmFiltri.Controls.Add(Me.edCode0)
    Me.fmFiltri.Controls.Add(Me.lbDoc)
    Me.fmFiltri.Controls.Add(Me.lbAzienda)
    Me.fmFiltri.Controls.Add(Me.lbUtente)
    Me.fmFiltri.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmFiltri.Location = New System.Drawing.Point(15, 38)
    Me.fmFiltri.Name = "fmFiltri"
    Me.fmFiltri.Size = New System.Drawing.Size(380, 120)
    Me.fmFiltri.TabIndex = 11
    Me.fmFiltri.Text = "Filtri"
    '
    'pnFiltriLeft
    '
    Me.pnFiltriLeft.AllowDrop = True
    Me.pnFiltriLeft.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnFiltriLeft.Appearance.Options.UseBackColor = True
    Me.pnFiltriLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnFiltriLeft.Controls.Add(Me.lbDip)
    Me.pnFiltriLeft.Controls.Add(Me.ckDip2)
    Me.pnFiltriLeft.Controls.Add(Me.ckDip1)
    Me.pnFiltriLeft.Controls.Add(Me.ckDip0)
    Me.pnFiltriLeft.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnFiltriLeft.Location = New System.Drawing.Point(5, 20)
    Me.pnFiltriLeft.Name = "pnFiltriLeft"
    Me.pnFiltriLeft.Size = New System.Drawing.Size(124, 98)
    Me.pnFiltriLeft.TabIndex = 10
    Me.pnFiltriLeft.Text = "NtsPanel1"
    '
    'lbDip
    '
    Me.lbDip.AutoSize = True
    Me.lbDip.BackColor = System.Drawing.Color.Transparent
    Me.lbDip.Location = New System.Drawing.Point(6, 4)
    Me.lbDip.Name = "lbDip"
    Me.lbDip.NTSDbField = ""
    Me.lbDip.Size = New System.Drawing.Size(67, 13)
    Me.lbDip.TabIndex = 7
    Me.lbDip.Text = "Dipendenze:"
    Me.lbDip.Tooltip = ""
    Me.lbDip.UseMnemonic = False
    '
    'ckDip2
    '
    Me.ckDip2.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDip2.Location = New System.Drawing.Point(9, 72)
    Me.ckDip2.Name = "ckDip2"
    Me.ckDip2.NTSCheckValue = "S"
    Me.ckDip2.NTSUnCheckValue = "N"
    Me.ckDip2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDip2.Properties.Appearance.Options.UseBackColor = True
    Me.ckDip2.Properties.Caption = "Tipo documento"
    Me.ckDip2.Size = New System.Drawing.Size(109, 19)
    Me.ckDip2.TabIndex = 6
    '
    'ckDip1
    '
    Me.ckDip1.Cursor = System.Windows.Forms.Cursors.Hand
    Me.ckDip1.Location = New System.Drawing.Point(9, 46)
    Me.ckDip1.Name = "ckDip1"
    Me.ckDip1.NTSCheckValue = "S"
    Me.ckDip1.NTSUnCheckValue = "N"
    Me.ckDip1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDip1.Properties.Appearance.Options.UseBackColor = True
    Me.ckDip1.Properties.Caption = "Ditta"
    Me.ckDip1.Size = New System.Drawing.Size(109, 19)
    Me.ckDip1.TabIndex = 5
    '
    'ckDip0
    '
    Me.ckDip0.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDip0.Location = New System.Drawing.Point(9, 20)
    Me.ckDip0.Name = "ckDip0"
    Me.ckDip0.NTSCheckValue = "S"
    Me.ckDip0.NTSUnCheckValue = "N"
    Me.ckDip0.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDip0.Properties.Appearance.Options.UseBackColor = True
    Me.ckDip0.Properties.Caption = "Operatore"
    Me.ckDip0.Size = New System.Drawing.Size(109, 19)
    Me.ckDip0.TabIndex = 4
    '
    'edCode2
    '
    Me.edCode2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCode2.Location = New System.Drawing.Point(216, 91)
    Me.edCode2.Name = "edCode2"
    Me.edCode2.NTSDbField = ""
    Me.edCode2.NTSForzaVisZoom = False
    Me.edCode2.NTSOldValue = ""
    Me.edCode2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCode2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCode2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCode2.Properties.MaxLength = 65536
    Me.edCode2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCode2.Size = New System.Drawing.Size(159, 20)
    Me.edCode2.TabIndex = 9
    '
    'edCode1
    '
    Me.edCode1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCode1.Location = New System.Drawing.Point(216, 65)
    Me.edCode1.Name = "edCode1"
    Me.edCode1.NTSDbField = ""
    Me.edCode1.NTSForzaVisZoom = False
    Me.edCode1.NTSOldValue = ""
    Me.edCode1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCode1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCode1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCode1.Properties.MaxLength = 65536
    Me.edCode1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCode1.Size = New System.Drawing.Size(159, 20)
    Me.edCode1.TabIndex = 8
    '
    'edCode0
    '
    Me.edCode0.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCode0.Location = New System.Drawing.Point(216, 39)
    Me.edCode0.Name = "edCode0"
    Me.edCode0.NTSDbField = ""
    Me.edCode0.NTSForzaVisZoom = False
    Me.edCode0.NTSOldValue = ""
    Me.edCode0.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCode0.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCode0.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCode0.Properties.MaxLength = 65536
    Me.edCode0.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCode0.Size = New System.Drawing.Size(159, 20)
    Me.edCode0.TabIndex = 7
    '
    'lbDoc
    '
    Me.lbDoc.AutoSize = True
    Me.lbDoc.BackColor = System.Drawing.Color.Transparent
    Me.lbDoc.Location = New System.Drawing.Point(129, 94)
    Me.lbDoc.Name = "lbDoc"
    Me.lbDoc.NTSDbField = ""
    Me.lbDoc.Size = New System.Drawing.Size(88, 13)
    Me.lbDoc.TabIndex = 6
    Me.lbDoc.Text = "Tipo Documento:"
    Me.lbDoc.Tooltip = ""
    Me.lbDoc.UseMnemonic = False
    '
    'lbAzienda
    '
    Me.lbAzienda.AutoSize = True
    Me.lbAzienda.BackColor = System.Drawing.Color.Transparent
    Me.lbAzienda.Location = New System.Drawing.Point(129, 68)
    Me.lbAzienda.Name = "lbAzienda"
    Me.lbAzienda.NTSDbField = ""
    Me.lbAzienda.Size = New System.Drawing.Size(69, 13)
    Me.lbAzienda.TabIndex = 5
    Me.lbAzienda.Text = "Codice Ditta:"
    Me.lbAzienda.Tooltip = ""
    Me.lbAzienda.UseMnemonic = False
    '
    'lbUtente
    '
    Me.lbUtente.AutoSize = True
    Me.lbUtente.BackColor = System.Drawing.Color.Transparent
    Me.lbUtente.Location = New System.Drawing.Point(129, 42)
    Me.lbUtente.Name = "lbUtente"
    Me.lbUtente.NTSDbField = ""
    Me.lbUtente.Size = New System.Drawing.Size(89, 13)
    Me.lbUtente.TabIndex = 4
    Me.lbUtente.Text = "Nome operatore:"
    Me.lbUtente.Tooltip = ""
    Me.lbUtente.UseMnemonic = False
    '
    'pnLeft
    '
    Me.pnLeft.AllowDrop = True
    Me.pnLeft.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnLeft.Appearance.Options.UseBackColor = True
    Me.pnLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnLeft.Controls.Add(Me.trGreg)
    Me.pnLeft.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnLeft.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnLeft.Location = New System.Drawing.Point(0, 0)
    Me.pnLeft.Name = "pnLeft"
    Me.pnLeft.Size = New System.Drawing.Size(281, 432)
    Me.pnLeft.TabIndex = 11
    Me.pnLeft.Text = "NtsPanel1"
    '
    'trGreg
    '
    Me.trGreg.Dock = System.Windows.Forms.DockStyle.Fill
    Me.trGreg.Location = New System.Drawing.Point(0, 0)
    Me.trGreg.Name = "trGreg"
    Me.trGreg.Size = New System.Drawing.Size(281, 432)
    Me.trGreg.TabIndex = 7
    '
    'ImageList1
    '
    Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
    Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
    Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
    '
    'FRM__GREG
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(701, 462)
    Me.Controls.Add(Me.pnGreg)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRM__GREG"
    Me.NTSLastControlFocussed = Me.grProp
    Me.Text = "REGISTRO"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnGreg, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGreg.ResumeLayout(False)
    CType(Me.pnFill, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFill.ResumeLayout(False)
    CType(Me.grProp, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvProp, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    CType(Me.fmFiltri, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmFiltri.ResumeLayout(False)
    Me.fmFiltri.PerformLayout()
    CType(Me.pnFiltriLeft, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFiltriLeft.ResumeLayout(False)
    Me.pnFiltriLeft.PerformLayout()
    CType(Me.ckDip2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDip1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDip0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCode2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCode1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCode0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnLeft, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnLeft.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRecordNuovo.GlyphPath = (oApp.ChildImageDir & "\recnew.gif")
        tlbRecordSalva.GlyphPath = (oApp.ChildImageDir & "\recagg.gif")
        tlbRecordRipristina.GlyphPath = (oApp.ChildImageDir & "\recrestore.gif")
        tlbRecordCancella.GlyphPath = (oApp.ChildImageDir & "\recdelete.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()
      '-------------------------------------------------
      grvProp.NTSSetParam(oMenu, oApp.Tr(Me, 128330289790800518, "Proprietà"))
      rp_nomprop.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128330289815488018, "Proprietà"), 0, False)
      rp_valprop.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128330289839081768, "Valore"), 0, True, True)
      rp_idut.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128330289856113018, "Ident. Utente"), 0, True)
      rp_idaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128330289875019268, "Ident. Ditta"), 0, True)
      rp_idtdoc.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128330289893925518, "Ident. Doc."), 0, True)
      rp_liv1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128330289912831768, "rp_liv1"), 0, True)
      rp_liv2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128330289975800518, "rp_liv2"), 0, True)
      rp_liv3.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128330290007831768, "rp_liv3"), 0, True)
      xx_seleziona.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128338841266804758, "Seleziona"), "S", "N")

      rp_idut.NTSSetParamZoom("ZOOMOPERAT")
      rp_idaz.NTSSetParamZoom("ZOOMTABANAZ")


      ckDip2.NTSSetParam(oMenu, oApp.Tr(Me, 128340791752043036, "Tipo documento"), "S", "N")
      ckDip1.NTSSetParam(oMenu, oApp.Tr(Me, 128340791752199036, "Ditta"), "S", "N")
      ckDip0.NTSSetParam(oMenu, oApp.Tr(Me, 129024310285819046, "Operatore"), "S", "N")
      edCode2.NTSSetParam(oMenu, oApp.Tr(Me, 129024310331445214, "Dipendenza documento"), 50)
      edCode1.NTSSetParam(oMenu, oApp.Tr(Me, 128340791752511036, "Dipendenza azienda"), 50)
      edCode0.NTSSetParam(oMenu, oApp.Tr(Me, 128340791752667036, "Dipendenza operatore"), 50)


      'non faccio lanciare lo zoom dal controllo visto che non devo cambiare il contenuto del campo ma riposizionare il dataset!!!
      rp_nomprop.NTSSetRichiesto()

      rp_nomprop.NTSSetParamZoom("ZOOMNOMPROP")
      rp_valprop.NTSSetParamZoom("ZOOMVALPROP")

      trGreg.ShowPlusMinus = False
      edCode0.Enabled = False
      edCode1.Enabled = False
      edCode2.Enabled = False

      ImageList1.Images.Add("", Bitmap.FromFile(oApp.ChildImageDir & "\open_treeview.gif"))
      ImageList1.Images.Add("", Bitmap.FromFile(oApp.ChildImageDir & "\open_treeviewsel.gif"))
      trGreg.ImageList = ImageList1
      trGreg.ImageIndex = 0
      trGreg.SelectedImageIndex = 1

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
  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano già stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 


      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcGreg, Me)

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
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__GREG", "BE__GREG", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128328672233059763, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleGreg = CType(oTmp, CLE__GREG)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__GREG", strRemoteServer, strRemotePort)
    AddHandler oCleGreg.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleGreg.Init(oApp, NTSScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    '------------------------------------------------
    If oCallParams Is Nothing Then
      oCleGreg.strChild = ""
    Else
      oCleGreg.strChild = oCallParams.strPar1
    End If

    Return True
  End Function

#Region "EventiForm"

  Public Overridable Sub FRM__GREG_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Dim bOut As Boolean
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      bOut = Apri()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      If Not CLN__STD.UserIsAdmin(oApp.User.Gruppo) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128965530351621094, "L'utente non è abilitato all'utilizzo di questro programma non essendo un amministratore"))
        Me.Close()
        Return
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__GREG_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      Salva()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub FRM__GREG_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      If Not oCallParams Is Nothing Then
        If oCallParams.strPar1 <> "" Then ClickNodo(trGreg.Nodes(0))
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub trGreg_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles trGreg.GotFocus
    Try
      Stato(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub trGreg_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles trGreg.KeyUp
    Dim strNewString As String
    Try
      If e.KeyCode = Keys.Delete Then Return 'disabilito uso tasto canc (se no da errore con registro vuoto creando prima cartella)
      strNewString = Trim(trGreg.SelectedNode.Text)
      If Len(strNewString) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128329573345319733, "Digitare un nome valido"))
        trGreg.Nodes.Remove(nodReg)
        NuovaCartella()
        Exit Sub
      End If
      If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Or e.KeyCode = Keys.Left Or e.KeyCode = Keys.Right Then
        FileApri(trGreg.SelectedNode)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub trGreg_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles trGreg.LostFocus
    Try
      If Len(Trim(nodReg.Text)) = 0 Then
        trGreg.Nodes.Remove(nodReg)
        nodReg = trGreg.SelectedNode
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub trGreg_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trGreg.NodeMouseClick
    Try
      ClickNodo(e.Node)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdFiltri_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFiltri.Click
    Dim nI As Integer
    Dim oParam As New CLE__CLDP
    Dim fmDipe As FRM__DIPE = Nothing
    Try
      bLoadTree = True
      bLoadTree = False
      fmDipe = CType(NTSNewFormModal("FRM__DIPE"), FRM__DIPE)
      fmDipe.Init(oMenu, oParam, DittaCorrente)
      fmDipe.InitEntity(oCleGreg)
      fmDipe.ShowDialog()
      edCode0.Text = oCleGreg.strGregNomOp
      edCode1.Text = oCleGreg.strGregCodAz
      edCode2.Text = oCleGreg.strGregTipoDoc
      For nI = 0 To 2
        If Trim(GetEdcode(nI)) = "*" Then
          SetckDipEnab(nI, False)
        Else
          If Len(Trim(GetEdcode(nI))) = 0 Then
            SetEdcode(nI, " ")
          End If
          SetckDipEnab(nI, True)
        End If
      Next
      FileApri(trGreg.SelectedNode)
      trGreg.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not fmDipe Is Nothing Then fmDipe.Dispose()
      fmDipe = Nothing
    End Try
  End Sub

  Public Overridable Sub ckDip_Click()
    Try
      If bLoadTree = False Then
        FileAggiorna()
      End If
      If Me.Visible Then
        trGreg.Focus()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckDip0_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckDip0.CheckedChanged
    Try
      ckDip_Click()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckDip1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckDip1.CheckedChanged
    Try
      ckDip_Click()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckDip2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckDip2.CheckedChanged
    Try
      ckDip_Click()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

#Region "EventiToolbar"

  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      NuovaCartella()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Try
      CancellaCartella()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim strCampi(0 To 2) As String
    Dim strPathCor As String
    Dim ctrlTmp As Control = Nothing
    Dim oParam As New CLE__PATB
    Try
      strPathCor = trGreg.SelectedNode.FullPath
      oCleGreg.EstraiDaPath(strPathCor, strCampi)

      '------------------------------------------------
      'questa funzione viene sempre scatenata quando premo F5 o ALT+F2 o ALT+F3 sulla griglia
      'zoom standard
      ctrlTmp = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      If tlbZoom.Enabled Then
        If trGreg.ContainsFocus Then
          treGreg_NodeClick(trGreg.SelectedNode)
          treGreg_NodeClick(trGreg.SelectedNode)
        ElseIf ctrlTmp.GetType.ToString.IndexOf("NTSGrid") > -1 Then
          SetFastZoom(NTSCStr(CType(CType(ctrlTmp, NTSGrid).DefaultView, NTSGridView).EditingValue), oParam)

          If grProp.Focused And (grvProp.FocusedColumn.Name.ToLower = "rp_nomprop" Or grvProp.FocusedColumn.Name.ToLower = "rp_valprop") Then
            If grProp.Focused And grvProp.FocusedColumn.Name.ToLower = "rp_nomprop" Then
              RemappaggioLivelli(strCampi)
              If bEsciDaZoom = True Then Return

              NTSZOOM.strIn = NTSCStr(grvProp.EditingValue)
              oParam.strTipo = strCampi(0) & "|" & strCampi(1) & "|" & strCampi(2)
              NTSZOOM.ZoomStrIn("ZOOMNOMPROP", DittaCorrente, oParam)
              If NTSZOOM.strIn <> NTSCStr(grvProp.EditingValue) Then grvProp.SetFocusedValue(NTSZOOM.strIn)
              grProp.Focus()
              Return
            ElseIf grProp.Focused And grvProp.FocusedColumn.Name.ToLower = "rp_valprop" Then
              If dcGreg.Position > -1 Then
                RemappaggioLivelli(strCampi)
                If bEsciDaZoom = True Then Return

                NTSZOOM.strIn = NTSCStr(grvProp.EditingValue)
                oParam.strTipo = strCampi(0) & "|" & strCampi(1) & "|" & strCampi(2) & "|" & NTSCStr(dsGreg.Tables("REGPROP").Rows(dcGreg.Position)!rp_nomprop)
                NTSZOOM.ZoomStrIn("ZOOMVALPROP", DittaCorrente, oParam)
                If NTSZOOM.strIn <> NTSCStr(grvProp.EditingValue) Then grvProp.SetFocusedValue(NTSZOOM.strIn)
                grProp.Focus()
                Return
              End If
            End If
          Else
            '------------------------------------
            'zoom standard di textbox e griglia
            NTSCallStandardZoom()
          End If
        Else
          '------------------------------------
          'zoom standard di textbox e griglia
          NTSCallStandardZoom()
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      bEsciDaZoom = False
    End Try
  End Sub

  Public Overridable Sub tlbRecordNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordNuovo.ItemClick
    Try
      grvProp.NTSNuovo()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRecordSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordSalva.ItemClick
    Try
      Salva()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRecordRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordRipristina.ItemClick
    Try
      If Not grvProp.NTSRipristinaRigaCorrenteBefore(dcGreg, True) Then Return
      oCleGreg.Ripristina(dcGreg.Position, dcGreg.Filter)
      grvProp.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbRecordCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordCancella.ItemClick
    Dim strLiv1 As String
    Dim strLiv2 As String
    Dim strliv3 As String
    Dim strIdut As String
    Dim strIdaz As String
    Dim strIdtdoc As String
    Dim strNomprop As String
    Try
      If dcGreg.Position < 0 Then Exit Sub
      strLiv1 = NTSCStr(dsGreg.Tables("REGPROP").Rows(dcGreg.Position)!rp_liv1)
      strLiv2 = NTSCStr(dsGreg.Tables("REGPROP").Rows(dcGreg.Position)!rp_liv2)
      strliv3 = NTSCStr(dsGreg.Tables("REGPROP").Rows(dcGreg.Position)!rp_liv3)
      strIdut = NTSCStr(dsGreg.Tables("REGPROP").Rows(dcGreg.Position)!rp_idut)
      strIdaz = NTSCStr(dsGreg.Tables("REGPROP").Rows(dcGreg.Position)!rp_idaz)
      strIdtdoc = NTSCStr(dsGreg.Tables("REGPROP").Rows(dcGreg.Position)!rp_idtdoc)
      strNomprop = NTSCStr(dsGreg.Tables("REGPROP").Rows(dcGreg.Position)!rp_nomprop)

      If Not grvProp.NTSDeleteRigaCorrente(dcGreg, True) Then Return
      SalvaDipendenze()
      oCleGreg.LogDelete(strLiv1, strLiv2, strliv3, strIdut, strIdaz, strIdtdoc, strNomprop)
      oCleGreg.Salva(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbModificaRbs_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbModificaRbs.ItemClick
    'Dim oParam As New CLE__CLDP
    'Dim fmEdRBS As FRM__EDRBS = Nothing
    Try
      'fmEdRBS = CType(NTSNewFormModal("FRM__EDRBS"), FRM__EDRBS)
      'fmEdRBS.Init(oMenu, oParam, DittaCorrente)
      'fmEdRBS.InitEntity(oCleGreg)
      'fmEdRBS.ShowDialog()
      Shell("notepad.exe", AppWinStyle.NormalFocus)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbControlloIntegrità_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbControlloIntegrità.ItemClick
    Dim oParam As New CLE__CLDP
    Dim fmGrck As FRM__GRCK = Nothing
    Try
      fmGrck = CType(NTSNewFormModal("FRM__GRCK"), FRM__GRCK)
      fmGrck.Init(oMenu, oParam, DittaCorrente)
      fmGrck.InitEntity(oCleGreg)
      fmGrck.ShowDialog()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not fmGrck Is Nothing Then fmGrck.Dispose()
      fmGrck = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbRinominaCartella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRinominaCartella.ItemClick
    Try
      BeforeRinominaCartella(False)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbEsportaRamo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsportaRamo.ItemClick
    Dim strTMPPath As String
    Dim strLivelli(0 To 2) As String
    Dim oParam As New CLE__CLDP
    Dim fmImex As FRM__IMEX = Nothing
    Try
      oCleGreg.bEsporta = True
      oCleGreg.strNomeUt = edCode0.Text
      oCleGreg.strNomeAz = edCode1.Text
      strTMPPath = trGreg.SelectedNode.FullPath
      oCleGreg.strLbPath = strTMPPath
      oCleGreg.EstraiDaPath(strTMPPath, strLivelli)
      oCleGreg.strCaption = "Esporta ramo di registro"
      oCleGreg.strLiv = strLivelli

      fmImex = CType(NTSNewFormModal("FRM__IMEX"), FRM__IMEX)
      fmImex.Init(oMenu, oParam, DittaCorrente)
      fmImex.InitEntity(oCleGreg)
      fmImex.ShowDialog()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not fmImex Is Nothing Then fmImex.Dispose()
      fmImex = Nothing
    End Try
  End Sub
  Public Overridable Sub tlbImportaRamo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImportaRamo.ItemClick
    Dim strTMPPath As String
    Dim oParam As New CLE__CLDP
    Dim fmImex As FRM__IMEX = Nothing
    Try
      oCleGreg.bEsporta = False
      oCleGreg.strNomeUt = edCode0.Text
      oCleGreg.strNomeAz = edCode1.Text
      strTMPPath = trGreg.SelectedNode.FullPath
      oCleGreg.strLbPath = strTMPPath
      oCleGreg.strCaption = "Importa ramo di registro"

      fmImex = CType(NTSNewFormModal("FRM__IMEX"), FRM__IMEX)
      fmImex.Init(oMenu, oParam, DittaCorrente)
      fmImex.InitEntity(oCleGreg)
      fmImex.ShowDialog()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not fmImex Is Nothing Then fmImex.Dispose()
      fmImex = Nothing
    End Try
  End Sub
  Public Overridable Sub tlbCopiaCartella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCopiaCartella.ItemClick
    Dim strFuz(0 To 2) As String
    Dim strCopy(0 To 2) As String
    Try
      If Trim(edCode0.Text) = "*" Or Trim(edCode1.Text) = "*" Or Trim(edCode2.Text) = "*" Then
        Exit Sub
      End If
      strCopyKey = trGreg.SelectedNode.FullPath
      oCleGreg.EstraiDaPath(strCopyKey, strCopy)
      nLivDa = 3
      If strCopy(2) = "." Then
        nLivDa = 2
        If strCopy(1) = "." Then
          nLivDa = 1
          If strCopy(0) = "." Then
            nLivDa = 0
          End If
        End If
      End If

      oCleGreg.CopiaCartella(nLivDa, strCopy, dsCopia)

      strCopyNomOp = oCleGreg.strGregNomOp
      strCopyCodAz = oCleGreg.strGregCodAz
      strCopyTipDoc = oCleGreg.strGregTipoDoc

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbIncollaCartella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbIncollaCartella.ItemClick
    Dim strCartPrec As String
    Dim strCartCor As String
    Dim strDip(0 To 2) As String
    Dim strCur(0 To 2) As String
    Dim newDip(0 To 2) As String
    Dim nI As Integer
    Dim nLivA As Integer
    Dim nIndex As Integer
    Dim bRiscrProp As Boolean
    Dim bConferma As Boolean
    Dim i As Integer
    Dim j As Integer
    Dim dsRegdir As DataSet = Nothing
    Dim dsRegprop As DataSet = Nothing
    Dim strRd_diput As String
    Dim strRd_dipaz As String
    Dim strRd_diptdoc As String
    Dim strRd_liv1 As String
    Dim strRd_liv2 As String
    Dim strRd_liv3 As String
    Dim strRp_nomprop As String
    Dim strRp_valprop As String
    Dim bRet As Boolean
    Dim lStartIndex As Integer = 0
    Try
      If Trim(edCode0.Text) = "*" Or Trim(edCode1.Text) = "*" Or Trim(edCode2.Text) = "*" Then
        Exit Sub
      End If
      oCleGreg.EstraiDaPath(trGreg.SelectedNode.FullPath, strCur)
      If dsCopia Is Nothing Then Exit Sub
      If nLivDa = 0 Then
        nLivDa = 1
        lStartIndex = 1
      End If
      nLivA = 3
      If strCur(2) = "." Then
        nLivA = 2
        If strCur(1) = "." Then
          nLivA = 1
          If strCur(0) = "." Then
            nLivA = 0
          End If
        End If
      End If
      If Len(Trim(strCopyKey)) = 0 Then
        Exit Sub
      End If
      strCartCor = ""
      strCartPrec = ""
      bRiscrProp = False
      bConferma = True
      For i = lStartIndex To dsCopia.Tables("REGDIR").Rows.Count - 1
        nIndex = nLivDa - 1
        For nI = nLivA + 1 To 3
          strCur(nI - 1) = NTSCStr(dsCopia.Tables("REGDIR").Rows(i)(nIndex))
          nIndex = nIndex + 1
          If nIndex = 3 Then Exit For
        Next
        strCartCor = strCur(0) & "\" & strCur(1) & "\" & strCur(2)

        oCleGreg.IncollaCartellaSelectRegdir(strCur, dsRegdir)

        If dsRegdir.Tables("REGDIR").Rows.Count > 0 Then
          If NTSCStr(dsRegdir.Tables("REGDIR").Rows(0)!rd_diput) = "S" Then
            newDip(0) = oCleGreg.strGregNomOp
          Else
            newDip(0) = " "
          End If
          If NTSCStr(dsRegdir.Tables("REGDIR").Rows(0)!rd_dipaz) = "S" Then
            newDip(1) = oCleGreg.strGregCodAz
          Else
            newDip(1) = " "
          End If
          If NTSCStr(dsRegdir.Tables("REGDIR").Rows(0)!rd_diptdoc) = "S" Then
            newDip(2) = oCleGreg.strGregTipoDoc
          Else
            newDip(2) = " "
          End If
          If bConferma Then
            If strCartCor <> strCartPrec Then
              strCartPrec = strCur(0) & "\" & strCur(1) & "\" & strCur(2)
              If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129024310469261242, "La cartella '|BUSINESS\" & strCur(0) & "\" & _
               strCur(1) & "\" & strCur(2) & "|' è già esistente, riscrivere le proprietà eventualmente esistenti?")) = Windows.Forms.DialogResult.Yes Then
                bRiscrProp = True
              End If
              If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129024310543794400, "Continuare a chiedere la conferma per le ulteriori cartelle già esistenti?")) = Windows.Forms.DialogResult.Yes Then
                bConferma = True
              Else
                bConferma = False
              End If
            End If
          End If
          GoTo REGPROP
        End If

        strRd_diput = NTSCStr(dsCopia.Tables("REGDIR").Rows(i)!rd_diput)
        strRd_dipaz = NTSCStr(dsCopia.Tables("REGDIR").Rows(i)!rd_dipaz)
        strRd_diptdoc = NTSCStr(dsCopia.Tables("REGDIR").Rows(i)!rd_diptdoc)

        oCleGreg.IncollaCartellaInsertIntoRegDir(strCur, strRd_diput, strRd_dipaz, strRd_diptdoc)

        If NTSCStr(dsCopia.Tables("REGDIR").Rows(i)!rd_diput) = "S" Then
          newDip(0) = strCopyNomOp
        Else
          newDip(0) = " "
        End If
        If NTSCStr(dsCopia.Tables("REGDIR").Rows(i)!rd_dipaz) = "S" Then
          newDip(1) = strCopyCodAz
        Else
          newDip(1) = " "
        End If
        If NTSCStr(dsCopia.Tables("REGDIR").Rows(i)!rd_diptdoc) = "S" Then
          newDip(2) = strCopyTipDoc
        Else
          newDip(2) = " "
        End If
REGPROP:
        If NTSCStr(dsCopia.Tables("REGDIR").Rows(i)!rd_diput) = "S" Then
          strDip(0) = strCopyNomOp
        Else
          strDip(0) = " "
        End If
        If NTSCStr(dsCopia.Tables("REGDIR").Rows(i)!rd_dipaz) = "S" Then
          strDip(1) = strCopyCodAz
        Else
          strDip(1) = " "
        End If
        If NTSCStr(dsCopia.Tables("REGDIR").Rows(i)!rd_diptdoc) = "S" Then
          strDip(2) = strCopyTipDoc
        Else
          strDip(2) = " "
        End If

        strRd_liv1 = NTSCStr(dsCopia.Tables("REGDIR").Rows(i)!rd_liv1)
        strRd_liv2 = NTSCStr(dsCopia.Tables("REGDIR").Rows(i)!rd_liv2)
        strRd_liv3 = NTSCStr(dsCopia.Tables("REGDIR").Rows(i)!rd_liv3)

        oCleGreg.IncollaCartellaSelectRegprop(strDip, strRd_liv1, strRd_liv2, strRd_liv3, dsRegprop)

        For j = 0 To dsRegprop.Tables("REGPROP").Rows.Count - 1

          strRp_nomprop = NTSCStr(dsRegprop.Tables("REGPROP").Rows(j)!rp_nomprop)
          strRp_valprop = NTSCStr(dsRegprop.Tables("REGPROP").Rows(j)!rp_valprop)

          bRet = oCleGreg.IncollaCartellaInsertIntoRegprop(strCur, newDip, strRp_nomprop, strRp_valprop)

          If bRet = False Then
            If bRiscrProp Then
              oCleGreg.IncollaCartellaUpdate(strCur, strRp_nomprop, strRp_valprop)
            End If
          End If
        Next
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbSelezionaCartella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSelezionaCartella.ItemClick
    Dim strCart As String
    Dim fOut As Boolean
    Dim nCartella As Integer
    Dim strKey As String
    Dim l As Integer
    Try
      strCart = oApp.InputBoxNew(oApp.Tr(Me, 128341715192352978, "Trova Cartella"))
      If Len(Trim(strCart)) = 0 Then
        Exit Sub
      End If
      strCart = strCart.ToLower
      nodReg = trGreg.SelectedNode
      strKey = nodReg.Name
      l = 1
      nCartella = 0
      l = InStr(l, strKey, "|", vbTextCompare)
      While l > 0
        nCartella = nCartella + 1
        l = l + 1
        l = InStr(l, strKey, "|", vbTextCompare)
      End While
      If nodReg.Nodes.Count = 0 Then ApriRamo(nodReg)
      fOut = Trova(nodReg, strCart, nCartella)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbCopiaRighe_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCopiaRighe.ItemClick
    Dim i As Integer
    Try
      dsCopiaCancRighe = dsGreg.Clone
      For i = 0 To dsGreg.Tables("REGPROP").Rows.Count - 1
        If NTSCStr(dsGreg.Tables("REGPROP").Rows(i)!xx_seleziona) = "S" Then
          dsCopiaCancRighe.Tables("REGPROP").ImportRow(dsGreg.Tables("REGPROP").Rows(i))
        End If
      Next
      For i = 0 To dsGreg.Tables("REGPROP").Rows.Count - 1
        dsGreg.Tables("REGPROP").Rows(i)!xx_seleziona = "N"
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbIncollaRighe_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbIncollaRighe.ItemClick
    Dim i As Integer
    Try
      If dsCopiaCancRighe Is Nothing Then Exit Sub
      Me.Cursor = Cursors.WaitCursor
      For i = 0 To dsCopiaCancRighe.Tables("REGPROP").Rows.Count - 1
        dsGreg.Tables("REGPROP").Rows.Add()
        dsGreg.Tables("REGPROP").Rows(dsGreg.Tables("REGPROP").Rows.Count - 1)!rp_nomprop = NTSCStr(dsCopiaCancRighe.Tables("REGPROP").Rows(i)!rp_nomprop)
        If ckDip0.Checked Then
          dsGreg.Tables("REGPROP").Rows(dsGreg.Tables("REGPROP").Rows.Count - 1)!rp_idut = edCode0.Text
        Else
          dsGreg.Tables("REGPROP").Rows(dsGreg.Tables("REGPROP").Rows.Count - 1)!rp_idut = NTSCStr(dsCopiaCancRighe.Tables("REGPROP").Rows(i)!rp_idut)
        End If
        If ckDip1.Checked Then
          dsGreg.Tables("REGPROP").Rows(dsGreg.Tables("REGPROP").Rows.Count - 1)!rp_idaz = edCode1.Text
        Else
          dsGreg.Tables("REGPROP").Rows(dsGreg.Tables("REGPROP").Rows.Count - 1)!rp_idaz = NTSCStr(dsCopiaCancRighe.Tables("REGPROP").Rows(i)!rp_idaz)
        End If
        If ckDip2.Checked Then
          dsGreg.Tables("REGPROP").Rows(dsGreg.Tables("REGPROP").Rows.Count - 1)!rp_idtdoc = edCode2.Text
        Else
          dsGreg.Tables("REGPROP").Rows(dsGreg.Tables("REGPROP").Rows.Count - 1)!rp_idtdoc = NTSCStr(dsCopiaCancRighe.Tables("REGPROP").Rows(i)!rp_idtdoc)
        End If
        dsGreg.Tables("REGPROP").Rows(dsGreg.Tables("REGPROP").Rows.Count - 1)!rp_valprop = NTSCStr(dsCopiaCancRighe.Tables("REGPROP").Rows(i)!rp_valprop)
        SalvaDipendenze()
        oCleGreg.Salva(False)
      Next
      trGreg.Enabled = True
      Me.Cursor = Cursors.Default
    Catch ex As Exception
      trGreg.Enabled = True
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub tlbEliminaRighe_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEliminaRighe.ItemClick
    Dim i As Integer
    Try
      trGreg.Enabled = False
      Me.Cursor = Cursors.WaitCursor
      If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128271029890038656, "Confermare la cancellazione delle righe selezionate?")) = Windows.Forms.DialogResult.Yes Then
        For i = dsGreg.Tables("REGPROP").Rows.Count - 1 To 0 Step -1
          If NTSCStr(dsGreg.Tables("REGPROP").Rows(i)!xx_seleziona) = "S" Then
            dsGreg.Tables("REGPROP").Rows(i).Delete()
            oCleGreg.Salva(True)
          End If
        Next
      End If
      trGreg.Enabled = True
      Me.Cursor = Cursors.Default
    Catch ex As Exception
      trGreg.Enabled = True
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbPropPrinter_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPropPrinter.ItemClick
    oMenu.ReportImposta(Me)
    oApp.MsgBoxInfo(oApp.Tr(Me, 128450411360592000, "Proprietà della stampante selezionata:") & vbCrLf & _
                   "ATTENZIONE: per le installazioni TERMINAL SERVER, " & vbCrLf & _
                   "il nome della stampante va riportato senza l'indicazione ""(Rendirizzamento ...)""" & vbCrLf & _
                   "------------------------------------" & vbCrLf & _
                   oMenu.ShowPrinterProperties)
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
  End Sub
  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

#Region "EventiGriglia"
  Public Overridable Sub grvProp_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvProp.NTSBeforeRowUpdate
    Try
      If Not Salva() Then
        'rimango sulla riga su cui sono
        e.Allow = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub grvProp_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvProp.GotFocus
    Try
      Stato(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub grProp_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grProp.Leave
    Try
      Salva()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
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
          Case "NUOVARIGA:"
            oCleGreg.strFullPathNewRow = trGreg.SelectedNode.FullPath
            oCleGreg.bckDip0NewRow = ckDip0.Checked
            oCleGreg.bckDip1NewRow = ckDip1.Checked
            oCleGreg.bckDip2NewRow = ckDip2.Checked
            oCleGreg.stredCode0NewRow = edCode0.Text
            oCleGreg.stredCode1NewRow = edCode1.Text
            oCleGreg.stredCode2NewRow = edCode2.Text
        End Select
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Apri() As Boolean
    Dim lMenu As Integer
    Try
      nTotRiga = -1
      oCleGreg.strGregNomOp = oApp.User.Nome
      oCleGreg.strGregCodAz = DittaCorrente
      oCleGreg.strGregTipoDoc = " "
      edCode0.Text = oCleGreg.strGregNomOp
      edCode1.Text = oCleGreg.strGregCodAz
      edCode2.Text = oCleGreg.strGregTipoDoc
      lbPath.Text = "BUSINESS"

      lMenu = trGreg.Nodes.Add(New TreeNode)
      nodReg = trGreg.Nodes(lMenu)
      trGreg.Nodes(lMenu).Text = "BUSINESS"
      trGreg.Nodes(lMenu).Name = "business"

      oCleGreg.CreaRoot()
      Stato(0)
      FileApri(nodReg)

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvProp.NTSSalvaRigaCorrente(dcGreg, oCleGreg.RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          SalvaDipendenze()

          If Not oCleGreg.Salva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleGreg.Ripristina(dcGreg.Position, dcGreg.Filter)
        Case System.Windows.Forms.DialogResult.Cancel
          'non posso fare nulla
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          'la riga non ha subito modifiche
      End Select
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub Stato(ByVal i As Integer)
    Try
      Select Case i
        Case 0
          tlbNuovo.Enabled = True
          tlbCancella.Enabled = True
          tlbRecordNuovo.Enabled = False
          tlbRecordCancella.Enabled = False
          tlbRecordRipristina.Enabled = False
          tlbRecordSalva.Enabled = False
          tlbCopiaRighe.Enabled = False
          tlbIncollaRighe.Enabled = False
          tlbEliminaRighe.Enabled = False
        Case 1
          tlbNuovo.Enabled = False
          tlbCancella.Enabled = False
          tlbRecordNuovo.Enabled = True
          tlbRecordCancella.Enabled = True
          tlbRecordRipristina.Enabled = True
          tlbRecordSalva.Enabled = True
          tlbCopiaRighe.Enabled = True
          tlbIncollaRighe.Enabled = True
          tlbEliminaRighe.Enabled = True
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FileApri(ByVal TempNodo As TreeNode)
    Dim nIndice As Integer
    Dim strDipend As String = ""
    Dim strPathCor As String
    Dim strCampi(0 To 2) As String
    Dim strCodici(0 To 2) As String
    Dim ds As New DataSet
    Try
      strPathCor = TempNodo.FullPath
      oCleGreg.EstraiDaPath(strPathCor, strCampi)
      oCleGreg.FileApri(strCampi, ds)

      If ds.Tables("REGDIR").Rows.Count = 0 Then
        Exit Sub
      End If
      bLoadTree = True
      For nIndice = 3 To 5
        Select Case nIndice
          Case 3
            strDipend = "rd_diput"
          Case 4
            strDipend = "rd_dipaz"
          Case 5
            strDipend = "rd_diptdoc"
        End Select
        If CheckEdcode(nIndice - 3, "*") Then
          strCodici(nIndice - 3) = ""
          If NTSCStr(ds.Tables("REGDIR").Rows(0)(strDipend)) = "S" Then
            SetckDip(nIndice - 3, True)
          Else
            SetckDip(nIndice - 3, False)
          End If
        Else
          If NTSCStr(ds.Tables("REGDIR").Rows(0)(strDipend)) = "S" Then
            SetckDip(nIndice - 3, True)
            strCodici(nIndice - 3) = GetEdcode(nIndice - 3)
          Else
            SetckDip(nIndice - 3, False)
            strCodici(nIndice - 3) = " "
          End If
        End If
      Next
      bLoadTree = False
      AggiornaGriglia(strCampi, strCodici)
      lbPath.Text = TempNodo.FullPath

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function CheckEdcode(ByVal i As Integer, ByVal strConfronto As String) As Boolean
    Try
      Select Case i
        Case 0
          If edCode0.Text = strConfronto Then
            Return True
          Else
            Return False
          End If
        Case 1
          If edCode1.Text = strConfronto Then
            Return True
          Else
            Return False
          End If
        Case 2
          If edCode2.Text = strConfronto Then
            Return True
          Else
            Return False
          End If
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetEdcode(ByVal i As Integer) As String
    GetEdcode = ""
    Try
      Select Case i
        Case 0
          Return edCode0.Text
        Case 1
          Return edCode1.Text
        Case 2
          Return edCode2.Text
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Sub SetEdcode(ByVal i As Integer, ByVal strVal As String)
    Try
      Select Case i
        Case 0
          edCode0.Text = strVal
        Case 1
          edCode1.Text = strVal
        Case 2
          edCode2.Text = strVal
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Function SetckDip(ByVal i As Integer, ByVal bChecked As Boolean) As Boolean
    Try
      Select Case i
        Case 0
          ckDip0.Checked = bChecked
        Case 1
          ckDip1.Checked = bChecked
        Case 2
          ckDip2.Checked = bChecked
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function SetckDipEnab(ByVal i As Integer, ByVal bChecked As Boolean) As Boolean
    Try
      Select Case i
        Case 0
          ckDip0.Enabled = bChecked
        Case 1
          ckDip1.Enabled = bChecked
        Case 2
          ckDip2.Enabled = bChecked
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub AggiornaGriglia(ByVal strFoglie() As String, ByVal strDip() As String)
    Dim i As Integer
    Try
      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleGreg.Apri(DittaCorrente, strFoglie, strDip, dsGreg) Then Me.Close()

      dcGreg.DataSource = dsGreg.Tables("REGPROP")
      grProp.DataSource = dcGreg

      dsGreg.Tables("REGPROP").Columns.Add(New DataColumn("xx_seleziona", GetType(String)))
      dsGreg.Tables("REGPROP").Columns("xx_seleziona").DefaultValue = "N"

      For i = 0 To dsGreg.Tables("REGPROP").Rows.Count - 1
        dsGreg.Tables("REGPROP").Rows(i)!xx_seleziona = "N"
      Next

      grvProp.Columns("rp_liv1").Visible = False
      grvProp.Columns("rp_liv2").Visible = False
      grvProp.Columns("rp_liv3").Visible = False

      Me.Cursor = Cursors.Default

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub treGreg_NodeClick(ByVal Node As TreeNode)
    Try
      If Len(Trim(nodReg.Text)) = 0 Then
        trGreg.Nodes.Remove(nodReg)
        nodReg = Node
        tlbNuovo.Enabled = True
        tlbCancella.Enabled = True
      End If
      If Not Node.IsExpanded Then
        FileApri(Node)
        ApriRamo(Node)
      Else
        FileApri(Node)
        Node.Collapse()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ClickNodo(ByVal curNode As TreeNode)
    Try
      If Len(Trim(nodReg.Text)) = 0 Then
        trGreg.Nodes.Remove(nodReg)
        nodReg = curNode
        tlbNuovo.Enabled = True
        tlbCancella.Enabled = True
      End If
      If Not curNode.IsExpanded Then
        FileApri(curNode)
        ApriRamo(curNode)
      Else
        FileApri(curNode)
        curNode.Collapse()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub NuovaCartella()
    Dim nIndice As Integer
    Dim nCont As Integer
    Dim strKeyCor As String
    Dim strNewChiave As String
    Dim strNewNome As String
    Dim lMenu As Integer
    Dim curNode As TreeNode
    Dim nodTmp As TreeNode
    Dim oNodeNew As TreeNode
    Try
      '-------------------------------------------------
      'creo una nuova cartella

      tlbNuovo.Enabled = False
      tlbCancella.Enabled = False
      nodTmp = trGreg.SelectedNode
      strKeyCor = nodTmp.Name.ToString
      nCont = 0
      For nIndice = 1 To Len(strKeyCor)
        If Mid(strKeyCor, nIndice, 1) = "|" Then
          nCont = nCont + 1
        End If
      Next
      If nCont = 3 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128328768144118091, "L'albero non può contenere più di 3 livelli."))
        tlbNuovo.Enabled = True
        tlbCancella.Enabled = True
        Exit Sub
      End If
      strNewNome = ""
      strNewChiave = strKeyCor & "|" & strNewNome.ToLower
      curNode = trGreg.SelectedNode
      oNodeNew = New TreeNode
      oNodeNew.Name = strNewChiave
      oNodeNew.Text = strNewNome
      lMenu = curNode.Nodes.Add(oNodeNew)
      bLoadTree = True
      ckDip0.Checked = False
      ckDip1.Checked = False
      ckDip2.Checked = False
      bLoadTree = False
      trGreg.SelectedNode = curNode.Nodes(lMenu)
      BeforeRinominaCartella(True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub BeforeRinominaCartella(ByVal bNuova As Boolean)
    Dim strOldText As String
    Dim strNewString As String = ""
    Dim i As Integer
    Dim j As Integer
    Dim k As Integer
    Dim z As Integer
    Dim nodTmp As TreeNode
    Dim bTrovato As Boolean
    Dim strT() As String = Nothing
    Dim strPath As String = ""
    Try
      If trGreg.SelectedNode.Name = "business" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128329320279713791, "Non è possibile rinominare la cartella Root"))
      Else
        strNewString = oApp.InputBoxNew(oApp.Tr(Me, 128341633671485113, "Digitare il nuovo nome della cartella"), trGreg.SelectedNode.Text)

        'attenzione non usare oApp.MsgBoxInfoYesNo_DefYes provoca 
        nodReg = trGreg.SelectedNode
        strOldText = nodReg.Text
        If Len(strNewString) = 0 Then
          nodReg.Text = strOldText
          nodReg = trGreg.SelectedNode
          If bNuova Then trGreg.SelectedNode.Remove() 'rimuovo il nodo creato nel caso registro con solo il root creo nuovo nodo vuoto cliccando poi dava err
          Exit Sub
        End If

        strPath = nodReg.Parent.Name & "|" & strNewString.ToLower

        'controllo chiave non presente in 4 livelli
        bTrovato = True
        For i = 0 To trGreg.Nodes.Count - 1
          nodTmp = trGreg.Nodes(i)
          If nodTmp.Name = strPath Then GoTo esciTrovato 'text
          For j = 0 To nodTmp.Nodes.Count() - 1
            nodTmp = trGreg.Nodes(i).Nodes(j)
            If nodTmp.Name = strPath Then GoTo esciTrovato
            For k = 0 To trGreg.Nodes(i).Nodes(j).Nodes.Count - 1
              nodTmp = trGreg.Nodes(i).Nodes(j).Nodes(k)
              If nodTmp.Name = strPath Then GoTo esciTrovato
              For z = 0 To trGreg.Nodes(i).Nodes(j).Nodes(k).Nodes.Count - 1
                nodTmp = trGreg.Nodes(i).Nodes(j).Nodes(k).Nodes(z)
                If nodTmp.Name = strPath Then GoTo esciTrovato
              Next
            Next
          Next
        Next
        bTrovato = False
esciTrovato:
        If bTrovato = True Then
          nodReg.Text = strOldText
          oApp.MsgBoxErr(oApp.Tr(Me, 128329583356585849, "Nome cartella già esistente, operazione annullata."))
          nodReg = trGreg.SelectedNode
          Exit Sub
        End If

        RinominaCartella(strNewString, strOldText)

        FileAggiorna()
        lbPath.Text = trGreg.SelectedNode.FullPath
        tlbNuovo.Enabled = True
        tlbCancella.Enabled = True
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub RinominaCartella(ByVal strNewText As String, ByVal strOldText As String)
    Dim nIndice As Integer
    Dim strPathCor As String
    Dim strFuz(0 To 2) As String
    Dim strCampi(0 To 2) As String
    Dim strCodici(0 To 2) As String
    Dim nCont As Integer
    Dim strKeyCor As String
    Dim i As Integer
    Dim nodTmp As TreeNode
    Try
      nodTmp = trGreg.SelectedNode
      strPathCor = nodTmp.FullPath

      nodTmp.Collapse()

      Me.Cursor = Cursors.WaitCursor
      oCleGreg.EstraiDaPath(strPathCor, strCampi)
      oCleGreg.GuardaDip(strFuz, strCodici, ckDip0.Checked, ckDip1.Checked, ckDip2.Checked, ckDip0.Enabled, ckDip1.Enabled, ckDip2.Enabled, edCode0.Text, edCode1.Text, edCode2.Text)

      strKeyCor = nodTmp.Name
      nCont = 0
      For nIndice = 1 To Len(strKeyCor)
        If Mid(strKeyCor, nIndice, 1) = "|" Then
          nCont = nCont + 1
        End If
      Next

      If strOldText <> "" Then oCleGreg.RinominaCartella(strCampi, strNewText, nCont)

      nodReg = nodTmp
      nodReg.Text = strNewText
      nodReg.Name = nodReg.Parent.Name & "|" & strNewText.ToLower
      strKeyCor = nodReg.Name
      'Elimiana tutti i figli (poi verranno ricostruiti aprendo la cartella)
      For i = nodReg.Nodes.Count - 1 To 0 Step -1
        nodReg.Nodes(i).Remove()
      Next

      FileApri(trGreg.SelectedNode)
      Me.Cursor = Cursors.Default

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub CancellaCartella()
    Dim strPathCor As String
    Dim strFuz(0 To 2) As String
    Dim strCampi(0 To 2) As String
    Dim strCodici(0 To 2) As String
    Dim dlgRes As DialogResult
    Dim ds As DataSet = Nothing
    Dim i As Integer
    Dim nodTmp As TreeNode
    Try
      '-------------------------------------------------
      'cancello la cartella

      nodTmp = trGreg.SelectedNode
      strPathCor = nodTmp.FullPath

      If nodTmp.Name.ToString = "business" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128328712953803825, "Non è possibile eliminare la cartella Root."))
        Exit Sub
      End If

      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128328755894719916, "Confermi l'eliminazione della cartella '|" & trGreg.SelectedNode.Text & "|'?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          Me.Cursor = Cursors.WaitCursor
          oCleGreg.EstraiDaPath(strPathCor, strCampi)
          oCleGreg.GuardaDip(strFuz, strCodici, ckDip0.Checked, ckDip1.Checked, ckDip2.Checked, ckDip0.Enabled, ckDip1.Enabled, ckDip2.Enabled, edCode0.Text, edCode1.Text, edCode2.Text)
          '-------------------------------------------------------------------------------------
          '--- Per ogni riga cancellata, scrive l'operazione in ACTLOG
          '-------------------------------------------------------------------------------------
          oCleGreg.GetRegProp(strCampi, ds)
          For i = 0 To ds.Tables("REGPROP").Rows.Count - 1
            With ds.Tables("REGPROP").Rows(i)
              oCleGreg.strLiv1Tmp = NTSCStr(!rp_liv1)
              oCleGreg.strLiv2Tmp = NTSCStr(!rp_liv2)
              oCleGreg.strLiv3Tmp = NTSCStr(!rp_liv3)
              oCleGreg.strIdutTmp = NTSCStr(!rp_idut)
              oCleGreg.strIdazTmp = NTSCStr(!rp_idaz)
              oCleGreg.strIdtdocTmp = NTSCStr(!rp_idtdoc)
              oCleGreg.strNompropTmp = NTSCStr(!rp_nomprop)
            End With
            oCleGreg.strSqlWhere = CStrSQL(oCleGreg.strLiv1Tmp) & "," & CStrSQL(oCleGreg.strLiv2Tmp) & "," & _
              CStrSQL(oCleGreg.strLiv3Tmp) & "," & CStrSQL(oCleGreg.strIdutTmp) & "," & _
              CStrSQL(oCleGreg.strIdazTmp) & "," & CStrSQL(oCleGreg.strIdtdocTmp) & "," & _
              CStrSQL(oCleGreg.strNompropTmp)
            oCleGreg.strDesogglog = "Cancellazione impostazioni registro di Business " & oCleGreg.strLiv1Tmp
            If oCleGreg.strLiv2Tmp <> "." Then oCleGreg.strDesogglog = oCleGreg.strDesogglog & "\" & oCleGreg.strLiv2Tmp
            If oCleGreg.strLiv3Tmp <> "." Then oCleGreg.strDesogglog = oCleGreg.strDesogglog & "\" & oCleGreg.strLiv3Tmp
            oCleGreg.strTipoop = "A"
            oCleGreg.strTipoogg = "D"
            oCleGreg.ScriviLog()
          Next
          '-------------------------------------------------------------------------------------
          oCleGreg.DeleteRegprop(strCampi)
          trGreg.Nodes.Remove(nodTmp)
          FileApri(trGreg.SelectedNode)
          Me.Cursor = Cursors.Default
      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ApriRamo(ByRef curNode As TreeNode)
    Dim nLivello As Integer
    Dim TempNode As TreeNode
    Dim strFiglio As String = ""
    Dim strChiavePadre As String
    Dim strChiave As String
    Dim strCampi(0 To 2) As String
    Dim ds As DataSet = Nothing
    Dim i As Integer
    Dim oNode As TreeNode = Nothing
    Try
      Me.Cursor = Cursors.WaitCursor
      TempNode = curNode
      nLivello = 1
      While TempNode.FullPath <> "BUSINESS"
        nLivello = nLivello + 1
        TempNode = TempNode.Parent
      End While
      If nLivello = 4 Then
        Exit Sub
      End If
      oCleGreg.EstraiDaPath(curNode.FullPath, strCampi)
      If strChild <> "" Then strCampi(0) = strChild

      oCleGreg.ApriRamo(strCampi, nLivello, ds)

      If ds.Tables("REGDIR").Rows.Count = 0 Then
        Exit Sub
      End If

      strChiavePadre = curNode.Name
      For i = 0 To ds.Tables("REGDIR").Rows.Count - 1
        If (NTSCStr(ds.Tables("REGDIR").Rows(i)("rd_liv" & nLivello)) <> strFiglio) Then
          strFiglio = NTSCStr(ds.Tables("REGDIR").Rows(i)("rd_liv" & nLivello))
          If strFiglio <> "." Then
            If Not EsisteFiglio(curNode, strChiavePadre & "|" & strFiglio.ToLower) Then
              strChiave = strChiavePadre & "|" & strFiglio.ToLower
              oNode = New TreeNode
              oNode.Name = strChiave
              oNode.Text = strFiglio
              curNode.Nodes.Add(oNode)
            End If
          End If
        End If
      Next
      curNode.Expand()
      Me.Cursor = Cursors.Default

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function EsisteFiglio(ByVal Padre As TreeNode, ByVal strKey As String) As Boolean
    Dim nFigli As Integer
    Dim nIndice As Integer
    Dim Figlio As TreeNode
    Try
      EsisteFiglio = False
      If Padre.Nodes.Count = 0 Then
        Exit Function
      End If
      nFigli = Padre.Nodes.Count
      Figlio = Padre.Nodes(0)
      nIndice = -1
      While nIndice < nFigli - 1
        nIndice = nIndice + 1
        Figlio = Padre.Nodes(nIndice)
        If Figlio.Name = strKey Then
          EsisteFiglio = True
          Exit Function
        End If
      End While
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub SalvaDipendenze()
    Try
      oCleGreg.bCkDip0 = ckDip0.Checked()
      oCleGreg.bCkDip1 = ckDip1.Checked()
      oCleGreg.bCkDip2 = ckDip2.Checked()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FileAggiorna()
    Dim strPathCor As String
    Dim strCampi(0 To 2) As String
    Dim strDipend(0 To 2) As String
    Dim strCodici(0 To 2) As String
    Dim dsRegDir As DataSet = Nothing
    Dim nodTmp As TreeNode
    Try
      oCleGreg.GuardaDip(strDipend, strCodici, ckDip0.Checked, ckDip1.Checked, ckDip2.Checked, ckDip0.Enabled, ckDip1.Enabled, ckDip2.Enabled, edCode0.Text, edCode1.Text, edCode2.Text)
      nodTmp = trGreg.SelectedNode
      strPathCor = nodTmp.FullPath
      oCleGreg.EstraiDaPath(strPathCor, strCampi)

      oCleGreg.FileAggiornaSelectRegdir(strCampi, dsRegDir)

      If dsRegDir.Tables("REGDIR").Rows.Count = 0 Then
        oCleGreg.FileAggiornaInsertIntoRegDir(strCampi, strDipend)
      Else
        oCleGreg.FileAggiornaUpdateRegDir(strCampi, strDipend)
      End If
      AggiornaGriglia(strCampi, strCodici)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Trova(ByVal Nodo As TreeNode, ByVal strCartella As String, ByVal nLev As Integer) As Boolean
    Dim nFigli, nIndice As Integer
    Dim fUscita As Boolean
    Dim bFirstLevel As Boolean
    Try
      If Trova Then Exit Function
      If nLev = 3 Then Exit Function
      bFirstLevel = False
      Nodo.Collapse()
      nFigli = Nodo.Nodes.Count
      If nFigli = 0 Then Exit Function
      Nodo = Nodo.Nodes(0)
      If nLev = 0 Then
        nLev = 2
        bFirstLevel = True
      Else
        nLev = nLev + 1
      End If
      For nIndice = 1 To nFigli
        If LCase(Nodo.Text) = strCartella Then
          FileApri(Nodo)
          trGreg.SelectedNode = Nodo
        End If
        If nLev < 3 And Not bFirstLevel Then
          ApriRamo(Nodo)
          fUscita = Trova(Nodo, strCartella, nLev)
          If fUscita Then Exit Function
        End If
        Nodo = Nodo.NextNode
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub RemappaggioLivelli(ByRef strCampi() As String)
    Try
      '--------------------------------------------------------------------------------------------------------------
      If (strCampi(1) = ".") And (strCampi(2) = ".") Then Return
      '--------------------------------------------------------------------------------------------------------------
      bEsciDaZoom = False
      '--------------------------------------------------------------------------------------------------------------
      Try
        If strCampi(2) = "." Then
          If (strCampi(1).Substring(0, 7)).ToLower = "reports" Then
            strCampi(0) = "."
            strCampi(1) = "REPORTS"
            strCampi(2) = "."
          Else
            If (strCampi(1)).ToUpper = "DCOM" Then
              strCampi(0) = "."
              strCampi(1) = "DCOM"
              strCampi(2) = "."
            End If
          End If
        Else
          If (strCampi(1).Substring(0, 3)).ToUpper = "ALE" Or (strCampi(2)).ToUpper = "ENTRYPOINT" Then
            strCampi(0) = "."
            strCampi(1) = "."
            strCampi(2) = "ALE"
          End If
          If (strCampi(2)).ToUpper = "ENTRYPOINT" Then
            strCampi(0) = "."
            strCampi(1) = "."
            strCampi(2) = "ENTRYPOINT"
          End If
          Select Case (strCampi(2).Substring(0, Len(strCampi(2)) - 1)).ToLower
            Case "rep"
              strCampi(0) = "."
              strCampi(1) = "."
              strCampi(2) = "Rep"
            Case "ctrl", "ctrl1", "ctrl2", "ctrl3", "ctrl4", "ctrl5"
              strCampi(0) = "."
              strCampi(1) = "."
              strCampi(2) = "Ctrl"
            Case "item", "item1", "item2", "item3", "item4", "item5"
              strCampi(0) = "."
              strCampi(1) = "."
              strCampi(2) = "Item"
            Case "entrypoin"    'la T manca appositamente !!
              strCampi(0) = "."
              strCampi(1) = "."
              strCampi(2) = "ENTRYPOINT"
          End Select
          If strCampi(2).ToLower = "rep10" Then 'deroga...
            strCampi(0) = "."
            strCampi(1) = "."
            strCampi(2) = "Rep"
          End If
        End If
      Catch ex As Exception
        bEsciDaZoom = True
      End Try
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub rp_nomprop_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles rp_nomprop.NTSZoomGest
    Try
      'per il momenento non c'è la possibilità di andare in gestione della destinazione diversa
      'se si cambia qualche dato occorrerebbe contemplare l'aggiornamento delle eventuali anagrafiche generali...
      'oApp.MsgBoxErr("da fare: chiamata per poter gestire ALT+F2 o ALT+F3")
      e.ZoomHandled = True        'non facci eseguire la routine standard del ALT+F2 o ALT+F3
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub rp_valprop_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles rp_valprop.NTSZoomGest
    Try
      'per il momenento non c'è la possibilità di andare in gestione della destinazione diversa
      'se si cambia qualche dato occorrerebbe contemplare l'aggiornamento delle eventuali anagrafiche generali...
      'oApp.MsgBoxErr("da fare: chiamata per poter gestire ALT+F2 o ALT+F3")
      e.ZoomHandled = True        'non facci eseguire la routine standard del ALT+F2 o ALT+F3
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class
