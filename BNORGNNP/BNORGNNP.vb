Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORGNNP
  Private Moduli_P As Integer = bsModMG + bsModVE
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

#Region "Variabili"
  Public oCleGnnp As CLEORGNNP
  Public oCallParams As CLE__CLDP
  Public dsGnnp As New DataSet
  Public dcGnnp As BindingSource = New BindingSource()
  Public strTiporkDaProgramma As String = ""
  Public strElencoProgressivi As String = ""

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
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
  Public WithEvents grGnnp As NTSInformatica.NTSGrid
  Public WithEvents grvGnnp As NTSInformatica.NTSGridView
  Public WithEvents xx_seleziona As NTSInformatica.NTSGridColumn
  Public WithEvents fd_anno As NTSInformatica.NTSGridColumn
  Public WithEvents fd_numdoc As NTSInformatica.NTSGridColumn
  Public WithEvents fd_serie As NTSInformatica.NTSGridColumn
  Public WithEvents fd_datdoc As NTSInformatica.NTSGridColumn
  Public WithEvents fd_totdoc As NTSInformatica.NTSGridColumn
  Public WithEvents fd_conto As NTSInformatica.NTSGridColumn
  Public WithEvents fd_descr As NTSInformatica.NTSGridColumn
  Public WithEvents fd_soloasa As NTSInformatica.NTSGridColumn
  Public WithEvents fd_tdflevas As NTSInformatica.NTSGridColumn
  Public WithEvents fd_codpaga As NTSInformatica.NTSGridColumn
  Public WithEvents fd_despaga As NTSInformatica.NTSGridColumn
  Public WithEvents fd_codtpbf As NTSInformatica.NTSGridColumn
  Public WithEvents fd_destpbf As NTSInformatica.NTSGridColumn
  Public WithEvents tlbElabora As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbModProdEvas As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRecordCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDettEvasImp As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDettEvasOrd As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDettEvasArt As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEliminaOrdValoreZero As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbEliminaOrdInEccessoMax As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbCancellaRigheSelezionate As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbCancellaRigheNonSelezionate As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbSelezionaRighe As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbDeselezionaRighe As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbEliminaOrdNonASaldo As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbRigheQtaPrelevareDivZero As NTSInformatica.NTSBarMenuItem
  Public WithEvents lbTotVal As NTSInformatica.NTSLabel
  Public WithEvents edTotVal As NTSInformatica.NTSTextBoxNum
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents pnBottom As NTSInformatica.NTSPanel
  Public WithEvents xx_descommess As NTSInformatica.NTSGridColumn
  Public WithEvents xx_righe As NTSInformatica.NTSGridColumn
  Public WithEvents xx_righemanc As NTSInformatica.NTSGridColumn
  Public WithEvents xx_dest As NTSInformatica.NTSGridColumn
  Public WithEvents xx_desdest As NTSInformatica.NTSGridColumn
  Public WithEvents fd_datcons As NTSInformatica.NTSGridColumn
  Public WithEvents xx_commess As NTSInformatica.NTSGridColumn
