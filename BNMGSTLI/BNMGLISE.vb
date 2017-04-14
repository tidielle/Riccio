Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGLISE
  Public oCallParams As CLE__CLDP
  Public oCleStli As CLEMGSTLI
  Public dsLise As New DataSet
  Public dcLise As New BindingSource
  Public dttUm As New DataTable

  Public bPromo1, bPromo2, bPromo3, bPromo4, bPromo5 As Boolean
  Public bValuta1, bValuta2, bValuta3, bValuta4, bValuta5 As Boolean
  Public bLavo1, bLavo2, bLavo3, bLavo4, bLavo5 As Boolean
  Public bPromoSc1, bPromoSc2, bPromoSc3, bPromoSc4 As Boolean
  Public bScont4, bScont5, bScont6 As Boolean
  Public bScont4_2, bScont5_2, bScont6_2 As Boolean
  Public bScont4_3, bScont5_3, bScont6_3 As Boolean
  Public bScont4_4, bScont5_4, bScont6_4 As Boolean
  Public bDaQuant1, bDaQuant2, bDaQuant3, bDaQuant4, bDaQuant5, bDaQuant, bDaQuant_2, bDaQuant_3, bDaQuant_4 As Boolean
  Public bAQuant1, bAQuant2, bAQuant3, bAQuant4, bAQuant5, bAQuant, bAQuant_2, bAQuant_3, bAQuant_4 As Boolean
  

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGLISE))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbElabora = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbCancellaTutto = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnFiltroColonna = New NTSInformatica.NTSPanel
    Me.ckColoreNero = New NTSInformatica.NTSCheckBox
    Me.fmFiltriColonna = New NTSInformatica.NTSGroupBox
    Me.ckSconti4 = New NTSInformatica.NTSCheckBox
    Me.ckSconti3 = New NTSInformatica.NTSCheckBox
    Me.ckSconti2 = New NTSInformatica.NTSCheckBox
    Me.ckSconti1 = New NTSInformatica.NTSCheckBox
    Me.ckListino5 = New NTSInformatica.NTSCheckBox
    Me.ckListino4 = New NTSInformatica.NTSCheckBox
    Me.ckListino3 = New NTSInformatica.NTSCheckBox
    Me.ckListino2 = New NTSInformatica.NTSCheckBox
    Me.ckListino1 = New NTSInformatica.NTSCheckBox
    Me.ckBlocca = New NTSInformatica.NTSCheckBox
    Me.pnGriglia = New NTSInformatica.NTSPanel
    Me.grList = New NTSInformatica.NTSGrid
    Me.grvList = New NTSInformatica.NTSGridView
    Me.ls_conto = New NTSInformatica.NTSGridColumn
    Me.xx_desccli = New NTSInformatica.NTSGridColumn
    Me.ls_codart = New NTSInformatica.NTSGridColumn
    Me.xx_descart = New NTSInformatica.NTSGridColumn
    Me.ls_fase = New NTSInformatica.NTSGridColumn
    Me.xx_fase = New NTSInformatica.NTSGridColumn
    Me.ls_perqta = New NTSInformatica.NTSGridColumn
    Me.ls_listino1 = New NTSInformatica.NTSGridColumn
    Me.ls_prz1 = New NTSInformatica.NTSGridColumn
    Me.ls_daquant1 = New NTSInformatica.NTSGridColumn
    Me.ls_aquant1 = New NTSInformatica.NTSGridColumn
    Me.ls_unmis1 = New NTSInformatica.NTSGridColumn
    Me.ls_codlavo1 = New NTSInformatica.NTSGridColumn
    Me.xx_desclavo1 = New NTSInformatica.NTSGridColumn
    Me.ls_tipo1 = New NTSInformatica.NTSGridColumn
    Me.ls_dadata1 = New NTSInformatica.NTSGridColumn
    Me.ls_adata1 = New NTSInformatica.NTSGridColumn
    Me.ls_codvalu1 = New NTSInformatica.NTSGridColumn
    Me.xx_descvalu1 = New NTSInformatica.NTSGridColumn
    Me.ls_codpromo1 = New NTSInformatica.NTSGridColumn
    Me.xx_descpromo1 = New NTSInformatica.NTSGridColumn
    Me.ls_prznet1 = New NTSInformatica.NTSGridColumn
    Me.ls_listino2 = New NTSInformatica.NTSGridColumn
    Me.ls_prz2 = New NTSInformatica.NTSGridColumn
    Me.ls_daquant2 = New NTSInformatica.NTSGridColumn
    Me.ls_aquant2 = New NTSInformatica.NTSGridColumn
    Me.ls_unmis2 = New NTSInformatica.NTSGridColumn
    Me.ls_codlavo2 = New NTSInformatica.NTSGridColumn
    Me.xx_desclavo2 = New NTSInformatica.NTSGridColumn
    Me.ls_tipo2 = New NTSInformatica.NTSGridColumn
    Me.ls_dadata2 = New NTSInformatica.NTSGridColumn
    Me.ls_adata2 = New NTSInformatica.NTSGridColumn
    Me.ls_codvalu2 = New NTSInformatica.NTSGridColumn
    Me.xx_descvalu2 = New NTSInformatica.NTSGridColumn
    Me.ls_codpromo2 = New NTSInformatica.NTSGridColumn
    Me.xx_descpromo2 = New NTSInformatica.NTSGridColumn
    Me.ls_prznet2 = New NTSInformatica.NTSGridColumn
    Me.ls_listino3 = New NTSInformatica.NTSGridColumn
    Me.ls_prz3 = New NTSInformatica.NTSGridColumn
    Me.ls_daquant3 = New NTSInformatica.NTSGridColumn
    Me.ls_aquant3 = New NTSInformatica.NTSGridColumn
    Me.ls_unmis3 = New NTSInformatica.NTSGridColumn
    Me.ls_codlavo3 = New NTSInformatica.NTSGridColumn
    Me.xx_desclavo3 = New NTSInformatica.NTSGridColumn
    Me.ls_tipo3 = New NTSInformatica.NTSGridColumn
    Me.ls_dadata3 = New NTSInformatica.NTSGridColumn
    Me.ls_adata3 = New NTSInformatica.NTSGridColumn
    Me.ls_codvalu3 = New NTSInformatica.NTSGridColumn
    Me.xx_descvalu3 = New NTSInformatica.NTSGridColumn
    Me.ls_codpromo3 = New NTSInformatica.NTSGridColumn
    Me.xx_descpromo3 = New NTSInformatica.NTSGridColumn
    Me.ls_prznet3 = New NTSInformatica.NTSGridColumn
    Me.ls_listino4 = New NTSInformatica.NTSGridColumn
    Me.ls_prz4 = New NTSInformatica.NTSGridColumn
    Me.ls_daquant4 = New NTSInformatica.NTSGridColumn
    Me.ls_aquant4 = New NTSInformatica.NTSGridColumn
    Me.ls_unmis4 = New NTSInformatica.NTSGridColumn
    Me.ls_codlavo4 = New NTSInformatica.NTSGridColumn
    Me.xx_desclavo4 = New NTSInformatica.NTSGridColumn
    Me.ls_tipo4 = New NTSInformatica.NTSGridColumn
    Me.ls_dadata4 = New NTSInformatica.NTSGridColumn
    Me.ls_adata4 = New NTSInformatica.NTSGridColumn
    Me.ls_codvalu4 = New NTSInformatica.NTSGridColumn
    Me.xx_descvalu4 = New NTSInformatica.NTSGridColumn
    Me.ls_codpromo4 = New NTSInformatica.NTSGridColumn
    Me.xx_descpromo4 = New NTSInformatica.NTSGridColumn
    Me.ls_prznet4 = New NTSInformatica.NTSGridColumn
    Me.ls_listino5 = New NTSInformatica.NTSGridColumn
    Me.ls_prz5 = New NTSInformatica.NTSGridColumn
    Me.ls_daquant5 = New NTSInformatica.NTSGridColumn
    Me.ls_aquant5 = New NTSInformatica.NTSGridColumn
    Me.ls_unmis5 = New NTSInformatica.NTSGridColumn
    Me.ls_codlavo5 = New NTSInformatica.NTSGridColumn
    Me.xx_desclavo5 = New NTSInformatica.NTSGridColumn
    Me.ls_tipo5 = New NTSInformatica.NTSGridColumn
    Me.ls_dadata5 = New NTSInformatica.NTSGridColumn
    Me.ls_adata5 = New NTSInformatica.NTSGridColumn
    Me.ls_codvalu5 = New NTSInformatica.NTSGridColumn
    Me.xx_descvalu5 = New NTSInformatica.NTSGridColumn
    Me.ls_codpromo5 = New NTSInformatica.NTSGridColumn
    Me.xx_descpromo5 = New NTSInformatica.NTSGridColumn
    Me.ls_prznet5 = New NTSInformatica.NTSGridColumn
    Me.ls_scont1 = New NTSInformatica.NTSGridColumn
    Me.ls_scont2 = New NTSInformatica.NTSGridColumn
    Me.ls_scont3 = New NTSInformatica.NTSGridColumn
    Me.ls_scont4 = New NTSInformatica.NTSGridColumn
    Me.ls_scont5 = New NTSInformatica.NTSGridColumn
    Me.ls_scont6 = New NTSInformatica.NTSGridColumn
    Me.ls_scdaquant = New NTSInformatica.NTSGridColumn
    Me.ls_scaquant = New NTSInformatica.NTSGridColumn
    Me.ls_tiposc = New NTSInformatica.NTSGridColumn
    Me.ls_dadatasc = New NTSInformatica.NTSGridColumn
    Me.ls_adatasc = New NTSInformatica.NTSGridColumn
    Me.ls_codtpro = New NTSInformatica.NTSGridColumn
    Me.xx_descpromo = New NTSInformatica.NTSGridColumn
    Me.ls_scont1_2 = New NTSInformatica.NTSGridColumn
    Me.ls_scont2_2 = New NTSInformatica.NTSGridColumn
    Me.ls_scont3_2 = New NTSInformatica.NTSGridColumn
    Me.ls_scont4_2 = New NTSInformatica.NTSGridColumn
    Me.ls_scont5_2 = New NTSInformatica.NTSGridColumn
    Me.ls_scont6_2 = New NTSInformatica.NTSGridColumn
    Me.ls_scdaquant_2 = New NTSInformatica.NTSGridColumn
    Me.ls_scaquant_2 = New NTSInformatica.NTSGridColumn
    Me.ls_tiposc_2 = New NTSInformatica.NTSGridColumn
    Me.ls_dadatasc_2 = New NTSInformatica.NTSGridColumn
    Me.ls_adatasc_2 = New NTSInformatica.NTSGridColumn
    Me.ls_codtpro_2 = New NTSInformatica.NTSGridColumn
    Me.xx_descpromo_2 = New NTSInformatica.NTSGridColumn
    Me.ls_scont1_3 = New NTSInformatica.NTSGridColumn
    Me.ls_scont2_3 = New NTSInformatica.NTSGridColumn
    Me.ls_scont3_3 = New NTSInformatica.NTSGridColumn
    Me.ls_scont4_3 = New NTSInformatica.NTSGridColumn
    Me.ls_scont5_3 = New NTSInformatica.NTSGridColumn
    Me.ls_scont6_3 = New NTSInformatica.NTSGridColumn
    Me.ls_scdaquant_3 = New NTSInformatica.NTSGridColumn
    Me.ls_scaquant_3 = New NTSInformatica.NTSGridColumn
    Me.ls_tiposc_3 = New NTSInformatica.NTSGridColumn
    Me.ls_dadatasc_3 = New NTSInformatica.NTSGridColumn
    Me.ls_adatasc_3 = New NTSInformatica.NTSGridColumn
    Me.ls_codtpro_3 = New NTSInformatica.NTSGridColumn
    Me.xx_descpromo_3 = New NTSInformatica.NTSGridColumn
    Me.ls_scont1_4 = New NTSInformatica.NTSGridColumn
    Me.ls_scont2_4 = New NTSInformatica.NTSGridColumn
    Me.ls_scont3_4 = New NTSInformatica.NTSGridColumn
    Me.ls_scont4_4 = New NTSInformatica.NTSGridColumn
    Me.ls_scont5_4 = New NTSInformatica.NTSGridColumn
    Me.ls_scont6_4 = New NTSInformatica.NTSGridColumn
    Me.ls_scdaquant_4 = New NTSInformatica.NTSGridColumn
    Me.ls_scaquant_4 = New NTSInformatica.NTSGridColumn
    Me.ls_tiposc_4 = New NTSInformatica.NTSGridColumn
    Me.ls_dadatasc_4 = New NTSInformatica.NTSGridColumn
    Me.ls_adatasc_4 = New NTSInformatica.NTSGridColumn
    Me.ls_codtpro_4 = New NTSInformatica.NTSGridColumn
    Me.xx_descpromo_4 = New NTSInformatica.NTSGridColumn
    Me.ls_clscan = New NTSInformatica.NTSGridColumn
    Me.xx_clscan = New NTSInformatica.NTSGridColumn
    Me.ls_clscar = New NTSInformatica.NTSGridColumn
    Me.xx_clscar = New NTSInformatica.NTSGridColumn
    Me.ar_codart = New NTSInformatica.NTSGridColumn
    Me.xx_desint = New NTSInformatica.NTSGridColumn
    Me.ls_coddest = New NTSInformatica.NTSGridColumn
    Me.xx_coddest = New NTSInformatica.NTSGridColumn
    Me.pnDatiRiga = New NTSInformatica.NTSPanel
    Me.fmDatiRiga = New NTSInformatica.NTSGroupBox
    Me.lbRossoDesc = New NTSInformatica.NTSLabel
    Me.lbRosso = New NTSInformatica.NTSLabel
    Me.lbBDesc = New NTSInformatica.NTSLabel
    Me.lbB = New NTSInformatica.NTSLabel
    Me.lbFase = New NTSInformatica.NTSLabel
    Me.lbArtico = New NTSInformatica.NTSLabel
    Me.lbConto = New NTSInformatica.NTSLabel
    Me.edArtico = New NTSInformatica.NTSTextBoxStr
    Me.edFase = New NTSInformatica.NTSTextBoxNum
    Me.lbDesFase = New NTSInformatica.NTSLabel
    Me.lbDesArtico = New NTSInformatica.NTSLabel
    Me.edConto = New NTSInformatica.NTSTextBoxNum
    Me.lbDesConto = New NTSInformatica.NTSLabel
    Me.tmTick = New System.Windows.Forms.Timer(Me.components)
    Me.tlbVarPrzSc = New NTSInformatica.NTSBarMenuItem
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnFiltroColonna, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFiltroColonna.SuspendLayout()
    CType(Me.ckColoreNero.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmFiltriColonna, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmFiltriColonna.SuspendLayout()
    CType(Me.ckSconti4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSconti3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSconti2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSconti1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckListino5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckListino4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckListino3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckListino2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckListino1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckBlocca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnGriglia, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGriglia.SuspendLayout()
    CType(Me.grList, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvList, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnDatiRiga, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDatiRiga.SuspendLayout()
    CType(Me.fmDatiRiga, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmDatiRiga.SuspendLayout()
    CType(Me.edArtico.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbGuida, Me.tlbEsci, Me.tlbSalva, Me.tlbRipristina, Me.tlbCancella, Me.tlbStrumenti, Me.tlbCancellaTutto, Me.tlbElabora, Me.tlbVarPrzSc})
    Me.NtsBarManager1.MaxItemId = 12
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbElabora), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbElabora
    '
    Me.tlbElabora.Caption = "Aggiorna listini"
    Me.tlbElabora.Glyph = CType(resources.GetObject("tlbElabora.Glyph"), System.Drawing.Image)
    Me.tlbElabora.GlyphPath = ""
    Me.tlbElabora.Id = 7
    Me.tlbElabora.Name = "tlbElabora"
    Me.tlbElabora.Visible = True
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.GlyphPath = ""
    Me.tlbSalva.Id = 2
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.GlyphPath = ""
    Me.tlbRipristina.Id = 3
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.GlyphPath = ""
    Me.tlbCancella.Id = 4
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 5
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancellaTutto, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbVarPrzSc)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbCancellaTutto
    '
    Me.tlbCancellaTutto.Caption = "Elimina stampa su griglia"
    Me.tlbCancellaTutto.GlyphPath = ""
    Me.tlbCancellaTutto.Id = 6
    Me.tlbCancellaTutto.Name = "tlbCancellaTutto"
    Me.tlbCancellaTutto.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Help"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 0
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 1
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnFiltroColonna
    '
    Me.pnFiltroColonna.AllowDrop = True
    Me.pnFiltroColonna.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnFiltroColonna.Appearance.Options.UseBackColor = True
    Me.pnFiltroColonna.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnFiltroColonna.Controls.Add(Me.ckColoreNero)
    Me.pnFiltroColonna.Controls.Add(Me.fmFiltriColonna)
    Me.pnFiltroColonna.Controls.Add(Me.ckBlocca)
    Me.pnFiltroColonna.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnFiltroColonna.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnFiltroColonna.Location = New System.Drawing.Point(0, 438)
    Me.pnFiltroColonna.Name = "pnFiltroColonna"
    Me.pnFiltroColonna.NTSActiveTrasparency = True
    Me.pnFiltroColonna.Size = New System.Drawing.Size(794, 81)
    Me.pnFiltroColonna.TabIndex = 5
    Me.pnFiltroColonna.Text = "NtsPanel2"
    '
    'ckColoreNero
    '
    Me.ckColoreNero.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckColoreNero.Location = New System.Drawing.Point(190, 57)
    Me.ckColoreNero.Name = "ckColoreNero"
    Me.ckColoreNero.NTSCheckValue = "S"
    Me.ckColoreNero.NTSUnCheckValue = "N"
    Me.ckColoreNero.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckColoreNero.Properties.Appearance.Options.UseBackColor = True
    Me.ckColoreNero.Properties.AutoHeight = False
    Me.ckColoreNero.Properties.Caption = "Mostra i dati dei listini\sconti a 0"
    Me.ckColoreNero.Size = New System.Drawing.Size(179, 19)
    Me.ckColoreNero.TabIndex = 10
    '
    'fmFiltriColonna
    '
    Me.fmFiltriColonna.AllowDrop = True
    Me.fmFiltriColonna.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.fmFiltriColonna.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmFiltriColonna.Appearance.Options.UseBackColor = True
    Me.fmFiltriColonna.Controls.Add(Me.ckSconti4)
    Me.fmFiltriColonna.Controls.Add(Me.ckSconti3)
    Me.fmFiltriColonna.Controls.Add(Me.ckSconti2)
    Me.fmFiltriColonna.Controls.Add(Me.ckSconti1)
    Me.fmFiltriColonna.Controls.Add(Me.ckListino5)
    Me.fmFiltriColonna.Controls.Add(Me.ckListino4)
    Me.fmFiltriColonna.Controls.Add(Me.ckListino3)
    Me.fmFiltriColonna.Controls.Add(Me.ckListino2)
    Me.fmFiltriColonna.Controls.Add(Me.ckListino1)
    Me.fmFiltriColonna.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmFiltriColonna.Location = New System.Drawing.Point(12, 3)
    Me.fmFiltriColonna.Name = "fmFiltriColonna"
    Me.fmFiltriColonna.Size = New System.Drawing.Size(770, 50)
    Me.fmFiltriColonna.TabIndex = 1
    Me.fmFiltriColonna.Text = "Visualizza le colonne di:"
    '
    'ckSconti4
    '
    Me.ckSconti4.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSconti4.EditValue = True
    Me.ckSconti4.Location = New System.Drawing.Point(694, 24)
    Me.ckSconti4.Name = "ckSconti4"
    Me.ckSconti4.NTSCheckValue = "S"
    Me.ckSconti4.NTSUnCheckValue = "N"
    Me.ckSconti4.Properties.Appearance.BackColor = System.Drawing.Color.Moccasin
    Me.ckSconti4.Properties.Appearance.Options.UseBackColor = True
    Me.ckSconti4.Properties.AutoHeight = False
    Me.ckSconti4.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
    Me.ckSconti4.Properties.Caption = "Sconto 4"
    Me.ckSconti4.Size = New System.Drawing.Size(70, 21)
    Me.ckSconti4.TabIndex = 8
    '
    'ckSconti3
    '
    Me.ckSconti3.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.ckSconti3.EditValue = True
    Me.ckSconti3.Location = New System.Drawing.Point(608, 24)
    Me.ckSconti3.Name = "ckSconti3"
    Me.ckSconti3.NTSCheckValue = "S"
    Me.ckSconti3.NTSUnCheckValue = "N"
    Me.ckSconti3.Properties.Appearance.BackColor = System.Drawing.Color.Khaki
    Me.ckSconti3.Properties.Appearance.Options.UseBackColor = True
    Me.ckSconti3.Properties.AutoHeight = False
    Me.ckSconti3.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
    Me.ckSconti3.Properties.Caption = "Sconto 3"
    Me.ckSconti3.Size = New System.Drawing.Size(70, 21)
    Me.ckSconti3.TabIndex = 7
    '
    'ckSconti2
    '
    Me.ckSconti2.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSconti2.EditValue = True
    Me.ckSconti2.Location = New System.Drawing.Point(522, 24)
    Me.ckSconti2.Name = "ckSconti2"
    Me.ckSconti2.NTSCheckValue = "S"
    Me.ckSconti2.NTSUnCheckValue = "N"
    Me.ckSconti2.Properties.Appearance.BackColor = System.Drawing.Color.Gold
    Me.ckSconti2.Properties.Appearance.Options.UseBackColor = True
    Me.ckSconti2.Properties.AutoHeight = False
    Me.ckSconti2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
    Me.ckSconti2.Properties.Caption = "Sconto 2"
    Me.ckSconti2.Size = New System.Drawing.Size(70, 21)
    Me.ckSconti2.TabIndex = 6
    '
    'ckSconti1
    '
    Me.ckSconti1.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSconti1.EditValue = True
    Me.ckSconti1.Location = New System.Drawing.Point(436, 24)
    Me.ckSconti1.Name = "ckSconti1"
    Me.ckSconti1.NTSCheckValue = "S"
    Me.ckSconti1.NTSUnCheckValue = "N"
    Me.ckSconti1.Properties.Appearance.BackColor = System.Drawing.Color.Yellow
    Me.ckSconti1.Properties.Appearance.Options.UseBackColor = True
    Me.ckSconti1.Properties.AutoHeight = False
    Me.ckSconti1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
    Me.ckSconti1.Properties.Caption = "Sconto 1"
    Me.ckSconti1.Size = New System.Drawing.Size(70, 21)
    Me.ckSconti1.TabIndex = 5
    '
    'ckListino5
    '
    Me.ckListino5.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckListino5.EditValue = True
    Me.ckListino5.Location = New System.Drawing.Point(350, 24)
    Me.ckListino5.Name = "ckListino5"
    Me.ckListino5.NTSCheckValue = "S"
    Me.ckListino5.NTSUnCheckValue = "N"
    Me.ckListino5.Properties.Appearance.BackColor = System.Drawing.Color.Honeydew
    Me.ckListino5.Properties.Appearance.Options.UseBackColor = True
    Me.ckListino5.Properties.AutoHeight = False
    Me.ckListino5.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
    Me.ckListino5.Properties.Caption = "Listino 5"
    Me.ckListino5.Size = New System.Drawing.Size(70, 21)
    Me.ckListino5.TabIndex = 4
    '
    'ckListino4
    '
    Me.ckListino4.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckListino4.EditValue = True
    Me.ckListino4.Location = New System.Drawing.Point(264, 24)
    Me.ckListino4.Name = "ckListino4"
    Me.ckListino4.NTSCheckValue = "S"
    Me.ckListino4.NTSUnCheckValue = "N"
    Me.ckListino4.Properties.Appearance.BackColor = System.Drawing.Color.PaleTurquoise
    Me.ckListino4.Properties.Appearance.Options.UseBackColor = True
    Me.ckListino4.Properties.AutoHeight = False
    Me.ckListino4.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
    Me.ckListino4.Properties.Caption = "Listino 4"
    Me.ckListino4.Size = New System.Drawing.Size(70, 21)
    Me.ckListino4.TabIndex = 3
    '
    'ckListino3
    '
    Me.ckListino3.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckListino3.EditValue = True
    Me.ckListino3.Location = New System.Drawing.Point(178, 24)
    Me.ckListino3.Name = "ckListino3"
    Me.ckListino3.NTSCheckValue = "S"
    Me.ckListino3.NTSUnCheckValue = "N"
    Me.ckListino3.Properties.Appearance.BackColor = System.Drawing.Color.LightSkyBlue
    Me.ckListino3.Properties.Appearance.Options.UseBackColor = True
    Me.ckListino3.Properties.AutoHeight = False
    Me.ckListino3.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
    Me.ckListino3.Properties.Caption = "Listino 3"
    Me.ckListino3.Size = New System.Drawing.Size(70, 21)
    Me.ckListino3.TabIndex = 2
    '
    'ckListino2
    '
    Me.ckListino2.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckListino2.EditValue = True
    Me.ckListino2.Location = New System.Drawing.Point(92, 24)
    Me.ckListino2.Name = "ckListino2"
    Me.ckListino2.NTSCheckValue = "S"
    Me.ckListino2.NTSUnCheckValue = "N"
    Me.ckListino2.Properties.Appearance.BackColor = System.Drawing.Color.Aquamarine
    Me.ckListino2.Properties.Appearance.Options.UseBackColor = True
    Me.ckListino2.Properties.AutoHeight = False
    Me.ckListino2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
    Me.ckListino2.Properties.Caption = "Listino 2"
    Me.ckListino2.Size = New System.Drawing.Size(70, 21)
    Me.ckListino2.TabIndex = 1
    '
    'ckListino1
    '
    Me.ckListino1.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.ckListino1.EditValue = True
    Me.ckListino1.Location = New System.Drawing.Point(6, 24)
    Me.ckListino1.Name = "ckListino1"
    Me.ckListino1.NTSCheckValue = "S"
    Me.ckListino1.NTSUnCheckValue = "N"
    Me.ckListino1.Properties.Appearance.BackColor = System.Drawing.Color.MediumAquamarine
    Me.ckListino1.Properties.Appearance.Options.UseBackColor = True
    Me.ckListino1.Properties.AutoHeight = False
    Me.ckListino1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
    Me.ckListino1.Properties.Caption = "Listino 1"
    Me.ckListino1.Size = New System.Drawing.Size(70, 21)
    Me.ckListino1.TabIndex = 0
    '
    'ckBlocca
    '
    Me.ckBlocca.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckBlocca.Location = New System.Drawing.Point(18, 57)
    Me.ckBlocca.Name = "ckBlocca"
    Me.ckBlocca.NTSCheckValue = "S"
    Me.ckBlocca.NTSUnCheckValue = "N"
    Me.ckBlocca.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckBlocca.Properties.Appearance.Options.UseBackColor = True
    Me.ckBlocca.Properties.AutoHeight = False
    Me.ckBlocca.Properties.Caption = "Blocca intestazione riga"
    Me.ckBlocca.Size = New System.Drawing.Size(136, 19)
    Me.ckBlocca.TabIndex = 9
    '
    'pnGriglia
    '
    Me.pnGriglia.AllowDrop = True
    Me.pnGriglia.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGriglia.Appearance.Options.UseBackColor = True
    Me.pnGriglia.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGriglia.Controls.Add(Me.grList)
    Me.pnGriglia.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGriglia.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGriglia.Location = New System.Drawing.Point(0, 106)
    Me.pnGriglia.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGriglia.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGriglia.Name = "pnGriglia"
    Me.pnGriglia.NTSActiveTrasparency = True
    Me.pnGriglia.Size = New System.Drawing.Size(794, 332)
    Me.pnGriglia.TabIndex = 5
    Me.pnGriglia.Text = "NtsPanel3"
    '
    'grList
    '
    Me.grList.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grList.EmbeddedNavigator.Name = ""
    Me.grList.Location = New System.Drawing.Point(0, 0)
    Me.grList.MainView = Me.grvList
    Me.grList.Name = "grList"
    Me.grList.Size = New System.Drawing.Size(794, 332)
    Me.grList.TabIndex = 0
    Me.grList.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvList})
    '
    'grvList
    '
    Me.grvList.ActiveFilterEnabled = False
    Me.grvList.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ls_conto, Me.xx_desccli, Me.ls_codart, Me.xx_descart, Me.ls_fase, Me.xx_fase, Me.ls_perqta, Me.ls_listino1, Me.ls_prz1, Me.ls_daquant1, Me.ls_aquant1, Me.ls_unmis1, Me.ls_codlavo1, Me.xx_desclavo1, Me.ls_tipo1, Me.ls_dadata1, Me.ls_adata1, Me.ls_codvalu1, Me.xx_descvalu1, Me.ls_codpromo1, Me.xx_descpromo1, Me.ls_prznet1, Me.ls_listino2, Me.ls_prz2, Me.ls_daquant2, Me.ls_aquant2, Me.ls_unmis2, Me.ls_codlavo2, Me.xx_desclavo2, Me.ls_tipo2, Me.ls_dadata2, Me.ls_adata2, Me.ls_codvalu2, Me.xx_descvalu2, Me.ls_codpromo2, Me.xx_descpromo2, Me.ls_prznet2, Me.ls_listino3, Me.ls_prz3, Me.ls_daquant3, Me.ls_aquant3, Me.ls_unmis3, Me.ls_codlavo3, Me.xx_desclavo3, Me.ls_tipo3, Me.ls_dadata3, Me.ls_adata3, Me.ls_codvalu3, Me.xx_descvalu3, Me.ls_codpromo3, Me.xx_descpromo3, Me.ls_prznet3, Me.ls_listino4, Me.ls_prz4, Me.ls_daquant4, Me.ls_aquant4, Me.ls_unmis4, Me.ls_codlavo4, Me.xx_desclavo4, Me.ls_tipo4, Me.ls_dadata4, Me.ls_adata4, Me.ls_codvalu4, Me.xx_descvalu4, Me.ls_codpromo4, Me.xx_descpromo4, Me.ls_prznet4, Me.ls_listino5, Me.ls_prz5, Me.ls_daquant5, Me.ls_aquant5, Me.ls_unmis5, Me.ls_codlavo5, Me.xx_desclavo5, Me.ls_tipo5, Me.ls_dadata5, Me.ls_adata5, Me.ls_codvalu5, Me.xx_descvalu5, Me.ls_codpromo5, Me.xx_descpromo5, Me.ls_prznet5, Me.ls_scont1, Me.ls_scont2, Me.ls_scont3, Me.ls_scont4, Me.ls_scont5, Me.ls_scont6, Me.ls_scdaquant, Me.ls_scaquant, Me.ls_tiposc, Me.ls_dadatasc, Me.ls_adatasc, Me.ls_codtpro, Me.xx_descpromo, Me.ls_scont1_2, Me.ls_scont2_2, Me.ls_scont3_2, Me.ls_scont4_2, Me.ls_scont5_2, Me.ls_scont6_2, Me.ls_scdaquant_2, Me.ls_scaquant_2, Me.ls_tiposc_2, Me.ls_dadatasc_2, Me.ls_adatasc_2, Me.ls_codtpro_2, Me.xx_descpromo_2, Me.ls_scont1_3, Me.ls_scont2_3, Me.ls_scont3_3, Me.ls_scont4_3, Me.ls_scont5_3, Me.ls_scont6_3, Me.ls_scdaquant_3, Me.ls_scaquant_3, Me.ls_tiposc_3, Me.ls_dadatasc_3, Me.ls_adatasc_3, Me.ls_codtpro_3, Me.xx_descpromo_3, Me.ls_scont1_4, Me.ls_scont2_4, Me.ls_scont3_4, Me.ls_scont4_4, Me.ls_scont5_4, Me.ls_scont6_4, Me.ls_scdaquant_4, Me.ls_scaquant_4, Me.ls_tiposc_4, Me.ls_dadatasc_4, Me.ls_adatasc_4, Me.ls_codtpro_4, Me.xx_descpromo_4, Me.ls_clscan, Me.xx_clscan, Me.ls_clscar, Me.xx_clscar, Me.ar_codart, Me.xx_desint, Me.ls_coddest, Me.xx_coddest})
    Me.grvList.Enabled = True
    Me.grvList.GridControl = Me.grList
    Me.grvList.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvList.MinRowHeight = 14
    Me.grvList.Name = "grvList"
    Me.grvList.NTSAllowDelete = True
    Me.grvList.NTSAllowInsert = True
    Me.grvList.NTSAllowUpdate = True
    Me.grvList.NTSMenuContext = Nothing
    Me.grvList.OptionsCustomization.AllowRowSizing = True
    Me.grvList.OptionsFilter.AllowFilterEditor = False
    Me.grvList.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvList.OptionsNavigation.UseTabKey = False
    Me.grvList.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvList.OptionsView.ColumnAutoWidth = False
    Me.grvList.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvList.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvList.OptionsView.ShowGroupPanel = False
    Me.grvList.RowHeight = 14
    '
    'ls_conto
    '
    Me.ls_conto.AppearanceCell.Options.UseBackColor = True
    Me.ls_conto.AppearanceCell.Options.UseTextOptions = True
    Me.ls_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_conto.Caption = "Cliente\Forn."
    Me.ls_conto.Enabled = False
    Me.ls_conto.FieldName = "ls_conto"
    Me.ls_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_conto.Name = "ls_conto"
    Me.ls_conto.NTSRepositoryComboBox = Nothing
    Me.ls_conto.NTSRepositoryItemCheck = Nothing
    Me.ls_conto.NTSRepositoryItemMemo = Nothing
    Me.ls_conto.NTSRepositoryItemText = Nothing
    Me.ls_conto.OptionsColumn.AllowEdit = False
    Me.ls_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_conto.OptionsColumn.ReadOnly = True
    Me.ls_conto.OptionsFilter.AllowFilter = False
    Me.ls_conto.Visible = True
    Me.ls_conto.VisibleIndex = 0
    '
    'xx_desccli
    '
    Me.xx_desccli.AppearanceCell.Options.UseBackColor = True
    Me.xx_desccli.AppearanceCell.Options.UseTextOptions = True
    Me.xx_desccli.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_desccli.Caption = "Descr. cli\for"
    Me.xx_desccli.Enabled = False
    Me.xx_desccli.FieldName = "xx_desccli"
    Me.xx_desccli.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_desccli.Name = "xx_desccli"
    Me.xx_desccli.NTSRepositoryComboBox = Nothing
    Me.xx_desccli.NTSRepositoryItemCheck = Nothing
    Me.xx_desccli.NTSRepositoryItemMemo = Nothing
    Me.xx_desccli.NTSRepositoryItemText = Nothing
    Me.xx_desccli.OptionsColumn.AllowEdit = False
    Me.xx_desccli.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_desccli.OptionsColumn.ReadOnly = True
    Me.xx_desccli.OptionsFilter.AllowFilter = False
    Me.xx_desccli.Visible = True
    Me.xx_desccli.VisibleIndex = 1
    '
    'ls_codart
    '
    Me.ls_codart.AppearanceCell.Options.UseBackColor = True
    Me.ls_codart.AppearanceCell.Options.UseTextOptions = True
    Me.ls_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_codart.Caption = "Articolo"
    Me.ls_codart.Enabled = False
    Me.ls_codart.FieldName = "ls_codart"
    Me.ls_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_codart.Name = "ls_codart"
    Me.ls_codart.NTSRepositoryComboBox = Nothing
    Me.ls_codart.NTSRepositoryItemCheck = Nothing
    Me.ls_codart.NTSRepositoryItemMemo = Nothing
    Me.ls_codart.NTSRepositoryItemText = Nothing
    Me.ls_codart.OptionsColumn.AllowEdit = False
    Me.ls_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_codart.OptionsColumn.ReadOnly = True
    Me.ls_codart.OptionsFilter.AllowFilter = False
    Me.ls_codart.Visible = True
    Me.ls_codart.VisibleIndex = 2
    '
    'xx_descart
    '
    Me.xx_descart.AppearanceCell.Options.UseBackColor = True
    Me.xx_descart.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descart.Caption = "Descr. Art."
    Me.xx_descart.Enabled = False
    Me.xx_descart.FieldName = "xx_descart"
    Me.xx_descart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descart.Name = "xx_descart"
    Me.xx_descart.NTSRepositoryComboBox = Nothing
    Me.xx_descart.NTSRepositoryItemCheck = Nothing
    Me.xx_descart.NTSRepositoryItemMemo = Nothing
    Me.xx_descart.NTSRepositoryItemText = Nothing
    Me.xx_descart.OptionsColumn.AllowEdit = False
    Me.xx_descart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descart.OptionsColumn.ReadOnly = True
    Me.xx_descart.OptionsFilter.AllowFilter = False
    Me.xx_descart.Visible = True
    Me.xx_descart.VisibleIndex = 3
    '
    'ls_fase
    '
    Me.ls_fase.AppearanceCell.Options.UseBackColor = True
    Me.ls_fase.AppearanceCell.Options.UseTextOptions = True
    Me.ls_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_fase.Caption = "Fase"
    Me.ls_fase.Enabled = False
    Me.ls_fase.FieldName = "ls_fase"
    Me.ls_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_fase.Name = "ls_fase"
    Me.ls_fase.NTSRepositoryComboBox = Nothing
    Me.ls_fase.NTSRepositoryItemCheck = Nothing
    Me.ls_fase.NTSRepositoryItemMemo = Nothing
    Me.ls_fase.NTSRepositoryItemText = Nothing
    Me.ls_fase.OptionsColumn.AllowEdit = False
    Me.ls_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_fase.OptionsColumn.ReadOnly = True
    Me.ls_fase.OptionsFilter.AllowFilter = False
    '
    'xx_fase
    '
    Me.xx_fase.AppearanceCell.Options.UseBackColor = True
    Me.xx_fase.AppearanceCell.Options.UseTextOptions = True
    Me.xx_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_fase.Caption = "Descr. Fase"
    Me.xx_fase.Enabled = False
    Me.xx_fase.FieldName = "xx_fase"
    Me.xx_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_fase.Name = "xx_fase"
    Me.xx_fase.NTSRepositoryComboBox = Nothing
    Me.xx_fase.NTSRepositoryItemCheck = Nothing
    Me.xx_fase.NTSRepositoryItemMemo = Nothing
    Me.xx_fase.NTSRepositoryItemText = Nothing
    Me.xx_fase.OptionsColumn.AllowEdit = False
    Me.xx_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_fase.OptionsColumn.ReadOnly = True
    Me.xx_fase.OptionsFilter.AllowFilter = False
    '
    'ls_perqta
    '
    Me.ls_perqta.AppearanceCell.Options.UseBackColor = True
    Me.ls_perqta.AppearanceCell.Options.UseTextOptions = True
    Me.ls_perqta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_perqta.Caption = "Per qta"
    Me.ls_perqta.Enabled = False
    Me.ls_perqta.FieldName = "ls_perqta"
    Me.ls_perqta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_perqta.Name = "ls_perqta"
    Me.ls_perqta.NTSRepositoryComboBox = Nothing
    Me.ls_perqta.NTSRepositoryItemCheck = Nothing
    Me.ls_perqta.NTSRepositoryItemMemo = Nothing
    Me.ls_perqta.NTSRepositoryItemText = Nothing
    Me.ls_perqta.OptionsColumn.AllowEdit = False
    Me.ls_perqta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_perqta.OptionsColumn.ReadOnly = True
    Me.ls_perqta.OptionsFilter.AllowFilter = False
    '
    'ls_listino1
    '
    Me.ls_listino1.AppearanceCell.Options.UseBackColor = True
    Me.ls_listino1.AppearanceCell.Options.UseTextOptions = True
    Me.ls_listino1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_listino1.Caption = "Listino 1"
    Me.ls_listino1.Enabled = False
    Me.ls_listino1.FieldName = "ls_listino1"
    Me.ls_listino1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_listino1.Name = "ls_listino1"
    Me.ls_listino1.NTSRepositoryComboBox = Nothing
    Me.ls_listino1.NTSRepositoryItemCheck = Nothing
    Me.ls_listino1.NTSRepositoryItemMemo = Nothing
    Me.ls_listino1.NTSRepositoryItemText = Nothing
    Me.ls_listino1.OptionsColumn.AllowEdit = False
    Me.ls_listino1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_listino1.OptionsColumn.ReadOnly = True
    Me.ls_listino1.OptionsFilter.AllowFilter = False
    Me.ls_listino1.Visible = True
    Me.ls_listino1.VisibleIndex = 4
    '
    'ls_prz1
    '
    Me.ls_prz1.AppearanceCell.Options.UseBackColor = True
    Me.ls_prz1.AppearanceCell.Options.UseTextOptions = True
    Me.ls_prz1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_prz1.Caption = "Prezzo 1"
    Me.ls_prz1.Enabled = True
    Me.ls_prz1.FieldName = "ls_prz1"
    Me.ls_prz1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_prz1.Name = "ls_prz1"
    Me.ls_prz1.NTSRepositoryComboBox = Nothing
    Me.ls_prz1.NTSRepositoryItemCheck = Nothing
    Me.ls_prz1.NTSRepositoryItemMemo = Nothing
    Me.ls_prz1.NTSRepositoryItemText = Nothing
    Me.ls_prz1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_prz1.OptionsFilter.AllowFilter = False
    Me.ls_prz1.Visible = True
    Me.ls_prz1.VisibleIndex = 5
    '
    'ls_daquant1
    '
    Me.ls_daquant1.AppearanceCell.Options.UseBackColor = True
    Me.ls_daquant1.AppearanceCell.Options.UseTextOptions = True
    Me.ls_daquant1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_daquant1.Caption = "Da qta 1"
    Me.ls_daquant1.Enabled = False
    Me.ls_daquant1.FieldName = "ls_daquant1"
    Me.ls_daquant1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_daquant1.Name = "ls_daquant1"
    Me.ls_daquant1.NTSRepositoryComboBox = Nothing
    Me.ls_daquant1.NTSRepositoryItemCheck = Nothing
    Me.ls_daquant1.NTSRepositoryItemMemo = Nothing
    Me.ls_daquant1.NTSRepositoryItemText = Nothing
    Me.ls_daquant1.OptionsColumn.AllowEdit = False
    Me.ls_daquant1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_daquant1.OptionsColumn.ReadOnly = True
    Me.ls_daquant1.OptionsFilter.AllowFilter = False
    '
    'ls_aquant1
    '
    Me.ls_aquant1.AppearanceCell.Options.UseBackColor = True
    Me.ls_aquant1.AppearanceCell.Options.UseTextOptions = True
    Me.ls_aquant1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_aquant1.Caption = "A qta 1"
    Me.ls_aquant1.Enabled = False
    Me.ls_aquant1.FieldName = "ls_aquant1"
    Me.ls_aquant1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_aquant1.Name = "ls_aquant1"
    Me.ls_aquant1.NTSRepositoryComboBox = Nothing
    Me.ls_aquant1.NTSRepositoryItemCheck = Nothing
    Me.ls_aquant1.NTSRepositoryItemMemo = Nothing
    Me.ls_aquant1.NTSRepositoryItemText = Nothing
    Me.ls_aquant1.OptionsColumn.AllowEdit = False
    Me.ls_aquant1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_aquant1.OptionsColumn.ReadOnly = True
    Me.ls_aquant1.OptionsFilter.AllowFilter = False
    '
    'ls_unmis1
    '
    Me.ls_unmis1.AppearanceCell.Options.UseBackColor = True
    Me.ls_unmis1.AppearanceCell.Options.UseTextOptions = True
    Me.ls_unmis1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_unmis1.Caption = "Un. mis. 1"
    Me.ls_unmis1.Enabled = False
    Me.ls_unmis1.FieldName = "ls_unmis1"
    Me.ls_unmis1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_unmis1.Name = "ls_unmis1"
    Me.ls_unmis1.NTSRepositoryComboBox = Nothing
    Me.ls_unmis1.NTSRepositoryItemCheck = Nothing
    Me.ls_unmis1.NTSRepositoryItemMemo = Nothing
    Me.ls_unmis1.NTSRepositoryItemText = Nothing
    Me.ls_unmis1.OptionsColumn.AllowEdit = False
    Me.ls_unmis1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_unmis1.OptionsColumn.ReadOnly = True
    Me.ls_unmis1.OptionsFilter.AllowFilter = False
    Me.ls_unmis1.Visible = True
    Me.ls_unmis1.VisibleIndex = 6
    '
    'ls_codlavo1
    '
    Me.ls_codlavo1.AppearanceCell.Options.UseBackColor = True
    Me.ls_codlavo1.AppearanceCell.Options.UseTextOptions = True
    Me.ls_codlavo1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_codlavo1.Caption = "Lavorazione 1"
    Me.ls_codlavo1.Enabled = False
    Me.ls_codlavo1.FieldName = "ls_codlavo1"
    Me.ls_codlavo1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_codlavo1.Name = "ls_codlavo1"
    Me.ls_codlavo1.NTSRepositoryComboBox = Nothing
    Me.ls_codlavo1.NTSRepositoryItemCheck = Nothing
    Me.ls_codlavo1.NTSRepositoryItemMemo = Nothing
    Me.ls_codlavo1.NTSRepositoryItemText = Nothing
    Me.ls_codlavo1.OptionsColumn.AllowEdit = False
    Me.ls_codlavo1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_codlavo1.OptionsColumn.ReadOnly = True
    Me.ls_codlavo1.OptionsFilter.AllowFilter = False
    Me.ls_codlavo1.Visible = True
    Me.ls_codlavo1.VisibleIndex = 7
    '
    'xx_desclavo1
    '
    Me.xx_desclavo1.AppearanceCell.Options.UseBackColor = True
    Me.xx_desclavo1.AppearanceCell.Options.UseTextOptions = True
    Me.xx_desclavo1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_desclavo1.Caption = "Descr. lavo. 1"
    Me.xx_desclavo1.Enabled = False
    Me.xx_desclavo1.FieldName = "xx_desclavo1"
    Me.xx_desclavo1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_desclavo1.Name = "xx_desclavo1"
    Me.xx_desclavo1.NTSRepositoryComboBox = Nothing
    Me.xx_desclavo1.NTSRepositoryItemCheck = Nothing
    Me.xx_desclavo1.NTSRepositoryItemMemo = Nothing
    Me.xx_desclavo1.NTSRepositoryItemText = Nothing
    Me.xx_desclavo1.OptionsColumn.AllowEdit = False
    Me.xx_desclavo1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_desclavo1.OptionsColumn.ReadOnly = True
    Me.xx_desclavo1.OptionsFilter.AllowFilter = False
    Me.xx_desclavo1.Visible = True
    Me.xx_desclavo1.VisibleIndex = 8
    '
    'ls_tipo1
    '
    Me.ls_tipo1.AppearanceCell.Options.UseBackColor = True
    Me.ls_tipo1.AppearanceCell.Options.UseTextOptions = True
    Me.ls_tipo1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_tipo1.Caption = "Tipo 1"
    Me.ls_tipo1.Enabled = False
    Me.ls_tipo1.FieldName = "ls_tipo1"
    Me.ls_tipo1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_tipo1.Name = "ls_tipo1"
    Me.ls_tipo1.NTSRepositoryComboBox = Nothing
    Me.ls_tipo1.NTSRepositoryItemCheck = Nothing
    Me.ls_tipo1.NTSRepositoryItemMemo = Nothing
    Me.ls_tipo1.NTSRepositoryItemText = Nothing
    Me.ls_tipo1.OptionsColumn.AllowEdit = False
    Me.ls_tipo1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_tipo1.OptionsColumn.ReadOnly = True
    Me.ls_tipo1.OptionsFilter.AllowFilter = False
    Me.ls_tipo1.Visible = True
    Me.ls_tipo1.VisibleIndex = 9
    '
    'ls_dadata1
    '
    Me.ls_dadata1.AppearanceCell.Options.UseBackColor = True
    Me.ls_dadata1.AppearanceCell.Options.UseTextOptions = True
    Me.ls_dadata1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_dadata1.Caption = "Da data 1"
    Me.ls_dadata1.Enabled = True
    Me.ls_dadata1.FieldName = "ls_dadata1"
    Me.ls_dadata1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_dadata1.Name = "ls_dadata1"
    Me.ls_dadata1.NTSRepositoryComboBox = Nothing
    Me.ls_dadata1.NTSRepositoryItemCheck = Nothing
    Me.ls_dadata1.NTSRepositoryItemMemo = Nothing
    Me.ls_dadata1.NTSRepositoryItemText = Nothing
    Me.ls_dadata1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_dadata1.OptionsFilter.AllowFilter = False
    Me.ls_dadata1.Visible = True
    Me.ls_dadata1.VisibleIndex = 10
    '
    'ls_adata1
    '
    Me.ls_adata1.AppearanceCell.Options.UseBackColor = True
    Me.ls_adata1.AppearanceCell.Options.UseTextOptions = True
    Me.ls_adata1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_adata1.Caption = "A data 1"
    Me.ls_adata1.Enabled = False
    Me.ls_adata1.FieldName = "ls_adata1"
    Me.ls_adata1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_adata1.Name = "ls_adata1"
    Me.ls_adata1.NTSRepositoryComboBox = Nothing
    Me.ls_adata1.NTSRepositoryItemCheck = Nothing
    Me.ls_adata1.NTSRepositoryItemMemo = Nothing
    Me.ls_adata1.NTSRepositoryItemText = Nothing
    Me.ls_adata1.OptionsColumn.AllowEdit = False
    Me.ls_adata1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_adata1.OptionsColumn.ReadOnly = True
    Me.ls_adata1.OptionsFilter.AllowFilter = False
    Me.ls_adata1.Visible = True
    Me.ls_adata1.VisibleIndex = 11
    '
    'ls_codvalu1
    '
    Me.ls_codvalu1.AppearanceCell.Options.UseBackColor = True
    Me.ls_codvalu1.AppearanceCell.Options.UseTextOptions = True
    Me.ls_codvalu1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_codvalu1.Caption = "Valuta 1"
    Me.ls_codvalu1.Enabled = False
    Me.ls_codvalu1.FieldName = "ls_codvalu1"
    Me.ls_codvalu1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_codvalu1.Name = "ls_codvalu1"
    Me.ls_codvalu1.NTSRepositoryComboBox = Nothing
    Me.ls_codvalu1.NTSRepositoryItemCheck = Nothing
    Me.ls_codvalu1.NTSRepositoryItemMemo = Nothing
    Me.ls_codvalu1.NTSRepositoryItemText = Nothing
    Me.ls_codvalu1.OptionsColumn.AllowEdit = False
    Me.ls_codvalu1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_codvalu1.OptionsColumn.ReadOnly = True
    Me.ls_codvalu1.OptionsFilter.AllowFilter = False
    Me.ls_codvalu1.Visible = True
    Me.ls_codvalu1.VisibleIndex = 12
    '
    'xx_descvalu1
    '
    Me.xx_descvalu1.AppearanceCell.Options.UseBackColor = True
    Me.xx_descvalu1.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descvalu1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descvalu1.Caption = "Descr. valu. 1"
    Me.xx_descvalu1.Enabled = False
    Me.xx_descvalu1.FieldName = "xx_descvalu1"
    Me.xx_descvalu1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descvalu1.Name = "xx_descvalu1"
    Me.xx_descvalu1.NTSRepositoryComboBox = Nothing
    Me.xx_descvalu1.NTSRepositoryItemCheck = Nothing
    Me.xx_descvalu1.NTSRepositoryItemMemo = Nothing
    Me.xx_descvalu1.NTSRepositoryItemText = Nothing
    Me.xx_descvalu1.OptionsColumn.AllowEdit = False
    Me.xx_descvalu1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descvalu1.OptionsColumn.ReadOnly = True
    Me.xx_descvalu1.OptionsFilter.AllowFilter = False
    Me.xx_descvalu1.Visible = True
    Me.xx_descvalu1.VisibleIndex = 13
    '
    'ls_codpromo1
    '
    Me.ls_codpromo1.AppearanceCell.Options.UseBackColor = True
    Me.ls_codpromo1.AppearanceCell.Options.UseTextOptions = True
    Me.ls_codpromo1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_codpromo1.Caption = "Promozione 1"
    Me.ls_codpromo1.Enabled = False
    Me.ls_codpromo1.FieldName = "ls_codpromo1"
    Me.ls_codpromo1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_codpromo1.Name = "ls_codpromo1"
    Me.ls_codpromo1.NTSRepositoryComboBox = Nothing
    Me.ls_codpromo1.NTSRepositoryItemCheck = Nothing
    Me.ls_codpromo1.NTSRepositoryItemMemo = Nothing
    Me.ls_codpromo1.NTSRepositoryItemText = Nothing
    Me.ls_codpromo1.OptionsColumn.AllowEdit = False
    Me.ls_codpromo1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_codpromo1.OptionsColumn.ReadOnly = True
    Me.ls_codpromo1.OptionsFilter.AllowFilter = False
    Me.ls_codpromo1.Visible = True
    Me.ls_codpromo1.VisibleIndex = 14
    '
    'xx_descpromo1
    '
    Me.xx_descpromo1.AppearanceCell.Options.UseBackColor = True
    Me.xx_descpromo1.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descpromo1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descpromo1.Caption = "Desc. promo. 1"
    Me.xx_descpromo1.Enabled = False
    Me.xx_descpromo1.FieldName = "xx_descpromo1"
    Me.xx_descpromo1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descpromo1.Name = "xx_descpromo1"
    Me.xx_descpromo1.NTSRepositoryComboBox = Nothing
    Me.xx_descpromo1.NTSRepositoryItemCheck = Nothing
    Me.xx_descpromo1.NTSRepositoryItemMemo = Nothing
    Me.xx_descpromo1.NTSRepositoryItemText = Nothing
    Me.xx_descpromo1.OptionsColumn.AllowEdit = False
    Me.xx_descpromo1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descpromo1.OptionsColumn.ReadOnly = True
    Me.xx_descpromo1.OptionsFilter.AllowFilter = False
    Me.xx_descpromo1.Visible = True
    Me.xx_descpromo1.VisibleIndex = 15
    '
    'ls_prznet1
    '
    Me.ls_prznet1.AppearanceCell.Options.UseBackColor = True
    Me.ls_prznet1.AppearanceCell.Options.UseTextOptions = True
    Me.ls_prznet1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_prznet1.Caption = "Prezzo netto 1"
    Me.ls_prznet1.Enabled = False
    Me.ls_prznet1.FieldName = "ls_prznet1"
    Me.ls_prznet1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_prznet1.Name = "ls_prznet1"
    Me.ls_prznet1.NTSRepositoryComboBox = Nothing
    Me.ls_prznet1.NTSRepositoryItemCheck = Nothing
    Me.ls_prznet1.NTSRepositoryItemMemo = Nothing
    Me.ls_prznet1.NTSRepositoryItemText = Nothing
    Me.ls_prznet1.OptionsColumn.AllowEdit = False
    Me.ls_prznet1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_prznet1.OptionsColumn.ReadOnly = True
    Me.ls_prznet1.OptionsFilter.AllowFilter = False
    Me.ls_prznet1.Visible = True
    Me.ls_prznet1.VisibleIndex = 16
    '
    'ls_listino2
    '
    Me.ls_listino2.AppearanceCell.Options.UseBackColor = True
    Me.ls_listino2.AppearanceCell.Options.UseTextOptions = True
    Me.ls_listino2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_listino2.Caption = "Listino 2"
    Me.ls_listino2.Enabled = False
    Me.ls_listino2.FieldName = "ls_listino2"
    Me.ls_listino2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_listino2.Name = "ls_listino2"
    Me.ls_listino2.NTSRepositoryComboBox = Nothing
    Me.ls_listino2.NTSRepositoryItemCheck = Nothing
    Me.ls_listino2.NTSRepositoryItemMemo = Nothing
    Me.ls_listino2.NTSRepositoryItemText = Nothing
    Me.ls_listino2.OptionsColumn.AllowEdit = False
    Me.ls_listino2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_listino2.OptionsColumn.ReadOnly = True
    Me.ls_listino2.OptionsFilter.AllowFilter = False
    Me.ls_listino2.Visible = True
    Me.ls_listino2.VisibleIndex = 17
    '
    'ls_prz2
    '
    Me.ls_prz2.AppearanceCell.Options.UseBackColor = True
    Me.ls_prz2.AppearanceCell.Options.UseTextOptions = True
    Me.ls_prz2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_prz2.Caption = "Prezzo 2"
    Me.ls_prz2.Enabled = True
    Me.ls_prz2.FieldName = "ls_prz2"
    Me.ls_prz2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_prz2.Name = "ls_prz2"
    Me.ls_prz2.NTSRepositoryComboBox = Nothing
    Me.ls_prz2.NTSRepositoryItemCheck = Nothing
    Me.ls_prz2.NTSRepositoryItemMemo = Nothing
    Me.ls_prz2.NTSRepositoryItemText = Nothing
    Me.ls_prz2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_prz2.OptionsFilter.AllowFilter = False
    Me.ls_prz2.Visible = True
    Me.ls_prz2.VisibleIndex = 18
    '
    'ls_daquant2
    '
    Me.ls_daquant2.AppearanceCell.Options.UseBackColor = True
    Me.ls_daquant2.AppearanceCell.Options.UseTextOptions = True
    Me.ls_daquant2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_daquant2.Caption = "Da qta 2"
    Me.ls_daquant2.Enabled = False
    Me.ls_daquant2.FieldName = "ls_daquant2"
    Me.ls_daquant2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_daquant2.Name = "ls_daquant2"
    Me.ls_daquant2.NTSRepositoryComboBox = Nothing
    Me.ls_daquant2.NTSRepositoryItemCheck = Nothing
    Me.ls_daquant2.NTSRepositoryItemMemo = Nothing
    Me.ls_daquant2.NTSRepositoryItemText = Nothing
    Me.ls_daquant2.OptionsColumn.AllowEdit = False
    Me.ls_daquant2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_daquant2.OptionsColumn.ReadOnly = True
    Me.ls_daquant2.OptionsFilter.AllowFilter = False
    '
    'ls_aquant2
    '
    Me.ls_aquant2.AppearanceCell.Options.UseBackColor = True
    Me.ls_aquant2.AppearanceCell.Options.UseTextOptions = True
    Me.ls_aquant2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_aquant2.Caption = "A qta 2"
    Me.ls_aquant2.Enabled = False
    Me.ls_aquant2.FieldName = "ls_aquant2"
    Me.ls_aquant2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_aquant2.Name = "ls_aquant2"
    Me.ls_aquant2.NTSRepositoryComboBox = Nothing
    Me.ls_aquant2.NTSRepositoryItemCheck = Nothing
    Me.ls_aquant2.NTSRepositoryItemMemo = Nothing
    Me.ls_aquant2.NTSRepositoryItemText = Nothing
    Me.ls_aquant2.OptionsColumn.AllowEdit = False
    Me.ls_aquant2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_aquant2.OptionsColumn.ReadOnly = True
    Me.ls_aquant2.OptionsFilter.AllowFilter = False
    '
    'ls_unmis2
    '
    Me.ls_unmis2.AppearanceCell.Options.UseBackColor = True
    Me.ls_unmis2.AppearanceCell.Options.UseTextOptions = True
    Me.ls_unmis2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_unmis2.Caption = "Un. mis. 2"
    Me.ls_unmis2.Enabled = False
    Me.ls_unmis2.FieldName = "ls_unmis2"
    Me.ls_unmis2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_unmis2.Name = "ls_unmis2"
    Me.ls_unmis2.NTSRepositoryComboBox = Nothing
    Me.ls_unmis2.NTSRepositoryItemCheck = Nothing
    Me.ls_unmis2.NTSRepositoryItemMemo = Nothing
    Me.ls_unmis2.NTSRepositoryItemText = Nothing
    Me.ls_unmis2.OptionsColumn.AllowEdit = False
    Me.ls_unmis2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_unmis2.OptionsColumn.ReadOnly = True
    Me.ls_unmis2.OptionsFilter.AllowFilter = False
    Me.ls_unmis2.Visible = True
    Me.ls_unmis2.VisibleIndex = 19
    '
    'ls_codlavo2
    '
    Me.ls_codlavo2.AppearanceCell.Options.UseBackColor = True
    Me.ls_codlavo2.AppearanceCell.Options.UseTextOptions = True
    Me.ls_codlavo2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_codlavo2.Caption = "Lavorazione 2"
    Me.ls_codlavo2.Enabled = False
    Me.ls_codlavo2.FieldName = "ls_codlavo2"
    Me.ls_codlavo2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_codlavo2.Name = "ls_codlavo2"
    Me.ls_codlavo2.NTSRepositoryComboBox = Nothing
    Me.ls_codlavo2.NTSRepositoryItemCheck = Nothing
    Me.ls_codlavo2.NTSRepositoryItemMemo = Nothing
    Me.ls_codlavo2.NTSRepositoryItemText = Nothing
    Me.ls_codlavo2.OptionsColumn.AllowEdit = False
    Me.ls_codlavo2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_codlavo2.OptionsColumn.ReadOnly = True
    Me.ls_codlavo2.OptionsFilter.AllowFilter = False
    Me.ls_codlavo2.Visible = True
    Me.ls_codlavo2.VisibleIndex = 20
    '
    'xx_desclavo2
    '
    Me.xx_desclavo2.AppearanceCell.Options.UseBackColor = True
    Me.xx_desclavo2.AppearanceCell.Options.UseTextOptions = True
    Me.xx_desclavo2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_desclavo2.Caption = "Descr. lavo. 2"
    Me.xx_desclavo2.Enabled = False
    Me.xx_desclavo2.FieldName = "xx_desclavo2"
    Me.xx_desclavo2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_desclavo2.Name = "xx_desclavo2"
    Me.xx_desclavo2.NTSRepositoryComboBox = Nothing
    Me.xx_desclavo2.NTSRepositoryItemCheck = Nothing
    Me.xx_desclavo2.NTSRepositoryItemMemo = Nothing
    Me.xx_desclavo2.NTSRepositoryItemText = Nothing
    Me.xx_desclavo2.OptionsColumn.AllowEdit = False
    Me.xx_desclavo2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_desclavo2.OptionsColumn.ReadOnly = True
    Me.xx_desclavo2.OptionsFilter.AllowFilter = False
    Me.xx_desclavo2.Visible = True
    Me.xx_desclavo2.VisibleIndex = 21
    '
    'ls_tipo2
    '
    Me.ls_tipo2.AppearanceCell.Options.UseBackColor = True
    Me.ls_tipo2.AppearanceCell.Options.UseTextOptions = True
    Me.ls_tipo2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_tipo2.Caption = "Tipo 2"
    Me.ls_tipo2.Enabled = False
    Me.ls_tipo2.FieldName = "ls_tipo2"
    Me.ls_tipo2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_tipo2.Name = "ls_tipo2"
    Me.ls_tipo2.NTSRepositoryComboBox = Nothing
    Me.ls_tipo2.NTSRepositoryItemCheck = Nothing
    Me.ls_tipo2.NTSRepositoryItemMemo = Nothing
    Me.ls_tipo2.NTSRepositoryItemText = Nothing
    Me.ls_tipo2.OptionsColumn.AllowEdit = False
    Me.ls_tipo2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_tipo2.OptionsColumn.ReadOnly = True
    Me.ls_tipo2.OptionsFilter.AllowFilter = False
    Me.ls_tipo2.Visible = True
    Me.ls_tipo2.VisibleIndex = 22
    '
    'ls_dadata2
    '
    Me.ls_dadata2.AppearanceCell.Options.UseBackColor = True
    Me.ls_dadata2.AppearanceCell.Options.UseTextOptions = True
    Me.ls_dadata2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_dadata2.Caption = "Da data 2"
    Me.ls_dadata2.Enabled = True
    Me.ls_dadata2.FieldName = "ls_dadata2"
    Me.ls_dadata2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_dadata2.Name = "ls_dadata2"
    Me.ls_dadata2.NTSRepositoryComboBox = Nothing
    Me.ls_dadata2.NTSRepositoryItemCheck = Nothing
    Me.ls_dadata2.NTSRepositoryItemMemo = Nothing
    Me.ls_dadata2.NTSRepositoryItemText = Nothing
    Me.ls_dadata2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_dadata2.OptionsFilter.AllowFilter = False
    Me.ls_dadata2.Visible = True
    Me.ls_dadata2.VisibleIndex = 23
    '
    'ls_adata2
    '
    Me.ls_adata2.AppearanceCell.Options.UseBackColor = True
    Me.ls_adata2.AppearanceCell.Options.UseTextOptions = True
    Me.ls_adata2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_adata2.Caption = "A data 2"
    Me.ls_adata2.Enabled = False
    Me.ls_adata2.FieldName = "ls_adata2"
    Me.ls_adata2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_adata2.Name = "ls_adata2"
    Me.ls_adata2.NTSRepositoryComboBox = Nothing
    Me.ls_adata2.NTSRepositoryItemCheck = Nothing
    Me.ls_adata2.NTSRepositoryItemMemo = Nothing
    Me.ls_adata2.NTSRepositoryItemText = Nothing
    Me.ls_adata2.OptionsColumn.AllowEdit = False
    Me.ls_adata2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_adata2.OptionsColumn.ReadOnly = True
    Me.ls_adata2.OptionsFilter.AllowFilter = False
    Me.ls_adata2.Visible = True
    Me.ls_adata2.VisibleIndex = 24
    '
    'ls_codvalu2
    '
    Me.ls_codvalu2.AppearanceCell.Options.UseBackColor = True
    Me.ls_codvalu2.AppearanceCell.Options.UseTextOptions = True
    Me.ls_codvalu2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_codvalu2.Caption = "Valuta 2"
    Me.ls_codvalu2.Enabled = False
    Me.ls_codvalu2.FieldName = "ls_codvalu2"
    Me.ls_codvalu2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_codvalu2.Name = "ls_codvalu2"
    Me.ls_codvalu2.NTSRepositoryComboBox = Nothing
    Me.ls_codvalu2.NTSRepositoryItemCheck = Nothing
    Me.ls_codvalu2.NTSRepositoryItemMemo = Nothing
    Me.ls_codvalu2.NTSRepositoryItemText = Nothing
    Me.ls_codvalu2.OptionsColumn.AllowEdit = False
    Me.ls_codvalu2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_codvalu2.OptionsColumn.ReadOnly = True
    Me.ls_codvalu2.OptionsFilter.AllowFilter = False
    Me.ls_codvalu2.Visible = True
    Me.ls_codvalu2.VisibleIndex = 25
    '
    'xx_descvalu2
    '
    Me.xx_descvalu2.AppearanceCell.Options.UseBackColor = True
    Me.xx_descvalu2.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descvalu2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descvalu2.Caption = "Descr. valu. 2"
    Me.xx_descvalu2.Enabled = False
    Me.xx_descvalu2.FieldName = "xx_descvalu2"
    Me.xx_descvalu2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descvalu2.Name = "xx_descvalu2"
    Me.xx_descvalu2.NTSRepositoryComboBox = Nothing
    Me.xx_descvalu2.NTSRepositoryItemCheck = Nothing
    Me.xx_descvalu2.NTSRepositoryItemMemo = Nothing
    Me.xx_descvalu2.NTSRepositoryItemText = Nothing
    Me.xx_descvalu2.OptionsColumn.AllowEdit = False
    Me.xx_descvalu2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descvalu2.OptionsColumn.ReadOnly = True
    Me.xx_descvalu2.OptionsFilter.AllowFilter = False
    Me.xx_descvalu2.Visible = True
    Me.xx_descvalu2.VisibleIndex = 26
    '
    'ls_codpromo2
    '
    Me.ls_codpromo2.AppearanceCell.Options.UseBackColor = True
    Me.ls_codpromo2.AppearanceCell.Options.UseTextOptions = True
    Me.ls_codpromo2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_codpromo2.Caption = "Promozione 2"
    Me.ls_codpromo2.Enabled = False
    Me.ls_codpromo2.FieldName = "ls_codpromo2"
    Me.ls_codpromo2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_codpromo2.Name = "ls_codpromo2"
    Me.ls_codpromo2.NTSRepositoryComboBox = Nothing
    Me.ls_codpromo2.NTSRepositoryItemCheck = Nothing
    Me.ls_codpromo2.NTSRepositoryItemMemo = Nothing
    Me.ls_codpromo2.NTSRepositoryItemText = Nothing
    Me.ls_codpromo2.OptionsColumn.AllowEdit = False
    Me.ls_codpromo2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_codpromo2.OptionsColumn.ReadOnly = True
    Me.ls_codpromo2.OptionsFilter.AllowFilter = False
    Me.ls_codpromo2.Visible = True
    Me.ls_codpromo2.VisibleIndex = 27
    '
    'xx_descpromo2
    '
    Me.xx_descpromo2.AppearanceCell.Options.UseBackColor = True
    Me.xx_descpromo2.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descpromo2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descpromo2.Caption = "Desc. promo. 2"
    Me.xx_descpromo2.Enabled = False
    Me.xx_descpromo2.FieldName = "xx_descpromo2"
    Me.xx_descpromo2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descpromo2.Name = "xx_descpromo2"
    Me.xx_descpromo2.NTSRepositoryComboBox = Nothing
    Me.xx_descpromo2.NTSRepositoryItemCheck = Nothing
    Me.xx_descpromo2.NTSRepositoryItemMemo = Nothing
    Me.xx_descpromo2.NTSRepositoryItemText = Nothing
    Me.xx_descpromo2.OptionsColumn.AllowEdit = False
    Me.xx_descpromo2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descpromo2.OptionsColumn.ReadOnly = True
    Me.xx_descpromo2.OptionsFilter.AllowFilter = False
    Me.xx_descpromo2.Visible = True
    Me.xx_descpromo2.VisibleIndex = 28
    '
    'ls_prznet2
    '
    Me.ls_prznet2.AppearanceCell.Options.UseBackColor = True
    Me.ls_prznet2.AppearanceCell.Options.UseTextOptions = True
    Me.ls_prznet2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_prznet2.Caption = "Prezzo netto 2"
    Me.ls_prznet2.Enabled = False
    Me.ls_prznet2.FieldName = "ls_prznet2"
    Me.ls_prznet2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_prznet2.Name = "ls_prznet2"
    Me.ls_prznet2.NTSRepositoryComboBox = Nothing
    Me.ls_prznet2.NTSRepositoryItemCheck = Nothing
    Me.ls_prznet2.NTSRepositoryItemMemo = Nothing
    Me.ls_prznet2.NTSRepositoryItemText = Nothing
    Me.ls_prznet2.OptionsColumn.AllowEdit = False
    Me.ls_prznet2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_prznet2.OptionsColumn.ReadOnly = True
    Me.ls_prznet2.OptionsFilter.AllowFilter = False
    Me.ls_prznet2.Visible = True
    Me.ls_prznet2.VisibleIndex = 29
    '
    'ls_listino3
    '
    Me.ls_listino3.AppearanceCell.Options.UseBackColor = True
    Me.ls_listino3.AppearanceCell.Options.UseTextOptions = True
    Me.ls_listino3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_listino3.Caption = "Listino 3"
    Me.ls_listino3.Enabled = False
    Me.ls_listino3.FieldName = "ls_listino3"
    Me.ls_listino3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_listino3.Name = "ls_listino3"
    Me.ls_listino3.NTSRepositoryComboBox = Nothing
    Me.ls_listino3.NTSRepositoryItemCheck = Nothing
    Me.ls_listino3.NTSRepositoryItemMemo = Nothing
    Me.ls_listino3.NTSRepositoryItemText = Nothing
    Me.ls_listino3.OptionsColumn.AllowEdit = False
    Me.ls_listino3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_listino3.OptionsColumn.ReadOnly = True
    Me.ls_listino3.OptionsFilter.AllowFilter = False
    Me.ls_listino3.Visible = True
    Me.ls_listino3.VisibleIndex = 30
    '
    'ls_prz3
    '
    Me.ls_prz3.AppearanceCell.Options.UseBackColor = True
    Me.ls_prz3.AppearanceCell.Options.UseTextOptions = True
    Me.ls_prz3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_prz3.Caption = "Prezzo 3"
    Me.ls_prz3.Enabled = True
    Me.ls_prz3.FieldName = "ls_prz3"
    Me.ls_prz3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_prz3.Name = "ls_prz3"
    Me.ls_prz3.NTSRepositoryComboBox = Nothing
    Me.ls_prz3.NTSRepositoryItemCheck = Nothing
    Me.ls_prz3.NTSRepositoryItemMemo = Nothing
    Me.ls_prz3.NTSRepositoryItemText = Nothing
    Me.ls_prz3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_prz3.OptionsFilter.AllowFilter = False
    Me.ls_prz3.Visible = True
    Me.ls_prz3.VisibleIndex = 31
    '
    'ls_daquant3
    '
    Me.ls_daquant3.AppearanceCell.Options.UseBackColor = True
    Me.ls_daquant3.AppearanceCell.Options.UseTextOptions = True
    Me.ls_daquant3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_daquant3.Caption = "Da qta 3"
    Me.ls_daquant3.Enabled = False
    Me.ls_daquant3.FieldName = "ls_daquant3"
    Me.ls_daquant3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_daquant3.Name = "ls_daquant3"
    Me.ls_daquant3.NTSRepositoryComboBox = Nothing
    Me.ls_daquant3.NTSRepositoryItemCheck = Nothing
    Me.ls_daquant3.NTSRepositoryItemMemo = Nothing
    Me.ls_daquant3.NTSRepositoryItemText = Nothing
    Me.ls_daquant3.OptionsColumn.AllowEdit = False
    Me.ls_daquant3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_daquant3.OptionsColumn.ReadOnly = True
    Me.ls_daquant3.OptionsFilter.AllowFilter = False
    '
    'ls_aquant3
    '
    Me.ls_aquant3.AppearanceCell.Options.UseBackColor = True
    Me.ls_aquant3.AppearanceCell.Options.UseTextOptions = True
    Me.ls_aquant3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_aquant3.Caption = "A qta 3"
    Me.ls_aquant3.Enabled = False
    Me.ls_aquant3.FieldName = "ls_aquant3"
    Me.ls_aquant3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_aquant3.Name = "ls_aquant3"
    Me.ls_aquant3.NTSRepositoryComboBox = Nothing
    Me.ls_aquant3.NTSRepositoryItemCheck = Nothing
    Me.ls_aquant3.NTSRepositoryItemMemo = Nothing
    Me.ls_aquant3.NTSRepositoryItemText = Nothing
    Me.ls_aquant3.OptionsColumn.AllowEdit = False
    Me.ls_aquant3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_aquant3.OptionsColumn.ReadOnly = True
    Me.ls_aquant3.OptionsFilter.AllowFilter = False
    '
    'ls_unmis3
    '
    Me.ls_unmis3.AppearanceCell.Options.UseBackColor = True
    Me.ls_unmis3.AppearanceCell.Options.UseTextOptions = True
    Me.ls_unmis3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_unmis3.Caption = "Un. mis. 3"
    Me.ls_unmis3.Enabled = False
    Me.ls_unmis3.FieldName = "ls_unmis3"
    Me.ls_unmis3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_unmis3.Name = "ls_unmis3"
    Me.ls_unmis3.NTSRepositoryComboBox = Nothing
    Me.ls_unmis3.NTSRepositoryItemCheck = Nothing
    Me.ls_unmis3.NTSRepositoryItemMemo = Nothing
    Me.ls_unmis3.NTSRepositoryItemText = Nothing
    Me.ls_unmis3.OptionsColumn.AllowEdit = False
    Me.ls_unmis3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_unmis3.OptionsColumn.ReadOnly = True
    Me.ls_unmis3.OptionsFilter.AllowFilter = False
    Me.ls_unmis3.Visible = True
    Me.ls_unmis3.VisibleIndex = 32
    '
    'ls_codlavo3
    '
    Me.ls_codlavo3.AppearanceCell.Options.UseBackColor = True
    Me.ls_codlavo3.AppearanceCell.Options.UseTextOptions = True
    Me.ls_codlavo3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_codlavo3.Caption = "Lavorazione 3"
    Me.ls_codlavo3.Enabled = False
    Me.ls_codlavo3.FieldName = "ls_codlavo3"
    Me.ls_codlavo3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_codlavo3.Name = "ls_codlavo3"
    Me.ls_codlavo3.NTSRepositoryComboBox = Nothing
    Me.ls_codlavo3.NTSRepositoryItemCheck = Nothing
    Me.ls_codlavo3.NTSRepositoryItemMemo = Nothing
    Me.ls_codlavo3.NTSRepositoryItemText = Nothing
    Me.ls_codlavo3.OptionsColumn.AllowEdit = False
    Me.ls_codlavo3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_codlavo3.OptionsColumn.ReadOnly = True
    Me.ls_codlavo3.OptionsFilter.AllowFilter = False
    Me.ls_codlavo3.Visible = True
    Me.ls_codlavo3.VisibleIndex = 33
    '
    'xx_desclavo3
    '
    Me.xx_desclavo3.AppearanceCell.Options.UseBackColor = True
    Me.xx_desclavo3.AppearanceCell.Options.UseTextOptions = True
    Me.xx_desclavo3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_desclavo3.Caption = "Descr. lavo. 3"
    Me.xx_desclavo3.Enabled = False
    Me.xx_desclavo3.FieldName = "xx_desclavo3"
    Me.xx_desclavo3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_desclavo3.Name = "xx_desclavo3"
    Me.xx_desclavo3.NTSRepositoryComboBox = Nothing
    Me.xx_desclavo3.NTSRepositoryItemCheck = Nothing
    Me.xx_desclavo3.NTSRepositoryItemMemo = Nothing
    Me.xx_desclavo3.NTSRepositoryItemText = Nothing
    Me.xx_desclavo3.OptionsColumn.AllowEdit = False
    Me.xx_desclavo3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_desclavo3.OptionsColumn.ReadOnly = True
    Me.xx_desclavo3.OptionsFilter.AllowFilter = False
    Me.xx_desclavo3.Visible = True
    Me.xx_desclavo3.VisibleIndex = 34
    '
    'ls_tipo3
    '
    Me.ls_tipo3.AppearanceCell.Options.UseBackColor = True
    Me.ls_tipo3.AppearanceCell.Options.UseTextOptions = True
    Me.ls_tipo3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_tipo3.Caption = "Tipo 3"
    Me.ls_tipo3.Enabled = False
    Me.ls_tipo3.FieldName = "ls_tipo3"
    Me.ls_tipo3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_tipo3.Name = "ls_tipo3"
    Me.ls_tipo3.NTSRepositoryComboBox = Nothing
    Me.ls_tipo3.NTSRepositoryItemCheck = Nothing
    Me.ls_tipo3.NTSRepositoryItemMemo = Nothing
    Me.ls_tipo3.NTSRepositoryItemText = Nothing
    Me.ls_tipo3.OptionsColumn.AllowEdit = False
    Me.ls_tipo3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_tipo3.OptionsColumn.ReadOnly = True
    Me.ls_tipo3.OptionsFilter.AllowFilter = False
    Me.ls_tipo3.Visible = True
    Me.ls_tipo3.VisibleIndex = 35
    '
    'ls_dadata3
    '
    Me.ls_dadata3.AppearanceCell.Options.UseBackColor = True
    Me.ls_dadata3.AppearanceCell.Options.UseTextOptions = True
    Me.ls_dadata3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_dadata3.Caption = "Da data 3"
    Me.ls_dadata3.Enabled = True
    Me.ls_dadata3.FieldName = "ls_dadata3"
    Me.ls_dadata3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_dadata3.Name = "ls_dadata3"
    Me.ls_dadata3.NTSRepositoryComboBox = Nothing
    Me.ls_dadata3.NTSRepositoryItemCheck = Nothing
    Me.ls_dadata3.NTSRepositoryItemMemo = Nothing
    Me.ls_dadata3.NTSRepositoryItemText = Nothing
    Me.ls_dadata3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_dadata3.OptionsFilter.AllowFilter = False
    Me.ls_dadata3.Visible = True
    Me.ls_dadata3.VisibleIndex = 36
    '
    'ls_adata3
    '
    Me.ls_adata3.AppearanceCell.Options.UseBackColor = True
    Me.ls_adata3.AppearanceCell.Options.UseTextOptions = True
    Me.ls_adata3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_adata3.Caption = "A data 3"
    Me.ls_adata3.Enabled = False
    Me.ls_adata3.FieldName = "ls_adata3"
    Me.ls_adata3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_adata3.Name = "ls_adata3"
    Me.ls_adata3.NTSRepositoryComboBox = Nothing
    Me.ls_adata3.NTSRepositoryItemCheck = Nothing
    Me.ls_adata3.NTSRepositoryItemMemo = Nothing
    Me.ls_adata3.NTSRepositoryItemText = Nothing
    Me.ls_adata3.OptionsColumn.AllowEdit = False
    Me.ls_adata3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_adata3.OptionsColumn.ReadOnly = True
    Me.ls_adata3.OptionsFilter.AllowFilter = False
    Me.ls_adata3.Visible = True
    Me.ls_adata3.VisibleIndex = 37
    '
    'ls_codvalu3
    '
    Me.ls_codvalu3.AppearanceCell.Options.UseBackColor = True
    Me.ls_codvalu3.AppearanceCell.Options.UseTextOptions = True
    Me.ls_codvalu3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_codvalu3.Caption = "Valuta 3"
    Me.ls_codvalu3.Enabled = False
    Me.ls_codvalu3.FieldName = "ls_codvalu3"
    Me.ls_codvalu3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_codvalu3.Name = "ls_codvalu3"
    Me.ls_codvalu3.NTSRepositoryComboBox = Nothing
    Me.ls_codvalu3.NTSRepositoryItemCheck = Nothing
    Me.ls_codvalu3.NTSRepositoryItemMemo = Nothing
    Me.ls_codvalu3.NTSRepositoryItemText = Nothing
    Me.ls_codvalu3.OptionsColumn.AllowEdit = False
    Me.ls_codvalu3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_codvalu3.OptionsColumn.ReadOnly = True
    Me.ls_codvalu3.OptionsFilter.AllowFilter = False
    Me.ls_codvalu3.Visible = True
    Me.ls_codvalu3.VisibleIndex = 38
    '
    'xx_descvalu3
    '
    Me.xx_descvalu3.AppearanceCell.Options.UseBackColor = True
    Me.xx_descvalu3.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descvalu3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descvalu3.Caption = "Descr. valu. 3"
    Me.xx_descvalu3.Enabled = False
    Me.xx_descvalu3.FieldName = "xx_descvalu3"
    Me.xx_descvalu3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descvalu3.Name = "xx_descvalu3"
    Me.xx_descvalu3.NTSRepositoryComboBox = Nothing
    Me.xx_descvalu3.NTSRepositoryItemCheck = Nothing
    Me.xx_descvalu3.NTSRepositoryItemMemo = Nothing
    Me.xx_descvalu3.NTSRepositoryItemText = Nothing
    Me.xx_descvalu3.OptionsColumn.AllowEdit = False
    Me.xx_descvalu3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descvalu3.OptionsColumn.ReadOnly = True
    Me.xx_descvalu3.OptionsFilter.AllowFilter = False
    Me.xx_descvalu3.Visible = True
    Me.xx_descvalu3.VisibleIndex = 39
    '
    'ls_codpromo3
    '
    Me.ls_codpromo3.AppearanceCell.Options.UseBackColor = True
    Me.ls_codpromo3.AppearanceCell.Options.UseTextOptions = True
    Me.ls_codpromo3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_codpromo3.Caption = "Promozione 3"
    Me.ls_codpromo3.Enabled = False
    Me.ls_codpromo3.FieldName = "ls_codpromo3"
    Me.ls_codpromo3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_codpromo3.Name = "ls_codpromo3"
    Me.ls_codpromo3.NTSRepositoryComboBox = Nothing
    Me.ls_codpromo3.NTSRepositoryItemCheck = Nothing
    Me.ls_codpromo3.NTSRepositoryItemMemo = Nothing
    Me.ls_codpromo3.NTSRepositoryItemText = Nothing
    Me.ls_codpromo3.OptionsColumn.AllowEdit = False
    Me.ls_codpromo3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_codpromo3.OptionsColumn.ReadOnly = True
    Me.ls_codpromo3.OptionsFilter.AllowFilter = False
    Me.ls_codpromo3.Visible = True
    Me.ls_codpromo3.VisibleIndex = 40
    '
    'xx_descpromo3
    '
    Me.xx_descpromo3.AppearanceCell.Options.UseBackColor = True
    Me.xx_descpromo3.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descpromo3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descpromo3.Caption = "Desc. promo. 3"
    Me.xx_descpromo3.Enabled = False
    Me.xx_descpromo3.FieldName = "xx_descpromo3"
    Me.xx_descpromo3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descpromo3.Name = "xx_descpromo3"
    Me.xx_descpromo3.NTSRepositoryComboBox = Nothing
    Me.xx_descpromo3.NTSRepositoryItemCheck = Nothing
    Me.xx_descpromo3.NTSRepositoryItemMemo = Nothing
    Me.xx_descpromo3.NTSRepositoryItemText = Nothing
    Me.xx_descpromo3.OptionsColumn.AllowEdit = False
    Me.xx_descpromo3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descpromo3.OptionsColumn.ReadOnly = True
    Me.xx_descpromo3.OptionsFilter.AllowFilter = False
    Me.xx_descpromo3.Visible = True
    Me.xx_descpromo3.VisibleIndex = 41
    '
    'ls_prznet3
    '
    Me.ls_prznet3.AppearanceCell.Options.UseBackColor = True
    Me.ls_prznet3.AppearanceCell.Options.UseTextOptions = True
    Me.ls_prznet3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_prznet3.Caption = "Prezzo netto 3"
    Me.ls_prznet3.Enabled = False
    Me.ls_prznet3.FieldName = "ls_prznet3"
    Me.ls_prznet3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_prznet3.Name = "ls_prznet3"
    Me.ls_prznet3.NTSRepositoryComboBox = Nothing
    Me.ls_prznet3.NTSRepositoryItemCheck = Nothing
    Me.ls_prznet3.NTSRepositoryItemMemo = Nothing
    Me.ls_prznet3.NTSRepositoryItemText = Nothing
    Me.ls_prznet3.OptionsColumn.AllowEdit = False
    Me.ls_prznet3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_prznet3.OptionsColumn.ReadOnly = True
    Me.ls_prznet3.OptionsFilter.AllowFilter = False
    Me.ls_prznet3.Visible = True
    Me.ls_prznet3.VisibleIndex = 42
    '
    'ls_listino4
    '
    Me.ls_listino4.AppearanceCell.Options.UseBackColor = True
    Me.ls_listino4.AppearanceCell.Options.UseTextOptions = True
    Me.ls_listino4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_listino4.Caption = "Listino 4"
    Me.ls_listino4.Enabled = False
    Me.ls_listino4.FieldName = "ls_listino4"
    Me.ls_listino4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_listino4.Name = "ls_listino4"
    Me.ls_listino4.NTSRepositoryComboBox = Nothing
    Me.ls_listino4.NTSRepositoryItemCheck = Nothing
    Me.ls_listino4.NTSRepositoryItemMemo = Nothing
    Me.ls_listino4.NTSRepositoryItemText = Nothing
    Me.ls_listino4.OptionsColumn.AllowEdit = False
    Me.ls_listino4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_listino4.OptionsColumn.ReadOnly = True
    Me.ls_listino4.OptionsFilter.AllowFilter = False
    Me.ls_listino4.Visible = True
    Me.ls_listino4.VisibleIndex = 43
    '
    'ls_prz4
    '
    Me.ls_prz4.AppearanceCell.Options.UseBackColor = True
    Me.ls_prz4.AppearanceCell.Options.UseTextOptions = True
    Me.ls_prz4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_prz4.Caption = "Prezzo 4"
    Me.ls_prz4.Enabled = True
    Me.ls_prz4.FieldName = "ls_prz4"
    Me.ls_prz4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_prz4.Name = "ls_prz4"
    Me.ls_prz4.NTSRepositoryComboBox = Nothing
    Me.ls_prz4.NTSRepositoryItemCheck = Nothing
    Me.ls_prz4.NTSRepositoryItemMemo = Nothing
    Me.ls_prz4.NTSRepositoryItemText = Nothing
    Me.ls_prz4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_prz4.OptionsFilter.AllowFilter = False
    Me.ls_prz4.Visible = True
    Me.ls_prz4.VisibleIndex = 44
    '
    'ls_daquant4
    '
    Me.ls_daquant4.AppearanceCell.Options.UseBackColor = True
    Me.ls_daquant4.AppearanceCell.Options.UseTextOptions = True
    Me.ls_daquant4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_daquant4.Caption = "Da qta 4"
    Me.ls_daquant4.Enabled = False
    Me.ls_daquant4.FieldName = "ls_daquant4"
    Me.ls_daquant4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_daquant4.Name = "ls_daquant4"
    Me.ls_daquant4.NTSRepositoryComboBox = Nothing
    Me.ls_daquant4.NTSRepositoryItemCheck = Nothing
    Me.ls_daquant4.NTSRepositoryItemMemo = Nothing
    Me.ls_daquant4.NTSRepositoryItemText = Nothing
    Me.ls_daquant4.OptionsColumn.AllowEdit = False
    Me.ls_daquant4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_daquant4.OptionsColumn.ReadOnly = True
    Me.ls_daquant4.OptionsFilter.AllowFilter = False
    '
    'ls_aquant4
    '
    Me.ls_aquant4.AppearanceCell.Options.UseBackColor = True
    Me.ls_aquant4.AppearanceCell.Options.UseTextOptions = True
    Me.ls_aquant4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_aquant4.Caption = "A qta 4"
    Me.ls_aquant4.Enabled = False
    Me.ls_aquant4.FieldName = "ls_aquant4"
    Me.ls_aquant4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_aquant4.Name = "ls_aquant4"
    Me.ls_aquant4.NTSRepositoryComboBox = Nothing
    Me.ls_aquant4.NTSRepositoryItemCheck = Nothing
    Me.ls_aquant4.NTSRepositoryItemMemo = Nothing
    Me.ls_aquant4.NTSRepositoryItemText = Nothing
    Me.ls_aquant4.OptionsColumn.AllowEdit = False
    Me.ls_aquant4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_aquant4.OptionsColumn.ReadOnly = True
    Me.ls_aquant4.OptionsFilter.AllowFilter = False
    '
    'ls_unmis4
    '
    Me.ls_unmis4.AppearanceCell.Options.UseBackColor = True
    Me.ls_unmis4.AppearanceCell.Options.UseTextOptions = True
    Me.ls_unmis4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_unmis4.Caption = "Un. mis. 4"
    Me.ls_unmis4.Enabled = False
    Me.ls_unmis4.FieldName = "ls_unmis4"
    Me.ls_unmis4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_unmis4.Name = "ls_unmis4"
    Me.ls_unmis4.NTSRepositoryComboBox = Nothing
    Me.ls_unmis4.NTSRepositoryItemCheck = Nothing
    Me.ls_unmis4.NTSRepositoryItemMemo = Nothing
    Me.ls_unmis4.NTSRepositoryItemText = Nothing
    Me.ls_unmis4.OptionsColumn.AllowEdit = False
    Me.ls_unmis4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_unmis4.OptionsColumn.ReadOnly = True
    Me.ls_unmis4.OptionsFilter.AllowFilter = False
    Me.ls_unmis4.Visible = True
    Me.ls_unmis4.VisibleIndex = 45
    '
    'ls_codlavo4
    '
    Me.ls_codlavo4.AppearanceCell.Options.UseBackColor = True
    Me.ls_codlavo4.AppearanceCell.Options.UseTextOptions = True
    Me.ls_codlavo4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_codlavo4.Caption = "Lavorazione 4"
    Me.ls_codlavo4.Enabled = False
    Me.ls_codlavo4.FieldName = "ls_codlavo4"
    Me.ls_codlavo4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_codlavo4.Name = "ls_codlavo4"
    Me.ls_codlavo4.NTSRepositoryComboBox = Nothing
    Me.ls_codlavo4.NTSRepositoryItemCheck = Nothing
    Me.ls_codlavo4.NTSRepositoryItemMemo = Nothing
    Me.ls_codlavo4.NTSRepositoryItemText = Nothing
    Me.ls_codlavo4.OptionsColumn.AllowEdit = False
    Me.ls_codlavo4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_codlavo4.OptionsColumn.ReadOnly = True
    Me.ls_codlavo4.OptionsFilter.AllowFilter = False
    Me.ls_codlavo4.Visible = True
    Me.ls_codlavo4.VisibleIndex = 46
    '
    'xx_desclavo4
    '
    Me.xx_desclavo4.AppearanceCell.Options.UseBackColor = True
    Me.xx_desclavo4.AppearanceCell.Options.UseTextOptions = True
    Me.xx_desclavo4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_desclavo4.Caption = "Descr. lavo. 4"
    Me.xx_desclavo4.Enabled = False
    Me.xx_desclavo4.FieldName = "xx_desclavo4"
    Me.xx_desclavo4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_desclavo4.Name = "xx_desclavo4"
    Me.xx_desclavo4.NTSRepositoryComboBox = Nothing
    Me.xx_desclavo4.NTSRepositoryItemCheck = Nothing
    Me.xx_desclavo4.NTSRepositoryItemMemo = Nothing
    Me.xx_desclavo4.NTSRepositoryItemText = Nothing
    Me.xx_desclavo4.OptionsColumn.AllowEdit = False
    Me.xx_desclavo4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_desclavo4.OptionsColumn.ReadOnly = True
    Me.xx_desclavo4.OptionsFilter.AllowFilter = False
    Me.xx_desclavo4.Visible = True
    Me.xx_desclavo4.VisibleIndex = 47
    '
    'ls_tipo4
    '
    Me.ls_tipo4.AppearanceCell.Options.UseBackColor = True
    Me.ls_tipo4.AppearanceCell.Options.UseTextOptions = True
    Me.ls_tipo4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_tipo4.Caption = "Tipo 4"
    Me.ls_tipo4.Enabled = False
    Me.ls_tipo4.FieldName = "ls_tipo4"
    Me.ls_tipo4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_tipo4.Name = "ls_tipo4"
    Me.ls_tipo4.NTSRepositoryComboBox = Nothing
    Me.ls_tipo4.NTSRepositoryItemCheck = Nothing
    Me.ls_tipo4.NTSRepositoryItemMemo = Nothing
    Me.ls_tipo4.NTSRepositoryItemText = Nothing
    Me.ls_tipo4.OptionsColumn.AllowEdit = False
    Me.ls_tipo4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_tipo4.OptionsColumn.ReadOnly = True
    Me.ls_tipo4.OptionsFilter.AllowFilter = False
    Me.ls_tipo4.Visible = True
    Me.ls_tipo4.VisibleIndex = 48
    '
    'ls_dadata4
    '
    Me.ls_dadata4.AppearanceCell.Options.UseBackColor = True
    Me.ls_dadata4.AppearanceCell.Options.UseTextOptions = True
    Me.ls_dadata4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_dadata4.Caption = "Da data 4"
    Me.ls_dadata4.Enabled = True
    Me.ls_dadata4.FieldName = "ls_dadata4"
    Me.ls_dadata4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_dadata4.Name = "ls_dadata4"
    Me.ls_dadata4.NTSRepositoryComboBox = Nothing
    Me.ls_dadata4.NTSRepositoryItemCheck = Nothing
    Me.ls_dadata4.NTSRepositoryItemMemo = Nothing
    Me.ls_dadata4.NTSRepositoryItemText = Nothing
    Me.ls_dadata4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_dadata4.OptionsFilter.AllowFilter = False
    Me.ls_dadata4.Visible = True
    Me.ls_dadata4.VisibleIndex = 49
    '
    'ls_adata4
    '
    Me.ls_adata4.AppearanceCell.Options.UseBackColor = True
    Me.ls_adata4.AppearanceCell.Options.UseTextOptions = True
    Me.ls_adata4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_adata4.Caption = "A data 4"
    Me.ls_adata4.Enabled = False
    Me.ls_adata4.FieldName = "ls_adata4"
    Me.ls_adata4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_adata4.Name = "ls_adata4"
    Me.ls_adata4.NTSRepositoryComboBox = Nothing
    Me.ls_adata4.NTSRepositoryItemCheck = Nothing
    Me.ls_adata4.NTSRepositoryItemMemo = Nothing
    Me.ls_adata4.NTSRepositoryItemText = Nothing
    Me.ls_adata4.OptionsColumn.AllowEdit = False
    Me.ls_adata4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_adata4.OptionsColumn.ReadOnly = True
    Me.ls_adata4.OptionsFilter.AllowFilter = False
    Me.ls_adata4.Visible = True
    Me.ls_adata4.VisibleIndex = 50
    '
    'ls_codvalu4
    '
    Me.ls_codvalu4.AppearanceCell.Options.UseBackColor = True
    Me.ls_codvalu4.AppearanceCell.Options.UseTextOptions = True
    Me.ls_codvalu4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_codvalu4.Caption = "Valuta 4"
    Me.ls_codvalu4.Enabled = False
    Me.ls_codvalu4.FieldName = "ls_codvalu4"
    Me.ls_codvalu4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_codvalu4.Name = "ls_codvalu4"
    Me.ls_codvalu4.NTSRepositoryComboBox = Nothing
    Me.ls_codvalu4.NTSRepositoryItemCheck = Nothing
    Me.ls_codvalu4.NTSRepositoryItemMemo = Nothing
    Me.ls_codvalu4.NTSRepositoryItemText = Nothing
    Me.ls_codvalu4.OptionsColumn.AllowEdit = False
    Me.ls_codvalu4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_codvalu4.OptionsColumn.ReadOnly = True
    Me.ls_codvalu4.OptionsFilter.AllowFilter = False
    Me.ls_codvalu4.Visible = True
    Me.ls_codvalu4.VisibleIndex = 51
    '
    'xx_descvalu4
    '
    Me.xx_descvalu4.AppearanceCell.Options.UseBackColor = True
    Me.xx_descvalu4.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descvalu4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descvalu4.Caption = "Descr. valu. 4"
    Me.xx_descvalu4.Enabled = False
    Me.xx_descvalu4.FieldName = "xx_descvalu4"
    Me.xx_descvalu4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descvalu4.Name = "xx_descvalu4"
    Me.xx_descvalu4.NTSRepositoryComboBox = Nothing
    Me.xx_descvalu4.NTSRepositoryItemCheck = Nothing
    Me.xx_descvalu4.NTSRepositoryItemMemo = Nothing
    Me.xx_descvalu4.NTSRepositoryItemText = Nothing
    Me.xx_descvalu4.OptionsColumn.AllowEdit = False
    Me.xx_descvalu4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descvalu4.OptionsColumn.ReadOnly = True
    Me.xx_descvalu4.OptionsFilter.AllowFilter = False
    Me.xx_descvalu4.Visible = True
    Me.xx_descvalu4.VisibleIndex = 52
    '
    'ls_codpromo4
    '
    Me.ls_codpromo4.AppearanceCell.Options.UseBackColor = True
    Me.ls_codpromo4.AppearanceCell.Options.UseTextOptions = True
    Me.ls_codpromo4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_codpromo4.Caption = "Promozione 4"
    Me.ls_codpromo4.Enabled = False
    Me.ls_codpromo4.FieldName = "ls_codpromo4"
    Me.ls_codpromo4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_codpromo4.Name = "ls_codpromo4"
    Me.ls_codpromo4.NTSRepositoryComboBox = Nothing
    Me.ls_codpromo4.NTSRepositoryItemCheck = Nothing
    Me.ls_codpromo4.NTSRepositoryItemMemo = Nothing
    Me.ls_codpromo4.NTSRepositoryItemText = Nothing
    Me.ls_codpromo4.OptionsColumn.AllowEdit = False
    Me.ls_codpromo4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_codpromo4.OptionsColumn.ReadOnly = True
    Me.ls_codpromo4.OptionsFilter.AllowFilter = False
    Me.ls_codpromo4.Visible = True
    Me.ls_codpromo4.VisibleIndex = 53
    '
    'xx_descpromo4
    '
    Me.xx_descpromo4.AppearanceCell.Options.UseBackColor = True
    Me.xx_descpromo4.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descpromo4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descpromo4.Caption = "Desc. promo. 4"
    Me.xx_descpromo4.Enabled = False
    Me.xx_descpromo4.FieldName = "xx_descpromo4"
    Me.xx_descpromo4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descpromo4.Name = "xx_descpromo4"
    Me.xx_descpromo4.NTSRepositoryComboBox = Nothing
    Me.xx_descpromo4.NTSRepositoryItemCheck = Nothing
    Me.xx_descpromo4.NTSRepositoryItemMemo = Nothing
    Me.xx_descpromo4.NTSRepositoryItemText = Nothing
    Me.xx_descpromo4.OptionsColumn.AllowEdit = False
    Me.xx_descpromo4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descpromo4.OptionsColumn.ReadOnly = True
    Me.xx_descpromo4.OptionsFilter.AllowFilter = False
    Me.xx_descpromo4.Visible = True
    Me.xx_descpromo4.VisibleIndex = 54
    '
    'ls_prznet4
    '
    Me.ls_prznet4.AppearanceCell.Options.UseBackColor = True
    Me.ls_prznet4.AppearanceCell.Options.UseTextOptions = True
    Me.ls_prznet4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_prznet4.Caption = "Prezzo netto 4"
    Me.ls_prznet4.Enabled = False
    Me.ls_prznet4.FieldName = "ls_prznet4"
    Me.ls_prznet4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_prznet4.Name = "ls_prznet4"
    Me.ls_prznet4.NTSRepositoryComboBox = Nothing
    Me.ls_prznet4.NTSRepositoryItemCheck = Nothing
    Me.ls_prznet4.NTSRepositoryItemMemo = Nothing
    Me.ls_prznet4.NTSRepositoryItemText = Nothing
    Me.ls_prznet4.OptionsColumn.AllowEdit = False
    Me.ls_prznet4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_prznet4.OptionsColumn.ReadOnly = True
    Me.ls_prznet4.OptionsFilter.AllowFilter = False
    Me.ls_prznet4.Visible = True
    Me.ls_prznet4.VisibleIndex = 55
    '
    'ls_listino5
    '
    Me.ls_listino5.AppearanceCell.Options.UseBackColor = True
    Me.ls_listino5.AppearanceCell.Options.UseTextOptions = True
    Me.ls_listino5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_listino5.Caption = "Listino 5"
    Me.ls_listino5.Enabled = False
    Me.ls_listino5.FieldName = "ls_listino5"
    Me.ls_listino5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_listino5.Name = "ls_listino5"
    Me.ls_listino5.NTSRepositoryComboBox = Nothing
    Me.ls_listino5.NTSRepositoryItemCheck = Nothing
    Me.ls_listino5.NTSRepositoryItemMemo = Nothing
    Me.ls_listino5.NTSRepositoryItemText = Nothing
    Me.ls_listino5.OptionsColumn.AllowEdit = False
    Me.ls_listino5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_listino5.OptionsColumn.ReadOnly = True
    Me.ls_listino5.OptionsFilter.AllowFilter = False
    Me.ls_listino5.Visible = True
    Me.ls_listino5.VisibleIndex = 56
    '
    'ls_prz5
    '
    Me.ls_prz5.AppearanceCell.Options.UseBackColor = True
    Me.ls_prz5.AppearanceCell.Options.UseTextOptions = True
    Me.ls_prz5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_prz5.Caption = "Prezzo 5"
    Me.ls_prz5.Enabled = True
    Me.ls_prz5.FieldName = "ls_prz5"
    Me.ls_prz5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_prz5.Name = "ls_prz5"
    Me.ls_prz5.NTSRepositoryComboBox = Nothing
    Me.ls_prz5.NTSRepositoryItemCheck = Nothing
    Me.ls_prz5.NTSRepositoryItemMemo = Nothing
    Me.ls_prz5.NTSRepositoryItemText = Nothing
    Me.ls_prz5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_prz5.OptionsFilter.AllowFilter = False
    Me.ls_prz5.Visible = True
    Me.ls_prz5.VisibleIndex = 57
    '
    'ls_daquant5
    '
    Me.ls_daquant5.AppearanceCell.Options.UseBackColor = True
    Me.ls_daquant5.AppearanceCell.Options.UseTextOptions = True
    Me.ls_daquant5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_daquant5.Caption = "Da qta 5"
    Me.ls_daquant5.Enabled = False
    Me.ls_daquant5.FieldName = "ls_daquant5"
    Me.ls_daquant5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_daquant5.Name = "ls_daquant5"
    Me.ls_daquant5.NTSRepositoryComboBox = Nothing
    Me.ls_daquant5.NTSRepositoryItemCheck = Nothing
    Me.ls_daquant5.NTSRepositoryItemMemo = Nothing
    Me.ls_daquant5.NTSRepositoryItemText = Nothing
    Me.ls_daquant5.OptionsColumn.AllowEdit = False
    Me.ls_daquant5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_daquant5.OptionsColumn.ReadOnly = True
    Me.ls_daquant5.OptionsFilter.AllowFilter = False
    '
    'ls_aquant5
    '
    Me.ls_aquant5.AppearanceCell.Options.UseBackColor = True
    Me.ls_aquant5.AppearanceCell.Options.UseTextOptions = True
    Me.ls_aquant5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_aquant5.Caption = "A qta 5"
    Me.ls_aquant5.Enabled = False
    Me.ls_aquant5.FieldName = "ls_aquant5"
    Me.ls_aquant5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_aquant5.Name = "ls_aquant5"
    Me.ls_aquant5.NTSRepositoryComboBox = Nothing
    Me.ls_aquant5.NTSRepositoryItemCheck = Nothing
    Me.ls_aquant5.NTSRepositoryItemMemo = Nothing
    Me.ls_aquant5.NTSRepositoryItemText = Nothing
    Me.ls_aquant5.OptionsColumn.AllowEdit = False
    Me.ls_aquant5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_aquant5.OptionsColumn.ReadOnly = True
    Me.ls_aquant5.OptionsFilter.AllowFilter = False
    '
    'ls_unmis5
    '
    Me.ls_unmis5.AppearanceCell.Options.UseBackColor = True
    Me.ls_unmis5.AppearanceCell.Options.UseTextOptions = True
    Me.ls_unmis5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_unmis5.Caption = "Un. mis. 5"
    Me.ls_unmis5.Enabled = False
    Me.ls_unmis5.FieldName = "ls_unmis5"
    Me.ls_unmis5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_unmis5.Name = "ls_unmis5"
    Me.ls_unmis5.NTSRepositoryComboBox = Nothing
    Me.ls_unmis5.NTSRepositoryItemCheck = Nothing
    Me.ls_unmis5.NTSRepositoryItemMemo = Nothing
    Me.ls_unmis5.NTSRepositoryItemText = Nothing
    Me.ls_unmis5.OptionsColumn.AllowEdit = False
    Me.ls_unmis5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_unmis5.OptionsColumn.ReadOnly = True
    Me.ls_unmis5.OptionsFilter.AllowFilter = False
    Me.ls_unmis5.Visible = True
    Me.ls_unmis5.VisibleIndex = 58
    '
    'ls_codlavo5
    '
    Me.ls_codlavo5.AppearanceCell.Options.UseBackColor = True
    Me.ls_codlavo5.AppearanceCell.Options.UseTextOptions = True
    Me.ls_codlavo5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_codlavo5.Caption = "Lavorazione 5"
    Me.ls_codlavo5.Enabled = False
    Me.ls_codlavo5.FieldName = "ls_codlavo5"
    Me.ls_codlavo5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_codlavo5.Name = "ls_codlavo5"
    Me.ls_codlavo5.NTSRepositoryComboBox = Nothing
    Me.ls_codlavo5.NTSRepositoryItemCheck = Nothing
    Me.ls_codlavo5.NTSRepositoryItemMemo = Nothing
    Me.ls_codlavo5.NTSRepositoryItemText = Nothing
    Me.ls_codlavo5.OptionsColumn.AllowEdit = False
    Me.ls_codlavo5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_codlavo5.OptionsColumn.ReadOnly = True
    Me.ls_codlavo5.OptionsFilter.AllowFilter = False
    Me.ls_codlavo5.Visible = True
    Me.ls_codlavo5.VisibleIndex = 59
    '
    'xx_desclavo5
    '
    Me.xx_desclavo5.AppearanceCell.Options.UseBackColor = True
    Me.xx_desclavo5.AppearanceCell.Options.UseTextOptions = True
    Me.xx_desclavo5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_desclavo5.Caption = "Descr. lavo. 5"
    Me.xx_desclavo5.Enabled = False
    Me.xx_desclavo5.FieldName = "xx_desclavo5"
    Me.xx_desclavo5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_desclavo5.Name = "xx_desclavo5"
    Me.xx_desclavo5.NTSRepositoryComboBox = Nothing
    Me.xx_desclavo5.NTSRepositoryItemCheck = Nothing
    Me.xx_desclavo5.NTSRepositoryItemMemo = Nothing
    Me.xx_desclavo5.NTSRepositoryItemText = Nothing
    Me.xx_desclavo5.OptionsColumn.AllowEdit = False
    Me.xx_desclavo5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_desclavo5.OptionsColumn.ReadOnly = True
    Me.xx_desclavo5.OptionsFilter.AllowFilter = False
    Me.xx_desclavo5.Visible = True
    Me.xx_desclavo5.VisibleIndex = 60
    '
    'ls_tipo5
    '
    Me.ls_tipo5.AppearanceCell.Options.UseBackColor = True
    Me.ls_tipo5.AppearanceCell.Options.UseTextOptions = True
    Me.ls_tipo5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_tipo5.Caption = "Tipo 5"
    Me.ls_tipo5.Enabled = False
    Me.ls_tipo5.FieldName = "ls_tipo5"
    Me.ls_tipo5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_tipo5.Name = "ls_tipo5"
    Me.ls_tipo5.NTSRepositoryComboBox = Nothing
    Me.ls_tipo5.NTSRepositoryItemCheck = Nothing
    Me.ls_tipo5.NTSRepositoryItemMemo = Nothing
    Me.ls_tipo5.NTSRepositoryItemText = Nothing
    Me.ls_tipo5.OptionsColumn.AllowEdit = False
    Me.ls_tipo5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_tipo5.OptionsColumn.ReadOnly = True
    Me.ls_tipo5.OptionsFilter.AllowFilter = False
    Me.ls_tipo5.Visible = True
    Me.ls_tipo5.VisibleIndex = 61
    '
    'ls_dadata5
    '
    Me.ls_dadata5.AppearanceCell.Options.UseBackColor = True
    Me.ls_dadata5.AppearanceCell.Options.UseTextOptions = True
    Me.ls_dadata5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_dadata5.Caption = "Da data 5"
    Me.ls_dadata5.Enabled = True
    Me.ls_dadata5.FieldName = "ls_dadata5"
    Me.ls_dadata5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_dadata5.Name = "ls_dadata5"
    Me.ls_dadata5.NTSRepositoryComboBox = Nothing
    Me.ls_dadata5.NTSRepositoryItemCheck = Nothing
    Me.ls_dadata5.NTSRepositoryItemMemo = Nothing
    Me.ls_dadata5.NTSRepositoryItemText = Nothing
    Me.ls_dadata5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_dadata5.OptionsFilter.AllowFilter = False
    Me.ls_dadata5.Visible = True
    Me.ls_dadata5.VisibleIndex = 62
    '
    'ls_adata5
    '
    Me.ls_adata5.AppearanceCell.Options.UseBackColor = True
    Me.ls_adata5.AppearanceCell.Options.UseTextOptions = True
    Me.ls_adata5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_adata5.Caption = "A data 5"
    Me.ls_adata5.Enabled = False
    Me.ls_adata5.FieldName = "ls_adata5"
    Me.ls_adata5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_adata5.Name = "ls_adata5"
    Me.ls_adata5.NTSRepositoryComboBox = Nothing
    Me.ls_adata5.NTSRepositoryItemCheck = Nothing
    Me.ls_adata5.NTSRepositoryItemMemo = Nothing
    Me.ls_adata5.NTSRepositoryItemText = Nothing
    Me.ls_adata5.OptionsColumn.AllowEdit = False
    Me.ls_adata5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_adata5.OptionsColumn.ReadOnly = True
    Me.ls_adata5.OptionsFilter.AllowFilter = False
    Me.ls_adata5.Visible = True
    Me.ls_adata5.VisibleIndex = 63
    '
    'ls_codvalu5
    '
    Me.ls_codvalu5.AppearanceCell.Options.UseBackColor = True
    Me.ls_codvalu5.AppearanceCell.Options.UseTextOptions = True
    Me.ls_codvalu5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_codvalu5.Caption = "Valuta 5"
    Me.ls_codvalu5.Enabled = False
    Me.ls_codvalu5.FieldName = "ls_codvalu5"
    Me.ls_codvalu5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_codvalu5.Name = "ls_codvalu5"
    Me.ls_codvalu5.NTSRepositoryComboBox = Nothing
    Me.ls_codvalu5.NTSRepositoryItemCheck = Nothing
    Me.ls_codvalu5.NTSRepositoryItemMemo = Nothing
    Me.ls_codvalu5.NTSRepositoryItemText = Nothing
    Me.ls_codvalu5.OptionsColumn.AllowEdit = False
    Me.ls_codvalu5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_codvalu5.OptionsColumn.ReadOnly = True
    Me.ls_codvalu5.OptionsFilter.AllowFilter = False
    Me.ls_codvalu5.Visible = True
    Me.ls_codvalu5.VisibleIndex = 64
    '
    'xx_descvalu5
    '
    Me.xx_descvalu5.AppearanceCell.Options.UseBackColor = True
    Me.xx_descvalu5.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descvalu5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descvalu5.Caption = "Descr. valu. 5"
    Me.xx_descvalu5.Enabled = False
    Me.xx_descvalu5.FieldName = "xx_descvalu5"
    Me.xx_descvalu5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descvalu5.Name = "xx_descvalu5"
    Me.xx_descvalu5.NTSRepositoryComboBox = Nothing
    Me.xx_descvalu5.NTSRepositoryItemCheck = Nothing
    Me.xx_descvalu5.NTSRepositoryItemMemo = Nothing
    Me.xx_descvalu5.NTSRepositoryItemText = Nothing
    Me.xx_descvalu5.OptionsColumn.AllowEdit = False
    Me.xx_descvalu5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descvalu5.OptionsColumn.ReadOnly = True
    Me.xx_descvalu5.OptionsFilter.AllowFilter = False
    Me.xx_descvalu5.Visible = True
    Me.xx_descvalu5.VisibleIndex = 65
    '
    'ls_codpromo5
    '
    Me.ls_codpromo5.AppearanceCell.Options.UseBackColor = True
    Me.ls_codpromo5.AppearanceCell.Options.UseTextOptions = True
    Me.ls_codpromo5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_codpromo5.Caption = "Promozione 5"
    Me.ls_codpromo5.Enabled = False
    Me.ls_codpromo5.FieldName = "ls_codpromo5"
    Me.ls_codpromo5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_codpromo5.Name = "ls_codpromo5"
    Me.ls_codpromo5.NTSRepositoryComboBox = Nothing
    Me.ls_codpromo5.NTSRepositoryItemCheck = Nothing
    Me.ls_codpromo5.NTSRepositoryItemMemo = Nothing
    Me.ls_codpromo5.NTSRepositoryItemText = Nothing
    Me.ls_codpromo5.OptionsColumn.AllowEdit = False
    Me.ls_codpromo5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_codpromo5.OptionsColumn.ReadOnly = True
    Me.ls_codpromo5.OptionsFilter.AllowFilter = False
    Me.ls_codpromo5.Visible = True
    Me.ls_codpromo5.VisibleIndex = 66
    '
    'xx_descpromo5
    '
    Me.xx_descpromo5.AppearanceCell.Options.UseBackColor = True
    Me.xx_descpromo5.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descpromo5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descpromo5.Caption = "Desc. promo. 5"
    Me.xx_descpromo5.Enabled = False
    Me.xx_descpromo5.FieldName = "xx_descpromo5"
    Me.xx_descpromo5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descpromo5.Name = "xx_descpromo5"
    Me.xx_descpromo5.NTSRepositoryComboBox = Nothing
    Me.xx_descpromo5.NTSRepositoryItemCheck = Nothing
    Me.xx_descpromo5.NTSRepositoryItemMemo = Nothing
    Me.xx_descpromo5.NTSRepositoryItemText = Nothing
    Me.xx_descpromo5.OptionsColumn.AllowEdit = False
    Me.xx_descpromo5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descpromo5.OptionsColumn.ReadOnly = True
    Me.xx_descpromo5.OptionsFilter.AllowFilter = False
    Me.xx_descpromo5.Visible = True
    Me.xx_descpromo5.VisibleIndex = 67
    '
    'ls_prznet5
    '
    Me.ls_prznet5.AppearanceCell.Options.UseBackColor = True
    Me.ls_prznet5.AppearanceCell.Options.UseTextOptions = True
    Me.ls_prznet5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_prznet5.Caption = "Prezzo netto 5"
    Me.ls_prznet5.Enabled = False
    Me.ls_prznet5.FieldName = "ls_prznet5"
    Me.ls_prznet5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_prznet5.Name = "ls_prznet5"
    Me.ls_prznet5.NTSRepositoryComboBox = Nothing
    Me.ls_prznet5.NTSRepositoryItemCheck = Nothing
    Me.ls_prznet5.NTSRepositoryItemMemo = Nothing
    Me.ls_prznet5.NTSRepositoryItemText = Nothing
    Me.ls_prznet5.OptionsColumn.AllowEdit = False
    Me.ls_prznet5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_prznet5.OptionsColumn.ReadOnly = True
    Me.ls_prznet5.OptionsFilter.AllowFilter = False
    Me.ls_prznet5.Visible = True
    Me.ls_prznet5.VisibleIndex = 68
    '
    'ls_scont1
    '
    Me.ls_scont1.AppearanceCell.Options.UseBackColor = True
    Me.ls_scont1.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scont1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scont1.Caption = "Sconto 1 I"
    Me.ls_scont1.Enabled = True
    Me.ls_scont1.FieldName = "ls_scont1"
    Me.ls_scont1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scont1.Name = "ls_scont1"
    Me.ls_scont1.NTSRepositoryComboBox = Nothing
    Me.ls_scont1.NTSRepositoryItemCheck = Nothing
    Me.ls_scont1.NTSRepositoryItemMemo = Nothing
    Me.ls_scont1.NTSRepositoryItemText = Nothing
    Me.ls_scont1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scont1.OptionsFilter.AllowFilter = False
    Me.ls_scont1.Visible = True
    Me.ls_scont1.VisibleIndex = 69
    '
    'ls_scont2
    '
    Me.ls_scont2.AppearanceCell.Options.UseBackColor = True
    Me.ls_scont2.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scont2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scont2.Caption = "Sconto 2 I"
    Me.ls_scont2.Enabled = True
    Me.ls_scont2.FieldName = "ls_scont2"
    Me.ls_scont2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scont2.Name = "ls_scont2"
    Me.ls_scont2.NTSRepositoryComboBox = Nothing
    Me.ls_scont2.NTSRepositoryItemCheck = Nothing
    Me.ls_scont2.NTSRepositoryItemMemo = Nothing
    Me.ls_scont2.NTSRepositoryItemText = Nothing
    Me.ls_scont2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scont2.OptionsFilter.AllowFilter = False
    Me.ls_scont2.Visible = True
    Me.ls_scont2.VisibleIndex = 70
    '
    'ls_scont3
    '
    Me.ls_scont3.AppearanceCell.Options.UseBackColor = True
    Me.ls_scont3.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scont3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scont3.Caption = "Sconto 3 I"
    Me.ls_scont3.Enabled = True
    Me.ls_scont3.FieldName = "ls_scont3"
    Me.ls_scont3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scont3.Name = "ls_scont3"
    Me.ls_scont3.NTSRepositoryComboBox = Nothing
    Me.ls_scont3.NTSRepositoryItemCheck = Nothing
    Me.ls_scont3.NTSRepositoryItemMemo = Nothing
    Me.ls_scont3.NTSRepositoryItemText = Nothing
    Me.ls_scont3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scont3.OptionsFilter.AllowFilter = False
    Me.ls_scont3.Visible = True
    Me.ls_scont3.VisibleIndex = 71
    '
    'ls_scont4
    '
    Me.ls_scont4.AppearanceCell.Options.UseBackColor = True
    Me.ls_scont4.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scont4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scont4.Caption = "Sconto 4 I"
    Me.ls_scont4.Enabled = True
    Me.ls_scont4.FieldName = "ls_scont4"
    Me.ls_scont4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scont4.Name = "ls_scont4"
    Me.ls_scont4.NTSRepositoryComboBox = Nothing
    Me.ls_scont4.NTSRepositoryItemCheck = Nothing
    Me.ls_scont4.NTSRepositoryItemMemo = Nothing
    Me.ls_scont4.NTSRepositoryItemText = Nothing
    Me.ls_scont4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scont4.OptionsFilter.AllowFilter = False
    '
    'ls_scont5
    '
    Me.ls_scont5.AppearanceCell.Options.UseBackColor = True
    Me.ls_scont5.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scont5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scont5.Caption = "Sconto 5 I"
    Me.ls_scont5.Enabled = True
    Me.ls_scont5.FieldName = "ls_scont5"
    Me.ls_scont5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scont5.Name = "ls_scont5"
    Me.ls_scont5.NTSRepositoryComboBox = Nothing
    Me.ls_scont5.NTSRepositoryItemCheck = Nothing
    Me.ls_scont5.NTSRepositoryItemMemo = Nothing
    Me.ls_scont5.NTSRepositoryItemText = Nothing
    Me.ls_scont5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scont5.OptionsFilter.AllowFilter = False
    '
    'ls_scont6
    '
    Me.ls_scont6.AppearanceCell.Options.UseBackColor = True
    Me.ls_scont6.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scont6.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scont6.Caption = "Sconto 6 I"
    Me.ls_scont6.Enabled = True
    Me.ls_scont6.FieldName = "ls_scont6"
    Me.ls_scont6.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scont6.Name = "ls_scont6"
    Me.ls_scont6.NTSRepositoryComboBox = Nothing
    Me.ls_scont6.NTSRepositoryItemCheck = Nothing
    Me.ls_scont6.NTSRepositoryItemMemo = Nothing
    Me.ls_scont6.NTSRepositoryItemText = Nothing
    Me.ls_scont6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scont6.OptionsFilter.AllowFilter = False
    '
    'ls_scdaquant
    '
    Me.ls_scdaquant.AppearanceCell.Options.UseBackColor = True
    Me.ls_scdaquant.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scdaquant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scdaquant.Caption = "Da qta I"
    Me.ls_scdaquant.Enabled = False
    Me.ls_scdaquant.FieldName = "ls_scdaquant"
    Me.ls_scdaquant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scdaquant.Name = "ls_scdaquant"
    Me.ls_scdaquant.NTSRepositoryComboBox = Nothing
    Me.ls_scdaquant.NTSRepositoryItemCheck = Nothing
    Me.ls_scdaquant.NTSRepositoryItemMemo = Nothing
    Me.ls_scdaquant.NTSRepositoryItemText = Nothing
    Me.ls_scdaquant.OptionsColumn.AllowEdit = False
    Me.ls_scdaquant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scdaquant.OptionsColumn.ReadOnly = True
    Me.ls_scdaquant.OptionsFilter.AllowFilter = False
    '
    'ls_scaquant
    '
    Me.ls_scaquant.AppearanceCell.Options.UseBackColor = True
    Me.ls_scaquant.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scaquant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scaquant.Caption = "A qta I"
    Me.ls_scaquant.Enabled = False
    Me.ls_scaquant.FieldName = "ls_scaquant"
    Me.ls_scaquant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scaquant.Name = "ls_scaquant"
    Me.ls_scaquant.NTSRepositoryComboBox = Nothing
    Me.ls_scaquant.NTSRepositoryItemCheck = Nothing
    Me.ls_scaquant.NTSRepositoryItemMemo = Nothing
    Me.ls_scaquant.NTSRepositoryItemText = Nothing
    Me.ls_scaquant.OptionsColumn.AllowEdit = False
    Me.ls_scaquant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scaquant.OptionsColumn.ReadOnly = True
    Me.ls_scaquant.OptionsFilter.AllowFilter = False
    '
    'ls_tiposc
    '
    Me.ls_tiposc.AppearanceCell.Options.UseBackColor = True
    Me.ls_tiposc.AppearanceCell.Options.UseTextOptions = True
    Me.ls_tiposc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_tiposc.Caption = "Tipo I"
    Me.ls_tiposc.Enabled = False
    Me.ls_tiposc.FieldName = "ls_tiposc"
    Me.ls_tiposc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_tiposc.Name = "ls_tiposc"
    Me.ls_tiposc.NTSRepositoryComboBox = Nothing
    Me.ls_tiposc.NTSRepositoryItemCheck = Nothing
    Me.ls_tiposc.NTSRepositoryItemMemo = Nothing
    Me.ls_tiposc.NTSRepositoryItemText = Nothing
    Me.ls_tiposc.OptionsColumn.AllowEdit = False
    Me.ls_tiposc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_tiposc.OptionsColumn.ReadOnly = True
    Me.ls_tiposc.OptionsFilter.AllowFilter = False
    Me.ls_tiposc.Visible = True
    Me.ls_tiposc.VisibleIndex = 72
    '
    'ls_dadatasc
    '
    Me.ls_dadatasc.AppearanceCell.Options.UseBackColor = True
    Me.ls_dadatasc.AppearanceCell.Options.UseTextOptions = True
    Me.ls_dadatasc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_dadatasc.Caption = "Da data I"
    Me.ls_dadatasc.Enabled = True
    Me.ls_dadatasc.FieldName = "ls_dadatasc"
    Me.ls_dadatasc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_dadatasc.Name = "ls_dadatasc"
    Me.ls_dadatasc.NTSRepositoryComboBox = Nothing
    Me.ls_dadatasc.NTSRepositoryItemCheck = Nothing
    Me.ls_dadatasc.NTSRepositoryItemMemo = Nothing
    Me.ls_dadatasc.NTSRepositoryItemText = Nothing
    Me.ls_dadatasc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_dadatasc.OptionsFilter.AllowFilter = False
    Me.ls_dadatasc.Visible = True
    Me.ls_dadatasc.VisibleIndex = 73
    '
    'ls_adatasc
    '
    Me.ls_adatasc.AppearanceCell.Options.UseBackColor = True
    Me.ls_adatasc.AppearanceCell.Options.UseTextOptions = True
    Me.ls_adatasc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_adatasc.Caption = "A data I"
    Me.ls_adatasc.Enabled = True
    Me.ls_adatasc.FieldName = "ls_adatasc"
    Me.ls_adatasc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_adatasc.Name = "ls_adatasc"
    Me.ls_adatasc.NTSRepositoryComboBox = Nothing
    Me.ls_adatasc.NTSRepositoryItemCheck = Nothing
    Me.ls_adatasc.NTSRepositoryItemMemo = Nothing
    Me.ls_adatasc.NTSRepositoryItemText = Nothing
    Me.ls_adatasc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_adatasc.OptionsFilter.AllowFilter = False
    Me.ls_adatasc.Visible = True
    Me.ls_adatasc.VisibleIndex = 74
    '
    'ls_codtpro
    '
    Me.ls_codtpro.AppearanceCell.Options.UseBackColor = True
    Me.ls_codtpro.AppearanceCell.Options.UseTextOptions = True
    Me.ls_codtpro.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_codtpro.Caption = "Promozione I"
    Me.ls_codtpro.Enabled = False
    Me.ls_codtpro.FieldName = "ls_codtpro"
    Me.ls_codtpro.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_codtpro.Name = "ls_codtpro"
    Me.ls_codtpro.NTSRepositoryComboBox = Nothing
    Me.ls_codtpro.NTSRepositoryItemCheck = Nothing
    Me.ls_codtpro.NTSRepositoryItemMemo = Nothing
    Me.ls_codtpro.NTSRepositoryItemText = Nothing
    Me.ls_codtpro.OptionsColumn.AllowEdit = False
    Me.ls_codtpro.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_codtpro.OptionsColumn.ReadOnly = True
    Me.ls_codtpro.OptionsFilter.AllowFilter = False
    Me.ls_codtpro.Visible = True
    Me.ls_codtpro.VisibleIndex = 75
    '
    'xx_descpromo
    '
    Me.xx_descpromo.AppearanceCell.Options.UseBackColor = True
    Me.xx_descpromo.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descpromo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descpromo.Caption = "Descr. promozione I"
    Me.xx_descpromo.Enabled = False
    Me.xx_descpromo.FieldName = "xx_descpromo"
    Me.xx_descpromo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descpromo.Name = "xx_descpromo"
    Me.xx_descpromo.NTSRepositoryComboBox = Nothing
    Me.xx_descpromo.NTSRepositoryItemCheck = Nothing
    Me.xx_descpromo.NTSRepositoryItemMemo = Nothing
    Me.xx_descpromo.NTSRepositoryItemText = Nothing
    Me.xx_descpromo.OptionsColumn.AllowEdit = False
    Me.xx_descpromo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descpromo.OptionsColumn.ReadOnly = True
    Me.xx_descpromo.OptionsFilter.AllowFilter = False
    Me.xx_descpromo.Visible = True
    Me.xx_descpromo.VisibleIndex = 76
    '
    'ls_scont1_2
    '
    Me.ls_scont1_2.AppearanceCell.Options.UseBackColor = True
    Me.ls_scont1_2.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scont1_2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scont1_2.Caption = "Sconto 1 II"
    Me.ls_scont1_2.Enabled = True
    Me.ls_scont1_2.FieldName = "ls_scont1_2"
    Me.ls_scont1_2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scont1_2.Name = "ls_scont1_2"
    Me.ls_scont1_2.NTSRepositoryComboBox = Nothing
    Me.ls_scont1_2.NTSRepositoryItemCheck = Nothing
    Me.ls_scont1_2.NTSRepositoryItemMemo = Nothing
    Me.ls_scont1_2.NTSRepositoryItemText = Nothing
    Me.ls_scont1_2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scont1_2.OptionsFilter.AllowFilter = False
    Me.ls_scont1_2.Visible = True
    Me.ls_scont1_2.VisibleIndex = 77
    '
    'ls_scont2_2
    '
    Me.ls_scont2_2.AppearanceCell.Options.UseBackColor = True
    Me.ls_scont2_2.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scont2_2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scont2_2.Caption = "Sconto 2 II"
    Me.ls_scont2_2.Enabled = True
    Me.ls_scont2_2.FieldName = "ls_scont2_2"
    Me.ls_scont2_2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scont2_2.Name = "ls_scont2_2"
    Me.ls_scont2_2.NTSRepositoryComboBox = Nothing
    Me.ls_scont2_2.NTSRepositoryItemCheck = Nothing
    Me.ls_scont2_2.NTSRepositoryItemMemo = Nothing
    Me.ls_scont2_2.NTSRepositoryItemText = Nothing
    Me.ls_scont2_2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scont2_2.OptionsFilter.AllowFilter = False
    Me.ls_scont2_2.Visible = True
    Me.ls_scont2_2.VisibleIndex = 78
    '
    'ls_scont3_2
    '
    Me.ls_scont3_2.AppearanceCell.Options.UseBackColor = True
    Me.ls_scont3_2.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scont3_2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scont3_2.Caption = "Sconto 3 II"
    Me.ls_scont3_2.Enabled = True
    Me.ls_scont3_2.FieldName = "ls_scont3_2"
    Me.ls_scont3_2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scont3_2.Name = "ls_scont3_2"
    Me.ls_scont3_2.NTSRepositoryComboBox = Nothing
    Me.ls_scont3_2.NTSRepositoryItemCheck = Nothing
    Me.ls_scont3_2.NTSRepositoryItemMemo = Nothing
    Me.ls_scont3_2.NTSRepositoryItemText = Nothing
    Me.ls_scont3_2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scont3_2.OptionsFilter.AllowFilter = False
    Me.ls_scont3_2.Visible = True
    Me.ls_scont3_2.VisibleIndex = 79
    '
    'ls_scont4_2
    '
    Me.ls_scont4_2.AppearanceCell.Options.UseBackColor = True
    Me.ls_scont4_2.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scont4_2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scont4_2.Caption = "Sconto 4 II"
    Me.ls_scont4_2.Enabled = True
    Me.ls_scont4_2.FieldName = "ls_scont4_2"
    Me.ls_scont4_2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scont4_2.Name = "ls_scont4_2"
    Me.ls_scont4_2.NTSRepositoryComboBox = Nothing
    Me.ls_scont4_2.NTSRepositoryItemCheck = Nothing
    Me.ls_scont4_2.NTSRepositoryItemMemo = Nothing
    Me.ls_scont4_2.NTSRepositoryItemText = Nothing
    Me.ls_scont4_2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scont4_2.OptionsFilter.AllowFilter = False
    '
    'ls_scont5_2
    '
    Me.ls_scont5_2.AppearanceCell.Options.UseBackColor = True
    Me.ls_scont5_2.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scont5_2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scont5_2.Caption = "Sconto 5 II"
    Me.ls_scont5_2.Enabled = True
    Me.ls_scont5_2.FieldName = "ls_scont5_2"
    Me.ls_scont5_2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scont5_2.Name = "ls_scont5_2"
    Me.ls_scont5_2.NTSRepositoryComboBox = Nothing
    Me.ls_scont5_2.NTSRepositoryItemCheck = Nothing
    Me.ls_scont5_2.NTSRepositoryItemMemo = Nothing
    Me.ls_scont5_2.NTSRepositoryItemText = Nothing
    Me.ls_scont5_2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scont5_2.OptionsFilter.AllowFilter = False
    '
    'ls_scont6_2
    '
    Me.ls_scont6_2.AppearanceCell.Options.UseBackColor = True
    Me.ls_scont6_2.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scont6_2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scont6_2.Caption = "Sconto 6 II"
    Me.ls_scont6_2.Enabled = True
    Me.ls_scont6_2.FieldName = "ls_scont6_2"
    Me.ls_scont6_2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scont6_2.Name = "ls_scont6_2"
    Me.ls_scont6_2.NTSRepositoryComboBox = Nothing
    Me.ls_scont6_2.NTSRepositoryItemCheck = Nothing
    Me.ls_scont6_2.NTSRepositoryItemMemo = Nothing
    Me.ls_scont6_2.NTSRepositoryItemText = Nothing
    Me.ls_scont6_2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scont6_2.OptionsFilter.AllowFilter = False
    '
    'ls_scdaquant_2
    '
    Me.ls_scdaquant_2.AppearanceCell.Options.UseBackColor = True
    Me.ls_scdaquant_2.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scdaquant_2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scdaquant_2.Caption = "Da qta II"
    Me.ls_scdaquant_2.Enabled = False
    Me.ls_scdaquant_2.FieldName = "ls_scdaquant_2"
    Me.ls_scdaquant_2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scdaquant_2.Name = "ls_scdaquant_2"
    Me.ls_scdaquant_2.NTSRepositoryComboBox = Nothing
    Me.ls_scdaquant_2.NTSRepositoryItemCheck = Nothing
    Me.ls_scdaquant_2.NTSRepositoryItemMemo = Nothing
    Me.ls_scdaquant_2.NTSRepositoryItemText = Nothing
    Me.ls_scdaquant_2.OptionsColumn.AllowEdit = False
    Me.ls_scdaquant_2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scdaquant_2.OptionsColumn.ReadOnly = True
    Me.ls_scdaquant_2.OptionsFilter.AllowFilter = False
    '
    'ls_scaquant_2
    '
    Me.ls_scaquant_2.AppearanceCell.Options.UseBackColor = True
    Me.ls_scaquant_2.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scaquant_2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scaquant_2.Caption = "A qta II"
    Me.ls_scaquant_2.Enabled = False
    Me.ls_scaquant_2.FieldName = "ls_scaquant_2"
    Me.ls_scaquant_2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scaquant_2.Name = "ls_scaquant_2"
    Me.ls_scaquant_2.NTSRepositoryComboBox = Nothing
    Me.ls_scaquant_2.NTSRepositoryItemCheck = Nothing
    Me.ls_scaquant_2.NTSRepositoryItemMemo = Nothing
    Me.ls_scaquant_2.NTSRepositoryItemText = Nothing
    Me.ls_scaquant_2.OptionsColumn.AllowEdit = False
    Me.ls_scaquant_2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scaquant_2.OptionsColumn.ReadOnly = True
    Me.ls_scaquant_2.OptionsFilter.AllowFilter = False
    '
    'ls_tiposc_2
    '
    Me.ls_tiposc_2.AppearanceCell.Options.UseBackColor = True
    Me.ls_tiposc_2.AppearanceCell.Options.UseTextOptions = True
    Me.ls_tiposc_2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_tiposc_2.Caption = "Tipo II"
    Me.ls_tiposc_2.Enabled = False
    Me.ls_tiposc_2.FieldName = "ls_tiposc_2"
    Me.ls_tiposc_2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_tiposc_2.Name = "ls_tiposc_2"
    Me.ls_tiposc_2.NTSRepositoryComboBox = Nothing
    Me.ls_tiposc_2.NTSRepositoryItemCheck = Nothing
    Me.ls_tiposc_2.NTSRepositoryItemMemo = Nothing
    Me.ls_tiposc_2.NTSRepositoryItemText = Nothing
    Me.ls_tiposc_2.OptionsColumn.AllowEdit = False
    Me.ls_tiposc_2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_tiposc_2.OptionsColumn.ReadOnly = True
    Me.ls_tiposc_2.OptionsFilter.AllowFilter = False
    Me.ls_tiposc_2.Visible = True
    Me.ls_tiposc_2.VisibleIndex = 80
    '
    'ls_dadatasc_2
    '
    Me.ls_dadatasc_2.AppearanceCell.Options.UseBackColor = True
    Me.ls_dadatasc_2.AppearanceCell.Options.UseTextOptions = True
    Me.ls_dadatasc_2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_dadatasc_2.Caption = "Da data II"
    Me.ls_dadatasc_2.Enabled = True
    Me.ls_dadatasc_2.FieldName = "ls_dadatasc_2"
    Me.ls_dadatasc_2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_dadatasc_2.Name = "ls_dadatasc_2"
    Me.ls_dadatasc_2.NTSRepositoryComboBox = Nothing
    Me.ls_dadatasc_2.NTSRepositoryItemCheck = Nothing
    Me.ls_dadatasc_2.NTSRepositoryItemMemo = Nothing
    Me.ls_dadatasc_2.NTSRepositoryItemText = Nothing
    Me.ls_dadatasc_2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_dadatasc_2.OptionsFilter.AllowFilter = False
    Me.ls_dadatasc_2.Visible = True
    Me.ls_dadatasc_2.VisibleIndex = 81
    '
    'ls_adatasc_2
    '
    Me.ls_adatasc_2.AppearanceCell.Options.UseBackColor = True
    Me.ls_adatasc_2.AppearanceCell.Options.UseTextOptions = True
    Me.ls_adatasc_2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_adatasc_2.Caption = "A data II"
    Me.ls_adatasc_2.Enabled = True
    Me.ls_adatasc_2.FieldName = "ls_adatasc_2"
    Me.ls_adatasc_2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_adatasc_2.Name = "ls_adatasc_2"
    Me.ls_adatasc_2.NTSRepositoryComboBox = Nothing
    Me.ls_adatasc_2.NTSRepositoryItemCheck = Nothing
    Me.ls_adatasc_2.NTSRepositoryItemMemo = Nothing
    Me.ls_adatasc_2.NTSRepositoryItemText = Nothing
    Me.ls_adatasc_2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_adatasc_2.OptionsFilter.AllowFilter = False
    Me.ls_adatasc_2.Visible = True
    Me.ls_adatasc_2.VisibleIndex = 82
    '
    'ls_codtpro_2
    '
    Me.ls_codtpro_2.AppearanceCell.Options.UseBackColor = True
    Me.ls_codtpro_2.AppearanceCell.Options.UseTextOptions = True
    Me.ls_codtpro_2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_codtpro_2.Caption = "Promozione II"
    Me.ls_codtpro_2.Enabled = False
    Me.ls_codtpro_2.FieldName = "ls_codtpro_2"
    Me.ls_codtpro_2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_codtpro_2.Name = "ls_codtpro_2"
    Me.ls_codtpro_2.NTSRepositoryComboBox = Nothing
    Me.ls_codtpro_2.NTSRepositoryItemCheck = Nothing
    Me.ls_codtpro_2.NTSRepositoryItemMemo = Nothing
    Me.ls_codtpro_2.NTSRepositoryItemText = Nothing
    Me.ls_codtpro_2.OptionsColumn.AllowEdit = False
    Me.ls_codtpro_2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_codtpro_2.OptionsColumn.ReadOnly = True
    Me.ls_codtpro_2.OptionsFilter.AllowFilter = False
    Me.ls_codtpro_2.Visible = True
    Me.ls_codtpro_2.VisibleIndex = 83
    '
    'xx_descpromo_2
    '
    Me.xx_descpromo_2.AppearanceCell.Options.UseBackColor = True
    Me.xx_descpromo_2.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descpromo_2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descpromo_2.Caption = "Descr. promozione II"
    Me.xx_descpromo_2.Enabled = False
    Me.xx_descpromo_2.FieldName = "xx_descpromo_2"
    Me.xx_descpromo_2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descpromo_2.Name = "xx_descpromo_2"
    Me.xx_descpromo_2.NTSRepositoryComboBox = Nothing
    Me.xx_descpromo_2.NTSRepositoryItemCheck = Nothing
    Me.xx_descpromo_2.NTSRepositoryItemMemo = Nothing
    Me.xx_descpromo_2.NTSRepositoryItemText = Nothing
    Me.xx_descpromo_2.OptionsColumn.AllowEdit = False
    Me.xx_descpromo_2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descpromo_2.OptionsColumn.ReadOnly = True
    Me.xx_descpromo_2.OptionsFilter.AllowFilter = False
    Me.xx_descpromo_2.Visible = True
    Me.xx_descpromo_2.VisibleIndex = 84
    '
    'ls_scont1_3
    '
    Me.ls_scont1_3.AppearanceCell.Options.UseBackColor = True
    Me.ls_scont1_3.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scont1_3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scont1_3.Caption = "Sconto 1 III"
    Me.ls_scont1_3.Enabled = True
    Me.ls_scont1_3.FieldName = "ls_scont1_3"
    Me.ls_scont1_3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scont1_3.Name = "ls_scont1_3"
    Me.ls_scont1_3.NTSRepositoryComboBox = Nothing
    Me.ls_scont1_3.NTSRepositoryItemCheck = Nothing
    Me.ls_scont1_3.NTSRepositoryItemMemo = Nothing
    Me.ls_scont1_3.NTSRepositoryItemText = Nothing
    Me.ls_scont1_3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scont1_3.OptionsFilter.AllowFilter = False
    Me.ls_scont1_3.Visible = True
    Me.ls_scont1_3.VisibleIndex = 85
    '
    'ls_scont2_3
    '
    Me.ls_scont2_3.AppearanceCell.Options.UseBackColor = True
    Me.ls_scont2_3.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scont2_3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scont2_3.Caption = "Sconto 2 III"
    Me.ls_scont2_3.Enabled = True
    Me.ls_scont2_3.FieldName = "ls_scont2_3"
    Me.ls_scont2_3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scont2_3.Name = "ls_scont2_3"
    Me.ls_scont2_3.NTSRepositoryComboBox = Nothing
    Me.ls_scont2_3.NTSRepositoryItemCheck = Nothing
    Me.ls_scont2_3.NTSRepositoryItemMemo = Nothing
    Me.ls_scont2_3.NTSRepositoryItemText = Nothing
    Me.ls_scont2_3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scont2_3.OptionsFilter.AllowFilter = False
    Me.ls_scont2_3.Visible = True
    Me.ls_scont2_3.VisibleIndex = 86
    '
    'ls_scont3_3
    '
    Me.ls_scont3_3.AppearanceCell.Options.UseBackColor = True
    Me.ls_scont3_3.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scont3_3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scont3_3.Caption = "Sconto 3 III"
    Me.ls_scont3_3.Enabled = True
    Me.ls_scont3_3.FieldName = "ls_scont3_3"
    Me.ls_scont3_3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scont3_3.Name = "ls_scont3_3"
    Me.ls_scont3_3.NTSRepositoryComboBox = Nothing
    Me.ls_scont3_3.NTSRepositoryItemCheck = Nothing
    Me.ls_scont3_3.NTSRepositoryItemMemo = Nothing
    Me.ls_scont3_3.NTSRepositoryItemText = Nothing
    Me.ls_scont3_3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scont3_3.OptionsFilter.AllowFilter = False
    Me.ls_scont3_3.Visible = True
    Me.ls_scont3_3.VisibleIndex = 87
    '
    'ls_scont4_3
    '
    Me.ls_scont4_3.AppearanceCell.Options.UseBackColor = True
    Me.ls_scont4_3.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scont4_3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scont4_3.Caption = "Sconto 4 III"
    Me.ls_scont4_3.Enabled = True
    Me.ls_scont4_3.FieldName = "ls_scont4_3"
    Me.ls_scont4_3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scont4_3.Name = "ls_scont4_3"
    Me.ls_scont4_3.NTSRepositoryComboBox = Nothing
    Me.ls_scont4_3.NTSRepositoryItemCheck = Nothing
    Me.ls_scont4_3.NTSRepositoryItemMemo = Nothing
    Me.ls_scont4_3.NTSRepositoryItemText = Nothing
    Me.ls_scont4_3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scont4_3.OptionsFilter.AllowFilter = False
    '
    'ls_scont5_3
    '
    Me.ls_scont5_3.AppearanceCell.Options.UseBackColor = True
    Me.ls_scont5_3.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scont5_3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scont5_3.Caption = "Sconto 5 III"
    Me.ls_scont5_3.Enabled = True
    Me.ls_scont5_3.FieldName = "ls_scont5_3"
    Me.ls_scont5_3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scont5_3.Name = "ls_scont5_3"
    Me.ls_scont5_3.NTSRepositoryComboBox = Nothing
    Me.ls_scont5_3.NTSRepositoryItemCheck = Nothing
    Me.ls_scont5_3.NTSRepositoryItemMemo = Nothing
    Me.ls_scont5_3.NTSRepositoryItemText = Nothing
    Me.ls_scont5_3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scont5_3.OptionsFilter.AllowFilter = False
    '
    'ls_scont6_3
    '
    Me.ls_scont6_3.AppearanceCell.Options.UseBackColor = True
    Me.ls_scont6_3.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scont6_3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scont6_3.Caption = "Sconto 6 III"
    Me.ls_scont6_3.Enabled = True
    Me.ls_scont6_3.FieldName = "ls_scont6_3"
    Me.ls_scont6_3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scont6_3.Name = "ls_scont6_3"
    Me.ls_scont6_3.NTSRepositoryComboBox = Nothing
    Me.ls_scont6_3.NTSRepositoryItemCheck = Nothing
    Me.ls_scont6_3.NTSRepositoryItemMemo = Nothing
    Me.ls_scont6_3.NTSRepositoryItemText = Nothing
    Me.ls_scont6_3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scont6_3.OptionsFilter.AllowFilter = False
    '
    'ls_scdaquant_3
    '
    Me.ls_scdaquant_3.AppearanceCell.Options.UseBackColor = True
    Me.ls_scdaquant_3.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scdaquant_3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scdaquant_3.Caption = "Da qta III"
    Me.ls_scdaquant_3.Enabled = False
    Me.ls_scdaquant_3.FieldName = "ls_scdaquant_3"
    Me.ls_scdaquant_3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scdaquant_3.Name = "ls_scdaquant_3"
    Me.ls_scdaquant_3.NTSRepositoryComboBox = Nothing
    Me.ls_scdaquant_3.NTSRepositoryItemCheck = Nothing
    Me.ls_scdaquant_3.NTSRepositoryItemMemo = Nothing
    Me.ls_scdaquant_3.NTSRepositoryItemText = Nothing
    Me.ls_scdaquant_3.OptionsColumn.AllowEdit = False
    Me.ls_scdaquant_3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scdaquant_3.OptionsColumn.ReadOnly = True
    Me.ls_scdaquant_3.OptionsFilter.AllowFilter = False
    '
    'ls_scaquant_3
    '
    Me.ls_scaquant_3.AppearanceCell.Options.UseBackColor = True
    Me.ls_scaquant_3.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scaquant_3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scaquant_3.Caption = "A qta III"
    Me.ls_scaquant_3.Enabled = False
    Me.ls_scaquant_3.FieldName = "ls_scaquant_3"
    Me.ls_scaquant_3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scaquant_3.Name = "ls_scaquant_3"
    Me.ls_scaquant_3.NTSRepositoryComboBox = Nothing
    Me.ls_scaquant_3.NTSRepositoryItemCheck = Nothing
    Me.ls_scaquant_3.NTSRepositoryItemMemo = Nothing
    Me.ls_scaquant_3.NTSRepositoryItemText = Nothing
    Me.ls_scaquant_3.OptionsColumn.AllowEdit = False
    Me.ls_scaquant_3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scaquant_3.OptionsColumn.ReadOnly = True
    Me.ls_scaquant_3.OptionsFilter.AllowFilter = False
    '
    'ls_tiposc_3
    '
    Me.ls_tiposc_3.AppearanceCell.Options.UseBackColor = True
    Me.ls_tiposc_3.AppearanceCell.Options.UseTextOptions = True
    Me.ls_tiposc_3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_tiposc_3.Caption = "Tipo III"
    Me.ls_tiposc_3.Enabled = False
    Me.ls_tiposc_3.FieldName = "ls_tiposc_3"
    Me.ls_tiposc_3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_tiposc_3.Name = "ls_tiposc_3"
    Me.ls_tiposc_3.NTSRepositoryComboBox = Nothing
    Me.ls_tiposc_3.NTSRepositoryItemCheck = Nothing
    Me.ls_tiposc_3.NTSRepositoryItemMemo = Nothing
    Me.ls_tiposc_3.NTSRepositoryItemText = Nothing
    Me.ls_tiposc_3.OptionsColumn.AllowEdit = False
    Me.ls_tiposc_3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_tiposc_3.OptionsColumn.ReadOnly = True
    Me.ls_tiposc_3.OptionsFilter.AllowFilter = False
    Me.ls_tiposc_3.Visible = True
    Me.ls_tiposc_3.VisibleIndex = 89
    '
    'ls_dadatasc_3
    '
    Me.ls_dadatasc_3.AppearanceCell.Options.UseBackColor = True
    Me.ls_dadatasc_3.AppearanceCell.Options.UseTextOptions = True
    Me.ls_dadatasc_3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_dadatasc_3.Caption = "Da data III"
    Me.ls_dadatasc_3.Enabled = True
    Me.ls_dadatasc_3.FieldName = "ls_dadatasc_3"
    Me.ls_dadatasc_3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_dadatasc_3.Name = "ls_dadatasc_3"
    Me.ls_dadatasc_3.NTSRepositoryComboBox = Nothing
    Me.ls_dadatasc_3.NTSRepositoryItemCheck = Nothing
    Me.ls_dadatasc_3.NTSRepositoryItemMemo = Nothing
    Me.ls_dadatasc_3.NTSRepositoryItemText = Nothing
    Me.ls_dadatasc_3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_dadatasc_3.OptionsFilter.AllowFilter = False
    Me.ls_dadatasc_3.Visible = True
    Me.ls_dadatasc_3.VisibleIndex = 88
    '
    'ls_adatasc_3
    '
    Me.ls_adatasc_3.AppearanceCell.Options.UseBackColor = True
    Me.ls_adatasc_3.AppearanceCell.Options.UseTextOptions = True
    Me.ls_adatasc_3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_adatasc_3.Caption = "A data III"
    Me.ls_adatasc_3.Enabled = True
    Me.ls_adatasc_3.FieldName = "ls_adatasc_3"
    Me.ls_adatasc_3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_adatasc_3.Name = "ls_adatasc_3"
    Me.ls_adatasc_3.NTSRepositoryComboBox = Nothing
    Me.ls_adatasc_3.NTSRepositoryItemCheck = Nothing
    Me.ls_adatasc_3.NTSRepositoryItemMemo = Nothing
    Me.ls_adatasc_3.NTSRepositoryItemText = Nothing
    Me.ls_adatasc_3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_adatasc_3.OptionsFilter.AllowFilter = False
    Me.ls_adatasc_3.Visible = True
    Me.ls_adatasc_3.VisibleIndex = 90
    '
    'ls_codtpro_3
    '
    Me.ls_codtpro_3.AppearanceCell.Options.UseBackColor = True
    Me.ls_codtpro_3.AppearanceCell.Options.UseTextOptions = True
    Me.ls_codtpro_3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_codtpro_3.Caption = "Promozione III"
    Me.ls_codtpro_3.Enabled = False
    Me.ls_codtpro_3.FieldName = "ls_codtpro_3"
    Me.ls_codtpro_3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_codtpro_3.Name = "ls_codtpro_3"
    Me.ls_codtpro_3.NTSRepositoryComboBox = Nothing
    Me.ls_codtpro_3.NTSRepositoryItemCheck = Nothing
    Me.ls_codtpro_3.NTSRepositoryItemMemo = Nothing
    Me.ls_codtpro_3.NTSRepositoryItemText = Nothing
    Me.ls_codtpro_3.OptionsColumn.AllowEdit = False
    Me.ls_codtpro_3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_codtpro_3.OptionsColumn.ReadOnly = True
    Me.ls_codtpro_3.OptionsFilter.AllowFilter = False
    Me.ls_codtpro_3.Visible = True
    Me.ls_codtpro_3.VisibleIndex = 91
    '
    'xx_descpromo_3
    '
    Me.xx_descpromo_3.AppearanceCell.Options.UseBackColor = True
    Me.xx_descpromo_3.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descpromo_3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descpromo_3.Caption = "Descr. promozione III"
    Me.xx_descpromo_3.Enabled = False
    Me.xx_descpromo_3.FieldName = "xx_descpromo_3"
    Me.xx_descpromo_3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descpromo_3.Name = "xx_descpromo_3"
    Me.xx_descpromo_3.NTSRepositoryComboBox = Nothing
    Me.xx_descpromo_3.NTSRepositoryItemCheck = Nothing
    Me.xx_descpromo_3.NTSRepositoryItemMemo = Nothing
    Me.xx_descpromo_3.NTSRepositoryItemText = Nothing
    Me.xx_descpromo_3.OptionsColumn.AllowEdit = False
    Me.xx_descpromo_3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descpromo_3.OptionsColumn.ReadOnly = True
    Me.xx_descpromo_3.OptionsFilter.AllowFilter = False
    Me.xx_descpromo_3.Visible = True
    Me.xx_descpromo_3.VisibleIndex = 92
    '
    'ls_scont1_4
    '
    Me.ls_scont1_4.AppearanceCell.Options.UseBackColor = True
    Me.ls_scont1_4.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scont1_4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scont1_4.Caption = "Sconto 1 IV"
    Me.ls_scont1_4.Enabled = True
    Me.ls_scont1_4.FieldName = "ls_scont1_4"
    Me.ls_scont1_4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scont1_4.Name = "ls_scont1_4"
    Me.ls_scont1_4.NTSRepositoryComboBox = Nothing
    Me.ls_scont1_4.NTSRepositoryItemCheck = Nothing
    Me.ls_scont1_4.NTSRepositoryItemMemo = Nothing
    Me.ls_scont1_4.NTSRepositoryItemText = Nothing
    Me.ls_scont1_4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scont1_4.OptionsFilter.AllowFilter = False
    Me.ls_scont1_4.Visible = True
    Me.ls_scont1_4.VisibleIndex = 93
    '
    'ls_scont2_4
    '
    Me.ls_scont2_4.AppearanceCell.Options.UseBackColor = True
    Me.ls_scont2_4.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scont2_4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scont2_4.Caption = "Sconto 2 IV"
    Me.ls_scont2_4.Enabled = True
    Me.ls_scont2_4.FieldName = "ls_scont2_4"
    Me.ls_scont2_4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scont2_4.Name = "ls_scont2_4"
    Me.ls_scont2_4.NTSRepositoryComboBox = Nothing
    Me.ls_scont2_4.NTSRepositoryItemCheck = Nothing
    Me.ls_scont2_4.NTSRepositoryItemMemo = Nothing
    Me.ls_scont2_4.NTSRepositoryItemText = Nothing
    Me.ls_scont2_4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scont2_4.OptionsFilter.AllowFilter = False
    Me.ls_scont2_4.Visible = True
    Me.ls_scont2_4.VisibleIndex = 94
    '
    'ls_scont3_4
    '
    Me.ls_scont3_4.AppearanceCell.Options.UseBackColor = True
    Me.ls_scont3_4.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scont3_4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scont3_4.Caption = "Sconto 3 IV"
    Me.ls_scont3_4.Enabled = True
    Me.ls_scont3_4.FieldName = "ls_scont3_4"
    Me.ls_scont3_4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scont3_4.Name = "ls_scont3_4"
    Me.ls_scont3_4.NTSRepositoryComboBox = Nothing
    Me.ls_scont3_4.NTSRepositoryItemCheck = Nothing
    Me.ls_scont3_4.NTSRepositoryItemMemo = Nothing
    Me.ls_scont3_4.NTSRepositoryItemText = Nothing
    Me.ls_scont3_4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scont3_4.OptionsFilter.AllowFilter = False
    Me.ls_scont3_4.Visible = True
    Me.ls_scont3_4.VisibleIndex = 95
    '
    'ls_scont4_4
    '
    Me.ls_scont4_4.AppearanceCell.Options.UseBackColor = True
    Me.ls_scont4_4.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scont4_4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scont4_4.Caption = "Sconto 4 IV"
    Me.ls_scont4_4.Enabled = True
    Me.ls_scont4_4.FieldName = "ls_scont4_4"
    Me.ls_scont4_4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scont4_4.Name = "ls_scont4_4"
    Me.ls_scont4_4.NTSRepositoryComboBox = Nothing
    Me.ls_scont4_4.NTSRepositoryItemCheck = Nothing
    Me.ls_scont4_4.NTSRepositoryItemMemo = Nothing
    Me.ls_scont4_4.NTSRepositoryItemText = Nothing
    Me.ls_scont4_4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scont4_4.OptionsFilter.AllowFilter = False
    '
    'ls_scont5_4
    '
    Me.ls_scont5_4.AppearanceCell.Options.UseBackColor = True
    Me.ls_scont5_4.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scont5_4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scont5_4.Caption = "Sconto 5 IV"
    Me.ls_scont5_4.Enabled = True
    Me.ls_scont5_4.FieldName = "ls_scont5_4"
    Me.ls_scont5_4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scont5_4.Name = "ls_scont5_4"
    Me.ls_scont5_4.NTSRepositoryComboBox = Nothing
    Me.ls_scont5_4.NTSRepositoryItemCheck = Nothing
    Me.ls_scont5_4.NTSRepositoryItemMemo = Nothing
    Me.ls_scont5_4.NTSRepositoryItemText = Nothing
    Me.ls_scont5_4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scont5_4.OptionsFilter.AllowFilter = False
    '
    'ls_scont6_4
    '
    Me.ls_scont6_4.AppearanceCell.Options.UseBackColor = True
    Me.ls_scont6_4.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scont6_4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scont6_4.Caption = "Sconto 6 IV"
    Me.ls_scont6_4.Enabled = True
    Me.ls_scont6_4.FieldName = "ls_scont6_4"
    Me.ls_scont6_4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scont6_4.Name = "ls_scont6_4"
    Me.ls_scont6_4.NTSRepositoryComboBox = Nothing
    Me.ls_scont6_4.NTSRepositoryItemCheck = Nothing
    Me.ls_scont6_4.NTSRepositoryItemMemo = Nothing
    Me.ls_scont6_4.NTSRepositoryItemText = Nothing
    Me.ls_scont6_4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scont6_4.OptionsFilter.AllowFilter = False
    '
    'ls_scdaquant_4
    '
    Me.ls_scdaquant_4.AppearanceCell.Options.UseBackColor = True
    Me.ls_scdaquant_4.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scdaquant_4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scdaquant_4.Caption = "Da qta IV"
    Me.ls_scdaquant_4.Enabled = False
    Me.ls_scdaquant_4.FieldName = "ls_scdaquant_4"
    Me.ls_scdaquant_4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scdaquant_4.Name = "ls_scdaquant_4"
    Me.ls_scdaquant_4.NTSRepositoryComboBox = Nothing
    Me.ls_scdaquant_4.NTSRepositoryItemCheck = Nothing
    Me.ls_scdaquant_4.NTSRepositoryItemMemo = Nothing
    Me.ls_scdaquant_4.NTSRepositoryItemText = Nothing
    Me.ls_scdaquant_4.OptionsColumn.AllowEdit = False
    Me.ls_scdaquant_4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scdaquant_4.OptionsColumn.ReadOnly = True
    Me.ls_scdaquant_4.OptionsFilter.AllowFilter = False
    '
    'ls_scaquant_4
    '
    Me.ls_scaquant_4.AppearanceCell.Options.UseBackColor = True
    Me.ls_scaquant_4.AppearanceCell.Options.UseTextOptions = True
    Me.ls_scaquant_4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_scaquant_4.Caption = "A qta IV"
    Me.ls_scaquant_4.Enabled = False
    Me.ls_scaquant_4.FieldName = "ls_scaquant_4"
    Me.ls_scaquant_4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_scaquant_4.Name = "ls_scaquant_4"
    Me.ls_scaquant_4.NTSRepositoryComboBox = Nothing
    Me.ls_scaquant_4.NTSRepositoryItemCheck = Nothing
    Me.ls_scaquant_4.NTSRepositoryItemMemo = Nothing
    Me.ls_scaquant_4.NTSRepositoryItemText = Nothing
    Me.ls_scaquant_4.OptionsColumn.AllowEdit = False
    Me.ls_scaquant_4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_scaquant_4.OptionsColumn.ReadOnly = True
    Me.ls_scaquant_4.OptionsFilter.AllowFilter = False
    '
    'ls_tiposc_4
    '
    Me.ls_tiposc_4.AppearanceCell.Options.UseBackColor = True
    Me.ls_tiposc_4.AppearanceCell.Options.UseTextOptions = True
    Me.ls_tiposc_4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_tiposc_4.Caption = "Tipo IV"
    Me.ls_tiposc_4.Enabled = False
    Me.ls_tiposc_4.FieldName = "ls_tiposc_4"
    Me.ls_tiposc_4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_tiposc_4.Name = "ls_tiposc_4"
    Me.ls_tiposc_4.NTSRepositoryComboBox = Nothing
    Me.ls_tiposc_4.NTSRepositoryItemCheck = Nothing
    Me.ls_tiposc_4.NTSRepositoryItemMemo = Nothing
    Me.ls_tiposc_4.NTSRepositoryItemText = Nothing
    Me.ls_tiposc_4.OptionsColumn.AllowEdit = False
    Me.ls_tiposc_4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_tiposc_4.OptionsColumn.ReadOnly = True
    Me.ls_tiposc_4.OptionsFilter.AllowFilter = False
    Me.ls_tiposc_4.Visible = True
    Me.ls_tiposc_4.VisibleIndex = 98
    '
    'ls_dadatasc_4
    '
    Me.ls_dadatasc_4.AppearanceCell.Options.UseBackColor = True
    Me.ls_dadatasc_4.AppearanceCell.Options.UseTextOptions = True
    Me.ls_dadatasc_4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_dadatasc_4.Caption = "Da data IV"
    Me.ls_dadatasc_4.Enabled = True
    Me.ls_dadatasc_4.FieldName = "ls_dadatasc_4"
    Me.ls_dadatasc_4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_dadatasc_4.Name = "ls_dadatasc_4"
    Me.ls_dadatasc_4.NTSRepositoryComboBox = Nothing
    Me.ls_dadatasc_4.NTSRepositoryItemCheck = Nothing
    Me.ls_dadatasc_4.NTSRepositoryItemMemo = Nothing
    Me.ls_dadatasc_4.NTSRepositoryItemText = Nothing
    Me.ls_dadatasc_4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_dadatasc_4.OptionsFilter.AllowFilter = False
    Me.ls_dadatasc_4.Visible = True
    Me.ls_dadatasc_4.VisibleIndex = 96
    '
    'ls_adatasc_4
    '
    Me.ls_adatasc_4.AppearanceCell.Options.UseBackColor = True
    Me.ls_adatasc_4.AppearanceCell.Options.UseTextOptions = True
    Me.ls_adatasc_4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_adatasc_4.Caption = "A data IV"
    Me.ls_adatasc_4.Enabled = True
    Me.ls_adatasc_4.FieldName = "ls_adatasc_4"
    Me.ls_adatasc_4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_adatasc_4.Name = "ls_adatasc_4"
    Me.ls_adatasc_4.NTSRepositoryComboBox = Nothing
    Me.ls_adatasc_4.NTSRepositoryItemCheck = Nothing
    Me.ls_adatasc_4.NTSRepositoryItemMemo = Nothing
    Me.ls_adatasc_4.NTSRepositoryItemText = Nothing
    Me.ls_adatasc_4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_adatasc_4.OptionsFilter.AllowFilter = False
    Me.ls_adatasc_4.Visible = True
    Me.ls_adatasc_4.VisibleIndex = 97
    '
    'ls_codtpro_4
    '
    Me.ls_codtpro_4.AppearanceCell.Options.UseBackColor = True
    Me.ls_codtpro_4.AppearanceCell.Options.UseTextOptions = True
    Me.ls_codtpro_4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_codtpro_4.Caption = "Promozione IV"
    Me.ls_codtpro_4.Enabled = False
    Me.ls_codtpro_4.FieldName = "ls_codtpro_4"
    Me.ls_codtpro_4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_codtpro_4.Name = "ls_codtpro_4"
    Me.ls_codtpro_4.NTSRepositoryComboBox = Nothing
    Me.ls_codtpro_4.NTSRepositoryItemCheck = Nothing
    Me.ls_codtpro_4.NTSRepositoryItemMemo = Nothing
    Me.ls_codtpro_4.NTSRepositoryItemText = Nothing
    Me.ls_codtpro_4.OptionsColumn.AllowEdit = False
    Me.ls_codtpro_4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_codtpro_4.OptionsColumn.ReadOnly = True
    Me.ls_codtpro_4.OptionsFilter.AllowFilter = False
    Me.ls_codtpro_4.Visible = True
    Me.ls_codtpro_4.VisibleIndex = 99
    '
    'xx_descpromo_4
    '
    Me.xx_descpromo_4.AppearanceCell.Options.UseBackColor = True
    Me.xx_descpromo_4.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descpromo_4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descpromo_4.Caption = "Descr. promozione IV"
    Me.xx_descpromo_4.Enabled = False
    Me.xx_descpromo_4.FieldName = "xx_descpromo_4"
    Me.xx_descpromo_4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descpromo_4.Name = "xx_descpromo_4"
    Me.xx_descpromo_4.NTSRepositoryComboBox = Nothing
    Me.xx_descpromo_4.NTSRepositoryItemCheck = Nothing
    Me.xx_descpromo_4.NTSRepositoryItemMemo = Nothing
    Me.xx_descpromo_4.NTSRepositoryItemText = Nothing
    Me.xx_descpromo_4.OptionsColumn.AllowEdit = False
    Me.xx_descpromo_4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descpromo_4.OptionsColumn.ReadOnly = True
    Me.xx_descpromo_4.OptionsFilter.AllowFilter = False
    Me.xx_descpromo_4.Visible = True
    Me.xx_descpromo_4.VisibleIndex = 100
    '
    'ls_clscan
    '
    Me.ls_clscan.AppearanceCell.Options.UseBackColor = True
    Me.ls_clscan.AppearanceCell.Options.UseTextOptions = True
    Me.ls_clscan.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_clscan.Caption = "Classe sconto cli\for"
    Me.ls_clscan.Enabled = False
    Me.ls_clscan.FieldName = "ls_clscan"
    Me.ls_clscan.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_clscan.Name = "ls_clscan"
    Me.ls_clscan.NTSRepositoryComboBox = Nothing
    Me.ls_clscan.NTSRepositoryItemCheck = Nothing
    Me.ls_clscan.NTSRepositoryItemMemo = Nothing
    Me.ls_clscan.NTSRepositoryItemText = Nothing
    Me.ls_clscan.OptionsColumn.AllowEdit = False
    Me.ls_clscan.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_clscan.OptionsColumn.ReadOnly = True
    Me.ls_clscan.OptionsFilter.AllowFilter = False
    Me.ls_clscan.Visible = True
    Me.ls_clscan.VisibleIndex = 101
    '
    'xx_clscan
    '
    Me.xx_clscan.AppearanceCell.Options.UseBackColor = True
    Me.xx_clscan.AppearanceCell.Options.UseTextOptions = True
    Me.xx_clscan.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_clscan.Caption = "Desc. classe cli\for"
    Me.xx_clscan.Enabled = False
    Me.xx_clscan.FieldName = "xx_clscan"
    Me.xx_clscan.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_clscan.Name = "xx_clscan"
    Me.xx_clscan.NTSRepositoryComboBox = Nothing
    Me.xx_clscan.NTSRepositoryItemCheck = Nothing
    Me.xx_clscan.NTSRepositoryItemMemo = Nothing
    Me.xx_clscan.NTSRepositoryItemText = Nothing
    Me.xx_clscan.OptionsColumn.AllowEdit = False
    Me.xx_clscan.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_clscan.OptionsColumn.ReadOnly = True
    Me.xx_clscan.OptionsFilter.AllowFilter = False
    Me.xx_clscan.Visible = True
    Me.xx_clscan.VisibleIndex = 102
    '
    'ls_clscar
    '
    Me.ls_clscar.AppearanceCell.Options.UseBackColor = True
    Me.ls_clscar.AppearanceCell.Options.UseTextOptions = True
    Me.ls_clscar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_clscar.Caption = "Classe sconto art."
    Me.ls_clscar.Enabled = False
    Me.ls_clscar.FieldName = "ls_clscar_4"
    Me.ls_clscar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_clscar.Name = "ls_clscar"
    Me.ls_clscar.NTSRepositoryComboBox = Nothing
    Me.ls_clscar.NTSRepositoryItemCheck = Nothing
    Me.ls_clscar.NTSRepositoryItemMemo = Nothing
    Me.ls_clscar.NTSRepositoryItemText = Nothing
    Me.ls_clscar.OptionsColumn.AllowEdit = False
    Me.ls_clscar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_clscar.OptionsColumn.ReadOnly = True
    Me.ls_clscar.OptionsFilter.AllowFilter = False
    Me.ls_clscar.Visible = True
    Me.ls_clscar.VisibleIndex = 103
    '
    'xx_clscar
    '
    Me.xx_clscar.AppearanceCell.Options.UseBackColor = True
    Me.xx_clscar.AppearanceCell.Options.UseTextOptions = True
    Me.xx_clscar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_clscar.Caption = "Desc. classe articolo"
    Me.xx_clscar.Enabled = False
    Me.xx_clscar.FieldName = "xx_clscar"
    Me.xx_clscar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_clscar.Name = "xx_clscar"
    Me.xx_clscar.NTSRepositoryComboBox = Nothing
    Me.xx_clscar.NTSRepositoryItemCheck = Nothing
    Me.xx_clscar.NTSRepositoryItemMemo = Nothing
    Me.xx_clscar.NTSRepositoryItemText = Nothing
    Me.xx_clscar.OptionsColumn.AllowEdit = False
    Me.xx_clscar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_clscar.OptionsColumn.ReadOnly = True
    Me.xx_clscar.OptionsFilter.AllowFilter = False
    Me.xx_clscar.Visible = True
    Me.xx_clscar.VisibleIndex = 104
    '
    'ar_codart
    '
    Me.ar_codart.AppearanceCell.Options.UseBackColor = True
    Me.ar_codart.AppearanceCell.Options.UseTextOptions = True
    Me.ar_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_codart.Caption = "Cod. articolo"
    Me.ar_codart.Enabled = False
    Me.ar_codart.FieldName = "ar_codart"
    Me.ar_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_codart.Name = "ar_codart"
    Me.ar_codart.NTSRepositoryComboBox = Nothing
    Me.ar_codart.NTSRepositoryItemCheck = Nothing
    Me.ar_codart.NTSRepositoryItemMemo = Nothing
    Me.ar_codart.NTSRepositoryItemText = Nothing
    Me.ar_codart.OptionsColumn.AllowEdit = False
    Me.ar_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_codart.OptionsColumn.ReadOnly = True
    Me.ar_codart.OptionsFilter.AllowFilter = False
    '
    'xx_desint
    '
    Me.xx_desint.AppearanceCell.Options.UseBackColor = True
    Me.xx_desint.AppearanceCell.Options.UseTextOptions = True
    Me.xx_desint.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_desint.Caption = "Descr. Interna"
    Me.xx_desint.Enabled = False
    Me.xx_desint.FieldName = "xx_desint"
    Me.xx_desint.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_desint.Name = "xx_desint"
    Me.xx_desint.NTSRepositoryComboBox = Nothing
    Me.xx_desint.NTSRepositoryItemCheck = Nothing
    Me.xx_desint.NTSRepositoryItemMemo = Nothing
    Me.xx_desint.NTSRepositoryItemText = Nothing
    Me.xx_desint.OptionsColumn.AllowEdit = False
    Me.xx_desint.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_desint.OptionsColumn.ReadOnly = True
    Me.xx_desint.OptionsFilter.AllowFilter = False
    '
    'ls_coddest
    '
    Me.ls_coddest.AppearanceCell.Options.UseBackColor = True
    Me.ls_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.ls_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ls_coddest.Caption = "Destinaz. diversa"
    Me.ls_coddest.CustomizationCaption = "ls_coddest"
    Me.ls_coddest.Enabled = False
    Me.ls_coddest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ls_coddest.Name = "ls_coddest"
    Me.ls_coddest.NTSRepositoryComboBox = Nothing
    Me.ls_coddest.NTSRepositoryItemCheck = Nothing
    Me.ls_coddest.NTSRepositoryItemMemo = Nothing
    Me.ls_coddest.NTSRepositoryItemText = Nothing
    Me.ls_coddest.OptionsColumn.AllowEdit = False
    Me.ls_coddest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ls_coddest.OptionsColumn.ReadOnly = True
    Me.ls_coddest.OptionsFilter.AllowFilter = False
    Me.ls_coddest.Visible = True
    Me.ls_coddest.VisibleIndex = 105
    '
    'xx_coddest
    '
    Me.xx_coddest.AppearanceCell.Options.UseBackColor = True
    Me.xx_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.xx_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_coddest.Caption = "Descr. destinaz. diversa"
    Me.xx_coddest.Enabled = False
    Me.xx_coddest.FieldName = "xx_coddest"
    Me.xx_coddest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_coddest.Name = "xx_coddest"
    Me.xx_coddest.NTSRepositoryComboBox = Nothing
    Me.xx_coddest.NTSRepositoryItemCheck = Nothing
    Me.xx_coddest.NTSRepositoryItemMemo = Nothing
    Me.xx_coddest.NTSRepositoryItemText = Nothing
    Me.xx_coddest.OptionsColumn.AllowEdit = False
    Me.xx_coddest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_coddest.OptionsColumn.ReadOnly = True
    Me.xx_coddest.OptionsFilter.AllowFilter = False
    Me.xx_coddest.Visible = True
    Me.xx_coddest.VisibleIndex = 106
    '
    'pnDatiRiga
    '
    Me.pnDatiRiga.AllowDrop = True
    Me.pnDatiRiga.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnDatiRiga.Appearance.Options.UseBackColor = True
    Me.pnDatiRiga.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnDatiRiga.Controls.Add(Me.fmDatiRiga)
    Me.pnDatiRiga.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnDatiRiga.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnDatiRiga.Location = New System.Drawing.Point(0, 30)
    Me.pnDatiRiga.Name = "pnDatiRiga"
    Me.pnDatiRiga.NTSActiveTrasparency = True
    Me.pnDatiRiga.Size = New System.Drawing.Size(794, 76)
    Me.pnDatiRiga.TabIndex = 5
    Me.pnDatiRiga.Text = "NtsPanel4"
    '
    'fmDatiRiga
    '
    Me.fmDatiRiga.AllowDrop = True
    Me.fmDatiRiga.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.fmDatiRiga.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmDatiRiga.Appearance.Options.UseBackColor = True
    Me.fmDatiRiga.Controls.Add(Me.lbRossoDesc)
    Me.fmDatiRiga.Controls.Add(Me.lbRosso)
    Me.fmDatiRiga.Controls.Add(Me.lbBDesc)
    Me.fmDatiRiga.Controls.Add(Me.lbB)
    Me.fmDatiRiga.Controls.Add(Me.lbFase)
    Me.fmDatiRiga.Controls.Add(Me.lbArtico)
    Me.fmDatiRiga.Controls.Add(Me.lbConto)
    Me.fmDatiRiga.Controls.Add(Me.edArtico)
    Me.fmDatiRiga.Controls.Add(Me.edFase)
    Me.fmDatiRiga.Controls.Add(Me.lbDesFase)
    Me.fmDatiRiga.Controls.Add(Me.lbDesArtico)
    Me.fmDatiRiga.Controls.Add(Me.edConto)
    Me.fmDatiRiga.Controls.Add(Me.lbDesConto)
    Me.fmDatiRiga.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmDatiRiga.Location = New System.Drawing.Point(12, 2)
    Me.fmDatiRiga.Name = "fmDatiRiga"
    Me.fmDatiRiga.Size = New System.Drawing.Size(770, 71)
    Me.fmDatiRiga.TabIndex = 1
    Me.fmDatiRiga.Text = "Riga selezionata"
    '
    'lbRossoDesc
    '
    Me.lbRossoDesc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbRossoDesc.AutoSize = True
    Me.lbRossoDesc.BackColor = System.Drawing.Color.Transparent
    Me.lbRossoDesc.Location = New System.Drawing.Point(630, 26)
    Me.lbRossoDesc.Name = "lbRossoDesc"
    Me.lbRossoDesc.NTSDbField = ""
    Me.lbRossoDesc.Size = New System.Drawing.Size(140, 13)
    Me.lbRossoDesc.TabIndex = 13
    Me.lbRossoDesc.Text = "= diverso dal valore attuale"
    Me.lbRossoDesc.Tooltip = ""
    Me.lbRossoDesc.UseMnemonic = False
    '
    'lbRosso
    '
    Me.lbRosso.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbRosso.AutoSize = True
    Me.lbRosso.BackColor = System.Drawing.Color.Transparent
    Me.lbRosso.Font = New System.Drawing.Font("Tahoma", 8.25!)
    Me.lbRosso.ForeColor = System.Drawing.Color.IndianRed
    Me.lbRosso.Location = New System.Drawing.Point(598, 26)
    Me.lbRosso.Name = "lbRosso"
    Me.lbRosso.NTSDbField = ""
    Me.lbRosso.Size = New System.Drawing.Size(36, 13)
    Me.lbRosso.TabIndex = 12
    Me.lbRosso.Text = "Rosso"
    Me.lbRosso.Tooltip = ""
    Me.lbRosso.UseMnemonic = False
    '
    'lbBDesc
    '
    Me.lbBDesc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbBDesc.AutoSize = True
    Me.lbBDesc.BackColor = System.Drawing.Color.Transparent
    Me.lbBDesc.Location = New System.Drawing.Point(525, 26)
    Me.lbBDesc.Name = "lbBDesc"
    Me.lbBDesc.NTSDbField = ""
    Me.lbBDesc.Size = New System.Drawing.Size(67, 13)
    Me.lbBDesc.TabIndex = 11
    Me.lbBDesc.Text = "= modificato"
    Me.lbBDesc.Tooltip = ""
    Me.lbBDesc.UseMnemonic = False
    '
    'lbB
    '
    Me.lbB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbB.AutoSize = True
    Me.lbB.BackColor = System.Drawing.Color.Transparent
    Me.lbB.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbB.Location = New System.Drawing.Point(465, 26)
    Me.lbB.Name = "lbB"
    Me.lbB.NTSDbField = ""
    Me.lbB.Size = New System.Drawing.Size(63, 13)
    Me.lbB.TabIndex = 10
    Me.lbB.Text = "Grassetto"
    Me.lbB.Tooltip = ""
    Me.lbB.UseMnemonic = False
    '
    'lbFase
    '
    Me.lbFase.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbFase.AutoSize = True
    Me.lbFase.BackColor = System.Drawing.Color.Transparent
    Me.lbFase.Location = New System.Drawing.Point(465, 50)
    Me.lbFase.Name = "lbFase"
    Me.lbFase.NTSDbField = ""
    Me.lbFase.Size = New System.Drawing.Size(30, 13)
    Me.lbFase.TabIndex = 9
    Me.lbFase.Text = "Fase"
    Me.lbFase.Tooltip = ""
    Me.lbFase.UseMnemonic = False
    '
    'lbArtico
    '
    Me.lbArtico.AutoSize = True
    Me.lbArtico.BackColor = System.Drawing.Color.Transparent
    Me.lbArtico.Location = New System.Drawing.Point(5, 50)
    Me.lbArtico.Name = "lbArtico"
    Me.lbArtico.NTSDbField = ""
    Me.lbArtico.Size = New System.Drawing.Size(43, 13)
    Me.lbArtico.TabIndex = 8
    Me.lbArtico.Text = "Articolo"
    Me.lbArtico.Tooltip = ""
    Me.lbArtico.UseMnemonic = False
    '
    'lbConto
    '
    Me.lbConto.AutoSize = True
    Me.lbConto.BackColor = System.Drawing.Color.Transparent
    Me.lbConto.Location = New System.Drawing.Point(5, 26)
    Me.lbConto.Name = "lbConto"
    Me.lbConto.NTSDbField = ""
    Me.lbConto.Size = New System.Drawing.Size(88, 13)
    Me.lbConto.TabIndex = 7
    Me.lbConto.Text = "Cliente\Fornitore"
    Me.lbConto.Tooltip = ""
    Me.lbConto.UseMnemonic = False
    '
    'edArtico
    '
    Me.edArtico.Cursor = System.Windows.Forms.Cursors.Default
    Me.edArtico.EditValue = ""
    Me.edArtico.Enabled = False
    Me.edArtico.Location = New System.Drawing.Point(99, 46)
    Me.edArtico.Name = "edArtico"
    Me.edArtico.NTSDbField = ""
    Me.edArtico.NTSForzaVisZoom = False
    Me.edArtico.NTSOldValue = ""
    Me.edArtico.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edArtico.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edArtico.Properties.AutoHeight = False
    Me.edArtico.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edArtico.Properties.MaxLength = 65536
    Me.edArtico.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edArtico.Size = New System.Drawing.Size(85, 20)
    Me.edArtico.TabIndex = 6
    '
    'edFase
    '
    Me.edFase.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edFase.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edFase.EditValue = "0"
    Me.edFase.Enabled = False
    Me.edFase.Location = New System.Drawing.Point(501, 46)
    Me.edFase.Name = "edFase"
    Me.edFase.NTSDbField = ""
    Me.edFase.NTSFormat = "0"
    Me.edFase.NTSForzaVisZoom = False
    Me.edFase.NTSOldValue = ""
    Me.edFase.Properties.Appearance.Options.UseTextOptions = True
    Me.edFase.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edFase.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edFase.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edFase.Properties.AutoHeight = False
    Me.edFase.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFase.Properties.MaxLength = 65536
    Me.edFase.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFase.Size = New System.Drawing.Size(43, 20)
    Me.edFase.TabIndex = 2
    '
    'lbDesFase
    '
    Me.lbDesFase.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbDesFase.BackColor = System.Drawing.Color.Transparent
    Me.lbDesFase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesFase.Location = New System.Drawing.Point(550, 46)
    Me.lbDesFase.Name = "lbDesFase"
    Me.lbDesFase.NTSDbField = ""
    Me.lbDesFase.Size = New System.Drawing.Size(212, 20)
    Me.lbDesFase.TabIndex = 5
    Me.lbDesFase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDesFase.Tooltip = ""
    Me.lbDesFase.UseMnemonic = False
    '
    'lbDesArtico
    '
    Me.lbDesArtico.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbDesArtico.BackColor = System.Drawing.Color.Transparent
    Me.lbDesArtico.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesArtico.Location = New System.Drawing.Point(190, 46)
    Me.lbDesArtico.Name = "lbDesArtico"
    Me.lbDesArtico.NTSDbField = ""
    Me.lbDesArtico.Size = New System.Drawing.Size(269, 20)
    Me.lbDesArtico.TabIndex = 4
    Me.lbDesArtico.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDesArtico.Tooltip = ""
    Me.lbDesArtico.UseMnemonic = False
    '
    'edConto
    '
    Me.edConto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edConto.EditValue = "0"
    Me.edConto.Enabled = False
    Me.edConto.Location = New System.Drawing.Point(99, 23)
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
    Me.edConto.Size = New System.Drawing.Size(85, 20)
    Me.edConto.TabIndex = 0
    '
    'lbDesConto
    '
    Me.lbDesConto.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbDesConto.BackColor = System.Drawing.Color.Transparent
    Me.lbDesConto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesConto.Location = New System.Drawing.Point(190, 23)
    Me.lbDesConto.Name = "lbDesConto"
    Me.lbDesConto.NTSDbField = ""
    Me.lbDesConto.Size = New System.Drawing.Size(269, 20)
    Me.lbDesConto.TabIndex = 3
    Me.lbDesConto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbDesConto.Tooltip = ""
    Me.lbDesConto.UseMnemonic = False
    '
    'tmTick
    '
    Me.tmTick.Interval = 300000
    '
    'tlbVarPrzSc
    '
    Me.tlbVarPrzSc.Caption = "Variazione massiva prezzi/sconti"
    Me.tlbVarPrzSc.GlyphPath = ""
    Me.tlbVarPrzSc.Id = 11
    Me.tlbVarPrzSc.Name = "tlbVarPrzSc"
    Me.tlbVarPrzSc.NTSIsCheckBox = False
    Me.tlbVarPrzSc.Visible = True
    '
    'FRMMGLISE
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(794, 519)
    Me.Controls.Add(Me.pnGriglia)
    Me.Controls.Add(Me.pnDatiRiga)
    Me.Controls.Add(Me.pnFiltroColonna)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMMGLISE"
    Me.Text = "STAMPA LISTINI SU GRIGLIA"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnFiltroColonna, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFiltroColonna.ResumeLayout(False)
    Me.pnFiltroColonna.PerformLayout()
    CType(Me.ckColoreNero.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmFiltriColonna, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmFiltriColonna.ResumeLayout(False)
    Me.fmFiltriColonna.PerformLayout()
    CType(Me.ckSconti4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSconti3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSconti2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSconti1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckListino5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckListino4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckListino3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckListino2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckListino1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckBlocca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnGriglia, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGriglia.ResumeLayout(False)
    CType(Me.grList, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvList, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnDatiRiga, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDatiRiga.ResumeLayout(False)
    CType(Me.fmDatiRiga, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmDatiRiga.ResumeLayout(False)
    Me.fmDatiRiga.PerformLayout()
    CType(Me.edArtico.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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

    Return True
  End Function
  Public Overridable Sub InitEntity(ByRef cleStli As CLEMGSTLI)
    oCleStli = cleStli
    AddHandler oCleStli.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttTipo, dttTipoSc As New DataTable
    Dim i As Integer = 0
    Try
      tlbElabora.GlyphPath = (oApp.ChildImageDir & "\elabora.gif")
      tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
      tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
      tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
      tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
      tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")


      tlbMain.NTSSetToolTip()

      dttTipo.Columns.Add("cod", GetType(String))
      dttTipo.Columns.Add("val", GetType(String))
      dttTipo.Rows.Add(New Object() {" ", "Generico art."})
      dttTipo.Rows.Add(New Object() {"C", "Specifico clie."})
      dttTipo.Rows.Add(New Object() {"F", "Specifico forn."})
      'dttTipo.Rows.Add(New Object() {"K", "Casella gen."})
      'dttTipo.Rows.Add(New Object() {"J", "Casella per cliente"})
      'dttTipo.Rows.Add(New Object() {"T", "Task"})
      'dttTipo.Rows.Add(New Object() {"U", "Task per cliente"})
      'dttTipo.Rows.Add(New Object() {"V", "Voce list. generico"})
      'dttTipo.Rows.Add(New Object() {"Z", "Voce list. cliente"})
      dttTipo.AcceptChanges()

      dttTipoSc.Columns.Add("cod", GetType(String))
      dttTipoSc.Columns.Add("val", GetType(String))
      dttTipoSc.Rows.Add(New Object() {"A", "Speciale cli./art."})
      dttTipoSc.Rows.Add(New Object() {"B", "Art./classe cliente"})
      dttTipoSc.Rows.Add(New Object() {"C", "Cliente/classe art."})
      dttTipoSc.Rows.Add(New Object() {"D", "Classe Cl./classe art."})
      dttTipoSc.Rows.Add(New Object() {"E", "Generico articolo"})
      dttTipoSc.Rows.Add(New Object() {"F", "Generico Cliente"})
      dttTipoSc.AcceptChanges()

      grvList.NTSSetParam(oMenu, oApp.Tr(Me, 129212564420625000, "Listini"))
      ls_conto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564420937500, "Conto"), "0", 9, 0, 999999999)
      ls_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212564421093750, "Articolo"), 0, True)
      ls_fase.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564421406250, "Fase"), "0", 4, 0, 9999)
      ls_listino1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564421718750, "Listino 1"), "0", 4, 0, 9999)
      ls_prz1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564421875000, "Prezzo 1"), oApp.FormatPrzUn, 15)
      ls_daquant1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564422031250, "Da qta 1"), oApp.FormatQta, 15)
      ls_aquant1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564422187500, "A qta 1"), oApp.FormatQta, 15)
      ls_perqta.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564422343750, "Per qta 1"), oApp.FormatQta, 15)
      'ls_unmis1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212564422500000, "Un. mis. 1"), 3, True)
      ls_codlavo1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564422656250, "Lavorazione 1"), "0", 4, 0, 9999)
      ls_tipo1.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129212564422968750, "Tipo 1"), dttTipo, "val", "cod")
      ls_dadata1.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129212564423125000, "Da data 1"), True)
      ls_adata1.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129212564423281250, "A data 1"), True)
      ls_codvalu1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564423437500, "Valuta 1"), "0", 4, 0, 9999)
      ls_prznet1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212564423750000, "Prezzo netto 1"), 1) ' Non uso CHK perchè non fa apparire o il check o i colori di sfondo
      ls_listino2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564423906250, "Listino 2"), "0", 4, 0, 9999)
      ls_prz2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564424062500, "Prezzo 2"), oApp.FormatPrzUn, 15)
      ls_daquant2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564424218750, "Da qta 2"), oApp.FormatQta, 15)
      ls_aquant2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564424375000, "A qta 2"), oApp.FormatQta, 15)
      'ls_unmis2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212564424687500, "Un. mis. 2"), 3, True)
      ls_codlavo2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564424843750, "Lavorazione 2"), "0", 4, 0, 9999)
      ls_tipo2.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129212564425156250, "Tipo 2"), dttTipo, "val", "cod")
      ls_dadata2.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129212564425312500, "Da data 2"), True)
      ls_adata2.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129212564425468750, "A data 2"), True)
      ls_codvalu2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564425625000, "Valuta 2"), "0", 4, 0, 9999)
      ls_prznet2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212564425937500, "Prezzo netto 2"), 1) ' Non uso CHK perchè non fa apparire o il check o i colori di sfondo
      ls_listino3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564426093750, "Listino 3"), "0", 4, 0, 9999)
      ls_prz3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564426250000, "Prezzo 3"), oApp.FormatPrzUn, 15)
      ls_daquant3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564426406250, "Da qta 3"), oApp.FormatQta, 15)
      ls_aquant3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564426562500, "A qta 3"), oApp.FormatQta, 15)
      'ls_unmis3.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212564426875000, "Un. mis. 3"), 3, True)
      ls_codlavo3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564427031250, "Lavorazione 3"), "0", 4, 0, 9999)
      ls_tipo3.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129212564427343750, "Tipo 3"), dttTipo, "val", "cod")
      ls_dadata3.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129212564427500000, "Da data 3"), True)
      ls_adata3.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129212564427656250, "A data 3"), True)
      ls_codvalu3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564427812500, "Valuta 3"), "0", 4, 0, 9999)
      ls_prznet3.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212564428125000, "Prezzo netto 3"), 1) ' Non uso CHK perchè non fa apparire o il check o i colori di sfondo
      ls_listino4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564428281250, "Listino 4"), "0", 4, 0, 9999)
      ls_prz4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564428437500, "Prezzo 4"), oApp.FormatPrzUn, 15)
      ls_daquant4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564428593750, "Da qta 4"), oApp.FormatQta, 15)
      ls_aquant4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564428750000, "A qta 4"), oApp.FormatQta, 15)
      'ls_unmis4.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212564429062500, "Un. mis. 4"), 3, True)
      ls_codlavo4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564429218750, "Lavorazione 4"), "0", 4, 0, 9999)
      ls_tipo4.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129212564429531250, "Tipo 4"), dttTipo, "val", "cod")
      ls_dadata4.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129212564429687500, "Da data 4"), True)
      ls_adata4.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129212564429843750, "A data 4"), True)
      ls_codvalu4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564430000000, "Valuta 4"), "0", 4, 0, 9999)
      ls_prznet4.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212564430312500, "Prezzo netto 4"), 1) ' Non uso CHK perchè non fa apparire o il check o i colori di sfondo
      ls_listino5.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564430468750, "Listino 5"), "0", 4, 0, 9999)
      ls_prz5.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564430625000, "Prezzo 5"), oApp.FormatPrzUn, 15)
      ls_daquant5.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564430781250, "Da qta 5"), oApp.FormatQta, 15)
      ls_aquant5.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564430937500, "A qta 5"), oApp.FormatQta, 15)
      'ls_unmis5.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212564431250000, "Un. mis. 5"), 3, True)
      ls_codlavo5.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564431406250, "Lavorazione 5"), "0", 4, 0, 9999)
      ls_tipo5.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129212564431718750, "Tipo 5"), dttTipo, "val", "cod")
      ls_dadata5.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129212564431875000, "Da data 5"), True)
      ls_adata5.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129212564432031250, "A data 5"), True)
      ls_codvalu5.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564432187500, "Valuta 5"), "0", 4, 0, 9999)
      ls_prznet5.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212564432500000, "Prezzo netto 5"), 1) ' Non uso CHK perchè non fa apparire o il check o i colori di sfondo
      ls_scont1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564432656250, "Sconto 1 I"), oApp.FormatSconti, 15, -100, 100)
      ls_scont2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564432812500, "Sconto 2 I"), oApp.FormatSconti, 15, -100, 100)
      ls_scont3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564432968750, "Sconto 3 I"), oApp.FormatSconti, 15, -100, 100)
      ls_scont4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564433125000, "Sconto 4 I"), oApp.FormatSconti, 15, -100, 100)
      ls_scont5.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564433281250, "Sconto 5 I"), oApp.FormatSconti, 15, -100, 100)
      ls_scont6.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564433437500, "Sconto 6 I"), oApp.FormatSconti, 15, -100, 100)
      ls_scdaquant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564433593750, "Da qta I"), oApp.FormatSconti, 15)
      ls_scaquant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564433750000, "A qta I"), oApp.FormatSconti, 15)
      ls_dadatasc.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129212564434062500, "Da data I"), True)
      ls_adatasc.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129212564434218750, "A data I"), True)
      ls_codtpro.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564434375000, "Promozione I"), "0", 4, 0, 9999)
      ls_clscan.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564434687500, "Classe sconto cli\for I"), "0", 4, 0, 9999)
      ls_clscar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564434843750, "Classe sconto art. I"), "0", 4, 0, 9999)
      ls_scont1_2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564435000000, "Sconto 1 II"), oApp.FormatSconti, 15, -100, 100)
      ls_scont2_2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564435156250, "Sconto 2 II"), oApp.FormatSconti, 15, -100, 100)
      ls_scont3_2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564435312500, "Sconto 3 II"), oApp.FormatSconti, 15, -100, 100)
      ls_scont4_2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564435468750, "Sconto 4 II"), oApp.FormatSconti, 15, -100, 100)
      ls_scont5_2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564435625000, "Sconto 5 II"), oApp.FormatSconti, 15, -100, 100)
      ls_scont6_2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564435781250, "Sconto 6 II"), oApp.FormatSconti, 15, -100, 100)
      ls_scdaquant_2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564435937500, "Da qta II"), oApp.FormatSconti, 15)
      ls_scaquant_2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564436093750, "A qta II"), oApp.FormatSconti, 15)
      ls_dadatasc_2.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129212564436406250, "Da data II"), True)
      ls_adatasc_2.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129212564436562500, "A data II"), True)
      ls_codtpro_2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564436718750, "Promozione II"), "0", 4, 0, 9999)
      ls_scont1_3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564437343750, "Sconto 1 III"), oApp.FormatSconti, 15, -100, 100)
      ls_scont2_3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564437500000, "Sconto 2 III"), oApp.FormatSconti, 15, -100, 100)
      ls_scont3_3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564437656250, "Sconto 3 III"), oApp.FormatSconti, 15, -100, 100)
      ls_scont4_3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564437812500, "Sconto 4 III"), oApp.FormatSconti, 15, -100, 100)
      ls_scont5_3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564437968750, "Sconto 5 III"), oApp.FormatSconti, 15, -100, 100)
      ls_scont6_3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564438125000, "Sconto 6 III"), oApp.FormatSconti, 15, -100, 100)
      ls_scdaquant_3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564438281250, "Da qta III"), oApp.FormatSconti, 15)
      ls_scaquant_3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564438437500, "A qta III"), oApp.FormatSconti, 15)
      ls_dadatasc_3.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129212564438750000, "Da data III"), True)
      ls_adatasc_3.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129212564438906250, "A data III"), True)
      ls_codtpro_3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564439062500, "Promozione III"), "0", 4, 0, 9999)
      ls_scont1_4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564439687500, "Sconto 1 IV"), oApp.FormatSconti, 15, -100, 100)
      ls_scont2_4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564439843750, "Sconto 2 IV"), oApp.FormatSconti, 15, -100, 100)
      ls_scont3_4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564440000000, "Sconto 3 IV"), oApp.FormatSconti, 15, -100, 100)
      ls_scont4_4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564440156250, "Sconto 4 IV"), oApp.FormatSconti, 15, -100, 100)
      ls_scont5_4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564440312500, "Sconto 5 IV"), oApp.FormatSconti, 15, -100, 100)
      ls_scont6_4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564440468750, "Sconto 6 IV"), oApp.FormatSconti, 15, -100, 100)
      ls_scdaquant_4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564440625000, "Da qta IV"), oApp.FormatSconti, 15)
      ls_scaquant_4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564440781250, "A qta IV"), oApp.FormatSconti, 15)
      ls_dadatasc_4.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129212564441093750, "Da data IV"), True)
      ls_adatasc_4.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129212564441250000, "A data IV"), True)
      ls_codtpro_4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564441406250, "Promozione IV"), "0", 4, 0, 9999)
      ls_clscan.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564441718750, "Classe sconto cli\for IV"), "0", 4, 0, 9999)
      ls_clscar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129212564441875000, "Classe sconto art. IV"), "0", 4, 0, 9999)
      ckSconti4.NTSSetParam(oMenu, oApp.Tr(Me, 129212564445000000, "Sconto 4"), "S", "N")
      ckSconti3.NTSSetParam(oMenu, oApp.Tr(Me, 129212564445156250, "Sconto 3"), "S", "N")
      ckSconti2.NTSSetParam(oMenu, oApp.Tr(Me, 129212564445312500, "Sconto 2"), "S", "N")
      ckSconti1.NTSSetParam(oMenu, oApp.Tr(Me, 129212564445468750, "Sconto 1"), "S", "N")
      ckListino5.NTSSetParam(oMenu, oApp.Tr(Me, 129212564445625000, "Listino 5"), "S", "N")
      ckListino4.NTSSetParam(oMenu, oApp.Tr(Me, 129212564445781250, "Listino 4"), "S", "N")
      ckListino3.NTSSetParam(oMenu, oApp.Tr(Me, 129212564445937500, "Listino 3"), "S", "N")
      ckListino2.NTSSetParam(oMenu, oApp.Tr(Me, 129212564446093750, "Listino 2"), "S", "N")
      ckListino1.NTSSetParam(oMenu, oApp.Tr(Me, 129212564446250000, "Listino 1"), "S", "N")
      xx_desccli.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212650557153350, "Descr. cli\for"), 0, True)
      xx_descart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212650557465852, "Descr. Art."), 0, True)
      xx_fase.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212650557778354, "Descr. Fase"), 0, True)
      xx_desclavo1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212650559028362, "Descr. lavo. 1"), 0, True)
      xx_descvalu1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212650559809617, "Descr. valu. 1"), 0, True)
      xx_desclavo2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212650561215876, "Descr. lavo. 2"), 0, True)
      xx_descvalu2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212650561997131, "Descr. valu. 2"), 0, True)
      xx_desclavo3.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212650563403390, "Descr. lavo. 3"), 0, True)
      xx_descvalu3.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212650564184645, "Descr. valu. 3"), 0, True)
      xx_desclavo4.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212650565590904, "Descr. lavo. 4"), 0, True)
      xx_descvalu4.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212650566372159, "Descr. valu. 4"), 0, True)
      xx_desclavo5.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212650567778418, "Descr. lavo. 5"), 0, True)
      xx_descvalu5.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212650568559673, "Descr. valu. 5"), 0, True)
      xx_descpromo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212650570747187, "Descr. promozione I"), 0, True)
      xx_descpromo_2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212650573090952, "Descr. promozione II"), 0, True)
      xx_descpromo_3.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212650575434717, "Descr. promozione III"), 0, True)
      xx_descpromo_4.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129212650577778482, "Descr. promozione IV"), 0, True)
      ls_codpromo1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129215816036999118, "Promozione 1"), "0", 4, 0, 9999)
      xx_descpromo1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129215816037155368, "Desc. promo. 1"), 0, True)
      ls_codpromo2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129215816037311618, "Promozione 2"), "0", 4, 0, 9999)
      xx_descpromo2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129215816037467868, "Desc. promo. 2"), 0, True)
      ls_codpromo3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129215816037624118, "Promozione 3"), "0", 4, 0, 9999)
      xx_descpromo3.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129215816037780368, "Desc. promo. 3"), 0, True)
      ls_codpromo4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129215816037936618, "Promozione 4"), "0", 4, 0, 9999)
      xx_descpromo4.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129215816038092868, "Desc. promo. 4"), 0, True)
      ls_codpromo5.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129215816038249118, "Promozione 5"), "0", 4, 0, 9999)
      xx_descpromo5.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129215816038405368, "Desc. promo. 5"), 0, True)
      xx_clscan.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129218657243064873, "Desc. classe Cli\for I"), 0, True)
      xx_clscan.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129218657243533623, "Desc. classe cli\for IV"), 0, True)
      xx_clscar.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129218657243689873, "Desc. classe articolo I"), 0, True)
      xx_clscar.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129218657244158623, "Desc. classe articolo IV"), 0, True)
      xx_desint.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130456605578551227, "Descr. interna"), 0, True)
      ls_tiposc.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129218657244314873, "Tipo I"), dttTipoSc, "val", "cod")
      ls_tiposc_2.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129218657244471123, "Tipo II"), dttTipoSc, "val", "cod")
      ls_tiposc_3.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129218657244627373, "Tipo III"), dttTipoSc, "val", "cod")
      ls_tiposc_4.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129218657244783623, "Tipo IV"), dttTipoSc, "val", "cod")
      ar_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129537466034227526, "Articolo"), 0, True)
      ls_coddest.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129048472787388675, "Destinazione diversa"), "0", 9, -1, 999999999)
      ls_coddest.CliePerDestdiv = ls_conto
      xx_coddest.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130585490810476458, "Descrizione destinazione diversa"), 0, True)

      ckColoreNero.NTSSetParam(oMenu, oApp.Tr(Me, 129218657251502373, "Testo nero per i dati non disponibili"), "S", "N")
      edArtico.NTSSetParam(oMenu, oApp.Tr(Me, 129215816042311618, "Articolo"), 0)
      edFase.NTSSetParam(oMenu, oApp.Tr(Me, 129215816042467868, "Fase"), "0")
      edConto.NTSSetParam(oMenu, oApp.Tr(Me, 129215816042936618, "Conto"), "0")
      ckBlocca.NTSSetParam(oMenu, oApp.Tr(Me, 129215816043092868, "Blocca intestazione riga"), "S", "N")

      grvList.NTSAllowInsert = False

      If CLN__STD.IsBis Then
        For i = 1 To 5
          grvList.AddColumnBackColor("backcolor_ls_listino" & i.ToString) 'sempre nella InitControls
          grvList.AddColumnBackColor("backcolor_ls_prz" & i.ToString) 'sempre nella InitControls
          grvList.AddColumnBackColor("backcolor_ls_daquant" & i.ToString) 'sempre nella InitControls
          grvList.AddColumnBackColor("backcolor_ls_aquant" & i.ToString) 'sempre nella InitControls
          grvList.AddColumnBackColor("backcolor_ls_unmis" & i.ToString) 'sempre nella InitControls
          grvList.AddColumnBackColor("backcolor_ls_codlavo" & i.ToString) 'sempre nella InitControls
          grvList.AddColumnBackColor("backcolor_xx_desclavo" & i.ToString) 'sempre nella InitControls
          grvList.AddColumnBackColor("backcolor_ls_tipo" & i.ToString) 'sempre nella InitControls
          grvList.AddColumnBackColor("backcolor_ls_dadata" & i.ToString) 'sempre nella InitControls
          grvList.AddColumnBackColor("backcolor_ls_adata" & i.ToString) 'sempre nella InitControls
          grvList.AddColumnBackColor("backcolor_ls_codvaluo" & i.ToString) 'sempre nella InitControls
          grvList.AddColumnBackColor("backcolor_xx_descvalu" & i.ToString) 'sempre nella InitControls
          grvList.AddColumnBackColor("backcolor_ls_prznet" & i.ToString) 'sempre nella InitControls
          grvList.AddColumnBackColor("backcolor_ls_codpromo" & i.ToString) 'sempre nella InitControls
          grvList.AddColumnBackColor("backcolor_xx_descpromo" & i.ToString) 'sempre nella InitControls
        Next

        For i = 1 To 4
          grvList.AddColumnBackColor("backcolor_ls_scont1_" & i.ToString) 'sempre nella InitControls
          grvList.AddColumnBackColor("backcolor_ls_scont2_" & i.ToString) 'sempre nella InitControls
          grvList.AddColumnBackColor("backcolor_ls_scont3_" & i.ToString) 'sempre nella InitControls
          grvList.AddColumnBackColor("backcolor_ls_scont4_" & i.ToString) 'sempre nella InitControls
          grvList.AddColumnBackColor("backcolor_ls_scont5_" & i.ToString) 'sempre nella InitControls
          grvList.AddColumnBackColor("backcolor_ls_scont6_" & i.ToString) 'sempre nella InitControls
          grvList.AddColumnBackColor("backcolor_ls_scdaquant_" & i.ToString) 'sempre nella InitControls
          grvList.AddColumnBackColor("backcolor_ls_scaquant_" & i.ToString) 'sempre nella InitControls
          grvList.AddColumnBackColor("backcolor_ls_tiposc_" & i.ToString) 'sempre nella InitControls
          grvList.AddColumnBackColor("backcolor_ls_dadatasc_" & i.ToString) 'sempre nella InitControls
          grvList.AddColumnBackColor("backcolor_ls_adatasc_" & i.ToString) 'sempre nella InitControls
          grvList.AddColumnBackColor("backcolor_ls_codtpro_" & i.ToString) 'sempre nella InitControls
          grvList.AddColumnBackColor("backcolor_xx_descpromo_" & i.ToString) 'sempre nella InitControls
        Next
      End If


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
  Public Overridable Sub FRMMGLISE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim bListino1, bListino2, bListino3, bListino4, bListino5 As Boolean
    Dim bSconti1, bSconti2, bSconti3, bSconti4 As Boolean
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '--------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      oCleStli.GetDataLise(dsLise)
      dcLise.DataSource = dsLise.Tables("LISTSES")
      If CLN__STD.IsBis Then
        For Each dtrT As DataRow In dsLise.Tables("LISTSES").Rows
          AssegnaColoriGriglia(dtrT)
        Next
      End If

      grList.DataSource = dcLise

      If dsLise.Tables("LISTSES").Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129217537728420695, "Non esistono dati con queste caratteristiche"))
        Me.Close()
        Return
      End If

      bPromo1 = CBool(dsLise.Tables("LISTSES").Select("ls_codpromo1 <> 0").Length)
      bPromo2 = CBool(dsLise.Tables("LISTSES").Select("ls_codpromo2 <> 0").Length)
      bPromo3 = CBool(dsLise.Tables("LISTSES").Select("ls_codpromo3 <> 0").Length)
      bPromo4 = CBool(dsLise.Tables("LISTSES").Select("ls_codpromo4 <> 0").Length)
      bPromo5 = CBool(dsLise.Tables("LISTSES").Select("ls_codpromo5 <> 0").Length)

      bValuta1 = CBool(dsLise.Tables("LISTSES").Select("ls_codvalu1 <> 0").Length)
      bValuta2 = CBool(dsLise.Tables("LISTSES").Select("ls_codvalu2 <> 0").Length)
      bValuta3 = CBool(dsLise.Tables("LISTSES").Select("ls_codvalu3 <> 0").Length)
      bValuta4 = CBool(dsLise.Tables("LISTSES").Select("ls_codvalu4 <> 0").Length)
      bValuta5 = CBool(dsLise.Tables("LISTSES").Select("ls_codvalu5 <> 0").Length)

      bLavo1 = CBool(dsLise.Tables("LISTSES").Select("ls_codlavo1 <> 0").Length)
      bLavo2 = CBool(dsLise.Tables("LISTSES").Select("ls_codlavo2 <> 0").Length)
      bLavo3 = CBool(dsLise.Tables("LISTSES").Select("ls_codlavo3 <> 0").Length)
      bLavo4 = CBool(dsLise.Tables("LISTSES").Select("ls_codlavo4 <> 0").Length)
      bLavo5 = CBool(dsLise.Tables("LISTSES").Select("ls_codlavo5 <> 0").Length)

      bPromoSc1 = CBool(dsLise.Tables("LISTSES").Select("ls_codtpro <> 0").Length)
      bPromoSc2 = CBool(dsLise.Tables("LISTSES").Select("ls_codtpro_2 <> 0").Length)
      bPromoSc3 = CBool(dsLise.Tables("LISTSES").Select("ls_codtpro_3 <> 0").Length)
      bPromoSc4 = CBool(dsLise.Tables("LISTSES").Select("ls_codtpro_4 <> 0").Length)

      bListino1 = CBool(dsLise.Tables("LISTSES").Select("ls_progr1 <> 0").Length) 'Così se indico un listino non valido non viene visualizzato.
      bListino2 = CBool(dsLise.Tables("LISTSES").Select("ls_progr2 <> 0").Length)
      bListino3 = CBool(dsLise.Tables("LISTSES").Select("ls_progr3 <> 0").Length)
      bListino4 = CBool(dsLise.Tables("LISTSES").Select("ls_progr4 <> 0").Length)
      bListino5 = CBool(dsLise.Tables("LISTSES").Select("ls_progr5 <> 0").Length)

      bSconti1 = CBool(dsLise.Tables("LISTSES").Select("ls_nuovosc = 'N'").Length)
      bSconti2 = CBool(dsLise.Tables("LISTSES").Select("ls_nuovosc_2 = 'N'").Length)
      bSconti3 = CBool(dsLise.Tables("LISTSES").Select("ls_nuovosc_3 = 'N'").Length)
      bSconti4 = CBool(dsLise.Tables("LISTSES").Select("ls_nuovosc_4 = 'N'").Length)

      bScont4 = grvList.Columns("ls_scont4").Visible
      bScont5 = grvList.Columns("ls_scont5").Visible
      bScont6 = grvList.Columns("ls_scont6").Visible
      bScont4_2 = grvList.Columns("ls_scont4_2").Visible
      bScont5_2 = grvList.Columns("ls_scont5_2").Visible
      bScont6_2 = grvList.Columns("ls_scont6_2").Visible
      bScont4_3 = grvList.Columns("ls_scont4_3").Visible
      bScont5_3 = grvList.Columns("ls_scont5_3").Visible
      bScont6_3 = grvList.Columns("ls_scont6_3").Visible
      bScont4_4 = grvList.Columns("ls_scont4_4").Visible
      bScont5_4 = grvList.Columns("ls_scont5_4").Visible
      bScont6_4 = grvList.Columns("ls_scont6_4").Visible
      bDaQuant1 = grvList.Columns("ls_daquant1").Visible
      bDaQuant2 = grvList.Columns("ls_daquant2").Visible
      bDaQuant3 = grvList.Columns("ls_daquant3").Visible
      bDaQuant4 = grvList.Columns("ls_daquant4").Visible
      bDaQuant5 = grvList.Columns("ls_daquant5").Visible
      bDaQuant = grvList.Columns("ls_scdaquant").Visible
      bDaQuant_2 = grvList.Columns("ls_scdaquant_2").Visible
      bDaQuant_3 = grvList.Columns("ls_scdaquant_3").Visible
      bDaQuant_4 = grvList.Columns("ls_scdaquant_4").Visible
      bAQuant1 = grvList.Columns("ls_aquant1").Visible
      bAQuant2 = grvList.Columns("ls_aquant2").Visible
      bAQuant3 = grvList.Columns("ls_aquant3").Visible
      bAQuant4 = grvList.Columns("ls_aquant4").Visible
      bAQuant5 = grvList.Columns("ls_aquant5").Visible
      bAQuant = grvList.Columns("ls_scaquant").Visible
      bAQuant_2 = grvList.Columns("ls_scaquant_2").Visible
      bAQuant_3 = grvList.Columns("ls_scaquant_3").Visible
      bAQuant_4 = grvList.Columns("ls_scaquant_4").Visible

      ckListino1.Text = oApp.Tr(Me, 129217539818576945, "Listino ") & NTSCInt(dsLise.Tables("LISTSES").Rows(0)!ls_listino1)
      ckListino2.Text = oApp.Tr(Me, 129217539977326945, "Listino ") & NTSCInt(dsLise.Tables("LISTSES").Rows(0)!ls_listino2)
      ckListino3.Text = oApp.Tr(Me, 129217539987795695, "Listino ") & NTSCInt(dsLise.Tables("LISTSES").Rows(0)!ls_listino3)
      ckListino4.Text = oApp.Tr(Me, 129217539997951945, "Listino ") & NTSCInt(dsLise.Tables("LISTSES").Rows(0)!ls_listino4)
      ckListino5.Text = oApp.Tr(Me, 129217540011545695, "Listino ") & NTSCInt(dsLise.Tables("LISTSES").Rows(0)!ls_listino5)

      ckListino1.Checked = bListino1
      ckListino2.Checked = bListino2
      ckListino3.Checked = bListino3
      ckListino4.Checked = bListino4
      ckListino5.Checked = bListino5
      ckSconti1.Checked = bSconti1
      ckSconti2.Checked = bSconti2
      ckSconti3.Checked = bSconti3
      ckSconti4.Checked = bSconti4

      ckListino1.Visible = bListino1
      ckListino2.Visible = bListino2
      ckListino3.Visible = bListino3
      ckListino4.Visible = bListino4
      ckListino5.Visible = bListino5
      ckSconti1.Visible = bSconti1
      ckSconti2.Visible = bSconti2
      ckSconti3.Visible = bSconti3
      ckSconti4.Visible = bSconti4

      BloccoColonne()

      ' Inizializzo qui la colonna unità di misura per avere l'articolo caricato e attivare solo le sue unità di misura
      dttUm = oCleStli.CaricaUnMis()
      dttUm.Rows.Add(New Object() {" "})
      dttUm.AcceptChanges()

      ls_unmis1.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128708025253792763, "U.M."), dttUm, "tb_codumis", "tb_codumis")
      ls_unmis2.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129217773831406250, "U.M."), dttUm, "tb_codumis", "tb_codumis")
      ls_unmis3.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129217773842031250, "U.M."), dttUm, "tb_codumis", "tb_codumis")
      ls_unmis4.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129217773853281250, "U.M."), dttUm, "tb_codumis", "tb_codumis")
      ls_unmis5.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129217773863437500, "U.M."), dttUm, "tb_codumis", "tb_codumis")


      If CLN__STD.IsBis Then
        ckBlocca.Visible = False
        lbB.Visible = False
        lbBDesc.Visible = False
        lbRosso.Visible = False
        lbRossoDesc.Visible = False
      End If

      tmTick.Start()
      'Controlla se ci sono delle modifiche ai listini\sconti originali
      oCleStli.CheckSync()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi"
  Public Overridable Sub ckListino1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckListino1.CheckedChanged
    Try
      If ckListino1.Checked Then
        GctlSetVisEnab(ls_listino1, True)
        GctlSetVisEnab(ls_prz1, True)
        If bDaQuant1 Then GctlSetVisEnab(ls_daquant1, True)
        If bAQuant1 Then GctlSetVisEnab(ls_aquant1, True)
        GctlSetVisEnab(ls_unmis1, True)
        If bLavo1 Then
          GctlSetVisEnab(ls_codlavo1, True)
          GctlSetVisEnab(xx_desclavo1, True)
        End If
        GctlSetVisEnab(ls_tipo1, True)
        GctlSetVisEnab(ls_dadata1, True)
        GctlSetVisEnab(ls_adata1, True)
        If bValuta1 Then
          GctlSetVisEnab(ls_codvalu1, True)
          GctlSetVisEnab(xx_descvalu1, True)
        End If
        GctlSetVisEnab(ls_prznet1, True)
        If bPromo1 Then
          GctlSetVisEnab(ls_codpromo1, True)
          GctlSetVisEnab(xx_descpromo1, True)
        End If
      Else
        ls_listino1.Visible = False
        ls_prz1.Visible = False
        ls_daquant1.Visible = False
        ls_aquant1.Visible = False
        ls_perqta.Visible = False
        ls_unmis1.Visible = False
        ls_codlavo1.Visible = False
        xx_desclavo1.Visible = False
        ls_tipo1.Visible = False
        ls_dadata1.Visible = False
        ls_adata1.Visible = False
        ls_codvalu1.Visible = False
        xx_descvalu1.Visible = False
        ls_prznet1.Visible = False
        ls_codpromo1.Visible = False
        xx_descpromo1.Visible = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckListino2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckListino2.CheckedChanged
    Try
      If ckListino2.Checked Then
        GctlSetVisEnab(ls_listino2, True)
        GctlSetVisEnab(ls_prz2, True)
        If bDaQuant2 Then GctlSetVisEnab(ls_daquant2, True)
        If bAQuant2 Then GctlSetVisEnab(ls_aquant2, True)
        GctlSetVisEnab(ls_unmis2, True)
        If bLavo2 Then
          GctlSetVisEnab(ls_codlavo2, True)
          GctlSetVisEnab(xx_desclavo2, True)
        End If
        GctlSetVisEnab(ls_tipo2, True)
        GctlSetVisEnab(ls_dadata2, True)
        GctlSetVisEnab(ls_adata2, True)
        If bValuta2 Then
          GctlSetVisEnab(ls_codvalu2, True)
          GctlSetVisEnab(xx_descvalu2, True)
        End If
        GctlSetVisEnab(ls_prznet2, True)
        If bPromo2 Then
          GctlSetVisEnab(ls_codpromo2, True)
          GctlSetVisEnab(xx_descpromo2, True)
        End If
      Else
        ls_listino2.Visible = False
        ls_prz2.Visible = False
        ls_daquant2.Visible = False
        ls_aquant2.Visible = False
        ls_unmis2.Visible = False
        ls_codlavo2.Visible = False
        xx_desclavo2.Visible = False
        ls_tipo2.Visible = False
        ls_dadata2.Visible = False
        ls_adata2.Visible = False
        ls_codvalu2.Visible = False
        xx_descvalu2.Visible = False
        ls_prznet2.Visible = False
        ls_codpromo2.Visible = False
        xx_descpromo2.Visible = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckListino3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckListino3.CheckedChanged
    Try
      If ckListino3.Checked Then
        GctlSetVisEnab(ls_listino3, True)
        GctlSetVisEnab(ls_prz3, True)
        If bDaQuant3 Then GctlSetVisEnab(ls_daquant3, True)
        If bAQuant3 Then GctlSetVisEnab(ls_aquant3, True)
        GctlSetVisEnab(ls_unmis3, True)
        If bLavo3 Then
          GctlSetVisEnab(ls_codlavo3, True)
          GctlSetVisEnab(xx_desclavo3, True)
        End If
        GctlSetVisEnab(ls_tipo3, True)
        GctlSetVisEnab(ls_dadata3, True)
        GctlSetVisEnab(ls_adata3, True)
        If bValuta3 Then
          GctlSetVisEnab(ls_codvalu3, True)
          GctlSetVisEnab(xx_descvalu3, True)
        End If
        GctlSetVisEnab(ls_prznet3, True)
        If bPromo3 Then
          GctlSetVisEnab(ls_codpromo3, True)
          GctlSetVisEnab(xx_descpromo3, True)
        End If
      Else
        ls_listino3.Visible = False
        ls_prz3.Visible = False
        ls_daquant3.Visible = False
        ls_aquant3.Visible = False
        ls_unmis3.Visible = False
        ls_codlavo3.Visible = False
        xx_desclavo3.Visible = False
        ls_tipo3.Visible = False
        ls_dadata3.Visible = False
        ls_adata3.Visible = False
        ls_codvalu3.Visible = False
        xx_descvalu3.Visible = False
        ls_prznet3.Visible = False
        ls_codpromo3.Visible = False
        xx_descpromo3.Visible = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckListino4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckListino4.CheckedChanged
    Try
      If ckListino4.Checked Then
        GctlSetVisEnab(ls_listino4, True)
        GctlSetVisEnab(ls_prz4, True)
        If bDaQuant4 Then GctlSetVisEnab(ls_daquant4, True)
        If bAQuant4 Then GctlSetVisEnab(ls_aquant4, True)
        GctlSetVisEnab(ls_unmis4, True)
        If bLavo4 Then
          GctlSetVisEnab(ls_codlavo4, True)
          GctlSetVisEnab(xx_desclavo4, True)
        End If
        GctlSetVisEnab(ls_tipo4, True)
        GctlSetVisEnab(ls_dadata4, True)
        GctlSetVisEnab(ls_adata4, True)
        If bValuta4 Then
          GctlSetVisEnab(ls_codvalu4, True)
          GctlSetVisEnab(xx_descvalu4, True)
        End If
        GctlSetVisEnab(ls_prznet4, True)
        If bPromo4 Then
          GctlSetVisEnab(ls_codpromo4, True)
          GctlSetVisEnab(xx_descpromo4, True)
        End If
      Else
        ls_listino4.Visible = False
        ls_prz4.Visible = False
        ls_daquant4.Visible = False
        ls_aquant4.Visible = False
        ls_unmis4.Visible = False
        ls_codlavo4.Visible = False
        xx_desclavo4.Visible = False
        ls_tipo4.Visible = False
        ls_dadata4.Visible = False
        ls_adata4.Visible = False
        ls_codvalu4.Visible = False
        xx_descvalu4.Visible = False
        ls_prznet4.Visible = False
        ls_codpromo4.Visible = False
        xx_descpromo4.Visible = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckListino5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckListino5.CheckedChanged
    Try
      If ckListino5.Checked Then
        GctlSetVisEnab(ls_listino5, True)
        GctlSetVisEnab(ls_prz5, True)
        If bDaQuant5 Then GctlSetVisEnab(ls_daquant5, True)
        If bAQuant5 Then GctlSetVisEnab(ls_aquant5, True)
        GctlSetVisEnab(ls_unmis5, True)
        If bLavo5 Then
          GctlSetVisEnab(ls_codlavo5, True)
          GctlSetVisEnab(xx_desclavo5, True)
        End If
        GctlSetVisEnab(ls_tipo5, True)
        GctlSetVisEnab(ls_dadata5, True)
        GctlSetVisEnab(ls_adata5, True)
        If bValuta5 Then
          GctlSetVisEnab(ls_codvalu5, True)
          GctlSetVisEnab(xx_descvalu5, True)
        End If
        GctlSetVisEnab(ls_prznet5, True)
        If bPromo5 Then
          GctlSetVisEnab(ls_codpromo5, True)
          GctlSetVisEnab(xx_descpromo5, True)
        End If
      Else
        ls_listino5.Visible = False
        ls_prz5.Visible = False
        ls_daquant5.Visible = False
        ls_aquant5.Visible = False
        ls_unmis5.Visible = False
        ls_codlavo5.Visible = False
        xx_desclavo5.Visible = False
        ls_tipo5.Visible = False
        ls_dadata5.Visible = False
        ls_adata5.Visible = False
        ls_codvalu5.Visible = False
        xx_descvalu5.Visible = False
        ls_prznet5.Visible = False
        ls_codpromo5.Visible = False
        xx_descpromo5.Visible = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckSconti1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSconti1.CheckedChanged
    Try
      If ckSconti1.Checked Then
        GctlSetVisEnab(ls_scont1, True)
        GctlSetVisEnab(ls_scont2, True)
        GctlSetVisEnab(ls_scont3, True)
        If bScont4 Then GctlSetVisEnab(ls_scont4, True)
        If bScont5 Then GctlSetVisEnab(ls_scont5, True)
        If bScont6 Then GctlSetVisEnab(ls_scont6, True)
        If bDaQuant Then GctlSetVisEnab(ls_scdaquant, True)
        If bAQuant Then GctlSetVisEnab(ls_scaquant, True)
        GctlSetVisEnab(ls_dadatasc, True)
        GctlSetVisEnab(ls_adatasc, True)
        GctlSetVisEnab(ls_codtpro, True)
        GctlSetVisEnab(xx_descpromo, True)
        GctlSetVisEnab(ls_tiposc, True)
      Else
        ls_scont1.Visible = False
        ls_scont2.Visible = False
        ls_scont3.Visible = False
        ls_scont4.Visible = False
        ls_scont5.Visible = False
        ls_scont6.Visible = False
        ls_scdaquant.Visible = False
        ls_scaquant.Visible = False
        ls_dadatasc.Visible = False
        ls_adatasc.Visible = False
        ls_codtpro.Visible = False
        xx_descpromo.Visible = False
        ls_tiposc.Visible = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckSconti2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSconti2.CheckedChanged
    Try
      If ckSconti2.Checked Then
        GctlSetVisEnab(ls_scont1_2, True)
        GctlSetVisEnab(ls_scont2_2, True)
        GctlSetVisEnab(ls_scont3_2, True)
        If bScont4_2 Then GctlSetVisEnab(ls_scont4_2, True)
        If bScont5_2 Then GctlSetVisEnab(ls_scont5_2, True)
        If bScont6_2 Then GctlSetVisEnab(ls_scont6_2, True)
        If bDaQuant_2 Then GctlSetVisEnab(ls_scdaquant_2, True)
        If bAQuant_2 Then GctlSetVisEnab(ls_scaquant_2, True)
        GctlSetVisEnab(ls_dadatasc_2, True)
        GctlSetVisEnab(ls_adatasc_2, True)
        GctlSetVisEnab(ls_codtpro_2, True)
        GctlSetVisEnab(xx_descpromo_2, True)
        GctlSetVisEnab(ls_tiposc_2, True)
      Else
        ls_scont1_2.Visible = False
        ls_scont2_2.Visible = False
        ls_scont3_2.Visible = False
        ls_scont4_2.Visible = False
        ls_scont5_2.Visible = False
        ls_scont6_2.Visible = False
        ls_scdaquant_2.Visible = False
        ls_scaquant_2.Visible = False
        ls_dadatasc_2.Visible = False
        ls_adatasc_2.Visible = False
        ls_codtpro_2.Visible = False
        xx_descpromo_2.Visible = False
        ls_tiposc_2.Visible = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckSconti3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSconti3.CheckedChanged
    Try
      If ckSconti3.Checked Then
        GctlSetVisEnab(ls_scont1_3, True)
        GctlSetVisEnab(ls_scont2_3, True)
        GctlSetVisEnab(ls_scont3_3, True)
        If bScont4_3 Then GctlSetVisEnab(ls_scont4_3, True)
        If bScont5_3 Then GctlSetVisEnab(ls_scont5_3, True)
        If bScont6_3 Then GctlSetVisEnab(ls_scont6_3, True)
        If bDaQuant_3 Then GctlSetVisEnab(ls_scdaquant_3, True)
        If bAQuant_3 Then GctlSetVisEnab(ls_scaquant_3, True)
        GctlSetVisEnab(ls_dadatasc_3, True)
        GctlSetVisEnab(ls_adatasc_3, True)
        GctlSetVisEnab(ls_codtpro_3, True)
        GctlSetVisEnab(xx_descpromo_3, True)
        GctlSetVisEnab(ls_tiposc_3, True)
      Else
        ls_scont1_3.Visible = False
        ls_scont2_3.Visible = False
        ls_scont3_3.Visible = False
        ls_scont4_3.Visible = False
        ls_scont5_3.Visible = False
        ls_scont6_3.Visible = False
        ls_scdaquant_3.Visible = False
        ls_scaquant_3.Visible = False
        ls_dadatasc_3.Visible = False
        ls_adatasc_3.Visible = False
        ls_codtpro_3.Visible = False
        xx_descpromo_3.Visible = False
        ls_tiposc_3.Visible = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckSconti4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSconti4.CheckedChanged
    Try
      If ckSconti4.Checked Then
        GctlSetVisEnab(ls_scont1_4, True)
        GctlSetVisEnab(ls_scont2_4, True)
        GctlSetVisEnab(ls_scont3_4, True)
        If bScont4_4 Then GctlSetVisEnab(ls_scont4_4, True)
        If bScont5_4 Then GctlSetVisEnab(ls_scont5_4, True)
        If bScont6_4 Then GctlSetVisEnab(ls_scont6_4, True)
        If bDaQuant_4 Then GctlSetVisEnab(ls_scdaquant_4, True)
        If bAQuant_4 Then GctlSetVisEnab(ls_scaquant_4, True)
        GctlSetVisEnab(ls_dadatasc_4, True)
        GctlSetVisEnab(ls_adatasc_4, True)
        GctlSetVisEnab(ls_codtpro_4, True)
        GctlSetVisEnab(xx_descpromo_4, True)
        GctlSetVisEnab(ls_tiposc_4, True)
      Else
        ls_scont1_4.Visible = False
        ls_scont2_4.Visible = False
        ls_scont3_4.Visible = False
        ls_scont4_4.Visible = False
        ls_scont5_4.Visible = False
        ls_scont6_4.Visible = False
        ls_scdaquant_4.Visible = False
        ls_scaquant_4.Visible = False
        ls_dadatasc_4.Visible = False
        ls_adatasc_4.Visible = False
        ls_codtpro_4.Visible = False
        xx_descpromo_4.Visible = False
        ls_tiposc_4.Visible = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckBlocca_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckBlocca.CheckedChanged
    Try
      If ckBlocca.Checked Then
        ls_conto.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        xx_desccli.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        ls_codart.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        xx_descart.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        ls_fase.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        xx_fase.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        ls_coddest.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        xx_coddest.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
      Else
        xx_fase.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None
        ls_fase.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None
        xx_descart.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None
        ls_codart.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None
        xx_desccli.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None
        ls_conto.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None
        ls_coddest.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None
        xx_coddest.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckColoreNero_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckColoreNero.CheckedChanged
    Dim lNum As Integer
    Try
      If grvList.FocusedRowHandle > 0 Then lNum = -1 Else lNum = 1

      grvList.FocusedRowHandle += lNum
      grvList.FocusedRowHandle -= lNum
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tmTick_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmTick.Tick
    Try
      'Controlla se ci sono delle modifiche ai listini\sconti originali
      oCleStli.CheckSync()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi Griglia"
  Public Overridable Sub grvList_CustomDrawCell(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles grvList.CustomDrawCell
    Dim r As Rectangle
    Dim dttUbicSup As New DataTable
    Try

      If e.RowHandle = grvList.FocusedRowHandle Then Return

      r = e.Bounds

      e.Handled = True
      With grvList.GetDataRow(e.RowHandle)
        ' escludendo la riga con il nome colora le righe in base al valore che contengono
        Select Case e.Column.FieldName
          Case "ls_listino1", "ls_prz1", "ls_daquant1", "ls_aquant1", "ls_unmis1", "ls_codlavo1", "xx_desclavo1", _
               "ls_tipo1", "ls_dadata1", "ls_adata1", "ls_codvalu1", "xx_descvalu1", "ls_prznet1", "ls_codpromo1", "xx_descpromo1"
            e.Graphics.FillRectangle(Brushes.MediumAquamarine, r)

            If NTSCInt(!ls_progr1) = 0 And Not ckColoreNero.Checked Then
              e.Appearance.ForeColor = Color.Silver
            End If

            If NTSCStr(!ls_modificato1) = "S" Then
              e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
            End If

            If NTSCStr(!xx_sync1) = "S" Then
              e.Appearance.ForeColor = Color.IndianRed
            End If
          Case "ls_listino2", "ls_prz2", "ls_daquant2", "ls_aquant2", "ls_unmis2", "ls_codlavo2", "xx_desclavo2", _
               "ls_tipo2", "ls_dadata2", "ls_adata2", "ls_codvalu2", "xx_descvalu2", "ls_prznet2", "ls_codpromo2", "xx_descpromo2"
            e.Graphics.FillRectangle(Brushes.Aquamarine, r)

            If NTSCInt(!ls_progr2) = 0 And Not ckColoreNero.Checked Then
              e.Appearance.ForeColor = Color.Silver
            End If

            If NTSCStr(!ls_modificato2) = "S" Then
              e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
            End If

            If NTSCStr(!xx_sync2) = "S" Then
              e.Appearance.ForeColor = Color.IndianRed
            End If
          Case "ls_listino3", "ls_prz3", "ls_daquant3", "ls_aquant3", "ls_unmis3", "ls_codlavo3", "xx_desclavo3", _
               "ls_tipo3", "ls_dadata3", "ls_adata3", "ls_codvalu3", "xx_descvalu3", "ls_prznet3", "ls_codpromo3", "xx_descpromo3"
            e.Graphics.FillRectangle(Brushes.LightSkyBlue, r)

            If NTSCInt(!ls_progr3) = 0 And Not ckColoreNero.Checked Then
              e.Appearance.ForeColor = Color.Silver
            End If

            If NTSCStr(!ls_modificato3) = "S" Then
              e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
            End If

            If NTSCStr(!xx_sync3) = "S" Then
              e.Appearance.ForeColor = Color.IndianRed
            End If
          Case "ls_listino4", "ls_prz4", "ls_daquant4", "ls_aquant4", "ls_unmis4", "ls_codlavo4", "xx_desclavo4", _
               "ls_tipo4", "ls_dadata4", "ls_adata4", "ls_codvalu4", "xx_descvalu4", "ls_prznet4", "ls_codpromo4", "xx_descpromo4"
            e.Graphics.FillRectangle(Brushes.PaleTurquoise, r)

            If NTSCInt(!ls_progr4) = 0 And Not ckColoreNero.Checked Then
              e.Appearance.ForeColor = Color.Silver
            End If

            If NTSCStr(!ls_modificato4) = "S" Then
              e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
            End If

            If NTSCStr(!xx_sync4) = "S" Then
              e.Appearance.ForeColor = Color.IndianRed
            End If
          Case "ls_listino5", "ls_prz5", "ls_daquant5", "ls_aquant5", "ls_unmis5", "ls_codlavo5", "xx_desclavo5", _
               "ls_tipo5", "ls_dadata5", "ls_adata5", "ls_codvalu5", "xx_descvalu5", "ls_prznet5", "ls_codpromo5", "xx_descpromo5"
            e.Graphics.FillRectangle(Brushes.Honeydew, r)

            If NTSCInt(!ls_progr5) = 0 And Not ckColoreNero.Checked Then
              e.Appearance.ForeColor = Color.Silver
            End If

            If NTSCStr(!ls_modificato5) = "S" Then
              e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
            End If

            If NTSCStr(!xx_sync5) = "S" Then
              e.Appearance.ForeColor = Color.IndianRed
            End If
          Case "ls_scont1", "ls_scont2", "ls_scont3", "ls_scont4", "ls_scont5", "ls_scont6", "ls_scdaquant", "ls_scaquant", "ls_tiposc", _
               "ls_dadatasc", "ls_adatasc", "ls_codtpro", "xx_descpromo"
            e.Graphics.FillRectangle(Brushes.Yellow, r)

            If NTSCStr(!ls_nuovosc) = "S" And Not ckColoreNero.Checked Then
              e.Appearance.ForeColor = Color.Silver
            End If

            If NTSCStr(!ls_modificatosc) = "S" Then
              e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
            End If

            If NTSCStr(!xx_syncsc) = "S" Then
              e.Appearance.ForeColor = Color.IndianRed
            End If
          Case "ls_scont1_2", "ls_scont2_2", "ls_scont3_2", "ls_scont4_2", "ls_scont5_2", "ls_scont6_2", "ls_scdaquant_2", "ls_scaquant_2", "ls_tiposc_2", _
               "ls_dadatasc_2", "ls_adatasc_2", "ls_codtpro_2", "xx_descpromo_2"
            e.Graphics.FillRectangle(Brushes.Gold, r)

            If NTSCStr(!ls_nuovosc_2) = "S" And Not ckColoreNero.Checked Then
              e.Appearance.ForeColor = Color.Silver
            End If

            If NTSCStr(!ls_modificatosc_2) = "S" Then
              e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
            End If

            If NTSCStr(!xx_syncsc_2) = "S" Then
              e.Appearance.ForeColor = Color.IndianRed
            End If
          Case "ls_scont1_3", "ls_scont2_3", "ls_scont3_3", "ls_scont4_3", "ls_scont5_3", "ls_scont6_3", "ls_scdaquant_3", "ls_scaquant_3", "ls_tiposc_3", _
               "ls_dadatasc_3", "ls_adatasc_3", "ls_codtpro_3", "xx_descpromo_3"
            e.Graphics.FillRectangle(Brushes.Khaki, r)

            If NTSCStr(!ls_nuovosc_3) = "S" And Not ckColoreNero.Checked Then
              e.Appearance.ForeColor = Color.Silver
            End If

            If NTSCStr(!ls_modificatosc_3) = "S" Then
              e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
            End If

            If NTSCStr(!xx_syncsc_3) = "S" Then
              e.Appearance.ForeColor = Color.IndianRed
            End If
          Case "ls_scont1_4", "ls_scont2_4", "ls_scont3_4", "ls_scont4_4", "ls_scont5_4", "ls_scont6_4", "ls_scdaquant_4", "ls_scaquant_4", "ls_tiposc_4", _
               "ls_dadatasc_4", "ls_adatasc_4", "ls_codtpro_4", "xx_descpromo_4"
            e.Graphics.FillRectangle(Brushes.Moccasin, r)

            If NTSCStr(!ls_nuovosc_4) = "S" And Not ckColoreNero.Checked Then
              e.Appearance.ForeColor = Color.Silver
            End If

            If NTSCStr(!ls_modificatosc_4) = "S" Then
              e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
            End If

            If NTSCStr(!xx_syncsc_4) = "S" Then
              e.Appearance.ForeColor = Color.IndianRed
            End If
          Case Else : e.Handled = False
        End Select
      End With

      If e.Handled Then
        r.Width -= 12
        e.Appearance.DrawString(e.Cache, e.DisplayText, r)
      End If

      'If e.Column.FieldName <> "xx_nome" And Not grvList.GetRowCellValue(e.RowHandle, e.Column.Name) Is Nothing Then
      '  If NTSCStr(grvList.GetRowCellValue(e.RowHandle, e.Column.FieldName)) = "N.C." Then
      '    e.Graphics.FillRectangle(Brushes.Bisque, r)
      '    e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
      '  End If

      '  r.Width -= 12
      '  e.Appearance.DrawString(e.Cache, e.DisplayText, r)
      '  e.Handled = True
      'End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvList_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvList.NTSFocusedRowChanged
    Try
      If grvList.NTSGetCurrentDataRow Is Nothing Then Return

      oCleStli.Salva()

      'Aggiorna l'intestazione sopra la griglia
      'Prima controllo se è cambiato, altrimenti evito di chiamare la ValCodiceDb.
      If NTSCInt(grvList.NTSGetCurrentDataRow!ls_conto) <> NTSCInt(edConto.Text) Then
        edConto.Text = NTSCStr(grvList.NTSGetCurrentDataRow()!ls_conto)
        oMenu.ValCodiceDb(edConto.Text, DittaCorrente, "ANAGRA", "N", lbDesConto.Text)
      End If

      If NTSCStr(grvList.NTSGetCurrentDataRow!ls_codart) <> NTSCStr(edArtico.Text) Then
        edArtico.Text = NTSCStr(grvList.NTSGetCurrentDataRow()!ls_codart)
        oMenu.ValCodiceDb(NTSCStr(grvList.NTSGetCurrentDataRow()!ar_codart), DittaCorrente, "ARTICO", "S", lbDesArtico.Text)
      End If

      If NTSCInt(grvList.NTSGetCurrentDataRow!ls_fase) <> NTSCInt(edFase.Text) Then
        edFase.Text = NTSCStr(grvList.NTSGetCurrentDataRow()!ls_fase)
        oMenu.ValCodiceDb(edFase.Text, DittaCorrente, "ARTFASI", "N", lbDesFase.Text, , NTSCStr(grvList.NTSGetCurrentDataRow()!ar_codart))
      End If

      LockCell(grvList.FocusedColumn.Name)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvList_NTSFocusedColumnChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles grvList.NTSFocusedColumnChanged
    Try
      LockCell(grvList.FocusedColumn.Name)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function GrvList_RowColChange(ByVal xx_unmis As NTSGridColumn) As Boolean
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Dim dcTmp As New BindingSource
    Dim strT() As String = Nothing
    Try
      If oCleStli Is Nothing Then Return True
      If dttUm Is Nothing Then Return True

      '--------------------------------------
      'compilo il combo delle unità di misura
      If Not xx_unmis.ColumnEdit Is Nothing Then
        CType(xx_unmis.ColumnEdit, NTSRepositoryItemComboBox).DataSource = dttUm
        grvList.liListCmb.Visible = False    'lo nascondo, visto che contiene tutte le unita di misura del db ...
        grList.Refresh()
        strTmp = oCleStli.GetArticoUnMis(NTSCStr(grvList.GetFocusedRowCellValue(ar_codart)))
        strT = strTmp.Split("§"c)
        dttTmp.Columns.Add("tb_codumis", GetType(String))
        dttTmp.Rows.Add(New Object() {" "})
        For i As Integer = 0 To strT.Length - 1
          dttTmp.Rows.Add(New Object() {strT(i)})
        Next
        dttTmp.AcceptChanges()
        CType(xx_unmis.ColumnEdit, NTSRepositoryItemComboBox).DataSource = dttTmp
        'attenzione: riagganciando a liListCmb il nuovo datatable viene resettato il valore contenuto nella cella della griglia:
        'memorizzo il valore corrente, disabilito la before/aftercolupdate, associo il datatable e reimposto il valore precedente
        dcTmp.DataSource = dttTmp
        For i As Integer = 0 To dcTmp.List.Count - 1
          If NTSCStr(CType(dcTmp.Current, DataRowView).Row!tb_codumis).ToUpper <> NTSCStr(grvList.GetFocusedRowCellValue(xx_unmis)).ToUpper Then
            dcTmp.MoveNext()
          Else
            Exit For
          End If
        Next
        grvList.liListCmb.DataSource = dcTmp
        '     grvList.liListCmb.Visible = True
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
#End Region

  Public Overridable Function LockCell(ByVal strCol As String) As Boolean
    Try
      'Verifico se la cella deve essere bloccata o sbloccata.
      With CType(grvList.Columns(strCol), NTSGridColumn)
        Select Case strCol
          'Listini
          Case "ls_daquant1", "ls_aquant1", "ls_unmis1", "ls_tipo1", "ls_prznet1"
            If NTSCInt(grvList.NTSGetCurrentDataRow!ls_progr1) = 0 And NTSCInt(grvList.NTSGetCurrentDataRow!ls_listino1) > 0 Then
              .Enabled = True
            Else
              .Enabled = False
            End If

            If strCol = "ls_unmis1" Then GrvList_RowColChange(CType(grvList.Columns(strCol), NTSGridColumn))
          Case "ls_daquant2", "ls_aquant2", "ls_unmis2", "ls_tipo2", "ls_prznet2"
            If NTSCInt(grvList.NTSGetCurrentDataRow!ls_progr2) = 0 And NTSCInt(grvList.NTSGetCurrentDataRow!ls_listino2) > 0 Then
              .Enabled = True
            Else
              .Enabled = False
            End If

            If strCol = "ls_unmis2" Then GrvList_RowColChange(CType(grvList.Columns(strCol), NTSGridColumn))
          Case "ls_daquant3", "ls_aquant3", "ls_unmis3", "ls_tipo3", "ls_prznet3"
            If NTSCInt(grvList.NTSGetCurrentDataRow!ls_progr3) = 0 And NTSCInt(grvList.NTSGetCurrentDataRow!ls_listino3) > 0 Then
              .Enabled = True
            Else
              .Enabled = False
            End If

            If strCol = "ls_unmis3" Then GrvList_RowColChange(CType(grvList.Columns(strCol), NTSGridColumn))
          Case "ls_daquant4", "ls_aquant4", "ls_unmis4", "ls_tipo4", "ls_prznet4"
            If NTSCInt(grvList.NTSGetCurrentDataRow!ls_progr4) = 0 And NTSCInt(grvList.NTSGetCurrentDataRow!ls_listino4) > 0 Then
              .Enabled = True
            Else
              .Enabled = False
            End If

            If strCol = "ls_unmis4" Then GrvList_RowColChange(CType(grvList.Columns(strCol), NTSGridColumn))
          Case "ls_daquant5", "ls_aquant5", "ls_unmis5", "ls_tipo5", "ls_prznet5"
            If NTSCInt(grvList.NTSGetCurrentDataRow!ls_progr5) = 0 And NTSCInt(grvList.NTSGetCurrentDataRow!ls_listino5) > 0 Then
              .Enabled = True
            Else
              .Enabled = False
            End If

            If strCol = "ls_unmis5" Then GrvList_RowColChange(CType(grvList.Columns(strCol), NTSGridColumn))
          Case "ls_prz1", "ls_dadata1"
            If NTSCInt(grvList.NTSGetCurrentDataRow!ls_listino1) > 0 Then
              .Enabled = True
            Else
              If (NTSCStr(grvList.NTSGetCurrentDataRow!ls_tipo1) = "C") Or _
                 (NTSCStr(grvList.NTSGetCurrentDataRow!ls_tipo1) = "F") Then
                .Enabled = True
              Else
                .Enabled = False
              End If
            End If
          Case "ls_prz2", "ls_dadata2"
            If NTSCInt(grvList.NTSGetCurrentDataRow!ls_listino2) > 0 Then
              .Enabled = True
            Else
              If (NTSCStr(grvList.NTSGetCurrentDataRow!ls_tipo2) = "C") Or _
                 (NTSCStr(grvList.NTSGetCurrentDataRow!ls_tipo2) = "F") Then
                .Enabled = True
              Else
                .Enabled = False
              End If
            End If
          Case "ls_prz3", "ls_dadata3"
            If NTSCInt(grvList.NTSGetCurrentDataRow!ls_listino3) > 0 Then
              .Enabled = True
            Else
              .Enabled = False
            End If
          Case "ls_prz4", "ls_dadata4"
            If NTSCInt(grvList.NTSGetCurrentDataRow!ls_listino4) > 0 Then
              .Enabled = True
            Else
              .Enabled = False
            End If
          Case "ls_prz5", "ls_dadata5"
            If NTSCInt(grvList.NTSGetCurrentDataRow!ls_listino5) > 0 Then
              .Enabled = True
            Else
              .Enabled = False
            End If
          Case "ls_listino1", "ls_codlavo1", "ls_codvalu1", "ls_codpromo1", "ls_listino2", "ls_codlavo2", "ls_codvalu2", "ls_codpromo2", _
               "ls_listino3", "ls_codlavo3", "ls_codvalu3", "ls_codpromo3", "ls_listino4", "ls_codlavo4", "ls_codvalu4", "ls_codpromo4", _
               "ls_listino5", "ls_codlavo5", "ls_codvalu5", "ls_codpromo5"
            'Queste sempre disabilitate
            .Enabled = False
          Case "ls_adata1"
            If NTSCInt(grvList.NTSGetCurrentDataRow!ls_codpromo1) = 0 Then
              .Enabled = False
            Else
              .Enabled = True
            End If
          Case "ls_adata2"
            If NTSCInt(grvList.NTSGetCurrentDataRow!ls_codpromo2) = 0 Then
              .Enabled = False
            Else
              .Enabled = True
            End If
          Case "ls_adata3"
            If NTSCInt(grvList.NTSGetCurrentDataRow!ls_codpromo3) = 0 Then
              .Enabled = False
            Else
              .Enabled = True
            End If
          Case "ls_adata4"
            If NTSCInt(grvList.NTSGetCurrentDataRow!ls_codpromo4) = 0 Then
              .Enabled = False
            Else
              .Enabled = True
            End If
          Case "ls_adata5"
            If NTSCInt(grvList.NTSGetCurrentDataRow!ls_codpromo5) = 0 Then
              .Enabled = False
            Else
              .Enabled = True
            End If

          Case "ls_perqta"
            .Enabled = False

            'Sconti
          Case "ls_scont1", "ls_scont1", "ls_scont2", "ls_scont4", "ls_scont5", "ls_scont6", _
               "ls_scont1_2", "ls_scont1_2", "ls_scont2_2", "ls_scont4_2", "ls_scont5_2", "ls_scont6_2", _
               "ls_scont1_3", "ls_scont1_3", "ls_scont2_3", "ls_scont4_3", "ls_scont5_3", "ls_scont6_3", _
               "ls_scont1_4", "ls_scont1_4", "ls_scont2_4", "ls_scont4_4", "ls_scont5_4", "ls_scont6_4"
            .Enabled = True 'Attivi a priori
          Case "ls_scdaquan", "ls_scaquant", "ls_tiposc"
            'Attivi sui nuovi sconti
            If NTSCStr(grvList.NTSGetCurrentDataRow!ls_nuovosc) = "S" Then
              .Enabled = True
            Else
              .Enabled = False
            End If
          Case "ls_scdaquan_2", "ls_scaquant_2", "ls_tiposc_2"
            'Attivi sui nuovi sconti
            If NTSCStr(grvList.NTSGetCurrentDataRow!ls_nuovosc_2) = "S" Then
              .Enabled = True
            Else
              .Enabled = False
            End If
          Case "ls_scdaquan_3", "ls_scaquant_3", "ls_tiposc_3"
            'Attivi sui nuovi sconti
            If NTSCStr(grvList.NTSGetCurrentDataRow!ls_nuovosc_3) = "S" Then
              .Enabled = True
            Else
              .Enabled = False
            End If
          Case "ls_scdaquan_4", "ls_scaquant_4", "ls_tiposc_4"
            'Attivi sui nuovi sconti
            If NTSCStr(grvList.NTSGetCurrentDataRow!ls_nuovosc_4) = "S" Then
              .Enabled = True
            Else
              .Enabled = False
            End If
          Case "ls_adatasc"
            If NTSCInt(grvList.NTSGetCurrentDataRow!ls_codtpro) = 0 Then
              .Enabled = False
            Else
              .Enabled = True
            End If
          Case "ls_adatasc_2"
            If NTSCInt(grvList.NTSGetCurrentDataRow!ls_codtpro_2) = 0 Then
              .Enabled = False
            Else
              .Enabled = True
            End If
          Case "ls_adatasc_3"
            If NTSCInt(grvList.NTSGetCurrentDataRow!ls_codtpro_3) = 0 Then
              .Enabled = False
            Else
              .Enabled = True
            End If
          Case "ls_adatasc_4"
            If NTSCInt(grvList.NTSGetCurrentDataRow!ls_codtpro_4) = 0 Then
              .Enabled = False
            Else
              .Enabled = True
            End If
        End Select
      End With
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

#Region "Eventi Toolbar"
  Public Overridable Sub tlbElabora_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbElabora.ItemClick
    Try
      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 129216875132993995, "Procedere con l'aggiornamento dei listini e degli sconti?")) = Windows.Forms.DialogResult.Yes Then
        If oCleStli.Aggiorna() Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 129216951896184986, "Aggiornamento completato correttamente."))
          Me.Close()
        Else
          oApp.MsgBoxErr(oApp.Tr(Me, 129216931807278736, "L'aggiornamento non è stato terminato."))
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      If oCleStli.bHasChanges Then
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129216099270968697, "Salvare le modifiche apportate?")) = Windows.Forms.DialogResult.Yes Then
          oCleStli.Salva()
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Try
      If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129216099915976953, "Cancellare la riga selezionata?")) = Windows.Forms.DialogResult.Yes Then
        grvList.NTSGetCurrentDataRow.Delete()
        oCleStli.Salva()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If oCleStli.bHasChanges Then
        If Not grvList.NTSRipristinaRigaCorrenteBefore(dcLise, True) Then Return
        dsLise.Tables("LISTSES").RejectChanges()
        grvList.NTSRipristinaRigaCorrenteAfter()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancellaTutto_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancellaTutto.ItemClick
    Try
      If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129216100497390645, "Attenzione!" & vbCrLf & _
              "Sei davvero sicuro di voler cancellare tutte le righe presenti nella selezione attuale?")) = Windows.Forms.DialogResult.Yes Then
        oCleStli.CancellaListSes()
        Me.Close()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick, tlbGuida.ItemClick
    Try
      SendKeys.Send("{F1}")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbVarPrzSc_Itemclick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbVarPrzSc.ItemClick
    Dim frmVmps As FRMMGVMPS = Nothing
    Dim oPar As New CLE__CLDP
    Try
      'controllo la colonna su cui sono posizionato
      If grvList.FocusedColumn.Name <> "ls_prz1" And grvList.FocusedColumn.Name <> "ls_prz2" And _
         grvList.FocusedColumn.Name <> "ls_prz3" And grvList.FocusedColumn.Name <> "ls_prz4" And _
         grvList.FocusedColumn.Name <> "ls_prz5" And grvList.FocusedColumn.Name <> "ls_scont1" And _
         grvList.FocusedColumn.Name <> "ls_scont2" And grvList.FocusedColumn.Name <> "ls_scont3" And _
         grvList.FocusedColumn.Name <> "ls_scont4" And grvList.FocusedColumn.Name <> "ls_scont5" And _
         grvList.FocusedColumn.Name <> "ls_scont6" And grvList.FocusedColumn.Name <> "ls_scont1_2" And _
         grvList.FocusedColumn.Name <> "ls_scont2_2" And grvList.FocusedColumn.Name <> "ls_scont3_2" And _
         grvList.FocusedColumn.Name <> "ls_scont4_2" And grvList.FocusedColumn.Name <> "ls_scont5_2" And _
         grvList.FocusedColumn.Name <> "ls_scont6_2" And grvList.FocusedColumn.Name <> "ls_scont1_3" And _
         grvList.FocusedColumn.Name <> "ls_scont2_3" And grvList.FocusedColumn.Name <> "ls_scont3_3" And _
         grvList.FocusedColumn.Name <> "ls_scont4_3" And grvList.FocusedColumn.Name <> "ls_scont5_3" And _
         grvList.FocusedColumn.Name <> "ls_scont6_3" And grvList.FocusedColumn.Name <> "ls_scont1_4" And _
         grvList.FocusedColumn.Name <> "ls_scont2_4" And grvList.FocusedColumn.Name <> "ls_scont3_4" And _
         grvList.FocusedColumn.Name <> "ls_scont4_4" And grvList.FocusedColumn.Name <> "ls_scont5_4" And _
         grvList.FocusedColumn.Name <> "ls_scont6_4" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 131058129611076452, "Posizionarsi prima su una colonna di tipo prezzo o sconto."))
        Return
      End If

      frmVmps = CType(NTSNewFormModal("FRMMGVMPS"), FRMMGVMPS)
      If Not frmVmps.Init(oMenu, oPar, DittaCorrente) Then Return
      frmVmps.oCleStli = oCleStli
      frmVmps.strColName = grvList.FocusedColumn.Name
      frmVmps.grvVmps = grvList

      frmVmps.ShowDialog() 'apro la form

      If frmVmps.dValoreOut = -1 Then
        Return 'annullato
      Else
        'altrimenti, se dValoreOut è valorizzato:
        Dim prz As Decimal = 0
        Dim sc As Decimal = 0
        Dim i As Integer = 0
        Try
          If frmVmps.bPrz = True Then 'per il prezzo:
            For i = frmVmps.grvVmps.RowCount - 1 To 0 Step -1
              'per ogni record che visualizzo, sostituisco o aggiungo il prezzo indicato
              If frmVmps.opSostituisci.Checked Then
                frmVmps.grvVmps.SetRowCellValue(i, frmVmps.strColName, frmVmps.dValoreOut)
              ElseIf frmVmps.opAggiungi.Checked Then
                prz = NTSCDec(frmVmps.grvVmps.GetRowCellValue(i, frmVmps.strColName))
                frmVmps.grvVmps.SetRowCellValue(i, frmVmps.strColName, prz + frmVmps.dValoreOut)
              End If
            Next
            Return
          ElseIf frmVmps.bPrz = False Then 'per lo sconto
            For i = frmVmps.grvVmps.RowCount - 1 To 0 Step -1
              'per ogni record che visualizzo, sostituisco o aggiungo lo sconto indicato
              If frmVmps.opSostituisci.Checked Then
                frmVmps.grvVmps.SetRowCellValue(i, frmVmps.strColName, frmVmps.dValoreOut)
              ElseIf frmVmps.opAggiungi.Checked Then
                sc = NTSCDec(frmVmps.grvVmps.GetRowCellValue(i, frmVmps.strColName))
                frmVmps.grvVmps.SetRowCellValue(i, frmVmps.strColName, sc + frmVmps.dValoreOut)
              End If
            Next
            Return
          End If
        Catch ex As Exception
          '-------------------------------------------------
          Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
          '-------------------------------------------------	
        End Try
      End If

      Return

    Catch ex As Exception

      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	

    Finally
      If Not frmVmps Is Nothing Then frmVmps.Dispose()
      frmVmps = Nothing
    End Try

  End Sub