#End Region

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMORGNNP))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbElabora = New NTSInformatica.NTSBarButtonItem
    Me.tlbModProdEvas = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecordCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbDettEvasImp = New NTSInformatica.NTSBarButtonItem
    Me.tlbDettEvasArt = New NTSInformatica.NTSBarButtonItem
    Me.tlbDettEvasOrd = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbEliminaOrdNonASaldo = New NTSInformatica.NTSBarMenuItem
    Me.tlbEliminaOrdValoreZero = New NTSInformatica.NTSBarMenuItem
    Me.tlbEliminaOrdInEccessoMax = New NTSInformatica.NTSBarMenuItem
    Me.tlbCancellaRigheSelezionate = New NTSInformatica.NTSBarMenuItem
    Me.tlbCancellaRigheNonSelezionate = New NTSInformatica.NTSBarMenuItem
    Me.tlbSelezionaRighe = New NTSInformatica.NTSBarMenuItem
    Me.tlbDeselezionaRighe = New NTSInformatica.NTSBarMenuItem
    Me.tlbRigheQtaPrelevareDivZero = New NTSInformatica.NTSBarMenuItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grGnnp = New NTSInformatica.NTSGrid
    Me.grvGnnp = New NTSInformatica.NTSGridView
    Me.xx_seleziona = New NTSInformatica.NTSGridColumn
    Me.fd_anno = New NTSInformatica.NTSGridColumn
    Me.fd_numdoc = New NTSInformatica.NTSGridColumn
    Me.fd_serie = New NTSInformatica.NTSGridColumn
    Me.fd_datdoc = New NTSInformatica.NTSGridColumn
    Me.fd_totdoc = New NTSInformatica.NTSGridColumn
    Me.fd_conto = New NTSInformatica.NTSGridColumn
    Me.fd_descr = New NTSInformatica.NTSGridColumn
    Me.fd_soloasa = New NTSInformatica.NTSGridColumn
    Me.fd_tdflevas = New NTSInformatica.NTSGridColumn
    Me.fd_codpaga = New NTSInformatica.NTSGridColumn
    Me.fd_despaga = New NTSInformatica.NTSGridColumn
    Me.fd_codtpbf = New NTSInformatica.NTSGridColumn
    Me.fd_destpbf = New NTSInformatica.NTSGridColumn
    Me.fd_datcons = New NTSInformatica.NTSGridColumn
    Me.xx_commess = New NTSInformatica.NTSGridColumn
    Me.xx_descommess = New NTSInformatica.NTSGridColumn
    Me.xx_righe = New NTSInformatica.NTSGridColumn
    Me.xx_righemanc = New NTSInformatica.NTSGridColumn
    Me.xx_dest = New NTSInformatica.NTSGridColumn
    Me.xx_desdest = New NTSInformatica.NTSGridColumn
    Me.edTotVal = New NTSInformatica.NTSTextBoxNum
    Me.lbTotVal = New NTSInformatica.NTSLabel
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.pnBottom = New NTSInformatica.NTSPanel
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grGnnp, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvGnnp, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTotVal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBottom.SuspendLayout()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbRipristina, Me.tlbElabora, Me.tlbModProdEvas, Me.tlbRecordCancella, Me.tlbDettEvasImp, Me.tlbDettEvasOrd, Me.tlbDettEvasArt, Me.tlbEliminaOrdNonASaldo, Me.tlbEliminaOrdValoreZero, Me.tlbEliminaOrdInEccessoMax, Me.tlbCancellaRigheSelezionate, Me.tlbCancellaRigheNonSelezionate, Me.tlbSelezionaRighe, Me.tlbDeselezionaRighe, Me.tlbRigheQtaPrelevareDivZero})
    Me.NtsBarManager1.MaxItemId = 33
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbElabora, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbModProdEvas), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordCancella, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDettEvasImp), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDettEvasArt, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDettEvasOrd), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.Id = 17
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbElabora
    '
    Me.tlbElabora.Caption = "Riassegna la disp. netta agli ordini rimasti"
    Me.tlbElabora.Glyph = CType(resources.GetObject("tlbElabora.Glyph"), System.Drawing.Image)
    Me.tlbElabora.Id = 18
    Me.tlbElabora.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbElabora.Name = "tlbElabora"
    Me.tlbElabora.Visible = True
    '
    'tlbModProdEvas
    '
    Me.tlbModProdEvas.Caption = "Modifica proposta evasione"
    Me.tlbModProdEvas.Glyph = CType(resources.GetObject("tlbModProdEvas.Glyph"), System.Drawing.Image)
    Me.tlbModProdEvas.Id = 19
    Me.tlbModProdEvas.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M))
    Me.tlbModProdEvas.Name = "tlbModProdEvas"
    Me.tlbModProdEvas.Visible = True
    '
    'tlbRecordCancella
    '
    Me.tlbRecordCancella.Caption = "RecordCancella"
    Me.tlbRecordCancella.Glyph = CType(resources.GetObject("tlbRecordCancella.Glyph"), System.Drawing.Image)
    Me.tlbRecordCancella.Id = 20
    Me.tlbRecordCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F4))
    Me.tlbRecordCancella.Name = "tlbRecordCancella"
    Me.tlbRecordCancella.Visible = True
    '
    'tlbDettEvasImp
    '
    Me.tlbDettEvasImp.Caption = "Dettaglio evasione ordine"
    Me.tlbDettEvasImp.Glyph = CType(resources.GetObject("tlbDettEvasImp.Glyph"), System.Drawing.Image)
    Me.tlbDettEvasImp.Id = 21
    Me.tlbDettEvasImp.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D))
    Me.tlbDettEvasImp.Name = "tlbDettEvasImp"
    Me.tlbDettEvasImp.Visible = True
    '
    'tlbDettEvasArt
    '
    Me.tlbDettEvasArt.Caption = "Stampa proposta evasione per articolo"
    Me.tlbDettEvasArt.Glyph = CType(resources.GetObject("tlbDettEvasArt.Glyph"), System.Drawing.Image)
    Me.tlbDettEvasArt.Id = 23
    Me.tlbDettEvasArt.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
    Me.tlbDettEvasArt.Name = "tlbDettEvasArt"
    Me.tlbDettEvasArt.Visible = True
    '
    'tlbDettEvasOrd
    '
    Me.tlbDettEvasOrd.Caption = "Stampa proposta evasione per ordine"
    Me.tlbDettEvasOrd.Glyph = CType(resources.GetObject("tlbDettEvasOrd.Glyph"), System.Drawing.Image)
    Me.tlbDettEvasOrd.Id = 22
    Me.tlbDettEvasOrd.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O))
    Me.tlbDettEvasOrd.Name = "tlbDettEvasOrd"
    Me.tlbDettEvasOrd.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 15
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEliminaOrdNonASaldo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEliminaOrdValoreZero), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEliminaOrdInEccessoMax), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancellaRigheSelezionate), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancellaRigheNonSelezionate), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSelezionaRighe), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDeselezionaRighe), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRigheQtaPrelevareDivZero), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbEliminaOrdNonASaldo
    '
    Me.tlbEliminaOrdNonASaldo.Caption = "Elimina ordini non a saldo"
    Me.tlbEliminaOrdNonASaldo.Id = 25
    Me.tlbEliminaOrdNonASaldo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F5))
    Me.tlbEliminaOrdNonASaldo.Name = "tlbEliminaOrdNonASaldo"
    Me.tlbEliminaOrdNonASaldo.NTSIsCheckBox = False
    Me.tlbEliminaOrdNonASaldo.Visible = True
    '
    'tlbEliminaOrdValoreZero
    '
    Me.tlbEliminaOrdValoreZero.Caption = "Elimina Ordini a valore zero"
    Me.tlbEliminaOrdValoreZero.Id = 26
    Me.tlbEliminaOrdValoreZero.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F6))
    Me.tlbEliminaOrdValoreZero.Name = "tlbEliminaOrdValoreZero"
    Me.tlbEliminaOrdValoreZero.NTSIsCheckBox = False
    Me.tlbEliminaOrdValoreZero.Visible = True
    '
    'tlbEliminaOrdInEccessoMax
    '
    Me.tlbEliminaOrdInEccessoMax.Caption = "Elimina Ordini in eccesso max"
    Me.tlbEliminaOrdInEccessoMax.Id = 27
    Me.tlbEliminaOrdInEccessoMax.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F7))
    Me.tlbEliminaOrdInEccessoMax.Name = "tlbEliminaOrdInEccessoMax"
    Me.tlbEliminaOrdInEccessoMax.NTSIsCheckBox = False
    Me.tlbEliminaOrdInEccessoMax.Visible = True
    '
    'tlbCancellaRigheSelezionate
    '
    Me.tlbCancellaRigheSelezionate.Caption = "Cancella le righe selezionate"
    Me.tlbCancellaRigheSelezionate.Id = 28
    Me.tlbCancellaRigheSelezionate.Name = "tlbCancellaRigheSelezionate"
    Me.tlbCancellaRigheSelezionate.NTSIsCheckBox = False
    Me.tlbCancellaRigheSelezionate.Visible = True
    '
    'tlbCancellaRigheNonSelezionate
    '
    Me.tlbCancellaRigheNonSelezionate.Caption = "Cancella le righe NON selezionate"
    Me.tlbCancellaRigheNonSelezionate.Id = 29
    Me.tlbCancellaRigheNonSelezionate.Name = "tlbCancellaRigheNonSelezionate"
    Me.tlbCancellaRigheNonSelezionate.NTSIsCheckBox = False
    Me.tlbCancellaRigheNonSelezionate.Visible = True
    '
    'tlbSelezionaRighe
    '
    Me.tlbSelezionaRighe.Caption = "Seleziona tutte le righe"
    Me.tlbSelezionaRighe.Id = 30
    Me.tlbSelezionaRighe.Name = "tlbSelezionaRighe"
    Me.tlbSelezionaRighe.NTSIsCheckBox = False
    Me.tlbSelezionaRighe.Visible = True
    '
    'tlbDeselezionaRighe
    '
    Me.tlbDeselezionaRighe.Caption = "Deseleziona tutte le righe"
    Me.tlbDeselezionaRighe.Id = 31
    Me.tlbDeselezionaRighe.Name = "tlbDeselezionaRighe"
    Me.tlbDeselezionaRighe.NTSIsCheckBox = False
    Me.tlbDeselezionaRighe.Visible = True
    '
    'tlbRigheQtaPrelevareDivZero
    '
    Me.tlbRigheQtaPrelevareDivZero.Caption = "Solo righe con quantità da prelevare <> 0"
    Me.tlbRigheQtaPrelevareDivZero.Id = 32
    Me.tlbRigheQtaPrelevareDivZero.Name = "tlbRigheQtaPrelevareDivZero"
    Me.tlbRigheQtaPrelevareDivZero.NTSIsCheckBox = True
    Me.tlbRigheQtaPrelevareDivZero.Visible = True
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
    'grGnnp
    '
    Me.grGnnp.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grGnnp.EmbeddedNavigator.Name = ""
    Me.grGnnp.Location = New System.Drawing.Point(0, 0)
    Me.grGnnp.MainView = Me.grvGnnp
    Me.grGnnp.Name = "grGnnp"
    Me.grGnnp.Size = New System.Drawing.Size(648, 375)
    Me.grGnnp.TabIndex = 5
    Me.grGnnp.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvGnnp})
    '
    'grvGnnp
    '
    Me.grvGnnp.ActiveFilterEnabled = False
    Me.grvGnnp.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_seleziona, Me.fd_anno, Me.fd_numdoc, Me.fd_serie, Me.fd_datdoc, Me.fd_totdoc, Me.fd_conto, Me.fd_descr, Me.fd_soloasa, Me.fd_tdflevas, Me.fd_codpaga, Me.fd_despaga, Me.fd_codtpbf, Me.fd_destpbf, Me.fd_datcons, Me.xx_commess, Me.xx_descommess, Me.xx_righe, Me.xx_righemanc, Me.xx_dest, Me.xx_desdest})
    Me.grvGnnp.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvGnnp.Enabled = True
    Me.grvGnnp.GridControl = Me.grGnnp
    Me.grvGnnp.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvGnnp.Name = "grvGnnp"
    Me.grvGnnp.NTSAllowDelete = True
    Me.grvGnnp.NTSAllowInsert = True
    Me.grvGnnp.NTSAllowUpdate = True
    Me.grvGnnp.NTSMenuContext = Nothing
    Me.grvGnnp.OptionsCustomization.AllowRowSizing = True
    Me.grvGnnp.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvGnnp.OptionsNavigation.UseTabKey = False
    Me.grvGnnp.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvGnnp.OptionsView.ColumnAutoWidth = False
    Me.grvGnnp.OptionsView.EnableAppearanceEvenRow = True
    Me.grvGnnp.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvGnnp.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvGnnp.OptionsView.ShowGroupPanel = False
    Me.grvGnnp.RowHeight = 16
    '
    'xx_seleziona
    '
    Me.xx_seleziona.AppearanceCell.Options.UseBackColor = True
    Me.xx_seleziona.AppearanceCell.Options.UseTextOptions = True
    Me.xx_seleziona.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_seleziona.Caption = "Seleziona"
    Me.xx_seleziona.Enabled = True
    Me.xx_seleziona.FieldName = "xx_seleziona"
    Me.xx_seleziona.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_seleziona.Name = "xx_seleziona"
    Me.xx_seleziona.NTSRepositoryComboBox = Nothing
    Me.xx_seleziona.NTSRepositoryItemCheck = Nothing
    Me.xx_seleziona.NTSRepositoryItemMemo = Nothing
    Me.xx_seleziona.NTSRepositoryItemText = Nothing
    Me.xx_seleziona.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_seleziona.OptionsFilter.AllowFilter = False
    Me.xx_seleziona.Visible = True
    Me.xx_seleziona.VisibleIndex = 0
    Me.xx_seleziona.Width = 70
    '
    'fd_anno
    '
    Me.fd_anno.AppearanceCell.Options.UseBackColor = True
    Me.fd_anno.AppearanceCell.Options.UseTextOptions = True
    Me.fd_anno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_anno.Caption = "Anno"
    Me.fd_anno.Enabled = False
    Me.fd_anno.FieldName = "fd_anno"
    Me.fd_anno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_anno.Name = "fd_anno"
    Me.fd_anno.NTSRepositoryComboBox = Nothing
    Me.fd_anno.NTSRepositoryItemCheck = Nothing
    Me.fd_anno.NTSRepositoryItemMemo = Nothing
    Me.fd_anno.NTSRepositoryItemText = Nothing
    Me.fd_anno.OptionsColumn.AllowEdit = False
    Me.fd_anno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_anno.OptionsColumn.ReadOnly = True
    Me.fd_anno.OptionsFilter.AllowFilter = False
    Me.fd_anno.Visible = True
    Me.fd_anno.VisibleIndex = 1
    Me.fd_anno.Width = 70
    '
    'fd_numdoc
    '
    Me.fd_numdoc.AppearanceCell.Options.UseBackColor = True
    Me.fd_numdoc.AppearanceCell.Options.UseTextOptions = True
    Me.fd_numdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_numdoc.Caption = "Numero"
    Me.fd_numdoc.Enabled = False
    Me.fd_numdoc.FieldName = "fd_numdoc"
    Me.fd_numdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_numdoc.Name = "fd_numdoc"
    Me.fd_numdoc.NTSRepositoryComboBox = Nothing
    Me.fd_numdoc.NTSRepositoryItemCheck = Nothing
    Me.fd_numdoc.NTSRepositoryItemMemo = Nothing
    Me.fd_numdoc.NTSRepositoryItemText = Nothing
    Me.fd_numdoc.OptionsColumn.AllowEdit = False
    Me.fd_numdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_numdoc.OptionsColumn.ReadOnly = True
    Me.fd_numdoc.OptionsFilter.AllowFilter = False
    Me.fd_numdoc.Visible = True
    Me.fd_numdoc.VisibleIndex = 2
    Me.fd_numdoc.Width = 70
    '
    'fd_serie
    '
    Me.fd_serie.AppearanceCell.Options.UseBackColor = True
    Me.fd_serie.AppearanceCell.Options.UseTextOptions = True
    Me.fd_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_serie.Caption = "Serie"
    Me.fd_serie.Enabled = False
    Me.fd_serie.FieldName = "fd_serie"
    Me.fd_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_serie.Name = "fd_serie"
    Me.fd_serie.NTSRepositoryComboBox = Nothing
    Me.fd_serie.NTSRepositoryItemCheck = Nothing
    Me.fd_serie.NTSRepositoryItemMemo = Nothing
    Me.fd_serie.NTSRepositoryItemText = Nothing
    Me.fd_serie.OptionsColumn.AllowEdit = False
    Me.fd_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_serie.OptionsColumn.ReadOnly = True
    Me.fd_serie.OptionsFilter.AllowFilter = False
    Me.fd_serie.Visible = True
    Me.fd_serie.VisibleIndex = 3
    Me.fd_serie.Width = 70
    '
    'fd_datdoc
    '
    Me.fd_datdoc.AppearanceCell.Options.UseBackColor = True
    Me.fd_datdoc.AppearanceCell.Options.UseTextOptions = True
    Me.fd_datdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_datdoc.Caption = "Data"
    Me.fd_datdoc.Enabled = False
    Me.fd_datdoc.FieldName = "fd_datdoc"
    Me.fd_datdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_datdoc.Name = "fd_datdoc"
    Me.fd_datdoc.NTSRepositoryComboBox = Nothing
    Me.fd_datdoc.NTSRepositoryItemCheck = Nothing
    Me.fd_datdoc.NTSRepositoryItemMemo = Nothing
    Me.fd_datdoc.NTSRepositoryItemText = Nothing
    Me.fd_datdoc.OptionsColumn.AllowEdit = False
    Me.fd_datdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_datdoc.OptionsColumn.ReadOnly = True
    Me.fd_datdoc.OptionsFilter.AllowFilter = False
    Me.fd_datdoc.Visible = True
    Me.fd_datdoc.VisibleIndex = 4
    Me.fd_datdoc.Width = 70
    '
    'fd_totdoc
    '
    Me.fd_totdoc.AppearanceCell.Options.UseBackColor = True
    Me.fd_totdoc.AppearanceCell.Options.UseTextOptions = True
    Me.fd_totdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_totdoc.Caption = "Totale doc."
    Me.fd_totdoc.Enabled = False
    Me.fd_totdoc.FieldName = "fd_totdoc"
    Me.fd_totdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_totdoc.Name = "fd_totdoc"
    Me.fd_totdoc.NTSRepositoryComboBox = Nothing
    Me.fd_totdoc.NTSRepositoryItemCheck = Nothing
    Me.fd_totdoc.NTSRepositoryItemMemo = Nothing
    Me.fd_totdoc.NTSRepositoryItemText = Nothing
    Me.fd_totdoc.OptionsColumn.AllowEdit = False
    Me.fd_totdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_totdoc.OptionsColumn.ReadOnly = True
    Me.fd_totdoc.OptionsFilter.AllowFilter = False
    Me.fd_totdoc.Visible = True
    Me.fd_totdoc.VisibleIndex = 5
    Me.fd_totdoc.Width = 70
    '
    'fd_conto
    '
    Me.fd_conto.AppearanceCell.Options.UseBackColor = True
    Me.fd_conto.AppearanceCell.Options.UseTextOptions = True
    Me.fd_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_conto.Caption = "Cliente"
    Me.fd_conto.Enabled = False
    Me.fd_conto.FieldName = "fd_conto"
    Me.fd_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_conto.Name = "fd_conto"
    Me.fd_conto.NTSRepositoryComboBox = Nothing
    Me.fd_conto.NTSRepositoryItemCheck = Nothing
    Me.fd_conto.NTSRepositoryItemMemo = Nothing
    Me.fd_conto.NTSRepositoryItemText = Nothing
    Me.fd_conto.OptionsColumn.AllowEdit = False
    Me.fd_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_conto.OptionsColumn.ReadOnly = True
    Me.fd_conto.OptionsFilter.AllowFilter = False
    Me.fd_conto.Visible = True
    Me.fd_conto.VisibleIndex = 6
    Me.fd_conto.Width = 70
    '
    'fd_descr
    '
    Me.fd_descr.AppearanceCell.Options.UseBackColor = True
    Me.fd_descr.AppearanceCell.Options.UseTextOptions = True
    Me.fd_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_descr.Caption = "Descr. clie."
    Me.fd_descr.Enabled = False
    Me.fd_descr.FieldName = "fd_descr"
    Me.fd_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_descr.Name = "fd_descr"
    Me.fd_descr.NTSRepositoryComboBox = Nothing
    Me.fd_descr.NTSRepositoryItemCheck = Nothing
    Me.fd_descr.NTSRepositoryItemMemo = Nothing
    Me.fd_descr.NTSRepositoryItemText = Nothing
    Me.fd_descr.OptionsColumn.AllowEdit = False
    Me.fd_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_descr.OptionsColumn.ReadOnly = True
    Me.fd_descr.OptionsFilter.AllowFilter = False
    Me.fd_descr.Visible = True
    Me.fd_descr.VisibleIndex = 7
    Me.fd_descr.Width = 70
    '
    'fd_soloasa
    '
    Me.fd_soloasa.AppearanceCell.Options.UseBackColor = True
    Me.fd_soloasa.AppearanceCell.Options.UseTextOptions = True
    Me.fd_soloasa.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_soloasa.Caption = "Solo a saldo"
    Me.fd_soloasa.Enabled = False
    Me.fd_soloasa.FieldName = "fd_soloasa"
    Me.fd_soloasa.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_soloasa.Name = "fd_soloasa"
    Me.fd_soloasa.NTSRepositoryComboBox = Nothing
    Me.fd_soloasa.NTSRepositoryItemCheck = Nothing
    Me.fd_soloasa.NTSRepositoryItemMemo = Nothing
    Me.fd_soloasa.NTSRepositoryItemText = Nothing
    Me.fd_soloasa.OptionsColumn.AllowEdit = False
    Me.fd_soloasa.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_soloasa.OptionsColumn.ReadOnly = True
    Me.fd_soloasa.OptionsFilter.AllowFilter = False
    Me.fd_soloasa.Visible = True
    Me.fd_soloasa.VisibleIndex = 8
    '
    'fd_tdflevas
    '
    Me.fd_tdflevas.AppearanceCell.Options.UseBackColor = True
    Me.fd_tdflevas.AppearanceCell.Options.UseTextOptions = True
    Me.fd_tdflevas.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_tdflevas.Caption = "Evaso a saldo"
    Me.fd_tdflevas.Enabled = False
    Me.fd_tdflevas.FieldName = "fd_tdflevas"
    Me.fd_tdflevas.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_tdflevas.Name = "fd_tdflevas"
    Me.fd_tdflevas.NTSRepositoryComboBox = Nothing
    Me.fd_tdflevas.NTSRepositoryItemCheck = Nothing
    Me.fd_tdflevas.NTSRepositoryItemMemo = Nothing
    Me.fd_tdflevas.NTSRepositoryItemText = Nothing
    Me.fd_tdflevas.OptionsColumn.AllowEdit = False
    Me.fd_tdflevas.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_tdflevas.OptionsColumn.ReadOnly = True
    Me.fd_tdflevas.OptionsFilter.AllowFilter = False
    Me.fd_tdflevas.Visible = True
    Me.fd_tdflevas.VisibleIndex = 9
    '
    'fd_codpaga
    '
    Me.fd_codpaga.AppearanceCell.Options.UseBackColor = True
    Me.fd_codpaga.AppearanceCell.Options.UseTextOptions = True
    Me.fd_codpaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_codpaga.Caption = "C.Pag."
    Me.fd_codpaga.Enabled = False
    Me.fd_codpaga.FieldName = "fd_codpaga"
    Me.fd_codpaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_codpaga.Name = "fd_codpaga"
    Me.fd_codpaga.NTSRepositoryComboBox = Nothing
    Me.fd_codpaga.NTSRepositoryItemCheck = Nothing
    Me.fd_codpaga.NTSRepositoryItemMemo = Nothing
    Me.fd_codpaga.NTSRepositoryItemText = Nothing
    Me.fd_codpaga.OptionsColumn.AllowEdit = False
    Me.fd_codpaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_codpaga.OptionsColumn.ReadOnly = True
    Me.fd_codpaga.OptionsFilter.AllowFilter = False
    Me.fd_codpaga.Visible = True
    Me.fd_codpaga.VisibleIndex = 10
    '
    'fd_despaga
    '
    Me.fd_despaga.AppearanceCell.Options.UseBackColor = True
    Me.fd_despaga.AppearanceCell.Options.UseTextOptions = True
    Me.fd_despaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_despaga.Caption = "Des. Pag."
    Me.fd_despaga.Enabled = False
    Me.fd_despaga.FieldName = "fd_despaga"
    Me.fd_despaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_despaga.Name = "fd_despaga"
    Me.fd_despaga.NTSRepositoryComboBox = Nothing
    Me.fd_despaga.NTSRepositoryItemCheck = Nothing
    Me.fd_despaga.NTSRepositoryItemMemo = Nothing
    Me.fd_despaga.NTSRepositoryItemText = Nothing
    Me.fd_despaga.OptionsColumn.AllowEdit = False
    Me.fd_despaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_despaga.OptionsColumn.ReadOnly = True
    Me.fd_despaga.OptionsFilter.AllowFilter = False
    Me.fd_despaga.Visible = True
    Me.fd_despaga.VisibleIndex = 11
    '
    'fd_codtpbf
    '
    Me.fd_codtpbf.AppearanceCell.Options.UseBackColor = True
    Me.fd_codtpbf.AppearanceCell.Options.UseTextOptions = True
    Me.fd_codtpbf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_codtpbf.Caption = "Tipo bf"
    Me.fd_codtpbf.Enabled = False
    Me.fd_codtpbf.FieldName = "fd_codtpbf"
    Me.fd_codtpbf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_codtpbf.Name = "fd_codtpbf"
    Me.fd_codtpbf.NTSRepositoryComboBox = Nothing
    Me.fd_codtpbf.NTSRepositoryItemCheck = Nothing
    Me.fd_codtpbf.NTSRepositoryItemMemo = Nothing
    Me.fd_codtpbf.NTSRepositoryItemText = Nothing
    Me.fd_codtpbf.OptionsColumn.AllowEdit = False
    Me.fd_codtpbf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_codtpbf.OptionsColumn.ReadOnly = True
    Me.fd_codtpbf.OptionsFilter.AllowFilter = False
    Me.fd_codtpbf.Visible = True
    Me.fd_codtpbf.VisibleIndex = 12
    '
    'fd_destpbf
    '
    Me.fd_destpbf.AppearanceCell.Options.UseBackColor = True
    Me.fd_destpbf.AppearanceCell.Options.UseTextOptions = True
    Me.fd_destpbf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_destpbf.Caption = "Des. Tipo bf"
    Me.fd_destpbf.Enabled = False
    Me.fd_destpbf.FieldName = "fd_destpbf"
    Me.fd_destpbf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_destpbf.Name = "fd_destpbf"
    Me.fd_destpbf.NTSRepositoryComboBox = Nothing
    Me.fd_destpbf.NTSRepositoryItemCheck = Nothing
    Me.fd_destpbf.NTSRepositoryItemMemo = Nothing
    Me.fd_destpbf.NTSRepositoryItemText = Nothing
    Me.fd_destpbf.OptionsColumn.AllowEdit = False
    Me.fd_destpbf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_destpbf.OptionsColumn.ReadOnly = True
    Me.fd_destpbf.OptionsFilter.AllowFilter = False
    Me.fd_destpbf.Visible = True
    Me.fd_destpbf.VisibleIndex = 13
    '
    'fd_datcons
    '
    Me.fd_datcons.AppearanceCell.Options.UseBackColor = True
    Me.fd_datcons.AppearanceCell.Options.UseTextOptions = True
    Me.fd_datcons.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.fd_datcons.Caption = "Data cons. ord."
    Me.fd_datcons.Enabled = False
    Me.fd_datcons.FieldName = "fd_datcons"
    Me.fd_datcons.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.fd_datcons.Name = "fd_datcons"
    Me.fd_datcons.NTSRepositoryComboBox = Nothing
    Me.fd_datcons.NTSRepositoryItemCheck = Nothing
    Me.fd_datcons.NTSRepositoryItemMemo = Nothing
    Me.fd_datcons.NTSRepositoryItemText = Nothing
    Me.fd_datcons.OptionsColumn.AllowEdit = False
    Me.fd_datcons.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.fd_datcons.OptionsColumn.ReadOnly = True
    Me.fd_datcons.OptionsFilter.AllowFilter = False
    Me.fd_datcons.Visible = True
    Me.fd_datcons.VisibleIndex = 14
    '
    'xx_commess
    '
    Me.xx_commess.AppearanceCell.Options.UseBackColor = True
    Me.xx_commess.AppearanceCell.Options.UseTextOptions = True
    Me.xx_commess.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_commess.Caption = "Commessa"
    Me.xx_commess.Enabled = False
    Me.xx_commess.FieldName = "xx_commess"
    Me.xx_commess.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_commess.Name = "xx_commess"
    Me.xx_commess.NTSRepositoryComboBox = Nothing
    Me.xx_commess.NTSRepositoryItemCheck = Nothing
    Me.xx_commess.NTSRepositoryItemMemo = Nothing
    Me.xx_commess.NTSRepositoryItemText = Nothing
    Me.xx_commess.OptionsColumn.AllowEdit = False
    Me.xx_commess.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_commess.OptionsColumn.ReadOnly = True
    Me.xx_commess.OptionsFilter.AllowFilter = False
    Me.xx_commess.Visible = True
    Me.xx_commess.VisibleIndex = 15
    '
    'xx_descommess
    '
    Me.xx_descommess.AppearanceCell.Options.UseBackColor = True
    Me.xx_descommess.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descommess.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descommess.Caption = "Descr commessa"
    Me.xx_descommess.Enabled = False
    Me.xx_descommess.FieldName = "xx_descommess"
    Me.xx_descommess.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descommess.Name = "xx_descommess"
    Me.xx_descommess.NTSRepositoryComboBox = Nothing
    Me.xx_descommess.NTSRepositoryItemCheck = Nothing
    Me.xx_descommess.NTSRepositoryItemMemo = Nothing
    Me.xx_descommess.NTSRepositoryItemText = Nothing
    Me.xx_descommess.OptionsColumn.AllowEdit = False
    Me.xx_descommess.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descommess.OptionsColumn.ReadOnly = True
    Me.xx_descommess.OptionsFilter.AllowFilter = False
    Me.xx_descommess.Visible = True
    Me.xx_descommess.VisibleIndex = 16
    '
    'xx_righe
    '
    Me.xx_righe.AppearanceCell.Options.UseBackColor = True
    Me.xx_righe.AppearanceCell.Options.UseTextOptions = True
    Me.xx_righe.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_righe.Caption = "N° righe"
    Me.xx_righe.Enabled = False
    Me.xx_righe.FieldName = "xx_righe"
    Me.xx_righe.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_righe.Name = "xx_righe"
    Me.xx_righe.NTSRepositoryComboBox = Nothing
    Me.xx_righe.NTSRepositoryItemCheck = Nothing
    Me.xx_righe.NTSRepositoryItemMemo = Nothing
    Me.xx_righe.NTSRepositoryItemText = Nothing
    Me.xx_righe.OptionsColumn.AllowEdit = False
    Me.xx_righe.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_righe.OptionsColumn.ReadOnly = True
    Me.xx_righe.OptionsFilter.AllowFilter = False
    '
    'xx_righemanc
    '
    Me.xx_righemanc.AppearanceCell.Options.UseBackColor = True
    Me.xx_righemanc.AppearanceCell.Options.UseTextOptions = True
    Me.xx_righemanc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_righemanc.Caption = "Righe mancanti"
    Me.xx_righemanc.Enabled = False
    Me.xx_righemanc.FieldName = "xx_righemanc"
    Me.xx_righemanc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_righemanc.Name = "xx_righemanc"
    Me.xx_righemanc.NTSRepositoryComboBox = Nothing
    Me.xx_righemanc.NTSRepositoryItemCheck = Nothing
    Me.xx_righemanc.NTSRepositoryItemMemo = Nothing
    Me.xx_righemanc.NTSRepositoryItemText = Nothing
    Me.xx_righemanc.OptionsColumn.AllowEdit = False
    Me.xx_righemanc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_righemanc.OptionsColumn.ReadOnly = True
    Me.xx_righemanc.OptionsFilter.AllowFilter = False
    '
    'xx_dest
    '
    Me.xx_dest.AppearanceCell.Options.UseBackColor = True
    Me.xx_dest.AppearanceCell.Options.UseTextOptions = True
    Me.xx_dest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_dest.Caption = "Destinazione"
    Me.xx_dest.Enabled = False
    Me.xx_dest.FieldName = "xx_dest"
    Me.xx_dest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_dest.Name = "xx_dest"
    Me.xx_dest.NTSRepositoryComboBox = Nothing
    Me.xx_dest.NTSRepositoryItemCheck = Nothing
    Me.xx_dest.NTSRepositoryItemMemo = Nothing
    Me.xx_dest.NTSRepositoryItemText = Nothing
    Me.xx_dest.OptionsColumn.AllowEdit = False
    Me.xx_dest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_dest.OptionsColumn.ReadOnly = True
    Me.xx_dest.OptionsFilter.AllowFilter = False
    '
    'xx_desdest
    '
    Me.xx_desdest.AppearanceCell.Options.UseBackColor = True
    Me.xx_desdest.AppearanceCell.Options.UseTextOptions = True
    Me.xx_desdest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_desdest.Caption = "Descr. destinazione"
    Me.xx_desdest.Enabled = False
    Me.xx_desdest.FieldName = "xx_desdest"
    Me.xx_desdest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_desdest.Name = "xx_desdest"
    Me.xx_desdest.NTSRepositoryComboBox = Nothing
    Me.xx_desdest.NTSRepositoryItemCheck = Nothing
    Me.xx_desdest.NTSRepositoryItemMemo = Nothing
    Me.xx_desdest.NTSRepositoryItemText = Nothing
    Me.xx_desdest.OptionsColumn.AllowEdit = False
    Me.xx_desdest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_desdest.OptionsColumn.ReadOnly = True
    Me.xx_desdest.OptionsFilter.AllowFilter = False
    '
    'edTotVal
    '
    Me.edTotVal.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTotVal.EditValue = "0"
    Me.edTotVal.Location = New System.Drawing.Point(163, 11)
    Me.edTotVal.Name = "edTotVal"
    Me.edTotVal.NTSDbField = ""
    Me.edTotVal.NTSFormat = "0"
    Me.edTotVal.NTSForzaVisZoom = False
    Me.edTotVal.NTSOldValue = ""
    Me.edTotVal.Properties.Appearance.Options.UseTextOptions = True
    Me.edTotVal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTotVal.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTotVal.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTotVal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTotVal.Properties.MaxLength = 65536
    Me.edTotVal.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTotVal.Size = New System.Drawing.Size(131, 20)
    Me.edTotVal.TabIndex = 7
    '
    'lbTotVal
    '
    Me.lbTotVal.AutoSize = True
    Me.lbTotVal.BackColor = System.Drawing.Color.Transparent
    Me.lbTotVal.Location = New System.Drawing.Point(10, 14)
    Me.lbTotVal.Name = "lbTotVal"
    Me.lbTotVal.NTSDbField = ""
    Me.lbTotVal.Size = New System.Drawing.Size(147, 13)
    Me.lbTotVal.TabIndex = 8
    Me.lbTotVal.Text = "Totale valore merce evadibile"
    Me.lbTotVal.UseMnemonic = False
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.edTotVal)
    Me.pnTop.Controls.Add(Me.lbTotVal)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.Size = New System.Drawing.Size(648, 37)
    Me.pnTop.TabIndex = 9
    Me.pnTop.Text = "pnBottom"
    '
    'pnBottom
    '
    Me.pnBottom.AllowDrop = True
    Me.pnBottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBottom.Appearance.Options.UseBackColor = True
    Me.pnBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBottom.Controls.Add(Me.grGnnp)
    Me.pnBottom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBottom.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnBottom.Location = New System.Drawing.Point(0, 67)
    Me.pnBottom.Name = "pnBottom"
    Me.pnBottom.Size = New System.Drawing.Size(648, 375)
    Me.pnBottom.TabIndex = 10
    Me.pnBottom.Text = "pnBottom"
    '
    'FRMORGNNP
    '
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.pnBottom)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMORGNNP"
    Me.NTSLastControlFocussed = Me.grGnnp
    Me.Text = "GENERAZIONE NOTE DI PRELIEVO DA IMPEGNI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grGnnp, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvGnnp, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTotVal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBottom.ResumeLayout(False)
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNORGNNP", "BEORGNNP", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128607611686875000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleGnnp = CType(oTmp, CLEORGNNP)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNORGNNP", strRemoteServer, strRemotePort)
    AddHandler oCleGnnp.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleGnnp.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbElabora.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
        tlbModProdEvas.GlyphPath = (oApp.ChildImageDir & "\ordini.gif")
        tlbRecordCancella.GlyphPath = (oApp.ChildImageDir & "\recdelete.gif")
        tlbDettEvasImp.GlyphPath = (oApp.ChildImageDir & "\ordini_2.gif")
        tlbDettEvasArt.GlyphPath = (oApp.ChildImageDir & "\doc.gif")
        tlbDettEvasOrd.GlyphPath = (oApp.ChildImageDir & "\doc_2.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvGnnp.NTSSetParam(oMenu, "GENERA ORDINI DA PROP. D'ORDINE")
      xx_seleziona.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128607611685625000, "Seleziona"), "S", "N")
      fd_anno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128607611685781250, "Anno"), "0", 4, 1900, 2099)
      fd_numdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128607611685937500, "Numero"), "0", 1, 0, 999999999)
      fd_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128607611686093750, "Serie"), CLN__STD.SerieMaxLen, False)
      fd_datdoc.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128607611686250000, "Data"), False, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      fd_totdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128607611686406250, "Totale doc."), oApp.FormatImporti, 9, -999999999, 999999999)
      fd_conto.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128607611686562500, "Fornitore"), tabanagra)
      fd_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128607611686718750, "Descr. forn."), 0, True)
      fd_soloasa.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128714060589935327, "Solo a saldo"), "S", "N")
      fd_tdflevas.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128714060590091565, "Evaso a saldo"), "S", "N")
      fd_codpaga.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128714060590247803, "C.Pag."), tabpaga)
      fd_despaga.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128714060590404041, "Des. Pag."), 0, True)
      fd_codtpbf.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128714060590560279, "Tipo bolla/fattura"), tabtpbf)
      fd_destpbf.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128714060590716517, "Des. Tipo bolla/fattura"), 0, True)
      edTotVal.NTSSetParam(oMenu, oApp.Tr(Me, 128727105754201784, "Totale valore merce evadibile"), oApp.FormatImporti)
      grvGnnp.NTSAllowInsert = False
      grvGnnp.NTSAllowDelete = True

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