#End Region

  Public Overridable Sub BloccoColonne()
    Try
      If ckListino1.Checked Then
        ls_codlavo1.Visible = bLavo1
        xx_desclavo1.Visible = bLavo1
        ls_codvalu1.Visible = bValuta1
        xx_descvalu1.Visible = bValuta1
        ls_codpromo1.Visible = bPromo1
        xx_descpromo1.Visible = bPromo1
      End If

      If ckListino2.Checked Then
        ls_codlavo2.Visible = bLavo2
        xx_desclavo2.Visible = bLavo2
        ls_codvalu2.Visible = bValuta2
        xx_descvalu2.Visible = bValuta2
        ls_codpromo2.Visible = bPromo2
        xx_descpromo2.Visible = bPromo2
      End If

      If ckListino3.Checked Then
        ls_codlavo3.Visible = bLavo3
        xx_desclavo3.Visible = bLavo3
        ls_codvalu3.Visible = bValuta3
        xx_descvalu3.Visible = bValuta3
        ls_codpromo3.Visible = bPromo3
        xx_descpromo3.Visible = bPromo3
      End If

      If ckListino4.Checked Then
        ls_codlavo4.Visible = bLavo4
        xx_desclavo4.Visible = bLavo4
        ls_codvalu4.Visible = bValuta4
        xx_descvalu4.Visible = bValuta4
        ls_codpromo4.Visible = bPromo4
        xx_descpromo4.Visible = bPromo4
      End If

      If ckListino5.Checked Then
        ls_codlavo5.Visible = bLavo5
        xx_desclavo5.Visible = bLavo5
        ls_codvalu5.Visible = bValuta5
        xx_descvalu5.Visible = bValuta5
        ls_codpromo5.Visible = bPromo5
        xx_descpromo5.Visible = bPromo5
      End If

      If ckSconti1.Checked Then
        ls_codtpro.Visible = bPromoSc1
        xx_descpromo.Visible = bPromoSc1
      End If

      If ckSconti2.Checked Then
        ls_codtpro_2.Visible = bPromoSc2
        xx_descpromo_2.Visible = bPromoSc2
      End If

      If ckSconti3.Checked Then
        ls_codtpro_3.Visible = bPromoSc3
        xx_descpromo_3.Visible = bPromoSc3
      End If

      If ckSconti4.Checked Then
        ls_codtpro_4.Visible = bPromoSc4
        xx_descpromo_4.Visible = bPromoSc4
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function AssegnaColoriGriglia(ByRef dtrT As DataRow) As Boolean
    'coloro le celle
    Dim i As Integer = 0
    Dim nColor As Integer = 0
    Try
      'quando viene cambiato il colore della cella backcolor_row o della backcolor_*, 
      'sbs invia al client la segnalazione di 'cella cambiata'
      'su SBC la cellace backcolor_ non viene mai disegnata, ma viene utilizzata per colorare la riga/cella
      'NB: la colonna backcolor_ deve essere inserita anche nella griglia (magari non visibile) altrimenti non viene passata a SBC!!!

      'dtrIn.Table.Columns.Add("backcolor_row", GetType(Integer))        'costante 'backcolor_row' utilizzato da SBC per colorare tutta la riga
      'dtrIn.Table.Columns.Add("backcolor_xx_status", GetType(Integer))  'prefisso 'backcolor_' utilizzato da SBC per colorare la singola cella

      If dtrT Is Nothing Then Return True

      'memorizzo lo stato prima della modifica del campo
      Dim oState As DataRowState = dtrT.RowState
      Dim bOk1 As Boolean = oCleStli.bHasChanges

      If dtrT.Table.Columns.Contains("backcolor_ls_listino1") = False Then
        For i = 1 To 5
          dtrT.Table.Columns.Add("backcolor_ls_listino" & i.ToString, GetType(Integer))
          dtrT.Table.Columns.Add("backcolor_ls_prz" & i.ToString, GetType(Integer))
          dtrT.Table.Columns.Add("backcolor_ls_daquant" & i.ToString, GetType(Integer))
          dtrT.Table.Columns.Add("backcolor_ls_aquant" & i.ToString, GetType(Integer))
          dtrT.Table.Columns.Add("backcolor_ls_unmis" & i.ToString, GetType(Integer))
          dtrT.Table.Columns.Add("backcolor_ls_codlavo" & i.ToString, GetType(Integer))
          dtrT.Table.Columns.Add("backcolor_xx_desclavo" & i.ToString, GetType(Integer))
          dtrT.Table.Columns.Add("backcolor_ls_tipo" & i.ToString, GetType(Integer))
          dtrT.Table.Columns.Add("backcolor_ls_dadata" & i.ToString, GetType(Integer))
          dtrT.Table.Columns.Add("backcolor_ls_adata" & i.ToString, GetType(Integer))
          dtrT.Table.Columns.Add("backcolor_ls_codvaluo" & i.ToString, GetType(Integer))
          dtrT.Table.Columns.Add("backcolor_xx_descvalu" & i.ToString, GetType(Integer))
          dtrT.Table.Columns.Add("backcolor_ls_prznet" & i.ToString, GetType(Integer))
          dtrT.Table.Columns.Add("backcolor_ls_codpromo" & i.ToString, GetType(Integer))
          dtrT.Table.Columns.Add("backcolor_xx_descpromo" & i.ToString, GetType(Integer))
        Next
        For i = 1 To 4
          dtrT.Table.Columns.Add("backcolor_ls_scont1_" & i.ToString, GetType(Integer))
          dtrT.Table.Columns.Add("backcolor_ls_scont2_" & i.ToString, GetType(Integer))
          dtrT.Table.Columns.Add("backcolor_ls_scont3_" & i.ToString, GetType(Integer))
          dtrT.Table.Columns.Add("backcolor_ls_scont4_" & i.ToString, GetType(Integer))
          dtrT.Table.Columns.Add("backcolor_ls_scont5_" & i.ToString, GetType(Integer))
          dtrT.Table.Columns.Add("backcolor_ls_scont6_" & i.ToString, GetType(Integer))
          dtrT.Table.Columns.Add("backcolor_ls_scdaquant_" & i.ToString, GetType(Integer))
          dtrT.Table.Columns.Add("backcolor_ls_scaquant_" & i.ToString, GetType(Integer))
          dtrT.Table.Columns.Add("backcolor_ls_tiposc_" & i.ToString, GetType(Integer))
          dtrT.Table.Columns.Add("backcolor_ls_dadatasc_" & i.ToString, GetType(Integer))
          dtrT.Table.Columns.Add("backcolor_ls_adatasc_" & i.ToString, GetType(Integer))
          dtrT.Table.Columns.Add("backcolor_ls_codtpro_" & i.ToString, GetType(Integer))
          dtrT.Table.Columns.Add("backcolor_xx_descpromo_" & i.ToString, GetType(Integer))
        Next
      End If

      'per default non è
      For i = 1 To 5
        Select Case i
          Case 1 : nColor = Color.MediumAquamarine.ToArgb
          Case 2 : nColor = Color.Aquamarine.ToArgb
          Case 3 : nColor = Color.LightSkyBlue.ToArgb
          Case 4 : nColor = Color.PaleTurquoise.ToArgb
          Case 5 : nColor = Color.Honeydew.ToArgb
        End Select
        dtrT("backcolor_ls_listino" & i.ToString) = nColor
        dtrT("backcolor_ls_prz" & i.ToString) = nColor
        dtrT("backcolor_ls_daquant" & i.ToString) = nColor
        dtrT("backcolor_ls_aquant" & i.ToString) = nColor
        dtrT("backcolor_ls_unmis" & i.ToString) = nColor
        dtrT("backcolor_ls_codlavo" & i.ToString) = nColor
        dtrT("backcolor_xx_desclavo" & i.ToString) = nColor
        dtrT("backcolor_ls_tipo" & i.ToString) = nColor
        dtrT("backcolor_ls_dadata" & i.ToString) = nColor
        dtrT("backcolor_ls_adata" & i.ToString) = nColor
        dtrT("backcolor_ls_codvaluo" & i.ToString) = nColor
        dtrT("backcolor_xx_descvalu" & i.ToString) = nColor
        dtrT("backcolor_ls_prznet" & i.ToString) = nColor
        dtrT("backcolor_ls_codpromo" & i.ToString) = nColor
        dtrT("backcolor_xx_descpromo" & i.ToString) = nColor
      Next
      For i = 1 To 4
        Select Case i
          Case 1 : nColor = Color.Yellow.ToArgb
          Case 2 : nColor = Color.Gold.ToArgb
          Case 3 : nColor = Color.Khaki.ToArgb
          Case 4 : nColor = Color.Moccasin.ToArgb
        End Select
        dtrT("backcolor_ls_scont1_" & i.ToString) = nColor
        dtrT("backcolor_ls_scont2_" & i.ToString) = nColor
        dtrT("backcolor_ls_scont3_" & i.ToString) = nColor
        dtrT("backcolor_ls_scont4_" & i.ToString) = nColor
        dtrT("backcolor_ls_scont5_" & i.ToString) = nColor
        dtrT("backcolor_ls_scont6_" & i.ToString) = nColor
        dtrT("backcolor_ls_scdaquant_" & i.ToString) = nColor
        dtrT("backcolor_ls_scaquant_" & i.ToString) = nColor
        dtrT("backcolor_ls_tiposc_" & i.ToString) = nColor
        dtrT("backcolor_ls_dadatasc_" & i.ToString) = nColor
        dtrT("backcolor_ls_adatasc_" & i.ToString) = nColor
        dtrT("backcolor_ls_codtpro_" & i.ToString) = nColor
        dtrT("backcolor_xx_descpromo_" & i.ToString) = nColor
      Next

      'reimposto lo stato a prima della modifica del campo
      If oState = DataRowState.Unchanged Then dtrT.AcceptChanges()
      oCleStli.bHasChanges = bOk1

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
End Class