#Region "Eventi di Form"
  Public Overridable Sub FRMORGNNP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      grGnnp.Visible = False

      '-----------------------------------------------------------------------------------------
      If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCO) Then oCleGnnp.bModExtTCO = True Else oCleGnnp.bModExtTCO = False
      If CBool(oMenu.ModuliDittaDitt(DittaCorrente) And bsModPM) Then oCleGnnp.bModPM = True Else oCleGnnp.bModPM = False

      '-------------------
      'verifico che tabpeve e tabpeac siano compilate
      If Not oCleGnnp.InitExt() Then
        Me.Close()
        Return
      End If

      'Recupero flags di ricalcolo
      oCleGnnp.bRicalcPrez = CBool(NTSCInt(oMenu.GetSettingBus("Bsorgnnp", "OPZIONI", ".", "RicalcolaPrezzi", "0", " ", "0")))
      oCleGnnp.bRicalcScon = CBool(NTSCInt(oMenu.GetSettingBus("Bsorgnnp", "OPZIONI", ".", "RicalcolaSconti", "0", " ", "0")))
      oCleGnnp.bRicalcProv = CBool(NTSCInt(oMenu.GetSettingBus("Bsorgnnp", "OPZIONI", ".", "RicalcolaProvvigioni", "0", " ", "0")))

      tlbRigheQtaPrelevareDivZero.Checked = CBool(oMenu.GetSettingBus("BSORGNNP", "RECENT", ".", "SoloRigheQtaPrenNoZero", "0", " ", "0"))

      'Recupero dell'instid per la tabella TTGEGNNP
      oCleGnnp.lIITTGeGnnp = oMenu.GetTblInstId("TTGEGNNP", False)
      Apri()
      'Recupero dell'instid per la tabella TTMOPERNP
      oCleGnnp.lIITTMoPernp = oMenu.GetTblInstId("TTMOPERNP", False)
      'Recupero dell'instid per la tabella TTDISPNET
      oCleGnnp.lIITTDispNet = oMenu.GetTblInstId("TTDISPNET", False)
      tlbElabora.Enabled = False

      SetStato(0)

      oCleGnnp.SetOpzioniReg()

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      edTotVal.Enabled = False

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMORGNNP_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      oMenu.ResetTblInstId("TTGEGNNP", False, oCleGnnp.lIITTGeGnnp)
      oMenu.ResetTblInstId("TTMOPERNP", False, oCleGnnp.lIITTMoPernp)
      oMenu.ResetTblInstId("TTDISPNET", False, oCleGnnp.lIITTDispNet)
      oMenu.ResetTblInstId("TTPROESEC", False, oCleGnnp.lIIttproesebappo)
      oMenu.ResetTblInstId("TTTASKS", False, oCleGnnp.lIItttasks)
      oMenu.SaveSettingBus("BSORGNNP", "RECENT", ".", "SoloRigheQtaPrenNoZero", NTSCStr(IIf(tlbRigheQtaPrelevareDivZero.Checked = True, "-1", "0")), " ", "NS.", "...", "...")

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMORGNNP_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcGnnp.Dispose()
      dsGnnp.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Dim frmSeic As FRMORSEIC = Nothing
    Dim frmSval As FRM__SVA1 = Nothing
    Dim dsTmp As DataSet = Nothing
    Dim dRes As DialogResult
    Try
      '--------------------------
      'visualizzo la form per la selezione degli estremi del documento da generare
      oCleGnnp.strSeicQuery = ""
      oCleGnnp.nSeicPriorita = 0
      oCleGnnp.nSeicMagaz = 0
      oCleGnnp.bSeicConssoloasa = False
      oCleGnnp.nSeicMaxnumord = 9999

      frmSeic = CType(NTSNewFormModal("FRMORSEIC"), FRMORSEIC)
      frmSeic.Init(oMenu, Nothing, DittaCorrente)
      frmSeic.InitEntity(oCleGnnp)
      frmSeic.ShowDialog()
      If frmSeic.bOk = False Then Return

      If oCleGnnp.strSeicQuery = "" Or oCleGnnp.nSeicMagaz = 0 Or oCleGnnp.nSeicPriorita = 0 Then Return

      Me.Cursor = Cursors.WaitCursor

      oMenu.ResetTblInstId("TTGEGNNP", False, oCleGnnp.lIITTGeGnnp)
      oMenu.ResetTblInstId("TTMOPERNP", False, oCleGnnp.lIITTMoPernp)
      oMenu.ResetTblInstId("TTDISPNET", False, oCleGnnp.lIITTDispNet)
      oMenu.ResetTblInstId("TTPROESEC", False, oCleGnnp.lIIttproesebappo)
      oMenu.ResetTblInstId("TTTASKS", False, oCleGnnp.lIItttasks)

      '-----------------------------------------------------------------------------------------
      '--- Adesso seleziona movord che soddisfano i creteri e assegna
      '-----------------------------------------------------------------------------------------
      If Not oCleGnnp.SelezionaMovord Then Return
      '-----------------------------------------------------------------------------------------
      If Not oCleGnnp.AssegnaMovord(False) Then Return
      '-----------------------------------------------------------------------------------------
      '--- Adesso visualizza le righe di testord relative ai record di movord evadibili, nella griglia ...
      '-----------------------------------------------------------------------------------------
      oCleGnnp.NuovoInsertTTGegnnp()
      '-----------------------------------------------------------------------------------------
      '--- Adesso aggiorna campo tdflevas su gegnnp ed eventualmente cancella quelle non a saldo se conssoloasa = 'S'
      '-----------------------------------------------------------------------------------------
      oCleGnnp.SettaOrdiniNonASaldo()
      '-----------------------------------------------------------------------------------------
      Apri()
      '-----------------------------------------------------------------------------------------
      '--- Chiede e cancella ordini non a saldo
      '-----------------------------------------------------------------------------------------
      If oCleGnnp.bSeicConssoloasa = True Then EliminaOrdiniNonasaldo(True) ' esegue anche il refresh di gliglia
      '-----------------------------------------------------------------------------------------
      If oCleGnnp.nSeicMaxnumord <> 9999 Then EliminaOrdiniOltremax(True)
      '-----------------------------------------------------------------------------------------
      '--- Chiede se vuio eliminare ordini a valore 0
      '-----------------------------------------------------------------------------------------
      EliminaOrdiniAvalorezero(True)
      '-----------------------------------------------------------------------------------------
      '--- A questo punto però se consoloasa dovrebbe ricalcolare tutto .... come elabora (filenuovo con true) ...
      '-----------------------------------------------------------------------------------------
      oCleGnnp.bFileRielabora = False
      dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128723665683743822, "Vuoi riassegnare ora eventuali disponibilità liberate?"))
      If dRes = System.Windows.Forms.DialogResult.Yes Then
        FileRielabora(True) 'il true sta per chiamato da filenuovo
        oCleGnnp.bFileRielabora = True
      End If
      '-----------------------------------------------------------------------------------------
      '--- Scrive il log con i dati trovati
      '-----------------------------------------------------------------------------------------
      oCleGnnp.ScriviLogSelezioneDati(dsGnnp.Tables("TTGEGNNP"))
      '-----------------------------------------------------------------------------------------
      '--- Le routine sopra indicate fanno loro il refresh di data control e di griglia
      '-----------------------------------------------------------------------------------------
      If dsGnnp.Tables("TTGEGNNP").Rows.Count = 0 Then Return
      '-----------------------------------------------------------------------------------------
      SetStato(1)
      '-----------------------------------------------------------------------------------------

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmSeic Is Nothing Then frmSeic.Dispose()
      frmSeic = Nothing
      If Not frmSval Is Nothing Then frmSval.Dispose()
      frmSval = Nothing
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      SetStato(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbElabora_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbElabora.ItemClick
    Try
      FileRielabora(False) ' non chiamato da altra parte
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbModProdEvas_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbModProdEvas.ItemClick
    Try
      FileModifica()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRecordCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordCancella.ItemClick
    Dim nAnno As Integer = NTSCInt(grvGnnp.NTSGetCurrentDataRow!fd_anno)
    Dim lNumord As Integer = NTSCInt(grvGnnp.NTSGetCurrentDataRow!fd_numdoc)
    Dim strSerie As String = NTSCStr(grvGnnp.NTSGetCurrentDataRow!fd_serie)

    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not grvGnnp.NTSDeleteRigaCorrente(dcGnnp, True) Then Return
      '--------------------------------------------------------------------------------------------------------------
      oCleGnnp.Salva(True)
      oCleGnnp.CancellaRecordTTMOPERNP(nAnno, strSerie, lNumord)
      '--------------------------------------------------------------------------------------------------------------
      CalcolaValoreMerce()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDettEvasImp_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDettEvasImp.ItemClick
    Try
      RecordDettaglio()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDettEvasArt_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDettEvasArt.ItemClick
    Try
      ReportPropostaVai("A")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDettEvasOrd_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDettEvasOrd.ItemClick
    Try
      ReportPropostaVai("O")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      If grvGnnp.RowCount = 0 Then Return
      Salva()
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      If grvGnnp.RowCount = 0 Then Return
      Salva()
      Stampa(0)
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

  Public Overridable Sub tlbEliminaOrdNonASaldo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEliminaOrdNonASaldo.ItemClick
    Try
      EliminaOrdiniNonasaldo(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbEliminaOrdValoreZero_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEliminaOrdValoreZero.ItemClick
    Try
      EliminaOrdiniAvalorezero(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbEliminaOrdInEccessoMax_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEliminaOrdInEccessoMax.ItemClick
    Try
      EliminaOrdiniOltremax(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancellaRigheSelezionate_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancellaRigheSelezionate.ItemClick
    Try
      If dsGnnp.Tables("TTGEGNNP").Rows.Count = 0 Then Return

      Salva()

      oCleGnnp.CancellaRigheSelezionate()

      Apri()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancellaRigheNonSelezionate_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancellaRigheNonSelezionate.ItemClick
    Try
      If dsGnnp.Tables("TTGEGNNP").Rows.Count = 0 Then Return

      Salva()

      oCleGnnp.CancellaRigheNonSelezionate()

      Apri()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSelezionaRighe_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSelezionaRighe.ItemClick
    Dim i As Integer
    Try
      For i = 0 To dsGnnp.Tables("TTGEGNNP").Rows.Count - 1
        dsGnnp.Tables("TTGEGNNP").Rows(i)!xx_seleziona = "S"
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDeselezionaRighe_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDeselezionaRighe.ItemClick
    Dim i As Integer
    Try
      For i = 0 To dsGnnp.Tables("TTGEGNNP").Rows.Count - 1
        dsGnnp.Tables("TTGEGNNP").Rows(i)!xx_seleziona = "N"
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub
#End Region

  Public Overridable Sub grvGnnp_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvGnnp.NTSBeforeRowUpdate
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

  Public Overridable Function Salva() As Boolean
    Try
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvGnnp.NTSSalvaRigaCorrente(dcGnnp, oCleGnnp.RecordIsChanged, False)

      '-------------------------------------------------
      'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
      If GctlControllaOutNotEqual() = False Then Return False

      If Not oCleGnnp.Salva(False) Then
        Return False
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object = Nothing
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer
    Dim dtrT() As DataRow = Nothing
    Dim nGiro As Integer = 0
    Dim strTmp As String = ""
    Dim frmDtac As FRMVEDTA1 = Nothing
    Dim dttTmp As New DataTable
    Dim strkey2 As String = ""
    Dim strNomeReport As String = ""
    Dim strMsg As String = ""
    Dim lGiaFat As Integer
    Dim frmSval As FRM__SVA1 = Nothing
    Dim strNomeFile As String = ""
    Try
      '--------------------------------------------------
      'test pre-stampa
      If dsGnnp.Tables.Count = 0 Then Return
      If dsGnnp.Tables("TTGEGNNP").Rows.Count = 0 Then Return
      dsGnnp.Tables("TTGEGNNP").AcceptChanges()
      dtrT = dsGnnp.Tables("TTGEGNNP").Select("xx_seleziona = 'S'")
      If dtrT.Length = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128611101258125000, "Non è presente alcun impegno da evadere."))
        Return
      End If

      oCleGnnp.lDtacNumdoc = 0
      oCleGnnp.strDtacTipork = "W"

      frmDtac = CType(NTSNewFormModal("FRMVEDTA1"), FRMVEDTA1)
      frmDtac.Init(oMenu, Nothing, DittaCorrente)
      frmDtac.InitEntity(oCleGnnp)
      frmDtac.ShowDialog()
      If frmDtac.bOk = False Then
        Return
      End If

      Select Case oCleGnnp.strGndtTipork
        Case "R"
          If Not (oCleGnnp.strDtacTipork = "W" Or oCleGnnp.strDtacTipork = "B" Or oCleGnnp.strDtacTipork = "A" Or oCleGnnp.strDtacTipork = "C") Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128729634142482295, "Il tipo documento da generare non è ammesso"))
            Return
          End If
        Case "X"
          If Not (oCleGnnp.strDtacTipork = "W" Or oCleGnnp.strDtacTipork = "B") Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128729638271319933, "Il tipo documento da generare non è ammesso"))
            Return
          End If
        Case "Y"
          If Not (oCleGnnp.strDtacTipork = "W" Or oCleGnnp.strDtacTipork = "B" Or oCleGnnp.strDtacTipork = "Z") Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128729638441040317, "Il tipo documento da generare non è ammesso"))
            Return
          End If
      End Select

      '----------------------
      'Chiede come operare con le valute
      frmSval = CType(NTSNewFormModal("FRM__SVA1"), FRM__SVA1)
      frmSval.Init(oMenu, Nothing, DittaCorrente, Nothing)
      frmSval.opValuta0.Checked = False
      frmSval.opValuta1.Checked = True
      frmSval.ShowDialog()
      oCleGnnp.nSvalOpzione = frmSval.nOptionOut

      'elimino quelle non checcate novità net
      dtrT = dsGnnp.Tables("TTGEGNNP").Select("xx_seleziona = 'N'")
      For i = 0 To dtrT.Length - 1
        oCleGnnp.DeleteTTMopernp(dtrT(i))
        dtrT(i).Delete()
      Next
      oCleGnnp.Salva(True)

      oCleGnnp.lANumero = 0
      oCleGnnp.nNumFat = 0

      oCleGnnp.GetTestateTemp(dttTmp)

      If dttTmp.Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128733082973031122, "Attenzione! Non ci sono ordini da cui generare documenti. Impossibile continuare."))
        Return
      End If

      oCleGnnp.bUltimaNotaGenerataPick = False
      oCleGnnp.lIdpick = 0
      oCleGnnp.bAggiungiPick = False 'la prima nota sovrascrive il file esistente

      If Not oCleGnnp.LogStart("BNORGNNP", "GENERAZIONE NOTE DI PRELIEVO DA IMPEGNI") Then Exit Sub

      For i = 0 To dttTmp.Rows.Count - 1
        With dttTmp.Rows(i)
          If Not oCleGnnp.VerificaOrdineInUso(NTSCStr(!mn_tipork), NTSCInt(!mn_anno), NTSCStr(!mn_serie), NTSCInt(!mn_numord)) Then dttTmp.Rows(i).Delete()
        End With
      Next

      dttTmp.AcceptChanges()

      For i = 0 To dttTmp.Rows.Count - 1
        oCleGnnp.LogWrite(oApp.Tr(Me, 128843566209814000, "Creazione documento |" & (i + 1).ToString & "| di |" & dttTmp.Rows.Count.ToString & "| in corso ..."), False)

        If i = dttTmp.Rows.Count - 1 Then
          oCleGnnp.bUltimaNotaGenerataPick = True
        End If
        oCleGnnp.CreaDocDaImpCli(dttTmp.Rows(i))
        oCleGnnp.bAggiungiPick = True
      Next
      oCleGnnp.LogStop()

      If oCleGnnp.LogError = True Then
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127940796626250000, "Esistono dei messaggi nel file di log. Visualizzare il file?")) = Windows.Forms.DialogResult.Yes Then
          NTSProcessStart("notepad", oCleGnnp.LogFileName)
        End If
      End If

      ' Se il documento non ha dati non lo stampo
      lGiaFat = oCleGnnp.nNumFat
      If lGiaFat <> 0 Then
        oCleGnnp.ScriviLogGenerazione()

        If oCleGnnp.bVal Then
          strkey2 = "Reports3"
        ElseIf oCleGnnp.bScorp Then
          strkey2 = "Reports2"
        Else
          strkey2 = "Reports1"
        End If
        'Preimposta il nome del report da stampare
        Select Case oCleGnnp.strDtacTipork
          Case "A", "C"
            If oCleGnnp.bVal Then
              strNomeReport = "BSVEFATV.RPT"
            ElseIf oCleGnnp.bScorp Then
              strNomeReport = "BSVEFATC.RPT"
            Else
              strNomeReport = "BSVEFATI.RPT"
            End If
          Case "B", "Z"
            If oCleGnnp.bVal Then
              strNomeReport = "BSVEBOLV.RPT"
            ElseIf oCleGnnp.bScorp Then
              strNomeReport = "BSVEBOLC.RPT"
            Else
              strNomeReport = "BSVEBOLL.RPT"
            End If
          Case "W"
            If oCleGnnp.bVal Then
              strNomeReport = "BSVEPRBV.RPT"
            ElseIf oCleGnnp.bScorp Then
              strNomeReport = "BSVEPRBC.RPT"
            Else
              strNomeReport = "BSVEPRBN.RPT"
            End If
        End Select
        oCleGnnp.bReprintDoc = CBool(NTSCInt(oMenu.GetSettingBus("BSORGNNP", "Opzioni", ".", "ConfermaRistampa", "0", " ", "0")))
        oCleGnnp.bRistampato = False

        '-----------------------------------------------------------------------------------------
        '--- Torna in stato 0 prima del lancio delle stampe, così, in caso di errore, non si può
        '--- rigenerare i documenti provocando errore di chiave duplicata
        '-----------------------------------------------------------------------------------------
        oMenu.ResetTblInstId("TTGEGNNP", False, oCleGnnp.lIITTGeGnnp)
        oMenu.ResetTblInstId("TTMOPERNP", False, oCleGnnp.lIITTMoPernp)
        oMenu.ResetTblInstId("TTDISPNET", False, oCleGnnp.lIITTDispNet)
        oMenu.ResetTblInstId("TTPROESEC", False, oCleGnnp.lIIttproesebappo)
        oMenu.ResetTblInstId("TTTASKS", False, oCleGnnp.lIItttasks)

Reprint:
        dtrT = dsGnnp.Tables("TTGEGNNP").Select(strTmp)
        If dtrT.Length > 0 Then
          '--------------------------------------------------
          'preparo il motore di stampa      
          strCrpe = "{testmag.codditt} = '" & DittaCorrente & "'" & _
            " And {testmag.tm_tipork} = '" & oCleGnnp.strDtacTipork & "'" & _
            " AND {testmag.tm_anno} = " & oCleGnnp.nDtacAnno & _
            " AND {testmag.tm_serie} = '" & oCleGnnp.strDtacSerie & "'" & _
            " AND {movmag.mm_stasino} <> 'N'"
          strCrpe += " and {testmag.tm_numdoc} In " & oCleGnnp.lDtacNumdoc & " To " & oCleGnnp.lANumero

          nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSVEBOLL", strkey2, oCleGnnp.strDtacTipork, 0, nDestin, strNomeReport, False, "Stampa Ordine", False)
          If Not nPjob Is Nothing Then
            '--------------------------------------------------
            'lancio tutti gli eventuali reports (gestisce già il multireport)
            For i = 1 To UBound(CType(nPjob, Array), 2)
              nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
              nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
            Next
          End If
        End If    'If dtrT.Length > 0 Then

        If oCleGnnp.bReprintDoc And Not oCleGnnp.bRistampato Then
          If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128715674754166396, "Ristampare il documento?")) = Windows.Forms.DialogResult.Yes Then
            oCleGnnp.bRistampato = True
            GoTo Reprint
          End If
        End If
      End If

      strMsg = oApp.Tr(Me, 129053480351298291, "Elaborazione completata.") & vbCrLf
      Select Case lGiaFat 'numero di doc generati
        Case 0
          If oCleGnnp.strDtacTipork = "W" Then strMsg = strMsg & oApp.Tr(Me, 129053480551156110, "Nessuna nota generata.") Else strMsg = strMsg & oApp.Tr(Me, 129053481609509343, "Nessun documento generato.")
        Case 1
          If oCleGnnp.strDtacTipork = "W" Then strMsg = strMsg & oApp.Tr(Me, 129053481811704607, "Una nota generata.") Else strMsg = strMsg & oApp.Tr(Me, 129053481649354623, "Un documento generato.")
        Case Else
          If oCleGnnp.strDtacTipork = "W" Then strMsg = strMsg & NTSCStr(lGiaFat) & oApp.Tr(Me, 129053481779047103, " note generate.") Else strMsg = strMsg & NTSCStr(lGiaFat) & oApp.Tr(Me, 129053481706075551, "documenti generati.")
      End Select
      If (oCleGnnp.strDtacTipork = "W") And (oCleGnnp.bDtacCreaPicking = True) And (oCleGnnp.bDtacPickingDistinti = False) Then
        If lGiaFat <> 0 Then
          strMsg = strMsg & vbCrLf & _
                    oApp.Tr(Me, 129053481967335583, "Creato ID pick ") & oCleGnnp.lIdpick
        End If
      End If
      oApp.MsgBoxInfo(strMsg)

      SetStato(0)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmDtac Is Nothing Then frmDtac.Dispose()
      frmDtac = Nothing
      If Not frmSval Is Nothing Then frmSval.Dispose()
      frmSval = Nothing
      Me.Cursor = Cursors.Default
      oCleGnnp.LogStop()
    End Try
  End Sub

  Public Overridable Function Apri() As Boolean
    Try
      oCleGnnp.Apri(dsGnnp)

      CalcolaValoreMerce()

      ''-------------------------------------------------
      ''leggo dal database i dati e collego il NTSBindingNavigator
      dcGnnp.DataSource = dsGnnp.Tables("TTGEGNNP")
      dsGnnp.AcceptChanges()
      grGnnp.DataSource = dcGnnp

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function EliminaOrdiniNonasaldo(ByVal bChiediconf As Boolean) As Boolean
    Try
      If Not oCleGnnp.EliminaOrdiniNonasaldo(bChiediconf) Then Return False

      Apri()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function EliminaOrdiniOltremax(ByVal bChiediconf As Boolean) As Boolean
    Try
      If Not oCleGnnp.EliminaOrdiniOltremax(bChiediconf) Then Return False

      Apri()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function EliminaOrdiniAvalorezero(ByVal bChiediconf As Boolean) As Boolean
    Try
      If Not oCleGnnp.EliminaOrdiniAvalorezero(bChiediconf) Then Return False

      Apri()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function FileRielabora(ByVal bChiamato As Boolean) As Boolean
    Try
      If Not oCleGnnp.AssegnaMovord(True) Then Return False

      If Not oCleGnnp.FileRielabora(bChiamato) Then Return False

      oCleGnnp.SettaOrdiniNonASaldo()

      Apri()

      If dsGnnp.Tables("TTGEGNNP").Rows.Count = 0 Then
        Return False
      Else
        SetStato(1)
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function FileModifica() As Boolean
    Dim fmGnda As FRMORGNDA = Nothing
    Try
      ' chiama la dialog di distribuzione delle giacenze nette per articolo
      ' con possibilità di modfica quantitativi
      If dsGnnp.Tables("TTGEGNNP").Rows.Count = 0 Then Return False

      fmGnda = CType(NTSNewFormModal("FRMORGNDA"), FRMORGNDA)
      fmGnda.Init(oMenu, Nothing, DittaCorrente)
      fmGnda.InitEntity(oCleGnnp)
      fmGnda.ShowDialog()

      oCleGnnp.FileModifica(dsGnnp)

      ' a questo punto alcuni ordini potrebbero essere a saldo
      oCleGnnp.SettaOrdiniNonASaldo()

      Apri()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not fmGnda Is Nothing Then fmGnda.Dispose()
      fmGnda = Nothing
    End Try
  End Function

  Public Overridable Function RecordDettaglio() As Boolean
    Dim fmGndt As FRMORGNDT = Nothing
    Try
      If dsGnnp.Tables("TTGEGNNP").Rows.Count = 0 Then Return False

      oCleGnnp.nGndtAnno = NTSCInt(dsGnnp.Tables("TTGEGNNP").Rows(dcGnnp.Position)!fd_anno)
      oCleGnnp.strGndtSerie = NTSCStr(dsGnnp.Tables("TTGEGNNP").Rows(dcGnnp.Position)!fd_serie)
      oCleGnnp.lGndtNumdoc = NTSCInt(dsGnnp.Tables("TTGEGNNP").Rows(dcGnnp.Position)!fd_numdoc)
      oCleGnnp.strGndtCodart = ""

      fmGndt = CType(NTSNewFormModal("FRMORGNDT"), FRMORGNDT)
      fmGndt.Init(oMenu, Nothing, DittaCorrente)
      fmGndt.InitEntity(oCleGnnp)
      fmGndt.ShowDialog()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not fmGndt Is Nothing Then fmGndt.Dispose()
      fmGndt = Nothing
    End Try
  End Function

  Public Overridable Function ReportPropostaVai(ByVal strTipo As String) As Boolean
    Dim nPjob As Object = Nothing
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim strKey As String = ""
    Dim strNomeReport As String = ""
    Dim strTitle As String = ""
    Dim i As Integer
    Try
      '-----------------------------------------------------------------------------------------
      Me.Cursor = Cursors.WaitCursor
      '-----------------------------------------------------------------------------------------
      '--- se non ci sono dati avvisa ed esce
      '-----------------------------------------------------------------------------------------
      If dsGnnp.Tables("TTGEGNNP").Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128725944687020980, "Non è presente alcun impegno da evadere."))
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      '--- Setta cartella, nome report, SelectionFormula e titolo report
      '-----------------------------------------------------------------------------------------
      Select Case strTipo
        Case "A"
          strKey = "Reports1"
          strNomeReport = "BSORPEPA.RPT"
          strCrpe = "{TTMOPERNP.instid} = " & oCleGnnp.lIITTMoPernp & _
            " AND {TTDISPNET.instid} = " & oCleGnnp.lIITTDispNet
          If tlbRigheQtaPrelevareDivZero.Checked = True Then
            strCrpe = strCrpe & " AND {TTMOPERNP.mn_mmquant} <> 0"
          End If
          strTitle = "Stampa proposta evasione per articolo"
        Case "O"
          strKey = "Reports2"
          strNomeReport = "BSORPEPO.RPT"
          strCrpe = "{TTMOPERNP.instid} = " & oCleGnnp.lIITTMoPernp & _
            " AND {TTDISPNET.instid} = " & oCleGnnp.lIITTDispNet
          If tlbRigheQtaPrelevareDivZero.Checked = True Then
            strCrpe = strCrpe & " AND {TTMOPERNP.mn_mmquant} <> 0"
          End If
          strTitle = "Stampa proposta evasione per conto"
      End Select

      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSORGNNP", strKey, " ", 0, 0, strNomeReport, False, strTitle, False)
      If nPjob Is Nothing Then Return False

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce già il multireport)
      For i = 1 To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Function

  Public Overridable Sub SetStato(ByVal nSetStato As Integer)
    Try
      If nSetStato = 0 Then
        tlbElabora.Enabled = False
        tlbRipristina.Enabled = False
        tlbNuovo.Enabled = True
        tlbRecordCancella.Enabled = False
        tlbDettEvasImp.Enabled = False
        tlbModProdEvas.Enabled = False
        tlbDettEvasArt.Enabled = False
        tlbDettEvasOrd.Enabled = False
        tlbStampa.Enabled = False
        tlbStampaVideo.Enabled = False

        tlbCancellaRigheSelezionate.Enabled = False
        tlbCancellaRigheNonSelezionate.Enabled = False
        tlbSelezionaRighe.Enabled = False
        tlbDeselezionaRighe.Enabled = False
        tlbImpostaStampante.Enabled = False
        tlbRigheQtaPrelevareDivZero.Enabled = False
        tlbEliminaOrdValoreZero.Enabled = False
        tlbEliminaOrdNonASaldo.Enabled = False
        tlbEliminaOrdInEccessoMax.Enabled = False
        grGnnp.Visible = False
        edTotVal.Text = "0"
        pnTop.Visible = False
      Else
        GctlSetVisEnab(tlbElabora, False)
        GctlSetVisEnab(tlbRipristina, False)
        tlbNuovo.Enabled = False
        GctlSetVisEnab(tlbRecordCancella, False)
        GctlSetVisEnab(tlbDettEvasImp, False)
        GctlSetVisEnab(tlbModProdEvas, False)
        GctlSetVisEnab(tlbDettEvasArt, False)
        GctlSetVisEnab(tlbDettEvasOrd, False)
        GctlSetVisEnab(tlbStampa, False)
        GctlSetVisEnab(tlbStampaVideo, False)

        GctlSetVisEnab(tlbCancellaRigheSelezionate, False)
        GctlSetVisEnab(tlbCancellaRigheNonSelezionate, False)
        GctlSetVisEnab(tlbSelezionaRighe, False)
        GctlSetVisEnab(tlbDeselezionaRighe, False)
        GctlSetVisEnab(tlbImpostaStampante, False)
        GctlSetVisEnab(tlbRigheQtaPrelevareDivZero, False)
        GctlSetVisEnab(tlbEliminaOrdValoreZero, False)
        GctlSetVisEnab(tlbEliminaOrdNonASaldo, False)
        GctlSetVisEnab(tlbEliminaOrdInEccessoMax, False)
        grGnnp.Visible = True
        pnTop.Visible = True
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub CalcolaValoreMerce()
    Dim dsTmp As DataSet = Nothing
    Try
      oCleGnnp.CalcolaValoreMerce(dsTmp)

      If dsTmp.Tables("TTGEGNNP").Rows.Count = 0 Then
        edTotVal.Text = "0"
      Else
        If NTSCStr(dsTmp.Tables("TTGEGNNP").Rows(0)!Valore) = "" Then
          edTotVal.Text = "0"
        Else
          edTotVal.Text = NTSCStr(dsTmp.Tables("TTGEGNNP").Rows(0)!Valore)
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class
